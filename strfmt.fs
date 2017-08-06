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

\ Prints into buffer c-addr2 using the format string at c-addr1 u.
\ caddr2 u3 is the resulting string.
: strfmt ( c-addr1 u1 c-addr2 -- caddr2 u3 )
dup >r -rot over + swap ?do
i c@ '%' = if i 1+ c@ case
'%' of '%' over c! 1+ 2 endof
'c' of tuck c! 1+ 2 endof
'n' of strfmt-n 2 endof
'u' of strfmt-u 2 endof
's' of strfmt-s 2 endof
'd' of i 2 + c@ case
    'n' of strfmt-dn endof
    'u' of strfmt-du endof
endcase 3 endof
endcase else i c@ over c! 1+ 1 then
+loop r> tuck - ;
