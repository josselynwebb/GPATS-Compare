'Option Strict Off
Option Explicit On
    '***************************************************************
    '* Nomenclature   : ATS-TETS SYSTEM SELF TEST                  *
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
    Dim FirstSwitchTested As Integer          'Indicates if switch is the first in the test
    Dim dReturnResistance As Double           'Holds the Measured Resistance value in OpenDetected and ClosureDetected
    Dim iStepCounter As Integer               'Counter to track step number in Loop, used as the Sub Test number
    Dim iTestStep As Integer                  'Test Step Counter
    Dim iSubStep As Integer                   'Test SubTest Counter
    Dim iModAddress As Integer                'Module Address
    Public dW24_Resistance As Double            'Resistance on Cable W24
    Public dW25_CT_Resistance As Double         'Resistance on Cable W25
    Public dW21_Resistance As Double            'Resistance on Cable W21
    Dim S As String

    'To compensate for the DMM accuracy of +- 200 mOhms
    'Subtract "0.2 Ohms" from the LL and add "0.2 Ohms" to the UL
    Const DMM_TOL As Double = 0.2       'DMM 2-Wire Tolerance    'DR#265, 03/18/02 DJOINER


    Function TestSwitching(InstrumentToTest As Integer) As Integer
        'DESCRIPTION
        '   This routine tests the Racal Instruments Switching cards.
        '            *** NOTE***
        '       SWITCH1 is the handle for all switch modules because it contains the controller
        'PARAMETERS:
        '   InstrumentToTest = The index of the switch to be tested (SWITCH1 - SWITCH5)

        '3 Switch1 - Low Frequency switches
        '4 Switch2 - Low Frequency switches
        '5 Switch3 - Low Frequency switches
        '6 Switch4 - Medium Frequency switches
        '7 Switch5 - High Frequency switches
        Dim FailOccur As Integer = False

        Dim dMeasurement As Double = 0
        Dim iCableTest As Integer = 0      'Holds Return 0 if Passed, Cable ID if Failed

        TestSwitching = PASSED
        If FirstPass = True Then
            MsgBox("Remove all cable connections from the SAIF.")
        End If
        AbortTest = False
        TestSwitching = True
        EchoTitle(InstrumentDescription(InstrumentToTest), True)
        If (RunningEndToEnd = False) Or (InstrumentToTest = SWITCH1) Then
            FirstSwitchTested = True
        Else
            FirstSwitchTested = False
        End If

        If InstrumentStatus(DMM) = FAILED And (RunningEndToEnd = True) Then
            TestSwitching = DMM
            Exit Function
        End If

        If InstrumentToTest = SWITCH4 Then
            If (InstrumentStatus(COUNTER) = FAILED) And (RunningEndToEnd = True) Then
                TestSwitching = COUNTER
                Exit Function
            End If
            If InstrumentStatus(COUNTER) = 0 Then
                Echo("ERROR -  " & InstrumentDescription(InstrumentToTest) & _
                " not tested.  Unable to establish instrument communication with the Counter/Timer.")
                TestSwitching = COUNTER
                Exit Function
            End If
            nSystErr = WriteMsg(COUNTER, "*RST")
            nSystErr = WriteMsg(COUNTER, "*CLS")
            nSystErr = WriteMsg(COUNTER, "INP1:IMP 50")
        End If

        If InstrumentStatus(DMM) = FAILED Then
            Echo("ERROR -  " & InstrumentDescription(InstrumentToTest) & _
            " not tested.  Unable to establish instrument communication with the Digital Multimeter.")
            TestSwitching = DMM
            Exit Function
        End If

        If iCableTest Then
            TestSwitching = iCableTest
            Exit Function
        End If

        Select Case InstrumentToTest
            Case SWITCH1    '1260-39 (1)  (LF1, 2amp and 10amp switches, with controller)
                'GEN-06-001 ~ GEN-06-003 Pass/Fail results reported in Function DetermineCableResistance()
                If dW24_Resistance = 0 Then
                    iCableTest = DetermineCableResistance(InstrumentToTest)
                End If
                FailOccur = Switch1or2Proc(InstrumentToTest)
            Case SWITCH2    '1260-39 (2)  (LF2, 2amp and 10amp switches, no controller)
                If dW24_Resistance = 0 Then
                    iCableTest = DetermineCableResistance(InstrumentToTest)
                End If
                FailOccur = Switch1or2Proc(InstrumentToTest)
            Case SWITCH3    '1260-38T     (LF3 is the 2amp two/four wire switches, no controller)
                If dW24_Resistance = 0 Then
                    iCableTest = DetermineCableResistance(InstrumentToTest)
                End If
                FailOccur = Switch3Proc()
            Case SWITCH4    '1260-58      (Medium frequency switches, no controller)
                If dW25_CT_Resistance = 0 Then
                    iCableTest = DetermineCableResistance(InstrumentToTest)
                End If
                FailOccur = Switch4Proc()
                nSystErr = WriteMsg(COUNTER, "*RST")
                nSystErr = WriteMsg(COUNTER, "*CLS")
            Case SWITCH5    '1260-66      (High frequency switches, no controller)
                If dW21_Resistance = 0 Then
                    iCableTest = DetermineCableResistance(InstrumentToTest)
                End If
                FailOccur = Switch5Proc()
        End Select

        If AbortTest = True Then
            If FailOccur = FAILED Then
                TestSwitching = FAILED
                ReportFailure(InstrumentToTest)
            Else
                ReportUnknown(InstrumentToTest)
                TestSwitching = 99
            End If
            Exit Function
        End If

        If FailOccur <> PASSED Then
            If FirstSwitchTested Then
                If InstrumentStatus(DMM) = PASSED And RunningEndToEnd Then
                    TestSwitching = FAILED
                    Exit Function
                End If
                S = "Disconnect all cables from the SAIF." & vbCrLf
                S &= "Use cable W24 (OBSERVE POLARITY) to connect" & vbCrLf & vbCrLf
                S &= "   DMM inputs (HI/LO)   to   UTILITY LOADS (HI/LO)."
                DisplaySetup(S, "TETS DIN_ULD.jpg", 1)
                nSystErr = WriteMsg(DMM, "*RST")
                nSystErr = WriteMsg(DMM, "*CLS")
                nSystErr = WriteMsg(DMM, "CONF:RES 30")
                nSystErr = WriteMsg(DMM, "INIT;FETCH?")
                'LFx-(TestStep)-D01
                dMeasurement = FetchMeasurement(DMM, "Switch Diagnostic Test", "7.9", "14.2", "Ohms", DMM, ReturnTestNumber(InstrumentToTest, iTestStep, 1, "D"))
                If dMeasurement < 7.9 Or dMeasurement > 14.2 Then
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

    Function ClosureDetected(SwitchLable As String, dLower As Double, dUpper As Double, _
                            Optional sInstrumentCode As String = "", _
                            Optional iFailingInstrument As Integer = 0, _
                            Optional sSwitchCallout As String = "", _
                            Optional dSwitchPathLoss As Single = 0) As Boolean
        'DESCRIPTION:
        '   This routine detects the presence of a closed circuit between the high and low terminals of the DMM.
        '   For use in switching tests only.
        'PARAMETERS:
        '   SwitchLable = The name of the switch(es) being tested
        '   Resistance  = The maximum allowable resistance for the switching being tested

        'DR# 266, 03/18/02 DJoiner
        'Added Optional Argument "sSwitchCallout" to pass Failing Switch Connection Info
        'to function FetchMeasurement() to record in the FHDB in the event of a failure.

        'Added Optional Argument dSwitchPathLoss to show resistance across individual switch where possible

        Dim dMeasurement As Double = 0     'Measurement Value
        Dim sMeasString As String = ""     'Measurement Value as a String
        Dim i As Integer = 0               'Counter Index for Loop

        sTNum = sInstrumentCode & "-" & Format(iTestStep, "00") & "-" & Format(iSubStep, "000")

        Delay(0.05)                              'Allow time for switch closure and settling
        nSystErr = WriteMsg(DMM, "MEAS:RES? 50,MAX")   'MAX resolution = 10 mOhm = fastest aperture
        nSystErr = WaitForResponse(DMM, 0)
        nSystErr = ReadMsg(DMM, sMeasString)
        dMeasurement = CDbl(sMeasString)          '- CableResistance# '<-Modified by KHSU per DR# 94

        If dSwitchPathLoss <> 0 Then
            dMeasurement = dMeasurement - dSwitchPathLoss
        End If
        dMeasurement = Math.Abs(dMeasurement)
        dReturnResistance = dMeasurement    'DJoiner
        If (dMeasurement > dUpper) Or (dMeasurement < dLower) Then
            '  Debug.Print("Before " & dMeasurement)
            sMsg = ""
            nSystErr = WriteMsg(SWITCH1, "PDATAOUT " & CStr(iModAddress))   'Request current switch closures
            nSystErr = ReadMsg(SWITCH1, sMeasString)            'Throw out the ID line
            Do While nSystErr = VI_SUCCESS
                nSystErr = ReadMsg(SWITCH1, sMeasString)        'Read "closed switches"
                If InStr(sMeasString, "END") <> 0 Then Exit Do 'Until last line
                sMsg = sMsg & sMeasString                       'Append multiple lines
            Loop
            'This loop cycles the relay contacts with DMM voltage applied
            ' (6V open, 1mA closed) to attempt to clean carbon deposits.
            For i = 1 To 5
                nSystErr = WriteMsg(SWITCH1, "OPEN " & sMsg)
                nSystErr = WriteMsg(SWITCH1, "CLOSE " & sMsg)
            Next i

            Delay(0.05)                              'Allow time for switch closure and settling
            nSystErr = WriteMsg(DMM, "MEAS:RES? 50,MIN")   'MIN resolution = 30 uOhm = slowest aperture
            nSystErr = WaitForResponse(DMM, 0)
            'Report Formatted result w/Pass-Fail Status to System Log, Record Failures in the FHDB
            dMeasurement = FetchMeasurement(DMM, SwitchLable & " Closed Test", CStr(dLower), CStr(dUpper), "Ohms", iFailingInstrument, sTNum, sSwitchCallout)
            '            Debug.Print("After " & dMeasurement & "  " & sMsg)
            If dSwitchPathLoss <> 0 Then
                dMeasurement = dMeasurement - dSwitchPathLoss
            End If
            dMeasurement = Math.Abs(dMeasurement)
            If dMeasurement > dUpper Or dMeasurement < dLower Then
                ClosureDetected = False
            Else
                ClosureDetected = True
                FirstSwitchTested = False
            End If
        Else
            'Report test results - Report Formatted result Pass Status to System Log
            RecordTest(sTNum, SwitchLable & " Closed Test", dLower, dUpper, dMeasurement, "Ohms")
            ClosureDetected = True
            FirstSwitchTested = False
        End If

    End Function

    Function DetermineCableResistance(iSwitchNumber As Integer) As Integer
        '
        'ECO-530,  DR#94,   06/28/01
        'Description:
        '             Records the resistance in cables used in switching tests.
        'Parameters:
        '             iSwitchNumber - The ID of the switch board being tested
        'Returns:
        '             0 if successful, Cable Index if not

        Dim sMeasString As String = ""    'Returned Measurement from Instrument
        Dim bRetry As Boolean = False     'Flag to indicate a second try
        Dim iCable As Integer = 0         'Cable Index to Identify Cable
        Dim dLL As Double = 0             'Lower Limit
        Dim dUL As Double = 0             'Upper Limit

        bRetry = False                  'Initialize Retry flag
        DetermineCableResistance = 0        'Cable Test Passed

        'DR# 265, 03/18/02 DJoiner
        'Allow for DMM 2-Wire Tolerance of +- 0.2 Ohmns

Start:
        Select Case iSwitchNumber
            Case SWITCH1, SWITCH2, SWITCH3
                If (dW24_Resistance > 0 And dW24_Resistance < 2.5) Then
                    Exit Function
                End If
                DetermineCableResistance = CBL_W24
Step1:
                S = "Connect one end of cable W24 (OBSERVE POLARITY)  to  DMM INPUT (HI/LO)"
                DisplaySetup(S, "TETS DIN_HL.jpg", 1, True, 1, 2)
                If AbortTest = True Then Exit Function
