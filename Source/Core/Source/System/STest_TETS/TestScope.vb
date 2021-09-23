'Option Strict Off
Option Explicit On

Public Module modScopeTest

    '**************************************************************
    '* Nomenclature   : ATS-TETS SYSTEM SELF TEST                 *
    '*                  Oscilloscope Test                         *
    '* Version        : 2.0                                       *
    '* Last Update    : Apr 1, 2017                               *
    '* Purpose        : This module contains code for the         *
    '*                  Oscilloscope Self Test                    *
    '**************************************************************

    Dim S As String = ""

    Public Function TestScope() As Integer
        'DESCRIPTION:
        '   This routine tests SCOPE and returns PASSED or FAILED
        'RETURNS:
        '   PASSED if the SCOPE test passes or FAILED if a failure occurs
        Dim strSelfTest As String = ""
        Dim dMeasurement As Double
        Dim Measurement1 As Double
        Dim Measurement2 As Double
        Dim LoopStepNo As Integer = 1

        HelpContextID = 1190
        'Oscilloscope Test Title block
        EchoTitle(InstrumentDescription(OSCOPE), True)
        EchoTitle("Oscilloscope Test", False)
        frmSTest.proProgress.Maximum = 100
        frmSTest.proProgress.Value = 1

        TestScope = UNTESTED
        If AbortTest = True Then GoTo TestComplete
        AbortTest = False

        MsgBox("Remove all cable connections from the SAIF.")

        TestScope = PASSED

        nSystErr = vxiClear(OSCOPE)
        nSystErr = vxiClear(DMM)
        nSystErr = vxiClear(ARB)
        nSystErr = vxiClear(COUNTER)
        LoopStepNo = 1

DSO_1:  '** Perform a SCOPE Power-up Self Test
        'DSO-01-001
        Echo("DSO-01-001 Oscilloscope Built-In Test (BIT takes 20 seconds)")
        nSystErr = WriteMsg(OSCOPE, "*RST")
        nSystErr = WriteMsg(OSCOPE, "*CLS")
        nSystErr = WriteMsg(OSCOPE, "SUMM:PRES")    'Preset the scope
        nSystErr = WriteMsg(OSCOPE, "*TST?")        'Perform test all routine
        nSystErr = WaitForResponse(OSCOPE, 0.22)
        nSystErr = ReadMsg(OSCOPE, strSelfTest)
        If nSystErr <> 0 Or Val(strSelfTest) <> 0 Then
            Echo(FormatResults(False, "Oscilloscope Built-In Test"))
            RegisterFailure(OSCOPE, ReturnTestNumber(OSCOPE, 1, 1), , , , , sGuiLabel(OSCOPE) & " FAILED Built-in Test")
            TestScope = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            Echo(FormatResults(True, "Oscilloscope Built-In Test"))
            IncStepPassed()
        End If  '1
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "DSO" And OptionStep = LoopStepNo Then
            GoTo DSO_1
        End If
        frmSTest.proProgress.Value = 50
        LoopStepNo += 1

        '** SCOPE Frequency Measurement Test
        nSystErr = WriteMsg(OSCOPE, "SYST:LANG COMP")
Step1:
        S = "Use cable  W15 to connect" & vbCrLf & vbCrLf
        S &= "   ARB OUTPUT   to   SCOPE INPUT 1. "
        DisplaySetup(S, "TETS AOP_SI1.jpg", 1, True, 1, 3)
        If AbortTest = True Then GoTo testcomplete
Step2:
        S = "Use cable W16  to connect" & vbCrLf & vbCrLf
        S &= "   ARB MARKER OUT   to   SCOPE TRIG IN."
        DisplaySetup(S, "TETS AMO_STI.jpg", 1, True, 2, 3)
        If AbortTest = True Then GoTo testcomplete
        If GoBack = True Then GoTo Step1
