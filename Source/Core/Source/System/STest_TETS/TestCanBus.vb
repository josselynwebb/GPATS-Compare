'Option Strict On
Option Explicit On

Imports System


Public Module modCANbus


    '**************************************************************
    '* Nomenclature   : ATS-ViperT SYSTEM SELF TEST               *
    '*                  CAN bus Functions                         *
    '* Version        : 2.0                                       *
    '* Last Update    : Apr 1, 2017                               *
    '* Purpose        : This module contains functions to access  *
    '*                : the CANbus via CICL. If a call fails, it  *
    '*                : will raise an exception.  Clients who use *
    '*                : this interface must establish an exception*
    '*                : handler in their routines in order to see *
    '*                : any errors that occur.                    *
    '**************************************************************

    ' Abbreviations
    ' CAN     - Controller Area Network

    Dim sXml As String

    'init setup variables
    Dim CanName As String = "exchange"
    Dim timingValue As String = "20000Hz"
    Dim threeSamples As String = "0"
    Dim singleFilter As String = "1"
    Dim acceptanceCode As String = "LLLLLLLLLLLLLLLLLLLHHLHLLLLHHLHL"
    Dim acceptanceMask As String = "HHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH"
    Dim m_talker As String = "0"
    Dim sChannel As String = "1"
    Dim data_bits As String = "FEDC,1,FE,DC,BA,98,76,54,32,10"
    Dim maxTime As String = "20s"
    Dim CanReadData As String = ""
    Dim CanWriteData As String = ""

    Dim rCanName As String = "exchange"
    Dim rTimingValue As String = "20000Hz"
    Dim rThreeSamples As String = "0"
    Dim rSingleFilter As String = "1"
    Dim rAcceptanceCode As String = "LLLLLLLLLLLLLLLLLLLHHLHLLLLHHLHL"
    Dim rAcceptanceMask As String = "HHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH"
    Dim rM_talker As String = "0"
    Dim rsChannel As String = "1"
    Dim rdata_bits As String = "FE,DC,BA,98,76,54,32,10"
    Dim rMaxTime As String = "20s"

    Dim wdata As String = "FE,DC,BA,98,76,54,32,10"
    Dim Config As Boolean = False

    Public Function TestCanBus() As Short

        Dim S As String
        Dim status As Integer
        Dim ErrMessage As String = ""
        Dim rData As String = ""
        Dim LoopStepNo As Integer

        EchoTitle(InstrumentDescription(CANBUS) & " ", True)
        EchoTitle("Can Bus Test", False)

        TestCanBus = UNTESTED
        If AbortTest = True Then GoTo TestComplete
        AbortTest = False
        TestCanBus = PASSED
        frmSTest.proProgress.Maximum = 100
        frmSTest.proProgress.Value = 10


        If Not RunningEndToEnd And FirstPass = True Then
            S = "Install cable W209 (93006H4241) as follows:" & vbCrLf
            S &= "1. Connect cable W209-P1 onto the CIC J21 (CAN1) connector. " & vbCrLf
            S &= "2. Connect cable W209-P2 onto the CIC J22 (CAN2) connector. " & vbCrLf
            DisplaySetup(S, "ST-W209-1.jpg", 2)
            Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete
        End If

        'init expected read status
        frmSTest.proProgress.Value = 10
        rCanName = "exchange"
        rTimingValue = "20000Hz"
        rThreeSamples = "0"
        rSingleFilter = "1"
        rAcceptanceCode = "LLLLLLLLLLLLLLLLLLLHHLHLLLLHHLHL"
        rAcceptanceMask = "HHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH"
        rM_talker = "0"
        rsChannel = "1"
        rdata_bits = "FEDC,1,FE,DC,BA,98,76,54,32,10"
        rMaxTime = "5s"
        LoopStepNo = 1

CAN1:   sTNum = "CAN-01-001" ' CAN BIT Test
        Echo(sTNum & " CAN Built-In Test")
        Application.DoEvents()
        status = CanBitTest() '  IssueIST 3
        If status <> 0 Then
            Echo(FormatResults(False, "CAN Built-In Test"))
            Echo("Error: " & CStr(status))
            status = FAILED
            Call IncStepFailed()
            RegisterFailure(CStr(CANBUS), sTNum, , , , , "CAN BIT TEST FAILED")
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            Echo(FormatResults(True, "CAN Built-In Test")) '1,2,3
            Call IncStepPassed()
        End If
        'CJW status = fnXMLCan("Reset", "Ch1")  ' reset Ch1
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "CANBUS" And OptionStep = LoopStepNo Then
            GoTo CAN1
        End If
        LoopStepNo += 1
        frmSTest.proProgress.Value = 30

