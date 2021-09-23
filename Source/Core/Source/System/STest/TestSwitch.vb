'Option Strict Off
Option Explicit On




    '***************************************************************
    '* Nomenclature   : ATS-ViperT SYSTEM SELF TEST                *
    '*                  Switch Tests                               *
    '* Version        : 2.0                                        *
    '* Last Update    : Apr 1, 2017                                *
    '*                                                             *
    '* Subroutines    : Test Switching - calls Test LF1-3,MF,HF    *
    '*                        Test LF1 - Switch1or2Proc            *
    '*                        Test LF2 - Switch1or2Proc            *
    '*                        Test LF3 - Switch3Proc               *
    '*                        Test MF  - Switch4Proc               *
    '*                        Test HF  - Switch5Proc               *
    '*                Closure Detected - Tests for a closed switch *
    '*                   Open Detected - Tests for an open switch  *
    '***************************************************************
Public Module modSwitchingTest
    Dim FirstSwitchTested As Boolean 'Indicates if switch is the first tested with the DMM
    Dim dReturnResistance As Double 'Holds the Measured Resistance value in OpenDetected and ClosureDetected
    Dim iStepCounter As Integer 'Counter to track step number in Loop, used as the Sub Test number
    Dim iTestStep As Integer 'Test Step Counter
    Dim iSubStep As Integer 'Test SubTest Counter
    Dim iModAddress As Integer 'Module Address
    Public PathImpedance As Double 'Switch Path Impedance
    Dim PowerSwitchTest As Boolean 'Determines if Power Switch Matrix is being tested

    'bb
    Dim TestSwitch As String = ""
    Dim EnableClean As Boolean = False
    Dim CleanSwitch As String = ""

    'To compensate for the DMM accuracy of +/- 200 mOhms
    'Subtract "0.2 Ohms" from the LL and add "0.2 Ohms" to the UL
    Const DMM_TOL As Double = 0.2 'DMM 2-Wire Tolerance    'DR#265, 03/18/02

    Function TestSwitching(ByVal InstrumentToTest As Integer) As Integer
        'DESCRIPTION
        '   This routine tests the Racal Instruments Switching cards.
        '            *** NOTE***
        '       SWITCH1 is the handle for all switch modules because it contains the controller
        'PARAMETERS:
        '   InstrumentToTest = The index of the switch to be tested (SWITCH1 - SWITCH5)

        ' Switch 1 - Low Frequency switches
        ' Switch 2 - Low Frequency switches
        ' Switch 3 - Low Frequency switches
        ' Switch 4 - Medium Frequency switches
        ' Switch 5 - High Frequency switches

        Dim FailOccur As Short
        Dim X As DialogResult
        Dim S As String

        AbortTest = False
        SwitchTest = True
        TestSwitching = PASSED
        EchoTitle(InstrumentDescription(InstrumentToTest) & " ", True)
        If (RunningEndToEnd = False) Or (InstrumentToTest = SWITCH1) Then
            FirstSwitchTested = True
        Else
            FirstSwitchTested = False
        End If

        Select Case InstrumentToTest
            Case SWITCH1
                '1260-39     (LF1, 2amp and 10amp switches, with controller)
                FailOccur = Switch1Or2Proc(InstrumentToTest)
            Case SWITCH2
                '1260-39     (LF2, 2amp and 10amp switches, no controller)
                FailOccur = Switch1Or2Proc(InstrumentToTest)
            Case SWITCH3
                '1260-38A(T) (LF3 is the 2amp two/four wire switches, no controller)
                FailOccur = Switch3Proc()
            Case SWITCH4
                '1260-58     (Medium frequency switches, no controller)
                FailOccur = Switch4Proc()
                nSystErr = WriteMsg(COUNTER, "*RST")
                nSystErr = WriteMsg(COUNTER, "*CLS")
            Case SWITCH5
                '1260-66A    (High frequency switches, no controller)
                FailOccur = Switch5Proc()
        End Select


        If AbortTest = True Then
            If FailOccur Then
                TestSwitching = FAILED
                ReportFailure(InstrumentToTest)
            Else
                ReportUnknown(InstrumentToTest)
                TestSwitching = 99
            End If
            Exit Function
        End If

        If FailOccur <> PASSED Then ' failoccur 0=passed, 1=failed?
            If FirstSwitchTested Then ' see if the dmm is ok
                'Note: SP1 must be installed.  What about MF,HF? may need to install SP1!
                If (RunningEndToEnd = False) And (InstrumentToTest = SWITCH4 Or InstrumentToTest = SWITCH5) Then
                    X = MsgBox("Is adapter SP1 installed on SAIF J1 connector?", MsgBoxStyle.YesNo)
                    If X <> DialogResult.Yes Then
                        S = "Install SP1-P1 Adapter onto the SAIF J1 connector. "
                        DisplaySetup(S, "ST-SP1-1.jpg", 1)
                    End If
                End If
                'see if the DMM is known good
                If InstrumentStatus(DMM) = PASSED And RunningEndToEnd Then
                    TestSwitching = FAILED
                    Exit Function
                End If
                'ok then verify the dmm
                ' open all switches, then close interconnect relays
                nSystErr = WriteMsg(SWITCH1, "RESET")
                nSystErr = WriteMsg(SWITCH1, "OPEN 3.1000,1002,2000,2001,3002,4002,5000,5001,5002,6002,7002,8002")
                nSystErr = WriteMsg(SWITCH1, "CLOSE 3.1001,2002,3000,3001,4000,4001,6000,6001,7000,7001")
                'close S101-25,26,27,28 to connect 13 ohm Load to DMM
                nSystErr = WriteMsg(SWITCH1, "CLOSE 2.0001")
                Delay(0.5)
                nSystErr = WriteMsg(DMM, "*RST")
                nSystErr = WriteMsg(DMM, "*CLS")
                nSystErr = WriteMsg(DMM, "CONF:RES 30")
                nSystErr = WriteMsg(DMM, "INIT;FETCH?")
                'LFx-(TestStep)-D01 - diagnostic step
                dMeasurement = FetchMeasurement(DMM, "Switch Diagnostic Test", "13", "17.2", "Ohms", DMM, ReturnTestNumber(InstrumentToTest, iTestStep, 1, "D"))
                nSystErr = WriteMsg(SWITCH1, "OPEN 2.0001") ' disconnect 13 ohm Load to DMM
                If dMeasurement < 13 Or dMeasurement > 17.2 Then
                    TestSwitching = DMM
                    Exit Function
                End If
            End If
            TestSwitching = FAILED
            ReportFailure(InstrumentToTest)
            Exit Function
        End If

        TestSwitching = PASSED
        ReportPass(InstrumentToTest)

    End Function


    Function SwitchingError(ByRef mModule As Integer) As Integer
        'DESCRIPTION
        '   This routine returns any errors reported by the 1260 relay cards
        'PARAMETERS:
        '   This integer is returned with the erroring switching module number
        'RETURNS:
        '   The error code of the switching card
        Dim errorcode As String = ""
        Dim PerPosition As Integer

        mModule = 0
        nSystErr = WriteMsg(SWITCH1, "YERR")
        nSystErr = ReadMsg(SWITCH1, errorcode)
        If Len(errorcode) > 4 Then
            PerPosition = InStr(errorcode, ".")
            mModule = CShort(Val(Strings.Mid(errorcode, PerPosition - 3, 3)))
            SwitchingError = CInt(Val(Strings.Mid(errorcode, PerPosition + 1, 2)))
        Else
            SwitchingError = nSystErr
        End If

    End Function


    Function ClosureDetected(SwitchLable As String, dLower As Double, dUpper As Double) As Boolean

        'DESCRIPTION:
        'This routine detects the presence of a closed circuit between the high and low
        'terminals of the DMM.
        'New, if a closed switch path tests greater than (maximum impedance - 0.5 ohms),
        ' then an automatic switch cleaning process will attempt to clean the switch.
        ' Afterwards, the switch will be retested at the regular maximum impedance.

        'For use in switching tests only.
        'PARAMETERS:
        ' dLower - minimum impedance allowed - NOT USED for LF1,LF2,LF3
        ' dUpper - maximum impedance allowed - External path is measured and subtracted from
        '          the results where possible.


        Dim dMeasurement As Double = 0 'Measurement Value


        Dim sMeasString As String = "" 'Measurement Value as a String
        Dim i As Short 'Counter Index for Loop
        Dim temp As String
        Dim CleanTestX As Single

        'pretest
        temp = ""
        ClosureDetected = True

        'if this is a switch that can be cleaned, test it at .5 ohm less than max imp
        CleanTestX = 0
        If EnableClean = True And CleanSwitch <> "" Then
            ' If the switch is within 0.5 ohms of the upper limit, clean the switch
            CleanTestX = 0.5
        End If

        For i = 1 To 5 'Allow up to 1 second for switch closure and settling
            Delay(0.1)
            nSystErr = WriteMsg(DMM, "MEAS:RES? 50,MAX")
            nSystErr = ReadMsg(DMM, sMeasString)
            If Strings.Left(sMeasString, 1) = "-" Then ' abs(sMeasString)
                sMeasString = Strings.Mid(sMeasString, 2)
            End If
            dMeasurement = CDbl(sMeasString)
            dReturnResistance = dMeasurement 'save 1st dmeasurement
            If PathImpedance > 0 Then
                dReturnResistance = Math.Abs(dMeasurement - PathImpedance)
                dMeasurement = dReturnResistance
            End If
            If (dMeasurement < (dUpper - CleanTestX)) Then Exit For
        Next i


        'now verify the switch passes maximum impedance test
        If (dLower > 0) And (dMeasurement < dLower) Then
            ClosureDetected = False ' switch path must be shorted

        ElseIf dMeasurement > (dUpper - CleanTestX) Then
            If (EnableClean = True) And (CleanSwitch <> "") Then
                ' this routine will clean the switch specified by CleanSwitch
                temp = "    Clean " + FnSwitchName(CleanSwitch) + " Before: " & EngNotate(dMeasurement) + "Ohms"

                ' First, open the selected switch so we can apply power to it
                nSystErr = WriteMsg(SWITCH1, "OPEN " & CleanSwitch)

                If iModAddress = 4 Or iModAddress = 5 Then ' no power for switches 4,5
                    For i = 1 To 50 ' bang the switch 50 times
                        nSystErr = WriteMsg(SWITCH1, "CLOSE " & CleanSwitch) 'on 49ms
                        Delay(0.049) ' delay 49 milliseconds
                        nSystErr = WriteMsg(SWITCH1, "OPEN " & CleanSwitch) 'off 49ms
                        Delay(0.049) ' delay 49 milliseconds
                    Next i

                Else
                    ' Turn on DC1(or DC10) to 20VDC at .1amps via SP1
                    If iModAddress = 2 Then 'LF2 uses DC10
                        nSystErr = WriteMsg(SWITCH1, "CLOSE 2.0000") ' connect DC10 to the DMM (DMM connected to 1-wire)
                        CommandSetCurrent(10, 0.1) 'set current limit to 100ma
                        CommandSetOptions(10, CInt(False), CInt(True), CInt(True), CInt(True)) ' close relay, set master, sense local, current limit
                        Delay(0.8)
                        CommandSetVoltage(10, 20)
                    Else                        ' LF1,LF3 uses DC1
                        nSystErr = WriteMsg(SWITCH1, "CLOSE 1.0000") ' connect DC1 to the DMM (DMM connected to 1-wire)
                        CommandSetCurrent(1, 0.1) 'set current limit to 100ma
                        CommandSetOptions(1, CInt(False), CInt(True), CInt(True), CInt(True)) ' close relay, set master, sense local, current limit
                        Delay(0.8)
                        CommandSetVoltage(1, 20)
                    End If

                    'This loop cycles the relay contacts with DC1(or DC10) voltage applied (20V)
                    ' to clean carbon deposits off the contacts.
                    For i = 1 To 10 ' bang the switch 10 times in one second
                        nSystErr = WriteMsg(SWITCH1, "CLOSE " & CleanSwitch) 'on time very short (about 1ms)
                        Delay(0.001) ' delay 1 milliseconds
                        nSystErr = WriteMsg(SWITCH1, "OPEN " & CleanSwitch) 'off 99ms
                        Delay(0.099) ' delay 99 milliseconds
                    Next i

                    ' Reset DC1 to turn it off
                    If iModAddress = 2 Then 'LF2 uses DC10
                        CommandSetOptions(10, CInt(True), CInt(True), CInt(True), CInt(True)) ' open DC10 output relay
                        Delay(0.2)
                        CommandSupplyReset(10) ' Reset will clear DC10 which may have tripped
                        Delay(2.5)
                        If PowerSwitchTest = False Then
                            nSystErr = WriteMsg(SWITCH1, "OPEN 2.0000")
                        End If
                    Else                        ' LF1,LF3 uses DC1
                        CommandSetOptions(1, CInt(True), CInt(True), CInt(True), CInt(True)) ' open DC1 output relay
                        Delay(0.2)
                        CommandSupplyReset(1) ' Reset will clear DC1 which may have tripped
                        Delay(2.5)
                        If PowerSwitchTest = False Then
                            nSystErr = WriteMsg(SWITCH1, "OPEN 1.0000")
                        End If
                    End If
                End If

                'Now retest the switch
                nSystErr = WriteMsg(SWITCH1, "CLOSE " & CleanSwitch)

                For i = 1 To 5 'Allow up to 1 second for switch closure and settling
                    Delay(0.1) 'Allow time for switch closure and settling
                    nSystErr = WriteMsg(DMM, "MEAS:RES? 50,MAX")
                    nSystErr = ReadMsg(DMM, sMeasString)
                    If Strings.Left(sMeasString, 1) = "-" Then ' abs(sMeasString)
                        sMeasString = Strings.Mid(sMeasString, 2)
                    End If
                    dMeasurement = CDbl(sMeasString)
                    If PathImpedance > 0 Then
                        dReturnResistance = Math.Abs(dMeasurement - PathImpedance)
                        dMeasurement = dReturnResistance
                    End If
                    If dMeasurement < dUpper Then Exit For
                Next i
                temp = temp & "  After: " & EngNotate(dMeasurement) & "Ohms" '& vbCrLf

                ' test switch after it is cleaned
                If (dMeasurement > dUpper) Then
                    ClosureDetected = False ' switch impedance test failed ********
                Else
                    FirstSwitchTested = False ' switch impedance test passed
                End If

                ' test switches where no cleaning is allowed
            ElseIf (dMeasurement > dUpper) Then
                ClosureDetected = False ' switch impedance test failed ********
            End If
        ElseIf (dLower <> 0) And (dMeasurement < dLower) Then
            ClosureDetected = False ' switch impedance test failed ********
        Else
            ' if at least one dmm measurement passed, no need to test dmm
            FirstSwitchTested = False
        End If

        'Report Formatted result w/Pass-Fail Status to System Log, Record Failures in the FHDB
        RecordTest(sTNum, SwitchLable & " Closed Test", dLower, dUpper, dMeasurement, "Ohms")

        ' add the switch cleaned information
        If temp <> "" Then
            Echo(temp, 5) ' report before and after cleaning results
        End If

    End Function


    Function OpenDetected(SwitchNum As String, sInstrumentCode As String) As Short
        'DESCRIPTION:
        '   This routine detects the presence of an open circuit between the
        '   high and low terminals of the DMM
        'PARAMETERS:
        '   SwitchNum = This is a string description of the switch being tested
        '------------------------------------------------------------------------
        '***  Failures are recorded in the FHDB in the calling routine  ***

        Dim StrMeas As String = "" 'String containing the Measurement
        Dim retVal As Integer 'Return Value from function
        Dim i As Short
        Dim MinImp As Integer

        Dim InstrumentToTest As Short

        If sInstrumentCode = "LF1" Then
            InstrumentToTest = SWITCH1
        ElseIf sInstrumentCode = "LF2" Then
            InstrumentToTest = SWITCH2
        ElseIf sInstrumentCode = "LF3" Then
            InstrumentToTest = SWITCH3
        ElseIf sInstrumentCode = "MFS" Then
            InstrumentToTest = SWITCH4
        ElseIf sInstrumentCode = "HFS" Then
            InstrumentToTest = SWITCH5
        End If


        MinImp = 1000000 '1 Meg

        For i = 1 To 10 ' allow up to 1 second for switch settling
            Delay(0.1)
            nSystErr = WriteMsg(DMM, "MEAS:RES? 10E6,MAX")
            retVal = ReadMsg(DMM, StrMeas)
            If StrMeas = "" Or Strings.Left(StrMeas, 1) = "-" Then StrMeas = "0" 'Protect Data Type from Error
            dReturnResistance = CDbl(StrMeas) 'Convert String to Double
            If Val(StrMeas) > MinImp And retVal = 0 Then Exit For
        Next i

        'Determine if switch falls within the acceptable Open range

        If Val(StrMeas) < MinImp Or retVal <> 0 Then
            'Report FAIL status to the System Log
            OpenDetected = FAILED
            FormatResultLine(sTNum & " " & FnSwitchName(SwitchNum) & " Open Test Measured: " & EngNotate(dReturnResistance, 3) & "Ohms ", False) 'failed

            'Report Failure to FHDB
            RegisterFailure(CStr(InstrumentToTest), sTNum, dReturnResistance, "Ohms", MinImp, , FnSwitchName(SwitchNum))

        Else            'Report PASSED status to the System Log
            OpenDetected = PASSED
            FormatResultLine(sTNum & " " & FnSwitchName(SwitchNum) & " Open Test ", True) ' passed
        End If

    End Function


    Function Switch1Or2Proc(InstrumentToTest As Integer) As Integer

        'DESCRIPTION:   Switch tests for LF1 and LF2
        ' some switch paths only include the SAIF internal wiring,
        '   where others include the SP1 plug
        ' This routine takes each switch path and opens/closes every switch in the path.
        '
        'PARAMETERS:
        '   InstrumentToTest = This determines whether LF-1 or LF-2 will be tested
        '                       (SWITCH1 for LF-1, SWITCH2 for LF-2)
        'RETURNS:
        '   PASSED (0) if the switch being tested passed or FAILED (1) if the test fails


        Dim X As Integer
        Dim Channels(25) As String
        Dim ModuleNo As String = ""
        Dim RetMess As String = ""
        Dim mModule As Integer
        Dim SError As Integer
        Dim LastChannel As Integer
        Dim Channel As Integer
        Dim group As Integer
        Dim CommonX As Integer
        Dim retVal As Integer
        Dim StrMeas As String = ""
        Dim StrMeasurement As String = ""
        Dim ValMeasurement As String = ""
        Dim iStatus As Integer
        Dim i As Integer
        Dim ChannelNo As String = ""
        Dim PathNo As String = ""
        Dim TestName As String = ""
        Dim PathX As Integer
        Dim TestLoad As Boolean
        Dim MaxImp As Double = 0
        Dim MinImp As Double = 0
        Dim sMeasString As String = ""
        Dim S As String
        Dim LoopStepNo As Integer

        Switch1Or2Proc = UNTESTED
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        AbortTest = False

        iTestStep = 1
        iSubStep = 0
        LoopStepNo = 0
        Application.DoEvents()
        PathImpedance = 0
        PowerSwitchTest = False
        frmSTest.proProgress.Maximum = 100
        frmSTest.proProgress.Value = 0
        EnableClean = True
        CleanSwitch = ""


        If InstrumentToTest = SWITCH1 Then 'LF1
            iModAddress = 1
            ModuleNo = "1"
            TestName = "LF1 Switch"
            HelpContextID = 1130
            EchoTitle("Low Frequency Switching # 1 Test ", False)
        Else            'SWITCH2 LF2
            iModAddress = 2
            ModuleNo = "2"
            TestName = "LF2 Switch"
            HelpContextID = 1140
            EchoTitle("Low Frequency Switching # 2 Test ", False)
        End If

        If (FirstPass = True) Then
            If (ControlStartTest = 0 And RunningEndToEnd = False) Then
                X = MsgBox("Is the SP1 adapter installed?", MsgBoxStyle.YesNo)
                If X <> DialogResult.Yes Then
                    S = "Install the SP1 Adapter (93006H6042) onto the SAIF J1 connector. "
                    DisplaySetup(S, "ST-SP1-1.jpg", 1)
                End If

            ElseIf (ControlStartTest = InstrumentToTest) Then
