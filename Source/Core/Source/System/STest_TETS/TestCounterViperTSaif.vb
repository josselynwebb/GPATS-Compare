'Option Strict Off
Option Explicit On

Imports System.Windows.Forms
Imports System.Math

Public Module modCounterViperTSaif


    '=========================================================
    '**************************************************************
    '* Nomenclature   : TETS SYSTEM SELF TEST                     *
    '*                  E1420B Counter Test                       *
    '* Version        : 2.0                                       *
    '* Last Update    : Apr 1, 2017                               *
    '* Purpose        : This module is the C/T self test          *
    '**************************************************************


    Public Function TestCounterVT() As Integer
        'DESCRIPTION:
        '   This routine tests Counter and returns PASSED or FAILED
        'RETURNS:
        '   PASSED if the COUNTER test passes or FAILED if a failure occurs

        Dim x As DialogResult
        Dim S As String
        Dim SSelfTest As String = ""
        Dim SelfTest As Integer
        Dim Inc As Single
        Dim Lev As Single
        Dim Lev1 As Single
        Dim Lev2 As Single
        Dim ErrorMsg As Integer
        Dim StrErrorMsg As String = ""
        Dim StrErrorMsg1 As String = ""
        Dim ErrorMsg1 As Integer
        Dim StrErrorMsg2 As String = ""
        Dim ErrorMsg2 As Integer
        Dim sMessage As String 'Describes Failure
        HelpContextID = 1200

        'Counter/Timer Test Title block
        EchoTitle(InstrumentDescription(COUNTER) & " ", True)
        EchoTitle("Counter/Timer Test", False)
        frmSTest.proProgress.Maximum = 100

        TestCounterVT = UNTESTED
        If AbortTest = True Then GoTo TestComplete
        AbortTest = False
        TestCounterVT = PASSED

        nSystErr = vxiClear(COUNTER)
        nSystErr = WriteMsg(COUNTER, "*CLS")
        nSystErr = WriteMsg(COUNTER, "*RST")
        nSystErr = vxiClear(ARB)
        nSystErr = WriteMsg(COUNTER, "SENS:ROSC:SOUR INT")
        nSystErr = WriteMsg(ARB, "*RST;*CLS")
        nSystErr = vxiClear(OSCOPE)
        nSystErr = WriteMsg(OSCOPE, "*RST;*CLS")
        nSystErr = WriteMsg(OSCOPE, "SYST:LANG COMP") 'Set Autoscale mode?

        nSystErr = WriteMsg(COUNTER, "SENS:ROSC:SOUR INT")

        If RunningEndToEnd = False And FirstPass = True Then
            x = MsgBox("Is the W201 cable installed?", MsgBoxStyle.YesNo)
            If x <> DialogResult.Yes Then
                'Install the W201 cable
                S = "Remove all cables/adapters from the SAIF." & vbCrLf
                S += "Install the W201 cable to the SAIF as follows:" & vbCrLf
                S += " 1. P1  to SAIF J1 connector." & vbCrLf
                S += " 2. P10 to SAIF ARB OUTPUT." & vbCrLf
                S += " 3. P12 to SAIF ARB MARKER OUT." & vbCrLf
                S += " 4. P15 to SAIF SCOPE INPUT 1." & vbCrLf
                S += " 5. P19 to SAIF C/T INPUT 1." & vbCrLf
                S += " 6. P20 to SAIF C/T INPUT 2." & vbCrLf
                S += " 7. P21 to SAIF C/T ARM/IN."
                DisplaySetup(S, "ST-W201-1.jpg", 7)
                If AbortTest = True Then GoTo TestComplete
            End If
        End If


        'C/T-01-001
CT1:
        '** Perform a COUNTER Power-up Built-In Test
        Echo("C/T-01-001 Counter Built-In Test. . .")
        nSystErr = WriteMsg(COUNTER, "*TST?")
        WaitForResponse(COUNTER, 0.1)
        SelfTest = ReadMsg(COUNTER, SSelfTest)

        If SelfTest <> 0 Or Val(SSelfTest) Then
            TestCounterVT = FAILED
            IncStepFailed()
            Echo(FormatResults(False, "Counter Built-In Test " & StripNullCharacters(SSelfTest)))
            RegisterFailure(COUNTER, "C/T-01-001", , , , , sGuiLabel(COUNTER) & " FAILED Built-in Test")
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
            Echo(FormatResults(True, "Counter Built-In Test")) '1
        End If
        frmSTest.proProgress.Value = 4
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "C/T" And OptionStep = 1 Then
            GoTo CT1
        End If

        'C/T-02-001

        '**************************************Input Channel 1 Test***************************************************
        'Assume all signals are connected to Counter Input 1 unless otherwise specified.
        'The measurement tolerance assumed to be +/- 5%

