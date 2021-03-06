'Option Strict Off
Option Explicit On

Public Module modDigital

    '**************************************************************
    '* Nomenclature   : ATS-TETS SYSTEM SELF TEST                 *
    '*                  Digital Test Subsystem Test               *
    '* Version        : 2.0                                       *
    '* Last Update    : Apr 1, 2017                               *
    '* Purpose        : This module contains code for the         *
    '*                  Digital Test Subsystem Self Test          *
    '**************************************************************

    Dim hndlDTI As Integer
    Dim CCC1PinList(63) As Integer ' 0 to 63
    Dim CCC2PinList(63) As Integer ' 0 to 63
    Dim CCC3PinList(63) As Integer ' 0 to 63
    Dim CCCPinResult(63) As Integer ' 0 to 64
    Dim HVModeState(31) As Integer '0 to 31
    Dim phaseEntry(3) As Double ' 0 to 3
    Dim HVPinmode As Integer
    Dim HVPinState As Integer
    Dim Maskbit As Integer
    Dim MeasuredValue As String

    Dim SelfTestBuffer As String = Space(256)
    Dim addlInfo As String = Space(2048)
    Dim S As String = ""

    Const TTL_LEVELS As Integer = 1
    Const CCC1 As Integer = 201
    Const CCC2 As Integer = 202
    Const CCC3 As Integer = 203

    Public GroundRefInterupt As Short

    Const DTS_SELF_TEST_BAT As String = "DTS.BAT"
    Const RESULTS_DIGITAL1 As String = "DTS1.TXT"
    Const RESULTS_DIGITAL2 As String = "DTS2.TXT"
    Const RESULTS_DIGITAL3 As String = "DTS3.TXT"

    Const TOTAL_DTS_FILE_LENGTH As integer = 5522
    Const TOTAL_CTEST_FILE_LENGTH As integer = 612
    Const TOTAL_DRV_REFTEST_FILE_LENGTH As integer = 500


    Function TestDTS() As Integer

        'DESCRIPTION:   This routine tests the DTS and returns PASSED or FAILED
        Dim PinCount As integer
        Dim InitStatus As Integer
        Dim retCount As Integer
        Dim TestFailure As Short
        Dim patternSetResult As Integer
        Dim PatCount As Short
        Dim resultCount As Integer
        Dim lastResultIndex As Integer
        Dim dMeasurement As Double
        Dim STFileHandle As Integer
        Dim StartTime As Single
        Dim FileL As Integer
        Dim TestStatus As integer
        Dim LineCount As Integer
        Dim ThisTest As Short
        Dim TestLine As String
        Dim lev As Double
        Dim sTempText As String = ""
        Dim SError As String 'Error Message
        Dim x As Integer
        Dim i As Integer
        Dim j As Integer

        Dim state As Integer
        Dim sMsg As String
        Dim LoopTestNo As Integer
        Dim temp As String
        Dim SAIFinstalled As Integer
        Dim x2 As Integer

        DigitalModule = 4 ' Controller

        ' was M9selftestPath = "C:\VXIPNP\WinNT\terM9\" 
        M9selftestPath = "C:\Program Files (x86)\IVI Foundation\VISA\WinNT\terM9\"
        SAIFinstalled = True
        HelpContextID = 1120

        'Digital Test Subsystem Test Title block
        EchoTitle(InstrumentDescription(DIGITAL) & " ", True)
        EchoTitle("Digital Test Subsystem Test", False)
        frmSTest.proProgress.Maximum = 100
        frmSTest.proProgress.Value = 1

        TestDTS = UNTESTED
        If AbortTest = True Then GoTo TestComplete
        AbortTest = False
        TestDTS = PASSED

        'Pin groups by card
        For PinCount = 0 To 63
            CCC1PinList(PinCount) = TERM9_SCOPE_CHAN(PinCount)
            CCC2PinList(PinCount) = TERM9_SCOPE_CHAN(PinCount + 64)
            CCC3PinList(PinCount) = TERM9_SCOPE_CHAN(PinCount + 128)
        Next PinCount

        InitStatus = terM9_init("TERM9::#0", VI_TRUE, VI_TRUE, hndlDTI) ' get the DTS handle

        If InitStatus > &H4FFF0000 Then
            SError = "    Initialization failure.  Most likely faulty module: CCRB."
            Echo(FormatResults(False, "DIGITAL Initialization failure", sTNum))
            RegisterFailure(DIGITAL, sTNum, , , , , SError)
            MsgBox(SError, vbExclamation)
            Echo(SError)
            TestDTS = FAILED
            Exit Function
        End If
        Application.DoEvents()
        instrumentHandle(DIGITAL) = hndlDTI
        nSystErr = terM9_initializeInstrument(hndlDTI)
        nSystErr = terM9_setSystemEnable(hndlDTI, VI_TRUE)

        If FirstPass = True Then
            S = "Remove all cable connections from the SAIF." & vbCrLf
            S &= "Use cable W15 to connect" & vbCrLf & vbCrLf
            S &= "    SCOPE INPUT 1   to   UTIL DTS CAL 2."
            DisplaySetup(S, "TETS SI1_UD2.jpg", 1)
           If AbortTest = True Then GoTo testcomplete
        End If

DTS_1_1:  'DTS-01-001
        nSystErr = terM9_reset(hndlDTI)
        nSystErr = terM9_resetProbe(hndlDTI)
        S = "1. Connect the Digital probe to the Digital Probe BNC connector." & vbCrLf
        S &= "2. Connect the Digital probe tip to the probe tip adapter on the CAL port." & vbCrLf
        DisplaySetup(S, "ST-DTS-Probe-1.jpg", 2)
        If AbortTest = True Then GoTo TestComplete

        nSystErr = MsgBox("Is COMP LED on the receiver lit?", vbYesNo, "Probe Test")

        If nSystErr = vbNo Then
            nSystErr = MsgBox("Slowly tweak the probe compensation adjustment until the COMP LED lights." & vbCrLf & "Does the LED light?", vbYesNo, "Probe Test")
            If nSystErr = vbNo Then
                Echo("DTS-01-001")
                Echo("    Digital Probe Compensation has failed.  Check probe and probe interface PCB.")
                TestDTS = FAILED
                x = MsgBox("Digital Probe has failed, do you want to continue DTS tests?", vbYesNo)
                If x <> vbYes Then
                    GoTo TestComplete
                End If
                Echo(FormatResults(False, "Digital Probe Compensation", "DTS-01-001"))
                RegisterFailure(DIGITAL, "DTS-01-001", , , , , "Digital Probe Compensation has failed.  Check probe and probe interface PCB.")
                IncStepFailed()
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else
                Echo(FormatResults(True, "Digital Probe Compensation", "DTS-01-001")) 'Passed
                IncStepPassed()
            End If
        Else
            Echo(FormatResults(True, "Digital Probe Compensation", "DTS-01-001")) 'Passed
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "DTS" And OptionStep = 1 Then
            For i = 1 To 5
                Delay(0.2)
                Application.DoEvents()
            Next i
            GoTo DTS_1_1
        End If
        frmSTest.proProgress.Value = 2


DTS_1_2:  'DTS-01-002
ReTestProbeLED:
        nSystErr = terM9_reset(hndlDTI)
        nSystErr = terM9_resetProbe(hndlDTI)
        terM9_initiateProbeButton(hndlDTI)
        x = MsgBox("Leave probe inserted in DTS Cal Port." & vbCrLf & "Do NOT press probe button at this time." & vbCrLf & "Is the Contact Detection LED (green) on the DTS probe lit?", vbYesNo, "Contact Detection LED Test")
        If x = vbNo Then
            x = MsgBox("Contact Detection LED Test failed." & vbCrLf & "Do you want to retry the Contact Detection LED test?", vbYesNo, "Contact Detection LED Test")
            If x = vbYes Then
                GoTo ReTestProbeLED
            End If
            TestDTS = FAILED
            Echo(FormatResults(False, "Digital Probe Contact Detection LED ON Test", "DTS-01-002")) 'Failed
            Echo("Check probe and probe interface PCB.")
            RegisterFailure(DIGITAL, "DTS-01-002", , , , , "Digital Probe Contact Detection LED ON Test failed")
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            Echo(FormatResults(True, "Digital Probe Contact Detection LED ON Test", "DTS-01-002")) 'Passed
            IncStepPassed()
        End If
        terM9_stopProbeButton(hndlDTI)
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "DTS" And OptionStep = 2 Then
            GoTo DTS_1_2
        End If
        frmSTest.proProgress.Value = 4

DTS_1_3:  'DTS-01-003
        nSystErr = terM9_reset(hndlDTI)
        nSystErr = terM9_resetProbe(hndlDTI)
        terM9_initiateProbeButton(hndlDTI)
        x = MsgBox("Do NOT press probe button at this time." & vbCrLf & "Remove the DTS probe from the DTS Cal Port, and then reinsert it." & vbCrLf & "Did the Contact Detection LED (green) on the DTS probe go out when the probe was removed, and illuminate when the probe was reinserted?", MsgBoxStyle.YesNo, "Contact Detection LED Test")
        If x = vbNo Then
            x = MsgBox("Contact Detection LED OFF Test failed." & vbCrLf & "Do you want to retry the Contact Detection LED test?", MsgBoxStyle.YesNo, "Contact Detection LED Test")
            If x = vbYes Then
                GoTo ReTestProbeLED
            End If
            TestDTS = FAILED
            Echo(FormatResults(False, "Digital Probe Contact Detection LED OFF Test", "DTS-01-003")) 'Failed
            Echo("Check probe and probe interface PCB.")
            RegisterFailure(DIGITAL, "DTS-01-003", , , , , "Digital Probe Contact Detection LED OFF Test failed")
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            Echo(FormatResults(True, "Digital Probe Contact Detection LED OFF Test", "DTS-01-003")) 'Passed
            IncStepPassed()
        End If
        terM9_stopProbeButton(hndlDTI)
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "DTS" And OptionStep = 3 Then
            GoTo DTS_1_3
        End If
        frmSTest.proProgress.Value = 6


DTS_1_4:  'DTS-01-004
ReTestProbeButton:  ' new test bb
        terM9_initiateProbeButton(hndlDTI)
        Delay(2)
        S = "Read all instructions before continuing this test." & vbCrLf
        S &= "1. Verify the Digital probe is inserted into the DTS Cal Port." & vbCrLf
        S &= "2. Press 'Continue' to start the DTS probe button test." & vbCrLf
        S &= "3. Then, click the DTS probe button within 30 seconds." & vbCrLf
        DisplaySetup(S, "ST-DTS-PROBE-1.jpg", 3)
        Echo("-- Waiting on DTS Probe button!")
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete

        If (terM9_waitForProbeButton(hndlDTI, 30) = TERM9_ERROR_PROBE_TIMEOUT) Then
            x = MsgBox("DTS Probe Button Test timed out." & vbCrLf & "Do you want to retry the Digital probe button test?", vbYesNo)
            If x = vbYes Then
                GoTo ReTestProbeButton
            End If
            TestDTS = FAILED
            Echo(FormatResults(False, "Digital Probe Button Test", "DTS-01-004")) 'Failed
            Echo("    Probe Button TimeOut Error.  Check probe and probe interface PCB.")
            RegisterFailure(DIGITAL, "DTS-01-004", , , , , "DTS Probe Button Test timed out.")
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            Echo(FormatResults(True, "Digital Probe Button Test", "DTS-01-004")) 'Passed
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "DTS" And OptionStep = 4 Then
            GoTo DTS_1_4
        End If

        frmSTest.proProgress.Value = 10


        '**************
        '**  Test CRB
        '**************
DTS_2_1:  'DTS-02-001 ~ DTS-02-032
        'Test High Voltage Pins 1 - 31
        'set all pins to pullup high except UB0
        HVPinmode = 1
        nSystErr = terM9_setHighVoltagePinGroupSettings(hndlDTI, 2.2, 32, HVPinmode)
        nSystErr = terM9_fetchHighVoltagePinGroupState(hndlDTI, 32, retCount, HVPinState) ' retrieves all bits

        If HVPinState <> 0 Then
            'Test that all pins are being pulled low (0 or 2.2V) by short or resistor divider network in the saif
            Maskbit = 1
            TestDTS = FAILED
            For PinCount = 0 To 31
                If (Maskbit And HVPinState) <> 0 Then
                    sTNum = "DTS-02-" & Format((PinCount + 1), "000")
                    Echo(FormatResults(False, "Utility Pin " & CStr(PinCount) & " on CRB Test 1", sTNum))
                    RegisterFailure(DIGITAL, sTNum, , , , , "Utility Pin " & CStr(PinCount) & " on CRB Test 1")
                    Select Case PinCount
                        Case 0 : Echo("  P8-24,P8-27,P8-30,P8-31") ' UB0
                        Case 1 : Echo("  P8-23,P8-63,P8-64") ' UB1
                        Case 2 : Echo("  P8-22,P8-59,P8-62") ' UB2
                        Case 3 : Echo("  P8-21,P8-55,P8-56") ' UB3
                        Case 4 : Echo("  P8-20,P8-53,P8-54") ' UB4
                        Case 5 : Echo("  P8-19,P8-51,P8-52") ' UB5
                        Case 6 : Echo("  P8-18,P8-50")  ' UB6
                        Case 7 : Echo("  P8-17,P8-49")  ' UB7
                        Case 8 : Echo("  P8-16,P8-48")  ' UB8
                        Case 9 : Echo("  P8-15,P8-47")  ' UB9
                        Case 10 : Echo("  P8-14,P8-46") ' UB10
                        Case 11 : Echo("  P8-13,P8-45") ' UB11
                        Case 12 : Echo("  P8-12,P8-44") ' UB12
                        Case 13 : Echo("  P8-11,P8-43") ' UB13
                        Case 14 : Echo("  P8-10,P8-42") ' UB14
                        Case 15 : Echo("  P8-9,P8-41")  ' UB15
                        Case 16 : Echo("  P8-8,P8-40")  ' UB16
                        Case 17 : Echo("  P8-7,P8-39")  ' UB17
                        Case 18 : Echo("  P8-6,P8-38")  ' UB18
                        Case 19 : Echo("  P8-5,P8-37")  ' UB19
                        Case 20 : Echo("  P8-4,P8-36")  ' UB20
                        Case 21 : Echo("  P8-3,P8-35")  ' UB21
                        Case 22 : Echo("  P8-2,P8-34")  ' UB22
                        Case 23 : Echo("  P8-1,P8-33")  ' UB23
                        Case 24 : Echo("  P9-40,P9-23,P9-24") ' UB24
                        Case 25 : Echo("  P9-39,P9-21,P9-22") ' UB25
                        Case 26 : Echo("  P9-38,P9-15,P9-16") ' UB26
                        Case 27 : Echo("  P9-37,P9-13,P9-14") ' UB27
                        Case 28 : Echo("  P9-36,P9-7,P9-8")   ' UB28
                        Case 29 : Echo("  P9-35,P9-5,P9-6")   ' UB29
                        Case 30 : Echo("  P9-34,P9-3,P9-4")   ' UB30
                        Case 31 : Echo("  P9-33,P9-1,P9-2")   ' UB31
                    End Select
                End If
                If PinCount < 30 Then ' 1,2,4,8,16,32,64,128,,,
                    Maskbit *= 2
                Else
                    Maskbit = -2147483648.0 ' hex 800000000
                End If
            Next PinCount
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            Echo(FormatResults(True, "Utility Pin Test 1", "DTS-02-001 to 032"))
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "DTS" And OptionStep = 5 Then
            GoTo DTS_2_1
        End If
        frmSTest.proProgress.Value = 13


DTS_3_1:  'DTS-03-001 ~ 'DTS-03-031
        'Test that non-shorted pins are able to go above 1V
        nSystErr = terM9_setHighVoltagePinGroupSettings(hndlDTI, 1, 32, HVPinmode)
        nSystErr = terM9_fetchHighVoltagePinGroupState(hndlDTI, 32, retCount, HVPinState)
        If (HVPinState And &HFF00003E) <> &HFF00003E Then '-16777154
            Maskbit = 2
            TestDTS = FAILED
            For PinCount = 1 To 31
                If PinCount < 6 Or PinCount > 23 Then
                    If (Maskbit And HVPinState) = 0 Then
                        sTNum = "DTS-03-" & Format((PinCount + 1), "000")
                        Echo(FormatResults(False, "Utility Pin " & CStr(PinCount) & " on CRB Test 2", sTNum))
                        RegisterFailure(DIGITAL, sTNum, , , , , "Utility Pin " & CStr(PinCount) & " on CRB Test 2")
                        Select Case PinCount
                            Case 0 : Echo("  P8-24,P8-27,P8-30,P8-31") ' UB0
                            Case 1 : Echo("  P8-23,P8-63,P8-64") ' UB1
                            Case 2 : Echo("  P8-22,P8-59,P8-62") ' UB2
                            Case 3 : Echo("  P8-21,P8-55,P8-56") ' UB3
                            Case 4 : Echo("  P8-20,P8-53,P8-54") ' UB4
                            Case 5 : Echo("  P8-19,P8-51,P8-52") ' UB5
                            Case 24 : Echo("  P9-40,P9-23,P9-24") ' UB24
                            Case 25 : Echo("  P9-39,P9-21,P9-22") ' UB25
                            Case 26 : Echo("  P9-38,P9-15,P9-16") ' UB26
                            Case 27 : Echo("  P9-37,P9-13,P9-14") ' UB27
                            Case 28 : Echo("  P9-36,P9-7,P9-8")   ' UB28
                            Case 29 : Echo("  P9-35,P9-5,P9-6")   ' UB29
                            Case 30 : Echo("  P9-34,P9-3,P9-4")   ' UB30
                            Case 31 : Echo("  P9-33,P9-1,P9-2")   ' UB31
                        End Select
                    End If
                End If
                If PinCount < 30 Then
                    Maskbit *= 2
                Else
                    Maskbit = -2147483648.0 ' ff 8000 0000
                End If
            Next PinCount
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            Echo(FormatResults(True, "Utility Pin Test 2", "DTS-03-001 to 031"))
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "DTS" And OptionStep = 6 Then
            GoTo DTS_3_1
        End If
        frmSTest.proProgress.Value = 15

