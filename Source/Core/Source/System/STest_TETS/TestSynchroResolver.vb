'Option Strict On
Option Explicit On

Public Module modSynchroResolver

    '**************************************************************
    '* Nomenclature   : ATS-TETS SYSTEM SELF TEST                 *
    '*                  Synchro Resolver                          *
    '* Version        : 2.0                                       *
    '* Last Update    : Apr 1, 2017                               *
    '* Purpose        : Self Test Procedure for Synchro Resolver  *
    '* Target         : North Atlantic VXI 65CS4                  *
    '* Reference      : NAI VXI 65CS4 Specification Manual        *
    '*                : P/N 65CS4_A001 Rev 5.2                    *
    '**************************************************************

    Dim iTestStep As Integer 'Test Step Counter
    Dim iSubStep As Integer 'Test SubTest Counter
    Dim dAngle(0 To 8) As Double
    Dim sMeasured As String 'Holds instrument return strings
    Dim dMeasured As Double 'Holds converted string values

    Public iSelMode As Short 'Indicates user selection from the Instruct box
    Public Const ALLTESTS As Short = 0 'Selected all tests or adjustments
    Public Const USER_TERMINATE As Short = -2 'Stop tests or adjustments

    Public Const MAX_LOW As Double = -1.0E+300 '\__  Used to replace 'dMeasured' values when error conditions exist
    Public Const MAX_HIGH As Double = 1.0E+300 '/

    Public bFailedAny As Boolean 'This flag designates that some test within a test group failed.
    ' It is initialized to False by 'bSetupTest' or manually when necessary.
    ' It is set by 'Test' when a test fails, or can be set manually.
    Public bTimedOut As Boolean 'This flag is set when an instrument does not respond to
    ' a measurement query within that instruments TMO time.
    ' It is initialized to False by 'bSetupTest'.

    'Public bPassed As Boolean    '\   These flags are set by the 'Test' procedure to
    Public bFailed As Boolean ' \  define the status of the current test.  The
    Public bOutHi As Boolean ' /  test status represented by each is self-evident.
    Public bOutLo As Boolean '/


    Public Function TestSR() As Integer

        Dim SelfTest As String
        Dim S As String
        Dim x As Integer
        Dim LoopStepNo As Integer
        Dim iStepNumber As Integer

        HelpContextID = 1240
        frmSTest.proProgress.Maximum = 100
        frmSTest.proProgress.Value = 0

        'Synchro Resolver Title block
        EchoTitle(InstrumentDescription(SYN_RES) & " ", True)
        EchoTitle("Synchro/Resolver Test", False)
        TestSR = UNTESTED
        If AbortTest = True Then GoTo TestComplete
        AbortTest = False

        If FirstPass = True Then
            S = "1. Remove all cable connections from the SAIF." & vbCrLf & vbCrLf
            S = S & "2. Connect the Synchro Resolver shorting plug adapter (93006N2114) to the secondary chassis S/R I/O connector."
            DisplaySetup(S, "TETS SR_1.jpg", 2)
            If AbortTest = True Then GoTo testcomplete
        End If
        TestSR = PASSED

        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete

        iTestStep = 1
        iSubStep = 1
        sTNum = "S/R-01-001"
        LoopStepNo = 1

