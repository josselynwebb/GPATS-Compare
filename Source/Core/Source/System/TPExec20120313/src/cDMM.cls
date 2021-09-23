VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "cDMM"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
DefInt A-Z

Public Sub SendMsg(sMsg As String)
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    frmMain.txtInstrument.Text = "DMM"
    frmMain.txtCommand.Text = "SendMsg"
    WriteMsg DMM, sMsg
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON
End Sub

Public Function sReadMsg(sMsg As String) As String
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    frmMain.txtInstrument.Text = "DMM"
    frmMain.txtCommand.Text = "ReadMsg"
    ReadMsg DMM, sReadMsg
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON
End Function

Public Function dMeasFreq(Optional sTrigger As String = "IMMEDIATE", _
                          Optional bGT400HZ As Boolean = False, _
                          Optional dDelay As Double = 0#, _
                          Optional sAutoZero As String = "ON", _
                          Optional APROBE As String = "OFF") As Double

    Dim sData As String
    Dim nErr As Long
    Dim sCurrentMsg As String
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    frmMain.txtInstrument.Text = "DMM"
    frmMain.txtCommand.Text = "dMeasFreq"
    sCurrentMsg = frmMain.lblStatus.Caption
    If APROBE <> "OFF" Then
        frmMain.lblStatus.Caption = "APPLY PROBE"
    End If
    
    If Not bSimulation Then
        WriteMsg DMM, "*CLS"
        WriteMsg DMM, "*RST"
        WriteMsg DMM, "CONF:FREQ AUTO"
    End If
    
    If bGT400HZ And Not bSimulation Then WriteMsg DMM, "BAND:DET 400"
    
    sTrigger = UCase(sTrigger)
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    Select Case sTrigger
        Case "EXTERNAL", "BUS", "TTLTRG0", "TTLTRG1", "TTLTRG2", "TTLTRG3", "TTLTRG4", "TTLTRG5", "TTLTRG6", "TTLTRG7"
            If Not bSimulation Then WriteMsg DMM, "TRIG:SOUR " & sTrigger
        Case "IMMEDIATE"
        Case Else
            Echo "DMM PROGRAMMING ERROR:  Command cmdDMM.dMeasFreq sTrigger argument out of range."
            dMeasFreq = 9.9E+37: frmMain.txtMeasured.Text = 9.9E+37: Err.Raise -1000
            dMeasFreq = 0
            Exit Function
    End Select
    
    sAutoZero = UCase(sAutoZero)
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    Select Case sAutoZero
        Case "OFF", "ONCE"
            If Not bSimulation Then WriteMsg DMM, "CAL:ZERO:AUTO " & sAutoZero
        Case "ON"
            Delay 0.3
        Case Else
            Echo "DMM PROGRAMMING ERROR:  Command cmdDMM.dMeasFreq sAutoZero argument out of range."
            Err.Raise -1001
            dMeasFreq = 0
            Exit Function
    End Select
    
    APROBE = UCase(APROBE)
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    Select Case Left(APROBE, 1)
        Case "O", "S", "C" ' Off, Single, Continuous
        Case Else
            Echo "DMM PROGRAMMING ERROR:  Command cmdDMM.dMeasFreq APROBE argument out of range."
            Err.Raise -1001
            dMeasFreq = 0
            Exit Function
    End Select
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    If Not bSimulation Then
        Select Case Left(UCase(APROBE), 1)
            Case "O"    ' Off
                WriteMsg DMM, "INIT;FETCH?"
                Delay 0.5
                nErr = ReadMsg(DMM, sData)
                If nErr <> 0 Then
                    dMeasFreq = 9.9E+37: frmMain.txtMeasured.Text = 9.9E+37
                    Err.Raise nErr
                    Exit Function
                End If
                dMeasFreq = CDbl(sData)
                frmMain.txtMeasured.Text = CSng(dMeasFreq)
                If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
            Case "S"    ' Single
              frmMain.lblStatus.Caption = "Waiting for Probe ..."
              MisProbe = vbYes
                Do While MisProbe = vbYes
                    Failed = False
                    UserEvent = 0
                    frmMain.TimerProbe.Enabled = True: frmMain.fraInstructions.Visible = True
                    Do While (UserEvent = 0) And (Not bProbeClosed)
                        DoEvents
                    Loop
                    Do While (UserEvent = 0) And (bProbeClosed)
                        DoEvents
                    Loop
                    frmMain.TimerProbe.Enabled = False
                    bProbeClosed = False
                    frmMain.lblStatus.Caption = "Making Measurement ..."
                    WriteMsg DMM, "INIT;FETCH?"
                    nErr = ReadMsg(DMM, sData)
                    If nErr <> 0 Then
                        dMeasFreq = 9.9E+37: frmMain.txtMeasured.Text = 9.9E+37
                        Err.Raise nErr
                        Exit Function
                    End If
                    dMeasFreq = CDbl(sData)
                    frmMain.txtMeasured.Text = CSng(dMeasFreq)
                    frmMain.lblStatus.Caption = "Measurement Complete ..."
                    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
                    If Failed = True Then
                        MisProbe = MsgBox("Analog Probe Measurement Failed. " & vbCrLf & _
                            "Do you wish to re-probe?", vbCritical + vbYesNo + vbSystemModal, "Analog Probe Failure")
                    Else
                        MisProbe = vbNo
                    End If
                Loop
                frmMain.lblStatus.Caption = sCurrentMsg
                
                    
            Case "C"    ' Continuous
                MisProbe = vbYes
                Do While MisProbe = vbYes
                    UserEvent = 0
                    Failed = False
                    frmMain.TimerProbe.Enabled = True: frmMain.fraInstructions.Visible = True
                    Do While (UserEvent = 0) And (Not bProbeClosed)
                        frmMain.lblStatus.Caption = "Making Measurement ..."
                        WriteMsg DMM, "INIT;FETCH?"
                        nErr = ReadMsg(DMM, sData)
                        If nErr <> 0 Then
                            dMeasFreq = 9.9E+37: frmMain.txtMeasured.Text = 9.9E+37
                            frmMain.txtMeasured.Text = dMeasFreq
                            Failed = True
                            Pass = False
                            Err.Raise nErr
                            Exit Function
                        End If
                        dMeasFreq = CDbl(sData)
                        frmMain.txtMeasured.Text = CSng(dMeasFreq)
                        If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
                    Loop
                    Do While (UserEvent = 0) And (bProbeClosed)
                        DoEvents
                    Loop
                    frmMain.TimerProbe.Enabled = False
                    bProbeClosed = False
                    frmMain.lblStatus.Caption = "Measurement Complete ..."
                    If Failed = True Then
                        MisProbe = MsgBox("Analog Probe Measurement Failed. " & vbCrLf & _
                            "Do you wish to re-probe?", vbCritical + vbYesNo + vbSystemModal, "Analog Probe Failure")
                    Else
                        MisProbe = vbNo
                    End If
                Loop
        End Select
    Else
        dMeasFreq = CDbl(InputBox("Command cmdDMM.dMeasFreq peformed." & vbCrLf & "Enter Frequency Value:", "SIMULATION MODE"))
        frmMain.txtMeasured.Text = CSng(dMeasFreq)
        If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON
    End If
    frmMain.lblStatus.Caption = sCurrentMsg
