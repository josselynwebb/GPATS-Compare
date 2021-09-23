'Option Strict Off
Option Explicit On

Imports System.Text

Public Module modRFSynthesizerViperTSaif


    '=========================================================
    '**************************************************************
    '* Nomenclature   : VIPER/T SYSTEM SELF TEST                  *
    '*                  RF Synthesizer Test                       *
    '* Version        : 2.0                                       *
    '* Last Update    : Apr 1, 2017                               *
    '* Purpose        : This module contains code for the         *
    '*                  RF Synthesizer Self Test                  *
    '**************************************************************
    Dim gRetVal As Integer

    Function TestRFSynthesizerVT() As Integer
        'DESCRIPTION:
        '   This routine runs the RF Synthesizer OVP and returns PASSED or FAILED
        'RETURNS:
        '   PASSED if the RF Synthesizer test passes or FAILED if a failure occurs


        Dim i As Short

        Dim Freq(16) As String
        Dim Power(16) As Single
        Dim Tol(16) As Single
        Dim CableLoss(16) As Single
        Dim SSelfTest As String = ""
        Dim SelfTest As Integer
        Dim StrMeasurement As String = ""
        Dim HiLimit As Double
        Dim LoLimit As Double
        Dim calstat As Integer
        Dim measResult As Single
        ''Dim sMeasured As String 'Holds instrument return strings
        Dim dMeasured As Double 'Holds converted string values

        Dim sSensorSn As String = ""
        Dim MA_MeasResult As Object 'bb was double
        MA_MeasResult = CDbl(0)
        Dim MA_Capture As Object 'bb was double
        MA_Capture = CDbl(0)
        ''Dim sTestNum As String
        Dim testx As Integer
        Dim S As String
        Dim strng As String = Space(256)
        Dim LoopStepNo As Integer

        HelpContextID = 1340

        'RF Stimulus Test Title block
        EchoTitle(InstrumentDescription(RFSYN) & " ", True)
        EchoTitle("RF Stimulus Test", False)
        frmSTest.proProgress.Maximum = 100
        frmSTest.proProgress.Value = 1

        TestRFSynthesizerVT = UNTESTED
        If AbortTest = True Then GoTo TestComplete
        AbortTest = False
        TestRFSynthesizerVT = PASSED


        '    '** Set Up Handles and instruments

        nSystErr = vxiClear(RFSYN)
        nSystErr = vxiClear(FGEN)

        If IDNResponse(RFSYN) = "50000" Then
            nSystErr = WriteMsg(RFSYN, "SCPI")
            Do
                nSystErr = WriteMsg(RFSYN, "SYST:ERR?")
                nSystErr = ReadMsg(RFSYN, StrMeasurement)
            Loop Until nSystErr = 0
        End If

RFS_1_1:  'RFS-01-001 ' RFSTIM BIT Self Test
        nSystErr = WriteMsg(RFSYN, "*RST")
        nSystErr = WriteMsg(RFSYN, "*TST?")
        WaitForResponse(RFSYN, 0.1)
        SelfTest = ReadMsg(RFSYN, SSelfTest)
        Echo("RFS-01-001     RF Stimulus Built-In Test...")
        If SelfTest <> 0 Then
            Echo(FormatResults(False, "Built-In Test Result = " & StripNullCharacters(SSelfTest)))
            RegisterFailure(RFSYN, "RFS-01-001", , , , , "RF Stimulus Built-in Test")
            TestRFSynthesizerVT = FAILED
            IncStepFailed()
            GoTo TestComplete ' no point in continuing
        Else
            Echo(FormatResults(True, "RF Stimulus Built-In Test"))
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "RF STIM" And OptionStep = 1 Then
            GoTo RFS_1_1
        End If
        frmSTest.proProgress.Value = 10

        'turn on the 10mhz output
        nSystErr = WriteMsg(RFSYN, ":SOUR:ROSC:SOUR INT")
        nSystErr = WriteMsg(RFSYN, ":OUTP:ROSC:STAT ON")

        '

RFS_1_2:  'RFS-01-002 RFPM BIT Tests
        ' first get the powerhead sensor number in simulation mode
        gRetVal = gRFPM.Close ' close session
        gRetVal = gRFPM.Open(1) : checkPMError(True) ' simulation mode
        gRetVal = gRFPM.setPowerHead("8481A") : checkPMError(True)
        gRetVal = gRFPM.getPowerHeadSerNum(sSensorSn) : checkPMError(True)
        gRetVal = gRFPM.Close ' close session

        If RunningEndToEnd = False Or LastCalibration <> "8481A" Then
            S = "Install the 8481A as follows:" + vbCrLf
            S = S + "1. Connect 8481A POWER SENSOR S/N " & sSensorSn & " to RF POWER METER SENSOR jack via W14 cable." + vbCrLf
            S += "2. Connect 8481A POWER SENSOR to RF POWER METER POWER REF jack." + vbCrLf
            DisplaySetup(S, "ST-8481A-1.jpg", 2) ' Psh_pro.jpg
        End If
        If AbortTest = True Then GoTo TestComplete

        gRetVal = gRFPM.Open(0) ' execution mode
        Sleep(3000)

        'check error, could have failed one of the RFPM bit tests
        If gRetVal <> 0 Then
            checkPMError(True)
            Echo(FormatResults(False, "RF Power Meter Built-in Test", "RFS-01-002"))
            RegisterFailure(RFPM, "RFS-01-002", , , , , "RF Power Meter Built-in Test")
            TestRFSynthesizerVT = RFPM
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            Echo(FormatResults(True, "RF Power Meter Built-In Test", "RFS-01-002"))
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "RF STIM" And OptionStep = 2 Then
            GoTo RFS_1_2
        End If
        frmSTest.proProgress.Value = 15

