'Option Strict On
Option Explicit On

Imports System.Net.NetworkInformation

Module modSerialBusses
    '**************************************************************
    '* Nomenclature   : ATS-ViperT SYSTEM SELF TEST               *
    '*                  Serial Busses Test                        *
    '* Version        : 2.0                                       *
    '* Last Update    : Apr 1, 2017                               *
    '* Purpose        : This module contains functions to access  *
    '*                : the Serial busses on the CIC.             *
    '*                                                            *
    '*    Com1 - RS422                                            *
    '*    Com2 = RS232                                            *
    '*    Serial Buss - RS485,RS232,RS422                         *
    '**************************************************************

    Public ApphandleBusses As Integer

    'local Module variables
    Dim Wparity As String = "ODD" 'parity = "ODD","EVEN","MARK","NONE"
    Dim Wbitrate As String = "9600Hz" 'bitrate = "9600Hz"
    Dim Wstopbits As String = "1bits" 'stopbits = "1bits"
    Dim Wwordlength As String = "8bits" 'wordlength = "8bits"
    Dim Wmaxt As String = "5s" 'maxt = "5s"
    Dim Wdelay As String
    Dim Wterm As String
    Dim Wspec As String
    Dim Wdata As String
    Dim WCOMSetting As String
    Dim RCOMSetting As String

    Dim Wmask As String
    Dim WlocalIP As String
    Dim Wgateway As String
    Dim WremoteIP As String
    Dim Wremport As String

    Dim Rparity As String = "ODD" 'parity = "ODD","EVEN","MARK","NONE"
    Dim Rbitrate As String = "9600Hz" 'bitrate = "9600Hz"
    Dim Rstopbits As String = "1bits" 'stopbits = "2bits"
    Dim Rwordlength As String = "8" 'wordlength = "8"
    Dim Rmaxt As String = "5s" 'maxt = "5s"
    Dim Rdelay As String
    Dim Rterm As String
    Dim Rspec As String
    Dim Rdata As String

    Dim Rmask As String
    Dim RlocalIP As String
    Dim Rgateway As String
    Dim RremoteIP As String
    Dim Rremport As String

    Dim rName(9) As String
    Dim pName(9) As String

    Const xRS422 As Short = 1
    Const xRS232 As Short = 2
    Const xSerRS485 As Short = 3
    Const xSerRS422 As Short = 4
    Const xSerRS232 As Short = 5
    Const xGBitEth1 As Short = 6
    Const xGBitEth2 As Short = 7
    Const xGBitEth3 As Short = 8
    Const xGBitEth4 As Short = 9

    Public Class MyUtilities
        Shared Sub RunCommandCom(command As String, arguments As String, permanent As Boolean)
            Dim p As Process = New Process()
            Dim pi As ProcessStartInfo = New ProcessStartInfo()
            pi.Arguments = " " & If(permanent = True, "/K", "/C") & " " & command & " " & arguments
            pi.FileName = "cmd.exe"

            p.StartInfo = pi
            p.StartInfo.CreateNoWindow = True ' not working??
            ''p.StartInfo.UseShellExecute = False
            ''p.StartInfo.RedirectStandardOutput = True

            p.Start()
        End Sub
    End Class

    Public Sub InitComVar()

        'resource names
        rName(1) = "COM_1"
        rName(2) = "COM_2"
        rName(3) = "COM_3"
        rName(4) = "COM_4"
        rName(5) = "COM_5"
        rName(6) = "ETHERNET_1" ' J15 Gigabit1 (Blackbody)
        rName(7) = "ETHERNET_2" ' J16 Gigabit2 (LAN)

        'protocol names
        pName(1) = "RS_422"
        pName(2) = "RS_232"
        pName(3) = "RS_485"
        pName(4) = "RS_422"
        pName(5) = "RS_232"
        pName(6) = "TCP" ' TCP or UDP
        pName(7) = "UDP" ' TCP or UDP
        pName(8) = "TCP" ' TCP or UDP

    End Sub

    Public Function TestCom1() As Short

        ' Test the RS422-9 pin via loop back plugs at J6 (RS422) port
        ' SP4 - DB9 loopback shorting plug
        Dim x As Integer
        Dim S As String
        Dim TestName As String = "RS422 COM1"

        Call InitComVar()

        'R422 COM Test Title block
        EchoTitle(InstrumentDescription(RS422) & " ", True)
        EchoTitle("COM Port Tests", False)
        frmSTest.proProgress.Maximum = 100
        frmSTest.proProgress.Value = 1

        TestCom1 = UNTESTED
        If AbortTest = True Then GoTo TestComplete
        AbortTest = False

        If FirstPass = True Then
            If RunningEndToEnd = False Then
                x = MsgBox("Is the SP4 adapter installed on the CIC J6 (RS422) connector?", MsgBoxStyle.YesNo)
                If x <> 6 Then
                    S = "Connect SP4(93006H6055) adapter to" & vbCrLf
                    S &= "    CIC J6 (RS422) connector." & vbCrLf
                    DisplaySetup(S, "ST-SP4-1.jpg", 1)
                End If
            ElseIf (ControlStartTest = RS422) Then
                'Install the SP2,SP3,SP4 adapters and the W206, W209 cables
                InstallSPCables()
            End If
            If AbortTest = True Then GoTo TestComplete
        End If
        System.Windows.Forms.Application.DoEvents()
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        TestCom1 = PASSED

        'RS422 Setup test 'from Witcher's Atlas example
COM1_1: sTNum = "COM-01-001"
        Wparity = "ODD"
        Wbitrate = "9600Hz"
        Wstopbits = "1bits" ' 1,1.5 or 2 bits
        Wwordlength = "8bits"
        Wmaxt = "5s"
        Wdata = "LLLHHLHL, LLHLHLHH, LLHHHHLL, LHLLHHLH, LHLHHHHL, "
        Wdata = Wdata & "LHHLHHHH, LHHHLLLL, HLLLLLLH, HLLHLLHL, HLHLLLHH, "
        Wdata = Wdata & "HLHLLLHH, HLHLLHLL, HLHLLHLH, HLHLLHHL, HLHLLHHH, "
        Wdata = Wdata & "HLHLHLLL, HLHLHLLH, HLHLHLHL, LHHHHLHH, HHHHHHHH"
        WCOMSetting = Wbitrate & "," & Wparity & "," & Wwordlength & "," & Wstopbits & "," & Wmaxt

        If FnTestSetup(xRS422) = PASSED Then
            Echo(FormatResults(True, "Com1 RS422 Setup, " & WCOMSetting, sTNum))
            Call IncStepPassed()
        Else
            Echo(FormatResults(False, "Com1 RS422 Setup, " & WCOMSetting, sTNum))
            sMsg = ""
            sMsg = "COM1/RS422 was not able to configure the port settings." & vbCrLf
            If Wparity <> Rparity Then sMsg = sMsg & "  Parity Write=" & Wparity & ",  Read=" & Rparity & vbCrLf
            If Wbitrate <> Rbitrate Then sMsg = sMsg & "  Bitrate Write=" & Wbitrate & ",  Read=" & Rbitrate & vbCrLf
            If Wstopbits <> Rstopbits Then sMsg = sMsg & "  StopBits Write=" & Wstopbits & ",  Read=" & Rstopbits & vbCrLf
            If Wwordlength <> Rwordlength Then sMsg = sMsg & "  WordLength Write=" & Wwordlength & ",  Read=" & Rwordlength & vbCrLf
            If Wmaxt <> Rmaxt Then sMsg = sMsg & "  MaxTimeOut Write=" & Wmaxt & ",  Read=" & Rmaxt & vbCrLf
            Echo(sMsg)
            RegisterFailure(CStr(RS422), sTNum, sComment:="COM1 RS422 SETUP TEST FAILED")
            Call IncStepFailed()
            TestCom1 = FAILED
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = 1 Then
            GoTo COM1_1
        End If
        frmSTest.proProgress.Value = 50

        ' RS422 Loopback
COM1_2: sTNum = "COM-01-002"
        Wparity = "ODD"
        Wbitrate = "9600Hz"
        Wstopbits = "1bits"
        Wwordlength = "8bits"
        Wmaxt = "5s"
        Wdata = "LLLHHLHL, LLHLHLHH, LLHHHHLL, LHLLHHLH, LHLHHHHL, "
        Wdata = Wdata & "LHHLHHHH, LHHHLLLL, HLLLLLLH, HLLHLLHL, HLHLLLHH, "
        Wdata = Wdata & "HLHLLLHH, HLHLLHLL, HLHLLHLH, HLHLLHHL, HLHLLHHH, "
        Wdata = Wdata & "HLHLHLLL, HLHLHLLH, HLHLHLHL, LHHHHLHH, HHHHHHHH"

        If FnTestLoopBack(xRS422) = PASSED Then
            Echo(FormatResults(True, "COM1 RS422 LoopBack", sTNum))
            Call IncStepPassed()
        Else
            Echo(FormatResults(False, "COM1 RS422 LoopBack", sTNum))
            sMsg = "Write Data=" & Wdata & vbCrLf
            sMsg = sMsg & " Read Data=" & Rdata
            Echo(sMsg)
            RegisterFailure(CStr(RS422), sTNum, sComment:="COM1 RS422 LOOPBACK TEST FAILED")
            TestCom1 = FAILED
            Call IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = 2 Then
            GoTo COM1_2
        End If
        frmSTest.proProgress.Value = 100

TestComplete:
        frmSTest.cmdAbort.Text = "Abort Test"
        frmSTest.cmdPause.Text = "Pause Test"
        If AbortTest = True Then
            If TestCom1 = FAILED Then
                ReportFailure(RS422)
            Else
                ReportUnknown(RS422)
                TestCom1 = -99
            End If
            sMsg = vbCrLf
            sMsg &= "      **********************************************" & vbCrLf
            sMsg &= "      *         COM1 RS422 tests aborted!          *" & vbCrLf
            sMsg &= "      **********************************************" & vbCrLf
            Echo(sMsg)
        ElseIf TestCom1 = PASSED Then
            ReportPass(RS422)
        ElseIf TestCom1 = FAILED Then
            ReportFailure(RS422)
        Else
            ReportUnknown(RS422)
        End If
        If CloseProgram = True Then
            EndProgram()
        End If

    End Function

    Public Function TestCom2() As Short

        ' Test the RS232-9pin via loop back plugs at J5 (RS232) port
        ' SP3 - DB9 loopback shorting plug
        Dim x As Integer
        Dim S As String
        Dim TestName As String = "RS232 COM2"

        Call InitComVar()


        'RS232 COM Test Title block
        EchoTitle(InstrumentDescription(RS232) & " ", True)
        EchoTitle("COM Port Tests", False)
        frmSTest.proProgress.Maximum = 100
        frmSTest.proProgress.Value = 1

        TestCom2 = UNTESTED
        If AbortTest = True Then GoTo TestComplete
        AbortTest = False

        If FirstPass = True Then
            If RunningEndToEnd = False Then
                x = MsgBox("Is the SP3 adapter installed on the CIC J5 (RS232) connector?", MsgBoxStyle.YesNo)
                If x <> 6 Then
                    S = "Connect SP3(93006H6054) adapter to the CIC J5 (RS232) connector." & vbCrLf
                    DisplaySetup(S, "ST-SP3-1.jpg", 1)
                End If
            ElseIf (ControlStartTest = RS232) Then
                'Install the SP2,SP3,SP4 adapters and the W206, W209 cables
                InstallSPCables()
            End If
            If AbortTest = True Then GoTo TestComplete
        End If
        System.Windows.Forms.Application.DoEvents()
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        TestCom2 = PASSED

        ' RS232 Setup test
