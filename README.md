strfmt ( c-addr1 u1 c-addr2 -- caddr2 u3 )

Prints into buffer c-addr2 using the format string at c-addr1 u.
caddr2 u3 is the resulting string.

Format specifiers:

    %% - %
    %c - character
    %n - signed number
    %u - unsigned number
    %dn - double-cell signed number
    %du - double-cell unsigned number
    %s - string

Example:

    > 10 s" Joe" s" %s has a %n%% discount!" pad strfmt type
    Joe has a 10% discount! ok
