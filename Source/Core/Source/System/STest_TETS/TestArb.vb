'Option Strict Off
Option Explicit On

Imports System.Windows.Forms

Public Module modTestArb

    '**************************************************************
    '* Nomenclature   : ATS-TETS SYSTEM SELF TEST                 *
    '*                  E1445A ARB Test                           *
    '* Version        : 2.0                                       *
    '* Last Update    : Apr 1, 2017                               *
    '* Purpose        : This module is the ARB self test          *
    '**************************************************************
   Dim S As String

    Public Function TestArb() As Integer
        'DESCRIPTION:
        '   This routine tests Arbitrary Waveform Generator and returns PASSED or FAILED
        'RETURNS:
        '   PASSED if the Arbitrary Waveform Generator test passes or FAILED if a failure occurs

        'Marker Out, Ref/Smpl In, Start Arm In, Stop Trig/ FSK/GATE In, Output 50/75

        Dim StrMeasurement As String = ""
        Dim SelfTest As Integer
        Dim SelfTest1 As Integer
        Dim dMeasurement As Double
        Dim SMeasurement As String
        Dim ErrorString As String = ""
        Dim AllErrString As String = ""
        Dim QueCount As Integer
        Dim LoopStepNo As Integer
        HelpContextID = 1210

        MsgBox("Remove all cable connections from the SAIF.")
        TestArb = PASSED

        'Arbitrary Waveform Generator Test Title block
        EchoTitle(InstrumentDescription(ARB), True)
        EchoTitle("Arbitrary Waveform Generator Test", False)
        frmSTest.proProgress.Value = 1
        frmSTest.proProgress.Maximum = 100


        ''If InstrumentStatus(ARB) = FAILED And RunningEndToEnd Then
        ''    MsgBox(InstrumentDescription(ARB) & FAILED_PREVIOUSLY, vbInformation + vbOKOnly)
        ''    Echo(InstrumentDescription(ARB) & FAILED_PREVIOUSLY)
        ''    TestArb = FAILED
        ''    GoTo TestComplete
        ''End If

        ''If InstrumentStatus(COUNTER) = FAILED And RunningEndToEnd Then
        ''    Echo(InstrumentDescription(COUNTER) & FAILED_PREVIOUSLY)
        ''    TestArb = COUNTER
        ''    GoTo TestComplete
        ''End If

        ''If InstrumentStatus(OSCOPE) = FAILED And RunningEndToEnd Then
        ''    Echo(InstrumentDescription(OSCOPE) & FAILED_PREVIOUSLY)
        ''    TestArb = OSCOPE
        ''    GoTo TestComplete
        ''End If



        LoopStepNo = 1
        nSystErr = WriteMsg(OSCOPE, "SYST:LANG COMP")
        Delay(1)
        TestArb = PASSED
        'ARB-01-001
Arb1:

        '** Perform Arb Power-up Self Test

        nSystErr = WriteMsg(ARB, "*RST")
        nSystErr = WriteMsg(ARB, "*CLS")
        nSystErr = WriteMsg(ARB, "*OPC?")
        nSystErr = WriteMsg(ARB, "MARK:STAT OFF") 'Disable the AFG to output a marker list    
        nSystErr = WriteMsg(ARB, "*TST?;*OPC?")
        'To make it fail uncomment this for debug only
        '    nSystErr = WriteMsg ARB, "*TST?"
        '    SelfTest& = ReadMsg(ARB, StrMeasurement)

        nSystErr = WaitForResponse(ARB, 0.1)

        Echo("ARB-01-001     ARB Built-In Test. . .")

        SelfTest = ReadMsg(ARB, StrMeasurement)
        If SelfTest <> 0 Or Val(StrMeasurement) <> 0 Then


            QueCount = 1
            Do
                QueCount += 1
                nSystErr = WriteMsg(ARB, "SYST:ERR?")
                SelfTest1 = ReadMsg(ARB, ErrorString)
                If InStr(3, ErrorString, "No error", CompareMethod.Binary) <> 5 Then 'error is next in que
                    AllErrString &= ErrorString & vbCrLf
                Else
                    Exit Do 'no more errors in que
                End If
            Loop While QueCount < 30

            TestArb = FAILED
            Echo(FormatResults(False, "ARB BIT Test", "ARB-01-001"))
            RegisterFailure(ARB, "ARB-01-001", , , , , sGuiLabel(ARB) & " FAILED Built-in Test" & vbCrLf & "Error Details: " & AllErrString)

            Echo("Built-In Test Results:" & vbCrLf & AllErrString, 4)
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
            Echo(FormatResults(True, "Arbitrary Waveform Generator Built-In Test"))
        End If


        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "ARB" And OptionStep = LoopStepNo Then
            GoTo Arb1
        End If
        frmSTest.proProgress.Value = 15
        LoopStepNo += 1

        'ARB-02-001
        'W15-W20:  CABLE ASSY, BNC MALE TO BNC MALE
