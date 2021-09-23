'Option Strict Off
Option Explicit On
Imports System.Text

Public Module modRFSynthesizerTets

    '**************************************************************
    '* Nomenclature   : ATS-TETS SYSTEM SELF TEST                 *
    '*                  RF Synthesizer Test                       *
    '* Version        : 2.0                                       *
    '* Last Update    : Apr 1, 2017                               *
    '* Purpose        : This module contains code for the         *
    '*                  RF Synthesizer Self Test                  *
    '**************************************************************

    Public GIGA_DOWN As Boolean = True  ' GT55210A
    Public Const DETOUT = 0
    Public Const MANUAL = -1


    Public Const AUTO = 0
    Public Const IFOUT = 1
    Public Const NO_MODEL = -254
    Public Const BUSY = -255

    Dim S As String = ""

    Private Property PowerCount As Integer


    Declare Function atxmlDF_eip_selectCorrectionsTable Lib "AtXmlDriverFunc.dll" (ByVal ResourceName As String, ByVal FilePath As String, ByVal TableName As String) As Integer

    Declare Function atxmlDF_eip_setCorrectedPower Lib "AtXmlDriverFunc.dll" (ByVal ResourceName As String, ByVal instrSession As Integer, ByVal frequency As Double, ByVal power As Single) As Integer



    Function TestRFSynthesizer() As Integer

        'DESCRIPTION:
        '   This routine runs the RF Synthesizer OVP and returns PASSED or FAILED
        'RETURNS:
        '   PASSED if the RF Synthesizer test passes or FAILED if a failure occurs

        Dim SSelfTest As String = ""
        Dim StrMeasurement As String = ""
        Dim HighLimit As String
        Dim LowLimit As String
        Dim dMeasurement As Double
        Dim InitStatus As Integer
        Dim RefMeasurement As Double
        Dim ModTest1Passed As Integer
        Dim ModTest2Passed As Integer
        Dim dFH_Lowlimit As Double
        Dim dFH_HighLimit As Double
        Dim dFH_Meas1 As Double
        Dim dFH_Meas2 As Double
        Dim LoopStepNo As Integer = 1

        TestRFSynthesizer = UNTESTED

        '**Initialize GUI

        'RF Stimulus Test Title block
        EchoTitle(InstrumentDescription(RFSYN), True)
        EchoTitle("RF Stimulus Test", False)
        frmSTest.proProgress.Maximum = 100
        frmSTest.proProgress.Value = 1

        TestRFSynthesizer = PASSED
        MsgBox("Remove all cable connections from the SAIF.")


        nSystErr = vxiClear(RFSYN)
        nSystErr = vxiClear(RFPM)
        nSystErr = vxiClear(RFCOUNTER)
        nSystErr = vxiClear(RFMODANAL)
        nSystErr = vxiClear(OSCOPE)
        nSystErr = vxiClear(FGEN)

        If IDNResponse(RFSYN) = "50000" Then
            nSystErr = WriteMsg(RFSYN, "SCPI")
            Do
                nSystErr = WriteMsg(RFSYN, "SYST:ERR?")
                nSystErr = ReadMsg(RFSYN, StrMeasurement)
            Loop Until nSystErr = 0
        End If

        nSystErr = WriteMsg(RFMODANAL, "PG99RE")
        nSystErr = WriteMsg(RFCOUNTER, "INP1:COUP AC")

        ' ''Load correction table into the STIM
        ''nSystErr = atxmlDF_eip_selectCorrectionsTable(ResourceName(RFSYN), sPathLossPath, "SAIF")
        ''nSystErr = atxmlDF_eip_setCorrectedPower(ResourceName(RFSYN), 1, 8500000000.0, 10.0) '8.5GHz, +10dB

        'Run Self Test
        nSystErr = WriteMsg(RFSYN, "*TST?")
        nSystErr = WaitForResponse(RFSYN, 0.5)
        nSystErr = ReadMsg(RFSYN, SSelfTest)
        LoopStepNo = 1

