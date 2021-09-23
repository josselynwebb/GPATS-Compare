'Option Strict Off
Option Explicit On

Imports System.Text

Public Module modRFPMeterVTsaif


    '=========================================================
    '**************************************************************
    '* Nomenclature   : VIPER/T SYSTEM SELF TEST                  *
    '*                  RF Power Meter Test                       *
    '* Version        : 2.0                                       *
    '* Last Update    : Apr 1, 2017                               *
    '* Purpose        : This module contains code for the         *
    '*                  RF Power Meter Self Test                  *
    '**************************************************************

    Dim gRetVal As Integer
    Dim dCalFactor As Double
    Dim dMeasured As Double
    Const CRFMS_ERROR As String = "RMFS Error"

    Function TestRFPMeterVT() As Integer
        'DESCRIPTION:
        '   This routine runs the RF Power Meter OVP
        ' Returns: PASSED or FAILED

        Dim strSNHP11708A As String = ""
        Dim strng As String = Space(256)
        Dim S As String, S2 As String
        Dim measResult As Single
        Dim calstat As Integer
        Dim sSensorSn As String = ""
        Dim i As Integer

        HelpContextID = 1330

        'RF Power Meter Test Title block
        EchoTitle(InstrumentDescription(RFPM) & " ", True)
        EchoTitle("RF Power Meter Test", False)
        frmSTest.proProgress.Maximum = 100
        frmSTest.proProgress.Value = 1

        TestRFPMeterVT = UNTESTED
        If AbortTest = True Then GoTo TestComplete
        AbortTest = False
        TestRFPMeterVT = PASSED


        'turn on the 10mhz output
        nSystErr = WriteMsg(RFSYN, ":SOUR:ROSC:SOUR INT")
        nSystErr = WriteMsg(RFSYN, ":OUTP:ROSC:STAT ON")


        gRetVal = gRFPM.Close
        gRetVal = gRFPM.Open(1) : checkPMError(True) ' open in simulation mode to get the serial numbers
        gRetVal = gRFPM.setPowerHead("8481D") : checkPMError(True)
        gRetVal = gRFPM.getPowerHeadSerNum(sSensorSn) : checkPMError(True)
        gRetVal = gRFPM.getAttenuatorSerNum(strSNHP11708A) : checkPMError(True)
        gRetVal = gRFPM.Close

        S = GetSN("HP8481D")
        If LCase(Trim(S)) <> LCase(Trim(sSensorSn)) Then
            S2 = "Operator Information: HP8481D serial number in Cal file does not match ViperT.ini file!" + vbCrLf
            S2 += "Cal Data file = " + sSensorSn + vbCrLf
            S2 += "ViperT.Ini file = " + S + vbCrLf
            MsgBox(S2, MsgBoxStyle.OkCancel)
        End If

        S = GetSN("HP11708A") '30 db attenuator
        If LCase(Trim(S)) <> LCase(Trim(strSNHP11708A)) Then
            S2 = "Operator Information: 30 dB attenuator serial number in Cal file does not match ViperT.ini file!" + vbCrLf
            S2 += "Cal Data file = " + strSNHP11708A + vbCrLf
            S2 += "ViperT.Ini file = " + S + vbCrLf
            MsgBox(S2, MsgBoxStyle.OkCancel)
        End If



        S = "Install the HP8481D as follows:" + vbCrLf
        S = S + "1. Connect HP8481D POWER SENSOR S/N " & sSensorSn & " to the W14 cable." + vbCrLf
        S = S + "2. Connect 11708A 30dB attenuator S/N " & strSNHP11708A & " to the HP8481D POWER SENSOR. " & vbCrLf
        S += "3. Connect 8481D POWER SENSOR to RF POWER METER POWER REF jack." + vbCrLf
        DisplaySetup(S, "ST-8481D-1.jpg", 3)
        If AbortTest = True Then GoTo TestComplete

        LastCalibration = ""
        dCalFactor = 100

RFPM_1:  'RFP-01-001  Power Meter Bit tests
        'Zero The Power Meter
        gRetVal = gRFPM.Open(0)
        Sleep(3000)
        If gRetVal <> 0 Then
            checkPMError(True) ' open session execution mode
            RegisterFailure(RFPM, "RFP-01-001", , , , , "RF Power Meter Built-In Tests")
            Echo(FormatResults(False, "Power Meter Built-In Tests", "RFP-01-001"))
            Echo("Error: Power Meter failed BIT tests on open statement.  Starting simulation mode.")
            TestRFPMeterVT = FAILED
            IncStepFailed()
            GoTo TestComplete ' no point in continuing
        Else
            Echo(FormatResults(True, "Power Meter Bit Tests", "RFP-01-001"))
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "RF PM" And OptionStep = 1 Then
            gRetVal = gRFPM.Close
            GoTo RFPM_1
        End If

