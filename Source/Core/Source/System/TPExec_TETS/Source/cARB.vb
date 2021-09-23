Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic

<System.Runtime.InteropServices.ProgId("cARB_NET.cARB")> Public Class cARB

    '****************************************************************
    '*** Constants **************************************************
    '****************************************************************

    'Amplitude Rules
    '****************************************************************
    'Hi-Z Impedance
    'Private Const DCMax = 10.2375
    'Private Const DCMin = -10.2400

    'Matched Impedance
    Const DCMax As Double = 5.11875
    Const DCMin As Double = -5.12

    'Hi-Z Impedance
    'Const DACFullScaleMax = 10.2375
    'Const DACFullScaleMin = 0.32374

    'Matched Impedance
    Const DACFullScaleMax As Double = 5.11875
    Const DACFullScaleMin As Double = 0.16187

    'DC Offset Rules
    '****************************************************************

    'Frequency Rules
    '****************************************************************
    Const RoscInt1Freq As Double = 42949672.96

    Const SineFrequencyMax As Double = 10737418.24
    Const SineFrequencyMin As Double = 0.01
    Const SineFrequencyResolution As Double = 0.01

    'Const SquareFrequencyMax = 2684354.56
    'Const SquareFrequencyMin = 0.0025
    'Const SquareFrequencyResolution = 0.0025

    '100 ppc, normal range
    'Const TriangleFrequencyMax = 107374.1824
    'Const TriangleFrequencyMin = 0.0001
    'Const TriangleFrequencyResolution = 0.0001

    'Points Rules
    '****************************************************************
    Const RampPointsMax As Integer = 262144
    Const RampPointsMin As Short = 4

    '100 ppc, doubled range
    'Const TriangleFrequencyMax = 214748.3648
    'Const TriangleFrequencyMin = 0.0002
    'Const TriangleFrequencyResolution = 0.0002

    ' First Coupling Group: "Frequency"
    Const TRIG_SOUR As Short = 1
    Const ROSC_FREQ_EXT As Short = 2
    Const ROSC_SOUR As Short = 3
    Const ARM_SWE_COUN As Short = 4
    Const ARM_SWE_SOUR As Short = 5
    Const FREQ_CENT As Short = 6
    Const FREQ As Short = 7
    Const FREQ_FSK_L As Short = 8
    Const FREQ_FSK_H As Short = 9
    Const FREQ_FSK_SOUR As Short = 10
    Const FREQ_MODE As Short = 11
    Const FREQ_RANG As Short = 12
    Const FREQ_SPAN As Short = 13
    Const FREQ_STAR As Short = 14
    Const FREQ_STOP As Short = 15
    Const FREQ2 As Short = 16
    Const LIST2_FREQ As Short = 17
    Const PM_SOUR As Short = 18
    Const PM_STAT As Short = 19

    Const SWE_COUN As Short = 20
    Const SWE_DIR As Short = 21
    Const SWE_POIN As Short = 22
    Const SWE_SPAC As Short = 23
    Const SWE_TIME As Short = 24
    Const TRIG_GATE_POL As Short = 25
    Const TRIG_GATE_SOUR As Short = 26
    Const TRIG_GATE_STAT As Short = 27

    Const TRIG_STOP_SLOP As Short = 28
    Const TRIG_STOP_SOUR As Short = 29
    Const TRIG_SWE_SOUR As Short = 30
    Const TRIG_SWE_TIM As Short = 31
    Const LAST_COUP_GROUP_1_COMMAND As Short = TRIG_SWE_TIM

    ' Second Coupling Group: "Frequency & Voltage"
    ' Send these with both groups
    Const ARB_DAC_SOUR As Short = 32
    Const FUNC As Short = 33
    Const RAMP_POIN As Short = 34
    Const LAST_COUP_GROUP_2_COMMAND As Short = RAMP_POIN

    ' Third Coupling Group: "Voltage"
    Const OUTP_IMP As Short = 35
    Const OUTP_LOAD As Short = 36
    Const OUTP_LOAD_AUTO As Short = 37
    Const RAMP_POL As Short = 38
    Const VOLT As Short = 39
    Const VOLT_OFFS As Short = 40
    Const LAST_COUP_GROUP_3_COMMAND As Short = VOLT_OFFS

    ' Non-Coupled: May be sent before or after coupled commands, but not in-between
    Const ABOR As Short = 41
    Const ARM_COUN As Short = 42
    Const ARM_LAY2_COUN As Short = 43
    Const ARM_LAY2 As Short = 44
    Const ARM_LAY2_SLOP As Short = 45
    Const ARM_LAY2_SOUR As Short = 46
    Const ARM_SWE As Short = 47
    Const ARM_SWE_LINK As Short = 48
    Const CAL_COUN As Short = 49
    Const CAL_DATA_AC As Short = 50
    Const CAL_DATA_AC2 As Short = 51
    Const CAL_DATA As Short = 52
    Const CAL_BEG As Short = 53
    Const CAL_POIN As Short = 54
    Const CAL_SEC_CODE As Short = 55
    Const CAL_SEC As Short = 56
    Const CAL_STAT As Short = 57
    Const CAL_STAT_AC As Short = 58
    Const CAL_STAT_DC As Short = 59
    Const INIT As Short = 60
    Const OUTP_FILT_FREQ As Short = 61
    Const OUTP_FILT As Short = 62
    Const OUTP As Short = 63
    Const ARB_DAC_FORM As Short = 64
    Const ARB_DOWN As Short = 65
    Const ARB_DOWN_COMP As Short = 66
    Const FUNC_USER As Short = 67
    Const LIST_FORM As Short = 68
    Const LIST_ADDR As Short = 69
    Const LIST_CAT As Short = 70
    Const LIST_COMB As Short = 71
    Const LIST_COMB_POIN As Short = 72
    Const LIST_DEF As Short = 73
    Const LIST_DEL_ALL As Short = 74
    Const LIST_DEL As Short = 75
    Const LIST_FREE As Short = 76
    Const LIST_MARK As Short = 77
    Const LIST_MARK_POIN As Short = 78
    Const LIST_MARK_SPO As Short = 79
    Const LIST_SEL As Short = 80
    Const LIST_VOLT As Short = 81
    Const LIST_VOLT_DAC As Short = 82
    Const LIST_VOLT_POIN As Short = 83
    Const LIST_SSEQ_ADDR As Short = 84
    Const LIST_SSEQ_CAT As Short = 85
    Const LIST_SSEQ_COMB As Short = 86
    Const LIST_SSEQ_COMB_POIN As Short = 87
    Const LIST_SSEQ_DEF As Short = 88
    Const LIST_SSEQ_DEL_ALL As Short = 89
    Const LIST_SSEQ_DEL As Short = 90
    Const LIST_SSEQ_DWEL_COUN As Short = 91
    Const LIST_SSEQ_DWEL_COUN_POIN As Short = 92
    Const LIST_SSEQ_FREE As Short = 93
    Const LIST_SSEQ_MARK As Short = 94
    Const LIST_SSEQ_MARK_POIN As Short = 95
    Const LIST_SSEQ_MARK_SPO As Short = 96
    Const LIST_SSEQ_SEL As Short = 97
    Const LIST_SSEQ_SEQ As Short = 98
    Const LIST_SSEQ_SEQ_SEG As Short = 99
    Const LIST2_FORM As Short = 100
    Const LIST2_POIN As Short = 101
    Const MARK_ECLT0_FEED As Short = 102
    Const MARK_ECLT0 As Short = 103
    Const MARK_ECLT1_FEED As Short = 104
    Const MARK_ECLT1 As Short = 105
    Const MARK_FEED As Short = 106
    Const MARK_POL As Short = 107
    Const MARK As Short = 108
    Const PM As Short = 109
    Const PM_UNIT As Short = 110
    Const VOLT_UNIT As Short = 111
    Const STAT_OPC_INIT As Short = 112
    Const STAT_OPER_COND As Short = 113
    Const STAT_OPER_ENAB As Short = 114
    Const STAT_OPER As Short = 115
    Const STAT_OPER_NTR As Short = 116
    Const STAT_OPER_PTR As Short = 117
    Const STAT_PRES As Short = 118
    Const SYST_ERR As Short = 119
    Const SYST_VER As Short = 120
    Const TRIG_COUN As Short = 121
    Const TRIG As Short = 122
    Const TRIG_SLOP As Short = 123
    Const TRIG_STOP As Short = 124
    Const TRIG_SWE As Short = 125
    Const TRIG_SWE_LINK As Short = 126
    Const VINS_LBUS As Short = 127
    Const VINS_LBUS_AUTO As Short = 128
    Const VINS_TEST_CONF As Short = 129
    Const VINS_TEST_DATA As Short = 130
    Const VINS_VME As Short = 131
    Const VINS_VME_REC_ADDR_DATA As Short = 132
    Const VINS_VME_REC_ADDR_READ As Short = 133
    Const VINS_IDEN As Short = 134

    'These are not SCPI parameters
    Const JDH_TIME As Short = 135
    Const JDH_FREQ As Short = 136
    Const JDH_CYCLES As Short = 137
    Const JDH_PHASE As Short = 138
    Const JDH_DCYC As Short = 139
    Const JDH_PM_CARR As Short = 140
    Const JDH_PM_MOD As Short = 141
    Const JDH_PM_POS_DEV As Short = 142
    Const JDH_PM_NEG_DEV As Short = 143
    Const JDH_DWEL_COUN As Short = 144
    Const VOLT_DIV_7 As Short = 145

    Const MAX_SETTINGS As Short = 146

    Private SetRes(MAX_SETTINGS) As String
    Private SetUOM(MAX_SETTINGS) As String
    Private SetMax(MAX_SETTINGS) As String
    Private SetMin(MAX_SETTINGS) As String
    Private SetDef(MAX_SETTINGS) As String
    Private GetVals(MAX_SETTINGS) As String
    Private SetMinInc(MAX_SETTINGS) As String
    Private ExecInit(MAX_SETTINGS) As Short
    Private SetCod(MAX_SETTINGS) As String

    Private CurFreqIdx As Short

    'Were Hidden Text Boxes in frm hpe1445a.frm
    Private txtFreq As String
    Private txtAmpl As String
    Private txtDcOffs As String
    Private txtAmplDiv7 As String
    'Private txtUserName As String
    'Private panAmplDiv7 As String 'Panel?

    Private txtSampleFreq As String
    Private UserEnteringData As Short
    Private bActivatingControls As Short
    Private Initiated As Short

    Private VOLT_At_V_50 As Double
    Private OFFS_At_V_50 As Double
    Private Quote As String
    Private instrumentHandle As Integer
    Private ErrorStatus As Integer
    '****************************************************************
    '*** End Of Constants *******************************************
    '****************************************************************

    Private SetCur(MAX_SETTINGS) As String ' "Current Settings" Array

    Private dVpkStepList(,) As Double

    Private iScale As Short

    Private ParameterError As Boolean

    Private Structure TracePreamble
        Dim Frmat As Short
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

    Private Trace As TracePreamble

    Public Sub SendMsg(ByRef sMsg As String)
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        gFrmMain.txtInstrument.Text = "ARB"
        gFrmMain.txtCommand.Text = "SendMsg"
        WriteMsg(ARB, sMsg)
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
    End Sub

    Public Function sReadMsg(ByRef sMsg As String) As String
        sReadMsg = ""

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        gFrmMain.txtInstrument.Text = "ARB"
        gFrmMain.txtCommand.Text = "ReadMsg"
        ReadMsg(ARB, sReadMsg)

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
    End Function

    Public Sub LoadConfigFile(ByRef sConfigFile As String, Optional ByRef nTime As Integer = 0)
        Const PRE_SEGMENT As String = "[PRE_SEGMENT_LOAD_COMMANDS]"
        Const SEGMENT_LIST As String = "[SEGMENT_LIST]"
        Const POST_SEGMENT As String = "[POST_SEGMENT_LOAD_COMMANDS]"
        Dim i As Short
        Dim nStatus As Integer
        Dim dOffSet As Double
        Dim FileNumber As Short
        Dim SegFileNumber As Short
        Dim sLineBuffer As String
        Dim sSegmentText As String
        Dim TyxStupidMagicNumber As Integer
        Dim TmpName As String
        Dim FileSize, NumBytes, MaxFileSize As Integer
        Dim nSeqmentPoints As Integer
        Dim Wave() As Double
        Dim iCurrentProgressValue As Short
        Dim sCurrentStatusMessage As String = ""
        Dim iCurrentMin As Short
        Dim iCurrentMax As Short
        Dim iCurrentValue As Short
        Dim bView As Boolean

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        gFrmMain.txtInstrument.Text = "ARB"
        gFrmMain.txtCommand.Text = "LoadConfigFile"
        nStatus = VI_SUCCESS

        If nTime > 5 Then
            iCurrentProgressValue = gFrmMain.ProgressBar.Value
            sCurrentStatusMessage = gFrmMain.lblStatus.Text
            iCurrentMax = gFrmMain.ProgressBar.Maximum
            iCurrentMin = gFrmMain.ProgressBar.Minimum
            iCurrentValue = gFrmMain.ProgressBar.Value
            bView = gFrmMain.ProgressBar.Visible

            gFrmMain.ProgressBar.Maximum = 100
            gFrmMain.ProgressBar.Minimum = 0
            gFrmMain.ProgressBar.Value = 0
            gFrmMain.lblStatus.Text = "Loading Config File ..."
            gFrmMain.Timer3.Enabled = True
        End If

        'Set Timeout value
        If Not bSimulation Then nStatus = viSetAttribute(nInstrumentHandle(ARB), VI_ATTR_TMO_VALUE, 10000) '10 sec.
        If nStatus <> VI_SUCCESS Then
            Echo("ARB PROGRAMMING ERROR:  Command cmdARB.LoadConfigFile - Unable to set VISA Attribute.")
            Err.Raise(-1001)
            Exit Sub
        End If

        'Open Configuration File
        FileNumber = FreeFile()
        On Error Resume Next
        FileOpen(FileNumber, ProgramPath & sConfigFile, OpenMode.Input, OpenAccess.Read)
        System.Windows.Forms.Application.DoEvents()
        If Err.Number Then
            Echo("ARB PROGRAMMING ERROR:  Command cmdARB.LoadConfigFile configuration file not found.")
            Err.Raise(-1001)
            Exit Sub
        End If

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub

        dOffSet = 0.0
        'Read Line 1 of Configuration File
        sLineBuffer = LineInput(FileNumber) ' Read line of data.
        If UCase(sLineBuffer) <> PRE_SEGMENT Then
            Echo("ARB PROGRAMMING ERROR:  Command cmdARB.LoadConfigFile configuration file corrupt.")
            Err.Raise(-1001)
            FileClose(FileNumber)
            Exit Sub
        End If
        System.Windows.Forms.Application.DoEvents()
        'Read in PRE_SEGMENT_LOAD_COMMANDS
        sLineBuffer = LineInput(FileNumber) ' Read Next line of data.
        Do While Not EOF(FileNumber)
            System.Windows.Forms.Application.DoEvents()
            Select Case sLineBuffer
                Case "", SEGMENT_LIST
                    Exit Do
                Case Else
                    i = InStr(sLineBuffer, "VOLT:OFFS")
                    If i <> 0 Then
                        dOffSet = CDbl(Mid(sLineBuffer, i + 10))
                    End If
                    If Not bSimulation Then WriteMsg(ARB, sLineBuffer)
                    Debug.Print(sLineBuffer)
            End Select
            sLineBuffer = LineInput(FileNumber) ' Read line of data.
        Loop

        'Read in SEGMENT FILE NAMES
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        If sLineBuffer = SEGMENT_LIST Then
            sLineBuffer = LineInput(FileNumber) ' Read line of data.
        End If
        Do While Not EOF(FileNumber)
            System.Windows.Forms.Application.DoEvents()
            Select Case sLineBuffer
                Case "", POST_SEGMENT
                    Exit Do
                Case Else
                    If Not bSimulation Then WriteMsg(ARB, ":LIST:SEL " & sLineBuffer)
                    Debug.Print(":LIST:SEL " & sLineBuffer)
                    sLineBuffer = sLineBuffer & ".SEG"
                    'Open Segment File for Read
                    SegFileNumber = FreeFile()
                    On Error Resume Next
                    FileOpen(SegFileNumber, ProgramPath & sLineBuffer, OpenMode.Binary, OpenAccess.Read)
                    If Err.Number Then
                        Echo("ARB PROGRAMMING ERROR:  Command cmdARB.LoadConfigFile " & sLineBuffer & " file not found.")
                        Err.Raise(-1001)
                        Exit Sub
                    End If
                    'Check Unique stupid number
                    FileGet(SegFileNumber, TyxStupidMagicNumber, 1) 'Get byte of data
                    If TyxStupidMagicNumber <> &HABCD4567 Then
                        Echo("ARB PROGRAMMING ERROR:  Command cmdARB.LoadConfigFile " & sLineBuffer & " file corrupt.")
                        Err.Raise(-1001)
                        FileClose(SegFileNumber)
                        Exit Sub
                    End If
                    'Get Number of Bytes
                    FileGet(SegFileNumber, NumBytes, 5)
                    If (NumBytes Mod 8) <> 0 Then
                        Echo("ARB PROGRAMMING ERROR:  Command cmdARB.LoadConfigFile " & sLineBuffer & " file incorrect .")
                        Err.Raise(-1001)
                        FileClose(SegFileNumber)
                        Exit Sub
                    End If
                    If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
                    'Allocate variable for number of datapoints
                    nSeqmentPoints = NumBytes / 8
                    If Not bSimulation Then WriteMsg(ARB, "LIST:DEF " & CStr(nSeqmentPoints))
                    ReDim Wave(nSeqmentPoints)
                    'Read the segment voltage points
                    FileGet(SegFileNumber, Wave, 257)
                    FileClose(SegFileNumber)
                    'Build String
                    sSegmentText = ":LIST:VOLT "
                    For i = 0 To (nSeqmentPoints - 1)
                        sSegmentText = sSegmentText & CStr(Wave(i)) & ","
                    Next i
                    sSegmentText = sSegmentText & CStr(Wave(nSeqmentPoints))
                    'Set Time out value per data points
                    If nSeqmentPoints > 4000 Then
                        If Not bSimulation Then nStatus = viSetAttribute(nInstrumentHandle(ARB), VI_ATTR_TMO_VALUE, (nSeqmentPoints / 400) * 1000)
                        If nStatus <> VI_SUCCESS Then
                            Echo("ARB PROGRAMMING ERROR:  Command cmdARB.LoadConfigFile - Unable to set VISA Attribute.")
                            Err.Raise(-1001)
                            Exit Sub
                        End If
                        If Not bSimulation Then WriteMsg(ARB, sSegmentText)
                        Debug.Print(sSegmentText)
                        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
                        If Not bSimulation Then nStatus = viSetAttribute(nInstrumentHandle(ARB), VI_ATTR_TMO_VALUE, 10000)
                        If nStatus <> VI_SUCCESS Then
                            Echo("ARB PROGRAMMING ERROR:  Command cmdARB.LoadConfigFile - Unable to set VISA Attribute.")
                            Err.Raise(-1001)
                            Exit Sub
                        End If
                    Else
                        If Not bSimulation Then WriteMsg(ARB, sSegmentText)
                        Debug.Print(sSegmentText)
                    End If
            End Select
            sLineBuffer = LineInput(FileNumber) ' Read line of data.
        Loop

        'Post Segment command
        If sLineBuffer = POST_SEGMENT Then
            sLineBuffer = LineInput(FileNumber) ' Read line of data.
        End If
        Do While Not EOF(FileNumber)
            System.Windows.Forms.Application.DoEvents()
            Select Case sLineBuffer
                Case ""
                    Exit Do
                Case Else
                    If Not bSimulation Then WriteMsg(ARB, sLineBuffer)
                    Debug.Print(sLineBuffer)
            End Select
            sLineBuffer = LineInput(FileNumber) ' Read line of data.
        Loop
        If sLineBuffer <> "" Or sLineBuffer <> POST_SEGMENT Then
            If Not bSimulation Then WriteMsg(ARB, sLineBuffer)
            Debug.Print(sLineBuffer)
        End If
        If Not bSimulation Then WriteMsg(ARB, ":INIT")
        Debug.Print(":INIT")

        If nTime > 5 Then
            gFrmMain.ProgressBar.Value = iCurrentProgressValue
            gFrmMain.lblStatus.Text = sCurrentStatusMessage
            gFrmMain.Timer3.Enabled = False
            gFrmMain.ProgressBar.Maximum = iCurrentMax
            gFrmMain.ProgressBar.Minimum = iCurrentMin
            gFrmMain.ProgressBar.Value = iCurrentValue
            gFrmMain.ProgressBar.Visible = bView
            gFrmMain.Timer3.Enabled = False
        End If
    End Sub

    Public Sub Remove()
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        gFrmMain.txtInstrument.Text = "ARB"
        gFrmMain.txtCommand.Text = "Remove"

        If Not bSimulation Then WriteMsg(ARB, "*RST;*CLS")
    End Sub

    Public Sub StopAll()
        If Not bSimulation Then
            WriteMsg(ARB, "ABOR ")
            WriteMsg(ARB, "OUTP:STAT OFF")
            WriteMsg(ARB, "*RST")
        End If
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
    End Sub

    Public Sub StandardWaveform(ByRef sShape As String,
                                ByRef dAmplitude As Double,
                                Optional ByRef dFrequency As Double = 1000,
                                Optional ByRef dDCOffset As Double = 0.0,
                                Optional ByRef sOutputZ As String = "50",
                                Optional ByRef sLoadZ As String = "Hi-Impedance",
                                Optional ByRef sLowPassFilter As String = "AUTO",
                                Optional ByRef lRampPoints As Integer = 100)
        Dim iStep As Short
        Dim iScale As Short

        gFrmMain.txtInstrument.Text = "ARB"
        gFrmMain.txtCommand.Text = UCase(sShape)

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub

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
                Echo(sLoadZ + " is not a valid output load impedance. " + vbCrLf + "Select ""50"", ""75"" or ""Hi-Z"".  The default is ""50"".")
                Err.Raise(1000, , sLoadZ + " is not a valid output load impedance. " + vbCrLf + "Select ""50"", ""75"" or ""Hi-Z"".  The default is ""50"".")
                Exit Sub
        End Select

        'Error Check Output Impedance
        Select Case UCase(sOutputZ)
            Case "50", "50 OHM", "50-OHM"
                If sLoadZ = "75" Then
                    'Instrument Manual Chapter 2 Page 70
                    Echo("Output impedance cannot be 50 ohms if load impedance is 75 ohms." & vbCrLf & "Select 75 ohms or infinite.")
                    Err.Raise(1000, , "Output impedance cannot be 50 ohms if load impedance is 75 ohms." & vbCrLf & "Select 75 ohms or infinite.")
                    Exit Sub
                Else
                    sOutputZ = "50"
                End If
            Case "75", "75 OHM", "75-OHM"
                If sLoadZ = "50" Then
                    'Instrument Manual Chapter 2 Page 70
                    Echo("Output impedance cannot be 75 ohms if load impedance is 50 ohms." & vbCrLf & "Select 50 ohms or infinite.")
                    Err.Raise(1000, , "Output impedance cannot be 75 ohms if load impedance is 50 ohms." & vbCrLf & "Select 50 ohms or infinite.")
                    Exit Sub
                Else
                    sOutputZ = "75"
                End If
            Case Else
                Echo(sOutputZ + " is not a valid output load impedance. " + vbCrLf + "Select ""50"" or ""75"" The default is ""50""." + vbCrLf + _
                     "If ""50"" or ""75"" has been selected for load impedance this parameter will have no effect.")
                Err.Raise(1000, , sOutputZ + " is not a valid output load impedance. " + vbCrLf + "Select ""50"" or ""75"" The default is ""50""." + vbCrLf + _
                          "If ""50"" or ""75"" has been selected for load impedance this parameter will have no effect.")
                Exit Sub
        End Select

        'Generate waveform of sShape
        Select Case UCase(sShape)
            Case "SIN", "SINE"
                sShape = "Sine"
                'CreateVpkStepList (iScale)
                iStep = FindVpkStep(dAmplitude, iScale)
                ParameterError = ErrorCheck(sShape, dAmplitude, dFrequency, sLowPassFilter, iStep, iScale, dDCOffset, lRampPoints)
                If ParameterError Then Exit Sub

                'Limit the resolution of the DC offset value
                dDCOffset = Fix(dDCOffset / (0.000499966 * iScale)) * 0.000499966 * iScale

                'Stop any previous waveform
                WriteMsg(ARB, "*RST")
                WriteMsg(ARB, "VOLT:UNIT:VOLT  V")
                WriteMsg(ARB, "VOLT  1") 'Temporary Amplitude to satisfy limits while impdedance is set
                Select Case sLoadZ
                    Case "50"
                        WriteMsg(ARB, "OUTP:IMP  50")
                        WriteMsg(ARB, "OUTP:LOAD  50")
                    Case "75"
                        WriteMsg(ARB, "OUTP:IMP  75")
                        WriteMsg(ARB, "OUTP:LOAD  75")
                    Case "HI-Z"
                        WriteMsg(ARB, "OUTP:IMP  " + sOutputZ)
                        WriteMsg(ARB, "OUTP:LOAD  INF")
                End Select
                WriteMsg(ARB, "VOLT  " & CStr(dVpkStepList(1, iStep)))
                WriteMsg(ARB, "FREQ " & CStr(dFrequency))
                If UCase(sLowPassFilter) <> "OFF" Then
                    WriteMsg(ARB, "OUTP:FILT:LPAS:FREQ " & sLowPassFilter)
                    WriteMsg(ARB, "OUTP:FILT:LPAS:STAT ON")
                End If
                WriteMsg(ARB, "FUNC  SIN")
                WriteMsg(ARB, "VOLT:OFFS " & CStr(dDCOffset))
                PrecisionDelay(0.05)
                WriteMsg(ARB, "INIT")

            Case "TRI", "TRIANGLE", "SAW", "SAWTOOTH", "TRIANGULAR"
                sShape = "Triangular"
                'CreateVpkStepList (iScale)
                iStep = FindVpkStep(dAmplitude, iScale)
                ParameterError = ErrorCheck(sShape, dAmplitude, dFrequency, sLowPassFilter, iStep, iScale, dDCOffset, lRampPoints)
                If ParameterError Then Exit Sub

                'Limit the resolution of the DC offset value
                dDCOffset = Fix(dDCOffset / (0.000499966 * iScale)) * 0.000499966 * iScale

                WriteMsg(ARB, "*RST")
                WriteMsg(ARB, "VOLT:UNIT:VOLT  V")
                WriteMsg(ARB, "VOLT  1") 'Temporary Amplitude to satisfy limits while impdedance is set
                Select Case sLoadZ
                    Case "50"
                        WriteMsg(ARB, "OUTP:IMP  50")
                        WriteMsg(ARB, "OUTP:LOAD  50")
                    Case "75"
                        WriteMsg(ARB, "OUTP:IMP  75")
                        WriteMsg(ARB, "OUTP:LOAD  75")
                    Case "HI-Z"
                        WriteMsg(ARB, "OUTP:IMP  " + sOutputZ)
                        WriteMsg(ARB, "OUTP:LOAD  INF")
                End Select
                WriteMsg(ARB, "VOLT  " & CStr(dVpkStepList(1, iStep)))
                WriteMsg(ARB, "FREQ:RANG " & CStr(dFrequency))
                WriteMsg(ARB, "FREQ " & CStr(dFrequency))
                If UCase(sLowPassFilter) <> "OFF" Then
                    WriteMsg(ARB, "OUTP:FILT:LPAS:FREQ " & sLowPassFilter)
                    WriteMsg(ARB, "OUTP:FILT:LPAS:STAT ON")
                End If
                WriteMsg(ARB, "FUNC  TRI")
                WriteMsg(ARB, "SOUR:RAMP:POIN " & CStr(lRampPoints))
                WriteMsg(ARB, "VOLT:OFFS " & CStr(dDCOffset))
                WriteMsg(ARB, "INIT")

            Case "SQU", "SQUARE", "SQU"
                sShape = "Square"
                'CreateVpkStepList (iScale)
                iStep = FindVpkStep(dAmplitude, iScale)
                ParameterError = ErrorCheck(sShape, dAmplitude, dFrequency, sLowPassFilter, iStep, iScale, dDCOffset, lRampPoints)
                If ParameterError Then Exit Sub

                'Limit the resolution of the DC offset value
                dDCOffset = Fix(dDCOffset / (0.000499966 * iScale)) * 0.000499966 * iScale

                WriteMsg(ARB, "*RST")
                WriteMsg(ARB, "VOLT:UNIT:VOLT  V")
                WriteMsg(ARB, "VOLT  1") 'Temporary Amplitude to satisfy limits while impdedance is set
                Select Case sLoadZ
                    Case "50"
                        WriteMsg(ARB, "OUTP:IMP  50")
                        WriteMsg(ARB, "OUTP:LOAD  50")
                    Case "75"
                        WriteMsg(ARB, "OUTP:IMP  75")
                        WriteMsg(ARB, "OUTP:LOAD  75")
                    Case "HI-Z"
                        WriteMsg(ARB, "OUTP:IMP  " + sOutputZ)
                        WriteMsg(ARB, "OUTP:LOAD  INF")
                End Select
                WriteMsg(ARB, "VOLT  " & CStr(dVpkStepList(1, iStep)))
                WriteMsg(ARB, "FREQ:RANG " & CStr(dFrequency))
                WriteMsg(ARB, "FREQ " & CStr(dFrequency))
                If UCase(sLowPassFilter) <> "OFF" Then
                    WriteMsg(ARB, "OUTP:FILT:LPAS:FREQ " & sLowPassFilter)
                    WriteMsg(ARB, "OUTP:FILT:LPAS:STAT ON")
                End If
                WriteMsg(ARB, "FUNC  SQU")
                WriteMsg(ARB, "VOLT:OFFS " & CStr(dDCOffset))
                WriteMsg(ARB, "INIT")

            Case "RAMP"
                sShape = "Ramp"
                'CreateVpkStepList (iScale)
                iStep = FindVpkStep(dAmplitude, iScale)
                ParameterError = ErrorCheck(sShape, dAmplitude, dFrequency, sLowPassFilter, iStep, iScale, dDCOffset, lRampPoints)
                If ParameterError Then Exit Sub

                'Limit the resolution of the DC offset value
                dDCOffset = Fix(dDCOffset / (0.000499966 * iScale)) * 0.000499966 * iScale

                WriteMsg(ARB, "*RST")
                WriteMsg(ARB, "VOLT:UNIT:VOLT  V")
                WriteMsg(ARB, "VOLT  1") 'Temporary Amplitude to satisfy limits while impdedance is set
                Select Case sLoadZ
                    Case "50"
                        WriteMsg(ARB, "OUTP:IMP  50")
                        WriteMsg(ARB, "OUTP:LOAD  50")
                    Case "75"
                        WriteMsg(ARB, "OUTP:IMP  75")
                        WriteMsg(ARB, "OUTP:LOAD  75")
                    Case "HI-Z"
                        WriteMsg(ARB, "OUTP:IMP  " + sOutputZ)
                        WriteMsg(ARB, "OUTP:LOAD  INF")
                End Select
                WriteMsg(ARB, "VOLT  " & CStr(dVpkStepList(1, iStep)))
                WriteMsg(ARB, "FREQ:RANG " & CStr(dFrequency))
                WriteMsg(ARB, "FREQ " & CStr(dFrequency))
                If UCase(sLowPassFilter) <> "OFF" Then
                    WriteMsg(ARB, "OUTP:FILT:LPAS:FREQ " & sLowPassFilter)
                    WriteMsg(ARB, "OUTP:FILT:LPAS:STAT ON")
                End If
                WriteMsg(ARB, "FUNC  RAMP")
                WriteMsg(ARB, "SOUR:RAMP:POIN " & CStr(lRampPoints))
                WriteMsg(ARB, "VOLT:OFFS " & CStr(dDCOffset))
                WriteMsg(ARB, "INIT")

            Case "DC"
                'Error Check dAmplitude
                If dAmplitude > (DCMax * iScale) Or dAmplitude < (DCMin * iScale) Then
                    Echo(CStr(dAmplitude) & "Vdc is beyond the valid amplitude range for DC function." & vbCrLf & _
                         CStr(DCMin * iScale) & "Vdc to " & CStr(DCMax * iScale) & "Vdc is the valid range.")
                    Err.Raise(1000, , CStr(dAmplitude) & "Vdc is beyond the valid amplitude range for DC function." & vbCrLf & _
                              CStr(DCMin * iScale) & "Vdc to " & CStr(DCMax * iScale) & "Vdc is the valid range.")
                    Exit Sub
                End If

                'Error Check DC Offset
                If dDCOffset > (5 * iScale) Or dDCOffset < (-5 * iScale) Then
                    Echo(CStr(CDbl("The DC offset specified is out of range for this amplitude setting." & vbCrLf & _
                         "The acceptable range for DC Offset is: ") + (-5 * iScale) + CDbl("Vdc to ") + (5 * iScale) + CDbl(" Vdc")))
                    Err.Raise(1000, ,
                              CDbl("The DC offset specified is out of range for this amplitude setting." & vbCrLf & _
                              "The acceptable range for DC Offset is: ") + (-5 * iScale) + CDbl("Vdc to ") + (5 * iScale) + CDbl(" Vdc"))
                    Exit Sub
                End If

                WriteMsg(ARB, "*RST")
                WriteMsg(ARB, "FUNC  DC")
                Select Case sLoadZ
                    Case "50"
                        WriteMsg(ARB, "OUTP:IMP  50")
                        WriteMsg(ARB, "OUTP:LOAD  50")
                    Case "75"
                        WriteMsg(ARB, "OUTP:IMP  75")
                        WriteMsg(ARB, "OUTP:LOAD  75")
                    Case "HI-Z"
                        WriteMsg(ARB, "OUTP:IMP  " + sOutputZ)
                        WriteMsg(ARB, "OUTP:LOAD  INF")
                End Select
                WriteMsg(ARB, "VOLT  " & CStr(dAmplitude))
                WriteMsg(ARB, "VOLT:OFFS " & CStr(dDCOffset))
                'WriteMsg ARB, "INIT"

            Case Else
                Err.Raise(1000, ,
                          sShape & " is not a recognized standard waveform shape supported by the ARB." & vbCrLf & _
                          "Supported waveform shapes are Sine, Triangle, Ramp and Square.")
                Echo(sShape & " is not a recognized standard waveform shape supported by the ARB." & vbCrLf & _
                     "Supported waveform shapes are Sine, Triangle, Ramp and Square.")
        End Select

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
    End Sub

    '*****************************************************************
    '     Private Subs and Functions                                 *
    '     Not visible to the interface                               *
    '*****************************************************************
    Private Function ErrorCheck(ByRef sShape As String, ByRef dAmplitude As Double, ByRef dFrequency As Double,
                                ByRef sLowPassFilter As String, ByRef iStep As Short, ByRef iScale As Short,
                                ByRef dDCOffset As Double, ByRef lRampPoints As Integer) As Boolean
        Dim FreqMax As Double
        Dim FreqMin As Double
        Dim DummyError As Integer
        Dim lAvailableSegmentMemory As Integer
        Dim sAvailableSegmentMemory As String = ""

        ErrorCheck = False

        'Error Check Frequency
        Select Case UCase(sShape)
            Case "SINE"
                FreqMax = RoscInt1Freq / 4 'Frequency doubling not available for sine wave
                FreqMin = 0
                If dFrequency > SineFrequencyMax Or dFrequency < SineFrequencyMin Then
                    Echo(CStr(dFrequency) & "Hz is out of range for the " & sShape & " wave function." & vbCrLf & _
                         "The available range is:  " & CStr(SineFrequencyMin) & " Hz to " & CStr(SineFrequencyMax) & " Hz.")
                    Err.Raise(1000, ,
                              CStr(dFrequency) & "Hz is out of range for the " & sShape & " wave function." & vbCrLf & _
                              "The available range is:  " & CStr(SineFrequencyMin) & " Hz to " & CStr(SineFrequencyMax) & " Hz.")
                    ErrorCheck = True
                End If
            Case "SQUARE"
                FreqMax = RoscInt1Freq / 8 'Allow for Frequency doubling
                FreqMin = 0
                If dFrequency > FreqMax Or dFrequency < FreqMin Then
                    Echo(CStr(dFrequency) & "Hz is out of range for the " & sShape & " wave function." & vbCrLf & _
                         "The available range is:  " & CStr(FreqMin) & " Hz to " & CStr(FreqMax) & " Hz.")
                    Err.Raise(1000, ,
                              CStr(dFrequency) & "Hz is out of range for the " & sShape & " wave function." & vbCrLf & _
                              "The available range is:  " & CStr(FreqMin) & " Hz to " & CStr(FreqMax) & " Hz.")
                    ErrorCheck = True
                End If
            Case "TRIANGULAR", "RAMP"
                FreqMax = RoscInt1Freq / 2 / lRampPoints ' Allow for automatic doubled frequency selection
                FreqMin = 0
                If dFrequency > FreqMax Or dFrequency < FreqMin Then
                    Echo(CStr(dFrequency) & "Hz is out of range for the " & sShape & " wave function." & vbCrLf & _
                         "The available range is:  " & CStr(FreqMin) & " Hz to " & CStr(FreqMax) & " Hz.")
                    Err.Raise(1000, ,
                              CStr(dFrequency) & "Hz is out of range for the " & sShape & " wave function." & vbCrLf & _
                              "The available range is:  " & CStr(FreqMin) & " Hz to " & CStr(FreqMax) & " Hz.")
                    ErrorCheck = True
                End If
            Case "DC"
        End Select

        'Error Check Amplitude
        If dAmplitude > (DACFullScaleMax * iScale) Or dAmplitude < (DACFullScaleMin * iScale) Then
            Echo(CStr(dAmplitude) & "Vdc is beyond the valid amplitude range." & vbCrLf & _
                 CStr(DACFullScaleMin * iScale) & "Vdc to " & CStr(DACFullScaleMax * iScale) & "Vdc is the valid range.")
            Err.Raise(1000, ,
                      CStr(dAmplitude) & "Vdc is beyond the valid amplitude range." & vbCrLf & _
                      CStr(DACFullScaleMin * iScale) & "Vdc to " & CStr(DACFullScaleMax * iScale) & "Vdc is the valid range.")
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
                Echo("The specified output low pass filter parameter is not supported: " & sLowPassFilter & vbCrLf & _
                     "Available low pass filter settings are ""Off"", ""250 KHz"", and ""10 MHz"".")
                Err.Raise(1000, ,
                          "The specified output low pass filter parameter is not supported: " & sLowPassFilter & vbCrLf & _
                          "Available low pass filter settings are ""Off"", ""250 KHz"", and ""10 MHz"".")
                ErrorCheck = True
        End Select

        'Error Check DC Offset
        If dDCOffset > dVpkStepList(2, iStep) Or dDCOffset < -dVpkStepList(2, iStep) Then
            Echo("The DC offset specified is out of range for this amplitude setting." & vbCrLf & _
                 "The acceptable range for DC Offset is: " & CStr(-dVpkStepList(2, iStep)) & "Vdc to " & CStr(dVpkStepList(2, iStep)) & " Vdc")
            Err.Raise(1000, ,
                      "The DC offset specified is out of range for this amplitude setting." & vbCrLf & _
                      "The acceptable range for DC Offset is: " & CStr(-dVpkStepList(2, iStep)) & "Vdc to " & CStr(dVpkStepList(2, iStep)) & " Vdc")
            ErrorCheck = True
        End If

        If Not bSimulation Then
            WriteMsg(ARB, "LIST:SEGM:FREE?")
            DummyError = ReadMsg(ARB, sAvailableSegmentMemory)
            lAvailableSegmentMemory = CInt(Left(sAvailableSegmentMemory, InStr(1, sAvailableSegmentMemory, ",") - 1))
        Else
            lAvailableSegmentMemory = 262144
        End If

        'Error Check Ramp Points
        Select Case UCase(sShape)
            '      Case "SINE", "SQUARE"
            '        If lRampPoints > lAvailableSegmentMemory Or lRampPoints < 8 Then
            '          Echo  Cstr(lRampPoints) + " is not a valid number of ramp points." + vbCrLf + _
            ''          "The available range is:  8 to " +  Cstr(lAvailableSegmentMemory) + "."
            '          Err.Raise 1000, , _
            ''           Cstr(lRampPoints) + " is not a valid number of ramp points." + vbCrLf + _
            ''          "The available range is:  8 to " +  Cstr(lAvailableSegmentMemory) + "."
            '          ErrorCheck = True
            '        End If
            Case "TRIANGULAR", "RAMP"
                If lRampPoints > lAvailableSegmentMemory Or lRampPoints < 4 Then
                    Echo(CStr(lRampPoints) & " is not a valid number of ramp points." & vbCrLf & _
                         "The available range is:  4 to " & CStr(lAvailableSegmentMemory) & ".")
                    Err.Raise(1000, ,
                              CStr(lRampPoints) & " is not a valid number of ramp points." & vbCrLf & _
                              "The available range is:  4 to " & CStr(lAvailableSegmentMemory) & ".")
                End If
            Case "DC"
        End Select

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
    End Function

    Private Sub CreateVpkStepList(ByRef iScale As Short)
        Dim dCurrentStepAttenuation As Double
        Dim iStep As Short

        'Necesary to Do only once.
        'Fill a table with exact Amplitude Steps on the linear scale for a matched output.
        ReDim dVpkStepList(3, 3001)
        For iStep = 0 To 3000
            dCurrentStepAttenuation = iStep * 0.01
            dVpkStepList(1, iStep) = InverseLog10((20 * Log10(DACFullScaleMax * iScale) - dCurrentStepAttenuation) / 20)
            dVpkStepList(2, iStep) = -1 'Symbolize empty - only calculate limit for needed step
        Next iStep

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
    End Sub

    Private Function FindVpkStep(ByRef dAmplitude As Double, ByRef iScale As Short) As Short
        'Will round down to the nearest step
        Dim dCurrentStepAttenuation As Double
        Dim iStep As Short
        Dim iUBound As Short
        Dim iLBound As Short

        ReDim dVpkStepList(3, 3001)

        iUBound = 3001
        iLBound = 0

        Do
            iStep = (iUBound - iLBound) \ 2 + iLBound
            dCurrentStepAttenuation = iStep * 0.01
            dVpkStepList(1, iStep) = InverseLog10((20 * Log10(DACFullScaleMax * iScale) - dCurrentStepAttenuation) / 20)
            If iStep = iUBound Or iStep = iLBound Then
                Exit Do
            End If

            If dVpkStepList(1, iStep) < dAmplitude Then
                iUBound = iStep
            Else
                iLBound = iStep
            End If
        Loop

        'At this point iLBound and iUbound are at a difference of 1
        'iLBound will be selected if dAmplitude is greater than the maximum allowable
        'iUBound will be selected if dAmplitude is between the two points or
        'if it is less than the minimum allowable
        If dAmplitude >= dVpkStepList(1, iLBound) Then
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


    Private Function Log10(ByRef dLinearNumber As Double) As Double
        Log10 = System.Math.Log(dLinearNumber) / System.Math.Log(10.0)
    End Function

    Private Function InverseLog10(ByRef dLogarithmicBase10Number As Double) As Double
        InverseLog10 = System.Math.Exp(System.Math.Log(10) * dLogarithmicBase10Number)
    End Function

    Private Sub PrecisionDelay(ByRef seconds As Double)
        'This procedure is dependent upon system performance
        'The Timer! object is updated every 55 mS
        'If the system performance permits excecution time of this loop
        'to be 55 mS or less then this will be the resolution of this Delay
        'and the additional error will be at least a 10th order smaller
        'but is also of course dependent on system performance
        'A better Time Measuring scheme could be written using windows API
        'function timeGetTime which has a resolution of 1 to 5 mS
        Dim StartTime As Double

        StartTime = VB.Timer()
        While VB.Timer() < (StartTime + seconds)
            If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        End While
    End Sub

    Public Sub DownloadSegmentFile(ByRef SegmentName As String, ByRef fileName As String, Optional ByRef sOutputZ As String = "50",
                                   Optional ByRef sLoadZ As String = "Hi-Impedance", Optional ByRef dAmplitude As Double = -1)
        Dim FileId As Short
        Dim FileSize, NumBytes, MaxFileSize As Integer
        Dim TyxStupidMagicNumber As Integer
        Dim TmpStr As String = ""
        Dim i As Short
        Dim ReadChar As Byte
        Dim List(11) As String
        Dim txtUserPoints As String
        Dim Wave() As Double = {0.0}
        'Dim bActivatingControls As Boolean
        Dim UserEnteringData As Short

        FileSize = FileLen(fileName)
        MaxFileSize = (8 * 262144) + 511 '256 Byte Header Block plus up to 255 filler bytes Added Version 1.2
        If FileSize > MaxFileSize Then
            'Filesize error
            MsgBox("File " & fileName & " is too big." & "Cannot be more than " & (MaxFileSize + 511).ToString() & " bytes.", MsgBoxStyle.Critical)
            Exit Sub
        End If
        If FileSize Mod (8) <> 0 Or FileSize < 512 Then
            'Filesize error
            MsgBox("File " & fileName & " is not the correct size.", MsgBoxStyle.Critical)
            Exit Sub
        End If

        '---------------Version 1.2 Modification By DHartley---------------
        '[Re]Format Wave for TETS-ATLAS Compatability (256 Byte File Blocks)
        FileId = FreeFile()
        FileOpen(FileId, fileName, OpenMode.Binary, OpenAccess.Read)

        'Added for V1.3 JHill
        FileGet(FileId, TyxStupidMagicNumber, 1)
        If TyxStupidMagicNumber <> &HABCD4567 Then
            'Tyx Magic Number Error
            MsgBox("File " & fileName & " does not have the correct format.", MsgBoxStyle.Critical)
            FileClose(FileId)
            Exit Sub
        End If

        FileGet(FileId, NumBytes, 5)
        SetCur(LIST_DEF) = (NumBytes / 8).ToString()
        SetRes(LIST_DEF) = "N0"
        txtUserPoints = EngNotate(CDbl(SetCur(LIST_DEF)), LIST_DEF)
        SizeArray(Wave, SetCur(LIST_DEF))

        'If a user-clicked Load, then do this, else its a Configuration load, so don't.
        'Dig out the embedded Trace Preamble data
        For i = 9 To 256
            FileGet(FileId, ReadChar, i)
            If ReadChar > 0 Then
                TmpStr = TmpStr & Chr(ReadChar)
            Else
                Exit For
            End If
        Next i

        'If found an IEEE Definite Length Block...
        If Left(TmpStr, 1) = "#" Then
            TmpStr = CStr(sIEEEDefinite("From", TmpStr))
            'If correct number of arguments, process Preamble data, else don't
            If StringToList(TmpStr, List, ",") = 10 Then
                Trace.Frmat = List(1) '?
                Trace.Type = CShort(List(2)) '?
                Trace.Points = CInt(List(3))
                Trace.count = CShort(List(4)) '?
                Trace.XInc = CDbl(List(5))
                Trace.XOrig = CDbl(List(6))
                Trace.XRef = CShort(List(7)) '?
                Trace.YInc = CDbl(List(8))
                Trace.YOrig = CDbl(List(9))
                Trace.YRef = CShort(List(10)) '?

                'Set sample clock (FREQ)
                'txtFreq = 1 / Trace.XInc
                'UserEnteringData = True
                'txtFreq_KeyPress (vbKeyReturn)

                'Set VOLT (Units is always V - equiv to Vpk)
                txtAmpl = CStr(System.Math.Round(CDbl(CStr(Trace.YInc * Trace.YRef)), 5))
                UserEnteringData = True
                'txtAmpl_KeyPress (vbKeyReturn)

                'Set VOLT_OFFS
                txtDcOffs = CStr(Trace.YOrig)
                UserEnteringData = True
                'txtDcOffs_KeyPress (vbKeyReturn)
            Else
                'Has been an error reading the preamble of fileName
            End If
        Else
            'Has been an error reading the preamble of fileName
        End If

        'Read the segment voltage points
        FileGet(FileId, Wave, 257)
        FileClose(FileId)

        DownloadSegmentArray(SegmentName, Wave, sOutputZ, sLoadZ, dAmplitude)
    End Sub

    ''' <summary>
    ''' Downloads a segment of data points to the instrument's segment memory.  If dAmplitude is not specified then the 
    ''' DAC ful scale will be adjusted to the highest peak value of the data in PointList().  This will cause the data 
    ''' to be distributed fully across the dynamic range of the DAC.  If an dAmplitude is specified the DAC full scale 
    ''' will be set to dAmplitude and the data will be distributed accordingly.  If specified, dAmplitude must be 
    ''' greater than or equal to the highest peak value in the array.
    ''' </summary>
    ''' <param name="SegmentName">Name of the segment to be created and stored</param>
    ''' <param name="PointList">List of voltage points to be placed in SegmentName in units of V (Vpk)</param>
    ''' <param name="sOutputZ">Intended output impedance of the instrument</param>
    ''' <param name="sLoadZ">Intended load impedance of the UUT</param>
    ''' <param name="dAmplitude">Intended full scale setting of the DAC.  Specify the same dAmplitude for ExecuteSequence.</param>
    ''' <remarks>DownloadSegmentArray first programs the impedance settings of the 
    ''' instrument so that the DAC codes and DAC full scale will be equal given the expected impedance setup.  The 
    ''' soft impedance setup of the instrument will affect the conversion of voltage points to DAC codes at the time of 
    ''' segment download.  It is suggested that the same impedance setup is used that will be used when the segment is executed.
    ''' 
    ''' If no dAmplitude is specified:
    ''' Any segments loaded with this procedure are scaled amplitudewise so that the points occupy the the full dynamic range 
    ''' of the DAC at least on one side of zero.  However if the waveform is vertically unsymetrical as in the positive peak 
    ''' is a different magnitude than the negative peak than the amplitude will be set to the highest of the two.  There may 
    ''' be a section of the DAC dynamic range that is unused.
    '''  
    ''' If dAmplitude is specified:
    ''' It is recomended to specify dAmplitude if the segment is to be one grouped with other segments in a sequence.  It is 
    ''' recomended that the same dAmplitude be specified for each call of DownLoadSegment Array and also for ExecuteSequence.</remarks>
    Public Sub DownloadSegmentArray(ByRef SegmentName As String, ByRef PointList() As Double,
                                    Optional ByRef sOutputZ As String = "50",
                                    Optional ByRef sLoadZ As String = "Hi-Impedance",
                                    Optional ByRef dAmplitude As Double = -1)
        Dim iScale As Short
        Dim sPointList As String
        Dim lPointCount As Integer
        Dim lMaxIndex As Integer
        Dim lMinIndex As Integer
        Dim lCurrentIndex As Integer
        Dim lHighestIndex As Integer
        Dim dMaxPeak As Double
        Dim nStatus As Integer

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub

        If Len(SegmentName) > 12 Then
            Echo("The segment name cannot be more than 12 characters.")
            Err.Raise(1000, , "The segment name cannot be more than 12 characters.")
            Exit Sub
        ElseIf IsNumeric(Left(SegmentName, 1)) Then
            Echo("The segment name cannot start with a number.")
            Err.Raise(1000, , "The segment name cannot start with a number.")
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
                Echo(sLoadZ & " is not a valid output load impedance. " & vbCrLf & "Select ""50"", ""75"" or ""Hi-Z"".  The default is ""50"".")
                Err.Raise(1000, , sLoadZ & " is not a valid output load impedance. " & vbCrLf & "Select ""50"", ""75"" or ""Hi-Z"".  The default is ""50"".")
                Exit Sub
        End Select

        'Error Check Output Impedance
        Select Case UCase(sOutputZ)
            Case "50", "50 OHM", "50-OHM"
                If sLoadZ = "75" Then
                    'Instrument Manual Chapter 2 Page 70
                    Echo("Output impedance cannot be 50 ohms if load impedance is 75 ohms." & vbCrLf & "Select 75 ohms or infinite.")
                    Err.Raise(1000, , "Output impedance cannot be 50 ohms if load impedance is 75 ohms." & vbCrLf & "Select 75 ohms or infinite.")
                    Exit Sub
                Else
                    sOutputZ = "50"
                End If
            Case "75", "75 OHM", "75-OHM"
                If sLoadZ = "50" Then
                    'Instrument Manual Chapter 2 Page 70
                    Echo("Output impedance cannot be 75 ohms if load impedance is 50 ohms." & vbCrLf & "Select 50 ohms or infinite.")
                    Err.Raise(1000, , "Output impedance cannot be 75 ohms if load impedance is 50 ohms." & vbCrLf & "Select 50 ohms or infinite.")
                    Exit Sub
                Else
                    sOutputZ = "75"
                End If
            Case Else
                Echo(sOutputZ & " is not a valid output load impedance. " & vbCrLf & "Select ""50"" or ""75"" The default is ""50""." & vbCrLf & _
                     "If ""50"" or ""75"" has been selected for load impedance this parameter will have no effect.")
                Err.Raise(1000, ,
                          sOutputZ & " is not a valid output load impedance. " & vbCrLf & "Select ""50"" or ""75"" The default is ""50""." & vbCrLf & _
                          "If ""50"" or ""75"" has been selected for load impedance this parameter will have no effect.")
                Exit Sub
        End Select

        If dAmplitude <> -1 Then
            'Error Check dAmplitude
            If dAmplitude > iScale * DACFullScaleMax Or dAmplitude < iScale * DACFullScaleMin Then
                Echo("Invalid setting for dAmplitude." & vbCrLf & "The acceptable range is: " & CStr(iScale * DACFullScaleMin) & "V to " & CStr(iScale * DACFullScaleMax) & " V")
                Err.Raise(1000, , "Invalid setting for dAmplitude." & vbCrLf & "The acceptable range is: " & CStr(iScale * DACFullScaleMin) & "V to " & CStr(iScale * DACFullScaleMax) & " V")
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
            If System.Math.Abs(PointList(lCurrentIndex)) > System.Math.Abs(PointList(lHighestIndex)) Then
                lHighestIndex = lCurrentIndex
            End If

            sPointList = sPointList & "," & CStr(PointList(lCurrentIndex))
            lCurrentIndex = lCurrentIndex + 1
        End While

        'Error Check PointList
        If PointList(lHighestIndex) > iScale * DCMax Or PointList(lHighestIndex) < iScale * DCMin Then
            Echo("The " & CStr(lHighestIndex + 1 - lMinIndex) & "th data point is out of range for this impedance configuration." & vbCrLf & _
                 "The acceptable range is: " & CStr(iScale * DCMin) & "V to " & CStr(iScale * DCMax) & " V")
            Err.Raise(1000, ,
                      "The " & CStr(lHighestIndex + 1 - lMinIndex) & "th data point is out of range for this impedance configuration." & vbCrLf & _
                      "The acceptable range is: " & CStr(iScale * DCMin) & "V to " & CStr(iScale * DCMax) & " V")
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
            WriteMsg(ARB, "*RST")
            If dAmplitude = -1 Then
                WriteMsg(ARB, "VOLT:LEV:IMM:AMPL " & CStr(dMaxPeak))
            Else
                WriteMsg(ARB, "VOLT:LEV:IMM:AMPL " & CStr(dAmplitude))
            End If

            'Sync Instrument With Desired Impedance Charateristics
            Select Case sLoadZ
                Case "50"
                    WriteMsg(ARB, "OUTP:IMP  50")
                    WriteMsg(ARB, "OUTP:LOAD  50")
                Case "75"
                    WriteMsg(ARB, "OUTP:IMP  75")
                    WriteMsg(ARB, "OUTP:LOAD  75")
                Case "HI-Z"
                    WriteMsg(ARB, "OUTP:IMP  " & sOutputZ)
                    WriteMsg(ARB, "OUTP:LOAD  INF")
            End Select

            WriteMsg(ARB, "LIST:SEGM:SEL " & SegmentName)
            WriteMsg(ARB, "LIST:SEGM:DEF " & CStr(lPointCount))
            If lPointCount > 3000 Then
                nStatus = viSetAttribute(nInstrumentHandle(ARB), VI_ATTR_TMO_VALUE, (lPointCount / 300) * 1000)
            Else
                nStatus = viSetAttribute(nInstrumentHandle(ARB), VI_ATTR_TMO_VALUE, 10000) '10 Sec
            End If
            PrecisionDelay(0.05)
            WriteMsg(ARB, "LIST:SEGM:VOLT " & sPointList)

            nStatus = viSetAttribute(nInstrumentHandle(ARB), VI_ATTR_TMO_VALUE, 10000) '10 Sec
        End If
    End Sub

    ''' <summary>
    ''' Downloads a sequence of segments to the instrument's sequence memory for later execution
    ''' </summary>
    ''' <param name="SequenceName">Name of the sequence to be created and stored</param>
    ''' <param name="SegmentList">List of segment names to be placed in SequenceName in respective order</param>
    ''' <param name="DwellCounts">List of segment dwell counts in respective order</param>
    ''' <remarks>The segment names in SegmentList must allready exist in the instruments segment memory or an error will occur.
    ''' 
    ''' DwellCounts may be a (1x1) array containing [1] if the dwell counts are to all equal 1.
    ''' 
    ''' Any segments loaded with this class module are scaled amplitudewise so that the points occupy the the full dynamic range 
    ''' of the DAC at least on one side of zero.  However if the waveform is vertically unsymetrical as in the MAX is a different 
    ''' magnitude than the MIN there may be a section of the DAC dynamic range that is unused.  This could be compensated for by 
    ''' storing the waveform points with a DC offset and then cancelling this offset out with an equal and opposite DC Offset 
    ''' setting of the instrument.</remarks>
    Public Sub DownloadSequence(ByRef SequenceName As String, ByRef SegmentList() As String, ByRef DwellCounts() As Integer)
        Dim strSegmentList As String
        Dim strDwellCounts As String
        Dim lPointCount As Integer
        Dim lMaxIndex As Integer
        Dim lMinIndex As Integer
        Dim lPointCountDwell As Integer
        Dim lMaxIndexDwell As Integer
        Dim lMinIndexDwell As Integer
        Dim lCurrentIndex As Integer
        Dim SequenceList As String = ""
        Dim DummyError As Integer

        WriteMsg(ARB, "LIST:SSEQ:CAT?")
        DummyError = ReadMsg(ARB, SequenceList)

        If Len(SequenceName) > 12 Then
            Echo("The sequence name cannot be more than 12 characters.")
            Err.Raise(1000, , "The sequence name cannot be more than 12 characters.")
            Exit Sub
        ElseIf IsNumeric(Left(SequenceName, 1)) Then
            Echo("The sequence name cannot start with a number.")
            Err.Raise(1000, , "The sequence name cannot start with a number.")
            Exit Sub
        End If

        If InStr(1, SequenceList, SequenceName) Then
            Echo(SequenceName & " is all ready an existing sequence in the instrument memory.")
            Err.Raise(1000, , SequenceName & " is all ready an existing sequence in the instrument memory.")
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
            Echo("There are an invalid number of dwell counts specified." & vbCrLf & _
                 "There must be the same number of Dwell counts as there are Segments or there must be exactly one dwell count.")
            Err.Raise(1000, ,
                      "There are an invalid number of dwell counts specified." & vbCrLf & _
                      "There must be the same number of Dwell counts as there are Segments or there must be exactly one dwell count.")
            Exit Sub
        End If

        'Create Segment List SCPI argument
        strSegmentList = ""
        For lCurrentIndex = lMinIndex To lMaxIndex
            If lCurrentIndex > lMinIndex Then
                strSegmentList = strSegmentList & ","
            End If

            strSegmentList = strSegmentList & SegmentList(lCurrentIndex)
        Next lCurrentIndex

        'Create Dwell Count List SCPI argument
        strDwellCounts = ""
        For lCurrentIndex = lMinIndexDwell To lMaxIndexDwell
            If lCurrentIndex > lMinIndexDwell Then
                strDwellCounts = strDwellCounts & ","
            End If
            If lPointCountDwell > 1 Then
                strDwellCounts = strDwellCounts & CStr(DwellCounts(lCurrentIndex))
            Else
                strDwellCounts = strDwellCounts & CStr(DwellCounts(lMinIndexDwell))
            End If
        Next lCurrentIndex

        If Not bSimulation Then
            WriteMsg(ARB, "*RST")
            WriteMsg(ARB, "LIST:SSEQ:SEL " & SequenceName)
            WriteMsg(ARB, "LIST:SSEQ:DEF " & CStr(lPointCount))
            WriteMsg(ARB, "LIST:SSEQ:SEQ " & strSegmentList)
            WriteMsg(ARB, "LIST:SSEQ:DWELl:COUN " & strDwellCounts)
        End If
    End Sub

    ''' <summary>
    ''' Executes a sequence previously stored in memory
    ''' </summary>
    ''' <param name="SequenceName">Name of the prviously stored sequence to be executed</param>
    ''' <param name="dAmplitude">Not used, left for compliance with existing TPSs</param>
    ''' <param name="SampleRate">Frequency at which the individual points of segments in SequenceName are to be output</param>
    ''' <remarks></remarks>
    Public Sub ExecuteSequence(ByRef SequenceName As String, ByRef dAmplitude As Double, ByRef SampleRate As Double)
        Dim SequenceList As String = ""
        Dim DummyError As Integer
        Dim FreqMax As Double
        Dim FreqMin As Double

        WriteMsg(ARB, "LIST:SSEQ:CAT?")
        DummyError = ReadMsg(ARB, SequenceList)

        FreqMax = RoscInt1Freq / 2 ' Allow for frequency doubling
        FreqMin = 0
        If SampleRate > FreqMax Or SampleRate < FreqMin Then
            Echo(CStr(SampleRate) & "Hz is out of range for the arbitrary wave function." & vbCrLf & _
                 "The available range is:  " & CStr(FreqMin) & " Hz to " & CStr(FreqMax) & " Hz.")
            Err.Raise(1000, ,
                      CStr(SampleRate) & "Hz is out of range for the arbitrary wave function." & vbCrLf & _
                      "The available range is:  " & CStr(FreqMin) & " Hz to " & CStr(FreqMax) & " Hz.")
            Exit Sub
        End If

        If InStr(1, SequenceList, SequenceName) Then
            'Good
        Else
            Echo(SequenceName & "Has not been stored in the instrument's sequence memory." & vbCrLf & _
                 "Sequences currently stored are " & SequenceList & ".")
            Err.Raise(1000, ,
                      SequenceName & "Has not been stored in the instrument's sequence memory." & vbCrLf & _
                      "Sequences currently stored are " & SequenceList & ".")
            Exit Sub
        End If

        If Not bSimulation Then
            StopAll()
            WriteMsg(ARB, "VOLT:UNIT:VOLT  V")
            WriteMsg(ARB, "VOLT  " & CStr(dAmplitude))
            WriteMsg(ARB, "ROSC:SOUR INT1")
            WriteMsg(ARB, "FREQ:RANG " & CStr(SampleRate))
            WriteMsg(ARB, "FREQ:FIX " & CStr(SampleRate))
            WriteMsg(ARB, "FUNC:SHAP USER")
            WriteMsg(ARB, "FUNC:USER " & SequenceName)
            WriteMsg(ARB, "INIT:IMM")
        End If
    End Sub

    Public Sub ClearAllSegments()
        If Not bSimulation Then
            StopAll()
            WriteMsg(ARB, "LIST:DEL:ALL")
        End If
    End Sub

    Public Sub ClearAllSequences()
        If Not bSimulation Then
            StopAll()
            PrecisionDelay(0.05)
            WriteMsg(ARB, "LIST:SSEQ:DEL:ALL")
        End If
    End Sub

    ''' <summary>
    ''' Returns passed number as numeric string in Engineering notation (every 3rd exponent) with selectable 
    ''' precision along with Unit Of Measure.
    ''' </summary>
    ''' <param name="Number">The number to be formatted</param>
    ''' <param name="SetIdx">The index to a set of parallel global arrays (see below)</param>
    ''' <returns>String representation of a numer in engeering units notation</returns>
    ''' <remarks>EXAMPLES:
    '''    With SetUOM(SetIdx)="Ohm" and SetRes(SetIdx)="D3",
    '''        EngNotate(10987.1, MEAS_RES) returns "10.987 KOhm"
    '''    With SetUOM(SetIdx)="Sec" and SetRes(SetIdx)="D2",
    '''        EngNotate(5.43e-5, MEAS_PER) returns "54.30 uSec"</remarks>
    Private Function EngNotate(ByVal Number As Double, ByRef SetIdx As Short) As String
        Dim Negative, Multiplier, Digits As Short
        Dim ReturnString, Prefix, FormatSpec As String
        Dim NumDigits As Short

        Multiplier = 0 : Negative = False 'Initialize local variables

        If Number < 0 Then ' If negative
            Number = System.Math.Abs(Number) 'Make it positive for now
            Negative = True 'Set flag
        End If

        If Number >= 1000 Then 'For positive exponent
            Do While Number >= 1000 And Multiplier <= 4
                Number = Number / 1000
                Multiplier = Multiplier + 1
            Loop
        ElseIf Number < 1 And Number <> 0 Then  'For negative exponent (but not 0)
            Do While Number < 1 And Multiplier >= -4
                Number = Number * 1000
                Multiplier = Multiplier - 1
            Loop
        End If

        Select Case Multiplier
            Case 4 : Prefix = " T" 'Tetra  E+12
            Case 3 : Prefix = " G" 'Giga   E+09
            Case 2 : Prefix = " M" 'Mega   E+06
            Case 1 : Prefix = " K" 'kilo   E+03
            Case 0 : Prefix = " " '<none> E+00
            Case -1 : Prefix = " m" 'milli  E-03
            Case -2 : Prefix = " " & Chr(181) 'micro  E-06
            Case -3 : Prefix = " n" 'nano   E-09
            Case -4 : Prefix = " p" 'pico   E-12
            Case Else
                Prefix = " "
        End Select

        If Negative Then Number = -Number

        If (Multiplier < -4) Or (Multiplier > 4) Then
            ReturnString = Number.ToString()
        Else
            Number = Val(Str(Number)) ' Clear out very low LSBs from binary math
            If Left(SetRes(SetIdx), 1) = "N" Then
                Digits = Val(Mid(SetRes(SetIdx), 2, 1))
                Prefix = " "
                Number = Number * 10 ^ (Multiplier * 3)
            ElseIf Left(SetRes(SetIdx), 1) = "D" Then  'Digits of resolution
                Digits = Val(Mid(SetRes(SetIdx), 2, 1)) - Int(System.Math.Abs(Number)).ToString().Length
                If Digits < 0 Then Digits = 0
            ElseIf Left(SetRes(SetIdx), 1) = "A" Then  'Absolute Digits of resolution
                Digits = Val(Mid(SetRes(SetIdx), 2, 1)) + (Multiplier * 3)
                If Digits < 0 Then Digits = 0
            Else
                MsgBox("EngNotate detected unspecified resolution for parameter: " & SetRes(SetIdx) & ". Notify maintenance.", MsgBoxStyle.Exclamation)
                Digits = -1
            End If

            FormatSpec = "#,###,###,###,###,##0"
            If Digits > 0 Then
                FormatSpec = FormatSpec & "."
            ElseIf Digits < 0 Then
                FormatSpec = "0.0#######"
            End If

            For NumDigits = 1 To Digits
                FormatSpec = FormatSpec & "0"
            Next NumDigits

            ReturnString = Number.ToString(FormatSpec)
        End If

        'Trim trailing zeros after decimal
        If InStr(ReturnString, ".") Then
            While Right(ReturnString, 1) = "0"
                ReturnString = Left(ReturnString, Len(ReturnString) - 1)
            End While
            If Right(ReturnString, 1) = "." Then
                ReturnString = Left(ReturnString, Len(ReturnString) - 1)
            End If
        End If

        EngNotate = Trim(ReturnString & Prefix & SetUOM(SetIdx))
    End Function

    Private Sub SizeArray(ByRef dArray() As Double, ByRef vUBound As String)
        'DESCRIPTION:
        '   This is a work-around for a bug in the National Instruments ComponentWorks
        '   CWGraph control.  The control won't display one last point for every 250
        '   points and one last point for every 62,500 points in the array being Y-plotted.
        '   Therefore, a number of bogus points must be added to the array so that the
        '   user can see them to modify them if desired.
        '   This subroutine adds the exact number of points (no more/no less) that are
        '   required to make the control appear to work correctly.
        'EXAMPLES:
        '   SizeArray Wave, SetDef(LIST_DEF)
        'PARAMETERS:
        '   dArray():   The double-precision array to be re-sized.
        '   vUBound:    The desired array size to displayed and filled.
        'GLOBAL VARIABLES USED: none