Step1:          ' install adapter SP1
                S = "Install the SP1 Adapter (93006H6042) onto the SAIF J1 connector. " & vbCrLf
                DisplaySetup(S, "ST-SP1-1.jpg", 1, True, 1, 2)
                If AbortTest = True Then GoTo TestComplete

Step2:          ' install W203 MF cable
                S = "Install cable W203 (93006H6044) as follows:" & vbCrLf
                S &= "1. Connect cable W203-P1 onto the SAIF J2 connector. " & vbCrLf
                S &= "2. Use a BNC 50 Ohm Feedthrough Terminator to connect cable W203-P2 onto the SAIF front panel MF IN connector. " & vbCrLf
                S &= "3. Connect cable W203-P3 onto the SAIF front panel MF OUT connector. " & vbCrLf
                DisplaySetup(S, "ST-W203-1.jpg", 3, True, 2, 2)
                If AbortTest = True Then GoTo TestComplete
                If GoBack = True Then GoTo Step1

            End If
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        Switch1Or2Proc = PASSED

        nSystErr = WriteMsg(SWITCH1, "RESET") ' opens all switches
        Delay(0.5)


        ' switch controller BIT tests
        If InstrumentToTest = SWITCH1 Then ' controller tests for LF1 only

LF1_1_1:    'LF1-01-001        Non-Destructive RAM Test
            sTNum = "LF1-01-001" : LoopStepNo = 1
            nSystErr = WriteMsg(SWITCH1, "TEST 0.1") 'Non-Destructive RAM Test
            Delay(0.5)
            nSystErr = ReadMsg(SWITCH1, RetMess)
            If Trim(RetMess) <> "7F" Then
                Echo(FormatResults(False, "1260 Switching Module Non-Destructive RAM Test", sTNum))
                Echo("  RAM Failure ERROR <" & SwitchingError(mModule) & ">")
                Switch1Or2Proc = FAILED
                FirstSwitchTested = False
                IncStepFailed()
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else
                Echo(FormatResults(True, "1260 Switching Module Non-Destructive RAM Test", sTNum))
                IncStepPassed()
            End If
            Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete
            If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then
                GoTo LF1_1_1
            End If

LF1_1_2:    'LF1-01-002        EPROM Checksum Test
            sTNum = "LF1-01-002" : LoopStepNo = 2
            nSystErr = WriteMsg(SWITCH1, "TEST 0.2") ' EPROM Checksum Test
            nSystErr = ReadMsg(SWITCH1, RetMess)
            If Trim(RetMess) <> "7F" Then
                Echo(FormatResults(False, "1260 Switching Module Checksum Test", sTNum))
                Echo("  EPROM Checksum Failure ERROR <" & SwitchingError(mModule) & ">")
                Switch1Or2Proc = FAILED
                IncStepFailed()
                FirstSwitchTested = False
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else
                Echo(FormatResults(True, "1260 Switching Module Checksum Test", sTNum))
                IncStepPassed()
            End If
            Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete
            If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then
                GoTo LF1_1_2
            End If

LF1_1_3:    'LF1-01-003        Non-Destructive Non-Volatile Memory Test
            sTNum = "LF1-01-003" : LoopStepNo = 3
            nSystErr = WriteMsg(SWITCH1, "TEST 0.3") 'Non-Destructive Non-Volatile Memory Test
            nSystErr = ReadMsg(SWITCH1, RetMess)
            If Trim(RetMess) <> "7F" Then
                Echo(FormatResults(False, "1260 Switching Module Non-Volatile Memory Test", sTNum))
                Echo("  Memory Failure ERROR <" & SwitchingError(mModule) & ">")
                Switch1Or2Proc = FAILED
                IncStepFailed()
                FirstSwitchTested = False
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else
                Echo(FormatResults(True, "1260 Switching Module Non-Volatile Memory Test", sTNum))
                IncStepPassed()
            End If
            Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete
            If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then
                GoTo LF1_1_3
            End If

LF1_1_4:    'LF1-01-004        Switch RESET Self test
            sTNum = "LF1-01-004" : LoopStepNo = 4
            nSystErr = WriteMsg(SWITCH1, "RESET")
            Delay(0.1)
            SError = SwitchingError(mModule)
            If (SError <> 0) Then
                Echo(FormatResults(False, "1260 Switching Module Reset Test", sTNum))
                Echo("  Reset Test error<" & SError & "> from module " & CStr(mModule))
                Echo("  This is a possible DIP switch error on one of the switch cards in the ATS.")
                Echo("  If possible, try swapping switch cards to identify the bad card, and ")
                Echo("    then try toggling each DIP switch on that card.")
                Switch1Or2Proc = FAILED
                IncStepFailed()
                FirstSwitchTested = False
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else
                Echo(FormatResults(True, "1260 Switching Module Reset Test", sTNum))
                IncStepPassed()
            End If
            Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete
            If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then
                GoTo LF1_1_4
            End If

        End If

        frmSTest.sbrUserInformation.Text = "Testing " & InstrumentDescription(SWITCH1)
        Application.DoEvents()
        LoopStepNo += 1 ' 1 for LF2 or 5 forLF1

        nSystErr = WriteMsg(SWITCH1, "CNF ON") ' enable the confidence test on


LF1_1_5:  'Close all switches Test
        If InstrumentToTest = SWITCH1 Then 'Define Test Number
            sTNum = "LF1-01-005"
        Else
            sTNum = "LF2-01-001"
        End If
        SError = SwitchingError(mModule) ' clear error buffer
        nSystErr = WriteMsg(SWITCH1, "CLOSE " & ModuleNo & ".0-4417") ' close all switches in LF1 or LF2
        SError = SwitchingError(mModule) ' read status (in confidence mode generate error flag)
        If (SError <> 0) Then
            Echo(FormatResults(False, "1260 Switching Module Close Test", sTNum))
            Echo("  CNF Close All error<" & SError & "> from module" & CStr(mModule))
            Echo("  This is a possible DIP switch error on one of the switch cards in the ATS.")
            Echo("  If possible, try swapping switch cards to identify the bad card, and ")
            Echo("    then try toggling each DIP switch on that card.")
            Switch1Or2Proc = FAILED
            IncStepFailed()
            FirstSwitchTested = False
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            Echo(FormatResults(True, "1260 Switching Module Close Test", sTNum))
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then
            GoTo LF1_1_5
        End If
        LoopStepNo += 1 ' 2 or 6

LF1_1_6:  'LF1-01-006; LF2-01-002       Open all switches Test
        If InstrumentToTest = SWITCH1 Then 'Define Test Number
            sTNum = "LF1-01-006"
        Else
            sTNum = "LF2-01-002"
        End If
        SError = SwitchingError(mModule) ' clear error buffer

        nSystErr = WriteMsg(SWITCH1, "OPEN " & ModuleNo & ".0-4417") ' open all switches in LF1 or LF2
        SError = SwitchingError(mModule)
        If (SError <> 0) And (mModule = CInt(ModuleNo)) Then
            Echo(FormatResults(False, "1260 Switching Module Open Test", sTNum))
            Echo("  CNF Open All error<" & SError & "> from module" & CStr(mModule))
            Echo("  This is a possible DIP switch error on one of the switch cards in the ATS.")
            Echo("  If possible, try swapping switch cards to identify the bad card, and ")
            Echo("    then try toggling each DIP switch on that card.")
            Switch1Or2Proc = FAILED
            IncStepFailed()
            FirstSwitchTested = False
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            Echo(FormatResults(True, "1260 Switching Module Open Test", sTNum))
            IncStepPassed()
        End If

        frmSTest.proProgress.Value = 10
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then
            GoTo LF1_1_6
        End If

        nSystErr = WriteMsg(SWITCH1, "CNF OFF") ' disable the confidence test





        '*************************************************************************
        ' 1st, test switches used to connect power supply and loads in the SAIF
        ' 2nd, test all other switches connected inside the SAIF, but not brought out to J1
        ' 3rd, test the switches brought out to J1 and connected by the SP1 adapter
        '*************************************************************************




        ' 1st Test Power supply switches (S101,S301)
        PowerSwitchTest = True
        ' LF1 and LF2 SAIF PCB wiring
        ' init LFx-02-001 to 006 tests
        If InstrumentToTest = SWITCH1 Then ' LF1
            Echo(vbCrLf & "---- Test S101-1,2(3,4), 5,6(7,8), S101-9,10(11,12), 13,14(15,16), 17,18(19,20), S301-57 to 96")
            ' DMM-HI to S101-2,1,9,10,13,14,17,18 to S301-87/96 to S301-67/76 to
            '   S101-5,6,8,7 to S301-57/66 to S301-77/86 to S101-20,19,16,15,12,11,3,4 to DMM-LO
            ' Module 1 'Close S101-1,2(3,4), 5,6(7,), S101-9,10(11,12), 13,14(15,16), 17,18(19,20), S301-57 to 96
            ChannelNo = "1.0000-0004,1028-1047"
            Channels(1) = "1.0000" ' short DC1 to DMM via S101-1,2(3,4)
            Channels(2) = "1.0001" ' short DC6-HI to DC6-LO via S101-5,6(7,8)
            Channels(3) = "1.0002" ' short DC1 to DC2 via S101-9,10(11,12)
            Channels(4) = "1.0003" ' short DC2 to DC3 via S101-13,14(15,16)
            Channels(5) = "1.0004" ' short DC3 to DC4 via S101-17,18(19,20)
            nSystErr = WriteMsg(SWITCH1, "OPEN 2.0000-0004") ' disconnect DC10 and Loads from DMM

        Else            'LF2
            Echo(vbCrLf & "---- Test S101-21,22(23,24), 25,26(27,28), 29,30(31,32), 33,34(35,36), 37,38(39,40), S301-153 to 192")
            ' DMM-HI to S101-22,21,34,33,30,29 to S301-163/172 to S301-183/192 to
            '   S101-5,6,8,7 to S301-173/182 to S301-153/162 to S101-31,32,35,36,23,24 to DMM-LO
            ' Module 2 'Close S101-21,22(23,24), 29,30(31,32), 33,34(35,36), S301-153 to 192
            ChannelNo = "2.0000,0002,0003,1028-1047"
            Channels(1) = "2.0000" ' short DC10 to DMM via S101-21,22(23,24)
            Channels(2) = "2.0002" ' short DC8 to DC9 via S101-29,30(31,32)
            Channels(3) = "2.0003" ' short DC9 to DC10 via S101-33,34(35,36)
            Channels(4) = "2.0001" ' short Loads to DMM via S101-25,26(27,28)
            Channels(5) = "2.0004" ' connect 21.42 ohms to loads S101-37,38(39,40)
            nSystErr = WriteMsg(SWITCH1, "OPEN 1.0000-0004") ' disconnect DC1 from DMM
            nSystErr = WriteMsg(SWITCH1, "CLOSE 1.0001") ' short DC6
            nSystErr = WriteMsg(SWITCH1, "OPEN 2.0001,0004") ' disconnect Load from DMM

        End If
        nSystErr = WriteMsg(SWITCH1, "CLOSE " & ChannelNo) ' close every switch in the circuit

        nSystErr = WriteMsg(DMM, "*RST") ' init DMM
        nSystErr = WriteMsg(DMM, "*CLS") ' init DMM
        nSystErr = WriteMsg(DMM, "SENS:RES:APER MIN")

        'Added to clear any errors
        ValMeasurement = CStr(SwitchingError(mModule))
        If mModule <> CInt(ModuleNo) Then
            ValMeasurement = "000.00"
        End If

        'LFx-02-001 to 010  S101 Power Switches Open/Closed Tests
        ' These tests require DC6 to be shorted and DC1(switch1) or DC10(switch2) connected to the dmm via SP1
        '   S101-5,6,7,8 shorts DC6 via SP1
        '   S101-1,2,3,4 connects DC1 to DMM via SP1
        '   S101-21,22,23,24 connects DC10 to DMM via SP1
        'For LF1
        ' LF1-02-001,002 test S101-1,2,3,4     open/closed (shorts DC1 to DMM)
        ' LF1-02-003,004 test S101-5,6,7,8     open/closed (shorts DC6)
        ' LF1-02-005,006 test S101-9,10,11,12  open/closed (shorts DC1 to DC2)
        ' LF1-02-007,008 test S101-13,14,15,16 open/closed (shorts DC2 to DC3)
        ' LF1-02-009,010 test S101-17,18,19,20 open/closed (shorts DC3 to DC4)
        'For LF2
        ' LF2-02-001,002 test S101-21,22,23,24 open/closed (shorts DC10 to DMM)
        ' LF2-02-003,004 test S101-29,30,31,32 open/closed (shorts DC8 to DC9)
        ' LF2-02-005,006 test S101-33,34,35,36 open/closed (shorts DC9 to DC10)
        ' LF2-02-007,008 test S101-25,26,27,28 open/closed (shorts Loads to DMM)
        ' LF2-02-009,010 test S101-37,38,39,40 open/closed (changes Loads from 12.97 to 8.08 ohms)
        iTestStep = 2
        iSubStep = 1

        For Channel = 1 To 5
            If InstrumentToTest = SWITCH2 And Channel = 4 Then ' test S101-25,26,27,28
                'open all power relays
                TestLoad = True
                nSystErr = WriteMsg(SWITCH1, "OPEN 2.0000,0002,0003,1028-1047")
            ElseIf InstrumentToTest = SWITCH2 And Channel = 5 Then                ' test S101-37,38,39,40
                'close the path from the loads to the dmm
                TestLoad = True
                nSystErr = WriteMsg(SWITCH1, "CLOSE 2.0001") ' connect 13 ohm Load to DMM
            Else
                TestLoad = False
            End If
            sTNum = "LF" & ModuleNo & "-" & Format(iTestStep, "00") & "-" & Format(iSubStep, "000")
            iSubStep += 1
            LoopStepNo += 1

