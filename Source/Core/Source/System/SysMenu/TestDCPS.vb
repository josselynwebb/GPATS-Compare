Public Module TestDCPS

'************************************************************
'* Nomenclature   : APS 6062 UUT Power Supplies             *
'*    DESCRIPTION:                                          *
'*     Send a command string to the power supplies          *
'*    EXAMPLE:                                              *
'*      SendDCPSCommand (3, Command$)                       *
'*    PARAMTERS:                                            *
'*     slot%     = The slot number of the supply where to   *
'*                 send the command                         *
'*     Command$  = The command string to send to the supply *
'*                                                          *
'*     NOTE: All commands must be three bytes in length     *
'*           even if one of the bytes is a null byte.       *
'*           All programmed voltages for the 40V supplies   *
'*           need to be multiplied by 100 (65V by 50) and   *
'*           converted to 12 bits.                          *
'*           All programmed currents need to be multiplied  *
'*           by 500 and converted to 12 bits.               *
'*                                                          *
'*                                                          *
'*    Commands     Description                              *
'*    1S, 00, 00   Reset Supply S(1-A)                      *
'*    2S, 4Z, ZZ   Program Supply S to ZZZ current limit    *
'*    2S, 5Z, ZZ   Program Supply S to ZZZ voltage limit    *
'*    4S, 00, 00   Execute BIT on supply S                  *
'*    2S, B0, 00   Close Output Relay on supply S           *
'*    2S, A0, 00   Open Output Relay on supply S (default)  *
'*    2S, 80, 03   Reverse Polarity on supply S             *
'*    2S, 80, 02   Normal Polarity on supply S (default)    *
'*    2S, 8C, 00   Slave Mode on supply S                   *
'*    2S, 88, 00   Master Mode on supply S (default)        *
'*    2S, 83, 00   Remote Sense on supply S                 *
'*    2S, 82, 00   Local Sense on supply S (default)        *
'*    2S, 80, 30   Constant Current Mode on supply S        *
'*    2S, 80, 20   Constant Voltage Mode on supply S (def)  *
'*                                                          *
'*    AS, 4Z, ZZ   Cal Start Location ZZZ for supply S      *
'*    CS, 4Z, ZZ   Cal Next Location ZZZ for supply S       *
'*    ES, 4x, ZZ   Cal Offset ZZ to write between Start and *
'*                 Next Locations                           *
'*    2S, 6Z, ZZ   Voltage Set command which bypasses Cal   *
'*                 Offsets - use for every voltage setting  *
'*                 during calibration to ignore past cal.   *
'*                                                          *
'*    9S, xx, xx   Cal finished for supply S                *
'*    2S, 80, 40   Place supply S into NoFault Mode         *
'*    8S, 4F, AA   Calibration Store Century Location       *
'*    8S, 6x, ZZ   Calibration Store Century ZZ             *
'*    8S, 4F, AB   Calibration Store Year Location          *
'*    8S, 6x, ZZ   Calibration Store Year ZZ                *
'*    8S, 4F, AC   Calibration Store Month Location         *
'*    8S, 4F, ZZ   Calibration Store Month ZZ               *
'*    8S, 4F, AD   Calibration Store Day Location           *
'*    8S, 4F, ZZ   Calibration Store Day ZZ                 *
'*    0S, 6F, AA   Read Cal Century - returns in byte 5     *
'*    0S, 6F, AA   Read Cal Year    - returns in byte 5     *
'*    0S, 6F, AA   Read Cal Month   - returns in byte 5     *
'*    0S, 6F, AA   Read Cal Day     - returns in byte 5     *
'*                                                          *
'*    0S, 44, 00   Status Query to supply S                 *
'*    0S, 42, 00   Measurement Query to supply S            *
'*                                                          *
'*   Status Query Byte 1  (Returned Data)                   *
'*   Bit 0 L/R     Sense Mode 0=local, 1=Remote             *
'*       1 x                                                *
'*       2 M/S     Mode 0=Master, 1=Slave                   *
'*       3 x                                                *
'*       4 O/C     Isolation Relay 0=open, 1=closed         *
'*       5 1                                                *
'*       6 NFM     1=NoFault Mode                           *
'*       7 0                                                *
'*                                                          *
'*   Status Query Byte 2 (Status Byte)                      *
'*   Bit 0 UV      Under Voltage faults (over current)      *
'*       1 STF     Self Test Failure                        *
'*       2 CC      Constant Current Mode                    *
'*       3 OC      Overcurrent Fault                        *
'*       4 OV      Overvoltage Fault                        *
'*       5 IC      Invalid Command or sequence              *
'*       6 CAL     Cal in progress                          *
'*       7 STP     Self Test Passed (result of last test)   *
'*                                                          *
'*   Status Query Byte 3 (Bit Fail Test)                    *
'*   Bit 0 2V      2V test failed                           *
'*       1 10V     10V test failed                          *
'*       2 20V     20V test failed                          *
'*       3 40V     40V test failed                          *
'*       4 65V     65V test failed                          *
'*       5 EE      EEprom test failed                       *
'*       6 x                                                *
'*       7 x                                                *
'*                                                          *
'*   Status Query Byte 4 (Rev of firmware  High.Low)        *
'*   Bit 0-2       Low                                      *
'*       4-7       High                                     *
'*                                                          *
'*   Status Query Byte 5 (EEprom Read Data)                 *
'*                                                          *
'*                                                          *
'*   Measurement Query                                      *
'*   Byte 1 5C     12 bit current reading CCC               *
'*        2 CC       (divide by 500 to get Amps)            *
'*        3 2V     12 bit voltage reading VVV               *
'*          AV       (when polarity is reversed)            *
'*        4 VV       (divide by 100 to get Volts)           *
'*        5 SB     Status Byte (same as above byte 2)       *
'*                                                          *
'*                                                          *
'*                                                          *
'************************************************************

