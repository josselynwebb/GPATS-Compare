'Option Strict Off
'Option Explicit On

Imports System
Imports System.Windows.Forms
Imports System.Diagnostics
Imports System.Text
Imports System.Math
Imports Microsoft.VisualBasic
Imports Microsoft.Win32

Public Module ArbMain
    '=========================================================
    '//2345678901234567890123456789012345678901234567890123456789012345678901234567890
    '//////////////////////////////////////////////////////////////////////////////////////////////////////
    '//
    '// Virtual Instrument Portable Equipment Repair/Tester (VIPER/T) Software Module
    '//
    '// File:       Main.bas
    '//
    '// Date:       27Oct06
    '//
    '// Purpose:    SAIS: Arbitrary Waveform Generator Front Panel
    '//
    '// Instrument: Agilent E1445A Arbitrary Waveform Generator (Arb)
    '//
    '//
    '// Revision History
    '// Rev      Date                  Reason                                   Author
    '// =======  =======    =======================================             ==================
    '// 1.1.0.0  27Oct06    Initial Release                                     EADS, North America Defense
    '// 1.1.0.1  15Nov06    Following changers were made:                       M.Hendricksen, EADS
    '//                     - Changed Logo
    '//                     - Changed code to implement CICL requirements
    '//                         of not sending any commands on start up or
    '//                         reset except query commands.
    '// 1.2.0.0 07Mar07     Corrected code for SetMin VOLT_DIV_7 in            M.Hendricksen, EADS
    '//                         AdjustVoltsLimits to only adjust the minimum
    '//                         to 6 digits.
    '// 1.3.0.1 16Apr07     Corrected code to set load in "INF" when High       M. Hendricksen, EADS
    '//                       impedance is selected in function optLoadImpHigh.
    '// 1.3.0.2 17Nov08     Added double quote char to args for :MARK:FEED
    '//                     group of commands in bActivateCommands              E. Larson, EADS
    '///////////////////////////////////////////////////////////////////////////////////////////////////////



    '----------------- VXIplug&play Driver Declarations ---------
    Declare Function hpe1445a_init Lib "Hpe1445a.dll" (ByVal resourceName As String, ByVal IDQuery As Short, ByVal resetDevice As Short, ByRef InstrumentHandle As Integer) As Integer
    Declare Function hpe1445a_close Lib "Hpe1445a.dll" (ByVal x1 As Integer) As Integer

    Declare Function viRead Lib "VISA32.DLL" Alias "#256" (ByVal vi As Integer, ByVal Buffer As String, ByVal count As Integer, ByRef retCount As Integer) As Integer
    Declare Function viWrite Lib "VISA32.DLL" Alias "#257" (ByVal vi As Integer, ByVal Buffer As String, ByVal count As Integer, ByRef retCount As Integer) As Integer

    Declare Function atxml_Initialize Lib "AtXmlApi.dll" (ByVal proctype As String, ByVal guid As String) As Integer

    Declare Function atxml_Close Lib "AtXmlApi.dll"  As Integer

    Declare Function atxml_ValidateRequirements Lib "AtXmlApi.dll" (ByVal TestRequirements As String, ByVal Allocation As String, ByVal Availability As String, ByVal BufferSize As Integer) As Integer

    Declare Function atxml_WriteCmds Lib "AtXmlApi.dll" (ByVal resourceName As String, ByVal InstrumentCmds As String, ByRef ActWriteLen As Integer) As Integer

    Declare Function atxml_ReadCmds Lib "AtXmlApi.dll" (ByVal resourceName As String, ByVal ReadBuffer As String, ByVal BufferSize As Integer, ByRef ActReadLen As Integer) As Integer

    Declare Function atxmlDF_viSetAttribute Lib "AtxmlDriverFunc.DLL" (ByVal resourceName As String, ByVal vi As Integer, ByVal attrName As Integer, ByVal attrValue As Integer) As Integer

    Declare Function atxmlDF_viOut16 Lib "AtxmlDriverFunc.DLL" (ByVal resourceName As String, ByVal vi As Integer, ByVal accSpace As Integer, ByVal offset As Integer, ByVal val16 As Integer) As Integer

    Declare Function atxmlDF_viStatusDesc Lib "AtxmlDriverFunc.DLL" (ByVal resourceName As String, ByVal vi As Integer, ByVal status As Integer, ByVal desc As String) As Integer

    Declare Function atxml_IssueDriverFunctionCall Lib "AtXmlApi.dll" (ByVal XmlBuffer As String, ByVal Response As String, ByVal BufferSize As Integer) As Integer
    Declare Function atxml_IssueSignal Lib "AtXmlApi.dll" (ByVal XmlBuffer As String, ByVal Response As String, ByVal BufferSize As Integer) As Integer
    Declare Function atxml_IssueIst Lib "AtXmlApi.dll" (ByVal XmlBuffer As String, ByVal Response As String, ByVal BufferSize As Integer) As Integer

    '-----------------API / DLL Declarations------------------------------
    Declare Function GetPrivateProfileString Lib "Kernel32" Alias "GetPrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpDefault As String, ByVal lpReturnedString As String, ByVal nSize As Integer, ByVal lpFileName As String) As Integer
    Declare Function WritePrivateProfileString Lib "Kernel32" Alias "WritePrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpString As String, ByVal lpFileName As String) As Integer
    ' WindowStates
    Public Const NORMAL As Short = 0 ' 0 - Normal
    Public Const MINIMIZED As Short = 1 ' 1 - Minimized
    Public Const MAXIMIZED As Short = 2 ' 2 - Maximized

    '---------------------------------------------------------------------
    '----------------- Global Constants ----------------------------------
    '---------------------------------------------------------------------
    ' Constant declarations for the indices to the SETTINGS arrays.
    ' Each constant name is formed from the non-optional SCPI keywords
    '   and non-optional characters for the parameter it represents.
    '   Example: "ARM[:STARt][:LAYer[1]]:COUNt " is "ARM_COUN"
    ' All SCPI commands are represented, not all are used.

    ' First Coupling Group: "Frequency"
    Public Const guid As String = "{F0036371-9B45-4d60-839B-4B94CEA259CE}"
    Public Const proctype As String = "SFP"

    Public resourceName As String
    Public bDebugMode As Boolean

    Public Const TRIG_SOUR As Short = 1
    Public Const ROSC_FREQ_EXT As Short = 2
    Public Const ROSC_SOUR As Short = 3
    Public Const ARM_SWE_COUN As Short = 4
    Public Const ARM_SWE_SOUR As Short = 5
    Public Const FREQ_CENT As Short = 6
    Public Const FREQ As Short = 7
    Public Const FREQ_FSK_L As Short = 8
    Public Const FREQ_FSK_H As Short = 9
    Public Const FREQ_FSK_SOUR As Short = 10
    Public Const FREQ_MODE As Short = 11
    Public Const FREQ_RANG As Short = 12
    Public Const FREQ_SPAN As Short = 13
    Public Const FREQ_STAR As Short = 14
    Public Const FREQ_STOP As Short = 15
    Public Const FREQ2 As Short = 16
    Public Const LIST2_FREQ As Short = 17
    Public Const PM_SOUR As Short = 18
    Public Const PM_STAT As Short = 19

    Public Const SWE_COUN As Short = 20
    Public Const SWE_DIR As Short = 21
    Public Const SWE_POIN As Short = 22
    Public Const SWE_SPAC As Short = 23
    Public Const SWE_TIME As Short = 24
    Public Const TRIG_GATE_POL As Short = 25
    Public Const TRIG_GATE_SOUR As Short = 26
    Public Const TRIG_GATE_STAT As Short = 27

    Public Const TRIG_STOP_SLOP As Short = 28
    Public Const TRIG_STOP_SOUR As Short = 29
    Public Const TRIG_SWE_SOUR As Short = 30
    Public Const TRIG_SWE_TIM As Short = 31
    Public Const LAST_COUP_GROUP_1_COMMAND As Short = TRIG_SWE_TIM

    ' Second Coupling Group: "Frequency & Voltage"
    ' Send these with both groups
    Public Const ARB_DAC_SOUR As Short = 32
    Public Const FUNC As Short = 33
    Public Const RAMP_POIN As Short = 34
    Public Const LAST_COUP_GROUP_2_COMMAND = RAMP_POIN

    ' Third Coupling Group: "Voltage"
    Public Const OUTP_IMP As Short = 35
    Public Const OUTP_LOAD As Short = 36
    Public Const OUTP_LOAD_AUTO As Short = 37
    Public Const RAMP_POL As Short = 38
    Public Const VOLT As Short = 39
    Public Const VOLT_OFFS As Short = 40
    Public Const LAST_COUP_GROUP_3_COMMAND As Short = VOLT_OFFS

    ' Non-Coupled: May be sent before or after coupled commands, but not in-between
    Public Const ABOR As Short = 41
    Public Const ARM_COUN As Short = 42
    Public Const ARM_LAY2_COUN As Short = 43
    Public Const ARM_LAY2 As Short = 44
    Public Const ARM_LAY2_SLOP As Short = 45
    Public Const ARM_LAY2_SOUR As Short = 46
    Public Const ARM_SWE As Short = 47
    Public Const ARM_SWE_LINK As Short = 48
    Public Const CAL_COUN As Short = 49
    Public Const CAL_DATA_AC As Short = 50
    Public Const CAL_DATA_AC2 As Short = 51
    Public Const CAL_DATA As Short = 52
    Public Const CAL_BEG As Short = 53
    Public Const CAL_POIN As Short = 54
    Public Const CAL_SEC_CODE As Short = 55
    Public Const CAL_SEC As Short = 56
    Public Const CAL_STAT As Short = 57
    Public Const CAL_STAT_AC As Short = 58
    Public Const CAL_STAT_DC As Short = 59
    Public Const INIT As Short = 60
    Public Const OUTP_FILT_FREQ As Short = 61
    Public Const OUTP_FILT As Short = 62
    Public Const OUTP As Short = 63
    Public Const ARB_DAC_FORM As Short = 64
    Public Const ARB_DOWN As Short = 65
    Public Const ARB_DOWN_COMP As Short = 66
    Public Const FUNC_USER As Short = 67
    Public Const LIST_FORM As Short = 68
    Public Const LIST_ADDR As Short = 69
    Public Const LIST_CAT As Short = 70
    Public Const LIST_COMB As Short = 71
    Public Const LIST_COMB_POIN As Short = 72
    Public Const LIST_DEF As Short = 73
    Public Const LIST_DEL_ALL As Short = 74
    Public Const LIST_DEL As Short = 75
    Public Const LIST_FREE As Short = 76
    Public Const LIST_MARK As Short = 77
    Public Const LIST_MARK_POIN As Short = 78
    Public Const LIST_MARK_SPO As Short = 79
    Public Const LIST_SEL As Short = 80
    Public Const LIST_VOLT As Short = 81
    Public Const LIST_VOLT_DAC As Short = 82
    Public Const LIST_VOLT_POIN As Short = 83
    Public Const LIST_SSEQ_ADDR As Short = 84
    Public Const LIST_SSEQ_CAT As Short = 85
    Public Const LIST_SSEQ_COMB As Short = 86
    Public Const LIST_SSEQ_COMB_POIN As Short = 87
    Public Const LIST_SSEQ_DEF As Short = 88
    Public Const LIST_SSEQ_DEL_ALL As Short = 89
    Public Const LIST_SSEQ_DEL As Short = 90
    Public Const LIST_SSEQ_DWEL_COUN As Short = 91
    Public Const LIST_SSEQ_DWEL_COUN_POIN As Short = 92
    Public Const LIST_SSEQ_FREE As Short = 93
    Public Const LIST_SSEQ_MARK As Short = 94
    Public Const LIST_SSEQ_MARK_POIN As Short = 95
    Public Const LIST_SSEQ_MARK_SPO As Short = 96
    Public Const LIST_SSEQ_SEL As Short = 97
    Public Const LIST_SSEQ_SEQ As Short = 98
    Public Const LIST_SSEQ_SEQ_SEG As Short = 99
    Public Const LIST2_FORM As Short = 100
    Public Const LIST2_POIN As Short = 101
    Public Const MARK_ECLT0_FEED As Short = 102
    Public Const MARK_ECLT0 As Short = 103
    Public Const MARK_ECLT1_FEED As Short = 104
    Public Const MARK_ECLT1 As Short = 105
    Public Const MARK_FEED As Short = 106
    Public Const MARK_POL As Short = 107
    Public Const MARK As Short = 108
    Public Const PM As Short = 109
    Public Const PM_UNIT As Short = 110
    Public Const VOLT_UNIT As Short = 111
    Public Const STAT_OPC_INIT As Short = 112
    Public Const STAT_OPER_COND As Short = 113
    Public Const STAT_OPER_ENAB As Short = 114
    Public Const STAT_OPER As Short = 115
    Public Const STAT_OPER_NTR As Short = 116
    Public Const STAT_OPER_PTR As Short = 117
    Public Const STAT_PRES As Short = 118
    Public Const SYST_ERR As Short = 119
    Public Const SYST_VER As Short = 120
    Public Const TRIG_COUN As Short = 121
    Public Const TRIG As Short = 122
    Public Const TRIG_SLOP As Short = 123
    Public Const TRIG_STOP As Short = 124
    Public Const TRIG_SWE As Short = 125
    Public Const TRIG_SWE_LINK As Short = 126
    Public Const VINS_LBUS As Short = 127
    Public Const VINS_LBUS_AUTO As Short = 128
    Public Const VINS_TEST_CONF As Short = 129
    Public Const VINS_TEST_DATA As Short = 130
    Public Const VINS_VME As Short = 131
    Public Const VINS_VME_REC_ADDR_DATA As Short = 132
    Public Const VINS_VME_REC_ADDR_READ As Short = 133
    Public Const VINS_IDEN As Short = 134

    'These are not SCPI parameters
    Public Const JDH_TIME As Short = 135
    Public Const JDH_FREQ As Short = 136
    Public Const JDH_CYCLES As Short = 137
    Public Const JDH_PHASE As Short = 138
    Public Const JDH_DCYC As Short = 139
    Public Const JDH_PM_CARR As Short = 140
    Public Const JDH_PM_MOD As Short = 141
    Public Const JDH_PM_POS_DEV As Short = 142
    Public Const JDH_PM_NEG_DEV As Short = 143
    Public Const JDH_DWEL_COUN As Short = 144
    Public Const VOLT_DIV_7 As Short = 145

    Public Const MAX_SETTINGS As Short = 145 ' The number of parameter settings

    '---------------------------------------------------------------------
    '-----------------Global Variables------------------------------------

    Public SetCur(MAX_SETTINGS) As String ' "Current Settings" Array
    Public SetDef(MAX_SETTINGS) As String ' "Default Settings" Array
    Public SetMin(MAX_SETTINGS) As String ' "Maximum Settings" Array
    Public SetMax(MAX_SETTINGS) As String ' "Minimum Settings" Array
    Public SetUOM(MAX_SETTINGS) As String ' "Unit Of Measure" Array
    Public SetRes(MAX_SETTINGS) As String ' "Setting Resolution" Array
    Public SetMinInc(MAX_SETTINGS) As String ' "Setting Minimum Increment" Array
    Public SetCod(MAX_SETTINGS) As String ' "Setting SCPI Code" Array
    Public ExecInit(MAX_SETTINGS) As Short ' "Executable when Initiated" Flag Array

    Public Initialized As Boolean ' Tracks when ARB is initiated
    Public Wave() As Double = {}
    Public Back() As Double = {}
    Public XSpan As Single
    Public YSpan As Single
    Public lListDefine As Integer ' Tracks last number of waveform points for undo
    Public WaveIdx As Short = 1 ' Index of the selected wave button in the tool bar
    Public ModeTrigSour(9) As String
    Public ModeTrigSourIdx As Short
    Public RefOscFreq As Double ' Tracks the current Reference Oscillator Frequency
    Public CurFreqIdx As Short ' Tracks the index of the currently selected Frequency source
    Public VOLT_At_V_50 As Double ' These track current VOLT and VOLT_OFFS values
    Public OFFS_At_V_50 As Double ' in V units for a matched impedance output.
    Public UserError As Boolean ' Used to detect an instrument error occurance
    Public Index As Integer
    Public bActivatingControls As Boolean 'Signifies when controls are being 'clicked' by
    'Configuration Load/Save and not by the user.
    'Allows ActivateControl to work in FormatEntry
    Public LastOffs As Double
    Dim sWindowsDir As String 'Path of Windows Directory
    Public lpString As String
    Dim nNumChars As Integer
    Public sTipCmds As String
    Dim bRunningTipCmds As Boolean
    Public sTipMode As String
    Public bInstrumentMode As Boolean ' True if in GetCurrentInstrument Mode, sfp only send queries

    'Options Tab Index Constants
    Public Const TAB_MAIN As Short = 0
    Public Const TAB_ARM As Short = 1
    Public Const TAB_MARKERS As Short = 2
    Public Const TAB_AGILE As Short = 3
    Public Const TAB_USER As Short = 4
    Public Const TAB_SEQUENCE As Short = 5
    Public Const TAB_OPTIONS As Short = 6

    Public Const INSTRUMENT_NAME As String = "Arbitrary Waveform Generator"
    Public Const MANUF As String = "Hewlett Packard"
    Public Const MODEL_CODE As String = "HP E1445A"
    Public Const RESOURCE_NAME As String = "VXI::48"

    'Added V1.3 JHill
    'User-Defined type compatible with HP E1428A scope trace preamble data.

    Structure TracePreamble
        Dim Format As Short
        Dim Type As Short
        Dim Points As Integer
        Dim count As Short
        Dim XInc As Double
        Dim XOrig As Double
        Dim XRef As Short
        Dim YInc As Double
        Dim YOrig As Double
        Dim YRef As Short
    End Structure
    Public Trace As TracePreamble

    '***Read Instrument Mode***
    Public iStatus As Short
    Public gctlParse As New VIPERT_Common_Controls.Common
    '###############################################################

    Public Function bActivateControl(ByVal SCPI As String) As Boolean
        'DESCRIPTION:
        '   Sets a panel control to the state indicated by the argument of the
        '   SCPI command in the SCPI$ string argument. It simulates the action
        '   of a user selecting the control except that the actual commands are not
        '   sent to the ARB. That is done outside of this sub to maintain coupling groups.
        'PARAMETERS:
        '   SCPI$:  A single SCPI or IEEE-488.2 command with argument(s) if any.

        Dim i As Short
        Dim Cmd As String, Arg As String
        Dim sErrMsg As String
        Dim sList() As String = {""}
        Dim sLoadedList() As String
        Dim bLoaded As Boolean
        Dim n As Integer

        bActivateControl = True
        'Filter out blank sends caused by redundant semi-colon character at end of command.
        If SCPI.Length < 2 Then Exit Function

        'Filter out IEEE-488.2 (non-SCPI) commands. Non of these affect the GUI
        'except *RST which is irrelevant because the SAIS and ARB are reset
        'before calling this sub.
        If InStr(SCPI, "*") Then Exit Function

        'Parse the command from the argument if it has one.
        If InStr(SCPI, " ") > 0 Then
            Cmd = Strings.Left(SCPI, InStr(SCPI, " ")) 'Include space to match SetCod$ array
            Arg = Mid(SCPI, InStr(SCPI, " ") + 1)
        Else
            Cmd = SCPI
            Arg = ""
        End If

        'Find match to a command index constant
        For i = 1 To MAX_SETTINGS
            If Cmd = SetCod(i) Then Exit For
        Next i

        With frmHpE1445a
            'OK, we have a recognized SCPI command. All are listed here but only
            'expected ones have been un-commented for error checking.
            sErrMsg = ""
            Select Case i
                Case ABOR
                    .tolOnOff.Buttons(0).Pushed = False
                    .tolOnOff.Buttons(1).Pushed = True
                Case ARM_COUN
                    .txtArmCoun.Text = Arg
                    frmHpE1445a.txtArmCoun_KeyPress(Keys.Return)
                Case ARM_LAY2_COUN
                    .txtArmLay2Coun.Text = Arg
                    .txtArmLay2Coun_KeyPress(Keys.Return)
                    'Case ARM_LAY2
                Case ARM_LAY2_SLOP
                    If Arg = "NEG" Then
                        .cboArmLay2Slop.SelectedIndex = 0
                    Else
                        .cboArmLay2Slop.SelectedIndex = 1
                    End If
                Case ARM_LAY2_SOUR
                    Select Case Arg
                        Case "BUS"
                            .cboArmLay2Sour.SelectedIndex = 0
                        Case "Bus"
                            .cboArmLay2Sour.SelectedIndex = 0
                        Case "ECLT0"
                            .cboArmLay2Sour.SelectedIndex = 1
                        Case "ECLTrg0"
                            .cboArmLay2Sour.SelectedIndex = 1
                        Case "ECLT1"
                            .cboArmLay2Sour.SelectedIndex = 2
                        Case "ECLTrg1"
                            .cboArmLay2Sour.SelectedIndex = 2
                        Case "EXT"
                            .cboArmLay2Sour.SelectedIndex = 3
                        Case "External"
                            .cboArmLay2Sour.SelectedIndex = 3
                        Case "HOLD"
                            .cboArmLay2Sour.SelectedIndex = 4
                        Case "Hold"
                            .cboArmLay2Sour.SelectedIndex = 4
                        Case "IMM"
                            .cboArmLay2Sour.SelectedIndex = 5
                        Case "Immediate"
                            .cboArmLay2Sour.SelectedIndex = 5

                        Case Else
                            If InStr(Arg, "TTLT") Then
                                .cboArmLay2Sour.SelectedIndex = CInt(Strings.Right(Arg, 1) + 6)
                            Else
                                sErrMsg = "Argument"
                            End If
                    End Select
                    'Case ARM_SWE_COUN
                    'Case ARM_SWE
                    'Case ARM_SWE_LINK
                Case ARM_SWE_SOUR
                    Select Case Arg
                        Case "BUS"
                            .cboAgileSweepTrigSour.SelectedIndex = 0
                        Case "Bus"
                            .cboAgileSweepTrigSour.SelectedIndex = 0
                        Case "HOLD"
                            .cboAgileSweepTrigSour.SelectedIndex = 1
                        Case "Hold"
                            .cboAgileSweepTrigSour.SelectedIndex = 1
                        Case "IMM"
                            .cboAgileSweepTrigSour.SelectedIndex = 2
                        Case "Immediate"
                            .cboAgileSweepTrigSour.SelectedIndex = 2
                        Case "LINK"
                            .cboAgileSweepTrigSour.SelectedIndex = 3
                        Case "Link"
                            .cboAgileSweepTrigSour.SelectedIndex = 3

                        Case Else
                            If InStr(Arg, "TTLT") Then
                                .cboAgileSweepTrigSour.SelectedIndex = CInt(Strings.Right(Arg, 1) + 4)
                            Else
                                sErrMsg = "Argument"
                            End If
                    End Select
                    'Case CAL_COUN1
                    'Case CAL_DATA_AC
                    'Case CAL_DATA_AC
                    'Case CAL_DATA
                    'Case CAL_BEG
                    'Case CAL_POIN
                    'Case CAL_SEC_CODE
                    'Case CAL_SEC
                    'Case CAL_STAT
                    'Case CAL_STAT_AC
                    'Case CAL_STAT_DC
                Case INIT
                    .tolOnOff.Buttons(0).Pushed = True
                    .tolOnOff.Buttons(1).Pushed = False
                Case OUTP_FILT_FREQ
                    Select Case Arg
                        Case "250 KHz"
                            .optCutOff250.Checked = True
                            .optCutOff250_Click(True)
                        Case "10 MHz"
                            .optCutOff10.Checked = True
                            .optCutOff10_Click(True)

                        Case Else
                            sErrMsg = "Argument"
                    End Select
                Case OUTP_FILT
                    Select Case Arg
                        Case "ON"
                            .chkFilterStateOn.Checked = True
                            .chkFilterStateOn_Click(True)
                        Case "OFF"
                            .chkFilterStateOn.Checked = False
                            .chkFilterStateOn_Click(False)

                        Case Else
                            sErrMsg = "Argument"
                    End Select
                Case OUTP_IMP, OUTP_LOAD
                    Select Case Arg
                        Case "INF"
                            .optLoadImpHigh.Checked = True
                            .optLoadImpHigh_Click(True)

                        Case Else
                            Select Case Decimal.Parse(Arg, Globalization.NumberStyles.Any)
                                Case 50
                                    .optLoadImp50.Checked = True
                                    .optLoadImp50_Click(True)
                                Case 75
                                    .optLoadImp75.Checked = True
                                    .optLoadImp75_Click(True)

                                Case Else
                                    sErrMsg = "Argument"
                            End Select
                    End Select
                Case OUTP_LOAD_AUTO
                    'OK, ignore. Doesn't affect any GUI settings
                Case OUTP
                    Select Case Arg
                        Case "ON"
                            .chkOutpStat.Checked = True
                            .chkOutpStat_Click(True)
                        Case "OFF"
                            .chkOutpStat.Checked = False
                            .chkOutpStat_Click(False)

                        Case Else
                            sErrMsg = "Argument"
                    End Select
                    'Case ARB_DAC_FORM
                    'Case ARB_DAC_SOUR
                    'Case ARB_DOWN
                    'Case ARB_DOWN_COMP
                    'Case FREQ_CENT
                Case FREQ, FREQ2
                    SetCur(FREQ) = Val(Arg)
                    .txtFreq.Text = EngNotate(SetCur(FREQ), FREQ)
                    '.txtFreq = Arg$
                    .txtFreq_KeyPress(Keys.Return)
                Case FREQ_FSK_L, FREQ_FSK_H
                    SetCur(FREQ_FSK_L) = Strings.Left(Arg, InStr(Arg, ",") - 1)
                    SetCur(FREQ_FSK_H) = Mid(Arg, InStr(Arg, ",") + 1)
                    .txtAgileFskKeyL.Text = EngNotate(SetCur(FREQ_FSK_L), FREQ)
                    .txtAgileFskKeyH.Text = EngNotate(SetCur(FREQ_FSK_H), FREQ)
                    .txtAgileFskKeyL_KeyPress(Keys.Return)
                    .txtAgileFskKeyH_KeyPress(Keys.Return)
                Case FREQ_FSK_SOUR
                    If Arg = "EXT" Then
                        .cboAgileFskSour.SelectedIndex = 0
                    ElseIf InStr(Arg, "TTLT") Then
                        .cboAgileFskSour.SelectedIndex = CInt(Strings.Right(Arg, 1) + 1)
                    Else
                        sErrMsg = "Argument"
                    End If
                Case FREQ_MODE
                    Select Case Arg
                        Case "FIX"
                            .optFreqModeFix.Checked = True
                            .optFreqModeFix_Click(True)
                        Case "SWE"
                            .optFreqModeSwe.Checked = True
                            .optFreqModeSwe_Click(True)
                        Case "FSK"
                            .optFreqModeFsk.Checked = True
                            .optFreqModeFsk_Click(True)

                        Case Else
                            sErrMsg = "Argument"
                    End Select
                Case FREQ_RANG
                    'OK, ignore. Doesn't affect any GUI settings
                    'Case FREQ_SPAN
                Case FREQ_STAR
                    SetCur(FREQ_STAR) = Val(Arg)
                    .txtAgileSweepStart.Text = EngNotate(SetCur(FREQ_STAR), FREQ)
                    .txtAgileSweepStart_KeyPress(Keys.Return)
                Case FREQ_STOP
                    SetCur(FREQ_STOP) = Val(Arg)
                    .txtAgileSweepStop.Text = EngNotate(SetCur(FREQ_STOP), FREQ)
                    .txtAgileSweepStop_KeyPress(Keys.Return)
                    'Case FREQ2
                Case FUNC
                    Dim buttonClickArgs As ToolBarButtonClickEventArgs = New ToolBarButtonClickEventArgs(.tolFuncWave.Buttons(0))
                    Select Case Arg
                        Case "SIN"
                            .tolFuncWave_ButtonClick(frmHpE1445a, buttonClickArgs)
                        Case "SQU"
                            buttonClickArgs.Button = .tolFuncWave.Buttons(1)
                            .tolFuncWave_ButtonClick(frmHpE1445a, buttonClickArgs)
                        Case "RAMP"
                            buttonClickArgs.Button = .tolFuncWave.Buttons(2)
                            .tolFuncWave_ButtonClick(frmHpE1445a, buttonClickArgs)
                        Case "TRI"
                            buttonClickArgs.Button = .tolFuncWave.Buttons(3)
                            .tolFuncWave_ButtonClick(frmHpE1445a, buttonClickArgs)
                        Case "DC"
                            buttonClickArgs.Button = .tolFuncWave.Buttons(4)
                            .tolFuncWave_ButtonClick(frmHpE1445a, buttonClickArgs)
                        Case "USER"
                            buttonClickArgs.Button = .tolFuncWave.Buttons(5)
                            .tolFuncWave_ButtonClick(frmHpE1445a, buttonClickArgs)
                        Case Else
                            sErrMsg = "Argument"
                    End Select
                Case FUNC_USER
                    SetCur(FUNC_USER) = Arg
                    'Case LIST_FORM
                    'Case LIST_ADDR
                    'Case LIST_CAT
                    'Case LIST_COMB
                    'Case LIST_COMB_POIN
                Case LIST_DEF
                    .txtUserPoints.Text = Arg
                    .txtUserPoints_KeyPress(Keys.Return)
                Case LIST_DEL_ALL
                    'OK, sent to ARB in SetControlsToReset
                    'Case LIST_DEL
                    'Case LIST_FREE
                    'Case LIST_MARK
                    'Case LIST_MARK_POIN
                Case LIST_MARK_SPO
                    SetCur(LIST_MARK_SPO) = Arg
                    .txtMarkSpo.Text = Val(SetCur(LIST_MARK_SPO))
                    .txtMarkSpo_KeyPress(Keys.Return)
                Case LIST_SEL
                    .lblListSel.Text = Arg
                    SetCur(LIST_SEL) = Arg
                    'Case LIST_VOLT
                    'Case LIST_VOLT_DAC
                    'Case LIST_VOLT_POIN
                    'Case LIST_SSEQ_ADDR
                    'Case LIST_SSEQ_CAT
                    'Case LIST_SSEQ_COMB
                    'Case LIST_SSEQ_COMB_POIN
                Case LIST_SSEQ_DEF
                    SetCur(LIST_SSEQ_DEF) = Arg
                Case LIST_SSEQ_DEL_ALL
                    'OK, sent to ARB in SetControlsToReset
                    'Case LIST_SSEQ_DEL
                Case LIST_SSEQ_DWEL_COUN
                    For i = 0 To StringToList(Arg, 0, sList, ",") - 1
                        .lstListSseqSeq.Items.Item(i) = Strings.Left(.lstListSseqSeq.Items.Item(i).ToString(), InStr(.lstListSseqSeq.Items.Item(i).ToString(), ",")) & " " & sList(i)
                    Next i
                    .txtDwelCoun.Text = sList(i - 1)
                    SetCur(LIST_SSEQ_DWEL_COUN) = Arg
                    'Case LIST_SSEQ_DWEL_COUN_POIN
                    'Case LIST_SSEQ_FREE
                    'Case LIST_SSEQ_MARK
                    'Case LIST_SSEQ_MARK_POIN
                    'Case LIST_SSEQ_MARK_SPO
                Case LIST_SSEQ_SEL
                    SetCur(LIST_SSEQ_SEL) = Arg
                Case LIST_SSEQ_SEQ
                    SetCur(LIST_SSEQ_SEQ) = Arg
                    ReDim sLoadedList(0)
                    For i = 1 To StringToList(Arg, 1, sList, ",")
                        .lstListSseqSeq.Items.Add(sList(i) & ", 1")
                        '- - - - New TIPs Section - - - - - - - - - - - - - - - - - - - - - - -
                        'This uses the argument for :LIST:SSEQ:SEQ to get the list of segment
                        'files that must be downloaded. However, a segment file may be used more
                        'than once in a sequence, but only needs to be downloaded once. So the
                        'sLoadedList() array and bLoaded flag are used to prevent unnecessary
                        'multiple segment file downloads.
                        If bRunningTipCmds Then
                            'Check to see if this segment file has already been downloaded
                            bLoaded = False
                            For n = LBound(sLoadedList) To UBound(sLoadedList)
                                If sList(i) = sLoadedList(n) Then
                                    bLoaded = True
                                    Exit For
                                End If
                            Next n
                            If Not bLoaded Then
                                ReDim Preserve sLoadedList(UBound(sLoadedList) + 1)
                                sLoadedList(UBound(sLoadedList)) = sList(i)
                                frmHpE1445a.txtUserName.Text = sList(i)
                                DoNotTalk = False
                                frmHpE1445a.cmdUserLoad_Click()
                                If Err.Number = 0 Then
                                    frmHpE1445a.tabOptions.SelectedIndex = TAB_USER 'Goto User Tab
                                    frmHpE1445a.cmdDownLoad_Click()
                                End If
                                DoNotTalk = True
                            End If
                        End If
                        '- - - - End of New TIPs Section - - - - - - - - - - - - - - - - - -
                    Next i
                    .lstListSseqSeq.SelectedIndex = i - 2
                    .lstListSseqSeq.Focus()
                    SetCur(LIST_SSEQ_SEQ) = Arg
                    'Case LIST_SSEQ_SEQ_SEG
                    'Case LIST2_FORM
                    'Case LIST2_FREQ
                    'Case LIST2_POIN
                Case MARK_ECLT0_FEED
                    'ARM,ARM:LAY2,FREQ:CHAN,LIST,PM:DEV:CHAN,ROSC,TRIG:SEQ
                    Select Case Arg
                        Case "ARM"
                            .cboMarkEclt0Feed.SelectedIndex = 0
                        Case Convert.ToString(Chr(34)) & "ARM" & Convert.ToString(Chr(34))
                            .cboMarkEclt0Feed.SelectedIndex = 0
                        Case "Arm:Start:Layer1"
                            .cboMarkEclt0Feed.SelectedIndex = 0
                        Case Convert.ToString(Chr(34)) & "Arm:Start:Layer1" & Convert.ToString(Chr(34))
                            .cboMarkEclt0Feed.SelectedIndex = 0
                        Case "ARM:LAY2"
                            .cboMarkEclt0Feed.SelectedIndex = 1
                        Case Convert.ToString(Chr(34)) & "ARM:LAY2" & Convert.ToString(Chr(34))
                            .cboMarkEclt0Feed.SelectedIndex = 1
                        Case "Arm:Start:Layer2"
                            .cboMarkEclt0Feed.SelectedIndex = 1
                        Case Convert.ToString(Chr(34)) & "Arm:Start:Layer2" & Convert.ToString(Chr(34))
                            .cboMarkEclt0Feed.SelectedIndex = 1
                        Case "FREQ:CHAN"
                            .cboMarkEclt0Feed.SelectedIndex = 2
                        Case Convert.ToString(Chr(34)) & "FREQ:CHAN" & Convert.ToString(Chr(34))
                            .cboMarkEclt0Feed.SelectedIndex = 2
                        Case "Freq:Change"
                            .cboMarkEclt0Feed.SelectedIndex = 2
                        Case Convert.ToString(Chr(34)) & "Freq:Change" & Convert.ToString(Chr(34))
                            .cboMarkEclt0Feed.SelectedIndex = 2
                        Case "LIST"
                            .cboMarkEclt0Feed.SelectedIndex = 3
                        Case Convert.ToString(Chr(34)) & "LIST" & Convert.ToString(Chr(34))
                            .cboMarkEclt0Feed.SelectedIndex = 3
                        Case "List"
                            .cboMarkEclt0Feed.SelectedIndex = 3
                        Case Convert.ToString(Chr(34)) & "List" & Convert.ToString(Chr(34))
                            .cboMarkEclt0Feed.SelectedIndex = 3
                        Case "PM:DEV:CHAN"
                            .cboMarkEclt0Feed.SelectedIndex = 4
                        Case Convert.ToString(Chr(34)) & "PM:DEV:CHAN" & Convert.ToString(Chr(34))
                            .cboMarkEclt0Feed.SelectedIndex = 4
                        Case "PM:Dev:Change"
                            .cboMarkEclt0Feed.SelectedIndex = 4
                        Case Convert.ToString(Chr(34)) & "PM:Dev:Change" & Convert.ToString(Chr(34))
                            .cboMarkEclt0Feed.SelectedIndex = 4
                        Case "ROSC"
                            .cboMarkEclt0Feed.SelectedIndex = 5
                        Case Convert.ToString(Chr(34)) & "ROSC" & Convert.ToString(Chr(34))
                            .cboMarkEclt0Feed.SelectedIndex = 5
                        Case "ROscillator"
                            .cboMarkEclt0Feed.SelectedIndex = 5
                        Case Convert.ToString(Chr(34)) & "ROscillator" & Convert.ToString(Chr(34))
                            .cboMarkEclt0Feed.SelectedIndex = 5
                        Case "TRIG:SEQ"
                            .cboMarkEclt0Feed.SelectedIndex = 6
                        Case Convert.ToString(Chr(34)) & "TRIG:SEQ" & Convert.ToString(Chr(34))
                            .cboMarkEclt0Feed.SelectedIndex = 6
                        Case "Trigger:Start"
                            .cboMarkEclt0Feed.SelectedIndex = 6
                        Case Convert.ToString(Chr(34)) & "Trigger:Start" & Convert.ToString(Chr(34))
                            .cboMarkEclt0Feed.SelectedIndex = 6

                        Case Else
                            sErrMsg = "Argument"
                    End Select
                Case MARK_ECLT0
                    Select Case Arg
                        Case "ON"
                            .chkMarkEclt0.Checked = True
                            .chkMarkEclt0_Click(True)
                        Case "OFF"
                            .chkMarkEclt0.Checked = False
                            .chkMarkEclt0_Click(False)

                        Case Else
                            sErrMsg = "Argument"
                    End Select
                Case MARK_ECLT1_FEED
                    'ARM,ARM:LAY2,FREQ:CHAN,LIST,PM:DEV:CHAN,ROSC,TRIG:SEQ
                    Select Case Arg
                        Case "ARM"
                            .cboMarkEclt1Feed.SelectedIndex = 0
                        Case Convert.ToString(Chr(34)) & "ARM" & Convert.ToString(Chr(34))
                            .cboMarkEclt1Feed.SelectedIndex = 0
                        Case "Arm:Start:Layer1"
                            .cboMarkEclt1Feed.SelectedIndex = 0
                        Case Convert.ToString(Chr(34)) & "Arm:Start:Layer1" & Convert.ToString(Chr(34))
                            .cboMarkEclt1Feed.SelectedIndex = 0
                        Case "ARM:LAY2"
                            .cboMarkEclt1Feed.SelectedIndex = 1
                        Case Convert.ToString(Chr(34)) & "ARM:LAY2" & Convert.ToString(Chr(34))
                            .cboMarkEclt1Feed.SelectedIndex = 1
                        Case "Arm:Start:Layer2"
                            .cboMarkEclt1Feed.SelectedIndex = 1
                        Case Convert.ToString(Chr(34)) & "Arm:Start:Layer2" & Convert.ToString(Chr(34))
                            .cboMarkEclt1Feed.SelectedIndex = 1
                        Case "FREQ:CHAN"
                            .cboMarkEclt1Feed.SelectedIndex = 2
                        Case Convert.ToString(Chr(34)) & "FREQ:CHAN" & Convert.ToString(Chr(34))
                            .cboMarkEclt1Feed.SelectedIndex = 2
                        Case "Freq:Change"
                            .cboMarkEclt1Feed.SelectedIndex = 2
                        Case Convert.ToString(Chr(34)) & "Freq:Change" & Convert.ToString(Chr(34))
                            .cboMarkEclt1Feed.SelectedIndex = 2
                        Case "LIST"
                            .cboMarkEclt1Feed.SelectedIndex = 3
                        Case Convert.ToString(Chr(34)) & "LIST" & Convert.ToString(Chr(34))
                            .cboMarkEclt1Feed.SelectedIndex = 3
                        Case "List"
                            .cboMarkEclt1Feed.SelectedIndex = 3
                        Case Convert.ToString(Chr(34)) & "List" & Convert.ToString(Chr(34))
                            .cboMarkEclt1Feed.SelectedIndex = 3
                        Case "PM:DEV:CHAN"
                            .cboMarkEclt1Feed.SelectedIndex = 4
                        Case Convert.ToString(Chr(34)) & "PM:DEV:CHAN" & Convert.ToString(Chr(34))
                            .cboMarkEclt1Feed.SelectedIndex = 4
                        Case "PM:Dev:Change"
                            .cboMarkEclt1Feed.SelectedIndex = 4
                        Case Convert.ToString(Chr(34)) & "PM:Dev:Change" & Convert.ToString(Chr(34))
                            .cboMarkEclt1Feed.SelectedIndex = 4
                        Case "ROSC"
                            .cboMarkEclt1Feed.SelectedIndex = 5
                        Case Convert.ToString(Chr(34)) & "ROSC" & Convert.ToString(Chr(34))
                            .cboMarkEclt1Feed.SelectedIndex = 5
                        Case "ROscillator"
                            .cboMarkEclt1Feed.SelectedIndex = 5
                        Case Convert.ToString(Chr(34)) & "ROscillator" & Convert.ToString(Chr(34))
                            .cboMarkEclt1Feed.SelectedIndex = 5
                        Case "TRIG:SEQ"
                            .cboMarkEclt1Feed.SelectedIndex = 6
                        Case Convert.ToString(Chr(34)) & "TRIG:SEQ" & Convert.ToString(Chr(34))
                            .cboMarkEclt1Feed.SelectedIndex = 6
                        Case "Trigger:Start"
                            .cboMarkEclt1Feed.SelectedIndex = 6
                        Case Convert.ToString(Chr(34)) & "Trigger:Start" & Convert.ToString(Chr(34))
                            .cboMarkEclt1Feed.SelectedIndex = 6

                        Case Else
                            sErrMsg = "Argument"
                    End Select
                Case MARK_ECLT1
                    Select Case Arg
                        Case "ON"
                            .chkMarkEclt1.Checked = True
                            .chkMarkEclt1_Click(True)
                        Case "OFF"
                            .chkMarkEclt1.Checked = False
                            .chkMarkEclt1_Click(False)

                        Case Else
                            sErrMsg = "Argument"
                    End Select
                Case MARK_FEED
                    'ARM,ARM:LAY2,FREQ:CHAN,LIST,PM:DEV:CHAN,ROSC,TRIG:SEQ
                    Select Case Arg
                        Case "ARM"
                            .cboMarkEclt1Feed.SelectedIndex = 0
                        Case Convert.ToString(Chr(34)) & "ARM" & Convert.ToString(Chr(34))
                            .cboMarkEclt1Feed.SelectedIndex = 0
                        Case "Arm:Start:Layer1"
                            .cboMarkEclt1Feed.SelectedIndex = 0
                        Case Convert.ToString(Chr(34)) & "Arm:Start:Layer1" & Convert.ToString(Chr(34))
                            .cboMarkEclt1Feed.SelectedIndex = 0
                        Case "ARM:LAY2"
                            .cboMarkEclt1Feed.SelectedIndex = 1
                        Case Convert.ToString(Chr(34)) & "ARM:LAY2" & Convert.ToString(Chr(34))
                            .cboMarkEclt1Feed.SelectedIndex = 1
                        Case "Arm:Start:Layer2"
                            .cboMarkEclt1Feed.SelectedIndex = 1
                        Case Convert.ToString(Chr(34)) & "Arm:Start:Layer2" & Convert.ToString(Chr(34))
                            .cboMarkEclt1Feed.SelectedIndex = 1
                        Case "FREQ:CHAN"
                            .cboMarkEclt1Feed.SelectedIndex = 2
                        Case Convert.ToString(Chr(34)) & "FREQ:CHAN" & Convert.ToString(Chr(34))
                            .cboMarkEclt1Feed.SelectedIndex = 2
                        Case "Freq:Change"
                            .cboMarkEclt1Feed.SelectedIndex = 2
                        Case Convert.ToString(Chr(34)) & "Freq:Change" & Convert.ToString(Chr(34))
                            .cboMarkEclt1Feed.SelectedIndex = 2
                        Case "LIST"
                            .cboMarkEclt1Feed.SelectedIndex = 3
                        Case Convert.ToString(Chr(34)) & "LIST" & Convert.ToString(Chr(34))
                            .cboMarkEclt1Feed.SelectedIndex = 3
                        Case "List"
                            .cboMarkEclt1Feed.SelectedIndex = 3
                        Case Convert.ToString(Chr(34)) & "List" & Convert.ToString(Chr(34))
                            .cboMarkEclt1Feed.SelectedIndex = 3
                        Case "PM:DEV:CHAN"
                            .cboMarkEclt1Feed.SelectedIndex = 4
                        Case Convert.ToString(Chr(34)) & "PM:DEV:CHAN" & Convert.ToString(Chr(34))
                            .cboMarkEclt1Feed.SelectedIndex = 4
                        Case "PM:Dev:Change"
                            .cboMarkEclt1Feed.SelectedIndex = 4
                        Case Convert.ToString(Chr(34)) & "PM:Dev:Change" & Convert.ToString(Chr(34))
                            .cboMarkEclt1Feed.SelectedIndex = 4
                        Case "ROSC"
                            .cboMarkEclt1Feed.SelectedIndex = 5
                        Case Convert.ToString(Chr(34)) & "ROSC" & Convert.ToString(Chr(34))
                            .cboMarkEclt1Feed.SelectedIndex = 5
                        Case "ROscillator"
                            .cboMarkEclt1Feed.SelectedIndex = 5
                        Case Convert.ToString(Chr(34)) & "ROscillator" & Convert.ToString(Chr(34))
                            .cboMarkEclt1Feed.SelectedIndex = 5
                        Case "TRIG:SEQ"
                            .cboMarkEclt1Feed.SelectedIndex = 6
                        Case Convert.ToString(Chr(34)) & "TRIG:SEQ" & Convert.ToString(Chr(34))
                            .cboMarkEclt1Feed.SelectedIndex = 6
                        Case "Trigger:Start"
                            .cboMarkEclt1Feed.SelectedIndex = 6
                        Case Convert.ToString(Chr(34)) & "Trigger:Start" & Convert.ToString(Chr(34))
                            .cboMarkEclt1Feed.SelectedIndex = 6

                        Case Else
                            sErrMsg = "Argument"
                    End Select
                Case MARK_POL
                    If Arg = "INV" Then
                        .cboMarkPol.SelectedIndex = 0
                    Else
                        .cboMarkPol.SelectedIndex = 1
                    End If
                Case MARK
                    Select Case Arg
                        Case "ON"
                            .chkMark.Checked = True
                            .chkMark_Click(True)
                        Case "OFF"
                            .chkMark.Checked = False
                            .chkMark_Click(False)

                        Case Else
                            sErrMsg = "Argument"
                    End Select
                    'Case PM
                    'Case PM_SOUR
                    'Case PM_STAT
                    'Case PM_UNIT
                Case RAMP_POL
                    If Arg = "INV" Then
                        .cboWavePol.SelectedIndex = 1
                    Else
                        .cboWavePol.SelectedIndex = 0 'NORM
                    End If
                Case RAMP_POIN
                    SetDef(RAMP_POIN) = Arg
                    .txtWavePoin.Text = EngNotate(Val(SetDef(RAMP_POIN)), RAMP_POIN)
                    'SendCommand SetIdx%
                    .txtWavePoin_KeyPress(Keys.Return)
                Case ROSC_FREQ_EXT
                    SetCur(ROSC_FREQ_EXT) = Val(Arg)
                    RefOscFreq = Val(SetCur(ROSC_FREQ_EXT))
                    .txtRoscFreqExt.Text = EngNotate(RefOscFreq, ROSC_FREQ_EXT)
                    .txtRoscFreqExt_KeyPress(Keys.Return)
                Case ROSC_SOUR
                    Select Case Arg
                        Case "CLK10"
                            .cboRoscSour.SelectedIndex = 0
                        Case "Clk10"
                            .cboRoscSour.SelectedIndex = 0
                        Case "EXT"
                            .cboRoscSour.SelectedIndex = 1
                        Case "External"
                            .cboRoscSour.SelectedIndex = 1
                        Case "ECLT0"
                            .cboRoscSour.SelectedIndex = 2
                        Case "ECLTrg0"
                            .cboRoscSour.SelectedIndex = 2
                        Case "ECLT1"
                            .cboRoscSour.SelectedIndex = 3
                        Case "ECLTrg1"
                            .cboRoscSour.SelectedIndex = 3
                        Case "INT"
                            .cboRoscSour.SelectedIndex = 4
                        Case "Internal1"
                            .cboRoscSour.SelectedIndex = 4
                        Case "INT2"
                            .cboRoscSour.SelectedIndex = 5
                        Case "Internal2"
                            .cboRoscSour.SelectedIndex = 5

                        Case Else
                            sErrMsg = "Argument"
                    End Select

                Case SWE_COUN
                    SetCur(SWE_COUN) = Val(Arg)
                    .txtSweCoun.Text = Arg
                    .txtSweCoun_KeyPress(Keys.Return)
                Case SWE_DIR
                    If Arg = "UP" Then
                        .cboAgileSweepDir.SelectedIndex = 1
                    Else
                        .cboAgileSweepDir.SelectedIndex = 0
                    End If
                Case SWE_POIN
                    SetCur(SWE_POIN) = Val(Arg)
                    .txtAgileSweepSteps.Text = EngNotate(SetCur(SWE_POIN), SWE_POIN)
                    .txtAgileSweepSteps_KeyPress(Keys.Return)
                Case SWE_SPAC
                    If Arg = "LIN" Then
                        .cboAgileSweepSpac.SelectedIndex = 0
                    Else
                        .cboAgileSweepSpac.SelectedIndex = 1
                    End If
                Case SWE_TIME
                    SetCur(SWE_TIME) = Val(Arg)
                    .txtAgileSweepDur.Text = EngNotate(SetCur(SWE_TIME), SWE_TIME)
                    .txtAgileSweepDur_KeyPress(Keys.Return)
                Case VOLT
                    SetCur(VOLT) = Val(Arg)
                    .txtAmpl.Text = EngNotate(SetCur(VOLT), VOLT)
                    ' need to call to set /7 ampl
                    SetDiv7FromAmpl()
                    .txtAmpl_KeyPress(Keys.Return)
                Case VOLT_UNIT
                    .cboUnits.Text = Arg
                Case VOLT_OFFS
                    SetCur(VOLT_OFFS) = Val(Arg)
                    .txtDcOffs.Text = EngNotate(SetCur(VOLT_OFFS), VOLT)
                    .txtDcOffs_KeyPress(Keys.Return)
                    'Case STAT_OPC_INIT
                Case STAT_OPER_COND
                    'status register bit0=cal, bit3=sweep, bit6=waitARM, bit8=init
                    'Handle only the init bit
                    If Val(Arg) / 128 >= 1 Then
                        .ribInit.Pushed = True
                        .ribAbor.Pushed = False
                    Else
                        frmHpE1445a.ribAbor_Click()
                        .ribInit.Pushed = False
                        .ribAbor.Pushed = True
                    End If
                    'Case STAT_OPER_ENAB
                    'Case STAT_OPER
                    'Case STAT_OPER_NTR
                    'Case STAT_OPER_PTR
                    'Case STAT_PRES
                    'Case SYST_ERR
                    'Case SYST_VER
                    'Case TRIG_COUN
                Case TRIG_GATE_POL
                    If Arg = "INV" Then
                        .cboTrigGatePol.SelectedIndex = 0
                    Else
                        .cboTrigGatePol.SelectedIndex = 1
                    End If
                Case TRIG_GATE_SOUR
                    If Arg = "EXT" Then
                        .cboTrigGateSour.SelectedIndex = 0
                    ElseIf InStr(Arg, "TTLT") Then
                        .cboTrigGateSour.SelectedIndex = CInt(Strings.Right(Arg, 1) + 1)
                    Else
                        sErrMsg = "Argument"
                    End If
                Case TRIG_GATE_STAT
                    Select Case Arg
                        Case "ON"
                            .chkTrigGateStat_Click(True)
                        Case "OFF"
                            .chkTrigGateStat_Click(False)

                        Case Else
                            sErrMsg = "Argument"
                    End Select
                    'Case TRIG
                Case TRIG_SLOP
                    If Arg = "NEG" Then
                        .cboTrigSlop.SelectedIndex = 0
                    Else
                        .cboTrigSlop.SelectedIndex = 1
                    End If
                Case TRIG_SOUR
                    Select Case Arg
                        Case "BUS"
                            .cboTrigSour.SelectedIndex = 0
                        Case "Bus"
                            .cboTrigSour.SelectedIndex = 0
                        Case "ECLT0"
                            .cboTrigSour.SelectedIndex = 1
                        Case "ECLTrg0"
                            .cboTrigSour.SelectedIndex = 1
                        Case "ECLT1"
                            .cboTrigSour.SelectedIndex = 2
                        Case "ECLTrg1"
                            .cboTrigSour.SelectedIndex = 2
                        Case "EXT"
                            .cboTrigSour.SelectedIndex = 3
                        Case "External"
                            .cboTrigSour.SelectedIndex = 3
                        Case "HOLD"
                            .cboTrigSour.SelectedIndex = 4
                        Case "Hold"
                            .cboTrigSour.SelectedIndex = 4
                        Case "INT"
                            .cboTrigSour.SelectedIndex = 5
                        Case "Internal1"
                            .cboTrigSour.SelectedIndex = 5
                        Case "INT2"
                            .cboTrigSour.SelectedIndex = 6
                        Case "Internal2"
                            .cboTrigSour.SelectedIndex = 6

                        Case Else
                            If InStr(Arg, "TTLT") Then
                                .cboTrigSour.SelectedIndex = CInt(Strings.Right(Arg, 1) + 7)
                            Else
                                sErrMsg = "Argument"
                            End If
                    End Select
                    'Case TRIG_STOP
                Case TRIG_STOP_SLOP
                    If Arg = "NEG" Then
                        .cboTrigStopSlop.SelectedIndex = 0
                    Else
                        .cboTrigStopSlop.SelectedIndex = 1
                    End If
                Case TRIG_STOP_SOUR
                    Select Case Arg
                        Case "BUS"
                            .cboTrigStopSour.SelectedIndex = 0
                        Case "Bus"
                            .cboTrigStopSour.SelectedIndex = 0
                        Case "EXT"
                            .cboTrigStopSour.SelectedIndex = 1
                        Case "External"
                            .cboTrigStopSour.SelectedIndex = 1
                        Case "HOLD"
                            .cboTrigStopSour.SelectedIndex = 2
                        Case "Hold"
                            .cboTrigStopSour.SelectedIndex = 2

                        Case Else
                            If InStr(Arg, "TTLT") Then
                                .cboTrigStopSour.SelectedIndex = CInt(Strings.Right(Arg, 1) + 3)
                            Else
                                sErrMsg = "Argument"
                            End If
                    End Select
                    'Case TRIG_SWE
                    'Case TRIG_SWE_LINK
                Case TRIG_SWE_SOUR
                    Select Case Arg
                        Case "BUS"
                            .cboAgileSweepAdvSour.SelectedIndex = 0
                        Case "Bus"
                            .cboAgileSweepAdvSour.SelectedIndex = 0
                        Case "HOLD"
                            .cboAgileSweepAdvSour.SelectedIndex = 1
                        Case "Hold"
                            .cboAgileSweepAdvSour.SelectedIndex = 1
                        Case "LINK"
                            .cboAgileSweepAdvSour.SelectedIndex = 2
                        Case "Link"
                            .cboAgileSweepAdvSour.SelectedIndex = 2
                        Case "TIM"
                            .cboAgileSweepAdvSour.SelectedIndex = 3
                        Case "Timer"
                            .cboAgileSweepAdvSour.SelectedIndex = 3

                        Case Else
                            If InStr(Arg, "TTLT") Then
                                .cboAgileSweepAdvSour.SelectedIndex = CInt(Strings.Right(Arg, 1) + 4)
                            Else
                                sErrMsg = "Argument"
                            End If
                    End Select
                    'Case TRIG_SWE_TIM
                    'Case VINS_LBUS
                    'Case VINS_LBUS_AUTO
                    'Case VINS_TEST_CONF
                    'Case VINS_TEST_DATA
                    'Case VINS_VME
                    'Case VINS_VME_REC_ADDR_DATA
                    'Case VINS_VME_REC_ADDR_READ
                    'Case VINS_IDEN

                Case Else
                    sErrMsg = "Command"
            End Select

            If sErrMsg <> "" Then
                If Not (sTipMode = "GET CURR CONFIG") Then
                    sErrMsg = "Unexpected " & sErrMsg & ": " & SCPI
                    If InStr(sTipMode, "TIP_Run") Then
                        SetKey("TIPS", "STATUS", "Error from ARB SAIS: " & sErrMsg)
                        .cmdQuit_Click()
                    End If
                    bActivateControl = False
                    MsgBox(sErrMsg & vbCrLf & vbCrLf & "Instrument will be set to RESET state.")
                    .cmdReset_Click()
                Else
                    sErrMsg = "Invalid <" & SCPI & "> argument received from instrument query."
                    MsgBox(sErrMsg, MsgBoxStyle.OkOnly, "Get Current - Unhandled Parameter")
                End If
            End If

        End With
    End Function

    Function BuildXML(sParam As String) As String
        BuildXML = "<AtXmlSignalDescription xmlns:atXml=""ATXML_TSF"">"
        BuildXML = BuildXML & "<SignalAction>" & sParam & "</SignalAction>"
        BuildXML = BuildXML & "<SignalResourceName>" & RESOURCE_NAME & "</SignalResourceName>"
        BuildXML = BuildXML & "</AtXmlSignalDescription>"
    End Function

    Sub AdjustFreqLimits()
        'Call after changing:
        '   FUNC, TRIG_SOUR, RAMP_POIN, ROSC_SOUR
        'See HP E1445A User's Manual page B-8 and 8-40

        Select Case frmHpE1445a.tolFuncWave.Buttons(WaveIdx - 1).Name
            Case "Sine"
                SetMin(FREQ) = "0"
                SetMax(FREQ) = Round(RefOscFreq / 4, 6)
            Case "Square"
                If CurFreqIdx = FREQ Then
                    SetMin(FREQ) = "0"
                    SetMax(FREQ) = Round((RefOscFreq / 16) * 2, 6)
                ElseIf CurFreqIdx = FREQ2 Then
                    SetMin(FREQ2) = Round(RefOscFreq / 4 / 131072, 8)
                    SetMax(FREQ2) = Round(RefOscFreq / 4, 8)
                End If
            Case "Ramp", "Triangle"
                If CurFreqIdx = FREQ Then
                    SetMin(FREQ) = "0"
                    SetMax(FREQ) = Round(RefOscFreq / 4 / Val(SetCur(RAMP_POIN)) * 2, 8)
                ElseIf CurFreqIdx = FREQ2 Then
                    SetMin(FREQ2) = Round(RefOscFreq / 131072 / Val(SetCur(RAMP_POIN)), 8)
                    SetMax(FREQ2) = Round(RefOscFreq / Val(SetCur(RAMP_POIN)), 8)
                End If
            Case "User"
                If CurFreqIdx = FREQ Then
                    SetMin(FREQ) = "0"
                    SetMax(FREQ) = RefOscFreq / 4 * 2
                ElseIf CurFreqIdx = FREQ2 Then
                    SetMin(FREQ2) = Round(RefOscFreq / 131072, 8)
                    SetMax(FREQ2) = RefOscFreq
                End If
            Case "DC"

            Case Else
                MsgBox("Unsupported selection in AdjustFreqLimits procedure: " & frmHpE1445a.tolFuncWave.Buttons(WaveIdx - 1).Name)
        End Select

        Void = AdjustToLimit(CurFreqIdx, frmHpE1445a.txtFreq)

        frmHpE1445a.txtSampleFreq.Text = frmHpE1445a.txtFreq.Text
        AdjustAgileLimits()
    End Sub

    Sub AdjustAgileLimits()
        'Call after changing:
        '   FUNC, ROSC_SOUR, TRIG_SOUR, SWE_SPAC, FREQ_MODE
        'See HP E1445A User's Manual page 8-40

        SetMin(FREQ_STAR) = "0"
        If CurFreqIdx = FREQ Then
            SetMax(FREQ_STAR) = SetMax(FREQ)
            SetCur(FREQ_RANG) = SetCur(FREQ)
        ElseIf CurFreqIdx = FREQ2 Then
            SetMax(FREQ_STAR) = SetMax(FREQ2)
            SetCur(FREQ_RANG) = SetCur(FREQ2)
        End If

        'Special case, see manual page 8-41
        If UCase(Strings.Left(SetCur(SWE_SPAC), 3)) = "LOG" Then
            SetMin(FREQ_STAR) = Round(Truncate(SetMax(FREQ_STAR) / 1073741824), 6)
        End If

        'Get all set
        SetMin(FREQ_STOP) = SetMin(FREQ_STAR)
        SetMax(FREQ_STOP) = SetMax(FREQ_STAR)
        SetMin(FREQ_FSK_L) = SetMin(FREQ_STAR)
        SetMin(FREQ_FSK_H) = SetMin(FREQ_FSK_L)
        SetMax(FREQ_FSK_L) = SetMax(FREQ_STAR)
        SetMax(FREQ_FSK_H) = SetMax(FREQ_FSK_L)

        'Ensure Sweep Start is lower than Sweep Stop
        If Val(SetMax(FREQ_STAR)) < Val(SetCur(FREQ_STAR)) Then
            SetMin(FREQ_STOP) = SetMax(FREQ_STAR)
        Else
            SetMin(FREQ_STOP) = SetCur(FREQ_STAR)
        End If
        If Val(SetMax(FREQ_STAR)) >= Val(SetCur(FREQ_STOP)) Then
            SetMax(FREQ_STAR) = SetCur(FREQ_STOP)
        End If

        'Set new value if current val is out of new limits.
        AdjustToLimit(FREQ_STAR, frmHpE1445a.txtAgileSweepStart)
        AdjustToLimit(FREQ_STOP, frmHpE1445a.txtAgileSweepStop)
        AdjustToLimit(FREQ_FSK_L, frmHpE1445a.txtAgileFskKeyL)
        AdjustToLimit(FREQ_FSK_H, frmHpE1445a.txtAgileFskKeyH)

        'Set FREQ:RANG to the highest pertinent frequency setting to enable
        'automatic frequency doubling.
        If frmHpE1445a.optFreqModeSwe.Checked Then
            SetCur(FREQ_RANG) = SetCur(FREQ_STOP)
        ElseIf frmHpE1445a.optFreqModeFsk.Checked Then
            If Val(SetCur(FREQ_FSK_L)) > Val(SetCur(FREQ_FSK_H)) Then
                SetCur(FREQ_RANG) = SetCur(FREQ_FSK_L)
            Else
                SetCur(FREQ_RANG) = SetCur(FREQ_FSK_H)
            End If
        End If
    End Sub

    Sub AdjustSweepTime()
        SetMin(SWE_TIME) = 0.00125 * (Val(SetCur(SWE_POIN)) - 1)
        SetMax(SWE_TIME) = 4.19430375 * (Val(SetCur(SWE_POIN)) - 1)
        Void = AdjustToLimit(SWE_TIME, frmHpE1445a.txtAgileSweepDur)
    End Sub

    Function GetFromQuotes(ByVal S As String) As String
        GetFromQuotes = ""
        Dim Rtn As String = "", i As Integer

        For i = 1 To Convert.ToString(S).Length
            If Mid(Convert.ToString(S), i, 1) = Quote Then Exit For
        Next i
        If i > Convert.ToString(S).Length Then
            GetFromQuotes = ""
            Exit Function
        End If
        For i = i + 1 To Convert.ToString(S).Length
            If Mid(Convert.ToString(S), i, 1) = Quote Then Exit For
            Rtn &= Mid(Convert.ToString(S), i, 1)
        Next i
        i += 1
        GetFromQuotes = Rtn
    End Function

    Function sGetTipCmds() As String
        sGetTipCmds = ""
        'DESCRIPTION:
        '   This function creates a complete syntactically correct command string
        '   that represents the current state of the SAIS and ARB. The commands
        '   are gathered in a specific order, as in Sub cmdConfigSave_Click. See
        '   the comments in that sub for specific details.
        'PARAMETERS:
        '   None
        'RETURNS:
        '   A string variable containing the command string.
        'GLOBAL VARIABLES USED:
        '   bActivatingControls
        'EXAMPLE:
        '   sTipCmds = sGetTipCmds()

        Dim i As Short

        HelpPanel("Please wait, Accumulating commands")
        frmHpE1445a.Cursor = Cursors.WaitCursor
        bActivatingControls = True

        'Write non-default ARB commands as per current front panel state
        'Print #FileId, "[PRE_SEGMENT_LOAD_COMMANDS]"
        sGetTipCmds = "*RST;*CLS"

        'Loop through non-coupled commands
        For i = LAST_COUP_GROUP_3_COMMAND + 1 To MAX_SETTINGS
            Select Case i
                    'Don't write these commands until after segments have been down-loaded
                Case LIST_SEL, LIST_DEF, LIST_VOLT, LIST_SSEQ_DEF, LIST_SSEQ_SEL, LIST_SSEQ_DWEL_COUN, LIST_SSEQ_SEQ, FUNC_USER, MARK_ECLT0_FEED, MARK_ECLT1_FEED, MARK_FEED, LIST_MARK, LIST_MARK_SPO
                    'Don't write these commands because they are not 'STATE' commands
                    'Advint disrgards:            Case ARB_DOWN, ARB_DOWN_COMP
                    'Advint disrgards:                'Do nothing

                Case Else
                    'OK, process these
                    If SetCod(i) <> "" And (SetCur(i) <> SetDef(i)) Then
                        sGetTipCmds = sGetTipCmds & ";" & SetCod(i) & SetCur(i)
                    End If
            End Select
        Next i

        'Combine Group 1 & 2 commands, with Group 2 going first
        For i = LAST_COUP_GROUP_1_COMMAND + 1 To LAST_COUP_GROUP_2_COMMAND
            If SetCur(i) <> SetDef(i) Then
                sGetTipCmds = sGetTipCmds & ";" & SetCod(i) & SetCur(i)
            End If
        Next i
        For i = 1 To LAST_COUP_GROUP_1_COMMAND
            If SetCur(i) <> SetDef(i) Then
                If i = FREQ_FSK_L Or i = FREQ_FSK_H Then
                    sGetTipCmds = sGetTipCmds & ";" & SetCod(i) & SetCur(FREQ_FSK_L) & "," & SetCur(FREQ_FSK_H)
                Else
                    sGetTipCmds = sGetTipCmds & ";" & SetCod(i) & SetCur(i)
                End If
            End If
        Next i

        'Loop through Group 3 commands
        For i = LAST_COUP_GROUP_2_COMMAND + 1 To LAST_COUP_GROUP_3_COMMAND
            If i = VOLT Then 'Always add Volt in case output impedance is high and volt=def
                sGetTipCmds = sGetTipCmds & ";" & SetCod(i) & SetCur(i)
            ElseIf SetCur(i) <> SetDef(i) Then
                sGetTipCmds = sGetTipCmds & ";" & SetCod(i) & SetCur(i)
            End If
        Next i

        'Send commands to setup loading segments
        sGetTipCmds = sGetTipCmds & ";" & SetCod(LIST_SSEQ_DEL_ALL) 'Delete all sequences
        sGetTipCmds = sGetTipCmds & ";" & SetCod(LIST_DEL_ALL) 'Delete all segments

        'Write Sequence definition commands (to be executed after segments are down-loaded to ARB)
        If frmHpE1445a.lstListSseqSeq.Items.Count > 0 Then 'If sequence list not empty
            sGetTipCmds = sGetTipCmds & ";" & SetCod(LIST_SSEQ_SEL) & SetCur(LIST_SSEQ_SEL)
            SetCur(LIST_SSEQ_DEF) = frmHpE1445a.lstListSseqSeq.Items.Count
            sGetTipCmds = sGetTipCmds & ";" & SetCod(LIST_SSEQ_DEF) & SetCur(LIST_SSEQ_DEF)
            sGetTipCmds = sGetTipCmds & ";" & SetCod(LIST_SSEQ_SEQ) & SetCur(LIST_SSEQ_SEQ)
            sGetTipCmds = sGetTipCmds & ";" & SetCod(LIST_SSEQ_DWEL_COUN) & SetCur(LIST_SSEQ_DWEL_COUN)
            sGetTipCmds = sGetTipCmds & ";" & SetCod(FUNC_USER) & SetCur(FUNC_USER)
        End If

        'Write Marker Feed commands last in case the feed source is a waveform point.
        For i = LAST_COUP_GROUP_3_COMMAND + 1 To MAX_SETTINGS
            Select Case i
                Case MARK_ECLT0_FEED, MARK_ECLT1_FEED, MARK_FEED
                    If SetCur(i) <> SetDef(i) Then
                        sGetTipCmds = sGetTipCmds & ";" & SetCod(i) & Quote & SetCur(i) & Quote
                    End If
                Case LIST_MARK, LIST_MARK_SPO
                    If SetCur(i) <> SetDef(i) Then
                        sGetTipCmds = sGetTipCmds & ";" & SetCod(i) & SetCur(i)
                    End If
            End Select
        Next i

        If frmHpE1445a.ribInit.Pushed = True Then
            sGetTipCmds = sGetTipCmds & ";" & SetCod(INIT)
        End If

        frmHpE1445a.Cursor = Cursors.Default
        HelpPanel("")
        bActivatingControls = False
    End Function

    Public Sub PlotLineInWave(ByVal EndX As Double, ByRef EndY As Double)
        Dim i As Integer, Inc As Single, Accum As Single, T1 As Integer, T2 As Integer, StepDir As Short
        Static StartX As Single, StartY As Single

        StartX = frmHpE1445a.CursorFrom.XPosition
        StartY = frmHpE1445a.CursorFrom.YPosition

        T1 = Fix(StartX + 0.5)
        T2 = Fix(EndX + 0.5)
        'If T1 = T2 Then Exit Sub
        If T1 <> T2 Then 'V1.2 JHill
            If T2 < T1 Then
                StepDir = -1
            Else
                StepDir = 1
            End If

            If Wave.Length < T2 Then
                ReDim Preserve Wave(T2)
            End If

            Accum = StartY
            Inc = (EndY - StartY) / Math.Abs(T2 - T1)
            For i = T1 To T2 Step StepDir
                Wave(i) = Accum
                Accum += Inc
            Next i
        Else            'V1.2 JHill
            Wave(T1) = EndY 'V1.2 JHill
        End If
        frmHpE1445a.G1.PlotY(Wave, 1, 1)
        frmHpE1445a.CursorFrom.XPosition = EndX
        frmHpE1445a.CursorFrom.YPosition = EndY
    End Sub

    Public Sub AdjustVoltLimits(ByVal ParameterChanged As Short)
        'Sets VOLT min/max values.
        'Changes current VOLT if necessary.
        'Call after changing:
        '   VOLT, VOLT_UNIT, VOLT_OFFS, FUNC, OUTP_LOAD or _IMP
        'See HP E1445A User's Manual pages B-9, 8-101 and 8-104

        Dim TmpVolt As Double, VoltMax As Double, VoltMin As Double, Factor As Double, Impedance As Double
        Dim TmpOffs As Double, OffsMax As Double, OffsMin As Double, Div7Factor As Short

        Impedance = Val(SetCur(OUTP_LOAD)) '50 or 75

        'Global VOLT_At_V_50 tracks SetCur$(VOLT) as if the ARB were
        '   set to a matched impedance with a UOM of V or Vpk.

        TmpVolt = VOLT_At_V_50
        TmpOffs = OFFS_At_V_50

        Select Case ParameterChanged
            Case VOLT, VOLT_OFFS
                'Convert new SetCur$ values to V_At_50 values
                Select Case UCase(SetCur(VOLT_UNIT))
                    Case "V", "VPK"
                        'From
                        TmpVolt = Val(SetCur(VOLT))
                    Case "VPP"
                        'From
                        TmpVolt = Val(SetCur(VOLT)) / 2
                    Case "VRMS", "W"
                        'From
                        Select Case UCase(SetCur(FUNC))
                            Case "SIN"
                                Factor = Math.Sqrt(2)
                            Case "SQU"
                                Factor = 1
                            Case "TRI", "RAMP"
                                Factor = 1.732050802
                        End Select
                        If UCase(SetCur(VOLT_UNIT)) = "VRMS" Then
                            TmpVolt = Val(SetCur(VOLT)) * Factor
                        Else
                            TmpVolt = (Math.Sqrt(Val(SetCur(VOLT)) * Impedance) * Factor) 'To Vpk
                        End If
                End Select
                TmpOffs = Val(SetCur(VOLT_OFFS))
                'Adjust for unmatched load impedance
                If SetCur(OUTP_LOAD) = "INF" Then
                    TmpVolt /= 2
                    TmpOffs /= 2
                End If
        End Select

        'At this point, TmpVolt and TmpOffs are correct for the ARB being set
        '   to UOM "V" with a matched impedance.
        'Now set Min's and Max's

        If SetCur(FUNC) = "DC" Then
            SetMinInc(VOLT) = "0.00125"
            SetMinInc(VOLT_DIV_7) = "0.000179"
            VoltMin = -5.12
            VoltMax = 5.11875
            OffsMin = -5
            OffsMax = 5
        Else
            SetMinInc(VOLT) = "0" 'Actual min_inc is < min value, so 0 is OK
            SetMinInc(VOLT_DIV_7) = "0"
            'Account for current OFFS
            If TmpOffs <= 1 Then
                VoltMin = 0.161869088
            Else
                VoltMin = 1.02468
                'SetDef$(VOLT) = "0"  Removed V1.4
            End If
            VoltMax = 6.025 - TmpOffs
            If VoltMax > 5.11875 Then VoltMax = 5.11875

            'Account for current VOLT
            If TmpVolt > 1.02426 Then
                OffsMin = -6.0225 + TmpVolt
                If OffsMin < -5 Then OffsMin = -5
                OffsMax = 6.0225 - TmpVolt
                If OffsMax > 5 Then OffsMax = 5
            Else
                OffsMin = -1.2025 + TmpVolt
                If OffsMin < -0.99993 Then OffsMin = -0.99993
                OffsMax = 1.2025 - TmpVolt
                If OffsMax > 0.99993 Then OffsMax = 0.99993
            End If
        End If

        If (ParameterChanged = OUTP_LOAD Or ParameterChanged = OUTP_IMP) And SetCur(OUTP_LOAD) = "INF" Then
            VoltMax *= 2
            VoltMin *= 2
            OffsMax *= 2
            OffsMin *= 2
            TmpVolt *= 2 'JHill V1.3
        End If

        If TmpVolt > VoltMax Then
            TmpVolt = VoltMax
        ElseIf TmpVolt < VoltMin Then
            TmpVolt = VoltMin
        End If

        If TmpOffs > OffsMax Then
            TmpOffs = OffsMax
        ElseIf TmpOffs < OffsMin Then
            TmpOffs = OffsMin
        End If

        If (ParameterChanged = OUTP_LOAD Or ParameterChanged = OUTP_IMP) And SetCur(OUTP_LOAD) = "INF" Then
            VoltMax /= 2
            VoltMin /= 2
            OffsMax /= 2
            OffsMin /= 2
            TmpVolt /= 2 'JHill V1.3
        End If

        VOLT_At_V_50 = TmpVolt
        OFFS_At_V_50 = TmpOffs

        'At this point, VOLT_At_V_50, VoltMin, VoltMax, OFFS_At_V_50, OffsMin and OffsMax
        '   are now correct for the ARB being set to UOM "V" with a matched impedance.

        'Now, adjust min, max and current values for VOLT and VOLT_OFFS according
        '   to the current UOM.
        Div7Factor = 7
        Select Case UCase(SetCur(VOLT_UNIT))
            Case "V", "VPK"
                Factor = 1
            Case "VPP"
                Factor = 2
            Case "VRMS", "W"
                Select Case UCase(SetCur(FUNC))
                    Case "SIN"
                        Factor = 1 / Math.Sqrt(2)
                    Case "SQU"
                        Factor = 1
                    Case "TRI", "RAMP"
                        Factor = 0.577350271
                End Select
                If UCase(SetCur(VOLT_UNIT)) = "W" Then
                    TmpVolt = ((TmpVolt * Factor) ^ 2) / Impedance
                    VoltMin = ((VoltMin * Factor) ^ 2) / Impedance
                    VoltMax = ((VoltMax * Factor) ^ 2) / Impedance
                    Factor = 1
                    Div7Factor = 49
                End If
        End Select

        If SetCur(OUTP_LOAD) = "INF" Then
            Factor *= 2
            SetCur(VOLT_OFFS) = Round(TmpOffs * 2, 9)
            SetMin(VOLT_OFFS) = Round(OffsMin * 2, 9)
            SetMax(VOLT_OFFS) = Round(OffsMax * 2, 9)
        Else
            SetCur(VOLT_OFFS) = Round(TmpOffs, 9)
            SetMin(VOLT_OFFS) = Round(OffsMin, 9)
            SetMax(VOLT_OFFS) = Round(OffsMax, 9)
        End If

        SetCur(VOLT) = Round(TmpVolt * Factor, 9)
        SetCur(VOLT_DIV_7) = Round(TmpVolt * Factor / Div7Factor, 9)

        SetMin(VOLT) = Round(VoltMin * Factor, 9)
        SetMin(VOLT_DIV_7) = Round(VoltMin * Factor / Div7Factor, 6)
        SetMax(VOLT) = Round(VoltMax * Factor, 9)
        SetMax(VOLT_DIV_7) = Round(VoltMax * Factor / Div7Factor, 9)

        'Added V1.4
        SetDef(VOLT) = SetMin(VOLT)
        SetDef(VOLT_DIV_7) = SetMin(VOLT_DIV_7)

        SetMinInc(VOLT) = Round(Val(SetMinInc(VOLT)) * Factor, 9)
        SetMinInc(VOLT_DIV_7) = Round(Val(SetMinInc(VOLT_DIV_7)) * Factor, 9)

        frmHpE1445a.txtAmpl.Text = EngNotate(SetCur(VOLT), VOLT)
        frmHpE1445a.txtDcOffs.Text = EngNotate(SetCur(VOLT_OFFS), VOLT_OFFS)

        ' Update the "Output / 7" display
        If Impedance = 50 Then
            frmHpE1445a.txtAmplDiv7.Text = EngNotate(SetCur(VOLT_DIV_7), VOLT_DIV_7)
            frmHpE1445a.panAmplDiv7.Enabled = True
        Else
            frmHpE1445a.txtAmplDiv7.Text = "Not Valid"
            frmHpE1445a.panAmplDiv7.Enabled = False
        End If
    End Sub

    Sub DisplayErrorMessage(ByVal ErrorStatus As Integer)
        If ErrorStatus Then
            '     Error = hpe1445a_errorMessage(InstrumentHandle&, ErrorStatus&, ReadBuffer$)
            MsgBox(ReadBuffer, MsgBoxStyle.Exclamation, "VISA Error Message")
            If InStr(sTipMode, "TIP_Run") Then
                SetKey("TIPS", "STATUS", "Error from VISA: " & Hex(ErrorStatus) & " " & ReadBuffer)
                frmHpE1445a.cmdQuit_Click()
            End If
        End If
    End Sub

    Sub RunTipCmds()
        'DESCRIPTION:
        '   This procedure takes a previously-stored ARB command string, sets up the
        '   ARB GUI to match those commands and programs the ARB with this command string.
        'PARAMETERS:    None
        'RETURNS:       None
        'GLOBAL VARIABLES USED:
        '   sTipCmds
        '   bRunningTipCmds
        'EXAMPLE:
        '   RunTipCmds

        Dim sCmds() As String = {""}
        Dim i As Short
        Dim iStrPos As Short
        Dim sTmpCmds As String

        HelpPanel("Please wait, Setting to new configuration")
        frmHpE1445a.Cursor = Cursors.WaitCursor
        bActivatingControls = True

        'Reset the ARB and panel
        WriteInstrument("*CLS;*RST")
        DoNotTalk = True
        SetControlsToReset()
        DoNotTalk = False
        bRunningTipCmds = True

        'First, process PRE_SEGMENT_LOAD_COMMANDS
        iStrPos = InStr(sTipCmds, ":LIST:SSEQ:SEL")
        If iStrPos <> 0 Then
            sTmpCmds = Strings.Left(sTipCmds, iStrPos - 2)
        Else
            sTmpCmds = sTipCmds
        End If
        WriteInstrument(sTmpCmds)
        DoNotTalk = True
        For i = 1 To StringToList(sTmpCmds, 1, sCmds, ";")
            If bActivateControl(sCmds(i)) = False Then Exit For
        Next i
        DoNotTalk = False

        'Now process POST_SEGMENT_LOAD_COMMANDS, if any
        If iStrPos <> 0 Then
            sTmpCmds = Mid(sTipCmds, iStrPos)
            DoNotTalk = True
            For i = 1 To StringToList(sTmpCmds, 1, sCmds, ";")
                If Not bActivateControl(sCmds(i)) Then Exit For
            Next i
            DoNotTalk = False
            WriteInstrument(sTmpCmds)
        End If

        frmHpE1445a.Cursor = Cursors.Default
        HelpPanel("")
        bActivatingControls = False
        bRunningTipCmds = False
    End Sub

    Sub SendSequence()
        Dim i As Integer, n As Short

        With frmHpE1445a
            'If Not .ribInit = True Then
            '  Exit Sub
            'End If
            ' Change 19JUN08 EWL - above command sequence was exiting sub unless ARB was
            '                      init'd
            If .ribInit.Pushed = True Then
                SendCommand(ABOR)
            End If

            ' Exit Sub
            'MH WriteInstrument SetCod$(FUNC_USER) & "NONE"
            WriteInstrument(SetCod(FUNC_USER) & "NONE")
            SendCommand(LIST_SSEQ_DEL_ALL)
            ' WriteInstrument "SOUR:FREQ1:FIX " & SetCur$(FREQ)
            ' WriteInstrument "SOUR:FUNC:SHAP USER"
            ' WriteInstrument "SOUR:VOLT:LEV:IMM:AMPL 1"

            If .lstListSseqSeq.Items.Count <> 0 Then
                n = InStr(.lstListSseqSeq.Items.Item(0).ToString(), ",")
                ' adds the first name in the sequence to the list
                SetCur(LIST_SSEQ_SEQ) = Strings.Left(.lstListSseqSeq.Items.Item(0).ToString(), n - 1)
                ' names the sequence after the first element with _OUT
                SetCur(FUNC_USER) = SetCur(LIST_SSEQ_SEQ) + "_OUT"
                SetCur(LIST_SSEQ_SEL) = SetCur(FUNC_USER)
                'Added "Format()" to next line for V1.4
                SetCur(LIST_SSEQ_DWEL_COUN) = Mid(.lstListSseqSeq.Items.Item(0).ToString(), n + 2)
                ' build list of segments and dwell list
                For i = 1 To .lstListSseqSeq.Items.Count - 1
                    n = InStr(.lstListSseqSeq.Items.Item(i).ToString(), ",")
                    SetCur(LIST_SSEQ_SEQ) &= "," & Strings.Left(.lstListSseqSeq.Items.Item(i).ToString(), n - 1)
                    'Added "Format()" to next line for V1.4
                    SetCur(LIST_SSEQ_DWEL_COUN) &= "," & Mid(.lstListSseqSeq.Items.Item(i).ToString(), n + 2)
                Next i
                SetCur(LIST_SSEQ_DEF) = .lstListSseqSeq.Items.Count
                SetCur(FUNC_USER) = SetCur(LIST_SSEQ_SEL)
                .tabOptions.Focus()
                WriteInstrument(SetCod(LIST_SSEQ_SEL) & SetCur(LIST_SSEQ_SEL) & ";" & SetCod(LIST_SSEQ_DEF) & SetCur(LIST_SSEQ_DEF) & ";" & SetCod(LIST_SSEQ_SEQ) & SetCur(LIST_SSEQ_SEQ) & ";" & SetCod(LIST_SSEQ_DWEL_COUN) & SetCur(LIST_SSEQ_DWEL_COUN) & ";" & SetCod(FUNC_USER) & SetCur(FUNC_USER)) '& ";"
                '        & SetCod$(FUNC) & SetCur$(FUNC)
                'WriteInstrument "SOUR:LIST1:SSEQ:FREE?"
                '.lblListSseqFree = Val(ReadARB)
                'WriteInstrument SetCod$(FUNC_USER) & SetCur$(FUNC_USER)

                If .ribInit.Pushed = True And .chkOutpStat.Checked Then
                    SendCommand(INIT)
                End If
            Else
                frmHpE1445a.ribAbor_Click()
                .ribInit.Pushed = False
                .ribAbor.Pushed = True
            End If
            SendCommand(LIST_SSEQ_FREE)
            .lblListSseqFree.Text = Val(ReadARB())
        End With
    End Sub

    Public Sub ProcessMarker()
        With frmHpE1445a
            If .cboMarkFeed.Text = "Source:List1" Or .cboMarkEclt0Feed.Text = "Source:List1" Or .cboMarkEclt1Feed.Text = "Source:List1" Then
                .fraMarkSpo.Visible = True
                .CursorMarker.Visible = True
                .txtMarkSpo.Text = SetCur(LIST_MARK_SPO)
                .CursorMarker.SnapMode = CursorSnapMode.Floating
                .CursorMarker.XPosition = Val(.txtMarkSpo.Text)
                .CursorMarker.YPosition = Wave(Val(.txtMarkSpo.Text))
                .CursorMarker.SnapMode = CursorSnapMode.ToPlot
                .cmdSnap.BackColor = Color.Silver
            Else
                .fraMarkSpo.Visible = False
                .CursorMarker.Visible = False
            End If
        End With
    End Sub

    Sub SizeYAxis()
        Dim Range As Double, VOffs As Double

        '    Range# = Val(SetCur$(VOLT))
        '    With frmHpE1445a
        '    .G1.YAxes(0).Minimum = -Range#
        '    .G1.YAxes(0).Maximum = Range#
        '    YSpan = .G1.YAxes(0).Maximum - .G1.YAxes(0).Minimum
        '    .G1.YAxes(0).DiscreteInterval = YSpan / 8191

        Range = Val(SetCur(VOLT))
        VOffs = Val(SetCur(VOLT_OFFS))
        Try
            With frmHpE1445a
                .YAxis1.Range = New Range(-(Range - VOffs), Range + VOffs)
                YSpan = .YAxis1.Range.Maximum - .YAxis1.Range.Minimum
            End With
        Catch ex As Exception
            'Get out of DC Issue
        End Try

    End Sub

    Sub WriteInstrument(ByVal Command As String)
        Dim count As Integer
        Dim ErrorCode As Integer
        Dim status As Integer

        If DoNotTalk Or Command = "" Then Exit Sub
        If Not LiveMode Then
            HelpPanel("Instrument Command: " & Quote & Command & Quote)
            Exit Sub
        End If
        'check to see if only queries are allowed
        If bInstrumentMode And InStr(Command, "?") = 0 Then Exit Sub

        'Check mode in
        If ((bDebugMode = True) And InStr(Command, "?") = False) Then
            Exit Sub
        End If

        '  ErrorStatus& = viWrite(InstrumentHandle&, Command$, CLng(Len(Command$)), count)

        status = atxml_WriteCmds(resourceName, Command, count)

        If status Then
            DisplayErrorMessage(status)
        Else
            If InStr(Command, "?") = 0 Then ' If NOT expecting a response
                Do
                    '        ErrorStatus& = hpe1445a_errorQuery(InstrumentHandle&, ErrorCode, ReadBuffer)
                    If ErrorCode = 0 Then Exit Do
                    UserError = True
                    MsgBox("Error Code: " & ErrorCode & vbCrLf & ReadBuffer, MsgBoxStyle.Exclamation, "Arbitrary Waveform Generator Error Message")
                    'If either TIP Run Mode
                    If InStr(sTipMode, "TIP_Run") Then
                        SetKey("TIPS", "STATUS", "Error from ARB: " & ErrorCode & " " & ReadBuffer)
                        frmHpE1445a.cmdQuit_Click()
                    End If
                Loop
            End If
        End If
    End Sub

    Function ReadARB() As String
        ReadARB = ""
        Dim CharsRead As Integer
        Dim ReadString As String = ""
        Dim status As Integer

        If Not LiveMode Then Exit Function
        If DoNotTalk Then Exit Function

        Do
            ' ErrorStatus& = viRead(InstrumentHandle&, ReadBuffer, 255&, CharsRead&)
            ReadBuffer = Space(4000)
            status = atxml_ReadCmds(resourceName, ReadBuffer, 255, CharsRead)

            ReadString &= Strings.Left(ReadBuffer, CharsRead - 1)
        Loop While CharsRead = 255

        DisplayErrorMessage(ErrorStatus)
        ReadARB = ReadString
    End Function

    Sub Main()
        Dim SystemDirectory As String = Environment.SystemDirectory
        Dim status As Integer
        Dim XmlBuf As String
        Dim Allocation As String
        Dim Response As String

        'Find Windows Directory
        sWindowsDir = Environment.GetFolderPath(Environment.SpecialFolder.Windows)

        'Check the command line argument to see if called by TIP Studio
        If Strings.Left(Microsoft.VisualBasic.Command(), 4) = "TIP_" Then
            sTipMode = Microsoft.VisualBasic.Command()
            sTipCmds = sGetKey("TIPS", "CMD")
        Else
            sTipMode = ""
            sTipCmds = ""
            '! Load frmAbout
            frmAbout.cmdOK.Visible = False
            frmAbout.Show()
            Application.DoEvents()
        End If

        Quote = Convert.ToString(Chr(34))
        DoNotTalk = True
        UserEnteringData = False

        resourceName = "ARB_GEN_1"

        'Check System and Instrument For Errors

        '        If FileExists(SystemDirectory$ & "VISA32.DLL") Then
        'Determine if the instrument is functioning
        '  ErrorStatus& = hpe1445a_init(ResourceName:=RESOURCE_NAME$, IDQuery:=1, resetDevice:=1, InstrumentHandle:=InstrumentHandle&)

        status = atxml_Initialize(proctype, guid)

        'WMT -- ADDED THIS APR 2006

        Response = Space(4096)

        '        Stop
        '                          1         2         3         4         5         6
        '                 123456789012345678901234567890123456789012345678901234567890
        XmlBuf = "<AtXmlTestRequirements>" & _
                 "    <ResourceRequirement>" & _
                 "        <ResourceType>Source</ResourceType>" & _
                 "        <SignalResourceName>ARB_GEN_1</SignalResourceName> " & _
                 "    </ResourceRequirement> " & _
                 "</AtXmlTestRequirements>"            '

        Allocation = Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\ATS\", "PAWSAllocationPath", Nothing)

        status = atxml_ValidateRequirements(XmlBuf, Allocation, Response, 4096)

        'Parse Availability XML string to get the status(Mode) of the instrument
        iStatus = gctlParse.ParseAvailiability(Response)

        '       Hard-Coded break point (remove before shipping)
        'Stop

        If status Then
            DisplayErrorMessage(ErrorStatus)
            MsgBox("The ARB is not responding.  Live mode is disabled.", MsgBoxStyle.Exclamation)
            LiveMode = False
        Else
            LiveMode = True
        End If

        bInstrumentMode = True
        Initialize()
        frmAbout.Hide()

        If sTipMode = "TIP_Design" Then
            frmHpE1445a.cmdUpdateTip.Visible = True
        Else
            frmHpE1445a.cmdUpdateTip.Visible = False
        End If

        frmHpE1445a.Refresh()
        frmHpE1445a.tolFuncWave.Buttons(0).Pushed = True 'Depress Sine button
        DoNotTalk = False
        WriteInstrument("*CLS") 'mh should not send a reset;*RST"

        Select Case sTipMode
            Case "TIP_Design"
                RunTipCmds()
            Case "TIP_RunSetup"
                RunTipCmds()
                frmHpE1445a.WindowState = FormWindowState.Minimized
                SetKey("TIPS", "STATUS", "Ready")
            Case "TIP_RunPersist"
                RunTipCmds()
                SetKey("TIPS", "STATUS", "Ready")
        End Select

        'Set the Refresh rate when in "Debug" mode from 1000 = 1 sec to 25000 = 25 sec
        'frmHpE1445a.Panel_Conifg.Refresh = 5000

        'Get current information from instrument
        frmHpE1445a.ConfigGetCurrent()
        bInstrumentMode = False

        If iStatus <> 1 Then
            bDebugMode = False
        Else
            bDebugMode = True
        End If
        frmHpE1445a.pctMode.Value = bDebugMode
        frmHpE1445a.timDebug.Enabled = bDebugMode
        frmHpE1445a.cmdLocal.Enabled = bDebugMode
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

        'Clear String Buffer
        lpString = Space(Len(lpString))
        Dim lpFileName As String 'INI File Key Name "Key=?"

        'Get the ini file location from the Registry
        lpFileName = Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\ATS\", "IniFilePath", Nothing)

        nNumChars = GetPrivateProfileString(sSection, sKey, "", lpString.ToString(), Len(lpString), lpFileName)
        sGetKey = Strings.Left(lpString.ToString(), nNumChars)

        Application.Run(frmHpE1445a)
    End Function

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

        Dim lpReturnedString As String 'Return Buffer
        Dim nSize As Integer 'Return Buffer Size
        Dim lpFileName As String 'INI File Key Name "Key=?"
        Dim ReturnValue As Integer 'Return Value Buffer
        Dim FileNameInfo As String 'Formatted Return String
        Dim lpString As String 'String to write to INI File

        'Clear String Buffer
        lpReturnedString = Space(260)
        'Get the ini file location from the Registry
        lpFileName = Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\ATS\", "IniFilePath", Nothing)
        nSize = 255
        lpReturnedString = Space(260)
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

    Sub SetKey(ByRef sSection As String, ByVal sKey As String, ByVal sKeyVal As String)
        'DESCRIPTION:
        '   This function sets a key value in VIPERT.INI
        'PARAMETERS:
        '   sSection    The VIPERT.INI file section where the key is located.
        '   sKey        The Key name.
        '   sKeyVal     The string to set the key value to.
        'RETURNS:   None
        'GLOBAL VARIABLES USED:
        '   sWindowsDir
        'EXAMPLE:
        '   SetKey "TIPS", "STATUS", "Ready"
        Dim lpFileName As String 'INI File Key Name "Key=?"

        'Get the ini file location from the Registry
        lpFileName = Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\ATS\", "IniFilePath", Nothing)

        WritePrivateProfileString(sSection, sKey, sKeyVal, lpFileName)
    End Sub

    Sub SendCommand(ByVal SetIdx As Short)
        'Sends the current setting for a parameter to "WriteInstrument".
        Dim Value As Double, i As Short, First As Short, Last As Short, ThisCmd As String
        Dim Line As String = ""
        Dim bGotAborted As Boolean

        If (Initialized = True) And (Not ExecInit(SetIdx)) Then
            bGotAborted = True
        End If

        'Start command string with THIS command and exception processing.
        ThisCmd = SetCod(SetIdx) & SetCur(SetIdx)
        Select Case SetIdx
            Case INIT
                If Initialized <> True Then
                    Initialized = True
                    WriteInstrument(SetCod(SetIdx))
                End If
                Exit Sub
            Case ABOR
                Initialized = False
                WriteInstrument(SetCod(SetIdx))
                Exit Sub
            Case FREQ, RAMP_POIN, TRIG_SOUR, ROSC_SOUR, ROSC_FREQ_EXT, FREQ_STOP
                AdjustFreqLimits()
            Case MARK_FEED, MARK_ECLT0_FEED, MARK_ECLT1_FEED
                'These parameters require arguments enclosed in Quotes
                ThisCmd = SetCod(SetIdx) & Quote & SetCur(SetIdx) & Quote
            Case VOLT
                AdjustVoltLimits(SetIdx)
            Case VOLT_DIV_7
                'Do nothing
                ThisCmd = ""
            Case OUTP_LOAD
                AdjustVoltLimits(SetIdx)
                '      Line$ = Line$ & SetCod(OUTP_IMP) & SetCur$(OUTP_IMP) & ";"
                '        & SetCod(VOLT) & SetCur$(VOLT) & ";"
            Case OUTP_IMP
                AdjustVoltLimits(SetIdx)
                '      Line$ = Line$ & SetCod(OUTP_LOAD) & SetCur$(OUTP_LOAD) & ";"
                '        & SetCod(VOLT) & SetCur$(VOLT) & ";"
            Case SWE_POIN
                AdjustSweepTime()
            Case SWE_SPAC
                AdjustAgileLimits()
            Case FREQ_FSK_H, FREQ_FSK_L
                ThisCmd = SetCod(SetIdx) & SetCur(FREQ_FSK_H) & "," & SetCur(FREQ_FSK_L)
                AdjustAgileLimits()
        End Select

        'Find Coupling Group for this command
        Select Case SetIdx
            Case Is <= LAST_COUP_GROUP_1_COMMAND
                'Coupling Group 1: Frequency
                First = 1
                Last = LAST_COUP_GROUP_1_COMMAND
            Case Is <= LAST_COUP_GROUP_2_COMMAND
                'Coupling Group 2: Frequency & Voltage
                First = 1
                Last = LAST_COUP_GROUP_3_COMMAND
            Case Is <= LAST_COUP_GROUP_3_COMMAND
                'Coupling Group 3: Voltage
                First = LAST_COUP_GROUP_2_COMMAND + 1
                Last = LAST_COUP_GROUP_3_COMMAND

            Case Else
                'Non-coupled commands
                First = 1 'Don't allow the 'add-on' loop to execute
                Last = 0
                Line = ThisCmd
        End Select

        'Add all commands in same Coupling group
        For i = First To Last
            '    If (i% <> SetIdx%) And (SetCur$(i) <> SetDef$(i)) Then
            If (i <> SetIdx) Then
                If i = FREQ_FSK_L Or i = FREQ_FSK_H Then
                    Line &= SetCod(i) & SetCur(FREQ_FSK_L) & "," & SetCur(FREQ_FSK_H) & ";"
                Else
                    If SetCod(i) <> "" Then
                        Line &= SetCod(i) & SetCur(i) & ";"
                    End If
                End If
                If (Initialized = True) And (Not ExecInit(i)) Then
                    bGotAborted = True
                End If
            Else
                Line &= ThisCmd & ";"
            End If
        Next i

        If (Initialized = True) And (bGotAborted = True) Then
            WriteInstrument(SetCod(ABOR))
        End If

        WriteInstrument(Line)

        'Special case post-processing
        Select Case SetIdx
            Case FREQ2, ROSC_SOUR, ROSC_FREQ_EXT
                'Reset display to actual freq (divide by n generator)
                If CurFreqIdx = FREQ2 Then
                    WriteInstrument("FREQ2?")
                    Value = Val(ReadARB())
                    If Value <> 0 Then
                        frmHpE1445a.txtFreq.Text = EngNotate(Value, FREQ2)
                        frmHpE1445a.txtSampleFreq.Text = frmHpE1445a.txtFreq.Text
                        SetCur(FREQ2) = Value
                    End If
                End If
            Case MARK_FEED
                If SetCur(MARK_FEED) = "Source:List1" Then
                    WriteInstrument(SetCod(LIST_MARK_SPO) & SetCur(LIST_MARK_SPO))
                End If
            Case FUNC
                Select Case SetCur(FUNC)
                    Case "USER"
                        SendSequence()
                        Exit Sub 'To prevent 'Init Ignored' error
                        '        Case "DC"
                        '          bGotAborted = False 'prevents an 'Init Ignored' error
                End Select
        End Select

        If Initialized And bGotAborted And SetCur(FUNC) <> "DC" Then
            WriteInstrument(SetCod(INIT))
        End If
    End Sub

    Sub Initialize()
        With frmHpE1445a
            '*** "Main" Tab
            'ROSC:SOUR
            .cboRoscSour.Items.AddRange({"Clk10", "External", "ECLTrg0", "ECLTrg1", "Internal1", "Internal2"})
            'VOLT:UNIT
            .cboUnits.Items.AddRange({"V", "Vpk", "Vpp", "Vrms", "W"})
            'RAMP:POL
            .cboWavePol.Items.AddRange({"Normal", "Inverted"})

            '*** "Arm-Trig" Tab
            'ARM:LAY2:SOUR
            .cboArmLay2Sour.Items.AddRange({"Bus", "ECLTrg0", "ECLTrg1", "External", "Hold", "Immediate", "TTLTrg0", "TTLTrg1", "TTLTrg2", "TTLTrg3", "TTLTrg4", "TTLTrg5", "TTLTrg6", "TTLTrg7"})
            'ARM:LAY2:SLOP
            .cboArmLay2Slop.Items.AddRange({"Negative", "Positive"})
            'TRIG:SOUR
            .cboTrigSour.Items.AddRange({"Bus", "ECLTrg0", "ECLTrg1", "External", "Hold", "Internal1", "Internal2", "TTLTrg0", "TTLTrg1", "TTLTrg2", "TTLTrg3", "TTLTrg4", "TTLTrg5", "TTLTrg6", "TTLTrg7"})
            'TRIG:STOP:SOUR
            .cboTrigStopSour.Items.AddRange({"Bus", "External", "Hold", "TTLTrg0", "TTLTrg1", "TTLTrg2", "TTLTrg3", "TTLTrg4", "TTLTrg5", "TTLTrg6", "TTLTrg7"})
            'TRIG:SLOP
            .cboTrigSlop.Items.AddRange({"Negative", "Positive"})
            'TRIG:STOP:SLOP
            .cboTrigStopSlop.Items.AddRange({"Negative", "Positive"})
            'TRIG:GATE:SOUR
            .cboTrigGateSour.Items.AddRange({"External", "TTLTrg0", "TTLTrg1", "TTLTrg2", "TTLTrg3", "TTLTrg4", "TTLTrg5", "TTLTrg6", "TTLTrg7"})
            'TRIG:GATE:POL
            .cboTrigGatePol.Items.AddRange({"Inverted", "Normal"})

            '*** "Markers" Tab
            'MARK:FEED
            .cboMarkFeed.Items.AddRange({"Arm:Start:Layer1", "Arm:Start:Layer2", "Freq:Change", "Source:List1", "PM:Dev:Change", "ROscillator", "Trigger:Start"})
            'MARK:POL
            .cboMarkPol.Items.AddRange({"Inverted", "Normal"})
            'MARK:ECLT0:FEED
            .cboMarkEclt0Feed.Items.AddRange({"Arm:Start:Layer1", "Arm:Start:Layer2", "Freq:Change", "Source:List1", "PM:Dev:Change", "ROscillator", "Trigger:Start"})
            'MARK:ECLT1:FEED
            .cboMarkEclt1Feed.Items.AddRange({"Arm:Start:Layer1", "Arm:Start:Layer2", "Freq:Change", "Source:List1", "PM:Dev:Change", "ROscillator", "Trigger:Start"})