SR1:
        Echo(sTNum & " S/R Built-In Test (BIT takes 40 seconds)")
        SelfTest = ""
        nSystErr = WriteMsg(SYN_RES, "*TST?")
        frmSTest.proProgress.Value = 0

        For x = 1 To 50 ' should exit at either 19 or 39
            nSystErr = ReadMsg(SYN_RES, SelfTest)
            If nSystErr = 0 Or SelfTest <> "" Then Exit For
            frmSTest.proProgress.Value = frmSTest.proProgress.Value + 1 ' +39 here
            If AbortTest = True Then GoTo TestComplete
            Application.DoEvents()
        Next x

        'S/R-01-001
        If SelfTest = "Self Test Passed" And nSystErr = 0 Then
            Echo(FormatResults(True, "Synchro/Resolver Built-In Test"))
            IncStepPassed()
        Else
            Echo(FormatResults(False, "Built-In Test Result = " & StripNullCharacters(SelfTest), sTNum))
            RegisterFailure(CStr(SYN_RES), "S/R-01-001", , , , , sGuiLabel(SYN_RES) & " FAILED Built-in Test")
            TestSR = FAILED
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        End If
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "S/R" And OptionStep = LoopStepNo Then
            GoTo SR1
        End If
        frmSTest.proProgress.Value = 36

        'S/R-02-001 to 008
        ' Test 5a  'Single Speed Simulation (Synchro) CH1
        '   "1. Connect SD CH1 S1 to DS CH1 S1"
        '   "2. Connect SD CH1 S2 to DS CH1 S2"
        '   "3. Connect SD CH1 S3 to DS CH1 S3"
        '   "4. Connect SD CH1 S4 to DS CH1 S4"

        dAngle(1) = 0
        dAngle(2) = 45
        dAngle(3) = 90
        dAngle(4) = 135
        dAngle(5) = 180
        dAngle(6) = 225
        dAngle(7) = 270
        dAngle(8) = 315


        nSystErr = WriteMsg(SYN_RES, "REF_GEN1 FREQ 400 ") ' sets REF_GEN1 freq to 400 hz
        nSystErr = WriteMsg(SYN_RES, "REF_GEN1 VOLT 26 ") ' sets REF_GEN1 volt to 26v
        nSystErr = WriteMsg(SYN_RES, "REF_GEN1 STATE CLOSED ") ' sets REF_GEN1 relay closed
        nSystErr = WriteMsg(SYN_RES, "SDH1 REF_SOURCE INT ") ' sets DSH1 reference source to internal
        nSystErr = WriteMsg(SYN_RES, "DSH1 REF_SOURCE EXT ") ' sets DSH1 reference source to external
        nSystErr = WriteMsg(SYN_RES, "SDH1 MODE SYN ") ' sets SDH1 input mode to synchronizer
        nSystErr = WriteMsg(SYN_RES, "SDH2 RATIO 1 ") ' sets SDH2 input ratio to 1
        nSystErr = WriteMsg(SYN_RES, "SDH1 STATE CLOSED ") ' sets SDH1 input relay closed
        nSystErr = WriteMsg(SYN_RES, "DSH1 MODE SYN ") ' sets DSH1 mode to synchronizer
        nSystErr = WriteMsg(SYN_RES, "DSH2 RATIO 1 ") ' sets DSH2 ratio to 1
        nSystErr = WriteMsg(SYN_RES, "DSH1 REF_VOLT_IN 26 ") ' sets DSH1 reference voltage to 26VDC
        nSystErr = WriteMsg(SYN_RES, "DSH1 VLL_VOLT 26 ") ' sets DSH1 line to line voltage
        nSystErr = WriteMsg(SYN_RES, "DSH1 STATE CLOSED ") ' sets DSH1 output relay closed

        iTestStep = 2
        LoopStepNo = 2
        Echo(vbCrLf & "Testing Single Speed Simulation (Synchro/Resolver) CH1")
        For iStepNumber = 1 To 8 ' bb using For iSubStep = 1 to 8  caused warnings?
            iSubStep = iStepNumber
            sTNum = "S/R-" & Format(iTestStep, "00") & "-" & Format(iSubStep, "000")
SR2:
            nSystErr = WriteMsg(SYN_RES, "DSH1 ANGLE " & dAngle(iSubStep) & " ") ' set DSH1 angle
            Delay(2)
            nSystErr = WriteMsg(SYN_RES, "SDH1 Angle? ") ' read SD1 angle
            Delay(1) : Fetch(SYN_RES)
            'May measure 359.xxx deg for 0.0 deg - compensate
            If dMeasured > 359 Then
                dMeasured = 360 - dMeasured
            End If
            RecordTest(sTNum, "DSH1/SDH1 ANGLE " & dAngle(iSubStep) & " Deg", dAngle(iSubStep) - 0.02, dAngle(iSubStep) + 0.02, dMeasured, "Deg")
            If (dMeasured < dAngle(iSubStep) - 0.02) Or (dMeasured > dAngle(iSubStep) + 0.02) Then
                TestSR = FAILED
                IncStepFailed()
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else
                IncStepPassed()
            End If
            Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete
            If OptionMode = LOSmode And OptionTestName = "S/R" And OptionStep = LoopStepNo Then
                GoTo SR2
            End If
            frmSTest.proProgress.Value = frmSTest.proProgress.Value + 4
            LoopStepNo += 1
        Next iStepNumber


        'S/R-03-001 to 008
        ' Test 6a  'Single Speed Simulation (Synchro) CH2
        '   "1. Connect SD CH2 S1 to DS CH2 S1"
        '   "2. Connect SD CH2 S2 to DS CH2 S2"
        '   "3. Connect SD CH2 S3 to DS CH2 S3"
        '   "4. Connect SD CH2 S4 to DS CH2 S4"

        ' Test 6b  'Single Speed Simulation (Resolver) CH2
        dAngle(1) = 0
        dAngle(2) = 45
        dAngle(3) = 90
        dAngle(4) = 135
        dAngle(5) = 180
        dAngle(6) = 225
        dAngle(7) = 270
        dAngle(8) = 315

        nSystErr = WriteMsg(SYN_RES, "REF_GEN1 FREQ 400 ") ' sets REF_GEN1 freq to 400 hz
        nSystErr = WriteMsg(SYN_RES, "REF_GEN1 VOLT 26 ") ' sets REF_GEN1 volt to 26v
        nSystErr = WriteMsg(SYN_RES, "REF_GEN1 STATE CLOSED ") ' sets REF_GEN1 relay closed
        nSystErr = WriteMsg(SYN_RES, "SDH2 REF_SOURCE EXT ") ' sets SDH2 reference source to external
        nSystErr = WriteMsg(SYN_RES, "DSH2 REF_SOURCE INT ") ' sets DSH2 reference source to internal
        nSystErr = WriteMsg(SYN_RES, "SDH2 MODE RSL ") ' sets SDH2 input mode to resolver
        nSystErr = WriteMsg(SYN_RES, "SDH1 RATIO 1 ") ' sets SDH1 input ratio to 1
        nSystErr = WriteMsg(SYN_RES, "SDH2 STATE CLOSED ") ' sets SDH2 input relay closed
        nSystErr = WriteMsg(SYN_RES, "DSH2 MODE RSL ") ' sets DSH2 mode to resolver
        nSystErr = WriteMsg(SYN_RES, "DSH1 RATIO 1 ") ' sets DSH1 ratio to 1
        nSystErr = WriteMsg(SYN_RES, "DSH2 REF_VOLT_IN 26 ") ' sets DSH2 reference voltage to 26VDC
        nSystErr = WriteMsg(SYN_RES, "DSH2 VLL_VOLT 26 ") ' sets DSH2 line to line voltage
        nSystErr = WriteMsg(SYN_RES, "DSH2 STATE CLOSED ") ' sets DSH2 output relay closed

        iTestStep = 3
        Echo(vbCrLf & "Testing Single Speed Simulation (Synchro/Resolver) CH2")
        For iStepNumber = 1 To 8 '  bb using For iSubStep = 1 to 8  caused warnings?
            iSubStep = iStepNumber
            sTNum = "S/R-" & Format(iTestStep, "00") & "-" & Format(iSubStep, "000")