End Function

Public Function dMeasPeriod(Optional sTrigger As String = "IMMEDIATE", _
                            Optional bGT400HZ As Boolean = False, _
                            Optional dDelay As Double = 0#, _
                            Optional sAutoZero As String = "ON", _
                            Optional APROBE As String = "OFF") As Double
    Dim sData As String
    Dim nErr As Long
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    frmMain.txtInstrument.Text = "DMM"
    frmMain.txtCommand.Text = "dMeasPeriod"

    Dim sCurrentMsg As String
    sCurrentMsg = frmMain.lblStatus.Caption
    If APROBE <> "OFF" Then
        frmMain.lblStatus.Caption = "APPLY PROBE"
    End If
    'Setup DMM for Frequency Measurement
    If Not bSimulation Then
        WriteMsg DMM, "*CLS"
        WriteMsg DMM, "*RST"
        WriteMsg DMM, "CONF:PER AUTO"
    End If
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    'Set Bandwidth
    If bGT400HZ And Not bSimulation Then WriteMsg DMM, "BAND:DET 400"
    'Set Trigger
    sTrigger = UCase(sTrigger)
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    Select Case sTrigger
        Case "EXTERNAL", "BUS", "TTLTRG0", "TTLTRG1", "TTLTRG2", "TTLTRG3", "TTLTRG4", "TTLTRG5", "TTLTRG6", "TTLTRG7"
            If Not bSimulation Then WriteMsg DMM, "TRIG:SOUR " & sTrigger
        Case "IMMEDIATE"
        Case Else
            Echo "DMM PROGRAMMING ERROR:  Command cmdDMM.dMeasFreq sTrigger argument out of range."
            dMeasPeriod = 9.9E+37: frmMain.txtMeasured.Text = 9.9E+37
            Err.Raise -1000
            Exit Function
    End Select
    'Set Case AutoZero
    sAutoZero = UCase(sAutoZero)
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    Select Case sAutoZero
        Case "OFF", "ONCE"
            If Not bSimulation Then WriteMsg DMM, "CAL:ZERO:AUTO " & sAutoZero
        Case "ON"
            Delay 0.2
        Case Else
            Echo "DMM PROGRAMMING ERROR:  Command cmdDMM.dMeasPeriod sAutoZero argument out of range."
            dMeasPeriod = 9.9E+37: frmMain.txtMeasured.Text = 9.9E+37
            Err.Raise -1000
            Exit Function
    End Select
    'Set Case Aprobe
    APROBE = UCase(APROBE)
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    Select Case Left(APROBE, 1)
        Case "O", "S", "C" ' Off, Single, Continuous
        Case Else
            Echo "DMM PROGRAMMING ERROR:  Command cmdDMM.dMeasPeriod APROBE argument out of range."
            dMeasPeriod = 9.9E+37: frmMain.txtMeasured.Text = 9.9E+37
            Err.Raise -1000
            Exit Function
    End Select
    'Make Measurement
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    If Not bSimulation Then
        Select Case Left(UCase(APROBE), 1)
            Case "O"    ' Off
                WriteMsg DMM, "INIT;FETCH?"
                Delay 0.5
                nErr = ReadMsg(DMM, sData)
                If nErr <> 0 Then
                    Echo "VISA ERROR: " & nErr
                    dMeasPeriod = 9.9E+37: frmMain.txtMeasured.Text = 9.9E+37
                    Err.Raise -1000
                    Exit Function
                End If
                dMeasPeriod = CDbl(sData)
                frmMain.txtMeasured.Text = CSng(dMeasPeriod)
                If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
            Case "S"    ' Single
                frmMain.lblStatus.Caption = "Waiting for Probe ..."
                MisProbe = vbYes
                Do While MisProbe = vbYes
                    Failed = False
                    UserEvent = 0
                    frmMain.TimerProbe.Enabled = True: frmMain.fraInstructions.Visible = True
                    Do While (UserEvent = 0) And (Not bProbeClosed)
                        DoEvents
                    Loop
                    Do While (UserEvent = 0) And (bProbeClosed)
                        DoEvents
                    Loop
                    frmMain.TimerProbe.Enabled = False
                    bProbeClosed = False
                    frmMain.lblStatus.Caption = "Making Measurement ..."
                    WriteMsg DMM, "INIT;FETCH?"
                    nErr = ReadMsg(DMM, sData)
                    If nErr <> 0 Then
                        Echo "VISA ERROR: " & nErr
                        dMeasPeriod = 9.9E+37: frmMain.txtMeasured.Text = 9.9E+37
                        Err.Raise -1000
                        Exit Function
                    End If
                    dMeasPeriod = CDbl(sData)
                    frmMain.txtMeasured.Text = CSng(dMeasPeriod)
                    frmMain.lblStatus.Caption = "Measurement Complete ..."
                    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
                    If Failed = True Then
                        MisProbe = MsgBox("Analog Probe Measurement Failed. " & vbCrLf & _
                            "Do you wish to re-probe?", vbCritical + vbYesNo + vbSystemModal, "Analog Probe Failure")
                    Else
                        MisProbe = vbNo
                    End If
                Loop
                frmMain.lblStatus.Caption = sCurrentMsg
                
                    
            Case "C"    ' Continuous
                MisProbe = vbYes
                Do While MisProbe = vbYes
                    UserEvent = 0
                    Failed = False
                    frmMain.TimerProbe.Enabled = True: frmMain.fraInstructions.Visible = True
                    Do While (UserEvent = 0) And (Not bProbeClosed)
                        frmMain.lblStatus.Caption = "Making Measurement ..."
                        WriteMsg DMM, "INIT;FETCH?"
                        nErr = ReadMsg(DMM, sData)
                        If nErr <> 0 Then
                            Echo "VISA ERROR: " & nErr
                            dMeasPeriod = 9.9E+37: frmMain.txtMeasured.Text = 9.9E+37
                            Err.Raise -1000
                            Exit Function
                        End If
                        dMeasPeriod = CDbl(sData)
                        frmMain.txtMeasured.Text = CSng(dMeasPeriod)
                        If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
                    Loop
                    Do While (UserEvent = 0) And (bProbeClosed)
                        DoEvents
                    Loop
                    frmMain.TimerProbe.Enabled = False
                    bProbeClosed = False
                    frmMain.lblStatus.Caption = "Measurement Complete ..."
                    If Failed = True Then
                        MisProbe = MsgBox("Analog Probe Measurement Failed. " & vbCrLf & _
                            "Do you wish to re-probe?", vbCritical + vbYesNo + vbSystemModal, "Analog Probe Failure")
                    Else
                        MisProbe = vbNo
                    End If
                Loop
        End Select
    Else
        dMeasPeriod = CDbl(InputBox("Command cmdDMM.dMeasPeriod peformed." & vbCrLf & "Enter Period Value:", "SIMULATION MODE"))
        frmMain.txtMeasured.Text = CSng(dMeasPeriod)
        If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON
    End If
    frmMain.lblStatus.Caption = sCurrentMsg
