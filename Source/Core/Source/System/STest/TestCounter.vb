'Option Strict Off
Option Explicit On

Imports System.Math

Public Module CounterTest


    '**************************************************************
    '* Nomenclature   : ATS-ViperT SYSTEM SELF TEST               *
    '*                  E1420B Counter Test                       *
    '* Version        : 2.0                                       *
    '* Last Update    : Apr 1, 2017                               *
    '* Purpose        : This module is the C/T self test          *
    '**************************************************************
    Dim S As String = ""


    Public Function TestCT() As Integer
        'DESCRIPTION:
        '   This routine tests Counter and returns PASSED or FAILED
        'RETURNS:
        '   PASSED if the COUNTER test passes or FAILED if a failure occurs

        
        Dim SSelfTest As String = ""
        Dim SelfTest As Integer
        Dim Inc As Single
        Dim Lev As Single
        Dim Lev1 As Single
        Dim Lev2 As Single
        Dim StrErrorMsg As String = ""
        Dim StrErrorMsg1 As String = ""
        Dim ErrorMsg1 As Integer
        Dim StrErrorMsg2 As String = ""
        Dim ErrorMsg2 As Integer
        Dim sMessage As String 'Describes Failure
        Dim LoopStepNo As Integer
        Dim x As DialogResult
        HelpContextID = 1200

        'Counter/Timer Test Title block
        EchoTitle(InstrumentDescription(COUNTER), True)
        EchoTitle("Counter/Timer Test", False)
        frmSTest.proProgress.Value = 1
        frmSTest.proProgress.Maximum = 100

        TestCT = UNTESTED
        If AbortTest = True Then GoTo TestComplete
        AbortTest = False
        TestCT = PASSED

        nSystErr = vxiClear(COUNTER)
        nSystErr = WriteMsg(COUNTER, "*RST")
        nSystErr = WriteMsg(COUNTER, "*CLS")
        nSystErr = vxiClear(ARB)
        nSystErr = WriteMsg(COUNTER, "SENS:ROSC:SOUR INT")
        nSystErr = WriteMsg(ARB, "*RST")
        nSystErr = WriteMsg(ARB, "*CLS")
        nSystErr = WriteMsg(ARB, "MARK:STAT OFF") 'Disable the AFG to output a marker list    
        nSystErr = WriteMsg(ARB, "OUTP:LOAD 50")
        nSystErr = vxiClear(OSCOPE)
        nSystErr = WriteMsg(OSCOPE, "*RST")
        nSystErr = WriteMsg(OSCOPE, "*CLS")
        nSystErr = WriteMsg(OSCOPE, "SYST:LANG COMP") 'Set Autoscale mode?

        nSystErr = WriteMsg(COUNTER, "SENS:ROSC:SOUR INT")
        LoopStepNo = 1
        If RunningEndToEnd = False And FirstPass = True Then
            x = MsgBox("Is cable W201 installed?", MsgBoxStyle.YesNo)
            If x <> DialogResult.Yes Then
                'Install cable W201
                InstallW201CablePartial()

                If AbortTest = True Then GoTo TestComplete
            End If
        End If


        'C/T-01-001
CT1:
        '** Perform a COUNTER Power-up Built-In Test
        Echo("C/T-01-001 Counter Built-In Test. . .")
        nSystErr = WriteMsg(COUNTER, "*TST?")
        nSystErr = WaitForResponse(COUNTER, 0.1)
        SelfTest = ReadMsg(COUNTER, SSelfTest)

        If SelfTest <> 0 Or Val(SSelfTest) Then
            TestCT = FAILED
            IncStepFailed()
            Echo(FormatResults(False, "Counter Built-In Test " & StripNullCharacters(SSelfTest)))
            RegisterFailure(COUNTER, "C/T-01-001", , , , , sGuiLabel(COUNTER) & " FAILED Built-in Test")
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
            Echo(FormatResults(True, "Counter Built-In Test")) '1
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "C/T" And OptionStep = LoopStepNo Then
            GoTo CT1
        End If
        frmSTest.proProgress.Value = 4
        LoopStepNo += 1

        'C/T-02-001

        '**************************************Input Channel 1 Test***************************************************
        'Assume all signals are connected to Counter Input 1 unless otherwise specified.
        'The measurement tolerance assumed to be +/- 5%

