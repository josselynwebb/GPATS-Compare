'Option Strict Off
Option Explicit On

Imports System.Windows.Forms

Public Module modDMMViperTSaif


    '=========================================================
    '**************************************************************
    '* Nomenclature   : TETS SYSTEM SELF TEST                     *
    '*                  RACAL 4152A DMM Test                      *
    '* Version        : 2.0                                       *
    '* Last Update    : Apr 1, 2017                               *
    '* Purpose        : This module contains code for the DMM     *
    '*                  Self Test         (was E1412A)            *
    '*                  6.5 Digit DMM Racal Instruments           *
    '*                  VXIbus message based slave device.        *
    '**************************************************************
    'Note: The obsolete HP1412A was replaced by Racal Instruments 4152A



    Function TestDMM_VT() As Integer
        'DESCRIPTION:
        '   This routine runs the DMM OVP and returns PASSED or FAILED
        'RETURNS:
        '   PASSED if the DMM OVP test passes or FAILED if a failure occurs
        Dim x As DialogResult
        Dim SelfTest As String = ""
        Dim count As Short
        Dim StrMeasurement As String = ""
        Dim S As String
        Dim LoopStepno As Integer
        HelpContextID = 1180

        TestDMM_VT = UNTESTED
        If AbortTest = True Then GoTo TestComplete
        AbortTest = False

        'Digital Multi-Meter Test Title block
        EchoTitle(InstrumentDescription(DMM) & " ", True)
        EchoTitle("Digital Multi-Meter Test", False)
        frmSTest.proProgress.Maximum = 100
        frmSTest.proProgress.Value = 1

InstallW201:
        If RunningEndToEnd = False And FirstPass = True Then
            x = MsgBox("Is the W201 cable installed?", MsgBoxStyle.YesNo)
            If x <> DialogResult.Yes Then
                S = "Remove all cables/adapters from the SAIF." & vbCrLf
                S += "Connect W201 cable to SAIF as follows:" & vbCrLf
                S += " 1. P1  to SAIF J1 connector." & vbCrLf
                S += " 2. P4  to SAIF FG OUTPUT." & vbCrLf
                S += " 3. P10 to SAIF ARB OUTPUT." & vbCrLf
                S += " 4. P15 to SAIF SCOPE INPUT 1." & vbCrLf
                S += " 5. P19 to SAIF C/T INPUT 1." & vbCrLf
                S += " 6. P22 to SAIF DMM TRIG IN." & vbCrLf
                S += " 7. P23 to SAIF DMM VCOMP OUT."
                DisplaySetup(S, "ST-W201-1.jpg", 7)
                If AbortTest = True Then GoTo TestComplete
            End If

        ElseIf RunningEndToEnd = True Then
Step1:
            'Install the W201 cable
            S = "Remove all cables/adapters from the SAIF." & vbCrLf
            S += "Connect W201 cable to SAIF as follows:" & vbCrLf
            S += "1.  P1  to SAIF J1 connector." & vbCrLf
            S += "2.  P2  to SAIF RFMEAS TRIG." & vbCrLf
            S += "3.  P3  to SAIF 10 MHZ REF." & vbCrLf
            S += "4.  P4  to SAIF FG OUTPUT." & vbCrLf
            S += "5.  P5  to SAIF FG TRIG/PLL IN." & vbCrLf
            S += "6.  P6  to SAIF FG SYNC OUT." & vbCrLf
            S += "7.  P7  to SAIF FG CLOCK IN." & vbCrLf
            S += "8.  P8  to SAIF FG PM IN." & vbCrLf
            S += "9.  P9  to SAIF ARB OUTPUT/7." & vbCrLf
            S += "10. P10 to SAIF ARB OUTPUT." & vbCrLf
            S += "11. P11 to SAIF ARB START ARM IN." & vbCrLf
            S += "12. P12 to SAIF ARB MARKER OUT." & vbCrLf
            S += "13. P13 to SAIF ARB REF/SMPL IN." & vbCrLf
            S += "14. P14 to SAIF ARB STOP TRIG/FSK/GATE IN." & vbCrLf
            S += "15. P15 to SAIF SCOPE INPUT 1." & vbCrLf
            S += "16. P16 to SAIF SCOPE INPUT 2." & vbCrLf
            S += "17. P17 to SAIF SCOPE TRIG IN." & vbCrLf
            S += "18. P18 to SAIF SCOPE COMP CAL." & vbCrLf
            S += "19. P19 to SAIF C/T INPUT 1." & vbCrLf
            S += "20. P20 to SAIF C/T INPUT 2." & vbCrLf
            S += "21. P21 to SAIF C/T ARM IN." & vbCrLf
            S += "22. P22 to SAIF DMM TRIG IN." & vbCrLf
            S += "23. P23 to SAIF DMM VCOMP OUT."
            DisplaySetup(S, "ST-W201-1.jpg", 23, True, 1, 3)
            If AbortTest = True Then GoTo TestComplete