CT2:
        'Generate 1 MHz sine wave with 1VPP
        nSystErr = WriteMsg(ARB, "*RST;*CLS")
        nSystErr = WriteMsg(ARB, "VOLT:UNIT:VOLT VPP")
        nSystErr = WriteMsg(ARB, "FREQ 1E+6")
        nSystErr = WriteMsg(ARB, "FUNC SIN")
        nSystErr = WriteMsg(ARB, "OUTP:FILT:FREQ 10E6")
        nSystErr = WriteMsg(ARB, "OUTP:FILT ON")

        nSystErr = WriteMsg(SWITCH1, "CLOSE 2.3001") 'close S404-1,3  Connect P10 ARB OUTPUT to P19 C/T INPUT 1
        nSystErr = WriteMsg(SWITCH1, "CLOSE 1.3001") 'close S401-1,3  Connect P12 ARB MARKER OUT to P21 C/T ARM IN

        '** Auto Frequency Measurement Test: checks the auto-triggering capability and frequency interpolation process
        'Offset of +2Vdc to ARB
        nSystErr = WriteMsg(ARB, "VOLT 3")
        nSystErr = WriteMsg(ARB, "VOLT:OFFS 2")
        nSystErr = WriteMsg(ARB, "INIT")
        Delay(1)

        'Connect a 10 MHz timebase standard to the Int/Ext ref. BNC.
        nSystErr = vxiClear(COUNTER)

        nSystErr = WriteMsg(COUNTER, "*RST;*CLS")
        nSystErr = WriteMsg(COUNTER, "INP1:IMP 50")
        nSystErr = WriteMsg(COUNTER, "INP2:IMP 50")
        nSystErr = WriteMsg(COUNTER, "SENS:ROSC:SOUR INT")
        nSystErr = WriteMsg(COUNTER, "ATIM:TIME 60")
        nSystErr = WriteMsg(COUNTER, "ATIM OFF")
        nSystErr = WriteMsg(COUNTER, "EVEN:LEV:AUTO ON") 'Place counter in auto-trigger mode
        nSystErr = WriteMsg(COUNTER, "CONF1:FREQ AUTO") 'Measure frequency input 1
        nSystErr = WriteMsg(COUNTER, "INIT1; FETCH1?")

        dMeasurement = FetchMeasurement(COUNTER, "Counter Auto Frequency Measurement Test", ".95E+6", "1.05E+6", "Hz", COUNTER, "C/T-02-001")
        If dMeasurement < 950000 Or dMeasurement > 1050000 Then
            'C/T-02-D01
            IncStepFailed()
            dFH_Meas = dMeasurement
            'Disconnect P10 Arb Output from P19 C/T Input 1
            nSystErr = WriteMsg(SWITCH1, "OPEN 2.3001") 'open  S404-1,3  bb
            'Connect P10 Arb Output to P15 Scope Input 1
            nSystErr = WriteMsg(SWITCH1, "CLOSE 2.3003") 'close S404-1,5  bb

            nSystErr = WriteMsg(OSCOPE, "*RST; *CLS")
            nSystErr = WriteMsg(OSCOPE, "AUT") 'Set Autoscale mode
            Delay(2)
            nSystErr = WriteMsg(OSCOPE, "DIG CHAN1")
            nSystErr = WriteMsg(OSCOPE, "MEAS:SOUR CHAN1")
            nSystErr = WriteMsg(OSCOPE, "MEAS:FREQ?")
            dMeasurement = FetchMeasurement(OSCOPE, "Counter Auto Frequency Measurement Diagnostic Test", ".90E+6", "1.10E+6", "Hz", COUNTER, "C/T-02-D01")
            If dMeasurement > 1050000 Or dMeasurement < 950000 Then
                Echo(FormatResults(False, "Counter Auto Frequency Measurement Diagnostic Test", "C/T-02-D01"))
                RegisterFailure(ARB, "C/T-02-D01", dFH_Meas, "Hz", 950000, 1050000, "Counter Auto Frequency Measurement Diagnostic Test")
                TestCounterVT = ARB
            Else
                TestCounterVT = FAILED
                Echo(FormatResults(False, "Counter Auto Frequency Measurement Diagnostic Test", "C/T-02-D01"))
                RegisterFailure(COUNTER, "C/T-02-D01", dFH_Meas, "Hz", 950000, 1050000, "Counter Auto Frequency Measurement Diagnostic Test")
                Echo("    Auto Frequency Measurement Failed.  Probable cause is from the auto-triggering circuitry.")
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            End If
            'Disconnect P10 Arb Output from P15 Scope Input 1
            nSystErr = WriteMsg(SWITCH1, "OPEN 2.3003") 'close S404-1,5  bb
            'Reconnect P10 Arb Output to P19 C/T Input 1
            nSystErr = WriteMsg(SWITCH1, "CLOSE 2.3001") 'open  S404-1,3  bb
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        frmSTest.proProgress.Value = 8
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "C/T" And OptionStep = 2 Then
            GoTo CT2
        End If

        'C/T-03-001
CT3:
        '** Input Signal Conditioning Test:  A Functional test of the input amplifier relays and circuitry associated with those relays
        nSystErr = WriteMsg(ARB, "ABORT")
        nSystErr = WriteMsg(ARB, "VOLT 3")
        nSystErr = WriteMsg(ARB, "VOLT:OFFS 0") 'Change the previous offset from 2V to 0V
        nSystErr = WriteMsg(ARB, "FREQ 1E+6") ' Output a 3Vpp sine wave
        nSystErr = WriteMsg(ARB, "INIT")
        Delay(1)
        nSystErr = vxiClear(COUNTER)

        nSystErr = WriteMsg(COUNTER, "*RST;*CLS") : Delay(0.5)
        nSystErr = WriteMsg(COUNTER, "INP1:IMP 50")
        nSystErr = WriteMsg(COUNTER, "INP2:IMP 50")
        nSystErr = WriteMsg(COUNTER, "SENS:ROSC:SOUR INT")
        nSystErr = WriteMsg(COUNTER, "INP:ROUT COMM") 'Common Input Mode

        'The following loop increases the trigger levels of Input 1 and Input 2 until the trigger lights just go off.
        'No trigger causes a timeout error, which ends the do loop
        Inc = 0.1 'Increase trigger level by 0.1V
        lev = 0.8 'Initial trigger level  (initial level was 0.8)

        Echo("C/T-03-001 Trigger Level Test, Channel 1")
        Do
            lev += Inc
            nSystErr = WriteMsg(COUNTER, "SENS1:EVEN:LEV" + Str(lev) + "V")
            nSystErr = WriteMsg(COUNTER, "MEAS1:FREQ?")
            Delay(1)
            ErrorMsg = ReadMsg(COUNTER, StrErrorMsg)
            If Val(StrErrorMsg) = 0 Or ErrorMsg <> 0 Then Exit Do
            Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete
        Loop Until lev = 2

        If (lev < 1.2) Or (lev > 1.8) Then
            RegisterFailure(COUNTER, "C/T-03-001", Str(lev), "", 1.2, 1.8, sGuiLabel(COUNTER) & " FAILED Trigger Level Test, Channel 1")
            Echo(FormatResults(False, , , "1.2", "1.8", CStr(lev), "V"))
            TestCounterVT = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            Echo(FormatResults(True, , , "1.2", "1.8", CStr(lev), "V"))
            IncStepPassed()
        End If
        frmSTest.proProgress.Value = 12
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "C/T" And OptionStep = 3 Then
            GoTo CT3
        End If

        'C/T-04-001