CAN2:   sTNum = "CAN-01-002" 'Test CAN CH1 setup
        CanName = "exchange"
        timingValue = "20000Hz"
        threeSamples = "0"
        singleFilter = "1"
        acceptanceCode = "LLLLLLLLLLLLLLLLLLLHHLHLLLLHHLHL"
        acceptanceMask = "HHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH"
        m_talker = "0"
        sChannel = "1"
        data_bits = "FEDC,1,FE,DC,BA,98,76,54,32,10"
        maxTime = "20s"

        status = fnCanStatus()
        status = fnXMLCan("Setup", "Ch1")  ' setup Ch1

        Config = True
        status = fnXMLCan("Fetch", "Ch1")  ' gets configuration data
        '   MsgBox(CanReadData)

        If status <> 0 Then
            TestCanBus = FAILED
            Echo(FormatResults(False, "CAN CH1 Setup Error " & ErrMessage, sTNum))
            RegisterFailure(CStr(CANBUS), sTNum, , , , , "CAN CH1 SETUP TEST FAILED")
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete

        Else
            rTimingValue = fnParseConfig(CanReadData, "timingValue") & "Hz"
            rThreeSamples = fnParseConfig(CanReadData, "threeSamples")
            rsChannel = fnParseConfig(CanReadData, "m_Channel")
            rSingleFilter = fnParseConfig(CanReadData, "singleFilter")
            rAcceptanceCode = fnParseConfig(CanReadData, "acceptanceCode")
            rAcceptanceMask = fnParseConfig(CanReadData, "acceptanceMask")
            rMaxTime = fnParseConfig(CanReadData, "maxTime")
            If IsNumeric(rMaxTime) Then
                rMaxTime = CStr(CSng(rMaxTime)) & "s"
            End If

            If (timingValue = rTimingValue) And (threeSamples = rThreeSamples) And _
                (singleFilter = rSingleFilter) And (acceptanceCode = rAcceptanceCode) And _
                (acceptanceMask = rAcceptanceMask) And (sChannel = rsChannel) And _
                (maxTime = rMaxTime) Then
                Echo(FormatResults(True, "CAN Ch1 Setup Test", sTNum))
                IncStepPassed()
            Else
                TestCanBus = FAILED
                Echo(FormatResults(False, "CAN Ch1 Setup Test", sTNum))
                If timingValue <> rTimingValue Then
                    Echo(" timingValue" & vbTab & "Wr " & timingValue & vbTab & "Rd " & rTimingValue)
                End If
                If (rThreeSamples <> "") And (IsNumeric(threeSamples) = True) Then
                    If CInt(threeSamples) <> CInt(rThreeSamples) Then
                        Echo(" threeSamples" & vbTab & "Wr " & threeSamples & vbTab & "Rd " & rThreeSamples)
                    End If
                Else
                    Echo(" threeSamples" & vbTab & "Wr " & threeSamples & vbTab & "Rd " & rThreeSamples)
                End If
                If (sChannel <> "") And (IsNumeric(rsChannel) = True) Then
                    If CInt(sChannel) <> CInt(rsChannel) Then
                        Echo(" m_Channel" & vbTab & "Wr " & sChannel & vbTab & "Rd " & rsChannel)
                    End If
                Else
                    Echo(" m_Channel" & vbTab & "Wr " & sChannel & vbTab & "Rd " & rsChannel)
                End If
                If (rSingleFilter <> "") And (IsNumeric(rSingleFilter) = True) Then
                    If CInt(singleFilter) <> CInt(rSingleFilter) Then
                        Echo(" singleFilter" & vbTab & "Wr " & singleFilter & vbTab & "Rd " & rSingleFilter)
                    End If
                Else
                    Echo(" singleFilter" & vbTab & "Wr " & singleFilter & vbTab & "Rd " & rSingleFilter)
                End If
                If acceptanceCode <> rAcceptanceCode Then
                    Echo(" acceptanceCode" & vbTab & "W" & acceptanceCode)
                    Echo("               " & vbTab & "R" & rAcceptanceCode)
                End If
                If acceptanceMask <> rAcceptanceMask Then
                    Echo(" acceptanceMask" & vbTab & "W" & acceptanceMask)
                    Echo("               " & vbTab & "R" & rAcceptanceMask)
                End If
                If maxTime <> rMaxTime Then
                    Echo(" maxTime" & vbTab & "W" & CStr(maxTime) & vbTab & "R" & rMaxTime)
                End If
                RegisterFailure(CStr(CANBUS), sTNum, , , , , "CAN CH1 SETUP TEST1 FAILED")
                IncStepFailed()
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            End If
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "CANBUS" And OptionStep = LoopStepNo Then
            GoTo CAN2
        End If
        LoopStepNo += 1
        frmSTest.proProgress.Value = 40


