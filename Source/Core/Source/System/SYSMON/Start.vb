'Option Strict Off
'Option Explicit On

Imports System
Imports System.Windows.Forms
Imports System.Text
Imports System.Drawing
Imports System.IO
Imports System.Math
Imports System.Diagnostics
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Imports NationalInstruments.VisaNS
Imports System.Threading
Imports System.ServiceProcess
Imports Microsoft.Win32

Public Module SystemStartUp


    '=========================================================
    '**************************************************************
    '**************************************************************
    '**                                                          **
    '** Nomenclature  : System Monitor  [SystemStartUp]          **
    '** Purpose       : This module handles the system startup   **
    '**                 procedures and events for the VIPERT.      **
    '**                 startup application.                     **
    '** Module Begins Execution In  --Sub:StartUpSystem--        **
    '**************************************************************
    '**************************************************************
    '-----------------Global Variables------------------------------------
    Public LogFile As String 'Holds the Path and File Name of the system Log
    Public UserTimeout As Short 'The countdown for the checklist to be showing after startup is complete
    Public EchoText As String 'Holds text to be written to the system log in Sub Echo
    Public SystemLogFilePath As String 'Path and File Name of the System Log Application
    Public VIPERTMenuFilePath As String 'Path and File Name of thr Menu Application
    Public ConfTestFilePath As String 'Path and File Name of the System Confidence Test Application
    Public UserName As String = System.Environment.UserName 'The System Log-In Name of the current user
    Public TestIndex As Short 'This is the current startup test being performed
    Public NextCalDate As String 'Holds the next callibration date
    
    Dim StartUpFailFlag As Short 'This flag is set to true if the startup fails critical system tests
    
    Dim CtestFailed As Short 'This Flag Determines that CTEST has PASSED or FAILED and is used to Launch Self-Test (or NOT)
    Public nTenFlag As Boolean 'This flag is for conditions that arise in -10 startup condition
    Dim CurrentStartupItem As Integer = 0
    Public StartupTestIndex As Integer = 0
    Public FailureMessage As String
    Public ShortStartFlag As Boolean = False
    'Launch-Executables Declares

    Structure STARTUPINFO
        Dim cb As Integer
        Dim lpReserved As String
        Dim lpDesktop As String
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

    Structure PROCESS_INFORMATION
        Dim hProcess As Integer
        Dim hThread As Integer
        Dim dwprocessID As Integer
        Dim dwThreadId As Integer
    End Structure
    Private Declare Function CreateProcessA Lib "kernel32" (ByVal lpApplicationName As String, ByVal lpCommandLine As String, ByVal lpProcessAttributes As Integer, ByVal lpThreadAttributes As Integer, ByVal bInheritHandles As Integer, ByVal dwCreationFlags As Integer, ByVal lpEnvironment As Integer, ByVal lpCurrentDirectory As String, ByRef lpStartupInfo As STARTUPINFO, ByRef lpProcessInformation As PROCESS_INFORMATION) As Integer
    Private Declare Function WaitForSingleObject Lib "kernel32" (ByVal hHandle As Integer, ByVal dwMilliseconds As Integer) As Integer
    Private Declare Function CloseHandle Lib "kernel32" (ByVal hObject As Integer) As Integer
    Private Declare Function GetExitCodeProcess Lib "kernel32" (ByVal hProcess As Integer, ByRef lpExitCode As Integer) As Integer
    Private Const NORMAL_PRIORITY_CLASS As Short = &H20
    Private Const STARTF_USESHOWWINDOW As Short = &H1
    Private Const SW_HIDE As Short = 0
    Private Const INFINITE As Short = -1

    Public Const MXI2 As Short = 1
    Public Const VCC As Short = 2

    Function CheckCalDates() As String
        CheckCalDates = ""

        Dim NewVal As String
        
        Dim Today As Integer
        Dim FailureMessage As String

        NewVal = GatherIniFileInformation("Calibration", "SYSTEM_EFFECTIVE", "")
        If NewVal = "" Then
            FailureMessage = "Invalid Date"
            GoTo EndOfFunction
        End If

        'Find Next Cal Date
        NextCalDate = VB6.Format(NewVal, "dd-mmm-yy")

        'Compare Date to today
        Today = DateSerial(Year(Now), Month(Now), DateAndTime.Day(Now)).ToOADate
        If CLng(NewVal) < Today Then
            FailureMessage = "Unknown Date"
            NextCalDate = "(Unknown Date)"
        End If

        'Update Checklist Label
        frmCheckList.InstrumentLabel(8).Text = "System Callibration Date: " & NextCalDate
        frmCheckList.Refresh()

EndOfFunction:
        'Return Function Value
        CheckCalDates = FailureMessage

    End Function



    Function GatherIniFileInformation(ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpDefault As String) As String
        GatherIniFileInformation = ""
        '************************************************************
        '************************************************************
        '* Nomenclature   : System Monitor  [SystemStartUp]         *
        '*    DESCRIPTION:                                          *
        '*     Finds a value on in the VIPERT.INI File                *
        '*    PARAMETERS:                                           *
        '*     lpApplicationName$ -[Application] in VIPERT.INI File   *
        '*     lpKeyName$ - KEYNAME= in VIPERT.INI File               *
        '*     lpDefault$ - Default value to return if not found    *
        '*    RETURNS                                               *
        '*     String containing the key value or the lpDefault     *
        '*    EXAMPLE:                                              *
        '*     FilePath$ = GatherIniFileInformation("Heading", ...  *
        '*      ..."MY_FILE", "")                                   *
        '************************************************************

        Dim lpReturnedString As New StringBuilder(255) 'Return Buffer
        Dim nSize As Integer 'Return Buffer Size
        Dim lpFileName As String 'INI File Key Name "Key=?"
        Dim ReturnValue As Integer 'Return Value Buffer
        Dim FileNameInfo As String 'Formatted Return String
        Dim lpString As String 'String to write to INI File

        lpFileName = Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\ATS\", "IniFilePath", Nothing)
        nSize = 255
        FileNameInfo = ""
        'Find File Locations
        ReturnValue = GetPrivateProfileString(lpApplicationName, lpKeyName, lpDefault, lpReturnedString, nSize, lpFileName)
        FileNameInfo = Trim(lpReturnedString.ToString())
        'If File Locations Missing, then create empty keys
        If FileNameInfo = lpDefault + Convert.ToString(Chr(0)) Or FileNameInfo = lpDefault Then
            lpString = Trim(lpDefault)
            ReturnValue = WritePrivateProfileString(lpApplicationName, lpKeyName, lpString, lpFileName)
        End If

        'Return Information In INI File
        GatherIniFileInformation = FileNameInfo

    End Function




    Function GatherFactoryDefaults(ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpDefault As String) As String
        GatherFactoryDefaults = ""
        '************************************************************
        '************************************************************
        '* Nomenclature   : System Monitor  [SystemStartUp]         *
        '*    DESCRIPTION:                                          *
        '*     Finds a value on in the VIPERT.INI File                *
        '*    PARAMETERS:                                           *
        '*     lpApplicationName$ -[Application] in VIPERT.INI File   *
        '*     lpKeyName$ - KEYNAME= in VIPERT.INI File               *
        '*     lpDefault$ - Default value to return if not found    *
        '*    RETURNS                                               *
        '*     String containing the key value or the lpDefault     *
        '*    EXAMPLE:                                              *
        '*     FilePath$ = GatherIniFileInformation("Heading", ...  *
        '*      ..."MY_FILE", "")                                   *
        '************************************************************
        
        Dim lpReturnedString As New StringBuilder(256) 'Return Buffer
        Dim nSize As Integer 'Return Buffer Size
        Dim lpFileName As String 'INI File Key Name "Key=?"
        Dim ReturnValue As Integer 'Return Value Buffer
        Dim FileNameInfo As String 'Formatted Return String
        Dim lpString As String 'String to write to INI File

        lpFileName = Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\ATS\", "FactoryDefaultsIniFilePath", Nothing)

        nSize = 255

        FileNameInfo = ""
        'Find File Locations
        ReturnValue = GetPrivateProfileString(lpApplicationName, lpKeyName, lpDefault, lpReturnedString, nSize, lpFileName)
        FileNameInfo = Trim(lpReturnedString.ToString())
        FileNameInfo = Mid(FileNameInfo, 1, Len(FileNameInfo) - 1)
        'If File Locations Missing, then create empty keys
        If FileNameInfo = lpDefault + Convert.ToString(Chr(0)) Or FileNameInfo = lpDefault Then
            lpString = Trim(lpDefault)
            ReturnValue = WritePrivateProfileString(lpApplicationName, lpKeyName, lpString, lpFileName)
        End If

        'Return Information In INI File
        GatherFactoryDefaults = FileNameInfo

    End Function





    Sub CheckUserName()
        '************************************************************
        '************************************************************
        '* Nomenclature   : System Monitor  [SystemStartUp]         *
        '*    DESCRIPTION:                                          *
        '*     Checks the System Login Name and "grants" or "denies"*
        '*     some System Monitor functionality the user           *
        '*    EXAMPLE:                                              *
        '*      CheckUserName                                       *
        '************************************************************

        Dim Frame As Short 'Index of frames to "grant"/"refuse" access
        Dim ChasSlot As Short

        If IsElevated(System.Environment.UserName) Or DebugDwh Then

            For Frame = 1 To 7
                frmSysMon.fraMaintenance(Frame).Visible = True
            Next Frame

            frmSysMon.cmdSaveThresholds(1).Visible = True
            frmSysMon.cmdSaveThresholds(2).Visible = True
            frmSysMon.cmdResetThresholds(1).Visible = True
            frmSysMon.cmdResetThresholds(2).Visible = True

            For ChasSlot = 0 To 25
                'frmSysMon.cwsSlotRise(ChasSlot).Pointers(1).mode = CWUIControlsLib.CWPointerModes.cwPointerModeControl
            Next ChasSlot

            frmSysMon.cmdFixEthernet.Visible = True
            frmSysMon.lblAccount.Visible = False
        Else
            For ChasSlot = 0 To 25
                'frmSysMon.cwsSlotRise(ChasSlot).Pointers(1).mode = CWUIControlsLib.CWPointerModes.cwPointerModeIndicator
            Next ChasSlot
        End If

    End Sub





    Function ChassisBackplaneSupplyCheck() As String
        ChassisBackplaneSupplyCheck = ""
        '************************************************************
        '************************************************************
        '* Nomenclature   : System Monitor  [SystemStartUp]         *
        '*    DESCRIPTION:                                          *
        '*     Checks to make sure that there are no hazardous      *
        '*     conditions thst should prevent the FPU from starting *
        '*    RETURNS:                                              *
        '*     Error message describing the failure or a NULL       *
        '*    EXAMPLE:                                              *
        '*      FpuError$ = ChassisBackplaneSupplyCheck             *
        '************************************************************
        Dim ReadTimeout As Short 'Number of times to try to get a valig GPIB Handle
        Dim Buffer As New StringBuilder(Space(255), 255) 'Data Dump Buffer
        Dim Status As Short 'Status Byte
        Dim retCount As Integer 'Size of returned data dump
        Dim Data As String 'Data Dump String of Bytes describing system events and properties
        Dim FailureMessage As String 'Description of failure
        Dim StartChassis As ChassisData 'Holds Formatted Data in a structure (user defined type)
        
        Dim InputPowerStatusByte As Integer 'The unformatted Input Power Status Byte
        Dim PhaseFail As Short 'Shows the Failing Phase Number or Zero
        'Get Chassis Data
        FailureMessage = GetOneDataDumpFromBothChassis()
        If FailureMessage <> "" Then
            FailureMessage = "Communication Error: Status Byte not returned."
            GoTo CompleteFunction 'Chassis Communication Error
        End If

        'Check for Input Power Fault
        InputPowerStatusByte = Asc(Mid(Buffer.ToString(), 96, 1))
        If (InputPowerStatusByte And &H6) = 0 Then
            'Power Ok
        Else            'Input Power Fail
            If (InputPowerStatusByte And &H4) = 4 Then 'Fault Low
                FailureMessage = "Error: Input Power Voltages have fallen below the safe operating level."
            End If
            If (InputPowerStatusByte And &H2) = 2 Then 'Fault High
                FailureMessage = "Error: Input Power Voltages have risen above the safe operating level."
            End If
            GoTo CompleteFunction
        End If

        If (PowerStatusPhase1 <> 0) And (PowerStatusDc = 0) Then 'HAM1
            FailureMessage = "Error: FPU HAM Phase 1 is not functioning correctly due to"
            PhaseFail = 1
        End If
        If (PowerStatusPhase2 <> 0) And (PowerStatusDc = 0) Then 'HAM2
            FailureMessage = "Error: FPU HAM Phase 2 is not functioning correctly due to"
            PhaseFail = 2
        End If
        If (PowerStatusPhase3 <> 0) And (PowerStatusDc = 0) Then 'HAM3
            FailureMessage = "Error: FPU HAM Phase 3 is not functioning correctly due to"
            PhaseFail = 3
        End If
        'Add some diagnostic data
        If (PowerStatusAc <> 0) And (PhaseFail <> 0) Then
            If PowerStatusSingle <> 0 Then 'Three Phase
                'Three Phase
                FailureMessage &= " a missing neutral connection or a missing phase connection"
            Else
                'Single Phase
                FailureMessage &= " a faulty power cable or connector"
            End If
            GoTo CompleteFunction
        End If

        'Check For FPU Module Faults
        CheckFpuStatusByte(StartChassis.StatusICPU) 'Get FPU Characteristics
        'FpuStatAddress% '= Module With Problem
        If FpuStatModFault <> 0 Then
            If FpuStatOffMismnatch = 0 Then 'Voltage Problem
                If FpuStatOverVolt = 0 Then 'Under Voltage
                    FailureMessage = "Error: FPU Module " & Str(FpuStatAddress) & " has faulted due to "
                    FailureMessage &= "an Under Voltage condition."
                    GoTo CompleteFunction
                Else                    'Over Voltage
                    FailureMessage = "Error: FPU Module " & Str(FpuStatAddress) & " has faulted due to "
                    FailureMessage &= "an Over Voltage condition."
                    GoTo CompleteFunction
                End If
            Else                'Mismatched Module
                FailureMessage = "Error: FPU Module " & Str(FpuStatAddress)
                FailureMessage &= " has been inserted in the wrong FPU slot."
                GoTo CompleteFunction
            End If
        End If

