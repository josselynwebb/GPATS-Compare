'Option Strict Off
Option Explicit On

Imports System.Text

Public Module modRFPMeterTets

    '**************************************************************
    '* Nomenclature   : ATS-TETS SYSTEM SELF TEST                 *
    '*                  RF Power Meter Test                       *
    '* Written By     : Michael McCabe                            *
    '* Version        : 2.0                                       *
    '* Last Update    : Apr 1, 2017                               *
    '* Purpose        : This module contains code for the         *
    '*                  RF Power Meter Self Test                  *
    '**************************************************************
    Dim S As String = ""

    Function TestRFPM() As Integer
        'DESCRIPTION:
        '   This routine runs the RF Power Meter OVP and returns PASSED or FAILED
        'RETURNS:
        '   PASSED if the DMM RF Power Meter test passes or FAILED if a failure occurs

        Dim SelfTest As String = ""
        Dim CalTest As String = ""
        Dim dMeasurement As Double
        Dim strng As String = Space(256)
        Dim strSNHP8481D As String
        Dim strSNHP8481A As String
        Dim strSNHP11708A As String
        Dim FileError As Integer
        Dim sMessage As String      'Fault description
        Dim SError As String            'Error Message
        Dim LoopStepNo As Integer = 1

        TestRFPM = UNTESTED
        MsgBox("Remove all cable connections from the SAIF.")

        FileError = GetPrivateProfileString("Serial Number", "HP8481A", "", strng, 256, sATS_INI)
        strSNHP8481A = StripNullCharacters(Trim(strng))
        strng = Space(256)
        FileError = GetPrivateProfileString("Serial Number", "HP8481D", "", strng, 256, sATS_INI)
        strSNHP8481D = StripNullCharacters(Trim(strng))
        strng = Space(256)
        FileError = GetPrivateProfileString("Serial Number", "HP11708A", "", strng, 256, sATS_INI)
        strSNHP11708A = StripNullCharacters(Trim(strng))

        S = "Use cable W14 to connect the " & vbCrLf & vbCrLf
        S &= "   8481D Power Sensor (SN# " & strSNHP8481D & ") " & vbCrLf
        S &= "   with 30dB attenuator (SN# " & strSNHP11708A & ") " & vbCrLf
        S &= "   to the POWER METER POWER REF jack."
        DisplaySetup(s, "TETS PSHD_PRO.jpg", 1)
        If AbortTest = True Then GoTo testcomplete
        TestRFPM = PASSED

        'RF Power Meter Test Title block
        EchoTitle(InstrumentDescription(RFPM), True)
        EchoTitle("RF Power Meter Test", False)
        frmSTest.proProgress.Maximum = 100
        frmSTest.proProgress.Value = 1

        If InstrumentStatus(DMM) = FAILED And RunningEndToEnd Then
            SError = sGuiLabel(DMM) & FAILED_PREVIOUSLY
            MsgBox(SError, vbExclamation) : Echo(SError)
            TestRFPM = DMM
            GoTo TestComplete
        End If

        '** Perform the Built-In Test
        LoopStepNo = 1
        frmSTest.proProgress.Value = 0
RFPM_1: Echo("RFP-01-001     RF Power Meter Built-In Test. . .")
        nSystErr = WriteMsg(RFPM, "*RST")
        nSystErr = WriteMsg(RFPM, "*CLS")
        nSystErr = WriteMsg(RFPM, "*TST?")
        nSystErr = WaitForResponse(RFPM, 0)

        nSystErr = ReadMsg(RFPM, SelfTest)
        'RPM-01-001
        If Val(SelfTest) <> 0 Or nSystErr <> 0 Then
            Echo(FormatResults(False, "RF Power Meter Built-In Test Result = " & StripNullCharacters(SelfTest)))
            RegisterFailure(RFPM, ReturnTestNumber(RFPM, 1, 1, "0"), , , , , "Built-In Test FAILED")
            TestRFPM = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            Echo(FormatResults(True, "RF Power Meter Built-In Test"))
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "RFPM" And OptionStep = LoopStepNo Then
            GoTo RFPM_1
        End If
        frmSTest.proProgress.Value = 10
        LoopStepNo += 1

        'Set up instructions
        S = "Use cable W25 (OBSERVE POLARITY) to connect " & vbCrLf & vbCrLf
        S &= "   DMM INPUT (HI/LO)   to   " & vbCrLf
        S &= "   RF POWER METER Recorder. "
        DisplaySetup(S, "TETS DIN_PMO.jpg", 1)
        If AbortTest = True Then GoTo testcomplete

        '*** Calibrate Power Meter ***