DTS_4_1:  'DTS-04-001   TestCRB
        'Test clutch circuitry
        '  >Initialize
        nSystErr = terM9_setSystemClockReference(hndlDTI, TERM9_SOURCE_INTERNAL)
        nSystErr = terM9_setDynamicPatternSetTimeout(hndlDTI, 3.0)
        '  >Load and execute
        nSystErr = terM9_prepareDynamicPatternSetLoading(hndlDTI)
        nSystErr = terM9_setDynamicPatternIndex(hndlDTI, 0)
        '      >> IOX all the pins
        nSystErr = terM9_setChannelPinOpcode(hndlDTI, TERM9_SCOPE_CHANSYSTEM, TERM9_OP_IOX)
        '      >> load 3 patterns
        nSystErr = terM9_loadDynamicPattern(hndlDTI, VI_TRUE, VI_TRUE)
        nSystErr = terM9_loadDynamicPattern(hndlDTI, VI_TRUE, VI_TRUE)
        nSystErr = terM9_loadDynamicPattern(hndlDTI, VI_TRUE, VI_TRUE)
        '      >> load PATC so the hardware knows where to stop
        nSystErr = terM9_setDynamicPatternControl(hndlDTI, TERM9_COP_HALT, 0, TERM9_COND_TRUE)
        nSystErr = terM9_loadDynamicPattern(hndlDTI, VI_TRUE, VI_TRUE)
        '      >> set clutch circuitry
        nSystErr = terM9_setSystemClutch(hndlDTI, VI_TRUE, TERM9_CLUTCH_INPUT_LOW, TERM9_LOGIC_TTL)
        nSystErr = terM9_setHighVoltagePinMode(hndlDTI, 0, TERM9_HVPINMODE_OFF)

        '      >> run the dynamic pattern set
        nSystErr = terM9_runDynamicPatternSet(hndlDTI, VI_FALSE, patternSetResult)
        If patternSetResult = TERM9_RESULT_MBT Then
            TestDTS = FAILED
            Echo(FormatResults(False, "Clutch/HV0 Test 1", "DTS-04-001"))
            RegisterFailure(DIGITAL, "DTS-04-001", , , , , "Clutch/HV0 Test 1")
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            Echo(FormatResults(True, "Clutch/HV0 Test 1", "DTS-04-001"))
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "DTS" And OptionStep = 7 Then
            GoTo DTS_4_1
        End If
        frmSTest.proProgress.Value = 17


DTS_5_1:  'DTS-05-001
        nSystErr = terM9_setHighVoltagePinMode(hndlDTI, 0, TERM9_HVPINMODE_ON)
        '      >> run the dynamic pattern set
        nSystErr = terM9_runDynamicPatternSet(hndlDTI, VI_FALSE, patternSetResult)
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete

        If patternSetResult <> TERM9_RESULT_MBT Then
            TestDTS = FAILED
            Echo(FormatResults(False, "Clutch/HV0 Test 2", "DTS-05-001"))
            RegisterFailure(DIGITAL, "DTS-05-001", , , , , "Clutch/HV0 Test 2")
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            Echo(FormatResults(True, "Clutch/HV0 Test 2", "DTS-05-001"))
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "DTS" And OptionStep = 8 Then
            GoTo DTS_5_1
        End If
        frmSTest.proProgress.Value = 20


DTS_6_1:  'DTS-06-001
        'Test Exeternal Clock input
        nSystErr = terM9_setSystemClutch(hndlDTI, VI_FALSE, TERM9_CLUTCH_INPUT_LOW, TERM9_LOGIC_TTL)
        nSystErr = terM9_prepareDynamicPatternSetLoading(hndlDTI)
        nSystErr = terM9_setDynamicPatternIndex(hndlDTI, 0)
        nSystErr = terM9_setDynamicPatternClocksPerPattern(hndlDTI, 1)
        nSystErr = terM9_setDynamicPatternSetTimeout(hndlDTI, 20.0)
        '      >> IOX all the pins
        nSystErr = terM9_setChannelPinOpcode(hndlDTI, TERM9_SCOPE_CHANSYSTEM, TERM9_OP_IOX)
        '      >> load 3 patterns
        nSystErr = terM9_loadDynamicPattern(hndlDTI, VI_TRUE, VI_TRUE)
        nSystErr = terM9_loadDynamicPattern(hndlDTI, VI_TRUE, VI_TRUE)
        nSystErr = terM9_loadDynamicPattern(hndlDTI, VI_TRUE, VI_TRUE)
        '      >> load PATC so the hardware knows where to stop
        nSystErr = terM9_setDynamicPatternControl(hndlDTI, TERM9_COP_HALT, 0, TERM9_COND_TRUE)
        nSystErr = terM9_loadDynamicPattern(hndlDTI, VI_TRUE, VI_TRUE)
        nSystErr = terM9_setSystemClock(hndlDTI, 1, TERM9_EDGE_RISING, TERM9_CLOCKMODE_ON, TERM9_SOURCE_EXTERNAL, TERM9_PATH_ENABLE, TERM9_LOGIC_TTL)
        nSystErr = terM9_prepareDynamicPatternSetExecution(hndlDTI, VI_TRUE, VI_FALSE, VI_FALSE, VI_FALSE, VI_FALSE, VI_TRUE, 4, TERM9_PATTERNSET_LEARN_PATS, TERM9_PATTERNSET_PRECOUNT_PATS, 0, -1)
        nSystErr = terM9_initiateDynamicPatternSetExecution(hndlDTI)

        For PatCount = 1 To 500
            nSystErr = terM9_setHighVoltagePinMode(hndlDTI, 0, TERM9_HVPINMODE_OFF)
            nSystErr = terM9_setHighVoltagePinMode(hndlDTI, 0, TERM9_HVPINMODE_ON)
            Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete
        Next PatCount

        nSystErr = terM9_waitForDynamicPatternSetExecution(hndlDTI)
        nSystErr = terM9_stopDynamicPatternSetExecution(hndlDTI)

        nSystErr = terM9_fetchDynamicPatternSetResult(hndlDTI, patternSetResult, resultCount, lastResultIndex)

        If patternSetResult = TERM9_RESULT_MBT Then
            TestDTS = FAILED
            Echo(FormatResults(False, "External Clock Input Test", "DTS-06-001"))
            RegisterFailure(DIGITAL, "DTS-06-001", , , , , "External Clock Input Test")
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            Echo(FormatResults(True, "External Clock Input Test", "DTS-06-001"))
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "DTS" And OptionStep = 9 Then
            GoTo DTS_6_1
        End If
        frmSTest.proProgress.Value = 25


DTS_7_1:  'DTS-07-001
        'Test User Clocks and Sync pulses
        nSystErr = terM9_reset(hndlDTI)
        nSystErr = terM9_setDynamicPatternSetTimeout(hndlDTI, 3.0)
        nSystErr = terM9_setTimingSetPeriod(hndlDTI, 0, 0.0000005) '  bb 2017 set the clock period to 0.5 us (was 1 us)
        nSystErr = terM9_prepareDynamicPatternSetLoading(hndlDTI)
        nSystErr = terM9_setDynamicPatternIndex(hndlDTI, 0)
        '  IOX all the pins in the system
        nSystErr = terM9_setChannelPinOpcode(hndlDTI, TERM9_SCOPE_CHANSYSTEM, TERM9_OP_IOX)
        '  load 16 patterns to flush the pipeline
        For PatCount = 0 To 15 'pattern 0 to 15
            nSystErr = terM9_loadDynamicPattern(hndlDTI, VI_TRUE, VI_TRUE)
        Next PatCount
        '  listen with front panel sync resource 0 pattern 16
        nSystErr = terM9_setFrontPanelSyncState(hndlDTI, 0, TERM9_SYNC_LISTEN)
        nSystErr = terM9_loadDynamicPattern(hndlDTI, VI_TRUE, VI_TRUE)
        nSystErr = terM9_setFrontPanelSyncState(hndlDTI, 0, TERM9_SYNC_IDLE)

        '  listen with front panel sync resource 1 pattern 17
        nSystErr = terM9_setFrontPanelSyncState(hndlDTI, 1, TERM9_SYNC_LISTEN)
        nSystErr = terM9_loadDynamicPattern(hndlDTI, VI_TRUE, VI_TRUE)
        nSystErr = terM9_setFrontPanelSyncState(hndlDTI, 1, TERM9_SYNC_IDLE)

        '  listen with front panel sync resource 2 pattern 18
        nSystErr = terM9_setFrontPanelSyncState(hndlDTI, 2, TERM9_SYNC_LISTEN)
        nSystErr = terM9_loadDynamicPattern(hndlDTI, VI_TRUE, VI_TRUE)
        nSystErr = terM9_setFrontPanelSyncState(hndlDTI, 2, TERM9_SYNC_IDLE)

        '  listen with front panel sync resource 3 pattern 19
        nSystErr = terM9_setFrontPanelSyncState(hndlDTI, 3, TERM9_SYNC_LISTEN)
        nSystErr = terM9_loadDynamicPattern(hndlDTI, VI_TRUE, VI_TRUE)
        nSystErr = terM9_setFrontPanelSyncState(hndlDTI, 3, TERM9_SYNC_IDLE)

        '  load 20 PATC HALTs on SYNC
        For PatCount = 1 To 20
            nSystErr = terM9_setDynamicPatternControl(hndlDTI, TERM9_COP_HALT, 0, TERM9_COND_SYNC)
            nSystErr = terM9_loadDynamicPattern(hndlDTI, VI_TRUE, VI_TRUE) ' patn 20-39
        Next PatCount

        '  load a PATC HALT on TRUE so that we always stop
        nSystErr = terM9_setDynamicPatternControl(hndlDTI, TERM9_COP_HALT, 0, TERM9_COND_ALWAYS)

        nSystErr = terM9_loadDynamicPattern(hndlDTI, VI_TRUE, VI_TRUE) '  pattern 40

        nSystErr = terM9_runDynamicPatternSet(hndlDTI, VI_FALSE, patternSetResult) '5 sec timeout
        nSystErr = terM9_fetchDynamicPatternSetResult(hndlDTI, patternSetResult, resultCount, lastResultIndex)

        ' setup an array with the user clock phase entries.  Channel 0?s phases assert at 100 ns and
        ' return at 900 ns.  Channel 1?s phases are noedged
        phaseEntry(0) = 0.0000001 'ch0 start 0.0000001
        phaseEntry(1) = 0.0000009 'ch0 stop  0.0000009
        phaseEntry(2) = -1.0      'ch1
        phaseEntry(3) = -1.0      'ch1

        'Sync 3 / Clock 0
        nSystErr = terM9_setUserClock(hndlDTI, 0, TERM9_RELAY_CLOSED, 3.0, 1.0, TERM9_UCLKMODE_ON, 4, phaseEntry(0), TERM9_UCLK_TRIGGER_SYSCLK)
        nSystErr = terM9_setFrontPanelSyncResource(hndlDTI, 3, TERM9_LOGIC_TTL, 1, TERM9_RELAY_CLOSED)
        ' run the dynamic pattern set
        nSystErr = terM9_runDynamicPatternSet(hndlDTI, VI_TRUE, patternSetResult) '5 sec

        ' fetch the pattern set result to get the last index and pattern count
        nSystErr = terM9_fetchDynamicPatternSetResult(hndlDTI, patternSetResult, resultCount, lastResultIndex)
        ' check the value of resultCount(36) and lastResultIdx(35).  
        System.Threading.Thread.Sleep(100)

        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete

        If (resultCount <> 36) Or (lastResultIndex <> 35) Then
            TestDTS = FAILED
            Echo(FormatResults(False, "User Clock 0 \ Sync 3 Test", "DTS-07-001"))
            Echo("    Result =  " & CStr(resultCount) & ", " & CStr(lastResultIndex) & ", Expected 36,35")
            RegisterFailure(DIGITAL, "DTS-07-001", , , , , "User Clock 0 \ Sync 3 Test")
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            Echo(FormatResults(True, "User Clock 0 \ Sync 3 Test", "DTS-07-001"))
'            Echo("    Result =  " & CStr(resultCount) & ", " & CStr(lastResultIndex) & ", Expected 36,35")
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "DTS" And OptionStep = 10 Then
            GoTo DTS_7_1
        End If
        frmSTest.proProgress.Value = 27


DTS_8_1:  'DTS-08-001
        nSystErr = terM9_setUserClock(hndlDTI, 0, TERM9_RELAY_OPEN, 3.0, 1.0, TERM9_UCLKMODE_OFF, 4, phaseEntry(0), TERM9_UCLK_TRIGGER_SYSCLK)
        System.Threading.Thread.Sleep(100)
        nSystErr = terM9_setFrontPanelSyncResource(hndlDTI, 3, TERM9_LOGIC_TTL, 1, TERM9_RELAY_OPEN)
        System.Threading.Thread.Sleep(100)

        'Sync 2/ Clock 1
        ' setup an array with the user clock phase entries.  Channel 1?s phases assert at 100 ns and
        ' return at 900 ns.  Channel 0?s phases are noedged
        phaseEntry(0) = -1.0
        phaseEntry(1) = -1.0
        phaseEntry(2) = 0.0000001
        phaseEntry(3) = 0.0000009

        nSystErr = terM9_setUserClock(hndlDTI, 0, TERM9_RELAY_CLOSED, 3.0, 1.0, TERM9_UCLKMODE_ON, 4, phaseEntry(0), TERM9_UCLK_TRIGGER_SYSCLK)
        nSystErr = terM9_setFrontPanelSyncResource(hndlDTI, 2, TERM9_LOGIC_TTL, 1, TERM9_RELAY_CLOSED)
        ' run the dynamic pattern set
        nSystErr = terM9_runDynamicPatternSet(hndlDTI, VI_TRUE, patternSetResult) ' 5 sec

        ' fetch the pattern set result to get the last index and pattern count
        nSystErr = terM9_fetchDynamicPatternSetResult(hndlDTI, patternSetResult, resultCount, lastResultIndex)
        ' check the value of resultCount(35) and lastResultIdx(34).  

        If (resultCount <> 35) Or (lastResultIndex <> 34) Then
            TestDTS = FAILED
            Echo(FormatResults(False, "User Clock 1 \ Sync 2 Test", "DTS-08-001"))
            Echo("    Result =  " & CStr(resultCount) & ", " & CStr(lastResultIndex) & ", Expected 35,34")
            RegisterFailure(DIGITAL, "DTS-08-001", , , , , "User Clock 1 \ Sync 2 Test")
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            Echo(FormatResults(True, "User Clock 1 \ Sync 2 Test", "DTS-08-001"))
  '          Echo("    Result =  " & CStr(resultCount) & ", " & CStr(lastResultIndex) & ", Expected 35,34")
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "DTS" And OptionStep = 11 Then
            GoTo DTS_8_1
        End If
        frmSTest.proProgress.Value = 30


DTS_9_1:  'DTS-09-001
        nSystErr = terM9_setUserClock(hndlDTI, 0, TERM9_RELAY_OPEN, 3.0, 1.0, TERM9_UCLKMODE_OFF, 4, phaseEntry(0), TERM9_UCLK_TRIGGER_SYSCLK)
        nSystErr = terM9_setFrontPanelSyncResource(hndlDTI, 2, TERM9_LOGIC_TTL, 1, TERM9_RELAY_OPEN)

        'Sync 1/ Clock 2
        phaseEntry(0) = 0.0000001
        phaseEntry(1) = 0.0000009
        phaseEntry(2) = -1.0
        phaseEntry(3) = -1.0

        nSystErr = terM9_setUserClock(hndlDTI, 1, TERM9_RELAY_CLOSED, 3.0, 1.0, TERM9_UCLKMODE_ON, 4, phaseEntry(0), TERM9_UCLK_TRIGGER_SYSCLK)

        nSystErr = terM9_setFrontPanelSyncResource(hndlDTI, 1, TERM9_LOGIC_TTL, 1, TERM9_RELAY_CLOSED)
        ' run the dynamic pattern set
        nSystErr = terM9_runDynamicPatternSet(hndlDTI, VI_TRUE, patternSetResult) '5 sec

        ' fetch the pattern set result to get the last index and pattern count
        nSystErr = terM9_fetchDynamicPatternSetResult(hndlDTI, patternSetResult, resultCount, lastResultIndex)
        ' check the value of resultCount(34) and lastResultIdx(33).  

        If (resultCount <> 34) Or (lastResultIndex <> 33) Then
            TestDTS = FAILED
            Echo(FormatResults(False, "User Clock 2 \ Sync 1 Test", "DTS-09-001"))
            Echo("    Result =  " & CStr(resultCount) & ", " & CStr(lastResultIndex) & ", Expected 34,33")
            RegisterFailure(DIGITAL, "DTS-09-001", , , , , "User Clock 2 \ Sync 1 Test")
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            Echo(FormatResults(True, "User Clock 2 \ Sync 1 Test", "DTS-09-001"))
'            Echo("    Result =  " & CStr(resultCount) & ", " & CStr(lastResultIndex) & ", Expected 34,33")
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "DTS" And OptionStep = 12 Then
            GoTo DTS_9_1
        End If
        frmSTest.proProgress.Value = 33