CompleteFunction:  'Goto Label Address

        'Return Function Value
        ChassisBackplaneSupplyCheck = FailureMessage

    End Function





    Function GetMaximumTemperatureRise() As Single
        '************************************************************
        '************************************************************
        '* Nomenclature   : System Monitor  [SystemStartUp]         *
        '*    DESCRIPTION:                                          *
        '*     Evaluates the Data of Both Chassis units and         *
        '*     calculates the maximum temperture rise value         *
        '*     Typically used during startup before the             *
        '*     user is presented with the SysMon GUI                *
        '*    RETURNS                                               *
        '*     Single Precision Floating Point of Rise Value .5     *
        '*     degree Celsius resolution.                           *
        '*    EXAMPLE:                                              *
        '*     MaxRise! = GetMaximumTemperatureRise()                *
        '************************************************************
        
        Dim ExitFlag As Short
        Dim MaximumRiseTemp As Single
        Dim ChassisSlot As Short

        ExitFlag = True
        MaximumRiseTemp = 0
        For ChassisSlot = 0 To 12
            If PrimaryChassis.TempRisePerSlot!(ChassisSlot) > 3 Then
                ExitFlag = False
            End If
            If SecondaryChassis.TempRisePerSlot!(ChassisSlot) > 3 Then
                ExitFlag = False
            End If
            If PrimaryChassis.TempRisePerSlot!(ChassisSlot) > MaximumRiseTemp Then
                MaximumRiseTemp = PrimaryChassis.TempRisePerSlot!(ChassisSlot)
            End If

            If SecondaryChassis.TempRisePerSlot!(ChassisSlot) > MaximumRiseTemp Then
                MaximumRiseTemp = SecondaryChassis.TempRisePerSlot!(ChassisSlot)
            End If
        Next ChassisSlot

        'Return Function Value
        GetMaximumTemperatureRise = MaximumRiseTemp

    End Function




    Function GetMaximumIntakeTemperature() As Single
        '************************************************************
        '************************************************************
        '* Nomenclature   : System Monitor  [SystemStartUp]         *
        '*    DESCRIPTION:                                          *
        '*     Evaluates the Data of Both Chassis units and         *
        '*     calculates the maximum temperture intake value       *
        '*     Typically used during startup before the             *
        '*     user is presented with the SysMon GUI                *
        '*    RETURNS                                               *
        '*     Single Precision Floating Point of Intake Temp Value *
        '*     .5 degree Celsius resolution.                        *
        '*    EXAMPLE:                                              *
        '*     Max! = GetMaximumTemperatureRise()                   *
        '************************************************************

        If PrimaryChassis.IntakeTemperature > SecondaryChassis.IntakeTemperature Then
            'Return Function Value
            GetMaximumIntakeTemperature = PrimaryChassis.IntakeTemperature
        Else
            'Return Function Value
            GetMaximumIntakeTemperature = SecondaryChassis.IntakeTemperature
        End If


    End Function




    Function GetMinimumIntakeTemperature() As Single
        '************************************************************
        '************************************************************
        '* Nomenclature   : System Monitor  [SystemStartUp]         *
        '*    DESCRIPTION:                                          *
        '*     Evaluates the Data of Both Chassis units and         *
        '*     calculates the minimum temperture intake value       *
        '*     Typically used during startup before the             *
        '*     user is presented with the SysMon GUI                *
        '*    RETURNS                                               *
        '*     Single Precision Floating Point of Intake Temp Value *
        '*     .5 degree Celsius resolution.                        *
        '*    EXAMPLE:                                              *
        '*     Max! = GetMinimumTemperatureRise()                   *
        '************************************************************

        If PrimaryChassis.IntakeTemperature < SecondaryChassis.IntakeTemperature Then
            'Return Function Value
            GetMinimumIntakeTemperature = PrimaryChassis.IntakeTemperature
        Else
            'Return Function Value
            GetMinimumIntakeTemperature = SecondaryChassis.IntakeTemperature
        End If


    End Function



    Function GetOneDataDumpFromBothChassis() As String
        GetOneDataDumpFromBothChassis = ""
        '************************************************************
        '************************************************************
        '* Nomenclature   : System Monitor  [SystemStartUp]         *
        '*    DESCRIPTION:                                          *
        '*     Gats a data dump from both chassis and formats the   *
        '*     data.  Typically used during startup before the      *
        '*     user is presented with the SysMon GUI                *
        '*    RETURNS                                               *
        '*     String containing the failure description text text  *
        '*    EXAMPLE:                                              *
        '*     Failure$ = GetOneDataDumpFromBothChassis()           *
        '************************************************************
        
        Dim Buffer As String = Space(255) 'Data Dump Buffer
        Dim Status As Short 'System Status Byte
        Dim retCount As Integer 'VISA: Number of characters returned
        Dim Data As String 'Input Buffer Formatted into a VB String
        Dim ReadTimeout As Short 'Number of "reads" left until an error is realized
        
        Dim PriChass As Short 'Got Primary Chassis Data Flag
        
        Dim SecChass As Short 'Got Secondary Chassis Data Flag
        Dim FailureMessage As String 'Description of failure
        Dim ChassisIndex As Short 'Chassis Index value
        On Error Resume Next

        'Get VXI Chassis Data
        ReadTimeout = 30 'Seconds To Acquire Chassis Data Before Time Out Failure
        'If DebugDwh% = True Then ReadTimeout% = 0

        Delay(2) 'Allow Slot 0 Firmware To Start Reporting Data

        'Test Case
        Dim defaultRMSession As Integer = 0
        Dim handle1 As Integer = 0
        Dim handle2 As Integer = 0
        viOpenDefaultRM(defaultRMSession)

        Do 'Acquire Primary and Secondary Chassis Informaton
            Delay(1)
            ReadTimeout -= 1

            RetValue = viReadSTB(GpibControllerHandle0, Status) 'Get Status Byte
            RetValue = viRead(GpibControllerHandle15, Buffer, 255, retCount) 'Get Data Dump

            Dim byteArray() As Byte

            'Debugging Test Case
            If DebugDwh = True Then
                Buffer = SendStringOfDebugData()
                retCount = 111
                Status = GetStatusDebug()
                'PrintDataDump (Buffer$)
            End If

            If Status And 128 Then
                Data = Mid(Buffer, 1, retCount)
                If Truncate(Asc(Mid(Buffer, 60, 1))) And 8 Then
                    'Get Chassis #2
                    SecondaryChassis = FormatSystemMonitorData(Buffer)
                    SecChass = True
                Else
                    'Get Chassis #1
                    PrimaryChassis = FormatSystemMonitorData(Buffer)
                    PriChass = True
                End If
            End If
        Loop Until (PriChass = True And SecChass = True) Or (ReadTimeout <= 0)

        'Do not prevent startup if factory Single Chassis Mode is set
        If SING_CHASSIS_OPTION = True Then
            GoTo EndOfFunction
        End If

        'Check for module fault
        If ModFaultDet = False Then
            CheckActionByte(CStr(Status)) 'Format Action Byte
            If ActModFault = 16 Then 'FPU/PPU modual faulted
                ModFaultDet = True
                FailureMessage = "FPU or PPU is faulted"
            End If
        End If

        If ReadTimeout <= 0 And ActModFault <> 16 Then 'If timeout occurs then report an error
            FailureMessage = "Chassis Communication Timeout Error"
            GoTo EndOfFunction
        End If


        'Check for Chassis Communication Error
        For ChassisIndex = 1 To 2
            If ChassisCommunication(ChassisIndex) = True Then
                If ChassisIndex = 1 Then
                    FailureMessage = "Communication Error: Primary VXI Chassis is not reporting data."
                    GoTo EndOfFunction
                Else
                    FailureMessage = "Communication Error: Secondary VXI Chassis is not reporting data."
                    GoTo EndOfFunction
                End If
            End If
        Next ChassisIndex

