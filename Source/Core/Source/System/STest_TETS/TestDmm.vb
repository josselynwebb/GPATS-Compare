'Option Strict Off
Option Explicit On

Public Module modDMM

    '************************************************************
    '* Nomenclature   : ATS-TETS SYSTEM SELF TEST               *
    '*                  RACAL 4152A DMM Test                    *
    '* Version        : 2.0                                     *
    '* Last Update    : Apr 1, 2017                             *
    '* Purpose        : This module contains code for the DMM   *
    '*                  Self Test                               *
    '************************************************************

    Dim S As String = ""

    Function TestDmm() As Integer
        'DESCRIPTION:
        '   This routine runs the DMM OVP and returns PASSED or FAILED
        'RETURNS:
        '   PASSED if the DMM OVP test passes or FAILED if a failure occurs
        Dim SelfTest As String = ""
        Dim dMeasurement As Double
        Dim count As Integer
        Dim StrMeasurement As String = ""
        Dim LoopStepno As Integer
        
        HelpContextID = 1180

        TestDMM = UNTESTED
        If AbortTest = True Then GoTo TestComplete
        AbortTest = False

        'Digital Multi-Meter Test Title block
        EchoTitle(InstrumentDescription(DMM), True)
        EchoTitle("Digital Multi-Meter Test", False)
        frmSTest.proProgress.Maximum = 100
        frmSTest.proProgress.Value = 1

        MsgBox("Remove all cable connections from the SAIF.")

        nSystErr = WriteMsg(DMM, "*RST")
        nSystErr = WriteMsg(DMM, "*CLS")
        nSystErr = WriteMsg(SWITCH1, "RESET") ' open all switches
        Delay(1)

        '** Perform a DMM Self Test
        frmSTest.proProgress.Value = 10
        LoopStepno = 1
        TestDmm = PASSED

DMM1:   'DMM-01-001
        Echo("DMM-01-001 DMM Built-In Test (BIT takes 12 seconds)")
        nSystErr = WriteMsg(DMM, "*TST?")
        nSystErr = WaitForResponse(DMM, 0.22)
        nSystErr = ReadMsg(DMM, SelfTest)
        If Val(SelfTest) <> 0 Or nSystErr <> 0 Then
            Echo(FormatResults(False, "DMM BIT Test" & StripNullCharacters(SelfTest), "DMM-01-001"))
            RegisterFailure(DMM, "DMM-01-001", , , , , sGuiLabel(DMM) & " FAILED Built-in Test")
            TestDmm = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
            Echo(FormatResults(True, "DMM Built-In Test"))
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "DMM" And OptionStep = LoopStepno Then
            GoTo DMM1
        End If
        frmSTest.proProgress.Value = 30
        LoopStepno += 1

Step1:
        S = "Use cables W24, W28 (OBSERVE POLARITY) to connect the "
        S &= "DMM inputs and the DMM sense lines to the Utility Loads as follows: " & vbCrLf & vbCrLf
        S &= "1. Connect  DMM Input (HI/LO)   to   Utility Loads (HI/LO). " & vbCrLf
        S &= "2. Connect  DMM Sense (HI/LO)   to   Utility Loads (HI/LO)."
        DisplaySetup(S, "TETS DFW_ULD.jpg", 1, True, 1, 3)
        If AbortTest = True Then GoTo TestComplete
Step2:
        S = "Use cable W15 to connect" & vbCrLf & vbCrLf
        S &= "   DMM Trig In   to   ARB output."
        DisplaySetup(S, "TETS AOP_DTI.jpg", 1, True, 2, 3)
        If AbortTest = True Then GoTo TestComplete
        If GoBack = True Then GoTo Step1
Step3:
        S = "Use cable W16 to connect" & vbCrLf & vbCrLf
        S &= "   DMM VM Comp Out   to   Scope Input 1."
        DisplaySetup(S, "TETS DVM_SI1.jpg", 1, True, 3, 3)
        If AbortTest = True Then GoTo TestComplete
        If GoBack = True Then GoTo Step2

