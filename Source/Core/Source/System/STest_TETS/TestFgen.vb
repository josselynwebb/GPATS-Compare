'Option Strict Off
Option Explicit On


Public Module modFgen

        '**************************************************************
        '* Nomenclature   : ATS-TETS SYSTEM SELF TEST                 *
        '*                  Function Generator Test                   *
        '* Version        : 2.0                                       *
        '* Last Update    : Apr 1, 2017                               *
        '* Written By     : Michael McCabe                            *
        '* Purpose        : This module contains code for the         *
        '*                  Function Generator Self Test              *
        '**************************************************************

    Dim S As String = ""

    Public Function TestFGen() As Integer

        'DESCRIPTION:
        ' Racal Instruments 3152 Function Gen (SC Slot 3)
        '   This routine tests Function Generator and returns PASSED or FAILED
        'RETURNS:
        '   PASSED if the Function Generator test passes or FAILED if a failure occurs

        Dim StrMeasurement As String = ""
        Dim SelfTest As Integer
        Dim i As Integer
        Dim dMeasurement As Double
        Dim LoopStepNo As Integer

        MsgBox("Remove all cable connections from the SAIF.")
        HelpContextID = 1220
        TestFGen = PASSED

        'Function Generator Test Title block
        EchoTitle(InstrumentDescription(FGEN), True)
        EchoTitle("Function Generator Test", False)
        frmSTest.proProgress.Maximum = 100
        frmSTest.proProgress.Value = 1



        LoopStepNo = 1

        '** Perform FG Power-up BIT Test
FGN1:   'FGN-01-001
        Echo("FGN-01-001     Function Generator Built-In Test. . .")
        nSystErr = vxiClear(FGEN)
        nSystErr = WriteMsg(FGEN, "*RST")
        nSystErr = WriteMsg(FGEN, "*CLS")
        Delay(0.5)
        nSystErr = WriteMsg(FGEN, "*TST?")
        Delay(0.5)
        nSystErr = WaitForResponse(FGEN, 0.8)
        Delay(0.5)
        SelfTest = ReadMsg(FGEN, StrMeasurement)
        If SelfTest <> 0 Or Val(StrMeasurement) <> 0 Then
            TestFGen = FAILED
            IncStepFailed()
            Echo(FormatResults(False, "Built-In Test Result = " & StripNullCharacters(StrMeasurement)))
            RegisterFailure(FGEN, "FGN-01-001", , , , , "Function Generator Built-In Test")
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        Echo(FormatResults(True, "Function Generator Built-In Test"))
        frmSTest.proProgress.Value = 20
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "FGEN" And OptionStep = LoopStepNo Then
            GoTo FGN1
        End If
        LoopStepNo += 1

        'FGN-02-
Step1:
        S = "Use cable W15 to connect" & vbCrLf & vbCrLf
        S &= "   FG OUTPUT   to   C/T INPUT 1."
        DisplaySetup(S, "TETS CI1_FGO.jpg", 1, True, 1, 3)
        If AbortTest = True Then GoTo testcomplete
Step2:
        S = "Use cable W16 to connect" & vbCrLf & vbCrLf
        S &= "   FG TRIG/PLL IN   to   ARB MARKER OUT."
        DisplaySetup(S, "TETS AMO_FTP.jpg", 1, True, 2, 3)
        If AbortTest = True Then GoTo testcomplete
        If GoBack = True Then GoTo Step1
Step3:
        S = "Use cable W17 to connect" & vbCrLf & vbCrLf
        S &= "   FG CLOCK IN   to   ARB OUTPUT."
        DisplaySetup(S, "TETS AOP_FCI.jpg", 1, True, 3, 3)
        If AbortTest = True Then GoTo testcomplete
        If GoBack = True Then GoTo Step2

