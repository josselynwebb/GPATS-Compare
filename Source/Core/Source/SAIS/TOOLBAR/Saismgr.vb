Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports System.Text
Imports Microsoft.Win32

Module SaisCommander
    '**************************************************************
    '**************************************************************
    '** ManTech Test Systems Software Module                     **
    '**                                                          **
    '** Nomenclature   : VIPERT SAIS Manager Toolbar               **
    '** Version        : 1.5                                     **
    '** Written By     : David W. Hartley                        **
    '** Last Update    : 10/30/04 Dave Joiner                    **
    '** Purpose        : This program provides convenient access **
    '**                  to VIPERT SAIS Front Panel Application.   **
    '** Dev Environ    : Visual Basic 6.0, Desaware SpyWorks 4.2 **
    '**                                                          **
    '** Program Begins Executing Instructions In Sub:MAIN        **
    '--------------------------------------------------------------
    '--                  Revision History                        --
    '--------------------------------------------------------------
    '**  Release 9.0                                             **
    '**     05/12/03 V1.3   ECO-3047-631     Dave Joiner         **
    '**  DR#200                                                  **
    '**     Add Function sDTSPath() to Saismgr.bas to find the   **
    '**     Root Directory for the "TerM9.chm". Added a Change   **
    '**     Directory (ChDir) call in Sub LaunchApplication()    **
    '**     to change the Default Directory so that "TerM9.chm"  **
    '**     can be found by the DTS SFP.                         **
    '**  Self-Initiated  05/12/03 Dave Joiner                    **
    '**     Added a Form_Load() procedure in "About.frm" to      **
    '**     Center form and update Version number automatically  **
    '**     to reflect Project Properties.                       **
    '**  Self-Initiated  05/12/03 Dave Joiner                    **
    '**     Modified the Comments section of the Project         **
    '**     Properties to reflect VB 6.0 instead of VB 4.0.      **
    '**  Self-Initiated  05/12/03 Dave Joiner                    **
    '**     Modified Header in About.frm as not reflect version  **
    '**     and date of last update. This information is         **
    '**     contained in the Saismgr.bas header.                 **
    '**                                                          **
    '**  Release 10.0    EO Support                              **
    '**     03/11/04 V1.4   ECO-3047-656       Tom Biggs         **
    '**     Replaced single EO button on frmToolBar with seperate**
    '**     buttons for IRWindows and Video Display Tool CSCI.   **
    '**     Modified frmToolBar.frm Subs Timer1_Timer() and      **
    '**     Callback1_EnumWindows and module Saismgr.bas Subs    **
    '**     Main() and LaunchApplication() to support both       **
    '**     and to inhibit use of one when the other is open.    **
    '**     Modified Subs ExitSaisToolBar(), LaunchApplication() **
    '**     and Timer1_Timer() to remove EO power when the SAIS  **
    '**     toolbar is closed and to provide the user an option  **
    '**     to remove power when the EO application is closed to **
    '**     allow switching between applications without the need**
    '**     to cycle power.  Also modified EO power On user      **
    '**     prompt to specifically identify PPUs 7, 8, and 9 as  **
    '**     those used by the SAIS as EO Power to prevent        **
    '**     possible damage if used with a TPS interface device, **
    '**     identify supported instruments, provide power/GPIB   **
    '**     hookup instructions and provide user with an option  **
    '**     to exit from the selected application.               **
    '**     Added frmEoPwrUpStatus to provide user with a status **
    '**     update via a progress bar while EO power on is in    **
    '**     progress versus a blank screen.  Modified Sub        **
    '**     EO_Power to display and update form/progress bar.    **
    '**  Self-Initiated  03/11/04 Tom Biggs                      **
    '**     Modified Sub Timer1_Timer to disable timer while code**
    '**     is executing, enabling reduction of timer-interval   **
    '**     from 4 seconds to 250 milliseconds, greatly speeding **
    '**     up Toolbar response to a SAIS application opening or **
    '**     closing.                                             **
    '**  Self-Initiated  07/12/04 Jeff Hill                      **
    '**     Added vbSystemModal button style argument for the    **
    '**     MsgBoxes used to ask for EO power on and off. Before,**
    '**     the 'turn off?' query would fall behind anything that**
    '**     may be showing on the screen.                        **
    '**  DR #317  08/04/04 Tom Biggs                             **
    '**     Modified Sub Main() to add the Mil-Std-1553 SAIS to  **
    '**     thosw in which the path is set to the VIPERT.INI key   **
    '**     defining the COTS directory created when the SAIS is **
    '**     installed.                                           **
    '--------------------------------------------------------------
    '**               ---   Release 10.1   ---                   **
    '**  ECO-3047-689                                            **
    '**  DR #330  10/30/04 Dave Joiner                           **
    '**     DR 319, which was satisfied at one time, needs to be **
    '**     re-applied to the SAIS Toolbar. Due to multiple      **
    '**     engineers working on this CSCI, DR 317 was           **
    '**     mistakenly applied to this CSCI, which overwrote     **
    '**     the application of DR 319.                           **
    '**     Modified Sub Main() to check for ":\" in the SAIS    **
    '**     File location string returned from the VIPERT.INI file.**
    '**     If the contains ":\" then use full path, otherwise,  **
    '**     use the SAIS directory path as a prefix. This will   **
    '**     allow the VIPERT.INI file key value to be changed      **
    '**     without modifying the SAISMgr Project if full SAIS   **
    '**     paths are modified or added in the future.           **
    '**************************************************************

    '-----------------API / DLL Declarations------------------------------
    Declare Function SetWindowPos Lib "user32" (ByVal hWnd As Integer, ByVal hWndInsertAfter As Integer, ByVal X As Integer, ByVal Y As Integer, ByVal cx As Integer, ByVal cy As Integer, ByVal wFlags As Integer) As Integer
    Declare Function GetPrivateProfileString Lib "kernel32" Alias "GetPrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpDefault As String, ByVal lpReturnedString As StringBuilder, ByVal nSize As Integer, ByVal lpFileName As String) As Integer
    Declare Function WritePrivateProfileString Lib "kernel32" Alias "WritePrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpString As String, ByVal lpFileName As String) As Integer
    Declare Function EnumWindows Lib "user32" (ByVal lpEnumFunc As Integer, ByVal lParam As Integer) As Integer
    Declare Function GetWindowTextLength Lib "user32" Alias "GetWindowTextLengthA" (ByVal hWnd As Integer) As Integer
    Declare Function GetWindowText Lib "user32" Alias "GetWindowTextA" (ByVal hWnd As Integer, ByVal lpString As String, ByVal cch As Integer) As Integer
    Declare Function GetWindow Lib "user32" (ByVal hWnd As Integer, ByVal wCmd As Integer) As Integer
    Declare Function PostMessage Lib "user32" Alias "PostMessageA" (ByVal hWnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
    'Declare Function aps6062_init Lib "APS6062.DLL" (ByVal resourceName As String, ByVal idQuery As Short, ByVal resetDevice As Short, ByRef instrumentHandle As Integer) As Integer
    'Declare Function aps6062_readInstrData Lib "APS6062.DLL" (ByVal instrumentHandle As Integer, ByVal numberBytesToRead As Integer, ByVal ReadBuffer As String, ByRef numBytesRead As Integer) As Integer
    'Declare Function aps6062_writeInstrData Lib "APS6062.DLL" (ByVal instrumentHandle As Integer, ByVal writeBuffer As String) As Integer
    'Declare Function aps6062_close Lib "APS6062.DLL" (ByVal instrumentHandle As Integer) As Integer
    Declare Function atxml_Initialize Lib "AtXmlApi.dll" (ByVal ProcType As String, ByVal guid As String) As Integer
    Declare Function atxml_Close Lib "AtXmlApi.dll" () As Integer
    Declare Function atxml_ValidateRequirements Lib "AtXmlApi.dll" (ByVal TestRequirements As String, ByVal Allocation As String, ByVal Availability As String, ByVal BufferSize As Short) As Integer
    Declare Function atxml_IssueSignal Lib "AtXmlApi.dll" (ByVal SignalDescription As String, ByVal Response As String, ByVal BufferSize As Short) As Integer
    Declare Function atxmlDF_viWrite Lib "AtxmlDriverFunc.DLL" (ByVal ResourceName As String, ByVal InstrumentHandle As Short, ByVal InstrumentCmds As String, ByVal BufferSize As Integer, ByRef ActWriteLen As Integer) As Integer
    Declare Function atxmlDF_viRead Lib "AtxmlDriverFunc.DLL" (ByVal ResourceName As String, ByVal InstrumentHandle As Short, ByVal ReadBuffer As String, ByVal BufferSize As Integer, ByRef ActReadLen As Integer) As Integer


    '-----------------Global Variables------------------------------------
    Public SaisPath As String 'Path of Stand Alone Instrument Software Directory
    Public RfInstalled As String '=YES when the RF Option is installed in VIPERT
    Public EoInstalled As String '=YES when the EO Option is installed in VIPERT
    Public lpEnumFunc As Integer 'Callback Address
    Public LargeButtonSize As Boolean 'Large/Small Button Flag
    Public Horizontal As Boolean = True 'Horizontal/Vertical Orientation Flag set to true by default
    Public InstrumentIndex As Short 'Current Instrument Selection
    Public Kill1553Title As String 'Fix to solve the changing 1553 Caption Situation
    Public KillDtsTitle As String 'Fix to solve the changing DTS Caption Situation
    
    Public SupplyHandle(10) As Integer
    Public SystErr As Integer
    Public SoftwareVersion As String
    '-----------------Global Constants------------------------------------
    Public Const GW_Child As Short = 5 'API Window Constant
    Public Const GW_HWNDFIRST As Short = 0 'API Window Constant
    Public Const GW_HWNDLAST As Short = 1 'API Window Constant
    Public Const GW_HWNDNEXT As Short = 2 'API Window Constant
    Public Const GW_HWNDPREV As Short = 3 'API Window Constant
    Public Const GW_OWNER As Short = 4 'API Window Constant

    Public Const DTS As Short = 0 'Digital Test Subsystem
    Public Const DMM As Short = 1 'Digital Multimeter
    Public Const ARB As Short = 2 'Arbitrary Function Generator
    Public Const DSCOPE As Short = 3 'Digitizing Oscilloscope
    Public Const UCT As Short = 4 'Universal Counter/Timer
    Public Const FG As Short = 5 'Function Generator
    Public Const RFS As Short = 6 'RF Source Generator
    Public Const RFC As Short = 7 'RF Counter
    Public Const RFM As Short = 8 'RF Measurement Analyzer
    Public Const RFP As Short = 9 'RF Power Meter
    Public Const SW As Short = 10 'Switching
    Public Const UUTPS As Short = 11 'User / Unit Under Test Power Supplies
    Public Const MIL1553 As Short = 12 'MIL-STD-1553 Bus Interface
    Public Const BUSIO As Short = 13 'BUS I/O
    Public Const SR As Short = 14 'Synchro/Resolver
    'Global Const CBTS% = 15
    Public Const EOV As Short = 15 'EO Video Display Tools
    Public Const IRWIN As Short = 16 'IRWindows
    Public Const TOOLBAR_WIDTH_PADDING = 18
    Public Const TOOLBAR_HEIGHT_PADDING = 42

    Public Const NUM_OF_SAIS_INSTRUMENTS As Short = 16 'Number Of Indexed Instruments
    '-----------------Global Arrays --------------------------------------
    Public SaisInstrument(NUM_OF_SAIS_INSTRUMENTS) As Short 'Holds Instrument Execution and Termination Information
    Public InstrumentWindowCaption(NUM_OF_SAIS_INSTRUMENTS) As String 'Instrument Caption
    Public InstrumentKey(NUM_OF_SAIS_INSTRUMENTS) As String 'Instrument INI File Key Name
    Public InstrumentFile(NUM_OF_SAIS_INSTRUMENTS) As String 'Instrument File Name
    Public Q As String 'ASCII Quote
    Public bolEoPowerOn As Boolean
    Public bEOInitializing As Boolean
    Public intNumButtons As Short
    Const SECS_IN_DAY As Integer = 86400
    Public powerOffPrompted = False
    Public garrsPsResourceName(9) As String         'Power Supply Resource Names ["DCPS_1" - "DCPS_10"]

    Sub AdjustToolbar()
        '************************************************************
        '* ManTech Test Systems Software Module                     *
        '************************************************************
        '* Nomenclature   : VIPERT SAIS Manager Toolbar               *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     This Routine size the toolbar accordingly.           *
        '*    EXAMPLE:                                              *
        '*      AdjustToolbar                                       *
        '************************************************************
        'Fix Form Width To Toolbar
        If Horizontal = True Then
            If LargeButtonSize = True Then
                frmToolBar.Width = (frmToolBar.tbrInstruments.Buttons(1).Width * intNumButtons) + TOOLBAR_WIDTH_PADDING
            Else 'Small Buttons
                frmToolBar.Width = (frmToolBar.tbrInstruments2.Buttons(1).Width * intNumButtons) + TOOLBAR_WIDTH_PADDING
            End If
            System.Windows.Forms.Application.DoEvents()
            'Fix Form Height To Toolbar
            If LargeButtonSize = True Then
                frmToolBar.Height = (frmToolBar.tbrInstruments.Buttons(1).Height) + TOOLBAR_HEIGHT_PADDING
            Else
                frmToolBar.Height = (frmToolBar.tbrInstruments2.Height) + TOOLBAR_HEIGHT_PADDING
            End If
            System.Windows.Forms.Application.DoEvents()
        Else 'Vertical
            If LargeButtonSize = True Then
                frmToolBar.Width = VB6.TwipsToPixelsX(frmToolBar.tbrInstruments.ButtonWidth + 100)
            Else
                frmToolBar.Width = VB6.TwipsToPixelsX(frmToolBar.tbrInstruments2.ButtonWidth + 100)
            End If
            System.Windows.Forms.Application.DoEvents()
            'Fix Form Height To Toolbar
            If LargeButtonSize = True Then
                frmToolBar.Height = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(frmToolBar.tbrInstruments.Height) + 350)
            Else
                frmToolBar.Height = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(frmToolBar.tbrInstruments2.Height) + 350)
            End If
            System.Windows.Forms.Application.DoEvents()
        End If
        'If Toolbar is Off The Screen, Then Correct
        'Too far left
        If VB6.PixelsToTwipsX(frmToolBar.Left) < 25 Then
            frmToolBar.Left = VB6.TwipsToPixelsX(25)
        End If
        System.Windows.Forms.Application.DoEvents()
        'Too Far Right
        If (VB6.PixelsToTwipsX(frmToolBar.Width) + VB6.PixelsToTwipsX(frmToolBar.Left)) > VB6.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) - 25 Then
            frmToolBar.Left = VB6.TwipsToPixelsX((VB6.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) - 25) - VB6.PixelsToTwipsX(frmToolBar.Width))
        End If
        'Too High
        If VB6.PixelsToTwipsY(frmToolBar.Top) < 50 Then
            frmToolBar.Top = VB6.TwipsToPixelsY(200)
        End If
        System.Windows.Forms.Application.DoEvents()
        'Too Low
        If (VB6.PixelsToTwipsY(frmToolBar.Top) + VB6.PixelsToTwipsY(frmToolBar.Height)) > VB6.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) - 25 Then
            frmToolBar.Top = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) - (25 + VB6.PixelsToTwipsY(frmToolBar.Height)))
        End If
        System.Windows.Forms.Application.DoEvents()

    End Sub


    Sub Delay(ByVal dSeconds As Double)
        'DESCRIPTION:
        '   Delays for a specified number of seconds.
        '   50 milli-second resolution.
        '   Minimum delay is about 50 milli-seconds.
        'PARAMETERS:
        '   dSeconds: The number of seconds to Delay.
        Dim t As Double

        t = dGetTime()
        dSeconds = dSeconds / SECS_IN_DAY
        Do
            System.Windows.Forms.Application.DoEvents()
        Loop While dGetTime() - t < dSeconds

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
        ' s = the fraction of today with 1 second resolution.
        'CLng(Now) returns only the number of days (the "d." part).
        'Timer returns the number of seconds since midnight with 10 mSec resoulution.
        'So, the following statement returns a composite double-float number representing
        ' the time since 1900 with 10 mSec resoultion. (no cross-over midnight bug)

        dGetTime = CLng(DateSerial(Year(Now), Month(Now), VB.Day(Now)).ToOADate) + (VB.Timer() / SECS_IN_DAY)

    End Function

    Sub CenterForm(ByRef Form As Object)
        '************************************************************
        '* ManTech Test Systems Software Module                     *
        '************************************************************
        '* Nomenclature   : HP E 1412ADigital Multimeter Front Panel*
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     This Module Centers One Form With Respect To The     *
        '*     User's Screen.                                       *
        '*    EXAMPLE:                                              *
        '*     CenterForm frmMain                                   *
        '************************************************************
        
        
        Form.Top = VB6.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) / 2 - Form.Height / 2
        
        
        Form.Left = VB6.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) / 2 - Form.Width / 2
    End Sub

    Sub DcpsSetOptions(ByRef Supply As Short, ByRef OpenRelay As Short, ByRef SetMaster As Short, ByRef SenseLocal As Short, ByRef ConstantCurrent As Short)
        'DESCRIPTION:
        '   Sets one or more parameters of a DC Supply
        'PARAMETERS:
        '   Supply%:    Number of supply to set (1-10)
        '   NOTE:   For the following parameters,
        '           True performs its name and False performs the opposite
        '   OpenRelay%:         Sets Output Relay Open/Closed
        '   SetMaster%:         Sets Master/Slave control mode
        '   SenseLocal%:        Sets Local/Remote Sensing
        '   ConstantCurrent%:   Sets Constant Current/Voltage mode
        'GLOBAL VARIABLES MODIFIED:
        '   none

        Dim B2, B1, B3 As Short

        B1 = &H20S + Supply
        Select Case OpenRelay
            Case True '(-1)
                B2 = B2 + &H20S 'Enable / Relay Open
            Case False '( 0)
                B2 = B2 + &H30S 'Enable / Relay Closed
        End Select
        Select Case SetMaster
            Case True '(-1)
                B2 = B2 + &H8S 'Enable / Set as Master
            Case False '( 0)
                B2 = B2 + &HCS 'Enable / Set as Slave
        End Select
        Select Case SenseLocal
            Case True '(-1)
                B2 = B2 + 2 'Enable / Sense Local
            Case False '( 0)
                B2 = B2 + 3 'Enable / Sense Remote
        End Select
        Select Case ConstantCurrent
            Case True '(-1)
                B3 = B3 + &H20S 'Enable / Current Limiting(Protection)
            Case False '( 0)
                B3 = B3 + &H30S 'Enable / Constant Current
        End Select

        If (B2 > 0) Or (B3 > 0) Then
            B2 = B2 + &H80S
        End If

        'Send Command
        SendDCPSCommand(Supply, Chr(B1) & Chr(B2) & Chr(B3))

    End Sub


    Public Sub DcpsSetCurrent(ByRef Supply As Short, ByVal current As Single)
        'DESCRIPTION:
        '   Sets the current limit value of a DC Supply
        'PARAMETERS:
        '   Supply%:    Number of supply to set (1-10)
        '   Current!:   The Current limit value
        'GLOBAL VARIABLES MODIFIED:
        '   none

        Dim B2, B1, B3 As Short

        'Convert to Integer DAC code
        current = current * 500 '20mA Resolution

        B1 = &H20S + Supply
        B2 = &H40S + (current \ &H100S)
        
        B3 = current Mod &H100S

        'Send Command
        SendDCPSCommand(Supply, Chr(B1) & Chr(B2) & Chr(B3))

    End Sub

    Public Sub DcpsSetVoltage(ByRef Supply As Short, ByVal Volts As Single)
        'DESCRIPTION:
        '   Sets the voltage level of a DC Supply
        'PARAMETERS:
        '   Supply%:    Number of supply to set (1-10)
        '   Volts!:     The Voltage level value
        'GLOBAL VARIABLES MODIFIED:
        '   none

        Dim B2, B1, B3 As Short

        'Convert to Integer DAC code
        If Supply = 10 Then
            Volts = Volts * 50 '20mV Resolution
        Else
            Volts = Volts * 100 '10mV Resolution
        End If

        B1 = &H20S + Supply
        B2 = &H50S + (Volts \ &H100S)
        
        B3 = Volts Mod &H100S

        'Send Command
        SendDCPSCommand(Supply, Chr(B1) & Chr(B2) & Chr(B3))

    End Sub
    Sub DCPSReset(ByRef Supply As Short)
        '************************************************************
        '*    DESCRIPTION:                                          *
        '*     This Module resets a power supply                    *
        '*    EXAMPLE:                                              *
        '*     CommandSupplyReset 4                                 *
        '*    PARAMETERS:                                           *
        '*     Supply%    =  The supply number of which to reset    *
        '************************************************************

        Dim B1 As Short
        Dim B2 As Short
        Dim B3 As Short

        'Send "Open All Relays" Command  2S, A0, 00   Open Output Relay on supply S (default)  *
        B1 = &H20S + Supply
        B2 = &HA0S 'was &HAA  bb
        B3 = 0 ' was &H20
        SendDCPSCommand(Supply, Chr(B1) & Chr(B2) & Chr(B3))

        'Send "Reset" Command 1S, 00, 00   Reset Supply S(1-A)                      *
        B1 = &H10S + Supply
        B2 = 0 ' was 128  bb
        B3 = 0 ' was 128  bb
        SendDCPSCommand(Supply, Chr(B1) & Chr(B2) & Chr(B3))

    End Sub
    

    Sub SendDCPSCommand(ByVal shSupply As Short, ByVal sCommand As String)

        '************************************************************
        '* ManTech Test Systems Software Sub                        *
        '************************************************************
        '* Nomenclature   : APS 6062 UUT Power Supplies             *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     Send a command string to the power supplies          *
        '*    EXAMPLE:                                              *
        '*      SendDCPSCommand (3, Command$                        *
        '*    PARAMTERS:                                            *
        '*     slot%     = The slot number of the supply where to   *
        '*                 send the command                         *
        '*     Command$  = The command string to send to the supply *
        '************************************************************

        Dim iErrStat As Integer
        Dim iWriteLenReturned As Integer

        If sCommand.Length <> 3 Then
            '            MsgBox("Invalid Command length in SendDCPSCommand Sub: " & sCommand, MsgBoxStyle.Critical, "Power Source Communication Error")
            Exit Sub
        End If

        iErrStat = atxmlDF_viWrite(garrsPsResourceName(shSupply - 1), 0, sCommand, CLng(sCommand.Length), iWriteLenReturned)

        'Copied from Freedom01502
        If iErrStat Or iWriteLenReturned = 0 Then
            MsgBox("Error in SendDCPSCommand for Slot: '" & garrsPsResourceName(shSupply - 1) & "'", MsgBoxStyle.Exclamation, "SendDCPSCommand Failed")
        End If

        Delay(1)
    End Sub


    Sub BringToTop(ByRef hWnd As Integer)
        '************************************************************
        '* ManTech Test Systems Software Module                     *
        '************************************************************
        '* Nomenclature   : VIPERT SAIS Manager Toolbar               *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     This Sub places a window at the top of the graphical *
        '*     desktop stack.                                       *
        '*    PARAMETERS:                                           *
        '*     hWnd& = Handle of Window to put on top               *
        '*    EXAMPLE:                                              *
        '*     BringToTop Me.hWnd                                   *
        '************************************************************
        Dim nFlags As Short
        Dim wFlags As Integer
        Dim RetValue As Integer
        Const HWND_TOPMOST As Short = -1
        Const SWP_NOMOVE As Short = &H2S
        Const SWP_NOSIZE As Short = &H1S

        wFlags = SWP_NOMOVE Or SWP_NOSIZE
        'Move Window
        '6/11/19 JWebb changed Y value to 100 to keep toolbar from being hidden by netbanner
        RetValue = SetWindowPos(hWnd, HWND_TOPMOST, 0, 100, 0, 0, wFlags)

    End Sub


    Sub ExitSaisToolBar()
        '************************************************************
        '* ManTech Test Systems Software Module                     *
        '************************************************************
        '* Nomenclature   : VIPERT SAIS Manager Toolbar               *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     This Sub terminates SIAS Instruments and Ends the    *
        '*     program.                                             *
        '*    EXAMPLE:                                              *
        '*     ExitSaisToolBar                                      *
        '************************************************************
        Dim SaisProgram As Short

        'If applied, remove EO Power
        If bolEoPowerOn Then
            EO_Power(("Off"))
        End If
        'Close Open Instruments
        atxml_Close()

        frmToolBar.Timer1.Enabled = False 'Stop Timer
        System.Windows.Forms.Application.DoEvents() 'Clear Windows Event Queue
        'frmToolBar.Timer1_Tick(Nothing, New System.EventArgs()) 'Check One Final Time for Instruments
        For SaisProgram = 0 To NUM_OF_SAIS_INSTRUMENTS
            If SaisInstrument(SaisProgram) = False Then
                'Terminate Open Instruments on Exit
                TerminateApp32(InstrumentWindowCaption(SaisProgram))
            End If
        Next SaisProgram
        If Kill1553Title <> "" Then
            TerminateApp32(Kill1553Title)
        End If
        If KillDtsTitle <> "" Then
            TerminateApp32(KillDtsTitle)
        End If

        End

    End Sub

    Function GatherSaisIniFileInformation(ByVal lpKeyName As String, ByVal lpDefault As String, ByRef Heading As String) As String
        '************************************************************
        '* ManTech Test Systems Software Module                     *
        '************************************************************
        '* Nomenclature   : VIPERT SAIS Manager Toolbar               *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     This Function Gathers INI File Information in the    *
        '*     [SAIS] Section of VIPERT.INI                           *
        '*    PARAMETERS:                                           *
        '*     lpKeyName$ = Key=? in INI File                       *
        '*     lpDefault$ = Default Value if call fails             *
        '*     Heading$ = [Heading] in INI File                     *
        '*    EXAMPLE:                                              *
        '*     Setting$=GatherSaisIniFileInformation "KEY", "Def"   *
        '*    RETURNS:                                              *
        '*     Value of Key in INI file or lpDefault$               *
        '************************************************************
        Dim lpApplicationName As String
        Dim lpReturnedString As New StringBuilder(4096)
        Dim nSize As Integer = 4096
        Dim lpFileName As String
        Dim ReturnValue As Integer
        Dim FileNameInfo As String
        Dim lpString As String


        'Define INI File Name
        lpFileName = "C:\Users\Public\Documents\ATS\ATS.ini"

        'Find File Locations
        lpApplicationName = Heading
        ReturnValue = GetPrivateProfileString(lpApplicationName, lpKeyName, lpDefault, lpReturnedString, nSize, lpFileName)
        FileNameInfo = Trim(lpReturnedString.ToString)
        FileNameInfo = Mid(FileNameInfo, 1, Len(FileNameInfo))
        'If File Locations Missing, then create empty keys
        If InStr(1, FileNameInfo, lpDefault) Or FileNameInfo = "" Then
            FileNameInfo = "NOT_CONFIGURED"
            lpString = " "
            ReturnValue = WritePrivateProfileString(lpApplicationName, lpKeyName, lpString, lpFileName)
        End If

        'Return Function Value
        GatherSaisIniFileInformation = FileNameInfo

    End Function

    Sub LaunchApplication(ByRef Button As ComctlLib.Button)
        '************************************************************
        '* ManTech Test Systems Software Module                     *
        '************************************************************
        '* Nomenclature   : VIPERT SAIS Manager Toolbar               *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     This Sub launches an application chosen by a user    *
        '*     toolbar button click.                                *
        '*    PARAMETERS:                                           *
        '*     Button = Toolbar Button object with a valid KEY Name *
        '*    EXAMPLE:                                              *
        '*     LaunchApplication Button                             *
        '************************************************************
        Dim hHandle As Integer
        Dim InstrIndex As Short
        Dim Supply As Short
        Dim sMsgText As String
        Dim iChoice As Short

        'Assign Indexes to Toolbar Keys
        Select Case Button.Key
            Case "DTS"
                InstrIndex = DTS
                'DR#200 - Change the Default Path to that of the "TerM9.chm" file
                ChDir(sDTSPath(InstrumentFile(DTS)))
            Case "SW"
                InstrIndex = SW
            Case "DMM"
                InstrIndex = DMM
            Case "ARB"
                InstrIndex = ARB
            Case "DSCOPE"
                InstrIndex = DSCOPE
            Case "UCT"
                InstrIndex = UCT
            Case "FG"
                InstrIndex = FG
            Case "RFS"
                InstrIndex = RFS
            Case "RFC"
                InstrIndex = RFC
            Case "RFM"
                InstrIndex = RFM
            Case "RFP"
                InstrIndex = RFP
            Case "UUTPS"
                InstrIndex = UUTPS
            Case "MIL1553"
                InstrIndex = MIL1553
            Case "BUSIO"
                InstrIndex = BUSIO
            Case "SR"
                InstrIndex = SR
                'Case "CBTS"
                'InstrIndex% = CBTS%
            Case "EOV"
                InstrIndex = EOV
            Case "IRWIN"
                InstrIndex = IRWIN

        End Select

        If FileExists(InstrumentFile(InstrIndex)) Then
            Button.Enabled = False
            If (InstrIndex = EOV) Or (InstrIndex = IRWIN) Then
                bEOInitializing = True
                'Initialize variables and handle special power on/setup required for EO functions
                'and don't allow launching both IRWindows and Video Display Tool together
                If InstrIndex = EOV Then
                    'Disable IRWindows button as well as Video Display Tools
                    frmToolBar.tbrInstruments.Buttons(IRWIN + 1).Enabled = False
                    SaisInstrument(IRWIN) = False
                    sMsgText = "Ensure that the EO Ethernet Cable is connected" & vbCrLf & " to J16 on the Interface Controller"
                Else
                    'Disable Video Display Tools button as well as IRWindows
                    frmToolBar.tbrInstruments.Buttons(EOV + 1).Enabled = False
                    SaisInstrument(EOV) = False
                    sMsgText = "Ensure that the EO Ethernet Cable is connected" & vbCrLf & " to J16 on the Interface Controller"
                End If

                sMsgText = sMsgText & vbCrLf & vbCrLf & "If using the LARRS be sure it is connected before power on and " & vbCrLf & "that the power cable is connected to the instrument."

                'If EO power is OFF, give user option to turn ON
                CheckEoPower()
                If bolEoPowerOn = False And InstrIndex = IRWIN Then
                    iChoice = MsgBox("Do you want power applied to the EO module?" & vbCrLf & "(DC1 [+28V Master] and DC2 [+28V Slave] and DC3 [+15V] will be used.)", MsgBoxStyle.YesNoCancel + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.SystemModal)
                    If iChoice = MsgBoxResult.Yes Then
                        'If POWER ON is selected then prompt user to setup instrument to VIPERT
                        If MsgBox(sMsgText, MsgBoxStyle.OkCancel + MsgBoxStyle.SystemModal) = MsgBoxResult.Ok Then
                            EO_Power("ON")
                        Else
                            bEOInitializing = False
                            Exit Sub 'CANCEL
                        End If
                    Else
                        If iChoice = MsgBoxResult.Cancel Then
                            bEOInitializing = False
                            Exit Sub 'CANCEL
                        End If
                    End If
                End If

                bEOInitializing = False 'Re-enable monitoring of Application Open/Closed Status

            End If '(InstrIndex% = EOV%) Or (InstrIndex% = IRWIN%)

            If (InstrIndex = MIL1553) Then 'release cicl handle to 1553 instrument
                Release1553CICLHandle()
            End If

            'Now open SAIS application
            hHandle = Shell(Q & InstrumentFile(InstrIndex) & Q, AppWinStyle.NormalNoFocus)
            SaisInstrument(InstrIndex) = False

            'Start Copilot Monitor Thread to Restore Cicl Handle when Copilot is exited
            If (InstrIndex = MIL1553) Then 'release cicl handle to 1553 instrument
                'Give copilot process enough time to start
                System.Threading.Thread.Sleep(100)
                Dim copilotMonitorThread As New System.Threading.Thread(AddressOf CopilotProcessMonitorThread)
                copilotMonitorThread.Start()
            End If

        Else 'Error opening file
            MsgBox("File Not Found:" & InstrumentFile(InstrIndex), MsgBoxStyle.Information, "Error")
        End If

    End Sub
    ''' <summary>
    ''' Check EO Power Supplies to determine State
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub CheckEoPower()
        Dim D1 As Single
        Dim D2 As Single
        Dim data As String
        Dim ErrorStatus As Integer
        Dim shSupply As Short 'Power Supply Number
        Dim dVolts As Double 'Voltage measurement returned
        Dim sReadBuffer As String
        Dim shParm1 As Short
        Dim shParm2 As Short
        Dim shParm3 As Short
        Dim Relayclosed As Boolean
        bolEoPowerOn = True

        For shSupply = 3 To 1 Step -2
            shParm1 = shSupply
            shParm2 = &H42
            shParm3 = &H0
            'Send Command
            SendDCPSCommand(shSupply, Convert.ToString(Chr(shParm1)) & Convert.ToString(Chr(shParm2)) & Convert.ToString(Chr(shParm3)))

            Delay(0.1)

            'Read Sense Measurement
            sReadBuffer = Space(4000)
            ErrorStatus = ReadDCPSCommand(shSupply, sReadBuffer)
            If ErrorStatus <> 0 Then
                Exit Sub
            End If

            'Voltage Measurement
            data = sReadBuffer

            If Asc(data.Substring(0, 1)) > 64 Then '80
                'voltage measurement divide by 100
                D1 = (Asc(data.Substring(2, 1))) And 15
                D1 = D1 * 256
                D2 = Asc(data.Substring(3, 1))
                dVolts = CDbl((D1 + D2) / 100.0)

                If (shSupply = 1 And (dVolts < 26 Or dVolts > 30)) Or ((shSupply = 3) And (dVolts < 13 Or dVolts > 17)) Then
                    'Indicate Power OFF
                    bolEoPowerOn = False
                    Exit Sub
                End If
            End If

            'Check to see if the relay is closed to apply power
            SendDCPSCommand(shSupply, Convert.ToString(Chr(shSupply)) & Convert.ToString(Chr(&H44)) & Convert.ToString(Chr(&H0)))
            Delay(0.1)
            ErrorStatus = ReadDCPSCommand(shSupply, sReadBuffer)
            If ErrorStatus <> 0 Then
                Exit Sub
            End If
            If Len(sReadBuffer) > 2 Then
                Relayclosed = RelayState(Asc(Mid(sReadBuffer, 1, 1))) 'Relay State?
                If (Relayclosed = False) Then
                    'Indicate Power OFF
                    bolEoPowerOn = False
                    Exit Sub
                End If
            End If
        Next shSupply
        bolEoPowerOn = True
    End Sub
    Function ReadDCPSCommand(ByRef SupplyX As Short, ByRef ReadBuffer As String) As Integer
        Dim ErrStatus As Integer
        Dim iReadLenReturned As Integer
        Dim S As String
        Dim i As Short

        ReadBuffer = Space(255)
        ErrStatus = atxmlDF_viRead(garrsPsResourceName(SupplyX - 1), 0, ReadBuffer, 255, iReadLenReturned)
        If ErrStatus <> 0 Then
            MsgBox("AtXml Power Source Communication Error", MsgBoxStyle.OkOnly, "Non-Zero Status Returned")
        Else
            'The power supply should always return 5 bytes for a status read command
            If iReadLenReturned = 5 Then
                ReadBuffer = ReadBuffer.Substring(0, iReadLenReturned)
            Else
                S = ""
                For i = 1 To iReadLenReturned
                    S = S & "Byte" & CStr(i) & " =" & Str(Asc(ReadBuffer.Substring(i - 1, 1))) & vbCrLf
                Next i

                ErrStatus = atxmlDF_viRead(garrsPsResourceName(SupplyX - 1), 0, ReadBuffer, 255, iReadLenReturned)
                If iReadLenReturned = 5 Then
                    ReadBuffer = ReadBuffer.Substring(0, iReadLenReturned)
                End If
            End If
            Delay(0.5)
        End If

        Return ErrStatus
    End Function
    Function RelayState(ByVal iBinary As Integer) As Boolean
        Dim msByte As String
        msByte = IntToBinary(iBinary)
        'EADS - DRB added 2/25/2008 to properly select supply for setting
        'Isolation Relay
        If Mid(msByte, 4, 1) = "1" Then
            RelayState = True
        Else
            RelayState = False
        End If

    End Function
    ' Convert this Integer into a binary string.
    Function IntToBinary(ByVal iValue As Integer) As String
        IntToBinary = ""

        Dim result_string As String = ""
        Dim digit_num As Short
        Dim factor As Short

        If iValue > 255 Then Exit Function
        ' Read the hexadecimal digits
        ' one at a time from right to left.
        factor = 128
        For digit_num = 0 To 7
            ' Convert the value into bits.

            If iValue And factor Then
                result_string &= "1"
            Else
                result_string &= "0"
            End If
            factor = factor \ 2

        Next digit_num

        ' Return the result.
        IntToBinary = result_string
    End Function
    Sub EO_Power(ByRef sState As String)
        'DESCRIPTION:
        '   Performs power up or power down of the attached EO module using
        '   PPU 7 (-15V), and PPU 8/9 (+30V @10A), if selected by user prompt.
        '   If Power-On, then a progress bar is displayed while power on is
        '   in progress.
        'PARAMETERS:
        '   sState:  Identifies whether power is to be applied or removed
        'GLOBAL VARIABLES MODIFIED:
        '   none
        Dim Supply As Short
        Dim i As Short

        If UCase(sState) = "ON" Then
            frmEoPwrUpStatus.pbrEoPower.Value = 0
            frmEoPwrUpStatus.lblProgress.Text = "EO Power-On in Progress Please Wait"
            frmEoPwrUpStatus.Show() 'Provide user a progress bar while EO Pwr up in progress
            For Supply = 3 To 1 Step -1
                Delay(0.2)
                frmEoPwrUpStatus.pbrEoPower.Value = frmEoPwrUpStatus.pbrEoPower.Value + 10
            Next Supply

            ' reset supply
            DCPSReset(1) ' Reset master supply
            DCPSReset(2) ' Reset slave supply
            DCPSReset(3) ' Reset +15v supply

            'Changes to EO Power on for new EO unit PCR Vsys-310

            'Set DC Supply 3 to 15 volts, Constant Current
            frmEoPwrUpStatus.pbrEoPower.Value = frmEoPwrUpStatus.pbrEoPower.Value + 10
            frmEoPwrUpStatus.Refresh()
            Delay(0.3)
            DcpsSetOptions(3, False, True, True, True) 'close output relay
            frmEoPwrUpStatus.pbrEoPower.Value = frmEoPwrUpStatus.pbrEoPower.Value + 10
            frmEoPwrUpStatus.Refresh()
            Delay(0.3)
            DcpsSetCurrent(3, 5)
            frmEoPwrUpStatus.pbrEoPower.Value = frmEoPwrUpStatus.pbrEoPower.Value + 10
            frmEoPwrUpStatus.Refresh()
            Delay(0.3)
            DcpsSetVoltage(3, 15)
            frmEoPwrUpStatus.pbrEoPower.Value = frmEoPwrUpStatus.pbrEoPower.Value + 10
            frmEoPwrUpStatus.Refresh()
            Delay(2)

            'Set DC Supply 2 to +28 volts slave mode
            DcpsSetOptions(2, 1, False, 1, 1) 'set slave mode
            frmEoPwrUpStatus.pbrEoPower.Value = frmEoPwrUpStatus.pbrEoPower.Value + 10
            frmEoPwrUpStatus.Refresh()
            Delay(2)

            DcpsSetOptions(2, False, 1, 1, 1) 'close output relay
            frmEoPwrUpStatus.pbrEoPower.Value = frmEoPwrUpStatus.pbrEoPower.Value + 10
            frmEoPwrUpStatus.Refresh()
            Delay(2)

            'Set DC Supply 1 to 28 volts master mode
            DcpsSetOptions(1, False, True, True, True)
            frmEoPwrUpStatus.pbrEoPower.Value = frmEoPwrUpStatus.pbrEoPower.Value + 10
            frmEoPwrUpStatus.Refresh()
            Delay(0.3)
            DcpsSetCurrent(1, 5)
            frmEoPwrUpStatus.pbrEoPower.Value = frmEoPwrUpStatus.pbrEoPower.Value + 10
            frmEoPwrUpStatus.Refresh()
            Delay(0.3)
            frmEoPwrUpStatus.pbrEoPower.Value = 120
            frmEoPwrUpStatus.Refresh()
            DcpsSetVoltage(1, 28)
            Delay(1)

            frmEoPwrUpStatus.pbrEoPower.Max = 180
            frmEoPwrUpStatus.pbrEoPower.Value = 0
            For i = 1 To 179
                frmEoPwrUpStatus.lblProgress.Text = "Please Wait 3 minutes for the EO to warm up and Initialize"
                Delay(1)
                frmEoPwrUpStatus.pbrEoPower.Value = i + 1
            Next i
            bolEoPowerOn = True
            frmEoPwrUpStatus.Hide()
        Else
            'Disconnect two supplies
            'Set Supply 1 Voltage to 0
            SendDCPSCommand(1, Convert.ToString(Chr(&H21)) & Convert.ToString(Chr(&H50)) & Convert.ToString(Chr(&H0)))
            'Set Supply 1 Current Limit to 0 Amps
            SendDCPSCommand(1, Convert.ToString(Chr(&H21)) & Convert.ToString(Chr(&H40)) & Convert.ToString(Chr(&H0)))

            'Set Supply 3 Voltage to 0
            SendDCPSCommand(3, Convert.ToString(Chr(&H23)) & Convert.ToString(Chr(&H50)) & Convert.ToString(Chr(&H0)))
            'Set Supply 3 Current Limit to 0 Amps
            SendDCPSCommand(3, Convert.ToString(Chr(&H23)) & Convert.ToString(Chr(&H40)) & Convert.ToString(Chr(&H0)))

            'Power Supplies 1, 2 and 3
            SendDCPSCommand(1, Convert.ToString(Chr(&H11)) & Convert.ToString(Chr(&H0)) & Convert.ToString(Chr(&H0)))
            SendDCPSCommand(2, Convert.ToString(Chr(&H12)) & Convert.ToString(Chr(&H0)) & Convert.ToString(Chr(&H0)))
            SendDCPSCommand(3, Convert.ToString(Chr(&H13)) & Convert.ToString(Chr(&H0)) & Convert.ToString(Chr(&H0)))

            DCPSReset(1) ' Reset master supply
            DCPSReset(2) ' Reset slave supply
            DCPSReset(3) ' Reset +15v supply
            bolEoPowerOn = False
        End If

    End Sub

    
    Public Sub Main()
        '************************************************************
        '* ManTech Test Systems Software Module                     *
        '************************************************************
        '* Nomenclature   : VIPERT SAIS Manager Toolbar               *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     Program Entry Point-Execution Begins Here            *
        '************************************************************
        Dim hWinCaption As String
        Dim SaisProgram As Short

        Dim Allocation As String = ""
        Dim errorStatus As Integer = 0
        Dim XmlBuf As String
        Dim Response As String = ""

        SAISMGR.frmToolBar.Timer1.Enabled = False
        'If App.PrevInstance Then End
        frmAbout.StartPosition = FormStartPosition.CenterScreen
        'CenterForm(frmAbout)
        Q = Chr(34)
        SoftwareVersion = GatherIniFileInformation("System Startup", "SWR", "")
        frmAbout.Label5.Text = SoftwareVersion
        frmAbout.Show()
        frmAbout.Refresh()
        Delay(4)

        'Define Keys
        InstrumentKey(DTS) = "DTS"
        InstrumentKey(SW) = "SW"
        InstrumentKey(DMM) = "DMM"
        InstrumentKey(ARB) = "ARB"
        InstrumentKey(DSCOPE) = "DSCOPE"
        InstrumentKey(UCT) = "UCT"
        InstrumentKey(FG) = "FG"
        InstrumentKey(RFS) = "RFS"
        InstrumentKey(RFC) = "RFC"
        InstrumentKey(RFM) = "RFM"
        InstrumentKey(RFP) = "RFP"
        InstrumentKey(UUTPS) = "UUTPS"
        InstrumentKey(MIL1553) = "MIL1553"
        InstrumentKey(BUSIO) = "BUSIO"
        InstrumentKey(SR) = "SR"
        'InstrumentKey$(CBTS%) = "CBTS"
        InstrumentKey(EOV) = "EOV"
        InstrumentKey(IRWIN) = "IRWIN"
        'Get SAIS Path Information
        SaisPath = GatherSaisIniFileInformation("PATH", "Default", "SAIS")

        'Get RF Option Information
        RfInstalled = GatherSaisIniFileInformation("RF_OPTION_INSTALLED", "Default", "System Startup")
        'Get EO Option Information
        EoInstalled = GatherSaisIniFileInformation("EO_OPTION_INSTALLED", "Default", "System Startup")

        'Get File Locations
        For SaisProgram = 0 To NUM_OF_SAIS_INSTRUMENTS
            hWinCaption = GatherSaisIniFileInformation(InstrumentKey(SaisProgram), "Default", "SAIS")

            'If the Location String contains ":\", then use the absolute path,
            'otherwise use the relative path.
            If InStr(1, hWinCaption, ":\") <> 0 Then
                InstrumentFile(SaisProgram) = hWinCaption
            Else
                InstrumentFile(SaisProgram) = SaisPath & "\" & hWinCaption
            End If
        Next SaisProgram

        'Get Window Captions
        For SaisProgram = 0 To NUM_OF_SAIS_INSTRUMENTS
            hWinCaption = GatherSaisIniFileInformation(InstrumentKey(SaisProgram) & "_C", "Default", "SAIS")
            InstrumentWindowCaption(SaisProgram) = hWinCaption & Chr(0)
        Next SaisProgram

        'Register Active Window Callback
        For InstrumentIndex = 0 To NUM_OF_SAIS_INSTRUMENTS
            SaisInstrument(InstrumentIndex) = True
        Next InstrumentIndex

        'Load Forms
        
        frmToolBar.Show()
        
        frmPopUp.Refresh()

        'Handle Variants
        intNumButtons = NUM_OF_SAIS_INSTRUMENTS + 1
        If RfInstalled <> "YES" Then
            frmToolBar.tbrInstruments.Buttons(7).Visible = False
            frmToolBar.tbrInstruments.Buttons(8).Visible = False
            frmToolBar.tbrInstruments.Buttons(9).Visible = False
            frmToolBar.tbrInstruments.Buttons(10).Visible = False
            frmToolBar.tbrInstruments2.Buttons(7).Visible = False
            frmToolBar.tbrInstruments2.Buttons(8).Visible = False
            frmToolBar.tbrInstruments2.Buttons(9).Visible = False
            frmToolBar.tbrInstruments2.Buttons(10).Visible = False
            intNumButtons = intNumButtons - 4
        End If
        If EoInstalled <> "YES" Then
            frmToolBar.tbrInstruments.Buttons(16).Visible = False
            frmToolBar.tbrInstruments2.Buttons(16).Visible = False
            frmToolBar.tbrInstruments.Buttons(17).Visible = False
            frmToolBar.tbrInstruments2.Buttons(17).Visible = False
            intNumButtons = intNumButtons - 2
        End If

        'INIT Settings
        hWinCaption = GatherSaisIniFileInformation("SIZE", "Default", "SAIS")
        Select Case hWinCaption
            Case "1"
                frmPopUp.mnuLarge_Click(Nothing, New System.EventArgs())
            Case "2"
                frmPopUp.mnuSmall_Click(Nothing, New System.EventArgs())
            Case Else
                frmPopUp.mnuLarge_Click(Nothing, New System.EventArgs())
        End Select

        hWinCaption = GatherSaisIniFileInformation("HV", "Default", "SAIS")
        Select Case hWinCaption
            Case "1"
                frmPopUp.mnuOrientationHorizontal_Click(Nothing, New System.EventArgs())
            Case "2"
                frmPopUp.mnuOrientationVertical_Click(Nothing, New System.EventArgs())
            Case Else
                frmPopUp.mnuOrientationHorizontal_Click(Nothing, New System.EventArgs())
        End Select

        frmAbout.Close()

        AdjustToolbar()
        System.Windows.Forms.Application.DoEvents()
        frmPopUp.mnuTopRight_Click(Nothing, New System.EventArgs())
        System.Windows.Forms.Application.DoEvents()

        'open handle to cicl
        errorStatus = atxml_Initialize("SFP", Guid.NewGuid.ToString())

        Allocation = Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\ATS\", "PAWSAllocationPath", Nothing)

        Response = Space(4096)
        XmlBuf = "<AtXmlTestRequirements>" & "    <ResourceRequirement>" & "        <ResourceType>Source</ResourceType>" & "        <SignalResourceName>MIL1553_1</SignalResourceName> " & "    </ResourceRequirement> " & "</AtXmlTestRequirements>"        '
        errorStatus = atxml_ValidateRequirements(XmlBuf, Allocation, Response, 4096)
        XmlBuf = "<AtXmlTestRequirements>"
        XmlBuf = XmlBuf & "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & "  <SignalResourceName>DCPS_1</SignalResourceName> " & "</ResourceRequirement> "
        XmlBuf = XmlBuf & "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & "  <SignalResourceName>DCPS_2</SignalResourceName> " & "</ResourceRequirement>"
        XmlBuf = XmlBuf & "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & "  <SignalResourceName>DCPS_3</SignalResourceName> " & "</ResourceRequirement>"
        XmlBuf = XmlBuf & "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & "  <SignalResourceName>DCPS_4</SignalResourceName> " & "</ResourceRequirement>"
        XmlBuf = XmlBuf & "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & "  <SignalResourceName>DCPS_5</SignalResourceName> " & "</ResourceRequirement>"
        XmlBuf = XmlBuf & "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & "  <SignalResourceName>DCPS_6</SignalResourceName> " & "</ResourceRequirement>"
        XmlBuf = XmlBuf & "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & "  <SignalResourceName>DCPS_7</SignalResourceName> " & "</ResourceRequirement>"
        XmlBuf = XmlBuf & "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & "       <SignalResourceName>DCPS_8</SignalResourceName> " & "</ResourceRequirement>"
        XmlBuf = XmlBuf & "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & "       <SignalResourceName>DCPS_9</SignalResourceName> " & "</ResourceRequirement>"
        XmlBuf = XmlBuf & "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & "       <SignalResourceName>DCPS_10</SignalResourceName> " & "</ResourceRequirement>"
        XmlBuf = XmlBuf & "</AtXmlTestRequirements>"

        errorStatus = atxml_ValidateRequirements(XmlBuf, Allocation, Response, Len(XmlBuf))

        garrsPsResourceName(0) = "DCPS_1"
        garrsPsResourceName(1) = "DCPS_2"
        garrsPsResourceName(2) = "DCPS_3"
        garrsPsResourceName(3) = "DCPS_4"
        garrsPsResourceName(4) = "DCPS_5"
        garrsPsResourceName(5) = "DCPS_6"
        garrsPsResourceName(6) = "DCPS_7"
        garrsPsResourceName(7) = "DCPS_8"
        garrsPsResourceName(8) = "DCPS_9"
        garrsPsResourceName(9) = "DCPS_10"

        SAISMGR.frmToolBar.Timer1.Enabled = True
        frmToolBar.Show()

    End Sub


    Function FileExists(ByRef path As String) As Short
        '************************************************************
        '* ManTech Test Systems Software Module                     *
        '************************************************************
        '* Nomenclature   : VIPERT SAIS Manager Toolbar               *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     This Routine will trap file errors                   *
        '*    PARAMETERS:                                           *
        '*     path$ = Full Path and File Name to be Tested         *
        '*     [Returns] = True / False Condition of File Opening   *
        '*    EXAMPLE:                                              *
        '*      If FileExists("C:\Myfile.txt") then Exit Sub        *
        '************************************************************
        Dim X As Object

        
        X = FreeFile()
        'Ignore Run-Time Numbers
        On Error Resume Next
        'Test File
        
        FileOpen(X, path, OpenMode.Input)
        If Err.Number = 0 Then
            FileExists = True
        Else
            FileExists = False 'File Error
        End If
        
        FileClose(X)

    End Function



    Sub TerminateApp32(ByRef AppTitle As String)
        '************************************************************
        '* ManTech Test Systems Software Module                     *
        '************************************************************
        '* Nomenclature   : VIPERT SAIS Manager Toolbar               *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     This Routine will terminatee an external application *
        '*    PARAMETERS:                                           *
        '*     AppTitle$ = Window Caption application to terminate  *
        '*    EXAMPLE:                                              *
        '*     TerminateApp32 Notepad                               *
        '************************************************************
        Dim CurrWnd As Integer
        Dim Title As String
        Dim WindowHandle As Integer
        Dim rc As Integer
        Dim lpString As New VB6.FixedLengthString(255)
        Dim Ret As Integer

        System.Windows.Forms.Application.DoEvents()
        CurrWnd = GetWindow(frmToolBar.Handle.ToInt32, GW_HWNDFIRST) 'Get First Window
        While CurrWnd <> 0 'Get All Window Information
            lpString.Value = Space(255)
            Ret = GetWindowText(CurrWnd, lpString.Value, 255)
            Title = Trim(lpString.Value)
            'MsgBox Title$
            If UCase(Title) = UCase(AppTitle) Then
                WindowHandle = CurrWnd
            End If
            CurrWnd = GetWindow(CurrWnd, GW_HWNDNEXT)
        End While
        If WindowHandle <> 0 Then
            rc = PostMessage(WindowHandle, &H10S, 0, 0) 'WM_CLOSE = &H10
        End If

    End Sub

    Public Sub DcpsSetPolarity(ByRef Supply As Short, ByRef polarity As String)
        '************************************************************
        '* Written By/Date     : Grady Johnson 11/2/98              *
        '*    DESCRIPTION:                                          *
        '*     This Module set the output polarity of a power supply*
        '*    EXAMPLE:                                              *
        '*     DcpsSetPolarity 7, "-"                               *
        '*    PARAMETERS:                                           *
        '*     Supply%   =  The supply number of which to set       *
        '*     Polarity%  =  The polarity "-" or "+"                *
        '* Procedure added per ECO-3047-078 for polarity testing.   *
        '************************************************************

        Dim Byte3 As Short

        If polarity = "-" Then
            Byte3 = &H3S 'Negative
        Else
            Byte3 = &H2S 'Default to Positive
        End If

        'Send Command
        SendDCPSCommand(Supply, Chr(32 + Supply) & Chr(&H80S) & Chr(Byte3)) 'Set polarity

    End Sub


    Public Function sDTSPath(ByRef sPath As String) As String
        Dim X As Short 'Index
        Dim iCounter As Short 'Loop Counter

        'Parse off end until a "\" is found
        For X = Len(sPath) To 1 Step -1
            If Asc(Mid(sPath, X, 1)) = 92 Then 'chr(92) = \
                Exit For
            End If
        Next X
        sDTSPath = Left(sPath, X - 1)

    End Function
    Function GatherIniFileInformation(ByRef lpApplicationName As String, ByRef lpKeyName As String, ByRef lpDefault As String) As String
        '************************************************************
        '* ManTech Test Systems Software Module                     *
        '************************************************************
        '* Nomenclature   : System Monitor  [SystemStartUp]         *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     Finds a value on in the TETS.INI File                *
        '*    PARAMETERS:                                           *
        '*     lpApplicationName$ -[Application] in TETS.INI File   *
        '*     lpKeyName$ - KEYNAME= in TETS.INI File               *
        '*     lpDefault$ - Default value to return if not found    *
        '*    RETURNS                                               *
        '*     String containing the key value or the lpDefault     *
        '*    EXAMPLE:                                              *
        '*     FilePath$ = GatherIniFileInformation("Heading", ...  *
        '*      ..."MY_FILE", "")                                   *
        '************************************************************
        'Reqiires (3) Windows Api Functions
        'Declare Function GetPrivateProfileString Lib "kernel32" Alias "GetPrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As Any, ByVal lpDefault As String, ByVal lpReturnedString As String, ByVal nSize As Long, ByVal lpFileName As String) As Long
        'Declare Function WritePrivateProfileString Lib "kernel32" Alias "WritePrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As Any, ByVal lpString As Any, ByVal lpFileName As String) As Long

        Dim lpReturnedString As New StringBuilder(4096)
        Dim nSize As Integer = 4096 'Return Buffer Size
        Dim lpFileName As String 'INI File Key Name "Key=?"
        Dim ReturnValue As Integer 'Return Value Buffer
        Dim FileNameInfo As String 'Formatted Return String
        Dim lpString As String 'String to write to INI File


        'Find Windows Directory

        lpFileName = "C:\Users\Public\Documents\ATS\ATS.ini"

        FileNameInfo = ""
        'Find File Locations
        ReturnValue = GetPrivateProfileString(lpApplicationName, lpKeyName, lpDefault, lpReturnedString, nSize, lpFileName)
        FileNameInfo = Trim(lpReturnedString.ToString)
        FileNameInfo = Mid(FileNameInfo, 1, Len(FileNameInfo))
        'If File Locations Missing, then create empty keys
        If FileNameInfo = lpDefault & Chr(0) Or FileNameInfo = lpDefault Then
            lpString = Trim(lpDefault)
            ReturnValue = WritePrivateProfileString(lpApplicationName, lpKeyName, lpString, lpFileName)
        End If

        'Return Information In INI File
        GatherIniFileInformation = FileNameInfo

    End Function

    Sub Release1553CICLHandle()
        Dim Allocation As String = ""
        Dim errorStatus As Integer = 0
        Dim XmlBuf As String
        Dim Response As String = ""

        XmlBuf = "<AtXmlSignalDescription> " & vbCrLf &
                        "   <SignalAction>Disable</SignalAction>" & vbCrLf &
                        "   <SignalResourceName>" & "MIL1553_1" &
                        "</SignalResourceName>" & vbCrLf &
                        "   <SignalSnippit>" & vbCrLf &
                        "       <Signal Name=""1553_SIGNAL"" Out=""exchange"">" & vbCrLf &
                        "           <" & "NA" & " name=""exchange""" & " ReleaseHandle=""True""" & "/>" & vbCrLf &
                        "       </Signal>" & vbCrLf &
                        "   </SignalSnippit>" & vbCrLf &
                        "</AtXmlSignalDescription>"

        Response = Space(4096)
        errorStatus = atxml_IssueSignal(XmlBuf, Response, 4096)

    End Sub

    Sub Restore1553CICLHandle()
        Dim Allocation As String = ""
        Dim errorStatus As Integer = 0
        Dim XmlBuf As String
        Dim Response As String = ""

        XmlBuf = "<AtXmlSignalDescription> " & vbCrLf &
                       "   <SignalAction>Enable</SignalAction>" & vbCrLf &
                       "   <SignalResourceName>" & "MIL1553_1" &
                       "</SignalResourceName>" & vbCrLf &
                       "   <SignalSnippit>" & vbCrLf &
                       "       <Signal Name=""1553_SIGNAL"" Out=""exchange"">" & vbCrLf &
                       "           <" & "NA" & " name=""exchange""" & " ReleaseHandle=""False""" & "/>" & vbCrLf &
                       "       </Signal>" & vbCrLf &
                       "   </SignalSnippit>" & vbCrLf &
                       "</AtXmlSignalDescription>"

        Response = Space(4096)
        errorStatus = atxml_IssueSignal(XmlBuf, Response, 4096)

    End Sub

    Sub CopilotProcessMonitorThread()
        Dim copilotProcesses() As Process
        copilotProcesses = Process.GetProcessesByName("copilot")

        While (copilotProcesses.Length > 0)
            System.Threading.Thread.Sleep(10)
            copilotProcesses = Process.GetProcessesByName("copilot")
        End While

        Restore1553CICLHandle()

    End Sub

End Module