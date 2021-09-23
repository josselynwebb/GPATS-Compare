Option Strict Off
Option Explicit On

Imports VB = Microsoft.VisualBasic
Imports Microsoft.Win32
Imports FHDB

Module MAINMod
    '***************************************************************************
    '*                                                                         *
    '*            MODULE NAME:  MAIN.BAS                                       *
    '*                PURPOSE:  Firefinder TPS Shell Program (VTS-1000)        *
    '*                                                                         *
    '*  DEVELOPED FOR TETS BY:  ManTech Test Systems, Inc.                     *
    '*                          Chantilly, Virginia                            *
    '*                                                                         *
    '*  MODIFIED FOR VIPER BY:  DME, Inc.                                      *
    '*                          Orlando, Florida                               *
    '*                                                                         *
    '*          DEVELOPED FOR:  USMC, MCLB, Albany, Georgia                    *
    '*                                                                         *
    '*                                                                         *
    '***************************************************************************
    '***************************************************************************
    '*                          RECORD OF REVISIONS                            *
    '***************************************************************************
    '* REV. *  DATE    *  PROGRAMMED BY:     *         REASON                  *
    '***************************************************************************
    '* 1.0  * 09/03/02 * T. BIGGS/G. JOHNSON * REVISED SHELLV02 VERSION 2.1    *
    '* 2.0  * 04/01/12 * B. Bamford          * REVISED FOR VIPERT              *
    '*      -  Added CICL                                                      *
    '*      -  Changed DSO Instrument                                          *
    '*      -  Changed RF Instruments                                          *
    '*                                                                         *
    '*                                                                         *
    '*                                                                         *
    '*                                                                         *
    '*                                                                         *
    '*                                                                         *
    '***************************************************************************
    '*      *          *                     *                                 *
    '***************************************************************************
    Public Declare Function FindExecutable Lib "shell32.dll" Alias "FindExecutableA" (ByVal lpFile As String, ByVal lpDirectory As String, ByVal lpResult As String) As Integer
    Public Declare Function ShellExecute Lib "shell32.dll" Alias "ShellExecuteA" (ByVal hwnd As Integer, ByVal lpOperation As String, ByVal lpFile As String, ByVal lpParameters As String, ByVal lpDirectory As String, ByVal nShowCmd As Integer) As Integer
    Declare Function GetPrivateProfileString Lib "kernel32" Alias "GetPrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpDefault As String, ByVal lpReturnedString As String, ByVal nSize As Integer, ByVal lpFileName As String) As Integer
    Declare Function WritePrivateProfileString Lib "kernel32" Alias "WritePrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpString As String, ByVal lpFileName As String) As Integer
    Declare Function DM_setMaxSeedingFaultSetsValue Lib "DiagMgr.dll" (ByVal value As Integer) As Integer

    'CDROM Search Functions
    Declare Function GetLogicalDriveStrings Lib "kernel32" Alias "GetLogicalDriveStringsA" (ByVal nBufferLength As Integer, ByVal lpBuffer As String) As Integer
    Declare Function GetDriveType Lib "kernel32" Alias "GetDriveTypeA" (ByVal nDrive As String) As Integer
    Declare Function GetVolumeInformation Lib "kernel32" Alias "GetVolumeInformationA" (ByVal lpRootPathName As String, ByVal lpVolumeNameBuffer As String, ByVal nVolumeNameSize As Integer, ByRef lpVolumeSerialNumber As Integer, ByRef lpMaximumComponentLength As Integer, ByRef lpFileSystemFlags As Integer, ByVal lpFileSystemNameBuffer As String, ByVal nFileSystemNameSize As Integer) As Integer
    Public Const DRIVE_CDROM As Short = 5
    Public Const DRIVE_FIXED As Short = 3
    Public Const DRIVE_RAMDISK As Short = 6
    Public Const DRIVE_REMOTE As Short = 4
    Public Const DRIVE_REMOVABLE As Short = 2

    'Setup Createprocess function
    Public Structure STARTUPINFO
        Dim cb As Integer
        Dim lpReserved As String
        Dim lpDeskTop As String
        Dim lpTitle As String
        Dim dwX As Integer
        Dim dwY As Integer
        Dim dwXSize As Integer
        Dim dwYSize As Integer
        Dim dwXCountChars As Integer
        Dim dwYCountChars As Integer
        Dim dwFillAttribute As Integer
        Dim dwFlags As Integer
        Dim wShowWindow As Short
        Dim cbReserved2 As Short
        Dim lpReserved2 As Integer
        Dim hStdInput As Integer
        Dim hStdOutput As Integer
        Dim hStdError As Integer
    End Structure

    Public Structure PROCESS_INFORMATION
        Dim hProcess As Integer
        Dim hThread As Integer
        Dim dwProcessID As Integer
        Dim dwThreadID As Integer
    End Structure

    Public Structure SECURITY_ATTRIBUTES
        Dim nLength As Integer
        Dim lpSecurityDescriptor As Integer
        Dim bInheritHandle As Boolean
    End Structure

    Public Declare Function CreateProcess Lib "kernel32" Alias "CreateProcessA" (ByVal lpApplicationName As String, ByVal lpCommandLine As String, ByRef lpProcessAttributes As SECURITY_ATTRIBUTES, ByRef lpThreadAttributes As SECURITY_ATTRIBUTES, ByVal bInheritHandles As Integer, ByVal dwCreationFlags As Integer, ByRef lpEnvironment As Integer, ByVal lpCurrentDriectory As String, ByRef lpStartupInfo As STARTUPINFO, ByRef lpProcessInformation As PROCESS_INFORMATION) As Integer
    Public Declare Function CreateProcessA Lib "kernel32" (ByVal lpApplicationName As Integer, ByVal lpCommandLine As String, ByVal lpProcessAttributes As Integer, ByVal lpThreadAttributes As Integer, ByVal bInheritHandles As Integer, ByVal dwCreationFlags As Integer, ByVal lpEnvironment As Integer, ByVal lpCurrentDirectory As Integer, ByRef lpStartupInfo As STARTUPINFO, ByRef lpProcessInformation As PROCESS_INFORMATION) As Integer
    Public Declare Function WaitForSingleObject Lib "kernel32" (ByVal hHandle As Integer, ByVal dwMilliseconds As Integer) As Integer
    Public Declare Function CloseHandle Lib "kernel32" (ByVal hObject As Integer) As Integer

    Public Const SW_HIDE As Short = 0
    Public Const SW_SHOWNORMAL As Short = 1
    Public Const SW_NORMAL As Short = 1
    Public Const SW_SHOWMINIMIZED As Short = 2
    Public Const SW_SHOWMAXIMIZED As Short = 3
    Public Const SW_MAXIMIZE As Short = 3
    Public Const SW_SHOWNOACTIVATE As Short = 4
    Public Const SW_SHOW As Short = 5
    Public Const SW_MINIMIZE As Short = 6
    Public Const SW_SHOWMINNOACTIVE As Short = 7
    Public Const SW_SHOWNA As Short = 8
    Public Const SW_RESTORE As Short = 9
    Public Const SW_SHOWDEFAULT As Short = 10
    Public Const SW_FORCEMINIMIZE As Short = 11
    Public Const SW_MAX As Short = 11

    Public Const STARTF_USESHOWWINDOW As Integer = &H1
    Public Const INFINITE As Short = -1
    Public Const NORMAL_PRIORITY_CLASS As Integer = &H20
    Public Const DIAGNOSTICS_DIRECTORY As String = "C:\APS\DATA"

    'AProbe Use and Shut Down because of handle or green button
    Declare Function viReadSTB Lib "VISA32.DLL" Alias "#259" (ByVal vi As Integer, ByRef status As Short) As Integer
    Dim ActChassisAddress As Short
    Public ActVolt28Ok As Short
    Dim ActProbeEvent As Short
    Dim ActReceiverEvent As Short

    Declare Function GetWindow Lib "user32" (ByVal hwnd As Integer, ByVal wCmd As Integer) As Integer
    Declare Function GetWindowText Lib "user32" Alias "GetWindowTextA" (ByVal hwnd As Integer, ByVal lpString As String, ByVal cch As Integer) As Integer
    Public Const GW_HWNDFIRST As Short = 0 'API Constant for Changing a Window "Z-Order"
    Public Const GW_HWNDLAST As Short = 1 'API Constant for Changing a Window "Z-Order"
    Public Const GW_HWNDNEXT As Short = 2 'API Constant for Changing a Window "Z-Order"

    Public bReceiverClosed As Boolean
    Public bProbeClosed As Boolean
    Public bReseting As Boolean

    ' *** GLOBAL CONSTANTS ***
    Public FormMainLoaded As Short

    Public Const MODAL As Short = 1
    Public Const PANNING As Short = 0
    Public Const ZOOMING As Short = 1
    Public Const MAGNIFYING As Short = 2
    Public Const SCROLLING As Short = 3
    Public Const MAX_MENU_OPT As Short = 11
    Public Const NORMAL As Short = 0 ' 0 - Normal
    Public Const MINIMIZED As Short = 1 ' 1 - Minimized
    Public Const MAXIMIZED As Short = 2 ' 2 - Maximized


    Public Const RUNTP_BUTTON As Short = -1 '6000-1 = 5999
    Public Const VIEW_SCHEMATIC As Short = -2 '6000-2 = 5998
    Public Const VIEW_ASSEMBLY As Short = -3 '6000-3 = 5997
    Public Const VIEW_PARTSLIST As Short = -4 '6000-4 = 5996
    Public Const ID_SURVEY As Short = -5 '6000-5 = 5995
    Public Const TPS_DOCUMENTATION As Short = -6 '6000-6 = 5994
    Public Const VIEW_ID_SCHEMATIC As Short = -7 '6000-7 = 5993
    Public Const VIEW_ID_ASSEMBLY As Short = -8 '6000-8 = 5992
    Public Const VIEW_ID_PARTSLIST As Short = -9 '6000-9 = 5991
    Public Const VIEW_TSR As Short = -10 '6000-10 = 5990
    Public Const VIEW_ELTD As Short = -11 '6000-11 = 5989
    Public Const VIEW_GENERAL_INFO As Short = -12 '6000-12 = 5988
    Public Const VIEW_FAULT_FILE As Short = -13 '6000-13 = 5987
    Public Const VIEW_IETM As Short = -14 '6000-14 = 5986

    Public Const BACKUP_BUTTON As Short = -14 '6000-14 = 5986
    Public Const RERUN_BUTTON As Short = -15 '6000-15 = 5985
    Public Const MAINMENU_BUTTON As Short = -16 '6000-16 = 5984
    Public Const ABORT_BUTTON As Short = -17 '6000-17 = 5983
    Public Const HELP_BUTTON As Short = -18 '6000-18 = 5982
    Public Const END_TO_END As Short = -19 '6000-19 = 5981
    Public Const STTO As Short = -20 '6000-20 = 5980
    Public Const PWR_ON As Short = -21 '6000-21 = 5979
    Public Const QUIT_BUTTON As Short = -22 '6000-22 = 5978
    Public Const CONTINUE_BUTTON As Short = -23 '6000-23 = 5977
    Public Const YES_BUTTON As Short = -24 '6000-24 = 5976
    Public Const NO_BUTTON As Short = -25 '6000-25 = 5975
    Public Const DIAGNOSTIC_BUTTON As Short = -26 '6000-26 = 5974
    Public Const USER_EVENT As Short = 6000

    Public Const DATA_BASE_NAME As String = "DATALOG.MDB"
    Public Const DATA_BASE_TABLE As String = "LOGTBL"

    Public Const TEST_PROGRAM As String = "TestProgram"
    Public Const SYSTEM_LOG As String = "syslog.txt"
    'Global Const TETS_INI = "tets.ini"

    ' *** LOCAL CONSTANTS ***
    Const CCA_NOMENCLATURE As String = "CCA Nomenclature"
    Const CCA_PART_NUMBER As String = "CCA Part Number"
    Const CCA_SERIAL_NUMBER As String = "CCA Serial Number"
    Const CCA_STATUS As String = "CCA Status"
    Const CALL_OUT As String = "Call Out"
    Const CURR_DATE As String = "Date"
    Const TEST_FAILED As String = "Test Failed"
    Const OPERATOR_NAME As String = "Operator ID"
    Const TESTER_NUMBER As String = "Tester Serial Number"
    Const TEST_NUMBER_FAILED As String = "Test Number Failed"
    Const CURR_TIME As String = "Time"

    ' *** GLOBAL VARIABLES ***
    Public iTime As Short
    Public bFirstRun As Boolean
    Public bEndToEnd As Boolean
    Public bHighVoltage As Boolean
    Public TPSAppExe As String
    Public DDESendErr As Short
    Public QuitButtonPressed As Short
    Public ImageHandle As Integer
    Public Choice As Short
    Public MouseAction As Short
    Public MagCursor As Short
    Public ReturnCode As Integer
    Public WindowCaption As String
    Public HelpKey As String
    Public Buttons() As String
    Public LOGFILE As Integer
    Public LogPath As String
    Public Logging As Short
    Public ConfigOK As Short
    Public UserEvent As Short
    Public TheMessage As String
    Public TestLog As String
    Public UserInputData As String
    Public InputNumber As Short
    Public iAcknowledge As Short
    Public bModuleMenuBuilt As Boolean
    Public Pass As Boolean
    Public Failed As Boolean
    Public OutHigh As Boolean
    Public OutLow As Boolean
    Public MisProbe As Short
    Public nNumberOfChannelPins As Integer
    Public SOF As Boolean

    Public DataLoggingEnabled As Short
    Public SysLogDB As String
    Public SysLogTbl As String

    Public MaxPartsListPages As Short
    Public MaxIDPartsListPages As Short
    Public MaxAssemblyPages As Short
    Public MaxIDAssemblyPages As Short
    Public MaxSchematicPages As Short
    Public MaxIDSchematicPages As Short
    Public MaxModules As Short

    Public TestProgramName As String
    Public ProgramPath As String
    Public TPSGraphics As String
    Public SchemPageNum As Short
    Public IDSchemPageNum As Short
    Public ProbeDataFile As String
    Public ScreenBuffer(21) As String
    Public Const MaxScreenLines As Short = 21
    Public ProbeReady As Short
    Public ImageViewerChoice As Boolean
    Public TPName As String
    Public TPVersion As String
    Public TPNotice As String
    Public UUTPartNo As String
    Public UUTName As String
    Public UUTSN As String
    Public UUTRev As String
    Public IDPartNo As String
    Public IDSN As String
    Public TPType As String


    Public LEDButtonPressed As String
    Public LEDUserResponse As Boolean

    'Added 4/5/00 SJM
    Public tempmsg As String

    'Added to new shell - GJohnson 1/10/02
    Public sTSRFileName As String
    Public sELTDFileName As String
    Public sInfoFileName As String

    Public ModuleNames() As String
    Public ModuleRunTime() As Integer
    Public DocumentsPath As String
    Public sPartsListFileNames() As String
    Public sIDPartsListFileNames() As String
    Public MaxPartListPages As Short
    Public PartListPageNum As Short
    Public IDPartListPageNum As Short
    Public PartListPath As String
    Public IDPartListPath As String
    Public sSchemFileNames() As String
    Public sIDSchemFileNames() As String
    Public SchemPath As String
    Public IDSchemPath As String
    Public sAssyFileNames() As String
    Public sIDAssyFileNames() As String
    Public MaxAssyPages As Short
    Public AssyPageNum As Short
    Public IDAssyPageNum As Short
    Public AssyPath As String
    Public IETMPath As String
    Public IDAssyPath As String
    Public sImageFile As String
    Public sProbeAssy As String
    Public sProbeData As String
    Public nFileError As Integer
    Public SystemDir As String
    Public VisaLibrary As String
    Public lpBuffer As String = Space(256)
    Public PathTETSIni As String
    Public PathSysLogExe As String
    Public sDelimiter As String
    Public NumElements As Short
    Public Button As Short
    Public bSystemInitialized As Boolean
    Public bTPSDevelopment As Boolean
    Public bSystemResetComplete As Boolean

    '*************************************************
    'bbbb added for ViperT Apr, 2011
    Public ReadBuffer As String = Space(256)
    Public nSessionHandle As Integer

    Public WeaponSystem As String
    Public WeaponSystemNomenclature As String
    Public DevelopedBy As String
    Public address As String

    'Define Data variables to pass to the FHDB
    Public Structure CurrentFailureRecord
        Dim sSubName As String
        Dim sTestNumber As String
        Dim sPath As String
        Dim sHighLimit As String
        Dim sLowLimit As String
        Dim sMeasured As String
        Dim sUnit As String
    End Structure

    Public Structure FHDBRecord
        Dim sStart As String
        Dim SStop As String
        Dim EROs As String
        Dim sTPCCN As String
        Dim sUUTSerial As String
        Dim sUUTRev As String
        Dim sIDSerial As String
        Dim nStatus As Integer
        Dim sFailStep As String
        Dim sCallout As String
        Dim dMeasurement As Double
        Dim sUOM As String
        Dim duLimit As Double
        Dim dlLimit As Double
        Dim sComment As String
    End Structure

    Public CurrentFailure As CurrentFailureRecord
    Public TestData As FHDBRecord
    Public FHDB As New DLLClass
    Public objData As New DLLClass

    'cicl stuff
    Public LiveMode(40) As Short
    Public ResourceName(40) As String
    Public PsResourceName(10) As String
    Public nSystErr As Integer

    'RF stuff
    Public s8481A_SN As String 'Serial Number of system HP8481A power sensor
    Public s8481D_SN As String 'Serial Number of system HP8481D power sensor
    Public s11708A_SN As String 'Serial Number of system 30dB attenuator

    ' From objects
    Public gFrmExit As New ExitForm()
    Public gFrmDisplayLED As New frmDisplayLED()
    Public gFrmFHDBComment As New frmFHDBComment()
    Public gFrmHV As New frmHV()
    Public gFrmImage As New frmImage()
    Public gFrmMain As New frmMain()
    Public gFrmOperatorComment As New frmOperatorComment()
    Public gFrmOperatorMsg As New frmOperatorMsg()
    Public gFrmSplash As New frmSplash()

    '**************************************************************

    Public Sub Main()
        Err.Clear()
        UserEvent = 0
        bModuleMenuBuilt = False
        gFrmMain.MenuOption(5).Image = gFrmMain.ImageList1.Images(1)
        bSystemInitialized = False
        bHighVoltage = False
        nInstrumentHandle(DIGITAL) = 0
        TestData.sIDSerial = ""
        sTSRFileName = ""
        sELTDFileName = ""
        sInfoFileName = ""
        bReseting = False

        TETS_ini = Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\ATS\", "IniFilePath", Nothing)

        If IO.Directory.Exists(LOG_FILE.Substring(0, LOG_FILE.Length - LOG_FILE.LastIndexOf("\"))) Then
            If IO.File.Exists(LOG_FILE) Then
                IO.File.Delete(LOG_FILE)
            End If
        Else
            IO.Directory.CreateDirectory(LOG_FILE.Substring(0, LOG_FILE.Length - LOG_FILE.LastIndexOf("\")).ToLowerInvariant())
        End If
        Logging = True

        Pass = True
        Failed = True
        OutHigh = True
        OutLow = True
        bEndToEnd = False

        Call InitVar()
        Call InitCicl()
    End Sub

    Sub InitVar()
        'bbbb Added to Support CICL
        SOF = False
        ResourceName(PPU) = "DCP_1"
        ResourceName(MIL_STD_1553) = "MIL1553_1"
        ResourceName(DMM) = "DMM_1"
        ResourceName(COUNTER) = "CNTR_1"
        ResourceName(OSCOPE) = "DSO_1"
        ResourceName(FGEN) = "FUNC_GEN_1"
        ResourceName(ARB) = "ARB_GEN_1"
        ResourceName(APROBE) = "PROBE"
        ResourceName(DIGITAL) = "DWG_1"
        ResourceName(SWITCH1) = "PAWS_SWITCH"
        ResourceName(SWITCH2) = "PAWS_SWITCH"
        ResourceName(SWITCH3) = "PAWS_SWITCH"
        ResourceName(SWITCH4) = "PAWS_SWITCH"
        ResourceName(SWITCH5) = "PAWS_SWITCH"
        ResourceName(SYN_RES) = "SRS_1_DS1"
        ResourceName(RFSYN) = "RFGEN_1"
        ResourceName(RFMEAS) = "RF_MEASAN_1"
        ResourceName(RFPM) = "RF_PWR_1"
        ResourceName(RS422) = "COM_1"
        ResourceName(RS232) = "COM_2"
        ResourceName(GIGA) = "ETHERNET_1"

        PsResourceName(1) = "DCPS_1"
        PsResourceName(2) = "DCPS_2"
        PsResourceName(3) = "DCPS_3"
        PsResourceName(4) = "DCPS_4"
        PsResourceName(5) = "DCPS_5"
        PsResourceName(6) = "DCPS_6"
        PsResourceName(7) = "DCPS_7"
        PsResourceName(8) = "DCPS_8"
        PsResourceName(9) = "DCPS_9"
        PsResourceName(10) = "DCPS_10"
    End Sub

    Sub InitCicl()
        Dim lpBuffer As String = Space(80)
        'bbbb, new cicl stuff, validate each instrument
        Dim i As Short
        Dim status As Integer
        Dim XmlBuf As String
        Dim Allocation As String
        Dim Response As String
        Dim Ret As Integer

        'atxml stuff, Verify presence of system
        SystemDir = Environment.SystemDirectory

        If Right(SystemDir, 1) <> "\" Then
            SystemDir = SystemDir & "\"
        End If
        VisaLibrary = SystemDir & "\VISA32.DLL"

        If Not IO.File.Exists(VisaLibrary) Then
            Echo("Cannot find VISA Run-Time System. Unable to perform System Self Test.")
        Else
            On Error Resume Next
            status = atxml_Initialize(proctype, guid)
            Allocation = "PawsAllocation.xml"

            'Determine if the ARB is functioning
            Response = Space(4096)
            XmlBuf = "<AtXmlTestRequirements>" & "<ResourceRequirement>" & "  <ResourceType>Source</ResourceType>" & "  <SignalResourceName>ARB_GEN_1</SignalResourceName> " & "</ResourceRequirement> " & "</AtXmlTestRequirements>"
            status = atxml_ValidateRequirements(XmlBuf, Allocation, Response, 4096)
            If status <> 0 Then
                MsgBox("The ARB is not responding.  Live mode is disabled.", MsgBoxStyle.Exclamation)
                LiveMode(ARB) = False
                i = MsgBox("The CiclKernelC.exe may not be running!" & vbCrLf & "Do you want to quit?", MsgBoxStyle.YesNo)
                If i = 6 Then
                    UserEvent = QUIT_BUTTON
                    '   Call EndProgram
                    Exit Sub
                End If
            Else
                LiveMode(ARB) = True
            End If


            ' Determine if the C/T is functioning
            Response = Space(4096)
            XmlBuf = "<AtXmlTestRequirements>" & "<ResourceRequirement>" & "   <ResourceType>Source</ResourceType>" & "   <SignalResourceName>CNTR_1</SignalResourceName> " & "</ResourceRequirement> " & "</AtXmlTestRequirements>" '
            status = atxml_ValidateRequirements(XmlBuf, Allocation, Response, 4096)
            If status <> 0 Then
                LiveMode(COUNTER) = False
                MsgBox("The Counter/Timer Is Not Responding.  Live Mode Disabled.", MsgBoxStyle.Information)
            Else
                LiveMode(COUNTER) = True
            End If


            'Determine if the DMM is functioning
            Response = Space(4096)
            XmlBuf = "<AtXmlTestRequirements>" & "<ResourceRequirement>" & "   <ResourceType>Source</ResourceType>" & "   <SignalResourceName>DMM_1</SignalResourceName> " & "</ResourceRequirement> " & "</AtXmlTestRequirements>" '
            status = atxml_ValidateRequirements(XmlBuf, Allocation, Response, 4096)
            If status <> 0 Then
                LiveMode(DMM) = False
                MsgBox("The DMM Is Not Responding.  Live Mode Disabled.", MsgBoxStyle.Information)
            Else
                LiveMode(DMM) = True
            End If

            'Determine If The DSCOPE Is Functioning
            Response = Space(4096)
            XmlBuf = "<AtXmlTestRequirements>" & "<ResourceRequirement>" & "  <ResourceType>Source</ResourceType>" & "  <SignalResourceName>DSO_1</SignalResourceName> " & "</ResourceRequirement>" & "</AtXmlTestRequirements>"
            status = atxml_ValidateRequirements(XmlBuf, Allocation, Response, 4096)
            If status <> 0 Then
                LiveMode(OSCOPE) = False
                MsgBox("The DSO Is Not Responding.  Live Mode Disabled.", MsgBoxStyle.Information)
            Else
                LiveMode(OSCOPE) = True
            End If

            'Determine if the FG is functioning
            Response = Space(4096)
            XmlBuf = "<AtXmlTestRequirements>" & "<ResourceRequirement>" & "   <ResourceType>Source</ResourceType>" & "   <SignalResourceName>FUNC_GEN_1</SignalResourceName> " & "</ResourceRequirement> " & "</AtXmlTestRequirements>"
            status = atxml_ValidateRequirements(XmlBuf, Allocation, Response, 4096)
            If status <> 0 Then
                LiveMode(FGEN) = False
                MsgBox("The Function Generator is not responding.  Live mode is disabled.", MsgBoxStyle.Exclamation)
            Else
                LiveMode(FGEN) = True
            End If

            'Determine if the Freedom PS is functioning
            Response = Space(4096)
            XmlBuf = "<AtXmlTestRequirements>"
            XmlBuf = XmlBuf & "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & "  <SignalResourceName>DCPS_40V5A_1</SignalResourceName> " & "</ResourceRequirement> "
            XmlBuf = XmlBuf & "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & "  <SignalResourceName>DCPS_40V5A_2</SignalResourceName> " & "</ResourceRequirement>"
            XmlBuf = XmlBuf & "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & "  <SignalResourceName>DCPS_40V5A_3</SignalResourceName> " & "</ResourceRequirement>"
            XmlBuf = XmlBuf & "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & "  <SignalResourceName>DCPS_40V5A_4</SignalResourceName> " & "</ResourceRequirement>"
            XmlBuf = XmlBuf & "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & "  <SignalResourceName>DCPS_40V5A_5</SignalResourceName> " & "</ResourceRequirement>"
            XmlBuf = XmlBuf & "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & "  <SignalResourceName>DCPS_40V5A_6</SignalResourceName> " & "</ResourceRequirement>"
            XmlBuf = XmlBuf & "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & "  <SignalResourceName>DCPS_40V5A_7</SignalResourceName> " & "</ResourceRequirement>"
            XmlBuf = XmlBuf & "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & "  <SignalResourceName>DCPS_40V5A_8</SignalResourceName> " & "</ResourceRequirement>"
            XmlBuf = XmlBuf & "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & "  <SignalResourceName>DCPS_40V5A_9</SignalResourceName> " & "</ResourceRequirement>"
            XmlBuf = XmlBuf & "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & "  <SignalResourceName>DCPS_65V5A_10</SignalResourceName> " & "</ResourceRequirement>"
            XmlBuf = XmlBuf & "</AtXmlTestRequirements>"
            status = atxml_ValidateRequirements(XmlBuf, Allocation, Response, Len(XmlBuf))
            If status Then
                LiveMode(PPU) = False
                MsgBox("The PPU is not responding.  Live mode is disabled.", MsgBoxStyle.Exclamation)
            Else
                LiveMode(PPU) = True
            End If


            'Determine if the Switch module is functioning
            Response = Space(4096)
            XmlBuf = "<AtXmlTestRequirements> " & "<ResourceRequirement> " & "   <ResourceType>Source</ResourceType> " & "   <SignalResourceName>PAWS_SWITCH</SignalResourceName> " & "</ResourceRequirement> " & "</AtXmlTestRequirements>"
            status = atxml_ValidateRequirements(XmlBuf, Allocation, Response, 4096)
            If status = conNoDLL Then
                MsgBox("Error in loading RI1260_32.DLL.  Switch Live mode is disabled.", MsgBoxStyle.Information)
                LiveMode(SWITCH1) = False
            ElseIf status <> 0 Then
                MsgBox("The Switch Module is not responting.  Live mode is disabled.", MsgBoxStyle.Information)
                LiveMode(SWITCH1) = False
            Else
                LiveMode(SWITCH1) = True
            End If

            'Determine if the RF STIM is functioning
            Response = Space(4096)
            XmlBuf = "<AtXmlTestRequirements> " & "<ResourceRequirement> " & "   <ResourceType>Source</ResourceType> " & "   <SignalResourceName>RFGEN_1</SignalResourceName> " & "</ResourceRequirement> " & "</AtXmlTestRequirements>"
            Response = Space(4096)
            status = atxml_ValidateRequirements(XmlBuf, Allocation, Response, 4096)
            If status <> 0 Then
                MsgBox("The RF Stimulus is not responding.  Live mode is disabled.", MsgBoxStyle.Exclamation)
                LiveMode(RFSYN) = False
            Else
                LiveMode(RFSYN) = True
            End If

            'Determine if the New Busses are functioning
            Response = Space(4096)
            XmlBuf = "<AtXmlTestRequirements>" & "<ResourceRequirement>" & "  <ResourceType>Source</ResourceType>" & "  <SignalResourceName>COM_1</SignalResourceName> " & "  </ResourceRequirement> " & "<ResourceRequirement>" & "  <ResourceType>Source</ResourceType>" & "  <SignalResourceName>COM_2</SignalResourceName> " & "  </ResourceRequirement> " & "<ResourceRequirement>" & "  <ResourceType>Source</ResourceType>" & "  <SignalResourceName>ETHERNET_1</SignalResourceName> " & "  </ResourceRequirement> " & "<ResourceRequirement>" & "  <ResourceType>Source</ResourceType>" & "  <SignalResourceName>ETHERNET_2</SignalResourceName> " & "  </ResourceRequirement> " & "</AtXmlTestRequirements>"
            status = atxml_ValidateRequirements(XmlBuf, Allocation, Response, 4096)

            If status = conNoDLL Then
                MsgBox("Error in loading Communication Bus Driver.", MsgBoxStyle.Information)
                LiveMode(RS232) = False
            ElseIf status <> 0 Then
                MsgBox("The Bus is not responding.", MsgBoxStyle.Exclamation)
                LiveMode(RS232) = False
            Else
                LiveMode(RS232) = True
            End If

            'Determine if the S/R is functioning
            Response = Space(4096)
            XmlBuf = "<AtXmlTestRequirements>"
            XmlBuf = XmlBuf & "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & "  <SignalResourceName>SRS_1_DS1</SignalResourceName> " & "</ResourceRequirement> "
            XmlBuf = XmlBuf & "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & "  <SignalResourceName>SRS_1_DS2</SignalResourceName> " & "</ResourceRequirement>"
            XmlBuf = XmlBuf & "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & "  <SignalResourceName>SRS_1_SD1</SignalResourceName> " & "</ResourceRequirement>"
            XmlBuf = XmlBuf & "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & "  <SignalResourceName>SRS_1_SD2</SignalResourceName> " & "</ResourceRequirement>"
            XmlBuf = XmlBuf & "</AtXmlTestRequirements>"

            status = atxml_ValidateRequirements(XmlBuf, Allocation, Response, 4096)
            If status <> 0 Then
                MsgBox("The Syncro/Resolver is not responding.  Live mode is disabled.", MsgBoxStyle.Exclamation)
                LiveMode(SYN_RES) = False
            Else
                LiveMode(SYN_RES) = True
            End If
            Delay(0.5)
        End If

        'Exit program if can not get a default session handle
        '    nErr = viOpenDefaultRM&(nSessionHandle&)
        '    If nErr <> VI_SUCCESS Then
        '        Echo sMsg
        '        sMsg = "VISA Resource Manager Error: " & GetVisaErrorMessage(ResourceName(InstrumentToInit), nErr) & _
        ''            vbCrLf & "Unable to perform System Self Test."
        '        MsgBox sMsg, vbExclamation
        '        End
        '    End If
    End Sub

    ''' <summary>
    ''' This Module Centers One Form With Respect To The User's Screen.
    ''' </summary>
    ''' <param name="frmForm">Form to be centered</param>
    ''' <remarks></remarks>
    Public Sub CenterForm(ByRef frmForm As System.Windows.Forms.Form)
        frmForm.StartPosition = FormStartPosition.CenterScreen
    End Sub

    ''' <summary>
    ''' Procedure to convert a delimited string into a list array
    ''' </summary>
    ''' <param name="strng">String to be converted.</param>
    ''' <param name="List">Array in which to return list of strings</param>
    ''' <param name="delimiter">Char array of valid delimiters</param>
    ''' <returns>Number of items in list; Returns -1 if number of elements exceeds upper bound of passed array</returns>
    ''' <remarks></remarks>
    Function StringToList(ByRef strng As String, ByRef List() As String, ByRef delimiter As String) As Short
        Dim numels As Short
        Dim inflag As Short
        Dim slength As Short
        Dim ch As Short
        Dim currentChar As String
        numels = 0
        inflag = 0
        Erase List ' bbbb added Apr, 2011

        'Go through parsed string a character at a time.
        slength = Len(strng)
        For ch = 1 To slength
            currentChar = Mid(strng, ch, 1)
            'Test for delimiter
            If InStr(delimiter, currentChar) = 0 Then
                If Not inflag Then
                    'Test for too many arguments.
                    If numels = UBound(List) Then
                        StringToList = -1
                        Exit For
                    End If
                    numels = numels + 1
                    inflag = -1
                End If
                'Add the character to the current argument.
                List(numels) = List(numels) & currentChar
            Else
                'Found a delimiter.
                'Set "Not in element" flag to FALSE.
                inflag = 0
            End If
        Next ch
        StringToList = numels
    End Function

    Sub EnableImageControl(ByRef index As Short)
        'Hide all test status indicators
        '    'gFrmMain.RibbonDivider.Visible = False
        '    'gFrmMain.TestStatusLabel.Visible = False
        '    gFrmMain.PWROffLight.Visible = False
        '    gFrmMain.PWROnLight.Visible = False
        '    gFrmMain.HVOnIndicator.Visible = False
        '    gFrmMain.CautionLabel.Visible = False
        '    gFrmMain.RedLight.Visible = False
        '    Delay 0.1

        ' Verify a current instance of the Image form.  If the last user exited the form using the (X) in the upper right hand corner 
        ' instead of the Quit button (quit button cause a form hide), the form is closed and disposed.  Therefore a new instance
        ' will be required.
        If gFrmImage.IsDisposed Then
            gFrmImage = New frmImage()
        End If
        gFrmImage.Hide()
        MouseAction = ZOOMING
        'Show image controls
        gFrmImage.MouseControl_0.Visible = True
        gFrmImage.MouseControl_1.Visible = True
        gFrmImage.MouseControl_2.Visible = True
        gFrmImage.ResetView.Visible = True
        gFrmImage.PPage.Visible = True
        gFrmImage.NPage.Visible = True
        gFrmImage.ZoomOut50.Visible = True
        gFrmImage.MouseControl_0.Enabled = True
        gFrmImage.MouseControl_1.Enabled = True
        gFrmImage.MouseControl_2.Enabled = True
        gFrmImage.ZoomOut50.Enabled = True
        gFrmImage.ResetView.Enabled = True
        gFrmImage.PrintButton.Enabled = True
        gFrmImage.PrintButton.Text = "Print Image"
        gFrmImage.ImageControlPanel.Visible = True
        gFrmImage.Quit.Visible = True
        gFrmImage.Quit.Enabled = True

        Select Case index
            Case VIEW_SCHEMATIC 'Schematic View
                If MaxSchematicPages = 1 Then
                    '5/7/03 by Soon Nam
                    'if there is only one schematic page,
                    'NPage and PPage should be disabled
                    gFrmImage.PPage.Enabled = False
                    gFrmImage.NPage.Enabled = False
                    Exit Sub
                End If

                Select Case SchemPageNum
                    Case Is = 1
                        gFrmImage.PPage.Enabled = False
                        If SchemPageNum + 1 > MaxSchematicPages Then
                            gFrmImage.NPage.Enabled = False
                        Else
                            gFrmImage.NPage.Enabled = True
                        End If
                    Case Is > 1
                        gFrmImage.PPage.Enabled = True
                        If SchemPageNum + 1 > MaxSchematicPages Then
                            gFrmImage.NPage.Enabled = False
                        Else
                            gFrmImage.NPage.Enabled = True
                        End If
                End Select

            Case VIEW_ID_SCHEMATIC 'ITA Schematic View
                If MaxIDSchematicPages = 1 Then
                    '5/9/03 by Soon Nam
                    'if there is only one ITA schematic page,
                    'NPage and PPage should be disabled
                    gFrmImage.PPage.Enabled = False
                    gFrmImage.NPage.Enabled = False
                    Exit Sub
                End If

                Select Case IDSchemPageNum
                    Case Is = 1
                        gFrmImage.PPage.Enabled = False
                        If IDSchemPageNum + 1 > MaxIDSchematicPages Then
                            gFrmImage.NPage.Enabled = False
                        Else
                            gFrmImage.NPage.Enabled = True
                        End If
                    Case Is > 1
                        gFrmImage.PPage.Enabled = True
                        If IDSchemPageNum + 1 > MaxIDSchematicPages Then
                            gFrmImage.NPage.Enabled = False
                        Else
                            gFrmImage.NPage.Enabled = True
                        End If
                End Select

            Case VIEW_ASSEMBLY 'Assembly View
                If MaxAssemblyPages = 1 Then
                    '5/7/03 by Soon Nam
                    'if there is only one assembly page,
                    'NPage and PPage should be disabled
                    gFrmImage.PPage.Enabled = False
                    gFrmImage.NPage.Enabled = False
                    Exit Sub
                End If

                Select Case AssyPageNum
                    Case Is = 1
                        gFrmImage.PPage.Enabled = False
                        If AssyPageNum + 1 > MaxAssemblyPages Then
                            gFrmImage.NPage.Enabled = False
                        Else
                            gFrmImage.NPage.Enabled = True
                        End If
                    Case Is > 1
                        gFrmImage.PPage.Enabled = True
                        If AssyPageNum + 1 > MaxAssemblyPages Then
                            gFrmImage.NPage.Enabled = False
                        Else
                            gFrmImage.NPage.Enabled = True
                        End If
                End Select

            Case VIEW_ID_ASSEMBLY 'ID Assembly View
                If MaxIDAssemblyPages = 1 Then
                    '5/9/03 by Soon Nam
                    'if there is only one ID assembly page,
                    'NPage and PPage should be disabled
                    gFrmImage.PPage.Enabled = False
                    gFrmImage.NPage.Enabled = False
                    Exit Sub
                End If

                Select Case IDAssyPageNum
                    Case Is = 1
                        gFrmImage.PPage.Enabled = False
                        If IDAssyPageNum + 1 > MaxIDAssemblyPages Then
                            gFrmImage.NPage.Enabled = False
                        Else
                            gFrmImage.NPage.Enabled = True
                        End If
                    Case Is > 1
                        gFrmImage.PPage.Enabled = True
                        If IDAssyPageNum + 1 > MaxIDAssemblyPages Then
                            gFrmImage.NPage.Enabled = False
                        Else
                            gFrmImage.NPage.Enabled = True
                        End If
                End Select

            Case VIEW_PARTSLIST 'Parts List View
                If MaxPartsListPages = 1 Then
                    '5/7/03 by Soon Nam
                    'if there is only one partslist page,
                    'NPage and PPage should be disabled
                    gFrmImage.PPage.Enabled = False
                    gFrmImage.NPage.Enabled = False
                    Exit Sub
                End If

                Select Case PartListPageNum
                    Case Is = 1
                        gFrmImage.PPage.Enabled = False
                        If PartListPageNum + 1 > MaxPartsListPages Then
                            gFrmImage.NPage.Enabled = False
                        Else
                            gFrmImage.NPage.Enabled = True
                        End If
                    Case Is > 1
                        gFrmImage.PPage.Enabled = True
                        If PartListPageNum + 1 > MaxPartsListPages Then
                            gFrmImage.NPage.Enabled = False
                        Else
                            gFrmImage.NPage.Enabled = True
                        End If
                End Select

            Case VIEW_ID_PARTSLIST 'ID Parts List View
                If MaxIDPartsListPages = 1 Then
                    '5/9/03 by Soon Nam
                    'if there is only one ID partslist page,
                    'NPage and PPage should be disabled
                    gFrmImage.PPage.Enabled = False
                    gFrmImage.NPage.Enabled = False
                    Exit Sub
                End If

                Select Case PartListPageNum
                    Case Is = 1
                        gFrmImage.PPage.Enabled = False
                        If PartListPageNum + 1 > MaxPartsListPages Then
                            gFrmImage.NPage.Enabled = False
                        Else
                            gFrmImage.NPage.Enabled = True
                        End If
                    Case Is > 1
                        gFrmImage.PPage.Enabled = True
                        If PartListPageNum + 1 > MaxPartsListPages Then
                            gFrmImage.NPage.Enabled = False
                        Else
                            gFrmImage.NPage.Enabled = True
                        End If
                End Select
        End Select
    End Sub

    Sub Delay(ByRef seconds As Single)
        Dim T As Single

        T = VB.Timer()
        Do While VB.Timer() - T < seconds
            'added 5/23/03 - fix 100% CPU usage during idle time
            System.Threading.Thread.Sleep(1)
        Loop
    End Sub

    Sub DoMenuChoice(ByRef Choice As Short)
        Dim lhwnlAPP As Integer
        Dim sTempString As String
        Dim flash As Short

        ImageViewerChoice = False

        Select Case Choice
            Case RUNTP_BUTTON, ID_SURVEY
                gFrmMain.Timer1.Enabled = False
                gFrmMain.lblStatus.Text = "Running Test Program"

                HelpKey = "!RUN TEST PROGRAM"
                gFrmMain.Text = "Test Program Set: " & UUTPartNo
                Echo("")
                Echo("")
                Echo("*******************************************************************************")
                Echo("               TEST PROGRAM: " & TPName)
                Echo("                      TPCCN: " & TestData.sTPCCN)
                Echo("       TEST PROGRAM VERSION: " & TPVersion)
                Echo("           UUT NOMENCLATURE: " & UUTName)
                Echo("            UUT PART NUMBER: " & UUTPartNo)
                Echo("          TESTING DATE/TIME: " & DateTime.Now.ToString("dddd, dd-MMM-yyyy HH:mm"))
                Echo("*******************************************************************************")

                If Choice = RUNTP_BUTTON Then
                    UserEvent = RUNTP_BUTTON
                Else
                    With TestData
                        .EROs = "ID Test"
                        .sUUTRev = "N/A"
                        .sUUTSerial = "N/A"
                        .sStart = CStr(Now)
                    End With
                    UserEvent = ID_SURVEY
                End If

            Case VIEW_SCHEMATIC
                HelpKey = "!VIEW SCHEMATIC"

                'DisableProgramControl
                SchemPageNum = gFrmMain.cboSchematic.SelectedIndex + 1
                gFrmImage.Text = "Schematic Diagram: " & UUTPartNo & " [Page " & Str(SchemPageNum) & " of " & Str(MaxSchematicPages) & "]"
                sImageFile = SchemPath & sSchemFileNames(SchemPageNum)
                If SchemPageNum = MaxSchematicPages Then
                    gFrmImage.NPage.Enabled = False
                Else
                    gFrmImage.NPage.Enabled = True
                End If
                If SchemPageNum = 1 Then
                    gFrmImage.PPage.Enabled = False
                Else
                    gFrmImage.PPage.Enabled = True
                End If
                ImageViewerChoice = True
                gFrmImage.Show()
                TheImage = LoadImage() ' Load Page
                If TheImage = 0 Then
                    ShowMainMenu()
                    gFrmImage.Hide()
                    Exit Sub
                End If

            Case VIEW_ID_SCHEMATIC
                'DisableProgramControl
                IDSchemPageNum = gFrmMain.cboITAWiring.SelectedIndex + 1
                gFrmImage.Text = "ITA Schematic Diagram: " & IDPartNo & " [Page " & Str(IDSchemPageNum) & " of " & Str(MaxIDSchematicPages) & "]"
                sImageFile = IDSchemPath & sIDSchemFileNames(IDSchemPageNum)
                If IDSchemPageNum = MaxIDSchematicPages Then
                    gFrmImage.NPage.Enabled = False
                Else
                    gFrmImage.NPage.Enabled = True
                End If
                If IDSchemPageNum = 1 Then
                    gFrmImage.PPage.Enabled = False
                Else
                    gFrmImage.PPage.Enabled = True
                End If
                gFrmImage.Show()
                TheImage = LoadImage() ' Load Page
                If TheImage = 0 Then
                    ShowMainMenu()
                    gFrmImage.Hide()
                    Exit Sub
                End If

            Case VIEW_ASSEMBLY
                HelpKey = "!VIEW ASSEMBLY"
                'DisableProgramControl
                AssyPageNum = gFrmMain.cboAssembly.SelectedIndex + 1
                gFrmImage.Text = "Assembly Diagram: " & UUTPartNo & " [Page " & Str(AssyPageNum) & " of " & Str(MaxAssemblyPages) & "]"
                sImageFile = AssyPath & sAssyFileNames(AssyPageNum)
                If AssyPageNum = MaxAssemblyPages Then
                    gFrmImage.NPage.Enabled = False
                Else
                    gFrmImage.NPage.Enabled = True
                End If
                If AssyPageNum = 1 Then
                    gFrmImage.PPage.Enabled = False
                Else
                    gFrmImage.PPage.Enabled = True
                End If
                gFrmImage.Show()
                TheImage = LoadImage() ' Load Page
                If TheImage = 0 Then
                    ShowMainMenu()
                    gFrmImage.Hide()
                    Exit Sub
                End If

            Case VIEW_ID_ASSEMBLY
                HelpKey = "!VIEW ASSEMBLY"
                'DisableProgramControl
                IDAssyPageNum = gFrmMain.cboITAAssy.SelectedIndex + 1
                gFrmImage.Text = "ITA Assembly Diagram: " & IDPartNo & " [Page " & Str(IDAssyPageNum) & " of " & Str(MaxIDAssemblyPages) & "]"
                sImageFile = IDAssyPath & sIDAssyFileNames(IDAssyPageNum)
                If IDAssyPageNum = MaxIDAssemblyPages Then
                    gFrmImage.NPage.Enabled = False
                Else
                    gFrmImage.NPage.Enabled = True
                End If
                If IDAssyPageNum = 1 Then
                    gFrmImage.PPage.Enabled = False
                Else
                    gFrmImage.PPage.Enabled = True
                End If
                gFrmImage.Show()
                TheImage = LoadImage() ' Load Page
                If TheImage = 0 Then
                    ShowMainMenu()
                    gFrmImage.Hide()
                    Exit Sub
                End If

            Case VIEW_PARTSLIST
                HelpKey = "!VIEW PARTS LIST"
                PartListPageNum = gFrmMain.cboPartsList.SelectedIndex + 1
                gFrmImage.Text = "Parts List: " & UUTPartNo & " [Page " & Str(PartListPageNum) & " of " & Str(MaxPartsListPages) & "]"
                'DisableProgramControl
                If LCase(Right(sPartsListFileNames(PartListPageNum), 4)) = ".txt" Then
                    DisplayTextFile((PartListPath & sPartsListFileNames(1)))
                    gFrmImage.Show()
                Else
                    sImageFile = PartListPath & sPartsListFileNames(PartListPageNum)
                    If PartListPageNum = MaxPartsListPages Then
                        gFrmImage.NPage.Enabled = False
                    Else
                        gFrmImage.NPage.Enabled = True
                    End If
                    If PartListPageNum = 1 Then
                        gFrmImage.PPage.Enabled = False
                    Else
                        gFrmImage.PPage.Enabled = True
                    End If
                    gFrmImage.Show()
                    TheImage = LoadImage() ' Load Page
                    If TheImage = 0 Then
                        ShowMainMenu()
                        gFrmImage.Hide()
                        Exit Sub
                    End If
                End If

            Case VIEW_ID_PARTSLIST
                HelpKey = "!VIEW PARTS LIST"
                IDPartListPageNum = gFrmMain.cboITAPartsList.SelectedIndex + 1
                gFrmImage.Text = "ITA Parts List: " & IDPartNo & " [Page " & Str(IDPartListPageNum) & " of " & Str(MaxIDPartsListPages) & "]"
                'DisableProgramControl
                If LCase(Right(sIDPartsListFileNames(IDPartListPageNum), 4)) = ".txt" Then
                    DisplayTextFile((IDPartListPath & sIDPartsListFileNames(1)))
                    gFrmImage.Show()
                Else
                    sImageFile = IDPartListPath & sIDPartsListFileNames(IDPartListPageNum)
                    If IDPartListPageNum = MaxIDPartsListPages Then
                        gFrmImage.NPage.Enabled = False
                    Else
                        gFrmImage.NPage.Enabled = True
                    End If
                    If IDPartListPageNum = 1 Then
                        gFrmImage.PPage.Enabled = False
                    Else
                        gFrmImage.PPage.Enabled = True
                    End If
                    gFrmImage.Show()
                    TheImage = LoadImage() ' Load Page
                    If TheImage = 0 Then
                        ShowMainMenu()
                        gFrmImage.Hide()
                        Exit Sub
                    End If
                End If
                gFrmImage.Show()

            Case VIEW_TSR
                sImageFile = DocumentsPath & sTSRFileName
                ShellExecute(lhwnlAPP, "open", sImageFile, "", "", 1)
                sTempString = gFrmMain.lblStatus.Text
                For flash = 1 To 6
                    gFrmMain.lblStatus.Text = "Opening Document"
                    gFrmMain.MenuOptionText(-VIEW_TSR).Visible = False
                    System.Windows.Forms.Application.DoEvents()
                    Delay((0.1))
                    gFrmMain.MenuOptionText(-VIEW_TSR).Visible = True
                    System.Windows.Forms.Application.DoEvents()
                    Delay((0.1))
                Next flash
                gFrmMain.lblStatus.Text = sTempString

            Case VIEW_ELTD
                sImageFile = DocumentsPath & sELTDFileName
                ShellExecute(lhwnlAPP, "open", sImageFile, "", "", 1)
                sTempString = gFrmMain.lblStatus.Text
                For flash = 1 To 6
                    gFrmMain.lblStatus.Text = "Opening Document"
                    gFrmMain.MenuOptionText(-VIEW_ELTD).Visible = False
                    System.Windows.Forms.Application.DoEvents()
                    Delay((0.1))
                    gFrmMain.MenuOptionText(-VIEW_ELTD).Visible = True
                    System.Windows.Forms.Application.DoEvents()
                    Delay((0.1))
                Next flash
                gFrmMain.lblStatus.Text = sTempString

            Case VIEW_GENERAL_INFO
                sImageFile = DocumentsPath & sInfoFileName
                ShellExecute(lhwnlAPP, "open", sImageFile, "", "", 1)
                sTempString = gFrmMain.lblStatus.Text
                For flash = 1 To 6
                    gFrmMain.lblStatus.Text = "Opening Document"
                    gFrmMain.MenuOptionText(-VIEW_GENERAL_INFO).Visible = False
                    System.Windows.Forms.Application.DoEvents()
                    Delay((0.1))
                    gFrmMain.MenuOptionText(-VIEW_GENERAL_INFO).Visible = True
                    System.Windows.Forms.Application.DoEvents()
                    Delay((0.1))
                Next flash
                gFrmMain.lblStatus.Text = sTempString

            Case TPS_DOCUMENTATION
                ShowTestDocumentation()

            Case VIEW_FAULT_FILE
                RunMyApp(sStartMode:="SW_MAXIMIZE")

            Case VIEW_IETM
                ShellExecute(lhwnlAPP, "open", IETMPath, "", "", 1)
                sTempString = gFrmMain.lblStatus.Text
                For flash = 1 To 6
                    gFrmMain.lblStatus.Text = "Opening Document"
                    gFrmMain.MenuOptionText(-VIEW_IETM).Visible = False
                    System.Windows.Forms.Application.DoEvents()
                    Delay((0.1))
                    gFrmMain.MenuOptionText(-VIEW_IETM).Visible = True
                    System.Windows.Forms.Application.DoEvents()
                    Delay((0.1))
                Next flash
                gFrmMain.lblStatus.Text = sTempString

            Case QUIT_BUTTON
                gFrmExit.ShowDialog()
        End Select
    End Sub

    Sub DisableProgramControl()
        gFrmMain.cmdContinue.Enabled = False
        gFrmMain.AbortButton.Enabled = False
    End Sub

    Sub ShowMainMenu()
        Dim opt As Short

        gFrmMain.Timer1.Enabled = True
        gFrmMain.Timer2.Enabled = False
        gFrmMain.fraInstrument.Visible = False
        gFrmMain.TimerStatusByteRec.Enabled = False

        gFrmMain.MainMenu.Visible = True
        gFrmMain.picTestDocumentation.Visible = False

        gFrmMain.ModuleMenu.Visible = False
        gFrmMain.MainMenuButton.Enabled = False
        For opt = 1 To MAX_MENU_OPT
            gFrmMain.MenuOption(opt).Visible = True
            '        frmmain.MenuOption(opt).Enabled = True
            gFrmMain.MenuOptionText(opt).Visible = True
            '        frmmain.MenuOptionText(opt).Enabled = True
        Next opt

        If UUTPartNo = IDPartNo Then
            gFrmMain.MenuOption(-ID_SURVEY).Enabled = False
            gFrmMain.MenuOptionText(-ID_SURVEY).Enabled = False
        End If

        gFrmMain.fraInstructions.Visible = False
        gFrmMain.cmdFHDB.Visible = False
        gFrmMain.pinp.Image = Nothing
        gFrmMain.pinp.Visible = False

        ImageViewerChoice = False 'Required for HELP function to work properly
        gFrmMain.PictureWindow.Visible = False
        gFrmMain.TextWindow.Visible = False
        gFrmMain.SeqTextWindow.Visible = False
        DisableProgramControl()
        gFrmMain.AbortButton.Visible = True
        gFrmMain.RerunButton.Visible = False
        gFrmMain.PrintButton.Visible = True
        gFrmMain.PrintButton.Enabled = False
        gFrmMain.cmdContinue.Enabled = False
        gFrmMain.Quit.Enabled = True
        gFrmMain.Text = "Test Program Set: " & UUTPartNo
        gFrmMain.cmdFHDB.Enabled = False
        gFrmMain.cmdFHDB.Visible = False
        If gFrmMain.cboAssembly.Items(0).ToString() <> "" Then gFrmMain.cboAssembly.Visible = True
        If gFrmMain.cboPartsList.Items(0).ToString() <> "" Then gFrmMain.cboPartsList.Visible = True
        If gFrmMain.cboSchematic.Items(0).ToString() <> "" Then gFrmMain.cboSchematic.Visible = True
        gFrmMain.cboITAAssy.Visible = False
        gFrmMain.cboITAPartsList.Visible = False
        gFrmMain.cboITAWiring.Visible = False

        HelpKey = "!MAIN MENU"
        gFrmMain.TextWindow.Text = ""

        If ProgramPath = " " Then
            MsgBox("Test Program Path not specified. Set the TPPath variable.", 16, "Shell Error - Fatal")
        End If

        For opt = 1 To MAX_MENU_OPT
            Select Case opt
                Case RUNTP_BUTTON 'Enable RUN TEST PROGRAM BUTTON
                    gFrmMain.MenuOption(opt).Enabled = True
                    gFrmMain.MenuOptionText(opt).Enabled = True

                Case VIEW_SCHEMATIC 'Enable VIEW SCHEMATIC BUTTON if schematics are available
                    If MaxSchematicPages > 0 Then
                        gFrmMain.MenuOption(opt).Enabled = True
                        gFrmMain.MenuOptionText(opt).Enabled = True
                    End If

                Case VIEW_ASSEMBLY 'Enable VIEW ASSEMBLY BUTTON if Assembly Drawing is available
                    If MaxAssemblyPages > 0 Then
                        gFrmMain.MenuOption(opt).Enabled = True
                        gFrmMain.MenuOptionText(opt).Enabled = True
                    End If

                Case VIEW_PARTSLIST 'Enable VIEW PARTS LIST BUTTON if Parts List is available
                    If MaxPartsListPages > 0 Then
                        gFrmMain.MenuOption(opt).Enabled = True
                        gFrmMain.MenuOptionText(opt).Enabled = True
                    End If
            End Select
        Next opt

        CenterForm(gFrmMain)
        gFrmMain.Text = "Test Program Set: " & UUTPartNo & " Version: " & TPVersion
        gFrmMain.Show()
    End Sub

    Sub DisplayTextFile(ByRef fileName As String)
        Dim TextFile As Short
        Dim dataline As String
        Dim TheFile As String = ""

        On Error Resume Next

        ShowWindow((gFrmMain.TextWindow))
        TextFile = FreeFile()
        FileOpen(TextFile, fileName, OpenMode.Input)
        If Err.Number Then
            MsgBox("Error opening specified file: " & fileName & " .  Check to see that path and filename specified are correct.", 16, "Shell Error")
            Exit Sub
        End If
        Do While Not EOF(TextFile)
            dataline = LineInput(TextFile)
            dataline = Mid(dataline, 1, 77) & vbCrLf
            TheFile = TheFile & dataline
        Loop
        FileClose(TextFile)
        If Len(TheFile) > 65000 Then
            MsgBox("File too large for viewer", 16, "Shell Error - Non-Fatal")
            Exit Sub
        End If
        gFrmMain.TextWindow.Text = TheFile
        gFrmMain.MainMenuButton.Enabled = True
    End Sub

    Sub ShowWindow(ByRef WindowToShow As System.Windows.Forms.Control)
        gFrmMain.PictureWindow.Visible = False
        gFrmMain.MainMenu.Visible = False
        gFrmMain.SeqTextWindow.Visible = False
        gFrmMain.ModuleMenu.Visible = False
        WindowToShow.Visible = True
        WindowToShow.BringToFront()
    End Sub

    Sub GetEnabledButtons()
        If gFrmMain.MainMenuButton.Enabled = True Then
            Buttons(Button) = "MainMenuButton"
            Button = Button + 1
        End If
        If gFrmMain.Quit.Enabled = True Then
            Buttons(Button) = "Quit"
            Button = Button + 1
        End If
        If gFrmMain.AbortButton.Enabled = True Then
            Buttons(Button) = "Abort"
            Button = Button + 1
        End If
        If gFrmMain.cmdContinue.Enabled = True Then
            Buttons(Button) = "Continue"
            Button = Button + 1
        End If
        If gFrmImage.MouseControl_0.Enabled = True Then
            Buttons(Button) = "MouseAction(PANNING)"
            Button = Button + 1
        End If
        If gFrmImage.MouseControl_1.Enabled = True Then
            Buttons(Button) = "MouseAction(ZOOMING)"
            Button = Button + 1
        End If
        If gFrmImage.MouseControl_2.Enabled = True Then
            Buttons(Button) = "MouseAction(MAGNIFYING)"
            Button = Button + 1
        End If
        If gFrmImage.PPage.Enabled = True Then
            Buttons(Button) = "PreviousPage"
            Button = Button + 1
        End If
        If gFrmImage.NPage.Enabled = True Then
            Buttons(Button) = "NextPage"
            Button = Button + 1
        End If
        If gFrmImage.ResetView.Enabled = True Then
            Buttons(Button) = "ResetView"
            Button = Button + 1
        End If
        If gFrmImage.ZoomOut50.Enabled = True Then
            Buttons(Button) = "ZoomOut50"
            Button = Button + 1
        End If
        If gFrmImage.PrintButton.Enabled = True Then
            Buttons(Button) = "Print"
            Button = Button + 1
        End If
        If gFrmMain.RerunButton.Enabled = True Then
            Buttons(Button) = "Rerun"
            Button = Button + 1
        End If
    End Sub

    Function Limit(ByRef CheckValue As Short, ByRef LimitValue As Short) As Short
        If CheckValue < LimitValue Then
            Limit = LimitValue
        Else
            Limit = CheckValue
        End If
    End Function

    Sub Echo(ByVal Message As String)
        Dim l As Short
        Dim PrintMessage As String

        'Indent message on screen by 2 spaces
        Message = Space(2) & Message

        gFrmMain.SeqTextWindowLabel.Text = String.Empty
        For l = 1 To MaxScreenLines - 1
            ScreenBuffer(l) = ScreenBuffer(l + 1)
            If l = 1 Then
                gFrmMain.SeqTextWindowLabel.Text += ScreenBuffer(l)
            Else
                gFrmMain.SeqTextWindowLabel.Text += vbCrLf & ScreenBuffer(l)
            End If
        Next l
        ScreenBuffer(MaxScreenLines) = Message
        gFrmMain.SeqTextWindowLabel.Text += vbCrLf & ScreenBuffer(MaxScreenLines)

        'Format test results for printer output
        PrintMessage = Space(6) & Message & vbCrLf
        gFrmMain.TextWindow.Text = gFrmMain.TextWindow.Text & PrintMessage

        Err.Clear()
        If Logging Then
            Try
                Dim FileStream As IO.FileStream = New IO.FileStream(LOG_FILE, IO.FileMode.Append, IO.FileAccess.Write, IO.FileShare.ReadWrite)
                Dim FileWriter As IO.StreamWriter = New IO.StreamWriter(FileStream)

                FileWriter.WriteLine(Message)
                FileWriter.Flush()
                FileWriter.Close()
                FileStream.Close()
            Catch ex As Exception
                Logging = False ' prevent this message from coming up on every write operation if there is an issue
                MessageBox.Show("Error opening the log file (" & LOG_FILE & "):" & vbCrLf & ex.Message & vbCrLf & "Logging is disabled.",
                                "Log file access error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End Try
        End If
        System.Windows.Forms.Application.DoEvents()
    End Sub

    Sub EnableAbort()
        gFrmMain.AbortButton.Visible = True
        gFrmMain.AbortButton.Enabled = True
        gFrmMain.RerunButton.Visible = False
        gFrmMain.RerunButton.Enabled = False
        gFrmMain.PrintButton.Enabled = False
        '***Modified For QuickView Version 3.7 DWH***
        gFrmMain.MainMenuButton.Enabled = False
        '**********End Of Modifications DWH**********
        gFrmMain.Quit.Enabled = False
    End Sub

    Sub LongDelay(ByRef seconds As Single)
        Dim startTime As DateTime = DateTime.Now
        Dim timeDiff As TimeSpan = DateTime.Now.Subtract(startTime)

        gFrmMain.Quit.Enabled = False
        Do While timeDiff.Seconds < seconds
            System.Threading.Thread.Sleep(1)
            timeDiff = DateTime.Now.Subtract(startTime)
            System.Windows.Forms.Application.DoEvents()
        Loop
    End Sub

    Sub DisablePrint()
        gFrmMain.PrintButton.Enabled = False
    End Sub

    Sub DisplayBitmap(ByRef FileSpec As String)
        On Error Resume Next

        gFrmMain.PictureWindow.BackgroundImage = Nothing
        '    frmmain.PictureWindow.Picture = LoadPicture()
        'Hide Progress Bar if visible
        If gFrmMain.PictureWindow.Visible = False Then
            ShowWindow(gFrmMain.PictureWindow)
        End If
        gFrmMain.pinp.Image = System.Drawing.Image.FromFile(FileSpec)
        If Err.Number Then
            MsgBox("Could not load file: " & FileSpec & " .  Check to see that the file exists and is specified properly.", 16, "Shell Error")
        End If
        gFrmMain.pinp.SetBounds(gFrmMain.PictureWindow.Left - 8, gFrmMain.PictureWindow.Top - 8, gFrmMain.PictureWindow.Width, gFrmMain.PictureWindow.Height)
        '    gFrmMain.pinp.Top = gFrmMain.PictureWindow.Height / 2 - gFrmMain.pinp.Height / 2
        '    gFrmMain.pinp.Left = gFrmMain.PictureWindow.Width / 2 - gFrmMain.pinp.Width / 2
        gFrmMain.pinp.Visible = True
        '  frmmain.Show
        gFrmMain.Quit.Enabled = True
        gFrmMain.cmdContinue.Enabled = True
        gFrmMain.AcceptButton = gFrmMain.cmdContinue
        gFrmMain.PrintButton.Enabled = False
        gFrmMain.AbortButton.Enabled = False
        gFrmMain.MainMenuButton.Enabled = True
    End Sub

    Sub EnableContinue()
        gFrmMain.cmdContinue.Enabled = True
    End Sub

    Sub EnableQuit()
        gFrmMain.Quit.Enabled = True
    End Sub

    Sub DisableQuit()
        gFrmMain.Quit.Enabled = False
    End Sub

    Sub UpdateProgress(ByRef PercentComplete As Single)
        Dim sTemp As String
        Dim iCount As Short

        'sTemp = gFrmMain.lblStatus.Caption
        gFrmMain.lblStatus.Text = CStr(PercentComplete) & "% Complete"
        On Error GoTo ErrorHandle

        gFrmMain.Timer2.Enabled = True
        gFrmMain.Timer1.Enabled = False
        If PercentComplete > 100 Then PercentComplete = 100

        '    Select Case PercentComplete
        '        Case 0 To 20
        '            gFrmMain.picStatus(0).Cls
        '            gFrmMain.picStatus(1).Cls
        '            gFrmMain.picStatus(2).Cls
        '            gFrmMain.picStatus(3).Cls
        '            gFrmMain.picStatus(4).Cls
        '
        '            gFrmMain.picStatus(0).Picture = gFrmMain.ImageList2.ListImages(2).Picture
        '            gFrmMain.picStatus(1).Picture = gFrmMain.ImageList2.ListImages(1).Picture
        '            gFrmMain.picStatus(2).Picture = gFrmMain.ImageList2.ListImages(1).Picture
        '            gFrmMain.picStatus(3).Picture = gFrmMain.ImageList2.ListImages(1).Picture
        '            gFrmMain.picStatus(4).Picture = gFrmMain.ImageList2.ListImages(1).Picture
        '
        '        Case Is > 20, Is <= 40
        '            gFrmMain.picStatus(0).Cls
        '            gFrmMain.picStatus(1).Cls
        '            gFrmMain.picStatus(2).Cls
        '            gFrmMain.picStatus(3).Cls
        '            gFrmMain.picStatus(4).Cls
        '            gFrmMain.picStatus(0).Picture = gFrmMain.ImageList2.ListImages(2).Picture
        '            gFrmMain.picStatus(1).Picture = gFrmMain.ImageList2.ListImages(2).Picture
        '            gFrmMain.picStatus(2).Picture = gFrmMain.ImageList2.ListImages(1).Picture
        '            gFrmMain.picStatus(3).Picture = gFrmMain.ImageList2.ListImages(1).Picture
        '            gFrmMain.picStatus(4).Picture = gFrmMain.ImageList2.ListImages(1).Picture
        '
        '        Case Is > 40, Is <= 60
        '            gFrmMain.picStatus(0).Cls
        '            gFrmMain.picStatus(1).Cls
        '            gFrmMain.picStatus(2).Cls
        '            gFrmMain.picStatus(3).Cls
        '            gFrmMain.picStatus(4).Cls
        '            gFrmMain.picStatus(0).Picture = gFrmMain.ImageList2.ListImages(2).Picture
        '            gFrmMain.picStatus(1).Picture = gFrmMain.ImageList2.ListImages(2).Picture
        '            gFrmMain.picStatus(2).Picture = gFrmMain.ImageList2.ListImages(2).Picture
        '            gFrmMain.picStatus(3).Picture = gFrmMain.ImageList2.ListImages(1).Picture
        '            gFrmMain.picStatus(4).Picture = gFrmMain.ImageList2.ListImages(1).Picture
        '
        '        Case Is > 60, Is <= 80
        '            gFrmMain.picStatus(0).Cls
        '            gFrmMain.picStatus(1).Cls
        '            gFrmMain.picStatus(2).Cls
        '            gFrmMain.picStatus(3).Cls
        '            gFrmMain.picStatus(4).Cls
        '            gFrmMain.picStatus(0).Picture = gFrmMain.ImageList2.ListImages(2).Picture
        '            gFrmMain.picStatus(1).Picture = gFrmMain.ImageList2.ListImages(2).Picture
        '            gFrmMain.picStatus(2).Picture = gFrmMain.ImageList2.ListImages(2).Picture
        '            gFrmMain.picStatus(3).Picture = gFrmMain.ImageList2.ListImages(2).Picture
        '            gFrmMain.picStatus(4).Picture = gFrmMain.ImageList2.ListImages(1).Picture
        '
        '        Case Is > 80, Is < 100
        '            gFrmMain.picStatus(0).Cls
        '            gFrmMain.picStatus(1).Cls
        '            gFrmMain.picStatus(2).Cls
        '            gFrmMain.picStatus(3).Cls
        '            gFrmMain.picStatus(4).Cls
        '            gFrmMain.picStatus(0).Picture = gFrmMain.ImageList2.ListImages(2).Picture
        '            gFrmMain.picStatus(1).Picture = gFrmMain.ImageList2.ListImages(2).Picture
        '            gFrmMain.picStatus(2).Picture = gFrmMain.ImageList2.ListImages(2).Picture
        '            gFrmMain.picStatus(3).Picture = gFrmMain.ImageList2.ListImages(2).Picture
        '            gFrmMain.picStatus(4).Picture = gFrmMain.ImageList2.ListImages(2).Picture
        '
        '        Case 100
        '            gFrmMain.picStatus(0).Cls
        '            gFrmMain.picStatus(1).Cls
        '            gFrmMain.picStatus(2).Cls
        '            gFrmMain.picStatus(3).Cls
        '            gFrmMain.picStatus(4).Cls
        '            gFrmMain.picStatus(0).Picture = gFrmMain.ImageList2.ListImages(2).Picture
        '            gFrmMain.picStatus(1).Picture = gFrmMain.ImageList2.ListImages(2).Picture
        '            gFrmMain.picStatus(2).Picture = gFrmMain.ImageList2.ListImages(2).Picture
        '            gFrmMain.picStatus(3).Picture = gFrmMain.ImageList2.ListImages(2).Picture
        '            gFrmMain.picStatus(4).Picture = gFrmMain.ImageList2.ListImages(2).Picture
        '
        '    End Select

        'added 5/20/03 Soon Nam
        'remove picStatus and insert progressbar
        For iCount = 0 To 4
            If gFrmMain.picStatus(iCount).Visible Then
                gFrmMain.picStatus(iCount).Visible = False
            End If
        Next iCount

        'enable progress bar
        gFrmMain.ProgressBar.Enabled = True
        gFrmMain.ProgressBar.Left = 623
        gFrmMain.ProgressBar.Width = 133
        gFrmMain.ProgressBar.Maximum = 100

        gFrmMain.ProgressBar.Visible = True
        gFrmMain.ProgressBar.Value = PercentComplete

        'remove progress bar at 100% and put back picStatus
        If PercentComplete = 100 Then
            'remove progress bar
            gFrmMain.ProgressBar.Visible = False
            Delay(1)
            For iCount = 0 To 4
                gFrmMain.picStatus(iCount).Visible = True
                gFrmMain.picStatus(iCount).Image = gFrmMain.ImageList2.Images(0)
            Next iCount
            gFrmMain.Timer2.Enabled = False
            gFrmMain.lblStatus.ForeColor = System.Drawing.Color.Black
            'gFrmMain.lblStatus.Caption = sTemp
        End If
        System.Windows.Forms.Application.DoEvents()
        Exit Sub
ErrorHandle:
        MsgBox("Error No.: " & Err.Number & vbCrLf & "Description: " & Err.Description, 16, "Shell Error")
        Err.Clear()
        Resume Next
    End Sub

    Sub DisableContinue()
        gFrmMain.cmdContinue.Enabled = False
    End Sub

    Sub DisableRerun()
        gFrmMain.RerunButton.Enabled = False
    End Sub

    Sub EnablePrint()
        gFrmMain.MainMenuButton.Enabled = False
        gFrmMain.AbortButton.Visible = False
        gFrmMain.AbortButton.Enabled = False
        gFrmMain.RerunButton.Visible = False
        gFrmMain.RerunButton.Enabled = False

        gFrmMain.PrintButton.Text = "Print Results"
        gFrmMain.PrintButton.Visible = True
        gFrmMain.PrintButton.Enabled = True
    End Sub

    Sub EnableRerun()
        gFrmMain.RerunButton.Visible = True
        gFrmMain.RerunButton.Enabled = True
        gFrmMain.AbortButton.Visible = False
        gFrmMain.AbortButton.Enabled = False
    End Sub

    Sub EnableButtonList()
        gFrmMain.MainMenuButton.Enabled = True
        gFrmMain.Quit.Enabled = True
        gFrmMain.AbortButton.Enabled = True
        gFrmMain.cmdContinue.Enabled = True
    End Sub

    Sub ShowForm(ByRef FormToShow As System.Windows.Forms.Form)
        CenterForm(FormToShow)
        FormToShow.ShowDialog()
    End Sub

    Public Sub ChangeTextColor(ByRef nColor As System.Drawing.Color)
        gFrmMain.SeqTextWindow.ForeColor = nColor
    End Sub

    Sub ShowModuleMenu()
        Dim i As Short

        ClearTestMeasurementInformation()
        gFrmMain.Timer2.Enabled = False
        gFrmMain.Timer1.Enabled = True
        gFrmMain.lblStatus.Text = "Waiting on User ..."
        gFrmMain.TimerStatusByteRec.Enabled = False

        gFrmMain.fraInstructions.Visible = False

        gFrmMain.PictureWindow.BackgroundImage = Nothing
        gFrmMain.PictureWindow.Visible = False
        gFrmMain.pinp.Image = Nothing
        gFrmMain.pinp.Visible = False
        gFrmMain.fraInstructions.Visible = False
        gFrmMain.cmdFHDB.Visible = False
        gFrmMain.cmdDiagnostics.Visible = False

        gFrmMain.MainMenu.Visible = False
        gFrmMain.MainMenuButton.Enabled = True
        gFrmMain.pinp.Image = Nothing
        gFrmMain.pinp.Visible = False
        gFrmMain.PictureWindow.Visible = False
        gFrmMain.TextWindow.Visible = False
        gFrmMain.SeqTextWindow.Visible = False
        gFrmMain.ModuleMenu.Visible = True
        DisableProgramControl()
        gFrmMain.AbortButton.Enabled = False
        gFrmMain.AbortButton.Visible = True
        gFrmMain.RerunButton.Visible = False
        gFrmMain.PrintButton.Visible = True
        gFrmMain.PrintButton.Enabled = False
        gFrmMain.PrintButton.Text = "Print"
        gFrmMain.cmdContinue.Enabled = False
        gFrmMain.Quit.Enabled = True
        gFrmMain.Text = "Test Program Set: " & UUTPartNo & " Module Menu"

        'Build Module Menu
        If bModuleMenuBuilt = False Then
            gFrmMain.lblPwrOn.Enabled = False
            gFrmMain.cmdPwrOnModule.Enabled = False
            gFrmMain.lblPwrOnStatus.Enabled = False

            gFrmMain.ModuleInner.AutoScrollMinSize = New Size(gFrmMain.ModuleInner.Width - 30, gFrmMain.ModuleMenu.Height - gFrmMain.lblStatusTitle.Height - 14)
            bModuleMenuBuilt = True

            gFrmMain.lblModuleName_1.Text = ModuleNames(0)
            If ModuleRunTime(0) > 60 Then
                gFrmMain.lblModuleRunTime_1.Text = (ModuleRunTime(0) / 60).ToString("##0.00") & " Min."
            Else
                gFrmMain.lblModuleRunTime_1.Text = ModuleRunTime(0) & " Sec."
            End If

            gFrmMain.lblModuleName_1.Visible = True
            gFrmMain.lblModuleRunTime_1.Visible = True
            gFrmMain.cmdModule_1.Visible = True
            gFrmMain.lblModuleStatus_1.Visible = True
            gFrmMain.lblModuleName_1.Enabled = False
            gFrmMain.lblModuleName_1.Text = ModuleNames(1)
            gFrmMain.lblModuleRunTime_1.Enabled = False
            If ModuleRunTime(1) > 60 Then
                gFrmMain.lblModuleRunTime_1.Text = (ModuleRunTime(1) / 60).ToString("##0.00") & " Min."
            Else
                gFrmMain.lblModuleRunTime_1.Text = ModuleRunTime(1) & " Sec."
            End If
            gFrmMain.cmdModule_1.Enabled = False
            gFrmMain.lblModuleStatus_1.Enabled = False

            For i = 2 To MaxModules - 1
                gFrmMain.lblModuleStatus(i).Top = gFrmMain.lblModuleStatus(i - 1).Top + 48
                gFrmMain.lblModuleStatus(i).Enabled = False
                gFrmMain.lblModuleStatus(i).Visible = True
                gFrmMain.lblModuleStatus(i).Left = gFrmMain.lblModuleStatus_1.Left

                gFrmMain.cmdModule(i).Top = gFrmMain.cmdModule(i - 1).Top + 48
                gFrmMain.cmdModule(i).Enabled = False
                gFrmMain.cmdModule(i).Visible = True
                gFrmMain.cmdModule(i).Left = gFrmMain.cmdModule_1.Left

                gFrmMain.lblModuleName(i).Top = gFrmMain.lblModuleName(i - 1).Top + 48
                gFrmMain.lblModuleName(i).Text = ModuleNames(i)
                gFrmMain.lblModuleName(i).Enabled = False
                gFrmMain.lblModuleName(i).Visible = True
                gFrmMain.lblModuleName(i).Left = gFrmMain.lblModuleName_1.Left

                gFrmMain.lblModuleRunTime(i).Top = gFrmMain.lblModuleRunTime(i - 1).Top + 48
                If ModuleRunTime(i - 1) > 60 Then
                    gFrmMain.lblModuleRunTime(i).Text = (ModuleRunTime(i - 1) / 60).ToString("##0.00") & " Min."
                Else
                    gFrmMain.lblModuleRunTime(i).Text = ModuleRunTime(i - 1) & " Sec."
                End If
                gFrmMain.lblModuleRunTime(i).Enabled = False
                gFrmMain.lblModuleRunTime(i).Visible = True
                gFrmMain.lblModuleRunTime(i).Left = gFrmMain.lblModuleRunTime_1.Left
            Next i
            bModuleMenuBuilt = True
        End If

        'Enable PWR_On Test if STTO Passes
        If gFrmMain.lblSTTOStatus.Text = "PASSED" Then
            gFrmMain.lblPwrOn.Enabled = True
            gFrmMain.cmdPwrOnModule.Enabled = True
            gFrmMain.lblPwrOnStatus.Enabled = True
        Else 'Disable if STTO
            gFrmMain.lblPwrOn.Enabled = False
            gFrmMain.cmdPwrOnModule.Enabled = False
            gFrmMain.lblPwrOnStatus.Enabled = False
        End If

        'Enable Module Menu items if PWR_On Passes
        If gFrmMain.lblPwrOnStatus.Text = "PASSED" Then
            For i = 1 To MaxModules - 1
                gFrmMain.lblModuleStatus(i).Enabled = True
                gFrmMain.cmdModule(i).Enabled = True
                gFrmMain.lblModuleName(i).Enabled = True
                gFrmMain.lblModuleRunTime(i).Enabled = True
            Next i
        Else 'Disable if Failed
            For i = 1 To MaxModules - 1
                gFrmMain.lblModuleStatus(i).Enabled = False
                gFrmMain.cmdModule(i).Enabled = False
                gFrmMain.lblModuleName(i).Enabled = False
                gFrmMain.lblModuleRunTime(i).Enabled = False
            Next i
        End If

        'Enable all If TPS Development
        If bTPSDevelopment Then
            gFrmMain.lblPwrOn.Enabled = True
            gFrmMain.cmdPwrOnModule.Enabled = True
            gFrmMain.lblPwrOnStatus.Enabled = True
            For i = 1 To MaxModules - 1
                gFrmMain.lblModuleStatus(i).Enabled = True
                gFrmMain.cmdModule(i).Enabled = True
                gFrmMain.lblModuleName(i).Enabled = True
                gFrmMain.lblModuleRunTime(i).Enabled = True
            Next i
        End If

        If bFirstRun Then
            ClearModuleStatus()
            gFrmMain.NewUUT.Visible = False
            bFirstRun = False
        Else
            gFrmMain.NewUUT.Visible = True
            gFrmMain.NewUUT.Enabled = True
        End If

        If ProgramPath = " " Then
            MsgBox("Test Program Path not specified. Set the TPPath variable.", 16, "Shell Error - Fatal")
        End If
    End Sub

    Sub ClearTestMeasurementInformation()
        gFrmMain.txtModule.Text = ""
        gFrmMain.txtTestName.Text = ""
        gFrmMain.txtStep.Text = ""
        gFrmMain.txtUpperLimit.Text = ""
        gFrmMain.txtLowerLimit.Text = ""
        gFrmMain.txtUnit.Text = ""
        gFrmMain.txtInstrument.Text = ""
        gFrmMain.txtCommand.Text = ""
        gFrmMain.txtMeasured.Text = ""
        gFrmMain.txtMeasured.BackColor = System.Drawing.Color.White
        gFrmMain.txtMeasuredBig.Text = ""
        gFrmMain.txtMeasuredBig.BackColor = System.Drawing.Color.White
    End Sub

    Public Function sUserInput(ByRef sTitle As String, ByRef nTextJustification As System.Drawing.ContentAlignment, ByRef sLinesOfText() As String) As String
        Dim i As Short
        Dim nNumberoflines As Integer

        Dim sTemp As String
        Dim bCurrentState As Boolean
        Dim bContinueState As Boolean
        Dim bQuitState As Boolean
        Dim bMainMenuState As Boolean

        sTemp = gFrmMain.lblStatus.Text
        bCurrentState = gFrmMain.Timer2.Enabled
        bContinueState = gFrmMain.cmdContinue.Enabled
        bQuitState = gFrmMain.Quit.Enabled
        bMainMenuState = gFrmMain.MainMenuButton.Enabled

        gFrmMain.cmdContinue.Enabled = False

        gFrmMain.Timer1.Enabled = True
        gFrmMain.Timer2.Enabled = False
        gFrmMain.lblStatus.Text = "Waiting for User ..."

        nNumberoflines = UBound(sLinesOfText)

        gFrmOperatorMsg.lblInput.Visible = True
        gFrmOperatorMsg.InputData.Text = ""
        gFrmOperatorMsg.InputData.Visible = True
        gFrmOperatorMsg.Width = 569
        gFrmOperatorMsg.SSPanel1.Width = 8175
        gFrmOperatorMsg.msgInBox.Width = 505
        gFrmOperatorMsg.lblMsg_1.Width = 481
        gFrmOperatorMsg.lblMsg_1.Left = 8

        If nNumberoflines > 20 Then
            'Set limit on Form Size
            gFrmOperatorMsg.Height = 443
        Else
            'Resize Form
            gFrmOperatorMsg.Height = 80 + (nNumberoflines * 16)
        End If
        gFrmOperatorMsg.msgInBox.AutoScrollMinSize = New Size(gFrmOperatorMsg.msgInBox.AutoScrollMinSize.Width, gFrmOperatorMsg.msgInBox.Height - 20)

        'Build Form
        gFrmOperatorMsg.lblMsg_1.TextAlign = nTextJustification
        gFrmOperatorMsg.lblMsg_1.Text = sLinesOfText(1)

        For i = 2 To nNumberoflines
            gFrmOperatorMsg.lblMsg(i).Top = gFrmOperatorMsg.lblMsg(i - 1).Top + 16
            gFrmOperatorMsg.lblMsg(i).Text = sLinesOfText(i)
            gFrmOperatorMsg.lblMsg(i).Visible = True
        Next i

        gFrmOperatorMsg.Text = sTitle
        gFrmOperatorMsg.InputData.Focus()
        CenterForm(gFrmOperatorMsg)
        gFrmOperatorMsg.ShowDialog()
        gFrmMain.Refresh()

        sUserInput = UCase(UserInputData)

        gFrmMain.lblStatus.Text = sTemp
        gFrmMain.Timer2.Enabled = bCurrentState

        gFrmMain.Timer1.Enabled = False

        sTemp = CStr(gFrmMain.lblStatus.Text = sTemp)
        gFrmMain.Timer2.Enabled = bCurrentState
        gFrmMain.cmdContinue.Enabled = bContinueState
        gFrmMain.Quit.Enabled = bQuitState
        gFrmMain.MainMenuButton.Enabled = bMainMenuState
    End Function

    Sub ShowTestDocumentation()
        Dim opt As Short

        gFrmMain.Timer1.Enabled = True
        gFrmMain.Timer2.Enabled = False
        gFrmMain.fraInstrument.Visible = False

        gFrmMain.MainMenu.Visible = True
        gFrmMain.picTestDocumentation.Visible = True
        gFrmMain.cboAssembly.Visible = False
        gFrmMain.cboPartsList.Visible = False
        gFrmMain.cboSchematic.Visible = False

        gFrmMain.ModuleMenu.Visible = False
        gFrmMain.MainMenuButton.Enabled = True
        For opt = 7 To MAX_MENU_OPT
            gFrmMain.MenuOption(opt).Visible = True
            '        frmmain.MenuOption(opt).Enabled = True
            gFrmMain.MenuOptionText(opt).Visible = True
            '        frmmain.MenuOptionText(opt).Enabled = True
        Next opt

        gFrmMain.pinp.Image = Nothing
        gFrmMain.pinp.Visible = False

        ImageViewerChoice = False 'Required for HELP function to work properly
        gFrmMain.PictureWindow.Visible = False
        gFrmMain.TextWindow.Visible = False
        gFrmMain.SeqTextWindow.Visible = False
        DisableProgramControl()
        gFrmMain.AbortButton.Visible = True
        gFrmMain.RerunButton.Visible = False
        gFrmMain.PrintButton.Visible = True
        gFrmMain.PrintButton.Enabled = False
        gFrmMain.cmdContinue.Enabled = False
        gFrmMain.Quit.Enabled = True
        gFrmMain.Text = "Test Program Set: " & UUTPartNo
        gFrmMain.cmdFHDB.Enabled = False
        gFrmMain.cmdFHDB.Visible = False

        HelpKey = "!MAIN MENU"
        gFrmMain.TextWindow.Text = ""

        If ProgramPath = " " Then
            MsgBox("Test Program Path not specified. Set the TPPath variable.", 16, "Shell Error - Fatal")
        End If

        For opt = 7 To MAX_MENU_OPT
            Select Case -opt
                Case VIEW_ID_SCHEMATIC 'Enable VIEW ITA SCHEMATIC BUTTON if schematics are available
                    If UCase(sIDSchemFileNames(1)) = "NONE" Then
                        gFrmMain.MenuOption(opt).Enabled = False
                        gFrmMain.MenuOptionText(opt).Enabled = False
                        gFrmMain.MenuOptionText(opt).ForeColor = System.Drawing.ColorTranslator.FromOle(&H808080)
                        gFrmMain.cboITAWiring.Visible = False
                    Else
                        gFrmMain.MenuOption(opt).Enabled = True
                        gFrmMain.MenuOptionText(opt).Enabled = True
                        gFrmMain.MenuOptionText(opt).ForeColor = System.Drawing.Color.Black
                        gFrmMain.cboITAWiring.Visible = True
                    End If

                Case VIEW_ID_ASSEMBLY 'Enable VIEW ASSEMBLY BUTTON if Assembly Drawinh is available
                    If UCase(sIDAssyFileNames(1)) = "NONE" Then
                        gFrmMain.MenuOption(opt).Enabled = False
                        gFrmMain.MenuOptionText(opt).Enabled = False
                        gFrmMain.MenuOptionText(opt).ForeColor = System.Drawing.ColorTranslator.FromOle(&H808080)
                        gFrmMain.cboITAAssy.Visible = False
                    Else
                        gFrmMain.MenuOption(opt).Enabled = True
                        gFrmMain.MenuOptionText(opt).Enabled = True
                        gFrmMain.MenuOptionText(opt).ForeColor = System.Drawing.Color.Black
                        gFrmMain.cboITAAssy.Visible = True
                    End If

                Case VIEW_ID_PARTSLIST 'Enable VIEW PARTS LIST BUTTON if Parts List is available
                    If UCase(sIDPartsListFileNames(1)) = "NONE" Then
                        gFrmMain.MenuOption(opt).Enabled = False
                        gFrmMain.MenuOptionText(opt).Enabled = False
                        gFrmMain.MenuOptionText(opt).ForeColor = System.Drawing.ColorTranslator.FromOle(&H808080)
                        gFrmMain.cboITAPartsList.Visible = False
                    Else
                        gFrmMain.MenuOption(opt).Enabled = True
                        gFrmMain.MenuOptionText(opt).Enabled = True
                        gFrmMain.MenuOptionText(opt).ForeColor = System.Drawing.Color.Black
                        gFrmMain.cboITAPartsList.Visible = False
                    End If

                Case VIEW_TSR 'Enable VIEW PARTS LIST BUTTON if Parts List is available
                    If UCase(sTSRFileName) = "NONE" Then
                        gFrmMain.MenuOption(opt).Enabled = False
                        gFrmMain.MenuOptionText(opt).Enabled = False
                        gFrmMain.MenuOptionText(opt).ForeColor = System.Drawing.ColorTranslator.FromOle(&H808080)
                    Else
                        gFrmMain.MenuOption(opt).Enabled = True
                        gFrmMain.MenuOptionText(opt).Enabled = True
                        gFrmMain.MenuOptionText(opt).ForeColor = System.Drawing.Color.Black
                    End If

                Case VIEW_ELTD 'Enable VIEW PARTS LIST BUTTON if Parts List is available
                    If UCase(sELTDFileName) = "NONE" Then
                        gFrmMain.MenuOption(opt).Enabled = False
                        gFrmMain.MenuOptionText(opt).Enabled = False
                        gFrmMain.MenuOptionText(opt).ForeColor = System.Drawing.ColorTranslator.FromOle(&H808080)
                    Else
                        gFrmMain.MenuOption(opt).Enabled = True
                        gFrmMain.MenuOptionText(opt).Enabled = True
                        gFrmMain.MenuOptionText(opt).ForeColor = System.Drawing.Color.Black
                    End If

                Case VIEW_GENERAL_INFO 'Enable VIEW INFO BUTTON
                    If UCase(sInfoFileName) = "NONE" Then
                        gFrmMain.MenuOption(opt).Enabled = False
                        gFrmMain.MenuOptionText(opt).Enabled = False
                        gFrmMain.MenuOptionText(opt).ForeColor = System.Drawing.ColorTranslator.FromOle(&H808080)
                    Else
                        gFrmMain.MenuOption(opt).Enabled = True
                        gFrmMain.MenuOptionText(opt).Enabled = True
                        gFrmMain.MenuOptionText(opt).ForeColor = System.Drawing.Color.Black
                    End If
            End Select
        Next opt

        CenterForm(gFrmMain)
        gFrmMain.Text = "Test Program Set: " & UUTPartNo & " Version: " & TPVersion
        gFrmMain.Show()
    End Sub

    Sub ClearModuleStatus()
        Dim iCount As Short

        gFrmMain.lblEndToEndStatus.Text = "Unknown"
        gFrmMain.lblEndToEndStatus.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFC0)
        gFrmMain.lblEndToEndStatus.ForeColor = System.Drawing.ColorTranslator.FromOle(&HC00000)

        gFrmMain.lblSTTOStatus.Text = "Unknown"
        gFrmMain.lblSTTOStatus.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFC0)
        gFrmMain.lblSTTOStatus.ForeColor = System.Drawing.ColorTranslator.FromOle(&HC00000)

        gFrmMain.lblPwrOnStatus.Text = "Unknown"
        gFrmMain.lblPwrOnStatus.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFC0)
        gFrmMain.lblPwrOnStatus.ForeColor = System.Drawing.ColorTranslator.FromOle(&HC00000)

        For iCount = 1 To MaxModules - 1
            gFrmMain.lblModuleStatus(iCount).Text = "Unknown"
            gFrmMain.lblModuleStatus(iCount).BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFC0)
            gFrmMain.lblModuleStatus(iCount).ForeColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        Next iCount
    End Sub

    Public Function ConvertHexToBin(ByRef HexChars As String) As String
        Dim Conv As String
        Dim sTemp As String
        Dim i As Short
        Dim nSize As Integer

        nSize = Len(HexChars)
        If nSize < 5 Then
            Conv = ""
            For i = 1 To nSize
                sTemp = Mid(HexChars, i, 1)
                Select Case UCase(sTemp)
                    Case "0"
                        Conv = Conv & "0000"
                    Case "1"
                        Conv = Conv & "0001"
                    Case "2"
                        Conv = Conv & "0010"
                    Case "3"
                        Conv = Conv & "0011"
                    Case "4"
                        Conv = Conv & "0100"
                    Case "5"
                        Conv = Conv & "0101"
                    Case "6"
                        Conv = Conv & "0110"
                    Case "7"
                        Conv = Conv & "0111"
                    Case "8"
                        Conv = Conv & "1000"
                    Case "9"
                        Conv = Conv & "1001"
                    Case "A"
                        Conv = Conv & "1010"
                    Case "B"
                        Conv = Conv & "1011"
                    Case "C"
                        Conv = Conv & "1100"
                    Case "D"
                        Conv = Conv & "1101"
                    Case "E"
                        Conv = Conv & "1110"
                    Case "F"
                        Conv = Conv & "1111"
                End Select
            Next i
            If bSimulation = False Then
                Conv = Right(Conv, nNumberOfChannelPins)
            Else
                Conv = Right(Conv, nSize * 4)
            End If
        Else
            Conv = "h" & HexChars
        End If
        ConvertHexToBin = Conv
    End Function

    Sub ShowSplashScreen(Optional ByRef bAboutButton As Boolean = False)
        gFrmSplash.lblWeaponSystem.Text = WeaponSystem
        gFrmSplash.lblWeaponSystemNomenclature.Text = WeaponSystemNomenclature
        gFrmSplash.lblCompany.Text = DevelopedBy
        gFrmSplash.lblAddress.Text = address
        If bAboutButton Then
            gFrmSplash.SplashTimer.Enabled = False
            gFrmSplash.cmdOk.Enabled = True
            gFrmSplash.cmdOk.Visible = True
        Else
            gFrmSplash.SplashTimer.Enabled = True
            gFrmSplash.cmdOk.Enabled = False
            gFrmSplash.cmdOk.Visible = False
        End If
        gFrmSplash.ShowDialog()
    End Sub

    Function RunMyApp(ByRef sStartMode As String, Optional ByRef sAppToBeUsed As String = "NOTEPAD", Optional ByRef sFilePathName As String = "C:\aps\data\fault-file") As Boolean
        Dim structProcess As PROCESS_INFORMATION
        Dim structStartInfo As STARTUPINFO = New STARTUPINFO()
        Dim lngReturn As Integer
        Dim sCreateForMe As String
        Dim iStartMode As Short

        On Error GoTo ProcessError

        'Check for existing file first
        '------------------------------------------
        'lngReturn = FreeFile
        'Open sFilePathName For Input As lngReturn
        'Close lngReturn
        '------------------------------------------

        If sStartMode = "SW_MINIMIZE" Then
            iStartMode = SW_MINIMIZE
        ElseIf sStartMode = "SW_MAXIMIZE" Then
            iStartMode = SW_MAXIMIZE
        ElseIf sStartMode = "SW_NORMAL" Then
            iStartMode = SW_NORMAL
        ElseIf sStartMode = "SW_HIDE" Then
            iStartMode = SW_HIDE
        End If

        'Build string
        sCreateForMe = sAppToBeUsed & " " & sFilePathName

        'Intialize the info structure
        structStartInfo.cb = Len(structStartInfo)
        structStartInfo.dwFlags = STARTF_USESHOWWINDOW
        structStartInfo.wShowWindow = iStartMode

        'Create the process
        lngReturn = CreateProcessA(0, sCreateForMe, 0, 0, 1, NORMAL_PRIORITY_CLASS, 0, 0, structStartInfo, structProcess)

        RunMyApp = True

        Exit Function

ProcessError:

        RunMyApp = False

        If Err.Number = 53 Then
            MsgBox("Error: " & CStr(Err.Number) & vbCrLf & Err.Description & "< " & sFilePathName & " >", MsgBoxStyle.Critical)
        Else
            MsgBox("Error: " & CStr(Err.Number) & vbCrLf & Err.Description, MsgBoxStyle.Critical)
        End If
    End Function

    ''' <summary>
    ''' This Routine will return a NULL string deliniated list of logical Compact Disc Drives on the system
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetCompactDiscDrives() As String
        'This Routine was added due to an IPR Customer Request for a
        'TPS Menu System DataBase ID#104
        Const nBufferLength As Integer = 255 'Length of API Return Buffer
        Dim ReturnCode As Integer 'API Error Code
        Dim lpBuffer As String = Space(nBufferLength) 'API Return String Buffer
        Dim DriveList As String 'Logical Drive Buffer Formatted for Visual Basic String Type
        Dim StringLocation As Short 'Counter/Pointer for String Parsing Loop
        Dim LogicalDrive As String 'Root Drive Description to test for Type
        Dim ReturnDriveList As String = "" 'String that is returned

        'Find All Logical Drives on System
        ReturnCode = GetLogicalDriveStrings(nBufferLength, lpBuffer)
        DriveList = Trim(lpBuffer)

        'Check all logical drives for drive type
        For StringLocation = 1 To Len(DriveList) Step 4
            LogicalDrive = Mid(DriveList, StringLocation, 4)
            ReturnCode = GetDriveType(LogicalDrive)
            'Check if the Drive is a CDROM drive
            If ReturnCode = DRIVE_CDROM Then
                'RAMDISK = 6, 'CDROM = 5,'REMOTE = 4,'FIXED = 3, 'REMOVABLE = 2
                ReturnDriveList = ReturnDriveList & LogicalDrive
            End If
        Next StringLocation

        'Return function Value (CD Drives on System)
        GetCompactDiscDrives = Mid(ReturnDriveList, 1, 3) 'Strip Null
    End Function
End Module