DMM2:   'DMM-02-001
        '** Test DMM Inputs and sense lines
        nSystErr = WriteMsg(ARB, "*RST")
        nSystErr = WriteMsg(ARB, "*CLS")
        nSystErr = WriteMsg(ARB, "MARK:STAT OFF") 'Disable the AFG to output a marker list    
        nSystErr = WriteMsg(ARB, "OUTP:LOAD 50")
        nSystErr = WriteMsg(ARB, "VOLT:UNIT:VOLT VPP")
        nSystErr = WriteMsg(ARB, "VOLT 2.4")
        nSystErr = WriteMsg(ARB, "VOLT:OFFS 1.23")
        nSystErr = WriteMsg(ARB, "FREQ 100")
        nSystErr = WriteMsg(ARB, "FUNC SQU")

        nSystErr = WriteMsg(DMM, "*RST")
        nSystErr = WriteMsg(DMM, "*CLS")
        nSystErr = WriteMsg(DMM, "CONF:FRES 30") ' set to 4 wire resistance measurement
        nSystErr = WriteMsg(DMM, "INIT;FETCH?")

        dMeasurement = FetchMeasurement(DMM, "DMM Four Wire Resistance Test", "12", "17", "Ohms", DMM, "DMM-02-001")        '<-Modified by           for DR# 128 on 02/15/2001
        If dMeasurement > 17 Or dMeasurement < 12 Then '<-Modified by           for DR# 128 on 02/15/2001
            TestDmm = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If

        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "DMM" And OptionStep = LoopStepno Then
            GoTo DMM2
        End If
        frmSTest.proProgress.Value = 50
        LoopStepno += 1

DMM3:   'DMM-03-001
        '** Test DMM External Trigger Input
        nSystErr = WriteMsg(DMM, "TRIG:SLOP POS")
        nSystErr = WriteMsg(DMM, "TRIG:SOUR EXT")
        nSystErr = WriteMsg(ARB, "INIT:IMM")
        nSystErr = WriteMsg(ARB, "OUTP:STAT ON")
        nSystErr = WriteMsg(DMM, "INIT;FETCH?")

        dMeasurement = FetchMeasurement(DMM, "Input Trigger Test", "12", "17", "Ohms", DMM, "DMM-03-001")        '<-Modified by           for DR# 128 on 02/15/2001
        If dMeasurement >= 9.0E+38 Then
            'DMM-03-D01
            TestDmm = FAILED
            IncStepFailed()
            dFH_Meas = dMeasurement
            ' If No Trigger then check to see if problem was in ARB by triggering with the FG
            nSystErr = WriteMsg(FGEN, "*RST")
            nSystErr = WriteMsg(FGEN, "*CLS")
            nSystErr = WriteMsg(FGEN, "FUNC:SHAP SQU")
            nSystErr = WriteMsg(FGEN, "FREQ 100")
            nSystErr = WriteMsg(FGEN, "VOLT 2.4")
            nSystErr = WriteMsg(FGEN, "VOLT:OFFS 1.245")

            S = "Move cable W15 connection from" & vbCrLf & vbCrLf
            S &= "    ARB Out   to   FG Out."
            DisplaySetup(S, "TETS DTI_FGO.jpg", 1)
            If AbortTest = True Then GoTo testcomplete
            nSystErr = WriteMsg(FGEN, "OUTP ON")
            nSystErr = WriteMsg(FGEN, "INIT")

            nSystErr = vxiClear(DMM)

            nSystErr = WriteMsg(DMM, "*RST")
            nSystErr = WriteMsg(DMM, "*CLS")
            nSystErr = WriteMsg(DMM, "CONF:FRES 30") ' 4 wire resistance measurement
            nSystErr = WriteMsg(DMM, "TRIG:SLOP POS")
            nSystErr = WriteMsg(DMM, "TRIG:SOUR EXT")
            nSystErr = WriteMsg(DMM, "INIT;FETCH?")
            dMeasurement = FetchMeasurement(DMM, "Input Trigger Diagnostic", "12", "15", "Ohms", DMM, "DMM-03-D01")            '<-Modified by           for DR# 128 on 02/15/2001
            'DMM-03-D01
            If dMeasurement > 9.0E+38 Then
                'If no measurement here, problem must be from the DMM . . .
            Else
                'DMM-03-D03
                ' . . . other wise it must be the ARB
                Echo(FormatResults(False, "DMM Input Trigger Diagnostic", "DMM-03-D01"))
                Echo("Cannot complete DMM Self-Test due to Arbitrary Waveform Generator Failure.")
                MsgBox("Cannot complete DMM Self-Test due to Arbitrary Waveform Generator Failure.  " & "Read 'Details' box or System Log file for more information.")
                TestDmm = ARB
                RegisterFailure(ARB, "DMM-03-D01", dFH_Meas, "Ohms", 12, 15, "DMM Self-Test not completed due to " & sGuiLabel(ARB) & " Failure.")
            End If
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        ElseIf (dMeasurement > 17) Or (dMeasurement < 12) Then            '<-Modified by           for DR# 128 on 02/15/2001
            TestDmm = FAILED
            'DMM is not measuring properly
            Echo(FormatResults(False, "DMM Input Trigger Diagnostic", "DMM-03-D01"))
            Echo("     The DMM is not measuring properly")
            RegisterFailure(DMM, "DMM-03-D01", dFH_Meas, "Ohms", 12, 15, "DMM is not measuring properly")
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "DMM" And OptionStep = LoopStepno Then
            GoTo DMM3
        End If
        frmSTest.proProgress.Value = 60
        LoopStepno += 1

