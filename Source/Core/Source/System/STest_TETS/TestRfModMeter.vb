'Option Strict Off
Option Explicit On

Imports System.Windows.Forms

Public Module modRFmodulationMeter

    '**************************************************************
    '* Nomenclature   : ATS-TETS SYSTEM SELF TEST                 *
    '*                  RF Measurement Analizer Test              *
    '* Written By     : Michael McCabe                            *
    '* Version        : 2.0                                       *
    '* Last Update    : Apr 1, 2017                               *
    '* Purpose        : This module contains code for the         *
    '*                  RF Measurement Analizer Self Test         *
    '**************************************************************

    Dim S As String = ""

    Function GetModMeterMeasurement(TestName As String, MeasCode As String, LowLimit As String, HighLimit As String, Units As String, MaxExceptVal As Single) As Double   'DESCRIPTION:
        '    This routine obtains a measurement from the Modulation Analyzer.  It is necessary
        '    because the Modulation analyzer measurement should be qualified by error, locked
        '    and optionally leveled status.
        'PARAMTERS:
        '    hndlModAn&      =   Handle of modulation meter
        '    TestName       =   String discription of the test being run
        '    MeasCode       =   The code required to make the desired measurement
        '    LowLimit       =   The lower tolorance of the test
        '    HightLimit     =   The upper tolorance of the test
        '    Units          =   The units of the desired measurement
        '    MaxExceptVal!   =   (no longer used at V2.0, but remains as an argument for backward
        '                         compatibility)
        'RETURNS:
        '    A double precision floating point value equal to the measurement

        Dim LoopCount As Integer
        Dim strMeasurement As String = ""
        Dim systErr As Integer
        Dim lStatus As Integer
        Dim iRetry As Integer
        Dim lFunc As Long
        Dim iQualified As Integer
        Dim sReturns(0 To 3) As String
        Dim kHzFlg As Boolean
        Dim dMeasurement As Double

        If Units = "KHz" Then
            kHzFlg = True
            Units = "Hz"
            LowLimit = CStr(1000 * CDbl(LowLimit))
            HighLimit = CStr(1000 * CDbl(HighLimit))
        Else
            kHzFlg = False
        End If

        Delay(0.3)
        nSystErr = WriteMsg(RFMODANAL, MeasCode)          'Send initial command to set mode
        Delay(0.3)
        If InStr(UCase(MeasCode), "TV") Then              'Check for a query command
            systErr = ReadMsg(RFMODANAL, strMeasurement)  'Read and ignore the first measurement
        End If
        Delay(0.1)

        iRetry = 0
        iQualified = 0
        Do
            For LoopCount = 1 To 6
                nSystErr = WriteMsg(RFMODANAL, "TS TF TV") 'Talk Status, Talk Function, Talk Value
                Delay(1)
                systErr = ReadMsg(RFMODANAL, strMeasurement)

                If (systErr < VI_SUCCESS) Or (LoopCount = 5) Then
                    GetModMeterMeasurement# = 9.99E+37
                    '5/24/02 Nick Sazonov
                    Echo(TestName & vbCrLf)
                    Echo("Status, Function, and Value could not be established")
                    Exit Function
                End If

                If StringToList(strMeasurement, sReturns, ",") = 3 Then
                    lStatus = CLng(sReturns(1))
                    If lStatus = 0 Then
                        lFunc = CLng("&H" & sReturns(2))
                        'Sometimes a huge number comes out that causes overflow
                        If lFunc > 0 And lFunc < 65536 Then Exit For
                    Else
                        '05/24/02 Nick Sazonov
                        Echo(TestName & vbCrLf)
                        Echo("    RF Measurement Analyzer Error Code: " & Format(lStatus))
                        GetModMeterMeasurement# = 9.99E+37
                        Exit Function
                    End If
                End If
            Next

            iRetry = iRetry + 1

            'Echo sReturns(2) & "got there"

            Select Case True
                Case (lFunc And &H800) <> 0 'If UnLocked
                    iQualified = 0
                    '   Echo sReturns(2) & "UnLocked"
                    GoTo EndLoop
                Case (lFunc And &H400) <> 0 'If UnLeveled
                    If InStr(UCase(MeasCode), "AM") Then ' what is this for?
                        iQualified = 0
                        '     Echo sReturns(2) & "UnLeveled"
                        GoTo EndLoop
                    End If
            End Select

            'If here, then it's Locked, and Leveled (for AM)
            If (lFunc And &H100) <> 0 Then   'If Audio Display Overrange
                GetModMeterMeasurement# = 9.99E+37
                '5/24/02 Nick Sazonov
                Echo(TestName & vbCrLf)
                Echo("Audio display overrange")
                Exit Function
            End If

            'If here, then measurement has been qualified
            iQualified = iQualified + 1

            'Throw away the first qualified measurement, to allow settling
            If iQualified = 1 Then GoTo EndLoop

            'If here, then two consecutive measurements have been qualified
            dMeasurement = CDbl(sReturns(3))

            If kHzFlg Then dMeasurement = 1000 * dMeasurement

            RecordTest(TestName, "", Val(LowLimit), Val(HighLimit), dMeasurement, Units)

            GetModMeterMeasurement# = dMeasurement
            Exit Function