Step2:
                S = "Use cable W28 to short the free end of cable W24."
                DisplaySetup(S, "TETS DIN_LOOP.jpg", 1, True, 2, 2)
                If AbortTest = True Then Exit Function
                If GoBack = True Then GoTo Step1

                iCable = CBL_W24        '"W24 or W28"
                dLL = 0 - DMM_TOL : dUL = 5
                sTNum = "GEN-06-001"
            Case SWITCH4
                If (dW25_CT_Resistance > 40) And (dW25_CT_Resistance < 60) Then
                    DetermineCableResistance = CBL_W25
                    Exit Function
                End If
                S = "Use cable W25 (OBSERVE POLARITY) to connect" & vbCrLf & vbCrLf
                S &= "   DMM INPUT (HI/LO)   to   C/T INPUT 1."
                DisplaySetup(S, "TETS DIN_CT1.jpg", 1)
                If AbortTest = True Then Exit Function

                iCable = CBL_W25        '"W25 or C/T INPUT 1"
                dLL = 40 : dUL = 60
                sTNum = "GEN-06-002"
            Case SWITCH5
                If (dW21_Resistance > 40) And (dW21_Resistance < 60) Then
                    DetermineCableResistance = CBL_W21
                    Exit Function
                End If
                S = "Use cable W21 (OBSERVE POLARITY) and an ""N"" Type COAX Adapter to connect " & vbCrLf & vbCrLf
                S &= "    DMM INPUT (HI/LO)   to   50 Ohm Terminator."
                DisplaySetup(S, "TETS DIN_50Term.jpg", 1)
                If AbortTest = True Then Exit Function

                iCable = CBL_W21        '"W21 or 50 Ohm Term"
                dLL = 40 : dUL = 60
                sTNum = "GEN-06-003"
        End Select

        DetermineCableResistance = iCable       'Initialize return to the cable index value (Fail)

        nSystErr = WriteMsg(DMM, "*RST;*CLS")
        nSystErr = WriteMsg(DMM, "SENS:RES:APER MIN")
        Delay(0.01)
        nSystErr = WriteMsg(DMM, "MEAS:RES? 50")
        nSystErr = WaitForResponse(DMM, 0)
        nSystErr = ReadMsg(DMM, sMeasString)
        'If returned Measurement is Over Range
        If (CDbl(sMeasString) < dLL) Or (CDbl(sMeasString) > dUL) Then
            If Not bRetry Then              'If first try
                Echo(FormatResults(False, InstrumentDescription(iCable) & " Cable Resistance Test ", sTNum, CStr(dLL), CStr(dUL), sMeasString, "Ohms"))
                MsgBox("Cable Resistence test failed. Please check cable connection and click OK to try again.")
                bRetry = True               'Set Retry Flag to True
                GoTo Start                  'Try to get a measurement that is in range one more time
            Else                            'If second try
                MsgBox("Defective cable detected: " & InstrumentDescription(iCable))
                Echo(InstrumentDescription(iCable) & " Cable Resistance Test second attempt")       'Second attempt failed
                RegisterFailure(InstrumentDescription(iCable), sTNum, CDbl(sMeasString), "Ohms", dLL, dUL, InstrumentDescription(iCable) & " Cable Resistance Test Failed")
                DetermineCableResistance = iCable
                Exit Function
            End If
        End If

        Echo(FormatResults(True, InstrumentDescription(iCable) & " Cable Resistance Test ", sTNum, CStr(dLL), CStr(dUL), sMeasString, "Ohms"))

        Select Case iSwitchNumber
            Case SWITCH1, SWITCH2, SWITCH3
                dW24_Resistance = CDbl(sMeasString)
            Case SWITCH4
                dW25_CT_Resistance = CDbl(sMeasString)
            Case SWITCH5
                dW21_Resistance = CDbl(sMeasString)
        End Select


    End Function

    Function OpenDetected(SwitchNum As String, Optional sInstrumentCode As String = "LF1") As Integer
        'DESCRIPTION:
        '   This routine detects the presence of an open circuit between the
        '   high and low terminals of the DMM
        'PARAMETERS:
        '   SwitchNum = This is a string description of the switch being tested
        '------------------------------------------------------------------------
        '***  Failures are recorded in the FHDB in the calling routine  ***

        Dim StrMeas As String = ""       'String containing the Measurement
        Dim retval As Integer = 0        'Return Value from function
        Dim Initialtext As String = 0    'Text to Echo to the System Log
        OpenDetected = PASSED

        'Build Full Test Number
        sTNum = sInstrumentCode & "-" & Format(iTestStep, "00") & "-" & Format(iSubStep, "000")

        nSystErr = WriteMsg(DMM, "MEAS:RES? 10E6,MAX")
        nSystErr = WaitForResponse(DMM, 0)
        retval = ReadMsg(DMM, StrMeas)
        If StrMeas = "" Then StrMeas = "0" 'Protect Data Type from Error
        dReturnResistance = CDbl(StrMeas)          'Convert String to Double
        'Determine if switch falls within the acceptable Open range
        If Val(StrMeas) < 2000000.0# Or retval <> 0 Then
            Delay(1)
            nSystErr = WriteMsg(DMM, "MEAS:RES? 10E6")
            nSystErr = WaitForResponse(DMM, 0)
            retval = ReadMsg(DMM, StrMeas)
            'If not, measure again and evaluate
            If Val(StrMeas) < 2000000.0# Or retval <> 0 Then
                'Report FAIL status to the System Log
                Echo(sTNum)
                FormatResultLine("  " & SwitchNum & " Open Test  Measured: " & EngNotate(dReturnResistance) & "Ohms", False)
                OpenDetected = FAILED
                Exit Function
            End If
        End If
        Initialtext = "   " & SwitchNum & " Open Test"
        Initialtext = Initialtext & Space(59 - Len(Initialtext))
        'Report PASSED status to the System Log
        Echo(sTNum & Initialtext & "PASSED")

        OpenDetected = PASSED

    End Function

    Function Switch1or2Proc(InstrumentToTest As Integer) As Integer
        'DESCRIPTION:
        '   This routine tests LF-1 or LF-2
        'PARAMETERS:
        '   InstrumentToTest = This determines whether LF-1 or LF-2 will be tested
        '                       (SWITCH1 for LF-1, SWITCH2 for LF-2)
        'RETURNS:
        '   PASSED(0) if the switch being tested passed or FAILED(1) if the test fails

        'DR# 266, 03/18/02 DJoiner
        'In calls to function ClosureDetected()
        'Allow for DMM 2-Wire Tolerance of +- 0.2 Ohmns

        Dim Channels(0 To 5) As String
        Dim ModuleNo As String = ""
        Dim RetMess As String = ""
        Dim ModuleX As Integer = 0
        Dim SError As Integer = 0
        Dim LastChannel As Integer = 0
        Dim Channel As Integer = 0
        Dim retval As Integer = 0
        Dim StrMeas As String = ""
        Dim StrMeasurement As String = ""
        Dim ValMesurement As String = ""
        Dim SelfTest As Integer = 0
        Dim FirstPair_1 As String = ""
        Dim FirstPair_2 As String = ""
        Dim SecondPair_1 As String = ""
        Dim SecondPair_2 As String = ""
        Dim CloseTestName As String = ""
        Dim i As Integer = 0
        Dim J As Integer = 0
        Dim ChannelNo As String = ""
        Dim FirstChannel As String = ""
        Dim SecondChannel As String = ""
        Dim ChannelOpen As String = ""
        Dim S501_1 As String = ""
        Dim S501_2 As String = ""
        Dim S504 As String = ""
        Dim Repeat As Integer = 0
        Dim LogicName1 As String = ""
        Dim LogicName2 As String = ""
        Dim TestName As String = ""
        Dim sSwitchCallout As String = ""   'Describes the switches failed for the Fault Callout
        Dim LoopStepNo As Integer
        Dim dSwitchPathLoss As Single
        Dim SwitchTestName As String
        Dim TestMin As Single
        Dim TestMax As Single

        frmSTest.proProgress.Maximum = 260
        frmSTest.proProgress.Value = 1


        iTestStep = 1
        iSubStep = 0
        Switch1or2Proc = PASSED
        LoopStepNo = 0
        Application.DoEvents()

        If InstrumentToTest = SWITCH1 Then
            iModAddress = 1
            ModuleNo = "1"
            TestName = "LF1 Switch"
            EchoTitle("Low Frequency Switching # 1 Test", False)
        Else              'SWITCH2
            iModAddress = 2
            ModuleNo = "2"
            TestName = "LF2 Switch"
            EchoTitle("Low Frequency Switching # 2 Test", False)
        End If
        nSystErr = WriteMsg(SWITCH1, "RESET")

        If InstrumentToTest = SWITCH1 Then

LF1_1_1:    'LF1-01-001        Non-Destructive RAM Test
            sTNum = "LF1-01-001"
            LoopStepNo = 1
            nSystErr = WriteMsg(SWITCH1, "TEST 0.1")
            Delay(0.5)
            nSystErr = ReadMsg(SWITCH1, RetMess)
            If Trim(RetMess) <> "7F" Then
                Echo(sTNum & "     1260 Switching Module Non-Destructive RAM Failure ERROR <" & CStr(SwitchingError(ModuleX)) & ">")
                Switch1or2Proc = FAILED
                RegisterFailure(CStr(InstrumentToTest), sTNum, sComment:="1260 Switching Module Non-Destructive RAM Failure ERROR <" & CStr(SwitchingError(ModuleX)) & ">")
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
            frmSTest.proProgress.Value += 1

            sTNum = "LF1-01-002"
            LoopStepNo = 2
LF1_1_2:    'LF1-01-002        EPROM Checksum Test
            nSystErr = WriteMsg(SWITCH1, "TEST 0.2")
            nSystErr = ReadMsg(SWITCH1, RetMess)
            If Trim(RetMess) <> "7F" Then
                Echo(sTNum & "     1260 Switching Module EPROM Checksum Failure ERROR <" & CStr(SwitchingError(ModuleX)) & ">")
                RegisterFailure(CStr(InstrumentToTest), sTNum, , , , , "1260 Switching ModuleX EPROM Checksum Failure ERROR <" & CStr(SwitchingError(ModuleX)) & ">")
                Switch1or2Proc = FAILED
                IncStepFailed()
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
            frmSTest.proProgress.Value += 1

            sTNum = "LF1-01-003"
            LoopStepNo = 3
LF1_1_3:    'LF1-01-003        Non-Destructive Non-Volatile Memory Test
            nSystErr = WriteMsg(SWITCH1, "TEST 0.3")
            nSystErr = ReadMsg(SWITCH1, RetMess)
            If Trim(RetMess) <> "7F" Then
                Echo(sTNum & "     1260 Switching Module Non-Volatile Memory Failure ERROR <" & CStr(SwitchingError(ModuleX)) & ">")
                RegisterFailure(CStr(InstrumentToTest), sTNum, , , , , "1260 Switching Module Non-Volatile Memory Failure ERROR <" & CStr(SwitchingError(ModuleX)) & ">")
                Switch1or2Proc = FAILED
                IncStepFailed()
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
            frmSTest.proProgress.Value += 1

            sTNum = "LF1-01-004"
            LoopStepNo = 4
LF1_1_4:    'LF1-01-004        Switch RESET Self test
            nSystErr = WriteMsg(SWITCH1, "RESET")
            Delay(0.1)
            nSystErr = WriteMsg(SWITCH1, "CNF ON")
            SError = SwitchingError(ModuleX)
            If SError <> 0 Then
                Echo(sTNum & "     1260 Switching Module Reset Test error<" & CStr(SError) & "> in module " & CStr(ModuleX))
                RegisterFailure(CStr(InstrumentToTest), sTNum, , , , , "1260 Switching Module Reset Test error<" & CStr(SError) & "> in module " & CStr(ModuleX))
                Switch1or2Proc = FAILED
                IncStepFailed()
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
        frmSTest.proProgress.Value += 1

        frmSTest.sbrUserInformation.Text = "Testing " & InstrumentDescription(SWITCH1)
        Application.DoEvents()
        'LF1-01-005; LF2-01-005         Close Test
        If ModuleNo = "1" Then
            sTNum = "LF1-01-005"
            LoopStepNo = 5
        Else
            sTNum = "LF2-01-001"
            LoopStepNo = 1
        End If
LF1_1_5: WriteMsg(SWITCH1, "CLOSE " & ModuleNo & ".0-4417")
        SError = SwitchingError(ModuleX)
        If SError <> 0 Then
            Echo(sTNum & "     1260 Switching Module Close Test error<" & CStr(SError) & "> in module" & ModuleNo)
            RegisterFailure(CStr(InstrumentToTest), sTNum, , , , , "1260 Switching Module Close Test error<" & CStr(SError) & "> in module" & ModuleNo)
            Switch1or2Proc = FAILED
            IncStepFailed()
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

        'LF1-01-005; LF2-01-005         Close Test
        If ModuleNo = "1" Then
            sTNum = "LF1-01-006"
        Else
            sTNum = "LF2-01-002"
        End If
        LoopStepNo += 1  '6 or 2
        frmSTest.proProgress.Value += 1

LF1_1_6: WriteMsg(SWITCH1, "OPEN " & ModuleNo & ".0-4417")
        SError = SwitchingError(ModuleX)
        If SError <> 0 Then
            Echo(sTNum & "     1260 Switching Module Open Test error<" & CStr(SError) & "> in module" & ModuleNo)
            RegisterFailure(CStr(InstrumentToTest), sTNum, , , , , "1260 Switching Module Open Test error<" & CStr(SError) & "> in module" & ModuleNo)
            Switch1or2Proc = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            Echo(FormatResults(True, "1260 Switching Module Open Test", sTNum))
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then
            GoTo LF1_1_6
        End If

        nSystErr = WriteMsg(SWITCH1, "CLOSE " & ModuleNo & ".0000-3") 'Closing Channels(1)-Channels(4)
        If InstrumentToTest = SWITCH1 Then
            ChannelNo = "1.0000-4"
            LastChannel = 5
Step1:
            S = "Use cable W24 (OBSERVE POLARITY) to connect" & vbCrLf & vbCrLf
            S &= "    DMM INPUT (HI/LO)   to   DC1 (HI/LO)." & vbCrLf & vbCrLf
            DisplaySetup(S, "TETS DC1_DIN.jpg", 1, True, 1, 2)
            If AbortTest = True Then GoTo testcomplete

            S = "Use cable W28 (OBSERVE POLARITY) to short  " & vbCrLf & vbCrLf
            S &= "   DC6 HI   to   DC6 LO."
            DisplaySetup(S, "TETS DC6_DC6.jpg", 1, True, 2, 2)
            If AbortTest = True Then GoTo testcomplete
            If GoBack = True Then GoTo Step1

            Channels(1) = "1.0000" 'S101-1,2(3,4)
            Channels(2) = "1.0001" 'S101-5,6(7,8)
            Channels(3) = "1.0002" 'S101-9,10(11,12)
            Channels(4) = "1.0003" 'S101-13,14(15,16)
            Channels(5) = "1.0004" 'S101-17,18(19,20)
        Else
            ChannelNo = "2.0000-3"
            LastChannel = 4
Step1b:
            S = "Use cable W24 (OBSERVE POLARITY) to connect" & vbCrLf & vbCrLf
            S &= "     DMM   INPUT (HI/LO)   to   DC10 (HI/LO)." & vbCrLf & vbCrLf
            DisplaySetup(S, "TETS DC10_DIN.jpg", 1, True, 1, 2)
            If AbortTest = True Then GoTo testcomplete

            S = "Use cable W28 (OBSERVE POLARITY) to short  " & vbCrLf & vbCrLf
            S &= "   DC6 HI   to   DC6 LO."
            DisplaySetup(S, "TETS DC6_DC6.jpg", 1, True, 2, 2)
            If AbortTest = True Then GoTo testcomplete
            If GoBack = True Then GoTo Step1b

            Channels(1) = "2.0003" 'S101-33,34(35,36)
            Channels(2) = "2.0002" 'S101-29,30(31,32)
            Channels(3) = "2.0001" 'S101-25,26(27,28)
            Channels(4) = "2.0000" 'S101-21,22(23,24)
        End If


        nSystErr = WriteMsg(SWITCH1, "CLOSE " & ChannelNo)
                nSystErr = WriteMsg(DMM, "*RST")
                nSystErr = WriteMsg(DMM, "*CLS")
        nSystErr = WriteMsg(DMM, "SENS:RES:APER MIN")

        LoopStepNo += 1 ' 7 or 3
        iTestStep = 2
        iSubStep = 1
        frmSTest.proProgress.Value += 1

LF1_2_1:  'LFx-02-001     S101 Closed
        sMsg = "Switch Channel " & ChannelNo & " Closed Test"
        sSwitchCallout = "Failed Switch Connections:  " & FnSwitchName(ChannelNo)
        TestMin = dW24_Resistance - DMM_TOL
        If TestMin < 0 Then TestMin = 0
        TestMax = 10 + dW24_Resistance + DMM_TOL
        If Not ClosureDetected(FnSwitchName(ChannelNo), TestMin, TestMax, "LF" & ModuleNo, InstrumentToTest, sSwitchCallout) Then
            Echo(FormatResults(False, sMsg, sTNum))
            Switch1or2Proc = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then
            GoTo LF1_2_1
        End If

        LoopStepNo += 1 ' 8 or 4
        frmSTest.proProgress.Value += 1
        iTestStep = 2
        iSubStep = 2
        For Channel = 1 To LastChannel ' LF1=8-12 or LF2=4-7
            'LFx-02-002 ~ 006    S101 Open
            sTNum = "LF" & ModuleNo & "-02-" & Format(iSubStep, "000")
