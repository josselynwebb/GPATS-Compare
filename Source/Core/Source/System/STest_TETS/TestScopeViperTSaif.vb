'Option Strict Off
Option Explicit On

Imports System.Windows.Forms

Public Module modScopeViperTSaif


    '=========================================================
    '**************************************************************
    '* Nomenclature   : TETS SYSTEM SELF TEST                     *
    '*                  Oscilloscope Test                         *
    '* Version        : 2.0                                       *
    '* Last Update    : Apr 1, 2017                               *
    '* Purpose        : This module contains code for the         *
    '*                  Oscilloscope Self Test                    *
    '**************************************************************


    Public Function TestScopeVT() As Integer

        'DESCRIPTION:
        '   This routine tests SCOPE and returns PASSED or FAILED
        'This test is for both HP1428A and ZTEC1428

        'RETURNS:
        '   PASSED if the SCOPE test passes or FAILED if a failure occurs
        Dim strSelfTest As String = ""
        Dim SelfTest As Integer
        Dim Measurement1 As Double
        Dim Measurement2 As Double
        Dim S As String
        Dim x As DialogResult
        Dim lStatus As Integer
        Dim LoopStepNo As Integer

        HelpContextID = 1190
        'Oscilloscope Test Title block
        EchoTitle(InstrumentDescription(OSCOPE) & " ", True)
        EchoTitle("Oscilloscope Test", False)
        frmSTest.proProgress.Maximum = 100
        frmSTest.proProgress.Value = 1

        TestScopeVT = UNTESTED
        If AbortTest = True Then GoTo TestComplete
        AbortTest = False
        TestScopeVT = PASSED

        'bb
        If RunningEndToEnd = False And FirstPass = True Then
            x = MsgBox("Is the W201 cable installed?", MsgBoxStyle.YesNo)
            If x <> DialogResult.Yes Then
                'Install the W201 cable
                S = "Remove all cables/adapters from the SAIF." & vbCrLf
                S += "Connect W201 cable to SAIF as follows:" & vbCrLf
                S += " 1. P1 to SAIF J1 connector." & vbCrLf
                S += " 2. P10 to SAIF ARB OUTPUT." & vbCrLf
                S += " 3. P12 to SAIF ARB MARKER OUT." & vbCrLf
                S += " 4. P15 to SAIF SCOPE INPUT 1." & vbCrLf
                S += " 5. P16 to SAIF SCOPE INPUT 2." & vbCrLf
                S += " 6. P17 to SAIF SCOPE TRIG IN." & vbCrLf
                S += " 7. P18 to SAIF SCOPE COMP CAL." & vbCrLf
                S += " 8. P19 to SAIF C/T INPUT 1."
                DisplaySetup(S, "ST-W201-1.jpg", 8)
                If AbortTest = True Then GoTo TestComplete
            End If
        End If

        nSystErr = WriteMsg(SWITCH1, "RESET") 'open all switches
        LoopStepNo = 1

DSO1:   'DSO-01-001
        nSystErr = vxiClear(OSCOPE)
        nSystErr = vxiClear(DMM)
        nSystErr = vxiClear(ARB)
        nSystErr = vxiClear(COUNTER)

        '** Perform a SCOPE Power-up Self Test
        nSystErr = WriteMsg(OSCOPE, "*CLS; *RST")
        nSystErr = WriteMsg(OSCOPE, "SUMM:PRES") 'Preset the scope
        nSystErr = WriteMsg(OSCOPE, "*TST?") 'Perform test all routine
        WaitForResponse(OSCOPE, 0.1)
        SelfTest = ReadMsg(OSCOPE, strSelfTest)

        Echo("DSO-01-001 SCOPE Built-In Test. . .")
        If SelfTest <> 0 Or Val(strSelfTest) <> 0 Then
            Echo(FormatResults(False, "Oscilloscope Built-In Test"))
            RegisterFailure(OSCOPE, "DSO-01-001", , , , , sGuiLabel(OSCOPE) & " FAILED Built-in Test")
            TestScopeVT = FAILED
            IncStepFailed()
            GoTo TestComplete ' no point in continuing
        Else
            Echo(FormatResults(True, "SCOPE Built-In Test"))
            IncStepPassed()
        End If '1
        Application.Doevents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "DSO" And OptionStep = LoopStepNo Then
            GoTo DSO1
        End If
        frmSTest.proProgress.Value = 50
        LoopStepNo += 1

