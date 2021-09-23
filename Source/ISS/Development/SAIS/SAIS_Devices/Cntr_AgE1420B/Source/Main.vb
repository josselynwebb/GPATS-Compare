'Option Strict Off
'Option Explicit On

Imports System
Imports System.Windows.Forms
Imports System.Windows.Forms.Screen
Imports System.Text
Imports System.Drawing
Imports System.Diagnostics
Imports Microsoft.VisualBasic
Imports Microsoft.Win32

Public Module CtMain


	'=========================================================
    '//2345678901234567890123456789012345678901234567890123456789012345678901234567890
    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////
    '//
    '// Virtual Instrument Portable Equipment Repair/Tester (VIPER/T) Software Module
    '//
    '// File:       Main.bas
    '//
    '// Date:       27OCT06
    '//
    '// Purpose:    SAIS: Universal Counter/Timer Front Panel
    '//
    '// Instrument: Agilent E1420B Universal Counter/Timer (Cntr)
    '//
    '//
    '// Revision History
    '// Rev      Date                  Reason                                           Author
    '// =======  =======    =======================================                     ===========================
    '// 1.0.0.0  27Oct06    Initial Release                                             EADS, North America Defense
    '// 1.1.0.1  15Nov06    The following changes were made:                            M.Hendricksen, EADS
    '//                     - Changed Logo
    '//                     - Implemented CICL requirements of not sending any commands
    '//                         except queries to the instrument on start up or Reset.
    '//                     - Correct the code for the Textbox Range entry to be able
    '//                         to scroll and input discrete numbers correctly.
    '//                     - Corrected the code for Modifiers ARM-START/STOP-LEVEL we were
    '//                         looking for MAX,MIN,DEF but instr was returning values.
    '//                         SFP now looks for values.
    '//                     - Added code to hide the "Auto-Range" feature when not in
    '//                         frequency mode, when loading from a file.
    '// 1.2.0.1 04Jan07     The following changes were made:                            M.Hendricksen, EADS North America Defense
    '//                     - Changed the code to measure Period in TakeMeasurement()
    '//                         from INIT1 FETC1 to just MEAS1:PER?  INIT and FETC1 had
    '//                         trouble at times performing period measurments.
    '//                     - Corrected code in TakeMeasurment() to clear out the error
    '//                         buffer in the event of an error.  Buffer was not being
    '//                         cleared out and would cause an error when a command was sent.
    '// 1.3.0.0 26Mar09     - Supported channel 2 (SENS2) commands when time interval   D.Masters, EADS North America Test & Services
    '//                         mode is selected
    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////
    '


    '-----------------API / DLL Declarations------------------------------
    'Declare Function hpe1420b_init Lib "..\Build\hpe1420b.dll" (ByVal resourceName$, ByVal IDQuery%, ByVal resetDevice%, instrumentHandle&) As Long
    'Declare Function hpe1420b_readInstrData Lib "..\Build\hpe1420b.dll" (ByVal instrumentHandle&, ByVal numberBytesToRead&, ByVal ReadBuffer$, numBytesRead&) As Long
    'Declare Function hpe1420b_writeInstrData Lib "..\Build\hpe1420b.dll" (ByVal instrumentHandle&, ByVal writeBuffer$) As Long
    'Declare Function hpe1420b_errorMessage Lib "hpe1420b.dll" (ByVal instrumentHandle&, ByVal errorCode&, ByVal errorMessage$) As Long
    'Declare Function hpe1420b_close Lib "hpe1420b.dll" (ByVal instrumentHandle&) As Long
    'Declare Function hpe1420b_errorQuery Lib "hpe1420b.dll" (ByVal instrumentHandle&, ByVal errorCode&, ByVal errorMessage$) As Long
    Declare Function atxml_Initialize Lib "AtXmlApi.dll" (ByVal proctype As String, ByVal guid As String) As Integer
    Declare Function atxml_Close Lib "AtXmlApi.dll"  As Integer

    Declare Function atxml_ValidateRequirements Lib "AtXmlApi.dll" (ByVal TestRequirements As String, ByVal Allocation As String, ByVal Availability As String, ByVal BufferSize As Integer) As Integer

    Declare Function atxml_WriteCmds Lib "AtXmlApi.dll" (ByVal ResourceName As String, ByVal InstrumentCmds As String, ByRef ActWriteLen As Integer) As Integer

    Declare Function atxml_ReadCmds Lib "AtXmlApi.dll" (ByVal ResourceName As String, ByVal ReadBuffer As String, ByVal BufferSize As Integer, ByRef ActReadLen As Integer) As Integer

    Declare Function atxmlDF_viSetAttribute Lib "AtxmlDriverFunc.DLL" (ByVal ResourceName As String, ByVal vi As Integer, ByVal attrName As Integer, ByVal attrValue As Integer) As Integer

    Declare Function atxmlDF_viOut16 Lib "AtxmlDriverFunc.DLL" (ByVal ResourceName As String, ByVal vi As Integer, ByVal accSpace As Integer, ByVal offset As Integer, ByVal val16 As Integer) As Integer

    Declare Function atxmlDF_viStatusDesc Lib "AtxmlDriverFunc.DLL" (ByVal ResourceName As String, ByVal vi As Integer, ByVal status As Integer, ByVal desc As String) As Integer

    Declare Function atxmlDF_viClear Lib "AtxmlDriverFunc.DLL" (ByVal ResourceName As String, ByVal vi As Integer) As Integer


    Declare Function GetPrivateProfileString Lib "kernel32" Alias "GetPrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpDefault As String, ByVal lpReturnedString As String, ByVal nSize As Integer, ByVal lpFileName As String) As Integer
    Declare Function WritePrivateProfileString Lib "kernel32" Alias "WritePrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpString As String, ByVal lpFileName As String) As Integer
    'Declare Function' viClear Lib "VISA32.DLL" Alias "#260" (ByVal vi As Long) As Long   'Code changed DJoiner 11/17/00
    'Declare Function viSetAttribute Lib "VISA32.DLL" Alias "#134" (ByVal vi As Long, ByVal attrName As Long, ByVal attrValue As Long) As Long
    '-----------------Init and GUID ---------------------------------------
    Public Const guid As String = "{5B7F8E05-0434-496d-B0B6-5CB00B438B44} "
    Public Const proctype As String = "SFP"
    '-----------------About Box Constants----------------------------------
    Public Const INSTRUMENT_NAME As String = "Counter/Timer" 'About Box Constant
    Public Const MANUF As String = "Keysight" 'About Box Constant
    Public Const MODEL_CODE As String = "E1420B" 'About Box Constant
    Public Const RESOURCE_NAME As String = "CNTR_1" 'About Box Constant
    Public frmParent As frmHPE1420B 'About Box Constant
    '-----------------Global Constants------------------------------------
    Public Q As String 'Utility ASCII Quote (") Symbol
    Public DebugDwh As Short 'Debugging Flag

    '-----------------VISA ERROR Global Constants------------------------------------
    Public Const VI_ERROR_SYSTEM_ERROR As Integer = &HBFFF0000
    Public Const VI_ERROR_TMO As Integer = &HBFFF0015

    '-----------------Delay Constants-------------------------------------
    Const SECS_IN_DAY As Integer = 86400
    '-----------------Global Variables------------------------------------
    Public LiveMode As Boolean 'Enable/Disable Live Instrument Communication
    Public InstrumentMode As Short 'Determines the current instrument function
    Public instrumentHandle As Integer 'VISA Handle for this instance of the Counter/Timer
    Public ErrorStatus As Integer 'VISA Return Error Value
    Public ResourceName As String 'VISA Instrument Name
    Public ReadBuffer As String 'VISA Data Input Buffer
    Public DisplayUnit As String 'Unit to be appended to a measurement
    Public Channel As Short 'Selected Counter/Timer Input Channel
    Public sWindowsDir As String 'Path of Windows Directory
    Public bTIP_Running As Boolean 'Flag indicating SAIS was invoked by TIP Studio
    Public bBuildingTIPstr As Boolean 'Flag indicating TIP design mode is building the SCPI CMD string
    Public sTIP_Mode As String 'TIP mode indicator
    Public sTIP_CMDstring As String 'TIP SCPI Command string defining instrument setup
    Public sTIP_Measured As String 'Measured value not in Eng. Notation format (Absolute Value)
    Public bDoNotTalk As Boolean
    Public bDualChannel As Boolean 'Flag for 2-channel mode
    Public VisaError As Boolean 'Needed to recover from visa error
    Public CommandBuffer As String 'Needed to help recover from visa error

    '-----------------Global Arrays---------------------------------------
    Public TRIGGER_SOURCE_VALUE(11) As String 'Combo Box Value
    Public OUTPUT_TRIGGER_STATE(7) As Short 'Combo Box Value
    Public ChanAttenuation(2) As Short 'Current Attuation for each Channel
    '-----------------Input Text Box / Spin Button Instruction -----------
    Public Const MAX_SETTINGS As Short = 58
    Public SetCur(MAX_SETTINGS) As String ' "Current Settings" Array
    Public SetDef(MAX_SETTINGS) As String ' "Default Settings" Array
    Public SetMin(MAX_SETTINGS) As String ' "Maximum Settings" Array
    Public SetMax(MAX_SETTINGS) As String ' "Minimum Settings" Array
    Public SetUOM(MAX_SETTINGS) As String ' "Unit Of Measure" Array
    Public SetInc(MAX_SETTINGS) As String ' "Increments" Array
    Public SetRes(MAX_SETTINGS) As String ' "Increments" Array
    Public SetCmd(MAX_SETTINGS) As String ' "SCPI command" Array
    Public SetMinInc(MAX_SETTINGS) As String ' "Increments" Array
    Public SetRngMsg(MAX_SETTINGS) As String ' "Range Message" for Status Bar Array
    Public Const RANGE As Short = 1 'Range Input Text Box Index
    Public Const APERTURE As Short = 2 'Aperture Input Text Box Index
    Public Const ABS_TRIGGER As Short = 3 'Absolute trigger Input Text Box Index
    Public Const REL_TRIGGER As Short = 4 'Relative Trigger Input Text Box Index
    Public Const TI_DELAY As Short = 5 'Time Interval Delay Input Text Box Index
    Public Const DUR_TIMEOUT As Short = 6 'Acquisition Timeout Duration Input Text Box Index, DJoiner 11/27/00
    Public Const ABS_TRIG_CHAN2 As Short = 7 'Absolute trigger input text box index, D.Masters 03/26/2009
    Public Const REL_TRIG_CHAN2 As Short = 8 'Relative trigger input text box index, D.Masters 03/26/2009
    Public RangeDisplay As Boolean 'Input Box Variable
    Public ModeUnit As String 'Unit Appended to a Measurement
    Public bolInitGUI As Boolean 'Boolean value initializing application
    Public bolReset As Boolean 'Boolean value for when Resetting application, DJoiner 11/16/00
    Public bolErrorReset As Boolean 'Booleen value for resetting  Error Counter, DJoiner 11/20/00
    Public bolMeasure As Boolean 'Boolean value while Measuring, DJoiner 11/20/00
    Public bolArm As Boolean 'Bolean value for if "Arm" tab is enabled, DJoiner 11/20/00
    Public Const MAX_HOLDERS As Short = 36 'Max number of Command String Holders, DJoiner 11/24/00 (Updated 03/26/2009 by D.Masters)
    Public strHolder(MAX_HOLDERS) As String 'Holder for Command String, DJoiner 11/24/00
    Public strTemp As String 'Temp holder for command string, DJoiner 11/22/00
    Public strTimeDur As String 'TimeOut Duration setting String Holder, DJoiner 11/28/00
    Public bChangeMode As Boolean 'Boolean value while Changing Instrument Mode, DJoiner 11/20/00
    Public intModeButton As Short 'Index of Instrument Mode, DJoiner 11/28/00
    Public strHold As String 'To make "Hold" Available when conditions are satisfied, DJoiner 12/01/00
    Dim bolTimeOut As Boolean 'Boolean value for a Time Out Error, DJoiner 12/01/00
    Dim ScreenRst As Boolean
    Public bSpin As Boolean 'Flag to not update Status Bar Message when from a SpinButton
    Private bStartup As Boolean
    Public bInstrumentMode As Boolean ' True if in GetCurrentInstrument Mode

    '-----------TIP CMD STRING CONSTANTS-----------
    '***INPut Options***
    Public Const INP1_ATT As Short = 7
    Public Const INP1_COUP As Short = 8
    Public Const INP1_IMP As Short = 9
    Public Const INP2_ATT As Short = 10
    Public Const INP2_COUP As Short = 11
    Public Const INP2_IMP As Short = 12
    Public Const INP_ROUT As Short = 13
    '***SENSe Options***
    Public Const SENS_AVER_STAT As Short = 14
    Public Const SENS_EVEN_LEV_ABS As Short = 15
    Public Const SENS_EVEN_LEV_ABS_AUTO As Short = 16
    Public Const SENS_EVEN_SLOP As Short = 17
    Public Const SENS_EVEN_HYST As Short = 18
    Public Const SENS_FREQ_APER As Short = 19
    Public Const SENS_PER_APER As Short = 20
    Public Const SENS_RAT_APER As Short = 21
    Public Const SENS_FREQ_RANG_AUTO As Short = 22
    Public Const SENS_ROSC_SOUR As Short = 23
    Public Const SENS_TINT_DEL_STAT As Short = 24
    Public Const SENS_TINT_DEL_TIME As Short = 25
    Public Const SENS_TOT_GATE_STAT As Short = 26
    Public Const SENS_TOT_GATE_POL As Short = 27
    Public Const SENS_ATIM_TIME As Short = 28
    Public Const SENS_ATIM_CHEC As Short = 29
    Public Const SENS_EVEN_LEV_REL As Short = 30
    Public Const SENS_FUNC As Short = 31
    '***ARM Options***
    Public Const ARM_STAR_SOUR As Short = 32
    Public Const ARM_STOP_SOUR As Short = 33
    Public Const ARM_STAR_LEV As Short = 34
    Public Const ARM_STAR_SLOP As Short = 35
    Public Const ARM_STOP_LEV As Short = 36
    Public Const ARM_STOP_SLOP As Short = 37
    '***OUTPut Options***
    Public Const OUTP_ROSC_STAT As Short = 38
    Public Const OUTP_TTLT0_STAT As Short = 39
    Public Const OUTP_TTLT1_STAT As Short = 40
    Public Const OUTP_TTLT2_STAT As Short = 41
    Public Const OUTP_TTLT3_STAT As Short = 42
    Public Const OUTP_TTLT4_STAT As Short = 43
    Public Const OUTP_TTLT5_STAT As Short = 44
    Public Const OUTP_TTLT6_STAT As Short = 45
    Public Const OUTP_TTLT7_STAT As Short = 46
    '***CONFigure Options***
    Public Const CONF_FREQ As Short = 47
    Public Const CONF_PER As Short = 48
    Public Const CONF_FREQ_RAT As Short = 49
    Public Const CONF_RTIM As Short = 50
    Public Const CONF_FTIM As Short = 51
    Public Const CONF_TINT As Short = 52
    Public Const CONF_PWID As Short = 53
    Public Const CONF_NWID As Short = 54
    Public Const CONF_TOT As Short = 55
    Public Const CONF_PHAS As Short = 56
    Public Const INIT As Short = 57
    Public Const CONF_QUOTE_FREQ As Short = 58 'mh added due to instrument returning "FREQ not FREQ have to compensate for both

    '***Read Instrument Mode***
    Public iStatus As Short
    Public gctlParse As New VIPERT_Common_Controls.Common
    '###############################################################

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

        Dim date1 As New Date(1900, 1, 1, 0, 0, 0)
        dGetTime = DateDiff(DateInterval.Day, Now, date1) + (DateTime.Now.TimeOfDay.TotalSeconds / SECS_IN_DAY)
    End Function

    Sub ExitCounterTimer()
        If sTIP_Mode = "TIP_RUNPERSIST" Or sTIP_Mode = "TIP_RUNSETUP" Or sTIP_Mode = "TIP_HOLDOFF" Then
            If Val(sTIP_Measured) = 9.91E+37 Then
                sTIP_Measured = "9.91E+37"
            End If
            If UCase(frmHPE1420B.txtCtDisplay.Text) <> "ERROR" And Trim(frmHPE1420B.txtCtDisplay.Text) <> "" Then
                'Put measurement into [TIPS] STATUS key for studio
                SetKey("TIPS", "STATUS", "Ready " & StripNullCharacters(sTIP_Measured))
                SetKey("TIPS", "CMD", "")
            End If
        End If
        bolErrorReset = True

        'ADVINT REMOVED this line:
        'SendScpiCommand ("*RST ; *CLS")

        'Give Resources Back To Op-System
        ReturnInstrumentHandle()
        Application.Exit()
    End Sub

    Public Function RoundMSD(ByVal Direction As String, ByVal Number As Double) As Double
        '************************************************************
        '* Nomenclature   : HP E1420B Universal Counter/Timer       *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     Rounds Number to the nearest Most Significant Digit).*
        '*    PARAMETERS:                                           *
        '*     Direction$: Must be "Up" or "Down".  Signifies which *
        '*                 way to round the number.                 *
        '*     Number#:    The number to be rounded.                *
        '*    EXAMPLE:                                              *
        '*     Number=10987.1, Direction$="Down" -> 10000           *
        '************************************************************
        'GLOBAL VARIABLES USED: None
        'REQUIRES:  None
        'VERSION:   8/13/96
        'CUSTODIAN: J. Hill
        Dim Exponent As Short

        Exponent = 0
        If Number >= 10 Then 'For positive exponent
            Do While Number >= 10
                Number /= 10.0
                Exponent += 1
            Loop
        ElseIf Number < 1 And Number <> 0 Then            'For negative exponent (but not 0)
            Do While Number < 1
                Number *= 10.0
                Exponent -= 1
            Loop
        End If

        Number = Val(Str(Number)) ' Clear out very low LSBs from binary math
        If UCase(Convert.ToString(Direction)) = "UP" Then
            Number = Fix(Number + 0.9) * 10 ^ Exponent
        Else
            Number = Fix(Number) * 10 ^ Exponent
        End If

        'Return Function Value
        RoundMSD = Number
    End Function

    Sub AdjustTriggerOutput()
        '************************************************************
        '* Nomenclature   : HP E1420B Universal Counter/Timer       *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     Sets SAIS Panel to default or user Trigger Choice    *
        '*    EXAMPLE:                                              *
        '*     AdjustTriggerOutput                                  *
        '************************************************************
        'Check Combo Box Text
        Select Case frmHPE1420B.cboTriggerOutput.Text
                'OUTPUT_TRIGGER_STATE remembers the user's trigger ON/OFF choice
            Case "TTL Trigger 7"
                frmHPE1420B.optTriggerOutputOn.Checked = OUTPUT_TRIGGER_STATE(7)
            Case "TTL Trigger 6"
                frmHPE1420B.optTriggerOutputOn.Checked = OUTPUT_TRIGGER_STATE(6)
            Case "TTL Trigger 5"
                frmHPE1420B.optTriggerOutputOn.Checked = OUTPUT_TRIGGER_STATE(5)
            Case "TTL Trigger 4"
                frmHPE1420B.optTriggerOutputOn.Checked = OUTPUT_TRIGGER_STATE(4)
            Case "TTL Trigger 3"
                frmHPE1420B.optTriggerOutputOn.Checked = OUTPUT_TRIGGER_STATE(3)
            Case "TTL Trigger 2"
                frmHPE1420B.optTriggerOutputOn.Checked = OUTPUT_TRIGGER_STATE(2)
            Case "TTL Trigger 1"
                frmHPE1420B.optTriggerOutputOn.Checked = OUTPUT_TRIGGER_STATE(1)
            Case "TTL Trigger 0"
                frmHPE1420B.optTriggerOutputOn.Checked = OUTPUT_TRIGGER_STATE(0)
        End Select

        'Set option button according to user choice
        frmHPE1420B.optTriggerOutputOff.Checked = Not frmHPE1420B.optTriggerOutputOn.Checked
    End Sub

    Sub FillArmSourceComboBox(ByRef sArray() As String)
        '************************************************************
        '* Nomenclature   : HP E1420B Universal Counter/Timer       *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     Fills the Combo Box Control with Text                *
        '*    EXAMPLE:                                              *
        '*     FillArmSourceComboBox TextArrayLabels$               *
        '************************************************************
        Dim ArmSourceInitLoop As Short 'Item Text Index
        Dim Box As Short 'Combo Box Control Index

        For Box = 1 To 2
            'Empty ComboBox
            frmHPE1420B.cboArmStartSource.Items.Clear()
            frmHPE1420B.cboArmStopSource.Items.Clear()
            'Fill With Specified Number of array elements necessary to Instrument Function
            For ArmSourceInitLoop = 0 To 11
                frmHPE1420B.cboArmStartSource.Items.Insert(0, sArray(ArmSourceInitLoop))
                frmHPE1420B.cboArmStopSource.Items.Insert(0, sArray(ArmSourceInitLoop))
            Next ArmSourceInitLoop
            frmHPE1420B.cboArmStartSource.SelectedIndex = 11
            frmHPE1420B.cboArmStopSource.SelectedIndex = 11
        Next Box
    End Sub

    Sub FillTriggerOutputComboBox(ByRef sArray() As String)
        '************************************************************
        '* Nomenclature   : HP E1420B Universal Counter/Timer       *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     Fills the Combo Box Control with Text                *
        '*    EXAMPLE:                                              *
        '*     FillTriggerOutputComboBox TextArrayLabels$           *
        '************************************************************
        Dim TriggerOutputInitLoop As Short 'Array Text Index

        'Empty ComboBox
        frmHPE1420B.cboTriggerOutput.Items.Clear()

        'Fill With Specified Number of array elements necessary to Instrument Function
        For TriggerOutputInitLoop = 4 To 11
            frmHPE1420B.cboTriggerOutput.Items.Insert(0, sArray(TriggerOutputInitLoop))
        Next TriggerOutputInitLoop
        'Select First Chioce
        frmHPE1420B.cboTriggerOutput.SelectedIndex = 0
    End Sub

