# Payment-card data

WBooking's schema includes fields for a primary account number (PAN), expiry
date, and cardholder name in reservation and operator records.

The included booking flow does not collect a PAN, expiry date, cardholder name,
or CVV/CVC. Its supported payment methods are bank transfer and direct payment
at the hotel. The administrator rejects the historical card and TPV method
values, and public booking submission fails closed if such a value is present
in an imported database.

The historical payment endpoints remain unavailable. They are not a provider
integration and must not be enabled in a public deployment.

## Recommended approach: do not store card numbers

Most installations should use a hosted payment page or provider-controlled
fields so WBooking receives a payment-method token rather than a PAN. Keep card
entry outside the Web Forms application and store only the information needed
for the integration, such as:

- the provider and payment-method token;
- card brand and last four digits for identification;
- expiry only when the provider requires it;
- transaction, authorization, or setup-intent identifiers.

No Stripe, PayPal, Redsys, or bank adapter is included. An adapter must follow
the provider's current hosted-checkout and signed-notification protocol, bind
the verified amount and currency to a server-side pending order, and make
booking creation idempotent. Redirecting to an arbitrary URL is not a payment
integration.

For a guarantee or charge-later workflow, select a provider that supports
authorizations, setup intents, or reusable payment methods. A CVV must not be
retained to reproduce a later card-not-present transaction.

## Installations that need a recoverable PAN

Some workflows, such as an OTA virtual card or a deferred hotel charge, may
require authorized staff to recover the complete PAN. A one-way password hash
cannot support that requirement. Prefer a dedicated card vault operated by a
payment provider.

If an installation deliberately implements local encryption, its design should
include at least:

- authenticated encryption, such as AES-256-GCM, with a fresh nonce per record;
- envelope encryption or an external KMS/HSM-backed key;
- a key identifier beside the ciphertext to support rotation;
- master keys stored and backed up separately from SQL Server;
- masked display by default and narrowly authorized reveal;
- audit logging and a documented retention/deletion period;
- no CVV column or value in the encrypted payload.

A SQL Server backup would contain ciphertext, not its encryption key. Recovery
therefore requires a separately protected key backup. Placing the key in the
same database or source repository defeats that separation.

## Deployment responsibility

WBooking is distributed as-is. The organization deploying it is responsible
for deciding whether its workflow needs card data and for meeting the security,
privacy, contractual, and regulatory requirements that apply to that
deployment.

Reasonable deployment choices include:

- using the included transfer or direct-payment workflow;
- removing or leaving unused all card-storage fields;
- integrating a hosted payment gateway and storing tokens only;
- implementing an appropriate card vault for a justified recoverable-PAN flow;
- maintaining the required behavior in a fork;
- proposing a general-purpose change to the WBooking project.

The presence of card-related schema fields is not a recommendation to collect
or retain card data. New demonstration fixtures and tests must continue to use
synthetic identities and empty card fields.

Official PCI Security Standards Council guidance on card verification codes:
<https://www.pcisecuritystandards.org/faqs/1280/>.