EndLoop:
            Application.DoEvents()
        Loop While iRetry < 6

        'If here, could not get locked (AND leveled for AM)

        '5/23/2002 TestName variable added to the unLocked/unLeveled message

        Echo(TestName)

        If (lFunc And &H800) <> 0 Then
            Echo("    RF Measurement Analyzer unLeveled")
        End If

        If (lFunc And &H400) <> 0 Then
            Echo("    RF Measurement Analyzer unLocked")
        End If

        GetModMeterMeasurement# = 9.99E+37

    End Function

    Function TestModAnalyzer() As Integer
        'DESCRIPTION:
        '   This routine tests the Modulation Analyzer and returns PASSED or FAILED
        'RETURNS:
        '   PASSED if the Modulation Analyzer SCOPE test passes or FAILED if a failure occurs

        Dim SSelfTest As String = ""
        Dim SelfTest As Integer
        Dim InitStatus As Integer
        Dim Dummmy As String = ""
        Dim DCLevel As String = ""
        Dim dMeasurement As Double
        Dim dMeas As Double
        Dim dFH_MeasModT1 As Double
        Dim dFH_MeasModT2 As Double
        Dim iModTest1Passed As Integer
        Dim iModTest2Passed As Integer
        Dim LoopStepNo As Integer = 1

        TestModAnalyzer = UNTESTED
        If Not RunningEndToEnd Then
            MsgBox("Remove all cable connections from the SAIF.")
        End If

        'RF Measurement Analyzer Test Title block
        EchoTitle(InstrumentDescription(RFMODANAL), True)
        EchoTitle("RF Measurement Analyzer Test", False)
        frmSTest.proProgress.Maximum = 100
        frmSTest.proProgress.Value = 1
        TestModAnalyzer = PASSED

        '*** Reset Instruments ****
        nSystErr = vxiClear(RFSYN)
        nSystErr = vxiClear(RFPM)
        nSystErr = vxiClear(RFCOUNTER)
        nSystErr = vxiClear(COUNTER)
        nSystErr = vxiClear(FGEN)
        nSystErr = vxiClear(OSCOPE)
        nSystErr = WriteMsg(FGEN, "*RST")
        nSystErr = WriteMsg(RFPM, "*RST")
        nSystErr = WriteMsg(RFCOUNTER, "*RST; *CLS")
        nSystErr = WriteMsg(RFCOUNTER, "INP1:IMP 50")
        nSystErr = WriteMsg(RFCOUNTER, "INP2:IMP 50")
        nSystErr = WriteMsg(COUNTER, "*RST; *CLS")
        nSystErr = WriteMsg(COUNTER, "INP1:IMP 50")
        nSystErr = WriteMsg(COUNTER, "INP2:IMP 50")
        nSystErr = WriteMsg(RFSYN, "*RST")

        '** Perform a Modulation Analyzer Power-up Self Test
        Echo("RMA-01-001     RF Measurement Analyzer Built-In Test. . .")
        nSystErr = vxiClear(RFMODANAL)
        nSystErr = WriteMsg(RFMODANAL, "PG99RE")
        Delay(1)
        nSystErr = WriteMsg(RFMODANAL, "CL CH TS")
        WaitForResponse(RFMODANAL, 0)
        SelfTest = ReadMsg(RFMODANAL, SSelfTest)

