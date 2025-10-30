---

applyTo: "VokabelTrainer/*.cs"

---

Use Hungarian notation. The prefix for bool is "b", the prefix for date and time is "dtm", the prefix for float is "f", the prefix for double is "dbl", the prefix for byte is "by", prefix for string is "str", the prefix for ints is "n", the prefix for longs is "l", the prefix for chars is "c", the prefix for arrays is "a", the prefix of arrays of strings is "astr", the prefix for text boxes is "tbx", the prefix for labels is "lbl", the prefix for check boxes is "chk", the prefix for buttons is "btn", the prefix for other controls is "ctl", the prefix for other objects is "o". If variables represent physical values, (e.g. pixels, meters, inches, circular degrees, etc.) then they shall have an additional prefix that represents the units of the variable. Member variables have an additional prefix "m_". Static member variables have an additional prefix "s_", instead of "m_".

The XML is saved manually in a way people can easily read it and also manually formatted, so the first and the second word are always at the same positions in the lines.

Document the source code, functions, and their parameters. The documentation must be in English. Be generous in commenting the code, explain the situation and what shall be done.

If a string is compared to null, then use == or != comparison. If a string is compared to another string that is not null, then use functions CompareTo or Equals.

When a string shall be composed of several parts, please put the result to a string variable, before using it. Explicitly declare that the variable is of type string.

When a function with more than two parameters shall be called, please use multiline syntax for calling, put different parameters into different lines, so the line doesn't become too long.

Explicitly declare the types of variables, don't use "var".

If an object is created that implements IDisposable interface, then please take care of its disposal. Either it must be stored in another object that will dispose it, or you need to dispose it explicitly after usage.

If a COM object is created then please take care of its release. Please take care about reference count of COM objects.

Please take care of localization/internationalization of source code. The used strings need to be taken from resources, so they are localizable for different cultures.

Avoid known security issues.
