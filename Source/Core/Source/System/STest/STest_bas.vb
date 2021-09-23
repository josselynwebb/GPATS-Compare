'Option Strict On
Option Explicit On

Imports System.Windows.Forms.Screen
Imports System.Runtime.InteropServices



Public Module STestMain


    '**************************************************************
    '* Nomenclature   : ATS-ViperT SYSTEM SELF TEST               *
    '* Version        : 2.0                                       *
    '* Last Update    : Apr 1, 2017                               *
    '*                                                            *
    '* Astronics Test Systems                                     *
    '* 12889 Ingenuity Dr.                                        *
    '* Orlando, Fl 32826                                          *
    '* 407-381-6062                                               *
    '*                                                            *
    '* Purpose        : This program performs a Self test of      *
    '*                  the ATS-ViperT system                     *
    '* Program Begins Executing Instructions in frmSTest_Load     *
    '**************************************************************

    '-----------------API / DLL Declarations------------------------------
    Declare Function GetPrivateProfileString Lib "kernel32" Alias "GetPrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpDefault As String, ByVal lpReturnedString As String, ByVal nSize As Integer, ByVal lpFileName As String) As Integer
    Declare Function WritePrivateProfileString Lib "kernel32" Alias "WritePrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpString As String, ByVal lpFileName As String) As Integer
    Declare Sub ExitProcess Lib "kernel32" (ByVal uExitCode As Integer)

    Declare Function SystemParametersInfo Lib "user32" Alias "SystemParametersInfoA" (ByVal uAction As Integer, ByVal uParam As Integer, ByRef lpvParam As RectType, ByVal fuWinIni As Integer) As Integer
    Public Const SPI_GETWORKAREA As Integer = 48 'uiAction Constant for SystemParametersInfo function

    Structure RectType 'Structure used in lpvParam of SystemParametersInfo function
        Dim Left As Integer
        Dim Top As Integer
        Dim Right As Integer
        Dim Bottom As Integer
    End Structure
    Public Rect As RectType


    ' WindowStates
    Public Const NORMAL As Short = 0 ' 0 - Normal
    Public Const MINIMIZED As Short = 1 ' 1 - Minimized
    Public Const MAXIMIZED As Short = 2 ' 2 - Maximized
    Public Const MAX_XML_SIZE As Short = 4096


    Public Const LOG_FILE As String = "logfile.txt"
    Public Const NOT_TESTED As Integer = 1
    Public Const FAILED As Integer = False '  0
    Public Const PASSED As Integer = True ' -1
    Public Const UNTESTED As Integer = -99
    Const INITIALIZED As Boolean = True
    Const MSG_RESP_REG As Short = 10
    Const DOR_MASK As Short = &H2000
    Public Const MODAL As Short = 1
    Public Const MANUF As Short = 1
    Public Const MODEL As Short = 2
    Const MANUF_REG As Short = 0
    Const MODEL_REG As Short = 2
    Const STATUS_REG As Short = 4
    Const LO_CONTROL_REG As Short = 0
    Const SWITCH_CONTROL_REG As Short = 2
    Public Const TEST_INCOMPLETE As Short = 1

    Public Const GENERAL_MESSAGE As Integer = -1
    Public Const CLOSE_BUTTON As Integer = -2
    Public Const ABORT_BUTTON As Integer = -3
    Public Const DETAILS_BUTTON As Integer = -4
    Public Const DETAILS_TEXT As Integer = -5
    Public Const PROGRESS_BAR As Integer = -6
    Public Const HELP_BUTTON As Integer = -7
    Public Const PAUSE_BUTTON As Integer = -8

    Public Const DC1 As Short = -1
    Public Const DC2 As Short = -2
    Public Const DC3 As Short = -3
    Public Const DC4 As Short = -4
    Public Const DC5 As Short = -5
    Public Const DC6 As Short = -6
    Public Const DC7 As Short = -7
    Public Const DC8 As Short = -8
    Public Const DC9 As Short = -9
    Public Const DC10 As Short = -10


    Public Const COM_1 As Short = 12
    Public Const COM_2 As Short = 13
    Public Const COM_3 As Short = 14
    Public Const COM_4 As Short = 15
    Public Const COM_5 As Short = 16


    Public Const SOE_BUTTON As Short = -20 ' stop on end
    Public Const LOE_BUTTON As Short = -21 ' loop on end
    Public Const LOS_BUTTON As Short = -22 ' loop on step
    Public Const SOF_BUTTON As Short = -23 ' stop on fault
    Public Const COF_BUTTON As Short = -24 ' continue on fault

    Public Const SECS_IN_DAY As Integer = 86400



    Public Const SWITCH_CONTROLLER As Short = 0
    Public Const LFSWITCH1 As Short = 1
    Public Const LFSWITCH2 As Short = 2
    Public Const LFSWITCH3 As Short = 3
    Public Const MFSWITCH As Short = 4
    Public Const RFSWITCH As Short = 5

    Public Const END_TO_END As Short = 0
    Public Const MIL_STD_1553 As Short = 1
    Public Const DIGITAL As Short = 2

    Public Const SWITCH1 As Short = 3
    Public Const SWITCH2 As Short = 4
    Public Const SWITCH3 As Short = 5
    Public Const SWITCH4 As Short = 6
    Public Const SWITCH5 As Short = 7
    Public Const DMM As Short = 8
    Public Const OSCOPE As Short = 9
    Public Const COUNTER As Short = 10
    Public Const ARB As Short = 11
    Public Const FGEN As Short = 12
    Public Const PPU As Short = 13
    Public Const SYN_RES As Short = 14

    Public Const SERIALCCA As Short = 15
    Public Const RS422 As Short = 16
    Public Const RS232 As Short = 17
    Public Const GIGA As Short = 18

    Public Const CANBUS As Short = 19
    Public Const LAST_CORE_INST = CANBUS

    Public Const APROBE As Short = 20
    Public Const APROBE_INST = APROBE

    Public Const RFPM As Short = 21
    Public Const RFSYN As Short = 22
    Public Const RFMEAS As Short = 23
    Public Const LAST_RF_INST = RFMEAS


    Public Const EO_VCC As Short = 24
    Public Const VEO2 As Short = 25
    Public Const LAST_EO_INST = VEO2
    Public Const LAST_INSTRUMENT = VEO2

    'Added to Identify specific RFMA instruments
    Public Const DOWNCONV As Short = LAST_INSTRUMENT + 1 ' 28
    Public Const LOCALOSC As Short = LAST_INSTRUMENT + 2 ' 29
    Public Const DIGITIZER As Short = LAST_INSTRUMENT + 3 ' 30
    Public Const CALIBRATOR As Short = LAST_INSTRUMENT + 4 ' 31
    Public Const VEO_LARRS As Short = LAST_INSTRUMENT + 5 ' 32

    Public iNumOfTests As Integer
    Public SAIFinstalled As Short
    Public AprobeButton As Integer
    Public InstFileName(LAST_INSTRUMENT + 5) As String
    Public instrumentHandle(LAST_INSTRUMENT + 5) As Integer
    Public CardStatus(LAST_INSTRUMENT + 5) As Integer
    Public IDNResponse(LAST_INSTRUMENT + 5) As String
    Public InstrumentSpec(LAST_INSTRUMENT + 5) As String
    Public InstrumentDescription(LAST_INSTRUMENT + 7) As String '(+7) Supports Cables and RF Meas instruments
    Public InstrumentInitialized(LAST_INSTRUMENT + 5) As Integer
    Public InstrumentStatus(LAST_INSTRUMENT + 5) As Integer
    Public sGuiLabel(LAST_INSTRUMENT + 7) As String
    Public sInstrumentIDCode(LAST_INSTRUMENT) As String 'Array of Instrument ID's for Failure Step
    Public sTestName(LAST_INSTRUMENT + 5) As String
    Public SupplyHandle(10) As Integer
    Public RFOptionInstalled As Short
    Public EoOptionInstalled As Short
    Public EOModule As Short
    Public STestComplete As Integer
    Public IDList(10) As String
    Public IDData() As String
    Public SwitchCardOK(5) As Integer
    Public ProgramPath As String
    Public M9selftestPath As String
    Public ReadBuffer As String = Space(256)
    Public SessionHandle As Integer
    Public DetailStatus As Integer
    Public SystemStatus As Integer
    Public lpBuffer As String = Space(256)
    Public SystemDirectory As String
    Public IgnorePressed As Integer
    Public CalCancel As Short
    Public STLogHandle As Integer
    Public STfileHandle As Integer
    Public STestBusy As Boolean = False
    Public DigitalModule As Integer

    Public CloseProgram As Boolean
    Public RunningEndToEnd As Boolean
    Public AbortTest As Integer
    Public PauseTest As Boolean
    Public AbortDump As Boolean
    Public SearchWord As String
    Public SwitchTest As Short
    Public Door_open As Boolean
    Public VEO2PowerOn As Integer
    Public ExternalPower As Integer
    Public ReceiverSwitchOK As Integer
    Public WarningMsg As String
    Public Timer2timeout As Integer
    Public ControlStartTest As Integer
    Public StartPPUTest As Integer
    Public StartDTSTest As Integer
    Public MoveW22 As Short
    Public Azimuth As String
    Public Elevation As String
    Public HelpContextID As Integer
    Public TestRunning As Integer
    Public TestingMessage As String = ""
    Public StartReady As Integer
    Public LastWriteMessage As String = ""
    'cicl stuff
    Public LiveMode(40) As Integer
    Public ResourceName(40) As String
    Public PsResourceName(10) As String
    Public ComResourceName(10) As String

    Public sMsg As String
    Public sTNum As String
    Public nErr As Integer
    Public sQuote As String = Chr(34)
    Public lBytesRead As Integer
    Public nSystErr As Integer
    Public dMeasurement As Double

    Public iTestNumXX As Short 'Two digit test number defined by Function ReturnTestNumber()
    Public dFH_UpperLimit As Double 'Upper Measurement value reported to the FHDB
    Public dFH_LowerLimit As Double 'Lower Measurement value reported to the FHDB
    Public dFH_Meas As Double 'The Measurement value reported to the FHDB
    Public sFailed_Step As String 'Unique test number defined by Function ReturnTestNumber()

    Public CrossFailure As Boolean
    Public RS343Video As Boolean
    Public iSelGUI As Integer 'Index of selected GUI button, label, etc.
    Public iSelInst As Integer 'Index of selected instrument
    Public bPassed As Boolean 'Identifies an Instrument Failure

    Public AppHandle As Integer
    Public Const PROCESS_TERMINATE As Short = 1

    Public bCalCancel As Boolean 'Indicates a 'Cancel' from the cal date form
    '  Public bSendingAltF4 As Boolean 'To prevent STest Close when running Diag32.exe under Windows 2000
    Public LastCalibration As String
    Public RFSTIM_10MHzON As Boolean = False
    Public UserName As String = System.Environment.UserName

    'Repetitive Strings
    Public Const FAILED_PREVIOUSLY As String = " FAILED during testing of previous module."
    Public Const NO_COMM As String = "Unable to establish instrument communication with the "

    ' Keyboard constants
    Public Const key_backspace As Short = 8
    Public Const key_tab As Short = 9
    Public Const key_Enter As Short = 13
    Public Const key_Return As Short = 13
    Public Const key_LineFeed As Short = 10
    Public Const key_escape As Short = 27
    Public Const key_left As Short = 37
    Public Const key_up As Short = 38
    Public Const key_right As Short = 39
    Public Const key_down As Short = 40
    Public Const key_delete As Short = 46
    Public Const key_F1 As Short = &H70
    Public Const key_F2 As Short = &H71
    Public Const key_F3 As Short = &H72
    Public Const key_F4 As Short = &H73
    Public Const key_F5 As Short = &H74
    Public Const key_F6 As Short = &H75
    Public Const key_F7 As Short = &H76
    Public Const key_F8 As Short = &H77
    Public Const key_F9 As Short = &H78
    Public Const key_F10 As Short = &H79
    Public Const key_F11 As Short = &H7A
    Public Const key_F12 As Short = &H7B

    'Added Run Option Constants
    Public Const SOEmode As Short = 0 ' stop on end
    Public Const LOEmode As Short = 1 ' loop on end
    Public Const LOSmode As Short = 2 ' loop on step
    Public Const SOFmode As Short = 10 ' stop on fault
    Public Const COFmode As Short = 11 ' continue on fault
    Public OptionMode As Short ' 0,1,2
    Public OptionFaultMode As Short ' 10,11
    Public OptionTestName As String ' "C/T",,,
    Public OptionStep As Integer '
    Public FirstPass As Integer
    Public TestPassedCount As Integer
    Public TestFailedCount As Integer
    Public StepPassedCount As Integer
    Public StepFailedCount As Integer
    Public GoBack As Integer
    Public RunEOtests As Integer

    Public gRetVal As Integer

    Public MouseButtonX As Long
    Public KeyShiftX As Long
    Public TestIndex As Integer
    Public StartDCPSTest As Integer

    Public CloseRelay As Short
    Public SetSlave As Short
    Public SenseRemote As Short
    Public CurrentConstant As Short
    Public OpenRelay As Short
    Public SetMaster As Short
    Public SenseLocal As Short
    Public CurrentLimit As Short


    Public STversion As String ' keeps the current version information (See StestMain/Main sub)
    Public StartTime As String
    Public sLogFilePath As String
    Public LogFileStartup As Integer = 0
    Public sATS_INI As String ' includes the path
    Public sAts_INI_path As String ' just thepath
    Public sLarrs_path As String
    Public ErrorDetected As Integer
    Public ErrorStatus As String
    Const MAX_PATH As Integer = 260


    <StructLayout(LayoutKind.Sequential)> _
    Structure PROCESSENTRY32
        Dim dwSize As Integer
        Dim cntUsage As Integer
        Dim th32ProcessID As Integer
        Dim th32DefaultHeapID As Integer
        Dim th32ModuleID As Integer
        Dim cntThreads As Integer
        Dim th32ParentProcessID As Integer
        Dim pcPriClassBase As Integer
        Dim dwFlags As Integer
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=MAX_PATH), VBFixedString(MAX_PATH)> Dim szexeFile As String
    End Structure

    Public Const SYNCHRONIZE As Integer = &H100000
    Public Const INFINITE As Integer = &HFFFF



    'sample ATS.ini - File Locations
    '   [File Locations]
    '    VXI_RESMAN=C:\Program Files (x86)\National Instruments\VXI\RESMAN.EXE
    '    SAIS_TOOLBAR=C:\Program Files (x86)\ATS\SAIS\SAISMGR.EXE
    '    PAWS_RTS=C:\USR\tyx\bin\wrts.exe
    '    IADS_READER=C:\iads\PROGRAMS\READR.EXE
    '    SYSTEM_CONF_TEST=C:\Program Files (x86)\ATS\System Test\CTEST.EXE
    '    SYSTEM_SELF_TEST=C:\Program Files (x86)\ATS\System Test\STEST.EXE
    '    SYSTEM_LOG=C:\Program Files (x86)\ATS\LOG\SYSLOG.EXE
    '    SYSTEM_CALIBRATION=D:\SYSCAL.EXE
    '    TETS_IATM=D:\ATS\ATS.IDE
    '    SYSTEM_MENU=C:\Program Files (x86)\ATS\SYSMENU.EXE
    '    PLC_GEN=C:\Program Files (x86)\ATS\System Test\PLC.EXE
    '    PLC_DATA=C:\Program Files (x86)\ATS\System Test\PLC.DAT
    '    PLC_LFS_DATA=C:\Program Files (x86)\ATS\System Test\Plc_lfs.dat
    '    SYSTEM_CONF_TEST_C=System Confidence Test
    '    SYSTEM_SELF_TEST_C=System Self Test
    '    FHDB_DATABASE=C:\Program Files (x86)\ATS\FHDB\FHDB.mdb
    '    FHDB_PROCESSOR=C:\Program Files (x86)\ATS\FHDB\FHDB_Processor.EXE
    '    FHDB_EXPORT=A:\Export.zip
    '    FHDB_IMPORT=A:\Import.zip
    '    TIPS=C:\Program Files (x86)\ATS\tipstudio.exe
    '    ITEX_BIN_CNF=C:\ITEX41\BIN\Wbincn32.exe

    Public Sub KillApp(ByVal myName As String)
        For Each prog As Process In Process.GetProcesses
            If LCase(prog.ProcessName) = LCase(myName) Then
                prog.Kill()
            End If
        Next
    End Sub

    Public Function AppRunning(ByVal myName As String) As Boolean
        AppRunning = False
        For Each prog As Process In Process.GetProcesses
            If LCase(prog.ProcessName) = LCase(myName) Then
                AppRunning = True
            End If
        Next
    End Function





    '#Const defUse_sGetKey = True
#If defUse_sGetKey Then
    Public Function sGetKey(ByVal sSection As String, ByVal sKey As String, Optional ByVal sDefault As String = "") As String
        sGetKey = ""
        'DESCRIPTION:
        '   This function retrieves a key value from ATS.INI
        'PARAMETERS:
        '   sSection    The ATS.INI file section where the key is located.
        '   sKey        The Key name.
        'RETURNS:
        '   The key value if found, else empty string
        'EXAMPLE:
        '   sTipCmds = sGetKey("TIPS", "CMD")
        'Check for module serial number.
        '  sSerialNum = sGetKey("Serial Number", InstFileName(iModIdx), "UNSET")

        Dim lpBuffer As string = Space(256)
        Dim nBytesRead As Integer

        nBytesRead = GetPrivateProfileString(sSection, sKey, sDefault, lpBuffer, 256, sATS_INI)
        If nBytesRead>0 Then
            sGetKey = Strings.Left(lpBuffer, nBytesRead)
        Else
            sGetKey = sDefault
        End If

    End Function
