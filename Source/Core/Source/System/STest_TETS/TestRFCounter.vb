'Option Strict Off
Option Explicit On


Public Module modRFCNTTets

    '**************************************************************
    '* Nomenclature   : ATS-TETS SYSTEM SELF TEST                 *
    '*                  RF Counter Test                           *
    '* Written By     : Michael McCabe                            *
    '* Version        : 2.0                                       *
    '* Last Update    : Apr 1, 2017                               *
    '* Purpose        : This module contains code for the         *
    '*                  RF Counter Self Test                      *
    '**************************************************************
    Declare Function eip1315a_init Lib "RFCNT.dll" (ByVal resourceName As String, ByRef instrumentHandle As Integer) As Integer
    'Declare Function eip1315a_setLO Lib "RFCNT.dll" (ByVal theSession As Integer, ByVal frequency As Double, ByVal mode As Short, ByVal attn As Integer, ByVal IFDET As Short) As Integer
    ''Declare Function eip1315a_setLO Lib "RFCNT.dll" (ByVal theSession As Integer, ByVal frequency As Double, ByVal mode As Integer, ByVal attn As Integer, ByVal IFDET As Integer) As Integer
    ''Declare Function eip1315a_close Lib "RFCNT.dll" (ByVal instrumentHandle As Integer) As Integer

    'DLL                    Function                 Legacy Function            Resource Name		TETS RF CICL Assets
    'AtxmlApi.dll	        atxml_WriteCmds          viWrite                    ANY                 RF_MEASAN_1
    'AtxmlApi.dll           atxml_ReadCmds           viRead                     ANY                 RF_CNTR_1
    'AtxmlDriverFunc.dll    atxmlDF_viSetAttribute   viSetAttribute             ANY                 RF_PWR_1
    'AtxmlDriverFunc.dll    atxmlDF_viClear          viClear                    ANY                 RFGEN_1
    'AtxmlDriverFunc.dll    atxmlDF_rf_peaktune      rf_peaktune                RF_CNTR_1		
    'AtxmlDriverFunc.dll    atxmlDF_eip1315a_setLO   eip1315a_setLO             RF_CNTR_1		
    'AtxmlDriverFunc.dll    atxmlDF_gt55210a_setLO   gt55210a_setLO             RF_CNTR_1		
    'AtxmlDriverFunc.dll    atxmlDF_rf_measfreq      rf_measfreq                RF_CNTR_1		
    'AtxmlDriverFunc.dll    atxmlDF_rf_measpw        rf_measpw                  RF_CNTR_1		
    'AtxmlDriverFunc.dll    atxmlDF_rf_measpd        rf_measpd                  RF_CNTR_1		


    ' Declare Function gt55210a_init Lib "RFCNT.dll" (ByVal resourceName As String, ByRef instrumentHandle As Integer) As Integer
    ' Declare Function gt55210a_setLO Lib "RFCNT.dll" (ByVal theSession As Integer, ByVal MHz As Double, ByVal mode As Short, ByVal attn As Integer, ByVal IFDET As Integer) As Integer
    'Declare Function gt55210a_setLO Lib "RFCNT.dll" (ByVal theSession As Integer, ByVal MHz As Double, ByVal mode As Integer, ByVal attn As Integer, ByVal IFDET As Integer) As Integer
    ' Declare Function gt55210a_writeInstrData Lib "RFCNT.dll" (ByVal instrSession As Integer, ByVal writeBuffer As String) As Integer
    'Declare Function gt55210a_readInstrData Lib "RFCNT.dll" (ByVal instrSession As Integer, ByVal numBytes As Short, ByVal rdBuf As String, ByRef bytesRead As Integer) As Integer
    'Declare Function gt55210a_readInstrData Lib "RFCNT.dll" (ByVal instrSession As Integer, ByVal numBytes As Integer, ByVal rdBuf As String, ByRef bytesRead As Integer) As Integer
    'Declare Function gt55210a_close Lib "RFCNT.dll" (ByVal instrumentHandle As Integer) As Integer

    'Declare Function rfcnt_init Lib "RFCNT.dll" (ByVal resourceName As String, ByRef instrumentHandle As Integer) As Integer
    'Declare Function rfcnt_reset Lib "RFCNT.dll" (ByVal instrSession As Integer) As Integer
    'Declare Function rfcnt_writeInstrData Lib "RFCNT.dll" (ByVal instrSession As Integer, ByVal writeBuffer As String) As Integer
    '  Declare Function rfcnt_readInstrData Lib "RFCNT.dll" (ByVal instrSession As Integer, ByVal numBytes As Short, ByVal rdBuf As String, ByRef bytesRead As Integer) As Integer
    'Declare Function rfcnt_readInstrData Lib "RFCNT.dll" (ByVal instrSession As Integer, ByVal numBytes As Integer, ByVal rdBuf As String, ByRef bytesRead As Integer) As Integer
    'Declare Function rfcnt_close Lib "RFCNT.dll" (ByVal instrumentHandle As Integer) As Integer

    '  Declare Function rf_measfreq Lib "RFCNT.dll" (ByVal ihDC As Integer, ByVal ihCNT As Integer, ByVal expectedfreq As Double, ByVal attn As Integer, measfreq As Double) As Integer
    '  Declare Function rf_peaktune Lib "RFCNT.dll" (ByVal ihDC As Integer, ByVal ihCNT As Integer, measfreq As Double, ByVal attn As Integer) As Integer
    '  Declare Function rf_measpw Lib "RFCNT.dll" (ByVal ihDC As Integer, ByVal ihCNT As Integer, ByVal carrierfreq As Double, ByVal expectedpw As Double, ByVal attn As Integer, ByRef measpw As Double) As Integer
    'Declare Function rf_measpd Lib "RFCNT.dll" (ByVal ihDC As Integer, ByVal ihCNT As Integer, ByVal carrierfreq As Double, ByVal expectedpw As Double, ByVal attn As Integer, ByRef measpd As Double) As Integer
    'Declare Function atlas_rf_peaktune Lib "RFCNT.dll" (ByVal ihDC As Integer, ByVal ihCNT As Integer, measfreq As Double, ByVal attn As Integer, ByVal freqmin As Double, ByVal freqmax As Double) As Integer
    '   Declare Function setbusy Lib "RFCNT.dll" (ByVal flag_SAIS As Short) As Integer
    'Declare Function setbusy Lib "RFCNT.dll" (ByVal flag_SAIS As Integer) As Integer
    'Declare Function readbusy Lib "RFCNT.dll" () As Integer

    Const AUTO = 0
    Const MANUAL = -1
    Const DETOUT = 0
    Const IFOUT = 1
    Const NO_MODEL = -254
    Const BUSY = -255

    Dim gRetVal As Integer
    Dim S As String = ""


    Public Function TestRFCounter() As Integer
        'DESCRIPTION:
        '   This routine tests the RF Counter and returns PASSED or FAILED
        'RETURNS:
        '   PASSED if the RF COUNTER test passes or FAILED if a failure occurs
        Dim SSelfTest As String = ""
        Dim InitStatus As Integer
        Dim DCLevel As String = ""
        Dim dMeasurement As Double
        Dim LoopStepNo As Integer = 1
        'Dim SError As String            'Error Message


        TestRFCounter = UNTESTED
        'RF Counter/Timer Test Title block
        EchoTitle(InstrumentDescription(RFCOUNTER), True)
        EchoTitle("RF Counter/Timer Test", False)
        frmSTest.proProgress.Maximum = 100
        frmSTest.proProgress.Value = 1

        If Not RunningEndToEnd = True Then
            MsgBox("Remove all cable connections from the SAIF.")
        Else
            MsgBox("Remove only cable W25 from the SAIF.")
        End If


        'Reset Instruments
        nSystErr = vxiClear(FGEN)
        nSystErr = vxiClear(COUNTER)
        nSystErr = vxiClear(RFSYN)     ' rf synthesizer
        nSystErr = vxiClear(RFMODANAL) ' rf modulation analyzer
        nSystErr = vxiClear(RFCOUNTER) ' rf counter

        nSystErr = WriteMsg(RFCOUNTER, "*RST")
        nSystErr = WriteMsg(RFCOUNTER, "*CLS")
        nSystErr = WriteMsg(RFCOUNTER, "INP1:IMP 50")
        nSystErr = WriteMsg(RFCOUNTER, "INP2:IMP 50")
        nSystErr = WriteMsg(FGEN, "*RST; *CLS")
        nSystErr = WriteMsg(COUNTER, "*RST; *CLS")
        nSystErr = WriteMsg(RFSYN, "*RST")
        nSystErr = WriteMsg(RFSYN, "*CLS")
        ''   nSystErr = WriteMsg(RFMODANAL, "PG99RE")

        TestRFCounter = PASSED

        '** Perform a COUNTER Power-up Self Test
