'Option Strict Off
Option Explicit On


Public Module modRFmeasureVTSaif


    '=========================================================
    '**************************************************************
    '* Nomenclature   : VIPER/T SYSTEM SELF TEST                  *
    '*                  RF Measurement Analyzer Tests             *
    '* Version        : 2.0                                       *
    '* Last Update    : Apr 1, 2017                               *
    '* Purpose        : This module contains code for the         *
    '*                  RF Measurement Analizer Self Test         *
    '**************************************************************
    Dim gRetVal As Integer

    Public GetUnits As String

    Function TestRFMeasVT() As Integer
        'DESCRIPTION:
        '   This routine tests the RF Measurement Analyzer and returns PASSED or FAILED

        ' ****   DTS stuff
        Dim testResult As Integer
        Dim hndlDTI As Integer

        ' ****  RFMEAS
        Dim testx As Integer
        Dim InitStatus As Integer
        Dim i As Integer
        Dim SError As String 'Error Message
        Dim S As String
        Dim LoopStepNo As Integer

        Dim FreqList(15) As String
        Dim PowerList(15) As Single
        Dim Tolerance As Single

        Dim MA_MeasResult As Object 'bb was double
        MA_MeasResult = CDbl(0)
        Dim MA_Capture As Object 'bb was double
        MA_Capture = CDbl(0)
        Dim RFC_MeasResult As Double

        Dim errorMessage As String = ""
        Dim InstrumentX As Integer

        TestRFmeasVT = UNTESTED
        If AbortTest = True Then GoTo TestComplete
        AbortTest = False
        HelpContextID = 1350 ' RFMeas
        TestRFmeasVT = PASSED

        If RunningEndToEnd = False And FirstPass = True Then
            'Install the W201 cable
Step1:
            S = "Connect W201 cable to SAIF as follows:" + vbCrLf
            S += "1. W201-P1 to SAIF J1 connector." + vbCrLf
            S += "2. W201-P2 to RFMEAS TRIG IN." + vbCrLf
            S += "3. W201-P4 to FG OUTPUT." + vbCrLf
            S += "4. W201-P11 to RFSTIM PULSE. " + vbCrLf
            S += "5. The remaining W201 cables can be connected to their normal positions or left disconnected."
            DisplaySetup(S, "ST-W201-2.jpg", 5, True, 1, 2)
            If AbortTest = True Then GoTo TestComplete

Step2:
            S = "Connect W22 cable from RFMEAS to RFSTIM." + vbCrLf
            DisplaySetup(S, "ST-RFS-W22-01.jpg", 1, True, 2, 2)
            MoveW22 = 1
            If GoBack = True Then GoTo Step1

            If AbortTest = True Then GoTo TestComplete
        End If

        If MoveW22 <> 1 Then
            S = "Move one end of the W22 cable from RF POWER METER INPUT to RF MEAS INPUT." + vbCrLf
            DisplaySetup(S, "ST-RFS-W22-01.jpg", 1)
            MoveW22 = 1
        End If

        'RF Measurement Analyzer Test Title block
        EchoTitle(InstrumentDescription(RFMEAS) & " ", True)
        EchoTitle("RF Measurement Analyzer Test", False)
        frmSTest.proProgress.Maximum = 100
        frmSTest.proProgress.Value = 1


        'RFM-01-001
        '*** Reset Instruments ****
        nSystErr = vxiClear(RFSYN)
        nSystErr = WriteMsg(RFSYN, "*RST")

        'turn on the 10mhz output
        nSystErr = WriteMsg(RFSYN, ":SOUR:ROSC:SOUR INT")
        nSystErr = WriteMsg(RFSYN, ":OUTP:ROSC:STAT ON")

        S = "Waiting 60 seconds for the LO to lock onto the 10Mhz reference."
        Echo(S)
        Echo("Waiting  0") ' show wait progress in message window
        For i = 1 To 60
            Sleep(1000) ' delay 1 second
            Application.DoEvents()
            If i Mod 10 = 0 Then BumpProgress(i \ 10)
            If AbortTest = True Then GoTo testcomplete
        Next i


