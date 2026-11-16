(function (window, document) {
    "use strict";

    var colors = ["#337ab7", "#5cb85c", "#f0ad4e"];

    function appendText(parent, className, value) {
        var element = document.createElement("span");
        element.className = className;
        element.textContent = value;
        parent.appendChild(element);
        return element;
    }

    window.renderWBookingBarChart = function (elementId, table, options) {
        var container = document.getElementById(elementId);
        if (!container || !table || table.length < 2) {
            return;
        }

        var headings = table[0].slice(1);
        var rows = table.slice(1);
        var maximum = 1;
        var rowIndex;
        var valueIndex;

        for (rowIndex = 0; rowIndex < rows.length; rowIndex += 1) {
            for (valueIndex = 1; valueIndex < rows[rowIndex].length; valueIndex += 1) {
                maximum = Math.max(maximum, Number(rows[rowIndex][valueIndex]) || 0);
            }
        }

        container.innerHTML = "";
        container.className = "wb-local-chart";
        appendText(container, "wb-local-chart-title", options.title || "");
        appendText(container, "wb-local-chart-subtitle", options.subtitle || "");

        var legend = document.createElement("div");
        legend.className = "wb-local-chart-legend";
        headings.forEach(function (heading, index) {
            var item = document.createElement("span");
            item.style.borderLeftColor = colors[index % colors.length];
            item.textContent = heading;
            legend.appendChild(item);
        });
        container.appendChild(legend);

        rows.forEach(function (row) {
            var group = document.createElement("div");
            group.className = "wb-local-chart-group";
            appendText(group, "wb-local-chart-date", row[0]);

            headings.forEach(function (heading, index) {
                var value = Number(row[index + 1]) || 0;
                var series = document.createElement("div");
                series.className = "wb-local-chart-series";
                series.setAttribute("aria-label", heading + ": " + value);

                var bar = document.createElement("span");
                bar.className = "wb-local-chart-bar";
                bar.style.backgroundColor = colors[index % colors.length];
                bar.style.width = ((value / maximum) * 100) + "%";
                series.appendChild(bar);
                appendText(series, "wb-local-chart-value", value);
                group.appendChild(series);
            });
            container.appendChild(group);
        });
    };
}(window, document));