RFPM_2:  'RFP-02-001  Power Meter Calibration
        frmSTest.proProgress.Value = 20
        Echo("RFP-02-001 8481D Power Meter Calibration (about 45 Seconds)")

        gRetVal = gRFPM.setPowerHead("8481D") : checkPMError(True)
        gRetVal = gRFPM.setMeasureMode(0) : checkPMError(True)
        gRetVal = gRFPM.setMeasureUnits("W") : checkPMError(True)
        gRetVal = gRFPM.setPwrMeterOn_Off(1) : checkPMError(True) ' turns on power meter
        Sleep(5000)

        gRetVal = gRFPM.doZeroAndCal
        If gRetVal = 0 Then
            Do
                gRetVal = gRFPM.getZeroCalStatus(calstat)
                If calstat = 0 Or gRetVal <> 0 Then
                    Exit Do
                End If
                If frmSTest.proProgress.Value < 40 Then
                    frmSTest.proProgress.Value = frmSTest.proProgress.Value + 0.5
                End If
                Application.DoEvents()
                If AbortTest = True Then GoTo TestComplete
                Sleep(500)
            Loop
        End If

        'print the results
        If gRetVal <> 0 Then
            checkPMError(True)
            RegisterFailure(RFPM, "RFP-02-001", , , , , "RF 8481D Power Meter Calibration")
            Echo(FormatResults(False, "8481D Power Meter Calibration", "RFP-02-001"))
            TestRFPMeterVT = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            LastCalibration = "8481D"
            Echo(FormatResults(True, "8481D Power Meter Calibration"))
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "RF PM" And OptionStep = 2 Then
            GoTo RFPM_2
        End If
        frmSTest.proProgress.Value = 40

RFPM_3:  'RFP-03-001  '8481D Measure Reference Power Level
        gRetVal = gRFPM.setRefOsc(1) : checkPMError(True)
        gRetVal = gRFPM.setExpFreq(50, "MHz") : checkPMError(True)
        Delay(5) 'Allow Signal Settling Time
        For i = 1 To 3
            gRetVal = gRFPM.getMeasurement(measResult, "W") : checkPMError(True)
        Next i
        dMeasured = measResult

        sTNum = "RFP-03-001" '
        RecordTest(sTNum, "8481D Measure Reference Power Level ", (0.000001 * 0.95), (0.000001 * 1.05), dMeasured, "W")

        If dMeasured < (0.000001 * 0.95) Or dMeasured > (0.000001 * 1.05) Then
            TestRFPMeterVT = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "RF PM" And OptionStep = 3 Then
            GoTo RFPM_3
        End If
        frmSTest.proProgress.Value = 60


        'RFP-04-001 Test 8481A calibration
        gRetVal = gRFPM.Close ' close session
        gRetVal = gRFPM.Open(1) : checkPMError(True) ' simulation mode
        gRetVal = gRFPM.setPwrMeterOn_Off(0) : checkPMError(True)
        gRetVal = gRFPM.setPowerHead("8481A") : checkPMError(True)
        gRetVal = gRFPM.getPowerHeadSerNum(sSensorSn) : checkPMError(True)
        gRetVal = gRFPM.Close ' close session

        S = GetSN("HP8481A")
        If LCase(Trim(S)) <> LCase(Trim(sSensorSn)) Then
            S2 = "Operator Information: HP8481A serial number in Cal file does not match ViperT.ini file!" + vbCrLf
            S2 += "Cal Data file = " + sSensorSn + vbCrLf
            S2 += "ViperT.Ini file = " + S + vbCrLf
            MsgBox(S2, MsgBoxStyle.OkCancel)
        End If

        S = "Install the 8481A as follows:" + vbCrLf
        S += "1. Remove the 8481D and the 30dB Attenuator." + vbCrLf
        S = S + "2. Connect the HP8481A POWER SENSOR S/N " & sSensorSn & " to the W14 cable." + vbCrLf
        S += "3. Connect the HP8481A to the POWER METER REF." + vbCrLf
        DisplaySetup(S, "ST-8481A-1.jpg", 3)
        If AbortTest = True Then GoTo TestComplete

        Echo("RFP-04-001 8481A Power Meter Calibration (about 45 Seconds)")

        ' Test 8481A calibration
        LastCalibration = ""
        dCalFactor = 100