Public Sub CommandSetCurrent(Supply%, Current2!)
'************************************************************
'*    DESCRIPTION:                                          *
'*     This Module set the current limit of a power supply  *
'*    EXAMPLE:                                              *
'*     CommandSetCurrent! 4, .5                             *
'*    PARAMETERS:                                           *
'*     Supply%   =  The supply number of which to set       *
'*     Current!  =  The current in amps for which to set    *
'*                  the supply                              *
'************************************************************
Dim B1%
Dim B2%
Dim B3%
Dim CurX&
Dim Current!

    Current! = Current2!

    'Convert To Send Format
    CurX = CInt(Current! * 500)  '2ma Resolution (1/.002=500)
    'ie, 1 amp = 1F4 (500), 5 amps = 9C4 (2500)

    ' Set Current Limit = 2s, 4z, zz
    B1% = &H20 + Supply%
    B2% = &H40 + CInt(CurX \ 256) ' note: Short division
    B3% = CInt(CurX Mod 256)

    'Send Command
    SendDCPSCommand(Supply%, Chr(B1%) & Chr(B2%) & Chr(B3%))

End Sub

Sub CommandSetOptions(Supply%, OpenRelay%, SetMaster%, SenseLocal%, ConstantVoltage%)
'************************************************************
'*    DESCRIPTION:                                          *
'*     This Module sets the optional settings of a power    *
'*     supply                                               *
'*    EXAMPLE:                                              *
'*     CommandSetOptions 4, False, True, False, False       *
'*    Note: If a +1 is passed do nothing (0)                *
'*     CommandSetOptions 4, 1, 1, True, False               *
'*                                                          *
'*    PARAMETERS:                                           *
'*     Supply%    =  The supply number of which to set      *
'*     OpenRelay% =  True to open the output relay          *
'*                   False to close the output relay        *
'*     SetMaster% =  True to set the supply to master mode  *
'*                   False to set the supply to slave mode  *
'*     SenseLocal%=  True to set to local sense             *
'*                   False to set to remote sense           *
'*     ConstantVoltage% = True for constant Voltage mode    *
'*                   False for constant current mode        *
'************************************************************

'2s,B0,00 Close Relay s
'2s,A0,00 Open Relay s
'2s,80,03 Reverse Polarity s
'2s,80,02 Normal Polarity s
'2s,8C,00 Slave s
'2s,88,00 Master s
'2s,83,00 Remote Sense s
'2s,82,00 Local Sense s
'2s,80,30 Constant Current s
'2s,80,20 Constant Voltage s
'2s,80,40 Nofault s
'2s,80,08 Disable Fail Safe s