Step3:
        S = "Use cable W25 (OBSERVE POLARITY) to connect" & vbCrLf & vbCrLf
        S &= "   DMM INPUT (HI/LO)   to" & vbCrLf
        S &= "   SCOPE PROBE COMP/CAL/TRIG OUTPUT "
        DisplaySetup(S, "TETS DIN_SPC.jpg", 1, True, 3, 3)
        If AbortTest = True Then GoTo testcomplete
        If GoBack = True Then GoTo Step2

        'Generate 10 MHz test input signal
        nSystErr = WriteMsg(ARB, "*RST")
        nSystErr = WriteMsg(ARB, "*CLS")
        nSystErr = WriteMsg(ARB, "MARK:STAT OFF") 'Disable the AFG to output a marker list    
        Delay(1)
        nSystErr = WriteMsg(ARB, "VOLT:UNIT:VOLT VPP")
        nSystErr = WriteMsg(ARB, "FREQ 1E+6")
        nSystErr = WriteMsg(ARB, "FUNC SIN")
        nSystErr = WriteMsg(ARB, "OUTP:LOAD 50")
        nSystErr = WriteMsg(ARB, "VOLT 2")
        nSystErr = WriteMsg(ARB, "OUTP:FILT:FREQ 10E6")
        nSystErr = WriteMsg(ARB, "OUTP:FILT ON")
        nSystErr = WriteMsg(ARB, "INIT:IMM")
        nSystErr = WriteMsg(ARB, "OUTP:STAT ON")

DSO_2:  nSystErr = WriteMsg(OSCOPE, "*RST")
        nSystErr = WriteMsg(OSCOPE, "*CLS")
        nSystErr = WriteMsg(OSCOPE, "SYST:LANG COMP")
        Delay(1) ' needed on HP scope to let previous command complete
        nSystErr = WriteMsg(OSCOPE, "BLAN CHAN2") 'TURN OFF CHAN2
        nSystErr = WriteMsg(OSCOPE, "VIEW CHAN1") 'TURN ON CHAN1
        nSystErr = WriteMsg(OSCOPE, "CHAN1:RANG 10V")
        nSystErr = WriteMsg(OSCOPE, "TRIG:LEV 0V")
        nSystErr = WriteMsg(OSCOPE, "TIM:RANG 4000NS")
        nSystErr = WriteMsg(OSCOPE, "CHAN1:COUP DCFIFTY")
        nSystErr = WriteMsg(OSCOPE, "CHAN2:COUP DCFIFTY")
        nSystErr = WriteMsg(OSCOPE, "DIG CHAN1")
        nSystErr = WriteMsg(OSCOPE, "MEAS:SOUR CHAN1")
        nSystErr = WriteMsg(OSCOPE, "MEAS:FREQ?")
        Delay(0.5)
        'DSO-02-001
        dMeasurement = FetchMeasurement(OSCOPE, "SCOPE Frequency Measurement Test", "90E+4", "110E+4", "Hz", OSCOPE, "DSO-02-001", 1)
        If dMeasurement > 1100000 Or dMeasurement < 900000 Then
            TestScope = FAILED
            dFH_Meas = dMeasurement
            S = "Move cable W15 connection from" & vbCrLf & vbCrLf
            S &= "   SCOPE INPUT 1   to   C/T INPUT 1."
            DisplaySetup(S, "TETS AOP_CI1.jpg", 1)
            If AbortTest = True Then GoTo testcomplete
            nSystErr = WriteMsg(COUNTER, "*RST")
            nSystErr = WriteMsg(COUNTER, "*CLS")
            nSystErr = WriteMsg(COUNTER, "INP1:IMP 50")
            nSystErr = WriteMsg(COUNTER, "INP2:IMP 50")
            nSystErr = WriteMsg(COUNTER, "EVEN:LEV:AUTO ON")    'Place counter in auto-triggering mode
            nSystErr = WriteMsg(COUNTER, "MEAS1:FREQ?")
            'DSO-02-D01
            dMeasurement = FetchMeasurement(COUNTER, "SCOPE Frequency Measurement Diagnostic Test", "90E+4", "110E+4", "Hz", COUNTER, "DSO-02-D01")
            'DSO-02-D02
            If dMeasurement > 1100000 Or dMeasurement < 900000 Then
                RegisterFailure(ARB, ReturnTestNumber(OSCOPE, 2, 2, "D"), dFH_Meas, "Hz", 900000, 1100000, "SCOPE Frequency Measurement Diagnostic Test")
                TestScope = ARB
            Else
                'DSO-02-D03
                RegisterFailure(OSCOPE, ReturnTestNumber(OSCOPE, 2, 3, "D"), dFH_Meas, "Hz", 900000, 1100000, "SCOPE Frequency Measurement Diagnostic Test")
            End If
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "DSO" And OptionStep = LoopStepNo Then
            GoTo DSO_2
        End If
        frmSTest.proProgress.Value = 55
        LoopStepNo += 1

