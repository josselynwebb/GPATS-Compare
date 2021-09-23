'Option Strict Off
Option Explicit On

Imports System.Windows.Forms

Public Module modArbVTSaif


    '=========================================================
    '**************************************************************
    '* Nomenclature   : TETS SYSTEM SELF TEST                     *
    '*                  E1445A ARB Test                           *
    '* Version        : 2.0                                       *
    '* Last Update    : Apr 1, 2017                               *
    '* Purpose        : This module is the ARB self test          *
    '**************************************************************


    Public Function TestArbVT() As Integer
        'DESCRIPTION:
        '   This routine tests Arbitrary Function Generator and returns PASSED or FAILED
        'RETURNS:
        '   PASSED if the Arbitrary Function Generator test passes or FAILED if any failure occurs

        'Marker Out, Ref/Smpl In, Start Arm In, Stop Trig/ FSK/GATE In, Output 50/75
        Dim x As DialogResult, S As String
        Dim StrMeasurement As String = "", SelfTest As Integer, SelfTest1 As Integer
        Dim SMeasurement As String
        Dim ErrorString As String = ""
        Dim AllErrString As String
        Dim ErrorMsg As Integer
        Dim QueCount As Integer


        AllErrString = ""
        HelpContextID = 1210

        'Arbitrary Waveform Generator Test Title block
        EchoTitle(InstrumentDescription(ARB) & " ", True)
        EchoTitle("Arbitrary Waveform Generator Test", False)
        frmSTest.proProgress.Maximum = 100

        TestArbVT = UNTESTED
        If AbortTest = True Then GoTo TestComplete
        AbortTest = False

        'bb
        If RunningEndToEnd = False And FirstPass = True Then
            x = MsgBox("Is the W201 cable installed?", MsgBoxStyle.YesNo)
            If x <> DialogResult.Yes Then
                'Install the W201 cable
                S = "Remove all adapters/cables from the SAIF." & vbCrLf
                S += "Connect W201 cable to SAIF as follows:" & vbCrLf
                S += " 1. P1  to SAIF J1 connector." & vbCrLf
                S += " 2. P4  to SAIF FG OUTPUT." & vbCrLf
                S += " 3. P9  to SAIF ARB OUTPUT/7." & vbCrLf
                S += " 4. P10 to SAIF ARB OUTPUT." & vbCrLf
                S += " 5. P11 to SAIF ARB START ARM IN." & vbCrLf
                S += " 6. P12 to SAIF ARB MARKER OUT." & vbCrLf
                S += " 7. P13 to SAIF ARB REF/SMPL IN." & vbCrLf
                S += " 8. P14 to SAIF ARB STOP TRIG/FSK/GATE IN." & vbCrLf
                S += " 9. P15 to SAIF SCOPE INPUT 1." & vbCrLf
                S += "10. P19 to SAIF C/T INPUT 1."
                DisplaySetup(S, "ST-W201-1.jpg", 10)
                If AbortTest = True Then GoTo TestComplete
            End If
        End If
        TestArbVT = PASSED


        nSystErr = WriteMsg(OSCOPE, "SYST:LANG COMP")
        Delay(1)

        'ARB-01-001