RFCT_1:  Echo("RCT-01-001     RF Counter Built-In Test. . .")
        nSystErr = WriteMsg(RFCOUNTER, "*TST?")
        nSystErr = WaitForResponse(RFCOUNTER, 2)
        nSystErr = ReadMsg(RFCOUNTER, SSelfTest)
        'RCT-01-001
        If nSystErr <> 0 Or Val(SSelfTest) Then
            RegisterFailure(RFCOUNTER, ReturnTestNumber(RFCOUNTER, 1, 1), , , , , "RF Counter Built-in Test")
            TestRFCounter = FAILED
            Echo(FormatResults(False, "Built-In Test Result = " & StripNullCharacters(SSelfTest)))
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            Echo(FormatResults(True, "Counter Built-In Test")) '1
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "RFCT" And OptionStep = LoopStepNo Then
            GoTo RFCT_1
        End If
        frmSTest.proProgress.Value = 35
        LoopStepNo += 1
        S = "Use cable W22 to connect" & vbCrLf & vbCrLf
        S &= "   RF STIM OUTPUT   to " & vbCrLf
        S &= "   RF MEAS INPUT."
        DisplaySetup(S, "TETS RFM_STO.jpg", 1)
        If AbortTest = True Then GoTo testcomplete

        '** Test RF Counter Input **
        nSystErr = WriteMsg(RFSYN, "*RST")
        nSystErr = WriteMsg(RFSYN, "*CLS")
        nSystErr = WriteMsg(RFSYN, "FREQ 100E6")
        nSystErr = WriteMsg(RFSYN, "POW 10 DBM")
        nSystErr = WriteMsg(RFSYN, ":OUTP ON")

        If GIGA_DOWN = True Then ' down converter GT55210A
            InitStatus = atxmlDF_gt55210a_setLO("RF_CNTR_1", 0, MANUAL, 0, IFOUT)
        Else
            InitStatus = atxmlDF_eip1315a_setLO("RF_CNTR_1", 0, MANUAL, 0, IFOUT)
        End If
        frmSTest.proProgress.Value = 75