RMA_1:  LoopStepNo = 1
        'RMA-01-001
        If SelfTest <> 0 Or Val(SSelfTest) <> 0 Then
            TestModAnalyzer = FAILED
            IncStepFailed()
            Echo(FormatResults(False, "Built-In Test Result = " & StripNullCharacters(SSelfTest)))
            RegisterFailure(RFMODANAL, ReturnTestNumber(RFMODANAL, 1, 1), , , , , "RF Measurement Analyzer Built-In Test")
            If SOFmode = SOFmode Then GoTo TestComplete
        Else
            Echo(FormatResults(True, "RF Measurement Analyzer Built-In Test"))
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "RFMA" And OptionStep = LoopStepNo Then
            GoTo RMA_1
        End If
        LoopStepNo += 1
        frmSTest.proProgress.Value = 10


        '**********************************
        '** Perform Frequency Measurement
        nSystErr = WriteMsg(FGEN, "*RST; *CLS")
        If RunningEndToEnd = True Then
          S = "Move one end of cable W15 from " & vbCrLf & vbCrLf
            S &= "  RF STIM  AM  MODULATION INPUT to  " & vbCrLf
            S &= "  RF STIM  FM  MODULATION INPUT. "
            DisplaySetup(S, "TETS FGO_SFM.jpg", 1)
          If AbortTest = True Then GoTo testcomplete
          If GoBack = True Then GoTo Step1d
        Else

Step1d:
          S = "Use cable W22 to connect " & vbCrLf & vbCrLf
            S &= "    RF STIM OUTPUT  to   " & vbCrLf
            S &= "    RF MEAS INPUT. "
            DisplaySetup(S, "TETS RFM_STO.jpg", 1, True, 1, 2)
          If AbortTest = True Then GoTo testcomplete
Step2d:
          S = "Use cable W15 to connect " & vbCrLf & vbCrLf
            S &= "   FG OUTPUT   to    " & vbCrLf
            S &= "   RF STIM  FM  MODULATION INPUT. "
            DisplaySetup(S, "TETS FGO_SFM.jpg", 1, True, 2, 2)
          If AbortTest = True Then GoTo testcomplete
          If GoBack = True Then GoTo Step1d

        End If

        nSystErr = WriteMsg(RFSYN, "*RST")
        nSystErr = WriteMsg(RFSYN, "*CLS")
        nSystErr = WriteMsg(RFSYN, "FREQ 1850E6")
        nSystErr = WriteMsg(RFSYN, "POW -15 DBM")
        nSystErr = WriteMsg(RFSYN, ":OUTP ON")

        If GIGA_DOWN = True Then  'GT55210A
            InitStatus = atxmlDF_gt55210a_setLO("RF_CNTR_1", 1680, MANUAL, 0, DETOUT)
        Else                      'EIP1315A
            InitStatus = atxmlDF_eip1315a_setLO("RF_CNTR_1", 1680, MANUAL, 0, DETOUT)
        End If

        Delay(2)
        nSystErr = WriteMsg(RFMODANAL, "CL FR HZ")
        Delay(3)
        nSystErr = WriteMsg(RFMODANAL, "TV")
        Delay(1)
        nSystErr = ReadMsg(RFMODANAL, Dummmy)
        nSystErr = WriteMsg(RFMODANAL, "TV")
        Delay(6)
        'RMA-02-001