Arb1:
        '** Perform Arb Power-up Self Test

        nSystErr = WriteMsg(ARB, "*RST;*CLS;*OPC?")
        nSystErr = WriteMsg(ARB, "*TST?;*OPC?")
        'To make it fail uncomment this for debug only
        '    nSystErr = WriteMsg ARB, "*TST?"
        '    SelfTest& = ReadMsg(ARB, StrMeasurement$)

        WaitForResponse(ARB, 0.1)

        Echo("ARB-01-001 ARB Built-In Test. . .")

        SelfTest = ReadMsg(ARB, StrMeasurement)
        If SelfTest <> 0 Or Val(StrMeasurement) <> 0 Then


            QueCount = 1
            Do
                QueCount += 1
                nSystErr = WriteMsg(ARB, "SYST:ERR?")
                SelfTest1 = ReadMsg(ARB, ErrorString)
                If InStr(3, ErrorString, "No error", CompareMethod.Binary) <> 5 Then 'error is next in que
                    AllErrString &= ErrorString & vbCrLf
                Else                    'no more errors in que
                    Exit Do
                End If
            Loop While QueCount < 30

            TestArbVT = FAILED
            Echo(FormatResults(False, "ARB BIT Test", "ARB-01-001"))
            RegisterFailure(ARB, "ARB-01-001", , , , , sGuiLabel(ARB) & " FAILED Built-in Test" & vbCrLf & "Error Details: " & AllErrString)

            Echo("Built-In Test Results:" & vbCrLf & AllErrString, 4)
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
            Echo(FormatResults(True, "Arbitrary Function Generator Built-In Test"))
        End If '1


        frmSTest.proProgress.Value = 15
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "ARB" And OptionStep = 1 Then
            GoTo Arb1
        End If

        'ARB-02-001
Arb2:
        '** Generate sign waves
        nsysterr = vxiClear(ARB)
        nSystErr = WriteMsg(ARB, "*RST")
        nSystErr = WriteMsg(ARB, "FREQ 1E+6")
        nSystErr = WriteMsg(ARB, "FUNC SIN")
        nSystErr = WriteMsg(ARB, "VOLT:UNIT:VOLT VPP")
        nSystErr = WriteMsg(ARB, "VOLT 3")
        nSystErr = WriteMsg(ARB, "OUTP:FILT:FREQ 10E6")
        nSystErr = WriteMsg(ARB, "OUTP:FILT ON")
        nSystErr = WriteMsg(ARB, "INIT")

        ' Connect P10 ARB OUTPUT to P19 C/T INPUT 1
        nSystErr = WriteMsg(SWITCH1, "CLOSE 2.3001") 'close S404-1,3   bb
        ' Connect P4 FG OUTPUT to P11 ARB START ARM IN
        nSystErr = WriteMsg(SWITCH1, "CLOSE 1.3100") 'close S402-1,2   bb
        Delay(2)

        nSystErr = vxiClear(COUNTER)

        nSystErr = WriteMsg(COUNTER, "*RST; *CLS")
        Delay(0.1)
        nSystErr = WriteMsg(COUNTER, "INP1:IMP 50")
        nSystErr = WriteMsg(COUNTER, "INP2:IMP 50")
        nSystErr = WriteMsg(COUNTER, "SENS1:EVEN:LEV .5")
        Delay(0.1)
        nSystErr = WriteMsg(COUNTER, "MEAS1:FREQ?")
        Delay(0.1)

        dMeasurement = FetchMeasurement(COUNTER, "ARB Sine Wave Generation Test", ".9E+6", "1.1E+6", "Hz", ARB, "ARB-02-001")
        If AbortTest = True Then GoTo TestComplete

        'if the frequency is out of range, then check which handle is in fault - ARB or Counter.
        If dMeasurement > 1100000 Or dMeasurement < 900000 Then
            IncStepFailed()
            dFH_Meas = dMeasurement
            'ARB-02-D01
            If InstrumentStatus(COUNTER) = PASSED And RunningEndToEnd Then
                Echo(FormatResults(False, "ARB Sine Wave Generation Test", "ARB-02-D01"))
                RegisterFailure(ARB, "ARB-02-D01", dFH_Meas, "Hz", 900000, 1100000, "ARB Sine Wave Generation Test")
                TestArbVT = FAILED
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            End If

            ' Disconnect P10 Arb Output from P19 C/T INPUT 1.
            nSystErr = WriteMsg(SWITCH1, "OPEN 2.3001") 'open  S404-1,3   bb
            ' Connect P10 Arb Output to P15 SCOPE INPUT 1.
            nSystErr = WriteMsg(SWITCH1, "CLOSE 2.3003") 'close S404-1,5   bb

            nSystErr = WriteMsg(OSCOPE, "*RST; *CLS")
            nSystErr = WriteMsg(OSCOPE, "AUT") 'Set Autoscale mode
            Delay(2)
            nSystErr = WriteMsg(OSCOPE, "DIG CHAN1")
            nSystErr = WriteMsg(OSCOPE, "MEAS:SOUR CHAN1")
            nSystErr = WriteMsg(OSCOPE, "MEAS:FREQ?")
            'ARB-02-D02
            dMeasurement = FetchMeasurement(OSCOPE, "Sine Wave Generation Test Diagnostic", ".90E+6", "1.1E+6", "Hz", ARB, "ARB-02-D02")

            'ARB-02-D03
            ' if dso measures bad, then the counter is bad, else arb is bad
            If dMeasurement < 1100000 And dMeasurement > 900000 Then
                Echo(FormatResults(False, "ARB Sine Wave Generation Test", "ARB-02-D03"))
                RegisterFailure(COUNTER, "ARB-02-D03", dFH_Meas, "Hz", 900000, 1100000, sGuiLabel(COUNTER) & " FAILED Sine Wave Generation Test Diagnostic")
                TestArbVT = COUNTER
            Else
                'ARB-02-D04
                Echo(FormatResults(False, "ARB Sine Wave Generation Test Diagnostic", "ARB-02-D04"))
                RegisterFailure(ARB, "ARB-02-D04", dFH_Meas, "Hz", 900000, 1100000, "Sine Wave Generation Test Diagnostic")
                TestArbVT = FAILED
            End If
            If OptionFaultMode = SOFmode Then GoTo TestComplete
            ' Disconnect P10 Arb Output from P15 SCOPE INPUT 1.
            nSystErr = WriteMsg(SWITCH1, "OPEN 2.3003") 'open S404-1,5
            ' Connect P10 Arb Output to P19 C/T INPUT 1.
            nSystErr = WriteMsg(SWITCH1, "CLOSE 2.3001") 'close  S404-1,3
        Else
            IncStepPassed()
        End If '2
        frmSTest.proProgress.Value = 30
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "ARB" And OptionStep = 2 Then
            GoTo Arb2
        End If

        'ARB-03-001
