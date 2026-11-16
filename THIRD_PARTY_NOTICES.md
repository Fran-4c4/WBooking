# Third-party notices

The MIT license in the repository root applies only to WBooking-authored code
and assets. Third-party components remain subject to their own licenses.

## NuGet dependencies

These packages are declared in `Web30/packages.config` or
`CFR.BookEngine/CFR.BookEngine.vbproj` and are downloaded during restore; their
package contents are not committed to this repository.

| Component | Version | License |
| --- | ---: | --- |
| Ajax Control Toolkit | 15.1.4.0 | BSD 3-Clause |
| Entity Framework | 6.1.3 | MIT |
| HtmlSanitizer | 11.0.26 | MIT |
| log4net | 3.5.0 | Apache License 2.0 |
| Newtonsoft.Json | 13.0.3 | MIT |

NuGet may restore additional transitive dependencies. Their license metadata
and notices are included in the restored packages and continue to apply.

## Distributed browser assets

| Component | Version | Location | License |
| --- | ---: | --- | --- |
| Bootstrap | 3.2.0 and 3.3.5 | `Web30/vendor/bootstrap-3.2.0`, `Web30/Admin/bootstrap` | MIT |
| Bootstrap Datepicker | 1.4.0 | `Web30/js/dt`, `Web30/Css/dt` | Apache License 2.0 |
| Bootstrap Switch | 3.3.2 | `Web30/Admin/plugins/bootstrap-switch` | MIT |
| CKEditor | 3.6.6.2 | `Web30/ckeditor` | GPL/LGPL/MPL tri-license; see `Web30/ckeditor/LICENSE.html` |
| Font Awesome | 4.5.0 | `Web30/vendor/font-awesome-4.5.0` | Font: SIL OFL 1.1; CSS: MIT |
| jQuery | 1.11.1 | `Web30/vendor/jquery-1.11.1`, `Web30/Admin/plugins` | MIT |
| jQuery UI | 1.11.1 and 1.11.4 | `Web30/Admin/plugins`, `Web30/vendor/jquery-ui-1.11.4` | MIT |
| jQuery Validation | 1.13.1 | `Web30/Admin/plugins` | MIT |
| Magnific Popup | 1.0.0 | `Web30/plugins/magnific-popup` | MIT |
| Moment.js | 2.11.1 | `Web30/js/momentjs.js` | MIT |
| Owl Carousel | 1.3.3 | `Web30/plugins/owl-carousel` | MIT |

Runtime CSS, JavaScript and font assets are served by WBooking itself. The
application does not require a public content-delivery network to render its
pages. Links in source comments or license headers identify upstream projects;
they are not runtime dependencies.

This inventory covers the third-party components intentionally retained in the
published source tree. Generated restore/build directories are excluded from
version control.
