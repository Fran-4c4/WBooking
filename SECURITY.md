# Security policy

Report suspected authentication bypasses, SQL injection, exposed secrets,
payment-flow problems, unsafe file uploads, cross-site scripting or personal
data leaks through GitHub private vulnerability reporting once enabled. If it
is unavailable, request a private contact channel without publishing exploit
details or sensitive records.

Use synthetic accounts, local email capture, test payment credentials and a
disposable database. Never attach a production backup, customer dataset or
live connection string to an issue.

The current code predates modern secure defaults. Successful compilation and
local execution do not constitute a security audit. Known review areas include
credential storage, dynamic SQL, session handling, upload paths, email and
payment integrations, cryptography, dependency vulnerabilities and output
encoding.
