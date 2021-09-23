'Option Strict Off
Option Explicit On

Public Module modRFDownConverter

    '**************************************************************
    '* Nomenclature   : ATS-TETS SYSTEM SELF TEST                 *
    '*                  RF Downconverter Test for eip1315a        *
    '* Written By     : Michael McCabe                            *
    '* Version        : 2.0                                       *
    '* Last Update    : Apr 1, 2017                               *
    '* Purpose        : This module contains code for the         *
    '*                  RF Downconverter Self Test                *
    '**************************************************************

    Const SELFTEST_BITS = &H204
    Dim S As String = ""

    Function TestEIPdownconverter() As Integer
        'DESCRIPTION:
        '   This routine tests the EIP RF Downconverter and reports the results to the caller
        'RETURNS:
        '   PASSED if the test passes, FAILED if it fails

        Dim InitStatus As Integer
        Dim DCLevel As String = ""
        Dim dMeasurement As Double
        Dim LOFrequency As Double
        Dim PowerRef As Double
        Dim trash As String = ""
        Dim iStepCounter As Integer     'Counter to track step number in Loop, used as the Sub Test number


        'RF Downconverter Test Tiltle block
        EchoTitle(InstrumentDescription(RFDOWN), True)
        EchoTitle("RF Downconverter Test", False)
        frmSTest.proProgress.Maximum = 100
        frmSTest.proProgress.Value = 1

        TestEIPdownconverter = PASSED
        MsgBox("Remove all cable connections from the SAIF.")

        'Perform Built-In Test
        Echo("RDC-01-001     Downconverter Built-In Test. . .")
        'RDC-01-001
        If CheckBITStatus(RFDOWN) <> PASSED Then
            Echo(FormatResults(False, "Downconverter Built-in Test"))
            RegisterFailure(RFDOWN, ReturnTestNumber(RFDOWN, 1, 1), , , , , sGuiLabel(RFDOWN) & " FAILED Built-in Test")
            TestEIPdownconverter = FAILED
            GoTo TestComplete
        End If  '1
        Echo(FormatResults(True, "Downconverter Built-in Test"))
        frmSTest.proProgress.Value = 5
Step1:
        S = "Use cable  W22 to connect" & vbCrLf & vbCrLf
        S &= "   RF STIM OUTPUT   to " & vbCrLf
        S &= "   RF MEAS INPUT."
        DisplaySetup(S, "TETS RFM_STO.jpg", 1, True, 1, 2)
        If AbortTest = True Then GoTo testcomplete