RMA_2:
        dMeasurement = FetchMeasurement(RFMODANAL, "Frequency Test 170 MHz", "160E6", "180E6", "Hz", RFMODANAL, "RMA-02-001")
        frmSTest.proProgress.Value = 20
        If dMeasurement < 160000000.0# Or dMeasurement > 180000000.0# Then
            IncStepFailed()
            TestModAnalyzer = FAILED
            If (OptionFaultMode = SOEmode) And (OptionMode <> LOSmode) Then
                dFH_Meas = dMeasurement
                If GIGA_DOWN = True Then 'GT55210A()
                    InitStatus = atxmlDF_gt55210a_setLO("RF_CNTR_1", 1680, MANUAL, 0, IFOUT)
                Else
                    InitStatus = atxmlDF_eip1315a_setLO("RF_CNTR_1", 1680, MANUAL, 0, IFOUT)
                End If

                nSystErr = WriteMsg(RFCOUNTER, "ATIM:TIME 60")
                nSystErr = WriteMsg(RFCOUNTER, "ATIM OFF")
                nSystErr = WriteMsg(RFCOUNTER, "*RST; *CLS")
                nSystErr = WriteMsg(RFCOUNTER, "INP1:IMP 50")
                nSystErr = WriteMsg(RFCOUNTER, "INP2:IMP 50")
                nSystErr = WriteMsg(RFCOUNTER, "MEAS:FREQ?")
                'RMA-02-D01
                dMeasurement = FetchMeasurement(RFMODANAL, "Frequency Test Diagnostic 1", "160E6", "190E6", "Hz", RFCOUNTER, "RMA-02-D01")
                If dMeasurement < 160000000.0# Or dMeasurement > 190000000.0# Then
                    If (OptionFaultMode = SOEmode) And (OptionMode <> LOSmode) Then
                        S = "Disconnect the W22 cable from the RF STIM OUTPUT." & vbCrLf
                        S &= "Connect the 8481A Power Sensor to the RF STIM OUTPUT."
                        DisplaySetup(S, "TETS PSH_STO.jpg", 1)
                        If AbortTest = True Then GoTo testcomplete
                        nSystErr = WriteMsg(RFPM, "*RST")
                        nSystErr = WriteMsg(RFPM, "UNIT:POW DBM")
                        nSystErr = WriteMsg(RFPM, "CAL:CSET ""HP8481A""")
                        nSystErr = WriteMsg(RFPM, "CAL:CSET:STAT ON")
                        nSystErr = WriteMsg(RFPM, "MEAS:POW:AC? -18DBM, MIN, 1850E6")
                        nSystErr = WaitForResponse(RFPM, 0)
                        'RMA-02-D02
                        dMeasurement = FetchMeasurement(RFPM, "Frequency Test Diagnostic 2A", "-19", "", "dBm", RFPM, "RMA-02-D02")
                        'RMA-02-D03
                        If dMeasurement < -19 Then
                            Echo("RMA-02-D03")
                            RegisterFailure(RFSYN, ReturnTestNumber(RFMODANAL, 2, 3, "D"), dFH_Meas, "Hz", 160000000, 180000000, "Frequency Test Diagnostic 2A")
                            TestModAnalyzer = RFSYN
                            Echo("    Could not complete RF Measurement test due to an RF Synthesizer failure.")
                        Else
                            'RMA-02-D04
                            Echo("RMA-02-D04")
                            RegisterFailure(RFDOWN, ReturnTestNumber(RFMODANAL, 2, 4, "D"), dFH_Meas, "Hz", 160000000, 180000000, "Frequency Test Diagnostic 2A")
                            TestModAnalyzer = RFDOWN
                            Echo("    Could not complete RF Measurement test due to an RF Downconverter failure.")
                        End If
                        GoTo TestComplete
                    End If
                Else

                    OpenEmiDoor()
                    If AbortTest = True Then GoTo testcomplete


                    If GIGA_DOWN = True Then  ' GT55210A
Step1:
                        S = "Disconnect the semi-rigid cable from the IF output "
                        S &= "on the front of Downconverter instrument."
                        DisplaySetup(S, "TETS DISC_IFO2.jpg", 1, True, 1, 2)
                        If AbortTest = True Then GoTo testcomplete
