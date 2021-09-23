VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "cARB"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
DefInt A-Z
'bbbb 8-3-2011 STR10606#2
'Made the following changes to the ARB class
'changed the loadConfigFile routine to not go through the cicl


'****************************************************************
'*** Constants **************************************************
'****************************************************************

'Amplitude Rules
'****************************************************************
'Matched Impedance
Const DCMax = 5.11875
Const DCMin = -5.12

'Matched Impedance
Const DACFullScaleMax = 5.11875
Const DACFullScaleMin = 0.16187

'DC Offset Rules
'****************************************************************

'Frequency Rules
'****************************************************************
Const RoscInt1Freq = 42949672.96

Const SineFrequencyMax = 10737418.24
Const SineFrequencyMin = 0.01
Const SineFrequencyResolution = 0.01

'Points Rules
'****************************************************************
Const RampPointsMax = 262144
Const RampPointsMin = 4

' First Coupling Group: "Frequency"
Const TRIG_SOUR = 1
Const ROSC_FREQ_EXT = 2
Const ROSC_SOUR = 3
Const ARM_SWE_COUN = 4
Const ARM_SWE_SOUR = 5
Const FREQ_CENT = 6
Const FREQ = 7
Const FREQ_FSK_L = 8
Const FREQ_FSK_H = 9
Const FREQ_FSK_SOUR = 10
Const FREQ_MODE = 11
Const FREQ_RANG = 12
Const FREQ_SPAN = 13
Const FREQ_STAR = 14
Const FREQ_STOP = 15
Const FREQ2 = 16
Const LIST2_FREQ = 17
Const PM_SOUR = 18
Const PM_STAT = 19

Const SWE_COUN = 20
Const SWE_DIR = 21
Const SWE_POIN = 22
Const SWE_SPAC = 23
Const SWE_TIME = 24
Const TRIG_GATE_POL = 25
Const TRIG_GATE_SOUR = 26
Const TRIG_GATE_STAT = 27

Const TRIG_STOP_SLOP = 28
Const TRIG_STOP_SOUR = 29
Const TRIG_SWE_SOUR = 30
Const TRIG_SWE_TIM = 31
Const LAST_COUP_GROUP_1_COMMAND = TRIG_SWE_TIM

' Second Coupling Group: "Frequency & Voltage"
' Send these with both groups
Const ARB_DAC_SOUR = 32
Const FUNC = 33
Const RAMP_POIN = 34
Const LAST_COUP_GROUP_2_COMMAND = RAMP_POIN

' Third Coupling Group: "Voltage"
Const OUTP_IMP = 35
Const OUTP_LOAD = 36
Const OUTP_LOAD_AUTO = 37
Const RAMP_POL = 38
Const VOLT = 39
Const VOLT_OFFS = 40
Const LAST_COUP_GROUP_3_COMMAND = VOLT_OFFS

' Non-Coupled: May be sent before or after coupled commands, but not in-between
Const ABOR = 41
Const ARM_COUN = 42
Const ARM_LAY2_COUN = 43
Const ARM_LAY2 = 44
Const ARM_LAY2_SLOP = 45
Const ARM_LAY2_SOUR = 46
Const ARM_SWE = 47
Const ARM_SWE_LINK = 48
Const CAL_COUN = 49
Const CAL_DATA_AC = 50
Const CAL_DATA_AC2 = 51
Const CAL_DATA = 52
Const CAL_BEG = 53
Const CAL_POIN = 54
Const CAL_SEC_CODE = 55
Const CAL_SEC = 56
Const CAL_STAT = 57
Const CAL_STAT_AC = 58
Const CAL_STAT_DC = 59
Const INIT = 60
Const OUTP_FILT_FREQ = 61
Const OUTP_FILT = 62
Const OUTP = 63
Const ARB_DAC_FORM = 64
Const ARB_DOWN = 65
Const ARB_DOWN_COMP = 66
Const FUNC_USER = 67
Const LIST_FORM = 68
Const LIST_ADDR = 69
Const LIST_CAT = 70
Const LIST_COMB = 71
Const LIST_COMB_POIN = 72
Const LIST_DEF = 73
Const LIST_DEL_ALL = 74
Const LIST_DEL = 75
Const LIST_FREE = 76
Const LIST_MARK = 77
Const LIST_MARK_POIN = 78
Const LIST_MARK_SPO = 79
Const LIST_SEL = 80
Const LIST_VOLT = 81
Const LIST_VOLT_DAC = 82
Const LIST_VOLT_POIN = 83
Const LIST_SSEQ_ADDR = 84
Const LIST_SSEQ_CAT = 85
Const LIST_SSEQ_COMB = 86
Const LIST_SSEQ_COMB_POIN = 87
Const LIST_SSEQ_DEF = 88
Const LIST_SSEQ_DEL_ALL = 89
Const LIST_SSEQ_DEL = 90
Const LIST_SSEQ_DWEL_COUN = 91
Const LIST_SSEQ_DWEL_COUN_POIN = 92
Const LIST_SSEQ_FREE = 93
Const LIST_SSEQ_MARK = 94
Const LIST_SSEQ_MARK_POIN = 95
Const LIST_SSEQ_MARK_SPO = 96
Const LIST_SSEQ_SEL = 97
Const LIST_SSEQ_SEQ = 98
Const LIST_SSEQ_SEQ_SEG = 99
Const LIST2_FORM = 100
Const LIST2_POIN = 101
Const MARK_ECLT0_FEED = 102
Const MARK_ECLT0 = 103
Const MARK_ECLT1_FEED = 104
Const MARK_ECLT1 = 105
Const MARK_FEED = 106
Const MARK_POL = 107
Const MARK = 108
Const PM = 109
Const PM_UNIT = 110
Const VOLT_UNIT = 111
Const STAT_OPC_INIT = 112
Const STAT_OPER_COND = 113
Const STAT_OPER_ENAB = 114
Const STAT_OPER = 115
Const STAT_OPER_NTR = 116
Const STAT_OPER_PTR = 117
Const STAT_PRES = 118
Const SYST_ERR = 119
Const SYST_VER = 120
Const TRIG_COUN = 121
Const TRIG = 122
Const TRIG_SLOP = 123
Const TRIG_STOP = 124
Const TRIG_SWE = 125
Const TRIG_SWE_LINK = 126
Const VINS_LBUS = 127
Const VINS_LBUS_AUTO = 128
Const VINS_TEST_CONF = 129
Const VINS_TEST_DATA = 130
Const VINS_VME = 131
Const VINS_VME_REC_ADDR_DATA = 132
Const VINS_VME_REC_ADDR_READ = 133
Const VINS_IDEN = 134

'These are not SCPI parameters
Const JDH_TIME = 135
Const JDH_FREQ = 136
Const JDH_CYCLES = 137
Const JDH_PHASE = 138
Const JDH_DCYC = 139
Const JDH_PM_CARR = 140
Const JDH_PM_MOD = 141
Const JDH_PM_POS_DEV = 142
Const JDH_PM_NEG_DEV = 143
Const JDH_DWEL_COUN = 144
Const VOLT_DIV_7 = 145

Const MAX_SETTINGS = 145


Private SetRes$(1 To MAX_SETTINGS)
Private SetUOM$(1 To MAX_SETTINGS)
Private SetMax$(1 To MAX_SETTINGS)
Private SetMin$(1 To MAX_SETTINGS)
Private SetDef$(1 To MAX_SETTINGS)
Private GetVals$(1 To MAX_SETTINGS)
Private SetMinInc$(1 To MAX_SETTINGS)
Private ExecInit%(1 To MAX_SETTINGS)
Private SetCod$(1 To MAX_SETTINGS)

Private CurFreqIdx As Integer

'Were Hidden Text Boxes in frm hpe1445a.frm
Private txtFreq As String
Private txtAmpl As String
Private txtDcOffs As String
Private txtAmplDiv7 As String

Private txtSampleFreq As String
Private UserEnteringData As Integer
Private bActivatingControls As Integer
Private Initiated As Integer

Private VOLT_At_V_50 As Double
Private OFFS_At_V_50 As Double
Private Quote As String
Private instrumentHandle As Long
Private ErrorStatus As Long
'****************************************************************
'*** End Of Constants *******************************************
'****************************************************************

Private SetCur$(1 To MAX_SETTINGS)    ' "Current Settings" Array

Private dVpkStepList() As Double

Private iScale As Integer

Private ParameterError As Boolean

Private Type TracePreamble
  Format As Integer
  Type As Integer
  Points As Long
  count As Integer
  XInc As Double
  XOrig As Double
  XRef As Integer
  YInc As Double
  YOrig As Double
  YRef As Integer
End Type
Private Trace As TracePreamble

Public Sub SendMsg(sMsg As String)
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    frmMain.txtInstrument.Text = "ARB"
    frmMain.txtCommand.Text = "SendMsg"
    WriteMsg ARB, sMsg
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
End Sub

Public Function sReadMsg(sMsg As String) As String
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    frmMain.txtInstrument.Text = "ARB"
    frmMain.txtCommand.Text = "ReadMsg"
    ReadMsg ARB, sReadMsg

    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