EndOfFunction:
        GetOneDataDumpFromBothChassis = FailureMessage

    End Function


    Sub InitIniTimeStamp()
        '************************************************************
        '************************************************************
        '* Nomenclature   : System Monitor  [SystemStartUp]         *
        '*    DESCRIPTION:                                          *
        '*     Writes the formatted time to the VIPERT.INI file.      *
        '*     Other applications look at this data for determining *
        '*     warm-up times and other system timing requirements.  *
        '*    EXAMPLE:                                              *
        '*      InitIniTimeStamp                                    *
        '************************************************************
        
        Dim ReturnValue As Integer 'API Returned Eror Code
        Dim lpFileName As String 'File to write to (VIPERT.INI)
        Dim lpKeyName As String 'KEY= in INI file
        Dim lpDefault As String 'Default value if key not found
        Dim lpApplicationName As String '[Application] in INI file
        
        Dim DateSerVal As Single 'Date Serial Code
        
        Dim TimeSerVal As Single 'Time Serial Code
        
        Dim DisplaySerVal As Double

        lpFileName = Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\ATS\", "IniFilePath", Nothing)

        'Init File
        lpKeyName = "STARTUP_TIME"
        DateSerVal = DateSerial(Year(Now), Month(Now), DateAndTime.Day(Now)).ToOADate
        TimeSerVal = TimeSerial(Hour(Now), Minute(Now), Second(Now)).ToOADate
        DisplaySerVal = DateSerVal + TimeSerVal
        lpDefault = VB6.Format(DisplaySerVal, "0.00000")
        lpApplicationName = "System Startup"
        ReturnValue = WritePrivateProfileString(lpApplicationName, lpKeyName, lpDefault, lpFileName)

    End Sub


    Sub CloseIniTimeStamp()
        '************************************************************
        '************************************************************
        '* Nomenclature   : System Monitor  [SystemStartUp]         *
        '*    DESCRIPTION:                                          *
        '*     Writes the formatted time to the VIPERT.INI file.      *
        '*     Other applications look at this data for determining *
        '*     shut-down times and other system timing requirements.*
        '*    EXAMPLE:                                              *
        '*      CloseIniTimeStamp                                   *
        '************************************************************
        
        Dim ReturnValue As Integer 'API Returned Eror Code
        Dim lpFileName As String 'File to write to (VIPERT.INI)
        Dim lpKeyName As String 'KEY= in INI file
        Dim lpDefault As String 'Default value if key not found
        Dim lpApplicationName As String '[Application] in INI file
        
        Dim DateSerVal As Single 'Date Serial Code
        
        Dim TimeSerVal As Single 'Time Serial Code
        
        Dim DisplaySerVal As Double

        lpFileName = Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\ATS\", "IniFilePath", Nothing)

        'Close Value
        lpKeyName = "SHUTDOWN_TIME"
        DateSerVal = DateSerial(Year(Now), Month(Now), DateAndTime.Day(Now)).ToOADate
        TimeSerVal = TimeSerial(Hour(Now), Minute(Now), Second(Now)).ToOADate
        DisplaySerVal = DateSerVal + TimeSerVal
        lpDefault = VB6.Format(DisplaySerVal, "0.00000")
        lpApplicationName = "System Startup"
        ReturnValue = WritePrivateProfileString(lpApplicationName, lpKeyName, lpDefault, lpFileName)

    End Sub



    Sub LaunchSelfTest()

        Dim KeyInfo As String
        Dim A As Integer

        'Inhibit for factory mode
        If SING_CHASSIS_OPTION Then Exit Sub

        KeyInfo = GatherIniFileInformation("File Locations", "SYSTEM_SELF_TEST", "")

        If FileExists(KeyInfo) Then
            A = Shell(Q & KeyInfo & Q, AppWinStyle.NormalFocus)
        Else
            MsgBox("File Not Found: " & KeyInfo, MsgBoxStyle.Critical)
        End If

    End Sub



    Function RunResMan() As String
        RunResMan = ""
        '************************************************************
        '************************************************************
        '* Nomenclature   : System Monitor  [SystemStartUp]         *
        '*    DESCRIPTION:                                          *
        '*     Shells To The Resource Manager and Delays until the  *
        '*     software has completed executing.                    *
        '*    RETURNS                                               *
        '*     String containing the failure description text text  *
        '*    EXAMPLE:                                              *
        '*     Failure$ = RunResMan()                               *
        '************************************************************
        
        Dim lpReturnedString As New StringBuilder(Space(255), 255) 'Return Data Buffer
        Dim VxiResManFilePath As String 'Path Of Application
        Dim ReturnValue As Integer 'API Return Code
        Dim LoopTimeout As Short 'Timeout until error
        Dim FailureMessage As String 'Description of failure

        'Clear String Buffer
        lpReturnedString = New StringBuilder(Space(255))
        'Find File Locations
        VxiResManFilePath = GatherIniFileInformation("File Locations", "VXI_RESMAN", " ")
        'Get Callback Address
        'frmCheckList.Load()
        'lpEnumFunc = frmCheckList.Callback1.ProcAddress

        Delay(1)
        'Run VXI Resource Manager (quick init -o)
        If FileExists(VxiResManFilePath) Then
            'added -l to the resman shell so that resman outputs a log file for debug use.
            'ReturnValue = Shell(Q & VxiResManFilePath & Q & " -o -l", AppWinStyle.MinimizedFocus)
            Dim resmanProcess As New Process()
            resmanProcess.StartInfo.FileName = VxiResManFilePath
            resmanProcess.StartInfo.Arguments = "-o -l"
            Try
                resmanProcess.Start()
                'launch process synchronously
                resmanProcess.WaitForExit()
            Catch ex As Exception
                FailureMessage = "Cannot Locate File: " & VxiResManFilePath & " "
                Err.Number = 0
                GoTo ErrorOutHere
            End Try

        Else
            FailureMessage = "Cannot Locate File: " & VxiResManFilePath & " "
        End If
        'Delay(0.5) 'Time to shell to the application

        'LoopTimeout = 90 '1.5+ Minutes
        'Do
        '    ResmanRunning = False
        '    Dim lParam As Integer
        '    Dim RetValue As Integer
        '    Application.DoEvents()
        '    lParam = 1
        '    'RetValue = EnumWindows(lpEnumFunc, lParam)
        '    If ResmanRunning = False Then
        '        Exit Do
        '    End If
        '    LoopTimeout -= 1
        '    Delay(1)
        'Loop Until LoopTimeout = 0

        'Check to see if Data Acquisition is enabled
        'If frmSysMon.chkDataInterval.Value = True Then
        '    frmSysMon.tmrDataPoll.Enabled = True
        'End If

        'If LoopTimeout = 0 Then
        '    FailureMessage = "VXI Chassis Initialization error. "
        '    GoTo ErrorOutHere
        'End If

        'Delay 1

