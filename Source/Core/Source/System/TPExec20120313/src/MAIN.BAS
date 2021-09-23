Attribute VB_Name = "MAINMod"
Option Explicit
DefInt A-Z

Public Declare Function FindExecutable Lib "shell32.dll" Alias "FindExecutableA" (ByVal lpFile As String, ByVal lpDirectory As String, ByVal lpResult As String) As Long
Public Declare Function ShellExecute Lib "shell32.dll" Alias "ShellExecuteA" (ByVal hwnd As Long, ByVal lpOperation As String, ByVal lpFile As String, ByVal lpParameters As String, ByVal lpDirectory As String, ByVal nShowCmd As Long) As Long
Public Declare Sub Sleep Lib "kernel32" (ByVal dwMilliseconds As Long)
Public Declare Function CreateProcess Lib "kernel32" Alias "CreateProcessA" (ByVal lpApplicationName As String, ByVal lpCommandLine As String, lpProcessAttributes As SECURITY_ATTRIBUTES, lpThreadAttributes As SECURITY_ATTRIBUTES, ByVal bInheritHandles As Long, ByVal dwCreationFlags As Long, lpEnvironment As Any, ByVal lpCurrentDriectory As String, lpStartupInfo As STARTUPINFO, lpProcessInformation As PROCESS_INFORMATION) As Long
Public Declare Function CreateProcessA& Lib "kernel32" (ByVal lpApplicationName&, ByVal lpCommandLine$, ByVal lpProcessAttributes&, ByVal lpThreadAttributes&, ByVal bInheritHandles&, ByVal dwCreationFlags&, ByVal lpEnvironment&, ByVal lpCurrentDirectory&, lpStartupInfo As STARTUPINFO, lpProcessInformation As PROCESS_INFORMATION)
Public Declare Function WaitForSingleObject Lib "kernel32" (ByVal hHandle As Long, ByVal dwMilliseconds As Long) As Long
Public Declare Function CloseHandle Lib "kernel32" (ByVal hObject As Long) As Long
Declare Function GetSystemDirectory Lib "kernel32" Alias "GetSystemDirectoryA" (ByVal lpBuffer As String, ByVal nSize As Long) As Long
Declare Function GetWindowsDirectory Lib "kernel32" Alias "GetWindowsDirectoryA" (ByVal lpBuffer As String, ByVal nSize As Long) As Long
Declare Function GetPrivateProfileString Lib "kernel32" Alias "GetPrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As Any, ByVal lpDefault As String, ByVal lpReturnedString As String, ByVal nSize As Long, ByVal lpFileName As String) As Long
Declare Function WritePrivateProfileString Lib "kernel32" Alias "WritePrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As Any, ByVal lpString As Any, ByVal lpFileName As String) As Long
Declare Function DM_setMaxSeedingFaultSetsValue Lib "DiagMgr.dll" (ByVal value As Long) As Long
Declare Function GetWindow Lib "user32" (ByVal hwnd As Long, ByVal wCmd As Long) As Long
Declare Function GetWindowText Lib "user32" Alias "GetWindowTextA" (ByVal hwnd As Long, ByVal lpString As String, ByVal cch As Long) As Long
Declare Function viReadSTB Lib "VISA32.DLL" Alias "#259" (ByVal vi As Long, Status As Integer) As Long

'CDROM Search Functions
Declare Function GetLogicalDriveStrings Lib "kernel32" Alias "GetLogicalDriveStringsA" (ByVal nBufferLength As Long, ByVal lpBuffer As String) As Long
Declare Function GetDriveType Lib "kernel32" Alias "GetDriveTypeA" (ByVal nDrive As String) As Long
Declare Function GetVolumeInformation Lib "kernel32" Alias "GetVolumeInformationA" (ByVal lpRootPathName As String, ByVal lpVolumeNameBuffer As String, ByVal nVolumeNameSize As Long, lpVolumeSerialNumber As Long, lpMaximumComponentLength As Long, lpFileSystemFlags As Long, ByVal lpFileSystemNameBuffer As String, ByVal nFileSystemNameSize As Long) As Long
Public Const DRIVE_CDROM = 5
Public Const DRIVE_FIXED = 3
Public Const DRIVE_RAMDISK = 6
Public Const DRIVE_REMOTE = 4
Public Const DRIVE_REMOVABLE = 2

'Setup Createprocess function
Public Type STARTUPINFO
    cb As Long
    lpReserved As String
    lpDeskTop As String
    lpTitle As String
    dwX As Long
    dwY As Long
    dwXSize As Long
    dwYSize As Long
    dwXCountChars As Long
    dwYCountChars As Long
    dwFillAttribute As Long
    dwFlags As Long
    wShowWindow As Integer
    cbReserved2 As Integer
    lpReserved2 As Long
    hStdInput As Long
    hStdOutput As Long
    hStdError As Long
End Type

Public Type PROCESS_INFORMATION
    hProcess As Long
    hThread As Long
    dwProcessID As Long
    dwThreadID As Long
End Type

Public Type SECURITY_ATTRIBUTES
        nLength As Long
        lpSecurityDescriptor As Long
        bInheritHandle As Boolean
End Type

Public Const SW_HIDE = 0
Public Const SW_SHOWNORMAL = 1
Public Const SW_NORMAL = 1
Public Const SW_SHOWMINIMIZED = 2
Public Const SW_SHOWMAXIMIZED = 3
Public Const SW_MAXIMIZE = 3
Public Const SW_SHOWNOACTIVATE = 4
Public Const SW_SHOW = 5
Public Const SW_MINIMIZE = 6
Public Const SW_SHOWMINNOACTIVE = 7
Public Const SW_SHOWNA = 8
Public Const SW_RESTORE = 9
Public Const SW_SHOWDEFAULT = 10
Public Const SW_FORCEMINIMIZE = 11
Public Const SW_MAX = 11

Public Const STARTF_USESHOWWINDOW = &H1
Public Const INFINITE = -1&
Public Const NORMAL_PRIORITY_CLASS = &H20&
Public Const DIAGNOSTICS_DIRECTORY = "C:\APS\DATA"

'AProbe Use and Shut Down because of handle or green button
Dim ActChassisAddress As Integer
Global ActVolt28Ok As Integer
Dim ActProbeEvent As Integer
Dim ActReceiverEvent As Integer

Public Const GW_HWNDFIRST = 0   'API Constant for Changing a Window "Z-Order"
Public Const GW_HWNDLAST = 1    'API Constant for Changing a Window "Z-Order"
Public Const GW_HWNDNEXT = 2    'API Constant for Changing a Window "Z-Order"

Public bReceiverClosed As Boolean
Public bProbeClosed As Boolean
Public bReseting As Boolean

Global FormMainLoaded As Integer

' *** GLOBAL CONSTANTS ***

Global Const MODAL = 1
Global Const PANNING = 0
Global Const ZOOMING = 1
Global Const MAGNIFYING = 2
Global Const SCROLLING = 3
Global Const MAX_MENU_OPT = 11
Global Const NORMAL = 0    ' 0 - Normal
Global Const MINIMIZED = 1 ' 1 - Minimized
Global Const MAXIMIZED = 2 ' 2 - Maximized

Global Const RUNTP_BUTTON = -1
Global Const VIEW_SCHEMATIC = -2
Global Const VIEW_ASSEMBLY = -3
Global Const VIEW_PARTSLIST = -4
Global Const ID_SURVEY = -5
Global Const TPS_DOCUMENTATION = -6
Public Const VIEW_ID_SCHEMATIC = -7
Public Const VIEW_ID_ASSEMBLY = -8
Public Const VIEW_ID_PARTSLIST = -9
Public Const VIEW_TSR = -10
Public Const VIEW_ELTD = -11
Public Const VIEW_GENERAL_INFO = -12
Public Const VIEW_FAULT_FILE = -13
Public Const VIEW_IETM = -14

Global Const BACKUP_BUTTON = -14
Global Const RERUN_BUTTON = -15
Global Const MAINMENU_BUTTON = -16
Global Const ABORT_BUTTON = -17
Global Const HELP_BUTTON = -18
Global Const END_TO_END = -19
Global Const STTO = -20
Global Const PWR_ON = -21
Global Const QUIT_BUTTON = -22
Global Const CONTINUE_BUTTON = -23
Global Const YES_BUTTON = -24
Global Const NO_BUTTON = -25
Global Const DIAGNOSTIC_BUTTON = -26
Global Const USER_EVENT = 6000

Global Const DATA_BASE_NAME = "DATALOG.MDB"
Global Const DATA_BASE_TABLE = "LOGTBL"

Global Const TEST_PROGRAM = "TestProgram"
Global Const SYSTEM_LOG = "syslog.txt"

' *** LOCAL CONSTANTS ***
Const CCA_NOMENCLATURE = "CCA Nomenclature"
Const CCA_PART_NUMBER = "CCA Part Number"
Const CCA_SERIAL_NUMBER = "CCA Serial Number"
Const CCA_STATUS = "CCA Status"
Const CALL_OUT = "Call Out"
Const CURR_DATE = "Date"
Const TEST_FAILED = "Test Failed"
Const OPERATOR_NAME = "Operator ID"
Const TESTER_NUMBER = "Tester Serial Number"
Const TEST_NUMBER_FAILED = "Test Number Failed"
Const CURR_TIME = "Time"

' *** GLOBAL VARIABLES ***
Public iTime As Integer
Public bFirstRun As Boolean
Public bEndToEnd As Boolean
Public bHighVoltage As Boolean
Global TPSAppExe$
Global DDESendErr%
Global QuitButtonPressed%
Global ImageHandle&
Global Choice%
Global MouseAction As Integer
Global MagCursor As Integer
Global ReturnCode&
Global WindowCaption$
Global HelpKey$
Global Buttons$()
Global LOGFILE&
Global LogPath As String
Global Logging%
Global ConfigOK%
Global UserEvent%
Global TheMessage$
Global TestLog$
Global UserInputData As String
Global InputNumber As Integer
Global iAcknowledge As Integer
Global bModuleMenuBuilt As Boolean
Public Pass As Boolean
Public Failed As Boolean
Public OutHigh As Boolean
Public OutLow As Boolean
Public MisProbe As Integer
Public nNumberOfChannelPins As Long

Global SOF As Boolean
Global DataLoggingEnabled%
Global SysLogDB$
Global SysLogTbl$

Global MaxPartsListPages%
Global MaxIDPartsListPages%
Global MaxAssemblyPages%
Global MaxIDAssemblyPages%
Global MaxSchematicPages%
Global MaxIDSchematicPages%
Global MaxModules As Integer

