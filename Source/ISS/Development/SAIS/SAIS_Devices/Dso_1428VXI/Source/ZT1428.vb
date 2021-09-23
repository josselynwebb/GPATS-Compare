Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Friend Class frmZT1428
    Inherits System.Windows.Forms.Form

    Dim dCursorX As Double
    Dim dCursorY As Double
    'measurement constants sent back from instr.
    Const VDCR As Short = 1
    Const VACR As Short = 2
    Const PER As Short = 3
    Const FREQ As Short = 4
    Const PWID As Short = 5
    Const NWID As Short = 6
    Const DUT As Short = 7
    Const DEL As Short = 8
    Const RIS As Short = 9
    Const FALL As Short = 10
    Const PRE As Short = 11
    Const OVER As Short = 12
    Const VPP As Short = 13
    Const VMIN As Short = 14
    Const VMAX As Short = 15
    Const VAV As Short = 16
    Const VAMP As Short = 17
    Const VBAS As Short = 18
    Const VTOP As Short = 19

    Private _updatingFromFile As Boolean
    Property UpdatingFromFile() As Boolean
        Get
            Return _updatingFromFile
        End Get
        Set(value As Boolean)
            _updatingFromFile = value
            If value Then
                Me.Cursor = Cursors.WaitCursor
            Else
                Me.Cursor = Cursors.Default
            End If
        End Set
    End Property



    
    Private Sub cboHorizRange_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboHorizRange.SelectedIndexChanged

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode
        SetHScale()
        If DiskWaveformFlag = True Then
            DiskWaveformFlag = False
            Me._chkDisplayTrace_3.Checked = False
            Me.chkContinuous.Checked = False
        End If

    End Sub

    
    Private Sub cboMathFunction_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboMathFunction.SelectedIndexChanged

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode
        SetMathFunction(True)

    End Sub

    
    Private Sub cboMathSourceA_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboMathSourceA.SelectedIndexChanged

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode
        SetMathFunction(True)

    End Sub

    
    Private Sub cboMathSourceB_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboMathSourceB.SelectedIndexChanged

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode
        SetMathFunction(True)

    End Sub

    
    Private Sub cboMeasSour_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboMeasSour.SelectedIndexChanged

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode
        SelectOption(MEAS_SOUR, (Me.cboMeasSour.SelectedIndex))

    End Sub

    
    Private Sub cboSampleType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboSampleType.SelectedIndexChanged

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode
        SelectOption(SAMPLE_MODE, (Me.cboSampleType.SelectedIndex))

    End Sub

    
    Private Sub cboStartSource_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboStartSource.SelectedIndexChanged

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode
        ChangeDelayMeasParam(START_SOURCE, (Me.cboStartSource.SelectedIndex))

    End Sub

    
    Private Sub cboStopSource_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboStopSource.SelectedIndexChanged

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode
        ChangeDelayMeasParam(STOP_SOURCE, (Me.cboStopSource.SelectedIndex))

    End Sub



    
    Private Sub cboTraceColor_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboTraceColor.SelectedIndexChanged

        ChangeColorBox()

    End Sub


    
    Private Sub cboTraceThickness_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboTraceThickness.SelectedIndexChanged

        ChangeTraceThickness()

    End Sub


    
    Private Sub cboTrigDelaySour_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboTrigDelaySour.SelectedIndexChanged

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode
        SelectOption(TRIG_DEL_SOUR, (Me.cboTrigDelaySour.SelectedIndex))

    End Sub

    
    Private Sub cboTrigSour_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboTrigSour.SelectedIndexChanged

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode
        SelectOption(TRIG_SOUR, (Me.cboTrigSour.SelectedIndex))

    End Sub

    
    Private Sub cboVertRange_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboVertRange.SelectedIndexChanged, _cboVertRange_0.SelectedIndexChanged, _cboVertRange_1.SelectedIndexChanged

    If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode

    Dim Index As Short
    Dim tempComboBox As ComboBox = eventSender

    If (eventSender.Name = "_cboVertRange_0") Then
        Index = 0
    ElseIf (eventSender.Name = "_cboVertRange_1") Then
        Index = 1
    End If


    SetVScaleGraph((Index))

    End Sub
    
    Private Sub cboAcqMode_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboAcqMode.SelectedIndexChanged

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode
        SelectOption(ACQ_MODE, (Me.cboAcqMode.SelectedIndex))

    End Sub


    Private Sub cmdAbout_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAbout.Click

        frmAbout.cmdOK.Visible = True
        frmAbout.ShowDialog()

    End Sub

    Public Function Build_Atlas() As String

        Dim sTestString As String

        Select Case CurrentMeas
            Case 1 'DC RMS Voltage
                sTestString = "MEASURE,(VOLTAGE INTO MEASUREMENT), DC SIGNAL," & vbCrLf & "VOLTAGE MAX " & MaxVoltRange(MinVoltage()) & vbCrLf

                'test-eq-impedance
                sTestString = sTestString & BuildTestEqImp()

                'Max-time/Strobe to event
                sTestString = sTestString & BuildMaxTimeStrobe() & " CNX VIA $"

            Case 2 'AC RMS Voltage
                sTestString = "MEASURE,(AV-VOLTAGE INTO MEASUREMENT), AC SIGNAL," & vbCrLf & "AV-VOLTAGE MAX " & "<tbd>" & vbCrLf

                'voltage
                sTestString = sTestString & "VOLTAGE " & MinVoltage() & "," & " VOLTAGE-PP " & MaxVoltRange(MinVoltage()) & vbCrLf
                'DC-offset
                sTestString = sTestString & BuildDCOffset("FREQ")
                'freq window
                sTestString = sTestString & BuildFreqWin()
                'test-eq-impedance
                sTestString = sTestString & BuildTestEqImp()
                'Max-time/Strobe to event
                sTestString = sTestString & BuildMaxTimeStrobe()
                'sample count
                sTestString = sTestString & BuildTrigger("SAMPLE")


                sTestString = sTestString & "CNX VIA $"

            Case 3 'Period
                sTestString = "MEASURE,(PERIOD INTO MEASUREMENT), AC SIGNAL," & vbCrLf & "PERIOD MAX " & PeriodRange() & "," & vbCrLf

                'voltage
                sTestString = sTestString & "VOLTAGE " & MinVoltage() & "," & " VOLTAGE-PP " & MaxVoltRange(MinVoltage()) & vbCrLf
                'DC-offset
                sTestString = sTestString & BuildDCOffset("NONE")
                'freq window
                sTestString = sTestString & BuildFreqWin()
                'test-eq-impedance
                sTestString = sTestString & BuildTestEqImp()
                'Max-time/Strobe to event
                sTestString = sTestString & BuildMaxTimeStrobe() & vbCrLf
                'trigger
                sTestString = sTestString & BuildTrigger("NONE")

                sTestString = sTestString & "CNX VIA $"

            Case 4 'Frequency
                sTestString = "MEASURE,(FREQ INTO MEASUREMENT), AC SIGNAL," & vbCrLf & "FREQ MIN " & MinHertz(PeriodRange()) & "," & vbCrLf

                'voltage
                sTestString = sTestString & "VOLTAGE " & MinVoltage() & "," & " VOLTAGE-PP " & MaxVoltRange(MinVoltage()) & vbCrLf
                'DC-offset
                sTestString = sTestString & BuildDCOffset("NONE")
                'freq window
                sTestString = sTestString & BuildFreqWin()
                'test-eq-impedance
                sTestString = sTestString & BuildTestEqImp()
                'Max-time/Strobe to event
                sTestString = sTestString & BuildMaxTimeStrobe() & vbCrLf
                'trigger
                sTestString = sTestString & BuildTrigger("NONE")

                sTestString = sTestString & "CNX VIA $"

            Case 5 'Positive Pulse Width
                sTestString = "MEASURE,(POS-PULSE-WIDTH INTO MEASUREMENT), PULSED DC," & vbCrLf & "POS-PULSE-WIDTH MAX " & MaxVoltRange(MinVoltage()) & vbCrLf

                'voltage
                sTestString = sTestString & "VOLTAGE " & MinVoltage() & "," & " VOLTAGE-PP " & MaxVoltRange(MinVoltage()) & vbCrLf
                'DC-offset
                sTestString = sTestString & BuildDCOffset("PRF")
                'test-eq-impedance
                sTestString = sTestString & BuildTestEqImp()
                'Max-time/Strobe to event
                sTestString = sTestString & BuildMaxTimeStrobe() & vbCrLf
                'sample count
                sTestString = sTestString & BuildTrigger("SAMPLE")

                sTestString = sTestString & "CNX VIA $"

            Case 6 'Negative Pulse Width
                sTestString = "MEASURE,(NEG-PULSE-WIDTH INTO MEASUREMENT), PULSED DC," & vbCrLf & "NEG-PULSE-WIDTH MAX " & MaxVoltRange(MinVoltage()) & vbCrLf
                'voltage
                sTestString = sTestString & "VOLTAGE " & MinVoltage() & "," & " VOLTAGE-PP " & MaxVoltRange(MinVoltage()) & vbCrLf
                'DC-offset
                sTestString = sTestString & BuildDCOffset("PRF")
                'test-eq-impedance
                sTestString = sTestString & BuildTestEqImp()
                'Max-time/Strobe to event
                sTestString = sTestString & BuildMaxTimeStrobe() & vbCrLf
                'sample count
                sTestString = sTestString & BuildTrigger("SAMPLE")

                sTestString = sTestString & "CNX VIA $"

            Case 7 'Duty Cycle
            Case 8 'Delay
            Case 9 'Rise Time
                sTestString = "MEASURE,(RISE-TIME INTO MEASUREMENT), RAMP SIGNAL," & vbCrLf & "RISE-TIME MAX " & PeriodRange() & vbCrLf

                'voltage
                sTestString = sTestString & "VOLTAGE " & MinVoltage() & "," & " VOLTAGE-PP " & MaxVoltRange(MinVoltage()) & vbCrLf
                'DC-offset
                sTestString = sTestString & BuildDCOffset("NONE")
                'test-eq-impedance
                sTestString = sTestString & BuildTestEqImp()
                'Max-time/Strobe to event
                sTestString = sTestString & BuildMaxTimeStrobe() & vbCrLf
                'sample count
                sTestString = sTestString & BuildTrigger("SAMPLE")


                sTestString = sTestString & "CNX VIA $"

            Case 10 'Fall Time
                sTestString = "MEASURE,(FALL-TIME INTO MEASUREMENT), RAMP SIGNAL," & vbCrLf & "FALL-TIME MAX " & PeriodRange() & vbCrLf

                'voltage
                sTestString = sTestString & "VOLTAGE " & MinVoltage() & "," & " VOLTAGE-PP " & MaxVoltRange(MinVoltage()) & vbCrLf
                'DC-offset
                sTestString = sTestString & BuildDCOffset("NONE")
                'test-eq-impedance
                sTestString = sTestString & BuildTestEqImp()
                'Max-time/Strobe to event
                sTestString = sTestString & BuildMaxTimeStrobe() & vbCrLf
                'sample count
                sTestString = sTestString & BuildTrigger("SAMPLE")


                sTestString = sTestString & "CNX VIA $"
            Case 11 'Preshoot
                sTestString = "MEASURE,(PRESHOOT INTO MEASUREMENT), PULSED DC," & vbCrLf & "PRESHOOT MAX " & "<tbd>" & vbCrLf
                'voltage
                sTestString = sTestString & "VOLTAGE " & MinVoltage() & "," & " VOLTAGE-PP " & MaxVoltRange(MinVoltage()) & vbCrLf
                'DC-offset
                sTestString = sTestString & BuildDCOffset("PRF")
                'test-eq-impedance
                sTestString = sTestString & BuildTestEqImp()
                'Max-time/Strobe to event
                sTestString = sTestString & BuildMaxTimeStrobe() & vbCrLf
                'sample count
                sTestString = sTestString & BuildTrigger("SAMPLE")

                sTestString = sTestString & "CNX VIA $"

            Case 12 'Overshoot
                sTestString = "MEASURE,(OVERSHOOT INTO MEASUREMENT), PULSED DC," & vbCrLf & "OVERSHOOT MAX " & "<tbd>" & vbCrLf
                'voltage
                sTestString = sTestString & "VOLTAGE " & MinVoltage() & "," & " VOLTAGE-PP " & MaxVoltRange(MinVoltage()) & vbCrLf
                'DC-offset
                sTestString = sTestString & BuildDCOffset("PRF")
                'test-eq-impedance
                sTestString = sTestString & BuildTestEqImp()
                'Max-time/Strobe to event
                sTestString = sTestString & BuildMaxTimeStrobe() & vbCrLf
                'sample count
                sTestString = sTestString & BuildTrigger("SAMPLE")

                sTestString = sTestString & "CNX VIA $"

            Case 13 'Voltage P-P
                sTestString = "MEASURE,(VOLTAGE-PP INTO MEASUREMENT), AC SIGNAL," & vbCrLf & "VOLTAGE-PP MIN " & MinVoltage() & vbCrLf

                'voltage
                sTestString = sTestString & "VOLTAGE " & MinVoltage() & "," & vbCrLf
                'DC-offset
                sTestString = sTestString & BuildDCOffset("FREQ")
                'freq window
                sTestString = sTestString & BuildFreqWin()
                'test-eq-impedance
                sTestString = sTestString & BuildTestEqImp()
                'Max-time/Strobe to event
                sTestString = sTestString & BuildMaxTimeStrobe() & vbCrLf
                'trigger
                sTestString = sTestString & BuildTrigger("NONE")

                sTestString = sTestString & "CNX VIA $"

            Case 14 'Min Voltage
            Case 15 'Max Voltage
            Case 16 'Avg Voltage
            Case 17 'Voltage Amplitude
            Case 18 'Voltage Base
            Case 19 'Voltage Top
            Case 20 'Measurement Off
        End Select

        '   Return ATLAS Statement
        Build_Atlas = sTestString

    End Function

    Private Function MinVoltage() As String
        Dim nIn As Short

        'Determine Min Voltage based on the range set (Probe Attenuation affects range)
        If cboMeasSour.Text = "Input 1" Then
            nIn = 0
        ElseIf cboMeasSour.Text = "Input 2" Then
            nIn = 1
        End If

        MinVoltage = VB.Left(cboVertRange(nIn).Text, InStr(cboVertRange(nIn).Text, "/") - 1)

    End Function
    Private Function MaxVoltRange(ByRef sVDiv As String) As String
        Dim nValue As Short
        Dim sRange As String

        'Determine Max Voltage range by multiplying the div voltage by 10 divs
        nValue = CShort(VB.Left(sVDiv, InStr(sVDiv, " ") - 1)) * 10
        sRange = Mid(sVDiv, InStr(sVDiv, " ") + 1, 2)

        If sRange = "µV" Then
            nValue = nValue / 1000
            sRange = CStr(nValue) & " mV"
        ElseIf sRange = "mV" Then
            If nValue < 1000 Then
                sRange = CStr(nValue) & " mV"
            Else
                nValue = nValue / 1000
                sRange = CStr(nValue) & " V"
            End If
        ElseIf sRange = "V" Then
            If nValue < 1000 Then
                sRange = CStr(nValue) & " V"
            Else
                nValue = nValue / 1000
                sRange = CStr(nValue) & " KV"
            End If
        ElseIf sRange = "KV" Then
            sRange = CStr(nValue) & " KV"
        End If

        MaxVoltRange = sRange

    End Function

    Private Function PeriodRange() As String
        Dim nValue As Short
        Dim sRange As String
        'Determine Max Voltage range by multiplying the div voltage by 10 divs
        nValue = CShort(VB.Left(cboHorizRange.Text, InStr(cboHorizRange.Text, " ") - 1)) * 10
        sRange = Mid(cboHorizRange.Text, InStr(cboHorizRange.Text, " ") + 1, 2)

        If sRange = "nS" Then
            If nValue < 1000 Then
                sRange = CStr(nValue) & " nSEC"
            Else
                nValue = nValue / 1000
                sRange = CStr(nValue) & " uSEC"
            End If
        ElseIf sRange = "uS" Then
            If nValue < 1000 Then
                sRange = CStr(nValue) & " uSEC"
            Else
                nValue = nValue / 1000
                sRange = CStr(nValue) & " mSEC"
            End If
        ElseIf sRange = "mS" Then
            If nValue < 1000 Then
                sRange = CStr(nValue) & " mSEC"
            Else
                nValue = nValue / 1000
                sRange = CStr(nValue) & " SEC"
            End If
        ElseIf sRange = "S/" Then
            sRange = CStr(nValue) & " SEC"
        End If

        PeriodRange = sRange

    End Function

    Private Function MinHertz(ByRef sTRange As String) As String
        Dim fValue As Single
        Dim sRange As String
        'Determine Max Voltage range by multiplying the div voltage by 10 divs
        fValue = CShort(VB.Left(sTRange, InStr(sTRange, " ") - 1)) * 10
        sRange = Mid(sTRange, InStr(sTRange, " ") + 1, 4)

        If sRange = "nSEC" Then
            fValue = fValue / 1000000000
            sRange = CStr(fValue) & " HZ,"
        ElseIf sRange = "uSEC" Then
            fValue = fValue / 1000000
            sRange = CStr(fValue) & " HZ,"
        ElseIf sRange = "mSEC" Then
            fValue = fValue / 1000
            sRange = CStr(fValue) & " HZ,"
        ElseIf sRange = "SEC" Then
            If fValue < 1000 Then
                sRange = CStr(fValue) & " HZ,"
            Else
                fValue = fValue / 1000
                sRange = CStr(fValue) & " KHZ,"
            End If
        End If

        MinHertz = sRange

    End Function

    Private Function BuildDCOffset(ByRef sFreq As String) As String
        Dim sTestString As String

        If cboMeasSour.Text = "Input 1" Then
            If Not txtVOffset1.Text = "0.0 V" Then
                sTestString = "DC-OFFSET " & txtVOffset1.Text & ", "
            End If
        ElseIf cboMeasSour.Text = "Input 2" Then
            If Not txtVOffset2.Text = "0.0 V" Then
                sTestString = "DC-OFFSET " & txtVOffset2.Text & ", "
            End If
        End If

        If sFreq = "NONE" Then
            If Not sTestString = "" Then
                sTestString = sTestString & vbCrLf
            End If
        ElseIf sFreq = "FREQ" Then
            sTestString = sTestString & "FREQ " & MinHertz(PeriodRange()) & vbCrLf
        ElseIf sFreq = "PRF" Then
            sTestString = sTestString & "PRF " & MinHertz(PeriodRange()) & vbCrLf
        End If

        BuildDCOffset = sTestString

    End Function

    Private Function BuildFreqWin() As String
        Dim sTestString As String
        If cboMeasSour.Text = "Input 1" Then
            If chkLFReject1.Checked = True And chkHFReject1.Checked = True Then
                sTestString = "FREQ-WINDOW RANGE 450 HZ TO 30 MHZ," & vbCrLf
            ElseIf chkLFReject1.Checked = True Then
                sTestString = "FREQ-WINDOW MAX < 30 MHZ," & vbCrLf
            ElseIf chkHFReject1.Checked = True Then
                sTestString = "FREQ-WINDOW MIN > 450 HZ," & vbCrLf
            End If
        ElseIf cboMeasSour.Text = "Input 2" Then
            If chkLFReject2.Checked = True And chkHFReject2.Checked = True Then
                sTestString = "FREQ-WINDOW RANGE 450 HZ TO 30 MHZ," & vbCrLf
            ElseIf chkLFReject2.Checked = True Then
                sTestString = "FREQ-WINDOW MAX < 30 MHZ," & vbCrLf
            ElseIf chkHFReject2.Checked = True Then
                sTestString = "FREQ-WINDOW MIN > 450 HZ," & vbCrLf
            End If
        End If

        BuildFreqWin = sTestString

    End Function

    Private Function BuildTestEqImp() As String
        Dim sTestString As String

        sTestString = "TEST-EQUIP-IMP "
        If cboStartSource.Text = "Input 1" Then

            If _optCoupling1_0.Checked = True Then
                sTestString = sTestString & "1 MOHM COUPLING DC," & vbCrLf
            ElseIf _optCoupling1_1.Checked = True Then
                sTestString = sTestString & "50 OHM COUPLING DC," & vbCrLf
            ElseIf _optCoupling1_2.Checked = True Then
                sTestString = sTestString & "1 MOHM COUPLING AC," & vbCrLf
            End If
        ElseIf cboStartSource.Text = "Input 2" Then

            If _optCoupling2_0.Checked = True Then
                sTestString = sTestString & "1 MOHM COUPLING DC," & vbCrLf
            ElseIf _optCoupling2_1.Checked = True Then
                sTestString = sTestString & "50 OHM COUPLING DC," & vbCrLf
            ElseIf _optCoupling2_2.Checked = True Then
                sTestString = sTestString & "1 MOHM COUPLING AC," & vbCrLf
            End If

        End If

        BuildTestEqImp = sTestString
    End Function

    Private Function BuildMaxTimeStrobe() As String
        Dim sTestString As String

        'max time
        If cboAcqMode.Text = "Triggered" Then
            sTestString = sTestString & "MAX-TIME " & "<dec value>" & "," & vbCrLf
        End If

        'strobe to event
        If InStr(cboTrigSour.Text, "Input") = 0 Then
            sTestString = sTestString & "STROBE-TO-EVENT" & "<event lable>" & " MAX-TIME " & "<dec value>" & " SEC," 'do not add newline for DC Signal

            If Not CurrentMeas = 1 Then
                sTestString = sTestString & vbCrLf
            End If
        End If

        BuildMaxTimeStrobe = sTestString
    End Function

    Private Function BuildTrigger(ByRef sSamp As String) As String
        Dim sTestString As String

        'sample count
        If sSamp = "SAMPLE" Then
            sTestString = sTestString & "SAMPLE-COUNT " & txtAvgCount.Text & ", "
        End If
        'trigger
        If Not txtTrigLevel.Text = "0.00 V" Then
            sTestString = sTestString & "TRIG-LEVEL " & txtTrigLevel.Text & ","
            sTestString = sTestString & " TRIG-SLOPE "
            If _optTrigSlope_0.Checked = True Then
                sTestString = sTestString & "POS," & vbCrLf
            Else
                sTestString = sTestString & "NEC," & vbCrLf
            End If
        End If

        If sTestString <> "" And VB.Right(sTestString, 1) = " " Then
            sTestString = sTestString & vbCrLf
        End If

        BuildTrigger = sTestString

    End Function


    Private Sub cmdAutoscale_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAutoscale.Click

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode
        AutoScaleScope()
        If _optDataPoints_1.Checked = True Then WriteMsg("ACQ:POIN 8000")
    End Sub

    Public Sub ConfigGetCurrent()

        Dim sInstrumentCmds As String
        Dim sReadBuffer As String
        Dim sFunc As String

        Dim Msg As String 'Used in error messages

        Dim sChan(4) As String
        Dim i As Short
        If LiveMode Then 'use Not to use simulator

            sReadBuffer = ""
            'Allow use of ActivateControl
            sTIP_Mode = "GET CURR CONFIG"

            On Error GoTo ErrorHandle

            'Verify Programming Language
            WriteMsg(("SYST:LANG?"))
            sReadBuffer = ReadMsg()

            'TIR 3.0.0.0010-0010
            If sReadBuffer <> "COMP" Then
                'MsgBox("Instrument Language is not set to COMP", MsgBoxStyle.Critical)
                Exit Sub
            End If

            '       *********
            '       Get Confuration (Non COMP command)
            '       *********
            'JRC 05-02-06
            'sInstrumentCmds = "MEAS~FUNC?"
            'WriteMsg$ (sInstrumentCmds)

            'DC_RMS,AC_RMS,PERIOD,FREQUENCY,POS_PWIDTH,NEG_PWIDTH,DUTY_CYCLE
            'DELAY_MEAS,RISE_TIME,FALL_TIME,PRE_SHOOT,OVR_SHOOT,V_PP,V_MIN
            'V_MAX,V_AVERAGE,V_AMPL,V_BASE,V_TOP,MEAS_OFF
            '##########################################
            'Action Required: Remove debug code
            'Fill return value selections
            '    '        gcolCmds..Add "Simulated response for: " & sInstrumentCmds
            '    '        gcolCmds..Add "1": gcolCmds.Add "2"
            '    '        gcolCmds..Add "3": gcolCmds.Add "4"
            '    '        gcolCmds..Add "5": gcolCmds.Add "6"
            '    '        gcolCmds..Add "7": gcolCmds.Add "8"
            '    '        gcolCmds..Add "9": gcolCmds.Add "10"
            '    '        gcolCmds..Add "11": gcolCmds.Add "12"
            '    '        gcolCmds..Add "13": gcolCmds.Add "14"
            '    '        gcolCmds..Add "15": gcolCmds.Add "16"
            '    '        gcolCmds..Add "17": gcolCmds.Add "18"
            '    '        gcolCmds..Add "19": gcolCmds.Add "20"
            '##########################################

            'Retrieve instrument output buffer
            'JRC 05-02-06
            'sReadBuffer = ReadMsg$()

            'NOTE: if result is not a value from 1-20 the action is needed
            'JRC 05-02-06
            'DscopeMain.ActivateControl ("MEAS~FUNC " & sReadBuffer)

            '       *********
            '       Get Measure Source
            '       *********
            sInstrumentCmds = "MEAS:SOUR?"
            WriteMsg((sInstrumentCmds))

            '##########################################
            'Action Required: Remove debug code
            'Fill return value selections
            '    '        gcolCmds..Add "Simulated response for: " & sInstrumentCmds
            '    '        gcolCmds..Add "CHAN1": gcolCmds.Add "CHAN2"
            '    '        gcolCmds..Add "WMEM1": gcolCmds.Add "FUNC1"
            '    '        gcolCmds..Add "CHAN1,CHAN2": gcolCmds.Add "CHAN2,FUNC1" 'Delay
            '##########################################

            'Retrieve instrument output buffer
            sReadBuffer = ReadMsg()

            DscopeMain.ActivateControl((":MEAS:SOUR " & sReadBuffer))

            'Get Delay Measurements
            GetDelayMeas()

            '---------------------------Channels----------------------------
            GetChannel("1")
            GetChannel("2")
            '---------------------------TimeBase----------------------------
            GetTimeBase()
            '----------------------------Trigger----------------------------
            GetTrigSourSlop("")
            GetTrigger()
            '----------------------------Options----------------------------

            '       *********
            '       Get measurment results (this will send back what measurment(s) was performed it is set up for)
            '       all we can do is pick the last measurment.  There is not a real command just for the measurment
            '       function
            '       *********
            sInstrumentCmds = "MEAS:RES?"
            WriteMsg((sInstrumentCmds))

            'Retrieve instrument output buffer
            sReadBuffer = ReadMsg()

            sFunc = VB.Right(sReadBuffer, 14)
            If VB.Left(sFunc, 1) = ";" Then
                sFunc = Mid(sFunc, 1, 5)
                sFunc = VB.Right(sFunc, 4)
            Else
                sFunc = VB.Left(sFunc, 4)
            End If
            sFunc = Trim(sFunc)

            'get the value and send it
            Select Case sFunc
                Case "VDCR"
                    sFunc = "1"
                Case "VACR"
                    sFunc = "2"
                Case "PER"
                    sFunc = "3"
                Case "FREQ"
                    sFunc = "4"
                Case "PWID"
                    sFunc = "5"
                Case "NWID"
                    sFunc = "6"
                Case "DUT"
                    sFunc = "7"
                Case "DEL"
                    sFunc = "8"
                Case "RIS"
                    sFunc = "9"
                Case "FALL"
                    sFunc = "10"
                Case "PRE"
                    sFunc = "11"
                Case "OVER"
                    sFunc = "12"
                Case "VPP"
                    sFunc = "13"
                Case "VMIN"
                    sFunc = "14"
                Case "VMAX"
                    sFunc = "15"
                Case "VAV"
                    sFunc = "16"
                Case "VAMP"
                    sFunc = "17"
                Case "VBAS"
                    sFunc = "18"
                Case "VTOP"
                    sFunc = "19"
                Case Else : sFunc = "20"
            End Select

            DscopeMain.ActivateControl(("MEAS~FUNC " & sFunc))


            '       *********
            '       Get Ext Trigger Coupling
            '       *********
            sInstrumentCmds = "TRIG:COUP?"
            WriteMsg((sInstrumentCmds))

            '##########################################
            'Action Required: Remove debug code
            'Fill return value selections
            '    '        gcolCmds..Add "Simulated response for: " & sInstrumentCmds
            '    '        gcolCmds..Add "DC": gcolCmds.Add "DCF"
            '##########################################

            'Retrieve instrument output buffer
            sReadBuffer = ReadMsg()

            DscopeMain.ActivateControl((":TRIG:COUP " & sReadBuffer))

            '**********************
            ' Check each output trigger enable
            GetOutputState(":EXT")
            GetOutputState(":ECLT0")
            GetOutputState(":ECLT1")

            '       *********
            '       Get Probe Comp
            '       *********
            ' may need to use SUMM:QUES:CAL:PROB:ENAB?
            sInstrumentCmds = "BNC?"
            WriteMsg((sInstrumentCmds))

            '##########################################
            'Action Required: Remove debug code
            'Fill return value selections
            '    '        gcolCmds..Add "Simulated response for: " & sInstrumentCmds
            '    '        gcolCmds..Add "PROB": gcolCmds.Add "TRIG"
            '    '        gcolCmds..Add "DCCAL": gcolCmds.Add "SCL"
            '    '        gcolCmds..Add "ZVOL": gcolCmds.Add "FVOL"
            '##########################################

            'Retrieve instrument output buffer
            sReadBuffer = ReadMsg()

            If VB.Left(sReadBuffer, 4) = "PROB" Then
                ribCompON.Checked = True
                imgProbeComp.Visible = True
            Else
                ribCompON.Checked = False
                imgProbeComp.Visible = False
            End If

            '*******************************
            ' Update enable status
            '*******************************

            sChan(0) = "CHAN1"
            sChan(1) = "CHAN2"
            sChan(2) = "FUNC1"
            sChan(3) = "WMEM1"


            Dim newCheckBox As CheckBox
            If (bIsHPE1428A = False) Then  'causes visa timeout on the hpe1428
                 For i = 0 To 3
                    sInstrumentCmds = "STAT?" & sChan(i)
                    WriteMsg((sInstrumentCmds))

                    '##########################################
                    'Action Required: Remove debug code
                    'Fill return value selections
                    '    '        gcolCmds..Add "Simulated response for: " & sInstrumentCmds
                    '    '        gcolCmds..Add "0": gcolCmds.Add "1"
                    '##########################################

                    'Retrieve instrument output buffer
                    If Not bInstrumentMode Then
                        sReadBuffer = ReadMsg()
                    Else
                        If sChan(i) = "CHAN1" Or sChan(i) = "CHAN2" Then
                            sReadBuffer = "1"
                        Else
                            sReadBuffer = "0"
                        End If
                    End If

                    Select Case i
                        Case 0
                            newCheckBox = Me._chkDisplayTrace_0
                        Case 1
                            newCheckBox = Me._chkDisplayTrace_1
                        Case 2
                            newCheckBox = Me._chkDisplayTrace_2
                        Case 3
                            newCheckBox = Me._chkDisplayTrace_3
                    End Select

                    If sReadBuffer = "1" Then
                        newCheckBox.Checked = True
                    Else
                        newCheckBox.Checked = False
                    End If
                Next i
            End If
           
            'call this function to fill the text boxes
            FillTextBoxes()

            '****************************************************************
            'clean-up
            sTIP_Mode = ""

        End If
        Exit Sub