Step2:
            'Install the W204 cable
            S = "Install the W204 cable as follows:" & vbCrLf
            S += "1. Connect W204-P1 cable onto the secondary chassis S/R I/O connector. " & vbCrLf
            S += "2. Connect W204-P2 cable onto the SAIF J5 connector. " & vbCrLf
            DisplaySetup(S, "ST-W204-1.jpg", 2, True, 2, 3)
            If GoBack = True Then GoTo Step1
            If AbortTest = True Then GoTo TestComplete

Step3:
            'Install the SP2,SP3,SP4 adapters
            S = "Install the SP2, SP3, SP4 and W206 as follows:" & vbCrLf
            S += "1. Connect SP2 adapter to J10 of the Docking Station." & vbCrLf
            S += "2. Connect SP3 adapter to J5 of the Docking Station." & vbCrLf
            S += "3. Connect SP4 adapter to J6 of the Docking Station." & vbCrLf
            S += "4. Connect W206 (93006H6861) cable from J15 (GIGABIT) to J16 (LAN) on the Docking Station." & vbCrLf
            DisplaySetup(S, "ST-SP234-1.jpg", 4, True, 3, 3)
            If AbortTest = True Then GoTo TestComplete
            If GoBack = True Then GoTo Step2

        End If

        'Add code here to check and verify that the W201 cable is installed onto the SAIF
        ' J1 connector and not the SP1 adapter.

        'Connect the DMM inputs to the Loads inputs.
        nSystErr = WriteMsg(SWITCH1, "CLOSE 1.0000") 'close S101-1,2(3,4)   bb
        Delay(1)

        nSystErr = WriteMsg(DMM, "*RST;*CLS")
        nSystErr = WriteMsg(DMM, "MEAS:RES? 50,MAX")
        nSystErr = ReadMsg(DMM, StrMeasurement)
        If Strings.Left(StrMeasurement, 1) = "-" Then ' abs(sMeasString)
            StrMeasurement = Mid(StrMeasurement, 2)
        End If
        dMeasurement = CDbl(StrMeasurement)

        If dMeasurement > 30 Then
            x = MsgBox("Are you sure that you installed the W201 cable on the J1 connector?", MsgBoxStyle.YesNoCancel)
            If x = DialogResult.Cancel Then
                AbortTest = True
                GoTo TestComplete
            ElseIf x = DialogResult.No Then
                GoTo InstallW201
            End If
        End If


        TestDMM_VT = PASSED
        nSystErr = WriteMsg(DMM, "*RST;*CLS")
        nSystErr = WriteMsg(SWITCH1, "RESET") ' open all switches
        Delay(1)

        '** Perform a DMM Self Test
        frmSTest.proProgress.Value = 10
        LoopStepno = 1

