## Normalized strings

Normalized strings have limited character escape sequences, and strict rules about which characters are and are not escaped. Normalized strings in their encoded form use only ASCII characters.

Only the following escape sequences are recognized:

- `\"` for a quotation mark (U+0022).
- `\\` for a backslash (U+005C).
- `\n` for a line feed (U+000A).
- `\r` for a carriage return (U+000D).

Furthermore, those code points *must* be encoded using those escape seuqences.

These JSON escape sequences are not supported by normalized strings: `\/`, `\b`, `\f`, and `\t`.

## Algorithm

Treat the source JSON string as a sequence of UTF-16 code units. For each code unit:

- If it is U+0022, U+005C, U+000A, or U+000D; then output the corresponding escape sequence `\"`, `\\`, `\n`, or `\r`.
- Otherwise, if it is in the inclusive range U+0020-U+007E; then output that code point directly (as a single-byte UTF-16/UTF-8/ASCII code unit).
- Otherwise, output the UTF-16 code unit as an escape sequence `\uXXXX`, where each `X` is an uppercase hexadecimal digit (U+0030 through U+0039 or U+0041 through U+0046).

## Examples

The JSON spec indicates that these strings are all equivalent: `"\u002F"`, `"\u002f"`, `"\/"`, `"/"`. Only the last string (`"/"`) is a normalized string.