CT2:    'Generate 1 MHz sine wave with 1VPP
        nSystErr = WriteMsg(ARB, "*RST")
        nSystErr = WriteMsg(ARB, "*CLS")
        nSystErr = WriteMsg(ARB, "MARK:STAT OFF") 'Disable the AFG to output a marker list   
        nSystErr = WriteMsg(ARB, "OUTP:LOAD 50")
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
        nSystErr = WriteMsg(ARB, "INIT:IMM")
        nSystErr = WriteMsg(ARB, "OUTP:STAT ON")
        Delay(1)

        'Connect a 10 MHz timebase standard to the Int/Ext ref. BNC.
        nSystErr = vxiClear(COUNTER)

        nSystErr = WriteMsg(COUNTER, "*RST")
        nSystErr = WriteMsg(COUNTER, "*CLS")
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
            TestCT = FAILED
            If (OptionFaultMode = SOFmode) And (OptionMode <> LOSmode) Then
                dFH_Meas = dMeasurement
                'Disconnect P10 Arb Output from P19 C/T Input 1
                nSystErr = WriteMsg(SWITCH1, "OPEN 2.3001") 'open  S404-1,3  bb
                'Connect P10 Arb Output to P15 Scope Input 1
                nSystErr = WriteMsg(SWITCH1, "CLOSE 2.3003") 'close S404-1,5  bb

                nSystErr = WriteMsg(OSCOPE, "*RST")
                nSystErr = WriteMsg(OSCOPE, "*CLS")
                nSystErr = WriteMsg(OSCOPE, "AUT") 'Set Autoscale mode
                Delay(2)
                nSystErr = WriteMsg(OSCOPE, "DIG CHAN1")
                nSystErr = WriteMsg(OSCOPE, "MEAS:SOUR CHAN1")
                nSystErr = WriteMsg(OSCOPE, "MEAS:FREQ?")
                dMeasurement = FetchMeasurement(OSCOPE, "Counter Auto Frequency Measurement Diagnostic Test", ".90E+6", "1.10E+6", "Hz", COUNTER, "C/T-02-D01")
                If dMeasurement > 1050000 Or dMeasurement < 950000 Then
                    Echo(FormatResults(False, "Counter Auto Frequency Measurement Diagnostic Test", "C/T-02-D01"))
                    RegisterFailure(ARB, "C/T-02-D01", dFH_Meas, "Hz", 950000, 1050000, "Counter Auto Frequency Measurement Diagnostic Test")
                    TestCT = ARB
                Else
                    Echo(FormatResults(False, "Counter Auto Frequency Measurement Diagnostic Test", "C/T-02-D01"))
                    RegisterFailure(COUNTER, "C/T-02-D01", dFH_Meas, "Hz", 950000, 1050000, "Counter Auto Frequency Measurement Diagnostic Test")
                    Echo("    Auto Frequency Measurement Failed.  Probable cause is from the auto-triggering circuitry.")
                    If OptionFaultMode = SOFmode Then GoTo TestComplete
                End If
                'Disconnect P10 Arb Output from P15 Scope Input 1
                nSystErr = WriteMsg(SWITCH1, "OPEN 2.3003") 'close S404-1,5  bb
                'Reconnect P10 Arb Output to P19 C/T Input 1
                nSystErr = WriteMsg(SWITCH1, "CLOSE 2.3001") 'open  S404-1,3  bb
            End If
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "C/T" And OptionStep = LoopStepNo Then
            GoTo CT2
        End If

        frmSTest.proProgress.Value = 8
        LoopStepNo += 1


CT3:    'C/T-03-001
        '** Input Signal Conditioning Test:  A Functional test of the input amplifier relays and circuitry associated with those relays
        nSystErr = WriteMsg(ARB, "ABORT")
        nSystErr = WriteMsg(ARB, "OUTP:LOAD 50")
        nSystErr = WriteMsg(ARB, "VOLT 3")
        nSystErr = WriteMsg(ARB, "VOLT:OFFS 0") 'Change the previous offset from 2V to 0V
        nSystErr = WriteMsg(ARB, "FREQ 1E+6") ' Output a 3Vpp sine wave
        nSystErr = WriteMsg(ARB, "INIT:IMM")
        nSystErr = WriteMsg(ARB, "OUTP:STAT ON")
        Delay(1)
        nSystErr = vxiClear(COUNTER)

        nSystErr = WriteMsg(COUNTER, "*RST")
        nSystErr = WriteMsg(COUNTER, "*CLS")
        Delay(0.5)
        nSystErr = WriteMsg(COUNTER, "INP1:IMP 50")
        nSystErr = WriteMsg(COUNTER, "INP2:IMP 50")
        nSystErr = WriteMsg(COUNTER, "SENS:ROSC:SOUR INT")
        nSystErr = WriteMsg(COUNTER, "INP:ROUT COMM") 'Common Input Mode

        'The following loop increases the trigger levels of Input 1 and Input 2 until the trigger lights just go off.
        'No trigger causes a timeout error, which ends the do loop
        Inc = 0.1 'Increase trigger level by 0.1V
        Lev = 0.8 'Initial trigger level  (initial level was 0.8)

        Echo("C/T-03-001 Trigger Level Test, Channel 1")
        Do
            Lev += Inc
            nSystErr = WriteMsg(COUNTER, "SENS1:EVEN:LEV " & CStr(Lev) & "V")
            nSystErr = WriteMsg(COUNTER, "MEAS1:FREQ?")
            Delay(1)
            nSystErr = ReadMsg(COUNTER, StrErrorMsg)
            If Val(StrErrorMsg) = 0 Or nSystErr <> 0 Then Exit Do
            If AbortTest = True Then GoTo TestComplete
        Loop Until Lev = 2

        If (Lev < 1.2) Or (Lev > 1.8) Then
            RegisterFailure(COUNTER, "C/T-03-001", CStr(Lev), "", 1.2, 1.8, sGuiLabel(COUNTER) & " FAILED Trigger Level Test, Channel 1")
            Echo(FormatResults(False, , , "1.2", "1.8", CStr(Lev), "V"))
            TestCT = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            Echo(FormatResults(True, , , "1.2", "1.8", CStr(Lev), "V"))
            IncStepPassed()
        End If
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "C/T" And OptionStep = LoopStepNo Then
            GoTo CT3
        End If

        LoopStepNo += 1
        frmSTest.proProgress.Value = 12