RFS_1_3:  'RFS-01-003 RFPM Calibration
        sMsg = "RFS-01-003 Calibrate 8481A Power Meter (about 45 seconds)"
        Echo(sMsg)

        'Zero The Power Meter
        gRetVal = gRFPM.setPowerHead("8481A") : checkPMError(True)
        gRetVal = gRFPM.setMeasureMode(0) : checkPMError(True)
        gRetVal = gRFPM.setMeasureUnits("dBm") : checkPMError(True)
        gRetVal = gRFPM.setPwrMeterOn_Off(1) : checkPMError(True) ' turns on power meter
        Sleep(5000)

        LastCalibration = ""
        gRetVal = gRFPM.doZeroAndCal
        If gRetVal = 0 Then
            Do
                gRetVal = gRFPM.getZeroCalStatus(calstat)
                If calstat = 0 Or gRetVal <> 0 Then
                    Exit Do
                End If
            Loop
            Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete
            Sleep(1000)
        End If
        If gRetVal <> 0 Then
            checkPMError(True)
            Echo(FormatResults(False, "8481A Power Meter Calibration", "RFS-01-003"))
            RegisterFailure(RFPM, , , , , , "    RF Power Meter Calibration")
            TestRFSynthesizerVT = RFPM
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            Echo(FormatResults(True, "8481A Power Meter Calibration"))
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "RF STIM" And OptionStep = 3 Then
            GoTo RFS_1_3
        End If
        frmSTest.proProgress.Value = 20

        '
RFS_1_4:  'RFS-01-004 RFPM Reference Power Level 50 Mhz, 1 mW (0db)
        ' Added this test to verify that the RF power meter is good here
        '  so if Synthesizer test fails at same power/freq, synthesizer is known bad
        gRetVal = gRFPM.setRefOsc(1) : checkPMError(True)
        gRetVal = gRFPM.setExpFreq(50, "MHz") : checkPMError(True)
        gRetVal = gRFPM.setMeasureUnits("W") : checkPMError(True)
        Sleep(5000) 'Allow Signal Settling Time
        For i = 1 To 3
            gRetVal = gRFPM.getMeasurement(measResult, "W") : checkPMError(True)
        Next i
        dMeasured = measResult

        sTNum = "RFS-01-004"
        RecordTest(sTNum, "8481A Measure Reference Power Level ", (0.001 * 0.95), (0.001 * 1.05), dMeasured, "W")
        If dMeasured < (0.001 * 0.95) Or dMeasured > (0.001 * 1.05) Then
            TestRFSynthesizerVT = RFPM
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "RF STIM" And OptionStep = 4 Then
            GoTo RFS_1_4
        End If

        ' Now setup to test the RF Synthesizer with the RF Power Meter
        ''        If RunningEndToEnd = True Then
        ''Step1:
        ''            S = "Move W201-P11,P13,P14 cables as follows:" + vbCrLf
        ''            S += "1. Move W201-P11 to RFSTIM PULSE. " + vbCrLf
        ''            S += "2. Move W201-P13 to RFSTIM AM. " + vbCrLf
        ''            S += "3. Move W201-P14 to RFSTIM FM. " + vbCrLf
        ''            DisplaySetup(S, "ST-W201-2.jpg", 3, True, 1, 3)
        ''            If AbortTest = True Then GoTo TestComplete

        ''Step2:
        ''            S = "Move 8481A POWER SENSOR to Power Meter Sensor Jack P5 on the Viper/T receiver."
        ''            DisplaySetup(S, "ST-RFS-8481A-2.jpg", 1, True, 2, 3)
        ''            If AbortTest = True Then GoTo TestComplete
        ''            If GoBack = True Then GoTo Step1

        ''Step3:
        ''            S = "Install a W22 cable from SAIF RF POWER METER INPUT TO SAIF RF STIM OUTPUT."
        ''            DisplaySetup(S, "ST-RFS-W22-2.jpg", 1, True, 3, 3)
        ''            MoveW22 = 2
        ''            If GoBack = True Then GoTo Step2

        ''        Else
        ''Step1b:
        ''            S = "Install the W201 cable as follows:" + vbCrLf
        ''            S += "1. Connect W201-P1  to SAIF J1. " + vbCrLf
        ''            S += "2. Connect W201-P3  to 10 MHZ REF. " + vbCrLf
        ''            S += "3. Connect W201-P4  to FG OUT. " + vbCrLf
        ''            S += "4. Connect W201-P11 to RFSTIM PULSE. " + vbCrLf
        ''            S += "5. Connect W201-P13 to RFSTIM AM. " + vbCrLf
        ''            S += "6. Connect W201-P14 to RFSTIM FM. " + vbCrLf
        ''            S += "7. Connect W201-P19 to C/T INPUT 1. " + vbCrLf
        ''            S += "8. The remaining W201 cables can be connected to their normal positions or left disconnected." + vbCrLf
        ''            DisplaySetup(S, "ST-W201-2.jpg", 8, True, 1, 3) ' Psh_pro.jpg
        ''            If AbortTest = True Then GoTo TestComplete

        ''Step2b:
        ''            S = "Move 8481A POWER SENSOR to Power Meter Sensor Jack P5 on the Viper/T receiver."
        ''            DisplaySetup(S, "ST-RFS-8481A-2.jpg", 1, True, 2, 3)
        ''            If AbortTest = True Then GoTo TestComplete
        ''            If GoBack = True Then GoTo Step1b

        ''Step3b:
        ''            S = "Install a W22 cable from SAIF RF POWER METER INPUT TO SAIF RF STIM OUTPUT."
        ''            DisplaySetup(S, "ST-RFS-W22-2.jpg", 1, True, 3, 3)
        ''            MoveW22 = 2
        ''            If GoBack = True Then GoTo Step2b

        ''        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete

        nSystErr = WriteMsg(RFSYN, "OUTPUT ON")

        'RFS-02-001 to 013 RFSTIM Frequency/Power Tests
        LoopStepNo = 5 '5 to 16
        Freq(1) = "50" : Tol(1) = 2 : Power(1) = "0" ' Note: this power/freq same as pm ref
        Freq(2) = "100" : Tol(2) = 2 : Power(2) = "-5"
        Freq(3) = "100" : Tol(3) = 2 : Power(3) = "0"
        Freq(4) = "100" : Tol(4) = 2 : Power(4) = "5"
        Freq(5) = "1000" : Tol(5) = 3 : Power(5) = "-5"
        Freq(6) = "1000" : Tol(6) = 3 : Power(6) = "0"
        Freq(7) = "1000" : Tol(7) = 3 : Power(7) = "5"
        Freq(8) = "8000" : Tol(8) = 5 : Power(8) = "-5"
        Freq(9) = "8000" : Tol(9) = 5 : Power(9) = "0"
        Freq(10) = "8000" : Tol(10) = 5 : Power(10) = "5"
        Freq(11) = "17000" : Tol(11) = 15 : Power(11) = "-5"
        Freq(12) = "17000" : Tol(12) = 15 : Power(12) = "0"
        Freq(13) = "17000" : Tol(13) = 15 : Power(13) = "5"

        For testx = 1 To 13