Step2:
                        S = "Use cable W16 to connect " & vbCrLf & vbCrLf
                        S &= "   C/T INPUT 1   to" & vbCrLf
                        S &= "   DOWNCONVERTER IF OUTPUT."
                        DisplaySetup(S, "TETS CI1_IFO2.jpg", 1, True, 2, 2)
                        If GoBack = True Then GoTo Step1
                    Else                     ' EIP1315A
Step1b:
                        S = "Disconnect the semi-rigid cable from the IF output " & vbCrLf
                        S &= "on the front of Downconverter instrument."
                        DisplaySetup(S, "TETS DISC_IFO.jpg", 1, True, 1, 2)
                        If AbortTest = True Then GoTo testcomplete
Step2b:
                        S = "Use cable W16 to connect " & vbCrLf & vbCrLf
                        S &= "   C/T INPUT 1   to " & vbCrLf
                        S &= "   DOWNCONVERTER IF OUTPUT."
                        DisplaySetup(S, "TETS CI1_IFO.jpg", 1, True, 2, 2)
                        If GoBack = True Then GoTo Step1b
                    End If
                    If AbortTest = True Then GoTo testcomplete

                    nSystErr = vxiClear(COUNTER)
                    nSystErr = WriteMsg(COUNTER, "*RST; *CLS")
                    nSystErr = WriteMsg(COUNTER, "INP1:IMP 50")
                    nSystErr = WriteMsg(COUNTER, "INP2:IMP 50")
                    nSystErr = WriteMsg(COUNTER, "ATIM:TIME 60")
                    nSystErr = WriteMsg(COUNTER, "ATIM OFF")
                    nSystErr = WriteMsg(COUNTER, "MEAS:FREQ?")
                    'RMA-02-D05
                    dMeasurement = FetchMeasurement(COUNTER, "Frequency Test Diagnostic 2B", "160E6", "180E6", "Hz", COUNTER, "RMA-02-D05")
                    'RMA-02-D06
                    If dMeasurement < 160000000.0# Or dMeasurement > 180000000.0# Then
                        Echo("RMA-02-D06")
                        RegisterFailure(RFDOWN, ReturnTestNumber(RFMODANAL, 2, 6, "D"), dFH_Meas, "Hz", 160000000, 180000000, "Frequency Test Diagnostic 2B")
                        TestModAnalyzer = RFDOWN
                        Echo("    Could not complete RF Measurement test due to an RF Downconverter failure.")
                    Else
                        'RMA-02-D07
                        Echo("RMA-02-D07")
                        RegisterFailure(RFMODANAL, ReturnTestNumber(RFMODANAL, 2, 7, "D"), dFH_Meas, "Hz", 160000000, 180000000, "Frequency Test Diagnostic 2B")
                    End If
                End If
            End If
            GoTo TestComplete
        Else
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "RFMA" And OptionStep = LoopStepNo Then
            GoTo RMA_2
        End If
        LoopStepNo += 1


        frmSTest.proProgress.Value = 30


        '*****************************************
        '** Perform Phase Modulation Measurement
        nSystErr = WriteMsg(FGEN, "*RST;*CLS")
        nSystErr = WriteMsg(FGEN, "VOLT 120E-3")
        nSystErr = WriteMsg(FGEN, "FREQ 1E4")
        nSystErr = WriteMsg(FGEN, "FUNC:SHAP SIN")
        nSystErr = WriteMsg(FGEN, "OUTP ON")

        nSystErr = WriteMsg(RFSYN, "POW -15 DBM")
        nSystErr = WriteMsg(RFSYN, ":AM:STATE OFF;:PULM:STATE OFF;:FM:STATE ON")

        nSystErr = WriteMsg(RFSYN, "FREQ 2000E6")


        If GIGA_DOWN = True Then  'GT55210A
            InitStatus = atxmlDF_gt55210a_setLO("RF_CNTR_1", 2080, MANUAL, 0, DETOUT)
        Else
            InitStatus = atxmlDF_eip1315a_setLO("RF_CNTR_1", 1890, MANUAL, 0, DETOUT)
        End If

        Delay(2)                                                 '1680
        'RMA-03-001
