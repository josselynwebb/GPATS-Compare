'Option Strict Off
Option Explicit On

Imports System.Windows.Forms

Public Module modFGenViperTSaif


    '=========================================================
    '**************************************************************
    '* Nomenclature   : TETS SYSTEM SELF TEST                     *
    '*                  Function Generator Test                   *
    '* Version        : 2.0                                       *
    '* Last Update    : Apr 1, 2017                               *
    '* Purpose        : This module contains code for the         *
    '*                  Function Generator Self Test              *
    '**************************************************************

    Public Function TestFunctionGeneratorVT() As Integer
        'DESCRIPTION:
        ' Racal Instruments 3152 Function Gen (SC Slot 3)
        '   This routine tests Function Generator and returns PASSED or FAILED
        'RETURNS:
        '   PASSED if the Function Generator test passes or FAILED if a failure occurs

        Dim x As DialogResult
        Dim S As String
        Dim StrMeasurement As String = ""
        Dim SelfTest As Integer
        Dim LoopStepNo As Integer
        HelpContextID = 1220

        TestFunctionGeneratorVT = UNTESTED
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
                S += " 2. P4  to SAIF FG OUT." & vbCrLf
                S += " 3. P5  to SAIF FG TRIG/PLL IN." & vbCrLf
                S += " 4. P6  to SAIF FG SYNC OUT." & vbCrLf
                S += " 5. P7  to SAIF FG CLOCK IN." & vbCrLf
                S += " 6. P8  to SAIF FG PM IN." & vbCrLf
                S += " 7. P10 to SAIF ARB OUTPUT." & vbCrLf
                S += " 8. P12 to SAIF ARB MARKER OUT." & vbCrLf
                S += " 9. P15 to SAIF SCOPE INPUT 1." & vbCrLf
                S += " 10. P19 to SAIF C/T INPUT 1."
                DisplaySetup(S, "ST-W201-1.jpg", 9)
                If AbortTest = True Then GoTo TestComplete
            End If
        End If
        TestFunctionGeneratorVT = PASSED
        LoopStepNo = 1

        'Function Generator Test Title block
        EchoTitle(InstrumentDescription(FGEN) & " ", True)
        EchoTitle("Function Generator Test", False)
        frmSTest.proProgress.Maximum = 100
        frmSTest.proProgress.Value = 1

FGN1:   'FGN-01-001
        '** Perform Arb Power-up Self Test

        Echo("FGN-01-001 Function Generator Built-In Test. . .")
        nSystErr = vxiClear(FGEN)
        nSystErr = WriteMsg(FGEN, "*RST;*CLS")
        Delay(0.5)
        nSystErr = WriteMsg(FGEN, "*TST?")
        Delay(0.5)
        WaitForResponse(FGEN, 0.1)
        Delay(0.5)
        SelfTest = ReadMsg(FGEN, StrMeasurement)

        If SelfTest <> 0 Or Val(StrMeasurement) <> 0 Then
            TestFunctionGeneratorVT = FAILED
            IncStepFailed()
            Echo(FormatResults(False, "Built-In Test Result = " & StripNullCharacters(StrMeasurement)))
            RegisterFailure(FGEN, "FGN-01-001", , , , , "Function Generator Built-In Test")
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        Application.Doevents()
        Echo(FormatResults(True, "Function Generator Built-In Test"))
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "FG" And OptionStep = Loopstepno Then
            GoTo FGN1
        End If
        frmSTest.proProgress.Value = 20
        LoopStepNo += 1