RFCT_2:  'RCT-02-001

        nSystErr = vxiClear(RFCOUNTER) ' rf counter
        nSystErr = WriteMsg(RFCOUNTER, "*RST; *CLS")
        nSystErr = WriteMsg(RFCOUNTER, "INP1:IMP 50")
        nSystErr = WriteMsg(RFCOUNTER, "INP2:IMP 50")
        nSystErr = WriteMsg(RFCOUNTER, "SENS:ROSC:SOUR EXT")   'Use DownConverter Time Base

        nSystErr = WriteMsg(RFCOUNTER, "INP1:COUP AC")
        nSystErr = WriteMsg(RFCOUNTER, "MEAS1:DC?")
        nSystErr = ReadMsg(RFCOUNTER, DCLevel)
        nSystErr = WriteMsg(RFCOUNTER, "SENS1:EVEN:LEV " & CStr(Val(DCLevel)))
        nSystErr = WriteMsg(RFCOUNTER, "SENS1:EVEN:HYST MAX")
        nSystErr = WriteMsg(RFCOUNTER, "MEAS:FREQ? 100E6")


        dMeasurement = FetchMeasurement(RFCOUNTER, "Frequency Measurement Test", "90E6", "110E6", "Hz", RFDOWN, "RCT-02-001")
        nSystErr = vxiClear(RFCOUNTER)
        nSystErr = WriteMsg(RFCOUNTER, "*RST; *CLS")
        nSystErr = WriteMsg(RFCOUNTER, "INP1:IMP 50")
        nSystErr = WriteMsg(RFCOUNTER, "INP2:IMP 50")
        If dMeasurement < 90000000.0 Or dMeasurement > 110000000.0 Then
            TestRFCounter = FAILED
            IncStepFailed()
            If (OptionFaultMode = SOEmode) And (OptionMode <> LOSmode) Then
                dFH_Meas = dMeasurement
                OpenEmiDoor()
                If AbortTest = True Then GoTo TestComplete
                If GIGA_DOWN = True Then ' Gigatronics 55210A
Step1:
                    S = "Disconnect the semi-rigid cable from the Det/IF Output"
                    S &= " on the front of RF Downconverter instrument."
                    DisplaySetup(S, "TETS DISC_DIF2.jpg", 1, True, 1, 2)
                    If AbortTest = True Then GoTo testcomplete