COM2_1: sTNum = "COM-02-001"
        Wparity = "ODD"
        Wbitrate = "9600Hz"
        Wstopbits = "1.5bits"
        Wwordlength = "5bits"
        Wmaxt = "5s"
        Wdata = "LLLHHLHL, LLHLHLHH, LLHHHHLL, LHLLHHLH, LHLHHHHL, "
        Wdata = Wdata & "LHHLHHHH, LHHHLLLL, HLLLLLLH, HLLHLLHL, HLHLLLHH, "
        Wdata = Wdata & "HLHLLLHH, HLHLLHLL, HLHLLHLH, HLHLLHHL, HLHLLHHH, "
        Wdata = Wdata & "HLHLHLLL, HLHLHLLH, HLHLHLHL, LHHHHLHH, HHHHHHHH"
        WCOMSetting = Wbitrate & "," & Wparity & "," & Wwordlength & "," & Wstopbits & "," & Wmaxt

        If FnTestSetup(xRS232) = PASSED Then
            Echo(FormatResults(True, "Com2 RS232 Setup, " & WCOMSetting, sTNum))
            Call IncStepPassed()
        Else
            Echo(FormatResults(False, "Com2 RS232 Setup, " & WCOMSetting, sTNum))
            sMsg = ""
            sMsg = "COM2/RS232 was not able to configure the port settings." & vbCrLf
            If Wparity <> Rparity Then sMsg = sMsg & "  Parity Write=" & Wparity & ",  Read=" & Rparity & vbCrLf
            If Wbitrate <> Rbitrate Then sMsg = sMsg & "  Bitrate Write=" & Wbitrate & ",  Read=" & Rbitrate & vbCrLf
            If Wstopbits <> Rstopbits Then sMsg = sMsg & "  StopBits Write=" & Wstopbits & ",  Read=" & Rstopbits & vbCrLf
            If Wwordlength <> Rwordlength Then sMsg = sMsg & "  WordLength Write=" & Wwordlength & ",  Read=" & Rwordlength & vbCrLf
            If Wmaxt <> Rmaxt Then sMsg = sMsg & "  MaxTimeOut Write=" & Wmaxt & ",  Read=" & Rmaxt & vbCrLf
            Echo(sMsg)
            RegisterFailure(CStr(RS232), "COM-02-001", sComment:="COM2/RS232 SETUP TEST FAILED")
            TestCom2 = FAILED
            Call IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = 1 Then
            GoTo COM2_1
        End If
        frmSTest.proProgress.Value = 50

        ' RS232 Loopback
COM2_2: sTNum = "COM-02-002"
        Wparity = "ODD"
        Wbitrate = "9600Hz"
        Wstopbits = "1bits"
        Wwordlength = "8bits"
        Wmaxt = "5s"
        Wdata = "LLLHHLHL, LLHLHLHH, LLHHHHLL, LHLLHHLH, LHLHHHHL, "
        Wdata = Wdata & "LHHLHHHH, LHHHLLLL, HLLLLLLH, HLLHLLHL, HLHLLLHH, "
        Wdata = Wdata & "HLHLLLHH, HLHLLHLL, HLHLLHLH, HLHLLHHL, HLHLLHHH, "
        Wdata = Wdata & "HLHLHLLL, HLHLHLLH, HLHLHLHL, LHHHHLHH, HHHHHHHH"

        If FnTestLoopBack(xRS232) = PASSED Then
            Echo(FormatResults(True, "COM2 RS232 LoopBack", sTNum))
            Call IncStepPassed()
        Else
            Echo(FormatResults(False, "COM2 RS232 LoopBack", sTNum))
            sMsg = "Write Data=" & Wdata & vbCrLf
            sMsg = sMsg & "Read Data=" & Rdata & vbCrLf
            Echo(sMsg)
            RegisterFailure(CStr(RS232), sTNum, sComment:="COM2 RS232 LOOPBACK TEST FAILED")
            TestCom2 = FAILED
            Call IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = 2 Then
            GoTo COM2_2
        End If
        frmSTest.proProgress.Value = 100

TestComplete:
        frmSTest.cmdAbort.Text = "Abort Test"
        frmSTest.cmdPause.Text = "Pause Test"
        If AbortTest = True Then
            If TestCom2 = FAILED Then
                ReportFailure(RS232)
            Else
                ReportUnknown(RS232)
                TestCom2 = -99
            End If
            sMsg = vbCrLf
            sMsg &= "      **********************************************" & vbCrLf
            sMsg &= "      *         COM2 RS232 tests aborted!          *" & vbCrLf
            sMsg &= "      **********************************************" & vbCrLf
            Echo(sMsg)
        ElseIf TestCom2 = PASSED Then
            ReportPass(RS232)
        ElseIf TestCom2 = FAILED Then
            ReportFailure(RS232)
        Else
            ReportUnknown(RS232)
        End If
        If CloseProgram = True Then
            EndProgram()
        End If


    End Function

    Public Function TestSerialBUS() As Short

        ' Test the Serial(RS485,RS422,RS232)-37pin via loop back plugs at J10 (SERIAL) port
        ' SP2 - DB37 loopback shorting plug
        Dim x As Integer
        Dim S As String
        Dim TestName As String = "PCI SERIAL"

        Call InitComVar()


        'RS485 COM Test Title block
        EchoTitle(InstrumentDescription(SERIALCCA) & " ", True)
        EchoTitle("COM Port Tests", False)
        frmSTest.proProgress.Maximum = 100
        frmSTest.proProgress.Value = 1

        TestSerialBUS = UNTESTED
        If AbortTest = True Then GoTo TestComplete
        AbortTest = False

        If FirstPass = True Then
            If RunningEndToEnd = False Then
                x = MsgBox("Is the SP2 adapter installed on the CIC J10 (serial) connector?", MsgBoxStyle.YesNo)
                If x <> 6 Then
                    S = "Connect SP2 (93006H4240) adapter to the CIC J10 (Serial) connector." & vbCrLf
                    DisplaySetup(S, "ST-SP2-1.jpg", 1)
                End If

            ElseIf (ControlStartTest = SERIALCCA) Then
                'Install the SP2,SP3,SP4 adapters and the W206, W209 cables
                InstallSPCables()
            End If
            If AbortTest = True Then GoTo TestComplete
        End If
        System.Windows.Forms.Application.DoEvents()
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        TestSerialBUS = PASSED

        'Serial RS485 Setup test
COM3_1: sTNum = "COM-03-001"
        Wparity = "ODD"
        Wbitrate = "9600Hz"
        Wstopbits = "1bits"
        Wwordlength = "8bits"
        Wmaxt = "5s"
        Wdata = "LLLHHLHL, LLHLHLHH, LLHHHHLL, LHLLHHLH, LHLHHHHL, "
        Wdata = Wdata & "LHHLHHHH, LHHHLLLL, HLLLLLLH, HLLHLLHL, HLHLLLHH, "
        Wdata = Wdata & "HLHLLLHH, HLHLLHLL, HLHLLHLH, HLHLLHHL, HLHLLHHH, "
        Wdata = Wdata & "HLHLHLLL, HLHLHLLH, HLHLHLHL, LHHHHLHH, HHHHHHHH"
        WCOMSetting = Wbitrate & "," & Wparity & "," & Wwordlength & "," & Wstopbits & "," & Wmaxt

        If FnTestSetup(xSerRS485) = PASSED Then
            Echo(FormatResults(True, "Serial RS485 Setup, " & WCOMSetting, sTNum))
            Call IncStepPassed()
        Else
            Echo(FormatResults(False, "Serial RS485 Setup, " & WCOMSetting, sTNum))
            sMsg = ""
            sMsg = "Serial RS485 was not able to configure the port settings." & vbCrLf
            If Wparity <> Rparity Then sMsg = sMsg & "  Parity Write=" & Wparity & ",  Read=" & Rparity & vbCrLf
            If Wbitrate <> Rbitrate Then sMsg = sMsg & "  Bitrate Write=" & Wbitrate & ",  Read=" & Rbitrate & vbCrLf
            If Wstopbits <> Rstopbits Then sMsg = sMsg & "  StopBits Write=" & Wstopbits & ",  Read=" & Rstopbits & vbCrLf
            If Wwordlength <> Rwordlength Then sMsg = sMsg & "  WordLength Write=" & Wwordlength & ",  Read=" & Rwordlength & vbCrLf
            If Wmaxt <> Rmaxt Then sMsg = sMsg & "  MaxTimeOut Write=" & Wmaxt & ",  Read=" & Rmaxt & vbCrLf
            Echo(sMsg)
            RegisterFailure(CStr(SERIALCCA), sTNum, sComment:="Serial RS485 SETUP TEST FAILED")
            TestSerialBUS = FAILED
            Call IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = 1 Then
            GoTo COM3_1
        End If
        frmSTest.proProgress.Value = 25

        ' Serial RS485 Loopback
COM3_2: sTNum = "COM-03-002"
        Wparity = "ODD"
        Wbitrate = "9600Hz"
        Wstopbits = "1bits"
        Wwordlength = "8bits"
        Wmaxt = "5s"
        Wdata = "LLLHHLHL, LLHLHLHH, LLHHHHLL, LHLLHHLH, LHLHHHHL, "
        Wdata = Wdata & "LHHLHHHH, LHHHLLLL, HLLLLLLH, HLLHLLHL, HLHLLLHH, "
        Wdata = Wdata & "HLHLLLHH, HLHLLHLL, HLHLLHLH, HLHLLHHL, HLHLLHHH, "
        Wdata = Wdata & "HLHLHLLL, HLHLHLLH, HLHLHLHL, LHHHHLHH, HHHHHHHH"
        'baudrate, parity, wordlength, stopbits
        WCOMSetting = "9600Hz,ODD,8,1bits"
        If FnTestLoopBack(xSerRS485) = PASSED Then
            Echo(FormatResults(True, "Serial RS485 LoopBack", sTNum))
            Call IncStepPassed()
        Else
            Echo(FormatResults(False, "Serial RS485 LoopBack", sTNum))
            sMsg = "Write Data=" & Wdata & vbCrLf
            sMsg = sMsg & "Read Data=" & Rdata & vbCrLf
            Echo(sMsg)
            RegisterFailure(CStr(SERIALCCA), sTNum, sComment:="Serial RS485 LOOPBACK TEST FAILED")
            TestSerialBUS = FAILED
            Call IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = 2 Then
            GoTo COM3_2
        End If
        frmSTest.proProgress.Value = 40

        ' Serial RS422 Setup test