RMA_3:
        dMeasurement = GetModMeterMeasurement("RMA-03-001     Phase Modulation Test, 30 RAD", "PMTV", "25", "35", "RAD", 300)
        frmSTest.proProgress.Value = 40

        If dMeasurement < 25 Or dMeasurement > 35 Then
            TestModAnalyzer = FAILED
            IncStepFailed()
            If (OptionFaultMode = SOEmode) And (OptionMode <> LOSmode) Then
                RegisterFailure(FGEN, ReturnTestNumber(RFMODANAL, 2, 1), dMeasurement, "RAD", 25, 35, "Phase Modulation Test, 30 RAD")
                dFH_MeasModT1 = dMeasurement
                S = "Use cable W16 to connect" & vbCrLf & vbCrLf
                S &= "   FG OUTPUT   to " & vbCrLf
                S &= "   SCOPE INPUT 1."
                DisplaySetup(S, "TETS FGO_SI1.jpg", 1)
                If AbortTest = True Then GoTo testcomplete
                nSystErr = WriteMsg(OSCOPE, "*RST; *CLS")
                nSystErr = WriteMsg(OSCOPE, "AUT")     'Set Autoscale mode
                Delay(2)
                nSystErr = WriteMsg(OSCOPE, "DIG CHAN1")
                nSystErr = WriteMsg(OSCOPE, "MEAS:SOUR CHAN1")
                nSystErr = WriteMsg(OSCOPE, "MEAS:VAMP?")
                'RMA-03-D01
                dMeas = FetchMeasurement(OSCOPE, "Phase Modulation Diagnostic", ".04", "", "V", OSCOPE, "RMA-03-D01")
                'RMA-03-D02
                If (dMeas < 0.04) Or (dMeas > 9.0E+38) Then
                    Echo("RMA-03-D02     Phase Modulation Diagnostic")
                    RegisterFailure(FGEN, ReturnTestNumber(RFMODANAL, 3, 2, "D"), dFH_MeasModT1, "RAD", 25, 35, "Phase Modulation Diagnostic")
                    Echo("    Could not complete RF Modulation Analyzer test due to a Function Generator failure.")
                    TestModAnalyzer = FGEN
                    GoTo TestComplete
                Else
                    S = "Use cable W17 to connect " & vbCrLf & vbCrLf
                    S &= "  FG OUTPUT   to " & vbCrLf
                    S &= "  RF STIM  FM  INPUT."
                    DisplaySetup(S, "TETS FGO_SFM.jpg", 1)
                    If AbortTest = True Then GoTo testcomplete
                End If
            End If
        Else
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "RFMA" And OptionStep = LoopStepNo Then
            GoTo RMA_3
        End If
        LoopStepNo += 1

        iModTest1Passed = (dMeasurement >= 25) And (dMeasurement <= 35)
        frmSTest.proProgress.Value = 50

        '**********************************************
        '** Perform Frequency Modulation Measurements
        nSystErr = WriteMsg(FGEN, "*RST;*CLS")
        nSystErr = WriteMsg(FGEN, "VOLT 150E-3") '160e-3
        nSystErr = WriteMsg(FGEN, "FREQ 1E4")
        nSystErr = WriteMsg(FGEN, "FUNC:SHAP SIN")
        nSystErr = WriteMsg(FGEN, "OUTP ON")


        If GIGA_DOWN = True Then  ' GT55210A
            InitStatus = atxmlDF_gt55210a_setLO("RF_CNTR_1", 2080, MANUAL, 0, DETOUT)
        Else
            InitStatus = atxmlDF_eip1315a_setLO("RF_CNTR_1", 1890, MANUAL, 0, DETOUT)
        End If

        Delay(2)                                                 '1680
        'RMA-04-001