End Function

Public Sub LoadConfigFile(sConfigFile As String, Optional nTime As Long = 0)

    Const PRE_SEGMENT = "[PRE_SEGMENT_LOAD_COMMANDS]"
    Const SEGMENT_LIST = "[SEGMENT_LIST]"
    Const POST_SEGMENT = "[POST_SEGMENT_LOAD_COMMANDS]"
    Dim i As Integer
    Dim nStatus As Long
    Dim dOffSet As Double
    Dim FileNumber
    Dim SegFileNumber
    Dim sLineBuffer As String
    Dim sSegmentText As String
    Dim TyxMagicNumber As Long
    Dim TmpName$, NumBytes&, FileSize&, MaxFileSize&
    Dim nSeqmentPoints As Long
    Dim Wave() As Double
    Dim iCurrentProgressValue As Integer
    Dim sCurrentStatusMessage As String
    Dim iCurrentMin As Integer
    Dim iCurrentMax As Integer
    Dim iCurrentValue As Integer
    Dim bView As Boolean
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    frmMain.txtInstrument.Text = "ARB"
    frmMain.txtCommand.Text = "LoadConfigFile"
    nStatus = VI_SUCCESS
    
    If nTime > 5 Then
        iCurrentProgressValue = frmMain.ProgressBar.value
        sCurrentStatusMessage = frmMain.lblStatus.Caption
        iCurrentMax = frmMain.ProgressBar.Max
        iCurrentMin = frmMain.ProgressBar.Min
        iCurrentValue = frmMain.ProgressBar.value
        bView = frmMain.ProgressBar.Visible
        
        frmMain.ProgressBar.Max = 100
        frmMain.ProgressBar.Min = 0
        frmMain.ProgressBar.value = 0
        frmMain.lblStatus.Caption = "Loading Config File ..."
        frmMain.Timer3.Enabled = True
    End If
    
    'Set Timeout value
    If Not bSimulation Then nStatus = viSetAttribute(nInstrumentHandle(ARB), VI_ATTR_TMO_VALUE, 10000) '10 sec.
    If nStatus <> VI_SUCCESS Then
            Echo "ARB PROGRAMMING ERROR:  Command cmdARB.LoadConfigFile - Unable to set VISA Attribute."
            Err.Raise -1001
            Exit Sub
    End If
    
    'Open Configuration File
    FileNumber = FreeFile
    On Error Resume Next
    Open ProgramPath$ + sConfigFile For Input Access Read As #FileNumber
    DoEvents
    If Err Then
        Echo "ARB PROGRAMMING ERROR:  Command cmdARB.LoadConfigFile configuration file not found."
        Err.Raise -1001
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub

    dOffSet = 0#
    'Read Line 1 of Configuration File
    Line Input #FileNumber, sLineBuffer   ' Read line of data.
    If UCase(sLineBuffer) <> PRE_SEGMENT Then
        Echo "ARB PROGRAMMING ERROR:  Command cmdARB.LoadConfigFile configuration file corrupt."
        Err.Raise -1001
        Close FileNumber
        Exit Sub
    End If
    DoEvents
    'Read in PRE_SEGMENT_LOAD_COMMANDS
    Line Input #FileNumber, sLineBuffer   ' Read Next line of data.
    Do While Not EOF(FileNumber)
        DoEvents
        Select Case sLineBuffer
            Case "", SEGMENT_LIST
                Exit Do
            Case Else
                i = InStr(sLineBuffer, "VOLT:OFFS")
                If i <> 0 Then
                    dOffSet = CDbl(Mid(sLineBuffer, i + 10))
                End If
                If Not bSimulation Then WriteMsg2 nInstrumentHandle(ARB), sLineBuffer
                Debug.Print sLineBuffer
        End Select
        Line Input #FileNumber, sLineBuffer   ' Read line of data.
    Loop
    
    'Read in SEGMENT FILE NAMES
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    If sLineBuffer = SEGMENT_LIST Then
        Line Input #FileNumber, sLineBuffer   ' Read line of data.
    End If
    Do While Not EOF(FileNumber)
        DoEvents
        Select Case sLineBuffer
            Case "", POST_SEGMENT
                Exit Do
            Case Else
                If Not bSimulation Then WriteMsg2 nInstrumentHandle(ARB), ":LIST:SEL " & sLineBuffer
                Debug.Print ":LIST:SEL " & sLineBuffer
                sLineBuffer = sLineBuffer & ".SEG"
                'Open Segment File for Read
                SegFileNumber = FreeFile
                On Error Resume Next
                Open ProgramPath$ + sLineBuffer For Binary Access Read As #SegFileNumber
                If Err Then
                    Echo "ARB PROGRAMMING ERROR:  Command cmdARB.LoadConfigFile " & sLineBuffer & " file not found."
                    Err.Raise -1001
                    Exit Sub
                End If
                'Check Unique number
                Get #SegFileNumber, 1, TyxMagicNumber 'Get byte of data
                If TyxMagicNumber <> &HABCD4567 Then
                    Echo "ARB PROGRAMMING ERROR:  Command cmdARB.LoadConfigFile " & sLineBuffer & " file corrupt."
                    Err.Raise -1001
                    Close SegFileNumber
                    Exit Sub
                End If
                'Get Number of Bytes
                Get #SegFileNumber, 5, NumBytes&
                If (NumBytes Mod 8) <> 0 Then
                    Echo "ARB PROGRAMMING ERROR:  Command cmdARB.LoadConfigFile " & sLineBuffer & " file incorrect ."
                    Err.Raise -1001
                    Close SegFileNumber
                    Exit Sub
                End If
                If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
                'Allocate variable for number of datapoints
                nSeqmentPoints = NumBytes / 8
                If Not bSimulation Then WriteMsg2 nInstrumentHandle(ARB), "LIST:DEF " & CStr(nSeqmentPoints)
                ReDim Wave(1 To nSeqmentPoints)
                'Read the segment voltage points
                Get #SegFileNumber, 257, Wave()
                Close SegFileNumber
                'Build String
                sSegmentText = ":LIST:VOLT "
                For i = 1 To (nSeqmentPoints - 1)
                    sSegmentText = sSegmentText & CStr(Wave(i)) & ","
                Next i
                sSegmentText = sSegmentText & CStr(Wave(nSeqmentPoints))
                'Set Time out value per data points
                If nSeqmentPoints > 4000 Then
                    If Not bSimulation Then nStatus = viSetAttribute(nInstrumentHandle(ARB), VI_ATTR_TMO_VALUE, ((nSeqmentPoints / 400) * 1000))
                    If nStatus <> VI_SUCCESS Then
                            Echo "ARB PROGRAMMING ERROR:  Command cmdARB.LoadConfigFile - Unable to set VISA Attribute."
                            Err.Raise -1001
                            Exit Sub
                    End If
                    If Not bSimulation Then WriteMsg2 nInstrumentHandle(ARB), sSegmentText
                    Debug.Print sSegmentText
                    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
                    If Not bSimulation Then nStatus = viSetAttribute(nInstrumentHandle(ARB), VI_ATTR_TMO_VALUE, 10000)
                    If nStatus <> VI_SUCCESS Then
                            Echo "ARB PROGRAMMING ERROR:  Command cmdARB.LoadConfigFile - Unable to set VISA Attribute."
                            Err.Raise -1001
                            Exit Sub
                    End If
                Else
                    If Not bSimulation Then WriteMsg2 nInstrumentHandle(ARB), sSegmentText
                    Debug.Print sSegmentText
                End If
        End Select
        Line Input #FileNumber, sLineBuffer   ' Read line of data.
    Loop

    'Post Segment command
    If sLineBuffer = POST_SEGMENT Then
        Line Input #FileNumber, sLineBuffer   ' Read line of data.
    End If
    Do While Not EOF(FileNumber)
        DoEvents
        Select Case sLineBuffer
            Case ""
                Exit Do
            Case Else
                If Not bSimulation Then WriteMsg2 nInstrumentHandle(ARB), sLineBuffer
                Debug.Print sLineBuffer
        End Select
        Line Input #FileNumber, sLineBuffer   ' Read line of data.
    Loop
    If sLineBuffer <> "" Or sLineBuffer <> POST_SEGMENT Then
        If Not bSimulation Then WriteMsg2 nInstrumentHandle(ARB), sLineBuffer
        Debug.Print sLineBuffer
    End If
    If Not bSimulation Then WriteMsg2 nInstrumentHandle(ARB), ":INIT"
    Debug.Print ":INIT"
    
    If nTime > 5 Then
        frmMain.ProgressBar.value = iCurrentProgressValue
        frmMain.lblStatus.Caption = sCurrentStatusMessage
        frmMain.Timer3.Enabled = False
        frmMain.ProgressBar.Max = iCurrentMax
        frmMain.ProgressBar.Min = iCurrentMin
        frmMain.ProgressBar.value = iCurrentValue
        frmMain.ProgressBar.Visible = bView
        frmMain.Timer3.Enabled = False
    End If

End Sub