End Function

Public Function dMeasAC(Optional dRange As Double = 1E+99, _
                            Optional sTrigger As String = "IMMEDIATE", _
                            Optional bGT400HZ As Boolean = False, _
                            Optional dDelay As Double = 0#, _
                            Optional sAutoZero As String = "ON", _
                            Optional APROBE As String = "OFF") As Double
                            
    Dim sData As String
    Dim nErr As Long
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    frmMain.txtInstrument.Text = "DMM"
    frmMain.txtCommand.Text = "dMeasAC"
    Dim sCurrentMsg As String
    sCurrentMsg = frmMain.lblStatus.Caption
    If APROBE <> "OFF" Then
        frmMain.lblStatus.Caption = "APPLY PROBE"
    End If
    'Setup DMM for Frequency Measurement
    If Not bSimulation Then
        WriteMsg DMM, "*CLS"
        WriteMsg DMM, "*RST"
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    If dRange = 1E+99 Then
        If Not bSimulation Then WriteMsg DMM, "CONF:VOLT:AC AUTO"
    Else
        Select Case dRange
            Case 0.1, 1, 10, 100, 300
                If Not bSimulation Then WriteMsg DMM, "CONF:VOLT:AC " & CStr(dRange)
            Case Else
                Echo "DMM PROGRAMMING ERROR:  Command cmdDMM.dMeasAC dRange argument out of range."
                dMeasAC = 9.9E+37: frmMain.txtMeasured.Text = 9.9E+37
                Err.Raise -1000
            Exit Function
        End Select
    End If
    
    'Set Bandwidth
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    If bGT400HZ And Not bSimulation Then WriteMsg DMM, "BAND:DET 400"
    'Set Trigger
    sTrigger = UCase(sTrigger)
    Select Case sTrigger
        Case "EXTERNAL", "BUS", "TTLTRG0", "TTLTRG1", "TTLTRG2", "TTLTRG3", "TTLTRG4", "TTLTRG5", "TTLTRG6", "TTLTRG7"
            If Not bSimulation Then WriteMsg DMM, "TRIG:SOUR " & sTrigger
        Case "IMMEDIATE"
        Case Else
            Echo "DMM PROGRAMMING ERROR:  Command cmdDMM.dMeasAC sTrigger argument out of range."
            dMeasAC = 9.9E+37: frmMain.txtMeasured.Text = 9.9E+37
            Err.Raise -1000
            Exit Function
    End Select
    'Set Case AutoZero
    sAutoZero = UCase(sAutoZero)
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    Select Case sAutoZero
        Case "OFF", "ONCE"
            If Not bSimulation Then WriteMsg DMM, "CAL:ZERO:AUTO " & sAutoZero
        Case "ON"
            Delay 0.3
        Case Else
            Echo "DMM PROGRAMMING ERROR:  Command cmdDMM.dMeasAC sAutoZero argument out of range."
            dMeasAC = 9.9E+37: frmMain.txtMeasured.Text = 9.9E+37
            Err.Raise -1000
            Exit Function
    End Select
    'Set Case Aprobe
    APROBE = UCase(APROBE)
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    Select Case Left(APROBE, 1)
        Case "O", "S", "C" ' Off, Single, Continuous
        Case Else
            Echo "DMM PROGRAMMING ERROR:  Command cmdDMM.dMeasAC APROBE argument out of range."
            dMeasAC = 9.9E+37: frmMain.txtMeasured.Text = 9.9E+37
            Err.Raise -1000
            Exit Function
    End Select
    'Make Measurement
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    If Not bSimulation Then
        Select Case Left(UCase(APROBE), 1)
            Case "O"    ' Off
                WriteMsg DMM, "INIT;FETCH?"
                Delay 0.5
                nErr = ReadMsg(DMM, sData)
                If nErr <> 0 Then
                    Echo "VISA ERROR: " & nErr
                    dMeasAC = 9.9E+37: frmMain.txtMeasured.Text = 9.9E+37
                    Err.Raise -1000
                    Exit Function
                End If
                dMeasAC = CDbl(sData)
                frmMain.txtMeasured.Text = CSng(dMeasAC)
                If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
            Case "S"    ' Single
                frmMain.lblStatus.Caption = "Waiting for Probe ..."
                MisProbe = vbYes
                Do While MisProbe = vbYes
                    Failed = False
                    UserEvent = 0
                    frmMain.TimerProbe.Enabled = True: frmMain.fraInstructions.Visible = True
                    Do While (UserEvent = 0) And (Not bProbeClosed)
                        DoEvents
                    Loop
                    Do While (UserEvent = 0) And (bProbeClosed)
                        DoEvents
                    Loop
                    frmMain.TimerProbe.Enabled = False
                    bProbeClosed = False
                    frmMain.lblStatus.Caption = "Making Measurement ..."
                    WriteMsg DMM, "INIT;FETCH?"
                    nErr = ReadMsg(DMM, sData)
                    If nErr <> 0 Then
                        Echo "VISA ERROR: " & nErr
                        dMeasAC = 9.9E+37: frmMain.txtMeasured.Text = 9.9E+37
                        Err.Raise -1000
                        Exit Function
                    End If
                    dMeasAC = CDbl(sData)
                    frmMain.txtMeasured.Text = CSng(dMeasAC)
                    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
                    If Failed = True Then
                        MisProbe = MsgBox("Analog Probe Measurement Failed. " & vbCrLf & _
                            "Do you wish to re-probe?", vbCritical + vbYesNo + vbSystemModal, "Analog Probe Failure")
                    Else
                        MisProbe = vbNo
                    End If
                Loop
                frmMain.lblStatus.Caption = sCurrentMsg
                
                    
            Case "C"    ' Continuous
                MisProbe = vbYes
                Do While MisProbe = vbYes
                    UserEvent = 0
                    Failed = False
                    frmMain.TimerProbe.Enabled = True: frmMain.fraInstructions.Visible = True
                    Do While (UserEvent = 0) And (Not bProbeClosed)
                        frmMain.lblStatus.Caption = "Making Measurement ..."
                        WriteMsg DMM, "INIT;FETCH?"
                        nErr = ReadMsg(DMM, sData)
                        If nErr <> 0 Then
                            Echo "VISA ERROR: " & nErr
                            dMeasAC = 9.9E+37: frmMain.txtMeasured.Text = 9.9E+37
                            Err.Raise -1000
                            Exit Function
                        End If
                        dMeasAC = CDbl(sData)
                        frmMain.txtMeasured.Text = CSng(dMeasAC)
                        If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
                    Loop
                    Do While (UserEvent = 0) And (bProbeClosed)
                        DoEvents
                    Loop
                    frmMain.TimerProbe.Enabled = False
                    bProbeClosed = False
                    frmMain.lblStatus.Caption = "Measurement Complete ..."
                    If Failed = True Then
                        MisProbe = MsgBox("Analog Probe Measurement Failed. " & vbCrLf & _
                            "Do you wish to re-probe?", vbCritical + vbYesNo + vbSystemModal, "Analog Probe Failure")
                    Else
                        MisProbe = vbNo
                    End If
                Loop
        End Select
    Else
        dMeasAC = CDbl(InputBox("Command cmdDMM.dMeasAC peformed." & vbCrLf & "Enter AC Voltage Value:", "SIMULATION MODE"))
        frmMain.txtMeasured.Text = CSng(dMeasAC)
        If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON
    End If
    frmMain.lblStatus.Caption = sCurrentMsg