COM4_1: sTNum = "COM-04-001"
        Wparity = "ODD"
        Wbitrate = "9600Hz"
        Wstopbits = "1bits"
        Wwordlength = "8bits"
        Wmaxt = "5s"
        Wdata = "LLLHHLHL, LLHLHLHH, LLHHHHLL, LHLLHHLH, LHLHHHHL, "
        Wdata = Wdata & "LHHLHHHH, LHHHLLLL, HLLLLLLH, HLLHLLHL, HLHLLLHH, "
        Wdata = Wdata & "HLHLLLHH, HLHLLHLL, HLHLLHLH, HLHLLHHL, HLHLLHHH, "
        Wdata = Wdata & "HLHLHLLL, HLHLHLLH, HLHLHLHL, LHHHHLHH, HHHHHHHH"
        WCOMSetting = Wbitrate & "," & Wparity & "," & Wwordlength & "," & Wstopbits & "," & Wmaxt

        If FnTestSetup(xSerRS422) = PASSED Then
            Echo(FormatResults(True, "Serial RS422 Setup, " & WCOMSetting, sTNum))
            Call IncStepPassed()
        Else
            Echo(FormatResults(False, "Serial RS422 Setup, " & WCOMSetting, sTNum))
            sMsg = ""
            sMsg = "Serial RS422 was not able to configure the port settings." & vbCrLf
            If Wparity <> Rparity Then sMsg = sMsg & "  Parity Write=" & Wparity & ",  Read=" & Rparity & vbCrLf
            If Wbitrate <> Rbitrate Then sMsg = sMsg & "  Bitrate Write=" & Wbitrate & ",  Read=" & Rbitrate & vbCrLf
            If Wstopbits <> Rstopbits Then sMsg = sMsg & "  StopBits Write=" & Wstopbits & ",  Read=" & Rstopbits & vbCrLf
            If Wwordlength <> Rwordlength Then sMsg = sMsg & "  WordLength Write=" & Wwordlength & ",  Read=" & Rwordlength & vbCrLf
            If Wmaxt <> Rmaxt Then sMsg = sMsg & "  MaxTimeOut Write=" & Wmaxt & ",  Read=" & Rmaxt & vbCrLf
            Echo(sMsg)
            RegisterFailure(CStr(SERIALCCA), sTNum, sComment:="Serial RS422 SETUP TEST FAILED")
            TestSerialBUS = FAILED
            Call IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = 3 Then
            GoTo COM4_1
        End If
        frmSTest.proProgress.Value = 55

        ' Serial RS422 Loopback
COM4_2: sTNum = "COM-04-002"
        Wparity = "ODD"
        Wbitrate = "9600Hz"
        Wstopbits = "1bits"
        Wwordlength = "8bits"
        Wmaxt = "5s"
        Wdata = "LLLHHLHL, LLHLHLHH, LLHHHHLL, LHLLHHLH, LHLHHHHL, "
        Wdata = Wdata & "LHHLHHHH, LHHHLLLL, HLLLLLLH, HLLHLLHL, HLHLLLHH, "
        Wdata = Wdata & "HLHLLLHH, HLHLLHLL, HLHLLHLH, HLHLLHHL, HLHLLHHH, "
        Wdata = Wdata & "HLHLHLLL, HLHLHLLH, HLHLHLHL, LHHHHLHH, HHHHHHHH"

        If FnTestLoopBack(xSerRS422) = PASSED Then
            Echo(FormatResults(True, "Serial RS422 LoopBack", sTNum))
            Call IncStepPassed()
        Else
            Echo(FormatResults(False, "Serial RS422 LoopBack", sTNum))
            sMsg = "Write Data=" & Wdata & vbCrLf
            sMsg = sMsg & "Read Data=" & Rdata & vbCrLf
            Echo(sMsg)
            RegisterFailure(CStr(SERIALCCA), sTNum, sComment:="Serial RS422 LOOPBACK TEST FAILED")
            TestSerialBUS = FAILED
            Call IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = 4 Then
            GoTo COM4_2
        End If
        frmSTest.proProgress.Value = 70

        ' Serial RS232 Setup test
COM5_1: sTNum = "COM-05-001"
        Wparity = "ODD"
        Wbitrate = "9600Hz"
        Wstopbits = "1bits"
        Wwordlength = "8bits"
        Wmaxt = "5s"
        Wdata = "LLLHHLHL, LLHLHLHH, LLHHHHLL, LHLLHHLH, LHLHHHHL, "
        Wdata = Wdata & "LHHLHHHH, LHHHLLLL, HLLLLLLH, HLLHLLHL, HLHLLLHH, "
        Wdata = Wdata & "HLHLLLHH, HLHLLHLL, HLHLLHLH, HLHLLHHL, HLHLLHHH, "
        Wdata = Wdata & "HLHLHLLL, HLHLHLLH, HLHLHLHL, LHHHHLHH, HHHHHHHH"
        WCOMSetting = Wbitrate & "," & Wparity & "," & Wwordlength & "," & Wstopbits & "," & Wmaxt

        If FnTestSetup(xSerRS232) = PASSED Then
            Echo(FormatResults(True, "Serial RS232 Setup, " & WCOMSetting, sTNum))
            Call IncStepPassed()
        Else
            Echo(FormatResults(False, "Serial RS232 Setup, " & WCOMSetting, sTNum))
            sMsg = ""
            sMsg = "COM2/RS232 was not able to configure the port settings." & vbCrLf
            If Wparity <> Rparity Then sMsg = sMsg & "  Parity Write=" & Wparity & ",  Read=" & Rparity & vbCrLf
            If Wbitrate <> Rbitrate Then sMsg = sMsg & "  Bitrate Write=" & Wbitrate & ",  Read=" & Rbitrate & vbCrLf
            If Wstopbits <> Rstopbits Then sMsg = sMsg & "  StopBits Write=" & Wstopbits & ",  Read=" & Rstopbits & vbCrLf
            If Wwordlength <> Rwordlength Then sMsg = sMsg & "  WordLength Write=" & Wwordlength & ",  Read=" & Rwordlength & vbCrLf
            If Wmaxt <> Rmaxt Then sMsg = sMsg & "  MaxTimeOut Write=" & Wmaxt & ",  Read=" & Rmaxt & vbCrLf
            Echo(sMsg)
            RegisterFailure(CStr(SERIALCCA), sTNum, sComment:="Serial RS232 SETUP TEST FAILED")
            TestSerialBUS = FAILED
            Call IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = 5 Then
            GoTo COM5_1
        End If
        frmSTest.proProgress.Value = 85

        ' Serial RS232 Loopback
COM5_2: sTNum = "COM-05-002"
        Wparity = "ODD"
        Wbitrate = "9600Hz"
        Wstopbits = "1bits"
        Wwordlength = "8bits"
        Wmaxt = "5s"
        Wdata = "LLLHHLHL, LLHLHLHH, LLHHHHLL, LHLLHHLH, LHLHHHHL, "
        Wdata = Wdata & "LHHLHHHH, LHHHLLLL, HLLLLLLH, HLLHLLHL, HLHLLLHH, "
        Wdata = Wdata & "HLHLLLHH, HLHLLHLL, HLHLLHLH, HLHLLHHL, HLHLLHHH, "
        Wdata = Wdata & "HLHLHLLL, HLHLHLLH, HLHLHLHL, LHHHHLHH, HHHHHHHH"

        If FnTestLoopBack(xSerRS232) = PASSED Then
            Echo(FormatResults(True, "Serial RS232 LoopBack", sTNum))
            Call IncStepPassed()
        Else
            Echo(FormatResults(False, "Serial RS232 LoopBack", sTNum))
            sMsg = "Write Data=" & Wdata & vbCrLf
            sMsg = sMsg & "Read Data=" & Rdata & vbCrLf
            Echo(sMsg)
            RegisterFailure(CStr(SERIALCCA), sTNum, sComment:="Serial RS232 LOOPBACK TEST FAILED")
            TestSerialBUS = FAILED
            Call IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = 6 Then
            GoTo COM5_2
        End If
        frmSTest.proProgress.Value = 100

TestComplete:
        frmSTest.cmdAbort.Text = "Abort Test"
        frmSTest.cmdPause.Text = "Pause Test"
        If AbortTest = True Then
            If TestSerialBUS = FAILED Then
                ReportFailure(SERIALCCA)
            Else
                ReportUnknown(SERIALCCA)
                TestSerialBUS = -99
            End If
            sMsg = vbCrLf
            sMsg &= "      **********************************************" & vbCrLf
            sMsg &= "      *         Serial Card tests aborted!         *" & vbCrLf
            sMsg &= "      **********************************************" & vbCrLf
            Echo(sMsg)
        ElseIf TestSerialBUS = PASSED Then
            ReportPass(SERIALCCA)
        ElseIf TestSerialBUS = FAILED Then
            ReportFailure(SERIALCCA)
        Else
            ReportUnknown(SERIALCCA)
        End If
        If CloseProgram = True Then
            EndProgram()
        End If

    End Function


    Public Function TestGigabitEthernet() As Short

        Dim x As Integer
        Dim x1 As Integer
        Dim S As String
        Dim s1 As String
        Dim s2 As String
        Dim status As Integer
        Dim TestName As String = "GIGABIT"
        Dim LoopStepNo As Integer = 1

        Call InitComVar()

        'Gigabit Ethernet Test Title block
        EchoTitle(InstrumentDescription(GIGA) & " ", True)
        EchoTitle("Gigabit Ethernet Bus Test", False)
        frmSTest.proProgress.Maximum = 100
        frmSTest.proProgress.Value = 1

        TestGigabitEthernet = UNTESTED
        If AbortTest = True Then GoTo TestComplete
        AbortTest = False

        If RunningEndToEnd = False And FirstPass = True Then
            x = MsgBox("Is cable W206(Red) installed from CIC J15 (GIGABIT) to CIC J16 (LAN)?" & vbCrLf & "Is cable W210(Green) installed from CIC J18 (GIGABIT) to CIC J19 (LAN)?", MsgBoxStyle.YesNo)
            If x <> 6 Then
                S = "Use cable W206 (93006H6861) to connect" & vbCrLf & vbCrLf
                S = "        CIC J15 (GIGABIT)   to   CIC J16 (LAN)." & vbCrLf & vbCrLf
                S = "Use cable W210 (93006H6861) to connect" & vbCrLf & vbCrLf
                S = "        CIC J18 (GIGABIT)   to   CIC J19 (LAN)."
                DisplaySetup(S, "ST-W206-1.jpg", 1)
                If AbortTest = True Then GoTo testcomplete
            End If
        End If
        System.Windows.Forms.Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        TestGigabitEthernet = PASSED

        status = FnComRemove(xGBitEth1) ' turn off ethernet 1
        status = FnComRemove(xGBitEth2) ' turn off ethernet 2
        status = FnComRemove(xGBitEth3) ' turn off ethernet 3
        status = FnComRemove(xGBitEth4) ' turn off ethernet 4
        LoopStepNo = 1


GIGA_1: sTNum = "GIGA-01-001" 'Test GigaBit1 Setup (Blackbody)
        WlocalIP = "192.168.0.1"
        WremoteIP = "192.168.0.2"
        Wmask = "255.255.255.0"
        Wgateway = "192.168.0.2"
        Wmaxt = "2s"
        Wremport = "21"
        Wspec = "TCP" ' or "UDP"

        If FnTestSetup(xGBitEth1) = PASSED Then
            Echo(FormatResults(True, "GIGABIT1 Setup", sTNum))
            Call IncStepPassed()
        Else
            Echo(FormatResults(False, "GIGABIT1 Setup", sTNum))
            sMsg = ""
            If WremoteIP <> RremoteIP Then sMsg = sMsg & "RemoteIP Write=" & WremoteIP & "  Read=" & RremoteIP & vbCrLf
            If WlocalIP <> RlocalIP Then sMsg = sMsg & "LocalIP Write=" & WlocalIP & "  Read=" & RlocalIP & vbCrLf
            If Wmask <> Rmask Then sMsg = sMsg & "LocalSubnetMask Write=" & Wmask & "  Read=" & Rmask & vbCrLf
            If Wgateway <> Rgateway Then sMsg = sMsg & "LocalGateway Write=" & Wgateway & "  Read=" & Rgateway & vbCrLf
            If Wmaxt <> Rmaxt Then sMsg = sMsg & "MaxTime Write=" & Wmaxt & "  Read=" & Rmaxt & vbCrLf
            If Wremport <> Rremport Then sMsg = sMsg & "RemotePort Write=" & Wremport & "  Read=" & Rremport & vbCrLf
            If Wspec <> Rspec Then sMsg = sMsg & "Protocol Write=" & Wspec & "  Read=" & Rspec & vbCrLf
            Echo(sMsg)
            RegisterFailure(CStr(GIGA), sTNum, sComment:="GIGABIT1 SETUP TEST FAILED")
            TestGigabitEthernet = FAILED
            Call IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then
            GoTo GIGA_1
        End If
        frmSTest.proProgress.Value = 16.7
        LoopStepNo += 1