FGN2:
        '** Freqency Accuracy Test
        nSystErr = WriteMsg(OSCOPE, "SYST:LANG COMP")
        Delay(1)
        nSystErr = vxiClear(FGEN)
        nSystErr = WriteMsg(FGEN, "*RST")
        nSystErr = WriteMsg(FGEN, "*CLS")
        Delay(0.5)
        nSystErr = WriteMsg(FGEN, "OUTP ON")
        Delay(2)
        nSystErr = WriteMsg(FGEN, "FUNC:SHAP SQU")
        nSystErr = WriteMsg(FGEN, "FREQ 10")                 'Frequency is set in the loop.
        nSystErr = WriteMsg(FGEN, "VOLT 1")

        nSystErr = vxiClear(COUNTER)
        nSystErr = WriteMsg(COUNTER, "*RST")
        nSystErr = WriteMsg(COUNTER, "*CLS")
        nSystErr = WriteMsg(COUNTER, "INP1:IMP 50")
        nSystErr = WriteMsg(COUNTER, "INP2:IMP 50")
        nSystErr = WriteMsg(COUNTER, "ATIM ON")
        nSystErr = WriteMsg(COUNTER, "ATIM:TIME 60")
        nSystErr = WriteMsg(COUNTER, "EVEN:LEV:AUTO ON")        'Place counter in auto-trigger mode
        Delay(2)
        nSystErr = WriteMsg(FGEN, "OUTP ON")
        nSystErr = WriteMsg(FGEN, "FREQ 10000")
        Delay(1)
        nSystErr = WriteMsg(COUNTER, "MEAS1:FREQ?")         'Measure frequency input 1
        Delay(2)
        dMeasurement = FetchMeasurement(COUNTER, "FG Frequency Accuracy Test 10 KHz", 9500, 10500, "Hz", COUNTER, "FGN-02-001")
        If dMeasurement > 10500 Or dMeasurement < 9500 Then
            TestFGen = FAILED
            IncStepFailed()
            If (OptionFaultMode = SOF_BUTTON) And (OptionMode <> LOSmode) Then
                dFH_Meas = dMeasurement ' save this measurement

                If InstrumentStatus(COUNTER) = PASSED And RunningEndToEnd Then
                    If OptionFaultMode = SOFmode Then GoTo TestComplete
                End If
                S = "Move cable W15 connection from" & vbCrLf & vbCrLf
                S &= "    C/T INPUT 1   to   SCOPE INPUT 1."
                DisplaySetup(S, "TETS FGO_SI1.jpg", 1)
                If AbortTest = True Then GoTo testcomplete
                nSystErr = vxiClear(OSCOPE)
                nSystErr = WriteMsg(OSCOPE, "*RST")
                nSystErr = WriteMsg(OSCOPE, "*CLS")
                nSystErr = WriteMsg(OSCOPE, "AUT")          'Set Autoscale mode
                Delay(2)

                nSystErr = WriteMsg(OSCOPE, "DIG CHAN1")
                nSystErr = WriteMsg(OSCOPE, "MEAS:SOUR CHAN1")
                nSystErr = WriteMsg(OSCOPE, "MEAS:FREQ?")
                nSystErr = WaitForResponse(OSCOPE, 0.1)
                'FGN-02-D01
                dMeasurement = FetchMeasurement(OSCOPE, "Function Generator Frequency Accuracy Test Diagnostic", "9500", "10500", "Hz", OSCOPE, "FGN-02-D01")
                'FGN-02-D02
                If dMeasurement > 9500 Or dMeasurement < 10500 Then
                    Echo("FGN-02-D02     Function Generator Frequency Accuracy Test Diagnostic.")
                    RegisterFailure(FGEN, "FGN-02-D01", dFH_Meas, "Hz", 9500, 10500, "Function Generator Frequency Accuracy Test Diagnostic.")
                Else
                    'FGN-02-D03
                    Echo("FGN-02-D03     Function Generator Frequency Accuracy Test Diagnostic.")
                    TestFGen = COUNTER
                    RegisterFailure(COUNTER, "FGN-02-D01", dFH_Meas, "Hz", 9500, 10500, "Function Generator Frequency Accuracy Test Diagnostic.")
                End If
            End If
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "FGEN" And OptionStep = LoopStepNo Then
            GoTo FGN2
        End If
        frmSTest.proProgress.Value = 40
        LoopStepNo += 1