Step2:
                    S = "Use cable W16 to connect " & vbCrLf & vbCrLf
                    S &= "   Counter/Timer Input 1   to" & vbCrLf
                    S &= "   RF Downconverter Det/IF Output."
                    DisplaySetup(S, "TETS CI1_DIF2.jpg", 1, True, 2, 2)
                    If AbortTest = True Then GoTo testcomplete
                    If GoBack = True Then GoTo Step1
                Else                   ' EIP1315A
Step1b:
                    S = "Disconnect the semi-rigid cable from the Det/IF Output" & vbCrLf
                    S &= " on the front of RF Downconverter instrument."
                    DisplaySetup(S, "TETS DISC_DIF.jpg", 1, True, 1, 2)
                    If AbortTest = True Then GoTo testcomplete
Step2b:
                    S = "Use cable W16 to connect " & vbCrLf & vbCrLf
                    S &= "   Counter/Timer Input 1   to" & vbCrLf
                    S &= "   RF Downconverter Det/IF Output."
                    DisplaySetup(S, "TETS CI1_DIF.jpg", 1, True, 2, 2)
                    If AbortTest = True Then GoTo testcomplete
                    If GoBack = True Then GoTo Step1b
                End If

                nSystErr = vxiClear(COUNTER)
                nSystErr = WriteMsg(COUNTER, "*RST; *CLS")
                nSystErr = WriteMsg(COUNTER, "INP1:IMP 50")
                nSystErr = WriteMsg(COUNTER, "INP2:IMP 50")
                nSystErr = WriteMsg(COUNTER, "ATIM:TIME 60")
                nSystErr = WriteMsg(COUNTER, "ATIM OFF")
                nSystErr = WriteMsg(COUNTER, "INP1:COUP AC")
                nSystErr = WriteMsg(COUNTER, "INP1:IMP 50")
                nSystErr = WriteMsg(COUNTER, "MEAS1:DC?")
                nSystErr = ReadMsg(COUNTER, DCLevel)
                nSystErr = WriteMsg(COUNTER, "SENS1:EVEN:LEV " & CStr(Val(DCLevel)))
                nSystErr = WriteMsg(COUNTER, "MEAS:FREQ? 100E6")

                'RCT-02-D01
                dMeasurement = FetchMeasurement(COUNTER, "RF Counter Diagnostic Test", "90E6", "110E6", "Hz", RFDOWN, "RCT-02-D01")
                If dMeasurement < 90000000.0 Or dMeasurement > 110000000.0 Then
                    TestRFCounter = RFDOWN
                Else
                    If GIGA_DOWN = True Then  ' Gigatronics 55210A
Step1c:
                        S = "Disconnect the semi-rigid cable from the"
                        S &= " TIME BASE OUT jack on the front of RF CNTRL."
                        DisplaySetup(S, "TETS DISC_10M2.jpg", 1, True, 1, 2)
                        If AbortTest = True Then GoTo testcomplete
Step2c:
                        S = "Use cable W16 to connect" & vbCrLf & vbCrLf
                        S &= "   Counter/Timer Input 1   to" & vbCrLf
                        S &= "   RF CNTRL TIME BASE OUT jack."
                        DisplaySetup(S, "TETS CI1_D10M2.jpg", 1, True, 2, 2)
                        If AbortTest = True Then GoTo testcomplete
                        If GoBack = True Then GoTo Step1c
                    Else                  ' EIP1315A
Step1d:
                        S = "Disconnect the semi-rigid cable from the"
                        S &= " 10MHz In/Out Output on the front of RF Downconverter instrument."
                        DisplaySetup(S, "TETS DISC_10M.jpg", 1, True, 1, 2)
                        If AbortTest = True Then GoTo testcomplete