GIGA_2: sTNum = "GIGA-01-002" ' Ethernet Ipconfig
        MyUtilities.RunCommandCom("ipconfig", " > " & sAts_INI_path & "ipConfig.txt", False)
        Delay(1)
        Application.DoEvents()

        STfileHandle = FreeFile()
        FileOpen(STfileHandle, sAts_INI_path & "ipConfig.txt", OpenMode.Binary)
        ''Ethernet adapter Gigabit1:

        ''   Connection-specific DNS Suffix  . :
        ''   Link-local IPv6 Address . . . . . : fe80::b100:aa24:7f98:5b9%23
        ''   IPv4 Address. . . . . . . . . . . : 192.168.0.1
        ''   Subnet Mask . . . . . . . . . . . : 255.255.255.0
        ''   Default Gateway . . . . . . . . . : 192.168.0.2

        'or
        ''Ethernet adapter Gigabit1:

        ''   Media State . . . . . . . . . . . : Media disconnected


        S = Space(LOF(STfileHandle))
        FileGet(STfileHandle, S)
        FileClose(STfileHandle)
        Application.DoEvents()
        x = InStr(1, S, "Ethernet adapter Gigabit1:", CompareMethod.Text)
        s1 = S
        If x > 0 Then
            s1 = Mid(S, x)
            'throw away the first 2 lines
            x1 = InStr(s1, vbCrLf)
            x1 = InStr(x1 + 2, s1, vbCrLf)
            s1 = Mid(s1, x1 + 2)
            'get the nextline
            x1 = InStr(s1, vbCrLf)
            s2 = Left(s1, x1 - 1) 'first line
            x1 = InStr(x1 + 2, s1, vbCrLf)
            s1 = Mid(s1, x1 + 2) 'remaining lines
            If InStr(1, s2, "Connection-specific DNS Suffix  . :", CompareMethod.Text) Then
                'get the next 3 lines
                x1 = InStr(s1, vbCrLf)
                x1 = InStr(x1 + 2, s1, vbCrLf)
                x1 = InStr(x1 + 2, s1, vbCrLf)
                s1 = Left(s1, x1 - 1) 'remaining 3 lines
            ElseIf InStr(1, s2, "Media State . . . . . . . . . . . : Media disconnected", CompareMethod.Text) Then
                s1 = s2
            End If
        End If

        x = InStr(1, s1, "IPv4 Address. . . . . . . . . . . : 192.168.0.1", CompareMethod.Text)
        If x = 0 Then
            FormatResultLine(sTNum & " GIGABIT1 Connecion 192.168.0.1", False) 'failed
            RegisterFailure(CStr(GIGA), sTNum, sComment:=" GIGABIT1 Comm Test FAILED.")
            Echo(s1)
            TestGigabitEthernet = FAILED
            Call IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            FormatResultLine(sTNum & " GIGABIT1 Connecion 192.168.0.1", True) 'Pass
            Echo(s1)
            Call IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then
            Application.DoEvents()
            GoTo GIGA_2
        End If
        LoopStepNo += 1
        frmSTest.proProgress.Value = 33.4

GIGA_3: sTNum = "GIGA-02-001" '  Test Gigabit2 Setup (LAN)
        WlocalIP = "192.168.200.1"
        WremoteIP = "192.168.200.2"
        Wmask = "255.255.255.0"
        Wgateway = "192.168.200.2"
        Wmaxt = "2.5s"
        Wremport = "21"
        Wspec = "UDP" ' or "TCP" or "UDP"

        If FnTestSetup(xGBitEth2) = PASSED Then
            Echo(FormatResults(True, "GIGABIT2 Setup", sTNum))
            Call IncStepPassed()
        Else
            Echo(FormatResults(False, "GIGABIT2 Setup", sTNum))
            sMsg = ""
            If WremoteIP <> RremoteIP Then sMsg = sMsg & "RemoteIP Write=" & WremoteIP & "  Read=" & RremoteIP & vbCrLf
            If WlocalIP <> RlocalIP Then sMsg = sMsg & "LocalIP Write=" & WlocalIP & "  Read=" & RlocalIP & vbCrLf
            If Wmask <> Rmask Then sMsg = sMsg & "LocalSubnetMask Write=" & Wmask & "  Read=" & Rmask & vbCrLf
            If Wgateway <> Rgateway Then sMsg = sMsg & "LocalGateway Write=" & Wgateway & "  Read=" & Rgateway & vbCrLf
            If Wmaxt <> Rmaxt Then sMsg = sMsg & "MaxTime Write=" & Wmaxt & "  Read=" & Rmaxt & vbCrLf
            If Wremport <> Rremport Then sMsg = sMsg & "RemotePort Write=" & Wremport & "  Read=" & Rremport & vbCrLf
            If Wspec <> Rspec Then sMsg = sMsg & "Protocol Write=" & Wspec & "  Read=" & Rspec & vbCrLf
            Echo(sMsg)
            RegisterFailure(CStr(GIGA), sTNum, sComment:="GIGABIT2 SETUP TEST FAILED")
            TestGigabitEthernet = FAILED
            Call IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then
            GoTo GIGA_3
        End If
        LoopStepNo += 1
        frmSTest.proProgress.Value = 50

GIGA_4: sTNum = "GIGA-02-002" ' Ethernet Ipconfig

        ''Ethernet adapter Gigabit2:

        ''   Connection-specific DNS Suffix  . :
        ''   Link-local IPv6 Address . . . . . : fe80::94:b716:1542:ca5c%24
        ''   IPv4 Address. . . . . . . . . . . : 192.168.200.1
        ''   Subnet Mask . . . . . . . . . . . : 255.255.255.0
        ''   Default Gateway . . . . . . . . . : 192.168.200.2

        ''Ethernet adapter Gigabit2:

        ''   Media State . . . . . . . . . . . : Media disconnected

        x = InStr(1, S, "Ethernet adapter Gigabit2:", CompareMethod.Text)
        If x > 0 Then
            s1 = Mid(S, x)
            'throw away the first 2 lines
            x1 = InStr(s1, vbCrLf)
            x1 = InStr(x1 + 2, s1, vbCrLf)
            s1 = Mid(s1, x1 + 2)
            'set s2 to the nextline
            x1 = InStr(s1, vbCrLf)
            s2 = Left(s1, x1 - 1)
            x1 = InStr(x1 + 2, s1, vbCrLf)
            s1 = Mid(s1, x1 + 2) 'remaining lines
            If InStr(1, s2, "Connection-specific DNS Suffix  . :", CompareMethod.Text) Then
                'get the next 3 lines
                x1 = InStr(s1, vbCrLf)
                x1 = InStr(x1 + 2, s1, vbCrLf)
                x1 = InStr(x1 + 2, s1, vbCrLf)
                s1 = Left(s1, x1 - 1) 'remaining 3 lines
            ElseIf InStr(1, s2, "Media State . . . . . . . . . . . : Media disconnected", CompareMethod.Text) Then
                s1 = s2
            End If
        End If

        x = InStr(1, s1, "IPv4 Address. . . . . . . . . . . : 192.168.200.1", CompareMethod.Text)
        If x = 0 Then
            FormatResultLine(sTNum & " GIGABIT2 Connecion 192.168.200.1", False) 'failed
            RegisterFailure(CStr(GIGA), sTNum, sComment:=" GIGABIT2 Comm Test FAILED.")
            Echo(s1)
            TestGigabitEthernet = FAILED
            Call IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            FormatResultLine(sTNum & " GIGABIT2 Connecion 192.168.200.1", True) 'Pass
            Echo(s1)
            Call IncStepPassed()
        End If

        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then
            GoTo GIGA_4
        End If
        frmSTest.proProgress.Value = 66.8

        'GIGA_5: sTNum = "GIGA-03-001" 'Test GigaBit1 Setup (Blackbody)
        '        WlocalIP = "192.168.10.1"
        '        WremoteIP = "192.168.10.2"
        '        Wmask = "255.255.255.0"
        '        Wgateway = "192.168.10.2"
        '        Wmaxt = "2s"
        '        Wremport = "21"
        '        Wspec = "TCP" ' or "UDP"

        '        If FnTestSetup(xGBitEth1) = PASSED Then
        '            Echo(FormatResults(True, "Local Area Connection Setup", sTNum))
        '            Call IncStepPassed()
        '        Else
        '            Echo(FormatResults(False, "Local Area Connection Setup", sTNum))
        '            sMsg = ""
        '            If WremoteIP <> RremoteIP Then sMsg = sMsg & "RemoteIP Write=" & WremoteIP & "  Read=" & RremoteIP & vbCrLf
        '            If WlocalIP <> RlocalIP Then sMsg = sMsg & "LocalIP Write=" & WlocalIP & "  Read=" & RlocalIP & vbCrLf
        '            If Wmask <> Rmask Then sMsg = sMsg & "LocalSubnetMask Write=" & Wmask & "  Read=" & Rmask & vbCrLf
        '            If Wgateway <> Rgateway Then sMsg = sMsg & "LocalGateway Write=" & Wgateway & "  Read=" & Rgateway & vbCrLf
        '            If Wmaxt <> Rmaxt Then sMsg = sMsg & "MaxTime Write=" & Wmaxt & "  Read=" & Rmaxt & vbCrLf
        '            If Wremport <> Rremport Then sMsg = sMsg & "RemotePort Write=" & Wremport & "  Read=" & Rremport & vbCrLf
        '            If Wspec <> Rspec Then sMsg = sMsg & "Protocol Write=" & Wspec & "  Read=" & Rspec & vbCrLf
        '            Echo(sMsg)
        '            RegisterFailure(CStr(GIGA), sTNum, sComment:="Local Area Connection SETUP TEST FAILED")
        '            TestGigabitEthernet = FAILED
        '            Call IncStepFailed()
        '            If OptionFaultMode = SOFmode Then GoTo TestComplete
        '        End If
        '        Application.DoEvents()
        '        If AbortTest = True Then GoTo TestComplete
        '        If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then
        '            GoTo GIGA_5
        '        End If
        '        frmSTest.proProgress.Value = 62.5
        '        LoopStepNo += 1

        'GIGA_6: sTNum = "GIGA-03-002" ' Ethernet Ipconfig
        '        MyUtilities.RunCommandCom("ipconfig", " > " & sAts_INI_path & "ipConfig.txt", False)
        '        Delay(1)
        '        Application.DoEvents()

        '        STfileHandle = FreeFile()
        '        FileOpen(STfileHandle, sAts_INI_path & "ipConfig.txt", OpenMode.Binary)
        '        ''Ethernet adapter Gigabit1:

        '        ''   Connection-specific DNS Suffix  . :
        '        ''   Link-local IPv6 Address . . . . . : fe80::b100:aa24:7f98:5b9%23
        '        ''   IPv4 Address. . . . . . . . . . . : 192.168.0.1
        '        ''   Subnet Mask . . . . . . . . . . . : 255.255.255.0
        '        ''   Default Gateway . . . . . . . . . : 192.168.0.2

        '        'or
        '        ''Ethernet adapter Gigabit1:

        '        ''   Media State . . . . . . . . . . . : Media disconnected


        '        S = Space(LOF(STfileHandle))
        '        FileGet(STfileHandle, S)
        '        FileClose(STfileHandle)
        '        Application.DoEvents()
        '        x = InStr(1, S, "Ethernet adapter Local Area Connection:", CompareMethod.Text)
        '        s1 = S
        '        If x > 0 Then
        '            s1 = Mid(S, x)
        '            'throw away the first 2 lines
        '            x1 = InStr(s1, vbCrLf)
        '            x1 = InStr(x1 + 2, s1, vbCrLf)
        '            s1 = Mid(s1, x1 + 2)
        '            'get the nextline
        '            x1 = InStr(s1, vbCrLf)
        '            s2 = Left(s1, x1 - 1) 'first line
        '            x1 = InStr(x1 + 2, s1, vbCrLf)
        '            s1 = Mid(s1, x1 + 2) 'remaining lines
        '            If InStr(1, s2, "Connection-specific DNS Suffix  . :", CompareMethod.Text) Then
        '                'get the next 3 lines
        '                x1 = InStr(s1, vbCrLf)
        '                x1 = InStr(x1 + 2, s1, vbCrLf)
        '                x1 = InStr(x1 + 2, s1, vbCrLf)
        '                s1 = Left(s1, x1 - 1) 'remaining 3 lines
        '            ElseIf InStr(1, s2, "Media State . . . . . . . . . . . : Media disconnected", CompareMethod.Text) Then
        '                s1 = s2
        '            End If
        '        End If

        '        x = InStr(1, s1, "IPv4 Address. . . . . . . . . . . : 192.168.10.1", CompareMethod.Text)
        '        If x = 0 Then
        '            FormatResultLine(sTNum & " Local Area Connection Connecion 192.1681.0.1", False) 'failed
        '            RegisterFailure(CStr(GIGA), sTNum, sComment:=" Local Area Connection Comm Test FAILED.")
        '            Echo(s1)
        '            TestGigabitEthernet = FAILED
        '            Call IncStepFailed()
        '            If OptionFaultMode = SOFmode Then GoTo TestComplete
        '        Else
        '            FormatResultLine(sTNum & " Local Area Connection Connecion 192.168.10.1", True) 'Pass
        '            Echo(s1)
        '            Call IncStepPassed()
        '        End If
        '        Application.DoEvents()
        '        If AbortTest = True Then GoTo TestComplete
        '        If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then
        '            Application.DoEvents()
        '            GoTo GIGA_6
        '        End If
        '        LoopStepNo += 1
        '        frmSTest.proProgress.Value = 75