'		Dim iExtraPoints As Short

        'iExtraPoints = (vUBound) \ 250
        'iExtraPoints = iExtraPoints + (vUBound) \ 62500
        'ReDim Preserve dArray(1 To (vUBound + iExtraPoints))

        'This change was undone 4/29/03 - Craig W. - Above three lines commented
        'The header of the .seg file appeared to indicate the correct number of points
        'Appending extra records to the array was yielding incorrect 0 V points because
        'there is no data in the file to fill these records.
        ReDim Preserve dArray(CInt(vUBound) + 1)
    End Sub

    Private Function sIEEEDefinite(ByRef sDirection As String, ByRef sMsg As String) As Short
        'Returns IEEE 488.2 Definite length block formatted string
        'Example: "15HELLO"
        Dim iNumDigits As Short
        Dim iNumChars As Short

        If UCase(sDirection) = "FROM" Then
            iNumDigits = CShort(Mid(sMsg, 2, 1))
            iNumChars = CShort(Mid(sMsg, 3, iNumDigits))
            sIEEEDefinite = CShort(Mid(sMsg, 3 + iNumDigits, iNumChars))
        Else
            iNumChars = Len(sMsg)
            iNumDigits = Len(CStr(iNumChars))
            sIEEEDefinite = CShort("#" & CStr(iNumDigits) & CStr(iNumChars) & sMsg)
        End If
    End Function
End Class