Step1:
        S = "Use cable W15 to connect" & vbCrLf & vbCrLf
        S &= "   ARB OUTPUT   to   C/T INPUT 1"
        DisplaySetup(S, "TETS AOP_CI1.jpg", 1, True, 1, 2)
        If AbortTest = True Then GoTo testcomplete

Step2:
        S = "Use cable W16 to connect" & vbCrLf & vbCrLf
        S &= "   ARB START ARM IN   to   FG OUTPUT"
        DisplaySetup(S, "TETS ASA_FGO.jpg", 1, True, 2, 2)
        If AbortTest = True Then GoTo testcomplete
        If GoBack = True Then GoTo Step1

Arb2:   '** Generate sign waves
        nSystErr = vxiClear(ARB)
        nSystErr = WriteMsg(ARB, "*RST")
        nSystErr = WriteMsg(ARB, "MARK:STAT OFF") 'Disable the AFG to output a marker list    
        nSystErr = WriteMsg(ARB, "OUTP:LOAD 50")
        nSystErr = WriteMsg(ARB, "FREQ 1E+6")
        nSystErr = WriteMsg(ARB, "FUNC SIN")
        nSystErr = WriteMsg(ARB, "VOLT:UNIT:VOLT VPP")
        nSystErr = WriteMsg(ARB, "VOLT 3")
        nSystErr = WriteMsg(ARB, "OUTP:FILT:FREQ 10E6")
        nSystErr = WriteMsg(ARB, "OUTP:FILT ON")
        nSystErr = WriteMsg(ARB, "INIT:IMM")
        nSystErr = WriteMsg(ARB, "OUTP:STAT ON")

        nSystErr = vxiClear(COUNTER)
        Delay(1)

        nSystErr = WriteMsg(COUNTER, "*RST")
        nSystErr = WriteMsg(COUNTER, "*CLS")
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
            If (OptionFaultMode = SOFmode) And (OptionMode <> LOSmode) Then
               'ARB-02-D01
                If InstrumentStatus(COUNTER) = PASSED And RunningEndToEnd Then
                  Echo(FormatResults(False, "ARB Sine Wave Generation Test", "ARB-02-D01"))
                  RegisterFailure(ARB, "ARB-02-D01", dFH_Meas, "Hz", 900000, 1100000, "ARB Sine Wave Generation Test")
                  TestArb = FAILED
                  If OptionFaultMode = SOFmode Then GoTo TestComplete
                End If
                S = "Move cable W15 connection from " & vbCrLf & vbCrLf
                S &= "   C/T INPUT 1   to   SCOPE INPUT 1"
                DisplaySetup(S, "TETS AOP_SI1.jpg", 1)
                If AbortTest = True Then GoTo testcomplete
                nSystErr = WriteMsg(OSCOPE, "*RST")
                nSystErr = WriteMsg(OSCOPE, "*CLS")
                nSystErr = WriteMsg(OSCOPE, "AUT")              'Set Autoscale mode
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
                    TestArb = COUNTER
                Else
                    'ARB-02-D04
                    Echo(FormatResults(False, "ARB Sine Wave Generation Test Diagnostic", "ARB-02-D04"))
                    RegisterFailure(ARB, "ARB-02-D04", dFH_Meas, "Hz", 900000, 1100000, "Sine Wave Generation Test Diagnostic")
                    TestArb = FAILED
                End If
            End If
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "ARB" And OptionStep = LoopStepNo Then
            GoTo Arb2
        End If
        frmSTest.proProgress.Value = 30
        LoopStepNo += 1

        'ARB-03-001