FGN2:   'FGN-02-001
        '** Freqency Accuracy Test
        nSystErr = WriteMsg(OSCOPE, "SYST:LANG COMP")
        Delay(1)
        nSystErr = vxiClear(FGEN)
        nSystErr = WriteMsg(FGEN, "*RST;*CLS")
        Delay(0.5)
        nSystErr = WriteMsg(FGEN, "OUTP ON")
        Delay(2)
        nSystErr = WriteMsg(FGEN, "FUNC:SHAP SQU")
        nSystErr = WriteMsg(FGEN, "FREQ 10")
        nSystErr = WriteMsg(FGEN, "VOLT 1")

        'Connect P4 FG OUTPUT to P19 C/T INPUT 1
        nSystErr = WriteMsg(SWITCH1, "CLOSE 1.3102") 'close S402-1,4
        'Connect P12 ARB MARKER OUT to P5 FG TRIG/PLL IN
        nSystErr = WriteMsg(SWITCH1, "CLOSE 1.3003") 'close S401-1,5
        'Connect P10 ARB OUTPUT to P7 FG CLOCK IN.
        nSystErr = WriteMsg(SWITCH1, "CLOSE 2.1004") 'close S301-105,106
        Delay(1)

        nSystErr = vxiClear(COUNTER)
        nSystErr = WriteMsg(COUNTER, "*RST; *CLS")
        nSystErr = WriteMsg(COUNTER, "INP1:IMP 50")
        nSystErr = WriteMsg(COUNTER, "INP2:IMP 50")
        nSystErr = WriteMsg(COUNTER, "ATIM ON")
        nSystErr = WriteMsg(COUNTER, "ATIM:TIME 60")
        nSystErr = WriteMsg(COUNTER, "EVEN:LEV:AUTO ON") 'Place counter in auto-trigger mode
        Delay(2)
        nSystErr = WriteMsg(FGEN, "OUTP ON")
        nSystErr = WriteMsg(FGEN, "FREQ 10000")
        Delay(1)
        nSystErr = WriteMsg(COUNTER, "MEAS1:FREQ?") 'Measure frequency input 1
        Delay(2)
        dMeasurement = FetchMeasurement(COUNTER, "FG Frequency Accuracy Test 10 KHz", 9500, 10500, "Hz", COUNTER, "FGN-02-001")
        If dMeasurement > 10500 Or dMeasurement < 9500 Then
            IncStepFailed()
            dFH_Meas = dMeasurement ' save this measurement
            If InstrumentStatus(COUNTER) = PASSED And RunningEndToEnd Then
                TestFunctionGeneratorVT = FAILED
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            End If
            'Disconnect P4 FG Out from P19 C/T Input 1
            nSystErr = WriteMsg(SWITCH1, "OPEN 1.3102") 'open  S402-1,4   bb
            'Connect P4 FG Out to P15 Scope Input 1
            nSystErr = WriteMsg(SWITCH1, "CLOSE 2.3102") 'close S405-1,4   bb
            nSystErr = vxiClear(OSCOPE)
            nSystErr = WriteMsg(OSCOPE, "*RST; *CLS")
            nSystErr = WriteMsg(OSCOPE, "AUT") 'Set Autoscale mode
            Delay(2)
            nSystErr = WriteMsg(OSCOPE, "DIG CHAN1")
            nSystErr = WriteMsg(OSCOPE, "MEAS:SOUR CHAN1")
            nSystErr = WriteMsg(OSCOPE, "MEAS:FREQ?")
            WaitForResponse(OSCOPE, 0.1)
            'FGN-02-D01
            dMeasurement = FetchMeasurement(OSCOPE, "Function Generator Frequency Accuracy Test Diagnostic", 9500, 10500, "Hz", OSCOPE, "FGN-02-D01")
            If dMeasurement < 9500 Or dMeasurement > 10500 Then
                Echo(FormatResults(False, "Function Generator Frequency Accuracy Test Diagnostic", "FGN-02-D01"))
                RegisterFailure(FGEN, "FGN-02-D01", dFH_Meas, "Hz", 9500, 10500, "Function Generator Frequency Accuracy Test Diagnostic.")
                TestFunctionGeneratorVT = FAILED
            Else
                Echo(FormatResults(False, "Function Generator Frequency Accuracy Test Diagnostic", "FGN-02-D01"))
                TestFunctionGeneratorVT = COUNTER
                RegisterFailure(COUNTER, "FGN-02-D01", dFH_Meas, "Hz", 9500, 10500, "Function Generator Frequency Accuracy Test Diagnostic.")
            End If
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        Application.Doevents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "FG" And OptionStep = LoopStepNo Then
            GoTo FGN2
        End If
        frmSTest.proProgress.Value = 40
        LoopStepNo += 1