FGN3:   'FGN-03-001
        '** Trig Mode Test
        nSystErr = vxiClear(FGEN)
        nSystErr = WriteMsg(FGEN, "*RST")
        nSystErr = WriteMsg(FGEN, "*CLS")
        nSystErr = WriteMsg(FGEN, "FUNC:SHAP SIN")
        nSystErr = WriteMsg(FGEN, "VOLT 1")
        nSystErr = WriteMsg(FGEN, "FREQ 1E+6")                   '1 MHz Freqency
        nSystErr = WriteMsg(FGEN, "INIT:CONT OFF") ' set fgen in triggered mode, one output waveform per trigger
        nSystErr = WriteMsg(FGEN, "OUTP ON")
        Delay(2)
        nSystErr = vxiClear(ARB)
        nSystErr = WriteMsg(ARB, "*RST")
        nSystErr = WriteMsg(ARB, "*CLS")
        '  nSystErr = WriteMsg(ARB, "OUTP:LOAD 50")
        nSystErr = WriteMsg(ARB, "FREQ 1E+4")                  '10 KHz Freqency
        nSystErr = WriteMsg(ARB, "FUNC SQU")
        nSystErr = WriteMsg(ARB, "VOLT:UNIT:VOLT VPP")
        nSystErr = WriteMsg(ARB, "VOLT 3")
        nSystErr = WriteMsg(ARB, "SOUR:MARK:FEED LIST1") 'Select the source, marker out
        nSystErr = WriteMsg(ARB, "MARKER:POL NORM") 'Select marker polarity
        nSystErr = WriteMsg(ARB, "MARK:STAT ON")               'Enable the AFG to output a marker list
        nSystErr = WriteMsg(ARB, "INIT:IMM") 'Initiate
        nSystErr = WriteMsg(ARB, "OUTP:STAT ON")
        Delay(2)
        nSystErr = vxiClear(COUNTER)
        nSystErr = WriteMsg(COUNTER, "*RST")
        nSystErr = WriteMsg(COUNTER, "*CLS")
        nSystErr = WriteMsg(COUNTER, "INP1:IMP 1E6")
        nSystErr = WriteMsg(COUNTER, "INP2:IMP 1E6")
        nSystErr = WriteMsg(COUNTER, "SENS1:EVEN:LEV 0.3V")
        nSystErr = WriteMsg(COUNTER, "MEAS1:FREQ?")
        Delay(2)
        'FGN-03-001
        dMeasurement = FetchMeasurement(COUNTER, "Function Generator Trigger Mode Test", "9000", "11000", "Hz", COUNTER, "FGN-03-001")
        frmSTest.proProgress.Value = 60
        If dMeasurement > 11000 Or dMeasurement < 9000 Then
            IncStepFailed()
            TestFGen = FAILED
            If (OptionFaultMode = SOF_BUTTON) And (OptionMode <> LOSmode) Then
                dFH_Meas = dMeasurement
                If InstrumentStatus(ARB) = PASSED And RunningEndToEnd Then
                    If OptionFaultMode = SOFmode Then GoTo TestComplete
                End If
                S = "Move cable W16 connection from" & vbCrLf & vbCrLf
                S &= "    FG TRIG/PLL IN   to   SCOPE INPUT 1."
                DisplaySetup(S, "TETS AMO_SI1.jpg", 1)
                If AbortTest = True Then GoTo testcomplete
                nSystErr = vxiClear(OSCOPE)
                nSystErr = WriteMsg(OSCOPE, "*RST")
                nSystErr = WriteMsg(OSCOPE, "*CLS")
                nSystErr = WriteMsg(OSCOPE, "AUT")                  'Set Autoscale mode
                Delay(2)
                If i = 1 Then
                    nSystErr = WriteMsg(OSCOPE, "TIM:RANG 500e-3")
                End If

                nSystErr = WriteMsg(OSCOPE, "DIG CHAN1")
                nSystErr = WriteMsg(OSCOPE, "MEAS:SOUR CHAN1")
                nSystErr = WriteMsg(OSCOPE, "MEAS:FREQ?")
                nSystErr = WaitForResponse(OSCOPE, 0.1)
                'FGN-03-D01
                dMeasurement = FetchMeasurement(OSCOPE, "FG Trigger Mode TEST Diagnostic", "9000", "11000", "Hz", OSCOPE, "FGN-03-D01")
                'FGN-03-D02
                If dMeasurement > 11000 Or dMeasurement < 9000 Then
                    Echo("FGN-03-D02     FG Trigger Mode TEST Diagnostic")
                    RegisterFailure(ARB, ReturnTestNumber(FGEN, 3, 2, "D"), dFH_Meas, "Hz", 9000, 11000, "FG Trigger Mode TEST Diagnostic")
                    TestFGen = ARB
                Else
                    'FGN-03-D03
                    Echo("FGN-03-D03     FG Trigger Mode TEST Diagnostic")
                    RegisterFailure(FGEN, ReturnTestNumber(FGEN, 3, 3, "D"), dFH_Meas, "Hz", 9000, 11000, "FG Trigger Mode TEST Diagnostic")
                End If
            End If
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "FGEN" And OptionStep = LoopStepNo Then
            GoTo FGN3
        End If
        frmSTest.proProgress.Value = 60
        LoopStepNo += 1

