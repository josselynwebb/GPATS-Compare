Option Strict Off
Option Explicit On
<System.Runtime.InteropServices.ProgId("cDCPS_NET.cDCPS")> Public Class cDCPS
    '**************************************************************
    '* Third Echelon Test Set (TETS) Software Module              *
    '*                                                            *
    '* Nomenclature   : DC Power Supply Utilities                 *
    '* Written By     : G. Johnson                                *
    '* Purpose        : This Clase module contains code for the   *
    '*                  General Power Supply Commands             *
    '**************************************************************

    'Programming Bytes
    Dim B1(10) As Short
    Dim B2(10) As Short
    Dim B3(10) As Short

    ''' <summary>
    ''' This programs supply Output Relay
    ''' </summary>
    ''' <param name="nSupply">The supply number </param>
    ''' <param name="bState">True/False</param>
    ''' <remarks></remarks>
    Public Sub CloseOutputRelay(ByRef nSupply As Integer, ByRef bState As Boolean)
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        gFrmMain.txtInstrument.Text = "DCPS " & CStr(nSupply)
        gFrmMain.txtCommand.Text = "CloseOutputRelay"

        'Check Supply number and voltage range
        If nSupply > 10 Or nSupply < 1 Then
            Echo("DCPS PROGRAMMING ERROR:  Command cmdDCPS.SetPolarityNegative nSupply argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If

        B2(nSupply) = 0
        If bState = True Then
            B2(nSupply) = B2(nSupply) Or &HB0 'Enable / Relay Closed
        Else
            B2(nSupply) = B2(nSupply) Or &HA0 'Enable / Relay Open
        End If

        'Send Command
        If Not bSimulation Then
            SendDCPSCommand(nSupply, Chr(B1(nSupply)) & Chr(B2(nSupply)) & Chr(B3(nSupply)))
        End If

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON)
    End Sub

    ''' <summary>
    ''' This Module Takes A Current Measurement From the internal DMM of a power supply
    ''' </summary>
    ''' <param name="nSupply">The supply number from which to measure</param>
    ''' <returns>The measured current in amps</returns>
    ''' <remarks></remarks>
    Public Function dMeasAmps(ByRef nSupply As Integer) As Double
        Dim Byte2, Byte1, Byte3 As Short
        Dim D1 As Double
        Dim D2 As Double
        Dim Data As String
        Dim ReadBufferV As String
        Dim ReadBuffer As String = Space(255)
        Dim ErrorStatus As Integer
        Dim ReadBufferA As String = ""

        If UserEvent = ABORT_BUTTON Then
            Err.Raise(USER_EVENT + ABORT_BUTTON)
            dMeasAmps = 0.0
            Exit Function
        End If
        gFrmMain.txtInstrument.Text = "DCPS " & CStr(nSupply)
        gFrmMain.txtCommand.Text = "dMeasAmps"

        'Error check Supply
        If nSupply > 10 Or nSupply < 1 Then
            Echo("DCPS PROGRAMMING ERROR:  Command cmdDCPS.dMeasAmps nSupply argument out of range.")
            dMeasAmps = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1000)
            Exit Function
        End If

        If bSimulation Then
            dMeasAmps = CDbl(InputBox("Command cmdDCPS.dMeasAmps performed." & vbCrLf & "Enter DCPS Amps Value:", "SIMULATION MODE"))
            gFrmMain.txtMeasured.Text = CStr(dMeasAmps)
            Exit Function
        End If

        Byte1 = nSupply
        Byte2 = &H42 ' Measurement query to S
        Byte3 = 0 ' 'bbbb was 128
        'Send Command
        SendDCPSCommand(nSupply, Chr(Byte1) & Chr(Byte2) & Chr(Byte3))
        'Read Sense Measurement
        'bbbb   ErrorStatus = aps6062_readInstrData(nSupplyHandle(nSupply), numberBytesToRead:=255, ReadBuffer:=ReadBuffer, numBytesRead:=Nbr)
        ErrorStatus = ReadDCPSCommand(nSupply, ReadBuffer) ' get 5 bytes

        If UserEvent = ABORT_BUTTON Then
            Err.Raise(USER_EVENT + ABORT_BUTTON)
            dMeasAmps = 0.0
            Exit Function
        End If
        If ErrorStatus <> 0 Then Err.Raise(ErrorStatus)

        Data = ReadBuffer
        If Asc(Mid(Data, 1, 1)) > 64 Then
            D1 = (Asc(Mid(Data, 3, 1))) And 15
            D1 = D1 * 256
            D2 = (Asc(Mid(Data, 4, 1)))
            ReadBufferV = CStr(Val(CStr((D1 + D2) / 100.0)))
            D1 = (Asc(Mid(Data, 1, 1))) And 15
            D1 = D1 * 256
            D2 = (Asc(Mid(Data, 2, 1)))
            ReadBufferA = CStr(Val(CStr((D1 + D2) / 500.0)))


            '65 Volt Supply Re-Scale Voltage Here
            If nSupply = 10 Then
                D1 = (Asc(Mid(Data, 3, 1))) And 15
                D1 = D1 * 256
                D2 = (Asc(Mid(Data, 4, 1)))
                ReadBufferV = CStr(Val(CStr((D1 + D2) / 50.0)))
            End If
            dMeasAmps = Val(ReadBufferA)
        Else
            dMeasAmps = 0.0
        End If
        gFrmMain.txtMeasured.Text = CStr(Val(ReadBufferA))
        System.Windows.Forms.Application.DoEvents()
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON)
    End Function

    ''' <summary>
    ''' This Module Takes A Voltage Measurement From the internal DMM of a power supply 
    ''' </summary>
    ''' <param name="nSupply">The supply number from which to measure</param>
    ''' <returns>The measured voltage in volts </returns>
    ''' <remarks></remarks>
    Public Function dMeasVoltage(ByRef nSupply As Integer) As Double
        Dim Byte2, Byte1, Byte3 As Short
        Dim D1 As Double
        Dim D2 As Double
        Dim Data As String
        Dim ReadBuffer As String = Space(255)
        Dim ReadBufferA As String
        Dim ReadBufferV As String
        Dim ErrorStatus As Integer

        dMeasVoltage = 0.0
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        gFrmMain.txtInstrument.Text = "DCPS " & CStr(nSupply)
        gFrmMain.txtCommand.Text = "dMeasVoltage"
        'Error check Supply
        If nSupply > 10 Or nSupply < 1 Then
            Echo("DCPS PROGRAMMING ERROR:  Command cmdDCPS.dMeasVoltage nSupply argument out of range.")
            dMeasVoltage = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1000)
            Exit Function
        End If

        If bSimulation Then
            dMeasVoltage = CDbl(InputBox("Command cmdDCPS.dMeasVoltage performed." & vbCrLf & "Enter DCPS Voltage Value:", "SIMULATION MODE"))
            gFrmMain.txtMeasured.Text = CStr(dMeasVoltage)
            Exit Function
        End If

        'Program Query Supply For Sense Measurement
        Byte1 = nSupply
        Byte2 = &H42 ' 64 + 2
        Byte3 = &H0 'bbbb was 128

        'Send Command
        SendDCPSCommand(nSupply, Chr(Byte1) & Chr(Byte2) & Chr(Byte3))

        'Read Sense Measurement
        ErrorStatus = ReadDCPSCommand(nSupply, ReadBuffer) ' get 5 bytes

        If UserEvent = ABORT_BUTTON Then
            Err.Raise(USER_EVENT + ABORT_BUTTON)
            dMeasVoltage = 0.0
            Exit Function
        End If
        If ErrorStatus <> 0 Then Err.Raise(ErrorStatus)
        'Voltage Measurement
        'only update display if valid data is returned (not zeros)
        Data = ReadBuffer
        If Asc(Mid(Data, 1, 1)) > 64 Then
            D1 = (Asc(Mid(Data, 3, 1))) And 15
            D1 = D1 * 256
            D2 = (Asc(Mid(Data, 4, 1)))
            ReadBufferV = CStr(Val(CStr((D1 + D2) / 100.0)))
            'Current Measurement
            D1 = (Asc(Mid(Data, 1, 1))) And 15
            D1 = D1 * 256
            D2 = (Asc(Mid(Data, 2, 1)))
            ReadBufferA = CStr(Val(CStr((D1 + D2) / 500.0)))

            '65 Volt Supply Re-Scale Voltage Here
            If nSupply = 10 Then
                D1 = (Asc(Mid(Data, 3, 1))) And 15
                D1 = D1 * 256
                D2 = (Asc(Mid(Data, 4, 1)))
                ReadBufferV = CStr(Val(CStr((D1 + D2) / 50.0)))
            End If
            dMeasVoltage = Val(ReadBufferV)
        End If

        'Determine Polarity
        'Program Query Supply For Status Query
        Byte1 = nSupply
        Byte2 = &H44
        Byte3 = 0
        'Send Command
        SendDCPSCommand(nSupply, Chr(Byte1) & Chr(Byte2) & Chr(Byte3))
        'Read Sense Measurement
        ErrorStatus = ReadDCPSCommand(nSupply, ReadBuffer) ' get 5 bytes

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        If ErrorStatus <> 0 Then Err.Raise(ErrorStatus)
        'Voltage Measurement
        'only update display if valid data is returned (not zeros)
        Data = ReadBuffer
        If (Asc(Data) And 2) = 2 Then
            dMeasVoltage = -dMeasVoltage
        End If

        gFrmMain.txtMeasured.Text = CStr(dMeasVoltage)
        System.Windows.Forms.Application.DoEvents()
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON)
    End Function

    ''' <summary>
    ''' This Module programs the power supplies
    ''' </summary>
    ''' <param name="nSupply">The supply number of which to set</param>
    ''' <param name="dVoltage">Voltage Value and/or Limit</param>
    ''' <param name="dAmps">Current Value and/or Limit</param>
    ''' <param name="bNegativePolarity">Set True or False. Default False</param>
    ''' <param name="bSlaveMode">Set True or False. Default False</param>
    ''' <param name="bConstantCurrentMode">Set True for Constant current mode. Default False</param>
    ''' <param name="bRemoteSense">Set True or False.  Default False</param>
    ''' <param name="bCloseOutput">Set True or False.  Default True</param>
    ''' <remarks></remarks>
    Public Sub PowerOn(ByRef nSupply As Integer, ByRef dVoltage As Double, ByRef dAmps As Double,
                       Optional ByRef bNegativePolarity As Boolean = False, Optional ByRef bSlaveMode As Boolean = False,
                       Optional ByRef bConstantCurrentMode As Boolean = False, Optional ByRef bRemoteSense As Boolean = False,
                       Optional ByRef bCloseOutput As Boolean = True)
        Dim ReadBuffer As String = Space(255)

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        gFrmMain.txtInstrument.Text = "DCPS " & CStr(nSupply)
        gFrmMain.txtCommand.Text = "PowerOn"
        'Error check Supply, Voltage and Current settings
        If nSupply > 10 Or nSupply < 1 Then
            Echo("DCPS PROGRAMMING ERROR:  Command cmdDCPS.PowerOn nSupply argument out of range.")
            Err.Raise(10000)
            Exit Sub
        End If
        If nSupply <= 9 And System.Math.Abs(dVoltage) > 45 Then
            Echo("DCPS PROGRAMMING ERROR:  Command cmdDCPS.PowerOn dVoltage argument out of range.")
            Err.Raise(-1001)
            Exit Sub
        End If
        If nSupply = 10 And System.Math.Abs(dVoltage) > 65 Then
            Echo("DCPS PROGRAMMING ERROR:  Command cmdDCPS.PowerOn dVoltage argument out of range.")
            Err.Raise(-1002)
            Exit Sub
        End If
        If dAmps > 5 Then
            Echo("DCPS PROGRAMMING ERROR:  Command cmdDCPS.PowerOn dCurrent argument out of range.")
            Err.Raise(-1003)
            Exit Sub
        End If
        If (nSupply = 1 Or nSupply = 10) And bSlaveMode Then
            Echo("DCPS PROGRAMMING ERROR:  Command cmdDCPS.PowerOn bSlaveMode argument out of range.")
            Err.Raise(-1004)
            Exit Sub
        End If

        'Init Supply
        If Not bSimulation Then
            SendDCPSCommand(nSupply, Chr(B1(nSupply)) & Chr(&HAA) & Chr(&H22))
            If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
            Delay(0.3)
        Else
            Exit Sub
        End If

        B2(nSupply) = &HA0 'bbbb relay open
        B3(nSupply) = &H0 'bbbb

        'Set Byte 3 Slave Master option
        Select Case bSlaveMode 'Hex 8C Slave, Hex 88 Master
            Case False
                B2(nSupply) = B2(nSupply) Or &H88 'Enable /Set as Master
            Case True
                B2(nSupply) = B2(nSupply) Or &H8C 'Set as Slave and enable
                SendDCPSCommand(nSupply, Chr(B1(nSupply)) & Chr(&H8C) & Chr(0))
                If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
                Delay(0.3)
        End Select

        'Set Byte 3 Polarity options
        If dVoltage < 0 Then bNegativePolarity = True
        Select Case bNegativePolarity 'Hex 03 Reverse Polarity, Hex 02 Normal Polarity
            Case False
                B3(nSupply) = B3(nSupply) Or &H2 'Set Normal Polarity
            Case True
                B3(nSupply) = B3(nSupply) Or &H3 'Set Negative Polarity
                SendDCPSCommand(nSupply, Chr(B1(nSupply)) & Chr(&H80) & Chr(&H3))
                If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
                Delay(0.3)
        End Select

        'Program Supply Options for Constant Current Mode
        Select Case bConstantCurrentMode 'Hex 30 Constant Current, Hex 20 Constant Voltage
            Case False
                B3(nSupply) = B3(nSupply) Or &HA0 'Set Constant Voltage Mode
            Case True
                B3(nSupply) = B3(nSupply) Or &HB0 'Set Constant Current Mode
                SendDCPSCommand(nSupply, Chr(B1(nSupply)) & Chr(&H80) & Chr(&HB0))
                If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
                Delay(0.3)
        End Select

        'Close Relay Now
        If bCloseOutput Then 'Hex B0 Relay Closed, A0 Relay Open
            Delay(0.5)
            B2(nSupply) = B2(nSupply) Or &HB0 'Enable / Relay Closed
            SendDCPSCommand(nSupply, Chr(B1(nSupply)) & Chr(&HB0) & Chr(0))
            If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        End If

        'Set remote sense last
        Select Case bRemoteSense 'Hex 83 Remote Sense, Hex 82 Local Sense
            Case False
                B2(nSupply) = B2(nSupply) Or &H82 'Enable / Sense Local
            Case True
                B2(nSupply) = B2(nSupply) Or &H83 'Enable/ Sense Remote
                Delay(0.5)
                SendDCPSCommand(nSupply, Chr(B1(nSupply)) & Chr(&H83) & Chr(0))
        End Select

        'Program Current Setting
        Delay(0.2)
        SetAmps(nSupply, dAmps)
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub

        'Program Voltage Setting
        SetVoltage(nSupply, System.Math.Abs(dVoltage))
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON)
    End Sub

    ''' <summary>
    ''' Resets The Hardware and Software To It's Default Power-On State 
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ResetAll()
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        gFrmMain.txtInstrument.Text = "DCPS 1 - 10"
        gFrmMain.txtCommand.Text = "ResetAll"

        If Not bSimulation Then
            iInitDCPS(bReset:=True)
            'Insure no supplies are slaved
            '        For nSupplyIndex = 9 To 1 Step -1
            '            SetVoltage nSupplyIndex, 0
            '            SetAmps nSupplyIndex, 0.075
            '            Delay 0.3
            '            If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
            '        Next nSupplyIndex
            '
            '        For nSupplyIndex = 10 To 1 Step -1
            '            SendDCPSCommand nSupplyIndex, Chr(&H10 + nSupplyIndex) & Chr(&H0) & Chr(&H0)
            '            SendDCPSCommand nSupplyIndex, Chr(B1(nSupplyIndex)) & Chr(&HAA) & Chr(&H22)
            '            If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
            '            B2(nSupplyIndex) = &HAA
            '            B3(nSupplyIndex) = &H22
            '            Delay 0.3
            '        Next nSupplyIndex
        End If
    End Sub

    ''' <summary>
    ''' This resets a specific Supply
    ''' </summary>
    ''' <param name="nSupply">The supply number</param>
    ''' <remarks></remarks>
    Public Sub ResetSupply(ByRef nSupply As Integer)
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        gFrmMain.txtInstrument.Text = "DCPS " & CStr(nSupply)
        gFrmMain.txtCommand.Text = "ResetSupply"

        'Check Supply number
        If nSupply > 10 Or nSupply < 1 Then
            Echo("DCPS PROGRAMMING ERROR:  Command cmdDCPS.ResetSupply nSupply argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If

        If Not bSimulation Then
            SetVoltage(nSupply, 0.0)
            SetAmps(nSupply, 0.075)
            SendDCPSCommand(nSupply, Chr(&H10 + nSupply) & Chr(&H0) & Chr(&H0))
            If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
            'Enable Open Relay, Master, Constant Voltage, Normal Polarity
            SendDCPSCommand(nSupply, Chr(B1(nSupply)) & Chr(&HAA) & Chr(&H22))
            B2(nSupply) = &HAA
            B3(nSupply) = &H22
            Delay(0.8)
        End If
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON)
    End Sub

    ''' <summary>
    ''' This programs current on a specific supply
    ''' </summary>
    ''' <param name="nSupply">The supply number</param>
    ''' <param name="dAmps"></param>
    ''' <remarks></remarks>Current value
    Public Sub SetAmps(ByRef nSupply As Integer, ByRef dAmps As Double)
        Dim TempByte2, TempByte3 As Short

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        gFrmMain.txtInstrument.Text = "DCPS " & CStr(nSupply)
        gFrmMain.txtCommand.Text = "SetAmps"

        'Check Supply number and current range
        If nSupply > 10 Or nSupply < 1 Then
            Echo("DCPS PROGRAMMING ERROR:  Command cmdDCPS.SetAmps nSupply argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If
        If dAmps > 5 Then
            Echo("DCPS PROGRAMMING ERROR:  Command cmdDCPS.SetAmps dCurrent argument out of range.")
            Err.Raise(-1003)
            Exit Sub
        End If

        'Convert to Integer DAC code
        dAmps = dAmps * 500 '20mA Resolution

        'Store Current values
        TempByte2 = B2(nSupply)
        TempByte3 = B3(nSupply)

        B2(nSupply) = &H40 + (dAmps \ &H100)
        B3(nSupply) = dAmps Mod &H100

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        'Send Command
        If Not bSimulation Then
            SendDCPSCommand(nSupply, Chr(B1(nSupply)) & Chr(B2(nSupply)) & Chr(B3(nSupply)))
        End If

        'Restore values
        B2(nSupply) = TempByte2
        B3(nSupply) = TempByte3
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON)

        Delay(0.2)
    End Sub

    ''' <summary>
    ''' This programs supply Constant Current Mode
    ''' </summary>
    ''' <param name="nSupply">The supply number</param>
    ''' <param name="bState">True/False</param>
    ''' <remarks></remarks>
    Public Sub ConstantCurrent(ByRef nSupply As Integer, ByRef bState As Boolean)
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        gFrmMain.txtInstrument.Text = "DCPS " & CStr(nSupply)
        gFrmMain.txtCommand.Text = "ConstantCurrent"

        'Check Supply number and voltage range
        If nSupply > 10 Or nSupply < 1 Then
            Echo("DCPS PROGRAMMING ERROR:  Command cmdDCPS.SetPolarityNegative nSupply argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If
        B2(nSupply) = &H80 'bbbb
        B3(nSupply) = 0 'bbbb
        If bState = True Then
            B3(nSupply) = B3(nSupply) Or &H20 'Set Constant Voltage Mode
        Else
            B3(nSupply) = B3(nSupply) Or &H30 'Set Constant Current Mode
        End If

        'Send Command
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        If Not bSimulation Then
            SendDCPSCommand(nSupply, Chr(B1(nSupply)) & Chr(B2(nSupply)) & Chr(B3(nSupply)))
        End If
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>This programs supply Polarity Mode 
    ''' <param name="nSupply">The supply number</param>
    ''' <param name="bState">True/False</param>
    ''' <remarks></remarks>
    Public Sub PolarityNegative(ByRef nSupply As Integer, ByRef bState As Boolean)
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        gFrmMain.txtInstrument.Text = "DCPS " & CStr(nSupply)
        gFrmMain.txtCommand.Text = "PolarityNegative"

        'Check Supply number and voltage range
        If nSupply > 10 Or nSupply < 1 Then
            Echo("DCPS PROGRAMMING ERROR:  Command cmdDCPS.SetPolarityNegative nSupply argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If
        B2(nSupply) = &H80 'bbbb
        B3(nSupply) = 0 'bbbb

        If bState = True Then
            B3(nSupply) = B3(nSupply) Or &H3 'Negative
        Else
            B3(nSupply) = B3(nSupply) Or &H2 'Positive
        End If

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        If Not bSimulation Then
            SendDCPSCommand(nSupply, Chr(B1(nSupply)) & Chr(B2(nSupply)) & Chr(B3(nSupply))) 'Set polarity
        End If
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
    End Sub

    ''' <summary>
    ''' This programs supply Remote Sense
    ''' </summary>
    ''' <param name="nSupply">The supply number</param>
    ''' <param name="bState">True/False</param>
    ''' <remarks></remarks>
    Public Sub RemoteSense(ByRef nSupply As Integer, ByRef bState As Boolean)
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        gFrmMain.txtInstrument.Text = "DCPS " & CStr(nSupply)
        gFrmMain.txtCommand.Text = "RemoteSense"
        'Check Supply number and voltage range
        If nSupply > 10 Or nSupply < 1 Then
            Echo("DCPS PROGRAMMING ERROR:  Command cmdDCPS.SetPolarityNegative nSupply argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If

        If bState = True Then
            B2(nSupply) = B2(nSupply) Or &H83 'Enable
        Else
            B2(nSupply) = B2(nSupply) Or &H82 'Enable / Sense Local
            B2(nSupply) = B2(nSupply) And &HFE 'bbbb
        End If

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        'Send Command
        If Not bSimulation Then
            SendDCPSCommand(nSupply, Chr(B1(nSupply)) & Chr(B2(nSupply)) & Chr(B3(nSupply)))
        End If
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
    End Sub

    ''' <summary>
    ''' This programs a specific supply's slave mode
    ''' </summary>
    ''' <param name="nSupply">The supply number</param>
    ''' <param name="bState">True/False</param>
    ''' <remarks></remarks>
    Public Sub SlaveMode(ByRef nSupply As Integer, ByRef bState As Boolean)
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        gFrmMain.txtInstrument.Text = "DCPS " & CStr(nSupply)
        gFrmMain.txtCommand.Text = "SlaveMode"

        'Check Supply number and voltage range
        If nSupply >= 10 Or nSupply < 1 Then
            Echo("DCPS PROGRAMMING ERROR:  Command cmdDCPS.SetSlaveMode nSupply argument out of range.")
            Err.Raise(-1004)
            Exit Sub
        End If

        If bState = True Then
            B3(nSupply) = B3(nSupply) Or &H3 'Negative
        Else
            B3(nSupply) = B3(nSupply) Or &H2 'Positive
            B3(nSupply) = B3(nSupply) And &HFE 'bbbb
        End If

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        'Send Command
        If Not bSimulation Then
            SendDCPSCommand(nSupply, Chr(B1(nSupply)) & Chr(B2(nSupply)) & Chr(B3(nSupply)))
        End If
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
    End Sub

    ''' <summary>
    ''' This programs supply voltage level
    ''' </summary>
    ''' <param name="nSupply">The supply number</param>
    ''' <param name="dVoltage">Voltage Level</param>
    ''' <remarks></remarks>
    Public Sub SetVoltage(ByRef nSupply As Integer, ByRef dVoltage As Double)
        Dim TempByte2, TempByte3 As Short

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        gFrmMain.txtInstrument.Text = "DCPS " & CStr(nSupply)
        gFrmMain.txtCommand.Text = "SetVoltage"

        'Check Supply number and voltage range
        If nSupply > 10 Or nSupply < 1 Then
            Echo("DCPS PROGRAMMING ERROR:  Command cmdDCPS.SetVoltage nSupply argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If
        If nSupply <= 9 And System.Math.Abs(dVoltage) > 45 Then
            Echo("DCPS PROGRAMMING ERROR:  Command cmdDCPS.SetVoltage dVoltage argument out of range.")
            Err.Raise(-1001)
            Exit Sub
        End If
        If nSupply = 10 And System.Math.Abs(dVoltage) > 65 Then
            Echo("DCPS PROGRAMMING ERROR:  Command cmdDCPS.SetVoltage dVoltage argument out of range.")
            Err.Raise(-1002)
            Exit Sub
        End If

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        'Check polarity
        If dVoltage < 0 Then
            PolarityNegative(nSupply, True)
            dVoltage = System.Math.Abs(dVoltage)
        End If
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub

        'Convert to Integer DAC code
        If nSupply = 10 Then
            dVoltage = dVoltage * 50 '20mV Resolution
        Else
            dVoltage = dVoltage * 100 '10mV Resolution
        End If

        'Store Current values
        TempByte2 = B2(nSupply)
        TempByte3 = B3(nSupply)

        B2(nSupply) = &H50 + (dVoltage \ &H100)
        B3(nSupply) = dVoltage Mod &H100

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        'Send Command
        If Not bSimulation Then
            SendDCPSCommand(nSupply, Chr(B1(nSupply)) & Chr(B2(nSupply)) & Chr(B3(nSupply)))
        End If

        'Restore values
        B2(nSupply) = TempByte2
        B3(nSupply) = TempByte3
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON)

        Delay(0.2)
    End Sub

    Private Sub cDCPS()
        Dim iSupply As Short
        'Set Byte Data to default
        For iSupply = 1 To 10
            B1(iSupply) = &H20 + iSupply 'Programming Supply
            B2(iSupply) = &HA0 Or &H88 Or &H82
            'Hex A0 Output Relay Open
            'Hex 88 Master Mode
            'Hex 82 Local Sense
            B3(iSupply) = &H22
            'Hex 20 Constant Voltage Mode
            'Hex 02 Normal Polarity
        Next iSupply
    End Sub

    Public Sub New()
        MyBase.New()
        cDCPS()
    End Sub
End Class