CT4:
        'Channel 2
        nSystErr = vxiClear(COUNTER)

        nSystErr = WriteMsg(COUNTER, "*RST;*CLS") : Delay(0.5)
        nSystErr = WriteMsg(COUNTER, "INP1:IMP 50")
        nSystErr = WriteMsg(COUNTER, "INP2:IMP 50")
        nSystErr = WriteMsg(COUNTER, "SENS:ROSC:SOUR INT")
        nSystErr = WriteMsg(COUNTER, "INP:ROUT COMM") ' this routes Input channel 1 to both channels
        Delay(1)
        lev = 0.5
        ErrorMsg = 0
        Echo("C/T-04-001 Trigger Level Test, Channel 2")
        Do
            lev += Inc
            nSystErr = WriteMsg(COUNTER, "SENS2:EVEN:LEV" + Str(lev))
            nSystErr = WriteMsg(COUNTER, "MEAS2:FREQ?")
            Delay(1)
            ErrorMsg = ReadMsg(COUNTER, StrErrorMsg)
            If Val(StrErrorMsg) = 0 Or ErrorMsg <> 0 Then Exit Do
            Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete
        Loop Until lev = 2

        If (lev < 1.2) Or (lev > 1.8) Then
            RegisterFailure(COUNTER, "C/T-04-001", Str(lev), "", 1.2, 1.8, sGuiLabel(COUNTER) & " FAILED Trigger Level Test, Channel 2")
            Echo(FormatResults(False, , , "1.2", "1.8", CStr(lev), "V"))
            TestCounterVT = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            Echo(FormatResults(True, , , "1.2", "1.8", CStr(lev), "V"))
            IncStepPassed()
        End If
        frmSTest.proProgress.Value = 16
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "C/T" And OptionStep = 4 Then
            GoTo CT4
        End If

        'C/T-05-001
CT5:
        nSystErr = WriteMsg(ARB, "ABORT")
        nSystErr = WriteMsg(ARB, "FREQ 1E+6")
        nSystErr = WriteMsg(ARB, "INIT")
        'Inputs 1,2:  1 Mohm impedance applied
        nSystErr = vxiClear(COUNTER)
        nSystErr = WriteMsg(COUNTER, "*RST;*CLS")
        nSystErr = WriteMsg(COUNTER, "SENS:ROSC:SOUR INT")
        nSystErr = WriteMsg(COUNTER, "SENS1:EVEN:LEV 2")
        nSystErr = WriteMsg(COUNTER, "SENS2:EVEN:LEV 2")
        nSystErr = WriteMsg(COUNTER, "INP1:IMP 1E6")
        nSystErr = WriteMsg(COUNTER, "INP2:IMP 1E6")

        nSystErr = WriteMsg(COUNTER, "INP:ROUT SEP") 'Common input mode OFF
        Delay(1)

        nSystErr = WriteMsg(COUNTER, "MEAS1:FREQ?")
        ErrorMsg1 = ReadMsg(COUNTER, StrErrorMsg1)
        nSystErr = WriteMsg(COUNTER, "MEAS2:FREQ?")
        ErrorMsg2 = ReadMsg(COUNTER, StrErrorMsg2)

        If ErrorMsg2 = 0 Or ErrorMsg1 <> 0 Then
            TestCounterVT = FAILED
            Echo(FormatResults(False, "C/T Separate Routing Test 1", "C/T-05-001"))
            RegisterFailure(COUNTER, "C/T-05-001", , , , , "Separate Routing Test 1")
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            Echo(FormatResults(True, "Separate Routing Test 1", "C/T-05-001"))
            IncStepPassed()
        End If
        frmSTest.proProgress.Value = 20
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "C/T" And OptionStep = 5 Then
            GoTo CT5
        End If

        'C/T-06-001
CT6:
        nSystErr = vxiClear(COUNTER)
        nSystErr = WriteMsg(COUNTER, "*RST;*CLS")
        nSystErr = WriteMsg(COUNTER, "SENS:ROSC:SOUR INT")
        nSystErr = WriteMsg(COUNTER, "SENS1:EVEN:LEV 2")
        nSystErr = WriteMsg(COUNTER, "SENS2:EVEN:LEV 2")
        nSystErr = WriteMsg(COUNTER, "INP:ROUT COMM")
        nSystErr = WriteMsg(COUNTER, "INP1:IMP 1E6")
        nSystErr = WriteMsg(COUNTER, "INP2:IMP 1E6")

        Delay(1)
        nSystErr = WriteMsg(COUNTER, "MEAS1:FREQ?")
        ErrorMsg1 = ReadMsg(COUNTER, StrErrorMsg1)
        nSystErr = WriteMsg(COUNTER, "MEAS2:FREQ?")
        ErrorMsg2 = ReadMsg(COUNTER, StrErrorMsg2)

        If ErrorMsg1 <> 0 Or ErrorMsg2 <> 0 Then
            TestCounterVT = FAILED
            Echo(FormatResults(False, "C/T Separate Routing Test 2", "C/T-06-001"))
            RegisterFailure(COUNTER, "C/T-06-001", , , , , "Separate Routing Test 2")
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            Echo(FormatResults(True, "Separate Routing Test 2", "C/T-06-001"))
            IncStepPassed()
        End If
        frmSTest.proProgress.Value = 24
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "C/T" And OptionStep = 6 Then
            GoTo CT6
        End If

        'C/T-07-001
