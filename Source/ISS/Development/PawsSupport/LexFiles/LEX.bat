echo on
copy config+lexcon.h+verbs+nouns+modifier+dims+ports+modtypes+modsdims+nounsmod+generate LexDB > nul:
%TYXROOT%\usr\tyx\prg\PLI < LexDB %TYXROOT%\usr\tyx\tab\pale.pro -c LexDB > Build.log

copy LexDB ..\..\..\..\..\Target\ROOTDrive\usr\tyx\sub\IEEE716.89\GPATSCIC\Subset\LexDB

copy LexDB.lex ..\..\..\..\..\Target\ROOTDrive\usr\tyx\sub\IEEE716.89\GPATSCIC\Subset\LexDB.lex 

pause