CAN3:   sTNum = "CAN-01-003" 'Test CAN CH2 setup
        CanName = "exchange"
        timingValue = "20000Hz"
        threeSamples = "0"
        singleFilter = "1"
        acceptanceCode = "LLLLLLLLLLLLLLLLLLLHHLHLLLLHHLHL"
        acceptanceMask = "HHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH"
        m_talker = "0"
        sChannel = "2"
        data_bits = "FEDC,1,FE,DC,BA,98,76,54,32,10"
        maxTime = "5s"

        status = fnCanStatus()
        status = fnXMLCan("Setup", "Ch2")  ' setup Ch2

        Config = True
        status = fnXMLCan("Fetch", "Ch2")  ' gets configuration data
        '   MsgBox(CanReadData)

        If status <> 0 Then
            TestCanBus = FAILED
            Echo(FormatResults(False, "CAN CH2 Setup Error " & ErrMessage, sTNum))
            RegisterFailure(CStr(CANBUS), sTNum, , , , , "CAN CH2 SETUP TEST FAILED")
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete

        Else
            rTimingValue = fnParseConfig(CanReadData, "timingValue") & "Hz"
            rThreeSamples = fnParseConfig(CanReadData, "threeSamples")
            rsChannel = fnParseConfig(CanReadData, "m_Channel")
            rSingleFilter = fnParseConfig(CanReadData, "singleFilter")
            rAcceptanceCode = fnParseConfig(CanReadData, "acceptanceCode")
            rAcceptanceMask = fnParseConfig(CanReadData, "acceptanceMask")
            rMaxTime = fnParseConfig(CanReadData, "maxTime")
            If IsNumeric(rMaxTime) Then
                rMaxTime = CStr(CSng(rMaxTime)) & "s"
            End If

            If (timingValue = rTimingValue) And (threeSamples = rThreeSamples) And _
                (singleFilter = rSingleFilter) And (acceptanceCode = rAcceptanceCode) And _
                (acceptanceMask = rAcceptanceMask) And (sChannel = rsChannel) And _
                (maxTime = rMaxTime) Then
                Echo(FormatResults(True, "CAN Ch2 Setup Test", sTNum))
                IncStepPassed()
            Else
                TestCanBus = FAILED
                Echo(FormatResults(False, "CAN Ch2 Setup Test", sTNum))
                If timingValue <> rTimingValue Then
                    Echo(" timingValue" & vbTab & "W" & timingValue & vbTab & "R" & rTimingValue)
                End If
                If rThreeSamples <> "" And IsNumeric(rThreeSamples) = True Then
                    If CInt(threeSamples) <> CInt(rThreeSamples) Then
                        Echo(" threeSamples" & vbTab & "W" & threeSamples & vbTab & "R" & rThreeSamples)
                    End If
                Else
                    Echo(" threeSamples" & vbTab & "W" & threeSamples & vbTab & "R" & rThreeSamples)
                End If
                If rsChannel <> "" And IsNumeric(rsChannel) = True Then
                    If CInt(sChannel) <> CInt(rsChannel) Then
                        Echo(" m_Channel" & vbTab & "W" & sChannel & vbTab & "R" & rsChannel)
                    End If
                Else
                    Echo(" m_Channel" & vbTab & "W" & sChannel & vbTab & "R" & rsChannel)
                End If
                If rSingleFilter <> "" And IsNumeric(rSingleFilter) = True Then
                    If CInt(singleFilter) <> CInt(rSingleFilter) Then
                        Echo(" singleFilter" & vbTab & "W" & singleFilter & vbTab & "R" & rSingleFilter)
                    End If
                Else
                    Echo(" singleFilter" & vbTab & "W" & singleFilter & vbTab & "R" & rSingleFilter)
                End If

                If acceptanceCode <> rAcceptanceCode Then
                    Echo(" acceptanceCode" & vbTab & "W" & acceptanceCode)
                    Echo("               " & vbTab & "R" & rAcceptanceCode)
                End If
                If acceptanceMask <> rAcceptanceMask Then
                    Echo(" acceptanceMask" & vbTab & "W" & acceptanceMask)
                    Echo("               " & vbTab & "R" & rAcceptanceMask)
                End If
                If maxTime <> rMaxTime Then
                    Echo(" maxTime" & vbTab & "W" & CStr(maxTime) & vbTab & "R" & rMaxTime)
                End If
                RegisterFailure(CStr(CANBUS), sTNum, , , , , "CAN CH2 SETUP TEST FAILED")
                IncStepFailed()
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            End If
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "CANBUS" And OptionStep = LoopStepNo Then
            GoTo CAN3
        End If
        LoopStepNo += 1
        frmSTest.proProgress.Value = 50

CAN4:   sTNum = "CAN-01-004" 'Test CANLoopback CH1 to CH2 Data1
        'setup w/r data
        CanName = "exchange"
        timingValue = "20000Hz"
        threeSamples = "0"
        singleFilter = "1"
        acceptanceCode = "LLLLLLLLLLLLLLLLLLLHHLHLLLLHHLHL"
        acceptanceMask = "HHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH"
        m_talker = "0"
        sChannel = "1"
        maxTime = "5s"
        Config = False
        wdata = "FE,DC,BA,98,76,54,32,10"
        data_bits = "FEDC,1," & wdata

        'Channel 1 is set to xmit, Channel 2 to receive
        'Channel 1 will xmit bursts until there is an acknowledge  (5 seconds timeout)
        'If channel 2 responds with an acknowledge then there will be only 1 burst of xmit data

        status = fnXMLCan("Reset", "Ch2")  ' Flush Ch2 buffer
        status = fnCanStatus()
        status = fnXMLCan("Setup", "Ch1")  ' setup Ch1
        status = fnCanStatus()
        status = fnXMLCan("Setup", "Ch2")  ' setup Ch2
        status = fnCanStatus()
        m_talker = "1" ' ListenOnly
        status = fnXMLCan("Enable", "Ch2") ' Enable Ch2 to receive
        m_talker = "0" ' Not ListenOnly
        status = fnXMLCan("Enable", "Ch1") ' Enable Ch1 to transmit
        status = fnXMLCan("Fetch", "Ch2")  ' gets CanReadData
        rData = fnParseConfig(CanReadData, "data") ' "HHHLHHHH, HHLLHHLH, HLHLHLHH, HLLLHLLH, LHHLLHHH, LHLLLHLH, LLHLLLHH, LLLLLLLH"
        rData = fnHLtoHex(rData)                   ' "EF,CD,AB,89,67,45,23,01"
        status = fnXMLCan("Reset", "Ch2")  ' Reset

        If rData = wdata Then
            Echo(FormatResults(True, "CAN Loopback Ch1-Ch2 Data1", sTNum))
            Echo("  Wr: " & wdata)
            Echo("  Rd: " & rData)
            IncStepPassed()
        Else
            TestCanBus = FAILED
            Echo(FormatResults(False, "CAN Loopback Ch1-Ch2 Data1 ", sTNum))
            Echo("  Wr: " & wdata)
            Echo("  Rd: " & rData)
            RegisterFailure(CStr(CANBUS), sTNum, , , , , "CAN LOOPBACK CH1-CH2 Data1 FAILED")
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "CANBUS" And OptionStep = LoopStepNo Then
            GoTo CAN4
        End If
        LoopStepNo += 1
        frmSTest.proProgress.Value = 60