RFM_1_1:  'RFM-01-001 RF Measurement Analyzer Built-In Test
        '   '** Perform a Modulation Analyzer Power-up Self Test
        gRetVal = gRFMa.Close
        Delay(1)
        gRetVal = gRFMa.Open(0) ' Open also performs a BIT operation
        If gRetVal <> 0 Then 'FAILED BIT
            Echo(FormatResults(False, "RF Measurement Analyzer Built-In Test", "RFM-01-001"))
            RegisterFailure(RFMEAS, "RFM-01-001", , , , , "RF Measurement Analyzer Built-In Test")
            EncodeRFMSErrors(errorMessage, InstrumentX)
            If InstrumentX > 0 Then
                TestRFmeasVT = InstrumentX
            Else
                TestRFmeasVT = FAILED
            End If
            Echo(errorMessage)
            IncStepFailed()
            GoTo TestComplete ' no point in continuing
        Else            'PASSED BIT
            Echo(FormatResults(True, "RF Measurement Analyzer Built-In Test", "RFM-01-001"), False)
            IncStepPassed()
        End If
        If AbortTest = True Then GoTo TestComplete
        gRetVal = gRFMa.Reset : checkRFMAError(True)
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "RF MA" And OptionStep = 1 Then
            GoTo RFM_1_1
        End If
        frmSTest.proProgress.Value = 10
        LoopStepNo = 2

        'RFM-02-001 to RFM-02-006 RFMA frequency tests
        Tolerance = 3333

        FreqList(1) = "50000000" 'Synthesizer is known good at 50 Mhz, 0 db via POWER METER REF
        FreqList(2) = "11113333" '11.113333 Mhz
        FreqList(3) = "111113333"
        FreqList(4) = "1111113333"
        FreqList(5) = "11111113333"
        FreqList(6) = "17911113333"

        'init RFsyn
        nSystErr = WriteMsg(RFSYN, "OUTP On")
        nSystErr = WriteMsg(RFSYN, "pow:0 dbm")
        Sleep(1000)
        gRetVal = gRFMa.setTrigSource(9) : checkRFMAError(True) ' 9=immediate trigger mode
        gRetVal = gRFMa.setCenterFreq(CDbl(FreqList(1)), "Hz") : checkRFMAError(True)
        gRetVal = gRFMa.setSpan(CDbl(10000), "Hz") : checkRFMAError(True)
        gRetVal = gRFMa.setRFLevel(CDbl(0), "dBm") : checkRFMAError(True)
        gRetVal = gRFMa.setRBW(CDbl(1), "Hz") : checkRFMAError(True) ' resolution band width
        gRetVal = gRFMa.setMeasureUnits("Hz") : checkRFMAError(True)
        gRetVal = gRFMa.setMeasSignalType(0) : checkRFMAError(True) ' 0 = AC unmodulated
        Sleep(30) ' ms
        If AbortTest = True Then GoTo TestComplete

        Echo(vbCrLf + "RFMA Frequency Measurement Tests")

        For testx = 1 To 6
            ' If testx = 4 Then Stop