'B1  = 2s
'      ******8*********7*********6*********5*********4**********3*********2*********1*****
'B2 =1 * change   *         * En C/O   * Closed * En M/S   * Slave   * En L/R   * Remote *
'    0 * Nochange *    0    * Disable  * Open   * Disable  * Master  * Disable  * Local  *
'      ******8*********7*********6*********5*********4**********3*********2*********1*****
'B3= 1 *          *         * En CC/CV * CCurr  * Dis FS   * Nofault * En R/N   * Revers *
'    0 *          *         * Disable  * CVolt  * FailSafe * Dis NF  * Disable  * Normal *
'      ******8*********7*********6*********5*********4**********3*********2*********1*****

Dim B1%
Dim B2%
Dim B3%

    B1% = &H20 + Supply%
    Select Case OpenRelay%
        Case True  '(-1)
            B2% = B2% + &H20 'Enable / Relay Open
        Case False '( 0)
            B2% = B2% + &H30 'Enable / Relay Closed
    End Select

    Select Case SetMaster%
        Case True  '(-1)
            B2% = B2% + &H8  'Enable / Set as Master
        Case False '( 0)
            B2% = B2% + &HC  'Enable / Set as Slave
    End Select

    Select Case SenseLocal%
        Case True  '(-1)
            B2% = B2% + 2    'Enable / Sense Local
        Case False '( 0)
            B2% = B2% + 3    'Enable / Sense Remote
    End Select

    Select Case ConstantVoltage%
      Case True  '(-1)
            B3% = B3% + &H20 'Enable / Current Limiting(Protection)
       Case False '( 0)
            B3% = B3% + &H30 'Enable / Constant Current
    End Select

    If (B2 > 0) Or (B3 > 0) Then
      B2% = B2% + &H80
    End If

    'Send Command
    SendDCPSCommand(Supply%, Chr(B1%) & Chr(B2%) & Chr(B3%))

End Sub

Public Sub CommandSetVoltage(Supply%, Volts2!)
'************************************************************
'*    DESCRIPTION:                                          *
'*     This Module set the output voltage of a power supply *
'*    EXAMPLE:                                              *
'*     CommandSetVoltage 4, .5                              *
'*    PARAMETERS:                                           *
'*     Supply%   =  The supply number of which to set       *
'*     Current!  =  The current in amps for which to set    *
'*                  the supply                              *
'*                                                          *
'*     'Set voltage command = 2s, 5z, zz                    *
'************************************************************

Dim B1%
Dim B2%
Dim B3%
Dim VoltsX&
Dim Volts!

    Volts! = Volts2!

    'Convert To Send Format
    If Supply% = 10 Then
        Volts! = Volts! * 50 '20mV Resolution (1/.02=50)
    Else
        Volts! = Volts! * 100 '10mV Resolution (1/.01=100)
    End If

    VoltsX = CInt(Volts!)

    B1% = &H20 + Supply%
    B2% = &H50 + CInt(VoltsX \ 256) ' Note: Short division
    B3% = CInt(VoltsX Mod 256)

    'Send Command
    SendDCPSCommand(Supply%, Chr(B1%) & Chr(B2%) & Chr(B3%))

End Sub

Sub CommandSupplyReset(Supply%)
'************************************************************
'*    DESCRIPTION:                                          *
'*     This Module resets a power supply                    *
'*    EXAMPLE:                                              *
'*     CommandSupplyReset 4                                 *
'*    PARAMETERS:                                           *
'*     Supply%    =  The supply number of which to reset    *
'************************************************************

Dim B1%
Dim B2%
Dim B3%

    'Send "Open All Relays" Command  2S, A0, 00   Open Output Relay on supply S (default)  *
    B1% = &H20 + Supply%
    B2% = &HA0  'was &HAA  bb
    B3% = 0     ' was &H20
    SendDCPSCommand(Supply%, Chr(B1%) & Chr(B2%) & Chr(B3%))

    'Send "Reset" Command 1S, 00, 00   Reset Supply S(1-A)                      *
    B1% = &H10 + Supply%
    B2% = 0      ' was 128  bb
    B3% = 0      ' was 128  bb
    SendDCPSCommand(Supply%, Chr(B1%) & Chr(B2%) & Chr(B3%))

End Sub

