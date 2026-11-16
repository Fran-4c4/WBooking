# Password storage and account recovery

## Password storage

Hotel-administration passwords and operator/agency
passwords are stored as salted, versioned
PBKDF2-HMAC-SHA256 hashes.

The hash is deliberately irreversible. WBooking can verify a supplied password
or replace it, but it cannot display, decrypt, or email the original password.
Each stored value records its work factor so the hashing parameters can evolve
without losing access to existing accounts.

Promotion codes are not passwords.

## Create and change passwords

A full-control hotel administrator can open **System users**, edit a user, and
enter a new password. Existing passwords are never populated into the form.
Leaving the password field empty keeps the current hash.

Operator and agency passwords are managed from their administration form. The
stored hash is never sent to the browser. An empty password field keeps the
current password; entering a value replaces it with a new hash.

An administrator cannot tell a user what their current password is. If the user
has forgotten it, the correct operation is to set a new password.

## Database backups

Database backups contain password hashes rather than recoverable passwords.
After restoring a backup, users authenticate with the same passwords, but the
backup does not make those passwords visible.

Access to a database backup still requires protection: an attacker can attempt
offline password guesses against stored hashes. Use strong passwords, restrict
backup access, and apply the same retention and encryption controls used for
other sensitive operational data.

## Emergency administrator recovery

If no hotel administrator can log in, run the offline recovery command from
the application server:

```powershell
.\scripts\Reset-AdminPassword.ps1 -Login admin -Database cfrbook
```

The command prompts twice without echoing the password, stores only its hash,
and revokes the selected user's existing sessions. It reads
`WBOOKING_SQL_CONNECTION_STRING`; in the repository's local environment it can
also use the ignored `.local/sqlserver/.env` file. It never prints the password
or hash.

Access to this command and the database is equivalent to administrator recovery
authority. Restrict operating-system and database permissions accordingly and
record its use in operational procedures.

The password column sizes and write procedures are part of the application
schema included in the demonstration database.
