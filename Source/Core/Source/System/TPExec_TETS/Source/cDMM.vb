Option Strict Off
Option Explicit On
<System.Runtime.InteropServices.ProgId("cDMM_NET.cDMM")> Public Class cDMM

    Public Sub SendMsg(ByRef sMsg As String)
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        gFrmMain.txtInstrument.Text = "DMM"
        gFrmMain.txtCommand.Text = "SendMsg"
        WriteMsg(DMM, sMsg)
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON)
    End Sub

    Public Function sReadMsg(ByRef sMsg As String) As String
        sReadMsg = ""
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        gFrmMain.txtInstrument.Text = "DMM"
        gFrmMain.txtCommand.Text = "ReadMsg"
        ReadMsg(DMM, sReadMsg)
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON)
    End Function

    Public Function dMeasFreq(Optional ByRef sTrigger As String = "IMMEDIATE",
                              Optional ByRef bGT400HZ As Boolean = False, Optional ByRef dDelay As Double = 0.0,
                              Optional ByRef sAutoZero As String = "ON", Optional ByRef APROBE As String = "OFF") As Double
        Dim sData As String = ""
        Dim nErr As Integer
        Dim sCurrentMsg As String

        dMeasFreq = 0.0
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function

        gFrmMain.txtInstrument.Text = "DMM"
        gFrmMain.txtCommand.Text = "dMeasFreq"
        sCurrentMsg = gFrmMain.lblStatus.Text

        If APROBE <> "OFF" Then
            gFrmMain.lblStatus.Text = "APPLY PROBE"
        End If
        'Setup DMM for Frequency Measurement
        If Not bSimulation Then
            WriteMsg(DMM, "*CLS")
            WriteMsg(DMM, "*RST")
            WriteMsg(DMM, "CONF:FREQ AUTO")
        End If

        'Set Bandwidth
        If bGT400HZ And Not bSimulation Then WriteMsg(DMM, "BAND:DET 400")

        'Set Trigger
        sTrigger = UCase(sTrigger)
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function

        Select Case sTrigger
            Case "EXTERNAL", "BUS", "TTLTRG0", "TTLTRG1", "TTLTRG2", "TTLTRG3", "TTLTRG4", "TTLTRG5", "TTLTRG6", "TTLTRG7"
                If Not bSimulation Then WriteMsg(DMM, "TRIG:SOUR " & sTrigger)
            Case "IMMEDIATE"
            Case Else
                Echo("DMM PROGRAMMING ERROR:  Command cmdDMM.dMeasFreq sTrigger argument out of range.")
                dMeasFreq = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1000)
                dMeasFreq = 0
                Exit Function
        End Select

        'Set Case AutoZero
        sAutoZero = UCase(sAutoZero)
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function

        Select Case sAutoZero
            Case "OFF", "ONCE"
                If Not bSimulation Then WriteMsg(DMM, "CAL:ZERO:AUTO " & sAutoZero)
            Case "ON"
            Case Else
                Echo("DMM PROGRAMMING ERROR:  Command cmdDMM.dMeasFreq sAutoZero argument out of range.")
                Err.Raise(-1001)
                dMeasFreq = 0
                Exit Function
        End Select

        'Set Case Aprobe
        APROBE = UCase(APROBE)
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function

        Select Case Left(APROBE, 1)
            Case "O", "S", "C" ' Off, Single, Continuous
            Case Else
                Echo("DMM PROGRAMMING ERROR:  Command cmdDMM.dMeasFreq APROBE argument out of range.")
                Err.Raise(-1001)
                dMeasFreq = 0
                Exit Function
        End Select

        'Make Measurement
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function

        If Not bSimulation Then
            Select Case Left(UCase(APROBE), 1)
                Case "O" ' Off
                    WriteMsg(DMM, "INIT;FETCH?")
                    nErr = ReadMsg(DMM, sData)
                    If nErr <> 0 Then
                        'Echo "VISA ERROR: " & nErr
                        dMeasFreq = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37)
                        Err.Raise(nErr)
                        Exit Function
                    End If

                    dMeasFreq = CDbl(sData)
                    gFrmMain.txtMeasured.Text = CStr(CSng(dMeasFreq))
                    If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
                Case "S" ' Single
                    gFrmMain.lblStatus.Text = "Waiting for Probe ..."
                    MisProbe = MsgBoxResult.Yes
                    Do While MisProbe = MsgBoxResult.Yes
                        Failed = False
                        UserEvent = 0
                        gFrmMain.TimerProbe.Enabled = True : gFrmMain.fraInstructions.Visible = True
                        Do While (UserEvent = 0) And (Not bProbeClosed)
                            System.Windows.Forms.Application.DoEvents()
                        Loop

                        gFrmMain.TimerProbe.Enabled = False
                        bProbeClosed = False
                        gFrmMain.lblStatus.Text = "Making Measurement ..."
                        WriteMsg(DMM, "INIT;FETCH?")
                        nErr = ReadMsg(DMM, sData)
                        If nErr <> 0 Then
                            'Echo "VISA ERROR: " & nErr
                            dMeasFreq = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37)
                            Err.Raise(nErr)
                            Exit Function
                        End If

                        dMeasFreq = CDbl(sData)
                        gFrmMain.txtMeasured.Text = CStr(CSng(dMeasFreq))
                        gFrmMain.lblStatus.Text = "Measurement Complete ..."
                        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function

                        If Failed = True Then
                            MisProbe = MsgBox("Analog Probe Measurement Failed. " & vbCrLf & "Do you wish to re-probe?",
                                              MsgBoxStyle.Critical + MsgBoxStyle.YesNo + MsgBoxStyle.SystemModal,
                                              "Analog Probe Failure")
                        Else
                            MisProbe = MsgBoxResult.No
                        End If
                    Loop
                    gFrmMain.lblStatus.Text = sCurrentMsg

                Case "C" ' Continuous
                    MisProbe = MsgBoxResult.Yes
                    Do While MisProbe = MsgBoxResult.Yes
                        UserEvent = 0
                        Failed = False
                        gFrmMain.TimerProbe.Enabled = True : gFrmMain.fraInstructions.Visible = True
                        Do While (UserEvent = 0) And (Not bProbeClosed)
                            gFrmMain.lblStatus.Text = "Making Measurement ..."
                            WriteMsg(DMM, "INIT;FETCH?")
                            nErr = ReadMsg(DMM, sData)
                            If nErr <> 0 Then
                                'Echo "VISA ERROR: " & nErr
                                dMeasFreq = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37)
                                gFrmMain.txtMeasured.Text = CStr(dMeasFreq)
                                Failed = True
                                Pass = False
                                Err.Raise(nErr)
                                Exit Function
                            End If

                            dMeasFreq = CDbl(sData)
                            gFrmMain.txtMeasured.Text = CStr(CSng(dMeasFreq))
                            If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
                        Loop

                        gFrmMain.TimerProbe.Enabled = False
                        bProbeClosed = False
                        gFrmMain.lblStatus.Text = "Measurement Complete ..."
                        If Failed = True Then
                            MisProbe = MsgBox("Analog Probe Measurement Failed. " & vbCrLf & "Do you wish to re-probe?",
                                              MsgBoxStyle.Critical + MsgBoxStyle.YesNo + MsgBoxStyle.SystemModal,
                                              "Analog Probe Failure")
                        Else
                            MisProbe = MsgBoxResult.No
                        End If
                    Loop
            End Select
        Else
            dMeasFreq = CDbl(InputBox("Command cmdDMM.dMeasFreq peformed." & vbCrLf & "Enter Frequency Value:", "SIMULATION MODE"))
            gFrmMain.txtMeasured.Text = CStr(CSng(dMeasFreq))
            If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON)
        End If
        gFrmMain.lblStatus.Text = sCurrentMsg
    End Function

    Public Function dMeasPeriod(Optional ByRef sTrigger As String = "IMMEDIATE",
                                Optional ByRef bGT400HZ As Boolean = False,
                                Optional ByRef dDelay As Double = 0.0,
                                Optional ByRef sAutoZero As String = "ON",
                                Optional ByRef APROBE As String = "OFF") As Double
        Dim sData As String = ""
        Dim nErr As Integer

        dMeasPeriod = 0.0
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        gFrmMain.txtInstrument.Text = "DMM"
        gFrmMain.txtCommand.Text = "dMeasPeriod"

        Dim sCurrentMsg As String
        sCurrentMsg = gFrmMain.lblStatus.Text

        If APROBE <> "OFF" Then
            gFrmMain.lblStatus.Text = "APPLY PROBE"
        End If

        'Setup DMM for Frequency Measurement
        If Not bSimulation Then
            WriteMsg(DMM, "*CLS")
            WriteMsg(DMM, "*RST")
            WriteMsg(DMM, "CONF:PER AUTO")
        End If

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function

        'Set Bandwidth
        If bGT400HZ And Not bSimulation Then WriteMsg(DMM, "BAND:DET 400")

        'Set Trigger
        sTrigger = UCase(sTrigger)

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function

        Select Case sTrigger
            Case "EXTERNAL", "BUS", "TTLTRG0", "TTLTRG1", "TTLTRG2", "TTLTRG3", "TTLTRG4", "TTLTRG5", "TTLTRG6", "TTLTRG7"
                If Not bSimulation Then WriteMsg(DMM, "TRIG:SOUR " & sTrigger)
            Case "IMMEDIATE"
            Case Else
                Echo("DMM PROGRAMMING ERROR:  Command cmdDMM.dMeasFreq sTrigger argument out of range.")
                dMeasPeriod = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37)
                Err.Raise(-1000)
                Exit Function
        End Select

        'Set Case AutoZero
        sAutoZero = UCase(sAutoZero)
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function

        Select Case sAutoZero
            Case "OFF", "ONCE"
                If Not bSimulation Then WriteMsg(DMM, "CAL:ZERO:AUTO " & sAutoZero)
            Case "on"
            Case Else
                Echo("DMM PROGRAMMING ERROR:  Command cmdDMM.dMeasPeriod sAutoZero argument out of range.")
                dMeasPeriod = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37)
                Err.Raise(-1000)
                Exit Function
        End Select

        'Set Case Aprobe
        APROBE = UCase(APROBE)
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function

        Select Case Left(APROBE, 1)
            Case "O", "S", "C" ' Off, Single, Continuous
            Case Else
                Echo("DMM PROGRAMMING ERROR:  Command cmdDMM.dMeasPeriod APROBE argument out of range.")
                dMeasPeriod = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37)
                Err.Raise(-1000)
                Exit Function
        End Select

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function

        'Make Measurement
        If Not bSimulation Then
            Select Case Left(UCase(APROBE), 1)
                Case "O" ' Off
                    WriteMsg(DMM, "INIT;FETCH?")
                    nErr = ReadMsg(DMM, sData)
                    If nErr <> 0 Then
                        Echo("VISA ERROR: " & nErr)
                        dMeasPeriod = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37)
                        Err.Raise(-1000)
                        Exit Function
                    End If

                    dMeasPeriod = CDbl(sData)
                    gFrmMain.txtMeasured.Text = CStr(CSng(dMeasPeriod))
                    If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
                Case "S" ' Single
                    gFrmMain.lblStatus.Text = "Waiting for Probe ..."
                    MisProbe = MsgBoxResult.Yes
                    Do While MisProbe = MsgBoxResult.Yes
                        Failed = False
                        UserEvent = 0
                        gFrmMain.TimerProbe.Enabled = True : gFrmMain.fraInstructions.Visible = True
                        Do While (UserEvent = 0) And (Not bProbeClosed)
                            System.Windows.Forms.Application.DoEvents()
                        Loop

                        gFrmMain.TimerProbe.Enabled = False
                        bProbeClosed = False
                        gFrmMain.lblStatus.Text = "Making Measurement ..."
                        WriteMsg(DMM, "INIT;FETCH?")
                        nErr = ReadMsg(DMM, sData)
                        If nErr <> 0 Then
                            Echo("VISA ERROR: " & nErr)
                            dMeasPeriod = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37)
                            Err.Raise(-1000)
                            Exit Function
                        End If

                        dMeasPeriod = CDbl(sData)
                        gFrmMain.txtMeasured.Text = CStr(CSng(dMeasPeriod))
                        gFrmMain.lblStatus.Text = "Measurement Complete ..."
                        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
                        If Failed = True Then
                            MisProbe = MsgBox("Analog Probe Measurement Failed. " & vbCrLf & "Do you wish to re-probe?",
                                              MsgBoxStyle.Critical + MsgBoxStyle.YesNo + MsgBoxStyle.SystemModal,
                                              "Analog Probe Failure")
                        Else
                            MisProbe = MsgBoxResult.No
                        End If
                    Loop
                    gFrmMain.lblStatus.Text = sCurrentMsg

                Case "C" ' Continuous
                    MisProbe = MsgBoxResult.Yes
                    Do While MisProbe = MsgBoxResult.Yes
                        UserEvent = 0
                        Failed = False
                        gFrmMain.TimerProbe.Enabled = True : gFrmMain.fraInstructions.Visible = True
                        Do While (UserEvent = 0) And (Not bProbeClosed)
                            gFrmMain.lblStatus.Text = "Making Measurement ..."
                            WriteMsg(DMM, "INIT;FETCH?")
                            nErr = ReadMsg(DMM, sData)
                            If nErr <> 0 Then
                                Echo("VISA ERROR: " & nErr)
                                dMeasPeriod = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37)
                                Err.Raise(-1000)
                                Exit Function
                            End If

                            dMeasPeriod = CDbl(sData)
                            gFrmMain.txtMeasured.Text = CStr(CSng(dMeasPeriod))
                            If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
                        Loop

                        gFrmMain.TimerProbe.Enabled = False
                        bProbeClosed = False
                        gFrmMain.lblStatus.Text = "Measurement Complete ..."
                        If Failed = True Then
                            MisProbe = MsgBox("Analog Probe Measurement Failed. " & vbCrLf & "Do you wish to re-probe?",
                                              MsgBoxStyle.Critical + MsgBoxStyle.YesNo + MsgBoxStyle.SystemModal,
                                              "Analog Probe Failure")
                        Else
                            MisProbe = MsgBoxResult.No
                        End If
                    Loop
            End Select
        Else
            dMeasPeriod = CDbl(InputBox("Command cmdDMM.dMeasPeriod peformed." & vbCrLf & "Enter Period Value:", "SIMULATION MODE"))
            gFrmMain.txtMeasured.Text = CStr(CSng(dMeasPeriod))
            If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON)
        End If
        gFrmMain.lblStatus.Text = sCurrentMsg
    End Function

    Public Function dMeasAC(Optional ByRef dRange As Double = 1.0E+99,
                            Optional ByRef sTrigger As String = "IMMEDIATE",
                            Optional ByRef bGT400HZ As Boolean = False,
                            Optional ByRef dDelay As Double = 0.0,
                            Optional ByRef sAutoZero As String = "ON",
                            Optional ByRef APROBE As String = "OFF") As Double
        Dim sData As String = ""
        Dim nErr As Integer

        dMeasAC = 0.0
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function

        gFrmMain.txtInstrument.Text = "DMM"
        gFrmMain.txtCommand.Text = "dMeasAC"
        Dim sCurrentMsg As String
        sCurrentMsg = gFrmMain.lblStatus.Text
        If APROBE <> "OFF" Then
            gFrmMain.lblStatus.Text = "APPLY PROBE"
        End If

        'Setup DMM for Frequency Measurement
        If Not bSimulation Then
            WriteMsg(DMM, "*CLS")
            WriteMsg(DMM, "*RST")
        End If

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function

        If dRange = 1.0E+99 Then
            If Not bSimulation Then WriteMsg(DMM, "CONF:VOLT:AC AUTO")
        Else
            Select Case dRange
                Case 0.1, 1, 10, 100, 300
                    If Not bSimulation Then WriteMsg(DMM, "CONF:VOLT:AC " & CStr(dRange))
                Case Else
                    Echo("DMM PROGRAMMING ERROR:  Command cmdDMM.dMeasAC dRange argument out of range.")
                    dMeasAC = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37)
                    Err.Raise(-1000)
                    Exit Function
            End Select
        End If

        'Set Bandwidth
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function

        If bGT400HZ And Not bSimulation Then WriteMsg(DMM, "BAND:DET 400")

        'Set Trigger
        sTrigger = UCase(sTrigger)
        Select Case sTrigger
            Case "EXTERNAL", "BUS", "TTLTRG0", "TTLTRG1", "TTLTRG2", "TTLTRG3", "TTLTRG4", "TTLTRG5", "TTLTRG6", "TTLTRG7"
                If Not bSimulation Then WriteMsg(DMM, "TRIG:SOUR " & sTrigger)
            Case "IMMEDIATE"
            Case Else
                Echo("DMM PROGRAMMING ERROR:  Command cmdDMM.dMeasAC sTrigger argument out of range.")
                dMeasAC = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37)
                Err.Raise(-1000)
                Exit Function
        End Select

        'Set Case AutoZero
        sAutoZero = UCase(sAutoZero)
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        Select Case sAutoZero
            Case "OFF", "ONCE"
                If Not bSimulation Then WriteMsg(DMM, "CAL:ZERO:AUTO " & sAutoZero)
            Case "ON"
            Case Else
                Echo("DMM PROGRAMMING ERROR:  Command cmdDMM.dMeasAC sAutoZero argument out of range.")
                dMeasAC = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37)
                Err.Raise(-1000)
                Exit Function
        End Select

        'Set Case Aprobe
        APROBE = UCase(APROBE)
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function

        Select Case Left(APROBE, 1)
            Case "O", "S", "C" ' Off, Single, Continuous
            Case Else
                Echo("DMM PROGRAMMING ERROR:  Command cmdDMM.dMeasAC APROBE argument out of range.")
                dMeasAC = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37)
                Err.Raise(-1000)
                Exit Function
        End Select

        'Make Measurement
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function

        If Not bSimulation Then
            Select Case Left(UCase(APROBE), 1)
                Case "O" ' Off
                    WriteMsg(DMM, "INIT;FETCH?")
                    nErr = ReadMsg(DMM, sData)
                    If nErr <> 0 Then
                        Echo("VISA ERROR: " & nErr)
                        dMeasAC = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37)
                        Err.Raise(-1000)
                        Exit Function
                    End If

                    dMeasAC = CDbl(sData)
                    gFrmMain.txtMeasured.Text = CStr(CSng(dMeasAC))
                    If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
                Case "S" ' Single
                    gFrmMain.lblStatus.Text = "Waiting for Probe ..."
                    MisProbe = MsgBoxResult.Yes
                    Do While MisProbe = MsgBoxResult.Yes
                        Failed = False
                        UserEvent = 0
                        gFrmMain.TimerProbe.Enabled = True : gFrmMain.fraInstructions.Visible = True
                        Do While (UserEvent = 0) And (Not bProbeClosed)
                            System.Windows.Forms.Application.DoEvents()
                        Loop

                        gFrmMain.TimerProbe.Enabled = False
                        bProbeClosed = False
                        gFrmMain.lblStatus.Text = "Making Measurement ..."
                        WriteMsg(DMM, "INIT;FETCH?")
                        nErr = ReadMsg(DMM, sData)
                        If nErr <> 0 Then
                            Echo("VISA ERROR: " & nErr)
                            dMeasAC = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37)
                            Err.Raise(-1000)
                            Exit Function
                        End If

                        dMeasAC = CDbl(sData)
                        gFrmMain.txtMeasured.Text = CStr(CSng(dMeasAC))
                        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
                        If Failed = True Then
                            MisProbe = MsgBox("Analog Probe Measurement Failed. " & vbCrLf & "Do you wish to re-probe?",
                                              MsgBoxStyle.Critical + MsgBoxStyle.YesNo + MsgBoxStyle.SystemModal,
                                              "Analog Probe Failure")
                        Else
                            MisProbe = MsgBoxResult.No
                        End If
                    Loop
                    gFrmMain.lblStatus.Text = sCurrentMsg

                Case "C" ' Continuous
                    MisProbe = MsgBoxResult.Yes
                    Do While MisProbe = MsgBoxResult.Yes
                        UserEvent = 0
                        Failed = False
                        gFrmMain.TimerProbe.Enabled = True : gFrmMain.fraInstructions.Visible = True
                        Do While (UserEvent = 0) And (Not bProbeClosed)
                            gFrmMain.lblStatus.Text = "Making Measurement ..."
                            WriteMsg(DMM, "INIT;FETCH?")
                            nErr = ReadMsg(DMM, sData)
                            If nErr <> 0 Then
                                Echo("VISA ERROR: " & nErr)
                                dMeasAC = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37)
                                Err.Raise(-1000)
                                Exit Function
                            End If

                            dMeasAC = CDbl(sData)
                            gFrmMain.txtMeasured.Text = CStr(CSng(dMeasAC))
                            If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
                        Loop

                        gFrmMain.TimerProbe.Enabled = False
                        bProbeClosed = False
                        gFrmMain.lblStatus.Text = "Measurement Complete ..."
                        If Failed = True Then
                            MisProbe = MsgBox("Analog Probe Measurement Failed. " & vbCrLf & "Do you wish to re-probe?",
                                              MsgBoxStyle.Critical + MsgBoxStyle.YesNo + MsgBoxStyle.SystemModal,
                                              "Analog Probe Failure")
                        Else
                            MisProbe = MsgBoxResult.No
                        End If
                    Loop
            End Select
        Else
            dMeasAC = CDbl(InputBox("Command cmdDMM.dMeasAC peformed." & vbCrLf & "Enter AC Voltage Value:", "SIMULATION MODE"))
            gFrmMain.txtMeasured.Text = CStr(CSng(dMeasAC))
            If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON)
        End If
        gFrmMain.lblStatus.Text = sCurrentMsg
    End Function

    Public Function dMeasDC(Optional ByRef dRange As Double = 1.0E+99,
                            Optional ByRef sTrigger As String = "IMMEDIATE",
                            Optional ByRef bGT400HZ As Boolean = False,
                            Optional ByRef dDelay As Double = 0.0,
                            Optional ByRef sAutoZero As String = "ON",
                            Optional ByRef APROBE As String = "OFF") As Double
        Dim sData As String = ""
        Dim nErr As Integer

        dMeasDC = 0.0
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function

        gFrmMain.txtInstrument.Text = "DMM"
        gFrmMain.txtCommand.Text = "dMeasDC"
        Dim sCurrentMsg As String
        sCurrentMsg = gFrmMain.lblStatus.Text
        If APROBE <> "OFF" Then
            gFrmMain.lblStatus.Text = "APPLY PROBE"
        End If

        'Setup DMM for Frequency Measurement
        If Not bSimulation Then
            WriteMsg(DMM, "*CLS")
            WriteMsg(DMM, "*RST")
        End If

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        If dRange = 1.0E+99 Then
            If Not bSimulation Then WriteMsg(DMM, "CONF:VOLT:DC AUTO")
        Else
            Select Case dRange
                Case 0.1, 1, 10, 100, 300
                    If Not bSimulation Then WriteMsg(DMM, "CONF:VOLT:DC " & CStr(dRange))
                Case Else
                    Echo("DMM PROGRAMMING ERROR:  Command cmdDMM.dMeasDC dRange argument out of range.")
                    dMeasDC = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37)
                    Err.Raise(-1000)
                    Exit Function
            End Select
        End If

        'Set Bandwidth
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function

        If bGT400HZ And Not bSimulation Then WriteMsg(DMM, "BAND:DET 400")

        'Set Trigger
        sTrigger = UCase(sTrigger)
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function

        Select Case sTrigger
            Case "EXTERNAL", "BUS", "TTLTRG0", "TTLTRG1", "TTLTRG2", "TTLTRG3", "TTLTRG4", "TTLTRG5", "TTLTRG6", "TTLTRG7"
                If Not bSimulation Then WriteMsg(DMM, "TRIG:SOUR " & sTrigger)
            Case "IMMEDIATE"
            Case Else
                Echo("DMM PROGRAMMING ERROR:  Command cmdDMM.dMeasDC sTrigger argument out of range.")
                dMeasDC = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37)
                Err.Raise(-1000)
                Exit Function
        End Select

        'Set Case AutoZero
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        sAutoZero = UCase(sAutoZero)
        Select Case sAutoZero
            Case "OFF", "ONCE"
                If Not bSimulation Then WriteMsg(DMM, "CAL:ZERO:AUTO " & sAutoZero)
            Case "ON"
            Case Else
                Echo("DMM PROGRAMMING ERROR:  Command cmdDMM.dMeasDC sAutoZero argument out of range.")
                dMeasDC = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37)
                Err.Raise(-1000)
                Exit Function
        End Select

        'Set Case Aprobe
        APROBE = UCase(APROBE)
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        Select Case Left(APROBE, 1)
            Case "O", "S", "C" ' Off, Single, Continuous
            Case Else
                Echo("DMM PROGRAMMING ERROR:  Command cmdDMM.dMeasFreq APROBE argument out of range.")
                dMeasDC = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37)
                Err.Raise(-1000)
                Exit Function
        End Select

        'Make Measurement
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        If Not bSimulation Then
            Select Case Left(UCase(APROBE), 1)
                Case "O" ' Off
                    WriteMsg(DMM, "INIT;FETCH?")
                    nErr = ReadMsg(DMM, sData)
                    If nErr <> 0 Then
                        Echo("VISA ERROR: " & nErr)
                        dMeasDC = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37)
                        Err.Raise(-1000)
                        Exit Function
                    End If

                    dMeasDC = CDbl(sData)
                    gFrmMain.txtMeasured.Text = CStr(CSng(dMeasDC))
                    If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
                Case "S" ' Single
                    gFrmMain.lblStatus.Text = "Waiting for Probe ..."
                    MisProbe = MsgBoxResult.Yes
                    Do While MisProbe = MsgBoxResult.Yes
                        Failed = False
                        UserEvent = 0
                        gFrmMain.TimerProbe.Enabled = True : gFrmMain.fraInstructions.Visible = True
                        Do While (UserEvent = 0) And (Not bProbeClosed)
                            System.Windows.Forms.Application.DoEvents()
                        Loop

                        gFrmMain.TimerProbe.Enabled = False
                        bProbeClosed = False
                        gFrmMain.lblStatus.Text = "Making Measurement ..."
                        WriteMsg(DMM, "INIT;FETCH?")
                        nErr = ReadMsg(DMM, sData)
                        If nErr <> 0 Then
                            Echo("VISA ERROR: " & nErr)
                            dMeasDC = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37)
                            Err.Raise(-1000)
                            Exit Function
                        End If

                        dMeasDC = CDbl(sData)
                        gFrmMain.txtMeasured.Text = CStr(CSng(dMeasDC))
                        gFrmMain.lblStatus.Text = "Measurement Complete ..."
                        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
                        If Failed = True Then
                            MisProbe = MsgBox("Analog Probe Measurement Failed. " & vbCrLf & "Do you wish to re-probe?",
                                              MsgBoxStyle.Critical + MsgBoxStyle.YesNo + MsgBoxStyle.SystemModal,
                                              "Analog Probe Failure")
                        Else
                            MisProbe = MsgBoxResult.No
                        End If
                    Loop
                    gFrmMain.lblStatus.Text = sCurrentMsg

                Case "C" ' Continuous
                    MisProbe = MsgBoxResult.Yes
                    Do While MisProbe = MsgBoxResult.Yes
                        UserEvent = 0
                        Failed = False
                        gFrmMain.TimerProbe.Enabled = True : gFrmMain.fraInstructions.Visible = True
                        Do While (UserEvent = 0) And (Not bProbeClosed)
                            gFrmMain.lblStatus.Text = "Making Measurement ..."
                            WriteMsg(DMM, "INIT;FETCH?")
                            nErr = ReadMsg(DMM, sData)
                            If nErr <> 0 Then
                                Echo("VISA ERROR: " & nErr)
                                dMeasDC = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37)
                                Err.Raise(-1000)
                                Exit Function
                            End If

                            dMeasDC = CDbl(sData)
                            gFrmMain.txtMeasured.Text = CStr(CSng(dMeasDC))
                            If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
                        Loop

                        gFrmMain.TimerProbe.Enabled = False
                        bProbeClosed = False
                        gFrmMain.lblStatus.Text = "Measurement Complete ..."
                        If Failed = True Then
                            MisProbe = MsgBox("Analog Probe Measurement Failed. " & vbCrLf & "Do you wish to re-probe?",
                                              MsgBoxStyle.Critical + MsgBoxStyle.YesNo + MsgBoxStyle.SystemModal,
                                              "Analog Probe Failure")
                        Else
                            MisProbe = MsgBoxResult.No
                        End If
                    Loop
            End Select
        Else
            dMeasDC = CDbl(InputBox("Command cmdDMM.dMeasDC peformed." & vbCrLf & "Enter DCPS Voltage Value:", "SIMULATION MODE"))
            gFrmMain.txtMeasured.Text = CStr(CSng(dMeasDC))
            If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON)
        End If
        gFrmMain.lblStatus.Text = sCurrentMsg
    End Function

    Public Function dMeasImp(Optional ByRef dRange As Double = 1.0E+99,
                             Optional ByRef b4Wire As Boolean = False,
                             Optional ByRef sTrigger As String = "IMMEDIATE",
                             Optional ByRef bGT400HZ As Boolean = False,
                             Optional ByRef dDelay As Double = -1.0,
                             Optional ByRef sAutoZero As String = "ON",
                             Optional ByRef APROBE As String = "OFF") As Double
        '***********************************************
        '*** Code Added To Supplort Settling Delay *****
        '*** Craig Weirich 4/15/03 *********************
        '***********************************************

        ' Set Default for optional parameter dDelay (above)
        ' change from ====> Optional dDelay As Double = 0,
        ' to =============> Optional dDelay As Double = -1,

        '***********************************************
        '*** End of Modified Code **********************
        '***********************************************
        Dim sData As String = ""
        Dim nErr As Integer

        dMeasImp = 0.0
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function

        gFrmMain.txtInstrument.Text = "DMM"
        gFrmMain.txtCommand.Text = "dMeasImp"
        Dim sCurrentMsg As String
        sCurrentMsg = gFrmMain.lblStatus.Text
        If APROBE <> "OFF" Then
            gFrmMain.lblStatus.Text = "APPLY PROBE"
        End If

        If Not bSimulation Then
            WriteMsg(DMM, "*CLS")
            WriteMsg(DMM, "*RST")
        End If

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function

        If dRange = 1.0E+99 Then
            If Not b4Wire Then
                If Not bSimulation Then WriteMsg(DMM, "CONF:RES AUTO")
            Else
                If Not bSimulation Then WriteMsg(DMM, "CONF:FRES AUTO")
            End If
        Else
            Select Case dRange
                Case 100, 1000, 10000, 100000, 1000000, 10000000, 100000000
                    If Not b4Wire Then
                        If Not bSimulation Then WriteMsg(DMM, "CONF:RES " & CStr(dRange))
                    Else
                        If Not bSimulation Then WriteMsg(DMM, "CONF:FRES " & CStr(dRange))
                    End If
                Case Else
                    Echo("DMM PROGRAMMING ERROR:  Command cmdDMM.dMeasImp dRange argument out of range.")
                    dMeasImp = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37)
                    Err.Raise(-1000)
                    Exit Function
            End Select
        End If

        'Set Bandwidth
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        If bGT400HZ And Not bSimulation Then WriteMsg(DMM, "BAND:DET 400")

        '***********************************************
        '*** Code Added To Support Settling Delay ******
        '*** Craig Weirich 4/15/03 *********************
        '***********************************************
        'Set Trigger Delay
        sTrigger = UCase(sTrigger)
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        If dDelay > 0 And dDelay < 3600 Then 'Delay within range?
            If Not bSimulation Then WriteMsg(DMM, "TRIG:DEL " & Str(dDelay))
        ElseIf dDelay < 0 Then
            'Do not specify a trig delay - instrument will auto select trig delay based on measurement range and NPLC
        Else
            Echo("DMM PROGRAMMING ERROR:  Command cDMM.dMeasImp Trigger delay out of range")
            dMeasImp = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37)
            Err.Raise(-1000)
            Exit Function
        End If

        '***********************************************
        '*** End of Modified Code **********************
        '***********************************************

        'Set Trigger Source
        sTrigger = UCase(sTrigger)
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        Select Case sTrigger
            Case "EXTERNAL", "BUS", "TTLTRG0", "TTLTRG1", "TTLTRG2", "TTLTRG3", "TTLTRG4", "TTLTRG5", "TTLTRG6", "TTLTRG7"
                If Not bSimulation Then WriteMsg(DMM, "TRIG:SOUR " & sTrigger)
            Case "IMMEDIATE"
            Case Else
                Echo("DMM PROGRAMMING ERROR:  Command cmdDMM.dMeasImp sTrigger argument out of range.")
                dMeasImp = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37)
                Err.Raise(-1000)
                Exit Function
        End Select

        'Set Case AutoZero
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function

        sAutoZero = UCase(sAutoZero)
        Select Case sAutoZero
            Case "OFF", "ONCE"
                If Not bSimulation Then WriteMsg(DMM, "CAL:ZERO:AUTO " & sAutoZero)
            Case "ON"
            Case Else
                Echo("DMM PROGRAMMING ERROR:  Command cmdDMM.dMeasImp sAutoZero argument out of range.")
                dMeasImp = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37)
                Err.Raise(-1000)
                Exit Function
        End Select

        'Make Measurement
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function

        If Not bSimulation Then
            Select Case Left(UCase(APROBE), 1)
                Case "O" ' Off
                    WriteMsg(DMM, "INIT")
                    If dDelay <> -1 Then
                        Delay(dDelay + 0.1)
                    End If

                    WriteMsg(DMM, "FETCH?")
                    nErr = ReadMsg(DMM, sData)
                    If nErr <> 0 Then
                        Echo("VISA ERROR: " & nErr)
                        dMeasImp = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37)
                        Err.Raise(-1000)
                        Exit Function
                    End If

                    dMeasImp = CDbl(sData)
                    gFrmMain.txtMeasured.Text = CStr(CSng(dMeasImp))
                    If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
                Case "S" ' Single
                    gFrmMain.lblStatus.Text = "Waiting for Probe ..."
                    MisProbe = MsgBoxResult.Yes
                    Do While MisProbe = MsgBoxResult.Yes
                        Failed = False
                        UserEvent = 0
                        gFrmMain.TimerProbe.Enabled = True : gFrmMain.fraInstructions.Visible = True
                        Do While (UserEvent = 0) And (Not bProbeClosed)
                            System.Windows.Forms.Application.DoEvents()
                        Loop

                        gFrmMain.TimerProbe.Enabled = False
                        bProbeClosed = False
                        gFrmMain.lblStatus.Text = "Making Measurement ..."
                        WriteMsg(DMM, "INIT")
                        If dDelay <> -1 Then
                            Delay(dDelay + 0.1)
                        End If

                        WriteMsg(DMM, "FETCH?")
                        nErr = ReadMsg(DMM, sData)
                        If nErr <> 0 Then
                            Echo("VISA ERROR: " & nErr)
                            dMeasImp = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37)
                            Err.Raise(-1000)
                            Exit Function
                        End If

                        dMeasImp = CDbl(sData)
                        gFrmMain.txtMeasured.Text = CStr(CSng(dMeasImp))
                        gFrmMain.lblStatus.Text = "Measurement Complete ..."
                        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function

                        If Failed = True Then
                            MisProbe = MsgBox("Analog Probe Measurement Failed. " & vbCrLf & "Do you wish to re-probe?",
                                              MsgBoxStyle.Critical + MsgBoxStyle.YesNo + MsgBoxStyle.SystemModal,
                                              "Analog Probe Failure")
                        Else
                            MisProbe = MsgBoxResult.No
                        End If
                    Loop
                    gFrmMain.lblStatus.Text = sCurrentMsg

                Case "C" ' Continuous
                    MisProbe = MsgBoxResult.Yes
                    Do While MisProbe = MsgBoxResult.Yes
                        UserEvent = 0
                        Failed = False
                        gFrmMain.TimerProbe.Enabled = True : gFrmMain.fraInstructions.Visible = True
                        Do While (UserEvent = 0) And (Not bProbeClosed)
                            gFrmMain.lblStatus.Text = "Making Measurement ..."
                            WriteMsg(DMM, "INIT")
                            If dDelay <> -1 Then
                                Delay(dDelay + 0.1)
                            End If

                            WriteMsg(DMM, "FETCH?")
                            nErr = ReadMsg(DMM, sData)
                            If nErr <> 0 Then
                                Echo("VISA ERROR: " & nErr)
                                dMeasImp = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37)
                                Err.Raise(-1000)
                                Exit Function
                            End If

                            dMeasImp = CDbl(sData)
                            gFrmMain.txtMeasured.Text = CStr(CSng(dMeasImp))
                            If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
                        Loop

                        gFrmMain.TimerProbe.Enabled = False
                        bProbeClosed = False
                        gFrmMain.lblStatus.Text = "Measurement Complete ..."
                        If Failed = True Then
                            MisProbe = MsgBox("Analog Probe Measurement Failed. " & vbCrLf & "Do you wish to re-probe?",
                                              MsgBoxStyle.Critical + MsgBoxStyle.YesNo + MsgBoxStyle.SystemModal,
                                              "Analog Probe Failure")
                        Else
                            MisProbe = MsgBoxResult.No
                        End If
                    Loop
            End Select
        Else
            dMeasImp = CDbl(InputBox("Command cmdDMM.dMeasimp peformed." & vbCrLf & "Enter Impedance Value:", "SIMULATION MODE"))
            gFrmMain.txtMeasured.Text = CStr(CSng(dMeasImp))
            If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON)
        End If
        gFrmMain.lblStatus.Text = sCurrentMsg
    End Function
End Class