Sub SendDCPSCommand(slot%, Command$)
'************************************************************
'* Nomenclature   : APS 6062 UUT Power Supplies             *
'*    DESCRIPTION:                                          *
'*     Send a command string to the power supplies          *
'*    EXAMPLE:                                              *
'*      SendDCPSCommand (3, Command$)                       *
'*    PARAMTERS:                                            *
'*     slot%     = The slot number of the supply where to   *
'*                 send the command                         *
'*     Command$  = The command string to send to the supply *
'*                                                          *
'*     NOTE: All commands must be three bytes in length     *
'*           even if one of the bytes is a null byte.       *
'*           All programmed voltages for the 40V supplies   *
'*           need to be multiplied by 100 (65V by 50) and   *
'*           converted to 12 bits.                          *
'*           All programmed currents need to be multiplied  *
'*           by 500 and converted to 12 bits.               *
'************************************************************

Dim ErrorStatus&
Dim Nbr As Long

    If Len(Command$) <> 3 Then Exit Sub

    ErrorStatus& = atxmlDF_viWrite(PsResourceName$(slot%), 0, Command$, CLng(Len(Command$)), Nbr)

    Delay(0.5)

End Sub

Public Sub ReleaseDCPS()

Dim status As Long
Dim XmlBuf As String
Dim Response As String


    'Release DCPS1
    XmlBuf = "<AtXmlDriverFunctionCall>" & _
                "<SignalResourceName>DCPS_1</SignalResourceName>" & _
                "<DriverFunctionCall>" & _
                    "<FunctionName>aps6062_reset</FunctionName>" & _
                    "<ReturnType>RetInt32</ReturnType>" & _
                    "<Parameter   ParamNumber=""1"" ParamType=""Handle"" Value=""0""/>" & _
                "</DriverFunctionCall>" & _
                "</AtXmlDriverFunctionCall>"
     status = atxml_IssueDriverFunctionCall(XmlBuf, Response, 4096)


    'Release DCPS2
    XmlBuf = "<AtXmlDriverFunctionCall>" & _
                "<SignalResourceName>DCPS_2</SignalResourceName>" & _
                "<DriverFunctionCall>" & _
                    "<FunctionName>aps6062_reset</FunctionName>" & _
                    "<ReturnType>RetInt32</ReturnType>" & _
                    "<Parameter   ParamNumber=""1"" ParamType=""Handle"" Value=""0""/>" & _
                "</DriverFunctionCall>" & _
                "</AtXmlDriverFunctionCall>"
     status = atxml_IssueDriverFunctionCall(XmlBuf, Response, 4096)


    'Release DCPS3
    XmlBuf = "<AtXmlDriverFunctionCall>" & _
                "<SignalResourceName>DCPS_3</SignalResourceName>" & _
                "<DriverFunctionCall>" & _
                    "<FunctionName>aps6062_reset</FunctionName>" & _
                    "<ReturnType>RetInt32</ReturnType>" & _
                    "<Parameter   ParamNumber=""1"" ParamType=""Handle"" Value=""0""/>" & _
                "</DriverFunctionCall>" & _
                "</AtXmlDriverFunctionCall>"
    status = atxml_IssueDriverFunctionCall(XmlBuf, Response, 4096)


    'Release DCPS4
    XmlBuf = "<AtXmlDriverFunctionCall>" & _
                "<SignalResourceName>DCPS_4</SignalResourceName>" & _
                "<DriverFunctionCall>" & _
                    "<FunctionName>aps6062_reset</FunctionName>" & _
                    "<ReturnType>RetInt32</ReturnType>" & _
                    "<Parameter   ParamNumber=""1"" ParamType=""Handle"" Value=""0""/>" & _
                "</DriverFunctionCall>" & _
                "</AtXmlDriverFunctionCall>"
    status = atxml_IssueDriverFunctionCall(XmlBuf, Response, 4096)


    'Release DCPS5
    XmlBuf = "<AtXmlDriverFunctionCall>" & _
                "<SignalResourceName>DCPS_5</SignalResourceName>" & _
                "<DriverFunctionCall>" & _
                    "<FunctionName>aps6062_reset</FunctionName>" & _
                    "<ReturnType>RetInt32</ReturnType>" & _
                    "<Parameter   ParamNumber=""1"" ParamType=""Handle"" Value=""0""/>" & _
                "</DriverFunctionCall>" & _
                "</AtXmlDriverFunctionCall>"
    status = atxml_IssueDriverFunctionCall(XmlBuf, Response, 4096)


    'Release DCPS6
    XmlBuf = "<AtXmlDriverFunctionCall>" & _
                "<SignalResourceName>DCPS_6</SignalResourceName>" & _
                "<DriverFunctionCall>" & _
                    "<FunctionName>aps6062_reset</FunctionName>" & _
                    "<ReturnType>RetInt32</ReturnType>" & _
                    "<Parameter   ParamNumber=""1"" ParamType=""Handle"" Value=""0""/>" & _
                "</DriverFunctionCall>" & _
                "</AtXmlDriverFunctionCall>"
    status = atxml_IssueDriverFunctionCall(XmlBuf, Response, 4096)


    'Release DCPS7
    XmlBuf = "<AtXmlDriverFunctionCall>" & _
                "<SignalResourceName>DCPS_7</SignalResourceName>" & _
                "<DriverFunctionCall>" & _
                    "<FunctionName>aps6062_reset</FunctionName>" & _
                    "<ReturnType>RetInt32</ReturnType>" & _
                    "<Parameter   ParamNumber=""1"" ParamType=""Handle"" Value=""0""/>" & _
                "</DriverFunctionCall>" & _
                "</AtXmlDriverFunctionCall>"
    status = atxml_IssueDriverFunctionCall(XmlBuf, Response, 4096)


    'Release DCPS8
    XmlBuf = "<AtXmlDriverFunctionCall>" & _
                "<SignalResourceName>DCPS_8</SignalResourceName>" & _
                "<DriverFunctionCall>" & _
                    "<FunctionName>aps6062_reset</FunctionName>" & _
                    "<ReturnType>RetInt32</ReturnType>" & _
                    "<Parameter   ParamNumber=""1"" ParamType=""Handle"" Value=""0""/>" & _
                "</DriverFunctionCall>" & _
                "</AtXmlDriverFunctionCall>"
    status = atxml_IssueDriverFunctionCall(XmlBuf, Response, 4096)


    'Release DCPS9
    XmlBuf = "<AtXmlDriverFunctionCall>" & _
                "<SignalResourceName>DCPS_9</SignalResourceName>" & _
                "<DriverFunctionCall>" & _
                    "<FunctionName>aps6062_reset</FunctionName>" & _
                    "<ReturnType>RetInt32</ReturnType>" & _
                    "<Parameter   ParamNumber=""1"" ParamType=""Handle"" Value=""0""/>" & _
                "</DriverFunctionCall>" & _
                "</AtXmlDriverFunctionCall>"
    status = atxml_IssueDriverFunctionCall(XmlBuf, Response, 4096)


    'Release DCPS10
    XmlBuf = "<AtXmlDriverFunctionCall>" & _
                "<SignalResourceName>DCPS_10</SignalResourceName>" & _
                "<DriverFunctionCall>" & _
                    "<FunctionName>aps6062_reset</FunctionName>" & _
                    "<ReturnType>RetInt32</ReturnType>" & _
                    "<Parameter   ParamNumber=""1"" ParamType=""Handle"" Value=""0""/>" & _
                "</DriverFunctionCall>" & _
                "</AtXmlDriverFunctionCall>"
    status = atxml_IssueDriverFunctionCall(XmlBuf, Response, 4096)

