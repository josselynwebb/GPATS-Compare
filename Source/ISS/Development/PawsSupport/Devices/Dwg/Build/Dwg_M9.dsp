# Microsoft Developer Studio Project File - Name="DWG" - Package Owner=<4>
# Microsoft Developer Studio Generated Build File, Format Version 6.00
# ** DO NOT EDIT **

# TARGTYPE "Win32 (x86) Dynamic-Link Library" 0x0102

CFG=DWG - Win32 Debug
!MESSAGE This is not a valid makefile. To build this project using NMAKE,
!MESSAGE use the Export Makefile command and run
!MESSAGE 
!MESSAGE NMAKE /f "Dwg_M9.mak".
!MESSAGE 
!MESSAGE You can specify a configuration when running NMAKE
!MESSAGE by defining the macro CFG on the command line. For example:
!MESSAGE 
!MESSAGE NMAKE /f "Dwg_M9.mak" CFG="DWG - Win32 Debug"
!MESSAGE 
!MESSAGE Possible choices for configuration are:
!MESSAGE 
!MESSAGE "DWG - Win32 Release" (based on "Win32 (x86) Dynamic-Link Library")
!MESSAGE "DWG - Win32 Debug" (based on "Win32 (x86) Dynamic-Link Library")
!MESSAGE 

# Begin Project
# PROP AllowPerConfigDependencies 0
# PROP Scc_ProjName ""$/TETS/PawsSupport/Devices/Dwg/Build", LHAAAAAA"
# PROP Scc_LocalPath "."
CPP=cl.exe
MTL=midl.exe
RSC=rc.exe

!IF  "$(CFG)" == "DWG - Win32 Release"

# PROP BASE Use_MFC 0
# PROP BASE Use_Debug_Libraries 0
# PROP BASE Output_Dir "Release"
# PROP BASE Intermediate_Dir "Release"
# PROP BASE Target_Dir ""
# PROP Use_MFC 0
# PROP Use_Debug_Libraries 0
# PROP Output_Dir "V6Release"
# PROP Intermediate_Dir "V6Release"
# PROP Ignore_Export_Lib 0
# PROP Target_Dir ""
# ADD BASE CPP /nologo /MT /W3 /GX /O2 /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /D "_MBCS" /D "_USRDLL" /D "DWG_EXPORTS" /YX /FD /c
# ADD CPP /nologo /W3 /GX /I "..\Vendor\Include" /I "..\..\Common\include" /I "\usr\tyx\include" /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /D "_MBCS" /D "_USRDLL" /FD /c
# ADD BASE MTL /nologo /D "NDEBUG" /mktyplib203 /win32
# ADD MTL /nologo /D "NDEBUG" /mktyplib203 /win32
# ADD BASE RSC /l 0x409 /d "NDEBUG"
# ADD RSC /l 0x409 /d "NDEBUG"
BSC32=bscmake.exe
# ADD BASE BSC32 /nologo
# ADD BSC32 /nologo
LINK32=link.exe
# ADD BASE LINK32 kernel32.lib user32.lib gdi32.lib winspool.lib comdlg32.lib advapi32.lib shell32.lib ole32.lib oleaut32.lib uuid.lib odbc32.lib odbccp32.lib /nologo /dll /machine:I386
# ADD LINK32 cem.lib cemsupport.lib swxsrvr.lib terM9.lib user32.lib winmm.lib wsock32.lib /nologo /dll /incremental:yes /machine:I386 /nodefaultlib:"libc.lib" /def:"\usr\tyx\include\WCEM.DEF" /out:"WcemDwg.dll" /libpath:"..\Vendor\Lib" /libpath:"..\..\Common\lib" /libpath:"\usr\tyx\lib"
# SUBTRACT LINK32 /pdb:none
# Begin Special Build Tool
SOURCE="$(InputPath)"
PostBuild_Cmds=copy WcemDwg.dll \usr\Tyx\Sub\IEEE716.89\TETS_II\Station
# End Special Build Tool