CT4:    'C/T-04-001 Channel 2
        nSystErr = vxiClear(COUNTER)

        nSystErr = WriteMsg(COUNTER, "*RST")
        nSystErr = WriteMsg(COUNTER, "*CLS")
        Delay(0.5)
        nSystErr = WriteMsg(COUNTER, "INP1:IMP 50")
        nSystErr = WriteMsg(COUNTER, "INP2:IMP 50")
        nSystErr = WriteMsg(COUNTER, "SENS:ROSC:SOUR INT")
        nSystErr = WriteMsg(COUNTER, "INP:ROUT COMM") ' this routes Input channel 1 to both channels
        Delay(1)
        Lev = 0.5
        nSystErr = 0
        Echo("C/T-04-001 Trigger Level Test, Channel 2")
        Do
            Lev += Inc
            nSystErr = WriteMsg(COUNTER, "SENS2:EVEN:LEV " & CStr(Lev))
            nSystErr = WriteMsg(COUNTER, "MEAS2:FREQ?")
            Delay(1)
            nSystErr = ReadMsg(COUNTER, StrErrorMsg)
            If Val(StrErrorMsg) = 0 Or nSystErr <> 0 Then Exit Do
            If AbortTest = True Then GoTo TestComplete
        Loop Until Lev = 2

        If (Lev < 1.2) Or (Lev > 1.8) Then
            RegisterFailure(COUNTER, "C/T-04-001", CStr(Lev), "", 1.2, 1.8, sGuiLabel(COUNTER) & " FAILED Trigger Level Test, Channel 2")
            Echo(FormatResults(False, , , "1.2", "1.8", CStr(Lev), "V"))
            TestCT = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            Echo(FormatResults(True, , , "1.2", "1.8", CStr(Lev), "V"))
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "C/T" And OptionStep = LoopStepNo Then
            GoTo CT4
        End If

        frmSTest.proProgress.Value = 16
        LoopStepNo += 1


CT5:    'C/T-05-001
        nSystErr = WriteMsg(ARB, "ABORT")
        nSystErr = WriteMsg(ARB, "FREQ 1E+6")
        nSystErr = WriteMsg(ARB, "INIT:IMM")
        nSystErr = WriteMsg(ARB, "OUTP:STAT ON")

        'Inputs 1,2:  1 Mohm impedance applied
        nSystErr = vxiClear(COUNTER)
        nSystErr = WriteMsg(COUNTER, "*RST")
        nSystErr = WriteMsg(COUNTER, "*CLS")
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
            TestCT = FAILED
            Echo(FormatResults(False, "C/T Separate Routing Test 1", "C/T-05-001"))
            RegisterFailure(COUNTER, "C/T-05-001", , , , , "Separate Routing Test 1")
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            Echo(FormatResults(True, "Separate Routing Test 1", "C/T-05-001"))
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "C/T" And OptionStep = LoopStepNo Then ' 5
            GoTo CT5
        End If

        frmSTest.proProgress.Value = 20
        LoopStepNo += 1

CT6:    'C/T-06-001
        nSystErr = vxiClear(COUNTER)
        nSystErr = WriteMsg(COUNTER, "*RST")
        nSystErr = WriteMsg(COUNTER, "*CLS")
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
            TestCT = FAILED
            Echo(FormatResults(False, "C/T Separate Routing Test 2", "C/T-06-001"))
            RegisterFailure(COUNTER, "C/T-06-001", , , , , "Separate Routing Test 2")
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            Echo(FormatResults(True, "Separate Routing Test 2", "C/T-06-001"))
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "C/T" And OptionStep = LoopStepNo Then
            GoTo CT6
        End If

        frmSTest.proProgress.Value = 24
        LoopStepNo += 1

CT7:    'C/T-07-001
        'Change both input impedance levels to 50 ohms
        nSystErr = WriteMsg(COUNTER, "INP1:IMP 50")
        nSystErr = WriteMsg(COUNTER, "MEAS1:FREQ?")
        ErrorMsg1 = ReadMsg(COUNTER, StrErrorMsg1)

        nSystErr = vxiClear(COUNTER)
        nSystErr = WriteMsg(COUNTER, "*RST")
        nSystErr = WriteMsg(COUNTER, "*CLS")
        nSystErr = WriteMsg(COUNTER, "INP1:IMP 50")
        nSystErr = WriteMsg(COUNTER, "INP2:IMP 50")
        nSystErr = WriteMsg(COUNTER, "SENS:ROSC:SOUR INT")
        nSystErr = WriteMsg(COUNTER, "SENS2:EVEN:LEV 2")
        nSystErr = WriteMsg(COUNTER, "INP:ROUT COMM")
        Delay(1)
        nSystErr = WriteMsg(COUNTER, "MEAS2:FREQ?")
        ErrorMsg2 = ReadMsg(COUNTER, StrErrorMsg2)


        If ErrorMsg1 = 0 Or ErrorMsg2 = 0 Then
            TestCT = FAILED
            RegisterFailure(COUNTER, "C/T-07-001", , , , , "Separate Routing Test 3")
            Echo(FormatResults(False, "Separate Routing Test 3", "C/T-07-001"))
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            Echo(FormatResults(True, "Separate Routing Test 3", "C/T-07-001"))
            IncStepPassed()
        End If
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "C/T" And OptionStep = LoopStepNo Then
            GoTo CT7
        End If

        frmSTest.proProgress.Value = 28
        LoopStepNo += 1