FGN4:   'FGN-04-001
        '** External Clock Input Test
        'Generate sign waves
        nSystErr = vxiClear(ARB)
        nSystErr = WriteMsg(ARB, "*RST")
        nSystErr = WriteMsg(ARB, "*CLS")
        nSystErr = WriteMsg(ARB, "OUTP:LOAD 50")
        nSystErr = WriteMsg(ARB, "FREQ 1E+6")              'The number of points used to define this wave form is 100.
        nSystErr = WriteMsg(ARB, "FUNC SQU")
        nSystErr = WriteMsg(ARB, "VOLT:UNIT:VOLT VPP")
        'The following levels create valid ECL (-0.8 to -1.6) when driving the Clock In input:
        nSystErr = WriteMsg(ARB, "VOLT 0.8")
        nSystErr = WriteMsg(ARB, "VOLT:OFFS -0.3")
        nSystErr = WriteMsg(ARB, "OUTP:FILT:FREQ 10E6")
        nSystErr = WriteMsg(ARB, "OUTP:FILT ON")
        nSystErr = WriteMsg(ARB, "INIT:IMM")
        nSystErr = WriteMsg(ARB, "OUTP:STAT ON")

        nSystErr = vxiClear(FGEN)

        nSystErr = WriteMsg(FGEN, "*RST")
        nSystErr = WriteMsg(FGEN, "*CLS")

        nSystErr = WriteMsg(FGEN, "OUTP ON")
        nSystErr = WriteMsg(FGEN, "FREQ 1E+6") '1 MHz Freqency to set the points
        nSystErr = WriteMsg(FGEN, "VOLT 2")
        nSystErr = WriteMsg(FGEN, "FREQ:RAST:SOUR EXT")      'Enable the external clock input
        nSystErr = WriteMsg(FGEN, "OUTP ON")
        nSystErr = WriteMsg(FGEN, "VOLT 2VPP")
        Delay(1)
        nSystErr = vxiClear(COUNTER)
        nSystErr = WriteMsg(COUNTER, "*RST;*CLS")
        nSystErr = WriteMsg(COUNTER, "INP1:IMP 50")
        nSystErr = WriteMsg(COUNTER, "INP2:IMP 50")
        nSystErr = WriteMsg(COUNTER, "EVEN:LEV:AUTO ON")
        nSystErr = WriteMsg(COUNTER, "MEAS1:FREQ?")             'Frequency = 1M/100pts ==>  Frequency ~ 10K
        'FGN-04-001
        dMeasurement = FetchMeasurement(COUNTER, "Function Generator External Clock Input Test", "8500", "11500", "Hz", COUNTER, "FGN-04-001")
        frmSTest.proProgress.Value = 80
        If dMeasurement < 8500 Or dMeasurement > 11500 Then
            IncStepFailed()
            TestFGen = FAILED
            If (OptionFaultMode = SOF_BUTTON) And (OptionMode <> LOSmode) Then
                dFH_Meas = dMeasurement
                'FGN-04-D01
                If InstrumentStatus(ARB) = PASSED And RunningEndToEnd Then
                    Echo("FGN-04-D01     Function Generator External Clock Input Test")
                    RegisterFailure(FGEN, ReturnTestNumber(FGEN, 4, 1, "D"), dFH_Meas, "Hz", 9000, 11000, "Function Generator External Clock Input Test")
                    If OptionFaultMode = SOFmode Then GoTo TestComplete
                End If
                S = "Move the W17 cable connection from" & vbCrLf & vbCrLf
                S &= "    FG CLOCK IN   to   SCOPE INPUT 1."
                DisplaySetup(S, "TETS AOP_SI1.jpg", 1)
                If AbortTest = True Then GoTo testcomplete
                nSystErr = WriteMsg(OSCOPE, "*RST")
                nSystErr = WriteMsg(OSCOPE, "*CLS")
                nSystErr = WriteMsg(OSCOPE, "AUT")              'Set Autoscale mode
                Delay(2)

                nSystErr = WriteMsg(OSCOPE, "DIG CHAN1")
                nSystErr = WriteMsg(OSCOPE, "MEAS:SOUR CHAN1")

                nSystErr = WriteMsg(OSCOPE, "MEAS:VAMP?")
                nSystErr = WaitForResponse(OSCOPE, 0.1)
                'FGN-04-D02
                dMeasurement = FetchMeasurement(OSCOPE, "Function Generator External Clock Input Test Diagnostic", "1", "", "V", OSCOPE, "FGN-04-D02")
                'FGN-04-D03
                If dMeasurement < 1 Then
                    Echo("FGN-04-D03     Function Generator External Clock Input Test Diagnostic")
                    TestFGen = ARB
                    RegisterFailure(ARB, ReturnTestNumber(FGEN, 4, 3, "D"), dFH_Meas, "Hz", 9000, 11000, "Function Generator External Clock Input Test Diagnostic")
                    If OptionFaultMode = SOFmode Then GoTo TestComplete
                Else
                    'FGN-04-D04
                    Echo("FGN-04-D04     Function Generator External Clock Input Test Diagnostic")
                    RegisterFailure(FGEN, ReturnTestNumber(FGEN, 4, 4, "D"), dFH_Meas, "Hz", 9000, 11000, "Function Generator External Clock Input Test Diagnostic")
                End If
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            End If
        Else
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "FGEN" And OptionStep = LoopStepNo Then
            GoTo FGN4
        End If
        frmSTest.proProgress.Value = 80
        LoopStepNo += 1

        S = "Move cable W15 connection from" & vbCrLf & vbCrLf
        S &= "    FG OUTPUT   to   FG SYNC OUT."
        DisplaySetup(S, "TETS CI1_FSO.jpg", 1)
        If AbortTest = True Then GoTo testcomplete
