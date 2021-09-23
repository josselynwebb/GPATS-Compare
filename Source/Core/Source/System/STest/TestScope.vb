'Option Strict Off
Option Explicit On

Public Module modScopeTest


   
    '**************************************************************
    '* Nomenclature   : ATS-ViperT SYSTEM SELF TEST               *
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
        'This test is for both HP1428A and ZTEC1428

        'RETURNS:
        '   PASSED if the SCOPE test passes or FAILED if a failure occurs
        Dim strSelfTest As String = ""
        Dim Measurement1 As Double
        Dim Measurement2 As Double
        
        Dim x As DialogResult
        Dim lStatus As Integer
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

        'bb
        If RunningEndToEnd = False And FirstPass = True Then
            x = MsgBox("Is cable W201 installed?", MsgBoxStyle.YesNo)
            If x <> DialogResult.Yes Then
                'Install cable W201
                S = "Remove all cables/adapters from the SAIF." & vbCrLf
                S &= "Connect cable W201 to SAIF as follows:" & vbCrLf
                S &= " 1. P1 to SAIF J1 connector." & vbCrLf
                S &= " 2. P10 to SAIF ARB OUTPUT." & vbCrLf
                S &= " 3. P12 to SAIF ARB MARKER OUT." & vbCrLf
                S &= " 4. P15 to SAIF SCOPE INPUT 1." & vbCrLf
                S &= " 5. P16 to SAIF SCOPE INPUT 2." & vbCrLf
                S &= " 6. P17 to SAIF SCOPE TRIG IN." & vbCrLf
                S &= " 7. P18 to SAIF SCOPE COMP CAL." & vbCrLf
                S &= " 8. P19 to SAIF C/T INPUT 1."
                DisplaySetup(S, "ST-W201-1.jpg", 8)
                If AbortTest = True Then GoTo TestComplete
            End If
        End If

        nSystErr = WriteMsg(SWITCH1, "RESET") 'open all switches

        'DSO-01-001

        TestScope = PASSED
        nSystErr = vxiClear(OSCOPE)
        nSystErr = vxiClear(DMM)
        nSystErr = vxiClear(ARB)
        nSystErr = vxiClear(COUNTER)
        LoopStepNo = 1