LF1_2_1:
            nSystErr = WriteMsg(SWITCH1, "OPEN " & Channels(Channel)) ' x.0000,0001,0002,0003, or 0004
            For i = 1 To 5 ' allow up to 1 second for switch settling
                Delay(0.1)
                If TestLoad = True And Channel = 5 Then ' LF2-02-009
                    nSystErr = WriteMsg(DMM, "MEAS:RES? 100,MAX") ' measure 13 ohms, set 100 ohm max
                Else
                    nSystErr = WriteMsg(DMM, "MEAS:RES? 10E3,MAX") ' measure open, set 10K ohm max
                End If
                retVal = ReadMsg(DMM, StrMeas)
                If StrMeas = "" Or Strings.Left(StrMeas, 1) = "-" Then StrMeas = "0" 'Protect Data Type from Error
                dReturnResistance = CDbl(StrMeas) 'Convert String to Double
                If Val(StrMeas) > 10000 And retVal = 0 Then Exit For
            Next i
            ValMeasurement = CStr(SwitchingError(mModule))

            ' test switch open
            'LFx-02-001,003,005,007,009
            ' Test for status error?
            ValMeasurement = CStr(SwitchingError(mModule))
            If (iStatus <> 0) Or (retVal <> 0) Or (mModule = CInt(ModuleNo)) Then
                sMsg = FnSwitchName(Channels(Channel)) & " Open Test"
                FormatResultLine(sTNum & " " & sMsg & "  Measured: " & StripNullCharacters(StrMeas) & " ", False)
                If StrMeas = "" Then StrMeas = "0"
                RegisterFailure(CStr(InstrumentToTest), sTNum, Val(StrMeas), "Ohms", MinImp, , sMsg)
                Switch1Or2Proc = FAILED
                IncStepFailed()
                If OptionFaultMode = SOFmode Then GoTo TestComplete

                'LF2-02-009
            ElseIf TestLoad = True And Channel = 5 Then
                MaxImp = 19
                MinImp = 13
                sMsg = FnSwitchName(Channels(Channel)) & " Open Test"
                If (Val(StrMeas) < (MinImp - DMM_TOL)) Or (Val(StrMeas) > (MaxImp + DMM_TOL)) Then ' open = 13.04 ohms
                    RecordTest(sTNum, sMsg, MinImp - DMM_TOL, MaxImp + DMM_TOL, Val(StrMeas), "Ohms")
                    If StrMeas = "" Then StrMeas = "0"
                    IncStepFailed()
                    Switch1Or2Proc = FAILED
                    If OptionFaultMode = SOFmode Then GoTo TestComplete
                Else
                    IncStepPassed()
                    RecordTest(sTNum, sMsg, MinImp - DMM_TOL, MaxImp + DMM_TOL, Val(StrMeas), "Ohms")
                End If

                'LF1-02-001,003,005,007,009, LF2-02-001,003,005,007
            ElseIf Val(StrMeas) < 500 Then
                'Code changed to take into account the bleed off resistor in the receiver being included
                'in the circuit that is measured.
                '
                ' Note: S101 is connected to the power supplies via the SAIF.  Even with the power supplies
                ' turned off the power filter assy connects the power supply lines to a bleedoff resistor
                ' (10kohms) and a filter cap (550uf) to the ATS front panel, hence to the S101 switches.
                sMsg = FnSwitchName(Channels(Channel)) & " Open Test"
                FormatResultLine(sTNum & " " & sMsg & "  Measured: " & StripNullCharacters(StrMeas), False)
                RegisterFailure(CStr(InstrumentToTest), sTNum, Val(StrMeas), "Ohms", MinImp, , sMsg)
                If StrMeas = "" Then StrMeas = "0"
                IncStepFailed()
                Switch1Or2Proc = FAILED
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else                ' passed
                IncStepPassed()
                sMsg = FnSwitchName(Channels(Channel)) & " Open Test "
                FormatResultLine(sTNum & " " & sMsg, True)
            End If
            Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete
            If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then
                GoTo LF1_2_1
            End If

            ' now test switch closed
            sTNum = "LF" & ModuleNo & "-" & Format(iTestStep, "00") & "-" & Format(iSubStep, "000")
            iSubStep += 1
            LoopStepNo += 1

LF1_2_2:
            nSystErr = WriteMsg(SWITCH1, "CLOSE " & Channels(Channel))
            PathImpedance = 0
            TestSwitch = FnSwitchName(Channels(Channel)) ' S101-1,2
            EnableClean = True
            CleanSwitch = Channels(Channel) ' 1.0000
            If TestLoad = True Then
                If Channel = 4 Then ' S101-25,26,27,28 closed
                    MaxImp = 19 + DMM_TOL '    = 13.04 + 2 relays + path  (typ 15 ohms)
                    MinImp = 13 - DMM_TOL
                ElseIf Channel = 5 Then                    ' S101-25,26,27,28 and S101-37,38,39,40 closed
                    MaxImp = 14 + DMM_TOL '    = 8.11 + 4 relays + path   (typ 10.5 ohms)
                    MinImp = 8 - DMM_TOL
                End If
            ElseIf InstrumentToTest = SWITCH1 Then
                MaxImp = 8 + DMM_TOL '      = 0 + 10 S101 relays + 4 ganged S301s + path  (typ 3.8 to 4 ohms)
                MinImp = 0
            Else
                MaxImp = 8 + DMM_TOL '       = 0 + 8 S101 relays + 4 ganged S301s + path   (typ 4 to 4.2 ohms)
                MinImp = 0
            End If

            '   LFx-02-002,004,006,008,010 ' Note: LF1-02-002 1st dmm short test
            If Not ClosureDetected(TestSwitch, MinImp, MaxImp) Then
                IncStepFailed()
                Switch1Or2Proc = FAILED
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else
                IncStepPassed()
            End If
            Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete
            If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then
                nSystErr = WriteMsg(SWITCH1, "OPEN " & Channels(Channel))
                GoTo LF1_2_2
            End If

            frmSTest.proProgress.Value = frmSTest.proProgress.Value + 2
            Application.DoEvents()
        Next Channel

        'LFx-02-011 to 034   S301 Power Switches Closed tests
        'if SWITCH1 then
        ' test S301-57 to S301-96  closed (shorts DC4 to DC5 to DC6),,,
        'elseif Switch2 then
        ' test S301-153 to S301-192 closed (shorts DC6 to DC7 to DC8),,,

        ' First, close the whole path
        If InstrumentToTest = SWITCH1 Then
            ' disconnect DC10 and Loads from DMM
            nSystErr = WriteMsg(SWITCH1, "OPEN 2.0000-0004")
            ' Close S101-1,2(3,4), 5,6(7,8), 9,10(11,12), 13,14(15,16), 17,18(19,20), S301-57 to 96
            nSystErr = WriteMsg(SWITCH1, "CLOSE 1.0000-0004,1028-1047")
        Else
            nSystErr = WriteMsg(SWITCH1, "OPEN 1.0000-0004") ' disconnect DC1 from DMM
            nSystErr = WriteMsg(SWITCH1, "OPEN 2.0001,0004") ' disconnect Load from DMM
            ' Close S101-21,22(23,24), 29,30(31,32), 33,34(35,36), S301-153 to 192
            nSystErr = WriteMsg(SWITCH1, "CLOSE 2.0000,0002,0003,1028-1047")
            nSystErr = WriteMsg(SWITCH1, "CLOSE 1.0001") ' short DC6
        End If

        ' measure path impedance, don't open the switch path again until after each parallel
        '  switch has been tested
        Delay(0.5)
        nSystErr = WriteMsg(DMM, "MEAS:RES? 50,MAX")
        Delay(0.3)
        nSystErr = ReadMsg(DMM, sMeasString)
        PathImpedance = CDbl(sMeasString) ' use this for all parallel switches only
        If PathImpedance > 10 Then ' invalid path impedance, try again
            Delay(0.3)
            nSystErr = WriteMsg(DMM, "MEAS:RES? 50,MAX")
            Delay(0.3)
            nSystErr = ReadMsg(DMM, sMeasString)
            PathImpedance = CDbl(sMeasString)
            If PathImpedance > 10 Then ' invalid path impedance
                Echo("---- Path Impedance too high at " & CStr(PathImpedance) & " Ohms!")
                Echo("---- Setting path impedance default to 2.0 Ohms.")
                PathImpedance = 2
            End If
        End If

        For group = 1 To 4
            Select Case group
                Case 1
                    nSystErr = WriteMsg(SWITCH1, "OPEN " & ModuleNo & ".1028-1032") ' open switches to be tested (group of 5)
                    TestSwitch = FnSwitchName(ModuleNo & ".1028-1032")
                Case 2
                    nSystErr = WriteMsg(SWITCH1, "CLOSE " & ModuleNo & ".1028-1032") ' close previously tested switches
                    nSystErr = WriteMsg(SWITCH1, "OPEN " & ModuleNo & ".1033-1037") ' open switches to be tested
                    TestSwitch = FnSwitchName(ModuleNo & ".1033-1037")
                Case 3
                    nSystErr = WriteMsg(SWITCH1, "CLOSE " & ModuleNo & ".1033-1037")
                    nSystErr = WriteMsg(SWITCH1, "OPEN " & ModuleNo & ".1038-1042")
                    TestSwitch = FnSwitchName(ModuleNo & ".1038-1042")
                Case 4
                    nSystErr = WriteMsg(SWITCH1, "CLOSE " & ModuleNo & ".1038-1042")
                    nSystErr = WriteMsg(SWITCH1, "OPEN " & ModuleNo & ".1043-1047")
                    TestSwitch = FnSwitchName(ModuleNo & ".1043-1047")
            End Select
            ' test group of 5 switches open
            'LFx-02-011,017,023,029
            sTNum = "LF" & ModuleNo & "-" & Format(iTestStep, "00") & "-" & Format(iSubStep, "000")
            iSubStep += 1
            LoopStepNo += 1
            sMsg = TestSwitch & " Open Test "

LF1_2_11:
            Delay(0.5)
            nSystErr = WriteMsg(DMM, "MEAS:RES?") ' do this twice, because of power supply caps, resistors
            Delay(0.1)
            retVal = ReadMsg(DMM, StrMeas)

            Delay(1)
            nSystErr = WriteMsg(DMM, "MEAS:RES?")
            Delay(0.1)
            retVal = ReadMsg(DMM, StrMeas)
            If StrMeas = "" Then StrMeas = "0"

            ValMeasurement = CStr(SwitchingError(mModule))
            If iStatus <> 0 Or retVal <> 0 Or mModule = CInt(ModuleNo) Then ' status error?
                FormatResultLine(sTNum & " " & sMsg & "Measured: " & Format(StripNullCharacters(StrMeas), "0.0") & " Ohms ", False)
                RegisterFailure(CStr(InstrumentToTest), sTNum, , , , , sMsg & "Measured: " & Format(StripNullCharacters(StrMeas), "0.0") & " Ohms ")
                Switch1Or2Proc = FAILED
                IncStepFailed()
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            ElseIf Val(StrMeas) < 500 Then                ' not open
                FormatResultLine(sTNum & " " & sMsg & "Measured: " & Format(StripNullCharacters(StrMeas), "0.0") & " Ohms ", False)
                RegisterFailure(CStr(InstrumentToTest), sTNum, , , , , sMsg & "Measured: " & Format(StripNullCharacters(StrMeas), "0.0") & " Ohms ")
                Switch1Or2Proc = FAILED
                IncStepFailed()
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else                ' test passed
                IncStepPassed()
                '  Echo FormatResults(True, sMsg, sTNum)
                FormatResultLine(sTNum & " " & sMsg, True)
            End If
            Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete
            If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then
                GoTo LF1_2_11
            End If

            For Channel = 1 To 5 '5 parallel switches per group
                'setup next test
                Select Case group ' 4 groups
                    Case 1 : ChannelNo = ModuleNo & ".10" & Format(Channel + 27, "00") ' S301-57,58 to 65,66
                    Case 2 : ChannelNo = ModuleNo & ".10" & Format(Channel + 32, "00") ' S301-67,68 to 75,76
                    Case 3 : ChannelNo = ModuleNo & ".10" & Format(Channel + 37, "00") ' S301-77,78 to 85,86
                    Case 4 : ChannelNo = ModuleNo & ".10" & Format(Channel + 42, "00") ' S301-87,88 to 95,96
                End Select
                'now test parallel switches closed one switch at a time
                sTNum = "LF" & ModuleNo & "-" & Format(iTestStep, "00") & "-" & Format(iSubStep, "000")
                iSubStep += 1
                LoopStepNo += 1

LF1_2_12:
                TestSwitch = FnSwitchName(ChannelNo) 'S301-57,58
                CleanSwitch = ChannelNo '1.1028
                nSystErr = WriteMsg(SWITCH1, "CLOSE " & ChannelNo) ' close switch

                'LFx-02- (0012-016,018-022,024-028,030-034)
                If Not ClosureDetected(TestSwitch, 0, 1.5 + DMM_TOL) Then
                    IncStepFailed()
                    Switch1Or2Proc = FAILED
                    If OptionFaultMode = SOFmode Then GoTo TestComplete
                Else
                    IncStepPassed()
                End If

                nSystErr = WriteMsg(SWITCH1, "OPEN " & ChannelNo) ' Open switch
                Application.DoEvents()
                If AbortTest = True Then GoTo TestComplete
                If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then
                    GoTo LF1_2_12
                End If
                frmSTest.proProgress.Value = frmSTest.proProgress.Value + 1
                Application.DoEvents()
            Next Channel
        Next group

        nSystErr = WriteMsg(SWITCH1, "OPEN " & ModuleNo & ".0-4417") ' open all switches in selected module
        Application.DoEvents()
        PowerSwitchTest = False


        '*************************************************************************
        ' 2nd, test all other switches connected inside the SAIF
        '   that are NOT brought out to the J1 top panel connector

        ' 1st, close every switch in the path and test each common switch open/closed
        ' 2nd, open all of the parallel switches, and test for an open path
        ' 3rd, test each parallel switch closed one switch at a time

        For PathX = 1 To 10
            'LFx-03-001 to 022 Path 1
            'LFx-04-001 to 009 Path 2
            'LFx-05-001 to 003 Path 3
            'LFx-06-001 to 007 Path 4
            'LFx-07-001 to 003 Path 5
            'LFx-08-001 to 012 Path 6
            'LFx-09-001 to 010 Path 7
            'LFx-10-001 to 010 Path 8
            'LFx-11-001 to 010 Path 9
            'LFx-12-001 to 010 Path 10
            iTestStep = 2 + PathX
            iSubStep = 1

            ' 10 common paths from S403 to S601
            ' Path 1   - LF1 S403-1,2 to S301-23,24(25,26,,,55-56) to S601-1,2
            ' Path 2,3 - LF1 S403-1,3 to S604-1,2(3) to S603-1,2(3) to S602-1,2 to S601-1,3
            ' Path 4,5 - LF1 S403-1,4 to S606-1,2(3) to S605-1,2(3) to S602-1,3 to S601-1,3
            ' Path 6   - LF1 S403-1,5 to S501-1,(3-10) to S502(3,4,5,6) to S501-2,(3-10) to S601-1,2
            ' Path 7   - LF1 S403-1,5 to S501-1,3 to S502-1,2 to (3,4,,,9,10) to S501-2,4  to S601-1,2
            ' Path 8   - LF1 S403-1,5 to S501-1,5 to S503-1,2 to (3,4,,,9,10) to S501-2,6  to S601-1,2
            ' Path 9   - LF1 S403-1,5 to S501-1,7 to S504-1,2 to (3,4,,,9,10) to S501-2,8  to S601-1,2
            ' Path 10  - LF1 S403-1,5 to S501-1,9 to S505-1,2 to (3,4,,,9,10) to S501-2,10 to S601-1,2

            ' 1st, close every switch in the path
            Select Case PathX
                Case 1
                    MaxImp = 6 : CommonX = 2
                    PathNo = ModuleNo & ".1011-1027,2000,3200" ' Path for S301-23 to 56 switches
                    Channels(1) = ModuleNo & ".3200" ' S403-1,2
                    Channels(2) = ModuleNo & ".2000" ' S601-1,2
                    TestSwitch = ModuleNo & ".3200,2000,1011-1027"
                Case 2
                    MaxImp = 7.5 : CommonX = 3
                    PathNo = ModuleNo & ".2001,2100,2200,2201,2300,2301,3201" ' Path for S603
                    Channels(1) = ModuleNo & ".3201" ' S403-1,3
                    Channels(2) = ModuleNo & ".2001" ' S601-1,3
                    Channels(3) = ModuleNo & ".2100" ' S602-1,2
                    TestSwitch = ModuleNo & ".3201,2001,2100,2200,2201"
                Case 3
                    MaxImp = 7.5 : CommonX = 0
                    PathNo = ModuleNo & ".2001,2100,2200,2201,2300,2301,3201" ' Path for S604
                    TestSwitch = ModuleNo & ".2300,2301"
                Case 4
                    MaxImp = 7.5 : CommonX = 2
                    PathNo = ModuleNo & ".2001,2101,2400,2401,2500,2501,3202" ' Path for S605
                    Channels(1) = ModuleNo & ".3202" ' S403-1,4
                    Channels(2) = ModuleNo & ".2101" ' S602-1,3
                    TestSwitch = ModuleNo & ".3202,2101,2400,2401"
                Case 5
                    MaxImp = 7.5 : CommonX = 0
                    PathNo = ModuleNo & ".2001,2101,2400,2401,2500,2501,3202" ' Path for S606
                    TestSwitch = ModuleNo & ".2500,2501"
                Case 6
                    MaxImp = 9 : CommonX = 1
                    PathNo = ModuleNo & ".2000,3203,4000-4017,4100-4417" ' Path for S501
                    Channels(1) = ModuleNo & ".3203" ' S403-1,5
                    TestSwitch = ModuleNo & ".3203,4000-4007,4010-4017"
                Case 7
                    MaxImp = 9 : CommonX = 0
                    PathNo = ModuleNo & ".2000,3203,4000,4011,4100-4117" ' Path for S502
                    TestSwitch = ModuleNo & ".4100-4107,4110-4117"
                Case 8
                    MaxImp = 9 : CommonX = 0
                    PathNo = ModuleNo & ".2000,3203,4002,4013,4200-4217" ' Path for S503
                    TestSwitch = ModuleNo & ".4200-4207,4210-4217"
                Case 9
                    MaxImp = 9 : CommonX = 0
                    PathNo = ModuleNo & ".2000,3203,4004,4015,4300-4317" ' Path for S504
                    TestSwitch = ModuleNo & ".4300-4307,4310-4317"
                Case 10
                    MaxImp = 9 : CommonX = 0
                    PathNo = ModuleNo & ".2000,3203,4006,4017,4400-4417" ' Path for S505
                    TestSwitch = ModuleNo & ".4400-4407,4410-4417"
            End Select
            nSystErr = WriteMsg(SWITCH1, "CLOSE " & PathNo) ' close the whole path
            Echo(vbCrLf & "---- Test " & FnSwitchName(TestSwitch))

            ' Test each common switch open/closed that has not been tested
            PathImpedance = 0
            For Channel = 1 To CommonX
                ChannelNo = Channels(Channel)
                ' Test the common switch open
                'LFx-03-001,003, LFx-04-001,003,005 LFx-06-001,003, LFx-08-001
                sTNum = "LF" & ModuleNo & "-" & Format(iTestStep, "00") & "-" & Format(iSubStep, "000")
                iSubStep += 1
                LoopStepNo += 1