Public Sub Remove()
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    frmMain.txtInstrument.Text = "ARB"
    frmMain.txtCommand.Text = "Remove"

    If Not bSimulation Then WriteMsg ARB, "*RST;*CLS"
End Sub

Public Sub StopAll()
    
    If Not bSimulation Then
      WriteMsg ARB, "ABOR "
      WriteMsg ARB, "OUTP:STAT OFF"
      WriteMsg ARB, "*RST"
    End If
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
End Sub

Public Sub StandardWaveform(sShape As String, _
                            dAmplitude#, _
                            Optional dFrequency# = 1000, _
                            Optional dDCOffset# = 0#, _
                            Optional sOutputZ As Variant = "50", _
                            Optional sLoadZ As Variant = "Hi-Impedance", _
                            Optional sLowPassFilter$ = "AUTO", _
                            Optional lRampPoints& = 100)
    
    Dim iStep As Integer
    Dim iScale As Integer
    
    frmMain.txtInstrument.Text = "ARB"
    frmMain.txtCommand.Text = UCase(sShape)
                          
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Error Check Load Impedance
    Select Case UCase(sLoadZ)
      Case "50", "50 OHM", "50-OHM"
        iScale = 1
        sLoadZ = "50"
      Case "75", "75 OHM", "75-OHM"
        iScale = 1
        sLoadZ = "75"
      Case "HIGH", "HI", "HIZ", "HI-Z", "HIIMP", "HI-IMP", "HIIMPEDANCE", "HI-IMPEDANCE", "INF", "INFINITE"
        iScale = 2
        sLoadZ = "HI-Z"
      Case Else
        Echo sLoadZ + " is not a valid output load impedance. " + vbCrLf + _
        "Select ""50"", ""75"" or ""Hi-Z"".  The default is ""50""."
        Err.Raise 1000, , _
        sLoadZ + " is not a valid output load impedance. " + vbCrLf + _
        "Select ""50"", ""75"" or ""Hi-Z"".  The default is ""50""."
        Exit Sub
    End Select
    
    'Error Check Output Impedance
    Select Case UCase(sOutputZ)
      Case "50", "50 OHM", "50-OHM"
        If sLoadZ = "75" Then
          'Instrument Manual Chapter 2 Page 70
          Echo "Output impedance cannot be 50 ohms if load impedance is 75 ohms." + vbCrLf + _
          "Select 75 ohms or infinite."
          Err.Raise 1000, , _
          "Output impedance cannot be 50 ohms if load impedance is 75 ohms." + vbCrLf + _
          "Select 75 ohms or infinite."
          Exit Sub
        Else
          sOutputZ = "50"
        End If
      Case "75", "75 OHM", "75-OHM"
        If sLoadZ = "50" Then
          'Instrument Manual Chapter 2 Page 70
          Echo "Output impedance cannot be 75 ohms if load impedance is 50 ohms." + vbCrLf + _
          "Select 50 ohms or infinite."
          Err.Raise 1000, , _
          "Output impedance cannot be 75 ohms if load impedance is 50 ohms." + vbCrLf + _
          "Select 50 ohms or infinite."
          Exit Sub
        Else
          sOutputZ = "75"
        End If
      Case Else
        Echo sOutputZ + " is not a valid output load impedance. " + vbCrLf + _
        "Select ""50"" or ""75"" The default is ""50""." + vbCrLf + _
        "If ""50"" or ""75"" has been selected for load impedance this parameter will have no effect."
        Err.Raise 1000, , _
        sOutputZ + " is not a valid output load impedance. " + vbCrLf + _
        "Select ""50"" or ""75"" The default is ""50""." + vbCrLf + _
        "If ""50"" or ""75"" has been selected for load impedance this parameter will have no effect."
        Exit Sub
    End Select
    
    'Generate waveform of sShape
    Select Case UCase(sShape)
      Case "SIN", "SINE"
        sShape = "Sine"
        iStep = FindVpkStep(dAmplitude, iScale)
        ParameterError = ErrorCheck(sShape, dAmplitude, dFrequency, sLowPassFilter, iStep, iScale, dDCOffset, lRampPoints)
        If ParameterError Then Exit Sub
        
        'Limit the resolution of the DC offset value
        dDCOffset = Fix(dDCOffset / (0.000499966 * iScale)) * 0.000499966 * iScale
        
        'Stop any previous waveform
        WriteMsg ARB, "*RST"
        WriteMsg ARB, "VOLT:UNIT:VOLT  V"
        WriteMsg ARB, "VOLT  1" 'Temporary Amplitude to satisfy limits while impdedance is set
        Select Case sLoadZ
          Case "50"
            WriteMsg ARB, "OUTP:IMP  50"
            WriteMsg ARB, "OUTP:LOAD  50"
          Case "75"
            WriteMsg ARB, "OUTP:IMP  75"
            WriteMsg ARB, "OUTP:LOAD  75"
          Case "HI-Z"
            WriteMsg ARB, "OUTP:IMP  " + sOutputZ
            WriteMsg ARB, "OUTP:LOAD  INF"
        End Select
        WriteMsg ARB, "VOLT  " + CStr(dVpkStepList(1, iStep))
        WriteMsg ARB, "FREQ " + CStr(dFrequency)
        If UCase(sLowPassFilter$) <> "OFF" Then
          WriteMsg ARB, "OUTP:FILT:LPAS:FREQ " + sLowPassFilter$
          WriteMsg ARB, "OUTP:FILT:LPAS:STAT ON"
        End If
        WriteMsg ARB, "FUNC  SIN"
        WriteMsg ARB, "VOLT:OFFS " + CStr(dDCOffset)
        PrecisionDelay 0.05
        WriteMsg ARB, "INIT"
      
      Case "TRI", "TRIANGLE", "SAW", "SAWTOOTH", "TRIANGULAR"
        sShape = "Triangular"
        iStep = FindVpkStep(dAmplitude, iScale)
        ParameterError = ErrorCheck(sShape, dAmplitude, dFrequency, sLowPassFilter, iStep, iScale, dDCOffset, lRampPoints)
        If ParameterError Then Exit Sub
        
        'Limit the resolution of the DC offset value
        dDCOffset = Fix(dDCOffset / (0.000499966 * iScale)) * 0.000499966 * iScale
                
        WriteMsg ARB, "*RST"
        WriteMsg ARB, "VOLT:UNIT:VOLT  V"
        WriteMsg ARB, "VOLT  1" 'Temporary Amplitude to satisfy limits while impdedance is set
        Select Case sLoadZ
          Case "50"
            WriteMsg ARB, "OUTP:IMP  50"
            WriteMsg ARB, "OUTP:LOAD  50"
          Case "75"
            WriteMsg ARB, "OUTP:IMP  75"
            WriteMsg ARB, "OUTP:LOAD  75"
          Case "HI-Z"
            WriteMsg ARB, "OUTP:IMP  " + sOutputZ
            WriteMsg ARB, "OUTP:LOAD  INF"
        End Select
        WriteMsg ARB, "VOLT  " + CStr(dVpkStepList(1, iStep))
        WriteMsg ARB, "FREQ:RANG " + CStr(dFrequency)
        WriteMsg ARB, "FREQ " + CStr(dFrequency)
        If UCase(sLowPassFilter$) <> "OFF" Then
          WriteMsg ARB, "OUTP:FILT:LPAS:FREQ " + sLowPassFilter$
          WriteMsg ARB, "OUTP:FILT:LPAS:STAT ON"
        End If
        WriteMsg ARB, "FUNC  TRI"
        WriteMsg ARB, "SOUR:RAMP:POIN " + CStr(lRampPoints)
        WriteMsg ARB, "VOLT:OFFS " + CStr(dDCOffset)
        WriteMsg ARB, "INIT"
                
      Case "SQU", "SQUARE", "SQU"
        sShape = "Square"
        iStep = FindVpkStep(dAmplitude, iScale)
        ParameterError = ErrorCheck(sShape, dAmplitude, dFrequency, sLowPassFilter, iStep, iScale, dDCOffset, lRampPoints)
        If ParameterError Then Exit Sub
        
        'Limit the resolution of the DC offset value
        dDCOffset = Fix(dDCOffset / (0.000499966 * iScale)) * 0.000499966 * iScale
        
        WriteMsg ARB, "*RST"
        WriteMsg ARB, "VOLT:UNIT:VOLT  V"
        WriteMsg ARB, "VOLT  1" 'Temporary Amplitude to satisfy limits while impdedance is set
        Select Case sLoadZ
          Case "50"
            WriteMsg ARB, "OUTP:IMP  50"
            WriteMsg ARB, "OUTP:LOAD  50"
          Case "75"
            WriteMsg ARB, "OUTP:IMP  75"
            WriteMsg ARB, "OUTP:LOAD  75"
          Case "HI-Z"
            WriteMsg ARB, "OUTP:IMP  " + sOutputZ
            WriteMsg ARB, "OUTP:LOAD  INF"
        End Select
        WriteMsg ARB, "VOLT  " + CStr(dVpkStepList(1, iStep))
        WriteMsg ARB, "FREQ:RANG " + CStr(dFrequency)
        WriteMsg ARB, "FREQ " + CStr(dFrequency)
        If UCase(sLowPassFilter$) <> "OFF" Then
          WriteMsg ARB, "OUTP:FILT:LPAS:FREQ " + sLowPassFilter$
          WriteMsg ARB, "OUTP:FILT:LPAS:STAT ON"
        End If
        WriteMsg ARB, "FUNC  SQU"
        WriteMsg ARB, "VOLT:OFFS " + CStr(dDCOffset)
        WriteMsg ARB, "INIT"
        
      Case "RAMP"
        sShape = "Ramp"
        iStep = FindVpkStep(dAmplitude, iScale)
        ParameterError = ErrorCheck(sShape, dAmplitude, dFrequency, sLowPassFilter, iStep, iScale, dDCOffset, lRampPoints)
        If ParameterError Then Exit Sub
        
        'Limit the resolution of the DC offset value
        dDCOffset = Fix(dDCOffset / (0.000499966 * iScale)) * 0.000499966 * iScale
        
        WriteMsg ARB, "*RST"
        WriteMsg ARB, "VOLT:UNIT:VOLT  V"
        WriteMsg ARB, "VOLT  1" 'Temporary Amplitude to satisfy limits while impdedance is set
        Select Case sLoadZ
          Case "50"
            WriteMsg ARB, "OUTP:IMP  50"
            WriteMsg ARB, "OUTP:LOAD  50"
          Case "75"
            WriteMsg ARB, "OUTP:IMP  75"
            WriteMsg ARB, "OUTP:LOAD  75"
          Case "HI-Z"
            WriteMsg ARB, "OUTP:IMP  " + sOutputZ
            WriteMsg ARB, "OUTP:LOAD  INF"
        End Select
        WriteMsg ARB, "VOLT  " + CStr(dVpkStepList(1, iStep))
        WriteMsg ARB, "FREQ:RANG " + CStr(dFrequency)
        WriteMsg ARB, "FREQ " + CStr(dFrequency)
        If UCase(sLowPassFilter$) <> "OFF" Then
          WriteMsg ARB, "OUTP:FILT:LPAS:FREQ " + sLowPassFilter$
          WriteMsg ARB, "OUTP:FILT:LPAS:STAT ON"
        End If
        WriteMsg ARB, "FUNC  RAMP"
        WriteMsg ARB, "SOUR:RAMP:POIN " + CStr(lRampPoints)
        WriteMsg ARB, "VOLT:OFFS " + CStr(dDCOffset)
        WriteMsg ARB, "INIT"
        
      Case "DC"
        'Error Check dAmplitude
        If dAmplitude > (DCMax * iScale) Or dAmplitude < (DCMin * iScale) Then
          Echo CStr(dAmplitude) + "Vdc is beyond the valid amplitude range for DC function." + vbCrLf + _
           CStr(DCMin * iScale) + "Vdc to " + CStr(DCMax * iScale) + "Vdc is the valid range."
          Err.Raise 1000, , _
           CStr(dAmplitude) + "Vdc is beyond the valid amplitude range for DC function." + vbCrLf + _
           CStr(DCMin * iScale) + "Vdc to " + CStr(DCMax * iScale) + "Vdc is the valid range."
          Exit Sub
        End If
        
        'Error Check DC Offset
        If dDCOffset > (5 * iScale) Or dDCOffset < (-5 * iScale) Then
          Echo "The DC offset specified is out of range for this amplitude setting." + vbCrLf + _
          "The acceptable range for DC Offset is: " + (-5 * iScale) + "Vdc to " + (5 * iScale) + " Vdc"
          Err.Raise 1000, , _
          "The DC offset specified is out of range for this amplitude setting." + vbCrLf + _
          "The acceptable range for DC Offset is: " + (-5 * iScale) + "Vdc to " + (5 * iScale) + " Vdc"
          Exit Sub
        End If
        
        WriteMsg ARB, "*RST"
        WriteMsg ARB, "FUNC  DC"
        Select Case sLoadZ
          Case "50"
            WriteMsg ARB, "OUTP:IMP  50"
            WriteMsg ARB, "OUTP:LOAD  50"
          Case "75"
            WriteMsg ARB, "OUTP:IMP  75"
            WriteMsg ARB, "OUTP:LOAD  75"
          Case "HI-Z"
            WriteMsg ARB, "OUTP:IMP  " + sOutputZ
            WriteMsg ARB, "OUTP:LOAD  INF"
        End Select
        WriteMsg ARB, "VOLT  " + CStr(dAmplitude)
        WriteMsg ARB, "VOLT:OFFS " + CStr(dDCOffset)
        
      Case Else
        Err.Raise 1000, , _
        sShape + " is not a recognized standard waveform shape supported by the ARB." + vbCrLf + _
        "Supported waveform shapes are Sine, Triangle, Ramp and Square."
        Echo sShape + " is not a recognized standard waveform shape supported by the ARB." + vbCrLf + _
        "Supported waveform shapes are Sine, Triangle, Ramp and Square."
    End Select

    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