RMA_4:
        dMeasurement = GetModMeterMeasurement("RMA-04-001     Frequency Modulation Test, 400 kHz", "FMTV", "300", "500", "KHz", 0)
        If dMeasurement < 300000.0# Or dMeasurement > 500000.0# Then
            IncStepFailed()
            TestModAnalyzer = FAILED
            ' 5/23/02 Nick Sazonov    Amplitude to Frequency
            RegisterFailure(RFSYN, ReturnTestNumber(RFMODANAL, 4, 1), dMeasurement, "Hz", 300000, 500000, "Frequency Modulation Test, 400 KHz")
            dFH_MeasModT2 = dMeasurement
            frmSTest.proProgress.Value = 70
        Else
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "RFMA" And OptionStep = LoopStepNo Then
            GoTo RMA_4
        End If
        LoopStepNo += 1

        iModTest2Passed = (dMeasurement >= 300000 And dMeasurement <= 500000)
        '**********************************************
        '** Perform Amplitude Modulation Measurements

        S = "Move cable W15 connection from " & vbCrLf & vbCrLf
        S &= "  RF STIM  FM  MODULATION INPUT  to" & vbCrLf
        S &= "  RF STIM  AM  MODULATION INPUT. "
        DisplaySetup(S, "TETS FGO_SAM.jpg", 1)
        If AbortTest = True Then GoTo testcomplete

        nSystErr = WriteMsg(RFSYN, "*RST")
        nSystErr = WriteMsg(RFSYN, "*CLS")
        nSystErr = WriteMsg(RFSYN, "FREQ 1945E6")
        nSystErr = WriteMsg(RFSYN, "POW -25 DBM")
        nSystErr = WriteMsg(RFSYN, ":FM:STATE OFF;:PULM:STATE OFF;:AM:STATE ON")
        nSystErr = WriteMsg(RFSYN, ":OUTP ON")

        nSystErr = WriteMsg(FGEN, "*RST;*CLS")
        nSystErr = WriteMsg(FGEN, "FUNC:SHAP SIN")
        nSystErr = WriteMsg(FGEN, "VOLT 0.5")
        nSystErr = WriteMsg(FGEN, "VOLT:OFFS 0")
        nSystErr = WriteMsg(FGEN, "FREQ 1E3")
        nSystErr = WriteMsg(FGEN, "OUTP ON")

        If GIGA_DOWN = True Then 'GT55210A()
            InitStatus = atxmlDF_gt55210a_setLO("RF_CNTR_1", 2000, MANUAL, 0, DETOUT)
        Else
            InitStatus = atxmlDF_eip1315a_setLO("RF_CNTR_1", 1890, MANUAL, 0, DETOUT)
        End If
        Delay(0.2)

        nSystErr = WriteMsg(RFMODANAL, "CL") ' P1 AM" 'talks to mod meter
        'Clear all errors, P+, AM modulation
        Delay(3)

        frmSTest.proProgress.Value = 80