RFM_2_X:
            nSystErr = WriteMsg(RFSYN, "FREQ " & FreqList(testx) & "Hz")
            Sleep(1000) 'ms
            gRetVal = gRFMa.setCenterFreq(CDbl(FreqList(testx)), "Hz") : checkRFMAError(True)
            gRetVal = gRFMa.setMeasureMode(0) : checkRFMAError(True) ' capture a waveform
            gRetVal = gRFMa.getMeasurement(MA_Capture, "Hz") : checkRFMAError(True)
            gRetVal = gRFMa.setMeasureMode(6) : checkRFMAError(True) ' mode = signal frequency
            gRetVal = gRFMa.setMeasureUnits("Hz") : checkRFMAError(True)
            Sleep(100) 'ms
            MA_MeasResult = CDbl(0)
            gRetVal = gRFMa.getMeasurement(MA_MeasResult, "Hz") : checkRFMAError(True) '  rf input power too low
            dMeasurement = MA_MeasResult
            If (dMeasurement < (FreqList(testx) - Tolerance)) Or (dMeasurement > (FreqList(testx) + Tolerance)) Then                ' make the measurement again
                nSystErr = WriteMsg(RFSYN, "FREQ " & FreqList(testx) & "Hz")
                Sleep(1000) 'ms
                gRetVal = gRFMa.setCenterFreq(CDbl(FreqList(testx)), "Hz") : checkRFMAError(True)
                gRetVal = gRFMa.setMeasureMode(0) : checkRFMAError(True) ' capture a waveform
                gRetVal = gRFMa.getMeasurement(MA_Capture, "Hz") : checkRFMAError(True)
                gRetVal = gRFMa.setMeasureMode(6) : checkRFMAError(True) ' mode = signal frequency
                gRetVal = gRFMa.setMeasureUnits("Hz") : checkRFMAError(True)
                Sleep(100) 'ms
                MA_MeasResult = CDbl(0)
                gRetVal = gRFMa.getMeasurement(MA_MeasResult, "Hz") : checkRFMAError(True)
                dMeasurement = MA_MeasResult
            End If

            If Val(FreqList(testx)) < 1000000000.0 Then
                'note: RecordTest does data logging and fhdb
                RecordTest("RFM-02-" & Format(testx, "000"), "RFMEAS Freq=" & EngNotate(FreqList(testx), 6) + "Hz", ((FreqList(testx) - Tolerance)), ((FreqList(testx) + Tolerance)), (dMeasurement), "Hz", 6)
            Else
                RecordTest("RFM-02-" & Format(testx, "000"), "RFMEAS Freq=" & EngNotate(FreqList(testx), 9) + "Hz", ((FreqList(testx) - Tolerance)), ((FreqList(testx) + Tolerance)), (dMeasurement), "Hz", 9)
            End If
            If (dMeasurement < (FreqList(testx) - Tolerance)) Or (dMeasurement > (FreqList(testx) + Tolerance)) Then
                TestRFmeasVT = FAILED
                IncStepFailed()
                If OptionFaultMode = SOFmode Then
                    If testx = 1 Then
                        'Synthesizer is known good at 50 Mhz, 0 db via POWER METER REF
                        ' Probable fault is RFMA
                    Else
                        Echo("NOTE: Not able to diagnose frequency measurements above 50Mhz.")
                        Echo("Probable fault is either RFSTIM or RFMEAS.")
                    End If
                    GoTo TestComplete
                End If
            Else
                IncStepPassed()
            End If
            Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete
            If OptionMode = LOSmode And OptionTestName = "RF MA" And OptionStep = LoopStepNo Then
                GoTo RFM_2_X
            End If
            If frmSTest.proProgress.Value < 25 Then
                frmSTest.proProgress.Value = frmSTest.proProgress.Value + 3 ' 13 to 25
            End If
            LoopStepNo += 1 '2 to 7
        Next testx
        frmSTest.proProgress.Value = 25

        'RFM-03-001 to RFM-03-006 RFMA power tests
        'init RFsyn
        nSystErr = WriteMsg(RFSYN, "OUTP On")
        nSystErr = WriteMsg(RFSYN, "pow:0 dbm")
        nSystErr = WriteMsg(RFSYN, "FREQ " & FreqList(1) & "Hz")
        Sleep(500) ' ms

        'init RFmeas
        gRetVal = gRFMa.setTrigSource(9) : checkRFMAError(True) ' 9=immediate trigger mode
        gRetVal = gRFMa.setCenterFreq(CDbl(FreqList(1)), "Hz") : checkRFMAError(True)
        gRetVal = gRFMa.setSpan(CDbl(100), "KHz") : checkRFMAError(True)
        gRetVal = gRFMa.setRFLevel(CDbl(0), "dBm") : checkRFMAError(True)
        gRetVal = gRFMa.setRBW(CDbl(1), "KHz") : checkRFMAError(True) ' resolution band width
        gRetVal = gRFMa.setMeasSignalType(0) : checkRFMAError(True) ' AC unmodulated
        gRetVal = gRFMa.setMeasureUnits("dBm") : checkRFMAError(True)
        gRetVal = gRFMa.setMeasureMode(0) : checkRFMAError(True) 'capture a waveform
        gRetVal = gRFMa.setMaxTime(10) : checkRFMAError(True) ' 10 second timeout
        Sleep(1000)
        gRetVal = gRFMa.setRFLevel(CDbl(0), "dBm") : checkRFMAError(True)
        gRetVal = gRFMa.setCenterFreq(CDbl(FreqList(1)), "Hz") : checkRFMAError(True)

        If AbortTest = True Then GoTo TestComplete

        Echo("RFMA Power Measurement Tests")

        testx = 1
        For testx = 1 To 6