FGN3:   'FGN-03-001
        '** Trig Mode Test
        nSystErr = vxiClear(FGEN)
        nSystErr = WriteMsg(FGEN, "*RST;*CLS")
        Delay(0.5)
        nSystErr = WriteMsg(FGEN, "OUTP ON")
        Delay(2)
        nSystErr = WriteMsg(FGEN, "FUNC:SHAP SIN")
        nSystErr = WriteMsg(FGEN, "FREQ 1E+6") '1 MHz Freqency
        nSystErr = WriteMsg(FGEN, "INIT:CONT OFF") ' set fgen in triggered mode, one output waveform per trigger
        nSystErr = WriteMsg(FGEN, "OUTP ON")
        Delay(2)
        nSystErr = vxiClear(ARB)
        nSystErr = WriteMsg(ARB, "*RST;*CLS")
        nSystErr = WriteMsg(ARB, "FREQ 1E+4") '10 KHz Freqency
        nSystErr = WriteMsg(ARB, "FUNC SIN")
        nSystErr = WriteMsg(ARB, "VOLT:UNIT:VOLT VPP")
        nSystErr = WriteMsg(ARB, "VOLT 3")
        nSystErr = WriteMsg(ARB, "SOUR:MARK:FEED LIST1") 'Select the source, marker out
        nSystErr = WriteMsg(ARB, "MARKER:POL NORM") 'Select marker polarity
        nSystErr = WriteMsg(ARB, "MARK:STAT ON") 'Enable the AFG to output a marker list
        nSystErr = WriteMsg(ARB, "INIT") 'Initiate
        nSystErr = WriteMsg(FGEN, "OUTP ON") 'Output
        Delay(2)
        nSystErr = vxiClear(COUNTER)
        nSystErr = WriteMsg(COUNTER, "*CLS")
        nSystErr = WriteMsg(COUNTER, "*RST")
        nSystErr = WriteMsg(COUNTER, "INP1:IMP 50")
        nSystErr = WriteMsg(COUNTER, "INP2:IMP 50")
        nSystErr = WriteMsg(COUNTER, "SENS1:EVEN:LEV 0.3V")
        nSystErr = WriteMsg(COUNTER, "MEAS1:FREQ?")
        Delay(1)

        dMeasurement = FetchMeasurement(COUNTER, "Function Generator Trigger Mode Test", "9000", "11000", "Hz", COUNTER, "FGN-03-001")
        If dMeasurement > 11000 Or dMeasurement < 9000 Then
            IncStepFailed()
            If InstrumentStatus(ARB) = PASSED And RunningEndToEnd Then
                TestFunctionGeneratorVT = FAILED
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            End If

            'FGN-03-D01 'Diagnostic Test ARB Marker OUT
            dFH_Meas = dMeasurement
            'Disconnect P12 Arb marker Out from P5 FG Trig/PLL IN
            nSystErr = WriteMsg(SWITCH1, "OPEN 1.3003") 'open  S401-1,5   bb
            'Connect P12 Arb marker Out to P15 Scope Input 1
            nSystErr = WriteMsg(SWITCH1, "CLOSE 2.3103") 'close S405-1,5   bb
            nSystErr = vxiClear(OSCOPE)
            nSystErr = WriteMsg(OSCOPE, "*RST; *CLS")
            nSystErr = WriteMsg(OSCOPE, "AUT") 'Set Autoscale mode
            Delay(2)

            nSystErr = WriteMsg(OSCOPE, "DIG CHAN1")
            nSystErr = WriteMsg(OSCOPE, "MEAS:SOUR CHAN1")
            nSystErr = WriteMsg(OSCOPE, "MEAS:FREQ?")
            WaitForResponse(OSCOPE, 0.1)
            dMeasurement = FetchMeasurement(OSCOPE, "FG Trigger Mode TEST Diagnostic", "9000", "11000", "Hz", ARB, "FGN-03-D01")
            If dMeasurement > 11000 Or dMeasurement < 9000 Then
                Echo(FormatResults(False, "FG Trigger Mode TEST Diagnostic", "FGN-03-D01"))
                RegisterFailure(ARB, "FGN-03-D01", dFH_Meas, "Hz", 9000, 11000, "FG Trigger Mode TEST Diagnostic")
                TestFunctionGeneratorVT = ARB
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            End If

            '        Echo "FGN-03-D01 FG Trigger Mode TEST Diagnostic"   ' why? already logged by fetchmeasurement
            '        RegisterFailure FGEN, "FGN-03-D01", dFH_Meas, "Hz",
            ''           9000, 11000, "FG Trigger Mode TEST Diagnostic"
            TestFunctionGeneratorVT = FAILED
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        Application.Doevents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "FG" And OptionStep = LoopStepNo Then
            GoTo FGN3
        End If
        frmSTest.proProgress.Value = 60
        LoopStepNo += 1

