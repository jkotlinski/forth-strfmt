require tester.fs
require strfmt.fs

testing strfmt wordset
hex

0 INVERT            CONSTANT MAX-UINT
0 INVERT 1 RSHIFT       CONSTANT MAX-INT
0 INVERT 1 RSHIFT INVERT    CONSTANT MIN-INT
0 INVERT 1 RSHIFT       CONSTANT MID-UINT
0 INVERT 1 RSHIFT INVERT    CONSTANT MID-UINT+1

\ empty string
T{ s" " pad strfmt s" " compare -> 0 }T

\ hai
T{ s" hai" pad strfmt s" hai" compare -> 0 }T

\ test %c
T{ 'r' 'a' 'h' 'c' s" %c%c%c%c" pad strfmt s" char" compare -> 0 }T

\ test %%
T{ s" %%" pad strfmt s" %" compare -> 0 }T

max-uint ffffffffffffffff = constant 64-bit

\ test %n
T{ 0 s" %n" pad strfmt s" 0" compare -> 0 }T
T{ max-uint s" %n" pad strfmt s" -1" compare -> 0 }T
64-bit [if]
    T{ max-int s" %n" pad strfmt s" 7FFFFFFFFFFFFFFF" compare -> 0 }T
    T{ min-int s" %n" pad strfmt s" -8000000000000000" compare -> 0 }T
[then]

\ test %u
T{ 0 s" %u" pad strfmt s" 0" compare -> 0 }T
64-bit [if]
    T{ max-uint s" %u" pad strfmt s" FFFFFFFFFFFFFFFF" compare -> 0 }T
[then]

\ test %dn
T{ 0 0 s" %dn" pad strfmt s" 0" compare -> 0 }T
T{ max-uint max-uint s" %dn" pad strfmt s" -1" compare -> 0 }T
64-bit [if]
    T{ max-uint max-int s" %dn" pad strfmt s" 7FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF" compare -> 0 }T
    T{ 0 min-int s" %dn" pad strfmt s" -80000000000000000000000000000000" compare -> 0 }T
[then]

\ test %du
T{ 0 0 s" %du" pad strfmt s" 0" compare -> 0 }T
64-bit [if]
    T{ max-uint max-uint s" %du" pad strfmt s" FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF" compare -> 0 }T
[then]

\ test %s
T{ s" bar" s" foo" s" %s%s" pad strfmt s" foobar" compare -> 0 }T

\ right justify
T{ 0 s" %2n" pad strfmt s"  0" compare -> 0 }T
T{ 'a' s" %2c" pad strfmt s"  a" compare -> 0 }T
T{ s" %2%" pad strfmt s"  %" compare -> 0 }T
T{ s" bar" s" %6s" pad strfmt s"    bar" compare -> 0 }T

\ left justify
T{ 0 s" %-2n" pad strfmt s" 0 " compare -> 0 }T
T{ 'a' s" %-2c" pad strfmt s" a " compare -> 0 }T
T{ s" %-2%" pad strfmt s" % " compare -> 0 }T
T{ s" bar" s" %-6s" pad strfmt s" bar   " compare -> 0 }T

\ Random tests
T{ 10 s" Joe" s" %s has a %n%% discount!" pad strfmt s" Joe has a 10% discount!" compare -> 0 }T