Arb3:
        '** Output Relay Test - Since it is assumed that frequency measurement test is passed, now disable the output relay.
        nSystErr = WriteMsg(ARB, "OUTP OFF")
        nSystErr = WriteMsg(COUNTER, "MEAS1:FREQ?")

        SMeasurement = ""
        ErrorMsg = ReadMsg(ARB, SMeasurement)


        If (Val(SMeasurement) < 9.0E+37 And SMeasurement <> "") Then
            '  If ErrorMsg& = 0 Or (Val(SMeasurement$) < 9E+37 And SMeasurement$ <> "") Then
            Echo(FormatResults(False, "ARB Output Relay Test", "ARB-03-001"))
            RegisterFailure(ARB, "ARB-03-001", Val(SMeasurement), "sec", , , "Output Relay Test")
            TestArbVT = FAILED
            IncStepPassed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
            Echo(FormatResults(True, "Output Relay Test", "ARB-03-001"))
        End If '3
        frmSTest.proProgress.Value = 40
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "ARB" And OptionStep = 3 Then
            GoTo Arb3
        End If

        'ARB-04-001
Arb4:
        '**Start Arm In Test
        nSystErr = WriteMsg(FGEN, "*RST;*CLR") 'Reset the FG to its default condition
        nSystErr = WriteMsg(FGEN, "FUNC:SHAP SIN") 'Set a waveform (sine)
        nSystErr = WriteMsg(FGEN, "FREQ 1E+6") '1MHz Frequency
        nSystErr = WriteMsg(FGEN, "VOLT 2.0")
        nSystErr = WriteMsg(FGEN, "OUTP OFF") 'Turn the main output off

        nSystErr = WriteMsg(ARB, "*RST;*CLS")

        nSystErr = WriteMsg(ARB, "ROSC:SOUR INT")
        nSystErr = WriteMsg(ARB, "FREQ 1E6") 'Set freq to 1 MHz
        nSystErr = WriteMsg(ARB, "VOLT 4VPP") 'Set AFG amplitude
        nSystErr = WriteMsg(ARB, "ARM:LAY2:SOUR EXT") 'External Start Arm source
        nSystErr = WriteMsg(ARB, "OUTP:FILT:FREQ 10E6")
        nSystErr = WriteMsg(ARB, "OUTP:FILT ON")
        nSystErr = WriteMsg(ARB, "INIT:IMM") 'Initiate

        nSystErr = vxiClear(COUNTER)
        nSystErr = WriteMsg(COUNTER, "*RST;*CLS")
        nSystErr = WriteMsg(COUNTER, "INP1:IMP 50")
        nSystErr = WriteMsg(COUNTER, "INP2:IMP 50")
        nSystErr = WriteMsg(COUNTER, "EVEN:LEV:AUTO ON") 'Place counter in auto-trigger mode
        nSystErr = WriteMsg(COUNTER, "MEAS1:FREQ?") 'Measure frequency input 1

        ErrorMsg = ReadMsg(ARB, SMeasurement)

        If Val(SMeasurement) < 9.0E+37 And SMeasurement <> "" Then
            TestArbVT = FAILED
            ' If ErrorMsg& = 0 Or (Val(SMeasurement$) < 9E+37 And SMeasurement$ <> "") Then
            Echo(FormatResults(False, "Start Arm In Timeout Test", "ARB-04-001"))
            RegisterFailure(ARB, "ARB-04-001", Val(SMeasurement), "sec", , , "Start Arm In Timeout Test")
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
            Echo(FormatResults(True, "Start Arm In Timeout Test", "ARB-04-001"))
        End If '5
        frmSTest.proProgress.Value = 50
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "ARB" And OptionStep = 4 Then
            GoTo Arb4
        End If

        'ARB-05-001