FGN4:   'FGN-04-001
        'Disconnect P4 FG Out from P19 C/T Input 1
        nSystErr = WriteMsg(SWITCH1, "OPEN 1.3102") 'open S402-1,4   bb
        'Connect P4 FG Out to P15 Scope Input 1
        nSystErr = WriteMsg(SWITCH1, "CLOSE 2.3102") 'close S405-1,4   bb
        Delay(1)

        '** External Clock Input Test
        'Generate sign waves
        nSystErr = vxiClear(ARB)
        nSystErr = WriteMsg(ARB, "*RST;*CLS")
        nSystErr = WriteMsg(ARB, "FREQ 1E+6") 'The number of points used to define this wave form is 100.
        nSystErr = WriteMsg(ARB, "FUNC SQU")
        nSystErr = WriteMsg(ARB, "VOLT:UNIT:VOLT VPP")
        'The following levels create valid ECL (-0.8 to -1.6) when driving the Clock In input:
        nSystErr = WriteMsg(ARB, "VOLT 0.8")
        nSystErr = WriteMsg(ARB, "VOLT:OFFS -0.3")
        nSystErr = WriteMsg(ARB, "OUTP:FILT:FREQ 10E6")
        nSystErr = WriteMsg(ARB, "OUTP:FILT ON")
        nSystErr = WriteMsg(ARB, "INIT")

        nSystErr = vxiClear(FGEN)
        nSystErr = WriteMsg(FGEN, "*RST;*CLS")

        nSystErr = WriteMsg(FGEN, "OUTP ON")
        nSystErr = WriteMsg(FGEN, "VOLT 2")
        nSystErr = WriteMsg(FGEN, "FREQ:RAST:SOUR EXT") 'Enable the external clock input

        nSystErr = WriteMsg(FGEN, "OUTP ON")
        nSystErr = WriteMsg(FGEN, "VOLT 2")
        Delay(0.5)

        nSystErr = WriteMsg(OSCOPE, "VIEW CHAN1") 'TURN ON CHAN1
        nSystErr = WriteMsg(OSCOPE, "BLAN CHAN2") 'TURN OFF CHAN2
        nSystErr = WriteMsg(OSCOPE, "CHAN1:COUP DCF")
        nSystErr = WriteMsg(OSCOPE, "CHAN2:COUP DCF")
        nSystErr = WriteMsg(OSCOPE, "CHAN1:RANG 2V")
        nSystErr = WriteMsg(OSCOPE, "TRIG:LEV 0.1V")
        nSystErr = WriteMsg(OSCOPE, "TIM:RANG 1E-3")

        nSystErr = WriteMsg(OSCOPE, "ACQ:POIN 8000")
        nSystErr = WriteMsg(OSCOPE, "ACQ:TYPE AVER")
        nSystErr = WriteMsg(OSCOPE, "ACQ:COUN 32")
        nSystErr = WriteMsg(OSCOPE, "MEAS:SOUR CHAN1")
        nSystErr = WriteMsg(OSCOPE, "DIG CHAN1")
        Delay(1)
        nSystErr = WriteMsg(OSCOPE, "MEAS:FREQ?")
        Delay(1)

        dMeasurement = FetchMeasurement(OSCOPE, "Function Generator External Clock Input Test", "8500", "11500", "Hz", OSCOPE, "FGN-04-001")
        If dMeasurement < 8500 Or dMeasurement > 11500 Then
            IncStepFailed()
            dFH_Meas = dMeasurement
            If InstrumentStatus(ARB) = PASSED And RunningEndToEnd Then
                Echo(FormatResults(False, "FG External Clock Input Test", "RFM-04-D01"))
                RegisterFailure(FGEN, "FGN-04-D01", dFH_Meas, "Hz", 9000, 11000, "Function Generator External Clock Input Test")
                TestFunctionGeneratorVT = FAILED
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            End If
            'FGN-04-D01
            ' Disconnect P10 Arb Output from P7 FG Clkin
            nSystErr = WriteMsg(SWITCH1, "OPEN 2.1004") 'open  S301-105,106 bb
            ' Connect P10 Arb Output to P15 Scope Input 1
            nSystErr = WriteMsg(SWITCH1, "CLOSE 2.3003") 'close S404-1,5     bb

            nSystErr = WriteMsg(OSCOPE, "*RST; *CLS")
            nSystErr = WriteMsg(OSCOPE, "AUT") 'Set Autoscale mode
            Delay(2)

            nSystErr = WriteMsg(OSCOPE, "DIG CHAN1")
            nSystErr = WriteMsg(OSCOPE, "MEAS:SOUR CHAN1")

            nSystErr = WriteMsg(OSCOPE, "MEAS:VAMP?")
            WaitForResponse(OSCOPE, 0.1)

            dMeasurement = FetchMeasurement(OSCOPE, "Function Generator External Clock Input Test Diagnostic", "1", "", "V", ARB, "FGN-04-D01")
            If dMeasurement < 1 Then
                TestFunctionGeneratorVT = ARB
                Echo(FormatResults(False, "FG External Clock Input Test Diagnostic", "RFM-04-D01"))
                RegisterFailure(ARB, "FGN-04-D01", dFH_Meas, "Hz", 9000, 11000, "Function Generator External Clock Input Test Diagnostic")
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            End If
            '        Echo "FGN-04-D01 Function Generator External Clock Input Test Diagnostic"' why??
            '        RegisterFailure FGEN, "FGN-04-D01", dFH_Meas, "Hz",
            ''           9000, 11000, "Function Generator External Clock Input Test Diagnostic"
            TestFunctionGeneratorVT = FAILED
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        Application.Doevents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "FG" And OptionStep = LoopStepNo Then
            GoTo FGN4
        End If
        frmSTest.proProgress.Value = 80
        LoopStepNo += 1