RFM_3_X:
            nSystErr = WriteMsg(RFSYN, "FREQ " & FreqList(testx) & "Hz")
            nSystErr = WriteMsg(RFSYN, "pow:0 dbm")
            Sleep(1000) '1 second settle time
            gRetVal = gRFMa.setRFLevel(CDbl(0), "dBm") : checkRFMAError(True)
            gRetVal = gRFMa.setCenterFreq(CDbl(FreqList(testx)), "Hz") : checkRFMAError(True)
            gRetVal = gRFMa.setMeasureMode(0) : checkRFMAError(True) ' capture a waveform
            gRetVal = gRFMa.getMeasurement(MA_Capture, "dBm") : checkRFMAError(True) ' dBm
            gRetVal = gRFMa.setMeasureMode(1) : checkRFMAError(True) ' measure power
            gRetVal = gRFMa.setMeasureUnits("dBm") : checkRFMAError(True)
            Sleep(100) 'ms
            gRetVal = gRFMa.getMeasurement(MA_MeasResult, "dBm") : checkRFMAError(True) 'dBm
            dMeasurement = MA_MeasResult

            If (dMeasurement < -10) Or (dMeasurement > 5) Then ' measure again
                nSystErr = WriteMsg(RFSYN, "FREQ " & FreqList(testx) & "Hz")
                nSystErr = WriteMsg(RFSYN, "pow:0 dbm")
                Sleep(1000) 'ms
                gRetVal = gRFMa.setRFLevel(CDbl(0), "dBm") : checkRFMAError(True)
                gRetVal = gRFMa.setCenterFreq(CDbl(FreqList(testx)), "Hz") : checkRFMAError(True)
                gRetVal = gRFMa.setMeasureMode(0) : checkRFMAError(True) ' capture a waveform
                gRetVal = gRFMa.getMeasurement(MA_Capture, "dBm") : checkRFMAError(True) ' dBm
                gRetVal = gRFMa.setMeasureMode(1) : checkRFMAError(True) ' measure power
                gRetVal = gRFMa.setMeasureUnits("dBm") : checkRFMAError(True)
                Sleep(100) 'ms
                gRetVal = gRFMa.getMeasurement(MA_MeasResult, "dBm") : checkRFMAError(True) 'dBm
                dMeasurement = MA_MeasResult
            End If

            If Val(FreqList(testx)) < 1000000000.0 Then '6dp
                RecordTest("RFM-03-" & Format(testx, "000"), "RFMEAS Stim 0 dBm at Freq=" & EngNotate(FreqList(testx), 6) + "Hz", -10, 5, dMeasurement, "dBm ")
            Else                '9dp
                RecordTest("RFM-03-" & Format(testx, "000"), "RFMEAS Stim 0 dBm at Freq=" & EngNotate(FreqList(testx), 9) + "Hz", -10, 5, dMeasurement, "dBm ")
            End If
            If (dMeasurement < -10) Or (dMeasurement > 5) Then
                TestRFmeasVT = FAILED
                IncStepFailed()
                If OptionFaultMode = SOFmode Then
                    'Need diagnostics here
                    If testx = 1 Then
                        'Synthesizer is known good at 50 Mhz, 0 db via POWER METER REF
                    Else
                        ' Probable fault could be either RFMA or RF Synthesizer
                        Echo("NOTE: Not able to diagnose power measurements over 50 Mhz.")
                        Echo("Probable fault is either RFSTIM or RFMEAS.")
                    End If
                    GoTo TestComplete
                End If
            Else
                IncStepPassed()
            End If
            Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete
            If OptionMode = LOSmode And OptionTestName = "RF MA" And OptionStep = LoopStepNo Then
                GoTo RFM_3_X
            End If
            If frmSTest.proProgress.Value < 40 Then ' +72
                frmSTest.proProgress.Value = frmSTest.proProgress.Value + 3 '18-40
            End If
            LoopStepNo += 1 '8 to 13
        Next testx
        frmSTest.proProgress.Value = 40

        'RFM-04-001
        ' init DTS
        InitStatus = terM9_init("TERM9::#0", VI_TRUE, VI_TRUE, hndlDTI) ' gets the handle
        If InitStatus > &H4FFF0000 Then
            SError = "    Initialization failure.  Most likely faulty module: CCRB."
            Echo(SError)
            TestRFmeasVT = DIGITAL
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        End If

