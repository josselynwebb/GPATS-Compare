# Microsoft Developer Studio Project File - Name="SwxSrvrDelv_TETS" - Package Owner=<4>
# Microsoft Developer Studio Generated Build File, Format Version 6.00
# ** DO NOT EDIT **

# TARGTYPE "Win32 (x86) Dynamic-Link Library" 0x0102

CFG=SwxSrvrDelv_TETS - Win32 Debug
!MESSAGE This is not a valid makefile. To build this project using NMAKE,
!MESSAGE use the Export Makefile command and run
!MESSAGE 
!MESSAGE NMAKE /f "SwxSrvrDeliv6.mak".
!MESSAGE 
!MESSAGE You can specify a configuration when running NMAKE
!MESSAGE by defining the macro CFG on the command line. For example:
!MESSAGE 
!MESSAGE NMAKE /f "SwxSrvrDeliv6.mak" CFG="SwxSrvrDelv_TETS - Win32 Debug"
!MESSAGE 
!MESSAGE Possible choices for configuration are:
!MESSAGE 
!MESSAGE "SwxSrvrDelv_TETS - Win32 Release" (based on "Win32 (x86) Dynamic-Link Library")
!MESSAGE "SwxSrvrDelv_TETS - Win32 Debug" (based on "Win32 (x86) Dynamic-Link Library")
!MESSAGE 

# Begin Project
# PROP AllowPerConfigDependencies 0
# PROP Scc_ProjName ""$/TETS/Delivery/Development/BuildAllSoftware", DPEAAAAA"
# PROP Scc_LocalPath "..\..\..\..\buildallsoftware"
CPP=cl.exe
MTL=midl.exe
RSC=rc.exe

!IF  "$(CFG)" == "SwxSrvrDelv_TETS - Win32 Release"

# PROP BASE Use_MFC 0
# PROP BASE Use_Debug_Libraries 0
# PROP BASE Output_Dir "Release"
# PROP BASE Intermediate_Dir "Release"
# PROP BASE Target_Dir ""
# PROP Use_MFC 0
# PROP Use_Debug_Libraries 0
# PROP Output_Dir "SwxSrvr6dRelease"
# PROP Intermediate_Dir "SwxSrvr6dRelease"
# PROP Ignore_Export_Lib 0
# PROP Target_Dir ""
# ADD BASE CPP /nologo /MT /W3 /GX /O2 /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /D "_MBCS" /D "_USRDLL" /D "SwxSrvrDelv_TETS_EXPORTS" /Yu"stdafx.h" /FD /c
# ADD CPP /nologo /MT /W3 /GX /O2 /I "$\usr\tyx\include" /I "..\Vendor\Include" /I "..\..\Common\Include" /I "..\..\..\..\TETS\Include" /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /D "_MBCS" /D "_USRDLL" /D "SWXSRVR_EXPORTS" /FD /c
# SUBTRACT CPP /YX /Yc /Yu
# ADD BASE MTL /nologo /D "NDEBUG" /mktyplib203 /win32
# ADD MTL /nologo /D "NDEBUG" /mktyplib203 /win32
# ADD BASE RSC /l 0x409 /d "NDEBUG"
# ADD RSC /l 0x409 /d "NDEBUG"
BSC32=bscmake.exe
# ADD BASE BSC32 /nologo
# ADD BSC32 /nologo
LINK32=link.exe
# ADD BASE LINK32 kernel32.lib user32.lib gdi32.lib winspool.lib comdlg32.lib advapi32.lib shell32.lib ole32.lib oleaut32.lib uuid.lib odbc32.lib odbccp32.lib /nologo /dll /machine:I386
# ADD LINK32 cem.lib AtXmlApi.lib kernel32.lib user32.lib gdi32.lib winspool.lib comdlg32.lib advapi32.lib shell32.lib ole32.lib oleaut32.lib uuid.lib odbc32.lib odbccp32.lib /nologo /dll /machine:I386 /out:"SwxSrvr_TETS.dll" /libpath:"\usr\tyx\lib" /libpath:"..\..\Common\Lib" /libpath:"..\..\..\..\TETS\Lib"
# Begin Special Build Tool
SOURCE="$(InputPath)"
PostBuild_Cmds=copy SSwxSrvr6dRelease\SwxSrvr_TETS.lib ..\..\Common\Lib\SwxSrvr.lib	copy SwxSrvr_TETS.dll SwxSrvr_TETS_Cicl.dll
# End Special Build Tool

!ELSEIF  "$(CFG)" == "SwxSrvrDelv_TETS - Win32 Debug"