Arb5:
        nSystErr = WriteMsg(FGEN, "OUTP ON") 'Turn the main output on
        Delay(1)
        nSystErr = vxiclear(COUNTER)
        nSystErr = WriteMsg(COUNTER, "*RST;*CLS")
        nSystErr = WriteMsg(COUNTER, "INP1:IMP 50")
        nSystErr = WriteMsg(COUNTER, "INP2:IMP 50")
        nSystErr = WriteMsg(COUNTER, "EVEN:LEV:AUTO ON") 'Place counter in auto-trigger mode
        Delay(2)
        nSystErr = WriteMsg(COUNTER, "MEAS1:FREQ?") 'Measure frequency input 1

        dMeasurement = FetchMeasurement(COUNTER, "Start Arm In Measurement Test", ".95E+6", "1.05E+6", "Hz", ARB, "ARB-05-001")

        'If the test fails, check which device is in error - FG or ARB
        If dMeasurement < 950000 Or dMeasurement > 1050000 Then
            IncStepFailed()
            dFH_Meas = dMeasurement
            nSystErr = WriteMsg(SWITCH1, "OPEN 2.3001") 'open  S404-1,3   Disconnect P10 Arb Output from P19 C/T Input 1
            nSystErr = WriteMsg(SWITCH1, "OPEN 1.3100") 'open  S402-1,2   Disconnect P4 FG Output from P11 ARB Start ARM IN
            nSystErr = WriteMsg(SWITCH1, "CLOSE 1.3102") 'close S402-1,4   Connect P4 FG Output to P19 C/T Input 1
            Delay(0.5)
            nSystErr = WriteMsg(COUNTER, "*RST;*CLS")
            nSystErr = WriteMsg(COUNTER, "INP1:IMP 50")
            nSystErr = WriteMsg(COUNTER, "INP2:IMP 50")
            nSystErr = WriteMsg(COUNTER, "EVEN:LEV:AUTO ON")
            Delay(1)
            nSystErr = WriteMsg(COUNTER, "MEAS1:FREQ?")
            'ARB-05-D01
            dMeasurement = FetchMeasurement(COUNTER, "Start Arm In Measurement Test Diagnostic", "95E+4", "105E+4", "Hz", ARB, "ARB-05-D01")

            'ARB-05-D02
            If dMeasurement > 950000 And dMeasurement < 1050000 Then
                Echo("ARB-05-D02 Start Arm In Measurement Test Diagnostic")
                Echo(FormatResults(False, , , CStr(950000), CStr(1050000), CStr(dFH_Meas), "Hz"))
                RegisterFailure(ARB, "ARB-05-D02", dFH_Meas, "Hz", 950000, 1050000, "Start Arm In Measurement Test Diagnostic")
                TestArbVT = FAILED
            Else
                'ARB-05-D03
                Echo("ARB-05-D03 Start Arm In Measurement Test Diagnostic")
                Echo(FormatResults(False, , , CStr(950000), CStr(1050000), CStr(dFH_Meas), "Hz"))
                RegisterFailure(FGEN, "ARB-05-D03", dFH_Meas, "Hz", 950000, 1050000, "Start Arm In Measurement Test Diagnostic")
                TestArbVT = FGEN
            End If
            If OptionFaultMode = SOFmode Then GoTo TestComplete
            nSystErr = WriteMsg(SWITCH1, "OPEN 1.3102") 'open  S402-1,4 Disconnect P4 FG Output to P19 C/T Input 1
            nSystErr = WriteMsg(SWITCH1, "CLOSE 2.3001") 'close  S404-1,3   Connect P10 Arb Output from P19 C/T Input 1
            nSystErr = WriteMsg(SWITCH1, "CLOSE 1.3100") 'close  S402-1,2   Connect P4 FG Output from P11 ARB Start ARM IN
        Else
            IncStepPassed()
        End If '6

        frmSTest.proProgress.Value = 60
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "ARB" And OptionStep = 5 Then
            GoTo Arb5
        End If

        'ARB-06-001