FGN5:   'FGN-05-001
        '**SYNC Output Test
        'Disconnect all signals?????
        'Disconnect P12 ARB MARKER OUT to P5 FG TRIG/PLL IN
        nSystErr = WriteMsg(SWITCH1, "OPEN 1.3003") 'open S401-1,5
        'Disconnect P10 ARB OUTPUT to P7 FG CLOCK IN.
        nSystErr = WriteMsg(SWITCH1, "OPEN 2.1004") 'open S301-105,106
        'Disconnect P4 FG Out from P15 Scope Input 1
        nSystErr = WriteMsg(SWITCH1, "OPEN 2.3102") 'open S405-1,4

        'Connect P6 FG Sync Out to P19 C/T Input 1
        nSystErr = WriteMsg(SWITCH1, "CLOSE 1.1001") 'close S301-3,4
        Delay(1)

        'The same sine waveform is applied to FG main output for this test
        nSystErr = vxiClear(ARB)
        nSystErr = WriteMsg(ARB, "*RST;*CLS")
        nSystErr = WriteMsg(FGEN, "*RST; *CLS")
        nSystErr = WriteMsg(FGEN, "OUTP ON")
        Delay(2)
        nSystErr = WriteMsg(FGEN, "FUNC:SHAP SIN")
        nSystErr = WriteMsg(FGEN, "FREQ 1E+6")
        nSystErr = WriteMsg(FGEN, "VOLT 3")

        nSystErr = WriteMsg(FGEN, "OUTP:SYNC ON") 'Enable the SYNC output (Default: OFF)
        nSystErr = WriteMsg(FGEN, "OUTP:SYNC:SOUR BIT") 'Generates a sync signal every time the segment is output.
        nSystErr = WriteMsg(FGEN, "OUTP:SYNC:POS:POINT 2")
        nSystErr = WriteMsg(FGEN, "OUTP ON")
        Delay(0.5)

        nSystErr = vxiClear(COUNTER)
        nSystErr = WriteMsg(COUNTER, "*RST")
        nSystErr = WriteMsg(COUNTER, "INP1:IMP 1E6")
        nSystErr = WriteMsg(COUNTER, "INP2:IMP 1E6")
        nSystErr = WriteMsg(COUNTER, "EVEN:LEV:AUTO OFF")
        nSystErr = WriteMsg(COUNTER, "SENS1:EVEN:HYST MAX")
        nSystErr = WriteMsg(COUNTER, "SENS1:EVEN:LEV 0.5")
        Delay(1)
        nSystErr = WriteMsg(COUNTER, "MEAS1:FREQ?")
        Delay(1)

        dMeasurement = FetchMeasurement(COUNTER, "SYNC Output Test", "950E3", "1050E3", "Hz", FGEN, "FGN-05-001")
        If (dMeasurement < 950000) Or (dMeasurement > 1050000) Then
            TestFunctionGeneratorVT = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        Application.Doevents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "FG" And OptionStep = LoopStepNo Then
            GoTo FGN5
        End If
        frmSTest.proProgress.Value = 100

TestComplete:
        frmSTest.cmdAbort.Text = "Abort Test"
        frmSTest.cmdPause.Text = "Pause Test"
        nSystErr = WriteMsg(SWITCH1, "RESET") ' open all switches
        nSystErr = vxiClear(COUNTER)
        nSystErr = WriteMsg(COUNTER, "*RST")
        nSystErr = vxiClear(ARB)
        nSystErr = WriteMsg(ARB, "*RST;*CLS")
        nSystErr = vxiClear(FGEN)
        nSystErr = WriteMsg(FGEN, "*RST;*CLS")
        nSystErr = vxiClear(OSCOPE)
        nSystErr = WriteMsg(OSCOPE, "*RST;*CLS")

        Application.Doevents()
        If AbortTest = True Then
            If TestFunctionGenerator = FAILED Then
                ReportFailure(FGEN)
            Else
                ReportUnknown(FGEN)
            End If
        ElseIf TestFunctionGenerator = PASSED Then
            ReportPass(FGEN)
        ElseIf TestFunctionGenerator = FAILED Then
            ReportFailure(FGEN)
        Else
            ReportUnknown(FGEN)
        End If


    End Function


End Module