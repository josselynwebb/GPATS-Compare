VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "cDCPS"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
DefInt A-Z

'Programming Bytes
Dim B1(10) As Integer
Dim B2(10) As Integer
Dim B3(10) As Integer

Public Sub CloseOutputRelay(nSupply As Long, bState As Boolean)
'************************************************************
'* Written By     : Grady Johnson                           *
'*    DESCRIPTION:                                          *
'*     This programs supply Output Relay                    *
'*    EXAMPLE:                                              *
'*     SetRemoteSense nSupply:=1, bState:=True              *
'*    PARAMETERS:                                           *
'*     nSupply   =  The supply number                       *
'*     bState    =  True/False                              *
'************************************************************
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    frmMain.txtInstrument.Text = "DCPS " & CStr(nSupply)
    frmMain.txtCommand.Text = "CloseOutputRelay"

    If nSupply > 10 Or nSupply < 1 Then
        Echo "DCPS PROGRAMMING ERROR:  Command cmdDCPS.SetPolarityNegative nSupply argument out of range."
        Err.Raise -1000
        Exit Sub
    End If

    B2(nSupply) = 0
    If bState = True Then
        B2(nSupply) = B2(nSupply) Or &HB0 'Enable / Relay Closed
    Else
        B2(nSupply) = B2(nSupply) Or &HA0 'Enable / Relay Open
    End If
    
    If Not bSimulation Then
        SendDCPSCommand nSupply, Chr$(B1(nSupply)) & Chr$(B2(nSupply)) & Chr$(B3(nSupply))
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON

End Sub

