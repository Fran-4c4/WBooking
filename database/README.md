# Sanitized demo database

`wbooking_demo.bak` is a SQL Server 2022 backup containing the application schema
and a deliberately small Demo Hotel fixture. It was produced from
a schema-only clone with statistics and Query Store omitted; real rows were
never copied into the target and then deleted.

The fixture contains one hotel, two hotel-administration users and three
transformed reservations. `admin` / `admin` has the `Control Total Sistema`
permission; `demo` / `demo` retains a restricted reception profile.
Names/contact data and birth dates are replaced or removed, amounts and
occupancy are synthetic, and stay dates are shifted into the future.
Payment-card fields, TPV rows, customer-account tables, request baskets,
sessions and access/operation logs are empty. Constraints are revalidated and
`DBCC CHECKDB` runs before the backup is emitted.

The `BookingCancellationToken` table is part of the initial schema and is
empty in the fixture. It stores only SHA-256 token hashes and UTC lifecycle
timestamps when confirmation email is generated. Historical reusable
cancellation login/password columns are empty and are not used by the public
workflow.

Both local credential pairs are intentionally trivial and must never be reused
outside an isolated development environment. Passwords are stored as salted,
versioned hashes rather than plaintext.

The public fixture is restored by `scripts/Initialize-DemoSqlServer.ps1`. It is
the initial database definition for the open-source project and already
contains the schema expected by the current source.