ErrorOutHere:

        'Return Function Value
        RunResMan = FailureMessage

    End Function

    Function ShellToCtest() As String
        ShellToCtest = ""
        '************************************************************
        '************************************************************
        '* Nomenclature   : System Monitor  [SystemStartUp]         *
        '*    DESCRIPTION:                                          *
        '*     Shells To The System COnfidence Test and Delays until*
        '*     the software has completed executing.                *
        '*    RETURNS                                               *
        '*     String containing the failure description text text  *
        '*    EXAMPLE:                                              *
        '*     Failure$ = ShellToCtest()                            *
        '************************************************************
        Dim RetVal As Integer 'Handle of CTEST application
        Dim TestTimeout As Short 'Time until failure
        Dim FailureMessage As String 'Description of failure
        Dim KeyInfo As String 'Information In VIPERT.INI Key
        'MessageBox.Show("SysMon", "SYSTEM_SURVEY", MessageBoxButtons.OK)

        If SING_CHASSIS_OPTION Then GoTo EndOfFunction

        'Shell to Confidence Test
        If FileExists(ConfTestFilePath) Then
            Dim cTestProcess As New Process
            cTestProcess.StartInfo.FileName = ConfTestFilePath
            cTestProcess.StartInfo.Arguments = "SYSMON"
            cTestProcess.Start()
            'RetVal = Shell(Q & ConfTestFilePath & Q & " CmdLineArg", AppWinStyle.NormalFocus)
        Else
            FailureMessage = "File Not Found: " & ConfTestFilePath
            GoTo EndOfFunction
        End If

        'Check to see if Confidence Test is Running
        TestTimeout = 10
        Do
            Delay(1)
            KeyInfo = GatherIniFileInformation("System Startup", "SYSTEM_SURVEY", "")
            TestTimeout -= 1
        Loop Until KeyInfo = "RUNNING" Or TestTimeout <= 0
        If TestTimeout <= 0 Then
            FailureMessage = "System Survey application is not responding"
            GoTo EndOfFunction
        End If

        'Wait unitl Confidence test has completed
        TestTimeout = 360 'Seconds to perform SYSTEM_SURVEY Operations (approx 6 Min.)
        If DebugDwh Then
            TestTimeout = 5
        End If
        Do
            Delay(1)
            KeyInfo = GatherIniFileInformation("System Startup", "SYSTEM_SURVEY", "")
            TestTimeout -= 1
        Loop Until KeyInfo <> "RUNNING" Or TestTimeout <= 0
        If TestTimeout <= 0 Then
            FailureMessage = "System Survey Application is not Responding"
            GoTo EndOfFunction
        End If

        'Check Results of Test
        If KeyInfo = "PASS" Then
            'Passed
        Else
            FailureMessage = "Instrumentation Error: System Survey Failed"
        End If

        frmCheckList.Refresh()

EndOfFunction:
        'Return Function Value
        ShellToCtest = FailureMessage

    End Function

    Function ChassisTemperatureCheck() As String
        ChassisTemperatureCheck = ""
        '************************************************************
        '************************************************************
        '* Nomenclature   : System Monitor  [SystemStartUp]         *
        '*    DESCRIPTION:                                          *
        '*     Evaluates chassis temperture conditions and enables  *
        '*     the system to start based upon the temperature data. *
        '*    RETURNS                                               *
        '*     String containing the failure description text text  *
        '*    EXAMPLE:                                              *
        '*     Failure$ = ChassisTemperatureCheck()                 *
        '************************************************************
        Dim UserText As String
        Dim MaximumRiseTemp As Single
        Dim MaxIntake As Single
        Dim MinIntake As Single
        Dim AverageAmbientTemperature As Single 'Chassis Temperature
        Dim FailureMessage As String 'Description of failure
        On Error Resume Next

        nTenFlag = False

        If SING_CHASSIS_OPTION = True Then
            GoTo EndOfFunction
        End If

        'Get Chassis Data
        FailureMessage = GetOneDataDumpFromBothChassis()
        If FailureMessage <> "" Then
            GoTo EndOfFunction 'Chassis Communication Error
        End If

        'Check Temp Rise Values before continuing
        MaximumRiseTemp = GetMaximumTemperatureRise()
        If MaximumRiseTemp > 3 Then
            'Set Fans 100% to De-Humidify System and notify user
            UserText = "The instrument chassis has not reached the target "
            UserText &= "temperature rise value.  Wait until "
            UserText &= "the countdown indicates that the tester is "
            UserText &= "ready to continue. If the target value is not "
            UserText &= "achieved in a 15 minute time period, then the "
            UserText &= "startup sequence will continue. "
            UserText &= "This procedure can be bypassed by pressing the "
            UserText &= "Continue button."
            frmSysWait.proCountdown.Value = 0
            frmSysWait.lblValue(0).Text = "3.0"
            frmSysWait.lblValue(1).Text = "30.0"
            Delay(1)
            SetFanSpeed(1, 100)
            Delay(1)
            SetFanSpeed(2, 100)

            frmSysWait.TempToWaitFor = "MaximumTemperatureRise"
            frmSysWait.dataDumpTimer.Enabled = True
            ShowSystemWaitDialog("&Continue", UserText, "Temp. Rise (Degrees Celsius)", 900)

            ResetChassis(1)
            ResetChassis(2)
            frmSysWait.Close()
        End If

        MaxIntake = GetMaximumIntakeTemperature()
        If MaxIntake > 55 Then
            'Set Fans 100% to De-Humidify System and notify user
            UserText = "The instrument chassis has not reached the target "
            UserText &= "temperature intake value.  Wait until "
            UserText &= "the countdown indicates that the tester is "
            UserText &= "ready to continue. If the target value is not "
            UserText &= "achieved in a 15 minute time period, then the "
            UserText &= "startup sequence will continue. "
            UserText &= "This procedure can be bypassed by pressing the "
            UserText &= "Continue button."

            frmSysWait.proCountdown.Value = 0
            frmSysWait.lblValue(0).Text = "55.0"
            frmSysWait.lblValue(1).Text = VB6.Format(MaxIntake, "0.0")
            SetFanSpeed(1, 100)
            Delay(1)
            SetFanSpeed(2, 100)

            frmSysWait.dataDumpTimer.Enabled = True
            frmSysWait.TempToWaitFor = "MaximumIntakeTemperature"
            ShowSystemWaitDialog("&Continue", UserText, "Temp. (Degrees Celsius)", 900)

            ResetChassis(1)
            ResetChassis(2)
            frmSysWait.Close()
        End If

        MinIntake = GetMinimumIntakeTemperature()
        If (MinIntake >= -10) And (MinIntake < 0) Then
            'Set Fans 100% to De-Humidify System and notify user
            nTenFlag = True
            UserText = "The instrument chassis has not reached the target "
            UserText &= "temperature intake value.  Wait until "
            UserText &= "the countdown indicates that the tester is "
            UserText &= "ready to continue. If the target value is not "
            UserText &= "achieved in a 30 minute time period, then the "
            UserText &= "startup sequence will continue. "
            UserText &= "This procedure can be bypassed by pressing the "
            UserText &= "Continue button."

            frmSysWait.proCountdown.Value = 0
            frmSysWait.lblValue(0).Text = "5.0"
            frmSysWait.lblValue(1).Text = VB6.Format(MinIntake, "0.0")
            SetFanSpeed(1, 60)
            Delay(1)
            SetFanSpeed(2, 60)
            Delay(1)
            SetHeater(0, True)
            SetHeater(1, True)
            SetHeater(2, True)
            SetHeater(3, True)
            frmSysWait.dataDumpTimer.Enabled = True
            frmSysWait.TempToWaitFor = "MinimumIntakeTemperature"
            ShowSystemWaitDialog("&Continue", UserText, "Temp. (Degrees Celsius)", 1800)
            frmSysWait.Close()
        End If

        'Check for "SAFE TO TURN ON" operating temperature
        AverageAmbientTemperature = (PrimaryChassis.IntakeTemperature)
        If (AverageAmbientTemperature < -10) Or (AverageAmbientTemperature > 65) Then
            FailureMessage = "Operating Temperature Error: " & Str(AverageAmbientTemperature) & " C"
            GoTo EndOfFunction
        End If

        AverageAmbientTemperature = (SecondaryChassis.IntakeTemperature)
        If (AverageAmbientTemperature < -10) Or (AverageAmbientTemperature > 65) Then
            FailureMessage = "Operating Temperature Error: " & Str(AverageAmbientTemperature) & " C"
            GoTo EndOfFunction
        End If


