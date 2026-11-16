# Contributing

Start with an issue describing the existing behavior, the intended behavior
and a minimal reproduction using synthetic data. Keep pull requests focused
and explain any compatibility effect on the Web Forms UI, booking engine or
stored procedures.

Do not submit real customer records, database backups, credentials, API keys,
payment data, certificates, production URLs or email archives. Security issues
must be reported privately as described in [SECURITY.md](SECURITY.md).

Before submitting a change:

1. Restore dependencies from NuGet; do not commit `packages/` or copied DLLs.
2. Build both VB.NET libraries and precompile the website in an x86 process.
3. Test database changes against a disposable SQL Server instance populated
   only with sanitized fixtures.
4. Preserve behavior unless the pull request explicitly documents a migration.
5. Record user-visible or compatibility changes in `CHANGELOG.md`.

Contributions are provided under the repository's MIT license. Preserve
third-party notices and disclose the origin and license of new assets or code.
