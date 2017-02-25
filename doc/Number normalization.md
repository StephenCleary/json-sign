In the [JSON standard](http://www.ecma-international.org/publications/files/ECMA-ST/ECMA-404.pdf), numbers are decimal floating-point values with arbitrary precision. In order to allow for all possible JSON numbers, the number normalization algorithm is treated as a string manipulation algorithm as much as possible. The exponent portion does require numeric manipulation, but it may be treated as an arbitrary-precision decimal integer rather than an arbitrary-precision decimal floating-point.

First, parse the number (as a string) according to this regular expression, which includes all valid JSON numbers:

```
(-)?(0|([1-9][0-9]*))(\.([0-9]+))?([Ee]([-+]?[0-9]+))?
S   I                   F              E
```

There are four components of note:

- `S` The sign of the number. Note that `S` may only be `"-"` or `""`; it cannot be `"+"`.
- `I` The integral portion of the number. Note that `I` is never `""`.
- `F` The fractional portion of the number. If there is no decimal point, than `F` is `""`.
- `E` The exponent. Note that `E` is parsed as an arbitrary-precision decimal integer value, not as a string. If there is no exponent in the input, then `E` is set to `0`.

Using these components, the number normalization algorithm is:

1. Remove trailing `"0"` characters from `F`.
2. While (`I` is equal to `"0"` and `F` is not equal to `""`):
   1. Remove first character from `F`.
   2. Set `I` to that character.
   3. Decrement `E`.
3. While (`length(I)` is greater than `1`):
   1. Remove last character from `I`.
   2. If that character is `"0"` and `F` is equal to `""`, discard that character. Otherwise, prepend that character to `F`.
   3. Increment `E`.
4. Output `S`.
5. Output `I`.
6. If (`I` is equal to `"0"`) then stop.
7. If (`F` is not equal to `""`) then:
   1. Output the character `"."`.
   2. Output `F`.
8. If (`E` is not equal to `0`) then:
   1. Output the character `"e"`.
   2. Output `E`.
      - If `E` is positive, then do *not* output a sign prefix.
	  - Do not output any leading `0` characters for `E`.