End Sub

'*****************************************************************
'     Private Subs and Functions                                 *
'     Not visible to the interface                               *
'*****************************************************************

Private Function ErrorCheck(sShape$, _
                            dAmplitude#, _
                            dFrequency#, _
                            sLowPassFilter$, _
                            iStep%, _
                            iScale, _
                            dDCOffset#, _
                            lRampPoints As Long) As Boolean

    Dim FreqMax As Double
    Dim FreqMin As Double
    Dim DummyError As Long
    Dim lAvailableSegmentMemory As Long
    Dim sAvailableSegmentMemory As String
    
    ErrorCheck = False

    'Error Check Frequency
    Select Case UCase(sShape)
      Case "SINE"
        FreqMax = RoscInt1Freq / 4 'Frequency doubling not available for sine wave
        FreqMin = 0
        If dFrequency > SineFrequencyMax Or dFrequency < SineFrequencyMin Then
          Echo CStr(dFrequency) + "Hz is out of range for the " + sShape + " wave function." + vbCrLf + _
          "The available range is:  " + CStr(SineFrequencyMin) + " Hz to " + CStr(SineFrequencyMax) + " Hz."
          Err.Raise 1000, , _
           CStr(dFrequency) + "Hz is out of range for the " + sShape + " wave function." + vbCrLf + _
          "The available range is:  " + CStr(SineFrequencyMin) + " Hz to " + CStr(SineFrequencyMax) + " Hz."
          ErrorCheck = True
        End If
      Case "SQUARE"
        FreqMax = RoscInt1Freq / 8  'Allow for Frequency doubling
        FreqMin = 0
        If dFrequency > FreqMax Or dFrequency < FreqMin Then
          Echo CStr(dFrequency) + "Hz is out of range for the " + sShape + " wave function." + vbCrLf + _
          "The available range is:  " + CStr(FreqMin) + " Hz to " + CStr(FreqMax) + " Hz."
          Err.Raise 1000, , _
           CStr(dFrequency) + "Hz is out of range for the " + sShape + " wave function." + vbCrLf + _
          "The available range is:  " + CStr(FreqMin) + " Hz to " + CStr(FreqMax) + " Hz."
          ErrorCheck = True
        End If
      Case "TRIANGULAR", "RAMP"
        FreqMax = RoscInt1Freq / 2 / lRampPoints ' Allow for automatic doubled frequency selection
        FreqMin = 0
        If dFrequency > FreqMax Or dFrequency < FreqMin Then
          Echo CStr(dFrequency) + "Hz is out of range for the " + sShape + " wave function." + vbCrLf + _
          "The available range is:  " + CStr(FreqMin) + " Hz to " + CStr(FreqMax) + " Hz."
          Err.Raise 1000, , _
           CStr(dFrequency) + "Hz is out of range for the " + sShape + " wave function." + vbCrLf + _
          "The available range is:  " + CStr(FreqMin) + " Hz to " + CStr(FreqMax) + " Hz."
          ErrorCheck = True
        End If
      Case "DC"
        
    End Select

    'Error Check Amplitude
    If dAmplitude > (DACFullScaleMax * iScale) Or dAmplitude < (DACFullScaleMin * iScale) Then
      Echo CStr(dAmplitude) + "Vdc is beyond the valid amplitude range." + vbCrLf + _
       CStr(DACFullScaleMin * iScale) + "Vdc to " + CStr(DACFullScaleMax * iScale) + "Vdc is the valid range."
      Err.Raise 1000, , _
       CStr(dAmplitude) + "Vdc is beyond the valid amplitude range." + vbCrLf + _
       CStr(DACFullScaleMin * iScale) + "Vdc to " + CStr(DACFullScaleMax * iScale) + "Vdc is the valid range."
      ErrorCheck = True
    End If
    
    Select Case UCase(sLowPassFilter)
      Case "OFF"
      Case "DEF", "DEFAULT", "AUTO", "AUTOMATIC"
        Select Case UCase(sShape)
          Case "TRIANGULAR", "RAMP"
            sLowPassFilter = "250 KHz"
          Case Else
            sLowPassFilter = "10 MHz"
        End Select
      Case "250 KHZ", "250KHZ", "250E3", "250E+3"
        sLowPassFilter = "250 KHz"
      Case "10 MHZ", "10MHZ", "10E6", "10E+6"
        sLowPassFilter = "10 MHz"
      Case Else
        Echo "The specified output low pass filter parameter is not supported: " + sLowPassFilter + vbCrLf + _
        "Available low pass filter settings are ""Off"", ""250 KHz"", and ""10 MHz""."
        Err.Raise 1000, , _
        "The specified output low pass filter parameter is not supported: " + sLowPassFilter + vbCrLf + _
        "Available low pass filter settings are ""Off"", ""250 KHz"", and ""10 MHz""."
        ErrorCheck = True
    End Select
  
    'Error Check DC Offset
    If dDCOffset > dVpkStepList(2, iStep) Or dDCOffset < -dVpkStepList(2, iStep) Then
      Echo "The DC offset specified is out of range for this amplitude setting." + vbCrLf + _
      "The acceptable range for DC Offset is: " + CStr(-dVpkStepList(2, iStep)) + "Vdc to " + CStr(dVpkStepList(2, iStep)) + " Vdc"
      Err.Raise 1000, , _
      "The DC offset specified is out of range for this amplitude setting." + vbCrLf + _
      "The acceptable range for DC Offset is: " + CStr(-dVpkStepList(2, iStep)) + "Vdc to " + CStr(dVpkStepList(2, iStep)) + " Vdc"
      ErrorCheck = True
    End If
    
    If Not bSimulation Then
      WriteMsg ARB, "LIST:SEGM:FREE?"
      DummyError = ReadMsg(ARB, sAvailableSegmentMemory)
      lAvailableSegmentMemory = CLng(Left(sAvailableSegmentMemory, (InStr(1, sAvailableSegmentMemory, ",") - 1)))
    Else
      lAvailableSegmentMemory = 262144
    End If
    
    'Error Check Ramp Points
    Select Case UCase(sShape)
      Case "TRIANGULAR", "RAMP"
        If lRampPoints > lAvailableSegmentMemory Or lRampPoints < 4 Then
          Echo CStr(lRampPoints) + " is not a valid number of ramp points." + vbCrLf + _
          "The available range is:  4 to " + CStr(lAvailableSegmentMemory) + "."
          Err.Raise 1000, , _
           CStr(lRampPoints) + " is not a valid number of ramp points." + vbCrLf + _
          "The available range is:  4 to " + CStr(lAvailableSegmentMemory) + "."
        End If
      Case "DC"
    End Select
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function

