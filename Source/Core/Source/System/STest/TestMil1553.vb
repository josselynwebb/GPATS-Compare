'Option Strict On
Option Explicit On


Public Module modMil1553Test


    '**************************************************************
    '* Nomenclature   : ATS-ViperT SYSTEM SELF TEST               *
    '*                  MIL-1553 Emulator Test                    *
    '* Version        : 2.0                                       *
    '* Last Update    : Apr 1, 2017                               *
    '* Purpose        : This module contains code for the         *
    '*                  MIL-1553 Emulator Self Test               *
    '*                  DDC BU-65553M2-607 MIL-STD-1553           *
    '**************************************************************

    Dim sXml As String
    Dim Read1553Data As String = ""

    Dim IEEEName As String = "exchange"
    Dim sRespTime As String = "0.000022s"
    Dim sMessageGap As String = "0.000100s"
    Dim sRole As String = "Slave"
    Dim sMode As String = "Con-RT"
    Dim sStatus As String = "LHLHLLLLLLLLLLLL"
    Dim sReceiveAddress As String = "10, 1"
    Dim sProcess As String = "Wait"
    Dim sExnm As String = "5"
    Dim sAttribute As String = "data"
    Dim S As String
    Dim xmlStatus As Long
    Dim lStatus As Long
    Dim lStatus2 As Long

    Dim sData As String = ""
    Dim sCommand As String = "LHLHLHLLLLHLHLHL"""
    Dim LoopStepNo As Integer

    Public Const FRAMETIME As Integer = 10000


    Function Test1553() As Short
        'DESCRIPTION:
        '   This routine tests MIL-1553 Emulator and returns PASSED or FAILED
        'RETURNS:
        '   PASSED if the tests MIL-1553 Emulator test passes or FAILED if a failure occurs

        AbortTest = False
        '1553 Emulator Test Title block
        EchoTitle(InstrumentDescription(MIL_STD_1553) & " ", True)
        EchoTitle("1553 Emulator Test", False)
        frmSTest.proProgress.Maximum = 100
        frmSTest.proProgress.Value = 1

        Test1553 = UNTESTED
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete

        nSystErr = WriteMsg(SWITCH1, "RESET") ' open all switches
        If FirstPass = True Then
            If (RunningEndToEnd = False And ControlStartTest = 0) Then
                S = "Use cable W26 (93006A9021) and " & vbCrLf
                S &= " a 78 Ohm Triax terminator (93006A9033) to connect" & vbCrLf & vbCrLf
                S &= "     1553 BUS A   to   1553 BUS B."
                DisplaySetup(S, "ST-1553-1.jpg", 1)
                If AbortTest = True Then GoTo TestComplete

            ElseIf RunningEndToEnd = True Or ControlStartTest = MIL_STD_1553 Then

Step1:
                S = "Use cable W26 (93006A9021) and a 78 Ohm Triax terminator (93006A9033) connect 1553 BUS A to BUS B ."
                DisplaySetup(S, "ST-1553-1.jpg", 1, True, 1, 4)
                If AbortTest = True Then GoTo TestComplete

Step2:
                ' install adapter SP1
                S = "Install Adapter (93006H6042) SP1-P1 onto the SAIF J1 connector. " & vbCrLf
                DisplaySetup(S, "ST-SP1-1.jpg", 1, True, 2, 4)
                If AbortTest = True Then GoTo TestComplete
                If GoBack = True Then GoTo Step1
Step3:
                ' install MF cable W203
                S = "Install MF cable W203 (93006H6044) as follows:" & vbCrLf
                S &= "1. Connect cable W203-P1 onto the SAIF J2 connector. " & vbCrLf
                S &= "2. Connect cable W203-P2 onto the SAIF front panel MF IN connector via a BNC 50 Ohm Feedthrough Terminator (93006H6786). " & vbCrLf
                S &= "3. Connect cable W203-P3 onto the SAIF front panel MF OUT connector. " & vbCrLf
                DisplaySetup(S, "ST-W203-1.jpg", 3, True, 3, 4)
                If AbortTest = True Then GoTo TestComplete
                If GoBack = True Then GoTo Step2