RFS_2_X:
            nSystErr = WriteMsg(RFSYN, "FREQ " & Freq(testx) & "MHZ")
            nSystErr = WriteMsg(RFSYN, "POW " & Power(testx) & "dBm")
            Delay(2) ' allow settle time

            gRetVal = gRFPM.setMeasureUnits("dBm") : checkPMError(True)
            gRetVal = gRFPM.setExpFreq(Freq(testx), "MHz") : checkPMError(True)
            For i = 1 To 5 ' flush the rfpm buffer
                gRetVal = gRFPM.getMeasurement(measResult, "dBm") : checkPMError(True)
            Next i
            dMeasured = measResult

            HiLimit = Power(testx) + Tol(testx)
            LoLimit = Power(testx) - Tol(testx)

            RecordTest("RFS-02-" + Format(testx, "000"), "RFSTIM " & " Freq=" & Freq(testx) & " Mhz" & " Power=" & Power(testx) & " dBm", LoLimit, HiLimit, dMeasured, "dBm ")

            If (dMeasured > HiLimit) Or (dMeasured < LoLimit) Then
                TestRFSynthesizerVT = FAILED
                IncStepFailed()
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else
                IncStepPassed()
            End If
            Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete
            If OptionMode = LOSmode And OptionTestName = "RF STIM" And OptionStep = LoopStepNo Then
                GoTo RFS_2_X
            End If
            If frmSTest.proProgress.Value < 55 Then
                frmSTest.proProgress.Value = frmSTest.proProgress.Value + 3 ' total =36+20=56
            End If
            LoopStepNo += 1
        Next testx

        nSystErr = WriteMsg(RFSYN, "OUTP OFF")
        nSystErr = WriteMsg(RFSYN, "*RST")

        gRetVal = gRFPM.Close
        frmSTest.proProgress.Value = 55
        If AbortTest = True Then GoTo TestComplete

bb1:
        '
        ' ******************************
        ' *** Test Modulation inputs ***
        ' ******************************
        'Connect RFSTIM to RFMEAS  (Note: RF Synthesizer is turned off)
        S = "Move one end of the W22 cable from RF POWER METER INPUT to RF MEAS INPUT." + vbCrLf
        DisplaySetup(S, "ST-RFS-W22-01.jpg", 1)
        MoveW22 = 1
        If AbortTest = True Then GoTo TestComplete

        'FG switch S402-1,2 connects FG to Pulse via W201 cable
        'Note: W201-P11 to RF STIM PULSE, W201-P13 to RF AM and W201-P14 to RF PM
        nSystErr = WriteMsg(SWITCH1, "CLOSE 1.3100") ' connect FG (P4) to pulse (P11)

RFS_3_1:  '  RFS-03-001  RFMEAS Bit test
        gRetVal = gRFMa.Close
        Delay(1)
        gRetVal = gRFMa.Open(0)

        'check error, could have failed one of the RF Meas bit tests
        If gRetVal <> 0 Then
            TestRFSynthesizerVT = RFMEAS
            checkRFMAError(True) ' echo error message
            Echo(FormatResults(False, "RF MEAS Built-In Test", "RFS-03-001"))
            RegisterFailure(RFMEAS, "RFS-03-001", , , , , "RF MEAS Built-in Test")
            IncStepFailed()
            GoTo TestComplete ' no point in continuing
        Else
            Echo(FormatResults(True, "RF MEAS Built-In Test", "RFS-03-001"))
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "RF STIM" And OptionStep = 17 Then
            GoTo RFS_3_1
        End If
        frmSTest.proProgress.Value = 60

RFS_4_1:  'RFS-04-001  RFSTIM CW Output power 50 Mhz, 0 dBm
        nSystErr = WriteMsg(RFSYN, "FREQ 50 MHZ")
        nSystErr = WriteMsg(RFSYN, "POW 0 DBM") '
        nSystErr = WriteMsg(RFSYN, ":OUTP ON")
        nSystErr = WriteMsg(RFSYN, ":FM:STATE OFF;:AM:STATE OFF;:PULM:STATE OFF")

        gRetVal = gRFMa.setCenterFreq(CDbl(50), "MHz") : checkRFMAError(True)
        gRetVal = gRFMa.setSpan(CDbl(100), "KHz") : checkRFMAError(True)
        gRetVal = gRFMa.setRFLevel(CDbl(0), "dBm") : checkRFMAError(True)
        gRetVal = gRFMa.setMeasSignalType(0) : checkRFMAError(True) ' AC unmodulated
        gRetVal = gRFMa.setMeasureUnits("dBm") : checkRFMAError(True)
        gRetVal = gRFMa.setMeasureMode(0) : checkRFMAError(True) 'capture a waveform
        gRetVal = gRFMa.setMaxTime(10) : checkRFMAError(True) ' 10 second timeout
        gRetVal = gRFMa.setTrigSource(9) : checkRFMAError(True) ' immediate trigger mode
        Sleep(1000)

        ' MA_MeasResult = CDbl(-99)
        gRetVal = gRFMa.getMeasurement(MA_Capture, "dBm") : checkRFMAError(True) ' dBm
        gRetVal = gRFMa.setMeasureMode(1) : checkRFMAError(True) ' measure power
        gRetVal = gRFMa.getMeasurement(MA_MeasResult, "dBm") : checkRFMAError(True) 'dBm
        dMeasured = MA_MeasResult
        RecordTest("RFS-04-001", "RFSTIM CW Output 50Mhz", -5, 5, dMeasured, "dBm ")
        If dMeasured < -5 Or dMeasured > 5 Then
            TestRFSynthesizerVT = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then
                TestRFSynthesizerVT = RFMEAS
                GoTo TestComplete
            End If
        Else
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "RF STIM" And OptionStep = 18 Then
            GoTo RFS_4_1
        End If
        frmSTest.proProgress.Value = 65