Public Function dMeasAmps(nSupply As Long) As Double
'************************************************************
'* Written By     : Grady Johnson                           *
'*    DESCRIPTION:                                          *
'*     This Module Takes A Current Measurement From the     *
'*     internal DMM of a power supply                       *
'*    EXAMPLE:                                              *
'*     Amps! = CommandMeasureAmps!(3)                       *
'*    PARAMETERS:                                           *
'*     Supply%   =  The supply number from which to measure *
'*    RETURNS:                                              *
'*     The measured current in amps                         *
'************************************************************

    Dim Byte1%, Byte2%, Byte3%
    Dim D1 As Double
    Dim D2 As Double
    Dim ReadByte%
    Dim Data$
    Dim ReadBufferV$
    Dim ReadBuffer As String * 255
    Dim NBR&
    Dim ErrorStatus&
    Dim ReadBufferA$
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    frmMain.txtInstrument.Text = "DCPS " & CStr(nSupply)
    frmMain.txtCommand.Text = "dMeasAmps"

    If nSupply > 10 Or nSupply < 1 Then
        Echo "DCPS PROGRAMMING ERROR:  Command cmdDCPS.dMeasAmps nSupply argument out of range."
        dMeasAmps = 9.9E+37: frmMain.txtMeasured.Text = 9.9E+37: Err.Raise -1000
        Exit Function
    End If

    If bSimulation Then
        dMeasAmps = CDbl(InputBox("Command cmdDCPS.dMeasAmps performed." & vbCrLf & "Enter DCPS Amps Value:", "SIMULATION MODE"))
        frmMain.txtMeasured.Text = dMeasAmps
        Exit Function
    End If
    
    Byte1% = nSupply
    Byte2% = &H42 ' Measurement query to S
    Byte3% = 0
    
    SendDCPSCommand nSupply, Chr$(Byte1%) & Chr$(Byte2%) & Chr$(Byte3%)
    ErrorStatus& = ReadDCPSCommand(nSupply, ReadBuffer$) ' get 5 bytes
        
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    If ErrorStatus <> 0 Then Err.Raise ErrorStatus
    
    Data$ = ReadBuffer$
    If Asc(Mid$(Data$, 1, 1)) > 64 Then
        D1 = (Asc(Mid$(Data$, 3, 1))) And 15
        D1 = D1 * 256
        D2 = (Asc(Mid$(Data$, 4, 1)))
        ReadBufferV$ = Val((D1 + D2) / 100#)
        D1 = (Asc(Mid$(Data$, 1, 1))) And 15
        D1 = D1 * 256
        D2 = (Asc(Mid$(Data$, 2, 1)))
        ReadBufferA$ = Val((D1 + D2) / 500#)
        
        '65 Volt Supply Re-Scale Voltage Here
        If nSupply = 10 Then
            D1 = (Asc(Mid$(Data$, 3, 1))) And 15
            D1 = D1 * 256
            D2 = (Asc(Mid$(Data$, 4, 1)))
            ReadBufferV$ = Val((D1 + D2) / 50#)
        End If
         dMeasAmps = Val(ReadBufferA$)
    End If
    frmMain.txtMeasured.Text = CStr(Val(ReadBufferA$))
    DoEvents
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON

End Function

Public Function dMeasVoltage(nSupply As Long) As Double
'************************************************************
'* Written By     : Grady Johnson                           *
'*    DESCRIPTION:                                          *
'*     This Module Takes A Voltage Measurement From the     *
'*     internal DMM of a power supply                       *
'*    EXAMPLE:                                              *
'*     Volts! = dMeasVoltage(nSupply:=4)                    *
'*    PARAMETERS:                                           *
'*    nSupply    =  The supply number from which to measure *
'*    RETURNS:                                              *
'*     The measured voltage in volts                        *
'************************************************************

    Dim Byte1%, Byte2%, Byte3%
    Dim D1 As Double
    Dim D2 As Double
    Dim ReadByte%
    Dim Data$
    Dim ReadBuffer As String * 255
    Dim ReadBufferA$
    Dim ReadBufferV$
    Dim NBR&
    Dim ErrorStatus&

     If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
     frmMain.txtInstrument.Text = "DCPS " & CStr(nSupply)
     frmMain.txtCommand.Text = "dMeasVoltage"
     
     If nSupply > 10 Or nSupply < 1 Then
         Echo "DCPS PROGRAMMING ERROR:  Command cmdDCPS.dMeasVoltage nSupply argument out of range."
         dMeasVoltage = 9.9E+37: frmMain.txtMeasured.Text = 9.9E+37: Err.Raise -1000
         Exit Function
     End If
    
     If bSimulation Then
         dMeasVoltage = CDbl(InputBox("Command cmdDCPS.dMeasVoltage performed." & vbCrLf & "Enter DCPS Voltage Value:", "SIMULATION MODE"))
         frmMain.txtMeasured.Text = dMeasVoltage
         Exit Function
     End If
    
    'Program Query Supply For Sense Measurement
     Byte1% = nSupply
     Byte2% = &H42 ' 64 + 2
     Byte3% = &H0
     
     SendDCPSCommand nSupply, Chr$(Byte1%) & Chr$(Byte2%) & Chr$(Byte3%)
     ErrorStatus& = ReadDCPSCommand(nSupply, ReadBuffer$) ' get 5 bytes
     
     If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
     If ErrorStatus <> 0 Then Err.Raise ErrorStatus
     
     'Voltage Measurement
     'only update display if valid data is returned (not zeros)
     Data$ = ReadBuffer$
     If Asc(Mid$(Data$, 1, 1)) > 64 Then
         D1 = (Asc(Mid$(Data$, 3, 1))) And 15
         D1 = D1 * 256
         D2 = (Asc(Mid$(Data$, 4, 1)))
         ReadBufferV$ = Val((D1 + D2) / 100#)
         
         'Current Measurement
         D1 = (Asc(Mid$(Data$, 1, 1))) And 15
         D1 = D1 * 256
         D2 = (Asc(Mid$(Data$, 2, 1)))
         ReadBufferA$ = Val((D1 + D2) / 500#)

         '65 Volt Supply Re-Scale Voltage Here
         If nSupply = 10 Then
             D1 = (Asc(Mid$(Data$, 3, 1))) And 15
             D1 = D1 * 256
             D2 = (Asc(Mid$(Data$, 4, 1)))
             ReadBufferV$ = Val((D1 + D2) / 50#)
         End If
         dMeasVoltage = Val(ReadBufferV$)
      End If
     
     'Determine Polarity
     'Program Query Supply For Status Query
      Byte1% = nSupply
      Byte2% = &H44
      Byte3% = 0
      
      SendDCPSCommand nSupply, Chr$(Byte1%) & Chr$(Byte2%) & Chr$(Byte3%)
      ErrorStatus& = ReadDCPSCommand(nSupply, ReadBuffer$) ' get 5 bytes
      
      If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
      If ErrorStatus <> 0 Then Err.Raise ErrorStatus
      
      'Voltage Measurement
      'only update display if valid data is returned (not zeros)
      Data$ = ReadBuffer$
      If (Asc(Data$) And 2) = 2 Then
         dMeasVoltage = -dMeasVoltage
      End If
              
     frmMain.txtMeasured.Text = CStr(dMeasVoltage)
     DoEvents
     If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON
        
End Function

Public Sub PowerOn(nSupply As Long, dVoltage As Double, dAmps As Double, _
                 Optional bNegativePolarity As Variant = False, _
                 Optional bSlaveMode As Boolean = False, _
                 Optional bConstantCurrentMode As Boolean = False, _
                 Optional bRemoteSense As Boolean = False, _
                 Optional bCloseOutput As Boolean = True)
'************************************************************
'* Written By     : Grady Johnson                           *
'*    DESCRIPTION:                                          *
'*     This Module programs the power supplies              *
'*    EXAMPLE:                                              *
'*     Poweron sSupply:=1, dVoltage:=2, dCurrent:=3         *
'*    PARAMETERS:                                           *
'*     nSupply  =  The supply number of which to set        *
'*     dVoltage =  Voltage Value and/or Limit               *
'*     dCurrent =  Current Value and/or Limit               *
'*    OPTIONAL PARAMETERS:                                  *
'*     bNegativePolarity =  Set True or False. Default False*
'*     bSlaveMode =  Set True or False. Default False       *
'*     bConstantCurrentMode =  Set True for Constant       *
'*                              current mode. Default False *
'*     bRemoteSense =  Set True or False.  Default False    *
'*     bCloseOutput =  Set True or False.  Default True     *
'************************************************************

    Dim D1 As Double
    Dim D2 As Double
    Dim ReadByte%
    Dim Data$
    Dim ReadBuffer As String * 255
    Dim ReadBufferA$
    Dim ReadBufferV$
    Dim NBR&
    Dim ErrorStatus&
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
        frmMain.txtInstrument.Text = "DCPS " & CStr(nSupply)
        frmMain.txtCommand.Text = "PowerOn"
    
    If nSupply > 10 Or nSupply < 1 Then
        Echo "DCPS PROGRAMMING ERROR:  Command cmdDCPS.PowerOn nSupply argument out of range."
        Err.Raise 10000
        Exit Sub
    End If
    If nSupply <= 9 And Abs(dVoltage) > 45 Then
        Echo "DCPS PROGRAMMING ERROR:  Command cmdDCPS.PowerOn dVoltage argument out of range."
        Err.Raise -1001
        Exit Sub
    End If
    If nSupply = 10 And Abs(dVoltage) > 65 Then
        Echo "DCPS PROGRAMMING ERROR:  Command cmdDCPS.PowerOn dVoltage argument out of range."
        Err.Raise -1002
        Exit Sub
    End If
    If dAmps > 5 Then
        Echo "DCPS PROGRAMMING ERROR:  Command cmdDCPS.PowerOn dCurrent argument out of range."
        Err.Raise -1003
        Exit Sub
    End If
    If (nSupply = 1 Or nSupply = 10) And bSlaveMode Then
        Echo "DCPS PROGRAMMING ERROR:  Command cmdDCPS.PowerOn bSlaveMode argument out of range."
        Err.Raise -1004
        Exit Sub
    End If
    
    If Not bSimulation Then
        SendDCPSCommand nSupply, Chr$(B1(nSupply)) & Chr$(&HAA) & Chr$(&H22)
        If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
        Delay 0.3
    Else
        Exit Sub
    End If
    
    B2(nSupply) = &HA0
    B3(nSupply) = &H0
    
    Select Case bSlaveMode  'Hex 8C Slave, Hex 88 Master
        Case False
            B2(nSupply) = B2(nSupply) Or &H88 'Enable /Set as Master
        Case True
            B2(nSupply) = B2(nSupply) Or &H8C 'Set as Slave and enable
            SendDCPSCommand nSupply, Chr$(B1(nSupply)) & Chr$(&H8C) & Chr$(0)
            If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
            Delay 0.3
    End Select
    
    If dVoltage < 0 Then bNegativePolarity = True
    Select Case bNegativePolarity 'Hex 03 Reverse Polarity, Hex 02 Normal Polarity
        Case False
            B3(nSupply) = B3(nSupply) Or &H2 'Set Normal Polarity
        Case True
            B3(nSupply) = B3(nSupply) Or &H3 'Set Negative Polarity
            SendDCPSCommand nSupply, Chr$(B1(nSupply)) & Chr$(&H80) & Chr$(&H3)
            If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
            Delay 0.3
    End Select

    Select Case bConstantCurrentMode    'Hex 30 Constant Current, Hex 20 Constant Voltage
      Case False
            B3(nSupply) = B3(nSupply) Or &HA0 'Set Constant Voltage Mode
       Case True
            B3(nSupply) = B3(nSupply) Or &HB0 'Set Constant Current Mode
           SendDCPSCommand nSupply, Chr$(B1(nSupply)) & Chr$(&H80) & Chr$(&HB0)
           If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
            Delay 0.3
    End Select
    
    If bCloseOutput Then 'Hex B0 Relay Closed, A0 Relay Open
      Delay 0.5
      B2(nSupply) = B2(nSupply) Or &HB0 'Enable / Relay Closed
      SendDCPSCommand nSupply, Chr$(B1(nSupply)) & Chr$(&HB0) & Chr$(0)
      If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    End If
    
    Select Case bRemoteSense    'Hex 83 Remote Sense, Hex 82 Local Sense
        Case False
            B2(nSupply) = B2(nSupply) Or &H82 'Enable / Sense Local
        Case True
            B2(nSupply) = B2(nSupply) Or &H83 'Enable/ Sense Remote
            Delay 0.5
            SendDCPSCommand nSupply, Chr$(B1(nSupply)) & Chr$(&H83) & Chr$(0)
    End Select
    
    Delay 0.2
    SetAmps nSupply, dAmps
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    SetVoltage nSupply, Abs(dVoltage)
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON

End Sub

Public Sub ResetAll()
'************************************************************
'* Written By     : Grady Johnson                           *
'*    DESCRIPTION:                                          *
'*     Resets The Hardware and Software                     *
'*     To It's Default Power-On State                       *
'*    EXAMPLE:                                              *
'*      ResetPowerSupplies                                  *
'************************************************************

    Dim nSupplyIndex As Long
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    frmMain.txtInstrument.Text = "DCPS 1 - 10"
    frmMain.txtCommand.Text = "ResetAll"

    If Not bSimulation Then
        iInitDCPS bReset:=True
    End If
    
End Sub

Public Sub ResetSupply(nSupply As Long)
'************************************************************
'* Written By     : Grady Johnson                           *
'*    DESCRIPTION:                                          *
'*     This resets a specific Supply                        *
'*    EXAMPLE:                                              *
'*     ResetSupply nSupply:=1                               *
'*    PARAMETERS:                                           *
'*     nSupply   =  The supply number                       *
'************************************************************

    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    frmMain.txtInstrument.Text = "DCPS " & CStr(nSupply)
    frmMain.txtCommand.Text = "ResetSupply"

    If nSupply > 10 Or nSupply < 1 Then
        Echo "DCPS PROGRAMMING ERROR:  Command cmdDCPS.ResetSupply nSupply argument out of range."
        Err.Raise -1000
        Exit Sub
    End If
        
    If Not bSimulation Then
        SetVoltage nSupply, 0#
        SetAmps nSupply, 0.075
        SendDCPSCommand nSupply, Chr$(&H10 + nSupply) & Chr$(&H0) & Chr$(&H0)
        If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
        
        'Enable Open Relay, Master, Constant Voltage, Normal Polarity
        SendDCPSCommand nSupply, Chr$(B1(nSupply)) & Chr$(&HAA) & Chr$(&H22)
        B2(nSupply) = &HAA
        B3(nSupply) = &H22
        Delay 0.8
    End If
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON
    
End Sub

Public Sub SetAmps(nSupply As Long, dAmps As Double)
'************************************************************
'* Written By     : Grady Johnson                           *
'*    DESCRIPTION:                                          *
'*     This programs current on a specific supply           *
'*    EXAMPLE:                                              *
'*     SetAmps nSupply:=1, dCurrent:=3                      *
'*    PARAMETERS:                                           *
'*     nSupply   =  The supply number                       *
'*     bCurrent  =  Current value                           *
'************************************************************

    Dim TempByte2, TempByte3
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    frmMain.txtInstrument.Text = "DCPS " & CStr(nSupply)
    frmMain.txtCommand.Text = "SetAmps"

    If nSupply > 10 Or nSupply < 1 Then
        Echo "DCPS PROGRAMMING ERROR:  Command cmdDCPS.SetAmps nSupply argument out of range."
        Err.Raise -1000
        Exit Sub
    End If
    If dAmps > 5 Then
        Echo "DCPS PROGRAMMING ERROR:  Command cmdDCPS.SetAmps dCurrent argument out of range."
        Err.Raise -1003
        Exit Sub
    End If
    
    'Convert to Integer DAC code
    dAmps = dAmps * 500  '20mA Resolution
    
    'Save values
    TempByte2 = B2(nSupply)
    TempByte3 = B3(nSupply)
    
    B2(nSupply) = &H40 + (dAmps \ &H100)
    B3(nSupply) = dAmps Mod &H100
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    If Not bSimulation Then
        SendDCPSCommand nSupply, Chr$(B1(nSupply)) & Chr$(B2(nSupply)) & Chr$(B3(nSupply))
    End If

    'Restore values
    B2(nSupply) = TempByte2
    B3(nSupply) = TempByte3
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON
    
    Delay 0.2

End Sub

Public Sub ConstantCurrent(nSupply As Long, bState As Boolean)
'************************************************************
'* Written By     : Grady Johnson                           *
'*    DESCRIPTION:                                          *
'*     This programs supply Constant Current Mode           *
'*    EXAMPLE:                                              *
'*     SetConstantCurrentMode nSupply:=1, bState:=True      *
'*    PARAMETERS:                                           *
'*     nSupply   =  The supply number                       *
'*     bState    =  True/False                              *
'************************************************************

    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    frmMain.txtInstrument.Text = "DCPS " & CStr(nSupply)
    frmMain.txtCommand.Text = "ConstantCurrent"

    If nSupply > 10 Or nSupply < 1 Then
        Echo "DCPS PROGRAMMING ERROR:  Command cmdDCPS.SetPolarityNegative nSupply argument out of range."
        Err.Raise -1000
        Exit Sub
    End If
    B2(nSupply) = &H80
    B3(nSupply) = 0
    If bState = True Then
        B3(nSupply) = B3(nSupply) Or &H20 'Set Constant Voltage Mode
    Else
        B3(nSupply) = B3(nSupply) Or &H30 'Set Constant Current Mode
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    If Not bSimulation Then
        SendDCPSCommand nSupply, Chr$(B1(nSupply)) & Chr$(B2(nSupply)) & Chr$(B3(nSupply))
    End If
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub

End Sub

Public Sub PolarityNegative(nSupply As Long, bState As Boolean)
'************************************************************
'* Written By     : Grady Johnson                           *
'*    DESCRIPTION:                                          *
'*     This programs supply Polarity Mode                   *
'*    EXAMPLE:                                              *
'*     SetPolarityNegative nSupply:=1, bState:=True         *
'*    PARAMETERS:                                           *
'*     nSupply   =  The supply number                       *
'*     bState    =  True/False                              *
'************************************************************

    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    frmMain.txtInstrument.Text = "DCPS " & CStr(nSupply)
    frmMain.txtCommand.Text = "PolarityNegative"

    If nSupply > 10 Or nSupply < 1 Then
        Echo "DCPS PROGRAMMING ERROR:  Command cmdDCPS.SetPolarityNegative nSupply argument out of range."
        Err.Raise -1000
        Exit Sub
    End If
    B2(nSupply) = &H80
    B3(nSupply) = 0
    
    If bState = True Then
        B3(nSupply) = B3(nSupply) Or &H3  'Negative
    Else
        B3(nSupply) = B3(nSupply) Or &H2   'Positive
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    If Not bSimulation Then
        SendDCPSCommand nSupply, Chr$(B1(nSupply)) & Chr$(B2(nSupply)) & Chr$(B3(nSupply)) 'Set polarity
    End If
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub

End Sub

Public Sub RemoteSense(nSupply As Long, bState As Boolean)
'************************************************************
'* Written By     : Grady Johnson                           *
'*    DESCRIPTION:                                          *
'*     This programs supply Remote Sense                    *
'*    EXAMPLE:                                              *
'*     SetRemoteSense nSupply:=1, bState:=True              *
'*    PARAMETERS:                                           *
'*     nSupply   =  The supply number                       *
'*     bState    =  True/False                              *
'************************************************************

    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    frmMain.txtInstrument.Text = "DCPS " & CStr(nSupply)
    frmMain.txtCommand.Text = "RemoteSense"
    
    If nSupply > 10 Or nSupply < 1 Then
        Echo "DCPS PROGRAMMING ERROR:  Command cmdDCPS.SetPolarityNegative nSupply argument out of range."
        Err.Raise -1000
        Exit Sub
    End If

    If bState = True Then
        B2(nSupply) = B2(nSupply) Or &H83 'Enable
    Else
        B2(nSupply) = B2(nSupply) Or &H82 'Enable / Sense Local
        B2(nSupply) = B2(nSupply) And &HFE
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    If Not bSimulation Then
        SendDCPSCommand nSupply, Chr$(B1(nSupply)) & Chr$(B2(nSupply)) & Chr$(B3(nSupply))
    End If
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
End Sub

Public Sub SlaveMode(nSupply As Long, bState As Boolean)
'************************************************************
'* Written By     : Grady Johnson                           *
'*    DESCRIPTION:                                          *
'*     This programs a specific supply's slave mode         *
'*    EXAMPLE:                                              *
'*     SetSlaveMode nSupply:=1, bState:=True                *
'*    PARAMETERS:                                           *
'*     nSupply   =  The supply number                       *
'*     bState    =  True/False                              *
'************************************************************
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    frmMain.txtInstrument.Text = "DCPS " & CStr(nSupply)
    frmMain.txtCommand.Text = "SlaveMode"

    If nSupply >= 10 Or nSupply < 1 Then
        Echo "DCPS PROGRAMMING ERROR:  Command cmdDCPS.SetSlaveMode nSupply argument out of range."
        Err.Raise -1004
        Exit Sub
    End If
    
    If bState = True Then
        B3(nSupply) = B3(nSupply) Or &H3  'Negative
    Else
        B3(nSupply) = B3(nSupply) Or &H2   'Positive
        B3(nSupply) = B3(nSupply) And &HFE
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    If Not bSimulation Then
        SendDCPSCommand nSupply, Chr$(B1(nSupply)) & Chr$(B2(nSupply)) & Chr$(B3(nSupply))
    End If
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub

End Sub

Public Sub SetVoltage(nSupply As Long, dVoltage As Double)
'************************************************************
'* Written By     : Grady Johnson                           *
'*    DESCRIPTION:                                          *
'*     This programs supply voltage level                   *
'*    EXAMPLE:                                              *
'*     SetVoltage nSupply:=1, dVoltage:=5                   *
'*    PARAMETERS:                                           *
'*     nSupply   =  The supply number                       *
'*     dVoltage  =  Voltage Level                           *
'************************************************************

    Dim TempByte2%, TempByte3%
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    frmMain.txtInstrument.Text = "DCPS " & CStr(nSupply)
    frmMain.txtCommand.Text = "SetVoltage"

    If nSupply > 10 Or nSupply < 1 Then
        Echo "DCPS PROGRAMMING ERROR:  Command cmdDCPS.SetVoltage nSupply argument out of range."
        Err.Raise -1000
        Exit Sub
    End If
    If nSupply <= 9 And Abs(dVoltage) > 45 Then
        Echo "DCPS PROGRAMMING ERROR:  Command cmdDCPS.SetVoltage dVoltage argument out of range."
        Err.Raise -1001
        Exit Sub
    End If
    If nSupply = 10 And Abs(dVoltage) > 65 Then
        Echo "DCPS PROGRAMMING ERROR:  Command cmdDCPS.SetVoltage dVoltage argument out of range."
        Err.Raise -1002
        Exit Sub
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    If dVoltage < 0 Then
        PolarityNegative nSupply, True
        dVoltage = Abs(dVoltage)
    End If
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    'Convert to Integer DAC code
    If nSupply = 10 Then
        dVoltage = dVoltage * 50 '20mV Resolution
    Else
        dVoltage = dVoltage * 100 '10mV Resolution
    End If
    
    'Save values
    TempByte2 = B2(nSupply)
    TempByte3 = B3(nSupply)
    
    B2(nSupply) = &H50 + (dVoltage \ &H100)
    B3(nSupply) = dVoltage Mod &H100

    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    If Not bSimulation Then
        SendDCPSCommand nSupply, Chr$(B1(nSupply)) & Chr$(B2(nSupply)) & Chr$(B3(nSupply))
    End If
    
    'Restore values
    B2(nSupply) = TempByte2
    B3(nSupply) = TempByte3
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON
    
    Delay 0.2

End Sub

Private Sub Class_Initialize()

    Dim iSupply As Integer
    
    'Set Byte Data to default
    For iSupply = 1 To 10
        
        'Hex A0 Output Relay Open
        'Hex 88 Master Mode
        'Hex 82 Local Sense
        B1(iSupply) = &H20 + iSupply   'Programming Supply
        B2(iSupply) = &HA0 Or &H88 Or &H82
        
        'Hex 20 Constant Voltage Mode
        'Hex 02 Normal Polarity
        B3(iSupply) = &H22
    Next iSupply

End Sub