CAN5:   sTNum = "CAN-01-005" 'Test CANLoopback CH1 to CH2 Data2
        'setup w/r data
        CanName = "exchange"
        timingValue = "20000Hz"
        threeSamples = "0"
        singleFilter = "1"
        acceptanceCode = "LLLLLLLLLLLLLLLLLLLHHLHLLLLHHLHL"
        acceptanceMask = "HHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH"
        m_talker = "0"
        sChannel = "1"
        maxTime = "5s"
        Config = False
        wdata = "08,07,06,05,04,03,02,01"
        data_bits = "FEDC,1," & wdata

        'Channel 1 is set to xmit, Channel 2 to receive
        'Channel 1 will xmit bursts until there is an acknowledge from Channel 2  (5 seconds timeout)
        'If channel 2 responds with an acknowledge then there will be only 1 burst of xmit data

        status = fnXMLCan("Reset", "Ch2")  ' Flush Ch2 buffer
        status = fnCanStatus()
        status = fnXMLCan("Setup", "Ch1")  ' setup Ch1
        status = fnCanStatus()
        status = fnXMLCan("Setup", "Ch2")  ' setup Ch2
        status = fnCanStatus()
        m_talker = "1" ' ListenOnly
        status = fnXMLCan("Enable", "Ch2") ' Enable Ch2 to receive
        m_talker = "0" ' Not ListenOnly
        status = fnXMLCan("Enable", "Ch1") ' Enable Ch1 to transmit
        status = fnXMLCan("Fetch", "Ch2")  ' gets CanReadData
        rData = fnParseConfig(CanReadData, "data") ' "LLLLHLLL, LLLLLHHH, LLLLLHHL, LLLLLHLH, LLLLLHLL, LLLLLLHH, LLLLLLHL, LLLLLLLH"
        rData = fnHLtoHex(rData)                   ' "08,07,06,05,04,03,02,01""
        status = fnXMLCan("Reset", "Ch2")  ' Reset

        If rData = wdata Then
            Echo(FormatResults(True, "CAN Loopback Ch1-Ch2 Data2", sTNum))
            Echo("  Wr: " & wdata)
            Echo("  Rd: " & rData)
            IncStepPassed()
        Else
            TestCanBus = FAILED
            Echo(FormatResults(False, "CAN Loopback Ch1-Ch2 Data2", sTNum))
            Echo("  Wr: " & wdata)
            Echo("  Rd: " & rData)
            RegisterFailure(CStr(CANBUS), sTNum, , , , , "CAN LOOPBACK CH1-CH2 Data2" & " FAILED")
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "CANBUS" And OptionStep = LoopStepNo Then
            GoTo CAN5
        End If
        LoopStepNo += 1
        frmSTest.proProgress.Value = 70