Arb6:
        '** Gate In Test
        nSystErr = WriteMsg(FGEN, "OUTP OFF") 'Turn FG main output off
        nSystErr = WriteMsg(FGEN, "FUNC:SHAP SQU")
        nSystErr = WriteMsg(FGEN, "VOLT:OFFS 1.25")
        nSystErr = WriteMsg(FGEN, "VOLT 2.5")
        nSystErr = WriteMsg(FGEN, "FREQ 200E3")
        nSystErr = WriteMsg(ARB, "*RST; *CLS")
        'Disconnect P4 FG Out from P11 Arb Start Arm In
        nSystErr = WriteMsg(SWITCH1, "OPEN 1.3100") 'open  S402-1,2   bb
        'Connect P4 FG Out from P14 Arb Stop Trig/FSK/Gate In
        nSystErr = WriteMsg(SWITCH1, "CLOSE 1.3101") 'close S402-1,3   bb
        Delay(0.5)

        nSystErr = WriteMsg(ARB, "FREQ 1E+6") 'Set freq to 1 MHz
        nSystErr = WriteMsg(ARB, "VOLT 4VPP") 'Set AFG amplitude
        nSystErr = WriteMsg(ARB, "TRIG:GATE:SOUR EXT")
        nSystErr = WriteMsg(ARB, "TRIG:GATE:STAT ON")
        nSystErr = WriteMsg(ARB, "OUTP:FILT:FREQ 10E6")
        nSystErr = WriteMsg(ARB, "OUTP:FILT ON")
        nSystErr = WriteMsg(ARB, "INIT:IMM") 'Initiate

        nSystErr = vxiClear(COUNTER)
        nSystErr = WriteMsg(COUNTER, "*RST;*CLS")
        nSystErr = WriteMsg(COUNTER, "INP1:IMP 50")
        nSystErr = WriteMsg(COUNTER, "INP2:IMP 50")
        nSystErr = WriteMsg(COUNTER, "EVEN:LEV:AUTO ON") 'Place counter in auto-trigger mode
        Delay(1)
        nSystErr = WriteMsg(COUNTER, "MEAS1:FREQ?") 'Measure frequency input 1

        dMeasurement = FetchMeasurement(COUNTER, "Gate In Off Test 1", ".95e6", "1.05e6", "Hz", ARB, "ARB-06-001")
        If dMeasurement > 1050000 Or dMeasurement < 950000 Then
            TestArbVT = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If '7

        frmSTest.proProgress.Value = 70
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "ARB" And OptionStep = 6 Then
            GoTo Arb6
        End If

        'ARB-07-001
