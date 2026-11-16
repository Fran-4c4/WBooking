(function (window, document) {
  "use strict";

  // Public configuration contract. These are the single authoritative defaults
  // used by both automatic initialization and WBookingWidget.init().
  var DEFAULTS = Object.freeze({
    language: "2",       // WBooking language ID: one of 1..8.
    minLeadDays: 1,       // Whole days from today to the earliest check-in.
    defaultStayDays: 3    // Whole nights used only when check-out is empty.
  });
  var LANGUAGE_IDS = /^(1|2|3|4|5|6|7|8)$/;
  var HOTEL_CODE = /^\d+$/;

  function addDays(date, days) {
    var result = new Date(date.getFullYear(), date.getMonth(), date.getDate());
    result.setDate(result.getDate() + days);
    return result;
  }

  function formatDate(date) {
    var month = String(date.getMonth() + 1).padStart(2, "0");
    var day = String(date.getDate()).padStart(2, "0");
    return date.getFullYear() + "-" + month + "-" + day;
  }

  function parseDate(value) {
    var parts = value.split("-");
    if (parts.length !== 3) return null;
    var date = new Date(Number(parts[0]), Number(parts[1]) - 1, Number(parts[2]));
    return formatDate(date) === value ? date : null;
  }

  function readInteger(form, attribute, fallback, minimum, maximum) {
    var raw = form.getAttribute(attribute);
    if (raw === null || raw === "") return fallback;
    if (!/^\d+$/.test(raw)) throw new Error(attribute + " must be a whole number.");
    var value = Number(raw);
    if (value < minimum || value > maximum) {
      throw new Error(attribute + " must be between " + minimum + " and " + maximum + ".");
    }
    return value;
  }

  function ensureHidden(form, name) {
    var input = form.querySelector('input[type="hidden"][name="' + name + '"]');
    if (!input) {
      input = document.createElement("input");
      input.type = "hidden";
      input.name = name;
      form.appendChild(input);
    }
    return input;
  }

  function setDateParts(form, date, suffix) {
    ensureHidden(form, "fechad" + suffix).value = String(date.getDate());
    ensureHidden(form, "fecham" + suffix).value = String(date.getMonth() + 1);
    ensureHidden(form, "fechay" + suffix).value = String(date.getFullYear());
  }

  function showConfigurationError(form, message) {
    var output = form.querySelector("[data-wbooking-error]");
    if (output) {
      output.textContent = message;
      output.hidden = false;
    }
    var submit = form.querySelector('[type="submit"]');
    if (submit) submit.disabled = true;
    form.setAttribute("data-wbooking-state", "error");
    if (window.console && window.console.error) window.console.error("WBooking widget: " + message);
  }

  function init(form) {
    if (!form || form.getAttribute("data-wbooking-initialized") === "true") return form;

    try {
      var endpointValue = form.getAttribute("action");
      var hotelCode = form.getAttribute("data-hotel-code");
      var language = form.getAttribute("data-language") || DEFAULTS.language;
      var minLeadDays = readInteger(form, "data-min-lead-days", DEFAULTS.minLeadDays, 0, 365);
      var defaultStayDays = readInteger(form, "data-default-stay-days", DEFAULTS.defaultStayDays, 1, 60);
      var checkIn = form.querySelector("[data-wbooking-check-in]");
      var checkOut = form.querySelector("[data-wbooking-check-out]");

      if (!endpointValue) throw new Error("The form action must identify b_Request.aspx.");
      var endpoint = new URL(endpointValue, window.location.href);
      if (endpoint.protocol !== "http:" && endpoint.protocol !== "https:") {
        throw new Error("The form action must use HTTP or HTTPS.");
      }
      if (!hotelCode || !HOTEL_CODE.test(hotelCode)) {
        throw new Error("data-hotel-code must contain an existing numeric hotel code.");
      }
      if (!LANGUAGE_IDS.test(language)) {
        throw new Error("data-language must be a WBooking language ID from 1 to 8.");
      }
      if (!checkIn || !checkOut) {
        throw new Error("The form must contain check-in and check-out fields.");
      }

      endpoint.searchParams.set("hotelcode", hotelCode);
      endpoint.searchParams.set("language", language);
      form.action = endpoint.href;
      form.method = "post";

      var today = new Date();
      var earliestCheckIn = addDays(today, minLeadDays);
      checkIn.min = formatDate(earliestCheckIn);
      if (!checkIn.value) checkIn.value = checkIn.min;

      function updateCheckOut() {
        var arrival = parseDate(checkIn.value);
        if (!arrival) return;
        var earliestCheckOut = addDays(arrival, 1);
        checkOut.min = formatDate(earliestCheckOut);
        if (!checkOut.value || checkOut.value <= checkIn.value) {
          checkOut.value = formatDate(addDays(arrival, defaultStayDays));
        }
        checkOut.setCustomValidity("");
      }

      checkIn.addEventListener("change", updateCheckOut);
      updateCheckOut();

      form.addEventListener("submit", function (event) {
        var arrival = parseDate(checkIn.value);
        var departure = parseDate(checkOut.value);
        checkOut.setCustomValidity("");
        if (!arrival || !departure || departure <= arrival) {
          event.preventDefault();
          checkOut.setCustomValidity("Check-out must be later than check-in.");
          checkOut.reportValidity();
          return;
        }
        setDateParts(form, arrival, "1");
        setDateParts(form, departure, "2");
      });

      form.setAttribute("data-wbooking-initialized", "true");
      form.setAttribute("data-wbooking-state", "ready");
      return form;
    } catch (error) {
      showConfigurationError(form, error.message);
      return form;
    }
  }

  function initAll(root) {
    var forms = (root || document).querySelectorAll("form[data-wbooking-widget]");
    Array.prototype.forEach.call(forms, init);
    return forms;
  }

  window.WBookingWidget = Object.freeze({
    defaults: DEFAULTS,
    init: init,
    initAll: initAll
  });

  if (document.readyState === "loading") {
    document.addEventListener("DOMContentLoaded", function () { initAll(document); });
  } else {
    initAll(document);
  }
}(window, document));
