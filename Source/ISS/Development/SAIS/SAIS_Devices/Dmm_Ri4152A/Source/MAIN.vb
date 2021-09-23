'Option Strict Off
'Option Explicit On

Imports System
Imports System.Windows.Forms
Imports System.Windows.Forms.Screen
Imports System.Text
Imports System.Diagnostics
Imports Microsoft.VisualBasic
Imports Microsoft.Win32

Public Module DmmMain
    '=========================================================
    '//2345678901234567890123456789012345678901234567890123456789012345678901234567890
    '///////////////////////////////////////////////////////////////////////////////////////////////////////
    '//
    '// Virtual Instrument Portable Equipment Repair/Tester (VIPER/T) Software Module
    '//
    '// File:       Main.bas
    '//
    '// Date:       27Oct06
    '//
    '// Purpose:    SAIS: Digital Multimeter Front Panel
    '//
    '// Instrument: Agilent E1412A Digital Multimeter (Dmm)
    '//
    '//
    '// Revision History
    '// Rev      Date                  Reason                                   Author
    '// =======  =======    =======================================             ====================
    '// 1.1.0.0  27Oct06    Initial Release                                     EADS, North Americ Defense
    '// 1.1.0.1  15Nov06    Following changes were made:                        M. Hendricksen, EADS
    '//                     - Added code to query the inst about "AUTO-RANGE"
    '//                         mode and if it was in "Auto Range" then ensure
    '//                         that the GUI was set up for "AUTO RANGE".
    '//                     - Added code to query if the Math Functions were
    '//                         enabled.  If they were not enabled set the GUI math
    '//                         functions box to "None".  If they were enabled then
    '//                         query the type of math function and set GUI accordingly.
    '//                     - Changed Logo
    '// 1.2.0.1 19Dec06     Corrected the code in TextBox(), to just set the    M. Hendricksen, EADS
    '//                         newval with whats in TextBox(Index%)then to range check value.
    '//                         It was range checking on TextBox(Index%) withtout removing
    '//                         the UOM that is also in the textbox, so therefor it would give error
    '// 1.3.0.1 16Apr07     Made correction in cmdSelfTest in frmRi4152A        M. Hendricksen, EADS
    '//                         increased the delay from 8 to 13 to give time for
    '//                         DMM to complete self-test and looked for a return
    '//                         value of "0" instead of an empty string for success.
    '*******************************************************************************************************



    Public DebugDwh As Short
    '-----------------API / DLL Declarations------------------------------

    'Prototype:
    'int atxml_Initialize(const char *ProcType, const char *Guid)
    Declare Function atxml_Initialize Lib "AtXmlApi.dll" (ByVal ProcType As String, ByVal ProcUuid As String) As Integer

    Declare Function atxml_Close Lib "AtXmlApi.dll" () As Integer

    'Prototype:
    'int atxml_ValidateRequirements(ATXML_ATML_Snippet* TestRequirements,
    '                               ATXML_XML_Filename* Allocation,
    '                               ATXML_XML_String*   Availability,
    '                               int                 BufferSize)
    Declare Function atxml_ValidateRequirements Lib "AtXmlApi.dll" (ByVal TestRequirements As String, ByVal Allocation As String, ByVal Availability As String, ByVal BufferSize As Integer) As Integer

    Declare Function atxml_WriteCmds Lib "AtXmlApi.dll" (ByVal ResourceName As String, ByVal InstrumentCmds As String, ByRef ActWriteLen As Integer) As Integer

    ' WMT 1/20/2006 Changed the BufferSize declaration
    ' from:  BufferSize As Long,
    ' to:    ByVal BufferSize as Integer,
    ' BUT ByRef ActReadLen As Long BECAUSE IT IS A PONITER
    Declare Function atxml_ReadCmds Lib "AtXmlApi.dll" (ByVal ResourceName As String, ByVal ReadBuffer As String, ByVal BufferSize As Integer, ByRef ActReadLen As Integer) As Integer
    Declare Function atxmlDF_viClear Lib "AtxmlDriverFunc.DLL" (ByVal ResourceName As String, ByVal vi As Integer) As Integer

    Declare Function GetPrivateProfileString Lib "Kernel32" Alias "GetPrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpDefault As String, ByVal lpReturnedString As String, ByVal nSize As Integer, ByVal lpFileName As String) As Integer
    Declare Function WritePrivateProfileString Lib "Kernel32" Alias "WritePrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpString As String, ByVal lpFileName As String) As Integer
    'Declare Function viClear Lib "VISA32.DLL" Alias "#260" (ByVal vi As Long) As Long
    'Declare Function viSetAttribute Lib "VISA32.DLL" Alias "#134" (ByVal vi As Long, ByVal attrName As Long, ByVal attrValue As Long) As Long
    'Declare Function viGetAttribute Lib "VISA32.DLL" (ByVal instrumentHandle&, ByVal attibute&, attibState As Any) As Long

    '-----------------WMT Test Structure----------------------

    Structure TestStruct
        Dim ShortVar As Short
        Dim UcharVar As Byte
        Dim FloatVar As Single
        Dim stringVar As String
    End Structure
    '-----------------Init and GUID ---------------------------------------
    Public Const guid As String = "{6826A2CA-2F6E-469d-80F3-FF50730D52DD}"
    Public Const ProcType As String = "SFP"
    '-----------------User Defined Types and Objects----------------------
    Public frmParent As frmRi4152A 'For Center Child Form Routine
    '-----------------Global Constants------------------------------------
    'Global XmlBuf As String * 4096
    'Global XmlBuf As String
    Public special_1_XmlBuf As String
    'Global special_2_XmlBuf As String
    Public special_3_XmlBuf As String
    '-----------------Global Constants------------------------------------
    Public Q As String 'Utility ASCII Quote (") Symbol
    Public Const INSTRUMENT_NAME As String = "Digital Multimeter" 'About Box Constant
    Public Const MANUF As String = "Astronics Test Systems" 'About Box Constant
    Public Const MODEL_CODE As String = "4152A" 'About Box Constant
    Public Const RESOURCE_NAME As String = "VXI::44" 'About Box Constant
    Public Const VI_ATTR_TMO_VALUE As Integer = &H3FFF001A 'Constant needed to set VISA Timeout
    Const SECS_IN_DAY As Integer = 86400
    '-----------------Global Variables------------------------------------
    Public sWindowsDir As String 'Path of Windows Directory
    Public LiveMode As Boolean 'Enable/Disable Live Dmm Communication
    Public InstrumentMode As Short 'Determines the current instrument function
    Public RangeDisplay As Boolean 'Track Range Data for Input Text Box Control
    Public RANGE As String 'User Range Choice
    Public TRIGGER As String 'User Trigger Choice
    Public SAMPLE As String 'User Sample Count Choice
    Public Aperture As String 'User Aperture Choice
    Public NPLC As String 'User Number of Power LIne Cycles Choice
    Public InstrumentHandle As Integer 'VISA Handle for this instance of the DMM
    Public ErrorStatus As Integer 'VISA Return Error Value
    Public ResourceName As String 'VISA Instrument Name
    Public AverageMeas As Short 'User Choice Average Measurement Flag
    Public bTIP_Running As Boolean 'Flag indicating SAIS was invoked by TIP Studio
    Public bBuildingTIPstr As Boolean 'Flag indicating TIP design mode is building the SCPI CMD string
    Public sTIP_Mode As String 'TIP mode indicator
    Public sTIP_CMDstring As String 'TIP SCPI Command string defining instrument setup
    Public sTIP_Measured As String 'Measured value not in Eng. Notation format (Absolute Value)
    Public bDoNotTalk As Boolean
    Public sFilterDelay As Short 'Sets a measurement delay if 3Hz filter is chosen
    Public QuitProgram As Boolean ' used to determine if resetting or quit out of program
    '-----------------Global Arrays---------------------------------------
    Public VOLTAGE_RANGE_VALUE(5) As String 'Combo Box Values
    Public CURRENT_RANGE_VALUE(4) As String 'Combo Box Values
    Public RESISTANCE_RANGE_VALUE(7) As String 'Combo Box Values
    Public MATH_FUNCTION_VALUE(7) As String 'Combo Box Values
    Public TRIGGER_SOURCE_VALUE(10) As String 'Combo Box Values
    Public Aperture_RANGE1_VALUE(4) As String 'Combo Box Values
    Public Aperture_RANGE2_VALUE(2) As String 'Combo Box Values
    Public NPLC_RANGE_VALUE(4) As String 'Combo Box Values
    Public OUTPUT_TRIGGER_STATE(7) As Boolean 'Combo Box Values
    '-----------------Input Text Box / Spin Button Instruction -----------
    Public Const MAX_SETTINGS As Short = 49 'Array Limits For Input Text Box/TIP Cmd strings
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

    Public Const MATHINPUT As Short = 1 'Math Input Text Box Index
    Public Const TRIGGER_DELAY As Short = 2 'Trigger Delay Input Text Box Index
    Public Const TRIG_COUN As Short = 3
    Public Const SAMP_COUN As Short = 4

    '------------VISA ERROR CONSTANTS---------------
    Public Const VI_ERROR_SYSTEM_ERROR As Integer = &HBFFF0000
    Public Const VI_ERROR_TMO As Integer = &HBFFF0015
    '-----------TIP CMD STRING CONSTANTS-----------
    '***SENSe Options***
    Public Const SENS_FUNC As Short = 5
    Public Const SENS_CURR_AC_RANG As Short = 6
    Public Const SENS_CURR_AC_RANG_AUTO As Short = 7
    Public Const SENS_CURR_DC_APER As Short = 8
    Public Const SENS_CURR_DC_NPLC As Short = 9
    Public Const SENS_CURR_DC_RANG As Short = 10
    Public Const SENS_CURR_DC_RANG_AUTO As Short = 11
    Public Const SENS_DET_BAND As Short = 12
    Public Const SENS_FREQ_APER As Short = 13
    Public Const SENS_FREQ_VOLT_RANG_AUTO As Short = 14
    Public Const SENS_FRES_APER As Short = 15
    Public Const SENS_FRES_NPLC As Short = 16
    Public Const SENS_FRES_RANG As Short = 17
    Public Const SENS_FRES_RANG_AUTO As Short = 18
    Public Const SENS_PER_APER As Short = 19
    Public Const SENS_PER_VOLT_RANG_AUTO As Short = 20
    Public Const SENS_RES_APER As Short = 21
    Public Const SENS_RES_NPLC As Short = 22
    Public Const SENS_RES_RANG As Short = 23
    Public Const SENS_RES_RANG_AUTO As Short = 24
    Public Const SENS_VOLT_AC_RANG As Short = 25
    Public Const SENS_VOLT_AC_RANG_AUTO As Short = 26
    Public Const SENS_VOLT_DC_APER As Short = 27
    Public Const SENS_VOLT_DC_NPLC As Short = 28
    Public Const SENS_VOLT_DC_RANG As Short = 29
    Public Const SENS_VOLT_DC_RANG_AUTO As Short = 30
    Public Const SENS_ZERO_AUTO As Short = 31
    ' added for freq:volt:rang and per:volt:rang for when not in auto
    Public Const SENS_FREQ_VOLT_RANG As Short = 48
    Public Const SENS_PER_VOLT_RANG As Short = 49

    '***CALCulate Options***
    Public Const CALC_STAT As Short = 32
    Public Const CALC_FUNC As Short = 33
    Public Const CALC_DB_REF As Short = 34
    Public Const CALC_DBM_REF As Short = 35
    '***INPut/OUTPut Options***
    Public Const INP_IMP_AUTO As Short = 36
    Public Const OUTP_TTLT0_STAT As Short = 37
    Public Const OUTP_TTLT1_STAT As Short = 38
    Public Const OUTP_TTLT2_STAT As Short = 39
    Public Const OUTP_TTLT3_STAT As Short = 40
    Public Const OUTP_TTLT4_STAT As Short = 41
    Public Const OUTP_TTLT5_STAT As Short = 42
    Public Const OUTP_TTLT6_STAT As Short = 43
    Public Const OUTP_TTLT7_STAT As Short = 44
    '***TRIGger Options***
    Public Const TRIG_DEL As Short = 45
    Public Const TRIG_DEL_AUTO As Short = 46
    Public Const TRIG_SOUR As Short = 47


    '***Read Instrument Mode***
    Public iStatus As Short
    Public gctlParse As New VIPERT_Common_Controls.Common
    '###############################################################

    Public Sub voidFunc(ByVal stringVar As String)
        MessageBox.Show(stringVar, "4152")
    End Sub

    Public Sub SetupCallbacks()
        '    CallbackExample AddressOf voidFunc
    End Sub

    Sub ExitDmm()
        'DESCRIPTION:
        '   This Module Exits the program.
        'EXAMPLE:
        '   ExitDmm

        If sTIP_Mode = "TIP_RUNPERSIST" Or sTIP_Mode = "TIP_RUNSETUP" Then
            If Strings.UCase(frmRi4152A.txtDmmDisplay.Text) <> "ERROR" And Strings.Trim(frmRi4152A.txtDmmDisplay.Text) <> "" Then
                'Put measurement into [TIPS] STATUS key for studio
                SetKey("TIPS", "STATUS", "Ready " & sTIP_Measured)
                SetKey("TIPS", "CMD", "")
            End If
        End If
        'Give Resources Back To Op-System
        ' only clear if just quitting out of program
        If QuitProgram Then
            SendScpiCommand("*CLS")
            QuitProgram = False
        Else
            SendScpiCommand("*RST:*CLS")
        End If
        ReturnInstrumentHandle()

        'Give Program Resources back to Operating System
        Application.Exit()
    End Sub

    Public Sub SplashEnd()
        'DESCRIPTION:
        '   This Module Removes the Splash/About Screen.
        'EXAMPLE:
        '   SplashEnd

        frmAbout.Hide()
    End Sub

    Public Sub SplashStart()
        'DESCRIPTION:
        '   This Module Displays the Splash/About Screen.
        'EXAMPLE:
        '   SplashStart

        '! Load frmAbout
        frmAbout.cmdOk.Visible = False
        frmAbout.Show()
        frmAbout.Refresh()
    End Sub

    Sub AdjustMathComboBox()
        'DESCRIPTION:
        '   This Module Displays the correct information in the
        '   SAIS panel according to the user's math choice.
        'EXAMPLE:
        '   AdjustMathComboBox

        Select Case frmRi4152A.cboMath.Text
            Case "dBm"
                frmRi4152A.panMath.Visible = True
                frmRi4152A.spnMath.Visible = True
                frmRi4152A.panMathFunction.Visible = True
                frmRi4152A.panMathFunction.Text = "Ref. (Ohms)"
                SetDef(MATHINPUT) = "600"
                SetMin(MATHINPUT) = "50"
                SetMax(MATHINPUT) = "8000"
                SetInc(MATHINPUT) = "1"
                SetRes(MATHINPUT) = "0"
                SetCur(MATHINPUT) = "600"
                SetCmd(MATHINPUT) = ""
                SetUOM(MATHINPUT) = "1"
                SetRngMsg(MATHINPUT) = "Min: 50 Ohms    Def: 600 Ohms    Max: 8000 Ohms"
                frmRi4152A.txtMath.Text = SetCur(MATHINPUT)
            Case "dB"
                frmRi4152A.panMath.Visible = True
                frmRi4152A.spnMath.Visible = True
                frmRi4152A.panMathFunction.Visible = True
                frmRi4152A.panMathFunction.Text = "Ref. (dBm)"
                SetDef(MATHINPUT) = "100"
                SetMin(MATHINPUT) = "-200"
                SetMax(MATHINPUT) = "200"
                SetInc(MATHINPUT) = "1"
                SetRes(MATHINPUT) = "0"
                SetCur(MATHINPUT) = "100"
                SetCmd(MATHINPUT) = ""
                SetUOM(MATHINPUT) = "1"
                SetRngMsg(MATHINPUT) = "Min: -200 dBm    Def: 100 dBm    Max: 200 dBm"
                frmRi4152A.txtMath.Text = SetCur(MATHINPUT)
            Case "Null"
                frmRi4152A.panMath.Visible = False
                frmRi4152A.spnMath.Visible = False
                frmRi4152A.panMathFunction.Visible = False
                frmRi4152A.txtMath.Text = ""
            Case "Average", "Min", "Max"
                frmRi4152A.panMath.Visible = False
                frmRi4152A.spnMath.Visible = False
                frmRi4152A.panMathFunction.Visible = False
                frmRi4152A.txtMath.Text = ""
            Case "None"
                frmRi4152A.panMath.Visible = False
                frmRi4152A.spnMath.Visible = False
                frmRi4152A.panMathFunction.Visible = False
                frmRi4152A.txtMath.Text = ""
        End Select
    End Sub


    Sub AdjustTriggerOutput()
        'DESCRIPTION:
        '   This Module Displays the correct information in the
        '   SAIS panel according to the user's trigger choice.
        'EXAMPLE:
        '   AdjustTriggerOutput

        Select Case frmRi4152A.cboTriggerOutput.Text
            Case "TTL Trigger 7"
                frmRi4152A.optTriggerOutputOn.Checked = OUTPUT_TRIGGER_STATE(7)
            Case "TTL Trigger 6"
                frmRi4152A.optTriggerOutputOn.Checked = OUTPUT_TRIGGER_STATE(6)
            Case "TTL Trigger 5"
                frmRi4152A.optTriggerOutputOn.Checked = OUTPUT_TRIGGER_STATE(5)
            Case "TTL Trigger 4"
                frmRi4152A.optTriggerOutputOn.Checked = OUTPUT_TRIGGER_STATE(4)
            Case "TTL Trigger 3"
                frmRi4152A.optTriggerOutputOn.Checked = OUTPUT_TRIGGER_STATE(3)
            Case "TTL Trigger 2"
                frmRi4152A.optTriggerOutputOn.Checked = OUTPUT_TRIGGER_STATE(2)
            Case "TTL Trigger 1"
                frmRi4152A.optTriggerOutputOn.Checked = OUTPUT_TRIGGER_STATE(1)
            Case "TTL Trigger 0"
                frmRi4152A.optTriggerOutputOn.Checked = OUTPUT_TRIGGER_STATE(0)
        End Select

        frmRi4152A.optTriggerOutputOff.Checked = Not frmRi4152A.optTriggerOutputOn.Checked
    End Sub

    Sub InitInputBoxLists()
        'DESCRIPTION:
        '   This Module sets the user input and combo box values.
        'EXAMPLE:
        '   InitInputBoxLists

        'Init flags
        RangeDisplay = False
        'Initialize Defaults
        SetDef(MATHINPUT) = "100"
        SetDef(TRIGGER_DELAY) = "0"
        SetDef(TRIG_COUN) = "1"
        SetDef(SAMP_COUN) = "1"
        'Initialize Minimums
        SetMin(MATHINPUT) = "10"
        SetMin(TRIGGER_DELAY) = "0"
        SetMin(TRIG_COUN) = "1"
        SetMin(SAMP_COUN) = "1"
        'Initialize Maximums
        SetMax(MATHINPUT) = "1000"
        SetMax(TRIGGER_DELAY) = "3600"
        SetMax(TRIG_COUN) = "50000"
        SetMax(SAMP_COUN) = "50000"
        'Initialize Increment Values
        SetInc(MATHINPUT) = "1"
        'SetInc$(TRIGGER_DELAY) = "1"
        SetMinInc(TRIGGER_DELAY) = "0.000001"
        SetInc(TRIG_COUN) = "1"
        SetInc(SAMP_COUN) = "1"
        'Initialize Increment Values
        SetUOM(MATHINPUT) = "1"
        SetUOM(TRIGGER_DELAY) = "S"
        SetUOM(TRIG_COUN) = "1"
        SetUOM(SAMP_COUN) = "1"
        'Initialize Resolution
        SetRes(MATHINPUT) = "1"
        'SetRes$(TRIGGER_DELAY) = "6"  'Resolution = 1 uSec
        SetRes(TRIG_COUN) = "1"
        SetRes(SAMP_COUN) = "1"
        'Initialize Current Value
        SetCur(MATHINPUT) = SetDef(MATHINPUT)
        SetCur(TRIGGER_DELAY) = SetDef(TRIGGER_DELAY)
        SetCur(TRIG_COUN) = SetDef(TRIG_COUN)
        SetCur(SAMP_COUN) = SetDef(SAMP_COUN)
        'Initialize Command
        SetCmd(MATHINPUT) = ""
        SetCmd(TRIGGER_DELAY) = ""
        SetCmd(TRIG_COUN) = ":TRIG:COUN "
        SetCmd(SAMP_COUN) = ":SAMP:COUN "
        'Initialize Range Message
        SetRngMsg(MATHINPUT) = "Min: 10MHz    Def: 4.255GHz    Max: 8.5GHz"
        SetRngMsg(TRIGGER_DELAY) = "Min: 0 Sec    Def: 0 Sec   Max: 3600 Sec"
        SetRngMsg(TRIG_COUN) = "Min: 1    Def: 1    Max: 50,000"
        SetRngMsg(SAMP_COUN) = "Min: 1    Def: 1    Max: 50,000"
        'Init Display
        frmRi4152A.txtMath.Text = ""
        frmRi4152A.txtTriggerDelay.Text = ""
        frmRi4152A.txtTriggerCount.Text = SetCur(TRIG_COUN)
        frmRi4152A.txtSampleCount.Text = SetCur(SAMP_COUN)
    End Sub

    Sub ChangeInstrumentMode()
        'DESCRIPTION:
        '   This Module adjusts the GUI with respect to user
        '   changes in instrument functions.
        'EXAMPLE:
        '   ChangeInstrumentMode

        'Disable Boxes
        frmRi4152A.cboAperture.Enabled = True
        frmRi4152A.cboNPLC.Enabled = True

        Select Case InstrumentMode
            Case 1
                'DCV
                FillRangeComboBox(VOLTAGE_RANGE_VALUE, 5)
                FillMathComboBox(6)
                FillApertureComboBox(Aperture_RANGE1_VALUE, 3)
                frmRi4152A.cboAperture.SelectedIndex = 2
            Case 2
                'ACV
                FillRangeComboBox(VOLTAGE_RANGE_VALUE, 5)
                FillMathComboBox(6)
                frmRi4152A.cboAperture.Items.Clear() : frmRi4152A.cboAperture.Enabled = False
                frmRi4152A.cboNPLC.Enabled = False
            Case 3
                'DCR
                FillRangeComboBox(VOLTAGE_RANGE_VALUE, 5)
                FillMathComboBox(2)
                FillApertureComboBox(Aperture_RANGE1_VALUE, 3)
                frmRi4152A.cboAperture.SelectedIndex = 2
            Case 4
                'DCA
                FillRangeComboBox(CURRENT_RANGE_VALUE, 4)
                FillMathComboBox(4)
                FillApertureComboBox(Aperture_RANGE1_VALUE, 3)
                frmRi4152A.cboAperture.SelectedIndex = 2
            Case 5
                'ACA
                FillRangeComboBox(CURRENT_RANGE_VALUE, 2)
                FillMathComboBox(4)
                frmRi4152A.cboAperture.Items.Clear() : frmRi4152A.cboAperture.Enabled = False
                frmRi4152A.cboNPLC.Enabled = False
            Case 6
                '2WR
                FillRangeComboBox(RESISTANCE_RANGE_VALUE, 7)
                FillMathComboBox(4)
                FillApertureComboBox(Aperture_RANGE1_VALUE, 3)
                frmRi4152A.cboAperture.SelectedIndex = 2
            Case 7
                'FREQ
                FillRangeComboBox(VOLTAGE_RANGE_VALUE, 5)
                FillMathComboBox(4)
                FillApertureComboBox(Aperture_RANGE2_VALUE, 2)
                frmRi4152A.cboAperture.SelectedIndex = 1
                frmRi4152A.cboNPLC.Enabled = False
            Case 8
                'PER
                FillRangeComboBox(VOLTAGE_RANGE_VALUE, 5)
                FillMathComboBox(4)
                FillApertureComboBox(Aperture_RANGE2_VALUE, 2)
                frmRi4152A.cboAperture.SelectedIndex = 1
                frmRi4152A.cboNPLC.Enabled = False
            Case 9
                '4WR
                FillRangeComboBox(RESISTANCE_RANGE_VALUE, 7)
                FillMathComboBox(4)
                FillApertureComboBox(Aperture_RANGE1_VALUE, 3)
                frmRi4152A.cboAperture.SelectedIndex = 2
        End Select
    End Sub

    Sub DisplayErrorMessage(ByVal ErrorStatus As Integer)
        '  'DESCRIPTION:
        '  '   This Module produces a text error message in response
        '  '   to a VISA error and reports to TIP if in TIP mode.
        '  'PARAMETERS:
        '  '   ErrorStatus& - The VISA error number.
        '  'EXAMPLE:
        '  '   DisplayErrorMessage (VisaFunctionReturnValue&)'''
        '
        'Dim A As Long 'Return Value
        'Dim ErrorText As String * 256 '256 Byte Error Text Buffer
        '
        '    'This Sub Routine is for Debugging Purposes
        '    A& = hpe1412a_errorMessage(instrumentHandle:=instrumentHandle&, errorCode:=ErrorStatus&, errorMessage:=ErrorText$)
        '    frmRi4152A.txtDmmDisplay.Text = "Error"
        '    MsgBox ErrorText$, vbExclamation, "VISA Error Message"
        '    If Strings.Left$(sTIP_Mode, 7) = "TIP_RUN" Then
        '        SetKey "TIPS", "STATUS", "Error from VISA: " &
        ''            conversion.hex$(ErrorStatus&) & " " & ErrorText$
        '        ExitDmm
        '    End If

    End Sub

    Sub ReturnInstrumentHandle()
        'DESCRIPTION:
        '   This Module closes a VISA instrument handle.
        'EXAMPLE:
        '   ReturnInstrumentHandle

        If LiveMode Then
            '       ErrorStatus& = hpe1412a_close(ByVal instrumentHandle&)
            ErrorStatus = atxml_Close()
        End If
    End Sub

    Sub SetINPutOptions()
        'DESCRIPTION:
        '   This Module Sets Instrument INP Subsystem Commands.
        'EXAMPLE:
        '   SetINPutOptions

        If frmRi4152A.optInputResistanceAuto.Checked Then
            'Auto Z ON
            SendScpiCommand("INP:IMP:AUTO ON")
        Else
            'Auto Z OFF
            SendScpiCommand("INP:IMP:AUTO OFF")
        End If
    End Sub

    Sub SetOUTPutOptions()
        'DESCRIPTION:
        '   This Module Sets Instrument OUTP Subsystem Commands.
        'EXAMPLE:
        '   SetOUTPutOptions

        Dim TRIGGER As Short 'User Trigger Choice
        Dim Send As String 'Command to sent instrument

        For TRIGGER = 0 To 7
            Send = "OUTP:TTLT" & TRIGGER & ":STAT "
            If OUTPUT_TRIGGER_STATE(TRIGGER) = True Then
                Send &= "ON"
            Else
                Send &= "OFF"
            End If

            'Don't append TTL Ooutputs that are set OFF to TIP CMD String
            If sTIP_Mode = "TIP_DESIGN" Then
                If InStr(Send, " ON") > 0 Then SendScpiCommand(Send)
            Else
                SendScpiCommand(Send)
            End If
        Next TRIGGER
    End Sub

    Sub SetTRIGgerOptions()
        'DESCRIPTION:
        '   This Module Sets Instrument TRIG Subsystem Commands.
        'EXAMPLE:
        '   SetTRIGgerOptions

        SendScpiCommand(TRIGGER & SAMPLE)

        If frmRi4152A.chkDelay.Checked Then
            'Auto Triggering
            SendScpiCommand("TRIG:DEL:AUTO ON")
        Else
            'Set User Trigger Value
            SendScpiCommand("TRIG:DEL " & SetCur(TRIGGER_DELAY))
        End If
    End Sub

    Function TranslateNPLCChoice(ByVal BoxText As String) As String
        TranslateNPLCChoice = ""
        'DESCRIPTION:
        '   This Module Sets the value that matches the user
        '    NPLC Combo Box Choice.
        'RETURNS:
        '    SCPI Value of user choice in SAIS instrument GUI
        'EXAMPLE:
        '   TranslateNPLCChoice

        Dim CommandInstruction As String 'User NPLC Choice

        Select Case BoxText
            Case "100 Cycles"
                CommandInstruction = "100"
            Case "10 Cycles"
                CommandInstruction = "10"
            Case "1 Cycle"
                CommandInstruction = "1"
            Case "0.2 Cycles"
                CommandInstruction = "0.2"
            Case "0.02 Cycles"
                CommandInstruction = "0.02"
            Case Else
                CommandInstruction = "100"
        End Select

        'Return Function Value
        TranslateNPLCChoice = CommandInstruction
    End Function

    Sub SetCALCulateOptions()
        'DESCRIPTION:
        '   This Module Sets Instrument CALC Subsystem Commands.
        'EXAMPLE:
        '   SetCALCulateOptions

        AverageMeas = 0

        Select Case frmRi4152A.cboMath.Text
            Case "None"
                SendScpiCommand("CALC:STAT OFF")
            Case "dBm"
                SendScpiCommand("CALC:STAT ON")
                SendScpiCommand("CALC:FUNC DBM")
                SendScpiCommand("CALC:DBM:REF " & frmRi4152A.txtMath.Text)
            Case "dB"
                SendScpiCommand("CALC:STAT ON")
                SendScpiCommand("CALC:FUNC DB")
                SendScpiCommand("CALC:DB:REF " & frmRi4152A.txtMath.Text)
            Case "Null"
                SendScpiCommand("CALC:STAT ON")
                SendScpiCommand("CALC:FUNC NULL")
            Case "Average"
                AverageMeas = 1
                SendScpiCommand("CALC:STAT ON")
                SendScpiCommand("CALC:FUNC AVER")
            Case "Min"
                AverageMeas = 2
                SendScpiCommand("CALC:STAT ON")
                SendScpiCommand("CALC:FUNC AVER")
            Case "Max"
                AverageMeas = 3
                SendScpiCommand("CALC:STAT ON")
                SendScpiCommand("CALC:FUNC AVER")
        End Select
    End Sub

    Sub SetSENSeOptions()
        'DESCRIPTION:
        '   This Module Sets Instrument SENS Subsystem Commands.
        'EXAMPLE:
        '   SetSENSeOptions
        'Dim Buffer As String
        'Select Function, Aperture, Number Of Power Line Cycles, and Ranging
        SendScpiCommand("*CLS")

        Select Case InstrumentMode
            Case 1
                'DCV
                SendScpiCommand("SENS:FUNC " & Q & "VOLT:DC" & Q) 'Function
                SendScpiCommand("SENS:" & "VOLT:DC" & ":APER " & Aperture) 'Aperture
                SendScpiCommand("SENS:" & "VOLT:DC" & ":NPLC " & NPLC) 'Number Of Power Line Cycles
                If RANGE = "AUTO" Then
                    SendScpiCommand("SENS:" & "VOLT:DC" & ":RANG:AUTO ON") 'Select AutoRange
                Else
                    SendScpiCommand("SENS:" & "VOLT:DC" & ":RANG:AUTO OFF") 'DeSelect AutoRange
                    SendScpiCommand("SENS:" & "VOLT:DC" & ":RANG " & RANGE) 'Select Range
                End If
            Case 2
                'ACV
                SendScpiCommand("SENS:FUNC " & Q & "VOLT:AC" & Q) 'Function
                If RANGE = "AUTO" Then
                    SendScpiCommand("SENS:" & "VOLT:AC" & ":RANG:AUTO ON") 'Select AutoRange
                Else
                    SendScpiCommand("SENS:" & "VOLT:AC" & ":RANG:AUTO OFF") 'DeSelect AutoRange
                    SendScpiCommand("SENS:" & "VOLT:AC" & ":RANG " & RANGE) 'Select Range
                End If
            Case 3
                'DC RATIO
                SendScpiCommand("SENS:FUNC " & Q & "VOLT:DC:RAT" & Q) 'Function
                SendScpiCommand("SENS:" & "VOLT:DC" & ":APER " & Aperture) 'Aperture
                SendScpiCommand("SENS:" & "VOLT:DC" & ":NPLC " & NPLC) 'Number Of Power Line Cycles
                If RANGE = "AUTO" Then
                    SendScpiCommand("SENS:" & "VOLT:DC" & ":RANG:AUTO ON") 'Select AutoRange
                Else
                    SendScpiCommand("SENS:" & "VOLT:DC" & ":RANG:AUTO OFF") 'DeSelect AutoRange
                    SendScpiCommand("SENS:" & "VOLT:DC" & ":RANG " & RANGE) 'Select Range
                End If
            Case 4
                'DCA
                SendScpiCommand("SENS:FUNC " & Q & "CURR:DC" & Q) 'Function
                SendScpiCommand("SENS:" & "CURR:DC" & ":APER " & Aperture) 'Aperture
                SendScpiCommand("SENS:" & "CURR:DC" & ":NPLC " & NPLC) 'Number Of Power Line Cycles
                If RANGE = "AUTO" Then
                    SendScpiCommand("SENS:" & "CURR:DC" & ":RANG:AUTO ON") 'Select AutoRange
                Else
                    SendScpiCommand("SENS:" & "CURR:DC" & ":RANG:AUTO OFF") 'DeSelect AutoRange
                    SendScpiCommand("SENS:" & "CURR:DC" & ":RANG " & RANGE) 'Select Range
                End If
            Case 5
                'ACA
                SendScpiCommand("SENS:FUNC " & Q & "CURR:AC" & Q) 'Function
                If RANGE = "AUTO" Then
                    SendScpiCommand("SENS:" & "CURR:AC" & ":RANG:AUTO ON") 'Select AutoRange
                Else
                    SendScpiCommand("SENS:" & "CURR:AC" & ":RANG:AUTO OFF") 'DeSelect AutoRange
                    SendScpiCommand("SENS:" & "CURR:AC" & ":RANG " & RANGE) 'Select Range
                End If
            Case 6
                '2WR
                SendScpiCommand("SENS:FUNC " & Q & "RES" & Q) 'Function
                SendScpiCommand("SENS:" & "RES" & ":APER " & Aperture) 'Aperture
                SendScpiCommand("SENS:" & "RES" & ":NPLC " & NPLC) 'Number Of Power Line Cycles
                If RANGE = "AUTO" Then
                    SendScpiCommand("SENS:" & "RES" & ":RANG:AUTO ON") 'Select AutoRange
                Else
                    SendScpiCommand("SENS:" & "RES" & ":RANG:AUTO OFF") 'DeSelect AutoRange
                    SendScpiCommand("SENS:" & "RES" & ":RANG " & RANGE) 'Select Range
                End If
            Case 7
                'FREQ
                SendScpiCommand("SENS:FUNC " & Q & "FREQ" & Q) 'Function
                SendScpiCommand("SENS:" & "FREQ" & ":APER " & Aperture) 'Aperture
                If RANGE = "AUTO" Then
                    SendScpiCommand("SENS:" & "FREQ:VOLT" & ":RANG:AUTO ON") 'Select AutoRange
                Else
                    SendScpiCommand("SENS:" & "FREQ:VOLT" & ":RANG:AUTO OFF") 'DeSelect AutoRange
                    SendScpiCommand("SENS:" & "FREQ:VOLT" & ":RANG " & RANGE) 'Select Range
                End If
            Case 8
                'PER
                SendScpiCommand("SENS:FUNC " & Q & "PER" & Q) 'Function
                SendScpiCommand("SENS:" & "PER" & ":APER " & Aperture) 'Aperture
                If RANGE = "AUTO" Then
                    SendScpiCommand("SENS:" & "PER:VOLT" & ":RANG:AUTO ON") 'Select AutoRange
                Else
                    SendScpiCommand("SENS:" & "PER:VOLT" & ":RANG:AUTO OFF") 'DeSelect AutoRange
                    SendScpiCommand("SENS:" & "PER:VOLT" & ":RANG " & RANGE) 'Select Range
                End If
            Case 9
                '4WR
                SendScpiCommand("SENS:FUNC " & Q & "FRES" & Q) 'Function
                SendScpiCommand("SENS:" & "FRES" & ":APER " & Aperture) 'Aperture
                SendScpiCommand("SENS:" & "FRES" & ":NPLC " & NPLC) 'Number Of Power Line Cycles
                If RANGE = "AUTO" Then
                    SendScpiCommand("SENS:" & "FRES" & ":RANG:AUTO ON") 'Select AutoRange
                Else
                    SendScpiCommand("SENS:" & "FRES" & ":RANG:AUTO OFF") 'DeSelect AutoRange
                    SendScpiCommand("SENS:" & "FRES" & ":RANG " & RANGE) 'Select Range
                End If
        End Select

        'Select AC Filter
        If frmRi4152A.optAcFilterFast.Checked = True Then
            'FAST AC FILTER
            SendScpiCommand("SENS:DET:BAND 200")
            sFilterDelay = 0 ' reset the delay when not in 3Hz filter
        End If
        If frmRi4152A.optAcFilterMedium.Checked = True Then
            'MEDIUM AC FILTER
            SendScpiCommand("SENS:DET:BAND 20")
            sFilterDelay = 0 ' reset the delay when not in 3Hz filter
        End If
        If frmRi4152A.optAcFilterSlow.Checked = True Then
            'SLOW AC FILTER
            SendScpiCommand("SENS:DET:BAND 3")
            sFilterDelay = 6 ' set the delay for 3Hz filter it's only capable of 1 read/7seconds
        End If

        'Select Auto Zero Mode
        If frmRi4152A.optAutoZeroOn.Checked = True Then
            'ON
            SendScpiCommand("SENS:ZERO:AUTO ON")
        End If
        If frmRi4152A.optAutoZeroOff.Checked = True Then
            'OFF
            SendScpiCommand("SENS:ZERO:AUTO OFF")
        End If
        If frmRi4152A.optAutoZeroOnce.Checked = True Then
            'ONCE
            SendScpiCommand("SENS:ZERO:AUTO ONCE")
        End If
    End Sub

    Sub TakeMeasurement()
        'DESCRIPTION:
        '   This Module Takes A Measurement From the DMM.
        'EXAMPLE:
        '   TakeMeasurement

        'Dim SendVXI As String 'Command String To Send To The Instrument
        Dim ReadBuffer As String 'Formatted Measurement Value
        Dim DisplayUnit As String = "" 'Unit Corresponding to measurement
        'Dim nReturnValue As Integer
        Dim nTimeOut As Integer
        Dim i As Short
        Dim iDelay As Short

        'Disable Measure During Continuous Mode
        If frmRi4152A.chkContinuous.Checked = True Then
            frmRi4152A.cmdMeasure.Enabled = False
        End If

        If sTIP_Mode = "TIP_RUNPERSIST" Or sTIP_Mode = "TIP_RUNSETUP" Then
            'Program Instrument per saved TIP settings settings
            SendScpiCommand(sTIP_CMDstring)
            If Not bGetInstrumentStatus() Then Exit Sub
        Else
            'Program Instrument per SAIS settings
            SetSENSeOptions()
            If Not bGetInstrumentStatus() Then Exit Sub
            SetINPutOptions()
            If Not bGetInstrumentStatus() Then Exit Sub
            SetCALCulateOptions()
            If Not bGetInstrumentStatus() Then Exit Sub
            SetOUTPutOptions()
            If Not bGetInstrumentStatus() Then Exit Sub
            SetTRIGgerOptions()
            If Not bGetInstrumentStatus() Then Exit Sub
        End If

        'Format The Display Units According To Instrument Mode
        Select Case InstrumentMode
            Case 1, 2
                'DCV 'ACV
                DisplayUnit = "Volts"
            Case 3
                'DCRAT
                DisplayUnit = ""
            Case 4, 5
                'DCA 'ACA
                DisplayUnit = "Amps"
            Case 6, 9
                '2WR '4WR
                DisplayUnit = "Ohms"
            Case 7
                'FREQ
                DisplayUnit = "Hertz"
            Case 8
                'PER
                DisplayUnit = "Seconds"
        End Select

        'Format The Display Units According To Math Mode
        Select Case frmRi4152A.cboMath.Text
            Case "dBm"
                DisplayUnit = "dBm"
            Case "dB"
                DisplayUnit = "dB"
            Case "Null"
                'Take reference reading for NULL Offset value. If an error, will be because
                'an over-range exists (-504). Prompt user to correct.
                If sTIP_Mode = "TIP_RUNSETUP" Then
                    MessageBox.Show("Connect the DMM for the NULL reference reading.  " & "Click OK when ready to continue.", "4152A", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    SendScpiCommand("INIT") 'Take Reference reading (Value is stored in DMM Null Register)
                    If bGetInstrumentStatus() Then
                        MessageBox.Show("The NULL reference reading is complete.  " & "Connect the DMM for the NULL reading.  " & "Click OK when ready to continue.", "4152A", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        MessageBox.Show("Verify the DMM is properly connected for the reference reading and " & "that a Range value has been selected that will not create an " & "over-range condition.", "4152A", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        ResetDigitalMultimeter()
                        Exit Sub
                    End If
                Else
                    SendScpiCommand("INIT") 'Take Reference reading (Value is stored in DMM Null Register)
                    If Not bGetInstrumentStatus() Then
                        MessageBox.Show("Verify the DMM is properly connected for the reference reading and " & "that a Range value has been selected that will not create an " & "over-range condition", "4152A", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        ResetDigitalMultimeter()
                        Exit Sub
                    End If
                    If Not bTIP_Running Then
                        frmRi4152A.chkContinuous.Checked = True
                        frmRi4152A.cmdMeasure.Enabled = False
                    End If
                End If
                DisplayUnit &= " (Null)"
            Case "Average"
                DisplayUnit &= " (Avg.)"
            Case "Min"
                DisplayUnit &= " (Min.)"
            Case "Max"
                DisplayUnit &= " (Max.)"
        End Select

        If LiveMode Then
            If Val(SetCur(TRIGGER_DELAY)) > Val(SetDef(TRIGGER_DELAY)) Then
                iDelay = CInt(SetCur(TRIGGER_DELAY))
                'Read default VISA Timeout
                'nReturnValue = viGetAttribute(instrumentHandle&, VI_ATTR_TMO_VALUE, nTimeOut)
                'Set VISA Time Out to DEFAULT + TRIGGER DELAY length
                nTimeOut += (1000 * Val(SetCur(TRIGGER_DELAY)))
                'nReturnValue = viSetAttribute(instrumentHandle&, VI_ATTR_TMO_VALUE, nTimeOut)
            End If
        End If

        'Continuous Operation Loop Code
        Do
            SendScpiCommand("READ?")
            'SendScpiCommand "INIT"

            ' if the 3Hz filter is used we must have a delay, it's capable of 1 read/7 seconds
            If sFilterDelay > 0 Then
                iDelay = sFilterDelay
            End If

            If iDelay > 1 Then
                For i = iDelay To 1 Step -1
                    frmRi4152A.sbrUserInformation.Panels(1 - 1).Text = "Delay: " & Conversion.Str(i) & " Seconds"
                    Delay(1)
                Next i
            End If
            If RANGE = "AUTO" Then
                frmRi4152A.txtDmmDisplay.Text = "Reading... Please Wait"
                frmRi4152A.sbrUserInformation.Panels(1 - 1).Text = "Auto Ranging... May take up to " & Conversion.Str(iDelay) & " seconds"
            Else
                Delay(0.2)
                frmRi4152A.txtDmmDisplay.Text = "Reading..."
            End If
            'SendScpiCommand "FETC?"

            ReadBuffer = ReadInstrumentBuffer()
            Select Case AverageMeas
                Case 1
                    SendScpiCommand("CALC:AVER:AVER?")
                    ReadBuffer = ReadInstrumentBuffer()
                Case 2
                    SendScpiCommand("CALC:AVER:MIN?")
                    ReadBuffer = ReadInstrumentBuffer()
                Case 3
                    SendScpiCommand("CALC:AVER:MAX?")
                    ReadBuffer = ReadInstrumentBuffer()
            End Select

            ReadBuffer = StripNullCharacters(ReadBuffer)
            sTIP_Measured = Strings.Format(CDbl(Val(ReadBuffer)), "0.#########") 'Absolute value is passed to TIP Studio
            'Format And Display Measurement
            If Strings.Mid(ReadBuffer, 1, 12) = "+9.90000E+37" Then
                sTIP_Measured = Strings.Left(sTIP_Measured, 12)
                ReadBuffer = "Out Of Range: "
            End If
            If Not (DisplayUnit = "") Then
                If (Len(ReadBuffer) >= 2) Then
                    frmRi4152A.txtDmmDisplay.Text = EngNotate(Val(ReadBuffer), 7, DisplayUnit)
                Else
                    frmRi4152A.txtDmmDisplay.Text = "0.0 " & DisplayUnit
                End If
            Else
                If ReadBuffer = "Out Of Range: " Then
                    frmRi4152A.txtDmmDisplay.Text = "+9.9 E+37"
                Else
                    frmRi4152A.txtDmmDisplay.Text = Strings.Format(Val(ReadBuffer), "0.0####")
                End If
            End If

            frmRi4152A.Refresh()
            Application.DoEvents()
        Loop Until frmRi4152A.chkContinuous.Checked = False Or Not bGetInstrumentStatus()
    End Sub

    Sub FillMathComboBox(ByVal NumberOfItems As Short)
        'DESCRIPTION:
        '   This Module fills the Math combo box with a specified
        '   number of elements.
        'EXAMPLE:
        '   FillMathComboBox ItemsToPutInComboBox%

        Dim MathFunctionInitLoop As Short 'Array Index

        'Empty Math ComboBox
        frmRi4152A.cboMath.Items.Clear()

        'Fill With Specified Number of array elements necessary to Instrument Function
        '        For MathFunctionInitLoop = 0 To NumberOfItems
        '            frmRi4152A.cboMath.Items.Insert(0, MATH_FUNCTION_VALUE(MathFunctionInitLoop))
        '        Next MathFunctionInitLoop

        For MathFunctionInitLoop = NumberOfItems To 0 Step -1
            frmRi4152A.cboMath.Items.Add(MATH_FUNCTION_VALUE(MathFunctionInitLoop))
        Next MathFunctionInitLoop
        frmRi4152A.cboMath.SelectedIndex = NumberOfItems
    End Sub

    Sub FillRangeComboBox(ByRef sArray() As String, ByVal NumberOfItems As Short)
        'DESCRIPTION:
        '   This Module fills the Range combo box with a specified
        '   number of elements defined in a string array argument.
        'EXAMPLE:
        '   FillRangeComboBox RangeArray$(), ItemsInComboBox%

        Dim RangeFunctionInitLoop As Short 'Array Index

        'Empty Range ComboBox
        frmRi4152A.cboRange.Items.Clear()

        'Fill With Specified Number of array elements necessary to Instrument Function
        '        For RangeFunctionInitLoop = 0 To NumberOfItems
        '            frmRi4152A.cboRange.Items.Insert(0, sArray(RangeFunctionInitLoop))
        '        Next RangeFunctionInitLoop

        For RangeFunctionInitLoop = NumberOfItems To 0 Step -1
            frmRi4152A.cboRange.Items.Add(sArray(RangeFunctionInitLoop))
        Next RangeFunctionInitLoop
        frmRi4152A.cboRange.SelectedIndex = NumberOfItems
    End Sub

    Sub FillTriggerOutputComboBox(ByRef sArray() As String)
        'DESCRIPTION:
        '   This Module fills the Trigger combo box with a specified
        '   number of elements defined in a string array argument.
        'EXAMPLE:
        '   FillTriggerOutputComboBox ComboBoxArray()$

        Dim TriggerOutputInitLoop As Short 'Array Index

        'Empty ComboBox
        frmRi4152A.cboTriggerOutput.Items.Clear()

        'Fill With Specified Number of array elements necessary to Instrument Function
        For TriggerOutputInitLoop = 3 To 10
            frmRi4152A.cboTriggerOutput.Items.Insert(0, sArray(TriggerOutputInitLoop))
        Next TriggerOutputInitLoop

        'For TriggerOutputInitLoop = NumberOfItems To 0 Step -1
        '    frmRi4152A.cboTriggerOutput.Items.Add(sArray(TriggerOutputInitLoop))
        'Next TriggerOutputInitLoop
        frmRi4152A.cboTriggerOutput.SelectedIndex = 0
    End Sub

    Sub FillTriggerSourceComboBox(ByRef sArray() As String, ByVal NumberOfItems As Short)
        'DESCRIPTION:
        '   This Module fills the Trigger Source combo box with a specified
        '   number of elements defined in a string array argument.
        'EXAMPLE:
        '   FillTriggerSourceComboBox Range$(), ItemsInComboBox%

        Dim TriggerSourceInitLoop As Short 'Array Index

        'Empty Range ComboBox
        frmRi4152A.cboTriggerSource.Items.Clear()

        'Fill With Specified Number of array elements necessary to Instrument Function
        For TriggerSourceInitLoop = 0 To NumberOfItems
            frmRi4152A.cboTriggerSource.Items.Insert(0, sArray(TriggerSourceInitLoop))
        Next TriggerSourceInitLoop

        'For TriggerSourceInitLoop = NumberOfItems To 0 Step -1
        '    frmRi4152A.cboTriggerSource.Items.Add(sArray(TriggerSourceInitLoop))
        'Next TriggerSourceInitLoop
        frmRi4152A.cboTriggerSource.SelectedIndex = NumberOfItems
    End Sub

    Sub FillApertureComboBox(ByRef sArray() As String, ByVal NumberOfItems As Short)
        'DESCRIPTION:
        '   This Module fills the Aperature combo box with a specified
        '   number of elements defined in a string array argument.
        'EXAMPLE:
        '   FillApertureComboBox Range$(), ItemsInComboBox%

        Dim InitLoop As Short 'Array Index

        'Empty Range ComboBox
        frmRi4152A.cboAperture.Items.Clear()
        'Fill With Specified Number of array elements necessary to Instrument Function
        '        For InitLoop = 0 To NumberOfItems
        '            frmRi4152A.cboAperture.Items.Insert(0, sArray(InitLoop))
        '        Next InitLoop

        For InitLoop = NumberOfItems To 0 Step -1
            frmRi4152A.cboAperture.Items.Add(sArray(InitLoop))
        Next InitLoop
    End Sub
    Sub FillNPLCComboBox(ByRef sArray() As String, ByVal NumberOfItems As Short)
        'DESCRIPTION:
        '   This Module fills the NPLC combo box with a specified
        '   number of elements defined in a string array argument.
        'EXAMPLE:
        '   FillNPLCComboBox Range$(), ItemsInComboBox%

        Dim InitLoop As Short 'Array Index

        'Empty Range ComboBox
        frmRi4152A.cboNPLC.Items.Clear()
        'Fill With Specified Number of array elements necessary to Instrument Function
        '        For InitLoop = 0 To NumberOfItems
        '            frmRi4152A.cboNPLC.Items.Insert(0, sArray(InitLoop))
        '        Next InitLoop

        For InitLoop = NumberOfItems To 0 Step -1
            frmRi4152A.cboNPLC.Items.Add(sArray(InitLoop))
        Next InitLoop
    End Sub

    Function TranslateRangeChoice(ByVal BoxText As String) As String
        TranslateRangeChoice = ""
        'DESCRIPTION:
        '   This Module evaluates a user combo box and returns
        '   a string value to be appended to a SCPI command.
        'EXAMPLE:
        '   ScpiValue$ = TranslateRangeChoice TextInRangeBox$

        Dim CommandInstruction As String 'Command to append to SCPI value

        Select Case BoxText
            Case "AUTO Range"
                CommandInstruction = "AUTO"
            Case "300 Volt"
                CommandInstruction = "300"
            Case "100 Volt"
                CommandInstruction = "100"
            Case "10 Volt"
                CommandInstruction = "10"
            Case "1 Volt"
                CommandInstruction = "1"
            Case "0.1 Volt"
                CommandInstruction = "0.1"
            Case "3 Amp"
                CommandInstruction = "3"
            Case "1 Amp"
                CommandInstruction = "1"
            Case "0.1 Amp"
                CommandInstruction = "0.1"
            Case "0.01 Amp"
                CommandInstruction = "0.01"
            Case "100 M Ohm"
                CommandInstruction = "100000000"
            Case "10 M Ohm"
                CommandInstruction = "10000000"
            Case "1 M Ohm"
                CommandInstruction = "1000000"
            Case "100 k Ohm"
                CommandInstruction = "100000"
            Case "10 k Ohm"
                CommandInstruction = "10000"
            Case "1 k Ohm"
                CommandInstruction = "1000"
            Case "100 Ohm"
                CommandInstruction = "100"
            Case Else
                CommandInstruction = ""
        End Select

        'Return Function Value
        TranslateRangeChoice = CommandInstruction
    End Function

    Function TranslateApertureChoice(ByVal BoxText As String) As String
        TranslateApertureChoice = ""
        'DESCRIPTION:
        '   This Module evaluates a user combo box and returns
        '   a string value to be appended to a SCPI command.
        'EXAMPLE:
        '   ScpiValue$ = TranslateApertureChoice TextInRangeBox$

        Dim CommandInstruction As String 'SCPI Command Value

        Select Case BoxText
            Case "1.67 s"
                CommandInstruction = "1.66667" '1.67 exceeds range, generates an ERROR
            Case "167 ms"
                CommandInstruction = "167E-3"
            Case "16.7 ms"
                CommandInstruction = "16.7E-3"
            Case "3.33 ms"
                CommandInstruction = "3.33E-3"
            Case ".333 ms"
                CommandInstruction = "3.33E-2"
            Case "1 s"
                CommandInstruction = "1"
            Case "0.1 s"
                CommandInstruction = "0.1"
            Case "0.01 s"
                CommandInstruction = "0.01"
            Case Else
                CommandInstruction = ""
        End Select

        'Return Function Value
        TranslateApertureChoice = CommandInstruction
    End Function

    Function TranslateTriggerChoice(ByVal BoxText As String) As String
        TranslateTriggerChoice = ""
        'DESCRIPTION:
        '   This Module evaluates a user combo box and returns
        '   a string value to be appended to a SCPI command.
        'EXAMPLE:
        '   ScpiValue$ = TranslateTriggerChoice TextInRangeBox$

        Dim CommandInstruction As String 'SCPI Command Value

        Select Case BoxText
            Case "TTL Trigger 7"
                CommandInstruction = "TRIG:SOUR TTLT7"
            Case "TTL Trigger 6"
                CommandInstruction = "TRIG:SOUR TTLT6"
            Case "TTL Trigger 5"
                CommandInstruction = "TRIG:SOUR TTLT5"
            Case "TTL Trigger 4"
                CommandInstruction = "TRIG:SOUR TTLT4"
            Case "TTL Trigger 3"
                CommandInstruction = "TRIG:SOUR TTLT3"
            Case "TTL Trigger 2"
                CommandInstruction = "TRIG:SOUR TTLT2"
            Case "TTL Trigger 1"
                CommandInstruction = "TRIG:SOUR TTLT1"
            Case "TTL Trigger 0"
                CommandInstruction = "TRIG:SOUR TTLT0"
            Case "Immediate Trigger"
                CommandInstruction = "TRIG:SOUR IMM"
            Case "External Trigger"
                CommandInstruction = "TRIG:SOUR EXT"
            Case "VXI Bus Trigger"
                CommandInstruction = "TRIG:SOUR BUS"
            Case Else
                CommandInstruction = ""
        End Select

        If BoxText <> "Immediate Trigger" Then
            CommandInstruction += ";:TRIG:COUN " + SetCur(TRIG_COUN)
        End If
        'Return Function Value
        TranslateTriggerChoice = CommandInstruction
    End Function

    Function VerifyInstrumentCommunication() As Boolean
        'DESCRIPTION:
        '   This Module Verifies Communication With Message Delivery System.
        'EXAMPLE:
        '   LiveMode% = VerifyInstrumentCommunication%

        Dim Pass As Boolean 'Instrument Pass Test (True/False)
        Dim VisaLibrary As String 'VISA Library File Path
        Dim Status As Integer
        Dim XmlBuf As String
        Dim Allocation As String
        Dim Response As String
        'Dim COUNT As Integer

        'error 49 with this not commented out
        'Response = Strings.Space$(4096)

        Pass = False 'Until Proven True
        ErrorStatus = 1

        VisaLibrary = Environment.SystemDirectory & "\VISA32.DLL"

        Response = Strings.Space(4096)

        Try ' On Error GoTo ErrHandle
            '    If FileExists(VisaLibrary$) Then
            'Determine If The DMM Is Functioning
            '        ErrorStatus& = hpe1412a_init(resourceName:="VXI::44", IDQuery:=1, resetDevice:=1, instrumentHandle:=instrumentHandle&)

            '       Hard-Coded break point (remove before shipping)
            '        Stop
            Status = atxml_Initialize(ProcType, guid)

            'WMT -- start of test routines:

            '        Stop
            '                          1         2         3         4         5         6
            '                 123456789012345678901234567890123456789012345678901234567890
            XmlBuf = "<AtXmlTestRequirements>" & _
                      "    <ResourceRequirement>" & _
                      "        <ResourceType>Source</ResourceType>" & _
                      "        <SignalResourceName>DMM_1</SignalResourceName> " & _
                      "    </ResourceRequirement> " & _
                      "</AtXmlTestRequirements>"            '

            '       Hard-Coded break point (remove before shipping)
            'Stop
            '
            Allocation = Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\ATS\", "PAWSAllocationPath", Nothing)
            '
            '
            '     ErrorStatus& = atxml_ValidateRequirements(XmlBuf, Allocation, wmt_Response, 4096)
            Status = atxml_ValidateRequirements(XmlBuf, Allocation, Response, 4096)

            'Parse Availability XML string to get the status(Mode) of the instrument
            iStatus = gctlParse.ParseAvailiability(Response)

            ' ErrorStatus& = 0
            '
            If Status <> 0 Then
                MessageBox.Show("The DMM Is Not Responding.  Live Mode Disabled.", "4152A", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                Pass = True
            End If
            'wmt    Else
            'wmt        MsgBox "VISA Message Delivery System Is Not Installed. Live Mode Disabled.", vbExclamation
            'wmt    End If

            '    'Return Function Value
            VerifyInstrumentCommunication = Pass
        Catch   ' ErrHandle:
            If Err.Number = 53 Then
                MessageBox.Show("unable to locate AtXmlApi.dll", "4152A", MessageBoxButtons.OK, MessageBoxIcon.Error)
                MsgBox("unable to locate AtXmlApi.dll", MsgBoxStyle.Critical)
            Else
                Debug.Print("Description :" & vbTab & Err.Description)
                Debug.Print("Number:" & vbTab & Str(Err.Number))
            End If

            VerifyInstrumentCommunication = False
        End Try
    End Function

    Function StripNullCharacters(ByVal Parsed As String) As String
        StripNullCharacters = ""
        'DESCRIPTION:
        '   This Module removes null characters from a sting.
        'EXAMPLE:
        '   StringWithoutNull = StripNullCharacters(StringNull$)

        Dim X As Integer 'String Character Position

        For X = 1 To Parsed.Length '32 - 126
            If Asc(Strings.Mid(Parsed, X, 1)) < 32 Then
                Exit For
            End If
        Next X

        'Return Function Value
        StripNullCharacters = Strings.Left(Parsed, X - 1)
    End Function

    Function FileExists(ByVal path As String) As Boolean
        'DESCRIPTION:
        '   This Module Checks To See If A Disk File Exists.
        'EXAMPLE:
        '   IsFileThere% = FileExists("C:\ISFILE.EXE")
        'RETURNS:
        '   TRUE if File is present
        '   FALSE if File is not present

        Dim X As Integer

        X = FreeFile()

        On Error Resume Next
        FileOpen(X, Convert.ToString(path), OpenMode.Input)
        If Err.Number = 0 Then
            FileExists = True
        Else
            FileExists = False
        End If
        FileClose(X)
    End Function

    Sub InitializeArrays()
        'DESCRIPTION:
        '   This Module Generates The Arrays and Character
        '   constants used in the SAIS Front Panel.
        'EXAMPLE:
        '   InitializeArrays

        'DMM VISA Library Resource Name
        ResourceName = "DMM_1" '    ResourceName$ = "VXI::44"
        Q = Strings.Chr(34)
        'Voltage Range Combo Box
        VOLTAGE_RANGE_VALUE(0) = "AUTO Range" 'Default Value
        VOLTAGE_RANGE_VALUE(1) = "300 Volt"
        VOLTAGE_RANGE_VALUE(2) = "100 Volt"
        VOLTAGE_RANGE_VALUE(3) = "10 Volt"
        VOLTAGE_RANGE_VALUE(4) = "1 Volt"
        VOLTAGE_RANGE_VALUE(5) = "0.1 Volt"
        'Current Range Combo Box
        CURRENT_RANGE_VALUE(0) = "AUTO Range"
        CURRENT_RANGE_VALUE(1) = "3 Amp"
        CURRENT_RANGE_VALUE(2) = "1 Amp"
        CURRENT_RANGE_VALUE(3) = "0.1 Amp"
        CURRENT_RANGE_VALUE(4) = "0.01 Amp"
        'Resistance Range Combo Box
        RESISTANCE_RANGE_VALUE(0) = "AUTO Range"
        RESISTANCE_RANGE_VALUE(1) = "100 M Ohm"
        RESISTANCE_RANGE_VALUE(2) = "10 M Ohm"
        RESISTANCE_RANGE_VALUE(3) = "1 M Ohm"
        RESISTANCE_RANGE_VALUE(4) = "100 k Ohm"
        RESISTANCE_RANGE_VALUE(5) = "10 k Ohm"
        RESISTANCE_RANGE_VALUE(6) = "1 k Ohm"
        RESISTANCE_RANGE_VALUE(7) = "100 Ohm"
        'Trigger Options Combo Box
        TRIGGER_SOURCE_VALUE(0) = "Immediate Trigger"
        TRIGGER_SOURCE_VALUE(1) = "External Trigger"
        TRIGGER_SOURCE_VALUE(2) = "VXI Bus Trigger"
        TRIGGER_SOURCE_VALUE(3) = "TTL Trigger 7"
        TRIGGER_SOURCE_VALUE(4) = "TTL Trigger 6"
        TRIGGER_SOURCE_VALUE(5) = "TTL Trigger 5"
        TRIGGER_SOURCE_VALUE(6) = "TTL Trigger 4"
        TRIGGER_SOURCE_VALUE(7) = "TTL Trigger 3"
        TRIGGER_SOURCE_VALUE(8) = "TTL Trigger 2"
        TRIGGER_SOURCE_VALUE(9) = "TTL Trigger 1"
        TRIGGER_SOURCE_VALUE(10) = "TTL Trigger 0"
        'Aperture Range1 Combo Box
        Aperture_RANGE1_VALUE(0) = "1.67 s"
        Aperture_RANGE1_VALUE(1) = "167 ms"
        Aperture_RANGE1_VALUE(2) = "16.7 ms"
        Aperture_RANGE1_VALUE(3) = "3.33 ms"
        ''Aperture_RANGE1_VALUE$(4) = ".333 ms"
        'Aperture Range2 Combo Box
        Aperture_RANGE2_VALUE(0) = "1 s"
        Aperture_RANGE2_VALUE(1) = "0.1 s"
        Aperture_RANGE2_VALUE(2) = "0.01 s"
        'NPLC Combo Box
        NPLC_RANGE_VALUE(0) = "100 Cycles"
        NPLC_RANGE_VALUE(1) = "10 Cycles"
        NPLC_RANGE_VALUE(2) = "1 Cycle"
        NPLC_RANGE_VALUE(3) = "0.2 Cycles"
        ''NPLC_RANGE_VALUE$(4) = "0.02 Cycles"
        'Math Combo Box
        MATH_FUNCTION_VALUE(0) = "None"
        MATH_FUNCTION_VALUE(1) = "Average"
        MATH_FUNCTION_VALUE(2) = "Min"
        MATH_FUNCTION_VALUE(3) = "Max"
        MATH_FUNCTION_VALUE(4) = "Null"
        MATH_FUNCTION_VALUE(5) = "dB"
        MATH_FUNCTION_VALUE(6) = "dBm"

        '--------------TIP CMD STRINGS-------------
        SetCmd(SENS_FUNC) = ":SENS:FUNC "
        SetCmd(SENS_CURR_AC_RANG_AUTO) = ":SENS:CURR:AC:RANG:AUTO "
        SetCmd(SENS_CURR_DC_RANG_AUTO) = ":SENS:CURR:DC:RANG:AUTO "
        SetCmd(SENS_FREQ_VOLT_RANG_AUTO) = ":SENS:FREQ:VOLT:RANG:AUTO "
        SetCmd(SENS_FREQ_VOLT_RANG) = ":SENS:FREQ:VOLT:RANG "
        SetCmd(SENS_FRES_RANG_AUTO) = ":SENS:FRES:RANG:AUTO "
        SetCmd(SENS_PER_VOLT_RANG_AUTO) = ":SENS:PER:VOLT:RANG:AUTO "
        SetCmd(SENS_PER_VOLT_RANG) = ":SENS:PER:VOLT:RANG "
        SetCmd(SENS_VOLT_AC_RANG_AUTO) = ":SENS:VOLT:AC:RANG:AUTO "
        SetCmd(SENS_VOLT_DC_RANG_AUTO) = ":SENS:VOLT:DC:RANG:AUTO "
        SetCmd(SENS_RES_RANG_AUTO) = ":SENS:RES:RANG:AUTO "
        SetCmd(SENS_ZERO_AUTO) = ":SENS:ZERO:AUTO "
        SetCmd(SENS_DET_BAND) = ":SENS:DET:BAND "
        SetCmd(SENS_VOLT_DC_RANG) = ":SENS:VOLT:DC:RANG "
        SetCmd(SENS_VOLT_AC_RANG) = ":SENS:VOLT:AC:RANG "
        SetCmd(SENS_RES_RANG) = ":SENS:RES:RANG "
        SetCmd(SENS_FRES_RANG) = ":SENS:FRES:RANG "
        SetCmd(SENS_CURR_DC_RANG) = ":SENS:CURR:DC:RANG "
        SetCmd(SENS_CURR_AC_RANG) = ":SENS:CURR:AC:RANG "
        SetCmd(SENS_VOLT_DC_APER) = ":SENS:VOLT:DC:APER "
        SetCmd(SENS_CURR_DC_APER) = ":SENS:CURR:DC:APER "
        SetCmd(SENS_RES_APER) = ":SENS:RES:APER "
        SetCmd(SENS_FRES_APER) = ":SENS:FRES:APER "
        SetCmd(SENS_FREQ_APER) = ":SENS:FREQ:APER "
        SetCmd(SENS_PER_APER) = ":SENS:PER:APER "
        SetCmd(SENS_VOLT_DC_NPLC) = ":SENS:VOLT:DC:NPLC "
        SetCmd(SENS_CURR_DC_NPLC) = ":SENS:CURR:DC:NPLC "
        SetCmd(SENS_RES_NPLC) = ":SENS:RES:NPLC "
        SetCmd(SENS_FRES_NPLC) = ":SENS:FRES:NPLC "

        SetCmd(INP_IMP_AUTO) = ":INP:IMP:AUTO "

        SetCmd(OUTP_TTLT0_STAT) = ":OUTP:TTLT0:STAT "
        SetCmd(OUTP_TTLT1_STAT) = ":OUTP:TTLT1:STAT "
        SetCmd(OUTP_TTLT2_STAT) = ":OUTP:TTLT2:STAT "
        SetCmd(OUTP_TTLT3_STAT) = ":OUTP:TTLT3:STAT "
        SetCmd(OUTP_TTLT4_STAT) = ":OUTP:TTLT4:STAT "
        SetCmd(OUTP_TTLT5_STAT) = ":OUTP:TTLT5:STAT "
        SetCmd(OUTP_TTLT6_STAT) = ":OUTP:TTLT6:STAT "
        SetCmd(OUTP_TTLT7_STAT) = ":OUTP:TTLT7:STAT "

        SetCmd(TRIG_DEL_AUTO) = ":TRIG:DEL:AUTO "
        SetCmd(TRIG_SOUR) = ":TRIG:SOUR "
        SetCmd(TRIG_DEL) = ":TRIG:DEL "

        SetCmd(CALC_STAT) = ":CALC:STAT "
        SetCmd(CALC_FUNC) = ":CALC:FUNC "
        SetCmd(CALC_DB_REF) = ":CALC:DB:REF "
        SetCmd(CALC_DBM_REF) = ":CALC:DBM:REF "
    End Sub

    Public Sub Main()
        'DESCRIPTION:
        '   This Module is the program entry (starting) point.

        Dim sCmdLine As String
        Dim OutputTriggerInitIndex As Short 'Array Index
        sFilterDelay = 0 ' ensure that the filter delay is not set

        'For Debugging Information Uncomment Line Below
        '------------------------------------------------
        'DebugDwh% = True
        DebugDwh = False
        '------------------------------------------------
        QuitProgram = False
        'Get command line arguments to determine if invoked by TIP Studio
        sCmdLine = Microsoft.VisualBasic.Command()
        sTIP_Mode = Strings.Trim(Strings.UCase(sCmdLine))
        If InStr(sTIP_Mode, "TIP") Then bTIP_Running = True
        'Show Introduction Form
        'ShowSplash "Digital Multimeter", "VX4234"
        frmParent = frmRi4152A 'Name of  Main Form
        If Not bTIP_Running Then
            SplashStart()
            Delay(0.5)
        End If

        'Check System and Instrument For Errors
        LiveMode = True
        LiveMode = VerifyInstrumentCommunication()

        'Set-Up Main Form
        InitializeArrays()
        InitInputBoxLists()

        If Not bTIP_Running Then
            CenterForm(frmRi4152A)
            'Show the Front Panel
            frmRi4152A.Show()
            frmRi4152A.Refresh()
            'Reset to Power-On Defaults

            '       Disable instrument reset on GUI load per Requirement
            'ResetDigitalMultimeter 'DRB EADS
            'Measurement Function: Dc Voltage
            frmRi4152A.tbrDmmFunctions.Buttons(0).Pushed = True
            InstrumentMode = 1
            'Range : AUTO
            FillRangeComboBox(VOLTAGE_RANGE_VALUE, 5)

            'Math Functions : Disabled (None)
            FillMathComboBox(4)
            'Math Input Box :Disabled
            'DisableEnableUserEntryBoxSpin MATH, False
            frmRi4152A.panMath.Visible = False
            frmRi4152A.spnMath.Visible = False
            'Auto Zero: ON
            frmRi4152A.optAutoZeroOn.Checked = True
            'AC Filter: Medium Frequency
            frmRi4152A.optAcFilterMedium.Checked = True
            'Auto Impedance: Off
            frmRi4152A.optInputResistance10MOhm.Checked = True
            'Output Triggers : Off
            For OutputTriggerInitIndex = 0 To 7
                OUTPUT_TRIGGER_STATE(OutputTriggerInitIndex) = False
            Next OutputTriggerInitIndex
            frmRi4152A.cboTriggerOutput.SelectedIndex = 0
            frmRi4152A.optTriggerOutputOff.Checked = True

            'Source Trigger : Immediate
            frmRi4152A.cboTriggerSource.SelectedIndex = 10
            'Trigger Delay : AUTO ON
            frmRi4152A.chkDelay.Checked = True
            'DisableEnableUserEntryBoxSpin TRIGGER_DELAY, False
            frmRi4152A.panTriggerDelay.Visible = False
            ' ?? frmRi4152A.panTdUnit.Visible = False


            SetCur(TRIGGER_DELAY) = "0"

            'APER: 0.1 Seconds, 0.166667 (60Hz), 0.2 (50Hz), 100ms
            FillApertureComboBox(Aperture_RANGE1_VALUE, 3)
            frmRi4152A.cboAperture.SelectedIndex = 2

            'Set / Re-Set Display To Ready State
            frmRi4152A.txtDmmDisplay.Text = "Ready"
            frmRi4152A.tabUserOptions.TabIndex = 0
            'Set / Re-Set Continuous Box To Ready State
            frmRi4152A.chkContinuous.Checked = False
            'Set / Re-Set Status Bar To Ready State
            'SendStatusBarMessage "HP E1412A Digital Multimeter Ready For Measurement"

            SplashEnd() 'Remove Introduction Form
        Else
            'Find Windows Directory
            sWindowsDir = Environment.GetFolderPath(Environment.SpecialFolder.Windows)
            'Position SAIS in upper Righthand corner of screen
            frmRi4152A.Top = 0
            frmRi4152A.Left = PrimaryScreen.Bounds.Width - frmRi4152A.Width
            'Show the Front Panel
            frmRi4152A.Show()
            frmRi4152A.Refresh()
            'Reset to Power-On Defaults
            ResetDigitalMultimeter()
            'Read, then clear the [TIPS]CMD key
            sTIP_CMDstring = sGetKey("TIPS", "CMD")
            sTIP_CMDstring = Strings.Trim(sTIP_CMDstring)
            If sTIP_CMDstring <> "" Then
                frmRi4152A.Cursor = Cursors.WaitCursor
                SetKey("TIPS", "CMD", "")
                ConfigureFrontPanel()
                frmRi4152A.Cursor = Cursors.Default
            End If
            Select Case sTIP_Mode
                Case "TIP_RUNPERSIST"
                    frmRi4152A.chkContinuous.Checked = True
                    TakeMeasurement()
                Case "TIP_DESIGN"
                    sTIP_CMDstring = "" 'Now that SAIS has been setup, Clear old data from string
                    frmRi4152A.cmdUpdateTIP.Visible = True

                Case Else
                    'Run Setup mode
                    TakeMeasurement()
                    Delay(0.5)
                    ExitDmm()
            End Select
        End If
        Application.DoEvents()

        '   Set instrument to default state
        '   This is required for proper Save/Restore
        frmRi4152A.SetMode("2")

        'Set the Refresh rate when in "Debug" mode from 1000 = 1 sec to over 24000 = 24 sec
        frmRi4152A.Panel_Conifg.Refresh = 5000


        'Get current information from instrument
        frmRi4152A.ConfigGetCurrent()

        'DO NOT Put a break point After this point. Causes error if timer is running
        'Set the Mode the Instrument is in: Local = True, Debug = False
        If iStatus <> 1 Then
            frmRi4152A.Panel_Conifg.SetDebugStatus(True)
        Else
            frmRi4152A.Panel_Conifg.SetDebugStatus(False)
        End If

        frmRi4152A.tabUserOptions.SelectedIndex = 0
        For Each Button In frmRi4152A.tbrDmmFunctions.Buttons
            Button.pushed = False
        Next
        frmRi4152A.tbrDmmFunctions.Buttons(0).Pushed = True
    End Sub

    Sub CenterForm(ByVal Form As Object)
        'DESCRIPTION:
        '   This Module Centers One Form With Respect To The
        '   User's Screen.
        'EXAMPLE:
        '   CenterForm frmRi4152A

        Form.Top = PrimaryScreen.Bounds.Height / 2 - Form.Height / 2
        Form.Left = PrimaryScreen.Bounds.Width / 2 - Form.Width / 2
    End Sub

    Function ReadInstrumentBuffer() As String
        ReadInstrumentBuffer = ""
        'DESCRIPTION:
        '   This Module issues a command via the HPE1412A.DLL to poll
        '   the instrument for the measured value.
        'EXAMPLE:
        '   Measurement$ = ReadInstrumentBuffer

        Dim Status As Integer
        'Dim XmlBuf As String
        'Dim Allocation As String
        'Dim Response As String
        'Dim COUNT As Integer

        Dim NBR As Integer 'Number of Bytes Read
        Dim clearStatus As Integer

        ' WMT 1/20/2006 Changed the numberBytesToRead declaration
        ' from:  Dim numberBytesToRead As Long
        ' to:    Dim numberBytesToRead As Integer
        Dim numberBytesToRead As Integer

        'NBR = 255
        numberBytesToRead = 255
        Dim ReadBuffer As String = Space(4000)

        If LiveMode Then
            '       Hard-Coded break point (remove before shipping)
            '       Stop
            Status = atxml_ReadCmds(ResourceName, ReadBuffer, 255, NBR)

            If NBR > 1 Then
                ReadBuffer = Strings.Left(ReadBuffer, NBR - 1)
            Else
                ReadBuffer = Strings.Trim(ReadBuffer)
            End If

            If Status Then
                If Status = VI_ERROR_SYSTEM_ERROR Or Status > VI_ERROR_SYSTEM_ERROR Then
                    clearStatus = atxmlDF_viClear(ResourceName, 255)
                End If
                DisplayErrorMessage(ErrorStatus)
                SendScpiCommand("*CLS")
            End If

            If Strings.Left(ReadBuffer, 2) = "+0" Then
                ReadBuffer = "0"
            End If
        End If

        '   ##########################################
        '   Action Required: Remove debug code
        '   This will enable simulation of commands
        '    If gcolCmds.Count <> 0 Then
        '        ReadBuffer$ = gctlDebug.Simulate(gcolCmds)
        '
        '       Clear collection
        '        Set gcolCmds = New Collection
        '    End If
        '   ##########################################


        'Return Function Value
        ReadInstrumentBuffer = Strings.Trim(ReadBuffer)
    End Function

    Sub ResetDigitalMultimeter()
        'DESCRIPTION:
        '   This Module Resets The DMM Hardware and SAIS
        '   To It's Default Power-On State.
        'EXAMPLE:
        '   ResetDigitalMultimeter

        Dim OutputTriggerInitIndex As Short 'Array Index
        'Dim SystErr As Integer 'VISA Return Error

        'Modified by SJM V1.2 4/23/97
        If LiveMode Then
            'SystErr& = viClear(instrumentHandle&)
        End If

        'The "*CLS ; *RST" Cammand Resets This Instrument to the Power-On State
        SendScpiCommand("*RST")

        'Measurement Function: Dc Voltage
        For Each Button In frmRi4152A.tbrDmmFunctions.Buttons
            Button.Pushed = False
        Next
        frmRi4152A.tbrDmmFunctions.Buttons(0).Pushed = True
        InstrumentMode = 1
        'Range : AUTO
        FillRangeComboBox(VOLTAGE_RANGE_VALUE, 5)

        'Math Functions : Disabled (None)
        FillMathComboBox(4)
        'Math Input Box :Disabled
        'DisableEnableUserEntryBoxSpin MATH, False
        frmRi4152A.panMath.Visible = False
        frmRi4152A.spnMath.Visible = False
        'Auto Zero: ON
        frmRi4152A.optAutoZeroOn.Checked = True
        'AC Filter: Medium Frequency
        frmRi4152A.optAcFilterMedium.Checked = True
        'Auto Impedance: Off
        frmRi4152A.optInputResistanceAuto.Checked = False
        frmRi4152A.optInputResistance10MOhm.Checked = True
        'Output Triggers : Off
        For OutputTriggerInitIndex = 0 To 7
            OUTPUT_TRIGGER_STATE(OutputTriggerInitIndex) = False
        Next OutputTriggerInitIndex
        frmRi4152A.optTriggerOutputOff.Checked = True

        'Source Trigger : Immediate
        'Trigger Delay : AUTO ON
        frmRi4152A.chkDelay.Checked = True
        'DisableEnableUserEntryBoxSpin TRIGGER_DELAY, False
        frmRi4152A.panTriggerDelay.Visible = False
        ' ?? frmRi4152A.panTdUnit.Visible = False


        SetCur(TRIGGER_DELAY) = "0"

        'APER: 0.1 Seconds, 0.166667 (60Hz), 0.2 (50Hz), 100ms
        FillApertureComboBox(Aperture_RANGE1_VALUE, 3)
        frmRi4152A.cboAperture.SelectedIndex = 2

        'Set / Re-Set Display To Ready State
        frmRi4152A.txtDmmDisplay.Text = "Ready"
        'Set / Re-Set Continuous Box To Ready State
        frmRi4152A.chkContinuous.Checked = False
        'Set / Re-Set Status Bar To Ready State
        'SendStatusBarMessage "HP E1412A Digital Multimeter Ready For Measurement"
    End Sub

    Public Function EngNotate(ByVal Number As Double, ByVal Digits As Short, ByVal Unit As String) As String
        EngNotate = ""
        'DESCRIPTION:
        '   Returns passed number as numeric string in Engineering notation (every
        '   3rd exponent) with selectable precision along with Unit Of Measure.
        'EXAMPLE:
        '   Number=10987.1, Uom="Ohm" -> "10.987 KOhm"

        Dim Multiplier As Short
        Dim Negative As Boolean
        Dim Prefix As String
        Dim ReturnString As String

        Multiplier = 0 : Negative = False 'Initialize local variables

        If Number < 0 Then 'If negative
            Number = System.Math.Abs(Val(Convert.ToString(Number))) 'Make it positive for now
            Negative = True 'Set flag
        End If

        If Number >= 1000 Then 'For positive exponent
            Do While Number >= 1000 And Multiplier <= 4
                Number /= 1000
                Multiplier += 1
            Loop
        ElseIf Number < 1 And Number <> 0 Then            'For negative exponent (but not 0)
            Do While Number < 1 And Multiplier >= -4
                Number *= 1000
                Multiplier -= 1
            Loop
        End If

        Select Case Multiplier
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
                Prefix = " " & Strings.Chr(181) 'micro  E-06
            Case -3
                Prefix = " n" 'nano   E-09
            Case -4
                Prefix = " p" 'pico   E-12

            Case Else
                Prefix = " "
        End Select

        If Negative Then Number = -Number

        If Multiplier > 4 Then
            ReturnString = "Ovr Rng"
        ElseIf Multiplier < -4 Then
            ReturnString = "UndrRng"
        Else
            'If Strings.Left$(SetRes$(SetIdx%), 1) = "D" Then
            '    Number = Val(Str$(Number))      ' Clear out very low LSBs from binary math
            '    Digits% = Val(Mid$(SetRes$(SetIdx%), 2, 1)) - Len(Format(Int(Abs(Number))))
            '    If Digits% < 0 Then Digits% = 0
            'Else
            '    Digits% = -1
            'End If
            Select Case Digits
                Case 0
                    ReturnString = Strings.Format(Number, "0")
                Case 1
                    ReturnString = Strings.Format(Number, "0.0")
                Case 2
                    ReturnString = Strings.Format(Number, "0.00")
                Case 3
                    ReturnString = Strings.Format(Number, "0.000")
                Case 4
                    ReturnString = Strings.Format(Number, "0.0000")
                Case 5
                    ReturnString = Strings.Format(Number, "0.00000")
                Case 6
                    ReturnString = Strings.Format(Number, "0.000000")
                Case 7
                    ReturnString = Strings.Format(Number, "0.0000000")
                Case 8
                    ReturnString = Strings.Format(Number, "0.00000000")

                Case Else
                    ReturnString = Strings.Format(Number, "0.000000000")
            End Select
        End If
        If Not (Negative) Then
            'Return Function Value
            EngNotate = "+" & ReturnString & Prefix & Unit
        Else
            'Return Function Value
            EngNotate = ReturnString & Prefix & Unit
        End If
    End Function

    Public Sub Delay(ByVal dSeconds As Double)
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

        Dim date1 As New Date(1900, 1, 1, 0, 0, 0)
        dGetTime = DateDiff(DateInterval.Day, Now, date1) + (DateTime.Now.TimeOfDay.TotalSeconds / SECS_IN_DAY)
    End Function

    Sub SendScpiCommand(ByVal Command As String)
        'DESCRIPTION:
        '   This Module sends a SCPI command to the instrument.  It is
        '   used by TIP Design mode to build the designed TIP CMD string.
        'EXAMPLE:
        '   SendScpiCommand "SENS:VOLT:DC"
        Dim Status As Integer
        Dim clearStatus As Integer
        Dim iActWtiteLen As Integer

        'Check mode in
        If frmRi4152A.Panel_Conifg.DebugMode = True And Strings.Right(Convert.ToString(Command), 1) <> "?" Then Exit Sub

        If bDoNotTalk Or Convert.ToString(Command) = "" Then Exit Sub
        If LiveMode Then
            Status = atxml_WriteCmds(ResourceName, Command, iActWtiteLen)
        End If

        'The follwing is performed following clicking "Update TIP" button
        ' for each non-query SCPI command string sent to the instrument
        If bBuildingTIPstr And InStr(Convert.ToString(Command), "*") < 1 And InStr(Convert.ToString(Command), "?") < 1 Then
            If sTIP_CMDstring = "" Then
                sTIP_CMDstring = ":" & Command
            Else
                sTIP_CMDstring &= ";:" & Command
            End If
        End If

        If Status Then
            If Status = VI_ERROR_SYSTEM_ERROR Or Status > VI_ERROR_SYSTEM_ERROR Then
                clearStatus = atxmlDF_viClear(ResourceName, 255)
                DisplayErrorMessage(ErrorStatus)
                SendScpiCommand("*CLS")
            End If
        End If
        If DebugDwh Then
            MessageBox.Show(Command & vbLf & vbCr & "ERROR_STAT: " & CStr(ErrorStatus), "4152A", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Sub SendStatusBarMessage(ByVal MessageString As String)
        'DESCRIPTION:
        '   This Module sends text to the Status Bar Control.
        'EXAMPLE:
        '   SendStatusBarMessage "Displayed On Status Bar Text"

        If MessageString <> frmRi4152A.sbrUserInformation.Panels(1 - 1).Text Then
            frmRi4152A.sbrUserInformation.Panels(1 - 1).Text = MessageString
        End If
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

        Dim numels As Integer, i As Integer
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
        'Dim nNumChars As Integer
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
        Dim nSize As Integer = 255 'Return Buffer Size
        Dim nNumChars As Integer
        Const MAX_PATH As Long = 260
        Dim lpFileName As String 'INI File Key Name "Key=?"

        'Clear String Buffer
        lpReturnedString = Space$(MAX_PATH)

        'Get the ini file location from the Registry
        lpFileName = Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\ATS\", "IniFilePath", Nothing)

        nNumChars = GetPrivateProfileString(sSection, sKey, "", lpReturnedString, lpReturnedString.Length, lpFileName)
        sGetKey = Strings.Left(lpReturnedString, nNumChars)
    End Function

    Function bGetInstrumentStatus() As Boolean
        'DESCRIPTION:
        '   Procedure to check the instrument status following issuing setup commands
        '   Updates VIPERT.INI [TIPS] status key to indicate an error if in TIP mode
        Dim sInstrumentStatus As String

        SendScpiCommand("SYST:ERR?")
        sInstrumentStatus = ReadInstrumentBuffer()
        If Val(sInstrumentStatus) = 0 Then
            bGetInstrumentStatus = True 'No error
        Else
            bGetInstrumentStatus = False
            frmRi4152A.txtDmmDisplay.Text = "Error"
            MessageBox.Show("Error Code: " & sInstrumentStatus, "DMM Error Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
'            MsgBox("Error Code: " & sInstrumentStatus, MsgBoxStyle.Exclamation, "DMM Error Message")
            If bTIP_Running Then 'Put Error Message in STATUS key, clear CMD key
                SetKey("TIPS", "STATUS", "Error " & sInstrumentStatus)
                SetKey("TIPS", "CMD", "")
                'Quit if in a TIP run mode
                If sTIP_Mode = "TIP_RUNPERSIST" Or sTIP_Mode = "TIP_RUNSETUP" Then ExitDmm()
            End If
            bGetInstrumentStatus() 'mh get the status until all errors are read out of the que
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
        Dim nSize As Integer = 255 'Return Buffer Size
        Dim lpFileName As String = "" 'INI File Key Name "Key=?"
        Dim ReturnValue As Integer 'Return Value Buffer
        Dim FileNameInfo As String = "" 'Formatted Return String
        Dim lpString As String = "" 'String to write to INI File
        Const MAX_PATH As Long = 260

        'Clear String Buffer
        lpReturnedString = Space$(MAX_PATH)
        'Get the ini file location from the Registry
        lpFileName = Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\ATS\", "IniFilePath", Nothing)
        nSize = 255
        lpReturnedString = Space$(MAX_PATH)
        FileNameInfo = ""
        'Find File Locations
        ReturnValue = GetPrivateProfileString(lpApplicationName, lpKeyName, lpDefault, lpReturnedString, nSize, lpFileName)
        FileNameInfo = Trim(lpReturnedString)
        FileNameInfo = Mid(FileNameInfo, 1, Len(FileNameInfo) - 1)
        'If File Locations Missing, then create empty keys
        If FileNameInfo = lpDefault + Convert.ToString(Chr(0)) Or FileNameInfo = lpDefault Then
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
        'Dim sList() As String

        sErrMsg = ""
        'Filter out blank sends caused by redundant semi-colon character at end of command.
        If Len(sSCPI) < 2 Then Exit Sub

        'Filter out IEEE-488.2 (non-SCPI) commands. None of these affect the GUI
        'except *RST which is irrelevant because the SAIS and instrument are reset
        'before calling this sub.
        If InStr(sSCPI, "*") Or InStr(sSCPI, "?") > 0 Then Exit Sub

        'Parse the command from the argument if it has one.
        If InStr(sSCPI, " ") > 0 Then
            sCmd = Strings.Left(sSCPI, InStr(sSCPI, " ")) 'Include space to match SetCod$ array
            sArg = Strings.Mid(sSCPI, InStr(sSCPI, " ") + 1)
        Else
            sCmd = sSCPI
        End If

        'Find match to a command index constant
        For i = 1 To MAX_SETTINGS
            If sCmd = SetCmd(i) Then Exit For
        Next i
        If i > MAX_SETTINGS Then
            sErrMsg = "Invalid command: " & sSCPI & " passed from TIP Studio."
        End If

        If sErrMsg = "" Then
            With frmRi4152A

                'OK, we have a recognized SCPI command. All are listed here but only
                'expected ones have been un-commented for error checking.
                Select Case i

                    'SENSe Options
                    Case SENS_FUNC
                        Select Case sArg
                            Case Q & "VOLT:DC" & Q
                                InstrumentMode = 1 'Default
                            Case Q & "VOLT:AC" & Q
                                InstrumentMode = 2
                            Case Q & "VOLT:DC:RAT" & Q
                                InstrumentMode = 3
                            Case Q & "CURR:DC" & Q
                                InstrumentMode = 4
                            Case Q & "CURR:AC" & Q
                                InstrumentMode = 5
                            Case Q & "RES" & Q
                                InstrumentMode = 6
                            Case Q & "FREQ" & Q
                                InstrumentMode = 7
                            Case Q & "PER" & Q
                                InstrumentMode = 8
                            Case Q & "FRES" & Q
                                InstrumentMode = 9

                            Case Else
                                sErrMsg = "Invalid " & sSCPI & " argument passed from TIP Studio."
                        End Select
                        'ADVINT    'If InstrumentMode% > 1 And sErrMsg = "" Then
                        If InstrumentMode > 0 And sErrMsg = "" Then
                            For Each Button In frmRi4152A.tbrDmmFunctions.Buttons
                                Button.Pushed = False
                            Next
                            .tbrDmmFunctions.Buttons(InstrumentMode - 1).Pushed = True
                            ChangeInstrumentMode()
                        End If

                    Case SENS_CURR_AC_RANG_AUTO, SENS_CURR_DC_RANG_AUTO, SENS_FREQ_VOLT_RANG_AUTO, SENS_FRES_RANG_AUTO, SENS_PER_VOLT_RANG_AUTO, SENS_VOLT_AC_RANG_AUTO, SENS_RES_RANG_AUTO, SENS_VOLT_DC_RANG_AUTO
                        If sArg <> "ON" And sArg <> "OFF" Then sErrMsg = "Invalid " & sSCPI & " argument passed from TIP Studio."

                        '************************************************************************
                        '*** Setup Range Combo Box control based upon value of RANGE argument ***
                        '************************************************************************
                    Case SENS_VOLT_DC_RANG, SENS_VOLT_AC_RANG, SENS_FREQ_VOLT_RANG, SENS_PER_VOLT_RANG
                        If .Panel_Conifg.DebugMode = False Then
                            .tabUserOptions.SelectedIndex = 0
                        End If
                        If .tabUserOptions.SelectedIndex = 0 Then
                            Select Case Val(sArg)
                                Case 0.1
                                    .cboRange.SelectedIndex = 0
                                Case 1
                                    .cboRange.SelectedIndex = 1
                                Case 10
                                    .cboRange.SelectedIndex = 2
                                Case 100
                                    .cboRange.SelectedIndex = 3
                                Case 300
                                    .cboRange.SelectedIndex = 4

                                Case Else
                                    If i = SENS_FREQ_VOLT_RANG Or i = SENS_PER_VOLT_RANG Then
                                        If sArg < 0.1 Then
                                            .cboRange.SelectedIndex = 0
                                        ElseIf sArg = 0.1 Or sArg > 0.1 Or sArg < 1 Then
                                            .cboRange.SelectedIndex = 1
                                        ElseIf sArg = 1 Or sArg > 1 Or sArg < 10 Then
                                            .cboRange.SelectedIndex = 2
                                        ElseIf sArg = 10 Or sArg > 10 Or sArg < 100 Then
                                            .cboRange.SelectedIndex = 3
                                        ElseIf sArg = 100 Or sArg > 100 Then
                                            .cboRange.SelectedIndex = 4
                                        End If
                                    Else
                                        sErrMsg = "Invalid " & sSCPI & " argument passed from TIP Studio."
                                    End If
                            End Select
                        End If

                    Case SENS_RES_RANG, SENS_FRES_RANG
                        If .Panel_Conifg.DebugMode = False Then
                            .tabUserOptions.SelectedIndex = 0
                        End If
                        If .tabUserOptions.SelectedIndex = 0 Then
                            Select Case Val(sArg)
                                Case 100
                                    .cboRange.SelectedIndex = 0
                                Case 1000
                                    .cboRange.SelectedIndex = 1
                                Case 10000
                                    .cboRange.SelectedIndex = 2
                                Case 100000
                                    .cboRange.SelectedIndex = 3
                                Case 1000000
                                    .cboRange.SelectedIndex = 4
                                Case 10000000
                                    .cboRange.SelectedIndex = 5
                                Case 100000000
                                    .cboRange.SelectedIndex = 6

                                Case Else
                                    sErrMsg = "Invalid " & sSCPI & " argument passed from TIP Studio."
                            End Select
                        End If

                    Case SENS_CURR_DC_RANG
                        If .Panel_Conifg.DebugMode = False Then
                            .tabUserOptions.SelectedIndex = 0
                        End If
                        If .tabUserOptions.SelectedIndex = 0 Then
                            Select Case Val(sArg)
                                Case 0.01
                                    .cboRange.SelectedIndex = 0
                                Case 0.1
                                    .cboRange.SelectedIndex = 1
                                Case 1
                                    .cboRange.SelectedIndex = 2
                                Case 3
                                    .cboRange.SelectedIndex = 3

                                Case Else
                                    sErrMsg = "Invalid " & sSCPI & " argument passed from TIP Studio."
                            End Select
                        End If

                    Case SENS_CURR_AC_RANG
                        If .Panel_Conifg.DebugMode = False Then
                            .tabUserOptions.SelectedIndex = 0
                        End If
                        If .tabUserOptions.SelectedIndex = 0 Then
                            Select Case Val(sArg)
                                Case 1
                                    .cboRange.SelectedIndex = 0
                                Case 3
                                    .cboRange.SelectedIndex = 1

                                Case Else
                                    sErrMsg = "Invalid " & sSCPI & " argument passed from TIP Studio."
                            End Select

                            'AUTO Range only available for PERIOD/FREQUENCY measurements
                            '      Case SENS_PER_VOLT_RANG
                            '      Case SENS_FREQ_VOLT_RANG
                        End If

                    Case SENS_VOLT_DC_APER, SENS_CURR_DC_APER, SENS_RES_APER, SENS_FRES_APER
                        If .Panel_Conifg.DebugMode = False Then
                            .tabUserOptions.SelectedIndex = 2
                        End If
                        If .tabUserOptions.SelectedIndex = 2 Then
                            Select Case CDec(sArg)
                                Case 1.66667
                                    .cboAperture.SelectedIndex = 3
                                Case 0.166667
                                    .cboAperture.SelectedIndex = 2
                                Case 0.0166667
                                    .cboAperture.SelectedIndex = 1
                                Case 0.00333
                                    .cboAperture.SelectedIndex = 0

                                Case Else
                                    sErrMsg = "Invalid " & sSCPI & " argument passed from TIP Studio."
                            End Select
                            Select Case InstrumentMode
                                Case 1 To 6, 9
                                    .cboNPLC.SelectedIndex = .cboAperture.SelectedIndex
                            End Select
                        End If

                    Case SENS_FREQ_APER, SENS_PER_APER
                        If .Panel_Conifg.DebugMode = False Then
                            .tabUserOptions.SelectedIndex = 2
                        End If
                        If .tabUserOptions.SelectedIndex = 2 Then
                            Select Case Val(sArg)
                                Case 1
                                    .cboAperture.SelectedIndex = 2
                                Case 0.1
                                    .cboAperture.SelectedIndex = 1
                                Case 0.01
                                    .cboAperture.SelectedIndex = 0

                                Case Else
                                    sErrMsg = "Invalid " & sSCPI & " argument passed from TIP Studio."
                            End Select
                        End If

                    Case SENS_VOLT_DC_NPLC, SENS_CURR_DC_NPLC, SENS_RES_NPLC, SENS_FRES_NPLC
                        If .Panel_Conifg.DebugMode = False Then
                            .tabUserOptions.SelectedIndex = 2
                        End If
                        If .tabUserOptions.SelectedIndex = 2 Then
                            .cboNPLC.SelectedIndex = .cboAperture.SelectedIndex
                        End If

                    Case SENS_DET_BAND
                        If .Panel_Conifg.DebugMode = False Then
                            .tabUserOptions.SelectedIndex = 1
                        End If
                        If .tabUserOptions.SelectedIndex = 1 Then
                            Select Case CInt(sArg)
                                Case 200
                                    .optAcFilterFast.Checked = True
                                Case 20
                                    .optAcFilterMedium.Checked = True
                                Case 3
                                    .optAcFilterSlow.Checked = True

                                Case Else
                                    sErrMsg = "Invalid " & sSCPI & " argument passed from TIP Studio."
                            End Select
                        End If

                    Case SENS_ZERO_AUTO
                        If .Panel_Conifg.DebugMode = False Then
                            .tabUserOptions.SelectedIndex = 1
                        End If
                        If .tabUserOptions.SelectedIndex = 1 Then
                            Select Case sArg
                                Case "ON"
                                    .optAutoZeroOn.Checked = True
                                Case "OFF"
                                    .optAutoZeroOff.Checked = True
                                Case "ONCE"
                                    .optAutoZeroOnce.Checked = True

                                Case Else
                                    sErrMsg = "Invalid " & sSCPI & " argument passed from TIP Studio."
                            End Select
                        End If

                        'TRIGger Options
                    Case TRIG_DEL_AUTO
                        If sArg <> "ON" And sArg <> "OFF" Then sErrMsg = "Invalid " & sSCPI & " argument passed from TIP Studio."

                    Case TRIG_DEL
                        If .Panel_Conifg.DebugMode = False Then
                            .tabUserOptions.SelectedIndex = 1
                        End If
                        If .tabUserOptions.SelectedIndex = 1 Then
                            If Val(sArg) < Val(SetMin(TRIGGER_DELAY)) Or Val(sArg) > Val(SetMax(TRIGGER_DELAY)) Then
                                sErrMsg = "Invalid " & sSCPI & " argument passed from TIP Studio."
                            Else
                                SetCur(TRIGGER_DELAY) = sArg
                                ' ADVINT added line:
                                .chkDelay.Checked = True 'This will call form sub "chkDelay_Click"
                                .chkDelay.Checked = False 'This will call form sub "chkDelay_Click"
                            End If
                        End If

                    Case TRIG_SOUR
                        If .Panel_Conifg.DebugMode = False Then
                            .tabUserOptions.SelectedIndex = 2
                        End If
                        If .tabUserOptions.SelectedIndex = 2 Then
                            Select Case sArg
                                Case "IMM"
                                    .cboTriggerSource.SelectedIndex = 10
                                Case "EXT"
                                    .cboTriggerSource.SelectedIndex = 9
                                Case "BUS"
                                    .cboTriggerSource.SelectedIndex = 8
                                Case "TTLT7"
                                    .cboTriggerSource.SelectedIndex = 7
                                Case "TTLT6"
                                    .cboTriggerSource.SelectedIndex = 6
                                Case "TTLT5"
                                    .cboTriggerSource.SelectedIndex = 5
                                Case "TTLT4"
                                    .cboTriggerSource.SelectedIndex = 4
                                Case "TTLT3"
                                    .cboTriggerSource.SelectedIndex = 3
                                Case "TTLT2"
                                    .cboTriggerSource.SelectedIndex = 2
                                Case "TTLT1"
                                    .cboTriggerSource.SelectedIndex = 1
                                Case "TTLT0"
                                    .cboTriggerSource.SelectedIndex = 0

                                Case Else
                                    sErrMsg = "Invalid " & sSCPI & " argument passed from TIP Studio."
                            End Select
                        End If

                        'CALCulate Options
                    Case CALC_STAT
                        If sArg <> "ON" And sArg <> "OFF" Then sErrMsg = "Invalid " & sSCPI & " argument passed from TIP Studio."

                    Case CALC_FUNC
                        If .Panel_Conifg.DebugMode = False Then
                            .tabUserOptions.SelectedIndex = 0
                        End If
                        If .tabUserOptions.SelectedIndex = 0 Then
                            Select Case sArg
                                Case "DBM"
                                    .cboMath.SelectedIndex = 0
                                Case "DB"
                                    .cboMath.SelectedIndex = 1
                                Case "NULL"
                                    'ADVINT .cboMath.ListIndex = 2
                                    Select Case InstrumentMode
                                        Case 1, 2
                                            .cboMath.SelectedIndex = 2

                                        Case Else
                                            .cboMath.SelectedIndex = 0
                                    End Select
                                Case "AVER"
                                    Select Case InstrumentMode
                                        Case 1, 2
                                            .cboMath.SelectedIndex = 3
                                            'mh Case 3:    .cboMath.ListIndex = 0

                                        Case Else
                                            .cboMath.SelectedIndex = 1
                                    End Select
                                Case "NONE"
                                    Select Case InstrumentMode
                                        Case 1, 2
                                            .cboMath.SelectedIndex = 6
                                        Case 3
                                            .cboMath.SelectedIndex = 2

                                        Case Else
                                            .cboMath.SelectedIndex = 4
                                    End Select

                                Case Else
                                    sErrMsg = "Invalid " & sSCPI & " argument passed from TIP Studio."
                            End Select
                            If sErrMsg = "" Then AdjustMathComboBox() 'Shows appropriate controls
                        End If

                    Case CALC_DB_REF, CALC_DBM_REF
                        If .Panel_Conifg.DebugMode = False Then
                            .tabUserOptions.SelectedIndex = 0
                        End If
                        If .tabUserOptions.SelectedIndex = 0 Then
                            If Val(sArg) < Val(SetMin(MATHINPUT)) Or Val(sArg) > Val(SetMax(MATHINPUT)) Then
                                sErrMsg = "Invalid " & sSCPI & " argument passed from TIP Studio."
                            Else
                                SetCur(MATHINPUT) = Val(sArg)
                                frmRi4152A.txtMath.Text = SetCur(MATHINPUT)
                            End If
                        End If

                    Case SAMP_COUN
                        If .Panel_Conifg.DebugMode = False Then
                            .tabUserOptions.SelectedIndex = 1
                        End If
                        If .tabUserOptions.SelectedIndex = 1 Then
                            If Val(sArg) < Val(SetMin(SAMP_COUN)) Or Val(sArg) > Val(SetMax(SAMP_COUN)) Then
                                sErrMsg = "Invalid " & sSCPI & " argument passed from TIP Studio."
                            Else
                                SetCur(SAMP_COUN) = sArg
                                frmRi4152A.txtSampleCount.Text = SetCur(SAMP_COUN)
                            End If
                        End If

                    Case TRIG_COUN
                        If .Panel_Conifg.DebugMode = False Then
                            .tabUserOptions.SelectedIndex = 1
                        End If
                        If .tabUserOptions.SelectedIndex = 1 Then
                            'mh If sArg = "INF" Then
                            'mh     frmRi4152A.chkTrigCoun.Checked = True
                            'mh     SetCur$(TRIG_COUN) = sArg
                            'mh    frmRi4152A.TextBox(TRIG_COUN) = SetCur$(TRIG_COUN)
                            If Val(sArg) < Val(SetMin(TRIG_COUN)) Or Val(sArg) > Val(SetMax(TRIG_COUN)) Then
                                sErrMsg = "Invalid " & sSCPI & " argument passed from TIP Studio."
                            Else
                                frmRi4152A.chkTriggerCount.Checked = False
                                SetCur(TRIG_COUN) = sArg
                                frmRi4152A.txtTriggerCount.Text = SetCur(TRIG_COUN)
                            End If
                        End If

                        'INPut Options
                    Case INP_IMP_AUTO
                        If .Panel_Conifg.DebugMode = False Then
                            .tabUserOptions.SelectedIndex = 1
                        End If
                        If .tabUserOptions.SelectedIndex = 1 Then
                            Select Case sArg
                                Case "ON"
                                    .optInputResistanceAuto.Checked = True
                                Case "OFF"
                                    .optInputResistance10MOhm.Checked = True

                                Case Else
                                    sErrMsg = "Invalid " & sSCPI & " argument passed from TIP Studio."
                            End Select
                        End If

                        'OUTPut Options
                    Case OUTP_TTLT0_STAT
                        If .Panel_Conifg.DebugMode = False Then
                            .tabUserOptions.SelectedIndex = 2
                        End If
                        If .tabUserOptions.SelectedIndex = 2 Then
                            If sArg = "ON" Then
                                .cboTriggerOutput.SelectedIndex = 0
                                .optTriggerOutputOn.Checked = True
                            End If
                        End If

                    Case OUTP_TTLT1_STAT
                        If .Panel_Conifg.DebugMode = False Then
                            .tabUserOptions.SelectedIndex = 2
                        End If
                        If .tabUserOptions.SelectedIndex = 2 Then
                            If sArg = "ON" Then
                                .cboTriggerOutput.SelectedIndex = 1
                                .optTriggerOutputOn.Checked = True
                            End If
                        End If

                    Case OUTP_TTLT2_STAT
                        If .Panel_Conifg.DebugMode = False Then
                            .tabUserOptions.SelectedIndex = 2
                        End If
                        If .tabUserOptions.SelectedIndex = 2 Then
                            If sArg = "ON" Then
                                .cboTriggerOutput.SelectedIndex = 2
                                .optTriggerOutputOn.Checked = True
                            End If
                        End If

                    Case OUTP_TTLT3_STAT
                        If .Panel_Conifg.DebugMode = False Then
                            .tabUserOptions.SelectedIndex = 2
                        End If
                        If .tabUserOptions.SelectedIndex = 2 Then
                            If sArg = "ON" Then
                                .cboTriggerOutput.SelectedIndex = 3
                                .optTriggerOutputOn.Checked = True
                            End If
                        End If

                    Case OUTP_TTLT4_STAT
                        If .Panel_Conifg.DebugMode = False Then
                            .tabUserOptions.SelectedIndex = 2
                        End If
                        If .tabUserOptions.SelectedIndex = 2 Then
                            If sArg = "ON" Then
                                .cboTriggerOutput.SelectedIndex = 4
                                .optTriggerOutputOn.Checked = True
                            End If
                        End If

                    Case OUTP_TTLT5_STAT
                        If .Panel_Conifg.DebugMode = False Then
                            .tabUserOptions.SelectedIndex = 2
                        End If
                        If .tabUserOptions.SelectedIndex = 2 Then
                            If sArg = "ON" Then
                                .cboTriggerOutput.SelectedIndex = 5
                                .optTriggerOutputOn.Checked = True
                            End If
                        End If

                    Case OUTP_TTLT6_STAT
                        If .Panel_Conifg.DebugMode = False Then
                            .tabUserOptions.SelectedIndex = 2
                        End If
                        If .tabUserOptions.SelectedIndex = 2 Then
                            If sArg = "ON" Then
                                .cboTriggerOutput.SelectedIndex = 6
                                .optTriggerOutputOn.Checked = True
                            End If
                        End If

                    Case OUTP_TTLT7_STAT
                        If .Panel_Conifg.DebugMode = False Then
                            .tabUserOptions.SelectedIndex = 2
                        End If
                        If .tabUserOptions.SelectedIndex = 2 Then
                            If sArg = "ON" Then
                                .cboTriggerOutput.SelectedIndex = 7
                                .optTriggerOutputOn.Checked = True
                            End If
                        End If

                    Case Else
                        sErrMsg = "DMM SAIS does not support '" & RTrim(sCmd) & "' command passed from TIP Studio."

                End Select
            End With
        End If

        If sErrMsg <> "" Then
            'Notify TIP studio that an error has occured and quit SAIS
            frmRi4152A.txtDmmDisplay.Text = "Error"
            'If sTIP_Mode <> "TIP_DESIGN" Then
            If Not (sTIP_Mode = "TIP_DESIGN") And Not (sTIP_Mode = "GET CURR CONFIG") Then
                Delay(1)
                SetKey("TIPS", "STATUS", "Error: " & sErrMsg)
                ExitDmm()
            End If
            If Not (sTIP_Mode = "GET CURR CONFIG") Then
                MessageBox.Show(sErrMsg & vbCrLf & vbCrLf & "Instrument will be set to RESET state.", "4152A", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                ResetDigitalMultimeter()
                sTIP_Mode = "TIP_ERROR" 'Flag to exit saved string setup
            Else
                sErrMsg = "Invalid " & sSCPI & " argument received from instrument query."
                MessageBox.Show(sErrMsg, "Get Current - Unhandled Parameter", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
    End Sub

    Private Sub ConfigureFrontPanel()
        'DESCRIPTION:
        '   This Module configures the SAIS in TIP Mode based on the settings of a
        '   saved command string.
        'EXAMPLE:
        '   ConfigureFrontPanel

        Dim i As Short
        Dim sCmds() As String = {""}

        frmRi4152A.Cursor = Cursors.WaitCursor

        'Parsing SCPI commands from string loaded from [TIPS]CMD key
        'Set SAIS controls and instrument from SCPI commands.
        bDoNotTalk = True
        For i = 1 To StringToList(sTIP_CMDstring, 1, sCmds, ";")
            ActivateControl(sCmds(i))
            If sTIP_Mode = "TIP_ERROR" Then
                'If a saved string error has occured, SAIS is set to reset state
                sTIP_Mode = "TIP_DESIGN"
                Exit For
            End If
        Next i
        bDoNotTalk = False

        frmRi4152A.Cursor = Cursors.Default
    End Sub
End Module