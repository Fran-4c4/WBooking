# Database setup

WBooking uses SQL Server. The
application database is `cfrbook` by default.

## Connection configuration

When `WBOOKING_SQL_CONNECTION_STRING` is unset or whitespace,
`Web30/web.config` owns the runtime database settings. The Entity Framework
connection named `cfrbookEntities` targets `localhost`, database `cfrbook`,
with Windows authentication. Its name and metadata must be preserved because
parameterless contexts resolve it by name.

`WBOOKING_SQL_CONNECTION_STRING` is an optional process-level SQL Server
provider connection string. When present, it overrides both Entity Framework
and the `ind.bk.DBMgr` compatibility settings. It must contain non-empty `Data Source`
and `Initial Catalog` fields; malformed or incomplete values are rejected. The
local scripts construct it from the ignored `.local/sqlserver/.env` file and
never log its value.

The `ind.bk.DBMgr` compatibility path uses these settings:

| Setting        | Meaning                     | Checked-in value             |
| -------------- | --------------------------- | ---------------------------- |
| `DDBBSERVER`   | SQL Server host or instance | `localhost`                  |
| `DDBBCATALOG`  | Database name               | `cfrbook`                    |
| `DDBBUSERID`   | SQL login                   | `wbooking`                   |
| `DDBBPASSWORD` | SQL login password          | empty, intentionally invalid |

All four values are required when the environment override is absent. An empty
value raises a configuration error before a `DBMgr` query is issued.

## Sanitized demonstration fixture

`database/wbooking_demo.bak` is the public development fixture. It is a SQL
Server 2022 backup containing the schema and the stored procedures and views
required by the application, with deliberately limited data:

- one generic property, `Demo Hotel` (`280316`);
- restricted reception user `demo` / `demo`;
- full-control hotel administrator `admin` / `admin`;
- three reservations with fictional identity, contact, occupancy and amount
  fields;
- seasons, prices and allotment for the documented 20–23 June 2027 widget
  search;
- no TPV rows, card values, customer accounts, request baskets, sessions or
  access/operation logs.

The credentials are intentionally trivial and must be used only in the
isolated demonstration environment.

Restore, start and test the fixture with:

```powershell
.\scripts\Initialize-DemoSqlServer.ps1
.\scripts\Start-WBooking.ps1 -Database wbooking_demo
.\scripts\Test-LocalEnvironment.ps1 -Database wbooking_demo
```

`Initialize-DemoSqlServer.ps1` uses SQL port `14333` by default and creates
random local SQL passwords in `.local/sqlserver/.env`. It preserves an existing
demo database unless `-ResetDatabase` is supplied. The explicit
`-Database wbooking_demo` argument is required because the runtime tools retain
`cfrbook` as their compatibility default.

The fixture was built from a schema-only clone by copying only explicitly
selected and transformed rows. Its constraints were revalidated, `DBCC CHECKDB`
was run, and sensitive tables and fields were checked before publication.

The backup is the initial database definition for this open-source release; no
earlier public WBooking database is required. Password semantics and emergency
administrator recovery are documented in
[passwords-and-recovery.md](passwords-and-recovery.md).

## Data policy

Use only synthetic or explicitly sanitized data in development, tests, issue
reports and distributable database artifacts. Never commit customer records,
live credentials, payment data, production connection strings or operational
backups.