DSO_1:   '** Perform a SCOPE Power-up Self Test
        'DSO-01-001
        Echo("DSO-01-001 Oscilloscope Built-In Test")
        nSystErr = WriteMsg(OSCOPE, "*RST")
        nSystErr = WriteMsg(OSCOPE, "*CLS")
        nSystErr = WriteMsg(OSCOPE, "SUMM:PRES") 'Preset the scope
        nSystErr = WriteMsg(OSCOPE, "*TST?") 'Perform test all routine
        nSystErr = WaitForResponse(OSCOPE, 0.22) ' ZT1428 takes 2 seconds
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
        End If '1
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "DSO" And OptionStep = LoopStepNo Then
            GoTo DSO_1
        End If
        frmSTest.proProgress.Value = 50
        LoopStepNo += 1

        '** SCOPE Frequency Measurement Test
        'Generate 1 MHz test input signal
        nSystErr = WriteMsg(ARB, "*RST")
        nSystErr = WriteMsg(ARB, "*CLS")
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


        ' Connect P10 ARB OUTPUT to P15 SCOPE INPUT 1.
        nSystErr = WriteMsg(SWITCH1, "CLOSE 2.3003") 'close S404-1,5 bb
        ' Connect P12 ARB MARKER OUT to P17 SCOPE TRIG IN.
        nSystErr = WriteMsg(SWITCH1, "CLOSE 1.3000") 'close S401-1,2 bb
        ' Connect DMM INPUT (HI/LO) to SCOPE PROBE COMP/CAL/TRIG OUTPUT.
        nSystErr = WriteMsg(SWITCH1, "CLOSE 1.1002,1003") 'close S301-5,6, S301-7,8 bb
        Delay(1)

        'DSO-02-001
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

        dMeasurement = FetchMeasurement(OSCOPE, "SCOPE Frequency Measurement Test, Channel 1", "90E+4", "110E+4", "Hz", OSCOPE, "DSO-02-001")
        If dMeasurement < 900000 Or dMeasurement > 1100000 Then
            'DSO-02-D01
            TestScope = FAILED
            IncStepFailed()

            If (OptionFaultMode = SOF_BUTTON) And (OptionMode <> LOSmode) Then
                dFH_Meas = dMeasurement
                'disconnect P10 Arb output from P15 SCOPE INPUT 1"
                nSystErr = WriteMsg(SWITCH1, "OPEN 2.3003") 'open  S404-1,5 bb
                'Connect P10 Arb output to P19 C/T INPUT 1."
                nSystErr = WriteMsg(SWITCH1, "CLOSE 2.3001") 'close S404-1,3 bb
                nSystErr = WriteMsg(COUNTER, "*RST")
                nSystErr = WriteMsg(COUNTER, "*CLS")
                nSystErr = WriteMsg(COUNTER, "INP1:IMP 50")
                nSystErr = WriteMsg(COUNTER, "INP2:IMP 50")
                nSystErr = WriteMsg(COUNTER, "EVEN:LEV:AUTO ON") 'Place counter in auto-triggering mode
                nSystErr = WriteMsg(COUNTER, "MEAS1:FREQ?")
                dMeasurement = FetchMeasurement(COUNTER, "SCOPE Frequency Measurement Diagnostic Test", "90E+4", "110E+4", "Hz", OSCOPE, "DSO-02-D01")
                If dMeasurement > 1100000 Or dMeasurement < 900000 Then
                    Echo(FormatResults(False, "DSO Freq Measurement Test Diagnostic", "DSO-02-D01"))
                    RegisterFailure(ARB, "DSO-02-D01", dFH_Meas, "Hz", 900000, 1100000, "SCOPE Frequency Measurement Diagnostic Test")
                    TestScope = ARB
                Else
                    Echo(FormatResults(False, "DSO Freq Measurement Test Diagnostic", "DSO-02-D01"))
                    RegisterFailure(OSCOPE, "DSO-02-D01", dFH_Meas, "Hz", 900000, 1100000, "SCOPE Frequency Measurement Diagnostic Test")
                End If
                IncStepFailed()
                If OptionFaultMode = SOFmode Then GoTo TestComplete
                'disconnect P10 Arb output from P19 C/T INPUT 1."
                nSystErr = WriteMsg(SWITCH1, "OPEN 2.3001") 'open S404-1,3 bb
                'reconnect P10 Arb output to P15 SCOPE INPUT 1"
                nSystErr = WriteMsg(SWITCH1, "CLOSE 2.3003") 'close  S404-1,5 bb
            End If
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
        nSystErr = WriteMsg(OSCOPE, "CHAN2:COUP DCFIFTY")
        nSystErr = WriteMsg(OSCOPE, "VIEW CHAN1") 'TURN ON CHAN1
        nSystErr = WriteMsg(OSCOPE, "CHAN1:RANG 10V")
        nSystErr = WriteMsg(OSCOPE, "TRIG:LEV 0V")
        nSystErr = WriteMsg(OSCOPE, "TIM:RANG 4000NS")
        nSystErr = WriteMsg(OSCOPE, "DIG CHAN1")
        nSystErr = WriteMsg(OSCOPE, "MEAS:SOUR CHAN1")
        nSystErr = WriteMsg(OSCOPE, "MEAS:VPP?")
        Delay(0.2)

        dMeasurement = FetchMeasurement(OSCOPE, "SCOPE Voltage Measurement Test, Channel 1", "1.0", "2.5", "VPP", OSCOPE, "DSO-03-001")
        If dMeasurement < 1 Or dMeasurement > 2.5 Then
            TestScope = FAILED
            'DSO-03-D01
            dFH_Meas = dMeasurement
            Echo("    Probable cause of failure - Scope Channel 1 50 Ohm input circuit.")
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