CT8:    'C/T-08-001
        nSystErr = vxiClear(COUNTER)
        nSystErr = WriteMsg(COUNTER, "*RST")
        nSystErr = WriteMsg(COUNTER, "*CLS")
        nSystErr = WriteMsg(COUNTER, "INP1:IMP 50")
        nSystErr = WriteMsg(COUNTER, "INP2:IMP 50")
        nSystErr = WriteMsg(COUNTER, "SENS:ROSC:SOUR INT")
        nSystErr = WriteMsg(COUNTER, "INP1:COUP AC")
        nSystErr = WriteMsg(COUNTER, "INP1:ATT 10") 'Turn on the x10 attenuator

        nSystErr = WriteMsg(ARB, "ABORT")
        nSystErr = WriteMsg(ARB, "VOLT:UNIT:VOLT VRMS")
        nSystErr = WriteMsg(ARB, "VOLT 3")
        nSystErr = WriteMsg(ARB, "INIT:IMM")
        nSystErr = WriteMsg(ARB, "OUTP:STAT ON")

        'Measure AC rms voltage of the input signal through the x10 attenuators
        Delay(0.5)
        nSystErr = WriteMsg(COUNTER, "MEAS1:AC?")
        Delay(0.5)

        dMeasurement = FetchMeasurement(COUNTER, "Attenuated AC RMS Voltage Measurement Test", "200E-3", "400E-3", "Volts", COUNTER, "C/T-08-001")
        If dMeasurement > 0.4 Or dMeasurement < 0.2 Then
            TestCT = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "C/T" And OptionStep = LoopStepNo Then
            GoTo CT8
        End If

        frmSTest.proProgress.Value = 32
        LoopStepNo += 1

CT9:    'C/T-09-001
        nSystErr = WriteMsg(COUNTER, "INP1:ATT 1")
        nSystErr = WriteMsg(COUNTER, "INP2:ATT 1")
        nSystErr = WriteMsg(COUNTER, "MEAS1:AC?")
        Delay(0.3)

        dMeasurement = FetchMeasurement(COUNTER, "AC RMS Voltage Measurement Test", "2", "4", "Volts", COUNTER, "C/T-09-001")
        If dMeasurement > 4 Or dMeasurement < 2 Then
            TestCT = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "C/T" And OptionStep = LoopStepNo Then
            GoTo CT9
        End If

        frmSTest.proProgress.Value = 36
        LoopStepNo += 1

CT10:   'C/T-10-001
        'Reset Counter
        nSystErr = vxiClear(COUNTER)
        nSystErr = WriteMsg(COUNTER, "*RST")
        nSystErr = WriteMsg(COUNTER, "*CLS")
        nSystErr = WriteMsg(COUNTER, "INP1:IMP 50")
        nSystErr = WriteMsg(COUNTER, "INP2:IMP 50")
        nSystErr = WriteMsg(COUNTER, "SENS:ROSC:SOUR INT")
        nSystErr = WriteMsg(COUNTER, "INP:ROUT COMM") 'Common input mode ON
        nSystErr = WriteMsg(COUNTER, "MEAS1:FREQ?")

        dMeasurement = FetchMeasurement(COUNTER, "AC/DC Relay Verification Test Channel 1", ".95E+6", "1.05E+6", "Hz", COUNTER, "C/T-10-001")
        If dMeasurement > 1050000 Or dMeasurement < 950000 Then
            TestCT = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "C/T" And OptionStep = LoopStepNo Then
            GoTo CT10
        End If

        frmSTest.proProgress.Value = 40
        LoopStepNo += 1

CT11:   'C/T-11-001
        nSystErr = WriteMsg(COUNTER, "MEAS2:FREQ?")

        dMeasurement = FetchMeasurement(COUNTER, "AC/DC Relay Verification Test Channel 2", ".95E+6", "1.05E+6", "Hz", COUNTER, "C/T-11-001")
        If dMeasurement > 1050000 Or dMeasurement < 950000 Then
            TestCT = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "C/T" And OptionStep = LoopStepNo Then
            GoTo CT11
        End If

        frmSTest.proProgress.Value = 42
        LoopStepNo += 1