RFS_4_2:  'RFS-04-002  RFSTIM CW Output power 200Mhz, 5dBm

        nSystErr = WriteMsg(RFSYN, "FREQ 200 MHZ")
        nSystErr = WriteMsg(RFSYN, "POW 5 DBM") '
        nSystErr = WriteMsg(RFSYN, ":OUTP ON")
        nSystErr = WriteMsg(RFSYN, ":FM:STATE OFF;:AM:STATE OFF;:PULM:STATE OFF") ' no modulation

        gRetVal = gRFMa.setCenterFreq(CDbl(200), "MHz") : checkRFMAError(True)
        gRetVal = gRFMa.setSpan(CDbl(100), "KHz") : checkRFMAError(True)
        gRetVal = gRFMa.setRFLevel(CDbl(5), "dBm") : checkRFMAError(True)
        gRetVal = gRFMa.setMeasSignalType(0) : checkRFMAError(True) ' AC unmodulated
        gRetVal = gRFMa.setMeasureUnits("dBm") : checkRFMAError(True)
        gRetVal = gRFMa.setMeasureMode(0) : checkRFMAError(True) 'capture a waveform
        gRetVal = gRFMa.setMaxTime(10) : checkRFMAError(True) ' 10 second timeout
        gRetVal = gRFMa.setTrigSource(9) : checkRFMAError(True) ' immediate trigger mode
        Sleep(1000)

        ' MA_MeasResult = CDbl(-99)
        gRetVal = gRFMa.getMeasurement(MA_Capture, "dBm") : checkRFMAError(True) ' dBm
        gRetVal = gRFMa.setMeasureMode(1) : checkRFMAError(True) ' measure power
        gRetVal = gRFMa.getMeasurement(MA_MeasResult, "dBm") : checkRFMAError(True) 'dBm
        dMeasured = MA_MeasResult
        RecordTest("RFS-04-002", "RFSTIM CW Output 200Mhz", -20, 20, dMeasured, "dBm ")
        If dMeasured < -20 Or dMeasured > 20 Then
            TestRFSynthesizerVT = FAILED
            IncStepFailed()

            'Diagnostics, use scope to verify RF output power
            If OptionFaultMode = SOFmode Then
                ' Need diagnostics here
                nSystErr = WriteMsg(RFSYN, ":OUTP OFF") ' turn off output to protect the synthesizer
                ' RFMA is used to test RFSTIM output power at 200 Mhz, 5 db
                ' If test fails, use DSO to measure the VAV.  If VAV is bad, fail RFS else RFMA
                S = "Connect SAIF RF STIM OUTPUT to SCOPE INPUT 1 using W27 cable."
                DisplaySetup(S, "ST-RFS-W27-1.jpg", 1)
                If AbortTest = True Then GoTo TestComplete

                nSystErr = WriteMsg(RFSYN, ":OUTP ON")
                Delay(1)
                nSystErr = WriteMsg(OSCOPE, "*RST; *CLS")
                nSystErr = WriteMsg(OSCOPE, "SYST:LANG COMP")
                Delay(1)
                nSystErr = WriteMsg(OSCOPE, "BLAN CHAN2") 'TURN OFF CHAN2
                nSystErr = WriteMsg(OSCOPE, "VIEW CHAN1") 'TURN ON CHAN1
                nSystErr = WriteMsg(OSCOPE, "CHAN1:RANG 1V")
                nSystErr = WriteMsg(OSCOPE, "TRIG:LEV 0V")
                nSystErr = WriteMsg(OSCOPE, "TIM:RANG 20NS") ' 200 mhz, 4 cycles = 20ns
                nSystErr = WriteMsg(OSCOPE, "CHAN1:COUP DCF")
                nSystErr = WriteMsg(OSCOPE, "CHAN2:COUP DCF")
                nSystErr = WriteMsg(OSCOPE, "DIG CHAN1")
                nSystErr = WriteMsg(OSCOPE, "MEAS:SOUR CHAN1")
                nSystErr = WriteMsg(OSCOPE, "MEAS:VPP?")
                Delay(0.5)

                nSystErr = ReadMsg(OSCOPE, StrMeasurement)
                dMeasured = Val(StrMeasurement) ' about 0.8 VPP
                RecordTest("RFS-04-002-D01", "RFSTIM CW Output ", 0.6, 1.2, dMeasured, "VPP ")
                If dMeasured < 0.6 Or dMeasured > 1.2 Then
                    TestRFSynthesizerVT = FAILED 'scope test fails, call out synthesizer
                Else
                    TestRFSynthesizerVT = RFMEAS 'scope test passes, call out RFMEAS
                End If
                GoTo TestComplete
            End If
        Else
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "RF STIM" And OptionStep = 19 Then
            GoTo RFS_4_2
        End If
        frmSTest.proProgress.Value = 65

        'RFS-05-001 RFSTIM Pulse Modulation Frequency
        nSystErr = WriteMsg(FGEN, "*RST;*CLS")
        Sleep(30)
        nSystErr = WriteMsg(FGEN, "FUNC:SHAP SQU")
        nSystErr = WriteMsg(FGEN, "VOLT 2.4")
        nSystErr = WriteMsg(FGEN, "VOLT:OFFS 1.24")
        nSystErr = WriteMsg(FGEN, "FREQ 1E4")
        Sleep(30)
        nSystErr = WriteMsg(RFSYN, ":PULM:STATE ON")
        nSystErr = WriteMsg(FGEN, "OUTP ON")
        Sleep(1000)

