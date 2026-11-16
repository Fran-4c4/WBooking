# WBooking

WBooking is a hotel and vehicle booking system built with ASP.NET Web Forms
and Visual Basic .NET. It contains the public booking flow, a hotel-scoped
administration interface, the booking engine and its database helper.

It compiles and includes a sanitized demo
environment.

## Repository layout

- `Web30/` — ASP.NET Web Site with the public and hotel-administration UIs.
- `CFR.BookEngine/` — booking domain and data-access library.
- `ind.DBHelper/` — shared SQL Server helper library.
- `Web30.sln` — Visual Studio solution for the website and both libraries.
- `database/` — sanitized demonstration fixture.
- `scripts/` and `docker-compose.sqlserver.yml` — local development and smoke test tooling.
- `docs/` — product architecture, configuration, integration and security documentation.

The supported baseline is Windows, x86 application pools, .NET Framework 4.8
and SQL Server.

## Prerequisites

- Windows 10/11 or Windows Server.
- Visual Studio 2022 or Build Tools with .NET Framework 4.8 developer tools.
- NuGet CLI for the ASP.NET Web Site's `packages.config` restoration.
- Docker Desktop, or a compatible SQL Server 2019/2022 installation.
- IIS Express or IIS configured for a 32-bit application pool.

## Restore and build

Restore the booking-engine `PackageReference` dependencies, then restore the
ASP.NET Web Site packages into the solution-level `packages` directory. The Web
Site has no project file, so solution restore alone cannot restore its
`packages.config`:

```powershell
msbuild .\CFR.BookEngine\CFR.BookEngine.vbproj /t:Restore
nuget restore .\Web30\packages.config -PackagesDirectory .\packages
```

Build the complete solution with the x86 configuration:

```powershell
msbuild .\Web30.sln /t:Rebuild /p:Configuration=Release /p:Platform=x86
```

Alternatively, validate the website directly with the 32-bit ASP.NET compiler:

```powershell
$out = Join-Path $env:TEMP 'wbooking-precompiled'
& "$env:WINDIR\Microsoft.NET\Framework\v4.0.30319\aspnet_compiler.exe" `
  -v / -p .\Web30 -f $out
```

The current source still produces compiler warnings. A successful compilation
does not by itself establish security or functional correctness.

## Run the demonstration environment

The application relies on SQL Server tables and stored procedures. The
repository contains only a deliberately limited, sanitized demonstration
fixture; it contains no production credentials or customer records. See the
[database documentation](docs/database.md) for its data contract.

With Docker and IIS Express installed:

```powershell
.\scripts\Initialize-DemoSqlServer.ps1
.\scripts\Start-WBooking.ps1 -Database wbooking_demo
.\scripts\Test-LocalEnvironment.ps1 -Database wbooking_demo
```

Open <http://localhost:8080/Admin/Login.aspx>. Use `admin` / `admin` for full
hotel configuration and availability, or `demo` / `demo` for the restricted
reception profile. These credentials are valid only in the isolated fixture.

The Demo Hotel booking flow is available at
<http://localhost:8080/b_Request.aspx?hotelcode=280316>.

The customer-site example at
<http://localhost:8080/examples/booking-widget.html> demonstrates a responsive
availability bar that opens matching room results in WBooking. See the
[booking integration guide](docs/embedding.md) for the request contract and
deployment requirements.

Stop the website while retaining SQL Server and its Docker volume:

```powershell
.\scripts\Stop-LocalEnvironment.ps1
```

Add `-StopSqlServer` to stop the SQL Server container as well. Neither option
deletes the database volume.

## Documentation

- [Architecture](docs/architecture.md)
- [Database setup and fixture](docs/database.md)
- [Embedding the booking flow](docs/embedding.md)
- [Password storage and recovery](docs/passwords-and-recovery.md)
- [Payment-card security](docs/payment-card-security.md)
- [Contributing](CONTRIBUTING.md)
- [Security policy](SECURITY.md)

## Security

WBooking processes authentication, personal data, email and payment-related
configuration. Read [SECURITY.md](SECURITY.md) before testing or reporting a
problem. Use only synthetic identities, test payment credentials and isolated
local infrastructure.

Administration and operator passwords use irreversible salted hashes. The
offline reset procedure is documented in
[password storage and recovery](docs/passwords-and-recovery.md).

Installations are encouraged not to store card numbers and to use a hosted
payment provider instead. The demonstration database contains no real customer
or card data, and CVV storage is not supported; see
[payment-card security](docs/payment-card-security.md).

Room images and other runtime-owned files are kept outside the website under
the configured `DataDirectory` (`/data/cfrbook` by default). Ensure the IIS
application-pool identity can write there and include it in backups; see
[architecture](docs/architecture.md#persistent-application-data).

Confirmation email supplies guests with a single-use cancellation code. Only
its SHA-256 hash is stored, and the code is consumed after a successful
cancellation; see [architecture](docs/architecture.md#web30).

## License

WBooking project code is licensed under the [MIT License](LICENSE). Bundled
third-party libraries, themes, fonts and scripts retain their own licenses;
see [THIRD_PARTY_NOTICES.md](THIRD_PARTY_NOTICES.md). The third-party inventory
must be completed before publishing a stable release.

## Author

[Francisco Rafael Moreno Santana](https://github.com/Fran-4c4)