Arb3:   '** Output Relay Test - Since it is assumed that frequency measurement test is passed, now disable the output relay.
        nSystErr = WriteMsg(ARB, "OUTP OFF")
        nSystErr = WriteMsg(COUNTER, "MEAS1:FREQ?")
        SMeasurement = ""
        nSystErr = ReadMsg(ARB, SMeasurement)
        If (Val(SMeasurement) < 9.0E+37 And SMeasurement <> "") Then
            '  If nSystErr = 0 Or (Val(SMeasurement) < 9E+37 And SMeasurement <> "") Then
            Echo(FormatResults(False, "ARB Output Relay Test", "ARB-03-001"))
            RegisterFailure(ARB, "ARB-03-001", Val(SMeasurement), "sec", , , "Output Relay Test")
            TestArb = FAILED
            IncStepPassed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
            Echo(FormatResults(True, "Output Relay Test", "ARB-03-001"))
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "ARB" And OptionStep = LoopStepNo Then
            GoTo Arb3
        End If
        frmSTest.proProgress.Value = 40
        LoopStepNo += 1

        'ARB-04-001
Arb4:   '**Start Arm In Test
        nSystErr = WriteMsg(FGEN, "*RST") 'Reset the FG to its default condition
        nSystErr = WriteMsg(FGEN, "**CLS")
        nSystErr = WriteMsg(FGEN, "FUNC:SHAP SIN")           'Set a waveform (sine)
        nSystErr = WriteMsg(FGEN, "FREQ 1E+6")               '1MHz Frequency
        nSystErr = WriteMsg(FGEN, "VOLT 2.0")
        nSystErr = WriteMsg(FGEN, "OUTP OFF")                'Turn the main output off

        nSystErr = WriteMsg(ARB, "*RST")
        nSystErr = WriteMsg(ARB, "*CLS")
        nSystErr = WriteMsg(ARB, "MARK:STAT OFF") 'Disable the AFG to output a marker list
        nSystErr = WriteMsg(ARB, "OUTP:LOAD 50")

        nSystErr = WriteMsg(ARB, "ROSC:SOUR INT")
        nSystErr = WriteMsg(ARB, "FREQ 1E6")               'Set freq to 1 MHz
        nSystErr = WriteMsg(ARB, "VOLT 4VPP") 'Set AFG amplitude
        nSystErr = WriteMsg(ARB, "ARM:LAY2:SOUR EXT")      'External Start Arm source
        nSystErr = WriteMsg(ARB, "OUTP:FILT:FREQ 10E6")
        nSystErr = WriteMsg(ARB, "OUTP:FILT ON")
        nSystErr = WriteMsg(ARB, "INIT:IMM")               'Initiate
        nSystErr = WriteMsg(ARB, "OUTP:STAT ON")

        nSystErr = vxiClear(COUNTER)
        nSystErr = WriteMsg(COUNTER, "*RST")
        nSystErr = WriteMsg(COUNTER, "*CLS")
        nSystErr = WriteMsg(COUNTER, "INP1:IMP 50")
        nSystErr = WriteMsg(COUNTER, "INP2:IMP 50")
        nSystErr = WriteMsg(COUNTER, "EVEN:LEV:AUTO ON")        'Place counter in auto-trigger mode
        nSystErr = WriteMsg(COUNTER, "MEAS1:FREQ?")             'Measure frequency input 1

        nSystErr = ReadMsg(ARB, SMeasurement)

        If Val(SMeasurement) < 9.0E+37 And SMeasurement <> "" Then
            TestArb = FAILED
            ' If nSystErr = 0 Or (Val(SMeasurement) < 9E+37 And SMeasurement <> "") Then
            Echo(FormatResults(False, "Start Arm In Timeout Test", "ARB-04-001"))
            RegisterFailure(ARB, "ARB-04-001", Val(SMeasurement), "sec", , , "Start Arm In Timeout Test")
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
            Echo(FormatResults(True, "Start Arm In Timeout Test", "ARB-04-001"))
        End If

        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "ARB" And OptionStep = LoopStepNo Then
            GoTo Arb4
        End If
        frmSTest.proProgress.Value = 50
        LoopStepNo += 1

        'ARB-05-001
