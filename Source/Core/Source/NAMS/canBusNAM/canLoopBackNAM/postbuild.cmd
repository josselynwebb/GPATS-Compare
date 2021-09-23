SETLOCAL EnableDelayedExpansion 
@ECHO OFF
REM ECHO Working Directory ---  %CD%
REM ECHO Solution Directory --- %1
SET SOLUTION=%1
SET SOURCE=%2

IF /I ""!SOLUTION!""==""c:\software development\ats\source\software build\"" (
	ECHO Copying file !SOURCE!
	copy !SOURCE! "..\..\..\..\..\..\..\..\..\Target\ROOTDrive\usr\tyx\bin\" /Y
) ELSE (ECHO Not Copying file)
ECHO ERRORLEVEL = %ERRORLEVEL%