DTS_10_1:  'DTS-10-001
        nSystErr = terM9_setUserClock(hndlDTI, 1, TERM9_RELAY_OPEN, 3.0, 1.0, TERM9_UCLKMODE_OFF, 4, phaseEntry(0), TERM9_UCLK_TRIGGER_SYSCLK)
        nSystErr = terM9_setFrontPanelSyncResource(hndlDTI, 1, TERM9_LOGIC_TTL, 1, TERM9_RELAY_OPEN)

        'Sync 0/ Clock 3
        phaseEntry(0) = -1.0
        phaseEntry(1) = -1.0
        phaseEntry(2) = 0.0000001
        phaseEntry(3) = 0.0000009

        nSystErr = terM9_setUserClock(hndlDTI, 1, TERM9_RELAY_CLOSED, 3.0, 1.0, TERM9_UCLKMODE_ON, 4, phaseEntry(0), TERM9_UCLK_TRIGGER_SYSCLK)
        nSystErr = terM9_setFrontPanelSyncResource(hndlDTI, 0, TERM9_LOGIC_TTL, 1, TERM9_RELAY_CLOSED)
        ' run the dynamic pattern set
        nSystErr = terM9_runDynamicPatternSet(hndlDTI, VI_TRUE, patternSetResult) ' 5 sec

        ' fetch the pattern set result to get the last index and pattern count
        nSystErr = terM9_fetchDynamicPatternSetResult(hndlDTI, patternSetResult, resultCount, lastResultIndex)
        ' check the value of resultCount(33) and lastResultIdx(32).  
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete

        'Test System Clock
        If (resultCount <> 33) Or (lastResultIndex <> 32) Then
            TestDTS = FAILED
            Echo(FormatResults(False, "User Clock 3 \ Sync 0 Test", "DTS-10-001"))
            Echo("    Result =  " & CStr(resultCount) & ", " & CStr(lastResultIndex) & ", Expected 33,32")
            RegisterFailure(DIGITAL, "DTS-10-001", , , , , "User Clock 3 \ Sync 0 Test")
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            Echo(FormatResults(True, "User Clock 3 \ Sync 0 Test", "DTS-10-001"))
 '           Echo("    Result =  " & CStr(resultCount) & ", " & CStr(lastResultIndex) & ", Expected 33,32")
            IncStepPassed()
        End If
        If OptionMode = LOSmode And OptionTestName = "DTS" And OptionStep = 13 Then
            GoTo DTS_10_1
        End If
        frmSTest.proProgress.Value = 35

        'bb 2017
        nSystErr = terM9_setTimingSetPeriod(hndlDTI, 0, 0.000001) '  set the clock period back to 1 us



