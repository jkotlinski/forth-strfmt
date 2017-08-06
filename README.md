strfmt ( c-addr1 u1 c-addr2 -- caddr2 u3 )

Prints into buffer c-addr2 using the format string at c-addr1 u.
caddr2 u3 is the resulting string.

The format string contains ordinary characters (except %), which are
copied unchanged to the destination buffer, and conversion specifications.
Conversion specifications have the following format:

 * Introductory % character
 * An optional - that specifies left justify
 * An optional 0 that left-pads using 0 instead of space
 * An optional decimal integer value that specifies minimum field width
 * A conversion format specifier

The following format specifiers are available:

    % - %
    c - character
    n - signed number
    u - unsigned number
    dn - double-cell signed number
    du - double-cell unsigned number
    s - string (c-addr u)

Examples:

    > 10 s" Joe" s" %s has a %n%% discount!" pad strfmt type
    Joe has a 10% discount! ok
    > 10 s" %05n" pad strfmt type
    00010 ok
    > s" spaced" s" %-10s out" pad strfmt type
    spaced     out ok
