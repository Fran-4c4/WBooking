# Embed the booking engine

WBooking's public entry point is `b_Request.aspx`. An installation can expose
it as a link, inside an `iframe`, as the target of a minimal HTML form, or
through the responsive search-bar component included in
[`Web30/examples/`](../Web30/examples/). The component collects only the search
criteria; the reservation remains inside WBooking.

## Minimum configuration

Select the hotel with `hotelcode` and the language with the numeric `language`
identifier:

```text
https://booking.example.com/b_Request.aspx?hotelcode=280316&language=2
```

Replace the domain, `hotelcode`, and language as appropriate. Do not copy a
`site` value from another installation: it is optional and must be sent only
when that hotel has a matching site configuration.

| Parameter | Required | Contract |
| --- | --- | --- |
| `hotelcode` | Yes | Code of an existing, active hotel. |
| `language` | No | `1` Spanish, `2` English, `3` German, `4` French, `5` Italian, `6` Catalan, `7` Portuguese, `8` Japanese. |
| `site` | No | Code of a visual configuration owned by the hotel. Omitting it is safer. |

## Reusable search-bar component

Copy `wbooking-widget.js` and, optionally, `wbooking-widget.css` from
`Web30/examples/` to the customer website. The script has no third-party
dependencies. It progressively enhances one or more ordinary HTML forms and
posts the selected criteria to WBooking. If JavaScript fails, the form remains
visible but does not invent partial date parameters.

```html
<link rel="stylesheet" href="/assets/wbooking-widget.css">
<script src="/assets/wbooking-widget.js" defer></script>

<form class="wbooking-widget"
      data-wbooking-widget
      data-hotel-code="280316"
      data-language="2"
      data-min-lead-days="1"
      data-default-stay-days="3"
      action="https://booking.example.com/b_Request.aspx"
      target="_blank">
  <label class="wbooking-widget__field">
    Check-in
    <input type="date" data-wbooking-check-in required>
  </label>
  <label class="wbooking-widget__field">
    Check-out
    <input type="date" data-wbooking-check-out required>
  </label>
  <label class="wbooking-widget__field">
    Adults
    <select name="f_numAdultos">
      <option value="1">1</option>
      <option value="2" selected>2</option>
    </select>
  </label>
  <input type="hidden" name="f_ninosPago" value="0">
  <input type="hidden" name="f_ninosfree" value="0">
  <input type="hidden" name="f_regimen" value="1">
  <button type="submit">Check availability</button>
  <p class="wbooking-widget__error" data-wbooking-error role="alert" hidden></p>
</form>
```

The form owns the component's public configuration contract:

| Field | Default | Contract and effect |
| --- | --- | --- |
| `action` | None | Required HTTP(S) URL of `b_Request.aspx`; relative URLs are resolved against the customer page. |
| `data-hotel-code` | None | Required numeric code of an existing hotel; it becomes the `hotelcode` request parameter. |
| `data-language` | `2` | WBooking language ID from `1` to `8`; any other value is rejected. |
| `data-min-lead-days` | `1` | Whole days from today to the earliest check-in, from `0` to `365`. |
| `data-default-stay-days` | `3` | Whole nights used only when check-out is empty, from `1` to `60`. If check-in moves beyond check-out, the component recalculates check-out with this value. |
| `target` | Browser default | Standard HTML form target. Use `_blank` to open WBooking in a new tab or omit it to continue in the same tab. |

Invalid or contradictory configuration disables the submit button and writes
the reason to the element marked `data-wbooking-error`; values are not silently
clamped or replaced. `window.WBookingWidget.init(form)` initializes a form
added after page load, while `initAll(root)` initializes all matching forms
under a document or element.

### Link or button

```html
<a href="https://booking.example.com/b_Request.aspx?hotelcode=280316&amp;language=2"
   target="_blank" rel="noopener">
  Book now
</a>
```

### Iframe

```html
<iframe
  src="https://booking.example.com/b_Request.aspx?hotelcode=280316&amp;language=2"
  title="Booking engine"
  style="width:100%;min-height:720px;border:0">
</iframe>
```

### External form

The engine accepts search criteria through `POST` or the query string. When
both are present, `POST` takes precedence. Every date component must be sent
together:

| Field | Contract |
| --- | --- |
| `fechad1`, `fecham1`, `fechay1` | Gregorian check-in day, month, and year. |
| `fechad2`, `fecham2`, `fechay2` | Gregorian check-out day, month, and year; it must be later than check-in. |
| `f_numAdultos` | From `1` to `5`. |
| `f_ninosPago` | From `0` to `5`. |
| `f_ninosfree` | From `0` to `2`. |
| `f_regimen` | `1` room only, `2` bed and breakfast, `3` half board, `4` full board, `5` all inclusive. The selected option must be enabled for the hotel. |

Minimal example with pre-split date values:

```html
<form method="post"
      action="https://booking.example.com/b_Request.aspx?hotelcode=280316&amp;language=2">
  <input type="hidden" name="fechad1" value="10">
  <input type="hidden" name="fecham1" value="6">
  <input type="hidden" name="fechay1" value="2027">
  <input type="hidden" name="fechad2" value="13">
  <input type="hidden" name="fecham2" value="6">
  <input type="hidden" name="fechay2" value="2027">
  <input type="hidden" name="f_numAdultos" value="2">
  <input type="hidden" name="f_ninosPago" value="0">
  <input type="hidden" name="f_ninosfree" value="0">
  <input type="hidden" name="f_regimen" value="1">
  <button type="submit">Check availability</button>
</form>
```

External date fields instruct WBooking to validate the complete search and run
the same availability lookup as the engine's search button. The response shows
matching rooms or the engine's normal availability message. It does not create
a reservation; the guest still selects a room and completes the booking inside
WBooking. A request containing only `hotelcode` and `language` continues to
show the normal search screen without running a lookup.

## Run the local example

Start the website as described in the repository README and open:

```text
http://localhost:8080/examples/booking-widget.html
```

The included example uses Demo Hotel (`280316`) from 20 to 23 June 2027. The
fixture contains matching seasons, prices and allotment for those dates. Edit
the form's documented configuration attributes and date values to test another
installation.

## Data required by an installation

A valid `hotelcode` is enough to display the entry point, but not enough to
offer rooms. Before testing real search results, the database must contain at
least:

- hotel parameters and meal plans;
- room types and rates;
- seasons and periods that include the requested dates;
- allotment or availability for those dates;
- applicable daily prices.

Moving season dates alone does not generate future allotment or prices. The
example therefore verifies the integration contract and preloading behavior,
while the selected database determines whether the search returns inventory.

## Deployment and security

- Use an absolute HTTPS URL for both the hotel website and WBooking.
- For a cross-origin `iframe`, deliberately configure the
  `Content-Security-Policy`/`frame-ancestors` headers and the proxy or server's
  cookie policy. Do not expose the application with an accidentally permissive
  policy.
- Expose only the required public pages; keep administration protected and
  separate.
- Do not connect live data, email, or payment gateways until the security
  review described in `SECURITY.md` is complete.