RFM_4_1:  'RF MEAS EXTERNAL TRIGGER TEST
        nSystErr = WriteMsg(RFSYN, "FREQ 2000 MHZ")
        nSystErr = WriteMsg(RFSYN, "POW 0 DBM")
        nSystErr = WriteMsg(RFSYN, ":OUTP ON")
        nSystErr = WriteMsg(RFSYN, ":FM:STATE OFF;:AM:STATE OFF;:PULM:STATE OFF")
        Sleep(1000) '1 sec
        gRetVal = gRFMa.setMaxTime(10) : checkRFMAError(True) ' 10 second timeout
        gRetVal = gRFMa.setTrigSource(8) : checkRFMAError(True) ' 8 = EXTERNAL TRIGGER
        gRetVal = gRFMa.setTrigLevel(1) : checkRFMAError(True)
        gRetVal = gRFMa.setTrigSlope(0) : checkRFMAError(True) ' 0=positive
        gRetVal = gRFMa.setRFLevel(CDbl(0), "dBm") : checkRFMAError(True)
        gRetVal = gRFMa.setCenterFreq(CDbl(2000), "MHz") : checkRFMAError(True)
        gRetVal = gRFMa.setSpan(CDbl(100), "KHz") : checkRFMAError(True)
        gRetVal = gRFMa.setRBW(CDbl(1), "KHz") : checkRFMAError(True) ' resolution band width
        gRetVal = gRFMa.setAttenuator(0) : checkRFMAError(True)
        gRetVal = gRFMa.setMeasSignalType(0) : checkRFMAError(True)
        gRetVal = gRFMa.setMeasureUnits("dBm") : checkRFMAError(True)

        frmSTest.proProgress.Value = 45

        ' starts a DTB that creates a continuous pulse on DTS Ch191 (RFMEAS trig in)
        nSystErr = terM9_executeDigitalTest(hndlDTI, ProgramPath & "RFMEAS_TRIGon.dtb", VI_TRUE, testResult)

        Sleep(1000) 'mS
        gRetVal = gRFMa.setMeasureMode(0) : checkRFMAError(True)
        Sleep(100) 'mS
        gRetVal = gRFMa.getMeasurement(MA_Capture, "dBm") : checkRFMAError(True) ' dBm
        Sleep(30) 'ms
        If gRetVal < 0 Then ' error from RFMA
            TestRFmeasVT = FAILED
            Echo(FormatResults(False, "RFMEAS External Trigger", "RFM-04-001"))
            RegisterFailure(RFMEAS, "RFM-04-001", , , , , "RF External Trigger Test")
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            gRetVal = gRFMa.setMeasureMode(7) : checkRFMAError(True) ' measure Signal Amplitude
            Sleep(100) 'ms
            gRetVal = gRFMa.getMeasurement(MA_MeasResult, "dBm") : checkRFMAError(True)
            dMeasurement = MA_MeasResult

            RecordTest("RFM-04-001", "RFMEAS External Trigger ", -10, 5, dMeasurement, "dBm ")
            If dMeasurement < -10 Or dMeasurement > 5 Then
                TestRFmeasVT = FAILED
                IncStepFailed()
                If OptionFaultMode = SOFmode Then
                    Echo("Check RF trigger cable installed at Digitizer pin ")
                    GoTo TestComplete
                End If
            Else
                IncStepPassed()
            End If
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "RF MA" And OptionStep = 14 Then
            GoTo RFM_4_1
        End If
        frmSTest.proProgress.Value = 50

        'stop the dtb
        nSystErr = terM9_executeDigitalTest(hndlDTI, ProgramPath & "RFMEAS_TRIGoff.dtb", VI_TRUE, testResult)
        nSystErr = terM9_initializeInstrument(hndlDTI)
        nSystErr = terM9_close(hndlDTI)

        'RFM-05-001 RF Counter Built-In Test
        'RFMEAS already verified RFSTIM at the same frequencies

        Tolerance = 3333
        FreqList(1) = "111113333" '111.13333 Mhz
        FreqList(2) = "1111113333"
        FreqList(3) = "11111113333"
        FreqList(4) = "17911113333"

        Echo(vbCrLf & "RF Counter Tests")

