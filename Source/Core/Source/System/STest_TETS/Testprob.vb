'Option Strict Off
Option Explicit On

Public Module modTestProbeButton

    '**************************************************************
    '* Nomenclature   : ATS-TETS SYSTEM SELF TEST                 *
    '*                  Analog Probe Test                         *
    '* Version        : 2.0                                       *
    '* Last Update    : Apr 1, 2017                               *
    '* Purpose        : This module contains code for the         *
    '*                  Analog Probe Test Self Test               *
    '**************************************************************

    Public Function TestAnalogProbe() As Integer

        Dim ButtonOff As Integer
        Dim ButtonPressed As Integer
        Dim DelayTime As Single
        Dim Ret As Integer
        Dim waittime As Single
        Dim SError As String 'Error Message
        Dim S As String
        Dim i As Integer
        TestAnalogProbe = UNTESTED
        HelpContextID = 1300
        'Analog Probe Test Title block
        EchoTitle(InstrumentDescription(APROBE), True)
        EchoTitle("Analog Probe Test", False)
        frmSTest.proProgress.Maximum = 100
        frmSTest.proProgress.Value = 1

        If AbortTest = True Then GoTo TestComplete
        AbortTest = False

        ButtonOff = -1
        ButtonPressed = 0
        DelayTime = 1 'sec


        MsgBox("Remove all cable connections from the SAIF.")
        frmSTest.proProgress.Value = 10
        If AbortTest = True Then GoTo TestComplete
        TestAnalogProbe = PASSED

        ReadReceiverSwitches()
        If AprobeButton = True Then
            MsgBox("Remove or ensure that the ANALOG PROBE is NOT connected to the ANALOG PROBE jack on the receiver.")
            ReadReceiverSwitches() 'Check if the button is off initially
            If AprobeButton = True Then
                SError = "ERROR - ANALOG PROBE circuitry in the receiver FAILED."
                MsgBox(SError, MsgBoxStyle.Exclamation)
                Echo(SError)
                RegisterFailure(APROBE, , , , , , SError)
                TestAnalogProbe = FAILED
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            End If
        End If
        frmSTest.proProgress.Value = 20
        If AbortTest = True Then GoTo TestComplete

        S = "Connect the ANALOG PROBE to the ANALOG PROBE jack (P2) on the receiver under the SAIF." & vbCrLf
        S &= "Do not press the analog probe button at this time." & vbCrLf
        DisplaySetup(S, "ST-APR-P2.jpg", 1)
        If AbortTest = True Then GoTo TestComplete
        frmSTest.proProgress.Value = 25

        DelayTime = 1 'sec

        'APR-01-001
        ReadReceiverSwitches()
        If AprobeButton = True Then 'Check if the button is off initially
            SError = "ERROR - The Probe Button is not off."
            MsgBox(SError, MsgBoxStyle.Exclamation)
            Echo(SError)
            Echo(FormatResults(False, "Probe Button OFF Test", "APR-01-001"))
            Echo("   " & SError)
            RegisterFailure(APROBE, "APR-01-001", , , , , "Probe Button OFF Test" & vbCrLf * SError)
            TestAnalogProbe = FAILED
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        End If
        Echo(FormatResults(True, "Probe Button OFF Test", "APR-01-001"))
        frmSTest.proProgress.Value = 30
        If AbortTest = True Then GoTo TestComplete

Step1:
       'Connect W24 HI/LO to DMM INPUTS HI/LO and connect W24 LO to the DMM GND.
        S = "Connect DMM INPUT-LO to DMM GND as follows:" & vbCrLf
        S &= "Use cable W24 (OBSERVE POLARITY) to connect" & vbCrLf & vbCrLf
        S &= "   SAIF DMM INPUT (HI/LO)   to   OPEN/DMM-GND(Green)." & vbCrLf & vbCrLf
        S &= "NOTE: Only the GND side of the W24 cable is used." & vbCrLf
        DisplaySetup(S, "TETS DIN_GND.jpg", 1, True, 1, 2)
        If AbortTest = True Then GoTo TestComplete