Step4:
                ' install HF cabled D21,W202,W207, sma to N adapter, N type 50 Ohm terminator
                S = "Setup for High Frequency switches:" & vbCrLf
                S &= "1. DO NOT torque J3 and J4 connections (hand tighten only)." & vbCrLf
                S &= "2. Use cable W207 (SMA to N) to connect SAIF J3-1   to   SAIF front panel HF IN. " & vbCrLf
                S &= "3. Use cable W202 (#1) to connect SAIF J3-2   to   SAIF J4-2. " & vbCrLf
                S &= "4. Use cable W202 (#1) to connect SAIF J3-3   to   SAIF J4-3. " & vbCrLf
                S &= "5. Use cable W202 (#1) to connect SAIF J3-4   to   SAIF J4-4. " & vbCrLf
                S &= "6. Use cable W202 (#2) to connect SAIF J3-5   to   SAIF J4-5. " & vbCrLf
                S &= "7. Use cable W202 (#2) to connect SAIF J3-6   to   SAIF J4-6. " & vbCrLf
                S &= "8. Use cable W202 (#2) to connect SAIF J3-7   to   SAIF J4-7. " & vbCrLf
                S &= "9. Use a SMA to N adapter to connect SAIF J4-1 to 50 Ohm terminator. " & vbCrLf
                S &= "10. Use cable W21 (Dual Banana Jack to N) to connect SAIF DMM-INPUT HI/LO   to   SAIF front panel HF OUT." & vbCrLf
                DisplaySetup(S, "ST-W202-1.jpg", 10, True, 4, 4)
                If GoBack = True Then GoTo Step3

Step5:
                ' install W15 cable from Scope Input 1 to DTS Cal 2.
                S = "Setup for DTS Tests:" & vbCrLf
                S &= "Use cable W15 to connect SCOPE INPUT 1   to   UTIL DTS CAL 2 connector."
                DisplaySetup(S, "ST-DTS-W15-01.jpg", 1, True, 2, 2)
                If GoBack = True Then GoTo Step4

            End If
        End If

        Test1553 = PASSED
        On Error Resume Next
        ChDir(ProgramPath)

        sRole = "Master"
        sMode = "Con-RT"
        IEEEName = "exchange"
        sData = "LLLHHLHLLLHLHLHH, LLHLHLHHLLHLHLHH, LLHHHHLLLLHHHHLL, LHLLHHLHLHLLHHLH, LHLHHHHLLHLHHHHL, LHHLHHHHLHHLHHHH, LHHHLLLLLHHHLLLL, HLLLLLLHLLLLLLHH, HLLHLLHLHLLHLLHL, HLHLLLHHHLHLLLHH"
        sCommand = "LHLHLHLLLLHLHLHL"
        sReceiveAddress = "10"
        sProcess = "Wait"
        sExnm = "2"
        sAttribute = "data"
        LoopStepNo = 1

T1553Step1:
        sTNum = "BUS-01-001" ' 1553 BIT Test
        Echo(sTNum & " 1553 Built-In Test")
        Application.DoEvents()
        lStatus = BitTest1553() '  IssueIST 3
        If lStatus <> 0 Then
            Echo(FormatResults(False, "1553 Built-In Test"))
            Echo("Error: " & CStr(lStatus))
            Test1553 = FAILED
            RegisterFailure(CStr(MIL_STD_1553), sTNum, , , , , "1553 BIT TEST FAILED")
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            Echo(FormatResults(True, "1553 Built-In Test")) '1,2,3
            Call IncStepPassed()
        End If
        sRole = "Master"
        lStatus = fnXml1553("Reset")
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "1553" And OptionStep = LoopStepNo Then
            GoTo T1553Step1
        End If
        frmSTest.proProgress.Value = 50
        LoopStepNo += 1