DMM4:   'DMM-04-001
        'Test VM Complete Line
        nSystErr = vxiClear(OSCOPE)
        nSystErr = WriteMsg(OSCOPE, "*RST")
        nSystErr = WriteMsg(OSCOPE, "*CLS")
        nSystErr = vxiClear(DMM)
        nSystErr = WriteMsg(DMM, "*RST")
        nSystErr = WriteMsg(DMM, "*CLS")
        Delay(1)

        ' setup scope to measure the first neg pulse width
        nSystErr = WriteMsg(OSCOPE, "MEAS:SOUR CHAN1")
        nSystErr = WriteMsg(OSCOPE, "CHAN1:RANG 10")
        nSystErr = WriteMsg(OSCOPE, "CHAN1:OFFS 2.5")
        nSystErr = WriteMsg(OSCOPE, "TIM:RANG 20E-6")
        nSystErr = WriteMsg(OSCOPE, "TRIG:SOUR CHAN1")
        nSystErr = WriteMsg(OSCOPE, "TRIG:SLOP NEG")
        nSystErr = WriteMsg(OSCOPE, "TRIG:LEV 0.8")
        nSystErr = WriteMsg(OSCOPE, "TIM:MODE TRIG")
        nSystErr = WriteMsg(OSCOPE, "CHAN1:COUP DCF")
        nSystErr = WriteMsg(OSCOPE, "DIG CHAN1")
        nSystErr = WriteMsg(OSCOPE, "MEAS:NWID?")

        ' generate a VMCOMP signal
        nSystErr = WriteMsg(DMM, "CONF:VOLT:AC")
        nSystErr = WriteMsg(DMM, "INIT;FETCH?")
        Delay(0.1)
        ' read the data but don't use it
        nSystErr = ReadMsg(DMM, StrMeasurement)

        ' setup the scope to measure the next negative pulse width
        nSystErr = WriteMsg(OSCOPE, "MEAS:SOUR CHAN1")
        nSystErr = WriteMsg(OSCOPE, "CHAN1:RANG 10")
        nSystErr = WriteMsg(OSCOPE, "CHAN1:OFFS 2.5")
        nSystErr = WriteMsg(OSCOPE, "TIM:RANG 20E-6")
        nSystErr = WriteMsg(OSCOPE, "TRIG:SOUR CHAN1")
        nSystErr = WriteMsg(OSCOPE, "TRIG:SLOP NEG")
        nSystErr = WriteMsg(OSCOPE, "TRIG:LEV 0.8")
        nSystErr = WriteMsg(OSCOPE, "TIM:MODE TRIG")
        nSystErr = WriteMsg(OSCOPE, "CHAN1:COUP DCF")
        nSystErr = WriteMsg(OSCOPE, "DIG CHAN1")

        nSystErr = WriteMsg(OSCOPE, "MEAS:NWID?")

        nSystErr = WriteMsg(DMM, "CONF:VOLT:AC")
        nSystErr = WriteMsg(DMM, "INIT;FETCH?")
        Delay(0.1)

        ' fetch and test the negative pulse width
        dMeasurement = FetchMeasurement(OSCOPE, "VM Complete Test", "1E-6", "4E-6", "S", OSCOPE, "DMM-04-001")
        If (dMeasurement < 0.000001) Or (dMeasurement > 0.000004) Then
            'DMM-04-D01
            TestDmm = FAILED
            IncStepFailed()
            dFH_Meas = dMeasurement
            'Check to see if the failure is in the scope
            nSystErr = vxiClear(COUNTER)
            nSystErr = WriteMsg(COUNTER, "*RST")
            nSystErr = WriteMsg(COUNTER, "*CLS")
            nSystErr = WriteMsg(COUNTER, "INP1:IMP 50")
            nSystErr = WriteMsg(COUNTER, "INP2:IMP 50")
            nSystErr = WriteMsg(COUNTER, "CONF1:NWID 0.8V, .000002")
            nSystErr = WriteMsg(COUNTER, "SENS1:EVEN:HYST MAX")
            S = "Move cable W16 connection from" & vbCrLf & vbCrLf
            S &= "    SCOPE INPUT 1   to   Counter/Timer Input 1."
            DisplaySetup(S, "TETS DVM_CI1.jpg", 1)
            If AbortTest = True Then GoTo testcomplete
            nSystErr = WriteMsg(COUNTER, "INIT;FETCH?")
            For count = 1 To 10    'Generate 10 VM Complete pulses
                nSystErr = WriteMsg(DMM, "INIT;FETCH?")
                nSystErr = ReadMsg(DMM, StrMeasurement)
            Next count
            dMeasurement = FetchMeasurement(COUNTER, "VM Complete Diagnostic", "1E-6", "4E-6", "S", COUNTER, "DMM-04-D01")
            If (dMeasurement < 0.000001) Or (dMeasurement > 0.000004) Then
                'If there is a bad measurement here, the problem is from the DMM . . .
                Echo(FormatResults(False, "DMM VM Complete Diagnostic", "DMM-04-D01"))
                RegisterFailure(DMM, "DMM-04-D01", dFH_Meas, "S", 0.000001, 0.000004, "VM Complete Diagnostic")
            Else
                ' . . . otherwise it must be the SCOPE
                Echo(FormatResults(False, "DMM VM Complete Diagnostic", "DMM-04-D01"))
                Echo(sGuiLabel(OSCOPE) & " FAILED, the " & sGuiLabel(DMM) & " Self-Test can not be Completed.")
                RegisterFailure(OSCOPE, "DMM-04-D01", dFH_Meas, "S", 0.000001, 0.000004, sGuiLabel(OSCOPE) & " FAILED, the " & sGuiLabel(DMM) & " Self-Test can not be Completed.")
                TestDmm = OSCOPE
            End If
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "DMM" And OptionStep = LoopStepno Then
            GoTo DMM4
        End If
        frmSTest.proProgress.Value = 80
        LoopStepno += 1

        'Test Current Measurement Capability