CAN6:   sTNum = "CAN-01-006" 'Test CANLoopback CH2 to CH1 Data1
        'setup w/r data
        CanName = "exchange"
        timingValue = "20000Hz"
        threeSamples = "0"
        singleFilter = "1"
        acceptanceCode = "LLLLLLLLLLLLLLLLLLLHHLHLLLLHHLHL"
        acceptanceMask = "HHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH"
        m_talker = "0"
        sChannel = "1"
        maxTime = "5s"
        Config = False
        wdata = "EF,CD,AB,89,67,45,23,01"
        data_bits = "FEDC,1," & wdata

        'Channel 1 is set to xmit, Channel 2 to receive
        'Channel 2 will xmit bursts until there is an acknowledge from Channel 1 (5 seconds timeout)
        'If channel 1 responds with an acknowledge then there will be only 1 burst of xmit data

        status = fnXMLCan("Reset", "Ch1")  ' Flush Ch1 buffer
        status = fnCanStatus()
        status = fnXMLCan("Setup", "Ch1")  ' setup Ch1
        status = fnCanStatus()
        status = fnXMLCan("Setup", "Ch2")  ' setup Ch2
        status = fnCanStatus()
        m_talker = "1" ' ListenOnly
        status = fnXMLCan("Enable", "Ch1") ' Enable Ch1 to receive
        m_talker = "0" ' Not ListenOnly
        status = fnXMLCan("Enable", "Ch2") ' Enable Ch2 to transmit
        status = fnXMLCan("Fetch", "Ch1")  ' gets CanReadData
        rData = fnParseConfig(CanReadData, "data") ' "HHHLHHHH, HHLLHHLH, HLHLHLHH, HLLLHLLH, LHHLLHHH, LHLLLHLH, LLHLLLHH, LLLLLLLH"
        rData = fnHLtoHex(rData)                   ' "EF,CD,AB,89,67,45,23,01"
        status = fnXMLCan("Reset", "Ch1")  ' Reset

        If rData = wdata Then
            Echo(FormatResults(True, "CAN Loopback Ch2-Ch1 Data1", sTNum))
            Echo("  Wr: " & wdata)
            Echo("  Rd: " & rData)
            IncStepPassed()
        Else
            TestCanBus = FAILED
            Echo(FormatResults(False, "CAN Loopback Ch2 to Ch1 Data1", sTNum))
            Echo("  Wr: " & wdata)
            Echo("  Rd: " & rData)
            RegisterFailure(CStr(CANBUS), sTNum, , , , , "CAN LOOPBACK CH2-CH1 Data1 FAILED")
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "CANBUS" And OptionStep = LoopStepNo Then
            GoTo CAN6
        End If
        LoopStepNo += 1
        frmSTest.proProgress.Value = 80

CAN7:   sTNum = "CAN-01-007" 'Test CANLoopback CH2 to CH1 Data2
        'setup w/r data
        CanName = "exchange"
        timingValue = "20000Hz"
        threeSamples = "0"
        singleFilter = "1"
        acceptanceCode = "LLLLLLLLLLLLLLLLLLLHHLHLLLLHHLHL"
        acceptanceMask = "HHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH"
        m_talker = "0"
        sChannel = "1"
        maxTime = "5s"
        Config = False
        wdata = "01,02,03,04,05,06,07,08"
        data_bits = "FEDC,1," & wdata

        'Channel 1 is set to xmit, Channel 2 to receive
        'Channel 2 will xmit bursts until there is an acknowledge from Channel 1  (5 seconds timeout)
        'If channel 1 responds with an acknowledge then there will be only 1 burst of xmit data

        status = fnXMLCan("Reset", "Ch1")  ' Flush Ch1 buffer
        status = fnCanStatus()
        status = fnXMLCan("Setup", "Ch1")  ' setup Ch1
        status = fnCanStatus()
        status = fnXMLCan("Setup", "Ch2")  ' setup Ch2
        status = fnCanStatus()
        m_talker = "1" ' ListenOnly
        status = fnXMLCan("Enable", "Ch1") ' Enable Ch1 to receive
        m_talker = "0" ' Not ListenOnly
        status = fnXMLCan("Enable", "Ch2") ' Enable Ch2 to transmit
        status = fnXMLCan("Fetch", "Ch1")  ' gets CanReadData
        rData = fnParseConfig(CanReadData, "data") ' "LLLLLLLH, LLLLLLHL, LLLLLLHH, LLLLLHLL, LLLLLHLH, LLLLLHHL, LLLLLHHH, LLLLHLLL"
        rData = fnHLtoHex(rData)                   ' "01,02,03,04,05,06,07,08"
        status = fnXMLCan("Reset", "Ch1")  ' Reset

        If rData = wdata Then
            Echo(FormatResults(True, "CAN Loopback Ch2-Ch1 Data2", sTNum))
            Echo("  Wr: " & wdata)
            Echo("  Rd: " & rData)
            IncStepPassed()
        Else
            TestCanBus = FAILED
            Echo(FormatResults(False, "CAN Loopback Ch2 to Ch1 Data2", sTNum))
            Echo("  Wr: " & wdata)
            Echo("  Rd: " & rData)
            RegisterFailure(CStr(CANBUS), sTNum, , , , , "CAN LOOPBACK CH2-CH1 Data2 FAILED")
            IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = "CANBUS" And OptionStep = LoopStepNo Then
            GoTo CAN7
        End If
        frmSTest.proProgress.Value = 90