CT12:   'C/T-12-001 DC Offset Level Test, Channel 1
        nSystErr = vxiClear(COUNTER)
        nSystErr = WriteMsg(COUNTER, "*RST")
        nSystErr = WriteMsg(COUNTER, "*CLS")
        nSystErr = WriteMsg(COUNTER, "INP1:IMP 50")
        nSystErr = WriteMsg(COUNTER, "INP2:IMP 50")
        nSystErr = WriteMsg(COUNTER, "SENS:ROSC:SOUR INT")
        nSystErr = WriteMsg(COUNTER, "INP:ROUT COMM")
        nSystErr = WriteMsg(COUNTER, "SENS1:EVEN:LEV -3")
        nSystErr = WriteMsg(COUNTER, "SENS2:EVEN:LEV -3")

        'Generate 1 MHz Square wave with no overshoot
        nSystErr = WriteMsg(ARB, "*RST")
        nSystErr = WriteMsg(ARB, "*CLS")
        nSystErr = WriteMsg(ARB, "MARK:STAT OFF") 'Disable the ARB to output a marker list    
        nSystErr = WriteMsg(ARB, "FUNC SQU")
        nSystErr = WriteMsg(ARB, "FREQ 1MHZ")
        nSystErr = WriteMsg(ARB, "OUTP:FILT:FREQ 10E6")
        nSystErr = WriteMsg(ARB, "OUTP:FILT ON")
        nSystErr = WriteMsg(ARB, "VOLT:UNIT Vpk")
        nSystErr = WriteMsg(ARB, "OUTP:LOAD 50")
        nSystErr = WriteMsg(ARB, "VOLT 3.5")
        nSystErr = WriteMsg(ARB, "INIT:IMM")
        nSystErr = WriteMsg(ARB, "OUTP:STAT ON")

        'The following loop decreases the signal level until Input 1 and Input 2 both stop triggering.
        'Step the level down until BOTH inputs fail to trigger
        ErrorMsg1 = 0
        ErrorMsg2 = 0
        For Lev = 3.5 To 2 Step -0.05
            nSystErr = WriteMsg(ARB, "VOLT " & CStr(Lev))
            Application.DoEvents()
            If ErrorMsg1 = 0 Then 'If still triggering, Record Vpk-neg for Input 1
                Lev1 = -Lev
                nSystErr = WriteMsg(COUNTER, "MEAS1:FREQ?")
                ErrorMsg1 = ReadMsg(COUNTER, StrErrorMsg)
                If ErrorMsg1 <> 0 Then 'If it did NOT trigger this time
                    nSystErr = vxiClear(COUNTER)
                    nSystErr = WriteMsg(COUNTER, "*CLS") 'Clear errors
                End If
            End If
            If ErrorMsg2 = 0 Then 'If still triggering, Record Vpk-neg for Input 2
                Lev2 = -Lev
                nSystErr = WriteMsg(COUNTER, "MEAS2:FREQ?")
                ErrorMsg2 = ReadMsg(COUNTER, StrErrorMsg)
                If ErrorMsg2 <> 0 Then 'If it did NOT trigger this time
                    nSystErr = vxiClear(COUNTER)
                    nSystErr = WriteMsg(COUNTER, "*CLS") 'Clear errors
                End If
            End If
            If ErrorMsg1 <> 0 And ErrorMsg2 <> 0 Then Exit For
        Next Lev


        Echo("C/T-12-001 DC Offset Level Test, Channel 1")
        If Lev1 < -3.5 Or Lev1 > -2.4 Then ' was -2.7
            Echo(FormatResults(False, , , "-3.5", "-2.4", CStr(Lev1), "V"))
            RegisterFailure(COUNTER, "C/T-12-001", Truncate(Lev1), "V", -3.5, -2.4, "Channel 1 -- DC Offset Level Test.")
            TestCT = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            Echo(FormatResults(True, , , "-3.5", "-2.4", CStr(Lev1), "V"))
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "C/T" And OptionStep = LoopStepNo Then
            GoTo CT12
        End If

        frmSTest.proProgress.Value = 45
        LoopStepNo += 1

CT13:   'C/T-13-001 DC Offset Level Test, Channel 2
        Echo("C/T-13-001 DC Offset Level Test, Channel 2")
        If Lev2 < -3.5 Or Lev2 > -2.4 Then
            Echo(FormatResults(False, , , "-3.5", "-2.4", CStr(Lev2), "V"))
            RegisterFailure(COUNTER, "C/T-13-001", Truncate(Lev2), "V", -3.5, -2.4, "Channel 2 -- DC Offset Level Test.")
            TestCT = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            Echo(FormatResults(True, , , "-3.5", "-2.4", CStr(Lev2), "V"))
            IncStepPassed()
        End If

        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "C/T" And OptionStep = LoopStepNo Then
            GoTo CT13
        End If
        frmSTest.proProgress.Value = 50
        LoopStepNo += 1

        'C/T-14-001
        nSystErr = WriteMsg(ARB, "ABOR")
        nSystErr = WriteMsg(ARB, "VOLT:OFFS 4;VOLT 2 Vpk")
        nSystErr = WriteMsg(ARB, "FUNC SIN")
        nSystErr = WriteMsg(ARB, "FREQ 1E6")
        nSystErr = WriteMsg(ARB, "INIT:IMM")
        nSystErr = WriteMsg(ARB, "OUTP:STAT ON")

CT14:   nSystErr = vxiClear(COUNTER)
        nSystErr = WriteMsg(COUNTER, "*RST")
        nSystErr = WriteMsg(COUNTER, "*CLS")
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
            TestCT = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "C/T" And OptionStep = LoopStepNo Then
            GoTo CT14
        End If

        frmSTest.proProgress.Value = 55
        LoopStepNo += 1

CT15:   'C/T-15-001
        nSystErr = WriteMsg(COUNTER, "MEAS2:FREQ? 1E6,1")
        dMeasurement = FetchMeasurement(COUNTER, "AC Coupling Verification Channel 2", "95E+4", "105E+4", "Hz", COUNTER, "C/T-15-001")
        If dMeasurement > 1050000.0 Or dMeasurement < 950000.0 Then
            TestCT = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "C/T" And OptionStep = LoopStepNo Then
            GoTo CT15
        End If

        frmSTest.proProgress.Value = 60
        LoopStepNo += 1