DSO2:   'DSO-02-001
        '** SCOPE Frequency Measurement Test
        'Generate 1 MHz test input signal
        nSystErr = WriteMsg(ARB, "*RST;*CLS")
        Delay(1)
        nSystErr = WriteMsg(ARB, "VOLT:UNIT:VOLT VPP")
        nSystErr = WriteMsg(ARB, "FREQ 1E+6")
        nSystErr = WriteMsg(ARB, "FUNC SIN")
        nSystErr = WriteMsg(ARB, "VOLT 2")
        nSystErr = WriteMsg(ARB, "OUTP:FILT:FREQ 10E6")
        nSystErr = WriteMsg(ARB, "OUTP:FILT ON")
        nSystErr = WriteMsg(ARB, "INIT")

        ' Connect P10 ARB OUTPUT to P15 SCOPE INPUT 1.
        nSystErr = WriteMsg(SWITCH1, "CLOSE 2.3003") 'close S404-1,5 bb
        ' Connect P12 ARB MARKER OUT to P17 SCOPE TRIG IN.
        nSystErr = WriteMsg(SWITCH1, "CLOSE 1.3000") 'close S401-1,2 bb
        ' Connect DMM INPUT (HI/LO) to SCOPE PROBE COMP/CAL/TRIG OUTPUT.
        nSystErr = WriteMsg(SWITCH1, "CLOSE 1.1002,1003") 'close S301-5,6, S301-7,8 bb
        Delay(1)

        nSystErr = WriteMsg(OSCOPE, "*RST; *CLS")
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

        dMeasurement = FetchMeasurement(OSCOPE, "SCOPE Frequency Measurement Test, Channel 1", "90E+4", "110E+4", "Hz", OSCOPE, "DSO-02-001")
        If dMeasurement < 900000 Or dMeasurement > 1100000 Then
            'DSO-02-D01
            IncStepFailed()
            dFH_Meas = dMeasurement
            'disconnect P10 Arb output from P15 SCOPE INPUT 1"
            nSystErr = WriteMsg(SWITCH1, "OPEN 2.3003") 'open  S404-1,5 bb
            'Connect P10 Arb output to P19 C/T INPUT 1."
            nSystErr = WriteMsg(SWITCH1, "CLOSE 2.3001") 'close S404-1,3 bb
            nSystErr = WriteMsg(COUNTER, "*RST;*CLS")
            nSystErr = WriteMsg(COUNTER, "INP1:IMP 50")
            nSystErr = WriteMsg(COUNTER, "INP2:IMP 50")
            nSystErr = WriteMsg(COUNTER, "EVEN:LEV:AUTO ON") 'Place counter in auto-triggering mode
            nSystErr = WriteMsg(COUNTER, "MEAS1:FREQ?")
            dMeasurement = FetchMeasurement(COUNTER, "SCOPE Frequency Measurement Diagnostic Test", "90E+4", "110E+4", "Hz", OSCOPE, "DSO-02-D01")
            If dMeasurement > 1100000 Or dMeasurement < 900000 Then
                Echo(FormatResults(False, "DSO Freq Measurement Test Diagnostic", "DSO-02-D01"))
                RegisterFailure(ARB, "DSO-02-D01", dFH_Meas, "Hz", 900000, 1100000, "SCOPE Frequency Measurement Diagnostic Test")
                TestScopeVT = ARB
            Else
                Echo(FormatResults(False, "DSO Freq Measurement Test Diagnostic", "DSO-02-D01"))
                RegisterFailure(OSCOPE, "DSO-02-D01", dFH_Meas, "Hz", 900000, 1100000, "SCOPE Frequency Measurement Diagnostic Test")
                TestScopeVT = FAILED
            End If
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        Application.Doevents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "DSO" And OptionStep = LoopStepNo Then
            GoTo DSO2
        End If
        frmSTest.proProgress.Value = 55
        LoopStepNo += 1