RFM_5_1:
        gRetVal = gRFCt.Close
        Delay(1)
        gRetVal = gRFCt.Open(0) ' Open also performs a BIT operation
        If gRetVal <> 0 Then 'FAILED BIT
            Echo(FormatResults(False, "RF Counter Built-In Test", "RFM-05-001"))
            RegisterFailure(RFMEAS, "RFM-05-001", , , , , "RF Counter Built-In Test")
            EncodeRFMSErrors(errorMessage, InstrumentX)
            If InstrumentX > 0 Then
                TestRFmeasVT = InstrumentX
            Else
                TestRFmeasVT = FAILED
            End If
            Echo(errorMessage)
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else            'PASSED BIT
            Echo(FormatResults(True, "RF Counter Built-In Test", "RFM-05-001"))
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "RF MA" And OptionStep = 15 Then
            GoTo RFM_5_1
        End If
        gRetVal = gRFCt.Reset
        frmSTest.proProgress.Value = 60

        'RFM-06-001 to RFM-06-004
        '   '** Perform a RF Counter Frequency Test
        'init RFsyn
        nSystErr = WriteMsg(RFSYN, "OUTP ON")
        nSystErr = WriteMsg(RFSYN, "POW:0 dBm")
        nSystErr = WriteMsg(RFSYN, "FREQ " & FreqList(1) & "Hz")
        Sleep(1000) ' mS

        'init RFcounter
        MA_MeasResult = CDbl(0)
        gRetVal = gRFCt.setTrigSource(9) : checkRFCounterError(True) ' 9=immediate trigger mode
        gRetVal = gRFCt.setMeasureUnits("Hz") : checkRFCounterError(True)
        ' gRetVal = gRFCt.getMeasureUnits(S): checkRFCounterError (True)
        gRetVal = gRFCt.setExpRFFreq(FreqList(2), "Hz") : checkRFCounterError(True)
        gRetVal = gRFCt.setExpRFLevel(CDbl(0), "dBm") : checkRFCounterError(True)
        gRetVal = gRFCt.setTrigLevel(0, "Vrms") : checkRFCounterError(True)
        gRetVal = gRFCt.setMeasureMode(2) : checkRFCounterError(True) 'RF freq
        gRetVal = gRFCt.setMaxTime(10) : checkRFCounterError(True) ' 10 second timeout
        Sleep(1000) 'mS

        If AbortTest = True Then GoTo TestComplete
        LoopStepNo = 16
        For testx = 1 To 4

RFM_6_X:
            nSystErr = WriteMsg(RFSYN, "FREQ " & FreqList(testx) & "Hz")
            Sleep(1000) 'ms
            gRetVal = gRFCt.setExpRFFreq(FreqList(testx), "Hz") : checkRFCounterError(True)
            Sleep(3) 'ms
            gRetVal = gRFCt.setMeasureMode(2) : checkRFCounterError(True) ' measure RF freq
            gRetVal = gRFCt.setMeasureUnits("Hz") : checkRFCounterError(True)
            Sleep(100)
            gRetVal = gRFCt.getMeasurement(RFC_MeasResult, "Hz") : checkRFCounterError(True)
            dMeasurement = RFC_MeasResult
            If (dMeasurement < (FreqList(testx) - Tolerance)) Or (dMeasurement > (FreqList(testx) + Tolerance)) Then
                nSystErr = WriteMsg(RFSYN, "FREQ " & FreqList(testx) & "Hz")
                Sleep(1000) 'mS
                gRetVal = gRFCt.setExpRFFreq(FreqList(testx), "Hz") : checkRFCounterError(True)
                Sleep(3) 'ms
                gRetVal = gRFCt.setMeasureMode(2) : checkRFCounterError(True) ' measure RF freq
                gRetVal = gRFCt.setMeasureUnits("Hz") : checkRFCounterError(True)
                Sleep(100) 'mS
                gRetVal = gRFCt.getMeasurement(RFC_MeasResult, "Hz") : checkRFCounterError(True)
                dMeasurement = RFC_MeasResult
            End If

            If Val(FreqList(testx)) < 1000000000.0 Then
                'note: RecordTest does data logging and fhdb
                RecordTest("RFM-06-" & Format(testx, "000"), "RF Counter Freq=" & EngNotate(FreqList(testx), 4) + "Hz", ((FreqList(testx) - Tolerance)), ((FreqList(testx) + Tolerance)), (dMeasurement), "Hz", 4)
            Else
                RecordTest("RFM-06-" & Format(testx, "000"), "RF Counter Freq=" & EngNotate(FreqList(testx), 7) + "Hz", ((FreqList(testx) - Tolerance)), ((FreqList(testx) + Tolerance)), (dMeasurement), "Hz", 7)
            End If
            If (dMeasurement < (FreqList(testx) - Tolerance)) Or (dMeasurement > (FreqList(testx) + Tolerance)) Then
                TestRFmeasVT = FAILED
                IncStepFailed()
                If OptionFaultMode = SOFmode Then
                    ' Probable fault is RF Counter, as RFMA, RFSTIM are known good
                    GoTo TestComplete
                End If
            Else
                IncStepPassed()
            End If
            Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete
            If OptionMode = LOSmode And OptionTestName = "RF MA" And OptionStep = LoopStepNo Then
                GoTo RFM_6_X
            End If
            If frmSTest.proProgress.Value < 80 Then
                frmSTest.proProgress.Value = frmSTest.proProgress.Value + 5 '61-80
            End If
            LoopStepNo += 1 '16 to 19
        Next testx

        nSystErr = WriteMsg(RFSYN, "OUTP OFF")
        frmSTest.proProgress.Value = 80


        '** Test RF Counter Pulse Period **
        'RFM-07-001 RF Counter Pulse Modulation Frequency
        ' FG switch S402-1,2 connects FG to Pulse
        nSystErr = WriteMsg(SWITCH1, "CLOSE 1.3100") ' connect FG (P4) to pulse (P11)

        'Note: W201-P11 to RF STIM PULSE, W201-P13 to RF AM and W201-P14 to RFPM
