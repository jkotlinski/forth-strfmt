variable min-field-width
create left-justify 1 allot
create pad-char 1 allot
create charbuf 1 allot

: pad-left ( dst c-addr u -- dst c-addr u )
left-justify c@ if exit then dup min-field-width @ < if
min-field-width @ over - >r rot dup r@ pad-char c@ fill r> + -rot then ;

: pad-right ( dst u -- dst )
tuck + swap left-justify c@ if min-field-width @ swap - >r r@ 0> if
dup r@ bl fill r@ + then rdrop else drop then ;

: add-field ( dst c-addr u -- dst ) pad-left >r over r@ move r> pad-right ;
: sprintf-n ( n dst -- dst ) swap dup s>d dabs <# #s rot sign #> add-field ;
: sprintf-u ( u dst -- dst ) swap 0 <# #s #> add-field ;
: sprintf-dn ( d dst -- dst ) -rot tuck dabs <# #s rot sign #> add-field ;
: sprintf-du ( du dst -- dst ) -rot <# #s #> add-field ;

: parse-min-field-width ( src srcend -- src srcend )
over c@ '-' = dup if rot 1+ -rot then left-justify c!
over c@ '0' = if swap 1+ swap '0' else bl then pad-char c!
base @ >r decimal over - 0 -rot 0 -rot >number
rot drop rot min-field-width ! over + r> base ! ;

: parse-cmdspec ( dst src srcend -- dst src srcend )
swap 1+ swap parse-min-field-width >r dup >r c@ case
'%' of s" %" add-field endof
'c' of swap charbuf c! charbuf 1 add-field endof
'n' of sprintf-n endof
'u' of sprintf-u endof
's' of -rot add-field endof
'd' of r> 1+ dup >r c@ case
    'n' of sprintf-dn endof
    'u' of sprintf-du endof
endcase endof endcase r> r> ;

\ Prints n*x into buffer c-addr2 using the format string at c-addr1 u.
\ caddr2 u3 is the resulting string.
: sprintf ( n*x c-addr1 u1 c-addr2 -- caddr2 u3 )
dup >r -rot over + begin 2dup < while over c@ '%' = if parse-cmdspec else
-rot 2dup c@ swap c! -rot 1+ -rot then swap 1+ swap repeat 2drop r> tuck - ;

\ Prints n*x using the format string at c-addr u.
: printf ( n*x c-addr u -- ) pad sprintf type ;