DSO3:   'DSO-03-001
        nSystErr = WriteMsg(OSCOPE, "CHAN1:COUP DCFIFTY")
        nSystErr = WriteMsg(OSCOPE, "CHAN2:COUP DCFIFTY")
        nSystErr = WriteMsg(OSCOPE, "VIEW CHAN1") 'TURN ON CHAN1
        nSystErr = WriteMsg(OSCOPE, "CHAN1:RANG 10V")
        nSystErr = WriteMsg(OSCOPE, "TRIG:LEV 0V")
        nSystErr = WriteMsg(OSCOPE, "TIM:RANG 4000NS")
        nSystErr = WriteMsg(OSCOPE, "DIG CHAN1")
        nSystErr = WriteMsg(OSCOPE, "MEAS:SOUR CHAN1")
        nSystErr = WriteMsg(OSCOPE, "MEAS:VPP?")
        Delay(0.1)

        dMeasurement = FetchMeasurement(OSCOPE, "SCOPE Voltage Measurement Test, Channel 1", "1.0", "2.5", "VPP", OSCOPE, "DSO-03-001")
        If dMeasurement < 1 Or dMeasurement > 2.5 Then
            TestScopeVT = FAILED
            'DSO-03-D01
            dFH_Meas = dMeasurement
            Echo("    Probable cause of failure - Scope Channel 1 50 Ohm input circuit.")
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        Application.Doevents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "DSO" And OptionStep = LoopStepNo Then
            GoTo DSO3
        End If
        frmSTest.proProgress.Value = 60
        LoopStepNo += 1

DSO4:   'DSO-04-001
        ' Signal connected to Input channel 2
        'Disconnect P10 ARB output from P15 SCOPE INPUT 1.
        nSystErr = WriteMsg(SWITCH1, "OPEN 2.3003") 'open  S404-1,5 bb
        'Connect P10 ARB output to P16 SCOPE INPUT 2.
        nSystErr = WriteMsg(SWITCH1, "CLOSE 2.3000") 'close S404-1,2 bb
        Delay(1)

        nSystErr = WriteMsg(OSCOPE, "*RST; *CLS")
        Delay(1)
        '    nSystErr = WriteMsg OSCOPE, "AUT;"
        '    Delay 3
        nSystErr = WriteMsg(OSCOPE, "BLAN CHAN1") 'TURN OFF CHAN1
        nSystErr = WriteMsg(OSCOPE, "VIEW CHAN2") 'TURN ON CHAN2
        nSystErr = WriteMsg(OSCOPE, "CHAN1:COUP DCFIFTY")
        nSystErr = WriteMsg(OSCOPE, "CHAN2:RANG 10V")
        nSystErr = WriteMsg(OSCOPE, "TRIG:LEV 0V")
        nSystErr = WriteMsg(OSCOPE, "TIM:RANG 4000NS")
        nSystErr = WriteMsg(OSCOPE, "CHAN2:COUP AC")
        nSystErr = WriteMsg(OSCOPE, "MEAS:SOUR CHAN2")
        nSystErr = WriteMsg(OSCOPE, "DIG CHAN2")
        nSystErr = WriteMsg(OSCOPE, "MEAS:PER?")
        Delay(1)

        dMeasurement = FetchMeasurement(OSCOPE, "SCOPE Period Measurement Test, Channel 2", "9E-7", "1.1E-6", "sec", OSCOPE, "DSO-04-001")
        If dMeasurement > 0.0000011 Or dMeasurement < 0.0000009 Then
            dFH_Meas = dMeasurement
            TestScopeVT = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If '3
        Application.Doevents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "DSO" And OptionStep = LoopStepNo Then
            GoTo DSO4
        End If
        frmSTest.proProgress.Value = 65
        LoopStepNo += 1