DMM1:   'DMM-01-001
        Echo("DMM-01-001 DMM Built-In Test (BIT takes 12 seconds)")
        nSystErr = WriteMsg(DMM, "*TST?")
        WaitForResponse(DMM, 0.1)
        nSystErr = ReadMsg(DMM, SelfTest)

        If Val(SelfTest) <> 0 Or nSystErr <> 0 Then
            Echo(FormatResults(False, "DMM BIT Test" + StripNullCharacters(SelfTest), "DMM-01-001"))
            RegisterFailure(DMM, "DMM-01-001", , , , , sGuiLabel(DMM) & " FAILED Built-in Test")
            TestDMM_VT = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
            Echo(FormatResults(True, "DMM Built-In Test"))
        End If
        Application.Doevents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "DMM" And OptionStep = LoopStepno Then
            GoTo DMM1
        End If
        LoopStepno += 1
        frmSTest.proProgress.Value = 30

DMM2:   'DMM-02-001
        '** Test DMM Inputs and sense lines
        nSystErr = WriteMsg(ARB, "*RST;*CLS")
        nSystErr = WriteMsg(ARB, "VOLT:UNIT:VOLT VPP")
        nSystErr = WriteMsg(ARB, "VOLT 2.4")
        nSystErr = WriteMsg(ARB, "VOLT:OFFS 1.23")
        nSystErr = WriteMsg(ARB, "FREQ 100")
        nSystErr = WriteMsg(ARB, "FUNC SQU")

        'Connect the DMM inputs and the DMM sense lines to the Loads inputs.
        nSystErr = WriteMsg(SWITCH1, "CLOSE 1.0000,1004,1005") 'close S101-1,2(3,4), S301-9,10 and S301-11,12   bb
        'Connect the ARB output to the DMM Trig In
        nSystErr = WriteMsg(SWITCH1, "CLOSE 2.1008") 'close S301-113,114   bb
        'Connect the DMM VM Comp Out to Scope Input 1
        nSystErr = WriteMsg(SWITCH1, "CLOSE 2.3101") 'close S405-1,3       bb
        Delay(1)

        nSystErr = WriteMsg(DMM, "*RST;*CLS")
        nSystErr = WriteMsg(DMM, "CONF:FRES 30") ' set to 4 wire resistance measurement
        nSystErr = WriteMsg(DMM, "INIT;FETCH?")

        dMeasurement = FetchMeasurement(DMM, "DMM Four Wire Resistance Test", "12", "17", "Ohms", DMM, "DMM-02-001")        '<-Modified by           for DR# 128 on 02/15/2001
        If dMeasurement > 17 Or dMeasurement < 12 Then '<-Modified by           for DR# 128 on 02/15/2001
            TestDMM_VT = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        Application.Doevents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "DMM" And OptionStep = LoopStepNo Then
            GoTo DMM2
        End If
        frmSTest.proProgress.Value = 50
        LoopStepno += 1