RFPM_2: Echo("RFP-02-001     Calibration Test (8481D). . .")
        nSystErr = WriteMsg(RFPM, "*RST")
        nSystErr = WriteMsg(RFPM, "*CLS")
        Delay(0.5)
        nSystErr = WriteMsg(RFPM, "CAL:CSET ""HP8481D""")
        nSystErr = WriteMsg(RFPM, "CAL:CSET:STAT ON")
        nSystErr = WriteMsg(RFPM, "CAL:ALL?")
        nSystErr = WaitForResponse(RFPM, 0)

        nSystErr = ReadMsg(RFPM, CalTest)

        'RPM-02-001
        If Val(CalTest) <> 0 Or CalTest = "" Or nSystErr <> 0 Then
            Echo("    Power Meter Failed to Calibrate. <" & CStr(Val(CalTest)) & "> ")
            RegisterFailure(RFPM, ReturnTestNumber(RFPM, 2, 1), , , , , "Power Meter Failed to Calibrate.")
            TestRFPM = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            Echo("    Calibration Successful.")
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "RFPM" And OptionStep = LoopStepNo Then
            GoTo RFPM_2
        End If
        frmSTest.proProgress.Value = 30
        LoopStepNo += 1

        '*** Measure Reference, check to see if it is in range ***
RFPM_3: nSystErr = vxiClear(RFPM)
        nSystErr = WriteMsg(RFPM, "*RST")
        nSystErr = WriteMsg(RFPM, "*CLS")
        nSystErr = WriteMsg(RFPM, "UNIT:POW DBM")
        nSystErr = WriteMsg(RFPM, "OUTP:ROSC:STAT ON")
        nSystErr = WriteMsg(RFPM, "MEAS:POW:AC? -30 DBM, MIN, 50E6")
        nSystErr = WaitForResponse(RFPM, 0)

        'RPM-03-001
        dMeasurement = FetchMeasurement(RFPM, "Reference Test (8481D)", "-30.2", "-29.8 ", "dBm", RFPM, "RPM-03-001")
        If dMeasurement < -30.2 Or dMeasurement > -29.8 Then
           'RPM-03-D01
            dFH_Meas = dMeasurement
            Echo("RFP-03-D01     Reference Test (8481D)")
            Echo("    HP8481D SN# " & strSNHP8481D & " and HP11708A SN# " & strSNHP11708A & _
                  " are consider a matched set due to calibration.  Ensure use of proper" & _
                  " sensor and 30 db Reference Attenuator.")
            RegisterFailure(RFPM, ReturnTestNumber(RFPM, 3, 1, "D"), dMeasurement, "dBm", -30.2, -29.8, "Reference Test (8481D)")
            TestRFPM = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "RFPM" And OptionStep = LoopStepNo Then
            GoTo RFPM_3
        End If
        frmSTest.proProgress.Value = 40
        LoopStepNo += 1

        '*** Calibrate Power Meter ***
        S = "Replace the 8481D with the 8481A Power Sensor as follows: " & vbCrLf & vbCrLf
        S &= "1. Disconnect the 8481D Power Sensor and 30dB attenuator from the POWER REF and from cable W14." & vbCrLf
        S &= "2. Connect the HP8481A power sensor (SN# " & strSNHP8481A & ") to cable W14 and to the POWER REF." & vbCrLf
        DisplaySetup(s, "TETS PSH_PRO.jpg", 2)
        If AbortTest = True Then GoTo testcomplete