Global TestProgramName As String
Global ProgramPath As String
Global TPSGraphics As String
Global SchemPageNum%
Global IDSchemPageNum%
Global ProbeDataFile As String
Global CRLF As String * 2
Global ScreenBuffer$(21)
Global Const MaxScreenLines = 21
Global ProbeReady%
Global ImageViewerChoice As Boolean
Public TPName As String
Global TPVersion$
Global TPNotice$
Global UUTPartNo$
Public UUTName As String
Global UUTSN$
Public UUTRev As String
Global IDPartNo$
Global IDSN$
Global TPType$

Global LEDButtonPressed As String
Global LEDUserResponse As Boolean
Global tempmsg As String
Global sTSRFileName As String
Global sELTDFileName As String
Global sInfoFileName As String
Global ModuleNames() As String
Global ModuleRunTime() As Long
Public DocumentsPath As String
Global sPartsListFileNames() As String
Global sIDPartsListFileNames() As String
Global MaxPartListPages%
Global PartListPageNum%
Global IDPartListPageNum%
Global PartListPath$
Global IDPartListPath$
Global sSchemFileNames() As String
Global sIDSchemFileNames() As String
Global SchemPath$
Global IDSchemPath$
Global sAssyFileNames() As String
Global sIDAssyFileNames() As String
Global MaxAssyPages%
Global AssyPageNum%
Global IDAssyPageNum%
Global AssyPath$
Global IETMPath$
Global IDAssyPath$
Global sImageFile As String
Public sProbeAssy As String
Public sProbeData As String
Public nFileError As Long
Public WindowsDirectory As String
Public SystemDir As String
Public VisaLibrary As String
Public lpBuffer As String * 256
Public PathTetsIni As String
Public PathSysLogExe As String
Public sDelimiter As String
Public NumElements As Integer
Public Button As Integer
Public bSystemInitialized As Boolean
Public bTPSDevelopment As Boolean
Public bSystemResetComplete As Boolean

Global ReadBuffer As String * 256

Public WeaponSystem As String
Public WeaponSystemNomenclature As String
Public DevelopedBy As String
Public address As String

'Define Data variables to pass to the FHDB
Public Type CurrentFailureRecord
    sSubName As String
    sTestNumber As String
    sPath As String
    sHighLimit As String
    sLowLimit As String
    sMeasured As String
    sUnit As String
End Type

'Variables used for FHDB
Public sStart As String
Public SStop As String
Public EROs As String
Public sTPCCN As String
Public sUUTSerial As String
Public sUUTRev As String
Public sIDSerial As String
Public nStatus As Long
Public sFailStep As String
Public sCallout As String
Public dMeasurement As Double
Public sUOM As String
Public duLimit As Double
Public dlLimit As Double
Public sComment As String

Public CurrentFailure As CurrentFailureRecord

'cicl stuff
Global LiveMode(40) As Integer
Global ResourceName(40) As String
Global PsResourceName(10) As String
Global nSystErr As Long

'RF stuff
Global s8481A_SN As String    'Serial Number of system HP8481A power sensor
Global s8481D_SN As String    'Serial Number of system HP8481D power sensor
Global s11708A_SN As String   'Serial Number of system 30dB attenuator
Global gRFMa As New RFMa_if   ' for RF measurement analyzer
Global gRFCt As New RFCt_if   ' for RF counter
Global gRFPM As New RFPm_if   ' for RF power meter

Public Sub Main()

    Err.Clear
    UserEvent = 0
    bModuleMenuBuilt = False
    frmMain.MenuOption(5).Picture = frmMain.ImageList1.ListImages(1).Picture
    bSystemInitialized = False
    bHighVoltage = False
    nInstrumentHandle(DIGITAL) = 0
    sIDSerial = ""
    sTSRFileName = ""
    sELTDFileName = ""
    sInfoFileName = ""
    bReseting = False

    LOGFILE = FreeFile
    On Error Resume Next
    Open LOG_FILE For Append As LOGFILE& Len = 10
    
    If Err Then
        MsgBox "Error opening " + LOG_FILE + " file for output.  Test results will not be logged.", 48, "Log file Error"
        Logging% = False
    Else
        Logging% = True
    End If
    
    Pass = True
    Failed = True
    OutHigh = True
    OutLow = True
    bEndToEnd = False

    Call InitVar
    Call InitCicl

End Sub

Sub InitVar()

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
    
    PsResourceName$(1) = "DCPS_1"
    PsResourceName$(2) = "DCPS_2"
    PsResourceName$(3) = "DCPS_3"
    PsResourceName$(4) = "DCPS_4"
    PsResourceName$(5) = "DCPS_5"
    PsResourceName$(6) = "DCPS_6"
    PsResourceName$(7) = "DCPS_7"
    PsResourceName$(8) = "DCPS_8"
    PsResourceName$(9) = "DCPS_9"
    PsResourceName$(10) = "DCPS_10"
 
End Sub