Step2d:
                        S = "Use cable W16 to connect" & vbCrLf & vbCrLf
                        S &= "   Counter/Timer Input 1   to" & vbCrLf
                        S &= "   RF Downconverter 10 MHz In/Out jack."
                        DisplaySetup(S, "TETS CI1_D10M.jpg", 1, True, 2, 2)
                        If AbortTest = True Then GoTo testcomplete
                        If GoBack = True Then GoTo Step1d
                    End If

                    nSystErr = vxiClear(COUNTER)
                    nSystErr = WriteMsg(COUNTER, "*RST; *CLS")
                    nSystErr = WriteMsg(COUNTER, "INP1:IMP 50")
                    nSystErr = WriteMsg(COUNTER, "INP2:IMP 50")
                    nSystErr = WriteMsg(COUNTER, "ATIM:TIME 60")
                    nSystErr = WriteMsg(COUNTER, "ATIM OFF")
                    nSystErr = WriteMsg(COUNTER, "INP1:COUP AC")
                    nSystErr = WriteMsg(COUNTER, "MEAS1:DC?")
                    nSystErr = ReadMsg(COUNTER, DCLevel)
                    nSystErr = WriteMsg(COUNTER, "SENS1:EVEN:LEV " & CStr(Val(DCLevel)))
                    nSystErr = WriteMsg(COUNTER, "MEAS:FREQ? 10E6")
                    'RCT-02-D02
                    dMeasurement = FetchMeasurement(COUNTER, "RF Counter External Reference Diagnostic Test", "9.5E6", "10.5E6", "Hz", RFDOWN, "RCT-02-D02")
                    'RCT-02-D03
                    If dMeasurement < 9500000.0 Or dMeasurement > 10500000.0 Then
                        Echo("RCT-02-D03     RF Counter External Reference Diagnostic Test")
                        TestRFCounter = RFDOWN
                        RegisterFailure(RFDOWN, ReturnTestNumber(RFCOUNTER, 2, 3, "D"), dFH_Meas, "Hz", 90000000, 110000000, "RF Counter External Reference Diagnostic Test")
                        'RCT-02-D04
                    Else
                        Echo("RCT-02-D04     RF Counter External Reference Diagnostic Test")
                        RegisterFailure(RFCOUNTER, ReturnTestNumber(RFCOUNTER, 2, 4, "D"), dFH_Meas, "Hz", 90000000, 110000000, "RF Counter External Reference Diagnostic Test")
                        TestRFCounter = FAILED
                    End If
                End If
            End If
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            Echo(FormatResults(True, "Counter Built-In Test")) '1
            IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "RFCT" And OptionStep = LoopStepNo Then
            GoTo RFCT_2
        End If

TestComplete:
        frmSTest.cmdAbort.Text = "Abort Test"
        frmSTest.proProgress.Value = 100
        frmSTest.cmdPause.Text = "Pause Test"
        nSystErr = WriteMsg(RFCOUNTER, "*RST; *CLS")
        nSystErr = WriteMsg(RFSYN, "OUTP OFF") ' turn off synthesizer
        nSystErr = WriteMsg(RFSYN, "*RST")
        nSystErr = WriteMsg(FGEN, "OUTP OFF")
        nSystErr = WriteMsg(FGEN, "*RST")
        nSystErr = WriteMsg(SWITCH1, "RESET") ' open all switches
        If AbortTest = True Then
            If TestRFCounter = FAILED Then
                ReportFailure(RFCOUNTER)
            Else
                ReportUnknown(RFCOUNTER)
                TestRFCounter = UNTESTED
            End If
            sMsg = vbCrLf
            sMsg &= "      **********************************************" & vbCrLf
            sMsg &= "      *           RF Counter tests aborted!        *" & vbCrLf
            sMsg &= "      **********************************************" & vbCrLf
            Echo(sMsg)
        ElseIf TestRFCounter = PASSED Then
            ReportPass(RFCOUNTER)
        ElseIf (TestRFCounter = FAILED) Then
            ReportFailure(RFCOUNTER)
        Else
            ReportUnknown(RFCOUNTER)
        End If
    End Function

    Public Sub OpenEmiDoor()
        'Description: This sub procedure tests to see if it has run before in the self test.
        'If it has not it will instruct the user on how to remove the EMI door to access the instruments.
        'Once it has run it will set a flag so that it will not be ran again during the same self test.
        'This code was added because some of the diagnostics require access to the instruments behind the EMI door.
        If Door_open = False Then
Step1:
            S = "Loosen the five (5) turnlock fasteners by turning them one quarter turn counter-clockwise."
            DisplaySetup(S, "TETS TRNLCK_FSTNR.jpg", 1, True, 1, 3)
            If AbortTest = True Then Exit Sub
Step2:
            S = "Pull the bottom of the EMI door out from the chassis and then slide the door down."
            DisplaySetup(S, "TETS OPENING_EMI.jpg", 1, True, 2, 3)
            If AbortTest = True Then Exit Sub
            If GoBack = True Then GoTo Step1
Step3:
            S = "The EMI door is now removed allowing access to the instruments."
            DisplaySetup(S, "TETS EMIDOOR_OPEN.jpg", 1, True, 3, 3)
            If AbortTest = True Then Exit Sub
            If GoBack = True Then GoTo Step2
            Door_open = True
        End If
    End Sub

End Module