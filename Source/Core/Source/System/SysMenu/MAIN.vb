'Option Strict Off
'Option Explicit On

Imports System
Imports System.Windows.Forms
Imports System.Windows.Forms.Screen
Imports System.Text
Imports System.Drawing
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Imports Microsoft.Win32
Imports System.IO
Imports System.IO.Compression
Imports System.Collections.Generic


Public Module SysPanel


    '=========================================================
    '**************************************************************
    '**************************************************************
    '** ManTech Test Systems Software Module                     **
    '**                                                          **
    '** Nomenclature   : VIPERT SYSTEM MENU                        **
    '** Version        : 1.14                                    **
    '** Written By     : David W. Hartley                        **
    '** Purpose        : This module controls the functions of   **
    '**                  the VIPERT System Menu                    **
    '** Program Begins Executing Instructions In Sub:MAIN        **
    '**************************************************************
    '** Revision History                                         **
    '** V1.8 ECP006 - GJohnson 13OCT99                           **
    '**         Modified the way the IADS reader launches the TPS**
    '**         IADS document and the VIPERT IADS document.  Added **
    '**         -userid32767 to the command line argument used in**
    '**         the Shell command.                               **
    '** V1.9 ECO-508 - JHill 12/28/00                            **
    '**   1. Converted from VB 4.0 to VB 6.0                     **
    '**   2. DR #108: Changed UDD Restore to restore only        **
    '**     selected keys in VIPERT.INI instead of the entire file.**
    '**     Modifications made to frmUdd (Udd.frm).              **
    '**     Modified function RestoreUddFiles() to call new      **
    '**     function RestoreHardwareUniqueKeys when VIPERT.INI is  **
    '**     found. Added sub RestoreSection and function         **
    '**     StrToDynList to support new functionality.           **
    '**   3. DR #109, ECP-026: Added user access to Info Cards.  **
    '**     Added two buttons to Maintenance tab for Inventory   **
    '**     list and Setup procedure. Added HTM files and        **
    '**     SetupProc_files sub-folder.                          **
    '**   4. DR #110: Added double-click feature to TPS tree     **
    '**     menu to launch the selected .obj with the RTS.       **
    '**   5. Replaced Sub Delay a corrected version that does    **
    '**     not have the cross-over midnight bug.                **
    '--------------- Revision History for V1.10 -------------------
    '** V1.10 ECO-3047-526                                       **
    '**   1. DR 167, JHill 04/27/01: Changed UDD save/restore to **
    '**      handle folder. Rule is if last char is '\', copy    **
    '**      all files within that folder.                       **
    '**   2. ECP-VIPERT-23, FHDB - DJoiner 06/13/01                **
    '**    +Rearranged Buttons on the Maintenance Tab to         **
    '**     display in a more logical order.                     **
    '**    +Added another Button to the Maintenance Tab on the   **
    '**     frmSysPanel to launch the FHDB Processor. Added      **
    '**     Index 14 and related values for the FHDB.            **
    '**    +Added Module FHDB_Services to support FHDB           **
    '**     functionality.                                       **
    '**    +Modified frmUdd to include capturing the FHDB        **
    '**     Database as part of the UDD process as well as       **
    '**     Restoring the FHDB Database and Relevant ini         **
    '**     keys.                                                **
    '**    +The UDD will check the size of the FHDB Database     **
    '**     File and the Free Space on the USB Disk. If the   **
    '**     Database will fit, it will copy it, if not, it will  **
    '**     Zip it to a Temp File and Check it's size. If it     **
    '**     will fit, it is copied as a Self Extracting Zip      **
    '**     file and the Temp file is deleted.                   **
    '**    +If the Zipped file is too large, the Import/Export   **
    '**     Operation (depending on which is due)will            **
    '**     automatically launch and the UDD is terminated.      **
    '**    +The UDD Restore will check the file's ext to         **
    '**     determine type. If zipped, it be extracted to a      **
    '**     temp file.                                           **
    '**    +The Restored File will be compared to                **
    '**     System file to determine latest, once found,         **
    '**     that file becomes the System file and others         **
    '**     are deleted.                                         **
    '** Self Initiated                                           **
    '**    +Modified the About Form to get Version Information   **
    '**     from the Project Properties.                         **
    '** DR213 & DR222   Dave Joiner  11/28/01                    **
    '**     Modified Menu accessibility to allow any User access **
    '**     to the UDD Restore Operation.                        **
    '** DR214           Dave Joiner  11/28/01                    **
    '**     Added an additional button to allow any User access  **
    '**     to the Path Loss Compensation(PLC) application.      **
    '** Self Initiated  Dave Joiner  12/04/01                    **
    '**     Add Option Explicit to frmUdd, Declare all           **
    '**     non-declared variables.                              **
    '**************************************************************
    '** V1.11, ECO-3047-578, Software Release 8.1                **
    '**                                                          **
    '**  DR#260 Dave Joiner 03/11/02                             **
    '**     Modified Sub RecoveryWizzardStep() to check for      **
    '**     either FHDB.mdb or FHDB_Database.exe on the UDD      **
    '**     Recover Disk in StepNumber 4. If either file exists, **
    '**     Recover Database, else, Recovery complete.           **
    '**     Reset global ZIP flags and delete local references   **
    '**     to "BINARY_FILE" this value is set Globally.         **
    '**  Self Initiated  Dave Joiner  03/27/02                   **
    '**     Modified ribProgram_Click event in frmSysPanl to     **
    '**     minimize the System Menu when executing the UDD      **
    '**     wizard or UDD Restore.                               **
    '**  Self Initiated  Dave Joiner  03/27/02                   **
    '**     Modified the UDD Restore operation to stop when the  **
    '**     Database Compare Information is displayed until the  **
    '**     user selects [Next]. This allows time to read info.  **
    '**************************************************************
    '**                                                          **
    '** ------ VIPERT Interactive Program (TIP) Development -------**
    '**                                                          **
    '** V1.12, ECO-3047-621, Software Release 9.0                **
    '** Nikolai Sazonov                                          **
    '**                                                          **
    '** Added an extra button to the 'Testing' tab of form       **
    '** 'frmSysPanl'. The button activates TIP Studio.           **
    '**                                                          **
    '** Added constant 'TIPS' and increased constant             **
    '** 'NUMBER_OF_PROGRAMS' to 16.                              **
    '**                                                          **
    '** Modified Sub ribProgram_Click to include activation of   **
    '** TIP Studio.                                              **
    '**                                                          **
    '** Modified Sub ProgramComment to include comment on        **
    '** TIP Studio                                               **
    '**                                                          **
    '** DR 299: When searching for a test program on             **
    '** an APS CD-ROM, the application will now also display     **
    '** and allow execution of .EXE files that start with 'TPS'  **
    '** Added Functions bTPSExecFile and bTPSExecPath            **
    '** Modified function GetTPSFiles                            **
    '**                                                          **
    '** Self Initiated: UDD Wizard will not display when the     **
    '** database on UDD and on System was last modified.         **
    '** Constants UDD_LAST_MODIFIED and SYSTEM_LAST_MODIFIED     **
    '** were taken out.                                          **
    '**************************************************************
    '** V1.13, ECO-3047-638   - Release 9.1 -                    **
    '** DR#308 - Dave Joiner  10/30/03                           **
    '**    Modified Function bTPSExecFile() to first check       **
    '**    the length of sFileName before checking for a         **
    '**    ".EXE" suffix, thus prevent a Run Time Error.         **
    '**************************************************************
    '** V1.14, ECO-3047-661   - Release 10.0 -                   **
    '** Universal Dock Modifications - Jeff Hill  3/30/04        **
    '**    General modifications to allow operation on systems   **
    '**    where the USB drive is "B:".                       **
    '**    The code is designed such that if E: exists, use it,  **
    '**    else if B: exists, use it, else issue warning message **
    '**    and set variables to use E:.                          **
    '**************************************************************

    '-----------------API / DLL Declarations------------------------------
    Declare Function GetPrivateProfileString Lib "kernel32" Alias "GetPrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpDefault As String, ByVal lpReturnedString As StringBuilder, ByVal nSize As Integer, ByVal lpFileName As String) As Integer
    Declare Function WritePrivateProfileString Lib "kernel32" Alias "WritePrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpString As String, ByVal lpFileName As String) As Integer
    Declare Function SetWindowPos Lib "user32" (ByVal hWnd As Integer, ByVal hWndInsertAfter As Integer, ByVal x As Integer, ByVal y As Integer, ByVal cx As Integer, ByVal cy As Integer, ByVal wFlags As Integer) As Integer
    Declare Function GetLogicalDriveStrings Lib "kernel32" Alias "GetLogicalDriveStringsA" (ByVal nBufferLength As Integer, ByVal lpBuffer As StringBuilder) As Integer
    Declare Function GetDriveType Lib "kernel32" Alias "GetDriveTypeA" (ByVal nDrive As String) As Integer
    Declare Function GetVolumeInformation Lib "kernel32" Alias "GetVolumeInformationA" (ByVal lpRootPathName As String, ByVal lpVolumeNameBuffer As StringBuilder, ByVal nVolumeNameSize As Integer, ByRef lpVolumeSerialNumber As Integer, ByRef lpMaximumComponentLength As Integer, ByRef lpFileSystemFlags As Integer, ByVal lpFileSystemNameBuffer As String, ByVal nFileSystemNameSize As Integer) As Integer
    Public Const DRIVE_CDROM As Short = 5
    Public Const DRIVE_FIXED As Short = 3
    Public Const DRIVE_RAMDISK As Short = 6
    Public Const DRIVE_REMOTE As Short = 4
    Public Const DRIVE_REMOVABLE As Short = 2
    '-----------------Global Constants------------------------------------
    Public Const DEBUG_FILE As String = "c:\\aps\\data\\DEBUGIT_SYSMENU"
    Public Const DEBUG_RECORD As String = "c:\\aps\\data\\SysMenu_Debug.txt"
    Public Const SAIS_TOOLBAR As Short = 1 'Stand Alone Instrument Software Toolbar
    Public Const PAWS_RTS As Short = 2 'Professional ATLAS Workstation Run-Time System
    Public Const IADS_READER As Short = 3 'Inter-Active Authoring And Display System Reader
    Public Const SYSTEM_SURVEY As Short = 4 'System Survey
    Public Const SYSTEM_SELF_TEST As Short = 5 'System Self Test
    Public Const SYSTEM_LOG As Short = 6 'System Log Viewer
    Public Const SYSTEM_CALIBRATION As Short = 7 'System Calibration Software
    Public Const TPS_MENU As Short = 8 'TPS Menu Unique (Launch the selected Test Program)
    Public Const TPS_TM As Short = 9 'Launch TPS IADS Technical Manual for the APS
    '---Added/Modified for Version 1.7 DWH---
    Public Const UPDATE_UDD As Short = 10 'Upadte the files from the Instrument Controller to the VIPERT Unique Data Disk
    Public Const RESTORE_UDD As Short = 11 'Restore the files from the VIPERT Unique Data Disk to the Instrument Controller
    Public Const INVENTORY_LIST As Short = 12 'JHill V1.9 DR #109, ECP-026
    Public Const SETUP_PROC As Short = 13 'JHill V1.9 DR #109, ECP-026
    Public Const FHDB_PROCESSOR As Short = 14 'DJoiner V1.10 ECP-VIPERT-23, FHDB
    Public Const STOW As Short = 15 'DJoiner V1.10 DR214
    Public Const TIPS As Short = 16 'NS
    Public Const NUMBER_OF_PROGRAMS As Short = 16 'TOTAL Number of Programs in VIPERT Menu 'NS
    '-----------------Input Text Box / Spin Button Instruction -----------
    Public Const MAX_SETTINGS As Short = 1 'Array Limits For Input Text Box
    Public SetCur(MAX_SETTINGS) As String ' "Current Settings" Array
    Public SetDef(MAX_SETTINGS) As String ' "Default Settings" Array
    Public SetMin(MAX_SETTINGS) As String ' "Maximum Settings" Array
    Public SetMax(MAX_SETTINGS) As String ' "Minimum Settings" Array
    Public SetUOM(MAX_SETTINGS) As String ' "Unit Of Measure" Array
    Public SetInc(MAX_SETTINGS) As String ' "Increments" Array
    Public SetRes(MAX_SETTINGS) As String ' "Increments" Array
    Public SetCmd(MAX_SETTINGS) As String ' "SCPI command" Array
    Public SetRngMsg(MAX_SETTINGS) As String ' "Range Message" for Status Bar Array
    Public SetMinInc(MAX_SETTINGS) As String ' "Increments" Array
    Public Const VIPERT_SN As Short = 1 'VIPERT Serial Number Input Text Box Index
    Public bDebug As Boolean = IsDebug()
    Public UpdateFlag As Short 'TRUE=Update UDD, FALSE=Restore UDD
    '-----------------Windows API Declarations----------------------------
    Public Declare Function SHFormatDrive Lib "shell32" (ByVal hWnd As Integer, ByVal Drive As Integer, ByVal fmtID As Integer, ByVal options As Integer) As Integer
    Declare Function SetVolumeLabel Lib "kernel32" Alias "SetVolumeLabelA" (ByVal lpRootPathName As String, ByVal lpVolumeName As String) As Integer
    '---End of Modification Version 1.7 DWH---
    '-----------------Global Variables------------------------------------
    Public FileNameKey(NUMBER_OF_PROGRAMS) As String 'Key Names in VIPERT.INI file
    Public FileNameData(NUMBER_OF_PROGRAMS) As String 'File Name+Path
    
    Public LoginAccess(NUMBER_OF_PROGRAMS) As Boolean 'Access Rights To Program
    Public UserName As String = System.Environment.UserName 'Login Name
    Public IatmFile As String 'IATM Executable
    Public IatmKey As String 'IATM INI Key Name
    Public Q As String 'ASCII Quote
    Public CdDrivesOnSystem As String 'Detected CD Media Logical Drives
    Public SelectedTpsProgram As String 'TPS Program Selected in the Tree View Control
    Public SelectedSystem As String 'System Selected in the Tree View Control
    Public PartNumber As String 'Characteristic of Last Inserted CD-ROM
    Public TpsVersion As String 'Characteristic of Last Inserted CD-ROM
    Const SECS_IN_DAY As Integer = 86400
    Public sFDD As String 'Letter of the system USB Disk Drive (E)
    Public sFileSelfX As String 'Self Extracting Zip File Name
    Public sFDD_DB As String 'File name and location on USB Disk
    Public nDrive As Integer 'Zero-based drive code for the SHFormatDrive() function
    
    Public usb_flag As Boolean 'flag for when usb is set
    Public uddHDflag As Boolean 'flag for when udd will be on HD
    Public iniFilePath As String
    Public tpsDiscInserted As Boolean = False
    Public tpsHDProgsExist As Boolean = False
    Public lastMediaPartNumber As String = ""
    Public lastMediaVolumeLabel As String = ""
    Public lastNodeSelected As String = ""
    Public currentNodeSelected As String = ""
    Public RefreshTreeView As Boolean = False
    Public CurrentSystemDir As String = ""
    Public LastSystemDir As String = ""

    'APS on HD
    Dim SystemDir As String
    Public PathVIPERTIni As String
    Public TPSLocation As String = "\"
    Public TPSDrive As String = "F:\" 'Initialize to ROM Drive

    Public VEO2PowerOn As Integer
    Public SAIFinstalled As Integer
    Public FPUHandle As Integer
    Public PsResourceName(10) As String
    Public ProgramPath As String
    Public GoBack As Integer
    Public ExternalPower As Integer

    'TPS Info
    Public iniFileList As List(Of String) = New List(Of String)

    'Declarations for displaying a browse window

    Structure BrowseInfo
        Dim hWndOwner As Integer
        Dim pIDLRoot As Integer
        Dim pszDisplayName As Integer
        Dim lpszTitle As String
        Dim ulFlags As Integer
        Dim lpfnCallback As Integer
        Dim lParam As Integer
        Dim iImage As Integer
    End Structure

    'Browsing type.
    Enum BrowseType
        BrowseForFolders = &H1
        BrowseForComputers = &H1000
        BrowseForPrinters = &H2000
        BrowseForEverything = &H4000
    End Enum

    'Folder Type
    Enum FolderType
        CSIDL_BITBUCKET = 10
        CSIDL_CONTROLS = 3
        CSIDL_DESKTOP = 0
        CSIDL_DRIVES = 17
        CSIDL_FONTS = 20
        CSIDL_NETHOOD = 18
        CSIDL_NETWORK = 19
        CSIDL_PERSONAL = 5
        CSIDL_PRINTERS = 4
        CSIDL_PROGRAMS = 2
        CSIDL_RECENT = 8
        CSIDL_SENDTO = 9
        CSIDL_STARTMENU = 11
    End Enum

    Private Const MAX_PATH As Short = 260
    Private Declare Sub CoTaskMemFree Lib "ole32.dll" (ByVal hMem As Integer)
    Private Declare Function lstrcat Lib "kernel32.dll" Alias "lstrcatA" (ByVal lpString1 As String, ByVal lpString2 As String) As Integer
    Private Declare Function SHBrowseForFolder Lib "shell32.dll" (ByRef lpbi As BrowseInfo) As Integer
    Private Declare Function SHGetPathFromIDList Lib "shell32.dll" (ByVal pidList As Integer, ByVal lpBuffer As String) As Integer
    Private Declare Function SHGetSpecialFolderLocation Lib "shell32.dll" (ByVal hWndOwner As Integer, ByVal nFolder As Integer, ByRef ListId As Integer) As Integer




    
    
    Sub CenterChildForm(ByVal ParentForm As frmSysPanl, ByVal Childform As frmAbout)
        '************************************************************
        '* ManTech Test Systems Software Module                     *
        '************************************************************
        '* Nomenclature   : VIPERT SYSTEM LOG MENU                    *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     Centers the passed form with respect to a parent form*
        '*    PARAMETERS:                                           *
        '*     Childform=form to center, ParentForm=reference form  *
        '*    EXAMPLE:                                              *
        '*      CenterChildForm frmMain1, frmMessage                *
        '************************************************************

        'Align Top
        Childform.Top = ((ParentForm.Height / 2) + ParentForm.Top) - Childform.Height / 2
        'Align Left
        Childform.Left = ((ParentForm.Width / 2) + ParentForm.Left) - Childform.Width / 2

    End Sub


    '#Const defUse_CheckCtestResults = True

    Sub CheckCtestResults()
        '************************************************************
        '* ManTech Test Systems Software Module                     *
        '************************************************************
        '* Nomenclature   : VIPERT SYSTEM LOG MENU                    *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     Limits Access rights according to CTEST status       *
        '*    EXAMPLE:                                              *
        '*      CheckCtestResults                                   *
        '************************************************************
        Dim lpApplicationName As String '[HEADING] in INI File
        
        Dim lpReturnedString As New StringBuilder(Space(255), 255) 'Return Buffer
        Dim nSize As Integer 'Return Buffer Size
        Dim lpFileName As String 'INI File Key Name "Key=?"
        Dim ReturnValue As Integer 'Return Value Buffer
        Dim FileNameInfo As String 'Formatted Return String
        Dim lpString As String 'String to write to INI File
        Dim lpKeyName As String 'Key Name In INI File
        Dim lpDefault As String 'Default Value for failed INI call

        'Clear String Buffer
        lpReturnedString = New StringBuilder(Space(255))
        'Find Windows Directory
        lpFileName = Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\ATS\", "IniFilePath", Nothing)
        nSize = 255
        'Find Ctest Result
        lpApplicationName = "System Startup"
        lpKeyName = "SYSTEM_SURVEY"
        lpDefault = ""
        'Clear String Buffer
        lpReturnedString = New StringBuilder(Space(255))

        ReturnValue = GetPrivateProfileString(lpApplicationName, lpKeyName, lpDefault, lpReturnedString, nSize, lpFileName)
        FileNameInfo = Trim(lpReturnedString.ToString())
        FileNameInfo = Mid(FileNameInfo, 1, Len(FileNameInfo))
        'If File Locations Missing, then create empty keys
        If FileNameInfo = "" Then
            lpString = " "
            ReturnValue = WritePrivateProfileString(lpApplicationName, lpKeyName, lpString, lpFileName)
        End If
        'The User Can only access instrumentation through the STEST or CTEST programs
        If UCase(FileNameInfo) <> "PASS" Then 'The SYSTEM_SURVEY on startup FAILED
            LoginAccess(SAIS_TOOLBAR) = False
            LoginAccess(PAWS_RTS) = False
            LoginAccess(IADS_READER) = True
            LoginAccess(SYSTEM_SURVEY) = True
            LoginAccess(SYSTEM_SELF_TEST) = True
            LoginAccess(SYSTEM_CALIBRATION) = False
            LoginAccess(SYSTEM_LOG) = True
            LoginAccess(FHDB_PROCESSOR) = True 'DJoiner ECP-VIPERT-23, FHDB
            LoginAccess(STOW) = True 'DJoiner DR214  01/18/02
        End If

    End Sub



    Sub CheckUserName()
        '************************************************************
        '* ManTech Test Systems Software Module                     *
        '************************************************************
        '* Nomenclature   : VIPERT SYSTEM LOG MENU                    *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     Queries Windows for Login Name and Grants Access     *
        '*    EXAMPLE:                                              *
        '*      CheckUserName                                       *
        '************************************************************
        Dim ProgramFile As Short 'Index For Login Access Array Storage
        Dim lpBuffer As New StringBuilder(Space(255), 255) 'Return Buffer For User Name
        Dim nSize As Integer 'Size of Return Buffer
        Dim ReturnValue As Integer 'API Function Return Value

        'Clear Data To Default State
        For ProgramFile = 1 To NUMBER_OF_PROGRAMS
            LoginAccess(ProgramFile) = True
        Next ProgramFile

        If IsElevated(System.Environment.UserName) Then
            LoginAccess(SYSTEM_CALIBRATION) = True
        Else
            LoginAccess(SYSTEM_CALIBRATION) = False
        End If

    End Sub

    Public Function GetTPSDrive() As String
        Dim tempstr As New System.Text.StringBuilder(256)

        ProgramPath = Application.StartupPath & "\"

        'Find Windows System directory
        'SystemDir = Environment.SystemDirectory
        SystemDir = "C:\Windows\SysWOW64\"

        'Find VIPERT Ini File
        PathVIPERTIni = Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\ATS\", "IniFilePath", Nothing)

        'Get file location for the PAWS TPS programs from INI file
        GetPrivateProfileString("File Locations", "PAWS_HD", "", tempstr, 256, PathVIPERTIni)

        GetTPSDrive = tempstr.ToString.ToUpper()

    End Function


    Public Function GetTPSLocation() As String
        Dim tempstr As New System.Text.StringBuilder(256)

        ProgramPath = Application.StartupPath & "\"

        'Find Windows System directory
        'SystemDir = Environment.SystemDirectory
        SystemDir = "C:\Windows\SysWOW64\"

        'Find VIPERT Ini File
        PathVIPERTIni = Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\ATS\", "IniFilePath", Nothing)

        'Get file location for the PAWS TPS programs from INI file
        GetPrivateProfileString("File Locations", "TPS_LOCATION", "", tempstr, 256, PathVIPERTIni)

        GetTPSLocation = tempstr.ToString.ToUpper()

    End Function
    Public Function dGetTime() As Double
        'DESCRIPTION:
        '   Determines the time since 1900.
        '   Avoids errors caused by 'Timer' when crossing midnight.
        'PARAMETERS:
        '   None
        'RETURNS:
        '   A double-precision floating-point number in the format d.s where:
        '     d = the number of days since 1900 and
        '     s = the fraction of a day with 10 milli-second resolution

        'The CDbl(Now) function returns a number formatted d.s where:
        ' d = the number of days since 1900 and
        ' s = the fraction of today with 1 second resolution.
        'CLng(Now) returns only the number of days (the "d." part).
        'Timer returns the number of seconds since midnight with 10 mSec resoulution.
        'So, the following statement returns a composite double-float number representing
        ' the time since 1900 with 10 mSec resoultion. (no cross-over midnight bug)

        dGetTime = CLng(DateSerial(Year(Now), Month(Now), DateAndTime.Day(Now)).ToString) + (Microsoft.VisualBasic.Timer() / SECS_IN_DAY)

    End Function

    Public Sub Delay(ByVal dSeconds As Double)

        System.Threading.Thread.Sleep(dSeconds * 1000)

    End Sub
    
    Sub CenterForm(ByVal Form As Object)
        '************************************************************
        '* ManTech Test Systems Software Module                     *
        '************************************************************
        '* Nomenclature   : VIPERT SYSTEM LOG MENU                    *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     This Module Centers One Form With Respect To The     *
        '*     User's Screen.                                       *
        '*    EXAMPLE:                                              *
        '*     CenterForm frmMain                                   *
        '************************************************************

        'Reposition Form
        'Form.Top = PrimaryScreen.Bounds.Height/2-Form.Height/2
        'Form.Left = PrimaryScreen.Bounds.Width/2-Form.Width/2

    End Sub


    Function GetCompactDiscDrives() As String
        GetCompactDiscDrives = ""
        '************************************************************
        '* ManTech Test Systems Software Module                     *
        '************************************************************
        '* Nomenclature   : VIPERT SYSTEM MENU                        *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     This Routine will return a NULL string deliniated    *
        '*     list of logical Compact Disc Drives on the system    *
        '*    PARAMETERS:                                           *
        '*     [Returns] = CD Drives (D:\[NULL]F:\[NULL]...)        *
        '*    EXAMPLE:                                              *
        '*     CD$=GetCompactDiscDrives()                           *
        '************************************************************
        'This Routine was added due to an IPR Customer Request for a
        'TPS Menu System DataBase ID#104
        Const nBufferLength As Integer = 255 'Length of API Return Buffer
        Dim ReturnCode As Integer 'API Error Code
        Dim lpBuffer As New StringBuilder(Space(nBufferLength), nBufferLength) 'API Return String Buffer
        Dim DriveList As String 'Logical Drive Buffer Formatted for Visual Basic String Type
        
        Dim StringLocation As Integer 'Counter/Pointer for String Parsing Loop
        Dim LogicalDrive As String 'Root Drive Description to test for Type
        Dim ReturnDriveList As String = "" 'String that is returned

        'Find All Logical Drives on System
        Dim drives = DriveInfo.GetDrives()
        Dim i As Integer = 0
        While i < drives.Length
            If drives(i).DriveType = DriveType.CDRom Then
                ReturnDriveList &= drives(i).Name
            End If
            i += 1
        End While

        'Return function Value (CD Drives on System)
        GetCompactDiscDrives = ReturnDriveList

    End Function

    Function GatherMenuIniFileInformation(ByVal lpKeyName As String, ByVal lpDefault As String) As String
        GatherMenuIniFileInformation = ""
        '************************************************************
        '* ManTech Test Systems Software Module                     *
        '************************************************************
        '* Nomenclature   : VIPERT SYSTEM LOG MENU                    *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     Graphically puts Window at top of desktop stack      *
        '*    PARAMETERS:                                           *
        '*     lpKeyName$ = Key To Extract Data From                *
        '*     lpDefault$ = Default Key Name                        *
        '*    EXAMPLE:                                              *
        '*      GatherMenuIniFileInformation "SYSTEM_SURVEY","C:\SYSTEMSURVEY.exe" *
        '************************************************************
        Dim lpApplicationName As String '[HEADING] in INI File   
        Dim lpReturnedString As New StringBuilder(Space(255), 255) 'Return Buffer
        Dim nSize As Integer 'Return Buffer Size
        Dim lpFileName As String 'INI File Key Name "Key=?"
        Dim ReturnValue As Integer 'Return Value Buffer
        Dim FileNameInfo As String 'Formatted Return String
        Dim lpString As String 'String to write to INI File

        'Clear String Buffer
        lpReturnedString = New StringBuilder(Space(255))
        lpFileName = Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\ATS\", "IniFilePath", Nothing)
        nSize = 255
        'Find File Locations
        lpApplicationName = "File Locations"
        ReturnValue = GetPrivateProfileString(lpApplicationName, lpKeyName, lpDefault, lpReturnedString, nSize, lpFileName)
        FileNameInfo = Trim(lpReturnedString.ToString())
        FileNameInfo = Mid(FileNameInfo, 1, Len(FileNameInfo))
        'If File Locations Missing, then create empty keys
        If FileNameInfo = lpDefault + Convert.ToString(Chr(0)) Or FileNameInfo = Convert.ToString(lpDefault) Then
            lpString = " "
            ReturnValue = WritePrivateProfileString(lpApplicationName, lpKeyName, lpString, lpFileName)
        End If
        'Return Information In INI File
        GatherMenuIniFileInformation = FileNameInfo
    End Function

    Function GetSerialNumber(ByVal FromHdd As Boolean) As String
        GetSerialNumber = ""
        '---Added/Modified for Version 1.7 DWH---
        '************************************************************
        '* ManTech Test Systems Software Module                     *
        '************************************************************
        '* Nomenclature   : VIPERT SYSTEM LOG MENU                    *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     Graphically puts Window at top of desktop stack      *
        '*    PARAMETERS:                                           *
        '*     lpKeyName$ = Key To Extract Data From                *
        '*     lpDefault$ = Default Key Name                        *
        '*    EXAMPLE:                                              *
        '*      BringToTop Me.hwnd                                  *
        '************************************************************
        Dim lpApplicationName As String '[HEADING] in INI File
        
        Dim lpReturnedString As New StringBuilder(Space(255), 255) 'Return Buffer
        Dim nSize As Integer 'Return Buffer Size
        Dim lpFileName As String 'INI File Key Name "Key=?"
        Dim ReturnValue As Integer 'Return Value Buffer
        Dim FileNameInfo As String 'Formatted Return String
        Dim lpKeyName As String
        Dim lpDefault As String

        'Clear String Buffer
        lpReturnedString = New StringBuilder(Space(255))

        If FromHdd = False Then
            lpFileName = sFDD & "\Users\Public\Documents\ATS\ATS.INI"
        Else
            'Find Windows Directory
            lpFileName = Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\ATS\", "IniFilePath", Nothing)
        End If
        nSize = 255
        'Find File Locations
        lpApplicationName = "System Startup"
        lpKeyName = "SN"
        If FromHdd = False Then
            lpDefault = ""
        Else
            lpDefault = "001"
        End If
        ReturnValue = GetPrivateProfileString(lpApplicationName, lpKeyName, lpDefault, lpReturnedString, nSize, lpFileName)
        FileNameInfo = Trim(lpReturnedString.ToString())
        FileNameInfo = Mid(FileNameInfo, 1, Len(FileNameInfo))

        'Return Information In INI File
        GetSerialNumber = FileNameInfo

        '---End of Modification Version 1.7 DWH---
    End Function
    Sub WriteSerialNumber(ByVal SnKey As String)
        '---Added/Modified for Version 1.7 DWH---
        '************************************************************
        '* ManTech Test Systems Software Module                     *
        '************************************************************
        '* Nomenclature   : VIPERT SYSTEM LOG MENU                    *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     Graphically puts Window at top of desktop stack      *
        '*    PARAMETERS:                                           *
        '*     lpKeyName$ = Key To Extract Data From                *
        '*     lpDefault$ = Default Key Name                        *
        '*    EXAMPLE:                                              *
        '*      BringToTop Me.hwnd                                  *
        '************************************************************
        Dim lpApplicationName As String '[HEADING] in INI File
        Dim lpFileName As String 'INI File Key Name "Key=?"
        Dim ReturnValue As Integer 'Return Value Buffer
        Dim lpString As String 'String to write to INI File
        Dim lpKeyName As String

        'Find Windows Directory
        lpFileName = Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\ATS\", "IniFilePath", Nothing)
        'Find File Locations
        lpApplicationName = "System Startup"
        lpKeyName = "SN"
        lpString = SnKey
        ReturnValue = WritePrivateProfileString(lpApplicationName, lpKeyName, lpString, lpFileName)

        '---End of Modification Version 1.7 DWH---
    End Sub


    Function GetDirectories(ByVal FilePath As String) As String
        GetDirectories = ""
        '************************************************************
        '* ManTech Test Systems Software Module                     *
        '************************************************************
        '* Nomenclature   : VIPERT SYSTEM MENU                        *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     This Function Finds all of the disk directories in a *
        '*     given disk memory path                               *
        '*    PARAMETERS:                                           *
        '*    FilePath   : Logical Disk Media Path to Search        *
        '*    [Returns] = Comma dileniated string containing the    *
        '*     names of all directories in the given path.          *
        '*    EXAMPLE:                                              *
        '*      DirNames$=GetDirectories("C:\TPS")                  *
        '************************************************************
        'This Routine was added due to an IPR Customer Request for a
        'TPS Menu System DataBase ID#104
        Dim DirectoriesInPath As String = ""
        Dim DirName As String

        DirName = Dir(FilePath, Microsoft.VisualBasic.FileAttribute.Directory) ' Retrieve the first entry.

        Do While (DirName <> "") ' Start the loop.
            If (DirName <> ".") And (DirName <> "..") And (UCase(DirName) <> "IETM") Then ' Ignore the current directory and the encompassing directory.
                If (GetAttr(FilePath & DirName) And Microsoft.VisualBasic.FileAttribute.Directory) = Microsoft.VisualBasic.FileAttribute.Directory Then ' Use bitwise comparison to make sure MyName is a directory.
                    DirectoriesInPath &= DirName & "," ' Display entry only if it it represents a directory.
                End If
            End If
            DirName = Dir() ' Get next entry.
        Loop

        'Strip Comma Dileneator
        If DirectoriesInPath <> "" Then
            If Mid(DirectoriesInPath, Len(DirectoriesInPath), 1) = "," Then
                DirectoriesInPath = Mid(DirectoriesInPath, 1, Len(DirectoriesInPath) - 1)
            End If
        End If

        'Return Function Value
        GetDirectories = DirectoriesInPath

    End Function

    Function GetTpsFiles(ByVal FilePath As String) As String
        GetTpsFiles = ""
        '************************************************************
        '* ManTech Test Systems Software Module                     *
        '************************************************************
        '* Nomenclature   : VIPERT SYSTEM MENU                        *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     This Function Finds all of the TPS files in a        *
        '*     given disk memory path                               *
        '*    PARAMETERS:                                           *
        '*    FilePath   : Logical Disk Media Path to Search        *
        '*    [Returns] = Comma dileniated string containing the    *
        '*     names of all .obj files in the given path.           *
        '*    EXAMPLE:                                              *
        '*      TpsProgName$=GetTpsFiles("C:\TPS")                  *
        '************************************************************
        'This Routine was added due to an IPR Customer Request for a
        'TPS Menu System DataBase ID#104
        Dim DirectoriesInPath As String = ""
        Dim DirName As String

        DirName = Dir(FilePath, Microsoft.VisualBasic.FileAttribute.Normal + Microsoft.VisualBasic.FileAttribute.Hidden + Microsoft.VisualBasic.FileAttribute.System + Microsoft.VisualBasic.FileAttribute.Volume + Microsoft.VisualBasic.FileAttribute.Directory) ' Retrieve the first entry.

        Do While DirName <> "" ' Start the loop.
            If Len(DirName) > 3 Then ' Ignore the current directory and the encompassing directory.
                If Not ((GetAttr(FilePath & DirName) And Microsoft.VisualBasic.FileAttribute.Directory) = Microsoft.VisualBasic.FileAttribute.Directory) Then ' Use bitwise comparison to make sure MyName is not a directory.
                    If (UCase(Mid(DirName, Len(DirName) - 3, 4)) = ".OBJ") Or bTPSExecFile(DirName) Then
                        DirectoriesInPath &= DirName & "," ' Display entry only if it it represents a tps.
                    End If
                End If
            End If
            DirName = Dir() ' Get next entry.
        Loop

        'Strip Comma Dileneator
        If DirectoriesInPath <> "" Then
            If Mid(DirectoriesInPath, Len(DirectoriesInPath), 1) = "," Then
                DirectoriesInPath = Mid(DirectoriesInPath, 1, Len(DirectoriesInPath) - 1)
            End If
        End If
        'Return Function Value
        GetTpsFiles = DirectoriesInPath


    End Function

    Function GetIetmFiles(ByVal FilePath As String) As String
        GetIetmFiles = ""
        '************************************************************
        '* ManTech Test Systems Software Module                     *
        '************************************************************
        '* Nomenclature   : VIPERT SYSTEM MENU                        *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     This Function Finds all of the IETM files in a       *
        '*     given disk memory path                               *
        '*    PARAMETERS:                                           *
        '*    FilePath   : Logical Disk Media Path to Search        *
        '*    [Returns] = Comma dileniated string containing the    *
        '*     names of all .ide files in the given path.           *
        '*    EXAMPLE:                                              *
        '*      IetmDocName$=GetIetmFiles("C:\TPS")                 *
        '************************************************************
        'This Routine was added due to an IPR Customer Request for a
        'TPS Menu System DataBase ID#104
        Dim DirectoriesInPath As String = ""
        Dim DirName As String

        If (System.IO.Directory.Exists(FilePath)) Then
            DirName = Dir(FilePath, Microsoft.VisualBasic.FileAttribute.Normal + Microsoft.VisualBasic.FileAttribute.Hidden + Microsoft.VisualBasic.FileAttribute.System + Microsoft.VisualBasic.FileAttribute.Volume + Microsoft.VisualBasic.FileAttribute.Directory) ' Retrieve the first entry.

            Do While DirName <> "" ' Start the loop.
                If Len(DirName) > 3 Then ' Ignore the current directory and the encompassing directory.
                    If Not ((GetAttr(FilePath & DirName) And Microsoft.VisualBasic.FileAttribute.Directory) = Microsoft.VisualBasic.FileAttribute.Directory) Then ' Use bitwise comparison to make sure MyName is not a directory.
                        If UCase(Mid(DirName, Len(DirName) - 3, 4)) = ".IDE" Or _
                            UCase(Mid(DirName, Len(DirName) - 3, 4)) = ".SGM" Or _
                            UCase(Mid(DirName, Len(DirName) - 3, 4)) = ".XML" Then
                            DirectoriesInPath &= DirName & "," ' Display entry only if it it represents an IETM DOc.
                        End If
                    End If
                End If
                DirName = Dir() ' Get next entry.
            Loop

            'Strip Comma Dileneator
            If DirectoriesInPath <> "" Then
                If Mid(DirectoriesInPath, Len(DirectoriesInPath), 1) = "," Then
                    DirectoriesInPath = Mid(DirectoriesInPath, 1, Len(DirectoriesInPath) - 1)
                End If
            End If
            'Return Function Value
            GetIetmFiles = DirectoriesInPath
        End If

    End Function

    Public Sub Main()
        '************************************************************
        '* ManTech Test Systems Software Module                     *
        '************************************************************
        '* Nomenclature   : VIPERT SYSTEM LOG MENU                    *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     Program Entry Point-Execution Begins Here            *
        '************************************************************
        Dim lpDefault As String 'Set Default INI File Key
        Dim Program As Short 'Index For Configuration/Access Arrays
        Dim lpKeyName As String 'INI File Key Name
        Dim lpReturnedString As New StringBuilder(256)

        If bDebug Then CreateDebugFile()

        If bDebug Then WriteDebugInfo("MAIN.Main() -- Checking for path to ATS.ini")
        'get ini file path
        iniFilePath = Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\ATS\", "IniFilePath", Nothing)

        'Allow only one instance of program
        If AppPrevInstance Then Application.Exit()

        If bDebug Then WriteDebugInfo("MAIN.Main() - No other instance of SysMenu is running")

        'Detect drive letter of system USB Disk Drive

        If System.IO.DriveInfo.GetDrives().Any(Function(di As System.IO.DriveInfo) di.Name.ToUpper = "F:\") Then '  oFileSystem.DriveExists("F:") Then       ' if hd is set up correctly then F will be the USB drive
            sFDD = "F:\"
            nDrive = 5
        ElseIf System.IO.DriveInfo.GetDrives().Any(Function(di As System.IO.DriveInfo) di.Name.ToUpper = "G:\") Then  'incase another hd, usb is present and taking up the F slot
            sFDD = "G:\"
            nDrive = 6
        ElseIf System.IO.DriveInfo.GetDrives().Any(Function(di As System.IO.DriveInfo) di.Name.ToUpper = "E:\") Then  'incase the hd isn't partitioned it checks to make sure E is the removeable
            sFDD = "E:\"
            nDrive = 4
        Else
            'MsgBox "No USB drive detected. Operations involving USB Disk will not succeed.", vbCritical
        End If

        sFileSelfX = sFDD & "FHBD_Database.zip"     'Self Extracting Zip File Name
        sFDD_DB = sFDD & "FHDB.mdb"                 'File name and location on USB Disk

        'CenterForm(frmSysPanl)
        Q = Convert.ToString(Chr(34))

        'DME change to populate the form with the cal date
        'STR 15653 remove Cal Date from Sys Menu JW 10/25/18
        'setCaldates()  

        '---Added/Modified for Version 1.7 DWH---
        'Init Input Box Variables
        SetCur(VIPERT_SN) = "1000" ' "Current Settings" Array
        SetDef(VIPERT_SN) = "1000" ' "Default Settings" Array
        SetMin(VIPERT_SN) = "1000" ' "Minimum Settings" Array
        SetMax(VIPERT_SN) = "9999" ' "Maximum Settings" Array
        SetUOM(VIPERT_SN) = "1" ' "Unit Of Measure" Array
        SetInc(VIPERT_SN) = "1" ' "Increments" Array
        SetRes(VIPERT_SN) = "0000" ' "Increments" Array
        SetCmd(VIPERT_SN) = "NONE" ' "SCPI command" Array
        SetRngMsg(VIPERT_SN) = "Invalid Serial Number.  Enter a number from 1000 to 5999" ' "Range Message" for Status Bar Array
        SetMinInc(VIPERT_SN) = "1" ' "Increments" Array
        '---End of Modification Version 1.7 DWH---

        'Init Key Names
        FileNameKey(SAIS_TOOLBAR) = "SAIS_TOOLBAR"
        FileNameKey(PAWS_RTS) = "PAWS_RTS"
        FileNameKey(IADS_READER) = "IADS_READER"
        FileNameKey(SYSTEM_SURVEY) = "SYSTEM_SURVEY"
        FileNameKey(SYSTEM_SELF_TEST) = "SYSTEM_SELF_TEST"
        FileNameKey(SYSTEM_CALIBRATION) = "SYSTEM_CALIBRATION"
        FileNameKey(SYSTEM_LOG) = "SYSTEM_LOG"
        FileNameKey(FHDB_PROCESSOR) = "FHDB_PROCESSOR" 'DJoiner ECP-VIPERT-23, FHDB
        FileNameKey(STOW) = "STOW" 'DR214  DJoiner  11/21/01
        FileNameKey(TIPS) = "TIPS"
        IatmKey = "ATS_IATM"
        lpDefault = "Not Found"

        If bDebug Then WriteDebugInfo("MAIN.Main() - Checking user permissions")
        'Grant Access According To Login Name
        CheckUserName()

        If bDebug Then WriteDebugInfo("MAIN.Main() - Updating userform based on access level")
        'If User does not have access or Program is missing or not configured in INI file, then
        'the control will be invisible.
        For Program = 1 To NUMBER_OF_PROGRAMS
            lpKeyName = FileNameKey(Program)
            FileNameData(Program) = GatherMenuIniFileInformation(lpKeyName, lpDefault)

            If LoginAccess(Program) = True And System.IO.File.Exists(FileNameData(Program)) Then
                frmSysPanl.ribProgram(Program).Visible = True
                frmSysPanl.lblProgDescription(Program).Visible = True
            Else
                frmSysPanl.ribProgram(Program).Visible = False
                frmSysPanl.lblProgDescription(Program).Visible = False
            End If
        Next Program

        'EXCEPTION: System Calibration Is Hosted On a CD
        If LoginAccess(SYSTEM_CALIBRATION) = True Then
            If bDebug Then WriteDebugInfo("MAIN.Main() - User has access to calibration functionality")
            frmSysPanl.ribProgram(SYSTEM_CALIBRATION).Visible = True
            frmSysPanl.lblProgDescription(SYSTEM_CALIBRATION).Visible = True
        End If

        'EXCEPTION: IETM Is Hosted On a CD
        '1320 change to Sys Menu JW 10/25/18
        If LoginAccess(IADS_READER) = True Then
            If bDebug Then WriteDebugInfo("MAIN.Main() - User has access to IADS functionality")
            frmSysPanl.ribProgram(IADS_READER).Visible = False
            frmSysPanl.lblProgDescription(IADS_READER).Visible = False
            IatmFile = GatherMenuIniFileInformation(IatmKey, lpDefault)
        End If

        'EXCEPTION: TPS MENU Depends upon the PAWS RTS
        If LoginAccess(TPS_MENU) = True Then
            If bDebug Then WriteDebugInfo("MAIN.Main() - User has access to TPS menu functionality")
            If LoginAccess(PAWS_RTS) = True And System.IO.File.Exists(FileNameData(PAWS_RTS)) Then
                frmSysPanl.ribProgram(TPS_MENU).Visible = True
                frmSysPanl.lblProgDescription(TPS_MENU).Visible = True
            Else
                frmSysPanl.ribProgram(TPS_MENU).Visible = False
                frmSysPanl.lblProgDescription(TPS_MENU).Visible = False
            End If
        End If

        'EXCEPTION: TPS Tech Manual Depends upon the IADS
        '1320 change to Sys Menu JW 10/25/18
        If LoginAccess(TPS_TM) = True Then
            If bDebug Then WriteDebugInfo("MAIN.Main() - User has access to TPS TM functionality")
            If LoginAccess(IADS_READER) = True And System.IO.File.Exists(FileNameData(IADS_READER)) Then
                frmSysPanl.ribProgram(TPS_TM).Visible = True
                frmSysPanl.lblProgDescription(TPS_TM).Visible = True
            Else
                frmSysPanl.ribProgram(TPS_TM).Visible = False
                frmSysPanl.lblProgDescription(TPS_TM).Visible = False
            End If
        End If

        '---Added/Modified for Version 1.7 DWH---
        'EXCEPTION: Update and Restore UDD Do not launch external EXECUTABLES, but
        'Functions, and Subs in this EXECUTABLE
        If LoginAccess(UPDATE_UDD) = True Then
            If bDebug Then WriteDebugInfo("MAIN.Main() - User has access to UDD Update functionality")
            frmSysPanl.ribProgram(UPDATE_UDD).Visible = True
            frmSysPanl.lblProgDescription(UPDATE_UDD).Visible = True
        Else
            frmSysPanl.ribProgram(UPDATE_UDD).Visible = False
            frmSysPanl.lblProgDescription(UPDATE_UDD).Visible = False
        End If

        If LoginAccess(RESTORE_UDD) = True Then
            If bDebug Then WriteDebugInfo("MAIN.Main() - User has access to UDD Restore functionality")
            frmSysPanl.ribProgram(RESTORE_UDD).Visible = True
            frmSysPanl.lblProgDescription(RESTORE_UDD).Visible = True
        Else
            frmSysPanl.ribProgram(RESTORE_UDD).Visible = False
            frmSysPanl.lblProgDescription(RESTORE_UDD).Visible = False
        End If
        '---End of Modification Version 1.7 DWH---

        'JHill V1.9 DR #109, ECP-026 begin -----
        'EXCEPTION: Info files use IExplorer
        '1320 change to Sys Menu JW 10/25/18
        If LoginAccess(INVENTORY_LIST) = True Then
            If bDebug Then WriteDebugInfo("MAIN.Main() - User has access to Inventory List functionality")
            frmSysPanl.ribProgram(INVENTORY_LIST).Visible = False
            frmSysPanl.lblProgDescription(INVENTORY_LIST).Visible = False
        End If

        If LoginAccess(SETUP_PROC) = True Then
            If bDebug Then WriteDebugInfo("MAIN.Main() - User has access to Setup Procedure functionality")
            '1320 change to Sys Menu JW 10/25/18
            frmSysPanl.ribProgram(SETUP_PROC).Visible = False
            frmSysPanl.lblProgDescription(SETUP_PROC).Visible = False
        End If
        'JHill V1.9 DR #109, ECP-026 end -----

        'If EO System, show STOW EO Collimator Button
        'STR 15510 ON the Viper System Software, the operator needs to have access to the "Stow" button for the VEO2 
        'regardless of what version system they are using  JW 10/25/18
        'GetPrivateProfileString("System Startup", "EO_OPTION_INSTALLED", "", lpReturnedString, 256, iniFilePath)
        'If (lpReturnedString.ToString.Contains("YES")) Then
        LoginAccess(STOW) = True
        frmSysPanl.ribProgram(STOW).Visible = True
        frmSysPanl.lblProgDescription(STOW).Visible = True
        'Else
        ' LoginAccess(STOW) = False
        'End If

        'move syscal icon
        MoveSysCal()

        'Check System for CD Drives
        If bDebug Then WriteDebugInfo("MAIN.Main() - Checking Optical Drives")
        CdDrivesOnSystem = GetCompactDiscDrives()

        'Show Menu
        frmSysPanl.tabUserOptions.SelectedIndex = 0
        SendStatusBarMessage("Automated Test System Main Menu.")
        frmSysPanl.lblUser.Text = "ATS Menu [" & UserName & "]"

    End Sub

    Public Sub USB_Detect()
        
        Dim mBr As DialogResult

        If System.IO.DriveInfo.GetDrives().Any(Function(di As System.IO.DriveInfo) di.Name.ToUpper = "G:\") Then 'G: is the default location for the USB
            If System.IO.File.Exists("G:\users\public\documents\ats\ats.ini") Then
                sFDD = "G:\"
                nDrive = 6
                usb_flag = True
            Else
                mBr = MsgBox("A USB device has been detected as drive 'G:'." & vbCrLf & "Drive 'G:' has not been formatted as a VIPER/T UDD." & vbCrLf & vbCrLf & "Would you like to use this drive as the VIPER/T UDD?.", MsgBoxStyle.YesNo)
                If mBr = DialogResult.Yes Then
                    sFDD = "G:\"
                    nDrive = 6
                    usb_flag = True
                    uddHDflag = False
                Else
                    usb_flag = False
                End If
            End If
        End If

        If System.IO.DriveInfo.GetDrives().Any(Function(di As System.IO.DriveInfo) di.Name.ToUpper = "H:\") And (mBr = 0 Or mBr = DialogResult.No) Then 'G: is the default location for the USB
            If System.IO.File.Exists("H:\users\public\documents\ats\ats.ini") Then
                sFDD = "H:\"
                nDrive = 7
                usb_flag = True
            Else
                mBr = MsgBox("A USB device has been detected as drive 'H:'." & vbCrLf & "Drive 'H:' has not been formatted as a ATS UDD." & vbCrLf & vbCrLf & "Would you like to use this drive as the ATS UDD?.", MsgBoxStyle.YesNo)
                If mBr = DialogResult.Yes Then
                    sFDD = "H:\"
                    nDrive = 7
                    usb_flag = True
                    uddHDflag = False
                Else
                    usb_flag = False
                End If
            End If
        End If

        If Not (System.IO.DriveInfo.GetDrives().Any(Function(di As System.IO.DriveInfo) di.Name.ToUpper = "H:\")) And Not (System.IO.DriveInfo.GetDrives().Any(Function(di As System.IO.DriveInfo) di.Name.ToUpper = "G:\")) Then
            MsgBox("No USB drive detected.", MsgBoxStyle.OkOnly)
            usb_flag = False
        End If

        sFileSelfX = sFDD & "FHBD_Database.zip" 'Self Extracting Zip File Name
        sFDD_DB = sFDD & "FHDB.mdb" 'File name and location on USB Disk
    End Sub

    Public Function UDD_HD_Path() As String
        UDD_HD_Path = ""

        Dim UDDPath As String
        Dim hWnd As Integer

        UDDPath = BrowseFolders(hWnd, "Select a Folder", BrowseType.BrowseForFolders, FolderType.CSIDL_DESKTOP)

        If UDDPath = "" Then Exit Function


        UDD_HD_Path = UDDPath

    End Function

    Public Function BrowseFolders(ByVal hWndOwner As Integer, ByVal sMessage As String, ByVal Browse As BrowseType, ByVal RootFolder As FolderType) As String
        BrowseFolders = ""
        Dim Nullpos As Short
        Dim lpIDList As Integer
        Dim res As Integer
        Dim sPath As String
        Dim BInfo As BrowseInfo
        Dim RootID As Integer

        SHGetSpecialFolderLocation(hWndOwner, RootFolder, RootID)
        BInfo.hWndOwner = hWndOwner
        BInfo.lpszTitle = sMessage
        BInfo.ulFlags = Browse
        If RootID <> 0 Then BInfo.pIDLRoot = RootID
        lpIDList = SHBrowseForFolder(BInfo)
        If lpIDList <> 0 Then
            sPath = StrDup(MAX_PATH, Chr(0))
            res = SHGetPathFromIDList(lpIDList, sPath)
            CoTaskMemFree(lpIDList)
            Nullpos = InStr(sPath, vbNullChar)
            If Nullpos <> 0 Then
                sPath = Strings.Left(sPath, Nullpos - 1)
            End If
        End If
        BrowseFolders = sPath
    End Function



    Sub ProgramComment(ByVal ProgramFile As Short)
        '************************************************************
        '* ManTech Test Systems Software Module                     *
        '************************************************************
        '* Nomenclature   : VIPERT SYSTEM LOG MENU                    *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     This Routine will echo comments in the status bar    *
        '*    PARAMETERS:                                           *
        '*     ProgramFile% = Program Index Value                   *
        '*    EXAMPLE:                                              *
        '*      ProgramComment SYSLOG                               *
        '************************************************************
        Dim Comment As String 'Holds the Comment Text to be displayed

        Select Case ProgramFile
            Case SAIS_TOOLBAR
                Comment = "SAIS Toolbar provides graphical selection of all instrument options installed in ATS"
            Case PAWS_RTS
                Comment = "PAWS RTS provides the Run-Time Engine for Test Program Sets (TPS)."
            Case IADS_READER
                Comment = "IADS Reader provides on-line technical documentation."
            Case SYSTEM_SURVEY
                Comment = "System Survey provides a confidence level test of the installed ATS instrumentation."
            Case SYSTEM_SELF_TEST
                Comment = "System Self Test provides a detailed check of installed ATS instrumentation."
            Case SYSTEM_CALIBRATION
                Comment = "System Calibration Software provides system calibration functions and to determines path loss at the UUT interface."
            Case SYSTEM_LOG
                Comment = "System Log Viewer provides recorded system tests, events, alarms, and warnings."
            Case TPS_MENU
                Comment = "Select and Execute a Test Program on an APS (Application Program Set) CDROM."
            Case TPS_TM
                Comment = "Select and View an Interactive Electronic Technical Manual for a Test Program Set."
                'Case TIPS
                '    Comment = "VIPERT Interactive Program Studio provides capability to design, run, and manipulate a semi-automated test program."
                '---Added/Modified for Version 1.7 DWH---
            Case UPDATE_UDD
                Comment = "The Unique Data Disk Wizard archives data unique to the ATS station for recovery purposes."
            Case RESTORE_UDD
                Comment = "Restore archived data unique to the ATS station."
                '---End of Modification Version 1.7 DWH---
                'JHill V1.9 DR #109, ECP-026 begin -----
            Case INVENTORY_LIST
                Comment = "View/Print system inventory list."
            Case SETUP_PROC
                Comment = "View/Print system setup and turn-on procedure."
                'JHill V1.9 DR #109, ECP-026 end -----
                'ECP-VIPERT-23, FHDB - DJoiner 04/30/01
            Case FHDB_PROCESSOR
                Comment = "Fault History Database Processor provides access to the Viewer, Importer, and Exporter."
            Case STOW
                Comment = "STOW VEO2 Collimator"

        End Select
        'Put Comment in Status Bar
        SendStatusBarMessage(Comment)

    End Sub

    'STR 15653 remove Cal Date from Sys Menu JW 10/25/18
    'Sub setCaldates()

    '    Dim intDate As String
    '    Dim dueDate As String

    '    Dim todayDate As Integer
    '    Dim days_left As Integer

    '    'get the cal date from the VIPERT.ini file in integer as string format
    '    intDate = GatherIniFileInformation("Calibration", "SYSTEM_EFFECTIVE", "")

    '    If intDate = "" Then
    '        intDate = 0
    '    End If

    '    'format the cal date
    '    dueDate = VB6.Format(intDate, "mm-dd-yy")

    '    'get today's date
    '    todayDate = DateSerial(Year(Now), Month(Now), DateAndTime.Day(Now)).ToOADate

    '    'Cal date red "overdue"
    '    If CLng(intDate) <= todayDate Then

    '        frmSysPanl.Cal_date0.ForeColor = Color.Red
    '        frmSysPanl.Cal_date0.Text = "Calibration Due Date: " & dueDate & ", OVERDUE"
    '        frmSysPanl.Cal_date1.ForeColor = Color.Red
    '        frmSysPanl.Cal_date1.Text = "Calibration Due Date: " & dueDate & ", OVERDUE"
    '        frmSysPanl.Cal_date2.ForeColor = Color.Red
    '        frmSysPanl.Cal_date2.Text = "Calibration Due Date: " & dueDate & ", OVERDUE"
    '    End If

    '    'Cal date green "a-ok"
    '    If CLng(intDate) > (todayDate + 30) Then
    '        frmSysPanl.Cal_date0.ForeColor = Color.Green
    '        frmSysPanl.Cal_date0.Text = "Calibration Due Date: " & dueDate & ", Calibration OK"
    '        frmSysPanl.Cal_date1.ForeColor = Color.Green
    '        frmSysPanl.Cal_date1.Text = "Calibration Due Date: " & dueDate & ", Calibration OK"
    '        frmSysPanl.Cal_date2.ForeColor = Color.Green
    '        frmSysPanl.Cal_date2.Text = "Calibration Due Date: " & dueDate & ", Calibration OK"
    '    End If

    '    'Cal date yellow "30 or less days to cal"
    '    If CLng(intDate) <= (todayDate + 30) And CLng(intDate) > todayDate Then

    '        'calculate days left to calibration
    '        Dim i As Short
    '        For i = 0 To 29
    '            If (todayDate + 30 - i) = CLng(intDate) Then days_left = 30 - i
    '        Next i

    '        'display info
    '        frmSysPanl.Cal_date0.ForeColor = Color.Goldenrod
    '        frmSysPanl.Cal_date0.Text = "Calibration Due Date: " & dueDate & ", " & days_left & " Day(s) Remain"
    '        frmSysPanl.Cal_date1.ForeColor = Color.Goldenrod
    '        frmSysPanl.Cal_date1.Text = "Calibration Due Date: " & dueDate & ", " & days_left & " Day(s) Remain"
    '        frmSysPanl.Cal_date2.ForeColor = Color.Goldenrod
    '        frmSysPanl.Cal_date2.Text = "Calibration Due Date: " & dueDate & ", " & days_left & " Day(s) Remain"
    '    End If

    'End Sub

    Sub SendStatusBarMessage(ByVal MessageString As String)
        '************************************************************
        '* ManTech Test Systems Software Module                     *
        '************************************************************
        '* Nomenclature   : VIPERT SYSTEM LOG MENU                    *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     This Routine will echo comments in the status bar    *
        '*    PARAMETERS:                                           *
        '*     MessageString$ = Data to be Echoed to status bar     *
        '*    EXAMPLE:                                              *
        '*      SendStatusBarMessage "This is Written in StatusBar" *
        '************************************************************

        'If Message is already there do not re-print
        If Convert.ToString(MessageString) <> frmSysPanl.StatusBar1.Panels(1 - 1).Text Then
            frmSysPanl.StatusBar1.Panels(1 - 1).Text = Convert.ToString(MessageString) 'This reduces status bar flicker
        End If

    End Sub

    
    Sub CheckForTpsCd()
        Dim allDrives() As IO.DriveInfo = IO.DriveInfo.GetDrives()

        Dim d As DriveInfo
        Dim iniFileName As String = ""
        Dim lpReturnedString As New StringBuilder(1024)

        'Reset Global TPS Disc Flag
        tpsDiscInserted = False

        For Each d In allDrives

            If (d.DriveType = DriveType.CDRom Or d.DriveType = DriveType.Removable Or d.DriveType = DriveType.Fixed) Then
                If (d.IsReady = True) Then
                    'Check If INI file is on Media
                    If (File.Exists(d.RootDirectory.ToString() & "VIPERT_TPS.INI") Or File.Exists(d.RootDirectory.ToString() & "TETS_TPS.INI") Or File.Exists(d.RootDirectory.ToString() & "SAVEO_TPS.INI") Or File.Exists(d.RootDirectory.ToString() & "ATS_TPS.INI")) Then
                        tpsDiscInserted = True
                        frmSysPanl.lblInstructions.Visible = False
                        frmSysPanl.trvTpsList.Visible = True
                        frmSysPanl.panCdInformation.Visible = True
                        'frmSysPanl.Button16.Visible = True
                        'frmSysPanl.lblProgDescription_16.Visible = True
                        frmSysPanl.Button9.Visible = True
                        frmSysPanl.lblProgDescription_9.Visible = True

                        'Set INI File String
                        If (File.Exists(d.RootDirectory.ToString() & "VIPERT_TPS.INI")) Then
                            iniFileName = "VIPERT_TPS.INI"
                        ElseIf (File.Exists(d.RootDirectory.ToString() & "TETS_TPS.INI")) Then
                            iniFileName = "TETS_TPS.INI"
                        ElseIf (File.Exists(d.RootDirectory.ToString() & "SAVEO_TPS.INI")) Then
                            iniFileName = "SAVEO_TPS.INI"
                        ElseIf (File.Exists(d.RootDirectory.ToString() & "ATS_TPS.INI")) Then
                            iniFileName = "ATS_TPS.INI"
                        End If

                        'Find Media Version

                        GetPrivateProfileString("TPS", "CD_VERSION", "", lpReturnedString, 1024, d.RootDirectory.ToString() & iniFileName)
                        If (lpReturnedString.ToString = "") Then
                            frmSysPanl.lblTpsInformation_1.Text = "Not Available"
                        Else
                            frmSysPanl.lblTpsInformation_1.Text = lpReturnedString.ToString()
                        End If


                        'Find Media Part Number
                        GetPrivateProfileString("TPS", "CD_PN", "", lpReturnedString, 1024, d.RootDirectory.ToString() & iniFileName)
                        If (lpReturnedString.ToString = "") Then
                            frmSysPanl.lblTpsInformation_2.Text = "Not Available"
                        Else
                            frmSysPanl.lblTpsInformation_2.Text = lpReturnedString.ToString()
                        End If

                        'Get Media Volume Information
                        If (d.VolumeLabel = "") Then
                            frmSysPanl.lblTpsInformation_0.Text = "[No Title]"
                        Else
                            frmSysPanl.lblTpsInformation_0.Text = d.VolumeLabel.ToString()

                        End If

                        If (lastMediaPartNumber <> frmSysPanl.lblTpsInformation_2.Text) And (lastMediaVolumeLabel <> frmSysPanl.lblTpsInformation_0.Text) Then

                            'Fill Tree Control
                            frmSysPanl.trvTpsList.Nodes.Clear()

                            Dim NumDirs As Integer = 0
                            Dim AIndex As Integer = 0
                            Dim BIndex As Integer = 0
                            Dim CIndex As Integer = 0
                            Dim DIndex As Integer = 0
                            Dim SystemCount = 0
                            Dim AssemblyCount = 0
                            Dim TestProgramCount = 0
                            Dim ALevelDirectories() As String
                            Dim temp_str(1) As String

                            If d.DriveType = DriveType.Fixed Then
                                ALevelDirectories = Directory.GetDirectories(d.RootDirectory.ToString() & "TPS_Programs\")
                            Else
                                ALevelDirectories = Directory.GetDirectories(d.RootDirectory.ToString())
                            End If


                            For AIndex = 0 To ALevelDirectories.Length - 1
                                Dim ALevelNode As New TreeNode()
                                ALevelNode.SelectedImageIndex = 3
                                SystemCount = SystemCount + 1

                                ALevelNode.Text = stripLastPartOfDirectoryName(ALevelDirectories(AIndex))
                                frmSysPanl.trvTpsList.Nodes.Add(ALevelNode)

                                Dim BLevelDirectories As String() = Directory.GetDirectories(ALevelDirectories(AIndex))
                                For BIndex = 0 To BLevelDirectories.Length - 1
                                    temp_str(0) = stripLastPartOfDirectoryName(BLevelDirectories(BIndex).ToString)
                                    If ((String.Compare(temp_str(0), "IETM")) = 0) Then
                                        GoTo SKIPIETM
                                    End If
                                    AssemblyCount = AssemblyCount + 1
                                    Dim BLevelNode As New TreeNode()
                                    BLevelNode.Text = stripLastPartOfDirectoryName(BLevelDirectories(BIndex).ToString)

                                    Dim CLevelDirectories As String() = Directory.GetDirectories(BLevelDirectories(BIndex))
                                    For CIndex = 0 To CLevelDirectories.Length - 1
                                        Dim CLevelNode As New TreeNode()
                                        CLevelNode.Text = stripLastPartOfDirectoryName(CLevelDirectories(CIndex.ToString))
                                        BLevelNode.Nodes.Add(CLevelNode)

                                        If Directory.GetFiles(CLevelDirectories(CIndex).ToString, "*.obj").Count > 0 Then
                                            Dim DLevelFiles As String() = Directory.GetFiles(CLevelDirectories(CIndex).ToString, "*.obj")
                                            For DIndex = 0 To DLevelFiles.Length - 1
                                                TestProgramCount = TestProgramCount + 1
                                                Dim DLevelNode As New TreeNode()
                                                DLevelNode.ToolTipText = DLevelFiles(DIndex.ToString)
                                                DLevelNode.Text = stripLastPartOfDirectoryName(DLevelFiles(DIndex.ToString))
                                                DLevelNode.ImageIndex = 0
                                                DLevelNode.SelectedImageIndex = 0
                                                CLevelNode.Nodes.Add(DLevelNode)
                                            Next
                                        Else
                                            Dim DLevelFiles As String() = Directory.GetFiles(CLevelDirectories(CIndex).ToString, "*.exe")
                                            For DIndex = 0 To DLevelFiles.Length - 1
                                                Dim tmpFile As String = DLevelFiles(DIndex)
                                                While tmpFile.Contains("\")
                                                    tmpFile = tmpFile.Remove(0, (tmpFile.IndexOf("\") + 1))
                                                End While

                                                If tmpFile.ToUpper.StartsWith("TP") Then
                                                    TestProgramCount = TestProgramCount + 1
                                                    Dim DLevelNode As New TreeNode()
                                                    DLevelNode.ToolTipText = DLevelFiles(DIndex.ToString)
                                                    DLevelNode.Text = stripLastPartOfDirectoryName(DLevelFiles(DIndex.ToString))
                                                    DLevelNode.ImageIndex = 0
                                                    DLevelNode.SelectedImageIndex = 0
                                                    CLevelNode.Nodes.Add(DLevelNode)
                                                End If
                                            Next
                                        End If
                                    Next

                                    frmSysPanl.trvTpsList.SelectedNode = ALevelNode
                                    frmSysPanl.trvTpsList.SelectedNode.Nodes.Add(BLevelNode)
SKIPIETM:
                                Next

                            Next

                            frmSysPanl.lblTpsInformation_3.Text = SystemCount
                            frmSysPanl.lblTpsInformation_4.Text = AssemblyCount
                            frmSysPanl.lblTpsInformation_5.Text = TestProgramCount

                        End If

                        'Only Display the First Media Found
                        lastMediaPartNumber = frmSysPanl.lblTpsInformation_2.Text
                        lastMediaVolumeLabel = frmSysPanl.lblTpsInformation_0.Text

                        Exit For
                    End If
                End If

            End If

        Next
        If (tpsDiscInserted = False) Then
            frmSysPanl.trvTpsList.Visible = False
            frmSysPanl.panCdInformation.Visible = False
            frmSysPanl.Button9.Visible = False
            frmSysPanl.lblProgDescription_9.Visible = False
            frmSysPanl.lblInstructions.Visible = True
        End If


    End Sub

    Sub CheckForTpsHd()
        Dim allDrives() As IO.DriveInfo = IO.DriveInfo.GetDrives()
        Dim SearchDir As String
        Dim d, TPSHD As DriveInfo
        Dim iniFileName As String = ""
        Dim lpReturnedString As New StringBuilder(1024)
        Dim useini As String
        Dim entrylist() As String
        Dim listctr As Short = 0

        'Reset Global TPS Disc Flag
        tpsHDProgsExist = False

        For Each d In allDrives
            If d.Name = TPSDrive And d.DriveType = DriveType.Fixed Then
                TPSHD = d
                Exit For
            End If
        Next

        If Not TPSHD Is Nothing Then
            If bDebug Then WriteDebugInfo("MAIN.CheckForTpsHd() - TPS hdd volume is:  " + TPSHD.Name)
            If (TPSHD.IsReady = True) Then
                'Check If INI file is on Media
                SearchDir = TPSDrive & TPSLocation
                Try
                    'For Each foundFile As String In My.Computer.FileSystem.GetFiles(SearchDir, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, "VIPERT_TPS.INI")
                    For Each foundFile As String In System.IO.Directory.GetFiles(SearchDir, "VIPERT_TPS.INI", SearchOption.AllDirectories)
                        iniFileList.Add(foundFile)
                    Next

                    'For Each foundFile As String In My.Computer.FileSystem.GetFiles(SearchDir, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, "ATS_TPS.INI")
                    For Each foundFile As String In System.IO.Directory.GetFiles(SearchDir, "ATS_TPS.INI", SearchOption.AllDirectories)
                        iniFileList.Add(foundFile)
                    Next

                    'For Each foundFile As String In My.Computer.FileSystem.GetFiles(SearchDir, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, "TETS_TPS.INI")
                    For Each foundFile As String In System.IO.Directory.GetFiles(SearchDir, "TETS_TPS.INI", SearchOption.AllDirectories)
                        iniFileList.Add(foundFile)
                    Next

                Catch ex As Exception
                    MsgBox(ex)
                End Try

                If iniFileList.Count > 0 Then
                    tpsHDProgsExist = True
                    frmSysPanl.lblInstructions.Visible = False
                    frmSysPanl.trvTpsList.Visible = True
                    frmSysPanl.panCdInformation.Visible = True
                    frmSysPanl.Button9.Visible = True
                    frmSysPanl.lblProgDescription_9.Visible = True

                    If (RefreshTreeView = True) Then

                        RefreshTreeView = False
                        'Fill Tree Control
                        frmSysPanl.trvTpsList.Nodes.Clear()

                        Dim NumDirs As Integer = 0
                        Dim RootIndex As Integer = 0
                        Dim AIndex As Integer = 0
                        Dim BIndex As Integer = 0
                        Dim CIndex As Integer = 0
                        Dim DIndex As Integer = 0
                        Dim SystemCount = 0
                        Dim AssemblyCount = 0
                        Dim TestProgramCount = 0
                        Dim RootLevelDirectories() As String
                        Dim ALevelDirectories() As String
                        Dim temp_str(1) As String

                        RootLevelDirectories = Directory.GetDirectories(SearchDir)

                        For RootIndex = 0 To RootLevelDirectories.Length - 1
                            Dim RootLevelNode As New TreeNode()
                            RootLevelNode.SelectedImageIndex = 3
                            SystemCount = SystemCount + 1
                            RootLevelNode.Text = stripLastPartOfDirectoryName(RootLevelDirectories(RootIndex))
                            If (frmSysPanl.tabUserOptions.Visible = True) Then
                                frmSysPanl.trvTpsList.Nodes.Add(RootLevelNode)
                            End If

                            ALevelDirectories = Directory.GetDirectories(RootLevelDirectories(RootIndex))
                            For AIndex = 0 To ALevelDirectories.Length - 1
                                Dim ALevelNode As New TreeNode()
                                ALevelNode.Text = stripLastPartOfDirectoryName(ALevelDirectories(AIndex))
                                If (frmSysPanl.tabUserOptions.Visible = True) Then
                                    RootLevelNode.Nodes.Add(ALevelNode)
                                End If

                                Dim BLevelDirectories As String() = Directory.GetDirectories(ALevelDirectories(AIndex))
                                For BIndex = 0 To BLevelDirectories.Length - 1
                                    temp_str(0) = stripLastPartOfDirectoryName(BLevelDirectories(BIndex).ToString)
                                    If ((String.Compare(temp_str(0), "IETM")) = 0) Then
                                        GoTo SKIPIETM
                                    End If
                                    AssemblyCount = AssemblyCount + 1
                                    Dim BLevelNode As New TreeNode()
                                    BLevelNode.Text = stripLastPartOfDirectoryName(BLevelDirectories(BIndex).ToString)

                                    Dim CLevelDirectories As String() = Directory.GetDirectories(BLevelDirectories(BIndex))
                                    For CIndex = 0 To CLevelDirectories.Length - 1
                                        Dim CLevelNode As New TreeNode()
                                        CLevelNode.Text = stripLastPartOfDirectoryName(CLevelDirectories(CIndex.ToString))
                                        BLevelNode.Nodes.Add(CLevelNode)

                                        If Directory.GetFiles(CLevelDirectories(CIndex).ToString, "*.obj").Count > 0 Then
                                            Dim DLevelFiles As String() = Directory.GetFiles(CLevelDirectories(CIndex).ToString, "*.obj")
                                            For DIndex = 0 To DLevelFiles.Length - 1
                                                TestProgramCount = TestProgramCount + 1
                                                Dim DLevelNode As New TreeNode()
                                                DLevelNode.ToolTipText = DLevelFiles(DIndex.ToString)
                                                DLevelNode.Text = stripLastPartOfDirectoryName(DLevelFiles(DIndex.ToString))
                                                DLevelNode.ImageIndex = 0
                                                DLevelNode.SelectedImageIndex = 0
                                                CLevelNode.Nodes.Add(DLevelNode)
                                            Next
                                        Else
                                            Dim DLevelFiles As String() = Directory.GetFiles(CLevelDirectories(CIndex).ToString, "*.exe")
                                            For DIndex = 0 To DLevelFiles.Length - 1
                                                Dim tmpFile As String = DLevelFiles(DIndex)
                                                While tmpFile.Contains("\")
                                                    tmpFile = tmpFile.Remove(0, (tmpFile.IndexOf("\") + 1))
                                                End While
                                                If tmpFile.ToUpper.StartsWith("TP") Then
                                                    TestProgramCount = TestProgramCount + 1
                                                    Dim DLevelNode As New TreeNode()
                                                    DLevelNode.ToolTipText = DLevelFiles(DIndex.ToString)
                                                    DLevelNode.Text = stripLastPartOfDirectoryName(DLevelFiles(DIndex.ToString))
                                                    DLevelNode.ImageIndex = 0
                                                    DLevelNode.SelectedImageIndex = 0
                                                    CLevelNode.Nodes.Add(DLevelNode)
                                                End If
                                            Next
                                        End If
                                    Next

                                    If (frmSysPanl.tabUserOptions.Visible = True) Then
                                        frmSysPanl.trvTpsList.SelectedNode = ALevelNode
                                        frmSysPanl.trvTpsList.SelectedNode.Nodes.Add(BLevelNode)

                                    End If

SKIPIETM:
                                Next
                            Next
                        Next



                        CurrentSystemDir = ReturnFirstPartOfDirectoryName(currentNodeSelected)
                        If CurrentSystemDir <> LastSystemDir Then
                            RefreshTreeView = True

                            'Update TPS info here
                            'TODO

                        End If
                        listctr = 0
                        For Each useini In iniFileList
                            entrylist = useini.Split("\")
                            If (entrylist(2) = SystemDir) Then
                                Exit For
                            End If
                            listctr += 1
                        Next
                        If listctr > iniFileList.Count() - 1 Then
                            listctr = 0
                        End If

                        'TODO Get selected Node

                        Try
                            GetPrivateProfileString("TPS", "CD_VERSION", "", lpReturnedString, 1024, iniFileList(listctr))
                        Catch
                        End Try
                        If (lpReturnedString.ToString = "") Then
                            frmSysPanl.lblTpsInformation_1.Text = "Not Available"

                        Else
                            frmSysPanl.lblTpsInformation_1.Text = lpReturnedString.ToString()
                        End If

                        'Find Media Part Number
                        Try
                            GetPrivateProfileString("TPS", "CD_PN", "", lpReturnedString, 1024, iniFileList(listctr))
                        Catch
                        End Try
                        If (lpReturnedString.ToString = "") Then
                            frmSysPanl.lblTpsInformation_2.Text = "Not Available"

                        Else
                            frmSysPanl.lblTpsInformation_2.Text = lpReturnedString.ToString()
                        End If

                        'Get Media Volume Information
                        If (TPSHD.VolumeLabel = "") Then
                            frmSysPanl.lblTpsInformation_0.Text = "[No Title]"

                        Else
                            frmSysPanl.lblTpsInformation_0.Text = d.VolumeLabel.ToString()
                        End If


                        frmSysPanl.lblTpsInformation_3.Text = SystemCount
                        frmSysPanl.lblTpsInformation_4.Text = AssemblyCount
                        frmSysPanl.lblTpsInformation_5.Text = TestProgramCount

                    End If

                    'Only Display the First Media Found
                    lastMediaPartNumber = frmSysPanl.lblTpsInformation_2.Text
                    lastMediaVolumeLabel = frmSysPanl.lblTpsInformation_0.Text

                End If
            End If
        Else
            If bDebug Then WriteDebugInfo("MAIN.CheckForTpsHd() - TPS hdd volume not found")
        End If

        If (tpsHDProgsExist = False) Then
            frmSysPanl.trvTpsList.Visible = False
            frmSysPanl.panCdInformation.Visible = False
            frmSysPanl.Button9.Visible = False
            frmSysPanl.lblProgDescription_9.Visible = False
            frmSysPanl.lblInstructions.Visible = True
        End If

        LastSystemDir = CurrentSystemDir

    End Sub

    Sub UpdateTPSInfo(indx As Short)

        Dim lpReturnedString As New StringBuilder(1024)

        'MsgBox("Index: " + indx.ToString())

        If iniFileList.Count > 0 Then


            Try
                GetPrivateProfileString("TPS", "CD_VERSION", "", lpReturnedString, 1024, iniFileList(indx))
            Catch
                'Catch all exceptions
            End Try

            If (lpReturnedString.ToString = "") Then
                frmSysPanl.lblTpsInformation_1.Text = "Not Available"
            Else
                frmSysPanl.lblTpsInformation_1.Text = lpReturnedString.ToString()
            End If

            'Find Media Part Number
            Try
                GetPrivateProfileString("TPS", "CD_PN", "", lpReturnedString, 1024, iniFileList(indx))
            Catch
                'Catch all exceptions
            End Try

            If (lpReturnedString.ToString = "") Then
                frmSysPanl.lblTpsInformation_2.Text = "Not Available"

            Else
                frmSysPanl.lblTpsInformation_2.Text = lpReturnedString.ToString()
            End If
        End If
    End Sub

    Function StringToList(ByVal strng As String, ByRef List() As String, ByVal delimiter As String) As Short
        '************************************************************
        '* ManTech Test Systems Software Module                     *
        '************************************************************
        '* Nomenclature   : VIPERT SYSTEM MENU                        *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     Procedure to convert a delimited string into a       *
        '*     list array                                           *
        '*    PARAMETERS:                                           *
        '*    strng$     : String to be converted.                  *
        '*    list$()    : Array in which to return list of strings *
        '*    Delimiter$ : Char array of valid delimiters.          *
        '*    [Returns] = Number of items in list or -1 if number   *
        '*    of number of elements exceeds upper bound of passed   *
        '*    array.                                                *
        '*    EXAMPLE:                                              *
        '*      NumElements%=StringToList(ParseStr$,ListArr$(),",") *
        '************************************************************
        
        
        
        Dim numels As Short, inflag As Short, ListClear As Integer, slength As Integer, ch As Integer
        Dim mChar As String

        numels = 0
        inflag = 0

        'Clear Passed Array
        For ListClear = 0 To UBound(List)
            List(ListClear) = ""
        Next ListClear

        'Go through parsed string a character at a time.
        slength = Convert.ToString(strng).Length
        For ch = 1 To slength
            mChar = Mid(Convert.ToString(strng), ch, 1)
            'Test for delimiter
            If InStr(Convert.ToString(delimiter), mChar) = 0 Then
                If Not inflag Then
                    'Test for too many arguments.
                    If numels = UBound(List) Then
                        StringToList = -1
                        Exit For
                    End If
                    numels += 1
                    inflag = -1
                End If
                'Add the character to the current argument.
                List(numels) += mChar
            Else
                'Found a delimiter.
                'Set "Not in element" flag to FALSE.
                inflag = 0
            End If
        Next ch
        StringToList = numels

    End Function



    Public Function DisplayPLC() As Boolean
        '*****************************************************************************
        '***      Checks the VIPERT.ini file to determine if RF is installed.        ***
        '***      Returns either True or False to display the PLC button only      ***
        '***      if the system is has RF Instrumentation.                         ***
        '***                                        Dave Joiner  01/18/2002        ***
        '*****************************************************************************

        Dim sRFInstalled As String 'Contents of the RF_OPTION_INSTALLED key

        sRFInstalled = GatherIniFileInformation("System Startup", "RF_OPTION_INSTALLED", "")

        If UCase(sRFInstalled) = "YES" Then
            DisplayPLC = True
        Else

            DisplayPLC = False
        End If

    End Function
    Public Sub UpdateIniFile(ByVal AppName As String, ByVal Key As String, ByVal IniInfo As String)
        '**************************************************************************************
        '***    Revised function to allow AppName, Key, and IniInfo to be passed.           ***
        '***    If IniInfo is an empty string "", then the Time Now is Updated              ***
        '***    in the Location specified.                                                  ***
        '**************************************************************************************


        Dim ReturnValue As Integer 'API Returned Eror Code
        Dim lpFileName As String 'File to write to (VIPERT.INI)
        Dim lpKeyName As String 'KEY= in INI file
        Dim lpDefault As String 'Default value if key not found
        Dim lpApplicationName As String '[Application] in INI file

        Dim DateSerVal As Single 'Date Serial Code

        Dim TimeSerVal As Single 'Time Serial Code

        Dim DisplaySerVal As Double

        lpFileName = Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\ATS\", "IniFilePath", Nothing)

        lpApplicationName = AppName
        lpKeyName = Key

        If IniInfo = "" Then
            'Init File
            DateSerVal = DateSerial(Year(Now), Month(Now), DateAndTime.Day(Now)).ToOADate
            TimeSerVal = TimeSerial(Hour(Now), Minute(Now), Second(Now)).ToOADate
            DisplaySerVal = DateSerVal + TimeSerVal
            lpDefault = VB6.Format(DisplaySerVal, "0.00000")
        Else
            lpDefault = IniInfo
        End If

        ReturnValue = WritePrivateProfileString(lpApplicationName, lpKeyName, lpDefault, lpFileName)

    End Sub

    Sub MoveSysCal()
        '*****************************************************************************
        '***      Procedure to move the [System Calibration] button to the [PLC]   ***
        '***      position in the event the system is not RF and the DOD_Admin     ***
        '***      or Maintainer has logged in. This provides for a gap free menu.  ***
        '***                                        Dave Joiner  01/23/2002        ***
        '*****************************************************************************

        If LoginAccess(SYSTEM_CALIBRATION) = True And LoginAccess(STOW) = False Then

            With frmSysPanl
                .ribProgram(SYSTEM_CALIBRATION).Left = 334
                .ribProgram(SYSTEM_CALIBRATION).Top = 146
                .lblProgDescription(SYSTEM_CALIBRATION).Left = 386
                .lblProgDescription(SYSTEM_CALIBRATION).Top = 158
            End With

        End If


    End Sub

    Public Function bTPSExecFile(ByVal sFilename As String) As Boolean
        '************************************************************
        '* ManTech Test Systems Software Module                     *
        '************************************************************
        '* Nomenclature   : VIPERT SYSTEM MENU                        *
        '* Written By     : Nikolai Sazonov                         *
        '*    DESCRIPTION:                                          *
        '*     Function returns TRUE if sFilename ends on '.exe'    *
        '*      and begins with 'TP'                                *
        '*    PARAMETERS:                                           *
        '*    sFilename: a string that contains the filename to     *
        '*      to be analyzed                                      *
        '************************************************************
        bTPSExecFile = False
        'Check File Name for at least 4 characters before checking for ".EXE"
        If Len(sFilename) > 4 Then
            If UCase(Mid(sFilename, Len(sFilename) - 3, 4)) = ".EXE" And UCase(Mid(sFilename, 1, 2)) = "TP" Then
                bTPSExecFile = True
            End If
        End If

    End Function

    Public Function bTPSExecPath(ByVal sFullPath As String) As Boolean
        '************************************************************
        '* ManTech Test Systems Software Module                     *
        '************************************************************
        '* Nomenclature   : VIPERT SYSTEM MENU                        *
        '* Written By     : Nikolai Sazonov                         *
        '*    DESCRIPTION:                                          *
        '*     Function returns TRUE if sFullExecPath is a path to a*
        '*      file that ends on '.EXE' and begins with 'TP'       *
        '*    PARAMETERS:                                           *
        '*    sFullExecPath: a string that contains the path to be  *
        '*      analyzed                                            *
        '************************************************************
        
        Dim i As Integer 'loop counter

        bTPSExecPath = False

        'the loop scans sFullPath from the last character back to the first
        'until it finds "\" character. Everything to the right of that
        'character is the filename that will be checked to end on '.EXE' and
        ' and to begin with 'TP'. If the filename matches those two conditions,
        'bTPSExecPath is set to TRUE, otherwise to FALSE. The loop counter
        ' is set to the lowest value to exit the loop.
        For i = Len(sFullPath) To 1 Step -1
            If UCase(Mid(sFullPath, i, 1)) = "\" Then
                If (UCase(Mid(sFullPath, Len(sFullPath) - 3, 4)) = ".EXE") And (UCase(Mid(sFullPath, i + 1, 2)) = "TP") Then
                    bTPSExecPath = True
                Else
                    bTPSExecPath = False
                End If
                i = 1 ' to exit the loop
            End If
        Next i
    End Function

    Function ReturnFirstPartOfDirectoryName(ByVal directoryName As String) As String

        Dim dirs() As String
        dirs = directoryName.Split("\")
        Return dirs(0)

    End Function
    Function stripLastPartOfDirectoryName(ByVal directoryName As String) As String

        Dim index As Integer = 0
        index = directoryName.LastIndexOf("\")

        Return directoryName.Substring(index + 1)

    End Function

    Public Const _tvwFirst As Integer = 0
    Public Const _tvwLast As Integer = 1
    Public Const _tvwNext As Integer = 2
    Public Const _tvwPrevious As Integer = 3
    Public Const _tvwChild As Integer = 4

    Public Function InsertNode(ByVal RelativeNode As TreeNode, ByVal Relationship As Integer, Optional ByVal Key As String = "", Optional ByVal Text As String = "", Optional ByVal imageIndex As Integer = -1, Optional ByVal selectedImageIndex As Integer = -1) As TreeNode
        If Relationship = _tvwChild Then Return RelativeNode.Nodes.Add(Key, Text)

        Dim pNodes As TreeNodeCollection
        If RelativeNode.Parent Is Nothing Then
            pNodes = RelativeNode.TreeView.Nodes
        Else
            pNodes = RelativeNode.Parent.Nodes
        End If

        Dim index As Integer = 0
        Select Case Relationship
            'Case _tvwFirst : index = 0
            Case _tvwLast : index = pNodes.Count
            Case _tvwPrevious : index = pNodes.IndexOf(RelativeNode)
            Case _tvwNext : index = pNodes.IndexOf(RelativeNode) + 1
        End Select

        Return pNodes.Insert(index, Key, Text, imageIndex, selectedImageIndex)

    End Function

    Public Function InsertNode(ByRef Nodes As TreeNodeCollection, ByVal RelativeKey As String, ByVal Relationship As Integer, Optional ByVal sKey As String = "", Optional ByVal sText As String = "", Optional ByVal imageIndex As Integer = -1, Optional ByVal selectedImageIndex As Integer = -1) As TreeNode
        Dim RelativeNodes() As TreeNode = Nodes.Find(RelativeKey, True)
        If RelativeNodes.Length = 0 Then Return Nothing
        Dim RelativeNode As TreeNode = RelativeNodes(0)
        Return InsertNode(RelativeNode, Relationship, sKey, sText, imageIndex, selectedImageIndex)
    End Function
    Public Function InsertNode(ByRef Nodes As TreeNodeCollection, ByVal RelativeTotalVB6Index As Integer, ByVal Relationship As Integer, Optional ByVal sKey As String = "", Optional ByVal sText As String = "", Optional ByVal imageIndex As Integer = -1, Optional ByVal selectedImageIndex As Integer = -1) As TreeNode
        Dim RelativeNode As TreeNode = Nodes(RelativeTotalVB6Index - 1)
        Return InsertNode(RelativeNode, Relationship, sKey, sText, imageIndex, selectedImageIndex)
    End Function


   
    Public ReadOnly Property AppPrevInstance() As Boolean
        Get
            AppPrevInstance = False
        End Get
    End Property

    Friend pDAODBEngine As New DAO.DBEngine

    Public Function CompareErrNumber(ByVal sConditon As String, ByVal Value As Integer) As Boolean
        Dim te As Integer = Err.Number
        ' te = xFun(te)

        Select Case sConditon
            Case "<=" : Return te <= Value
            Case ">=" : Return te >= Value
            Case "<>" : Return te <> Value
            Case "=" : Return te = Value
            Case ">" : Return te > Value
            Case "<" : Return te < Value
            Case Else
                Return False    ' - ???
        End Select

    End Function

    Public Sub ResumeNext(Optional ByVal sFunction As String = "")
        ' ...
    End Sub
    Public Function CountCharacter(ByVal value As String, ByVal ch As Char) As Integer
        Dim cnt As Integer = 0
        For i As Integer = 0 To value.Length - 1
            If value(i) = ch Then
                cnt += 1
            End If
        Next
        Return cnt
    End Function
     Sub DisplaySetup(ByVal Caption As String, ByVal Graphic As String, ByVal LineCount As Integer, Optional ByVal BackOK As Integer = False, Optional ByVal BackNo As Integer = 0, Optional ByVal BackCount As Integer = 0)

        'DESCRIPTION
        '   This routine displays frmDirections, displays a caption on it to explain
        '   to the operator what manual intervention to perform, and loads the
        '   graphic file associated with this action
        'PARAMETERS
        '   Caption  = This is the caption to be displayed on the form
        '   Graphic  = This is the file name of the graphic to be displayed (426 x 222 pixels)

        frmSysPanl.WindowState = FormWindowState.Normal
        GoBack = False

        Application.DoEvents()
        If LineCount < 2 Then
            frmDirections.txtDirections.Text = "Perform the following operation:  " & vbCrLf & vbCrLf & " " & Caption
        Else
            frmDirections.txtDirections.Text = "Perform the following " & CStr(LineCount) & " operations:  " & vbCrLf & vbCrLf & Caption
        End If

        frmDirections.cmdBack.Visible = False
        frmDirections.Text = "Setup Instructions "
        If BackOK = True Then
            If BackNo > 1 Then
                frmDirections.cmdBack.Visible = True
            End If
            frmDirections.Text = "Setup Instructions (" & CStr(BackNo) & " of " & CStr(BackCount) & ")"
        End If

        ProgramPath = Application.StartupPath

        Try
            frmDirections.picGraphic.Image = LoadPicture(ProgramPath & "\GRAPHICS\" & Graphic)
        Catch ex As Exception
            frmDirections.picGraphic.Image = LoadPicture(ProgramPath & "\GRAPHICS\APN_PCU.jpg")
        End Try

        frmDirections.TopMost = True
        frmDirections.ShowDialog()


    End Sub

    Public Function LoadPicture(ByVal file_name As String) As Image
        If file_name Is Nothing Or file_name.Length = 0 Then Return New Bitmap(1, 1)
        Dim src As New Bitmap(file_name)
        Dim dst As New Bitmap(src.Width, src.Height, Imaging.PixelFormat.Format24bppRgb)
        Graphics.FromImage(dst).DrawImage(src, 0, 0, src.Width, src.Height)
        src.Dispose()
        Return dst
    End Function

    Public Function IsAdmin(userName As String) As Boolean

        Const GROUP_ADMIN = "administrators"

        Using context As System.DirectoryServices.AccountManagement.PrincipalContext = New System.DirectoryServices.AccountManagement.PrincipalContext(DirectoryServices.AccountManagement.ContextType.Machine)
            Dim groupPrincipal As System.DirectoryServices.AccountManagement.GroupPrincipal = System.DirectoryServices.AccountManagement.GroupPrincipal.FindByIdentity(context, GROUP_ADMIN)
            If groupPrincipal IsNot Nothing Then
                Dim members = groupPrincipal.GetMembers
                For Each member As System.DirectoryServices.AccountManagement.Principal In members
                    If String.Compare(member.Name, userName, True) = 0 Then
                        IsAdmin = True
                        Exit Function
                    End If
                Next
            End If
        End Using

        IsAdmin = False

    End Function

    Public Function IsElevated(userName As String) As Boolean

        Const GROUP_ADMIN = "administrators"
        Const GROUP_POWER = "power users"

        Using context As System.DirectoryServices.AccountManagement.PrincipalContext = New System.DirectoryServices.AccountManagement.PrincipalContext(DirectoryServices.AccountManagement.ContextType.Machine)
            Dim groupPrincipal As System.DirectoryServices.AccountManagement.GroupPrincipal = System.DirectoryServices.AccountManagement.GroupPrincipal.FindByIdentity(context, GROUP_ADMIN)
            If groupPrincipal IsNot Nothing Then
                Dim members = groupPrincipal.GetMembers
                For Each member As System.DirectoryServices.AccountManagement.Principal In members
                    If String.Compare(member.Name, userName, True) = 0 Then
                        IsElevated = True
                        Exit Function
                    End If
                Next
            End If

            groupPrincipal = System.DirectoryServices.AccountManagement.GroupPrincipal.FindByIdentity(context, GROUP_POWER)

            If groupPrincipal IsNot Nothing Then
                Dim members = groupPrincipal.GetMembers
                For Each member As System.DirectoryServices.AccountManagement.Principal In members
                    If String.Compare(member.Name, userName, True) = 0 Then
                        IsElevated = True
                        Exit Function
                    End If
                Next
            End If

        End Using

        IsElevated = False

    End Function

End Module