CT16:   'C/T-16-001 Time Interval Test
        nSystErr = WriteMsg(ARB, "*RST")
        nSystErr = WriteMsg(ARB, "*CLS")
        nSystErr = WriteMsg(ARB, "MARK:STAT OFF") 'Disable the AFG to output a marker list    
        nSystErr = WriteMsg(ARB, "OUTP:LOAD 50")
        nSystErr = WriteMsg(ARB, "VOLT:UNIT:VOLT VPP")
        nSystErr = WriteMsg(ARB, "FREQ 1E+6")
        nSystErr = WriteMsg(ARB, "FUNC SQU")
        nSystErr = WriteMsg(ARB, "VOLT 1")
        nSystErr = WriteMsg(ARB, "OUTP:FILT:FREQ 10E6")
        nSystErr = WriteMsg(ARB, "OUTP:FILT ON")
        nSystErr = WriteMsg(ARB, "INIT:IMM")
        nSystErr = WriteMsg(ARB, "OUTP:STAT ON")
        Delay(3)

        '** Time Interval Test
        'connect 1 MHz test input to Input 1
        nSystErr = WriteMsg(COUNTER, "*RST")
        Delay(1)
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
            TestCT = FAILED
            If (dMeasurement > 0.0000001) Or (dMeasurement < 0) Then 'Totally inaccurate measurement(on the order of more than 50ns)
                sMessage = "Malfunctioning in slope switching"
            Else
                sMessage = "Probable cause is from the Input Board and the trigger level circuitry"
            End If
            Echo("C/T-16-D01     Time Interval Test Diagnostic")
            Echo("    " & sMessage)
            RegisterFailure(COUNTER, ReturnTestNumber(COUNTER, 16, 1, "D"), dMeasurement, "sec", 0.00000045, 0.00000053, "Time Interval Test Diagnostic" & vbCrLf & sMessage)
            IncStepFailed()
            If (OptionFaultMode = SOFmode) And (OptionMode <> LOSmode) Then
            '=============================================================================================
                MsgBox("Time Interval Test failed." & vbCrLf & "Performing Counter/Timer Adjustment 2 (Input Amplifier Offset)" & vbCrLf & _
                " in the ATS-ViperT Calibration Program may correct this problem.", vbInformation)
            '=============================================================================================
            End If
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "C/T" And OptionStep = LoopStepNo Then
            GoTo CT16
        End If

        frmSTest.proProgress.Value = 65
        LoopStepNo += 1

CT17:   'C/T-17-001
        'This time, turn on X10 attenuation and repeat
        nSystErr = WriteMsg(COUNTER, "INP1:COUP AC;ATT 10")
        nSystErr = WriteMsg(COUNTER, "INP2:COUP AC;ATT 10")
        nSystErr = WriteMsg(ARB, "VOLT 3 VRMS")
        nSystErr = WriteMsg(ARB, "INIT:IMM")
        nSystErr = WriteMsg(ARB, "OUTP:STAT ON")
        Delay(0.5)
        nSystErr = WriteMsg(COUNTER, "MEAS1:TINT?")

        dMeasurement = FetchMeasurement(COUNTER, "Time Interval Test w/ Attenuation", "450E-9", "530E-9", "sec", COUNTER, "C/T-17-001")
        If (dMeasurement > 0.00000053) Or (dMeasurement < 0.00000045) Then
            TestCT = FAILED
            dFH_Meas = dMeasurement
            If (dMeasurement > 0.0000001) Or (dMeasurement < 0) Then 'Totally inaccurate measurement(on the order of more than 50ns)
                sMessage = "Malfunctioning in slope switching"
            Else
                sMessage = "Probable cause is from the Input Board and the trigger level circuitry"
            End If
            Echo("C/T-17-001 Time Interval Test w/ Attenuation Diagnostic")
            Echo("    " & sMessage)
            RegisterFailure(COUNTER, ReturnTestNumber(COUNTER, 17, 1, "D"), dMeasurement, "sec", 0.00000045, 0.00000053, "Time Interval Test w/ Attenuation Diagnostic" & vbCrLf & sMessage)
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "C/T" And OptionStep = LoopStepNo Then
            GoTo CT17
        End If

        frmSTest.proProgress.Value = 70
        LoopStepNo += 1

CT18:   'C/T-18-001
        'This time, measure the negative portion of the 1 MHz input waveform.
        nSystErr = WriteMsg(ARB, "VOLT 1 VPP")
        nSystErr = WriteMsg(ARB, "INIT:IMM")
        nSystErr = WriteMsg(ARB, "OUTP:STAT ON")

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
            TestCT = FAILED
            If (dMeasurement > 0.0000001) Or (dMeasurement < 0) Then 'Totally inaccurate measurement(on the order of more than 50ns)
                sMessage = "Malfunctioning in slope switching"
            Else
                sMessage = "Probable cause is from the Input Board and the trigger level circuitry"
            End If
            Echo("C/T-18-001 Time Interval Test (Negative Edge) Diagnostic")
            Echo("    " & sMessage)
            RegisterFailure(COUNTER, ReturnTestNumber(COUNTER, 18, 1, "D"), dMeasurement, "sec", 0.00000047, 0.00000053, "Time Interval Test (Negative Edge) Diagnostic" & vbCrLf & sMessage)
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "C/T" And OptionStep = LoopStepNo Then
            GoTo CT18
        End If

        frmSTest.proProgress.Value = 75
        LoopStepNo += 1