DSO_3:  'SCOPE Voltage Measurement Test, Channel 1
        nSystErr = WriteMsg(OSCOPE, "CHAN1:COUP DCFIFTY")
        nSystErr = WriteMsg(OSCOPE, "AUT")     'Set Autoscale mode
        Delay(3)
        nSystErr = WriteMsg(OSCOPE, "DIG CHAN1")
        nSystErr = WriteMsg(OSCOPE, "MEAS:SOUR CHAN1")
        nSystErr = WriteMsg(OSCOPE, "MEAS:VPP?")
        'DSO-03-001
        dMeasurement = FetchMeasurement(OSCOPE, "SCOPE Voltage Measurement Test, Channel 1", "1.5", "2.5", "VPP", OSCOPE, "DSO-03-001")
        'DSO-03-D01
        If dMeasurement > 2.5 Or dMeasurement < 1.5 Then
            dFH_Meas = dMeasurement
            Echo("    Probable cause of failure - Scope Channel 1 50 Ohm input circuit.")
            RegisterFailure(OSCOPE, ReturnTestNumber(OSCOPE, 3, 1, "D"), dFH_Meas, "VPP", 1.5, 2.5, "SCOPE Voltage Measurement Test, Channel 1")
            TestScope = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "DSO" And OptionStep = LoopStepNo Then
            GoTo DSO_3
        End If
        frmSTest.proProgress.Value = 60
        LoopStepNo += 1

        ' Signal connected to Input channel 2
        S = "Move cable W15 connection from" & vbCrLf & vbCrLf
        S &= "   SCOPE INPUT 1   to   SCOPE INPUT 2."
        DisplaySetup(S, "TETS AOP_SI2.jpg", 1)
        If AbortTest = True Then GoTo testcomplete

DSO_4:  '** SCOPE Autoscale Period Measurement Test
        nSystErr = WriteMsg(OSCOPE, "*RST; *CLS")
        nSystErr = WriteMsg(OSCOPE, "AUT")     'Set Autoscale mode
        Delay(3)
        nSystErr = WriteMsg(OSCOPE, "DIG CHAN2")
        nSystErr = WriteMsg(OSCOPE, "MEAS:SOUR CHAN2")
        nSystErr = WriteMsg(OSCOPE, "MEAS:PER?")
        'DSO-04-001
        dMeasurement = FetchMeasurement(OSCOPE, "SCOPE Period Measurement Test, Channel 2", "9E-7", "1.1E-6", "sec", OSCOPE, "DSO-04-001")
        'DSO-04-D01
        If dMeasurement > 0.0000011 Or dMeasurement < 0.0000009 Then
            Echo("DSO-04-D01     SCOPE Period Measurement Test, Channel 2")
            dFH_Meas = dMeasurement
            RegisterFailure(OSCOPE, ReturnTestNumber(OSCOPE, 4, 1, "D"), dFH_Meas, "sec", 0.0000009, 0.0000011, "SCOPE Period Measurement Test, Channel 2")
            TestScope = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If      '3
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "DSO" And OptionStep = LoopStepNo Then
            GoTo DSO_4
        End If
        frmSTest.proProgress.Value = 70
        LoopStepNo += 1