Step1b:
        S = "Disconnect cables W24 and W28. " & vbCrLf
        S &= "Use cable W24 (OBSERVE POLARITY) to connect" & vbCrLf & vbCrLf
        S &= "    DMM-Current (YEL)  and  DMM-LO (BLK)   to" & vbCrLf
        S &= "    DC1-HI (RED)            and  LOADS-HI (RED)." & vbCrLf
        DisplaySetup(S, "TETS DC1_DIC.jpg", 1, True, 1, 2)
        If AbortTest = True Then GoTo testcomplete
Step2b:
        S = "Use cable W28 (OBSERVE POLARITY) to short" & vbCrLf & vbCrLf
        S &= "   DC1-LO(BLK)   and   LOADS-LO(BLK)." & vbCrLf
        DisplaySetup(S, "TETS DMMCUR2.jpg", 1, True, 2, 2)
        If AbortTest = True Then GoTo testcomplete
        If GoBack = True Then GoTo Step1b

DMM5:   'DMM-05-001
        'Test Current Measurement Capability
        nSystErr = vxiClear(ARB)
        nSystErr = WriteMsg(ARB, "*RST")
        nSystErr = WriteMsg(ARB, "*CLS")
        nSystErr = WriteMsg(ARB, "MARK:STAT OFF") 'Disable the AFG to output a marker list 
        nSystErr = WriteMsg(ARB, "OUTP:LOAD 50")

        'Set DC Supply 1 to 26 volts
        CommandSetOptions(1, False, True, True, True)
        Delay(0.3)
        CommandSetCurrent(1, 4) ' was 2.5
        Delay(0.3)
        CommandSetVoltage(1, 26)


        nSystErr = WriteMsg(DMM, "*RST")
        nSystErr = WriteMsg(DMM, "*CLS")
        Delay(1)
        nSystErr = WriteMsg(DMM, "MEAS:CURR:DC?")
        nSystErr = WaitForResponse(DMM, 0.1)

        dMeasurement = FetchMeasurement(DMM, "Current Measurement Test", "1.30", "2.00", "A", DMM, "DMM-05-001")
        If dMeasurement < 1.3 Or dMeasurement > 2.0 Then
            TestDmm = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        If OptionMode = LOSmode And OptionTestName = "DMM" And OptionStep = LoopStepno Then
            GoTo DMM5
        End If
        CommandSupplyReset(1)


TestComplete:

        frmSTest.proProgress.Value = 100
        frmSTest.proProgress.Maximum = 100
        Application.DoEvents()
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
        nSystErr = vxiClear(DMM)
        nSystErr = WriteMsg(DMM, "*RST")
        nSystErr = WriteMsg(DMM, "*CLS")
        Application.DoEvents()
        If AbortTest = True Then
            If TestDmm = FAILED Then
                ReportFailure(DMM)
            Else
                ReportUnknown(DMM)
                TestDmm = UNTESTED
            End If
            sMsg = vbCrLf
            sMsg &= "      **********************************************" & vbCrLf
            sMsg &= "      *              DMM tests aborted!            *" & vbCrLf
            sMsg &= "      **********************************************" & vbCrLf
            Echo(sMsg)
        ElseIf TestDmm = PASSED Then
            ReportPass(DMM)
        ElseIf TestDmm = FAILED Then
            ReportFailure(DMM)
        Else
            ReportUnknown(DMM)
        End If
    End Function


End Module