DSO5:   'DSO-05-001
        nSystErr = WriteMsg(OSCOPE, "BLAN CHAN1") 'TURN OFF CHAN1
        nSystErr = WriteMsg(OSCOPE, "VIEW CHAN2") 'TURN ON CHAN2
        nSystErr = WriteMsg(OSCOPE, "CHAN1:COUP DCFIFTY")
        nSystErr = WriteMsg(OSCOPE, "CHAN2:COUP DCFIFTY")
        nSystErr = WriteMsg(OSCOPE, "CHAN2:RANG 10V")
        nSystErr = WriteMsg(OSCOPE, "TRIG:LEV 0V")
        nSystErr = WriteMsg(OSCOPE, "TIM:RANG 4000NS")
        nSystErr = WriteMsg(OSCOPE, "MEAS:SOUR CHAN2")
        nSystErr = WriteMsg(OSCOPE, "DIG CHAN2")
        nSystErr = WriteMsg(OSCOPE, "MEAS:VPP?")

        dMeasurement = FetchMeasurement(OSCOPE, "SCOPE Voltage Measurement Test, Channel 2", "1.0", "2.5", "VPP", OSCOPE, "DSO-05-001")
        If dMeasurement < 1 Or dMeasurement > 2.5 Then
            dFH_Meas = dMeasurement
            Echo("    Probable cause of failure - Scope Channel 2 50 Ohm input circuit.")
            TestScopeVT = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        Application.Doevents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "DSO" And OptionStep = LoopStepNo Then
            GoTo DSO5
        End If
        frmSTest.proProgress.Value = 70
        LoopStepNo += 1

        'DSO-06-001
        '** Ext. Trigg Test: Capture the signal at Channel 2 and measure the period at Ext. Trigger.
        'Generate marker pulses
        sTNum = "DSO-06-001"
        nSystErr = WriteMsg(ARB, "MARKER:POL NORM")
        nSystErr = WriteMsg(ARB, "MARK:STAT ON") 'Enable the AFG to output a marker list