End Function

Private Sub CreateVpkStepList(iScale As Integer)
    
    Dim dCurrentStepAttenuation As Double
    Dim iStep As Integer
    
    'Necesary to Do only once.
    'Fill a table with exact Amplitude Steps on the linear scale for a matched output.
      
    ReDim dVpkStepList(1 To 2, 0 To 3000) As Double
    For iStep = 0 To 3000
      dCurrentStepAttenuation = iStep * 0.01
      dVpkStepList(1, iStep) = InverseLog10((20 * Log10(DACFullScaleMax * iScale) - dCurrentStepAttenuation) / 20)
      dVpkStepList(2, iStep) = -1 'Symbolize empty - only calculate limit for needed step
    Next iStep
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub

End Sub

Private Function FindVpkStep(dAmplitude As Double, iScale As Integer)

  'Will round down to the nearest step

  Dim dCurrentStepAttenuation As Double
  Dim iStep As Integer
  Dim iUBound As Integer
  Dim iLBound As Integer
  ReDim dVpkStepList(1 To 2, 0 To 3000) As Double
  
  iUBound = 3000
  iLBound = 0

  Do
    
    iStep = (iUBound - iLBound) \ 2 + iLBound
    dCurrentStepAttenuation = iStep * 0.01
    dVpkStepList(1, iStep) = InverseLog10((20 * Log10(DACFullScaleMax * iScale) - dCurrentStepAttenuation) / 20)
    If iStep = iUBound Or iStep = iLBound Then
      Exit Do
    End If
  
    If dVpkStepList(1, iStep) < dAmplitude# Then
      iUBound = iStep
    Else
      iLBound = iStep
    End If
  
  Loop
  
  'At this point iLBound and iUbound are at a difference of 1
  'iLBound will be selected if dAmplitude# is greater than the maximum allowable
  'iUBound will be selected if dAmplitude is between the two points or
  'if it is less than the minimum allowable
  If dAmplitude# >= dVpkStepList(1, iLBound) Then
    iStep = iLBound
  Else
    iStep = iUBound
  End If
    
  dVpkStepList(2, iStep) = -1
  'Now calculate the maximum DC Offset for the selected amplitude
  If dVpkStepList(2, iStep) = -1 Then
    If dAmplitude > 1.02426 * iScale Then
        'High Range - Sum must be less than 6.025 - DC Offset is multiple of 2.5 mV
        dVpkStepList(2, iStep) = Fix((6.025 * iScale - dVpkStepList(1, iStep)) / 0.0025 * iScale) * 0.0025 * iScale
        If dVpkStepList(2, iStep) > (5 * iScale) Then
          dVpkStepList(2, iStep) = (5 * iScale)
        End If
    Else
        'Low Range - Sum must be less than 6.025 - DC Offset is multiple of .499966 mV
        dVpkStepList(2, iStep) = Fix((1.205 * iScale - dVpkStepList(1, iStep)) / 0.000499966 * iScale) * 0.000499966 * iScale
        If dVpkStepList(2, iStep) > (0.999932 * iScale) Then
          dVpkStepList(2, iStep) = (0.999932 * iScale)
        End If
    End If
        
  End If

  FindVpkStep = iStep

End Function