CT7:
        'Change both input impedance levels to 50 ohms
        nSystErr = WriteMsg(COUNTER, "INP1:IMP 50")
        nSystErr = WriteMsg(COUNTER, "MEAS1:FREQ?")
        ErrorMsg1 = ReadMsg(COUNTER, StrErrorMsg1)

        nSystErr = vxiClear(COUNTER)
        nSystErr = WriteMsg(COUNTER, "*RST;*CLS")
        nSystErr = WriteMsg(COUNTER, "INP1:IMP 50")
        nSystErr = WriteMsg(COUNTER, "INP2:IMP 50")
        nSystErr = WriteMsg(COUNTER, "SENS:ROSC:SOUR INT")
        nSystErr = WriteMsg(COUNTER, "SENS2:EVEN:LEV 2")
        nSystErr = WriteMsg(COUNTER, "INP:ROUT COMM")
        Delay(1)
        nSystErr = WriteMsg(COUNTER, "MEAS2:FREQ?")
        ErrorMsg2 = ReadMsg(COUNTER, StrErrorMsg2)


        If ErrorMsg1 = 0 Or ErrorMsg2 = 0 Then
            TestCounterVT = FAILED
            RegisterFailure(COUNTER, "C/T-07-001", , , , , "Separate Routing Test 3")
            Echo(FormatResults(False, "Separate Routing Test 3", "C/T-07-001"))
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            Echo(FormatResults(True, "Separate Routing Test 3", "C/T-07-001"))
            IncStepPassed()
        End If
        frmSTest.proProgress.Value = 28
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "C/T" And OptionStep = 7 Then
            GoTo CT7
        End If

        'C/T-08-001
CT8:
        nSystErr = vxiClear(COUNTER)
        nSystErr = WriteMsg(COUNTER, "*RST;*CLS")
        nSystErr = WriteMsg(COUNTER, "INP1:IMP 50")
        nSystErr = WriteMsg(COUNTER, "INP2:IMP 50")
        nSystErr = WriteMsg(COUNTER, "SENS:ROSC:SOUR INT")
        nSystErr = WriteMsg(COUNTER, "INP1:COUP AC")
        nSystErr = WriteMsg(COUNTER, "INP1:ATT 10") 'Turn on the x10 attenuator

        nSystErr = WriteMsg(ARB, "ABORT")
        nSystErr = WriteMsg(ARB, "VOLT:UNIT:VOLT VRMS")
        nSystErr = WriteMsg(ARB, "VOLT 3")
        nSystErr = WriteMsg(ARB, "INIT")

        'Measure AC rms voltage of the input signal through the x10 attenuators
        Delay(0.5)
        nSystErr = WriteMsg(COUNTER, "MEAS1:AC?")
        Delay(0.5)

        dMeasurement = FetchMeasurement(COUNTER, "Attenuated AC RMS Voltage Measurement Test", "200E-3", "400E-3", "Volts", COUNTER, "C/T-08-001")
        If dMeasurement > 0.4 Or dMeasurement < 0.2 Then
            TestCounterVT = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        frmSTest.proProgress.Value = 32
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "C/T" And OptionStep = 8 Then
            GoTo CT8
        End If

        'C/T-09-001
CT9:
        nSystErr = WriteMsg(COUNTER, "INP1:ATT 1")
        nSystErr = WriteMsg(COUNTER, "INP2:ATT 1")
        nSystErr = WriteMsg(COUNTER, "MEAS1:AC?")
        Delay(0.3)

        dMeasurement = FetchMeasurement(COUNTER, "AC RMS Voltage Measurement Test", "2", "4", "Volts", COUNTER, "C/T-09-001")
        If dMeasurement > 4 Or dMeasurement < 2 Then
            TestCounterVT = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        frmSTest.proProgress.Value = 36
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "C/T" And OptionStep = 9 Then
            GoTo CT9
        End If

        'C/T-10-001
CT10:
        'Reset Counter
        nSystErr = vxiClear(COUNTER)
        nSystErr = WriteMsg(COUNTER, "*RST;*CLS")
        nSystErr = WriteMsg(COUNTER, "INP1:IMP 50")
        nSystErr = WriteMsg(COUNTER, "INP2:IMP 50")
        nSystErr = WriteMsg(COUNTER, "SENS:ROSC:SOUR INT")
        nSystErr = WriteMsg(COUNTER, "INP:ROUT COMM") 'Common input mode ON
        nSystErr = WriteMsg(COUNTER, "MEAS1:FREQ?")

        dMeasurement = FetchMeasurement(COUNTER, "AC/DC Relay Verification Test Channel 1", ".95E+6", "1.05E+6", "Hz", COUNTER, "C/T-10-001")
        If dMeasurement > 1050000 Or dMeasurement < 950000 Then
            TestCounterVT = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        frmSTest.proProgress.Value = 40
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "C/T" And OptionStep = 10 Then
            GoTo CT10
        End If

        'C/T-11-001
CT11:
        nSystErr = WriteMsg(COUNTER, "MEAS2:FREQ?")

        dMeasurement = FetchMeasurement(COUNTER, "AC/DC Relay Verification Test Channel 2", ".95E+6", "1.05E+6", "Hz", COUNTER, "C/T-11-001")
        If dMeasurement > 1050000 Or dMeasurement < 950000 Then
            TestCounterVT = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        frmSTest.proProgress.Value = 42
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "C/T" And OptionStep = 11 Then
            GoTo CT11
        End If

        'C/T-12-001 DC Offset Level Test, Channel 1