LF1_2_2:    nSystErr = WriteMsg(SWITCH1, "OPEN " & Channels(Channel))
            Delay(0.5)
            nSystErr = WriteMsg(DMM, "MEAS:RES?")
            nSystErr = WaitForResponse(DMM, 0)
            retval = ReadMsg(DMM, StrMeas)
            Delay(1)
            nSystErr = WriteMsg(DMM, "MEAS:RES?")
            nSystErr = WaitForResponse(DMM, 0)
            retval = ReadMsg(DMM, StrMeas)
            nSystErr = WriteMsg(SWITCH1, "YERR")
            SelfTest = ReadMsg(SWITCH1, StrMeasurement)
            ValMesurement = Right(StrMeasurement, 6)

            If Val(StrMeas) < 500.0# Or SelfTest <> 0 Or retval <> 0 Or ValMesurement <> "000.00" Then
                'Code changed to take into account the bleed off resistor in the reciever being included
                'in the circuit that is measured. Per ECO 3047-432 fixes TDR-00A165. Jay Joiner 06/08/00.
                Echo(sTNum)
                sMsg = "Switch Channel " & Channels(Channel) & " Open Test"
                sSwitchCallout = "Failed Switch Connections:  " & FnSwitchName(Channels(Channel))
                FormatResultLine(sMsg & "  Measured: " & StripNullCharacters(StrMeas), False)
                Echo(sSwitchCallout)
                RegisterFailure(CStr(InstrumentToTest), sTNum, CDbl(StrMeas), "Ohms", 0, 500, sMsg & vbCrLf & sSwitchCallout)
                Switch1or2Proc = FAILED
                IncStepFailed()
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else
                Echo(FormatResults(True, FnSwitchName(Channels(Channel)) & " Open Test", sTNum))
                IncStepPassed()
            End If
            Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete
            If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then
                GoTo LF1_2_2
            End If
            nSystErr = WriteMsg(SWITCH1, "CLOSE " & Channels(Channel))
            LoopStepNo += 1
            iSubStep += 1
            frmSTest.proProgress.Value += 1
        Next Channel

        nSystErr = WriteMsg(SWITCH1, "OPEN " & ModuleNo & ".0-4417")
        Application.DoEvents()

        'S400's and S600's Test
        'LFx-03-001 - 021    Open/Closed
        'LoopStepNo = 13 or 8, only incremented from here down
        iTestStep = 3
        iSubStep = 1
        If InstrumentToTest = SWITCH1 Then
            S = "Move one end of cable W24 (OBSERVE POLARITY) from" & vbCrLf & vbCrLf
            S &= "    DC1 (HI/LO)   to   UTILITY 1-WIRE   (HI/LO)."
            DisplaySetup(S, "TETS DIN_U1W.jpg", 1) 'use the same jpg file as Switch1
            If AbortTest = True Then GoTo testcomplete
            LogicName1 = "S401-1, S401-3, "
            LogicName2 = "S402-3, S402-1"
        Else
            S = "Move one end of cable W24 (OBSERVE POLARITY) from" & vbCrLf & vbCrLf
            S &= "    DC10 (HI/LO)   to   UTILITY 1-WIRE   (HI/LO)."
            DisplaySetup(S, "TETS DIN_U1W.jpg", 1) 'use the same jpg file as Switch1
            If AbortTest = True Then GoTo testcomplete
            LogicName1 = "S404-1, S404-3, "
            LogicName2 = "S405-3, S405-1"
        End If

        'StepNo LF1-03-001 to 
        'Test LF1 S401-1,2,S402-1,2, S401-1,3,S402-1,3, S601-S606 open and closed
        'or
        'Test LF2 S404-1,2,S405-1,2, S404-1,3,S405-1,3, S607-S612 open and closed
        For Channel = 0 To 4
            Select Case Channel
                Case 0
                    FirstPair_1 = ModuleNo & ".3000"  'S401-1,2 or S404-1,2
                    FirstPair_2 = ModuleNo & ".3100"  'S402-1,2 or S405-1,2
                    SecondPair_1 = ""
                    SecondPair_2 = ""
                Case 1
                    FirstPair_1 = ModuleNo & ".3001" 'S401-1,3 or S404-1,3
                    FirstPair_2 = ModuleNo & ".3101" 'S402-1,3 or S405-1,3
                    SecondPair_1 = ModuleNo & ".2000"  'S601-1,2 or S607-1,2
                    SecondPair_2 = ModuleNo & ".2500"  'S606-1,2 or S612-1,2
                Case 2
                    FirstPair_1 = ModuleNo & ".2001" 'S601-1,3 or S607-1,3
                    FirstPair_2 = ModuleNo & ".2501" 'S606-1,3 or S612-1,3
                    SecondPair_1 = ModuleNo & ".2100"  'S602-1,2 or S608-1,2
                    SecondPair_2 = ModuleNo & ".2400"  'S605-1,2 or S611-1,2
                Case 3
                    FirstPair_1 = ModuleNo & ".2101" 'S602-1,3 or S608-1,3
                    FirstPair_2 = ModuleNo & ".2401" 'S605-1,3 or S611-1,3
                    SecondPair_1 = ModuleNo & ".2200"  'S603-1,2 or S609-1,2
                    SecondPair_2 = ModuleNo & ".2300"  'S604-1,2 or S610-1,2
                Case 4
                    FirstPair_1 = ModuleNo & ".2201" 'S603-1,3 or S609-1,3
                    FirstPair_2 = ModuleNo & ".2301" 'S604-1,3 or S610-1,3
                    SecondPair_1 = ""
                    SecondPair_2 = ""

            End Select

LF1_3_1:    nSystErr = WriteMsg(SWITCH1, "CLOSE " & FirstPair_1)
            nSystErr = WriteMsg(SWITCH1, "CLOSE " & FirstPair_2)
            If SecondPair_1 <> "" Then
                nSystErr = WriteMsg(SWITCH1, "CLOSE " & SecondPair_1)
                nSystErr = WriteMsg(SWITCH1, "CLOSE " & SecondPair_2)
            End If

            'DJoiner 03/15/02
            'LFx-03-001,4,9,14,19
            If SecondPair_1 = "" Then
                sSwitchCallout = "Failed Switch Connections: " & LogicName1 & FnSwitchName(FirstPair_1 & "," & Right(FirstPair_2, 4))
                CloseTestName = FirstPair_1 & ", " & FirstPair_2
            Else
                sSwitchCallout = "Failed Switch Connections: " & LogicName1 & FnSwitchName(FirstPair_1 & "," & Right(FirstPair_2, 4)) & FnSwitchName(SecondPair_1 & "," & Right(SecondPair_2, 4))
                CloseTestName = FirstPair_1 & ", " & FirstPair_2 & ", " & SecondPair_1 & ", " & SecondPair_2
            End If

            ' note: ClosureDetected updates sTNum
            TestMin = dW24_Resistance - DMM_TOL
            If TestMin < 0 Then TestMin = 0
            TestMax = 3 * (Channel + 1) + dW24_Resistance + DMM_TOL
            If Not ClosureDetected(FnSwitchName(CloseTestName), TestMin, TestMax, "LF" & ModuleNo, InstrumentToTest, sSwitchCallout) Then
                'The path resistance is directly proportional to the Channel, each channel is
                '3 Ohms more than its previous channel.
                Echo("Channel Closing Test Failed:  Channel No: " & CloseTestName)
                Switch1or2Proc = FAILED
                IncStepFailed()
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else
                IncStepPassed()
            End If
            Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete
            If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then ' LFx-03-001,004,007,010
                GoTo LF1_3_1
            End If
            iSubStep += 1
            LoopStepNo += 1
            frmSTest.proProgress.Value += 1

            'Test each switch in open position, one open switch at a time

            J = 3
            If SecondPair_1 = "" Then J = 1

            'LFx-03-002-3, 5-8, 10-13, 15-18,20-21
            For i = 0 To J ' tests for each switch open up to 4 switches
LF1_3_2:        Select Case i
                    Case 0 : nSystErr = WriteMsg(SWITCH1, "OPEN " & FirstPair_1) : ChannelNo = FirstPair_1
                    Case 1 : nSystErr = WriteMsg(SWITCH1, "OPEN " & FirstPair_2) : ChannelNo = FirstPair_2
                    Case 2 : nSystErr = WriteMsg(SWITCH1, "OPEN " & SecondPair_1) : ChannelNo = SecondPair_1
                    Case 3 : nSystErr = WriteMsg(SWITCH1, "OPEN " & SecondPair_2) : ChannelNo = SecondPair_2
                End Select

                If Not OpenDetected(FnSwitchName(ChannelNo), "LF" & ModuleNo) Then ' note: OpenDetected updates sTNum
                    sMsg = "Channel Opening Test Failed:  Channel No: " & ChannelNo
                    sSwitchCallout = "Failed Switch Connections:  " & FnSwitchName(ChannelNo)
                    Echo(sMsg)
                    RegisterFailure(CStr(InstrumentToTest), sTNum, dReturnResistance, "Ohms", 2000000, 9.9E+37, sMsg & vbCrLf & sSwitchCallout)
                    Switch1or2Proc = FAILED
                    IncStepFailed()
                    If OptionFaultMode = SOFmode Then GoTo TestComplete
                Else
                    IncStepPassed()
                End If
                Application.DoEvents()
                If AbortTest = True Then GoTo TestComplete
                If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then
                    GoTo LF1_3_2
                End If
                Select Case i
                    Case 0 : nSystErr = WriteMsg(SWITCH1, "CLOSE " & FirstPair_1)
                    Case 1 : nSystErr = WriteMsg(SWITCH1, "CLOSE " & FirstPair_2)
                    Case 2 : nSystErr = WriteMsg(SWITCH1, "CLOSE " & SecondPair_1)
                    Case 3 : nSystErr = WriteMsg(SWITCH1, "CLOSE " & SecondPair_2)
                End Select
                iSubStep += 1
                LoopStepNo += 1
                frmSTest.proProgress.Value += 1
skipOpenTest:
            Next i
            If SecondPair_1 = "" Then
                nSystErr = WriteMsg(SWITCH1, "OPEN " & FirstPair_1)
                nSystErr = WriteMsg(SWITCH1, "OPEN " & FirstPair_2)
            Else
                nSystErr = WriteMsg(SWITCH1, "OPEN " & SecondPair_1)
                nSystErr = WriteMsg(SWITCH1, "OPEN " & SecondPair_2)
            End If

        Next Channel

        'Open all previous S401,S402,S601-S606 channels
        nSystErr = WriteMsg(SWITCH1, "OPEN " & ModuleNo & ".3000-3003,3100-3103,2000-2501")

        'Setup to test the S301.1,2-95,96
        'S401 S402 and S301 Test
        If InstrumentToTest = SWITCH1 Then
            LogicName1 = "S401-1, S401-4, "
            LogicName2 = "S402-4, S402-1"
            SwitchTestName = "S301-1,2 to S301-47,48"
        Else
            LogicName1 = "S404-1, S404-4, "
            LogicName2 = "S405-4, S405-1"
            SwitchTestName = "S301-49,50 to S301-95,96"
        End If

        iStepCounter = 1
        iTestStep = 4
        iSubStep = 1

        'Test S301-1,2 to 95,96 or S301-97,98 to 191,192
        'First, Measure switchpathloss with all S301 relays closed, then open, S401-1,4, S402-1,4 and all S301 switches
LF1_4_1: nSystErr = WriteMsg(SWITCH1, "CLOSE " & ModuleNo & ".3002,3102") ' (S401-1,4 and S402-1,4) or (S404-1,4 and S405-1,4)
        nSystErr = WriteMsg(SWITCH1, "CLOSE " & ModuleNo & ".1000-1047") '  (S301-1,2 to S301-95,96) or (S301-97,98 to S301-191,192)
        Delay(1)
        nSystErr = WriteMsg(DMM, "MEAS:RES? 50,MAX")
        nSystErr = WaitForResponse(DMM, 0)
        retval = ReadMsg(DMM, StrMeas)
        If IsNumeric(StrMeas) Then
            dSwitchPathLoss = CSng(StrMeas)
        Else
            dSwitchPathLoss = 0
        End If

        'Test Closed switch impedance
        sSwitchCallout = "Failed Switch Connections:  " & LogicName1 & SwitchTestName & ", " & LogicName2
        TestMin = dW24_Resistance - DMM_TOL
        If TestMin < 0 Then TestMin = 0
        TestMax = 3 + dW24_Resistance + DMM_TOL
        If Not ClosureDetected(FnSwitchName(ModuleNo & ".1000-1047"), TestMin, TestMax, "LF" & ModuleNo, InstrumentToTest, sSwitchCallout, 0) Then
            '(S401, S402) or (S404, S405) is 3 Ohms, and S301=1.5 Ohms
            Echo("Channel Closing Test Failed:  Channel No: " & ModuleNo & ".1000-1047")
            Switch1or2Proc = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then ' LFx-03-21, 23,,,115
            GoTo LF1_4_1
        End If

        iSubStep += 1
        LoopStepNo += 1
        frmSTest.proProgress.Value += 1

        'Test S401-1,4 and S402-1,4 open
        For Channel = 0 To 1
LF1_4_2:    If Channel = 1 Then
                ChannelNo = "3002" 'S401-1,4 or S404-1,4
                nSystErr = WriteMsg(SWITCH1, "OPEN " & ModuleNo & ".3002")
            Else
                ChannelNo = "3102" ' S402-1,4 or S405-1,2
                nSystErr = WriteMsg(SWITCH1, "OPEN " & ModuleNo & ".3102")
            End If

            'LFx-03-117 ~ LFx-03-118
            If Not OpenDetected(FnSwitchName(ModuleNo & "." & ChannelNo), "LF" & ModuleNo) Then
                sMsg = "Channel Opening Test Failed:  Channel No: " & ModuleNo & "." & ChannelNo
                sSwitchCallout = "Failed Switch Connections:  " & LogicName1 & FnSwitchName(ModuleNo & "." & ChannelNo) & LogicName2
                Echo(sMsg)
                RegisterFailure(CStr(InstrumentToTest), sTNum, dReturnResistance, "Ohms", 2000000, 9.9E+37, sMsg & vbCrLf & sSwitchCallout)
                Switch1or2Proc = FAILED
                IncStepFailed()
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else
                IncStepPassed()
            End If
            Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete
            If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then ' LFx-03-117,118
                GoTo LF1_4_2
            End If
            nSystErr = WriteMsg(SWITCH1, "CLOSE " & ModuleNo & ".3002,3102") '  S401-1,2,S402-1,4 or S404-1,4,S405-1,2
            iSubStep += 1
            LoopStepNo += 1
            frmSTest.proProgress.Value += 1
        Next Channel

        'Test all S301 switches are open
LF1_4_3: nSystErr = WriteMsg(SWITCH1, "OPEN " & ModuleNo & ".1000-1047") ' S301-1,2 to S301-95,96' Measure switchpathloss
        Delay(1)
        If Not OpenDetected(FnSwitchName(ModuleNo & ".1000-1047"), "LF" & ModuleNo) Then
            sMsg = "Channel Opening Test Failed:  Channel No: " & ModuleNo & ".1000-1047, " & SwitchTestName
            sSwitchCallout = "Failed Switch Connections:  " & LogicName1 & SwitchTestName & ", " & LogicName2
            Echo(sMsg)
            RegisterFailure(CStr(InstrumentToTest), sTNum, dReturnResistance, "Ohms", 2000000, 9.9E+37, sMsg & vbCrLf & sSwitchCallout)
            Switch1or2Proc = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then ' LFx-03-21, 23,,,115
            GoTo LF1_4_3
        End If

        iSubStep += 1
        LoopStepNo += 1
        frmSTest.proProgress.Value += 1

        'Test each S301 switch closed one at a time
        For Channel = 1000 To 1047
LF1_4_4:    nSystErr = WriteMsg(SWITCH1, "CLOSE " & ModuleNo & "." & CStr(Channel))

            'LFx-03-021 ~ LFx-03-115  (Odd)
            'DJoiner 03/15/02
            sSwitchCallout = "Failed Switch Connections:  " & LogicName1 & FnSwitchName(ModuleNo & "." & CStr(Channel)) & ", " & LogicName2
            TestMin = dW24_Resistance - DMM_TOL
            If TestMin < 0 Then TestMin = 0
            TestMax = 3 + dW24_Resistance + DMM_TOL
            If Not ClosureDetected(FnSwitchName(ModuleNo & "." & CStr(Channel)), TestMin, TestMax, "LF" & ModuleNo, InstrumentToTest, sSwitchCallout, dSwitchPathLoss) Then
                '(S401, S402) or (S404, S405) is 3 Ohms, and S301=1.5 Ohms
                Echo("Channel Closing Test Failed:  Channel No: " & ModuleNo & "." & CStr(Channel))
                Switch1or2Proc = FAILED
                IncStepFailed()
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else
                IncStepPassed()
            End If
            Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete
            If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then ' LFx-03-21, 23,,,115
                GoTo LF1_4_4
            End If

            iSubStep += 1
            LoopStepNo += 1
            frmSTest.proProgress.Value += 1
            nSystErr = WriteMsg(SWITCH1, "OPEN " & ModuleNo & "." & CStr(Channel))

        Next Channel



        '******************************************************************

        'S501-S505 or S506-S510 Tests
        nSystErr = WriteMsg(SWITCH1, "RESET")
        Delay(2)
        'S401, S402, & S501
        If InstrumentToTest = SWITCH1 Then
            LogicName1 = "S401-1, S401-5, "
            LogicName2 = "S402-5, S402-1"
        Else
            LogicName1 = "S404-1, S404-5, "
            LogicName2 = "S405-5, S405-1"
        End If

        nSystErr = WriteMsg(SWITCH1, "CLOSE " & ModuleNo & ".3003,3103") 'S401-1,5 and S402-1,5 or S404-1,5 and S405-1,5

        iStepCounter = 1
        iTestStep = 5
        iSubStep = 1

        For Channel = 0 To 1