RFSYN_1:  'RST-01-001
        Echo("RST-01-001     RF Stimulus Built-In Test...")
        If nSystErr <> 0 Then
            Echo(FormatResults(False, "Built-In Test Result = " & StripNullCharacters(SSelfTest)))
            RegisterFailure(RFSYN, ReturnTestNumber(RFSYN, 1, 1), , , , , "RF Stimulus Built-in Test")
            TestRFSynthesizer = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            Echo(FormatResults(True, "RF Stimulus Built-In Test"))
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "RFSYN" And OptionStep = LoopStepNo Then
            GoTo RFsyn_1
        End If
        LoopStepNo += 1
        frmSTest.proProgress.Value = 10

        'Test Power Level
          S = "Connect the 8481A power sensor to the RF STIM OUTPUT. "
          DisplaySetup(S, "TETS PSH_STO.jpg", 1)
          If AbortTest = True Then GoTo testcomplete
RFSYN_2:
        nSystErr = WriteMsg(RFSYN, "*RST")
        nSystErr = WriteMsg(RFSYN, ":FM:STATE OFF;:AM:STATE OFF;:PULM:STATE OFF")
        nSystErr = WriteMsg(RFSYN, ":OUTP ON")

        nSystErr = WriteMsg(RFPM, "*RST")
        nSystErr = WriteMsg(RFPM, "UNIT:POW DBM")
        nSystErr = WriteMsg(RFPM, "CAL:CSET ""HP8481A""")
        nSystErr = WriteMsg(RFPM, "CAL:CSET:STATE ON")

        nSystErr = WriteMsg(RFSYN, "FREQ 8.5GHz")
        nSystErr = WriteMsg(RFSYN, "POW 10 DBM")
        Delay(0.5)
        nSystErr = WriteMsg(RFPM, "MEAS:POW:AC? 10 DBM, MAX, 8.5 GHz")
        HighLimit = CStr(10 + 1.5)
        LowLimit = CStr(0)
        nSystErr = WaitForResponse(RFPM, 0)

        'RST-02-001
        dMeasurement = FetchMeasurement(RFPM, "Power Output Test, 10 DBM, 8.5 GHz", LowLimit, HighLimit, "dBm", RFSYN, "RST-02-001")

        If dMeasurement > Val(HighLimit) Or dMeasurement < Val(LowLimit) Then
            TestRFSynthesizer = FAILED
            If (OptionFaultMode = SOFmode) And (OptionMode <> LOSmode) Then
                dFH_Meas = dMeasurement
                dFH_Lowlimit = LowLimit
                dFH_HighLimit = HighLimit
                'RST-02-D01
                If InstrumentStatus(RFPM) = PASSED And RunningEndToEnd Then
                    Echo("RST-01-D01     Power Output Test")
                    RegisterFailure(RFSYN, ReturnTestNumber(RFSYN, 2, 1, "D"), dFH_Meas, "dBm", dFH_Lowlimit, dFH_HighLimit, _
                    "Power Output Test")
                    If OptionFaultMode = SOFmode Then GoTo TestComplete
                End If
                S = "Connect the 8481A power sensor to the Reference Output jack. " & vbCrLf & vbCrLf
                DisplaySetup(S, "TETS PSH_PRO.jpg", 1)
                If AbortTest = True Then GoTo testcomplete
                nSystErr = WriteMsg(RFPM, "*RST")
                nSystErr = WriteMsg(RFPM, "UNIT:POW DBM")
                nSystErr = WriteMsg(RFPM, "OUTP:ROSC:STAT ON")
                nSystErr = WriteMsg(RFPM, "MEAS:POW:AC? 0DBM, MIN, 50E6")
                HighLimit = CStr(10 + 1)
                LowLimit = CStr(0)
                nSystErr = WaitForResponse(RFPM, 0)
                'RST-02-D02
                dMeasurement = FetchMeasurement(RFPM, "RF Power Meter Diagnostic", "-.1", ".1", "dBm", RFPM, "RST-02-D02")
                'RST-02-D03
                If dMeasurement > 0.1 Or dMeasurement < -0.1 Then
                    Echo("RST-02-D03     RF Power Meter Diagnostic")
                    RegisterFailure(RFPM, ReturnTestNumber(RFSYN, 2, 3, "D"), dFH_Meas, "dBm", dFH_Lowlimit, dFH_HighLimit, "RF Power Meter Diagnostic")
                    TestRFSynthesizer = RFPM
                    If OptionFaultMode = SOFmode Then GoTo TestComplete
                End If
                'RST-02-D04
                Echo("RST-02-D04     RF Power Meter Diagnostic")
                RegisterFailure(RFSYN, ReturnTestNumber(RFSYN, 2, 4, "D"), dFH_Meas, "dBm", dFH_Lowlimit, dFH_HighLimit, "RF Power Meter Diagnostic")
            End If
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "RFSYN" And OptionStep = LoopStepNo Then
            GoTo RFsyn_2
        End If
        LoopStepNo += 1
        frmSTest.proProgress.Value = 20


        '******************************
        '*** Test Modulation inputs ***
        '******************************

