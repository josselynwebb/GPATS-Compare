'Option Strict Off
Option Explicit On

Public Module modRFGTDownConverter

    '**************************************************************
    '* Nomenclature   : ATS-TETS SYSTEM SELF TEST                 *
    '*                  RF GT Downconverter Test for gt55210a     *
    '* Written By     : Jay Joiner                                *
    '* Version        : 2.0                                       *
    '* Last Update    : Apr 1, 2017                               *
    '* Purpose        : This module contains code for the         *
    '*                  RF Giga-Tronics Downconverter Self Test   *
    '**************************************************************

    Const SELFTEST_BITS = &H204
    Dim S As String = ""

    Function TestGTdownconverter() As Integer
        'DESCRIPTION:
        '   This routine tests the Giga-Tronics gt55210a RF Downconverter and reports the results to the caller
        'RETURNS:
        '   PASSED if the test passes, FAILED if it fails
        Dim InitStatus As Integer
        Dim DCLevel As String = ""
        Dim dMeasurement As Double
        Dim LOFrequency As Double
        Dim PowerRef As Double
        Dim trash As String = ""
        Dim iStepCounter As Integer     'Counter to track step number in Loop, used as the Sub Test number
        Dim LoopStepNo As Integer = 1

        TestGTdownconverter = UNTESTED
        If Not RunningEndToEnd Then
          MsgBox("Remove all cable connections from the SAIF.")
        End If
        'RF Downconverter Test Title block
        EchoTitle(InstrumentDescription(RFDOWN), True)
        EchoTitle("RF Downconverter Test", False)
        frmSTest.proProgress.Maximum = 100
        frmSTest.proProgress.Value = 1

        frmSTest.proProgress.Value = 5
        If RunningEndToEnd Then
Step1e1e:
            S = "Use cable W15 to connect " & vbCrLf & vbCrLf
            S &= "   FG OUTPUT   to" & vbCrLf
            S &= "   RF STIM  AM  MODULATION INPUT."
            DisplaySetup(S, "TETS FGO_SAM.jpg", 1)
            If AbortTest = True Then GoTo testcomplete
            LoopStepNo = 1
        Else
Step1:
            S = "Use cable W22 to connect" & vbCrLf & vbCrLf
            S &= "   RF STIM OUTPUT   to" & vbCrLf
            S &= "   RF MEAS INPUT."
            DisplaySetup(S, "TETS RFM_STO.jpg", 1, True, 1, 2)
            If AbortTest = True Then GoTo testcomplete
Step2:

            S = "Use cable W15 to connect " & vbCrLf & vbCrLf
            S &= "   FG OUTPUT   to" & vbCrLf
            S &= "   RF STIM  AM  MODULATION INPUT."
            DisplaySetup(S, "TETS FGO_SAM.jpg", 1, True, 2, 2)
            If AbortTest = True Then GoTo testcomplete
            If GoBack = True Then GoTo Step1
            LoopStepNo = 1
        End If
        TestGTdownconverter = PASSED