RFM_7_1:
        nSystErr = WriteMsg(FGEN, "*RST;*CLS")
        nSystErr = WriteMsg(FGEN, "FUNC:SHAP SQU")
        nSystErr = WriteMsg(FGEN, "VOLT 2.4")
        nSystErr = WriteMsg(FGEN, "VOLT:OFFS 1.24")
        nSystErr = WriteMsg(FGEN, "FREQ 1E4")

        nSystErr = WriteMsg(RFSYN, "FREQ 2000 MHZ")
        nSystErr = WriteMsg(RFSYN, "POW 5 DBM") '
        nSystErr = WriteMsg(RFSYN, ":OUTP ON")
        nSystErr = WriteMsg(RFSYN, ":FM:STATE OFF;:AM:STATE OFF;:PULM:STATE OFF")

        nSystErr = WriteMsg(RFSYN, ":PULM:STATE ON")
        nSystErr = WriteMsg(FGEN, "OUTP ON")
        Sleep(1000) 'mS

        gRetVal = gRFCt.setExpRFFreq("2000", "MHz") : checkRFCounterError(True)
        gRetVal = gRFCt.setExpRFLevel("5", "DBM") : checkRFCounterError(True)
        gRetVal = gRFCt.setMeasureMode(0) : checkRFCounterError(True) ' pulse width
        gRetVal = gRFCt.setMeasureUnits("uS") : checkRFCounterError(True)
        Sleep(100) 'mS
        gRetVal = gRFCt.getMeasurement(RFC_MeasResult, "uS") : checkRFCounterError(True)
        dMeasurement = RFC_MeasResult

        RecordTest("RFM-07-001", "RF Counter Pulse Period", 95, 105, dMeasurement, "uS")
        If dMeasurement < 95 Or dMeasurement > 105 Then
            TestRFmeasVT = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then
                'Probable fault is RF Counter, RFSTIM, RFmeas known good
                GoTo TestComplete
            End If
        Else
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "RF MA" And OptionStep = 20 Then
            GoTo RFM_7_1
        End If
        frmSTest.proProgress.Value = 100