RFS_5_1:
        gRetVal = gRFMa.setMeasureMode(0) : checkRFMAError(True) ' capture
        gRetVal = gRFMa.setMeasSignalType(3) : checkRFMAError(True) ' measure PM modulation
        gRetVal = gRFMa.setModFreq(8, "KHz") : checkRFMAError(True)
        gRetVal = gRFMa.getMeasurement(MA_Capture, "Hz") : checkRFMAError(True) ' Hz
        gRetVal = gRFMa.setMeasureMode(8) : checkRFMAError(True) ' measure frequency of a modulated signal
        gRetVal = gRFMa.getMeasurement(MA_MeasResult, "Hz") : checkRFMAError(True)
        dMeasured = MA_MeasResult
        RecordTest("RFS-05-001", "RFSTIM PM Frequency", 5000, 15000, dMeasured, "Hz")
        If dMeasured < 5000 Or dMeasured > 15000 Then
            TestRFSynthesizerVT = FAILED
            IncStepFailed()

            If OptionFaultMode = SOFmode Then
                'Diagnostics, could be FG, RFSTIM or RFMEAS
                ' Use C/T to verify FG, then use DSO to verify RFSTIM
                nSystErr = WriteMsg(SWITCH1, "OPEN 1.3100") ' disconnect FG (P4) from pulse (P11)
                nSystErr = WriteMsg(SWITCH1, "CLOSE 1.3102") ' connect FG (P4) to CT Input 1(P19)

                nSystErr = WriteMsg(COUNTER, "*RST;*CLS")
                Delay(1)
                nSystErr = WriteMsg(COUNTER, "INP1:IMP 50")
                nSystErr = WriteMsg(COUNTER, "INP2:IMP 50")
                nSystErr = WriteMsg(COUNTER, "SENS1:EVEN:LEV 1.0V")
                nSystErr = WriteMsg(COUNTER, "MEAS1:FREQ? 1E4")
                Delay(2) ' minimum 1 second required for accuracy

                nSystErr = ReadMsg(COUNTER, StrMeasurement)
                dMeasured = Val(StrMeasurement)
                RecordTest("RFS-05-001-D01", "Verify FG Output ", 9500, 10500, dMeasured, "Hz ")
                If dMeasured < 9500 Or dMeasured > 10500 Then
                    TestRFSynthesizerVT = FGEN
                    GoTo TestComplete
                End If

                nSystErr = WriteMsg(SWITCH1, "OPEN 1.3102") ' disconnect FG (P4) from CT Input 1(P19)
                nSystErr = WriteMsg(SWITCH1, "CLOSE 1.3100") ' reconnect  FG (P4) to pulse (P11)

                'Use DSO to Verify RFSYN output power
                nSystErr = WriteMsg(RFSYN, ":OUTP OFF") ' turn off output to protect the synthesizer
                ' RFMA is used to test RFSTIM output power at 200 Mhz, 5 db
                ' If test fails, use DSO to measure the VAV.  If VAV is bad, fail RFS else RFMA
                S = "Connect SAIF RF STIM OUTPUT to SCOPE INPUT 1 using W27 cable."
                DisplaySetup(S, "ST-RFS-W27-1.jpg", 1)
                If AbortTest = True Then GoTo TestComplete

                nSystErr = WriteMsg(RFSYN, ":OUTP ON")
                Delay(1)
                nSystErr = WriteMsg(OSCOPE, "*RST; *CLS")
                nSystErr = WriteMsg(OSCOPE, "SYST:LANG COMP")
                Delay(1)
                nSystErr = WriteMsg(OSCOPE, "BLAN CHAN2") 'TURN OFF CHAN2
                nSystErr = WriteMsg(OSCOPE, "VIEW CHAN1") 'TURN ON CHAN1
                nSystErr = WriteMsg(OSCOPE, "CHAN1:RANG 1V")
                nSystErr = WriteMsg(OSCOPE, "TRIG:LEV 1V")
                nSystErr = WriteMsg(OSCOPE, "TIM:RANG 0.5MS") ' 10 khz, 1 cycles = 20ns
                nSystErr = WriteMsg(OSCOPE, "CHAN1:COUP DCFIFTY")
                nSystErr = WriteMsg(OSCOPE, "CHAN2:COUP DCFIFTY")
                nSystErr = WriteMsg(OSCOPE, "DIG CHAN1")
                nSystErr = WriteMsg(OSCOPE, "MEAS:SOUR CHAN1")
                nSystErr = WriteMsg(OSCOPE, "MEAS:FREQ?")
                Delay(0.5)
                nSystErr = ReadMsg(OSCOPE, StrMeasurement)
                dMeasured = Val(StrMeasurement)
                RecordTest("RFS-05-001-D02", "Verify RFSTIM Output ", 5000, 15000, dMeasured, "Hz ")
                If dMeasured < 5000 Or dMeasured > 15000 Then
                    TestRFSynthesizerVT = FAILED 'scope test fails, call out synthesizer
                Else
                    TestRFSynthesizerVT = RFMEAS 'scope test passes, call out RFMEAS
                End If
                GoTo TestComplete
            End If
        Else
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "RF STIM" And OptionStep = 20 Then
            GoTo RFS_5_1
        End If
        frmSTest.proProgress.Value = 70

RFS_5_2:  'RFS-05-002 RFSTIM PM Phase Deviation
        gRetVal = gRFMa.setMeasureMode(0) : checkRFMAError(True) ' capture a waveform
        gRetVal = gRFMa.setMeasSignalType(3) : checkRFMAError(True) ' signal type phase modulation
        gRetVal = gRFMa.getMeasurement(MA_Capture, "DEG") : checkRFMAError(True) ' Deg
        gRetVal = gRFMa.setMeasureMode(17) : checkRFMAError(True) ' measuremode = Phase deviation of a PM signal
        gRetVal = gRFMa.getMeasurement(MA_MeasResult, "DEG") : checkRFMAError(True)
        dMeasured = MA_MeasResult
        RecordTest("RFS-05-002", "RFSTIM PM Phase Deviation", 30, 330, dMeasured, "Degrees")
        If dMeasured < 30 Or dMeasured > 330 Then
            TestRFSynthesizerVT = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then
                Echo("NOTE: Not able to diagnose PM Phase Deviation Errors.")
                Echo("Probable fault is either RFSTIM or RFMEAS.")
                TestRFSynthesizerVT = FAILED 'call out synthesizer
                GoTo TestComplete
            End If
        Else
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "RF STIM" And OptionStep = 21 Then
            GoTo RFS_5_2
        End If
        frmSTest.proProgress.Value = 75