#End If


    Function ReturnTestNumber(Optional ByVal iInstrumentIndex As Integer = 0, Optional ByVal iTestNum As Integer = 0, Optional ByVal iSubTestNum As Integer = 1, Optional ByVal vPrefix As String = "") As String
        ReturnTestNumber = ""
        '************************************************************************************************
        '***    Returns a test number in the TTT-XX-SSS format.        TTT-XX-SSS                     ***
        '***    TTT : Instrument Identification code                                                  ***
        '***    XX  : Two digit test number (Begins with 01 for each Instrument, except for           ***
        '***          Inialization, which will be 00)                                                 ***
        '***    SSS : Three digit subtest number (begins with 001)                                    ***
        '***    vPrefix:  Prefix to Indentify Step in the Sub Test Number.                            ***
        '************************************************************************************************
        Dim sInstrumentID As String 'Instrument Identification String

        '(Initialization) an "N" is placed in the first digits place of the Sub Test Number
        '(Diagnostic) a "D" is placed in the first digits place of the Sub Test Number

        If Not IsNothing(vPrefix) Then 'If vPrefix contains a value
            If vPrefix.Length > 1 Then 'Two Character Prefix
                ReturnTestNumber = sInstrumentIDCode(iInstrumentIndex) & "-" & Format(iTestNum, "0#") & "-" & vPrefix & Format(iSubTestNum, "0")
            Else                'One character Prefix
                ReturnTestNumber = sInstrumentIDCode(iInstrumentIndex) & "-" & Format(iTestNum, "0#") & "-" & vPrefix & Format(iSubTestNum, "0#")
            End If
        Else
            'If the Instrument Index is missing, Default to a General Failure.
            If IsNothing(iInstrumentIndex) Then
                sInstrumentID = "GEN"
            Else
                sInstrumentID = sInstrumentIDCode(iInstrumentIndex)
            End If

            ReturnTestNumber = sInstrumentID & "-" & Format(iTestNum, "0#") & "-" & Format(iSubTestNum, "0##")

        End If

    End Function


    Public Sub RecordTest(ByVal sfailstep As String, ByVal MeasComment As String, ByVal LowLimitIn As Double, ByVal HighLimitIn As Double, ByVal MeasuredIn As Double, ByVal Units As String, Optional ByRef iDPlaces As Integer = 0)
        'DESCRIPTION:
        '   This routine records the formatted results of a test using the echo subroutine
        'PARAMETERS:
        '   MeasComment = This is the name of the test to be recorded
        '   LowLimitIn#  = This is the lower tolerance limit of the test
        '   HighLimitIn# = This is the upper tolerance limit of the test
        '   MeasuredIn#  = This is the actual measured value
        '   Units       = This is the units of the test
        '   iDPlaces     = Number of decimal places for result (3, 6 or 9)

        Dim LowLimit As String = ""
        Dim HighLimit As String = ""
        Dim MeasText As String = ""
        Dim Measurment As Double = 0
        Dim StatusText As String = ""
        Dim MinField As String = ""
        Dim MaxField As String = ""
        Dim MeasField As String = ""
        Dim i As Integer

        If (iDPlaces = 0) Or IsNothing(iDPlaces) Then
            iDPlaces = 3
        End If

        If (MeasuredIn < LowLimitIn) And (LowLimitIn <> 0) Then
            bPassed = False
        ElseIf (MeasuredIn > HighLimitIn) Then
            bPassed = False
        Else
            bPassed = True
        End If
        LowLimit = CStr(LowLimitIn)
        HighLimit = CStr(HighLimitIn)
        If MeasuredIn = 0 Then
            MeasText = CStr(MeasuredIn)
        Else
            MeasText = Format(MeasuredIn, "#.####################")
        End If
        Measurment = MeasuredIn

        ' no low or high limits (no pass fail?)
        If LowLimit = "" And HighLimit = "" Then
            Echo(sfailstep & " " & MeasComment & " = " & MeasText & Units)

            ' both low and high limits
        ElseIf LowLimit <> "" And HighLimit <> "" Then
            Echo(sfailstep & " " & MeasComment)
            If (Val(LowLimit) <> 0) Or (Units <> "Ohms") Then
                If (Measurment >= Val(LowLimit)) And (Measurment <= Val(HighLimit)) And (Measurment < 9.0E+38) Then
                    StatusText = "PASSED"
                Else
                    StatusText = "FAILED"
                End If
            Else
                If (Measurment <= Val(HighLimit)) And (Measurment < 9.0E+38) Then
                    StatusText = "PASSED"
                Else
                    StatusText = "FAILED"
                End If
            End If
            Select Case Trim(Units)
                Case "Rad/sec", "uW/cm2-sr", "uW/cm2", "nW/cm2", "dBm", "msec", "usec", Chr(181) & "sec", "meters", "DAC", ""
                    MinField = "    Min: " & LowLimit & " " & Units
                    MinField &= Space(25 - Len(MinField))
                    MaxField = "Max: " & HighLimit & " " & Units
                    MaxField &= Space(21 - Len(MaxField))
                    i = InStr(MeasText, ".")
                    If (i > 0) And (i < Len(MeasText) - iDPlaces - 1) Then
                        MeasText = Strings.Left(MeasText, i + iDPlaces)
                    End If
                    MeasField = "Meas: " & MeasText & " " & Units
                    If Len(MinField & " " & MaxField & " " & MeasField) < 70 Then
                        MeasField &= Space(67 - Len(MinField & MaxField & MeasField))
                        Echo(MinField & " " & MaxField & " " & MeasField & StatusText)
                    Else
                        MeasField = "     " & MeasField & Space(64 - Len(MeasField))
                        Echo(MinField & " " & MaxField)
                        Echo(MeasField & StatusText)
                    End If

                Case Else
                    If (SwitchTest = True) And (LowLimit = " 0") Then
                        MinField = Space(24)
                    Else
                        If iDPlaces = 3 Then
                            MinField = "    Min: " & EngNotate(Val(LowLimit)) & Units
                        Else
                            MinField = "    Min: " & EngNotate(Val(LowLimit), iDPlaces) & Units
                        End If
                        If Strings.Right(MinField, 4) = "KMhz" Then
                            MinField = Strings.Left(MinField, Len(MinField) - 4) & "Ghz"
                        End If
                        If Len(MinField) < 24 Then
                            MinField &= Space(24 - Len(MinField))
                        Else
                            MinField &= " "
                        End If
                    End If
                    If iDPlaces = 3 Then
                        MaxField = "Max: " & EngNotate(Val(HighLimit)) & Units
                    Else
                        MaxField = "Max: " & EngNotate(Val(HighLimit), iDPlaces) & Units

                    End If
                    If Strings.Right(MaxField, 4) = "KMhz" Then
                        MaxField = Strings.Left(MaxField, Len(MaxField) - 4) & "Ghz"
                    End If
                    If Len(MaxField) < 20 Then
                        MaxField &= Space(20 - Len(MaxField))
                    Else
                        MaxField &= " "
                    End If
                    If iDPlaces = 3 Then
                        MeasField = "Meas: " & EngNotate(Val(MeasText)) & Units & " "
                    Else
                        MeasField = "Meas: " & EngNotate(Val(MeasText), iDPlaces) & Units & " "
                    End If
                    If Strings.Right(MeasField, 4) = "KMhz" Then
                        MeasField = Strings.Left(MeasField, Len(MeasField) - 4) & "Ghz"
                    End If
                    If Len(MinField & MaxField & MeasField) < 69 Then
                        MeasField &= Space(69 - Len(MinField & MaxField & MeasField))
                    End If
                    If Len(MinField & MaxField & MeasField & StatusText) < 76 Then
                        Echo(MinField & MaxField & MeasField & StatusText)
                    Else
                        Echo(MinField & MaxField & MeasField)
                        Echo(Space(75 - Len(StatusText)) & StatusText)
                    End If
            End Select

            ' lower limit only
        ElseIf LowLimit <> "" And HighLimit = "" Then
            Echo(sfailstep & " " & MeasComment)
            If (Measurment >= Val(LowLimit)) And (Measurment < 9.0E+38) Then
                StatusText = "PASSED"
            Else
                StatusText = "FAILED"
            End If
            If iDPlaces = 3 Then
                MinField = "    Min: " & EngNotate(Val(LowLimit)) & Units
                MinField &= Space(44 - Len(MinField))
                MeasField = "Measured: " & EngNotate(Val(MeasText)) & Units
            Else
                MinField = "    Min: " & EngNotate(Val(LowLimit), iDPlaces) & Units
                MinField &= Space(44 - Len(MinField))
                MeasField = "Measured: " & EngNotate(Val(MeasText), iDPlaces) & Units
            End If
            MeasField &= Space(25 - Len(MeasField))
            Echo(MinField & MaxField & MeasField & StatusText)

            ' upper limit only
        Else
            Echo(sfailstep & " " & MeasComment)
            If (Measurment <= Val(HighLimit)) Then
                StatusText = "PASSED"
            Else
                StatusText = "FAILED"
            End If
            If iDPlaces = 3 Then
                MaxField = "    Max: " & EngNotate(Val(HighLimit)) & Units
                MaxField &= Space(44 - Len(MaxField))
                MeasField = "Measured: " & EngNotate(Val(MeasText)) & Units
            Else
                MaxField = "    Max: " & EngNotate(Val(HighLimit), iDPlaces) & Units
                MaxField &= Space(44 - Len(MaxField))
                MeasField = "Measured: " & EngNotate(Val(MeasText), iDPlaces) & Units
            End If
            MeasField &= Space(25 - Len(MeasField))
            Echo(MinField & MaxField & MeasField & StatusText)
        End If

        If StatusText = "FAILED" Then
            'Record Failure Record in the FHDB
            WriteData(CStr(dtTestStartTime), CStr(Now), 0, sfailstep, MeasComment, Measurment, Units, HighLimitIn, LowLimitIn)
        End If

    End Sub

    Sub ResetSystem(ByVal ResetSupplies As Boolean)
        'DESCRIPTION:
        '   This routine resets the system.  If InCompleteReset is false then the power
        '   supplies are not reset due to the lenth of time to do so.
        'PARAMETERS:
        '   ResetSupplies = if true then Power Supplies are reset.
        Dim Supply As Integer
        Dim InitStatus As Integer

        If ResetSupplies Then
            For Supply = 1 To 10
                CommandSupplyReset(Supply)
            Next
        End If

        nSystErr = vxiClear(DMM)
        nSystErr = vxiClear(COUNTER)
        nSystErr = vxiClear(ARB)
        nSystErr = vxiClear(OSCOPE)
        nSystErr = vxiClear(FGEN)
        nSystErr = vxiClear(SWITCH1)

        If RFOptionInstalled Then
            nSystErr = vxiClear(RFPM)
            nSystErr = vxiClear(RFSYN)
        End If

        nSystErr = WriteMsg(DMM, "*RST")
        nSystErr = WriteMsg(DMM, "*CLS")
        nSystErr = WriteMsg(COUNTER, "*RST")
        nSystErr = WriteMsg(COUNTER, "*CLS")
        nSystErr = WriteMsg(ARB, "*RST")
        nSystErr = WriteMsg(ARB, "*CLS")
        nSystErr = WriteMsg(ARB, "MARK:STAT OFF") 'Disable the AFG to output a marker list    
        nSystErr = WriteMsg(ARB, "OUTP:LOAD 50")
        nSystErr = WriteMsg(OSCOPE, "*RST")
        nSystErr = WriteMsg(OSCOPE, "*CLS")
        nSystErr = WriteMsg(FGEN, "*RST")
        nSystErr = WriteMsg(FGEN, "*CLS")
        nSystErr = WriteMsg(SWITCH1, "RESET")
        ' close interconnect relays
        nSystErr = WriteMsg(SWITCH1, "OPEN 3.1000,1002,2000,2001,3002,4002,5000,5001,5002,6002,7002,8002")
        nSystErr = WriteMsg(SWITCH1, "CLOSE 3.1001,2002,3000,3001,4000,4001,6000,6001,7000,7001")


        If RFOptionInstalled Then
            nSystErr = WriteMsg(RFPM, "*RST")
            nSystErr = WriteMsg(RFPM, "*CLS")
            nSystErr = WriteMsg(RFSYN, "*RST")
            nSystErr = WriteMsg(RFSYN, "*CLS")
        End If
        If InstrumentInitialized(DIGITAL) <> 0 Then
            InitStatus = terM9_init("TERM9::#0", VI_TRUE, VI_TRUE, instrumentHandle(DIGITAL))
            nSystErr = terM9_reset(instrumentHandle(DIGITAL))
            InitStatus = terM9_close(instrumentHandle(DIGITAL))
        End If

    End Sub


    Sub DisplaySetup(ByVal Caption As String, ByVal Graphic As String, ByVal LineCount As Integer, Optional ByVal BackOK As Integer = False, Optional ByVal BackNo As Integer = 0, Optional ByVal BackCount As Integer = 0)

        'DESCRIPTION
        '   This routine displays frmDirections, displays a caption on it to explain
        '   to the operator what manual intervention to perform, and loads the
        '   graphic file associated with this action
        'PARAMETERS
        '   Caption  = This is the caption to be displayed on the form
        '   Graphic  = This is the file name of the graphic to be displayed (426 x 222 pixels)

        frmSTest.WindowState = FormWindowState.Normal
        GoBack = False

        Application.DoEvents()
        If LineCount < 2 Then
            frmDirections.txtDirections.Text = "Perform the following operation:  " & vbCrLf & vbCrLf & " " & Caption
        Else
            frmDirections.txtDirections.Text = "Perform the following " & CStr(LineCount) & " operations:  " & vbCrLf & vbCrLf & Caption
        End If

        frmDirections.cmdBack.Visible = False
        frmDirections.Text = "Test Setup Instructions "
        If BackOK = True Then
            If BackNo > 1 Then
                frmDirections.cmdBack.Visible = True
            End If
            frmDirections.Text = "Test Setup Instructions (" & CStr(BackNo) & " of " & CStr(BackCount) & ")"
        End If

        On Error Resume Next
        If Dir(ProgramPath & "GRAPHICS\" & Graphic, CType(17, FileAttribute)) <> "" Then
            frmDirections.picGraphic.Image = LoadPicture(ProgramPath & "GRAPHICS\" & Graphic)
        Else
            frmDirections.picGraphic.Image = LoadPicture(ProgramPath & "GRAPHICS\APN_PCU.jpg")
        End If

        frmDirections.TopMost = True
        frmDirections.ShowDialog()


    End Sub


    Sub EnterNewCalDate(ByVal instrumentIndex As Integer)
        'DESCRIPTION:
        '   This routine prompts the user to enter a new calibration date.
        'PARAMETERS:
        '   InstrumentIndex = The index of the instrument to have be given a new cal date.
        '   Dim IDList(5)
        Dim lpBuffer As String = Space(256)
        Dim FileError As Integer
        Dim StoredSerialNumber As String
        Dim CalDate As String
        Dim ReturnValue As Integer
        Dim UserResponse As Integer

        'get stored serial number if it exists
        FileError = GetPrivateProfileString("Serial Number", InstFileName(instrumentIndex), "", lpBuffer, 256, sATS_INI)
        StoredSerialNumber = Trim(StripNullCharacters(lpBuffer))
        'get stored CalDate if it exists
        lpBuffer = Space(256)
        FileError = GetPrivateProfileString("Calibration", InstFileName(instrumentIndex), "", lpBuffer, 256, sATS_INI)
        CalDate = Trim(StripNullCharacters(lpBuffer))

        Select Case instrumentIndex
            'Do Nothing - These either don't require calibration or they have internal calibration dates.
            Case MIL_STD_1553, APROBE, DIGITAL, DMM, ARB, PPU
            Case SWITCH1, SWITCH2, SWITCH3, SWITCH4
            Case SERIALCCA, RS422, RS232, GIGA, CANBUS
            Case EO_VCC

            Case FGEN, COUNTER, OSCOPE, RFSYN
                ' Get cal date from user, save in sATS.ini file
                UserResponse = MsgBox("Has the " & InstrumentDescription(instrumentIndex) & " been replaced?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Self Test")
                If UserResponse = DialogResult.Yes Then
                    CalDate = GetCalDueDateFromUser(instrumentIndex, 0)
                    If CalDate <> "" Then
                        ReturnValue = WritePrivateProfileString("Calibration", InstFileName(instrumentIndex), CalDate, sATS_INI)
                    End If
                End If

            Case SWITCH5
                UserResponse = MsgBox("Has the " & InstrumentDescription(instrumentIndex) & " been replaced?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Self Test")
                If UserResponse = DialogResult.Yes Then
                    lpBuffer = ""
                    FileError = GetPrivateProfileString("File Locations", "PLC_GEN", "", lpBuffer, 256, sATS_INI)
                    FileError = Shell("""" & Trim(StripNullCharacters(lpBuffer)) & """", AppWinStyle.NormalFocus)
                End If

            Case VEO2
                UserResponse = MsgBox("Has the " & InstrumentDescription(VEO2) & " been replaced?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Self Test")
                If UserResponse = DialogResult.Yes Then
                    CalDate = GetCalDueDateFromUser(VEO2, 0)
                    If CalDate <> "" Then
                        ReturnValue = WritePrivateProfileString("Calibration", InstFileName(VEO2), CalDate, sATS_INI)
                    End If
                End If

        End Select

    End Sub

    Function FetchMeasurement(ByVal Instrument As Integer, ByRef MeasComment As String, ByRef LowLimit As String, ByRef HighLimit As String, ByRef Units As String, Optional ByVal iFailingInstrumentIndex As Integer = 0, Optional ByVal sTestNum As String = "", Optional ByVal sSwitchCallout As String = "") As Double
        'DESCRIPTION:
        '   This routine grabs a measurement from a specified instrument,
        '   converts it to an floating point value, echo's the test name to
        '   the screen, echos the measurement to the screen with its high and
        '   low limits and reports a pass or fail message to the screen.
        '   If the contents of LowLimit or HighLimit are empty, then that
        '   parameter is not used in determining the pass/fail criterion.
        'PARAMETERS:
        '   Instrument  = The handle to the instrument from which to
        '                  obtain the measurement
        '   MeasComment = Some text which identifies the measurement being
        '                  taken
        '   LowLimit/   = The high and low limits of the measurement
        '   HighLimit
        '   Unit        = Unit of the measurement
        '   iFailingInstrumentIndex = The Index to ID the Failing Instrument
        '   sTestNum = Instrument Test Number
        'RETURNS:
        '   A floating point value equivalent to the instruments measurement

        'DR# 266, 03/18/02
        'Added Optional Argument "sSwitchCallout" to pass Failing Switch Connection Info
        'from function ClosureDetected() to record in the FHDB in the event of a failure.

        Dim StrMeasurement As String = ""
        Dim LowLimt As String = ""
        Dim MeasText As String = ""
        Dim StatusText As String = ""
        Dim MinField As String = ""
        Dim MaxField As String = ""
        Dim MeasField As String = ""
        Dim sFaultCallout As String = "" 'String value to record in the FHDB FaultCallout Field
        Dim kHzFlg As Boolean

        'Format Comment Line
        sFaultCallout = MeasComment
        If MeasComment.Length > 60 Then
            MeasComment = sTestNum & vbCrLf & MeasComment
        Else
            MeasComment = sTestNum & " " & MeasComment
        End If

        If Units = "kHz" Then
            kHzFlg = True
            Units = "Hz"
            LowLimit = CStr(1000 * CDbl(LowLimit))
            HighLimit = CStr(1000 * CDbl(HighLimit))
        Else
            kHzFlg = False
        End If

        nSystErr = ReadMsg(Instrument, StrMeasurement)
        If nSystErr <> 0 Then
            If HighLimit = "" And LowLimt <> "" Then
                dMeasurement = -9.9E+38
            Else
                dMeasurement = 9.9E+38
            End If
            MeasText = "NO MEAS "
        Else
            dMeasurement = Val(StrMeasurement)
            If kHzFlg Then dMeasurement = 1000 * dMeasurement
            MeasText = EngNotate(dMeasurement)
        End If

        If LowLimit = "" And HighLimit = "" Then
            Echo(MeasComment & " = " & MeasText & Units)
        ElseIf LowLimit <> "" And HighLimit <> "" Then
            Echo(MeasComment)
            If (dMeasurement > Val(LowLimit)) And (dMeasurement < Val(HighLimit)) And (dMeasurement < 9.0E+38) Then
                StatusText = "PASSED"
            Else
                StatusText = "FAILED"
            End If
            MinField = "    Min: " & EngNotate(Val(LowLimit)) & Units
            MinField &= Space(24 - Len(MinField))
            MaxField = "Max: " & EngNotate(Val(HighLimit)) & Units
            MaxField &= Space(20 - Len(MaxField))

            MeasField = "Measured: " & MeasText & Units
            '===============================================================================
            MeasField &= Space(25 - Len(MeasField))
            Echo(MinField & MaxField & MeasField & StatusText)
        ElseIf LowLimit <> "" And HighLimit = "" Then            '"Greater-Than" test
            Echo(MeasComment)
            If (dMeasurement > Val(LowLimit)) And (dMeasurement < 9.0E+38) Then
                StatusText = "PASSED"
            Else
                StatusText = "FAILED"
            End If
            MinField = "    Min: " & EngNotate(Val(LowLimit)) & Units
            MinField &= Space(44 - Len(MinField))

            MeasField = "Measured: " & MeasText & Units
            '===============================================================================
            MeasField &= Space(25 - Len(MeasField))
            Echo(MinField & MeasField & StatusText)
            HighLimit = "+9.9E+38"
        Else            '"Less-Than" test
            Echo(MeasComment)
            If dMeasurement < Val(HighLimit) Then
                StatusText = "PASSED"
            Else
                StatusText = "FAILED"
            End If
            MaxField = "    Max: " & EngNotate(Val(HighLimit)) & Units
            MaxField = Space(20) & MaxField & Space(24 - Len(MaxField))

            MeasField = "Measured: " & MeasText & Units
            '===============================================================================
            MeasField &= Space(25 - Len(MeasField))
            Echo(MaxField & MeasField & StatusText)
            LowLimit = "-9.9E+38"
        End If

        If StatusText = "FAILED" Then 'Record failure in FHDB
            If sSwitchCallout <> "" Then 'If there is switch connection info, add to FaultCallout field
                sFaultCallout &= vbCrLf & sSwitchCallout
            End If
            RegisterFailure(CStr(iFailingInstrumentIndex), sTestNum, dMeasurement, Units, Val(LowLimit), Val(HighLimit), sFaultCallout)
        End If

        FetchMeasurement = dMeasurement

    End Function



    Private Function GetCalDueDateFromUser(ByVal iInst As Integer, ByVal lDateSeed As Integer) As String
        GetCalDueDateFromUser = ""
        'DESCRIPTION:
        '   This routine obtains a date from the user, suggesting today.
        'PARAMETER:
        '   iInst:      Instrument index
        '                 or if 8481 then for 8481A Sensor
        '                 or if -8481 then for 8481D Sensor
        '   lDateSeed:  0 for today, otherwise DateSerial code
        'RETURNS:
        '   The date of calibration selected by the user in DateSerial format

        Dim ValidDate As Boolean
        Dim TheDayToday As Integer, TheMonthToday As Integer, TheYearToday As Integer
        Dim TheDay As Integer, TheMonth As Integer, TheYear As Integer
        Dim TheMonthText As String
        Dim sDateSeed As String

        If lDateSeed = 0 Then
            sDateSeed = CStr(Today)
        Else
            sDateSeed = Format(lDateSeed, "m-d-yyyy")
        End If

        With frmCalDate
            ValidDate = False
            .txtDay.Text = Format(sDateSeed, "d")
            .txtMonth.Text = Format(sDateSeed, "m")
            .txtYear.Text = Format(sDateSeed, "yyyy")
            TheDayToday = CInt(Val(Format(Today, "d")))
            TheMonthToday = CInt(Val(Format(Today, "m")))
            TheYearToday = CInt(Val(Format(Today, "yyyy")))

            CenterForm(frmCalDate)
            .chkIsDueDate.Visible = True
            If iInst = 8481 Then
                .lblPrompt.Text = "Enter calibration date printed on the HP 8481A power sensor." & vbCrLf & "Indicate if it is a 'Due Date' by checking the box below. Otherwise, it is considered the 'Calibration Date'."
                .chkIsDueDate.Checked = CBool(CheckState.Checked)
            ElseIf iInst = -8481 Then
                .lblPrompt.Text = "Enter calibration date printed on the HP 8481D power sensor." & vbCrLf & "Indicate if it is a 'Due Date' by checking the box below. Otherwise, it is considered the 'Calibration Date'."
                .chkIsDueDate.Checked = CBool(CheckState.Checked)
            Else
                .lblPrompt.Text = "Enter calibration date for" & vbCrLf & InstrumentDescription(iInst) & "." & vbCrLf & "Indicate if it is a 'Due Date' by checking the box below. Otherwise, it is considered the 'Calibration Date'."
                .chkIsDueDate.Checked = CBool(CheckState.Unchecked)
            End If

            Do
                .ShowDialog() 'modal
                If bCalCancel Then
                    bCalCancel = False
                    GetCalDueDateFromUser = ""
                    Exit Function
                End If

                If Not (IsNumeric(.txtDay.Text) And IsNumeric(.txtMonth.Text) And IsNumeric(.txtYear.Text)) Then
                    MsgBox("Invalid Date.  Non-numeric entry.  Only numeric values excepted.")
                    GoTo EndOfLoop
                End If

                TheDay = CInt(Val(.txtDay.Text))
                TheMonth = CInt(Val(.txtMonth.Text))
                TheYear = CInt(Val(.txtYear.Text))

                If TheYear < 100 Then
                    If TheYear > 80 Then
                        TheYear += 1900
                        .txtYear.Text = CStr(TheYear)
                    Else
                        TheYear += 2000
                        .txtYear.Text = CStr(TheYear)
                    End If
                End If

                If TheMonth > 12 Or TheMonth < 1 Then
                    MsgBox("Invalid Date.  Valid month values are 1 through 12.")
                    GoTo EndOfLoop
                End If

                Select Case TheMonth
                    Case 2 'Feb
                        If (TheYear Mod 4) = 0 Then 'leap year
                            If TheDay > 29 Or TheMonth < 1 Then
                                MsgBox("Invalid Date.  Valid day values for February in a leap year are 1 through 29.")
                                GoTo EndOfLoop
                            End If
                        Else
                            If TheDay > 28 Or TheMonth < 1 Then
                                MsgBox("Invalid Date.  Valid day values for February in a non-leap year are 1 through 28.")
                                GoTo EndOfLoop
                            End If
                        End If
                    Case 1, 3, 5, 7, 8, 10, 12
                        TheMonthText = Format(31 * (TheMonth - 1) + 2, "mmmm")
                        If TheDay > 31 Or TheMonth < 1 Then
                            MsgBox("Invalid Date.  Valid day values for " & TheMonthText & " are 1 through 31.")
                            GoTo EndOfLoop
                        End If
                    Case 4, 6, 9, 11 ' Apr,Jun,Sept,Nov
                        TheMonthText = Format(31 * (TheMonth - 1) + 2, "mmmm")
                        If TheDay > 30 Or TheMonth < 1 Then
                            MsgBox("Invalid Date.  Valid day values for " & TheMonthText & " are 1 through 30.")
                            GoTo EndOfLoop
                        End If
                End Select

                ValidDate = True

                If IIf(frmCalDate.chkIsDueDate.Checked, 1, 0) = (CheckState.Unchecked) Then
                    If (Math.Abs(iInst) = 8481) Then
                        'If a sensor cal due date, then subtract 3 years
                        GetCalDueDateFromUser = CStr(CInt(DateSerial(TheYear + 3, TheMonth, TheDay).ToOADate))
                        Exit Function
                    Else
                        GetCalDueDateFromUser = CStr(CInt(DateSerial(TheYear + 1, TheMonth, TheDay).ToOADate))
                    End If
                Else
                    GetCalDueDateFromUser = CStr(CInt(DateSerial(TheYear, TheMonth, TheDay).ToOADate))
                End If

EndOfLoop:
            Loop While ValidDate = False
        End With
    End Function


    Function GetNowTime() As Double
        'DESCRIPTION:
        '   This routine returns the number of seconds since 1900
        'RETURNS:
        '   The Number of seconds since 1900

        GetNowTime = (TimeSerial(Hour(Now), Minute(Now), Second(Now)).ToOADate + DateSerial(Year(Now), Month(Now), DateAndTime.Day(Now)).ToOADate) * 24 * 60 * 60

    End Function


    Public Function dGetTime() As Double
        '************************************************************************************
        '* Nomenclature   : ATS-ViperT SYSTEM SELF TEST                                     *
        '* DESCRIPTION:                                                                     *
        '*   Determines the time since 1900.                                                *
        '*   Avoids errors caused by 'Timer' when crossing midnight.                        *
        '* PARAMETERS:                                                                      *
        '*    None                                                                          *
        '* RETURNS:                                                                         *
        '*   A double-precision floating-point number in the format d.s where:              *
        '*     d = the number of days since Dec 30, 1899 and                                *
        '*     s = the fraction of a day with 10 milli-second resolution                    *
        '* The CDbl(Now) function returns a number formatted d.s where:                     *
        '*   d = the number of days since 1900 and                                          *
        '*   s = the fraction of today with 1 second resolution.                            *
        '* CLng(Now) returns only the number of days (the "d." part) as a whole number.     *
        '************************************************************************************
        Try
            dGetTime = Now.ToOADate ' 'something like 42500.99999999
        Catch
            dGetTime = Now.ToOADate
        End Try

    End Function


    Function GetStartUpTime() As Double ' from ATS.ini file
        'DESCRIPTION:
        '   This routine gets the startup time from the sATS.ini file, ie 42523.65424
        'RETURNS:
        '   The number of days and time(fraction of day) since midnight December 30, 1899

        Dim lpBuffer As String = Space(256)
        Dim FileError As Integer
        Dim SomeBuffer As String
        ''  Dim SecondsSince1997 As Double

        '[System Startup]
        'STARTUP_TIME = 42523.65424
        FileError = GetPrivateProfileString("System Startup", "SYSTEM_STARTUP", "", lpBuffer, 256, sATS_INI)
        SomeBuffer = StripNullCharacters(lpBuffer)

        ''SecondsSince1997 = Val(SomeBuffer) * 24 * 60 * 60
        ''GetStartUpTime = SecondsSince1997

        GetStartUpTime = Val(SomeBuffer)

    End Function


    Function InitDigital() As Integer
        'DESCRIPTION:
        '   This Routine checks to see if the Digital Test System (DTS) is available and
        '   configured properly.
        'RETURNS:
        '   True if the DTS initializes and is properly configured
        Dim ErrorStatus As Integer
        Dim ManufID As Short
        Dim DevType As Short
        Dim iDevType As Short
        Dim StatusReg As Short
        Dim ErrorStatusRead As Integer
        Dim iSlot As Integer
        Dim sTestNum As String
        Dim sDesc As String = ""

        Echo("")
        For iSlot = 1 To 4
            Select Case iSlot
                Case 1
                    'Channel Card #1
                    InstrumentSpec(DIGITAL) = "VXI::33::INSTR"
                    sDesc = InstrumentDescription(DIGITAL) & " Slot 1 Channel Card "
                    iDevType = &H78A0
                Case 2
                    'Channel Card #2
                    InstrumentSpec(DIGITAL) = "VXI::34::INSTR"
                    sDesc = InstrumentDescription(DIGITAL) & " Slot 2 Channel Card "
                    iDevType = &H78A0
                Case 3
                    'Channel Card #3
                    InstrumentSpec(DIGITAL) = "VXI::35::INSTR"
                    sDesc = InstrumentDescription(DIGITAL) & " Slot 3 Channel Card "
                    iDevType = &H78A0
                Case 4
                    'Central Resource Board
                    InstrumentSpec(DIGITAL) = "VXI::36::INSTR"
                    sDesc = InstrumentDescription(DIGITAL) & " Slot 4 CRB "
                    iDevType = &H5CE0
            End Select

            'DTS-00-N01, DTS-00-N03, DTS-00-N05, DTS-00-N07
            sTestNum = "DTS-00-N" & Format((iSlot * 2) - 1, "00")
            Echo(sTestNum)
            ErrorStatus = viOpen(SessionHandle, InstrumentSpec(DIGITAL), VI_NULL, VI_NULL, instrumentHandle(DIGITAL))

            If ErrorStatus <> VI_SUCCESS Then
                Echo("Could not get instrument handle for " & InstrumentSpec(DIGITAL))
                FormatResultLine(sDesc & " initialization ", False)
                RegisterFailure(DIGITAL, sTestNum, sComment:="    Initialization Failure for " & sDesc & ". VISA Error Code: " & GetVisaErrorMessage(ResourceName(DIGITAL), ErrorStatus))
                InstrumentInitialized(DIGITAL) = False
                ReportFailure(DIGITAL)
                InitDigital = False
                Exit Function
            End If
            FormatResultLine(sDesc & " initialization ", True)

            'DTS-00-N02, DTS-00-N04, DTS-00-N06, DTS-00-N08
            sTestNum = ReturnTestNumber(DIGITAL, 0, (iSlot * 2), "N")
            Echo(sTestNum)
            ErrorStatusRead = viIn16(instrumentHandle(DIGITAL), VI_A16_SPACE, MANUF_REG, ManufID)
            ErrorStatusRead = viIn16(instrumentHandle(DIGITAL), VI_A16_SPACE, MODEL_REG, DevType)
            ErrorStatusRead = viIn16(instrumentHandle(DIGITAL), VI_A16_SPACE, STATUS_REG, StatusReg)

            Echo(InstrumentSpec(DIGITAL) & " Slot " & CStr(iSlot) & " => Manufacturer ID Register => " & Hex(ManufID) & " (Expected CEDF)")
            Echo(InstrumentSpec(DIGITAL) & " Slot " & CStr(iSlot) & " => Model ID Register => " & Hex(DevType) & " (Expected " & Hex(iDevType) & ")")
            Echo(InstrumentSpec(DIGITAL) & " Slot " & CStr(iSlot) & " => Status Register => " & Hex(StatusReg) & " (Expected C00C)")
            'check error status, manufacturer ID=&HCEDF, Device type=&H78A0 or , and status register=C00C
            If (ErrorStatusRead = VI_SUCCESS) And (ManufID = &HFFFFCEDF) And (DevType = iDevType) And (StatusReg = &HFFFFC00C) Then
                FormatResultLine(sDesc & " identification ", True)
            Else
                FormatResultLine(sDesc & " identification ", False)
                RegisterFailure(DIGITAL, sTestNum, sComment:="    Identification Failure for " & sDesc & ".")
                ReportFailure(DIGITAL)
                InitDigital = False
                Exit Function
            End If
        Next iSlot
        InitDigital = True

    End Function


    Function InitPowerSupplies() As Integer

        'DESCRIPTION:
        ' This routine Initializes the power supplies
        '   and places their handles in SupplyHandle&() array
        ' Note: Still need to get the handle for PS1
        '   for the Receiver Switch and the Aprobe tests
        Dim Supply As Short, mTry As Short
        Dim sPPU_Test_Number As String = ""
        Dim Nbr As Integer = 0

        Echo("")
        InitPowerSupplies = PASSED

        For mTry = 1 To 3 'Try multiple times to get a handle
            nSystErr = aps6062_init(ResourceNameX:="GPIB0::5::1", idQuery:=0, resetDevice:=0, instrumentHandle:=SupplyHandle(1))
            If nSystErr = VI_SUCCESS Then Exit For
            Delay(0.2)
        Next mTry

        For Supply = 1 To 10

            'P01-00-N02 ~ P10-00-N02
            sPPU_Test_Number = "P" & Format(Supply, "00") & "-00-N01" 'Build Test Number
            nSystErr = viSetAttribute(SupplyHandle(Supply), VI_ATTR_TMO_VALUE, 3000) ' 3 sec

            'Read Status
            For mTry = 1 To 5
                'Send Status Query
                nSystErr = atxmlDF_viWrite(PsResourceName(Supply), 0, CStr(Chr(Supply)) & CStr(Chr(&H44)) & CStr(Chr(0)), 3, Nbr)
                lpBuffer = Space(255)
                nSystErr = atxmlDF_viRead(PsResourceName(Supply), 0, lpBuffer, 255, Nbr)
                If Asc(Strings.Left(lpBuffer, 1)) <> 0 Then Exit For
            Next mTry

            'If MODE byte is zero, supply module is absent or dead
            If Asc(Strings.Left(lpBuffer, 1)) <> 0 Then
                'Identification PASSED
                FormatResultLine(sPPU_Test_Number & " " & InstrumentDescription(PPU) & " DC" & Supply & " identification ", True)
            Else                'Identification FAILED
                FormatResultLine(sPPU_Test_Number & " " & InstrumentDescription(PPU) & " DC" & Supply & " identification ", False)
                RegisterFailure(Cstr(PPU), sPPU_Test_Number, sComment:="    Identification Failure for Supply " & CStr(Supply) & ".")
                InitPowerSupplies = FAILED
                SupplyHandle(Supply) = 0
            End If
        Next Supply

        If InitPowerSupplies = FAILED Then
            ReportFailure(PPU)
        End If

    End Function

    Function GetVisaErrorMessage(ByVal ResourceNameX As String, ByVal statusCode As Integer) As String
        GetVisaErrorMessage = ""
        'DESCRIPTION:
        '   This routine obtains a string representation of the error code from a particular instrument
        'PARAMERTERS:
        '   Handle&     = The handle of the instrument with an error
        '   statusCode& = The error code from the VISA call
        'RETURNS:
        '   A string description of the VISA error

        '    nSystErr = viStatusDesc(Handle&, statusCode&, lpBuffer)

        '    nSystErr = atxmlDF_viStatusDesc(ResourceNameX, 0&, statusCode&, lpBuffer)
        '    GetVisaErrorMessage = StripNullCharacters(lpBuffer)
        GetVisaErrorMessage = ""

    End Function


    Sub PerformSelfTest(ByVal iButton As Integer)
        'DESCRIPTION:
        '   This routine performs the actual self test on an individual module
        Dim EndToEndTesting As Integer = UNTESTED
        Dim StartInst As Integer
        Dim StopInst As Integer

        Dim EndToEndStartTime As Date
        Dim EndToEndRunTime As Long
        Dim EndToEndStopTime As Date
        Dim StartTest As Date
        Dim StopTest As Date
        Dim LastTest As Integer
        Dim TestStatus As Integer
        Dim STMessage As String
        Dim StopEndToEnd As Integer
        Dim iResponse As Integer 'Holds the Response integer from the Messsage Box
        Dim sRemoveMsg As String
        Dim xx As Integer
        Dim i As Integer
        Dim j As Integer
        dim x As Integer
        Dim CommandData As String
        Dim iErrStatusCode As Integer
        Dim Nbr As Integer
        Dim TestPassedCountStart As Integer
        Dim TestFailedCountStart As Integer
        Dim StepPassedCountStart As Integer
        Dim StepFailedCountStart As Integer
        Dim iTestNumber As Integer ' bb using iSelGUI caused warnings?

        Dim Hrs As Integer
        Dim Mins As Integer
        Dim Secs As Integer
        Dim sTestTime As String = ""


        HelpContextID = 1100
        FirstPass = True
        TestPassedCount = 0
        TestFailedCount = 0
        StepPassedCount = 0
        StepFailedCount = 0
        frmSTest.lblPassCount.Text = CStr(TestPassedCount)
        frmSTest.lblFailCount.Text = CStr(TestFailedCount)
        frmSTest.lblStepPassCount.Text = CStr(StepPassedCount)
        frmSTest.lblStepFailCount.Text = CStr(StepFailedCount)

        Select Case iButton
            Case 0 : iSelInst = frmSTest.sscTest_0.Tag
            Case 1 : iSelInst = frmSTest.sscTest_1.Tag
            Case 2 : iSelInst = frmSTest.sscTest_2.Tag
            Case 3 : iSelInst = frmSTest.sscTest_3.Tag
            Case 4 : iSelInst = frmSTest.sscTest_4.Tag
            Case 5 : iSelInst = frmSTest.sscTest_5.Tag
            Case 6 : iSelInst = frmSTest.sscTest_6.Tag
            Case 7 : iSelInst = frmSTest.sscTest_7.Tag
            Case 8 : iSelInst = frmSTest.sscTest_8.Tag
            Case 9 : iSelInst = frmSTest.sscTest_9.Tag
            Case 10 : iSelInst = frmSTest.sscTest_10.Tag
            Case 11 : iSelInst = frmSTest.sscTest_11.Tag
            Case 12 : iSelInst = frmSTest.sscTest_12.Tag
            Case 13 : iSelInst = frmSTest.sscTest_13.Tag
            Case 14 : iSelInst = frmSTest.sscTest_14.Tag
            Case 15 : iSelInst = frmSTest.sscTest_15.Tag
            Case 16 : iSelInst = frmSTest.sscTest_16.Tag
            Case 17 : iSelInst = frmSTest.sscTest_17.Tag
            Case 18 : iSelInst = frmSTest.sscTest_18.Tag
            Case 19 : iSelInst = frmSTest.sscTest_19.Tag
            Case 20 : iSelInst = frmSTest.sscTest_21.Tag
            Case 21 : iSelInst = frmSTest.sscTest_22.Tag
            Case 22 : iSelInst = frmSTest.sscTest_23.Tag
            Case 23 : iSelInst = frmSTest.sscTest_20.Tag
            Case 24 : iSelInst = frmSTest.sscTest_24.Tag
            Case 25 : iSelInst = frmSTest.sscTest_25.Tag
        End Select
        AbortTest = False
        RunEOtests = False
        If (EoOptionInstalled = True) And ((iSelInst = END_TO_END) Or ((ControlStartTest > END_TO_END) And (ControlStartTest < VEO2))) Then
            j = MsgBox("Do you want to run the EO selftests?", MsgBoxStyle.YesNo)
            If j = DialogResult.Yes Then
                RunEOtests = True
            End If
            If AbortTest = True Then
                Echo("Test Aborted" & vbCrLf)
                GoTo TestComplete
            End If
        End If
        If iSelInst = VEO2 Then
            RunEOtests = True
        End If


InstallSaif:

        ' do NOT NEED the SAIF for Serial,com1,com2,ethernet bus, power meter, analog probe tests
        ' SAIF is removed in Perform VEO tests
        Select Case iButton
            Case SERIALCCA, RS422, RS232, GIGA, VEO2, CANBUS, EO_VCC
                ' SAIF not needed (SAIF removed in VEO2 test)


            Case Else
                ' SAIF is needed
                If fnSAIFinstalled(True) = False Then ' make sure the saif is installed for any test that needs it
                    sMsg = "Install the SAIF onto the tester." & vbCrLf
                    DisplaySetup(sMsg, "ST-SAIF-ON-1.jpg", 1)
                    If AbortTest = True Then
                        Echo("Test Aborted" & vbCrLf)
                        GoTo TestComplete
                    End If
                    If fnSAIFinstalled(True) = False Then
                        sMsg = "Are you sure that you installed the SAIF onto the tester?"
                        x = MsgBox(sMsg, MsgBoxStyle.YesNo)
                        If x = DialogResult.No Then GoTo InstallSaif
                        If fnSAIFinstalled(True) = False Then
                            Echo("   *******************************************")
                            Echo(FormatResults(False, "Receiver ITA switch closed test."), 4) ' print red
                            Echo("   The Receiver Switch on the tester must be open.", 4) 'print red
                            Echo("   *******************************************")
                            ReceiverSwitchOK = False
                        End If
                    End If
                End If
        End Select

TestEOpower:  'Need to test EO power pins to verify EO is not installed
        If EoOptionInstalled Then
            'added software to verify that the EO module is not connected to the power supply
            frmSTest.proProgress.Value = 0
            frmSTest.proProgress.Visible = True
            frmSTest.proProgress.BringToFront()
            frmSTest.DetailsText.SelectionLength = 0
            '' ScreenCursorActive(Cursors.WaitCursor)

            ' make sure EO Pwr - J7 is not connected to VEO2 for
            '   SWITCH1, SWITCH2, SWITCH3, SWITCH4, SWITCH5, PPU
            If (iSelInst = END_TO_END) Or ((ControlStartTest >= SWITCH1 And ControlStartTest <= PPU)) Or (iSelInst = DMM) Or ((iSelInst >= SWITCH1) And (iSelInst <= SWITCH5)) Or (iSelInst = PPU) Then

                Echo(vbCrLf & "** Verifying EO PWR-J7 is not connected to VEO2.", 0)
                ' Turn DC3 on to 15v, make sure current is less than .2 amps, otherwise ask if EO power cable is disconnected
                CommandSetOptions(3, CloseRelay, SetMaster, SenseLocal, CurrentLimit)
                CommandSetPolarity(3, "+")

                'Turn on DC3 to 15V, 0.1 amps
                CommandSetOptions(3, CloseRelay, SetMaster, SenseLocal, CurrentLimit) ' close output relay
                CommandSetCurrent(3, 0.2) 'set current limit to 0.2 Amp
                CommandSetVoltage(3, 15) 'set voltage to 15
                Delay(3) ' allow time for power supply to trip

                'Note: If VEO2 is connected, power supply will trip, or current will be greater than 0.1 amps
                dMeasurement = CommandMeasureVolts(3) ' measure volts
                If dMeasurement < 10 Then ' it must have tripped
                    CommandSupplyReset(3) ' reset supply, it may have tripped
                    Echo("  Failed, low voltage detected.")
                    ''ScreenCursorActive(Cursors.Default)
                    x = MsgBox("Are you sure that you disconnected the VEO2 power cable from ATS PDU?", MsgBoxStyle.YesNo)
                    If x = DialogResult.No Then
                        MsgBox("Disconnect the VEO2 power cable from the ATS PDU, then press enter.")
                        GoTo TestEOpower
                    End If
                Else
                    ' voltage is ok, make sure it is not pulling current
                    dMeasurement = CommandMeasureAmps(3) ' measure amps
                    If dMeasurement > 0.1 Then
                        CommandSupplyReset(3) ' reset supply, it may have tripped
                        Echo("  Failed, excess current detected.")
                        ''ScreenCursorActive(Cursors.Default)
                        x = MsgBox("Are you sure that you disconnected the VEO2 power cable from ATS PDU?", MsgBoxStyle.YesNo)
                        If x = DialogResult.No Then
                            MsgBox("Disconnect the VEO2 power cable from the ATS PDU, then press enter.")
                            GoTo TestEOpower
                        End If
                    Else                            ' ok, not pulling current, make sure power supply didn't trip
                        ' ok,  check for error status = over current fault
                        'DC3 Status Query
                        CommandData = CStr(Chr(3)) & CStr(Chr(&H44)) & CStr(Chr(&H0)) 'Status query
                        ErrorStatus = atxmlDF_viWrite(PsResourceName(3), 0, CommandData, CLng(Len(CommandData)), Nbr)
                        Delay(1)

                        'get status query returned data
                        ReadBuffer = ""
                        ErrorStatus = ReadDCPSCommand(3, ReadBuffer) ' get 5 bytes

                        'Check Status Byte (BYTE2)
                        iErrStatusCode = Asc(Mid(ReadBuffer, 2, 1))
                        If (iErrStatusCode And &HFB) <> &H80 Then ' tripped supply will read status 129
                            Echo("  Failed, power supply tripped.")
                            CommandSupplyReset(3) ' reset supply, it has tripped
                            ''ScreenCursorActive(Cursors.Default)
                            x = MsgBox("Are you sure that you disconnected the VEO2 power cable from ATS PDU?", MsgBoxStyle.YesNo)
                            If x = DialogResult.No Then
                                MsgBox("Disconnect the VEO2 power cable from the ATS PDU, then press enter.")
                                GoTo TestEOpower
                            End If
                        Else
                            CommandSupplyReset(3) ' reset supply anyway
                        End If
                    End If
                End If
            End If
            ''ScreenCursorActive(Cursors.Default)
        End If
        Echo("Start: " & Today & " " & TimeOfDay)

LoopOnEnd:
        'bb 2016-11-07
        ''vOnErrorGoToLabel = curOnErrorGoToLabel_PSTErrorHandler	' On Error GoTo PSTErrorHandler 'Enable Error Handling routine

        'may need to reinsall the SAIF if in LOE mode
        If (EoOptionInstalled = True) And (RunningEndToEnd = True) And (OptionMode = LOEmode) And (FirstPass = False) Then
            If fnSAIFinstalled(True) = False Then ' make sure the saif is installed for any test
                sMsg = "Install the SAIF onto the tester." & vbCrLf
                DisplaySetup(sMsg, "ST-SAIF-ON-1.jpg", 1)
                If fnSAIFinstalled(True) = False Then
                    sMsg = "Are you sure that you installed the SAIF onto the tester?"
                    x = MsgBox(sMsg, MsgBoxStyle.YesNo)
                    If x = DialogResult.No Then GoTo InstallSaif
                    If fnSAIFinstalled(True) = False Then
                        Echo("   *******************************************")
                        Echo(FormatResults(False, "Receiver ITA switch closed test."), 4) 'print red
                        Echo("   The Receiver Switch on the tester must be open.", 4) 'print red
                        Echo("   *******************************************")
                        ReceiverSwitchOK = False
                    End If
                End If
            End If
        End If
        ' bTestHasBeenPerformed = True
        frmSTest.sspSTestPanel.Enabled = False
        frmSTest.cmdAbort.Visible = True
        frmSTest.cmdPause.Visible = True

        STestBusy = True
        EndToEndTesting = PASSED
        StopEndToEnd = False
        iSelGUI = iButton
        EndToEndStartTime = Now
        iNumOfTests = 25
        If iSelGUI = END_TO_END Then
            EndToEndStartTime = Now
            StartInst = 1
            StopInst = iNumOfTests
            RunningEndToEnd = True
            For i = 0 To iNumOfTests
                InstrumentStatus(i) = UNTESTED
                Select Case i
                    Case 0 : frmSTest.PassFrame_0.Image = frmSTest.picNoTest.Image
                    Case 1 : frmSTest.PassFrame_1.Image = frmSTest.picNoTest.Image
                    Case 2 : frmSTest.PassFrame_2.Image = frmSTest.picNoTest.Image
                    Case 3 : frmSTest.PassFrame_3.Image = frmSTest.picNoTest.Image
                    Case 4 : frmSTest.PassFrame_4.Image = frmSTest.picNoTest.Image
                    Case 5 : frmSTest.PassFrame_5.Image = frmSTest.picNoTest.Image
                    Case 6 : frmSTest.PassFrame_6.Image = frmSTest.picNoTest.Image
                    Case 7 : frmSTest.PassFrame_7.Image = frmSTest.picNoTest.Image
                    Case 8 : frmSTest.PassFrame_8.Image = frmSTest.picNoTest.Image
                    Case 9 : frmSTest.PassFrame_9.Image = frmSTest.picNoTest.Image
                    Case 10 : frmSTest.PassFrame_10.Image = frmSTest.picNoTest.Image
                    Case 11 : frmSTest.PassFrame_11.Image = frmSTest.picNoTest.Image
                    Case 12 : frmSTest.PassFrame_12.Image = frmSTest.picNoTest.Image
                    Case 13 : frmSTest.PassFrame_13.Image = frmSTest.picNoTest.Image
                    Case 14 : frmSTest.PassFrame_14.Image = frmSTest.picNoTest.Image
                    Case 15 : frmSTest.PassFrame_15.Image = frmSTest.picNoTest.Image
                    Case 16 : frmSTest.PassFrame_16.Image = frmSTest.picNoTest.Image
                    Case 17 : frmSTest.PassFrame_17.Image = frmSTest.picNoTest.Image
                    Case 18 : frmSTest.PassFrame_18.Image = frmSTest.picNoTest.Image
                    Case 19 : frmSTest.PassFrame_19.Image = frmSTest.picNoTest.Image
                    Case 20 : frmSTest.PassFrame_20.Image = frmSTest.picNoTest.Image
                    Case 21 : frmSTest.PassFrame_21.Image = frmSTest.picNoTest.Image 'rf or eo
                    Case 22 : frmSTest.PassFrame_22.Image = frmSTest.picNoTest.Image 'rf or eo
                    Case 23 : frmSTest.PassFrame_23.Image = frmSTest.picNoTest.Image 'rf or eo
                    Case 24 : frmSTest.PassFrame_24.Image = frmSTest.picNoTest.Image 'eo
                    Case 25 : frmSTest.PassFrame_25.Image = frmSTest.picNoTest.Image 'eo
                End Select

            Next i

            Echo("")
            CenterTextBox("END TO END TEST", Today & " " & TimeOfDay)
            Echo("")
            ''frmSTest.imgLogo.Image = frmSTest.imlViper.Images(9 - 1) 'green
        Else
            InstrumentStatus(iSelGUI) = UNTESTED
            Select Case iSelGUI
                Case 0 : frmSTest.PassFrame_0.Image = frmSTest.picNoTest.Image
                Case 1 : frmSTest.PassFrame_1.Image = frmSTest.picNoTest.Image
                Case 2 : frmSTest.PassFrame_2.Image = frmSTest.picNoTest.Image
                Case 3 : frmSTest.PassFrame_3.Image = frmSTest.picNoTest.Image
                Case 4 : frmSTest.PassFrame_4.Image = frmSTest.picNoTest.Image
                Case 5 : frmSTest.PassFrame_5.Image = frmSTest.picNoTest.Image
                Case 6 : frmSTest.PassFrame_6.Image = frmSTest.picNoTest.Image
                Case 7 : frmSTest.PassFrame_7.Image = frmSTest.picNoTest.Image
                Case 8 : frmSTest.PassFrame_8.Image = frmSTest.picNoTest.Image
                Case 9 : frmSTest.PassFrame_9.Image = frmSTest.picNoTest.Image
                Case 10 : frmSTest.PassFrame_10.Image = frmSTest.picNoTest.Image
                Case 11 : frmSTest.PassFrame_11.Image = frmSTest.picNoTest.Image
                Case 12 : frmSTest.PassFrame_12.Image = frmSTest.picNoTest.Image
                Case 13 : frmSTest.PassFrame_13.Image = frmSTest.picNoTest.Image
                Case 14 : frmSTest.PassFrame_14.Image = frmSTest.picNoTest.Image
                Case 15 : frmSTest.PassFrame_15.Image = frmSTest.picNoTest.Image
                Case 16 : frmSTest.PassFrame_16.Image = frmSTest.picNoTest.Image
                Case 17 : frmSTest.PassFrame_17.Image = frmSTest.picNoTest.Image
                Case 18 : frmSTest.PassFrame_18.Image = frmSTest.picNoTest.Image
                Case 19 : frmSTest.PassFrame_19.Image = frmSTest.picNoTest.Image
                Case 20 : frmSTest.PassFrame_20.Image = frmSTest.picNoTest.Image
                Case 21 : frmSTest.PassFrame_21.Image = frmSTest.picNoTest.Image
                Case 22 : frmSTest.PassFrame_22.Image = frmSTest.picNoTest.Image
                Case 23 : frmSTest.PassFrame_23.Image = frmSTest.picNoTest.Image
                Case 24 : frmSTest.PassFrame_24.Image = frmSTest.picNoTest.Image
                Case 25 : frmSTest.PassFrame_25.Image = frmSTest.picNoTest.Image
            End Select
            RunningEndToEnd = False
            StartInst = iSelGUI
            StopInst = iSelGUI
            If ControlStartTest > 0 Then ' added so control left click would start end to end test on any button
                For i = StartInst To iNumOfTests
                    InstrumentStatus(i) = UNTESTED
                    Select Case i
                        Case 0 : frmSTest.PassFrame_0.Image = frmSTest.picNoTest.Image
                        Case 1 : frmSTest.PassFrame_1.Image = frmSTest.picNoTest.Image
                        Case 2 : frmSTest.PassFrame_2.Image = frmSTest.picNoTest.Image
                        Case 3 : frmSTest.PassFrame_3.Image = frmSTest.picNoTest.Image
                        Case 4 : frmSTest.PassFrame_4.Image = frmSTest.picNoTest.Image
                        Case 5 : frmSTest.PassFrame_5.Image = frmSTest.picNoTest.Image
                        Case 6 : frmSTest.PassFrame_6.Image = frmSTest.picNoTest.Image
                        Case 7 : frmSTest.PassFrame_7.Image = frmSTest.picNoTest.Image
                        Case 8 : frmSTest.PassFrame_8.Image = frmSTest.picNoTest.Image
                        Case 9 : frmSTest.PassFrame_9.Image = frmSTest.picNoTest.Image
                        Case 10 : frmSTest.PassFrame_10.Image = frmSTest.picNoTest.Image
                        Case 11 : frmSTest.PassFrame_11.Image = frmSTest.picNoTest.Image
                        Case 12 : frmSTest.PassFrame_12.Image = frmSTest.picNoTest.Image
                        Case 13 : frmSTest.PassFrame_13.Image = frmSTest.picNoTest.Image
                        Case 14 : frmSTest.PassFrame_14.Image = frmSTest.picNoTest.Image
                        Case 15 : frmSTest.PassFrame_15.Image = frmSTest.picNoTest.Image
                        Case 16 : frmSTest.PassFrame_16.Image = frmSTest.picNoTest.Image
                        Case 17 : frmSTest.PassFrame_17.Image = frmSTest.picNoTest.Image
                        Case 18 : frmSTest.PassFrame_18.Image = frmSTest.picNoTest.Image
                        Case 19 : frmSTest.PassFrame_19.Image = frmSTest.picNoTest.Image
                        Case 20 : frmSTest.PassFrame_20.Image = frmSTest.picNoTest.Image
                        Case 21 : frmSTest.PassFrame_21.Image = frmSTest.picNoTest.Image
                        Case 22 : frmSTest.PassFrame_22.Image = frmSTest.picNoTest.Image
                        Case 23 : frmSTest.PassFrame_23.Image = frmSTest.picNoTest.Image
                        Case 24 : frmSTest.PassFrame_24.Image = frmSTest.picNoTest.Image
                        Case 25 : frmSTest.PassFrame_25.Image = frmSTest.picNoTest.Image
                    End Select
                Next i
                RunningEndToEnd = True
                StopInst = iNumOfTests
            End If

        End If

        frmSTest.timTimer.Enabled = True
        frmSTest.proProgress.Visible = True

        ' **************************************************************************
        LastTest = -1
        For iTestNumber = StartInst To StopInst '  bb was using "For iSelGUI = StartInst To StopInst"  caused warnings?
            StartTest = Now
            iSelGUI = iTestNumber
            Select Case iSelGUI
                Case 21, 22, 23
                    If RFOptionInstalled = False Then GoTo SkipTest1
                Case EO_VCC
                    If (EoOptionInstalled = False) Then GoTo SkipTest1
                Case VEO2
                    If (EoOptionInstalled = False) Or (RunEOtests = False) Then GoTo SkipTest1
            End Select

            Application.DoEvents()
            Select Case iSelGUI
                Case 0 : iSelInst = frmSTest.sscTest_0.Tag
                Case 1 : iSelInst = frmSTest.sscTest_1.Tag
                Case 2 : iSelInst = frmSTest.sscTest_2.Tag
                Case 3 : iSelInst = frmSTest.sscTest_3.Tag
                Case 4 : iSelInst = frmSTest.sscTest_4.Tag
                Case 5 : iSelInst = frmSTest.sscTest_5.Tag
                Case 6 : iSelInst = frmSTest.sscTest_6.Tag
                Case 7 : iSelInst = frmSTest.sscTest_7.Tag
                Case 8 : iSelInst = frmSTest.sscTest_8.Tag
                Case 9 : iSelInst = frmSTest.sscTest_9.Tag
                Case 10 : iSelInst = frmSTest.sscTest_10.Tag
                Case 11 : iSelInst = frmSTest.sscTest_11.Tag
                Case 12 : iSelInst = frmSTest.sscTest_12.Tag
                Case 13 : iSelInst = frmSTest.sscTest_13.Tag
                Case 14 : iSelInst = frmSTest.sscTest_14.Tag
                Case 15 : iSelInst = frmSTest.sscTest_15.Tag
                Case 16 : iSelInst = frmSTest.sscTest_16.Tag
                Case 17 : iSelInst = frmSTest.sscTest_17.Tag
                Case 18 : iSelInst = frmSTest.sscTest_18.Tag
                Case 19 : iSelInst = frmSTest.sscTest_19.Tag
                Case 20 : iSelInst = frmSTest.sscTest_20.Tag
                Case 21 : iSelInst = frmSTest.sscTest_21.Tag 'rf
                Case 22 : iSelInst = frmSTest.sscTest_22.Tag 'rf
                Case 23 : iSelInst = frmSTest.sscTest_23.Tag 'rf
                Case 24 : iSelInst = frmSTest.sscTest_24.Tag 'eo
                Case 25 : iSelInst = frmSTest.sscTest_25.Tag 'eo
            End Select
            SwitchTest = False
            TestPassedCountStart = TestPassedCount
            TestFailedCountStart = TestFailedCount
            StepPassedCountStart = StepPassedCount
            StepFailedCountStart = StepFailedCount

Retest:     frmSTest.timTimer.Enabled = True
            frmSTest.proProgress.Value = 0
            TestingMessage = "Testing the " & InstrumentDescription(iSelInst) & "."
            frmSTest.sbrUserInformation.Text = TestingMessage
            Select Case iSelGUI
                Case 0 : frmSTest.PassFrame_0.Image = frmSTest.picNoTest.Image
                Case 1 : frmSTest.PassFrame_1.Image = frmSTest.picNoTest.Image
                Case 2 : frmSTest.PassFrame_2.Image = frmSTest.picNoTest.Image
                Case 3 : frmSTest.PassFrame_3.Image = frmSTest.picNoTest.Image
                Case 4 : frmSTest.PassFrame_4.Image = frmSTest.picNoTest.Image
                Case 5 : frmSTest.PassFrame_5.Image = frmSTest.picNoTest.Image
                Case 6 : frmSTest.PassFrame_6.Image = frmSTest.picNoTest.Image
                Case 7 : frmSTest.PassFrame_7.Image = frmSTest.picNoTest.Image
                Case 8 : frmSTest.PassFrame_8.Image = frmSTest.picNoTest.Image
                Case 9 : frmSTest.PassFrame_9.Image = frmSTest.picNoTest.Image
                Case 10 : frmSTest.PassFrame_10.Image = frmSTest.picNoTest.Image
                Case 11 : frmSTest.PassFrame_11.Image = frmSTest.picNoTest.Image
                Case 12 : frmSTest.PassFrame_12.Image = frmSTest.picNoTest.Image
                Case 13 : frmSTest.PassFrame_13.Image = frmSTest.picNoTest.Image
                Case 14 : frmSTest.PassFrame_14.Image = frmSTest.picNoTest.Image
                Case 15 : frmSTest.PassFrame_15.Image = frmSTest.picNoTest.Image
                Case 16 : frmSTest.PassFrame_16.Image = frmSTest.picNoTest.Image
                Case 17 : frmSTest.PassFrame_17.Image = frmSTest.picNoTest.Image
                Case 18 : frmSTest.PassFrame_18.Image = frmSTest.picNoTest.Image
                Case 19 : frmSTest.PassFrame_19.Image = frmSTest.picNoTest.Image
                Case 20 : frmSTest.PassFrame_20.Image = frmSTest.picNoTest.Image
                Case 21 : frmSTest.PassFrame_21.Image = frmSTest.picNoTest.Image 'rf
                Case 22 : frmSTest.PassFrame_22.Image = frmSTest.picNoTest.Image 'rf
                Case 23 : frmSTest.PassFrame_23.Image = frmSTest.picNoTest.Image 'rf
                Case 24 : frmSTest.PassFrame_24.Image = frmSTest.picNoTest.Image 'eo
                Case 25 : frmSTest.PassFrame_25.Image = frmSTest.picNoTest.Image 'eo
                Case Else
            End Select

            frmSTest.timTimer.Tag = CStr(iSelGUI)
            If RunningEndToEnd Then
                frmSTest.timTimer.Tag = frmSTest.timTimer.Tag & "E"
            End If
            frmSTest.proProgress.Value = 0
            Echo("")
            TestStatus = UNTESTED
            ChDir(Application.StartupPath)
            Select Case iSelInst
                Case MIL_STD_1553
                    HelpContextID = 1110
                    TestStatus = Test1553() '1
                Case DIGITAL
                    nSystErr = WriteMsg(SWITCH1, "RESET") ' open all switches
                    ResetSystem(False)
                    ChDir(ProgramPath)
                    HelpContextID = 1120
                    TestStatus = TestDTS() '2

                Case SWITCH1
                    HelpContextID = 1130
                    TestStatus = TestSwitching(SWITCH1) '3 LF1
                Case SWITCH2
                    HelpContextID = 1140
                    TestStatus = TestSwitching(SWITCH2) '4 LF2
                Case SWITCH3
                    HelpContextID = 1150
                    TestStatus = TestSwitching(SWITCH3) '5 LF3
                Case SWITCH4
                    HelpContextID = 1160
                    TestStatus = TestSwitching(SWITCH4) '6 MF
                Case SWITCH5
                    TestStatus = TestSwitching(SWITCH5) '7 HF
                    HelpContextID = 1170

                Case DMM
                    HelpContextID = 1180
                    TestStatus = TestDmm() '8
                Case OSCOPE
                    HelpContextID = 1190
                    TestStatus = TestScope() '9
                Case COUNTER
                    HelpContextID = 1200
                    TestStatus = TestCT() '10
                Case ARB
                    HelpContextID = 1210
                    TestStatus = TestArb() '11
                Case FGEN
                    HelpContextID = 1220
                    TestStatus = TestFGen() '12
                Case PPU
                    HelpContextID = 1230
                    TestStatus = TestPowerSupplies() '13
                Case SYN_RES
                    HelpContextID = 1240
                    TestStatus = TestSR() '14 

                    ' serial ports
                Case SERIALCCA
                    HelpContextID = 1250
                    TestStatus = TestSerialBUS() '15
                Case RS422
                    HelpContextID = 1260
                    TestStatus = TestCom1() '16
                Case RS232
                    HelpContextID = 1270
                    TestStatus = TestCom2() '17
                Case GIGA
                    HelpContextID = 1280
                    TestStatus = TestGigabitEthernet() '18
                Case CANBUS
                    HelpContextID = 1290
                    TestStatus = TestCanBus() '19

                Case APROBE
                    HelpContextID = 1300
                    TestStatus = TestAnalogProbe() '20
                    'EO 24,25
                Case EO_VCC
                    HelpContextID = 1360
                    If EoOptionInstalled Then
                        TestStatus = TestVCC() '24
                    End If
                Case VEO2
                    HelpContextID = 1370
                    If EoOptionInstalled Then
                        TestStatus = TestVEO() '25
                    End If
            End Select
            'bb 2016-11-07
            ''vOnErrorGoToLabel = curOnErrorGoToLabel_PSTErrorHandler	' On Error GoTo PSTErrorHandler 'Enable Error Handling routine

            frmSTest.timTimer.Enabled = False
            Delay(0.4) ' allow time for timer to quit
            If CloseProgram = True Then Exit Sub
            frmSTest.InstrumentLabel_0.ForeColor = Color.Black
            Select Case iSelGUI
                Case 1 : frmSTest.InstrumentLabel_1.ForeColor = Color.Black
                Case 2 : frmSTest.InstrumentLabel_2.ForeColor = Color.Black
                Case 3 : frmSTest.InstrumentLabel_3.ForeColor = Color.Black
                Case 4 : frmSTest.InstrumentLabel_4.ForeColor = Color.Black
                Case 5 : frmSTest.InstrumentLabel_5.ForeColor = Color.Black
                Case 6 : frmSTest.InstrumentLabel_6.ForeColor = Color.Black
                Case 7 : frmSTest.InstrumentLabel_7.ForeColor = Color.Black
                Case 8 : frmSTest.InstrumentLabel_8.ForeColor = Color.Black
                Case 9 : frmSTest.InstrumentLabel_9.ForeColor = Color.Black
                Case 10 : frmSTest.InstrumentLabel_10.ForeColor = Color.Black
                Case 11 : frmSTest.InstrumentLabel_11.ForeColor = Color.Black
                Case 12 : frmSTest.InstrumentLabel_12.ForeColor = Color.Black
                Case 13 : frmSTest.InstrumentLabel_13.ForeColor = Color.Black
                Case 14 : frmSTest.InstrumentLabel_14.ForeColor = Color.Black
                Case 15 : frmSTest.InstrumentLabel_15.ForeColor = Color.Black
                Case 16 : frmSTest.InstrumentLabel_16.ForeColor = Color.Black
                Case 17 : frmSTest.InstrumentLabel_17.ForeColor = Color.Black
                Case 18 : frmSTest.InstrumentLabel_18.ForeColor = Color.Black
                Case 19 : frmSTest.InstrumentLabel_19.ForeColor = Color.Black
                Case 20 : frmSTest.InstrumentLabel_20.ForeColor = Color.Black
                Case 21 : frmSTest.InstrumentLabel_21.ForeColor = Color.Black 'rf
                Case 22 : frmSTest.InstrumentLabel_22.ForeColor = Color.Black 'rf
                Case 23 : frmSTest.InstrumentLabel_23.ForeColor = Color.Black 'rf
                Case 24 : frmSTest.InstrumentLabel_24.ForeColor = Color.Black 'eo
                Case 25 : frmSTest.InstrumentLabel_25.ForeColor = Color.Black 'eo
            End Select
            If iSelInst = DMM Then
                ResetSystem(True) 'Evaluation: If iSelInst=DMM then True
            End If

            sRemoveMsg = ""

            If CloseProgram = True Then Exit Sub

            If (AbortTest = True) Then
                If RunningEndToEnd = True Then
                    iResponse = MsgBox("Test Aborted by operator.  Do you want to retry this test?" & vbCrLf & "Answering 'Yes' will cause the test to be re-run.", MsgBoxStyle.YesNo)
                    'If Yes, Re-run the test.
                    If iResponse = DialogResult.Yes Then
                        Echo("-> RETESTING MODULE " & InstrumentDescription(iSelInst))
                        InstrumentStatus(iSelInst) = UNTESTED
                        AbortTest = False
                        frmSTest.cmdAbort.Text = "Abort Test"
                        frmSTest.cmdPause.Text = "Pause Test"
                        TestPassedCount = TestPassedCountStart
                        TestFailedCount = TestFailedCountStart
                        StepPassedCount = StepPassedCountStart
                        StepFailedCount = StepFailedCountStart
                        frmSTest.lblPassCount.Text = CStr(TestPassedCount)
                        frmSTest.lblFailCount.Text = CStr(TestFailedCount)
                        frmSTest.lblStepPassCount.Text = CStr(StepPassedCount)
                        frmSTest.lblStepFailCount.Text = CStr(StepFailedCount)
                        GoTo Retest
                    Else
                        xx = CType(MsgBox("Do you want to continue the End-to-End testing?", MsgBoxStyle.Critical Or MsgBoxStyle.YesNoCancel), DialogResult)
                        If xx <> DialogResult.Yes Then
                            If EndToEndTesting <> FAILED Then
                                EndToEndTesting = UNTESTED
                            End If
                            StopEndToEnd = True
                        End If
                    End If
                End If
                InstrumentStatus(iSelInst) = UNTESTED
                If StopEndToEnd = True Then
                    LastTest = 0
                    Exit For
                End If

            ElseIf (TestStatus = FAILED) Then
                TestFailedCount += 1
                frmSTest.lblFailCount.Text = CStr(TestFailedCount)
                FormatResultLine(InstrumentDescription(iSelInst), False)
                'bb added 9-18-2017 per test requirement
                EchoSlotTemperature(iSelInst)

                'If the current Instrument fails Self Test, Ask the Operator if he/she caused it.
                If RunningEndToEnd = True Then
                    iResponse = MsgBox("Was the module failure caused by operator error?" & vbCrLf & "Answering 'Yes' will cause the test to be re-run.", MsgBoxStyle.YesNo)
                    'If Yes, Re-run the test.
                    If iResponse = DialogResult.Yes Then
                        Echo("Test Failure due to operator error.")
                        Echo("-> RETESTING MODULE " & InstrumentDescription(iSelInst))
                        If sRemoveMsg <> "" Then
                            MsgBox(sRemoveMsg)
                        End If
                        InstrumentStatus(iSelInst) = UNTESTED
                        TestPassedCount = TestPassedCountStart
                        TestFailedCount = TestFailedCountStart
                        StepPassedCount = StepPassedCountStart
                        StepFailedCount = StepFailedCountStart
                        frmSTest.lblPassCount.Text = CStr(TestPassedCount)
                        frmSTest.lblFailCount.Text = CStr(TestFailedCount)
                        frmSTest.lblStepPassCount.Text = CStr(StepPassedCount)
                        frmSTest.lblStepFailCount.Text = CStr(StepFailedCount)
                        GoTo Retest

                    Else 'If No, Ask the Operator if he/she wants to continue.
                        EndToEndTesting = FAILED
                        InstrumentStatus(iSelInst) = FAILED
                        If iSelGUI <> StopInst Then 'If not last instrument
                            iResponse = MsgBox(InstrumentDescription(iSelInst) & " failed." & vbCrLf & "Do you want to Continue End to End Testing?", MsgBoxStyle.YesNo)
                            'If No, Terminate End-To-End Testing
                            If iResponse = DialogResult.No Then
                                StopEndToEnd = True
                                If sRemoveMsg <> "" Then
                                    MsgBox(sRemoveMsg)
                                End If
                                LastTest = 0
                                Exit For
                            End If
                        End If
                    End If
                Else
                    EndToEndTesting = FAILED
                End If

            ElseIf (TestStatus = PASSED) Then
                InstrumentStatus(iSelInst) = PASSED
                TestPassedCount += 1
                frmSTest.lblPassCount.Text = CStr(TestPassedCount)
                FormatResultLine(InstrumentDescription(iSelInst), True)
                '********************************************************
                'Added June 4, 2007 as requested by Chris Connelly
                ' in order to log individual instrument passes in FHDB
                If RunningEndToEnd = False Then
                    LogFHDBTestPassed("Passed - " & InstrumentDescription(iSelInst))

                End If
                '********************************************************

            ElseIf (TestStatus = UNTESTED) Then
                InstrumentStatus(iSelInst) = UNTESTED
                Echo(InstrumentDescription(iSelInst) & " NOT TESTED.")
            ElseIf (TestStatus <> iSelInst) Then                    'Test failed due to a failure in a different instrument
                iResponse = MsgBox("Was the failure caused by operator error?" & vbCrLf & "Answering 'Yes' will cause the test to be re-run.", MsgBoxStyle.YesNo)
                'If Yes, Re-run the test.
                If iResponse = DialogResult.Yes Then
                    Echo("-> RETESTING MODULE " & InstrumentDescription(iSelInst))
                    InstrumentStatus(iSelInst) = UNTESTED
                    AbortTest = False
                    frmSTest.cmdAbort.Text = "Abort Test"
                    frmSTest.cmdPause.Text = "Pause Test"
                    TestPassedCount = TestPassedCountStart
                    TestFailedCount = TestFailedCountStart
                    StepPassedCount = StepPassedCountStart
                    StepFailedCount = StepFailedCountStart
                    frmSTest.lblPassCount.Text = CStr(TestPassedCount)
                    frmSTest.lblFailCount.Text = CStr(TestFailedCount)
                    frmSTest.lblStepPassCount.Text = CStr(StepPassedCount)
                    frmSTest.lblStepFailCount.Text = CStr(StepFailedCount)
                    GoTo Retest
                Else 'If No, Ask the Operator if he/she wants to continue E2E.
                    EndToEndTesting = FAILED
                    InstrumentStatus(iSelInst) = FAILED
                    If iSelGUI <> StopInst Then 'If not last instrument
                        iResponse = MsgBox(InstrumentDescription(iSelInst) & " failed." & vbCrLf & "Do you want to Continue End to End Testing?", MsgBoxStyle.YesNo)
                        'If No, Terminate End-To-End Testing
                        If iResponse = DialogResult.No Then
                            StopEndToEnd = True
                            If sRemoveMsg <> "" Then
                                MsgBox(sRemoveMsg)
                            End If
                            LastTest = 0
                            Exit For
                        End If
                    End If
                End If

                InstrumentStatus(iSelInst) = UNTESTED
                '    MsgBox("The " & InstrumentDescription(iSelInst) & " was unable to be tested due to a failure in another module. " & vbCrLf & "(" & InstrumentDescription(iSelInst) & ")" & vbCrLf & "Read 'Details' box or System Log file for more information.")
                ReportUnknown(iSelInst)
                Echo(InstrumentDescription(iSelInst) & " NOT TESTED due to failure of the " & InstrumentDescription(TestStatus) & ".")
                EndToEndTesting = FAILED
                EchoSlotTemperature(TestStatus)
                End If

                If DetailStatus = True Then
                    frmSTest.DetailsText.Focus()
                    Application.DoEvents()
                End If

                If (TestStatus = PASSED) Or (TestStatus = FAILED) Then
                    If sRemoveMsg <> "" Then
                        MsgBox(sRemoveMsg)
                    End If
                End If
                If iSelGUI <> StopInst Then ' clear abort before next test
                    AbortTest = False
                End If
                'add test module time
                StopTest = Now
                Secs = CInt(DateDiff(DateInterval.Second, StartTest, StopTest))
                Hrs = CInt(Secs \ 3600)
                Secs = CInt(Secs Mod 3600)
                Mins = CInt(Secs \ 60)
                Secs = CInt(Secs Mod 60)
                sTestTime = Format(Hrs, "00:") & Format(Mins, "00:") & Format(Secs, "00") ' & " (HR:MIN:SEC)"
                Echo("-- Test Time: " & sTestTime)
                LastTest = iTestNumber
SkipTest1:
        Next iTestNumber

        If LastTest = -1 Then ' did not show the last test time
            StopTest = Now
            Secs = CInt(DateDiff(DateInterval.Second, StartTest, StopTest))
            Hrs = CInt(Secs \ 3600)
            Secs = CInt(Secs Mod 3600)
            Mins = CInt(Secs \ 60)
            Secs = CInt(Secs Mod 60)
            sTestTime = Format(Hrs, "00:") & Format(Mins, "00:") & Format(Secs, "00") ' & " (HR:MIN:SEC)"
            Echo("-- Test Time: " & sTestTime)
        End If

        If RunningEndToEnd = True And EndToEndTesting = PASSED Then
            LogFHDBTestPassed("Passed - EndtoEnd")
        End If

        HelpContextID = 1100
        frmSTest.cmdAbort.Visible = False
        frmSTest.cmdPause.Visible = False
        frmSTest.cmdAbort.Text = "Abort Test"
        frmSTest.cmdPause.Text = "Pause Test"
        PauseTest = False

        EndToEndStopTime = Now
        If RunningEndToEnd Then
            iSelInst = END_TO_END
            iSelGUI = END_TO_END
            EndToEndRunTime = DateDiff(DateInterval.Second, EndToEndStartTime, EndToEndStopTime)
            If EndToEndTesting = FAILED Then
                InstrumentStatus(END_TO_END) = FAILED
                ReportFailure(END_TO_END)
            ElseIf EndToEndTesting = PASSED Then
                ReportPass(END_TO_END)
            Else
                ReportUnknown(END_TO_END)
            End If


            Echo("")
            Echo("***************************************************************************")
            Echo("***************************************************************************")
            Echo("***                                                                     ***")
            Echo("***                      END TO END TEST SUMMARY                        ***")
            Echo("***                                                                     ***")
            Echo("***************************************************************************")
            Echo("***************************************************************************")
            Echo("")
            If StopEndToEnd Then
                Echo("END TO END TEST CANCELED DUE TO FAILURE.")
                Echo("")
            End If


            Hrs = CInt(EndToEndRunTime \ 3600)
            EndToEndRunTime = EndToEndRunTime Mod 3600
            Mins = CInt(EndToEndRunTime \ 60)
            Secs = CInt(EndToEndRunTime Mod 60)
            sTestTime = Format(Hrs, "00:") & Format(Mins, "00:") & Format(Secs, "00") ' & " (HR:MIN:SEC)"
            CenterTextBox("END TO END TEST END", "Start Test: " & EndToEndStartTime, "End Test:   " & EndToEndStopTime, "Run Time: " & sTestTime)
            Echo("")
            Application.DoEvents()
            If CloseProgram = True Then Exit Sub

            bTestFailed = False
            Dim iTest As Integer
            For iTest = StartInst To StopInst
                iSelInst = iTest
                iSelGUI = iTest
                Select Case iTest
                    Case 21, 22, 23
                        If RFOptionInstalled = False Then GoTo SkipMessage
                    Case EO_VCC
                        If EoOptionInstalled = False Then GoTo SkipMessage
                    Case VEO2
                        If EoOptionInstalled = False Then GoTo SkipMessage
                        If RunEOtests = False Then GoTo SkipMessage
                End Select
                If InstrumentStatus(iSelInst) = PASSED Then
                    STMessage = "PASSED"
                    ReportPass(iSelInst)
                    Echo(InstrumentDescription(iSelInst) & Space(69 - Len(InstrumentDescription(iSelInst))) & STMessage)
                ElseIf InstrumentStatus(iSelInst) = FAILED Then
                    bTestFailed = True
                    STMessage = "FAILED"
                    ReportFailure(iSelInst)
                    Echo(InstrumentDescription(iSelInst) & Space(69 - Len(InstrumentDescription(iSelInst))) & STMessage)
                ElseIf InstrumentStatus(iSelInst) = UNTESTED Then
                    STMessage = "NOT TESTED"
                    ReportUnknown(iSelInst)
                    Echo(InstrumentDescription(iSelInst) & Space(65 - Len(InstrumentDescription(iSelInst))) & STMessage)
                ElseIf InstrumentStatus(iSelInst) <> iSelInst Then                        'Set to itself when not available
                    STMessage = "NOT TESTED"
                    Echo(InstrumentDescription(iSelInst) & Space(65 - Len(InstrumentDescription(iSelInst))) & STMessage)
                    If TestStatus >= 0 Then
                        Echo("     Due to failure of " & InstrumentDescription(TestStatus) & ".") '07/02/01
                    End If
                End If
SkipMessage:
            Next iTest
            frmSTest.DetailsText.SelectionStart = Len(frmSTest.DetailsText.Text)
            If DetailStatus = True Then
                frmSTest.DetailsText.Focus()
                Application.DoEvents()
            End If

        End If

        frmSTest.DetailsText.SelectionStart = Len(frmSTest.DetailsText.Text)
        frmSTest.DetailsText.SelectionLength = 0
        frmSTest.DetailsText.SelectionColor = Color.Black

TestComplete:
        If CloseProgram = True Then Exit Sub
        STestBusy = False
        frmSTest.sbrUserInformation.Text = "Ready."
        frmSTest.sspSTestPanel.Enabled = True
        frmSTest.timTimer.Tag = "0"
        frmSTest.timTimer.Enabled = False
        frmSTest.InstrumentLabel_0.ForeColor = Color.Black
        Select Case iSelInst
            Case 1 : frmSTest.InstrumentLabel_1.ForeColor = Color.Black
            Case 2 : frmSTest.InstrumentLabel_2.ForeColor = Color.Black
            Case 3 : frmSTest.InstrumentLabel_3.ForeColor = Color.Black
            Case 4 : frmSTest.InstrumentLabel_4.ForeColor = Color.Black
            Case 5 : frmSTest.InstrumentLabel_5.ForeColor = Color.Black
            Case 6 : frmSTest.InstrumentLabel_6.ForeColor = Color.Black
            Case 7 : frmSTest.InstrumentLabel_7.ForeColor = Color.Black
            Case 8 : frmSTest.InstrumentLabel_8.ForeColor = Color.Black
            Case 9 : frmSTest.InstrumentLabel_9.ForeColor = Color.Black
            Case 10 : frmSTest.InstrumentLabel_10.ForeColor = Color.Black
            Case 11 : frmSTest.InstrumentLabel_11.ForeColor = Color.Black
            Case 12 : frmSTest.InstrumentLabel_12.ForeColor = Color.Black
            Case 13 : frmSTest.InstrumentLabel_13.ForeColor = Color.Black
            Case 14 : frmSTest.InstrumentLabel_14.ForeColor = Color.Black
            Case 15 : frmSTest.InstrumentLabel_15.ForeColor = Color.Black
            Case 16 : frmSTest.InstrumentLabel_16.ForeColor = Color.Black
            Case 17 : frmSTest.InstrumentLabel_17.ForeColor = Color.Black
            Case 18 : frmSTest.InstrumentLabel_18.ForeColor = Color.Black
            Case 19 : frmSTest.InstrumentLabel_19.ForeColor = Color.Black
            Case 20 : frmSTest.InstrumentLabel_20.ForeColor = Color.Black
            Case 21 : frmSTest.InstrumentLabel_21.ForeColor = Color.Black 'rf
            Case 22 : frmSTest.InstrumentLabel_22.ForeColor = Color.Black 'rf
            Case 23 : frmSTest.InstrumentLabel_23.ForeColor = Color.Black 'rf
            Case 24 : frmSTest.InstrumentLabel_24.ForeColor = Color.Black 'eo
            Case 25 : frmSTest.InstrumentLabel_25.ForeColor = Color.Black 'eo
        End Select
        Application.DoEvents()
        frmSTest.proProgress.Visible = False

        If CloseProgram = True Then Exit Sub

        If OptionFaultMode = SOFmode And TestStatus <> PASSED Then
            ' don't loop when in sof mode and test failed
        ElseIf AbortTest = True Then
            ' don't loop anymore if operator pressed abort test command
        ElseIf OptionMode = LOEmode Then
            FirstPass = False
            GoTo LoopOnEnd
        End If
        AbortTest = False
        frmSTest.cmdAbort.Text = "Abort Test"
        frmSTest.cmdPause.Text = "Pause Test"
        Exit Sub

PSTErrorHandler:
        MsgBox("Error Number: " & Err.Number & vbCrLf & "Error Description: " & Err.Description, MsgBoxStyle.Exclamation)
        Err.Clear()


    End Sub



    Sub RegisterFailure(Optional ByVal vInstrumentIndex As String = "", Optional ByVal sfailstep As String = "", Optional ByVal dBadMeas As Double = 0, Optional ByVal sUnit As String = "", Optional ByVal dLL As Double = 0, Optional ByVal dUL As Double = 0, Optional ByRef sComment As String = "")
        'DESCRIPTION:
        '   This routine reports a failure to the Self Test Log, the screen and the FHDB
        'PARAMETERS:
        '   InstrumentIndex =  If the index is numeric, report of the failing instrument
        '                       If the index is missing, report a General Failure.
        '                       If the index is nonnumeric, report as a Cable.
        '   sFailStep        =  Failure Step Code
        '   dBadMeas         =  The Measured value that Failed
        '   sUnit            =  The Unit of Measure
        '   dLL              =  The Lower Limit (only for cables?)
        '   dUL              =  The Upper Limit
        '   sComment         =  Any Comments to describe the Failure

        Dim ReadoutText As String 'Readout text value
        Dim sFaultCallout As String 'Fault Callout String
        Dim sMessage As String 'Message String
        Dim iInstrumentIndex As Integer 'Instrument Index as an Integer
        Dim sMinField As String 'Minimum Field Value
        Dim sMaxField As String 'Maximum Field Value
        Dim sMeasField As String 'Measured Field Value
        Dim sOpComment As String 'Concatenated Comment String

        'If the Instrument Index is missing, Default to a General Failure.
        If IsNothing(vInstrumentIndex) Then
            sFaultCallout = "-----  General Failure or Information  -----"
        ElseIf Not IsNumeric(vInstrumentIndex) Then            'If it is a Cable ID
            sFaultCallout = InstrumentDescription(CInt(vInstrumentIndex))
            '***  Report Failing measurements to operator and ID Failing Cable  ***
            sMinField = "    Min: " & EngNotate(dLL) & sUnit
            sMinField &= Space(24 - Len(sMinField))
            sMaxField = "Max: " & EngNotate(dUL) & sUnit
            sMaxField &= Space(20 - Len(sMaxField))
            sMeasField = "Measured: " & EngNotate(dBadMeas) & sUnit
            sMeasField &= Space(25 - Len(sMeasField))
            Echo(sMinField & sMaxField & sMeasField & "FAILED", 4) ' red print

            sMessage = sFaultCallout & " may be defective or have a bad connection."
            Echo(sMessage, 4) ' print red
            sComment = sMessage 'Add the Message to the FHDB Comment field

        Else
            iInstrumentIndex = CInt(vInstrumentIndex)
            sFaultCallout = InstrumentDescription(iInstrumentIndex)
            '    ReportFailure iInstrumentIndex

            Select Case iInstrumentIndex
                Case Is > 0
                    ' most instruments
                    ' example
                    'The Astronics 1260-66 HF Switches (PC Slot 9) is malfunctioning.
                    'Replace or service.
                    sMessage = "    The " & InstrumentDescription(iInstrumentIndex) & " is malfunctioning. " & vbCrLf & "    Replace or service."
                    ReadoutText = sMessage
                    Echo(sMessage, 4) ' print red
                    CardStatus(iInstrumentIndex) = FAILED
                    Select Case iSelGUI
                        Case 0 : frmSTest.PassFrame_0.Image = frmSTest.picTestFailed.Image
                        Case 1 : frmSTest.PassFrame_1.Image = frmSTest.picTestFailed.Image
                        Case 2 : frmSTest.PassFrame_2.Image = frmSTest.picTestFailed.Image
                        Case 3 : frmSTest.PassFrame_3.Image = frmSTest.picTestFailed.Image
                        Case 4 : frmSTest.PassFrame_4.Image = frmSTest.picTestFailed.Image
                        Case 5 : frmSTest.PassFrame_5.Image = frmSTest.picTestFailed.Image
                        Case 6 : frmSTest.PassFrame_6.Image = frmSTest.picTestFailed.Image
                        Case 7 : frmSTest.PassFrame_7.Image = frmSTest.picTestFailed.Image
                        Case 8 : frmSTest.PassFrame_8.Image = frmSTest.picTestFailed.Image
                        Case 9 : frmSTest.PassFrame_9.Image = frmSTest.picTestFailed.Image
                        Case 10 : frmSTest.PassFrame_10.Image = frmSTest.picTestFailed.Image
                        Case 11 : frmSTest.PassFrame_11.Image = frmSTest.picTestFailed.Image
                        Case 12 : frmSTest.PassFrame_12.Image = frmSTest.picTestFailed.Image
                        Case 13 : frmSTest.PassFrame_13.Image = frmSTest.picTestFailed.Image
                        Case 14 : frmSTest.PassFrame_14.Image = frmSTest.picTestFailed.Image
                        Case 15 : frmSTest.PassFrame_15.Image = frmSTest.picTestFailed.Image
                        Case 16 : frmSTest.PassFrame_16.Image = frmSTest.picTestFailed.Image
                        Case 17 : frmSTest.PassFrame_17.Image = frmSTest.picTestFailed.Image
                        Case 18 : frmSTest.PassFrame_18.Image = frmSTest.picTestFailed.Image
                        Case 19 : frmSTest.PassFrame_19.Image = frmSTest.picTestFailed.Image
                        Case 20 : frmSTest.PassFrame_20.Image = frmSTest.picTestFailed.Image
                        Case 21 : frmSTest.PassFrame_21.Image = frmSTest.picTestFailed.Image
                        Case 22 : frmSTest.PassFrame_22.Image = frmSTest.picTestFailed.Image
                        Case 23 : frmSTest.PassFrame_23.Image = frmSTest.picTestFailed.Image
                        Case 24 : frmSTest.PassFrame_24.Image = frmSTest.picTestFailed.Image
                        Case 25 : frmSTest.PassFrame_25.Image = frmSTest.picTestFailed.Image
                    End Select

                Case -10
                    sMessage = "    Freedom Power Systems 65V Power Supply, DC10, is failing. " & vbCrLf & "    Replace or Service."
                    Echo(sMessage, 4) 'print red
                    CardStatus(PPU) = FAILED
                    Select Case iSelGUI
                        Case 0 : frmSTest.PassFrame_0.Image = frmSTest.picTestFailed.Image
                        Case 1 : frmSTest.PassFrame_1.Image = frmSTest.picTestFailed.Image
                        Case 2 : frmSTest.PassFrame_2.Image = frmSTest.picTestFailed.Image
                        Case 3 : frmSTest.PassFrame_3.Image = frmSTest.picTestFailed.Image
                        Case 4 : frmSTest.PassFrame_4.Image = frmSTest.picTestFailed.Image
                        Case 5 : frmSTest.PassFrame_5.Image = frmSTest.picTestFailed.Image
                        Case 6 : frmSTest.PassFrame_6.Image = frmSTest.picTestFailed.Image
                        Case 7 : frmSTest.PassFrame_7.Image = frmSTest.picTestFailed.Image
                        Case 8 : frmSTest.PassFrame_8.Image = frmSTest.picTestFailed.Image
                        Case 9 : frmSTest.PassFrame_9.Image = frmSTest.picTestFailed.Image
                        Case 10 : frmSTest.PassFrame_10.Image = frmSTest.picTestFailed.Image
                        Case 11 : frmSTest.PassFrame_11.Image = frmSTest.picTestFailed.Image
                        Case 12 : frmSTest.PassFrame_12.Image = frmSTest.picTestFailed.Image
                        Case 13 : frmSTest.PassFrame_13.Image = frmSTest.picTestFailed.Image
                        Case 14 : frmSTest.PassFrame_14.Image = frmSTest.picTestFailed.Image
                        Case 15 : frmSTest.PassFrame_15.Image = frmSTest.picTestFailed.Image
                        Case 16 : frmSTest.PassFrame_16.Image = frmSTest.picTestFailed.Image
                        Case 17 : frmSTest.PassFrame_17.Image = frmSTest.picTestFailed.Image
                        Case 18 : frmSTest.PassFrame_18.Image = frmSTest.picTestFailed.Image
                        Case 19 : frmSTest.PassFrame_19.Image = frmSTest.picTestFailed.Image
                        Case 20 : frmSTest.PassFrame_20.Image = frmSTest.picTestFailed.Image
                        Case 21 : frmSTest.PassFrame_21.Image = frmSTest.picTestFailed.Image
                        Case 22 : frmSTest.PassFrame_22.Image = frmSTest.picTestFailed.Image
                        Case 23 : frmSTest.PassFrame_23.Image = frmSTest.picTestFailed.Image
                        Case 24 : frmSTest.PassFrame_24.Image = frmSTest.picTestFailed.Image
                        Case 25 : frmSTest.PassFrame_25.Image = frmSTest.picTestFailed.Image
                    End Select

                Case Else
                    ' -9 to 0
                    sMessage = "    Freedom Power Systems 40V Power Supply, DC" & -iInstrumentIndex & ", is failing. " & vbCrLf & "    Replace or Service."
                    Echo(sMessage, 4) 'print red
                    CardStatus(PPU) = FAILED
                    Select Case iSelGUI
                        Case 0 : frmSTest.PassFrame_0.Image = frmSTest.picTestFailed.Image
                        Case 1 : frmSTest.PassFrame_1.Image = frmSTest.picTestFailed.Image
                        Case 2 : frmSTest.PassFrame_2.Image = frmSTest.picTestFailed.Image
                        Case 3 : frmSTest.PassFrame_3.Image = frmSTest.picTestFailed.Image
                        Case 4 : frmSTest.PassFrame_4.Image = frmSTest.picTestFailed.Image
                        Case 5 : frmSTest.PassFrame_5.Image = frmSTest.picTestFailed.Image
                        Case 6 : frmSTest.PassFrame_6.Image = frmSTest.picTestFailed.Image
                        Case 7 : frmSTest.PassFrame_7.Image = frmSTest.picTestFailed.Image
                        Case 8 : frmSTest.PassFrame_8.Image = frmSTest.picTestFailed.Image
                        Case 9 : frmSTest.PassFrame_9.Image = frmSTest.picTestFailed.Image
                        Case 10 : frmSTest.PassFrame_10.Image = frmSTest.picTestFailed.Image
                        Case 11 : frmSTest.PassFrame_11.Image = frmSTest.picTestFailed.Image
                        Case 12 : frmSTest.PassFrame_12.Image = frmSTest.picTestFailed.Image
                        Case 13 : frmSTest.PassFrame_13.Image = frmSTest.picTestFailed.Image
                        Case 14 : frmSTest.PassFrame_14.Image = frmSTest.picTestFailed.Image
                        Case 15 : frmSTest.PassFrame_15.Image = frmSTest.picTestFailed.Image
                        Case 16 : frmSTest.PassFrame_16.Image = frmSTest.picTestFailed.Image
                        Case 17 : frmSTest.PassFrame_17.Image = frmSTest.picTestFailed.Image
                        Case 18 : frmSTest.PassFrame_18.Image = frmSTest.picTestFailed.Image
                        Case 19 : frmSTest.PassFrame_19.Image = frmSTest.picTestFailed.Image
                        Case 20 : frmSTest.PassFrame_20.Image = frmSTest.picTestFailed.Image
                        Case 21 : frmSTest.PassFrame_21.Image = frmSTest.picTestFailed.Image
                        Case 22 : frmSTest.PassFrame_22.Image = frmSTest.picTestFailed.Image
                        Case 23 : frmSTest.PassFrame_23.Image = frmSTest.picTestFailed.Image
                        Case 24 : frmSTest.PassFrame_24.Image = frmSTest.picTestFailed.Image
                        Case 25 : frmSTest.PassFrame_25.Image = frmSTest.picTestFailed.Image
                    End Select
            End Select

        End If

ReportToFHDB:
        'DR# 267, 03/15/02
        'If there is a a General Info message, add it to the first failing record.
        ' sGeneralInfo may be "This session started before the proper amount of warm up time has expired."
        If Len(sGeneralInfo) > 0 Then
            sOpComment = sComment & sGeneralInfo
        Else
            sOpComment = sComment
        End If

        'Record Failure Record in the FHDB
        WriteData(CStr(dtTestStartTime), CStr(Now), 0, sfailstep, sFaultCallout & vbCrLf & sOpComment, dBadMeas, sUnit, dUL, dLL)
        sGeneralInfo = "" 'Clear any General Info

    End Sub

    Sub EchoSlotTemperature(TestX As Integer)

        Dim ChasX As Integer = 0
        Dim SlotX As Integer = 0
        Dim TempX As Single = 0


        '1st get the chassis and slot
        ChasX = 0
        SlotX = 0
        Select Case TestX
            Case 1  ' 1553
            Case 2 : ChasX = 1 : SlotX = 1
            Case 3 : ChasX = 1 : SlotX = 5 'Switch LF1
            Case 4 : ChasX = 1 : SlotX = 6 'Switch LF2
            Case 5 : ChasX = 1 : SlotX = 7 'Switch LF3
            Case 6 : ChasX = 1 : SlotX = 8 'Switch MF
            Case 7 : ChasX = 1 : SlotX = 9 'Switch HF
            Case 8 : ChasX = 1 : SlotX = 11  ' DMM
            Case 9 : ChasX = 2 : SlotX = 1   ' OSCOPE
            Case 10 : ChasX = 2 : SlotX = 2  ' C/T
            Case 11 : ChasX = 1 : SlotX = 12 ' ARB
            Case 12 : ChasX = 2 : SlotX = 3  ' FGEN
            Case 13  ' PPU
            Case 14 : ChasX = 2 : SlotX = 12 ' S/R
            Case 15  ' SERIAL
            Case 16  ' RS422
            Case 17  ' RS232
            Case 18  ' GIGA
            Case 19  ' CANBUS
            Case 20  ' APROBE
            Case 21 : ChasX = 2 : SlotX = 7  ' RFPM     slot 7
            Case 22 : ChasX = 2 : SlotX = 4  ' RFSYN    slot 4,5,6
            Case 23 : ChasX = 2 : SlotX = 8  ' RFMEAS   slot 7,8,9
            Case 24  ' VCC
            Case 25  ' VEO2
        End Select

        If (ChasX < 1) Or (ChasX > 2) Then Exit Sub
        GetOneDataDumpFromBothChassis()

        '2nd Get the temperature
        Select Case TestX
            Case 2 'Digital Slots 1,2,3,4
                TempX = GetSlotTemperature(1, 1)
                TempX = GetSlotTemperature(1, 2)
                TempX = GetSlotTemperature(1, 3)
                TempX = GetSlotTemperature(1, 4)

            Case 22  'RFSTIM Slots 4,5,6
                TempX = GetSlotTemperature(2, 4)
                TempX = GetSlotTemperature(2, 5)
                TempX = GetSlotTemperature(2, 6)
                TempX = GetSlotTemperature(2, 4)

            Case 23  'RFMEAS Slots 7,8,9
                TempX = GetSlotTemperature(2, 7)
                TempX = GetSlotTemperature(2, 8)
                TempX = GetSlotTemperature(2, 9)

            Case Else
                TempX = GetSlotTemperature(ChasX, SlotX)
        End Select

    End Sub


    Function GetSlotTemperature(ChasX As Integer, SlotX As Integer) As Single

        Dim SlotTemp As Single
        Dim IntakeTemp As Single
        Dim TempX As Single

        If ChasX = 1 Then
            IntakeTemp = PrimaryChassis.IntakeTemperature
            Select Case SlotX
                Case 0 : SlotTemp = PrimaryChassis.Temperature0
                Case 1 : SlotTemp = PrimaryChassis.Temperature1
                Case 2 : SlotTemp = PrimaryChassis.Temperature2
                Case 3 : SlotTemp = PrimaryChassis.Temperature3
                Case 4 : SlotTemp = PrimaryChassis.Temperature4
                Case 5 : SlotTemp = PrimaryChassis.Temperature5
                Case 6 : SlotTemp = PrimaryChassis.Temperature6
                Case 7 : SlotTemp = PrimaryChassis.Temperature7
                Case 8 : SlotTemp = PrimaryChassis.Temperature8
                Case 9 : SlotTemp = PrimaryChassis.Temperature9
                Case 10 : SlotTemp = PrimaryChassis.Temperature10
                Case 11 : SlotTemp = PrimaryChassis.Temperature11
                Case 12 : SlotTemp = PrimaryChassis.Temperature12
            End Select
            TempX = (SlotTemp / 2) - 45

        ElseIf ChasX = 2 Then
            IntakeTemp = SecondaryChassis.IntakeTemperature
            Select Case SlotX
                Case 0 : SlotTemp = SecondaryChassis.Temperature0
                Case 1 : SlotTemp = SecondaryChassis.Temperature1
                Case 2 : SlotTemp = SecondaryChassis.Temperature2
                Case 3 : SlotTemp = SecondaryChassis.Temperature3
                Case 4 : SlotTemp = SecondaryChassis.Temperature4
                Case 5 : SlotTemp = SecondaryChassis.Temperature5
                Case 6 : SlotTemp = SecondaryChassis.Temperature6
                Case 7 : SlotTemp = SecondaryChassis.Temperature7
                Case 8 : SlotTemp = SecondaryChassis.Temperature8
                Case 9 : SlotTemp = SecondaryChassis.Temperature9
                Case 10 : SlotTemp = SecondaryChassis.Temperature10
                Case 11 : SlotTemp = SecondaryChassis.Temperature11
                Case 12 : SlotTemp = SecondaryChassis.Temperature12
            End Select
            TempX = (SlotTemp / 2) - 45
        Else
            TempX = -99
        End If
        If ChasX = 1 Then
            Echo("-- Primary Slot " & CStr(SlotX) & ": " & CStr(TempX) & " degrees Celsius")
        ElseIf ChasX = 2 Then
            Echo("-- Secondary Slot " & CStr(SlotX) & ": " & CStr(TempX) & " degrees Celsius")
        End If
        GetSlotTemperature = TempX
        Application.DoEvents()

    End Function


    Function CheckOptionInstalled(ByVal sOpt As String) As Boolean
        'DESCRIPTION:
        '   This routine determines if the ATS RF or EO option is installed
        '   by reading the ATS.INI file located in the C:\USERS\Public\Documents\ directory
        '
        '   [System Startup]
        '   RF_OPTION_INSTALLED=YES
        '   EO_OPTION_INSTALLED=NO
        '
        'RETURNS:
        '   TRUE if the RF option is installed

        Dim lpBuffer As String = Space(256)
        Dim readbuffer As String
        Dim FileError As Integer = -1
        Dim sKey As String = ""

        Select Case UCase(sOpt)
            Case "EO"
                sKey = "EO_OPTION_INSTALLED"
            Case "RF"
                sKey = "RF_OPTION_INSTALLED"

            Case Else
                MsgBox("Invalid argument in CheckOptionInstalled function, notify maintenance.")
        End Select

        FileError = GetPrivateProfileString("System Startup", sKey, "YES", lpBuffer, 256, sATS_INI)
        readbuffer = StripNullCharacters(lpBuffer)
        If UCase(Trim(readbuffer)) = "YES" Then
            CheckOptionInstalled = True
        Else
            CheckOptionInstalled = False
        End If

    End Function



    Sub CheckMassiveFailure()
        'DESCRIPTION:
        '   This routine determines if a massive failure has occurred in the system and reports
        '   whether or not a chassis/controller problem is likely the cause
        Dim Chassis1Functioning As Boolean
        Dim Chassis2Functioning As Boolean
        Dim StatusSum As Short

        Chassis1Functioning = True
        Chassis2Functioning = True

        StatusSum = CardStatus(DIGITAL) + CardStatus(SWITCH1) + CardStatus(DMM) + CardStatus(ARB)
        If StatusSum = 0 Then
            Chassis1Functioning = False
        End If
        StatusSum = CardStatus(OSCOPE) + CardStatus(COUNTER) + CardStatus(FGEN)
        If StatusSum = 0 Then
            Chassis2Functioning = False
        End If
        If Not Chassis1Functioning And Not Chassis2Functioning Then
            'GEN-05-001
            Echo("GEN-05-001")
            sMsg = "All VXI instrumentation not responding."
            RegisterFailure(sfailstep:="GEN-05-001", sComment:=sMsg)
            sMsg &= " Check controller, software configuration, MXI cable (W4), and MXI cards for possible failures."
            Echo(sMsg)
            MsgBox(sMsg, MsgBoxStyle.Exclamation)
            'GEN-03-001
        ElseIf Not Chassis1Functioning Then
            Echo("GEN-03-001")
            sMsg = "All instrumentation in the primary VXI chassis is not functioning."
            RegisterFailure(sfailstep:="GEN-03-001", sComment:=sMsg)
            sMsg &= " Check MXI cable (W4) and MXI controller on the primary chassis."
            Echo(sMsg)
            MsgBox(sMsg, MsgBoxStyle.Exclamation)
            'GEN-04-001
        ElseIf Not Chassis2Functioning Then
            Echo("GEN-04-001")
            sMsg = "All instrumentation in the secondary VXI chassis is not functioning."
            RegisterFailure(sfailstep:="GEN-04-001", sComment:=sMsg)
            sMsg &= " Check MXI cable (W4) and MXI controller on the secondary chassis."
            Echo(sMsg)
            MsgBox(sMsg, MsgBoxStyle.Exclamation)
        End If

    End Sub


    Sub Echo(ByRef DataLine As String, Optional ByVal FaultMessage As Short = 99)
        ' default 99

        'DESCRIPTION:
        '   This routine updates the test history of the system Self test
        'PARAMETERS:
        '   DataLine =  Line of text to be appended to the test history
        '   Note: text window is named DetailsText
        '-----------------------------------------------------------------------
        'Modified to determine the number of lines, based on vbCrLf occurrences.
        'Each line is then checked for length and processed.
        'Modified to paint "Failed" red and "Passed" green   BB
        '-----------------------------------------------------------------------
        Dim SpacePos As Integer
        Dim BeginPos As Integer
        Dim LineNum As Integer
        Dim HitEnd As Integer
        Dim LastPos As Integer
        Dim PaddingSpaces As String
        Dim i As Integer 'Loop Index
        Dim nLines As Integer 'Number of Line in the Data String
        Dim sList() As String = {} 'Data Lines Array
        Dim x1 As Integer
        Dim x2 As Integer

        Try
            ' frmSTest.DetailsText.Refresh()
            frmSTest.DetailsText.SelectionStart = Len(frmSTest.DetailsText.Text) ' move insertion point to end of text
            frmSTest.DetailsText.SelectionColor = Color.Black ' then change it back to black
            frmSTest.DetailsText.SelectionLength = 0
        Catch
        End Try

        DataLine = RTrim(DataLine)
        'Find out how many lines are in the DataLine String (Key off of vbCrLf)
        nLines = StringToList2(DataLine, 1, sList, vbCrLf) ' creates a sList array of nLines

        'Process each Line
        For i = 1 To nLines
            If Len(sList(i)) > 75 Then ' more than one line
                SpacePos = 1
                BeginPos = 1
                LineNum = 1
                HitEnd = 0
                Do ' break one line into multiple lines if longer than 75 characters
                    Do ' find first space after character 75
                        If SpacePos <> 0 Then
                            LastPos = SpacePos + 1
                        End If
                        SpacePos = InStr(LastPos, sList(i), " ", CompareMethod.Text)
                    Loop Until (SpacePos > (74 + BeginPos)) Or (SpacePos = 0)
                    If (SpacePos = 0) And (Len(sList(i)) - BeginPos) <= 75 Then ' no more spaces, last line
                        LastPos = Len(sList(i)) + 1 ' we are done after this line
                    End If
                    If LineNum = 1 Then '<-- This won't work right in some instances, but for
                        PaddingSpaces = "" '    the use in this program, it won't cause a
                    Else                        '    problem (it might print 4 chars off the screen.)
                        PaddingSpaces = "    "
                    End If
                    LineNum += 1
                    Try
                        '  frmSTest.DetailsText.Refresh()
                        frmSTest.DetailsText.SelectionStart = Len(frmSTest.DetailsText.Text) ' move insertion point to end of text
                        frmSTest.DetailsText.SelectionLength = 0
                        frmSTest.DetailsText.SelectedText = PaddingSpaces & Strings.Mid(sList(i), BeginPos, LastPos - BeginPos) & vbCrLf

                        ' frmSTest.DetailsText.Refresh()
                        frmSTest.DetailsText.SelectionStart = Len(frmSTest.DetailsText.Text)
                        If DetailStatus = True Then
                            frmSTest.DetailsText.Focus()
                        End If
                    Catch
                    End Try
                    BeginPos = LastPos
                Loop Until (LastPos = Len(sList(i)) + 1)

            Else                ' add a whole line
                Try
                    '  frmSTest.DetailsText.Refresh()
                    frmSTest.DetailsText.SelectionStart = Len(frmSTest.DetailsText.Text) ' move insertion point to end of text
                    frmSTest.DetailsText.SelectionLength = 0
                    frmSTest.DetailsText.SelectedText = sList(i) & vbCrLf ' append data to textbox
                    frmSTest.DetailsText.SelectionStart = Len(frmSTest.DetailsText.Text) ' move insertion point to end of text
                    If DetailStatus = True Then
                        frmSTest.DetailsText.Focus()
                    End If
                Catch
                End Try
            End If
            sList(i) += vbCrLf ' put a crlf at end of each line for details search

            If FaultMessage = 4 Then ' paint whole line red
                x1 = Len(sList(i)) + 2 ' x1 is length of the line including the crlf
                frmSTest.DetailsText.SelectionStart = Len(frmSTest.DetailsText.Text) - x1 + 2
                frmSTest.DetailsText.SelectionLength = x1
                frmSTest.DetailsText.SelectionColor = ColorTranslator.FromOle(QBColor(4)) ' red
            ElseIf FaultMessage = 5 Then ' paint whole line blue (cleaned switch)
                x1 = Len(sList(i)) + 2 ' x1 is length of the line including the crlf
                frmSTest.DetailsText.SelectionStart = Len(frmSTest.DetailsText.Text) - x1 + 2
                frmSTest.DetailsText.SelectionLength = x1
                frmSTest.DetailsText.SelectionColor = Color.Blue ' blue
            Else
                ' Paint "Failed" red and "Passed" green
                x1 = Len(sList(i)) + 2 ' x1 is length of the line including the crlf
                x2 = InStr(1, sList(i), " passed" & vbCrLf, CompareMethod.Text) ' not case sensitive
                If x2 > 0 Then
                    frmSTest.DetailsText.SelectionStart = Len(frmSTest.DetailsText.Text) - x1 + x2 + 3
                    frmSTest.DetailsText.SelectionLength = 6
                    frmSTest.DetailsText.SelectionColor = ColorTranslator.FromOle(QBColor(2)) ' green
                End If
                x2 = InStr(1, sList(i), " failed" & vbCrLf, CompareMethod.Text) ' not case sensitive
                If x2 > 0 Then
                    frmSTest.DetailsText.SelectionStart = Len(frmSTest.DetailsText.Text) - x1 + x2 + 3
                    frmSTest.DetailsText.SelectionLength = 6
                    frmSTest.DetailsText.SelectionColor = ColorTranslator.FromOle(QBColor(4)) ' red
                End If
                x2 = InStr(1, sList(i), " not tested" & vbCrLf, CompareMethod.Text) ' not case sensitive
                If x2 > 0 Then
                    frmSTest.DetailsText.SelectionStart = Len(frmSTest.DetailsText.Text) - x1 + x2 + 3
                    frmSTest.DetailsText.SelectionLength = 11
                    frmSTest.DetailsText.SelectionColor = ColorTranslator.FromOle(QBColor(9)) ' blue
                End If
            End If
            ' now scroll to bottom of box
            Try
                '    frmSTest.DetailsText.Refresh()
                frmSTest.DetailsText.SelectionStart = Len(frmSTest.DetailsText.Text) ' move insertion point to end of data
                frmSTest.DetailsText.SelectionLength = 0
                frmSTest.DetailsText.SelectionColor = Color.Black
            Catch
            End Try

        Next 'Next Line

        UpdateLogFile(DataLine)
        frmSTest.DetailsText.Refresh()

    End Sub

    Sub UpdateLogFile(DataLine As String)
        'bb 2016-10-25

        Dim x1 As Integer
        Dim FileHandle As Integer
        Dim Filename As String
        Dim nSize As Integer = 4096
        Dim SystemSoftwareVersion As String = ""
        Dim sTemp As String = ""

        Err.Clear()
        'Get the sLogFilePath and write header info only once at startup
        If LogFileStartup = 0 Then
            LogFileStartup = 1
            Try
                If sATS_INI = "" Then ' gets the sATS.ini file location if missing
                    ' get sATS_INI path\filename from registry
                    sATS_INI = Microsoft.Win32.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\ATS\", "IniFilePath", Nothing)
                    If sATS_INI = "" Then
                        sATS_INI = "C:\Users\Public\Documents\ATS.ini" 'C:\Users\Public\Documents\ATS.ini
                    End If
                End If
                If sLogFilePath = "" Then ' gets the LogFile.txt file path if missing
                    x1 = InStr(sATS_INI, "\ATS.ini", CompareMethod.Text)
                    If x1 > 0 Then
                        sLogFilePath = Strings.Left(sATS_INI, x1)
                    Else
                        sLogFilePath = "C:\Users\Public\Documents\"
                    End If
                End If
            Catch
                '  "Error getting path to ATS.ini file from registry. "
            End Try

            '1st get the SystemSoftwareVersion
            lpBuffer = Space(256)
            nSize = Len(lpBuffer)
            ErrorStatus = GetPrivateProfileString("System Startup", "SWR", "", lpBuffer, nSize, sATS_INI)
            SystemSoftwareVersion = Trim(lpBuffer)

            Try
                FileHandle = FreeFile()
                Filename = sLogFilePath & "logfile.txt" ' LOG_FILE
                FileOpen(FileHandle, Filename, OpenMode.Output) ' Note: output mode will overwrite previous logfile
                If Err.Number <> 0 Then
                    'MsgBox("Error writing to selftest log file, results will not be logged.  " & Err.Description & "<" & Err.Number & ">")
                    FileClose(FileHandle)
                    Err.Clear()
                Else
                    PrintLine(FileHandle, "ATS-ViperT Serial Number: " & sUUT_Serial_No)
                    PrintLine(FileHandle, "System Software Version: " & SystemSoftwareVersion)
                End If
                FileClose(FileHandle)
            Catch
                Reset()
                Exit Sub
            End Try
        End If

        'Write Log File
        ' append data to the datalog file
        ' logfile.txt is used to report the results of the last self test
        ' this file is then loaded if the program was aborted and restarted.

        'This routine will echo everything to the logfile name in aps\data\stest dir
        'It is called from the echo command unless it is commented out
        'It will open/append/close the log file each echo command so that 
        '  1. Everything is logged in the event that the system crashes
        '  2. The logfile can be opened by notepad when the program is still running
        Err.Clear()
        Try
            FileHandle = FreeFile()
            FileOpen(FileHandle, sLogFilePath & LOG_FILE, OpenMode.Append)
            PrintLine(FileHandle, DataLine)
            FileClose(FileHandle)
        Catch
            '  MsgBox("Error writing to file " & sLogFilePath & LOG_FILE & "." & vbCrLf & Err.Description)
            FileClose(FileHandle)
        End Try

        'Note Logfile is appended to system log file at EndProgram routine

    End Sub


    Sub EndProgram()
        'DESCRIPTION:
        '   This routine ends the program, logs the results to the system log, and
        '   notifies the main program of the results of the system Self test
        '   and gracefully exits the program.


        Dim lpBuffer As String = Space(256)
        Dim InstrumentToClose As Short
        Dim ErrorStatus As Integer
        Dim SystemLogSpec As String = ""
        'Dim FileHandle As Integer
        'Dim Filename As String
        Dim s As String
        Dim nSize As Integer = 4096
        Dim SystemSoftwareVersion As String = ""
        Dim sTemp As String = ""
        Dim x As Integer


        frmSTest.cmdClose.Text = "Closing, Please Wait..."
        '  frmSTest.CmdClose.Font = VB6.FontChangeSize(frmSTest.CmdClose.Font, 24)
        frmSTest.cmdClose.Width = 200 ' was 3000?
        frmSTest.cmdClose.Height = 200
        frmSTest.cmdClose.Left = (frmSTest.ClientRectangle.Width - 200) / 2
        frmSTest.cmdClose.Top = (frmSTest.ClientRectangle.Height - 200) / 2
        ''  frmSTest.CmdClose.zorder(0)

        Application.DoEvents()
        frmSTest.Enabled = False
        frmSTest.timTimer.Enabled = False
        frmSTest.sbrUserInformation.Text = "Closing Program. . ."
        ResetSystem(True)
        If instrumentHandle(DIGITAL) <> 0 Then
            nSystErr = terM9_close(instrumentHandle(DIGITAL))
        End If
        instrumentHandle(DIGITAL) = 0

        'Return Instrument Handles
        Err.Clear()
        Try
            For InstrumentToClose = 0 To LAST_INSTRUMENT
                If instrumentHandle(InstrumentToClose) <> 0 Then
                    nSystErr = viClose(instrumentHandle(InstrumentToClose))
                End If
            Next InstrumentToClose

        Catch
        End Try

        Err.Clear()
        Try
            If sATS_INI = "" Then ' gets the sATS.ini file location if missing
                ' get sATS_INI path\filename from registry
                sATS_INI = Microsoft.Win32.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\ATS\", "IniFilePath", Nothing)
                If sATS_INI = "" Then
                    sATS_INI = "C:\Users\Public\Documents\ATS.ini" 'C:\Users\Public\Documents\ATS.ini
                End If
            End If
            If sLogFilePath = "" Then ' gets the LogFile.txt file path if missing
                x = InStr(sATS_INI, "\ATS.ini", CompareMethod.Text)
                If x > 0 Then
                    sLogFilePath = Strings.Left(sATS_INI, x)
                Else
                    sLogFilePath = "C:\Users\Public\Documents\"
                End If
            End If
        Catch
            MsgBox("Error getting path to ATS.ini file from registry. ")
        End Try

        ReleaseSystemHandles() ' release GPIB handles used for getting temperature

        '1st get the SystemSoftwareVersion
        lpBuffer = Space(256)
        nSize = Len(lpBuffer)
        ErrorStatus = GetPrivateProfileString("System Startup", "SWR", "", lpBuffer, nSize, sATS_INI)
        SystemSoftwareVersion = Trim(lpBuffer)

        Try

            'Get Sytem log EXE.file name and specification from ATS.ini file
            nSystErr = GetPrivateProfileString("File Locations", "SYSTEM_LOG", "", lpBuffer, 256, sATS_INI)
            SystemLogSpec = """" & Trim(StripNullCharacters(lpBuffer)) & """"

            'SystemLogSpec appends the
            ' selftest log file      ("C:\Users\Public\Documents\logfile")
            ' to the system log file "C:\Users\Public\Documents\SysLog.txt file")
            nSystErr = Shell(SystemLogSpec & " " & sLogFilePath & LOG_FILE, vbNormalFocus)
            If Err.Number <> 0 Then
                MsgBox(SystemLogSpec & " " & sLogFilePath & LOG_FILE)
                MsgBox("Error accessing system log program, results will not be logged.  " & ErrorToString() & "<" & Err.Number & ">")
                Err.Clear()
            End If
        Catch
            s = "Error appending logfile to system log file." & vbCrLf
            s = s & sLogFilePath & "logifle.txt" & vbCrLf
            s = s & "Results will not be logged.  " & vbCrLf
            s = s & "Error " & CStr(Err.Number) & ": " & Err.Description
            MsgBox(s)
            Reset()
        End Try

        EndData() 'FHDB Ending Record   06/20/01

        ErrorStatus = atxml_Close()
        Application.Exit()

    End Sub


    Sub HandleDetails()
        'DESCRIPTION:
        '   This routine displays or hides the details text box
        Dim iScreenHeight As Integer
        Dim i As Integer

        SystemParametersInfo(SPI_GETWORKAREA, 0, Rect, 0)
        iScreenHeight = (Rect.Bottom - Rect.Top)

        With frmSTest
            If DetailStatus = False Then
                i = iScreenHeight - .Top - .DetailsText.Top - 60
                If i < 0 Then
                    .DetailsText.Height = 0
                Else
                    .DetailsText.Height = i
                End If
                DetailStatus = True
                .cmdDetails.Text = "<< No Details"
                .Height = .Height + .DetailsText.Height - 5

                .DetailsText.Visible = True
                .DetailsText.Focus()
                .DetailsText.SelectionStart = Len(.DetailsText.Text)
                .DetailsText.Focus()
            Else
                DetailStatus = False
                .cmdDetails.Text = "Details >>"
                .sbrUserInformation.Text = "Press this button to view detailed testing information."
                .Height = 480
                .DetailsText.Visible = False
                .FrSearch.Visible = False
            End If
        End With

    End Sub


    Function InitMessageBasedInstrument(ByVal InstrumentToInit As Integer) As Integer
        'DESCRIPTION:
        '   This Routine initializes a message based instrument and verifies that it
        '   is the correct instrument
        'PARAMETERS:
        '   InstrumentToInit = The index of the instrument to initialize
        'RETURNS:
        '   An integer representing status PASSED, FAILED or UNTESTED
        'EXAMPLE:
        '   nSystErr = ReceiveMessage (DMMHandle&, Ret) '* This will retrieve a message from the DMM
        Dim IHandle As Integer
        Dim DevSpec As String
        Dim IDSpec As String
        Dim InitStatus As Integer = 0
        Dim Items As Integer
        Dim sMsg As String = ""
        Dim i As Integer

        InitMessageBasedInstrument = False

        IHandle = instrumentHandle(InstrumentToInit) 'The VISA session handle (zero to start with)
        DevSpec = InstrumentSpec(InstrumentToInit) 'The unique symbolic name of the resource
        IDSpec = IDNResponse(InstrumentToInit) 'The concatenation of the first two elements returned from the *IDN?

        'XXX-00-N01
        Echo(sInstrumentIDCode(InstrumentToInit) & "-00-N01")
        If InitStatus <> VI_SUCCESS Then
            Echo("Could not get instrument handle for " & DevSpec)
            FormatResultLine(InstrumentDescription(InstrumentToInit) & " initialization ", False)
            RegisterFailure(InstrumentToInit, ReturnTestNumber(InstrumentToInit, 1, 1, "N"), _
                 sComment:="    Initialization Failure. VISA Error Code: " & GetVisaErrorMessage(ResourceName(InstrumentToInit), InitStatus))
            InstrumentInitialized(InstrumentToInit) = False
            ReportFailure(InstrumentToInit)
            Exit Function
        End If

        nSystErr = vxiClear(InstrumentToInit)
        Delay(0.25)

        'Exclude the EO modules from unsupported commands

        If (InstrumentToInit <= LAST_RF_INST) Or (InstrumentToInit > LAST_EO_INST) Then
            nSystErr = WriteMsg(InstrumentToInit, "*RST") ' uses the cicl
            nSystErr = WriteMsg(InstrumentToInit, "*CLS")
            Delay(0.5)
            i = 1
            Do
                nSystErr = WriteMsg(InstrumentToInit, "SYST:ERR?")
                Delay(0.5)
                nSystErr = ReadMsg(InstrumentToInit, ReadBuffer)
                i += 1
                If i > 10 Then Exit Do
            Loop While Val(ReadBuffer) <> 0
        End If

        'XXX-00-N02
        nSystErr = WriteMsg(InstrumentToInit, "*IDN?") ' uses the cicl
        Delay(0.5)
        nSystErr = ReadMsg(InstrumentToInit, sMsg)

        Echo(sInstrumentIDCode(InstrumentToInit) & "-00-N02")
        Echo(DevSpec & " => *IDN? => " & sMsg)
        Items = StringToList(sMsg, IDList, ",")

        If IDSpec = "E1428A" Then
            If InStr(Trim(IDList(MANUF)) & Trim(IDList(MODEL)), "ZT1428") <> 0 Then
                IDSpec = "ZT1428"
                IDNResponse(OSCOPE) = "ZT1428" ' change instrument to ZTEC 1428
                InstrumentDescription(OSCOPE) = "ZTEC 1428 Digitizing Oscilloscope (SC Slot 1)"
            End If
        ElseIf IDSpec = "E1412A" Then
            If InStr(Trim(IDList(MANUF)) & Trim(IDList(MODEL)), "Racal Instruments4152A") <> 0 Then
                IDSpec = "Racal Instruments4152A"
                IDNResponse(DMM) = "4152A" ' change DMM to RACAL
                InstrumentDescription(DMM) = "Astronics 4152A DMM (PC Slot 11)"
            End If
        End If

        'now compare the IDN reponse to the expected response

        If InStr(1, Trim(IDList(MANUF)) & Trim(IDList(MODEL)), IDSpec, CompareMethod.Text) <> 0 Then ' changed to not case sensitive
            FormatResultLine(InstrumentDescription(InstrumentToInit) & " identification ", True)
            InitMessageBasedInstrument = True
            InstrumentInitialized(InstrumentToInit) = True


        Else
            Echo("Could not find instrument ID string: """ & Trim(IDList(MANUF)) & IDSpec & sQuote & " in " & sQuote & Trim(IDList(MANUF)) & Trim(IDList(MODEL)) & sQuote)
            FormatResultLine(InstrumentDescription(InstrumentToInit) & " identification ", False)

            RegisterFailure(InstrumentToInit, ReturnTestNumber(InstrumentToInit, 1, 2, "N"), sComment:="    Identification Failure. Unknown: " & Trim(IDList(MANUF)) & Trim(IDList(MODEL)))
            InstrumentInitialized(InstrumentToInit) = False
            ReportFailure(InstrumentToInit)
            InitMessageBasedInstrument = False
        End If

        instrumentHandle(InstrumentToInit) = IHandle

    End Function

    Function InitSwitching(ByVal iInst As Integer) As Integer
        'DESCRIPTION:
        '   This Routine checks to see if the proper switching cards are available and
        '   functioning.
        'PARAMETERS:
        '   iInst = the index of the instrument
        'RETURNS:
        '   True if all switching cards are available and configured correctly
        'MODIFIES:
        '   instrumentHandle&(SWITCH1)
        '   SwitchCardOK()
        'EXAMPLE:
        '   If InitSwitching(iInstToInitialize) Then
        Dim ErrorStatus As Integer
        Static InitStatus As Integer
        Dim RetDevParam As Short
        Dim MsgRespReg As Short
        Dim SwitchIDText As Short
        Dim Items As Short

        If InitStatus = 99 Then 'If no communication with switch controller
            Select Case iInst
                Case 3 : frmSTest.InstrumentLabel_3.Enabled = False
                    frmSTest.sscTest_3.Enabled = False
                Case 4 : frmSTest.InstrumentLabel_4.Enabled = False
                    frmSTest.sscTest_4.Enabled = False
                Case 5 : frmSTest.InstrumentLabel_5.Enabled = False
                    frmSTest.sscTest_5.Enabled = False
                Case 6 : frmSTest.InstrumentLabel_6.Enabled = False
                    frmSTest.sscTest_6.Enabled = False
                Case 7 : frmSTest.InstrumentLabel_7.Enabled = False
                    frmSTest.sscTest_7.Enabled = False
            End Select
            InitSwitching = False
            Exit Function
        End If
        InitSwitching = True

        Echo("")
        Select Case iInst
            Case SWITCH1
                'LF1-00-N01
                sTNum = "LF1-00-N01"
                Echo(sTNum)
                For Items = SWITCH_CONTROLLER To RFSWITCH
                    SwitchCardOK(Items) = False
                Next
                ErrorStatus = -1
                ErrorStatus = viOpen(SessionHandle, InstrumentSpec(SWITCH1), VI_NULL, VI_NULL, instrumentHandle(SWITCH1))
                '  ErrorStatus& = atxmlDF_viOpen(ResourceName(SWITCH1), SessionHandle&, InstrumentSpec(SWITCH1), VI_NULL, instrumentHandle&(SWITCH1))

                If ErrorStatus < VI_SUCCESS Then
                    Echo("Could not get instrument Handle for " & InstrumentSpec(SWITCH1))
                    FormatResultLine(InstrumentDescription(SWITCH1) & " initialization ", False)
                    RegisterFailure(CStr(SWITCH1), sTNum, , , , , "    Initialization Failure. VISA Error Code: " & GetVisaErrorMessage(ResourceName(SWITCH1), ErrorStatus))
                    InitStatus = 99
                    InitSwitching = False
                    Exit Function
                Else
                    FormatResultLine(InstrumentDescription(SWITCH1) & " initialization ", True)
                    nSystErr = WriteMsg(SWITCH1, "PDATAOUT 0-12")
                    Echo(InstrumentSpec(SWITCH1) & " => PDATAOUT 0-12 => . . .")
                    RetDevParam = 0
                    Do
                        ReDim Preserve IDData(RetDevParam)
                        nSystErr = ReadMsg(SWITCH1, IDData(RetDevParam))
                        Echo(IDData(RetDevParam))
                        RetDevParam += 1
                        ' Delay enough time for switching module to output another line of data to the output
                        '   buffer and then check the Data Output Ready(DOR) bit to see if there is another
                        '   line of text to be downloaded.  Repeat process until done
                        Delay(0.2)
                        MsgRespReg = &H3FF7
                        nSystErr = atxmlDF_viIn16(ResourceName(SWITCH1), 0, VI_A16_SPACE, MSG_RESP_REG, MsgRespReg)
                        If AbortTest = True Then Exit Function

                    Loop While ((MsgRespReg And DOR_MASK) <> 0) And (RetDevParam < 31)
                    For SwitchIDText = 0 To RetDevParam - 1
                        Items = StringToList(IDData(SwitchIDText), IDList, ".")
                        Select Case Val(IDList(1))
                            Case SWITCH_CONTROLLER
                                If Trim(IDList(2)) = "MODEL 1260 UNIVERSAL SWITCH CONTROLLER FOR VXI REV 1" Then
                                    SwitchCardOK(SWITCH_CONTROLLER) = True
                                End If
                            Case LFSWITCH1
                                If Trim(IDList(2)) = "1260-39 HIGH DENSITY MULTIPLE CONFIGURATION SWITCH MODULE" Then
                                    SwitchCardOK(LFSWITCH1) = True
                                End If
                            Case LFSWITCH2
                                If Trim(IDList(2)) = "1260-39 HIGH DENSITY MULTIPLE CONFIGURATION SWITCH MODULE" Then
                                    SwitchCardOK(LFSWITCH2) = True
                                End If
                            Case LFSWITCH3
                                If Trim(IDList(2)) = "1260-38A 1x128 2-WIRE  SCANNER/MULTIPLEXER" Then ' & vbCrLf & "SWITCH MODULE"
                                    SwitchCardOK(LFSWITCH3) = True
                                End If
                            Case MFSWITCH
                                If Trim(IDList(2)) = "1260-58 4 1x8 750 MHZ SWITCHING MODULE" Then
                                    SwitchCardOK(MFSWITCH) = True
                                End If
                            Case RFSWITCH
                                If Trim(IDList(2)) = "1260-66A Six 1x6 MICROWAVE SWITCHING MODULE" Then
                                    SwitchCardOK(RFSWITCH) = True
                                End If
                        End Select
                    Next
                End If

                'LF1-00-N02
                sTNum = "LF1-00-N02"
                Echo(sTNum)
                sMsg = InstrumentDescription(SWITCH1) & " Controller identification "
                If Not SwitchCardOK(SWITCH_CONTROLLER) Then
                    FormatResultLine(sMsg, False)
                    RegisterFailure(CStr(SWITCH1), sTNum, sComment:=sMsg & " FAILED.")
                    ReportFailure(SWITCH1)
                    InitSwitching = False
                Else
                    FormatResultLine(sMsg, True)
                End If

                'LF1-00-N03
                sTNum = "LF1-00-N03"
                Echo(sTNum)
                sMsg = InstrumentDescription(SWITCH1) & " identification "
                If Not SwitchCardOK(LFSWITCH1) Then
                    FormatResultLine(sMsg, False)
                    RegisterFailure(CStr(SWITCH1), sTNum, sComment:=sMsg & " FAILED.")
                    ReportFailure(SWITCH1)
                    InitSwitching = False
                Else
                    FormatResultLine(sMsg, True)
                End If

                'LF2-00-N01, LF3-00-N01, MFS-00-N01, HFS-00-N01
            Case SWITCH2, SWITCH3, SWITCH4, SWITCH5 ' now 4,5,6,7
                sTNum = sInstrumentIDCode(iInst) & "-00-N01"
                Echo(sTNum)
                sMsg = InstrumentDescription(iInst) & " identification "
                Dim cardId As Short
                Select Case iInst
                    Case 4 : cardId = 2 ' switch 2
                    Case 5 : cardId = 3 ' switch 3
                    Case 6 : cardId = 4 ' switch 4
                    Case 7 : cardId = 5 ' switch 5
                End Select
                If Not SwitchCardOK(cardId) Then
                    FormatResultLine(sMsg, False)
                    RegisterFailure(CStr(iInst), sTNum, sComment:=sMsg & " FAILED.")
                    ReportFailure(iInst)
                    InitSwitching = False
                Else
                    FormatResultLine(sMsg, True)
                End If
        End Select

    End Function

    Sub InitVar()
        'DESRIPTION:
        '   This routine initializes the program's variables

        Dim CardIndex As Short
        Dim i As Integer
        InitData() 'Initialize FHDB Data as soon as possible

        OptionMode = SOEmode ' Init run modes to SOF, SOE
        OptionFaultMode = SOFmode

        ProgramPath = Application.StartupPath
        i = InStr(ProgramPath, "\bin")
        If i > 0 Then
            ProgramPath = Left(ProgramPath, i - 1)
        End If
        ProgramPath &= "\"

        SystemStatus = PASSED
        For CardIndex = 0 To LAST_INSTRUMENT
            CardStatus(CardIndex) = PASSED
        Next CardIndex

        IDNResponse(DMM) = "E1412A" 'will be changed to "4152A" in InitMessageBasedInstrument for ATS-Viper/T
        IDNResponse(COUNTER) = "E1420B"
        IDNResponse(ARB) = "E1445A"
        IDNResponse(RFPM) = "E1416A"
        IDNResponse(RFSYN) = "1140B"
        IDNResponse(OSCOPE) = "E1428A" 'will be changed to "ZT1428" in InitMessageBasedInstrument for ATS-Viper/T
        IDNResponse(SYN_RES) = "65CS4"
        IDNResponse(FGEN) = "3152"

        IDNResponse(RFMEAS) = "VM2601"
        IDNResponse(EO_VCC) = "VCC"
        IDNResponse(VEO2) = "VEO"

        InstrumentSpec(OSCOPE) = "VXI::17::INSTR"
        InstrumentSpec(COUNTER) = "VXI::18::INSTR"
        InstrumentSpec(FGEN) = "VXI::19::INSTR"
        InstrumentSpec(RFSYN) = "VXI0::20::INSTR"
        InstrumentSpec(RFMEAS) = "VXI::25::INSTR"
        InstrumentSpec(RFPM) = "VXI::28::INSTR"
        InstrumentSpec(SYN_RES) = "VXI::30::INSTR"
        InstrumentSpec(SWITCH1) = "VXI::38::INSTR"
        InstrumentSpec(DMM) = "VXI::44::INSTR"
        InstrumentSpec(ARB) = "VXI::48::INSTR"

        InstrumentDescription(END_TO_END) = "End to End"
        InstrumentDescription(DMM) = "Racal Instruments 4152A DMM (PC Slot 11)"
        InstrumentDescription(COUNTER) = "HP E1420B Counter/Timer (SC Slot 2)"
        InstrumentDescription(ARB) = "HP E1445A Arbitrary Waveform Gen (PC Slot 12)"
        InstrumentDescription(DIGITAL) = "Teradyne M910 Digital Subsystem (PC Slots 1-4) "
        InstrumentDescription(FGEN) = "Racal Instruments 3152A Function Gen (SC Slot 3)"
        InstrumentDescription(RFPM) = "Phase Matrix 1313B RF Power Meter (SC Slot 7)"
        InstrumentDescription(RFSYN) = "Phase Matrix 1140B RF Synthesizer (SC Slots 4-6)"
        InstrumentDescription(RFMEAS) = "PM 1313B,20309, VXITech VM2601IF,VM7510 (SC Slots7-9)"
        InstrumentDescription(DOWNCONV) = "Phase Matrix 1313B-D7 (SC Slot 7)"
        InstrumentDescription(LOCALOSC) = "Phase Matrix 20309 (SC Slot 8)"
        InstrumentDescription(DIGITIZER) = "VXITech VM2601IF (SC Slot 9)"
        InstrumentDescription(CALIBRATOR) = "VXITech VM7510 (SC Slot 9)"

        InstrumentDescription(OSCOPE) = "ZTEC 1428VXI Digitizing Oscilloscope (SC Slot 1)"
        InstrumentDescription(PPU) = "Freedom Power Systems FPU/PPU 6062"
        InstrumentDescription(SWITCH1) = "Racal 1260-39,OPT1 LF Sw #1 (PC Slot 5)"
        InstrumentDescription(SWITCH2) = "Racal 1260-39 LF Sw #2 (PC Slot 6)"
        InstrumentDescription(SWITCH3) = "Racal 1260-38T LF Sw #3 (PC Slot 7)"
        InstrumentDescription(SWITCH4) = "Racal 1260-58 MF Sw (PC Slot 8)"
        InstrumentDescription(SWITCH5) = "Racal 1260-67A HF Sw (PC Slot 9)"
        InstrumentDescription(MIL_STD_1553) = "Ballard Lx1553-5, 1553 I/F (CIC Slot 8)"
        InstrumentDescription(SYN_RES) = "North Atlantic Instruments 65CS4 (SC Slot 12)"
        InstrumentDescription(APROBE) = "Analog Probe"
        InstrumentDescription(EO_VCC) = "Epix PIXC1-A310 (CIC Slot 9)"
        InstrumentDescription(VEO2) = "SBIR 93006G7100 Viper/T EO"
        InstrumentDescription(VEO_LARRS) = "SBIR LARRS"

        InstrumentDescription(SERIALCCA) = "SeaLevel Systems 7404 Serial Bus (CIC Slot 3)"
        InstrumentDescription(RS422) = "RS422 9 pin SeaLevel Systems 7404 Serial Bus (CIC Slot 3)"
        InstrumentDescription(RS232) = "RS232 9 pin COM2 (CIC Slot 1)"
        InstrumentDescription(GIGA) = "Intel i350-T4V2 (CIC Slot 10)"
        InstrumentDescription(CANBUS) = "Tews TPMC810 Can Bus Interface (CIC Slot 4)"

        'ATS.INI keys for sections [Self Test], [Serial Number], [Calibration]
        InstFileName(DIGITAL) = "DTS"
        InstFileName(SWITCH1) = "LFS1"
        InstFileName(SWITCH2) = "LFS2"
        InstFileName(SWITCH3) = "LFS3"
        InstFileName(SWITCH4) = "MFS"
        InstFileName(SWITCH5) = "HFS"
        InstFileName(DMM) = "DMM"
        InstFileName(ARB) = "ARB"
        InstFileName(COUNTER) = "UCT"
        InstFileName(FGEN) = "FGEN"
        InstFileName(RFMEAS) = "RFM"
        InstFileName(DOWNCONV) = "RFD"
        InstFileName(LOCALOSC) = "RFLO"
        InstFileName(DIGITIZER) = "RFDIG"

        InstFileName(RFSYN) = "RFS"
        InstFileName(RFPM) = "RFP"
        InstFileName(OSCOPE) = "DSCOPE"
        InstFileName(PPU) = "UUTPS"
        InstFileName(MIL_STD_1553) = "MIL1553"
        InstFileName(EO_VCC) = "EO_VIDEOCAP"
        InstFileName(VEO2) = "EO_LASER"

        InstFileName(SERIALCCA) = "RS232_422_485"
        InstFileName(RS422) = "RS422"
        InstFileName(RS232) = "RS232"
        InstFileName(GIGA) = "GIGABIT ETHERNET"
        InstFileName(CANBUS) = "CANBUS"
        InstFileName(SYN_RES) = "SYN_RES"

        sGuiLabel(END_TO_END) = "End to End"
        sGuiLabel(MIL_STD_1553) = "MIL-STD-1553 Interface  (CIC Slot 8)"
        sGuiLabel(DIGITAL) = "Digital Test Subsystem (PC Slots 1-4)"
        sGuiLabel(SWITCH1) = "Low Frequency Switches #1 (PC Slot 5)"
        sGuiLabel(SWITCH2) = "Low Frequency Switches #2 (PC Slot 6)"
        sGuiLabel(SWITCH3) = "Low Frequency Switches #3 (PC Slot 7)"
        sGuiLabel(SWITCH4) = "Medium Frequency Switches (PC Slot 8)"
        sGuiLabel(SWITCH5) = "High Frequency Switches   (PC Slot 9)"
        sGuiLabel(DMM) = "Digital Multimeter (PC Slot 11)"
        sGuiLabel(OSCOPE) = "Digitizing Oscilloscope (SC Slot 1)"
        sGuiLabel(COUNTER) = "Counter/Timer (SC Slot 2)"
        sGuiLabel(ARB) = "Arbitrary Waveform Gen (PC Slot 12)"
        sGuiLabel(FGEN) = "Function Generator (SC Slot 3)"
        sGuiLabel(PPU) = "Programmable Power Unit (PPU Slot C,D)"
        sGuiLabel(SYN_RES) = "Synchro/Resolver (SC Slot 12)"

        sGuiLabel(SERIALCCA) = "Serial Bus RS232/422/485  (CIC Slot 3)" ' serial busses (one 37 pin connector)
        sGuiLabel(RS422) = "RS422 COM1 (Serial Bus CIC Slot 3)"        ' (one 9 pin connector, also in the serial card)
        sGuiLabel(RS232) = "RS232 COM2 (CIC Slot 1)"                 ' (one 9 pin connector, via CIC)
        sGuiLabel(GIGA) = "Gigabit Ethernet (CIC Slot 10)"              ' (via CIC back panel)
        sGuiLabel(CANBUS) = "CAN Bus (CIC Slot 4)"                      ' (via PMC carrier SC slot 11)

        sGuiLabel(APROBE) = "Analog Probe (Receiver P2)"
        sGuiLabel(RFPM) = "RF Power Meter (SC Slot 7)"
        sGuiLabel(RFSYN) = "RF Stimulus   (SC Slots 4-6)"
        sGuiLabel(RFMEAS) = "RF Measurement Analyzer (SC Slots 7-9)"

        sGuiLabel(EO_VCC) = "Video Capture Module (CIC Slot 9)"
        sGuiLabel(VEO2) = "Viper/T EO (VEO2) "

        sGuiLabel(DOWNCONV) = "Down Converter (SC Slot 7)"
        sGuiLabel(LOCALOSC) = "Local Oscillator (SC Slot 8)"
        sGuiLabel(DIGITIZER) = "Digitizer (SC Slot 9)"

        For i = 1 To LAST_INSTRUMENT
            InstrumentStatus(i) = UNTESTED
        Next i

        'Added to Support Instrument Identification for Failure Step in the FHDB
        '07/09/01
        sInstrumentIDCode(PPU) = "PPU"
        sInstrumentIDCode(MIL_STD_1553) = "BUS"
        sInstrumentIDCode(DMM) = "DMM"
        sInstrumentIDCode(COUNTER) = "C/T"
        sInstrumentIDCode(OSCOPE) = "DSO"
        sInstrumentIDCode(FGEN) = "FGN"
        sInstrumentIDCode(ARB) = "ARB"
        sInstrumentIDCode(APROBE) = "APR"
        sInstrumentIDCode(DIGITAL) = "DTS"
        sInstrumentIDCode(SWITCH1) = "LF1"
        sInstrumentIDCode(SWITCH2) = "LF2"
        sInstrumentIDCode(SWITCH3) = "LF3"
        sInstrumentIDCode(SWITCH4) = "MFS"
        sInstrumentIDCode(SWITCH5) = "HFS"
        sInstrumentIDCode(SYN_RES) = "S/R"

        sInstrumentIDCode(SERIALCCA) = "SER"
        sInstrumentIDCode(RS422) = "422"
        sInstrumentIDCode(RS232) = "232"
        sInstrumentIDCode(GIGA) = "GBE"
        sInstrumentIDCode(CANBUS) = "CAN"
        sInstrumentIDCode(RFPM) = "RPM"
        sInstrumentIDCode(RFSYN) = "RST"
        sInstrumentIDCode(RFMEAS) = "RMA"
        sInstrumentIDCode(EO_VCC) = "EVC"
        sInstrumentIDCode(VEO2) = "EMF"

        'Added to Support CICL
        ResourceName(MIL_STD_1553) = "MIL1553_1"
        ResourceName(DIGITAL) = "DWG_1"
        ResourceName(SWITCH1) = "PAWS_SWITCH"
        ResourceName(SWITCH2) = "PAWS_SWITCH"
        ResourceName(SWITCH3) = "PAWS_SWITCH"
        ResourceName(SWITCH4) = "PAWS_SWITCH"
        ResourceName(SWITCH5) = "PAWS_SWITCH"
        ResourceName(DMM) = "DMM_1"
        ResourceName(OSCOPE) = "DSO_1"
        ResourceName(COUNTER) = "CNTR_1"
        ResourceName(ARB) = "ARB_GEN_1"
        ResourceName(FGEN) = "FUNC_GEN_1"
        ResourceName(PPU) = "DCP_1"
        ResourceName(SYN_RES) = "SRS_1_DS1"
        ResourceName(SERIALCCA) = "PCI_SERIAL_1"
        ResourceName(RS422) = "COM_1"
        ResourceName(RS232) = "COM_2"
        ResourceName(GIGA) = "ETHERNET_1"
        ResourceName(CANBUS) = "CAN_1"
        ResourceName(APROBE) = "PROBE"
        ResourceName(RFPM) = "RF_PWR_1"
        ResourceName(RFSYN) = "RFGEN_1"
        ResourceName(RFMEAS) = "RF_MEASAN_1"
        'ResourceName(EO_VC) = "EVC"

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

        ComResourceName(1) = "COM_1"
        ComResourceName(2) = "COM_2"
        ComResourceName(3) = "PCI_SERIAL_1"
        ComResourceName(4) = "PCI_SERIAL_1"
        ComResourceName(5) = "PCI_SERIAL_1"

        'Added for Loop On Step        
        sTestName(MIL_STD_1553) = "1553"
        sTestName(DIGITAL) = "DTS"
        sTestName(SWITCH1) = "LF1 Switch"
        sTestName(SWITCH2) = "LF2 Switch"
        sTestName(SWITCH3) = "LF3 Switch"
        sTestName(SWITCH4) = "MF Switch"
        sTestName(SWITCH5) = "HF Switch"
        sTestName(DMM) = "DMM"
        sTestName(OSCOPE) = "DSO"
        sTestName(COUNTER) = "C/T"
        sTestName(ARB) = "ARB"
        sTestName(FGEN) = "FGEN"
        sTestName(PPU) = "PPU"
        sTestName(SYN_RES) = "S/R"
        sTestName(SERIALCCA) = "PCI SERIAL"
        sTestName(RS422) = "RS422 COM1"
        sTestName(RS232) = "RS232 COM2"
        sTestName(GIGA) = "ETHERNET"
        sTestName(CANBUS) = "CANBUS"
        sTestName(APROBE) = "Analog Probe"
        sTestName(RFPM) = "RF PM"
        sTestName(RFSYN) = "RF STIM"
        sTestName(RFMEAS) = "RF MA"
        sTestName(EO_VCC) = "VCC"
        sTestName(VEO2) = "VEO2"

        RunningEndToEnd = False
        AppHandle = -1
        STLogHandle = -1


    End Sub



    Function ReadMsg(ByVal InstrumentIndex As Integer, ByRef ReturnMessage As String) As Integer

        'DESCRIPTION:
        '   This Routine is a pass through to the VISA layer using VB conventions to
        '   facilitate clean Word Serial read communications to message based instruments.
        'PARAMETERS:
        '   Instrument = the name of the instrument to read
        '   ReturnMessage = the string returned to VB
        'RETURNS:
        '   A long integer representing any errors that may occur
        'EXAMPLE:
        '   nSystErr = ReceiveMessage (DMMHandle&, Ret) '* This will retrieve a message from the DMM
        Dim retCount As Integer
        Dim S As String = ""
        ReadBuffer = ""
        ReturnMessage = Space(256)
        Application.DoEvents()

        ReturnMessage = Space(256)
        nSystErr = atxml_ReadCmds(ResourceName(InstrumentIndex), ReturnMessage, 256, retCount)
        '    nSystErr = atxmlDF_viRead(ResourceName(Instrument), 0, ReturnMessage, 256, retCount&)

        Application.DoEvents()
        ReturnMessage = Strings.Left(CStr(ReturnMessage), retCount)
        ReturnMessage = StripNullCharacters(ReturnMessage)
        ReadMsg = nSystErr

    End Function


    Sub ReportFailure(ByVal instrumentIndex As Short)

        'DESCRIPTION:
        '   This routine places a red 'X' in the appropriate box of a failing
        '   instrument
        'PARAMETERS:
        '   InstrumentIndex = The index of the failing instrument

        InstrumentStatus(instrumentIndex) = FAILED
        Select Case iSelGUI
            Case 0 : frmSTest.PassFrame_0.Image = frmSTest.picTestFailed.Image
            Case 1 : frmSTest.PassFrame_1.Image = frmSTest.picTestFailed.Image
            Case 2 : frmSTest.PassFrame_2.Image = frmSTest.picTestFailed.Image
            Case 3 : frmSTest.PassFrame_3.Image = frmSTest.picTestFailed.Image
            Case 4 : frmSTest.PassFrame_4.Image = frmSTest.picTestFailed.Image
            Case 5 : frmSTest.PassFrame_5.Image = frmSTest.picTestFailed.Image
            Case 6 : frmSTest.PassFrame_6.Image = frmSTest.picTestFailed.Image
            Case 7 : frmSTest.PassFrame_7.Image = frmSTest.picTestFailed.Image
            Case 8 : frmSTest.PassFrame_8.Image = frmSTest.picTestFailed.Image
            Case 9 : frmSTest.PassFrame_9.Image = frmSTest.picTestFailed.Image
            Case 10 : frmSTest.PassFrame_10.Image = frmSTest.picTestFailed.Image
            Case 11 : frmSTest.PassFrame_11.Image = frmSTest.picTestFailed.Image
            Case 12 : frmSTest.PassFrame_12.Image = frmSTest.picTestFailed.Image
            Case 13 : frmSTest.PassFrame_13.Image = frmSTest.picTestFailed.Image
            Case 14 : frmSTest.PassFrame_14.Image = frmSTest.picTestFailed.Image
            Case 15 : frmSTest.PassFrame_15.Image = frmSTest.picTestFailed.Image
            Case 16 : frmSTest.PassFrame_16.Image = frmSTest.picTestFailed.Image
            Case 17 : frmSTest.PassFrame_17.Image = frmSTest.picTestFailed.Image
            Case 18 : frmSTest.PassFrame_18.Image = frmSTest.picTestFailed.Image
            Case 19 : frmSTest.PassFrame_19.Image = frmSTest.picTestFailed.Image
            Case 20 : frmSTest.PassFrame_20.Image = frmSTest.picTestFailed.Image
            Case 21 : frmSTest.PassFrame_21.Image = frmSTest.picTestFailed.Image
            Case 22 : frmSTest.PassFrame_22.Image = frmSTest.picTestFailed.Image
            Case 23 : frmSTest.PassFrame_23.Image = frmSTest.picTestFailed.Image
            Case 24 : frmSTest.PassFrame_24.Image = frmSTest.picTestFailed.Image
            Case 25 : frmSTest.PassFrame_25.Image = frmSTest.picTestFailed.Image
        End Select
        SystemStatus = FAILED
        CardStatus(instrumentIndex) = FAILED
        SaveInstStatus(instrumentIndex, FAILED)
        bTestFailed = True 'Set switch to indicate a failure

    End Sub

    Sub ReportUnknown(ByVal instrumentIndex As Short)

        'DESCRIPTION:
        '   This routine places a black '?' in the appropraite box of a untested
        '   instrument
        'PARAMETERS:
        '   InstrumentIndex = The index of the failing instrument
        Select Case iSelGUI
            Case 0 : frmSTest.PassFrame_0.Image = frmSTest.picTestUnknown.Image
            Case 1 : frmSTest.PassFrame_1.Image = frmSTest.picTestUnknown.Image
            Case 2 : frmSTest.PassFrame_2.Image = frmSTest.picTestUnknown.Image
            Case 3 : frmSTest.PassFrame_3.Image = frmSTest.picTestUnknown.Image
            Case 4 : frmSTest.PassFrame_4.Image = frmSTest.picTestUnknown.Image
            Case 5 : frmSTest.PassFrame_5.Image = frmSTest.picTestUnknown.Image
            Case 6 : frmSTest.PassFrame_6.Image = frmSTest.picTestUnknown.Image
            Case 7 : frmSTest.PassFrame_7.Image = frmSTest.picTestUnknown.Image
            Case 8 : frmSTest.PassFrame_8.Image = frmSTest.picTestUnknown.Image
            Case 9 : frmSTest.PassFrame_9.Image = frmSTest.picTestUnknown.Image
            Case 10 : frmSTest.PassFrame_10.Image = frmSTest.picTestUnknown.Image
            Case 11 : frmSTest.PassFrame_11.Image = frmSTest.picTestUnknown.Image
            Case 12 : frmSTest.PassFrame_12.Image = frmSTest.picTestUnknown.Image
            Case 13 : frmSTest.PassFrame_13.Image = frmSTest.picTestUnknown.Image
            Case 14 : frmSTest.PassFrame_14.Image = frmSTest.picTestUnknown.Image
            Case 15 : frmSTest.PassFrame_15.Image = frmSTest.picTestUnknown.Image
            Case 16 : frmSTest.PassFrame_16.Image = frmSTest.picTestUnknown.Image
            Case 17 : frmSTest.PassFrame_17.Image = frmSTest.picTestUnknown.Image
            Case 18 : frmSTest.PassFrame_18.Image = frmSTest.picTestUnknown.Image
            Case 19 : frmSTest.PassFrame_19.Image = frmSTest.picTestUnknown.Image
            Case 20 : frmSTest.PassFrame_20.Image = frmSTest.picTestUnknown.Image
            Case 21 : frmSTest.PassFrame_21.Image = frmSTest.picTestUnknown.Image
            Case 22 : frmSTest.PassFrame_22.Image = frmSTest.picTestUnknown.Image
            Case 23 : frmSTest.PassFrame_23.Image = frmSTest.picTestUnknown.Image
            Case 24 : frmSTest.PassFrame_24.Image = frmSTest.picTestUnknown.Image
            Case 25 : frmSTest.PassFrame_25.Image = frmSTest.picTestUnknown.Image
        End Select
        SystemStatus = FAILED

        SaveInstStatus(instrumentIndex, FAILED)

    End Sub

    Sub ReportPass(ByVal instrumentIndex As Integer)

        'DESCRIPTION:
        '  This routine places a green check in the appropriate box of a passing instrument
        '  It also will retrieve the current status of the instrument from in the ATS.INI
        '  file and call the "EnterNewCalData" routine if the previous status was failed.
        '  Then, it will write the new instrument status as PASSED in the ATS.INI file.

        'PARAMETERS:
        '   InstrumentIndex = The index of the passing instrument

        Dim lpBuffer As String = Space(256)
        Dim nNumChar As Integer

        InstrumentStatus(instrumentIndex) = PASSED
        Select Case iSelGUI
            Case 0 : frmSTest.PassFrame_0.Image = frmSTest.picTestPassed.Image
            Case 1 : frmSTest.PassFrame_1.Image = frmSTest.picTestPassed.Image
            Case 2 : frmSTest.PassFrame_2.Image = frmSTest.picTestPassed.Image
            Case 3 : frmSTest.PassFrame_3.Image = frmSTest.picTestPassed.Image
            Case 4 : frmSTest.PassFrame_4.Image = frmSTest.picTestPassed.Image
            Case 5 : frmSTest.PassFrame_5.Image = frmSTest.picTestPassed.Image
            Case 6 : frmSTest.PassFrame_6.Image = frmSTest.picTestPassed.Image
            Case 7 : frmSTest.PassFrame_7.Image = frmSTest.picTestPassed.Image
            Case 8 : frmSTest.PassFrame_8.Image = frmSTest.picTestPassed.Image
            Case 9 : frmSTest.PassFrame_9.Image = frmSTest.picTestPassed.Image
            Case 10 : frmSTest.PassFrame_10.Image = frmSTest.picTestPassed.Image
            Case 11 : frmSTest.PassFrame_11.Image = frmSTest.picTestPassed.Image
            Case 12 : frmSTest.PassFrame_12.Image = frmSTest.picTestPassed.Image
            Case 13 : frmSTest.PassFrame_13.Image = frmSTest.picTestPassed.Image
            Case 14 : frmSTest.PassFrame_14.Image = frmSTest.picTestPassed.Image
            Case 15 : frmSTest.PassFrame_15.Image = frmSTest.picTestPassed.Image
            Case 16 : frmSTest.PassFrame_16.Image = frmSTest.picTestPassed.Image
            Case 17 : frmSTest.PassFrame_17.Image = frmSTest.picTestPassed.Image
            Case 18 : frmSTest.PassFrame_18.Image = frmSTest.picTestPassed.Image
            Case 19 : frmSTest.PassFrame_19.Image = frmSTest.picTestPassed.Image
            Case 20 : frmSTest.PassFrame_20.Image = frmSTest.picTestPassed.Image
            Case 21 : frmSTest.PassFrame_21.Image = frmSTest.picTestPassed.Image
            Case 22 : frmSTest.PassFrame_22.Image = frmSTest.picTestPassed.Image
            Case 23 : frmSTest.PassFrame_23.Image = frmSTest.picTestPassed.Image
            Case 24 : frmSTest.PassFrame_24.Image = frmSTest.picTestPassed.Image
            Case 25 : frmSTest.PassFrame_25.Image = frmSTest.picTestPassed.Image
        End Select

        lpBuffer = Space(256)
        nNumChar = GetPrivateProfileString("Self Test", InstFileName(instrumentIndex), "PASSED", lpBuffer, 256, sATS_INI)

        If Strings.Left(lpBuffer, nNumChar) = "FAILED" Then
            If OptionMode <> LOEmode Then
                EnterNewCalDate(instrumentIndex)
            End If
        End If

        SaveInstStatus(instrumentIndex, PASSED)

    End Sub


    Sub SaveInstStatus(ByVal instrumentIndex As Integer, ByVal status As Short)
        'DESCRIPTION:
        '   This routine saves the pass or fail status to the ATS.INI file
        'PARAMETERS:
        '   InstrumentIndex = The index of the instrument of which to record
        '                      pass or fail information
        '   status          = The pass or fail status of the instrument
        Dim sString As String

        If status = PASSED Then
            sString = "PASSED"
        Else
            sString = "FAILED"
        End If

        WritePrivateProfileString("Self Test", InstFileName(instrumentIndex), sString, sATS_INI)
    End Sub



    Function SwitchingError(ByRef mModule As Integer) As Integer
        'DESCRIPTION
        '   This routine returns any errors reported by the 1260 relay cards
        'PARAMETERS:
        '   This integer is returned with the erroring switching module number
        'RETURNS:
        '   The error code of the switching card
        Dim errorcode As String = ""
        Dim PerPosition As Short

        nSystErr = WriteMsg(SWITCH1, "YERR")
        nSystErr = ReadMsg(SWITCH1, errorcode)
        If Len(errorcode) > 4 Then
            PerPosition = InStr(errorcode, ".")
            mModule = Val(Mid(errorcode, PerPosition - 3, 3))
            SwitchingError = Val(Mid(errorcode, PerPosition + 1, 2))
        Else
            SwitchingError = nSystErr
        End If

    End Function


    Public Function EngNotate(ByVal Number As Double, Optional ByRef iDPlaces As Integer = 0) As String

        'DESCRIPTION:
        '   Returns passed number as numeric string in Engineering notation (every
        '   3rd exponent) with selectable precision along with Unit Of Measure.
        'PARAMETERS:
        '   Number#:    The number to be formatted

        Dim MULTIPLIER As Integer
        Dim Negative As Integer
        Dim Prefix As String
        Dim returnstring As String
        Dim temp As String

        EngNotate = ""
        MULTIPLIER = 0
        Negative = False 'Initialize local variables
        If iDPlaces = 0 Then
            iDPlaces = 3
        End If

        If Number < 0 Then 'If negative
            Number = Math.Abs(Number) 'Make it positive for now
            Negative = True 'Set flag
        End If
        If Number > 8.0E+36 Then
            EngNotate = "9.99E+38" ' highest possible value
            Exit Function
        End If

        If Number >= 1000 Then 'For positive exponent
            Do While Number >= 1000 And MULTIPLIER <= 4
                Number /= 1000
                MULTIPLIER += 1
            Loop
        ElseIf Number < 1 And Number <> 0 Then            'For negative exponent (but not 0)
            Do While Number < 1 And MULTIPLIER >= -4
                Number *= 1000
                MULTIPLIER -= 1
            Loop
        End If

        Select Case MULTIPLIER
            Case 4
                Prefix = " T" 'Tetra  E+12
            Case 3
                Prefix = " G" 'Giga   E+09
            Case 2
                Prefix = " M" 'Mega   E+06
            Case 1
                Prefix = " K" 'kilo   E+03
            Case 0
                Prefix = " " '<none> E+00
            Case -1
                Prefix = " m" 'milli  E-03
                'Case -2: Prefix = " µ"     'micro  E-06 µVolts or µOhms,,,
            Case -2
                Prefix = " u" 'micro  E-06 changed because the Mu char does not show in all text viewers.
            Case -3
                Prefix = " n" 'nano   E-09
            Case -4
                Prefix = " p" 'pico   E-12

            Case Else
                Prefix = " "
                Number = 0
                MULTIPLIER = 0
        End Select

        If Negative Then Number = -Number

        If MULTIPLIER > 4 Then
            returnstring = "Ovr Rng"
        Else
            Number = Val(CStr(Number)) ' Clear out very low LSBs from binary math
            If iDPlaces = 3 Then
                returnstring = Format(Number, "0.0##")
            Else
                temp = StrDup(iDPlaces, "0")
                returnstring = Format(Number, "0." & temp)
            End If
        End If

        EngNotate = returnstring & Prefix

    End Function

    Sub UpdateStatus(ByRef InstIndex As Short)
        'DESCRIPTION:
        '   This routine updates the status bar at the bottom of the main panel
        'PARAMETERS:
        '   This is an index which references the message to be displayed
        '   on the main panel status bar

        Select Case InstIndex
            Case Is > GENERAL_MESSAGE
                ' (0 to +32)

                Select Case InstIndex
                    Case 0 : InstIndex = frmSTest.sscTest_0.Tag
                    Case 1 : InstIndex = frmSTest.sscTest_1.Tag
                    Case 2 : InstIndex = frmSTest.sscTest_2.Tag
                    Case 3 : InstIndex = frmSTest.sscTest_3.Tag
                    Case 4 : InstIndex = frmSTest.sscTest_4.Tag
                    Case 5 : InstIndex = frmSTest.sscTest_5.Tag
                    Case 6 : InstIndex = frmSTest.sscTest_6.Tag
                    Case 7 : InstIndex = frmSTest.sscTest_7.Tag
                    Case 8 : InstIndex = frmSTest.sscTest_8.Tag
                    Case 9 : InstIndex = frmSTest.sscTest_9.Tag
                    Case 10 : InstIndex = frmSTest.sscTest_10.Tag
                    Case 11 : InstIndex = frmSTest.sscTest_11.Tag
                    Case 12 : InstIndex = frmSTest.sscTest_12.Tag
                    Case 13 : InstIndex = frmSTest.sscTest_13.Tag
                    Case 14 : InstIndex = frmSTest.sscTest_14.Tag
                    Case 15 : InstIndex = frmSTest.sscTest_15.Tag
                    Case 16 : InstIndex = frmSTest.sscTest_16.Tag
                    Case 17 : InstIndex = frmSTest.sscTest_17.Tag
                    Case 18 : InstIndex = frmSTest.sscTest_18.Tag
                    Case 19 : InstIndex = frmSTest.sscTest_19.Tag
                    Case 20 : InstIndex = frmSTest.sscTest_20.Tag
                    Case 21 : InstIndex = frmSTest.sscTest_21.Tag
                    Case 22 : InstIndex = frmSTest.sscTest_22.Tag
                    Case 23 : InstIndex = frmSTest.sscTest_23.Tag
                    Case 24 : InstIndex = frmSTest.sscTest_24.Tag
                    Case 25 : InstIndex = frmSTest.sscTest_25.Tag
                End Select
                If STestBusy = False Then
                    If InstIndex = 0 Then
                        frmSTest.sbrUserInformation.Text = "Press this button to run all System Self Tests."
                    Else
                        frmSTest.sbrUserInformation.Text = "Press this button to run the test." & InstrumentDescription(InstIndex) & " Right-click to view dependencies"
                    End If
                End If
            Case GENERAL_MESSAGE
                ' (-1)
                If STestBusy = False Then
                    frmSTest.sbrUserInformation.Text = "Ready."
                Else
                    frmSTest.sbrUserInformation.Text = TestingMessage
                End If
            Case CLOSE_BUTTON
                ' (-2)
                frmSTest.sbrUserInformation.Text = "Press this button to shut down the self test program."
            Case ABORT_BUTTON
                ' (-3)
                frmSTest.sbrUserInformation.Text = "Press this button to abort the test currently running."
            Case DETAILS_BUTTON
                ' (-4)
                If DetailStatus = True Then
                    frmSTest.sbrUserInformation.Text = "Press this button to hide detailed testing information."
                Else
                    frmSTest.sbrUserInformation.Text = "Press this button to view detailed testing information."
                End If
            Case DETAILS_TEXT
                ' (-5)
                '    frmSTest.sbrUserInformation.SimpleText = "Detailed testing information log."
            Case PROGRESS_BAR
                ' (-6)
                '    frmSTest.sbrUserInformation.SimpleText = "Progress of current test procedure."
            Case HELP_BUTTON
                ' (-7)
                frmSTest.sbrUserInformation.Text = "Press this button to open the Selftest Help program."
        End Select

    End Sub


    Sub WaitForWarmUpTime()
        'DESCRIPTION:
        '   This routine causes the program to wait the proper warm-up time
        '   since tester start up, or until the user cancels the wait function

        Dim StartUpTime As Double
        Dim TimeDif As Double
        Dim CountDownTime As Double
        Dim Minutes_ As Integer
        Dim Seconds_ As Integer
        Dim GetNowTime As Double
        Dim Answer As Integer
        Dim FirstTime As Integer = 0

        Try ' On Error GoTo ErrorHandler
            StartUpTime = GetStartUpTime() ' from ATS.INI file
            GetNowTime = Now.ToOADate
            TimeDif = (GetNowTime - StartUpTime) * (24 * 60 * 60) ' in seconds

            Do While TimeDif < 1800 'Seconds (30 minutes)
                Application.DoEvents()
                If FirstTime = 0 Then
                    frmWait.Top = frmSTest.Top + CInt((frmSTest.Height - frmWait.Height) / 2)
                    frmWait.Left = frmSTest.Left + CInt((frmSTest.Width - frmWait.Width) / 2)
                    frmWait.Show()
                    frmWait.BringToFront()
                    FirstTime = 1
                End If

                If IgnorePressed Then
                    Answer = MsgBox("ATTENTION!: Starting this session before proper warm-up procedures could result in erroneous failures.  Do you want to ignore the warm up period?", MsgBoxStyle.YesNo, "WARNING")
                    If Answer = vbYes Then
                        Echo("*******************************")
                        Echo("*          ATTENTION:         *")
                        Echo("* This session started before *")
                        Echo("*  the proper amount of warm  *")
                        Echo("*     up time has expired.    *")
                        Echo("*    This may yield invalid   *")
                        Echo("*   data resulting erroneous  *")
                        Echo("*          failures.          *")
                        Echo("*******************************")

                        'DR#267, 03/15/02
                        'Add this message to the FaultCallout of the first Failing Record.
                        sGeneralInfo = vbCrLf & vbCrLf & "        -----  General Information  -----" & vbCrLf & "This session started before the proper amount of warm up time has expired." & vbCrLf & "This may yield invalid data resulting erroneous failures."
                        Exit Do
                    End If
                    IgnorePressed = False
                ElseIf AbortTest = True Then
                    CloseProgram = True
                    Exit Do
                End If

                GetNowTime = Now.ToOADate
                TimeDif = (GetNowTime - StartUpTime) * (24 * 60 * 60) ' in seconds
                CountDownTime = 1800 - TimeDif
                Minutes_ = CInt(CountDownTime \ 60)
                Seconds_ = CountDownTime - (60 * Minutes_)
                frmWait.lblMin.Text = CStr(Minutes_)
                If Seconds_ > -1 Then
                    frmWait.lblSec.Text = CStr(Seconds_)
                End If
                If TimeDif > 0 Then 'This should always be true
                    frmWait.proCountdown.Value = CInt(TimeDif)
                End If
            Loop
            frmWait.Close()
            frmWait.Dispose()
            Exit Sub
        Catch   ' ErrorHandler:
            frmWait.Close()
            frmWait.Dispose()

            MsgBox("Could not get system startup time.")
            Exit Sub
        End Try
    End Sub

    Function WaitForResponse(ByVal instrumentIndex As Short, ByVal ProgIncr As Single) As Integer
        'DESCRIPTION
        '   This routine waits for a message to be placed in the instruments output
        '   buffer queue and blinks the appropriate text.
        'PARAMETERS:
        '   InstrumentIndex = The Index of the instrument for which to wait
        '   ProgIncr!        = The progress increment for each .1 second
        Dim count As Short
        Dim MsgRespReg As Short
        WaitForResponse = 0

        For count = 1 To 600 'This allows for a wait of about 60 seconds max
            Delay(0.1)
            If frmSTest.proProgress.Value + ProgIncr < 100 Then
                frmSTest.proProgress.Value = frmSTest.proProgress.Value + CInt(ProgIncr)
            End If
            'Check to see if Data Output Ready (DOR) bit goes high
            '     nSystErr = viIn16(instrumentHandle&(instrumentIndex), VI_A16_SPACE, MSG_RESP_REG, MsgRespReg)
            nSystErr = atxmlDF_viIn16(ResourceName(instrumentIndex), 0, VI_A16_SPACE, MSG_RESP_REG, MsgRespReg)

            If (MsgRespReg And DOR_MASK) <> 0 Then
                WaitForResponse = nSystErr
                Exit Function
            End If
            If AbortTest = True Then Exit For
        Next count
        WaitForResponse = nSystErr

    End Function

    Function vxiClear(InstrumentIndex As Integer) As Integer

        nSystErr = atxmlDF_viClear(ResourceName(InstrumentIndex), 0)
        vxiClear = nSystErr

    End Function

    Function WriteMsg(ByVal InstrumentIndex As Integer, ByVal MessageToSend As String) As Integer

        'DESCRIPTION:
        '   This Routine is a pass through to the VISA layer using VB conventions to
        '   fascilitate clean Word Serial write communications to message based instruments.
        'PARAMETERS:
        '   Instrument = the name of the instrument to write
        '   MessageToSend = the string of data to be written
        'EXAMPLE:
        '   SendMessage DMMHandle&, "*TST?" '* This will run a self test on the DMM
        Dim retCount As Integer
        Dim ReturnMessage As String
        ReadBuffer = ""
        ReturnMessage = Space(256)

        Application.DoEvents()
        LastWriteMessage = MessageToSend

        nSystErr = atxml_WriteCmds(ResourceName(InstrumentIndex), MessageToSend, retCount)
        If nSystErr Then
            Echo("CICL error " & CStr(nSystErr))
            nSystErr = atxml_WriteCmds(ResourceName(InstrumentIndex), "ERR?", retCount)
            nSystErr = atxmlDF_viRead(ResourceName(InstrumentIndex), 0, ReturnMessage, 256, retCount)
        Else

        End If
        WriteMsg = nSystErr


    End Function



    Function StringToList(ByVal strng As String, ByRef List() As String, ByVal delimiter As String) As Short
        'DESCRIPTION:
        ' Procedure to convert a delimited string into a list array
        'Parameters:
        ' strng     : String to be converted.
        ' list()    : Array in which to return list of strings
        ' Delimiter : Char array of valid delimiters.

        'Returns:
        ' Number of items in list
        ' Returns -1 if number of number of elements exceeds
        ' upper bound of passed array
        Dim numels As Integer
        Dim inflag As Integer
        Dim slength As Integer
        Dim ch As Integer
        Dim mchar As String

        numels = 0
        inflag = 0

        'Clear Passed Array
        Array.Clear(List, 0, List.Length)

        'Go through parsed string a character at a time.
        slength = strng.Length
        For ch = 1 To slength
            mchar = Strings.Mid(strng, ch, 1)
            'Test for delimiter
            If InStr(delimiter, mchar) = 0 Then
                If inflag = 0 Then
                    'Test for too many arguments.
                    If numels = UBound(List) Then
                        StringToList = -1
                        Exit For
                    End If
                    numels += 1
                    inflag = -1
                End If
                'Add the character to the current argument.
                List(numels) += mchar
            Else
                'Found a delimiter.
                'Set "Not in element" flag to FALSE.
                inflag = 0
            End If
        Next ch
        StringToList = numels

    End Function

    Public Function StringToList2(ByVal sStr As String, ByVal iLower As Short, ByRef List() As String, ByVal sDelimiter As String) As Integer
        'DESCRIPTION:
        '   Procedure to convert a delimited string into a dynamic string array
        '   ReDims the array from iLower to the number of elements in string
        'Parameters:
        '   sStr       : String to be parsed.
        '   iLower     : Lower bound of target array
        '   List()    : Dynamic array in which to return list of strings
        '   sDelimiter : Delimiter string.
        'Returns:
        '   Number of items in string
        '   or 0 if string is empty

        Dim numels As Integer, i As Integer
        Dim iDelimiterLength As Integer

        iDelimiterLength = Len(sDelimiter)
        If sStr = "" Then
            StringToList2 = 0
            Exit Function
        End If

        numels = 1
        ReDim List(iLower)
        'Go through parsed string a character at a time.
        ' ie LF1-00-N01
        For i = 1 To Len(sStr)
            'Test for delimiter
            If Strings.Mid(sStr, i, iDelimiterLength) <> sDelimiter Then
                'Add the character to the current argument.
                List(iLower + numels - 1) &= Strings.Mid(sStr, i, 1)
            Else
                'Found a delimiter.
                ReDim Preserve List(iLower + numels)
                numels += 1
                i += iDelimiterLength - 1
            End If
        Next i
        StringToList2 = numels

    End Function


    Public Sub Delay(ByVal dSeconds As Double)
        'DESCRIPTION:
        '   Delays the program for a specified time
        'PARAMETERS:
        '   Seconds! = number of seconds to Delay
        'EXAMPLE:
        '           Delay 2.3

        'now.ToOAdate fractional component represents the time of day divided by 24,  where 6 A.M. is represented by 0.25.

        Dim t As Double

        t = dGetTime() ' 65000.25

        dSeconds /= SECS_IN_DAY  ' ie, 10 sec = 10/86400 = 0.0001157407
        Do
            Application.DoEvents()
        Loop While (dGetTime() - t) < dSeconds

    End Sub


    Function StripNullCharacters(ByRef Parsed As String) As String
        StripNullCharacters = ""
        'DESCRIPTION:
        '   This routine strips characters with ASCII values less than 32 from the
        '   end of a string
        'PARAMTERS:
        '   Parsed = String from which to remove null characters
        'RETURNS:
        '   A the reultant parsed string
        Dim x As Integer
        Parsed = Trim(Parsed) ' delete leading/trailing spaces
        For x = Parsed.Length To 1 Step -1
            If Asc(Strings.Mid(Parsed, x, 1)) > 32 Then
                Exit For
            End If
        Next x
        StripNullCharacters = Strings.Left(Parsed, x)
    End Function


    Sub CenterForm(ByVal Form As Form)
        '************************************************************
        '* Nomenclature   : ATS-ViperT SYSTEM SELF TEST             *
        '*    DESCRIPTION:                                          *
        '*     This Module Centers One Form With Respect To The     *
        '*     User's Screen.                                       *
        '*    EXAMPLE:                                              *
        '*     CenterForm frmAbout                               *
        '************************************************************
        Form.Top = CInt((PrimaryScreen.Bounds.Height - Form.Height) / 2)
        Form.Left = CInt((PrimaryScreen.Bounds.Width - Form.Width) / 2)

    End Sub


    Public Sub VXIPowerCheck()
        '************************************************************
        '* Nomenclature   : ATS-ViperT SYSTEM SELF TEST             *
        '*    DESCRIPTION:                                          *
        '*     This Module checks the ATS.INI file and ensures      *
        '*     the VXI power switch is on. Added Ver 1.3 4/28/98    *
        '*    EXAMPLE:                                              *
        '*     VXI28voltOn = VXIPowerCheck                          *
        '************************************************************
        Dim keyinfo As String = Space(256)
        Dim errorMessage As Integer
        Dim Answer As DialogResult

        Do
            errorMessage = GetPrivateProfileString("System Monitor", "CHASSIS_STATE", "OFF", keyinfo, 256, sATS_INI)

            If UCase(Trim(StripNullCharacters(keyinfo))) = "OFF" Then
                Answer = MsgBox("Instrument Chassis Power Switch Not Activated.", MsgBoxStyle.RetryCancel)
                If Answer = DialogResult.Cancel Then
                    Exit Do
                End If
            Else
                Exit Do
            End If
        Loop

    End Sub


    Public Sub GetSerialNumber(ByVal iSensor As Short)
        'DESCRIPTION:
        '   This routine obtains a Serial number from the user and
        '   checks the power meter table memory for that number.  If
        '   not in Power Meter memory, has the user load the data in
        '   memory.
        'PARAMETER:
        '   iSensor:   8481 - get 8481A Sensor Serial Number from Operator
        '             -8481 - get 8481D Sensor Serial Number
        '             11708 - get 11708 Attenuator Serial Number

        Dim strSensorName As String
        Dim strSN As String
        Dim strInstrumentMemTable As String = ""
        Dim sList() As String = {}
        Dim i As Short
        Dim iNumOfSensors As Short
        Dim ReturnValue As Integer

        With frmSerialNum

            CenterForm(frmSerialNum)
            If iSensor = 8481 Then
                .lblPrompt.Text = "Enter full serial number printed on the HP 8481A power sensor." & vbCrLf
                strSensorName = "HP8481A"
            ElseIf iSensor = -8481 Then
                .lblPrompt.Text = "Enter full serial number printed on the HP 8481D power sensor." & vbCrLf
                strSensorName = "HP8481D"
            ElseIf iSensor = 11708 Then
                .lblPrompt.Text = "Enter full serial number printed on the HP 11708A 30db" & vbCrLf & "Reference Attenuator."
                strSensorName = "HP11708A"
            Else
                .lblPrompt.Text = "Enter full serial number printed on the" & vbCrLf & InstrumentDescription(iSensor) & " Module."
                strSensorName = InstFileName(iSensor)
            End If

            .txtSerialNum.Text = ""
            .ShowDialog()
            strSN = Trim(.txtSerialNum.Text)
            If strSN = "" Then Exit Sub ' canceled
            ReturnValue = WritePrivateProfileString("Serial Number", strSensorName, strSN, sATS_INI)
            If InStr(strSensorName, "HP8481") = 0 Then
                Exit Sub
            End If

            strSensorName = """" & strSensorName & "_" & Strings.Right(RTrim(.txtSerialNum.Text), 4) & """"

            nSystErr = WriteMsg(RFPM, "MEM:CAT?")
            nSystErr = WaitForResponse(RFPM, 0.1)
            nSystErr = ReadMsg(RFPM, strInstrumentMemTable)
            iNumOfSensors = StringToList2(strInstrumentMemTable, 1, sList, ",")
            For i = 1 To iNumOfSensors
                If strSensorName = sList(i) Then
                    Exit Sub
                End If
            Next i

        End With
    End Sub


    '#Const defUse_ReturnDependentInstrumentString = True
#If defUse_ReturnDependentInstrumentString Then
    Public Function ReturnDependentInstrumentString(ByVal iTestIndex As Short, ByVal iDependentIndex As Short) As String
        ReturnDependentInstrumentString = ""
        '******************************************************************************************
        '***    Returns a String that describes why the Instrument could not be tested          ***
        '***    when a dependant Instrument failed in a previous module.                        ***
        '***    This is recorded in the Comments field of the FHDB.                             ***
        '***                                                                     07/09/2001     ***
        '******************************************************************************************
        Dim sFirstString As String 'Holds the First String
        Dim sLastString As String 'Holds the Last String

        sFirstString = "The " & sGuiLabel(iDependentIndex) & " failed in a previous module and it is required to perform the " & sGuiLabel(iTestIndex) & " Self Test."
        sLastString = "    The " & sGuiLabel(iTestIndex) & " could not be tested."

        Echo(sFirstString)
        Echo(sLastString)

        ReturnDependentInstrumentString = sFirstString & vbCrLf & sLastString

    End Function
#End If


    '#Const defUse_ReturnNoCommString = True
#If defUse_ReturnNoCommString Then
    Public Function ReturnNoCommString(ByVal iInstrumentNoComm As Short, ByVal iInstrumentNotTested As Short) As String
        ReturnNoCommString = ""
        '******************************************************************************************
        '***    Returns a String that indicates which Instrument could not establish            ***
        '***    communications and which Instrument could not be tested.                        ***
        '***    This is recorded in the Comments field of the FHDB.                             ***
        '***                                                                     07/10/2001     ***
        '******************************************************************************************
        Dim sFirstString As String 'Holds the First String
        Dim sLastString As String 'Holds the Last String

        sFirstString = NO_COMM & sGuiLabel(iInstrumentNoComm) & "."
        sLastString = "The " & sGuiLabel(iInstrumentNotTested) & " could not be tested."

        ReturnNoCommString = sFirstString & vbCrLf & sLastString

    End Function
#End If


    Public Function FormatResults(ByVal bStatus As Boolean, Optional ByVal sTextPassed As String = "", Optional ByVal sTestNum As String = "", Optional ByVal sMin As String = "", Optional ByVal sMax As String = "", Optional ByVal sMeas As String = "", Optional ByVal sUnit As String = "") As String
        FormatResults = ""
        '******************************************************************************************
        '***    Returns a formated String depending on arguments sent.                          ***
        '***    Formats Min/Max/Meas value field.                                               ***
        '***    Formats either a single test number or a range.                                 ***
        '***    Formats an informational Pass/Fail Text line.                                   ***
        '***                                                                     08/31/2001     ***
        '******************************************************************************************
        Dim sReturnField As String 'Holds the text as the String is built
        Dim sStatus As String 'Holds the Text value of Pass/Fail Status
        Dim iSpaces As Short 'Holds the number of spaces for formating
        Dim sMinField As String 'Holds the Min value text
        Dim sMaxField As String 'Holds the Max value text
        Dim sMeasField As String 'Holds the Measured value text

        'Define Pass/Fail Status
        If CInt(bStatus) = 0 Then
            sStatus = "FAILED"
        Else
            sStatus = "PASSED"
        End If

        If (Len(sUnit) > 0) Or (Len(sTestNum) < 1) Or (InStr(sTextPassed, "Cable Resistance Test") > 0) Then 'A Test Number was not Passed
            If Len(sUnit) > 0 Then 'It's a Min/Max/Meas String
                'Format for a Min/Max/Meas String
                sMinField = "    Min: " & EngNotate(CDbl(sMin)) & sUnit
                sMinField &= Space(24 - Len(sMinField))
                sMaxField = "Max: " & EngNotate(CDbl(sMax)) & sUnit
                sMaxField &= Space(20 - Len(sMaxField))
                sMeasField = "Measured: " & EngNotate(CDbl(sMeas)) & sUnit
                sMeasField &= Space(25 - Len(sMeasField))
                sReturnField = sMinField & sMaxField & sMeasField & sStatus
            Else                'It's not a Min/Max/Meas String and it's not a Test Number String
                sReturnField = "    " & sTextPassed
                If Len(sReturnField) < 70 Then
                    sReturnField &= Space(69 - Len(sReturnField))
                End If
                sReturnField &= sStatus
            End If
        Else            'Test Number was Passed
            If Len(sTestNum) = 10 Then 'It's only one code
                iSpaces = 69
            Else                'It's a Range
                iSpaces = 69
            End If

            'Format Text to return
            sReturnField = sTestNum & " " & sTextPassed
            If Len(sReturnField) - iSpaces < 0 Then
                sReturnField &= Space(iSpaces - Len(sReturnField))
            End If
            sReturnField &= sStatus
        End If
        If InStr(sTextPassed, "Cable Resistance Test") > 0 Then
            Echo(sTestNum & " " & sTextPassed)
            '            sReturnField = sTestNum & " " & sTextPassed & vbCrLf & Trim(sReturnField) 'Trim spaces to allow placement on one line.
        End If

        'Return Formated Results
        FormatResults = sReturnField

    End Function

    Public Sub EchoTitle(ByVal sTitle As String, Optional ByVal bHeader As Boolean = False)
        '******************************************************************************************
        '***        Builds the Test Title Header. Blank Line, Row of Asterisks, 3 Asterisks--   ***
        '***        Centered Title Text Passed--3 Asterisks, Row of Asterisks, Blank Line.      ***
        '***                                                                     09/05/2001     ***
        '******************************************************************************************
        Dim iIndex As Integer 'Index of the character position
        Dim iLineLen As Integer 'Length of the Title Line
        Dim sLineOut As String = "" 'Final Text Line Out
        Dim iStart As Integer 'Index of the First Title character
        Dim iEnd As Integer 'Index of the Last Title character

        If bHeader Then
            Echo(vbCrLf)
        Else
            First_Last_Line(True)
        End If


        iLineLen = Len(sTitle) 'Find the Length of the Title Text
        iStart = CInt(38 - (iLineLen / 2)) 'Find the Starting Index for the Title Text
        iEnd = iStart + iLineLen 'Find the Ending Index for the Title Text

        For iIndex = 1 To 76 Step 1

            If iIndex >= iStart And iIndex <= iEnd Then
                'Print Title Text Character
                sLineOut &= Strings.Mid(sTitle, (iIndex + 1 - iStart), 1)
            Else

                Select Case iIndex
                    'If Index is one of these postions, character is an Asterisk
                    Case 1, 2, 3, 74, 75, 76
                        If Not bHeader Then
                            sLineOut &= "*"
                        Else
                            sLineOut &= " " 'Else Character is a Space
                        End If

                    Case Else
                        sLineOut &= " " 'Else Character is a Space
                End Select
            End If

        Next

        Echo(sLineOut) 'Output text line to the Log
        If Not bHeader Then First_Last_Line(False) 'Print the Last line of Asterisks, Blank line after

    End Sub


    Public Sub First_Last_Line(ByVal bFirst As Boolean)
        '******************************************************************************************
        '***        Builds a row of 75 Asterisks and either a Blank line before or after        ***
        '***        for the Test Title Header.                                                  ***
        '***                                                                     09/05/2001     ***
        '******************************************************************************************
        Dim iIndex As Short 'Index for the Counter
        Dim sLineOut As String = "" 'Final Text Line Out

        For iIndex = 1 To 75 Step 1
            sLineOut &= "*"
        Next

        Echo(sLineOut)
        If Not bFirst Then Echo("")

    End Sub


    Public Sub FormatResultLine(ByVal vInstrumentID_String As String, Optional ByVal bResult As Boolean = False)
        '******************************************************************************************
        '*** Formats the Final result text of the individual test. If the ID is an Index, the   ***
        '*** Instrument Name is used. If it's not numeric, string value is used.                ***
        '***                                                                                    ***
        '*** Example:(Instrument Name) Test........................... PASSED                   ***
        '*** Example: LF1-01-003 S101 Open Test                        PASSED                   ***
        '******************************************************************************************
        Dim iLineLen As Integer 'Length of the Text Line
        Dim sLineOut As String 'Final Text Line Out
        Dim sStatus As String 'PASSED/FAILED Identifier
        Dim sText As String 'Instrument Description
        Dim bNoBlankLine As Boolean 'Blank Line Switch

        If IsNumeric(vInstrumentID_String) Then
            bNoBlankLine = False
            sText = InstrumentDescription(CInt(vInstrumentID_String)) & " Test"
        Else
            bNoBlankLine = True
            sText = vInstrumentID_String
        End If

        iLineLen = Len(sText) 'Find out the Length of the Text
        If bResult = False Then 'Define Pass/Fail Status
            sStatus = "FAILED"
        Else
            sStatus = "PASSED" 'PASSED/FAILED = 6 characters
        End If

        If iLineLen < 70 Then
            If bNoBlankLine Then
                sLineOut = sText & StrDup(69 - iLineLen, " ")
            ElseIf iLineLen < 69 Then
                sLineOut = sText & StrDup(68 - iLineLen, ".") & " "
            Else
                sLineOut = sText & " "
            End If
        Else            '  More than one line, fix this to fill out to 70 characters
            sLineOut = sText
        End If

        If Not bNoBlankLine Then Echo("")
        Echo(sLineOut & sStatus)
        If Not bNoBlankLine Then Echo("")

    End Sub


    Public Sub CenterTextBox(ByRef sText1 As String, Optional ByRef sText2 As String = "", Optional ByRef sText3 As String = "", Optional ByRef sText4 As String = "")
        '******************************************************************************************
        '***    Centers Text box in 75 character field. Uses the longest of the two             ***
        '***    Strings as the Text Box Width. If only one String is passed, the Box is put     ***
        '***    around a single line.                                                           ***
        '******************************************************************************************
        Dim chrIndex As Integer 'Index for loop
        Dim sLineOut As String = "" 'Final Text Line Out
        Dim sInputText As String = "" 'Text line center
        Dim iLineLen1 As Integer 'Length of Text Line 1
        Dim iStart1 As Integer 'Starting Index of Text1
        Dim iEnd1 As Integer 'Ending Index of Text1
        Dim iLineLen2 As Integer 'Length of Text Line 2
        Dim iStart2 As Integer 'Starting Index of Text2
        Dim iEnd2 As Integer 'Ending Index of Text2
        Dim iTotalLines As Integer 'Total Lines in Text Box
        Dim iLineLen As Integer 'Total Length of asterisks Line
        Dim iLineNumber As Integer 'Line number of Text Box
        Dim iStart As Integer 'Starting Index of asterisks
        Dim iEnd As Integer 'Ending Index of asterisks
        Dim iStartText As Integer 'Starting Index of Text
        Dim iendText As Integer 'Ending Index of Text

        'added bb
        Dim iLineLen3 As Integer 'Length of Text Line 3
        Dim iStart3 As Integer 'Starting Index of Text3
        Dim iEnd3 As Integer 'Ending Index of Text3
        Dim iLineLen4 As Integer 'Length of Text Line 4
        Dim iStart4 As Integer 'Starting Index of Text4
        Dim iEnd4 As Integer 'Ending Index of Text4
        Dim i As Integer

        iTotalLines = 2 'Initialize Total Number of Lines

        'If Text1 is Passed, Find Start and End Points
        If Len(sText1) > 0 Then
            iLineLen1 = Len(sText1) '+ 4        'Find Line Length1
            iStart1 = CInt(38 - (iLineLen1 / 2)) 'Find Starting Point1
            iEnd1 = iStart1 + iLineLen1 'Find Ending Point1
            iTotalLines = iTotalLines + 1 'Add 1 to Total Number of Lines
        End If

        'If Text1 is Passed, Find Start and End Points
        If Len(sText2) > 0 Then
            iLineLen2 = Len(sText2) ' + 4       'Find Line Length2
            iStart2 = CInt(38 - (iLineLen2 / 2)) 'Find Starting Point2
            iEnd2 = iStart2 + iLineLen2 'Find Ending Point2
            iTotalLines = iTotalLines + 1 'Add 1 to Total Number of Lines
        End If

        If Len(sText3) > 0 Then
            iLineLen3 = Len(sText3) ' + 4       'Find Line Length3
            iStart3 = CInt(38 - (iLineLen3 / 2)) 'Find Starting Point3
            iEnd3 = iStart3 + iLineLen3 'Find Ending Point3
            iTotalLines = iTotalLines + 1 'Add 1 to Total Number of Lines
        End If

        If Len(sText4) > 0 Then
            iLineLen4 = Len(sText4) ' + 4       'Find Line Length4
            iStart4 = CInt(38 - (iLineLen4 / 2)) 'Find Starting Point4
            iEnd4 = iStart4 + iLineLen4 'Find Ending Point4
            iTotalLines = iTotalLines + 1 'Add 1 to Total Number of Lines
        End If


        'Find the Longest of the four lines and define variables
        Dim LongestLine As Integer
        iLineLen = iLineLen1
        LongestLine = 1
        iStart = iStart1 - 2
        iEnd = iEnd1 + 1

        For i = 2 To 4
            If i = 2 And iLineLen2 > iLineLen Then
                LongestLine = 2
                iLineLen = iLineLen2
                iStart = iStart2 - 2
                iEnd = iEnd2 + 1
            End If
            If i = 3 And iLineLen3 > iLineLen Then
                LongestLine = 3
                iLineLen = iLineLen3
                iStart = iStart3 - 2
                iEnd = iEnd3 + 1
            End If
            If i = 4 And iLineLen4 > iLineLen Then
                LongestLine = 4
                iLineLen = iLineLen4
                iStart = iStart4 - 2
                iEnd = iEnd4 + 1
            End If
        Next

        ''If iLineLen1 > iLineLen2 Then
        ''    iLineLen = iLineLen1 'Add correction to allow for extra characters
        ''    iStart = iStart1 - 2
        ''    iEnd = iEnd1 + 1
        ''Else
        ''    iLineLen = iLineLen2
        ''    iStart = iStart2 - 2
        ''    iEnd = iEnd2 + 1
        ''End If


        For iLineNumber = 1 To iTotalLines 'Loop through lines
            'Assign variables in relationship to line number
            If iLineNumber = 2 Then
                sInputText = sText1
                iStartText = iStart1
                iendText = iEnd1 - 1
            ElseIf iLineNumber = 3 Then
                sInputText = sText2
                iStartText = iStart2
                iendText = iEnd2 - 1
            ElseIf iLineNumber = 4 Then
                sInputText = sText3
                iStartText = iStart3
                iendText = iEnd3 - 1
            ElseIf iLineNumber = 5 Then
                sInputText = sText4
                iStartText = iStart4
                iendText = iEnd4 - 1
            End If

            For chrIndex = 1 To 76 Step 1 'Loop through Text Field
                If chrIndex >= iStart And chrIndex <= iEnd Then
                    'Print centered line of asterisks the length of iLineLen
                    If iLineNumber = 1 Or iLineNumber = iTotalLines Then
                        sLineOut = sLineOut & "*" 'Add an asterisk to the output line
                    Else
                        If chrIndex = iStart Or chrIndex = iEnd Then
                            sLineOut = sLineOut & "*" 'Add an asterisk to the output line
                        ElseIf chrIndex >= iStartText And chrIndex <= iendText Then
                            'Print Character to the output line
                            sLineOut = sLineOut & Mid(sInputText, chrIndex + 1 - iStartText, 1)
                        Else
                            sLineOut = sLineOut & " " 'Add a Space to the output line
                        End If
                    End If
                Else
                    If chrIndex = iStart Or chrIndex = iEnd Then
                        sLineOut = sLineOut & "*" 'Add an asterisk to the output line
                    Else
                        sLineOut = sLineOut & " " 'Add a Space to the output line
                    End If
                End If
            Next
            Echo(sLineOut)
            sLineOut = ""
        Next

    End Sub



    Function fnSAIFinstalled(ByVal Expect As Boolean) As Boolean

        ReadReceiverSwitches()
        If Expect = SAIFinstalled Then ' if the switch is what we expected then exit
            fnSAIFinstalled = SAIFinstalled ' true or false
            Exit Function
        End If

        'If the switch was not what was expected then
        '   delay 5 seconds for the receiver switch to be recognized and test it again
        frmSTest.Cursor = Cursors.WaitCursor
        Application.DoEvents()

        Delay(5)
        frmSTest.Cursor = Cursors.Default 'and some various me.Cursor / current.cursor 
        Application.DoEvents()

        ReadReceiverSwitches()
        fnSAIFinstalled = SAIFinstalled ' true or false

    End Function


    Sub ReadReceiverSwitches()

        Dim status As Short
        Dim ActionByteValue As Integer
        Dim ActChassisAddress As Integer
        Dim ActVolt28OK As Integer
        Dim ActProbeEvent As Integer
        Dim ActReceiverEvent As Integer

        '***Get Status Byte***
        nSystErr = viReadSTB(SupplyHandle(1), status)
        Application.DoEvents()

        '***Evaluate Status Byte***
        If ((status And &HF0) = &HB0) Then
            '-------------------------------------------------------------------------------
            '- Action Status Byte ----------------------------------------------------------
            '-        B7 |  B6 |        B5 |           B4 | B3 |     B2 |     B1 |      B0 -
            '- Data Dump | RQS | Ret Error | Module Fault | A3 | A2/28V | A1/PRB | A0/RCVR -
            '-------------------------------------------------------------------------------
            'Data Dump 1000 xxxx
            'Query Failed 0010 xxxx
            'Mod Failed 0001 ADDR
            'Action Byte 1011 xABC where A=28V, B=PRB, C=RCVR
            'ICPU Response 0011 xxxx
            'Module Response 0100 ADDR
            'Generally 80H = Data Dump
            '          BXH = Action Byte
            '-------------------------------------------------------------------------------
            ActionByteValue = CInt(status)
            ActChassisAddress = ActionByteValue And 8
            ActVolt28OK = ActionByteValue And 4
            ActProbeEvent = ActionByteValue And 2
            ActReceiverEvent = ActionByteValue And 1
            ''        'Check For A Probe Event
            '        If ActProbeEvent Then
            '            MsgBox ("Probe Button is off")
            '        Else
            '            MsgBox ("Probe Button is on")
            '        End If

            '        'Check ITA/SAIF Receiver Switch Power Switch
            If ActReceiverEvent = 1 Then 'RCVR Handle Open
                SAIFinstalled = False

            Else                'RCVR Handle Closed
                SAIFinstalled = True
            End If

            If ActProbeEvent = 2 Then ' Probe button not Pressed
                AprobeButton = False
            Else
                AprobeButton = True
            End If
        End If
        Application.DoEvents()

    End Sub


    Sub IncStepPassed()

        StepPassedCount += 1
        frmSTest.lblStepPassCount.Text = CStr(StepPassedCount)

        If PauseTest = True Then ' wait here until "Continue" or "Abort" is pressed
            Do
                Application.DoEvents()
            Loop Until PauseTest = False
        End If

    End Sub

    Sub IncStepFailed()

        StepFailedCount += 1
        frmSTest.lblStepFailCount.Text = CStr(StepFailedCount)

        If PauseTest = True Then ' wait here until "Continue" or "Abort" is pressed
            Do
                Application.DoEvents()
            Loop Until PauseTest = False
        End If


    End Sub

    Sub SetVEOPower(ByVal OnOff As Integer)

        Dim CloseRelay As Integer
        Dim SetSlave As Integer
        Dim SenseRemote As Integer
        Dim CurrentConstant As Integer
        Dim OpenRelay As Integer
        Dim SetMaster As Integer
        Dim SenseLocal As Integer
        Dim CurrentLimit As Integer
        Dim x As DialogResult

        'Define command options
        CloseRelay = 0
        SetSlave = 0
        SenseRemote = 0
        CurrentConstant = 0

        OpenRelay = -1
        SetMaster = -1
        SenseLocal = -1
        CurrentLimit = -1 '

        If OnOff = True Then ' turn power on

            ExternalPower = False
            If ExternalPower = False Then
                x = MsgBox("Verify that everything is ready to power the VEO2 unit." & vbCrLf & vbCrLf & "Are you ready to turn on VEO2 power?", MsgBoxStyle.YesNo)
                If x <> DialogResult.Yes Then
                    VEO2PowerOn = False
                    Exit Sub
                End If
                Echo("**** Turning on VEO2 Power ****")

                ' reset supply
                CommandSupplyReset(1) ' Reset master supply
                CommandSupplyReset(2) ' Reset slave supply
                CommandSupplyReset(3) ' Reset +15v supply
                Delay(1)

                'Turn on 15v
                CommandSetOptions(3, CloseRelay, SetMaster, SenseLocal, CurrentLimit) ' close output relay
                CommandSetCurrent(3, 5) 'set current limit to 5 Amp
                CommandSetVoltage(3, 15) 'set voltage to 15
                Delay(1)

                'setup slave
                CommandSetOptions(2, 1, SetSlave, 1, 1) ' set slave mode
                Delay(0.3)
                CommandSetOptions(2, CloseRelay, 1, 1, 1) ' close output relay
                Delay(0.3)

                'Turn on master (and slave)
                CommandSetOptions(1, 1, SetMaster, SenseLocal, CurrentLimit)
                Delay(0.3)
                CommandSetOptions(1, CloseRelay, 1, 1, 1) ' close output relay
                Delay(0.3)
                CommandSetCurrent(1, 5) 'set current limit to 5 Amp
                CommandSetVoltage(1, 28) 'set voltage to 28
                Delay(1)
                VEO2PowerOn = True

            End If

        Else            ' turn power off

            Echo("**** Turning off VEO2 Power ****")

            'disconnect two supplies
            CommandSetVoltage(1, 0) 'set voltage to 0
            CommandSetCurrent(1, 0) 'set current limit to 0 amp
            CommandSetVoltage(3, 0) 'set voltage to 0
            CommandSetCurrent(3, 0) 'set current limit to 0 amp
            CommandSetOptions(1, OpenRelay, SetMaster, SenseLocal, CurrentLimit)
            CommandSetOptions(2, OpenRelay, SetMaster, SenseLocal, CurrentLimit)
            CommandSetOptions(3, OpenRelay, SetMaster, SenseLocal, CurrentLimit)
            Delay(1)
            VEO2PowerOn = False
            Echo("- VEO2 Power is OFF.")
        End If

    End Sub

    Function FnFileExists(ByVal pathfile As String) As Integer
        '************************************************************
        '* Nomenclature   : ATS-ViperT SYSTEM SELF TEST             *
        '*    DESCRIPTION:                                          *
        '*     This Module Checks To See If A Disk File Exists      *
        '*    EXAMPLE:                                              *
        '*     IsFileThere = FnFileExists("C:\ISFILE.EX")           *
        '*    RETURNS:                                              *
        '*     TRUE if File is present.                             *
        '*     False if File is not present.                        *
        '************************************************************
        Dim x As Integer
        x = FreeFile()

        On Error Resume Next
        FileOpen(x, CStr(pathfile), OpenMode.Input)
        If (Err.Number = 0) Then
            FnFileExists = True
        Else
            FnFileExists = False
            Err.Clear()
        End If
        FileClose(x)

    End Function



    Sub BumpProgress(ByVal count As Short)

        'This routine is used to print time on screen as
        ' count = 0123456789012345678901234567890
        '        "1-----2-----3-----4-----5"
        '
        ' Move cursor to end of text - 2
        frmSTest.DetailsText.SelectionStart = Len(frmSTest.DetailsText.Text) - 1 ' move insertion point to end of text
        frmSTest.DetailsText.SelectionLength = 0
        If (count Mod 6) = 0 Then
            frmSTest.DetailsText.SelectedText = CStr(count \ 6)
        Else
            frmSTest.DetailsText.SelectedText = "-"
        End If
        Application.DoEvents()

    End Sub


    Function GetSN(ByVal sOpt As String) As String
        GetSN = ""

        '  [Serial Number]
        '  HP8481A = MY41098082
        '  HP8481D = MY41093897
        '  HP11708A = 29442

        'DESCRIPTION:
        '   This routine gets a serial number
        '   by reading the ATS.INI file located in the C:\users\public\public documents directory
        '

        Dim lpBuffer As String = Space(256)
        Dim FileError As Integer
        Dim ReadBuffer As String = Space(256)
        Dim sKey As String
        sKey = sOpt

        FileError = GetPrivateProfileString("Serial Number", sKey, "", lpBuffer, 256, sATS_INI)
        ReadBuffer = StripNullCharacters(lpBuffer)
        GetSN = Trim(ReadBuffer)
        If GetSN = "" Then
            GetSN = "No Serial Number found!"
        End If

    End Function

    Function GatherIniFileInformation(ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpDefault As String) As String
        GatherIniFileInformation = ""
        '************************************************************
        '* Nomenclature   : ATS-ViperT SYSTEM SELF TEST             *
        '*                  System Monitor  [SystemStartUp]         *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     Finds a value on in the ATS.ini File                 *
        '*    PARAMETERS:                                           *
        '*     lpApplicationName -[Application] in ATS.ini File     *
        '*     lpKeyName - KEYNAME= in ATS.ini File                 *
        '*     lpDefault - Default value to return if not found     *
        '*    RETURNS                                               *
        '*     String containing the key value or the lpDefault     *
        '*    EXAMPLE:                                              *
        '*     FilePath = GatherIniFileInformation("Heading", ...   *
        '*      ..."MY_FILE", "")                                   *
        '************************************************************
        'Reqiires (3) Windows Api Functions
        'Declare Function GetPrivateProfileString Lib "kernel32" Alias "GetPrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As Any, ByVal lpDefault As String, ByVal lpReturnedString As String, ByVal nSize As Long, ByVal lpFileName As String) As Long
        'Declare Function WritePrivateProfileString Lib "kernel32" Alias "WritePrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As Any, ByVal lpString As Any, ByVal lpFileName As String) As Long

        Dim lpReturnedString As String = Space(256)
        Dim nSize As Integer = 256 'Return Buffer Size
        Dim ReturnValue As Integer 'Return Value Buffer
        Dim FileNameInfo As String 'Formatted Return String
        Dim lpString As String 'String to write to INI File

        'Clear String Buffer
        lpReturnedString = Space(256)
        nSize = 256
        lpReturnedString = Space(256)
        FileNameInfo = ""
        'Find File Locations
        ReturnValue = GetPrivateProfileString(lpApplicationName, lpKeyName, lpDefault, lpReturnedString, nSize, sATS_INI)
        FileNameInfo = Trim(lpReturnedString)
        FileNameInfo = Strings.Mid(FileNameInfo, 1, Len(FileNameInfo) - 1)
        'If File Locations Missing, then create empty keys
        If FileNameInfo = (lpDefault & Chr(0)) Or (FileNameInfo = lpDefault) Then
            lpString = Trim(lpDefault)
            ReturnValue = WritePrivateProfileString(lpApplicationName, lpKeyName, lpString, sATS_INI)
        End If

        'Return Information In INI File
        GatherIniFileInformation = FileNameInfo

    End Function


    Public Function LoadPicture(ByVal file_name As String) As Image
        If file_name Is Nothing Or file_name.Length = 0 Then Return New Bitmap(1, 1)
        Dim src As New Bitmap(file_name)
        Dim dst As New Bitmap(src.Width, src.Height, Imaging.PixelFormat.Format24bppRgb)
        Graphics.FromImage(dst).DrawImage(src, 0, 0, src.Width, src.Height)
        src.Dispose()
        Return dst
    End Function

    Public Sub InstallSPCables()
        Dim S As String
        S = "Install SP2, SP3, SP4, W206, W209 and W210 as follows:" & vbCrLf
        S &= "1. Connect adapter SP2 (93006H4240) to the CIC J10 (Serial) connector." & vbCrLf
        S &= "2. Connect adapter SP3 (93006H6054) to the CIC J5 (RS232) connector." & vbCrLf
        S &= "3. Connect adapter SP4 (93006H6055) to the CIC J6 (RS422) connector." & vbCrLf
        S &= "4. Connect cable W206 (Red) cable(93006H6861) from CIC J15 (GIGABIT) to CIC J16 (GIGABIT)." & vbCrLf
        S &= "5. Connect cable W210 (Green) cable from CIC J18 (LAN) to J19 (GIGABIT)." & vbCrLf
        S &= "6. Connect cable W209 (93006H4241) from CIC J21 (CAN1) to CIC J22 (CAN2)." & vbCrLf
        DisplaySetup(S, "ST-SPALL-1.jpg", 6, True, 3, 3)
    End Sub

    Public Sub InstallW204Cable()
        Dim S As String
        S = "Install cable W204 (93006H6048) as follows:" & vbCrLf
        S &= "1. Connect cable W204-P1 onto the secondary chassis S/R I/O connector. " & vbCrLf
        S &= "2. Connect cable W204-P2 onto the SAIF J5 connector. " & vbCrLf
        DisplaySetup(S, "ST-W204-1.jpg", 2, True, 2, 3)
    End Sub

    Public Sub InstallW201Cable()
        Dim S As String
        S = "Remove all cables/adapters from the SAIF." & vbCrLf
        S &= "Connect cable W201 (93006H6045) to SAIF as follows:" & vbCrLf
        S &= "1.  P1  to SAIF J1 connector." & vbCrLf
        S &= "2.  P4  to SAIF FG OUTPUT." & vbCrLf
        S &= "3.  P5  to SAIF FG TRIG/PLL IN." & vbCrLf
        S &= "4.  P6  to SAIF FG SYNC OUT." & vbCrLf
        S &= "5.  P7  to SAIF FG CLOCK IN." & vbCrLf
        S &= "6.  P8  to SAIF FG PM IN." & vbCrLf
        S &= "7.  P9  to SAIF ARB OUTPUT/7." & vbCrLf
        S &= "8.  P10 to SAIF ARB OUTPUT." & vbCrLf
        S &= "9.  P11 to SAIF ARB START ARM IN." & vbCrLf
        S &= "10. P12 to SAIF ARB MARKER OUT." & vbCrLf
        S &= "11. P13 to SAIF ARB REF/SMPL IN." & vbCrLf
        S &= "12. P14 to SAIF ARB STOP TRIG/FSK/GATE IN." & vbCrLf
        S &= "13. P15 to SAIF SCOPE INPUT 1." & vbCrLf
        S &= "14. P16 to SAIF SCOPE INPUT 2." & vbCrLf
        S &= "15. P17 to SAIF SCOPE TRIG IN." & vbCrLf
        S &= "16. P18 to SAIF SCOPE COMP CAL." & vbCrLf
        S &= "17. P19 to SAIF C/T INPUT 1." & vbCrLf
        S &= "18. P20 to SAIF C/T INPUT 2." & vbCrLf
        S &= "19. P21 to SAIF C/T ARM IN." & vbCrLf
        S &= "20. P22 to SAIF DMM TRIG IN." & vbCrLf
        S &= "21. P23 to SAIF DMM VCOMP OUT."
        DisplaySetup(S, "ST-W201-1.jpg", 21, True, 1, 3)
    End Sub

    Public Sub InstallW201CablePartial()
        Dim S As String
        S = "Remove all cables/adapters from the SAIF." & vbCrLf
        S &= "Install cable W201 to the SAIF as follows:" & vbCrLf
        S &= " 1. P1  to SAIF J1 connector." & vbCrLf
        S &= " 2. P10 to SAIF ARB OUTPUT." & vbCrLf
        S &= " 3. P12 to SAIF ARB MARKER OUT." & vbCrLf
        S &= " 4. P15 to SAIF SCOPE INPUT 1." & vbCrLf
        S &= " 5. P19 to SAIF C/T INPUT 1." & vbCrLf
        S &= " 6. P20 to SAIF C/T INPUT 2." & vbCrLf
        S &= " 7. P21 to SAIF C/T ARM/IN."
        DisplaySetup(S, "ST-W201-1.jpg", 7)
    End Sub

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