: move-and-inc-dst ( dst c-addr u -- dst )
>r over r@ move r> + ;
: strfmt-n ( n dst -- dst )
swap dup s>d dabs <# #s rot sign #> move-and-inc-dst ;
: strfmt-u ( u dst -- dst )
swap 0 <# #s #> move-and-inc-dst ;
: strfmt-dn ( d dst -- dst )
-rot tuck dabs <# #s rot sign #> move-and-inc-dst ;
: strfmt-du ( d dst -- dst )
-rot <# #s #> move-and-inc-dst ;
: strfmt-s ( c-addr u dst -- dst )
-rot move-and-inc-dst ;

: parse-cmdspec ( dst src srcend -- dst src srcend )
>r 1+ dup >r c@ case
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