RFS_6_1:  'RFS-06-001 RFSTIM AM Modulation Frequency
        '** Test Amplitude Modulation Input **
        nSystErr = WriteMsg(RFSYN, "POW 0 DBM")
        nSystErr = WriteMsg(FGEN, "OUTP OFF")
        nSystErr = WriteMsg(FGEN, "FUNC:SHAP SIN")
        nSystErr = WriteMsg(FGEN, "FREQ 1E4")
        nSystErr = WriteMsg(FGEN, "VOLT 1")
        nSystErr = WriteMsg(FGEN, "VOLT:OFFS 0")

        'Move the W15 connection from the RF STIM PULSE MODULATION INPUT
        '  to the RF STIM AM MODULATION INPUT.
        nSystErr = WriteMsg(SWITCH1, "OPEN 1.3100") ' disconnect FG (P4) to pulse (P11)
        nSystErr = WriteMsg(SWITCH1, "CLOSE 1.3103") ' connect FG (P4) to AM(P13)

        nSystErr = WriteMsg(RFSYN, ":FM:STATE OFF;:PULM:STATE OFF;:AM:STATE ON")
        nSystErr = WriteMsg(RFSYN, ":AM:DEPT 50;")

        nSystErr = WriteMsg(FGEN, "OUTP ON")
        Delay(1)
        nSystErr = WriteMsg(RFSYN, "FREQ 200 MHZ")

        gRetVal = gRFMa.setCenterFreq(CDbl(200), "MHz") : checkRFMAError(True)
        Sleep(500)
        gRetVal = gRFMa.setMeasureMode(0) : checkRFMAError(True) ' capture a waveform
        gRetVal = gRFMa.setMeasSignalType(1) : checkRFMAError(True) ' signal type = AM (amplitude modulation)
        gRetVal = gRFMa.getMeasurement(MA_Capture, "KHz") : checkRFMAError(True) ' Khz
        gRetVal = gRFMa.setMeasureMode(8) : checkRFMAError(True) ' measuremode = frequency of a modulated signal
        gRetVal = gRFMa.setMeasureUnits("Hz") : checkRFMAError(True)
        gRetVal = gRFMa.getMeasurement(MA_MeasResult, "KHz") : checkRFMAError(True)
        dMeasured = MA_MeasResult
        RecordTest("RFS-06-001", "RF STIM AM Frequency", 5000, 15000, dMeasured, "Hz")
        If dMeasured < 5000 Or dMeasured > 15000 Then
            TestRFSynthesizerVT = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then
                'Diagnostics, Probable fault could be RFSTIM or RFMA
                'Use DSO to Verify RFSYN
                nSystErr = WriteMsg(RFSYN, ":OUTP OFF") ' turn off output to protect the synthesizer
                ' RFMA is used to test RFSTIM output power at 200 Mhz, 5 db
                ' If test fails, use DSO to measure the VAV.  If VAV is bad, fail RFS else RFMA
                S = "Connect SAIF RF STIM OUTPUT to SCOPE INPUT 1 using W27 cable."
                DisplaySetup(S, "ST-RFS-W27-1.jpg", 1)
                If AbortTest = True Then GoTo TestComplete

                nSystErr = WriteMsg(RFSYN, ":OUTP ON")
                Delay(1)
                nSystErr = WriteMsg(OSCOPE, "*RST; *CLS")
                nSystErr = WriteMsg(OSCOPE, "SYST:LANG COMP")
                Delay(1)
                nSystErr = WriteMsg(OSCOPE, "BLAN CHAN2") 'TURN OFF CHAN2
                nSystErr = WriteMsg(OSCOPE, "VIEW CHAN1") 'TURN ON CHAN1
                nSystErr = WriteMsg(OSCOPE, "CHAN1:RANG 1V")
                nSystErr = WriteMsg(OSCOPE, "TRIG:LEV 0.0V")
                nSystErr = WriteMsg(OSCOPE, "TIM:RANG .5mS") ' 10 khz, 1 cycles = 20ns
                nSystErr = WriteMsg(OSCOPE, "CHAN1:COUP DCFIFTY")
                nSystErr = WriteMsg(OSCOPE, "CHAN2:COUP DCFIFTY")
                nSystErr = WriteMsg(OSCOPE, "DIG CHAN1")
                nSystErr = WriteMsg(OSCOPE, "MEAS:SOUR CHAN1")
                nSystErr = WriteMsg(OSCOPE, "MEAS:FREQ?")
                Delay(0.5)
                nSystErr = ReadMsg(OSCOPE, StrMeasurement)
                dMeasured = Val(StrMeasurement)
                RecordTest("RFS-06-001-D02", "Verify RFSTIM Output ", 5000, 15000, dMeasured, "Hz ")
                If dMeasured < 5000 Or dMeasured > 15000 Then
                    TestRFSynthesizerVT = FAILED 'scope test fails, call out synthesizer
                Else
                    TestRFSynthesizerVT = RFMEAS 'scope test passes, call out RFMEAS
                End If
                GoTo TestComplete
            End If
        Else
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "RF STIM" And OptionStep = 22 Then
            GoTo RFS_6_1
        End If
        frmSTest.proProgress.Value = 80


RFS_6_2:  'RFS-06-002 RFSTIM AM Index
        gRetVal = gRFMa.setPostDetectionFilter(1) : checkRFMAError(True) '15K low pass filter
        gRetVal = gRFMa.setMeasureMode(0) : checkRFMAError(True) ' capture a waveform
        gRetVal = gRFMa.setMeasSignalType(1) : checkRFMAError(True) ' signal type AM
        gRetVal = gRFMa.getMeasurement(MA_Capture, "PCT") : checkRFMAError(True) ' PCT
        gRetVal = gRFMa.setMeasureMode(24) : checkRFMAError(True) ' Modulation index of an AM signal
        gRetVal = gRFMa.getMeasurement(MA_MeasResult, "PCT") : checkRFMAError(True)
        dMeasured = MA_MeasResult
        RecordTest("RFS-06-002", "RF STIM AM Index", 25, 90, dMeasured, "%")
        If dMeasured < 25 Or dMeasured > 90 Then
            TestRFSynthesizerVT = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then
                Echo("NOTE: Not able to diagnose AM Index Errors.")
                Echo("Probable fault is either RFSTIM or RFMEAS.")
                TestRFSynthesizerVT = FAILED 'call out synthesizer
                GoTo TestComplete
            End If
        Else
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "RF STIM" And OptionStep = 23 Then
            GoTo RFS_6_2
        End If
        frmSTest.proProgress.Value = 85

        'RFS-07-001 RFSTim FM Modulation Frequency
        '    '** Test Frequency Modulation Input **
        nSystErr = WriteMsg(FGEN, "OUTP OFF")
        nSystErr = WriteMsg(RFSYN, ":AM:STATE OFF;:PULM:STATE OFF;:FM:STATE ON")
        nSystErr = WriteMsg(FGEN, "VOLT 94E-3")
        nSystErr = WriteMsg(FGEN, "FREQ 20000") ' 20 Khz

        'Move the W15 connection from the RF STIM AM MODULATION INPUT
        '   to the RF STIM FM MODULATION INPUT.
        nSystErr = WriteMsg(SWITCH1, "OPEN 1.3102") ' disconnect FG (P4) to AM(P13)
        nSystErr = WriteMsg(SWITCH1, "CLOSE 1.3101") ' connect FG (P4) to FM(P14)

        nSystErr = WriteMsg(FGEN, "OUTP ON")
        Delay(1)