Sub InitCicl()

    Dim i
    Dim Status As Long
    Dim XmlBuf As String
    Dim Allocation As String
    Dim Response As String
    Dim Ret&
     
    Ret& = GetSystemDirectory(lpBuffer$, ByVal 80)
    SystemDir$ = StripNullCharacters(lpBuffer$)
    VisaLibrary$ = SystemDir$ & "\VISA32.DLL"
 
     If Not bFileExists(VisaLibrary$) Then
        Echo "Cannot find VISA Run-Time System. Unable to perform System Self Test."
        
    Else
     
      On Error Resume Next
      Status = atxml_Initialize(proctype, guid)
      Allocation = "C:\Program Files\VIPERT\ISS\Config\PawsAllocation.xml"
     
      Response = Space(4096)
      XmlBuf = "<AtXmlTestRequirements>" & _
               "<ResourceRequirement>" & _
               "  <ResourceType>Source</ResourceType>" & _
               "  <SignalResourceName>ARB_GEN_1</SignalResourceName> " & _
               "</ResourceRequirement> " & _
               "</AtXmlTestRequirements>"
      Status = atxml_ValidateRequirements(XmlBuf, Allocation, Response, 4096)
      If Status <> 0 Then
          MsgBox "The ARB is not responding.  Live mode is disabled.", vbExclamation
          LiveMode(ARB) = False
          i = MsgBox("The CiclKernelC.exe may not be running!" + vbCrLf + "Do you want to quit?", vbYesNo)
          If i = 6 Then
            UserEvent% = QUIT_BUTTON
            Exit Sub
          End If
          
      Else
          LiveMode(ARB) = True
      End If
            
      Response = Space(4096)
      XmlBuf = "<AtXmlTestRequirements>" & _
               "<ResourceRequirement>" & _
               "   <ResourceType>Source</ResourceType>" & _
               "   <SignalResourceName>CNTR_1</SignalResourceName> " & _
               "</ResourceRequirement> " & _
               "</AtXmlTestRequirements>" '
      Status = atxml_ValidateRequirements(XmlBuf, Allocation, Response, 4096)
      If Status <> 0 Then
          LiveMode(COUNTER) = False
          MsgBox "The Counter/Timer Is Not Responding.  Live Mode Disabled.", vbInformation
      Else
          LiveMode(COUNTER) = True
      End If
      
      Response = Space(4096)
      XmlBuf = "<AtXmlTestRequirements>" & _
               "<ResourceRequirement>" & _
               "   <ResourceType>Source</ResourceType>" & _
               "   <SignalResourceName>DMM_1</SignalResourceName> " & _
               "</ResourceRequirement> " & _
               "</AtXmlTestRequirements>" '
      Status = atxml_ValidateRequirements(XmlBuf, Allocation, Response, 4096)
      If Status <> 0 Then
          LiveMode(DMM) = False
          MsgBox "The DMM Is Not Responding.  Live Mode Disabled.", vbInformation
      Else
          LiveMode(DMM) = True
      End If
      
      Response = Space(4096)
      XmlBuf = "<AtXmlTestRequirements>" & _
               "<ResourceRequirement>" & _
               "  <ResourceType>Source</ResourceType>" & _
               "  <SignalResourceName>DSO_1</SignalResourceName> " & _
               "</ResourceRequirement>" & _
               "</AtXmlTestRequirements>"
      Status& = atxml_ValidateRequirements(XmlBuf, Allocation, Response, 4096)
      If Status <> 0 Then
          LiveMode(OSCOPE) = False
          MsgBox "The DSO Is Not Responding.  Live Mode Disabled.", vbInformation
      Else
          LiveMode(OSCOPE) = True
      End If
      
      Response = Space(4096)
      XmlBuf = "<AtXmlTestRequirements>" & _
               "<ResourceRequirement>" & _
               "   <ResourceType>Source</ResourceType>" & _
               "   <SignalResourceName>FUNC_GEN_1</SignalResourceName> " & _
               "</ResourceRequirement> " & _
               "</AtXmlTestRequirements>"
      Status = atxml_ValidateRequirements(XmlBuf, Allocation, Response, 4096)
      If Status <> 0 Then
          LiveMode(FGEN) = False
          MsgBox "The Function Generator is not responding.  Live mode is disabled.", vbExclamation
      Else
          LiveMode(FGEN) = True
      End If
      
      Response = Space(4096)
      XmlBuf = "<AtXmlTestRequirements>"
      XmlBuf = XmlBuf & _
         "<ResourceRequirement> " & _
         "  <ResourceType>Source</ResourceType> " & _
         "  <SignalResourceName>DCPS_40V5A_1</SignalResourceName> " & _
         "</ResourceRequirement> "
      XmlBuf = XmlBuf & _
         "<ResourceRequirement> " & _
         "  <ResourceType>Source</ResourceType> " & _
         "  <SignalResourceName>DCPS_40V5A_2</SignalResourceName> " & _
         "</ResourceRequirement>"
      XmlBuf = XmlBuf & _
         "<ResourceRequirement> " & _
         "  <ResourceType>Source</ResourceType> " & _
         "  <SignalResourceName>DCPS_40V5A_3</SignalResourceName> " & _
         "</ResourceRequirement>"
      XmlBuf = XmlBuf & _
         "<ResourceRequirement> " & _
         "  <ResourceType>Source</ResourceType> " & _
         "  <SignalResourceName>DCPS_40V5A_4</SignalResourceName> " & _
         "</ResourceRequirement>"
      XmlBuf = XmlBuf & _
         "<ResourceRequirement> " & _
         "  <ResourceType>Source</ResourceType> " & _
         "  <SignalResourceName>DCPS_40V5A_5</SignalResourceName> " & _
         "</ResourceRequirement>"
      XmlBuf = XmlBuf & _
         "<ResourceRequirement> " & _
         "  <ResourceType>Source</ResourceType> " & _
         "  <SignalResourceName>DCPS_40V5A_6</SignalResourceName> " & _
         "</ResourceRequirement>"
      XmlBuf = XmlBuf & _
         "<ResourceRequirement> " & _
         "  <ResourceType>Source</ResourceType> " & _
         "  <SignalResourceName>DCPS_40V5A_7</SignalResourceName> " & _
         "</ResourceRequirement>"
      XmlBuf = XmlBuf & _
         "<ResourceRequirement> " & _
         "  <ResourceType>Source</ResourceType> " & _
         "  <SignalResourceName>DCPS_40V5A_8</SignalResourceName> " & _
         "</ResourceRequirement>"
      XmlBuf = XmlBuf & _
         "<ResourceRequirement> " & _
         "  <ResourceType>Source</ResourceType> " & _
         "  <SignalResourceName>DCPS_40V5A_9</SignalResourceName> " & _
         "</ResourceRequirement>"
      XmlBuf = XmlBuf & _
         "<ResourceRequirement> " & _
         "  <ResourceType>Source</ResourceType> " & _
         "  <SignalResourceName>DCPS_65V5A_10</SignalResourceName> " & _
         "</ResourceRequirement>"
      XmlBuf = XmlBuf & "</AtXmlTestRequirements>"
      Status = atxml_ValidateRequirements(XmlBuf, Allocation, Response, Len(XmlBuf))
      If Status Then
          LiveMode(PPU) = False
          MsgBox "The PPU is not responding.  Live mode is disabled.", vbExclamation
      Else
          LiveMode(PPU) = True
      End If
      
      Response = Space(4096)
      XmlBuf = "<AtXmlTestRequirements> " & _
               "<ResourceRequirement> " & _
               "   <ResourceType>Source</ResourceType> " & _
               "   <SignalResourceName>PAWS_SWITCH</SignalResourceName> " & _
               "</ResourceRequirement> " & _
               "</AtXmlTestRequirements>"
      Status = atxml_ValidateRequirements(XmlBuf, Allocation, Response, 4096)
      If Status = conNoDLL Then
        MsgBox "Error in loading RI1260_32.DLL.  Switch Live mode is disabled.", vbInformation
        LiveMode(SWITCH1) = False
      ElseIf Status <> 0 Then
        MsgBox "The Switch Module is not responting.  Live mode is disabled.", vbInformation
        LiveMode(SWITCH1) = False
      Else
        LiveMode(SWITCH1) = True
      End If
      
      Response = Space(4096)
      XmlBuf = "<AtXmlTestRequirements> " & _
               "<ResourceRequirement> " & _
               "   <ResourceType>Source</ResourceType> " & _
               "   <SignalResourceName>RFGEN_1</SignalResourceName> " & _
               "</ResourceRequirement> " & _
               "</AtXmlTestRequirements>"
      Response = Space(4096)
      Status = atxml_ValidateRequirements(XmlBuf, Allocation, Response, 4096)
      If Status <> 0 Then
        MsgBox "The RF Stimulus is not responding.  Live mode is disabled.", vbExclamation
        LiveMode(RFSYN) = False
      Else
        LiveMode(RFSYN) = True
      End If
      
      Response = Space(4096)
      XmlBuf = "<AtXmlTestRequirements>" & _
               "<ResourceRequirement>" & _
               "  <ResourceType>Source</ResourceType>" & _
               "  <SignalResourceName>COM_1</SignalResourceName> " & _
               "  </ResourceRequirement> " & _
               "<ResourceRequirement>" & _
               "  <ResourceType>Source</ResourceType>" & _
               "  <SignalResourceName>COM_2</SignalResourceName> " & _
               "  </ResourceRequirement> " & _
               "<ResourceRequirement>" & _
               "  <ResourceType>Source</ResourceType>" & _
               "  <SignalResourceName>ETHERNET_1</SignalResourceName> " & _
               "  </ResourceRequirement> " & _
               "<ResourceRequirement>" & _
               "  <ResourceType>Source</ResourceType>" & _
               "  <SignalResourceName>ETHERNET_2</SignalResourceName> " & _
               "  </ResourceRequirement> " & _
              "</AtXmlTestRequirements>"
      Status = atxml_ValidateRequirements(XmlBuf, Allocation, Response, 4096)
      
      If Status = conNoDLL Then
          MsgBox "Error in loading Communication Bus Driver.", vbInformation
          LiveMode(RS232) = False
      ElseIf Status <> 0 Then
          MsgBox "The Bus is not responding.", vbExclamation
          LiveMode(RS232) = False
      Else
          LiveMode(RS232) = True
      End If

      Response = Space(4096)
      XmlBuf = "<AtXmlTestRequirements>"
      XmlBuf = XmlBuf & _
         "<ResourceRequirement> " & _
         "  <ResourceType>Source</ResourceType> " & _
         "  <SignalResourceName>SRS_1_DS1</SignalResourceName> " & _
         "</ResourceRequirement> "
      XmlBuf = XmlBuf & _
         "<ResourceRequirement> " & _
         "  <ResourceType>Source</ResourceType> " & _
         "  <SignalResourceName>SRS_1_DS2</SignalResourceName> " & _
         "</ResourceRequirement>"
      XmlBuf = XmlBuf & _
         "<ResourceRequirement> " & _
         "  <ResourceType>Source</ResourceType> " & _
         "  <SignalResourceName>SRS_1_SD1</SignalResourceName> " & _
         "</ResourceRequirement>"
      XmlBuf = XmlBuf & _
         "<ResourceRequirement> " & _
         "  <ResourceType>Source</ResourceType> " & _
         "  <SignalResourceName>SRS_1_SD2</SignalResourceName> " & _
         "</ResourceRequirement>"
      XmlBuf = XmlBuf & "</AtXmlTestRequirements>"

      Status = atxml_ValidateRequirements(XmlBuf, Allocation, Response, 4096)
      If Status <> 0 Then
        MsgBox "The Syncro/Resolver is not responding.  Live mode is disabled.", vbExclamation
        LiveMode(SYN_RES) = False
      Else
        LiveMode(SYN_RES) = True
      End If
      Delay 0.5

  End If

End Sub

Public Sub CenterForm(frmForm As Object)
'************************************************************
'* ManTech Test Systems Software Module                     *
'************************************************************
'*    DESCRIPTION:                                          *
'*     This Module Centers One Form With Respect To The     *
'*     User's Screen.                                       *
'*    EXAMPLE:                                              *
'*     CenterForm frmMain                                   *
'************************************************************

     frmForm.Top = Screen.Height / 2 - frmForm.Height / 2
     frmForm.Left = Screen.Width / 2 - frmForm.Width / 2
     
End Sub

Function StringToList%(strng$, List$(), delimiter$)

    'DESCRIPTION:
    ' Procedure to convert a delimited string into a list array
    'Parameters:
    ' strng$     : String to be converted.
    ' list$()    : Array in which to return list of strings
    ' Delimiter$ : Char array of valid delimiters.
    'Returns:
    ' Number of items in list
    ' Returns -1 if number of elements exceeds
    ' upper bound of passed array
    
    Dim numels%
    Dim inflag%
    Dim slength%
    Dim ch%
    Dim Char$
    
    numels% = 0
    inflag% = 0
    Erase List$

    'Go through parsed string a character at a time.
    slength% = Len(strng$)
    
    For ch% = 1 To slength%
        Char$ = Mid$(strng$, ch%, 1)
        
        If InStr(delimiter$, Char$) = 0 Then
        
            If Not inflag% Then
                
                If numels% = UBound(List$) Then
                    StringToList% = -1
                    Exit For
                End If
                
                numels% = numels% + 1
                inflag% = -1
            End If
            
            List$(numels%) = List$(numels%) + Char$
        Else
            'Found a delimiter.
            'Set "Not in element" flag to FALSE.
            inflag% = 0
        End If
    Next ch%
    
    StringToList% = numels%
    
End Function