RFSYN_3: If GIGA_DOWN = True Then  ' GT55210A
            InitStatus = atxmlDF_gt55210a_setLO("RF_CNTR_1", 2000, MANUAL, 0, DETOUT)
        Else
            InitStatus = atxmlDF_eip1315a_setLO("RF_CNTR_1", 1890, MANUAL, 0, DETOUT)
        End If

        '** Test Pulse Modulation Input **
        nSystErr = WriteMsg(FGEN, "*RST;*CLS")
        nSystErr = WriteMsg(FGEN, "FUNC:SHAP SQU")
        nSystErr = WriteMsg(FGEN, "VOLT 2.4")
        nSystErr = WriteMsg(FGEN, "VOLT:OFFS 1.24")
        nSystErr = WriteMsg(FGEN, "FREQ 1E5")



        If GIGA_DOWN = True Then  ' GT55210A
            nSystErr = WriteMsg(RFSYN, "FREQ 1930E6")
        Else                      ' EIP1315A
            nSystErr = WriteMsg(RFSYN, "FREQ 1950E6")
        End If

        nSystErr = WriteMsg(RFSYN, "POW 10 DBM")
        nSystErr = WriteMsg(RFSYN, ":OUTP ON")
        nSystErr = WriteMsg(RFSYN, ":FM:STATE OFF;:AM:STATE OFF;:PULM:STATE OFF")

        nSystErr = WriteMsg(RFPM, "MEAS:POW:AC? 10DBM, MIN, 1950E6")
        nSystErr = WaitForResponse(RFPM, 0)

        'RST-03-001
        RefMeasurement = FetchMeasurement(RFPM, "Modulation Reference", "", "1.0E38", "dBm", RFPM, "RST-03-001")
        If RefMeasurement > 1.0E+38 Then ' measured 8.3
            TestRFSynthesizer = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "RFSYN" And OptionStep = LoopStepNo Then
            GoTo RFSYN_3
        End If
        LoopStepNo += 1
        frmSTest.proProgress.Value = 30

        S = "Use cable W15 to connect" & vbCrLf & vbCrLf
        S &= "   FG OUTPUT   to   " & vbCrLf
        S &= "   RF STIM  PULSE  MODULATION INPUT."
        DisplaySetup(S, "TETS FGO_SPM.jpg", 1)
        If AbortTest = True Then GoTo testcomplete