Arb5:   nSystErr = WriteMsg(FGEN, "OUTP ON")                 'Turn the main output on
        Delay(1)
        nSystErr = vxiClear(COUNTER)
        nSystErr = WriteMsg(COUNTER, "*RST")
        nSystErr = WriteMsg(COUNTER, "*CLS")
        nSystErr = WriteMsg(COUNTER, "INP1:IMP 50")
        nSystErr = WriteMsg(COUNTER, "INP2:IMP 50")
        nSystErr = WriteMsg(COUNTER, "EVEN:LEV:AUTO ON")        'Place counter in auto-trigger mode
        Delay(2)
        nSystErr = WriteMsg(COUNTER, "MEAS1:FREQ?")             'Measure frequency input 1

        dMeasurement = FetchMeasurement(COUNTER, "Start Arm In Measurement Test", ".95E+6", "1.05E+6", "Hz", ARB, "ARB-05-001")

        'If the test fails, check which device is in error - FG or ARB
        If dMeasurement < 950000 Or dMeasurement > 1050000 Then
            IncStepFailed()
            If (OptionFaultMode = SOFmode) And (OptionMode <> LOSmode) Then
                dFH_Meas = dMeasurement
                S = "Disconnect cable W15 from the SAIF. " & vbCrLf
                S &= "Move cable W16 connection from" & vbCrLf & vbCrLf
                S &= "   ARB START ARM IN   to   C/T INPUT 1."
                DisplaySetup(S, "TETS CI1_FGO.jpg", 1)
                If AbortTest = True Then GoTo testcomplete
                nSystErr = WriteMsg(COUNTER, "*RST")
                nSystErr = WriteMsg(COUNTER, "*CLS")
                nSystErr = WriteMsg(COUNTER, "INP1:IMP 50")
                nSystErr = WriteMsg(COUNTER, "INP2:IMP 50")
                nSystErr = WriteMsg(COUNTER, "EVEN:LEV:AUTO ON")
                Delay(1)
                nSystErr = WriteMsg(COUNTER, "MEAS1:FREQ?")
                'ARB-05-D01
                dMeasurement = FetchMeasurement(COUNTER, "Start Arm In Measurement Test Diagnostic", "95E+4", "105E+4", "Hz", ARB, "ARB-05-D01")

                'ARB-05-D02
                If dMeasurement > 950000 And dMeasurement < 1050000 Then
                    Echo("ARB-05-D02    Start Arm In Measurement Test Diagnostic")
                    Echo(FormatResults(False, , , CStr(950000), CStr(1050000), CStr(dFH_Meas), "Hz"))
                    RegisterFailure(ARB, "ARB-05-D02", dFH_Meas, "Hz", 950000, 1050000, "Start Arm In Measurement Test Diagnostic")
                    TestArb = FAILED
                Else
                    'ARB-05-D03
                    Echo("ARB-05-D03    Start Arm In Measurement Test Diagnostic")
                    Echo(FormatResults(False, , , CStr(950000), CStr(1050000), CStr(dFH_Meas), "Hz"))
                    RegisterFailure(FGEN, "ARB-05-D03", dFH_Meas, "Hz", 950000, 1050000, "Start Arm In Measurement Test Diagnostic")
                    TestArb = FGEN
                End If
            End If
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If

        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "ARB" And OptionStep = LoopStepNo Then
            GoTo Arb5
        End If
        frmSTest.proProgress.Value = 60
        LoopStepNo += 1

        'ARB-06-001
        S = "Move cable W16 connection from  " & vbCrLf & vbCrLf
        S &= "    ARB START ARM IN   to" & vbCrLf
        S &= "    ARB STOP TRIG/FSK/GATE IN."
        DisplaySetup(S, "TETS AST_FGO.jpg", 1)
        If AbortTest = True Then GoTo testcomplete
        'and ARB Output channel to Counter input 1"
