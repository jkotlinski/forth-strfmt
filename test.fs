require tester.fs
require printf.fs

testing sprintf wordset
hex

0 INVERT                    CONSTANT MAX-UINT
0 INVERT 1 RSHIFT           CONSTANT MAX-INT
0 INVERT 1 RSHIFT INVERT    CONSTANT MIN-INT
0 INVERT 1 RSHIFT           CONSTANT MID-UINT
0 INVERT 1 RSHIFT INVERT    CONSTANT MID-UINT+1

\ empty string
T{ s" " pad sprintf s" " compare -> 0 }T

\ hai
T{ s" hai" pad sprintf s" hai" compare -> 0 }T

\ test %c
T{ 'r' 'a' 'h' 'c' s" %c%c%c%c" pad sprintf s" char" compare -> 0 }T

\ test %%
T{ s" %%" pad sprintf s" %" compare -> 0 }T

max-uint ffffffffffffffff = constant 64-bit

\ test %n
T{ 0 s" %n" pad sprintf s" 0" compare -> 0 }T
T{ max-uint s" %n" pad sprintf s" -1" compare -> 0 }T
64-bit [if]
    T{ max-int s" %n" pad sprintf s" 7FFFFFFFFFFFFFFF" compare -> 0 }T
    T{ min-int s" %n" pad sprintf s" -8000000000000000" compare -> 0 }T
[then]

\ test %u
T{ 0 s" %u" pad sprintf s" 0" compare -> 0 }T
64-bit [if]
    T{ max-uint s" %u" pad sprintf s" FFFFFFFFFFFFFFFF" compare -> 0 }T
[then]

\ test %dn
T{ 0 0 s" %dn" pad sprintf s" 0" compare -> 0 }T
T{ max-uint max-uint s" %dn" pad sprintf s" -1" compare -> 0 }T
64-bit [if]
    T{ max-uint max-int s" %dn" pad sprintf s" 7FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF" compare -> 0 }T
    T{ 0 min-int s" %dn" pad sprintf s" -80000000000000000000000000000000" compare -> 0 }T
[then]

\ test %du
T{ 0 0 s" %du" pad sprintf s" 0" compare -> 0 }T
64-bit [if]
    T{ max-uint max-uint s" %du" pad sprintf s" FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF" compare -> 0 }T
[then]

\ test %s
T{ s" bar" s" foo" s" %s%s" pad sprintf s" foobar" compare -> 0 }T

\ right justify
T{ 0 s" %2n" pad sprintf s"  0" compare -> 0 }T
T{ 'a' s" %2c" pad sprintf s"  a" compare -> 0 }T
T{ s" %2%" pad sprintf s"  %" compare -> 0 }T
T{ s" bar" s" %6s" pad sprintf s"    bar" compare -> 0 }T
T{ s" foobar" s" %3s" pad sprintf s" foobar" compare -> 0 }T
T{ 1 s" %02n" pad sprintf s" 01" compare -> 0 }T
T{ 'a' s" %02c" pad sprintf s" 0a" compare -> 0 }T
T{ s" %02%" pad sprintf s" 0%" compare -> 0 }T
T{ s" bar" s" %06s" pad sprintf s" 000bar" compare -> 0 }T
T{ s" foobar" s" %03s" pad sprintf s" foobar" compare -> 0 }T

\ left justify
T{ 0 s" %-2n" pad sprintf s" 0 " compare -> 0 }T
T{ 'a' s" %-2c" pad sprintf s" a " compare -> 0 }T
T{ s" %-2%" pad sprintf s" % " compare -> 0 }T
T{ s" bar" s" %-6s" pad sprintf s" bar   " compare -> 0 }T
T{ s" foobar" s" %-3s" pad sprintf s" foobar" compare -> 0 }T
T{ 1 s" %-02n" pad sprintf s" 1 " compare -> 0 }T
T{ 'a' s" %-02c" pad sprintf s" a " compare -> 0 }T
T{ s" %-02%" pad sprintf s" % " compare -> 0 }T
T{ s" bar" s" %-06s" pad sprintf s" bar   " compare -> 0 }T
T{ s" foobar" s" %-03s" pad sprintf s" foobar" compare -> 0 }T

\ printf tests
cr ." The text 'Joe has a 10% discount!' should appear below." cr
10 s" Joe" s" %s has a %n%% discount!" printf