LF1_3_1:
                nSystErr = WriteMsg(SWITCH1, "OPEN " & ChannelNo)
                If OpenDetected(ChannelNo, "LF" & ModuleNo) = FAILED Then
                    IncStepFailed()
                    Switch1Or2Proc = FAILED
                    If OptionFaultMode = SOFmode Then GoTo TestComplete
                Else
                    IncStepPassed()
                End If
                Application.DoEvents()
                If AbortTest = True Then GoTo TestComplete
                If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then
                    GoTo LF1_3_1
                End If

                'Test the common switch closed (path impedance=0)
                'LFx-03-002,004, LFx-04-002,004,006 LFx-06-002,004, LFx-08-002
                sTNum = "LF" & ModuleNo & "-" & Format(iTestStep, "00") & "-" & Format(iSubStep, "000")
                iSubStep += 1
                LoopStepNo += 1

LF1_3_2:
                TestSwitch = FnSwitchName(ChannelNo)
                CleanSwitch = ChannelNo ' name switch to clean
                nSystErr = WriteMsg(SWITCH1, "CLOSE " & ChannelNo)
                'LFx-0x-001
                If Not ClosureDetected(TestSwitch, 0, MaxImp + DMM_TOL) Then
                    IncStepFailed()
                    Switch1Or2Proc = FAILED
                    If OptionFaultMode = SOFmode Then GoTo TestComplete
                Else
                    IncStepPassed()
                End If
                Application.DoEvents()
                If AbortTest = True Then GoTo TestComplete
                If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then
                    nSystErr = WriteMsg(SWITCH1, "OPEN " & ChannelNo)
                    GoTo LF1_3_2
                End If
            Next Channel


            ' measure path impedance, then don't open the common switch path again until after each
            ' parallel switch has been tested so that the path impedance doesn't change.
            '   Delay 0.5
            nSystErr = WriteMsg(DMM, "MEAS:RES? 50,MAX")
            Delay(0.1)
            nSystErr = ReadMsg(DMM, sMeasString)
            Delay(1)
            nSystErr = WriteMsg(DMM, "MEAS:RES? 50,MAX")
            Delay(0.1)
            nSystErr = ReadMsg(DMM, sMeasString)
            PathImpedance = CDbl(sMeasString) ' use this for all parallel switches only

            If PathImpedance > 10 Then ' invalid path impedance
                Echo("---- Path Impedance too high at " & CStr(PathImpedance) & " Ohms!")
                Echo("---- Setting path impedance default to 3.0 Ohms.")
                PathImpedance = 3
            End If

            'open and test the parallel switch group open
            Select Case PathX
                Case 1 : ChannelNo = ModuleNo & ".1011-1027" ' S301-23 to 56
                Case 2 : ChannelNo = ModuleNo & ".2200,2201" ' S603-1,2(3)
                Case 3 : ChannelNo = ModuleNo & ".2300,2301" ' S604-1,2(3)
                Case 4 : ChannelNo = ModuleNo & ".2400,2401" ' S605-1,2(3)
                Case 5 : ChannelNo = ModuleNo & ".2500,2501" ' S606-1,2(3)
                Case 6 : ChannelNo = ModuleNo & ".4000-4007" ' S501-1 to 3-10 (one half of S501)
                Case 7 : ChannelNo = ModuleNo & ".4100-4107" ' S501-1 to 3-10
                Case 8 : ChannelNo = ModuleNo & ".4200-4207" ' S501-1 to 3-10
                Case 9 : ChannelNo = ModuleNo & ".4300-4307" ' S501-1 to 3-10
                Case 10 : ChannelNo = ModuleNo & ".4400-4407" 'S501-1 to 3-10
            End Select
            nSystErr = WriteMsg(SWITCH1, "OPEN " & ChannelNo)

            ' now test all parallel switches open
            'LFx-03-005, LFx-04-007, LFx-05-001, LFx-06-005, LFx-07-001, LFx-08-003, LFx-09-001, LFx-10-001, LFx-11-001, LFx-12-001
            sTNum = "LF" & ModuleNo & "-" & Format(iTestStep, "00") & "-" & Format(iSubStep, "000")
            iSubStep += 1
            LoopStepNo += 1

LF1_3_5:
            If OpenDetected(ChannelNo, "LF" & ModuleNo) = FAILED Then
                IncStepFailed()
                Switch1Or2Proc = FAILED
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else
                IncStepPassed()
            End If
            Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete
            If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then
                GoTo LF1_3_5
            End If

            'test two wire switches (S501-S510) 2nd half open
            If PathX > 5 Then
                nSystErr = WriteMsg(SWITCH1, "CLOSE " & ChannelNo) ' close all switches connected to pin 1

                Select Case PathX
                    Case 6 : ChannelNo = ModuleNo & ".4010-4017"
                    Case 7 : ChannelNo = ModuleNo & ".4110-4117"
                    Case 8 : ChannelNo = ModuleNo & ".4210-4217"
                    Case 9 : ChannelNo = ModuleNo & ".4310-4317"
                    Case 10 : ChannelNo = ModuleNo & ".4410-4417"
                End Select
                nSystErr = WriteMsg(SWITCH1, "OPEN " & ChannelNo) ' open all switches connected to pin 2
                ' now test all pin 2 switches open
                'LFx-08-004,LFx-09-002,LFx-10-002,LFx-11-002,LFx-12-002
                sTNum = "LF" & ModuleNo & "-" & Format(iTestStep, "00") & "-" & Format(iSubStep, "000")
                iSubStep += 1
                LoopStepNo += 1
LF1_8_1:
                If OpenDetected(ChannelNo, "LF" & ModuleNo) = FAILED Then
                    IncStepFailed()
                    Switch1Or2Proc = FAILED
                    If OptionFaultMode = SOFmode Then GoTo TestComplete
                Else
                    IncStepPassed()
                End If
                Application.DoEvents()
                If AbortTest = True Then GoTo TestComplete
                If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then
                    GoTo LF1_8_1
                End If

                Select Case PathX
                    Case 6 : ChannelNo = ModuleNo & ".4000-4007"
                    Case 7 : ChannelNo = ModuleNo & ".4100-4107"
                    Case 8 : ChannelNo = ModuleNo & ".4200-4207"
                    Case 9 : ChannelNo = ModuleNo & ".4300-4307"
                    Case 10 : ChannelNo = ModuleNo & ".4400-4407"
                End Select
                nSystErr = WriteMsg(SWITCH1, "OPEN " & ChannelNo)
            End If

            ' now test each parallel switch closed one at a time
            LastChannel = 0
            Select Case PathX
                Case 1
                    LastChannel = 17 : MaxImp = 1.5
                    Channels(1) = ModuleNo & ".1011" ' S301-23,24
                    Channels(2) = ModuleNo & ".1012" ' S301-25,26
                    Channels(3) = ModuleNo & ".1013" ' S301-27,28
                    Channels(4) = ModuleNo & ".1014" ' S301-29,30
                    Channels(5) = ModuleNo & ".1015" ' S301-31,32
                    Channels(6) = ModuleNo & ".1016" ' S301-33,34
                    Channels(7) = ModuleNo & ".1017" ' S301-35,36
                    Channels(8) = ModuleNo & ".1018" ' S301-37,38
                    Channels(9) = ModuleNo & ".1019" ' S301-39,40
                    Channels(10) = ModuleNo & ".1020" ' S301-41,42
                    Channels(11) = ModuleNo & ".1021" ' S301-43,44
                    Channels(12) = ModuleNo & ".1022" ' S301-45,46
                    Channels(13) = ModuleNo & ".1023" ' S301-47,48
                    Channels(14) = ModuleNo & ".1024" ' S301-49,50
                    Channels(15) = ModuleNo & ".1025" ' S301-51,52
                    Channels(16) = ModuleNo & ".1026" ' S301-53,54
                    Channels(17) = ModuleNo & ".1027" ' S301-55,56
                Case 2
                    LastChannel = 2 : MaxImp = 1.5
                    Channels(1) = ModuleNo & ".2200" ' S603-1,2
                    Channels(2) = ModuleNo & ".2201" ' S603-1,3
                Case 3
                    LastChannel = 2 : MaxImp = 1.5
                    Channels(1) = ModuleNo & ".2300" ' S604-1,2
                    Channels(2) = ModuleNo & ".2301" ' S604-1,3
                Case 4
                    LastChannel = 2 : MaxImp = 1.5
                    Channels(1) = ModuleNo & ".2400" ' S605-1,2
                    Channels(2) = ModuleNo & ".2401" ' S605-1,3
                Case 5
                    LastChannel = 2 : MaxImp = 1.5
                    Channels(1) = ModuleNo & ".2500" ' S606-1,2
                    Channels(2) = ModuleNo & ".2501" ' S606-1,3
                Case 6
                    LastChannel = 8 : MaxImp = 1.5
                    Channels(1) = ModuleNo & ".4000,4011" ' S501-1,3 and S501-2,4
                    Channels(2) = ModuleNo & ".4001,4010" ' S501-1,4 and S501-2,3
                    Channels(3) = ModuleNo & ".4002,4013" ' S501-1,5 and S501-2,6
                    Channels(4) = ModuleNo & ".4003,4012" ' S501-1,6 and S501-2,5
                    Channels(5) = ModuleNo & ".4004,4015" ' S501-1,7 and S501-2,8
                    Channels(6) = ModuleNo & ".4005,4014" ' S501-1,8 and S501-2,7
                    Channels(7) = ModuleNo & ".4006,4017" ' S501-1,9 and S501-2,10
                    Channels(8) = ModuleNo & ".4007,4016" ' S501-1,10 and S501-2,9
                Case 7
                    LastChannel = 8 : MaxImp = 1.5
                    Channels(1) = ModuleNo & ".4100,4111" ' S502-1,3 and S502-2,4
                    Channels(2) = ModuleNo & ".4101,4110" ' S502-1,4 and S502-2,3
                    Channels(3) = ModuleNo & ".4102,4113" ' S502-1,5 and S502-2,6
                    Channels(4) = ModuleNo & ".4103,4112" ' S502-1,6 and S502-2,5
                    Channels(5) = ModuleNo & ".4104,4115" ' S502-1,7 and S502-2,8
                    Channels(6) = ModuleNo & ".4105,4114" ' S502-1,8 and S502-2,7
                    Channels(7) = ModuleNo & ".4106,4117" ' S502-1,9 and S502-2,10
                    Channels(8) = ModuleNo & ".4107,4116" ' S502-1,10 and S502-2,9
                Case 8
                    LastChannel = 8 : MaxImp = 1.5
                    Channels(1) = ModuleNo & ".4200,4211" ' S503-1,3 and S503-2,4
                    Channels(2) = ModuleNo & ".4201,4210" ' S503-1,4 and S503-2,3
                    Channels(3) = ModuleNo & ".4202,4213" ' S503-1,5 and S503-2,6
                    Channels(4) = ModuleNo & ".4203,4212" ' S503-1,6 and S503-2,5
                    Channels(5) = ModuleNo & ".4204,4215" ' S503-1,7 and S503-2,8
                    Channels(6) = ModuleNo & ".4205,4214" ' S503-1,8 and S503-2,7
                    Channels(7) = ModuleNo & ".4206,4217" ' S503-1,9 and S503-2,10
                    Channels(8) = ModuleNo & ".4207,4216" ' S503-1,10 and S503-2,9
                Case 9
                    LastChannel = 8 : MaxImp = 1.5
                    Channels(1) = ModuleNo & ".4300,4311" ' S504-1,3 and S504-2,4
                    Channels(2) = ModuleNo & ".4301,4310" ' S504-1,4 and S504-2,3
                    Channels(3) = ModuleNo & ".4302,4313" ' S504-1,5 and S504-2,6
                    Channels(4) = ModuleNo & ".4303,4312" ' S504-1,6 and S504-2,5
                    Channels(5) = ModuleNo & ".4304,4315" ' S504-1,7 and S504-2,8
                    Channels(6) = ModuleNo & ".4305,4314" ' S504-1,8 and S504-2,7
                    Channels(7) = ModuleNo & ".4306,4317" ' S504-1,9 and S504-2,10
                    Channels(8) = ModuleNo & ".4307,4316" ' S504-1,10 and S504-2,9
                Case 10
                    LastChannel = 8 : MaxImp = 1.5
                    Channels(1) = ModuleNo & ".4400,4411" ' S505-1,3 and S505-2,4
                    Channels(2) = ModuleNo & ".4401,4410" ' S505-1,4 and S505-2,3
                    Channels(3) = ModuleNo & ".4402,4413" ' S505-1,5 and S505-2,6
                    Channels(4) = ModuleNo & ".4403,4412" ' S505-1,6 and S505-2,5
                    Channels(5) = ModuleNo & ".4404,4415" ' S505-1,7 and S505-2,8
                    Channels(6) = ModuleNo & ".4405,4414" ' S505-1,8 and S505-2,7
                    Channels(7) = ModuleNo & ".4406,4417" ' S505-1,9 and S505-2,10
                    Channels(8) = ModuleNo & ".4407,4416" ' S505-1,10 and S505-2,9
            End Select

            For Channel = 1 To LastChannel ' test every parallel switch closed one at a time
                ChannelNo = Channels(Channel)

                ' test each parallel switch closed
                sTNum = "LF" & ModuleNo & "-" & Format(iTestStep, "00") & "-" & Format(iSubStep, "000")
                iSubStep += 1
                LoopStepNo += 1

LF1_3_6:
                nSystErr = WriteMsg(SWITCH1, "CLOSE " & Channels(Channel)) ' close the channel
                TestSwitch = FnSwitchName(ChannelNo)
                CleanSwitch = Channels(Channel)
                'LFx-03-006-022, LFx-04-008-009, LFx-05-002-003, LFx-06-006-007, LFx-07-002-003,
                'LFx-08-005-012, LFx-09-003-010, LFx-10-003-010, LFx-11-003-010, LFx-12-003-010
                If Not ClosureDetected(TestSwitch, 0, MaxImp + DMM_TOL) Then
                    IncStepFailed()
                    Switch1Or2Proc = FAILED
                    If OptionFaultMode = SOFmode Then GoTo TestComplete
                Else
                    IncStepPassed()
                End If
                ' now open this parallel switch
                nSystErr = WriteMsg(SWITCH1, "OPEN " & Channels(Channel)) ' open one(or two) switch(s)
                Application.DoEvents()
                If AbortTest = True Then GoTo TestComplete
                If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then
                    GoTo LF1_3_6
                End If

            Next Channel

            'setup for next path by opening all of this path switches
            Select Case PathX
                Case 1 : ChannelNo = ModuleNo & ".3200,1011-1027,2000" ' S301 switches
                Case 2 : ChannelNo = ModuleNo & ".3201,2300-2301,2200-2201,2100,2001" ' S603,S604
                Case 3 : ChannelNo = ModuleNo & ".3201,2300-2301,2200-2201,2100,2001" ' S603,S604
                Case 4 : ChannelNo = ModuleNo & ".3202,2500-2501,2400-2401,2101,2001" ' S605,S606
                Case 5 : ChannelNo = ModuleNo & ".3202,2500-2501,2400-2401,2101,2001" ' S605,S606
                Case 6 : ChannelNo = ModuleNo & ".3203,4000-4417,2000" ' S501
                Case 7 : ChannelNo = ModuleNo & ".3203,4000,4100-4117,4011,2000" ' S502
                Case 8 : ChannelNo = ModuleNo & ".3203,4002,4200-4217,4013,2000" ' S503
                Case 9 : ChannelNo = ModuleNo & ".3203,4004,4300-4317,4015,2000" ' S504
                Case 10 : ChannelNo = ModuleNo & ".3203,4006,4400-4417,4017,2000" ' S505
            End Select

            nSystErr = WriteMsg(SWITCH1, "OPEN " & ChannelNo) ' open the whole path
            frmSTest.proProgress.Value = frmSTest.proProgress.Value + 4
            Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete

        Next PathX

        '*************************************************************************
        ' 3rd, test the switches brought out to J1 and connected by the SP1 adapter
        ' LF1 - S301(1-22),   S401,S402
        ' LF2 - S301(97-118), S404,S405

        'LFx-13-001-004, LFx-14-001-004, LFx-15-001-016, LFx-16-001-004
        ' Path 1 - LF1 S401-1,2 to S402-1,2
        ' Path 2 - LF1 S401-1,3 to S402-1,3
        ' Path 3 - LF1 S401-1,4 to S301-1,2(-21,22) to S402-1,3
        ' Path 4 - LF1 S401-1,5 to S402-1,4

        ' 1st, close every switch in the path and test each common switch open/closed
        ' 2nd, open all of the parallel switches, and test for an open path
        ' 3rd, test each parallel switch closed one switch at a time

        For PathX = 1 To 4
            iTestStep = 12 + PathX ' (13 to 16)
            iSubStep = 1

            Select Case PathX
                Case 1 : Channels(1) = ModuleNo & ".3000" : Channels(2) = ModuleNo & ".3100"
                Case 2 : Channels(1) = ModuleNo & ".3001" : Channels(2) = ModuleNo & ".3101"
                Case 3 : Channels(1) = ModuleNo & ".3002" : Channels(2) = ModuleNo & ".3102"
                Case 4 : Channels(1) = ModuleNo & ".3003" : Channels(2) = ModuleNo & ".3103"
            End Select

            ' 1st close every switch in the path
            ' Path 1 S401-1,2 to S402-1,2
            ' Path 2 S401-1,3 to S402-1,3
            ' Path 3 S401-1,4 to S301-1 to 22 to S402-1,3
            ' Path 4 S401-1,5 to S402-1,4
            Select Case PathX
                Case 1 : MaxImp = 8 : PathNo = ModuleNo & ".3000,3100" 'S401-1,2,S402-1,2 or S404-1,2,S406=1,4
                Case 2 : MaxImp = 8 : PathNo = ModuleNo & ".3001,3101" 'S401-1,3,S402-1,3 or S404-1,3,S405-1,3
                Case 3 : MaxImp = 9 : PathNo = ModuleNo & ".3002,3102,1000-1010" 'S401-1,4,S402-1,4,S301-1-22 or S404-1,4,S405-1,4,S301-97-118
                Case 4 : MaxImp = 8 : PathNo = ModuleNo & ".3003,3103" 'S401-1,5,S402-1,5 or S404-1,5,S406=1,5
            End Select
            nSystErr = WriteMsg(SWITCH1, "CLOSE " & PathNo) ' closes the whole path
            Echo(vbCrLf & "---- Test " & FnSwitchName(PathNo))

            For Channel = 1 To 2
                ChannelNo = Channels(Channel)
                TestSwitch = FnSwitchName(ChannelNo)

                ' Test each common switch open
                'LFx-13-001,003, LFx-14-001,003, LFx-15-001,003, LFx-16-001,003
                sTNum = "LF" & ModuleNo & "-" & Format(iTestStep, "00") & "-" & Format(iSubStep, "000")
                iSubStep += 1
                LoopStepNo += 1