RFSYN_4:
        nSystErr = WriteMsg(RFSYN, ":FM:STATE OFF;:AM:STATE OFF;:PULM:STATE ON")
        nSystErr = WriteMsg(FGEN, "OUTP ON")
        Delay(1)
        nSystErr = WriteMsg(RFPM, "MEAS:POW:AC? 7 DBM, MIN, 1950E6")
        nSystErr = WaitForResponse(RFPM, 0)
        'RST-04-001
        dMeasurement = FetchMeasurement(RFPM, "Pulse Modulation Power", "", CStr(RefMeasurement# - 2.4), "dBm", RFPM, "RST-04-001")

        If dMeasurement > (RefMeasurement# - 2.4) Then
            TestRFSynthesizer = FAILED
            If (OptionFaultMode = SOFmode) And (OptionMode <> LOSmode) Then
                dFH_Meas = dMeasurement
                dFH_HighLimit = RefMeasurement# - 2.4
                'RST-04-D01
                If InstrumentStatus(FGEN) = PASSED And RunningEndToEnd Then
                    Echo("RST-04-D01     Pulse Modulation Power")
                    RegisterFailure(RFSYN, ReturnTestNumber(RFSYN, 4, 1, "D"), dFH_Meas, "dBm", 0, dFH_HighLimit, "Pulse Modulation Power")
                    If OptionFaultMode = SOFmode Then GoTo TestComplete
                End If
                S = "Move cable W15 connection from" & vbCrLf & vbCrLf
                S &= "    RF STIM  PULSE  MODULATION   to" & vbCrLf
                S &= "    SCOPE INPUT 1."
                DisplaySetup(S, "TETS FGO_SI1.jpg", 1)
                If AbortTest = True Then GoTo testcomplete
                nSystErr = WriteMsg(OSCOPE, "*RST; *CLS")
                nSystErr = WriteMsg(OSCOPE, "AUT")
                Delay(3)
                nSystErr = WriteMsg(OSCOPE, "DIG CHAN1")
                nSystErr = WriteMsg(OSCOPE, "MEAS:SOUR CHAN1")
                nSystErr = WriteMsg(OSCOPE, "MEAS:VAMP?")
                'RST-04-D02
                dMeasurement = FetchMeasurement(OSCOPE, "RF Pulse Modulation Diagnostic Test", "3", "", "V", OSCOPE, "RST-04-D02")
                'RST-04-D03
                If dMeasurement < 3 Then
                    Echo("RST-04-D03     RF Pulse Modulation Diagnostic Test")
                    RegisterFailure(FGEN, ReturnTestNumber(RFSYN, 4, 3, "D"), dFH_Meas, "dBm", 0, dFH_HighLimit, "RF Pulse Modulation Diagnostic Test")
                    TestRFSynthesizer = FGEN
                    'RST-04-D04
                Else
                    Echo("RST-04-D04     RF Pulse Modulation Diagnostic Test")
                    RegisterFailure(RFSYN, ReturnTestNumber(RFSYN, 4, 4, "D"), dFH_Meas, "dBm", 0, dFH_HighLimit, "RF Pulse Modulation Diagnostic Test")
                End If
            End If
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "RFSYN" And OptionStep = LoopStepNo Then
            GoTo RFSYN_4
        End If
        LoopStepNo += 1
        frmSTest.proProgress.Value = 40

        S = "Remove 8481A power sensor from RF STIM OUTPUT." & vbCrLf
        S &= "Use cable W22 to connect " & vbCrLf & vbCrLf
        S &= "    RF STIM OUTPUT   to " & vbCrLf
        S &= "    RF MEAS INPUT."
        DisplaySetup(S, "TETS RFM_STO.jpg", 1)
        If AbortTest = True Then GoTo testcomplete

RFSYN_5:
        nSystErr = vxiClear(RFCOUNTER)
        nSystErr = WriteMsg(RFCOUNTER, "*RST; *CLS")
        nSystErr = WriteMsg(RFCOUNTER, "INP1:IMP 50")
        nSystErr = WriteMsg(RFCOUNTER, "INP2:IMP 50")
        nSystErr = WriteMsg(RFCOUNTER, "INP1:COUP AC")

        nSystErr = WriteMsg(RFSYN, "POW -10 DBM")  'Added per DR# 183
        nSystErr = WriteMsg(RFCOUNTER, "SENS1:EVEN:HYST MAX")
        nSystErr = WriteMsg(RFCOUNTER, "MEAS:FREQ? 1E5")
        'RST-05-001
        dMeasurement = FetchMeasurement(RFCOUNTER, "Pulse Modulation Test", "90E3", "110E3", "Hz", RFCOUNTER, "RST-05-001")
        dFH_Meas1 = dMeasurement
        ' ModTest1Passed = (dMeasurement > 90000.0# And dMeasurement < 110000.0#)
        If (dMeasurement < 90000.0# Or dMeasurement > 110000.0#) Then
            ModTest1Passed = False
            TestRFSynthesizer = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            ModTest1Passed = True
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "RFSYN" And OptionStep = LoopStepNo Then
            GoTo RFSYN_5
        End If
        LoopStepNo += 1
        frmSTest.proProgress.Value = 50

        S = "Move cable W15 connection from" & vbCrLf & vbCrLf
        S &= "   RF STIM  PULSE  MODULATION INPUT  to" & vbCrLf
        S &= "   RF STIM  AM  MODULATION INPUT."
        DisplaySetup(S, "TETS FGO_SAM.jpg", 1)
        If AbortTest = True Then GoTo testcomplete
RFSYN_6:
        nSystErr = vxiClear(RFCOUNTER)
        nSystErr = WriteMsg(RFCOUNTER, "*RST; *CLS")
        nSystErr = WriteMsg(RFCOUNTER, "INP1:IMP 50")
        nSystErr = WriteMsg(RFCOUNTER, "INP2:IMP 50")
        nSystErr = WriteMsg(RFCOUNTER, "INP1:COUP AC")
        frmSTest.proProgress.Value = 60

        '** Test Amplitude Modulation Input **
        nSystErr = WriteMsg(RFSYN, "POW -25 DBM")
        nSystErr = WriteMsg(FGEN, "OUTP OFF")
        nSystErr = WriteMsg(FGEN, "FUNC:SHAP SIN")
        nSystErr = WriteMsg(FGEN, "FREQ 1E4")
        nSystErr = WriteMsg(FGEN, "VOLT 1")
        nSystErr = WriteMsg(FGEN, "VOLT:OFFS 0")


        nSystErr = WriteMsg(FGEN, "OUTP ON")
        nSystErr = WriteMsg(RFSYN, ":FM:STATE OFF;:PULM:STATE OFF;:AM:STATE ON")

        If GIGA_DOWN = True Then  ' GT55210A
            InitStatus = atxmlDF_gt55210a_setLO("RF_CNTR_1", 2000, MANUAL, 0, DETOUT)
        Else                      ' EIP1315A
            InitStatus = atxmlDF_eip1315a_setLO("RF_CNTR_1", 1890, MANUAL, 0, DETOUT)
        End If

        nSystErr = vxiClear(RFCOUNTER)
        nSystErr = WriteMsg(RFCOUNTER, "*RST; *CLS")
        nSystErr = WriteMsg(RFCOUNTER, "INP1:IMP 50")
        nSystErr = WriteMsg(RFCOUNTER, "INP2:IMP 50")
        nSystErr = WriteMsg(RFCOUNTER, "INP1:COUP AC")
        nSystErr = WriteMsg(RFCOUNTER, "INP1:IMP 50")
        nSystErr = WriteMsg(RFCOUNTER, "SENS1:EVEN:HYST MAX")
        nSystErr = WriteMsg(RFCOUNTER, "MEAS:FREQ? 1E4")
        'RST-06-001
        dMeasurement = FetchMeasurement(RFCOUNTER, "Amplitude Modulation Test", "9E3", "11E3", "Hz", RFCOUNTER, "RST-06-001")
        dFH_Meas2 = dMeasurement
        ModTest2Passed = (dMeasurement > 9000.0# And dMeasurement < 11000.0#)
        nSystErr = vxiClear(RFCOUNTER)
        nSystErr = WriteMsg(RFCOUNTER, "*RST; *CLS")
        nSystErr = WriteMsg(RFCOUNTER, "INP1:IMP 50")
        nSystErr = WriteMsg(RFCOUNTER, "INP2:IMP 50")
        nSystErr = WriteMsg(RFCOUNTER, "INP1:COUP AC")
        frmSTest.proProgress.Value = 80

        '*** Failure Diagnostic ***
        If ModTest1Passed And ModTest2Passed Then
            IncStepPassed()
        ElseIf ModTest1Passed Xor ModTest2Passed Then
            TestRFSynthesizer = FAILED
            IncStepFailed()

            'RST-06-D01
            If (OptionFaultMode = SOFmode) And (OptionMode <> LOSmode) Then
                If Not ModTest1Passed Then
                    Echo("RST-06-D01     Amplitude Modulation Test")
                    RegisterFailure(RFSYN, ReturnTestNumber(RFSYN, 6, 1, "D"), dFH_Meas1, "Hz", 90000, 110000, "Amplitude Modulation Test")
                    'RST-06-D02
                Else
                    Echo("RST-06-D02     Amplitude Modulation Test")
                    RegisterFailure(RFSYN, ReturnTestNumber(RFSYN, 6, 2, "D"), dFH_Meas2, "Hz", 9000, 11000, "Amplitude Modulation Test")
                End If
                'RST-06-D03
                Echo("RST-06-D03     Pulse Modulation Test")
                RegisterFailure(RFSYN, ReturnTestNumber(RFSYN, 6, 3, "D"), , , , , "Pulse Modulation Test")
            End If
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
            'RST-06-D04
        ElseIf (Not ModTest1Passed) And (Not ModTest2Passed) Then
            If (OptionFaultMode = SOFmode) And (OptionMode <> LOSmode) Then
                Echo("RST-06-D04")
                Echo("    Modulation failure due to an RF Counter, or RF Downconverter.  Examine test results of these components to determin failure.")
                RegisterFailure(RFSYN, ReturnTestNumber(RFSYN, 6, 4, "D"), , , , , "Modulation failure due to an RF Counter, or RF Downconverter.")
                TestRFSynthesizer = RFCOUNTER
            End If
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "RFSYN" And OptionStep = LoopStepNo Then
            GoTo RFSYN_6
        End If
        LoopStepNo += 1

        frmSTest.proProgress.Value = 80
        S = "Move cable W15 connection from" & vbCrLf & vbCrLf
        S &= "   RF STIM  AM  MODULATION INPUT  to" & vbCrLf
        S &= "   RF STIM  FM  MODULATION INPUT."
        DisplaySetup(S, "TETS FGO_SFM.jpg", 1)
        If AbortTest = True Then GoTo testcomplete

RFSYN_7:

        '** Test Frequency Modulation Input **
        nSystErr = WriteMsg(FGEN, "OUTP OFF")
        nSystErr = WriteMsg(RFSYN, ":AM:STATE OFF;:PULM:STATE OFF;:FM:STATE ON")
        nSystErr = WriteMsg(FGEN, "VOLT 94E-3")
        nSystErr = WriteMsg(FGEN, "FREQ 10000")

        nSystErr = WriteMsg(FGEN, "OUTP ON")

        If GIGA_DOWN = True Then   ' GT55210A
            InitStatus = atxmlDF_gt55210a_setLO("RF_CNTR_1", 2000, MANUAL, 0, DETOUT)
        Else                       ' EIP1315A
            InitStatus = atxmlDF_eip1315a_setLO("RF_CNTR_1", 1890, MANUAL, 0, DETOUT)
        End If

        nSystErr = WriteMsg(RFMODANAL, "FM")
        Delay(3)
        nSystErr = WriteMsg(RFMODANAL, "TV")

        'RST-07-001
        dMeasurement = FetchMeasurement(RFMODANAL, "Frequency Modulation Test", _
          "100", "400", "kHz", RFSYN, "RST-07-001")

        'RST-07-D01
        If dMeasurement < 100000 Or dMeasurement > 400000 Then
            Echo("RST-07-001     Frequency Modulation Test")
            RegisterFailure(RFSYN, ReturnTestNumber(RFSYN, 7, 1, "D"), dMeasurement, "Hz", 100000, 400000, "Frequency Modulation Test")
            Echo("    Possibility of RF Measurement Analyzer or RF Downconverter failure.")
            TestRFSynthesizer = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            ModTest1Passed = True
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "RFSYN" And OptionStep = LoopStepNo Then
            GoTo RFSYN_7
        End If
        frmSTest.proProgress.Value = 100

TestComplete:
        frmSTest.cmdAbort.Text = "Abort Test"
        frmSTest.cmdPause.Text = "Pause Test"
        nSystErr = WriteMsg(RFSYN, "OUTP OFF")
        nSystErr = WriteMsg(RFSYN, "*RST")
        nSystErr = WriteMsg(RFSYN, ":SOUR:ROSC:SOUR INT") ' 10MHZ REFERENCE OUTPUT
        nSystErr = WriteMsg(RFSYN, ":OUTP:ROSC:STAT OFF")
        nSystErr = WriteMsg(SWITCH1, "RESET")
        nSystErr = WriteMsg(RFCOUNTER, "*RST")
        nSystErr = WriteMsg(RFCOUNTER, "*CLS")
        nSystErr = WriteMsg(RFMODANAL, "*RST")
        nSystErr = WriteMsg(RFMODANAL, "*CLS")
        nSystErr = WriteMsg(RFPM, "*RST")
        nSystErr = WriteMsg(RFPM, "*CLS")
        Echo("")
        If AbortTest = True Then
            If TestRFSynthesizer = FAILED Then
                ReportFailure(RFSYN)
            Else
                ReportUnknown(RFSYN)
                TestRFSynthesizer = UNTESTED
            End If
            sMsg = vbCrLf
            sMsg &= "      **********************************************" & vbCrLf
            sMsg &= "      *       RF Synthesizer tests aborted!        *" & vbCrLf
            sMsg &= "      **********************************************" & vbCrLf
            Echo(sMsg)
        ElseIf TestRFSynthesizer = FAILED Then
            ReportFailure(RFSYN)
        ElseIf TestRFSynthesizer = PASSED Then
            ReportPass(RFSYN)
        Else
            ReportUnknown(RFSYN)
        End If

    End Function


End Module