Step2:
        'Connect X1 probe to DMM INPUTS HI as follows:
        S = "Test the Analog Probe X1 switch as follows:" & vbCrLf
        S &= "1. Move the switch on the Analog Probe to the X1 position." & vbCrLf
        S &= "2. Connect the Analog Probe TIP to DMM INPUT-HI." & vbCrLf
        S &= "3. Press 'Continue' to continue test." & vbCrLf
        S &= "4. Then press the Probe Button (hold for 1 second) within 15 sec." & vbCrLf
        DisplaySetup(S, "TETS APR-1X.jpg", 4, True, 2, 2)
        If AbortTest = True Then GoTo TestComplete
        If GoBack = True Then GoTo Step1

        frmSTest.proProgress.Value = 35

        Ret = ButtonOff
        For i = 1 To 15
            ReadReceiverSwitches()
            If AprobeButton = True Then
                Ret = ButtonPressed
                Exit For
            End If
            Delay(1)
        Next i
        Application.DoEvents()

        frmSTest.proProgress.Value = 40
        If AbortTest = True Then GoTo TestComplete

        nSystErr = WriteMsg(DMM, "*RST")
        nSystErr = WriteMsg(DMM, "*CLS")
        nSystErr = WriteMsg(DMM, "MEAS:RES?")
        'APR-02-001
        dMeasurement = FetchMeasurement(DMM, "Probe Button X1 Test", "", "500", "Ohms", APROBE, "APR-02-001")
        frmSTest.proProgress.Value = 50
        If AbortTest = True Then GoTo TestComplete

        Select Case Ret
            'APR-02-D01
            Case ButtonPressed
                'The call was successful and the button was depressed before time-out.
                If dMeasurement > 500 Then
                    Echo("APR-02-D01 The call was successful and the button was depressed before time-out.")
                    RegisterFailure(APROBE, "APR-02-D01", dMeasurement, "Ohms", 0, 500, "The call was successful and the button was depressed before time-out.")
                    TestAnalogProbe = FAILED
                    If OptionFaultMode = SOFmode Then GoTo TestComplete
                End If
                'APR-02-D02
            Case ButtonOff
                'The call was successful and the button was NOT depressed before time-out.
                Echo("APR-02-D02")
                Echo("The Probe Button is not depressed within the allowed time period, " & waittime & " sec.")
                RegisterFailure(APROBE, "APR-02-D02", dMeasurement, "Ohms", 0, 500, "The Probe Button is not depressed within the allowed time period.")
                TestAnalogProbe = FAILED
                If OptionFaultMode = SOFmode Then GoTo TestComplete
                'APR-02-D03

            Case Else
                'The call was NOT successful.
                Echo("APR-02-D03 The call was NOT successful")
                RegisterFailure(APROBE, "APR-02-D03", dMeasurement, "Ohms", 0, 500, "The call was NOT successful.")
                TestAnalogProbe = FAILED
                If OptionFaultMode = SOFmode Then GoTo TestComplete
        End Select

        'Start of X10 probe test
        ButtonOff = -1
        ButtonPressed = 0
        S = "Test the Analog Probe X10 switch as follows:" & vbCrLf
        S &= "1. Move the switch on the Analog Probe to the X10 position." & vbCrLf
        S &= "2. Connect the Analog Probe TIP  to   DMM INPUT HI." & vbCrLf
        S &= "3. Press 'Continue' to continue test." & vbCrLf
        S &= "4. Then press the Probe Button (hold for 1 second) within 15 sec." & vbCrLf
        DisplaySetup(S, "TETS APR-10X.jpg", 4)
        If AbortTest = True Then GoTo TestComplete

        Application.DoEvents()
        Ret = ButtonOff
        For i = 1 To 15
            ReadReceiverSwitches()
            If AprobeButton = True Then
                Ret = ButtonPressed
                Exit For
            End If
            Delay(1)
        Next i
        Application.DoEvents()

        frmSTest.proProgress.Value = 60
        If AbortTest = True Then GoTo TestComplete

        nSystErr = WriteMsg(DMM, "*RST")
        nSystErr = WriteMsg(DMM, "*CLS")
        nSystErr = WriteMsg(DMM, "MEAS:RES?")
        'APR-03-001
        dMeasurement = FetchMeasurement(DMM, "Probe Button X10 Test", "8.5E6", "9.5E6", "Ohms", APROBE, "APR-03-001")

        frmSTest.proProgress.Value = 70
        If AbortTest = True Then GoTo TestComplete

        Select Case Ret
            'APR-03-D01
            Case ButtonPressed
                'The call was successful and the button was depressed before time-out.
                If dMeasurement < 8500000.0 Or dMeasurement > 9500000.0 Then
                    Echo("APR-03-D01 The call was successful and the button was depressed before time-out.")
                    RegisterFailure(APROBE, "APR-03-D01", dMeasurement, "Ohms", 8500000, 9500000, "The call was successful and the button was depressed before time-out.")
                    TestAnalogProbe = FAILED
                    If OptionFaultMode = SOFmode Then GoTo TestComplete
                End If
                'APR-03-D02
            Case ButtonOff
                'The call was successful and the button was NOT depressed before time-out.
                Echo("APR-03-D02 The call was successful and the button was NOT depressed before time-out")
                Echo("The Probe Button is not depressed within the allowed time period, " & waittime & " sec.")
                RegisterFailure(APROBE, "APR-03-D02", dMeasurement, "Ohms", 8500000, 9500000, "The Probe Button is not depressed within the allowed time period.")
                TestAnalogProbe = FAILED
                If OptionFaultMode = SOFmode Then GoTo TestComplete
                'APR-03-D03

            Case Else
                'The call was NOT successful.
                Echo("APR-02-D03 The call was NOT successful")
                RegisterFailure(APROBE, "APR-02-D03", dMeasurement, "Ohms", 8500000, 9500000, "The call was NOT successful.")
                TestAnalogProbe = FAILED
                If OptionFaultMode = SOFmode Then GoTo TestComplete
        End Select

        'Start of reference probe test
        ButtonOff = -1
        ButtonPressed = 0

        S = "Test the Analog Probe Ref switch as follows:" & vbCrLf
        S &= "1. Move the switch on the Analog Probe to the Ref position." & vbCrLf
        S &= "2. Connect the Analog Probe TIP   to   DMM INPUT HI." & vbCrLf
        S &= "3. Press 'Continue' to continue test." & vbCrLf
        S &= "4. Then press the Probe Button (hold for 1 second) within 15 sec." & vbCrLf
        DisplaySetup(S, "TETS APR-REF.jpg", 4)
        If AbortTest = True Then GoTo TestComplete

        Application.DoEvents()
        Ret = ButtonOff
        For i = 1 To 15
            ReadReceiverSwitches()
            If AprobeButton = True Then
                Ret = ButtonPressed
                Exit For
            End If
            Delay(1)
        Next i
        Application.DoEvents()

        frmSTest.proProgress.Value = 80
        If AbortTest = True Then GoTo TestComplete

        nSystErr = WriteMsg(DMM, "*RST")
        nSystErr = WriteMsg(DMM, "*CLS")
        nSystErr = WriteMsg(DMM, "MEAS:RES?")
        'APR-04-001
        dMeasurement = FetchMeasurement(DMM, "Probe Button Reference Test", "8.5E6", "9.5E6", "Ohms", APROBE, "APR-04-001")

        frmSTest.proProgress.Value = 90
        If AbortTest = True Then GoTo TestComplete

        Select Case Ret
            'APR-04-D01
            Case ButtonPressed
                'The call was successful and the button was depressed before time-out.
                If dMeasurement < 8500000.0 Or dMeasurement > 9500000.0 Then
                    Echo("APR-03-D01 The call was successful and the button was depressed before time-out.")
                    RegisterFailure(APROBE, "APR-03-D01", dMeasurement, "Ohms", 8500000, 9500000, "The call was successful and the button was depressed before time-out.")
                    TestAnalogProbe = FAILED
                    If OptionFaultMode = SOFmode Then GoTo TestComplete
                End If
                'APR-04-D02
            Case ButtonOff
                'The call was successful and the button was NOT depressed before time-out.
                Echo("APR-03-D02")
                Echo("The Probe Button is not depressed within the allowed time period, " & waittime & " sec.")
                RegisterFailure(APROBE, "APR-03-D02", dMeasurement, "Ohms", 8500000, 9500000, "The call was successful and the button was NOT depressed before time-out.")
                TestAnalogProbe = FAILED
                If OptionFaultMode = SOFmode Then GoTo TestComplete
                'APR-04-D03

            Case Else
                'The call was NOT successful.
                Echo("APR-03-D03 The call was NOT successful.")
                RegisterFailure(APROBE, "APR-03-D03", dMeasurement, "Ohms", 8500000, 9500000, "The call was NOT successful.")
                TestAnalogProbe = FAILED
                If OptionFaultMode = SOFmode Then GoTo TestComplete
        End Select
        frmSTest.proProgress.Value = 100



TestComplete:
        frmSTest.cmdAbort.Text = "Abort Test"
        frmSTest.cmdPause.Text = "Pause Test"
        nSystErr = WriteMsg(SWITCH1, "RESET") ' open all switches
        MsgBox("Remove the ANALOG PROBE from the ANALOG PROBE jack on the receiver.", MsgBoxStyle.OkOnly)

        If AbortTest = True Then
            If TestAnalogProbe = FAILED Then
                ReportFailure(APROBE)
            Else
                ReportUnknown(APROBE)
                TestAnalogProbe = UNTESTED
            End If
            sMsg = vbCrLf
            sMsg &= "      **********************************************" & vbCrLf
            sMsg &= "      *         Analog Probe tests aborted!        *" & vbCrLf
            sMsg &= "      **********************************************" & vbCrLf
            Echo(sMsg)
        ElseIf TestAnalogProbe = PASSED Then
            ReportPass(APROBE)
        ElseIf TestAnalogProbe = FAILED Then
            ReportFailure(APROBE)
        Else
            ReportUnknown(APROBE)
        End If

    End Function

End Module