TestComplete:

        frmSTest.cmdAbort.Text = "Abort Test"
        frmSTest.CmdPause.Text = "Pause Test"
        nSystErr = WriteMsg(RFSYN, "OUTP OFF") ' turn off synthesizer
        nSystErr = WriteMsg(RFSYN, "*RST")
        nSystErr = WriteMsg(FGEN, "OUTP OFF")
        nSystErr = WriteMsg(FGEN, "*RST")
        gRetVal = gRFMa.setTrigSource(9) ' immediate
        gRetVal = gRFMa.Reset
        gRetVal = gRFMa.Close ' end RF measure analyzer session
        gRetVal = gRFCt.Reset
        gRetVal = gRFCt.Close ' end RF counter session
        nSystErr = WriteMsg(SWITCH1, "RESET") ' open all switches

        Echo("")

        nSystErr = terM9_initializeInstrument(hndlDTI)
        nSystErr = terM9_close(hndlDTI)

        If AbortTest = True Then
            If TestRFmeasVT = FAILED Then
                ReportFailure(RFMEAS)
            Else
                ReportUnknown(RFMEAS)
            End If
        ElseIf TestRFmeasVT = PASSED Then
            ReportPass(RFMEAS)
        ElseIf (TestRFmeasVT = FAILED) Or (TestRFmeasVT = InstrumentX) Then
            ReportFailure(RFMEAS)
        Else
            ReportUnknown(RFMEAS)
        End If

    End Function


    Private Sub checkRFMAError(ByVal EchoErrMsg As Integer)

        Dim message As String
        Dim errorcode As Integer
        Dim ErrorSeverity As Integer
        Dim ErrorDescr As String = ""
        Dim MoreErrorInfo As String = ""

        Sleep(600) 'ms

        If gRetVal <> 0 Then
            gRFMa.getError(errorcode, ErrorSeverity, ErrorDescr, 256, MoreErrorInfo, 256)
            message = "Error:  " & errorcode & " --" & ErrorDescr & ". " & MoreErrorInfo
            '   MsgBox message, vbCritical, "RMFS Error"
            If EchoErrMsg = True Then
                Echo(message)
            End If
        End If

    End Sub

    Private Sub checkRFCounterError(ByVal EchoErrMsg As Integer)

        Dim message As String
        Dim errorcode As Integer
        Dim ErrorSeverity As Integer
        Dim ErrorDescr As String = ""
        Dim MoreErrorInfo As String = ""

        If gRetVal <> 0 Then
            gRFCt.getError(errorcode, ErrorSeverity, ErrorDescr, 256, MoreErrorInfo, 256)
            message = "Error:  " & errorcode & " --" & ErrorDescr & ". " & MoreErrorInfo
            '   MsgBox message, vbCritical, "RMFS Error"
            If EchoErrMsg = True Then
                Echo(message)
            End If
        End If

    End Sub


    Sub EncodeRFMSErrors(ByRef errorMessage As String, ByRef InstrumentX As Integer)

        ' This routine returns errorMessage and InstrumentX

        Dim ErrorSeverity As Integer
        Dim ErrorDescr As String = "" 'Dim ErrorDescr As String * 256 ?
        Dim MoreErrorInfo As String = "" 'Dim MoreErrorInfo As String * 256 ?
        Dim temp As String = ""
        Dim errorcode As Integer

        'attach error codes to the instrument that caused the error
        gRFMa.getError(errorcode, ErrorSeverity, ErrorDescr, 256, MoreErrorInfo, 256)
        InstrumentX = 0
        errorMessage = "RFMS error:" + Str(errorcode) + vbCrLf
        errorMessage += ErrorDescr + vbCrLf
        If MoreErrorInfo <> "" Then
            errorMessage += MoreErrorInfo + vbCrLf
        End If
        Select Case errorcode
            Case 13 To 17
                'find which instrument failed  (power failures)
                Select Case errorcode ' either PM1313B or PM20309 voltage errors
                    Case 13
                        temp = "+10V"
                    Case 14
                        temp = "+21V"
                    Case 15
                        temp = "-21V"
                    Case 16
                        temp = "-10V"
                    Case 17
                        temp = "+5V"
                End Select
                errorMessage += "VXI Chassis " + temp + " Power Failure!" + vbCrLf
            Case 25, 26, 27
                'PM1313B errors (Down converter)
                errorMessage += "PM1313B (Down Converter) Failed BIT!" + vbCrLf
                InstrumentX = DOWNCONV
            Case 28, 29, 30
                'PM20309 errors (local oscillator)
                errorMessage += "PM20309 (Local Oscillator) Failed BIT!" + vbCrLf
                InstrumentX = LOCALOSC
            Case 34
                'VM2601 digitizer error
                errorMessage += "VM2601 (Digitizer) Failed BIT!" + vbCrLf
                InstrumentX = DIGITIZER
            Case 41, 42
                'VM7510 calibration module errors (same card as Digitizer VM2601))
                errorMessage += "VM7510 (Calibrator) Failed BIT!" + vbCrLf
                InstrumentX = CALIBRATOR

            Case Else
        End Select

    End Sub


End Module