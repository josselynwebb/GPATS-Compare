'Option Strict Off
'Option Explicit On

Imports System

Public Module DCPS


	'=========================================================
    Public Sub DcpsSetCurrent(ByVal Supply As Short, ByVal current As Single)
        'DESCRIPTION:
        '   Sets the current limit value of a DC Supply
        'PARAMETERS:
        '   Supply%:    Number of supply to set (1-10)
        '   Current!:   The Current limit value
        'GLOBAL VARIABLES MODIFIED:
        '   none

        Dim B1 As Short, B2 As Short, B3 As Short

        'Convert to Integer DAC code
        current *= 500 '20mA Resolution

        B1 = &H20+ObjectToDouble(Supply)
        B2 = &H40+(current\&H100)
        B3 = current Mod &H100

        'Send Command
        SendDCPSCommand(Supply, Convert.ToString(Chr(B1)) & Convert.ToString(Chr(B2)) & Convert.ToString(Chr(B3)))

    End Sub
    
    
    
    
    Sub DcpsSetOptions(ByVal Supply As Short, ByVal OpenRelay As Short, ByVal SetMaster As Short, ByVal SenseLocal As Short, ByVal ConstantCurrent As Short)
        'DESCRIPTION:
        '   Sets one or more parameters of a DC Supply
        'PARAMETERS:
        '   Supply%:    Number of supply to set (1-10)
        '   NOTE:   For the following parameters,
        '           True performs its name and False performs the opposite
        '   OpenRelay%:         Sets Output Relay Open/Closed
        '   SetMaster%:         Sets Master/Slave control mode
        '   SenseLocal%:        Sets Local/Remote Sensing
        '   ConstantCurrent%:   Sets Constant Current/Voltage mode
        'GLOBAL VARIABLES MODIFIED:
        '   none

        Dim B1 As Short, B2 As Short, B3 As Short

        B1 = &H20+ObjectToDouble(Supply)

        Select Case OpenRelay
            Case True:
                '(-1)
                B2 += 32 'Enable / Relay Open
            Case False:
                '( 0)
                B2 += 32 'Enable
                B2 += 16 'Relay Closed
        End Select
        Select Case SetMaster
            Case True:
                '(-1)
                B2 += 8 'Enable /Set as Master
            Case False:
                '( 0)
                B2 += 8 'Enable
                B2 += 4 'Set as Slave
        End Select
        Select Case SenseLocal
            Case True:
                '(-1)
                B2 += 2 'Enable / Sense Local
            Case False:
                '( 0)
                B2 += 2 'Enable
                B2 += 1 'Sense Remote
        End Select
        Select Case ConstantCurrent
            Case True:
                '(-1)
                B2 += 128
                B3 += 32 'Enable
                B3 += 16 'Constant Current
            Case False:
                '( 0)
                B2 += 128
                B3 += 32 'Enable / Constant Voltage
        End Select

        'Send Command
        SendDCPSCommand(Supply, Convert.ToString(Chr(B1)) & Convert.ToString(Chr(B2)) & Convert.ToString(Chr(B3)))

    End Sub
    Public Sub DcpsSetPolarity(ByVal Supply As Short, ByVal polarity As String)
        '************************************************************
        '* Written By/Date     : Grady Johnson 11/2/98              *
        '*    DESCRIPTION:                                          *
        '*     This Module set the output polarity of a power supply*
        '*    EXAMPLE:                                              *
        '*     DcpsSetPolarity 7, "-"                               *
        '*    PARAMETERS:                                           *
        '*     Supply%   =  The supply number of which to set       *
        '*     Polarity%  =  The polarity "-" or "+"                *
        '* Procedure added per ECO-3047-078 for polarity testing.   *
        '************************************************************

        Dim Byte3 As Short

        If Convert.ToString(polarity)="-" Then
            Byte3 = &H3 'Negative
        Else
            Byte3 = &H2 'Default to Positive
        End If

        'Send Command
        SendDCPSCommand(Supply, Convert.ToString(Chr(32+Supply)) & Convert.ToString(Chr(&H80)) & Convert.ToString(Chr(Byte3))) 'Set polarity

    End Sub
    Sub SendDCPSCommand(ByVal slot As Short, ByVal Command As String)
        '************************************************************
        '* ManTech Test Systems Software Sub                        *
        '************************************************************
        '* Nomenclature   : APS 6062 UUT Power Supplies             *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     Send a command string to the power supplies          *
        '*    EXAMPLE:                                              *
        '*      SendDCPSCommand (3, Command$                        *
        '*    PARAMTERS:                                            *
        '*     slot%     = The slot number of the supply where to   *
        '*                 send the command                         *
        '*     Command$  = The command string to send to the supply *
        '************************************************************
        Dim ErrorStatus As Integer

        Dim retCount As Integer
        If Convert.ToString(Command).Length<>3 Then Exit Sub
        ErrorStatus = atxmlDF_viWrite(SignalResourceNameArray(slot), 0, Command, CLng(Convert.ToString(Command).Length), retCount)

    End Sub

    Public Sub DcpsSetVoltage(ByVal Supply As Short, ByVal Volts As Single)
        'DESCRIPTION:
        '   Sets the voltage level of a DC Supply
        'PARAMETERS:
        '   Supply%:    Number of supply to set (1-10)
        '   Volts!:     The Voltage level value
        'GLOBAL VARIABLES MODIFIED:
        '   none

        Dim B1 As Short, B2 As Short, B3 As Short

        'Convert to Integer DAC code
        If ObjectToDouble(Supply)=10 Then
            Volts *= 50 '20mV Resolution
        Else
            Volts *= 100 '10mV Resolution
        End If

        B1 = &H20+ObjectToDouble(Supply)
        B2 = &H50+(Volts\&H100)
        B3 = Volts Mod &H100

        'Send Command
        SendDCPSCommand(Supply, Convert.ToString(Chr(B1)) & Convert.ToString(Chr(B2)) & Convert.ToString(Chr(B3)))

    End Sub
    Public Sub ReleaseDCPS()

        Dim status As Integer
        Dim XmlBuf As String
        Dim Allocation As String
        Dim Response As String = Space(4096)


        'Release DCPS1
        XmlBuf = "<AtXmlDriverFunctionCall>" & "<SignalResourceName>DCPS_1</SignalResourceName>" & "<DriverFunctionCall>" & "<FunctionName>aps6062_reset</FunctionName>" & "<ReturnType>RetInt32</ReturnType>" & "<Parameter   ParamNumber=""1"" ParamType=""Handle"" Value=""0""/>" & "</DriverFunctionCall>" & "</AtXmlDriverFunctionCall>"
        status = atxml_IssueDriverFunctionCall(XmlBuf, Response, 4096)


        'Release DCPS2
        XmlBuf = "<AtXmlDriverFunctionCall>" & "<SignalResourceName>DCPS_2</SignalResourceName>" & "<DriverFunctionCall>" & "<FunctionName>aps6062_reset</FunctionName>" & "<ReturnType>RetInt32</ReturnType>" & "<Parameter   ParamNumber=""1"" ParamType=""Handle"" Value=""0""/>" & "</DriverFunctionCall>" & "</AtXmlDriverFunctionCall>"
        status = atxml_IssueDriverFunctionCall(XmlBuf, Response, 4096)


        'Release DCPS3
        XmlBuf = "<AtXmlDriverFunctionCall>" & "<SignalResourceName>DCPS_3</SignalResourceName>" & "<DriverFunctionCall>" & "<FunctionName>aps6062_reset</FunctionName>" & "<ReturnType>RetInt32</ReturnType>" & "<Parameter   ParamNumber=""1"" ParamType=""Handle"" Value=""0""/>" & "</DriverFunctionCall>" & "</AtXmlDriverFunctionCall>"
        status = atxml_IssueDriverFunctionCall(XmlBuf, Response, 4096)


        'Release DCPS4
        XmlBuf = "<AtXmlDriverFunctionCall>" & "<SignalResourceName>DCPS_4</SignalResourceName>" & "<DriverFunctionCall>" & "<FunctionName>aps6062_reset</FunctionName>" & "<ReturnType>RetInt32</ReturnType>" & "<Parameter   ParamNumber=""1"" ParamType=""Handle"" Value=""0""/>" & "</DriverFunctionCall>" & "</AtXmlDriverFunctionCall>"
        status = atxml_IssueDriverFunctionCall(XmlBuf, Response, 4096)


        'Release DCPS5
        XmlBuf = "<AtXmlDriverFunctionCall>" & "<SignalResourceName>DCPS_5</SignalResourceName>" & "<DriverFunctionCall>" & "<FunctionName>aps6062_reset</FunctionName>" & "<ReturnType>RetInt32</ReturnType>" & "<Parameter   ParamNumber=""1"" ParamType=""Handle"" Value=""0""/>" & "</DriverFunctionCall>" & "</AtXmlDriverFunctionCall>"
        status = atxml_IssueDriverFunctionCall(XmlBuf, Response, 4096)


        'Release DCPS6
        XmlBuf = "<AtXmlDriverFunctionCall>" & "<SignalResourceName>DCPS_6</SignalResourceName>" & "<DriverFunctionCall>" & "<FunctionName>aps6062_reset</FunctionName>" & "<ReturnType>RetInt32</ReturnType>" & "<Parameter   ParamNumber=""1"" ParamType=""Handle"" Value=""0""/>" & "</DriverFunctionCall>" & "</AtXmlDriverFunctionCall>"
        status = atxml_IssueDriverFunctionCall(XmlBuf, Response, 4096)


        'Release DCPS7
        XmlBuf = "<AtXmlDriverFunctionCall>" & "<SignalResourceName>DCPS_7</SignalResourceName>" & "<DriverFunctionCall>" & "<FunctionName>aps6062_reset</FunctionName>" & "<ReturnType>RetInt32</ReturnType>" & "<Parameter   ParamNumber=""1"" ParamType=""Handle"" Value=""0""/>" & "</DriverFunctionCall>" & "</AtXmlDriverFunctionCall>"
        status = atxml_IssueDriverFunctionCall(XmlBuf, Response, 4096)


        'Release DCPS8
        XmlBuf = "<AtXmlDriverFunctionCall>" & "<SignalResourceName>DCPS_8</SignalResourceName>" & "<DriverFunctionCall>" & "<FunctionName>aps6062_reset</FunctionName>" & "<ReturnType>RetInt32</ReturnType>" & "<Parameter   ParamNumber=""1"" ParamType=""Handle"" Value=""0""/>" & "</DriverFunctionCall>" & "</AtXmlDriverFunctionCall>"
        status = atxml_IssueDriverFunctionCall(XmlBuf, Response, 4096)


        'Release DCPS9
        XmlBuf = "<AtXmlDriverFunctionCall>" & "<SignalResourceName>DCPS_9</SignalResourceName>" & "<DriverFunctionCall>" & "<FunctionName>aps6062_reset</FunctionName>" & "<ReturnType>RetInt32</ReturnType>" & "<Parameter   ParamNumber=""1"" ParamType=""Handle"" Value=""0""/>" & "</DriverFunctionCall>" & "</AtXmlDriverFunctionCall>"
        status = atxml_IssueDriverFunctionCall(XmlBuf, Response, 4096)


        'Release DCPS10
        XmlBuf = "<AtXmlDriverFunctionCall>" & "<SignalResourceName>DCPS_10</SignalResourceName>" & "<DriverFunctionCall>" & "<FunctionName>aps6062_reset</FunctionName>" & "<ReturnType>RetInt32</ReturnType>" & "<Parameter   ParamNumber=""1"" ParamType=""Handle"" Value=""0""/>" & "</DriverFunctionCall>" & "</AtXmlDriverFunctionCall>"
        status = atxml_IssueDriverFunctionCall(XmlBuf, Response, 4096)

    End Sub


End Module