DSO6:   nSystErr = WriteMsg(OSCOPE, "*RST; *CLS")
        Delay(1)
        nSystErr = WriteMsg(OSCOPE, "TRIG:SOUR EXT")
        nSystErr = WriteMsg(OSCOPE, "TRIG:COUP DCF")
        nSystErr = WriteMsg(OSCOPE, "TRIG:COUP DC")
        nSystErr = WriteMsg(OSCOPE, "TIM:MODE SINGLE")
        nSystErr = WriteMsg(OSCOPE, "VIEW CHAN2")
        nSystErr = WriteMsg(OSCOPE, "TRIG:SLOP POS")
        nSystErr = WriteMsg(OSCOPE, "TRIG:LEV 1V") ' causes scope error at 2.5
        nSystErr = WriteMsg(OSCOPE, "RUN")
        Delay(3)
        nSystErr = WriteMsg(OSCOPE, "TER?")
        Delay(0.1)
        lStatus = ReadMsg(OSCOPE, sMsg)
        nSystErr = WriteMsg(OSCOPE, "STOP") ' need this after run

        If Val(sMsg) = 1 Then
            Echo(FormatResults(True, "SCOPE External Trigger Test", sTNum))
            IncStepPassed()
        Else
            Echo(FormatResults(False, "SCOPE External Trigger Test", sTNum))
            IncStepFailed()

            'DSO-06-D01
            If OptionFaultMode = SOFmode Then ' do diagnostics
                'Disconnect P12 ARB Marker Out from P17 Scope Trig In
                nSystErr = WriteMsg(SWITCH1, "OPEN 1.3000") 'open  S401-1,2 bb
                'Connect P12 ARB Marker Out to P15 SCOPE INPUT 1.
                nSystErr = WriteMsg(SWITCH1, "CLOSE 2.3103") 'close S405-1,5 bb

                nSystErr = vxiClear(OSCOPE)
                nSystErr = WriteMsg(OSCOPE, "*RST; *CLS")
                nSystErr = WriteMsg(OSCOPE, "AUT") 'Set Autoscale mode
                Delay(2)
                nSystErr = WriteMsg(OSCOPE, "DIG CHAN1")
                nSystErr = WriteMsg(OSCOPE, "MEAS:SOUR CHAN1")
                nSystErr = WriteMsg(OSCOPE, "MEAS:VAMP?")
                dMeasurement = FetchMeasurement(OSCOPE, "SCOPE External Trigger Diagnostic Test", "1.5", "", "V", ARB, "DSO-06-D01")
                If dMeasurement < 1.5 Then
                    TestScopeVT = ARB ' arb is bad
                Else
                    Echo(FormatResults(False, "SCOPE External Trigger Diagnostic Test", "DSO-06-D01"))
                    RegisterFailure(OSCOPE, "DSO-06-D01", , , , , sGuiLabel(OSCOPE) & " SCOPE External Trigger Diagnostic Test")
                    TestScopeVT = FAILED
                End If
                GoTo TestComplete
            Else
                TestScopeVT = FAILED
            End If
        End If
        Application.Doevents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "DSO" And OptionStep = LoopStepNo Then
            GoTo DSO6
        End If
        frmSTest.proProgress.Value = 80
        LoopStepNo += 1

DSO7:   'DSO-07-001
        '**DC Calibrator Test Procedure
        nSystErr = WriteMsg(OSCOPE, "*RST; *CLS") 'Reset the scope
        Delay(0.3)
        nSystErr = WriteMsg(OSCOPE, "CAL:SCAL:DOUT ZVOL") 'DC Calibrator Output to 0V
        nSystErr = WriteMsg(DMM, "*CLS;*RST")
        nSystErr = WriteMsg(DMM, "CONF:VOLT:DC")
        nSystErr = WriteMsg(DMM, "INIT;FETCH?")

        Measurement1 = FetchMeasurement(DMM, "SCOPE DC Calibrator Test 1", "-0.2", "0.2", "V", OSCOPE, "DSO-07-001")
        If Measurement1 > 0.2 Or Measurement1 < -0.2 Then
            TestScopeVT = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If '5
        Application.Doevents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "DSO" And OptionStep = LoopStepNo Then
            GoTo DSO7
        End If
        frmSTest.proProgress.Value = 90
        LoopStepNo += 1

        'DSO-08-001