Sub EnableImageControl(index As Integer)
    
    Dim sfunction As String
    
    'Hide all test status indicators
    Unload frmImage
    MouseAction = ZOOMING
    
    frmImage.MouseControl(PANNING).Visible = True
    frmImage.MouseControl(ZOOMING).Visible = True
    frmImage.MouseControl(MAGNIFYING).Visible = True
    frmImage.ResetView.Visible = True
    frmImage.PPage.Visible = True
    frmImage.NPage.Visible = True
    frmImage.ZoomOut50.Visible = True
    frmImage.MouseControl(PANNING).Enabled = True
    frmImage.MouseControl(ZOOMING).Enabled = True
    frmImage.MouseControl(MAGNIFYING).Enabled = True
    frmImage.ZoomOut50.Enabled = True
    frmImage.ResetView.Enabled = True
    frmImage.PrintButton.Enabled = True
    frmImage.PrintButton.Caption = "Print Image"
    frmImage.ImageControlPanel.Visible = True
    frmImage.Quit.Visible = True
    frmImage.Quit.Enabled = True

    Select Case index
    
        Case VIEW_SCHEMATIC 'Schematic View
        
            If MaxSchematicPages% = 1 Then
                frmImage.PPage.Enabled = False
                frmImage.NPage.Enabled = False
                Exit Sub
            End If
            
            Select Case SchemPageNum%
            
                Case Is = 1
                    frmImage.PPage.Enabled = False
                    
                    If SchemPageNum% + 1 > MaxSchematicPages% Then
                        frmImage.NPage.Enabled = False
                    Else
                        frmImage.NPage.Enabled = True
                    End If
                    
                Case Is > 1
                    frmImage.PPage.Enabled = True
                    
                    If SchemPageNum% + 1 > MaxSchematicPages% Then
                        frmImage.NPage.Enabled = False
                    Else
                        frmImage.NPage.Enabled = True
                    End If
                    
            End Select
            
        Case VIEW_ID_SCHEMATIC 'ITA Schematic View
        
            If MaxIDSchematicPages% = 1 Then
                frmImage.PPage.Enabled = False
                frmImage.NPage.Enabled = False
                Exit Sub
            End If
            
            Select Case IDSchemPageNum%
            
                Case Is = 1
                    frmImage.PPage.Enabled = False
                    
                    If IDSchemPageNum% + 1 > MaxIDSchematicPages% Then
                        frmImage.NPage.Enabled = False
                    Else
                        frmImage.NPage.Enabled = True
                    End If
                    
                Case Is > 1
                    frmImage.PPage.Enabled = True
                    
                    If IDSchemPageNum% + 1 > MaxIDSchematicPages% Then
                        frmImage.NPage.Enabled = False
                    Else
                        frmImage.NPage.Enabled = True
                    End If
                    
            End Select
        
        Case VIEW_ASSEMBLY 'Assembly View
        
            If MaxAssemblyPages% = 1 Then
                frmImage.PPage.Enabled = False
                frmImage.NPage.Enabled = False
                Exit Sub
            End If
            
            Select Case AssyPageNum%
            
                Case Is = 1
                    frmImage.PPage.Enabled = False
                    
                    If AssyPageNum% + 1 > MaxAssemblyPages% Then
                        frmImage.NPage.Enabled = False
                    Else
                        frmImage.NPage.Enabled = True
                    End If
                    
                Case Is > 1
                    frmImage.PPage.Enabled = True
                    
                    If AssyPageNum% + 1 > MaxAssemblyPages% Then
                        frmImage.NPage.Enabled = False
                    Else
                        frmImage.NPage.Enabled = True
                    End If
                    
            End Select
            
        Case VIEW_ID_ASSEMBLY 'ID Assembly View
        
            If MaxIDAssemblyPages% = 1 Then
                frmImage.PPage.Enabled = False
                frmImage.NPage.Enabled = False
                Exit Sub
            End If
            
            Select Case IDAssyPageNum%
            
                Case Is = 1
                    frmImage.PPage.Enabled = False
                    
                    If IDAssyPageNum% + 1 > MaxIDAssemblyPages% Then
                        frmImage.NPage.Enabled = False
                    Else
                        frmImage.NPage.Enabled = True
                    End If
                    
                Case Is > 1
                    frmImage.PPage.Enabled = True
                    
                    If IDAssyPageNum% + 1 > MaxIDAssemblyPages% Then
                        frmImage.NPage.Enabled = False
                    Else
                        frmImage.NPage.Enabled = True
                    End If
                    
            End Select
            
        Case VIEW_PARTSLIST 'Parts List View
        
            If MaxPartsListPages% = 1 Then
                frmImage.PPage.Enabled = False
                frmImage.NPage.Enabled = False
                Exit Sub
            End If
            
            Select Case PartListPageNum%
            
                Case Is = 1
                    frmImage.PPage.Enabled = False
                    
                    If PartListPageNum% + 1 > MaxPartsListPages% Then
                        frmImage.NPage.Enabled = False
                    Else
                        frmImage.NPage.Enabled = True
                    End If
                    
                Case Is > 1
                    frmImage.PPage.Enabled = True
                    
                    If PartListPageNum% + 1 > MaxPartsListPages% Then
                        frmImage.NPage.Enabled = False
                    Else
                        frmImage.NPage.Enabled = True
                    End If
                    
            End Select
            
        Case VIEW_ID_PARTSLIST 'ID Parts List View
        
            If MaxIDPartsListPages% = 1 Then
                frmImage.PPage.Enabled = False
                frmImage.NPage.Enabled = False
                Exit Sub
            End If
            
            Select Case PartListPageNum%
            
                Case Is = 1
                    frmImage.PPage.Enabled = False
                    
                    If PartListPageNum% + 1 > MaxPartsListPages% Then
                        frmImage.NPage.Enabled = False
                    Else
                        frmImage.NPage.Enabled = True
                    End If
                    
                Case Is > 1
                    frmImage.PPage.Enabled = True
                    
                    If PartListPageNum% + 1 > MaxPartsListPages% Then
                        frmImage.NPage.Enabled = False
                    Else
                        frmImage.NPage.Enabled = True
                    End If
                    
            End Select
            
    End Select

End Sub

Sub Delay(seconds As Single)
    Dim T!

    T! = Timer
        Do While Timer - T! < seconds
            'added 5/23/03 - fix 100% CPU usage during idle time
            Sleep 1
            DoEvents
        Loop

End Sub

Sub DoMenuChoice(Choice%)

      Dim lhwnlAPP As Long
      Dim sTempString As String
      Dim flash As Integer
      
      ImageViewerChoice = False
      
      Select Case Choice%
      
        Case RUNTP_BUTTON, ID_SURVEY
            frmMain.Timer1.Enabled = False
            frmMain.lblStatus.Caption = "Running Test Program"
            HelpKey$ = "!RUN TEST PROGRAM"
            frmMain.Caption = "Test Program Set: " + UUTPartNo$
            Echo ""
            Echo ""
            Echo "*******************************************************************************"
            Echo "               TEST PROGRAM: " + TPName
            Echo "                      TPCCN: " + sTPCCN
            Echo "       TEST PROGRAM VERSION: " + TPVersion
            Echo "           UUT NOMENCLATURE: " + UUTName
            Echo "            UUT PART NUMBER: " + UUTPartNo
            Echo "          TESTING DATE/TIME: " + Format$(Now, "dddd, d-mmm-yyyy h:mm")
            Echo "*******************************************************************************"
            
            If Choice = RUNTP_BUTTON Then
                UserEvent% = RUNTP_BUTTON
            Else
                EROs = "PM119"
                sUUTRev = "N/A"
                sUUTSerial = "N/A"
                sStart = Now
                UserEvent% = ID_SURVEY
            End If
            
        Case VIEW_SCHEMATIC
            HelpKey$ = "!VIEW SCHEMATIC"
            SchemPageNum = frmMain.cboSchematic.ListIndex + 1
            frmImage.Caption = "Schematic Diagram: " + UUTPartNo$ + " [Page " + Str$(SchemPageNum%) _
                                + " of " + Str$(MaxSchematicPages%) + "]"
            sImageFile = SchemPath$ + sSchemFileNames(SchemPageNum%)
            
            If SchemPageNum% = MaxSchematicPages% Then
                frmImage.NPage.Enabled = False
            Else
                frmImage.NPage.Enabled = True
            End If
            
            If SchemPageNum% = 1 Then
                frmImage.PPage.Enabled = False
            Else
                frmImage.PPage.Enabled = True
            End If
            
            ImageViewerChoice = True
            frmImage.Show
            TheImage& = LoadImage() ' Load Page
            
            If TheImage& = 0 Then
               ShowMainMenu
               Unload frmImage
               Exit Sub
            End If
            
        Case VIEW_ID_SCHEMATIC
            
            IDSchemPageNum% = frmMain.cboITAWiring.ListIndex + 1
            frmImage.Caption = "ITA Schematic Diagram: " + IDPartNo$ + " [Page " + Str$(IDSchemPageNum%) _
                                + " of " + Str$(MaxIDSchematicPages%) + "]"
            sImageFile = IDSchemPath$ + sIDSchemFileNames(IDSchemPageNum%)
            
            If IDSchemPageNum% = MaxIDSchematicPages% Then
                frmImage.NPage.Enabled = False
            Else
                frmImage.NPage.Enabled = True
            End If
            
            If IDSchemPageNum = 1 Then
                frmImage.PPage.Enabled = False
            Else
                frmImage.PPage.Enabled = True
            End If
            
            frmImage.Show
            TheImage& = LoadImage() ' Load Page
            
            If TheImage& = 0 Then
               ShowMainMenu
               Unload frmImage
               Exit Sub
            End If
        
        Case VIEW_ASSEMBLY
            
            HelpKey$ = "!VIEW ASSEMBLY"
            AssyPageNum% = frmMain.cboAssembly.ListIndex + 1
            frmImage.Caption = "Assembly Diagram: " + UUTPartNo$ + " [Page " + Str$(AssyPageNum%) _
                                + " of " + Str$(MaxAssemblyPages%) + "]"
            sImageFile = AssyPath$ + sAssyFileNames(AssyPageNum%)
            
            If AssyPageNum% = MaxAssemblyPages% Then
                frmImage.NPage.Enabled = False
            Else
                frmImage.NPage.Enabled = True
            End If
            
            If AssyPageNum% = 1 Then
                frmImage.PPage.Enabled = False
            Else
                frmImage.PPage.Enabled = True
            End If
            
            frmImage.Show
            TheImage& = LoadImage() ' Load Page
            
            If TheImage& = 0 Then
               ShowMainMenu
               Unload frmImage
               Exit Sub
            End If
            
        Case VIEW_ID_ASSEMBLY
            
            HelpKey$ = "!VIEW ASSEMBLY"
            IDAssyPageNum% = frmMain.cboITAAssy.ListIndex + 1
            frmImage.Caption = "ITA Assembly Diagram: " + IDPartNo$ + " [Page " + Str$(IDAssyPageNum%) _
                                + " of " + Str$(MaxIDAssemblyPages%) + "]"
            sImageFile = IDAssyPath$ + sIDAssyFileNames(IDAssyPageNum%)
            
            If IDAssyPageNum% = MaxIDAssemblyPages% Then
                frmImage.NPage.Enabled = False
            Else
                frmImage.NPage.Enabled = True
            End If
            
            If IDAssyPageNum = 1 Then
                frmImage.PPage.Enabled = False
            Else
                frmImage.PPage.Enabled = True
            End If
            
            frmImage.Show
            TheImage& = LoadImage() ' Load Page
            
            If TheImage& = 0 Then
               ShowMainMenu
               Unload frmImage
               Exit Sub
            End If
        
        Case VIEW_PARTSLIST
            
            HelpKey$ = "!VIEW PARTS LIST"
            PartListPageNum% = frmMain.cboPartsList.ListIndex + 1
            frmImage.Caption = "Parts List: " + UUTPartNo$ + " [Page " + Str$(PartListPageNum%) _
                                + " of " + Str$(MaxPartsListPages) + "]"
            
            If LCase(Right(sPartsListFileNames(PartListPageNum%), 4)) = ".txt" Then
                DisplayTextFile (PartListPath$ + sPartsListFileNames(1))
                frmImage.Show
            Else
                sImageFile = PartListPath$ + sPartsListFileNames(PartListPageNum%)
                
                If PartListPageNum% = MaxPartsListPages% Then
                    frmImage.NPage.Enabled = False
                Else
                    frmImage.NPage.Enabled = True
                End If
                
                If PartListPageNum = 1 Then
                    frmImage.PPage.Enabled = False
                Else
                    frmImage.PPage.Enabled = True
                End If
                
                frmImage.Show
                TheImage& = LoadImage() ' Load Page
                
                If TheImage& = 0 Then
                   ShowMainMenu
                   Unload frmImage
                   Exit Sub
                End If
            End If
        
        Case VIEW_ID_PARTSLIST
            HelpKey$ = "!VIEW PARTS LIST"
            IDPartListPageNum% = frmMain.cboITAPartsList.ListIndex + 1
            frmImage.Caption = "ITA Parts List: " + IDPartNo$ + " [Page " + Str$(IDPartListPageNum%) _
                                + " of " + Str$(MaxIDPartsListPages) + "]"
            
            If LCase(Right(sIDPartsListFileNames(IDPartListPageNum%), 4)) = ".txt" Then
                DisplayTextFile (IDPartListPath$ + sIDPartsListFileNames(1))
                frmImage.Show
            Else
                sImageFile = IDPartListPath$ + sIDPartsListFileNames(IDPartListPageNum%)
                
                If IDPartListPageNum% = MaxIDPartsListPages% Then
                    frmImage.NPage.Enabled = False
                Else
                    frmImage.NPage.Enabled = True
                End If
                
                If IDPartListPageNum = 1 Then
                    frmImage.PPage.Enabled = False
                Else
                    frmImage.PPage.Enabled = True
                End If
                
                frmImage.Show
                TheImage& = LoadImage() ' Load Page
                
                If TheImage& = 0 Then
                   ShowMainMenu
                   Unload frmImage
                   Exit Sub
                End If
            End If
            frmImage.Show
        
        Case VIEW_TSR
            sImageFile = DocumentsPath$ + sTSRFileName
            ShellExecute lhwnlAPP, "open", sImageFile, "", "", 1
            sTempString = frmMain.lblStatus.Caption
            
            For flash% = 1 To 6
                frmMain.lblStatus.Caption = "Opening Document"
                frmMain.MenuOptionText(-VIEW_TSR).Visible = False
                DoEvents
                Delay (0.1)
                frmMain.MenuOptionText(-VIEW_TSR).Visible = True
                DoEvents
                Delay (0.1)
            Next flash%
            
            frmMain.lblStatus.Caption = sTempString

        Case VIEW_ELTD
            sImageFile = DocumentsPath$ + sELTDFileName
            ShellExecute lhwnlAPP, "open", sImageFile, "", "", 1
            sTempString = frmMain.lblStatus.Caption
            
            For flash% = 1 To 6
                frmMain.lblStatus.Caption = "Opening Document"
                frmMain.MenuOptionText(-VIEW_ELTD).Visible = False
                DoEvents
                Delay (0.1)
                frmMain.MenuOptionText(-VIEW_ELTD).Visible = True
                DoEvents
                Delay (0.1)
            Next flash%
            
            frmMain.lblStatus.Caption = sTempString
        
        Case VIEW_GENERAL_INFO
            sImageFile = DocumentsPath$ + sInfoFileName
            ShellExecute lhwnlAPP, "open", sImageFile, "", "", 1
            sTempString = frmMain.lblStatus.Caption
            
            For flash% = 1 To 6
                frmMain.lblStatus.Caption = "Opening Document"
                frmMain.MenuOptionText(-VIEW_GENERAL_INFO).Visible = False
                DoEvents
                Delay (0.1)
                frmMain.MenuOptionText(-VIEW_GENERAL_INFO).Visible = True
                DoEvents
                Delay (0.1)
            Next flash%
            
            frmMain.lblStatus.Caption = sTempString
        
        Case TPS_DOCUMENTATION
            ShowTestDocumentation
        
        Case VIEW_FAULT_FILE
            RunMyApp sStartMode:="SW_MAXIMIZE"
        
        Case VIEW_IETM
            ShellExecute lhwnlAPP, "open", IETMPath, "", "", 1
            sTempString = frmMain.lblStatus.Caption
            
            For flash% = 1 To 6
                frmMain.lblStatus.Caption = "Opening Document"
                frmMain.MenuOptionText(-VIEW_IETM).Visible = False
                DoEvents
                Delay (0.1)
                frmMain.MenuOptionText(-VIEW_IETM).Visible = True
                DoEvents
                Delay (0.1)
            Next flash%
            
            frmMain.lblStatus.Caption = sTempString
        
        Case QUIT_BUTTON
            ExitForm.Show 1
            
    End Select