FGN5:   'FGN-05-001
        '**SYNC Output Test
        'The same sine waveform is applied to FG main output for this test
        nSystErr = vxiClear(ARB)
        nSystErr = WriteMsg(ARB, "*RST")
        nSystErr = WriteMsg(ARB, "*CLS")
        nSystErr = WriteMsg(FGEN, "*RST")
        nSystErr = WriteMsg(FGEN, "*CLS")
        nSystErr = WriteMsg(FGEN, "OUTP ON")
        Delay(2)
        nSystErr = WriteMsg(FGEN, "FUNC:SHAP SIN")
        nSystErr = WriteMsg(FGEN, "FREQ 1E+6")
        nSystErr = WriteMsg(FGEN, "VOLT 3")

        nSystErr = WriteMsg(FGEN, "OUTP:SYNC ON")            'Enable the SYNC output (Default: OFF)
        nSystErr = WriteMsg(FGEN, "OUTP:SYNC:SOUR BIT")      'Generates a sync signal every time the segment is output.
        nSystErr = WriteMsg(FGEN, "OUTP:SYNC:POS:POINT 2")
        nSystErr = WriteMsg(FGEN, "OUTP ON")
        Delay(0.5)

        nSystErr = vxiClear(COUNTER)
        nSystErr = WriteMsg(COUNTER, "*RST")
        nSystErr = WriteMsg(COUNTER, "INP1:IMP 1e6")
        nSystErr = WriteMsg(COUNTER, "INP2:IMP 1e6")
        nSystErr = WriteMsg(COUNTER, "EVEN:LEV:AUTO OFF")
        nSystErr = WriteMsg(COUNTER, "SENS1:EVEN:HYST MAX")
        nSystErr = WriteMsg(COUNTER, "SENS1:EVEN:LEV 1")
        nSystErr = WriteMsg(COUNTER, "MEAS1:FREQ?")
        Delay(1)
        'FGN-05-001
        dMeasurement = FetchMeasurement(COUNTER, "SYNC Output Test", "950E3", "1050E3", "Hz", FGEN, "FGN-05-001")
        If dMeasurement < 950000.0# Or dMeasurement > 1050000.0# Then
            TestFGen = FAILED
            IncStepFailed()
            If (OptionFaultMode = SOF_BUTTON) And (OptionMode <> LOSmode) Then
                dFH_Meas = dMeasurement

                'KHSU diagnostic test added per DR# 163
                S = "Use cable W15 to connect" & vbCrLf & vbCrLf
                S &= "   C/T INPUT 1   to   ARB OUTPUT."
                DisplaySetup(S, "TETS AOP_CI1.jpg", 1)
                If AbortTest = True Then GoTo testcomplete
                nSystErr = WriteMsg(ARB, "*RST")
                nSystErr = WriteMsg(ARB, "*CLS")
                nSystErr = WriteMsg(ARB, "MARK:STAT OFF") 'Disable the AFG to output a marker list    
                nSystErr = WriteMsg(ARB, "OUTP:LOAD 50")
                nSystErr = WriteMsg(ARB, "FREQ 1E+6")      '10 KHz Freqency
                nSystErr = WriteMsg(ARB, "FUNC SIN")
                nSystErr = WriteMsg(ARB, "VOLT:UNIT:VOLT VPP")
                nSystErr = WriteMsg(ARB, "VOLT 3")
                nSystErr = WriteMsg(ARB, "INIT:IMM")
                nSystErr = WriteMsg(ARB, "OUTP:STAT ON")
                'FGN-05-D01
                dMeasurement = FetchMeasurement(COUNTER, "C/T Diagnostic Test", "950E3", "1050E3", "Hz", COUNTER, "FGN-05-D01")
                'FGN-05-D02
                If dMeasurement < 950000.0# Or dMeasurement > 1050000.0# Then
                    Echo("FGN-05-D02     C/T Diagnostic Test")
                    RegisterFailure(COUNTER, ReturnTestNumber(FGEN, 5, 2, "D"), dFH_Meas, "Hz", 9000, 11000, "C/T Diagnostic Test")
                    TestFGen = FGEN
                Else
                    'FGN-05-D03
                    Echo("FGN-05-D03     C/T Diagnostic Test")
                    RegisterFailure(FGEN, ReturnTestNumber(FGEN, 5, 3, "D"), dFH_Meas, "Hz", 9000, 11000, "C/T Diagnostic Test")
                End If
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            End If
        Else
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "FGEN" And OptionStep = LoopStepNo Then
            GoTo FGN5
        End If
        frmSTest.proProgress.Value = 100