LF1_5_1:    If Channel = 0 Then
                FirstChannel = ModuleNo & ".4000"  'S501-1,3
                SecondChannel = ModuleNo & ".4011" 'S501-2,4
            Else
                FirstChannel = ModuleNo & ".4001"  'S501-1,4
                SecondChannel = ModuleNo & ".4010" 'S501-2,3
            End If

            nSystErr = WriteMsg(SWITCH1, "CLOSE " & FirstChannel)
            nSystErr = WriteMsg(SWITCH1, "CLOSE " & SecondChannel)

            'LFx-03-119; LFx-03-122
            sSwitchCallout = "Failed Switch Connections:  " & LogicName1 & FnSwitchName(FirstChannel & "," & Right(SecondChannel, 4)) & LogicName2
            TestMin = dW24_Resistance - DMM_TOL
            If TestMin < 0 Then TestMin = 0
            TestMax = 6 + dW24_Resistance + DMM_TOL
            If Not ClosureDetected(FnSwitchName(FirstChannel & ", " & SecondChannel), TestMin, TestMax, "LF" & ModuleNo, InstrumentToTest, sSwitchCallout) Then
                Echo("Channel Closing Test Failed:  Channel No: " & FirstChannel & ", " & SecondChannel)
                Switch1or2Proc = FAILED
                IncStepFailed()
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else
                IncStepPassed()
            End If
            Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete
            If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then ' LFx-03-119, 122
                GoTo LF1_5_1
            End If
            iSubStep += 1
            LoopStepNo += 1
            frmSTest.proProgress.Value += 1

            For i = 0 To 1
                If i = 0 Then
                    nSystErr = WriteMsg(SWITCH1, "OPEN " & FirstChannel)
                    ChannelNo = FirstChannel
                Else
                    nSystErr = WriteMsg(SWITCH1, "OPEN " & SecondChannel)
                    ChannelNo = SecondChannel
                End If

                'LFx-03-120, 121, 123, 124
LF1_5_2:        If Not OpenDetected(FnSwitchName(ChannelNo), "LF" & ModuleNo) Then
                    sMsg = "Channel Opening Test Failed:  Channel No: " & ChannelNo
                    sMsg = "Failed Switch Connections:  " & LogicName1 & FnSwitchName(ChannelNo) & LogicName2
                    Echo(sMsg)
                    RegisterFailure(CStr(InstrumentToTest), sTNum, dReturnResistance, "Ohms", 2000000, 9.9E+37, sMsg & vbCrLf & sSwitchCallout)
                    Switch1or2Proc = FAILED
                    IncStepFailed()
                    If OptionFaultMode = SOFmode Then GoTo TestComplete
                Else
                    IncStepPassed()
                End If
                Application.DoEvents()
                If AbortTest = True Then GoTo TestComplete
                If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then ' LFx-03-120,121, 123,124
                    GoTo LF1_5_2
                End If
                If i = 0 Then
                    nSystErr = WriteMsg(SWITCH1, "CLOSE " & FirstChannel)
                Else
                    nSystErr = WriteMsg(SWITCH1, "OPEN " & FirstChannel)
                End If
                iSubStep += 1
                LoopStepNo += 1
                frmSTest.proProgress.Value += 1
            Next i
        Next Channel

        nSystErr = WriteMsg(SWITCH1, "CLOSE " & FirstChannel)
        nSystErr = WriteMsg(SWITCH1, "CLOSE " & SecondChannel)

        For Channel = 0 To 1
LF1_5_3:    If Channel = 0 Then
                nSystErr = WriteMsg(SWITCH1, "OPEN " & ModuleNo & ".3003")
                ChannelNo = ModuleNo & ".3003" 'S401-1,5 or S404-1,5
            Else
                nSystErr = WriteMsg(SWITCH1, "OPEN " & ModuleNo & ".3103")
                ChannelNo = ModuleNo & ".3103" 'S402-1,5 or S405-1,5
            End If

            'LFx-03-125, 126
            If Not OpenDetected(FnSwitchName(ChannelNo), "LF" & ModuleNo) Then
                sMsg = "Channel Opening Test Failed:  Channel No: " & ChannelNo
                sSwitchCallout = "Failed Switch Connections:  " & LogicName1 & FnSwitchName(ChannelNo) & LogicName2
                Echo(sMsg)
                RegisterFailure(CStr(InstrumentToTest), sTNum, dReturnResistance, "Ohms", 2000000, 9.9E+37, sMsg & vbCrLf & sSwitchCallout)
                Switch1or2Proc = FAILED
                IncStepFailed()
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else
                IncStepPassed()
            End If
            Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete
            If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then 'LFx-03-125,126
                GoTo LF1_5_3
            End If
            nSystErr = WriteMsg(SWITCH1, "CLOSE " & ChannelNo)

            If Channel = 1 Then
                nSystErr = WriteMsg(SWITCH1, "OPEN " & ModuleNo & ".4001,4010")
            End If
            iSubStep += 1
            LoopStepNo += 1
            frmSTest.proProgress.Value += 1
        Next Channel

        '=============================================================================================
        'ModuleNo.3003 and ModuleNo.3103 are remained closed
        '=============================================================================================
        'S401-1, S402, S501, S505

        For i = 0 To 1
            If i = 0 Then
                nSystErr = WriteMsg(SWITCH1, "CLOSE " & ModuleNo & ".4002,4013")    'S501-1,5 and S501-2,6 or S506-1,5 and S506-2,6
            Else
                nSystErr = WriteMsg(SWITCH1, "OPEN " & ModuleNo & ".4002,4013")     ' "
                nSystErr = WriteMsg(SWITCH1, "CLOSE " & ModuleNo & ".4003,4012")    'S501-1,6 and S501-2,5 or S506-1,6 and S506-2,5
            End If

            For Channel = 0 To 3

                If i = 0 Then
                    Select Case Channel
                        Case 0
                            FirstChannel = ModuleNo & ".4400" ' LFx-03-127  'S505-1,3 or S510-1,3
                            SecondChannel = ModuleNo & ".4411"              'S505-2,4 or S510-2,4

                        Case 1
                            FirstChannel = ModuleNo & ".4402" ' LFx-03-130  'S505-1,5 or
                            SecondChannel = ModuleNo & ".4413"              'S505-2,6 or 

                        Case 2
                            FirstChannel = ModuleNo & ".4404" ' LFx-03-133  'S505-1,7 or
                            SecondChannel = ModuleNo & ".4415"              'S505-2,8 or 

                        Case Else
                            FirstChannel = ModuleNo & ".4406" ' LFx-03-136  'S505-1,9 or
                            SecondChannel = ModuleNo & ".4417"              'S505-2,10 or

                    End Select
                Else
                    Select Case Channel
                        Case 0
                            FirstChannel = ModuleNo & ".4401" ' LFx-03-139  'S505-1,4 or
                            SecondChannel = ModuleNo & ".4410"              'S505-2,3 or
                        Case 1
                            FirstChannel = ModuleNo & ".4403" ' LFx-03-142  'S505-1,6 or
                            SecondChannel = ModuleNo & ".4412"              'S505-2,5 or

                        Case 2
                            FirstChannel = ModuleNo & ".4405" ' LFx-03-145  'S505-1,8 or
                            SecondChannel = ModuleNo & ".4414"              'S505-2,7 or

                        Case Else
                            FirstChannel = ModuleNo & ".4407" ' LFx-03-148  'S505-1,10 or 
                            SecondChannel = ModuleNo & ".4416"              'S505-2,9 or
                    End Select
                End If


LF1_5_4:        nSystErr = WriteMsg(SWITCH1, "CLOSE " & FirstChannel)
                nSystErr = WriteMsg(SWITCH1, "CLOSE " & SecondChannel)

                'LFx-03-127, 130, 133, 136, 139, 142, 145, 148
                sSwitchCallout = "Failed Switch Connections:  " & LogicName1 & FnSwitchName(FirstChannel & "," & Right(SecondChannel, 4)) & LogicName2
                TestMin = dW24_Resistance - DMM_TOL
                If TestMin < 0 Then TestMin = 0
                TestMax = 9 + dW24_Resistance + DMM_TOL
                If Not ClosureDetected(FnSwitchName(FirstChannel & ", " & SecondChannel), TestMin, TestMax, "LF" & ModuleNo, InstrumentToTest, sSwitchCallout) Then 'finished
                    Echo("Channel Closing Test Failed:  Channel No: " & FirstChannel & ", " & SecondChannel)
                    Switch1or2Proc = FAILED
                    IncStepFailed()
                    If OptionFaultMode = SOFmode Then GoTo TestComplete
                Else
                    IncStepPassed()
                End If
                Application.DoEvents()
                If AbortTest = True Then GoTo TestComplete
                If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then ' LFx-03-127, 130, 133, 136,   139, 142, 145, 148
                    GoTo LF1_5_4
                End If
                iSubStep += 1
                LoopStepNo += 1
                frmSTest.proProgress.Value += 1


                For J = 0 To 1
                    If J = 0 Then
                        ChannelOpen = FirstChannel
                    Else
                        ChannelOpen = SecondChannel
                    End If

LF1_5_5:            nSystErr = WriteMsg(SWITCH1, "OPEN " & ChannelOpen)

                    'LFx-03-128, 129, 131, 132, 134, 135, 137, 138, 140, 141, 143, 144, 146, 147, 149, 150
                    If Not OpenDetected(FnSwitchName(ChannelOpen), "LF" & ModuleNo) Then
                        sMsg = "Channel Opening Test Failed:  Channel No: " & ChannelOpen
                        sSwitchCallout = "Failed Switch Connections:  " & LogicName1 & FnSwitchName(ChannelOpen) & LogicName2
                        Echo(sMsg)
                        RegisterFailure(CStr(InstrumentToTest), sTNum, dReturnResistance, "Ohms", 2000000, 9.9E+37, sMsg & vbCrLf & sSwitchCallout)
                        Switch1or2Proc = FAILED
                        IncStepFailed()
                        If OptionFaultMode = SOFmode Then GoTo TestComplete
                    Else
                        IncStepPassed()
                    End If
                    nSystErr = WriteMsg(SWITCH1, "CLOSE " & ChannelOpen)
                    Application.DoEvents()
                    If AbortTest = True Then GoTo TestComplete
                    If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then ' LFx-03-128,129, 131,132, 134,135, 137,138,   140,141, 143,144, 146,147, 149,150
                        GoTo LF1_5_5
                    End If
                    iSubStep += 1
                    LoopStepNo += 1
                    frmSTest.proProgress.Value += 1
                Next J

                nSystErr = WriteMsg(SWITCH1, "OPEN " & FirstChannel)
                nSystErr = WriteMsg(SWITCH1, "OPEN " & SecondChannel)
            Next Channel
        Next i


        'Now, test channel, 4002, 4013, 4003, and 4012.  At this point, 4003, and 4012 are remained closed.
        nSystErr = WriteMsg(SWITCH1, "CLOSE " & ModuleNo & ".4400,4411") 'S505-1,3 and S505-2,4 or S510-1,3 and S510-2,4

        For i = 0 To 1
            If i = 0 Then
                FirstChannel = ModuleNo & ".4003" ' LFx-03-151  'S501-1,6 or 
                SecondChannel = ModuleNo & ".4012" ' LFx-03-152 'S501,2,5 or
            Else
                FirstChannel = ModuleNo & ".4002" ' LFx-03-153  'S501-1,5 or
                SecondChannel = ModuleNo & ".4013" ' LFx-03-154 'S501-2,6 or
            End If

            nSystErr = WriteMsg(SWITCH1, "CLOSE " & FirstChannel)
            nSystErr = WriteMsg(SWITCH1, "CLOSE " & SecondChannel)
            For J = 0 To 1
LF1_5_6:        If J = 0 Then
                    nSystErr = WriteMsg(SWITCH1, "OPEN " & FirstChannel)
                    ChannelNo = FirstChannel
                Else
                    nSystErr = WriteMsg(SWITCH1, "CLOSE " & FirstChannel)
                    nSystErr = WriteMsg(SWITCH1, "OPEN " & SecondChannel)
                    ChannelNo = SecondChannel
                End If

                'LFx-03-151 ~ LFx-03-154
                If Not OpenDetected(FnSwitchName(ChannelNo), "LF" & ModuleNo) Then
                    sMsg = "Channel Opening Test Failed:  Channel No: " & ChannelNo
                    sSwitchCallout = "Failed Switch Connections:  " & LogicName1 & FnSwitchName(ChannelNo) & LogicName2
                    Echo(sMsg)
                    RegisterFailure(CStr(InstrumentToTest), sTNum, dReturnResistance, "Ohms", 2000000, 9.9E+37, sMsg & vbCrLf & sSwitchCallout)
                    Switch1or2Proc = FAILED
                    IncStepFailed()
                    If OptionFaultMode = SOFmode Then GoTo TestComplete
                Else
                    IncStepPassed()
                End If
                Application.DoEvents()
                If AbortTest = True Then GoTo TestComplete
                If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then 'LFx-03-151,152,  153,154
                    GoTo LF1_5_6
                End If
                iSubStep += 1
                LoopStepNo += 1
                frmSTest.proProgress.Value += 1
            Next J
            nSystErr = WriteMsg(SWITCH1, "OPEN " & FirstChannel)
            nSystErr = WriteMsg(SWITCH1, "OPEN " & SecondChannel)
        Next i
        nSystErr = WriteMsg(SWITCH1, "OPEN " & ModuleNo & ".4400,4411")

        'S401, S501, S504, S403 Test
        For i = 0 To 1
            If i = 0 Then
                S501_1 = ModuleNo & ".4004" 'S501-1,7 or
                S501_2 = ModuleNo & ".4015" 'S501-2,8 or 
                S504 = ModuleNo & ".4304"   'S504-1,7 or
            Else
                S501_1 = ModuleNo & ".4005" 'S501-1,8 or
                S501_2 = ModuleNo & ".4014" 'S501-2,7 or
                S504 = ModuleNo & ".4314"   'S504-2,7 or
            End If
            nSystErr = WriteMsg(SWITCH1, "CLOSE " & S501_1)
            nSystErr = WriteMsg(SWITCH1, "CLOSE " & S501_2)
            nSystErr = WriteMsg(SWITCH1, "CLOSE " & S504)

            For Channel = 0 To 3
                Select Case i
                    Case 0
                        Select Case Channel
                            Case 0
                                FirstChannel = ModuleNo & ".4310" ' LFx-03-155 'S504-2,3 or
                                SecondChannel = ModuleNo & ".3200"             'S403-1,2
                            Case 1
                                FirstChannel = ModuleNo & ".4311" ' LFx-03-158 'S504-2,4
                                SecondChannel = ModuleNo & ".3201"             'S403-1,3
                            Case 2
                                FirstChannel = ModuleNo & ".4312" ' LFx-03-161 'S504-2,5
                                SecondChannel = ModuleNo & ".3202"             'S403-1,4
                            Case 3
                                FirstChannel = ModuleNo & ".4313" ' LFx-03-164 'S504-2,6
                                SecondChannel = ModuleNo & ".3203"             'S403-1,5
                        End Select
                    Case 1
                        Select Case Channel
                            Case 0

                                FirstChannel = ModuleNo & ".4300"  'S504-1,3
                                SecondChannel = ModuleNo & ".3200" 'S403-1,2
                            Case 1
                                FirstChannel = ModuleNo & ".4301"  'S504-1,4
                                SecondChannel = ModuleNo & ".3201" 'S403-1,3
                            Case 2
                                FirstChannel = ModuleNo & ".4302"  'S504-1,5
                                SecondChannel = ModuleNo & ".3202" 'S403-1,4
                            Case 3
                                FirstChannel = ModuleNo & ".4303"  'S504-1,6
                                SecondChannel = ModuleNo & ".3203" 'S403-1,5
                        End Select
                End Select