DSO8:
        nSystErr = WriteMsg(OSCOPE, "*RST; *CLS") 'Reset the scope
        Delay(0.3)
        nSystErr = WriteMsg(OSCOPE, "CAL:SCAL:DOUT FVOL") 'DC Calibrator Output to 5V
        nSystErr = WriteMsg(DMM, "*CLS;*RST")
        nSystErr = WriteMsg(DMM, "CONF:VOLT:DC")
        nSystErr = WriteMsg(DMM, "INIT;FETCH?")
        Measurement2 = FetchMeasurement(DMM, "SCOPE DC Calibrator Test 2", "4", "5.5", "V", OSCOPE, "DSO-08-001")

        dMeasurement = Measurement2 - Measurement1
        If dMeasurement < 4 Or dMeasurement > 5.5 Then
            TestScopeVT = FAILED
            IncStepFailed()
            'DSO-08-D01
            If InstrumentStatus(DMM) = PASSED And RunningEndToEnd = True Then
                Echo(FormatResults(False, "SCOPE DC Calibrator Test 2", "DSO-08-D01"))
                RegisterFailure(OSCOPE, "DSO-08-D01", dMeasurement, "V", 4, 5.5, "SCOPE DC Calibrator Test 2")
                TestScopeVT = FAILED
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            End If
            'DSO-08-D02
            'Disconnect P18 Scope Probe Comp Cal from DMM-HI/LO
            nSystErr = WriteMsg(SWITCH1, "OPEN 1.1002,1003") 'open  S301-5,6 and S301-7,8 bb
            'Connect P18 Scope Probe Comp Cal to P15 Scope input 1
            nSystErr = WriteMsg(SWITCH1, "CLOSE 2.3100") 'close S405-1,2 bb

            nSystErr = WriteMsg(OSCOPE, "CAL:SCAL:DOUT ZVOL") 'DC Calibrator Output to 0Vs
            nSystErr = WriteMsg(OSCOPE, "*RST") 'Reset the scope
            nSystErr = WriteMsg(OSCOPE, "TIM:RANG 1E-3")
            nSystErr = WriteMsg(OSCOPE, "CHAN1:RANG 20")
            nSystErr = WriteMsg(OSCOPE, "CHAN1:COUP DC")
            nSystErr = WriteMsg(OSCOPE, "TIM:MODE AUTO")
            nSystErr = WriteMsg(OSCOPE, "DIG CHAN1")
            nSystErr = WriteMsg(OSCOPE, "MEAS:SOUR CHAN1")
            nSystErr = WriteMsg(OSCOPE, "MEAS:VDCR?")
            Measurement1 = FetchMeasurement(OSCOPE, "SCOPE DC Calibrator Test Diagnostic Test, 0V", "", "", "V", OSCOPE, "DSO-08-D02")

            'DSO-08-D03
            'Reset the scope
            nSystErr = WriteMsg(OSCOPE, "CAL:SCAL:DOUT FVOL") 'DC Calibrator Output to 5V
            nSystErr = WriteMsg(OSCOPE, "DIG CHAN1")
            nSystErr = WriteMsg(OSCOPE, "MEAS:SOUR CHAN1")
            nSystErr = WriteMsg(OSCOPE, "MEAS:VDCR?")
            Measurement2 = FetchMeasurement(OSCOPE, "SCOPE DC Calibrator Test Diagnostic Test, 5V", "", "", "Volts", OSCOPE, "DSO-08-D03")
            dMeasurement = Measurement2 - Measurement1
            If dMeasurement > 5.5 Or dMeasurement < 4 Then
                'DSO-08-D04
                Echo(FormatResults(False, "SCOPE DC Calibrator Diagnostic Test", "DSO-08-D04"))
                RegisterFailure(OSCOPE, "DSO-08-D04", dMeasurement, "V", 4, 5.5, "SCOPE DC Calibrator Diagnostic Test, 5V")
                TestScopeVT = FAILED
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else
                'DSO-08-D05
                Echo(FormatResults(False, "SCOPE DC Calibrator Diagnostic Test, 5V", "DSO-08-D05"))
                RegisterFailure(DMM, "DSO-08-D05", dMeasurement, "V", , , "SCOPE DC Calibrator Test Diagnostic Test, 5V")
                TestScopeVT = DMM
            End If
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If '5
        Application.Doevents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "DSO" And OptionStep = LoopStepNo Then
            GoTo DSO8
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

        If AbortTest = True Then
            If TestScopeVT = FAILED Then
                ReportFailure(OSCOPE)
            Else
                ReportUnknown(OSCOPE)
            End If
        ElseIf TestScopeVT = PASSED Then
            ReportPass(OSCOPE)
        ElseIf TestScopeVT = FAILED Then
            ReportFailure(OSCOPE)
        Else
            ReportUnknown(OSCOPE)
        End If

    End Function


End Module