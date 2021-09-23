'Option Strict Off
'Option Explicit On

Imports System
Imports System.Windows.Forms
Imports System.Windows.Forms.Screen
Imports System.Text
Imports System.Math
Imports System.Runtime.InteropServices
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Imports Scripting
Imports NationalInstruments.VisaNS
Imports System.Threading
Imports System.Net.NetworkInformation
Imports Microsoft.Win32
Imports System.ServiceProcess
Imports System.Security
Imports System.Management
Imports System.Text.RegularExpressions

Public Module SysmonMain


    '=========================================================
    '**************************************************************
    '**************************************************************
    '**                                                          **
    '** Nomenclature  : System Monitor  [SysmonMain]             **
    '** Version       : 1.16                                     **
    '** Last Update   : 05/21/04                                 **
    '** Purpose       : This module is the controlling system    **
    '**                 application for the VIPERT.  This is the   **
    '**                 startup application.                     **
    '** Program Begins Executing Instructions In Sub:MAIN        **
    '**************************************************************
    '** Change History:                                          **
    '** DME UPDATE PCR VSYS-7 POWEROFF SYSTEM - has been applied **
    '**    to the below code.  Useing ExitWindowsEX instead of   **
    '**    InitiateSystemShutdown.  Added 1 function and         **
    '**    supporting constants and type file.  Also changed the **
    '**    API call to use ExitWindowsEX.                        **
    '**************************************************************

    Private Structure LUID

    Dim UsedPart As Integer

    Dim IgnoredForNowHigh32BitPart As Integer

    End Structure

    Private Structure TOKEN_PRIVILEGES

        Dim PrivilegeCount As Integer

        Dim TheLuid As LUID

        Dim Attributes As Integer

    End Structure

    Public FPU_FORCE_SD As Boolean
    Public FPU_IgnoreFaults As Boolean 'flag to ignore fault conditions
    Public FPU_faulted As Boolean 'fpu fault flag
    Public ShutdownFlag As Short
    Public TypeOfShutdownValue As Integer
    Public HeaterAdjustInhibitFlag As Short
    Public SING_CHASSIS_OPTION As Short
    Public SHUTDOWN_FROM_SYSMON As Boolean = False
    Public LOGOUT_FROM_SYSMON As Boolean = False
    Public SHUTTING_DOWN As Boolean = False
    Public RestartSysmonFlag As Boolean = False
    Public ReSetupFlag As Boolean = False

    '-----------------API / DLL Declarations------------------------------
    Declare Function GetPrivateProfileString Lib "kernel32" Alias "GetPrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpDefault As String, ByVal lpReturnedString As StringBuilder, ByVal nSize As Integer, ByVal lpFileName As String) As Integer
    Declare Function WritePrivateProfileString Lib "kernel32" Alias "WritePrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpString As String, ByVal lpFileName As String) As Integer
    Declare Function EnumWindows Lib "user32" (ByVal lpEnumFunc As Integer, ByVal lParam As Integer) As Integer
    Declare Function GetWindowTextLength Lib "user32" Alias "GetWindowTextLengthA" (ByVal hWnd As Integer) As Integer
    Declare Function GetWindowText Lib "user32" Alias "GetWindowTextA" (ByVal hWnd As Integer, ByVal lpString As String, ByVal cch As Integer) As Integer
    Declare Function GetTokenInformation Lib "advapi32.dll" (ByVal TokenHandle As Integer, ByRef TokenInformationClass As Short, ByRef TokenInformation As System.Delegate, ByVal TokenInformationLength As Integer, ByRef ReturnLength As Integer) As Integer
    Declare Function GetCurrentProcess Lib "kernel32" () As Integer
    Declare Function SHShutDownDialog Lib "shell32" Alias "#60" (ByVal lType As Integer) As Integer
    Declare Function ExitWindowsEx Lib "user32" (ByVal dwOptions As Int32, ByVal dwReserved As Int32) As Int32
    Private Declare Function OpenProcessToken Lib "advapi32" (ByVal ProcessHandle As IntPtr, ByVal DesiredAccess As Integer, ByRef TokenHandle As Integer) As Integer
    Private Declare Function LookupPrivilegeValue Lib "advapi32" Alias "LookupPrivilegeValueA" (ByVal lpSystemName As String, ByVal lpName As String, ByRef lpLuid As LUID) As Integer
    Private Declare Function AdjustTokenPrivileges Lib "advapi32" (ByVal TokenHandle As Integer, ByVal DisableAllPrivileges As Boolean, ByRef NewState As TOKEN_PRIVILEGES, ByVal BufferLength As Integer, ByRef PreviousState As TOKEN_PRIVILEGES, ByRef ReturnLength As Integer) As Integer


    'functions and constants for having messages always on top
    Public Const SWP_NOMOVE As Short = 2
    Public Const SWP_NOSIZE As Short = 1
    Public Const flags As Boolean = SWP_NOMOVE Or SWP_NOSIZE
    Public Const HWND_TOPMOST As Short = -1
    Public Const HWND_NOTOPMOST As Short = -2
    Public Const BUFFER_SIZE As Short = 256

    Declare Function SetWindowPos Lib "user32" (ByVal hWnd As Integer, ByVal hWndInsertAfter As Integer, ByVal X As Integer, ByVal Y As Integer, ByVal cx As Integer, ByVal cy As Integer, ByVal wFlags As Integer) As Integer

    'function to comunicate with the DTI
    Declare Function terM9_init Lib "terM9_32.dll" (ByVal rsrcName As String, ByVal idQuery As Short, ByVal resetInstr As Short, ByRef vi As Integer) As Integer
    Declare Function terM9_setChannelConnect Lib "terM9_32.dll" (ByVal vi As Integer, ByVal pinListCount As Integer, ByRef pinList As Integer, ByVal connect As Integer) As Integer
    Declare Function terM9_reset Lib "terM9_32.dll" (ByVal vi As Integer) As Integer
    Declare Function terM9_close Lib "terM9_32.dll" (ByVal vi As Integer) As Integer


    Structure SYSTEMTIME
        Dim wYear As Short
        Dim wMonth As Short
        Dim wDayOfWeek As Short
        Dim wDay As Short
        Dim wHour As Short
        Dim wMinute As Short
        Dim wSecond As Short
        Dim wMilliseconds As Short
    End Structure

    Structure TIME_ZONE_INFORMATION
        Dim Bias As Integer
        Dim StandardName() As Short
        Dim StandardDate As SYSTEMTIME
        Dim StandardBias As Integer
        Dim DaylightName() As Short
        Dim DaylightDate As SYSTEMTIME
        Dim DaylightBias As Integer

        Public Sub New(ByVal unusedParam As Integer)
            ReDim StandardName(32)
            ReDim DaylightName(32)
        End Sub
    End Structure

    Declare Function GetTimeZoneInformation Lib "kernel32" (ByRef lpTimeZoneInformation As TIME_ZONE_INFORMATION) As Integer
    Declare Sub GetLocalTime Lib "kernel32" (ByRef lpSystemTime As SYSTEMTIME)
    Declare Sub GetSystemTime Lib "kernel32" (ByRef lpSystemTime As SYSTEMTIME)
    Declare Function GetParent Lib "user32" (ByVal hWnd As Integer) As Integer
    Declare Function SetForegroundWindow Lib "user32" (ByVal hWnd As Integer) As Integer
    Declare Function SetFocus Lib "user32" (ByVal hWnd As Integer) As Integer
    Public lpEnumFunc As Integer 'Callback Address
    Public ResmanRunning As Short 'Callback Flag
    '-----------------User Defined Types----------------------------------


    Structure ChassisData
        Dim Action1 As Integer
        Dim SlotCurrent() As Single
        Dim SlotVoltage() As Single
        Dim SlotPolarity() As Boolean ' Normal=True, Reversed=False
        Dim UutPowerTotal As Single
        Dim StatusICPU As Integer
        Dim P5ACurrent As Single
        Dim P5BCurrent As Single
        Dim P12ACurrent As Single
        Dim P12BCurrent As Single
        Dim P24ACurrent As Single
        Dim P24BCurrent As Single
        Dim N2ACurrent As Single
        Dim N2BCurrent As Single
        Dim N52ACurrent As Single
        Dim N52BCurrent As Single
        Dim N24ACurrent As Single
        Dim N24BCurrent As Single
        Dim N12ACurrent As Single
        Dim N12BCurrent As Single
        Dim P5AVoltage As Single
        Dim P5BVoltage As Single
        Dim P12AVoltage As Single
        Dim P12BVoltage As Single
        Dim P24AVoltage As Single
        Dim P24BVoltage As Single
        Dim N2AVoltage As Single
        Dim N2BVoltage As Single
        Dim N52AVoltage As Single
        Dim N52BVoltage As Single
        Dim N24AVoltage As Single
        Dim N24BVoltage As Single
        Dim N12AVoltage As Single
        Dim N12BVoltage As Single
        Dim FpuPowerTotal As Single
        Dim Action2 As Integer
        Dim ChassisStatus As Integer
        Dim FanSpeed As Short
        Dim Temperature() As Single
        Dim IntakeTemperature As Single
        Dim ChP24V As Single
        Dim ChP12V As Single
        Dim ChP5V As Single
        Dim ChN2V As Single
        Dim ChN52V As Single
        Dim ChN12V As Single
        Dim ChN24V As Single
        Dim VinSinglePhase As Single
        Dim VinThreePhase As Single
        Dim CurrentIn As Single
        Dim PowerOk As Integer
        Dim DcvLevel As Single
        Dim TempRisePerSlot() As Single
        Dim StatusInputPower As Integer

        Public Sub New(ByVal unusedParam As Integer)
            ReDim SlotCurrent(10)
            ReDim SlotVoltage(10)
            ReDim SlotPolarity(10)
            ReDim Temperature(12)
            ReDim TempRisePerSlot(12)
        End Sub
    End Structure
    '-----------------Global Variables------------------------------------
    Public RmSession As Integer
    Public GpibControllerHandle0 As Integer
    Public GpibControllerHandle11 As Integer
    Public ChassisControllerHandle1 As Integer
    Public ChassisControllerHandle2 As Integer
    Public GpibControllerHandle15 As Integer
    Public PpuControllerHandle(10) As Integer
    Public Addr As Integer
    Public RetValue As Integer
    Public PrimaryChassis As New ChassisData(0)
    Public SecondaryChassis As New ChassisData(0)
    Public Path_File As String 'For Probe / Status Byte File
    Public TempertureThreshold(25) As Single
    Public RcvrActivated As Short 'Receiver Handle Latch
    Public InputPower As Short 'Power Budget Variable
    Public HeaterPower As Short 'Power Budget Variable
    Public FanPower As Short 'Power Budget Variable
    Public UserPowerConsumption As Short 'Power Budget Variable
    Public TotalPowerUsage As Short 'Power Budget Variable
    Public DcInputCurrent As Short 'Power Budget Variable
    Public StatHeater2 As Short
    Public StatHeater1 As Short
    Public FpuStatModFault As Short 'FPU Status Byte Variable
    Public FpuStatOn As Short 'FPU Status Byte Variable
    Public FpuStatOffMismnatch As Short 'FPU Status Byte Variable
    Public FpuStatOverVolt As Short 'FPU Status Byte Variable
    Public FpuStatAddress As Short 'FPU Status Byte Variable
    Public FactoryDef(25) As String 'Factory Default Cooling Curve Thresholds
    Public UserDef(25) As String 'Factory Default Cooling Curve Thresholds
    Public Q As String 'To Hold the ASCII String 34 (The Quote Symbol)
    '-----------------Global Constants For the POWER Budget---------------
    Public Const HEATER_RES As Single = 3.145 'The Resistance in Ohms of 1  Heating Unit
    'Assuming that the (Worst Case) Heater Resistance is 3.145 Ohms and The Max Voltage is
    '28 VOlts During Single/Three Phase AC Imput Power Mode and 17-33V in DC Mode
    'therefore the power dissipated by the heating units will be (V*V/R)
    'this gives us about 249 to 346 Watts
    Public Const POWER_MARGIN As Short = 10 'The Power Safety Margin Threshold Constant in Watts
    Public Const FPU_INPUT_POWER_EFFICENCY As Single = 0.7 'Input Power Efficency Factor in decimal
    Public Const PPU_INPUT_POWER_EFFICENCY As Single = 0.8 'Input Power Efficency Factor in decimal
    Public Const FAN_SPEED_POWER As Single = 0.75 'The Power in Watts for each fan speed percentage amount
    Public Const FAN_PDU_MINIMUM_DRAW As Short = 58 'The Power In Watts that fans/IC/Misc draw when active
    Public Const PPU_PRELOAD_CURRENT As Single = 0.025 'Current Per Volt consumed by an unloaded PPU supply
    Public Const IC_POWER As Short = 60 'Instrument Controller Power Disspiation in Watts
    Public Const SE_PRIVILEGE_ENABLED As Short = &H2
    Public Const SE_SHUTDOWN_NAME As String = "SeShutdownPrivilege"
    Public Const SE_DEBUG_NAME As String = "SeDebugPrivilege"
    Public Const SECS_IN_DAY As Integer = 86400

    '-----------------Data types and Constants For CICL Handling------------------

    Structure PROCESS_INFORMATION
        Dim hProcess As Integer
        Dim hThread As Integer
        Dim dwprocessID As Integer
        Dim dwThreadId As Integer
    End Structure


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

    Const SYNCHRONIZE As Integer = 1048576
    Const NORMAL_PRIORITY_CLASS As Short = &H20
    Const SW_HIDE As Short = 0
    Const STARTF_USESHOWWINDOW As Short = &H1

    'functions and types to close applications
    Public pInfo As PROCESS_INFORMATION
    Public sInfo As STARTUPINFO
    Public sNull As String

    Declare Function CreateProcess Lib "kernel32" Alias "CreateProcessA" (ByVal lpApplicationName As String, ByVal lpCommandLine As String, ByRef lpProcessAttributes As Integer, ByRef lpThreadAttributes As Integer, ByVal bInheritHandles As Integer, ByVal dwCreationFlags As Integer, ByRef lpEnvironment As Integer, ByVal lpCurrentDriectory As String, ByRef lpStartupInfo As STARTUPINFO, ByRef lpProcessInformation As PROCESS_INFORMATION) As Integer

    Declare Function OpenProcess Lib "kernel32.dll" (ByVal dwAccess As Integer, ByVal fInherit As Short, ByVal hObject As Integer) As Integer

    Declare Function TerminateProcess Lib "kernel32" (ByVal hProcess As Integer, ByVal uExitCode As Integer) As Integer

    Declare Function CloseHandle Lib "kernel32" (ByVal hObject As Integer) As Integer
    Declare Function CreateToolhelpSnapshot Lib "kernel32" Alias "CreateToolhelp32Snapshot" (ByVal lFlags As Integer, ByRef lProcessID As Integer) As Integer
    Declare Function ProcessFirst Lib "kernel32" Alias "Process32First" (ByVal hSnapshot As Integer, ByRef uProcess As PROCESSENTRY32) As Integer
    Declare Function ProcessNext Lib "kernel32" Alias "Process32Next" (ByVal hSnapshot As Integer, ByRef uProcess As PROCESSENTRY32) As Integer
    Declare Function GetVersionExA Lib "kernel32" (ByRef lpVersionInformation As OSVERSIONINFO) As Short


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
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=260), VBFixedString(260)> Dim szExeFile As String
    End Structure

    Const PROCESS_ALL_ACCESS As Short = 0
    Const TH32CS_SNAPPROCESS As Integer = 2

    Const WINNT As Short = 2
    Const WIN98 As Short = 1
    Public KillAppReturn As Boolean


    '-----------------Local Module Variables------------------------------
    Dim ActChassisAddress As Short
    Public ActVolt28Ok As Short
    Dim ActProbeEvent As Short
    Dim ActReceiverEvent As Short

    Public ActModFault As Short 'Bit 5 of Action byte: 1 if PPU/FPU module is faulted; 0 if all is ok
    Public ActModAddr As Short 'Last 4 bits of action byte when bit 5 = 1, address of faulted fpu/ppu
    Public ModFaultDet As Boolean 'flag so message isn't displayed more than once

    Public PDUerrCount As Short 'counter for 3 consecutive PDU input power faults
    Public TimePolCount As Short 'counter for timer polling event to reset PPUerrCounter

    Public StatSelfTest As Short
    Public StatAirFlowSensor3 As Short
    Public StatAirFlowSensor2 As Short
    Public StatAirFlowSensor1 As Short
    Public PowerStatus28FailHigh As Short 'POWER Status Byte Variable
    Public PowerStatusAc As Short 'POWER Status Byte Variable AC MODE
    Public PowerStatusDc As Short 'POWER Status Byte Variable DC MODE
    Public PowerStatusSingle As Short 'POWER Status Byte Variable
    Public PowerStatusPhase1 As Short 'POWER Status Byte Variable
    Public PowerStatusPhase2 As Short 'POWER Status Byte Variable
    Public PowerStatusPhase3 As Short 'POWER Status Byte Variable
    Public PowerStatus2800Watt As Short 'POWER Status Byte Variable
    Public PowerSwitch As Short 'Latches The FPU "Power On" Command
    Public AllPowerOff As Short 'Shows ALL FPU Voltages at 0 or NOT
    Public SysWaitTimeout As Integer 'System Wait Dialog Timeout
    '-----------------Global Arrays---------------------------------------
    '-----------------Input Text Box / Spin Button Instruction -----------
    Public Const FAN_PRIMARY As Short = 1
    Public Const FAN_SECONDARY As Short = 2
    Public Const OPT_TEMP_SECONDARY As Short = 4
    Public Const OPT_TEMP_PRIMARY As Short = 3
    Public Const MAX_SETTINGS As Short = 4
    Public RangeDisplay As Short
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
    Public Const SAFE As Short = 0 'Safe to turn on heaters
    Public Const PRI As Short = 1 'Primary Chassis Indicator
    Public Const Sec As Short = 2 'Secondary Chassis Indicator
    Public Const NOHEAT As Single = 11 'Temperature required to Inhibit Heater from comming on.


    Structure POINTAPI
        Dim X As Integer
        Dim Y As Integer
    End Structure

    Structure msg
        Dim hWnd As Integer
        Dim Message As Integer
        Dim wParam As Integer
        Dim lParam As Integer
        Dim time As Integer
        Dim pt As POINTAPI
    End Structure


    Structure POINT_TYPE
        Dim X As Integer
        Dim Y As Integer
    End Structure

    Public Declare Function GetQueueStatus Lib "user32" (ByVal fuFlags As Integer) As Integer
    Declare Function DispatchMessage Lib "user32.dll" Alias "DispatchMessageA" (ByRef lpMsg As msg) As Integer
    Declare Function GetCursorPos Lib "user32.dll" (ByRef lpPoint As POINTAPI) As Integer

    Private Declare Function FormatMessage Lib "kernel32" Alias "FormatMessageA" (ByVal dwFlags As Integer, ByRef lpSource As Integer, ByVal dwMessageId As Integer, ByVal dwLanguageId As Integer, ByVal lpBuffer As String, ByVal nSize As Integer, ByRef Arguments As Integer) As Integer

    Private Const TOKEN_QUERY As Short = (&H8)
    Private Const TOKEN_ADJUST_PRIVILEGES As Short = (&H20)

    'DME UPDATE PCR VSYS-7
    Private Declare Function GetVersionEx Lib "kernel32" Alias "GetVersionExA" (ByRef lpVersionInformation As OSVERSIONINFO) As Integer

    Const FORMAT_MESSAGE_FROM_SYSTEM As Short = &H1000

    Private Const EWX_LOGOFF As Integer = &H0
    Private Const EWX_SHUTDOWN As Integer = &H1
    Private Const EWX_REBOOT As Integer = &H2
    Private Const EWX_FORCE As Integer = &H4
    Private Const EWX_POWEROFF As Integer = &H8
    Private Const EWX_FORCEIFHUNG As Integer = &H10 '2000/XP only

    Private Const ANYSIZE_ARRAY As Short = 1
    Private Const VER_PLATFORM_WIN32_NT As Short = 2


    Structure OSVERSIONINFO 'this should already be there for isNT
        Dim dwOSVersionInfoSize As Integer
        Dim dwMajorVersion As Integer
        Dim dwMinorVersion As Integer
        Dim dwBuildNumber As Integer
        Dim dwPlatformId As Integer
        Dim szCSDVersion As StringBuilder

        Public Sub New(ByVal unusedParam As Integer)
            szCSDVersion = New StringBuilder(Space(128), 128)
        End Sub
    End Structure
    '_END DME UPDATE PCR VSYS-7______

    'added for ethernet fix tool, pcr vsys-351
    'Public Declare Function OpenProcess Lib "kernel32" (ByVal dwDesiredAccess As Long, ByVal bInheritHandle As Long, ByVal dwprocessID As Long) As Long
    Public Declare Function GetExitCodeProcess Lib "kernel32" (ByVal hProcess As Integer, ByRef lbExitCode As Integer) As Integer

    Const PROCESS_QUERY_INFORMATION As Short = &H400
    Const STILL_ACTIVE As Short = &H103

    Public CEPFlag As Boolean

    Public Const GBit1IP As String = "192.168.0.1"
    Public Const GBitMask As String = "255.255.255.0"
    Public Const GBit1DG As String = "192.168.0.2"
    Public Const GBit2IP As String = "192.168.200.1"
    Public Const GBit2DG As String = "192.168.200.2"
    Public Const LACIP As String = "192.168.10.1"
    Public Const LACDG As String = "192.168.10.2"
    Public Const GBit4IP As String = "192.168.20.1"
    Public Const GBit4DG As String = "192.168.20.2"

    'Global variables for ETI data's previous entry
    Public PreT1P As Integer
    Public PreT3P As Integer
    Public PreTDC As Integer
    Public PreTTotal As Integer


    'Globals for power change
    Public InputPowerByte As Single 'Data Dump Byte Containing Input Information
    Public Input1PhaseByte As Single 'Data Dump Byte Containing Input Information
    Public Input3PhaseByte As Single 'Data Dump Byte Containing Input Information



    Sub CommandSetOptions(ByVal Supply As Short, ByVal OpenRelay As Short, ByVal SetMaster As Short, ByVal SenseLocal As Short, ByVal CurrentLimit As Short, ByVal ReversePolarity As Short)
        '************************************************************
        '************************************************************
        '* Nomenclature   : VIPERT SYSTEM MONITOR                     *
        '*    DESCRIPTION:                                          *
        '*     Sets options of selected supply                      *
        '*      a +1= no action, a -1=true and a 0 = false          *
        '*    EXAMPLE:                                              *
        '*       CommandSetOptions EditSupply%, -1, 1, 0, -1, 0     *
        '************************************************************

        ' Add Procedure to "bleed" Capacitance from PPU Connection Paths

        Dim B1 As Short
        Dim B2 As Short
        Dim B3 As Short
        Dim retCount As Integer
        Dim SendBuffer As String = Space(BUFFER_SIZE)

        B1 = 32 + ObjectToDouble(Supply)

        'Note: If a +1 is passed do nothing
        Select Case OpenRelay
            Case True
                '(-1)
                B2 += 32 'Enable / Relay Open
            Case False
                '( 0)
                B2 += 32 'Enable
                B2 += 16 'Relay Closed
        End Select
        Select Case SetMaster
            Case True
                '(-1)
                B2 += 8 'Enable /Set as Master
            Case False
                '( 0)
                B2 += 4 'Set as Slave
                B2 += 8 'Enable
        End Select
        Select Case SenseLocal
            Case True
                '(-1)
                B2 += 2 'Enable / Sense Local
            Case False
                '( 0)
                B2 += 2 'Enable
                B2 += 1 'Sense Remote
        End Select
        Select Case CurrentLimit
            Case True
                '(-1)
                B2 += 128
                B3 += 32 'Enable / Current Limiting
            Case False
                '( 0)
                B2 += 128
                B3 += 16 'Constant Current
                B3 += 32 'Enable
        End Select
        Select Case ReversePolarity
            Case True
                '(-1)
                B2 += 128
                B3 += 1 'Set to reverse polarity
                B3 += 2 'Enable
            Case False
                '( 0)
                B2 += 128
                B3 += 2 'Set to Normal polarity
        End Select

        'Send Command
        SendBuffer = Convert.ToString(Chr(B1)) & Convert.ToString(Chr(B2)) & Convert.ToString(Chr(B3))
        viWrite(PpuControllerHandle(Supply), SendBuffer, BUFFER_SIZE, retCount)

        Delay(0.1)

    End Sub




    Sub OnProbeSendEnter()
        '************************************************************
        '************************************************************
        '* Nomenclature   : VIPERT SYSTEM MONITOR                     *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     This Module will search for the ATLAS Probe Window.  *
        '*     If the window is present the System Monitor will     *
        '*     send an [Enter] key character.
        '*    EXAMPLE:                                              *
        '*     OnProbeSendEnter                                     *
        '************************************************************
        Dim CurrWnd As Integer 'Current Window Handle
        Dim Title As String 'Title of window found
        Dim WindowHandle As Integer 'Registersd Windows Operating System Handle
        Dim rc As Integer 'Return Value of Windows API Call
        Dim lpString As String
        Dim Ret As Integer 'Window Text Error Code
        Dim ParentHandle As Integer 'RTS Window Handle
        'Get First Window
        CurrWnd = GetWindow(My.Forms.frmToolbar.Handle, GW_HWNDFIRST)

        'Get Window Information for all Windows on the desktop
        Do While CurrWnd <> 0

            'Get Title of Window
            lpString = Space(255)
            Ret = GetWindowText(CurrWnd, lpString, 255)
            Title = Trim(lpString)

            'Check Window Title
            If Title <> Convert.ToString(Chr(0)) Then 'Disregard windows without titles

                'Check if the Probe Window is on the desktop
                'MsgBox Title$ 'Uncomment this line is for debugging
                If InStr(1, Title, "Boolean Input", Microsoft.VisualBasic.CompareMethod.Binary) <> 0 Then

                    'Get Window Parent Title
                    Ret = GetParent(CurrWnd)
                    lpString = Space(255)
                    ParentHandle = Ret
                    Ret = GetWindowText(Ret, lpString, 255)
                    Title = Trim(lpString)

                    'Verify that the Probe Window belongs to the PAWS Runtime System
                    'and not another application
                    If InStr(1, Title, "Run Time System", Microsoft.VisualBasic.CompareMethod.Binary) <> 0 Then
                        GoTo FoundIt 'Exit Loop
                    End If

                End If

            End If

            'Acquire the next Window on the Desktop
            CurrWnd = GetWindow(CurrWnd, GW_HWNDNEXT)

        Loop

