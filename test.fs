require tester.fs
require strfmt.fs

testing strfmt wordset
hex

100 value bufsize
variable buf bufsize allot

0 INVERT            CONSTANT MAX-UINT
0 INVERT 1 RSHIFT       CONSTANT MAX-INT
0 INVERT 1 RSHIFT INVERT    CONSTANT MIN-INT
0 INVERT 1 RSHIFT       CONSTANT MID-UINT
0 INVERT 1 RSHIFT INVERT    CONSTANT MID-UINT+1

\ empty string
T{ s" " buf strfmt -> buf 0 }T

\ hai
T{ s" hai" buf strfmt s" hai" compare -> 0 }T

\ test %c
T{ 'r' 'a' 'h' 'c' s" %c%c%c%c" buf strfmt s" char" compare -> 0 }T

\ test %%
T{ s" %%" buf strfmt s" %" compare -> 0 }T

max-uint ffffffffffffffff = constant 64-bit

\ test %n
T{ 0 s" %n" buf strfmt s" 0" compare -> 0 }T
T{ max-uint s" %n" buf strfmt s" -1" compare -> 0 }T
64-bit [if]
    T{ max-int s" %n" buf strfmt s" 7FFFFFFFFFFFFFFF" compare -> 0 }T
    T{ min-int s" %n" buf strfmt s" -8000000000000000" compare -> 0 }T
[then]

\ test %u
T{ 0 s" %u" buf strfmt s" 0" compare -> 0 }T
64-bit [if]
    T{ max-uint s" %u" buf strfmt s" FFFFFFFFFFFFFFFF" compare -> 0 }T
[then]

\ test %dn
T{ 0 0 s" %dn" buf strfmt s" 0" compare -> 0 }T
T{ max-uint max-uint s" %dn" buf strfmt s" -1" compare -> 0 }T
64-bit [if]
    T{ max-uint max-int s" %dn" buf strfmt s" 7FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF" compare -> 0 }T
    T{ 0 min-int s" %dn" buf strfmt s" -80000000000000000000000000000000" compare -> 0 }T
[then]

\ test %du
T{ 0 0 s" %du" buf strfmt s" 0" compare -> 0 }T
64-bit [if]
    T{ max-uint max-uint s" %du" buf strfmt s" FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF" compare -> 0 }T
[then]

\ test %s
T{ s" bar" s" foo" s" %s%s" buf strfmt s" foobar" compare -> 0 }T

\ min field width
T{ 0 s" %2n" buf strfmt s"  0" compare -> 0 }T

\ Random tests
T{ 10 s" Joe" s" %s has a %n%% discount!" buf strfmt s" Joe has a 10% discount!" compare -> 0 }T