GIGA_5: sTNum = "GIGA-04-001" 'Test GigaBit1 Setup (Blackbody)
        WlocalIP = "192.168.20.1"
        WremoteIP = "192.168.20.2"
        Wmask = "255.255.255.0"
        Wgateway = "192.168.20.2"
        Wmaxt = "2s"
        Wremport = "21"
        Wspec = "TCP" ' or "UDP"

        If FnTestSetup(xGBitEth1) = PASSED Then
            Echo(FormatResults(True, "GIGABIT4 Setup", sTNum))
            Call IncStepPassed()
        Else
            Echo(FormatResults(False, "GIGABIT4 Setup", sTNum))
            sMsg = ""
            If WremoteIP <> RremoteIP Then sMsg = sMsg & "RemoteIP Write=" & WremoteIP & "  Read=" & RremoteIP & vbCrLf
            If WlocalIP <> RlocalIP Then sMsg = sMsg & "LocalIP Write=" & WlocalIP & "  Read=" & RlocalIP & vbCrLf
            If Wmask <> Rmask Then sMsg = sMsg & "LocalSubnetMask Write=" & Wmask & "  Read=" & Rmask & vbCrLf
            If Wgateway <> Rgateway Then sMsg = sMsg & "LocalGateway Write=" & Wgateway & "  Read=" & Rgateway & vbCrLf
            If Wmaxt <> Rmaxt Then sMsg = sMsg & "MaxTime Write=" & Wmaxt & "  Read=" & Rmaxt & vbCrLf
            If Wremport <> Rremport Then sMsg = sMsg & "RemotePort Write=" & Wremport & "  Read=" & Rremport & vbCrLf
            If Wspec <> Rspec Then sMsg = sMsg & "Protocol Write=" & Wspec & "  Read=" & Rspec & vbCrLf
            Echo(sMsg)
            RegisterFailure(CStr(GIGA), sTNum, sComment:="GIGABIT4 SETUP TEST FAILED")
            TestGigabitEthernet = FAILED
            Call IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then
            GoTo GIGA_5
        End If
        frmSTest.proProgress.Value = 83.5
        LoopStepNo += 1

GIGA_6: sTNum = "GIGA-04-002" ' Ethernet Ipconfig
        MyUtilities.RunCommandCom("ipconfig", " > " & sAts_INI_path & "ipConfig.txt", False)
        Delay(1)
        Application.DoEvents()

        STfileHandle = FreeFile()
        FileOpen(STfileHandle, sAts_INI_path & "ipConfig.txt", OpenMode.Binary)
        ''Ethernet adapter Gigabit1:

        ''   Connection-specific DNS Suffix  . :
        ''   Link-local IPv6 Address . . . . . : fe80::b100:aa24:7f98:5b9%23
        ''   IPv4 Address. . . . . . . . . . . : 192.168.0.1
        ''   Subnet Mask . . . . . . . . . . . : 255.255.255.0
        ''   Default Gateway . . . . . . . . . : 192.168.0.2

        'or
        ''Ethernet adapter Gigabit1:

        ''   Media State . . . . . . . . . . . : Media disconnected


        S = Space(LOF(STfileHandle))
        FileGet(STfileHandle, S)
        FileClose(STfileHandle)
        Application.DoEvents()
        x = InStr(1, S, "Ethernet adapter Gigabit4:", CompareMethod.Text)
        s1 = S
        If x > 0 Then
            s1 = Mid(S, x)
            'throw away the first 2 lines
            x1 = InStr(s1, vbCrLf)
            x1 = InStr(x1 + 2, s1, vbCrLf)
            s1 = Mid(s1, x1 + 2)
            'get the nextline
            x1 = InStr(s1, vbCrLf)
            s2 = Left(s1, x1 - 1) 'first line
            x1 = InStr(x1 + 2, s1, vbCrLf)
            s1 = Mid(s1, x1 + 2) 'remaining lines
            If InStr(1, s2, "Connection-specific DNS Suffix  . :", CompareMethod.Text) Then
                'get the next 3 lines
                x1 = InStr(s1, vbCrLf)
                x1 = InStr(x1 + 2, s1, vbCrLf)
                x1 = InStr(x1 + 2, s1, vbCrLf)
                s1 = Left(s1, x1 - 1) 'remaining 3 lines
            ElseIf InStr(1, s2, "Media State . . . . . . . . . . . : Media disconnected", CompareMethod.Text) Then
                s1 = s2
            End If
        End If

        x = InStr(1, s1, "IPv4 Address. . . . . . . . . . . : 192.168.20.1", CompareMethod.Text)
        If x = 0 Then
            FormatResultLine(sTNum & " GIGABIT4 Connecion 192.168.20.1", False) 'failed
            RegisterFailure(CStr(GIGA), sTNum, sComment:=" GIGABIT4 Comm Test FAILED.")
            Echo(s1)
            TestGigabitEthernet = FAILED
            Call IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            FormatResultLine(sTNum & " GIGABIT4 Connecion 192.168.20.1", True) 'Pass
            Echo(s1)
            Call IncStepPassed()
        End If
        Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = LoopStepNo Then
            Application.DoEvents()
            GoTo GIGA_6
        End If
        LoopStepNo += 1
        frmSTest.proProgress.Value = 100

