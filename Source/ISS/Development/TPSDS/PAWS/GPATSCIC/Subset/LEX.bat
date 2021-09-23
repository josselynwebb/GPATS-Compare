echo on
copy config+lexcon.h+verbs+nouns+modifier+dims+ports+modtypes+modsdims+nounsmod+generate LexDB.src > nul:
%TYXROOT%\usr\tyx\prg\PLI < LexDB.src %TYXROOT%\usr\tyx\tab\pale.pro -c LexDB > Build.log
del LexDB.src
copy LexDB.lex %TYXROOT%\usr\tyx\sub\IEEE716.89\VIPERT\subset\LexDB.lex 
pause