DMM3:   'DMM-03-001
        '** Test DMM External Trigger Input
        nSystErr = WriteMsg(DMM, "TRIG:SLOP POS")
        nSystErr = WriteMsg(DMM, "TRIG:SOUR EXT")
        nSystErr = WriteMsg(ARB, "INIT")
        nSystErr = WriteMsg(DMM, "INIT;FETCH?")

        dMeasurement = FetchMeasurement(DMM, "Input Trigger Test", "12", "17", "Ohms", DMM, "DMM-03-001")        '<-Modified by           for DR# 128 on 02/15/2001
        If dMeasurement >= 9.0E+38 Then
            'DMM-03-D01
            IncStepFailed()
            dFH_Meas = dMeasurement
            ' If No Trigger then check to see if problem was in ARB by triggering with the FG
            nSystErr = WriteMsg(FGEN, "*RST;*CLS")
            nSystErr = WriteMsg(FGEN, "FUNC:SHAP SQU")
            nSystErr = WriteMsg(FGEN, "FREQ 100")
            nSystErr = WriteMsg(FGEN, "VOLT 2.4")
            nSystErr = WriteMsg(FGEN, "VOLT:OFFS 1.245")

            'Move the W15 cable connection from ARB Out to FG Out.
            nSystErr = WriteMsg(SWITCH1, "OPEN 2.1008") 'open S301-113,114   ARB-OUT (P10) TO DMM TRIG IN (P22)
            nSystErr = WriteMsg(SWITCH1, "CLOSE 2.1009") 'open S301-115,116  FG-OUT (P4) TO DMM TRIG IN (P22)

            nSystErr = WriteMsg(FGEN, "OUTP ON")
            nSystErr = WriteMsg(FGEN, "INIT")

            nSystErr = vxiClear(DMM)

            nSystErr = WriteMsg(DMM, "*RST;*CLS")
            nSystErr = WriteMsg(DMM, "CONF:FRES 30") ' 4 wire resistance measurement
            nSystErr = WriteMsg(DMM, "TRIG:SLOP POS")
            nSystErr = WriteMsg(DMM, "TRIG:SOUR EXT")
            nSystErr = WriteMsg(DMM, "INIT;FETCH?")
            dMeasurement = FetchMeasurement(DMM, "Input Trigger Diagnostic", "12", "15", "Ohms", DMM, "DMM-03-D01")            '<-Modified by           for DR# 128 on 02/15/2001
            'DMM-03-D01
            If dMeasurement > 9.0E+38 Then
                'If no measurement here, problem must be from the DMM . . .
                TestDMM_VT = FAILED
            Else
                ' . . . other wise it must be the ARB
                Echo(FormatResults(False, "DMM Input Trigger Diagnostic", "DMM-03-D01"))
                Echo("Cannot complete DMM Self-Test due to Arbitrary Function Generator Failure.")
                MsgBox("Cannot complete DMM Self-Test due to Arbitrary Function Generator Failure.  " & "Read 'Details' box or System Log file for more information.")
                TestDMM_VT = ARB
                RegisterFailure(ARB, "DMM-03-D01", dFH_Meas, "Ohms", 12, 15, "DMM Self-Test not completed due to " & sGuiLabel(ARB) & " Failure.")
            End If
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        ElseIf (dMeasurement > 17) Or (dMeasurement < 12) Then            '<-Modified by           for DR# 128 on 02/15/2001
            TestDMM_VT = FAILED
            'DMM is not measuring properly
            Echo(FormatResults(False, "DMM Input Trigger Diagnostic", "DMM-03-D01"))
            Echo("     The DMM is not measuring properly")
            RegisterFailure(DMM, "DMM-03-D01", dFH_Meas, "Ohms", 12, 15, "DMM is not measuring properly")
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        Application.doevents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "DMM" And OptionStep = LoopStepno Then
            GoTo DMM3
        End If
        frmSTest.proProgress.Value = 60
        LoopStepno += 1

DMM4:   'DMM-04-001
        'Test VM Complete Line
        nSystErr = vxiClear(OSCOPE)
        nSystErr = WriteMsg(OSCOPE, "*RST;*CLS")
        nSystErr = vxiClear(DMM)
        nSystErr = WriteMsg(DMM, "*RST;*CLS")
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
            IncStepFailed()
            dFH_Meas = dMeasurement
            'Check to see if the failure is in the scope
            nSystErr = vxiClear(COUNTER)
            nSystErr = WriteMsg(COUNTER, "*RST;*CLS")
            nSystErr = WriteMsg(COUNTER, "INP1:IMP 50")
            nSystErr = WriteMsg(COUNTER, "INP2:IMP 50")
            nSystErr = WriteMsg(COUNTER, "CONF1:NWID 0.8V, .000002")
            nSystErr = WriteMsg(COUNTER, "SENS1:EVEN:HYST MAX")
            'Move the W16 cable connection from SCOPE INPUT 1 to Counter/Timer Input 1.
            nSystErr = WriteMsg(SWITCH1, "OPEN 2.3101") 'open S405-1,3       DMM VCOMP OUT (P23) TO SCOPE INPUT 1 (P15)
            nSystErr = WriteMsg(SWITCH1, "CLOSE 2.1010") 'close S301-117,118  DMM VCOMP OUT (P23) TO CT INPUT 1 (P19)
            nSystErr = WriteMsg(COUNTER, "INIT;FETCH?")
            For count = 1 To 10 'Generate 10 VM Complete pulses
                nSystErr = WriteMsg(DMM, "INIT;FETCH?")
                nSystErr = ReadMsg(DMM, StrMeasurement)
            Next count

            dMeasurement = FetchMeasurement(COUNTER, "VM Complete Diagnostic", "1E-6", "4E-6", "S", COUNTER, "DMM-04-D01")
            If (dMeasurement < 0.000001) Or (dMeasurement > 0.000004) Then
                'If there is a bad measurement here, the problem is from the DMM . . .
                Echo(FormatResults(False, "DMM VM Complete Diagnostic", "DMM-04-D01"))
                RegisterFailure(DMM, "DMM-04-D01", dFH_Meas, "S", 0.000001, 0.000004, "VM Complete Diagnostic")
                TestDMM_VT = FAILED
            Else
                ' . . . otherwise it must be the SCOPE
                Echo(FormatResults(False, "DMM VM Complete Diagnostic", "DMM-04-D01"))
                Echo(sGuiLabel(OSCOPE) & " FAILED, the " & sGuiLabel(DMM) & " Self-Test can not be Completed.")
                RegisterFailure(OSCOPE, "DMM-04-D01", dFH_Meas, "S", 0.000001, 0.000004, sGuiLabel(OSCOPE) & " FAILED, the " & sGuiLabel(DMM) & " Self-Test can not be Completed.")
                TestDMM_VT = OSCOPE
            End If
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        Application.doevents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "DMM" And OptionStep = LoopStepNo Then
            GoTo DMM4
        End If
        frmSTest.proProgress.Value = 80
        LoopStepno += 1