Private Function Log10(dLinearNumber As Double) As Double
   
    Log10 = Log(dLinearNumber) / Log(10#)
   
End Function

Private Function InverseLog10(dLogarithmicBase10Number As Double) As Double

    InverseLog10 = Exp(Log(10) * dLogarithmicBase10Number)
    
End Function

Private Sub PrecisionDelay(seconds As Double)

'This procedure is dependent upon system performance
'The Timer! object is updated every 55 mS
'If the system performance permits excecution time of this loop
'to be 55 mS or less then this will be the resolution of this Delay
'and the additional error will be at least a 10th order smaller
'but is also of course dependent on system performance
'A better Time Measuring scheme could be written using windows API
'function timeGetTime which has a resolution of 1 to 5 mS

    Dim StartTime As Double
    
    StartTime = Timer
    While Timer < (StartTime + seconds)
        If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    Wend

End Sub

Public Sub DownloadSegmentFile(SegmentName As String, _
                                fileName As String, _
                                Optional sOutputZ As String = "50", _
                                Optional sLoadZ As String = "Hi-Impedance", _
                                Optional dAmplitude As Double = -1)

  Dim FileId As Integer, NumBytes&, FileSize&, MaxFileSize&
  Dim TyxMagicNumber As Long
  Dim TmpStr$, i%
  Dim Char As Byte
  Dim List(1 To 10) As String
  Dim txtUserPoints As String
  Dim Wave() As Double
  Dim UserEnteringData As Integer
  
  FileSize& = FileLen(fileName)
  MaxFileSize& = (8 * 262144) + 511 '256 Byte Header Block plus up to 255 filler bytes Added Version 1.2
  If FileSize& > MaxFileSize& Then
    'Filesize error
    MsgBox "File " & fileName & " is too big." & "Cannot be more than " & Format(MaxFileSize& + 511) & " bytes.", vbCritical
    Exit Sub
  End If
  If FileSize& Mod (8) <> 0 Or FileSize& < 512 Then
    'Filesize error
    MsgBox "File " & fileName & " is not the correct size.", vbCritical
    Exit Sub
  End If
  
  '---------------Version 1.2 Modification By DHartley---------------
  '[Re]Format Wave for TETS-ATLAS Compatability (256 Byte File Blocks)
  FileId = FreeFile
  Open fileName For Binary Access Read As FileId
  
  'Added for V1.3 JHill
  Get #FileId, 1, TyxMagicNumber&
  If TyxMagicNumber& <> &HABCD4567 Then
    'Tyx Magic Number Error
    MsgBox "File " & fileName & " does not have the correct format.", vbCritical
    Close FileId
    Exit Sub
  End If
  
  Get #FileId, 5, NumBytes&
  SetCur$(LIST_DEF) = Format(NumBytes& / 8)
  SetRes$(LIST_DEF) = "N0"
  txtUserPoints = EngNotate$(SetCur$(LIST_DEF), LIST_DEF)
  SizeArray Wave, SetCur$(LIST_DEF)
  
  'If a user-clicked Load, then do this, else its a Configuration load, so don't.
  'Dig out the embedded Trace Preamble data
  For i = 9 To 256
    Get #FileId, i, Char
    If Char > 0 Then
      TmpStr$ = TmpStr$ & Chr$(Char)
    Else
      Exit For
    End If
  Next i
    
  'If found an IEEE Definite Length Block...
  If Left$(TmpStr$, 1) = "#" Then
    TmpStr$ = sIEEEDefinite("From", TmpStr$)
    'If correct number of arguments, process Preamble data, else don't
    If StringToList(TmpStr$, List$(), ",") = 10 Then
      Trace.Format = List$(1) '?
      Trace.Type = List$(2)   '?
      Trace.Points = List$(3)
      Trace.count = List$(4)  '?
      Trace.XInc = List$(5)
      Trace.XOrig = List$(6)
      Trace.XRef = List$(7)   '?
      Trace.YInc = List$(8)
      Trace.YOrig = List$(9)
      Trace.YRef = List$(10)  '?
      
      'Set VOLT (Units is always V - equiv to Vpk)
      txtAmpl = Round(CStr(Trace.YInc * Trace.YRef), 5)
      UserEnteringData% = True
      
      'Set VOLT_OFFS
      txtDcOffs = CStr(Trace.YOrig)
      UserEnteringData% = True
      
    Else
      'Has been an error reading the preamble of fileName
    End If
  Else
    'Has been an error reading the preamble of fileName
  End If
  
  'Read the segment voltage points
  Get #FileId, 257, Wave()
  Close FileId
  
  DownloadSegmentArray SegmentName, Wave(), sOutputZ, sLoadZ, dAmplitude

End Sub

Public Sub DownloadSegmentArray(SegmentName As String, _
                           PointList() As Double, _
                           Optional sOutputZ As String = "50", _
                           Optional sLoadZ As String = "Hi-Impedance", _
                           Optional dAmplitude As Double = -1)
   
'**********************************************************
    'Sub DownloadSegmentArray()
    '--------------------------------
    ' Downloads a segment of data points to the instrument's segment
    ' memory.  If dAmplitude is not specified then the DAC ful scale
    ' will be adjusted to the highest peak value of the data in
    ' PointList().  This will cause the data to be distributed
    ' fully across the dynamic range of the DAC.
    ' If an dAmplitude is specified the DAC full scale will be set
    ' to dAmplitude and the data will be distributed accordingly.
    ' If specified, dAmplitude must be greater than or equal to the
    ' highest peak value in the array.
    '
    '
    'Parameters:
    '--------------------------------
    ' SegmentName - Name of the segment to be created and stored.
    '
    ' PointList - List of voltage points to be placed in SegmentName
    '             in units of V (Vpk).
    '
    ' sOutputZ - Intended output impedance of the instrument.
    '
    ' sLoadZ - Intended load impedance of the UUT.
    '
    ' dAmplitude - Intended full scale setting of the DAC.  Specify
    '              the same dAmplitude for ExecuteSequence.
    '
    'Notes:
    '--------------------------------
    ' DownloadSegmentArray first programs the impedance settings of the
    ' instrument so that the DAC codes and DAC full scale will be
    ' equal given the expected impedance setup.  The
    ' soft impedance setup of the instrument will affect the
    ' conversion of voltage points to DAC codes at the time of
    ' segment download.  It is suggested that the same impedance
    ' setup is used that will be used when the segment is executed.
    '
    ' If no dAmplitude is specified:
    ' Any segments loaded with this procedure are scaled amplitudewise
    ' so that the points occupy the the full dynamic range of the DAC
    ' at least on one side of zero.  However if the waveform is vertically
    ' unsymetrical as in the positive peak is a different magnitude than the
    ' negative peak than the amplitude will be set to the highest of the two.
    ' There may be a section of the DAC dynamic range that is unused.
    '
    ' If dAmplitude is specified
    ' It is recomended to specify dAmplitude if the segment is to be one
    ' grouped with other segments in a sequence.  It is recomended that the
    ' same dAmplitude be specified for each call of DownLoadSegment Array
    ' and also for ExecuteSequence.
'**********************************************************
  
    Dim iScale As Integer
    Dim sPointList As String
    Dim lPointCount As Long
    Dim lMaxIndex As Long
    Dim lMinIndex As Long
    Dim lCurrentIndex As Long
    Dim lHighestIndex As Long
    Dim dMaxPeak As Double
    Dim nStatus As Long
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    If Len(SegmentName) > 12 Then
      Echo "The segment name cannot be more than 12 characters."
      Err.Raise 1000, , _
      "The segment name cannot be more than 12 characters."
      Exit Sub
    ElseIf IsNumeric(Left$(SegmentName, 1)) Then
      Echo "The segment name cannot start with a number."
      Err.Raise 1000, , _
      "The segment name cannot start with a number."
      Exit Sub
    End If
    
    'Error Check Load Impedance
    Select Case UCase(sLoadZ)
      Case "50", "50 OHM", "50-OHM"
        iScale = 1
        sLoadZ = "50"
      Case "75", "75 OHM", "75-OHM"
        iScale = 1
        sLoadZ = "75"
      Case "HIGH", "HI", "HIZ", "HI-Z", "HIIMP", "HI-IMP", "HIIMPEDANCE", "HI-IMPEDANCE", "INF", "INFINITE"
        iScale = 2
        sLoadZ = "HI-Z"
      Case Else
        Echo sLoadZ + " is not a valid output load impedance. " + vbCrLf + _
        "Select ""50"", ""75"" or ""Hi-Z"".  The default is ""50""."
        Err.Raise 1000, , _
        sLoadZ + " is not a valid output load impedance. " + vbCrLf + _
        "Select ""50"", ""75"" or ""Hi-Z"".  The default is ""50""."
        Exit Sub
    End Select
    
    'Error Check Output Impedance
    Select Case UCase(sOutputZ)
      Case "50", "50 OHM", "50-OHM"
        If sLoadZ = "75" Then
          'Instrument Manual Chapter 2 Page 70
          Echo "Output impedance cannot be 50 ohms if load impedance is 75 ohms." + vbCrLf + _
          "Select 75 ohms or infinite."
          Err.Raise 1000, , _
          "Output impedance cannot be 50 ohms if load impedance is 75 ohms." + vbCrLf + _
          "Select 75 ohms or infinite."
          Exit Sub
        Else
          sOutputZ = "50"
        End If
      Case "75", "75 OHM", "75-OHM"
        If sLoadZ = "50" Then
          'Instrument Manual Chapter 2 Page 70
          Echo "Output impedance cannot be 75 ohms if load impedance is 50 ohms." + vbCrLf + _
          "Select 50 ohms or infinite."
          Err.Raise 1000, , _
          "Output impedance cannot be 75 ohms if load impedance is 50 ohms." + vbCrLf + _
          "Select 50 ohms or infinite."
          Exit Sub
        Else
          sOutputZ = "75"
        End If
      Case Else
        Echo sOutputZ + " is not a valid output load impedance. " + vbCrLf + _
        "Select ""50"" or ""75"" The default is ""50""." + vbCrLf + _
        "If ""50"" or ""75"" has been selected for load impedance this parameter will have no effect."
        Err.Raise 1000, , _
        sOutputZ + " is not a valid output load impedance. " + vbCrLf + _
        "Select ""50"" or ""75"" The default is ""50""." + vbCrLf + _
        "If ""50"" or ""75"" has been selected for load impedance this parameter will have no effect."
        Exit Sub
    End Select
  
    If dAmplitude <> -1 Then
      'Error Check dAmplitude
      If dAmplitude > iScale * DACFullScaleMax _
      Or dAmplitude < iScale * DACFullScaleMin Then
        Echo "Invalid setting for dAmplitude." + vbCrLf + _
        "The acceptable range is: " + CStr(iScale * DACFullScaleMin) + "V to " + CStr(iScale * DACFullScaleMax) + " V"
        Err.Raise 1000, , _
        "Invalid setting for dAmplitude." + vbCrLf + _
        "The acceptable range is: " + CStr(iScale * DACFullScaleMin) + "V to " + CStr(iScale * DACFullScaleMax) + " V"
        Exit Sub
      End If
    End If
  
    'StopAll
    lMinIndex = LBound(PointList, 1)
    lMaxIndex = UBound(PointList, 1)
    lPointCount = lMaxIndex - lMinIndex + 1

    lHighestIndex = lMinIndex
    sPointList = ""
    lCurrentIndex = lMinIndex
    
    sPointList = CStr(PointList(lCurrentIndex))
    lCurrentIndex = lCurrentIndex + 1
    While lCurrentIndex <= lMaxIndex
      'Keep track of highest point for optimal amplitude (attenuation) setting
      'Optimal use of the DAC Dynamic Range
      If Abs(PointList(lCurrentIndex)) > Abs(PointList(lHighestIndex)) Then
        lHighestIndex = lCurrentIndex
      End If
      sPointList = sPointList + "," + CStr(PointList(lCurrentIndex))
      lCurrentIndex = lCurrentIndex + 1
    Wend
    
    'Error Check PointList
    If PointList(lHighestIndex) > iScale * DCMax _
    Or PointList(lHighestIndex) < iScale * DCMin Then
    Echo "The " + CStr(lHighestIndex + 1 - lMinIndex) + "th data point is out of range for this impedance configuration." + vbCrLf + _
        "The acceptable range is: " + CStr(iScale * DCMin) + "V to " + CStr(iScale * DCMax) + " V"
        Err.Raise 1000, , _
        "The " + CStr(lHighestIndex + 1 - lMinIndex) + "th data point is out of range for this impedance configuration." + vbCrLf + _
        "The acceptable range is: " + CStr(iScale * DCMin) + "V to " + CStr(iScale * DCMax) + " V"
        Exit Sub
    End If
    
    'Extract the highest magnitude point form PointList
    dMaxPeak = PointList(lHighestIndex)
    
    'Massage dMaxPeak to fit within valid limits
    If dMaxPeak > DACFullScaleMax * iScale Then
      dMaxPeak = DACFullScaleMax * iScale
    End If
    If dMaxPeak < DACFullScaleMin * iScale Then
      dMaxPeak = DACFullScaleMin * iScale
    End If
    
    If Not bSimulation Then
      
      WriteMsg ARB, "*RST"
      If dAmplitude = -1 Then
        WriteMsg ARB, "VOLT:LEV:IMM:AMPL " + CStr(dMaxPeak)
      Else
        WriteMsg ARB, "VOLT:LEV:IMM:AMPL " + CStr(dAmplitude)
      End If
      'Sync Instrument With Desired Impedance Charateristics
      Select Case sLoadZ
        Case "50"
          WriteMsg ARB, "OUTP:IMP  50"
          WriteMsg ARB, "OUTP:LOAD  50"
        Case "75"
          WriteMsg ARB, "OUTP:IMP  75"
          WriteMsg ARB, "OUTP:LOAD  75"
        Case "HI-Z"
          WriteMsg ARB, "OUTP:IMP  " + sOutputZ
          WriteMsg ARB, "OUTP:LOAD  INF"
      End Select
      
      WriteMsg ARB, "LIST:SEGM:SEL " + SegmentName
      WriteMsg ARB, "LIST:SEGM:DEF " + CStr(lPointCount)
      If lPointCount > 3000 Then
          nStatus = atxmlDF_viSetAttribute(ResourceName(ARB), nInstrumentHandle(ARB), VI_ATTR_TMO_VALUE, ((lPointCount / 300) * 1000))
      Else
          nStatus = atxmlDF_viSetAttribute(ResourceName(ARB), nInstrumentHandle(ARB), VI_ATTR_TMO_VALUE, 10000) '10 Sec
      End If
      PrecisionDelay 0.05
      WriteMsg ARB, "LIST:SEGM:VOLT " + sPointList
      
      nStatus = atxmlDF_viSetAttribute(ResourceName(ARB), nInstrumentHandle(ARB), VI_ATTR_TMO_VALUE, 10000) '10 Sec
      
    End If

End Sub

Public Sub DownloadSegmentArrayCicl(SegmentName As String, _
                           PointList() As Double, _
                           Optional sOutputZ As String = "50", _
                           Optional sLoadZ As String = "Hi-Impedance", _
                           Optional dAmplitude As Double = -1)
   
    DownloadSegmentArray SegmentName, PointList(), sOutputZ, sLoadZ, dAmplitude

End Sub

Public Sub DownloadSequence(SequenceName As String, _
                            ByRef SegmentList() As String, _
                            ByRef DwellCounts() As Long)
                            
'**********************************************************
    'Sub DownloadSequence()
    '--------------------------------
    ' Downloads a sequence of segments to the instrument's sequence
    ' memory for later execution.
    '
    'Parameters:
    '--------------------------------
    ' SequenceName - Name of the sequence to be created and stored.
    '
    ' SegmentList - List of segment names to be placed in SequenceName
    '               in respective order.
    '
    ' DwellCounts - List of segment dwell counts in respective order.
    '
    'Notes:
    '--------------------------------
    ' The segment names in SegmentList must allready exist in the
    ' instruments segment memory or an error will occur.
    '
    ' DwellCounts may be a (1x1) array containing [1] if the dwell
    ' counts are to all equal 1.
    '
    'Any segments loaded with this class module are scaled amplitudewise
    'so that the points occupy the the full dynamic range of the DAC
    'at least on one side of zero.  However if the waveform is vertically
    'unsymetrical as in the MAX is a different magnitude than the MIN
    'there may be a section of the DAC dynamic range that is unused.
    'This could be compensated for by storing the waveform points with a
    'DC offset and then cancelling this offset out with an equal and
    'opposite DC Offset setting of the instrument.
'**********************************************************
    
    Dim iScale As Integer
    Dim strSegmentList As String
    Dim strDwellCounts As String
    Dim lPointCount As Long
    Dim lMaxIndex As Long
    Dim lMinIndex As Long
    Dim lPointCountDwell As Long
    Dim lMaxIndexDwell As Long
    Dim lMinIndexDwell As Long
    Dim lCurrentIndex As Long
    Dim SequenceList As String
    Dim DummyError As Long

    WriteMsg ARB, "LIST:SSEQ:CAT?"
    DummyError = ReadMsg(ARB, SequenceList)

    If Len(SequenceName) > 12 Then
      Echo "The sequence name cannot be more than 12 characters."
      Err.Raise 1000, , _
      "The sequence name cannot be more than 12 characters."
      Exit Sub
    ElseIf IsNumeric(Left$(SequenceName, 1)) Then
      Echo "The sequence name cannot start with a number."
      Err.Raise 1000, , _
      "The sequence name cannot start with a number."
      Exit Sub
    End If

    If InStr(1, SequenceList, SequenceName) Then
      Echo SequenceName + " is all ready an existing sequence in the instrument memory."
      Err.Raise 1000, , _
      SequenceName + " is all ready an existing sequence in the instrument memory."
      Exit Sub
    Else
      'Good
    End If

    lMinIndex = LBound(SegmentList)
    lMaxIndex = UBound(SegmentList)
    lPointCount = lMaxIndex - lMinIndex + 1

    lMinIndexDwell = LBound(DwellCounts)
    lMaxIndexDwell = UBound(DwellCounts)
    lPointCountDwell = lMaxIndexDwell - lMinIndexDwell + 1

    If ((lMaxIndexDwell - lMinIndexDwell + 1) <> lPointCount) And ((lMaxIndexDwell - lMinIndexDwell + 1) <> 1) Then
      Echo "There are an invalid number of dwell counts specified." + vbCrLf + _
      "There must be the same number of Dwell counts as there are Segments or there must be exactly one dwell count."
      Err.Raise 1000, , _
      "There are an invalid number of dwell counts specified." + vbCrLf + _
      "There must be the same number of Dwell counts as there are Segments or there must be exactly one dwell count."
      Exit Sub
    End If

    'Create Segment List SCPI argument
    strSegmentList = ""
    For lCurrentIndex = lMinIndex To lMaxIndex

      If lCurrentIndex > lMinIndex Then
        strSegmentList = strSegmentList + ","
      End If
      strSegmentList = strSegmentList + SegmentList(lCurrentIndex)

    Next lCurrentIndex

    'Create Dwell Count List SCPI argument
    strDwellCounts = ""
    For lCurrentIndex = lMinIndexDwell To lMaxIndexDwell
      If lCurrentIndex > lMinIndexDwell Then
        strDwellCounts = strDwellCounts + ","
      End If
      If lPointCountDwell > 1 Then
        strDwellCounts = strDwellCounts + CStr(DwellCounts(lCurrentIndex))
      Else
        strDwellCounts = strDwellCounts + CStr(DwellCounts(lMinIndexDwell))
      End If
    Next lCurrentIndex

    If Not bSimulation Then
      WriteMsg ARB, "*RST"
      WriteMsg ARB, "LIST:SSEQ:SEL " + SequenceName
      WriteMsg ARB, "LIST:SSEQ:DEF " + CStr(lPointCount)
      WriteMsg ARB, "LIST:SSEQ:SEQ " + strSegmentList
      WriteMsg ARB, "LIST:SSEQ:DWELl:COUN " + strDwellCounts
    End If
                           
End Sub

Public Sub ExecuteSequence(SequenceName As String, _
                           dAmplitude As Double, _
                           SampleRate As Double)

'**********************************************************
    'Sub ExecuteSequence()
    '--------------------------------
    'Executes a sequence previously stored in memory
    '
    'Parameters:
    '--------------------------------
    ' SequenceName - Name of the prviously stored sequence to be executed.
    '
    ' SampleRate - Frequency at which the individual points of segments in
    '              SequenceName are to be output.
    '
    'Notes:
    '--------------------------------
    'The sample frequency and amplitude should be appropriately set while
    'The segment file is being created using the SAI wave tool.
    '
'**********************************************************

    Dim SequenceList As String
    Dim DummyError As Long
    Dim FreqMax As Double
    Dim FreqMin As Double

    WriteMsg ARB, "LIST:SSEQ:CAT?"
    DummyError = ReadMsg(ARB, SequenceList)
    
    FreqMax = RoscInt1Freq / 2 ' Allow for frequency doubling
    FreqMin = 0
    If SampleRate > FreqMax Or SampleRate < FreqMin Then
      Echo CStr(SampleRate) + "Hz is out of range for the arbitrary wave function." + vbCrLf + _
      "The available range is:  " + CStr(FreqMin) + " Hz to " + CStr(FreqMax) + " Hz."
      Err.Raise 1000, , _
       CStr(SampleRate) + "Hz is out of range for the arbitrary wave function." + vbCrLf + _
      "The available range is:  " + CStr(FreqMin) + " Hz to " + CStr(FreqMax) + " Hz."
      Exit Sub
    End If
    
    If InStr(1, SequenceList, SequenceName) Then
      'Good
    Else
      Echo SequenceName + "Has not been stored in the instrument's sequence memory." + vbCrLf + _
      "Sequences currently stored are " + SequenceList + "."
      Err.Raise 1000, , _
      SequenceName + "Has not been stored in the instrument's sequence memory." + vbCrLf + _
      "Sequences currently stored are " + SequenceList + "."
      Exit Sub
    End If
    
    If Not bSimulation Then
      StopAll
      WriteMsg ARB, "VOLT:UNIT:VOLT  V"
      WriteMsg ARB, "VOLT  " + CStr(dAmplitude)
      WriteMsg ARB, "ROSC:SOUR INT1"
      WriteMsg ARB, "FREQ:RANG " + CStr(SampleRate)
      WriteMsg ARB, "FREQ:FIX " + CStr(SampleRate)
      WriteMsg ARB, "FUNC:SHAP USER"
      WriteMsg ARB, "FUNC:USER " + SequenceName
      WriteMsg ARB, "INIT:IMM"
    End If
    
End Sub

Public Sub ClearAllSegments()
    
    If Not bSimulation Then
      StopAll
      WriteMsg ARB, "LIST:DEL:ALL"
    End If

End Sub

Public Sub ClearAllSequences()

    If Not bSimulation Then
      StopAll
      PrecisionDelay 0.05
      WriteMsg ARB, "LIST:SSEQ:DEL:ALL"
    End If
    
End Sub

Private Function EngNotate(ByVal Number As Double, SetIdx As Integer) As String
    'DESCRIPTION:
    '   Returns passed number as numeric string in Engineering notation (every
    '   3rd exponent) with selectable precision along with Unit Of Measure.
    'EXAMPLES:
    '   With SetUOM$(SetIdx%)="Ohm" and SetRes$(SetIdx%)="D3",
    '       EngNotate$(10987.1, MEAS_RES) returns "10.987 KOhm"
    '   With SetUOM$(SetIdx%)="Sec" and SetRes$(SetIdx%)="D2",
    '       EngNotate$(5.43e-5, MEAS_PER) returns "54.30 uSec"
    'PARAMETERS:
    '   Number#:    The number to be formatted
    '   SetIdx%:    The index to a set of parallel global arrays (see below)
    'GLOBAL VARIABLES USED:
    '   SetRes$():  SETtings RESolution array:
    '               "A0" = Absolute integer resolution
    '               "An" where 'n' is digits of displayed resolution
    '               "D0" = integer resolution
    '               "Dn" where 'n' is digits of displayed resolution
    '               "N0" = integer resolution with no engineering notation
    '               "Nn" where 'n' is digits of displayed resolution
    '       Note: Use "An" when resolution is specified as an absolute value.
    '             Use "Dn" when resolution is specified as "digits".
    '   SetUOM$():  SETtings Unit-Of-Measure array:
    '               "V", "Vp-p", "Vpk", "A", "Hz", "S",
    '               "%", "% Mod", "Deg", or ""
    'REQUIRES:  None
    'VERSION:   3/14/97
    'AUTHOR: J. Hill
    
    Dim Multiplier%, Negative%, Digits%
    Dim Prefix$, ReturnString$, NumDigits%, FormatSpec$
    
    Multiplier% = 0: Negative% = False  'Initialize local variables
    
    If Number < 0 Then      ' If negative
        Number = Abs(Number)    'Make it positive for now
        Negative% = True        'Set flag
    End If
    
    If Number >= 1000 Then                    'For positive exponent
        Do While Number >= 1000 And Multiplier <= 4
            Number = Number / 1000
            Multiplier% = Multiplier% + 1
        Loop
    ElseIf Number < 1 And Number <> 0 Then    'For negative exponent (but not 0)
        Do While Number < 1 And Multiplier >= -4
            Number = Number * 1000
            Multiplier% = Multiplier% - 1
        Loop
    End If
    
    Select Case Multiplier%
        Case 4:  Prefix$ = " T"     'Tetra  E+12
        Case 3:  Prefix$ = " G"     'Giga   E+09
        Case 2:  Prefix$ = " M"     'Mega   E+06
        Case 1:  Prefix$ = " K"     'kilo   E+03
        Case 0:  Prefix$ = " "      '<none> E+00
        Case -1: Prefix$ = " m"     'milli  E-03
        Case -2: Prefix$ = " " & Chr$(181)     'micro  E-06
        Case -3: Prefix$ = " n"     'nano   E-09
        Case -4: Prefix$ = " p"     'pico   E-12
        Case Else:
            Prefix$ = " "
    End Select
    
    If Negative% Then Number = -Number
    
    If (Multiplier < -4) Or (Multiplier > 4) Then
        ReturnString$ = Format(Number)
    Else
        Number = Val(Str$(Number))      ' Clear out very low LSBs from binary math
        If Left$(SetRes$(SetIdx%), 1) = "N" Then
            Digits% = Val(Mid$(SetRes$(SetIdx%), 2, 1))
            Prefix$ = " "
            Number = Number * 10 ^ (Multiplier * 3)
        ElseIf Left$(SetRes$(SetIdx%), 1) = "D" Then        'Digits of resolution
            Digits% = Val(Mid$(SetRes$(SetIdx%), 2, 1)) - Len(Format(Int(Abs(Number))))
            If Digits% < 0 Then Digits% = 0
        ElseIf Left$(SetRes$(SetIdx%), 1) = "A" Then    'Absolute Digits of resolution
            Digits% = Val(Mid$(SetRes$(SetIdx%), 2, 1)) + (Multiplier * 3)
            If Digits% < 0 Then Digits% = 0
        Else
            MsgBox "EngNotate detected unspecified resolution for parameter: " & SetRes$(SetIdx%) & ". Notify maintenance.", vbExclamation
            Digits = -1
        End If
        
        FormatSpec$ = "#,###,###,###,###,##0"
        If Digits > 0 Then
            FormatSpec$ = FormatSpec$ & "."
        ElseIf Digits < 0 Then
            FormatSpec$ = "0.0#######"
        End If
        
        For NumDigits% = 1 To Digits
            FormatSpec$ = FormatSpec$ & "0"
        Next NumDigits%
        
        ReturnString$ = Format(Number, FormatSpec$)
    End If
    
    'Trim trailing zeros after decimal
    If InStr(ReturnString$, ".") Then
        While Right$(ReturnString$, 1) = "0"
            ReturnString$ = Left$(ReturnString$, Len(ReturnString$) - 1)
        Wend
        If Right$(ReturnString$, 1) = "." Then
            ReturnString$ = Left$(ReturnString$, Len(ReturnString$) - 1)
        End If
    End If
    
    EngNotate = Trim(ReturnString$ & Prefix$ & SetUOM$(SetIdx%))

End Function

Private Sub SizeArray(dArray() As Double, vUBound As String)
    'DESCRIPTION:
    '   This is a work-around for a bug in the National Instruments ComponentWorks
    '   CWGraph control.  The control won't display one last point for every 250
    '   points and one last point for every 62,500 points in the array being Y-plotted.
    '   Therefore, a number of bogus points must be added to the array so that the
    '   user can see them to modify them if desired.
    '   This subroutine adds the exact number of points (no more/no less) that are
    '   required to make the control appear to work correctly.
    'EXAMPLES:
    '   SizeArray Wave, SetDef$(LIST_DEF)
    'PARAMETERS:
    '   dArray():   The double-precision array to be re-sized.
    '   vUBound:    The desired array size to displayed and filled.
    'GLOBAL VARIABLES USED: none
    
    Dim iExtraPoints As Integer

    'iExtraPoints = (vUBound) \ 250
    'iExtraPoints = iExtraPoints + (vUBound) \ 62500
    'ReDim Preserve dArray(1 To (vUBound + iExtraPoints))
    
    'This change was undone 4/29/03 - Craig W. - Above three lines commented
    'The header of the .seg file appeared to indicate the correct number of points
    'Appending extra records to the array was yielding incorrect 0 V points because
    'there is no data in the file to fill these records.
    ReDim Preserve dArray(1 To vUBound)

End Sub

Private Function sIEEEDefinite(sDirection As String, sMsg As String)
  'Returns IEEE 488.2 Definite length block formatted string
  'Example: "#15HELLO"
  
  Dim iNumDigits As Integer
  Dim iNumChars As Integer
  Dim i As Integer
  
  If UCase(sDirection) = "FROM" Then
    iNumDigits = Mid$(sMsg, 2, 1)
    iNumChars = Mid$(sMsg, 3, iNumDigits)
    sIEEEDefinite = Mid$(sMsg, 3 + iNumDigits, iNumChars)
  Else
    iNumChars = Len(sMsg)
    iNumDigits = Len(CStr(iNumChars))
    sIEEEDefinite = "#" & CStr(iNumDigits) & CStr(iNumChars) & sMsg
  End If
  
End Function