# PROP BASE Use_MFC 0
# PROP BASE Use_Debug_Libraries 1
# PROP BASE Output_Dir "Debug"
# PROP BASE Intermediate_Dir "Debug"
# PROP BASE Target_Dir ""
# PROP Use_MFC 0
# PROP Use_Debug_Libraries 1
# PROP Output_Dir "SwxSrvr6Debug"
# PROP Intermediate_Dir "SwxSrvr6Debug"
# PROP Ignore_Export_Lib 0
# PROP Target_Dir ""
# ADD BASE CPP /nologo /MTd /W3 /Gm /GX /ZI /Od /D "WIN32" /D "_DEBUG" /D "_WINDOWS" /D "_MBCS" /D "_USRDLL" /D "SwxSrvrDelv_TETS_EXPORTS" /Yu"stdafx.h" /FD /GZ /c
# ADD CPP /nologo /MTd /W3 /Gm /GX /ZI /Od /I "\usr\tyx\include" /I "..\Vendor\Include" /I "..\..\Common\Include" /I "..\..\..\..\TETS\Include" /D "WIN32" /D "_DEBUG" /D "_WINDOWS" /D "_MBCS" /D "_USRDLL" /D "SWXSRVR_EXPORTS" /FD /GZ /c
# SUBTRACT CPP /YX /Yc /Yu
# ADD BASE MTL /nologo /D "_DEBUG" /mktyplib203 /win32
# ADD MTL /nologo /D "_DEBUG" /mktyplib203 /win32
# ADD BASE RSC /l 0x409 /d "_DEBUG"
# ADD RSC /l 0x409 /d "_DEBUG"
BSC32=bscmake.exe
# ADD BASE BSC32 /nologo
# ADD BSC32 /nologo
LINK32=link.exe
# ADD BASE LINK32 kernel32.lib user32.lib gdi32.lib winspool.lib comdlg32.lib advapi32.lib shell32.lib ole32.lib oleaut32.lib uuid.lib odbc32.lib odbccp32.lib /nologo /dll /debug /machine:I386 /pdbtype:sept
# ADD LINK32 cem.lib AtXmlApi.lib kernel32.lib user32.lib gdi32.lib winspool.lib comdlg32.lib advapi32.lib shell32.lib ole32.lib oleaut32.lib uuid.lib odbc32.lib odbccp32.lib /nologo /dll /debug /machine:I386 /out:"SwxSrvr_TETS.dll" /pdbtype:sept /libpath:"\usr\tyx\lib" /libpath:"..\..\Common\Lib" /libpath:"..\..\..\..\TETS\Lib"
# Begin Special Build Tool
SOURCE="$(InputPath)"
PostBuild_Cmds=copy SwxSrvr6Debug\SwxSrvr_TETS.lib ..\..\Common\Lib\SwxSrvr.lib	copy SwxSrvr_TETS.dll SwxSrvr_TETS_Cicl.dll
# End Special Build Tool

!ENDIF 

# Begin Target

# Name "SwxSrvrDelv_TETS - Win32 Release"
# Name "SwxSrvrDelv_TETS - Win32 Debug"
# Begin Group "Source Files"

# PROP Default_Filter "cpp;c;cxx;rc;def;r;odl;idl;hpj;bat"
# Begin Source File

SOURCE=..\SwxSrvr\StdAfx.cpp
# End Source File
# Begin Source File

SOURCE=..\SwxSrvr\SwxSrvr.cpp
# End Source File
# Begin Source File

SOURCE=..\SwxSrvr\TETS_Switch.cpp
# End Source File
# Begin Source File

SOURCE=..\SwxSrvr\TETS_UniqFnc.cpp
# End Source File
# End Group
# Begin Group "Header Files"

# PROP Default_Filter "h;hpp;hxx;hm;inl"
# Begin Source File

SOURCE=..\..\..\..\TETS\Include\AtXmlInterfaceApiC.h
# End Source File
# Begin Source File

SOURCE=..\SwxSrvr\StdAfx.h
# End Source File
# Begin Source File

SOURCE=..\SwxSrvr\Swx_TetsMap.h
# End Source File
# Begin Source File

SOURCE=..\..\Common\Include\SwxSrvr.h
# End Source File
# Begin Source File

SOURCE=..\SwxSrvr\SwxSrvrGlbl.h
# End Source File
# End Group
# Begin Group "Resource Files"

# PROP Default_Filter "ico;cur;bmp;dlg;rc2;rct;bin;rgs;gif;jpg;jpeg;jpe"
# Begin Source File

SOURCE=..\SwxSrvr\resource.h
# End Source File
# Begin Source File

SOURCE=..\SwxSrvr\SwxSrvr.rc
# End Source File
# End Group
# Begin Source File

SOURCE=.\ReadMe.txt
# End Source File
# End Target
# End Project