TestComplete:
        frmSTest.cmdAbort.Text = "Abort Test"
        frmSTest.cmdPause.Text = "Pause Test"
        nSystErr = WriteMsg(SWITCH1, "RESET") ' open all switches
        nSystErr = vxiClear(COUNTER)
        nSystErr = WriteMsg(COUNTER, "*RST")
        nSystErr = WriteMsg(COUNTER, "*CLS")
        nSystErr = vxiClear(ARB)
        nSystErr = WriteMsg(ARB, "*RST")
        nSystErr = WriteMsg(ARB, "*CLS")
        nSystErr = WriteMsg(ARB, "MARK:STAT OFF") 'Disable the AFG to output a marker list    

        nSystErr = vxiClear(FGEN)
        nSystErr = WriteMsg(FGEN, "*RST")
        nSystErr = WriteMsg(FGEN, "*CLS")
        nSystErr = vxiClear(OSCOPE)
        nSystErr = WriteMsg(OSCOPE, "*RST")
        nSystErr = WriteMsg(OSCOPE, "*CLS")

        Application.DoEvents()
        If AbortTest = True Then
            If TestFGen = FAILED Then
                ReportFailure(FGEN)
            Else
                ReportUnknown(FGEN)
                TestFGen = UNTESTED

            End If
            sMsg = vbCrLf
            sMsg &= "      **********************************************" & vbCrLf
            sMsg &= "      *     Function Generator tests aborted!      *" & vbCrLf
            sMsg &= "      **********************************************" & vbCrLf
            Echo(sMsg)
        ElseIf TestFGen = PASSED Then
            ReportPass(FGEN)
        ElseIf TestFGen = FAILED Then
            ReportFailure(FGEN)
        Else
            ReportUnknown(FGEN)
        End If


    End Function

End Module