T1553Step2:

        sTNum = "BUS-01-002" ' 1553 Wrap Test
        Echo(sTNum & " 1553 Wrap Test")
        Application.DoEvents()

        ''debug test 15553
        lStatus = WrapTest1553()

        If Not lStatus Then
            Echo(FormatResults(False, "1553 Wrap Test"))
            Test1553 = FAILED
            Call IncStepFailed()
            RegisterFailure(CStr(MIL_STD_1553), sTNum, , , , , "1553 Wrap FAILED")
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            Echo(FormatResults(True, "1553 Wrap Test"))
            Call IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "1553" And OptionStep = LoopStepNo Then
            GoTo T1553Step2
        End If
        
        frmSTest.proProgress.Value = 100


TestComplete:
        Application.DoEvents()
        frmSTest.cmdAbort.Text = "Abort Test"
        frmSTest.cmdPause.Text = "Pause Test"
        If AbortTest = True Then
            If Test1553 = FAILED Then
                ReportFailure(MIL_STD_1553)
            Else
                ReportUnknown(MIL_STD_1553)
                Test1553 = -99
            End If
            sMsg = vbCrLf
            sMsg &= "      **********************************************" & vbCrLf
            sMsg &= "      *             1553 test aborted!             *" & vbCrLf
            sMsg &= "      **********************************************" & vbCrLf
        ElseIf Test1553 = PASSED Then
            ReportPass(MIL_STD_1553)
        ElseIf Test1553 = FAILED Then
            ReportFailure(MIL_STD_1553)
        Else
            ReportUnknown(MIL_STD_1553)
        End If
        If CloseProgram = True Then
            EndProgram()
        End If

    End Function



    Function fn1553Status() As Integer
        ' ErrStatus = fn1553Status("Ch1")' "Ch1", "Ch2"

        Dim status As Integer
        Dim Response As String = Space(4096)
        Dim s As String

        Response = Space(4096)
        s = "<AtXmlSignalDescription xmlns:atXml=""ATXML_TSF"">" & vbCrLf
        s = s & "<SignalAction>Status</SignalAction>" & vbCrLf
        s = s & "<SignalResourceName>MIL1553_1</SignalResourceName>" & vbCrLf
        s = s & "<SignalSnippit>" & vbCrLf
        s = s & "<Signal />" & vbCrLf
        s = s & "</SignalSnippit>" & vbCrLf
        s = s & "</AtXmlSignalDescription>"
        sXml = s
        status = atxml_IssueSignal(sXml, Response, MAX_XML_SIZE)
        If Left(Response, 1) = "<" Then
            Echo("Status Response=" & vbCrLf & Response)
        End If
        fn1553Status = status

        If status <> 0 Then
            Echo("1553 BUS Error: atxml_IssueSignal failed.")
            Echo("XML = " & sXml)
        End If

    End Function

    Function fn1553(sCommand As String) As Integer
        ' ErrStatus = fn1553Status("Ch1")' "Ch1", "Ch2"

        Dim status As Integer
        Dim Response As String = Space(4096)
        Dim s As String

        Response = Space(4096)
        s = "<AtXmlSignalDescription xmlns:atXml=""ATXML_TSF"">" & vbCrLf
        s = s & "<SignalAction>" & sCommand & "</SignalAction>" & vbCrLf
        s = s & "<SignalResourceName>MIL1553_1</SignalResourceName>" & vbCrLf
        s = s & "<SignalSnippit>" & vbCrLf
        s = s & "</SignalSnippit>" & vbCrLf
        s = s & "<SignalTimeOut>10.000000</SignalTimeOut>" & vbCrLf
        s = s & "</AtXmlSignalDescription>"
        sXml = s
        status = atxml_IssueSignal(sXml, Response, MAX_XML_SIZE)
        If Left(Response, 1) = "<" Then
            Echo("Status Response=" & vbCrLf & Response)
        End If
        fn1553 = status

        If status <> 0 Then
            Echo("1553 BUS Error: atxml_IssueSignal failed.")
            Echo("XML = " & sXml)
        End If

    End Function


    Function fnXml1553(ByVal XMLcommand As String) As Integer
        ' ErrStatus = FnXML1553("Setup")
        ' ErrStatus = FnXML1553("Enable")
        ' ErrStatus = FnXML1553("Fetch")
        ' ErrStatus = FnXML1553("Reset")

        Dim status As Integer
        Dim Response As String = Space(4096)
        Dim s As String

        Select Case LCase(XMLcommand)
            Case "setup" : XMLcommand = "Setup"
            Case "enable" : XMLcommand = "Enable"
            Case "fetch" : XMLcommand = "Fetch"
            Case "reset" : XMLcommand = "Reset"
            Case Else
                Echo("Illegal Command: " & XMLcommand)
                fnXml1553 = -1
                Exit Function
        End Select

        Select Case LCase(sRole)
            Case "master" : sRole = "Master"
            Case "slave" : sRole = "Slave"
            Case Else
                Echo("Illegal Master/Slave: " & sRole)
                sRole = "Master"
        End Select

        fnXml1553 = 0
        Response = Space(4096)
        s = "<AtXmlSignalDescription xmlns:atXml=""ATXML_TSF"">" & vbCrLf
        s = s & "<SignalAction>" & XMLcommand & "</SignalAction>" & vbCrLf
        s = s & "<SignalResourceName>MIL1553_1</SignalResourceName>" & vbCrLf
        s = s & "<SignalSnippit>" & vbCrLf
        s = s & "<Signal Name=""1553_SIGNAL"" Out=""exchange"">" & vbCrLf
        s = s & "<IEEE_1553B name=""" & IEEEName & """" ' "exchange"
        s = s & " respTime=""" & sRespTime & """" ' "0.00022s"
        s = s & " messageGap=""" & sMessageGap & """" ' "0.000100s"
        s = s & " Role=""" & sRole & """" ' "Slave" or "Master"
        s = s & " Mode=""" & sMode & """" ' "Con-RT"
        If sRole = "Slave" Then
            s = s & " status=""" & sStatus & """" ' "LHLHLLLLLLLLLLLL"
        Else
            s = s & " data=""" & sData & """" ' "LLLHHLHLLLHLHLHH, LLHLHLHHLLHLHLHH, LLHHHHLLLLHHHHLL, LHLLHHLHLHLLHHLH, LHLHHHHLLHLHHHHL, LHHLHHHHLHHLHHHH, LHHHLLLLLHHHLLLL, HLLLLLLHLLLLLLH, HLLHLLHLHLLHLLHL, HLHLLLHHHLHLLLHH"
            s = s & " command=""" & sCommand & """" ' "LHLHLHLLLLHLHLHL"
        End If
        s = s & " recieveAddress=""" & sReceiveAddress & """" ' "10, 1"
        s = s & " process=""" & sProcess & """" ' "Wait"
        s = s & " Exnm=""" & sExnm & """" ' "5" or "2"
        If XMLcommand = "Fetch" Then
            s = s & " Attribute=""" & sAttribute & """" ' "data" or "config" or "status"
        End If
        s = s & " />" & vbCrLf
        s = s & "</Signal>" & vbCrLf
        s = s & "</SignalSnippit>" & vbCrLf
        s = s & "<SignalTimeOut>10.000000</SignalTimeOut>" & vbCrLf
        s = s & "</AtXmlSignalDescription>"
        sXml = s
        status = atxml_IssueSignal(sXml, Response, MAX_XML_SIZE)
        If XMLcommand = "Fetch" Then
            Read1553Data = Response
        End If
        If Left(Response, 1) = "<" Then
            Echo(XMLcommand & " " & sRole & " " & sAttribute)
            Echo("Response=" & vbCrLf & Response)
        End If
        fnXml1553 = status

    End Function


    Public Function BitTest1553() As Integer
        Dim Response As String = Space(255)
        Dim sXML As String
        Dim nSize As Integer = 255
        Dim status As Integer

        'Reset Instrument and GUI
        sRole = "Slave"
        status = fnXml1553("Reset")
        sRole = "Master"
        status = fnXml1553("Reset")
        BitTest1553 = status
        If status <> 0 Then
            Echo("1553 Error: atxml_IssueSignal failed.")
            ' MsgBox("1553 Error: atxml_IssueSignal failed." & vbCrLf & "XML = " & sXML, MsgBoxStyle.Exclamation, "1553 Error Message")
            Exit Function
        End If
        Delay(1) 'Allow time for a full reset

        If LiveMode(MIL_STD_1553) = CInt(True) Then 'If Communication has been verified
            sXML = "<AtXmlIssueIst>"
            sXML = sXML & "<SignalAction>IssueIST</SignalAction>"
            sXML = sXML & "<SignalResourceName>MIL1553_1</SignalResourceName>"
            sXML = sXML & "<Level>3</Level>"
            sXML = sXML & "</AtXmlIssueIst>"
            status = atxml_IssueIst(sXML, Response, nSize)
            BitTest1553 = status
            If (status <> 0) Or (InStr(Response, "PnP Error") > 0) Then
                Echo("1553 Error: atxml_IssueIST failed." & vbCrLf & "1553 BIT Test Response:" & vbCrLf & Left(Response, 100))
                ' MsgBox("1553 Error: atxml_IssueIST failed." & vbCrLf & "XML = " & sXML, MsgBoxStyle.Exclamation, "1553 Error")
                ' MsgBox("1553 BIT Test Response:" & vbCrLf & Response)
                BitTest1553 = -99
            End If
        Else            'Communication error condition
            Echo("1553 Instrument Is Not Responding")
            BitTest1553 = -99
        End If

    End Function

    Function WrapTest1553() As Boolean

        Dim status As Integer
        Dim hCard As IntPtr
        Dim hCore As IntPtr
        Dim BCMsg As ULong
        Dim RTMsg As ULong
        Dim BCData() As UShort = {0, 0, 0}
        Dim RTData() As UShort = {0, 0, 0}
        Dim cardnum = 0
        Dim channum = 0
        Dim corenum = 0
        Dim MsgDex As Integer = 0
        Dim MsgErrors As ULong

        WrapTest1553 = False

        'Get Handle to 1553 Card number 0
        status = BTICard_CardOpen(hCard, cardnum)
        If (status) Then
            GoTo exittest
        End If

        'Get Handle to 1553 Core number 0
        status = BTICard_CoreOpen(hCore, corenum, hCard)
        If (status) Then
            GoTo ExitTest
        End If

        'Reset Core
        BTICard_CardReset(hCore)
        If (status) Then
            GoTo ExitTest
        End If

        'Configure the core
        status = BTI1553_BCConfig(BCCFG1553_DEFAULT, channum, hCore)
        If (status) Then
            GoTo ExitTest
        End If

        'Creat the message
        BCMsg = BTI1553_BCCreateMsg(MSGCRT1553_DEFAULT, &H843, 0, BCData, hCore)
        If (status) Then
            GoTo ExitTest
        End If

        BTI1553_BCSchedFrame(FRAMETIME, channum, hCore)
        MsgDex = BTI1553_BCSchedMsg(BCMsg, channum, hCore)
        'Single shot the message
        BTI1553_CmdShotWr(1, MsgDex, channum, hCore)

        'Configured to Channel B only Response
        status = BTI1553_RTConfig(RTCFG1553_CHANB, 1, channum, hCore)
        If (status) Then
            GoTo ExitTest
        End If

        RTMsg = BTI1553_RTGetMsg(SUBADDRESS, 1, RCV, 2, channum, hCore)

        'Configure the channel for monitor mode
        status = BTI1553_MonConfig(MONCFG1553_DEFAULT, channum, hCore)
        If (status) Then
            GoTo ExitTest
        End If

        'Monitor RT address 1
        status = BTI1553_MonFilterTA(&H2, channum, hCore)
        If (status) Then
            GoTo ExitTest
        End If

        'Configure the sequential record.
        status = BTICard_SeqConfig(SEQCFG_DEFAULT, hCore)
        If (status) Then
            GoTo ExitTest
        End If

        BTICard_CardStart(hCore)

        BTI1553_MsgDataWr(BCData, 3, BCMsg, hCore)

        'Read the data received by the RT
        BTI1553_MsgDataRd(RTData, 3, RTMsg, hCore)
        Delay(0.5)
        MsgErrors = BTI1553_MsgFieldRd(FIELD1553_ERROR, BCMsg, hCore)


        If (MsgErrors > 1) Then
            GoTo ExitTest
        End If
        WrapTest1553 = True
ExitTest:
        lStatus2 = BTICard_CardClose(hCard)

    End Function



End Module