RFPM_4:
        frmSTest.proProgress.Value = 60
        'Zero The Power Meter
        gRetVal = gRFPM.Open(0)
        Sleep(3000)
        If gRetVal <> 0 Then
            checkPMError(True) ' execution mode
            Echo("Error: Power Meter failed BIT tests on open statement.  Starting simulation mode.")
        End If

        gRetVal = gRFPM.setPwrMeterOn_Off(0)
        gRetVal = gRFPM.setPowerHead("8481A") : checkPMError(True)
        Sleep(3000)
        gRetVal = gRFPM.setMeasureMode(0) : checkPMError(True)
        gRetVal = gRFPM.setMeasureUnits("W") : checkPMError(True)
        gRetVal = gRFPM.setPwrMeterOn_Off(1) : checkPMError(True) ' turns on power meter
        Sleep(5000)

        gRetVal = gRFPM.doZeroAndCal
        If gRetVal = 0 Then
            Do
                gRetVal = gRFPM.getZeroCalStatus(calstat)
                If calstat = 0 Or gRetVal = -1 Then
                    Exit Do
                End If
                If frmSTest.proProgress.Value < 80 Then
                    frmSTest.proProgress.Value = frmSTest.proProgress.Value + 0.5
                End If
                Application.DoEvents()
                If AbortTest = True Then GoTo TestComplete
                Sleep(500)
            Loop
        End If

        'print the results
        If gRetVal <> 0 Then
            checkPMError(True)
            RegisterFailure(RFPM, "RFP-04-001", , , , , "RF 8481A Power Meter Calibration")
            Echo(FormatResults(False, "8481A Power Meter Calibration", "RFP-04-001"))
            TestRFPMeterVT = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            LastCalibration = "8481A"
            Echo(FormatResults(True, "8481A Power Meter Calibration"))
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "RF PM" And OptionStep = 4 Then
            gRetVal = gRFPM.Close ' close session
            GoTo RFPM_4
        End If
        frmSTest.proProgress.Value = 80

        'RFP-05-001  'Measure Reference Power Level 50 Mhz, 1 mW (0db)

RFPM_5:
        gRetVal = gRFPM.setRefOsc(1) : checkPMError(True)
        gRetVal = gRFPM.setExpFreq(50, "MHz") : checkPMError(True)
        Sleep(5000) 'Allow Signal Settling Time
        For i = 1 To 3
            gRetVal = gRFPM.getMeasurement(measResult, "W") : checkPMError(True)
        Next i
        dMeasured = measResult

        sTNum = "RFP-05-001"
        RecordTest(sTNum, "8481A Measure Reference Power Level ", (0.001 * 0.95), (0.001 * 1.05), dMeasured, "W")
        If dMeasured < (0.001 * 0.95) Or dMeasured > (0.001 * 1.05) Then
            TestRFPMeterVT = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "RF PM" And OptionStep = 5 Then
            GoTo RFPM_5
        End If
        frmSTest.proProgress.Value = 100

TestComplete:
        frmSTest.cmdAbort.Text = "Abort Test"
        frmSTest.CmdPause.Text = "Pause Test"
        gRetVal = gRFPM.Reset : checkPMError(False)
        gRetVal = gRFPM.setRefOsc(0) : checkPMError(False)
        gRetVal = gRFPM.setPwrMeterOn_Off(0) : checkPMError(False)
        gRetVal = gRFPM.Close ' end session
        nSystErr = WriteMsg(RFSYN, ":SOUR:ROSC:SOUR INT") ' 10MHZ REFERENCE OUTPUT
        nSystErr = WriteMsg(RFSYN, ":OUTP:ROSC:STAT OFF")

        If AbortTest = True Then
            If TestRFPMeterVT = FAILED Then
                ReportFailure(RFPM)
            Else
                ReportUnknown(RFPM)
            End If
        ElseIf TestRFPMeterVT = PASSED Then
            ReportPass(RFPM)
        ElseIf TestRFPMeterVT = FAILED Then
            ReportFailure(RFPM)
        Else
            ReportUnknown(RFPM)
        End If


    End Function

    Private Sub checkPMError(ByVal EchoErrMsg As Integer)
        Dim message As String
        Dim errorcode As Integer
        Dim ErrorSeverity As Integer
        Dim ErrorDescr As String = ""
        Dim MoreErrorInfo As String = ""

        If gRetVal <> 0 Then
            gRFPM.getError(errorcode, ErrorSeverity, ErrorDescr, 256, MoreErrorInfo, 256)
            message = "Error:  " & errorcode & " --" & ErrorDescr & ". " & vbCrLf & MoreErrorInfo & " "
            '   MsgBox message, vbCritical, CRFMS_ERROR
            If EchoErrMsg = True Then
                Echo(FormatResults(False, message))
            End If
        End If

    End Sub






End Module