End Sub

Sub SetVEOPower(ByVal OnOff As Integer)

        Dim CloseRelay As Integer
        Dim SetSlave As Integer
        Dim SenseRemote As Integer
        Dim CurrentConstant As Integer
        Dim OpenRelay As Integer
        Dim SetMaster As Integer
        Dim SenseLocal As Integer
        Dim CurrentLimit As Integer
        Dim x As DialogResult

        'Define command options
        CloseRelay = 0
        SetSlave = 0
        SenseRemote = 0
        CurrentConstant = 0

        OpenRelay = -1
        SetMaster = -1
        SenseLocal = -1
        CurrentLimit = -1 '

        If OnOff = True Then ' turn power on

            ExternalPower = False
            If ExternalPower = False Then
                x = MsgBox("Verify that everything is ready to power the VEO2 unit." & vbCrLf & vbCrLf & "Are you ready to turn on VEO2 power?", MsgBoxStyle.YesNo)
                If x <> DialogResult.Yes Then
                    VEO2PowerOn = False
                    Exit Sub
                End If

                frmDialog.lblDialog.Text = "Resetting VEO2 Power Supplies..."
                frmDialog.Show()
                Application.DoEvents()
                frmDialog.Refresh()

                ' reset supply
                CommandSupplyReset(1) ' Reset master supply
                CommandSupplyReset(2) ' Reset slave supply
                CommandSupplyReset(3) ' Reset +15v supply
                Delay(1)

                'Turn on 15v
                CommandSetOptions(3, CloseRelay, SetMaster, SenseLocal, CurrentLimit) ' close output relay
                CommandSetCurrent(3, 5) 'set current limit to 5 Amp
                CommandSetVoltage(3, 15) 'set voltage to 15
                Delay(1)

                'setup slave
                CommandSetOptions(2, 1, SetSlave, 1, 1) ' set slave mode
                Delay(0.3)
                CommandSetOptions(2, CloseRelay, 1, 1, 1) ' close output relay
                Delay(0.3)

                'Turn on master (and slave)
                CommandSetOptions(1, 1, SetMaster, SenseLocal, CurrentLimit)
                Delay(0.3)
                CommandSetOptions(1, CloseRelay, 1, 1, 1) ' close output relay
                Delay(0.3)
                CommandSetCurrent(1, 5) 'set current limit to 5 Amp
                CommandSetVoltage(1, 28) 'set voltage to 28
                Delay(1)
                VEO2PowerOn = True

                frmDialog.Close()

                frmEoPwrUpStatus.ShowDialog()

            End If

        Else            ' turn power off

            'disconnect two supplies
            CommandSetVoltage(1, 0) 'set voltage to 0
            CommandSetCurrent(1, 0) 'set current limit to 0 amp
            CommandSetVoltage(3, 0) 'set voltage to 0
            CommandSetCurrent(3, 0) 'set current limit to 0 amp
            CommandSetOptions(1, OpenRelay, SetMaster, SenseLocal, CurrentLimit)
            CommandSetOptions(2, OpenRelay, SetMaster, SenseLocal, CurrentLimit)
            CommandSetOptions(3, OpenRelay, SetMaster, SenseLocal, CurrentLimit)
            Delay(1)
            VEO2PowerOn = False

        End If

    End Sub