EndOfFunction:

        'Return Function Value
        ChassisTemperatureCheck = FailureMessage

    End Function

    Function InitGpibSupplies() As String
        InitGpibSupplies = ""
        '************************************************************
        '************************************************************
        '* Nomenclature   : System Monitor  [SystemStartUp]         *
        '*    DESCRIPTION:                                          *
        '*     Evaluates GPIB communication status enables then     *
        '*     system to start based upon the communication data    *
        '*    RETURNS                                               *
        '*     String containing the failure description text text  *
        '*    EXAMPLE:                                              *
        '*     Failure$ = InitGpibSupplies()                        *
        '************************************************************
        Dim ResourceName As String 'VISA Resource Name
        Dim SupplyIndex As Short 'Index of PPU Supoply
        Dim InitTry As Short 'Number of attempts at getting GPIB communication
        Dim FailureMessage As String 'Description of GPIB failure
        Dim Status As Short 'Status Byte from Slot Zero Controller
        Dim retCount = 0

        'MessageBox.Show("Debug", "SysMon", MessageBoxButtons.OK) 'keep for debugging purposes

        'Open A Resource Manager Session
        Delay(1)
        RetValue = viOpenDefaultRM(RmSession)
        If RetValue <> VI_SUCCESS Then
            FailureMessage = "Error Initializing VISA GPIB Resource Manager " 'Corrected misspelled word
            GoTo CompleteFunction 'Resouce to Resource
        End If

        'Open The UUTPS Controller 5:0 (Status Byte)
        ResourceName = "GPIB0::5::0"
        InitTry = 0
        Do
            RetValue = viOpen(RmSession, ResourceName, 0, 10, GpibControllerHandle0)
            Delay(0.5)
            InitTry += 1
        Loop Until ((RetValue = 0) Or InitTry >= 10) 'Changed From "RetValue& = VI_SUCCESS" to RetValue& >= VI_SUCCESS

        If (RetValue <> 0) Then
            FailureMessage = "VISA Communication Error: " & " " & ResourceName
            GoTo CompleteFunction
        End If

        'Extra Query Added to determine if GPIB Cable is connected to VIPERT
        RetValue = viReadSTB(GpibControllerHandle0, Status)
        If (RetValue <> 0) Then
            FailureMessage = "VISA Communication Error: " & " " & ResourceName
            GoTo CompleteFunction
        End If

        'Open The UUTPS (PPU) Supplies 5:1 to 5:10
        For SupplyIndex = 1 To 10
            Delay(0.1)
            ResourceName = "GPIB0::5::" & Trim(Str(SupplyIndex))
            RetValue = viOpen(RmSession, ResourceName, 0, 10, PpuControllerHandle(SupplyIndex))
            If (RetValue <> 0) Then
                FailureMessage = "VISA Communication Error: " & " " & ResourceName
                GoTo CompleteFunction
            End If

        Next SupplyIndex

        Delay(0.1)
        'Open The System Controller 5:11
        ResourceName = "GPIB0::5::11"
        RetValue = viOpen(RmSession, ResourceName, 0, 10, GpibControllerHandle11)
        If (RetValue <> 0) Then
            FailureMessage = "VISA Communication Error: " & " " & ResourceName
            GoTo CompleteFunction
        End If


        'Open The Primary Chassis Controller 5:12
        Delay(0.1)
        ResourceName = "GPIB0::5::12"
        RetValue = viOpen(RmSession, ResourceName, 0, 10, ChassisControllerHandle1)
        If (RetValue <> 0) Then
            FailureMessage = "VISA Communication Error: " & " " & ResourceName
            GoTo CompleteFunction
        End If

        'Open The Secondary Chassis Controller 5:13
        Delay(0.1)
        ResourceName = "GPIB0::5::13"

        RetValue = viOpen(RmSession, ResourceName, 0, 10, ChassisControllerHandle2)
        If (RetValue <> 0) Then
            FailureMessage = "VISA Communication Error: " & " " & ResourceName
            GoTo CompleteFunction
        End If

        Delay(0.1)
        'Open GPIB Data Controller 5:15
        ResourceName = "GPIB0::5::15"
        InitTry = 0
        Do
            RetValue = viOpen(RmSession, ResourceName, VI_NULL, VI_NULL, GpibControllerHandle15)
            Delay(0.5)
            InitTry += 1
        Loop Until (RetValue >= VI_SUCCESS) Or InitTry >= 6 'Changed from "RetValue& <> VI_SUCCESS" to "RetValue& >= VI_SUCCESS"
        If (RetValue <> 0) Then
            FailureMessage = "VISA Communication Error: " & " " & ResourceName
            GoTo CompleteFunction
        End If

        If (ShortStartFlag <> True) Then
            SetFpu(False) 'Safety OFF if left on from an abnormal operating condition
        End If
        ResetPdu() 'Clear Input Faults and Shutdown Failsafe Status

CompleteFunction:

        'Return Function Value
        InitGpibSupplies = FailureMessage


    End Function

    Function StartChassis() As String
        StartChassis = ""
        '************************************************************
        '************************************************************
        '* Nomenclature   : System Monitor  [SystemStartUp]         *
        '*    DESCRIPTION:                                          *
        '*     Evaluates chassis data and if the data is valid the  *
        '*     FPU will start.                                      *
        '*    RETURNS                                               *
        '*     String containing the failure description text text  *
        '*    EXAMPLE:                                              *
        '*     Failure$ = StartChassis()                            *
        '************************************************************
        Dim ReadTimeout As Short 'Seconds to get a status byte before timeout error
        Dim Status As Short 'Status Byte
        
        Dim Answer As DialogResult 'Message Box Return Value
        Dim FailureMessage As String 'Description of failure

        'Check For Green Chassis Power Switch Activation
        Do
            ReadTimeout = 5 'Seconds to get a status byte before timeout error
            Do 'Get A Status Byte
                Delay(1)
                ReadTimeout -= 1
                RetValue = viReadSTB(GpibControllerHandle0, Status)
            Loop Until (((Status And &HF0) = &HB0) And (Status <> &HB0)) Or (ReadTimeout <= 0)
            If ReadTimeout <= 0 Then
                FailureMessage = "Communication Error: Status Byte not returned."
            End If

            PowerSwitch = True 'Set Flag

            CheckActionByte(CStr(Status))
            If ActModFault = 16 Then Exit Do

            If ActVolt28Ok Then 'Green Switch Activated
                If PowerSwitch Then
                    PowerSwitch = False 'Clear Flag
                    SetFpu(True)
                End If
                Exit Do 'Ready To Continue With Startup
            Else                'Green Switch NOT Activated
                Answer = MsgBox("Instrument Chassis Power Switch Not Activated.", MsgBoxStyle.RetryCancel)
                If Answer = DialogResult.Cancel Then
                    FailureMessage = "Error: Instrument Chassis Power Switch Not Activated."
                    Exit Do
                End If
                Delay(2)  'wait two seconds for pdu to respond to power switch
            End If
        Loop


        'Delay 1
        ResetChassis(1) 'RESET Chassis 1 Controller
        'Delay 1
        ResetChassis(2) 'RESET Chassis 2 Controller