CT12:
        nSystErr = vxiClear(COUNTER)
        nSystErr = WriteMsg(COUNTER, "*RST;*CLS")
        nSystErr = WriteMsg(COUNTER, "INP1:IMP 50")
        nSystErr = WriteMsg(COUNTER, "INP2:IMP 50")
        nSystErr = WriteMsg(COUNTER, "SENS:ROSC:SOUR INT")
        nSystErr = WriteMsg(COUNTER, "INP:ROUT COMM")
        nSystErr = WriteMsg(COUNTER, "SENS1:EVEN:LEV -3")
        nSystErr = WriteMsg(COUNTER, "SENS2:EVEN:LEV -3")

        'Generate 1 MHz Square wave with no overshoot
        nSystErr = WriteMsg(ARB, "*RST;*CLS")
        nSystErr = WriteMsg(ARB, "FUNC SQU")
        nSystErr = WriteMsg(ARB, "FREQ 1MHZ")
        nSystErr = WriteMsg(ARB, "OUTP:FILT:FREQ 10E6")
        nSystErr = WriteMsg(ARB, "OUTP:FILT ON")
        nSystErr = WriteMsg(ARB, "VOLT:UNIT Vpk")
        nSystErr = WriteMsg(ARB, "VOLT 3.5")
        nSystErr = WriteMsg(ARB, "INIT")

        'The following loop decreases the signal level until Input 1 and Input 2 both stop triggering.
        'Step the level down until BOTH inputs fail to trigger
        ErrorMsg1 = 0
        ErrorMsg2 = 0
        For lev = 3.5 To 2 Step -0.05
            nSystErr = WriteMsg(ARB, "VOLT " & CStr(lev))
            Application.DoEvents()
            If ErrorMsg1 = 0 Then 'If still triggering
                Lev1 = -lev 'Record Vpk-neg for Input 1
                nSystErr = WriteMsg(COUNTER, "MEAS1:FREQ?")
                ErrorMsg1 = ReadMsg(COUNTER, StrErrorMsg)
                If ErrorMsg1 <> 0 Then 'If it did NOT trigger this time
                    nSystErr = vxiClear(COUNTER)
                    nSystErr = WriteMsg(COUNTER, "*CLS") 'Clear errors
                End If
            End If
            If ErrorMsg2 = 0 Then 'If still triggering
                Lev2 = -lev 'Record Vpk-neg for Input 2
                nSystErr = WriteMsg(COUNTER, "MEAS2:FREQ?")
                ErrorMsg2 = ReadMsg(COUNTER, StrErrorMsg)
                If ErrorMsg2 <> 0 Then 'If it did NOT trigger this time
                    nSystErr = vxiClear(COUNTER)
                    nSystErr = WriteMsg(COUNTER, "*CLS") 'Clear errors
                End If
            End If
            If ErrorMsg1 <> 0 And ErrorMsg2 <> 0 Then Exit For
        Next lev


        Echo("C/T-12-001 DC Offset Level Test, Channel 1")
        If Lev1 < -3.3 Or Lev1 > -2.4 Then ' was -2.7
            Echo(FormatResults(False, , , "-3.3", "-2.4", CStr(Lev1), "V"))
            RegisterFailure(COUNTER, "C/T-12-001", Truncate(Lev1), "V", -3.3, -2.4, "Channel 1 -- DC Offset Level Test.")
            TestCounterVT = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            Echo(FormatResults(True, , , "-3.3", "-2.4", CStr(Lev1), "V"))
            IncStepPassed()
        End If
        frmSTest.proProgress.Value = 45
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "C/T" And OptionStep = 12 Then
            GoTo CT12
        End If

        'C/T-13-001 DC Offset Level Test, Channel 2
CT13:
        Echo("C/T-13-001 DC Offset Level Test, Channel 2")
        If Lev2 < -3.3 Or Lev2 > -2.4 Then
            Echo(FormatResults(False, , , "-3.3", "-2.4", CStr(Lev2), "V"))
            RegisterFailure(COUNTER, "C/T-13-001", Truncate(Lev2), "V", -3.3, -2.4, "Channel 2 -- DC Offset Level Test.")
            TestCounterVT = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            Echo(FormatResults(True, , , "-3.3", "-2.4", CStr(Lev2), "V"))
            IncStepPassed()
        End If

        frmSTest.proProgress.Value = 50
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "C/T" And OptionStep = 13 Then
            GoTo CT13
        End If

        'C/T-14-001
        nSystErr = WriteMsg(ARB, "ABOR")
        nSystErr = WriteMsg(ARB, "VOLT:OFFS 4;VOLT 2 Vpk")
        nSystErr = WriteMsg(ARB, "FUNC SIN")
        nSystErr = WriteMsg(ARB, "FREQ 1E6")
        nSystErr = WriteMsg(ARB, "INIT")

CT14:
        nSystErr = vxiClear(COUNTER)
        nSystErr = WriteMsg(COUNTER, "*RST;*CLS")
        nSystErr = WriteMsg(COUNTER, "INP1:IMP 50")
        nSystErr = WriteMsg(COUNTER, "INP2:IMP 50")
        nSystErr = WriteMsg(COUNTER, "SENS:ROSC:SOUR INT")
        nSystErr = WriteMsg(COUNTER, "INP:ROUT COMM")
        nSystErr = WriteMsg(COUNTER, "INP1:COUP AC")
        nSystErr = WriteMsg(COUNTER, "INP2:COUP AC")

        Delay(0.5)
        nSystErr = WriteMsg(COUNTER, "MEAS1:FREQ? 1E6,1")
        dMeasurement = FetchMeasurement(COUNTER, "AC Coupling Verification Channel 1", "95E+4", "105E+4", "Hz", COUNTER, "C/T-14-001")
        If dMeasurement > 1050000.0 Or dMeasurement < 950000.0 Then
            TestCounterVT = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        frmSTest.proProgress.Value = 55
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "C/T" And OptionStep = 14 Then
            GoTo CT14
        End If

        'C/T-15-001
        nSystErr = WriteMsg(SWITCH1, "OPEN 1.3001") 'close S401-1,3  Disconnect P12 ARB MARKER OUT to P21 C/T ARM IN