RFS_7_1:
        nSystErr = WriteMsg(RFSYN, "FREQ 200 MHZ")
        gRetVal = gRFMa.setCenterFreq(CDbl(200), "MHz") : checkRFMAError(True)
        Sleep(500)
        gRetVal = gRFMa.setMeasureMode(0) : checkRFMAError(True) ' capture waveform
        gRetVal = gRFMa.setMeasSignalType(2) : checkRFMAError(True) 'type is FM
        gRetVal = gRFMa.getMeasurement(MA_Capture, "Hz") : checkRFMAError(True) ' Hz
        gRetVal = gRFMa.setMeasureMode(8) : checkRFMAError(True) ' measure frequency of a modulated signal
        gRetVal = gRFMa.setMeasureUnits("Hz") : checkRFMAError(True)
        gRetVal = gRFMa.getMeasurement(MA_MeasResult, "Hz") : checkRFMAError(True) 'Hz
        dMeasured = MA_MeasResult
        RecordTest("RFS-07-001", "RF Stim FM Frequency", 15000, 25000, dMeasured, "Hz") ' 20 Khz
        If dMeasured < 15000 Or dMeasured > 25000 Then
            TestRFSynthesizerVT = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then
                'Diagnostics,  Probable fault could be RFSTIM, RFMA
                'Use DSO to Verify RFSYN
                nSystErr = WriteMsg(RFSYN, ":OUTP OFF") ' turn off output to protect the synthesizer
                ' RFMA is used to test RFSTIM output power at 200 Mhz, 5 db
                ' If test fails, use DSO to measure the Modulation Freq.
                S = "Connect SAIF RF STIM OUTPUT to SCOPE INPUT 1 using W27 cable."
                DisplaySetup(S, "ST-RFS-W27-1.jpg", 1)
                If AbortTest = True Then GoTo TestComplete

                nSystErr = WriteMsg(RFSYN, ":OUTP ON")
                Delay(1)
                nSystErr = WriteMsg(OSCOPE, "*RST; *CLS")
                nSystErr = WriteMsg(OSCOPE, "SYST:LANG COMP")
                Delay(1)
                nSystErr = WriteMsg(OSCOPE, "BLAN CHAN2") 'TURN OFF CHAN2
                nSystErr = WriteMsg(OSCOPE, "VIEW CHAN1") 'TURN ON CHAN1
                nSystErr = WriteMsg(OSCOPE, "CHAN1:RANG 1V")
                nSystErr = WriteMsg(OSCOPE, "TRIG:LEV 0.1V")
                nSystErr = WriteMsg(OSCOPE, "TIM:RANG 4MS") ' 20 khz, 1 cycles = 20ns
                nSystErr = WriteMsg(OSCOPE, "CHAN1:COUP DCFIFTY")
                nSystErr = WriteMsg(OSCOPE, "CHAN2:COUP DCFIFTY")
                nSystErr = WriteMsg(OSCOPE, "DIG CHAN1")
                nSystErr = WriteMsg(OSCOPE, "MEAS:SOUR CHAN1")
                nSystErr = WriteMsg(OSCOPE, "MEAS:FREQ?")
                Delay(0.5)
                nSystErr = ReadMsg(OSCOPE, StrMeasurement)
                dMeasured = Val(StrMeasurement)
                RecordTest("RFS-07-001-D01", "Verify RFSTIM Output ", 15000, 25000, dMeasured, "Hz ")
                If dMeasured < 15000 Or dMeasured > 25000 Then
                    TestRFSynthesizerVT = FAILED 'scope test fails, call out synthesizer
                Else
                    TestRFSynthesizerVT = RFMEAS 'scope test passes, call out RFMEAS
                End If

                GoTo TestComplete
            End If
        Else
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "RF STIM" And OptionStep = 24 Then
            GoTo RFS_7_1
        End If
        frmSTest.proProgress.Value = 90

RFS_7_2:  'RFS-07-002 RFSTIM FM Deviation
        gRetVal = gRFMa.setMeasureMode(0) : checkRFMAError(True) ' capture waveform
        gRetVal = gRFMa.setMeasSignalType(2) : checkRFMAError(True) ' type = FM
        gRetVal = gRFMa.getMeasurement(MA_Capture, "Hz") : checkRFMAError(True) 'Hz
        gRetVal = gRFMa.setMeasureMode(14) : checkRFMAError(True) ' frequency deviation of a FM signal
        gRetVal = gRFMa.setMeasureUnits("Hz") : checkRFMAError(True)
        gRetVal = gRFMa.getMeasurement(MA_MeasResult, "Hz") : checkRFMAError(True)
        dMeasured = MA_MeasResult
        RecordTest("RFS-07-002", "RFSTIM FM Deviation", 100000, 1000000, dMeasured, "HZ")
        If dMeasured < 100000 Or dMeasured > 1000000 Then
            TestRFSynthesizerVT = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then
                Echo("NOTE: Not able to diagnose FM Deviation Errors.")
                Echo("Probable fault is either RFSTIM or RFMEAS.")
                TestRFSynthesizerVT = FAILED 'call out synthesizer
                GoTo TestComplete
            End If
        Else
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "RF STIM" And OptionStep = 25 Then
            GoTo RFS_7_2
        End If
        frmSTest.proProgress.Value = 95

        gRetVal = gRFMa.Close
        nSystErr = WriteMsg(RFSYN, "OUTP OFF")
        nSystErr = WriteMsg(RFSYN, "*RST")

        'RFS-08-001 RFSTIM 10 Mhz Reference Ouput
        'Test the 10 mhz output
        nSystErr = WriteMsg(RFSYN, ":SOUR:ROSC:SOUR INT") ' already on?
        nSystErr = WriteMsg(RFSYN, ":OUTP:ROSC:STAT ON")

        'Connect 10Mhz Reference (P3) to C/T Input 1 (P19)
        nSystErr = WriteMsg(SWITCH1, "RESET")
        Delay(1)
        nSystErr = WriteMsg(SWITCH1, "CLOSE 2.1007") 'S301-111,112
        nSystErr = WriteMsg(SWITCH1, "CLOSE 2.1005") 'S301-107,108
        Delay(1)