Arb7:
        nSystErr = WriteMsg(FGEN, "OUTP ON") 'Turn the main output on

        nSystErr = vxiClear(COUNTER)
        nSystErr = WriteMsg(COUNTER, "*RST;*CLS")
        nSystErr = WriteMsg(COUNTER, "INP1:IMP 50")
        nSystErr = WriteMsg(COUNTER, "INP2:IMP 50")
        nSystErr = WriteMsg(COUNTER, "EVEN:LEV:AUTO ON") 'Place counter in auto-trigger mode
        nSystErr = WriteMsg(COUNTER, "MEAS1:FREQ?") 'Measure frequency input 1

        dMeasurement = FetchMeasurement(COUNTER, "Gate In On Test 2", "450E+3", "550E3", "Hz", ARB, "ARB-07-001")
        If dMeasurement < 450000 Or dMeasurement > 550000 Then
            TestArbVT = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If '8
        frmSTest.proProgress.Value = 80
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "ARB" And OptionStep = 7 Then
            GoTo Arb7
        End If

        'ARB-08-001
Arb8:
        nSystErr = WriteMsg(FGEN, "OUTP OFF")
        nSystErr = WriteMsg(ARB, "TRIG:GATE:STAT OFF")

        'Disconnect P10 ARB Output from P19 C/T Input 1
        nSystErr = WriteMsg(SWITCH1, "OPEN 2.3001") 'open  S404-1,3   bb
        'Connect P9 ARB Output/7 to P19 C/T Input 1
        nSystErr = WriteMsg(SWITCH1, "CLOSE 1.1000") 'close S301-1,2   bb

        nSystErr = WriteMsg(ARB, "VOLT 8VPP") 'Set AFG amplitude bb
        Delay(0.5)
        nSystErr = vxiClear(COUNTER)
        nSystErr = WriteMsg(COUNTER, "*RST;*CLS")
        nSystErr = WriteMsg(COUNTER, "INP1:IMP 50")
        nSystErr = WriteMsg(COUNTER, "INP2:IMP 50")
        nSystErr = WriteMsg(COUNTER, "EVEN:LEV:AUTO ON") 'Place counter in auto-trigger mode
        nSystErr = WriteMsg(COUNTER, "MEAS1:AC?") 'Measure AC RMS input 1

        dMeasurement = FetchMeasurement(COUNTER, "ARB Output/7 Test", ".3", ".6", "V", ARB, "ARB-08-001")
        If dMeasurement > 0.6 Or dMeasurement < 0.3 Then
            TestArbVT = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        frmSTest.proProgress.Value = 90
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "ARB" And OptionStep = 8 Then
            GoTo Arb8
        End If

        'ARB-09-001