FoundIt:

        'If the window was located, then send an enter key
        If CurrWnd <> 0 Then

            'BringToTop ParentHandle&
            'BringToTop CurrWnd&

            Ret = SetForegroundWindow(ParentHandle)

            Ret = SetForegroundWindow(CurrWnd) 'Set Probe Window to Foreground

            Ret = SetFocus(CurrWnd)

            Ret = SetFocus(CurrWnd)
            SendKeys.SendWait("{ENTER}") 'Send an Enter Key to the Probe Window
        End If

    End Sub



    Sub AdjustHeaters()

        Dim ChasSlot As Short
        Dim PriChassMin As Single
        Dim SecChassMin As Single
        Static ColdCount As Short
        Static CurrentMaxFanSpeed(2) As Short
        Static ResetChassisOnce As Short
        Static ResetChassisOnce2 As Short


        'Check for operator manual intervention
        If HeaterAdjustInhibitFlag = True Then Exit Sub
        '----------------End Version 1.8 Modification-----------------

        'Find Minimum Exhaust Temp
        PriChassMin = 100
        SecChassMin = 100
        For ChasSlot = 0 To 12
            If PrimaryChassis.Temperature!(ChasSlot) < PriChassMin Then
                PriChassMin = PrimaryChassis.Temperature!(ChasSlot)
            End If
            If SecondaryChassis.Temperature!(ChasSlot) < SecChassMin Then
                SecChassMin = SecondaryChassis.Temperature!(ChasSlot)
            End If
        Next ChasSlot

        If PrimaryChassis.IntakeTemperature < PriChassMin Then
            PriChassMin = PrimaryChassis.IntakeTemperature
        End If
        If SecondaryChassis.IntakeTemperature < SecChassMin Then
            SecChassMin = SecondaryChassis.IntakeTemperature
        End If


        '----------------------------------
        'Rules of Heating Control
        '----------------------------------
        '-10 to 0 C FPU Off both heaters on
        '0 C one heater off power up FPU
        '5 C heater off
        '----------------------------------


        If (PriChassMin <= 10) Or (SecChassMin <= 10) Then
            ColdCount += 1
            If ColdCount >= 30 Then '30 Timer Interval Damping Factor
                ColdCount = 0 'Reset Damping Timer
                'If there is a potential temperature sensor failure in the Primary Chassis,
                'Inhibit the Primay Chassis Heaters
                If VerifyTemperatureSensors(True) = PRI Then PriChassMin = NOHEAT

                'If there is a potential temperature sensor failure in the Secondary Chassis,
                'Inhibit the Secondary Chassis Heaters
                If VerifyTemperatureSensors(True) = Sec Then SecChassMin = NOHEAT

                'If Firmware Max Fan Speed is 100 reduce to 70
                If (CurrentMaxFanSpeed(1) = 0) Or (CurrentMaxFanSpeed(1) <> 70) Or (PrimaryChassis.FanSpeed > 70) Or (SecondaryChassis.FanSpeed > 70) Then
                    SetMaxFanSpeed(1, 70)
                    SetMaxFanSpeed(2, 70)
                    CurrentMaxFanSpeed(1) = 70
                End If
            Else
                Exit Sub
            End If
        Else
            ColdCount = 0 'Reset Damping Timer
            'If Firmware Max Fan Speed is 70 increase to 100
            If (CurrentMaxFanSpeed(1) <> 100) Then
                SetMaxFanSpeed(1, 100)
                SetMaxFanSpeed(2, 100)
                CurrentMaxFanSpeed(1) = 100
            End If
            Exit Sub 'Do not make any heater adjustments this Timer Polling Event
        End If

        'The SetHeater routine indexes the four heaters as 0 to 3

        'There is a possibility of exceeding the PDU power limits
        'by turning on a heating unit during a low temperture / high power case.
        If TotalPowerUsage < POWER_MARGIN Then 'Power Budget Exceeded By User
            'Extra Trap to inhibit heaters during high power conditions
            SetHeater(0, False)
            SetHeater(1, False)
            SetHeater(2, False)
            SetHeater(3, False)
        Else            'There is enough power remaining for heater use
            Select Case PriChassMin
                Case Is > 5
                    'Turn Off Heaters
                    If ResetChassisOnce <> True Then
                        ResetChassis(1)
                        ResetChassisOnce = True
                    End If
                    SetHeater(0, False)
                    SetHeater(1, False)
                Case 0 To 5
                    SetHeater(0, True)
                    SetHeater(1, False)
                Case (-10) To 0
                    SetHeater(0, True)
                    SetHeater(1, True)
            End Select

            Select Case SecChassMin
                Case Is > 5
                    'Turn Off Heaters
                    If ResetChassisOnce2 <> True Then
                        ResetChassis(2)
                        ResetChassisOnce2 = True
                    End If
                    SetHeater(2, False)
                    SetHeater(3, False)
                Case 0 To 5
                    SetHeater(2, True)
                    SetHeater(3, False)
                Case (-10) To 0
                    SetHeater(2, True)
                    SetHeater(3, True)
            End Select
        End If

    End Sub





    Sub EnableShutdownFailSafeCommand()

        Dim Handle As Integer
        Dim Buffer As String
        Dim retCount As Integer
        
        Dim Pass As Short
        Dim Acount As Integer

        Handle = GpibControllerHandle0
        Buffer = Convert.ToString(Chr(&H40)) & "AA"
        Acount = 3
        RetValue = viWrite(Handle, Buffer, Acount, retCount)
        If RetValue <> VI_SUCCESS Then
            Pass = False
        End If

    End Sub



    Sub InitInputBoxLists()
        '************************************************************
        '************************************************************
        '* Nomenclature   : VIPERT SYSTEM MONITOR                     *
        '*    DESCRIPTION:                                          *
        '*     This Module Initialized the user Input/Spin/Text Box *
        '*     Control Values.                                      *
        '*    EXAMPLE:                                              *
        '*     InitInputBoxLists                                    *
        '************************************************************

        ''Init flags
        RangeDisplay = False
        ''Initialize Defaults
        SetDef(FAN_PRIMARY) = "80"
        SetDef(FAN_SECONDARY) = "80"
        SetDef(OPT_TEMP_PRIMARY) = "35"
        SetDef(OPT_TEMP_SECONDARY) = "35"
        ''Initialize Minimums
        SetMin(FAN_PRIMARY) = "50"
        SetMin(FAN_SECONDARY) = "50"
        SetMin(OPT_TEMP_PRIMARY) = "10"
        SetMin(OPT_TEMP_SECONDARY) = "10"
        ' 'Initialize Maximums
        SetMax(FAN_PRIMARY) = "100"
        SetMax(FAN_SECONDARY) = "100"
        SetMax(OPT_TEMP_PRIMARY) = "55"
        SetMax(OPT_TEMP_SECONDARY) = "55"
        ''Initialize Increment Values
        SetInc(FAN_PRIMARY) = "1"
        SetInc(FAN_SECONDARY) = "1"
        SetInc(OPT_TEMP_PRIMARY) = "1"
        SetInc(OPT_TEMP_SECONDARY) = "1"
        ''Initialize Increment Values
        SetUOM(FAN_PRIMARY) = "1"
        SetUOM(FAN_SECONDARY) = "1"
        SetUOM(OPT_TEMP_PRIMARY) = "1"
        SetUOM(OPT_TEMP_SECONDARY) = "1"
        ''Initialize Resolution
        SetRes(FAN_PRIMARY) = "#0"
        SetRes(FAN_SECONDARY) = "#0"
        SetRes(OPT_TEMP_PRIMARY) = "#0"
        SetRes(OPT_TEMP_SECONDARY) = "#0"
        ''Initialize Current Value
        SetCur(FAN_PRIMARY) = SetDef(FAN_PRIMARY)
        SetCur(FAN_SECONDARY) = SetDef(FAN_SECONDARY)
        SetCur(OPT_TEMP_PRIMARY) = SetDef(OPT_TEMP_PRIMARY)
        SetCur(OPT_TEMP_SECONDARY) = SetDef(OPT_TEMP_SECONDARY)
        ''Initialize Command
        SetCmd(FAN_PRIMARY) = ""
        SetCmd(FAN_SECONDARY) = ""
        SetCmd(OPT_TEMP_PRIMARY) = ""
        SetCmd(OPT_TEMP_SECONDARY) = ""

        ''Initialize Range Message
        SetRngMsg(FAN_PRIMARY) = "Min: 50%    Def: 80%    Max: 100%"
        SetRngMsg(FAN_SECONDARY) = "Min: 50%    Def: 80%    Max: 100%"
        SetRngMsg(OPT_TEMP_PRIMARY) = "Min: 10 Deg C    Def: 35 Deg C    Max: 55 Deg C"
        SetRngMsg(OPT_TEMP_SECONDARY) = "Min: 10 Deg C    Def: 35 Deg C    Max: 55 Deg C"

        ''Initialize FPU Tolerances
        SetFpuTolerances()

    End Sub


    Function GetEchoTime() As String
        GetEchoTime = ""
        '************************************************************
        '************************************************************
        '* Nomenclature   : VIPERT SYSTEM Monitor                     *
        '*    DESCRIPTION:                                          *
        '*     This function will determine local and GMT (Z) Time  *
        '*    EXAMPLE:                                              *
        '*      Time$ = GetEchoTime                                 *
        '************************************************************
        Dim TimeZoneInformation As New TIME_ZONE_INFORMATION(0) 'API Structure
        Dim StandardDate As SYSTEMTIME 'API Structure
        Dim DaylightDate As SYSTEMTIME 'API Structure
        Dim LocalTimeStruct As SYSTEMTIME 'API Structure
        Dim SystemTimeStruct As SYSTEMTIME 'API Structure
        Dim EchoTimeData As String = "" 'Formatted Time String
        Dim RetVal As Integer 'API Return Value
        Dim StdName As String 'String Std Format
        Dim ChrLoop As Short 'Character Index
        Dim Letter As String 'Position in array
        Dim Pos As Short 'Position in string
        On Error Resume Next

        'Check Local Time Values
        GetLocalTime(LocalTimeStruct)
        'Check System Time Values
        GetSystemTime(SystemTimeStruct)
        'Check System For Time, Date , Timezone
        RetVal = GetTimeZoneInformation(TimeZoneInformation)

        StdName = ""
        For ChrLoop = 0 To 32
            Letter = Convert.ToString(Chr(TimeZoneInformation.StandardName(ChrLoop)))
            If Letter = Convert.ToString(Chr(0)) Then Exit For
            StdName &= Letter
        Next ChrLoop

        'Error when System is Set in GMT+0 Timezone

        Pos = InStr(StdName, "Standard")
        If Pos <> 0 Then
            StdName = Strings.Left(StdName, Pos - 1)
        Else
            StdName &= " " 'Add A Space
        End If

        If RetVal = 2 Then 'Daylight Time
            StdName += "Daylight Time"
        End If
        If RetVal = 1 Then 'Standard Time
            StdName += "Standard Time"
        End If

        EchoTimeData &= VB6.Format(CStr(SystemTimeStruct.wMonth), "00") & "/"
        EchoTimeData &= VB6.Format(CStr(SystemTimeStruct.wDay), "00") & "/"
        EchoTimeData &= VB6.Format(CStr(SystemTimeStruct.wYear), "00") & " "
        EchoTimeData &= VB6.Format(CStr(SystemTimeStruct.wHour), "00") & ":"
        EchoTimeData &= VB6.Format(CStr(SystemTimeStruct.wMinute), "00") & ":"
        EchoTimeData &= VB6.Format(CStr(SystemTimeStruct.wSecond), "00") & " "
        EchoTimeData &= "Zulu Time (GMT)"
        EchoTimeData &= vbCrLf

        EchoTimeData &= VB6.Format(CStr(LocalTimeStruct.wMonth), "00") & "/"
        EchoTimeData &= VB6.Format(CStr(LocalTimeStruct.wDay), "00") & "/"
        EchoTimeData &= VB6.Format(CStr(LocalTimeStruct.wYear), "00") & " "
        EchoTimeData &= VB6.Format(CStr(LocalTimeStruct.wHour), "00") & ":"
        EchoTimeData &= VB6.Format(CStr(LocalTimeStruct.wMinute), "00") & ":"
        EchoTimeData &= VB6.Format(CStr(LocalTimeStruct.wSecond), "00") & " "

        If TimeZoneInformation.Bias & -60 >= 0 Then
            EchoTimeData &= StdName & " (GMT+" & Trim(CStr(TimeZoneInformation.Bias & -60)) & ") "
        Else
            EchoTimeData &= StdName & " (GMT" & Trim(CStr(TimeZoneInformation.Bias & -60)) & ") "
        End If

        EchoTimeData &= vbCrLf

        GetEchoTime = EchoTimeData


    End Function



    Sub OnProbeSendF2()
        ' DESCRIPTION:
        '   This Module will send the F2 keypress to the
        '   PAWS RTS if it is active to simulate a Manual Intervention
        '   keypress by the operator. Used for escaping from a
        '   MONITOR statement.

        Dim CurrWnd As Integer 'Current Window Handle
        Dim Title As String 'Title of window found
        
        Dim lpString As String 'Buffer for WIndow text in API call

        'Get First Window

        CurrWnd = GetWindow(My.Forms.frmToolbar.Handle, GW_HWNDFIRST)

        'Get Window Information for all Windows on the desktop
        Do While CurrWnd <> 0
            'Get Title of Window
            lpString = Space(255)
            GetWindowText(CurrWnd, lpString, 255)
            Title = Trim(lpString)
            If Title <> Convert.ToString(Chr(0)) Then 'Disregard windows without titles
                'Check if the Probe Window is on the desktop
                'MsgBox Title$ 'Uncomment this line is for debugging
                If InStr(1, Title, "Run Time System", Microsoft.VisualBasic.CompareMethod.Binary) <> 0 Then

                    SetForegroundWindow(CurrWnd) 'Set RTS Window to Foreground
                    SetFocus(CurrWnd)

                    SendKeys.SendWait("{F2}") 'Send an F2 Key
                    SendKeys.SendWait("{F2}") 'Send an F2 Key

                    Exit Sub
                End If
            End If
            'Acquire the next Window on the Desktop
            CurrWnd = GetWindow(CurrWnd, GW_HWNDNEXT)
        Loop
    End Sub



    Sub ResetChassis(ByVal Chassis As Short)

        Dim Acount As Integer
        Dim B1 As Short
        Dim B2 As Short
        Dim B3 As Short
        Dim Handle As Integer
        Dim Buffer As String
        Dim retCount As Integer
        
        Dim Pass As Short

        Acount = 3
        B1 = &H7F
        B2 = &HFF
        B3 = &HFF

        If Chassis = 1 Then
            Handle = ChassisControllerHandle1
        Else
            Handle = ChassisControllerHandle2
        End If
        Buffer = Convert.ToString(Chr(B1)) & Convert.ToString(Chr(B2)) & Convert.ToString(Chr(B3))

        'Delay(1) 'Pevent Command Overruns
        RetValue = viWrite(Handle, Buffer, Acount, retCount)

        If RetValue <> VI_SUCCESS Then
            Pass = False
        End If

        SetMinFanSpeed(Chassis, 50)

    End Sub



    Sub ResetPdu()

        Dim Handle As Integer
        Dim Buffer As String
        Dim retCount As Integer
        
        Dim Pass As Short
        Dim Acount As Integer

        Handle = GpibControllerHandle0
        Buffer = vbLf & "AA"
        Acount = 3
        RetValue = viWrite(Handle, Buffer, Acount, retCount)
        'Try
        '    Handle.Write(Buffer)
        'Catch ex As Exception
        '    Pass = False
        'End Try

    End Sub



    
    Sub ResetPPU(ByVal Check As Short)

        Dim SupplyLoop As Short
        Dim retCount As Integer

        Try
            If Check = True Then
            'Check For Already Reset Supplies
                For SupplyLoop = 1 To 10
                    If (PrimaryChassis.SlotVoltage!(SupplyLoop) <> 0) Or (SecondaryChassis.SlotVoltage!(SupplyLoop) <> 0) Then
                        Exit For
                    Else
                        Exit Sub
                    End If
                Next SupplyLoop
            End If

            'NOTE: Supplies are reset from 10 to 1 forclearing  possible slaving settings
            For SupplyLoop = 10 To 1 Step -1 'Reset PPU Power Supplies
                CommandSupplyReset(SupplyLoop)
                Delay(0.3)
            Next SupplyLoop

        Catch ex As Exception

        End Try

    End Sub



    Sub SendSysMonStatusBarMessage(ByVal MessageString As String)
        '************************************************************
        '* Virtual Instrument Portable Equipment Repair/Test(VIPERT)*
        '*                                                          *
        '* Nomenclature   : SAIS: DMM Front Panel: Main:            *
        '************************************************************
        'MessageString$ is printed in the Instrument Status Bar

        If MessageString <> frmCheckList.sbrUserInformation.Panels(0).Text Then
            frmSysMon.sbrUserInformation.Panels(0).Text = MessageString
        End If

    End Sub



    Sub SetFanSpeed(ByVal Fan As Short, ByVal SpeedPercent As Short)

        Dim Acount As Integer
        Dim B1 As Short
        Dim B2 As Short
        Dim B3 As Short
        Dim Handle As Integer
        Dim Buffer As String
        Dim retCount As Integer
        
        Dim Pass As Short

        Acount = 3
        B1 = &H6F : B3 = &HFF
        B2 = SpeedPercent * 2
        If Fan = 1 Then
            Handle = ChassisControllerHandle1
        Else
            Handle = ChassisControllerHandle2
        End If
        Buffer = Convert.ToString(Chr(B1)) & Convert.ToString(Chr(B2)) & Convert.ToString(Chr(B3))
        RetValue = viWrite(Handle, Buffer, Acount, retCount)
        If RetValue <> VI_SUCCESS Then
            Pass = False
        End If
    End Sub


    Sub SetMinFanSpeed(ByVal Chassis As Short, ByVal SpeedPercent As Short)

        Dim Acount As Integer
        Dim B1 As Short
        Dim B2 As Short
        Dim B3 As Short
        Dim Handle As Integer
        Dim Buffer As String
        Dim retCount As Integer
        
        Dim Pass As Short

        Delay(3) 'Allow Previous Commands To Process In Firmware

        Acount = 3
        B1 = &HCF : B3 = &HFF
        B2 = SpeedPercent * 2
        If Chassis = 1 Then
            Handle = ChassisControllerHandle1
        Else
            Handle = ChassisControllerHandle2
        End If
        Buffer = Convert.ToString(Chr(B1)) & Convert.ToString(Chr(B2)) & Convert.ToString(Chr(B3))
        RetValue = viWrite(Handle, Buffer, Acount, retCount)
        If RetValue <> VI_SUCCESS Then
            Pass = False
        End If
        'Delay(1) 'Allow time for command to process

    End Sub

    Sub SetMaxFanSpeed(ByVal Chassis As Short, ByVal SpeedPercent As Short)

        Dim Acount As Integer
        Dim B1 As Short
        Dim B2 As Short
        Dim B3 As Short
        Dim Handle As Integer
        Dim Buffer As String
        Dim retCount As Integer
        
        Dim Pass As Short

        Delay(3) 'Allow Previous Commands To Process In Firmware

        Acount = 3
        B1 = &H3F : B3 = &HFF
        B2 = SpeedPercent * 2
        If Chassis = 1 Then
            Handle = ChassisControllerHandle1
        Else
            Handle = ChassisControllerHandle2
        End If
        Buffer = Convert.ToString(Chr(B1)) & Convert.ToString(Chr(B2)) & Convert.ToString(Chr(B3))
        RetValue = viWrite(Handle, Buffer, Acount, retCount)
        If RetValue <> VI_SUCCESS Then
            Pass = False
        End If

    End Sub



    
    Sub SetFpu(ByVal State As Short)

        Dim ABuffer As String
        Dim count As Integer
        Dim retCount As Integer

        If State = False Then 'Off
            ABuffer = "+AA" + Convert.ToString(Chr(0))
            frmSysMon.optFpuState_0.Checked = False
            frmSysMon.optFpuState_1.Checked = True
            frmSysMon.ETITimer.Enabled = False
        End If
        If State = True Then 'On
            ABuffer = "KAA" + Convert.ToString(Chr(0))
            frmSysMon.optFpuState_0.Checked = True
            frmSysMon.optFpuState_1.Checked = False
            frmSysMon.ETITimer.Enabled = True
        End If

        count = 4
        RetValue = viWrite(GpibControllerHandle11, ABuffer, count, retCount)

    End Sub



    Sub GetTemperatureThresholds()

        Dim slot As Short 'Chassis Slot Index
        Dim SingleChassisBoot As String 'Read Key For Setting Single Chassis Mode
        For slot = 0 To 25
            'Get Values From INI Fle
            UserDef(slot) = GatherIniFileInformation("System Monitor", "USR_DEF_" & Str(slot), "30")
            'Init Temp Threshold Values
            TempertureThreshold(slot) = UserDef(slot)
            frmSysMon.cwsSlotRise(slot).Value = UserDef(slot)
        Next slot

        For slot = 0 To 25
            FactoryDef(slot) = GatherFactoryDefaults("Factory Defaults", "CC" & CStr(slot), "30")
        Next slot

        'This Will Set A Global Variable to alter program flow to accomodate
        'factory testing of a single chassis.  On a factory testing station the
        'technician needs to modify FACDEF.INI:
        '[CHASSIS]
        'STARTUP=ONECHASSIS
        SingleChassisBoot = GatherFactoryDefaults("CHASSIS", "STARTUP", "NORMAL")
        If UCase(SingleChassisBoot) = "ONECHASSIS" Then
            SING_CHASSIS_OPTION = True
        Else
            SING_CHASSIS_OPTION = False
        End If

    End Sub



    
    Sub SetHeater(ByVal Heater As Short, ByVal State As Short)

        Dim B1 As Short
        Dim B2 As Short
        Dim B3 As Short
        Dim Handle As Integer
        Dim Acount As Integer
        Dim Buffer As String
        Dim retCount As Integer
        
        Dim Pass As Short

        Select Case Heater
            Case 0
                CheckChassisStatusByte(PrimaryChassis.ChassisStatus)
                If (State = True) Then
                    If (StatHeater1 = True) Then Exit Sub 'Already ON
                    B1 = &H8F
                Else
                    If (StatHeater1 = False) Then Exit Sub 'Already OFF
                    B1 = &H9F
                End If
                Handle = ChassisControllerHandle1
            Case 1
                CheckChassisStatusByte(PrimaryChassis.ChassisStatus)
                If (State = True) Then
                    If (StatHeater2 = True) Then Exit Sub 'Already ON
                    B1 = &HAF
                Else
                    If (StatHeater2 = False) Then Exit Sub 'Already OFF
                    B1 = &HBF
                End If
                Handle = ChassisControllerHandle1
            Case 2
                CheckChassisStatusByte(SecondaryChassis.ChassisStatus)
                If (State = True) Then
                    If (StatHeater1 = True) Then Exit Sub 'Already ON
                    B1 = &H8F
                Else
                    If (StatHeater1 = False) Then Exit Sub 'Already OFF
                    B1 = &H9F
                End If
                Handle = ChassisControllerHandle2
            Case 3
                CheckChassisStatusByte(SecondaryChassis.ChassisStatus)
                If (State = True) Then
                    If (StatHeater2 = True) Then Exit Sub 'Already ON
                    B1 = &HAF
                Else
                    If (StatHeater2 = False) Then Exit Sub 'Already OFF
                    B1 = &HBF
                End If
                Handle = ChassisControllerHandle2
        End Select

        Acount = 3
        B2 = &HFF
        B3 = &HFF
        Buffer = Convert.ToString(Chr(B1)) & Convert.ToString(Chr(B2)) & Convert.ToString(Chr(B3))
        RetValue = viWrite(Handle, Buffer, Acount, retCount)
        Delay(0.02) 'This gives the Instrument Controller Time to process this command
        'If another Command Is sent it could be ignored if this delay is removed
        If RetValue <> VI_SUCCESS Then
            Pass = False
        End If

    End Sub


    Sub SetTemperatureThresholds(ByVal slot As Short, ByVal Rise As Single)

        Dim ReturnValue As Integer 'API Returned Eror Code
        Dim lpFileName As String 'File to write to (VIPERT.INI)
        Dim lpKeyName As String 'KEY= in INI file
        Dim lpDefault As String 'Default value if key not found
        Dim lpApplicationName As String '[Application] in INI file


        lpFileName = Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\ATS\", "IniFilePath", Nothing)

        'Init File
        lpKeyName = "USR_DEF_" & (Str(slot))
        lpDefault = Trim(Str(Rise))
        lpApplicationName = "System Monitor"
        ReturnValue = WritePrivateProfileString(lpApplicationName, lpKeyName, lpDefault, lpFileName)

    End Sub




    Sub SetTempSensorOffsets(ByVal ChassisNum As Short, ByVal slot As Short, ByVal HexOffset As String)

        
        Dim ReturnValue As Integer 'API Returned Eror Code
        Dim lpFileName As String 'File to write to (VIPERT.INI)
        Dim lpKeyName As String 'KEY= in INI file
        Dim lpDefault As String 'Default value if key not found
        Dim lpApplicationName As String '[Application] in INI file
        Dim Key As String

        lpFileName = Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\ATS\", "IniFilePath", Nothing)

        'Init File
        If ChassisNum = 1 Then
            Key = "PRI-TSO" & (Str(slot))
        Else
            Key = "SEC-TSO" & (Str(slot))
        End If
        lpKeyName = Key
        lpDefault = HexOffset
        lpApplicationName = "System Monitor"
        ReturnValue = WritePrivateProfileString(lpApplicationName, lpKeyName, lpDefault, lpFileName)

    End Sub




    
    Sub ShowVIPERTDialog(ByVal ContinueButton As Short, ByVal Message As String)

        CenterForm(frmDialog)
        frmDialog.lblDialog.Text = Message
        If ContinueButton Then
            frmDialog.cmdClose.Visible = True
        Else
            frmDialog.cmdClose.Visible = False
        End If

        frmDialog.Show()
        frmDialog.Refresh()

    End Sub



    Sub ShowSystemWaitDialog(ByVal ContinueText As String, ByVal Message As String, ByVal Units As String, ByVal TimeoutSeconds As Integer)

        ' "System Wait Dialog Modified with more accurate timer"
        SysWaitTimeout = TimeoutSeconds
        frmSysWait.tmrTimeout.Enabled = True
        CenterForm(frmSysWait)
        frmSysWait.lblComment.Text = Message
        frmSysWait.cmdClose.Text = ContinueText
        frmSysWait.lblFormat(2).Text = Units
        frmSysWait.lblFormat(3).Text = Units
        frmSysWait.ShowDialog()
        frmSysWait.cmdClose.Select()

    End Sub




    Sub WriteStatusByte(ByVal Status As Short)

        Dim StatusFormat As String

        StatusFormat = VB6.Format(Str(Status), "000")
        'frmToolbar.Text = "ATS SYSTEM MONITOR STATUS BYTE DWH: " & StatusFormat

    End Sub





    Sub CenterForm(ByVal Form As Object)
        '************************************************************
        '************************************************************
        '* Nomenclature   : VIPERT SYSTEM MONITOR                     *
        '*    DESCRIPTION:                                          *
        '*     This Module Centers One Form With Respect To The     *
        '*     User's Screen.                                       *
        '*    EXAMPLE:                                              *
        '*     CenterForm frmMain                                   *
        '************************************************************
        Form.Top = PrimaryScreen.Bounds.Height / 2 - Form.Height / 2
        Form.Left = PrimaryScreen.Bounds.Width / 2 - Form.Width / 2

    End Sub



    
    Sub ChangeHeaterState(ByVal Indicator As Control, ByVal State As Short)
        '************************************************************
        '************************************************************
        '* Nomenclature   : VIPERT SYSTEM MONITOR                     *
        '*    DESCRIPTION:                                          *
        '*    This Module Toggles GUI Controls Between An On and an *
        '*    Off State.                                            *
        '*    Indicator: is the name of the TextBox To Change       *
        '*    State: is the state of the TextBox To Change TRUE=ON  *
        '*    EXAMPLE:                                              *
        '*     ChangeHeaterState txtHeater, TRUE                    *
        '************************************************************
        Dim led As New NationalInstruments.UI.WindowsForms.Led

        'led = Indicator

        'If State Then 'On
        '    led.Value = True
        'Else            'Off
        '    led.Value = False
        'End If

    End Sub



    
    Sub ChangeIndicatorState(ByVal Indicator As Control, ByVal State As Short)
        '************************************************************
        '************************************************************
        '* Nomenclature   : VIPERT SYSTEM MONITOR                     *
        '*    DESCRIPTION:                                          *
        '*    This Module Toggles GUI Controls Between A PASS and a *
        '*    FAIL State.                                           *
        '*    Indicator: is the name of the TextBox To Change       *
        '*    State: is the state of the TextBox To Change TRUE=PASS*
        '*    EXAMPLE:                                              *
        '*     ChangeIndicatorState txtSelfTest, TRUE               *
        '************************************************************
        If (Indicator.GetType() Is GetType(NationalInstruments.UI.WindowsForms.Led)) Then
            Dim ledIndicator As New NationalInstruments.UI.WindowsForms.Led
            ledIndicator = Indicator
            If State Then 'Pass
                ledIndicator.Value = True
            Else            'Fail
                ledIndicator.Value = False
            End If
        Else
            'Do nothing for now
            'If State Then 'Pass
            '    Indicator.Enabled = True
            'Else            'Fail
            '    Indicator.Enabled = False
            'End If
        End If

    End Sub



    Sub CheckActionByte(ByVal ActionByte As String)
        '************************************************************
        '************************************************************
        '* Nomenclature   : VIPERT SYSTEM MONITOR                     *
        '*    DESCRIPTION:                                          *
        '*    This Module Evaluates the Logical Bits in an Action   *
        '*    Byte by Masking with logical AND and placing the      *
        '*    results in module level dimensioned variables.        *
        '*    ActionByte$: An ASCII Character Representing The Value*
        '*     of the System Monitor Action Byte.                   *
        '*    EXAMPLES:                                             *
        '*     CheckActionByte "33"                                 *
        '*     CheckActionByte Str(AByte%)                          *
        '************************************************************

        Dim ActionByteValue As Short

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
        ActionByteValue = Val(ActionByte)
        ActModFault = ActionByteValue And 240
        If ActModFault = 16 Then
            ActModAddr = ActionByteValue And 15
        End If
        ActChassisAddress = ActionByteValue And 8
        ActVolt28Ok = ActionByteValue And 4
        ActProbeEvent = ActionByteValue And 2
        ActReceiverEvent = ActionByteValue And 1

    End Sub




    
    Sub CheckFpuStatusByte(ByVal FpuByte As String)
        '************************************************************
        '************************************************************
        '* Nomenclature   : VIPERT SYSTEM MONITOR                     *
        '*    DESCRIPTION:                                          *
        '*    This Module Evaluates the Logical Bits in an FPU      *
        '*    Byte by Masking with logical AND and placing the      *
        '*    results in module level dimensioned variables.        *
        '*    FpuByte$: An ASCII Character Representing The   *
        '*    Value of the System Monitor FPU Status Byte.          *
        '*    EXAMPLES:                                             *
        '*     CheckFpuStatusByte "33"                        *
        '*     CheckFpuStatusByte Str(AByte%)                 *
        '************************************************************
        
        Dim FpuValue As Short

        FpuValue = Val(FpuByte)
        '----------------------------------------------------------------
        '- Fixed Power Unit Status Byte ---------------------------------
        '-        B7 | B6 |             B5 |     B4 | B3 | B2 | B1 | B0 -
        '- Mod Fault | On | Off / Mismatch | OV/*UV | A3 | A2 | A1 | A0 -
        '----------------------------------------------------------------
        'Mismatch 1010 ADDR         ADDR=Address of Module In incorrect slot
        'Over Voltage 1001 ADDR     ADDR=Address of a faulting module
        'Undervoltage 1000 ADDR     ADDR=Address of a faulting module
        'On 0100 XXXX               1 If any modules are on
        'Off 0010 XXXX              0 If any modules are off
        'Ex. FpuStatModFault%=1     1 | X | Mismatch=1 VoltProb=0 | OV/*UV | A3 | A2 | A1 | A0
        'Ex. FpuStatModFault%=0     0 | On | Off | X | X | X | X | X
        '----------------------------------------------------------------
        FpuStatModFault = FpuValue And 128 'Mask Bit 7 (Clear All Other Bits)
        FpuStatOn = FpuValue And 64 'Mask Bit 6 (Clear All Other Bits)
        FpuStatOffMismnatch = FpuValue And 32 'Mask Bit 5 (Clear All Other Bits)
        FpuStatOverVolt = FpuValue And 16 'Mask Bit 4 (Clear All Other Bits)
        FpuStatAddress = FpuValue And &HF 'Mask Bits 0,1,2,3 (Clear All Other Bits)

    End Sub



    
    Sub CheckPowerStatusByte(ByVal PowerStatusByte As String)
        '************************************************************
        '************************************************************
        '* Nomenclature   : VIPERT SYSTEM MONITOR                     *
        '*    DESCRIPTION:                                          *
        '*    This Module Evaluates the Logical Bits in a POWER     *
        '*    Byte by Masking with logical AND and placing the      *
        '*    results in module level dimensioned variables.        *
        '*    PowerStatusByte$: An ASCII Character Representing The *
        '*    Value of the System Monitor POWER Status Byte.        *
        '*    EXAMPLES:                                             *
        '*     CheckPowerStatusByte "33"                            *
        '*     CheckPowerStatusByte Str(AByte%)                     *
        '************************************************************
        
        Dim PowerStatusValue As Short

        PowerStatusValue = Val(PowerStatusByte)
        '-------------------------------------------------------------------------------------
        '- Power Status Byte -----------------------------------------------------------------
        '-         B7 | B6 |              B5 |            B4 |   B3 |   B2 |   B1 |      B0  -
        '- Input Fail | AC | DC / Fail State | Three/*Single | HPH1 | HPH2 | HPH3 | 2800W OK -
        '-------------------------------------------------------------------------------------
        'Single Phase AC x100 0000 Phase 1,2,or 3 will go Hi on HAM fault
        '                          2800 W will go Hi upon a converter failure
        'Three Phase AC x101 0000  Phase 1,2, or 3 will indicate missing phase or
        '                          HAM Fault, 2800 W will go Hi upon a converter failure
        'DC Mode x010 xxxx
        'Input Failure detected by PPU 1010 0000 Input voltage level exceeded 32 V
        '-------------------------------------------------------------------------------------
        PowerStatus28FailHigh = PowerStatusValue And 128 'Mask Bit 7 (Clear All Other Bits)
        PowerStatusAc = PowerStatusValue And 64 'Mask Bit 6 (Clear All Other Bits)
        PowerStatusDc = PowerStatusValue And 32 'Mask Bit 5 (Clear All Other Bits)
        PowerStatusSingle = PowerStatusValue And 16 'Mask Bit 4 (Clear All Other Bits)
        PowerStatusPhase1 = PowerStatusValue And 8 'Mask Bit 3 (Clear All Other Bits)
        PowerStatusPhase2 = PowerStatusValue And 4 'Mask Bit 2 (Clear All Other Bits)
        PowerStatusPhase3 = PowerStatusValue And 2 'Mask Bit 1 (Clear All Other Bits)
        PowerStatus2800Watt = PowerStatusValue And 1 'Mask Bit 0 (Clear All Other Bits)

    End Sub



    Public Function CheckEthernetPorts() As String
        CheckEthernetPorts = ""
        'GOOD = good ports, ADDRESS = bad address, NAMES = bad names, TOOMANY = too many ports in registry

        
        Dim ShellResult As Integer
        
        
        Dim nFile As Integer, nFile2 As Integer
        
        Dim FileName As String, FileName2 As String
        
        
        Dim sLineResult As String, sTemp As String, sTemp2 As String
        
        Dim FileArray(50) As String, fileArray2(35) As String
        
        
        
        Dim i As Short, j As Short, count As Short, errCount As Short
        Dim addrPos As Short
        Dim ProcID As Double
        
        Dim bGig1 As Boolean, bGig2 As Boolean
        
        Dim MesBoxResp As DialogResult
        
        Dim AddressFlag As Boolean, TryAgainFlag As Boolean
        
        Dim NamPos As Short, ColPos As Short

        TryAgainFlag = False

        On Error GoTo ErrorHandler
        errCount = 0