DSO_5:  nSystErr = WriteMsg(OSCOPE, "CHAN2:COUP DCFIFTY")
        nSystErr = WriteMsg(OSCOPE, "AUT")     'Set Autoscale mode
        Delay(3)
        nSystErr = WriteMsg(OSCOPE, "DIG CHAN2")
        nSystErr = WriteMsg(OSCOPE, "MEAS:SOUR CHAN2")
        nSystErr = WriteMsg(OSCOPE, "MEAS:VPP?")
        'DSO-05-001
        dMeasurement = FetchMeasurement(OSCOPE, "SCOPE Voltage Measurement Test, Channel 2", "1.5", "2.5", "VPP", OSCOPE, "DSO-05-001")
        'DSO-05-D01
        If dMeasurement > 2.5 Or dMeasurement < 1.5 Then
            dFH_Meas = dMeasurement
            Echo("    Probable cause of failure - Scope Channel 2 50 Ohm input circuit.")
            RegisterFailure(OSCOPE, ReturnTestNumber(OSCOPE, 5, 1, "D"), dFH_Meas, "VPP", 1.5, 2.5, "SCOPE Voltage Measurement Test, Channel 2")
            TestScope = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "DSO" And OptionStep = LoopStepNo Then
            GoTo DSO_5
        End If
        frmSTest.proProgress.Value = 80
        LoopStepNo += 1

        '** Ext. Trigg Test: Capture the signal at Channel 2 and measure the period at Ext. Trigger.

DSO_6:  'Generate marker pulses
        nSystErr = WriteMsg(OSCOPE, "*RST; *CLS")
        nSystErr = WriteMsg(OSCOPE, "AUT")     'Set Autoscale mode
        Delay(3)
        nSystErr = WriteMsg(ARB, "MARKER:POL NORM")
        nSystErr = WriteMsg(ARB, "MARK:STAT ON")                'Enable the AFG to output a marker list
        nSystErr = WriteMsg(OSCOPE, "TRIG:SOUR EXT")             'Trigger source to EXT Trig
        nSystErr = WriteMsg(OSCOPE, "TIM:MODE TRIG")
        nSystErr = WriteMsg(OSCOPE, "TRIG:LEV 1")
        nSystErr = WriteMsg(OSCOPE, "DIG CHAN2")
        nSystErr = WriteMsg(OSCOPE, "MEAS:SOUR CHAN2")
        nSystErr = WriteMsg(OSCOPE, "MEAS:PER?")   'if the signal is not present, 9.99999E+37 returned
        'DSO-06-001
        dMeasurement = FetchMeasurement(OSCOPE, "SCOPE External Trigger Test", "9E-7", "1.1E-6", "sec", OSCOPE, "DSO-06-001")
        nSystErr = WriteMsg(OSCOPE, "TIM:MODE AUTO")
        nSystErr = WriteMsg(OSCOPE, "TRIG:SOUR CHAN2")

        If dMeasurement > 0.0000011 Or dMeasurement < 0.0000009 Then
            TestScope = FAILED
            dFH_Meas = dMeasurement
            S = "Move cable W16 connection from" & vbCrLf & vbCrLf
            S &= "   SCOPE TRIG IN   to   SCOPE INPUT 1."
            DisplaySetup(S, "TETS AMO_SI1.jpg", 1)
            If AbortTest = True Then GoTo testcomplete
            nSystErr = vxiClear(OSCOPE)
            nSystErr = WriteMsg(OSCOPE, "*RST; *CLS")
            nSystErr = WriteMsg(OSCOPE, "AUT")     'Set Autoscale mode
            Delay(2)
            nSystErr = WriteMsg(OSCOPE, "DIG CHAN1")
            nSystErr = WriteMsg(OSCOPE, "MEAS:SOUR CHAN1")
            nSystErr = WriteMsg(OSCOPE, "MEAS:VAMP?")
            'DSO-06-D01
            dMeasurement = FetchMeasurement(OSCOPE, "SCOPE External Trigger Diagnostic Test", "1.5", "", "V", OSCOPE, "DSO-06-D01")
            'DSO-06-D02
            If dMeasurement < 1.5 Then
                RegisterFailure(ARB, ReturnTestNumber(OSCOPE, 6, 2, "D"), dFH_Meas, "sec", 0.0000009, 0.0000011, "SCOPE External Trigger Diagnostic Test")
                TestScope = ARB
            Else
                'DSO-06-D03
                RegisterFailure(OSCOPE, ReturnTestNumber(OSCOPE, 6, 3, "D"), dFH_Meas, "sec", 0.0000009, 0.0000011, "SCOPE External Trigger Diagnostic Test")
            End If
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "DSO" And OptionStep = LoopStepNo Then
            GoTo DSO_6
        End If
        frmSTest.proProgress.Value = 90
        LoopStepNo += 1