DTS_11_1:  'DTS-11-001
        nSystErr = terM9_setUserClock(hndlDTI, 1, TERM9_RELAY_OPEN, 3.0#, 1.0#, TERM9_UCLKMODE_OFF, 4, phaseEntry(0), TERM9_UCLK_TRIGGER_SYSCLK)
        nSystErr = terM9_setFrontPanelSyncResource(hndlDTI, 0, TERM9_LOGIC_TTL, 1, TERM9_RELAY_OPEN)
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete

RetryW15:

        nSystErr = terM9_setSystemClock(hndlDTI, 1, TERM9_EDGE_FALLING, TERM9_CLOCKMODE_ON, TERM9_SOURCE_INTERNAL, TERM9_PATH_ENABLE, TERM9_LOGIC_TTL)

        nSystErr = WriteMsg(OSCOPE, "*RST")
        nSystErr = WriteMsg(OSCOPE, "*CLS")
        nSystErr = WriteMsg(OSCOPE, "CHAN1:RANG 5")
        nSystErr = WriteMsg(OSCOPE, "TIM:RANG 5E-7")
        nSystErr = WriteMsg(OSCOPE, "TRIG:SOUR CHAN1")
        nSystErr = WriteMsg(OSCOPE, "TRIG:LEV 2")
        nSystErr = WriteMsg(OSCOPE, "TIM:MODE TRIG")
        nSystErr = WriteMsg(OSCOPE, "CHAN1:OFFS 2")
        nSystErr = WriteMsg(OSCOPE, "DIG CHAN1")
        For PatCount = 1 To 100
            nSystErr = terM9_runDynamicPatternSet(hndlDTI, VI_TRUE, patternSetResult)
            Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete
        Next PatCount
        Delay(1)
        nSystErr = WriteMsg(OSCOPE, "MEAS:SOUR CHAN1")
        nSystErr = WriteMsg(OSCOPE, "MEAS:VTOP?")
        Delay(0.3)

        'verify that cable W15 is installed if there is a failure
        nSystErr = ReadMsg(OSCOPE, MeasuredValue) ' dummy read cycle
        If MeasuredValue = "" Then MeasuredValue = "0"
        dMeasurement = Val(MeasuredValue)
        If dMeasurement < 2 Or dMeasurement > 6 Then
            x = MsgBox("Are you sure that cable W15 is connected from UTIL DTS CAL2 to  SCOPE INPUT 1?", vbYesNo)
            If x <> vbYes Then
                S = "Use cable W15 to connect" & vbCrLf & vbCrLf
                S &= "   SCOPE INPUT 1   to   UTIL DTS CAL 2"
                DisplaySetup(S, "TETS SI1_UD2.jpg", 1)
                If AbortTest = True Then GoTo TestComplete
                GoTo RetryW15
            End If
        End If
        nSystErr = WriteMsg(OSCOPE, "MEAS:VTOP?")
        Delay(0.3)

        dMeasurement = FetchMeasurement(OSCOPE, "DTS T0CYCLE Output Amplitude (DTS CAL2)", "2", "6", "V", DIGITAL, "DTS-11-001")
        If dMeasurement < 2 Or dMeasurement > 6 Then
            TestDTS = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then
                S = "Move cable W15 connection from" & vbCrLf & vbCrLf
                S &= "       UTIL DTS CAL 2   to   ARB OUTPUT."
                DisplaySetup(S, "TETS AOP_SI1.jpg", 1)
                If AbortTest = True Then GoTo TestComplete

                nSystErr = WriteMsg(ARB, "*RST")
                nSystErr = WriteMsg(ARB, "*CLS")
                nSystErr = WriteMsg(ARB, "MARK:STAT OFF") 'Disable the AFG to output a marker list    
                nSystErr = WriteMsg(ARB, "OUTP:LOAD 50")
                nSystErr = WriteMsg(ARB, "VOLT:UNIT:VOLT VPP")
                nSystErr = WriteMsg(ARB, "FREQ 1E+7")
                nSystErr = WriteMsg(ARB, "FUNC SIN")
                nSystErr = WriteMsg(ARB, "OUTP:FILT:FREQ 10E6")
                nSystErr = WriteMsg(ARB, "OUTP:FILT ON")
                nSystErr = WriteMsg(ARB, "VOLT 3")
                nSystErr = WriteMsg(ARB, "VOLT:OFFS 2")
                nSystErr = WriteMsg(ARB, "INIT:IMM")
                nSystErr = WriteMsg(ARB, "OUTP:STAT ON")
                Delay(1)
                nSystErr = WriteMsg(OSCOPE, "*RST")
                nSystErr = WriteMsg(OSCOPE, "*CLS")
                nSystErr = WriteMsg(OSCOPE, "AUT") 'Set Autoscale mode
                Delay(2)
                nSystErr = WriteMsg(OSCOPE, "DIG CHAN1")
                nSystErr = WriteMsg(OSCOPE, "MEAS:SOUR CHAN1")
                nSystErr = WriteMsg(OSCOPE, "MEAS:FREQ?")
                'DTS-11-D01
                dMeasurement = FetchMeasurement(OSCOPE, "DTS T0CYCLE Output Amplitude Diagnostic Test", "9.5E6", "10.5E6", "Hz", DIGITAL, "DTS-11-D01")
                'DTS-11-D02
                If dMeasurement > 10500000.0 Or dMeasurement < 9500000.0 Then
                    Echo("DTS-11-D01 " & sGuiLabel(OSCOPE) & " Failure.")
                    TestDTS = OSCOPE
                    RegisterFailure(OSCOPE, "DTS-11-D01", _
                        dMeasurement, "Hz", 9500000, 10500000, _
                        "DTS T0CYCLE Output Amplitude Diagnostic Test" & vbCrLf & _
                        "sGuiLabel(OSCOPE) & Failure.")
                Else
                    Echo("DTS-11-D01 " & sGuiLabel(DIGITAL) & " Failure.")
                    RegisterFailure(DIGITAL, "DTS-11-D01", _
                        dMeasurement, "Hz", 9500000, 10500000, _
                            "DTS T0CYCLE Output Amplitude Diagnostic Test" & vbCrLf & _
                            sGuiLabel(DIGITAL) & " Failure.")
                End If
                nSystErr = WriteMsg(ARB, "*RST")
                nSystErr = WriteMsg(ARB, "*CLS")
                nSystErr = WriteMsg(ARB, "MARK:STAT OFF") 'Disable the AFG to output a marker list    
                GoTo TestComplete
            End If
        Else
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "DTS" And OptionStep = 14 Then
            GoTo DTS_11_1
        End If
        frmSTest.proProgress.Value = 37


DTS_12_1:  'DTS-12-001 test TOOCYCLE output frequency
        nSystErr = WriteMsg(OSCOPE, "*RST")
        nSystErr = WriteMsg(OSCOPE, "*CLS")
        nSystErr = WriteMsg(OSCOPE, "*WAI")
        nSystErr = WriteMsg(OSCOPE, "CHAN1:RANG 2")
        nSystErr = WriteMsg(OSCOPE, "TRIG:SOUR CHAN1")
        nSystErr = WriteMsg(OSCOPE, "TRIG:LEV 2;")
        nSystErr = WriteMsg(OSCOPE, "CHAN1:HFR:FREQ 30000000;")
        nSystErr = WriteMsg(OSCOPE, "CHAN1:HFR ON;")
        nSystErr = WriteMsg(OSCOPE, "CHAN1:LFR ON;")
        nSystErr = WriteMsg(OSCOPE, "CHAN1:COUP AC")
        nSystErr = WriteMsg(OSCOPE, "TIM:RANG 10E-6")
        nSystErr = WriteMsg(OSCOPE, "DIG CHAN1")
        nSystErr = WriteMsg(OSCOPE, "MEAS:SOUR CHAN1")
        Delay(0.5)
        nSystErr = WriteMsg(OSCOPE, "MEAS:FREQ?")
        Delay(0.5)

        nSystErr = ReadMsg(OSCOPE, MeasuredValue) ' dummy read cycle
        Delay(0.5)

        nSystErr = WriteMsg(OSCOPE, "MEAS:FREQ?")
        Delay(0.5)

        dMeasurement = FetchMeasurement(OSCOPE, "DTS T0CYCLE Output Frequency", "0.95E6", "1.05E6", "Hz", DIGITAL, "DTS-12-001")

        If (dMeasurement < 950000.0) Or (dMeasurement > 1050000.0) Then
            TestDTS = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "DTS" And OptionStep = 15 Then
            GoTo DTS_12_1
        End If
        frmSTest.proProgress.Value = 40


        '**************
        '**  Test CCC1
        '**************
DTS_13_1:  'DTS-13-000 to DTS-13-063
        '* Set modes and disconnect relays in CCC1
        DigitalModule = 3

        'changed order, added new commands,added SetSystemEnable after levelset commands
        nSystErr = terM9_setSystemEnable(hndlDTI, VI_TRUE) ' new command
        nSystErr = terM9_setGroundReference(hndlDTI, TERM9_SCOPE_SYSTEM, TERM9_GROUND_INTERNAL)
        nSystErr = terM9_setLowPower(hndlDTI, TERM9_SCOPE_SYSTEM, VI_FALSE)
        nSystErr = terM9_setPinmapGroup(hndlDTI, CCC1, 64, CCC1PinList(0))
        nSystErr = terM9_setChannelImpedance(hndlDTI, 64, CCC1PinList(0), TERM9_IMPED_LOWZ) 'new
        nSystErr = terM9_setChannelLoad(hndlDTI, 64, CCC1PinList(0), TERM9_LOAD_OFF)
        nSystErr = terM9_setChannelConnect(hndlDTI, 64, CCC1PinList(0), TERM9_RELAY_CLOSED)
        nSystErr = terM9_setChannelLevel(hndlDTI, 64, CCC1PinList(0), TTL_LEVELS)
        nSystErr = terM9_setSystemEnable(hndlDTI, VI_TRUE) ' new command
        nSystErr = terM9_setChannelChanMode(hndlDTI, 64, CCC1PinList(0), TERM9_CHANMODE_STATIC)
        nSystErr = terM9_setLevelSet(hndlDTI, TERM9_SCOPE_SYSTEM, TTL_LEVELS, 5.0, 0.0, 2.5, 2.4, 0.0, 0.0, 0.0, TERM9_SLEWRATE_LOW)
        nSystErr = terM9_setSystemEnable(hndlDTI, VI_TRUE) ' new command
        nSystErr = terM9_setStaticPatternDelay(hndlDTI, 0.0001) 'new 100uS
        nSystErr = terM9_setChannelPinOpcode(hndlDTI, CCC1, TERM9_OP_IHOL)
        nSystErr = terM9_runStaticPattern(hndlDTI, VI_TRUE, VI_TRUE)
        nSystErr = terM9_fetchStaticPinResults(hndlDTI, 64, CCC1PinList(0), resultCount, CCCPinResult(0))

        '    TERM9_SCOPE_CHANCARD(1) in Slot 3
        TestFailure = False
        For PinCount = 0 To 63
            If CCCPinResult(PinCount) = TERM9_RESULT_FAIL Then
                sTNum = "DTS-13-" & Format((PinCount), "000")
                Echo(FormatResults(False, "Channel " & CStr(PinCount) & " on CCC1 in slot 3 IHOL test", sTNum))
                RegisterFailure(DIGITAL, sTNum, , , , , "Channel " & CStr(PinCount) & " on CCC1 in slot 3 IHOL test")
                TestFailure = True
                Select Case PinCount
                    Case 0 : Echo("  P6-25(Ch0) to P6-57(GCh0)")
                    Case 1 : Echo("  P6-26(Ch1) to P6-58(GCh1)")
                    Case 2 : Echo("  P6-27(Ch2) to P6-59(GCh2)")
                    Case 3 : Echo("  P6-28(Ch3) to P6-60(GCh3)")
                    Case 4 : Echo("  P6-29(Ch4) to P6-61(GCh4)")
                    Case 5 : Echo("  P6-30(Ch5) to P6-62(GCh5)")
                    Case 6 : Echo("  P6-31(Ch6) to P6-63(GCh6)")
                    Case 7 : Echo("  P6-32(Ch7) to P6-64(GCh7)")
                    Case 8 : Echo("  P6-80(Ch8) to P6-81(GCh8)")
                    Case 9 : Echo("  P6-82(Ch9) to P6-83(GCh9)")
                    Case 10 : Echo("  P6-84(Ch10) to P6-85(GCh10)")
                    Case 11 : Echo("  P6-86(Ch11) to P6-87(GCh11)")
                    Case 12 : Echo("  P6-88(Ch12) to P6-89(GCh12)")
                    Case 13 : Echo("  P6-90(Ch13) to P6-91(GCh13)")
                    Case 14 : Echo("  P6-92(Ch14) to P6-93(GCh14)")
                    Case 15 : Echo("  P6-94(Ch15) to P6-96(FSIG GND0)")
                    Case 16 : Echo("  P6-24(Ch16) to P6-56(GCh16)")
                    Case 17 : Echo("  P6-23(Ch17) to P6-55(GCh17)")
                    Case 18 : Echo("  P6-22(Ch18) to P6-54(GCh18)")
                    Case 19 : Echo("  P6-21(Ch19) to P6-53(GCh19)")
                    Case 20 : Echo("  P6-20(Ch20) to P6-52(GCh20)")
                    Case 21 : Echo("  P6-19(Ch21) to P6-51(GCh21)")
                    Case 22 : Echo("  P6-18(Ch22) to P6-50(GCh22)")
                    Case 23 : Echo("  P6-17(Ch23) to P6-49(GCh23)")
                    Case 24 : Echo("  P6-16(Ch24) to P6-48(GCh24)")
                    Case 25 : Echo("  P6-15(Ch25) to P6-47(GCh25)")
                    Case 26 : Echo("  P6-14(Ch26) to P6-46(GCh26)")
                    Case 27 : Echo("  P6-13(Ch27) to P6-45(GCh27)")
                    Case 28 : Echo("  P6-12(Ch28) to P6-44(GCh28)")
                    Case 29 : Echo("  P6-11(Ch29) to P6-43(GCh29)")
                    Case 30 : Echo("  P6-10(Ch30) to P6-42(GCh30)")
                    Case 31 : Echo("  P6-09(Ch31) to P6-41(GCh31)")
                    Case 32 : Echo("  P6-08(Ch32) to P6-40(GCh32)")
                    Case 33 : Echo("  P6-07(Ch33) to P6-39(GCh33)")
                    Case 34 : Echo("  P6-06(Ch34) to P6-38(GCh34)")
                    Case 35 : Echo("  P6-05(Ch35) to P6-37(GCh35)")
                    Case 36 : Echo("  P6-04(Ch36) to P6-36(GCh36)")
                    Case 37 : Echo("  P6-03(Ch37) to P6-35(GCh37)")
                    Case 38 : Echo("  P6-02(Ch38) to P6-34(GCh38)")
                    Case 39 : Echo("  P6-01(Ch39) to P6-33(GCh39)")
                    Case 40 : Echo("  P7-56(Ch40) to P7-24(GCh40)")
                    Case 41 : Echo("  P7-55(Ch41) to P7-23(GCh41)")
                    Case 42 : Echo("  P7-54(Ch42) to P7-22(GCh42)")
                    Case 43 : Echo("  P7-53(Ch43) to P7-21(GCh43)")
                    Case 44 : Echo("  P7-52(Ch44) to P7-20(GCh44)")
                    Case 45 : Echo("  P7-51(Ch45) to P7-19(GCh45)")
                    Case 46 : Echo("  P7-50(Ch46) to P7-18(GCh46)")
                    Case 47 : Echo("  P7-49(Ch47) to P7-17(GCh47)")
                    Case 48 : Echo("  P7-48(Ch48) to P7-16(GCh48)")
                    Case 49 : Echo("  P7-47(Ch49) to P7-15(GCh49)")
                    Case 50 : Echo("  P7-46(Ch50) to P7-14(GCh50)")
                    Case 51 : Echo("  P7-45(Ch51) to P7-13(GCh51)")
                    Case 52 : Echo("  P7-44(Ch52) to P7-12(GCh52)")
                    Case 53 : Echo("  P7-43(Ch53) to P7-11(GCh53)")
                    Case 54 : Echo("  P7-42(Ch54) to P7-10(GCh54)")
                    Case 55 : Echo("  P7-41(Ch55) to P7-09(GCh55)")
                    Case 56 : Echo("  P7-40(Ch56) to P7-08(GCh56)")
                    Case 57 : Echo("  P7-39(Ch57) to P7-07(GCh57)")
                    Case 58 : Echo("  P7-38(Ch58) to P7-06(GCh58)")
                    Case 59 : Echo("  P7-37(Ch59) to P7-05(GCh59)")
                    Case 60 : Echo("  P7-36(Ch60) to P7-04(GCh60)")
                    Case 61 : Echo("  P7-35(Ch61) to P7-03(GCh61)")
                    Case 62 : Echo("  P7-34(Ch62) to P7-02(GCh62)")
                    Case 63 : Echo("  P7-33(Ch63) to P7-01(GCh63)")
                End Select
            End If
        Next PinCount

        If TestFailure Then
            TestDTS = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            Echo(FormatResults(True, "Slot 3 CCC1 all channels IHOL test", "DTS-13-000 to 063"))
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "DTS" And OptionStep = 16 Then
            GoTo DTS_13_1
        End If
        frmSTest.proProgress.Value = 42

        '**************
        '**  Test CCC2
        '**************
DTS_13_64:  'DTS-13-064 to DTS-13-127
        DigitalModule = 2
        'Set Ground Reference Interupt Handler
        GroundRefInterupt = False

        nSystErr = terM9_setChannelPinOpcode(hndlDTI, CCC1, TERM9_OP_IL)
        nSystErr = terM9_runStaticPattern(hndlDTI, VI_FALSE, VI_TRUE)
        Application.DoEvents()
        GroundRefInterupt = False

        '* Set in modes and disconnect relays in CCC2
        nSystErr = terM9_setLevelSet(hndlDTI, TERM9_SCOPE_SYSTEM, TTL_LEVELS, 5.0, 0.0, 2.5, 2.5, 0.0, 0.0, 0.0, TERM9_SLEWRATE_LOW)
        nSystErr = terM9_setSystemEnable(hndlDTI, VI_TRUE) ' new command
        nSystErr = terM9_setChannelChanMode(hndlDTI, 64, CCC2PinList(0), TERM9_CHANMODE_STATIC)
        nSystErr = terM9_setGroundReference(hndlDTI, TERM9_SCOPE_SYSTEM, TERM9_GROUND_INTERNAL)
        nSystErr = terM9_setChannelLoad(hndlDTI, 64, CCC2PinList(0), TERM9_LOAD_OFF)
        nSystErr = terM9_setChannelConnect(hndlDTI, 64, CCC2PinList(0), TERM9_RELAY_CLOSED)
        nSystErr = terM9_setPinmapGroup(hndlDTI, CCC2, 64, CCC2PinList(0))
        nSystErr = terM9_setChannelLevel(hndlDTI, 64, CCC2PinList(0), TTL_LEVELS)
        nSystErr = terM9_setStaticPatternDelay(hndlDTI, 0.0001)
        nSystErr = terM9_setSystemEnable(hndlDTI, VI_TRUE) ' new command
        nSystErr = terM9_setChannelPinOpcode(hndlDTI, CCC2, TERM9_OP_IHOL)
        nSystErr = terM9_runStaticPattern(hndlDTI, VI_FALSE, VI_TRUE)
        nSystErr = terM9_fetchStaticPinResults(hndlDTI, 64, CCC2PinList(0), resultCount, CCCPinResult(0))

        TestFailure = False
        For PinCount = 0 To 63
            If CCCPinResult(PinCount) = TERM9_RESULT_FAIL Then
                sTNum = "DTS-13-" & Format((PinCount + 64), "000")
                Echo(FormatResults(False, "Channel " & CStr(PinCount + 64) & " on CCC2 in slot 2 IHOL test", sTNum))
                RegisterFailure(DIGITAL, sTNum, , , , , "Channel " & CStr(PinCount + 64) & " on CCC2 in slot 2 IHOL test")
                TestFailure = True
                Select Case PinCount
                    Case 0 : Echo("  P4-25(Ch64) to P4-57(GCh64)")
                    Case 1 : Echo("  P4-26(Ch65) to P4-58(GCh65)")
                    Case 2 : Echo("  P4-27(Ch66) to P4-59(GCh66)")
                    Case 3 : Echo("  P4-28(Ch67) to P4-60(GCh67)")
                    Case 4 : Echo("  P4-29(Ch68) to P4-61(GCh68)")
                    Case 5 : Echo("  P4-30(Ch69) to P4-62(GCh69)")
                    Case 6 : Echo("  P4-31(Ch70) to P4-63(GCh70)")
                    Case 7 : Echo("  P4-32(Ch71) to P4-64(GCh71)")
                    Case 8 : Echo("  P4-80(Ch72) to P4-81(GCh72)")
                    Case 9 : Echo("  P4-82(Ch73) to P4-83(GCh73)")
                    Case 10 : Echo("  P4-84(Ch74) to P4-85(GCh74)")
                    Case 11 : Echo("  P4-86(Ch75) to P4-87(GCh75)")
                    Case 12 : Echo("  P4-88(Ch76) to P4-89(GCh76)")
                    Case 13 : Echo("  P4-90(Ch77) to P4-91(GCh77)")
                    Case 14 : Echo("  P4-92(Ch78) to P4-93(GCh78)")
                    Case 15 : Echo("  P4-94(Ch79) to P4-96(FSIG GND0)")
                    Case 16 : Echo("  P4-24(Ch80) to P4-56(GCh80)")
                    Case 17 : Echo("  P4-23(Ch81) to P4-55(GCh81)")
                    Case 18 : Echo("  P4-22(Ch82) to P4-54(GCh82)")
                    Case 19 : Echo("  P4-21(Ch83) to P4-53(GCh83)")
                    Case 20 : Echo("  P4-20(Ch84) to P4-52(GCh84)")
                    Case 21 : Echo("  P4-19(Ch85) to P4-51(GCh85)")
                    Case 22 : Echo("  P4-18(Ch86) to P4-50(GCh86)")
                    Case 23 : Echo("  P4-17(Ch87) to P4-49(GCh87)")
                    Case 24 : Echo("  P4-16(Ch88) to P4-48(GCh88)")
                    Case 25 : Echo("  P4-15(Ch89) to P4-47(GCh89)")
                    Case 26 : Echo("  P4-14(Ch90) to P4-46(GCh90)")
                    Case 27 : Echo("  P4-13(Ch91) to P4-45(GCh91)")
                    Case 28 : Echo("  P4-12(Ch92) to P4-44(GCh92)")
                    Case 29 : Echo("  P4-11(Ch93) to P4-43(GCh93)")
                    Case 30 : Echo("  P4-10(Ch94) to P4-42(GCh94)")
                    Case 31 : Echo("  P4-09(Ch95) to P4-41(GCh95)")
                    Case 32 : Echo("  P4-08(Ch96) to P4-40(GCh96)")
                    Case 33 : Echo("  P4-07(Ch97) to P4-39(GCh97)")
                    Case 34 : Echo("  P4-06(Ch98) to P4-38(GCh98)")
                    Case 35 : Echo("  P4-05(Ch99) to P4-37(GCh99)")
                    Case 36 : Echo("  P4-04(Ch100) to P4-36(GCh100)")
                    Case 37 : Echo("  P4-03(Ch101) to P4-35(GCh101)")
                    Case 38 : Echo("  P4-02(Ch102) to P4-34(GCh102)")
                    Case 39 : Echo("  P4-01(Ch103) to P4-33(GCh103)")
                    Case 40 : Echo("  P5-56(Ch104) to P5-24(GCh104)")
                    Case 41 : Echo("  P5-55(Ch105) to P5-23(GCh105)")
                    Case 42 : Echo("  P5-54(Ch106) to P5-22(GCh106)")
                    Case 43 : Echo("  P5-53(Ch107) to P5-21(GCh107)")
                    Case 44 : Echo("  P5-52(Ch108) to P5-20(GCh108)")
                    Case 45 : Echo("  P5-51(Ch109) to P5-19(GCh109)")
                    Case 46 : Echo("  P5-50(Ch110) to P5-18(GCh110)")
                    Case 47 : Echo("  P5-49(Ch111) to P5-17(GCh111)")
                    Case 48 : Echo("  P5-48(Ch112) to P5-16(GCh112)")
                    Case 49 : Echo("  P5-47(Ch113) to P5-15(GCh113)")
                    Case 50 : Echo("  P5-46(Ch114) to P5-14(GCh114)")
                    Case 51 : Echo("  P5-45(Ch115) to P5-13(GCh115)")
                    Case 52 : Echo("  P5-44(Ch116) to P5-12(GCh116)")
                    Case 53 : Echo("  P5-43(Ch117) to P5-11(GCh117)")
                    Case 54 : Echo("  P5-42(Ch118) to P5-10(GCh118)")
                    Case 55 : Echo("  P5-41(Ch119) to P5-09(GCh119)")
                    Case 56 : Echo("  P5-40(Ch120) to P5-08(GCh120)")
                    Case 57 : Echo("  P5-39(Ch121) to P5-07(GCh121)")
                    Case 58 : Echo("  P5-38(Ch122) to P5-06(GCh122)")
                    Case 59 : Echo("  P5-37(Ch123) to P5-05(GCh123)")
                    Case 60 : Echo("  P5-36(Ch124) to P5-04(GCh124)")
                    Case 61 : Echo("  P5-35(Ch125) to P5-03(GCh125)")
                    Case 62 : Echo("  P5-34(Ch126) to P5-02(GCh126)")
                    Case 63 : Echo("  P5-33(Ch127) to P5-01(GCh127)")
                End Select
            End If
            Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete
        Next PinCount

        If TestFailure Then
            TestDTS = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            Echo(FormatResults(True, "Slot 2 CCC2 all channels IHOL test", "DTS-13-064 to 127"))
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "DTS" And OptionStep = 17 Then
            GoTo DTS_13_64
        End If
        frmSTest.proProgress.Value = 45

        '**************
        '**  Test CCC3
        '**************
DTS_13_128:  'DTS-13-128 to DTS-13-191
        DigitalModule = 1
        nSystErr = terM9_setChannelPinOpcode(hndlDTI, CCC2, TERM9_OP_IL)
        nSystErr = terM9_runStaticPattern(hndlDTI, VI_FALSE, VI_TRUE)
        Application.DoEvents()
        GroundRefInterupt = False

        Application.DoEvents()
        '* Set in modes and disconnect relays in CCC3

        nSystErr = terM9_setLevelSet(hndlDTI, TERM9_SCOPE_SYSTEM, TTL_LEVELS, 5.0, 0.0, 2.5, 2.5, 0.0, 0.0, 0.0, TERM9_SLEWRATE_LOW)
        nSystErr = terM9_setSystemEnable(hndlDTI, VI_TRUE) ' new command
        nSystErr = terM9_setChannelChanMode(hndlDTI, 64, CCC3PinList(0), TERM9_CHANMODE_STATIC)
        nSystErr = terM9_setGroundReference(hndlDTI, TERM9_SCOPE_SYSTEM, TERM9_GROUND_INTERNAL)
        nSystErr = terM9_setChannelLoad(hndlDTI, 64, CCC3PinList(0), TERM9_LOAD_OFF)
        nSystErr = terM9_setChannelConnect(hndlDTI, 64, CCC3PinList(0), TERM9_RELAY_CLOSED)
        nSystErr = terM9_setPinmapGroup(hndlDTI, CCC3, 64, CCC3PinList(0))
        nSystErr = terM9_setChannelLevel(hndlDTI, 64, CCC3PinList(0), TTL_LEVELS)
        nSystErr = terM9_setStaticPatternDelay(hndlDTI, 0.0001)
        nSystErr = terM9_setSystemEnable(hndlDTI, VI_TRUE) ' new command
        nSystErr = terM9_setChannelPinOpcode(hndlDTI, CCC3, TERM9_OP_IHOL)
        nSystErr = terM9_runStaticPattern(hndlDTI, VI_FALSE, VI_TRUE)
        nSystErr = terM9_fetchStaticPinResults(hndlDTI, 64, CCC3PinList(0), resultCount, CCCPinResult(0))

        TestFailure = False
        For PinCount = 0 To 63
            If CCCPinResult(PinCount) = TERM9_RESULT_FAIL Then
                sTNum = "DTS-13-" & Format((PinCount + 128), "000")
                Echo(FormatResults(False, "Channel " & CStr(PinCount + 128) & " on CCC3 in slot 1 IHOL test", sTNum))
                RegisterFailure(DIGITAL, sTNum, , , , , "Channel " & CStr(PinCount + 128) & " on CCC3 in slot 1 IHOL test")
                TestFailure = True
                Select Case PinCount
                    Case 0 : Echo("  P2-25(Ch128) to P2-57(GCh128)")
                    Case 1 : Echo("  P2-26(Ch129) to P2-58(GCh129)")
                    Case 2 : Echo("  P2-27(Ch130) to P2-59(GCh130)")
                    Case 3 : Echo("  P2-28(Ch131) to P2-60(GCh131)")
                    Case 4 : Echo("  P2-29(Ch132) to P2-61(GCh132)")
                    Case 5 : Echo("  P2-30(Ch133) to P2-62(GCh133)")
                    Case 6 : Echo("  P2-31(Ch134) to P2-63(GCh134)")
                    Case 7 : Echo("  P2-32(Ch135) to P2-64(GCh135)")
                    Case 8 : Echo("  P2-80(Ch136) to P2-81(GCh136)")
                    Case 9 : Echo("  P2-82(Ch137) to P2-83(GCh137)")
                    Case 10 : Echo("  P2-84(Ch138) to P2-85(GCh138)")
                    Case 11 : Echo("  P2-86(Ch139) to P2-87(GCh139)")
                    Case 12 : Echo("  P2-88(Ch140) to P2-89(GCh140)")
                    Case 13 : Echo("  P2-90(Ch141) to P2-91(GCh141)")
                    Case 14 : Echo("  P2-92(Ch142) to P2-93(GCh142)")
                    Case 15 : Echo("  P2-94(Ch143) to P2-96(FSIG GND2)")
                    Case 16 : Echo("  P2-24(Ch144) to P2-56(GCh144)")
                    Case 17 : Echo("  P2-23(Ch145) to P2-55(GCh145)")
                    Case 18 : Echo("  P2-22(Ch146) to P2-54(GCh146)")
                    Case 19 : Echo("  P2-21(Ch147) to P2-53(GCh147)")
                    Case 20 : Echo("  P2-20(Ch148) to P2-52(GCh148)")
                    Case 21 : Echo("  P2-19(Ch149) to P2-51(GCh149)")
                    Case 22 : Echo("  P2-18(Ch150) to P2-50(GCh150)")
                    Case 23 : Echo("  P2-17(Ch151) to P2-49(GCh151)")
                    Case 24 : Echo("  P2-16(Ch152) to P2-48(GCh152)")
                    Case 25 : Echo("  P2-15(Ch153) to P2-47(GCh153)")
                    Case 26 : Echo("  P2-14(Ch154) to P2-46(GCh154)")
                    Case 27 : Echo("  P2-13(Ch155) to P2-45(GCh155)")
                    Case 28 : Echo("  P2-12(Ch156) to P2-44(GCh156)")
                    Case 29 : Echo("  P2-11(Ch157) to P2-43(GCh157)")
                    Case 30 : Echo("  P2-10(Ch158) to P2-42(GCh158)")
                    Case 31 : Echo("  P2-09(Ch159) to P2-41(GCh159)")
                    Case 32 : Echo("  P2-08(Ch160) to P2-40(GCh160)")
                    Case 33 : Echo("  P2-07(Ch161) to P2-39(GCh161)")
                    Case 34 : Echo("  P2-06(Ch162) to P2-38(GCh162)")
                    Case 35 : Echo("  P2-05(Ch163) to P2-37(GCh163)")
                    Case 36 : Echo("  P2-04(Ch164) to P2-36(GCh164)")
                    Case 37 : Echo("  P2-03(Ch165) to P2-35(GCh165)")
                    Case 38 : Echo("  P2-02(Ch166) to P2-34(GCh166)")
                    Case 39 : Echo("  P2-01(Ch167) to P2-33(GCh167)")
                    Case 40 : Echo("  P3-56(Ch168) to P3-24(GCh168)")
                    Case 41 : Echo("  P3-55(Ch169) to P3-23(GCh169)")
                    Case 42 : Echo("  P3-54(Ch170) to P3-22(GCh170)")
                    Case 43 : Echo("  P3-53(Ch171) to P3-21(GCh171)")
                    Case 44 : Echo("  P3-52(Ch172) to P3-20(GCh172)")
                    Case 45 : Echo("  P3-51(Ch173) to P3-19(GCh173)")
                    Case 46 : Echo("  P3-50(Ch174) to P3-18(GCh174)")
                    Case 47 : Echo("  P3-49(Ch175) to P3-17(GCh175)")
                    Case 48 : Echo("  P3-48(Ch176) to P3-16(GCh176)")
                    Case 49 : Echo("  P3-47(Ch177) to P3-15(GCh177)")
                    Case 50 : Echo("  P3-46(Ch178) to P3-14(GCh178)")
                    Case 51 : Echo("  P3-45(Ch179) to P3-13(GCh179)")
                    Case 52 : Echo("  P3-44(Ch180) to P3-12(GCh180)")
                    Case 53 : Echo("  P3-43(Ch181) to P3-11(GCh181)")
                    Case 54 : Echo("  P3-42(Ch182) to P3-10(GCh182)")
                    Case 55 : Echo("  P3-41(Ch183) to P3-09(GCh183)")
                    Case 56 : Echo("  P3-40(Ch184) to P3-08(GCh184)")
                    Case 57 : Echo("  P3-39(Ch185) to P3-07(GCh185)")
                    Case 58 : Echo("  P3-38(Ch186) to P3-06(GCh186)")
                    Case 59 : Echo("  P3-37(Ch187) to P3-05(GCh187)")
                    Case 60 : Echo("  P3-36(Ch188) to P3-04(GCh188)")
                    Case 61 : Echo("  P3-35(Ch189) to P3-03(GCh189)")
                    Case 62 : Echo("  P3-34(Ch190) to P3-02(GCh190)")
                    Case 63 : Echo("  P3-33(Ch191) to P3-01(GCh191)")
                End Select
            End If
        Next PinCount

        If TestFailure Then
            TestDTS = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            Echo(FormatResults(True, "Slot 1 CCC3 all channels IHOL test", "DTS-13-128 to 191"))
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "DTS" And OptionStep = 18 Then
            GoTo DTS_13_128
        End If
        LoopTestNo = 19
        frmSTest.proProgress.Value = 47

DTS_14_1:  ' DTS-14-001  CCRB Status Test
        DigitalModule = 4
        nSystErr = terM9_setChannelPinOpcode(hndlDTI, CCC3, TERM9_OP_IL)
        nSystErr = terM9_runStaticPattern(hndlDTI, VI_FALSE, VI_TRUE)
        Application.DoEvents()
        GroundRefInterupt = False
        nSystErr = terM9_initializeInstrument(hndlDTI)
        nSystErr = terM9_close(hndlDTI)

        InitStatus = terM9_init("TERM9::#0", VI_TRUE, VI_TRUE, hndlDTI) 'gets new handle

        If InitStatus > &H4FFF0000 Then
            TestDTS = FAILED
            Echo(FormatResults(False, "CCRB Initialization Test", "DTS-14-001"))
            Echo("ERROR - " & CStr(InitStatus) & " Most likely faulty module: CCRB.")
            RegisterFailure(DIGITAL, "DTS-14-001", , , , , "CCRB Initialization Test")
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            Echo(FormatResults(True, "CCRB Initialization test", "DTS-14-001"))
            IncStepPassed()
        End If

        instrumentHandle(DIGITAL) = hndlDTI
        nSystErr = terM9_initializeInstrument(hndlDTI)
        nSystErr = terM9_setSystemEnable(hndlDTI, VI_TRUE)
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "DTS" And OptionStep = 19 Then
            GoTo DTS_14_1
        End If
        LoopTestNo = 20
        frmSTest.proProgress.Value = 50
        If AbortTest = True Then GoTo TestComplete
        Echo("Remove SAIF")

        '*********************************************************************************
        '
        '                                   REMOVE THE SAIF
        '
        '*********************************************************************************

RemoveSAIF:  ' remove the saif even if receiver switch says it was already removed
        S = "Disconnect the SAIF from the tester. " & vbCrLf & vbCrLf
        S &= "DO NOT remove the Digital Probe at this time."
        DisplaySetup(S, "TETS SAIF_REM.jpg", 1)
        If AbortTest = True Then GoTo TestComplete
        If ReceiverSwitchOK = True Then
            If fnSAIFinstalled(False) = True Then
                sMsg = "Are you sure that you removed the SAIF from the tester?" & vbCrLf
                x = MsgBox(sMsg, vbYesNo)
                If x = vbNo Then GoTo RemoveSAIF ' try again
                If fnSAIFinstalled(False) = True Then
                    Echo("   *******************************************")
                    Echo(FormatResults(False, "Receiver ITA switch open test failed."), 4)
                    Echo("   The Receiver ITA Switch on the tester must be shorted.", 4)
                    Echo("   *******************************************")
                    sMsg = "The Receiver ITA Switch on the tester must be shorted." & vbCrLf & "Do you want to continue?"
                    x = MsgBox(sMsg, vbYesNo)
                    If x = vbNo Then
                        GoTo TestComplete
                    End If
                End If
            End If
        End If
        SAIFinstalled = False ' this will instruct the operator to reinstall the switch at the end of the DTS test.

DTS_15_0:  'DTS-15-000 to DTS-15-063  ***Shorted Pin Test***
        DigitalModule = 3
        'Channel card 1 - SLOT 3 CCC1 all channels set low
        'Set all pins on card to output a low
        nSystErr = terM9_setLevelSet(hndlDTI, TERM9_SCOPE_SYSTEM, TTL_LEVELS, 5.0, 0.0, 4.0, 2.5, 0.0, 0.0, 0.0, TERM9_SLEWRATE_LOW)
        nSystErr = terM9_setSystemEnable(hndlDTI, VI_TRUE)
        nSystErr = terM9_setChannelChanMode(hndlDTI, 64, CCC1PinList(0), TERM9_CHANMODE_STATIC)
        nSystErr = terM9_setGroundReference(hndlDTI, TERM9_SCOPE_SYSTEM, TERM9_GROUND_INTERNAL)
        nSystErr = terM9_setLowPower(hndlDTI, TERM9_SCOPE_SYSTEM, VI_FALSE)
        nSystErr = terM9_setChannelLoad(hndlDTI, 64, CCC1PinList(0), TERM9_LOAD_OFF)
        nSystErr = terM9_setChannelConnect(hndlDTI, 64, CCC1PinList(0), TERM9_RELAY_CLOSED)
        nSystErr = terM9_setPinmapGroup(hndlDTI, CCC1, 64, CCC1PinList(0))
        nSystErr = terM9_setChannelLevel(hndlDTI, 64, CCC1PinList(0), TTL_LEVELS)
        nSystErr = terM9_setSystemEnable(hndlDTI, VI_TRUE)
        nSystErr = terM9_setChannelPinOpcode(hndlDTI, CCC1, TERM9_OP_ML)
        nSystErr = terM9_setStaticPatternDelay(hndlDTI, 0.00001)
        nSystErr = terM9_runStaticPattern(hndlDTI, VI_FALSE, VI_TRUE)
        nSystErr = terM9_fetchStaticPinResults(hndlDTI, 64, CCC1PinList(0), resultCount, CCCPinResult(0))

        '    TERM9_SCOPE_CHANCARD(1)
        TestFailure = False
        For PinCount = 0 To 63
            If CCCPinResult(PinCount) = TERM9_RESULT_FAIL Then
                sTNum = "DTS-15-" & Format(PinCount, "000")
                Echo(FormatResults(False, "Channel " & CStr(PinCount) & " on CCC1 in slot 3 ML test", sTNum))
                RegisterFailure(DIGITAL, sTNum, , , , , "Channel " & CStr(PinCount) & " on CCC1 in slot 3 ML test")
                TestFailure = True
                Select Case PinCount
                    Case 0 : Echo("  P6-25(Ch0) to P6-57(GCh0)")
                    Case 1 : Echo("  P6-26(Ch1) to P6-58(GCh1)")
                    Case 2 : Echo("  P6-27(Ch2) to P6-59(GCh2)")
                    Case 3 : Echo("  P6-28(Ch3) to P6-60(GCh3)")
                    Case 4 : Echo("  P6-29(Ch4) to P6-61(GCh4)")
                    Case 5 : Echo("  P6-30(Ch5) to P6-62(GCh5)")
                    Case 6 : Echo("  P6-31(Ch6) to P6-63(GCh6)")
                    Case 7 : Echo("  P6-32(Ch7) to P6-64(GCh7)")
                    Case 8 : Echo("  P6-80(Ch8) to P6-81(GCh8)")
                    Case 9 : Echo("  P6-82(Ch9) to P6-83(GCh9)")
                    Case 10 : Echo("  P6-84(Ch10) to P6-85(GCh10)")
                    Case 11 : Echo("  P6-86(Ch11) to P6-87(GCh11)")
                    Case 12 : Echo("  P6-88(Ch12) to P6-89(GCh12)")
                    Case 13 : Echo("  P6-90(Ch13) to P6-91(GCh13)")
                    Case 14 : Echo("  P6-92(Ch14) to P6-93(GCh14)")
                    Case 15 : Echo("  P6-94(Ch15) to P6-95(GCh15)")
                    Case 16 : Echo("  P6-24(Ch16) to P6-56(GCh16)")
                    Case 17 : Echo("  P6-23(Ch17) to P6-55(GCh17)")
                    Case 18 : Echo("  P6-22(Ch18) to P6-54(GCh18)")
                    Case 19 : Echo("  P6-21(Ch19) to P6-53(GCh19)")
                    Case 20 : Echo("  P6-20(Ch20) to P6-52(GCh20)")
                    Case 21 : Echo("  P6-19(Ch21) to P6-51(GCh21)")
                    Case 22 : Echo("  P6-18(Ch22) to P6-50(GCh22)")
                    Case 23 : Echo("  P6-17(Ch23) to P6-49(GCh23)")
                    Case 24 : Echo("  P6-16(Ch24) to P6-48(GCh24)")
                    Case 25 : Echo("  P6-15(Ch25) to P6-47(GCh25)")
                    Case 26 : Echo("  P6-14(Ch26) to P6-46(GCh26)")
                    Case 27 : Echo("  P6-13(Ch27) to P6-45(GCh27)")
                    Case 28 : Echo("  P6-12(Ch28) to P6-44(GCh28)")
                    Case 29 : Echo("  P6-11(Ch29) to P6-43(GCh29)")
                    Case 30 : Echo("  P6-10(Ch30) to P6-42(GCh30)")
                    Case 31 : Echo("  P6-09(Ch31) to P6-41(GCh31)")
                    Case 32 : Echo("  P6-08(Ch32) to P6-40(GCh32)")
                    Case 33 : Echo("  P6-07(Ch33) to P6-39(GCh33)")
                    Case 34 : Echo("  P6-06(Ch34) to P6-38(GCh34)")
                    Case 35 : Echo("  P6-05(Ch35) to P6-37(GCh35)")
                    Case 36 : Echo("  P6-04(Ch36) to P6-36(GCh36)")
                    Case 37 : Echo("  P6-03(Ch37) to P6-35(GCh37)")
                    Case 38 : Echo("  P6-02(Ch38) to P6-34(GCh38)")
                    Case 39 : Echo("  P6-01(Ch39) to P6-33(GCh39)")
                    Case 40 : Echo("  P7-56(Ch40) to P7-34(GCh40)")
                    Case 41 : Echo("  P7-55(Ch41) to P7-33(GCh41)")
                    Case 42 : Echo("  P7-54(Ch42) to P7-32(GCh42)")
                    Case 43 : Echo("  P7-53(Ch43) to P7-31(GCh43)")
                    Case 44 : Echo("  P7-52(Ch44) to P7-30(GCh44)")
                    Case 45 : Echo("  P7-51(Ch45) to P7-29(GCh45)")
                    Case 46 : Echo("  P7-50(Ch46) to P7-28(GCh46)")
                    Case 47 : Echo("  P7-49(Ch47) to P7-27(GCh47)")
                    Case 48 : Echo("  P7-48(Ch48) to P7-26(GCh48)")
                    Case 49 : Echo("  P7-47(Ch49) to P7-25(GCh49)")
                    Case 50 : Echo("  P7-46(Ch50) to P7-24(GCh50)")
                    Case 51 : Echo("  P7-45(Ch51) to P7-23(GCh51)")
                    Case 52 : Echo("  P7-44(Ch52) to P7-22(GCh52)")
                    Case 53 : Echo("  P7-43(Ch53) to P7-21(GCh53)")
                    Case 54 : Echo("  P7-42(Ch54) to P7-20(GCh54)")
                    Case 55 : Echo("  P7-41(Ch55) to P7-09(GCh55)")
                    Case 56 : Echo("  P7-40(Ch56) to P7-08(GCh56)")
                    Case 57 : Echo("  P7-39(Ch57) to P7-07(GCh57)")
                    Case 58 : Echo("  P7-38(Ch58) to P7-06(GCh58)")
                    Case 59 : Echo("  P7-37(Ch59) to P7-05(GCh59)")
                    Case 60 : Echo("  P7-36(Ch60) to P7-04(GCh60)")
                    Case 61 : Echo("  P7-35(Ch61) to P7-03(GCh61)")
                    Case 62 : Echo("  P7-34(Ch62) to P7-02(GCh62)")
                    Case 63 : Echo("  P7-33(Ch63) to P7-01(GCh63)")
                End Select
            End If
            Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete
        Next PinCount

        If TestFailure Then
            TestDTS = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            Echo(FormatResults(True, "Slot 3 CCC1 all channels ML test", "DTS-15-000 to 063"))
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "DTS" And OptionStep = 20 Then
            GoTo DTS_15_0
        End If
        frmSTest.proProgress.Value = 52


DTS_16_0:  'DTS-16-000 to DTS-16-063    'Start walking a one through the card
        For PinCount = 0 To 63
            'MH 0; OL 1-63
            'MH 1; OL 0,2-63
            'MH 2; OL 0-1,3-63
            'MH 3; OL 0-2,4-63
            nSystErr = terM9_setChannelPinOpcode(hndlDTI, CCC1PinList(PinCount), TERM9_OP_MH)
            nSystErr = terM9_runStaticPattern(hndlDTI, VI_FALSE, VI_TRUE)
            nSystErr = terM9_fetchStaticPinResults(hndlDTI, 64, CCC1PinList(0), resultCount, CCCPinResult(0))
            If CCCPinResult(PinCount) = TERM9_RESULT_FAIL Then
                sTNum = "DTS-16-" & Format((PinCount), "000")
                Echo(FormatResults(False, "Channel " & CStr(PinCount) & " on CCC1 in slot 3 MH test", sTNum))
                RegisterFailure(DIGITAL, sTNum, , , , , "Channel " & CStr(PinCount) & " on CCC1 in slot 3 MH test")
                TestFailure = True
                Select Case PinCount
                    Case 0 : Echo("  P6-25(Ch0) to P6-57(GCh0)")
                    Case 1 : Echo("  P6-26(Ch1) to P6-58(GCh1)")
                    Case 2 : Echo("  P6-27(Ch2) to P6-59(GCh2)")
                    Case 3 : Echo("  P6-28(Ch3) to P6-60(GCh3)")
                    Case 4 : Echo("  P6-29(Ch4) to P6-61(GCh4)")
                    Case 5 : Echo("  P6-30(Ch5) to P6-62(GCh5)")
                    Case 6 : Echo("  P6-31(Ch6) to P6-63(GCh6)")
                    Case 7 : Echo("  P6-32(Ch7) to P6-64(GCh7)")
                    Case 8 : Echo("  P6-80(Ch8) to P6-81(GCh8)")
                    Case 9 : Echo("  P6-82(Ch9) to P6-83(GCh9)")
                    Case 10 : Echo("  P6-84(Ch10) to P6-85(GCh10)")
                    Case 11 : Echo("  P6-86(Ch11) to P6-87(GCh11)")
                    Case 12 : Echo("  P6-88(Ch12) to P6-89(GCh12)")
                    Case 13 : Echo("  P6-90(Ch13) to P6-91(GCh13)")
                    Case 14 : Echo("  P6-92(Ch14) to P6-93(GCh14)")
                    Case 15 : Echo("  P6-94(Ch15) to P6-95(GCh15)")
                    Case 16 : Echo("  P6-24(Ch16) to P6-56(GCh16)")
                    Case 17 : Echo("  P6-23(Ch17) to P6-55(GCh17)")
                    Case 18 : Echo("  P6-22(Ch18) to P6-54(GCh18)")
                    Case 19 : Echo("  P6-21(Ch19) to P6-53(GCh19)")
                    Case 20 : Echo("  P6-20(Ch20) to P6-52(GCh20)")
                    Case 21 : Echo("  P6-19(Ch21) to P6-51(GCh21)")
                    Case 22 : Echo("  P6-18(Ch22) to P6-50(GCh22)")
                    Case 23 : Echo("  P6-17(Ch23) to P6-49(GCh23)")
                    Case 24 : Echo("  P6-16(Ch24) to P6-48(GCh24)")
                    Case 25 : Echo("  P6-15(Ch25) to P6-47(GCh25)")
                    Case 26 : Echo("  P6-14(Ch26) to P6-46(GCh26)")
                    Case 27 : Echo("  P6-13(Ch27) to P6-45(GCh27)")
                    Case 28 : Echo("  P6-12(Ch28) to P6-44(GCh28)")
                    Case 29 : Echo("  P6-11(Ch29) to P6-43(GCh29)")
                    Case 30 : Echo("  P6-10(Ch30) to P6-42(GCh30)")
                    Case 31 : Echo("  P6-09(Ch31) to P6-41(GCh31)")
                    Case 32 : Echo("  P6-08(Ch32) to P6-40(GCh32)")
                    Case 33 : Echo("  P6-07(Ch33) to P6-39(GCh33)")
                    Case 34 : Echo("  P6-06(Ch34) to P6-38(GCh34)")
                    Case 35 : Echo("  P6-05(Ch35) to P6-37(GCh35)")
                    Case 36 : Echo("  P6-04(Ch36) to P6-36(GCh36)")
                    Case 37 : Echo("  P6-03(Ch37) to P6-35(GCh37)")
                    Case 38 : Echo("  P6-02(Ch38) to P6-34(GCh38)")
                    Case 39 : Echo("  P6-01(Ch39) to P6-33(GCh39)")
                    Case 40 : Echo("  P7-56(Ch40) to P7-24(GCh40)")
                    Case 41 : Echo("  P7-55(Ch41) to P7-23(GCh41)")
                    Case 42 : Echo("  P7-54(Ch42) to P7-22(GCh42)")
                    Case 43 : Echo("  P7-53(Ch43) to P7-21(GCh43)")
                    Case 44 : Echo("  P7-52(Ch44) to P7-20(GCh44)")
                    Case 45 : Echo("  P7-51(Ch45) to P7-19(GCh45)")
                    Case 46 : Echo("  P7-50(Ch46) to P7-18(GCh46)")
                    Case 47 : Echo("  P7-49(Ch47) to P7-17(GCh47)")
                    Case 48 : Echo("  P7-48(Ch48) to P7-16(GCh48)")
                    Case 49 : Echo("  P7-47(Ch49) to P7-15(GCh49)")
                    Case 50 : Echo("  P7-46(Ch50) to P7-14(GCh50)")
                    Case 51 : Echo("  P7-45(Ch51) to P7-13(GCh51)")
                    Case 52 : Echo("  P7-44(Ch52) to P7-12(GCh52)")
                    Case 53 : Echo("  P7-43(Ch53) to P7-11(GCh53)")
                    Case 54 : Echo("  P7-42(Ch54) to P7-10(GCh54)")
                    Case 55 : Echo("  P7-41(Ch55) to P7-09(GCh55)")
                    Case 56 : Echo("  P7-40(Ch56) to P7-08(GCh56)")
                    Case 57 : Echo("  P7-39(Ch57) to P7-07(GCh57)")
                    Case 58 : Echo("  P7-38(Ch58) to P7-06(GCh58)")
                    Case 59 : Echo("  P7-37(Ch59) to P7-05(GCh59)")
                    Case 60 : Echo("  P7-36(Ch60) to P7-04(GCh60)")
                    Case 61 : Echo("  P7-35(Ch61) to P7-03(GCh61)")
                    Case 62 : Echo("  P7-34(Ch62) to P7-02(GCh62)")
                    Case 63 : Echo("  P7-33(Ch63) to P7-01(GCh63)")
                End Select
            End If
            nSystErr = terM9_setChannelPinOpcode(hndlDTI, CCC1PinList(PinCount), TERM9_OP_ML)
            nSystErr = terM9_runStaticPattern(hndlDTI, VI_FALSE, VI_TRUE)

            Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete
        Next PinCount

        If TestFailure Then
            TestDTS = FAILED
            Echo("******************************")
            Echo("Check failed pins for possible")
            Echo("shorted signal to return.     ")
            Echo("******************************")
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            Echo(FormatResults(True, "Slot 3 CCC1 walking ones MH test", "DTS-16-000 to 063"))
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "DTS" And OptionStep = 21 Then
            GoTo DTS_16_0
        End If
        frmSTest.proProgress.Value = 55

        'Channel card 2
DTS_15_64:  'DTS-15-064 to DTS-15-127
        DigitalModule = 2

        'Set all pins on card to output a low
        nSystErr = terM9_setStaticPatternDelay(hndlDTI, 0.0001)
        nSystErr = terM9_setLevelSet(hndlDTI, TERM9_SCOPE_SYSTEM, TTL_LEVELS, 5.0, 0.0, 4.0, 2.5, 0.0, 0.0, 0.0, TERM9_SLEWRATE_LOW)
        nSystErr = terM9_setSystemEnable(hndlDTI, VI_TRUE)
        nSystErr = terM9_setChannelChanMode(hndlDTI, 64, CCC2PinList(0), TERM9_CHANMODE_STATIC)
        nSystErr = terM9_setGroundReference(hndlDTI, TERM9_SCOPE_SYSTEM, TERM9_GROUND_INTERNAL)
        nSystErr = terM9_setLowPower(hndlDTI, TERM9_SCOPE_SYSTEM, VI_FALSE)
        nSystErr = terM9_setChannelLoad(hndlDTI, 64, CCC2PinList(0), TERM9_LOAD_OFF)
        nSystErr = terM9_setChannelConnect(hndlDTI, 64, CCC2PinList(0), TERM9_RELAY_CLOSED)
        nSystErr = terM9_setPinmapGroup(hndlDTI, CCC2, 64, CCC2PinList(0))
        nSystErr = terM9_setChannelLevel(hndlDTI, 64, CCC2PinList(0), TTL_LEVELS)
        nSystErr = terM9_setSystemEnable(hndlDTI, VI_TRUE)
        nSystErr = terM9_setChannelPinOpcode(hndlDTI, CCC2, TERM9_OP_ML)
        nSystErr = terM9_runStaticPattern(hndlDTI, VI_FALSE, VI_TRUE)
        nSystErr = terM9_fetchStaticPinResults(hndlDTI, 64, CCC2PinList(0), resultCount, CCCPinResult(0))

        '    TERM9_SCOPE_CHANCARD(2)
        TestFailure = False
        For PinCount = 0 To 63 ' test all pins for output low
            If CCCPinResult(PinCount) = TERM9_RESULT_FAIL Then
                sTNum = "DTS-15-" & Format((PinCount + 64), "000")
                Echo(FormatResults(False, "Channel " & CStr(PinCount + 64) & " on CCC2 in slot 2 ML test", sTNum))
                RegisterFailure(DIGITAL, sTNum, , , , , "Channel " & CStr(PinCount + 64) & " on CCC2 in slot 2 ML test")
                TestFailure = True
                Select Case PinCount
                    Case 0 : Echo("  P4-25(Ch64) to P4-57(GCh64)")
                    Case 1 : Echo("  P4-26(Ch65) to P4-58(GCh65)")
                    Case 2 : Echo("  P4-27(Ch66) to P4-59(GCh66)")
                    Case 3 : Echo("  P4-28(Ch67) to P4-60(GCh67)")
                    Case 4 : Echo("  P4-29(Ch68) to P4-61(GCh68)")
                    Case 5 : Echo("  P4-30(Ch69) to P4-62(GCh69)")
                    Case 6 : Echo("  P4-31(Ch70) to P4-63(GCh70)")
                    Case 7 : Echo("  P4-32(Ch71) to P4-64(GCh71)")
                    Case 8 : Echo("  P4-80(Ch72) to P4-81(GCh72)")
                    Case 9 : Echo("  P4-82(Ch73) to P4-83(GCh73)")
                    Case 10 : Echo("  P4-84(Ch74) to P4-85(GCh74)")
                    Case 11 : Echo("  P4-86(Ch75) to P4-87(GCh75)")
                    Case 12 : Echo("  P4-88(Ch76) to P4-89(GCh76)")
                    Case 13 : Echo("  P4-90(Ch77) to P4-91(GCh77)")
                    Case 14 : Echo("  P4-92(Ch78) to P4-93(GCh78)")
                    Case 15 : Echo("  P4-94(Ch79) to P4-95(GCh79)")
                    Case 16 : Echo("  P4-24(Ch80) to P4-56(GCh80)")
                    Case 17 : Echo("  P4-23(Ch81) to P4-55(GCh81)")
                    Case 18 : Echo("  P4-22(Ch82) to P4-54(GCh82)")
                    Case 19 : Echo("  P4-21(Ch83) to P4-53(GCh83)")
                    Case 20 : Echo("  P4-20(Ch84) to P4-52(GCh84)")
                    Case 21 : Echo("  P4-19(Ch85) to P4-51(GCh85)")
                    Case 22 : Echo("  P4-18(Ch86) to P4-50(GCh86)")
                    Case 23 : Echo("  P4-17(Ch87) to P4-49(GCh87)")
                    Case 24 : Echo("  P4-16(Ch88) to P4-48(GCh88)")
                    Case 25 : Echo("  P4-15(Ch89) to P4-47(GCh89)")
                    Case 26 : Echo("  P4-14(Ch90) to P4-46(GCh90)")
                    Case 27 : Echo("  P4-13(Ch91) to P4-45(GCh91)")
                    Case 28 : Echo("  P4-12(Ch92) to P4-44(GCh92)")
                    Case 29 : Echo("  P4-11(Ch93) to P4-43(GCh93)")
                    Case 30 : Echo("  P4-10(Ch94) to P4-42(GCh94)")
                    Case 31 : Echo("  P4-09(Ch95) to P4-41(GCh95)")
                    Case 32 : Echo("  P4-08(Ch96) to P4-40(GCh96)")
                    Case 33 : Echo("  P4-07(Ch97) to P4-39(GCh97)")
                    Case 34 : Echo("  P4-06(Ch98) to P4-38(GCh98)")
                    Case 35 : Echo("  P4-05(Ch99) to P4-37(GCh99)")
                    Case 36 : Echo("  P4-04(Ch100) to P4-36(GCh100)")
                    Case 37 : Echo("  P4-03(Ch101) to P4-35(GCh101)")
                    Case 38 : Echo("  P4-02(Ch102) to P4-34(GCh102)")
                    Case 39 : Echo("  P4-01(Ch103) to P4-33(GCh103)")
                    Case 40 : Echo("  P5-56(Ch104) to P5-24(GCh104)")
                    Case 41 : Echo("  P5-55(Ch105) to P5-23(GCh105)")
                    Case 42 : Echo("  P5-54(Ch106) to P5-22(GCh106)")
                    Case 43 : Echo("  P5-53(Ch107) to P5-21(GCh107)")
                    Case 44 : Echo("  P5-52(Ch108) to P5-20(GCh108)")
                    Case 45 : Echo("  P5-51(Ch109) to P5-19(GCh109)")
                    Case 46 : Echo("  P5-50(Ch110) to P5-18(GCh110)")
                    Case 47 : Echo("  P5-49(Ch111) to P5-17(GCh111)")
                    Case 48 : Echo("  P5-48(Ch112) to P5-16(GCh112)")
                    Case 49 : Echo("  P5-47(Ch113) to P5-15(GCh113)")
                    Case 50 : Echo("  P5-46(Ch114) to P5-14(GCh114)")
                    Case 51 : Echo("  P5-45(Ch115) to P5-13(GCh115)")
                    Case 52 : Echo("  P5-44(Ch116) to P5-12(GCh116)")
                    Case 53 : Echo("  P5-43(Ch117) to P5-11(GCh117)")
                    Case 54 : Echo("  P5-42(Ch118) to P5-10(GCh118)")
                    Case 55 : Echo("  P5-41(Ch119) to P5-09(GCh119)")
                    Case 56 : Echo("  P5-40(Ch120) to P5-08(GCh120)")
                    Case 57 : Echo("  P5-49(Ch121) to P5-07(GCh121)")
                    Case 58 : Echo("  P5-38(Ch122) to P5-06(GCh122)")
                    Case 59 : Echo("  P5-37(Ch123) to P5-05(GCh123)")
                    Case 60 : Echo("  P5-36(Ch124) to P5-04(GCh124)")
                    Case 61 : Echo("  P5-35(Ch125) to P5-03(GCh125)")
                    Case 62 : Echo("  P5-34(Ch126) to P5-02(GCh126)")
                    Case 63 : Echo("  P5-33(Ch127) to P5-01(GCh127)")
                End Select
            End If
            Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete
        Next PinCount

        If TestFailure Then
            TestDTS = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            Echo(FormatResults(True, "Slot 2 CCC2 all channels ML test", "DTS-15-064 to 127"))
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "DTS" And OptionStep = 22 Then
            GoTo DTS_15_64
        End If
        frmSTest.proProgress.Value = 57

DTS_16_64:  'DTS-16-064 to DTS-16-127
        'Start walking a one through the card
        For PinCount = 0 To 63
            nSystErr = terM9_setChannelPinOpcode(hndlDTI, CCC2PinList(PinCount), TERM9_OP_MH)
            nSystErr = terM9_runStaticPattern(hndlDTI, VI_FALSE, VI_TRUE)
            nSystErr = terM9_fetchStaticPinResults(hndlDTI, 64, CCC2PinList(0), resultCount, CCCPinResult(0))
            If CCCPinResult(PinCount) = TERM9_RESULT_FAIL Then
                sTNum = "DTS-16-" & Format((PinCount + 64), "000")
                Echo(FormatResults(False, "Channel " & CStr(PinCount + 64) & " on CCC2 in slot 2 MH test", sTNum))
                RegisterFailure(DIGITAL, sTNum, , , , , "Channel " & CStr(PinCount + 64) & " on CCC2 in slot 2 MH test")
                TestFailure = True
                Select Case PinCount
                    Case 0 : Echo("  P4-25(Ch64) to P4-57(GCh64)")
                    Case 1 : Echo("  P4-26(Ch65) to P4-58(GCh65)")
                    Case 2 : Echo("  P4-27(Ch66) to P4-59(GCh66)")
                    Case 3 : Echo("  P4-28(Ch67) to P4-60(GCh67)")
                    Case 4 : Echo("  P4-29(Ch68) to P4-61(GCh68)")
                    Case 5 : Echo("  P4-30(Ch69) to P4-62(GCh69)")
                    Case 6 : Echo("  P4-31(Ch70) to P4-63(GCh70)")
                    Case 7 : Echo("  P4-32(Ch71) to P4-64(GCh71)")
                    Case 8 : Echo("  P4-80(Ch72) to P4-81(GCh72)")
                    Case 9 : Echo("  P4-82(Ch73) to P4-83(GCh73)")
                    Case 10 : Echo("  P4-84(Ch74) to P4-85(GCh74)")
                    Case 11 : Echo("  P4-86(Ch75) to P4-87(GCh75)")
                    Case 12 : Echo("  P4-88(Ch76) to P4-89(GCh76)")
                    Case 13 : Echo("  P4-90(Ch77) to P4-91(GCh77)")
                    Case 14 : Echo("  P4-92(Ch78) to P4-93(GCh78)")
                    Case 15 : Echo("  P4-94(Ch79) to P4-95(GCh79)")
                    Case 16 : Echo("  P4-24(Ch80) to P4-56(GCh80)")
                    Case 17 : Echo("  P4-23(Ch81) to P4-55(GCh81)")
                    Case 18 : Echo("  P4-22(Ch82) to P4-54(GCh82)")
                    Case 19 : Echo("  P4-21(Ch83) to P4-53(GCh83)")
                    Case 20 : Echo("  P4-20(Ch84) to P4-52(GCh84)")
                    Case 21 : Echo("  P4-19(Ch85) to P4-51(GCh85)")
                    Case 22 : Echo("  P4-18(Ch86) to P4-50(GCh86)")
                    Case 23 : Echo("  P4-17(Ch87) to P4-49(GCh87)")
                    Case 24 : Echo("  P4-16(Ch88) to P4-48(GCh88)")
                    Case 25 : Echo("  P4-15(Ch89) to P4-47(GCh89)")
                    Case 26 : Echo("  P4-14(Ch90) to P4-46(GCh90)")
                    Case 27 : Echo("  P4-13(Ch91) to P4-45(GCh91)")
                    Case 28 : Echo("  P4-12(Ch92) to P4-44(GCh92)")
                    Case 29 : Echo("  P4-11(Ch93) to P4-43(GCh93)")
                    Case 30 : Echo("  P4-10(Ch94) to P4-42(GCh94)")
                    Case 31 : Echo("  P4-09(Ch95) to P4-41(GCh95)")
                    Case 32 : Echo("  P4-08(Ch96) to P4-40(GCh96)")
                    Case 33 : Echo("  P4-07(Ch97) to P4-39(GCh97)")
                    Case 34 : Echo("  P4-06(Ch98) to P4-38(GCh98)")
                    Case 35 : Echo("  P4-05(Ch99) to P4-37(GCh99)")
                    Case 36 : Echo("  P4-04(Ch100) to P4-36(GCh100)")
                    Case 37 : Echo("  P4-03(Ch101) to P4-35(GCh101)")
                    Case 38 : Echo("  P4-02(Ch102) to P4-34(GCh102)")
                    Case 39 : Echo("  P4-01(Ch103) to P4-33(GCh103)")
                    Case 40 : Echo("  P5-56(Ch104) to P5-24(GCh104)")
                    Case 41 : Echo("  P5-55(Ch105) to P5-23(GCh105)")
                    Case 42 : Echo("  P5-54(Ch106) to P5-22(GCh106)")
                    Case 43 : Echo("  P5-53(Ch107) to P5-21(GCh107)")
                    Case 44 : Echo("  P5-52(Ch108) to P5-20(GCh108)")
                    Case 45 : Echo("  P5-51(Ch109) to P5-19(GCh109)")
                    Case 46 : Echo("  P5-50(Ch110) to P5-18(GCh110)")
                    Case 47 : Echo("  P5-49(Ch111) to P5-17(GCh111)")
                    Case 48 : Echo("  P5-48(Ch112) to P5-16(GCh112)")
                    Case 49 : Echo("  P5-47(Ch113) to P5-15(GCh113)")
                    Case 50 : Echo("  P5-46(Ch114) to P5-14(GCh114)")
                    Case 51 : Echo("  P5-45(Ch115) to P5-13(GCh115)")
                    Case 52 : Echo("  P5-44(Ch116) to P5-12(GCh116)")
                    Case 53 : Echo("  P5-43(Ch117) to P5-11(GCh117)")
                    Case 54 : Echo("  P5-42(Ch118) to P5-10(GCh118)")
                    Case 55 : Echo("  P5-41(Ch119) to P5-09(GCh119)")
                    Case 56 : Echo("  P5-40(Ch120) to P5-08(GCh120)")
                    Case 57 : Echo("  P5-39(Ch121) to P5-07(GCh121)")
                    Case 58 : Echo("  P5-38(Ch122) to P5-06(GCh122)")
                    Case 59 : Echo("  P5-37(Ch123) to P5-05(GCh123)")
                    Case 60 : Echo("  P5-36(Ch124) to P5-04(GCh124)")
                    Case 61 : Echo("  P5-35(Ch125) to P5-03(GCh125)")
                    Case 62 : Echo("  P5-34(Ch126) to P5-02(GCh126)")
                    Case 63 : Echo("  P5-33(Ch127) to P5-01(GCh127)")
                End Select
            End If
            nSystErr = terM9_setChannelPinOpcode(hndlDTI, CCC2PinList(PinCount), TERM9_OP_ML)
            nSystErr = terM9_runStaticPattern(hndlDTI, VI_FALSE, VI_TRUE)
            Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete
        Next PinCount

        If TestFailure Then
            TestDTS = FAILED
            Echo("******************************")
            Echo("Check failed pins for possible")
            Echo("shorted signal to return.     ")
            Echo("******************************")
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            Echo(FormatResults(True, "Slot 2 CCC2 walking ones MH test", "DTS-16-064 to 127"))
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "DTS" And OptionStep = 23 Then
            GoTo DTS_16_64
        End If
        frmSTest.proProgress.Value = 60

        'Channel card 3
DTS_15_128:  'DTS-15-128 to DTS-15-191
        DigitalModule = 1
        'Set all pins on card to output a low
        nSystErr = terM9_setStaticPatternDelay(hndlDTI, 0.00001)
        nSystErr = terM9_setLevelSet(hndlDTI, TERM9_SCOPE_SYSTEM, TTL_LEVELS, 5.0, 0.0, 4.0, 2.5, 0.0, 0.0, 0.0, TERM9_SLEWRATE_LOW)
        nSystErr = terM9_setSystemEnable(hndlDTI, VI_TRUE)
        nSystErr = terM9_setChannelChanMode(hndlDTI, 64, CCC3PinList(0), TERM9_CHANMODE_STATIC)
        nSystErr = terM9_setGroundReference(hndlDTI, TERM9_SCOPE_SYSTEM, TERM9_GROUND_INTERNAL)
        nSystErr = terM9_setLowPower(hndlDTI, TERM9_SCOPE_SYSTEM, VI_FALSE)
        nSystErr = terM9_setChannelLoad(hndlDTI, 64, CCC3PinList(0), TERM9_LOAD_OFF)
        nSystErr = terM9_setChannelConnect(hndlDTI, 64, CCC3PinList(0), TERM9_RELAY_CLOSED)
        nSystErr = terM9_setPinmapGroup(hndlDTI, CCC3, 64, CCC3PinList(0))
        nSystErr = terM9_setChannelLevel(hndlDTI, 64, CCC3PinList(0), TTL_LEVELS)
        nSystErr = terM9_setSystemEnable(hndlDTI, VI_TRUE)
        nSystErr = terM9_setChannelPinOpcode(hndlDTI, CCC3, TERM9_OP_ML)
        nSystErr = terM9_runStaticPattern(hndlDTI, VI_FALSE, VI_TRUE)
        nSystErr = terM9_fetchStaticPinResults(hndlDTI, 64, CCC3PinList(0), resultCount, CCCPinResult(0))

        '    TERM9_SCOPE_CHANCARD(3)
        TestFailure = False
        For PinCount = 0 To 63
            If CCCPinResult(PinCount) = TERM9_RESULT_FAIL Then
                sTNum = "DTS-15-" & Format((PinCount + 128), "000")
                Echo(FormatResults(False, "Channel " & CStr(PinCount + 128) & " on CCC3 in slot 1 ML test", sTNum))
                RegisterFailure(DIGITAL, sTNum, , , , , "Channel " & CStr(PinCount + 128) & " on CCC3 in slot 1 ML test")
                TestFailure = True
                Select Case PinCount
                    Case 0 : Echo("  P2-25(Ch128) to P2-57(GCh128)")
                    Case 1 : Echo("  P2-26(Ch129) to P2-58(GCh129)")
                    Case 2 : Echo("  P2-27(Ch130) to P2-59(GCh130)")
                    Case 3 : Echo("  P2-28(Ch131) to P2-60(GCh131)")
                    Case 4 : Echo("  P2-29(Ch132) to P2-61(GCh132)")
                    Case 5 : Echo("  P2-30(Ch133) to P2-62(GCh133)")
                    Case 6 : Echo("  P2-31(Ch134) to P2-63(GCh134)")
                    Case 7 : Echo("  P2-32(Ch135) to P2-64(GCh135)")
                    Case 8 : Echo("  P2-80(Ch136) to P2-81(GCh136)")
                    Case 9 : Echo("  P2-82(Ch137) to P2-83(GCh137)")
                    Case 10 : Echo("  P2-84(Ch138) to P2-85(GCh138)")
                    Case 11 : Echo("  P2-86(Ch139) to P2-87(GCh139)")
                    Case 12 : Echo("  P2-88(Ch140) to P2-89(GCh140)")
                    Case 13 : Echo("  P2-90(Ch141) to P2-91(GCh141)")
                    Case 14 : Echo("  P2-92(Ch142) to P2-93(GCh142)")
                    Case 15 : Echo("  P2-94(Ch143) to P2-95(GCh143)")
                    Case 16 : Echo("  P2-24(Ch144) to P2-56(GCh144)")
                    Case 17 : Echo("  P2-23(Ch145) to P2-55(GCh145)")
                    Case 18 : Echo("  P2-22(Ch146) to P2-54(GCh146)")
                    Case 19 : Echo("  P2-21(Ch147) to P2-53(GCh147)")
                    Case 20 : Echo("  P2-20(Ch148) to P2-52(GCh148)")
                    Case 21 : Echo("  P2-19(Ch149) to P2-51(GCh149)")
                    Case 22 : Echo("  P2-18(Ch150) to P2-50(GCh150)")
                    Case 23 : Echo("  P2-17(Ch151) to P2-49(GCh151)")
                    Case 24 : Echo("  P2-16(Ch152) to P2-48(GCh152)")
                    Case 25 : Echo("  P2-15(Ch153) to P2-47(GCh153)")
                    Case 26 : Echo("  P2-14(Ch154) to P2-46(GCh154)")
                    Case 27 : Echo("  P2-13(Ch155) to P2-45(GCh155)")
                    Case 28 : Echo("  P2-12(Ch156) to P2-44(GCh156)")
                    Case 29 : Echo("  P2-11(Ch157) to P2-43(GCh157)")
                    Case 30 : Echo("  P2-10(Ch158) to P2-42(GCh158)")
                    Case 31 : Echo("  P2-09(Ch159) to P2-41(GCh159)")
                    Case 32 : Echo("  P2-08(Ch160) to P2-40(GCh160)")
                    Case 33 : Echo("  P2-07(Ch161) to P2-39(GCh161)")
                    Case 34 : Echo("  P2-06(Ch162) to P2-38(GCh162)")
                    Case 35 : Echo("  P2-05(Ch163) to P2-37(GCh163)")
                    Case 36 : Echo("  P2-04(Ch164) to P2-36(GCh164)")
                    Case 37 : Echo("  P2-03(Ch165) to P2-35(GCh165)")
                    Case 38 : Echo("  P2-02(Ch166) to P2-34(GCh166)")
                    Case 39 : Echo("  P2-01(Ch167) to P2-33(GCh167)")
                    Case 40 : Echo("  P3-46(Ch168) to P3-24(GCh168)")
                    Case 41 : Echo("  P3-45(Ch169) to P3-23(GCh169)")
                    Case 42 : Echo("  P3-44(Ch170) to P3-22(GCh170)")
                    Case 43 : Echo("  P3-43(Ch171) to P3-21(GCh171)")
                    Case 44 : Echo("  P3-42(Ch172) to P3-20(GCh172)")
                    Case 45 : Echo("  P3-31(Ch173) to P3-19(GCh173)")
                    Case 46 : Echo("  P3-30(Ch174) to P3-18(GCh174)")
                    Case 47 : Echo("  P3-39(Ch175) to P3-17(GCh175)")
                    Case 48 : Echo("  P3-38(Ch176) to P3-16(GCh176)")
                    Case 49 : Echo("  P3-37(Ch177) to P3-15(GCh177)")
                    Case 50 : Echo("  P3-26(Ch178) to P3-14(GCh178)")
                    Case 51 : Echo("  P3-25(Ch179) to P3-13(GCh179)")
                    Case 52 : Echo("  P3-24(Ch180) to P3-12(GCh180)")
                    Case 53 : Echo("  P3-23(Ch181) to P3-11(GCh181)")
                    Case 54 : Echo("  P3-22(Ch182) to P3-10(GCh182)")
                    Case 55 : Echo("  P3-11(Ch183) to P3-09(GCh183)")
                    Case 56 : Echo("  P3-10(Ch184) to P3-08(GCh184)")
                    Case 57 : Echo("  P3-19(Ch185) to P3-07(GCh185)")
                    Case 58 : Echo("  P3-18(Ch186) to P3-06(GCh186)")
                    Case 59 : Echo("  P3-17(Ch187) to P3-05(GCh187)")
                    Case 60 : Echo("  P3-06(Ch188) to P3-04(GCh188)")
                    Case 61 : Echo("  P3-05(Ch189) to P3-03(GCh189)")
                    Case 62 : Echo("  P3-04(Ch190) to P3-02(GCh190)")
                    Case 63 : Echo("  P3-33(Ch191) to P3-01(GCh191)")
                End Select
            End If
            Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete
        Next PinCount

        If TestFailure Then
            TestDTS = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            Echo(FormatResults(True, "Slot 1 CCC3 all channels ML test", "DTS-15-128 to 191"))
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "DTS" And OptionStep = 24 Then
            GoTo DTS_15_128
        End If
        frmSTest.proProgress.Value = 62

DTS_16_128:  'DTS-16-128 to DTS-16-191
        'Start walking a one through the card
        For PinCount = 0 To 63
            nSystErr = terM9_setChannelPinOpcode(hndlDTI, CCC3PinList(PinCount), TERM9_OP_MH)
            nSystErr = terM9_runStaticPattern(hndlDTI, VI_FALSE, VI_TRUE)
            nSystErr = terM9_fetchStaticPinResults(hndlDTI, 64, CCC3PinList(0), resultCount, CCCPinResult(0))
            If CCCPinResult(PinCount) = TERM9_RESULT_FAIL Then
                sTNum = "DTS-16-" & Format((PinCount + 128), "000")
                Echo(FormatResults(False, "Channel " & CStr(PinCount + 128) & " on CCC3 in slot 1 MH test", sTNum))
                RegisterFailure(DIGITAL, sTNum, , , , , "Channel " & CStr(PinCount + 128) & " on CCC3 MH test")
                TestFailure = True
                Select Case PinCount
                    Case 0 : Echo("  P2-25(Ch128) to P2-57(GCh128)")
                    Case 1 : Echo("  P2-26(Ch129) to P2-58(GCh129)")
                    Case 2 : Echo("  P2-27(Ch130) to P2-59(GCh130)")
                    Case 3 : Echo("  P2-28(Ch131) to P2-60(GCh131)")
                    Case 4 : Echo("  P2-29(Ch132) to P2-61(GCh132)")
                    Case 5 : Echo("  P2-30(Ch133) to P2-62(GCh133)")
                    Case 6 : Echo("  P2-31(Ch134) to P2-63(GCh134)")
                    Case 7 : Echo("  P2-32(Ch135) to P2-64(GCh135)")
                    Case 8 : Echo("  P2-80(Ch136) to P2-81(GCh136)")
                    Case 9 : Echo("  P2-82(Ch137) to P2-83(GCh137)")
                    Case 10 : Echo("  P2-84(Ch138) to P2-85(GCh138)")
                    Case 11 : Echo("  P2-86(Ch139) to P2-87(GCh139)")
                    Case 12 : Echo("  P2-88(Ch140) to P2-89(GCh140)")
                    Case 13 : Echo("  P2-90(Ch141) to P2-91(GCh141)")
                    Case 14 : Echo("  P2-92(Ch142) to P2-93(GCh142)")
                    Case 15 : Echo("  P2-94(Ch143) to P2-95(GCh143)")
                    Case 16 : Echo("  P2-24(Ch144) to P2-56(GCh144)")
                    Case 17 : Echo("  P2-23(Ch145) to P2-55(GCh145)")
                    Case 18 : Echo("  P2-22(Ch146) to P2-54(GCh146)")
                    Case 19 : Echo("  P2-21(Ch147) to P2-53(GCh147)")
                    Case 20 : Echo("  P2-20(Ch148) to P2-52(GCh148)")
                    Case 21 : Echo("  P2-19(Ch149) to P2-51(GCh149)")
                    Case 22 : Echo("  P2-18(Ch150) to P2-50(GCh150)")
                    Case 23 : Echo("  P2-17(Ch151) to P2-49(GCh151)")
                    Case 24 : Echo("  P2-16(Ch152) to P2-48(GCh152)")
                    Case 25 : Echo("  P2-15(Ch153) to P2-47(GCh153)")
                    Case 26 : Echo("  P2-14(Ch154) to P2-46(GCh154)")
                    Case 27 : Echo("  P2-13(Ch155) to P2-45(GCh155)")
                    Case 28 : Echo("  P2-12(Ch156) to P2-44(GCh156)")
                    Case 29 : Echo("  P2-11(Ch157) to P2-43(GCh157)")
                    Case 30 : Echo("  P2-10(Ch158) to P2-42(GCh158)")
                    Case 31 : Echo("  P2-09(Ch159) to P2-41(GCh159)")
                    Case 32 : Echo("  P2-08(Ch160) to P2-40(GCh160)")
                    Case 33 : Echo("  P2-07(Ch161) to P2-39(GCh161)")
                    Case 34 : Echo("  P2-06(Ch162) to P2-38(GCh162)")
                    Case 35 : Echo("  P2-05(Ch163) to P2-37(GCh163)")
                    Case 36 : Echo("  P2-04(Ch164) to P2-36(GCh164)")
                    Case 37 : Echo("  P2-03(Ch165) to P2-35(GCh165)")
                    Case 38 : Echo("  P2-02(Ch166) to P2-34(GCh166)")
                    Case 39 : Echo("  P2-01(Ch167) to P2-33(GCh167)")
                    Case 40 : Echo("  P3-56(Ch168) to P3-24(GCh168)")
                    Case 41 : Echo("  P3-55(Ch169) to P3-23(GCh169)")
                    Case 42 : Echo("  P3-54(Ch170) to P3-22(GCh170)")
                    Case 43 : Echo("  P3-53(Ch171) to P3-21(GCh171)")
                    Case 44 : Echo("  P3-52(Ch172) to P3-20(GCh172)")
                    Case 45 : Echo("  P3-51(Ch173) to P3-19(GCh173)")
                    Case 46 : Echo("  P3-50(Ch174) to P3-18(GCh174)")
                    Case 47 : Echo("  P3-49(Ch175) to P3-17(GCh175)")
                    Case 48 : Echo("  P3-48(Ch176) to P3-16(GCh176)")
                    Case 49 : Echo("  P3-47(Ch177) to P3-15(GCh177)")
                    Case 50 : Echo("  P3-46(Ch178) to P3-14(GCh178)")
                    Case 51 : Echo("  P3-45(Ch179) to P3-13(GCh179)")
                    Case 52 : Echo("  P3-44(Ch180) to P3-12(GCh180)")
                    Case 53 : Echo("  P3-43(Ch181) to P3-11(GCh181)")
                    Case 54 : Echo("  P3-42(Ch182) to P3-10(GCh182)")
                    Case 55 : Echo("  P3-41(Ch183) to P3-09(GCh183)")
                    Case 56 : Echo("  P3-40(Ch184) to P3-08(GCh184)")
                    Case 57 : Echo("  P3-39(Ch185) to P3-07(GCh185)")
                    Case 58 : Echo("  P3-38(Ch186) to P3-06(GCh186)")
                    Case 59 : Echo("  P3-37(Ch187) to P3-05(GCh187)")
                    Case 60 : Echo("  P3-36(Ch188) to P3-04(GCh188)")
                    Case 61 : Echo("  P3-35(Ch189) to P3-03(GCh189)")
                    Case 62 : Echo("  P3-34(Ch190) to P3-02(GCh190)")
                    Case 63 : Echo("  P3-33(Ch191) to P3-01(GCh191)")
                End Select
            End If
            nSystErr = terM9_setChannelPinOpcode(hndlDTI, CCC3PinList(PinCount), TERM9_OP_ML)
            nSystErr = terM9_runStaticPattern(hndlDTI, VI_FALSE, VI_TRUE)

            Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete
        Next PinCount

        If TestFailure Then
            TestDTS = FAILED
            Echo("******************************")
            Echo("Check failed pins for possible")
            Echo("shorted signal to return.     ")
            Echo("******************************")
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            Echo(FormatResults(True, "Slot 1 CCC3 walking ones MH test", "DTS-16-128 to 191"))
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "DTS" And OptionStep = 25 Then
            GoTo DTS_16_128
        End If
        frmSTest.proProgress.Value = 65

DTS_17_1:  ' DTS-17-001  Initialize DTS
        DigitalModule = 4
        '*********   DTS Built-in-Tests   *********
        nSystErr = terM9_setStaticPatternDelay(hndlDTI, 0.00001)
        nSystErr = terM9_setChannelPinOpcode(hndlDTI, CCC3, TERM9_OP_IL)
        nSystErr = terM9_runStaticPattern(hndlDTI, VI_FALSE, VI_TRUE)
        Application.DoEvents()
        GroundRefInterupt = False
        nSystErr = terM9_initializeInstrument(hndlDTI)
        Delay(10.0)
        nSystErr = terM9_close(hndlDTI)
        Delay(10.0)

        InitStatus = terM9_init("TERM9::#0", VI_TRUE, VI_TRUE, hndlDTI) 'gets new handle
        If InitStatus > &H4FFF0000 Then
            TestDTS = FAILED
            Echo(FormatResults(False, "CCRB Initialization Test", "DTS-17-001"))
            Echo("ERROR - " & CStr(InitStatus) & " Most likely faulty module: CCRB.")
            RegisterFailure(DIGITAL, "DTS-17-001", , , , , "CCRB Initialization Test")
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            Echo(FormatResults(True, "CCRB Initialization Test", "DTS-17-001"))
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "DTS" And OptionStep = 26 Then
            GoTo DTS_17_1
        End If

        If Not FnFileExists(M9selftestPath & "M9selftest.exe") Then
            TestDTS = FAILED
            SError = "Missing file " & M9selftestPath & "M9selftest.exe. Can not continue!"
            Echo(SError)
            MsgBox(SError, vbCritical)
            IncStepFailed()
            GoTo TestComplete ' can not continue with DTS tests
        End If
        frmSTest.proProgress.Value = 67


DTS_18_1:  'DTS-18-001 -- Confidence Built-in-Test
        nSystErr = terM9_initializeInstrument(hndlDTI)
        nSystErr = terM9_close(hndlDTI)
        InitStatus = terM9_init("TERM9::#0", VI_TRUE, VI_TRUE, hndlDTI) 'gets new handle
        STFileHandle = FreeFile()
        Echo("Executing " & "M9Selftest.EXE -c")

        STFileHandle = FreeFile()
        FileOpen(STFileHandle, sAts_INI_path & DTS_SELF_TEST_BAT, OpenMode.Output)
        PrintLine(STFileHandle, "M9Selftest.EXE -c > " & sAts_INI_path & RESULTS_DIGITAL1)
        FileClose(STFileHandle)

        On Error Resume Next
        On Error GoTo 0
        StartTime = Microsoft.VisualBasic.Timer()
        AppHandle = Shell(sAts_INI_path & DTS_SELF_TEST_BAT, AppWinStyle.Hide)
        Application.DoEvents()
        If Err.Number <> 0 Then
            '** File could not be found -- DO NOT Record in FHDB **
            TestDTS = FAILED
            SError = "    DTS Self Test M9Selftest.exe could not be located."
            MsgBox(SError, MsgBoxStyle.Exclamation)
            Echo(SError)
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            'Wait until Self test has been performed and then open the results file
            Do
                System.Threading.Thread.Sleep(4980) ' ten second delay
                Application.DoEvents()

                System.Threading.Thread.Sleep(5000)
                Application.DoEvents()
                If (Microsoft.VisualBasic.Timer() - StartTime) > 30 Then 'max wait is 30 seconds
                    TestDTS = FAILED
                    Echo(FormatResults(False, "DTS Timeout occurred."))
                    TestStatus = FAILED
                    IncStepFailed()
                    If OptionFaultMode = SOFmode Then GoTo TestComplete
                    Exit Do
                End If
                FileL = FileLen(sAts_INI_path & RESULTS_DIGITAL1)
                Application.DoEvents()
            Loop While FileL < TOTAL_CTEST_FILE_LENGTH - 1

            If AbortTest = True Then GoTo TestComplete

            On Error Resume Next
            STLogHandle = FreeFile()
            FileOpen(STLogHandle, sAts_INI_path & RESULTS_DIGITAL1, OpenMode.Binary)
            LineCount = 0
            'Compare test result file with known good data
            S = Space(LOF(STLogHandle))
            FileGet(STLogHandle, S, )
            FileClose(STLogHandle)

            If InStr(S, "*****PASSED*****") Then
                ThisTest = PASSED
                Echo(FormatResults(True, "DTS Short Built-In Test...", "DTS-18-001")) 'PASSED
                IncStepPassed()
            Else
                ThisTest = FAILED
                Echo(FormatResults(False, "DTS Short Built-In Test...", "DTS-18-001"))
                frmSTest.DetailsText.SelectionStart = Len(frmSTest.DetailsText.Text) ' move insertion point to end of text
                frmSTest.DetailsText.SelectionLength = 0
                frmSTest.DetailsText.SelectionColor = ColorTranslator.FromOle(QBColor(0)) ' make it black
                frmSTest.DetailsText.SelectedText = S & vbCrLf ' append data to textbox
                RegisterFailure(DIGITAL, "DTS-18-001", , , , , "DTS Short Built-In Test...")
                TestDTS = FAILED
                IncStepFailed()
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            End If
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "DTS" And OptionStep = 27 Then
            GoTo DTS_18_1
        End If
        frmSTest.proProgress.Value = 70



        'DTS-19-001 -- Long Built-in-Test
DTS_19_1:
        'Added these codes to reset the DTS before running the '-a' test
        'PCR - 463
        nSystErr = terM9_initializeInstrument(hndlDTI)
        nSystErr = terM9_close(hndlDTI)
        InitStatus = terM9_init("TERM9::#0", VI_TRUE, VI_TRUE, hndlDTI) 'gets new handle
        KillApp("M9selftest") ' turn off the exe if it is still running
        System.Threading.Thread.Sleep(1000)
        Echo("Executing M9Selftest.EXE -a  0") ' -a for all tests excluding slow mode


        STFileHandle = FreeFile()
        FileOpen(STFileHandle, sAts_INI_path & DTS_SELF_TEST_BAT, OpenMode.Output)
        PrintLine(STFileHandle, "M9Selftest.EXE -a > " & sAts_INI_path & RESULTS_DIGITAL2)
        FileClose(STFileHandle)
        On Error Resume Next

        AppHandle = Shell(sAts_INI_path & DTS_SELF_TEST_BAT, AppWinStyle.Hide)
        Application.DoEvents()
        If Not Err.Number <> 0 Then
            'Wait until Self test has been performed and then open the results file
            On Error Resume Next
            StartTime = Microsoft.VisualBasic.Timer()
            i = 0
            Do
                System.Threading.Thread.Sleep(4980) ' use sleep to give teradyne more processor time
                Application.DoEvents()
                System.Threading.Thread.Sleep(5000)
                Application.DoEvents()
                If (Microsoft.VisualBasic.Timer() - StartTime) > 480 Then 'Allow 8 minutes, takes 5.5 minutes
                    Echo(FormatResults(False, "DTS Timeout occurred."), False)
                    TestStatus = FAILED
                    TestDTS = FAILED
                    If SOFmode = True Then GoTo TestComplete
                    Exit Do
                End If
                i += 1
                BumpProgress(i)
                Application.DoEvents()
                If AbortTest = True Then GoTo TestComplete
                If PauseTest = True Then ' wait here until "Continue" or "Abort" is pressed
                    Do
                        Application.DoEvents()
                    Loop Until PauseTest = False
                End If
                FileL = FileLen(sAts_INI_path & RESULTS_DIGITAL2)
                If Not AppRunning("M9selftest") Then Exit Do
            Loop While FileL < 200000 ' TOTAL_DTS_FILE_LENGTH - 1
            KillApp("M9selftest") ' turn off the exe if it is still running
            System.Threading.Thread.Sleep(1000)

            STLogHandle = FreeFile()

            'Compare test result file with known good data
            FileOpen(STLogHandle, sAts_INI_path & RESULTS_DIGITAL2, OpenMode.Binary)
            S = Space(LOF(STLogHandle))
            FileGet(STLogHandle, S, )
            FileClose(STLogHandle)

            'Parse out summary
            x2 = InStr(S, "Selftest Summary")
            If x2 > 0 Then
              S = Mid(S, x2)
            End If

            ''Selftest Summary
            ''----------------

            ''System deskew passed
            ''                     Slot()
            ''Test Name             1  2  3  4
            ''register             P  P  P  P 
            ''shortram             P  P  P  P 
            ''shortpatc            P  P  P  P 
            ''CRB_shortresultram P
            ''detector_ref         P  P  P    
            ''detector_offset      P  P  P    
            ''driver_detector      P  P  P    
            ''driver_edge          P  P  P    
            ''load                 P  P  P    
            ''vcom                 P  P  P    
            ''sfail                P  P  P    
            ''sopcodes             P  P  P    
            ''dfail                P  P  P    
            ''burst                P  P  P    
            ''dopcodes             P  P  P    
            ''format               P  P  P    
            ''phase                P  P  P    
            ''window               P  P  P    
            ''cpp                  P  P  P    
            ''patc_branch          P  P  P    
            ''SlowMode            *F**F**F*
            ''UC_Ref P
            ''UC_Frq P
            ''SR_Ref P
            ''SR_Frq P
            ''SR_Loopback P
            ''High_Vbits P
            ''lseq                 P  P  P  P 
            ''edge                 P  P  P    
            ''capture              P  P  P    
            ''Dmtx                 P  P  P    
            ''Stmtx                P  P  P    
            ''Timer                P  P  P  P 
            ''Patc_Nest            P  P  P    
            ''Tdr                  P  P  P  P 
            ''MBT                  P  P  P    
            ''StatusBus            P  P  P    
            ''LocalBus P
            ''Vernier              P  P  P    
            ''Hprom                P  P  P  P 
            ''prbInterface P
            ''prbRamShort P
            ''prbStaticSTB P
            ''prbStaticWIN P
            ''prbLearn1M P
            ''prbPFail1M P



            'Parse out slow mode failure
            'x2 = InStr(S, "slowMode            *F**F**F*")
            'If x2 > 0 Then
                'S = Left(S, x2 - 1) & Mid(S, x2 + Len("slowMode            *F**F**F*") + 2)
                'Echo("SlowMode test failed")
            'End If

            If Not InStr(S, "*F*") Then
                ThisTest = PASSED
                Echo(FormatResults(True, "DTS Long Built-In Test...", "DTS-19-001"), False)
                IncStepPassed()
            Else
                ThisTest = FAILED
                Echo(FormatResults(False, "DTS Long Built-In Test...", "DTS-19-001"))
                frmSTest.DetailsText.SelectionStart = Len(frmSTest.DetailsText.Text) ' move insertion point to end of text
                x2 = InStr(S, "Selftest Summary")
                If x2 > 0 Then
                    S = Mid(S, x2)
                    frmSTest.DetailsText.SelectionStart = Len(frmSTest.DetailsText.Text) ' move insertion point to end of text
                    frmSTest.DetailsText.SelectionLength = 0
                    frmSTest.DetailsText.SelectionColor = ColorTranslator.FromOle(QBColor(0)) ' make it black
                    frmSTest.DetailsText.SelectedText = S & vbCrLf ' append data to textbox
                End If
                Echo("You can view the complete results of this test at " & sAts_INI_path & "DTS2.TXT." & vbCrLf)
                TestDTS = FAILED
                IncStepFailed()
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            End If
            Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete

        Else
            '** File could not be found -- DO NOT Record in FHDB **
            TestDTS = FAILED
            SError = "    DTS Self Test .exe could not be located."
            MsgBox(SError, MsgBoxStyle.Exclamation)
            Echo(SError)
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        End If
        frmSTest.proProgress.Value = 80
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "DTS" And OptionStep = 28 Then
            GoTo DTS_19_1
        End If

DTS_20_1:  'DTS-20-001 -- DRIVER REFERENCE Built-in-Test
        nSystErr = terM9_initializeInstrument(hndlDTI)
        nSystErr = terM9_close(hndlDTI) 'close  handle
        InitStatus = terM9_init("TERM9::#0", VI_TRUE, VI_TRUE, hndlDTI) 'gets new handle
        Echo("Executing M9Selftest.EXE -tdriver_ref 0")

        STFileHandle = FreeFile()
        FileOpen(STFileHandle, sAts_INI_path & DTS_SELF_TEST_BAT, OpenMode.Output)
        PrintLine(STFileHandle, "M9Selftest.EXE -tdriver_ref > " & sAts_INI_path & RESULTS_DIGITAL3)
        FileClose(STFileHandle)

        AppHandle = Shell(sAts_INI_path & DTS_SELF_TEST_BAT, AppWinStyle.Hide)
        Application.DoEvents()
        If Err.Number Then
            '** File could not be found -- DO NOT Record in FHDB **
            TestDTS = FAILED
            SError = "    DTS Self Test .exe could not be located."
            MsgBox(SError, MsgBoxStyle.Exclamation)
            Echo(SError)
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            'Wait until Self test has been performed and then open the results file
            On Error Resume Next
            StartTime = Microsoft.VisualBasic.Timer()
            i = 0
            Do
                System.Threading.Thread.Sleep(4980) ' sleep gives more processor time to teradyne
                Application.DoEvents()
                System.Threading.Thread.Sleep(5000)
                Application.DoEvents()
                If (Microsoft.VisualBasic.Timer() - StartTime) > 480 Then 'Allow 8 minutes, takes 5.5 minutes
                    Echo(FormatResults(False, "DTS Timeout ocurred"), False)
                    TestStatus = FAILED
                    TestDTS = FAILED
                    If OptionFaultMode = SOFmode Then GoTo TestComplete
                    Exit Do
                End If
                i += 1
                BumpProgress(i)
                Application.DoEvents()
                If PauseTest = True Then ' wait here until "Continue" or "Abort" is pressed
                    Do
                        Application.DoEvents()
                    Loop Until PauseTest = False
                End If
                If AbortTest = True Then GoTo TestComplete
                FileL = FileLen(sAts_INI_path & RESULTS_DIGITAL3)
                If Not AppRunning("M9selftest") Then Exit Do
            Loop While FileL < 100000 ' TOTAL_DRV_REFTEST_FILE_LENGTH - 1
            Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete

            KillApp("M9selftest") ' turn off the exe if it is still running
            System.Threading.Thread.Sleep(1000)

            STLogHandle = FreeFile()
            FileOpen(STLogHandle, sAts_INI_path & RESULTS_DIGITAL3, OpenMode.Binary)
            LineCount = 0
            temp = ""

            S = Space(LOF(STLogHandle))
            FileGet(STLogHandle, S)
            FileClose(STLogHandle)
            If InStr(S, "*****PASSED*****") Then
                ThisTest = PASSED
                Echo(FormatResults(True, "DTS Driver Ref Built-In Test...", "DTS-20-001"), False)
                IncStepPassed()
            Else
                ThisTest = FAILED
                Echo(FormatResults(False, "DTS Driver Ref Built-In Test", "DTS-20-001"), False)
                x2 = InStr(S, "Selftest Summary")
                If x2 > 0 Then
                    S = Mid(S, x2)
                    frmSTest.DetailsText.SelectionStart = Len(frmSTest.DetailsText.Text) ' move insertion point to end of text
                    frmSTest.DetailsText.SelectionLength = 0
                    frmSTest.DetailsText.SelectionColor = ColorTranslator.FromOle(QBColor(0)) ' make it black
                    frmSTest.DetailsText.SelectedText = S & vbCrLf ' append data to textbox
                End If
                Echo("You can view the complete results of this test at " & sAts_INI_path & "DTS3.TXT." & vbCrLf)
                RegisterFailure(DIGITAL, "DTS-20-001", , , , , "DTS Driver Ref Built-In Test")
                TestDTS = FAILED
                IncStepFailed()
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            End If
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "DTS" And OptionStep = 29 Then
            GoTo DTS_20_1
        End If




TestComplete:
        KillApp("M9selftest") ' turn off the exe if it is still running
        System.Threading.Thread.Sleep(1000)
        Reset() ' closes any open files
        frmSTest.cmdAbort.Text = "Abort Test"
        frmSTest.cmdPause.Text = "Pause Test"
        frmSTest.proProgress.Value = 100
        'Init DTS
        If InstrumentInitialized(DIGITAL) <> 0 Then
            InitStatus = terM9_init("TERM9::#0", VI_TRUE, VI_TRUE, instrumentHandle(DIGITAL))
            nSystErr = terM9_reset(instrumentHandle(DIGITAL))
            InitStatus = terM9_close(instrumentHandle(DIGITAL))
        End If
        If hndlDTI > 0 Then
            nSystErr = terM9_close(hndlDTI)
        End If
        instrumentHandle(DIGITAL) = 0
        hndlDTI = 0

        Echo("")
        Application.DoEvents()
        If AbortTest = True Then
            If TestDTS = FAILED Then
                ReportFailure(DIGITAL)
            Else
                ReportUnknown(DIGITAL)
                TestDTS = -99
            End If
            sMsg = vbCrLf
            sMsg &= "      **********************************************" & vbCrLf
            sMsg &= "      *                DTS tests aborted!          *" & vbCrLf
            sMsg &= "      **********************************************" & vbCrLf
            Echo(sMsg)
        ElseIf TestDTS = PASSED Then
            ReportPass(DIGITAL)
        ElseIf TestDTS = FAILED Then
            ReportFailure(DIGITAL)
        Else
            ReportUnknown(DIGITAL)
        End If


InstallSaif2:
        ' install the SAIF even if receiver switch says it is already installed
        ' unless the test was aborted before the saif was removed
        If SAIFinstalled = False Then
            S = "1. Remove the Digital Probe from the Digital Probe Jack on the receiver." & vbCrLf
            S &= "2. Re-install the SAIF onto the tester." & vbCrLf
            DisplaySetup(S, "TETS SAIF_CON.jpg", 2)
            If AbortTest = False Then
                If ReceiverSwitchOK = True Then
                    If fnSAIFinstalled(True) = False Then
                        sMsg = "Are you sure that you re-installed the SAIF to the tester?"
                        x = MsgBox(sMsg, MsgBoxStyle.YesNo)
                        If x = DialogResult.No Then GoTo InstallSaif2
                        If fnSAIFinstalled(True) = False Then
                            Echo("   *******************************************")
                            Echo(FormatResults(False, "Receiver ITA switch closed test failed."), 4)
                            Echo("The Receiver Switch on the tester must be open.", 4)
                            Echo("   *******************************************")
                        End If
                    End If
                End If
            End If
        End If

    End Function




End Module