DSO_4:  '** SCOPE Autoscale Period Measurement Test
        ' Signal connected to Input channel 2
        'Disconnect P10 ARB output from P15 SCOPE INPUT 1.
        nSystErr = WriteMsg(SWITCH1, "OPEN 2.3003") 'open  S404-1,5 bb
        'Connect P10 ARB output to P16 SCOPE INPUT 2.
        nSystErr = WriteMsg(SWITCH1, "CLOSE 2.3000") 'close S404-1,2 bb
        Delay(1)

        nSystErr = WriteMsg(OSCOPE, "*RST")
        nSystErr = WriteMsg(OSCOPE, "*CLS")
        Delay(1)
        '    WriteMsg OSCOPE, "AUT;"
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
        End If '3
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "DSO" And OptionStep = LoopStepNo Then
            GoTo DSO_4
        End If
        frmSTest.proProgress.Value = 70
        LoopStepNo += 1
        'DSO-05-001
DSO_5:
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
        Delay(0.2)

        dMeasurement = FetchMeasurement(OSCOPE, "SCOPE Voltage Measurement Test, Channel 2", "1.0", "2.5", "VPP", OSCOPE, "DSO-05-001")
        If dMeasurement < 1 Or dMeasurement > 2.5 Then
            dFH_Meas = dMeasurement
            Echo("    Probable cause of failure - Scope Channel 2 50 Ohm input circuit.")
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

        '** Ext. Trigger Test: Capture the signal at Channel 2 and measure the period at Ext. Trigger.
DSO_6:  'Generate marker pulses
        sTNum = "DSO-06-001"
        nSystErr = WriteMsg(ARB, "MARKER:POL NORM")
        nSystErr = WriteMsg(ARB, "MARK:STAT ON") 'Enable the AFG to output a marker list


        nSystErr = WriteMsg(OSCOPE, "*RST")
        nSystErr = WriteMsg(OSCOPE, "*CLS")
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
        Delay(0.2)
        lStatus = ReadMsg(OSCOPE, sMsg)
        nSystErr = WriteMsg(OSCOPE, "STOP") ' need this after run

        If Val(sMsg) = 1 Then
            Echo(FormatResults(True, "SCOPE External Trigger Test", sTNum))
            IncStepPassed()
        Else
            Echo(FormatResults(False, "SCOPE External Trigger Test", sTNum))
            IncStepFailed()
            TestScope = FAILED

            'DSO-06-D01
            If (OptionFaultMode = SOF_BUTTON) And (OptionMode <> LOSmode) Then ' do diagnostics
                'Disconnect P12 ARB Marker Out from P17 Scope Trig In
                nSystErr = WriteMsg(SWITCH1, "OPEN 1.3000") 'open  S401-1,2 bb
                'Connect P12 ARB Marker Out to P15 SCOPE INPUT 1.
                nSystErr = WriteMsg(SWITCH1, "CLOSE 2.3103") 'close S405-1,5 bb

                nSystErr = vxiClear(OSCOPE)
                nSystErr = WriteMsg(OSCOPE, "*RST")
                nSystErr = WriteMsg(OSCOPE, "*CLS")
                nSystErr = WriteMsg(OSCOPE, "AUT") 'Set Autoscale mode
                Delay(2)
                nSystErr = WriteMsg(OSCOPE, "DIG CHAN1")
                nSystErr = WriteMsg(OSCOPE, "MEAS:SOUR CHAN1")
                nSystErr = WriteMsg(OSCOPE, "MEAS:VAMP?")
                dMeasurement = FetchMeasurement(OSCOPE, "SCOPE External Trigger Diagnostic Test", "1.5", "", "V", ARB, "DSO-06-D01")
                If dMeasurement < 1.5 Then
                    TestScope = ARB ' arb is bad
                Else
                    Echo(FormatResults(False, "SCOPE External Trigger Diagnostic Test", "DSO-06-D01"))
                    RegisterFailure(OSCOPE, "DSO-06-D01", , , , , sGuiLabel(OSCOPE) & " SCOPE External Trigger Diagnostic Test")
                End If
                'disConnect P12 ARB Marker Out from P15 SCOPE INPUT 1.
                nSystErr = WriteMsg(SWITCH1, "OPEN 2.3103") 'open S405-1,5 bb
                'reconnect P12 ARB Marker Out to P17 Scope Trig In
                nSystErr = WriteMsg(SWITCH1, "CLOSE 1.3000") 'close  S401-1,2 bb

            End If
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
            End If
            Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete
            If OptionMode = LOSmode And OptionTestName = "DSO" And OptionStep = LoopStepNo Then
                GoTo DSO_6
            End If
            frmSTest.proProgress.Value = 90
            LoopStepNo += 1

            'DSO-07-001