!ELSEIF  "$(CFG)" == "DWG - Win32 Debug"

# PROP BASE Use_MFC 0
# PROP BASE Use_Debug_Libraries 1
# PROP BASE Output_Dir "DWG___Win32_Debug"
# PROP BASE Intermediate_Dir "DWG___Win32_Debug"
# PROP BASE Target_Dir ""
# PROP Use_MFC 0
# PROP Use_Debug_Libraries 1
# PROP Output_Dir "V6Debug"
# PROP Intermediate_Dir "V6Debug"
# PROP Ignore_Export_Lib 0
# PROP Target_Dir ""
# ADD BASE CPP /nologo /MTd /W3 /Gm /GX /ZI /Od /D "WIN32" /D "_DEBUG" /D "_WINDOWS" /D "_MBCS" /D "_USRDLL" /D "DWG_EXPORTS" /YX /FD /GZ /c
# ADD CPP /nologo /W3 /Gm /GX /ZI /Od /I "..\Vendor\Include" /I "..\..\Common\include" /I "\usr\tyx\include" /D "WIN32" /D "_DEBUG" /D "_WINDOWS" /D "_MBCS" /D "_USRDLL" /FD /GZ /c
# ADD BASE MTL /nologo /D "_DEBUG" /mktyplib203 /win32
# ADD MTL /nologo /D "_DEBUG" /mktyplib203 /win32
# ADD BASE RSC /l 0x409 /d "_DEBUG"
# ADD RSC /l 0x409 /d "_DEBUG"
BSC32=bscmake.exe
# ADD BASE BSC32 /nologo
# ADD BSC32 /nologo
LINK32=link.exe
# ADD BASE LINK32 kernel32.lib user32.lib gdi32.lib winspool.lib comdlg32.lib advapi32.lib shell32.lib ole32.lib oleaut32.lib uuid.lib odbc32.lib odbccp32.lib /nologo /dll /debug /machine:I386 /pdbtype:sept
# ADD LINK32 cem.lib cemsupport.lib swxsrvr.lib terM9.lib user32.lib winmm.lib wsock32.lib /nologo /dll /debug /machine:I386 /nodefaultlib:"libc.lib" /def:"c:\usr\tyx\include\WCEM.DEF" /out:"WcemDwg.dll" /pdbtype:sept /libpath:"..\Vendor\Lib" /libpath:"..\..\Common\lib" /libpath:"\usr\tyx\lib"
# SUBTRACT LINK32 /pdb:none

!ENDIF 

# Begin Target

# Name "DWG - Win32 Release"
# Name "DWG - Win32 Debug"
# Begin Group "Source Files"

# PROP Default_Filter "cpp;c;cxx;rc;def;r;odl;idl;hpj;bat"
# Begin Source File

SOURCE=..\Cem\Dwg.cpp
# End Source File
# Begin Source File

SOURCE=..\Cem\Dwg.rc
# End Source File
# Begin Source File

SOURCE=..\Cem\Dwg_T.cpp
# End Source File
# Begin Source File

SOURCE=..\Cem\error.cpp
# End Source File
# Begin Source File

SOURCE=..\Cem\Wrapper.cpp
# End Source File
# End Group
# Begin Group "Header Files"

# PROP Default_Filter "h;hpp;hxx;hm;inl"
# Begin Source File

SOURCE=..\Cem\Dwg_T.h
# End Source File
# Begin Source File

SOURCE=..\Cem\functionCodes_TETS.h
# End Source File
# Begin Source File

SOURCE=..\Cem\Key.h
# End Source File
# Begin Source File

SOURCE=..\Cem\resource.h
# End Source File
# End Group
# Begin Group "Resource Files"

# PROP Default_Filter "ico;cur;bmp;dlg;rc2;rct;bin;rgs;gif;jpg;jpeg;jpe"
# End Group
# End Target
# End Project