Step2:
        S = "Use cable W15 to connect" & vbCrLf & vbCrLf
        S &= "   FG OUTPUT   to " & vbCrLf
        S &= "   RF STIM  AM  MODULATION INPUT."
        DisplaySetup(S, "TETS FGO_SAM.jpg", 1, True, 2, 2)
        If AbortTest = True Then GoTo testcomplete
        If GoBack = True Then GoTo Step1

        nSystErr = WriteMsg(RFSYN, "*RST")
        nSystErr = WriteMsg(RFSYN, "*CLS")
        nSystErr = WriteMsg(RFSYN, "FREQ 100E6")
        nSystErr = WriteMsg(RFSYN, "POW -15 DBM")
        nSystErr = WriteMsg(RFSYN, ":OUTP ON")

        InitStatus = atxmlDF_eip1315a_setLO("RF_CNTR_1", 0, MANUAL, 0, IFOUT)

        If CheckBITStatus(RFDOWN) <> PASSED Then
            Echo(FormatResults(False, "Downconverter BIT Status", "RDC-01-B01"))
            RegisterFailure(RFDOWN, ReturnTestNumber(RFDOWN, 1, 1), , , , , sGuiLabel(RFDOWN) & " FAILED BIT Status.")
            TestEIPdownconverter = FAILED
            GoTo TestComplete
        End If  '1

        nSystErr = WriteMsg(RFCOUNTER, "*RST; *CLS")
        nSystErr = WriteMsg(RFCOUNTER, "INP1:IMP 50")
        nSystErr = WriteMsg(RFCOUNTER, "INP2:IMP 50")
        nSystErr = WriteMsg(RFCOUNTER, "INP1:COUP AC")
        nSystErr = WriteMsg(RFCOUNTER, "MEAS1:DC?")
        nSystErr = ReadMsg(RFCOUNTER, DCLevel)
        nSystErr = WriteMsg(RFCOUNTER, "SENS1:EVEN:LEV " & CStr(Val(DCLevel)))
        nSystErr = WriteMsg(RFCOUNTER, "SENS1:EVEN:HYST MAX")
        nSystErr = WriteMsg(RFCOUNTER, "MEAS:FREQ? 100E6")

        'RDC-02-001
        dMeasurement = FetchMeasurement(RFCOUNTER, "LO = 0 Hz", "90E6", "110E6", "Hz", RFDOWN, "RDC-02-001")
        nSystErr = vxiClear(RFCOUNTER)
        nSystErr = WriteMsg(RFCOUNTER, "*RST; *CLS")
        nSystErr = WriteMsg(RFCOUNTER, "INP1:IMP 50")
        nSystErr = WriteMsg(RFCOUNTER, "INP2:IMP 50")
        If dMeasurement < 90000000.0# Or dMeasurement > 110000000.0# Then
            TestEIPdownconverter = FAILED
            IncStepFailed()
            If (OptionFaultMode = SOEmode) And (OptionMode <> LOSmode) Then
                dFH_Meas = dMeasurement
                nSystErr = WriteMsg(RFMODANAL, "PG99RE")
                nSystErr = WriteMsg(RFMODANAL, "CL")
                'RDC-02-D01
                dMeasurement = GetModMeterMeasurement("RDC-02-D01     LO = 0 Hz Diagnostic 1", "FRHZTV", "90E6", "110E6", "Hz", 300000000.0#)
                If dMeasurement < 90000000.0# Or dMeasurement > 110000000.0# Then
                    S = "Disconnect cable W22 from the SAIF." & vbCrLf
                    S &= "Use cable W27 to connect " & vbCrLf & vbCrLf
                    S &= "    RF STIM OUTPUT   to " & vbcrlf
                    S &= "    C/T INPUT 1" & vbCrLf
                    DisplaySetup(S, "TETS CI1_STO.jpg", 1)
                    If AbortTest = True Then GoTo testcomplete
                    nSystErr = WriteMsg(RFSYN, "POW 10 DBM")
                    nSystErr = WriteMsg(RFCOUNTER, "*RST; *CLS")
                    nSystErr = WriteMsg(RFCOUNTER, "INP1:IMP 50")
                    nSystErr = WriteMsg(RFCOUNTER, "INP2:IMP 50")
                    nSystErr = WriteMsg(COUNTER, "ATIM:TIME 60")
                    nSystErr = WriteMsg(COUNTER, "ATIM OFF")
                    nSystErr = WriteMsg(COUNTER, "INP1:COUP AC")
                    nSystErr = WriteMsg(COUNTER, "MEAS1:DC?")
                    nSystErr = ReadMsg(COUNTER, DCLevel)
                    nSystErr = WriteMsg(COUNTER, "SENS1:EVEN:LEV " & CStr(Val(DCLevel)))
                    nSystErr = WriteMsg(COUNTER, "SENS1:EVEN:HYST MAX")
                    nSystErr = WriteMsg(COUNTER, "MEAS:FREQ? 100E6")

                    'RDC-02-D02
                    dMeasurement = FetchMeasurement(COUNTER, "LO = 0 Hz Diagnostic 2A", "90E6", "110E6", "Hz", RFDOWN, "RDC-02-D02")
                    'RDC-02-D03         'Dignosic to determine if the RF Synthesizer is malfunctioning
                    If dMeasurement < 90000000.0# Or dMeasurement > 110000000.0# Then
                        Echo("   The " & InstrumentDescription(RFSYN) & " is malfunctioning")
                        RegisterFailure(RFSYN, ReturnTestNumber(RFDOWN, 2, 3, "D"), dFH_Meas, "Hz", 90000000, 110000000, "LO = 0 Hz Diagnostic 2A")
                        TestEIPdownconverter = RFSYN
                    Else
                    End If
                Else
                    'RDC-02-D04
                    If InstrumentStatus(RFCOUNTER) = PASSED And RunningEndToEnd Then
                        Echo("RDC-01-D04     LO = 0 Hz Diagnostic 2A")
                        RegisterFailure(RFDOWN, ReturnTestNumber(RFDOWN, 2, 4, "D"), dFH_Meas, "Hz", 90000000, 110000000, "LO = 0 Hz Diagnostic 2A")
                        GoTo TestComplete
                    End If
                    OpenEmiDoor()
                    If AbortTest = True Then GoTo Testcomplete
Step1b:
                    S = "Disconnect the semi-rigid cable from the DET/IF output"
                    S &= " on the front of Downconverter instrument."
                    DisplaySetup(S, "TETS DISC_DIF.jpg", 1, True, 1, 2)
                    If AbortTest = True Then GoTo testcomplete
Step2b:
                    S = "Use cable W16 to connect" & vbCrLf & vbCrLf
                    S &= "   C/T INPUT 1   to  " & vbCrLf
                    S &= "   RF Downconverter DET/IF Output."
                    DisplaySetup(S, "TETS CI1_DIF.jpg", 1, True, 2, 2)
                    If AbortTest = True Then GoTo testcomplete
                    If GoBack = True Then GoTo Step1b

                    nSystErr = vxiClear(COUNTER)
                    nSystErr = WriteMsg(COUNTER, "*RST; *CLS")
                    nSystErr = WriteMsg(COUNTER, "INP1:IMP 50")
                    nSystErr = WriteMsg(COUNTER, "INP2:IMP 50")
                    nSystErr = WriteMsg(COUNTER, "ATIM:TIME 60")
                    nSystErr = WriteMsg(COUNTER, "ATIM OFF")
                    nSystErr = WriteMsg(COUNTER, "MEAS:FREQ?")
                    'RDC-02-D05
                    dMeasurement = FetchMeasurement(COUNTER, "LO = 0 Hz Diagnostic 2B", "90E6", "110E6", "Hz", RFDOWN, "RDC-02-D05")

                    If dMeasurement < 90000000.0# Or dMeasurement > 110000000.0# Then
                        'RDC-02-D06         'Diagnostic to determine if the RF Counter is malfunctioning
                    Else
                        Echo("RDC-02-D06     LO = 0 Hz Diagnostic 2B Failed.")
                        Echo("   The " & InstrumentDescription(RFCOUNTER) & " is malfunctioning")
                        RegisterFailure(RFCOUNTER, ReturnTestNumber(RFDOWN, 2, 6, "D"), dFH_Meas, "Hz", 90000000, 110000000, "LO = 0 Hz Diagnostic 2B Failed")
                        TestEIPdownconverter = RFCOUNTER
                    End If
                End If
                GoTo TestComplete
            End If
        Else
            IncStepPassed()
        End If

        frmSTest.proProgress.Value = 20
        iStepCounter = 1

        'Check all down conveter LO Frequencies
        For LOFrequency# = 420 To 8400 Step 210
            nSystErr = WriteMsg(RFSYN, "FREQ " & CStr((LOFrequency * 1000000.0#) + 100000000.0#))
            InitStatus = atxmlDF_eip1315a_setLO("RF_CNTR_1", LOFrequency, MANUAL, 0, IFOUT)
            Delay(0.1)
            'RDC-03-001 ~ RDC-03-039
            If CheckBITStatus(RFDOWN) <> PASSED Then
                IncStepFailed()
                Echo("RDC-03-" & Format(iStepCounter, "000") & "     Check BIT Status Failed")
                RegisterFailure(RFDOWN, ReturnTestNumber(RFDOWN, 3, iStepCounter), , , , , sGuiLabel(RFDOWN) & " FAILED BIT Status.")
                TestEIPdownconverter = FAILED
                GoTo TestComplete
            Else
                IncStepPassed()
            End If  '1
            Delay(0.1)
            nSystErr = WriteMsg(RFCOUNTER, "MEAS:FREQ? 100E6")
            'RDC-04-001 ~ RDC-04-039
            dMeasurement = FetchMeasurement(RFCOUNTER, "LO =" & CStr(LOFrequency#) & " MHz", "90E6", "110E6", "Hz", RFDOWN, "RDC-04-" & Format(iStepCounter, "000"))

            If dMeasurement < 90000000.0# Or dMeasurement > 110000000.0# Then
                IncStepFailed()
                TestEIPdownconverter = FAILED
                GoTo TestComplete
            Else
                IncStepPassed()
            End If
            frmSTest.proProgress.Value = frmSTest.proProgress.Value + 1
            iStepCounter = iStepCounter + 1         'Bump Step Counter
        Next LOFrequency#
        frmSTest.proProgress.Value = 60

        'Check attenuator, 0dBm
        nSystErr = WriteMsg(RFSYN, "*RST")
        nSystErr = WriteMsg(RFSYN, "*CLS")
        nSystErr = WriteMsg(RFSYN, "FREQ 100E6")
        nSystErr = WriteMsg(RFSYN, "POW -15 DBM")
        nSystErr = WriteMsg(RFSYN, ":OUTP ON")

        InitStatus = atxmlDF_eip1315a_setLO("RF_CNTR_1", 0, MANUAL, 0, DETOUT)

        'RDC-05-001
        If CheckBITStatus(RFDOWN) <> PASSED Then
            IncStepFailed()
            Echo("RDC-05-001     Check BIT Status Failed")
            RegisterFailure(RFDOWN, ReturnTestNumber(RFDOWN, 5, 1), , , , , sGuiLabel(RFDOWN) & " FAILED BIT Status.")
            TestEIPdownconverter = FAILED
            GoTo TestComplete
        Else
            IncStepPassed()
        End If  '1

        nSystErr = WriteMsg(RFMODANAL, "PG99RE")
        nSystErr = WriteMsg(RFMODANAL, "CL")
        Delay(1)
        'RDC-06-001
        PowerRef# = GetModMeterMeasurement("RDC-06-001     Attenuator 0 dBm", "RLAUTV", "-5", "5", "dBm", 100)
        If PowerRef# < -5 Or PowerRef# > 5 Then
            TestEIPdownconverter = FAILED
            IncStepFailed()
            If (OptionFaultMode = SOEmode) And (OptionMode <> LOSmode) Then
                RegisterFailure(RFSYN, ReturnTestNumber(RFDOWN, 6, 1), PowerRef#, "dBm", -5, 5, "Attenuator 0 dBm")
                dFH_Meas = PowerRef#
                S = "Disconnect the W22 cable from the SAIF." & vbCrLf
                S &= "Connect the RF POWER Sensor (HP8481A)  to" & vbCrLf
                S &= "  the RF STIM OUTPUT." & vbCrLf
                DisplaySetup(S, "TETS PSH_STO.jpg", 1)
                If AbortTest = True Then GoTo testcomplete
                nSystErr = WriteMsg(RFPM, "*RST; *CLS")
                nSystErr = WriteMsg(RFPM, "UNIT:POW DBM")
                nSystErr = WriteMsg(RFPM, "CAL:CSET ""HP8481A""")
                nSystErr = WriteMsg(RFPM, "CAL:CSET:STATE ON")
                nSystErr = WriteMsg(RFPM, "MEAS:POW:AC? -16 DBM, MIN, 100 MHz")
                'RDC-06-D01
                dMeasurement = FetchMeasurement(RFPM, "Attenuator 0 dBm Diagnostic 1", "-20", "10", "dBm", RFPM, "RDC-06-D01")

                If dMeasurement < -20 Then
                    TestEIPdownconverter = RFSYN
                Else
                    OpenEmiDoor()
                    If AbortTest = True Then GoTo Testcomplete
Step1c:
                    S = "Remove the 8481 Power Meter Sensor." & vbCrLf
                    S &= "Use cable W22 to reconnect " & vbCrLf & vbCrLf
                    S &= "  RF STIM Output   to " & vbCrLf
                    S &= "  RF Measurement Input."
                    DisplaySetup(S, "TETS RFM_STO.jpg", 1, True, 1, 3)
                    If AbortTest = True Then GoTo testcomplete
Step2c:
                    S = "Disconnect the semi-rigid cable from the IF output on the front of Downconverter instrument."
                    DisplaySetup(S, "TETS DISC_IFO2.jpg", 1, True, 2, 3)
                    If AbortTest = True Then GoTo testcomplete
                    If GoBack = True Then GoTo Step1c
Step3c:
                    S = "Use cable W27 and an N-Female to N-Female Adapter to connect" & vbCrLf & vbCrLf
                    S &= "  RF POWER METER Power Sensor Head (HP8481A)   to" & vbCrLf
                    S &= "  IF Output on the front of the RF DOWNCONVERTER."
                    DisplaySetup(S, "TETS IFO_PSH.jpg", 1, True, 3, 3)
                    If AbortTest = True Then GoTo testcomplete
                    If GoBack = True Then GoTo Step2c

                    nSystErr = WriteMsg(RFPM, "MEAS:POW:AC? 3 DBM, MIN, 100 MHz")
                    'RDC-06-D02
                    dMeasurement = FetchMeasurement(RFPM, "Attenuator 0 dBm Diagnostic 2", "0", "20", "dBm", RFPM, "RDC-06-D02")

                    If dMeasurement < 0 Or dMeasurement > 20 Then
                    Else
                        TestEIPdownconverter = RFMODANAL

                    End If
                End If
                GoTo TestComplete
            End If
        Else
            IncStepPassed()
        End If
        frmSTest.proProgress.Value = 70

        'Check attenuator, -20 dBm
        nSystErr = WriteMsg(RFSYN, "POW 5 DBM")
        InitStatus = atxmlDF_eip1315a_setLO("RF_CNTR_1", 0, MANUAL, 20, DETOUT)
        'RDC-07-001
        If CheckBITStatus(RFDOWN) <> PASSED Then
            Echo("RDC-07-001" & "     " & sGuiLabel(RFDOWN) & " FAILED BIT Status.")
            RegisterFailure(RFDOWN, ReturnTestNumber(RFDOWN, 7, 1), , , , , "Attenuator, -20 dBm")
            TestEIPdownconverter = FAILED
            GoTo TestComplete
        End If  '1

        Delay(1)
        'RDC-08-001
        dMeasurement = GetModMeterMeasurement("RDC-08-001 Attenuator -20 dBm", "RLAUTV", CStr(PowerRef# - 1), CStr(PowerRef# + 2), "dBm", 100)
        If dMeasurement < PowerRef# - 1 Or dMeasurement > PowerRef# + 2 Then
            RegisterFailure(RFDOWN, ReturnTestNumber(RFDOWN, 8, 1), Val(dMeasurement), "dBm", PowerRef# - 1, PowerRef# + 2, "Attenuator -20 dBm")
            TestEIPdownconverter = FAILED
            GoTo TestComplete
        End If
        frmSTest.proProgress.Value = 80

        frmSTest.proProgress.Value = 95

        'Check detector
        nSystErr = WriteMsg(RFSYN, "*RST")
        nSystErr = WriteMsg(RFSYN, "*CLS")
        nSystErr = WriteMsg(RFSYN, "FREQ 1950E6")
        nSystErr = WriteMsg(RFSYN, "POW -25 DBM")
        nSystErr = WriteMsg(RFSYN, ":FM:STATE OFF;:PULM:STATE OFF;:AM:STATE ON")
        nSystErr = WriteMsg(RFSYN, ":OUTP ON")

        nSystErr = WriteMsg(FGEN, "*RST;*CLS")
        nSystErr = WriteMsg(FGEN, "FUNC:SHAP SIN")
        nSystErr = WriteMsg(FGEN, "VOLT 1.0")
        nSystErr = WriteMsg(FGEN, "VOLT:OFFS 0")
        nSystErr = WriteMsg(FGEN, "FREQ 1E4")
        nSystErr = WriteMsg(FGEN, "OUTP ON")

        InitStatus = atxmlDF_eip1315a_setLO("RF_CNTR_1", 1890, MANUAL, 0, DETOUT)
        Delay(0.2)
        'RDC-09-001
        If CheckBITStatus(RFDOWN) <> PASSED Then
            IncStepFailed()
            Echo(FormatResults(False, sGuiLabel(RFDOWN) & " Check BIT Status Failed", "RDC-09-001"))
            RegisterFailure(RFDOWN, ReturnTestNumber(RFDOWN, 9, 1), , , , , sGuiLabel(RFDOWN) & " FAILED BIT Status.")
            TestEIPdownconverter = FAILED
            GoTo TestComplete
        Else
            IncStepPassed()
        End If  '1

        nSystErr = vxiClear(RFCOUNTER)
        nSystErr = WriteMsg(RFCOUNTER, "*RST; *CLS")
        nSystErr = WriteMsg(RFCOUNTER, "INP1:IMP 50")
        nSystErr = WriteMsg(RFCOUNTER, "INP2:IMP 50")
        nSystErr = WriteMsg(RFCOUNTER, "INP1:COUP AC")
        nSystErr = WriteMsg(RFCOUNTER, "MEAS1:DC?")
        nSystErr = ReadMsg(RFCOUNTER, DCLevel)
        nSystErr = WriteMsg(RFCOUNTER, "SENS1:EVEN:LEV " & CStr(Val(DCLevel)))
        nSystErr = WriteMsg(RFCOUNTER, "SENS1:EVEN:HYST MAX")

        nSystErr = WriteMsg(RFCOUNTER, "MEAS:FREQ? 1E4")
        'RDC-10-001
        dMeasurement = FetchMeasurement(RFCOUNTER, "RF Detector Test", "9E3", "11E3", "Hz", RFDOWN, "RDC-10-001")
        If dMeasurement < 9000.0# Or dMeasurement > 11000.0# Then
            IncStepFailed()
            TestEIPdownconverter = FAILED
            If (OptionFaultMode = SOEmode) And (OptionMode <> LOSmode) Then
                dFH_Meas = dMeasurement
                nSystErr = WriteMsg(RFMODANAL, "CL P1 AM")
                Delay(3)
                nSystErr = WriteMsg(RFMODANAL, "TV")
                Delay(1)
                nSystErr = ReadMsg(RFMODANAL, trash)
                Delay(6)
                nSystErr = WriteMsg(RFMODANAL, "TV")
                Delay(10)
                'RDC-10-D01
                dMeasurement = FetchMeasurement(RFMODANAL, "RF Detector Test Diagnostic 1", "80", "120", "%", , "RDC-10-D01")
                'RDC-10-D02
                If dMeasurement < 80 Or dMeasurement > 120 Then
                    Echo("RDC-05-D02     RF Detector Test Diagnostic 1")
                    RegisterFailure(FGEN, ReturnTestNumber(RFDOWN, 5, 2, "D"), dFH_Meas, "Hz", 9000, 11000, "RF Detector Test Diagnostic 1")
                    TestEIPdownconverter = FGEN
                    GoTo TestComplete

                Else
                    Echo("RDC-10-D02     RF Detector Test Diagnostic 1")
                    RegisterFailure(RFDOWN, ReturnTestNumber(RFDOWN, 10, 2, "D"), dFH_Meas, "Hz", 9000, 11000, "RF Detector Test Diagnostic 1")
                    GoTo TestComplete
                End If
            End If
        Else
            IncStepPassed()
        End If

        frmSTest.proProgress.Value = 100
TestComplete:
        frmSTest.cmdAbort.Text = "Abort Test"
        frmSTest.cmdPause.Text = "Pause Test"
        nSystErr = WriteMsg(RFSYN, "OUTP OFF") ' turn off synthesizer
        nSystErr = WriteMsg(RFSYN, "*RST")
        nSystErr = WriteMsg(FGEN, "OUTP OFF")
        nSystErr = WriteMsg(FGEN, "*RST")
        nSystErr = WriteMsg(SWITCH1, "RESET") ' open all switches

        Echo("")

        If AbortTest = True Then
            If TestEIPdownconverter = FAILED Then
                ReportFailure(RFDOWN)
            Else
                ReportUnknown(RFDOWN)
                TestEIPdownconverter = UNTESTED
            End If
            sMsg = vbCrLf
            sMsg &= "      **********************************************" & vbCrLf
            sMsg &= "      *     RF Down Converter tests aborted!       *" & vbCrLf
            sMsg &= "      **********************************************" & vbCrLf
            Echo(sMsg)
        ElseIf TestEIPdownconverter = PASSED Then
            ReportPass(RFDOWN)
        ElseIf TestEIPdownconverter = FAILED Then
            ReportFailure(RFDOWN)
        Else
            ReportUnknown(RFDOWN)
        End If
    End Function

    Function CheckBITStatus(iINSTRUMENT As Integer) As Integer
        'DESCRIPTION:
        '   This routine checks the RF Downconverter self test bits and reports
        '   pass or fail to the calling routine.
        'RETURNS:
        '   PASSED if self test bits are 0 or FAILED if a failure bit is turned on
        Dim ErrorBits As Integer
        Dim ErrorStatusRead As Integer
        Dim SelfTestMessage As String
        Dim BitPossition As Integer

        ErrorStatusRead = viIn16(instrumentHandle(iINSTRUMENT), VI_A24_SPACE, SELFTEST_BITS, ErrorBits)
        ErrorBits = ErrorBits And &HFF
        If (ErrorBits = 0) And (ErrorStatusRead = 0) Then
            CheckBITStatus = PASSED
        Else
            CheckBITStatus = FAILED
            SelfTestMessage = ""
            For BitPossition = 0 To 7
                If ErrorBits And (2 ^ BitPossition) Then
                    Select Case BitPossition
                        Case 0
                            SelfTestMessage = "+10V Rail Failure.  "
                        Case 1
                            SelfTestMessage = SelfTestMessage & "+21V Rail Failure.  "
                        Case 2
                            SelfTestMessage = SelfTestMessage & "-21V Rail Failure.  "
                        Case 3
                            SelfTestMessage = SelfTestMessage & "-10V Rail Failure.  "
                        Case 4
                            SelfTestMessage = SelfTestMessage & "+5V Rail Failure.  "
                        Case 5
                            SelfTestMessage = SelfTestMessage & "Reference Generator Module Failure.  "
                        Case 6
                            SelfTestMessage = SelfTestMessage & "Divider Module Failure.  "
                        Case 7
                            SelfTestMessage = SelfTestMessage & "I.F. Module Failure.  "
                    End Select
                End If
            Next BitPossition
            Echo("    Downconverter Self Diagnostic Failure.")
            Echo("    " & SelfTestMessage)
        End If
    End Function


End Module
