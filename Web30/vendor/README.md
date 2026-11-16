# Local browser dependencies

WBooking serves these files locally so its user interfaces do not depend on a
public content-delivery network at runtime.

| Directory | Component | Version | Origin | License |
| --- | --- | ---: | --- | --- |
| `bootstrap-3.2.0` | Bootstrap | 3.2.0 | `github.com/twbs/bootstrap` | MIT |
| `font-awesome-4.5.0` | Font Awesome | 4.5.0 | `github.com/FortAwesome/Font-Awesome` | CSS: MIT; font: SIL OFL 1.1 |
| `jquery-1.11.1` | jQuery | 1.11.1 | `jquery.com` | MIT |
| `jquery-ui-1.11.4` | jQuery UI with Smoothness theme | 1.11.4 | `jqueryui.com` | MIT |

Version and license headers are preserved in the distributed CSS and
JavaScript files. The repository-wide third-party inventory is maintained in
`THIRD_PARTY_NOTICES.md`; the Font Awesome font license text is included as
`font-awesome-4.5.0/OFL-1.1.txt`.

The former Google Font imports were removed because none of those font
families was used by the retained styles. The request-statistics page uses the
small local chart implementation in `Admin/js/wbooking-chart.js` instead of
loading Google Charts.