Arb6:   '** Gate In Test
        nSystErr = WriteMsg(FGEN, "OUTP OFF")                'Turn FG main output off
        nSystErr = WriteMsg(FGEN, "FUNC:SHAP SQU")
        nSystErr = WriteMsg(FGEN, "VOLT:OFFS 1.25")
        nSystErr = WriteMsg(FGEN, "VOLT 2.5")
        nSystErr = WriteMsg(FGEN, "FREQ 200E3")
        nSystErr = WriteMsg(ARB, "*RST")
        nSystErr = WriteMsg(ARB, "*CLS")
        nSystErr = WriteMsg(ARB, "MARK:STAT OFF") 'Disable the AFG to output a marker list
        nSystErr = WriteMsg(ARB, "OUTP:LOAD 50")

        nSystErr = WriteMsg(ARB, "FREQ 1E+6")              'Set freq to 1 MHz
        nSystErr = WriteMsg(ARB, "VOLT 4VPP")              'Set AFG amplitude
        nSystErr = WriteMsg(ARB, "TRIG:GATE:SOUR EXT")
        nSystErr = WriteMsg(ARB, "TRIG:GATE:STAT ON")
        nSystErr = WriteMsg(ARB, "OUTP:FILT:FREQ 10E6")
        nSystErr = WriteMsg(ARB, "OUTP:FILT ON")
        nSystErr = WriteMsg(ARB, "INIT:IMM")               'Initiate
        nSystErr = WriteMsg(ARB, "OUTP:STAT ON")

        nSystErr = vxiClear(COUNTER)
        nSystErr = WriteMsg(COUNTER, "*RST")
        nSystErr = WriteMsg(COUNTER, "*CLS")
        nSystErr = WriteMsg(COUNTER, "INP1:IMP 50")
        nSystErr = WriteMsg(COUNTER, "INP2:IMP 50")
        nSystErr = WriteMsg(COUNTER, "EVEN:LEV:AUTO ON")        'Place counter in auto-trigger mode
        Delay(1)
        nSystErr = WriteMsg(COUNTER, "MEAS1:FREQ?")             'Measure frequency input 1
        dMeasurement = FetchMeasurement(COUNTER, "Gate In Off Test 1", ".95e6", "1.05e6", "Hz", ARB, "ARB-06-001")


        If dMeasurement > 1050000 Or dMeasurement < 950000 Then
            TestArb = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If 

        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "ARB" And OptionStep = LoopStepNo Then
            GoTo Arb6
        End If
        frmSTest.proProgress.Value = 70
        LoopStepNo += 1

        'ARB-07-001
Arb7:   nSystErr = WriteMsg(FGEN, "OUTP ON")                 'Turn the main output on
        nSystErr = vxiClear(COUNTER)
        nSystErr = WriteMsg(COUNTER, "*RST")
        nSystErr = WriteMsg(COUNTER, "*CLS")
        nSystErr = WriteMsg(COUNTER, "INP1:IMP 50")
        nSystErr = WriteMsg(COUNTER, "INP2:IMP 50")
        nSystErr = WriteMsg(COUNTER, "EVEN:LEV:AUTO ON")        'Place counter in auto-trigger mode
        nSystErr = WriteMsg(COUNTER, "MEAS1:FREQ?")             'Measure frequency input 1

        dMeasurement = FetchMeasurement(COUNTER, "Gate In On Test 2", "450E+3", "550E3", "Hz", ARB, "ARB-07-001")
        If dMeasurement < 450000 Or dMeasurement > 550000 Then
            TestArb = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If 
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "ARB" And OptionStep = LoopStepNo Then
            GoTo Arb7
        End If
        frmSTest.proProgress.Value = 80
        LoopStepNo += 1

        'ARB-08-001
        S = "Move cable W15 connection from  " & vbCrLf & vbCrLf
        S &= "    ARB OUTPUT   to   ARB OUTPUT/7. " & vbCrLf
        DisplaySetup(S, "TETS AO7_CI1.jpg", 1)
        If AbortTest = True Then GoTo testcomplete
Arb8:   nSystErr = WriteMsg(FGEN, "OUTP OFF")
        nSystErr = WriteMsg(ARB, "TRIG:GATE:STAT OFF")

        nSystErr = WriteMsg(ARB, "VOLT 8VPP") 'Set AFG amplitude bb
        Delay(0.5)
        nSystErr = vxiClear(COUNTER)
        nSystErr = WriteMsg(COUNTER, "*RST")
        nSystErr = WriteMsg(COUNTER, "*CLS")
        nSystErr = WriteMsg(COUNTER, "INP1:IMP 50")
        nSystErr = WriteMsg(COUNTER, "INP2:IMP 50")
        nSystErr = WriteMsg(COUNTER, "EVEN:LEV:AUTO ON")        'Place counter in auto-trigger mode
        nSystErr = WriteMsg(COUNTER, "MEAS1:AC?")               'Measure AC RMS input 1

        dMeasurement = FetchMeasurement(COUNTER, "ARB Output/7 Test", ".3", ".6", "V", ARB, "ARB-08-001")
        If dMeasurement > 0.6 Or dMeasurement < 0.3 Then
            TestArb = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "ARB" And OptionStep = LoopStepNo Then
            GoTo Arb8
        End If
        frmSTest.proProgress.Value = 90
        LoopStepNo += 1

        'ARB-09-001
Step1b:
        S = "Move cable W15 connection from  " & vbCrLf & vbCrLf
        S &= "    ARB OUTPUT/7   to   ARB MARKER OUT. " & vbCrLf
        DisplaySetup(S, "TETS AMO_CI1.jpg", 1, True, 1, 2)
        If AbortTest = True Then GoTo testcomplete