End Sub


Sub DisableProgramControl()
    
    frmMain.Continue.Enabled = False
    frmMain.AbortButton.Enabled = False

End Sub
Sub ShowMainMenu()
    Dim opt%
    
    frmMain.Timer1.Enabled = True
    frmMain.Timer2.Enabled = False
    frmMain.fraInstrument.Visible = False
    frmMain.TimerStatusByteRec.Enabled = False
    
    frmMain.MainMenu.Visible = True
    frmMain.picTestDocumentation.Visible = False

    frmMain.ModuleMenu(0).Visible = False
    frmMain.MainMenuButton.Enabled = False
    For opt% = 1 To MAX_MENU_OPT
        frmMain.MenuOption(opt%).Visible = True
        frmMain.MenuOptionText(opt%).Visible = True
    Next opt%
    
    If UUTPartNo = IDPartNo$ Then
        frmMain.MenuOption(-ID_SURVEY).Enabled = False
        frmMain.MenuOptionText(-ID_SURVEY).Enabled = False
    End If
    
    
    frmMain.fraInstructions.Visible = False
    frmMain.cmdFHDB.Visible = False
    frmMain.pinp.Picture = LoadPicture()
    frmMain.pinp.Visible = False
    
    ImageViewerChoice = False   'Required for HELP function to work properly
    frmMain.PictureWindow.Visible = False
    frmMain.TextWindow.Visible = False
    frmMain.SeqTextWindow.Visible = False
    DisableProgramControl
    frmMain.AbortButton.Visible = True
    frmMain.RerunButton.Visible = False
    frmMain.PrintButton.Visible = True
    frmMain.PrintButton.Enabled = False
    frmMain.Continue.Enabled = False
    frmMain.Quit.Enabled = True
    frmMain.Caption = "Test Program Set: " + UUTPartNo$
    frmMain.cmdFHDB.Enabled = False
    frmMain.cmdFHDB.Visible = False
    If frmMain.cboAssembly.List(0) <> "" Then frmMain.cboAssembly.Visible = True
    If frmMain.cboPartsList.List(0) <> "" Then frmMain.cboPartsList.Visible = True
    If frmMain.cboSchematic.List(0) <> "" Then frmMain.cboSchematic.Visible = True
    frmMain.cboITAAssy.Visible = False
    frmMain.cboITAPartsList.Visible = False
    frmMain.cboITAWiring.Visible = False
    
    HelpKey$ = "!MAIN MENU"
    frmMain.TextWindow.Text = ""

    If ProgramPath$ = " " Then
        MsgBox "Test Program Path not specified. Set the TPPath$ variable.", 16, "Shell Error - Fatal"
    End If

    For opt% = 1 To MAX_MENU_OPT
        Select Case opt%
            Case RUNTP_BUTTON  'Enable RUN TEST PROGRAM BUTTON
                frmMain.MenuOption(opt%).Enabled = True
                frmMain.MenuOptionText(opt%).Enabled = True
            
            Case VIEW_SCHEMATIC    'Enable VIEW SCHEMATIC BUTTON if schematics are available
                If MaxSchematicPages% > 0 Then
                    frmMain.MenuOption(opt%).Enabled = True
                    frmMain.MenuOptionText(opt%).Enabled = True
                End If
            
            Case VIEW_ASSEMBLY    'Enable VIEW ASSEMBLY BUTTON if Assembly Drawing is available
                If MaxAssemblyPages% > 0 Then
                    frmMain.MenuOption(opt%).Enabled = True
                    frmMain.MenuOptionText(opt%).Enabled = True
                End If
            
            Case VIEW_PARTSLIST    'Enable VIEW PARTS LIST BUTTON if Parts List is available
                If MaxPartsListPages% > 0 Then
                    frmMain.MenuOption(opt%).Enabled = True
                    frmMain.MenuOptionText(opt%).Enabled = True
                End If
        End Select
    Next opt%
    
    CenterForm frmMain
    frmMain.Caption = "Test Program Set: " + UUTPartNo$ + " Version: " + TPVersion
    frmMain.Show
End Sub

Sub DisplayTextFile(fileName As String)
    Dim TextFile As Integer
    Dim dataline As String
    Dim TheFile As String
        
    On Error Resume Next
    
    ShowWindow frmMain.TextWindow
    TextFile% = FreeFile
    Open fileName For Input As TextFile%
    If Err Then
        MsgBox "Error opening specified file: " + fileName + " .  Check to see that path and filename specified are correct.", 16, "Shell Error"
        Exit Sub
    End If
    Do While Not EOF(TextFile%)
        Line Input #TextFile%, dataline$
        dataline$ = Mid$(dataline$, 1, 77) + CRLF
        TheFile$ = TheFile$ + dataline$
    Loop
    Close TextFile%
    If Len(TheFile$) > 65000 Then
        MsgBox "File too large for viewer", 16, "Shell Error - Non-Fatal"
        Exit Sub
    End If
    frmMain.TextWindow.Text = TheFile$
    frmMain.MainMenuButton.Enabled = True
   
End Sub

Sub ShowWindow(WindowToShow As Control)
    frmMain.PictureWindow.Visible = False
    frmMain.MainMenu.Visible = False
    frmMain.SeqTextWindow.Visible = False
    frmMain.ModuleMenu(0).Visible = False
    WindowToShow.Visible = True
End Sub
Sub HelpUser()
    
End Sub

Sub GetEnabledButtons()

    Button% = 1
    If frmMain.MainMenuButton.Enabled = True Then
        Buttons$(Button%) = "MainMenuButton"
        Button% = Button% + 1
    End If
    If frmMain.Quit.Enabled = True Then
        Buttons$(Button%) = "Quit"
        Button% = Button% + 1
    End If
    If frmMain.AbortButton.Enabled = True Then
        Buttons$(Button%) = "Abort"
        Button% = Button% + 1
    End If
    If frmMain.Continue.Enabled = True Then
        Buttons$(Button%) = "Continue"
        Button% = Button% + 1
    End If
    If frmImage.MouseControl(PANNING).Enabled = True Then
        Buttons$(Button%) = "MouseAction(PANNING)"
        Button% = Button% + 1
    End If
    If frmImage.MouseControl(ZOOMING).Enabled = True Then
        Buttons$(Button%) = "MouseAction(ZOOMING)"
        Button% = Button% + 1
    End If
    If frmImage.MouseControl(MAGNIFYING).Enabled = True Then
        Buttons$(Button%) = "MouseAction(MAGNIFYING)"
        Button% = Button% + 1
    End If
    If frmImage.PPage.Enabled = True Then
        Buttons$(Button%) = "PreviousPage"
        Button% = Button% + 1
    End If
    If frmImage.NPage.Enabled = True Then
        Buttons$(Button%) = "NextPage"
        Button% = Button% + 1
    End If
    If frmImage.ResetView.Enabled = True Then
        Buttons$(Button%) = "ResetView"
        Button% = Button% + 1
    End If
    If frmImage.ZoomOut50.Enabled = True Then
        Buttons$(Button%) = "ZoomOut50"
        Button% = Button% + 1
    End If
    If frmImage.PrintButton.Enabled = True Then
        Buttons$(Button%) = "Print"
        Button% = Button% + 1
    End If
    If frmMain.RerunButton.Enabled = True Then
        Buttons$(Button%) = "Rerun"
        Button% = Button% + 1
    End If

