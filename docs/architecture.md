# Architecture

## Runtime shape

WBooking is an ASP.NET Web Forms application hosted as a Web Site rather than
an MSBuild web application project. Pages and `App_Code` are compiled by
ASP.NET; reusable logic is supplied by two VB.NET class libraries.

```text
Web30 (public booking and hotel-administration UI)
  -> CFR.BookEngine (booking domain, managers and generated data objects)
       -> ind.DBHelper (ADO.NET SQL Server helpers)
  -> SQL Server stored procedures and tables
  -> email and payment-gateway integrations
```

## Components

### Web30

The public flow is centered on `B_request.aspx`, cancellation, confirmation and
payment-return pages. `Web30/Admin` contains hotel, room, tariff, availability,
promotion, customer and reporting screens. The site uses server-rendered Web
Forms controls with Bootstrap and jQuery assets.

The demonstration booking entry point is
`/b_Request.aspx?hotelcode=280316`. The public entry accepts search parameters
by query string or POST, validates dates, occupancy and meal plan, and then
displays the normal search and confirmation flow. Supported fields and
embedding patterns are documented in [embedding.md](embedding.md).

Guest cancellation uses `B_Cancel.aspx`. Each client confirmation email
contains the booking number, a 256-bit single-use cancellation code, and a link
to the form. The code is not placed in the URL. SQL Server stores only its
SHA-256 hash, expiry and consumption timestamps. A successful cancellation
consumes it; invalid attempts are limited independently by IP address and
booking number. `EnablePublicCancellation=0` can intentionally turn this route
off, but the checked-in default is enabled.

`/Admin/Login.aspx` is the per-hotel administration interface. Each user
belongs to a hotel and receives a hotel-scoped session. Permission `1`, named
`Control Total Sistema`, bypasses individual section checks but does not grant
access outside that hotel.

WBooking does not include a global SaaS control plane for registering customer
companies, assigning hotels or generating platform billing. Deployments that
need those functions must provide them separately.

### CFR.BookEngine

The engine contains booking managers, domain objects, generated table managers,
Entity Framework models and UI helpers shared by the website. Its persistence
layer is coupled to SQL Server and stored procedures.

### ind.DBHelper

The helper wraps ADO.NET connections, commands, parameters and transactions.
Its source is included so the solution builds without another checkout.

## Persistent application data

Runtime-owned files live below the absolute `DataDirectory` configured in
`Web30/web.config`. The checked-in default is `/data/cfrbook`, which resolves to
`C:\data\cfrbook` on Windows. WBooking owns the fixed `images` child directory
and serves its generated JPEG files through `RoomImage.ashx`; files are not
written below the executable web tree.

The application-pool identity must have read/write access to this directory.
Back up this data directory together with SQL Server, and move its existing
contents before changing the setting. An empty or relative value is rejected.

## Runtime constraints

- The supported deployment model is Windows and x86.
- The application targets .NET Framework 4.8.
- SQL Server is the supported database provider.
- The supported built-in payment methods are bank transfer and direct payment
  at the hotel. Online card payment requires a separate hosted-provider adapter.
- The Web Forms pages combine presentation and business orchestration, so
  behavioral changes require regression testing across both layers.
- Email and payment integrations must use test doubles or sandbox endpoints in
  development environments.