RFDOWN_1:
        nSystErr = vxiClear(RFSYN)
        nSystErr = WriteMsg(RFSYN, "*RST")
        nSystErr = WriteMsg(RFSYN, "*CLS")


        nSystErr = WriteMsg(RFSYN, "FREQ 100E6")
        nSystErr = WriteMsg(RFSYN, "POW -15 DBM")
        nSystErr = WriteMsg(RFSYN, ":OUTP ON")

        nSystErr = atxmlDF_gt55210a_setLO("RF_CNTR_1", 0, MANUAL, 0, IFOUT)

        If nSystErr <> VI_SUCCESS Then
            Echo("ERROR -  " & sGuiLabel(RFDOWN) & " Failed to initialize.")
            TestGTdownconverter = FAILED
            GoTo TestComplete
        End If  '1 

        nSystErr = vxiClear(RFCOUNTER)
        'set Counter timeout to 10 sec
        nSystErr = atxmlDF_viSetAttribute("RF_CNTR_1", 0, VI_ATTR_TMO_VALUE, 3000) ' 3 sec
        nSystErr = WriteMsg(RFCOUNTER, "*RST")
        nSystErr = WriteMsg(RFCOUNTER, "*CLS")
        nSystErr = WriteMsg(RFCOUNTER, "INP1:IMP 50")
        nSystErr = WriteMsg(RFCOUNTER, "INP2:IMP 50")
        nSystErr = WriteMsg(RFCOUNTER, "INP1:COUP AC")
        nSystErr = WriteMsg(RFCOUNTER, "MEAS1:DC?")
        nSystErr = ReadMsg(RFCOUNTER, DCLevel)
        If DCLevel = "" Then
            DCLevel = "0.000"
        End If
        nSystErr = WriteMsg(RFCOUNTER, "SENS1:EVEN:LEV " & CStr(Val(DCLevel)))
        nSystErr = WriteMsg(RFCOUNTER, "SENS1:EVEN:HYST MAX")
        nSystErr = WriteMsg(RFCOUNTER, "MEAS:FREQ? 100E6")

        'RDC-01-001
        dMeasurement = FetchMeasurement(RFCOUNTER, "LO = 0 Hz", "90E6", "110E6", "Hz", RFDOWN, "RDC-01-001")
        nSystErr = vxiClear(RFCOUNTER)
        nSystErr = WriteMsg(RFCOUNTER, "*RST")
        nSystErr = WriteMsg(RFCOUNTER, "*CLS")
        nSystErr = WriteMsg(RFCOUNTER, "INP1:IMP 50")
        nSystErr = WriteMsg(RFCOUNTER, "INP2:IMP 50")
        If dMeasurement < 90000000.0# Or dMeasurement > 110000000.0# Then
            IncStepFailed()
            dFH_Meas = dMeasurement
            nSystErr = WriteMsg(RFMODANAL, "PG99RE")
            nSystErr = WriteMsg(RFMODANAL, "CL")
            TestGTdownconverter = FAILED
            'RDC-01-D01
            If (OptionFaultMode = SOFmode) And (OptionMode <> LOSmode) Then
                dMeasurement = GetModMeterMeasurement("RDC-01-D01     LO = 0 Hz Diagnostic 1", "FRHZTV", "90E6", "110E6", "Hz", 300000000.0#)
                If dMeasurement < 90000000.0# Or dMeasurement > 110000000.0# Then
                    S = "Disconnect cable W22 from the SAIF. " & vbCrLf
                    S &= "Use cable W27 to connect " & vbCrLf & vbCrLf
                    S &= "    RF STIM OUTPUT   to " & vbcrlf
                    S &= "    C/T INPUT 1."
                    DisplaySetup(S, "TETS CI1_STO.jpg", 1) 'Modified by Kevin Hsu per DR# 152 on 03-21-2001
                    If AbortTest = True Then GoTo testcomplete
                    nSystErr = WriteMsg(RFSYN, "POW 10 DBM")
                    nSystErr = vxiClear(RFCOUNTER)
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
                    'RDC-01-D02
                    dMeasurement = FetchMeasurement(COUNTER, "LO = 0 Hz Diagnostic 2A", "90E6", "110E6", "Hz", RFDOWN, "RDC-01-D02")
                    'RDC-01-D03
                    If dMeasurement < 90000000.0# Or dMeasurement > 110000000.0# Then
                        Echo("RDC-01-D03     LO = 0 Hz Diagnostic 2A")
                        Echo("   The " & InstrumentDescription(RFSYN) & " is malfunctioning")
                        RegisterFailure(RFSYN, ReturnTestNumber(RFDOWN, 1, 3, "D"), dFH_Meas, "Hz", 90000000, 110000000, "LO = 0 Hz Diagnostic 2A")
                        TestGTdownconverter = RFSYN
                    End If
                Else
                    'RDC-01-D04
                    If InstrumentStatus(RFCOUNTER) = PASSED And RunningEndToEnd Then
                        Echo("RDC-01-D04     LO = 0 Hz Diagnostic 2A")
                        RegisterFailure(RFDOWN, ReturnTestNumber(RFDOWN, 1, 4, "D"), dFH_Meas, "Hz", 90000000, 110000000, "LO = 0 Hz Diagnostic 2A")
                        TestGTdownconverter = FAILED
                        GoTo TestComplete
                    End If

                    OpenEmiDoor()
                    If AbortTest = True Then GoTo TestComplete
Step1b:
                    S = "Disconnect the semi-rigid cable from "
                    S &= " DET/IF output on the front of Downconverter instrument. "
                    DisplaySetup(S, "TETS DISC_DIF2.jpg", 1, True, 1, 2)
                    If AbortTest = True Then GoTo testcomplete
Step2b:
                    S = "Use cable W16 to connect" & vbCrLf & vbCrLf
                    S &= "   C/T INPUT 1   to  " & vbCrLf
                    S &= "   RF Downconverter DET/IF Output."
                    DisplaySetup(S, "TETS CI1_DIF2.jpg", 1, True, 2, 2)
                    If AbortTest = True Then GoTo testcomplete
                    If GoBack = True Then GoTo Step1b

                    nSystErr = vxiClear(COUNTER)
                    nSystErr = WriteMsg(COUNTER, "*RST; *CLS")
                    nSystErr = WriteMsg(COUNTER, "INP1:IMP 50")
                    nSystErr = WriteMsg(COUNTER, "INP2:IMP 50")
                    nSystErr = WriteMsg(COUNTER, "ATIM:TIME 60")
                    nSystErr = WriteMsg(COUNTER, "ATIM OFF")
                    nSystErr = WriteMsg(COUNTER, "MEAS:FREQ?")
                    'RDC-01-D05
                    dMeasurement = FetchMeasurement(COUNTER, "LO = 0 Hz Diagnostic 2B", "90E6", "110E6", "Hz", RFDOWN, "RDC-01-D05")

                    If dMeasurement < 90000000.0# Or dMeasurement > 110000000.0# Then
                        TestGTdownconverter = FAILED
                        'RDC-01-D06
                    Else
                        Echo("RDC-01-D06     LO = 0 Hz Diagnostic 2B")
                        Echo("   The " & InstrumentDescription(RFCOUNTER) & " is malfunctioning")
                        RegisterFailure(RFCOUNTER, ReturnTestNumber(RFDOWN, 1, 6, "D"), dFH_Meas, "Hz", 90000000, 110000000, "LO = 0 Hz Diagnostic 2B")
                        TestGTdownconverter = RFCOUNTER
                    End If
                End If
            End If
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "RFDOWN" And OptionStep = LoopStepNo Then
            GoTo RFDOWN_1
        End If
        frmSTest.proProgress.Value = 10
        LoopStepNo += 1

        iStepCounter = 1
        'Check all down conveter LO Frequencies
        For LOFrequency# = 400 To 8640 Step 160

RFDOWN_2:   If LOFrequency# = 2160 Then LOFrequency = 2080
            nSystErr = WriteMsg(RFSYN, "FREQ " & CStr((LOFrequency# * 1000000.0#) - 140000000.0#))
            Delay(0.3)
            InitStatus = atxmlDF_gt55210a_setLO("RF_CNTR_1", LOFrequency#, MANUAL, 0, IFOUT)
            nSystErr = vxiClear(RFCOUNTER)
            nSystErr = WriteMsg(RFCOUNTER, "*RST; *CLS")
            Delay(0.3)
            nSystErr = WriteMsg(RFCOUNTER, "MEAS:FREQ? 140E6")

            'RDC-02-001 ~ RDC-02-053
            S = "LO = " & CStr(LOFrequency#) & " MHz, " & "RFSTIM = " & CStr(LOFrequency - 140) & " MHZ, Diff = 140 Mhz"
            dMeasurement = FetchMeasurement(RFCOUNTER, S, "130E6", "150E6", "Hz", RFDOWN, "RDC-02-" & Format(iStepCounter, "000"))

            If dMeasurement < 130000000.0# Or dMeasurement > 150000000.0# Then
                TestGTdownconverter = FAILED
                IncStepFailed()
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else
                IncStepPassed()
            End If
            Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete
            If OptionMode = LOSmode And OptionTestName = "RFDOWN" And OptionStep = LoopStepNo Then
                GoTo RFDOWN_2
            End If
            LoopStepNo += 1
            frmSTest.proProgress.Value += 1
            iStepCounter = iStepCounter + 1         'Bump Step Counter
        Next LOFrequency#

        frmSTest.proProgress.Value = 60

RFDOWN_3:  'Check attenuator, 0dBm
        nSystErr = WriteMsg(RFSYN, "*RST")
        nSystErr = WriteMsg(RFSYN, "*CLS")
        nSystErr = WriteMsg(RFSYN, "FREQ 100E6")
        nSystErr = WriteMsg(RFSYN, "POW -15 DBM")
        nSystErr = WriteMsg(RFSYN, ":OUTP ON")

        InitStatus = atxmlDF_gt55210a_setLO("RF_CNTR_1", 0, MANUAL, 0, DETOUT)

        nSystErr = WriteMsg(RFMODANAL, "PG99RE") ' init
        nSystErr = WriteMsg(RFMODANAL, "CL")     ' clear error buffer
        Delay(1)
        'RDC-03-001
        PowerRef# = GetModMeterMeasurement("RDC-03-001     Attenuator 0 dBm", "RLAUTV", "-5", "12", "dBm", 100)
        If PowerRef# < -5 Or PowerRef# > 12 Then
            TestGTdownconverter = FAILED
            If (OptionFaultMode = SOFmode) And (OptionMode <> LOSmode) Then
                dFH_Meas = PowerRef
                S = "Disconnect the W22 cable from the SAIF. " & vbCrLf
                S &= "Connect the RF 8481A Power Sensor to the RF STIM OUTPUT. "
                DisplaySetup(S, "TETS PSH_STO.jpg", 1)
                If AbortTest = True Then GoTo testcomplete
                nSystErr = WriteMsg(RFPM, "*RST; *CLS")
                nSystErr = WriteMsg(RFPM, "UNIT:POW DBM")
                nSystErr = WriteMsg(RFPM, "CAL:CSET ""HP8481A""")
                nSystErr = WriteMsg(RFPM, "CAL:CSET:STATE ON")
                nSystErr = WriteMsg(RFPM, "MEAS:POW:AC? -16 DBM, MIN, 100 MHz")
                'RDC-03-D01
                dMeasurement = FetchMeasurement(RFPM, "Attenuator 0 dBm Diagnostic 1", "-20", "10", "dBm", RFDOWN, "RDC-03-D01")
                'RDC-03-D02
                If dMeasurement < -20 Then
                    Echo("RDC-03-D02     Attenuator 0 dBm Diagnostic 1")
                    Echo("   The " & InstrumentDescription(RFSYN) & " is malfunctioning")
                    RegisterFailure(RFSYN, ReturnTestNumber(RFDOWN, 3, 2, "D"), dFH_Meas, "dBm", -20, 10, "Attenuator 0 dBm Diagnostic 1")
                    TestGTdownconverter = RFSYN
                Else
                    OpenEmiDoor()
                    If AbortTest = True Then GoTo Testcomplete

Step1c:
                    S = "Disconnect the 8481A Power Sensor. " & vbCrLf
                    S &= "Use cable W22 to reconnect " & vbCrLf & vbCrLf
                    S &= "    RF STIM OUTPUT   to " & vbCrLf
                    S &= "    RF Measurement Input."
                    DisplaySetup(S, "TETS RFM_STO.jpg", 1, True, 1, 3)
                    If AbortTest = True Then GoTo testcomplete
Step2c:
                    S = "Disconnect the semi-rigid cable from the IF output" & vbCrLf
                    S &= " on the front of Downconverter instrument."
                    DisplaySetup(S, "TETS DISC_IFO2.jpg", 1, True, 2, 3)
                    If AbortTest = True Then GoTo testcomplete
                    If GoBack = True Then GoTo Step1c
Step3c:
                    S = "Use cable W27 and an N-Female to N-Female Adapter  " & vbCrLf & vbCrLf
                    S &= " to connect the 8481A Power Sensor " & vbCrLf
                    S &= " to the IF Output on the front of the RF DOWNCONVERTER."
                    DisplaySetup(S, "TETS IFO_PSH2.jpg", 1, True, 3, 3)
                    If AbortTest = True Then GoTo testcomplete
                    If GoBack = True Then GoTo Step2c

                    nSystErr = WriteMsg(RFPM, "MEAS:POW:AC? 3 DBM, MIN, 100 MHz")
                    'RDC-03-D03
                    dMeasurement = FetchMeasurement(RFPM, "Attenuator 0 dBm Diagnostic 2", "-5", "12", "dBm", RFDOWN, "RDC-03-D03")

                    If dMeasurement < -5 Or dMeasurement > 12 Then
                        'RDC-03-D04
                    Else
                        Echo("RDC-03-D04     Attenuator 0 dBm Diagnostic 2")
                        Echo("   The " & InstrumentDescription(RFMODANAL) & " is malfunctioning")
                        RegisterFailure(RFMODANAL, ReturnTestNumber(RFDOWN, 3, 4, "D"), dFH_Meas, "dBm", -20, 10, "Attenuator 0 dBm Diagnostic 2")
                        TestGTdownconverter = RFMODANAL
                    End If
                End If
            Else
                RecordTest("RDC-03-001  Attenuator 0 dBm", "Moduluation Meter Error", -5, 12, PowerRef, "dBm")
            End If
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "RFDOWN" And OptionStep = LoopStepNo Then
            GoTo RFDOWN_3
        End If
        LoopStepNo += 1
        frmSTest.proProgress.Value = 70

        'Check attenuator, -20 dBm
RFDOWN_4: nSystErr = WriteMsg(RFSYN, "POW 5 DBM")
        InitStatus = atxmlDF_gt55210a_setLO("RF_CNTR_1", 0, MANUAL, 20, DETOUT)

        Delay(1)
        'RDC-04-001
        dMeasurement = GetModMeterMeasurement("RDC-04-001     Attenuator -20 dBm", "RLAUTV", CStr(PowerRef# - 3), CStr(PowerRef# + 5), "dBm", 100)

        If (dMeasurement < PowerRef# - 3) Or (dMeasurement > PowerRef# + 5) Then
            Echo("RDC-04-001     Attenuator -20 dBm")
            RegisterFailure(RFDOWN, ReturnTestNumber(RFDOWN, 4, 1), Val(dMeasurement), "dBm", PowerRef# - 3, PowerRef + 5, "Checking attenuator, -20 dBm")
            TestGTdownconverter = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "RFDOWN" And OptionStep = LoopStepNo Then
            GoTo RFDOWN_4
        End If
        LoopStepNo += 1
        frmSTest.proProgress.Value = 95


RFDOWN_5:  'Check detector
        nSystErr = WriteMsg(RFSYN, "*RST")
        nSystErr = WriteMsg(RFSYN, "*CLS")
        nSystErr = WriteMsg(RFSYN, "FREQ 1950E6")
        nSystErr = WriteMsg(RFSYN, "POW -15 DBM")
        nSystErr = WriteMsg(RFSYN, ":FM:STATE OFF;:PULM:STATE OFF;:AM:STATE ON")
        nSystErr = WriteMsg(RFSYN, ":OUTP ON")

        nSystErr = WriteMsg(FGEN, "*RST;*CLS")
        nSystErr = WriteMsg(FGEN, "FUNC:SHAP SIN")
        nSystErr = WriteMsg(FGEN, "VOLT 1.0")
        nSystErr = WriteMsg(FGEN, "VOLT:OFFS 0")
        nSystErr = WriteMsg(FGEN, "FREQ 1E4")
        nSystErr = WriteMsg(FGEN, "OUTP ON")

        InitStatus = atxmlDF_gt55210a_setLO("RF_CNTR_1", 2080, MANUAL, 0, DETOUT)
        Delay(0.2)

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
        'RDC-05-001
        dMeasurement = FetchMeasurement(RFCOUNTER, "RF Detector Test", "9E3", "11E3", "Hz", RFDOWN, "RDC-05-001")
        If dMeasurement < 9000.0# Or dMeasurement > 11000.0# Then
            TestGTdownconverter = FAILED
            If (OptionFaultMode = SOFmode) And (OptionMode = LOSmode) Then
                dFH_Meas = dMeasurement
                nSystErr = WriteMsg(RFMODANAL, "CL P1 AM")
                Delay(3)
                nSystErr = WriteMsg(RFMODANAL, "TV")
                Delay(1)
                nSystErr = ReadMsg(RFMODANAL, trash)
                Delay(6)
                nSystErr = WriteMsg(RFMODANAL, "TV")
                Delay(10)
                'RDC-05-D01
                dMeasurement = FetchMeasurement(RFMODANAL, "RF Detector Test Diagnostic 1", "80", "120", "%", RFDOWN, "RDC-05-D01")
                'RDC-05-D02
                If dMeasurement < 80 Or dMeasurement > 120 Then
                    Echo("RDC-05-D02     RF Detector Test Diagnostic 1")
                    RegisterFailure(FGEN, ReturnTestNumber(RFDOWN, 5, 2, "D"), dFH_Meas, "Hz", 9000, 11000, "RF Detector Test Diagnostic 1")
                    TestGTdownconverter = FGEN
                    GoTo TestComplete
                    'RDC-05-D03
                Else
                    Echo("RDC-05-D03     RF Detector Test Diagnostic 1")
                    RegisterFailure(RFDOWN, ReturnTestNumber(RFDOWN, 5, 3, "D"), dFH_Meas, "Hz", 9000, 11000, "RF Detector Test Diagnostic 1")
                End If
            End If
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "RFDOWN" And OptionStep = LoopStepNo Then
            GoTo RFDOWN_5
        End If
        LoopStepNo += 1

        frmSTest.proProgress.Value = 100
TestComplete:
        frmSTest.cmdAbort.Text = "Abort Test"
        frmSTest.cmdPause.Text = "Pause Test"
        nSystErr = WriteMsg(RFSYN, "OUTP OFF") ' turn off synthesizer
        nSystErr = WriteMsg(RFSYN, "*RST")
        nSystErr = WriteMsg(FGEN, "OUTP OFF")
        nSystErr = WriteMsg(FGEN, "*RST")
        nSystErr = vxiClear(RFCOUNTER)
        nSystErr = WriteMsg(RFCOUNTER, "*RST; *CLS")
        nSystErr = WriteMsg(SWITCH1, "RESET") ' open all switches
        If AbortTest = True Then
            If TestGTdownconverter = FAILED Then
                ReportFailure(RFDOWN)
            Else
                ReportUnknown(RFDOWN)
                TestGTdownconverter = UNTESTED
            End If
            sMsg = vbCrLf
            sMsg &= "      **********************************************" & vbCrLf
            sMsg &= "      *     RF Down Converter tests aborted!       *" & vbCrLf
            sMsg &= "      **********************************************" & vbCrLf
            Echo(sMsg)
        ElseIf TestGTdownconverter = PASSED Then
            ReportPass(RFDOWN)
        ElseIf TestGTdownconverter = FAILED Then
            ReportFailure(RFDOWN)
        Else
            ReportUnknown(RFDOWN)
        End If
    End Function

End Module