TestComplete:
        frmSTest.proProgress.Value = 100
        Application.DoEvents()
        frmSTest.cmdAbort.Text = "Abort Test"
        frmSTest.cmdPause.Text = "Pause Test"
        If AbortTest = True Then
            If TestCanBus = FAILED Then
                ReportFailure(CANBUS)
            Else
                ReportUnknown(CANBUS)
                TestCanBus = -99
            End If
            sMsg = vbCrLf
            sMsg &= "      **********************************************" & vbCrLf
            sMsg &= "      *           CAN bus tests aborted!           *" & vbCrLf
            sMsg &= "      **********************************************" & vbCrLf
            Echo(sMsg)
        ElseIf TestCanBus = PASSED Then
            ReportPass(CANBUS)
        ElseIf TestCanBus = FAILED Then
            ReportFailure(CANBUS)
        Else
            ReportUnknown(CANBUS)
        End If
        If CloseProgram = True Then
            EndProgram()
        End If
        Exit Function

    End Function

    ''Function BuildXML(sParam As String, sResource As String) As String
    ''    ' sXML = BuildXML("Identify", "SWITCH")
    ''    ' status = atxml_IssueSignal(sXML, Response, nSize)

    ''    Dim s As String
    ''    s = "<AtXmlSignalDescription xmlns:atXml=""ATXML_TSF"">" & vbCrLf
    ''    s = s & "<SignalAction>" & sParam & "</SignalAction>" & vbCrLf
    ''    s = s & "<SignalResourceName>" & sResource & "</SignalResourceName>" & vbCrLf
    ''    s = s & "</AtXmlSignalDescription>"
    ''    BuildXML = s

    ''End Function

    Function BuildCanXML(sParam As String) As String
        ' sXML = BuildCanXML("Identify")
        ' status = atxml_IssueSignal(sXML, Response, nSize)

        Dim s As String
        s = "<AtXmlSignalDescription xmlns:atXml=""ATXML_TSF"">" & vbCrLf
        s = s & "<SignalAction>" & sParam & "</SignalAction>" & vbCrLf
        s = s & "<SignalResourceName>CAN_1</SignalResourceName>" & vbCrLf
        s = s & "</AtXmlSignalDescription>"
        BuildCanXML = s

    End Function

    Public Function CanBitTest() As Integer
        Dim Response As String = Space(255)
        Dim sXML As String
        Dim nSize As Integer = 255
        Dim status As Integer

        'Reset Instrument and GUI
        status = fnXMLCan("Reset", "Ch1")  ' Reset Ch1
        status = fnXMLCan("Reset", "Ch2")  ' Reset Ch2
        CanBitTest = status
        If status <> 0 Then
            Echo("CAN Error: atxml_IssueSignal failed.")
            ' MsgBox("CAN Error: atxml_IssueSignal failed." & vbCrLf & "XML = " & sXML, MsgBoxStyle.Exclamation, "CAN Error Message")
            Exit Function
        End If
        Delay(1) 'Allow time for a full reset

        If LiveMode(CANBUS) = CInt(True) Then 'If Communication has been verified
            sXML = "<AtXmlIssueIst>"
            sXML = sXML & "<SignalAction>IssueIST</SignalAction>"
            sXML = sXML & "<SignalResourceName>CAN_1</SignalResourceName>"
            sXML = sXML & "<Level>3</Level>"
            sXML = sXML & "</AtXmlIssueIst>"
            status = atxml_IssueIst(sXML, Response, nSize)
            CanBitTest = status
            If status <> 0 Then
                Echo("CAN Error: atxml_IssueIST failed." & vbCrLf & "CAN BIT Test Response:" & vbCrLf & Response)
                ' MsgBox("CAN Error: atxml_IssueIST failed." & vbCrLf & "XML = " & sXML, MsgBoxStyle.Exclamation, "CAN Error")
                ' MsgBox("CAN BIT Test Response:" & vbCrLf & Response)
                CanBitTest = -99
            End If
        Else            'Communication error condition
            Echo("CAN Instrument Is Not Responding")
            CanBitTest = -99
        End If

    End Function


    Function fnCanStatus() As Integer
        ' ErrStatus = FnCanStatus("Ch1")' "Ch1", "Ch2"

        Dim status As Integer
        Dim Response As String = Space(4096)
        Dim s As String

        Response = Space(4096)
        s = "<AtXmlSignalDescription xmlns:atXml=""ATXML_TSF"">" & vbCrLf
        s = s & "<SignalAction>Status</SignalAction>" & vbCrLf
        s = s & "<SignalResourceName>CAN_1</SignalResourceName>" & vbCrLf
        s = s & "<SignalSnippit>" & vbCrLf
        s = s & "<Signal Name="""" Out=""Meas"" In=""Cha Ext Chb"">" & vbCrLf
        s = s & "<Instantaneous name=""Meas"" In=""Cha"" attribute=""ac_ampl""/>" & vbCrLf
        s = s & "</Signal>" & vbCrLf
        s = s & "</SignalSnippit>" & vbCrLf
        s = s & "</AtXmlSignalDescription>"
        sXml = s
        status = atxml_IssueSignal(sXml, Response, MAX_XML_SIZE)
        fnCanStatus = status

        If status <> 0 Then
            Echo("CAN_BUS Error: atxml_IssueSignal failed.")
        End If

    End Function


    Function fnXMLCan(ByVal Command As String, ByVal Channel As String) As Integer
        ' ErrStatus = FnXMLCan("Setup", "Ch1")
        ' ErrStatus = FnXMLCan("Enable", "Ch1")
        ' ErrStatus = FnXMLCan("Fetch", "Ch1")
        ' ErrStatus = FnXMLCan("Reset", "Ch1")

        Dim status As Integer
        Dim Response As String = Space(4096)
        Dim s As String
        Select Case LCase(Command)
            Case "setup" : Command = "Setup"
            Case "enable" : Command = "Enable"
            Case "fetch" : Command = "Fetch"
            Case "reset" : Command = "Reset"
            Case Else
                MsgBox("Illegal Command: " & Command)
                fnXMLCan = -1
                Exit Function
        End Select

        Select Case LCase(Channel)
            Case "ch1", "1" : sChannel = "1"
            Case "ch2", "2" : sChannel = "2"
            Case Else
                Echo("Illegal Channel: " & Channel)
                sChannel = "1"
        End Select

        fnXMLCan = 0
        Response = Space(4096)
        s = "<AtXmlSignalDescription xmlns:atXml=""ATXML_TSF"">" & vbCrLf
        s = s & "<SignalAction>" & Command & "</SignalAction>" & vbCrLf
        s = s & "<SignalResourceName>CAN_1</SignalResourceName>" & vbCrLf
        s = s & "<SignalSnippit>" & vbCrLf
        s = s & "<Signal Name=""BUS_SIGNAL"" Out=""exchange"">" & vbCrLf

        s = s & "<CAN name=""" & CanName & """"
        s = s & " timingValue=""" & timingValue & """"
        s = s & " threeSamples=""" & threeSamples & """"
        s = s & " singleFilter=""1"""
        s = s & " acceptanceCode=""" & acceptanceCode & """" ' LLLLLLLLLLLLLLLLLLLHHLHLLLLHHLHL
        s = s & " acceptanceMask=""" & acceptanceMask & """" ' HHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH
        s = s & " listenOnly=""" & m_talker & """"
        s = s & " channel=""" & sChannel & """"
        s = s & " data_bits=""" & data_bits & """"           ' FEDC,1,FE,DC,BA,98,76,54,32,10
        s = s & " maxTime=""" & maxTime & """"
        If Command = "Fetch" Then
            If Config = False Then
                s = s & " attribute=""data"""
                'example data response
                ''<AtXmlResponse>
                ''  	<ReturnData>
                ''		<ValuePair>
                ''			<Attribute>data</Attribute>
                ''			<Value>
                ''				<c:Datum xsi:type="c:string" unit=""><c:Value>HHHHHHHL, HHLHHHLL, HLHHHLHL, HLLHHLLL, LHHHLHHL, LHLHLHLL, LLHHLLHL, LLLHLLLL</c:Value></c:Datum>
                ''			</Value>
                ''		</ValuePair>
                ''	</ReturnData>
                ''</AtXmlResponse>
            Else
                s = s & " attribute=""config"""
                'see fnParseConfig for example
            End If
        End If
        s = s & " />" & vbCrLf
        s = s & "</Signal>" & vbCrLf
        s = s & "</SignalSnippit>" & vbCrLf
        s = s & "</AtXmlSignalDescription>"
        sXml = s
        status = atxml_IssueSignal(sXml, Response, MAX_XML_SIZE)
        If Command = "Fetch" Then
            CanReadData = Response
        End If
        fnXMLCan = status

    End Function

    Function fnParseConfig(ByVal s As String, ByRef pData As String) As String
        Dim x1 As Integer
        Dim x2 As Integer
        Dim x3 As Integer

        ''s = "<AtXmlResponse>"
        ''s = s & "  	<ReturnData>"
        ''s = s & "		<ValuePair>"
        ''s = s & "			<Attribute>timingValue</Attribute>"
        ''s = s & "			<Value>"
        ''s = s & "				<c:Datum xsi:type=""c:integer"" unit="""" value=""20000""/>"
        ''s = s & "			</Value>"
        ''s = s & "		</ValuePair>"
        ''s = s & "	</ReturnData>"
        ''s = s & "</AtXmlResponse>"

        ''s = s & "<AtXmlResponse>"
        ''s = s & "  	<ReturnData>"
        ''s = s & "		<ValuePair>"
        ''s = s & "			<Attribute>threeSamples</Attribute>"
        ''s = s & "			<Value>"
        ''s = s & "				<c:Datum xsi:type=""c:integer"" unit="""" value=""0""/>"
        ''s = s & "			</Value>"
        ''s = s & "		</ValuePair>"
        ''s = s & "	</ReturnData>"
        ''s = s & "</AtXmlResponse>"

        ''s = s & "<AtXmlResponse>"
        ''s = s & "  	<ReturnData>"
        ''s = s & "		<ValuePair>"
        ''s = s & "			<Attribute>m_Channel</Attribute>"
        ''s = s & "			<Value>"
        ''s = s & "				<c:Datum xsi:type=""c:integer"" unit="""" value=""1""/>"
        ''s = s & "			</Value>"
        ''s = s & "		</ValuePair>"
        ''s = s & "	</ReturnData>"
        ''s = s & "</AtXmlResponse>"

        ''s = s & "<AtXmlResponse>"
        ''s = s & "  	<ReturnData>"
        ''s = s & "		<ValuePair>"
        ''s = s & "			<Attribute>singleFilter</Attribute>"
        ''s = s & "			<Value>"
        ''s = s & "				<c:Datum xsi:type=""c:integer"" unit="""" value=""1""/>"
        ''s = s & "			</Value>"
        ''s = s & "		</ValuePair>"
        ''s = s & "	</ReturnData>"
        ''s = s & "</AtXmlResponse>"

        ''s = s & "<AtXmlResponse>"
        ''s = s & "  	<ReturnData>"
        ''s = s & "		<ValuePair>"
        ''s = s & "			<Attribute>acceptanceCode</Attribute>"
        ''s = s & "			<Value>"
        ''s = s & "				<c:Datum xsi:type=""c:string"" unit=""""><c:Value>LLLLLLLLLLLLLLLLLLLHHLHLLLLHHLHL</c:Value></c:Datum>"
        ''s = s & "			</Value>"
        ''s = s & "		</ValuePair>"
        ''s = s & "	</ReturnData>"
        ''s = s & "</AtXmlResponse>"

        ''s = s & "<AtXmlResponse>"
        ''s = s & "  	<ReturnData>"
        ''s = s & "		<ValuePair>"
        ''s = s & "			<Attribute>acceptanceMask</Attribute>"
        ''s = s & "			<Value>"
        ''s = s & "				<c:Datum xsi:type=""c:string"" unit=""""><c:Value>HHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH</c:Value></c:Datum>"
        ''s = s & "			</Value>"
        ''s = s & "		</ValuePair>"
        ''s = s & "	</ReturnData>"
        ''s = s & "</AtXmlResponse>"

        ''s = s & "<AtXmlResponse>"
        ''s = s & "  	<ReturnData>"
        ''s = s & "		<ValuePair>"
        ''s = s & "			<Attribute>maxTime</Attribute>"
        ''s = s & "			<Value>"
        ''s = s & "				<c:Datum xsi:type=""c:double"" unit="""" value=""2.000000000000E+001""/>"
        ''s = s & "			</Value>"
        ''s = s & "		</ValuePair>"
        ''s = s & "	</ReturnData>"
        ''s = s & "</AtXmlResponse>"

        ''s = s & "<AtXmlResponse>"
        ''s = s & "  	<ReturnData>"
        ''s = s & "		<ValuePair>"
        ''s = s & "			<Attribute>spec</Attribute>"
        ''s = s & "			<Value>"
        ''s = s & "				<c:Datum xsi:type=""c:string"" unit=""""><c:Value>CAN</c:Value></c:Datum>"
        ''s = s & "			</Value>"
        ''s = s & "		</ValuePair>"
        ''s = s & "	</ReturnData>"
        ''s = s & "</AtXmlResponse>"



        x1 = InStr(1, s, "<Attribute>" & pData & "</Attribute>", CompareMethod.Text)
        If x1 = 0 Then
            fnParseConfig = ""
            Exit Function
        End If
        x2 = InStr(x1, s, "</AtXmlResponse>", CompareMethod.Text)
        s = Mid(s, x1, x2 - x1)

        x1 = 1
        If InStr(s, "type=""c:string") > 0 Then ' string
            x2 = InStr(x1, s, "c:Value>", CompareMethod.Text)
            If x2 = 0 Then
                fnParseConfig = ""
                Exit Function
            End If
            x2 = x2 + 8
            x3 = InStr(x2, s, "</", CompareMethod.Text)
            If x3 = 0 Then
                fnParseConfig = ""
                Exit Function
            End If
            fnParseConfig = Mid(s, x2, x3 - x2)
        Else
            x2 = InStr(x1, s, "value=", CompareMethod.Text)
            If x2 = 0 Then
                fnParseConfig = ""
                Exit Function
            End If
            x2 = x2 + 7
            x3 = InStr(x2, s, "/>", CompareMethod.Text)
            If x3 = 0 Then
                fnParseConfig = ""
                Exit Function
            End If
            fnParseConfig = Mid(s, x2, x3 - x2 - 1)
        End If


    End Function

    Function fnHLtoHex(ByVal s As String) As String
        's = "HHHHHHHL, HHLHHHLL, HLHHHLHL, HLLHHLLL, LHHHLHHL, LHLHLHLL, LLHHLLHL, LLLHLLLL"
        'fnHLtoHex = "FE,DC,BA,98,76,54,32,10"
        Dim i As Integer
        Dim j As Integer
        Dim x1 As Integer
        Dim s2 As String = ""
        Dim s3 As String = "00"

        fnHLtoHex = ""
        For i = 1 To 8 'process each byte
            s2 = s
            x1 = InStr(s, ",") 'find next comma, no comma after last byte
            If x1 > 0 Then
                s2 = Trim(Left(s, x1 - 1))
                s = Trim(Mid(s, x1 + 1))
            End If
            x1 = 0
            For j = 1 To 8 ' process each bit
                Select Case j
                    Case 1 : If Mid(s2, 1, 1) = "H" Then x1 += 128
                    Case 2 : If Mid(s2, 2, 1) = "H" Then x1 += 64
                    Case 3 : If Mid(s2, 3, 1) = "H" Then x1 += 32
                    Case 4 : If Mid(s2, 4, 1) = "H" Then x1 += 16
                    Case 5 : If Mid(s2, 5, 1) = "H" Then x1 += 8
                    Case 6 : If Mid(s2, 6, 1) = "H" Then x1 += 4
                    Case 7 : If Mid(s2, 7, 1) = "H" Then x1 += 2
                    Case 8 : If Mid(s2, 8, 1) = "H" Then x1 += 1
                End Select
            Next j
            If x1 > 15 Then
                fnHLtoHex = fnHLtoHex & Hex(x1)
            Else
                fnHLtoHex = fnHLtoHex & "0" & Hex(x1)
            End If
            If i < 8 Then
                fnHLtoHex &= "," 'add commas,  no comma after last byte
            End If
        Next i
    End Function

End Module