'#Const defUse_CenterChildForm = True
#If defUse_CenterChildForm Then
    Sub CenterChildForm(ByVal ParentForm As Form, ByVal Childform As Form)
        '************************************************************
        '* ManTech Test Systems Software Module                     *
        '************************************************************
        '* Nomenclature   : Instrument About Box / Splash Screen    *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     This Module Centers One Form With Respect To The     *
        '*     Current Position of Another Form.                    *
        '*    EXAMPLE:                                              *
        '*     CenterChildForm frmMain, frmAboutBox                 *
        '************************************************************

        Childform.Top = ((ParentForm.Height/2)+ParentForm.Top)-Childform.Height/2
        Childform.Left = ((ParentForm.Width/2)+ParentForm.Left)-Childform.Width/2
    End Sub
#End If

    Sub CenterForm(ByVal Form As Object)
        '************************************************************
        '* ManTech Test Systems Software Module                     *
        '************************************************************
        '* Nomenclature   : HP E1420B Universal Counter/Timer       *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     This Module Centers One Form With Respect To The     *
        '*     User's Screen.                                       *
        '*    EXAMPLE:                                              *
        '*     CenterForm frmMain                                   *
        '************************************************************
        Form.Top = PrimaryScreen.Bounds.Height / 2 - Form.Height / 2
        Form.Left = PrimaryScreen.Bounds.Width / 2 - Form.Width / 2
    End Sub

    Sub ChangeInstrumentMode()
        '************************************************************
        '* Nomenclature   : HP E1420B Universal Counter/Timer       *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     Updates panel objects when the user changes          *
        '*     instrument measurement modes.                        *
        '*    EXAMPLE:                                              *
        '*     ChangeInstrumentMode                                 *
        '************************************************************
        frmHPE1420B.optInputChannel2.Enabled = True
        ''*****************RESET INPut Subsystem*****************
        bDualChannel = False
        frmHPE1420B.optCh1Attenuation1X.Enabled = True
        frmHPE1420B.optCh1CouplingDC.Enabled = True
        frmHPE1420B.optCh1Impedance1MOhm.Enabled = True
        frmHPE1420B.optCh1Attenuation10X.Enabled = True
        frmHPE1420B.optCh1CouplingAC.Enabled = True
        frmHPE1420B.optCh1Impedance50Ohm.Enabled = True
        'Attenuation: X1
        frmHPE1420B.optCh1Attenuation1X.Checked = True
        frmHPE1420B.optCh2Attenuation1X.Checked = True
        frmHPE1420B.optCh1Attenuation10X.Checked = False
        frmHPE1420B.optCh2Attenuation10X.Checked = False
        'Coupling: DC
        frmHPE1420B.optCh1CouplingDC.Checked = True
        frmHPE1420B.optCh2CouplingDC.Checked = True
        frmHPE1420B.optCh1CouplingAC.Checked = False
        frmHPE1420B.optCh2CouplingAC.Checked = False
        'Impedance: 1 MOhm
        frmHPE1420B.optCh2Impedance50Ohm.Checked = False
        frmHPE1420B.optCh2Impedance50Ohm.Checked = False
        frmHPE1420B.optCh1Impedance1MOhm.Checked = True
        frmHPE1420B.optCh2Impedance1MOhm.Checked = True

        frmHPE1420B.fraTotalizeGateState.Visible = False 'Show only in TOTALIZE mode:  T.Biggs 1/31/03
        frmHPE1420B.fraTotalizeGatePolarity.Visible = False
        frmHPE1420B.fraTiDelay.Visible = False 'Show only in TIME-INTERVAL mode:  T.Biggs 1/31/03
        frmHPE1420B.fraAperture.Visible = False 'Aperture
        frmHPE1420B.fraCh1Range.Visible = True 'Range
        frmHPE1420B.chkRange.Checked = False 'Auto Range
        frmHPE1420B.chkRange.Visible = False 'Auto Range

        'Initialize trigger mode/level controls
        Select Case InstrumentMode
            Case 4, 5, 7, 8
                'Don't show for R/T, F/T, -PW, or +PW
                frmHPE1420B.fraCh1TriggeringMethod.Visible = False
                frmHPE1420B.fraCh1RelativeTrigger.Visible = False
                frmHPE1420B.fraCh1AbsoluteTrigger.Visible = False
                frmHPE1420B.fraCh2RelativeTrigger.Visible = False
                frmHPE1420B.fraCh2AbsoluteTrigger.Visible = False
                frmHPE1420B.FrameChannel2TriggerSetup.Visible = False ' D. Masters 3/26/2009
            Case 6
                ' Time Interval mode
                frmHPE1420B.fraCh1TriggeringMethod.Visible = True
                If frmHPE1420B.chkCh1AbsTrigAuto.Checked = True Then
                    frmHPE1420B.fraCh1RelativeTrigger.Visible = True
                Else
                    frmHPE1420B.fraCh1AbsoluteTrigger.Visible = True
                End If

                'Added by D. Masters 3/26/2009
                frmHPE1420B.FrameChannel2TriggerSetup.Visible = True
                If frmHPE1420B.chkCh2AbsTrigAuto.Checked Then
                    frmHPE1420B.fraCh2RelativeTrigger.Visible = True
                Else
                    frmHPE1420B.fraCh2AbsoluteTrigger.Visible = True
                End If

            Case Else
                frmHPE1420B.fraCh1TriggeringMethod.Visible = True
                If frmHPE1420B.chkCh1AbsTrigAuto.Checked Then
                    frmHPE1420B.fraCh1RelativeTrigger.Visible = True
                Else
                    frmHPE1420B.fraCh1AbsoluteTrigger.Visible = True
                End If
                frmHPE1420B.fraCh2RelativeTrigger.Visible = False
                frmHPE1420B.fraCh2AbsoluteTrigger.Visible = False
                frmHPE1420B.FrameChannel2TriggerSetup.Visible = False ' D. Masters 3/26/2009
        End Select

        Select Case InstrumentMode 'Global Var
            Case 1
                '"Frequency" 1.0E-3 to 2.0E+08 DEF AUTO
                frmHPE1420B.fraRouting.Visible = False 'Disable Routing
                frmHPE1420B.fraAperture.Visible = True 'Aperture
                frmHPE1420B.panRange.Visible = False
                frmHPE1420B.chkRange.Enabled = True
                frmHPE1420B.chkRange.Checked = True 'Auto Range
                frmHPE1420B.chkRange.Visible = True 'Range


                '<EXP VAL>,<Resolution>
                SetMin(RANGE) = "0.001"
                SetMax(RANGE) = "200000000" 'Changed code to reflect the true max ranges
                SetDef(RANGE) = "1000000"
                SetRes(RANGE) = "0.0000E+00"
                SetRangeMessage()
                SetCur(RANGE) = SetDef(RANGE)
                DisableGateAverage()
                frmHPE1420B.txtRange.Text = SetCur(RANGE)
                ModeUnit = "Hz"
                frmHPE1420B.txtRange_Leave(New Object(), New EventArgs())
                'MHfrmHPE1420B.TextBox(RANGE).Visible = False
                'MHfrmHPE1420B.TextBox(RANGE).Enabled = False
            Case 2
                '"Period" 5.0E-9 to 1.0E+03 DEF:100.000ns
                frmHPE1420B.fraRouting.Visible = False 'Disable Routing
                frmHPE1420B.optInputChannel1.Enabled = True
                frmHPE1420B.optInputChannel2.Enabled = True
                frmHPE1420B.fraAperture.Visible = True 'Aperture
                '<EXP VAL>,<Resolution>
                SetMin(RANGE) = "5.0E-9"
                SetMax(RANGE) = "1.0E+03"
                SetDef(RANGE) = "1.0E-7"
                SetRes(RANGE) = "0.0000E+00"
                SetRngMsg(RANGE) = "Min: 5 ns    Def: 100 ns    Max: 1000 s"
                SetCur(RANGE) = SetDef(RANGE)
                DisableGateAverage()
                frmHPE1420B.panRange.Visible = True
                frmHPE1420B.txtRange.Text = SetCur(RANGE)
                frmHPE1420B.chkRange.Enabled = False
                frmHPE1420B.chkRange.Checked = False 'Auto Range
                frmHPE1420B.chkRange.Visible = False 'Range
                ModeUnit = "Sec"
                frmHPE1420B.txtRange_Leave(New Object(), New EventArgs())
            Case 4
                '"RiseTime" 1.5E-08 to 3.0E-02 DEF:100.000ns
                frmHPE1420B.fraRouting.Visible = False 'Disable Routing
                '<LOW REF>,<UP REF>,<EXP VAL>,<Resolution>
                SetMin(RANGE) = "1.5E-08"
                SetMax(RANGE) = "1E-03" 'entered correct value (kim)
                SetDef(RANGE) = "1.0E-7"
                SetRes(RANGE) = "0.0000E+00"
                SetRngMsg(RANGE) = "Min: 15 ns    Def: 100 ns    Max: 1 ms"
                SetCur(RANGE) = SetDef(RANGE)
                frmHPE1420B.txtRange.Text = SetCur(RANGE)
                DisableGateAverage()
                DisableCh2()
                ModeUnit = "Sec"
                frmHPE1420B.txtRange_Leave(New Object(), New EventArgs())
            Case 5
                '"FallTime" 1.5E-08 to 3.0E-02 DEF:100.000ns
                frmHPE1420B.fraRouting.Visible = False 'Disable Routing
                '<LOW REF>,<UP REF>,<EXP VAL>,<Resolution>
                SetMin(RANGE) = "1.5E-08"
                SetMax(RANGE) = "1E-03" 'entered correct value (kim)
                SetDef(RANGE) = "1.0E-7"
                SetRes(RANGE) = "0.0000E+00"
                SetRngMsg(RANGE) = "Min: 15 ns    Def: 100 ns    Max: 1 ms"
                SetCur(RANGE) = SetDef(RANGE)
                frmHPE1420B.txtRange.Text = SetCur(RANGE)
                DisableGateAverage()
                DisableCh2()
                ModeUnit = "Sec"
                frmHPE1420B.txtRange_Leave(New Object(), New EventArgs())
            Case 7
                '"PositivePulseWidth" 5.0E-09 to 1.0E-02 DEF:100.000ns
                frmHPE1420B.fraRouting.Visible = False 'Disable Routing
                '<REF>,<EXP VAL>,<Resolution>
                SetMin(RANGE) = "5.0E-09"
                SetMax(RANGE) = "1.0E-02"
                SetDef(RANGE) = "1.0E-7"
                SetRes(RANGE) = "0.0000E+00"
                SetRngMsg(RANGE) = "Min: 5 ns    Def: 100 ns    Max: 10 ms"
                SetCur(RANGE) = SetDef(RANGE)
                frmHPE1420B.optInputChannel1.Enabled = True
                frmHPE1420B.optInputChannel2.Enabled = True
                'Trigger levels are hard set with CONFigure
                frmHPE1420B.fraCh1TriggeringMethod.Visible = False
                frmHPE1420B.fraCh1AbsoluteTrigger.Visible = False
                frmHPE1420B.fraCh1RelativeTrigger.Visible = False
                EnableGateAverage()
                DisableCh2()
                frmHPE1420B.txtRange.Text = SetCur(RANGE)
                ModeUnit = "Sec"
                frmHPE1420B.txtRange_Leave(New Object(), New EventArgs())
            Case 8
                '"NegativePulseWidth" 5.0E-09 to 1.0E-02 DEF:100.000ns
                frmHPE1420B.fraRouting.Visible = False 'Disable Routing
                '<REF>,<EXP VAL>,<Resolution>
                SetMin(RANGE) = "5.0E-09"
                SetMax(RANGE) = "1.0E-02"
                SetDef(RANGE) = "1.0E-7"
                SetRes(RANGE) = "0.0000E+00"
                SetRngMsg(RANGE) = "Min: 5 ns    Def: 100 ns    Max: 10 ms"
                SetCur(RANGE) = SetDef(RANGE)
                frmHPE1420B.optInputChannel1.Enabled = True
                frmHPE1420B.optInputChannel2.Enabled = True
                EnableGateAverage()
                DisableCh2()
                frmHPE1420B.txtRange.Text = SetCur(RANGE)
                ModeUnit = "Sec"
                frmHPE1420B.txtRange_Leave(New Object(), New EventArgs())
            Case 6
                '"TimeInterval" -1.0E-09 to 1.0E+03 DEF:100.000ns
                '<EXP VAL>,<Resolution>
                SetMin(RANGE) = "1.0E-09" 'entered correct positive value (kim)
                SetMax(RANGE) = "1.0E+03"
                SetDef(RANGE) = "1.0E-7"
                SetRes(RANGE) = "0.0000E+00"
                SetRngMsg(RANGE) = "Min: 1 ns    Def: 100 ns    Max: 1000 s"
                SetCur(RANGE) = SetDef(RANGE)
                frmHPE1420B.txtRange.Text = SetCur(RANGE)
                frmHPE1420B.fraTiDelay.Visible = True
                frmHPE1420B.fraRouting.Visible = True 'Enable Routing
                frmHPE1420B.optRoutingSeparate.Checked = True 'Default to Sep Operation
                EnableGateAverage()
                frmHPE1420B.optInputChannel1.Checked = True
                frmHPE1420B.optInputChannel1.Enabled = True
                frmHPE1420B.optInputChannel2.Enabled = False
                EnableCh2() 'Enable Input Tab controls for CH2
                ModeUnit = "Sec"
                frmHPE1420B.txtRange_Leave(New Object(), New EventArgs())
            Case 9
                '"Totalize"
                frmHPE1420B.fraTotalizeGateState.Visible = True
                If frmHPE1420B.chkTotalizeGateState.Checked = True Then
                    frmHPE1420B.fraTotalizeGatePolarity.Visible = True
                End If
                frmHPE1420B.fraCh1Range.Visible = False
                frmHPE1420B.fraRouting.Visible = False 'Disable Routing
                '<EXP VAL>,<Resolution>
                'No Range (Disabled)
                frmHPE1420B.fraRouting.Visible = False 'Disable Routing
                frmHPE1420B.optInputChannel1.Enabled = True
                frmHPE1420B.optInputChannel2.Enabled = True
                DisableGateAverage()
                frmHPE1420B.optInputChannel1.Enabled = True
                frmHPE1420B.optInputChannel2.Enabled = True
                frmHPE1420B.optInputChannel1.Checked = True
                frmHPE1420B.optInputChannel2.Checked = False
                ModeUnit = "Events"
                frmHPE1420B.txtRange_Leave(New Object(), New EventArgs())
            Case 3
                '"FrequencyRatio" 1.0E-11 to 1.0E+11 DEF: 1
                bDualChannel = True
                frmHPE1420B.fraAperture.Visible = True 'Aperture
                frmHPE1420B.fraRouting.Visible = False 'Disable Routing
                '<EXP VAL>,<Resolution>
                SetMin(RANGE) = "1.0E-11" 'new min value entered from the manual (kim)
                SetMax(RANGE) = "1.0E+11" ' new max value entered from manual (kim)
                SetDef(RANGE) = "1.0000E+00"
                SetRes(RANGE) = "0.0000E+00"
                SetRngMsg(RANGE) = "Min: 1.0E-10    Def: 1    Max: 1.0E+12"
                SetCur(RANGE) = SetDef(RANGE)
                EnableCh2()
                DisableGateAverage()
                frmHPE1420B.txtRange.Text = SetCur(RANGE)
                ModeUnit = ""
                frmHPE1420B.txtRange_Leave(New Object(), New EventArgs())
                'These functions are available, but the firmware is unreliable, the measurement is inaccurate, and
                'these functions are not required.  If the firmware improves the instrument can be modified here.
            Case 12
                '"MinimumVoltage"
            Case 13
                '"MaximumVoltage"
            Case 14
                'mh this used to be 10"DcVoltage"
                ' phase function was added by kim 9-27-2000
            Case 10
                '"Phase", 10 is the button number pushed
                'MH Case 14 '"Phase"
                frmHPE1420B.fraAperture.Visible = False 'Aperture disable
                frmHPE1420B.fraRouting.Visible = False 'Disable Routing
                '<EXP VAL>,<Resolution>
                SetMin(RANGE) = "0" '
                SetMax(RANGE) = "360" '
                SetDef(RANGE) = "1.0000E+00"
                SetRes(RANGE) = "0.0000E+00"
                SetRngMsg(RANGE) = "Min: 0    Def: 1    Max: 360"
                SetCur(RANGE) = SetDef(RANGE)
                DisableGateAverage()
                DisableCh2()
                frmHPE1420B.fraCh1Range.Visible = False
                frmHPE1420B.txtRange.Text = SetCur(RANGE)
                ModeUnit = "Degree"
                frmHPE1420B.txtRange_Leave(New Object(), New EventArgs())

        End Select
        frmHPE1420B.fraAperture.BringToFront()
        frmHPE1420B.fraCh1Range.BringToFront()
        frmHPE1420B.fraChannel.BringToFront()
        frmHPE1420B.fraRouting.BringToFront()
    End Sub

    Sub Delay(ByRef dSeconds As Double)
        'DESCRIPTION:
        '   Delays for a specified number of seconds.
        '   50 milli-second resolution.
        '   Minimum delay is about 50 milli-seconds.
        'PARAMETERS:
        '   dSeconds: The number of seconds to Delay.
        Dim t As Double

        t = dGetTime()
        dSeconds /= SECS_IN_DAY
        Do
            Threading.Thread.Sleep(10)
        Loop While dGetTime() - t < dSeconds
    End Sub


'#Const defUse_DisableEnableUserEntryBoxSpin = True
#If defUse_DisableEnableUserEntryBoxSpin Then
    Sub DisableEnableUserEntryBoxSpin(ByVal ControlIndex As Short, ByVal Enable As Short)
        '************************************************************
        '* Nomenclature   : HP E1420B Universal Counter/Timer       *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     Enables/Disables Text Entry Objects                  *
        '*    PARAMETERS:                                           *
        '*     ControlIndex% = Index of Text Input Controls         *
        '*     Enable% = True for Enable, False for disable         *
        '*    EXAMPLE:                                              *
        '*     DisableEnableUserEntryBoxSpin TRIGGER, true          *
        '************************************************************
        If Enable Then 'Enable Objects
            frmHPE1420B.TextBox(ControlIndex).Enabled = True
            frmHPE1420B.TextBox(ControlIndex).BackColor = ColorTranslator.FromOle(QBColor(15))
            frmHPE1420B.SpinButton(ControlIndex).Enabled = True
        Else            'Disable Objects
            frmHPE1420B.TextBox(ControlIndex).Enabled = False
            frmHPE1420B.TextBox(ControlIndex).BackColor = ColorTranslator.FromOle(QBColor(7))
            frmHPE1420B.SpinButton(ControlIndex).Enabled = False
        End If

    End Sub