Function FnSAIFinstalled(Expect As Short) As Short

  Call ReadReceiverSwitches()

  If Expect = SAIFinstalled Then ' if the switch is what we expected then exit
    FnSAIFinstalled = SAIFinstalled ' true or false
    Exit Function
  End If

End Function

Sub ReadReceiverSwitches()

Dim status%
Dim ActionByteValue%
Dim ActChassisAddress%
Dim ActVolt28OK%
Dim ActProbeEvent%
Dim ActReceiverEvent%
Dim nSystErr As Long

    '***Get Status Byte***
    nSystErr = viReadSTB(FPUHandle, status)
    Application.DoEvents()

    '***Evaluate Status Byte***
    If ((status% And &HF0) = &HB0) Then
        '-------------------------------------------------------------------------------
        '- Action Status Byte ----------------------------------------------------------
        '-        B7 |  B6 |        B5 |           B4 | B3 |     B2 |     B1 |      B0 -
        '- Data Dump | RQS | Ret Error | Module Fault | A3 | A2/28V | A1/PRB | A0/RCVR -
        '-------------------------------------------------------------------------------
        'Data Dump 1000 xxxx
        'Query Failed 0010 xxxx
        'Mod Failed 0001 ADDR
        'Action Byte 1011 xABC where A=28V, B=PRB, C=RCVR
        'ICPU Response 0011 xxxx
        'Module Response 0100 ADDR
        'Generally 80H = Data Dump
        '          BXH = Action Byte
        '-------------------------------------------------------------------------------
        ActionByteValue% = Val(Str$(status%))
        ActChassisAddress% = ActionByteValue% And 8
        ActVolt28OK% = ActionByteValue% And 4
        ActProbeEvent% = ActionByteValue% And 2
        ActReceiverEvent% = ActionByteValue% And 1

'        'Check ITA/SAIF Receiver Switch Power Switch
        If ActReceiverEvent% = 1 Then 'RCVR Handle Open
            SAIFinstalled = False

        Else 'RCVR Handle Closed
            SAIFinstalled = True
        End If

    End If
    Application.DoEvents()

End Sub

End Module