DSO_7:  '**DC Calibrator Test Procedure
        nSystErr = WriteMsg(OSCOPE, "*RST")                 'Reset the scope
        nSystErr = WriteMsg(OSCOPE, "*CLS")
        Delay(0.3)
        nSystErr = WriteMsg(OSCOPE, "CAL:SCAL:DOUT ZVOL")   'DC Calibrator Output to 0V
        nSystErr = WriteMsg(DMM, "*RST")
        nSystErr = WriteMsg(DMM, "*CLS")
        nSystErr = WriteMsg(DMM, "CONF:VOLT:DC")
        nSystErr = WriteMsg(DMM, "INIT;FETCH?")
        'DSO-07-001
        Measurement1# = FetchMeasurement(DMM, "SCOPE DC Calibrator Test 1", "-0.2", "0.2", "V", DMM, "DSO-07-001")
        If dMeasurement < -0.2 Or dMeasurement > 0.2 Then
            TestScope = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "DSO" And OptionStep = LoopStepNo Then
            GoTo DSO_7
        End If
        LoopStepNo += 1

DSO_8:  'DSO-08-001
        nSystErr = WriteMsg(OSCOPE, "*RST") 'Reset the scope
        nSystErr = WriteMsg(OSCOPE, "*CLS")
        Delay(0.3)
        nSystErr = WriteMsg(OSCOPE, "CAL:SCAL:DOUT FVOL") 'DC Calibrator Output to 5V
        nSystErr = WriteMsg(DMM, "*RST")
        nSystErr = WriteMsg(DMM, "*CLS")
        nSystErr = WriteMsg(DMM, "CONF:VOLT:DC")
        nSystErr = WriteMsg(DMM, "INIT;FETCH?")
        'DSO-08-001
        Measurement2# = FetchMeasurement(DMM, "SCOPE DC Calibrator Test 2", "4", "5.5", "V", DMM, "DSO-08-001")

        dMeasurement = Measurement2# - Measurement1#
        If dMeasurement > 5.5 Or dMeasurement < 4 Then
            'DSO-08-D01
            TestScope = FAILED
            IncStepFailed()
            If (OptionFaultMode = SOFmode) And (OptionMode <> LOSmode) Then
                If InstrumentStatus(DMM) = PASSED And RunningEndToEnd Then
                    Echo("DSO-08-D01     SCOPE DC Calibrator Test 2")
                    RegisterFailure(OSCOPE, ReturnTestNumber(OSCOPE, 7, 1, "D"), dMeasurement, "V", 4, 5.5, "SCOPE DC Calibrator Test 2")
                    If OptionFaultMode = SOFmode Then GoTo TestComplete
                End If
                S = "Disconnect cable W25." & vbCrLf & vbCrLf
                S &= "Use cable W17 to connect " & vbCrLf & vbCrLf
                S &= "   SCOPE INPUT 1  to  SCOPE PROBE COMP/CAL/TRIG OUT"
                DisplaySetup(S, "TETS SI1_SPC.jpg", 1)
                If AbortTest = True Then GoTo testcomplete
                nSystErr = WriteMsg(OSCOPE, "CAL:SCAL:DOUT ZVOL")   'DC Calibrator Output to 0Vs
                nSystErr = WriteMsg(OSCOPE, "*RST")                 'Reset the scope
                nSystErr = WriteMsg(OSCOPE, "TIM:RANG 1E-3")
                nSystErr = WriteMsg(OSCOPE, "CHAN1:RANG 20")
                nSystErr = WriteMsg(OSCOPE, "CHAN1:COUP DC")
                nSystErr = WriteMsg(OSCOPE, "TIM:MODE AUTO")
                nSystErr = WriteMsg(OSCOPE, "DIG CHAN1")
                nSystErr = WriteMsg(OSCOPE, "MEAS:SOUR CHAN1")
                nSystErr = WriteMsg(OSCOPE, "MEAS:VDCR?")
                'DSO-08-D02
                Measurement1# = FetchMeasurement(OSCOPE, "SCOPE DC Calibrator Test Diagnostic Test, 0V", "", "", "V", OSCOPE, "DSO-08-D02")

                'Reset the scope
                nSystErr = WriteMsg(OSCOPE, "CAL:SCAL:DOUT FVOL")   'DC Calibrator Output to 5V
                nSystErr = WriteMsg(OSCOPE, "DIG CHAN1")
                nSystErr = WriteMsg(OSCOPE, "MEAS:SOUR CHAN1")
                nSystErr = WriteMsg(OSCOPE, "MEAS:VDCR?")
                'DSO-08-D03
                Measurement2# = FetchMeasurement(OSCOPE, "SCOPE DC Calibrator Test Diagnostic Test, 5V", "", "", "Volts", OSCOPE, "DSO-08-D03")
                dMeasurement = Measurement2# - Measurement1#
                'DSO-08-D04
                If dMeasurement > 5.5 Or dMeasurement < 4 Then
                    Echo("DSO-07-D04     SCOPE DC Calibrator Test Diagnostic Test, 5V")
                    RegisterFailure(OSCOPE, ReturnTestNumber(OSCOPE, 7, 4, "D"), dMeasurement, "V", 4, 5.5, "SCOPE DC Calibrator Test Diagnostic Test, 5V")
                    If OptionFaultMode = SOFmode Then GoTo TestComplete
                Else
                    'DSO-08-D05
                    Echo("DSO-07-D05     SCOPE DC Calibrator Test Diagnostic Test, 5V")
                    RegisterFailure(DMM, ReturnTestNumber(OSCOPE, 7, 5, "D"), dMeasurement, "V", , , "SCOPE DC Calibrator Test Diagnostic Test, 5V")
                    TestScope = DMM
                End If
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            End If
        Else
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "DSO" And OptionStep = LoopStepNo Then
            GoTo DSO_8
        End If
        frmSTest.proProgress.Value = 100