End Function

Public Function dMeasDC(Optional dRange As Double = 1E+99, _
                            Optional sTrigger As String = "IMMEDIATE", _
                            Optional bGT400HZ As Boolean = False, _
                            Optional dDelay As Double = 0#, _
                            Optional sAutoZero As String = "ON", _
                            Optional APROBE As String = "OFF") As Double
    Dim sData As String
    Dim nErr As Long
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    frmMain.txtInstrument.Text = "DMM"
    frmMain.txtCommand.Text = "dMeasDC"
    Dim sCurrentMsg As String
    sCurrentMsg = frmMain.lblStatus.Caption
    If APROBE <> "OFF" Then
        frmMain.lblStatus.Caption = "APPLY PROBE"
    End If
    'Setup DMM for Frequency Measurement
    If Not bSimulation Then
        WriteMsg DMM, "*CLS"
        WriteMsg DMM, "*RST"
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    If dRange = 1E+99 Then
        If Not bSimulation Then WriteMsg DMM, "CONF:VOLT:DC AUTO"
    Else
        Select Case dRange
            Case 0.1, 1, 10, 100, 300
                If Not bSimulation Then WriteMsg DMM, "CONF:VOLT:DC " & CStr(dRange)
            Case Else
                Echo "DMM PROGRAMMING ERROR:  Command cmdDMM.dMeasDC dRange argument out of range."
                dMeasDC = 9.9E+37: frmMain.txtMeasured.Text = 9.9E+37
                Err.Raise -1000
            Exit Function
        End Select
    End If
    
    'Set Bandwidth
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    If bGT400HZ And Not bSimulation Then WriteMsg DMM, "BAND:DET 400"
    'Set Trigger
    sTrigger = UCase(sTrigger)
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    Select Case sTrigger
        Case "EXTERNAL", "BUS", "TTLTRG0", "TTLTRG1", "TTLTRG2", "TTLTRG3", "TTLTRG4", "TTLTRG5", "TTLTRG6", "TTLTRG7"
            If Not bSimulation Then WriteMsg DMM, "TRIG:SOUR " & sTrigger
        Case "IMMEDIATE"
        Case Else
            Echo "DMM PROGRAMMING ERROR:  Command cmdDMM.dMeasDC sTrigger argument out of range."
            dMeasDC = 9.9E+37: frmMain.txtMeasured.Text = 9.9E+37
            Err.Raise -1000
            Exit Function
    End Select
    'Set Case AutoZero
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    sAutoZero = UCase(sAutoZero)
    Select Case sAutoZero
        Case "OFF", "ONCE"
            If Not bSimulation Then WriteMsg DMM, "CAL:ZERO:AUTO " & sAutoZero
        Case "ON"
        Case Else
            Echo "DMM PROGRAMMING ERROR:  Command cmdDMM.dMeasDC sAutoZero argument out of range."
            dMeasDC = 9.9E+37: frmMain.txtMeasured.Text = 9.9E+37
            Err.Raise -1000
            Exit Function
    End Select
    'Set Case Aprobe
    APROBE = UCase(APROBE)
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    Select Case Left(APROBE, 1)
        Case "O", "S", "C" ' Off, Single, Continuous
        Case Else
            Echo "DMM PROGRAMMING ERROR:  Command cmdDMM.dMeasFreq APROBE argument out of range."
            dMeasDC = 9.9E+37: frmMain.txtMeasured.Text = 9.9E+37
            Err.Raise -1000
            Exit Function
    End Select
    'Make Measurement
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    If Not bSimulation Then
        Select Case Left(UCase(APROBE), 1)
            Case "O"    ' Off
                WriteMsg DMM, "INIT;FETCH?"
                nErr = ReadMsg(DMM, sData)
                If nErr <> 0 Then
                    Echo "VISA ERROR: " & nErr
                    dMeasDC = 9.9E+37: frmMain.txtMeasured.Text = 9.9E+37
                    Err.Raise -1000
                    Exit Function
                End If
                dMeasDC = CDbl(sData)
                frmMain.txtMeasured.Text = CSng(dMeasDC)
                If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
            Case "S"    ' Single
                frmMain.lblStatus.Caption = "Waiting for Probe ..."
                MisProbe = vbYes
                Do While MisProbe = vbYes
                    Failed = False
                    UserEvent = 0
                    frmMain.TimerProbe.Enabled = True: frmMain.fraInstructions.Visible = True
                    Do While (UserEvent = 0) And (Not bProbeClosed)
                        DoEvents
                    Loop
                    Do While (UserEvent = 0) And (bProbeClosed)
                        DoEvents
                    Loop
                    frmMain.TimerProbe.Enabled = False
                    bProbeClosed = False
                    frmMain.lblStatus.Caption = "Making Measurement ..."
                    WriteMsg DMM, "INIT;FETCH?"
                    nErr = ReadMsg(DMM, sData)
                    If nErr <> 0 Then
                        Echo "VISA ERROR: " & nErr
                        dMeasDC = 9.9E+37: frmMain.txtMeasured.Text = 9.9E+37
                        Err.Raise -1000
                        Exit Function
                    End If
                    dMeasDC = CDbl(sData)
                    frmMain.txtMeasured.Text = CSng(dMeasDC)
                    frmMain.lblStatus.Caption = "Measurement Complete ..."
                    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
                    If Failed = True Then
                        MisProbe = MsgBox("Analog Probe Measurement Failed. " & vbCrLf & _
                            "Do you wish to re-probe?", vbCritical + vbYesNo + vbSystemModal, "Analog Probe Failure")
                    Else
                        MisProbe = vbNo
                    End If
                Loop
                frmMain.lblStatus.Caption = sCurrentMsg
                
                    
            Case "C"    ' Continuous
                MisProbe = vbYes
                Do While MisProbe = vbYes
                    UserEvent = 0
                    Failed = False
                    frmMain.TimerProbe.Enabled = True: frmMain.fraInstructions.Visible = True
                    Do While (UserEvent = 0) And (Not bProbeClosed)
                        frmMain.lblStatus.Caption = "Making Measurement ..."
                        WriteMsg DMM, "INIT;FETCH?"
                        nErr = ReadMsg(DMM, sData)
                        If nErr <> 0 Then
                            Echo "VISA ERROR: " & nErr
                            dMeasDC = 9.9E+37: frmMain.txtMeasured.Text = 9.9E+37
                            Err.Raise -1000
                            Exit Function
                        End If
                        dMeasDC = CDbl(sData)
                        frmMain.txtMeasured.Text = CSng(dMeasDC)
                        If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
                    Loop
                    Do While (UserEvent = 0) And (bProbeClosed)
                        DoEvents
                    Loop
                    frmMain.TimerProbe.Enabled = False
                    bProbeClosed = False
                    frmMain.lblStatus.Caption = "Measurement Complete ..."
                    If Failed = True Then
                        MisProbe = MsgBox("Analog Probe Measurement Failed. " & vbCrLf & _
                            "Do you wish to re-probe?", vbCritical + vbYesNo + vbSystemModal, "Analog Probe Failure")
                    Else
                        MisProbe = vbNo
                    End If
                Loop
        End Select
    Else
        dMeasDC = CDbl(InputBox("Command cmdDMM.dMeasDC peformed." & vbCrLf & "Enter DCPS Voltage Value:", "SIMULATION MODE"))
        frmMain.txtMeasured.Text = CSng(dMeasDC)
        If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON
    End If
    frmMain.lblStatus.Caption = sCurrentMsg