LF1_5_7:        nSystErr = WriteMsg(SWITCH1, "CLOSE " & FirstChannel)
                nSystErr = WriteMsg(SWITCH1, "CLOSE " & SecondChannel)

                'LFx-03-155, 158, 161, 164,   170, 173, 176, 179
                sSwitchCallout = "Failed Switch Connections:  " & LogicName1 & FnSwitchName(FirstChannel & "," & Right(SecondChannel, 4)) & LogicName2
                TestMin = dW24_Resistance - DMM_TOL
                If TestMin < 0 Then TestMin = 0
                TestMax = 10.5 + dW24_Resistance + DMM_TOL
                If Not ClosureDetected(FnSwitchName(FirstChannel & ", " & SecondChannel), TestMin, TestMax, "LF" & ModuleNo, InstrumentToTest, sSwitchCallout) Then
                    Echo("Channel Closing Test Failed:  Channel No: " & FirstChannel & ", " & SecondChannel)
                    Switch1or2Proc = FAILED
                    IncStepFailed()
                    If OptionFaultMode = SOFmode Then GoTo TestComplete
                Else
                    IncStepPassed()
                End If
                Application.DoEvents()
                If AbortTest = True Then GoTo TestComplete
                If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then ' LFx-03-155, 158, 161, 164,   170, 173, 176, 179
                    GoTo LF1_5_7
                End If

                iSubStep += 1
                LoopStepNo += 1
                frmSTest.proProgress.Value += 1

LF1_5_8:        nSystErr = WriteMsg(SWITCH1, "OPEN " & FirstChannel)

                'LFx-03-156, 159, 162, 165, 171, 174, 177, 180
                If Not OpenDetected(FnSwitchName(FirstChannel), "LF" & ModuleNo) Then
                    sMsg = "Channel Opening Test Failed:  Channel No: " & FirstChannel
                    sSwitchCallout = "Failed Switch Connections:  " & LogicName1 & FnSwitchName(FirstChannel) & LogicName2
                    Echo(sMsg)
                    RegisterFailure(CStr(InstrumentToTest), sTNum, dReturnResistance, "Ohms", 2000000, 9.9E+37, sMsg & vbCrLf & sSwitchCallout)
                    Switch1or2Proc = FAILED
                    IncStepFailed()
                    If OptionFaultMode = SOFmode Then GoTo TestComplete
                Else
                    IncStepPassed()
                End If
                Application.DoEvents()
                If AbortTest = True Then GoTo TestComplete
                If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then 'LFx-03-156, 159, 162, 165,   171, 174, 177, 180
                    GoTo LF1_5_8
                End If

                iSubStep += 1
                LoopStepNo += 1
                frmSTest.proProgress.Value += 1

LF1_5_9:        nSystErr = WriteMsg(SWITCH1, "CLOSE " & FirstChannel)
                nSystErr = WriteMsg(SWITCH1, "OPEN " & SecondChannel)

                'LFx-03-157, 160, 163, 166, 172, 175, 178, 181
                If Not OpenDetected(FnSwitchName(SecondChannel), "LF" & ModuleNo) Then
                    sMsg = "Channel Opening Test Failed:  Channel No: " & SecondChannel
                    sSwitchCallout = "Failed Switch Connections:  " & LogicName1 & FnSwitchName(SecondChannel) & LogicName2
                    Echo(sMsg)
                    RegisterFailure(CStr(InstrumentToTest), sTNum, dReturnResistance, "Ohms", 2000000, 9.9E+37, sMsg & vbCrLf & sSwitchCallout)
                    Switch1or2Proc = FAILED
                    IncStepFailed()
                    If OptionFaultMode = SOFmode Then GoTo TestComplete
                Else
                    IncStepPassed()
                End If

                nSystErr = WriteMsg(SWITCH1, "OPEN " & FirstChannel)
                nSystErr = WriteMsg(SWITCH1, "OPEN " & SecondChannel)
                Application.DoEvents()
                If AbortTest = True Then GoTo TestComplete
                If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then 'LFx-03-157, 160, 163, 166,   172, 175, 178, 181
                    GoTo LF1_5_9
                End If
                iSubStep += 1
                LoopStepNo += 1
                frmSTest.proProgress.Value += 1
            Next Channel

LF1_5_10:   WriteMsg(SWITCH1, "OPEN " & S501_1)

            'LFx-03-167, 182
            If Not OpenDetected(FnSwitchName(S501_1), "LF" & ModuleNo) Then
                sMsg = "Channel Opening Test Failed:  Channel No: " & S501_1
                sSwitchCallout = "Failed Switch Connections:  " & LogicName1 & FnSwitchName(S501_1) & LogicName2
                Echo(sMsg)
                RegisterFailure(CStr(InstrumentToTest), sTNum, dReturnResistance, "Ohms", 2000000, 9.9E+37, sMsg & vbCrLf & sSwitchCallout)
                Switch1or2Proc = FAILED
                IncStepFailed()
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else
                IncStepPassed()
            End If
            Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete
            If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then 'LFx-03-167,182
                GoTo LF1_5_10
            End If

            iSubStep += 1
            LoopStepNo += 1
            frmSTest.proProgress.Value += 1

LF1_5_11:   WriteMsg(SWITCH1, "CLOSE " & S501_1)
            nSystErr = WriteMsg(SWITCH1, "OPEN " & S501_2)

            'LFx-03-168, 183
            If Not OpenDetected(FnSwitchName(S501_2), "LF" & ModuleNo) Then
                sMsg = "Channel Opening Test Failed:  Channel No: " & S501_2
                sSwitchCallout = "Failed Switch Connections:  " & LogicName1 & FnSwitchName(S501_2) & LogicName2
                Echo(sMsg)
                RegisterFailure(CStr(InstrumentToTest), sTNum, dReturnResistance, "Ohms", 2000000, 9.9E+37, sMsg & vbCrLf & sSwitchCallout)
                Switch1or2Proc = FAILED
                IncStepFailed()
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else
                IncStepPassed()
            End If
            Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete
            If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then 'LFx-03-168, 183
                GoTo LF1_5_11
            End If

            iSubStep += 1
            LoopStepNo += 1
            frmSTest.proProgress.Value += 1

LF1_5_12:   WriteMsg(SWITCH1, "CLOSE " & S501_2)
            nSystErr = WriteMsg(SWITCH1, "OPEN " & S504)

            'LFx-03-169, 184
            If Not OpenDetected(FnSwitchName(S504), "LF" & ModuleNo) Then
                sMsg = "Channel Opening Test Failed:  Channel No: " & S504
                sSwitchCallout = "Failed Switch Connections:  " & LogicName1 & FnSwitchName(S504) & LogicName2
                Echo(sMsg)
                RegisterFailure(CStr(InstrumentToTest), sTNum, dReturnResistance, "Ohms", 2000000, 9.9E+37, sMsg & vbCrLf & sSwitchCallout)
                Switch1or2Proc = FAILED
                IncStepFailed()
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else
                IncStepPassed()
            End If
            Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete
            If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then 'LFx-03-169, 184
                GoTo LF1_5_12
            End If
            nSystErr = WriteMsg(SWITCH1, "OPEN " & S501_1)
            nSystErr = WriteMsg(SWITCH1, "OPEN " & S501_2)
            nSystErr = WriteMsg(SWITCH1, "OPEN " & S504)
            iSubStep += 1
            LoopStepNo += 1
            frmSTest.proProgress.Value += 1
        Next i

        nSystErr = WriteMsg(SWITCH1, "CLOSE " & S501_1)
        nSystErr = WriteMsg(SWITCH1, "CLOSE " & S501_2)
        'LFx-03-185-193
        For Channel = 0 To 2
            Select Case Channel
                Case 0
                    FirstChannel = ModuleNo & ".4315"  'S504-2,8
                    SecondChannel = ModuleNo & ".4306" 'S504-1,9
                Case 1
                    FirstChannel = ModuleNo & ".4316"  'S504-2,9
                    SecondChannel = ModuleNo & ".4307" 'S504-1,10
                Case 2
                    FirstChannel = ModuleNo & ".4317"  'S504-2,10
                    SecondChannel = ModuleNo & ".4305" 'S504-1,8
            End Select

LF1_5_13:   WriteMsg(SWITCH1, "CLOSE " & FirstChannel)
            nSystErr = WriteMsg(SWITCH1, "CLOSE " & SecondChannel)

            'LFx-03-185, 188, 191
            sSwitchCallout = "Failed Switch Connections:  " & LogicName1 & FnSwitchName(FirstChannel & "," & Right(SecondChannel, 4)) & LogicName2
            TestMin = dW24_Resistance - DMM_TOL
            If TestMin < 0 Then TestMin = 0
            TestMax = 9 + dW24_Resistance + DMM_TOL
            If Not ClosureDetected(FnSwitchName(FirstChannel & ", " & SecondChannel), TestMin, TestMax, "LF" & ModuleNo, InstrumentToTest, sSwitchCallout) Then
                Echo("Channel Closing Test Failed:  Channel No: " & FirstChannel & ", " & SecondChannel)
                Switch1or2Proc = FAILED
                IncStepFailed()
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else
                IncStepPassed()
            End If
            Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete
            If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then 'LFx-03-185, 188, 191
                GoTo LF1_5_13
            End If

            iSubStep += 1
            LoopStepNo += 1
            frmSTest.proProgress.Value += 1

LF1_5_14:   WriteMsg(SWITCH1, "OPEN " & FirstChannel)

            'LFx-03-186, 189, 192
            If Not OpenDetected(FnSwitchName(FirstChannel), "LF" & ModuleNo) Then
                sMsg = "Channel Opening Test Failed:  Channel No: " & FirstChannel
                sSwitchCallout = "Failed Switch Connections:  " & LogicName1 & FnSwitchName(FirstChannel) & LogicName2
                Echo(sMsg)
                RegisterFailure(CStr(InstrumentToTest), sTNum, dReturnResistance, "Ohms", 2000000, 9.9E+37, sMsg & vbCrLf & sSwitchCallout)
                Switch1or2Proc = FAILED
                IncStepFailed()
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else
                IncStepPassed()
            End If
            Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete
            If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then 'LFx-03-186, 189, 192
                GoTo LF1_5_14
            End If

            iSubStep += 1
            LoopStepNo += 1
            frmSTest.proProgress.Value += 1

LF1_5_15:   WriteMsg(SWITCH1, "CLOSE " & FirstChannel)
            nSystErr = WriteMsg(SWITCH1, "OPEN " & SecondChannel)

            'LFx-03-187, 190, 193
            If Not OpenDetected(FnSwitchName(SecondChannel), "LF" & ModuleNo) Then
                sMsg = "Channel Opening Test Failed:  Channel No: " & SecondChannel
                sSwitchCallout = "Failed Switch Connections:  " & LogicName1 & FnSwitchName(SecondChannel) & LogicName2
                Echo(sMsg)
                RegisterFailure(CStr(InstrumentToTest), "LF" & ModuleNo & "-" & Format(iTestStep, "00") & "-000", dReturnResistance, "Ohms", 2000000, 9.9E+37, sMsg & vbCrLf & sSwitchCallout)
                Switch1or2Proc = FAILED
                IncStepFailed()
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else
                IncStepPassed()
            End If
            nSystErr = WriteMsg(SWITCH1, "OPEN " & FirstChannel)
            Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete
            If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then 'LFx-03-187, 190, 193
                GoTo LF1_5_15
            End If
            iSubStep += 1
            LoopStepNo += 1
            frmSTest.proProgress.Value += 1
        Next Channel

        nSystErr = WriteMsg(SWITCH1, "OPEN " & S501_1)
        nSystErr = WriteMsg(SWITCH1, "OPEN " & S501_2)
        'LFx-03-194-

        'S401, S501, S502, S503, S402
        nSystErr = WriteMsg(SWITCH1, "CLOSE " & ModuleNo & ".4006,4017")

        For Channel = 0 To 2
            Select Case Channel
                Case 0
                    FirstPair_1 = ModuleNo & ".4100" ' LFx-03-194 ' S502-1,3
                    FirstPair_2 = ModuleNo & ".4111"              ' S502-2,4
                    SecondPair_1 = ModuleNo & ".4101" ' LFx-03-197' S502-1,4
                    SecondPair_2 = ModuleNo & ".4110"             ' S502-2,3
                Case 1
                    FirstPair_1 = ModuleNo & ".4102" ' LFx-03-200 ' S502-1,5
                    FirstPair_2 = ModuleNo & ".4113"              ' S502-2,6
                    SecondPair_1 = ModuleNo & ".4103" ' LFx-03-203' S502-1,6
                    SecondPair_2 = ModuleNo & ".4112"             ' S502-2,5
                Case Else
                    FirstPair_1 = ModuleNo & ".4104" ' LFx-03-206 ' S502-1,7
                    FirstPair_2 = ModuleNo & ".4115"              ' S502-2,8
                    SecondPair_1 = ModuleNo & ".4105" ' LFx-03-209' S502-1,8
                    SecondPair_2 = ModuleNo & ".4114"             ' S502-2,7
            End Select

            For Repeat = 0 To 1
                If Repeat = 1 Then
                    FirstPair_1 = SecondPair_1
                    FirstPair_2 = SecondPair_2
                End If
LF1_5_16:       nSystErr = WriteMsg(SWITCH1, "CLOSE " & FirstPair_1)
                nSystErr = WriteMsg(SWITCH1, "CLOSE " & FirstPair_2)

                'LFx-03-194,197, 200,203, 206,209
                sSwitchCallout = "Failed Switch Connections:  " & LogicName1 & FnSwitchName(FirstPair_1 & "," & Right(FirstPair_2, 4)) & LogicName2
                TestMin = dW24_Resistance - DMM_TOL
                If TestMin < 0 Then TestMin = 0
                TestMax = 9 + dW24_Resistance + DMM_TOL
                If Not ClosureDetected(FnSwitchName(FirstPair_1 & ", " & FirstPair_2), TestMin, TestMax, "LF" & ModuleNo, InstrumentToTest, sSwitchCallout) Then
                    Echo("Channel Closing Test Failed:  Channel No: " & FirstPair_1 & ", " & FirstPair_2)
                    Switch1or2Proc = FAILED
                    IncStepFailed()
                    If OptionFaultMode = SOFmode Then GoTo TestComplete
                Else
                    IncStepPassed()
                End If
                Application.DoEvents()
                If AbortTest = True Then GoTo TestComplete
                If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then 'LFx-03-194, 197,   200,203
                    GoTo LF1_5_16
                End If

                iSubStep += 1
                LoopStepNo += 1
                frmSTest.proProgress.Value += 1

LF1_5_17:       nSystErr = WriteMsg(SWITCH1, "OPEN " & FirstPair_1)

                'LFx-03-195,198, 201,204, 207,210
                If Not OpenDetected(FnSwitchName(FirstPair_1), "LF" & ModuleNo) Then
                    sMsg = "Channel Opening Test Failed:  Channel No: " & FirstPair_1
                    sSwitchCallout = "Failed Switch Connections:  " & LogicName1 & FnSwitchName(FirstPair_1) & LogicName2
                    Echo(sMsg)
                    RegisterFailure(CStr(InstrumentToTest), sTNum, dReturnResistance, "Ohms", 2000000, 9.9E+37, sMsg & vbCrLf & sSwitchCallout)
                    Switch1or2Proc = FAILED
                    IncStepFailed()
                    If OptionFaultMode = SOFmode Then GoTo TestComplete
                Else
                    IncStepPassed()
                End If
                Application.DoEvents()
                If AbortTest = True Then GoTo TestComplete
                If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then 'LFx-03-198, 198,   201,204
                    GoTo LF1_5_17
                End If

                iSubStep += 1
                LoopStepNo += 1
                frmSTest.proProgress.Value += 1