DMM5:   'DMM-05-001
        'Test Current Measurement Capability
        nSystErr = vxiClear(ARB)
        nSystErr = WriteMsg(ARB, "*RST;*CLS")

        nSystErr = WriteMsg(SWITCH1, "OPEN 1.0000,0001,1004,1005") 'open S101-1,2, S101-5,6, S301-9,10 and S301-11,12   bb
        nSystErr = WriteMsg(SWITCH1, "OPEN 2.1008,3101") 'open S301-113,114 and S405-1,3

        'Connect the DMM Current measurement input to DC1+ and connect DMM LO to LOADS RED.
        nSystErr = WriteMsg(SWITCH1, "CLOSE 1.1006,1007") 'close S301-13,14, S301-15,16     connect DMM-CUR to LOADS-HI
        nSystErr = WriteMsg(SWITCH1, "CLOSE 2.1000,1001") 'close S301-97,98, S301-99,100    connect DC1-HI to DMM-LO

        'Connect DC1- and LOADS BLACK.
        nSystErr = WriteMsg(SWITCH1, "CLOSE 2.1002,1003") 'close S301-101,102, S301-103,104 connect DC1-LO to LOADS-LO

        'Set DC Supply 1 to 26 volts
        CommandSetOptions(1, False, True, True, True)
        Delay(0.3)
        CommandSetCurrent(1, 4) ' was 2.5
        Delay(0.3)
        CommandSetVoltage(1, 26)


        nSystErr = WriteMsg(DMM, "*RST;*CLS")
        Delay(1)
        nSystErr = WriteMsg(DMM, "MEAS:CURR:DC?")
        WaitForResponse(DMM, 0.1)

        dMeasurement = FetchMeasurement(DMM, "Current Measurement Test", "1.30", "2.00", "A", DMM, "DMM-05-001")
        If dMeasurement < 1.3 Or dMeasurement > 2.0 Then
            TestDMM_VT = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        If OptionMode = LOSmode And OptionTestName = "DMM" And OptionStep = LoopStepno Then
            GoTo DMM5
        End If
        CommandSupplyReset(1)
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
        nSystErr = vxiClear(DMM)
        nSystErr = WriteMsg(DMM, "*RST;*CLS")

        If AbortTest = True Then
            If TestDMM_VT() = FAILED Then
                ReportFailure(DMM)
            Else
                ReportUnknown(DMM)
            End If
        ElseIf TestDMM_VT() = PASSED Then
            ReportPass(DMM)
        ElseIf TestDMM_VT() = FAILED Then
            ReportFailure(DMM)
        Else
            ReportUnknown(DMM)
        End If

    End Function



End Module