End Function

Public Function dMeasImp(Optional dRange As Double = 1E+99, Optional b4Wire As Boolean = False, _
                            Optional sTrigger As String = "IMMEDIATE", _
                            Optional bGT400HZ As Boolean = False, _
                            Optional dDelay As Double = -1#, _
                            Optional sAutoZero As String = "ON", _
                            Optional APROBE As String = "OFF") As Double
    
    Dim sData As String
    Dim nErr As Long
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    frmMain.txtInstrument.Text = "DMM"
    frmMain.txtCommand.Text = "dMeasImp"
   Dim sCurrentMsg As String
    sCurrentMsg = frmMain.lblStatus.Caption
    If APROBE <> "OFF" Then
        frmMain.lblStatus.Caption = "APPLY PROBE"
    End If
    If Not bSimulation Then
        WriteMsg DMM, "*CLS"
        WriteMsg DMM, "*RST"
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    If dRange = 1E+99 Then
        If Not b4Wire Then
            If Not bSimulation Then WriteMsg DMM, "CONF:RES AUTO"
        Else
            If Not bSimulation Then WriteMsg DMM, "CONF:FRES AUTO"
        End If
    Else
        Select Case dRange
            Case 100, 1000, 10000, 100000, 1000000, 10000000, 100000000
                If Not b4Wire Then
                    If Not bSimulation Then WriteMsg DMM, "CONF:RES " & CStr(dRange)
                Else
                    If Not bSimulation Then WriteMsg DMM, "CONF:FRES " & CStr(dRange)
                End If
            Case Else
                Echo "DMM PROGRAMMING ERROR:  Command cmdDMM.dMeasImp dRange argument out of range."
                dMeasImp = 9.9E+37: frmMain.txtMeasured.Text = 9.9E+37
                Err.Raise -1000
            Exit Function
        End Select
    End If
    
    'Set Bandwidth
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    If bGT400HZ And Not bSimulation Then WriteMsg DMM, "BAND:DET 400"
    
    'Set Trigger Delay
    sTrigger = UCase(sTrigger)
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    If dDelay > 0 And dDelay < 3600 Then    'Delay within range?
      If Not bSimulation Then WriteMsg DMM, "TRIG:DEL " & Str(dDelay)
    ElseIf dDelay < 0 Then
        'Do not specify a trig delay - instrument will auto select trig delay based on measurement range and NPLC
    Else
        Echo "DMM PROGRAMMING ERROR:  Command cDMM.dMeasImp Trigger delay out of range"
        dMeasImp = 9.9E+37: frmMain.txtMeasured.Text = 9.9E+37
        Err.Raise -1000
        Exit Function
    End If
    
    'Set Trigger Source
    sTrigger = UCase(sTrigger)
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    Select Case sTrigger
        Case "EXTERNAL", "BUS", "TTLTRG0", "TTLTRG1", "TTLTRG2", "TTLTRG3", "TTLTRG4", "TTLTRG5", "TTLTRG6", "TTLTRG7"
            If Not bSimulation Then WriteMsg DMM, "TRIG:SOUR " & sTrigger
        Case "IMMEDIATE"
        Case Else
            Echo "DMM PROGRAMMING ERROR:  Command cmdDMM.dMeasImp sTrigger argument out of range."
                dMeasImp = 9.9E+37: frmMain.txtMeasured.Text = 9.9E+37
                Err.Raise -1000
            Exit Function
    End Select
    'Set Case AutoZero
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    sAutoZero = UCase(sAutoZero)
    Select Case sAutoZero
        Case "OFF", "ONCE"
            If Not bSimulation Then WriteMsg DMM, "CAL:ZERO:AUTO " & sAutoZero
        Case "ON"
        Case Else
            Echo "DMM PROGRAMMING ERROR:  Command cmdDMM.dMeasImp sAutoZero argument out of range."
                dMeasImp = 9.9E+37: frmMain.txtMeasured.Text = 9.9E+37
                Err.Raise -1000
            Exit Function
    End Select
    'Make Measurement
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    If Not bSimulation Then
        Select Case Left(UCase(APROBE), 1)
            Case "O"    ' Off
                WriteMsg DMM, "INIT"
                If dDelay <> -1 Then
                   Delay dDelay + 0.1
                End If
                WriteMsg DMM, "FETCH?"
                nErr = ReadMsg(DMM, sData)
                If nErr <> 0 Then
                    Echo "VISA ERROR: " & nErr
                    dMeasImp = 9.9E+37: frmMain.txtMeasured.Text = 9.9E+37
                    Err.Raise -1000
                    Exit Function
                End If
                dMeasImp = CDbl(sData)
                frmMain.txtMeasured.Text = CSng(dMeasImp)
                If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
            Case "S"    ' Single
                frmMain.lblStatus.Caption = "Waiting for Probe ..."
                MisProbe = vbYes
                Do While MisProbe = vbYes
                    Failed = False
                    UserEvent = 0
                    frmMain.TimerProbe.Enabled = True: frmMain.fraInstructions.Visible = True
                    Do While (UserEvent = 0) And (Not bProbeClosed)
                        DoEvents
                    Loop
                    Do While (UserEvent = 0) And (bProbeClosed)
                        DoEvents
                    Loop
                    frmMain.TimerProbe.Enabled = False
                    bProbeClosed = False
                    frmMain.lblStatus.Caption = "Making Measurement ..."
                    WriteMsg DMM, "INIT"
                    If dDelay <> -1 Then
                       Delay dDelay + 0.1
                    End If
                    WriteMsg DMM, "FETCH?"
                    nErr = ReadMsg(DMM, sData)
                    If nErr <> 0 Then
                        Echo "VISA ERROR: " & nErr
                        dMeasImp = 9.9E+37: frmMain.txtMeasured.Text = 9.9E+37
                        Err.Raise -1000
                        Exit Function
                    End If
                    dMeasImp = CDbl(sData)
                    frmMain.txtMeasured.Text = CSng(dMeasImp)
                    frmMain.lblStatus.Caption = "Measurement Complete ..."
                    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
                    If Failed = True Then
                        MisProbe = MsgBox("Analog Probe Measurement Failed. " & vbCrLf & _
                            "Do you wish to re-probe?", vbCritical + vbYesNo + vbSystemModal, "Analog Probe Failure")
                    Else
                        MisProbe = vbNo
                    End If
                Loop
                frmMain.lblStatus.Caption = sCurrentMsg
                
                    
            Case "C"    ' Continuous
                MisProbe = vbYes
                Do While MisProbe = vbYes
                    UserEvent = 0
                    Failed = False
                    frmMain.TimerProbe.Enabled = True: frmMain.fraInstructions.Visible = True
                    Do While (UserEvent = 0) And (Not bProbeClosed)
                        frmMain.lblStatus.Caption = "Making Measurement ..."
                        WriteMsg DMM, "INIT"
                        If dDelay <> -1 Then
                           Delay dDelay + 0.1
                        End If
                        WriteMsg DMM, "FETCH?"
                        nErr = ReadMsg(DMM, sData)
                        If nErr <> 0 Then
                            Echo "VISA ERROR: " & nErr
                            dMeasImp = 9.9E+37: frmMain.txtMeasured.Text = 9.9E+37
                            Err.Raise -1000
                            Exit Function
                        End If
                        dMeasImp = CDbl(sData)
                        frmMain.txtMeasured.Text = CSng(dMeasImp)
                        If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
                    Loop
                    Do While (UserEvent = 0) And (bProbeClosed)
                        DoEvents
                    Loop
                    frmMain.TimerProbe.Enabled = False
                    bProbeClosed = False
                    frmMain.lblStatus.Caption = "Measurement Complete ..."
                    If Failed = True Then
                        MisProbe = MsgBox("Analog Probe Measurement Failed. " & vbCrLf & _
                            "Do you wish to re-probe?", vbCritical + vbYesNo + vbSystemModal, "Analog Probe Failure")
                    Else
                        MisProbe = vbNo
                    End If
                Loop
        End Select
    Else
        dMeasImp = CDbl(InputBox("Command cmdDMM.dMeasimp peformed." & vbCrLf & "Enter Impedance Value:", "SIMULATION MODE"))
        frmMain.txtMeasured.Text = CSng(dMeasImp)
        If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON
    End If
    frmMain.lblStatus.Caption = sCurrentMsg
End Function