DSO_7:      '**DC Calibrator Test Procedure
            nSystErr = WriteMsg(OSCOPE, "*RST") 'Reset the scope
            nSystErr = WriteMsg(OSCOPE, "*CLS")
            Delay(0.3)
            nSystErr = WriteMsg(OSCOPE, "CAL:SCAL:DOUT ZVOL") 'DC Calibrator Output to 0V
            nSystErr = WriteMsg(DMM, "*RST")
            nSystErr = WriteMsg(DMM, "*CLS")
            nSystErr = WriteMsg(DMM, "CONF:VOLT:DC")
            nSystErr = WriteMsg(DMM, "INIT;FETCH?")
            Delay(0.2)

            Measurement1 = FetchMeasurement(DMM, "SCOPE DC Calibrator Test 1", "-0.2", "0.2", "V", OSCOPE, "DSO-07-001")
            If Measurement1 > 0.2 Or Measurement1 < -0.2 Then
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

DSO_8:      'DSO-08-001
            nSystErr = WriteMsg(OSCOPE, "*RST") 'Reset the scope
            nSystErr = WriteMsg(OSCOPE, "*CLS")
            Delay(0.3)
            nSystErr = WriteMsg(OSCOPE, "CAL:SCAL:DOUT FVOL") 'DC Calibrator Output to 5V
            nSystErr = WriteMsg(DMM, "*RST")
            nSystErr = WriteMsg(DMM, "*CLS")
            nSystErr = WriteMsg(DMM, "CONF:VOLT:DC")
            nSystErr = WriteMsg(DMM, "INIT;FETCH?")
            Measurement2 = FetchMeasurement(DMM, "SCOPE DC Calibrator Test 2", "4", "5.5", "V", OSCOPE, "DSO-08-001")
            Delay(0.2)

            dMeasurement = Measurement2 - Measurement1
            If dMeasurement < 4 Or dMeasurement > 5.5 Then
                TestScope = FAILED
                IncStepFailed()
                If (OptionFaultMode = SOFmode) And (OptionMode <> LOSmode) Then
                    'DSO-08-D01
                    If InstrumentStatus(DMM) = PASSED And RunningEndToEnd = True Then
                        Echo(FormatResults(False, "SCOPE DC Calibrator Test 2", "DSO-08-D01"))
                        RegisterFailure(OSCOPE, "DSO-08-D01", dMeasurement, "V", 4, 5.5, "SCOPE DC Calibrator Test 2")
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
                        If OptionFaultMode = SOFmode Then GoTo TestComplete
                    Else
                        'DSO-08-D05
                        Echo(FormatResults(False, "SCOPE DC Calibrator Diagnostic Test, 5V", "DSO-08-D05"))
                        RegisterFailure(DMM, "DSO-08-D05", dMeasurement, "V", , , "SCOPE DC Calibrator Test Diagnostic Test, 5V")
                        TestScope = DMM
                    End If
                    If OptionFaultMode = SOFmode Then GoTo TestComplete
                'disConnect P18 Scope Probe Comp Cal from P15 Scope input 1
                nSystErr = WriteMsg(SWITCH1, "OPEN 2.3100") 'close S405-1,2 bb
                'reconnect P18 Scope Probe Comp Cal to DMM-HI/LO
                nSystErr = WriteMsg(SWITCH1, "CLOSE 1.1002,1003") 'open  S301-5,6 and S301-7,8 bb
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
                    TestScope = -99
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