ErrorHandle:
        If Err.Number = 13 Then
            Msg = "Type Mismatch: Data received is not what was expected"
            MsgBox(Msg, MsgBoxStyle.Exclamation)
            'Type Mismatch
            Resume Next
        Else
            ' Display unanticipated error message.
            Msg = "Unanticipated error " & Err.Number & ": " & Err.Description
            MsgBox(Msg, MsgBoxStyle.Critical)
            sTIP_CMDstring = ""
            sTIP_Mode = ""
        End If

    End Sub

    Private Sub GetDelayMeas()

        Dim sInstrumentCmds As String
        Dim sReadBuffer As String

        If DscopeMain.CurrentMeas = DELAY_MEAS Then

            '*********
            'Get Upper measurement threshold level.
            '*********
            sInstrumentCmds = "MEAS:UNIT?"
            WriteMsg((sInstrumentCmds))

            '##########################################
            'Action Required: Remove debug code
            'Fill return value selections
            '    '        gcolCmds..Add "Simulated response for: " & sInstrumentCmds
            '    '        gcolCmds..Add "VOLT": gcolCmds.Add "PERC"
            '##########################################

            'Retrieve instrument output buffer
            sReadBuffer = ReadMsg()

            DscopeMain.ActivateControl((":MEAS:UNIT " & sReadBuffer))


            '*********
            'Get Upper measurement threshold level.
            '*********
            sInstrumentCmds = "MEAS:UPP?"
            WriteMsg((sInstrumentCmds))

            '##########################################
            'Action Required: Remove debug code
            'Fill return value selections
            '    '        gcolCmds..Add "Simulated response for: " & sInstrumentCmds
            '    '        gcolCmds..Add "-250000": gcolCmds.Add "250000"
            '    '        gcolCmds..Add "-500": gcolCmds.Add "4000"
            '    '        gcolCmds..Add "-3.50": gcolCmds.Add "55.00"
            '    '        gcolCmds..Add "-25.00": gcolCmds.Add "125.00"
            '##########################################

            'Retrieve instrument output buffer
            sReadBuffer = ReadMsg()

            DscopeMain.ActivateControl((":MEAS:UPP " & sReadBuffer))

            '*********
            'Get Lower measurement threshold level.
            '*********
            sInstrumentCmds = "MEAS:LOW?"
            WriteMsg((sInstrumentCmds))

            '##########################################
            'Action Required: Remove debug code
            'Fill return value selections
            '    '        gcolCmds..Add "Simulated response for: " & sInstrumentCmds
            '    '        gcolCmds..Add "-250000": gcolCmds.Add "250000"
            '    '        gcolCmds..Add "-500": gcolCmds.Add "4000"
            '    '        gcolCmds..Add "-3.50": gcolCmds.Add "55.00"
            '    '        gcolCmds..Add "-25.00": gcolCmds.Add "125.00"
            '##########################################

            'Retrieve instrument output buffer
            sReadBuffer = ReadMsg()

            DscopeMain.ActivateControl((":MEAS:LOW " & sReadBuffer))


            '*********
            'Get Delay Measurement
            '*********
            sInstrumentCmds = "MEAS:DEF?DEL"
            WriteMsg((sInstrumentCmds))

            '##########################################
            'Action Required: Remove debug code
            'Fill return value selections
            '    '        gcolCmds..Add "Simulated response for: " & sInstrumentCmds
            '    '        gcolCmds..Add "DEL,NEG,20,LOW,POS,50,UPP"
            '    '        gcolCmds..Add "DEL,POS,1,LOW,NEG,100,UPP"
            '    '        gcolCmds..Add "DEL,NEG,8,MIDD,POS,15,UPP"
            '    '        gcolCmds..Add "DEL,NEG,5,LOW,POS,50,MIDD"
            '##########################################

            'Retrieve instrument output buffer
            sReadBuffer = ReadMsg()

            DscopeMain.ActivateControl((":MEAS:DEF " & sReadBuffer))

        End If
    End Sub

    Private Sub GetChannel(ByRef sChan As String)

        Dim sInstrumentCmds As String
        Dim sReadBuffer As String

        '*********
        'Get Probe Attenuation
        '*********
        sInstrumentCmds = "CHAN" & sChan & ":PROB?"
        WriteMsg((sInstrumentCmds))

        '##########################################
        'Action Required: Remove debug code
        'Fill return value selections
        ''        gcolCmds..Add "Simulated response for: " & sInstrumentCmds
        ''        gcolCmds..Add "1": gcolCmds.Add "10"
        ''        gcolCmds..Add "100": gcolCmds.Add "1000"
        '##########################################

        'Retrieve instrument output buffer
        sReadBuffer = ReadMsg()

        If (Not bIsHPE1428A) Then
            DscopeMain.ActivateControl((":CHAN" & sChan & ":PROB " & sReadBuffer))
        End If

        '*********
        'Get Range
        '*********
        sInstrumentCmds = "CHAN" & sChan & ":RANG?"
        WriteMsg((sInstrumentCmds))

        '##########################################
        'Action Required: Remove debug code
        'Fill return value selections
        ''        gcolCmds..Add "Simulated response for: " & sInstrumentCmds
        ''        gcolCmds..Add "0.008": gcolCmds.Add "0.050"
        ''        gcolCmds..Add "2.000": gcolCmds.Add "10.000"
        ''        gcolCmds..Add "40.000"
        '##########################################

        'Retrieve instrument output buffer
        sReadBuffer = ReadMsg()

        DscopeMain.ActivateControl((":CHAN" & sChan & ":RANG " & sReadBuffer))

        '*********
        'Get Offset
        '*********
        sInstrumentCmds = "CHAN" & sChan & ":OFFS?"
        WriteMsg((sInstrumentCmds))

        '##########################################
        'Action Required: Remove debug code
        'Fill return value selections
        '        gcolCmds..Add "Simulated response for: " & sInstrumentCmds
        '        gcolCmds..Add "0.008": gcolCmds.Add "0.040"
        '        gcolCmds..Add "2.000": gcolCmds.Add "30.000"
        '        gcolCmds..Add "-250": gcolCmds.Add "250"
        '##########################################

        'Retrieve instrument output buffer
        sReadBuffer = ReadMsg()

        DscopeMain.ActivateControl((":CHAN" & sChan & ":OFFS " & sReadBuffer))

        '*********
        'Get Coupling
        '*********
        sInstrumentCmds = "CHAN" & sChan & ":COUP?"
        WriteMsg((sInstrumentCmds))

        '##########################################
        'Action Required: Remove debug code
        'Fill return value selections
        '        gcolCmds..Add "Simulated response for: " & sInstrumentCmds
        '        gcolCmds..Add "AC"
        '        gcolCmds..Add "DC"
        '        gcolCmds..Add "DCF"
        '##########################################

        'Retrieve instrument output buffer
        sReadBuffer = ReadMsg()

        DscopeMain.ActivateControl((":CHAN" & sChan & ":COUP " & sReadBuffer))

        '*********
        'Get Low Freq Reject
        '*********
        sInstrumentCmds = "CHAN" & sChan & ":LFR?"
        WriteMsg((sInstrumentCmds))

        '##########################################
        'Action Required: Remove debug code
        'Fill return value selections
        '        gcolCmds..Add "Simulated response for: " & sInstrumentCmds
        '        gcolCmds..Add "0": gcolCmds.Add "1"
        '##########################################

        'Retrieve instrument output buffer
        sReadBuffer = ReadMsg()

        If sReadBuffer = "1" Then
            DscopeMain.ActivateControl((":CHAN" & sChan & ":LFR ON"))
        Else
            DscopeMain.ActivateControl((":CHAN" & sChan & ":LFR OFF"))
        End If

        '*********
        'Get High Freq Reject
        '*********
        sInstrumentCmds = "CHAN" & sChan & ":HFR?"
        WriteMsg((sInstrumentCmds))

        '##########################################
        'Action Required: Remove debug code
        'Fill return value selections
        '        gcolCmds..Add "Simulated response for: " & sInstrumentCmds
        '        gcolCmds..Add "0": gcolCmds.Add "1"
        '##########################################

        'Retrieve instrument output buffer
        sReadBuffer = ReadMsg()

        If sReadBuffer = "1" Then
            DscopeMain.ActivateControl((":CHAN" & sChan & ":HFR ON"))
        Else
            DscopeMain.ActivateControl((":CHAN" & sChan & ":HFR OFF"))
        End If

    End Sub

    Private Sub GetTimeBase()

        Dim sInstrumentCmds As String
        Dim sReadBuffer As String
        Dim sSampType As String

        '*********
        'Get Sample Type
        '*********
        sInstrumentCmds = "TIM:SAMP?"
        WriteMsg((sInstrumentCmds))

        '##########################################
        'Action Required: Remove debug code
        'Fill return value selections
        '        gcolCmds..Add "Simulated response for: " & sInstrumentCmds
        '        gcolCmds..Add "REAL": gcolCmds.Add "REP"
        '##########################################

        'Retrieve instrument output buffer
        sReadBuffer = ReadMsg()
        sSampType = sReadBuffer

        DscopeMain.ActivateControl((":TIM:SAMP " & sSampType))

        If sSampType = "REAL" Then
            '*********
            'Get Data Points
            '*********
            sInstrumentCmds = "ACQ:POIN?"
            WriteMsg((sInstrumentCmds))

            '##########################################
            'Action Required: Remove debug code
            'Fill return value selections
            '    '        gcolCmds..Add "Simulated response for: " & sInstrumentCmds
            '    '        gcolCmds..Add "500": gcolCmds.Add "8000"
            '##########################################

            'Retrieve instrument output buffer
            sReadBuffer = ReadMsg()

            DscopeMain.ActivateControl((":ACQ:POIN " & sReadBuffer))

        Else
            '*********
            'Get Avg count
            '*********
            sInstrumentCmds = "ACQ:COUN?"
            WriteMsg((sInstrumentCmds))

            '##########################################
            'Action Required: Remove debug code
            'Fill return value selections
            '    '        gcolCmds..Add "Simulated response for: " & sInstrumentCmds
            '    '        gcolCmds..Add "1": gcolCmds.Add "20"
            '    '        gcolCmds..Add "48": gcolCmds.Add "708"
            '    '        gcolCmds..Add "1453": gcolCmds.Add "2048"
            '##########################################

            'Retrieve instrument output buffer
            sReadBuffer = ReadMsg()

            DscopeMain.ActivateControl((":ACQ:COUN " & sReadBuffer))

            '*********
            'Get percent complete
            '*********
            sInstrumentCmds = "ACQ:COMP?"
            WriteMsg((sInstrumentCmds))

            '##########################################
            'Action Required: Remove debug code
            'Fill return value selections
            '    '        gcolCmds..Add "Simulated response for: " & sInstrumentCmds
            '    '        gcolCmds..Add "0": gcolCmds.Add "20"
            '    '        gcolCmds..Add "50": gcolCmds.Add "70"
            '    '        gcolCmds..Add "95": gcolCmds.Add "100"
            '##########################################

            'Retrieve instrument output buffer
            sReadBuffer = ReadMsg()

            DscopeMain.ActivateControl((":ACQ:COMP " & sReadBuffer))

        End If

        '*********
        'Get Timebase mode
        '*********
        sInstrumentCmds = "TIM:MODE?"
        WriteMsg((sInstrumentCmds))

        '##########################################
        'Action Required: Remove debug code
        'Fill return value selections
        '        gcolCmds..Add "Simulated response for: " & sInstrumentCmds
        '        gcolCmds..Add "AUTO"
        '        gcolCmds..Add "TRIG"
        '        gcolCmds..Add "SING"
        '##########################################

        'Retrieve instrument output buffer
        sReadBuffer = ReadMsg()

        DscopeMain.ActivateControl((":TIM:MODE " & sReadBuffer))

        '*********
        'Get Timebase range
        '*********
        sInstrumentCmds = "TIM:RANG?"
        WriteMsg((sInstrumentCmds))

        '##########################################
        'Action Required: Remove debug code
        'Fill return value selections
        '        gcolCmds..Add "Simulated response for: " & sInstrumentCmds
        '        gcolCmds..Add "0.000005": gcolCmds.Add "0.000020"
        '        gcolCmds..Add "0.005000": gcolCmds.Add "0.010000"
        '        gcolCmds..Add "0.500000": gcolCmds.Add "2.00000"
        '        gcolCmds..Add "10.00000": gcolCmds.Add "50.00000"
        '##########################################

        'Retrieve instrument output buffer
        sReadBuffer = ReadMsg()

        DscopeMain.ActivateControl((":TIM:RANG " & sReadBuffer))

        '*********
        'Get Timebase Reference
        '*********
        sInstrumentCmds = "TIM:REF?"
        WriteMsg((sInstrumentCmds))

        '##########################################
        'Action Required: Remove debug code
        'Fill return value selections
        '        gcolCmds..Add "Simulated response for: " & sInstrumentCmds
        '        gcolCmds..Add "LEFT"
        '        gcolCmds..Add "CENT"
        '        gcolCmds..Add "RIGH"
        '##########################################

        'Retrieve instrument output buffer
        sReadBuffer = ReadMsg()

        DscopeMain.ActivateControl((":TIM:REF " & sReadBuffer))

        '*********
        'Get Timebase delay
        '*********
        sInstrumentCmds = "TIM:DEL?"
        WriteMsg((sInstrumentCmds))

        '##########################################
        'Action Required: Remove debug code
        'Fill return value selections
        '        gcolCmds..Add "Simulated response for: " & sInstrumentCmds
        '        gcolCmds..Add "0.000005": gcolCmds.Add "0.000020"
        '        gcolCmds..Add "0.005000": gcolCmds.Add "0.010000"
        '        gcolCmds..Add "0.500000": gcolCmds.Add "2.00000"
        '        gcolCmds..Add "10.00000": gcolCmds.Add "50.00000"
        '##########################################

        'Retrieve instrument output buffer
        sReadBuffer = ReadMsg()

        DscopeMain.ActivateControl((":TIM:DEL " & sReadBuffer))

        '*********
        'Get Timebase hold off time
        '*********
        sInstrumentCmds = "TRIG:HOLD?"
        WriteMsg((sInstrumentCmds))

        '##########################################
        'Action Required: Remove debug code
        'Fill return value selections
        '        gcolCmds..Add "Simulated response for: " & sInstrumentCmds
        '        gcolCmds..Add "TIME,0.00002": gcolCmds.Add "TIME,0.00600"
        '        gcolCmds..Add "TIME,0.08500": gcolCmds.Add "TIME,0.32000"
        '        gcolCmds..Add "EVEN,2": gcolCmds.Add "EVEN,512"
        '        gcolCmds..Add "EVEN,1024": gcolCmds.Add "EVEN,65536"
        '##########################################

        'Retrieve instrument output buffer
        sReadBuffer = ReadMsg()

        DscopeMain.ActivateControl((":TRIG:HOLD " & sReadBuffer))

    End Sub


    Private Sub GetTrigger()

        Dim sInstrumentCmds As String
        Dim sReadBuffer As String
        Dim sTrigMode As String

        '*********
        'Get Trigger Mode
        '*********
        sInstrumentCmds = "TRIG:MODE?"
        WriteMsg((sInstrumentCmds))

        '##########################################
        'Action Required: Remove debug code
        'Fill return value selections
        '        gcolCmds..Add "Simulated response for: " & sInstrumentCmds
        '        gcolCmds..Add "EDGE"
        '        gcolCmds..Add "DEL"
        '        gcolCmds..Add "TV"
        '##########################################

        'Retrieve instrument output buffer
        sTrigMode = ReadMsg() 'keep the mode

        DscopeMain.ActivateControl((":TRIG:MODE " & sTrigMode))

        '*********
        'Get Trigger level
        '*********
        sInstrumentCmds = "TRIG:LEV?"
        WriteMsg((sInstrumentCmds))

        '##########################################
        'Action Required: Remove debug code
        'Fill return value selections
        '        gcolCmds..Add "Simulated response for: " & sInstrumentCmds
        '        gcolCmds..Add "0.000": gcolCmds.Add "0.006"
        '        gcolCmds..Add "0.080": gcolCmds.Add "0.200"
        '        gcolCmds..Add "0.400": gcolCmds.Add "1.000"
        '        gcolCmds..Add "1.500": gcolCmds.Add "2.000"
        '##########################################

        'Retrieve instrument output buffer
        sReadBuffer = ReadMsg() 'keep the mode

        DscopeMain.ActivateControl((":TRIG:LEV " & sReadBuffer))

        '*********
        'Get Trigger Sensitive
        '*********
        sInstrumentCmds = "TRIG:SENS?"
        WriteMsg((sInstrumentCmds))

        '##########################################
        'Action Required: Remove debug code
        'Fill return value selections
        '        gcolCmds..Add "Simulated response for: " & sInstrumentCmds
        '        gcolCmds..Add "NORM": gcolCmds.Add "LOW"
        '##########################################

        'Retrieve instrument output buffer
        sReadBuffer = ReadMsg() 'keep the mode

        DscopeMain.ActivateControl((":TRIG:SENS " & sReadBuffer))

        ' the following settings are needed for TV or DELay Mode
        If sTrigMode = "TV" Or sTrigMode = "DEL" Then

            '*********
            'Get Trigger Occurance
            '*********
            sInstrumentCmds = "TRIG:OCC?"
            WriteMsg((sInstrumentCmds))

            '##########################################
            'Action Required: Remove debug code
            'Fill return value selections
            '    '        gcolCmds..Add "Simulated response for: " & sInstrumentCmds
            '    '        gcolCmds..Add "1": gcolCmds.Add "54"
            '    '        gcolCmds..Add "367": gcolCmds.Add "89000"
            '    '        gcolCmds..Add "567567": gcolCmds.Add "16000000"
            '##########################################

            'Retrieve instrument output buffer
            sReadBuffer = ReadMsg() 'keep the mode

            DscopeMain.ActivateControl((":TRIG:OCC " & sReadBuffer))

            '*********
            'Get Timebase Delay "Only for DEL mode"
            '*********
            sInstrumentCmds = "TRIG:DEL?"
            WriteMsg((sInstrumentCmds))

            '##########################################
            'Action Required: Remove debug code
            'Fill return value selections
            '    '        gcolCmds..Add "Simulated response for: " & sInstrumentCmds
            '    '        gcolCmds..Add "TIME,30E-9": gcolCmds.Add "TIME,0.00600"
            '    '        gcolCmds..Add "TIME,0.08500": gcolCmds.Add "TIME,0.16000"
            '    '        gcolCmds..Add "EVEN,2": gcolCmds.Add "EVEN,512"
            '    '        gcolCmds..Add "EVEN,1024": gcolCmds.Add "EVEN,65536"
            '##########################################

            'Retrieve instrument output buffer
            sReadBuffer = ReadMsg()

            DscopeMain.ActivateControl((":TRIG:DEL " & sReadBuffer))

            If VB.Left(sReadBuffer, 4) = "EVEN" Then
                GetTrigSourSlop(":DEL")
            End If
        End If

        ' the following settings are only needed for TV Mode
        If sTrigMode = "TV" Then

            '*********
            'Get Trigger Standard
            '*********
            sInstrumentCmds = "TRIG:STAN?"
            WriteMsg((sInstrumentCmds))

            '##########################################
            'Action Required: Remove debug code
            'Fill return value selections
            '    '        gcolCmds..Add "Simulated response for: " & sInstrumentCmds
            '    '        gcolCmds..Add "525": gcolCmds.Add "625"
            '    '        gcolCmds..Add "USER"
            '##########################################

            'Retrieve instrument output buffer
            sReadBuffer = ReadMsg() 'keep the mode

            DscopeMain.ActivateControl((":TRIG:STAN " & sReadBuffer))

            If sReadBuffer <> "USER" Then 'Not used in USER standard
                '*********
                'Get Trigger Field
                '*********
                sInstrumentCmds = "TRIG:FIEL?"
                WriteMsg((sInstrumentCmds))

                '##########################################
                'Action Required: Remove debug code
                'Fill return value selections
                '    '        gcolCmds..Add "Simulated response for: " & sInstrumentCmds
                '    '        gcolCmds..Add "1": gcolCmds.Add "2"
                '##########################################

                'Retrieve instrument output buffer
                sReadBuffer = ReadMsg() 'keep the mode

                DscopeMain.ActivateControl((":TRIG:FIEL " & sReadBuffer))

                '*********
                'Get Trigger Line
                '*********
                sInstrumentCmds = "TRIG:LINE?"
                WriteMsg((sInstrumentCmds))

                '##########################################
                'Action Required: Remove debug code
                'Fill return value selections
                '    '        gcolCmds..Add "Simulated response for: " & sInstrumentCmds
                '    '        gcolCmds..Add "1": gcolCmds.Add "5"
                '    '        gcolCmds..Add "9": gcolCmds.Add "29"
                '    '        gcolCmds..Add "87": gcolCmds.Add "234"
                '    '        gcolCmds..Add "456": gcolCmds.Add "625"
                '##########################################

                'Retrieve instrument output buffer
                sReadBuffer = ReadMsg() 'keep the mode

                DscopeMain.ActivateControl((":TRIG:LINE " & sReadBuffer))
            End If
        End If

    End Sub

    Private Sub GetTrigSourSlop(ByRef sType As String)

        Dim sInstrumentCmds As String
        Dim sReadBuffer As String

        '*********
        'Get Source
        '*********
        sInstrumentCmds = "TRIG" & sType & ":SOUR?"
        WriteMsg((sInstrumentCmds))

        '##########################################
        'Action Required: Remove debug code
        'Fill return value selections
        '        gcolCmds..Add "Simulated response for: " & sInstrumentCmds
        '        gcolCmds..Add "CHAN1": gcolCmds.Add "CHAN2"
        '        gcolCmds..Add "ECLT0": gcolCmds.Add "ECLT1"
        '        gcolCmds..Add "EXT"
        '##########################################

        'Retrieve instrument output buffer
        sReadBuffer = ReadMsg() 'keep the mode

        DscopeMain.ActivateControl((":TRIG" & sType & ":SOUR " & sReadBuffer))

        '*********
        'Get Slope
        '*********
        sInstrumentCmds = "TRIG" & sType & ":SLOP?"
        WriteMsg((sInstrumentCmds))

        '##########################################
        'Action Required: Remove debug code
        'Fill return value selections
        '        gcolCmds..Add "Simulated response for: " & sInstrumentCmds
        '        gcolCmds..Add "POS": gcolCmds.Add "NEG"
        '##########################################

        'Retrieve instrument output buffer
        sReadBuffer = ReadMsg() 'keep the mode

        DscopeMain.ActivateControl((":TRIG" & sType & ":SLOP " & sReadBuffer))

    End Sub

    Private Sub GetOutputState(ByRef sTrig As String)

        Dim sInstrumentCmds As String
        Dim sReadBuffer As String

        '*********
        'Get Range
        '*********
        sInstrumentCmds = "OUTP" & sTrig & ":STAT?"
        WriteMsg((sInstrumentCmds))

        '##########################################
        'Action Required: Remove debug code
        'Fill return value selections
        '        gcolCmds..Add "Simulated response for: " & sInstrumentCmds
        '        gcolCmds..Add "0": gcolCmds.Add "1"
        '##########################################

        'Retrieve instrument output buffer
        sReadBuffer = ReadMsg() 'keep the mode

        If sReadBuffer = "1" Then
            DscopeMain.ActivateControl((":OUTP" & sTrig & ":STAT ON"))
        Else
            DscopeMain.ActivateControl((":OUTP" & sTrig & ":STAT OFF"))
        End If


    End Sub


    Private Sub cmdDisplaySize_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdDisplaySize.Click
        Dim newsize As Size
        newsize.Height = 236
        If Me.cmdDisplaySize.Text = "&Full Size" Then
            Me.cmdDisplaySize.Text = "&Half Size"
            newsize.Width = (297 * 2.1)
            Me.cwgScopeDisplay.Size = newsize
            Me.cwgScopeDisplay.BringToFront()

            newsize.Width = (297 * 2.1)
            newsize.Height = 236
            Me.panScopeDisplay.Size = newsize
            Me.panScopeDisplay.BringToFront()
            Me.cwgScopeDisplay.BringToFront()
        Else
            Me.cmdDisplaySize.Text = "&Full Size"
            newsize.Width = 297
            Me.cwgScopeDisplay.Size = newsize

            newsize.Height = 236
            newsize.Width = 297
            Me.panScopeDisplay.Size = newsize

        End If


    End Sub

    Private Sub cmdHelp_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdHelp.Click

        CenterForm(frmHelp)
        frmHelp.Show()

    End Sub


    Private Sub cmdLoadFromDisk_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdLoadFromDisk.Click

        LoadDiskToMemory()

    End Sub

    Private Sub cmdMeasure_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdMeasure.Click
        'Added code to check for excessive acquisition time. If the time needed for acquisition is greater than
        '30 seconds ask the user if they want to continue. V1.8 JCB
        If (Panel_Conifg.DebugMode = True) Then 'Only perform switch operation if in Local Mode
            Exit Sub
        End If

        Dim Continue_Renamed As Short

        If dAcqTime > 30 Then
            Continue_Renamed = MsgBox("This acquisition will take approximately " & Round(dAcqTime / 60, 1) & " minutes. Proceed?", MsgBoxStyle.OkCancel, "Acquisition Time Notification")
        End If
        If Continue_Renamed = MsgBoxResult.Cancel Then
            Exit Sub
        End If
        DigitizeWaveform()

    End Sub


    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click

        tabMainFunctions.SelectedIndex = 0
        Application.DoEvents()
        PrintForm1.Print()

    End Sub

    Private Sub cmdQuit_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdQuit.Click

        QuitProgram = True
        EndProgram()

    End Sub



    Private Sub cmdReset_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdReset.Click
        ' ensure that this is a reset, not quit out of program
        QuitProgram = False

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode
        ResetScope()

    End Sub

    Private Sub cmdSaveToDisk_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSaveToDisk.Click
        Dim Index As Short = cmdSaveToDisk.GetIndex(eventSender)

        SaveToDisk(Index)

    End Sub

    Private Sub cmdSaveToMem_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSaveToMem.Click
        Dim Index As Short = cmdSaveToMem.GetIndex(eventSender)

        SaveToMemory(Index)

    End Sub
    Private Sub cmdSelfTest_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSelfTest.Click

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode
        RunSelfTest()

    End Sub

    Private Sub cmdUpdateTIP_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdUpdateTIP.Click

        Dim sNonCOMPstr As String
        Dim sCOMPstr As String
        Dim sACQstr As String
        Dim sErrorMessage As String
        Dim iParamIndex As Short


        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        sTIP_CMDstring = ""

        '****************************
        '*** Build TIP CMD string ***
        '****************************
        'Part 1: Define non-COMP commands
        'Define measurement function (GUI only)
        sNonCOMPstr = MainParam(MEAS_FUNCTION).SetCod & CStr(CurrentMeas) & ";"
        'Define saved waveform segment filename (if applicable)
        If sSegFileName <> "" Then
            If InStr(sSegFileName, ".") = 0 Then 'If No File Extension then use .seg
                sNonCOMPstr = sNonCOMPstr & MainParam(SEGMENT_FILE).SetCod & sSegFileName & ".seg;"
            Else
                sNonCOMPstr = sNonCOMPstr & MainParam(SEGMENT_FILE).SetCod & sSegFileName & ";"
            End If
        End If
        'Define Trace settings if modified from default (GUI only)
        If cboTraceThickness.SelectedIndex = 1 Or cboTraceThickness.SelectedIndex = 2 Then
            sNonCOMPstr = sNonCOMPstr & MainParam(TRACE_THICKNESS).SetCod & CStr(cboTraceThickness.SelectedIndex) & ";"
        End If
        If TraceColor(0) <> RGB(255, 255, 0) Then 'CH 1 default color
            sNonCOMPstr = sNonCOMPstr & MainParam(CH1_TRACE_COLOR).SetCod & CStr(TraceColor(0)) & ";"
        End If
        If TraceColor(1) <> RGB(0, 255, 255) Then 'CH 2 default color
            sNonCOMPstr = sNonCOMPstr & MainParam(CH2_TRACE_COLOR).SetCod & CStr(TraceColor(1)) & ";"
        End If
        If TraceColor(2) <> RGB(255, 0, 255) Then 'MATH default color
            sNonCOMPstr = sNonCOMPstr & MainParam(MATH_TRACE_COLOR).SetCod & CStr(TraceColor(2)) & ";"
        End If
        If TraceColor(3) <> RGB(0, 255, 0) Then 'CH 2 default color
            sNonCOMPstr = sNonCOMPstr & MainParam(MEMORY_TRACE_COLOR).SetCod & CStr(TraceColor(3)) & ";"
        End If
        'Set Math Range/Offset
        If picMathOptions.Visible Then
            For iParamIndex = FUNC_RANGE To FUNC_OFFSET
                sNonCOMPstr = sNonCOMPstr & MainParam(iParamIndex).SetCod & MainParam(iParamIndex).SetCur & ";"
            Next iParamIndex
        End If
        'Set Memory Range/Offset
        If picMemOptions.Visible Then
            For iParamIndex = MEM_RANGE To MEM_OFFSET
                sNonCOMPstr = sNonCOMPstr & MainParam(iParamIndex).SetCod & MainParam(iParamIndex).SetCur & ";"
            Next iParamIndex
        End If
        'Remove the ";" from the end of the string
        sNonCOMPstr = VB.Left(sNonCOMPstr, Len(sNonCOMPstr) - 1)

        'Part 2: Define COMP CMD string
        'Define Measurement source(s) first
        If bTIP_UserMeas Then
            sCOMPstr = sCOMPstr & ":MEAS:SOUR " & StartSource & "," & StopSource & ";"
        Else
            sCOMPstr = sCOMPstr & sTIPsSetMEASureOptions()
        End If
        sCOMPstr = sCOMPstr & sTIPsSetCHANnel1Options()
        sCOMPstr = sCOMPstr & sTIPsSetCHANnel2Options()
        sCOMPstr = sCOMPstr & sTIPsSetOUTPutOptions()
        sCOMPstr = sCOMPstr & sTIPsSetTRIGgerOptions()
        sCOMPstr = sCOMPstr & sTIPsSetTIMebaseOptions()
        sCOMPstr = sCOMPstr & sTIPsSetFUNCtionOptions()
        If ribCompON.Checked = True Then
            sCOMPstr = sCOMPstr & ":BNC PROB"
        Else
            'Remove the ";" from the end of the string
            sCOMPstr = VB.Left(sCOMPstr, Len(sCOMPstr) - 1)
        End If
        'Now send entire COMP command string and check for errors such as setting conflicts
        WriteMsg("*RST")
        WriteMsg("*CLS")
        WriteMsg(sCOMPstr)
        'Don't exit if error, allow user opportunity to correct
        If Not bGetInstrumentStatus() Then GoTo ErrorHandle

        If cboSampleType.SelectedIndex = 1 Then 'Repetitive
            sACQstr = sTIPsSetACQuireOptions()
            'Remove the ";" from the end of the string
            sACQstr = VB.Left(sACQstr, Len(sACQstr) - 1)
            WriteMsg(sACQstr)
            If Not bGetInstrumentStatus() Then GoTo ErrorHandle
            sTIP_CMDstring = sNonCOMPstr & "|" & sCOMPstr & "|" & sACQstr
        Else
            sTIP_CMDstring = sNonCOMPstr & "|" & sCOMPstr
        End If

        'If applicable, append DELAY measurement parameters to TIP command string
        If bTIP_UserMeas Then
            sDelayMeasStr = sTIPsSetMEASureOptions()
            'Remove the ";" from the end of the string
            sDelayMeasStr = VB.Left(sDelayMeasStr, Len(sDelayMeasStr) - 1)
            WriteMsg(sDelayMeasStr)
            If Not bGetInstrumentStatus() Then GoTo ErrorHandle
            sTIP_CMDstring = sTIP_CMDstring & "|" & sDelayMeasStr
        End If

        'Added to support the ScopeNAM  DJoiner
        If cmdUpdateTIP.Text = "&Update TIP" Then
            'Send TIP CMD string to VIPERT.INI file and close
            SetKey("TIPS", "CMD", sTIP_CMDstring)
            SetKey("TIPS", "STATUS", "Ready")
        Else
            If bSaveDSO_File(sTIP_CMDstring) Then
                EndProgram()
            Else

                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                Exit Sub
            End If
        End If

        EndProgram()

