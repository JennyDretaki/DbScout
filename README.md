# DbScoutBaby

A pastel-themed C# WinForms explorer for large Microsoft SQL Server environments.


## Search coverage

DbScoutBaby searches metadata for:

- Tables
- Columns
- Views
- Stored Procedures
- Functions
- Triggers
- Synonyms
- Sequences
- Constraints
- Indexes
- User-defined Types
- SQL Server Agent Jobs
- SQL Server Agent Alerts
- SQL Server Agent Operators
- SQL source code / module definitions

It intentionally does not scan application/business records.

## Performance

Metadata is loaded once and cached in memory. After the first metadata load, repeated searches are much faster.

Use **Refresh Metadata Cache** whenever database objects or Agent objects have changed.

## SQL Agent bug fix

SQL Server Agent `sysoperators.enabled` may be exposed as `tinyint`. This version explicitly casts it to `bit` and safely converts the returned value to `bool`, avoiding the previous:

`Unable to cast object of type 'System.Byte' to type 'System.Boolean'`

error.

## UI

The interface uses soft pastel colors:

- Lavender
- Baby blue
- Mint
- Blush pink
- Warm cream

## Security

Do not commit passwords or sensitive SQL connection credentials to a public repository.