CT15:
        nSystErr = WriteMsg(COUNTER, "MEAS2:FREQ? 1E6,1")
        dMeasurement = FetchMeasurement(COUNTER, "AC Coupling Verification Channel 2", "95E+4", "105E+4", "Hz", COUNTER, "C/T-15-001")
        If dMeasurement > 1050000.0 Or dMeasurement < 950000.0 Then
            TestCounterVT = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        frmSTest.proProgress.Value = 60
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "C/T" And OptionStep = 15 Then
            GoTo CT15
        End If

        'C/T-16-001 Time Interval Test
        nSystErr = WriteMsg(ARB, "*RST;*CLS")
        nSystErr = WriteMsg(ARB, "VOLT:UNIT:VOLT VPP")
        nSystErr = WriteMsg(ARB, "FREQ 1E+6")
        nSystErr = WriteMsg(ARB, "FUNC SQU")
        nSystErr = WriteMsg(ARB, "VOLT 1")
        nSystErr = WriteMsg(ARB, "OUTP:FILT:FREQ 10E6")
        nSystErr = WriteMsg(ARB, "OUTP:FILT ON")
        nSystErr = WriteMsg(ARB, "INIT")
        Delay(3)

CT16:
        '** Time Interval Test
        'connect 1 MHz test input to Input 1
        nSystErr = WriteMsg(COUNTER, "*RST")
        Delay(0.5)
        nSystErr = WriteMsg(COUNTER, "INP1:IMP 50")
        nSystErr = WriteMsg(COUNTER, "INP2:IMP 50")
        nSystErr = WriteMsg(COUNTER, "SENS:ROSC:SOUR INT")
        nSystErr = WriteMsg(COUNTER, "INP:ROUT COMM") 'Common Input Mode
        nSystErr = WriteMsg(COUNTER, "SENS1:EVEN:SLOP POS") 'Input 1 trigger on positive slope
        nSystErr = WriteMsg(COUNTER, "SENS2:EVEN:SLOP NEG") 'Input 2 trigger on negative slope
        nSystErr = WriteMsg(COUNTER, "MEAS1:TINT?") 'Measure time Interval 1->2
        Delay(1)

        dMeasurement = FetchMeasurement(COUNTER, "Time Interval Test", "450E-9", "530E-9", "sec", COUNTER, "C/T-16-001")

        If (dMeasurement > 0.00000053) Or (dMeasurement < 0.00000045) Then
            If (dMeasurement > 0.0000001) Or (dMeasurement < 0) Then 'Totally inaccurate measurement(on the order of more than 50ns)
                sMessage = "Malfunctioning in slope switching"
            Else
                sMessage = "Probable cause is from the Input Board and the trigger level circuitry"
            End If
            Echo("C/T-16-001 Time Interval Test Diagnostic")
            Echo("    " & sMessage)
            TestCounterVT = FAILED
            IncStepFailed()
            'Added by           for DR# 129 on 2/14/2001
            '=============================================================================================
            MsgBox("Time Interval Test failed. Performing Counter/Timer Adjustment 2 (Input Amplifier Offset)" & vbCrLf & " in the TETS Calibration Program may correct this problem.", MsgBoxStyle.Information)
            '=============================================================================================
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        frmSTest.proProgress.Value = 65
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "C/T" And OptionStep = 16 Then
            GoTo CT16
        End If

        'C/T-17-001
CT17:
        'This time, turn on X10 attenuation and repeat
        nSystErr = WriteMsg(COUNTER, "INP1:COUP AC;ATT 10")
        nSystErr = WriteMsg(COUNTER, "INP2:COUP AC;ATT 10")
        nSystErr = WriteMsg(ARB, "VOLT 3 VRMS")
        nSystErr = WriteMsg(ARB, "INIT")
        Delay(0.5)
        nSystErr = WriteMsg(COUNTER, "MEAS1:TINT?")

        dMeasurement = FetchMeasurement(COUNTER, "Time Interval Test w/ Attenuation", "450E-9", "530E-9", "sec", COUNTER, "C/T-17-001")
        If (dMeasurement > 0.00000053) Or (dMeasurement < 0.00000045) Then
            dFH_Meas = dMeasurement
            If (dMeasurement > 0.0000001) Or (dMeasurement < 0) Then 'Totally inaccurate measurement(on the order of more than 50ns)
                sMessage = "Malfunctioning in slope switching"
            Else
                sMessage = "Probable cause is from the Input Board and the trigger level circuitry"
            End If
            Echo("C/T-17-001 Time Interval Test w/ Attenuation Diagnostic")
            Echo("    " & sMessage)
            TestCounterVT = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        frmSTest.proProgress.Value = 70
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "C/T" And OptionStep = 17 Then
            GoTo CT17
        End If

        'C/T-18-001
CT18:
        'This time, measure the negative portion of the 1 MHz input waveform.
        nSystErr = WriteMsg(ARB, "VOLT 1 VPP")
        nSystErr = WriteMsg(ARB, "INIT")
        'connect 10 MHz test input to Input 1
        nSystErr = WriteMsg(COUNTER, "*RST")
        nSystErr = WriteMsg(COUNTER, "INP1:IMP 50")
        nSystErr = WriteMsg(COUNTER, "INP2:IMP 50")
        nSystErr = WriteMsg(COUNTER, "SENS:ROSC:SOUR INT")
        nSystErr = WriteMsg(COUNTER, "INP:ROUT COMM") 'Common Input Mode
        nSystErr = WriteMsg(COUNTER, "SENS1:EVEN:SLOP NEG") 'Input 1 trigger on negative slope
        nSystErr = WriteMsg(COUNTER, "SENS2:EVEN:SLOP POS") 'Input 2 trigger on positive slope
        nSystErr = WriteMsg(COUNTER, "MEAS1:TINT?") 'Measure time Interval 1->2

        dMeasurement = FetchMeasurement(COUNTER, "Time Interval Test (Negative Edge)", "470E-9", "530E-9", "sec", COUNTER, "C/T-18-001")
        If (dMeasurement > 0.00000053) Or (dMeasurement < 0.00000047) Then
            If (dMeasurement > 0.0000001) Or (dMeasurement < 0) Then 'Totally inaccurate measurement(on the order of more than 50ns)
                sMessage = "Malfunctioning in slope switching"
            Else
                sMessage = "Probable cause is from the Input Board and the trigger level circuitry"
            End If
            Echo("C/T-18-001 Time Interval Test (Negative Edge) Diagnostic")
            Echo("    " & sMessage)
            TestCounterVT = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        frmSTest.proProgress.Value = 75
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "C/T" And OptionStep = 18 Then
            GoTo CT18
        End If

        nSystErr = WriteMsg(SWITCH1, "CLOSE 1.3001") 'close S401-1,3  Reconnect P12 ARB MARKER OUT to P21 C/T ARM IN

        'C/T-19-001