LF1_5_18:       nSystErr = WriteMsg(SWITCH1, "CLOSE " & FirstPair_1)
                nSystErr = WriteMsg(SWITCH1, "OPEN " & FirstPair_2)

                'LFx-03-196,199,  202,205, 208,211
                If Not OpenDetected(FnSwitchName(FirstPair_2), "LF" & ModuleNo) Then
                    sMsg = "Channel Opening Test Failed:  Channel No: " & FirstPair_2
                    sSwitchCallout = "Failed Switch Connections:  " & LogicName1 & FnSwitchName(FirstPair_2) & LogicName2
                    Echo(sMsg)
                    RegisterFailure(CStr(InstrumentToTest), sTNum, dReturnResistance, "Ohms", 2000000, 9.9E+37, sMsg & vbCrLf & sSwitchCallout)
                    Switch1or2Proc = FAILED
                    IncStepFailed()
                    If OptionFaultMode = SOFmode Then GoTo TestComplete
                Else
                    IncStepPassed()
                End If
                nSystErr = WriteMsg(SWITCH1, "Open " & FirstPair_1)
                Application.DoEvents()
                If AbortTest = True Then GoTo TestComplete
                If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then 'LFx-03-199,199,   202,205
                    GoTo LF1_5_18
                End If
                LoopStepNo += 1
                iSubStep += 1
                frmSTest.proProgress.Value += 1
            Next Repeat
        Next Channel

        nSystErr = WriteMsg(SWITCH1, "CLOSE " & ModuleNo & ".4100,4111")

        For Channel = 0 To 1
            If Channel = 0 Then
                FirstChannel = ModuleNo & ".4006" ' LFx-03-212  'S501-1,9
                SecondChannel = ModuleNo & ".4017" ' LFx-03-213 'S501-2,10
            Else
                FirstChannel = ModuleNo & ".4007" ' LFx-03-214  'S501-1,10
                SecondChannel = ModuleNo & ".4016" ' LFx-03-215 'S501-2,9
            End If

LF1_5_19:   WriteMsg(SWITCH1, "OPEN " & FirstChannel)

            'LFx-03-212, 214
            If Not OpenDetected(FnSwitchName(FirstChannel), "LF" & ModuleNo) Then
                sMsg = "Channel Opening Test Failed:  Channel No: " & FirstChannel
                sSwitchCallout = "Failed Switch Connections:  " & LogicName1 & FnSwitchName(FirstChannel) & LogicName2
                Echo(sMsg)
                RegisterFailure(CStr(InstrumentToTest), sTNum, dReturnResistance, "Ohms", 2000000, 9.9E+37, sMsg & vbCrLf & sSwitchCallout)
                Switch1or2Proc = FAILED
                IncStepFailed()
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else
                IncStepPassed()
            End If
            Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete
            If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then 'LFx-03-206
                GoTo LF1_5_19
            End If

            iSubStep += 1
            LoopStepNo += 1
            frmSTest.proProgress.Value += 1

LF1_5_20:   WriteMsg(SWITCH1, "CLOSE " & FirstChannel)
            nSystErr = WriteMsg(SWITCH1, "OPEN " & SecondChannel)

            'LFx-03-213, 215
            If Not OpenDetected(FnSwitchName(SecondChannel), "LF" & ModuleNo) Then
                sMsg = "Channel Opening Test Failed:  Channel No: " & SecondChannel
                sSwitchCallout = "Failed Switch Connections:  " & LogicName1 & FnSwitchName(SecondChannel) & LogicName2
                Echo(sMsg)
                RegisterFailure(CStr(InstrumentToTest), sTNum, dReturnResistance, "Ohms", 2000000, 9.9E+37, sMsg & vbCrLf & sSwitchCallout)
                Switch1or2Proc = FAILED
                IncStepFailed()
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else
                IncStepPassed()
            End If
            Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete
            If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then
                GoTo LF1_5_20
            End If

            nSystErr = WriteMsg(SWITCH1, "OPEN " & FirstChannel)
            nSystErr = WriteMsg(SWITCH1, "OPEN " & SecondChannel)
            nSystErr = WriteMsg(SWITCH1, "CLOSE " & ModuleNo & ".4007,4016") 'Remainding closed for the next test out of this For-loop
            iSubStep += 1
            LoopStepNo += 1
            frmSTest.proProgress.Value += 1
        Next Channel

        nSystErr = WriteMsg(SWITCH1, "OPEN " & ModuleNo & ".4100,4111") 'S502-1,3 and S502-2,4
        nSystErr = WriteMsg(SWITCH1, "CLOSE " & ModuleNo & ".4106,4117") 'S502-1,9 and S502-2,10

        For Channel = 0 To 3
            Select Case Channel
                Case 0
                    FirstPair_1 = ModuleNo & ".4200" ' LFx-03-216  'S503-1,3
                    FirstPair_2 = ModuleNo & ".4211"               'S503-2,4
                    SecondPair_1 = ModuleNo & ".4201" ' LFx-03-219 'S503-1,4
                    SecondPair_2 = ModuleNo & ".4210"              'S503-2,3

                Case 1
                    FirstPair_1 = ModuleNo & ".4202" ' LFx-03-222  'S503-1,5
                    FirstPair_2 = ModuleNo & ".4213"               'S503-2,6
                    SecondPair_1 = ModuleNo & ".4203" ' LFx-03-225 'S503-1,6
                    SecondPair_2 = ModuleNo & ".4212"              'S503-2,5

                Case 2
                    FirstPair_1 = ModuleNo & ".4204" ' LFx-03-228  'S503-1,7
                    FirstPair_2 = ModuleNo & ".4215"               'S503-2,8
                    SecondPair_1 = ModuleNo & ".4205" ' LFx-03-231 'S503-1,8
                    SecondPair_2 = ModuleNo & ".4214"              'S503-2,7

                Case Else
                    FirstPair_1 = ModuleNo & ".4206" ' LFx-03-234   'S503-1,9
                    FirstPair_2 = ModuleNo & ".4217"                'S503-2,10
                    SecondPair_1 = ModuleNo & ".4207" ' LFx-03-237  'S503-1,10
                    SecondPair_2 = ModuleNo & ".4216"               'S503-2,9
            End Select

            For Repeat = 0 To 1
                If Repeat = 1 Then
                    FirstPair_1 = SecondPair_1
                    FirstPair_2 = SecondPair_2
                End If

LF1_5_21:       nSystErr = WriteMsg(SWITCH1, "CLOSE " & FirstPair_1)
                nSystErr = WriteMsg(SWITCH1, "CLOSE " & FirstPair_2)

                'LFx-03-216,219, 222,225, 228,231, 234,237
                sSwitchCallout = "Failed Switch Connections:  " & LogicName1 & FnSwitchName(FirstPair_1 & "," & Right(FirstPair_2, 4)) & LogicName2
                TestMin = dW24_Resistance - DMM_TOL
                If TestMin < 0 Then TestMin = 0
                TestMax = 12 + dW24_Resistance + DMM_TOL
                If Not ClosureDetected(FnSwitchName(FirstPair_1 & ", " & FirstPair_2), TestMin, TestMax, "LF" & ModuleNo, InstrumentToTest, sSwitchCallout) Then
                    Echo("Channel Closing Test Failed:  Channel No: " & FirstPair_1 & ", " & FirstPair_2)
                    Switch1or2Proc = FAILED
                    IncStepFailed()
                    If OptionFaultMode = SOFmode Then GoTo TestComplete
                Else
                    IncStepPassed()
                End If
                Application.DoEvents()
                If AbortTest = True Then GoTo TestComplete
                If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then
                    GoTo LF1_5_21
                End If

                iSubStep += 1
                LoopStepNo += 1
                frmSTest.proProgress.Value += 1

LF1_5_22:       nSystErr = WriteMsg(SWITCH1, "OPEN " & FirstPair_1)

                'LFx-03-217,220, 223,226, 229,232, 235,238
                If Not OpenDetected(FnSwitchName(FirstPair_1), "LF" & ModuleNo) Then
                    sMsg = "Channel Opening Test Failed:  Channel No: " & FirstPair_1
                    sSwitchCallout = "Failed Switch Connections:  " & LogicName1 & FnSwitchName(FirstPair_1) & LogicName2
                    Echo(sMsg)
                    RegisterFailure(CStr(InstrumentToTest), sTNum, dReturnResistance, "Ohms", 2000000, 9.9E+37, sMsg & vbCrLf & sSwitchCallout)
                    Switch1or2Proc = FAILED
                    IncStepFailed()
                    If OptionFaultMode = SOFmode Then GoTo TestComplete
                Else
                    IncStepPassed()
                End If
                Application.DoEvents()
                If AbortTest = True Then GoTo TestComplete
                If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then
                    GoTo LF1_5_22
                End If
                iSubStep += 1
                LoopStepNo += 1
                frmSTest.proProgress.Value += 1

LF1_5_23:       nSystErr = WriteMsg(SWITCH1, "CLOSE " & FirstPair_1)
                nSystErr = WriteMsg(SWITCH1, "OPEN " & FirstPair_2)

                'LFx-03-218,221, 224,227, 230,233 236,239
                If Not OpenDetected(FnSwitchName(FirstPair_2), "LF" & ModuleNo) Then
                    sMsg = "Channel Opening Test Failed:  Channel No: " & FirstPair_2
                    sSwitchCallout = "Failed Switch Connections:  " & LogicName1 & FnSwitchName(FirstPair_2) & LogicName2
                    Echo(sMsg)
                    RegisterFailure(CStr(InstrumentToTest), sTNum, dReturnResistance, "Ohms", 2000000, 9.9E+37, sMsg & vbCrLf & sSwitchCallout)
                    Switch1or2Proc = FAILED
                    IncStepFailed()
                    If OptionFaultMode = SOFmode Then GoTo TestComplete
                Else
                    IncStepPassed()
                End If

                nSystErr = WriteMsg(SWITCH1, "OPEN " & FirstPair_1)
                Application.DoEvents()
                If AbortTest = True Then GoTo TestComplete
                If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then
                    GoTo LF1_5_23
                End If
                iSubStep += 1
                LoopStepNo += 1
                frmSTest.proProgress.Value += 1
            Next Repeat
        Next Channel

        nSystErr = WriteMsg(SWITCH1, "CLOSE " & ModuleNo & ".4200,4211")

        For Channel = 0 To 1
            If Channel = 0 Then
                FirstChannel = ModuleNo & ".4106" ' LFx-03-240 'S502-1,9
                SecondChannel = ModuleNo & ".4117"             'S502-2,10
            Else
                FirstChannel = ModuleNo & ".4107" ' LFx-03-243'S502-1,10
                SecondChannel = ModuleNo & ".4116"            'S502-2,9
            End If


LF1_5_24:   WriteMsg(SWITCH1, "CLOSE " & FirstChannel)
            nSystErr = WriteMsg(SWITCH1, "CLOSE " & SecondChannel)

            'LFx-03-240,243
            sSwitchCallout = "Failed Switch Connections:  " & LogicName1 & FnSwitchName(FirstChannel & "," & Right(SecondChannel, 4)) & LogicName2
            TestMin = dW24_Resistance - DMM_TOL
            If TestMin < 0 Then TestMin = 0
            TestMax = 12 + dW24_Resistance + DMM_TOL
            If Not ClosureDetected(FnSwitchName(FirstChannel & ", " & SecondChannel), TestMin, TestMax, "LF" & ModuleNo, InstrumentToTest, sSwitchCallout) Then
                Echo("Channel Closing Test Failed:  Channel No: " & FirstChannel & ", " & SecondChannel)
                Switch1or2Proc = FAILED
                IncStepFailed()
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else
                IncStepPassed()
            End If
            Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete
            If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then
                GoTo LF1_5_24
            End If

            iSubStep += 1
            LoopStepNo += 1
            frmSTest.proProgress.Value += 1

LF1_5_25:   WriteMsg(SWITCH1, "OPEN " & FirstChannel)

            'LFx-03-241, 244
            If Not OpenDetected(FnSwitchName(FirstChannel), "LF" & ModuleNo) Then
                sMsg = "Channel Opening Test Failed:  Channel No: " & FirstChannel
                sSwitchCallout = "Failed Switch Connections:  " & LogicName1 & FnSwitchName(FirstChannel) & LogicName2
                Echo(sMsg)
                RegisterFailure(CStr(InstrumentToTest), sTNum, dReturnResistance, "Ohms", 2000000, 9.9E+37, sMsg & vbCrLf & sSwitchCallout)
                Switch1or2Proc = FAILED
                IncStepFailed()
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else
                IncStepPassed()
            End If
            Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete
            If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then
                GoTo LF1_5_25
            End If

            iSubStep += 1
            LoopStepNo += 1
LF1_5_26:   WriteMsg(SWITCH1, "CLOSE " & FirstChannel)
            nSystErr = WriteMsg(SWITCH1, "OPEN " & SecondChannel)

            'LFx-03-242, 245
            If Not OpenDetected(FnSwitchName(SecondChannel), "LF" & ModuleNo) Then
                sMsg = "Channel Opening Test Failed:  Channel No: " & SecondChannel
                sSwitchCallout = "Failed Switch Connections:  " & LogicName1 & FnSwitchName(SecondChannel) & LogicName2
                Echo(sMsg)
                RegisterFailure(CStr(InstrumentToTest), sTNum, dReturnResistance, "Ohms", 2000000, 9.9E+37, sMsg & vbCrLf & sSwitchCallout)
                Switch1or2Proc = FAILED
                IncStepFailed()
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else
                IncStepPassed()
            End If
            nSystErr = WriteMsg(SWITCH1, "OPEN " & FirstChannel)
            Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete
            If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then
                GoTo LF1_5_26
            End If
            iSubStep += 1
            LoopStepNo += 1
            frmSTest.proProgress.Value += 1
        Next Channel

TestComplete:

        frmSTest.proProgress.Value = 100
        frmSTest.proProgress.Maximum = 100
        frmSTest.cmdAbort.Text = "Abort Test"
        frmSTest.cmdPause.Text = "Pause Test"
        nSystErr = WriteMsg(SWITCH1, "OPEN " & ModuleNo & ".0-4417")    'Make sure that all the relays are open at the end of the Switch 2 Self-test
        nSystErr = WriteMsg(DMM, "*RST") ' init DMM
        nSystErr = WriteMsg(DMM, "*CLS") ' init DMM
        If Switch1or2Proc = FAILED Then 'If Test Fails, register failure
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
        '   This routine tests LF-3
        'RETURNS:
        '   0 if the switch being tested passed or 1 if the test fails
        Dim ModuleX As Integer = 0
        Dim SError As Integer = 0
        Dim Mux As Integer = 0
        Dim Channel As Integer = 0
        Dim ChannelNo As String = ""
        Dim sS751 As String = ""
        Dim sSwitchCallout As String = ""    'Describes the switches failed for the Fault Callout
        Dim LoopStepNo As Integer = 0
        Dim TestName As String = "LF3 Switch"

        Dim TestMin As Single
        Dim TestMax As Single


        iModAddress = 3
        iTestStep = 1
        iSubStep = 0
        Switch3Proc = PASSED
        Application.DoEvents()

        EchoTitle("Low Frequency Switching #3 Test", False)
        frmSTest.proProgress.Maximum = 265
        frmSTest.proProgress.Value = 0

        frmSTest.sbrUserInformation.Text = "Testing " & InstrumentDescription(SWITCH3)
        Application.DoEvents()
        LoopStepNo = 1
        nSystErr = WriteMsg(SWITCH1, "RESET")

