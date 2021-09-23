@ECHO off
cls

ECHO Connecting Gigabit1
netsh interface ip set address "Gigabit1" static 192.168.0.1 255.255.255.0 none 1

ECHO Connecting Gigabit2
netsh interface ip set address "Gigabit2" static 192.168.200.1 255.255.255.0 192.168.200.2 1

ECHO Connecting Local Area Connection
netsh interface ip set address "Local Area Connection" dhcp

ECHO Connecting Gigabit4
netsh interface ip set address "Gigabit4" static 192.168.20.1 255.255.255.0 none 1

ECHO Connecting Local Area Connection X
netsh interface ip set address "Local Area Connection X" static 192.168.30.1 255.255.255.0 none 1

ECHO Connecting Local Area Connection Y
netsh interface ip set address "Local Area Connection Y" static 192.168.40.1 255.255.255.0 none 1

set choice=
set /p choice=Hit Enter to exit.

goto end

:bye
ECHO BYE
goto end

:end