End Sub

Function Limit%(CheckValue%, LimitValue%)
    If CheckValue < LimitValue Then
        Limit = LimitValue
    Else
        Limit = CheckValue
    End If
End Function

Sub Echo(ByVal Message As String)
    Dim l%
    Dim PrintMessage As String
    
    On Error Resume Next
    'Indent message on screen by 2 spaces
    Message = Space(2) + Message
    
    frmMain.SeqTextWindow.Cls
    For l% = 1 To MaxScreenLines - 1
        ScreenBuffer$(l%) = ScreenBuffer$(l% + 1)
        frmMain.SeqTextWindow.Print ScreenBuffer$(l%)
    Next l%
    ScreenBuffer$(MaxScreenLines) = Message
    frmMain.SeqTextWindow.Print ScreenBuffer$(MaxScreenLines)
    
    'Format test results for printer output
    PrintMessage$ = Space(6) + Message + vbCrLf
    frmMain.TextWindow.Text = frmMain.TextWindow.Text + PrintMessage$
    
    Err.Clear
    If Logging% Then
        Print #LOGFILE&, Message
        If Err Then
            MsgBox "Error writing to log file. Error " + Str$(Err) + ".  Logging disabled.", 48, "File Error"
            Logging% = False
        End If
    End If
    DoEvents

End Sub

Sub EnableAbort()
    frmMain.AbortButton.Visible = True
    frmMain.AbortButton.Enabled = True
    frmMain.RerunButton.Visible = False
    frmMain.RerunButton.Enabled = False
    frmMain.PrintButton.Enabled = False
    '***Modified For QuickView Version 3.7 DWH***
    frmMain.MainMenuButton.Enabled = False
    '**********End Of Modifications DWH**********
    frmMain.Quit.Enabled = False
End Sub
Sub LongDelay(seconds As Single)

Dim T As Single
Dim x As Single
    
    On Error GoTo ErrorHandle
    
    T = Timer
    DoEvents
    frmMain.Quit.Enabled = False
    Do While Timer - T < seconds
        'added 5/23/03 - fix 100% CPU usage during idle time
        Sleep 1
        x = Timer - T
        DoEvents
    Loop
    
    Exit Sub

ErrorHandle:
     MsgBox "Error No.: " & Err.Number & vbCrLf _
       & "Description: " & Err.Description, 16, "Shell Error"
    Err.Clear
    Resume Next

End Sub
Sub DisablePrint()
    frmMain.PrintButton.Enabled = False
End Sub


Sub DisplayBitmap(FileSpec As String)
    
    On Error Resume Next
    
    frmMain.PictureWindow.Cls
    'Hide Progress Bar if visible
    If frmMain.PictureWindow.Visible = False Then
       ShowWindow frmMain.PictureWindow
    End If
    frmMain.pinp.Picture = LoadPicture(FileSpec)
    If Err Then
        MsgBox "Could not load file: " + FileSpec + " .  Check to see that the file exists and is specified properly.", 16, "Shell Error"
    End If
    frmMain.pinp.Move frmMain.PictureWindow.Left - 120, frmMain.PictureWindow.Top - 120, frmMain.PictureWindow.Width, frmMain.PictureWindow.Height
    frmMain.pinp.Visible = True
    frmMain.Quit.Enabled = True
    frmMain.Continue.Enabled = True
    frmMain.Continue.Default = True
    frmMain.PrintButton.Enabled = False
    frmMain.AbortButton.Enabled = False
    frmMain.MainMenuButton.Enabled = True

End Sub

Sub EnableContinue()
    frmMain.Continue.Enabled = True
End Sub
Sub EnableQuit()
    frmMain.Quit.Enabled = True
End Sub
Sub DisableQuit()
    frmMain.Quit.Enabled = False
End Sub

Sub UpdateProgress(PercentComplete As Single)
    Dim sTemp As String
    Dim iCount As Integer
    
    frmMain.lblStatus.Caption = CStr(PercentComplete) & "% Complete"
    On Error GoTo ErrorHandle
    
    frmMain.Timer2.Enabled = True
    frmMain.Timer1.Enabled = False
    If PercentComplete > 100 Then PercentComplete = 100
        
    'added 5/20/03 Soon Nam
    'remove picStatus and insert progressbar
    For iCount = 0 To 4
        If (frmMain.picStatus(iCount)) Then
            frmMain.picStatus(iCount).Visible = False
        End If
    Next iCount
    
    'enable progress bar
    frmMain.ProgressBar.Enabled = True
    frmMain.ProgressBar.Left = 9350
    frmMain.ProgressBar.Width = 2000
    frmMain.ProgressBar.Max = 100

    frmMain.ProgressBar.Visible = True
    frmMain.ProgressBar.value = PercentComplete
    
    'remove progress bar at 100% and put back picStatus
    If PercentComplete = 100 Then
        'remove progress bar
        frmMain.ProgressBar.Visible = False
        Delay 1
        For iCount = 0 To 4
            frmMain.picStatus(iCount).Visible = True
            frmMain.picStatus(iCount).Cls
            frmMain.picStatus(iCount).Picture = frmMain.ImageList2.ListImages(1).Picture
        Next iCount
        frmMain.Timer2.Enabled = False
        frmMain.lblStatus.ForeColor = vbBlack
    End If
    DoEvents
    Exit Sub
ErrorHandle:
     MsgBox "Error No.: " & Err.Number & vbCrLf _
       & "Description: " & Err.Description, 16, "Shell Error"
    Err.Clear
    Resume Next

End Sub

Sub DisableContinue()
    frmMain.Continue.Enabled = False
End Sub

Sub DisableRerun()
    frmMain.RerunButton.Enabled = False
End Sub

Sub EnablePrint()
    frmMain.MainMenuButton.Enabled = False
    frmMain.AbortButton.Visible = False
    frmMain.AbortButton.Enabled = False
    frmMain.RerunButton.Visible = False
    frmMain.RerunButton.Enabled = False
    
    frmMain.PrintButton.Caption = "Print Results"
    frmMain.PrintButton.Visible = True
    frmMain.PrintButton.Enabled = True

End Sub

Sub EnableRerun()
    frmMain.RerunButton.Visible = True
    frmMain.RerunButton.Enabled = True
    frmMain.AbortButton.Visible = False
    frmMain.AbortButton.Enabled = False
End Sub

Sub EnableButtonList()

    For Button% = 1 To 15
        
        Select Case Buttons$(Button%)
            
            Case "MainMenuButton"
                frmMain.MainMenuButton.Enabled = True
            Case "Quit"
                frmMain.Quit.Enabled = True
            Case "Abort"
                frmMain.AbortButton.Enabled = True
            Case "Continue"
                frmMain.Continue.Enabled = True
            Case Else
                'Do nothing
        End Select
    Next Button%
End Sub

Sub ShowForm(FormToShow As Form)

    CenterForm FormToShow
    FormToShow.Show MODAL

End Sub

Public Sub ChangeTextColor(nColor As Long)
    frmMain.SeqTextWindow.ForeColor = nColor
End Sub