LF3_1_1:  'LF3-01-001
        sTNum = "LF3-01-001"
        nSystErr = WriteMsg(SWITCH1, "CLOSE 3.0000-7002")
        SError = SwitchingError(ModuleX)
        If SError <> 0 Then
            sMsg = "1260-38 Switching Module Close BIT error<" & CStr(SError) & "> in module 3"
            Echo(sTNum & "     " & sMsg)
            RegisterFailure(CStr(SWITCH3), sTNum, sComment:=sMsg)
            Switch3Proc = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            FormatResultLine(sTNum & "     1260-38 Switching Module Close BIT", True)
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then
            GoTo LF3_1_1
        End If
        LoopStepNo = 2
        frmSTest.proProgress.Value += 1

LF3_1_2:  'LF3-01-002
        sTNum = "LF3-01-002"
        nSystErr = WriteMsg(SWITCH1, "OPEN 3.0000-7002")
        SError = SwitchingError(ModuleX)
        If SError <> 0 Then
            sMsg = "1260-38 Switching Module Open BIT error<" & CStr(SError) & "> in module 3"
            Echo(sTNum & "     " & sMsg)
            RegisterFailure(CStr(SWITCH3), sTNum, sComment:=sMsg)
            Switch3Proc = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            FormatResultLine(sTNum & "     1260-38 Switching Module Open BIT", True)
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then
            GoTo LF3_1_2
        End If
        LoopStepNo = 3
        If FirstPass = True Then
            S = "Use cable W24 (OBSERVE POLARITY) to connect" & vbCrLf & vbCrLf
            S &= "   DMM INPUT (HI/LO)   to   UTILITY 2-WIRE (HI/LO)."
            DisplaySetup(S, "TETS DIN_U2W.jpg", 1)
            If AbortTest = True Then GoTo testcomplete
        End If

        'Close LF3 interconnect relays
        nSystErr = WriteMsg(SWITCH1, "CLOSE 3.1001")
        nSystErr = WriteMsg(SWITCH1, "CLOSE 3.2002")
        nSystErr = WriteMsg(SWITCH1, "CLOSE 3.3000,3001")
        nSystErr = WriteMsg(SWITCH1, "CLOSE 3.4000,4001")
        nSystErr = WriteMsg(SWITCH1, "CLOSE 3.6000,6001")
        nSystErr = WriteMsg(SWITCH1, "CLOSE 3.7000,7001")

        'Testing S751. Close all switches connected to it
        nSystErr = WriteMsg(SWITCH1, "CLOSE 3.0010-0157")

        iTestStep = 2
        iSubStep = 1
        frmSTest.proProgress.Value += 1
        For Channel = 0 To 7
LF3_2_1:    sTNum = "LF3-02-" & Format(iSubStep, "000") 'LF3-02-001 ~ LF3-02-015    (Odd)
            sS751 = "3.000" & Format(Channel, "0")
            sSwitchCallout = "Failed Switch Connections:  " & FnSwitchName(sS751)
            nSystErr = WriteMsg(SWITCH1, "CLOSE " & sS751)
            TestMin = dW24_Resistance - DMM_TOL
            If TestMin < 0 Then TestMin = 0
            TestMax = 2 + dW24_Resistance + DMM_TOL
            If Not ClosureDetected(FnSwitchName(sS751), TestMin, TestMax, "LF3", SWITCH3, sSwitchCallout) Then
                Echo("Switch 3 Channel Closing Test Failed:  Channel No: " & sS751)
                Switch3Proc = FAILED
                IncStepFailed()
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else
                IncStepPassed()
            End If
            Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete
            If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then
                GoTo LF3_2_1
            End If
            iSubStep += 1
            LoopStepNo += 1
            frmSTest.proProgress.Value += 1

LF3_2_2:    sTNum = "LF3-02-" & Format(iSubStep, "000") 'LF3-02-002 ~ LF3-02-016    (Even)
            nSystErr = WriteMsg(SWITCH1, "OPEN " & sS751)
            If Not OpenDetected(FnSwitchName(sS751), "LF3") Then
                sMsg = "Switch 3 Channel Opening Test Failed:  Channel No: " & sS751
                sSwitchCallout = "Failed Switch Connections:  " & FnSwitchName(sS751)
                Echo(sMsg)
                RegisterFailure(CStr(SWITCH3), sTNum, dReturnResistance, "Ohms", 2000000, 9.9E+37, sMsg & vbCrLf & sSwitchCallout)
                Switch3Proc = FAILED
                IncStepFailed()
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else
                IncStepPassed()
            End If
            Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete
            If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then
                GoTo LF3_2_2
            End If
            iSubStep += 1
            LoopStepNo += 1
            frmSTest.proProgress.Value += 1

        Next Channel
        nSystErr = WriteMsg(SWITCH1, "OPEN 3.0010-0157")

        'Testing:
        '   S701    LF3-02-001 ~ LF3-02-048
        '   S201    LF3-03-001 ~ LF3-03-096
        '   S202    LF3-04-001 ~ LF3-04-096

        For Mux = 1 To 15
            Select Case Mux
                Case 1
                    iTestStep = 3       'S701
                    iSubStep = 1
                Case 4
                    iTestStep = 4       'S201
                    iSubStep = 1
                Case 10
                    iTestStep = 5       'S202
                    iSubStep = 1
            End Select
            Select Case Mux
                Case Is <= 3
                    sS751 = "3.0005"    'S751, Channel 5 for Mux 1, 2, 3
                Case 4, 6, 8
                    sS751 = "3.0000"    'S751, Channel 0 for Mux 4, 6, 8
                Case 5, 7, 9
                    sS751 = "3.0001"    'S751, Channel 1 for Mux 5, 7, 9
                Case 10, 12, 14
                    sS751 = "3.0004"    'S751, Channel 4 for Mux 10, 12, 14
                Case 11, 13, 15
                    sS751 = "3.0003"    'S751, Channel 3 for Mux 11, 13, 15
            End Select

            nSystErr = WriteMsg(SWITCH1, "CLOSE " & sS751)

            For Channel = 0 To 7       'Each Mux has 8 channels
LF3_3_1:        ChannelNo = "3.0" & Format(Mux, "00") & Format(Channel, "0")
                sTNum = "LF3-" & Format(iTestStep, "00") & "-" & Format(iSubStep, "000")
                nSystErr = WriteMsg(SWITCH1, "CLOSE " & ChannelNo)
                sSwitchCallout = "Failed Switch Connections:  " & FnSwitchName(ChannelNo)
                'LF3-03-001 ~ LF3-03-047; LF3-04-001 ~ LF3-04-095; LF3-05-001 ~ LF3-05-095  (Odd)
                TestMin = dW24_Resistance - DMM_TOL
                If TestMin < 0 Then TestMin = 0
                TestMax = 4 + dW24_Resistance + DMM_TOL
                If Not ClosureDetected(FnSwitchName(ChannelNo), TestMin, TestMax, "LF3", SWITCH3, sSwitchCallout) Then
                    'finished need to verify
                    Echo("Switch 3 Channel Closing Test Failed:  Channel No: " & ChannelNo)
                    Switch3Proc = FAILED
                    IncStepFailed()
                    If OptionFaultMode = SOFmode Then GoTo TestComplete
                Else
                    IncStepPassed()
                End If
                Application.DoEvents()
                If AbortTest = True Then GoTo TestComplete
                If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then
                    GoTo LF3_3_1
                End If
                iSubStep += 1
                LoopStepNo += 1
                frmSTest.proProgress.Value += 1

LF3_3_2:        nSystErr = WriteMsg(SWITCH1, "OPEN " & ChannelNo)
                'LF3-03-002 ~ LF3-22-048; LF3-04-002 ~ LF3-04-096; LF3-05-002 ~ LF3-05-096  (Even)
                sTNum = "LF3-" & Format(iTestStep, "00") & "-" & Format(iSubStep, "000")
                If Not OpenDetected(FnSwitchName(ChannelNo), "LF3") Then
                    sMsg = "Switch 3 Channel Opening Test Failed:  Channel No: " & ChannelNo
                    sSwitchCallout = "Failed Switch Connections:  " & FnSwitchName(ChannelNo)
                    Echo(sMsg)
                    RegisterFailure(CStr(SWITCH3), sTNum, dReturnResistance, "Ohms", 2000000, 9.9E+37, sMsg & vbCrLf & sSwitchCallout)
                    Switch3Proc = FAILED
                    IncStepFailed()
                    If OptionFaultMode = SOFmode Then GoTo TestComplete
                Else
                    IncStepPassed()
                End If
                Application.DoEvents()
                If AbortTest = True Then GoTo TestComplete
                If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then
                    GoTo LF3_3_2
                End If
                LoopStepNo += 1
                iSubStep += 1
                frmSTest.proProgress.Value = frmSTest.proProgress.Value + 1
            Next Channel
            nSystErr = WriteMsg(SWITCH1, "OPEN " & sS751)
        Next Mux

TestComplete:
        frmSTest.proProgress.Value = 100
        frmSTest.proProgress.Maximum = 100
        frmSTest.cmdAbort.Text = "Abort Test"
        frmSTest.cmdPause.Text = "Pause Test"
        nSystErr = WriteMsg(SWITCH1, "RESET")
        nSystErr = WriteMsg(SWITCH1, "OPEN 3.1000,1002,2000,2001,3002,4002,5000,5001,5002,6002,7002,8002")
        nSystErr = WriteMsg(SWITCH1, "CLOSE 3.1001,2002,3000,3001,4000,4001,6000,6001,7000,7001")
        nSystErr = WriteMsg(DMM, "*RST")
        nSystErr = WriteMsg(DMM, "*CLS")
        If Switch3Proc = FAILED Then 'If Test Fails, register failure
            RegisterFailure(CStr(SWITCH3))
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
        '   This routine tests the medium frequency switches
        'RETURNS:
        '   PASSED if the switch being tested passed or FALIED if the test fails

        Dim Channel As Integer = 0
        Dim ChanChar As String = ""
        Dim ModuleX As Integer = 0
        Dim SError As Integer = 0
        Dim PairNo As Integer = 0
        Dim ChannelNo1 As String = ""
        Dim ChannelNo2 As String = ""
        Dim sSwitchCallout As String = ""   'Describes the switches failed for the Fault Callout
        Dim LoopStepNo As Integer = 0
        Dim TestName As String = "MF Switch"
        Dim TestMin As Single
        Dim TestMax As Single

        EchoTitle("Medium Frequency Switching Test", False)
        frmSTest.proProgress.Value = 0
        frmSTest.proProgress.Maximum = 120

        iModAddress = 4
        Switch4Proc = PASSED
        Application.DoEvents()
        frmSTest.sbrUserInformation.Text = "Testing " & InstrumentDescription(SWITCH4)
        nSystErr = WriteMsg(SWITCH1, "RESET")

        'MFS-01-001 ~ MFS-01-018  Odd = Close BIT
        iTestStep = 1
        iSubStep = 1
        LoopStepNo = 1
        frmSTest.proProgress.Value = 1
MFS_1_1: sTNum = "MFS-01-001"
        For Channel = 0 To 8
            ChanChar = CStr(Channel)
            nSystErr = WriteMsg(SWITCH1, "CLOSE 4.0" & ChanChar & ",1" & ChanChar & ",2" & ChanChar & ",3" & ChanChar)
            SError = SwitchingError(ModuleX)
            If SError <> 0 Then
                Echo(sTNum)
                sMsg = "1260-58 Switching Module Close Test error<" & CStr(SError) & "> in module 4"
                Echo(sMsg)
                RegisterFailure(CStr(SWITCH4), sTNum, sComment:=sMsg)
                Switch4Proc = FAILED
                Exit For
            End If

            'MFS-01-001 ~ MFS-01-018  Even = Open BIT
            nSystErr = WriteMsg(SWITCH1, "OPEN 4.0" & ChanChar & ",1" & ChanChar & ",2" & ChanChar & ",3" & ChanChar)
            SError = SwitchingError(ModuleX)
            If SError <> 0 Then
                Echo(sTNum)
                sMsg = "1260-58 Switching Module Open Test error<" & CStr(SError) & "> in module 4"
                Echo(sMsg)
                RegisterFailure(CStr(SWITCH4), sTNum, sComment:=sMsg)
                Switch4Proc = FAILED
                Exit For
            End If
            iSubStep = iSubStep + 1
            frmSTest.proProgress.Value += 1
        Next Channel
        FormatResultLine("MFS-01-001 ~ MFS-01-018   1260-58 Switching Module Close/Open Test", True)
        If SError <> 0 Then
            IncStepFailed()
        Else
            IncStepPassed()
        End If

        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then
            GoTo MFS_1_1
        End If
        LoopStepNo = 2
        frmSTest.proProgress.Value += 1

        frmSTest.proProgress.Value = 10
        If FirstPass = True Then
            S = "Setup the MF switch test as follows:" & vbCrLf
            S &= "1. Use cable W25 (OBSERVE POLARITY) to connect" & vbCrLf
            S &= "   DMM INPUT (HI/LO)   to   UTILITY MF IN." & vbCrLf
            S &= "2. Use cable W15 to connect" & vbCrLf
            S &= "   C/T INPUT 1   to   UTILITY MF OUT."
            DisplaySetup(S, "TETS DMM_MFI.jpg", 2)
            If AbortTest = True Then GoTo testcomplete
        End If

        nSystErr = WriteMsg(DMM, "*RST")
        nSystErr = WriteMsg(DMM, "*CLS")
        Delay(0.1)
        nSystErr = WriteMsg(DMM, "SENS:RES:APER MIN")
        Delay(0.1)

        For PairNo = 0 To 1
            iSubStep = 1
            iTestStep = PairNo + 2 ' ie MFS-02 or MFS-03
            If PairNo = 0 Then
                nSystErr = WriteMsg(SWITCH1, "CLOSE 4.10")
                nSystErr = WriteMsg(SWITCH1, "CLOSE 4.30")
            ElseIf PairNo = 1 Then
                nSystErr = WriteMsg(SWITCH1, "CLOSE 4.07")
                nSystErr = WriteMsg(SWITCH1, "CLOSE 4.27")
            End If
            For Channel = 0 To 7
                sTNum = "MFS-" & Format(iTestStep, "00") & "-" & Format(iSubStep, "000")
                If PairNo = 0 Then
                    ChannelNo1 = "4.0" & CStr(Channel)
                    ChannelNo2 = "4.2" & CStr(Channel)
                Else
                    ChannelNo1 = "4.1" & CStr(Channel)
                    ChannelNo2 = "4.3" & CStr(Channel)
                End If

                nSystErr = WriteMsg(SWITCH1, "CLOSE " & ChannelNo1)
                nSystErr = WriteMsg(SWITCH1, "CLOSE " & ChannelNo2)


                sSwitchCallout = "Failed Switch Connections:  " & FnSwitchName(ChannelNo1 & "," & Right(ChannelNo2, 2))
                'MFS-02-001, 004, 007, 010, 013, 016, 019;    MFS-03-001, 004, 007, 010, 013, 016, 019, 022
                TestMin = -DMM_TOL
                If TestMin < 0 Then TestMin = 0
                TestMax = (PairNo + 1) * 2 + DMM_TOL