Arb9:
        '** Ref In & Marker Out Test
        'Generate 1 MHz squarewave using Function Generator
        nSystErr = WriteMsg(FGEN, "*RST") 'Reset the FG to its default condition
        nSystErr = WriteMsg(FGEN, "FUNC:SHAP SQU") 'Set the output waveform th square
        nSystErr = WriteMsg(FGEN, "FREQ 1E+6") '1MHz Frequency
        nSystErr = WriteMsg(FGEN, "VOLT 2") '2VPK
        nSystErr = WriteMsg(FGEN, "OUTP ON") 'Turn the main output on

        'Disconnect P9 ARB Output/7 from P19 C/T Input 1
        nSystErr = WriteMsg(SWITCH1, "OPEN 1.1000") 'open  S301-1,2   bb
        'Connect P12 Arb Marker Out to P19 C/T Input 1
        nSystErr = WriteMsg(SWITCH1, "CLOSE 1.3002") 'close S401-1,4   bb
        'Disconnect P4 FG Out from P14 ARB STOP TRIG/FSK/GATE IN
        nSystErr = WriteMsg(SWITCH1, "OPEN 1.3101") 'open  S402-1,3   bb
        'Connect P4 FG Out from P13 ARB REF/SMPL IN.
        nSystErr = WriteMsg(SWITCH1, "CLOSE 1.3103") 'close S402-1,5   bb

        nSystErr = WriteMsg(ARB, "*RST")
        nSystErr = WriteMsg(ARB, "ROSC:SOUR EXT") 'External ref oscillator
        nSystErr = WriteMsg(ARB, "MARK:FEED ""ROSC""") 'Marker source is ROSC
        nSystErr = WriteMsg(ARB, "INIT:IMM") 'Initiate
        nSystErr = WriteMsg(ARB, "OUTP:FILT:FREQ 10E6")
        nSystErr = WriteMsg(ARB, "OUTP:FILT ON")
        nSystErr = vxiClear(COUNTER)
        nSystErr = WriteMsg(COUNTER, "*RST;*CLS")
        nSystErr = WriteMsg(COUNTER, "INP1:IMP 50")
        nSystErr = WriteMsg(COUNTER, "INP2:IMP 50")
        nSystErr = WriteMsg(COUNTER, "EVEN:LEV:AUTO ON") 'Place counter in auto-trigger mode
        Delay(1)
        nSystErr = WriteMsg(COUNTER, "MEAS1:FREQ?") 'Measure frequency input 1

        dMeasurement = FetchMeasurement(COUNTER, "ARB Ref/Sampl In, Marker Out Test", ".95E+6", "1.05E+6", "Hz", ARB, "ARB-09-001")
        'If the test fails, check which device is in error - FG, ARB, or SCOPE
        If dMeasurement > 1050000 Or dMeasurement < 950000 Then
            TestArbVT = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If '4
        If OptionMode = LOSmode And OptionTestName = "ARB" And OptionStep = 9 Then
            GoTo Arb9
        End If
        frmSTest.proProgress.Value = 100
        Application.DoEvents()

TestComplete:
        frmSTest.cmdAbort.Text = "Abort Test"
        frmSTest.cmdPause.Text = "Pause Test"
        nSystErr = WriteMsg(SWITCH1, "RESET") ' open all switches
        nSystErr = WriteMsg(ARB, "*RST;*CLS")
        nSystErr = WriteMsg(COUNTER, "*RST;*CLS")
        nSystErr = WriteMsg(FGEN, "*RST")
        nSystErr = WriteMsg(OSCOPE, "*RST; *CLS")
        If AbortTest = True Then
            If TestArbVT = FAILED Then
                ReportFailure(ARB)
            Else
                ReportUnknown(ARB)
            End If
        ElseIf TestArbVT = PASSED Then
            ReportPass(ARB)
        ElseIf TestArbVT = FAILED Then
            ReportFailure(ARB)
        Else
            ReportUnknown(ARB)
        End If


    End Function


End Module