LF1_13_1:
                nSystErr = WriteMsg(SWITCH1, "OPEN " & ChannelNo) ' opens one switch
                If OpenDetected(ChannelNo, "LF" & ModuleNo) = FAILED Then
                    IncStepFailed()
                    Switch1Or2Proc = FAILED
                    If OptionFaultMode = SOFmode Then GoTo TestComplete
                Else
                    IncStepPassed()
                End If
                Application.DoEvents()
                If AbortTest = True Then GoTo TestComplete
                If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then
                    GoTo LF1_13_1
                End If

                ' Test each common switch closed
                'LFx-13-002,004, LFx-14-002,004, LFx-15-002,004, LFx-16-002,004
                sTNum = "LF" & ModuleNo & "-" & Format(iTestStep, "00") & "-" & Format(iSubStep, "000")
                iSubStep += 1
                LoopStepNo += 1

LF1_13_2:
                PathImpedance = 0
                CleanSwitch = ChannelNo
                nSystErr = WriteMsg(SWITCH1, "CLOSE " & ChannelNo) ' closes one switch
                If Not ClosureDetected(TestSwitch, 0, MaxImp + DMM_TOL) Then ' test complete path
                    IncStepFailed()
                    Switch1Or2Proc = FAILED
                    If OptionFaultMode = SOFmode Then GoTo TestComplete
                Else
                    IncStepPassed()
                End If
                Application.DoEvents()
                If AbortTest = True Then GoTo TestComplete
                If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then
                    nSystErr = WriteMsg(SWITCH1, "OPEN " & ChannelNo)
                    GoTo LF1_13_2
                End If
            Next Channel

            ' don't open the common switch path again until after each parallel switch has
            '  been tested so that the path impedance doesn't change.

            If PathX = 3 Then ' measure path impedance if parallel switches are tested.
                Delay(0.5)
                nSystErr = WriteMsg(DMM, "MEAS:RES? 50,MAX")
                Delay(0.1)
                nSystErr = ReadMsg(DMM, sMeasString)
                PathImpedance = CDbl(sMeasString)
                If PathImpedance > 10 Then ' invalid path impedance?
                    Echo("---- Path Impedance too high at " & CStr(PathImpedance) & " Ohms!")
                    Echo("---- Setting path impedance default to 3.0 Ohms.")
                    PathImpedance = 3 ' use default of 3.0 ohms for parallel switch tests.
                End If

                '2nd, open and test all of the parallel switches
                'LFx-15-005
                sTNum = "LF" & ModuleNo & "-" & Format(iTestStep, "00") & "-" & Format(iSubStep, "000")
                iSubStep += 1
                LoopStepNo += 1

LF1_15_1:
                ChannelNo = ModuleNo & ".1000-1010"
                nSystErr = WriteMsg(SWITCH1, "OPEN " & ChannelNo) ' open all parallel switches
                If OpenDetected(ChannelNo, "LF" & ModuleNo) = FAILED Then
                    IncStepFailed()
                    Switch1Or2Proc = FAILED
                    If OptionFaultMode = SOFmode Then GoTo TestComplete
                Else
                    IncStepPassed()
                End If
                If AbortTest = True Then GoTo TestComplete
                If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then
                    GoTo LF1_15_1
                End If

                ' 3rd, test parallel switches closed one switch at a time
                ' setup the parallel switch tests
                MaxImp = 3
                Channels(1) = ModuleNo & ".1000" ' S301-1,2
                Channels(2) = ModuleNo & ".1001" ' S301-3,4
                Channels(3) = ModuleNo & ".1002" ' S301-5,6
                Channels(4) = ModuleNo & ".1003" ' S301-7,8
                Channels(5) = ModuleNo & ".1004" ' S301-9,10
                Channels(6) = ModuleNo & ".1005" ' S301-11,12
                Channels(7) = ModuleNo & ".1006" ' S301-13,14
                Channels(8) = ModuleNo & ".1007" ' S301-15,16
                Channels(9) = ModuleNo & ".1008" ' S301-17,18
                Channels(10) = ModuleNo & ".1009" ' S301-19,20
                Channels(11) = ModuleNo & ".1010" ' S301-21,22
                For Channel = 1 To 11
                    ChannelNo = Channels(Channel)

                    'LFx-15-006-016
                    ' now test each parallel switch closed
                    sTNum = "LF" & ModuleNo & "-" & Format(iTestStep, "00") & "-" & Format(iSubStep, "000")
                    iSubStep += 1
                    LoopStepNo += 1

LF1_15_6:
                    nSystErr = WriteMsg(SWITCH1, "CLOSE " & Channels(Channel)) ' close the parallel switch
                    TestSwitch = FnSwitchName(ChannelNo)
                    CleanSwitch = Channels(Channel)

                    If Not ClosureDetected(TestSwitch, 0, MaxImp + DMM_TOL) Then
                        Switch1Or2Proc = FAILED
                        IncStepFailed()
                        If OptionFaultMode = SOFmode Then GoTo TestComplete
                    Else
                        IncStepPassed()
                    End If

                    nSystErr = WriteMsg(SWITCH1, "OPEN " & Channels(Channel)) ' open the parallel switch
                    Application.DoEvents()
                    If AbortTest = True Then GoTo TestComplete
                    If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then
                        GoTo LF1_15_6
                    End If
                Next Channel
            End If

            nSystErr = WriteMsg(SWITCH1, "OPEN " & PathNo) ' open the whole circuit path
            frmSTest.proProgress.Value = frmSTest.proProgress.Value + 2
            Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete

        Next PathX
        frmSTest.proProgress.Value = 100
        Application.DoEvents()


TestComplete:

        frmSTest.cmdAbort.Text = "Abort Test"
        frmSTest.cmdPause.Text = "Pause Test"
        nSystErr = WriteMsg(SWITCH1, "OPEN " & ModuleNo & ".0-4417")    'Make sure that all the relays are open at the end of the Switch 2 Self-test
        nSystErr = WriteMsg(DMM, "*RST") ' init DMM
        nSystErr = WriteMsg(DMM, "*CLS") ' init DMM
        If Switch1Or2Proc = FAILED Then 'If Test Fails, register failure
            RegisterFailure(CStr(InstrumentToTest))
        End If
        Application.DoEvents()
        If AbortTest = True Then
            sMsg = vbCrLf
            sMsg &= "      **********************************************" & vbCrLf
            sMsg &= "      *        LF" & ModuleNo & " Switch test aborted!            *" & vbCrLf
            sMsg &= "      **********************************************" & vbCrLf
            Echo(sMsg)
        End If

    End Function


    Function Switch3Proc() As Integer

        'DESCRIPTION:
        '   This routine tests LF-3  (S201,S202,S701,S751)
        'RETURNS:
        '   0 if the switch being tested passed or 1 if the test fails

        Dim i As Short, X As Integer
        Dim Channels(8) As String
        Dim mModule As Integer
        Dim SError As Integer
        Dim Mux As Integer
        Dim Channel As Integer
        Dim ChannelNo As String
        Dim SwitchGroupX As Integer
        Dim ChannelX As Integer
        Dim sS751 As String = ""
        Dim sMeasString As String = "" 'Measurement Value as a String
        Dim S As String
        Dim MaxImp As Double
        Dim TestSwitch As String = ""
        Dim LoopStepNo As Integer

        Switch3Proc = UNTESTED
        If AbortTest = True Then GoTo TestComplete
        AbortTest = False
        HelpContextID = 1150
        iModAddress = 3
        iTestStep = 1
        iSubStep = 0
        Application.DoEvents()
        PathImpedance = 0
        AbortTest = False
        PowerSwitchTest = False
        LoopStepNo = 0
        frmSTest.proProgress.Maximum = 147

        nSystErr = WriteMsg(SWITCH1, "RESET")
        Delay(0.5)

        If (FirstPass = True) Then
            If (ControlStartTest = 0) And (RunningEndToEnd = False) Then
                X = MsgBox("Is the SP1 adapter installed?", MsgBoxStyle.YesNo)
                If X <> DialogResult.Yes Then
                    S = "Install the SP1 Adapter (93006H6042) onto the SAIF J1 connector. "
                    DisplaySetup(S, "ST-SP1-1.jpg", 1)
                End If

            ElseIf (ControlStartTest = SWITCH3) Then
Step1:          ' install adapter SP1
                S = "Install the SP1 Adapter (93006H6042) onto the SAIF J1 connector. " & vbCrLf
                DisplaySetup(S, "ST-SP1-1.jpg", 1, True, 1, 2)
                If AbortTest = True Then GoTo TestComplete
                If GoBack = True Then GoTo Step1

Step2:          ' install W203 MF cable
                S = "Install cable W203 (93006H6044) as follows:" & vbCrLf
                S &= "1. Connect cable W203-P1 onto the SAIF J2 connector. " & vbCrLf
                S &= "2. Connect cable W203-P2 onto the SAIF front panel MF IN connector via a BNC 50 Ohm Feedthrough Terminator. " & vbCrLf
                S &= "3. Connect cable W203-P3 onto the SAIF front panel MF OUT connector. " & vbCrLf
                DisplaySetup(S, "ST-W203-1.jpg", 3, True, 2, 2)
                If AbortTest = True Then GoTo TestComplete
                If GoBack = True Then GoTo Step1

            End If
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        Switch3Proc = PASSED

        frmSTest.proProgress.Value = 4
        Application.DoEvents()
        Application.DoEvents()
        Application.DoEvents()

        EchoTitle("Low Frequency Switching #3 Test", False)
        frmSTest.sbrUserInformation.Text = "Testing " & InstrumentDescription(SWITCH3)
        Application.DoEvents()
        EnableClean = True
        CleanSwitch = ""


        nSystErr = WriteMsg(SWITCH1, "CNF ON") ' enable the confidence test on

        '1260-38 Switching Module Close BIT Test
        LoopStepNo = 1
        nSystErr = WriteMsg(SWITCH1, "RESET")
LF3_1_1: sTNum = "LF3-01-001"
        SError = SwitchingError(mModule) ' clear error buffer
        nSystErr = WriteMsg(SWITCH1, "CLOSE 3.0000-7002") ' close all module 3 switches
        SError = SwitchingError(mModule)
        If (SError <> 0) And (mModule = 3) Then
            Echo(FormatResults(False, "1260-38 Switching Module Close BIT", sTNum))
            Echo("  Close BIT error<" & SError & "> from module " & CStr(mModule))
            Echo("  This is a possible DIP switch error on one of the switch cards in the ATS.")
            Echo("  If possible, try swapping switch cards to identify the bad card, and ")
            Echo("    then try toggling each DIP switch on that card.")
            RegisterFailure(CStr(SWITCH3), sTNum, , , , , "1260-38 Switching Module Close BIT Test error<" & SError & "> in module 3")
            Switch3Proc = FAILED
            FirstSwitchTested = False
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
            FormatResultLine(sTNum & " 1260-38 Switching Module Close BIT", True)
        End If
        ' frmSTest.InstrumentLabel(SWITCH3).ForeColor = Color.Black' causes exception
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "LF3 Switch" And OptionStep = LoopStepNo Then
            GoTo LF3_1_1
        End If


        frmSTest.proProgress.Value += 1
        LoopStepNo += 1


        '1260-38 Switching Module Open BIT Test
LF3_1_2: sTNum = "LF3-01-002"
        SError = SwitchingError(mModule) ' clear error buffer
        nSystErr = WriteMsg(SWITCH1, "OPEN 3.0000-7002") ' open all module 3 switches
        SError = SwitchingError(mModule)
        Application.DoEvents() : Application.DoEvents()
        If SError <> 0 Then
            Echo(FormatResults(False, "1260-38 Switching Module Open BIT", sTNum))
            Echo("  Open BIT error<" & SError & "> from module " & CStr(mModule))
            Echo("  This is a possible DIP switch error on one of the switch cards in the ATS.")
            Echo("  If possible, try swapping switch cards to identify the bad card, and ")
            Echo("    then try toggling each DIP switch on that card.")
            RegisterFailure(CStr(SWITCH3), sTNum, , , , , "1260-38 Switching Module Open BIT Test error<" & SError & "> in module 3")
            Switch3Proc = FAILED
            IncStepFailed()
            FirstSwitchTested = False
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
            FormatResultLine(sTNum & " 1260-38 Switching Module Open BIT", True)
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "LF3 Switch" And OptionStep = LoopStepNo Then
            GoTo LF3_1_2
        End If
        frmSTest.proProgress.Value += 1


        nSystErr = WriteMsg(SWITCH1, "CNF OFF") ' disable the confidence test

        'Close interconnect relays             'Connect:
        nSystErr = WriteMsg(SWITCH1, "CLOSE 3.1001") 'Mux1  to Mux3  (S701)
        nSystErr = WriteMsg(SWITCH1, "CLOSE 3.2002") 'Mux2  to Mux3  (S701)
        nSystErr = WriteMsg(SWITCH1, "CLOSE 3.3000,3001") 'Mux4  to Mux6  (S201) and Mux5  to Mux7  (S201)
        nSystErr = WriteMsg(SWITCH1, "CLOSE 3.4000,4001") 'Mux6  to Mux8  (S201) and Mux7  to Mux9  (S201)
        nSystErr = WriteMsg(SWITCH1, "CLOSE 3.6000,6001") 'Mux10 to Mux12 (S202) and Mux11 to Mux13 (S202)
        nSystErr = WriteMsg(SWITCH1, "CLOSE 3.7000,7001") 'Mux12 to Mux14 (S202) and Mux13 to Mux15 (S202)


        Channels(0) = "3.0040-0047,0060-0067,0080-0087" 's201-1,2(5-52)
        Channels(1) = "3.0050-0057,0070-0077,0090-0097" 's201-3,4(5-52)
        Channels(2) = ""
        Channels(3) = "3.0110-0117,0130-0137,0150-0157" 's202-3,4(5-52)
        Channels(4) = "3.0100-0107,0120-0127,0140-0147" 's202-1,2(5-52)
        Channels(5) = "3.0010-0037" 's701-1,2(3-50)
        Channels(6) = ""
        Channels(7) = ""

        'Testing S751. Close all switches connected to it
        Echo(vbCrLf & "---- Test S751-1,2(3,4 to 17,18)")


        iTestStep = 2
        iSubStep = 1

        'get path impedance for S751 (added Jan 7, 2008)
        nSystErr = WriteMsg(SWITCH1, "CLOSE 3.0000-0157") ' close all LF3
        Delay(0.5) 'Allow time for switch closure and settling
        nSystErr = WriteMsg(DMM, "MEAS:RES? 50,MAX")
        Delay(0.1)
        nSystErr = ReadMsg(DMM, sMeasString)
        PathImpedance = CDbl(sMeasString)
        If PathImpedance > 5 Then ' invalid path impedance
            Echo("---- Path Impedance too high at " & CStr(PathImpedance) & " Ohms!")
            Echo("---- Setting default path impedance to 3.0 Ohms.")
            PathImpedance = 3
        End If
        nSystErr = WriteMsg(SWITCH1, "OPEN 3.0000-0157")

        MaxImp = 1.5
        For Channel = 0 To 7
            If Channels(Channel) <> "" Then
                nSystErr = WriteMsg(SWITCH1, "CLOSE " & Channels(Channel))
            End If

            sS751 = "3.000" & Format(Channel, "0") ' 3.0000 to 3.0007

            'LF3-02-001 ~ LF3-02-015 (Odd)
            sTNum = "LF3-" & Format(iTestStep, "00") & "-" & Format(iSubStep, "000")
            iSubStep += 1
            LoopStepNo += 1

            TestSwitch = FnSwitchName(sS751)
            CleanSwitch = sS751