RMA_5:  'RMA-05-001
        ' Statement 'FR 55E06 Sets frequency value
        dMeasurement = GetModMeterMeasurement#("RMA-05-001     Amplitude Modulation Test, 50 %", "FR 55E06 AMTV", "40", "60", "%", 2500)
        If dMeasurement < 40.0# Or dMeasurement > 60.0# Then
            IncStepFailed()
            TestModAnalyzer = FAILED
            If (OptionFaultMode = SOEmode) And (OptionMode <> LOSmode) Then
                RegisterFailure(RFSYN, ReturnTestNumber(RFMODANAL, 5, 1), dMeasurement, "%", 40, 60, "Amplitude Modulation Test, 50 %")
                dFH_Meas = dMeasurement
                nSystErr = vxiClear(RFCOUNTER)
                nSystErr = WriteMsg(RFCOUNTER, "*RST; *CLS")
                nSystErr = WriteMsg(RFCOUNTER, "INP1:IMP 50")
                nSystErr = WriteMsg(RFCOUNTER, "INP2:IMP 50")
                nSystErr = WriteMsg(RFCOUNTER, "INP1:COUP AC")
                nSystErr = WriteMsg(RFCOUNTER, "MEAS1:DC?")
                nSystErr = ReadMsg(RFCOUNTER, DCLevel)
                nSystErr = WriteMsg(RFCOUNTER, "SENS1:EVEN:LEV " & CStr(Val(DCLevel)))
                nSystErr = WriteMsg(RFCOUNTER, "SENS1:EVEN:HYST MAX")

                nSystErr = WriteMsg(RFCOUNTER, "MEAS:FREQ? 1E3")
                'RMA-05-D01
                dMeasurement = FetchMeasurement(RFCOUNTER, "AM Diagnostic Test", "9E2", "11E2", "Hz", RFCOUNTER, "RMA-05-D01")
                'RMA-05-D02
                If dMeasurement < 900.0# Or dMeasurement > 1100.0# Then
                    Echo("RMA-05-D02")
                    RegisterFailure(RFSYN, ReturnTestNumber(RFMODANAL, 5, 2, "D"), dFH_Meas, "Hz", 40000, 60000, "AM Diagnostic Test")
                    Echo("    Could not complete RF Modulation Analyzer test due to a RF Synthesizer failure.")
                    TestModAnalyzer = RFSYN
                    GoTo TestComplete
                Else
                    'RMA-05-D03
                    Echo("RMA-05-D03")
                    RegisterFailure(RFMODANAL, ReturnTestNumber(RFMODANAL, 5, 3, "D"), dFH_Meas, "Hz", 40000, 60000, "AM Diagnostic Test")
                    GoTo TestComplete
                End If
            End If
            If iModTest1Passed Xor iModTest2Passed Then
                'RMA-05-D04
                If iModTest2Passed Then
                    Echo("RMA-05-D04     Phase Modulation Test Diagnostic")
                    RegisterFailure(RFMODANAL, ReturnTestNumber(RFMODANAL, 5, 4, "D"), dFH_MeasModT1, "RAD", 25, 35, "Phase Modulation Test , 30 RAD  FAILED")
                    GoTo TestComplete
                ElseIf iModTest1Passed Then
                    'RMA-05-D05
                    Echo("RMA-05-D05     Frequency Modulation Test Diagnostic")
                    RegisterFailure(RFSYN, ReturnTestNumber(RFMODANAL, 5, 5, "D"), dMeasurement, "Hz", 400000, 500000, "Amplitude Modulation Test, 400 kHz  FAILED")
                End If
            End If
            'RMA-05-D06
            If Not (iModTest1Passed Or iModTest2Passed) Then
                Echo("RMA-05-D06")
                RegisterFailure(RFSYN, ReturnTestNumber(RFMODANAL, 5, 6, "D"), dFH_MeasModT2, "RAD", 25, 35, "Possible RF Measurement Analyzer failure.")
                TestModAnalyzer = RFSYN
                Echo("    Could not complete RF Measurement test due to an RF Synthesizer failure. (Possible RF Measurement Analyzer failure.)")
                GoTo TestComplete
            End If
        Else
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "RFMA" And OptionStep = LoopStepNo Then
            GoTo RMA_5
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

        If AbortTest = True Then
            If TestModAnalyzer = FAILED Then
                ReportFailure(RFMODANAL)
            Else
                ReportUnknown(RFMODANAL)
                TestModAnalyzer = UNTESTED
            End If
            sMsg = vbCrLf
            sMsg &= "      **********************************************" & vbCrLf
            sMsg &= "      *    RF Modulation Analyzer tests aborted!   *" & vbCrLf
            sMsg &= "      **********************************************" & vbCrLf
            Echo(sMsg)
        ElseIf TestModAnalyzer = PASSED Then
            ReportPass(RFMODANAL)
        ElseIf TestModAnalyzer = FAILED Then
            ReportFailure(RFMODANAL)
        Else
            ReportUnknown(RFMODANAL)
        End If
    End Function

End Module