Sub ShowModuleMenu()
    Dim i As Integer
    
    ClearTestMeasurementInformation
    frmMain.Timer2.Enabled = False
    frmMain.Timer1.Enabled = True
    frmMain.lblStatus.Caption = "Waiting on User ..."
    frmMain.TimerStatusByteRec.Enabled = False
    
    frmMain.fraInstructions.Visible = False
    
    frmMain.PictureWindow.Cls
    frmMain.PictureWindow.Visible = False
    frmMain.pinp.Cls
    frmMain.pinp.Visible = False
    frmMain.fraInstructions.Visible = False
    frmMain.cmdFHDB.Visible = False
    frmMain.cmdDiagnostics.Visible = False
    
    frmMain.MainMenu.Visible = False
    frmMain.MainMenuButton.Enabled = True
    frmMain.pinp.Picture = LoadPicture()
    frmMain.pinp.Visible = False
    frmMain.PictureWindow.Visible = False
    frmMain.TextWindow.Visible = False
    frmMain.SeqTextWindow.Visible = False
    frmMain.ModuleOuter.Visible = True
    frmMain.ModuleMenu(0).Visible = True
    DisableProgramControl
    frmMain.AbortButton.Enabled = False
    frmMain.AbortButton.Visible = True
    frmMain.RerunButton.Visible = False
    frmMain.PrintButton.Visible = True
    frmMain.PrintButton.Enabled = False
    frmMain.PrintButton.Caption = "Print"
    frmMain.Continue.Enabled = False
    frmMain.Quit.Enabled = True
    frmMain.Caption = "Test Program Set: " + UUTPartNo$ + " Module Menu"

    
    'Build Module Menu
    If bModuleMenuBuilt = False Then
        frmMain.lblPwrOn(0).Enabled = False
        frmMain.cmdPwrOnModule(0).Enabled = False
        frmMain.lblPwrOnStatus(0).Enabled = False
        
        frmMain.VScroll1.LargeChange = 10
        i = (MaxModules + 3) - 10
        frmMain.VScroll1.Min = 0
        If (i > 0) Then
          frmMain.VScroll1.Max = i
          frmMain.VScroll1.Visible = True
          frmMain.ModuleOuter.Width = 9855
        Else
          frmMain.VScroll1.Max = 0
          ' for the calculation below the number needs to be one or more or else there
          ' will be no height to the picturebox
          i = 1
          frmMain.VScroll1.Visible = False
          frmMain.ModuleOuter.Width = 10215
        End If
        frmMain.ModuleInner.Height = (MaxModules + 3) * (frmMain.cmdEndToEnd(0).Height + 280)
        bModuleMenuBuilt = True
    
        frmMain.lblModuleName(1).Caption = ModuleNames(1)
        If ModuleRunTime(1) > 60 Then
            frmMain.lblModuleRunTime(1).Caption = Format(ModuleRunTime(1) / 60, "##0.00") & " Min."
        Else
            frmMain.lblModuleRunTime(1).Caption = ModuleRunTime(1) & " Sec."
        End If
        
        frmMain.lblModuleName(1).Visible = True
        frmMain.lblModuleRunTime(1).Visible = True
        frmMain.cmdModule(1).Visible = True
        frmMain.lblModuleStatus(1).Visible = True
        frmMain.lblModuleName(1).Enabled = False
        frmMain.lblModuleRunTime(1).Enabled = False
        frmMain.cmdModule(1).Enabled = False
        frmMain.lblModuleStatus(1).Enabled = False
        
        For i = 2 To MaxModules
            
            Load frmMain.lblModuleStatus(i)
            frmMain.lblModuleStatus(i).Top = frmMain.lblModuleStatus(i - 1).Top + 720
            frmMain.lblModuleStatus(i).Enabled = False
            frmMain.lblModuleStatus(i).Visible = True
            
            Load frmMain.cmdModule(i)
            frmMain.cmdModule(i).Top = frmMain.cmdModule(i - 1).Top + 720
            frmMain.cmdModule(i).Enabled = False
            frmMain.cmdModule(i).Visible = True
            
            Load frmMain.lblModuleName(i)
            frmMain.lblModuleName(i).Top = frmMain.lblModuleName(i - 1).Top + 720
            frmMain.lblModuleName(i).Caption = ModuleNames(i)
            frmMain.lblModuleName(i).Enabled = False
            frmMain.lblModuleName(i).Visible = True
            
            Load frmMain.lblModuleRunTime(i)
            frmMain.lblModuleRunTime(i).Top = frmMain.lblModuleRunTime(i - 1).Top + 720
            If ModuleRunTime(i) > 60 Then
                frmMain.lblModuleRunTime(i).Caption = Format(ModuleRunTime(i) / 60, "##0.00") & " Min."
            Else
                frmMain.lblModuleRunTime(i).Caption = ModuleRunTime(i) & " Sec."
            End If
            frmMain.lblModuleRunTime(i).Enabled = False
            frmMain.lblModuleRunTime(i).Visible = True
        Next i
        bModuleMenuBuilt = True
    End If
        
    'Enable PWR_On Test if STTO Passes
    If frmMain.lblSTTOStatus(1).Caption = "PASSED" Then
        frmMain.lblPwrOn(0).Enabled = True
        frmMain.cmdPwrOnModule(0).Enabled = True
        frmMain.lblPwrOnStatus(0).Enabled = True
    Else 'Disable if STTO
        frmMain.lblPwrOn(0).Enabled = False
        frmMain.cmdPwrOnModule(0).Enabled = False
        frmMain.lblPwrOnStatus(0).Enabled = False
    End If

    'Enable Module Menu items if PWR_On Passes
    If frmMain.lblPwrOnStatus(0).Caption = "PASSED" Then
        For i = 1 To MaxModules
            frmMain.lblModuleStatus(i).Enabled = True
            frmMain.cmdModule(i).Enabled = True
            frmMain.lblModuleName(i).Enabled = True
            frmMain.lblModuleRunTime(i).Enabled = True
        Next i
    Else 'Disable if Failed
        For i = 1 To MaxModules
            frmMain.lblModuleStatus(i).Enabled = False
            frmMain.cmdModule(i).Enabled = False
            frmMain.lblModuleName(i).Enabled = False
            frmMain.lblModuleRunTime(i).Enabled = False
        Next i
    End If
    
    'Enable all If TPS Development
    If bTPSDevelopment Then
        frmMain.lblPwrOn(0).Enabled = True
        frmMain.cmdPwrOnModule(0).Enabled = True
        frmMain.lblPwrOnStatus(0).Enabled = True
        For i = 1 To MaxModules
            frmMain.lblModuleStatus(i).Enabled = True
            frmMain.cmdModule(i).Enabled = True
            frmMain.lblModuleName(i).Enabled = True
            frmMain.lblModuleRunTime(i).Enabled = True
        Next i
    End If

    If bFirstRun Then
        ClearModuleStatus
        frmMain.NewUUT.Visible = False
        bFirstRun = False
    Else
        frmMain.NewUUT.Visible = True
        frmMain.NewUUT.Enabled = True
    End If
    
    If ProgramPath$ = " " Then
        MsgBox "Test Program Path not specified. Set the TPPath$ variable.", 16, "Shell Error - Fatal"
    End If

End Sub

Sub ClearTestMeasurementInformation()
    frmMain.txtModule.Text = ""
    frmMain.txtTestName.Text = ""
    frmMain.txtStep.Text = ""
    frmMain.txtUpperLimit.Text = ""
    frmMain.txtLowerLimit.Text = ""
    frmMain.txtUnit.Text = ""
    frmMain.txtInstrument.Text = ""
    frmMain.txtCommand.Text = ""
    frmMain.txtMeasured.Text = ""
    frmMain.txtMeasured.BackColor = vbWhite
    frmMain.txtMeasuredBig.Text = ""
    frmMain.txtMeasuredBig.BackColor = vbWhite
End Sub

Public Function sUserInput(sTitle As String, nTextJustification As Long, sLinesOfText() As String) As String
    Dim i As Integer
    Dim nNumberoflines As Long

    Dim sTemp As String
    Dim bCurrentState As Boolean
    Dim bContinueState As Boolean
    Dim bQuitState As Boolean
    Dim bMainMenuState As Boolean
    
    sTemp = frmMain.lblStatus.Caption
    bCurrentState = frmMain.Timer2.Enabled
    bContinueState = frmMain.Continue.Enabled
    bQuitState = frmMain.Quit.Enabled
    bMainMenuState = frmMain.MainMenuButton.Enabled
    
    frmMain.Continue.Enabled = False
    
    frmMain.Timer1.Enabled = True
    frmMain.Timer2.Enabled = False
    frmMain.lblStatus.Caption = "Waiting for User ..."
    
    nNumberoflines = UBound(sLinesOfText)
    
    frmOperatorMsg.lblInput.Visible = True
    frmOperatorMsg.InputData.Text = ""
    frmOperatorMsg.InputData.Visible = True
    frmOperatorMsg.Width = 8535
    frmOperatorMsg.SSPanel1.Width = 8175
    frmOperatorMsg.msgOutBox.Width = 7575
    frmOperatorMsg.msgInBox.Width = 7575
    frmOperatorMsg.lblMsg(1).Width = 7215
    frmOperatorMsg.lblMsg(1).Left = 120
    frmOperatorMsg.imgLogo.Left = 120

    If nNumberoflines > 20 Then
        'Set limit on Form Size
        frmOperatorMsg.Height = 6645
        
        'Locate Continue Button
        frmOperatorMsg.Continue.Left = 6840
        frmOperatorMsg.SSPanel1.Height = 5295
        frmOperatorMsg.msgOutBox.Height = 5055
        frmOperatorMsg.VScroll1.Height = 5055
        frmOperatorMsg.VScroll1.Visible = True
        frmOperatorMsg.VScroll1.LargeChange = 20
        frmOperatorMsg.VScroll1.Min = 0
        frmOperatorMsg.VScroll1.Max = nNumberoflines - 20
    Else
        'Resize Form
        frmOperatorMsg.Width = 8100
        frmOperatorMsg.SSPanel1.Width = 7815
        frmOperatorMsg.Height = 2085 + (frmOperatorMsg.lblMsg(1).Height * nNumberoflines)
        frmOperatorMsg.SSPanel1.Height = 735 + (frmOperatorMsg.lblMsg(1).Height * nNumberoflines)
        frmOperatorMsg.msgOutBox.Height = 495 + (frmOperatorMsg.lblMsg(1).Height * nNumberoflines)

        'Locate Buttons and Logo
        frmOperatorMsg.Continue.Left = 6360
        frmOperatorMsg.imgLogo.Left = 120
        frmOperatorMsg.VScroll1.Visible = False
        
    End If
    
    'Build Form
    frmOperatorMsg.Continue.Top = frmOperatorMsg.SSPanel1.Top + frmOperatorMsg.SSPanel1.Height + 120
    frmOperatorMsg.imgLogo.Top = frmOperatorMsg.Continue.Top
    frmOperatorMsg.lblInput.Top = frmOperatorMsg.Continue.Top
    frmOperatorMsg.InputData.Top = frmOperatorMsg.Continue.Top

    frmOperatorMsg.msgInBox.Height = 495 + ((nNumberoflines) * (frmOperatorMsg.lblMsg(1).Height))
    frmOperatorMsg.lblMsg(1).Alignment = nTextJustification
    frmOperatorMsg.lblMsg(1).Caption = sLinesOfText(1)

    For i = 2 To nNumberoflines
        Load frmOperatorMsg.lblMsg(i)
        frmOperatorMsg.lblMsg(i).Top = frmOperatorMsg.lblMsg(i - 1).Top + 240
        frmOperatorMsg.lblMsg(i).Caption = sLinesOfText(i)
        frmOperatorMsg.lblMsg(i).Visible = True
    Next i

    frmOperatorMsg.Caption = sTitle
    CenterForm frmOperatorMsg
    frmOperatorMsg.Show vbModal

    sUserInput = UCase(UserInputData)
    
    frmMain.lblStatus.Caption = sTemp
    frmMain.Timer2.Enabled = bCurrentState
    
    frmMain.Timer1.Enabled = False
    
    sTemp = frmMain.lblStatus.Caption = sTemp
    frmMain.Timer2.Enabled = bCurrentState
    frmMain.Continue.Enabled = bContinueState
    frmMain.Quit.Enabled = bQuitState
    frmMain.MainMenuButton.Enabled = bMainMenuState

End Function