LF3_2_1:
            nSystErr = WriteMsg(SWITCH1, "CLOSE " & sS751)
            '   PathImpedance = 0
            If Not ClosureDetected(TestSwitch, 0, MaxImp + DMM_TOL) Then
                Switch3Proc = FAILED
                IncStepFailed()
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else
                IncStepPassed()
            End If
            Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete
            If OptionMode = LOSmode And OptionTestName = "LF3 Switch" And OptionStep = LoopStepNo Then
                nSystErr = WriteMsg(SWITCH1, "OPEN " & sS751)
                GoTo LF3_2_1
            End If
            frmSTest.proProgress.Value += 1

            'LF3-02-002 ~ LF3-02-016 (Even)
            sTNum = "LF3-" & Format(iTestStep, "00") & "-" & Format(iSubStep, "000")
            iSubStep += 1
            LoopStepNo += 1

LF3_2_2:
            nSystErr = WriteMsg(SWITCH1, "OPEN " & sS751)
            Application.DoEvents() : Application.DoEvents()
            If OpenDetected(sS751, "LF3") = FAILED Then
                Switch3Proc = FAILED
                IncStepFailed()
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else
                IncStepPassed()
            End If
            If Channels(Channel) <> "" Then
                nSystErr = WriteMsg(SWITCH1, "OPEN " & Channels(Channel))
            End If
            Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete
            If OptionMode = LOSmode And OptionTestName = "LF3 Switch" And OptionStep = LoopStepNo Then
                GoTo LF3_2_2
            End If

            frmSTest.proProgress.Value += 1
        Next Channel

        ' now test S701(mux1-3), S201(mux4-9), S202(mux 10-15)
        ' Mux 0     S751
        ' Mux 1-3   S701
        ' Mux 4-9   S201
        ' Mux 10-15 S202
        For SwitchGroupX = 1 To 5
            iTestStep = SwitchGroupX + 2 ' (3 to 7)
            iSubStep = 1
            'Testing:
            '   S701-1,2    LF3-03-001 to LF3-03-025
            '   S201-1,2    LF3-04-001 to LF3-04-025
            '   S201-3,4    LF3-05-001 to LF3-05-025
            '   S202-1,2    LF3-06-001 to LF3-06-025
            '   S201-3,4    LF3-07-001 to LF3-07-025

            'Get Path impedance for this switch group
            'Close one S751 switch and 24 parallel switches and measure the
            ' path resistance.  Then open the 24 parallel switches and test each
            ' switch, one at a time (subtracting the path impedance).
            Select Case SwitchGroupX
                Case 1
                    ChannelX = 5 'S751-1,2(13,14), Channel 5 for Mux 1, 2, 3 (s701)
                    sS751 = "3.0005" : TestSwitch = "S701-1,2(3,4 to 49,50)"
                Case 2
                    ChannelX = 0 'S751-1,2(3,4),   Channel 0 for Mux 4, 6, 8 (s201-1,2)
                    sS751 = "3.0000" : TestSwitch = "S201-1,2(5,6 to 51,52)"
                Case 3
                    ChannelX = 1 'S751-1,2(5,6),   Channel 1 for Mux 5, 7, 9 (s201-3,4)
                    sS751 = "3.0001" : TestSwitch = "S201-3,4(5,6 to 51,52)"
                Case 4
                    ChannelX = 4 'S751-1,2(11,12), Channel 4 for Mux 10, 12, 14 (s202-1,2)
                    sS751 = "3.0004" : TestSwitch = "S202-1,2(5,6 to 51,52)"
                Case 5
                    ChannelX = 3 'S751-1,2(9,10),  Channel 3 for Mux 11, 13, 15 (s202-3,4)
                    sS751 = "3.0003" : TestSwitch = "S202-3,4(5,6 to 51,52)"

            End Select
            Echo(vbCrLf & "---- Test " & TestSwitch)
            nSystErr = WriteMsg(SWITCH1, "CLOSE " & sS751) ' do not open this switch until all 24 switches are tested
            nSystErr = WriteMsg(SWITCH1, "CLOSE " & Channels(ChannelX)) 'close parallel switches

            ' measure pathimpedance
            Delay(0.5) 'Allow time for switch closure and settling
            nSystErr = WriteMsg(DMM, "MEAS:RES? 50,MAX")
            Delay(0.1)
            nSystErr = ReadMsg(DMM, sMeasString)
            PathImpedance = CDbl(sMeasString)
            If PathImpedance > 10 Then ' invalid path impedance
                Echo("---- Path Impedance too high at " & CStr(PathImpedance) & " Ohms!")
                Echo("---- Setting default path impedance to 3.0 Ohms.")
                PathImpedance = 3
            End If

            ' Test switch group open
            sTNum = "LF3-" & Format(iTestStep, "00") & "-001"
            iSubStep += 1
            LoopStepNo += 1

LF3_3_1:
            nSystErr = WriteMsg(SWITCH1, "OPEN " & Channels(ChannelX)) ' open parallel switches, test for open once
            'LF3-xx-001
            If OpenDetected(Channels(ChannelX), "LF3") = FAILED Then
                Switch3Proc = FAILED
                IncStepFailed()
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else
                IncStepPassed()
            End If
            Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete
            If OptionMode = LOSmode And OptionTestName = "LF3 Switch" And OptionStep = LoopStepNo Then
                GoTo LF3_3_1
            End If
            frmSTest.proProgress.Value += 1

            For i = 1 To 3
                Select Case SwitchGroupX
                    Case 1 : Mux = i
                    Case 2 : Mux = (i + 1) * 2
                    Case 3 : Mux = (i + 1) * 2 + 1
                    Case 4 : Mux = (i + 4) * 2
                    Case 5 : Mux = (i + 4) * 2 + 1
                End Select

                For Channel = 0 To 7 'Each Mux has 8 channels
                    ChannelNo = "3.0" & Format(Mux, "00") & Format(Channel, "0")
                    sTNum = "LF3-" & Format(iTestStep, "00") & "-" & Format(iSubStep, "000")
                    iSubStep += 1 ' 3,4,5,6,7,8,9,10
                    TestSwitch = FnSwitchName(ChannelNo)
                    LoopStepNo += 1

LF3_3_2:
                    nSystErr = WriteMsg(SWITCH1, "CLOSE " & ChannelNo)
                    CleanSwitch = ChannelNo
                    'LF3-03-002 to 025; LF3-04-002 to 025,,,
                    If Not ClosureDetected(TestSwitch, 0, MaxImp + DMM_TOL) Then
                        Switch3Proc = FAILED
                        IncStepFailed()
                        If OptionFaultMode = SOFmode Then GoTo TestComplete
                    Else
                        IncStepPassed()
                    End If

                    nSystErr = WriteMsg(SWITCH1, "OPEN " & ChannelNo)
                    Application.DoEvents()
                    If AbortTest = True Then GoTo TestComplete
                    If OptionMode = LOSmode And OptionTestName = "LF3 Switch" And OptionStep = LoopStepNo Then
                        GoTo LF3_3_2
                    End If

                    frmSTest.proProgress.Value += 1
                Next Channel
            Next i
            nSystErr = WriteMsg(SWITCH1, "OPEN " & sS751)
        Next SwitchGroupX
        Application.DoEvents()

TestComplete:
        frmSTest.proProgress.Value = 100 ' done
        frmSTest.proProgress.Maximum = 100
        frmSTest.cmdAbort.Text = "Abort Test"
        frmSTest.cmdPause.Text = "Pause Test"
        nSystErr = WriteMsg(SWITCH1, "RESET")
        nSystErr = WriteMsg(SWITCH1, "OPEN 3.1000,1002,2000,2001,3002,4002,5000,5001,5002,6002,7002,8002")
        nSystErr = WriteMsg(SWITCH1, "CLOSE 3.1001,2002,3000,3001,4000,4001,6000,6001,7000,7001")
        nSystErr = WriteMsg(DMM, "*RST") ' init DMM
        nSystErr = WriteMsg(DMM, "*CLS")
        If Switch3Proc = FAILED Then 'fail
            RegisterFailure(SWITCH3)
        End If
        Application.DoEvents()

        If AbortTest = True Then
            sMsg = vbCrLf
            sMsg &= "      **********************************************" & vbCrLf
            sMsg &= "      *            LF3 Switch test aborted!        *" & vbCrLf
            sMsg &= "      **********************************************" & vbCrLf
            Echo(sMsg)
        End If

    End Function

    Function Switch4Proc() As Integer
        'DESCRIPTION:
        '   This routine tests the medium frequency switches (S801-S804)
        'RETURNS:
        '   PASSED if the switch being tested passed or
        '   FAILED if the test fails

        Dim X As Integer
        Dim Channel As Integer = 0
        Dim ChanChar As String = ""
        Dim mModule As Integer = 0
        Dim SError As Integer = 0
        Dim PairNo As Integer = 0
        Dim ChannelNo1 As String = ""
        Dim ChannelNo2 As String = ""
        Dim sSwitchCallout As String = "" 'Describes the switches failed for the Fault Callout
        Dim S As String = ""
        Dim MaxImp As Single = 0
        Dim LoopStepNo As Integer = 0

        frmSTest.proProgress.Maximum = 100
        frmSTest.proProgress.Value = 0
        Switch4Proc = UNTESTED
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        AbortTest = False
        PathImpedance = 0
        EnableClean = True
        HelpContextID = 1160
        EchoTitle("Medium Frequency Switching Test", False)
        iModAddress = 4
        LoopStepNo = 0
        PowerSwitchTest = False
        Application.DoEvents()
        frmSTest.sbrUserInformation.Text = "Testing " & InstrumentDescription(SWITCH4)
        nSystErr = WriteMsg(SWITCH1, "RESET")
        If (RunningEndToEnd = False) And (FirstPass = True) Then
            X = MsgBox("Are adapter SP1 and cable W203 installed?", MsgBoxStyle.YesNo)
            If X <> DialogResult.Yes Then
Step1:          ' install adapter SP1
                S = "Install the SP1 Adapter (93006H6042) onto the SAIF J1 connector. " & vbCrLf
                DisplaySetup(S, "ST-SP1-1.jpg", 1, True, 1, 2)
                If AbortTest = True Then GoTo TestComplete

Step2:          ' install W203 MF cable
                S = "Install cable W203 (93006H6044) as follows:" & vbCrLf
                S &= "1. Connect cable W203-P1 onto the SAIF J2 connector. " & vbCrLf
                S &= "2. Connect cable W203-P2 onto the SAIF front panel MF IN connector via a BNC 50 Ohm Feedthrough Terminator. " & vbCrLf
                S &= "3. Connect cable W203-P3 onto the SAIF front panel MF OUT connector. " & vbCrLf
                DisplaySetup(S, "ST-W203-1.jpg", 3, True, 2, 2)
                If AbortTest = True Then GoTo TestComplete
                If GoBack = True Then GoTo Step1
            End If
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        Switch4Proc = PASSED


        nSystErr = WriteMsg(SWITCH1, "RESET")
        Delay(0.5)
        Echo(vbCrLf & "---- Test S801-S804 Error Status")

        nSystErr = WriteMsg(SWITCH1, "CNF ON") ' enable the confidence test on


        'MFS-01-001 ~ MFS-01-018  Odd = Close BIT
        iTestStep = 1
        iSubStep = 1
        For Channel = 0 To 8
            ChanChar = CStr(Channel)
            '0 Close 4.00,10,20,30 (S801-1,2, S802-1,2, S803-1,2, S804-1,2)
            '1 Close 4.01,11,21,30 (S801-1,3, S802-1,3, S803-1,3, S804-1,3)
            '2 Close 4.02,12,22,32 (S801-1,4, S802-1,4, S803-1,4, S804-1,4)
            '3 Close 4.03,13,23,33 (S801-1,5, S802-1,5, S803-1,5, S804-1,5)
            '4 Close 4.04,14,24,34 (S801-1,6, S802-1,6, S803-1,6, S804-1,6)
            '5 Close 4.05,15,25,35 (S801-1,7, S802-1,7, S803-1,7, S804-1,7)
            '6 Close 4.06,16,26,36 (S801-1,8, S802-1,8, S803-1,8, S804-1,8)
            '7 Close 4.07,17,27,37 (S801-1,9, S802-1,9, S803-1,9, S804-1,9)

            '8 Close 4.08,18,28,38 (S801-nc, S802-nc, S803-nc, S804-nc)

            sTNum = "MFS-01-" & Format(iSubStep, "000")
            iSubStep += 1
            LoopStepNo += 1

MF_1_1:
            SError = SwitchingError(mModule) ' clear error buffer
            nSystErr = WriteMsg(SWITCH1, "CLOSE 4.0" & ChanChar & ",1" & ChanChar & ",2" & ChanChar & ",3" & ChanChar)
            SError = SwitchingError(mModule)
            If (SError <> 0) Then
                Echo(FormatResults(False, "1260-58 Switching Module Close BIT" & CStr(Channel), sTNum))
                Echo("  Close Test error<" & SError & "> from module " & CStr(mModule))
                Echo("  This is a possible DIP switch error on one of the switch cards in the ATS.")
                Echo("  If possible, try swapping switch cards to identify the bad card, and ")
                Echo("    then try toggling each DIP switch on that card.")
                RegisterFailure(CStr(SWITCH4), sTNum, , , , , "1260-58 Switching Module Close BIT Test error<" & SError & "> in module 4")
                Switch4Proc = FAILED
                IncStepFailed()
                FirstSwitchTested = False
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else
                IncStepPassed()
                Echo(FormatResults(True, "1260-58 Switching Module Close BIT" & CStr(Channel), sTNum))
            End If
            Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete
            If OptionMode = LOSmode And OptionTestName = "MF Switch" And OptionStep = LoopStepNo Then
                GoTo MF_1_1
            End If

            'MFS-01-002 ~ MFS-01-018  Even = Open BIT
            '0 Open 4.00,10,20,30 (S801-1,2, S802-1,2, S803-1,2, S804-1,2)
            '1 Open 4.01,11,21,30 (S801-1,3, S802-1,3, S803-1,3, S804-1,3)
            '2 Open 4.02,12,22,32 (S801-1,4, S802-1,4, S803-1,4, S804-1,4)
            '3 Open 4.03,13,23,33 (S801-1,5, S802-1,5, S803-1,5, S804-1,5)
            '4 Open 4.04,14,24,34 (S801-1,6, S802-1,6, S803-1,6, S804-1,6)
            '5 Open 4.05,15,25,35 (S801-1,7, S802-1,7, S803-1,7, S804-1,7)
            '6 Open 4.06,16,26,36 (S801-1,8, S802-1,8, S803-1,8, S804-1,8)
            '7 Open 4.07,17,27,37 (S801-1,9, S802-1,9, S803-1,9, S804-1,9)
            '8 Open 4.08,18,28,38 (S801-nc, S802-nc, S803-nc, S804-nc)
            sTNum = "MFS-01-" & Format(iSubStep, "000")
            iSubStep += 1
            LoopStepNo += 1

MF_1_2:
            SError = SwitchingError(mModule) ' clear error buffer
            nSystErr = WriteMsg(SWITCH1, "OPEN 4.0" & ChanChar & ",1" & ChanChar & ",2" & ChanChar & ",3" & ChanChar)
            SError = SwitchingError(mModule)
            If (SError <> 0) Then
                Echo(FormatResults(False, "1260-58 Switching Module Open BIT" & CStr(Channel), sTNum))
                Echo("  Open BIT error<" & SError & "> from module " & CStr(mModule))
                Echo("  This is a possible DIP switch error on one of the switch cards in the ATS.")
                Echo("  If possible, try swapping switch cards to identify the bad card, and ")
                Echo("    then try toggling each DIP switch on that card.")
                RegisterFailure(CStr(SWITCH4), sTNum, , , , , "1260-58 Switching Module Open BIT Test error<" & SError & "> in module 4")
                Switch4Proc = FAILED
                IncStepFailed()
                FirstSwitchTested = False
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else
                IncStepPassed()
                Echo(FormatResults(True, "1260-58 Switching Module Open BIT" & CStr(Channel), sTNum))
            End If
            Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete
            If OptionMode = LOSmode And OptionTestName = "MF Switch" And OptionStep = LoopStepNo Then
                GoTo MF_1_2
            End If
            frmSTest.proProgress.Value = frmSTest.proProgress.Value + 1

        Next Channel

        nSystErr = WriteMsg(SWITCH1, "CNF OFF") ' disable the confidence test

        ' passed all previous tests
        '    Echo FormatResults(True, "1260-58 Switching Module Close/Open BIT", "MFS-01-001 to 018")

        ' setup DMM for impedance test
        nSystErr = WriteMsg(DMM, "*RST")
        nSystErr = WriteMsg(DMM, "*CLS")
        Delay(0.1)
        nSystErr = WriteMsg(DMM, "SENS:RES:APER MIN")
        Delay(0.1)

        For PairNo = 0 To 1
            If PairNo = 0 Then
                Echo(vbCrLf & "---- Test S801,S802")
            Else
                Echo(vbCrLf & "---- Test S803,S804")
            End If
            iTestStep = PairNo + 2
            iSubStep = 1
            MaxImp = 1.5
            For Channel = 0 To 7
                'MFS-02-001, 004, 007, 010, 013, 016, 019, 021
                'MFS-03-001, 004, 007, 010, 013, 016, 019, 022
                If PairNo = 0 Then
                    ChannelNo1 = "4.0" & Channel ' S801-1,2 to S801-1,9
                    ChannelNo2 = "4.1" & Channel ' S802-1,2 to S802-1,9
                    CleanSwitch = ChannelNo1 & ",1" & CStr(Channel)
                Else
                    ChannelNo1 = "4.2" & Channel ' S803-1,2 to S803-1,9
                    ChannelNo2 = "4.3" & Channel ' S804-1,2 to S804-1,9
                    CleanSwitch = ChannelNo1 & ",3" & CStr(Channel)
                End If

                TestSwitch = FnSwitchName(ChannelNo1) & " and " & FnSwitchName(ChannelNo2)
                sSwitchCallout = "Failed Switch Connections:  " & TestSwitch

                sTNum = "MFS-0" & Format(PairNo + 2, "0") & "-" & Format(iSubStep, "000")
                iSubStep += 1
                LoopStepNo += 1