MFS_2_1:        sTNum = "MFS-" & Format(iTestStep, "00") & "-" & Format(iSubStep, "000")
                If Not ClosureDetected(FnSwitchName(ChannelNo1 & ", " & ChannelNo2), TestMin, TestMax, "MFS", SWITCH4, sSwitchCallout, dW25_CT_Resistance) Then
                    'Path resistance is directly proportional to the PairNo,
                    'Each pair is 2 Ohms more than the previous pair.
                    Echo("Switch Channel Closing Test Failed:  Channel No: " & ChannelNo1 & ", " & ChannelNo2)
                    Switch4Proc = FAILED
                    IncStepFailed()
                    If OptionFaultMode = SOFmode Then GoTo TestComplete
                Else
                    IncStepPassed()
                End If
                Application.DoEvents()
                If AbortTest = True Then GoTo TestComplete
                If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then
                    GoTo MFS_2_1
                End If
                iSubStep += 1
                LoopStepNo += 1
                frmSTest.proProgress.Value += 1

                'Open closed switches before the next test
MFS_2_2:        sTNum = "MFS-" & Format(iTestStep, "00") & "-" & Format(iSubStep, "000")
                nSystErr = WriteMsg(SWITCH1, "OPEN " & ChannelNo1)
                'MFS-02-002, 005, 008, 011, 014, 017, 020;   MFS-03-002, 005, 008, 011, 014, 017, 020, 023
                If Not OpenDetected(FnSwitchName(ChannelNo1), "MFS") Then
                    sMsg = "Switch Channel Opening Test Failed:  Channel No: " & ChannelNo1
                    sSwitchCallout = "Failed Switch Connections:  " & FnSwitchName(ChannelNo1)
                    Echo(sMsg)
                    RegisterFailure(CStr(SWITCH4), sTNum, dReturnResistance, "Ohms", 2000000, 9.9E+37, sMsg & vbCrLf & sSwitchCallout)
                    Switch4Proc = FAILED
                    IncStepFailed()
                    If OptionFaultMode = SOFmode Then GoTo TestComplete
                Else
                    IncStepPassed()
                End If
                Application.DoEvents()
                If AbortTest = True Then GoTo TestComplete
                If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then
                    GoTo MFS_2_2
                End If
                iSubStep += 1
                LoopStepNo += 1
                frmSTest.proProgress.Value += 1

MFS_2_3:        sTNum = "MFS-" & Format(iTestStep, "00") & "-" & Format(iSubStep, "000")
                nSystErr = WriteMsg(SWITCH1, "CLOSE " & ChannelNo1)
                nSystErr = WriteMsg(SWITCH1, "OPEN " & ChannelNo2)
                'MFS-02-003, 006, 009, 012, 015, 018, 021;   MFS-03-003, 006, 009, 012, 015, 018, 021, 024
                If Not OpenDetected(FnSwitchName(ChannelNo2), "MFS") Then
                    sMsg = "Switch Channel Opening Test Failed:  Channel No: " & ChannelNo2
                    sSwitchCallout = "Failed Switch Connections:  " & FnSwitchName(ChannelNo2)
                    Echo(sMsg)
                    RegisterFailure(CStr(SWITCH4), sTNum, dReturnResistance, "Ohms", 2000000, 9.9E+37, sMsg & vbCrLf & sSwitchCallout)
                    Switch4Proc = FAILED
                    IncStepFailed()
                    If OptionFaultMode = SOFmode Then GoTo TestComplete
                Else
                    IncStepPassed()
                End If
                Application.DoEvents()
                If AbortTest = True Then GoTo TestComplete
                If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then
                    GoTo MFS_2_3
                End If
                iSubStep += 1
                LoopStepNo += 1
                frmSTest.proProgress.Value += 1
                nSystErr = WriteMsg(SWITCH1, "OPEN " & ChannelNo1)
            Next Channel
        Next PairNo

        Application.DoEvents()


TestComplete:
        frmSTest.proProgress.Maximum = 100
        frmSTest.proProgress.Value = 100

        If Switch4Proc = FAILED Then dW25_CT_Resistance = 0 'If Test Fails, Reset Cable Resistance

        frmSTest.cmdAbort.Text = "Abort Test"
        frmSTest.cmdPause.Text = "Pause Test"
        nSystErr = WriteMsg(SWITCH1, "OPEN 4.00-37")
        nSystErr = WriteMsg(DMM, "*RST")
        nSystErr = WriteMsg(DMM, "*CLS")
        If Switch4Proc = FAILED Then 'If Test Fails, register failure
            RegisterFailure(CStr(SWITCH4))
        End If
        Application.DoEvents()
        If AbortTest = True Then
            dW25_CT_Resistance = 0
            sMsg = vbCrLf
            sMsg &= "      **********************************************" & vbCrLf
            sMsg &= "      *        MF  Switch test aborted!            *" & vbCrLf
            sMsg &= "      **********************************************" & vbCrLf
            Echo(sMsg)
        End If

    End Function

    Function Switch5Proc() As Integer

        'DESCRIPTION:
        '   This routine tests the high frequency switches
        'RETURNS:
        '   PASSED if the switch being tested passed, or FAILED if the test fails

        Dim Channel As Integer = 0
        Dim ChanChar As String = ""
        Dim ModuleX As Integer = 0
        Dim SError As Integer = 0
        Dim PairNo As Integer = 0
        Dim Mux1 As Integer = 0
        Dim Mux2 As Integer = 0
        Dim ChannelNo1 As String = ""
        Dim ChannelNo2 As String = ""
        Dim ChannelBridge1 As String = ""
        Dim ChannelBridge2 As String = ""
        Dim sSwitchCallout As String = ""    'Describes the switches failed for the Fault Callout
        Dim LoopStepNo As Integer = 0
        Dim TestName As String = "HF Switch"
        Dim HFPathMax As Single
        Dim TestMax As Single
        Dim TestMin As Single

        iModAddress = 5
        iTestStep = 1
        iSubStep = 1
        Switch5Proc = PASSED
        Application.DoEvents()
        EchoTitle("High Frequency Switching Test", False)
        frmSTest.sbrUserInformation.Text = "Testing " & InstrumentDescription(SWITCH5)
        frmSTest.proProgress.Value = 0
        frmSTest.proProgress.Maximum = 100
        nSystErr = WriteMsg(SWITCH1, "RESET")

        iTestStep = 1
        iSubStep = 1
        LoopStepNo = 1
HFS_1_1: sTNum = "HFS-01-001"
        For Channel = 0 To 5
            Application.DoEvents()
            ChanChar = CStr(Channel)
            'HFS-01-001,3,5,7,9,11  close BIT
            'Close 5.00,10,20,30,40,50,  5.01,11,21,31,41,51, ,,,
            nSystErr = WriteMsg(SWITCH1, "CLOSE 5.0" & ChanChar & ",1" & ChanChar & ",2" & ChanChar & ",3" & ChanChar & ",4" & ChanChar & ",5" & ChanChar)
            SError = SwitchingError(ModuleX)
            If SError <> 0 Then
                sTNum = "HFS-00-" & Format(iSubStep, "000")
                Echo(sTNum)
                sMsg = "1260-66 Switching Module Close Test error<" & CStr(SError) & "> in module 5"
                Echo(sMsg)
                RegisterFailure(CStr(SWITCH5), sTNum, sComment:=sMsg)
                Switch5Proc = FAILED
                Exit For
            End If
            iSubStep += 1

            'HFS-01-002,4,6,8,10,12 open BIT
            'Close 5.00,10,20,30,40,50,  5.01,11,21,31,41,51, ,,,
            nSystErr = WriteMsg(SWITCH1, "OPEN 5.0" & ChanChar & ",1" & ChanChar & ",2" & ChanChar & ",3" & ChanChar & ",4" & ChanChar & ",5" & ChanChar)
            SError = SwitchingError(ModuleX)
            If SError <> 0 Then
                sTNum = "HFS-00-" & Format(iSubStep, "000")
                Echo(sTNum)
                sMsg = "1260-66 Switching Module Open Test error<" & CStr(SError) & "> in module 5"
                Echo(sMsg)
                RegisterFailure(CStr(SWITCH5), sTNum, sComment:=sMsg)
                Switch5Proc = FAILED
                Exit For
            End If
            iSubStep += 1
        Next Channel

        If SError <> 0 Then
            IncStepFailed()
        Else
            IncStepPassed()
        End If
        FormatResultLine("HFS-01-001 ~ HFS-01-012   1260-66 Switching Module Close/Open Test", True)
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then
            GoTo HFS_1_1
        End If
        LoopStepNo += 1
        frmSTest.proProgress.Value += 1

        If FirstPass = True Then
            S = "Setup the HF switch test as follows:" & vbCrLf
            S &= "1. Use cable W21 (OBSERVE POLARITY) to connect" & vbCrLf
            S &= "   DMM INPUT (HI/LO)   to   UTILITY HF IN." & vbCrLf
            S &= "2. Connect an N-type 50 OHM TERMINATOR to" & vbCrLf
            S &= "   UTILITY HF OUT."
            DisplaySetup(S, "TETS DIN_UHI.jpg", 2) ' 2 steps, one picture
            If AbortTest = True Then GoTo testcomplete
        End If

        nSystErr = WriteMsg(DMM, "*RST")
        nSystErr = WriteMsg(DMM, "*CLS")
        nSystErr = WriteMsg(DMM, "SENS:RES:APER MIN")

        'HFS-02-001 ~ HFS-02-020
        'HFS-03-001 ~ HFS-03-012
        'HFS-04-001 ~ HFS-04-020
        'PairNo is the switch pair that is being tested
        For PairNo = 0 To 2  ' 0=S901,S902, 1=S903,S904, 2=S905,S906
            iSubStep = 1
            iTestStep = PairNo + 2
            If PairNo = 0 Then
                Mux1 = 0 'S901
                Mux2 = 3 'S904
                nSystErr = WriteMsg(SWITCH1, "CLOSE 5.10") 'S902-1,2
                nSystErr = WriteMsg(SWITCH1, "CLOSE 5.20") 'S903-1,2
                nSystErr = WriteMsg(SWITCH1, "CLOSE 5.40") 'S905-1,2
                nSystErr = WriteMsg(SWITCH1, "CLOSE 5.50") 'S906-1,2
            ElseIf PairNo = 1 Then
                Mux1 = 1 'S903
                Mux2 = 2 'S904
                nSystErr = WriteMsg(SWITCH1, "CLOSE 5.00") 'S901-1,2
                nSystErr = WriteMsg(SWITCH1, "CLOSE 5.30") 'S904-1,2
            ElseIf PairNo = 2 Then
                Mux1 = 4 'S904
                Mux2 = 5 'S905
                nSystErr = WriteMsg(SWITCH1, "CLOSE 5.05") 'S901-1,7
                nSystErr = WriteMsg(SWITCH1, "CLOSE 5.35") 'S904-1,7
            End If

            For Channel = 0 To 5
                ChannelNo1 = "5." & CStr(Mux1) & CStr(Channel)
                ChannelNo2 = "5." & CStr(Mux2) & CStr(Channel)
                nSystErr = WriteMsg(SWITCH1, "CLOSE " & ChannelNo1)
                nSystErr = WriteMsg(SWITCH1, "CLOSE " & ChannelNo2)
                If (PairNo = 0) And (Channel > 0) And (Channel < 5) Then
                    HFPathMax = 2
                Else
                    HFPathMax = 4
                End If

                'Test switches closed
HFS_2_1:        sTNum = "HFS-" & Format(iTestStep, "00") & "-" & Format(iSubStep, "000")
                sSwitchCallout = "Failed Switch Connections:  " & FnSwitchName(ChannelNo1 & "," & Right(ChannelNo2, 2)) ' ie 

                TestMin = -DMM_TOL
                If TestMin < 0 Then TestMin = 0
                TestMax = HFPathMax + DMM_TOL
                If Not ClosureDetected(FnSwitchName(ChannelNo1 & ", " & ChannelNo2), TestMin, TestMax, "HFS", SWITCH5, sSwitchCallout, dW21_Resistance) Then
                    'Path resistance is 2 Ohms when PairNo is odd and 4 Ohms when PairNo is even.
                    Switch5Proc = FAILED
                    IncStepFailed()
                    If OptionFaultMode = SOFmode Then GoTo TestComplete
                Else
                    IncStepPassed()
                End If
                Application.DoEvents()
                If AbortTest = True Then GoTo TestComplete
                If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then
                    GoTo HFS_2_1
                End If
                iSubStep += 1
                LoopStepNo += 1
                frmSTest.proProgress.Value += 1

                'Test first switch open
HFS_2_2:        sTNum = "HFS-" & Format(iTestStep, "00") & "-" & Format(iSubStep, "000")
                nSystErr = WriteMsg(SWITCH1, "OPEN " & ChannelNo1)

                If Not OpenDetected(FnSwitchName(ChannelNo1), "HFS") Then
                    sMsg = "Switch 5 Channel Opening Test Failed:  Channel No: " & ChannelNo1
                    sSwitchCallout = "Failed Switch Connections:  " & FnSwitchName(ChannelNo1)
                    Echo(sMsg)
                    RegisterFailure(CStr(SWITCH5), sTNum, dReturnResistance, "Ohms", 2000000, 9.9E+37, sMsg & vbCrLf & sSwitchCallout)
                    Switch5Proc = FAILED
                    IncStepFailed()
                    If OptionFaultMode = SOFmode Then GoTo TestComplete
                Else
                    IncStepPassed()
                End If
                Application.DoEvents()
                If AbortTest = True Then GoTo TestComplete
                If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then
                    GoTo HFS_2_2
                End If
                iSubStep += 1
                LoopStepNo += 1
                frmSTest.proProgress.Value += 1

                'Test second switch open
HFS_2_3:        sTNum = "HFS-02-" & Format(iSubStep, "000")
                nSystErr = WriteMsg(SWITCH1, "CLOSE " & ChannelNo1)
                nSystErr = WriteMsg(SWITCH1, "OPEN " & ChannelNo2)

                If Not OpenDetected(FnSwitchName(ChannelNo2), "HFS") Then
                    sMsg = "Switch Channel Opening Test Failed:  Channel No: " & ChannelNo2
                    sSwitchCallout = "Failed Switch Connections:  " & FnSwitchName(ChannelNo2)
                    Echo(sMsg)
                    RegisterFailure(CStr(SWITCH5), sTNum, dReturnResistance, "Ohms", 2000000, 9.9E+37, sMsg & vbCrLf & sSwitchCallout)
                    Switch5Proc = FAILED
                    IncStepFailed()
                    If OptionFaultMode = SOFmode Then GoTo TestComplete
                Else
                    IncStepPassed()
                End If
                Application.DoEvents()
                If AbortTest = True Then GoTo TestComplete
                If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then
                    GoTo HFS_2_3
                End If
                nSystErr = WriteMsg(SWITCH1, "OPEN  " & ChannelNo1 & CStr(Channel))
                iSubStep += 1
                LoopStepNo += 1
                frmSTest.proProgress.Value += 1

                'Setup for next test
                nSystErr = WriteMsg(SWITCH1, "OPEN " & ChannelNo1)
                nSystErr = WriteMsg(SWITCH1, "OPEN " & ChannelNo2)
            Next Channel
        Next PairNo

TestComplete:
        frmSTest.proProgress.Maximum = 100
        frmSTest.proProgress.Value = 100
        Application.DoEvents()
        'DJoiner 03/15/02
        If Switch5Proc = FAILED Then dW21_Resistance = 0 'If Test Fails, Reset Cable Resistance

        frmSTest.cmdAbort.Text = "Abort Test"
        frmSTest.cmdPause.Text = "Pause Test"
        nSystErr = WriteMsg(SWITCH1, "OPEN 5.00-55")
        nSystErr = WriteMsg(DMM, "*RST")
        nSystErr = WriteMsg(DMM, "*CLS")
        If Switch5Proc = FAILED Then 'If Test Fails, register failure
            RegisterFailure(CStr(SWITCH5))
        End If
        Application.DoEvents()
        If AbortTest = True Then
            dW21_Resistance = 0
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