TryAgain:
        i = 0
        j = 0
        count = 0

        'kill windows app that could cause an error durring ethernet checking
        KillappNT("ncs2prov.exe")

        If TryAgainFlag = True Then
            MsgBox("There was a problem checking the registry. Press [Ok] to try again.", MsgBoxStyle.OkOnly)
            TryAgainFlag = False
        End If

        If CEPFlag = False Then
            MesBoxResp = MsgBox("Please verify that the W206 loopback ethernet cable is plugged in." & vbCrLf & "Click [Ok] when the cable is plugged in." & vbCrLf & "Clicking [Cancel] will skip the Ethernet Address validation.", MsgBoxStyle.OkCancel)
            CEPFlag = True
        End If

        If MesBoxResp = DialogResult.OK Then
            AddressFlag = True
        ElseIf MesBoxResp = DialogResult.Cancel Then
            AddressFlag = False
        End If

        'Check Ethernet Registry info
        frmSetup.Text1.Text = "Checking Ethernet Ports in the System Registry."

        Delay(1)

        ProcID = Shell("C:\windows\system32\cmd.exe", AppWinStyle.NormalFocus)
        Delay(1)
        'AppActivate("c:\windows\system32\cmd.exe")

        SendKeys.SendWait("cd ""C:\Program Files {(}x86{)}\ATS\System Test\""" & vbCrLf)
        Delay(1)
        'AppActivate("c:\windows\system32\cmd.exe")

        SendKeys.SendWait("devcon findall =net >t.txt" & vbCrLf)
        Delay(1)
        'AppActivate("c:\windows\system32\cmd.exe")

        SendKeys.SendWait("exit" & vbCrLf)
        Delay(1)

        nFile2 = FreeFile()
        FileName2 = "C:\Program Files (x86)\ATS\System Test\t.txt"

        If FileExists("C:\Program Files (x86)\ATS\System Test\t.txt") = False Then
            KillappNT("cmd.exe")
            errCount += 1
            If errCount = 3 Then GoTo ErrorHandler
            TryAgainFlag = True
            GoTo TryAgain
        End If

        FileOpen(nFile2, FileName2, OpenMode.Input)
        ' Read the contents of the file
        Do While Not EOF(nFile2)
            sLineResult = LineInput(nFile2)
            fileArray2(i) = sLineResult
            i += 1
        Loop
        FileClose(nFile2)
        'count the PCI ethernet ports

        For j = 0 To 35
            If InStr(1, fileArray2(j), "PCI\", Microsoft.VisualBasic.CompareMethod.Binary) Then count += 1
        Next j

        Kill("C:\Program Files (x86)\ATS\System Test\t.txt")

        If count > 999 Then 'Too many ethernet ports in system registry
            CheckEthernetPorts = "TOOMANY"
            frmSetup.Text1.Text = "There are too many Ethernet Entries in the System Registry."
            Exit Function
        End If


        i = 0
        j = 0
        'Check Names and Addresses
        frmSetup.Text1.Text = "Checking the Ethernet Names and IP Addresses."

        ShellResult = ShellAndWait("cmd.exe /c ipconfig >ipconfig.txt", Microsoft.VisualBasic.FileAttribute.Hidden)

        nFile = FreeFile()
        FileName = "ipconfig.txt"
        FileOpen(nFile, FileName, OpenMode.Input)

        Do While Not EOF(nFile)
            sLineResult = LineInput(nFile)
            FileArray(i) = sLineResult
            i += 1
        Loop
        FileClose(nFile)

        'check Gigabit1 name and address
        For j = 0 To 50

            If InStr(1, UCase(FileArray(j)), "GIGABIT1", Microsoft.VisualBasic.CompareMethod.Binary) Then
                NamPos = InStr(1, UCase(FileArray(j)), "GIGABIT1", Microsoft.VisualBasic.CompareMethod.Binary)
                ColPos = InStr(1, FileArray(j), ":", Microsoft.VisualBasic.CompareMethod.Binary)
                sTemp2 = Mid(FileArray(j), NamPos, ColPos - NamPos)
            End If

            If InStr(1, UCase(FileArray(j)), "GIGABIT1", Microsoft.VisualBasic.CompareMethod.Binary) And Len(sTemp2) = 8 Then
                addrPos = InStr(1, FileArray(j + 6), ": ", Microsoft.VisualBasic.CompareMethod.Binary)
                If addrPos = 0 Then
                    If (MsgBox("Your Ethernet cable is not plugged in or is malfunctioning.", MsgBoxStyle.OkCancel) = MsgBoxResult.Cancel) Then
                        Return ""
                    End If
                    CEPFlag = False
                    GoTo TryAgain
                End If
                sTemp = Mid(FileArray(j + 6), addrPos + 2, Convert.ToString(FileArray(j + 6)).Length)
                If sTemp <> "192.168.0.1" And AddressFlag = True Then 'address incorrect
                    'fix address
                    CheckEthernetPorts = "ADDRESS"
                    frmSetup.Text1.Text = "Ethernet Addresses are not correct."
                    Exit Function
                Else
                    bGig1 = True
                End If
            End If
            'name not present need to set names
            If j = 50 And bGig1 <> True Then
                CheckEthernetPorts = "NAMES"
                frmSetup.Text1.Text = "Ethernet Names are not correct."
                Exit Function
            End If
        Next j

        'check Gigabit2 name and address
        For j = 0 To 50

            If InStr(1, UCase(FileArray(j)), "GIGABIT2", Microsoft.VisualBasic.CompareMethod.Binary) Then
                NamPos = InStr(1, UCase(FileArray(j)), "GIGABIT2", Microsoft.VisualBasic.CompareMethod.Binary)
                ColPos = InStr(1, FileArray(j), ":", Microsoft.VisualBasic.CompareMethod.Binary)
                sTemp2 = Mid(FileArray(j), NamPos, ColPos - NamPos)
            End If

            If InStr(1, UCase(FileArray(j)), "GIGABIT2", Microsoft.VisualBasic.CompareMethod.Binary) And Len(sTemp2) = 8 Then
                addrPos = InStr(1, FileArray(j + 6), ": ", Microsoft.VisualBasic.CompareMethod.Binary)
                sTemp = Mid(FileArray(j + 6), addrPos + 2, Convert.ToString(FileArray(j + 6)).Length)
                If sTemp <> "192.168.200.1" And AddressFlag = True Then 'address incorrect
                    'fix address
                    CheckEthernetPorts = "ADDRESS"
                    frmSetup.Text1.Text = "Ethernet Addresses are not correct."
                    Exit Function
                Else                        'Gigabit2 found and Passed
                    bGig2 = True
                End If
            End If
            'name not present need to set names
            If j = 50 And bGig2 <> True Then
                CheckEthernetPorts = "NAMES"
                frmSetup.Text1.Text = "Ethernet Names are not Correct."
                Exit Function
            End If
        Next j
        Kill("ipconfig.txt")

        CheckEthernetPorts = "GOOD" 'pass
        CEPFlag = False 'reset to false incase of re-run later
        Exit Function

ErrorHandler:
        MsgBox("There was a problem checking the Ethernet Ports.  Press [Ok] and contact a Maintenance Operator.", MsgBoxStyle.OkOnly)
        If FileExists("C:\Program Files (x86)\ATS\System Test\t.txt") Then Kill("C:\Program Files (x86)\ATS\System Test\t.txt")
        'KillappNT "cmd.exe"
        CheckEthernetPorts = "BAD"

    End Function


    Public Function CheckEthernetRegistry() As Boolean
        'GOOD = good ports, ADDRESS = bad address, NAMES = bad names, TOOMANY = too many ports in registry

        Dim ShellResult As Integer
        
        Dim nFile As Object, nFile2 As Integer
        Dim FileName As Object, FileName2 As String
        
        Dim sLineResult() As String, sTemp As String
        Dim FileArray(50) As Object, fileArray2(35) As String
        
        
        
        Dim i As Short, j As Short, count As Short, errCount As Short
        Dim addrPos As Short
        Dim ProcID As Double
        
        Dim MesBoxResp As DialogResult
        Dim TryAgainFlag As Boolean

        TryAgainFlag = False

TryAgain:
        i = 0
        j = 0
        count = 0

        If TryAgainFlag = True Then
            MesBoxResp = MsgBox("Sorry, there was a problem checking the registry. Click [Ok] to try again, or 'Cancel' to Cancel.", MsgBoxStyle.OkCancel)
            If MesBoxResp = DialogResult.Cancel Then Exit Function
        End If

        'Check Ethernet Registry info
        ProcID = Shell("C:\windows\system32\cmd.exe", AppWinStyle.NormalFocus)
        Delay(1)
        AppActivate(CInt(ProcID))
        SendKeys.SendWait("cd C:\Program Files {(}x86{)}\VIPERT\System Test\""" & vbCrLf)
        Delay(1)
        SendKeys.SendWait("devcon findall =net >t.txt" & vbCrLf)
        Delay(1)
        SendKeys.SendWait("exit" & vbCrLf)
        Delay(1)

        nFile2 = FreeFile()
        FileName2 = "C:\Program Files (x86)\ATS\System Test\t.txt"

        If FileExists(FileName2) = False Then
            KillappNT("cmd.exe")
            TryAgainFlag = True
            GoTo TryAgain
        End If

        FileOpen(nFile2, FileName2, OpenMode.Input)
        ' Read the contents of the file
        Do While Not EOF(nFile2)
            sLineResult(i) = LineInput(nFile2)
            fileArray2(i) = sLineResult(i)
            i = i + 1
        Loop
        FileClose(nFile2)
        Kill("C:\Program Files (x86)\ATS\System Test\t.txt")
        'count the PCI ethernet ports

        For j = 0 To 35
            If InStr(1, fileArray2(j), "PCI\", Microsoft.VisualBasic.CompareMethod.Binary) Then count += 1
        Next j

        If count <> 3 Then 'Too many ethernet ports in system registry
            CheckEthernetRegistry = False
        Else
            CheckEthernetRegistry = True
        End If

    End Function



    Public Function CheckEthernetNames() As String
        CheckEthernetNames = ""

        
        Dim ShellResult As Integer
        
        Dim nFile As Integer
        Dim FileName As String
        
        
        Dim sLineResult() As String, sTemp As String, sTemp2 As String
        Dim FileArray(50) As String
        
        Dim i As Short, j As Short
        
        
        Dim addrPos As Short, NamPos As Short, ColPos As Short
        
        Dim bGig1 As Boolean, bGig2 As Boolean, bGig4 As Boolean
        
        Dim MesBoxResp As DialogResult
        Dim AddressFlag As Object, TryAgainFlag As Boolean


TryAgain2:

        If CEPFlag = False Then
            MesBoxResp = MsgBox("Please verify that the W206 loopback ethernet cable is plugged in." & vbCrLf & "Click [Ok] when the cable is plugged in.", MsgBoxStyle.OkOnly)
            CEPFlag = True
        End If

        Dim adapters() As NetworkInterface = NetworkInterface.GetAllNetworkInterfaces()

        i = 0
        For Each NetworkInterface In adapters
            If (adapters(i).Name = "Gigabit1") Then
                If adapters(i).OperationalStatus = OperationalStatus.Down Then
                    If (MsgBox("Your Ethernet cable is not plugged in or is malfunctioning.", MsgBoxStyle.OkCancel) = MsgBoxResult.Cancel) Then
                        Return "CANCEL"
                    End If
                    GoTo TryAgain2
                End If
                Dim IPInterfaceProperties = adapters(i).GetIPProperties()
                Dim UnicastIPAddressInformation = IPInterfaceProperties.UnicastAddresses
                If UnicastIPAddressInformation.Item(1).Address.ToString <> "192.168.0.1" Then
                    'fix address
                    CheckEthernetNames = "ADDRESS"
                    Exit Function
                Else
                    bGig1 = True
                    Exit For
                End If
            End If
            i += 1
        Next NetworkInterface
        'name not present need to set names
        If bGig1 <> True Then
            CheckEthernetNames = "NAMES"
            Exit Function
        End If

        'check Gigabit2 name and address
        i = 0
        For Each NetworkInterface In adapters
            If (adapters(i).Name = "Gigabit2") Then
                If adapters(i).OperationalStatus = OperationalStatus.Down Then
                    If (MsgBox("Your Ethernet cable is not plugged in or is malfunctioning.", MsgBoxStyle.OkCancel) = MsgBoxResult.Cancel) Then
                        Return "CANCEL"
                    End If
                    GoTo TryAgain2
                End If
                Dim IPInterfaceProperties = adapters(i).GetIPProperties()
                Dim UnicastIPAddressInformation = IPInterfaceProperties.UnicastAddresses
                If UnicastIPAddressInformation.Item(1).Address.ToString <> "192.168.200.1" Then
                    'fix address
                    CheckEthernetNames = "ADDRESS"
                    Exit Function
                Else
                    bGig2 = True
                    Exit For
                End If
            End If
            i += 1
        Next NetworkInterface
        'name not present need to set names
        If bGig2 <> True Then
            CheckEthernetNames = "NAMES"
            Exit Function
        End If


        'check Gigabit4 name and address
        i = 0
        For Each NetworkInterface In adapters
            If (adapters(i).Name = "Gigabit4") Then
                If adapters(i).OperationalStatus = OperationalStatus.Down Then
                    If (MsgBox("Your Ethernet cable is not plugged in or is malfunctioning.", MsgBoxStyle.OkCancel) = MsgBoxResult.Cancel) Then
                        Return "CANCEL"
                    End If
                    GoTo TryAgain2
                End If
                Dim IPInterfaceProperties = adapters(i).GetIPProperties()
                Dim UnicastIPAddressInformation = IPInterfaceProperties.UnicastAddresses
                If UnicastIPAddressInformation.Item(1).Address.ToString <> "192.168.20.1" Then
                    'fix address
                    CheckEthernetNames = "ADDRESS"
                    Exit Function
                Else
                    bGig4 = True
                    Exit For
                End If
            End If
            i += 1
        Next NetworkInterface
        'name not present need to set names
        If bGig4 <> True Then
            CheckEthernetNames = "NAMES"
            Exit Function
        End If

        CheckEthernetNames = "GOOD"

    End Function

    Public Function RenameEthernetPorts(ByVal TextFlag As Short) As Boolean
        ' 1 for finding the ports as well as renaming them
        ' 2 for simply renaming the ports


        If TextFlag = 1 Then
            MsgBox("- Open the Control Panel" & vbCrLf & "- Click 'Network and Internet'" & vbCrLf &
                   "- In the right pane click Network and Sharing Center" & vbCrLf & "- In the left pane click Change adapter settings" & vbCrLf &
                   "- Rename port corresponding to J15 to 'Gigabit1' Say [Yes] at the User Account Control" & vbCrLf &
                   "- Rename port corresponding to J16 to 'Gigabit2' Say [Yes] at the User Account Control" & vbCrLf &
                   "- Rename port corresponding to J18 to 'Local Area Connection' Say [Yes] at the User Account Control" & vbCrLf &
                   "- Rename port corresponding to J19 to 'Gigabit4' Say [Yes] at the User Account Control",
                   MsgBoxStyle.OkCancel)
        ElseIf TextFlag = 2 Then
            If (MsgBox("- Open the Control Panel" & vbCrLf & "- Click 'Network and Internet'" & vbCrLf &
                       "- In the right pane click Network and Sharing Center'" & vbCrLf & "- In the left pane click Change adapter settings" & vbCrLf &
                       "- Rename port corresponding to J15 to 'Gigabit1' Say [Yes] at the User Account Control" & vbCrLf &
                       "- Rename port corresponding to J16 to 'Gigabit2' Say [Yes] at the User Account Control" & vbCrLf &
                       "- Rename port corresponding to J18 to 'Local Area Connection' Say [Yes] at the User Account Control" & vbCrLf &
                       "- Rename port corresponding to J19 to 'Gigabit4' Say [Yes] at the User Account Control",
                       MsgBoxStyle.OkCancel) = MsgBoxResult.Cancel) Then
                Return True
            Else
                Return False
            End If
        End If

       


    End Function



    Public Sub SetIPAddress()

        Dim interfaces As NetworkInterface() = NetworkInterface.GetAllNetworkInterfaces()
        For Each adapter As NetworkInterface In interfaces
            If adapter.Name = "Gigabit1" Then
                setupNetwork(adapter.GetPhysicalAddress().ToString, GBit1IP, GBitMask, "", "", "")
            ElseIf adapter.Name = "Gigabit2" Then
                setupNetwork(adapter.GetPhysicalAddress().ToString, GBit2IP, GBitMask, GBit2DG, "", "")
            ElseIf adapter.Name = "Local Area Connection" Then
                Process.Start("netsh", "interface ip set address \""Local Area Connection\"" dhcp")
                'setupNetwork(adapter.GetPhysicalAddress().ToString, LACIP, GBitMask, "", "", "")
            ElseIf adapter.Name = "Gigabit4" Then
                setupNetwork(adapter.GetPhysicalAddress().ToString, GBit4IP, GBitMask, "", "", "")
            End If

        Next

    End Sub


Function setupNetwork(phyAddr As String, ipAddr As String, netmask As String, gateway As String, dns1 As String, dns2 As String) As Boolean
    Dim result As Boolean = False
    ' concatenate two dns addresses into one
    Dim dnsSearchOrder As String = dns1 + "," + dns2

    Dim objMC As ManagementClass = New ManagementClass("Win32_NetworkAdapterConfiguration")
    Dim objMOC As ManagementObjectCollection = objMC.GetInstances()

    For Each objMO As ManagementObject In objMOC

        If (CBool(objMO("IPEnabled"))) Then

            ' remove colons from mac address so that it could match the
            ' provided mac address
            Dim origMAC As String = objMO("MACAddress").ToString()
            Dim pattern As String = ":"
            Dim replacement As String = ""
            Dim rgx As New Regex(pattern)
            ' the mac address with colons removed from it
            Dim repMAC As String = rgx.Replace(origMAC, replacement)

            If (String.Equals(phyAddr, repMAC)) Then
                Try
                    Dim objNewIP As ManagementBaseObject = Nothing
                    Dim objNewGate As ManagementBaseObject = Nothing
                    Dim objNewDNS As ManagementBaseObject = Nothing
                    Dim objSetIP As ManagementBaseObject = Nothing

                    objNewIP = objMO.GetMethodParameters("EnableStatic")
                    objNewGate = objMO.GetMethodParameters("SetGateways")
                    objNewDNS = objMO.GetMethodParameters("SetDNSServerSearchOrder")

                    'set defaultgateway
                    objNewGate("DefaultIPGateway") = New String() {gateway}
                    objNewGate("GatewayCostMetric") = New Integer() {1}

                    'set ipaddress and subnetmask
                    objNewIP("IPAddress") = New String() {ipAddr}
                    objNewIP("SubnetMask") = New String() {netmask}
                    objNewDNS("DNSServerSearchOrder") = dnsSearchOrder.Split(",")

                    objSetIP = objMO.InvokeMethod("EnableStatic", objNewIP, Nothing)
                    objSetIP = objMO.InvokeMethod("SetGateways", objNewGate, Nothing)
                    objSetIP = objMO.InvokeMethod("SetDNSServerSearchOrder", objNewDNS, Nothing)

                    result = True
                    Exit For

                Catch ex As Exception
                    MessageBox.Show("Couldn't Set IP Address!")
                End Try

            End If

        End If
    Next



    Return result

End Function

'set computers host name
Private Function setHostname(hostname As String) As Boolean
    Dim result As Boolean = False

    Dim path As New ManagementPath

    path.Server = System.Net.Dns.GetHostName
    path.NamespacePath = "root\CIMV2"
    path.RelativePath = "Win32_Computersystem.Name='" & path.Server & "'"

    Dim objMO As New ManagementObject(path)
    Dim params() As Object = {hostname}
    objMO.InvokeMethod("Rename", params)
    result = True

    Return result
End Function




    
    Function ShellAndWait(ByVal PathName As String, Optional ByRef WindowState As Object = Nothing) As Double

        Dim hProg As Integer
        Dim hProcess As Integer, exitCode As Integer

        If IsNothing(WindowState) Then WindowState = 1
        hProg = Shell(PathName, WindowState)
        'get the process handle -
        hProcess = OpenProcess(PROCESS_QUERY_INFORMATION, False, hProg)
        Do
            'get the ExitCode
            GetExitCodeProcess(hProcess, exitCode)

        Loop While exitCode = STILL_ACTIVE
        ShellAndWait = 1
    End Function


    
    Sub CheckChassisStatusByte(ByVal StatusByte As Short)
        '************************************************************
        '************************************************************
        '* Nomenclature   : VIPERT SYSTEM MONITOR                     *
        '*    DESCRIPTION:                                          *
        '*    This Module Evaluates the Logical Bits in a STATUS    *
        '*    Byte by Masking with logical AND and placing the      *
        '*    results in module level dimensioned variables.        *
        '*    StatusByte%: An Integer Representing The              *
        '*    Value of the System Monitor Status Byte.              *
        '*    EXAMPLES:                                             *
        '*     CheckChassisStatusByte 33                            *
        '*     CheckChassisStatusByte Chr$(AsciiByte$)              *
        '************************************************************

        StatSelfTest = StatusByte And 32
        StatHeater2 = StatusByte And 16
        StatHeater1 = StatusByte And 8
        StatAirFlowSensor3 = StatusByte And 4
        StatAirFlowSensor2 = StatusByte And 2
        StatAirFlowSensor1 = StatusByte And 1

    End Sub




    
    Function ConvertBytesToWordAndScale(ByVal DataHighByte As String, ByVal DataLowByte As String, ByVal ScaleMode As Short) As Single
        '************************************************************
        '************************************************************
        '* Nomenclature   : VIPERT SYSTEM MONITOR                     *
        '*    DESCRIPTION:                                          *
        '*     This Module Converts System Data Into Values That Can*
        '*     be more easily rRecognised by the user               *
        '*        DataHighByte$: ASCII Rep Of High Byte Value       *
        '*        DataLowByte$: ASCII Rep Of Low Byte Value         *
        '*        ScaleMode%: Chooses Scaling Method                *
        '*    EXAMPLE:                                              *
        '*     ConvertBytesToWordAndScale "33", "44", 1             *
        '************************************************************
        Dim ScaleValue As Single
        Dim HighByte As Integer
        Dim LowByte As Integer

        HighByte = Asc(DataHighByte)
        'Mask Off the MS Nibble
        HighByte = HighByte And 15
        LowByte = Asc(DataLowByte)
        'Shift Highbyte To Correct Binary Weights
        HighByte *= 256
        Select Case ScaleMode
            Case 1
                '24 Volt Scale
                ScaleValue = ((HighByte + LowByte) * 0.16)
            Case 2
                '12 Volt Scale
                ScaleValue = ((HighByte + LowByte) * 0.00488) * 2
            Case 3
                'Straight Scale
                ScaleValue = ((HighByte + LowByte) / 100)
            Case 4
                'UUT Power Current
                ScaleValue = (LowByte / 500) * 16
            Case 5
                'FPU Currents
                ScaleValue = (HighByte + LowByte) * 0.16
            Case 6
                'Vin Single Scale
                ScaleValue = LowByte * 0.598
            Case 7
                'Vin Three Phase
                ScaleValue = (1.288 - ((LowByte - 103) * 0.000838)) * LowByte
            Case 8
                'Current Input Scale
                ScaleValue = LowByte / 4.65
            Case 9
                'Dc Input Scale
                If LowByte = 0 Then Exit Function
                ScaleValue = 4 / (LowByte * 0.001013)
            Case 10
                'Straight Scale for 65V supply changed 5/11/97 SJM
                ScaleValue = ((HighByte + LowByte) / 50)
            Case 11
                'Chassis Measured Supply Level 24v
                ScaleValue = HighByte + LowByte
                ScaleValue = (ScaleValue * 8) / 1000
            Case 12
                'Chassis Measured Supply Level 12v
                ScaleValue = HighByte + LowByte
                ScaleValue = (ScaleValue * 4) / 1000
            Case 13
                'Chassis Measured Supply Level 5v /5.2
                ScaleValue = HighByte + LowByte
                ScaleValue = (ScaleValue * 2) / 1000
            Case 14
                'Chassis Measured Supply Level 2v
                ScaleValue = HighByte + LowByte
                ScaleValue = (ScaleValue) / 1000
            Case 15
                'chassis measured supply current for +5A
                ScaleValue = (HighByte + LowByte) * 0.32

            Case Else
                ' Error
                ConvertBytesToWordAndScale = 0
                Exit Function
        End Select
        'Add Corrected HighByte to Low Byte
        ConvertBytesToWordAndScale = ScaleValue

    End Function


    Function FormatPolarity(ByVal DataHighByte As String) As Boolean
        '************************************************************
        '************************************************************
        '* Nomenclature   : VIPERT SYSTEM MONITOR                     *
        '*    DESCRIPTION:                                          *
        '*     This Module Converts System Data Into Values That Can*
        '*     be more easily recognised by the user                *
        '*        DataHighByte$: ASCII Rep Of High Byte Value       *
        '*    EXAMPLE:                                              *
        '*     SlotPolarity = FormatPolarity(Asc(PowerByte))        *
        '************************************************************
        Dim HighByte As Integer
        HighByte = Asc(DataHighByte)

        If ((HighByte And &H80) <> 0) Then
            FormatPolarity = False ' Normal=True, Reversed=False
        Else
            FormatPolarity = True ' Normal=True, Reversed=False
        End If

    End Function



    
    Function FileExists(ByVal sPath As String) As Short
        '************************************************************
        '*    DESCRIPTION:                                          *
        '*     This Module Checks To See If A Disk File Exists      *
        '*    EXAMPLE:                                              *
        '*     IsFileThere% = FileExists("C:\ISFILE.EX")            *
        '*    RETURNS:                                              *
        '*     TRUE if File is present.                             *
        '*     False if File is not present.                        *
        '************************************************************

        If System.IO.File.Exists(sPath) Then
            FileExists = True
        Else
            FileExists = False
        End If

    End Function




    
    Function FormatSystemMonitorData(ByVal Data As String) As ChassisData
        '************************************************************
        '************************************************************
        '* Nomenclature   : VIPERT SYSTEM MONITOR                     *
        '*    DESCRIPTION:                                          *
        '*     This Module takes the SYSMON large data dump string  *
        '*     and formats the bytes into useable data and places   *
        '*     the data into a user defined type --> ChassisData    *
        '*    EXAMPLE:                                              *
        '*     PrimaryChassis = FormatSystemMonitorData(DataBuffer$)*
        '*    RETURNS:                                              *
        '*     Formatted Data as ChassisData User Defined Type      *
        '************************************************************
        Dim SlotIndex As Short
        Dim Chassis As New ChassisData(0)
        Dim Heater As Short

        'ActionByte
        Chassis.Action1 = Asc(Mid(Data, 1, 1))

        'Power Supply Current and Voltage
        Chassis.SlotCurrent!(1) = ConvertBytesToWordAndScale(0, (Mid(Data, 2, 1)), 4)
        Chassis.SlotPolarity(1) = FormatPolarity(Mid(Data, 3, 1))
        Chassis.SlotVoltage!(1) = ConvertBytesToWordAndScale(Mid(Data, 3, 1), Mid(Data, 4, 1), 3)
        Chassis.SlotCurrent!(2) = ConvertBytesToWordAndScale(0, (Mid(Data, 5, 1)), 4)
        Chassis.SlotPolarity(2) = FormatPolarity(Mid(Data, 6, 1))
        Chassis.SlotVoltage!(2) = ConvertBytesToWordAndScale(Mid(Data, 6, 1), Mid(Data, 7, 1), 3)
        Chassis.SlotCurrent!(3) = ConvertBytesToWordAndScale(0, (Mid(Data, 8, 1)), 4)
        Chassis.SlotPolarity(3) = FormatPolarity(Mid(Data, 9, 1))
        Chassis.SlotVoltage!(3) = ConvertBytesToWordAndScale(Mid(Data, 9, 1), Mid(Data, 10, 1), 3)
        Chassis.SlotCurrent!(4) = ConvertBytesToWordAndScale(0, (Mid(Data, 11, 1)), 4)
        Chassis.SlotPolarity(4) = FormatPolarity(Mid(Data, 12, 1))
        Chassis.SlotVoltage!(4) = ConvertBytesToWordAndScale(Mid(Data, 12, 1), Mid(Data, 13, 1), 3)
        Chassis.SlotCurrent!(5) = ConvertBytesToWordAndScale(0, (Mid(Data, 14, 1)), 4)
        Chassis.SlotPolarity(5) = FormatPolarity(Mid(Data, 15, 1))
        Chassis.SlotVoltage!(5) = ConvertBytesToWordAndScale(Mid(Data, 15, 1), Mid(Data, 16, 1), 3)
        Chassis.SlotCurrent!(6) = ConvertBytesToWordAndScale(0, (Mid(Data, 17, 1)), 4)
        Chassis.SlotPolarity(6) = FormatPolarity(Mid(Data, 18, 1))
        Chassis.SlotVoltage!(6) = ConvertBytesToWordAndScale(Mid(Data, 18, 1), Mid(Data, 19, 1), 3)
        Chassis.SlotCurrent!(7) = ConvertBytesToWordAndScale(0, (Mid(Data, 20, 1)), 4)
        Chassis.SlotPolarity(7) = FormatPolarity(Mid(Data, 21, 1))
        Chassis.SlotVoltage!(7) = ConvertBytesToWordAndScale(Mid(Data, 21, 1), Mid(Data, 22, 1), 3)
        Chassis.SlotCurrent!(8) = ConvertBytesToWordAndScale(0, (Mid(Data, 23, 1)), 4)
        Chassis.SlotPolarity(8) = FormatPolarity(Mid(Data, 24, 1))
        Chassis.SlotVoltage!(8) = ConvertBytesToWordAndScale(Mid(Data, 24, 1), Mid(Data, 25, 1), 3)
        Chassis.SlotCurrent!(9) = ConvertBytesToWordAndScale(0, (Mid(Data, 26, 1)), 4)
        Chassis.SlotPolarity(9) = FormatPolarity(Mid(Data, 27, 1))
        Chassis.SlotVoltage!(9) = ConvertBytesToWordAndScale(Mid(Data, 27, 1), Mid(Data, 28, 1), 3)
        Chassis.SlotCurrent!(10) = ConvertBytesToWordAndScale(0, (Mid(Data, 29, 1)), 4)
        Chassis.SlotPolarity(10) = FormatPolarity(Mid(Data, 30, 1))
        Chassis.SlotVoltage!(10) = ConvertBytesToWordAndScale(Mid(Data, 30, 1), Mid(Data, 31, 1), 10)

        'Uut Supply Power
        Chassis.UutPowerTotal = 0
        For SlotIndex = 1 To 10
            Chassis.UutPowerTotal += ((Chassis.SlotVoltage!(SlotIndex) * Chassis.SlotCurrent!(SlotIndex)))
            If Chassis.SlotCurrent!(SlotIndex) < 0.5 Then
                Chassis.UutPowerTotal += (((Chassis.SlotVoltage!(SlotIndex) * PPU_PRELOAD_CURRENT) * PrimaryChassis.DcvLevel) * FPU_INPUT_POWER_EFFICENCY)
            End If
        Next SlotIndex
        Chassis.UutPowerTotal /= PPU_INPUT_POWER_EFFICENCY

        'ICPU Status
        Chassis.StatusICPU = Asc(Mid(Data, 32, 1))
        CheckFpuStatusByte(Chassis.StatusICPU)

        'Backplane Supply Currents
        Chassis.P5ACurrent = ConvertBytesToWordAndScale(Convert.ToString(Chr(0)), (Mid(Data, 33, 1)), 15)
        Chassis.P5BCurrent = ConvertBytesToWordAndScale(Convert.ToString(Chr(0)), (Mid(Data, 34, 1)), 5)
        Chassis.P12ACurrent = ConvertBytesToWordAndScale(Convert.ToString(Chr(0)), (Mid(Data, 35, 1)), 5)
        Chassis.P24ACurrent = ConvertBytesToWordAndScale(Convert.ToString(Chr(0)), (Mid(Data, 36, 1)), 5)
        Chassis.N2ACurrent = ConvertBytesToWordAndScale(Convert.ToString(Chr(0)), (Mid(Data, 37, 1)), 5)
        Chassis.N52ACurrent = ConvertBytesToWordAndScale(Convert.ToString(Chr(0)), (Mid(Data, 38, 1)), 5)
        Chassis.N24ACurrent = ConvertBytesToWordAndScale(Convert.ToString(Chr(0)), (Mid(Data, 39, 1)), 5)
        Chassis.N12ACurrent = ConvertBytesToWordAndScale(Convert.ToString(Chr(0)), (Mid(Data, 40, 1)), 5)
        Chassis.N2BCurrent = ConvertBytesToWordAndScale(Convert.ToString(Chr(0)), (Mid(Data, 41, 1)), 5)

        'Backplane Supply Voltages
        Chassis.P5AVoltage = ConvertBytesToWordAndScale(Mid(Data, 42, 1), Mid(Data, 43, 1), 3)
        Chassis.P5BVoltage = ConvertBytesToWordAndScale(Mid(Data, 44, 1), Mid(Data, 45, 1), 3)
        Chassis.P12AVoltage = ConvertBytesToWordAndScale(Mid(Data, 46, 1), Mid(Data, 47, 1), 3)
        Chassis.P24AVoltage = ConvertBytesToWordAndScale(Mid(Data, 48, 1), Mid(Data, 49, 1), 3)
        Chassis.N2AVoltage = ConvertBytesToWordAndScale(Mid(Data, 50, 1), Mid(Data, 51, 1), 3)
        Chassis.N52AVoltage = ConvertBytesToWordAndScale(Mid(Data, 52, 1), Mid(Data, 53, 1), 3)
        Chassis.N24AVoltage = ConvertBytesToWordAndScale(Mid(Data, 54, 1), Mid(Data, 55, 1), 3)
        Chassis.N12AVoltage = ConvertBytesToWordAndScale(Mid(Data, 56, 1), Mid(Data, 57, 1), 3)
        Chassis.N2BVoltage = ConvertBytesToWordAndScale(Mid(Data, 58, 1), Mid(Data, 59, 1), 3)

        'Backplane Supply Power
        Chassis.FpuPowerTotal = Chassis.P5ACurrent * Chassis.P5AVoltage
        Chassis.FpuPowerTotal += (Chassis.P5BCurrent * Chassis.P5BVoltage)
        Chassis.FpuPowerTotal += (Chassis.P12ACurrent * Chassis.P12AVoltage)
        Chassis.FpuPowerTotal += (Chassis.P24ACurrent * Chassis.P24AVoltage)
        Chassis.FpuPowerTotal += (Chassis.N2ACurrent * Chassis.N2AVoltage)
        Chassis.FpuPowerTotal += (Chassis.N52ACurrent * Chassis.N52AVoltage)
        Chassis.FpuPowerTotal += (Chassis.N24ACurrent * Chassis.N24AVoltage)
        Chassis.FpuPowerTotal += (Chassis.N12ACurrent * Chassis.N12AVoltage)
        Chassis.FpuPowerTotal += (Chassis.N2BCurrent * Chassis.N2BVoltage)

        'Action Byte
        Chassis.Action2 = Asc(Mid(Data, 60, 1))
        'Chassis Status Byte
        Chassis.ChassisStatus = Asc(Mid(Data, 61, 1))
        'Fan Speed = Ascue / 2
        Chassis.FanSpeed = (Asc(Mid(Data, 62, 1)) / 2)
        'Chassis Temperature
        Chassis.Temperature!(0) = (Asc(Mid(Data, 63, 1)) / 2) - 45
        Chassis.Temperature!(1) = (Asc(Mid(Data, 64, 1)) / 2) - 45
        Chassis.Temperature!(2) = (Asc(Mid(Data, 65, 1)) / 2) - 45
        Chassis.Temperature!(3) = (Asc(Mid(Data, 66, 1)) / 2) - 45
        Chassis.Temperature!(4) = (Asc(Mid(Data, 67, 1)) / 2) - 45
        Chassis.Temperature!(5) = (Asc(Mid(Data, 68, 1)) / 2) - 45
        Chassis.Temperature!(6) = (Asc(Mid(Data, 69, 1)) / 2) - 45
        Chassis.Temperature!(7) = (Asc(Mid(Data, 70, 1)) / 2) - 45
        Chassis.Temperature!(8) = (Asc(Mid(Data, 71, 1)) / 2) - 45
        Chassis.Temperature!(9) = (Asc(Mid(Data, 72, 1)) / 2) - 45
        Chassis.Temperature!(10) = (Asc(Mid(Data, 73, 1)) / 2) - 45
        Chassis.Temperature!(11) = (Asc(Mid(Data, 74, 1)) / 2) - 45
        Chassis.Temperature!(12) = (Asc(Mid(Data, 75, 1)) / 2) - 45
        'Ambient (Intake) Temperature
        Chassis.IntakeTemperature = (Asc(Mid(Data, 76, 1)) / 2) - 45
        'Backplane Data
        Chassis.ChP24V = ConvertBytesToWordAndScale(Mid(Data, 77, 1), Mid(Data, 78, 1), 11)
        Chassis.ChP12V = ConvertBytesToWordAndScale(Mid(Data, 79, 1), Mid(Data, 80, 1), 12)
        Chassis.ChP5V = ConvertBytesToWordAndScale(Mid(Data, 81, 1), Mid(Data, 82, 1), 13)
        Chassis.ChN2V = ConvertBytesToWordAndScale(Mid(Data, 83, 1), Mid(Data, 84, 1), 14)
        Chassis.ChN52V = ConvertBytesToWordAndScale(Mid(Data, 85, 1), Mid(Data, 86, 1), 13)
        Chassis.ChN12V = ConvertBytesToWordAndScale(Mid(Data, 87, 1), Mid(Data, 88, 1), 12)
        Chassis.ChN24V = ConvertBytesToWordAndScale(Mid(Data, 89, 1), Mid(Data, 90, 1), 11)


        'Calculate Rise Per Slot (Absolute Values)
        For SlotIndex = 0 To 12
            Chassis.TempRisePerSlot!(SlotIndex) = Chassis.Temperature!(SlotIndex) - Chassis.IntakeTemperature
        Next SlotIndex

        'External Power Data
        Chassis.VinSinglePhase = ConvertBytesToWordAndScale(0, Mid(Data, 91, 1), 6)
        Chassis.VinThreePhase = ConvertBytesToWordAndScale(0, Mid(Data, 92, 1), 7)
        Chassis.CurrentIn = ConvertBytesToWordAndScale(0, (Mid(Data, 93, 1)), 8)
        Chassis.PowerOk = Asc(Mid(Data, 94, 1))
        CheckPowerStatusByte(Chassis.PowerOk)

        Chassis.DcvLevel = ConvertBytesToWordAndScale(0, (Mid(Data, 95, 1)), 9)
        Chassis.StatusInputPower = Asc(Mid(Data, 96, 1))


        'DME added for new Freedom Power Supply
        'NEW CURRENTS
        Chassis.P12BCurrent = ConvertBytesToWordAndScale(Convert.ToString(Chr(0)), (Mid(Data, 97, 1)), 5)
        Chassis.P24BCurrent = ConvertBytesToWordAndScale(Convert.ToString(Chr(0)), (Mid(Data, 98, 1)), 5)
        Chassis.N52BCurrent = ConvertBytesToWordAndScale(Convert.ToString(Chr(0)), (Mid(Data, 99, 1)), 5)
        Chassis.N24BCurrent = ConvertBytesToWordAndScale(Convert.ToString(Chr(0)), (Mid(Data, 100, 1)), 5)
        Chassis.N12BCurrent = ConvertBytesToWordAndScale(Convert.ToString(Chr(0)), (Mid(Data, 101, 1)), 5)

        'new voltages
        Chassis.P12BVoltage = ConvertBytesToWordAndScale(Mid(Data, 102, 1), Mid(Data, 103, 1), 3)
        Chassis.P24BVoltage = ConvertBytesToWordAndScale(Mid(Data, 104, 1), Mid(Data, 105, 1), 3)
        Chassis.N52BVoltage = ConvertBytesToWordAndScale(Mid(Data, 106, 1), Mid(Data, 107, 1), 3)
        Chassis.N24BVoltage = ConvertBytesToWordAndScale(Mid(Data, 108, 1), Mid(Data, 109, 1), 3)
        Chassis.N12BVoltage = ConvertBytesToWordAndScale(Mid(Data, 110, 1), Mid(Data, 111, 1), 3)

        'Update total power calcs    DME
        Chassis.FpuPowerTotal += (Chassis.P12BCurrent * Chassis.P12BVoltage)
        Chassis.FpuPowerTotal += (Chassis.P24BCurrent * Chassis.P24BVoltage)
        Chassis.FpuPowerTotal += (Chassis.N52BCurrent * Chassis.N52BVoltage)
        Chassis.FpuPowerTotal += (Chassis.N24BCurrent * Chassis.N24BVoltage)
        Chassis.FpuPowerTotal += (Chassis.N12BCurrent * Chassis.N12BVoltage)

        Chassis.FpuPowerTotal /= FPU_INPUT_POWER_EFFICENCY


        'Power Budget
        'Check Power Limitations
        'Calculate Input Power
        If Chassis.VinSinglePhase = 0 And Chassis.VinThreePhase = 0 Then 'DC Power
            If Chassis.DcvLevel <= 28 Then
                If Chassis.DcvLevel > 18 Then
                    InputPower = Chassis.DcvLevel * 100 'Assume 100 AMP or some Constant Input
                End If
            Else
                InputPower = 2800 '2800 Watts During Normal Operation
            End If
        Else
            InputPower = 2800 '2800 Watts During Normal Operation
        End If

        'Calculate Heater Draw
        CheckChassisStatusByte(PrimaryChassis.ChassisStatus)
        If StatHeater1 Then
            Heater += 1
        End If
        If StatHeater2 Then
            Heater += 1
        End If
        CheckChassisStatusByte(SecondaryChassis.ChassisStatus)
        If StatHeater1 Then
            Heater += 1
        End If
        If StatHeater2 Then
            Heater += 1
        End If
        HeaterPower = Heater * ((Chassis.DcvLevel * Chassis.DcvLevel) / HEATER_RES)
        'Calculate Fan Draw
        FanPower = PrimaryChassis.FanSpeed * FAN_SPEED_POWER
        FanPower += (SecondaryChassis.FanSpeed * FAN_SPEED_POWER)
        FanPower += FAN_PDU_MINIMUM_DRAW
        'Calculate User Draw on System
        UserPowerConsumption = (PrimaryChassis.UutPowerTotal) + (PrimaryChassis.FpuPowerTotal)
        UserPowerConsumption += FanPower + HeaterPower + IC_POWER
        'Calculate System Power Usage
        TotalPowerUsage = InputPower - UserPowerConsumption
        'Calculate DC Input Current Level
        If Chassis.DcvLevel = 0 Then Chassis.DcvLevel = 1 'Divide By Zero Trap
        DcInputCurrent = UserPowerConsumption / Chassis.DcvLevel

        'Set Function Value
        FormatSystemMonitorData = Chassis

    End Function


    Sub Main()

        '************************************************************
        '* DME added for USMC requirements to VIPERT  PCR # 48      *
        '************************************************************


        '************************************************************
        '************************************************************
        '* Nomenclature   : VIPERT SYSTEM MONITOR                     *
        '*    DESCRIPTION:                                          *
        '*     This Module is the program code entry point          *
        '************************************************************
        'DebugDwh = True 'Debugging Switch

        'Initialize flag to false
        ModFaultDet = False

        'Initialize error count and timer polling count to 0
        PDUerrCount = 0
        TimePolCount = 0

        Dim commandLineArgs() As String
        commandLineArgs = Environment.GetCommandLineArgs
        For i As Integer = 0 To commandLineArgs.Length() - 1
            If (commandLineArgs(i) = "-Restart") Then
                RestartSysmonFlag = True
            End If
        Next

        For i As Integer = 0 To commandLineArgs.Length() - 1
            If (commandLineArgs(i) = "ReSetup") Then
                ReSetupFlag = True
            End If
        Next

        'Allow only one instance of the system monitor ' 
        If (PrevInstance() And RestartSysmonFlag = False And ReSetupFlag = False) Then Environment.Exit(0)

        Q = Convert.ToString(Chr(34)) 'Init Quote Var

        GetOSVersion() 'Identify Operating System Version
        'Is this a New Software Installation

        If bInstallation() = False Then 'NOT a software installation, execute normal startup

            '10/26/18 JW Removing Set Up Procedures pop up (changed in 1.3.2.0)
            'display setup button for 10 seconds if not a restart
            'If (RestartSysmonFlag = False) Then
            '    Setup_Proc.WaitTimer.Enabled = True
            '    Setup_Proc.ShowDialog()
            '    Setup_Proc.Visible = False
            'End If

            FPU_IgnoreFaults = False 'set flag at sysmon startup

            InitFHDBData()

            'Remove Unused Items from Startup Checklist for Restart
            If (RestartSysmonFlag = True) Then
                RemoveNonRestartChecklistItems()
            End If

            'Initialize VIPERT
            StartUpSystem()

            FHDB_Scheduler() 'Initiate FHDB Scheduler

        End If

    End Sub


    Public Sub Delay(ByVal dSeconds As Double)
        'DESCRIPTION:
        '   Delays for a specified number of seconds.

        System.Threading.Thread.Sleep(dSeconds * 1000)

    End Sub




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
        ' s = the fraction of a day with 10 milli-second resolution.
        'CLng(Now) returns only the number of days (the "d." part).
        'Timer returns the number of seconds since midnight with 10 mSec resoulution.
        'So, the following statement returns a composite double-float number representing
        ' the time since 1900 with 10 mSec resoultion. (no cross-over midnight bug)

        dGetTime = CLng(DateSerial(Year(Now), Month(Now), Microsoft.VisualBasic.Day(Now)).ToOADate) + (Microsoft.VisualBasic.Timer() / SECS_IN_DAY)

    End Function



    Sub ShutDownSysmon()

        Dim SupplySlot As Short
        Dim ABuffer As New StringBuilder(Space(4), 4)
        Dim count As Integer
        Dim retCount As Integer
        Dim ChassisSlot As Short
        Dim ExitFlag As Short
        Dim UserText As String
        Dim MaximumRiseTemp As Single
        Dim FailureMessage As String
        Dim EO_OPT As String

        'check for multiple shutdown calls
        If (frmAppStartup.systemShutdown = True) Then
            Exit Sub
        End If

        frmAppStartup.systemShutdown = True

        'If this is a software installation, do not write an FHDB record
        If bInstall = False Then
            'Write Closing Record to Database       VIPERT-ECP-023    DJoiner 04/26/01
            FHDB_Shutdown()
            'Delay(1) 'Delay Shutdown so Record can be added
        Else 'First Time installation needs shutdown time stamp for zyntehsized shutdown to work
            ValidatedDate("System Startup", "SYSTEM_SHUTDOWN", "Update_Now")
        End If

        Try

            'Shut Down Chassis Power
            'Str 15510  If VEO3 ask user if they want to STOW Collimator
            'EO_OPT = GatherIniFileInformation("System Startup", "EO_OPTION_INSTALLED", "")

            'If bInstall = False Then
            '    If EO_OPT = True Then
            '        Dim msg = "Do you want to STOW the VEO2 Collimator?"
            '        Dim title = "STOW VEO2 Collimator"
            '        Dim style = MsgBoxStyle.YesNo
            '        Dim response = MsgBox(msg, style, title)

            '        If response = MsgBoxResult.Yes Then
            '            SysMenu.StowVEO2Collimator()
            '        End If
            '    End If

            'End If
            MsgBox("Remove SP3 at CIC J5 it can cause the ATS to restart after power down.", MsgBoxStyle.OkOnly)

            'ShowVIPERTDialog(True, "Remove SP3 at CIC J5 it can cause the ATS to restart after power down.")
            ShowVIPERTDialog(False, "Shutting Down FPU...")
            SetFpu(False)
            ShowVIPERTDialog(False, "Resetting PPU...")
            ResetPPU(Check:=False)
            frmDialog.Hide()

            'Stop Data Polling Events
            frmSysMon.tmrDataPoll.Enabled = False
            frmSysMon.Visible = False
            If frmCheckList.Visible = True Then
                frmCheckList.Visible = False
                frmCheckList.Close()
            End If

            'Check Temp Rise Values
            MaximumRiseTemp = GetMaximumTemperatureRise()
            If MaximumRiseTemp > 3 Then
                UserText = "The instrument chassis has not reached the target "
                UserText &= "temperature rise value.  Wait until "
                UserText &= "the countdown indicates that the tester is "
                UserText &= "ready to shut down. If the target value is not "
                UserText &= "achieved in a 15 minute time period, then the "
                UserText &= "shut down sequence will continue. "
                UserText &= "This procedure can be bypassed by pressing the "
                UserText &= "Shut Down button."

                frmSysWait.proCountdown.Value = 0
                frmSysWait.lblValue(0).Text = "3.0"
                frmSysWait.lblValue(1).Text = "30.0"
                SetFanSpeed(1, 100)
                Delay(1)
                SetFanSpeed(2, 100)
                frmSysWait.Refresh()

                '15 Minutes or 3 Degrees within Equalization or User Bypass
                frmSysWait.TempToWaitFor = "MaximumTemperatureRise"
                frmSysWait.dataDumpTimer.Enabled = True
                If (LOGOUT_FROM_SYSMON = True) Then
                    ShowSystemWaitDialog("Log Off", UserText, "Temp. Rise (Degrees Celsius)", 900)
                Else
                    ShowSystemWaitDialog("Shut Down", UserText, "Temp. Rise (Degrees Celsius)", 900)
                End If

                frmSysWait.Close()
            End If

            'Reset Chassis

            ShowVIPERTDialog(False, "Shutting Down System...")
            frmDialog.Refresh()
            ResetChassis(1)
            ResetChassis(2)

            'Prevent PPU Supplies from "blinking" after a clean shut-down
            EnableShutdownFailSafeCommand()
            Delay(1)

            viClose(GpibControllerHandle11)
            viClose(GpibControllerHandle15)
            viClose(GpibControllerHandle0)
            viClose(ChassisControllerHandle1)
            viClose(ChassisControllerHandle2)
            'GpibControllerHandle11.Dispose()
            'GpibControllerHandle15.Dispose()
            'GpibControllerHandle0.Dispose()
            'ChassisControllerHandle1.Dispose()
            'ChassisControllerHandle2.Dispose()
            For SupplySlot = 1 To 10
                'PpuControllerHandle(SupplySlot).Dispose()
                viClose(PpuControllerHandle(SupplySlot))
            Next SupplySlot
        Catch ex As Exception

        End Try

        'Terminate the cicl service
        Dim service As ServiceController = New ServiceController("CICLKernel")
        If service.Status.Equals(ServiceControllerStatus.Running) Then
            service.Stop()
        End If

        'Kill RFMS System if running
        'Try
        '    Dim RFMSservice As ServiceController = New ServiceController("RFMS")
        '    If RFMSservice.Status.Equals(ServiceControllerStatus.Running) Then
        '        RFMSservice.Stop()
        '    End If

        'Catch ex As Exception

        'End Try

        frmAppStartup.ShutdownBlockReasonDestroy(frmAppStartup.Handle)

        'Shutdown Windows
        ExitWindowsNT()

    End Sub




    Sub TimerPollingEvent()

        Dim Status As Short
        Dim retCount As Integer
        Dim Data As String
        Dim Bit28Volt As Short
        Dim count As Integer
        Dim ResourceName As String
        Dim FailureMessage As String
        Dim SlotIndex As Short
        Dim ABuffer As New StringBuilder(Space(4), 4)
        
        Dim Buffer As String = Space(255)
        Dim A As String
        Dim CheckData As Short
        Dim ErrCondition As String
        Static CommTimeout As Short = 0
        Static bProbeEdge As Boolean 'Used to identify consecutive probe events

        '***Get Status Byte***
RetryChassiStatus:
        '***Get Data Dump***
        RetValue = viReadSTB(GpibControllerHandle0, Status)
        If (RetValue <> 0) Then
            If ((CommTimeout > 120) And (FPU_IgnoreFaults = False)) Then 'Timeout has occured, ignore timeout error if ignoreFaults is true
                OrderlyShutdown("Communication Error", "ATS System", 3001, 240)
            Else
                CommTimeout += 1
                GoTo RetryChassiStatus
            End If
        End If

        If DebugDwh = True Then
            Status = GetStatusDebug()
        End If
        WriteStatusByte(Status) 'Write To Caption For Probe Button

        Dim rmSession As Integer = 0
        Dim handle As Integer = 0
        RetValue = viRead(GpibControllerHandle15, Buffer, 255, retCount)
        If DebugDwh = True Then
            Buffer = SendStringOfDebugData()
            retCount = 111
            'Uncomment Next Line for Debugging
            'PrintDataDump (Buffer$)
        End If
        Data = Mid(Buffer.ToString(), 1, retCount)
        If Mid(Data, 1, 1) = Convert.ToString(Chr(0)) Or retCount <> 111 Then
            CommTimeout += 1 'Add to Timeout Value
            Exit Sub
        Else            '
            CommTimeout = 0 'Reset Timeout
        End If

        '***Evaluate Status Byte***
        If ((Status And &HF0) = &HB0) Then
            CheckActionByte(CStr(Status)) 'Format Action Byte
            'Check For A Probe Event
            If ActProbeEvent Then
                ChangeIndicatorState(frmSysMon.cwbProbe, False)
                bProbeEdge = False
            Else
                ChangeIndicatorState(frmSysMon.cwbProbe, True)
                If Not bProbeEdge Then
                    OnProbeSendEnter() 'For Input, Go-NoGo execution
                    OnProbeSendF2() 'For Manual Intervention execution
                End If
                Beep()
                bProbeEdge = True
            End If
            'Check Chassis Power Switch
            If ActVolt28Ok Then
                ChangeIndicatorState(frmSysMon.cwb28VOk, True)
                If PowerSwitch Then
                    PowerSwitch = False 'Reset Flag
                    'Check For Power Hazards
                    ErrCondition = ChassisBackplaneSupplyCheck()
                    If ErrCondition <> "" Then
                        MsgBox(ErrCondition)
                        ShowVIPERTDialog(False, "Error: FPU will not start")
                        Delay(5)
                        frmDialog.Close()
                        AllPowerOff = False
                        Exit Sub
                    End If
                    ShowVIPERTDialog(False, "Resetting Primary Chassis...")
                    ResetChassis(1)
                    ShowVIPERTDialog(False, "Resetting Secondary Chassis...")
                    ResetChassis(2)
                    ShowVIPERTDialog(False, "Starting FPU...")
                    SetFpu(True)
                    InitIniTimeStamp() 'Reset Warm-up Time Stamp
                    ShowVIPERTDialog(False, "Initializing Resource Manager...")
                    A = RunResMan() 'Run Software Resource Manager
                    frmCheckList.Close() 'Free Memory (This form is loaded in the RunResMan routine for it's callback custom control)
                    ShowVIPERTDialog(False, "Chassis Restart Complete")
                    Delay(1)
                    WriteChassisStateToIniFile(True)
                    AllPowerOff = False
                    frmDialog.Close()

                    '**************begin added per customer request
                    MessageBox.Show("Sysmon Will Now Restart", "ATS Notification")
                    Dim newSysmonProcess As New Process()
                    newSysmonProcess.StartInfo.FileName = "C:\Program Files (x86)\ATS\Sysmon.exe"
                    newSysmonProcess.StartInfo.Arguments = "-Restart"
                    newSysmonProcess.Start()
                    'hide first sysmon tray icon
                    frmToolbar.trayIcon.Visible = False
                    'write shutdown event to fhdb
                    FHDB_Shutdown()
                    Environment.Exit(0)
                    'ShortStart()
                    FPU_IgnoreFaults = False
                    '************end added per customer request

                End If
            Else
                ChangeIndicatorState(frmSysMon.cwb28VOk, False)
                If AllPowerOff = False Then
                    PowerSwitch = True
                    AllPowerOff = True
                    ShowVIPERTDialog(False, "Shutting Down Instrument Chassis...")
                    CloseIniTimeStamp() 'Report Shutdown Time
                    SetFpu(False)
                    WriteChassisStateToIniFile(False)
                    TerminateInstrumentApplications()
                    Delay(1)
                    frmDialog.Close()
                End If
            End If
            'Check ITA/SAIF Receiver Switch Power Switch
            If ActReceiverEvent Then 'RCVR Handle Open
                ChangeIndicatorState(frmSysMon.cwbRcvrSwitch, False)
                If RcvrActivated = True Then
                    EventReceiverActivation() 'Pop Up Warning To User if PPU has active supplies
                    RcvrActivated = False 'Reset Flag
                End If
            Else                'RCVR Handle Closed
                ChangeIndicatorState(frmSysMon.cwbRcvrSwitch, True)
                RcvrActivated = True
            End If
        End If

        '***Evaluate Data Dump***
        If Truncate(Asc(Mid(Buffer.ToString(), 60, 1))) And 224 Then
            'Invalid Data Do Not Use this data dump
            Exit Sub
        End If

        If Truncate(Asc(Mid(Buffer.ToString(), 60, 1))) And 8 Then
            SecondaryChassis = FormatSystemMonitorData(Buffer.ToString()) 'Get Chassis #2 Data
            AlarmWarningRoutineHere() 'Check Alarm/Warning Conditions
            'If Form is visible then update GUI
            If frmSysMon.Visible = True Then 'Update All
                UpdateSysMonGui(SecondaryChassis)
                frmSysMon.lblActivity.Text = "Secondary Chassis"
            Else                'Just Update The Voltages for DDE Link to Switching Front Panel
                For SlotIndex = 1 To 10
                    frmSysMon.lblUutVolt(SlotIndex).Text = VB6.Format(SecondaryChassis.SlotVoltage!(SlotIndex), "#0.00")
                Next SlotIndex
            End If
            AdjustHeaters()
        Else
            PrimaryChassis = FormatSystemMonitorData(Buffer.ToString()) 'Get Chassis #1 Data
            AlarmWarningRoutineHere() 'Check Alarm/Warning Conditions
            'If Form is visible then update GUI
            If frmSysMon.Visible = True Then 'Update All
                UpdateSysMonGui(PrimaryChassis)
                frmSysMon.lblActivity.Text = "Primary Chassis"
            Else                'Just Update The Voltages for DDE Link to Switching Front Panel
                For SlotIndex = 1 To 10
                    frmSysMon.lblUutVolt(SlotIndex).Text = VB6.Format(PrimaryChassis.SlotVoltage!(SlotIndex), "#0.00")
                Next SlotIndex
            End If
            AdjustHeaters()
        End If

        'If a PDU error doesn't occur 3 times in 12 pulling events then reset the error counts
        'dme pcr vsys 308
        If TimePolCount = 12 Then
            PDUerrCount = 0
            TimePolCount = 0
        End If
        TimePolCount += 1

    End Sub




    Sub CommandSupplyReset(ByVal Supply As Short)

        Dim count As Integer
        Dim B1 As Short
        Dim B2 As Short
        Dim B3 As Short
        
        Dim ABuffer As String = Space(4)
        Dim retCount As Integer

        B1 = 16 + Supply
        B2 = 128
        B3 = 128
        'Send Command
        ABuffer = (Convert.ToString(Chr(B1)) & Convert.ToString(Chr(B2)) & Convert.ToString(Chr(B3)) + Convert.ToString(Chr(0)))
        count = 4
        RetValue = viWrite(PpuControllerHandle(Supply), ABuffer, count, retCount)
        Delay(0.1)

    End Sub


    Sub UpdateSysMonGui(ByVal Chassis As ChassisData)

        Dim FloodVal As Double
        Dim PpuSupplyFormatPolarity As String
        Const FAN_THRESH As Short = 75

        Dim SlotIndex As Short
        ''ActionByte (Ignore For Now)
        'Chassis.Action1% = Asc(Mid$(Data$, 1, 1))
        ''Power Supply Current and Voltage
        For SlotIndex = 1 To 10
            frmSysMon.lblUutCur(SlotIndex).Text = VB6.Format(Chassis.SlotCurrent!(SlotIndex), "#0.00")
        Next SlotIndex

        For SlotIndex = 1 To 10
            If (Chassis.SlotPolarity(SlotIndex) = True) Then
                PpuSupplyFormatPolarity = "\+#0.00" 'Normal Polarity
            Else
                PpuSupplyFormatPolarity = "\-#0.00" 'Reversed Polarity
            End If
            frmSysMon.lblUutVolt(SlotIndex).Text = VB6.Format(Chassis.SlotVoltage!(SlotIndex), PpuSupplyFormatPolarity)
        Next SlotIndex

        ''UUT Power Supply Totals
        frmSysMon.lblSysPower(3).Text = VB6.Format(Chassis.UutPowerTotal, " 000.00 \W")
        ''ICPU Status (Fixed Power Unit)
        ''Backplane Supply Currents

        'DME changes to implement new Freedom Power Supply
        frmSysMon.lblBackCurr(0).Text = VB6.Format(Chassis.P24ACurrent, "#0.00")
        frmSysMon.lblBackCurr(1).Text = VB6.Format(Chassis.P12ACurrent, "#0.00")
        frmSysMon.lblBackCurr(2).Text = VB6.Format(Chassis.P5ACurrent, "#0.00")
        frmSysMon.lblBackCurr(3).Text = VB6.Format(Chassis.N2ACurrent, "#0.00")
        frmSysMon.lblBackCurr(4).Text = VB6.Format(Chassis.N52ACurrent, "#0.00")
        frmSysMon.lblBackCurr(5).Text = VB6.Format(Chassis.N12ACurrent, "#0.00")
        frmSysMon.lblBackCurr(6).Text = VB6.Format(Chassis.N24ACurrent, "#0.00")

        frmSysMon.lblBackCurr(7).Text = VB6.Format(Chassis.P24BCurrent, "#0.00")
        frmSysMon.lblBackCurr(8).Text = VB6.Format(Chassis.P12BCurrent, "#0.00")
        frmSysMon.lblBackCurr(9).Text = VB6.Format(Chassis.P5BCurrent, "#0.00")
        frmSysMon.lblBackCurr(10).Text = VB6.Format(Chassis.N2BCurrent, "#0.00")
        frmSysMon.lblBackCurr(11).Text = VB6.Format(Chassis.N52BCurrent, "#0.00")
        frmSysMon.lblBackCurr(12).Text = VB6.Format(Chassis.N12BCurrent, "#0.00")
        frmSysMon.lblBackCurr(13).Text = VB6.Format(Chassis.N24BCurrent, "#0.00")

        ''Backplane Supply Voltages
        frmSysMon.lblBackVolt(0).Text = VB6.Format(Chassis.P24AVoltage, "\+#0.00")
        frmSysMon.lblBackVolt(1).Text = VB6.Format(Chassis.P12AVoltage, "\+#0.00")
        frmSysMon.lblBackVolt(2).Text = VB6.Format(Chassis.P5AVoltage, "\+#0.00")
        frmSysMon.lblBackVolt(3).Text = VB6.Format(Chassis.N2AVoltage, "\-#0.00")
        frmSysMon.lblBackVolt(4).Text = VB6.Format(Chassis.N52AVoltage, "\-#0.00")
        frmSysMon.lblBackVolt(5).Text = VB6.Format(Chassis.N12AVoltage, "\-#0.00")
        frmSysMon.lblBackVolt(6).Text = VB6.Format(Chassis.N24AVoltage, "\-#0.00")

        frmSysMon.lblBackVolt(7).Text = VB6.Format(Chassis.P24BVoltage, "\+#0.00")
        frmSysMon.lblBackVolt(8).Text = VB6.Format(Chassis.P12BVoltage, "\+#0.00")
        frmSysMon.lblBackVolt(9).Text = VB6.Format(Chassis.P5BVoltage, "\+#0.00")
        frmSysMon.lblBackVolt(10).Text = VB6.Format(Chassis.N2BVoltage, "\-#0.00")
        frmSysMon.lblBackVolt(11).Text = VB6.Format(Chassis.N52BVoltage, "\-#0.00")
        frmSysMon.lblBackVolt(12).Text = VB6.Format(Chassis.N12BVoltage, "\-#0.00")
        frmSysMon.lblBackVolt(13).Text = VB6.Format(Chassis.N24BVoltage, "\-#0.00")

        ''Backplane Supply Totals
        frmSysMon.lblSysPower(4).Text = VB6.Format(Chassis.FpuPowerTotal, " 000.00 \W")
        ''Action Byte
        ''Chassis Status Byte
        'For Primary Chassis
        CheckChassisStatusByte(PrimaryChassis.ChassisStatus)
        If StatSelfTest Then
            ChangeIndicatorState(frmSysMon.cwbChasSelfTestIndicator(1), True)
        Else
            ChangeIndicatorState(frmSysMon.cwbChasSelfTestIndicator(1), False)
        End If
        If StatHeater2 Then
            frmSysMon.cwbHeaterIndicatorOO_1.Value = True
        Else
            frmSysMon.cwbHeaterIndicatorOO_1.Value = False
        End If
        If StatHeater1 Then
            frmSysMon.cwbHeaterIndicatorOO_0.Value = True
        Else
            frmSysMon.cwbHeaterIndicatorOO_0.Value = False
        End If

        'PCR VSys - 326 fixing the fan displays to key off the air flow sensor bit
        If StatAirFlowSensor3 Then
            ChangeIndicatorState(frmSysMon.cwbAirFlowIndicatorPF_3, True)
        Else
            'If PrimaryChassis.FanSpeed% > FAN_THRESH% Or PrimaryChassis.FanSpeed% = 0 Then
            ChangeIndicatorState(frmSysMon.cwbAirFlowIndicatorPF_3, False)
            'Else
            '    ChangeIndicatorState frmSysMon.cwbAirFlowIndicatorPF(3), True
            'End If
        End If
        If StatAirFlowSensor2 Then
            ChangeIndicatorState(frmSysMon.cwbAirFlowIndicatorPF_2, True)
        Else
            'If PrimaryChassis.FanSpeed% > FAN_THRESH% Or PrimaryChassis.FanSpeed% = 0 Then
            ChangeIndicatorState(frmSysMon.cwbAirFlowIndicatorPF_2, False)
            'Else
            '    ChangeIndicatorState frmSysMon.cwbAirFlowIndicatorPF(2), True
            'End If
        End If
        If StatAirFlowSensor1 Then
            ChangeIndicatorState(frmSysMon.cwbAirFlowIndicatorPF_1, True)
        Else
            'If PrimaryChassis.FanSpeed% > FAN_THRESH% Or PrimaryChassis.FanSpeed% = 0 Then
            ChangeIndicatorState(frmSysMon.cwbAirFlowIndicatorPF_1, False)
            'Else
            '    ChangeIndicatorState frmSysMon.cwbAirFlowIndicatorPF(1), True
            'End If
        End If
        'For Secondary Chassis
        CheckChassisStatusByte(CStr(SecondaryChassis.ChassisStatus))
        If StatSelfTest Then
            ChangeIndicatorState(frmSysMon.cwbChasSelfTestIndicator(2), True)
        Else
            ChangeIndicatorState(frmSysMon.cwbChasSelfTestIndicator(2), False)
        End If
        If StatHeater2 Then
            frmSysMon.cwbHeaterIndicatorOO_3.Value = True
        Else
            frmSysMon.cwbHeaterIndicatorOO_3.Value = False
        End If
        If StatHeater1 Then
            frmSysMon.cwbHeaterIndicatorOO_2.Value = True
        Else
            frmSysMon.cwbHeaterIndicatorOO_2.Value = False
        End If
        If StatAirFlowSensor3 Then
            ChangeIndicatorState(frmSysMon.cwbAirFlowIndicatorPF_6, True)
        Else
            'If SecondaryChassis.FanSpeed% > FAN_THRESH% Or SecondaryChassis.FanSpeed% = 0 Then
            ChangeIndicatorState(frmSysMon.cwbAirFlowIndicatorPF_6, False)
            'Else
            '    ChangeIndicatorState frmSysMon.cwbAirFlowIndicatorPF(6), True
            'End If
        End If
        If StatAirFlowSensor2 Then
            ChangeIndicatorState(frmSysMon.cwbAirFlowIndicatorPF_5, True)
        Else
            'If SecondaryChassis.FanSpeed% > FAN_THRESH% Or SecondaryChassis.FanSpeed% = 0 Then
            ChangeIndicatorState(frmSysMon.cwbAirFlowIndicatorPF_5, False)
            'Else
            '    ChangeIndicatorState frmSysMon.cwbAirFlowIndicatorPF(5), True
            'End If
        End If
        If StatAirFlowSensor1 Then
            ChangeIndicatorState(frmSysMon.cwbAirFlowIndicatorPF_4, True)
        Else
            'If SecondaryChassis.FanSpeed% > FAN_THRESH% Or SecondaryChassis.FanSpeed% = 0 Then
            ChangeIndicatorState(frmSysMon.cwbAirFlowIndicatorPF_4, False)
            'Else
            '    ChangeIndicatorState frmSysMon.cwbAirFlowIndicatorPF(4), True
            'End If
        End If
        'Fan Speed
        If (PrimaryChassis.FanSpeed > 100) Then
            PrimaryChassis.FanSpeed = 100
        Else
            If (PrimaryChassis.FanSpeed < 0) Then
                PrimaryChassis.FanSpeed = 0
            End If
        End If
        If (SecondaryChassis.FanSpeed > 100) Then
            SecondaryChassis.FanSpeed = 100
        Else
            If (SecondaryChassis.FanSpeed < 0) Then
                SecondaryChassis.FanSpeed = 0
            End If
        End If
        frmSysMon.panFanSpeed_0.Value = PrimaryChassis.FanSpeed
        frmSysMon.panFanSpeed_1.Value = SecondaryChassis.FanSpeed
        frmSysMon.txtFanSpeedValue(1).Text = PrimaryChassis.FanSpeed & "%"
        frmSysMon.txtFanSpeedValue(2).Text = SecondaryChassis.FanSpeed & "%"
        'Chassis Temperature / 'Rise Per Slot
        For SlotIndex = 0 To 12
            frmSysMon.lblExhaustValue(SlotIndex).Text = VB6.Format(PrimaryChassis.Temperature!(SlotIndex), "#0.0")
            'If Off Scale Set To Max
            If (PrimaryChassis.TempRisePerSlot(SlotIndex)) > 30 Then
                PrimaryChassis.TempRisePerSlot(SlotIndex) = 30
            End If
            If (PrimaryChassis.TempRisePerSlot(SlotIndex)) < 0 Then
                PrimaryChassis.TempRisePerSlot(SlotIndex) = 0
            End If
            frmSysMon.cwsSlotRiseActual(SlotIndex).Value = PrimaryChassis.TempRisePerSlot(SlotIndex)
            'Find Diff Between Threshold and Rise
            Select Case ((PrimaryChassis.TempRisePerSlot(SlotIndex)) / (TempertureThreshold(SlotIndex))) * 100
                Case 0 To 89
                    'Green
                    frmSysMon.cwsSlotRiseActual(SlotIndex).FillColor = System.Drawing.Color.Green
                Case 90 To 100
                    'Yellow
                    frmSysMon.cwsSlotRiseActual(SlotIndex).FillColor = System.Drawing.Color.Yellow
                Case Is > 100
                    'Red
                    frmSysMon.cwsSlotRiseActual(SlotIndex).FillColor = System.Drawing.Color.Red
            End Select
        Next SlotIndex
        For SlotIndex = 13 To 25
            frmSysMon.lblExhaustValue(SlotIndex).Text = VB6.Format(SecondaryChassis.Temperature!(SlotIndex - 13), "#0.0")
            'If Off Scale Set To Max
            If SecondaryChassis.TempRisePerSlot(SlotIndex - 13) > 30 Then
                SecondaryChassis.TempRisePerSlot(SlotIndex - 13) = 30
            End If
            If (SecondaryChassis.TempRisePerSlot(SlotIndex - 13)) < 0.0 Then
                SecondaryChassis.TempRisePerSlot(SlotIndex - 13) = 0
            End If
            frmSysMon.cwsSlotRiseActual(SlotIndex).Value = SecondaryChassis.TempRisePerSlot(SlotIndex - 13)
            'Find Diff Between Threshold and Rise
            Select Case ((SecondaryChassis.TempRisePerSlot(SlotIndex - 13)) / (TempertureThreshold(SlotIndex))) * 100
                Case 0 To 89
                    'Green
                    frmSysMon.cwsSlotRiseActual(SlotIndex).FillColor = System.Drawing.Color.Green
                Case 90 To 100
                    'Yellow
                    frmSysMon.cwsSlotRiseActual(SlotIndex).FillColor = System.Drawing.Color.Yellow
                Case Is > 100
                    'Red
                    frmSysMon.cwsSlotRiseActual(SlotIndex).FillColor = System.Drawing.Color.Red
            End Select
        Next SlotIndex
        ''Ambient (Intake) Temperature
        frmSysMon.lblIntakeValue(0).Text = VB6.Format(PrimaryChassis.IntakeTemperature, "#0.0")

        frmSysMon.lblIntakeValue(1).Text = VB6.Format(SecondaryChassis.IntakeTemperature, "#0.0")

        ''Backplane Data (Diagnostic Only) 50 % Tolerance
        frmSysMon.lblLevel(0).Text = VB6.Format(PrimaryChassis.ChP24V, "\+#0.00")
        frmSysMon.lblLevel(1).Text = VB6.Format(PrimaryChassis.ChP12V, "\+#0.00")
        frmSysMon.lblLevel(2).Text = VB6.Format(PrimaryChassis.ChP5V, "\+#0.00")
        frmSysMon.lblLevel(3).Text = VB6.Format(PrimaryChassis.ChN2V, "\-#0.00")
        frmSysMon.lblLevel(4).Text = VB6.Format(PrimaryChassis.ChN52V, "\-#0.00")
        frmSysMon.lblLevel(5).Text = VB6.Format(PrimaryChassis.ChN12V, "\-#0.00")
        frmSysMon.lblLevel(6).Text = VB6.Format(PrimaryChassis.ChN24V, "\-#0.00")
        frmSysMon.lblLevel(7).Text = VB6.Format(SecondaryChassis.ChP24V, "\+#0.00")
        frmSysMon.lblLevel(8).Text = VB6.Format(SecondaryChassis.ChP12V, "\+#0.00")
        frmSysMon.lblLevel(9).Text = VB6.Format(SecondaryChassis.ChP5V, "\+#0.00")
        frmSysMon.lblLevel(10).Text = VB6.Format(SecondaryChassis.ChN2V, "\-#0.00")
        frmSysMon.lblLevel(11).Text = VB6.Format(SecondaryChassis.ChN52V, "\-#0.00")
        frmSysMon.lblLevel(12).Text = VB6.Format(SecondaryChassis.ChN12V, "\-#0.00")
        frmSysMon.lblLevel(13).Text = VB6.Format(SecondaryChassis.ChN24V, "\-#0.00")
        ''External Power Data
        frmSysMon.lblInPower(0).Text = VB6.Format(Str(Chassis.VinSinglePhase), "00.00 \V")
        frmSysMon.lblInPower(1).Text = VB6.Format(Str(Chassis.VinThreePhase), "00.00 \V")
        frmSysMon.lblInPower(3).Text = VB6.Format(Str(Chassis.DcvLevel), "00.00 \V")
        frmSysMon.lblInPower(4).Text = VB6.Format(Str(DcInputCurrent), "00.00 \A")

        ''PowOkStatus
        'CheckPowerStatusByte (Chassis.PowerOk%)

        'PowerStatus28FailHigh
        If PowerStatusAc <> 0 Then
            If PowerStatusSingle = 0 Then
                frmSysMon.lblSourceMode.Text = "Source Mode: Single Phase"
                frmSysMon.lblInPower(2).Text = VB6.Format(Str(Chassis.CurrentIn), "00.00 \A")
            Else
                frmSysMon.lblSourceMode.Text = "Source Mode: Three Phase"
                frmSysMon.lblInPower(2).Text = VB6.Format(Str(Chassis.CurrentIn), "00.00 \A")
            End If
        End If

        If PowerStatusDc <> 0 Then
            frmSysMon.lblSourceMode.Text = "Source Mode: DC"
            frmSysMon.lblInPower(2).Text = "0 A"
        End If

        If PowerStatus2800Watt Then
            ChangeIndicatorState(frmSysMon.cwbPowerConverters, False)
        Else
            ChangeIndicatorState(frmSysMon.cwbPowerConverters, True)
        End If

        'PowerStatusPhase1
        If PowerStatusPhase1 Then
            frmSysMon.cwbHamPhase_1.Value = False
        Else
            frmSysMon.cwbHamPhase_1.Value = True
        End If

        'PowerStatusPhase2
        If PowerStatusPhase2 Then
            frmSysMon.cwbHamPhase_2.Value = False
        Else
            frmSysMon.cwbHamPhase_2.Value = True
        End If
        'PowerStatusPhase3
        If PowerStatusPhase3 Then
            frmSysMon.cwbHamPhase_3.Value = False
        Else
            frmSysMon.cwbHamPhase_3.Value = True
        End If


        'Input Power Status
        ' Due to the changes in phases the input power will differ pending the phase
        '        therfore the display will be based off the results of the alarm conditions for input power
        '        If the input power is bad the FPU_faulted flag will be triggered to true, the FPU_faulted flag
        '        uses the same perameters in the alarm conditions as it does here and is done prior to updating the GUI
        If FPU_faulted = False Then '((Chassis.StatusInputPower%) And &H6) = 0 Then 'Power OK
            ChangeIndicatorState(frmSysMon.cwbInputPower, True)
        Else            'Low Fail or High Fail?

            'Alarm Condition routine will inform the user if it is high or low failure
            ChangeIndicatorState(frmSysMon.cwbInputPower, False)

            '        If ((Chassis.StatusInputPower%) And &H4) = 4 Then 'Input Power Low Condition
            '            ChangeIndicatorState frmSysMon.cwbInputPower, False
            '        End If
            '        If ((Chassis.StatusInputPower%) And &H2) = 2 Then 'Input Power High Condition
            '            ChangeIndicatorState frmSysMon.cwbInputPower, False
            '        End If
        End If


        'Update Power Budget
        frmSysMon.lblSysPower(5).Text = CStr(InputPower) & " W"
        frmSysMon.lblSysPower(2).Text = CStr(HeaterPower) & " W"
        frmSysMon.lblSysPower(1).Text = CStr(FanPower) & " W"
        frmSysMon.lblSysPower(6).Text = CStr(UserPowerConsumption) & " W"
        frmSysMon.lblSysPower(7).Text = CStr(TotalPowerUsage) & " W"
        If InputPower = 0 Then
            InputPower = 1 'Divide By Zero Error Trap
            UserPowerConsumption = 1 'Overflow Trap
        End If
        FloodVal = (UserPowerConsumption / InputPower) * 100
        If FloodVal > 100 Then FloodVal = 100
        If FloodVal < 0 Then FloodVal = 0
        frmSysMon.panPower.Text = CInt(FloodVal)
        frmSysMon.panPower.Value = CInt(FloodVal)
        frmSysMon.lblPower.Text = VB6.Format(FloodVal, "#0\%")

    End Sub



    Public Sub ExitWindowsNT()
        Const curOnErrorGoToLabel_Default As Integer = 0
        Const curOnErrorGoToLabel_ErrorHandler As Integer = 1
        Dim vOnErrorGoToLabel As Integer = curOnErrorGoToLabel_Default
        Dim nRetVal As Integer = 0

        frmDialog.lblDialog.Text = "Shutting Down"
        frmDialog.cmdClose.Text = "Cancel Shutdown"
        frmDialog.cmdClose.Visible = True
        frmDialog.Show() 'Show the Shutdown notice while system resumes shutdown
        frmDialog.Refresh()

        AdjustToken()

        If (LOGOUT_FROM_SYSMON = True) Then
            nRetVal = ExitWindowsEx(EWX_LOGOFF, Nothing)
        Else
            nRetVal = ExitWindowsEx(EWX_POWEROFF, Nothing)
        End If

        Environment.Exit(0)

    End Sub



    Sub SendStatusBarMessage(ByVal MessageString As String)
        '************************************************************
        '* Virtual Instrument Portable Equipment Repair/Test(VIPERT)*
        '*                                                          *
        '* Nomenclature   : SAIS: DMM Front Panel: Main:            *
        '************************************************************
        'MessageString$ is printed in the Instrument Status Bar

        If MessageString <> frmCheckList.sbrUserInformation.Panels(0).Text Then
            frmCheckList.sbrUserInformation.Panels(0).Text = MessageString
        End If

    End Sub



    Sub CenterChildForm(ByVal ParentForm As Form, ByVal Childform As Form)
        '************************************************************
        '************************************************************
        '* Nomenclature   : VIPERT SYSTEM LOG MENU                    *
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



    Sub SetOptTemp()

        Dim B1 As Short
        Dim B2 As Short
        Dim B3 As Short
        Dim Acount As Integer
        Dim Handle As Integer
        Dim Buffer As String
        Dim retCount As Integer

        Acount = 3
        B3 = &HFF
        'Set New Temp Target
        B1 = &H4F
        B2 = 25 + 55
        Handle = ChassisControllerHandle1
        Buffer = Convert.ToString(Chr(B1)) & Convert.ToString(Chr(B2)) & Convert.ToString(Chr(B3))
        RetValue = viWrite(Handle, Buffer, Acount, retCount)

        'Set New Temp target
        B1 = &H4F
        B2 = 25 + 55
        Handle = ChassisControllerHandle2
        Buffer = Convert.ToString(Chr(B1)) & Convert.ToString(Chr(B2)) & Convert.ToString(Chr(B3))
        RetValue = viWrite(Handle, Buffer, Acount, retCount)


    End Sub


    Sub ReleaseHandles()
        '***************************************************************************
        '* This process is created to release handles when shutting down the FPU's
        '* DME PCR 340
        '* 06/19/08
        '****************************************************************************



        Dim iIndex As Short
        
        Dim i As Short
        Dim InitStatus As Integer, ErrorStatus As Integer, Ret As Integer
        Dim lSystErr As Integer

        ' release PPU handles
        For i = 1 To 10
            If PpuControllerHandle(i) <> 0 Then
                ErrorStatus = viClose(PpuControllerHandle(i))
                'PpuControllerHandle(i).Dispose()
                PpuControllerHandle(i) = 0
            End If
        Next i

        'Release DTS handles
        If viSession > 0 Then
            lSystErr = terM9_close(viSession)
        End If
        viSession = 0

        If RmSession <> 0 Then ' release session handle
            lSystErr = viClose(RmSession)
            RmSession = 0
        End If

        'Close the CICL
        Ret = TerminateProcess(pInfo.hProcess, 0)
        Ret = CloseHandle(pInfo.hThread)
        Ret = CloseHandle(pInfo.hProcess)

        'close RFMS.exe
        'KillappNT("RFMS.exe")

        MsgBox("FPU's Successfully Terminated", MsgBoxStyle.OkOnly)

    End Sub

    Function KillappNT(ByVal myName As String) As Object
        Dim uProcess As PROCESSENTRY32
        Dim rProcessFound As Integer
        Dim hSnapshot As Integer
        Dim szExename As String
        Dim exitCode As Integer
        Dim myProcess As Integer
        
        Dim AppKill As Boolean
        Dim appCount As Short
        Dim i As Short
        Try ' On Error GoTo Finish
            appCount = 0
            uProcess.dwSize = Len(uProcess)
            hSnapshot = CreateToolhelpSnapshot(TH32CS_SNAPPROCESS, 0)
            rProcessFound = ProcessFirst(hSnapshot, uProcess)
            Do While rProcessFound
                i = InStr(1, uProcess.szExeFile.ToString(), Convert.ToString(Chr(0)), Microsoft.VisualBasic.CompareMethod.Binary)
                szExename = LCase(Strings.Left(uProcess.szExeFile.ToString(), i - 1))
                If Strings.Right(szExename, Len(myName)) = LCase(myName) Then
                    KillAppReturn = True
                    appCount += 1
                    myProcess = OpenProcess(1, -1, uProcess.th32ProcessID)
                    AppKill = TerminateProcess(myProcess, 0)
                    CloseHandle(myProcess)
                End If
                rProcessFound = ProcessNext(hSnapshot, uProcess)
            Loop
            CloseHandle(hSnapshot)
        Catch   ' Finish:
            KillAppReturn = False
        End Try
    End Function


    Public Function sWinError(ByVal nLastDLLError As Integer) As String
        sWinError = ""
        '******************************************************
        '**     Returns the Last DLL Error Description      ***
        '******************************************************
        Dim lpBuffer As String 'Buffer to return Error Description
        Dim nCount As Integer 'Number of TCHARs stored in the buffer

        'Define Error Message Constants
        Const FORMAT_MESSAGE_IGNORE_INSERTS As Short = &H200
        Const FORMAT_MESSAGE_FROM_SYSTEM As Short = &H1000

        ' Return the error message associated with LastDLLError:
        lpBuffer = StrDup(256, Chr(0))
        nCount = FormatMessage(FORMAT_MESSAGE_FROM_SYSTEM Or FORMAT_MESSAGE_IGNORE_INSERTS, 0, nLastDLLError, 0, lpBuffer, Len(lpBuffer), 0)
        If nCount Then
            sWinError = Strings.Left(lpBuffer, nCount)
        End If

    End Function

    Function PrevInstance() As Boolean
        If UBound(Diagnostics.Process.GetProcessesByName _
           (Diagnostics.Process.GetCurrentProcess.ProcessName)) _
           > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function ObjectToDouble(ByVal v As Object) As Double
        Dim d As Double
        If TypeOf v Is String Then
            d = Val(v)
        ElseIf TypeOf v Is DateTime Then
            d = CType(v, DateTime).ToOADate()
        Else
            d = Convert.ToDouble(v)
        End If
        Return d
    End Function

    Private Sub AdjustToken()

        Const TOKEN_ADJUST_PRIVILEGES As Int32 = &H20

        Const TOKEN_QUERY As Int32 = &H8

        Const SE_PRIVILEGE_ENABLED As Int32 = &H2

        Dim hdlProcessHandle As IntPtr

        Dim hdlTokenHandle As Int32

        Dim tmpLuid As LUID

        Dim tkp As TOKEN_PRIVILEGES

        Dim tkpNewButIgnored As TOKEN_PRIVILEGES

        Dim lBufferNeeded As Int32

        hdlProcessHandle = Process.GetCurrentProcess.Handle

        OpenProcessToken(hdlProcessHandle, (TOKEN_ADJUST_PRIVILEGES Or TOKEN_QUERY), hdlTokenHandle)

        'Get the LUID for shutdown privilege.
        LookupPrivilegeValue("", "SeShutdownPrivilege", tmpLuid)

        tkp.PrivilegeCount = 1 'One privilege to set

        tkp.TheLuid = tmpLuid

        tkp.Attributes = SE_PRIVILEGE_ENABLED

        'Enable the shutdown privilege in the access token of this process.
        AdjustTokenPrivileges(hdlTokenHandle, False, tkp, Len(tkpNewButIgnored), tkpNewButIgnored, lBufferNeeded)

    End Sub


End Module