TestComplete:

        frmSTest.cmdAbort.Text = "Abort Test"
        frmSTest.cmdPause.Text = "Pause Test"

        If AbortTest = True Then
            If TestGigabitEthernet = FAILED Then
                ReportFailure(GIGA)
            Else
                ReportUnknown(GIGA)
                TestGigabitEthernet = -99
            End If
            sMsg = vbCrLf
            sMsg &= "      **********************************************" & vbCrLf
            sMsg &= "      *       Gigabit Ethernet tests aborted!      *" & vbCrLf
            sMsg &= "      **********************************************" & vbCrLf
            Echo(sMsg)
        ElseIf TestGigabitEthernet = PASSED Then
            ReportPass(GIGA)
        ElseIf TestGigabitEthernet = FAILED Then
            ReportFailure(GIGA)
        Else
            ReportUnknown(GIGA)
        End If
        If CloseProgram = True Then
            EndProgram()
        End If

    End Function



    Private Function FnTestSetup(ByRef target As Integer) As Integer

        Dim Response As String = Space(4096)
        Dim sXML As String
        Dim nSize As Short = 4096
        Dim status As Integer

        'test com1 or com2 setup
        FnTestSetup = FAILED
        Select Case target
            Case xRS422, xRS232, xSerRS485, xSerRS422, xSerRS232
                sXML = BuildComXML("Status", target)
                status = atxml_IssueSignal(sXML, Response, nSize)

                status = FnComSetup(target, 1) ' 1st setup, Mode= All Listener
                Delay(1)

                sXML = BuildComXML("Status", target)
                status = atxml_IssueSignal(sXML, Response, nSize)

                status = FnComSetup(target, 2) ' 2nd setup, talker= UUT, Mode= Talker-Listener
                Delay(1)

                status = FnComFetchConfig(target)
                status = FnComReset(target, 2)
                If RCOMSetting = WCOMSetting Then
                    FnTestSetup = PASSED
                End If

                ' test the ethernet ports setup
            Case xGBitEth1, xGBitEth2
                status = FnComRemove(target)
                Delay(1)
                status = FnComSetup(target, 1)
                Delay(5)
                status = FnComFetchConfig(target)
                If Rspec = Wspec And Rmaxt = Wmaxt And RlocalIP = WlocalIP And Rmask = Wmask And Rgateway = Wgateway And RremoteIP = WremoteIP And Rremport = Wremport Then
                    FnTestSetup = PASSED
                End If
        End Select

    End Function



    Private Function FnTestLoopBack(ByRef target As Integer) As Integer

        Dim Response As String = Space(4096)
        Dim sXML As String
        Dim nSize As Short = 4096 ' was 255
        Dim status As Integer

        'test Com1,Com2,Com3,Com4 and Com5 loopback
        FnTestLoopBack = FAILED
        Select Case target
            Case xRS232, xRS422, xSerRS232, xSerRS422, xSerRS485

                '   If LiveMode(target) = CInt(True) Then 'If Communication has been verified
                sXML = BuildComXML("Status", target)
                status = atxml_IssueSignal(sXML, Response, nSize)

                status = FnComSetup(target, 1) ' 1st setup, Mode= All Listener
                Delay(1)

                sXML = BuildComXML("Status", target)
                status = atxml_IssueSignal(sXML, Response, nSize)

                status = FnComSetup(target, 2) ' 2nd setup, talker= UUT, Mode= Talker-Listener
                status = FnComEnable(target, 1)
                Delay(1)
                status = FnComFetchData(target) ' Note: gets Rdata
                status = FnComReset(target, 2)

                'Compare send and receive
                If Rdata = Wdata Then
                    FnTestLoopBack = PASSED
                Else
                    FnTestLoopBack = FAILED
                End If
                '    End If
        End Select

    End Function




    Function FnComDisable(ByRef target As Integer) As Integer
        '  ErrStatus = FnComDisable(xRS232)' xRS422,xSerRS232,xSerRS422,xSerRS485

        Dim status As Integer
        Dim XmlBuf As String = Space(4096)
        Dim Response As String = Space(4096)

        'Com1 or Com2 or Serial Card
        Select Case target
            Case xRS232, xRS422, xSerRS232, xSerRS422, xSerRS485
                XmlBuf = BuildComXML("Disable", target)
                status = atxml_IssueSignal(XmlBuf, Response, MAX_XML_SIZE)

                'Ethernet Card
            Case xGBitEth1, xGBitEth2
                XmlBuf = BuildComXML("Disable", target)
                status = atxml_IssueSignal(XmlBuf, Response, MAX_XML_SIZE)
        End Select
        FnComDisable = status
    End Function

    Function FnComFetchConfig(ByRef target As Integer) As Integer
        '  ErrStatus = FnComFetchConfig(xRS232)' xRS422,xSerRS232,xSerRS422,xSerRS485, xGbitEth1, xGbitEth2

        Dim status As Integer
        Dim XmlBuf As String = Space(4096)
        Dim Response As String = Space(4096)
        Dim tempStr As String = Space(255)
        Dim s As String
        Dim x1 As Integer

        ' Com1 or Com2 or Serial
        Select Case target
            Case xRS422, xRS232, xSerRS485, xSerRS422, xSerRS232
                ' Fetch
                Response = Space(4096)
                s = "<AtXmlSignalDescription xmlns:atXml=""ATXML_TSF"">" & vbCrLf
                s = s & "<SignalAction>Fetch</SignalAction>" & vbCrLf
                s = s & "<SignalResourceName>" & rName(target) & "</SignalResourceName>" & vbCrLf
                s = s & "<SignalSnippit>" & vbCrLf
                s = s & "<Signal Name=""RS_SIGNAL"" Out=""exchange"">" & vbCrLf
                s = s & "<" & pName(target) & " name=""exchange"""
                s = s & " attribute=""config"" />" & vbCrLf
                s = s & "</Signal>" & vbCrLf
                s = s & "</SignalSnippit>" & vbCrLf
                s = s & "</AtXmlSignalDescription>"
                XmlBuf = s

                status = atxml_IssueSignal(XmlBuf, Response, MAX_XML_SIZE)
                FnComFetchConfig = status
                'Sample Response
                ''<AtXmlResponse>
                '' <ReturnData>
                ''<ValuePair>
                '' <Attribute>config</Attribute>
                ''<Value>
                ''<c:Datum xsi:type="c:string" unit="None"><c:Value>9.6e3,8,1,0,5.000000</c:Value></c:Datum>
                ''</Value>
                ''</ValuePair>
                ''</ReturnData>
                '' </AtXmlResponse>
                If status <> 0 Then
                    Echo("COM Error - Fetch " & CStr(status))
                End If
                '   MsgBox(Response)
                '9600.000000,8,1,0,5.000000

                x1 = InStr(Response, "<c:Value>")
                If x1 > 0 Then
                    Response = Mid(Response, x1 + 9)
                    x1 = InStr(Response, "</c:Value>")
                    If x1 > 8 Then
                        Response = Left(Response, x1 - 1)
                    End If
                End If
                Rbitrate = ""
                Rwordlength = ""
                Rstopbits = ""
                Rparity = ""
                Rmaxt = ""
                x1 = InStr(Response, ",")
                If x1 > 0 Then
                    Rbitrate = Left(Response, x1 - 1)
                    Response = Mid(Response, x1 + 1)
                    If IsNumeric(Rbitrate) Then
                        Rbitrate = CStr(CInt(Rbitrate))
                    End If
                    Rbitrate = Rbitrate & "Hz"
                    x1 = InStr(Response, ",")
                    If x1 > 0 Then
                        Rwordlength = Left(Response, x1 - 1) & "bits"
                        Response = Mid(Response, x1 + 1)
                        x1 = InStr(Response, ",")
                        If x1 > 0 Then
                            Rparity = Left(Response, x1 - 1)
                            Response = Mid(Response, x1 + 1)
                            Select Case Rparity
                                Case "0" : Rparity = "NONE"
                                Case "1" : Rparity = "ODD"
                                Case "2" : Rparity = "EVEN"
                            End Select
                            x1 = InStr(Response, ",")
                            If x1 > 0 Then
                                Rstopbits = Left(Response, x1 - 1)
                                Response = Mid(Response, x1 + 1)
                                Select Case Rstopbits
                                    Case "0" : Rstopbits = "1" & "bits"
                                    Case "1" : Rstopbits = "1.5" & "bits"
                                    Case "2" : Rstopbits = "2" & "bits"
                                End Select
                            End If
                            Rmaxt = Response
                            x1 = InStr(Rmaxt, ".")
                            If x1 > 1 Then
                                Rmaxt = Left(Rmaxt, x1 - 1)
                            End If
                            Rmaxt = Rmaxt & "s"
                        End If
                    End If
                End If
                RCOMSetting = Rbitrate & "," & Rparity & "," & Rwordlength & "," & Rstopbits & "," & Rmaxt


                'Fetch Ethernet settings
            Case xGBitEth1, xGBitEth2
                Response = Space(4096)
                ' initialize resource name
                s = "<AtXmlSignalDescription xmlns:atXml=""ATXML_TSF"">" & vbCrLf
                s = s & "<SignalAction>Fetch</SignalAction>" & vbCrLf
                s = s & "<SignalResourceName>" & rName(target) & "</SignalResourceName>" & vbCrLf
                s = s & "<SignalSnippit>" & vbCrLf
                s = s & "<Signal Name=""ETHERNET_SIGNAL"" Out=""exchange"">" & vbCrLf
                s = s & "<" & pName(target) & " name=""exchange"""
                s = s & " data_bits=""" & Wdata & """"
                s = s & " maxTime=""" & Wmaxt & """"
                s = s & " localIP=""" & WlocalIP & """"
                s = s & " localSubnetMask=""" & Wmask & """"
                s = s & " localGateway=""" & Wgateway & """"
                s = s & " remoteIP=""" & WremoteIP & """"
                s = s & " remotePort=""" & Wremport & """"
                s = s & " attribute=""config""/>" & vbCrLf
                s = s & "</Signal>" & vbCrLf
                s = s & "</SignalSnippit>" & vbCrLf
                s = s & "</AtXmlSignalDescription>"
                XmlBuf = s
                status = atxml_IssueSignal(XmlBuf, Response, MAX_XML_SIZE)
                FnComFetchConfig = status
                'Sample Response
                ''s = "<AtXmlResponse>"
                ''s = s & "<ReturnData>"
                ''s = s & "<ValuePair>"
                ''s = s & "<Attribute>remoteIP</Attribute>"
                ''s = s & "<Value>"
                ''s = s & "<c:Datum xsi:type=""c:string"" unit=""""><c:Value>192.168.0.1</c:Value></c:Datum>"
                ''s = s & "</Value>"
                ''s = s & "</ValuePair>"
                ''s = s & "</ReturnData>"
                ''s = s & "</AtXmlResponse>"

                ''s = s & "<AtXmlResponse>"
                ''s = s & "<ReturnData>"
                ''s = s & "<ValuePair>"
                ''s = s & "<Attribute>remotePort</Attribute>"
                ''s = s & "<Value>"
                ''s = s & "<c:Datum xsi:type=""c:integer"" unit="""" value=""21""/>"
                ''s = s & "</Value>"
                ''s = s & "</ValuePair>"
                ''s = s & "</ReturnData>"
                ''s = s & "</AtXmlResponse>"

                ''s = s & "<AtXmlResponse>"
                ''s = s & "<ReturnData>"
                ''s = s & "<ValuePair>"
                ''s = s & "<Attribute>spec</Attribute>"
                ''s = s & "<Value>"
                ''s = s & "<c:Datum xsi:type=""c:string"" unit=""""><c:Value>TCP</c:Value></c:Datum>"
                ''s = s & "</Value>"
                ''s = s & "</ValuePair>"
                ''s = s & "</ReturnData>"
                ''s = s & "</AtXmlResponse>"

                ''s = s & "<AtXmlResponse>"
                ''s = s & "<ReturnData>"
                ''s = s & "<ValuePair>"
                ''s = s & "<Attribute>localIP</Attribute>"
                ''s = s & "<Value>"
                ''s = s & "<c:Datum xsi:type=""c:string"" unit=""""><c:Value>192.168.0.1</c:Value></c:Datum>"
                ''s = s & "</Value>"
                ''s = s & "</ValuePair>"
                ''s = s & "</ReturnData>"
                ''s = s & "</AtXmlResponse>"

                ''s = s & "<AtXmlResponse>"
                ''s = s & "<ReturnData>"
                ''s = s & "<ValuePair>"
                ''s = s & "<Attribute>localSubnetMask</Attribute>"
                ''s = s & "<Value>"
                ''s = s & "<c:Datum xsi:type=""c:string"" unit=""""><c:Value>255.255.255.0</c:Value></c:Datum>"
                ''s = s & "</Value>"
                ''s = s & "</ValuePair>"
                ''s = s & "</ReturnData>"
                ''s = s & "</AtXmlResponse>"

                ''s = s & "<AtXmlResponse>"
                ''s = s & "<ReturnData>"
                ''s = s & "<ValuePair>"
                ''s = s & "<Attribute>localGateway</Attribute>"
                ''s = s & "<Value>"
                ''s = s & "<c:Datum xsi:type=""c:string"" unit=""""><c:Value>192.168.0.2</c:Value></c:Datum>"
                ''s = s & "</Value>"
                ''s = s & "</ValuePair>"
                ''s = s & "</ReturnData>"
                ''s = s & "</AtXmlResponse>"

                ''s = s & "<AtXmlResponse>"
                ''s = s & "<ReturnData>"
                ''s = s & "<ValuePair>"
                ''s = s & "<Attribute>maxTime</Attribute>"
                ''s = s & "<Value>"
                ''s = s & "<c:Datum xsi:type=""c:double"" unit="""" value=""2.000000000000E+000""/>"
                ''s = s & "</Value>"
                ''s = s & "</ValuePair>"
                ''s = s & "</ReturnData>"
                ''s = s & "</AtXmlResponse>"
                ''Response = s

                Rspec = ""
                If InStr(Response, "spec") > 0 Then
                    Rspec = GetStringValue(Response, "spec") ' was 255
                End If

                Rmaxt = ""
                If InStr(Response, "maxTime") > 0 Then
                    Rmaxt = GetStringValue(Response, "maxTime")
                    Rmaxt = CStr(CDbl(Rmaxt)) & "s"
                End If

                RlocalIP = ""
                If InStr(Response, "localIP") > 0 Then
                    RlocalIP = GetStringValue(Response, "localIP")
                End If

                Rmask = ""
                If InStr(Response, "localSubnetMask") > 0 Then
                    Rmask = GetStringValue(Response, "localSubnetMask")
                End If

                Rgateway = ""
                If InStr(Response, "localGateway") > 0 Then
                    Rgateway = GetStringValue(Response, "localGateway")
                End If

                RremoteIP = ""
                If InStr(Response, "remoteIP") > 0 Then
                    RremoteIP = GetStringValue(Response, "remoteIP")
                End If

                Rremport = ""
                If InStr(Response, "remotePort") > 0 Then
                    Rremport = GetStringValue(Response, "remotePort")
                    Rremport = CStr(CInt(Rremport))
                End If
        End Select
        FnComFetchConfig = status

    End Function

    Function FnComFetchData(ByRef target As Integer) As Integer
        ' ErrStatus = FnComFetch(xRS232)' xRS422,xSerRS232,xSerRS422,xSerRS485

        Dim status As Integer
        Dim XmlBuf As String = Space(4096)
        Dim Response As String = Space(4096)
        Dim temp As String = Space(4096)
        Dim s As String
        Dim i As Integer

        ' Com1 or Com2 or Serial
        Select Case target
            Case xRS422, xRS232, xSerRS232, xSerRS422, xSerRS485
                Response = Space(4096)
                ' Fetch
                s = "<AtXmlSignalDescription xmlns:atXml=""ATXML_TSF"">" & vbCrLf
                s = s & "<SignalAction>Fetch</SignalAction>" & vbCrLf
                s = s & "<SignalResourceName>" & rName(target) & "</SignalResourceName>" & vbCrLf
                s = s & "<SignalSnippit>" & vbCrLf
                s = s & "<Signal Name=""RS_SIGNAL"" Out=""exchange"">" & vbCrLf
                s = s & "<" & pName(target) & " name=""exchange"""
                s = s & " parity=""" & Wparity & """"
                s = s & " baud_rate=""" & Wbitrate & """"
                s = s & " stop_bits=""" & Wstopbits & """"
                s = s & " data_bits=""" & Wdata & """"
                s = s & " talker=""UUT"""
                s = s & " Mode=""Talker-Listener"""
                s = s & " wordLength=""" & Wwordlength & """"
                s = s & " terminate=""OFF"""
                s = s & " maxTime=""" & Wmaxt & """"
                s = s & " Delay=""1e-005s"""
                s = s & " attribute=""data"" />" & vbCrLf
                s = s & "</Signal>" & vbCrLf
                s = s & "</SignalSnippit>" & vbCrLf
                s = s & "</AtXmlSignalDescription>"
                XmlBuf = s
                status = atxml_IssueSignal(XmlBuf, Response, MAX_XML_SIZE)
                FnComFetchData = status

                'Ethernet Card
            Case xGBitEth1, xGBitEth2
                s = "<AtXmlSignalDescription xmlns:atXml=""ATXML_TSF"">" & vbCrLf
                s = s & "<SignalAction>Fetch</SignalAction>" & vbCrLf
                s = s & "<SignalResourceName>" & rName(target) & "</SignalResourceName>" & vbCrLf
                s = s & "<SignalSnippit>" & vbCrLf
                s = s & "<Signal Name=""ETHERNET_SIGNAL"" Out=""exchange"">" & vbCrLf
                s = s & "<" & pName(target) & " name=""exchange"""
                s = s & " data_bits=""" & Wdata & """"
                s = s & " maxTime=""" & Wmaxt & """"
                s = s & " localIP=""" & WlocalIP & """"
                s = s & " localSubnetMask=""" & Wmask & """"
                s = s & " localGateway=""" & Wgateway & """"
                s = s & " remoteIP=""" & WremoteIP & """"
                s = s & " remotePort=""" & Wremport & """"
                s = s & " attribute=""data""/>" & vbCrLf
                s = s & "</Signal>" & vbCrLf
                s = s & "</SignalSnippit>" & vbCrLf
                s = s & "</AtXmlSignalDescription>"
                XmlBuf = s
                status = atxml_IssueSignal(XmlBuf, Response, MAX_XML_SIZE)
                FnComFetchData = status
        End Select

        temp = ""
        Rdata = ""
        Dim x1 As Integer
        Dim x2 As Integer

        i = InStr(Response, "<c:Value>")
        If i > 0 Then
            temp = Mid(Response, i + 9)
            i = InStr(temp, "</c:Value>")
            If i > 8 Then
                temp = Left(temp, i)
                Rdata = temp
            End If
        End If


        If InStr(Response, "ErrStatus") = 0 Then 'Separate the data from the xml
            If InStr(Response, "data") > 0 Then
                status = atxml_GetStringValue(Response, "data", temp, 4096)
                Rdata = FnTrim(temp)
            End If
        Else
            x1 = InStr(Response, "errText") + 9
            If x1 > 0 Then
                x2 = InStr(x1, Response, "><")
                If x2 > 0 Then
                    sMsg = Mid(Response, x1, x2 - x1 - 1)
                End If
            End If
            sMsg = "Error: " & sMsg & vbCrLf
        End If
        FnComFetchData = status

    End Function

    Function FnComRemove(ByRef target As Integer) As Integer
        '  ErrStatus = FnComRemove(xRS232)' Removes COM 1 from kernel
        Dim status As Integer
        Dim XmlBuf As String = ""
        Dim Response As String = Space(4096)

        Select Case target
            Case xRS232, xRS422, xSerRS232, xSerRS422, xSerRS485
                XmlBuf = BuildComXML("Disable", target)
                status = atxml_IssueSignal(XmlBuf, Response, MAX_XML_SIZE)

            Case xGBitEth1, xGBitEth2, xGBitEth3, xGBitEth4
                XmlBuf = BuildComXML("Disable", target)
                status = atxml_IssueSignal(XmlBuf, Response, MAX_XML_SIZE)

        End Select
        FnComRemove = status

    End Function

    Function FnComReset(ByRef target As Integer, ModeX As Integer) As Integer 'Resets Com 1
        ' ErrStatus = FnComReset(xRS232)
        'target = xRS422(COM_1), xRS232(COM_2), xSerRS232(COM_3), xSerRS422(COM_4), xSerRS485(COM_5) 

        Dim status As Integer
        Dim XmlBuf As String = Space(4096)
        Dim Response As String = Space(4096)
        Dim s As String = ""

        'Test example
        'Wparity = "ODD"
        'Wbitrate = "115200Hz"
        'Wstopbits = "1bits"
        'Wwordlength = "8bits"
        'Wmaxt = "5s"
        'wdata = "LLLHHLHL, LLHLHLHH, LLHHHHLL, LHLLHHLH, LHLHHHHL, LHHLHHHH, LHHHLLLL, HLLLLLLH, HLLHLLHL, HLHLLLHH,
        '         HLHLLLHH, HLHLLHLL, HLHLLHLH, HLHLLHHL, HLHLLHHH, HLHLHLLL, HLHLHLLH, HLHLHLHL, LHHHHLHH, HHHHHHHH"

        Select Case target
            Case xRS232, xRS422, xSerRS232, xSerRS422, xSerRS485
                Response = Space(4096)
                s = "<AtXmlSignalDescription xmlns:atXml=""ATXML_TSF"">" & vbCrLf
                s = s & "<SignalAction>Reset</SignalAction>" & vbCrLf
                s = s & "<SignalResourceName>" & rName(target) & "</SignalResourceName>" & vbCrLf
                s = s & "<SignalSnippit>" & vbCrLf
                s = s & "<Signal Name=""RS_SIGNAL"" Out=""exchange"">" & vbCrLf
                s = s & "<" & pName(target) & " name=""exchange"""
                s = s & " parity= """ & Wparity & """"
                s = s & " baud_rate= """ & Wbitrate & """"
                s = s & " stop_bits= """ & Wstopbits & """"
                s = s & " data_bits= """ & Wdata & """"
                If ModeX = 1 Then
                    s = s & " Mode=""All-Listener"""
                Else
                    s = s & " Talker=""UUT"""
                    s = s & " Mode=""Talker-Listener"""
                End If
                s = s & " wordLength= """ & Wwordlength & """"
                s = s & " terminate = ""OFF"""
                s = s & " maxTime= """ & Wmaxt & """"
                s = s & " Delay= ""1e-005s"""
                s = s & " attribute= ""data"" />" & vbCrLf
                s = s & "</Signal>" & vbCrLf
                s = s & "</SignalSnippit>" & vbCrLf
                s = s & "</AtXmlSignalDescription>"

            Case xGBitEth1, xGBitEth2, xGBitEth3, xGBitEth4
                s = BuildComXML("Reset", target)

        End Select
        XmlBuf = s
        status = atxml_IssueSignal(XmlBuf, Response, MAX_XML_SIZE)
        FnComReset = status

    End Function



    Function FnComSetup(ByRef target As Integer, ModeX As Integer) As Integer
        ' ErrStatus = FnComSetup(xRS232)' xRS422,xSerRS232,xSerRS422,xSerRS485
        'target = xRS422(COM_1), xRS232(COM_2), xSerRS232(COM_3), xSerRS422(COM_4), xSerRS485(COM_5) 

        Dim status As Integer
        Dim XmlBuf As String
        Dim Response As String = Space(4096)
        Dim s As String

        Response = Space(4096)
        'Wparity = "ODD"
        'Wbitrate = "115200Hz"
        'Wstopbits = "1bits"
        'Wwordlength = "8bits"
        'Wmaxt = "5s"
        'wdata = "LLLHHLHL, LLHLHLHH, LLHHHHLL, LHLLHHLH, LHLHHHHL, "
        'Wdata = Wdata & "LHHLHHHH, LHHHLLLL, HLLLLLLH, HLLHLLHL, HLHLLLHH, "
        'Wdata = Wdata & "HLHLLLHH, HLHLLHLL, HLHLLHLH, HLHLLHHL, HLHLLHHH, "
        'Wdata = Wdata & "HLHLHLLL, HLHLHLLH, HLHLHLHL, LHHHHLHH, HHHHHHHH"
        Select Case target
            Case xRS232, xRS422, xSerRS232, xSerRS422, xSerRS485
                s = "<AtXmlSignalDescription xmlns:atXml=""ATXML_TSF"">" & vbCrLf
                s = s & "<SignalAction>Setup</SignalAction>" & vbCrLf
                s = s & "<SignalResourceName>" & rName(target) & "</SignalResourceName>" & vbCrLf
                s = s & "<SignalSnippit>" & vbCrLf
                s = s & "<Signal Name=""RS_SIGNAL"" Out=""exchange"">" & vbCrLf
                s = s & "<" & pName(target) & " name=""exchange"""
                s = s & " parity=""" & Wparity & """"
                s = s & " baud_rate=""" & Wbitrate & """"
                s = s & " stop_bits=""" & Wstopbits & """"
                s = s & " data_bits=""" & Wdata & """"
                If ModeX = 1 Then
                    s = s & " Mode=""All-Listener"""
                Else
                    s = s & " Talker=""UUT"""
                    s = s & " Mode=""Talker-Listener"""
                End If
                s = s & " wordLength=""" & Wwordlength & """"
                s = s & " terminate=""OFF"""
                s = s & " maxTime=""" & Wmaxt & """"
                s = s & " delay=""1e-005s"" />" & vbCrLf
                s = s & "</Signal>" & vbCrLf
                s = s & "</SignalSnippit>" & vbCrLf
                s = s & "</AtXmlSignalDescription>"
                XmlBuf = s
                status = atxml_IssueSignal(XmlBuf, Response, MAX_XML_SIZE)

            Case xGBitEth1, xGBitEth2, xGBitEth3, xGBitEth4 '   Ethernet card
                s = "<AtXmlSignalDescription xmlns:atXml=""ATXML_TSF"">" & vbCrLf
                s = s & "<SignalAction>Setup</SignalAction>" & vbCrLf
                s = s & "<SignalResourceName>" & rName(target) & "</SignalResourceName>" & vbCrLf
                s = s & "<SignalSnippit>" & vbCrLf
                s = s & "<Signal Name=""ETHERNET_SIGNAL"" Out=""exchange"">" & vbCrLf
                s = s & "<" & pName(target) & " name=""exchange"""
                s = s & " data_bits= """ & Wdata & """"
                s = s & " maxTime= """ & Wmaxt & """"
                s = s & " localIP= """ & WlocalIP & """"
                s = s & " localSubnetMask= """ & Wmask & """"
                s = s & " localGateway= """ & Wgateway & """"
                s = s & " remoteIP= """ & WremoteIP & """"
                s = s & " remotePort= """ & Wremport & """"
                s = s & " />" & vbCrLf
                s = s & "</Signal>" & vbCrLf
                s = s & "</SignalSnippit>" & vbCrLf
                s = s & "</AtXmlSignalDescription>"
                XmlBuf = s
                status = atxml_IssueSignal(XmlBuf, Response, MAX_XML_SIZE)

        End Select
        FnComSetup = status

    End Function

    Function FnComEnable(ByRef target As Integer, ModeX As Integer) As Integer 'Resets Com 1
        ' ErrStatus = FnComEnable(xRS232)
        'target = xRS422(COM_1), xRS232(COM_2), xSerRS232(COM_3), xSerRS422(COM_4), xSerRS485(COM_5) 

        Dim status As Integer
        Dim XmlBuf As String = Space(4096)
        Dim Response As String = Space(4096)
        Dim s As String = ""

        Select Case target
            Case xRS232, xRS422, xSerRS232, xSerRS422, xSerRS485
                s = "<AtXmlSignalDescription xmlns:atXml=""ATXML_TSF"">" & vbCrLf
                s = s & "<SignalAction>Enable</SignalAction>" & vbCrLf
                s = s & "<SignalResourceName>" & rName(target) & "</SignalResourceName>" & vbCrLf
                s = s & "<SignalSnippit>" & vbCrLf
                s = s & "<Signal Name=""RS_SIGNAL"" Out=""exchange"">" & vbCrLf
                s = s & "<" & pName(target) & " name=""exchange"""
                s = s & " parity= """ & Wparity & """"
                s = s & " baud_rate= """ & Wbitrate & """"
                s = s & " stop_bits= """ & Wstopbits & """"
                s = s & " data_bits= """ & Wdata & """"
                If ModeX = 1 Then
                    s = s & " Mode=""All-Listener"""
                Else
                    s = s & " Talker=""UUT"""
                    s = s & " Mode=""Talker-Listener"""
                End If
                s = s & " wordLength= """ & Wwordlength & """"
                s = s & " terminate= ""OFF"""
                s = s & " maxTime= """ & Wmaxt & """"
                s = s & " delay= "" 1e-005s"" />" & vbCrLf
                s = s & "</Signal>" & vbCrLf
                s = s & "</SignalSnippit>" & vbCrLf
                s = s & "</AtXmlSignalDescription>"

            Case xGBitEth1, xGBitEth2, xGBitEth3, xGBitEth4
                s = "<AtXmlSignalDescription xmlns:atXml=""ATXML_TSF"">" & vbCrLf
                s = s & "<SignalAction>Enable</SignalAction>" & vbCrLf
                s = s & "<SignalResourceName>" & rName(target) & "</SignalResourceName>" & vbCrLf
                s = s & "<SignalSnippit>" & vbCrLf
                s = s & "<Signal Name=""ETHERNET_SIGNAL"" Out=""exchange"">" & vbCrLf
                s = s & "<" & pName(target) & " name=""exchange"""
                s = s & " data_bits= """ & Wdata & """"
                s = s & " maxTime= """ & Wmaxt & """"
                s = s & " localIP= """ & WlocalIP & """"
                s = s & " localSubnetMask= """ & Wmask & """"
                s = s & " localGateway= """ & Wgateway & """"
                s = s & " remoteIP= """ & WremoteIP & """"
                s = s & " remotePort= """ & Wremport & """"
                s = s & " attribute= ""data"" />" & vbCrLf
                s = s & "</Signal>" & vbCrLf
                s = s & "</SignalSnippit>" & vbCrLf
                s = s & "</AtXmlSignalDescription>"
        End Select
        XmlBuf = s
        status = atxml_IssueSignal(XmlBuf, Response, MAX_XML_SIZE)
        FnComEnable = status


    End Function



    Function FnTrim(ByRef temp As String) As String

        ' this routine will trim chr = 0 from the string
        Dim x As String
        Dim Newtemp As String = ""
        Dim i As Short
        temp = Trim(temp) ' get rid of the spaces on each end

        i = 1
        For i = 1 To CShort(Len(temp))
            x = Mid(temp, i, 1)
            If x <> Chr(0) Then
                Newtemp = Newtemp & x
            End If
        Next i

        FnTrim = Newtemp


    End Function

    Function BuildComXML(sParam As String, target As Integer) As String

        Dim s As String
        s = "<AtXmlSignalDescription xmlns:atXml=""ATXML_TSF"">" & vbCrLf
        s = s & "<SignalAction>" & sParam & "</SignalAction>" & vbCrLf
        s = s & "<SignalResourceName>" & rName(target) & "</SignalResourceName>" & vbCrLf
        s = s & "</AtXmlSignalDescription>"
        BuildComXML = s


    End Function

    Function GetStringValue(ByVal Response As String, ByVal Attr As String) As String
        Dim sAttribute As String = ""
        Dim x1 As Integer = 0
        Dim x2 As Integer = 0
        Dim x3 As Integer = 0
        Dim s As String

        GetStringValue = "Error: not found"
        s = Response


        ' ''Sample string
        ''s = "<AtXmlResponse>"
        ''s = s & "<ReturnData>"
        ''s = s & "<ValuePair>"
        ''s = s & "<Attribute>remoteIP</Attribute>"
        ''s = s & "<Value>"
        ''s = s & "<c:Datum xsi:type=""c:string"" unit=""""><c:Value>192.168.0.1</c:Value></c:Datum>"
        ''s = s & "</Value>"
        ''s = s & "</ValuePair>"
        ''s = s & "</ReturnData>"
        ''s = s & "</AtXmlResponse>"

        ''s = s & "<AtXmlResponse>"
        ''s = s & "<ReturnData>"
        ''s = s & "<ValuePair>"
        ''s = s & "<Attribute>remotePort</Attribute>"
        ''s = s & "<Value>"
        ''s = s & "<c:Datum xsi:type=""c:integer"" unit="""" value=""21""/>"
        ''s = s & "</Value>"
        ''s = s & "</ValuePair>"
        ''s = s & "</ReturnData>"
        ''s = s & "</AtXmlResponse>"

        ''s = s & "<AtXmlResponse>"
        ''s = s & "<ReturnData>"
        ''s = s & "<ValuePair>"
        ''s = s & "<Attribute>spec</Attribute>"
        ''s = s & "<Value>"
        ''s = s & "<c:Datum xsi:type=""c:string"" unit=""""><c:Value>TCP</c:Value></c:Datum>"
        ''s = s & "</Value>"
        ''s = s & "</ValuePair>"
        ''s = s & "</ReturnData>"
        ''s = s & "</AtXmlResponse>"

        ''s = s & "<AtXmlResponse>"
        ''s = s & "<ReturnData>"
        ''s = s & "<ValuePair>"
        ''s = s & "<Attribute>localIP</Attribute>"
        ''s = s & "<Value>"
        ''s = s & "<c:Datum xsi:type=""c:string"" unit=""""><c:Value>192.168.0.1</c:Value></c:Datum>"
        ''s = s & "</Value>"
        ''s = s & "</ValuePair>"
        ''s = s & "</ReturnData>"
        ''s = s & "</AtXmlResponse>"

        ''s = s & "<AtXmlResponse>"
        ''s = s & "<ReturnData>"
        ''s = s & "<ValuePair>"
        ''s = s & "<Attribute>localSubnetMask</Attribute>"
        ''s = s & "<Value>"
        ''s = s & "<c:Datum xsi:type=""c:string"" unit=""""><c:Value>255.255.255.0</c:Value></c:Datum>"
        ''s = s & "</Value>"
        ''s = s & "</ValuePair>"
        ''s = s & "</ReturnData>"
        ''s = s & "</AtXmlResponse>"

        ''s = s & "<AtXmlResponse>"
        ''s = s & "<ReturnData>"
        ''s = s & "<ValuePair>"
        ''s = s & "<Attribute>localGateway</Attribute>"
        ''s = s & "<Value>"
        ''s = s & "<c:Datum xsi:type=""c:string"" unit=""""><c:Value>192.168.0.2</c:Value></c:Datum>"
        ''s = s & "</Value>"
        ''s = s & "</ValuePair>"
        ''s = s & "</ReturnData>"
        ''s = s & "</AtXmlResponse>"

        ''s = s & "<AtXmlResponse>"
        ''s = s & "<ReturnData>"
        ''s = s & "<ValuePair>"
        ''s = s & "<Attribute>maxTime</Attribute>"
        ''s = s & "<Value>"
        ''s = s & "<c:Datum xsi:type=""c:double"" unit="""" value=""2.000000000000E+000""/>"
        ''s = s & "</Value>"
        ''s = s & "</ValuePair>"
        ''s = s & "</ReturnData>"
        ''s = s & "</AtXmlResponse>"

        ''Attr = "remoteIP"
        ''Attr = "remotePort"
        ''Attr = "spec"
        ''Attr = "localIP"
        ''Attr = "localSubnetMask"
        ''Attr = "localGateway"
        ''Attr = "maxTime"

        x1 = InStr(s, "<Attribute>" & Attr & "</Attribute>")
        If x1 > 0 Then
            x3 = InStr(x1 + 1, s, "</AtXmlResponse>", CompareMethod.Text) ' end of Attribute
            If x3 = 0 Then x3 = Len(s) ' last one?
            x2 = InStr(x1, s, "<c:Value>") ' is a string?
            If x2 > 0 And x2 < x3 Then
                x1 = x2 + 9
                x2 = InStr(x1, s, "</c:Value>", CompareMethod.Text)
                GetStringValue = Mid(s, x1, x2 - x1)
            Else
                x2 = InStr(x1, s, "value=", CompareMethod.Text)
                If x2 > 0 And x2 < x3 Then
                    x1 = x2 + 6
                    x2 = InStr(x1, s, "/>")
                    GetStringValue = Mid(s, x1, x2 - x1)
                    If Left(GetStringValue, 1) = Chr(34) Then ' drop the left parenthesis
                        GetStringValue = Mid(GetStringValue, 2)
                    End If
                    If Right(GetStringValue, 1) = Chr(34) Then ' drop the right parenthesis
                        GetStringValue = Left(GetStringValue, Len(GetStringValue) - 1)
                    End If
                End If
            End If
        End If
    End Function

    Private Sub TerminateProcess(app_exe As String)
        Dim Process As Object
        For Each Process In GetObject("winmgmts:").ExecQuery("Select Name from Win32_Process Where Name = '" & app_exe & "'")
            Process.Terminate()
        Next
    End Sub

End Module