Sub ShowTestDocumentation()
    Dim opt%
    
    frmMain.Timer1.Enabled = True
    frmMain.Timer2.Enabled = False
    frmMain.fraInstrument.Visible = False
    
    frmMain.MainMenu.Visible = True
    frmMain.picTestDocumentation.Visible = True
    frmMain.cboAssembly.Visible = False
    frmMain.cboPartsList.Visible = False
    frmMain.cboSchematic.Visible = False
    
    frmMain.ModuleMenu(0).Visible = False
    frmMain.MainMenuButton.Enabled = True
    For opt% = 7 To MAX_MENU_OPT
        frmMain.MenuOption(opt%).Visible = True
        frmMain.MenuOptionText(opt%).Visible = True
    Next opt%
    
    
    frmMain.pinp.Picture = LoadPicture()
    frmMain.pinp.Visible = False
    
    ImageViewerChoice = False   'Required for HELP function to work properly
    frmMain.PictureWindow.Visible = False
    frmMain.TextWindow.Visible = False
    frmMain.SeqTextWindow.Visible = False
    DisableProgramControl
    frmMain.AbortButton.Visible = True
    frmMain.RerunButton.Visible = False
    frmMain.PrintButton.Visible = True
    frmMain.PrintButton.Enabled = False
    frmMain.Continue.Enabled = False
    frmMain.Quit.Enabled = True
    frmMain.Caption = "Test Program Set: " + UUTPartNo$
    frmMain.cmdFHDB.Enabled = False
    frmMain.cmdFHDB.Visible = False

    HelpKey$ = "!MAIN MENU"
    frmMain.TextWindow.Text = ""

    If ProgramPath$ = " " Then
        MsgBox "Test Program Path not specified. Set the TPPath$ variable.", 16, "Shell Error - Fatal"
    End If

    For opt% = 7 To MAX_MENU_OPT
        Select Case -opt%
            Case VIEW_ID_SCHEMATIC    'Enable VIEW ITA SCHEMATIC BUTTON if schematics are available
                If UCase(sIDSchemFileNames(1)) = "NONE" Then
                    frmMain.MenuOption(opt%).Enabled = False
                    frmMain.MenuOptionText(opt%).Enabled = False
                    frmMain.MenuOptionText(opt%).ForeColor = &H808080
                    frmMain.cboITAWiring.Visible = False
                Else
                    frmMain.MenuOption(opt%).Enabled = True
                    frmMain.MenuOptionText(opt%).Enabled = True
                    frmMain.MenuOptionText(opt%).ForeColor = vbBlack
                    frmMain.cboITAWiring.Visible = True
                End If
            
            Case VIEW_ID_ASSEMBLY    'Enable VIEW ASSEMBLY BUTTON if Assembly Drawinh is available
               If UCase(sIDAssyFileNames(1)) = "NONE" Then
                    frmMain.MenuOption(opt%).Enabled = False
                    frmMain.MenuOptionText(opt%).Enabled = False
                    frmMain.MenuOptionText(opt%).ForeColor = &H808080
                    frmMain.cboITAAssy.Visible = False
                Else
                    frmMain.MenuOption(opt%).Enabled = True
                    frmMain.MenuOptionText(opt%).Enabled = True
                    frmMain.MenuOptionText(opt%).ForeColor = vbBlack
                    frmMain.cboITAAssy.Visible = True
                End If
            
            Case VIEW_ID_PARTSLIST    'Enable VIEW PARTS LIST BUTTON if Parts List is available
                If UCase(sIDPartsListFileNames(1)) = "NONE" Then
                    frmMain.MenuOption(opt%).Enabled = False
                    frmMain.MenuOptionText(opt%).Enabled = False
                    frmMain.MenuOptionText(opt%).ForeColor = &H808080
                    frmMain.cboITAPartsList.Visible = False
                Else
                    frmMain.MenuOption(opt%).Enabled = True
                    frmMain.MenuOptionText(opt%).Enabled = True
                    frmMain.MenuOptionText(opt%).ForeColor = vbBlack
                    frmMain.cboITAPartsList.Visible = False
                End If
            
            Case VIEW_TSR    'Enable VIEW PARTS LIST BUTTON if Parts List is available
                If UCase(sTSRFileName) = "NONE" Then
                    frmMain.MenuOption(opt%).Enabled = False
                    frmMain.MenuOptionText(opt%).Enabled = False
                    frmMain.MenuOptionText(opt%).ForeColor = &H808080
                Else
                    frmMain.MenuOption(opt%).Enabled = True
                    frmMain.MenuOptionText(opt%).Enabled = True
                    frmMain.MenuOptionText(opt%).ForeColor = vbBlack
                End If
            
            Case VIEW_ELTD    'Enable VIEW PARTS LIST BUTTON if Parts List is available
                If UCase(sELTDFileName) = "NONE" Then
                    frmMain.MenuOption(opt%).Enabled = False
                    frmMain.MenuOptionText(opt%).Enabled = False
                    frmMain.MenuOptionText(opt%).ForeColor = &H808080
                Else
                    frmMain.MenuOption(opt%).Enabled = True
                    frmMain.MenuOptionText(opt%).Enabled = True
                    frmMain.MenuOptionText(opt%).ForeColor = vbBlack
                End If
            
            Case VIEW_GENERAL_INFO   'Enable VIEW INFO BUTTON
                If UCase(sInfoFileName) = "NONE" Then
                    frmMain.MenuOption(opt%).Enabled = False
                    frmMain.MenuOptionText(opt%).Enabled = False
                    frmMain.MenuOptionText(opt%).ForeColor = &H808080
                Else
                    frmMain.MenuOption(opt%).Enabled = True
                    frmMain.MenuOptionText(opt%).Enabled = True
                    frmMain.MenuOptionText(opt%).ForeColor = vbBlack
                End If
        End Select
    Next opt%
    
    CenterForm frmMain
    frmMain.Caption = "Test Program Set: " + UUTPartNo$ + " Version: " + TPVersion
    frmMain.Show
End Sub

Sub ClearModuleStatus()
        Dim iCount As Integer
    
        frmMain.lblEndToEndStatus(0).Caption = "Unknown"
        frmMain.lblEndToEndStatus(0).BackColor = &HFFFFC0
        frmMain.lblEndToEndStatus(0).ForeColor = &HC00000
        
        frmMain.lblSTTOStatus(1).Caption = "Unknown"
        frmMain.lblSTTOStatus(1).BackColor = &HFFFFC0
        frmMain.lblSTTOStatus(1).ForeColor = &HC00000
        
        frmMain.lblPwrOnStatus(0).Caption = "Unknown"
        frmMain.lblPwrOnStatus(0).BackColor = &HFFFFC0
        frmMain.lblPwrOnStatus(0).ForeColor = &HC00000
        
        For iCount = 1 To MaxModules
            frmMain.lblModuleStatus(iCount).Caption = "Unknown"
            frmMain.lblModuleStatus(iCount).BackColor = &HFFFFC0
            frmMain.lblModuleStatus(iCount).ForeColor = &HC00000
        Next iCount
End Sub


Public Function ConvertHexToBin(HexChars$) As String

    Dim Conv As String
    Dim sTemp As String
    Dim i As Integer
    Dim nSize As Long
    
    nSize = Len(HexChars$)
    If nSize < 5 Then
        Conv = ""
        For i = 1 To nSize
            sTemp = Mid(HexChars, i, 1)
            Select Case UCase(sTemp)
                Case "0"
                    Conv$ = Conv$ & "0000"
                Case "1"
                    Conv$ = Conv$ & "0001"
                Case "2"
                    Conv$ = Conv$ & "0010"
                Case "3"
                    Conv$ = Conv$ & "0011"
                Case "4"
                    Conv$ = Conv$ & "0100"
                Case "5"
                    Conv$ = Conv$ & "0101"
                Case "6"
                    Conv$ = Conv$ & "0110"
                Case "7"
                    Conv$ = Conv$ & "0111"
                Case "8"
                    Conv$ = Conv$ & "1000"
                Case "9"
                    Conv$ = Conv$ & "1001"
                Case "A"
                    Conv$ = Conv$ & "1010"
                Case "B"
                    Conv$ = Conv$ & "1011"
                Case "C"
                    Conv$ = Conv$ & "1100"
                Case "D"
                    Conv$ = Conv$ & "1101"
                Case "E"
                    Conv$ = Conv$ & "1110"
                Case "F"
                    Conv$ = Conv$ & "1111"
            End Select
        Next i
        If bSimulation = False Then
            Conv$ = Right(Conv$, nNumberOfChannelPins)
        Else
            Conv$ = Right(Conv$, nSize * 4)
        End If
    Else
        Conv$ = "h" & HexChars$
    End If
    ConvertHexToBin = Conv$
    
End Function

Sub ShowSplashScreen(Optional bAboutButton As Boolean = False)
    frmSplash.lblWeaponSystem = WeaponSystem
    frmSplash.lblWeaponSystemNomenclature = WeaponSystemNomenclature
    frmSplash.lblCompany = DevelopedBy
    frmSplash.lblAddress = address
    If bAboutButton Then
        frmSplash.SplashTimer.Enabled = False
        frmSplash.cmdOK.Enabled = True
        frmSplash.cmdOK.Visible = True
    Else
        frmSplash.SplashTimer.Enabled = True
        frmSplash.cmdOK.Enabled = False
        frmSplash.cmdOK.Visible = False
    End If
    frmSplash.Show vbModal
End Sub

Function RunMyApp(sStartMode As String, Optional sAppToBeUsed As String = "NOTEPAD", Optional sFilePathName As String = "C:\aps\data\fault-file") As Boolean

  Dim structProcess As PROCESS_INFORMATION
  Dim structStartInfo As STARTUPINFO
  Dim lngReturn As Long
  Dim sCreateForMe As String
  Dim iStartMode As Integer
  
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
  sCreateForMe = sAppToBeUsed + " " + sFilePathName
  
  'Intialize the info structure
  structStartInfo.cb = Len(structStartInfo)
  structStartInfo.dwFlags = STARTF_USESHOWWINDOW
  structStartInfo.wShowWindow = iStartMode
  
  'Create the process
  lngReturn = CreateProcessA(0&, sCreateForMe, 0&, 0&, 1&, NORMAL_PRIORITY_CLASS, 0&, 0&, structStartInfo, structProcess)
                              
  RunMyApp = True
  
  Exit Function
  
ProcessError:
  
  RunMyApp = False
  
  If Err.Number = 53 Then
      MsgBox "Error: " + CStr(Err.Number) + vbCrLf + Err.Description + "< " + sFilePathName + " >", vbCritical
  Else
      MsgBox "Error: " + CStr(Err.Number) + vbCrLf + Err.Description, vbCritical
  End If

End Function

Function GetCompactDiscDrives() As String

    '************************************************************
    '* ManTech Test Systems Software Module                     *
    '************************************************************
    '* Nomenclature   : ViperT SYSTEM MENU                      *
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
    Const nBufferLength& = 255               'Length of API Return Buffer
    Dim ReturnCode As Long                   'API Error Code
    Dim lpBuffer As String * nBufferLength   'API Return String Buffer
    Dim DriveList As String                  'Logical Drive Buffer Formatted for VB String Type
    Dim StringLocation As Integer            'Counter/Pointer for String Parsing Loop
    Dim LogicalDrive As String               'Root Drive Description to test for Type
    Dim ReturnDriveList As String            'String that is returned

    'Find All Logical Drives on System
    ReturnCode& = GetLogicalDriveStrings(ByVal nBufferLength&, ByVal lpBuffer$)
    DriveList$ = Trim$(lpBuffer$)
    
    'Check all logical drives for drive type
    For StringLocation% = 1 To Len(DriveList$) Step 4
        LogicalDrive$ = Mid$(DriveList$, StringLocation%, 4)
        ReturnCode& = GetDriveType(ByVal LogicalDrive$)
        'Check if the Drive is a CDROM drive
        If ReturnCode& = DRIVE_CDROM Then
            'RAMDISK = 6, 'CDROM = 5,'REMOTE = 4,'FIXED = 3, 'REMOVABLE = 2
            ReturnDriveList$ = ReturnDriveList$ & LogicalDrive$
        End If
    Next StringLocation%
    
    'Return function Value (CD Drives on System)
    GetCompactDiscDrives = Mid$(ReturnDriveList$, 1, 3) 'Strip Null

End Function