ErrorHandle:
        ResetScope()
        sTIP_CMDstring = ""



        System.Windows.Forms.Cursor.Current = Cursors.Default

    End Sub

    Private Sub cmdUpdateTIP_MouseMove(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles cmdUpdateTIP.MouseMove
        Dim Button As Short = eventArgs.Button \ &H100000
        Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
        Dim x As Single = VB6.PixelsToTwipsX(eventArgs.X)
        Dim y As Single = VB6.PixelsToTwipsY(eventArgs.Y)
        'Display Status Bar Message only when the "NAM File" caption is displayed
        If cmdUpdateTIP.Text = "&NAM File" Then
            StatusBarMessage("Save a Scope configuration file for use with the ScopeNAM.")
        End If

    End Sub

    Private Sub cwgScopeDisplay_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)

        Me.panScopeDisplay.BringToFront()

    End Sub

    Private Sub frmZT1428_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        '   Set Common Controls parent properties
        Atlas_SFP.Parent_Object = Me
        Panel_Conifg.Parent_Object = Me
        Main()
    End Sub

    Function Limit(ByRef CheckValue As Short, ByRef LimitValue As Short) As Short
        If CheckValue < LimitValue Then
            Limit = LimitValue
        Else
            Limit = CheckValue
        End If
    End Function

       
    Private Sub Form_Terminate_Renamed()

        QuitProgram = True
        EndProgram()

    End Sub


    Private Sub frmZT1428_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        QuitProgram = True
        EndProgram()

    End Sub


    Private Sub fraOccurrence_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode
        RangeUpdate(TRIG_OCC)

    End Sub


    Private Sub imgLogo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)

        If Me.WindowState = System.Windows.Forms.FormWindowState.Minimized Or Me.WindowState = System.Windows.Forms.FormWindowState.Maximized Then Exit Sub

        Me.Width = VB6.TwipsToPixelsX(9480)
        Me.Height = VB6.TwipsToPixelsY(5475)

    End Sub

    Private Sub Label1_MouseMove(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs)
        Dim Button As Short = eventArgs.Button \ &H100000
        Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
        Dim x As Single = VB6.PixelsToTwipsX(eventArgs.X)
        Dim y As Single = VB6.PixelsToTwipsY(eventArgs.Y)

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then Exit Sub 'If TIP Run mode instrument is configured for test
        RangeUpdate(STOP_LEVEL)

    End Sub

    Private Sub lblEvents_MouseMove(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs)
        Dim Button As Short = eventArgs.Button \ &H100000
        Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
        Dim x As Single = VB6.PixelsToTwipsX(eventArgs.X)
        Dim y As Single = VB6.PixelsToTwipsY(eventArgs.Y)

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then Exit Sub 'If TIP Run mode instrument is configured for test
        RangeUpdate(HOLD_OFF)

    End Sub

    Private Sub lblStartEdgeCap_MouseMove(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs)
        Dim Button As Short = eventArgs.Button \ &H100000
        Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
        Dim x As Single = VB6.PixelsToTwipsX(eventArgs.X)
        Dim y As Single = VB6.PixelsToTwipsY(eventArgs.Y)

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then Exit Sub 'If TIP Run mode instrument is configured for test
        RangeUpdate(START_EDGE)

    End Sub

    Private Sub lblStartLevelCap_MouseMove(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs)
        Dim Button As Short = eventArgs.Button \ &H100000
        Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
        Dim x As Single = VB6.PixelsToTwipsX(eventArgs.X)
        Dim y As Single = VB6.PixelsToTwipsY(eventArgs.Y)

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then Exit Sub 'If TIP Run mode instrument is configured for test
        RangeUpdate(START_LEVEL)

    End Sub

    Private Sub lblStopEdgeCap_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then Exit Sub 'If TIP Run mode instrument is configured for test
        RangeUpdate(STOP_EDGE)

    End Sub

    Private Sub lblTrigDelayEvents_MouseMove(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs)
        Dim Button As Short = eventArgs.Button \ &H100000
        Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
        Dim x As Single = VB6.PixelsToTwipsX(eventArgs.X)
        Dim y As Single = VB6.PixelsToTwipsY(eventArgs.Y)

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then Exit Sub 'If TIP Run mode instrument is configured for test
        RangeUpdate(TRIG_DEL)

    End Sub

    Private Sub panScopeDisplay_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)

        Me.panScopeDisplay.BringToFront()

    End Sub

    Private Sub picTraceColor_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles picTraceColor.Click

        If sTIP_Mode = "TIP_DESIGN" Then bSkipDialog = False 'Allow pop up color dialog box in Sub ChangeTraceColor()
        ChangeTraceColor()

    End Sub

    Private Sub Picture4_MouseMove(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs)
        Dim Button As Short = eventArgs.Button \ &H100000
        Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
        Dim x As Single = VB6.PixelsToTwipsX(eventArgs.X)
        Dim y As Single = VB6.PixelsToTwipsY(eventArgs.Y)

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then Exit Sub 'If TIP Run mode instrument is configured for test
        RangeUpdate(START_LEVEL)

    End Sub


    Private Sub SpinButton1_SpinDown(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SpinButton1.DownButtonClicked

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode
        SpinParam(FUNC_RANGE, "DOWN")

    End Sub

    Private Sub SpinButton1_SpinUp(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SpinButton1.UpButtonClicked

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode
        SpinParam(FUNC_RANGE, "UP")

    End Sub

    Private Sub SpinButton2_SpinDown(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SpinButton2.DownButtonClicked

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode
        SpinParam(MEM_RANGE, "DOWN")

    End Sub

    Private Sub SpinButton2_SpinUp(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SpinButton2.UpButtonClicked

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode
        SpinParam(MEM_RANGE, "UP")

    End Sub

    Private Sub spnAvgCount_SpinDown(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles spnAvgCount.DownButtonClicked

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode
        SpinParam(AVR_COUNT, "DOWN")

    End Sub

    Private Sub spnAvgCount_SpinUp(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles spnAvgCount.UpButtonClicked

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode
        SpinParam(AVR_COUNT, "UP")

    End Sub


    Private Sub spnCompletion_SpinDown(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles spnCompletion.DownButtonClicked

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode
        SpinParam(COMPLETION, "DOWN")

    End Sub

    Private Sub spnCompletion_SpinUp(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles spnCompletion.UpButtonClicked

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode
        SpinParam(COMPLETION, "UP")

    End Sub

    Private Sub spnDelay_SpinDown(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles NumericEdit1.DownButtonClicked

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode
        SpinParam(REF_DELAY, "DOWN")

    End Sub

    Private Sub spnDelay_SpinUp(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles NumericEdit1.UpButtonClicked

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode
        SpinParam(REF_DELAY, "UP")

    End Sub

    Private Sub spnDelEvents_SpinDown(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles spnDelEvents.DownButtonClicked

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode
        SpinParam(TRIG_DEL, "DOWN")

    End Sub

    Private Sub spnDelEvents_SpinUp(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles spnDelEvents.UpButtonClicked

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode
        SpinParam(TRIG_DEL, "UP")

    End Sub

    Private Sub spnHoldOff_SpinDown(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles spnHoldOff.DownButtonClicked

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode
        SpinParam(HOLD_OFF, "DOWN")

    End Sub

    Private Sub spnHoldOff_SpinUp(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles spnHoldOff.UpButtonClicked

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode
        SpinParam(HOLD_OFF, "UP")

    End Sub

    Private Sub spnLine_SpinDown(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles spnLine.DownButtonClicked

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode
        SpinParam(TRIG_LINE, "DOWN")

    End Sub

    Private Sub spnLine_SpinUp(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles spnLine.UpButtonClicked

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode
        SpinParam(TRIG_LINE, "UP")

    End Sub

    Private Sub spnMathVOffset_SpinDown(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles spnMathVOffset.DownButtonClicked

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode
        SpinParam(FUNC_OFFSET, "DOWN")

    End Sub

    Private Sub spnMathVOffset_SpinUp(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles spnMathVOffset.UpButtonClicked

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode
        SpinParam(FUNC_OFFSET, "UP")

    End Sub

    Private Sub spnMemVOffset_SpinDown(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles spnMemVOffset.DownButtonClicked

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode
        SpinParam(MEM_OFFSET, "DOWN")

    End Sub

    Private Sub spnMemVOffset_SpinUp(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles spnMemVOffset.UpButtonClicked

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode
        SpinParam(MEM_OFFSET, "UP")

    End Sub

    Private Sub spnOccurrence_SpinDown(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles spnOccurrence.DownButtonClicked

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode
        SpinParam(TRIG_OCC, "DOWN")

    End Sub

    Private Sub spnOccurrence_SpinUp(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles spnOccurrence.UpButtonClicked

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode
        SpinParam(TRIG_OCC, "UP")

    End Sub

    Private Sub spnStartEdge_SpinDown(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles spnStartEdge.DownButtonClicked

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode
        SpinParam(START_EDGE, "DOWN")

    End Sub

    Private Sub spnStartEdge_SpinUp(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles spnStartEdge.UpButtonClicked

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode
        SpinParam(START_EDGE, "UP")

    End Sub

    Private Sub spnStartLevel_SpinDown(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles spnStartLevel.DownButtonClicked

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode
        SpinParam(START_LEVEL, "DOWN")

    End Sub

    Private Sub spnStartLevel_SpinUp(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles spnStartLevel.UpButtonClicked

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode
        SpinParam(START_LEVEL, "UP")

    End Sub

    Private Sub spnStopEdge_SpinDown(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles spnStopEdge.DownButtonClicked

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode
        SpinParam(STOP_EDGE, "DOWN")

    End Sub

    Private Sub spnStopEdge_SpinUp(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles spnStopEdge.UpButtonClicked

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode
        SpinParam(STOP_EDGE, "UP")

    End Sub

    Private Sub spnStopLevel_SpinDown(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles spnStopLevel.DownButtonClicked

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode
        SpinParam(STOP_LEVEL, "DOWN")

    End Sub

    Private Sub spnStopLevel_SpinUp(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles spnStopLevel.UpButtonClicked

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode
        SpinParam(STOP_LEVEL, "UP")

    End Sub

    Private Sub spnTrigLevel_SpinDown(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles spnTrigLevel.DownButtonClicked

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode
        SpinParam(TRIG_LEV, "DOWN")

    End Sub

    Private Sub spnTrigLevel_SpinUp(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles spnTrigLevel.UpButtonClicked

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode
        SpinParam(TRIG_LEV, "UP")

    End Sub

    Private Sub spnVOffset1_SpinDown(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles spnVOffset1.DownButtonClicked

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode
        SpinParam(IN1_OFFSET, "DOWN")

    End Sub

    Private Sub spnVOffset1_SpinUp(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles spnVOffset1.UpButtonClicked

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode
        SpinParam(IN1_OFFSET, "UP")

    End Sub

    Private Sub spnVOffset2_SpinDown(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles spnVOffset2.DownButtonClicked

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode
        SpinParam(IN2_OFFSET, "DOWN")

    End Sub

    Private Sub spnVOffset2_SpinUp(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles spnVOffset2.UpButtonClicked

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode
        SpinParam(IN2_OFFSET, "UP")

    End Sub

    Private Sub tabMainFunctions_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles tabMainFunctions.SelectedIndexChanged
        Static PreviousTab As Short = tabMainFunctions.SelectedIndex()

        Me.tabMainFunctions.BringToFront()
        If (sTIP_Mode = "TIP_RUNSETUP" Or sTIP_Mode = "TIP_RUNPERSIST" Or sTIP_Mode = "TIP_MANUAL" Or sTIP_Mode = "TIP_MEASONLY") And tabMainFunctions.SelectedIndex = 6 Then
            Me.cmdUpdateTIP.Visible = False
            Me.cmdSelfTest.Visible = False
        End If

        PreviousTab = tabMainFunctions.SelectedIndex()
    End Sub

    Private Sub tabMainFunctions_MouseDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles tabMainFunctions.MouseDown
        Dim Button As Short = eventArgs.Button \ &H100000
        Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
        Dim x As Single = VB6.PixelsToTwipsX(eventArgs.X)
        Dim y As Single = VB6.PixelsToTwipsY(eventArgs.Y)

        If Button = VB6.MouseButtonConstants.LeftButton Then
            Me.tabMainFunctions.BringToFront()
        End If

    End Sub


    Private Sub txtAvgCount_Enter(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAvgCount.Enter

        GotFocusSelect()

    End Sub


    Private Sub txtAvgCount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAvgCount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = CShort(RegisterKeyPress(KeyAscii, AVR_COUNT))

        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtAvgCount_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAvgCount.Leave

        RegisterEntry(AVR_COUNT)

    End Sub


    Private Sub txtAvgCount_MouseMove(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles txtAvgCount.MouseMove
        Dim Button As Short = eventArgs.Button \ &H100000
        Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
        Dim x As Single = VB6.PixelsToTwipsX(eventArgs.X)
        Dim y As Single = VB6.PixelsToTwipsY(eventArgs.Y)

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then Exit Sub 'If TIP Run mode instrument is configured for test
        RangeUpdate(AVR_COUNT)

    End Sub


    Private Sub txtCompletion_Enter(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCompletion.Enter

        GotFocusSelect()

    End Sub


    Private Sub txtCompletion_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCompletion.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = CShort(RegisterKeyPress(KeyAscii, COMPLETION))

        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtCompletion_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCompletion.Leave

        RegisterEntry(COMPLETION)

    End Sub


    Private Sub txtCompletion_MouseMove(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles txtCompletion.MouseMove
        Dim Button As Short = eventArgs.Button \ &H100000
        Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
        Dim x As Single = VB6.PixelsToTwipsX(eventArgs.X)
        Dim y As Single = VB6.PixelsToTwipsY(eventArgs.Y)

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then Exit Sub 'If TIP Run mode instrument is configured for test
        RangeUpdate(COMPLETION)

    End Sub


    Private Sub txtDelay_Enter(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDelay.Enter

        GotFocusSelect()

    End Sub


    Private Sub txtDelay_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDelay.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = CShort(RegisterKeyPress(KeyAscii, REF_DELAY))

        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtDelay_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDelay.Leave

        RegisterEntry(REF_DELAY)

    End Sub

    Private Sub txtDelay_MouseMove(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles txtDelay.MouseMove
        Dim Button As Short = eventArgs.Button \ &H100000
        Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
        Dim x As Single = VB6.PixelsToTwipsX(eventArgs.X)
        Dim y As Single = VB6.PixelsToTwipsY(eventArgs.Y)

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then Exit Sub 'If TIP Run mode instrument is configured for test
        RangeUpdate(REF_DELAY)

    End Sub


    Private Sub txtDelEvents_Enter(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDelEvents.Enter

        GotFocusSelect()

    End Sub


    Private Sub txtDelEvents_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDelEvents.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = CShort(RegisterKeyPress(KeyAscii, TRIG_DEL))

        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtDelEvents_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDelEvents.Leave

        RegisterEntry(TRIG_DEL)

    End Sub


    Private Sub txtDelEvents_MouseMove(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles txtDelEvents.MouseMove
        Dim Button As Short = eventArgs.Button \ &H100000
        Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
        Dim x As Single = VB6.PixelsToTwipsX(eventArgs.X)
        Dim y As Single = VB6.PixelsToTwipsY(eventArgs.Y)

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then Exit Sub 'If TIP Run mode instrument is configured for test
        RangeUpdate(TRIG_DEL)

    End Sub


    Private Sub txtHoldOff_Enter(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtHoldOff.Enter

        GotFocusSelect()

    End Sub


    Private Sub txtHoldOff_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtHoldOff.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = CShort(RegisterKeyPress(KeyAscii, HOLD_OFF))

        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtHoldOff_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtHoldOff.Leave

        RegisterEntry(HOLD_OFF)

    End Sub


    Private Sub txtHoldOff_MouseMove(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles txtHoldOff.MouseMove
        Dim Button As Short = eventArgs.Button \ &H100000
        Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
        Dim x As Single = VB6.PixelsToTwipsX(eventArgs.X)
        Dim y As Single = VB6.PixelsToTwipsY(eventArgs.Y)

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then Exit Sub 'If TIP Run mode instrument is configured for test
        RangeUpdate(HOLD_OFF)

    End Sub


    Private Sub txtLine_Enter(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLine.Enter

        GotFocusSelect()

    End Sub
    Private Sub txtLine_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtLine.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = CShort(RegisterKeyPress(KeyAscii, TRIG_LINE))

        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtLine_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLine.Leave

        RegisterEntry(TRIG_LINE)

    End Sub


    Private Sub txtLine_MouseMove(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles txtLine.MouseMove
        Dim Button As Short = eventArgs.Button \ &H100000
        Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
        Dim x As Single = VB6.PixelsToTwipsX(eventArgs.X)
        Dim y As Single = VB6.PixelsToTwipsY(eventArgs.Y)

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then Exit Sub 'If TIP Run mode instrument is configured for test
        RangeUpdate(TRIG_LINE)

    End Sub


    Private Sub txtMathRange_Enter(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMathRange.Enter

        GotFocusSelect()

    End Sub


    Private Sub txtMathRange_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtMathRange.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = CShort(RegisterKeyPress(KeyAscii, FUNC_RANGE))

        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtMathRange_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMathRange.Leave

        RegisterEntry(FUNC_RANGE)

    End Sub


    Private Sub txtMathRange_MouseMove(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles txtMathRange.MouseMove
        Dim Button As Short = eventArgs.Button \ &H100000
        Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
        Dim x As Single = VB6.PixelsToTwipsX(eventArgs.X)
        Dim y As Single = VB6.PixelsToTwipsY(eventArgs.Y)

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then Exit Sub 'If TIP Run mode instrument is configured for test
        RangeUpdate(FUNC_RANGE)

    End Sub

    Private Sub txtMathVOffset_Enter(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMathVOffset.Enter

        GotFocusSelect()

    End Sub


    Private Sub txtMathVOffset_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtMathVOffset.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = CShort(RegisterKeyPress(KeyAscii, FUNC_OFFSET))

        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtMathVOffset_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMathVOffset.Leave

        RegisterEntry(FUNC_OFFSET)

    End Sub


    Private Sub txtMathVOffset_MouseMove(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles txtMathVOffset.MouseMove
        Dim Button As Short = eventArgs.Button \ &H100000
        Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
        Dim x As Single = VB6.PixelsToTwipsX(eventArgs.X)
        Dim y As Single = VB6.PixelsToTwipsY(eventArgs.Y)

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then Exit Sub 'If TIP Run mode instrument is configured for test
        RangeUpdate(FUNC_OFFSET)

    End Sub


    Private Sub txtMemoryRange_Enter(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMemoryRange.Enter

        GotFocusSelect()

    End Sub


    Private Sub txtMemoryRange_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtMemoryRange.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = CShort(RegisterKeyPress(KeyAscii, MEM_RANGE))

        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtMemoryRange_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMemoryRange.Leave

        RegisterEntry(MEM_RANGE)

    End Sub

    Private Sub txtMemoryRange_MouseMove(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles txtMemoryRange.MouseMove
        Dim Button As Short = eventArgs.Button \ &H100000
        Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
        Dim x As Single = VB6.PixelsToTwipsX(eventArgs.X)
        Dim y As Single = VB6.PixelsToTwipsY(eventArgs.Y)

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then Exit Sub 'If TIP Run mode instrument is configured for test
        RangeUpdate(MEM_RANGE)

    End Sub

    Private Sub txtMemVoltOffset_Enter(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMemVoltOffset.Enter

        GotFocusSelect()

    End Sub


    Private Sub txtMemVoltOffset_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtMemVoltOffset.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = CShort(RegisterKeyPress(KeyAscii, MEM_OFFSET))

        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtMemVoltOffset_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMemVoltOffset.Leave

        RegisterEntry(MEM_OFFSET)

    End Sub

    Private Sub txtMemVoltOffset_MouseMove(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles txtMemVoltOffset.MouseMove
        Dim Button As Short = eventArgs.Button \ &H100000
        Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
        Dim x As Single = VB6.PixelsToTwipsX(eventArgs.X)
        Dim y As Single = VB6.PixelsToTwipsY(eventArgs.Y)

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then Exit Sub 'If TIP Run mode instrument is configured for test
        RangeUpdate(MEM_OFFSET)

    End Sub
    Private Sub txtOccurrence_Enter(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOccurrence.Enter

        GotFocusSelect()

    End Sub


    Private Sub txtOccurrence_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtOccurrence.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = CShort(RegisterKeyPress(KeyAscii, TRIG_OCC))

        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtOccurrence_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOccurrence.Leave

        RegisterEntry(TRIG_OCC)

    End Sub


    Private Sub txtOccurrence_MouseMove(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles txtOccurrence.MouseMove
        Dim Button As Short = eventArgs.Button \ &H100000
        Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
        Dim x As Single = VB6.PixelsToTwipsX(eventArgs.X)
        Dim y As Single = VB6.PixelsToTwipsY(eventArgs.Y)

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then Exit Sub 'If TIP Run mode instrument is configured for test
        RangeUpdate(TRIG_OCC)

    End Sub



    Private Sub txtStartEdge_Enter(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtStartEdge.Enter

        GotFocusSelect()

    End Sub


    Private Sub txtStartEdge_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtStartEdge.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = CShort(RegisterKeyPress(KeyAscii, START_EDGE))

        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtStartEdge_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtStartEdge.Leave

        RegisterEntry(START_EDGE)

    End Sub


    Private Sub txtStartEdge_MouseMove(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles txtStartEdge.MouseMove
        Dim Button As Short = eventArgs.Button \ &H100000
        Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
        Dim x As Single = VB6.PixelsToTwipsX(eventArgs.X)
        Dim y As Single = VB6.PixelsToTwipsY(eventArgs.Y)

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then Exit Sub 'If TIP Run mode instrument is configured for test
        RangeUpdate(START_EDGE)

    End Sub


    Private Sub txtStartLevel_Enter(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtStartLevel.Enter

        GotFocusSelect()

    End Sub


    Private Sub txtStartLevel_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtStartLevel.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = CShort(RegisterKeyPress(KeyAscii, START_LEVEL))

        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtStartLevel_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtStartLevel.Leave

        RegisterEntry(START_LEVEL)

    End Sub



    Private Sub txtStartLevel_MouseMove(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles txtStartLevel.MouseMove
        Dim Button As Short = eventArgs.Button \ &H100000
        Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
        Dim x As Single = VB6.PixelsToTwipsX(eventArgs.X)
        Dim y As Single = VB6.PixelsToTwipsY(eventArgs.Y)

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then Exit Sub 'If TIP Run mode instrument is configured for test
        RangeUpdate(START_LEVEL)

    End Sub


    Private Sub txtStopEdge_Enter(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtStopEdge.Enter

        GotFocusSelect()

    End Sub


    Private Sub txtStopEdge_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtStopEdge.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = CShort(RegisterKeyPress(KeyAscii, STOP_EDGE))

        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtStopEdge_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtStopEdge.Leave

        RegisterEntry(STOP_EDGE)

    End Sub


    Private Sub txtStopEdge_MouseMove(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles txtStopEdge.MouseMove
        Dim Button As Short = eventArgs.Button \ &H100000
        Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
        Dim x As Single = VB6.PixelsToTwipsX(eventArgs.X)
        Dim y As Single = VB6.PixelsToTwipsY(eventArgs.Y)

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then Exit Sub 'If TIP Run mode instrument is configured for test
        RangeUpdate(STOP_EDGE)

    End Sub


    Private Sub txtStopLevel_Enter(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtStopLevel.Enter

        GotFocusSelect()

    End Sub



    Private Sub txtStopLevel_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtStopLevel.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = CShort(RegisterKeyPress(KeyAscii, STOP_LEVEL))

        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtStopLevel_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtStopLevel.Leave

        RegisterEntry(STOP_LEVEL)

    End Sub

    Private Sub txtStopLevel_MouseMove(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles txtStopLevel.MouseMove
        Dim Button As Short = eventArgs.Button \ &H100000
        Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
        Dim x As Single = VB6.PixelsToTwipsX(eventArgs.X)
        Dim y As Single = VB6.PixelsToTwipsY(eventArgs.Y)

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then Exit Sub 'If TIP Run mode instrument is configured for test
        RangeUpdate(STOP_LEVEL)

    End Sub


    Private Sub txtTrigLevel_Enter(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTrigLevel.Enter

        GotFocusSelect()

    End Sub


    Private Sub txtTrigLevel_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTrigLevel.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = CShort(RegisterKeyPress(KeyAscii, TRIG_LEV))

        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtTrigLevel_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTrigLevel.Leave

        RegisterEntry(TRIG_LEV)

    End Sub


    Private Sub txtTrigLevel_MouseMove(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles txtTrigLevel.MouseMove
        Dim Button As Short = eventArgs.Button \ &H100000
        Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
        Dim x As Single = VB6.PixelsToTwipsX(eventArgs.X)
        Dim y As Single = VB6.PixelsToTwipsY(eventArgs.Y)

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then Exit Sub 'If TIP Run mode instrument is configured for test
        RangeUpdate(TRIG_LEV)

    End Sub

    Private Sub txtVOffset1_Enter(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVOffset1.Enter

        GotFocusSelect()

    End Sub


    Private Sub txtVOffset1_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtVOffset1.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = CShort(RegisterKeyPress(KeyAscii, IN1_OFFSET))

        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtVOffset1_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVOffset1.Leave

        RegisterEntry(IN1_OFFSET)

    End Sub


    Private Sub txtVOffset1_MouseMove(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles txtVOffset1.MouseMove
        Dim Button As Short = eventArgs.Button \ &H100000
        Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
        Dim x As Single = VB6.PixelsToTwipsX(eventArgs.X)
        Dim y As Single = VB6.PixelsToTwipsY(eventArgs.Y)

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then Exit Sub 'If TIP Run mode instrument is configured for test
        RangeUpdate(IN1_OFFSET)

    End Sub
    Private Sub txtVOffset2_Enter(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVOffset2.Enter

        GotFocusSelect()

    End Sub


    Private Sub txtVOffset2_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtVOffset2.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = CShort(RegisterKeyPress(KeyAscii, IN2_OFFSET))

        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtVOffset2_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVOffset2.Leave

        RegisterEntry(IN2_OFFSET)

    End Sub


    Private Sub txtVOffset2_MouseMove(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles txtVOffset2.MouseMove
        Dim Button As Short = eventArgs.Button \ &H100000
        Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
        Dim x As Single = VB6.PixelsToTwipsX(eventArgs.X)
        Dim y As Single = VB6.PixelsToTwipsY(eventArgs.Y)

        If bDoNotTalk And CurrentMeas <> MEAS_OFF Then Exit Sub 'If TIP Run mode instrument is configured for test
        RangeUpdate(IN2_OFFSET)

    End Sub

Private Sub chk_Channel_On_Off(sender As Object, e As EventArgs) Handles _chkDisplayTrace_0.CheckedChanged, _chkDisplayTrace_1.CheckedChanged, _chkDisplayTrace_2.CheckedChanged, _chkDisplayTrace_3.CheckedChanged

    Dim tempCheckBox As CheckBox = sender

    If (tempCheckBox.Name = "_chkDisplayTrace_0") Then
       ChannelOn(CHANNEL_1) = tempCheckBox.Checked
       Me.cwgScopeDisplay.Plots(CHANNEL_1).Visible = tempCheckBox.Checked
       SelectTraces(CHANNEL_1, tempCheckBox.Checked)
    ElseIf (tempCheckBox.Name = "_chkDisplayTrace_1") Then
        ChannelOn(CHANNEL_2) = tempCheckBox.Checked
        Me.cwgScopeDisplay.Plots(CHANNEL_2).Visible = tempCheckBox.Checked
        SelectTraces(CHANNEL_2, tempCheckBox.Checked)
    ElseIf (tempCheckBox.Name = "_chkDisplayTrace_2") Then
        ChannelOn(MATH_AREA) = tempCheckBox.Checked
        Me.cwgScopeDisplay.Plots(MATH_AREA).Visible = tempCheckBox.Checked
        SelectTraces(MATH_AREA, tempCheckBox.Checked)
    ElseIf (tempCheckBox.Name = "_chkDisplayTrace_3") Then
        ChannelOn(MEMORY_AREA) = tempCheckBox.Checked
        Me.cwgScopeDisplay.Plots(MEMORY_AREA).Visible = tempCheckBox.Checked
        SelectTraces(MEMORY_AREA, tempCheckBox.Checked)
    End If



End Sub


Private Sub optDataPoints_ClickEvent(sender As Object, e As EventArgs) Handles _optDataPoints_1.Click, _optDataPoints_0.Click

    Dim Index As Short
    Dim tempRadioButtion As RadioButton = sender

    If (tempRadioButtion.Name = "_optDataPoints_0") Then
        Index = 0
    ElseIf (tempRadioButtion.Name = "_optDataPoints_1") Then
        Index = 1
    End If

    If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode
    SelectOption(WF_POINTS, Index)


End Sub


Private Sub optStandard_CheckChanged(sender As Object, e As EventArgs) Handles _optReference_0.CheckedChanged, _optReference_2.CheckedChanged, _optReference_1.CheckedChanged

    If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode

    Dim Index As Short
    Dim tempRadioButtion As RadioButton = sender

    If (tempRadioButtion.Name = "_optReference_0" And tempRadioButtion.Checked) Then
        Index = 0
    ElseIf (tempRadioButtion.Name = "_optReference_1" And tempRadioButtion.Checked) Then
        Index = 1
    ElseIf (tempRadioButtion.Name = "_optReference_2" And tempRadioButtion.Checked) Then
        Index = 2
    End If

    If (sender.Name <> "") Then
        SelectOption(H_REF_POS, Index)
    End If

End Sub

Private Sub cwgScopeDisplay_MouseMove(sender As Object, e As MouseEventArgs) Handles cwgScopeDisplay.MouseMove

        Dim plot As NationalInstruments.UI.IScatterGraph = sender
        If (plot.Cursors.Count > 0) Then
            dCursorX = Me.cwgScopeDisplay.Cursors(0).XPosition
            dCursorY = Me.cwgScopeDisplay.Cursors(0).YPosition

            UpdateCursorCaption(dCursorX, dCursorY)

        End If
End Sub


Private Sub _optHoldOff_CheckedChanged(sender As Object, e As EventArgs) Handles _optHoldOff_0.CheckedChanged, _optHoldOff_1.CheckedChanged

    If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode

    Dim Index As Short
    Dim tempRadioButtion As RadioButton = sender

    If (tempRadioButtion.Name = "_optHoldOff_0" And tempRadioButtion.Checked) Then
        Index = 0
    ElseIf (tempRadioButtion.Name = "_optHoldOff_1" And tempRadioButtion.Checked) Then
        Index = 1
    End If

    If (sender.Name <> "") Then
      SelectOption(HOLD_OFF, Index)
    End If

End Sub

Private Sub tbrMeasFunction_ButtonClick(sender As Object, e As ToolBarButtonClickEventArgs) Handles tbrMeasFunction.ButtonClick
  If bDoNotTalk Then EndProgram() 'Do not allow user to change settings if TIP Run mode

  Dim newToolbar As ToolBar = sender

  ChangeMeasurement(newToolbar.Buttons.IndexOf(e.Button))

End Sub

Private Sub chkContinuous_CheckedChanged(sender As Object, e As EventArgs) Handles chkContinuous.CheckedChanged
  If chkContinuous.Checked = True Then
            cmdReset.Enabled = False 'Disable RESET while in continuous mode
        Else
            cmdReset.Enabled = True
        End If
End Sub

Private Sub optProbe1_Click(sender As Object, e As EventArgs) Handles _optProbe1_0.Click, _optProbe1_3.Click, _optProbe1_2.Click, _optProbe1_1.Click

    Dim Index As Short
    Dim tempRadioButton As RadioButton = sender

    If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode

    If (tempRadioButton.Name = "_optProbe1_0") Then
        Index = 0
    ElseIf (tempRadioButton.Name = "_optProbe1_1") Then
        Index = 1
    ElseIf (tempRadioButton.Name = "_optProbe1_2") Then
        Index = 2
    ElseIf (tempRadioButton.Name = "_optProbe1_3") Then
        Index = 3
    End If

    SelectOption(IN1_ATTN_FAC, Index)

End Sub

Private Sub optCoupling1_Click(sender As Object, e As EventArgs) Handles _optCoupling1_0.Click, _optCoupling1_2.Click, _optCoupling1_1.Click


    Dim Index As Short
    Dim tempRadioButton As RadioButton = sender

    If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode

    If (tempRadioButton.Name = "_optCoupling1_0") Then
        Index = 0
    ElseIf (tempRadioButton.Name = "_optCoupling1_1") Then
        Index = 1
    ElseIf (tempRadioButton.Name = "_optCoupling1_2") Then
        Index = 2
    End If

    SelectOption(IN1_COUP, Index)

End Sub

Private Sub chkLFReject1_CheckedChanged(sender As Object, e As EventArgs) Handles chkLFReject1.CheckedChanged

    Dim tempCheckBox As CheckBox = sender

    If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode
    If tempCheckBox.Checked = True Then
        SelectOption(IN1_LFREJ, 1)
    Else
        SelectOption(IN1_LFREJ, 0)
    End If

End Sub

Private Sub chkHFReject1_CheckedChanged(sender As Object, e As EventArgs) Handles chkHFReject1.CheckedChanged

    Dim tempCheckBox As CheckBox = sender

    If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode
    If tempCheckBox.Checked = True Then
        SelectOption(IN1_HFREJ, 1)
    Else
        SelectOption(IN1_HFREJ, 0)
    End If

End Sub


Private Sub optProbe2_Click(sender As Object, e As EventArgs) Handles _optProbe2_0.Click, _optProbe2_3.Click, _optProbe2_2.Click, _optProbe2_1.Click

    Dim Index As Short
    Dim tempRadioButton As RadioButton = sender

    If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode  

    If (tempRadioButton.Name = "_optProbe2_0") Then
        Index = 0
    ElseIf (tempRadioButton.Name = "_optProbe2_1") Then
        Index = 1
    ElseIf (tempRadioButton.Name = "_optProbe2_2") Then
        Index = 2
    ElseIf (tempRadioButton.Name = "_optProbe2_3") Then
        Index = 3
    End If
    SelectOption(IN2_ATTN_FAC, Index)

End Sub

Private Sub optCoupling2_Click(sender As Object, e As EventArgs) Handles _optCoupling2_0.Click, _optCoupling2_1.Click, _optCoupling2_2.Click

    Dim Index As Short
    Dim tempRadioButton As RadioButton = sender

    If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode

    If (tempRadioButton.Name = "_optCoupling2_0") Then
        Index = 0
    ElseIf (tempRadioButton.Name = "_optCoupling2_1") Then
        Index = 1
    ElseIf (tempRadioButton.Name = "_optCoupling2_2") Then
        Index = 2
    End If

    SelectOption(IN2_COUP, Index)

End Sub


Private Sub chkLFReject2_CheckedChanged(sender As Object, e As EventArgs) Handles chkLFReject2.CheckedChanged

    Dim tempCheckBox As CheckBox = sender

    If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode
    If tempCheckBox.Checked = True Then
        SelectOption(IN2_LFREJ, 1)
    Else
        SelectOption(IN2_LFREJ, 0)
    End If

End Sub

Private Sub chkHFReject2_CheckedChanged(sender As Object, e As EventArgs) Handles chkHFReject2.CheckedChanged

    Dim tempCheckBox As CheckBox = sender

    If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode
    If tempCheckBox.Checked = True Then
        SelectOption(IN2_HFREJ, 1)
    Else
        SelectOption(IN2_HFREJ, 0)
    End If

End Sub

Private Sub optStartSlope_Click(sender As Object, e As EventArgs) Handles _optStartSlope_0.Click, _optStartSlope_1.Click

    Dim Index As Short
    Dim tempRadioButton As RadioButton = sender

    If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode

    If (tempRadioButton.Name = "_optStartSlope_0") Then
        Index = 0
    ElseIf (tempRadioButton.Name = "_optStartSlope_1") Then
        Index = 1
    End If

    ChangeDelayMeasParam(START_SLOPE, Index)

End Sub

Private Sub optStopSlope_Click(sender As Object, e As EventArgs) Handles _optStopSlope_0.Click, _optStopSlope_1.Click

    Dim Index As Short
    Dim tempRadioButton As RadioButton = sender

    If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode

    If (tempRadioButton.Name = "_optStopSlope_0") Then
        Index = 0
    ElseIf (tempRadioButton.Name = "_optStopSlope_1") Then
        Index = 1
    End If

    ChangeDelayMeasParam(STOP_SLOPE, Index)

End Sub

Private Sub optStartLevel_Click(sender As Object, e As EventArgs) Handles _optStartLevel_0.Click, _optStartLevel_1.Click

    Dim Index As Short
    Dim tempRadioButton As RadioButton = sender

    If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode

    If (tempRadioButton.Name = "_optStartLevel_0") Then
            Index = 0
            RadioButton2.Checked = True
    ElseIf (tempRadioButton.Name = "_optStartLevel_1") Then
            Index = 1
            RadioButton1.Checked = True
    End If

    ChangeDelayMeasParam(START_LEVEL, Index)
End Sub

Private Sub optTrigMode_Click(sender As Object, e As EventArgs) Handles _optTrigMode_0.Click, _optTrigMode_2.Click, _optTrigMode_1.Click

    Dim Index As Short
    Dim tempRadioButton As RadioButton = sender

    If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode

    If (tempRadioButton.Name = "_optTrigMode_0") Then
        Index = 0
    ElseIf (tempRadioButton.Name = "_optTrigMode_1") Then
        Index = 1
    ElseIf (tempRadioButton.Name = "_optTrigMode_2") Then
        Index = 2
    End If

    SelectOption(TRIG_MODE, Index)
End Sub

Private Sub optTrigSlope_Click(sender As Object, e As EventArgs) Handles _optTrigSlope_0.Click, _optTrigSlope_1.Click

    Dim Index As Short
    Dim tempRadioButton As RadioButton = sender

    If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode

    If (tempRadioButton.Name = "_optTrigSlope_0") Then
        Index = 0
    ElseIf (tempRadioButton.Name = "_optTrigSlope_1") Then
        Index = 1
    End If

    SelectOption(TRIG_SLOPE, Index)
End Sub

Private Sub optNoiseRej_Click(sender As Object, e As EventArgs) Handles _optNoiseRej_0.Click, _optNoiseRej_1.Click

    Dim Index As Short
    Dim tempRadioButton As RadioButton = sender

    If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode

    If (tempRadioButton.Name = "_optNoiseRej_0") Then
        Index = 0
    ElseIf (tempRadioButton.Name = "_optNoiseRej_1") Then
        Index = 1
    End If

    SelectOption(TRIG_SENS, Index)

End Sub

Private Sub optTrigDelay_Click(sender As Object, e As EventArgs) Handles _optTrigDelay_0.Click, _optTrigDelay_1.Click

    Dim Index As Short
    Dim tempRadioButton As RadioButton = sender

    If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode

    If (tempRadioButton.Name = "_optTrigDelay_0") Then
        Index = 0
    ElseIf (tempRadioButton.Name = "_optTrigDelay_1") Then
        Index = 1
    End If

    SelectOption(TRIG_DEL, Index)
End Sub

Private Sub optDelaySlope_Click(sender As Object, e As EventArgs) Handles _optDelaySlope_0.Click, _optDelaySlope_1.Click

    Dim Index As Short
    Dim tempRadioButton As RadioButton = sender

    If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode

    If (tempRadioButton.Name = "_optDelaySlope_0") Then
        Index = 0
    ElseIf (tempRadioButton.Name = "_optDelaySlope_1") Then
        Index = 1
    End If

    SelectOption(TRIG_DEL_SLOPE, Index)

End Sub

Private Sub optStandard_Click(sender As Object, e As EventArgs) Handles _optStandard_0.Click, _optStandard_1.Click

    Dim Index As Short
    Dim tempRadioButton As RadioButton = sender

    If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode

    If (tempRadioButton.Name = "_optStandard_0") Then
        Index = 0
    ElseIf (tempRadioButton.Name = "_optStandard_1") Then
        Index = 1
    End If

    SelectOption(TRIG_STAND, Index)

End Sub

Private Sub optField_Click(sender As Object, e As EventArgs) Handles _optField_0.Click, _optField_1.Click

    Dim Index As Short
    Dim tempRadioButton As RadioButton = sender

    If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode

    If (tempRadioButton.Name = "_optField_0") Then
        Index = 0
    ElseIf (tempRadioButton.Name = "_optField_1") Then
        Index = 1
    End If

    SelectOption(TRIG_FIELD, Index)

End Sub

Private Sub optExtTrigCoup_Click(sender As Object, e As EventArgs) Handles _optExtTrigCoup_0.Click, _optExtTrigCoup_1.Click

    Dim Index As Short
    Dim tempRadioButton As RadioButton = sender

    If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode

    If (tempRadioButton.Name = "_optExtTrigCoup_0") Then
        Index = 0
    ElseIf (tempRadioButton.Name = "_optExtTrigCoup_1") Then
        Index = 1
    End If

    SelectOption(EXT_TRIG_COU, Index)

End Sub

Private Sub chkExternalOutput_CheckedChanged(sender As Object, e As EventArgs) Handles chkExternalOutput.CheckedChanged

    Dim tempCheckBox As CheckBox = sender

    If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode
    If tempCheckBox.Checked = True Then
        SelectOption(OUT_TRIG_STATE, 1)
    Else
        SelectOption(OUT_TRIG_STATE, 0)
    End If
End Sub

Private Sub chkECL0_CheckedChanged(sender As Object, e As EventArgs) Handles chkECL0.CheckedChanged

    Dim tempCheckBox As CheckBox = sender

    If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode
    If tempCheckBox.Checked = True Then
        SelectOption(ECLT0_STATE, 1)
    Else
        SelectOption(ECLT0_STATE, 0)
    End If

End Sub

Private Sub chkECL1_CheckedChanged(sender As Object, e As EventArgs) Handles chkECL1.CheckedChanged

    Dim tempCheckBox As CheckBox = sender

    If bDoNotTalk And CurrentMeas <> MEAS_OFF Then EndProgram() 'Do not allow user to change settings if TIP Run mode
    If tempCheckBox.Checked = True Then
        SelectOption(ECLT1_STATE, 1)
    Else
        SelectOption(ECLT1_STATE, 0)
    End If

End Sub

Private Sub ribCompON_CheckedChanged(sender As Object, e As EventArgs) Handles ribCompON.CheckedChanged
    Dim newCheckBox As CheckBox = sender

    If newCheckBox.Checked = True Then
        imgProbeComp.Visible = True
        WriteMsg("BNC PROB")
        ribCompOFF.Checked = False
    End If

    'leave box checked if clicking again
    If (ribCompOFF.Checked = False) Then
        ribCompON.Checked = True
    End If

End Sub

Private Sub ribCompOFF_CheckedChanged(sender As Object, e As EventArgs) Handles ribCompOFF.CheckedChanged

    Dim newCheckBox As CheckBox = sender

    If newCheckBox.Checked = True Then
        imgProbeComp.Visible = False
        WriteMsg("BNC DCCAL")
        ribCompON.Checked = False
    End If

    'leave box checked if clicking again
    If (ribCompON.Checked = False) Then
        ribCompOFF.Checked = True
    End If

End Sub

    Public Function GetMode() As String
        GetMode = bInstrumentMode
    End Function

Public Sub SetMode(ByVal sMode As String)
        If sMode <> bInstrumentMode Then
            bInstrumentMode = sMode
            'Apply the instrument to the control
            'ChangeInstrumentMode()
            Dim i As Integer = 0
            For Each ToolBarButton As ToolBarButton In tbrMeasFunction.Buttons
                If (i = sMode) Then
                    ToolBarButton.Pushed = True
                Else
                    ToolBarButton.Pushed = False
                End If

                i = i + 1
            Next
            'Depress the corresponding button on the toolbar
        End If
End Sub

    Private Sub RadioButton2_Click(sender As Object, e As EventArgs) Handles RadioButton2.Click
        _optStartLevel_0.Checked = True
        optStartLevel_Click(_optStartLevel_0, e)
    End Sub

    Private Sub RadioButton1_Click(sender As Object, e As EventArgs) Handles RadioButton1.Click
        _optStartLevel_1.Checked = True
        optStartLevel_Click(_optStartLevel_1, e)
    End Sub
End Class