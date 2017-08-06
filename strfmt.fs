variable min-field-width

: right-justify ( dst c-addr u -- dst c-addr u )
dup min-field-width @ < if \ insert space
min-field-width @ over - >r
rot dup r@ bl fill r> + -rot then ;

: left-justify ;

: move-content ( dst c-addr u -- dst )
right-justify
>r over r@ move r> +
left-justify ;

: strfmt-n ( n dst -- dst )
swap dup s>d dabs <# #s rot sign #> move-content ;
: strfmt-u ( u dst -- dst )
swap 0 <# #s #> move-content ;
: strfmt-dn ( d dst -- dst )
-rot tuck dabs <# #s rot sign #> move-content ;
: strfmt-du ( d dst -- dst )
-rot <# #s #> move-content ;
: strfmt-s ( c-addr u dst -- dst )
-rot move-content ;

: parse-min-field-width ( src srcend -- src srcend )
base @ >r decimal
over - >r >r 0 0 r> r>
>number rot drop rot min-field-width !
over + r> base ! ;

: parse-cmdspec ( dst src srcend -- dst src srcend )
swap 1+ swap parse-min-field-width
>r dup >r c@ case
'%' of '%' over c! 1+ endof
'c' of tuck c! 1+ endof
'n' of strfmt-n endof
'u' of strfmt-u endof
's' of strfmt-s endof
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