CT19:   'C/T-19-001
        'This time, turn on X10 attenuation and repeat
        nSystErr = WriteMsg(COUNTER, "INP1:COUP AC;ATT 10")
        nSystErr = WriteMsg(COUNTER, "INP2:COUP AC;ATT 10")
        nSystErr = WriteMsg(ARB, "VOLT 3 VRMS")
        nSystErr = WriteMsg(ARB, "INIT:IMM")
        nSystErr = WriteMsg(ARB, "OUTP:STAT ON")
        Delay(1)
        nSystErr = WriteMsg(COUNTER, "MEAS1:TINT?")

        dMeasurement = FetchMeasurement(COUNTER, "Time Interval Test (Negative Edge w/ Attenuation)", "470E-9", "530E-9", "sec", COUNTER, "C/T-19-001")
        If (dMeasurement > 0.00000053) Or (dMeasurement < 0.00000047) Then
            TestCT = FAILED
            If (dMeasurement > 0.0000001) Or (dMeasurement < 0) Then 'Totally inaccurate measurement(on the order of more than 50ns)
                sMessage = "Malfunctioning in slope switching"
            Else
                sMessage = "Probable cause is from the Input Board and the trigger level circuitry"
            End If
            Echo("C/T-19-001 Time Interval Test (Negative Edge w/ Attenuation) Diagnostic")
            Echo("    " & sMessage)
            RegisterFailure(COUNTER, ReturnTestNumber(COUNTER, 19, 1, "D"), dMeasurement, "sec", 0.00000047, 0.00000053, "Time Interval Test (Negative Edge w/ Attenuation) Diagnostic" & vbCrLf & sMessage)
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "C/T" And OptionStep = LoopStepNo Then
            GoTo CT19
        End If

        frmSTest.proProgress.Value = 80
        LoopStepNo += 1

CT20:   'C/T-20-001
        '** External Arm Input Test: checks the front panel external arm input amplifier circuitry by configuring the arm
        '   to act as the gate for as the gate for measuring a signal input on channel 1

        'The following procedures are performed to make measuremensts using the first positive edge of the arm input
        'to start the aperture gate, and the second positive edge of the arm input stop the gate.
        'The aperture time is therefore the period of the arm input signal: 10ns

        nSystErr = WriteMsg(ARB, "MARK:STAT ON") ' DO NOT turn off, ARB MARKER needed for this test
        nSystErr = WriteMsg(ARB, "Volt 1 VPP")
        nSystErr = WriteMsg(ARB, "INIT:IMM")
        nSystErr = WriteMsg(ARB, "OUTP:STAT ON")

        nSystErr = WriteMsg(COUNTER, "*RST")
        nSystErr = WriteMsg(COUNTER, "*CLS")
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
            TestCT = FAILED
            IncStepFailed()
            'C/T-20-D01
            If (OptionFaultMode = SOFmode) And (OptionMode <> LOSmode) Then
                dFH_Meas = dMeasurement
                'Disconnect P12 Arb Marker Out from P21 C/T ARM IN
                nSystErr = WriteMsg(SWITCH1, "OPEN 1.3001") 'open  S401-1,3  bb
                'Connect P12 Arb Marker Out to P15 SCOPE INPUT 1.
                nSystErr = WriteMsg(SWITCH1, "CLOSE 2.3103") 'close S405-1,5  bb

                nSystErr = WriteMsg(OSCOPE, "*RST")
                nSystErr = WriteMsg(OSCOPE, "*CLS")
                nSystErr = WriteMsg(OSCOPE, "AUT") 'Set Autoscale mode
                Delay(2)
                nSystErr = WriteMsg(OSCOPE, "DIG CHAN1")
                nSystErr = WriteMsg(OSCOPE, "MEAS:SOUR CHAN1")
                nSystErr = WriteMsg(OSCOPE, "MEAS:VAMP?")

                dMeasurement = FetchMeasurement(OSCOPE, "ARB Marker Out Diagnostic Test", "1", "", "V", ARB, "C/T-20-D01")
                If dMeasurement > 1 Then
                    sMessage = "The likely cause of the failure is the Counter/Timer Input Board."
                    Echo("C/T-20-D02     ARB Marker Out Diagnostic Test")
                    Echo("    " & sMessage)
                    RegisterFailure(COUNTER, ReturnTestNumber(COUNTER, 20, 2, "D"), dFH_Meas, "Hz", 950000, 1050000, "ARB Marker Out Diagnostic Test" & vbCrLf & sMessage)
                Else
                    Echo(FormatResults(False, "C/T ARB Marker Out Diagnostic Test", "C/T-20-D01"))
                    RegisterFailure(ARB, "C/T-20-D01", dFH_Meas, "Hz", 950000, 1050000, "ARB Marker Out Diagnostic Test")
                    TestCT = ARB
                End If
                'Disconnect P12 Arb Marker Out from P15 SCOPE INPUT 1.
                nSystErr = WriteMsg(SWITCH1, "OPEN 2.3103") 'open S405-1,5  bb
                'Reconnect P12 Arb Marker Out to P21 C/T ARM IN
                nSystErr = WriteMsg(SWITCH1, "CLOSE 1.3001") 'close  S401-1,3  bb
            End If
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "C/T" And OptionStep = LoopStepNo Then
            GoTo CT20
        End If

        frmSTest.proProgress.Value = 85
        LoopStepNo += 1