CT19:
        'This time, turn on X10 attenuation and repeat
        nSystErr = WriteMsg(COUNTER, "INP1:COUP AC;ATT 10")
        nSystErr = WriteMsg(COUNTER, "INP2:COUP AC;ATT 10")
        nSystErr = WriteMsg(ARB, "VOLT 3 VRMS")
        nSystErr = WriteMsg(ARB, "INIT")
        Delay(1)
        nSystErr = WriteMsg(COUNTER, "MEAS1:TINT?")

        dMeasurement = FetchMeasurement(COUNTER, "Time Interval Test (Negative Edge w/ Attenuation)", "470E-9", "530E-9", "sec", COUNTER, "C/T-19-001")
        If (dMeasurement > 0.00000053) Or (dMeasurement < 0.00000047) Then
            If (dMeasurement > 0.0000001) Or (dMeasurement < 0) Then 'Totally inaccurate measurement(on the order of more than 50ns)
                sMessage = "Malfunctioning in slope switching"
            Else
                sMessage = "Probable cause is from the Input Board and the trigger level circuitry"
            End If
            Echo("C/T-19-001 Time Interval Test (Negative Edge w/ Attenuation) Diagnostic")
            Echo("    " & sMessage)
            TestCounterVT = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        frmSTest.proProgress.Value = 80
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "C/T" And OptionStep = 19 Then
            GoTo CT19
        End If

        'C/T-20-001
CT20:
        '** External Arm Input Test: checks the front panel external arm input amplifier circuitry by configuring the arm
        '                            to act as the gate for as the gate for measuring a signal input on channel 1

        'The following procedures are performed to make measuremensts using the first positive edge of the arm input
        'to start the aperture gate, and the second positive edge of the arm input stop the gate.
        'The aperture time is therefore the period of the arm input signal: 10ns

        nSystErr = WriteMsg(ARB, "Volt 1 VPP")
        nSystErr = WriteMsg(ARB, "INIT")

        nSystErr = WriteMsg(COUNTER, "*RST;*CLS")
        nSystErr = WriteMsg(COUNTER, "INP1:IMP 50")
        nSystErr = WriteMsg(COUNTER, "INP2:IMP 50")
        nSystErr = WriteMsg(COUNTER, "SENS:ROSC:SOUR INT")
        nSystErr = WriteMsg(COUNTER, "ARM:STAR:SOUR EXT;LEV 0") 'ARM Input: Start, Stop on
        nSystErr = WriteMsg(COUNTER, "ARM:STAR:SLOP POS") 'positive edges of External
        nSystErr = WriteMsg(COUNTER, "ARM:STOP:SOUR EXT;LEV 0") 'Source with an arm trigger
        nSystErr = WriteMsg(COUNTER, "ARM:STOP:SLOP POS") 'Level of 0 volts
        nSystErr = WriteMsg(COUNTER, "MEAS1:FREQ?") 'Measure Freaquency: Input 1

        dMeasurement = FetchMeasurement(COUNTER, "External Arm Input Test", "95E+4", "105E+4", "Hz", COUNTER, "C/T-20-001")
        If (dMeasurement > 1050000) Or (dMeasurement < 950000) Then
            IncStepFailed()
            'C/T-20-D01
            dFH_Meas = dMeasurement
            'Disconnect P12 Arb Marker Out from P21 C/T ARM IN
            nSystErr = WriteMsg(SWITCH1, "OPEN 1.3001") 'open  S401-1,3  bb
            'Connect P12 Arb Marker Out to P15 SCOPE INPUT 1.
            nSystErr = WriteMsg(SWITCH1, "CLOSE 2.3103") 'close S405-1,5  bb

            nSystErr = WriteMsg(OSCOPE, "*RST; *CLS")
            nSystErr = WriteMsg(OSCOPE, "AUT") 'Set Autoscale mode
            Delay(2)
            nSystErr = WriteMsg(OSCOPE, "DIG CHAN1")
            nSystErr = WriteMsg(OSCOPE, "MEAS:SOUR CHAN1")
            nSystErr = WriteMsg(OSCOPE, "MEAS:VAMP?")

            dMeasurement = FetchMeasurement(OSCOPE, "ARB Marker Out Diagnostic Test", "1", "", "V", ARB, "C/T-20-D01")
            If dMeasurement > 1 Then
                sMessage = "The likely cause of the failure is the Counter/Timer Input Board."
                Echo(FormatResults(False, "C/T ARB Marker Out Diagnostic Test", "C/T-20-D01"))
                Echo("    " & sMessage)
                RegisterFailure(COUNTER, "C/T-20-D01", dFH_Meas, "Hz", 950000, 1050000, "ARB Marker Out Diagnostic Test" & vbCrLf & sMessage)
                TestCounterVT = FAILED
            Else
                Echo(FormatResults(False, "C/T ARB Marker Out Diagnostic Test", "C/T-20-D01"))
                RegisterFailure(ARB, "C/T-20-D01", dFH_Meas, "Hz", 950000, 1050000, "ARB Marker Out Diagnostic Test")
                TestCounterVT = ARB
            End If
            'Disconnect P12 Arb Marker Out from P15 SCOPE INPUT 1.
            nSystErr = WriteMsg(SWITCH1, "OPEN 2.3103") 'open S405-1,5  bb
            'Reconnect P12 Arb Marker Out to P21 C/T ARM IN
            nSystErr = WriteMsg(SWITCH1, "CLOSE 1.3001") 'close  S401-1,3  bb
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        frmSTest.proProgress.Value = 85
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "C/T" And OptionStep = 20 Then
            GoTo CT20
        End If

        'C/T-21-001
