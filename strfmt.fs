variable min-field-width
variable left-justify

: pad-left ( dst c-addr u -- dst c-addr u )
left-justify @ if exit then
dup min-field-width @ < if \ insert space
min-field-width @ over - >r
rot dup r@ bl fill r> + -rot then ;

: pad-right ( dst u -- dst )
tuck + swap left-justify @ if
min-field-width @ swap - >r r@ 0> if
dup r@ bl fill r@ + then rdrop
else drop then ;

: add-field ( dst c-addr u -- dst )
pad-left
>r over r@ move r> \ dst u
pad-right ;

: strfmt-n ( n dst -- dst )
swap dup s>d dabs <# #s rot sign #> add-field ;
: strfmt-u ( u dst -- dst )
swap 0 <# #s #> add-field ;
: strfmt-dn ( d dst -- dst )
-rot tuck dabs <# #s rot sign #> add-field ;
: strfmt-du ( d dst -- dst )
-rot <# #s #> add-field ;

: parse-min-field-width ( src srcend -- src srcend )
over c@ '-' = dup if rot 1+ -rot then left-justify !
base @ >r decimal
over - >r >r 0 0 r> r>
>number rot drop rot min-field-width !
over + r> base ! ;

variable charbuf 1 allot

: parse-cmdspec ( dst src srcend -- dst src srcend )
swap 1+ swap parse-min-field-width
>r dup >r c@ case
'%' of s" %" add-field endof
'c' of swap charbuf c! charbuf 1 add-field endof
'n' of strfmt-n endof
'u' of strfmt-u endof
's' of -rot add-field endof
'd' of r> 1+ dup >r c@ case
    'n' of strfmt-dn endof
    'u' of strfmt-du endof
endcase endof endcase r> r> ;

\ Prints into buffer c-addr2 using the format string at c-addr1 u.
\ caddr2 u3 is the resulting string.
: strfmt ( c-addr1 u1 c-addr2 -- caddr2 u3 )
dup >r -rot over + \ dst src srcend
begin 2dup < while
over c@ '%' = if parse-cmdspec
else -rot 2dup c@ swap c! -rot 1+ -rot then
swap 1+ swap repeat
2drop r> tuck - ;