#End If

    Sub DisplayErrorMessage(ByVal ErrorStatus As Integer)
        '************************************************************
        '* Nomenclature   : HP E1420B Universal Counter/Timer       *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     Displays a VISA error code in text form              *
        '*    PARAMETERS:                                           *
        '*     ErrorStatus& = VISA Return Code                      *
        '*    EXAMPLE:                                              *
        '*     DisplayErrorMessage VisaRetVal&                      *
        '************************************************************
        Dim ErrorText As String = "" 'Error Text Buffer
        Dim intResponse As DialogResult 'DJoiner 12/01/00
        Dim TrimmedError As String = "" 'DJoiner 12/01/00

        'errorLookupStatus = atxmlDF_viStatusDesc(ResourceName, errorLookupStatus&, errorStatus, ErrorText$)
        '
        '    TrimmedError = StripNullCharacters(ErrorText$)
        '
        If bolTimeOut Then
            If bolMeasure And bTIP_Running Then
                sTIP_Measured = "9.91E+37" 'No signal
                frmHPE1420B.txtCtDisplay.Text = "0.0 " & DisplayUnit
                Delay(1)
            End If
            intResponse = MsgBox("The Counter/Timer Has Timed Out or there is no signal present at " & "the selected input." & vbCrLf & vbCrLf & "Would you like to RESET Instrument?", MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.Question Or MsgBoxStyle.Critical Or MsgBoxStyle.YesNo, "Instrument Error")

        Else
            frmHPE1420B.txtCtDisplay.Text = "Error"
            If Strings.Left(sTIP_Mode, 7) = "TIP_RUN" Then
                MsgBox(ErrorText, MsgBoxStyle.Exclamation, "VISA Error Message")
                SetKey("TIPS", "STATUS", "Error from VISA: " & Hex(ErrorStatus) & " " & StripNullCharacters(ErrorText.ToString()))
                ' ExitCounterTimer
            Else
                intResponse = MsgBox("Instrument Status: " & TrimmedError & vbCrLf & vbCrLf & "Would you like to RESET Instrument?", MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.Question Or MsgBoxStyle.Critical Or MsgBoxStyle.YesNo, "Instrument Error")
            End If
        End If '

        If intResponse = DialogResult.Yes Then 'If Yes
            frmHPE1420B.txtCtDisplay.Text = "Reading..."
            Delay(1)
            bolErrorReset = True
        Else
            bolErrorReset = False
            If bolMeasure And bTIP_Running Then
                'SendScpiCommand ("ABOR1;ABOR2;*CLS")
                ExitCounterTimer() 'Reset instrument and exit
            End If
        End If
    End Sub

    Public Function EngNotate(ByVal Number As Double, ByVal Digits As Short, ByVal Unit As String) As String
        EngNotate = ""
        '************************************************************
        '* Nomenclature   : HP E1420B Universal Counter/Timer       *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     Returns passed number as numeric string in           *
        '*     Engineering notation (every*3rd exponent) with       *
        '*     selectable precision along with Unit Of Measure.     *
        '*    EXAMPLE:                                              *
        '*     Number=10987.1, Uom="Ohm" -> "10.987 KOhm"           *
        '************************************************************
        Dim MULTIPLIER As Short
        Dim Negative As Boolean
        Dim Prefix As String
        Dim ReturnString As String

        MULTIPLIER = 0 : Negative = False 'Initialize local variables

        If Number < 0 Then ' If negative
            Number = Math.Abs(Number) 'Make it positive for now
            Negative = True 'Set flag
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
                Prefix = " T" 'Terra  E+12
            Case 3
                Prefix = " G" 'Giga   E+09
            Case 2
                Prefix = " M" 'Mega   E+06
            Case 1
                Prefix = " K" 'Kilo   E+03
            Case 0
                Prefix = "  " '<none> E+00
            Case -1
                Prefix = " m" 'milli  E-03
            Case -2
                Prefix = " " & Convert.ToString(Chr(181)) 'micro  E-06
            Case -3
                Prefix = " n" 'nano   E-09
            Case -4
                Prefix = " p" 'pico   E-12

            Case Else
                Prefix = " "
        End Select

        If Negative Then Number = -Number

        If MULTIPLIER > 4 Then
            ReturnString = "Ovr Rng"
        ElseIf MULTIPLIER < -4 Then
            ReturnString = "UndrRng"
        Else
            Select Case Digits
                Case 0
                    ReturnString = Number.ToString("N0")
                Case 1
                    ReturnString = Number.ToString("N1")
                Case 2
                    ReturnString = Number.ToString("N2")
                Case 3
                    ReturnString = Number.ToString("N3")
                Case 4
                    ReturnString = Number.ToString("N4")
                Case 5
                    ReturnString = Number.ToString("N5")
                Case 6
                    ReturnString = Number.ToString("N6")
                Case 7
                    ReturnString = Number.ToString("N7")
                Case 8
                    ReturnString = Number.ToString("N8")
                Case Else
                    ReturnString = Number.ToString("N8")
            End Select
        End If
        If Not (Negative) Then
            'Return Function Value
            'EngNotate$ = "+" & ReturnString$ & Prefix$ & Unit$
            EngNotate = ReturnString & Prefix & Unit 'Code change
        Else            'to eliminate "+"
            'Return Function Value
            EngNotate = ReturnString & Prefix & Unit
        End If
    End Function

    Sub InitInputBoxLists()
        '************************************************************
        '* Nomenclature   : HP E1420B Universal Counter/Timer       *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     Initializes program values and is typically called   *
        '*     once upon program start-up                           *
        '*    EXAMPLE:                                              *
        '*     InitInputBoxLists                                    *
        '************************************************************
        'Init flags
        RangeDisplay = False
        'Initialize Minimums
        SetMin(RANGE) = "0.001"
        SetMin(APERTURE) = "0.001"
        SetMin(ABS_TRIGGER) = "-10.2375"
        SetMin(REL_TRIGGER) = "10"
        SetMin(TI_DELAY) = ".001" 'correct min TI delay value entered
        SetMin(DUR_TIMEOUT) = ".1"
        SetMin(ABS_TRIG_CHAN2) = "-10.2375" ' Added D.Masters 03/26/2009
        SetMin(REL_TRIG_CHAN2) = "10" ' Added D.Masters 03/26/2009
        'Initialize Maximums
        SetMax(RANGE) = "100E6"
        SetMax(APERTURE) = "99.999"
        SetMax(ABS_TRIGGER) = "10.2375"
        SetMax(REL_TRIGGER) = "90"
        SetMax(TI_DELAY) = "99.999"
        SetMax(DUR_TIMEOUT) = "1500.000"
        SetMax(ABS_TRIG_CHAN2) = "10.2375" ' Added D.Masters 03/26/2009
        SetMax(REL_TRIG_CHAN2) = "90" ' Added D.Masters 03/26/2009
        'Initialize The Range Messages
        SetRngMsg(RANGE) = "Min: 0.001 Hz    Def: 100 MHz    Max: 200 MHz"
        SetRngMsg(APERTURE) = "Min: 1 mSec    Def: 100 mSec   Max: 99.999 Sec"
        SetRngMsg(ABS_TRIGGER) = "Min: -10.2375 V    Def: 0 V    Max: 10.2375 V"
        SetRngMsg(REL_TRIGGER) = "Min: 10%    Def: 50%   Max: 90%"
        SetRngMsg(TI_DELAY) = "Min: 1 mSec    Def: 100 mSec    Max: 99.999 Sec"
        SetRngMsg(DUR_TIMEOUT) = "Min: 100 mSec    Def: 5 Sec    Max: 1500 Sec"
        SetRngMsg(ABS_TRIG_CHAN2) = "Min: -10.2375 V    Def: 0 V    Max: 10.2375 V" ' Added D.Masters 03/26/2009
        SetRngMsg(REL_TRIG_CHAN2) = "Min: 10%    Def: 50%   Max: 90%" ' Added D.Masters 03/26/2009
        'Initialize Increment Values
        SetInc(RANGE) = "1"
        SetInc(APERTURE) = "1"
        SetInc(ABS_TRIGGER) = "1"
        SetInc(REL_TRIGGER) = "1"
        SetInc(TI_DELAY) = "1"
        SetInc(DUR_TIMEOUT) = ".1"
        SetInc(ABS_TRIG_CHAN2) = "1" ' Added D.Masters 03/26/2009
        SetInc(REL_TRIG_CHAN2) = "1" ' Added D.Masters 03/26/2009
        'Initialize Resolution
        SetRes(RANGE) = "0.000E+00"
        SetRes(APERTURE) = "0.000E+00"
        SetRes(ABS_TRIGGER) = "0.0000"
        SetRes(REL_TRIGGER) = "00"
        SetRes(TI_DELAY) = "00.000E+00"
        SetRes(DUR_TIMEOUT) = "0.0"
        'Initialize Current Value
        SetCur(RANGE) = "1.0000E+06"
        SetCur(APERTURE) = "100E-03"
        SetCur(ABS_TRIGGER) = "00.0000"
        SetCur(REL_TRIGGER) = "50"
        SetCur(TI_DELAY) = "10.000E-02"
        SetCur(DUR_TIMEOUT) = "5.0"
        SetCur(ABS_TRIG_CHAN2) = "00.0000" ' Added D.Masters 03/26/2009
        SetCur(REL_TRIG_CHAN2) = "50" ' Added D.Masters 03/26/2009
        'Initialize Defaults
        SetDef(RANGE) = "100E6"
        SetDef(APERTURE) = "100E-3"
        SetDef(ABS_TRIGGER) = "00.0000"
        SetDef(REL_TRIGGER) = "50"
        SetDef(TI_DELAY) = "10.000E-02"
        SetDef(DUR_TIMEOUT) = "5.0"
        SetDef(ABS_TRIG_CHAN2) = "00.0000" ' Added D.Masters 03/26/2009
        SetDef(REL_TRIG_CHAN2) = "50" ' Added D.Masters 03/26/2009
        'Initialize Command
        SetCmd(RANGE) = ""
        SetCmd(APERTURE) = ""
        SetCmd(ABS_TRIGGER) = ""
        SetCmd(REL_TRIGGER) = ""
        SetCmd(TI_DELAY) = ""
        SetCmd(DUR_TIMEOUT) = ""
        SetCmd(ABS_TRIG_CHAN2) = "" ' Added D.Masters 03/26/2009
        SetCmd(REL_TRIG_CHAN2) = "" ' Added D.Masters 03/26/2009
        'Set Unit Scale
        SetUOM(RANGE) = "1"
        SetUOM(APERTURE) = "1"
        SetUOM(ABS_TRIGGER) = "1"
        SetUOM(REL_TRIGGER) = "1"
        SetUOM(TI_DELAY) = "1"
        SetUOM(DUR_TIMEOUT) = "1"
        SetUOM(ABS_TRIG_CHAN2) = "1" ' Added D.Masters 03/26/2009
        SetUOM(REL_TRIG_CHAN2) = "1" ' Added D.Masters 03/26/2009
        'Set Min Inc
        SetMinInc(RANGE) = ".1"
        SetMinInc(APERTURE) = ".1"
        SetMinInc(ABS_TRIGGER) = ".1"
        SetMinInc(REL_TRIGGER) = ".1"
        SetMinInc(TI_DELAY) = ".1"
        SetMinInc(DUR_TIMEOUT) = ".1"
        SetMinInc(ABS_TRIG_CHAN2) = ".1" ' Added D.Masters 03/26/2009
        SetMinInc(REL_TRIG_CHAN2) = ".1" ' Added D.Masters 03/26/2009
        'Init Display
        frmHPE1420B.txtRange.Text = SetCur(RANGE)
        frmHPE1420B.txtRange_Leave(New Object(), New EventArgs())
        frmHPE1420B.txtAperture.Text = SetCur(APERTURE)
        frmHPE1420B.txtAperture_Leave(New Object(), New EventArgs())
        frmHPE1420B.txtCh1AbsoluteTrigger.Text = SetCur(ABS_TRIGGER)
        frmHPE1420B.txtCh1AbsoluteTrigger_Leave(New Object(), New EventArgs())
        frmHPE1420B.txtCh1RelativeTrigger.Text = SetCur(REL_TRIGGER)
        frmHPE1420B.txtCh1RelativeTrigger_Leave(New Object(), New EventArgs())
        frmHPE1420B.txtTiDelay.Text = SetCur(TI_DELAY)
        frmHPE1420B.txtTimeOut_Leave(New Object(), New EventArgs())

        ' Added D.Masters 03/26/2009
        frmHPE1420B.txtCh2AbsoluteTrigger.Text = SetCur(ABS_TRIG_CHAN2)
        frmHPE1420B.txtCh2AbsoluteTrigger_Leave(New Object(), New EventArgs())
        frmHPE1420B.txtCh2RelativeTrigger.Text = SetCur(REL_TRIG_CHAN2)
        frmHPE1420B.txtCh2RelativeTrigger_Leave(New Object(), New EventArgs())
    End Sub

    'Sub InitializeUniversalCounterTimer()
    '************************************************************
    '* Nomenclature   : HP E1420B Universal Counter/Timer       *
    '* Written By     : David W. Hartley                        *
    '*    DESCRIPTION:                                          *
    '*     Initializes Instrument Hardware and verifies         *
    '*     communication                                        *
    '*    EXAMPLE:                                              *
    '*     InitializeUniversalCounterTimer                      *
    '************************************************************
    '    'Counter Timer TETS Address--> VXI::136
    '    ErrorStatus& = hpe1420b_init(ResourceName:="VXI::136", IDQuery:=1, resetDevice:=1, instrumentHandle:=instrumentHandle&)
    '    If ErrorStatus& Then DisplayErrorMessage ErrorStatus&'
    '
    'End Sub
    '------------------------------------------------------------------

    Public Sub Main()
        '************************************************************
        '* Nomenclature   : HP E1420B Universal Counter/Timer       *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     This module is the program entry (starting) point.   *
        '************************************************************

        Dim sCmdLine As String

        'DebugDwh% = True 'Uncomment this line for debugging support
        frmParent = frmHPE1420B

        'Get command line arguments to determine if invoked by TIP Studio
        sCmdLine = Microsoft.VisualBasic.Command()
        sTIP_Mode = Trim(UCase(sCmdLine))
        If InStr(sTIP_Mode, "TIP") Then bTIP_Running = True
        LiveMode = True

        'Show Introduction Form
        If bolErrorReset = False Then
            If Not bTIP_Running Then SplashStart() 'Prevents showing Splash Screen when reset is called
            Delay(0.5)
            'Check System and Instrument For Errors
            LiveMode = VerifyInstrumentCommunication()
        End If
        'Set-Up Main Form
        strHold = "Not Available"
        InitializeArrays()
        If bolErrorReset = False Then
            CenterForm(frmHPE1420B) 'Prevents Screen from centering when reset is called
        End If 'while program is running.
        bolInitGUI = False 'EADS - added 5/12/2006
        bStartup = True
        FillTriggerOutputComboBox(TRIGGER_SOURCE_VALUE)
        FillArmSourceComboBox(TRIGGER_SOURCE_VALUE)
        InitInputBoxLists()
        bStartup = False
        'ToolBar Control Bug Fix
        frmHPE1420B.tbrCtFunctions.Height = 2310
        frmHPE1420B.tbrCtFunctions.Buttons(1 - 1).Pushed = True
        frmHPE1420B.tabUserOptions.SelectedIndex = 0

        If Not bTIP_Running Then
            CenterForm(frmHPE1420B)
            'Show the Front Panel
            frmHPE1420B.Refresh()
            'Reset Counter
            'ResetCounterTimer
            If Not bolErrorReset Then SplashEnd() 'Remove Introduction Form
        Else
            'Find Windows Directory
            sWindowsDir = Environment.GetFolderPath(Environment.SpecialFolder.Windows)
            'Position SAIS in upper Righthand corner of screen
            frmHPE1420B.Top = 0
            frmHPE1420B.Left = PrimaryScreen.Bounds.Width - frmHPE1420B.Width
            'Show the Front Panel
            frmHPE1420B.Refresh()
            'Reset Counter
            ResetCounterTimer()
            'Read, then clear the [TIPS]CMD key
            sTIP_CMDstring = sGetKey("TIPS", "CMD")
            sTIP_CMDstring = Trim(sTIP_CMDstring)
            If sTIP_CMDstring <> "" Then
                frmHPE1420B.Cursor = Cursors.WaitCursor
                SetKey("TIPS", "CMD", "")
                ConfigureFrontPanel()
                frmHPE1420B.Cursor = Cursors.Default
            End If
            If InstrumentMode = 9 And UCase(strHold) = "HOLD" Then
                frmHPE1420B.tabUserOptions.SelectedIndex = 3 'Display ARM tab
                SendStatusBarMessage("Click on START to initiate Events Count") 'Set Status Bar Message
            Else
                Select Case sTIP_Mode
                    Case "TIP_RUNPERSIST"
                        frmHPE1420B.chkContinuous.Checked = True
                        TakeMeasurement()
                    Case "TIP_DESIGN"
                        sTIP_CMDstring = "" 'Now that SAIS has been setup, Clear old data from string

                    Case Else
                        'Run Setup mode
                        TakeMeasurement()
                        Delay(0.5)
                        ExitCounterTimer()
                End Select
            End If
        End If

        'Set the Refresh rate when in "Debug" mode from 1000 = 1 sec to 25000 = 25 sec

        frmHPE1420B.Panel_Conifg.Refresh = 5000
        bInstrumentMode = True
        'If instrument is "Live" then get current configuration
        frmHPE1420B.ConfigGetCurrent()
        bInstrumentMode = False

        'DO NOT Put a break point After this point. Causes error if timer is running
        'Set the Mode the Instrument is in: Local = True, Debug = False
        If iStatus <> 1 Then
            frmHPE1420B.Panel_Conifg.SetDebugStatus(True)
        Else
            frmHPE1420B.Panel_Conifg.SetDebugStatus(False)
        End If
    End Sub

    Sub SetCONFigureOptions()
        '************************************************************
        '* Nomenclature   : HP E1420B Universal Counter/Timer       *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     Sets CONF Subsystem Options in instrument            *
        '*     communication                                        *
        '*    EXAMPLE:                                              *
        '*     SetCONFigureOptions                                  *
        '************************************************************

        Select Case InstrumentMode
            Case 1
                '"Frequency" 1.0E-3 to 2.0E+08 DEF AUTO
                '<EXP VAL>,<Resolution>
                If frmHPE1420B.chkRange.Checked = False Then
                    SendScpiCommand("CONF" & Channel & ":FREQ " & SetCur(RANGE))
                End If
            Case 2
                '"Period" 5.0E-9 to 1.0E+03 DEF:100.000ns
                '<EXP VAL>,<Resolution>
                SendScpiCommand("CONF" & Channel & ":PER " & SetCur(RANGE))
            Case 4
                '"RiseTime" 1.5E-08 to 3.0E-02 DEF:100.000ns
                '<LOW REF>,<UP REF>,<EXP VAL>,<Resolution>
                SendScpiCommand("CONF" & Channel & ":RTIM 90, 10, " & SetCur(RANGE))
            Case 5
                '"FallTime" 1.5E-08 to 3.0E-02 DEF:100.000ns
                '<LOW REF>,<UP REF>,<EXP VAL>,<Resolution>
                SendScpiCommand("CONF" & Channel & ":FTIM 90, 10, " & SetCur(RANGE))
            Case 7
                '"PositivePulseWidth" 5.0E-09 to 1.0E-02 DEF:100.000ns
                '<REF>,<EXP VAL>,<Resolution>
                SendScpiCommand("CONF" & Channel & ":PWID 50, " & SetCur(RANGE))
            Case 8
                '"NegativePulseWidth" 5.0E-09 to 1.0E-02 DEF:100.000ns
                '<REF>,<EXP VAL>,<Resolution>
                SendScpiCommand("CONF" & Channel & ":NWID 50, " & SetCur(RANGE))
            Case 6
                '"TimeInterval" -1.0E-09 to 1.0E+03 DEF:100.000ns
                '<EXP VAL>,<Resolution>
                SendScpiCommand("CONF" & Channel & ":TINT " & SetCur(RANGE))
            Case 9
                '"Totalize"
                '<EXP VAL>,<Resolution>
                'No Range (Disabled)
                SendScpiCommand("CONF" & Channel & ":TOT")
            Case 3
                '"FrequencyRatio" 1.0E-10 to 1.0E+12 DEF: 1
                '<EXP VAL>,<Resolution>
                SendScpiCommand("CONF" & Channel & ":FREQ:RAT " & SetCur(RANGE))
            Case 12
                '"MinimumVoltage"
            Case 13
                '"MaximumVoltage"
            Case 14
                '"DcVoltage"
            Case 11
                '"AcVoltage"
            Case 10
                '"Phase"    0 to 360 degree def: 1  phase function
                '<EXP VAL>,<RESOLUTION>
                SendScpiCommand("CONF" & Channel & ":PHAS " & SetCur(RANGE))
        End Select
    End Sub

    Sub SetRangeMessage()
        ' Procedure added so that correct settings are in the message box
        SetRngMsg(RANGE) = "Min: 0.001 Hz    Def: 1.00 MHz     Max: " & Mid(SetMax(RANGE), 1, 3) & " MHz" 'jaydude
    End Sub

    Public Sub SplashStart()
        '************************************************************
        '* Nomenclature   : HP E1420B Universal Counter/Timer       *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     This module displays the About/Splash Screen         *
        '*    EXAMPLE:                                              *
        '*     SplashStart                                          *
        '************************************************************

        '! Load frmAbout
        frmAbout.cmdOk.Visible = False
        frmAbout.Show()
        frmAbout.Refresh()
    End Sub

    Public Sub SplashEnd()
        '************************************************************
        '* Nomenclature   : HP E1420B Universal Counter/Timer       *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     This module removes the About/Splash Screen          *
        '*    EXAMPLE:                                              *
        '*     SplashEnd                                            *
        '************************************************************

        frmAbout.Hide()
    End Sub


    Public Function ReadInstrumentBuffer() As String
        ReadInstrumentBuffer = ""
        '************************************************************
        '* Nomenclature   : HP E1420B Universal Counter/Timer       *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     This module reads the contents of the instrument     *
        '*     output register following a querry command           *
        '*    RETURNS:                                              *
        '*     String contents of the instrument output buffer      *
        '*    EXAMPLE:                                              *
        '*     ReadInstrumentBuffer                                 *
        '************************************************************
        Dim NBR As Integer 'Number of Bytes Read
        Dim status As Integer
        Dim clearStatus As Integer

        If LiveMode Then
            '
            ReadBuffer = Space(255)
            status = atxml_ReadCmds(ResourceName, ReadBuffer, 255, NBR)

            If NBR = 0 Then
                ReadBuffer = "0.0"
            End If
            ReadBuffer = StripNullCharacters(ReadBuffer)

            ' The instr sends a quote in beginning of mode i.e. "FREQ, need to parse out
            If InStr(ReadBuffer.ToString(), """") = 1 Then
                ReadBuffer = Strings.Right(ReadBuffer, Len(ReadBuffer) - 1)
            End If
            ' clear the status if we have a Visa Error so SFP can recover
            If VisaError = True Then
                status = 0
                Exit Function
            End If


            If status Then
                If status = VI_ERROR_TMO Then
                    bolTimeOut = True
                End If

                If status = VI_ERROR_SYSTEM_ERROR Or status > VI_ERROR_SYSTEM_ERROR Then
                    VisaError = True
                    frmHPE1420B.txtCtDisplay.Font = New Font("Ms Sans Serif", 18) 'Change Font size to 18 point
                    frmHPE1420B.txtCtDisplay.Text = "VISA ERROR"
                    frmHPE1420B.chkContinuous.Checked = False
                    SendStatusBarMessage("Counter/Timer Ready For Measurement")
                    frmHPE1420B.tabUserOptions.SelectedIndex = 0
                    DisplayErrorMessage(ErrorStatus)
                    ScreenRst = True
                    bolTimeOut = False
                    clearStatus = atxmlDF_viClear(ResourceName, 255)
                    If bolErrorReset Then
                        ResetCounterTimer()
                    Else
                        SendScpiCommand("ABOR1;ABOR2;*CLS")
                    End If
                    VisaError = False
                    frmHPE1420B.txtCtDisplay.Text = "Ready"
                End If
            Else
                VisaError = False
            End If
            '    Else
            '    '   ##########################################
            '    '   Action Required: Remove debug code
            '    '   This will enable simulation of commands
            '        If gcolCmds.Count <> 0 Then
            '            ReadBuffer$ = gctlDebug.Simulate(gcolCmds)
            '
            '    '       Clear collection
            '            Set gcolCmds = New Collection
            '        End If
            '    '   ##########################################
            '
            '
            '        'Return Function Value
            '         'ReadInstrumentBuffer$ = Trim$(ReadBuffer$)
        End If

        'Return Function Value
        If ScreenRst = False Then
            ReadInstrumentBuffer = StripNullCharacters(ReadBuffer)
            bolErrorReset = False
        End If
    End Function


    Sub ResetCounterTimer()
        '************************************************************
        '* ManTech Test Systems Software Sub                        *
        '************************************************************
        '* Nomenclature   : HP E1420B Universal Counter/Timer       *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     Resets The Counter Hardware and Software             *
        '*     To It's Default Power-On State                       *
        '*    EXAMPLE:                                              *
        '*      ResetCounterTimer                                   *
        '************************************************************
        Dim OutputTriggerInitIndex As Short

        If LiveMode Then
            ' ' viClear (instrumentHandle&)
        End If
        bolReset = False
        ResetHolders()
        bolInitGUI = True
        'Start Watchdog Timer
        'SendScpiCommand "SENS:ATIM:CHEC STAR"
        ''*****************INPut Subsystem*****************
        'Attenuation: X1
        frmHPE1420B.optCh1Attenuation1X.Checked = True
        frmHPE1420B.optCh2Attenuation1X.Checked = True
        frmHPE1420B.optCh1Attenuation10X.Checked = False
        frmHPE1420B.optCh2Attenuation10X.Checked = False
        'Coupling: DC
        frmHPE1420B.optCh1CouplingDC.Checked = True
        frmHPE1420B.optCh2CouplingDC.Checked = True
        frmHPE1420B.optCh1CouplingAC.Checked = False
        frmHPE1420B.optCh2CouplingAC.Checked = False
        'Impedance: 1 MOhm
        frmHPE1420B.optCh1Impedance1MOhm.Checked = False
        frmHPE1420B.optCh2Impedance1MOhm.Checked = False
        frmHPE1420B.optCh1Impedance50Ohm.Checked = True
        frmHPE1420B.optCh2Impedance50Ohm.Checked = True
        frmHPE1420B.fraRouting.Visible = True
        frmHPE1420B.optRoutingSeparate.Checked = True
        DisableCh2()
        ''*****************ARM Subsystem*****************
        'Manual Open/Close gate
        frmHPE1420B.cmdArmStart.Enabled = True
        frmHPE1420B.cmdArmStop.Enabled = False
        'External Start Level: 1.6V (TTL)
        frmHPE1420B.optArmStartLevelTTL.Checked = True
        frmHPE1420B.optArmStartLevelECL.Checked = False
        frmHPE1420B.optArmStartLevelGND.Checked = False
        'External Stop Level: 1.6V (TTL)
        frmHPE1420B.optArmStopLevelTTL.Checked = True
        frmHPE1420B.optArmStopLevelECL.Checked = False
        frmHPE1420B.optArmStopLevelGND.Checked = False
        'Start Slope: Positive
        frmHPE1420B.optArmStartSlopePositive.Checked = True
        frmHPE1420B.optArmStartSlopeNegative.Checked = False
        'Start Source : Immediate
        frmHPE1420B.cboArmStartSource.SelectedIndex = 11
        'Stop Slope: Positive
        frmHPE1420B.optArmStopSlopePositive.Checked = True
        frmHPE1420B.optArmStopSlopeNegative.Checked = False
        'Stop Source : Immediate
        frmHPE1420B.cboArmStopSource.SelectedIndex = 11
        ''*****************Added by Wm. Thompson
        ''*****************October 20, 2005
        SetCur(RANGE) = SetDef(RANGE)
        frmHPE1420B.txtRange.Text = SetCur(RANGE)
        frmHPE1420B.txtRange_Leave(New Object(), New EventArgs())
        frmHPE1420B.txtRange.Visible = True
        ''*****************SENSe Subsystem*****************
        'Aperature: 100ms
        SetCur(APERTURE) = SetDef(APERTURE)
        frmHPE1420B.txtAperture.Text = SetCur(APERTURE)
        frmHPE1420B.txtAperture_Leave(New Object(), New EventArgs())
        'Auto Trigger State: OFF
        frmHPE1420B.chkCh1AbsTrigAuto.Checked = False
        frmHPE1420B.fraCh1AbsoluteTrigger.Visible = True
        frmHPE1420B.fraCh1RelativeTrigger.Visible = False
        frmHPE1420B.chkCh2AbsTrigAuto.Checked = False ' Added by D.Masters 03/26/2009

        'Average State: OFF
        frmHPE1420B.optTiGateAvgOff.Checked = True
        'TINT Delay State: OFF
        frmHPE1420B.chkTIDelay.Checked = False
        frmHPE1420B.panTiDelay.Visible = False
        'TINT Delay Time: 100ms
        SetCur(TI_DELAY) = SetDef(TI_DELAY)
        frmHPE1420B.txtTiDelay.Text = SetCur(TI_DELAY)
        frmHPE1420B.txtTiDelay_Leave(New Object(), New EventArgs())
        'Event Trigger Level: 0 volts
        SetCur(ABS_TRIGGER) = SetDef(ABS_TRIGGER)
        frmHPE1420B.txtCh1AbsoluteTrigger.Text = SetCur(ABS_TRIGGER)
        frmHPE1420B.txtCh1RelativeTrigger_Leave(New Object(), New EventArgs())

        'Added by D. Masters 3/26/2009
        SetCur(REL_TRIGGER) = SetDef(REL_TRIGGER)
        frmHPE1420B.txtCh1RelativeTrigger.Text = SetCur(REL_TRIGGER)
        frmHPE1420B.txtCh1RelativeTrigger_Leave(New Object(), New EventArgs())

        'Event Slope: Positive
        frmHPE1420B.optCh1TriggerSlopePositive.Checked = True
        frmHPE1420B.optCh1TriggerSlopeNegative.Checked = False
        'Function: Frequency
        InstrumentMode = 1
        frmHPE1420B.tbrCtFunctions.Buttons(1 - 1).Pushed = True
        ChangeInstrumentMode()
        'Hysteresis: Default
        frmHPE1420B.optCh1HysteresisMax.Checked = False
        frmHPE1420B.optCh1HysteresisDef.Checked = True
        frmHPE1420B.optCh1HysteresisMin.Checked = False

        'Added by D. Masters 03/26/2009
        frmHPE1420B.optCh2HysteresisMin.Checked = False
        frmHPE1420B.optCh2HysteresisDef.Checked = True
        frmHPE1420B.optCh2HysteresisMax.Checked = False

        frmHPE1420B.optCh2TriggerSlopePositive.Checked = True
        frmHPE1420B.optCh2TriggerSlopeNegative.Checked = False

        frmHPE1420B.fraCh2RelativeTrigger.Visible = False
        frmHPE1420B.fraCh2AbsoluteTrigger.Visible = False
        frmHPE1420B.FrameChannel2TriggerSetup.Visible = False

        SetCur(ABS_TRIG_CHAN2) = SetDef(ABS_TRIG_CHAN2)
        frmHPE1420B.txtCh2AbsoluteTrigger.Text = SetCur(ABS_TRIG_CHAN2)
        frmHPE1420B.txtCh2AbsoluteTrigger_Leave(New Object(), New EventArgs())

        SetCur(REL_TRIG_CHAN2) = SetDef(REL_TRIG_CHAN2)
        frmHPE1420B.txtCh2RelativeTrigger.Text = SetCur(REL_TRIG_CHAN2)
        frmHPE1420B.txtCh2RelativeTrigger_Leave(New Object(), New EventArgs())
        'End Added by D.Masters 03/26/2009

        'Set Default Channel 1
        frmHPE1420B.optInputChannel1.Checked = True
        Channel = 1
        frmHPE1420B.FrameChannel1TriggerSetup.Text = "Channel 1" ' Added D.Masters 03/26/2009

        'Timeout Duration
        frmHPE1420B.txtTimeOut.Text = SetDef(DUR_TIMEOUT)
        frmHPE1420B.txtTimeOut_Leave(New Object(), New EventArgs())
        'Range: Auto
        frmHPE1420B.chkRange.Checked = True
        frmHPE1420B.panRange.Visible = False
        'Relative Trigger Level: 50%
        SetCur(REL_TRIGGER) = SetDef(REL_TRIGGER)
        frmHPE1420B.txtCh1RelativeTrigger.Text = SetCur(REL_TRIGGER)
        frmHPE1420B.txtCh1RelativeTrigger_Leave(New Object(), New EventArgs())
        'ROSCillator Source: CLK10
        frmHPE1420B.optTimebaseSourceVXIClk.Checked = False
        frmHPE1420B.optTimebaseSourceIntern.Checked = True
        frmHPE1420B.optTimebaseSourceExtern.Checked = False
        'Totalize Gate Polarity: Normal
        frmHPE1420B.optTotalizeGatePolarityNormal.Checked = True
        frmHPE1420B.optTotalizeGatePolarityInvert.Checked = False
        'Totalize Gate State: OFF
        frmHPE1420B.chkTotalizeGateState.Checked = False
        ''*****************OUTPut Subsystem*****************
        'TTLTrg<n>:State: OFF
        For OutputTriggerInitIndex = 0 To 7
            'Output Triggers : Off
            OUTPUT_TRIGGER_STATE(OutputTriggerInitIndex) = False
        Next OutputTriggerInitIndex
        frmHPE1420B.optTriggerOutputOff.Checked = True
        frmHPE1420B.optTriggerOutputOn.Checked = False
        frmHPE1420B.cboTriggerOutput.SelectedIndex = 0
        'RSOCillator State: OFF
        frmHPE1420B.chkOutputTimebase.Checked = False
        ''***************** Reset User GUI *****************
        '    'Set / Re-Set Display To Ready State
        frmHPE1420B.txtCtDisplay.Font = New Font("MS Sans Serif", 18) 'Change Font size to 18 point
        frmHPE1420B.txtCtDisplay.Text = "Ready"
        '    'Set / Re-Set Continuous Box To Ready State
        frmHPE1420B.chkContinuous.Checked = False
        '    'Set / Re-Set Status Bar To Ready State
        SendStatusBarMessage("Counter/Timer Ready For Measurement")
        frmHPE1420B.tabUserOptions.SelectedIndex = 0
        frmHPE1420B.tbrCtFunctions.Enabled = True
        For Each Button In frmHPE1420B.tbrCtFunctions.Buttons
            Button.Pushed = False
        Next
        frmHPE1420B.tbrCtFunctions.Buttons(InstrumentMode - 1).Pushed = True
        frmHPE1420B.optAcquisitionTimeoutOn.Checked = True
        CheckForManGate() 'Check for "Hold" as a option
        bolInitGUI = False 'Reset Boolean values to False
        bolErrorReset = False
        bolMeasure = False
        ScreenRst = False
        SendScpiCommand("*CLS") 'Reset This Instrument to the Power-On State
        SendScpiCommand("*RST")
    End Sub

    Sub ReturnInstrumentHandle()
        '************************************************************
        '* ManTech Test Systems Software Sub                        *
        '************************************************************
        '* Nomenclature   : HP E1420B Universal Counter/Timer       *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     Gives instrument handle resources back to VISA layer *
        '*    EXAMPLE:                                              *
        '*      ReturnInstrumentHandle                              *
        '************************************************************

        If LiveMode Then
            '        ErrorStatus& = hpe1420b_close(ByVal instrumentHandle&)
        End If
    End Sub

    Public Sub SendScpiCommand(ByVal Command As String)
        '************************************************************
        '* ManTech Test Systems Software Sub                        *
        '************************************************************
        '* Nomenclature   : HP E1420B Universal Counter/Timer       *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     Sends a SCPI command to the instrument               *
        '*    PARAMETERS:                                           *
        '*     Command$ = SCPI Command to send instrument.          *
        '*    EXAMPLE:                                              *
        '*      SendScpiCommand "*RST"                              *
        '* Error checking during send-message routine is added by   *
        '* Kim 10-20-2000                                           *
        '************************************************************
        Dim sInstrumentStatus As String
        Dim status As Integer
        Debug.Print(Command) 'For Debuging purposes
        If bolErrorReset Then Exit Sub 'Exit Sub if Resetting
        If bStartup=True Then Exit Sub
        'only send query commands when set
        If bInstrumentMode And InStr(Command, "?") = 0 Then Exit Sub

        'Check mode in
        If frmHPE1420B.Panel_Conifg.DebugMode = True And Strings.Right(Command, 1) <> "?" Then Exit Sub

        If LiveMode And ( Not bolInitGUI) Then
            Delay(0.05)
            '       ErrorStatus& = hpe1420b_writeInstrData(instrumentHandle&, writeBuffer:=Command$)
            status = atxml_WriteCmds(ResourceName, Command, Command.Length)

            CommandBuffer = Command

            If VisaError=True Then Exit Sub

            If InStr(Command, "?") = 0 Then
                '            ErrorStatus& = hpe1420b_writeInstrData(instrumentHandle&, "SYST:ERR?")
                status = atxml_WriteCmds(ResourceName, "SYST:ERR?", CLng(Len("SYST:ERR?")))

                sInstrumentStatus = ReadInstrumentBuffer()
                If bolErrorReset Then
                    Exit Sub 'Exit Sub if Resetting
                End If
                If Val(sInstrumentStatus) <> 0 Then
                    If bTIP_Running Then 'Put Error Message in STATUS key, clear CMD key
                        frmHPE1420B.txtCtDisplay.Text = "Error"
                        MsgBox("Error Code: " & sInstrumentStatus & vbCrLf & vbCrLf & "Cmd Str: " & Command, MsgBoxStyle.Exclamation, "Counter/Timer Error Message")
                        SetKey("TIPS", "STATUS", "Error " & StripNullCharacters(sInstrumentStatus))
                        SetKey("TIPS", "CMD", "")
                        status = atxml_WriteCmds(ResourceName, "*CLS", CLng(Len("*CLS")))
                        ExitCounterTimer()
                    Else
                        MsgBox("Error sending " & Q & Command & Q & ". Error number: " & sInstrumentStatus)
                        status = atxml_WriteCmds(ResourceName, "*CLS", CLng(Len("*CLS")))
                    End If
                End If
            End If
        End If

        'The follwing is performed following clicking "Update TIP" button
        ' for each non-query SCPI command string sent to the instrument
        If bBuildingTIPstr And Not InStr(Command, "*") And InStr(Command, "?") < 1 Then
            If sTIP_CMDstring = "" Then
                sTIP_CMDstring = ":" & Command
            Else
                sTIP_CMDstring &= ";:" & Command
            End If
        End If
    End Sub

    Sub SendStatusBarMessage(ByVal MessageString As String)
        'MessageString$ is printed in the Instrument Status Bar

        If Convert.ToString(MessageString)<>frmHPE1420B.sbrUserInformation.Panels(1 - 1).Text Then
            frmHPE1420B.sbrUserInformation.Panels(1 - 1).Text = Convert.ToString(MessageString)
            '        Debug.Print MessageString$
        End If
    End Sub

    Sub SetARMOptions()
        '************************************************************
        '* Nomenclature   : HP E1420B Universal Counter/Timer       *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     Sets ARM Subsystem Options in instrument             *
        '*     communication                                        *
        '*    EXAMPLE:                                              *
        '*     SetARMOptions                                        *
        '************************************************************

        '* Manual open and close gate function is added 10-30-2000
        '* Manual open and close gate option is enabled when "HOLD" is selected as arm source

        Dim SourceVar As String = "IMM" 'Start Source Appended SCPI Command
        '**START**
        'Start Source : Immediate
        Select Case frmHPE1420B.cboArmStartSource.Text
            Case "Immediate Trigger"
                SourceVar = "IMM"
            Case "External Trigger"
                SourceVar = "EXT"
            Case "VXI Bus Trigger"
                SourceVar = "BUS"
            Case "Hold"
                SourceVar = "HOLD"
            Case "TTL Trigger 7"
                SourceVar = "TTLT7"
            Case "TTL Trigger 6"
                SourceVar = "TTLT6"
            Case "TTL Trigger 5"
                SourceVar = "TTLT5"
            Case "TTL Trigger 4"
                SourceVar = "TTLT4"
            Case "TTL Trigger 3"
                SourceVar = "TTLT3"
            Case "TTL Trigger 2"
                SourceVar = "TTLT2"
            Case "TTL Trigger 1"
                SourceVar = "TTLT1"
            Case "TTL Trigger 0"
                SourceVar = "TTLT0"
        End Select

        strTemp = "ARM:STAR:SOUR " & SourceVar
        If strTemp <> strHolder(21) Then
            strHolder(21) = strTemp
            SendScpiCommand("ARM:STAR:SOUR " & SourceVar)
        End If

        'Manual open/close gate is added by kim 10-12-2000
        If SourceVar = "HOLD" Then
            frmHPE1420B.cmdArmStart.Visible = True
            frmHPE1420B.cmdArmStop.Visible = True

        Else
            frmHPE1420B.cmdArmStart.Visible = False
            frmHPE1420B.cmdArmStop.Visible = False

        End If

        If frmHPE1420B.fraArmStartLevel.Visible = True Then
            'External Level: 1.6V (TTL)
            If frmHPE1420B.optArmStartLevelTTL.Checked = True Then

                strTemp = "ARM:STAR:LEV MAX"
                If strTemp <> strHolder(22) Then
                    strHolder(22) = strTemp
                    SendScpiCommand("ARM:STAR:LEV MAX")
                End If
            End If
            If frmHPE1420B.optArmStartLevelECL.Checked = True Then

                strTemp = "ARM:STAR:LEV MIN"
                If strTemp <> strHolder(22) Then
                    strHolder(22) = strTemp
                    SendScpiCommand("ARM:STAR:LEV MIN")
                End If
            End If
            If frmHPE1420B.optArmStartLevelGND.Checked = True Then

                strTemp = "ARM:STAR:LEV DEF"
                If strTemp <> strHolder(22) Then
                    strHolder(22) = strTemp
                    SendScpiCommand("ARM:STAR:LEV DEF")
                End If
            End If
        End If

        'Start Slope: Negative
        If frmHPE1420B.fraCh1TriggerSlope.Visible = True Then
            If frmHPE1420B.optArmStartSlopePositive.Checked = True Then
                strTemp = "ARM:STAR:SLOP POS"
                If strTemp <> strHolder(23) Then
                    strHolder(23) = strTemp
                    SendScpiCommand("ARM:STAR:SLOP POS")
                End If
            Else

                strTemp = "ARM:STAR:SLOP NEG"
                If strTemp <> strHolder(23) Then
                    strHolder(23) = strTemp
                    SendScpiCommand("ARM:STAR:SLOP NEG")
                End If
            End If
        End If
        '**STOP**

        'Stop Source : Immediate
        Select Case frmHPE1420B.cboArmStopSource.Text
            Case "Immediate Trigger"
                SourceVar = "IMM"
            Case "External Trigger"
                SourceVar = "EXT"
            Case "VXI Bus Trigger"
                SourceVar = "BUS"
            Case "Hold"
                SourceVar = "HOLD"
            Case "TTL Trigger 7"
                SourceVar = "TTLT7"
            Case "TTL Trigger 6"
                SourceVar = "TTLT6"
            Case "TTL Trigger 5"
                SourceVar = "TTLT5"
            Case "TTL Trigger 4"
                SourceVar = "TTLT4"
            Case "TTL Trigger 3"
                SourceVar = "TTLT3"
            Case "TTL Trigger 2"
                SourceVar = "TTLT2"
            Case "TTL Trigger 1"
                SourceVar = "TTLT1"
            Case "TTL Trigger 0"
                SourceVar = "TTLT0"
        End Select

        strTemp = "ARM:STOP:SOUR " & SourceVar
        If strTemp <> strHolder(24) Then
            strHolder(24) = strTemp
            SendScpiCommand("ARM:STOP:SOUR " & SourceVar)
        End If

        If SourceVar = "HOLD" Then
            If strTemp <> strHolder(27) Then
                strHolder(27) = strTemp
                SendScpiCommand("INIT" & Channel)
            End If
        End If

        'External Level: 1.6V (TTL)
        If frmHPE1420B.fraArmStopLevel.Visible = True Then
            If frmHPE1420B.optArmStopLevelTTL.Checked = True Then


                strTemp = "ARM:STOP:LEV MAX"
                If strTemp <> strHolder(25) Then
                    strHolder(25) = strTemp
                    SendScpiCommand("ARM:STOP:LEV MAX")
                End If
            End If
            If frmHPE1420B.optArmStopLevelECL.Checked = True Then


                strTemp = "ARM:STOP:LEV MIN"
                If strTemp <> strHolder(25) Then
                    strHolder(25) = strTemp
                    SendScpiCommand("ARM:STOP:LEV MIN")
                End If
            End If
            If frmHPE1420B.optArmStopLevelGND.Checked = True Then


                strTemp = "ARM:STOP:LEV DEF"
                If strTemp <> strHolder(25) Then
                    strHolder(25) = strTemp
                    SendScpiCommand("ARM:STOP:LEV DEF")
                End If
            End If
        End If
        'Start Slope: Negative
        If frmHPE1420B.fraArmStopSlope.Visible = True Then
            If frmHPE1420B.optArmStopSlopePositive.Checked = True Then
                strTemp = "ARM:STOP:SLOP POS"
                If strTemp <> strHolder(26) Then
                    strHolder(26) = strTemp
                    SendScpiCommand("ARM:STOP:SLOP POS")
                End If
            Else
                strTemp = "ARM:STOP:SLOP NEG"
                If strTemp <> strHolder(26) Then
                    strHolder(26) = strTemp
                    SendScpiCommand("ARM:STOP:SLOP NEG")
                End If
            End If
        End If
    End Sub

    Sub SetINPutOptions()
        '************************************************************
        '* Nomenclature   : HP E1420B Universal Counter/Timer       *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     Sets INP Subsystem Options in instrument             *
        '*     communication                                        *
        '*    EXAMPLE:                                              *
        '*     SetINPutOptions                                      *
        '************************************************************

        If frmHPE1420B.optCh1Attenuation1X.Enabled = True Then
            'Attenuation:
            If frmHPE1420B.optCh1Attenuation1X.Enabled Then
                strTemp = "INP1:ATT 1" 'X1
                If strTemp <> strHolder(18) Then
                    strHolder(18) = strTemp
                    SendScpiCommand("INP1:ATT 1") 'X1
                End If
            Else
                strTemp = "INP1:ATT 10" 'X10
                If strTemp <> strHolder(18) Then
                    strHolder(18) = strTemp
                    SendScpiCommand("INP1:ATT 10") 'X10
                End If
            End If

            'Coupling:
            If frmHPE1420B.optCh1CouplingDC.Checked Then
                strTemp = "INP1:COUP DC" 'DC
                If strTemp <> strHolder(19) Then
                    strHolder(19) = strTemp
                    SendScpiCommand("INP1:COUP DC") 'DC
                End If
            Else
                strTemp = "INP1:COUP AC" 'AC
                If strTemp <> strHolder(19) Then
                    strHolder(19) = strTemp
                    SendScpiCommand("INP1:COUP AC") 'AC
                End If
            End If

            'Impedance:
            If frmHPE1420B.optCh1Impedance1MOhm.Checked Then
                strTemp = "INP1:IMP 1E6" '1M
                If strTemp <> strHolder(20) Then
                    strHolder(20) = strTemp
                    SendScpiCommand("INP1:IMP 1E6") '1M
                End If
            Else
                strTemp = "INP1:IMP 50" '50
                If strTemp <> strHolder(20) Then
                    strHolder(20) = strTemp
                    SendScpiCommand("INP1:IMP 50") '50
                End If
            End If
        End If 'frmHPE1420B.optAttenuationCh1(1).Enabled

        If frmHPE1420B.optCh2Attenuation1X.Enabled = True Then
            'Attenuation:
            If frmHPE1420B.optCh2Attenuation1X.Checked Then
                strTemp = "INP2:ATT 1" 'X1
                If strTemp <> strHolder(28) Then
                    strHolder(28) = strTemp
                    SendScpiCommand("INP2:ATT 1") 'X1
                End If
            Else
                strTemp = "INP2:ATT 10" 'X10
                If strTemp <> strHolder(28) Then
                    strHolder(28) = strTemp
                    SendScpiCommand("INP2:ATT 10") 'X10
                End If
            End If

            'Coupling:
            If frmHPE1420B.optCh2CouplingDC.Checked Then
                strTemp = "INP2:COUP DC" 'DC
                If strTemp <> strHolder(29) Then
                    strHolder(29) = strTemp
                    SendScpiCommand("INP2:COUP DC") 'DC
                End If
            Else
                strTemp = "INP2:COUP AC" 'AC
                If strTemp <> strHolder(29) Then
                    strHolder(29) = strTemp
                    SendScpiCommand("INP2:COUP AC") 'AC
                End If
            End If

            'Impedance:
            If frmHPE1420B.optCh2Impedance1MOhm.Checked Then
                strTemp = "INP2:IMP 1E6" '1M
                If strTemp <> strHolder(30) Then
                    strHolder(30) = strTemp
                    SendScpiCommand("INP2:IMP 1E6") '1M
                End If
            Else
                strTemp = "INP2:IMP 50" '50
                If strTemp <> strHolder(30) Then
                    strHolder(30) = strTemp
                    SendScpiCommand("INP2:IMP 50") '50
                End If
            End If
        End If 'frmHPE1420B.optAttenuationCh2(1).Enabled

        If frmHPE1420B.fraRouting.Visible Then 'Send only if control is used
            'Route: Separate
            If frmHPE1420B.optRoutingSeparate.Checked Then
                strTemp = "INP1:ROUT SEP" 'Separate
                If strTemp <> strHolder(31) Then
                    strHolder(31) = strTemp
                    SendScpiCommand("INP1:ROUT SEP") 'Separate
                End If
            Else
                strTemp = "INP1:ROUT COMM" 'Common
                If strTemp <> strHolder(31) Then
                    strHolder(31) = strTemp
                    SendScpiCommand("INP1:ROUT COMM") 'Common
                End If
            End If
        End If
    End Sub


    Sub SetOUTPutOptions()
        '************************************************************
        '* Nomenclature   : HP E1420B Universal Counter/Timer       *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     Sets OUTP Subsystem Options in instrument            *
        '*     communication                                        *
        '*    EXAMPLE:                                              *
        '*     SetOUTPutOptions                                     *
        '************************************************************
        Dim TRIGGER As Short 'Output Trigger Index
        Dim Send As String 'SCPI Command

        'TTLTrg<n>:State: ON/OFF
        For TRIGGER = 0 To 7
            Send = "OUTP:TTLT" & TRIGGER & ":STAT "
            If OUTPUT_TRIGGER_STATE(TRIGGER)=True Then
                Send &= "ON"
            Else
                Send &= "OFF"
            End If
            SendScpiCommand(Send)
        Next TRIGGER

        If frmHPE1420B.chkOutputTimebase.Checked = True Then
            SendScpiCommand("OUTP:ROSC:STAT ON")
        Else
            SendScpiCommand("OUTP:ROSC:STAT OFF")
        End If
    End Sub

    Sub SetSENSeOptions()
        '************************************************************
        '* Nomenclature   : HP E1420B Universal Counter/Timer       *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     Sets SENS Subsystem Options in instrument            *
        '*     communication                                        *
        '*    EXAMPLE:                                              *
        '*     SetSENSeOptions                                      *
        '************************************************************
        Dim ModeFunction As String = "FREQ" 'SCPI Appended Instrument Mode Command '
        Dim dTimeOutDuration As Double 'Current TimeOut Duration Setting
        Dim dApertureSetting As Double 'Current Aperture Setting
        Dim nSetVISA_TMO As Integer 'Value of Timeout setting in milliseconds

        'Function: Frequency
        Select Case InstrumentMode
            Case 1:
                '"Frequency"
                ModeFunction = "FREQ" 'Function
            Case 2:
                '"Period"
                ModeFunction = "PER" 'Function
            Case 14:
                '"DcVoltage"
                ModeFunction = "DC" 'Function
            Case 11:
                '"AcVoltage"
                ModeFunction = "AC" 'Function
            Case 4:
                '"RiseTime"
                ModeFunction = "RTIM" 'Function
            Case 5:
                '"FallTime"
                ModeFunction = "FTIM" 'Function
            Case 7:
                '"PositivePulseWidth"
                ModeFunction = "PWID" 'Function
            Case 8:
                '"NegativePulseWidth"
                ModeFunction = "NWID" 'Function
            Case 6:
                '"TimeInterval"
                ModeFunction = "TINT" 'Function
            Case 9:
                '"Totalize"
                ModeFunction = "TOT" 'Function
            Case 12:
                '"MinimumVoltage"
                ModeFunction = "MIN" 'Function
            Case 13:
                '"MaximumVoltage"
                ModeFunction = "MAX" 'Function
            Case 3:
                '"FrequencyRatio"
                ModeFunction = "FREQ:RAT" 'Function
            Case 10:
                'Phase"
                ModeFunction = "PHAS" 'Function
        End Select

        '    strTemp = "SENS" & Channel% & ":FUNC:" & ModeFunction$
        '    If strTemp <> strHolder(1) Then
        '        strHolder(1) = strTemp
        '        SendScpiCommand "SENS" & Channel% & ":FUNC:" & ModeFunction$
        '    End If
        strTemp = "SENS" & Channel & ":FUNC " & Q & ModeFunction & Q
        If strTemp<>strHolder(1) Then
            strHolder(1) = strTemp
            SendScpiCommand("SENS" & Channel & ":FUNC " & Q & ModeFunction & Q)
        End If

        'Average State: OFF
        If frmHPE1420B.optTiGateAvgOn.Checked = True Then
            strTemp = "SENS" & Channel & ":AVER:STAT ON"
            If strTemp <> strHolder(2) Then
                strHolder(2) = strTemp
                SendScpiCommand("SENS" & Channel & ":AVER:STAT ON")
            End If
        Else
            strTemp = "SENS" & Channel & ":AVER:STAT OFF"
            If strTemp <> strHolder(2) Then
                strHolder(2) = strTemp
                SendScpiCommand("SENS" & Channel & ":AVER:STAT OFF")
            End If
        End If

        strTemp = "SENS:ATIM:TIME " & SetCur(DUR_TIMEOUT)
        'Convert Timeout settings to milliseconds
        dApertureSetting = CDbl(SetCur(APERTURE))*1000
        dTimeOutDuration = CDbl(SetCur(DUR_TIMEOUT))*1000
        'Find the greatest Timeout value
        If dApertureSetting>dTimeOutDuration Then
            nSetVISA_TMO = CLng(dApertureSetting)
        Else
            nSetVISA_TMO = CLng(dTimeOutDuration)
        End If
        If frmHPE1420B.optAcquisitionTimeoutOff.Checked = True Then

        End If
        If strTemp<>strHolder(3) Then
            strHolder(3) = strTemp
            SendScpiCommand(strHolder(3))
        End If

        'Acquisition TMO set to "ON"
        'The measurement Aborts if an acquisition (measured from the point at which
        'the acquisition is enabled) has not completed within the number of seconds specified.
        If frmHPE1420B.optAcquisitionTimeoutOn.Checked = True Then
            strTemp = "SENS:ATIM:CHEC ON"
            If strTemp <> strHolder(4) Then
                strHolder(4) = strTemp
                SendScpiCommand("SENS:ATIM:CHEC ON")
            End If
        Else
            'Acquisition TMO set to "OFF", No Time Acquisition
            If frmHPE1420B.optAcquisitionTimeoutOff.Checked = True Then

                'If Acquisition TMO is "OFF" then use Aperature Setting to determine VISA TMO
                nSetVISA_TMO = CLng(dApertureSetting)
                If nSetVISA_TMO < 4000 Then 'If TMO is less than 4 Seconds then set to 4 Sec
                    nSetVISA_TMO = 4000 'This will provide a minimum VISA TMO of 5 Seconds
                End If

                strTemp = "SENS:ATIM:CHEC OFF"
                If strTemp <> strHolder(4) Then
                    strHolder(4) = strTemp
                    SendScpiCommand("SENS:ATIM:CHEC OFF")
                End If

            Else
                'Acquisition TMO set to "START"
                'The measurement Aborts if the start arm and start trigger events
                'have not been detected within the number of seconds specified.
                strTemp = "SENS:ATIM:CHEC STAR"
                If strTemp <> strHolder(4) Then
                    strHolder(4) = strTemp
                    SendScpiCommand("SENS:ATIM:CHEC STAR")
                End If
            End If
        End If

        'Set VISA Time Out...
        If LiveMode Then
            'Set VISA Timeout to 1 second over the greatest TMO value
            'ErrorStatus& = viSetAttribute(instrumentHandle&, VI_ATTR_TMO_VALUE, nSetVISA_TMO + 1000)
        End If

        If InstrumentMode<>4 And InstrumentMode<>5 Then
            'Rise and Fall Time Will Be affected by setting Trigger
            'Auto Trigger State: OFF
            If frmHPE1420B.chkCh1AbsTrigAuto.Checked = True Then
                strTemp = "SENS" & Channel & ":EVEN:LEV:ABS:AUTO ON"
                If strTemp <> strHolder(5) Then
                    strHolder(5) = strTemp
                    SendScpiCommand("SENS" & Channel & ":EVEN:LEV:ABS:AUTO ON")
                End If

                'Relative Trigger Level: 50%
                strTemp = "SENS" & Channel & ":EVEN:LEV:REL " & SetCur(REL_TRIGGER)
                If strTemp <> strHolder(6) Then
                    strHolder(6) = strTemp
                    SendScpiCommand("SENS" & Channel & ":EVEN:LEV:REL " & SetCur(REL_TRIGGER))
                End If
            Else
                strTemp = "SENS" & Channel & ":EVEN:LEV:ABS " & SetCur(ABS_TRIGGER)
                If strTemp <> strHolder(5) Then
                    strHolder(5) = strTemp
                    SendScpiCommand("SENS" & Channel & ":EVEN:LEV:ABS " & SetCur(ABS_TRIGGER))
                End If

                strTemp = "SENS" & Channel & ":EVEN:LEV:ABS:AUTO OFF"
                If strTemp <> strHolder(6) Then
                    strHolder(6) = strTemp
                    SendScpiCommand("SENS" & Channel & ":EVEN:LEV:ABS:AUTO OFF")
                End If
            End If

            '**************************************************************
            'Added D.Masters 03/26/2009
            'Channel 2 is used for TI measurement mode
            If InstrumentMode=6 Then
                If frmHPE1420B.chkCh2AbsTrigAuto.Checked = True Then
                    strTemp = "SENS2:EVEN:LEV:ABS:AUTO ON"
                    If strTemp <> strHolder(32) Then
                        strHolder(32) = strTemp
                        SendScpiCommand(strTemp)
                    End If

                    strTemp = "SENS2:EVEN:LEV:REL " & SetCur(REL_TRIG_CHAN2)
                    If strTemp <> strHolder(33) Then
                        strHolder(33) = strTemp
                        SendScpiCommand(strTemp)
                    End If
                Else
                    strTemp = "SENS2:EVEN:LEV:ABS " & SetCur(ABS_TRIG_CHAN2)
                    If strTemp <> strHolder(32) Then
                        strHolder(32) = strTemp
                        SendScpiCommand(strTemp)
                    End If

                    strTemp = "SENS2:EVEN:LEV:ABS:AUTO OFF"
                    If strTemp <> strHolder(33) Then
                        strHolder(33) = strTemp
                        SendScpiCommand(strTemp)
                    End If
                End If
            End If
            'end Added D.Masters 03/26/2009
            '******************************
        Else
            If frmHPE1420B.chkCh1AbsTrigAuto.Checked = False Then
                strTemp = "SENS" & Channel & ":EVEN:LEV:ABS:AUTO ON"
                If strTemp <> strHolder(5) Then
                    strHolder(5) = strTemp
                    SendScpiCommand("SENS" & Channel & ":EVEN:LEV:ABS:AUTO ON")
                End If

                strTemp = "SENS" & Channel & ":EVEN:LEV:REL 10"
                If strTemp <> strHolder(6) Then
                    strHolder(6) = strTemp
                    SendScpiCommand("SENS" & Channel & ":EVEN:LEV:REL 10")
                End If

                '******************************
                'special for TI mode, channel2 programmable.  Added D.Masters 3/26/2009
                If InstrumentMode = 6 Then
                    strTemp = "SENS2:EVEN:LEV:ABS:AUTO ON"
                    If strTemp <> strHolder(32) Then
                        strHolder(32) = strTemp
                        SendScpiCommand(strTemp)
                    End If

                    strTemp = "SENS2:EVEN:LEV:REL 10"
                    If strTemp <> strHolder(33) Then
                        strHolder(33) = strTemp
                        SendScpiCommand(strTemp)
                    End If
                End If
                'end Added D.Masters 03/26/2009
                '******************************
            End If
        End If

        'Event Slope: Positive
        If frmHPE1420B.optCh1TriggerSlopePositive.Checked = True Then
            strTemp = "SENS" & Channel & ":EVEN:SLOP POS"

            If strTemp <> strHolder(7) Then
                strHolder(7) = strTemp
                SendScpiCommand("SENS" & Channel & ":EVEN:SLOP POS")
            End If
        Else
            strTemp = "SENS" & Channel & ":EVEN:SLOP NEG"

            If strTemp <> strHolder(7) Then
                strHolder(7) = strTemp
                SendScpiCommand("SENS" & Channel & ":EVEN:SLOP NEG")
            End If
        End If

        'Hysteresis: Default
        If frmHPE1420B.optCh1HysteresisMin.Checked = True Then
            strTemp = "SENS" & Channel & ":EVEN:HYST MIN"
            If strTemp <> strHolder(8) Then
                strHolder(8) = strTemp
                SendScpiCommand("SENS" & Channel & ":EVEN:HYST MIN")
            End If
        End If

        If frmHPE1420B.optCh1HysteresisDef.Checked = True Then
            strTemp = "SENS" & Channel & ":EVEN:HYST DEF"
            If strTemp <> strHolder(8) Then
                strHolder(8) = strTemp
                SendScpiCommand("SENS" & Channel & ":EVEN:HYST DEF")
            End If
        End If

        If frmHPE1420B.optCh1HysteresisMax.Checked = True Then
            strTemp = "SENS" & Channel & ":EVEN:HYST MAX"
            If strTemp <> strHolder(8) Then
                strHolder(8) = strTemp
                SendScpiCommand("SENS" & Channel & ":EVEN:HYST MAX")
            End If
        End If

        'Added D.Masters 03/26/2009
        'For TI mode, we must program channel 2 hysteresis
        If InstrumentMode=6 Then
            If frmHPE1420B.optCh2TriggerSlopePositive.Checked = True Then
                strTemp = "SENS2:EVEN:SLOP POS"
                If strTemp <> strHolder(35) Then
                    strHolder(35) = strTemp
                    SendScpiCommand(strTemp)
                End If
            Else
                strTemp = "SENS2:EVEN:SLOP NEG"
                If strTemp <> strHolder(35) Then
                    strHolder(35) = strTemp
                    SendScpiCommand(strTemp)
                End If
            End If

            If frmHPE1420B.optCh2HysteresisMin.Checked = True Then
                strTemp = "SENS2:EVEN:HYST MIN"
                If strTemp <> strHolder(36) Then
                    strHolder(36) = strTemp
                    SendScpiCommand(strTemp)
                End If
            End If

            If frmHPE1420B.optCh2HysteresisDef.Checked = True Then
                strTemp = "SENS2:EVEN:HYST DEF"
                If strTemp <> strHolder(36) Then
                    strHolder(36) = strTemp
                    SendScpiCommand(strTemp)
                End If
            End If

            If frmHPE1420B.optCh2HysteresisMax.Checked = True Then
                strTemp = "SENS2:EVEN:HYST MAX"
                If strTemp <> strHolder(36) Then
                    strHolder(36) = strTemp
                    SendScpiCommand(strTemp)
                End If
            End If
        End If

        If InstrumentMode=1 Then
            'Freq Aperature: Min: 100ms   Def: 100mS    Max:99.999S
            strTemp = "SENS" & Channel & ":FREQ:APER " & SetCur(APERTURE)
            If strTemp<>strHolder(9) Then
                strHolder(9) = strTemp
                SendScpiCommand("SENS" & Channel & ":FREQ:APER " & SetCur(APERTURE))
            End If
        End If

        If InstrumentMode=2 Then
            'Per Aperature:
            strTemp = "SENS" & Channel & ":PER:APER " & SetCur(APERTURE)
            If strTemp<>strHolder(10) Then
                strHolder(10) = strTemp
                SendScpiCommand("SENS" & Channel & ":PER:APER " & SetCur(APERTURE))
            End If
        End If

        If InstrumentMode=3 Then
            'Ratio Aperature:
            strTemp = "SENS" & Channel & ":RAT:APER " & SetCur(APERTURE)
            If strTemp<>strHolder(11) Then
                strHolder(11) = strTemp
                SendScpiCommand("SENS" & Channel & ":RAT:APER " & SetCur(APERTURE))
            End If
        End If

        If InstrumentMode=1 Or InstrumentMode=2 Or InstrumentMode=3 Then
            'Range: Auto
            If frmHPE1420B.chkRange.Checked = True Then
                strTemp = "SENS" & Channel & ":FREQ:RANG:AUTO ON"
                If strTemp <> strHolder(12) Then
                    strHolder(12) = strTemp
                    SendScpiCommand("SENS" & Channel & ":FREQ:RANG:AUTO ON")
                End If
            Else

                strTemp = "SENS" & Channel & ":FREQ:RANG:AUTO OFF"
                If strTemp <> strHolder(12) Then
                    strHolder(12) = strTemp
                    SendScpiCommand("SENS" & Channel & ":FREQ:RANG:AUTO OFF")
                End If
            End If
        End If

        'ROSCillator Source: CLK10
        If frmHPE1420B.optTimebaseSourceVXIClk.Checked = True Then
            strTemp = "SENS" & Channel & ":ROSC:SOUR CLK10"
            If strTemp <> strHolder(13) Then
                strHolder(13) = strTemp
                SendScpiCommand("SENS" & Channel & ":ROSC:SOUR CLK10")
            End If
        End If

        If frmHPE1420B.optTimebaseSourceIntern.Checked = True Then
            strTemp = "SENS" & Channel & ":ROSC:SOUR INT"
            If strTemp <> strHolder(13) Then
                strHolder(13) = strTemp
                SendScpiCommand("SENS" & Channel & ":ROSC:SOUR INT")
            End If
        End If

        If frmHPE1420B.optTimebaseSourceExtern.Checked = True Then
            strTemp = "SENS" & Channel & ":ROSC:SOUR EXT"
            If strTemp <> strHolder(13) Then
                strHolder(13) = strTemp
                SendScpiCommand("SENS" & Channel & ":ROSC:SOUR EXT")
            End If
        End If

        'Do only if in TIME-INTERVAL mode
        If InstrumentMode=6 Then
            'TINT Delay State: OFF
            If frmHPE1420B.chkTIDelay.Checked = True Then
                strTemp = "SENS" & Channel & ":TINT:DEL:STAT ON"
                If strTemp <> strHolder(14) Then
                    strHolder(14) = strTemp
                    SendScpiCommand("SENS" & Channel & ":TINT:DEL:STAT ON")
                End If
            Else
                strTemp = "SENS" & Channel & ":TINT:DEL:STAT OFF"
                If strTemp <> strHolder(14) Then
                    strHolder(14) = strTemp
                    SendScpiCommand("SENS" & Channel & ":TINT:DEL:STAT OFF")
                End If
            End If

            strTemp = "SENS" & Channel & ":TINT:DEL:TIME " & SetCur(TI_DELAY)
            If strTemp<>strHolder(15) Then
                strHolder(15) = strTemp
                SendScpiCommand("SENS" & Channel & ":TINT:DEL:TIME " & SetCur(TI_DELAY))
            End If
        End If 'IF InstrumentMode% = 6

        'Do only if in TOTALIZE mode
        If InstrumentMode=9 Then
            If frmHPE1420B.chkTotalizeGateState.Checked = True Then
                strTemp = "SENS" & Channel & ":TOT:GATE:STAT ON"
                If strTemp <> strHolder(16) Then
                    strHolder(16) = strTemp
                    SendScpiCommand("SENS" & Channel & ":TOT:GATE:STAT ON")
                End If
            Else
                strTemp = "SENS" & Channel & ":TOT:GATE:STAT OFF"
                If strTemp <> strHolder(16) Then
                    strHolder(16) = strTemp
                    SendScpiCommand("SENS" & Channel & ":TOT:GATE:STAT OFF")
                End If
            End If

            'Totalize Gate Polarity: Normal
            If frmHPE1420B.optTotalizeGatePolarityNormal.Checked = True Then
                strTemp = "SENS" & Channel & ":TOT:GATE:POL NORM"
                If strTemp <> strHolder(17) Then
                    strHolder(17) = strTemp
                    SendScpiCommand("SENS" & Channel & ":TOT:GATE:POL NORM")
                End If
            End If

            If frmHPE1420B.optTotalizeGatePolarityInvert.Checked = True Then
                strTemp = "SENS" & Channel & ":TOT:GATE:POL INV"
                If strTemp <> strHolder(17) Then
                    strHolder(17) = strTemp
                    SendScpiCommand("SENS" & Channel & ":TOT:GATE:POL INV")
                End If
            End If
        End If 'IF InstrumentMode% = 9
    End Sub

    Function StripNullCharacters(ByVal Parsed As String) As String
        StripNullCharacters = ""
        '************************************************************
        '* ManTech Test Systems Software Sub                        *
        '************************************************************
        '* Nomenclature   : HP E1420B Universal Counter/Timer       *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     Removes ASCII characters under 32 in value           *
        '*    PARAMETERS:                                           *
        '*     Parsed$ = String to be parsed                        *
        '*    RETURNS:                                              *
        '*     Parsed String                                        *
        '*    EXAMPLE:                                              *
        '*      Fixed$ = StripNullCharacters(StringWithNull$)       *
        '************************************************************
        Dim X As Integer

        For X = 1 To Parsed.Length '32 - 126
            If Asc(Mid(Parsed, X, 1)) < 32 Then
                Exit For
            End If
        Next X

        'Return Function Value
        StripNullCharacters = Strings.Left(Parsed, X - 1)
    End Function

    Sub TakeMeasurement()
        '************************************************************
        '* Nomenclature   : HP E1420B Universal Counter/Timer       *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     This Module Takes A Measurement From the Counter     *
        '*    EXAMPLE:                                              *
        '*     TakeMeasurement                                      *
        '************************************************************
        Dim ReadBuffer As String
        Dim TimeError As String
        Dim ErrorText As String

        If frmHPE1420B.chkContinuous.Checked = True Then
            frmHPE1420B.cmdMeasure.Enabled = False
        End If

        If sTIP_Mode="TIP_RUNPERSIST" Or sTIP_Mode="TIP_RUNSETUP" Then
            'Program Instrument per saved TIP settings settings
            SendScpiCommand(sTIP_CMDstring)
            If  Not bGetInstrumentStatus() Then ExitCounterTimer()
        Else
            If frmHPE1420B.cmdArmStop.Visible = False Then
                'Program Instrument
                ResetHolders() 'Reset Command Holders
                SetINPutOptions()
                SetARMOptions()
                SetOUTPutOptions()
                SetCONFigureOptions()
                SetSENSeOptions()
            End If
        End If

        'Format The Display Units According To Instrument Mode
        Select Case InstrumentMode
            Case 1:
                '"Frequency",
                DisplayUnit = "Hz"
            Case 2, 4, 5, 7, 8, 6:
                '"Period", "RiseTime", "FallTime", "+PulseWidth", "-PulseWidth", "TimeInterval"
                DisplayUnit = "Sec"
            Case 11, 12, 13, 14:
                '"AcVoltage", "MinimumVoltage", "MaximumVoltage", "DcVoltage"
                DisplayUnit = "Volts"
            Case 9:
                '"Totalize"
                DisplayUnit = "Events"
            Case 10:
                '"Phase"
                DisplayUnit = "Deg"
            Case 3:
                '"FrequencyRatio"

                If Channel=1 Then
                    DisplayUnit = "In1:In2" 'Change from "CH#" to "In#"
                Else
                    DisplayUnit = "In2:In1"
                End If
        End Select

        'Continuous Operation Loop
        frmHPE1420B.txtCtDisplay.Text = "Reading..."
        do
            If bChangeMode Then Exit Do 'Jump out of loop if Mode is changed
            bolMeasure = True
            If frmHPE1420B.cmdArmStop.Visible = False Then
                Delay(0.05) 'Delay to keep from receiving errors
                'mh SendScpiCommand "ABOR"
                SendScpiCommand("*CLS") 'Clear any stale error states prior to measurement
                SendScpiCommand("ABOR" & Channel) 'mh if no channel is given it always assume ch.1
                SendScpiCommand("*WAI")
            End If

            Select Case InstrumentMode
                Case 2:
                    SendScpiCommand("MEAS" & Channel & ":PER?")
                Case 4:
                    SendScpiCommand("MEAS" & Channel & ":RTIM?")
                Case 5:
                    SendScpiCommand("MEAS" & Channel & ":FTIM?")
                Case 10:
                    SendScpiCommand("MEAS" & Channel & ":PHAS?")
                Case 9:
                    If frmHPE1420B.cmdArmStop.Visible = True Then
                        SendScpiCommand("FETC" & Channel & "?")
                    Else
                        SendScpiCommand("INIT" & Channel)
                        SendScpiCommand("FETC" & Channel & "?")
                    End If

                Case Else
                    SendScpiCommand("INIT" & Channel)
                    SendScpiCommand("FETC" & Channel & "?")
            End Select
            If bolErrorReset Then
                Exit Sub
            End If
            ReadBuffer = ReadInstrumentBuffer()
            'Format And Display Measurement
            If ReadBuffer<>"" Then
                If Asc(ReadBuffer)=255 Then
                    frmHPE1420B.chkContinuous.Checked = False
                    Exit Sub
                End If
            End If
            If Len(ReadBuffer)=0 Then
                ReadBuffer = "0.0"
            End If

            If ScreenRst=False Then
                If ReadBuffer="" Or Val(ReadBuffer)=9.91E+37 Or ReadBuffer=Convert.ToString(Chr(255)) Then
                    do
                        SendScpiCommand("SYST:ERR?")
                        TimeError = ReadInstrumentBuffer()
                        'Second condition added to break infinite loop
                    Loop Until TimeError="" Or (Val(TimeError)=0)
                    ErrorText = "The Counter/Timer Has Timed Out or there is no signal sent at the selected input."
                    MsgBox(ErrorText)
                    frmHPE1420B.txtCtDisplay.Font = New Font("MS Sans Serif", 18) 'Change Font size to 18 point
                    frmHPE1420B.txtCtDisplay.Text = "Ready"
                    frmHPE1420B.chkContinuous.Checked = False
                    SendStatusBarMessage("Counter/Timer Ready For Measurement")
                    frmHPE1420B.tabUserOptions.SelectedIndex = 0
                    Exit Do
                End If
            Else                'If Reset is selected, Exit
                If bolErrorReset Then
                    Exit Sub 'Exit Sub if Resetting
                End If

                ScreenRst = False
                Exit Do
            End If
            frmHPE1420B.txtCtDisplay.Font = New Font("MS Sans Serif", 18) 'Change Font size to 18 point
            If bChangeMode Then Exit Do 'Jump out of loop if Mode is changed

            Select Case InstrumentMode
                Case 9:
                    frmHPE1420B.txtCtDisplay.Font = New Font("MS Sans Serif", 19) 'Change Font size to 18 point
                    If Len(Convert.ToDouble(ReadBuffer).ToString("0.000 E+00") & DisplayUnit) > 18 Then
                        frmHPE1420B.txtCtDisplay.Font = New Font("MS Sans Serif", 14) 'Change Font size to 14 point
                    End If
                    frmHPE1420B.txtCtDisplay.Text = Convert.ToDouble(ReadBuffer).ToString("0.000 E+00 ") & DisplayUnit
                Case 3:
                    If Len(Convert.ToDouble(ReadBuffer).ToString("###0.0000000000000 ") & DisplayUnit) > 19 Then
                        frmHPE1420B.txtCtDisplay.Font = New Font("MS Sans Serif", 14) 'Change Font size to 14 point
                    End If
                    frmHPE1420B.txtCtDisplay.Text = Convert.ToDouble(ReadBuffer).ToString("###0.0000000000000 ") & DisplayUnit

                Case Else
                    If Len(EngNotate((ReadBuffer), 10, DisplayUnit))>19 Then
                        frmHPE1420B.txtCtDisplay.Font = New Font("MS Sans Serif", 14) 'Change Font size to 14 point
                    End If
                    frmHPE1420B.txtCtDisplay.Text = EngNotate((ReadBuffer), 10, DisplayUnit)
            End Select
            sTIP_Measured = Convert.ToDouble(ReadBuffer).ToString("0.#########") 'Absolute value is passed to TIP Studio
            Application.DoEvents()
        Loop Until (frmHPE1420B.chkContinuous.Checked = False)

        bolErrorReset = False
        bolMeasure = False
    End Sub

    Function VerifyInstrumentCommunication() As Boolean
        '************************************************************
        '* ManTech Test Systems Software Module                     *
        '************************************************************
        '* Nomenclature   : HP E1212A Digital Multimeter Front Panel*
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     Verifies Communication With Message Delivery System  *
        '*    EXAMPLE:                                              *
        '*      LiveMode% = VerifyInstrumentCommunication%          *
        '************************************************************
        Dim Pass As Boolean 'Instrument Pass Test (True/False)
        Dim SystemDir As String
        Dim status As Integer
        Dim XmlBuf As String
        Dim Response As String

        Dim Allocation As String
        ' initialize resource name
        ResourceName = "CNTR_1"

        XmlBuf = Space(4096)
        Response = Space(4096)

        Pass = False 'Until Proven True
        ErrorStatus = 1

        SystemDir = Environment.SystemDirectory
        'Determine If The Instrument Is Functioning
        '        ErrorStatus& = hpe1420b_init(resourceName:="VXI::18", IDQuery:=1, resetDevice:=1, instrumentHandle:=instrumentHandle&)
        Try ' On Error GoTo ErrHandle
            status = atxml_Initialize(proctype, guid)

            '        XmlBuf = "<AtXmlTestRequirements>" &
            ''                 "    <ResourceRequirement>" &
            ''                 "        <SignalResourceName>CNTR_1</SignalResourceName> " &
            ''                 "    </ResourceRequirement> " &
            ''                 "</AtXmlTestRequirements>" '
            '
            XmlBuf = "<AtXmlTestRequirements>" & _
                     "    <ResourceRequirement>" & _
                     "        <ResourceType>Source</ResourceType>" & _
                     "        <SignalResourceName>CNTR_1</SignalResourceName> " & _
                     "    </ResourceRequirement> " & _
                     "</AtXmlTestRequirements>"            '


            Allocation = Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\ATS\", "PAWSAllocationPath", Nothing)
            '
            '

            '     ErrorStatus& = atxml_ValidateRequirements(XmlBuf, Allocation, wmt_Response, 4096)
            status = atxml_ValidateRequirements(XmlBuf, Allocation, Response, 4096)

            'Parse Availability XML string to get the status(Mode) of the instrument
            iStatus = gctlParse.ParseAvailiability(Response)

            If status <> 0 Then
                MsgBox("The Counter/Timer Is Not Responding.  Live Mode Disabled.", MsgBoxStyle.Information)
            Else
                Pass = True
            End If

            VerifyInstrumentCommunication = Pass
        Catch   ' ErrHandle:
            If Err.Number = 53 Then
                MsgBox("unable to locate AtXmlApi.dll", MsgBoxStyle.Critical)
            ElseIf Err.Number = 49 Then
                Debug.Print("Description :" & vbTab & Err.Description)
                Debug.Print("Number:" & vbTab & Str(Err.Number))
            Else
                Debug.Print("Description :" & vbTab & Err.Description)
                Debug.Print("Number:" & vbTab & Str(Err.Number))
            End If

            VerifyInstrumentCommunication = False
        End Try
    End Function

    Sub InitializeArrays()
        '************************************************************
        '* Nomenclature   : HP E1420B Universal Counter/Timer       *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     This Module Generates The Arrays and Character       *
        '*     constants used in the SAIS Front Panel.              *
        '*    EXAMPLE:                                              *
        '*     InitializeArrays                                     *
        '************************************************************
        'C/T VISA Library Resource Name
        ResourceName = "CNTR_1" ' "VXI::136"
        Q = Convert.ToString(Chr(34))

        Channel = 1
        InstrumentMode = 1
        'Trigger Options Combo Box
        InitializeTriggerSource()
    End Sub

    'Procedure to Reset all Command Holders
    'DJoiner 11/27/00
    Sub ResetHolders()
        Dim i As Short 'Set all command string Holders to an empty string

        For i = 1 To MAX_HOLDERS
            strHolder(i) = ""
        Next
    End Sub

    'Check for all prerequisite conditions are met to make Manual Gating avalible
    'Dave Joiner 11/27/00
    Sub CheckForManGate()
        If frmHPE1420B.tbrCtFunctions.Buttons(9 - 1).Pushed = True Then 'If "TZ" function is selected
            strHold = "Hold" 'Make "Hold" available
            InitializeTriggerSource()
            FillArmSourceComboBox(TRIGGER_SOURCE_VALUE) 'Fill comboboxes including "Hold"
        End If
        'If a Mode other than "TZ" is selected and "Hold" is available, make it "Not Available"
        If frmHPE1420B.tbrCtFunctions.Buttons(9 - 1).Pushed<>True And strHold="Hold" Or bolInitGUI Then
            strHold = "Not Available"
            InitializeTriggerSource()
            FillArmSourceComboBox(TRIGGER_SOURCE_VALUE) 'Fill comboboxes including "Hold"
            frmHPE1420B.txtTiDelay.Text = 5 'Set Acquisition Timeout Settings to default
            frmHPE1420B.optAcquisitionTimeoutOn.Checked = True
            frmHPE1420B.txtTimeOut_Leave(New Object(), New EventArgs())
        End If
    End Sub

    'Disable controls to prevent User from changing Parameters while taking a measurement
    'Dave Joiner 11/30/00
    Sub DisableControls()
        Dim intTabIndex As Short
        intTabIndex = 1 'Disable controls while a measurement is being taken

        frmHPE1420B.fraChannel.Enabled = False
        frmHPE1420B.fraCh1Range.Enabled = False
        frmHPE1420B.fraAperture.Enabled = False

        For intTabIndex = 1 To 4
            frmHPE1420B.tabUserOptions.TabPages.Item(intTabIndex).Enabled = False
        Next

        'If taking a Manual Gate Measurement, Disable Arm Source Controls
        If bolArm Then
            frmHPE1420B.fraArmStartSource.Enabled = False
            frmHPE1420B.fraArmStopSource.Enabled = False
            frmHPE1420B.tabUserOptions.TabPages.Item(3).Enabled = True 'Enable Arm tab to allow User to Close Gate
        End If

        frmHPE1420B.tbrCtFunctions.Enabled = False 'Prevent User from changing Functions when measuring
    End Sub

    'Enable controls after measurement is complete to allow User to set up Parameters for next Measurement
    'Dave Joiner 11/30/00
    Sub EnableControls()
        Dim intTabIndex As Short
        intTabIndex = 1 'If Manual Gate Measurement is complete, Enable Controls

        If bolArm=False Then
            frmHPE1420B.fraChannel.Enabled = True
            frmHPE1420B.fraCh1Range.Enabled = True
            frmHPE1420B.fraAperture.Enabled = True
            frmHPE1420B.fraArmStartSource.Enabled = True
            frmHPE1420B.fraArmStopSource.Enabled = True

            If frmHPE1420B.cmdArmStart.Enabled = False Then
                frmHPE1420B.tbrCtFunctions.Enabled = False 'Prevent User from changing Functions while Manual Gate is "Open"
            Else
                frmHPE1420B.tbrCtFunctions.Enabled = True 'Allow User to change Functions
            End If
            For intTabIndex = 1 To 4 ' Restore User controls
                frmHPE1420B.tabUserOptions.TabPages.Item(intTabIndex).Enabled = True
            Next
        End If
    End Sub

    'Set values for Trigger Source Combo Box
    'Dave Joiner 12/04/00
    Sub InitializeTriggerSource()

        'Trigger Options Combo Box
        TRIGGER_SOURCE_VALUE(0) = "Immediate Trigger"
        TRIGGER_SOURCE_VALUE(1) = "External Trigger"
        TRIGGER_SOURCE_VALUE(2) = "VXI Bus Trigger"
        TRIGGER_SOURCE_VALUE(3) = strHold
        TRIGGER_SOURCE_VALUE(4) = "TTL Trigger 7"
        TRIGGER_SOURCE_VALUE(5) = "TTL Trigger 6"
        TRIGGER_SOURCE_VALUE(6) = "TTL Trigger 5"
        TRIGGER_SOURCE_VALUE(7) = "TTL Trigger 4"
        TRIGGER_SOURCE_VALUE(8) = "TTL Trigger 3"
        TRIGGER_SOURCE_VALUE(9) = "TTL Trigger 2"
        TRIGGER_SOURCE_VALUE(10) = "TTL Trigger 1"
        TRIGGER_SOURCE_VALUE(11) = "TTL Trigger 0"

    End Sub

    'Disable Channel 2 Input Controls on Mearurement Tab and Input Tab
    'Dave Joiner 01/04/01
    Sub DisableCh2()
        bDualChannel = False
        'Disable Channel 2 Input Controls on Mearurement Tab and Set (Ch1) to Default
        frmHPE1420B.optInputChannel1.Enabled = True
        frmHPE1420B.optInputChannel2.Enabled = False
        frmHPE1420B.optInputChannel1.Checked = True
        frmHPE1420B.optInputChannel2.Checked = False
        'Set to Default values
        frmHPE1420B.optCh2Attenuation1X.Checked = True
        frmHPE1420B.optCh2CouplingDC.Checked = True
        frmHPE1420B.optCh2Impedance1MOhm.Checked = True
        'Disable Channel 2 Controls on the Input Tab
        frmHPE1420B.optCh2Attenuation1X.Enabled = False
        frmHPE1420B.optCh2CouplingDC.Enabled = False
        frmHPE1420B.optCh2Impedance1MOhm.Enabled = False
        frmHPE1420B.optCh2Attenuation10X.Enabled = False
        frmHPE1420B.optCh2CouplingAC.Enabled = False
        frmHPE1420B.optCh2Impedance50Ohm.Enabled = False
    End Sub

    'Disable optgateaverage mode
    'Dave Joiner 01/04/01
    Sub DisableGateAverage()
        frmHPE1420B.optTiGateAvgOn.Enabled = False
        frmHPE1420B.optTiGateAvgOff.Enabled = True
        frmHPE1420B.optTiGateAvgOn.Checked = False
        frmHPE1420B.optTiGateAvgOff.Checked = True
    End Sub

    'Enable GateAverage Mode
    'Dave Joiner 01/04/01
    Sub EnableGateAverage()
        frmHPE1420B.optTiGateAvgOn.Enabled = True
        frmHPE1420B.optTiGateAvgOff.Enabled = True
        frmHPE1420B.optTiGateAvgOn.Checked = True
        frmHPE1420B.optTiGateAvgOff.Checked = False
    End Sub

    'Enable Channel 2 Controls on the Input Tab
    'Dave Joiner 01/04/01
    Sub EnableCh2()
        frmHPE1420B.optCh2Attenuation1X.Enabled = True
        frmHPE1420B.optCh2CouplingDC.Enabled = True
        frmHPE1420B.optCh2Impedance1MOhm.Enabled = True
        frmHPE1420B.optCh2Attenuation10X.Enabled = True
        frmHPE1420B.optCh2CouplingAC.Enabled = True
        frmHPE1420B.optCh2Impedance50Ohm.Enabled = True
        bDualChannel = True
    End Sub

    Public Function StringToList(ByVal sStr As String, ByVal iLower As Short, ByRef List() As String, ByVal sDelimiter As String) As Short
        'DESCRIPTION:
        '   Procedure to convert a delimited string into a dynamic string array
        '   ReDims the array from iLower to the number of elements in string
        'Parameters:
        '   sStr       : String to be parsed.
        '   iLower     : Lower bound of target array
        '   List$()    : Dynamic array in which to return list of strings
        '   sDelimiter : Delimiter string.
        'Returns:
        '   Number of items in string
        '   or 0 if string is empty

        Dim numels As Short, i As Integer
        Dim iDelimiterLength As Integer

        iDelimiterLength = Len(sDelimiter)
        If sStr = "" Then
            StringToList = 0
            Exit Function
        End If

        numels = 1
        ReDim List(iLower)
        'Go through parsed string a character at a time.
        For i = 1 To Len(sStr)
            'Test for delimiter
            If Mid(sStr, i, iDelimiterLength) <> sDelimiter Then
                'Add the character to the current argument.
                List(iLower + numels - 1) &= Mid(sStr, i, 1)
            Else
                'Found a delimiter.
                ReDim Preserve List(iLower + numels)
                numels += 1
                i += iDelimiterLength - 1
            End If
        Next i
        StringToList = numels
    End Function

    Sub SetKey(ByRef sSection As String, ByVal sKey As String, ByVal sKeyVal As String)
        'DESCRIPTION:
        '   This function sets a key value in VIPERT.INI
        'PARAMETERS:
        '   sSection    The VIPERT.INI file section where the key is located.
        '   sKey        The Key name.
        '   sKeyVal     The string to set the key value to.
        'GLOBAL VARIABLES USED:
        '   sWindowsDir
        'EXAMPLE:
        '   SetKey "TIPS", "STATUS", "Ready"
        Dim lpFileName As String 'INI File Key Name "Key=?"

        'Get the ini file location from the Registry
        lpFileName = Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\ATS\", "IniFilePath", Nothing)

        WritePrivateProfileString(sSection, sKey, sKeyVal, lpFileName)
    End Sub

    Function sGetKey(ByVal sSection As String, ByVal sKey As String) As String
        sGetKey = ""
        'DESCRIPTION:
        '   This function retrieves a key value from VIPERT.INI
        'PARAMETERS:
        '   sSection    The VIPERT.INI file section where the key is located.
        '   sKey        The Key name.
        'RETURNS:
        '   The key value.
        'GLOBAL VARIABLES USED:
        '   sWindowsDir
        'EXAMPLE:
        '   sTipCmds = sGetKey("TIPS", "CMD")
        Dim lpReturnedString As String
        Dim nNumChars As Integer
        Dim lpFileName As String 'INI File Key Name "Key=?"

        'Clear String Buffer
        lpReturnedString = Space(255)

        'Get the ini file location from the Registry
        lpFileName = Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\ATS\", "IniFilePath", Nothing)

        nNumChars = GetPrivateProfileString(sSection, sKey, "", lpReturnedString, Len(lpReturnedString), lpFileName)
        sGetKey = Strings.Left(lpReturnedString, nNumChars)
    End Function

    Function bGetInstrumentStatus() As Boolean
        'DESCRIPTION:
        '   Procedure to check the instrument status following issuing setup commands
        '   Updates VIPERT.INI [TIPS] status key to indicate an error if in TIP mode
        Dim sInstrumentStatus As String

        bGetInstrumentStatus = True
        If bolErrorReset Then Exit Function
        SendScpiCommand("SYST:ERR?")
        sInstrumentStatus = ReadInstrumentBuffer()
        sInstrumentStatus = StripNullCharacters(sInstrumentStatus)
        If Val(sInstrumentStatus)=0 Then
            bGetInstrumentStatus = True 'No error
        Else
            bGetInstrumentStatus = False
            frmHPE1420B.txtCtDisplay.Text = "Error"
            MsgBox("Error Code: " & sInstrumentStatus, MsgBoxStyle.Exclamation, "Counter/Timer Error Message")
            If bTIP_Running Then 'Put Error Message in STATUS key, clear CMD key
                SetKey("TIPS", "STATUS", "Error " & StripNullCharacters(sInstrumentStatus))
                SetKey("TIPS", "CMD", "")
            End If
        End If
    End Function

    'JRC Added 033009
    Function GatherIniFileInformation(ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpDefault As String) As String
        GatherIniFileInformation = ""
        '************************************************************
        '*     Finds a value on in the VIPERT.INI File                *
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

        Dim lpReturnedString As String = "" 'Return Buffer
        Dim nSize As Integer 'Return Buffer Size
        Dim lpFileName As String 'INI File Key Name "Key=?"
        Dim ReturnValue As Integer 'Return Value Buffer
        Dim FileNameInfo As String 'Formatted Return String
        Dim lpString As String 'String to write to INI File
        Const MAX_PATH As Long = 260

        'Clear String Buffer
        lpReturnedString = Space$(MAX_PATH)
        'Get the ini file location from the Registry
        lpFileName = Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\ATS\", "IniFilePath", Nothing)
        nSize = 255
        FileNameInfo = ""
        'Find File Locations
        ReturnValue = GetPrivateProfileString(lpApplicationName, lpKeyName, lpDefault, lpReturnedString, nSize, lpFileName)
        FileNameInfo = Trim(lpReturnedString)
        FileNameInfo = Mid(FileNameInfo, 1, Len(FileNameInfo)-1)
        'If File Locations Missing, then create empty keys
        If FileNameInfo=lpDefault+Convert.ToString(Chr(0)) Or FileNameInfo=lpDefault Then
            lpString = Trim(lpDefault)
            ReturnValue = WritePrivateProfileString(lpApplicationName, lpKeyName, lpString, lpFileName)
        End If

        'Return Information In INI File
        GatherIniFileInformation = FileNameInfo
    End Function

    Public Sub ActivateControl(ByVal sSCPI As String)
        'DESCRIPTION:
        '   Sets a panel control to the state indicated by the argument of the
        '   SCPI command in the sSCPI string argument. It simulates the action
        '   of a user selecting the control except that the actual commands are not
        '   sent to the instrument.
        'PARAMETERS:
        '   sSCPI:  A single SCPI or IEEE-488.2 command with argument(s) if any.

        Dim i As Short
        Dim sCmd As String
        Dim sArg As String = ""
        Dim sErrMsg As String
        Dim sList() As String = {""}

        sErrMsg = ""
        'Filter out blank sends caused by redundant semi-colon character at end of command.
        If Len(sSCPI)<2 Then Exit Sub

        'Filter out IEEE-488.2 (non-SCPI) commands. None of these affect the GUI
        'except *RST which is irrelevant because the SAIS and instrument are reset
        'before calling this sub.
        If InStr(sSCPI, "*") Or InStr(sSCPI, "?")>0 Then Exit Sub

        'Parse the command from the argument if it has one.
        If InStr(sSCPI, " ")>0 Then
            sCmd = Strings.Left(sSCPI, InStr(sSCPI, " ")) 'Include space to match SetCod$ array
            sArg = Mid(sSCPI, InStr(sSCPI, " ")+1)
        Else
            sCmd = sSCPI
        End If

        'Find match to a command index constant
        For i = 1 To MAX_SETTINGS
            If sCmd=SetCmd(i) Then Exit For
        Next i
        If i>MAX_SETTINGS Then
            sErrMsg = "Invalid command: " & sSCPI & " passed from TIP Studio."
        End If

        If sErrMsg="" Then
            With frmHPE1420B
                'OK, we have a recognized SCPI command. All are listed here but only
                'expected ones have been un-commented for error checking.
                Select Case i

                        'INPut Options    'Note: Only the enabled channel will be modified
                    Case INP1_ATT
                        .fraCh1Attenuation.Enabled = True
                        Select Case Val(sArg) 'mh used to be Select Case sArg, but changed to get rid of '+' or '-' signs in front
                            Case "1"
                                .optCh1Attenuation1X.Checked = True
                            Case "10"
                                .optCh1Attenuation10X.Checked = True

                            Case Else
                                sErrMsg = "Invalid " & sSCPI & " argument passed from TIP Studio."
                        End Select

                    Case INP1_COUP
                        .fraCh1Coupling.Enabled = True
                        Select Case sArg
                            Case "DC"
                                .optCh1CouplingDC.Checked = True
                            Case "AC"
                                .optCh1CouplingAC.Checked = True

                            Case Else
                                sErrMsg = "Invalid " & sSCPI & " argument passed from TIP Studio."
                        End Select

                    Case INP1_IMP
                        .fraCh1Impedance.Enabled = True
                        Select Case Val(sArg) 'mh used to be Select Case sArg, but changed to get rid of '+' or '-' signs in front
                            Case "1E6", "1000000"
                                .optCh1Impedance1MOhm.Checked = True
                            Case "50"
                                .optCh1Impedance50Ohm.Checked = True

                            Case Else
                                sErrMsg = "Invalid " & sSCPI & " argument passed from TIP Studio."
                        End Select

                    Case INP2_ATT
                        .fraCh2Attenuation.Enabled = True
                        Select Case Val(sArg) 'mh used to be Select Case sArg, but changed to get rid of '+' or '-' signs in front
                            Case "1"
                                .optCh2Attenuation1X.Checked = True
                            Case "10"
                                .optCh2Attenuation10X.Checked = True

                            Case Else
                                sErrMsg = "Invalid " & sSCPI & " argument passed from TIP Studio."
                        End Select

                    Case INP2_COUP
                        .fraCh2Coupling.Enabled = True
                        Select Case sArg
                            Case "DC"
                                .optCh2CouplingDC.Checked = True
                            Case "AC"
                                .optCh2CouplingAC.Checked = True

                            Case Else
                                sErrMsg = "Invalid " & sSCPI & " argument passed from TIP Studio."
                        End Select

                    Case INP2_IMP
                        .fraCh2Impedance.Enabled = True
                        Select Case Val(sArg) 'mh used to be Select Case sArg, but changed to get rid of '+' or '-' signs in front
                            Case "1E6", "1000000"
                                .optCh2Impedance1MOhm.Checked = True
                            Case "50"
                                .optCh2Impedance50Ohm.Checked = True

                            Case Else
                                sErrMsg = "Invalid " & sSCPI & " argument passed from TIP Studio."
                        End Select

                    Case INP_ROUT
                        Select Case sArg
                            Case "SEP"
                                .optRoutingSeparate.Checked = True
                            Case "COMM"
                                .optRoutingCommon.Checked = True

                            Case Else
                                sErrMsg = "Invalid " & sSCPI & " argument passed from TIP Studio."
                        End Select

                        'OUTPut Options
                    Case OUTP_ROSC_STAT
                        Select Case sArg
                            Case "ON"
                                .chkOutputTimebase.Checked = True
                            Case "OFF"
                                .chkOutputTimebase.Checked = False

                            Case Else
                                sErrMsg = "Invalid " & sSCPI & " argument passed from TIP Studio."
                        End Select

                    Case OUTP_TTLT0_STAT
                        If sArg = "ON" Then
                            .cboTriggerOutput.SelectedIndex = 0
                            .optTriggerOutputOn.Checked = True
                        End If

                    Case OUTP_TTLT1_STAT
                        If sArg = "ON" Then
                            .cboTriggerOutput.SelectedIndex = 1
                            .optTriggerOutputOn.Checked = True
                        End If

                    Case OUTP_TTLT2_STAT
                        If sArg = "ON" Then
                            .cboTriggerOutput.SelectedIndex = 2
                            .optTriggerOutputOn.Checked = True
                        End If

                    Case OUTP_TTLT3_STAT
                        If sArg = "ON" Then
                            .cboTriggerOutput.SelectedIndex = 3
                            .optTriggerOutputOn.Checked = True
                        End If

                    Case OUTP_TTLT4_STAT
                        If sArg = "ON" Then
                            .cboTriggerOutput.SelectedIndex = 4
                            .optTriggerOutputOn.Checked = True
                        End If

                    Case OUTP_TTLT5_STAT
                        If sArg = "ON" Then
                            .cboTriggerOutput.SelectedIndex = 5
                            .optTriggerOutputOn.Checked = True
                        End If

                    Case OUTP_TTLT6_STAT
                        If sArg = "ON" Then
                            .cboTriggerOutput.SelectedIndex = 6
                            .optTriggerOutputOn.Checked = True
                        End If

                    Case OUTP_TTLT7_STAT
                        If sArg = "ON" Then
                            .cboTriggerOutput.SelectedIndex = 7
                            .optTriggerOutputOn.Checked = True
                        End If

                        'ARM Options
                    Case ARM_STAR_SOUR
                        Select Case sArg
                            Case "IMM"
                                .cboArmStartSource.SelectedIndex = 11 'Immediate Trigger
                            Case "EXT"
                                .cboArmStartSource.SelectedIndex = 10 'External Trigger
                            Case "BUS"
                                .cboArmStartSource.SelectedIndex = 9 'VXI Bus Trigger
                            Case "HOLD"
                                CheckForManGate()
                                .cboArmStartSource.SelectedIndex = 8 'Holdoff
                            Case "TTLT7"
                                .cboArmStartSource.SelectedIndex = 7 'TTL Trigger 7
                            Case "TTLT6"
                                .cboArmStartSource.SelectedIndex = 6 'TTL Trigger 6
                            Case "TTLT5"
                                .cboArmStartSource.SelectedIndex = 5 'TTL Trigger 5
                            Case "TTLT4"
                                .cboArmStartSource.SelectedIndex = 4 'TTL Trigger 4
                            Case "TTLT3"
                                .cboArmStartSource.SelectedIndex = 3 'TTL Trigger 3
                            Case "TTLT2"
                                .cboArmStartSource.SelectedIndex = 2 'TTL Trigger 2
                            Case "TTLT1"
                                .cboArmStartSource.SelectedIndex = 1 'TTL Trigger 1
                            Case "TTLT0"
                                .cboArmStartSource.SelectedIndex = 0 'TTL Trigger 0

                            Case Else
                                sErrMsg = "Invalid " & sSCPI & " argument passed from TIP Studio."
                        End Select
                        If sErrMsg = "" Then .cboArmStartSource_Click(New Object(), New EventArgs()) 'Setup ARM tab on SAIS

                    Case ARM_STOP_SOUR
                        Select Case sArg
                            Case "IMM"
                                .cboArmStopSource.SelectedIndex = 11 'Immediate Trigger
                            Case "EXT"
                                .cboArmStopSource.SelectedIndex = 10 'External Trigger
                            Case "BUS"
                                .cboArmStopSource.SelectedIndex = 9 'VXI Bus Trigger
                            Case "HOLD"
                                CheckForManGate()
                                .cboArmStopSource.SelectedIndex = 8 'Holdoff
                            Case "TTLT7"
                                .cboArmStopSource.SelectedIndex = 7 'TTL Trigger 7
                            Case "TTLT6"
                                .cboArmStopSource.SelectedIndex = 6 'TTL Trigger 6
                            Case "TTLT5"
                                .cboArmStopSource.SelectedIndex = 5 'TTL Trigger 5
                            Case "TTLT4"
                                .cboArmStopSource.SelectedIndex = 4 'TTL Trigger 4
                            Case "TTLT3"
                                .cboArmStopSource.SelectedIndex = 3 'TTL Trigger 3
                            Case "TTLT2"
                                .cboArmStopSource.SelectedIndex = 2 'TTL Trigger 2
                            Case "TTLT1"
                                .cboArmStopSource.SelectedIndex = 1 'TTL Trigger 1
                            Case "TTLT0"
                                .cboArmStopSource.SelectedIndex = 0 'TTL Trigger 0

                            Case Else
                                sErrMsg = "Invalid " & sSCPI & " argument passed from TIP Studio."
                        End Select
                        If sErrMsg = "" Then .cboArmStopSource_Click(New Object(), New EventArgs()) 'Setup ARM tab on SAIS

                    Case ARM_STAR_LEV
                        Select Case Val(sArg)
                            Case 1.6
                                .optArmStartLevelTTL.Checked = True 'TTL
                            Case -1.3
                                .optArmStartLevelECL.Checked = True 'ECL
                            Case 0
                                .optArmStartLevelGND.Checked = True 'GND

                            Case Else
                                sErrMsg = "Invalid " & sSCPI & " argument passed from TIP Studio."
                        End Select

                    Case ARM_STAR_SLOP
                        Select Case sArg
                            Case "POS"
                                .optArmStartSlopePositive.Checked = True
                            Case "NEG"
                                .optArmStartSlopeNegative.Checked = True

                            Case Else
                                sErrMsg = "Invalid " & sSCPI & " argument passed from TIP Studio."
                        End Select

                    Case ARM_STOP_LEV
                        Select Case Val(sArg)
                            Case 1.6
                                .optArmStopLevelTTL.Checked = True 'TTL
                            Case -1.3
                                .optArmStopLevelECL.Checked = True 'ECL
                            Case 0
                                .optArmStopLevelGND.Checked = True 'GND

                            Case Else
                                sErrMsg = "Invalid " & sSCPI & " argument passed from TIP Studio."
                        End Select

                    Case ARM_STOP_SLOP
                        Select Case sArg
                            Case "POS"
                                .optArmStopSlopePositive.Checked = True
                            Case "NEG"
                                .optArmStopSlopeNegative.Checked = True

                            Case Else
                                sErrMsg = "Invalid " & sSCPI & " argument passed from TIP Studio."
                        End Select

                    Case INIT
                        'Ignore channel INIT command, used only in HOLDoff ARM mode

                        'SENSe Options
                    Case SENS_FUNC
                        'Function has already been determined, no further action required

                    Case SENS_ROSC_SOUR
                        Select Case sArg
                            Case "CLK10"
                                .optTimebaseSourceVXIClk.Checked = True
                            Case "INT"
                                .optTimebaseSourceIntern.Checked = True
                            Case "EXT"
                                .optTimebaseSourceExtern.Checked = True

                            Case Else
                                sErrMsg = "Invalid " & sSCPI & " argument passed from TIP Studio."
                        End Select

                    Case SENS_AVER_STAT
                        Select Case sArg
                            Case "ON"
                                .optTiGateAvgOn.Checked = True
                            Case "OFF"
                                .optTiGateAvgOff.Checked = True

                            Case Else
                                sErrMsg = "Invalid " & sSCPI & " argument passed from TIP Studio."
                        End Select

                    Case SENS_EVEN_HYST
                        Select Case sArg
                            Case "MAX"
                                .optCh1HysteresisMax.Checked = True
                            Case "DEF"
                                .optCh1HysteresisDef.Checked = True
                            Case "MIN"
                                .optCh1HysteresisMin.Checked = True

                            Case Else
                                sErrMsg = "Invalid " & sSCPI & " argument passed from TIP Studio."
                        End Select

                    Case SENS_EVEN_SLOP
                        Select Case sArg
                            Case "POS"
                                .optCh1TriggerSlopePositive.Checked = True
                            Case "NEG"
                                .optCh1TriggerSlopeNegative.Checked = True

                            Case Else
                                sErrMsg = "Invalid " & sSCPI & " argument passed from TIP Studio."
                        End Select

                    Case SENS_EVEN_LEV_ABS_AUTO
                        Select Case sArg
                            Case "ON"
                                .chkCh1AbsTrigAuto.Checked = True
                            Case "OFF"
                                .chkCh1AbsTrigAuto.Checked = False

                            Case Else
                                sErrMsg = "Invalid " & sSCPI & " argument passed from TIP Studio."
                        End Select

                    Case SENS_EVEN_LEV_ABS
                        If Val(sArg) < Val(SetMin(ABS_TRIGGER)) Or Val(sArg) > Val(SetMax(ABS_TRIGGER)) Then
                            sErrMsg = "Invalid " & sSCPI & " argument passed from TIP Studio."
                        Else
                            SetCur(ABS_TRIGGER) = sArg
                            frmHPE1420B.DispTxt(ABS_TRIGGER)
                        End If

                    Case SENS_EVEN_LEV_REL
                        If Val(sArg) < Val(SetMin(REL_TRIGGER)) Or Val(sArg) > Val(SetMax(REL_TRIGGER)) Then
                            sErrMsg = "Invalid " & sSCPI & " argument passed from TIP Studio."
                        Else
                            SetCur(REL_TRIGGER) = sArg
                            frmHPE1420B.DispTxt(REL_TRIGGER)
                        End If

                    Case SENS_FREQ_APER, SENS_PER_APER, SENS_RAT_APER
                        If Val(sArg) < Val(SetMin(APERTURE)) Or Val(sArg) > Val(SetMax(APERTURE)) Then
                            sErrMsg = "Invalid " & sSCPI & " argument passed from TIP Studio."
                        Else
                            SetCur(APERTURE) = sArg
                            frmHPE1420B.DispTxt(APERTURE)
                        End If

                    Case SENS_FREQ_RANG_AUTO
                        Select Case sArg
                            Case "OFF"
                                .chkRange.Checked = False
                            Case "ON"
                                .chkRange.Checked = True

                            Case Else
                                sErrMsg = "Invalid " & sSCPI & " argument passed from TIP Studio."
                        End Select

                    Case SENS_TINT_DEL_STAT
                        Select Case sArg
                            Case "OFF"
                                .chkTIDelay.Checked = False
                            Case "ON"
                                .chkTIDelay.Checked = True

                            Case Else
                                sErrMsg = "Invalid " & sSCPI & " argument passed from TIP Studio."
                        End Select

                    Case SENS_TINT_DEL_TIME
                        If Val(sArg) < Val(SetMin(TI_DELAY)) Or Val(sArg) > Val(SetMax(TI_DELAY)) Then
                            sErrMsg = "Invalid " & sSCPI & " argument passed from TIP Studio."
                        Else
                            SetCur(TI_DELAY) = sArg
                            frmHPE1420B.DispTxt(TI_DELAY)
                        End If

                    Case SENS_TOT_GATE_STAT
                        Select Case sArg
                            Case "OFF"
                                .chkTotalizeGateState.Checked = False
                            Case "ON"
                                .chkTotalizeGateState.Checked = True

                            Case Else
                                sErrMsg = "Invalid " & sSCPI & " argument passed from TIP Studio."
                        End Select

                    Case SENS_TOT_GATE_POL
                        Select Case sArg
                            Case "NORM"
                                .optTotalizeGatePolarityNormal.Checked = True
                            Case "INV"
                                .optTotalizeGatePolarityInvert.Checked = True

                            Case Else
                                sErrMsg = "Invalid " & sSCPI & " argument passed from TIP Studio."
                        End Select

                    Case SENS_ATIM_CHEC
                        Select Case sArg
                            Case "ON"
                                .optAcquisitionTimeoutOn.Checked = True
                            Case "OFF"
                                .optAcquisitionTimeoutOff.Checked = True
                            Case "STAR"
                                .optAcquisitionTimeoutStart.Checked = True

                            Case Else
                                sErrMsg = "Invalid " & sSCPI & " argument passed from TIP Studio."
                        End Select

                    Case SENS_ATIM_TIME
                        If Convert.ToDouble(sArg) < Convert.ToDouble(SetMin(DUR_TIMEOUT)) Or Convert.ToDouble(sArg) > Convert.ToDouble(SetMax(DUR_TIMEOUT)) Then
                            sErrMsg = "Invalid " & sSCPI & " argument passed from TIP Studio."
                        Else
                            SetCur(DUR_TIMEOUT) = sArg
                            frmHPE1420B.DispTxt(DUR_TIMEOUT)
                        End If

                        'CONFigure Options
                    Case CONF_FREQ, CONF_PER, CONF_FREQ_RAT, CONF_TINT, CONF_QUOTE_FREQ
                        'mh added last one
                        If Strings.Left(sArg, 1) <> "," Then
                            If Val(sArg) < Val(SetMin(RANGE)) Or Val(sArg) > Val(SetMax(RANGE)) Then
                                If InStr(sArg, "MAX") > 0 Then 'mh added 'cause instruments sends back string "MAX"
                                    SetCur(RANGE) = Val(SetMax(RANGE))
                                ElseIf InStr(sArg, "MIN") > 0 Then
                                    SetCur(RANGE) = Val(SetMin(RANGE))
                                ElseIf InStr(sArg, "DEF") > 0 Then
                                    SetCur(RANGE) = Val(SetDef(RANGE))
                                Else
                                    sErrMsg = "Invalid " & sSCPI & " argument passed from TIP Studio."
                                End If
                            Else
                                SetCur(RANGE) = Val(sArg)
                                frmHPE1420B.DispTxt(RANGE)
                            End If
                        End If

                    Case CONF_RTIM, CONF_FTIM
                        'Example:  CONF1:RTIM 90, 10,  .0000001
                        StringToList(sArg, 1, sList, ",")
                        sArg = LTrim(sList(3)) 'Only interested in Argument #3
                        'if SFP is starting up and didn't retrieve values from instr. set to def. value
                        If bInstrumentMode And sArg = "" Then
                            sArg = Val(SetDef(RANGE))
                        End If
                        If Val(sArg) < Val(SetMin(RANGE)) Or Val(sArg) > Val(SetMax(RANGE)) Then
                            sErrMsg = "Invalid " & sSCPI & " argument passed from TIP Studio."
                        Else
                            SetCur(RANGE) = sArg
                            frmHPE1420B.DispTxt(RANGE)
                        End If

                    Case CONF_PWID, CONF_NWID
                        'Example:  CONF1:PWID 50,  .0000001
                        StringToList(sArg, 1, sList, ",")
                        sArg = LTrim(sList(2)) 'Only interested in Argument #2
                        If Val(sArg) < Val(SetMin(RANGE)) Or Val(sArg) > Val(SetMax(RANGE)) Then
                            sErrMsg = "Invalid " & sSCPI & " argument passed from TIP Studio."
                        Else
                            SetCur(RANGE) = sArg
                            frmHPE1420B.DispTxt(RANGE)
                        End If

                    Case CONF_TOT, CONF_PHAS
                        'No further action required


                    Case Else
                        sErrMsg = "Counter/Timer SAIS does not support '" & RTrim(sCmd) & "' command passed from TIP Studio."
                End Select
            End With
        End If

        If sErrMsg<>"" Then
            'Notify TIP studio that an error has occured and quit SAIS
            frmHPE1420B.txtCtDisplay.Text = "Error"
            If  Not (sTIP_Mode="TIP_DESIGN") And  Not (sTIP_Mode="GET CURR CONFIG") Then
                'If sTIP_Mode <> "TIP_DESIGN" Then
                Delay(1)
                SetKey("TIPS", "STATUS", "Error: " & StripNullCharacters(sErrMsg))
                ExitCounterTimer()
            End If
            If  Not (sTIP_Mode="GET CURR CONFIG") Then
                MsgBox(sErrMsg & vbCrLf & vbCrLf & "Instrument will be set to RESET state.", MsgBoxStyle.Exclamation)
                ResetCounterTimer()
                sTIP_Mode = "TIP_ERROR" 'Flag to exit saved string setup
            Else
                sErrMsg = "Invalid <" & sSCPI & "> argument received from instrument query."
                MsgBox(sErrMsg, MsgBoxStyle.OkOnly, "Get Current - Unhandled Parameter")
            End If
        End If
    End Sub

    Public Sub ConfigureFrontPanel()
        'DESCRIPTION:
        '   This Module configures the SAIS in TIP Mode based on the settings of a
        '   saved command string.
        'EXAMPLE:
        '   ConfigureFrontPanel

        Dim i As Integer
        Dim sCmds() As String = {""}
        Dim sArg As String
        Dim sChan As String = LTrim(Str(Channel))
        Dim sErrMsg As String = ""
        Dim oldInstrumentMode As Short = InstrumentMode

        Application.DoEvents()
        InstrumentMode = 1

        'Parsing SCPI commands from string loaded from [TIPS]CMD key
        'Set SAIS controls and instrument from SCPI commands.
        bDoNotTalk = True
        For i = 1 To StringToList(sTIP_CMDstring, 1, sCmds, ";")
            'Ascertain the Channel used and the Measurement function
            If i = 1 Then
                If InStr(sCmds(1), "INP1") > 0 Then
                    Channel = 1
                Else
                    Channel = 2
                End If
                'Select channel on GUI Measurement tab
                frmHPE1420B.optInputChannel1.Checked = True
                sChan = LTrim(Str(Channel))
            End If
            If InStr(sCmds(i), "SENS" & sChan & ":FUNC")>0 Then
                sArg = Mid(sCmds(i), InStr(sCmds(i), " ")+1)
                Select Case sArg
                    Case Q & """FREQ" & Q:
                        InstrumentMode = 1 'added because this instrument returns with extra quote, not sure all will
                    Case Q & "FREQ" & Q:
                        InstrumentMode = 1 '"Frequency Measurement" (Default)
                    Case Q & "PER" & Q:
                        InstrumentMode = 2 '"Period Measurement"
                    Case Q & "FREQ:RAT" & Q:
                        InstrumentMode = 3 '"Frequency Ratio Measurement"
                    Case Q & "RTIM" & Q:
                        InstrumentMode = 4 '"Rise Time Measurement"
                    Case Q & "FTIM" & Q:
                        InstrumentMode = 5 '"Fall Time Measurement"
                    Case Q & "TINT" & Q:
                        InstrumentMode = 6 '"Time Interval Measurement"
                    Case Q & "PWID" & Q:
                        InstrumentMode = 7 '"Positive (+) Pulse Width Measurement"
                    Case Q & "NWID" & Q:
                        InstrumentMode = 8 '"Negative (-) Pulse Width Measurement"
                    Case Q & "TOT" & Q:
                        InstrumentMode = 9 '"Totalize Measurement"
                    Case Q & "PHAS" & Q:
                        InstrumentMode = 10 'mh was 14"Phase Measurement"

                    Case Else
                        sErrMsg = "Invalid SENS:FUNC <function> argument passed from TIP Studio."
                End Select
                If InstrumentMode > 0 And sErrMsg = "" Then
                    If Not frmHPE1420B.Panel_Conifg.DebugMode Or InstrumentMode <> oldInstrumentMode Then
                        For Each Button In frmHPE1420B.tbrCtFunctions.Buttons
                            Button.pushed = False
                        Next
                        frmHPE1420B.tbrCtFunctions.Buttons(InstrumentMode - 1).Pushed = True
                        ChangeInstrumentMode()
                    End If
                Else ':                    Exit For
                End If
            End If
        Next i

        If sErrMsg<>"" Then
            'Notify TIP studio that an error has occured and quit SAIS
            frmHPE1420B.txtCtDisplay.Text = "Error"
            SetKey("TIPS", "STATUS", "Error: " & StripNullCharacters(sErrMsg))
            'ExitCounterTimer
        End If

        'Now that the channel is known, initialize TIP Command string array
        SetCmd(SENS_FUNC) = ":SENS" & sChan & ":FUNC "
        SetCmd(SENS_AVER_STAT) = ":SENS" & sChan & ":AVER:STAT "
        SetCmd(SENS_EVEN_LEV_ABS) = ":SENS" & sChan & ":EVEN:LEV:ABS "
        SetCmd(SENS_EVEN_LEV_REL) = ":SENS" & sChan & ":EVEN:LEV:REL "
        SetCmd(SENS_EVEN_LEV_ABS_AUTO) = ":SENS" & sChan & ":EVEN:LEV:ABS:AUTO "
        SetCmd(SENS_EVEN_SLOP) = ":SENS" & sChan & ":EVEN:SLOP "
        SetCmd(SENS_EVEN_HYST) = ":SENS" & sChan & ":EVEN:HYST "
        SetCmd(SENS_FREQ_APER) = ":SENS" & sChan & ":FREQ:APER "
        SetCmd(SENS_PER_APER) = ":SENS" & sChan & ":PER:APER "
        SetCmd(SENS_RAT_APER) = ":SENS" & sChan & ":RAT:APER "
        SetCmd(SENS_FREQ_RANG_AUTO) = ":SENS" & sChan & ":FREQ:RANG:AUTO "
        SetCmd(SENS_ROSC_SOUR) = ":SENS" & sChan & ":ROSC:SOUR "
        SetCmd(SENS_TINT_DEL_STAT) = ":SENS" & sChan & ":TINT:DEL:STAT "
        SetCmd(SENS_TINT_DEL_TIME) = ":SENS" & sChan & ":TINT:DEL:TIME "
        SetCmd(SENS_TOT_GATE_STAT) = ":SENS" & sChan & ":TOT:GATE:STAT "
        SetCmd(SENS_TOT_GATE_POL) = ":SENS" & sChan & ":TOT:GATE:POL "
        SetCmd(SENS_ATIM_TIME) = ":SENS:ATIM:TIME "
        SetCmd(SENS_ATIM_CHEC) = ":SENS:ATIM:CHEC "
        SetCmd(INIT) = ":INIT" & sChan

        SetCmd(INP1_ATT) = ":INP1:ATT "
        SetCmd(INP1_COUP) = ":INP1:COUP "
        SetCmd(INP1_IMP) = ":INP1:IMP "
        SetCmd(INP2_ATT) = ":INP2:ATT "
        SetCmd(INP2_COUP) = ":INP2:COUP "
        SetCmd(INP2_IMP) = ":INP2:IMP "
        SetCmd(INP_ROUT) = ":INP1:ROUT "

        SetCmd(CONF_FREQ) = ":CONF" & sChan & ":FREQ "
        SetCmd(CONF_QUOTE_FREQ) = ":CONF" & sChan & ":""FREQ " 'mh added because instrument returns "FREQ instead of just FREQ
        SetCmd(CONF_PER) = ":CONF" & sChan & ":PER "
        SetCmd(CONF_FREQ_RAT) = ":CONF" & sChan & ":FREQ:RAT "
        SetCmd(CONF_RTIM) = ":CONF" & sChan & ":RTIM "
        SetCmd(CONF_FTIM) = ":CONF" & sChan & ":FTIM "
        SetCmd(CONF_TINT) = ":CONF" & sChan & ":TINT "
        SetCmd(CONF_PWID) = ":CONF" & sChan & ":PWID "
        SetCmd(CONF_NWID) = ":CONF" & sChan & ":NWID "
        SetCmd(CONF_TOT) = ":CONF" & sChan & ":TOT" 'No Arguments, thus no Space
        SetCmd(CONF_PHAS) = ":CONF" & sChan & ":PHAS "

        SetCmd(ARM_STAR_SOUR) = ":ARM:STAR:SOUR "
        SetCmd(ARM_STOP_SOUR) = ":ARM:STOP:SOUR "
        SetCmd(ARM_STAR_LEV) = ":ARM:STAR:LEV "
        SetCmd(ARM_STAR_SLOP) = ":ARM:STAR:SLOP "
        SetCmd(ARM_STOP_LEV) = ":ARM:STOP:LEV "
        SetCmd(ARM_STOP_SLOP) = ":ARM:STOP:SLOP "

        SetCmd(OUTP_ROSC_STAT) = ":OUTP:ROSC:STAT "
        SetCmd(OUTP_TTLT0_STAT) = ":OUTP:TTLT0:STAT "
        SetCmd(OUTP_TTLT1_STAT) = ":OUTP:TTLT1:STAT "
        SetCmd(OUTP_TTLT2_STAT) = ":OUTP:TTLT2:STAT "
        SetCmd(OUTP_TTLT3_STAT) = ":OUTP:TTLT3:STAT "
        SetCmd(OUTP_TTLT4_STAT) = ":OUTP:TTLT4:STAT "
        SetCmd(OUTP_TTLT5_STAT) = ":OUTP:TTLT5:STAT "
        SetCmd(OUTP_TTLT6_STAT) = ":OUTP:TTLT6:STAT "
        SetCmd(OUTP_TTLT7_STAT) = ":OUTP:TTLT7:STAT "

        'If sTIP_Mode <> "TIP_DESIGN" Then
        If  Not (sTIP_Mode="TIP_DESIGN") And  Not (sTIP_Mode="GET CURR CONFIG") Then
            For i = 1 To 2 'Initially disable input 1/2 controls
                frmHPE1420B.fraCh1Impedance.Enabled = False
                frmHPE1420B.fraCh1Coupling.Enabled = False
                frmHPE1420B.fraCh1Attenuation.Enabled = False
                frmHPE1420B.fraCh2Impedance.Enabled = False
                frmHPE1420B.fraCh2Coupling.Enabled = False
                frmHPE1420B.fraCh2Attenuation.Enabled = False
            Next i
        End If

        If  Not (sTIP_Mode="GET CURR CONFIG") Then
            'Now setup controls one at a time IAW command string
            For i = 1 To UBound(sCmds)
                ActivateControl(sCmds(i))
                If sTIP_Mode="TIP_ERROR" Then
                    'If a saved string error has occured, SAIS is set to reset state
                    sTIP_Mode = "TIP_DESIGN"
                    Exit For
                End If
            Next i

            bDoNotTalk = False
            Application.DoEvents()
        End If
    End Sub

    Sub TipBuildINPutString(ByVal iChan As Short)
        'DESCRIPTION:
        '   Appends the INPut Subsystem SAIS settings to the TIP
        '   SCPI command string when Update TIP command buttton
        '   is pressed in TIP Design mode, based upon the selected
        '   input channel and whether a particular channel is enabled.
        'EXAMPLE:
        '   TipBuildINPutString (1)

        If iChan=1 Then
            If frmHPE1420B.optCh1Attenuation1X.Enabled = False Then Exit Sub

            'Attenuation:
            If frmHPE1420B.optCh1Attenuation1X.Checked Then
                sTIP_CMDstring &= "INP1:ATT 1;:" 'X1
            Else
                sTIP_CMDstring &= "INP1:ATT 10;:" 'X10
            End If

            'Coupling:
            If frmHPE1420B.optCh1CouplingDC.Checked Then
                sTIP_CMDstring &= "INP1:COUP DC;:" 'DC
            Else
                sTIP_CMDstring &= "INP1:COUP AC;:" 'AC
            End If

            'Impedance:
            If frmHPE1420B.optCh1Impedance1MOhm.Checked Then
                sTIP_CMDstring &= "INP1:IMP 1E6;:" '1M
            Else
                sTIP_CMDstring &= "INP1:IMP 50;:" '50
            End If
        Else
            If frmHPE1420B.optCh2Attenuation1X.Enabled = False Then Exit Sub

            'Attenuation:
            If frmHPE1420B.optCh2Attenuation1X.Checked Then
                sTIP_CMDstring &= "INP2:ATT 1;:" 'X1
            Else
                sTIP_CMDstring &= "INP2:ATT 10;:" 'X10
            End If

            'Coupling:
            If frmHPE1420B.optCh2CouplingDC.Checked Then
                sTIP_CMDstring &= "INP2:COUP DC;:" 'DC
            Else
                sTIP_CMDstring &= "INP2:COUP AC;:" 'AC
            End If

            'Impedance:
            If frmHPE1420B.optCh2Impedance1MOhm.Checked Then
                sTIP_CMDstring &= "INP2:IMP 1E6;:" '1M
            Else
                sTIP_CMDstring &= "INP2:IMP 50;:" '50
            End If
        End If

    End Sub
End Module