RFS_8_1:
        nSystErr = vxiClear(COUNTER)
        nSystErr = WriteMsg(COUNTER, "*RST; *CLS")
        Delay(1)
        nSystErr = WriteMsg(COUNTER, "INP1:IMP 50")
        nSystErr = WriteMsg(COUNTER, "INP2:IMP 50")
        nSystErr = WriteMsg(COUNTER, "SENS1:EVEN:LEV 0.0V")
        nSystErr = WriteMsg(COUNTER, "MEAS1:FREQ? 1E7")
        Delay(2) ' minimum 1 second required for accuracy

        nSystErr = ReadMsg(COUNTER, StrMeasurement)
        dMeasurement = Val(StrMeasurement)

        RecordTest("RFS-08-001", "RFSTIM 10 Mhz Reference Test", "9.95E+6", "10.05E+6", dMeasurement, "HZ", 6)
        If dMeasurement < 9950000 Or dMeasurement > 10050000 Then
            TestRFSynthesizerVT = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then
                'Diagnostics,  Probable fault could be C/T, RFSTIM
                'First disconnect C/T from 10 Mhz Reference
                nSystErr = WriteMsg(SWITCH1, "OPEN 2.1007") 'S301-111,112
                nSystErr = WriteMsg(SWITCH1, "OPEN 2.1005") 'S301-107,108

                'Use DSO to Verify RFSYN 10 Mhz output
                S = "Connect SAIF 10 MHZ to SCOPE INPUT 1 using W15 cable."
                DisplaySetup(S, "ST-RFS-W15-1.jpg", 1)
                If AbortTest = True Then GoTo TestComplete

                nSystErr = WriteMsg(OSCOPE, "*RST; *CLS")
                nSystErr = WriteMsg(OSCOPE, "SYST:LANG COMP")
                Delay(1)
                nSystErr = WriteMsg(OSCOPE, "BLAN CHAN2") 'TURN OFF CHAN2
                nSystErr = WriteMsg(OSCOPE, "VIEW CHAN1") 'TURN ON CHAN1
                nSystErr = WriteMsg(OSCOPE, "CHAN1:RANG 1V")
                nSystErr = WriteMsg(OSCOPE, "TRIG:LEV 0.1V")
                nSystErr = WriteMsg(OSCOPE, "TIM:RANG 400NS") ' 10 mhz, 4 cycles = 400ns
                nSystErr = WriteMsg(OSCOPE, "CHAN1:COUP DCFIFTY")
                nSystErr = WriteMsg(OSCOPE, "CHAN2:COUP DCFIFTY")
                nSystErr = WriteMsg(OSCOPE, "DIG CHAN1")
                nSystErr = WriteMsg(OSCOPE, "MEAS:SOUR CHAN1")
                nSystErr = WriteMsg(OSCOPE, "MEAS:FREQ?")
                Delay(0.5)
                nSystErr = ReadMsg(OSCOPE, StrMeasurement)
                dMeasured = Val(StrMeasurement)
                RecordTest("RFS-08-001-D01", "Verify RFSTIM 10 Mhz Output ", 9500000, 10500000, dMeasured, "HZ ")
                If dMeasured < 9500000 Or dMeasured > 10500000 Then
                    TestRFSynthesizerVT = FAILED 'synthesizer 10 Mhz output failed
                Else
                    TestRFSynthesizerVT = COUNTER 'C/T failed to measure 10 Mhz
                End If
                GoTo TestComplete
            End If
        Else
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "RF STIM" And OptionStep = 26 Then
            GoTo RFS_8_1
        End If

        nSystErr = WriteMsg(SWITCH1, "OPEN 2.1007") 'S301-111,112
        nSystErr = WriteMsg(SWITCH1, "OPEN 2.1005") 'S301-107,108
        frmSTest.proProgress.Value = 100



TestComplete:
        frmSTest.cmdAbort.Text = "Abort Test"
        frmSTest.CmdPause.Text = "Pause Test"
        nSystErr = WriteMsg(RFSYN, "OUTP OFF")
        nSystErr = WriteMsg(RFSYN, "*RST")
        nSystErr = WriteMsg(RFSYN, ":SOUR:ROSC:SOUR INT") ' 10MHZ REFERENCE OUTPUT
        nSystErr = WriteMsg(RFSYN, ":OUTP:ROSC:STAT OFF")
        nSystErr = WriteMsg(SWITCH1, "RESET")
        gRetVal = gRFPM.Reset : checkPMError(False)
        gRetVal = gRFPM.setRefOsc(0) : checkPMError(False)
        gRetVal = gRFPM.setPwrMeterOn_Off(0) : checkPMError(False)
        gRetVal = gRFPM.Close ' end session
        gRetVal = gRFMa.Reset
        gRetVal = gRFMa.Close
        Echo("")
        If AbortTest = True Then
            If TestRFSynthesizerVT = FAILED Then
                ReportFailure(RFSYN)
            Else
                ReportUnknown(RFSYN)
            End If
        ElseIf TestRFSynthesizerVT = FAILED Then
            ReportFailure(RFSYN)
        ElseIf TestRFSynthesizerVT = PASSED Then
            ReportPass(RFSYN)
        Else
            ReportUnknown(RFSYN)
        End If

    End Function

    Private Sub checkPMError(ByVal EchoErrMsg As Integer)
        Dim message As String
        Dim errorcode As Integer
        Dim ErrorSeverity As Integer
        Dim ErrorDescr As String = ""
        Dim MoreErrorInfo As String = ""

        If gRetVal <> 0 Then
            gRFPM.getError(errorcode, ErrorSeverity, ErrorDescr, 256, MoreErrorInfo, 256)
            message = "Error:  " & errorcode & " --" & ErrorDescr & ". " & MoreErrorInfo
            '  MsgBox message, vbCritical, CRFMS_ERROR
            If EchoErrMsg = True Then
                Echo(message)
            End If
        End If

    End Sub
    Private Sub checkRFMAError(ByVal EchoErrMsg As Integer)

        Dim message As String
        Dim errorcode As Integer
        Dim ErrorSeverity As Integer
        Dim ErrorDescr As String = ""
        Dim MoreErrorInfo As String = ""

        If gRetVal <> 0 Then
            gRFMa.getError(errorcode, ErrorSeverity, ErrorDescr, 256, MoreErrorInfo, 256)
            message = "Error:  " & errorcode & " --" & ErrorDescr & ". " & MoreErrorInfo
            '   MsgBox message, vbCritical, CRFMS_ERROR
            If EchoErrMsg = True Then
                Echo(message)
            End If
        End If
        Application.DoEvents()

    End Sub

End Module