MF_2_1:
                nSystErr = WriteMsg(SWITCH1, "CLOSE " & ChannelNo1)
                nSystErr = WriteMsg(SWITCH1, "CLOSE " & ChannelNo2)
                If Not ClosureDetected(TestSwitch, 45 + MaxImp - DMM_TOL, 53 + MaxImp + DMM_TOL) Then
                    Switch4Proc = FAILED
                    IncStepFailed()
                    If OptionFaultMode = SOFmode Then GoTo TestComplete
                Else
                    IncStepPassed()
                End If
                Application.DoEvents()
                If AbortTest = True Then GoTo TestComplete
                If OptionMode = LOSmode And OptionTestName = "MF Switch" And OptionStep = LoopStepNo Then
                    nSystErr = WriteMsg(SWITCH1, "OPEN " & ChannelNo1)
                    nSystErr = WriteMsg(SWITCH1, "OPEN " & ChannelNo2)
                    GoTo MF_2_1
                End If

                'MFS-02-002, 005, 008, 011, 014, 017, 020, 022
                'MFS-03-002, 005, 008, 011, 014, 017, 020, 023
                sTNum = "MFS-0" & Format(PairNo + 2, "0") & "-" & Format(iSubStep, "000")
                iSubStep += 1
                LoopStepNo += 1
                TestSwitch = FnSwitchName(ChannelNo1)

MF_2_2:
                'Open closed switches before the next test
                nSystErr = WriteMsg(SWITCH1, "OPEN " & ChannelNo1)
                If OpenDetected(ChannelNo1, "MFS") = FAILED Then
                    Switch4Proc = FAILED
                    IncStepFailed()
                    If OptionFaultMode = SOFmode Then GoTo TestComplete
                Else
                    IncStepPassed()
                End If
                Application.DoEvents()
                If AbortTest = True Then GoTo TestComplete
                If OptionMode = LOSmode And OptionTestName = "MF Switch" And OptionStep = LoopStepNo Then
                    GoTo MF_2_2
                End If

                'MFS-02-003, 006, 009, 012, 015, 018, 021, 023
                'MFS-03-003, 006, 009, 012, 015, 018, 021, 024

                sTNum = "MFS-0" & Format(PairNo + 2, "0") & "-" & Format(iSubStep, "000")
                iSubStep += 1
                LoopStepNo += 1
                TestSwitch = FnSwitchName(ChannelNo2)
                nSystErr = WriteMsg(SWITCH1, "CLOSE " & ChannelNo1)

MF_2_3:
                nSystErr = WriteMsg(SWITCH1, "OPEN " & ChannelNo2)
                If OpenDetected(ChannelNo2, "MFS") = FAILED Then
                    Switch4Proc = FAILED
                    IncStepFailed()
                    If OptionFaultMode = SOFmode Then GoTo TestComplete
                Else
                    IncStepPassed()
                End If
                Application.DoEvents()
                If AbortTest = True Then GoTo TestComplete
                If OptionMode = LOSmode And OptionTestName = "MF Switch" And OptionStep = LoopStepNo Then
                    GoTo MF_2_3
                End If

                nSystErr = WriteMsg(SWITCH1, "OPEN " & ChannelNo1)
                frmSTest.proProgress.Value = frmSTest.proProgress.Value + 5
                Application.DoEvents()
                Application.DoEvents()
                Application.DoEvents()
            Next Channel
            Echo("")
        Next PairNo

        frmSTest.proProgress.Value = 100
        Application.DoEvents()

TestComplete:
        frmSTest.cmdAbort.Text = "Abort Test"
        frmSTest.cmdPause.Text = "Pause Test"
        nSystErr = WriteMsg(SWITCH1, "OPEN 4.00-37")
        nSystErr = WriteMsg(DMM, "*RST") ' init DMM
        nSystErr = WriteMsg(DMM, "*CLS")
        If Switch4Proc = FAILED Then 'If Test Fails, register failure
            RegisterFailure(CStr(SWITCH4), , , , , , "MF Switch Module Failed")
        End If
        Application.DoEvents()
        If AbortTest = True Then
            sMsg = vbCrLf
            sMsg &= "      **********************************************" & vbCrLf
            sMsg &= "      *        MF  Switch test aborted!            *" & vbCrLf
            sMsg &= "      **********************************************" & vbCrLf
            Echo(sMsg)
        End If

    End Function

    Function Switch5Proc() As Short

        'DESCRIPTION:
        '   This routine tests the high frequency switches (S901-S906)
        'RETURNS:
        '   PASSED or FAILED if the test passes/fails

        Dim Channel As Integer
        Dim ChanChar As String
        Dim mModule As Integer
        Dim SError As Integer
        Dim PairNo As Integer
        Dim Mux1 As Integer
        Dim Mux2 As Integer
        Dim ChannelNo1 As String
        Dim ChannelNo2 As String
        Dim S As String
        Dim PathImpX As Single
        Dim LoopStepNo As Integer

        Switch5Proc = UNTESTED
        PathImpedance = 0
        EnableClean = True
        iModAddress = 5
        iTestStep = 1
        iSubStep = 1
        LoopStepNo = 0
        PowerSwitchTest = False
        Application.DoEvents()
        HelpContextID = 1170
        EchoTitle("High Frequency Switching Test", False)
        frmSTest.sbrUserInformation.Text = "Testing " & InstrumentDescription(SWITCH5)
        If AbortTest = True Then GoTo TestComplete
        AbortTest = False

        nSystErr = WriteMsg(SWITCH1, "RESET")
        Delay(0.5)

        If RunningEndToEnd = False And FirstPass = True Then
Step1:
            S = "Install Adapter SP1-P1 onto the SAIF J1 connector. " & vbCrLf
            DisplaySetup(S, "ST-SP1-1.jpg", 1, True, 1, 2)
            If AbortTest = True Then GoTo TestComplete

Step2:
            S = "Setup for High Frequency switches:" & vbCrLf
            S &= "1. DO NOT torque J3 and J4 connections (hand tighten only)." & vbCrLf
            S &= "2. Use cable W207 (SMA to N) to connect SAIF J3-1   to   SAIF front panel HF IN. " & vbCrLf
            S &= "3. Use cable W202 (#1) to connect SAIF J3-2   to   SAIF J4-2. " & vbCrLf
            S &= "4. Use cable W202 (#1) to connect SAIF J3-3   to   SAIF J4-3. " & vbCrLf
            S &= "5. Use cable W202 (#1) to connect SAIF J3-4   to   SAIF J4-4. " & vbCrLf
            S &= "6. Use cable W202 (#2) to connect SAIF J3-5   to   SAIF J4-5. " & vbCrLf
            S &= "7. Use cable W202 (#2) to connect SAIF J3-6   to   SAIF J4-6. " & vbCrLf
            S &= "8. Use cable W202 (#2) to connect SAIF J3-7   to   SAIF J4-7. " & vbCrLf
            S &= "9. Use a SMA to N adapter to connect SAIF J4-1 to 50 Ohm terminator. " & vbCrLf
            S &= "10. Use cable W21 (Dual Banana Jack to N) to connect SAIF DMM-INPUT HI/LO   to   SAIF front panel HF OUT." & vbCrLf
            DisplaySetup(S, "ST-W202-1.jpg", 10, True, 2, 2)
            If GoBack = True Then GoTo Step1

        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        Switch5Proc = PASSED

        'HFS-01-001 ~ HFS-01-012  Odd = Close BIT
        iTestStep = 1
        iSubStep = 1
        LoopStepNo = 1
        Echo(vbCrLf & "---- Test S901-S906 Error Status")
        For Channel = 0 To 5
            Application.DoEvents()
            ChanChar = Channel
            '0 Close 5.00,10,20,30,40,50 (S901-1,2, S902-1,2, S903-1,2, S904-1,2, S905-1,2, S906-1,2)
            '1 Close 5.01,11,21,31,41,51 (S901-1,3, S902-1,3, S903-1,3, S904-1,3, S905-1,3, S906-1,3)
            '2 Close 5.02,12,22,32,42,52 (S901-1,4, S902-1,4, S903-1,4, S904-1,4, S905-1,4, S906-1,4)
            '3 Close 5.03,13,23,33,43,53 (S901-1,5, S902-1,5, S903-1,5, S904-1,5, S905-1,5, S906-1,5)
            '4 Close 5.04,14,24,34,44,54 (S901-1,6, S902-1,6, S903-1,6, S904-1,6, S905-1,6, S906-1,6)
            '5 Close 5.05,15,25,35,45,55 (S901-1,7, S902-1,7, S903-1,7, S904-1,7, S905-1,7, S906-1,7)

HF_1_1:
            nSystErr = WriteMsg(SWITCH1, "CLOSE 5.0" & ChanChar & ",1" & ChanChar & ",2" & ChanChar & ",3" & ChanChar & ",4" & ChanChar & ",5" & ChanChar)
            SError = SwitchingError(mModule)
            sTNum = "HFS-01-" & Format(iSubStep, "000")
            If SError <> 0 Then
                Echo(FormatResults(False, "1260-66 Switching Module Close BIT" & CStr(Channel), sTNum))
                Echo("  Close Test error<" & SError & "> in module 5")
                RegisterFailure(SWITCH5, sTNum, , , , , "1260-66 Switching Module Close BIT Test error<" & SError & "> in module 5")
                Switch5Proc = FAILED
                IncStepFailed()
                FirstSwitchTested = False
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else
                IncStepPassed()
                Echo(FormatResults(True, "1260-66 Switching Module Close BIT" & CStr(Channel), sTNum))
            End If
            Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete
            If OptionMode = LOSmode And OptionTestName = "HF Switch" And OptionStep = LoopStepNo Then
                GoTo HF_1_1
            End If

            iSubStep += 1
            LoopStepNo += 1

            'HFS-01-002 ~ HFS-01-012  Even = Open BIT
            '0 Open 5.00,10,20,30,40,50 (S901-1,2 to S906-1,2)
            '1 Open 5.01,11,21,31,41,51 (S901-1,3 to S906-1,3)
            '2 Open 5.02,12,22,32,42,52 (S901-1,4 to S906-1,4)
            '3 Open 5.03,13,23,33,43,53 (S901-1,5 to S906-1,5)
            '4 Open 5.04,14,24,34,44,54 (S901-1,6 to S906-1,6)
            '5 Open 5.05,15,25,35,45,55 (S901-1,7 to S906-1,7)
HF_1_2:
            nSystErr = WriteMsg(SWITCH1, "OPEN 5.0" & ChanChar & ",1" & ChanChar & ",2" & ChanChar & ",3" & ChanChar & ",4" & ChanChar & ",5" & ChanChar)
            SError = SwitchingError(mModule)
            sTNum = "HFS-01-" & Format(iSubStep, "000")
            If SError <> 0 Then
                Echo(FormatResults(False, "1260-66 Switching Module Open BIT" & CStr(Channel), sTNum))
                Echo("  Open Test error<" & SError & "> in module 5")
                RegisterFailure(SWITCH5, sTNum, , , , , "1260-66 Switching Module Open BIT Test error<" & SError & "> in module 5")
                Switch5Proc = FAILED
                IncStepFailed()
                FirstSwitchTested = False
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else
                IncStepPassed()
                Echo(FormatResults(True, "1260-66 Switching Module Open BIT" & CStr(Channel), sTNum))
            End If
            Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete
            If OptionMode = LOSmode And OptionTestName = "HF Switch" And OptionStep = LoopStepNo Then
                GoTo HF_1_2
            End If

            iSubStep += 1
            LoopStepNo += 1

            frmSTest.proProgress.Value = frmSTest.proProgress.Value + 1
        Next Channel
        LoopStepNo = 12

        'setup DMM for impedance measurement
        nSystErr = WriteMsg(DMM, "*RST")
        nSystErr = WriteMsg(DMM, "*CLS")
        nSystErr = WriteMsg(DMM, "SENS:RES:APER MIN")

        'HFS-02-001 ~ HFS-02-018 (S901,S902)
        'HFS-03-001 ~ HFS-03-018 (S903,S904)
        'HFS-04-001 ~ HFS-04-018 (S905,S906)
        For PairNo = 0 To 2 ' three pairs of switches
            iTestStep = PairNo + 2
            iSubStep = 1
            If PairNo = 0 Then
                Echo(vbCrLf & "---- Test S901,S902")
                Mux1 = 0 ' S901
                Mux2 = 1 ' S902
                nSystErr = WriteMsg(SWITCH1, "CLOSE 5.20") '  S903-1,2
                nSystErr = WriteMsg(SWITCH1, "CLOSE 5.30") '  S904-1,2
            ElseIf PairNo = 1 Then
                Echo(vbCrLf & "---- Test S903,S904")
                Mux1 = 2 ' S903
                Mux2 = 3 ' S904
                nSystErr = WriteMsg(SWITCH1, "CLOSE 5.05") '  S901-1,7
                nSystErr = WriteMsg(SWITCH1, "CLOSE 5.15") '  S902-1,7
                nSystErr = WriteMsg(SWITCH1, "CLOSE 5.40") '  S905-1,2
                nSystErr = WriteMsg(SWITCH1, "CLOSE 5.50") '  S906-1,2
            Else
                Echo(vbCrLf & "---- Test S905,S906")
                Mux1 = 4 ' S905
                Mux2 = 5 ' S906
                nSystErr = WriteMsg(SWITCH1, "CLOSE 5.05") '  S901-1,7
                nSystErr = WriteMsg(SWITCH1, "CLOSE 5.15") '  S902-1,7
                nSystErr = WriteMsg(SWITCH1, "CLOSE 5.25") '  S903-1,7
                nSystErr = WriteMsg(SWITCH1, "CLOSE 5.35") '  S904-1,7
            End If

            For Channel = 0 To 5
                ChannelNo1 = "5." & Mux1 & Channel
                ChannelNo2 = "5." & Mux2 & Channel
                '             Mux 1,2  (Mux0,3    Mux4,5)
                '0 Close 5.10 S902-1,2 (S901-1,2, S905-1,2)
                '  Close 5.20 S903-1,2 (S904-1,2, S906-1,2)
                '1 Close 5.11 S902-1,3 (S901-1,3, S905-1,3)
                '  Close 5.21 S903-1,3 (S904-1,3, S906-1,3)
                '2 Close 5.12 S902-1,4 (S901-1,4, S905-1,4)
                '  Close 5.22 S903-1,4 (S904-1,4, S906-1,4)
                '3 Close 5.13 S902-1,5 (S901-1,5, S905-1,5)
                '  Close 5.23 S903-1,5 (S904-1,5, S906-1,5)
                '4 Close 5.14 S902-1,6 (S901-1,6, S905-1,6)
                '  Close 5.24 S903-1,6 (S904-1,6, S906-1,6)
                '5 Close 5.15 S902-1,7 (S901-1,7, S905-1,7)
                '  Close 5.25 S903-1,7 (S904-1,7, S906-1,7)

                ' fix this for various impedances
                ' S901,S902 position 2-6 (2 ohms) 7 (4 ohms)
                ' S903,S904 position 2-6 (4 ohms) 7 (6 ohms)
                ' S905,S906 position 2-7 (6 ohms)

                If PairNo = 0 And Channel <> 5 Then
                    PathImpX = 1
                ElseIf PairNo = 0 Or (PairNo = 1 And Channel <> 5) Then
                    PathImpX = 1.2
                Else
                    PathImpX = 1.4
                End If

                TestSwitch = FnSwitchName(ChannelNo1) & ", " & FnSwitchName(ChannelNo2)
                CleanSwitch = CStr(ChannelNo1) & "," & CStr(Mux2) & CStr(Channel)

                'HFS-02-001,004,007,010,013,016
                'HFS-03-001,004,007,010,013,016
                'HFS-04-001,004,007,010,013,016
                sTNum = "HFS-" & Format(iTestStep, "00") & "-" & Format(iSubStep, "000")
                iSubStep += 1
                LoopStepNo += 1


HF_2_1:
                nSystErr = WriteMsg(SWITCH1, "CLOSE " & ChannelNo1)
                nSystErr = WriteMsg(SWITCH1, "CLOSE " & ChannelNo2)

                If Not ClosureDetected(TestSwitch, 40 + PathImpX - DMM_TOL, 60 + PathImpX + DMM_TOL) Then
                    IncStepFailed()
                    Switch5Proc = FAILED
                    If OptionFaultMode = SOFmode Then GoTo TestComplete
                Else
                    IncStepPassed()
                End If
                Application.DoEvents()
                If AbortTest = True Then GoTo TestComplete
                If OptionMode = LOSmode And OptionTestName = "HF Switch" And OptionStep = LoopStepNo Then
                    nSystErr = WriteMsg(SWITCH1, "OPEN " & ChannelNo1)
                    nSystErr = WriteMsg(SWITCH1, "OPEN " & ChannelNo2)
                    GoTo HF_2_1
                End If

                'HFS-02-002,005,008,011,014,017
                'HFS-03-002,005,008,011,014,017
                'HFS-04-002,005,008,011,014,017
                sTNum = "HFS-" & Format(iTestStep, "00") & "-" & Format(iSubStep, "000")
                iSubStep += 1
                LoopStepNo += 1

