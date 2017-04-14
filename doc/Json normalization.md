[JSON](http://www.json.org/) is used for countless applications, as it strikes a nice balance between ease of (computer) processing and ease of (human) understanding.

JSON itself is quite flexible, and allows multiple representations for the same data. This document defines *normalized JSON*, which defines a single (normalized) representation for any given JSON data. Normalized JSON is capable of representing any JSON value. At the same time, normalized JSON is a strict subset of JSON; in other words, every normalized JSON document is a JSON document.

Converting a JSON document to a normalized JSON document can be done using a recursive algorithm:

- If the current JSON node is an object, then:
  1. Normalize all [string keys](String normalization.md) of the object.
  2. Order the normalized keys ordinally, preserving escape sequences.
  3. Normalize the values of each key.
- If the current JSON node is an array, then normalize the items of the array.
- If the current JSON node is a string, then [normalize the string](String normalization.md).
- If the current JSON node is a number, then [normalize the number](Number normalization.md).
- If the current JSON node is `true`, `false`, or `null`, then it is already normalized.

Normalized JSON has no insignificant whitespace.

Notes:
- Normalized JSON does distinguish between `-0` and `+0`.