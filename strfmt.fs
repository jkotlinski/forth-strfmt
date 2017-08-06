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

variable src
0 value srcend

\ Prints into buffer c-addr2 using the format string at c-addr1 u.
\ caddr2 u3 is the resulting string.
: strfmt ( c-addr1 u1 c-addr2 -- caddr2 u3 )
dup >r -rot over + to srcend src !
begin src @ srcend < while
src @ c@ '%' = if 1 src +! src @ c@ case
'%' of '%' over c! 1+ endof
'c' of tuck c! 1+ endof
'n' of strfmt-n endof
'u' of strfmt-u endof
's' of strfmt-s endof
'd' of 1 src +! src @ c@ case
    'n' of strfmt-dn endof
    'u' of strfmt-du endof
endcase endof
endcase else src @ c@ over c! 1+ then
1 src +! repeat
r> tuck - ;