HF_2_2:
                nSystErr = WriteMsg(SWITCH1, "OPEN " & ChannelNo1)
                If Not OpenDetected(ChannelNo1, "HFS") Then
                    Switch5Proc = FAILED
                    IncStepFailed()
                    If OptionFaultMode = SOFmode Then GoTo TestComplete
                Else
                    IncStepPassed()
                End If
                Application.DoEvents()
                If AbortTest = True Then GoTo TestComplete
                If OptionMode = LOSmode And OptionTestName = "HF Switch" And OptionStep = LoopStepNo Then
                    GoTo HF_2_2
                End If

                nSystErr = WriteMsg(SWITCH1, "CLOSE " & ChannelNo1)

                'HFS-02-003,006,009,012,015,018
                'HFS-03-003,006,009,012,015,018
                'HFS-04-003,006,009,012,015,018
                sTNum = "HFS-" & Format(iTestStep, "00") & "-" & Format(iSubStep, "000")
                iSubStep += 1
                LoopStepNo += 1

HF_2_3:
                nSystErr = WriteMsg(SWITCH1, "OPEN " & ChannelNo2)
                If Not OpenDetected(ChannelNo2, "HFS") Then
                    Switch5Proc = FAILED
                    IncStepFailed()
                    If OptionFaultMode = SOFmode Then GoTo TestComplete
                Else
                    IncStepPassed()
                End If
                Application.DoEvents()
                If AbortTest = True Then GoTo TestComplete
                If OptionMode = LOSmode And OptionTestName = "HF Switch" And OptionStep = LoopStepNo Then
                    GoTo HF_2_3
                End If


                nSystErr = WriteMsg(SWITCH1, "OPEN  " & ChannelNo1)
                frmSTest.proProgress.Value = frmSTest.proProgress.Value + 5
                Application.DoEvents()
                Application.DoEvents()
                Application.DoEvents()
            Next Channel
        Next PairNo
        frmSTest.proProgress.Value = 100
        Application.DoEvents()
        Application.DoEvents()
        Application.DoEvents()

TestComplete:
        frmSTest.cmdAbort.Text = "Abort Test"
        frmSTest.cmdPause.Text = "Pause Test"
        nSystErr = WriteMsg(SWITCH1, "OPEN 5.00-55")
        nSystErr = WriteMsg(DMM, "*RST") ' init DMM
        nSystErr = WriteMsg(DMM, "*CLS")
        If Switch5Proc = FAILED Then 'If Test Fails, register failure
            RegisterFailure(SWITCH5, , , , , , "HF Switch Module Failed")
        End If
        If AbortTest = True Then
            sMsg = vbCrLf
            sMsg &= "      **********************************************" & vbCrLf
            sMsg &= "      *        HF Switch test aborted!             *" & vbCrLf
            sMsg &= "      **********************************************" & vbCrLf
            Echo(sMsg)
        End If

    End Function


    '**************************************************************
    '* Nomenclature   : System Self Test: Switch Name Module      *
    '* Rewritten      : Jan 9,2006                                *
    '* Purpose        : This module take the programming syntax   *
    '*                  for the Switch modules and creates a      *
    '*                  string containing the ATLAS logic Names.  *
    '**************************************************************

    Public Function FnSwitchName(ByVal ChannelString As String) As String
        FnSwitchName = ""
        'DESCRIPTION:
        'Procedure to Format a string containg the ATLAS logic Names for the Switch Modules
        'Parameters:
        ' ChannelString: String containg the programing syntax to open or close each switch.
        ' ie 1.0000-0004,1000-1047,2000-2501,3000-3203,4000-4417
        'Returns:
        ' Formated String Containing the ATLAS logic Names for the switch modules
        ' ie S101-1,2(3,4) to S101-1,2(17,18), S301-1,2 to S301-47,48,,,,

        Dim ChannelNumber(50) As String
        Dim NumberOfChannels As Integer
        Dim FirstX As Double, LastX As Double

        Dim ModuleNumber As Integer 'used by all cards
        Dim LogicName As String 'used by all cards
        Dim LocalString As String 'used by all cards
        Dim i As Integer, j As Integer 'used in For Next loops

        'build a list of channels into the ChannelNumber() array
        ' comma is the only valid delimiter to separate 'Channels'
        NumberOfChannels = StringToList(ChannelString, ChannelNumber, ",")

        LocalString = ""
        'First entry must always have a module number
        ModuleNumber = CInt(Strings.Left(ChannelNumber(1), 1)) ' "1.1010-1017"
        ChannelNumber(1) = Mid(ChannelNumber(1), 3)            ' "1010-1017"

        For j = 1 To NumberOfChannels
            LogicName = ""
            If InStr(ChannelNumber(j), ".") Then ' get new ModuleNumber '("1.1010-1017")
                ChannelNumber(j) = Trim(ChannelNumber(j))
                ModuleNumber = CInt(Strings.Left(ChannelNumber(j), 1))  '("1") just module number
                ChannelNumber(j) = Mid(ChannelNumber(j), 3)             '(1010-1017) deletes module number
            End If

            Select Case ModuleNumber
                Case 1, 2
                    '1260-39 CARD LF1 and LF2
                    i = InStr(ChannelNumber(j), "-")
                    If i > 1 Then
                        FirstX = Val(Strings.Left(ChannelNumber(j), i - 1))
                        LastX = Val(Mid(ChannelNumber(j), i + 1))
                        LogicName = FnNameSwitch(ModuleNumber, FirstX)
                        LogicName &= " to " & FnNameSwitch(ModuleNumber, LastX)
                    Else
                        LogicName = FnNameSwitch(ModuleNumber, Val(ChannelNumber(j)))
                    End If
                    LogicName &= ", "

                Case 3
                    '1260-38 CARD LF3
                    i = InStr(ChannelNumber(j), "-")
                    If i > 1 Then
                        FirstX = Val(Strings.Left(ChannelNumber(j), i - 1))
                        LastX = Val(Mid(ChannelNumber(j), i + 1))
                        LogicName = FnNameSwitch(3, FirstX)
                        LogicName &= " to " & FnNameSwitch(3, LastX)
                    Else
                        LogicName = FnNameSwitch(3, Val(ChannelNumber(j)))
                    End If
                    LogicName &= ", "

                Case 4
                    '1260-58 CARD (4.00-07, 4.10-17, 4.20-27, 4.30-37)
                    i = InStr(ChannelNumber(j), "-")
                    If i > 1 Then
                        FirstX = Val(Strings.Left(ChannelNumber(j), i - 1))
                        LastX = Val(Mid(ChannelNumber(j), i + 1))
                        LogicName = FnNameSwitch(4, FirstX)
                        LogicName &= " to " & FnNameSwitch(4, LastX)
                    Else
                        LogicName = FnNameSwitch(4, Val(ChannelNumber(j)))
                    End If
                    LogicName &= ", "

                Case 5
                    '1260-66A CARD (5.00-05, 5.10-15, 5.20-25, 5.30-35, 5.40-45, 5.50-55)
                    i = InStr(ChannelNumber(j), "-")
                    If i > 1 Then
                        FirstX = Val(Strings.Left(ChannelNumber(j), i - 1))
                        LastX = Val(Mid(ChannelNumber(j), i + 1))
                        LogicName = FnNameSwitch(5, FirstX)
                        LogicName &= " to " & FnNameSwitch(5, LastX)
                    Else
                        LogicName = FnNameSwitch(5, Val(ChannelNumber(j)))
                    End If
                    LogicName &= ", "
            End Select

            LocalString &= LogicName
        Next j
        If Len(LocalString) > 2 Then
            LocalString = Strings.Left(LocalString, Len(LocalString) - 2) 'Strip the last ", "
        End If
        FnSwitchName = LocalString

    End Function

    Function FnNameSwitch(ByVal ModuleX As Integer, ByVal Designator As Integer) As String
        FnNameSwitch = ""

        ' This function
        '    inputs one switch designator (4417) and
        '    outputs one switch name (S505-2,10)
        Dim NameX As String = ""
        Dim SwitchX As Integer

        If ModuleX = 1 Then ' LF1 1.0000-1.4417
            Select Case Designator
                Case 0 : NameX = "S101-1,2" '(3,4)"
                Case 1 : NameX = "S101-5,6" '(7,8)"
                Case 2 : NameX = "S101-9,10" '(11,12)"
                Case 3 : NameX = "S101-13,14" '(15,16)"
                Case 4 : NameX = "S101-17,18" '(19,20)"
                Case 1000 To 1047
                    SwitchX = (Designator Mod 1000) * 2 + 1
                    NameX = "S301-" & SwitchX & "," & (SwitchX + 1) ' S301-1,2
                Case 2000 To 2501
                    SwitchX = (Designator - 2000) \ 100 + 1
                    NameX = "S60" & Format(SwitchX, "0") & "-"
                    Select Case Designator Mod 10
                        Case 0 : NameX &= "1,2"
                        Case 1 : NameX &= "1,3"
                    End Select
                Case 3000 To 3203
                    SwitchX = (Designator - 3000) \ 100 + 1
                    NameX = "S40" & Format(SwitchX, "0") & "-"
                    Select Case Designator Mod 10
                        Case 0 : NameX &= "1,2"
                        Case 1 : NameX &= "1,3"
                        Case 2 : NameX &= "1,4"
                        Case 3 : NameX &= "1,5"
                    End Select
                Case 4000 To 4417
                    SwitchX = (Designator - 4000) \ 100 + 1
                    NameX = "S5" & Format(SwitchX, "00") & "-"
                    Select Case Designator Mod 100
                        Case 0 : NameX &= "1,3"
                        Case 1 : NameX &= "1,4"
                        Case 2 : NameX &= "1,5"
                        Case 3 : NameX &= "1,6"
                        Case 4 : NameX &= "1,7"
                        Case 5 : NameX &= "1,8"
                        Case 6 : NameX &= "1,9"
                        Case 7 : NameX &= "1,10"
                        Case 10 : NameX &= "2,3"
                        Case 11 : NameX &= "2,4"
                        Case 12 : NameX &= "2,5"
                        Case 13 : NameX &= "2,6"
                        Case 14 : NameX &= "2,7"
                        Case 15 : NameX &= "2,8"
                        Case 16 : NameX &= "2,9"
                        Case 17 : NameX &= "2,10"
                    End Select
            End Select

        ElseIf ModuleX = 2 Then            ' LF2 2.0000-2.4417
            Select Case Designator
                Case 0 : NameX = "S101-21,22" '(23,24)"
                Case 1 : NameX = "S101-25,26" '(27,28)"
                Case 2 : NameX = "S101-29,30" '(31,32)"
                Case 3 : NameX = "S101-33,34" '(35,36)"
                Case 4 : NameX = "S101-37,38" '(39,40)"
                Case 1000 To 1047
                    SwitchX = (Designator Mod 1000) * 2 + 97
                    NameX = "S301-" & SwitchX & "," & (SwitchX + 1) ' S301-1,2
                Case 2000 To 2501
                    SwitchX = (Designator - 2000) \ 100 + 7
                    NameX = "S6" & Format(SwitchX, "00") & "-"
                    Select Case Designator Mod 10
                        Case 0 : NameX &= "1,2"
                        Case 1 : NameX &= "1,3"
                    End Select
                Case 3000 To 3203
                    SwitchX = (Designator - 3000) \ 100 + 4
                    NameX = "S40" & Format(SwitchX, "0") & "-"
                    Select Case Designator Mod 10
                        Case 0 : NameX &= "1,2" ' s401-1,2
                        Case 1 : NameX &= "1,3"
                        Case 2 : NameX &= "1,4"
                        Case 3 : NameX &= "1,5"
                    End Select
                Case 4000 To 4417
                    SwitchX = (Designator - 4000) \ 100 + 6
                    NameX = "S5" & Format(SwitchX, "00") & "-"
                    Select Case Designator Mod 100
                        Case 0 : NameX &= "1,3"
                        Case 1 : NameX &= "1,4"
                        Case 2 : NameX &= "1,5"
                        Case 3 : NameX &= "1,6"
                        Case 4 : NameX &= "1,7"
                        Case 5 : NameX &= "1,8"
                        Case 6 : NameX &= "1,9"
                        Case 7 : NameX &= "1,10"
                        Case 10 : NameX &= "2,3"
                        Case 11 : NameX &= "2,4"
                        Case 12 : NameX &= "2,5"
                        Case 13 : NameX &= "2,6"
                        Case 14 : NameX &= "2,7"
                        Case 15 : NameX &= "2,8"
                        Case 16 : NameX &= "2,9"
                        Case 17 : NameX &= "2,10"
                    End Select
            End Select

        ElseIf ModuleX = 3 Then            ' LF3 3.0000-3.0157
            ' Designator
            ' 0000-0007 S751-1,2(3,4) to S751-1,2(17,18)
            ' 0010-0037 S701-1,2(3,4) to S701-1,2(49,50)
            ' 0040-0087 S201-1,2(5,6) to S201-1,2(51,52)
            ' 0050-0097 S201-3,4(5,6) to S201-3,4(51,52)
            ' 0100-0147 S202-1,2(5,6) to S202-1,2(51,52)
            ' 0110-0157 S202-3,4(5,6) to S202-3,4(51,52)
            Select Case Designator
                Case 0 To 7
                    ' S751-1,2(3,4), S751-1,2(5,6),,,
                    SwitchX = (Designator Mod 10) * 2 + 3
                    NameX = "S751-1,2(" & SwitchX & "," & (SwitchX + 1) & ")"
                Case 10 To 17
                    ' S701-1,2(3,4)
                    SwitchX = (Designator Mod 10) * 2 + 3
                    NameX = "S701-1,2(" & SwitchX & "," & (SwitchX + 1) & ")"
                Case 20 To 27
                    ' S701-1,2(19,20)
                    SwitchX = (Designator Mod 10) * 2 + 19
                    NameX = "S701-1,2(" & SwitchX & "," & (SwitchX + 1) & ")"
                Case 30 To 37
                    ' S701-1,2(35,36)
                    SwitchX = (Designator Mod 10) * 2 + 35
                    NameX = "S701-1,2(" & SwitchX & "," & (SwitchX + 1) & ")"
                Case 40 To 47
                    ' S201-1,2(5,6)
                    SwitchX = (Designator Mod 10) * 2 + 5
                    NameX = "S201-1,2(" & SwitchX & "," & (SwitchX + 1) & ")"
                Case 50 To 57
                    ' S201-3,4(5,6)
                    SwitchX = (Designator Mod 10) * 2 + 5
                    NameX = "S201-3,4(" & SwitchX & "," & (SwitchX + 1) & ")"
                Case 60 To 67
                    ' S201-1,2(21,22)
                    SwitchX = (Designator Mod 10) * 2 + 21
                    NameX = "S201-1,2(" & SwitchX & "," & (SwitchX + 1) & ")"
                Case 70 To 77
                    ' S201-3,4(21,22)
                    SwitchX = (Designator Mod 10) * 2 + 21
                    NameX = "S201-3,4(" & SwitchX & "," & (SwitchX + 1) & ")"
                Case 80 To 87
                    ' S201-1,2(37,38)
                    SwitchX = (Designator Mod 10) * 2 + 37
                    NameX = "S201-1,2(" & SwitchX & "," & (SwitchX + 1) & ")"
                Case 90 To 97
                    ' S201-3,4(37,38)
                    SwitchX = (Designator Mod 10) * 2 + 37
                    NameX = "S201-3,4(" & SwitchX & "," & (SwitchX + 1) & ")"
                Case 100 To 107
                    ' S202-1,2(5,6)
                    SwitchX = (Designator Mod 10) * 2 + 5
                    NameX = "S202-1,2(" & SwitchX & "," & (SwitchX + 1) & ")"
                Case 110 To 117
                    ' S202-3,4(5,6)
                    SwitchX = (Designator Mod 10) * 2 + 5
                    NameX = "S202-3,4(" & SwitchX & "," & (SwitchX + 1) & ")"
                Case 120 To 127
                    ' S202-1,2(21,22)
                    SwitchX = (Designator Mod 10) * 2 + 21
                    NameX = "S202-1,2(" & SwitchX & "," & (SwitchX + 1) & ")"
                Case 130 To 137
                    ' S202-3,4(21,22)
                    SwitchX = (Designator Mod 10) * 2 + 21
                    NameX = "S202-3,4(" & SwitchX & "," & (SwitchX + 1) & ")"
                Case 140 To 147
                    ' S202-1,2(37,38)
                    SwitchX = (Designator Mod 10) * 2 + 37
                    NameX = "S202-1,2(" & SwitchX & "," & (SwitchX + 1) & ")"
                Case 150 To 157
                    ' S202-3,4(37,38)
                    SwitchX = (Designator Mod 10) * 2 + 37
                    NameX = "S202-3,4(" & SwitchX & "," & (SwitchX + 1) & ")"
            End Select

        ElseIf ModuleX = 4 Then            ' 4.00-4.37  Medium frequency switches
            'Designator 00-37 to  S801-1,2 to S804-1,9
            SwitchX = (Designator Mod 10) + 2
            NameX = "S80" & Format((Designator \ 10) + 1, "0") & "-1," & Format(SwitchX, "0")


        ElseIf ModuleX = 5 Then            ' 5.00-5.55  High frequency switches
            'Designator 00-55, S901-1,2 to S906-1,7
            SwitchX = (Designator Mod 10) + 2
            NameX = "S90" & Format((Designator \ 10) + 1, "0") & "-1," & Format(SwitchX, "0")

        End If
        FnNameSwitch = NameX

    End Function


End Module