SR3:
            nSystErr = WriteMsg(SYN_RES, "DSH2 ANGLE " & CStr(dAngle(iSubStep)) & " ") ' set DSH2 angle
            Delay(2)
            nSystErr = WriteMsg(SYN_RES, "SDH2 Angle? ") ' read SDH2 angle
            Delay(1) : Fetch(SYN_RES)
            'May measure 359.xxx deg for 0.0 deg - compensate
            If dMeasured > 359 Then
                dMeasured = 360 - dMeasured
            End If
            RecordTest(sTNum, "DSH2/SDH2 ANGLE " & dAngle(iSubStep) & " Deg", dAngle(iSubStep) - 0.02, dAngle(iSubStep) + 0.02, dMeasured, "Deg")
            If (dMeasured < dAngle(iSubStep) - 0.02) Or (dMeasured > dAngle(iSubStep) + 0.02) Then
                TestSR = FAILED
                IncStepFailed()
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else
                IncStepPassed()
            End If
            Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete
            If OptionMode = LOSmode And OptionTestName = "S/R" And OptionStep = LoopStepNo Then ' 3 to 10
                GoTo SR3
            End If
            frmSTest.proProgress.Value = frmSTest.proProgress.Value + 4
            LoopStepNo += 1
        Next iStepNumber
        frmSTest.proProgress.Value = 100


TestComplete:
        frmSTest.proProgress.Value = 100
        frmSTest.proProgress.Maximum = 100
        frmSTest.cmdAbort.Text = "Abort Test"
        frmSTest.cmdPause.Text = "Pause Test"
        nSystErr = WriteMsg(SYN_RES, "*RST") ' reset synchro
        Application.DoEvents()
        If AbortTest = True Then
            If TestSR = FAILED Then
                ReportFailure(SYN_RES)
            Else
                ReportUnknown(SYN_RES)
                TestSR = UNTESTED
            End If
            sMsg = vbCrLf
            sMsg &= "      **********************************************" & vbCrLf
            sMsg &= "      *        Syncro/Resolver tests aborted!       *" & vbCrLf
            sMsg &= "      **********************************************" & vbCrLf
            Echo(sMsg)
        ElseIf TestSR = PASSED Then
            ReportPass(SYN_RES)
        ElseIf TestSR = FAILED Then
            ReportFailure(SYN_RES)
        Else
            ReportUnknown(SYN_RES)
        End If
        If CloseProgram = True Then
            EndProgram()
        End If

    End Function



    Function Fetch(ByVal Instrument As Integer) As Integer
        'DESCRIPTION:
        '   This routine retrieves a measurement from a specified instrument,
        '     and stores it in Global dMeasured.
        '   It also sets bTimedOut if a timeout occurs.
        'PARAMETERS:
        '   InstrumentIndex  = The handle to the instrument from which to
        '    obtain the measurement
        'RETURNS:
        '   VISA Error code
        'GLOBAL VARIABLES MODIFIED:
        '   dMeasured   sMeasured    bTimedOut

        '  bTimedOut = False
        nSystErr = ReadMsg(Instrument, sMeasured)

        Select Case nSystErr
            Case VI_SUCCESS
                dMeasured = Val(sMeasured)
            Case VI_ERROR_TMO
                dMeasured = MAX_LOW
                '    bTimedOut = True    'Set the 'TIMED OUT' flag for the 'TEST' sub

            Case Else
                dMeasured = MAX_LOW
        End Select

        Fetch = nSystErr

    End Function





End Module