'            .cboMarkEclt1Feed.Items.Item(6) = "Trigger:Start"

            '*** "Freq. Agility" Tab
            'FREQ:FSK:SOUR
            .cboAgileFskSour.Items.AddRange({"External", "TTLTrg0", "TTLTrg1", "TTLTrg2", "TTLTrg3", "TTLTrg4", "TTLTrg5", "TTLTrg6", "TTLTrg7"})
            'SWE:DIR
            .cboAgileSweepDir.Items.AddRange({"Down", "Up"})
            'SWE:SPAC
            .cboAgileSweepSpac.Items.AddRange({"Linear", "Logarithmic"})
            'ARM:SWE:SOUR
            .cboAgileSweepTrigSour.Items.AddRange({"Bus", "Hold", "Immediate", "Link", "TTLTrg0", "TTLTrg1", "TTLTrg2", "TTLTrg3", "TTLTrg4", "TTLTrg5", "TTLTrg6", "TTLTrg7"})
            'TRIG:SWE:SOUR
            .cboAgileSweepAdvSour.Items.AddRange({"Bus", "Hold", "Link", "Timer", "TTLTrg0", "TTLTrg1", "TTLTrg2", "TTLTrg3", "TTLTrg4", "TTLTrg5", "TTLTrg6", "TTLTrg7"})

            '*** "Sequence" Tab
            .lblListSseqFree.Text = "32768"
            .lblListFree.Text = "262144"

            '*** "Options" Tab
        End With

        SetCod(ABOR) = ":ABOR"
        ExecInit(ABOR) = True

        SetCod(ARM_COUN) = ":ARM:COUN "
        SetDef(ARM_COUN) = "INF"
        SetMin(ARM_COUN) = "1"
        SetMax(ARM_COUN) = "65536"
        SetUOM(ARM_COUN) = ""
        SetRes(ARM_COUN) = "N0"
        SetMinInc(ARM_COUN) = "1"
        ExecInit(ARM_COUN) = False

        SetCod(ARM_LAY2_COUN) = ":ARM:LAY2:COUN "
        SetDef(ARM_LAY2_COUN) = "1"
        SetMin(ARM_LAY2_COUN) = "1"
        SetMax(ARM_LAY2_COUN) = "65535"
        SetUOM(ARM_LAY2_COUN) = ""
        SetRes(ARM_LAY2_COUN) = "N0"
        ExecInit(ARM_LAY2_COUN) = False

        SetCod(ARM_LAY2) = ":ARM:LAY2"
        ExecInit(ARM_LAY2) = True

        SetCod(ARM_LAY2_SLOP) = ":ARM:LAY2:SLOP "
        SetDef(ARM_LAY2_SLOP) = "Positive"
        ExecInit(ARM_LAY2_SLOP) = False

        SetCod(ARM_LAY2_SOUR) = ":ARM:LAY2:SOUR "
        SetDef(ARM_LAY2_SOUR) = "Immediate"
        ExecInit(ARM_LAY2_SOUR) = False

        SetCod(ARM_SWE_COUN) = ":ARM:SWE:COUN "
        SetDef(ARM_SWE_COUN) = "1"
        SetMin(ARM_SWE_COUN) = "1"
        SetMax(ARM_SWE_COUN) = "2147483647"
        SetUOM(ARM_SWE_COUN) = ""
        SetRes(ARM_SWE_COUN) = "N0"
        ExecInit(ARM_SWE_COUN) = False

        SetCod(ARM_SWE) = ":ARM:SWE"
        ExecInit(ARM_SWE) = True

        SetCod(ARM_SWE_LINK) = ":ARM:SWE:LINK "
        SetDef(ARM_SWE_LINK) = "ARM:LAY2"
        ExecInit(ARM_SWE_LINK) = True

        SetCod(ARM_SWE_SOUR) = ":ARM:SWE:SOUR "
        SetDef(ARM_SWE_SOUR) = "Immediate"
        ExecInit(ARM_SWE_SOUR) = False

        SetCod(CAL_COUN) = ":CAL:COUN?"
        ExecInit(CAL_COUN) = True

        SetCod(CAL_DATA_AC) = ":CAL:DATA:AC "
        ExecInit(CAL_DATA_AC) = True

        SetCod(CAL_DATA_AC2) = ":CAL:DATA:AC2 "
        ExecInit(CAL_DATA_AC2) = True

        SetCod(CAL_DATA) = ":CAL:DATA "
        ExecInit(CAL_DATA) = True

        SetCod(CAL_BEG) = ":CAL:BEG"
        ExecInit(CAL_BEG) = False

        SetCod(CAL_POIN) = ":CAL:POIN? "
        ExecInit(CAL_POIN) = False

        SetCod(CAL_SEC_CODE) = ":CAL:SEC:CODE "
        ExecInit(CAL_SEC_CODE) = True

        SetCod(CAL_SEC) = ":CAL:SEC "
        SetDef(CAL_SEC) = "ON"
        ExecInit(CAL_SEC) = True

        SetCod(CAL_STAT) = ":CAL:STAT "
        SetDef(CAL_STAT) = "ON"
        ExecInit(CAL_STAT) = True

        SetCod(CAL_STAT_AC) = ":CAL:STAT:AC "
        SetDef(CAL_STAT_AC) = "ON"
        ExecInit(CAL_STAT_AC) = True

        SetCod(CAL_STAT_DC) = ":CAL:STAT:DC "
        SetDef(CAL_STAT_DC) = "ON"
        ExecInit(CAL_STAT_DC) = True

        SetCod(INIT) = ":INIT"
        ExecInit(INIT) = False

        SetCod(OUTP_FILT_FREQ) = ":OUTP:FILT:FREQ "
        SetDef(OUTP_FILT_FREQ) = "250 kHz"
        SetUOM(OUTP_FILT_FREQ) = "Hz"
        SetRes(OUTP_FILT_FREQ) = "A0"
        ExecInit(OUTP_FILT_FREQ) = True

        SetCod(OUTP_FILT) = ":OUTP:FILT "
        SetDef(OUTP_FILT) = "OFF"
        ExecInit(OUTP_FILT) = True

        SetCod(OUTP_IMP) = ":OUTP:IMP "
        SetDef(OUTP_IMP) = "50"
        SetUOM(OUTP_IMP) = "Ohm"
        SetRes(OUTP_IMP) = "N0"
        ExecInit(OUTP_IMP) = True

        SetCod(OUTP_LOAD) = ":OUTP:LOAD "
        SetDef(OUTP_LOAD) = "50"
        SetUOM(OUTP_LOAD) = "Ohm"
        SetRes(OUTP_LOAD) = "N0"
        ExecInit(OUTP_LOAD) = True

        SetCod(OUTP_LOAD_AUTO) = ":OUTP:LOAD:AUTO "
        SetDef(OUTP_LOAD_AUTO) = "ON"
        ExecInit(OUTP_LOAD_AUTO) = True

        SetCod(OUTP) = ":OUTP "
        SetDef(OUTP) = "ON"
        ExecInit(OUTP) = True

        SetCod(ARB_DAC_FORM) = ":ARB:DAC:FORM "
        SetDef(ARB_DAC_FORM) = "SIGN"
        ExecInit(ARB_DAC_FORM) = False

        SetCod(ARB_DAC_SOUR) = ":ARB:DAC:SOUR "
        SetDef(ARB_DAC_SOUR) = "INT"
        ExecInit(ARB_DAC_SOUR) = False

        SetCod(ARB_DOWN) = ":ARB:DOWN "
        ExecInit(ARB_DOWN) = False

        SetCod(ARB_DOWN_COMP) = ":ARB:DOWN:COMP"
        ExecInit(ARB_DOWN_COMP) = False

        SetCod(FREQ_CENT) = ":FREQ:CENT "
        SetDef(FREQ_CENT) = "5368709.12"
        SetMin(FREQ_CENT) = "5368709.12"
        SetMax(FREQ_CENT) = "5368709.12"
        SetUOM(FREQ_CENT) = "Hz"
        SetRes(FREQ_CENT) = "A2"
        SetMinInc(FREQ_CENT) = "0.01"
        ExecInit(FREQ_CENT) = False

        SetCod(FREQ) = ":FREQ "
        SetDef(FREQ) = "10000"
        SetMin(FREQ) = "0"
        SetMax(FREQ) = "10737418.24"
        SetUOM(FREQ) = "Hz"
        SetRes(FREQ) = "A6"
        SetMinInc(FREQ) = "0.01"
        ExecInit(FREQ) = False 'Different because FREQ_RANG gets changed with it

        SetCod(FREQ_FSK_L) = ":FREQ:FSK "
        SetDef(FREQ_FSK_L) = "10000"
        SetMin(FREQ_FSK_L) = "0"
        SetMax(FREQ_FSK_L) = "10737418.24"
        SetUOM(FREQ_FSK_L) = "Hz"
        SetRes(FREQ_FSK_L) = "D5"
        SetMinInc(FREQ_FSK_L) = "1"
        ExecInit(FREQ_FSK_L) = True

        SetCod(FREQ_FSK_H) = ":FREQ:FSK "
        SetDef(FREQ_FSK_H) = "10000000"
        SetMin(FREQ_FSK_H) = "0"
        SetMax(FREQ_FSK_H) = "10737418.24"
        SetUOM(FREQ_FSK_H) = "Hz"
        SetRes(FREQ_FSK_H) = "D5"
        SetMinInc(FREQ_FSK_H) = "1"
        ExecInit(FREQ_FSK_H) = True

        SetCod(FREQ_FSK_SOUR) = ":FREQ:FSK:SOUR "
        SetDef(FREQ_FSK_SOUR) = "External"
        ExecInit(FREQ_FSK_SOUR) = False

        SetCod(FREQ_MODE) = ":FREQ:MODE "
        SetDef(FREQ_MODE) = "FIX"
        ExecInit(FREQ_MODE) = False

        SetCod(FREQ_RANG) = ":FREQ:RANG "
        SetDef(FREQ_RANG) = "0"
        SetMin(FREQ_RANG) = "MIN"
        SetMax(FREQ_RANG) = "MAX"
        SetUOM(FREQ_RANG) = "Hz"
        SetRes(FREQ_RANG) = "A2"
        ExecInit(FREQ_RANG) = False

        SetCod(FREQ_SPAN) = ":FREQ:SPAN "
        SetDef(FREQ_SPAN) = "10737418.24"
        SetMin(FREQ_SPAN) = ".02"
        SetMax(FREQ_SPAN) = "10737418.24"
        SetUOM(FREQ_SPAN) = "Hz"
        SetRes(FREQ_SPAN) = "A2"
        SetMinInc(FREQ_SPAN) = "0.01"
        ExecInit(FREQ_SPAN) = False

        SetCod(FREQ_STAR) = ":FREQ:STAR "
        SetDef(FREQ_STAR) = "0"
        SetMin(FREQ_STAR) = "0"
        SetMax(FREQ_STAR) = "10737418.24"
        SetUOM(FREQ_STAR) = "Hz"
        SetRes(FREQ_STAR) = "D5"
        SetMinInc(FREQ_STAR) = "0.001"
        ExecInit(FREQ_STAR) = False

        SetCod(FREQ_STOP) = ":FREQ:STOP "
        SetDef(FREQ_STOP) = "10737418.24"
        SetMin(FREQ_STOP) = "1"
        SetMax(FREQ_STOP) = "10737418.24"
        SetUOM(FREQ_STOP) = "Hz"
        SetRes(FREQ_STOP) = "D5"
        SetMinInc(FREQ_STOP) = "0.001"
        ExecInit(FREQ_STOP) = False

        SetCod(FREQ2) = ":FREQ2 "
        SetDef(FREQ2) = "10000"
        SetMin(FREQ2) = "81.92"
        SetMax(FREQ2) = "10737418.24"
        SetUOM(FREQ2) = "Hz"
        SetRes(FREQ2) = "A8"
        ExecInit(FREQ2) = True

        SetCod(FUNC) = ":FUNC "
        SetDef(FUNC) = "SIN"
        ExecInit(FUNC) = False

        SetCod(FUNC_USER) = ":FUNC:USER "
        SetDef(FUNC_USER) = "NONE"
        ExecInit(FUNC_USER) = False

        SetCod(LIST_FORM) = ":LIST:FORM "
        SetDef(LIST_FORM) = "ASC"
        ExecInit(LIST_FORM) = False

        SetCod(LIST_ADDR) = ":LIST:ADDR?"
        ExecInit(LIST_ADDR) = True

        SetCod(LIST_CAT) = ":LIST:CAT?"
        ExecInit(LIST_CAT) = True

        SetCod(LIST_COMB) = ":LIST:COMB "
        ExecInit(LIST_COMB) = False

        SetCod(LIST_COMB_POIN) = ":LIST:COMB:POIN?"
        ExecInit(LIST_COMB_POIN) = True

        SetCod(LIST_DEF) = ":LIST:DEF "
        SetDef(LIST_DEF) = "1000" 'Chosen - no default defined
        SetMin(LIST_DEF) = "4"
        SetMax(LIST_DEF) = "262144"
        SetUOM(LIST_DEF) = ""
        SetRes(LIST_DEF) = "N0"
        ExecInit(LIST_DEF) = True

        SetCod(LIST_DEL_ALL) = ":LIST:DEL:ALL"
        ExecInit(LIST_DEL_ALL) = False

        SetCod(LIST_DEL) = ":LIST:DEL"
        ExecInit(LIST_DEL) = False

        SetCod(LIST_FREE) = ":LIST:FREE?"
        SetDef(LIST_FREE) = "262144"
        SetRes(LIST_FREE) = "N0"
        ExecInit(LIST_FREE) = True

        SetCod(LIST_MARK) = ":LIST:MARK "
        SetDef(LIST_MARK) = "0"
        ExecInit(LIST_MARK) = False

        SetCod(LIST_MARK_POIN) = ":LIST:MARK:POIN?"
        ExecInit(LIST_MARK_POIN) = True

        SetCod(LIST_MARK_SPO) = ":LIST:MARK:SPO "
        SetDef(LIST_MARK_SPO) = "1"
        SetMin(LIST_MARK_SPO) = "1"
        SetMax(LIST_MARK_SPO) = "1000" 'Changes with LIST:DEF current setting
        SetUOM(LIST_MARK_SPO) = ""
        SetRes(LIST_MARK_SPO) = "N0"
        ExecInit(LIST_MARK_SPO) = False

        SetCod(LIST_SEL) = ":LIST:SEL "
        ExecInit(LIST_SEL) = True

        SetCod(LIST_VOLT) = ":LIST:VOLT "
        ExecInit(LIST_VOLT) = False

        SetCod(LIST_VOLT_DAC) = ":LIST:VOLT:DAC "
        ExecInit(LIST_VOLT_DAC) = False

        SetCod(LIST_VOLT_POIN) = ":LIST:VOLT:POIN?"
        ExecInit(LIST_VOLT_POIN) = True

        SetCod(LIST_SSEQ_ADDR) = ":LIST:SSEQ:ADDR?"
        ExecInit(LIST_SSEQ_ADDR) = True

        SetCod(LIST_SSEQ_CAT) = ":LIST:SSEQ:CAT?"
        ExecInit(LIST_SSEQ_CAT) = True

        SetCod(LIST_SSEQ_COMB) = ":LIST:SSEQ:COMB "
        ExecInit(LIST_SSEQ_COMB) = False

        SetCod(LIST_SSEQ_COMB_POIN) = ":LIST:SSEQ:COMB:POIN?"
        ExecInit(LIST_SSEQ_COMB_POIN) = True

        SetCod(LIST_SSEQ_DEF) = ":LIST:SSEQ:DEF "
        SetDef(LIST_SSEQ_DEF) = ""
        SetMin(LIST_SSEQ_DEF) = "1"
        SetMax(LIST_SSEQ_DEF) = "32768"
        SetRes(LIST_SSEQ_DEF) = "N0"
        ExecInit(LIST_SSEQ_DEF) = True

        SetCod(LIST_SSEQ_DEL_ALL) = ":LIST:SSEQ:DEL:ALL"
        ExecInit(LIST_SSEQ_DEL_ALL) = False

        SetCod(LIST_SSEQ_DEL) = ":LIST:SSEQ:DEL"
        ExecInit(LIST_SSEQ_DEL) = False

        SetCod(LIST_SSEQ_DWEL_COUN) = ":LIST:SSEQ:DWEL:COUN "
        SetDef(LIST_SSEQ_DWEL_COUN) = ""
        SetMin(LIST_SSEQ_DWEL_COUN) = "1"
        SetMax(LIST_SSEQ_DWEL_COUN) = "4096"
        SetUOM(LIST_SSEQ_DWEL_COUN) = ""
        SetRes(LIST_SSEQ_DWEL_COUN) = "N0"
        ExecInit(LIST_SSEQ_DWEL_COUN) = False

        SetCod(LIST_SSEQ_DWEL_COUN_POIN) = ":LIST:SSEQ:DWEL:POIN?"
        ExecInit(LIST_SSEQ_DWEL_COUN_POIN) = True

        SetCod(LIST_SSEQ_FREE) = ":LIST:SSEQ:FREE?"
        SetRes(LIST_SSEQ_FREE) = "N0"
        ExecInit(LIST_SSEQ_FREE) = True

        SetCod(LIST_SSEQ_MARK) = ":LIST:SSEQ:MARK "
        ExecInit(LIST_SSEQ_MARK) = False

        SetCod(LIST_SSEQ_MARK_POIN) = ":LIST:SSEQ:MARK:POIN?"
        ExecInit(LIST_SSEQ_MARK_POIN) = True

        SetCod(LIST_SSEQ_MARK_SPO) = ":LIST:SSEQ:MARK:SPO "
        SetDef(LIST_SSEQ_MARK_SPO) = ""
        SetMin(LIST_SSEQ_MARK_SPO) = "1"
        SetMax(LIST_SSEQ_MARK_SPO) = "32768"
        SetUOM(LIST_SSEQ_MARK_SPO) = ""
        SetRes(LIST_SSEQ_MARK_SPO) = "N0"
        ExecInit(LIST_SSEQ_MARK_SPO) = False

        SetCod(LIST_SSEQ_SEL) = ":LIST:SSEQ:SEL "
        SetDef(LIST_SSEQ_SEL) = "DEFAULT"
        ExecInit(LIST_SSEQ_SEL) = True

        SetCod(LIST_SSEQ_SEQ) = ":LIST:SSEQ:SEQ "
        ExecInit(LIST_SSEQ_SEQ) = False

        SetCod(LIST_SSEQ_SEQ_SEG) = ":LIST:SSEQ:SEQ:SEG?"
        ExecInit(LIST_SSEQ_SEQ_SEG) = True

        SetCod(LIST2_FORM) = ":LIST2:FORM "
        SetDef(LIST2_FORM) = "ASC"
        ExecInit(LIST2_FORM) = False

        '    SetCod$(LIST2_FREQ) = ":LIST2:FREQ "
        ExecInit(LIST2_FREQ) = False

        SetCod(LIST2_POIN) = ":LIST2:POIN?"
        ExecInit(LIST2_POIN) = True

        SetCod(MARK_ECLT0_FEED) = ":MARK:ECLT0:FEED "
        SetDef(MARK_ECLT0_FEED) = "Arm:Start:Layer1"
        ExecInit(MARK_ECLT0_FEED) = True

        SetCod(MARK_ECLT1_FEED) = ":MARK:ECLT1:FEED "
        SetDef(MARK_ECLT1_FEED) = "Trigger:Start"
        ExecInit(MARK_ECLT1_FEED) = True

        SetCod(MARK_ECLT0) = ":MARK:ECLT0 "
        SetDef(MARK_ECLT0) = "OFF"
        ExecInit(MARK_ECLT0) = True

        SetCod(MARK_ECLT1) = ":MARK:ECLT1 "
        SetDef(MARK_ECLT1) = "OFF"
        ExecInit(MARK_ECLT1) = True

        SetCod(MARK_FEED) = ":MARK:FEED "
        SetDef(MARK_FEED) = "Arm:Start:Layer1"
        ExecInit(MARK_FEED) = True

        SetCod(MARK_POL) = ":MARK:POL "
        SetDef(MARK_POL) = "Normal"
        ExecInit(MARK_POL) = True

        SetCod(MARK) = ":MARK "
        SetDef(MARK) = "ON"
        ExecInit(MARK) = True

        SetCod(PM) = ":PM "
        SetDef(PM) = "0"
        SetMin(PM) = "-3.141592654"
        SetMax(PM) = "3.141592654"
        SetUOM(PM) = "RAD"
        SetRes(PM) = "A9"
        ExecInit(PM) = True

        SetCod(PM_SOUR) = ":PM:SOUR "
        SetDef(PM_SOUR) = "INT"
        ExecInit(PM_SOUR) = True

        SetCod(PM_STAT) = ":PM:STAT "
        SetDef(PM_STAT) = "OFF"
        ExecInit(PM_STAT) = True

        SetCod(PM_UNIT) = ":PM:UNIT "
        SetDef(PM_UNIT) = "RAD"
        ExecInit(PM_UNIT) = True

        SetCod(RAMP_POL) = ":RAMP:POL "
        SetDef(RAMP_POL) = "Normal"
        ExecInit(RAMP_POL) = False

        SetCod(RAMP_POIN) = ":RAMP:POIN "
        SetDef(RAMP_POIN) = "100"
        SetMin(RAMP_POIN) = "4"
        SetMax(RAMP_POIN) = "262144"
        SetUOM(RAMP_POIN) = "Pts"
        SetRes(RAMP_POIN) = "N0"
        ExecInit(RAMP_POIN) = False

        SetCod(ROSC_FREQ_EXT) = ":ROSC:FREQ:EXT "
        SetDef(ROSC_FREQ_EXT) = "40000000"
        SetMin(ROSC_FREQ_EXT) = "1"
        SetMax(ROSC_FREQ_EXT) = "40000000"
        SetUOM(ROSC_FREQ_EXT) = "Hz"
        SetRes(ROSC_FREQ_EXT) = "A2"
        ExecInit(ROSC_FREQ_EXT) = False

        SetCod(ROSC_SOUR) = ":ROSC:SOUR "
        SetDef(ROSC_SOUR) = "Internal1"
        ExecInit(ROSC_SOUR) = False

        SetCod(SWE_COUN) = ":SWE:COUN "
        SetDef(SWE_COUN) = "1"
        SetMin(SWE_COUN) = "1"
        SetMax(SWE_COUN) = "2147483647"
        SetUOM(SWE_COUN) = ""
        SetRes(SWE_COUN) = "N0"
        ExecInit(SWE_COUN) = False

        SetCod(SWE_DIR) = ":SWE:DIR "
        SetDef(SWE_DIR) = "Up"
        ExecInit(SWE_DIR) = False

        SetCod(SWE_POIN) = ":SWE:POIN "
        SetDef(SWE_POIN) = "800"
        SetMin(SWE_POIN) = "2"
        SetMax(SWE_POIN) = "1073741824"
        SetUOM(SWE_POIN) = ""
        SetRes(SWE_POIN) = "N0"
        SetMinInc(SWE_POIN) = "1"
        ExecInit(SWE_POIN) = False

        SetCod(SWE_SPAC) = ":SWE:SPAC "
        SetDef(SWE_SPAC) = "Linear"
        ExecInit(SWE_SPAC) = False

        SetCod(SWE_TIME) = ":SWE:TIME "
        SetDef(SWE_TIME) = "1"
        SetMin(SWE_TIME) = ".99875"
        SetMax(SWE_TIME) = "3351.248696"
        SetUOM(SWE_TIME) = "S"
        SetRes(SWE_TIME) = "N8"
        ExecInit(SWE_TIME) = False

        SetCod(VOLT) = ":VOLT "
        SetDef(VOLT) = "0.161869088"
        SetMin(VOLT) = "0.161869088"
        SetMax(VOLT) = "5.11875"
        SetUOM(VOLT) = "V"
        SetRes(VOLT) = "A5"
        SetMinInc(VOLT) = "0.00125"
        ExecInit(VOLT) = True

        SetDef(VOLT_DIV_7) = "0"
        SetMin(VOLT_DIV_7) = "0.023124"
        SetMax(VOLT_DIV_7) = "0.73125"
        SetUOM(VOLT_DIV_7) = "V"
        SetRes(VOLT_DIV_7) = "A6"
        SetMinInc(VOLT_DIV_7) = "1.78571428571429E-04"

        SetCod(VOLT_UNIT) = ":VOLT:UNIT "
        SetDef(VOLT_UNIT) = "V"
        ExecInit(VOLT_UNIT) = True

        SetCod(VOLT_OFFS) = ":VOLT:OFFS "
        SetDef(VOLT_OFFS) = "0"
        SetMin(VOLT_OFFS) = "-0.999432034"
        SetMax(VOLT_OFFS) = "0.999432034"
        SetUOM(VOLT_OFFS) = "V"
        SetRes(VOLT_OFFS) = "A9"
        SetMinInc(VOLT_OFFS) = "0.000499966"
        ExecInit(VOLT_OFFS) = True

        SetCod(STAT_OPC_INIT) = ":STAT:OPC:INIT "
        SetDef(STAT_OPC_INIT) = "ON"
        ExecInit(STAT_OPC_INIT) = True

        SetCod(STAT_OPER_COND) = ":STAT:OPER:COND? "
        ExecInit(STAT_OPER_COND) = True

        SetCod(STAT_OPER_ENAB) = ":STAT:OPER:ENAB "
        SetDef(STAT_OPER_ENAB) = "0"
        SetMin(STAT_OPER_ENAB) = "0"
        SetMax(STAT_OPER_ENAB) = "32767"
        SetRes(STAT_OPER_ENAB) = "N0"
        ExecInit(STAT_OPER_ENAB) = True

        SetCod(STAT_OPER) = ":STAT:OPER?"
        ExecInit(STAT_OPER) = True

        SetCod(STAT_OPER_NTR) = ":STAT:OPER:NTR "
        SetDef(STAT_OPER_NTR) = "0"
        SetMin(STAT_OPER_NTR) = "0"
        SetMax(STAT_OPER_NTR) = "32767"
        SetRes(STAT_OPER_NTR) = "N0"
        ExecInit(STAT_OPER_NTR) = True

        SetCod(STAT_OPER_PTR) = ":STAT:OPER:PTR "
        SetDef(STAT_OPER_PTR) = "32767"
        SetMin(STAT_OPER_PTR) = "0"
        SetMax(STAT_OPER_PTR) = "32767"
        SetRes(STAT_OPER_PTR) = "N0"
        ExecInit(STAT_OPER_PTR) = True

        SetCod(STAT_PRES) = ":STAT:PRES"
        ExecInit(STAT_PRES) = True

        SetCod(SYST_ERR) = ":SYST:ERR?"
        ExecInit(SYST_ERR) = True

        SetCod(SYST_VER) = ":SYST:VER?"
        ExecInit(SYST_VER) = True

        SetCod(TRIG_COUN) = ":TRIG:COUN "
        SetDef(TRIG_COUN) = "9.91e+37"
        SetMin(TRIG_COUN) = "9.91e+37"
        SetMax(TRIG_COUN) = "9.91e+37"
        SetRes(TRIG_COUN) = "A0"
        ExecInit(TRIG_COUN) = False

        SetCod(TRIG_GATE_POL) = ":TRIG:GATE:POL "
        SetDef(TRIG_GATE_POL) = "Inverted"
        ExecInit(TRIG_GATE_POL) = False

        SetCod(TRIG_GATE_SOUR) = ":TRIG:GATE:SOUR "
        SetDef(TRIG_GATE_SOUR) = "External"
        ExecInit(TRIG_GATE_SOUR) = False

        SetCod(TRIG_GATE_STAT) = ":TRIG:GATE:STAT "
        SetDef(TRIG_GATE_STAT) = "OFF"
        ExecInit(TRIG_GATE_STAT) = False

        SetCod(TRIG) = ":TRIG"
        ExecInit(TRIG) = True

        SetCod(TRIG_SLOP) = ":TRIG:SLOP "
        SetDef(TRIG_SLOP) = "Positive"
        ExecInit(TRIG_SLOP) = False

        SetCod(TRIG_SOUR) = ":TRIG:SOUR "
        SetDef(TRIG_SOUR) = "Internal1"
        ExecInit(TRIG_SOUR) = False

        SetCod(TRIG_STOP) = ":TRIG:STOP"
        ExecInit(TRIG_STOP) = True

        SetCod(TRIG_STOP_SLOP) = ":TRIG:STOP:SLOP "
        SetDef(TRIG_STOP_SLOP) = "Positive"
        ExecInit(TRIG_STOP_SLOP) = False

        SetCod(TRIG_STOP_SOUR) = ":TRIG:STOP:SOUR "
        SetDef(TRIG_STOP_SOUR) = "Hold"
        ExecInit(TRIG_STOP_SOUR) = False

        SetCod(TRIG_SWE) = ":TRIG:SWE"
        ExecInit(TRIG_SWE) = True

        SetCod(TRIG_SWE_LINK) = ":TRIG:SWE:LINK "
        SetDef(TRIG_SWE_LINK) = "ARM:LAY2"
        ExecInit(TRIG_SWE_LINK) = True

        SetCod(TRIG_SWE_SOUR) = ":TRIG:SWE:SOUR "
        SetDef(TRIG_SWE_SOUR) = "Timer"
        ExecInit(TRIG_SWE_SOUR) = False

        SetCod(TRIG_SWE_TIM) = ":TRIG:SWE:TIM "
        SetDef(TRIG_SWE_TIM) = 1.0 / 799.0
        SetMin(TRIG_SWE_TIM) = ".00125"
        SetMax(TRIG_SWE_TIM) = "4.19430375"
        SetUOM(TRIG_SWE_TIM) = "S"
        SetRes(TRIG_SWE_TIM) = "A8"
        ExecInit(TRIG_SWE_TIM) = False

        SetCod(VINS_LBUS) = ":VINS:LBUS "
        SetDef(VINS_LBUS) = "OFF"
        ExecInit(VINS_LBUS) = True

        SetCod(VINS_LBUS_AUTO) = ":VINS:LBUS:AUTO "
        SetDef(VINS_LBUS_AUTO) = "ON"
        ExecInit(VINS_LBUS_AUTO) = True

        SetCod(VINS_TEST_CONF) = ":VINS:TEST:CONF "
        SetDef(VINS_TEST_CONF) = ""
        SetMin(VINS_TEST_CONF) = "2"
        SetMax(VINS_TEST_CONF) = "32768"
        SetRes(VINS_TEST_CONF) = "N0"
        ExecInit(VINS_TEST_CONF) = False

        SetCod(VINS_TEST_DATA) = ":VINS:TEST:DATA?"
        ExecInit(VINS_TEST_DATA) = False

        SetCod(VINS_VME) = ":VINS:VME "
        SetDef(VINS_VME) = "CONS"
        ExecInit(VINS_VME) = True

        SetCod(VINS_VME_REC_ADDR_DATA) = ":VINS:VME:REC:ADDR:DATA?"
        ExecInit(VINS_VME_REC_ADDR_DATA) = True

        SetCod(VINS_VME_REC_ADDR_READ) = ":VINS:VME:REC:ADDR:READ?"
        ExecInit(VINS_VME_REC_ADDR_READ) = True

        SetCod(VINS_IDEN) = ":VINS:IDEN?"
        ExecInit(VINS_IDEN) = True


        SetUOM(JDH_TIME) = "Sec"
        SetRes(JDH_TIME) = "D6"

        SetUOM(JDH_FREQ) = "Hz"
        SetRes(JDH_FREQ) = "D6"

        SetDef(JDH_CYCLES) = "1"
        SetMin(JDH_CYCLES) = "0.1"
        SetMax(JDH_CYCLES) = "131072"
        SetUOM(JDH_CYCLES) = "Cycles"
        SetRes(JDH_CYCLES) = "N2"
        SetMinInc(JDH_CYCLES) = "0.1"

        SetDef(JDH_PHASE) = "0"
        SetMin(JDH_PHASE) = "-360"
        SetMax(JDH_PHASE) = "360"
        SetUOM(JDH_PHASE) = "Degrees"
        SetRes(JDH_PHASE) = "N0"
        SetMinInc(JDH_PHASE) = "1"

        SetDef(JDH_DCYC) = "50"
        SetMin(JDH_DCYC) = "0.1"
        SetMax(JDH_DCYC) = "99.9"
        SetUOM(JDH_DCYC) = "%"
        SetRes(JDH_DCYC) = "N5"
        SetMinInc(JDH_DCYC) = SetMin(JDH_DCYC)

        SetDef(JDH_PM_CARR) = ""
        SetMin(JDH_PM_CARR) = "10"
        SetMax(JDH_PM_CARR) = "5000000"
        SetUOM(JDH_PM_CARR) = "Hz"
        SetRes(JDH_PM_CARR) = "A5"
        SetMinInc(JDH_PM_CARR) = ""

        SetDef(JDH_PM_MOD) = ""
        SetMin(JDH_PM_MOD) = "0"
        SetMax(JDH_PM_MOD) = "500000"
        SetUOM(JDH_PM_MOD) = "Hz"
        SetRes(JDH_PM_MOD) = "A5"
        SetMinInc(JDH_PM_MOD) = ""

        SetDef(JDH_PM_POS_DEV) = "180"
        SetMin(JDH_PM_POS_DEV) = "-360"
        SetMax(JDH_PM_POS_DEV) = "360"
        SetUOM(JDH_PM_POS_DEV) = "Degrees"
        SetRes(JDH_PM_POS_DEV) = "N0"
        SetMinInc(JDH_PM_POS_DEV) = "1"

        SetDef(JDH_PM_NEG_DEV) = "-180"
        SetMin(JDH_PM_NEG_DEV) = "-360"
        SetMax(JDH_PM_NEG_DEV) = "360"
        SetUOM(JDH_PM_NEG_DEV) = "Degrees"
        SetRes(JDH_PM_NEG_DEV) = "N0"
        SetMinInc(JDH_PM_NEG_DEV) = "1"

        SetDef(JDH_DWEL_COUN) = "1" ' Used for individual dwell count controls
        SetMin(JDH_DWEL_COUN) = "1"
        SetMax(JDH_DWEL_COUN) = "4096"
        SetUOM(JDH_DWEL_COUN) = ""
        SetRes(JDH_DWEL_COUN) = "N0"

        '    SetCod$() = ": "
        '    SetDef$() = ""
        '    SetMin$() = ""
        '    SetMax$() = ""
        '    SetUOM$() = ""
        '    SetRes$() = "A0"
        '    SetMinInc$() = ""

        SetControlsToReset()

        With frmHpE1445a
            .XAxis.Range = New Range(1, Val(SetDef(LIST_DEF)))
            .YAxis1.Range = New Range(-Val(SetMax(VOLT)), Val(SetMax(VOLT)))
            XSpan = .XAxis.Range.Maximum - .XAxis.Range.Minimum
            YSpan = .YAxis1.Range.Maximum - .YAxis1.Range.Minimum
            .CursorTo.XPosition = 0
            .CursorTo.YPosition = 0
            .CursorFrom.XPosition = 0
            .CursorFrom.YPosition = 0
            .CursorMarker.XPosition = 0
            .CursorMarker.YPosition = 0
            .CursorMarker.Visible = False
            .CursorTo.SnapMode = CursorSnapMode.Floating
            .cmdSnap.BackColor = Color.White
            .G1.InteractionModeDefault = GraphDefaultInteractionMode.None
            .cmdZoom.BackColor = Color.White
            .cmdPan.BackColor = Color.White
            .CursorFrom.XPosition = 1
            ReDim Wave(Convert.ToInt32(SetDef(LIST_DEF)))
            ReDim Back(Convert.ToInt32(SetDef(LIST_DEF)))
            .G1.PlotY(Wave, 1, 1)
            .tabOptions.SelectedIndex = TAB_MAIN
        End With
    End Sub

    Sub AdjustUserWave()
        Dim NumSamps As Integer, VOffs As Double, i As Integer

        Try
            If SetCur(RAMP_POL) = "Inverted" Then
                For i = LBound(Wave) To UBound(Wave)
                    Wave(i) = -Wave(i)
                Next
            End If

            VOffs = Val(SetCur(VOLT_OFFS))

            NumSamps = CLng(SetCur(LIST_DEF))
            If VOffs <> 0 Then
                For i = 1 To NumSamps
                    Wave(i) += VOffs
                Next
            End If
            frmHpE1445a.Plot1.LineStyle = NationalInstruments.UI.LineStyle.Solid
            frmHpE1445a.G1.ClearData()
            frmHpE1445a.G1.PlotY(Wave, 1, 1)
        Catch ex As Exception
            MessageBox.Show("AdjustUserWave: " + ex.Message)
        End Try
    End Sub

    Public Sub ApplyDcOffsToWave()
        Dim VOffs As Double, Change As Double, NumSamps As Integer, i As Integer

        VOffs = Val(SetCur(VOLT_OFFS))
        If VOffs = LastOffs Then
            Exit Sub
        Else
            Change = VOffs - LastOffs 'Get difference
            LastOffs = VOffs 'Store for next time
        End If

        NumSamps = CLng(SetCur(LIST_DEF))
        If Change <> 0 Then
            If Wave.Length = NumSamps Then
                For i = 1 To NumSamps
                    Wave(i) += Change
                Next
            End If
        End If
        frmHpE1445a.G1.PlotY(Wave, 1, 1)
    End Sub

    Sub SetControlsToReset()
        Dim i As Short
        Dim S As String

        'Change all Current Settings to their Defaults
        SetDef(VOLT) = "0.161869088"
        For i = 1 To MAX_SETTINGS
            SetCur(i) = SetDef(i)
        Next i

        bRunningTipCmds = False
        UserError = False
        SetMax(LIST_MARK_SPO) = SetCur(LIST_DEF)
        VOLT_At_V_50 = Val(SetDef(VOLT))
        OFFS_At_V_50 = Val(SetDef(VOLT_OFFS))
        RefOscFreq = Val(SetDef(ROSC_FREQ_EXT))
        CurFreqIdx = FREQ

        WaveIdx = 1

        ' Display default settings
        With frmHpE1445a
            '*** Main Form Settings
            .ribInit.Pushed = False
            .ribAbor.Pushed = True
            frmHpE1445a.ribAbor_Click()
            Initialized = False 'Reset this flag that was set by ribInit
            .imgWaveDisplay.Image = .imlOutRelayWave.Images(0)
            .cboRoscSour.Text = .cboRoscSour.Items.Item(4).ToString() '"Internal1"
            frmHpE1445a.cboRoscSour_SelectedIndexChanged(frmHpE1445a, New System.EventArgs())

            '*** "Main" Tab
            .tolFuncWave_ButtonClick(frmHpE1445a, New ToolBarButtonClickEventArgs(.tolFuncWave.Buttons(0))) 'Run Sine parameters
            .tolFuncWave.Buttons(0).Pushed = True 'Depress Sine button
            .txtFreq.Text = EngNotate(Val(SetDef(FREQ)), FREQ)
            .cboRoscSour.Text = .cboRoscSour.Items.Item(4).ToString() '"Internal1"
            frmHpE1445a.cboRoscSour_SelectedIndexChanged(frmHpE1445a, New System.EventArgs())
            .txtAmpl.Text = EngNotate(Val(SetDef(VOLT)), VOLT)
            .cboUnits.Text = SetDef(VOLT_UNIT)
            .txtDcOffs.Text = EngNotate(Val(SetDef(VOLT_OFFS)), VOLT_OFFS)
            .cboWavePol.Text = .cboWavePol.Items.Item(0).ToString()
            .txtWavePoin.Text = EngNotate(Val(SetDef(RAMP_POIN)), RAMP_POIN)
            .ribAbor.Enabled = True

            '*** "Arm-Trig" Tab
            .cboArmLay2Sour.Text = .cboArmLay2Sour.Items.Item(5).ToString()
            .cboArmLay2Slop.Text = .cboArmLay2Slop.Items.Item(1).ToString()
            .txtArmLay2Coun.Text = EngNotate(Val(SetDef(ARM_LAY2_COUN)), ARM_LAY2_COUN)
            .cboTrigStopSour.Text = .cboTrigStopSour.Items.Item(2).ToString() '"Hold"
            .cboTrigSlop.Text = .cboTrigSlop.Items.Item(1).ToString()
            .cboTrigStopSlop.Text = .cboTrigStopSlop.Items.Item(1).ToString()
            .txtArmCoun.Text = SetDef(ARM_COUN)
            .cboTrigGatePol.Text = .cboTrigGatePol.Items.Item(0).ToString()
            .cboTrigGateSour.Visible = False
            .cboTrigGateSour.Text = .cboTrigGateSour.Items.Item(0).ToString()
            .chkTrigGateStat.Checked = False
            .chkTrigGateStat_Click(False)

            '*** "Markers" Tab
            .cboMarkFeed.Text = .cboMarkFeed.Items.Item(0).ToString()
            .cboMarkPol.Text = .cboMarkPol.Items.Item(1).ToString()
            .chkMark.Checked = True
            .chkMark_Click(True)
            .cboMarkEclt0Feed.Text = .cboMarkEclt0Feed.Items.Item(0).ToString()
            .chkMarkEclt0.Checked = False
            .chkMarkEclt0_Click(False)
            .cboMarkEclt1Feed.Text = .cboMarkEclt1Feed.Items.Item(6).ToString()
            .chkMarkEclt1.Checked = False
            .chkMarkEclt1_Click(False)
            .txtMarkSpo.Text = EngNotate(Val(SetDef(LIST_MARK_SPO)), LIST_MARK_SPO)

            '*** "Freq. Agility" Tab
            .optFreqModeFix.Checked = True
            .optFreqModeFix_Click(True)
            .optFreqModeSwe.Checked = False
            .optFreqModeFsk.Checked = False
            .cboAgileFskSour.Text = .cboAgileFskSour.Items.Item(0).ToString()
            .txtAgileFskKeyL.Text = EngNotate(SetCur(FREQ_FSK_L), FREQ_FSK_L)
            .txtAgileFskKeyH.Text = EngNotate(SetCur(FREQ_FSK_H), FREQ_FSK_H)
            .txtAgileSweepStart.Text = EngNotate(SetCur(FREQ_STAR), FREQ_STAR)
            .txtAgileSweepStop.Text = EngNotate(SetCur(FREQ_STOP), FREQ_STOP)
            .txtAgileSweepSteps.Text = EngNotate(SetCur(SWE_POIN), SWE_POIN)
            .txtAgileSweepDur.Text = EngNotate(SetCur(SWE_TIME), SWE_TIME)
            .cboAgileSweepDir.Text = .cboAgileSweepDir.Items.Item(1).ToString()
            .cboAgileSweepSpac.Text = .cboAgileSweepSpac.Items.Item(0).ToString()
            .cboAgileSweepTrigSour.Text = .cboAgileSweepTrigSour.Items.Item(2).ToString()
            .cboAgileSweepAdvSour.Text = .cboAgileSweepAdvSour.Items.Item(3).ToString()
            .txtSweCoun.Text = EngNotate(SetCur(SWE_COUN), SWE_COUN)

            '*** "User" Tab
            .txtUserName.Text = ""
            .txtUserPoints.Text = EngNotate(SetDef(LIST_DEF), LIST_DEF)
            .txtSampleFreq.Text = .txtFreq.Text
            .lblModFreq.Text = ""
            .lblWrf.Text = ""
            .lblModFreq.Visible = False
            .lblModFreq1.Visible = False
            .cmdZoomOut_Click()
            .cmdUserClear_Click()
            'ADVINT removed        .dlgFileIO.DefaultExt = "seg"
            'ADVINT removed        .dlgFileIO.Filter = "Segment File (*.seg)|*.seg|All Files (*.*)|*.*"
            .prgDownLoad.Visible = False
            .Plot1.LineStep = NationalInstruments.UI.LineStep.XYStep
            .cmdUserUndo.Enabled = False

            '*** "Sequence" Tab
            .lstListCat.Items.Clear()
            .lstListSseqSeq.Items.Clear()
            .txtDwelCoun.Text = ""
            DoNotTalk = False
            SendCommand(LIST_SSEQ_DEL_ALL)
            SendCommand(LIST_DEL_ALL)
            WriteInstrument(":LIST:FREE?")
            S = ReadARB()
            DoNotTalk = True
            If S <> "" Then
                .lblListFree.Text = Val(S)
            Else
                .lblListFree.Text = SetDef(LIST_FREE)
            End If

            '*** "Options" Tab
            .optLoadImp50.Checked = False
            .optLoadImpHigh.Checked = True
            .optLoadImpHigh_Click(True)
            .chkFilterStateOn.Checked = False
            .chkFilterStateOn_Click(False)
            .optCutOff250.Checked = True
            .optCutOff250_Click(True)
            .chkOutpStat.Checked = True
            .chkOutpStat_Click(True)
            '        .lblConfigFileName = ""
        End With
    End Sub

    Public Sub UpdateGraph()
        UpdateWrf()
        With frmHpE1445a
            ReDim Back(Wave.Length)
            Array.Copy(Wave, Back, Wave.Length)
            .cmdUserUndo.Enabled = True
            If Val(SetCur(LIST_DEF)) <= .XAxis.Range.Minimum Then
                If (Val(SetCur(LIST_DEF)) - XSpan) < 1 Then
                    .XAxis.Range = New Range(1, .XAxis.Range.Maximum)
                Else
                    .XAxis.Range = New Range(Val(SetCur(LIST_DEF)) - XSpan, .XAxis.Range.Maximum)
                End If
            End If
            .XAxis.Range = New Range(.XAxis.Range.Minimum, Val(SetCur(LIST_DEF)))
            .YAxis1.Range = New Range(-1 * (GetFlatValue(.txtAmpl.Text) + GetFlatValue(.txtDcOffs.Text)), GetFlatValue(.txtAmpl.Text) + GetFlatValue(.txtDcOffs.Text))
            XSpan = .XAxis.Range.Maximum - .XAxis.Range.Minimum
            YSpan = .YAxis1.Range.Maximum - .YAxis1.Range.Minimum
            .Plot1.LineStyle = NationalInstruments.UI.LineStyle.Solid
            .G1.ClearData()
            .G1.PlotY(Wave, 1, 1)
        End With
    End Sub

    Public Sub UpdateWrf()
        Dim Cycles As Integer, ModFreq As Double

        With frmHpE1445a
            Cycles = Int(Val(SetCur(JDH_CYCLES)))
            If Cycles < 1 Then Cycles = 1
            ModFreq = Val(SetCur(CurFreqIdx)) / Val(SetCur(LIST_DEF))
            .lblModFreq.Text = EngNotate(ModFreq, JDH_FREQ)
            If .lblWrf1.Text = "Segment Freq." Then
                .lblWrf.Text = EngNotate(Round(ModFreq, 4), JDH_FREQ)
            Else
                .lblWrf.Text = EngNotate(Round((ModFreq * Cycles), 4), JDH_FREQ)
            End If
        End With
    End Sub

    Public Sub SetAmplFromDiv7()
        Dim Factor As Short

        If SetCur(VOLT_UNIT) = "W" Then 'If Watts
            Factor = 49
        Else
            Factor = 7
        End If

        SetCur(VOLT) = Val(SetCur(VOLT_DIV_7)) * Factor
        frmHpE1445a.txtAmpl.Text = EngNotate(Val(SetCur(VOLT)), VOLT)
        SendCommand(VOLT)
    End Sub

    Public Sub SetDiv7FromAmpl()
        Dim Factor As Short

        If SetCur(VOLT_UNIT) = "W" Then 'If Watts
            Factor = 49
        Else
            Factor = 7
        End If

        SetCur(VOLT_DIV_7) = Val(SetCur(VOLT)) / Factor
        frmHpE1445a.txtAmplDiv7.Text = EngNotate(Val(SetCur(VOLT_DIV_7)), VOLT)
        SendCommand(VOLT)
    End Sub
End Module