CT21:
        '************************************Input Channel 2 Test***************************************
        '** The same Auto Frequency Measurement Test is repeated to see if Input 2 Channel functionally works fine
        'Connect a 1-MHz timebase standard to the Int/Ext ref. BNC.
        nSystErr = vxiClear(COUNTER)
        nSystErr = WriteMsg(ARB, "*RST;*CLS")
        nSystErr = WriteMsg(ARB, "FREQ 1E+6")
        nSystErr = WriteMsg(ARB, "FUNC SIN")
        nSystErr = WriteMsg(ARB, "VOLT 3VPP")
        nSystErr = WriteMsg(ARB, "VOLT:OFFS 2")
        nSystErr = WriteMsg(ARB, "OUTP:FILT:FREQ 10E6")
        nSystErr = WriteMsg(ARB, "OUTP:FILT ON")
        nSystErr = WriteMsg(ARB, "INIT")
        Delay(0.5)

        ' Disconnect P10 Arb output from P19 C/T INPUT 1.
        nSystErr = WriteMsg(SWITCH1, "OPEN  2.3001") 'open  S404-1,3  bb
        ' Connect P10 Arb output to P20 C/T INPUT 2.
        nSystErr = WriteMsg(SWITCH1, "CLOSE 2.3002") 'close S404-1,4  bb

        nSystErr = vxiClear(COUNTER)
        nSystErr = WriteMsg(COUNTER, "*RST;*CLS")
        nSystErr = WriteMsg(COUNTER, "INP1:IMP 50")
        nSystErr = WriteMsg(COUNTER, "INP2:IMP 50")
        nSystErr = WriteMsg(COUNTER, "SENS:ROSC:SOUR INT")
        nSystErr = WriteMsg(COUNTER, "SENS2:EVEN:LEV:AUTO ON") 'Place counter in auto-trigger mode
        Delay(1)
        nSystErr = WriteMsg(COUNTER, "CONF2:FREQ AUTO")
        nSystErr = WriteMsg(COUNTER, "INIT2; FETCH2?")
        Delay(2)

        dMeasurement = FetchMeasurement(COUNTER, "Counter Auto Frequency Measurement Test Channel 2", ".90E+6", "1.05E+6", "Hz", COUNTER, "C/T-21-001")
        If (dMeasurement > 1050000) Or (dMeasurement < 900000) Then
            TestCounterVT = FAILED
            'C/T-21-001
            sMessage = "Probable cause is from the auto-triggering circuitry"
            Echo("C/T-21-001 Counter Auto Frequency Measurement Test Channel 2 Diagnostic")
            Echo("    " & sMessage)
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        frmSTest.proProgress.Value = 90
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "C/T" And OptionStep = 21 Then
            GoTo CT21
        End If

        'C/T-22-001
CT22:
        nSystErr = vxiClear(COUNTER)
        nSystErr = WriteMsg(COUNTER, "*RST;*CLS")
        nSystErr = WriteMsg(COUNTER, "INP1:IMP 50")
        nSystErr = WriteMsg(COUNTER, "INP2:IMP 50")
        nSystErr = WriteMsg(COUNTER, "SENS:ROSC:SOUR INT")
        nSystErr = WriteMsg(COUNTER, "INP2:COUP AC")
        nSystErr = WriteMsg(COUNTER, "INP2:IMP 50")

        'Turn on the x10 attenuator
        nSystErr = WriteMsg(COUNTER, "INP2:ATT 10")
        nSystErr = WriteMsg(ARB, "ABORT")
        nSystErr = WriteMsg(ARB, "VOLT:UNIT:VOLT VRMS")
        Delay(1)
        nSystErr = WriteMsg(ARB, "VOLT 3")
        nSystErr = WriteMsg(ARB, "INIT")
        Delay(1)
        nSystErr = WriteMsg(COUNTER, "MEAS2:AC?")
        Delay(2)

        dMeasurement = FetchMeasurement(COUNTER, "AC RMS Voltage Channel 2 w/ Attenuation", "0.2", "0.4", "Volts", COUNTER, "C/T-22-001")
        If dMeasurement > 0.4 Or dMeasurement < 0.2 Then
            TestCounterVT = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        frmSTest.proProgress.Value = 95
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "C/T" And OptionStep = 22 Then
            GoTo CT22
        End If

        'C/T-23-001
CT23:
        nSystErr = WriteMsg(COUNTER, "INP2:ATT 1")
        nSystErr = WriteMsg(COUNTER, "MEAS2:AC?")
        Delay(1)

        dMeasurement = FetchMeasurement(COUNTER, "AC RMS Voltage Channel 2", "2", "4", "Volts", COUNTER, "C/T-23-001")
        If dMeasurement > 4 Or dMeasurement < 2 Then
            TestCounterVT = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        frmSTest.proProgress.Value = 100
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "C/T" And OptionStep = 23 Then
            GoTo CT23
        End If

TestComplete:
        frmSTest.cmdAbort.Text = "Abort Test"
        frmSTest.cmdPause.Text = "Pause Test"
        nSystErr = WriteMsg(SWITCH1, "RESET") ' open all switches
        nSystErr = WriteMsg(COUNTER, "*RST")
        nSystErr = WriteMsg(ARB, "*RST;*CLS")
        nSystErr = WriteMsg(OSCOPE, "*RST;*CLS")

        Application.DoEvents()
        If AbortTest = True Then
            If TestCounterVT = FAILED Then
                ReportFailure(COUNTER)
            Else
                ReportUnknown(COUNTER)
            End If
        ElseIf TestCounterVT = PASSED Then
            ReportPass(COUNTER)
        ElseIf TestCounterVT = FAILED Then
            ReportFailure(COUNTER)
        Else
            ReportUnknown(COUNTER)
        End If
    End Function





End Module