RFPM_4: Echo("RFP-04-001     Calibration Test (8481A). . .")
        nSystErr = WriteMsg(RFPM, "*RST;*CLS")
        nSystErr = WriteMsg(RFPM, "CAL:CSET ""HP8481A""")
        nSystErr = WriteMsg(RFPM, "CAL:CSET:STAT ON")
        nSystErr = WriteMsg(RFPM, "CAL:ALL?")
         nSystErr = WaitForResponse(RFPM, 0)

        nSystErr = ReadMsg(RFPM, CalTest)
        'RPM-04-001
        If Val(CalTest) <> 0 Or CalTest = "" Or nSystErr <> 0 Then
            Echo("    Power Meter Failed to Calibrate. <" & CStr(Val(CalTest)) & "> ")
            RegisterFailure(RFPM, ReturnTestNumber(RFPM, 4, 1), , , , , "Power Meter Failed to Calibrate.")
            TestRFPM = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            Echo("    Calibration Successful.")
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "RFPM" And OptionStep = LoopStepNo Then
            GoTo RFPM_4
        End If
        frmSTest.proProgress.Value = 60
        LoopStepNo += 1

        '*** Measure Reference, check to see if it is in range ***
        ' nSystErr = vxiClear(RFPM)
RFPM_5: nSystErr = vxiClear(RFPM)
        nSystErr = WriteMsg(RFPM, "*RST;*CLS")
        nSystErr = WriteMsg(RFPM, "UNIT:POW DBM")
        nSystErr = WriteMsg(RFPM, "OUTP:ROSC:STAT ON")
        nSystErr = WriteMsg(RFPM, "MEAS:POW:AC? 0DBM, MIN, 50E6")
        nSystErr = WaitForResponse(RFPM, 0)

        'RPM-05-001
        dMeasurement = FetchMeasurement(RFPM, "Reference Test", "-.02", "+.02", "dBm", RFPM, "RPM-05-001")

        If dMeasurement < -0.02 Or dMeasurement > 0.02 Then
            TestRFPM = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "RFPM" And OptionStep = LoopStepNo Then
            GoTo RFPM_5
        End If
        frmSTest.proProgress.Value = 70
        LoopStepNo += 1

        '*** Recorder Output Test High ***
RFPM_6: nSystErr = WriteMsg(RFPM, "POW:RANG -5 DBM")
        Delay(2)
        nSystErr = WriteMsg(DMM, "*RST;*CLS")
        nSystErr = WriteMsg(DMM, "MEAS:VOLT? 1")
        'RPM-06-001
        dMeasurement = FetchMeasurement(DMM, "Recorder Out High", ".8", "", "V", RFPM, "RPM-06-001")
        If dMeasurement < 0.8 Or dMeasurement > 9.0E+38 Then
            TestRFPM = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "RFPM" And OptionStep = LoopStepNo Then
            GoTo RFPM_6
        End If
        frmSTest.proProgress.Value = 80
        LoopStepNo += 1

        '*** Recorder Output Test Low ***
RFPM_7: nSystErr = WriteMsg(RFPM, "POW:RANG 20 DBM")
        Delay(0.5)
        nSystErr = WriteMsg(DMM, "*RST;*CLS")
        nSystErr = WriteMsg(DMM, "MEAS:VOLT? 1")
        'RPM-07-001
        dMeasurement = FetchMeasurement(DMM, "Recorder Out Low", "", ".2", "V", RFPM, "RPM-07-001")

        If dMeasurement > 0.2 Then
            TestRFPM = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "RFPM" And OptionStep = LoopStepNo Then
            GoTo RFPM_7
        End If
        frmSTest.proProgress.Value = 90
        LoopStepNo += 1

        '*** Test Power Meter Interface ***