TestComplete:
        frmSTest.cmdAbort.Text = "Abort Test"
        frmSTest.cmdPause.Text = "Pause Test"
        nSystErr = WriteMsg(SWITCH1, "RESET") ' open all switches
        nSystErr = vxiClear(COUNTER)
        nSystErr = WriteMsg(COUNTER, "*RST")
        nSystErr = vxiClear(ARB)
        nSystErr = WriteMsg(ARB, "*RST")
        nSystErr = WriteMsg(ARB, "*CLS")
        nSystErr = WriteMsg(ARB, "MARK:STAT OFF") 'Disable the AFG to output a marker list    
        nSystErr = WriteMsg(ARB, "OUTP:LOAD 50")
        nSystErr = vxiClear(FGEN)
        nSystErr = WriteMsg(FGEN, "*RST")
        nSystErr = WriteMsg(FGEN, "*CLS")
        nSystErr = vxiClear(OSCOPE)
        nSystErr = WriteMsg(OSCOPE, "*RST")
        nSystErr = WriteMsg(OSCOPE, "*CLS")

        If AbortTest = True Then
            If TestScope = FAILED Then
                ReportFailure(OSCOPE)
            Else
                ReportUnknown(OSCOPE)
                TestScope = UNTESTED
            End If
            sMsg = vbCrLf
            sMsg &= "      **********************************************" & vbCrLf
            sMsg &= "      *        Oscilloscope tests aborted!         *" & vbCrLf
            sMsg &= "      **********************************************" & vbCrLf
            Echo(sMsg)
        ElseIf TestScope = PASSED Then
            ReportPass(OSCOPE)
        ElseIf TestScope = FAILED Then
            ReportFailure(OSCOPE)
        Else
            ReportUnknown(OSCOPE)
        End If

    End Function

End Module