EndOfFunc:

        'Return Function Value
        StartChassis = FailureMessage

    End Function

    Sub StartUpSystem()
        '************************************************************
        '************************************************************
        '* Nomenclature   : VIPERT SYSTEM STARTUP SEQUENCE            *
        '*    DESCRIPTION:                                          *
        '*     This Module is the module code entry point           *
        '************************************************************
        Dim RetVal As Integer 'Application Handle

        'Pop Splash Screen
        ' Do not show if restart
        If (RestartSysmonFlag = False) Then
            CenterForm(frmSplash) : frmSplash.Show() : frmSplash.Refresh()
            frmSplash.cmdOK.Visible = False 'make button invisible
            Delay(2)
        End If
        'Map/Search VIPERT.INI For System Files
        LogFile = Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\ATS\", "LoggingDirectory", Nothing) & "START.LOG"
        SystemLogFilePath = GatherIniFileInformation("File Locations", "SYSTEM_LOG", " ")
        VIPERTMenuFilePath = GatherIniFileInformation("File Locations", "SYSTEM_MENU", " ")
        ConfTestFilePath = GatherIniFileInformation("File Locations", "SYSTEM_SURVEY", " ")
        'Check For Valid Maintenance Account Security Rights
        CheckUserName()
        'Initialize VIPERT.INI File
        InitIniTimeStamp()

        'Delete Old Log Files
        On Error Resume Next
        Kill(LogFile)
        'Set Log File Start-Up Header
        Echo("***************************************************************************************")
        Echo("********************** USMC Automated Test System (ATS)*")
        Echo("**************************************************************************************")
        Echo("System Startup Time:")
        'Echo(GetEchoTime)
        Echo(DateTime.Now.ToString)
        Echo("Log in account: " & UserName)

        'Show Checklist
        CenterForm(frmCheckList)
        'Get Sysmon
        'frmSysMon.Load()
        frmSysMon.Text = "ATS System Monitor [" & UserName & "]"
        InitInputBoxLists()
        'Remove Splash Screen
        frmSplash.Close()
        frmCheckList.Refresh()
        frmCheckList.Show()
        frmCheckList.Refresh()

        'STARTUP CHECKLIST BEGINS HERE
        SendStatusBarMessage("Beginning ATS Startup Procedure...")

        For TestIndex = 0 To 9
            StartupTest(TestIndex)
        Next TestIndex
        SendStatusBarMessage("ATS Startup Completed.  Click [CLOSE] to Continue.")
        Echo("ATS Startup Completed.")

        'Countdown or Ok_Click
        If (RestartSysmonFlag = False) Then

            UserTimeout = 120
            frmCheckList.cmdClose.Visible = True
            frmCheckList.cmdClose.Select()
            frmCheckList.Refresh()
            Application.DoEvents()
            Do
                Application.DoEvents()
                frmCheckList.Refresh()
                frmCheckList.sbrUserInformation.Panels(1).Text = CStr(UserTimeout)
                Delay(1)
                frmCheckList.sbrUserInformation.Refresh()
                UserTimeout -= 1
                Application.DoEvents()
                frmCheckList.Refresh()
            Loop Until UserTimeout <= 0
            Application.DoEvents()
            frmCheckList.Visible = False

            If SING_CHASSIS_OPTION Then Exit Sub

            If (DebugDwh <> True) Then
                'Shell to System Menu
                'Removed until System menu is upgraded to vb.net
                RetVal = Shell(Q & VIPERTMenuFilePath & Q, AppWinStyle.NormalFocus)
                Application.DoEvents()
                'Need to Add back in when Ctest is completed
                If CtestFailed = True Then LaunchSelfTest()
            End If

        Else
            frmCheckList.Visible = False
        End If

        'Set Log File Start-Up Footer
        Echo("********************************************************************************")
        Echo("********************** End Of ATS Startup Sequence ****************************")
        Echo("********************************************************************************")
        UpdateLogFile()
        Delay(0.5)

        Application.DoEvents()

        'Modify Bus Config File To Reflect Current Configuration (CORE or RF Option)
        '    CopyAtlasDeviceFiles

        '************************** ADDED FOR DEBUGGING  **********************
        'StartUpFailFlag% = False 'For Debuging only, comment out to run on VIPERT

        'Remove Checklist
        If StartUpFailFlag = True Then
            Environment.Exit(-1)
        End If

    End Sub

    Sub Init_CICL()

        Dim Allocation As String = ""
        Dim XmlBuf As String = ""
        Dim Response As String = ""
        Dim initTries As Integer = 0
        Dim errorStatus As Integer = 1

        'Give CICL a chance to start responding to clients
        Delay(15)

        While (errorStatus <> 0)
            errorStatus = atxml_Initialize(ProcType, guid)
            Delay(1)
            If (initTries > 60) Then
                Exit Sub
            End If
            initTries = initTries + 1
        End While

        'Wait An Additional 4 seconds for the CICL session to establish
        Delay(4)
        Allocation = Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\ATS\", "PAWSAllocationPath", Nothing)

        'Determine if the ARB is functioning
        Response = Space(4096)
        XmlBuf = "<AtXmlTestRequirements>" & "    <ResourceRequirement>" & "        <ResourceType>Source</ResourceType>" & "        <SignalResourceName>ARB_GEN_1</SignalResourceName> " & "    </ResourceRequirement> " & "</AtXmlTestRequirements>"        '
        errorStatus = atxml_ValidateRequirements(XmlBuf, Allocation, Response, 4096)
        'If Status <> 0 Then

        ' Determine if the C/T is functioning
        'XmlBuf = "<AtXmlTestRequirements>" &
        '      '         "    <ResourceRequirement>" &
        '      '         "        <ResourceType>Source</ResourceType>" &
        '      '         "        <SignalResourceName>CNTR_1</SignalResourceName> " &
        '      '         "    </ResourceRequirement> " &
        '      '         "</AtXmlTestRequirements>" '
        'Status = atxml_ValidateRequirements(XmlBuf, Allocation, Response, 4096)
        'If Status <> 0 Then

        'Determine if the DMM is functioning
        'XmlBuf = "<AtXmlTestRequirements>" &
        '      '         "    <ResourceRequirement>" &
        '      '         "        <ResourceType>Source</ResourceType>" &
        '      '         "        <SignalResourceName>DMM_1</SignalResourceName> " &
        '      '         "    </ResourceRequirement> " &
        '      '         "</AtXmlTestRequirements>" '
        'Status = atxml_ValidateRequirements(XmlBuf, Allocation, Response, 4096)
        'If Status <> 0 Then

        'Determine If The DSCOPE Is Functioning
        'XmlBuf = "<AtXmlTestRequirements>" &
        '      '         "<ResourceRequirement>" &
        '      '         "<ResourceType>Source</ResourceType>" &
        '      '         "<SignalResourceName>DSO_1</SignalResourceName> " &
        '      '         "</ResourceRequirement>" &
        '      '         "</AtXmlTestRequirements>"
        'Status& = atxml_ValidateRequirements(XmlBuf, Allocation, Response, 4096)
        'If Status <> 0 Then

        'Determine if the FG is functioning
        XmlBuf = "<AtXmlTestRequirements>" & "    <ResourceRequirement>" & "        <ResourceType>Source</ResourceType>" & "        <SignalResourceName>FUNC_GEN_1</SignalResourceName> " & "    </ResourceRequirement> " & "</AtXmlTestRequirements>"
        errorStatus = atxml_ValidateRequirements(XmlBuf, Allocation, Response, 4096)
        'If Status <> 0 Then

        'Determine if the Freedom PS is functioning
        XmlBuf = "<AtXmlTestRequirements>"
        XmlBuf &= "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & "  <SignalResourceName>DCPS_1</SignalResourceName> " & "</ResourceRequirement> "
        XmlBuf &= "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & "  <SignalResourceName>DCPS_2</SignalResourceName> " & "</ResourceRequirement>"
        XmlBuf &= "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & "  <SignalResourceName>DCPS_3</SignalResourceName> " & "</ResourceRequirement>"
        XmlBuf &= "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & "  <SignalResourceName>DCPS_4</SignalResourceName> " & "</ResourceRequirement>"
        XmlBuf &= "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & "  <SignalResourceName>DCPS_5</SignalResourceName> " & "</ResourceRequirement>"
        XmlBuf &= "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & "  <SignalResourceName>DCPS_6</SignalResourceName> " & "</ResourceRequirement>"
        XmlBuf &= "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & "  <SignalResourceName>DCPS_7</SignalResourceName> " & "</ResourceRequirement>"
        XmlBuf &= "<ResourceRequirement> " & "       <ResourceType>Source</ResourceType> " & "       <SignalResourceName>DCPS_8</SignalResourceName> " & "</ResourceRequirement>"
        XmlBuf &= "<ResourceRequirement> " & "       <ResourceType>Source</ResourceType> " & "       <SignalResourceName>DCPS_9</SignalResourceName> " & "</ResourceRequirement>"
        XmlBuf &= "<ResourceRequirement> " & "       <ResourceType>Source</ResourceType> " & "       <SignalResourceName>DCPS_10</SignalResourceName> " & "</ResourceRequirement>"
        XmlBuf &= "</AtXmlTestRequirements>"
        errorStatus = atxml_ValidateRequirements(XmlBuf, Allocation, Response, Len(XmlBuf))
        Delay(1)

        'If Status Then

        'Determine if the Switch module is functioning
        'XmlBuf = "<AtXmlTestRequirements> " &
        '      '    "    <ResourceRequirement> " &
        '      '    "       <ResourceType>Source</ResourceType> " &
        '      '    "       <SignalResourceName>PAWS_SWITCH</SignalResourceName> " &
        '      '    "    </ResourceRequirement> " &
        '      '    "</AtXmlTestRequirements>"
        'Status = atxml_ValidateRequirements(XmlBuf, Allocation, Response, 4096)
        'If Status = conNoDLL Then

        'Determine if the RF STIM is functioning
        XmlBuf = "<AtXmlTestRequirements> " & "    <ResourceRequirement> " & "       <ResourceType>Source</ResourceType> " & "       <SignalResourceName>RFGEN_1</SignalResourceName> " & "    </ResourceRequirement> " & "</AtXmlTestRequirements>"
        Response = Space(4096)
        errorStatus = atxml_ValidateRequirements(XmlBuf, Allocation, Response, 4096)
        'If Status <> 0 Then

        'Determine if the New Busses are functioning
        'Response = space(4096)
        'XmlBuf = "<AtXmlTestRequirements>" &
        '      '         "<ResourceRequirement>" &
        '      '         "  <ResourceType>Source</ResourceType>" &
        '      '         "  <SignalResourceName>COM_1</SignalResourceName> " &
        '      '         "  </ResourceRequirement> " &
        '      '         "<ResourceRequirement>" &
        '      '         "  <ResourceType>Source</ResourceType>" &
        '      '         "  <SignalResourceName>COM_2</SignalResourceName> " &
        '      '         "  </ResourceRequirement> " &
        '      '         "<ResourceRequirement>" &
        '      '         "  <ResourceType>Source</ResourceType>" &
        '      '         "  <SignalResourceName>ETHERNET_1</SignalResourceName> " &
        '      '         "  </ResourceRequirement> " &
        '      '         "<ResourceRequirement>" &
        '      '         "  <ResourceType>Source</ResourceType>" &
        '      '         "  <SignalResourceName>ETHERNET_2</SignalResourceName> " &
        '      '         "  </ResourceRequirement> " &
        '      '         "<ResourceRequirement>" &
        '      '         "  <ResourceType>Source</ResourceType>" &
        '      '         "  <SignalResourceName>PCISERIAL_1</SignalResourceName> " &
        '      '         "  </ResourceRequirement> " &
        '               "</AtXmlTestRequirements>"
        ' Status = atxml_ValidateRequirements(XmlBuf, Allocation, Response, 4096)

        'Determine if the S/R is functioning
        XmlBuf = "<AtXmlTestRequirements>"
        XmlBuf &= "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & "  <SignalResourceName>SRS_1_DS1</SignalResourceName> " & "</ResourceRequirement> "
        XmlBuf &= "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & "  <SignalResourceName>SRS_1_DS2</SignalResourceName> " & "</ResourceRequirement>"
        XmlBuf &= "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & "  <SignalResourceName>SRS_1_SD1</SignalResourceName> " & "</ResourceRequirement>"
        XmlBuf &= "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & "  <SignalResourceName>SRS_1_SD2</SignalResourceName> " & "</ResourceRequirement>"
        XmlBuf &= "</AtXmlTestRequirements>"

        Response = Space(4096)
        errorStatus = atxml_ValidateRequirements(XmlBuf, Allocation, Response, 4096)
        'If Status <> 0 Then

        'determine if the DTI is functioning
        For Pin_Index = 0 To 63
            Pin_array1(Pin_Index) = TERM9_SCOPE_CHAN(Pin_Index)
            Pin_array2(Pin_Index) = TERM9_SCOPE_CHAN(Pin_Index + 64)
            Pin_array3(Pin_Index) = TERM9_SCOPE_CHAN(Pin_Index + 128)
        Next Pin_Index

        errorStatus = terM9_init("TERM9::#0", VI_TRUE, VI_TRUE, viSession)
        If errorStatus < &H4FFF0000 Then Instr_Hand_DTI = viSession


        Delay(0.5)
        On Error GoTo 0 ' turn off error trapping

    End Sub


    Sub Echo(ByVal DataLine As String)
        '************************************************************
        '************************************************************
        '* Nomenclature   : System Monitor  [SystemStartUp]         *
        '*    DESCRIPTION:                                          *
        '*     Builds the string that is sent to the System Log     *
        '*     application.                                         *
        '*    EXAMPLE:                                              *
        '*     Echo "THIS IS ADDED TO THE LOG FILE STRING"          *
        '************************************************************
        'EchoText$ is a global variable

        EchoText &= DataLine & vbCrLf
        Application.DoEvents()
        Debug.Print(EchoText)
    End Sub

    
    Sub PassTest(ByVal TestIndexNumber As Short, ByVal Pass As Short)

        If Pass Then
            frmCheckList.PassFrame(TestIndexNumber).Image = My.Resources.frmCheckList_picPass 'LoadPicture(App.path & "\" & PASS_BITMAP)
        Else
            frmCheckList.FailFrame(TestIndexNumber).Image = My.Resources.frmCheckList_picFail 'LoadPicture(App.path & "\" & FAIL_BITMAP)
        End If

    End Sub




    Public Function sInitDevice(ByVal IDevice As Short) As String
        sInitDevice = ""
        '*************************************************************************
        '** DESCRIPTION:                                                        **
        '**     This function will attempt to enable the device identified      **
        '**     by the iDevice argument.                                        **
        '** PARAMTERS:                                                          **
        '**     iDevice:  The index of the Device to Enable                     **
        '** RETURNS:                                                            **
        '**     Empty String if successful                                      **
        '**     Error String if FAILED                                          **
        '** Windows NT4.0 NOTE:                                                 **
        '**     If the OS is NT4 (DEVCON not supported) and the device to       **
        '**     initialize is the MXI-2, Return Success and Exit Function.      **
        '*************************************************************************

        Dim sDevconCmd As String 'Devcon Command String to Enable Device
        
        Dim iFileNum As Integer 'File Number to Open
        Const DEVCON_BAT As String = "Devcon.bat" 'Batch File for executing Devcon
        Const DEVCON_LOG As String = "Devcon.log" 'Redirected Log File created for Devcon

        'No longer needs to be run. MXI-2 Controller is now installed in the Controllers
        Return ""

        'Verify Device and set command string
        If IDevice = MXI2 Then
            '--  WINDOWS NT4 OS DOES NOT SUPPORT DEVCON  --
            If iOpSystem = WINNT Then
                sInitDevice = "" 'Return Success and exit function.
                Exit Function
            End If
            sDevconCmd = "DEVCON.EXE ENABLE PCI\VEN_1093"
        ElseIf IDevice = VCC Then
            sDevconCmd = "DEVCON.EXE ENABLE PCI\VEN_112f"
        Else
            sInitDevice = "Error: Unrecognized Device."
            Exit Function 'Exit here, return with Error
        End If

        'Change current directory to the location of DEVCON.EXE
        ChDir(Application.StartupPath & "\System Test")

        On Error Resume Next

        Kill(DEVCON_BAT) 'Delete Batch File, if it exists
        Kill(DEVCON_LOG) 'Delete Log File, if it exists
        Application.DoEvents()
        Err.Clear()
        On Error GoTo 0 'Ignore any Errors

        'Setup batch file for Devcon.exe
        iFileNum = FreeFile()
        FileOpen(iFileNum, DEVCON_BAT, OpenMode.Output)
        PrintLine(iFileNum, "Devcon.exe Enable " & sDevconCmd & " > " & DEVCON_LOG)
        FileClose(iFileNum)

        'Attempt to init the device, if already enabled, the result is the same as success.
        If ExecCmd(DEVCON_BAT) = 0 Then
            If bFindInFile(DEVCON_LOG, "2 device(s) enabled.") = True Then
                sInitDevice = ""
            Else
                sInitDevice = "The MXI-2 Controller couldn't be enabled."
            End If
        Else
            sInitDevice = "Error executing Devcon.exe"
        End If

        'Clean up...
        Kill(DEVCON_BAT) 'Delete Batch File, if it exists
        Kill(DEVCON_LOG) 'Delete Log File, if it exists
        ChDir(Application.StartupPath) 'Change current directory back to the App Dir

    End Function




    Sub StartupTest(ByRef TestIndex As Short)

        Dim TextIndex As Short
        Dim TestName As String
        Dim SendBuffer As String
        Dim FailLoop As Short
        Dim success As Integer
        Dim Ciclpath As String

        Ciclpath = Application.StartupPath & "\ISS\Bin\CiclKernelC.exe"


        If TestIndex = 9 Then Exit Sub
        If SING_CHASSIS_OPTION Then
            Select Case TestIndex
                Case 4, 7, 8
                    Exit Sub 'Do Not Run That Test
                Case 5
                    frmSysMon.tmrDataPoll.Enabled = True
                    Exit Sub 'Do Not Run That Test
            End Select
        End If

        'Setup Test
        frmCheckList.tmrFlash.Tag = TestIndex 'Flash Correct Test Label
        frmCheckList.tmrFlash.Enabled = True 'Start Flash Timer
        FailureMessage = ""
        frmCheckList.InstrumentLabel(TestIndex).ForeColor = Color.Yellow

        frmCheckList.Refresh()


        'Perform Test
        Select Case TestIndex
            Case 0
                TestName = "General Purpose Instruction Bus Initialization"
                StartupTestIndex = 0
                frmCheckList.startupWorker.RunWorkerAsync()
                'Wait for background thread to finish
                While (frmCheckList.startupWorker.IsBusy)
                    Application.DoEvents()
                End While
            Case 1
                TestName = "VXI Chassis Operating Temperature"
                'Init Temp Threshold Values for the System Monitor Panel
                GetTemperatureThresholds()
                StartupTestIndex = 1
                'skip if restart
                If (RestartSysmonFlag = False) Then
                    frmCheckList.startupWorker.RunWorkerAsync()
                    'Wait for background thread to finish
                    While (frmCheckList.startupWorker.IsBusy)
                        Application.DoEvents()
                    End While
                End If
            Case 2
                TestName = "VXI Chassis Backplane Power Supplies"
                StartupTestIndex = 2
                frmCheckList.startupWorker.RunWorkerAsync()
                'Wait for background thread to finish
                While (frmCheckList.startupWorker.IsBusy)
                    Application.DoEvents()
                End While
            Case 3
                TestName = "VXI Chassis Mainframes"
                StartupTestIndex = 3
                frmCheckList.startupWorker.RunWorkerAsync()
                'Wait for background thread to finish
                While (frmCheckList.startupWorker.IsBusy)
                    Application.DoEvents()
                End While
                frmSysMon.ETITimer.Enabled = True
            Case 4
                TestName = "PCI-MXI Controller"
                StartupTestIndex = 4
                frmCheckList.startupWorker.RunWorkerAsync()
                While (frmCheckList.startupWorker.IsBusy)
                    Application.DoEvents()
                End While
            Case 5
                TestName = "MXI-VXI Resource Manager"
                StartupTestIndex = 5
                frmCheckList.startupWorker.RunWorkerAsync()
                While (frmCheckList.startupWorker.IsBusy)
                    Application.DoEvents()
                End While
            Case 6
                TestName = "System Monitoring Units"
                ''Enable chassis status polling timer
                frmSysMon.tmrDataPoll.Enabled = True
                frmSysMon.Show()
                Dim origLocation As New Point(frmSysMon.Location)
                Dim leftCorner As New Point(1280, 1024)
                Dim centerscreen As New Point(320, 256)
                'frmSysMon.Location = leftCorner
                frmSysMon.Refresh()
                TimerPollingEvent() 'force initial polling event
                frmSysMon.Refresh()
                'frmSysMon.Location = centerscreen
                frmSysMon.Refresh()
                frmSysMon.Hide()
                Application.DoEvents()
                StartupTestIndex = 6
                frmCheckList.startupWorker.RunWorkerAsync()
                While (frmCheckList.startupWorker.IsBusy)
                    Application.DoEvents()
                End While
                'Hide ChecKlist if restart
                If (RestartSysmonFlag = True) Then
                    frmCheckList.Hide()
                End If

                frmToolbar.Hide() 'Activate Tooltray Icon

            Case 7
                UpdateLogFile() 'Write startup header to log
                TestName = "System Survey"
                StartupTestIndex = 7
                frmCheckList.startupWorker.RunWorkerAsync()
                While (frmCheckList.startupWorker.IsBusy)
                    Application.DoEvents()
                End While
            Case 8
                TestName = "System Calibration Period"
                StartupTestIndex = 8
                frmCheckList.startupWorker.RunWorkerAsync()
                While (frmCheckList.startupWorker.IsBusy)
                    Application.DoEvents()
                End While
        End Select

        'Make sure temperature thesholds have not be overwritten by form loads
        GetTemperatureThresholds()

        'Finish Test
        If FailureMessage = "" Then 'Test Passed
            PassTest(TestIndex, True)
            FormatResultLine(TestName, True)
            SendStatusBarMessage(TestName & " PASSED")

        ElseIf FailureMessage = "Invalid System Configuration Detected. Setup Called" Then
            Dim lpFileName = Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\ATS\", "IniFilePath", Nothing)
            WritePrivateProfileString("System Startup", "SN", "", lpFileName)
            Dim newSysmonProcess As New Process()
            newSysmonProcess.StartInfo.FileName = "C:\Program Files (x86)\ATS\Sysmon.exe"
            newSysmonProcess.StartInfo.Arguments = "-ReSetup"
            newSysmonProcess.Start()
            Environment.Exit(0)

        Else            'Test Failed
            PassTest(TestIndex, False)
            FormatResultLine(TestName, False)
            Echo("      " & FailureMessage)
            SendStatusBarMessage(TestName & "FAILED")
            If TestIndex = 7 Then
                CtestFailed = True
            End If

        If DebugDwh <> True Then 'IF Debugging is ON then do not fail out
                If (TestIndex <> 7) And (TestIndex <> 8) Then 'Exclude CTEST and Cal Check
                    SendBuffer = "An error occurred that prevents the system from starting." & vbCrLf & vbCrLf
                    MsgBox(SendBuffer & FailureMessage, MsgBoxStyle.Critical, "Startup Error")
                    For FailLoop = TestIndex To 8
                        PassTest(FailLoop, False)
                    Next FailLoop
                    frmCheckList.tmrFlash.Enabled = False 'Stop Timer
                    Delay(0.5)
                    frmCheckList.InstrumentLabel(TestIndex).ForeColor = System.Drawing.Color.FromArgb(&H0) 'Reset Label Color
                    Delay(0.5)
                    TestIndex = 8
                    frmSysMon.Close()
                    StartUpFailFlag = True
                    If FailureMessage = "FPU or PPU is faulted" Then
                        'write failure data to FHDB
                        WriteData(CStr(Now), CStr(Now), "Sysmon Startup", , "Start FPU", "START EVEN FAILED:" & vbCrLf & "FPU or PPU is faulted at Address: " & ActModAddr & ".  This is a fatal error which must be fixed in order to run ATS.", , " ", , , " ")
                        MsgBox("FPU or PPU Modual at Address: " & ActModAddr & " is faulted." & vbCrLf & "The ATS System should be shutdown until the problem is repaired." & vbCrLf & "An entry as been made in the FHDB", MsgBoxStyle.OkOnly)
                        'ShutDownSysmon
                    End If
                    Exit Sub
                End If
            End If

        End If

        frmCheckList.tmrFlash.Enabled = False 'Stop Timer
        Delay(0.4)
        frmCheckList.InstrumentLabel(TestIndex).ForeColor = System.Drawing.Color.FromArgb(&H0) 'Reset Label Color
        'Delay 0.5
        frmCheckList.Refresh() 'Allow label to return to black color

    End Sub


    Sub UpdateLogFile()

        Dim FileHandle As Integer
        Dim ProgramPath As String
        Dim syslogFileHandle As Integer
        On Error Resume Next

        'Inhibit Logging in factory mode
        If SING_CHASSIS_OPTION Then Exit Sub

        'Write Log File
        FileHandle = FreeFile()
        FileOpen(FileHandle, LogFile, OpenMode.Append)
        If Err.Number Then
            MsgBox("Error writing to log file, results will not be logged.")
        Else
            PrintLine(FileHandle, EchoText)
            FileClose(FileHandle)
        End If
        Delay(0.1) 'ensure that the file has been written
        Err.Clear()
        Dim logfileInfo As New FileInfo(LogFile)
        Dim syslogFileName As String = logfileInfo.DirectoryName.ToString() & "\SYSLOG.TXT"
        syslogFileHandle = FreeFile()
        FileOpen(syslogFileHandle, syslogFileName, OpenMode.Append)
        PrintLine(syslogFileHandle, EchoText)
        FileClose(syslogFileHandle)
        'RetValue = Shell(Q & SystemLogFilePath & Q & " " & LogFile, AppWinStyle.NormalFocus)
        If Err.Number Then
            MsgBox("Error accessing system log program, results will not be logged.")
        End If
        Err.Clear()
        EchoText = "" 'Clear echo string

    End Sub




    
    Sub WriteChassisStateToIniFile(ByVal State As Short)

        
        Dim ReturnValue As Integer 'API Returned Eror Code
        Dim lpFileName As String 'File to write to (VIPERT.INI)
        Dim lpKeyName As String 'KEY= in INI file
        Dim lpDefault As String 'Default value if key not found
        Dim lpApplicationName As String '[Application] in INI file

        lpFileName = Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\ATS\", "IniFilePath", Nothing)

        'Init File
        lpKeyName = "CHASSIS_STATE"
        If State = True Then
            lpDefault = "ON"
        Else
            lpDefault = "OFF"
        End If

        lpApplicationName = "System Monitor"
        ReturnValue = WritePrivateProfileString(lpApplicationName, lpKeyName, lpDefault, lpFileName)

    End Sub




    
    Public Sub FormatResultLine(ByVal vInstrumentID_String As String, Optional ByVal bResult As Boolean = False)
        '******************************************************************************************
        '***        Formats the Final result text of the individual test. If the ID is an       ***
        '***        Index, the Instrument Name is used. If it's not numeric, to string value    ***
        '***        is used.                                                                    ***
        '***        Example:(Instrument Name).................................PASSED            ***
        '******************************************************************************************

        
        Dim iLineLen As Integer 'Length of the Text Line
        Dim Index As Short 'Index for loop
        Dim sLineOut As String = "" 'Final Text Line Out
        Dim sStatus As String 'PASSED/FAILED Identifier
        Dim sText As String 'Instrument Description
        Dim bNoBlankLine As Boolean 'Blank Line Switch

        bNoBlankLine = False

        If IsNumeric(vInstrumentID_String) Then
        Else
            bNoBlankLine = False
            sText = vInstrumentID_String
        End If

        iLineLen = Len(sText) 'Find out the Length of the Text
        If bResult = False Then 'Define Pass/Fail Status
            sStatus = "FAILED"
        Else
            sStatus = "PASSED" 'PASSED/FAILED = 6 characters
        End If

        For Index = 1 To 64 Step 1

            If Index >= 1 And Index <= iLineLen Then
                'Print Character
                sLineOut &= Mid(sText, (Index), 1)
            Else
                If Index > iLineLen And Index < 64 Then
                    If bNoBlankLine Then
                        sLineOut &= " "
                    Else
                        sLineOut &= "."
                    End If
                End If
            End If
        Next

        Echo(sLineOut & sStatus)

    End Sub



    Function ExecCmd(ByVal sCmdLine As String) As Integer
        Dim Proc As PROCESS_INFORMATION
        Dim START As STARTUPINFO
        Dim Ret As Integer

        'Initialize the STARTUPINFO structure
        START.cb = Len(START)
        START.dwFlags = STARTF_USESHOWWINDOW 'Enable wShowWindow flags
        START.wShowWindow = SW_HIDE 'Hide the Console window

        'Start the shelled application
        Ret = CreateProcessA(vbNullString, sCmdLine, 0, 0, 1, NORMAL_PRIORITY_CLASS, 0, vbNullString, START, Proc)

        'Wait for the shelled application to finish
        If Ret <> 0 Then 'If successful
            WaitForSingleObject(Proc.hProcess, INFINITE)
            GetExitCodeProcess(Proc.hProcess, ExecCmd)
            CloseHandle(Proc.hThread)
            CloseHandle(Proc.hProcess)
        Else
            ExecCmd = -99
        End If

    End Function




    Function bFindInFile(ByVal sFile As String, ByVal sStr As String) As Boolean
        'DESCRIPTION:
        '   This function searches for a string in a file
        'PARAMTERS:
        '   sFile:  The filename (with path if necessary) of the file to search
        '   sStr:   The string to search for in the file
        'RETURNS:
        '   True if found
        '   False if not found or file error

        
        Dim iFileNum As Integer
        Dim sLine As String = ""

        bFindInFile = False
        Try ' On Error GoTo bFindInFileError
            iFileNum = FreeFile()
            FileOpen(iFileNum, sFile, OpenMode.Input)
            Do
                sLine = LineInput(iFileNum)
                If InStr(sLine, sStr) Then
                    bFindInFile = True
                    Exit Do
                End If
            Loop While (Not EOF(iFileNum))
            FileClose(iFileNum)
            Exit Function

        Catch   ' bFindInFileError:
            bFindInFile = False

        End Try
    End Function


    Sub RemoveNonRestartChecklistItems()
        'frmCheckList.PassFrame_6.Visible = False
        frmCheckList.PassFrame_7.Visible = False
        frmCheckList.PassFrame_8.Visible = False
        'frmCheckList.FailFrame_6.Visible = False
        frmCheckList.FailFrame_7.Visible = False
        frmCheckList.FailFrame_8.Visible = False
        'frmCheckList.Icon_6.Visible = False
        frmCheckList.Icon_7.Visible = False
        frmCheckList.Icon_8.Visible = False
        'frmCheckList.InstrumentLabel_6.Visible = False
        frmCheckList.InstrumentLabel_6.Text = "Restart System Monitoring"
        frmCheckList.InstrumentLabel_7.Visible = False
        frmCheckList.InstrumentLabel_8.Visible = False
        Dim newSize As New Size(349, 170)
        frmCheckList.panStartup.Size = newSize
        newSize.Width = 389
        newSize.Height = 250
        frmCheckList.Size = newSize
        frmCheckList.Text = "ATS Mini-Startup"
    End Sub


     Function CheckSystemConfiguration() As Boolean

        Dim dsoSession As Integer = 0
        Dim retCount As Integer = 0
        Dim manfId As Integer = 0
        Dim modelCode As Integer = 0
        Dim detectedSystemType As String
        Dim currentSystemType As String = Space(256)
        Dim session As Integer



        currentSystemType = GatherIniFileInformation("System Startup", "SYSTEM_TYPE", "")
        'open new resource manager session
        viOpenDefaultRM(session)

        'open visa session and get manf id and model codes for the DSO stimulus intruments
        If (viOpen(RmSession, "VXI0::17::INSTR", 0, 10, dsoSession) = 0) Then

            viGetAttribute(dsoSession, VI_ATTR_MANF_ID, manfId)
            viGetAttribute(dsoSession, VI_ATTR_MODEL_CODE, modelCode)

            'Check Device and Manf Id for Instrument to Determine System Configuration Type
            If (manfId = TETS_DSO_MANF_ID And modelCode = TETS_DSO_MODEL_CODE) Then
                detectedSystemType = "AN/USM-657(V)2"
            ElseIf (manfId = VIPERT_DSO_MANF_ID And modelCode = VIPERT_DSO_MODEL_CODE) Then
                Dim serialNo = GatherIniFileInformation("System Startup", "SN", "")

                If CInt(serialNo) > 1000 And CInt(serialNo) < 2000 Then
                    detectedSystemType = "AN/USM-717(V)2"
                ElseIf CInt(sSerial_No) > 2000 And CInt(sSerial_No) < 3000 Then
                    detectedSystemType = "AN/USM-717(V)3"
                End If
            End If
        End If


        'viGetAttribute(rfSession, VI_ATTR_MANF_ID, manfId)
        'viGetAttribute(rfSession, VI_ATTR_MODEL_CODE, modelCode)

        'Check Device and Manf Id for Instrument to Determine System Configuration Type
        'If (manfId = TETS_RF_MANF_ID And modelCode = TETS_RF_MODEL_CODE) Then
        '    detectedSystemType = "AN/USM-657(V)2"
        'ElseIf (manfId = VIPERT_RF_MANF_ID And modelCode = VIPERT_RF_MODEL_CODE) Then
        '    detectedSystemType = "AN/USM-717(V)2"
        'Else
        '    detectedSystemType = "AN/USM-717(V)3"
        'End If


        If (detectedSystemType = currentSystemType) Then
            CheckSystemConfiguration = True
        Else
            CheckSystemConfiguration = False
        End If

        viClose(dsoSession)

        Return CheckSystemConfiguration

    End Function



End Module