Step2b:
        S = "Move cable W16 connection from  " & vbCrLf & vbCrLf
        S &= "    ARB STOP TRIG/FSK/GATE IN   to" & vbCrLf
        S &= "    ARB REF/SMPL IN. " & vbCrLf
        DisplaySetup(S, "TETS ARS_FGO.jpg", 1, True, 2, 2)
        If AbortTest = True Then GoTo TestComplete
        If GoBack = True Then GoTo Step1b



Arb9:   '** Ref In & Marker Out Test
        'Generate 1 MHz squarewave using Function Generator
        nSystErr = WriteMsg(FGEN, "*RST")                    'Reset the FG to its default condition
        nSystErr = WriteMsg(FGEN, "FUNC:SHAP SQU")           'Set the output waveform th square
        nSystErr = WriteMsg(FGEN, "FREQ 1E+6") '1MHz Frequency
        nSystErr = WriteMsg(FGEN, "VOLT 2")                  '2VPK
        nSystErr = WriteMsg(FGEN, "OUTP ON")                 'Turn the main output on

        nSystErr = WriteMsg(ARB, "*RST")
        nSystErr = WriteMsg(ARB, "OUTP:LOAD 50")
        nSystErr = WriteMsg(ARB, "ROSC:SOUR EXT")          'External ref oscillator
        nSystErr = WriteMsg(ARB, "MARK:FEED ""ROSC""")     'Marker source is ROSC
        nSystErr = WriteMsg(ARB, "INIT:IMM")               'Initiate
        nSystErr = WriteMsg(ARB, "OUTP:FILT:FREQ 10E6")
        nSystErr = WriteMsg(ARB, "OUTP:FILT ON")
        nSystErr = vxiClear(COUNTER)
        nSystErr = WriteMsg(COUNTER, "*RST")
        nSystErr = WriteMsg(COUNTER, "*CLS")
        nSystErr = WriteMsg(COUNTER, "INP1:IMP 50")
        nSystErr = WriteMsg(COUNTER, "INP2:IMP 50")
        nSystErr = WriteMsg(COUNTER, "EVEN:LEV:AUTO ON")        'Place counter in auto-trigger mode
        Delay(1)
        nSystErr = WriteMsg(COUNTER, "MEAS1:FREQ?")             'Measure frequency input 1

        dMeasurement = FetchMeasurement(COUNTER, "ARB Ref/Sampl In, Marker Out Test", ".95E+6", "1.05E+6", "Hz", ARB, "ARB-09-001")
        'If the test fails, check which device is in error - FG, ARB, or SCOPE
        If dMeasurement > 1050000 Or dMeasurement < 950000 Then
            TestArb = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If '4
        If OptionMode = LOSmode And OptionTestName = "ARB" And OptionStep = LoopStepNo Then
            GoTo Arb9
        End If

TestComplete:
        frmSTest.proProgress.Value = 100
        Application.DoEvents()
        frmSTest.cmdAbort.Text = "Abort Test"
        frmSTest.cmdPause.Text = "Pause Test"
        nSystErr = WriteMsg(SWITCH1, "RESET") ' open all switches
        nSystErr = WriteMsg(ARB, "*RST")
        nSystErr = WriteMsg(ARB, "*CLS")
        WriteMsg(ARB, "MARK:STAT OFF") 'Disable the AFG to output a marker list
        nSystErr = WriteMsg(COUNTER, "*RST")
        nSystErr = WriteMsg(COUNTER, "*CLS")
        nSystErr = WriteMsg(FGEN, "*RST")
        nSystErr = WriteMsg(OSCOPE, "*RST")
        nSystErr = WriteMsg(OSCOPE, "*CLS")
        If AbortTest = True Then
            If TestArb = FAILED Then
                ReportFailure(ARB)
            Else
                ReportUnknown(ARB)
                TestArb = UNTESTED
            End If
            sMsg = vbCrLf
            sMsg &= "      **********************************************" & vbCrLf
            sMsg &= "      *               ARB tests aborted!           *" & vbCrLf
            sMsg &= "      **********************************************" & vbCrLf
            Echo(sMsg)
        ElseIf TestArb = PASSED Then
            ReportPass(ARB)
        ElseIf TestArb = FAILED Then
            ReportFailure(ARB)
        Else
            ReportUnknown(ARB)
        End If

    End Function

End Module