Step1:
        S = "Move the 8481A from the POWER REF to the reciever as follows: " & vbCrLf & vbCrLf
        S &= "1. Disconnect the 8481A from the POWER REF." & vbCrLf
        S &= "2. Connect the HP8481A power sensor to the POWER METER SENSOR (P5) on the ATS receiver under the SAIF." & vbCrLf
        DisplaySetup(S, "TETS PBI_PSH.jpg", 1, True, 1, 2)
        If AbortTest = True Then GoTo testcomplete
Step2:
        S = "Use cable W22 to connect  " & vbCrLf & vbCrLf
        S &= "   RF STIM OUTPUT   to  " & vbCrLf
        S &= "   RF POWER METER INPUT."
        DisplaySetup(S, "TETS PMI_STO.jpg", 1, True, 2, 2)
        If AbortTest = True Then GoTo testcomplete
        If GoBack = True Then GoTo Step1

RFPM_8: nSystErr = WriteMsg(RFSYN, "*RST")
        Delay(1)
        nSystErr = WriteMsg(RFSYN, ":FM:STATE OFF;:AM:STATE OFF;:PULM:STATE OFF")
        nSystErr = WriteMsg(RFSYN, ":FREQ 500E6")
        nSystErr = WriteMsg(RFSYN, ":POW 10 DBM")
        nSystErr = WriteMsg(RFSYN, ":OUTP ON")

        ' nSystErr = vxiClear(RFPM)
        nSystErr = vxiClear(RFPM)
        nSystErr = WriteMsg(RFPM, "*RST;*CLS")
        nSystErr = WriteMsg(RFPM, "UNIT:POW DBM")
        nSystErr = WriteMsg(RFPM, "OUTP:ROSC:STAT ON")
        nSystErr = WriteMsg(RFPM, "MEAS:POW:AC? 5DBM, MIN, 500E6")
        nSystErr = WaitForResponse(RFPM, 0)

        'RPM-08-001
        dMeasurement = FetchMeasurement(RFPM, "RF Power Meter Interface Test", "5", "", "dBm", RFPM, "RPM-08-001")
        'RPM-08-D01
        If dMeasurement < 5 Then
            Echo("RFP-08-D01     RF Power Meter Interface Test")
            sMessage = "Failure due to RF Synthesizer or Cabling from the Receiver bulkhead to Receiver System Interface."
            Echo("    " & sMessage)
            RegisterFailure(RFPM, ReturnTestNumber(RFPM, 8, 1, "D"), dMeasurement, "dBm", 5, 9.0E+38, "RF Power Meter Interface Test" & vbCrLf & sMessage)
            TestRFPM = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "RFPM" And OptionStep = LoopStepNo Then
            GoTo RFPM_8
        End If

TestComplete:
        frmSTest.proProgress.Value = 100
        nSystErr = WriteMsg(RFPM, "*RST")
        nSystErr = WriteMsg(RFPM, "*CLS")
        nSystErr = WriteMsg(RFSYN, ":SOUR:ROSC:SOUR INT") ' 10MHZ REFERENCE OUTPUT
        nSystErr = WriteMsg(RFSYN, ":OUTP:ROSC:STAT OFF")
        nSystErr = WriteMsg(RFSYN, "*RST")
        frmSTest.cmdAbort.Text = "Abort Test"
        frmSTest.cmdPause.Text = "Pause Test"


        If AbortTest = True Then
            If TestRFPM = FAILED Then
                ReportFailure(RFPM)
            Else
                ReportUnknown(RFPM)
                TestRFPM = UNTESTED
            End If
            sMsg = vbCrLf
            sMsg &= "      **********************************************" & vbCrLf
            sMsg &= "      *       RF Power Meter tests aborted!        *" & vbCrLf
            sMsg &= "      **********************************************" & vbCrLf
            Echo(sMsg)
        ElseIf TestRFPM = PASSED Then
            ReportPass(RFPM)
        ElseIf TestRFPM = FAILED Then
            ReportFailure(RFPM)
        Else
            ReportUnknown(RFPM)
        End If
    End Function


End Module