CT21:   'C/T-21-001
        '************************************Input Channel 2 Test***************************************
        '** The same Auto Frequency Measurement Test is repeated to see if Input 2 Channel functionally works fine
        'Connect a 1-MHz timebase standard to the Int/Ext ref. BNC.
        nSystErr = vxiClear(COUNTER)
        nSystErr = WriteMsg(ARB, "*RST")
        nSystErr = WriteMsg(ARB, "*CLS")
        nSystErr = WriteMsg(ARB, "MARK:STAT OFF") 'Disable the AFG to output a marker list    
        nSystErr = WriteMsg(ARB, "FREQ 1E+6")
        nSystErr = WriteMsg(ARB, "FUNC SIN")
        nSystErr = WriteMsg(ARB, "VOLT 3VPP")
        nSystErr = WriteMsg(ARB, "OUTP:LOAD 50")
        nSystErr = WriteMsg(ARB, "VOLT:OFFS 2")
        nSystErr = WriteMsg(ARB, "OUTP:FILT:FREQ 10E6")
        nSystErr = WriteMsg(ARB, "OUTP:FILT ON")
        nSystErr = WriteMsg(ARB, "INIT:IMM")
        nSystErr = WriteMsg(ARB, "OUTP:STAT ON")
        Delay(0.5)

        ' Disconnect P10 Arb output from P19 C/T INPUT 1.
        nSystErr = WriteMsg(SWITCH1, "OPEN  2.3001") 'open  S404-1,3  bb
        ' Connect P10 Arb output to P20 C/T INPUT 2.
        nSystErr = WriteMsg(SWITCH1, "CLOSE 2.3002") 'close S404-1,4  bb

        nSystErr = vxiClear(COUNTER)
        nSystErr = WriteMsg(COUNTER, "*RST")
        nSystErr = WriteMsg(COUNTER, "*CLS")
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
            TestCT = FAILED
            'C/T-21-001
            sMessage = "Probable cause is from the auto-triggering circuitry"
            Echo("C/T-21-D01 Counter Auto Frequency Measurement Test Channel 2 Diagnostic")
            Echo("    " & sMessage)
            RegisterFailure(COUNTER, ReturnTestNumber(COUNTER, 21, 1, "D"), dMeasurement, "Hz", 900000, 1050000, "Counter Auto Frequency Measurement Test Channel 2 Diagnostic" & vbCrLf & sMessage)
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "C/T" And OptionStep = LoopStepNo Then
            GoTo CT21
        End If

        frmSTest.proProgress.Value = 90
        LoopStepNo += 1

CT22:   'C/T-22-001
        nSystErr = vxiClear(COUNTER)
        nSystErr = WriteMsg(COUNTER, "*RST")
        nSystErr = WriteMsg(COUNTER, "*CLS")
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
        nSystErr = WriteMsg(ARB, "INIT:IMM")
        nSystErr = WriteMsg(ARB, "OUTP:STAT ON")
        Delay(1)
        nSystErr = WriteMsg(COUNTER, "MEAS2:AC?")
        Delay(2)

        dMeasurement = FetchMeasurement(COUNTER, "AC RMS Voltage Channel 2 w/ Attenuation", "0.2", "0.4", "Volts", COUNTER, "C/T-22-001")
        If dMeasurement > 0.4 Or dMeasurement < 0.2 Then
            TestCT = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "C/T" And OptionStep = LoopStepNo Then
            GoTo CT22
        End If

        frmSTest.proProgress.Value = 95
        LoopStepNo += 1

CT23:   'C/T-23-001
        nSystErr = WriteMsg(COUNTER, "INP2:ATT 1")
        nSystErr = WriteMsg(COUNTER, "MEAS2:AC?")
        Delay(1)

        dMeasurement = FetchMeasurement(COUNTER, "AC RMS Voltage Channel 2", "2", "4", "Volts", COUNTER, "C/T-23-001")
        If dMeasurement > 4 Or dMeasurement < 2 Then
            TestCT = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        frmSTest.proProgress.Value = 100

        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "C/T" And OptionStep = LoopStepNo Then
            GoTo CT23
        End If

TestComplete:
        frmSTest.cmdAbort.Text = "Abort Test"
        frmSTest.cmdPause.Text = "Pause Test"
        nSystErr = WriteMsg(SWITCH1, "RESET") ' open all switches
        nSystErr = WriteMsg(COUNTER, "*RST")
        nSystErr = WriteMsg(ARB, "*RST")
        nSystErr = WriteMsg(ARB, "*CLS")
        nSystErr = WriteMsg(ARB, "MARK:STAT OFF") 'Disable the AFG to output a marker list    
        nSystErr = WriteMsg(OSCOPE, "*RST")
        nSystErr = WriteMsg(OSCOPE, "*CLS")

        If AbortTest = True Then
            If TestCT = FAILED Then
                ReportFailure(COUNTER)
            Else
                ReportUnknown(COUNTER)
                TestCT = -99

            End If
            sMsg = vbCrLf
            sMsg &= "      **********************************************" & vbCrLf
            sMsg &= "      *         Counter/Timer tests aborted!       *" & vbCrLf
            sMsg &= "      **********************************************" & vbCrLf
            Echo(sMsg)
        ElseIf TestCT = PASSED Then
            ReportPass(COUNTER)
        ElseIf TestCT = FAILED Then
            ReportFailure(COUNTER)
        Else
            ReportUnknown(COUNTER)
        End If
    End Function





End Module