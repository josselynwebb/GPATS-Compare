Option Strict Off
Option Explicit On
<System.Runtime.InteropServices.ProgId("cOSCOPE_NET.cOSCOPE")> Public Class cOSCOPE
	
    Public Sub ArmVPP(Optional ByRef nChannel As Integer = 1, Optional ByRef dVoltRange As Double = 4,
                      Optional ByRef dVoltOffSet As Double = 0, Optional ByRef dTimeRange As Double = 0.001,
                      Optional ByRef dTriggerLevel As Double = 0, Optional ByRef sTriggerSlope As String = "POS",
                      Optional ByRef sTriggerSource As String = "INP1", Optional ByRef nInputImp As Integer = 1000000,
                      Optional ByRef sInputCoupling As String = "DC", Optional ByRef sLPAS As String = "OFF",
                      Optional ByRef sHPAS As String = "OFF")

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        gFrmMain.txtInstrument.Text = "OSCOPE"
        gFrmMain.txtCommand.Text = "ArmVPP"

        'Error Check Arguments
        If nChannel < 1 Or nChannel > 2 Then
            Echo("OSCOPE PROGRAMMING ERROR:  Command cmdOSCOPE.ArmVPP nChannel argument out of range.")
            Echo("RANGE: 1 TO 2")
            Err.Raise(-1001)
            Exit Sub
        End If
        If dVoltRange < 0.008 Or dVoltRange > 40 Then
            Echo("OSCOPE PROGRAMMING ERROR:  Command cmdOSCOPE.ArmVPP dVoltRange argument out of range.")
            Echo("RANGE: .008 TO 40")
            Err.Raise(-1001)
            Exit Sub
        End If
        If dVoltOffSet <> 0 Then
            If dVoltRange > 10 Then
                If dVoltOffSet < -250 Or dVoltOffSet > 250 Then
                    Echo("OSCOPE PROGRAMMING ERROR:  Command cmdOSCOPE.ArmVPP dVoltOffSet argument out of range.")
                    Echo("RANGE: -250 TO 250")
                    Err.Raise(-1001)
                    Exit Sub
                End If
            ElseIf dVoltRange > 2 Then
                If dVoltOffSet < -50 Or dVoltOffSet > 50 Then
                    Echo("OSCOPE PROGRAMMING ERROR:  Command cmdOSCOPE.ArmVPP dVoltOffSet argument out of range.")
                    Echo("RANGE: -50 TO 50")
                    Err.Raise(-1001)
                    Exit Sub
                End If
            ElseIf dVoltRange > 0.4 Then
                If dVoltOffSet < -10 Or dVoltOffSet > 10 Then
                    Echo("OSCOPE PROGRAMMING ERROR:  Command cmdOSCOPE.ArmVPP dVoltOffSet argument out of range.")
                    Echo("RANGE: -10 TO 10")
                    Err.Raise(-1001)
                    Exit Sub
                End If
            ElseIf dVoltRange > 0.008 Then
                If dVoltOffSet < -2 Or dVoltOffSet > 2 Then
                    Echo("OSCOPE PROGRAMMING ERROR:  Command cmdOSCOPE.ArmVPP dVoltOffSet argument out of range.")
                    Echo("RANGE: -2 TO 2")
                    Err.Raise(-1001)
                    Exit Sub
                End If
            End If
        End If
        If dTimeRange < 0.00000005 Or dTimeRange > 50 Then
            Echo("OSCOPE PROGRAMMING ERROR:  Command cmdOSCOPE.ArmVPP dTimeRange argument out of range.")
            Echo("RANGE: 50E-9 TO 50")
            Err.Raise(-1001)
            Exit Sub
        End If
        sTriggerSlope = UCase(sTriggerSlope)
        If sTriggerSlope <> "POS" And sTriggerSlope <> "NEG" Then
            Echo("OSCOPE PROGRAMMING ERROR:  Command cmdOSCOPE.ArmVPP sTriggerSlope argument out of range.")
            Echo("RANGE: POS or NEG")
            Err.Raise(-1001)
            Exit Sub
        End If
        sTriggerSource = UCase(sTriggerSource)
        If sTriggerSource <> "INP1" And sTriggerSource <> "INP2" And sTriggerSource <> "EXT" Then
            Echo("OSCOPE PROGRAMMING ERROR:  Command cmdOSCOPE.ArmVPP sTriggerSource argument out of range.")
            Echo("RANGE: INP1 or INP2 or EXT")
            Err.Raise(-1001)
            Exit Sub
        End If
        If nInputImp <> 50 And nInputImp <> 1000000 Then
            Echo("OSCOPE PROGRAMMING ERROR:  Command cmdOSCOPE.ArmVPP sTriggerSource argument out of range.")
            Echo("RANGE: 50 or 1E6")
            Err.Raise(-1001)
            Exit Sub
        End If
        sInputCoupling = UCase(sInputCoupling)
        If sInputCoupling <> "DC" And sInputCoupling <> "AC" Then
            Echo("OSCOPE PROGRAMMING ERROR:  Command cmdOSCOPE.ArmVPP sInputCoupling argument out of range.")
            Echo("RANGE: DC or AC")
            Err.Raise(-1001)
            Exit Sub
        End If
        sLPAS = UCase(sLPAS)
        If sLPAS <> "OFF" And sLPAS <> "ON" Then
            Echo("OSCOPE PROGRAMMING ERROR:  Command cmdOSCOPE.ArmVPP sLPAS argument out of range.")
            Echo("RANGE: OFF or ON")
            Err.Raise(-1001)
            Exit Sub
        End If
        sHPAS = UCase(sHPAS)
        If sHPAS <> "OFF" And sHPAS <> "ON" Then
            Echo("OSCOPE PROGRAMMING ERROR:  Command cmdOSCOPE.ArmVPP sHPAS argument out of range.")
            Echo("RANGE: OFF or ON")
            Err.Raise(-1001)
            Exit Sub
        End If

        If Not bSimulation Then
            nSystErr = atxmlDF_viClear(ResourceName(OSCOPE), 0)
            WriteMsg(OSCOPE, "SYST:LANG COMP")
            Delay(1)
            WriteMsg(OSCOPE, "*CLS")
            WriteMsg(OSCOPE, "*RST")
            WriteMsg(OSCOPE, "TIM:RANG " & CStr(dTimeRange))
            WriteMsg(OSCOPE, "TIM:DEL 0")
            WriteMsg(OSCOPE, "TIM:mode TRIG")
            WriteMsg(OSCOPE, "CHAN" & CStr(nChannel) & ":RANG " & CStr(dVoltRange))
            WriteMsg(OSCOPE, "CHAN" & CStr(nChannel) & ":OFFS " & CStr(dVoltOffSet))
            If nInputImp = 50 Then
                WriteMsg(OSCOPE, "CHAN" & CStr(nChannel) & ":COUP DCF")
            Else
                WriteMsg(OSCOPE, "CHAN" & CStr(nChannel) & ":COUP " & sInputCoupling)
            End If
            WriteMsg(OSCOPE, "CHAN" & CStr(nChannel) & ":LFR " & sLPAS)
            WriteMsg(OSCOPE, "CHAN" & CStr(nChannel) & ":HFR " & sHPAS)
            WriteMsg(OSCOPE, "TRIG:LEV " & CStr(dTriggerLevel))
            WriteMsg(OSCOPE, "TRIG:SLOPE " & sTriggerSlope)
            WriteMsg(OSCOPE, "MEAS:SOUR CHAN" & CStr(nChannel))
            WriteMsg(OSCOPE, "MEAS:mode STAN")
            WriteMsg(OSCOPE, "RUN")
            WriteMsg(OSCOPE, "DIG CHAN1, CHAN2")
            Delay(0.5)
            WriteMsg(OSCOPE, "MEAS:SOUR CHAN" & CStr(nChannel))
            WriteMsg(OSCOPE, "MEAS:VPP?")
        End If
    End Sub

	Public Function dMeasFetch(Optional ByRef nChannel As Short = 1) As Double
        Dim dMeasFretch As Object
        Dim sData As String
        Dim ReadBuffer As String = Space(8100)
		Dim NBR As Integer
        Dim nErr As Integer
		Dim Q As String
		Q = Chr(34)

        dMeasFetch = 0.0

		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
		gFrmMain.txtInstrument.Text = "Oscope"
		gFrmMain.txtCommand.Text = "dMeasFetch"

		'Error Check Arguments
		If nChannel < 1 Or nChannel > 2 Then
			Echo("Oscope PROGRAMMING ERROR:  Command cmdOSCOPE.dMeasFreq nChannel argument out of range.")
			Echo("RANGE: 1 TO 2")
            dMeasFretch = 9.9E+37
			Err.Raise(-1001)
			Exit Function
		End If
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
		
		If Not bSimulation Then
            nErr = hpe1428a_readInstrData(nInstrumentHandle(OSCOPE), numberBytesToRead:=8100, ReadBuffer:=ReadBuffer, numBytesRead:=NBR)
			'nErr = ReadMsg(OSCOPE, sData)
			Delay(0.5)
            nSystErr = atxmlDF_viClear(ResourceName(OSCOPE), 0)
            WriteMsg(OSCOPE, "*CLS")
			If nErr <> 0 Then
				dMeasFetch = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37)
				Err.Raise(nErr)
				Exit Function
			End If
            sData = Left(ReadBuffer, NBR)
			dMeasFetch = CDbl(sData)
			gFrmMain.txtMeasured.Text = CStr(CSng(dMeasFetch))
			If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
		Else
			dMeasFetch = CDbl(InputBox("Command cmdOscope.dMeasFetch peformed." & vbCrLf & "Enter Value:", "SIMULATION MODE"))
			gFrmMain.txtMeasured.Text = CStr(CSng(dMeasFetch))
			If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON)
		End If
	End Function
	
	Public Sub SendMsg(ByRef sMsg As String)
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		gFrmMain.txtInstrument.Text = "OSCOPE"
		gFrmMain.txtCommand.Text = "SendMsg"
		WriteMsg(OSCOPE, sMsg)
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON)
	End Sub
	
    Public Function sReadMsg(ByRef sMsg As String) As String
        Dim ReadMessage As String = ""

        sReadMsg = ""

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        gFrmMain.txtInstrument.Text = "OSCOPE"
        gFrmMain.txtCommand.Text = "ReadMsg"
        ReadMsg(OSCOPE, ReadMessage)
        sReadMsg = ReadMessage
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON)
    End Function

    ''' <summary>
    ''' perform Frequency measurement
    ''' </summary>
    ''' <param name="sAutoScale">"ON" or "OFF"</param>
    ''' <param name="sINP">select INP1 or INP2 (OSCOPE input channel)</param>
    ''' <param name="dVoltRange">set the full scale vertical range;  min = 8mV, max = 40V, at reset = 4V</param>
    ''' <param name="dTimeRange">set the full scale horizontal range for sweep;  min = 10 ns, max = 50 s, at reset = 1 ms</param>
    ''' <param name="dTriggerLevel">set trigger level voltage (0V at reset)</param>
    ''' <param name="sTriggerSlope">set trigger slope ("POS" at reset)</param>
    ''' <param name="sTriggerSource"></param>
    ''' <param name="nInputImp">seleset trigger source ("INP1" at reset)ct input impedance 50 or 1M (1M at reset)</param>
    ''' <param name="sInputCoupling">select input coupling for input channels ("DC" at reset)</param>
    ''' <param name="sLPAS">select low-pass filter ("OFF" at reset)</param>
    ''' <param name="sHPAS">select high-pass filter ("OFF" at reset)</param>
    ''' <param name="sHYST">set noise rejection ("OFF" at reset)</param>
    ''' <param name="APROBE">Indicates AProbe usage; "OFF", "Single" Measurement, or "Contiuous" Measurement</param>
    ''' <returns>Measured frequency</returns>
    ''' <remarks>sAutoScale: 
    '''             auto scale can be used to determine vertical and horizontal parameters when sAutoScale = "ON", optional parameters are ignored.  
    ''' Autoscale should only be used with relatively stable input signals having a duty cycle of greater than 0.5% and a frequency greater than 50Hz.
    ''' sINP:
    '''        At reset, INP1 is on and INP2 if off.  Max input voltage that can be applied to the two input connectors is 5 Vrms at 50 ohms 
    ''' or +/-250V at 1Mohms.</remarks>
    Public Function dMeasFreq(ByRef sAutoScale As String, ByRef sINP As String, Optional ByRef dVoltRange As Double = 4,
                              Optional ByRef dTimeRange As Double = 0.001, Optional ByRef dTriggerLevel As Double = 0,
                              Optional ByRef sTriggerSlope As String = "POS", Optional ByRef sTriggerSource As String = "INP1",
                              Optional ByRef nInputImp As Integer = 1000000, Optional ByRef sInputCoupling As String = "DC",
                              Optional ByRef sLPAS As String = "OFF", Optional ByRef sHPAS As String = "OFF",
                              Optional ByRef sHYST As String = "OFF", Optional ByRef APROBE As String = "OFF") As Double
        Dim sData As String = ""
        Dim nErr As Integer
        Dim sTimeRange As String
        Dim sVoltRange As String
        Dim sTriggerLevel As String
        Dim sInputImp As String
        Dim sCurrentMsg As String

        dMeasFreq = 0.0

        sCurrentMsg = gFrmMain.lblStatus.Text
        If APROBE <> "OFF" Then
            gFrmMain.lblStatus.Text = "APPLY PROBE"
        End If

        'convert data type to string type
        sInputImp = CStr(nInputImp)
        sTimeRange = CStr(dTimeRange)
        sVoltRange = CStr(dVoltRange)
        sTriggerLevel = CStr(dTriggerLevel)

        'convert string data to upper case
        sAutoScale = UCase(sAutoScale)
        sINP = UCase(sINP)
        sInputCoupling = UCase(sInputCoupling)
        sLPAS = UCase(sLPAS)
        sHPAS = UCase(sHPAS)
        sHYST = UCase(sHYST)
        sTriggerSlope = UCase(sTriggerSlope)
        sTriggerSource = UCase(sTriggerSource)

        'display measurement info to TPS shell
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        gFrmMain.txtInstrument.Text = "OSCOPE"
        gFrmMain.txtCommand.Text = "dMeasFreq"

        'Setup OSCOPE for Frequency Measurement
        If Not bSimulation Then
            WriteMsg(OSCOPE, "SYST:LANG SCPI")

            LongDelay((1))
            WriteMsg(OSCOPE, "*CLS")
            WriteMsg(OSCOPE, "*RST")

            'select auto scale
            Select Case sAutoScale
                Case "ON", "AUTO"
                    WriteMsg(OSCOPE, "SYST:AUT")
                    LongDelay((2))

                Case "OFF" 'manually set vertical and horizontal ranges
                    'set input channel: INP1 or INP2
                    If (sINP = "INP1" Or sINP = "INP2") Then
                        WriteMsg(OSCOPE, sINP & " ON")
                    Else
                        Echo("OSCOPE INPUT CHANNEL SELECTION ERROR")
                        dMeasFreq = 0
                        Exit Function
                    End If

                    'set input impedance: 1000000 or 50
                    If (nInputImp = 50 Or nInputImp = 1000000) Then
                        WriteMsg(OSCOPE, sINP & ":IMP " & sInputImp)
                    Else
                        Echo("OSCOPE INPUT IMPEDANCE PARAMETER ERROR")
                        dMeasFreq = 0
                        Exit Function
                    End If

                    'set input coupling: DC or AC
                    If (sInputCoupling = "DC" Or sInputCoupling = "AC") Then
                        WriteMsg(OSCOPE, sINP & ":COUP " & sInputCoupling)
                    Else
                        Echo("OSCOPE INPUT COUPLING PARAMETER ERROR")
                        dMeasFreq = 0
                        Exit Function
                    End If

                    'set input low-pass filter state: OFF or ON
                    If (sLPAS = "ON" Or sLPAS = "OFF") Then
                        WriteMsg(OSCOPE, sINP & ":FILT:LPAS " & sLPAS)
                    Else
                        Echo("OSCOPE INPUT FILTER STATE PARAMETER ERROR")
                        dMeasFreq = 0
                        Exit Function
                    End If

                    'set input high-pass filter state: OFF or ON
                    If (sHPAS = "ON" Or sHPAS = "OFF") Then
                        WriteMsg(OSCOPE, sINP & ":FILT:hPAS " & sHPAS)
                    Else
                        Echo("OSCOPE INPUT FILTER STATE PARAMETER ERROR")
                        dMeasFreq = 0
                        Exit Function
                    End If

                    'set volt range: 8mV to 40V
                    If (dVoltRange < 0.008 Or dVoltRange > 40) Then
                        Echo("OSCOPE VOLT RANGE ERROR")
                        dMeasFreq = 0
                        Exit Function
                    Else
                        Select Case sINP
                            Case "INP1"
                                WriteMsg(OSCOPE, "VOLT1:RANG " & sVoltRange)
                            Case "INP2"
                                WriteMsg(OSCOPE, "VOLT2:RANG " & sVoltRange)
                        End Select
                    End If

                    'set time range:10ns to 50s
                    If (dTimeRange < 0.00000001 Or dTimeRange > 50) Then
                        Echo("OSCOPE TIME RANGE ERROR")
                        dMeasFreq = 0
                        Exit Function
                    Else
                        WriteMsg(OSCOPE, "SWE:TIME:RANG " & sTimeRange)
                    End If

                    'set trigger level
                    WriteMsg(OSCOPE, "TRIG:LEV " & sTriggerLevel)

                    'set niose rejection: OFF or ON
                    If (sHYST = "ON" Or sHYST = "OFF") Then
                        WriteMsg(OSCOPE, "TRIG:HYST " & sHYST)
                    Else
                        Echo("OSCOPE TRIGGER INPUT PARAMETER ERROR")
                        dMeasFreq = 0
                        Exit Function
                    End If

                    'set trigger slope: POS or NEG
                    If (sTriggerSlope = "POS" Or sTriggerSlope = "NEG") Then
                        WriteMsg(OSCOPE, "TRIG:SLOP " & sTriggerSlope)
                    Else
                        Echo("OSCOPE TRIGGER SLOPE PARAMETER ERROR")
                        dMeasFreq = 0
                        Exit Function
                    End If

                    'set trigger source
                    Select Case sTriggerSource
                        Case "INP1", "INP2", "EXT", "ECLT0", "ECLT1"
                            WriteMsg(OSCOPE, "TRIG:SOUR " & sTriggerSource)

                        Case Else
                            Echo("OSCOPE TRIGGER SOURCE PARAMETER ERROR")
                            dMeasFreq = 0
                            Exit Function
                    End Select

                Case Else
                    Echo("OSCOPE AUTO SCALE PARAMETER ERROR")
                    dMeasFreq = 0
                    Exit Function
            End Select
        End If

        'Make Mesurement
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        If Not bSimulation Then
            Select Case Left(UCase(APROBE), 1)
                Case "O" ' Off
                    WriteMsg(OSCOPE, "MEAS:VOLT:FREQ? (@" & sINP & ")")
                    LongDelay((1))
                    nErr = ReadMsg(OSCOPE, sData)
                    WriteMsg(OSCOPE, "*CLS")
                    If nErr <> 0 Then
                        Echo("VISA ERROR: " & nErr)
                        dMeasFreq = 9.9E+37 : Err.Raise(nErr)
                        Exit Function
                    End If
                    dMeasFreq = CDbl(sData)
                    gFrmMain.txtMeasured.Text = CStr(dMeasFreq)
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
                        '5/14/03 by Soon Nam''''''''''''''''''''''''''''''
                        'set auto scale with analog probe measurements
                        If (sAutoScale = "ON" Or sAutoScale = "AUTO") Then
                            WriteMsg(OSCOPE, "SYST:AUT")
                            LongDelay((1))
                        End If
                        'end add''''''''''''''''''''''''''''''''''''''''''
                        gFrmMain.lblStatus.Text = "Making Measurement ..."
                        WriteMsg(OSCOPE, "MEAS:VOLT:FREQ? (@" & sINP & ")")
                        LongDelay((1))
                        nErr = ReadMsg(OSCOPE, sData)
                        WriteMsg(OSCOPE, "*CLS")
                        If nErr <> 0 Then
                            Echo("VISA ERROR: " & nErr)
                            dMeasFreq = 9.9E+37 : Err.Raise(nErr)
                            Exit Function
                        End If
                        dMeasFreq = CDbl(sData)
                        gFrmMain.txtMeasured.Text = CStr(dMeasFreq)
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

                Case "C" ' Continuous
                    MisProbe = MsgBoxResult.Yes
                    Do While MisProbe = MsgBoxResult.Yes
                        UserEvent = 0
                        Failed = False
                        gFrmMain.TimerProbe.Enabled = True : gFrmMain.fraInstructions.Visible = True
                        Do While (UserEvent = 0) And (Not bProbeClosed)
                            '5/14/03 by Soon Nam''''''''''''''''''''''''''''''
                            'set auto scale with analog probe measurements
                            If (sAutoScale = "ON" Or sAutoScale = "AUTO") Then
                                WriteMsg(OSCOPE, "SYST:AUT")
                                LongDelay((1))
                            End If
                            'end add''''''''''''''''''''''''''''''''''''''''''
                            gFrmMain.lblStatus.Text = "Making Measurement ..."
                            WriteMsg(OSCOPE, "MEAS:VOLT:FREQ? (@" & sINP & ")")
                            LongDelay((1))
                            nErr = ReadMsg(OSCOPE, sData)
                            WriteMsg(OSCOPE, "*CLS")
                            If nErr <> 0 Then
                                Echo("VISA ERROR: " & nErr)
                                dMeasFreq = 9.9E+37 : Err.Raise(nErr)
                                Exit Function
                            End If
                            dMeasFreq = CDbl(sData)
                            gFrmMain.txtMeasured.Text = CStr(dMeasFreq)
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
            dMeasFreq = CDbl(InputBox("Command cmdOSCOPE.dMeasFreq peformed." & vbCrLf & "Enter Frequency Value:", "SIMULATION MODE"))
            gFrmMain.txtMeasured.Text = CStr(dMeasFreq)
            If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON)
        End If
        gFrmMain.lblStatus.Text = sCurrentMsg
    End Function

    ''' <summary>
    ''' perform High voltage measurement
    ''' </summary>
    ''' <param name="sAutoScale">"ON" or "OFF"</param>
    ''' <param name="sINP">select INP1 or INP2 (OSCOPE input channel)</param>
    ''' <param name="dVoltRange">set the full scale vertical range;  min = 8mV, max = 40V, at reset = 4V</param>
    ''' <param name="dTimeRange">set the full scale horizontal range for sweep;  min = 10 ns, max = 50 s, at reset = 1 ms</param>
    ''' <param name="dTriggerLevel">set trigger level voltage (0V at reset)</param>
    ''' <param name="sTriggerSlope">set trigger slope ("POS" at reset)</param>
    ''' <param name="sTriggerSource">set trigger source ("INP1" at reset)</param>
    ''' <param name="nInputImp">select input impedance 50 or 1M (1M at reset)</param>
    ''' <param name="sInputCoupling">select input coupling for input channels ("DC" at reset)</param>
    ''' <param name="sLPAS">select low-pass filter ("OFF" at reset)</param>
    ''' <param name="sHPAS">select high-pass filter ("OFF" at reset)</param>
    ''' <param name="sHYST">set noise rejection ("OFF" at reset)</param>
    ''' <param name="APROBE">Indicates AProbe usage; "OFF", "Single" Measurement, or "Contiuous" Measurement</param>
    ''' <returns>Measured voltage</returns>
    ''' <remarks>sAutoScale: 
    '''             auto scale can be used to determine vertical and horizontal parameters when sAutoScale = "ON", optional parameters are ignored.  
    ''' Autoscale should only be used with relatively stable input signals having a duty cycle of greater than 0.5% and a frequency greater than 50Hz.
    ''' sINP:
    '''        At reset, INP1 is on and INP2 if off.  Max input voltage that can be applied to the two input connectors is 5 Vrms at 50 ohms 
    ''' or +/-250V at 1Mohms.</remarks>
    Public Function dMeasVoltHigh(ByRef sAutoScale As String, ByRef sINP As String, Optional ByRef dVoltRange As Double = 4,
                                  Optional ByRef dTimeRange As Double = 0.001, Optional ByRef dTriggerLevel As Double = 0,
                                  Optional ByRef sTriggerSlope As String = "POS", Optional ByRef sTriggerSource As String = "INP1",
                                  Optional ByRef nInputImp As Integer = 1000000, Optional ByRef sInputCoupling As String = "DC",
                                  Optional ByRef sLPAS As String = "OFF", Optional ByRef sHPAS As String = "OFF",
                                  Optional ByRef sHYST As String = "OFF", Optional ByRef APROBE As String = "OFF") As Double
        Dim sData As String = ""
        Dim nErr As Integer
        Dim sTimeRange As String
        Dim sVoltRange As String
        Dim sTriggerLevel As String
        Dim sInputImp As String
        Dim sCurrentMsg As String

        dMeasVoltHigh = 0.0

        sCurrentMsg = gFrmMain.lblStatus.Text
        If APROBE <> "OFF" Then
            gFrmMain.lblStatus.Text = "APPLY PROBE"
        End If

        'convert data type to string type
        sInputImp = CStr(nInputImp)
        sTimeRange = CStr(dTimeRange)
        sVoltRange = CStr(dVoltRange)
        sTriggerLevel = CStr(dTriggerLevel)

        'convert string data to upper case
        sAutoScale = UCase(sAutoScale)
        sINP = UCase(sINP)
        sInputCoupling = UCase(sInputCoupling)
        sLPAS = UCase(sLPAS)
        sHPAS = UCase(sHPAS)
        sHYST = UCase(sHYST)
        sTriggerSlope = UCase(sTriggerSlope)
        sTriggerSource = UCase(sTriggerSource)

        'display measurement info to TPS shell
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        gFrmMain.txtInstrument.Text = "OSCOPE"
        gFrmMain.txtCommand.Text = "dMeasVoltHigh"

        'Setup OSCOPE for Frequency Measurement
        If Not bSimulation Then
            WriteMsg(OSCOPE, "SYST:LANG SCPI")

            LongDelay((1))
            WriteMsg(OSCOPE, "*CLS")
            WriteMsg(OSCOPE, "*RST")

            'select auto scale
            Select Case sAutoScale
                Case "ON", "AUTO"
                    WriteMsg(OSCOPE, "SYST:AUT")
                    LongDelay((2))

                Case "OFF" 'manually set vertical and horizontal ranges
                    'set input channel: INP1 or INP2
                    If (sINP = "INP1" Or sINP = "INP2") Then
                        WriteMsg(OSCOPE, sINP & " ON")
                    Else
                        Echo("OSCOPE INPUT CHANNEL SELECTION ERROR")
                        dMeasVoltHigh = 0
                        Exit Function
                    End If

                    'set input coupling: DC or AC
                    If (sInputCoupling = "DC" Or sInputCoupling = "AC") Then
                        WriteMsg(OSCOPE, sINP & ":COUP " & sInputCoupling)
                    Else
                        Echo("OSCOPE INPUT COUPLING PARAMETER ERROR")
                        dMeasVoltHigh = 0
                        Exit Function
                    End If

                    'set input impedance: 1000000 or 50
                    If (nInputImp = 50 Or nInputImp = 1000000) Then
                        WriteMsg(OSCOPE, sINP & ":IMP " & sInputImp)
                    Else
                        Echo("OSCOPE INPUT IMPEDANCE PARAMETER ERROR")
                        dMeasVoltHigh = 0
                        Exit Function
                    End If

                    'set input low-pass filter state: OFF or ON
                    If (sLPAS = "ON" Or sLPAS = "OFF") Then
                        WriteMsg(OSCOPE, sINP & ":FILT:LPAS " & sLPAS)
                    Else
                        Echo("OSCOPE INPUT FILTER STATE PARAMETER ERROR")
                        dMeasVoltHigh = 0
                        Exit Function
                    End If

                    'set input high-pass filter state: OFF or ON
                    If (sHPAS = "ON" Or sHPAS = "OFF") Then
                        WriteMsg(OSCOPE, sINP & ":FILT:hPAS " & sHPAS)
                    Else
                        Echo("OSCOPE INPUT FILTER STATE PARAMETER ERROR")
                        dMeasVoltHigh = 0
                        Exit Function
                    End If

                    'set volt range: 8mV to 40V
                    If (dVoltRange < 0.008 Or dVoltRange > 40) Then
                        Echo("OSCOPE VOLT RANGE ERROR")
                        dMeasVoltHigh = 0
                        Exit Function
                    Else
                        Select Case sINP
                            Case "INP1"
                                WriteMsg(OSCOPE, "VOLT1:RANG " & sVoltRange)
                            Case "INP2"
                                WriteMsg(OSCOPE, "VOLT2:RANG " & sVoltRange)
                        End Select
                    End If

                    'set time range:10ns to 50s
                    If (dTimeRange < 0.00000001 Or dTimeRange > 50) Then
                        Echo("OSCOPE TIME RANGE ERROR")
                        dMeasVoltHigh = 0
                        Exit Function
                    Else
                        WriteMsg(OSCOPE, "SWE:TIME:RANG " & sTimeRange)
                    End If

                    'set trigger level
                    WriteMsg(OSCOPE, "TRIG:LEV " & sTriggerLevel)

                    'set niose rejection: OFF or ON
                    If (sHYST = "ON" Or sHYST = "OFF") Then
                        WriteMsg(OSCOPE, "TRIG:HYST " & sHYST)
                    Else
                        Echo("OSCOPE TRIGGER INPUT PARAMETER ERROR")
                        dMeasVoltHigh = 0
                        Exit Function
                    End If

                    'set trigger slope: POS or NEG
                    If (sTriggerSlope = "POS" Or sTriggerSlope = "NEG") Then
                        WriteMsg(OSCOPE, "TRIG:SLOP " & sTriggerSlope)
                    Else
                        Echo("OSCOPE TRIGGER SLOPE PARAMETER ERROR")
                        dMeasVoltHigh = 0
                        Exit Function
                    End If

                    'set trigger source
                    Select Case sTriggerSource
                        Case "INP1", "INP2", "EXT", "ECLT0", "ECLT1"
                            WriteMsg(OSCOPE, "TRIG:SOUR " & sTriggerSource)

                        Case Else
                            Echo("OSCOPE TRIGGER SOURCE PARAMETER ERROR")
                            dMeasVoltHigh = 0
                            Exit Function
                    End Select

                Case Else
                    Echo("OSCOPE AUTO SCALE PARAMETER ERROR")
                    dMeasVoltHigh = 0
                    Exit Function
            End Select
        End If

        'Make Mesurement
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        If Not bSimulation Then
            Select Case Left(UCase(APROBE), 1)
                Case "O" ' Off
                    WriteMsg(OSCOPE, "MEAS:VOLT:HIGH? (@" & sINP & ")")
                    LongDelay((1))
                    nErr = ReadMsg(OSCOPE, sData)
                    WriteMsg(OSCOPE, "*CLS")
                    If nErr <> 0 Then
                        Echo("VISA ERROR: " & nErr)
                        dMeasVoltHigh = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(nErr)
                        dMeasVoltHigh = 0
                        Exit Function
                    End If
                    dMeasVoltHigh = CDbl(sData)
                    gFrmMain.txtMeasured.Text = CStr(dMeasVoltHigh)
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
                        '5/14/03 by Soon Nam''''''''''''''''''''''''''''''
                        'set auto scale with analog probe measurements
                        If (sAutoScale = "ON" Or sAutoScale = "AUTO") Then
                            WriteMsg(OSCOPE, "SYST:AUT")
                            LongDelay((1))
                        End If
                        'end add''''''''''''''''''''''''''''''''''''''''''
                        gFrmMain.lblStatus.Text = "Making Measurement ..."
                        WriteMsg(OSCOPE, "MEAS:VOLT:HIGH? (@" & sINP & ")")
                        LongDelay((1))
                        nErr = ReadMsg(OSCOPE, sData)
                        WriteMsg(OSCOPE, "*CLS")
                        If nErr <> 0 Then
                            Echo("VISA ERROR: " & nErr)
                            dMeasVoltHigh = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(nErr)
                            dMeasVoltHigh = 0
                            Exit Function
                        End If
                        dMeasVoltHigh = CDbl(sData)
                        gFrmMain.txtMeasured.Text = CStr(dMeasVoltHigh)
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

                Case "C" ' Continuous
                    MisProbe = MsgBoxResult.Yes
                    Do While MisProbe = MsgBoxResult.Yes
                        UserEvent = 0
                        Failed = False
                        gFrmMain.TimerProbe.Enabled = True : gFrmMain.fraInstructions.Visible = True
                        Do While (UserEvent = 0) And (Not bProbeClosed)
                            '5/14/03 by Soon Nam''''''''''''''''''''''''''''''
                            'set auto scale with analog probe measurements
                            If (sAutoScale = "ON" Or sAutoScale = "AUTO") Then
                                WriteMsg(OSCOPE, "SYST:AUT")
                                LongDelay((1))
                            End If
                            'end add''''''''''''''''''''''''''''''''''''''''''
                            gFrmMain.lblStatus.Text = "Making Measurement ..."
                            WriteMsg(OSCOPE, "MEAS:VOLT:HIGH? (@" & sINP & ")")
                            LongDelay((1))
                            nErr = ReadMsg(OSCOPE, sData)
                            WriteMsg(OSCOPE, "*CLS")
                            If nErr <> 0 Then
                                Echo("VISA ERROR: " & nErr)
                                dMeasVoltHigh = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(nErr)
                                dMeasVoltHigh = 0
                                Exit Function
                            End If
                            dMeasVoltHigh = CDbl(sData)
                            gFrmMain.txtMeasured.Text = CStr(dMeasVoltHigh)
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
            dMeasVoltHigh = CDbl(InputBox("Command cmdOSCOPE.dMeasVoltHigh peformed." & vbCrLf & "Enter Voltage Value:", "SIMULATION MODE"))
            gFrmMain.txtMeasured.Text = CStr(dMeasVoltHigh)
            If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON)
        End If
        gFrmMain.lblStatus.Text = sCurrentMsg
    End Function
	
    ''' <summary>
    ''' perform Low voltage measurement
    ''' </summary>
    ''' <param name="sAutoScale">"ON" or "OFF"</param>
    ''' <param name="sINP">select INP1 or INP2 (OSCOPE input channel)</param>
    ''' <param name="dVoltRange">set the full scale vertical range;  min = 8mV, max = 40V, at reset = 4V</param>
    ''' <param name="dTimeRange">set the full scale horizontal range for sweep;  min = 10 ns, max = 50 s, at reset = 1 ms</param>
    ''' <param name="dTriggerLevel">set trigger level voltage (0V at reset)</param>
    ''' <param name="sTriggerSlope">set trigger slope ("POS" at reset)</param>
    ''' <param name="sTriggerSource">set trigger source ("INP1" at reset)</param>
    ''' <param name="nInputImp">select input impedance 50 or 1M (1M at reset)</param>
    ''' <param name="sInputCoupling">select input coupling for input channels ("DC" at reset)</param>
    ''' <param name="sLPAS">select low-pass filter ("OFF" at reset)</param>
    ''' <param name="sHPAS">select high-pass filter ("OFF" at reset)</param>
    ''' <param name="sHYST">set noise rejection ("OFF" at reset)</param>
    ''' <param name="APROBE">Indicates AProbe usage; "OFF", "Single" Measurement, or "Contiuous" Measurement</param>
    ''' <returns>Measured voltage</returns>
    ''' <remarks>sAutoScale: 
    '''             auto scale can be used to determine vertical and horizontal parameters when sAutoScale = "ON", optional parameters are ignored.  
    ''' Autoscale should only be used with relatively stable input signals having a duty cycle of greater than 0.5% and a frequency greater than 50Hz.
    ''' sINP:
    '''        At reset, INP1 is on and INP2 if off.  Max input voltage that can be applied to the two input connectors is 5 Vrms at 50 ohms 
    ''' or +/-250V at 1Mohms.</remarks>
    Public Function dMeasVoltLow(ByRef sAutoScale As String, ByRef sINP As String, Optional ByRef dVoltRange As Double = 4,
                                 Optional ByRef dTimeRange As Double = 0.001, Optional ByRef dTriggerLevel As Double = 0,
                                 Optional ByRef sTriggerSlope As String = "POS", Optional ByRef sTriggerSource As String = "INP1",
                                 Optional ByRef nInputImp As Integer = 1000000, Optional ByRef sInputCoupling As String = "DC",
                                 Optional ByRef sLPAS As String = "OFF", Optional ByRef sHPAS As String = "OFF",
                                 Optional ByRef sHYST As String = "OFF", Optional ByRef APROBE As String = "OFF") As Double
        Dim sData As String = ""
        Dim nErr As Integer
        Dim sTimeRange As String
        Dim sVoltRange As String
        Dim sTriggerLevel As String
        Dim sInputImp As String
        Dim sCurrentMsg As String

        dMeasVoltLow = 0.0

        sCurrentMsg = gFrmMain.lblStatus.Text
        If APROBE <> "OFF" Then
            gFrmMain.lblStatus.Text = "APPLY PROBE"
        End If

        'convert data type to string type
        sInputImp = CStr(nInputImp)
        sTimeRange = CStr(dTimeRange)
        sVoltRange = CStr(dVoltRange)
        sTriggerLevel = CStr(dTriggerLevel)

        'convert string data to upper case
        sAutoScale = UCase(sAutoScale)
        sINP = UCase(sINP)
        sInputCoupling = UCase(sInputCoupling)
        sLPAS = UCase(sLPAS)
        sHPAS = UCase(sHPAS)
        sHYST = UCase(sHYST)
        sTriggerSlope = UCase(sTriggerSlope)
        sTriggerSource = UCase(sTriggerSource)

        'display measurement info to TPS shell
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        gFrmMain.txtInstrument.Text = "OSCOPE"
        gFrmMain.txtCommand.Text = "dMeasVoltLow"

        'Setup OSCOPE for Frequency Measurement
        If Not bSimulation Then
            WriteMsg(OSCOPE, "SYST:LANG SCPI")

            LongDelay((1))
            WriteMsg(OSCOPE, "*CLS")
            WriteMsg(OSCOPE, "*RST")

            'select auto scale
            Select Case sAutoScale
                Case "ON", "AUTO"
                    WriteMsg(OSCOPE, "SYST:AUT")
                    LongDelay((2))

                Case "OFF" 'manually set vertical and horizontal ranges
                    'set input channel: INP1 or INP2
                    If (sINP = "INP1" Or sINP = "INP2") Then
                        WriteMsg(OSCOPE, sINP & " ON")
                    Else
                        Echo("OSCOPE INPUT CHANNEL SELECTION ERROR")
                        dMeasVoltLow = 0
                        Exit Function
                    End If

                    'set input coupling: DC or AC
                    If (sInputCoupling = "DC" Or sInputCoupling = "AC") Then
                        WriteMsg(OSCOPE, sINP & ":COUP " & sInputCoupling)
                    Else
                        Echo("OSCOPE INPUT COUPLING PARAMETER ERROR")
                        dMeasVoltLow = 0
                        Exit Function
                    End If

                    'set input impedance: 1000000 or 50
                    If (nInputImp = 50 Or nInputImp = 1000000) Then
                        WriteMsg(OSCOPE, sINP & ":IMP " & sInputImp)
                    Else
                        Echo("OSCOPE INPUT IMPEDANCE PARAMETER ERROR")
                        dMeasVoltLow = 0
                        Exit Function
                    End If

                    'set input low-pass filter state: OFF or ON
                    If (sLPAS = "ON" Or sLPAS = "OFF") Then
                        WriteMsg(OSCOPE, sINP & ":FILT:LPAS " & sLPAS)
                    Else
                        Echo("OSCOPE INPUT FILTER STATE PARAMETER ERROR")
                        dMeasVoltLow = 0
                        Exit Function
                    End If

                    'set input high-pass filter state: OFF or ON
                    If (sHPAS = "ON" Or sHPAS = "OFF") Then
                        WriteMsg(OSCOPE, sINP & ":FILT:hPAS " & sHPAS)
                    Else
                        Echo("OSCOPE INPUT FILTER STATE PARAMETER ERROR")
                        dMeasVoltLow = 0
                        Exit Function
                    End If

                    'set volt range: 8mV to 40V
                    If (dVoltRange < 0.008 Or dVoltRange > 40) Then
                        Echo("OSCOPE VOLT RANGE ERROR")
                        dMeasVoltLow = 0
                        Exit Function
                    Else
                        Select Case sINP
                            Case "INP1"
                                WriteMsg(OSCOPE, "VOLT1:RANG " & sVoltRange)
                            Case "INP2"
                                WriteMsg(OSCOPE, "VOLT2:RANG " & sVoltRange)
                        End Select
                    End If

                    'set time range:10ns to 50s
                    If (dTimeRange < 0.00000001 Or dTimeRange > 50) Then
                        Echo("OSCOPE TIME RANGE ERROR")
                        dMeasVoltLow = 0
                        Exit Function
                    Else
                        WriteMsg(OSCOPE, "SWE:TIME:RANG " & sTimeRange)
                    End If

                    'set trigger level
                    WriteMsg(OSCOPE, "TRIG:LEV " & sTriggerLevel)

                    'set niose rejection: OFF or ON
                    If (sHYST = "ON" Or sHYST = "OFF") Then
                        WriteMsg(OSCOPE, "TRIG:HYST " & sHYST)
                    Else
                        Echo("OSCOPE TRIGGER INPUT PARAMETER ERROR")
                        dMeasVoltLow = 0
                        Exit Function
                    End If

                    'set trigger slope: POS or NEG
                    If (sTriggerSlope = "POS" Or sTriggerSlope = "NEG") Then
                        WriteMsg(OSCOPE, "TRIG:SLOP " & sTriggerSlope)
                    Else
                        Echo("OSCOPE TRIGGER SLOPE PARAMETER ERROR")
                        dMeasVoltLow = 0
                        Exit Function
                    End If

                    'set trigger source
                    Select Case sTriggerSource
                        Case "INP1", "INP2", "EXT", "ECLT0", "ECLT1"
                            WriteMsg(OSCOPE, "TRIG:SOUR " & sTriggerSource)

                        Case Else
                            Echo("OSCOPE TRIGGER SOURCE PARAMETER ERROR")
                            dMeasVoltLow = 0
                            Exit Function
                    End Select

                Case Else
                    Echo("OSCOPE AUTO SCALE PARAMETER ERROR")
                    dMeasVoltLow = 0
                    Exit Function
            End Select
        End If

        'Make Mesurement
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        If Not bSimulation Then
            Select Case Left(UCase(APROBE), 1)
                Case "O" ' Off
                    WriteMsg(OSCOPE, "MEAS:VOLT:LOW? (@" & sINP & ")")
                    LongDelay((1))
                    nErr = ReadMsg(OSCOPE, sData)
                    WriteMsg(OSCOPE, "*CLS")
                    If nErr <> 0 Then
                        Echo("VISA ERROR: " & nErr)
                        dMeasVoltLow = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(nErr)
                        dMeasVoltLow = 0
                        Exit Function
                    End If
                    dMeasVoltLow = CDbl(sData)
                    gFrmMain.txtMeasured.Text = CStr(dMeasVoltLow)
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
                        '5/14/03 by Soon Nam''''''''''''''''''''''''''''''
                        'set auto scale with analog probe measurements
                        If (sAutoScale = "ON" Or sAutoScale = "AUTO") Then
                            WriteMsg(OSCOPE, "SYST:AUT")
                            LongDelay((1))
                        End If
                        'end add''''''''''''''''''''''''''''''''''''''''''
                        gFrmMain.lblStatus.Text = "Making Measurement ..."
                        WriteMsg(OSCOPE, "MEAS:VOLT:LOW? (@" & sINP & ")")
                        LongDelay((1))
                        nErr = ReadMsg(OSCOPE, sData)
                        WriteMsg(OSCOPE, "*CLS")
                        If nErr <> 0 Then
                            Echo("VISA ERROR: " & nErr)
                            dMeasVoltLow = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(nErr)
                            dMeasVoltLow = 0
                            Exit Function
                        End If
                        dMeasVoltLow = CDbl(sData)
                        gFrmMain.txtMeasured.Text = CStr(dMeasVoltLow)
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

                Case "C" ' Continuous
                    MisProbe = MsgBoxResult.Yes
                    Do While MisProbe = MsgBoxResult.Yes
                        UserEvent = 0
                        Failed = False
                        gFrmMain.TimerProbe.Enabled = True : gFrmMain.fraInstructions.Visible = True
                        Do While (UserEvent = 0) And (Not bProbeClosed)
                            '5/14/03 by Soon Nam''''''''''''''''''''''''''''''
                            'set auto scale with analog probe measurements
                            If (sAutoScale = "ON" Or sAutoScale = "AUTO") Then
                                WriteMsg(OSCOPE, "SYST:AUT")
                                LongDelay((1))
                            End If
                            'end add''''''''''''''''''''''''''''''''''''''''''
                            gFrmMain.lblStatus.Text = "Making Measurement ..."
                            WriteMsg(OSCOPE, "MEAS:VOLT:LOW? (@" & sINP & ")")
                            LongDelay((1))
                            nErr = ReadMsg(OSCOPE, sData)
                            WriteMsg(OSCOPE, "*CLS")
                            If nErr <> 0 Then
                                Echo("VISA ERROR: " & nErr)
                                dMeasVoltLow = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(nErr)
                                dMeasVoltLow = 0
                                Exit Function
                            End If
                            dMeasVoltLow = CDbl(sData)
                            gFrmMain.txtMeasured.Text = CStr(dMeasVoltLow)
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
            dMeasVoltLow = CDbl(InputBox("Command cmdOSCOPE.dMeasVoltLow peformed." & vbCrLf & "Enter Voltage Value:", "SIMULATION MODE"))
            gFrmMain.txtMeasured.Text = CStr(dMeasVoltLow)
            If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON)
        End If

        gFrmMain.lblStatus.Text = sCurrentMsg
    End Function

    ''' <summary>
    ''' perform maximum voltage measurement
    ''' </summary>
    ''' <param name="sAutoScale">"ON" or "OFF"</param>
    ''' <param name="sINP">select INP1 or INP2 (OSCOPE input channel)</param>
    ''' <param name="dVoltRange">set the full scale vertical range;  min = 8mV, max = 40V, at reset = 4V</param>
    ''' <param name="dTimeRange">set the full scale horizontal range for sweep;  min = 10 ns, max = 50 s, at reset = 1 ms</param>
    ''' <param name="dTriggerLevel">set trigger level voltage (0V at reset)</param>
    ''' <param name="sTriggerSlope">set trigger slope ("POS" at reset)</param>
    ''' <param name="sTriggerSource">set trigger source ("INP1" at reset)</param>
    ''' <param name="nInputImp">select input impedance 50 or 1M (1M at reset)</param>
    ''' <param name="sInputCoupling">select input coupling for input channels ("DC" at reset)</param>
    ''' <param name="sLPAS">select low-pass filter ("OFF" at reset)</param>
    ''' <param name="sHPAS">select high-pass filter ("OFF" at reset)</param>
    ''' <param name="sHYST">set noise rejection ("OFF" at reset)</param>
    ''' <param name="APROBE">Indicates AProbe usage; "OFF", "Single" Measurement, or "Contiuous" Measurement</param>
    ''' <returns>Measured voltage</returns>
    ''' <remarks>sAutoScale: 
    '''             auto scale can be used to determine vertical and horizontal parameters when sAutoScale = "ON", optional parameters are ignored.  
    ''' Autoscale should only be used with relatively stable input signals having a duty cycle of greater than 0.5% and a frequency greater than 50Hz.
    ''' sINP:
    '''        At reset, INP1 is on and INP2 if off.  Max input voltage that can be applied to the two input connectors is 5 Vrms at 50 ohms 
    ''' or +/-250V at 1Mohms.</remarks>
    Public Function dMeasVoltMax(ByRef sAutoScale As String, ByRef sINP As String, Optional ByRef dVoltRange As Double = 4,
                                 Optional ByRef dTimeRange As Double = 0.001, Optional ByRef dTriggerLevel As Double = 0,
                                 Optional ByRef sTriggerSlope As String = "POS", Optional ByRef sTriggerSource As String = "INP1",
                                 Optional ByRef nInputImp As Integer = 1000000, Optional ByRef sInputCoupling As String = "DC",
                                 Optional ByRef sLPAS As String = "OFF", Optional ByRef sHPAS As String = "OFF",
                                 Optional ByRef sHYST As String = "OFF", Optional ByRef APROBE As String = "OFF") As Double
        Dim sData As String = ""
        Dim nErr As Integer
        Dim sTimeRange As String
        Dim sVoltRange As String
        Dim sTriggerLevel As String
        Dim sInputImp As String
        Dim sCurrentMsg As String

        dMeasVoltMax = 0.0

        sCurrentMsg = gFrmMain.lblStatus.Text
        If APROBE <> "OFF" Then
            gFrmMain.lblStatus.Text = "APPLY PROBE"
        End If

        'convert data type to string type
        sInputImp = CStr(nInputImp)
        sTimeRange = CStr(dTimeRange)
        sVoltRange = CStr(dVoltRange)
        sTriggerLevel = CStr(dTriggerLevel)

        'convert string data to upper case
        sAutoScale = UCase(sAutoScale)
        sINP = UCase(sINP)
        sInputCoupling = UCase(sInputCoupling)
        sLPAS = UCase(sLPAS)
        sHPAS = UCase(sHPAS)
        sHYST = UCase(sHYST)
        sTriggerSlope = UCase(sTriggerSlope)
        sTriggerSource = UCase(sTriggerSource)

        'display measurement info to TPS shell
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        gFrmMain.txtInstrument.Text = "OSCOPE"
        gFrmMain.txtCommand.Text = "dMeasVoltMax"

        'Setup OSCOPE for Frequency Measurement
        If Not bSimulation Then
            WriteMsg(OSCOPE, "SYST:LANG SCPI")

            LongDelay((1))
            WriteMsg(OSCOPE, "*CLS")
            WriteMsg(OSCOPE, "*RST")

            'select auto scale
            Select Case sAutoScale
                Case "ON", "AUTO"
                    WriteMsg(OSCOPE, "SYST:AUT")
                    LongDelay((2))

                Case "OFF" 'manually set vertical and horizontal ranges
                    'set input channel: INP1 or INP2
                    If (sINP = "INP1" Or sINP = "INP2") Then
                        WriteMsg(OSCOPE, sINP & " ON")
                    Else
                        Echo("OSCOPE INPUT CHANNEL SELECTION ERROR")
                        dMeasVoltMax = 0
                        Exit Function
                    End If

                    'set input coupling: DC or AC
                    If (sInputCoupling = "DC" Or sInputCoupling = "AC") Then
                        WriteMsg(OSCOPE, sINP & ":COUP " & sInputCoupling)
                    Else
                        Echo("OSCOPE INPUT COUPLING PARAMETER ERROR")
                        dMeasVoltMax = 0
                        Exit Function
                    End If

                    'set input impedance: 1000000 or 50
                    If (nInputImp = 50 Or nInputImp = 1000000) Then
                        WriteMsg(OSCOPE, sINP & ":IMP " & sInputImp)
                    Else
                        Echo("OSCOPE INPUT IMPEDANCE PARAMETER ERROR")
                        dMeasVoltMax = 0
                        Exit Function
                    End If

                    'set input low-pass filter state: OFF or ON
                    If (sLPAS = "ON" Or sLPAS = "OFF") Then
                        WriteMsg(OSCOPE, sINP & ":FILT:LPAS " & sLPAS)
                    Else
                        Echo("OSCOPE INPUT FILTER STATE PARAMETER ERROR")
                        dMeasVoltMax = 0
                        Exit Function
                    End If

                    'set input high-pass filter state: OFF or ON
                    If (sHPAS = "ON" Or sHPAS = "OFF") Then
                        WriteMsg(OSCOPE, sINP & ":FILT:hPAS " & sHPAS)
                    Else
                        Echo("OSCOPE INPUT FILTER STATE PARAMETER ERROR")
                        dMeasVoltMax = 0
                        Exit Function
                    End If

                    'set volt range: 8mV to 40V
                    If (dVoltRange < 0.008 Or dVoltRange > 40) Then
                        Echo("OSCOPE VOLT RANGE ERROR")
                        dMeasVoltMax = 0
                        Exit Function
                    Else
                        Select Case sINP
                            Case "INP1"
                                WriteMsg(OSCOPE, "VOLT1:RANG " & sVoltRange)
                            Case "INP2"
                                WriteMsg(OSCOPE, "VOLT2:RANG " & sVoltRange)
                        End Select
                    End If

                    'set time range:10ns to 50s
                    If (dTimeRange < 0.00000001 Or dTimeRange > 50) Then
                        Echo("OSCOPE TIME RANGE ERROR")
                        dMeasVoltMax = 0
                        Exit Function
                    Else
                        WriteMsg(OSCOPE, "SWE:TIME:RANG " & sTimeRange)
                    End If

                    'set trigger level
                    WriteMsg(OSCOPE, "TRIG:LEV " & sTriggerLevel)

                    'set niose rejection: OFF or ON
                    If (sHYST = "ON" Or sHYST = "OFF") Then
                        WriteMsg(OSCOPE, "TRIG:HYST " & sHYST)
                    Else
                        Echo("OSCOPE TRIGGER INPUT PARAMETER ERROR")
                        dMeasVoltMax = 0
                        Exit Function
                    End If

                    'set trigger slope: POS or NEG
                    If (sTriggerSlope = "POS" Or sTriggerSlope = "NEG") Then
                        WriteMsg(OSCOPE, "TRIG:SLOP " & sTriggerSlope)
                    Else
                        Echo("OSCOPE TRIGGER SLOPE PARAMETER ERROR")
                        dMeasVoltMax = 0
                        Exit Function
                    End If

                    'set trigger source
                    Select Case sTriggerSource
                        Case "INP1", "INP2", "EXT", "ECLT0", "ECLT1"
                            WriteMsg(OSCOPE, "TRIG:SOUR " & sTriggerSource)

                        Case Else
                            Echo("OSCOPE TRIGGER SOURCE PARAMETER ERROR")
                            dMeasVoltMax = 0
                            Exit Function
                    End Select

                Case Else
                    Echo("OSCOPE AUTO SCALE PARAMETER ERROR")
                    dMeasVoltMax = 0
                    Exit Function
            End Select
        End If

        'Make Mesurement
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        If Not bSimulation Then
            Select Case Left(UCase(APROBE), 1)
                Case "O" ' Off
                    WriteMsg(OSCOPE, "MEAS:VOLT:MAX? (@" & sINP & ")")
                    LongDelay((1))
                    nErr = ReadMsg(OSCOPE, sData)
                    WriteMsg(OSCOPE, "*CLS")
                    If nErr <> 0 Then
                        Echo("VISA ERROR: " & nErr)
                        dMeasVoltMax = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(nErr)
                        dMeasVoltMax = 0
                        Exit Function
                    End If
                    dMeasVoltMax = CDbl(sData)
                    gFrmMain.txtMeasured.Text = CStr(dMeasVoltMax)
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
                        '5/14/03 by Soon Nam''''''''''''''''''''''''''''''
                        'set auto scale with analog probe measurements
                        If (sAutoScale = "ON" Or sAutoScale = "AUTO") Then
                            WriteMsg(OSCOPE, "SYST:AUT")
                            LongDelay((1))
                        End If
                        'end add''''''''''''''''''''''''''''''''''''''''''
                        gFrmMain.lblStatus.Text = "Making Measurement ..."
                        WriteMsg(OSCOPE, "MEAS:VOLT:MAX? (@" & sINP & ")")
                        LongDelay((1))
                        nErr = ReadMsg(OSCOPE, sData)
                        WriteMsg(OSCOPE, "*CLS")
                        If nErr <> 0 Then
                            Echo("VISA ERROR: " & nErr)
                            dMeasVoltMax = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(nErr)
                            dMeasVoltMax = 0
                            Exit Function
                        End If
                        dMeasVoltMax = CDbl(sData)
                        gFrmMain.txtMeasured.Text = CStr(dMeasVoltMax)
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

                Case "C" ' Continuous
                    MisProbe = MsgBoxResult.Yes
                    Do While MisProbe = MsgBoxResult.Yes
                        UserEvent = 0
                        Failed = False
                        gFrmMain.TimerProbe.Enabled = True : gFrmMain.fraInstructions.Visible = True
                        Do While (UserEvent = 0) And (Not bProbeClosed)
                            '5/14/03 by Soon Nam''''''''''''''''''''''''''''''
                            'set auto scale with analog probe measurements
                            If (sAutoScale = "ON" Or sAutoScale = "AUTO") Then
                                WriteMsg(OSCOPE, "SYST:AUT")
                                LongDelay((1))
                            End If
                            'end add''''''''''''''''''''''''''''''''''''''''''
                            gFrmMain.lblStatus.Text = "Making Measurement ..."
                            WriteMsg(OSCOPE, "MEAS:VOLT:MAX? (@" & sINP & ")")
                            LongDelay((1))
                            nErr = ReadMsg(OSCOPE, sData)
                            WriteMsg(OSCOPE, "*CLS")
                            If nErr <> 0 Then
                                Echo("VISA ERROR: " & nErr)
                                dMeasVoltMax = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(nErr)
                                dMeasVoltMax = 0
                                Exit Function
                            End If
                            dMeasVoltMax = CDbl(sData)
                            gFrmMain.txtMeasured.Text = CStr(dMeasVoltMax)
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
            dMeasVoltMax = CDbl(InputBox("Command cmdOSCOPE.dMeasVoltMax peformed." & vbCrLf & "Enter Voltage Value:", "SIMULATION MODE"))
            gFrmMain.txtMeasured.Text = CStr(dMeasVoltMax)
            If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON)
        End If

        gFrmMain.lblStatus.Text = sCurrentMsg
    End Function
	
    ''' <summary>
    ''' perform minimum voltage measurement
    ''' </summary>
    ''' <param name="sAutoScale">"ON" or "OFF"</param>
    ''' <param name="sINP">select INP1 or INP2 (OSCOPE input channel)</param>
    ''' <param name="dVoltRange">set the full scale vertical range;  min = 8mV, max = 40V, at reset = 4V</param>
    ''' <param name="dTimeRange">set the full scale horizontal range for sweep;  min = 10 ns, max = 50 s, at reset = 1 ms</param>
    ''' <param name="dTriggerLevel">set trigger level voltage (0V at reset)</param>
    ''' <param name="sTriggerSlope">set trigger slope ("POS" at reset)</param>
    ''' <param name="sTriggerSource">set trigger source ("INP1" at reset)</param>
    ''' <param name="nInputImp">select input impedance 50 or 1M (1M at reset)</param>
    ''' <param name="sInputCoupling">select input coupling for input channels ("DC" at reset)</param>
    ''' <param name="sLPAS">select low-pass filter ("OFF" at reset)</param>
    ''' <param name="sHPAS">select high-pass filter ("OFF" at reset)</param>
    ''' <param name="sHYST">set noise rejection ("OFF" at reset)</param>
    ''' <param name="APROBE">Indicates AProbe usage; "OFF", "Single" Measurement, or "Contiuous" Measurement</param>
    ''' <returns>Measured voltage</returns>
    ''' <remarks>sAutoScale: 
    '''             auto scale can be used to determine vertical and horizontal parameters when sAutoScale = "ON", optional parameters are ignored.  
    ''' Autoscale should only be used with relatively stable input signals having a duty cycle of greater than 0.5% and a frequency greater than 50Hz.
    ''' sINP:
    '''        At reset, INP1 is on and INP2 if off.  Max input voltage that can be applied to the two input connectors is 5 Vrms at 50 ohms 
    ''' or +/-250V at 1Mohms.</remarks>
    Public Function dMeasVoltMin(ByRef sAutoScale As String, ByRef sINP As String, Optional ByRef dVoltRange As Double = 4,
                                 Optional ByRef dTimeRange As Double = 0.001, Optional ByRef dTriggerLevel As Double = 0,
                                 Optional ByRef sTriggerSlope As String = "POS", Optional ByRef sTriggerSource As String = "INP1",
                                 Optional ByRef nInputImp As Integer = 1000000, Optional ByRef sInputCoupling As String = "DC",
                                 Optional ByRef sLPAS As String = "OFF", Optional ByRef sHPAS As String = "OFF",
                                 Optional ByRef sHYST As String = "OFF", Optional ByRef APROBE As String = "OFF") As Double
        Dim sData As String = ""
        Dim nErr As Integer
        Dim sTimeRange As String
        Dim sVoltRange As String
        Dim sTriggerLevel As String
        Dim sInputImp As String
        Dim sCurrentMsg As String

        dMeasVoltMin = 0.0

        sCurrentMsg = gFrmMain.lblStatus.Text
        If APROBE <> "OFF" Then
            gFrmMain.lblStatus.Text = "APPLY PROBE"
        End If

        'convert data type to string type
        sInputImp = CStr(nInputImp)
        sTimeRange = CStr(dTimeRange)
        sVoltRange = CStr(dVoltRange)
        sTriggerLevel = CStr(dTriggerLevel)

        'convert string data to upper case
        sAutoScale = UCase(sAutoScale)
        sINP = UCase(sINP)
        sInputCoupling = UCase(sInputCoupling)
        sLPAS = UCase(sLPAS)
        sHPAS = UCase(sHPAS)
        sHYST = UCase(sHYST)
        sTriggerSlope = UCase(sTriggerSlope)
        sTriggerSource = UCase(sTriggerSource)

        'display measurement info to TPS shell
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        gFrmMain.txtInstrument.Text = "OSCOPE"
        gFrmMain.txtCommand.Text = "dMeasVoltMin"

        'Setup OSCOPE for Frequency Measurement
        If Not bSimulation Then
            WriteMsg(OSCOPE, "SYST:LANG SCPI")

            LongDelay((1))
            WriteMsg(OSCOPE, "*CLS")
            WriteMsg(OSCOPE, "*RST")

            'select auto scale
            Select Case sAutoScale
                Case "ON", "AUTO"
                    WriteMsg(OSCOPE, "SYST:AUT")
                    LongDelay((2))

                Case "OFF" 'manually set vertical and horizontal ranges
                    'set input channel: INP1 or INP2
                    If (sINP = "INP1" Or sINP = "INP2") Then
                        WriteMsg(OSCOPE, sINP & " ON")
                    Else
                        Echo("OSCOPE INPUT CHANNEL SELECTION ERROR")
                        dMeasVoltMin = 0
                        Exit Function
                    End If

                    'set input coupling: DC or AC
                    If (sInputCoupling = "DC" Or sInputCoupling = "AC") Then
                        WriteMsg(OSCOPE, sINP & ":COUP " & sInputCoupling)
                    Else
                        Echo("OSCOPE INPUT COUPLING PARAMETER ERROR")
                        dMeasVoltMin = 0
                        Exit Function
                    End If

                    'set input impedance: 1000000 or 50
                    If (nInputImp = 50 Or nInputImp = 1000000) Then
                        WriteMsg(OSCOPE, sINP & ":IMP " & sInputImp)
                    Else
                        Echo("OSCOPE INPUT IMPEDANCE PARAMETER ERROR")
                        dMeasVoltMin = 0
                        Exit Function
                    End If

                    'set input low-pass filter state: OFF or ON
                    If (sLPAS = "ON" Or sLPAS = "OFF") Then
                        WriteMsg(OSCOPE, sINP & ":FILT:LPAS " & sLPAS)
                    Else
                        Echo("OSCOPE INPUT FILTER STATE PARAMETER ERROR")
                        dMeasVoltMin = 0
                        Exit Function
                    End If

                    'set input high-pass filter state: OFF or ON
                    If (sHPAS = "ON" Or sHPAS = "OFF") Then
                        WriteMsg(OSCOPE, sINP & ":FILT:hPAS " & sHPAS)
                    Else
                        Echo("OSCOPE INPUT FILTER STATE PARAMETER ERROR")
                        dMeasVoltMin = 0
                        Exit Function
                    End If

                    'set volt range: 8mV to 40V
                    If (dVoltRange < 0.008 Or dVoltRange > 40) Then
                        Echo("OSCOPE VOLT RANGE ERROR")
                        dMeasVoltMin = 0
                        Exit Function
                    Else
                        Select Case sINP
                            Case "INP1"
                                WriteMsg(OSCOPE, "VOLT1:RANG " & sVoltRange)
                            Case "INP2"
                                WriteMsg(OSCOPE, "VOLT2:RANG " & sVoltRange)
                        End Select
                    End If

                    'set time range:10ns to 50s
                    If (dTimeRange < 0.00000001 Or dTimeRange > 50) Then
                        Echo("OSCOPE TIME RANGE ERROR")
                        dMeasVoltMin = 0
                        Exit Function
                    Else
                        WriteMsg(OSCOPE, "SWE:TIME:RANG " & sTimeRange)
                    End If

                    'set trigger level
                    WriteMsg(OSCOPE, "TRIG:LEV " & sTriggerLevel)

                    'set niose rejection: OFF or ON
                    If (sHYST = "ON" Or sHYST = "OFF") Then
                        WriteMsg(OSCOPE, "TRIG:HYST " & sHYST)
                    Else
                        Echo("OSCOPE TRIGGER INPUT PARAMETER ERROR")
                        dMeasVoltMin = 0
                        Exit Function
                    End If

                    'set trigger slope: POS or NEG
                    If (sTriggerSlope = "POS" Or sTriggerSlope = "NEG") Then
                        WriteMsg(OSCOPE, "TRIG:SLOP " & sTriggerSlope)
                    Else
                        Echo("OSCOPE TRIGGER SLOPE PARAMETER ERROR")
                        dMeasVoltMin = 0
                        Exit Function
                    End If

                    'set trigger source
                    Select Case sTriggerSource
                        Case "INP1", "INP2", "EXT", "ECLT0", "ECLT1"
                            WriteMsg(OSCOPE, "TRIG:SOUR " & sTriggerSource)

                        Case Else
                            Echo("OSCOPE TRIGGER SOURCE PARAMETER ERROR")
                            dMeasVoltMin = 0
                            Exit Function
                    End Select

                Case Else
                    Echo("OSCOPE AUTO SCALE PARAMETER ERROR")
                    dMeasVoltMin = 0
                    Exit Function
            End Select
        End If

        'Make Mesurement
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        If Not bSimulation Then
            Select Case Left(UCase(APROBE), 1)
                Case "O" ' Off
                    WriteMsg(OSCOPE, "MEAS:VOLT:MIN? (@" & sINP & ")")
                    LongDelay((1))
                    nErr = ReadMsg(OSCOPE, sData)
                    WriteMsg(OSCOPE, "*CLS")
                    If nErr <> 0 Then
                        Echo("VISA ERROR: " & nErr)
                        dMeasVoltMin = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(nErr)
                        dMeasVoltMin = 0
                        Exit Function
                    End If
                    dMeasVoltMin = CDbl(sData)
                    gFrmMain.txtMeasured.Text = CStr(dMeasVoltMin)
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
                        '5/14/03 by Soon Nam''''''''''''''''''''''''''''''
                        'set auto scale with analog probe measurements
                        If (sAutoScale = "ON" Or sAutoScale = "AUTO") Then
                            WriteMsg(OSCOPE, "SYST:AUT")
                            LongDelay((1))
                        End If
                        'end add''''''''''''''''''''''''''''''''''''''''''
                        gFrmMain.lblStatus.Text = "Making Measurement ..."
                        WriteMsg(OSCOPE, "MEAS:VOLT:MIN? (@" & sINP & ")")
                        LongDelay((1))
                        nErr = ReadMsg(OSCOPE, sData)
                        WriteMsg(OSCOPE, "*CLS")
                        If nErr <> 0 Then
                            Echo("VISA ERROR: " & nErr)
                            dMeasVoltMin = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(nErr)
                            dMeasVoltMin = 0
                            Exit Function
                        End If
                        dMeasVoltMin = CDbl(sData)
                        gFrmMain.txtMeasured.Text = CStr(dMeasVoltMin)
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

                Case "C" ' Continuous
                    MisProbe = MsgBoxResult.Yes
                    Do While MisProbe = MsgBoxResult.Yes
                        UserEvent = 0
                        Failed = False
                        gFrmMain.TimerProbe.Enabled = True : gFrmMain.fraInstructions.Visible = True
                        Do While (UserEvent = 0) And (Not bProbeClosed)
                            '5/14/03 by Soon Nam''''''''''''''''''''''''''''''
                            'set auto scale with analog probe measurements
                            If (sAutoScale = "ON" Or sAutoScale = "AUTO") Then
                                WriteMsg(OSCOPE, "SYST:AUT")
                                LongDelay((1))
                            End If
                            'end add''''''''''''''''''''''''''''''''''''''''''
                            gFrmMain.lblStatus.Text = "Making Measurement ..."
                            WriteMsg(OSCOPE, "MEAS:VOLT:MIN? (@" & sINP & ")")
                            LongDelay((1))
                            nErr = ReadMsg(OSCOPE, sData)
                            WriteMsg(OSCOPE, "*CLS")
                            If nErr <> 0 Then
                                Echo("VISA ERROR: " & nErr)
                                dMeasVoltMin = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(nErr)
                                dMeasVoltMin = 0
                                Exit Function
                            End If
                            dMeasVoltMin = CDbl(sData)
                            gFrmMain.txtMeasured.Text = CStr(dMeasVoltMin)
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
            dMeasVoltMin = CDbl(InputBox("Command cmdOSCOPE.dMeasVoltMin peformed." & vbCrLf & "Enter Voltage Value:", "SIMULATION MODE"))
            gFrmMain.txtMeasured.Text = CStr(dMeasVoltMin)
            If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON)
        End If

        gFrmMain.lblStatus.Text = sCurrentMsg
    End Function
	
    ''' <summary>
    ''' perform an overshoot measurement on falling edge of signal: ((LOW-MIN)/AMPL)*100
    ''' </summary>
    ''' <param name="sAutoScale">"ON" or "OFF"</param>
    ''' <param name="sINP">select INP1 or INP2 (OSCOPE input channel)</param>
    ''' <param name="dVoltRange">set the full scale vertical range;  min = 8mV, max = 40V, at reset = 4V</param>
    ''' <param name="dTimeRange">set the full scale horizontal range for sweep;  min = 10 ns, max = 50 s, at reset = 1 ms</param>
    ''' <param name="dTriggerLevel">set trigger level voltage (0V at reset)</param>
    ''' <param name="sTriggerSlope">set trigger slope ("POS" at reset)</param>
    ''' <param name="sTriggerSource">set trigger source ("INP1" at reset)</param>
    ''' <param name="nInputImp">select input impedance 50 or 1M (1M at reset)</param>
    ''' <param name="sInputCoupling">select input coupling for input channels ("DC" at reset)</param>
    ''' <param name="sLPAS">select low-pass filter ("OFF" at reset)</param>
    ''' <param name="sHPAS">select high-pass filter ("OFF" at reset)</param>
    ''' <param name="sHYST">set noise rejection ("OFF" at reset)</param>
    ''' <param name="APROBE">Indicates AProbe usage; "OFF", "Single" Measurement, or "Contiuous" Measurement</param>
    ''' <returns>Measured falling edge overshoot</returns>
    ''' <remarks>sAutoScale: 
    '''             auto scale can be used to determine vertical and horizontal parameters when sAutoScale = "ON", optional parameters are ignored.  
    ''' Autoscale should only be used with relatively stable input signals having a duty cycle of greater than 0.5% and a frequency greater than 50Hz.
    ''' sINP:
    '''        At reset, INP1 is on and INP2 if off.  Max input voltage that can be applied to the two input connectors is 5 Vrms at 50 ohms 
    ''' or +/-250V at 1Mohms.</remarks>
    Public Function dMeasFallOver(ByRef sAutoScale As String, ByRef sINP As String, Optional ByRef dVoltRange As Double = 4,
                                  Optional ByRef dTimeRange As Double = 0.001, Optional ByRef dTriggerLevel As Double = 0,
                                  Optional ByRef sTriggerSlope As String = "POS", Optional ByRef sTriggerSource As String = "INP1",
                                  Optional ByRef nInputImp As Integer = 1000000, Optional ByRef sInputCoupling As String = "DC",
                                  Optional ByRef sLPAS As String = "OFF", Optional ByRef sHPAS As String = "OFF",
                                  Optional ByRef sHYST As String = "OFF", Optional ByRef APROBE As String = "OFF") As Double
        Dim sData As String = ""
        Dim nErr As Integer
        Dim sTimeRange As String
        Dim sVoltRange As String
        Dim sTriggerLevel As String
        Dim sInputImp As String
        Dim sCurrentMsg As String

        dMeasFallOver = 0.0

        sCurrentMsg = gFrmMain.lblStatus.Text
        If APROBE <> "OFF" Then
            gFrmMain.lblStatus.Text = "APPLY PROBE"
        End If

        'convert data type to string type
        sInputImp = CStr(nInputImp)
        sTimeRange = CStr(dTimeRange)
        sVoltRange = CStr(dVoltRange)
        sTriggerLevel = CStr(dTriggerLevel)

        'convert string data to upper case
        sAutoScale = UCase(sAutoScale)
        sINP = UCase(sINP)
        sInputCoupling = UCase(sInputCoupling)
        sLPAS = UCase(sLPAS)
        sHPAS = UCase(sHPAS)
        sHYST = UCase(sHYST)
        sTriggerSlope = UCase(sTriggerSlope)
        sTriggerSource = UCase(sTriggerSource)

        'display measurement info to TPS shell
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        gFrmMain.txtInstrument.Text = "OSCOPE"
        gFrmMain.txtCommand.Text = "dMeasFallOver"

        'Setup OSCOPE for FALL:OVERshoot Measurement
        If Not bSimulation Then
            WriteMsg(OSCOPE, "SYST:LANG SCPI")

            LongDelay((1))
            WriteMsg(OSCOPE, "*CLS")
            WriteMsg(OSCOPE, "*RST")

            'select auto scale
            Select Case sAutoScale
                Case "ON", "AUTO"
                    WriteMsg(OSCOPE, "SYST:AUT")
                    LongDelay((2))

                Case "OFF" 'manually set vertical and horizontal ranges
                    'set input channel: INP1 or INP2
                    If (sINP = "INP1" Or sINP = "INP2") Then
                        WriteMsg(OSCOPE, sINP & " ON")
                    Else
                        Echo("OSCOPE INPUT CHANNEL SELECTION ERROR")
                        dMeasFallOver = 0
                        Exit Function
                    End If

                    'set input coupling: DC or AC
                    If (sInputCoupling = "DC" Or sInputCoupling = "AC") Then
                        WriteMsg(OSCOPE, sINP & ":COUP " & sInputCoupling)
                    Else
                        Echo("OSCOPE INPUT COUPLING PARAMETER ERROR")
                        dMeasFallOver = 0
                        Exit Function
                    End If

                    'set input impedance: 1000000 or 50
                    If (nInputImp = 50 Or nInputImp = 1000000) Then
                        WriteMsg(OSCOPE, sINP & ":IMP " & sInputImp)
                    Else
                        Echo("OSCOPE INPUT IMPEDANCE PARAMETER ERROR")
                        dMeasFallOver = 0
                        Exit Function
                    End If

                    'set input low-pass filter state: OFF or ON
                    If (sLPAS = "ON" Or sLPAS = "OFF") Then
                        WriteMsg(OSCOPE, sINP & ":FILT:LPAS " & sLPAS)
                    Else
                        Echo("OSCOPE INPUT FILTER STATE PARAMETER ERROR")
                        dMeasFallOver = 0
                        Exit Function
                    End If

                    'set input high-pass filter state: OFF or ON
                    If (sHPAS = "ON" Or sHPAS = "OFF") Then
                        WriteMsg(OSCOPE, sINP & ":FILT:hPAS " & sHPAS)
                    Else
                        Echo("OSCOPE INPUT FILTER STATE PARAMETER ERROR")
                        dMeasFallOver = 0
                        Exit Function
                    End If

                    'set volt range: 8mV to 40V
                    If (dVoltRange < 0.008 Or dVoltRange > 40) Then
                        Echo("OSCOPE VOLT RANGE ERROR")
                        dMeasFallOver = 0
                        Exit Function
                    Else
                        Select Case sINP
                            Case "INP1"
                                WriteMsg(OSCOPE, "VOLT1:RANG " & sVoltRange)
                            Case "INP2"
                                WriteMsg(OSCOPE, "VOLT2:RANG " & sVoltRange)
                        End Select
                    End If

                    'set time range:10ns to 50s
                    If (dTimeRange < 0.00000001 Or dTimeRange > 50) Then
                        Echo("OSCOPE TIME RANGE ERROR")
                        dMeasFallOver = 0
                        Exit Function
                    Else
                        WriteMsg(OSCOPE, "SWE:TIME:RANG " & sTimeRange)
                    End If

                    'set trigger level
                    WriteMsg(OSCOPE, "TRIG:LEV " & sTriggerLevel)

                    'set niose rejection: OFF or ON
                    If (sHYST = "ON" Or sHYST = "OFF") Then
                        WriteMsg(OSCOPE, "TRIG:HYST " & sHYST)
                    Else
                        Echo("OSCOPE TRIGGER INPUT PARAMETER ERROR")
                        dMeasFallOver = 0
                        Exit Function
                    End If

                    'set trigger slope: POS or NEG
                    If (sTriggerSlope = "POS" Or sTriggerSlope = "NEG") Then
                        WriteMsg(OSCOPE, "TRIG:SLOP " & sTriggerSlope)
                    Else
                        Echo("OSCOPE TRIGGER SLOPE PARAMETER ERROR")
                        dMeasFallOver = 0
                        Exit Function
                    End If

                    'set trigger source
                    Select Case sTriggerSource
                        Case "INP1", "INP2", "EXT", "ECLT0", "ECLT1"
                            WriteMsg(OSCOPE, "TRIG:SOUR " & sTriggerSource)

                        Case Else
                            Echo("OSCOPE TRIGGER SOURCE PARAMETER ERROR")
                            dMeasFallOver = 0
                            Exit Function
                    End Select

                Case Else
                    Echo("OSCOPE AUTO SCALE PARAMETER ERROR")
                    dMeasFallOver = 0
                    Exit Function
            End Select
        End If

        'Make Mesurement
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        If Not bSimulation Then
            Select Case Left(UCase(APROBE), 1)
                Case "O" ' Off
                    WriteMsg(OSCOPE, "MEAS:VOLT:FALL:OVER? (@" & sINP & ")")
                    LongDelay((1))
                    nErr = ReadMsg(OSCOPE, sData)
                    WriteMsg(OSCOPE, "*CLS")
                    If nErr <> 0 Then
                        Echo("VISA ERROR: " & nErr)
                        dMeasFallOver = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(nErr)
                        dMeasFallOver = 0
                        Exit Function
                    End If
                    dMeasFallOver = CDbl(sData)
                    gFrmMain.txtMeasured.Text = CStr(dMeasFallOver)
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
                        '5/14/03 by Soon Nam''''''''''''''''''''''''''''''
                        'set auto scale with analog probe measurements
                        If (sAutoScale = "ON" Or sAutoScale = "AUTO") Then
                            WriteMsg(OSCOPE, "SYST:AUT")
                            LongDelay((1))
                        End If
                        'end add''''''''''''''''''''''''''''''''''''''''''
                        gFrmMain.lblStatus.Text = "Making Measurement ..."
                        WriteMsg(OSCOPE, "MEAS:VOLT:FALL:OVER? (@" & sINP & ")")
                        LongDelay((1))
                        nErr = ReadMsg(OSCOPE, sData)
                        WriteMsg(OSCOPE, "*CLS")
                        If nErr <> 0 Then
                            Echo("VISA ERROR: " & nErr)
                            dMeasFallOver = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(nErr)
                            dMeasFallOver = 0
                            Exit Function
                        End If
                        dMeasFallOver = CDbl(sData)
                        gFrmMain.txtMeasured.Text = CStr(dMeasFallOver)
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

                Case "C" ' Continuous
                    MisProbe = MsgBoxResult.Yes
                    Do While MisProbe = MsgBoxResult.Yes
                        UserEvent = 0
                        Failed = False
                        gFrmMain.TimerProbe.Enabled = True : gFrmMain.fraInstructions.Visible = True
                        Do While (UserEvent = 0) And (Not bProbeClosed)
                            '5/14/03 by Soon Nam''''''''''''''''''''''''''''''
                            'set auto scale with analog probe measurements
                            If (sAutoScale = "ON" Or sAutoScale = "AUTO") Then
                                WriteMsg(OSCOPE, "SYST:AUT")
                                LongDelay((1))
                            End If
                            'end add''''''''''''''''''''''''''''''''''''''''''
                            gFrmMain.lblStatus.Text = "Making Measurement ..."
                            WriteMsg(OSCOPE, "MEAS:VOLT:FALL:OVER? (@" & sINP & ")")
                            LongDelay((1))
                            nErr = ReadMsg(OSCOPE, sData)
                            WriteMsg(OSCOPE, "*CLS")
                            If nErr <> 0 Then
                                Echo("VISA ERROR: " & nErr)
                                dMeasFallOver = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(nErr)
                                dMeasFallOver = 0
                                Exit Function
                            End If
                            dMeasFallOver = CDbl(sData)
                            gFrmMain.txtMeasured.Text = CStr(dMeasFallOver)
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
            dMeasFallOver = CDbl(InputBox("Command cmdOSCOPE.dMeasFallOver peformed." & vbCrLf & "Enter Value:", "SIMULATION MODE"))
            gFrmMain.txtMeasured.Text = CStr(dMeasFallOver)
            If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON)
        End If

        gFrmMain.lblStatus.Text = sCurrentMsg
    End Function
	
    ''' <summary>
    ''' perform an overshoot measurement on rising edge of signal: ((HIGH-MAX)/AMPL)*100
    ''' </summary>
    ''' <param name="sAutoScale">"ON" or "OFF"</param>
    ''' <param name="sINP">select INP1 or INP2 (OSCOPE input channel)</param>
    ''' <param name="dVoltRange">set the full scale vertical range;  min = 8mV, max = 40V, at reset = 4V</param>
    ''' <param name="dTimeRange">set the full scale horizontal range for sweep;  min = 10 ns, max = 50 s, at reset = 1 ms</param>
    ''' <param name="dTriggerLevel">set trigger level voltage (0V at reset)</param>
    ''' <param name="sTriggerSlope">set trigger slope ("POS" at reset)</param>
    ''' <param name="sTriggerSource">set trigger source ("INP1" at reset)</param>
    ''' <param name="nInputImp">select input impedance 50 or 1M (1M at reset)</param>
    ''' <param name="sInputCoupling">select input coupling for input channels ("DC" at reset)</param>
    ''' <param name="sLPAS">select low-pass filter ("OFF" at reset)</param>
    ''' <param name="sHPAS">select high-pass filter ("OFF" at reset)</param>
    ''' <param name="sHYST">set noise rejection ("OFF" at reset)</param>
    ''' <param name="APROBE">Indicates AProbe usage; "OFF", "Single" Measurement, or "Contiuous" Measurement</param>
    ''' <returns>Measured rising edge overshoot</returns>
    ''' <remarks>sAutoScale: 
    '''             auto scale can be used to determine vertical and horizontal parameters when sAutoScale = "ON", optional parameters are ignored.  
    ''' Autoscale should only be used with relatively stable input signals having a duty cycle of greater than 0.5% and a frequency greater than 50Hz.
    ''' sINP:
    '''        At reset, INP1 is on and INP2 if off.  Max input voltage that can be applied to the two input connectors is 5 Vrms at 50 ohms 
    ''' or +/-250V at 1Mohms.</remarks>
    Public Function dMeasRiseOver(ByRef sAutoScale As String, ByRef sINP As String, Optional ByRef dVoltRange As Double = 4,
                                  Optional ByRef dTimeRange As Double = 0.001, Optional ByRef dTriggerLevel As Double = 0,
                                  Optional ByRef sTriggerSlope As String = "POS", Optional ByRef sTriggerSource As String = "INP1",
                                  Optional ByRef nInputImp As Integer = 1000000, Optional ByRef sInputCoupling As String = "DC",
                                  Optional ByRef sLPAS As String = "OFF", Optional ByRef sHPAS As String = "OFF",
                                  Optional ByRef sHYST As String = "OFF", Optional ByRef APROBE As String = "OFF") As Double
        Dim sData As String = ""
        Dim nErr As Integer
        Dim sTimeRange As String
        Dim sVoltRange As String
        Dim sTriggerLevel As String
        Dim sInputImp As String
        Dim sCurrentMsg As String

        dMeasRiseOver = 0.0

        sCurrentMsg = gFrmMain.lblStatus.Text
        If APROBE <> "OFF" Then
            gFrmMain.lblStatus.Text = "APPLY PROBE"
        End If

        'convert data type to string type
        sInputImp = CStr(nInputImp)
        sTimeRange = CStr(dTimeRange)
        sVoltRange = CStr(dVoltRange)
        sTriggerLevel = CStr(dTriggerLevel)

        'convert string data to upper case
        sAutoScale = UCase(sAutoScale)
        sINP = UCase(sINP)
        sInputCoupling = UCase(sInputCoupling)
        sLPAS = UCase(sLPAS)
        sHPAS = UCase(sHPAS)
        sHYST = UCase(sHYST)
        sTriggerSlope = UCase(sTriggerSlope)
        sTriggerSource = UCase(sTriggerSource)

        'display measurement info to TPS shell
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        gFrmMain.txtInstrument.Text = "OSCOPE"
        gFrmMain.txtCommand.Text = "dMeasRiseOver"

        'Setup OSCOPE for FALL:OVERshoot Measurement
        If Not bSimulation Then
            WriteMsg(OSCOPE, "SYST:LANG SCPI")

            LongDelay((1))
            WriteMsg(OSCOPE, "*CLS")
            WriteMsg(OSCOPE, "*RST")

            'select auto scale
            Select Case sAutoScale
                Case "ON", "AUTO"
                    WriteMsg(OSCOPE, "SYST:AUT")
                    LongDelay((2))

                Case "OFF" 'manually set vertical and horizontal ranges
                    'set input channel: INP1 or INP2
                    If (sINP = "INP1" Or sINP = "INP2") Then
                        WriteMsg(OSCOPE, sINP & " ON")
                    Else
                        Echo("OSCOPE INPUT CHANNEL SELECTION ERROR")
                        dMeasRiseOver = 0
                        Exit Function
                    End If

                    'set input coupling: DC or AC
                    If (sInputCoupling = "DC" Or sInputCoupling = "AC") Then
                        WriteMsg(OSCOPE, sINP & ":COUP " & sInputCoupling)
                    Else
                        Echo("OSCOPE INPUT COUPLING PARAMETER ERROR")
                        dMeasRiseOver = 0
                        Exit Function
                    End If

                    'set input impedance: 1000000 or 50
                    If (nInputImp = 50 Or nInputImp = 1000000) Then
                        WriteMsg(OSCOPE, sINP & ":IMP " & sInputImp)
                    Else
                        Echo("OSCOPE INPUT IMPEDANCE PARAMETER ERROR")
                        dMeasRiseOver = 0
                        Exit Function
                    End If

                    'set input low-pass filter state: OFF or ON
                    If (sLPAS = "ON" Or sLPAS = "OFF") Then
                        WriteMsg(OSCOPE, sINP & ":FILT:LPAS " & sLPAS)
                    Else
                        Echo("OSCOPE INPUT FILTER STATE PARAMETER ERROR")
                        dMeasRiseOver = 0
                        Exit Function
                    End If

                    'set input high-pass filter state: OFF or ON
                    If (sHPAS = "ON" Or sHPAS = "OFF") Then
                        WriteMsg(OSCOPE, sINP & ":FILT:hPAS " & sHPAS)
                    Else
                        Echo("OSCOPE INPUT FILTER STATE PARAMETER ERROR")
                        dMeasRiseOver = 0
                        Exit Function
                    End If

                    'set volt range: 8mV to 40V
                    If (dVoltRange < 0.008 Or dVoltRange > 40) Then
                        Echo("OSCOPE VOLT RANGE ERROR")
                        dMeasRiseOver = 0
                        Exit Function
                    Else
                        Select Case sINP
                            Case "INP1"
                                WriteMsg(OSCOPE, "VOLT1:RANG " & sVoltRange)
                            Case "INP2"
                                WriteMsg(OSCOPE, "VOLT2:RANG " & sVoltRange)
                        End Select
                    End If

                    'set time range:10ns to 50s
                    If (dTimeRange < 0.00000001 Or dTimeRange > 50) Then
                        Echo("OSCOPE TIME RANGE ERROR")
                        dMeasRiseOver = 0
                        Exit Function
                    Else
                        WriteMsg(OSCOPE, "SWE:TIME:RANG " & sTimeRange)
                    End If

                    'set trigger level
                    WriteMsg(OSCOPE, "TRIG:LEV " & sTriggerLevel)

                    'set niose rejection: OFF or ON
                    If (sHYST = "ON" Or sHYST = "OFF") Then
                        WriteMsg(OSCOPE, "TRIG:HYST " & sHYST)
                    Else
                        Echo("OSCOPE TRIGGER INPUT PARAMETER ERROR")
                        dMeasRiseOver = 0
                        Exit Function
                    End If

                    'set trigger slope: POS or NEG
                    If (sTriggerSlope = "POS" Or sTriggerSlope = "NEG") Then
                        WriteMsg(OSCOPE, "TRIG:SLOP " & sTriggerSlope)
                    Else
                        Echo("OSCOPE TRIGGER SLOPE PARAMETER ERROR")
                        dMeasRiseOver = 0
                        Exit Function
                    End If

                    'set trigger source
                    Select Case sTriggerSource
                        Case "INP1", "INP2", "EXT", "ECLT0", "ECLT1"
                            WriteMsg(OSCOPE, "TRIG:SOUR " & sTriggerSource)

                        Case Else
                            Echo("OSCOPE TRIGGER SOURCE PARAMETER ERROR")
                            dMeasRiseOver = 0
                            Exit Function
                    End Select

                Case Else
                    Echo("OSCOPE AUTO SCALE PARAMETER ERROR")
                    dMeasRiseOver = 0
                    Exit Function
            End Select
        End If

        'Make Mesurement
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        If Not bSimulation Then
            Select Case Left(UCase(APROBE), 1)
                Case "O" ' Off
                    WriteMsg(OSCOPE, "MEAS:VOLT:RISE:OVER? (@" & sINP & ")")
                    LongDelay((1))
                    nErr = ReadMsg(OSCOPE, sData)
                    WriteMsg(OSCOPE, "*CLS")
                    If nErr <> 0 Then
                        Echo("VISA ERROR: " & nErr)
                        dMeasRiseOver = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(nErr)
                        dMeasRiseOver = 0
                        Exit Function
                    End If
                    dMeasRiseOver = CDbl(sData)
                    gFrmMain.txtMeasured.Text = CStr(dMeasRiseOver)
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
                        '5/14/03 by Soon Nam''''''''''''''''''''''''''''''
                        'set auto scale with analog probe measurements
                        If (sAutoScale = "ON" Or sAutoScale = "AUTO") Then
                            WriteMsg(OSCOPE, "SYST:AUT")
                            LongDelay((1))
                        End If
                        'end add''''''''''''''''''''''''''''''''''''''''''
                        gFrmMain.lblStatus.Text = "Making Measurement ..."
                        WriteMsg(OSCOPE, "MEAS:VOLT:RISE:OVER? (@" & sINP & ")")
                        LongDelay((1))
                        nErr = ReadMsg(OSCOPE, sData)
                        WriteMsg(OSCOPE, "*CLS")
                        If nErr <> 0 Then
                            Echo("VISA ERROR: " & nErr)
                            dMeasRiseOver = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(nErr)
                            dMeasRiseOver = 0
                            Exit Function
                        End If
                        dMeasRiseOver = CDbl(sData)
                        gFrmMain.txtMeasured.Text = CStr(dMeasRiseOver)
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

                Case "C" ' Continuous
                    MisProbe = MsgBoxResult.Yes
                    Do While MisProbe = MsgBoxResult.Yes
                        UserEvent = 0
                        Failed = False
                        gFrmMain.TimerProbe.Enabled = True : gFrmMain.fraInstructions.Visible = True
                        Do While (UserEvent = 0) And (Not bProbeClosed)
                            '5/14/03 by Soon Nam''''''''''''''''''''''''''''''
                            'set auto scale with analog probe measurements
                            If (sAutoScale = "ON" Or sAutoScale = "AUTO") Then
                                WriteMsg(OSCOPE, "SYST:AUT")
                                LongDelay((1))
                            End If
                            'end add''''''''''''''''''''''''''''''''''''''''''
                            gFrmMain.lblStatus.Text = "Making Measurement ..."
                            WriteMsg(OSCOPE, "MEAS:VOLT:RISE:OVER? (@" & sINP & ")")
                            LongDelay((1))
                            nErr = ReadMsg(OSCOPE, sData)
                            WriteMsg(OSCOPE, "*CLS")
                            If nErr <> 0 Then
                                Echo("VISA ERROR: " & nErr)
                                dMeasRiseOver = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(nErr)
                                dMeasRiseOver = 0
                                Exit Function
                            End If
                            dMeasRiseOver = CDbl(sData)
                            gFrmMain.txtMeasured.Text = CStr(dMeasRiseOver)
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
            dMeasRiseOver = CDbl(InputBox("Command cmdOSCOPE.dMeasRiseOver peformed." & vbCrLf & "Enter Value:", "SIMULATION MODE"))
            gFrmMain.txtMeasured.Text = CStr(dMeasRiseOver)
            If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON)
        End If

        gFrmMain.lblStatus.Text = sCurrentMsg
    End Function

    ''' <summary>
    ''' perform an preshoot measurement on falling edge of signal: ((HIGH-MAX)/AMPL)*100
    ''' </summary>
    ''' <param name="sAutoScale">"ON" or "OFF"</param>
    ''' <param name="sINP">select INP1 or INP2 (OSCOPE input channel)</param>
    ''' <param name="dVoltRange">set the full scale vertical range;  min = 8mV, max = 40V, at reset = 4V</param>
    ''' <param name="dTimeRange">set the full scale horizontal range for sweep;  min = 10 ns, max = 50 s, at reset = 1 ms</param>
    ''' <param name="dTriggerLevel">set trigger level voltage (0V at reset)</param>
    ''' <param name="sTriggerSlope">set trigger slope ("POS" at reset)</param>
    ''' <param name="sTriggerSource">set trigger source ("INP1" at reset)</param>
    ''' <param name="nInputImp">select input impedance 50 or 1M (1M at reset)</param>
    ''' <param name="sInputCoupling">select input coupling for input channels ("DC" at reset)</param>
    ''' <param name="sLPAS">select low-pass filter ("OFF" at reset)</param>
    ''' <param name="sHPAS">select high-pass filter ("OFF" at reset)</param>
    ''' <param name="sHYST">set noise rejection ("OFF" at reset)</param>
    ''' <param name="APROBE">Indicates AProbe usage; "OFF", "Single" Measurement, or "Contiuous" Measurement</param>
    ''' <returns>Measured falling edge preshoot</returns>
    ''' <remarks>sAutoScale: 
    '''             auto scale can be used to determine vertical and horizontal parameters when sAutoScale = "ON", optional parameters are ignored.  
    ''' Autoscale should only be used with relatively stable input signals having a duty cycle of greater than 0.5% and a frequency greater than 50Hz.
    ''' sINP:
    '''        At reset, INP1 is on and INP2 if off.  Max input voltage that can be applied to the two input connectors is 5 Vrms at 50 ohms 
    ''' or +/-250V at 1Mohms.</remarks>
    Public Function dMeasFallPre(ByRef sAutoScale As String, ByRef sINP As String, Optional ByRef dVoltRange As Double = 4,
                                 Optional ByRef dTimeRange As Double = 0.001, Optional ByRef dTriggerLevel As Double = 0,
                                 Optional ByRef sTriggerSlope As String = "POS", Optional ByRef sTriggerSource As String = "INP1",
                                 Optional ByRef nInputImp As Integer = 1000000, Optional ByRef sInputCoupling As String = "DC",
                                 Optional ByRef sLPAS As String = "OFF", Optional ByRef sHPAS As String = "OFF",
                                 Optional ByRef sHYST As String = "OFF", Optional ByRef APROBE As String = "OFF") As Double
        Dim sData As String = ""
        Dim nErr As Integer
        Dim sTimeRange As String
        Dim sVoltRange As String
        Dim sTriggerLevel As String
        Dim sInputImp As String
        Dim sCurrentMsg As String

        dMeasFallPre = 0.0

        sCurrentMsg = gFrmMain.lblStatus.Text
        If APROBE <> "OFF" Then
            gFrmMain.lblStatus.Text = "APPLY PROBE"
        End If

        'convert data type to string type
        sInputImp = CStr(nInputImp)
        sTimeRange = CStr(dTimeRange)
        sVoltRange = CStr(dVoltRange)
        sTriggerLevel = CStr(dTriggerLevel)

        'convert string data to upper case
        sAutoScale = UCase(sAutoScale)
        sINP = UCase(sINP)
        sInputCoupling = UCase(sInputCoupling)
        sLPAS = UCase(sLPAS)
        sHPAS = UCase(sHPAS)
        sHYST = UCase(sHYST)
        sTriggerSlope = UCase(sTriggerSlope)
        sTriggerSource = UCase(sTriggerSource)

        'display measurement info to TPS shell
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        gFrmMain.txtInstrument.Text = "OSCOPE"
        gFrmMain.txtCommand.Text = "dMeasFallPre"

        'Setup OSCOPE for FALL:OVERshoot Measurement
        If Not bSimulation Then
            WriteMsg(OSCOPE, "SYST:LANG SCPI")

            LongDelay((1))
            WriteMsg(OSCOPE, "*CLS")
            WriteMsg(OSCOPE, "*RST")

            'select auto scale
            Select Case sAutoScale
                Case "ON", "AUTO"
                    WriteMsg(OSCOPE, "SYST:AUT")
                    LongDelay((2))

                Case "OFF" 'manually set vertical and horizontal ranges
                    'set input channel: INP1 or INP2
                    If (sINP = "INP1" Or sINP = "INP2") Then
                        WriteMsg(OSCOPE, sINP & " ON")
                    Else
                        Echo("OSCOPE INPUT CHANNEL SELECTION ERROR")
                        dMeasFallPre = 0
                        Exit Function
                    End If

                    'set input coupling: DC or AC
                    If (sInputCoupling = "DC" Or sInputCoupling = "AC") Then
                        WriteMsg(OSCOPE, sINP & ":COUP " & sInputCoupling)
                    Else
                        Echo("OSCOPE INPUT COUPLING PARAMETER ERROR")
                        dMeasFallPre = 0
                        Exit Function
                    End If

                    'set input impedance: 1000000 or 50
                    If (nInputImp = 50 Or nInputImp = 1000000) Then
                        WriteMsg(OSCOPE, sINP & ":IMP " & sInputImp)
                    Else
                        Echo("OSCOPE INPUT IMPEDANCE PARAMETER ERROR")
                        dMeasFallPre = 0
                        Exit Function
                    End If

                    'set input low-pass filter state: OFF or ON
                    If (sLPAS = "ON" Or sLPAS = "OFF") Then
                        WriteMsg(OSCOPE, sINP & ":FILT:LPAS " & sLPAS)
                    Else
                        Echo("OSCOPE INPUT FILTER STATE PARAMETER ERROR")
                        dMeasFallPre = 0
                        Exit Function
                    End If

                    'set input high-pass filter state: OFF or ON
                    If (sHPAS = "ON" Or sHPAS = "OFF") Then
                        WriteMsg(OSCOPE, sINP & ":FILT:hPAS " & sHPAS)
                    Else
                        Echo("OSCOPE INPUT FILTER STATE PARAMETER ERROR")
                        dMeasFallPre = 0
                        Exit Function
                    End If

                    'set volt range: 8mV to 40V
                    If (dVoltRange < 0.008 Or dVoltRange > 40) Then
                        Echo("OSCOPE VOLT RANGE ERROR")
                        dMeasFallPre = 0
                        Exit Function
                    Else
                        Select Case sINP
                            Case "INP1"
                                WriteMsg(OSCOPE, "VOLT1:RANG " & sVoltRange)
                            Case "INP2"
                                WriteMsg(OSCOPE, "VOLT2:RANG " & sVoltRange)
                        End Select
                    End If

                    'set time range:10ns to 50s
                    If (dTimeRange < 0.00000001 Or dTimeRange > 50) Then
                        Echo("OSCOPE TIME RANGE ERROR")
                        dMeasFallPre = 0
                        Exit Function
                    Else
                        WriteMsg(OSCOPE, "SWE:TIME:RANG " & sTimeRange)
                    End If

                    'set trigger level
                    WriteMsg(OSCOPE, "TRIG:LEV " & sTriggerLevel)

                    'set niose rejection: OFF or ON
                    If (sHYST = "ON" Or sHYST = "OFF") Then
                        WriteMsg(OSCOPE, "TRIG:HYST " & sHYST)
                    Else
                        Echo("OSCOPE TRIGGER INPUT PARAMETER ERROR")
                        dMeasFallPre = 0
                        Exit Function
                    End If

                    'set trigger slope: POS or NEG
                    If (sTriggerSlope = "POS" Or sTriggerSlope = "NEG") Then
                        WriteMsg(OSCOPE, "TRIG:SLOP " & sTriggerSlope)
                    Else
                        Echo("OSCOPE TRIGGER SLOPE PARAMETER ERROR")
                        dMeasFallPre = 0
                        Exit Function
                    End If

                    'set trigger source
                    Select Case sTriggerSource
                        Case "INP1", "INP2", "EXT", "ECLT0", "ECLT1"
                            WriteMsg(OSCOPE, "TRIG:SOUR " & sTriggerSource)

                        Case Else
                            Echo("OSCOPE TRIGGER SOURCE PARAMETER ERROR")
                            dMeasFallPre = 0
                            Exit Function
                    End Select

                Case Else
                    Echo("OSCOPE AUTO SCALE PARAMETER ERROR")
                    dMeasFallPre = 0
                    Exit Function
            End Select
        End If

        'Make Mesurement
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        If Not bSimulation Then
            Select Case Left(UCase(APROBE), 1)
                Case "O" ' Off
                    WriteMsg(OSCOPE, "MEAS:VOLT:FALL:PRES? (@" & sINP & ")")
                    LongDelay((1))
                    nErr = ReadMsg(OSCOPE, sData)
                    WriteMsg(OSCOPE, "*CLS")
                    If nErr <> 0 Then
                        Echo("VISA ERROR: " & nErr)
                        dMeasFallPre = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(nErr)
                        dMeasFallPre = 0
                        Exit Function
                    End If
                    dMeasFallPre = CDbl(sData)
                    gFrmMain.txtMeasured.Text = CStr(dMeasFallPre)
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
                        '5/14/03 by Soon Nam''''''''''''''''''''''''''''''
                        'set auto scale with analog probe measurements
                        If (sAutoScale = "ON" Or sAutoScale = "AUTO") Then
                            WriteMsg(OSCOPE, "SYST:AUT")
                            LongDelay((1))
                        End If
                        'end add''''''''''''''''''''''''''''''''''''''''''
                        gFrmMain.lblStatus.Text = "Making Measurement ..."
                        WriteMsg(OSCOPE, "MEAS:VOLT:FALL:PRES? (@" & sINP & ")")
                        LongDelay((1))
                        nErr = ReadMsg(OSCOPE, sData)
                        WriteMsg(OSCOPE, "*CLS")
                        If nErr <> 0 Then
                            Echo("VISA ERROR: " & nErr)
                            dMeasFallPre = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(nErr)
                            dMeasFallPre = 0
                            Exit Function
                        End If
                        dMeasFallPre = CDbl(sData)
                        gFrmMain.txtMeasured.Text = CStr(dMeasFallPre)
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

                Case "C" ' Continuous
                    MisProbe = MsgBoxResult.Yes
                    Do While MisProbe = MsgBoxResult.Yes
                        UserEvent = 0
                        Failed = False
                        gFrmMain.TimerProbe.Enabled = True : gFrmMain.fraInstructions.Visible = True
                        Do While (UserEvent = 0) And (Not bProbeClosed)
                            '5/14/03 by Soon Nam''''''''''''''''''''''''''''''
                            'set auto scale with analog probe measurements
                            If (sAutoScale = "ON" Or sAutoScale = "AUTO") Then
                                WriteMsg(OSCOPE, "SYST:AUT")
                                LongDelay((1))
                            End If
                            'end add''''''''''''''''''''''''''''''''''''''''''
                            gFrmMain.lblStatus.Text = "Making Measurement ..."
                            WriteMsg(OSCOPE, "MEAS:VOLT:FALL:PRES? (@" & sINP & ")")
                            LongDelay((1))
                            nErr = ReadMsg(OSCOPE, sData)
                            WriteMsg(OSCOPE, "*CLS")
                            If nErr <> 0 Then
                                Echo("VISA ERROR: " & nErr)
                                dMeasFallPre = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(nErr)
                                dMeasFallPre = 0
                                Exit Function
                            End If
                            dMeasFallPre = CDbl(sData)
                            gFrmMain.txtMeasured.Text = CStr(dMeasFallPre)
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
            dMeasFallPre = CDbl(InputBox("Command cmdOSCOPE.dMeasFallPre peformed." & vbCrLf & "Enter Value:", "SIMULATION MODE"))
            gFrmMain.txtMeasured.Text = CStr(dMeasFallPre)
            If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON)
        End If
        gFrmMain.lblStatus.Text = sCurrentMsg
    End Function
	
    ''' <summary>
    ''' perform an preshoot measurement on rising edge of signal: ((LOW-MIN)/AMPL)*100
    ''' </summary>
    ''' <param name="sAutoScale">"ON" or "OFF"</param>
    ''' <param name="sINP">select INP1 or INP2 (OSCOPE input channel)</param>
    ''' <param name="dVoltRange">set the full scale vertical range;  min = 8mV, max = 40V, at reset = 4V</param>
    ''' <param name="dTimeRange">set the full scale horizontal range for sweep;  min = 10 ns, max = 50 s, at reset = 1 ms</param>
    ''' <param name="dTriggerLevel">set trigger level voltage (0V at reset)</param>
    ''' <param name="sTriggerSlope">set trigger slope ("POS" at reset)</param>
    ''' <param name="sTriggerSource">set trigger source ("INP1" at reset)</param>
    ''' <param name="nInputImp">select input impedance 50 or 1M (1M at reset)</param>
    ''' <param name="sInputCoupling">select input coupling for input channels ("DC" at reset)</param>
    ''' <param name="sLPAS">select low-pass filter ("OFF" at reset)</param>
    ''' <param name="sHPAS">select high-pass filter ("OFF" at reset)</param>
    ''' <param name="sHYST">set noise rejection ("OFF" at reset)</param>
    ''' <param name="APROBE">Indicates AProbe usage; "OFF", "Single" Measurement, or "Contiuous" Measurement</param>
    ''' <returns>measured rising edge preshoot</returns>
    ''' <remarks>sAutoScale: 
    '''             auto scale can be used to determine vertical and horizontal parameters when sAutoScale = "ON", optional parameters are ignored.  
    ''' Autoscale should only be used with relatively stable input signals having a duty cycle of greater than 0.5% and a frequency greater than 50Hz.
    ''' sINP:
    '''        At reset, INP1 is on and INP2 if off.  Max input voltage that can be applied to the two input connectors is 5 Vrms at 50 ohms 
    ''' or +/-250V at 1Mohms.</remarks>
    Public Function dMeasRisePre(ByRef sAutoScale As String, ByRef sINP As String, Optional ByRef dVoltRange As Double = 4,
                                 Optional ByRef dTimeRange As Double = 0.001, Optional ByRef dTriggerLevel As Double = 0,
                                 Optional ByRef sTriggerSlope As String = "POS", Optional ByRef sTriggerSource As String = "INP1",
                                 Optional ByRef nInputImp As Integer = 1000000, Optional ByRef sInputCoupling As String = "DC",
                                 Optional ByRef sLPAS As String = "OFF", Optional ByRef sHPAS As String = "OFF",
                                 Optional ByRef sHYST As String = "OFF", Optional ByRef APROBE As String = "OFF") As Double
        Dim sData As String = ""
        Dim nErr As Integer
        Dim sTimeRange As String
        Dim sVoltRange As String
        Dim sTriggerLevel As String
        Dim sInputImp As String
        Dim sCurrentMsg As String

        dMeasRisePre = 0.0

        sCurrentMsg = gFrmMain.lblStatus.Text
        If APROBE <> "OFF" Then
            gFrmMain.lblStatus.Text = "APPLY PROBE"
        End If

        'convert data type to string type
        sInputImp = CStr(nInputImp)
        sTimeRange = CStr(dTimeRange)
        sVoltRange = CStr(dVoltRange)
        sTriggerLevel = CStr(dTriggerLevel)

        'convert string data to upper case
        sAutoScale = UCase(sAutoScale)
        sINP = UCase(sINP)
        sInputCoupling = UCase(sInputCoupling)
        sLPAS = UCase(sLPAS)
        sHPAS = UCase(sHPAS)
        sHYST = UCase(sHYST)
        sTriggerSlope = UCase(sTriggerSlope)
        sTriggerSource = UCase(sTriggerSource)

        'display measurement info to TPS shell
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        gFrmMain.txtInstrument.Text = "OSCOPE"
        gFrmMain.txtCommand.Text = "dMeasRisePre"

        'Setup OSCOPE for FALL:OVERshoot Measurement
        If Not bSimulation Then
            WriteMsg(OSCOPE, "SYST:LANG SCPI")

            LongDelay((1))
            WriteMsg(OSCOPE, "*CLS")
            WriteMsg(OSCOPE, "*RST")

            'select auto scale
            Select Case sAutoScale
                Case "ON", "AUTO"
                    WriteMsg(OSCOPE, "SYST:AUT")
                    LongDelay((2))

                Case "OFF" 'manually set vertical and horizontal ranges
                    'set input channel: INP1 or INP2
                    If (sINP = "INP1" Or sINP = "INP2") Then
                        WriteMsg(OSCOPE, sINP & " ON")
                    Else
                        Echo("OSCOPE INPUT CHANNEL SELECTION ERROR")
                        dMeasRisePre = 0
                        Exit Function
                    End If

                    'set input coupling: DC or AC
                    If (sInputCoupling = "DC" Or sInputCoupling = "AC") Then
                        WriteMsg(OSCOPE, sINP & ":COUP " & sInputCoupling)
                    Else
                        Echo("OSCOPE INPUT COUPLING PARAMETER ERROR")
                        dMeasRisePre = 0
                        Exit Function
                    End If

                    'set input impedance: 1000000 or 50
                    If (nInputImp = 50 Or nInputImp = 1000000) Then
                        WriteMsg(OSCOPE, sINP & ":IMP " & sInputImp)
                    Else
                        Echo("OSCOPE INPUT IMPEDANCE PARAMETER ERROR")
                        dMeasRisePre = 0
                        Exit Function
                    End If

                    'set input low-pass filter state: OFF or ON
                    If (sLPAS = "ON" Or sLPAS = "OFF") Then
                        WriteMsg(OSCOPE, sINP & ":FILT:LPAS " & sLPAS)
                    Else
                        Echo("OSCOPE INPUT FILTER STATE PARAMETER ERROR")
                        dMeasRisePre = 0
                        Exit Function
                    End If

                    'set input high-pass filter state: OFF or ON
                    If (sHPAS = "ON" Or sHPAS = "OFF") Then
                        WriteMsg(OSCOPE, sINP & ":FILT:hPAS " & sHPAS)
                    Else
                        Echo("OSCOPE INPUT FILTER STATE PARAMETER ERROR")
                        dMeasRisePre = 0
                        Exit Function
                    End If

                    'set volt range: 8mV to 40V
                    If (dVoltRange < 0.008 Or dVoltRange > 40) Then
                        Echo("OSCOPE VOLT RANGE ERROR")
                        dMeasRisePre = 0
                        Exit Function
                    Else
                        Select Case sINP
                            Case "INP1"
                                WriteMsg(OSCOPE, "VOLT1:RANG " & sVoltRange)
                            Case "INP2"
                                WriteMsg(OSCOPE, "VOLT2:RANG " & sVoltRange)
                        End Select
                    End If

                    'set time range:10ns to 50s
                    If (dTimeRange < 0.00000001 Or dTimeRange > 50) Then
                        Echo("OSCOPE TIME RANGE ERROR")
                        dMeasRisePre = 0
                        Exit Function
                    Else
                        WriteMsg(OSCOPE, "SWE:TIME:RANG " & sTimeRange)
                    End If

                    'set trigger level
                    WriteMsg(OSCOPE, "TRIG:LEV " & sTriggerLevel)

                    'set niose rejection: OFF or ON
                    If (sHYST = "ON" Or sHYST = "OFF") Then
                        WriteMsg(OSCOPE, "TRIG:HYST " & sHYST)
                    Else
                        Echo("OSCOPE TRIGGER INPUT PARAMETER ERROR")
                        dMeasRisePre = 0
                        Exit Function
                    End If

                    'set trigger slope: POS or NEG
                    If (sTriggerSlope = "POS" Or sTriggerSlope = "NEG") Then
                        WriteMsg(OSCOPE, "TRIG:SLOP " & sTriggerSlope)
                    Else
                        Echo("OSCOPE TRIGGER SLOPE PARAMETER ERROR")
                        dMeasRisePre = 0
                        Exit Function
                    End If

                    'set trigger source
                    Select Case sTriggerSource
                        Case "INP1", "INP2", "EXT", "ECLT0", "ECLT1"
                            WriteMsg(OSCOPE, "TRIG:SOUR " & sTriggerSource)

                        Case Else
                            Echo("OSCOPE TRIGGER SOURCE PARAMETER ERROR")
                            dMeasRisePre = 0
                            Exit Function
                    End Select

                Case Else
                    Echo("OSCOPE AUTO SCALE PARAMETER ERROR")
                    dMeasRisePre = 0
                    Exit Function
            End Select
        End If

        'Make Mesurement
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        If Not bSimulation Then
            Select Case Left(UCase(APROBE), 1)
                Case "O" ' Off
                    WriteMsg(OSCOPE, "MEAS:VOLT:RISE:PRES? (@" & sINP & ")")
                    LongDelay((1))
                    nErr = ReadMsg(OSCOPE, sData)
                    WriteMsg(OSCOPE, "*CLS")
                    If nErr <> 0 Then
                        Echo("VISA ERROR: " & nErr)
                        dMeasRisePre = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(nErr)
                        dMeasRisePre = 0
                        Exit Function
                    End If
                    dMeasRisePre = CDbl(sData)
                    gFrmMain.txtMeasured.Text = CStr(dMeasRisePre)
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
                        '5/14/03 by Soon Nam''''''''''''''''''''''''''''''
                        'set auto scale with analog probe measurements
                        If (sAutoScale = "ON" Or sAutoScale = "AUTO") Then
                            WriteMsg(OSCOPE, "SYST:AUT")
                            LongDelay((1))
                        End If
                        'end add''''''''''''''''''''''''''''''''''''''''''
                        gFrmMain.lblStatus.Text = "Making Measurement ..."
                        WriteMsg(OSCOPE, "MEAS:VOLT:RISE:PRES? (@" & sINP & ")")
                        LongDelay((1))
                        nErr = ReadMsg(OSCOPE, sData)
                        WriteMsg(OSCOPE, "*CLS")
                        If nErr <> 0 Then
                            Echo("VISA ERROR: " & nErr)
                            dMeasRisePre = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(nErr)
                            dMeasRisePre = 0
                            Exit Function
                        End If
                        dMeasRisePre = CDbl(sData)
                        gFrmMain.txtMeasured.Text = CStr(dMeasRisePre)
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

                Case "C" ' Continuous
                    MisProbe = MsgBoxResult.Yes
                    Do While MisProbe = MsgBoxResult.Yes
                        UserEvent = 0
                        Failed = False
                        gFrmMain.TimerProbe.Enabled = True : gFrmMain.fraInstructions.Visible = True
                        Do While (UserEvent = 0) And (Not bProbeClosed)
                            '5/14/03 by Soon Nam''''''''''''''''''''''''''''''
                            'set auto scale with analog probe measurements
                            If (sAutoScale = "ON" Or sAutoScale = "AUTO") Then
                                WriteMsg(OSCOPE, "SYST:AUT")
                                LongDelay((1))
                            End If
                            'end add''''''''''''''''''''''''''''''''''''''''''
                            gFrmMain.lblStatus.Text = "Making Measurement ..."
                            WriteMsg(OSCOPE, "MEAS:VOLT:RISE:PRES? (@" & sINP & ")")
                            LongDelay((1))
                            nErr = ReadMsg(OSCOPE, sData)
                            WriteMsg(OSCOPE, "*CLS")
                            If nErr <> 0 Then
                                Echo("VISA ERROR: " & nErr)
                                dMeasRisePre = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(nErr)
                                dMeasRisePre = 0
                                Exit Function
                            End If
                            dMeasRisePre = CDbl(sData)
                            gFrmMain.txtMeasured.Text = CStr(dMeasRisePre)
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
            dMeasRisePre = CDbl(InputBox("Command cmdOSCOPE.dMeasRisePre peformed." & vbCrLf & "Enter Value:", "SIMULATION MODE"))
            gFrmMain.txtMeasured.Text = CStr(dMeasRisePre)
            If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON)
        End If

        gFrmMain.lblStatus.Text = sCurrentMsg
    End Function

    ''' <summary>
    ''' perform fall time measurement: default at 10%/90% threshold
    ''' </summary>
    ''' <param name="sAutoScale">"ON" or "OFF"</param>
    ''' <param name="sINP">select INP1 or INP2 (OSCOPE input channel)</param>
    ''' <param name="dVoltRange">set the full scale vertical range;  min = 8mV, max = 40V, at reset = 4V</param>
    ''' <param name="dTimeRange">set the full scale horizontal range for sweep;  min = 10 ns, max = 50 s, at reset = 1 ms</param>
    ''' <param name="dTriggerLevel">set trigger level voltage (0V at reset)</param>
    ''' <param name="sTriggerSlope">set trigger slope ("POS" at reset)</param>
    ''' <param name="sTriggerSource">set trigger source ("INP1" at reset)</param>
    ''' <param name="sLowerLimit">can be entered either in percent or voltage values (default = 10%)</param>
    ''' <param name="sUpperLimit">can be entered either in percent or voltage values (default = 90%)</param>
    ''' <param name="nInputImp">select input impedance 50 or 1M (1M at reset)</param>
    ''' <param name="sInputCoupling">select input coupling for input channels ("DC" at reset)</param>
    ''' <param name="sLPAS">select low-pass filter ("OFF" at reset)</param>
    ''' <param name="sHPAS">select high-pass filter ("OFF" at reset)</param>
    ''' <param name="sHYST">set noise rejection ("OFF" at reset)</param>
    ''' <param name="APROBE">Indicates AProbe usage; "OFF", "Single" Measurement, or "Contiuous" Measurement</param>
    ''' <returns>Measured fall time</returns>
    ''' <remarks>sAutoScale: 
    '''             auto scale can be used to determine vertical and horizontal parameters when sAutoScale = "ON", optional parameters are ignored.  
    ''' Autoscale should only be used with relatively stable input signals having a duty cycle of greater than 0.5% and a frequency greater than 50Hz.
    ''' sINP:
    '''        At reset, INP1 is on and INP2 if off.  Max input voltage that can be applied to the two input connectors is 5 Vrms at 50 ohms 
    ''' or +/-250V at 1Mohms.</remarks>
    Public Function dMeasFallTime(ByRef sAutoScale As String, ByRef sINP As String, Optional ByRef dVoltRange As Double = 4,
                                  Optional ByRef dTimeRange As Double = 0.001, Optional ByRef dTriggerLevel As Double = 0,
                                  Optional ByRef sTriggerSlope As String = "POS", Optional ByRef sTriggerSource As String = "INP1",
                                  Optional ByRef sLowerLimit As String = "10%", Optional ByRef sUpperLimit As String = "90%",
                                  Optional ByRef nInputImp As Integer = 1000000.0, Optional ByRef sInputCoupling As String = "DC",
                                  Optional ByRef sLPAS As String = "OFF", Optional ByRef sHPAS As String = "OFF",
                                  Optional ByRef sHYST As String = "OFF", Optional ByRef APROBE As String = "OFF") As Double
        Dim sData As String = ""
        Dim nErr As Integer
        Dim sTimeRange As String
        Dim sVoltRange As String
        Dim sTriggerLevel As String
        Dim sInputImp As String
        Dim sCurrentMsg As String

        dMeasFallTime = 0.0

        sCurrentMsg = gFrmMain.lblStatus.Text
        If APROBE <> "OFF" Then
            gFrmMain.lblStatus.Text = "APPLY PROBE"
        End If

        'convert data type to string type
        sInputImp = CStr(nInputImp)
        sTimeRange = CStr(dTimeRange)
        sVoltRange = CStr(dVoltRange)
        sTriggerLevel = CStr(dTriggerLevel)

        'convert string data to upper case
        sAutoScale = UCase(sAutoScale)
        sINP = UCase(sINP)
        sInputCoupling = UCase(sInputCoupling)
        sLPAS = UCase(sLPAS)
        sHPAS = UCase(sHPAS)
        sHYST = UCase(sHYST)
        sTriggerSlope = UCase(sTriggerSlope)
        sTriggerSource = UCase(sTriggerSource)

        'display measurement info to TPS shell
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        gFrmMain.txtInstrument.Text = "OSCOPE"
        gFrmMain.txtCommand.Text = "dMeasFallTime"

        'Setup OSCOPE for FALL:OVERshoot Measurement
        If Not bSimulation Then
            WriteMsg(OSCOPE, "SYST:LANG SCPI")

            LongDelay((1))
            WriteMsg(OSCOPE, "*CLS")
            WriteMsg(OSCOPE, "*RST")

            'select auto scale
            Select Case sAutoScale
                Case "ON", "AUTO"
                    WriteMsg(OSCOPE, "SYST:AUT")
                    LongDelay((2))

                Case "OFF" 'manually set vertical and horizontal ranges
                    'set input channel: INP1 or INP2
                    If (sINP = "INP1" Or sINP = "INP2") Then
                        WriteMsg(OSCOPE, sINP & " ON")
                    Else
                        Echo("OSCOPE INPUT CHANNEL SELECTION ERROR")
                        dMeasFallTime = 0
                        Exit Function
                    End If

                    'set input coupling: DC or AC
                    If (sInputCoupling = "DC" Or sInputCoupling = "AC") Then
                        WriteMsg(OSCOPE, sINP & ":COUP " & sInputCoupling)
                    Else
                        Echo("OSCOPE INPUT COUPLING PARAMETER ERROR")
                        dMeasFallTime = 0
                        Exit Function
                    End If

                    'set input impedance: 1000000 or 50
                    If (nInputImp = 50 Or nInputImp = 1000000) Then
                        WriteMsg(OSCOPE, sINP & ":IMP " & sInputImp)
                    Else
                        Echo("OSCOPE INPUT IMPEDANCE PARAMETER ERROR")
                        dMeasFallTime = 0
                        Exit Function
                    End If

                    'set input low-pass filter state: OFF or ON
                    If (sLPAS = "ON" Or sLPAS = "OFF") Then
                        WriteMsg(OSCOPE, sINP & ":FILT:LPAS " & sLPAS)
                    Else
                        Echo("OSCOPE INPUT FILTER STATE PARAMETER ERROR")
                        dMeasFallTime = 0
                        Exit Function
                    End If

                    'set input high-pass filter state: OFF or ON
                    If (sHPAS = "ON" Or sHPAS = "OFF") Then
                        WriteMsg(OSCOPE, sINP & ":FILT:hPAS " & sHPAS)
                    Else
                        Echo("OSCOPE INPUT FILTER STATE PARAMETER ERROR")
                        dMeasFallTime = 0
                        Exit Function
                    End If

                    'set volt range: 8mV to 40V
                    If (dVoltRange < 0.008 Or dVoltRange > 40) Then
                        Echo("OSCOPE VOLT RANGE ERROR")
                        dMeasFallTime = 0
                        Exit Function
                    Else
                        Select Case sINP
                            Case "INP1"
                                WriteMsg(OSCOPE, "VOLT1:RANG " & sVoltRange)
                            Case "INP2"
                                WriteMsg(OSCOPE, "VOLT2:RANG " & sVoltRange)
                        End Select
                    End If

                    'set time range:10ns to 50s
                    If (dTimeRange < 0.00000001 Or dTimeRange > 50) Then
                        Echo("OSCOPE TIME RANGE ERROR")
                        dMeasFallTime = 0
                        Exit Function
                    Else
                        WriteMsg(OSCOPE, "SWE:TIME:RANG " & sTimeRange)
                    End If

                    'set trigger level
                    WriteMsg(OSCOPE, "TRIG:LEV " & sTriggerLevel)

                    'set niose rejection: OFF or ON
                    If (sHYST = "ON" Or sHYST = "OFF") Then
                        WriteMsg(OSCOPE, "TRIG:HYST " & sHYST)
                    Else
                        Echo("OSCOPE TRIGGER INPUT PARAMETER ERROR")
                        dMeasFallTime = 0
                        Exit Function
                    End If

                    'set trigger slope: POS or NEG
                    If (sTriggerSlope = "POS" Or sTriggerSlope = "NEG") Then
                        WriteMsg(OSCOPE, "TRIG:SLOP " & sTriggerSlope)
                    Else
                        Echo("OSCOPE TRIGGER SLOPE PARAMETER ERROR")
                        dMeasFallTime = 0
                        Exit Function
                    End If

                    'set trigger source
                    Select Case sTriggerSource
                        Case "INP1", "INP2", "EXT", "ECLT0", "ECLT1"
                            WriteMsg(OSCOPE, "TRIG:SOUR " & sTriggerSource)

                        Case Else
                            Echo("OSCOPE TRIGGER SOURCE PARAMETER ERROR")
                            dMeasFallTime = 0
                            Exit Function
                    End Select

                Case Else
                    Echo("OSCOPE AUTO SCALE PARAMETER ERROR")
                    dMeasFallTime = 0
                    Exit Function
            End Select
        End If

        'Make Mesurement
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        If Not bSimulation Then
            Select Case Left(UCase(APROBE), 1)
                Case "O" ' Off
                    'WriteMsg OSCOPE, "INIT:CONT AUTO"
                    If (sLowerLimit = "10%" And sUpperLimit = "90%") Then
                        WriteMsg(OSCOPE, "MEAS:VOLT:FALL:TIME? (@" & sINP & ")")
                    ElseIf Not (sLowerLimit = "10%") Then
                        WriteMsg(OSCOPE, "MEAS:VOLT:FALL:TIME? " & sLowerLimit & ", " & " 90%, (@" & sINP & ")")
                    Else
                        WriteMsg(OSCOPE, "MEAS:VOLT:FALL:TIME? " & sLowerLimit & ", " & sUpperLimit & ", (@" & sINP & ")")
                    End If
                    LongDelay((1))
                    nErr = ReadMsg(OSCOPE, sData)
                    WriteMsg(OSCOPE, "*CLS")
                    If nErr <> 0 Then
                        Echo("VISA ERROR: " & nErr)
                        dMeasFallTime = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(nErr)
                        dMeasFallTime = 0
                        Exit Function
                    End If
                    dMeasFallTime = CDbl(sData)
                    gFrmMain.txtMeasured.Text = CStr(dMeasFallTime)
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
                        '5/14/03 by Soon Nam''''''''''''''''''''''''''''''
                        'set auto scale with analog probe measurements
                        If (sAutoScale = "ON" Or sAutoScale = "AUTO") Then
                            WriteMsg(OSCOPE, "SYST:AUT")
                            LongDelay((1))
                        End If
                        'end add''''''''''''''''''''''''''''''''''''''''''
                        gFrmMain.lblStatus.Text = "Making Measurement ..."
                        If (sLowerLimit = "10%" And sUpperLimit = "90%") Then
                            WriteMsg(OSCOPE, "MEAS:VOLT:FALL:TIME? (@" & sINP & ")")
                        ElseIf Not (sLowerLimit = "10%") Then
                            WriteMsg(OSCOPE, "MEAS:VOLT:FALL:TIME? " & sLowerLimit & ", " & " 90%, (@" & sINP & ")")
                        Else
                            WriteMsg(OSCOPE, "MEAS:VOLT:FALL:TIME? " & sLowerLimit & ", " & sUpperLimit & ", (@" & sINP & ")")
                        End If
                        LongDelay((1))
                        nErr = ReadMsg(OSCOPE, sData)
                        WriteMsg(OSCOPE, "*CLS")
                        If nErr <> 0 Then
                            Echo("VISA ERROR: " & nErr)
                            dMeasFallTime = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(nErr)
                            dMeasFallTime = 0
                            Exit Function
                        End If
                        dMeasFallTime = CDbl(sData)
                        gFrmMain.txtMeasured.Text = CStr(dMeasFallTime)
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

                Case "C" ' Continuous
                    MisProbe = MsgBoxResult.Yes
                    Do While MisProbe = MsgBoxResult.Yes
                        UserEvent = 0
                        Failed = False
                        gFrmMain.TimerProbe.Enabled = True : gFrmMain.fraInstructions.Visible = True
                        Do While (UserEvent = 0) And (Not bProbeClosed)
                            '5/14/03 by Soon Nam''''''''''''''''''''''''''''''
                            'set auto scale with analog probe measurements
                            If (sAutoScale = "ON" Or sAutoScale = "AUTO") Then
                                WriteMsg(OSCOPE, "SYST:AUT")
                                LongDelay((1))
                            End If
                            'end add''''''''''''''''''''''''''''''''''''''''''
                            gFrmMain.lblStatus.Text = "Making Measurement ..."
                            If (sLowerLimit = "10%" And sUpperLimit = "90%") Then
                                WriteMsg(OSCOPE, "MEAS:VOLT:FALL:TIME? (@" & sINP & ")")
                            ElseIf Not (sLowerLimit = "10%") Then
                                WriteMsg(OSCOPE, "MEAS:VOLT:FALL:TIME? " & sLowerLimit & ", " & " 90%, (@" & sINP & ")")
                            Else
                                WriteMsg(OSCOPE, "MEAS:VOLT:FALL:TIME? " & sLowerLimit & ", " & sUpperLimit & ", (@" & sINP & ")")
                            End If
                            LongDelay((1))
                            nErr = ReadMsg(OSCOPE, sData)
                            WriteMsg(OSCOPE, "*CLS")
                            If nErr <> 0 Then
                                Echo("VISA ERROR: " & nErr)
                                dMeasFallTime = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(nErr)
                                dMeasFallTime = 0
                                Exit Function
                            End If
                            dMeasFallTime = CDbl(sData)
                            gFrmMain.txtMeasured.Text = CStr(dMeasFallTime)
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
            dMeasFallTime = CDbl(InputBox("Command cmdOSCOPE.dMeasFallTime peformed." & vbCrLf & "Enter Fall Time Value:", "SIMULATION MODE"))
            gFrmMain.txtMeasured.Text = CStr(dMeasFallTime)
            If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON)
        End If

        gFrmMain.lblStatus.Text = sCurrentMsg
    End Function
	
    ''' <summary>
    ''' perform rise time measurement: default at 10%/90% threshold
    ''' </summary>
    ''' <param name="sAutoScale">"ON" or "OFF"</param>
    ''' <param name="sINP">select INP1 or INP2 (OSCOPE input channel)</param>
    ''' <param name="dVoltRange">set the full scale vertical range;  min = 8mV, max = 40V, at reset = 4V</param>
    ''' <param name="dTimeRange">set the full scale horizontal range for sweep;  min = 10 ns, max = 50 s, at reset = 1 ms</param>
    ''' <param name="dTriggerLevel">set trigger level voltage (0V at reset)</param>
    ''' <param name="sTriggerSlope">set trigger slope ("POS" at reset)</param>
    ''' <param name="sTriggerSource">set trigger source ("INP1" at reset)</param>
    ''' <param name="sLowerLimit">can be entered either in percent or voltage values (default = 10%)</param>
    ''' <param name="sUpperLimit">can be entered either in percent or voltage values (default = 90%)</param>
    ''' <param name="nInputImp">select input impedance 50 or 1M (1M at reset)</param>
    ''' <param name="sInputCoupling">select input coupling for input channels ("DC" at reset)</param>
    ''' <param name="sLPAS">select low-pass filter ("OFF" at reset)</param>
    ''' <param name="sHPAS">select high-pass filter ("OFF" at reset)</param>
    ''' <param name="sHYST">set noise rejection ("OFF" at reset)</param>
    ''' <param name="APROBE">Indicates AProbe usage; "OFF", "Single" Measurement, or "Contiuous" Measurement</param>
    ''' <returns>Measured rise time</returns>
    ''' <remarks>sAutoScale: 
    '''             auto scale can be used to determine vertical and horizontal parameters when sAutoScale = "ON", optional parameters are ignored.  
    ''' Autoscale should only be used with relatively stable input signals having a duty cycle of greater than 0.5% and a frequency greater than 50Hz.
    ''' sINP:
    '''        At reset, INP1 is on and INP2 if off.  Max input voltage that can be applied to the two input connectors is 5 Vrms at 50 ohms 
    ''' or +/-250V at 1Mohms.</remarks>
    Public Function dMeasRiseTime(ByRef sAutoScale As String, ByRef sINP As String, Optional ByRef dVoltRange As Double = 4,
                                  Optional ByRef dTimeRange As Double = 0.001, Optional ByRef dTriggerLevel As Double = 0,
                                  Optional ByRef sTriggerSlope As String = "POS", Optional ByRef sTriggerSource As String = "INP1",
                                  Optional ByRef sLowerLimit As String = "10%", Optional ByRef sUpperLimit As String = "90%",
                                  Optional ByRef nInputImp As Integer = 1000000, Optional ByRef sInputCoupling As String = "DC",
                                  Optional ByRef sLPAS As String = "OFF", Optional ByRef sHPAS As String = "OFF",
                                  Optional ByRef sHYST As String = "OFF", Optional ByRef APROBE As String = "OFF") As Double
        Dim sData As String = ""
        Dim nErr As Integer
        Dim sTimeRange As String
        Dim sVoltRange As String
        Dim sTriggerLevel As String
        Dim sInputImp As String
        Dim sCurrentMsg As String

        dMeasRiseTime = 0.0

        sCurrentMsg = gFrmMain.lblStatus.Text
        If APROBE <> "OFF" Then
            gFrmMain.lblStatus.Text = "APPLY PROBE"
        End If

        'convert data type to string type
        sInputImp = CStr(nInputImp)
        sTimeRange = CStr(dTimeRange)
        sVoltRange = CStr(dVoltRange)
        sTriggerLevel = CStr(dTriggerLevel)

        'convert string data to upper case
        sAutoScale = UCase(sAutoScale)
        sINP = UCase(sINP)
        sInputCoupling = UCase(sInputCoupling)
        sLPAS = UCase(sLPAS)
        sHPAS = UCase(sHPAS)
        sHYST = UCase(sHYST)
        sTriggerSlope = UCase(sTriggerSlope)
        sTriggerSource = UCase(sTriggerSource)

        'display measurement info to TPS shell
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        gFrmMain.txtInstrument.Text = "OSCOPE"
        gFrmMain.txtCommand.Text = "dMeasRiseTime"

        'Setup OSCOPE for FALL:OVERshoot Measurement
        If Not bSimulation Then
            WriteMsg(OSCOPE, "SYST:LANG SCPI")

            LongDelay((1))
            WriteMsg(OSCOPE, "*CLS")
            WriteMsg(OSCOPE, "*RST")

            'select auto scale
            Select Case sAutoScale
                Case "ON", "AUTO"
                    WriteMsg(OSCOPE, "SYST:AUT")
                    LongDelay((2))

                Case "OFF" 'manually set vertical and horizontal ranges
                    'set input channel: INP1 or INP2
                    If (sINP = "INP1" Or sINP = "INP2") Then
                        WriteMsg(OSCOPE, sINP & " ON")
                    Else
                        Echo("OSCOPE INPUT CHANNEL SELECTION ERROR")
                        dMeasRiseTime = 0
                        Exit Function
                    End If

                    'set input coupling: DC or AC
                    If (sInputCoupling = "DC" Or sInputCoupling = "AC") Then
                        WriteMsg(OSCOPE, sINP & ":COUP " & sInputCoupling)
                    Else
                        Echo("OSCOPE INPUT COUPLING PARAMETER ERROR")
                        dMeasRiseTime = 0
                        Exit Function
                    End If

                    'set input impedance: 1000000 or 50
                    If (nInputImp = 50 Or nInputImp = 1000000) Then
                        WriteMsg(OSCOPE, sINP & ":IMP " & sInputImp)
                    Else
                        Echo("OSCOPE INPUT IMPEDANCE PARAMETER ERROR")
                        dMeasRiseTime = 0
                        Exit Function
                    End If

                    'set input low-pass filter state: OFF or ON
                    If (sLPAS = "ON" Or sLPAS = "OFF") Then
                        WriteMsg(OSCOPE, sINP & ":FILT:LPAS " & sLPAS)
                    Else
                        Echo("OSCOPE INPUT FILTER STATE PARAMETER ERROR")
                        dMeasRiseTime = 0
                        Exit Function
                    End If

                    'set input high-pass filter state: OFF or ON
                    If (sHPAS = "ON" Or sHPAS = "OFF") Then
                        WriteMsg(OSCOPE, sINP & ":FILT:hPAS " & sHPAS)
                    Else
                        Echo("OSCOPE INPUT FILTER STATE PARAMETER ERROR")
                        dMeasRiseTime = 0
                        Exit Function
                    End If

                    'set volt range: 8mV to 40V
                    If (dVoltRange < 0.008 Or dVoltRange > 40) Then
                        Echo("OSCOPE VOLT RANGE ERROR")
                        dMeasRiseTime = 0
                        Exit Function
                    Else
                        Select Case sINP
                            Case "INP1"
                                WriteMsg(OSCOPE, "VOLT1:RANG " & sVoltRange)
                            Case "INP2"
                                WriteMsg(OSCOPE, "VOLT2:RANG " & sVoltRange)
                        End Select
                    End If

                    'set time range:10ns to 50s
                    If (dTimeRange < 0.00000001 Or dTimeRange > 50) Then
                        Echo("OSCOPE TIME RANGE ERROR")
                        dMeasRiseTime = 0
                        Exit Function
                    Else
                        WriteMsg(OSCOPE, "SWE:TIME:RANG " & sTimeRange)
                    End If

                    'set trigger level
                    WriteMsg(OSCOPE, "TRIG:LEV " & sTriggerLevel)

                    'set niose rejection: OFF or ON
                    If (sHYST = "ON" Or sHYST = "OFF") Then
                        WriteMsg(OSCOPE, "TRIG:HYST " & sHYST)
                    Else
                        Echo("OSCOPE TRIGGER INPUT PARAMETER ERROR")
                        dMeasRiseTime = 0
                        Exit Function
                    End If

                    'set trigger slope: POS or NEG
                    If (sTriggerSlope = "POS" Or sTriggerSlope = "NEG") Then
                        WriteMsg(OSCOPE, "TRIG:SLOP " & sTriggerSlope)
                    Else
                        Echo("OSCOPE TRIGGER SLOPE PARAMETER ERROR")
                        dMeasRiseTime = 0
                        Exit Function
                    End If

                    'set trigger source
                    Select Case sTriggerSource
                        Case "INP1", "INP2", "EXT", "ECLT0", "ECLT1"
                            WriteMsg(OSCOPE, "TRIG:SOUR " & sTriggerSource)

                        Case Else
                            Echo("OSCOPE TRIGGER SOURCE PARAMETER ERROR")
                            dMeasRiseTime = 0
                            Exit Function
                    End Select

                Case Else
                    Echo("OSCOPE AUTO SCALE PARAMETER ERROR")
                    dMeasRiseTime = 0
                    Exit Function
            End Select
        End If

        'Make Mesurement
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        If Not bSimulation Then
            Select Case Left(UCase(APROBE), 1)
                Case "O" ' Off
                    'WriteMsg OSCOPE, "INIT:CONT AUTO"
                    If (sLowerLimit = "10%" And sUpperLimit = "90%") Then
                        WriteMsg(OSCOPE, "MEAS:VOLT:RISE:TIME? (@" & sINP & ")")
                    ElseIf Not (sLowerLimit = "10%") Then
                        WriteMsg(OSCOPE, "MEAS:VOLT:RISE:TIME? " & sLowerLimit & ", " & " 90%, (@" & sINP & ")")
                    Else
                        WriteMsg(OSCOPE, "MEAS:VOLT:RISE:TIME? " & sLowerLimit & ", " & sUpperLimit & ", (@" & sINP & ")")
                    End If
                    LongDelay((1))
                    nErr = ReadMsg(OSCOPE, sData)
                    WriteMsg(OSCOPE, "*CLS")
                    If nErr <> 0 Then
                        Echo("VISA ERROR: " & nErr)
                        dMeasRiseTime = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(nErr)
                        dMeasRiseTime = 0
                        Exit Function
                    End If
                    dMeasRiseTime = CDbl(sData)
                    gFrmMain.txtMeasured.Text = CStr(dMeasRiseTime)
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
                        '5/14/03 by Soon Nam''''''''''''''''''''''''''''''
                        'set auto scale with analog probe measurements
                        If (sAutoScale = "ON" Or sAutoScale = "AUTO") Then
                            WriteMsg(OSCOPE, "SYST:AUT")
                            LongDelay((1))
                        End If
                        'end add''''''''''''''''''''''''''''''''''''''''''
                        gFrmMain.lblStatus.Text = "Making Measurement ..."
                        If (sLowerLimit = "10%" And sUpperLimit = "90%") Then
                            WriteMsg(OSCOPE, "MEAS:VOLT:RISE:TIME? (@" & sINP & ")")
                        ElseIf Not (sLowerLimit = "10%") Then
                            WriteMsg(OSCOPE, "MEAS:VOLT:RISE:TIME? " & sLowerLimit & ", " & " 90%, (@" & sINP & ")")
                        Else
                            WriteMsg(OSCOPE, "MEAS:VOLT:RISE:TIME? " & sLowerLimit & ", " & sUpperLimit & ", (@" & sINP & ")")
                        End If
                        LongDelay((1))
                        nErr = ReadMsg(OSCOPE, sData)
                        WriteMsg(OSCOPE, "*CLS")
                        If nErr <> 0 Then
                            Echo("VISA ERROR: " & nErr)
                            dMeasRiseTime = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(nErr)
                            dMeasRiseTime = 0
                            Exit Function
                        End If
                        dMeasRiseTime = CDbl(sData)
                        gFrmMain.txtMeasured.Text = CStr(dMeasRiseTime)
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

                Case "C" ' Continuous
                    MisProbe = MsgBoxResult.Yes
                    Do While MisProbe = MsgBoxResult.Yes
                        UserEvent = 0
                        Failed = False
                        gFrmMain.TimerProbe.Enabled = True : gFrmMain.fraInstructions.Visible = True
                        Do While (UserEvent = 0) And (Not bProbeClosed)
                            '5/14/03 by Soon Nam''''''''''''''''''''''''''''''
                            'set auto scale with analog probe measurements
                            If (sAutoScale = "ON" Or sAutoScale = "AUTO") Then
                                WriteMsg(OSCOPE, "SYST:AUT")
                                LongDelay((1))
                            End If
                            'end add''''''''''''''''''''''''''''''''''''''''''
                            gFrmMain.lblStatus.Text = "Making Measurement ..."
                            If (sLowerLimit = "10%" And sUpperLimit = "90%") Then
                                WriteMsg(OSCOPE, "MEAS:VOLT:RISE:TIME? (@" & sINP & ")")
                            ElseIf Not (sLowerLimit = "10%") Then
                                WriteMsg(OSCOPE, "MEAS:VOLT:RISE:TIME? " & sLowerLimit & ", " & " 90%, (@" & sINP & ")")
                            Else
                                WriteMsg(OSCOPE, "MEAS:VOLT:RISE:TIME? " & sLowerLimit & ", " & sUpperLimit & ", (@" & sINP & ")")
                            End If
                            LongDelay((1))
                            nErr = ReadMsg(OSCOPE, sData)
                            WriteMsg(OSCOPE, "*CLS")
                            If nErr <> 0 Then
                                Echo("VISA ERROR: " & nErr)
                                dMeasRiseTime = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(nErr)
                                dMeasRiseTime = 0
                                Exit Function
                            End If
                            dMeasRiseTime = CDbl(sData)
                            gFrmMain.txtMeasured.Text = CStr(dMeasRiseTime)
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
            dMeasRiseTime = CDbl(InputBox("Command cmdOSCOPE.dMeasRiseTime peformed." & vbCrLf & "Enter Rise Time Value:", "SIMULATION MODE"))
            gFrmMain.txtMeasured.Text = CStr(dMeasRiseTime)
            If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON)
        End If

        gFrmMain.lblStatus.Text = sCurrentMsg
    End Function
	
    ''' <summary>
    ''' perform time max measurement
    ''' </summary>
    ''' <param name="sAutoScale">"ON" or "OFF"</param>
    ''' <param name="sINP">select INP1 or INP2 (OSCOPE input channel)</param>
    ''' <param name="dVoltRange">set the full scale vertical range;  min = 8mV, max = 40V, at reset = 4V</param>
    ''' <param name="dTimeRange">set the full scale horizontal range for sweep;  min = 10 ns, max = 50 s, at reset = 1 ms</param>
    ''' <param name="dTriggerLevel">set trigger level voltage (0V at reset)</param>
    ''' <param name="sTriggerSlope">set trigger slope ("POS" at reset)</param>
    ''' <param name="sTriggerSource">set trigger source ("INP1" at reset)</param>
    ''' <param name="sLowerLimit">can be entered either in percent or voltage values (default = 10%)</param>
    ''' <param name="sUpperLimit">can be entered either in percent or voltage values (default = 90%)</param>
    ''' <param name="nInputImp">select input impedance 50 or 1M (1M at reset)</param>
    ''' <param name="sInputCoupling">select input coupling for input channels ("DC" at reset)</param>
    ''' <param name="sLPAS">select low-pass filter ("OFF" at reset)</param>
    ''' <param name="sHPAS">select high-pass filter ("OFF" at reset)</param>
    ''' <param name="sHYST">set noise rejection ("OFF" at reset)</param>
    ''' <param name="APROBE">Indicates AProbe usage; "OFF", "Single" Measurement, or "Contiuous" Measurement</param>
    ''' <returns>Time at which first max voltage occurred</returns>
    ''' <remarks>sAutoScale: 
    '''             auto scale can be used to determine vertical and horizontal parameters when sAutoScale = "ON", optional parameters are ignored.  
    ''' Autoscale should only be used with relatively stable input signals having a duty cycle of greater than 0.5% and a frequency greater than 50Hz.
    ''' sINP:
    '''        At reset, INP1 is on and INP2 if off.  Max input voltage that can be applied to the two input connectors is 5 Vrms at 50 ohms 
    ''' or +/-250V at 1Mohms.</remarks>
    Public Function dMeasTimeMax(ByRef sAutoScale As String, ByRef sINP As String, Optional ByRef dVoltRange As Double = 4,
                                 Optional ByRef dTimeRange As Double = 0.001, Optional ByRef dTriggerLevel As Double = 0,
                                 Optional ByRef sTriggerSlope As String = "POS", Optional ByRef sTriggerSource As String = "INP1",
                                 Optional ByRef sLowerLimit As String = "10%", Optional ByRef sUpperLimit As String = "90%",
                                 Optional ByRef nInputImp As Integer = 1000000, Optional ByRef sInputCoupling As String = "DC",
                                 Optional ByRef sLPAS As String = "OFF", Optional ByRef sHPAS As String = "OFF",
                                 Optional ByRef sHYST As String = "OFF", Optional ByRef APROBE As String = "OFF") As Double
        Dim sData As String = ""
        Dim nErr As Integer
        Dim sTimeRange As String
        Dim sVoltRange As String
        Dim sTriggerLevel As String
        Dim sInputImp As String
        Dim sCurrentMsg As String

        dMeasTimeMax = 0.0

        sCurrentMsg = gFrmMain.lblStatus.Text
        If APROBE <> "OFF" Then
            gFrmMain.lblStatus.Text = "APPLY PROBE"
        End If

        'convert data type to string type
        sInputImp = CStr(nInputImp)
        sTimeRange = CStr(dTimeRange)
        sVoltRange = CStr(dVoltRange)
        sTriggerLevel = CStr(dTriggerLevel)

        'convert string data to upper case
        sAutoScale = UCase(sAutoScale)
        sINP = UCase(sINP)
        sInputCoupling = UCase(sInputCoupling)
        sLPAS = UCase(sLPAS)
        sHPAS = UCase(sHPAS)
        sHYST = UCase(sHYST)
        sTriggerSlope = UCase(sTriggerSlope)
        sTriggerSource = UCase(sTriggerSource)

        'display measurement info to TPS shell
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        gFrmMain.txtInstrument.Text = "OSCOPE"
        gFrmMain.txtCommand.Text = "dMeasTimeMax"

        'Setup OSCOPE for FALL:OVERshoot Measurement
        If Not bSimulation Then
            WriteMsg(OSCOPE, "SYST:LANG SCPI")

            LongDelay((1))
            WriteMsg(OSCOPE, "*CLS")
            WriteMsg(OSCOPE, "*RST")

            'select auto scale
            Select Case sAutoScale
                Case "ON", "AUTO"
                    WriteMsg(OSCOPE, "SYST:AUT")
                    LongDelay((2))

                Case "OFF" 'manually set vertical and horizontal ranges
                    'set input channel: INP1 or INP2
                    If (sINP = "INP1" Or sINP = "INP2") Then
                        WriteMsg(OSCOPE, sINP & " ON")
                    Else
                        Echo("OSCOPE INPUT CHANNEL SELECTION ERROR")
                        dMeasTimeMax = 0
                        Exit Function
                    End If

                    'set input coupling: DC or AC
                    If (sInputCoupling = "DC" Or sInputCoupling = "AC") Then
                        WriteMsg(OSCOPE, sINP & ":COUP " & sInputCoupling)
                    Else
                        Echo("OSCOPE INPUT COUPLING PARAMETER ERROR")
                        dMeasTimeMax = 0
                        Exit Function
                    End If

                    'set input impedance: 1000000 or 50
                    If (nInputImp = 50 Or nInputImp = 1000000) Then
                        WriteMsg(OSCOPE, sINP & ":IMP " & sInputImp)
                    Else
                        Echo("OSCOPE INPUT IMPEDANCE PARAMETER ERROR")
                        dMeasTimeMax = 0
                        Exit Function
                    End If

                    'set input low-pass filter state: OFF or ON
                    If (sLPAS = "ON" Or sLPAS = "OFF") Then
                        WriteMsg(OSCOPE, sINP & ":FILT:LPAS " & sLPAS)
                    Else
                        Echo("OSCOPE INPUT FILTER STATE PARAMETER ERROR")
                        dMeasTimeMax = 0
                        Exit Function
                    End If

                    'set input high-pass filter state: OFF or ON
                    If (sHPAS = "ON" Or sHPAS = "OFF") Then
                        WriteMsg(OSCOPE, sINP & ":FILT:hPAS " & sHPAS)
                    Else
                        Echo("OSCOPE INPUT FILTER STATE PARAMETER ERROR")
                        dMeasTimeMax = 0
                        Exit Function
                    End If

                    'set volt range: 8mV to 40V
                    If (dVoltRange < 0.008 Or dVoltRange > 40) Then
                        Echo("OSCOPE VOLT RANGE ERROR")
                        dMeasTimeMax = 0
                        Exit Function
                    Else
                        Select Case sINP
                            Case "INP1"
                                WriteMsg(OSCOPE, "VOLT1:RANG " & sVoltRange)
                            Case "INP2"
                                WriteMsg(OSCOPE, "VOLT2:RANG " & sVoltRange)
                        End Select
                    End If

                    'set time range:10ns to 50s
                    If (dTimeRange < 0.00000001 Or dTimeRange > 50) Then
                        Echo("OSCOPE TIME RANGE ERROR")
                        dMeasTimeMax = 0
                        Exit Function
                    Else
                        WriteMsg(OSCOPE, "SWE:TIME:RANG " & sTimeRange)
                    End If

                    'set trigger level
                    WriteMsg(OSCOPE, "TRIG:LEV " & sTriggerLevel)

                    'set niose rejection: OFF or ON
                    If (sHYST = "ON" Or sHYST = "OFF") Then
                        WriteMsg(OSCOPE, "TRIG:HYST " & sHYST)
                    Else
                        Echo("OSCOPE TRIGGER INPUT PARAMETER ERROR")
                        dMeasTimeMax = 0
                        Exit Function
                    End If

                    'set trigger slope: POS or NEG
                    If (sTriggerSlope = "POS" Or sTriggerSlope = "NEG") Then
                        WriteMsg(OSCOPE, "TRIG:SLOP " & sTriggerSlope)
                    Else
                        Echo("OSCOPE TRIGGER SLOPE PARAMETER ERROR")
                        dMeasTimeMax = 0
                        Exit Function
                    End If

                    'set trigger source
                    Select Case sTriggerSource
                        Case "INP1", "INP2", "EXT", "ECLT0", "ECLT1"
                            WriteMsg(OSCOPE, "TRIG:SOUR " & sTriggerSource)

                        Case Else
                            Echo("OSCOPE TRIGGER SOURCE PARAMETER ERROR")
                            dMeasTimeMax = 0
                            Exit Function
                    End Select

                Case Else
                    Echo("OSCOPE AUTO SCALE PARAMETER ERROR")
                    dMeasTimeMax = 0
                    Exit Function
            End Select
        End If

        'Make Mesurement
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        If Not bSimulation Then
            Select Case Left(UCase(APROBE), 1)
                Case "O" ' Off
                    WriteMsg(OSCOPE, "MEAS:VOLT:TMAX? (@" & sINP & ")")
                    LongDelay((1))
                    nErr = ReadMsg(OSCOPE, sData)
                    WriteMsg(OSCOPE, "*CLS")
                    If nErr <> 0 Then
                        Echo("VISA ERROR: " & nErr)
                        dMeasTimeMax = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(nErr)
                        dMeasTimeMax = 0
                        Exit Function
                    End If
                    dMeasTimeMax = CDbl(sData)
                    gFrmMain.txtMeasured.Text = CStr(dMeasTimeMax)
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
                        '5/14/03 by Soon Nam''''''''''''''''''''''''''''''
                        'set auto scale with analog probe measurements
                        If (sAutoScale = "ON" Or sAutoScale = "AUTO") Then
                            WriteMsg(OSCOPE, "SYST:AUT")
                            LongDelay((1))
                        End If
                        'end add''''''''''''''''''''''''''''''''''''''''''
                        gFrmMain.lblStatus.Text = "Making Measurement ..."
                        WriteMsg(OSCOPE, "MEAS:VOLT:TMAX? (@" & sINP & ")")
                        LongDelay((1))
                        nErr = ReadMsg(OSCOPE, sData)
                        WriteMsg(OSCOPE, "*CLS")
                        If nErr <> 0 Then
                            Echo("VISA ERROR: " & nErr)
                            dMeasTimeMax = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(nErr)
                            dMeasTimeMax = 0
                            Exit Function
                        End If
                        dMeasTimeMax = CDbl(sData)
                        gFrmMain.txtMeasured.Text = CStr(dMeasTimeMax)
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

                Case "C" ' Continuous
                    MisProbe = MsgBoxResult.Yes
                    Do While MisProbe = MsgBoxResult.Yes
                        UserEvent = 0
                        Failed = False
                        gFrmMain.TimerProbe.Enabled = True : gFrmMain.fraInstructions.Visible = True
                        Do While (UserEvent = 0) And (Not bProbeClosed)
                            '5/14/03 by Soon Nam''''''''''''''''''''''''''''''
                            'set auto scale with analog probe measurements
                            If (sAutoScale = "ON" Or sAutoScale = "AUTO") Then
                                WriteMsg(OSCOPE, "SYST:AUT")
                                LongDelay((1))
                            End If
                            'end add''''''''''''''''''''''''''''''''''''''''''
                            gFrmMain.lblStatus.Text = "Making Measurement ..."
                            WriteMsg(OSCOPE, "MEAS:VOLT:TMAX? (@" & sINP & ")")
                            LongDelay((1))
                            nErr = ReadMsg(OSCOPE, sData)
                            WriteMsg(OSCOPE, "*CLS")
                            If nErr <> 0 Then
                                Echo("VISA ERROR: " & nErr)
                                dMeasTimeMax = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(nErr)
                                dMeasTimeMax = 0
                                Exit Function
                            End If
                            dMeasTimeMax = CDbl(sData)
                            gFrmMain.txtMeasured.Text = CStr(dMeasTimeMax)
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
            dMeasTimeMax = CDbl(InputBox("Command cmdOSCOPE.dMeasMaxTime peformed." & vbCrLf & "Enter Value:", "SIMULATION MODE"))
            gFrmMain.txtMeasured.Text = CStr(dMeasTimeMax)
            If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON)
        End If

        gFrmMain.lblStatus.Text = sCurrentMsg
    End Function

    ''' <summary>
    ''' perform time min measurement
    ''' </summary>
    ''' <param name="sAutoScale">"ON" or "OFF"</param>
    ''' <param name="sINP">select INP1 or INP2 (OSCOPE input channel)</param>
    ''' <param name="dVoltRange">set the full scale vertical range;  min = 8mV, max = 40V, at reset = 4V</param>
    ''' <param name="dTimeRange">set the full scale horizontal range for sweep;  min = 10 ns, max = 50 s, at reset = 1 ms</param>
    ''' <param name="dTriggerLevel">set trigger level voltage (0V at reset)</param>
    ''' <param name="sTriggerSlope">set trigger slope ("POS" at reset)</param>
    ''' <param name="sTriggerSource">set trigger source ("INP1" at reset)</param>
    ''' <param name="sLowerLimit">can be entered either in percent or voltage values (default = 10%)</param>
    ''' <param name="sUpperLimit">can be entered either in percent or voltage values (default = 90%)</param>
    ''' <param name="nInputImp">select input impedance 50 or 1M (1M at reset)</param>
    ''' <param name="sInputCoupling">select input coupling for input channels ("DC" at reset)</param>
    ''' <param name="sLPAS">select low-pass filter ("OFF" at reset)</param>
    ''' <param name="sHPAS">select high-pass filter ("OFF" at reset)</param>
    ''' <param name="sHYST">set noise rejection ("OFF" at reset)</param>
    ''' <param name="APROBE">Indicates AProbe usage; "OFF", "Single" Measurement, or "Contiuous" Measurement</param>
    ''' <returns>The time at which first min voltage occurred (trig level as reference)</returns>
    ''' <remarks>sAutoScale: 
    '''             auto scale can be used to determine vertical and horizontal parameters when sAutoScale = "ON", optional parameters are ignored.  
    ''' Autoscale should only be used with relatively stable input signals having a duty cycle of greater than 0.5% and a frequency greater than 50Hz.
    ''' sINP:
    '''        At reset, INP1 is on and INP2 if off.  Max input voltage that can be applied to the two input connectors is 5 Vrms at 50 ohms 
    ''' or +/-250V at 1Mohms.</remarks>
    Public Function dMeasTimeMin(ByRef sAutoScale As String, ByRef sINP As String, Optional ByRef dVoltRange As Double = 4,
                                 Optional ByRef dTimeRange As Double = 0.001, Optional ByRef dTriggerLevel As Double = 0,
                                 Optional ByRef sTriggerSlope As String = "POS", Optional ByRef sTriggerSource As String = "INP1",
                                 Optional ByRef sLowerLimit As String = "10%", Optional ByRef sUpperLimit As String = "90%",
                                 Optional ByRef nInputImp As Integer = 1000000, Optional ByRef sInputCoupling As String = "DC",
                                 Optional ByRef sLPAS As String = "OFF", Optional ByRef sHPAS As String = "OFF",
                                 Optional ByRef sHYST As String = "OFF", Optional ByRef APROBE As String = "OFF") As Double
        Dim sData As String = ""
        Dim nErr As Integer
        Dim sTimeRange As String
        Dim sVoltRange As String
        Dim sTriggerLevel As String
        Dim sInputImp As String
        Dim sCurrentMsg As String

        dMeasTimeMin = 0.0

        sCurrentMsg = gFrmMain.lblStatus.Text
        If APROBE <> "OFF" Then
            gFrmMain.lblStatus.Text = "APPLY PROBE"
        End If

        'convert data type to string type
        sInputImp = CStr(nInputImp)
        sTimeRange = CStr(dTimeRange)
        sVoltRange = CStr(dVoltRange)
        sTriggerLevel = CStr(dTriggerLevel)

        'convert string data to upper case
        sAutoScale = UCase(sAutoScale)
        sINP = UCase(sINP)
        sInputCoupling = UCase(sInputCoupling)
        sLPAS = UCase(sLPAS)
        sHPAS = UCase(sHPAS)
        sHYST = UCase(sHYST)
        sTriggerSlope = UCase(sTriggerSlope)
        sTriggerSource = UCase(sTriggerSource)

        'display measurement info to TPS shell
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        gFrmMain.txtInstrument.Text = "OSCOPE"
        gFrmMain.txtCommand.Text = "dMeasTimeMin"

        'Setup OSCOPE for FALL:OVERshoot Measurement
        If Not bSimulation Then
            WriteMsg(OSCOPE, "SYST:LANG SCPI")

            LongDelay((1))
            WriteMsg(OSCOPE, "*CLS")
            WriteMsg(OSCOPE, "*RST")

            'select auto scale
            Select Case sAutoScale
                Case "ON", "AUTO"
                    WriteMsg(OSCOPE, "SYST:AUT")
                    LongDelay((2))

                Case "OFF" 'manually set vertical and horizontal ranges
                    'set input channel: INP1 or INP2
                    If (sINP = "INP1" Or sINP = "INP2") Then
                        WriteMsg(OSCOPE, sINP & " ON")
                    Else
                        Echo("OSCOPE INPUT CHANNEL SELECTION ERROR")
                        dMeasTimeMin = 0
                        Exit Function
                    End If

                    'set input coupling: DC or AC
                    If (sInputCoupling = "DC" Or sInputCoupling = "AC") Then
                        WriteMsg(OSCOPE, sINP & ":COUP " & sInputCoupling)
                    Else
                        Echo("OSCOPE INPUT COUPLING PARAMETER ERROR")
                        dMeasTimeMin = 0
                        Exit Function
                    End If

                    'set input impedance: 1000000 or 50
                    If (nInputImp = 50 Or nInputImp = 1000000) Then
                        WriteMsg(OSCOPE, sINP & ":IMP " & sInputImp)
                    Else
                        Echo("OSCOPE INPUT IMPEDANCE PARAMETER ERROR")
                        dMeasTimeMin = 0
                        Exit Function
                    End If

                    'set input low-pass filter state: OFF or ON
                    If (sLPAS = "ON" Or sLPAS = "OFF") Then
                        WriteMsg(OSCOPE, sINP & ":FILT:LPAS " & sLPAS)
                    Else
                        Echo("OSCOPE INPUT FILTER STATE PARAMETER ERROR")
                        dMeasTimeMin = 0
                        Exit Function
                    End If

                    'set input high-pass filter state: OFF or ON
                    If (sHPAS = "ON" Or sHPAS = "OFF") Then
                        WriteMsg(OSCOPE, sINP & ":FILT:hPAS " & sHPAS)
                    Else
                        Echo("OSCOPE INPUT FILTER STATE PARAMETER ERROR")
                        dMeasTimeMin = 0
                        Exit Function
                    End If

                    'set volt range: 8mV to 40V
                    If (dVoltRange < 0.008 Or dVoltRange > 40) Then
                        Echo("OSCOPE VOLT RANGE ERROR")
                        dMeasTimeMin = 0
                        Exit Function
                    Else
                        Select Case sINP
                            Case "INP1"
                                WriteMsg(OSCOPE, "VOLT1:RANG " & sVoltRange)
                            Case "INP2"
                                WriteMsg(OSCOPE, "VOLT2:RANG " & sVoltRange)
                        End Select
                    End If

                    'set time range:10ns to 50s
                    If (dTimeRange < 0.00000001 Or dTimeRange > 50) Then
                        Echo("OSCOPE TIME RANGE ERROR")
                        dMeasTimeMin = 0
                        Exit Function
                    Else
                        WriteMsg(OSCOPE, "SWE:TIME:RANG " & sTimeRange)
                    End If

                    'set trigger level
                    WriteMsg(OSCOPE, "TRIG:LEV " & sTriggerLevel)

                    'set niose rejection: OFF or ON
                    If (sHYST = "ON" Or sHYST = "OFF") Then
                        WriteMsg(OSCOPE, "TRIG:HYST " & sHYST)
                    Else
                        Echo("OSCOPE TRIGGER INPUT PARAMETER ERROR")
                        dMeasTimeMin = 0
                        Exit Function
                    End If

                    'set trigger slope: POS or NEG
                    If (sTriggerSlope = "POS" Or sTriggerSlope = "NEG") Then
                        WriteMsg(OSCOPE, "TRIG:SLOP " & sTriggerSlope)
                    Else
                        Echo("OSCOPE TRIGGER SLOPE PARAMETER ERROR")
                        dMeasTimeMin = 0
                        Exit Function
                    End If

                    'set trigger source
                    Select Case sTriggerSource
                        Case "INP1", "INP2", "EXT", "ECLT0", "ECLT1"
                            WriteMsg(OSCOPE, "TRIG:SOUR " & sTriggerSource)

                        Case Else
                            Echo("OSCOPE TRIGGER SOURCE PARAMETER ERROR")
                            dMeasTimeMin = 0
                            Exit Function
                    End Select

                Case Else
                    Echo("OSCOPE AUTO SCALE PARAMETER ERROR")
                    dMeasTimeMin = 0
                    Exit Function
            End Select
        End If

        'Make Mesurement
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        If Not bSimulation Then
            Select Case Left(UCase(APROBE), 1)
                Case "O" ' Off
                    WriteMsg(OSCOPE, "MEAS:VOLT:TMIN? (@" & sINP & ")")
                    LongDelay((1))
                    nErr = ReadMsg(OSCOPE, sData)
                    WriteMsg(OSCOPE, "*CLS")
                    If nErr <> 0 Then
                        Echo("VISA ERROR: " & nErr)
                        dMeasTimeMin = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(nErr)
                        dMeasTimeMin = 0
                        Exit Function
                    End If
                    dMeasTimeMin = CDbl(sData)
                    gFrmMain.txtMeasured.Text = CStr(dMeasTimeMin)
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
                        '5/14/03 by Soon Nam''''''''''''''''''''''''''''''
                        'set auto scale with analog probe measurements
                        If (sAutoScale = "ON" Or sAutoScale = "AUTO") Then
                            WriteMsg(OSCOPE, "SYST:AUT")
                            LongDelay((1))
                        End If
                        'end add''''''''''''''''''''''''''''''''''''''''''
                        gFrmMain.lblStatus.Text = "Making Measurement ..."
                        WriteMsg(OSCOPE, "MEAS:VOLT:TMIN? (@" & sINP & ")")
                        LongDelay((1))
                        nErr = ReadMsg(OSCOPE, sData)
                        WriteMsg(OSCOPE, "*CLS")
                        If nErr <> 0 Then
                            Echo("VISA ERROR: " & nErr)
                            dMeasTimeMin = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(nErr)
                            dMeasTimeMin = 0
                            Exit Function
                        End If
                        dMeasTimeMin = CDbl(sData)
                        gFrmMain.txtMeasured.Text = CStr(dMeasTimeMin)
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

                Case "C" ' Continuous
                    MisProbe = MsgBoxResult.Yes
                    Do While MisProbe = MsgBoxResult.Yes
                        UserEvent = 0
                        Failed = False
                        gFrmMain.TimerProbe.Enabled = True : gFrmMain.fraInstructions.Visible = True
                        Do While (UserEvent = 0) And (Not bProbeClosed)
                            '5/14/03 by Soon Nam''''''''''''''''''''''''''''''
                            'set auto scale with analog probe measurements
                            If (sAutoScale = "ON" Or sAutoScale = "AUTO") Then
                                WriteMsg(OSCOPE, "SYST:AUT")
                                LongDelay((1))
                            End If
                            'end add''''''''''''''''''''''''''''''''''''''''''
                            gFrmMain.lblStatus.Text = "Making Measurement ..."
                            WriteMsg(OSCOPE, "MEAS:VOLT:TMIN? (@" & sINP & ")")
                            LongDelay((1))
                            nErr = ReadMsg(OSCOPE, sData)
                            WriteMsg(OSCOPE, "*CLS")
                            If nErr <> 0 Then
                                Echo("VISA ERROR: " & nErr)
                                dMeasTimeMin = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(nErr)
                                dMeasTimeMin = 0
                                Exit Function
                            End If
                            dMeasTimeMin = CDbl(sData)
                            gFrmMain.txtMeasured.Text = CStr(dMeasTimeMin)
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
            dMeasTimeMin = CDbl(InputBox("Command cmdOSCOPE.dMeasTimeMin peformed." & vbCrLf & "Enter Value:", "SIMULATION MODE"))
            gFrmMain.txtMeasured.Text = CStr(dMeasTimeMin)
            If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON)
        End If
        gFrmMain.lblStatus.Text = sCurrentMsg

    End Function

    ''' <summary>
    ''' perform duty cycle measurement
    ''' </summary>
    ''' <param name="sAutoScale">"ON" or "OFF"</param>
    ''' <param name="sINP">select INP1 or INP2 (OSCOPE input channel)</param>
    ''' <param name="dVoltRange">set the full scale vertical range;  min = 8mV, max = 40V, at reset = 4V</param>
    ''' <param name="dTimeRange">set the full scale horizontal range for sweep;  min = 10 ns, max = 50 s, at reset = 1 ms</param>
    ''' <param name="dTriggerLevel">set trigger level voltage (0V at reset)</param>
    ''' <param name="sTriggerSlope">set trigger slope ("POS" at reset)</param>
    ''' <param name="sTriggerSource">set trigger source ("INP1" at reset)</param>
    ''' <param name="nInputImp">select input impedance 50 or 1M (1M at reset)</param>
    ''' <param name="sInputCoupling">select input coupling for input channels ("DC" at reset)</param>
    ''' <param name="sLPAS">select low-pass filter ("OFF" at reset)</param>
    ''' <param name="sHPAS">select high-pass filter ("OFF" at reset)</param>
    ''' <param name="sHYST">set noise rejection ("OFF" at reset)</param>
    ''' <param name="APROBE">Indicates AProbe usage; "OFF", "Single" Measurement, or "Contiuous" Measurement</param>
    ''' <returns>+pulse width/period</returns>
    ''' <remarks>sAutoScale: 
    '''             auto scale can be used to determine vertical and horizontal parameters when sAutoScale = "ON", optional parameters are ignored.  
    ''' Autoscale should only be used with relatively stable input signals having a duty cycle of greater than 0.5% and a frequency greater than 50Hz.
    ''' sINP:
    '''        At reset, INP1 is on and INP2 if off.  Max input voltage that can be applied to the two input connectors is 5 Vrms at 50 ohms 
    ''' or +/-250V at 1Mohms.</remarks>
    Public Function dMeasDCYC(ByRef sAutoScale As String, ByRef sINP As String, Optional ByRef dVoltRange As Double = 4,
                              Optional ByRef dTimeRange As Double = 0.001, Optional ByRef dTriggerLevel As Double = 0,
                              Optional ByRef sTriggerSlope As String = "POS", Optional ByRef sTriggerSource As String = "INP1",
                              Optional ByRef nInputImp As Integer = 1000000, Optional ByRef sInputCoupling As String = "DC",
                              Optional ByRef sLPAS As String = "OFF", Optional ByRef sHPAS As String = "OFF",
                              Optional ByRef sHYST As String = "OFF", Optional ByRef APROBE As String = "OFF") As Double
        Dim sData As String = ""
        Dim nErr As Integer
        Dim sTimeRange As String
        Dim sVoltRange As String
        Dim sTriggerLevel As String
        Dim sInputImp As String
        Dim sCurrentMsg As String

        dMeasDCYC = 0.0

        sCurrentMsg = gFrmMain.lblStatus.Text
        If APROBE <> "OFF" Then
            gFrmMain.lblStatus.Text = "APPLY PROBE"
        End If

        'convert data type to string type
        sInputImp = CStr(nInputImp)
        sTimeRange = CStr(dTimeRange)
        sVoltRange = CStr(dVoltRange)
        sTriggerLevel = CStr(dTriggerLevel)

        'convert string data to upper case
        sAutoScale = UCase(sAutoScale)
        sINP = UCase(sINP)
        sInputCoupling = UCase(sInputCoupling)
        sLPAS = UCase(sLPAS)
        sHPAS = UCase(sHPAS)
        sHYST = UCase(sHYST)
        sTriggerSlope = UCase(sTriggerSlope)
        sTriggerSource = UCase(sTriggerSource)

        'display measurement info to TPS shell
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        gFrmMain.txtInstrument.Text = "OSCOPE"
        gFrmMain.txtCommand.Text = "dMeasDCYC"

        'Setup OSCOPE for duty cycle Measurement
        If Not bSimulation Then
            WriteMsg(OSCOPE, "SYST:LANG SCPI")

            LongDelay((1))
            WriteMsg(OSCOPE, "*CLS")
            WriteMsg(OSCOPE, "*RST")

            'select auto scale
            Select Case sAutoScale
                Case "ON", "AUTO"
                    WriteMsg(OSCOPE, "SYST:AUT")
                    LongDelay((2))

                Case "OFF" 'manually set vertical and horizontal ranges
                    'set input channel: INP1 or INP2
                    If (sINP = "INP1" Or sINP = "INP2") Then
                        WriteMsg(OSCOPE, sINP & " ON")
                    Else
                        Echo("OSCOPE INPUT CHANNEL SELECTION ERROR")
                        dMeasDCYC = 0
                        Exit Function
                    End If

                    'set input coupling: DC or AC
                    If (sInputCoupling = "DC" Or sInputCoupling = "AC") Then
                        WriteMsg(OSCOPE, sINP & ":COUP " & sInputCoupling)
                    Else
                        Echo("OSCOPE INPUT COUPLING PARAMETER ERROR")
                        dMeasDCYC = 0
                        Exit Function
                    End If

                    'set input impedance: 1000000 or 50
                    If (nInputImp = 50 Or nInputImp = 1000000) Then
                        WriteMsg(OSCOPE, sINP & ":IMP " & sInputImp)
                    Else
                        Echo("OSCOPE INPUT IMPEDANCE PARAMETER ERROR")
                        dMeasDCYC = 0
                        Exit Function
                    End If

                    'set input low-pass filter state: OFF or ON
                    If (sLPAS = "ON" Or sLPAS = "OFF") Then
                        WriteMsg(OSCOPE, sINP & ":FILT:LPAS " & sLPAS)
                    Else
                        Echo("OSCOPE INPUT FILTER STATE PARAMETER ERROR")
                        dMeasDCYC = 0
                        Exit Function
                    End If

                    'set input high-pass filter state: OFF or ON
                    If (sHPAS = "ON" Or sHPAS = "OFF") Then
                        WriteMsg(OSCOPE, sINP & ":FILT:hPAS " & sHPAS)
                    Else
                        Echo("OSCOPE INPUT FILTER STATE PARAMETER ERROR")
                        dMeasDCYC = 0
                        Exit Function
                    End If

                    'set volt range: 8mV to 40V
                    If (dVoltRange < 0.008 Or dVoltRange > 40) Then
                        Echo("OSCOPE VOLT RANGE ERROR")
                        dMeasDCYC = 0
                        Exit Function
                    Else
                        Select Case sINP
                            Case "INP1"
                                WriteMsg(OSCOPE, "VOLT1:RANG " & sVoltRange)
                            Case "INP2"
                                WriteMsg(OSCOPE, "VOLT2:RANG " & sVoltRange)
                        End Select
                    End If

                    'set time range:10ns to 50s
                    If (dTimeRange < 0.00000001 Or dTimeRange > 50) Then
                        Echo("OSCOPE TIME RANGE ERROR")
                        dMeasDCYC = 0
                        Exit Function
                    Else
                        WriteMsg(OSCOPE, "SWE:TIME:RANG " & sTimeRange)
                    End If

                    'set trigger level
                    WriteMsg(OSCOPE, "TRIG:LEV " & sTriggerLevel)

                    'set niose rejection: OFF or ON
                    If (sHYST = "ON" Or sHYST = "OFF") Then
                        WriteMsg(OSCOPE, "TRIG:HYST " & sHYST)
                    Else
                        Echo("OSCOPE TRIGGER INPUT PARAMETER ERROR")
                        dMeasDCYC = 0
                        Exit Function
                    End If

                    'set trigger slope: POS or NEG
                    If (sTriggerSlope = "POS" Or sTriggerSlope = "NEG") Then
                        WriteMsg(OSCOPE, "TRIG:SLOP " & sTriggerSlope)
                    Else
                        Echo("OSCOPE TRIGGER SLOPE PARAMETER ERROR")
                        dMeasDCYC = 0
                        Exit Function
                    End If

                    'set trigger source
                    Select Case sTriggerSource
                        Case "INP1", "INP2", "EXT", "ECLT0", "ECLT1"
                            WriteMsg(OSCOPE, "TRIG:SOUR " & sTriggerSource)

                        Case Else
                            Echo("OSCOPE TRIGGER SOURCE PARAMETER ERROR")
                            dMeasDCYC = 0
                            Exit Function
                    End Select

                Case Else
                    Echo("OSCOPE AUTO SCALE PARAMETER ERROR")
                    dMeasDCYC = 0
                    Exit Function
            End Select
        End If

        'Make Mesurement
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        If Not bSimulation Then
            Select Case Left(UCase(APROBE), 1)
                Case "O" ' Off
                    WriteMsg(OSCOPE, "MEAS:VOLT:DCYC? (@" & sINP & ")")
                    LongDelay((1))
                    nErr = ReadMsg(OSCOPE, sData)
                    WriteMsg(OSCOPE, "*CLS")
                    If nErr <> 0 Then
                        Echo("VISA ERROR: " & nErr)
                        dMeasDCYC = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(nErr)
                        dMeasDCYC = 0
                        Exit Function
                    End If
                    dMeasDCYC = CDbl(sData)
                    gFrmMain.txtMeasured.Text = CStr(dMeasDCYC)
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
                        '5/14/03 by Soon Nam''''''''''''''''''''''''''''''
                        'set auto scale with analog probe measurements
                        If (sAutoScale = "ON" Or sAutoScale = "AUTO") Then
                            WriteMsg(OSCOPE, "SYST:AUT")
                            LongDelay((1))
                        End If
                        'end add''''''''''''''''''''''''''''''''''''''''''
                        gFrmMain.lblStatus.Text = "Making Measurement ..."
                        WriteMsg(OSCOPE, "MEAS:VOLT:DCYC? (@" & sINP & ")")
                        LongDelay((1))
                        nErr = ReadMsg(OSCOPE, sData)
                        WriteMsg(OSCOPE, "*CLS")
                        If nErr <> 0 Then
                            Echo("VISA ERROR: " & nErr)
                            dMeasDCYC = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(nErr)
                            dMeasDCYC = 0
                            Exit Function
                        End If
                        dMeasDCYC = CDbl(sData)
                        gFrmMain.txtMeasured.Text = CStr(dMeasDCYC)
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

                Case "C" ' Continuous
                    MisProbe = MsgBoxResult.Yes
                    Do While MisProbe = MsgBoxResult.Yes
                        UserEvent = 0
                        Failed = False
                        gFrmMain.TimerProbe.Enabled = True : gFrmMain.fraInstructions.Visible = True
                        Do While (UserEvent = 0) And (Not bProbeClosed)
                            '5/14/03 by Soon Nam''''''''''''''''''''''''''''''
                            'set auto scale with analog probe measurements
                            If (sAutoScale = "ON" Or sAutoScale = "AUTO") Then
                                WriteMsg(OSCOPE, "SYST:AUT")
                                LongDelay((1))
                            End If
                            'end add''''''''''''''''''''''''''''''''''''''''''
                            gFrmMain.lblStatus.Text = "Making Measurement ..."
                            WriteMsg(OSCOPE, "MEAS:VOLT:DCYC? (@" & sINP & ")")
                            LongDelay((1))
                            nErr = ReadMsg(OSCOPE, sData)
                            WriteMsg(OSCOPE, "*CLS")
                            If nErr <> 0 Then
                                Echo("VISA ERROR: " & nErr)
                                dMeasDCYC = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(nErr)
                                dMeasDCYC = 0
                                Exit Function
                            End If
                            dMeasDCYC = CDbl(sData)
                            gFrmMain.txtMeasured.Text = CStr(dMeasDCYC)
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
            dMeasDCYC = CDbl(InputBox("Command cmdOSCOPE.dMeasDCYC peformed." & vbCrLf & "Enter Duty Cycle Value:", "SIMULATION MODE"))
            gFrmMain.txtMeasured.Text = CStr(dMeasDCYC)
            If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON)
        End If

        gFrmMain.lblStatus.Text = sCurrentMsg
    End Function
	
    ''' <summary>
    ''' perform negative duty cycle measurement
    ''' </summary>
    ''' <param name="sAutoScale">"ON" or "OFF"</param>
    ''' <param name="sINP">select INP1 or INP2 (OSCOPE input channel)</param>
    ''' <param name="dVoltRange">set the full scale vertical range;  min = 8mV, max = 40V, at reset = 4V</param>
    ''' <param name="dTimeRange">set the full scale horizontal range for sweep;  min = 10 ns, max = 50 s, at reset = 1 ms</param>
    ''' <param name="dTriggerLevel">set trigger level voltage (0V at reset)</param>
    ''' <param name="sTriggerSlope">set trigger slope ("POS" at reset)</param>
    ''' <param name="sTriggerSource">set trigger source ("INP1" at reset)</param>
    ''' <param name="nInputImp">select input impedance 50 or 1M (1M at reset)</param>
    ''' <param name="sInputCoupling">select input coupling for input channels ("DC" at reset)</param>
    ''' <param name="sLPAS">select low-pass filter ("OFF" at reset)</param>
    ''' <param name="sHPAS">select high-pass filter ("OFF" at reset)</param>
    ''' <param name="sHYST">set noise rejection ("OFF" at reset)</param>
    ''' <param name="APROBE">Indicates AProbe usage; "OFF", "Single" Measurement, or "Contiuous" Measurement</param>
    ''' <returns>-pulse width/period</returns>
    ''' <remarks>sAutoScale: 
    '''             auto scale can be used to determine vertical and horizontal parameters when sAutoScale = "ON", optional parameters are ignored.  
    ''' Autoscale should only be used with relatively stable input signals having a duty cycle of greater than 0.5% and a frequency greater than 50Hz.
    ''' sINP:
    '''        At reset, INP1 is on and INP2 if off.  Max input voltage that can be applied to the two input connectors is 5 Vrms at 50 ohms 
    ''' or +/-250V at 1Mohms.</remarks>
    Public Function dMeasNDUT(ByRef sAutoScale As String, ByRef sINP As String, Optional ByRef dVoltRange As Double = 4,
                              Optional ByRef dTimeRange As Double = 0.001, Optional ByRef dTriggerLevel As Double = 0,
                              Optional ByRef sTriggerSlope As String = "POS", Optional ByRef sTriggerSource As String = "INP1",
                              Optional ByRef nInputImp As Integer = 1000000, Optional ByRef sInputCoupling As String = "DC",
                              Optional ByRef sLPAS As String = "OFF", Optional ByRef sHPAS As String = "OFF",
                              Optional ByRef sHYST As String = "OFF", Optional ByRef APROBE As String = "OFF") As Double
        Dim sData As String = ""
        Dim nErr As Integer
        Dim sTimeRange As String
        Dim sVoltRange As String
        Dim sTriggerLevel As String
        Dim sInputImp As String
        Dim sCurrentMsg As String

        dMeasNDUT = 0.0

        sCurrentMsg = gFrmMain.lblStatus.Text
        If APROBE <> "OFF" Then
            gFrmMain.lblStatus.Text = "APPLY PROBE"
        End If

        'convert data type to string type
        sInputImp = CStr(nInputImp)
        sTimeRange = CStr(dTimeRange)
        sVoltRange = CStr(dVoltRange)
        sTriggerLevel = CStr(dTriggerLevel)

        'convert string data to upper case
        sAutoScale = UCase(sAutoScale)
        sINP = UCase(sINP)
        sInputCoupling = UCase(sInputCoupling)
        sLPAS = UCase(sLPAS)
        sHPAS = UCase(sHPAS)
        sHYST = UCase(sHYST)
        sTriggerSlope = UCase(sTriggerSlope)
        sTriggerSource = UCase(sTriggerSource)

        'display measurement info to TPS shell
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        gFrmMain.txtInstrument.Text = "OSCOPE"
        gFrmMain.txtCommand.Text = "dMeasNDUT"

        'Setup OSCOPE for duty cycle Measurement
        If Not bSimulation Then
            WriteMsg(OSCOPE, "SYST:LANG SCPI")

            LongDelay((1))
            WriteMsg(OSCOPE, "*CLS")
            WriteMsg(OSCOPE, "*RST")

            'select auto scale
            Select Case sAutoScale
                Case "ON", "AUTO"
                    WriteMsg(OSCOPE, "SYST:AUT")
                    LongDelay((2))

                Case "OFF" 'manually set vertical and horizontal ranges
                    'set input channel: INP1 or INP2
                    If (sINP = "INP1" Or sINP = "INP2") Then
                        WriteMsg(OSCOPE, sINP & " ON")
                    Else
                        Echo("OSCOPE INPUT CHANNEL SELECTION ERROR")
                        dMeasNDUT = 0
                        Exit Function
                    End If

                    'set input coupling: DC or AC
                    If (sInputCoupling = "DC" Or sInputCoupling = "AC") Then
                        WriteMsg(OSCOPE, sINP & ":COUP " & sInputCoupling)
                    Else
                        Echo("OSCOPE INPUT COUPLING PARAMETER ERROR")
                        dMeasNDUT = 0
                        Exit Function
                    End If

                    'set input impedance: 1000000 or 50
                    If (nInputImp = 50 Or nInputImp = 1000000) Then
                        WriteMsg(OSCOPE, sINP & ":IMP " & sInputImp)
                    Else
                        Echo("OSCOPE INPUT IMPEDANCE PARAMETER ERROR")
                        dMeasNDUT = 0
                        Exit Function
                    End If

                    'set input low-pass filter state: OFF or ON
                    If (sLPAS = "ON" Or sLPAS = "OFF") Then
                        WriteMsg(OSCOPE, sINP & ":FILT:LPAS " & sLPAS)
                    Else
                        Echo("OSCOPE INPUT FILTER STATE PARAMETER ERROR")
                        dMeasNDUT = 0
                        Exit Function
                    End If

                    'set input high-pass filter state: OFF or ON
                    If (sHPAS = "ON" Or sHPAS = "OFF") Then
                        WriteMsg(OSCOPE, sINP & ":FILT:hPAS " & sHPAS)
                    Else
                        Echo("OSCOPE INPUT FILTER STATE PARAMETER ERROR")
                        dMeasNDUT = 0
                        Exit Function
                    End If

                    'set volt range: 8mV to 40V
                    If (dVoltRange < 0.008 Or dVoltRange > 40) Then
                        Echo("OSCOPE VOLT RANGE ERROR")
                        dMeasNDUT = 0
                        Exit Function
                    Else
                        Select Case sINP
                            Case "INP1"
                                WriteMsg(OSCOPE, "VOLT1:RANG " & sVoltRange)
                            Case "INP2"
                                WriteMsg(OSCOPE, "VOLT2:RANG " & sVoltRange)
                        End Select
                    End If

                    'set time range:10ns to 50s
                    If (dTimeRange < 0.00000001 Or dTimeRange > 50) Then
                        Echo("OSCOPE TIME RANGE ERROR")
                        dMeasNDUT = 0
                        Exit Function
                    Else
                        WriteMsg(OSCOPE, "SWE:TIME:RANG " & sTimeRange)
                    End If

                    'set trigger level
                    WriteMsg(OSCOPE, "TRIG:LEV " & sTriggerLevel)

                    'set niose rejection: OFF or ON
                    If (sHYST = "ON" Or sHYST = "OFF") Then
                        WriteMsg(OSCOPE, "TRIG:HYST " & sHYST)
                    Else
                        Echo("OSCOPE TRIGGER INPUT PARAMETER ERROR")
                        dMeasNDUT = 0
                        Exit Function
                    End If

                    'set trigger slope: POS or NEG
                    If (sTriggerSlope = "POS" Or sTriggerSlope = "NEG") Then
                        WriteMsg(OSCOPE, "TRIG:SLOP " & sTriggerSlope)
                    Else
                        Echo("OSCOPE TRIGGER SLOPE PARAMETER ERROR")
                        dMeasNDUT = 0
                        Exit Function
                    End If

                    'set trigger source
                    Select Case sTriggerSource
                        Case "INP1", "INP2", "EXT", "ECLT0", "ECLT1"
                            WriteMsg(OSCOPE, "TRIG:SOUR " & sTriggerSource)

                        Case Else
                            Echo("OSCOPE TRIGGER SOURCE PARAMETER ERROR")
                            dMeasNDUT = 0
                            Exit Function
                    End Select

                Case Else
                    Echo("OSCOPE AUTO SCALE PARAMETER ERROR")
                    dMeasNDUT = 0
                    Exit Function
            End Select
        End If

        'Make Mesurement
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        If Not bSimulation Then
            Select Case Left(UCase(APROBE), 1)
                Case "O" ' Off
                    WriteMsg(OSCOPE, "MEAS:VOLT:NDUT? (@" & sINP & ")")
                    LongDelay((1))
                    nErr = ReadMsg(OSCOPE, sData)
                    WriteMsg(OSCOPE, "*CLS")
                    If nErr <> 0 Then
                        Echo("VISA ERROR: " & nErr)
                        dMeasNDUT = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(nErr)
                        dMeasNDUT = 0
                        Exit Function
                    End If
                    dMeasNDUT = CDbl(sData)
                    gFrmMain.txtMeasured.Text = CStr(dMeasNDUT)
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
                        '5/14/03 by Soon Nam''''''''''''''''''''''''''''''
                        'set auto scale with analog probe measurements
                        If (sAutoScale = "ON" Or sAutoScale = "AUTO") Then
                            WriteMsg(OSCOPE, "SYST:AUT")
                            LongDelay((1))
                        End If
                        'end add''''''''''''''''''''''''''''''''''''''''''
                        gFrmMain.lblStatus.Text = "Making Measurement ..."
                        WriteMsg(OSCOPE, "MEAS:VOLT:NDUT? (@" & sINP & ")")
                        LongDelay((1))
                        nErr = ReadMsg(OSCOPE, sData)
                        WriteMsg(OSCOPE, "*CLS")
                        If nErr <> 0 Then
                            Echo("VISA ERROR: " & nErr)
                            dMeasNDUT = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(nErr)
                            dMeasNDUT = 0
                            Exit Function
                        End If
                        dMeasNDUT = CDbl(sData)
                        gFrmMain.txtMeasured.Text = CStr(dMeasNDUT)
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

                Case "C" ' Continuous
                    MisProbe = MsgBoxResult.Yes
                    Do While MisProbe = MsgBoxResult.Yes
                        UserEvent = 0
                        Failed = False
                        gFrmMain.TimerProbe.Enabled = True : gFrmMain.fraInstructions.Visible = True
                        Do While (UserEvent = 0) And (Not bProbeClosed)
                            '5/14/03 by Soon Nam''''''''''''''''''''''''''''''
                            'set auto scale with analog probe measurements
                            If (sAutoScale = "ON" Or sAutoScale = "AUTO") Then
                                WriteMsg(OSCOPE, "SYST:AUT")
                                LongDelay((1))
                            End If
                            'end add''''''''''''''''''''''''''''''''''''''''''
                            gFrmMain.lblStatus.Text = "Making Measurement ..."
                            WriteMsg(OSCOPE, "MEAS:VOLT:NDUT? (@" & sINP & ")")
                            LongDelay((1))
                            nErr = ReadMsg(OSCOPE, sData)
                            WriteMsg(OSCOPE, "*CLS")
                            If nErr <> 0 Then
                                Echo("VISA ERROR: " & nErr)
                                dMeasNDUT = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(nErr)
                                dMeasNDUT = 0
                                Exit Function
                            End If
                            dMeasNDUT = CDbl(sData)
                            gFrmMain.txtMeasured.Text = CStr(dMeasNDUT)
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
            dMeasNDUT = CDbl(InputBox("Command cmdOSCOPE.dMeasNDUT peformed." & vbCrLf & "Enter Negative Duty Cycle Value:", "SIMULATION MODE"))
            gFrmMain.txtMeasured.Text = CStr(dMeasNDUT)
            If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON)
        End If
        gFrmMain.lblStatus.Text = sCurrentMsg

    End Function
	
    ''' <summary>
    ''' perform negative pulse width measurement
    ''' </summary>
    ''' <param name="sAutoScale">"ON" or "OFF"</param>
    ''' <param name="sINP">select INP1 or INP2 (OSCOPE input channel)</param>
    ''' <param name="dVoltRange">set the full scale vertical range;  min = 8mV, max = 40V, at reset = 4V</param>
    ''' <param name="dTimeRange">set the full scale horizontal range for sweep;  min = 10 ns, max = 50 s, at reset = 1 ms</param>
    ''' <param name="dTriggerLevel">set trigger level voltage (0V at reset)</param>
    ''' <param name="sTriggerSlope">set trigger slope ("POS" at reset)</param>
    ''' <param name="sTriggerSource">set trigger source ("INP1" at reset)</param>
    ''' <param name="nInputImp">select input impedance 50 or 1M (1M at reset)</param>
    ''' <param name="sInputCoupling">select input coupling for input channels ("DC" at reset)</param>
    ''' <param name="sLPAS">select low-pass filter ("OFF" at reset)</param>
    ''' <param name="sHPAS">select high-pass filter ("OFF" at reset)</param>
    ''' <param name="sHYST">set noise rejection ("OFF" at reset)</param>
    ''' <param name="APROBE">Indicates AProbe usage; "OFF", "Single" Measurement, or "Contiuous" Measurement</param>
    ''' <returns>Negative pulse width measurement</returns>
    ''' <remarks>sAutoScale: 
    '''             auto scale can be used to determine vertical and horizontal parameters when sAutoScale = "ON", optional parameters are ignored.  
    ''' Autoscale should only be used with relatively stable input signals having a duty cycle of greater than 0.5% and a frequency greater than 50Hz.
    ''' sINP:
    '''        At reset, INP1 is on and INP2 if off.  Max input voltage that can be applied to the two input connectors is 5 Vrms at 50 ohms 
    ''' or +/-250V at 1Mohms.</remarks>
    Public Function dMeasNWID(ByRef sAutoScale As String, ByRef sINP As String, Optional ByRef dVoltRange As Double = 4,
                              Optional ByRef dTimeRange As Double = 0.001, Optional ByRef dTriggerLevel As Double = 0,
                              Optional ByRef sTriggerSlope As String = "POS", Optional ByRef sTriggerSource As String = "INP1",
                              Optional ByRef nInputImp As Integer = 1000000, Optional ByRef sInputCoupling As String = "DC",
                              Optional ByRef sLPAS As String = "OFF", Optional ByRef sHPAS As String = "OFF",
                              Optional ByRef sHYST As String = "OFF", Optional ByRef APROBE As String = "OFF") As Double
        Dim sData As String = ""
        Dim nErr As Integer
        Dim sTimeRange As String
        Dim sVoltRange As String
        Dim sTriggerLevel As String
        Dim sInputImp As String
        Dim sCurrentMsg As String

        dMeasNWID = 0.0

        sCurrentMsg = gFrmMain.lblStatus.Text
        If APROBE <> "OFF" Then
            gFrmMain.lblStatus.Text = "APPLY PROBE"
        End If

        'convert data type to string type
        sInputImp = CStr(nInputImp)
        sTimeRange = CStr(dTimeRange)
        sVoltRange = CStr(dVoltRange)
        sTriggerLevel = CStr(dTriggerLevel)

        'convert string data to upper case
        sAutoScale = UCase(sAutoScale)
        sINP = UCase(sINP)
        sInputCoupling = UCase(sInputCoupling)
        sLPAS = UCase(sLPAS)
        sHPAS = UCase(sHPAS)
        sHYST = UCase(sHYST)
        sTriggerSlope = UCase(sTriggerSlope)
        sTriggerSource = UCase(sTriggerSource)

        'display measurement info to TPS shell
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        gFrmMain.txtInstrument.Text = "OSCOPE"
        gFrmMain.txtCommand.Text = "dMeasNWID"

        'Setup OSCOPE for duty cycle Measurement
        If Not bSimulation Then
            WriteMsg(OSCOPE, "SYST:LANG SCPI")

            LongDelay((1))
            WriteMsg(OSCOPE, "*CLS")
            WriteMsg(OSCOPE, "*RST")

            'select auto scale
            Select Case sAutoScale
                Case "ON", "AUTO"
                    WriteMsg(OSCOPE, "SYST:AUT")
                    LongDelay((2))

                Case "OFF" 'manually set vertical and horizontal ranges
                    'set input channel: INP1 or INP2
                    If (sINP = "INP1" Or sINP = "INP2") Then
                        WriteMsg(OSCOPE, sINP & " ON")
                    Else
                        Echo("OSCOPE INPUT CHANNEL SELECTION ERROR")
                        dMeasNWID = 0
                        Exit Function
                    End If

                    'set input coupling: DC or AC
                    If (sInputCoupling = "DC" Or sInputCoupling = "AC") Then
                        WriteMsg(OSCOPE, sINP & ":COUP " & sInputCoupling)
                    Else
                        Echo("OSCOPE INPUT COUPLING PARAMETER ERROR")
                        dMeasNWID = 0
                        Exit Function
                    End If

                    'set input impedance: 1000000 or 50
                    If (nInputImp = 50 Or nInputImp = 1000000) Then
                        WriteMsg(OSCOPE, sINP & ":IMP " & sInputImp)
                    Else
                        Echo("OSCOPE INPUT IMPEDANCE PARAMETER ERROR")
                        dMeasNWID = 0
                        Exit Function
                    End If

                    'set input low-pass filter state: OFF or ON
                    If (sLPAS = "ON" Or sLPAS = "OFF") Then
                        WriteMsg(OSCOPE, sINP & ":FILT:LPAS " & sLPAS)
                    Else
                        Echo("OSCOPE INPUT FILTER STATE PARAMETER ERROR")
                        dMeasNWID = 0
                        Exit Function
                    End If

                    'set input high-pass filter state: OFF or ON
                    If (sHPAS = "ON" Or sHPAS = "OFF") Then
                        WriteMsg(OSCOPE, sINP & ":FILT:hPAS " & sHPAS)
                    Else
                        Echo("OSCOPE INPUT FILTER STATE PARAMETER ERROR")
                        dMeasNWID = 0
                        Exit Function
                    End If

                    'set volt range: 8mV to 40V
                    If (dVoltRange < 0.008 Or dVoltRange > 40) Then
                        Echo("OSCOPE VOLT RANGE ERROR")
                        dMeasNWID = 0
                        Exit Function
                    Else
                        Select Case sINP
                            Case "INP1"
                                WriteMsg(OSCOPE, "VOLT1:RANG " & sVoltRange)
                            Case "INP2"
                                WriteMsg(OSCOPE, "VOLT2:RANG " & sVoltRange)
                        End Select
                    End If

                    'set time range:10ns to 50s
                    If (dTimeRange < 0.00000001 Or dTimeRange > 50) Then
                        Echo("OSCOPE TIME RANGE ERROR")
                        dMeasNWID = 0
                        Exit Function
                    Else
                        WriteMsg(OSCOPE, "SWE:TIME:RANG " & sTimeRange)
                    End If

                    'set trigger level
                    WriteMsg(OSCOPE, "TRIG:LEV " & sTriggerLevel)

                    'set niose rejection: OFF or ON
                    If (sHYST = "ON" Or sHYST = "OFF") Then
                        WriteMsg(OSCOPE, "TRIG:HYST " & sHYST)
                    Else
                        Echo("OSCOPE TRIGGER INPUT PARAMETER ERROR")
                        dMeasNWID = 0
                        Exit Function
                    End If

                    'set trigger slope: POS or NEG
                    If (sTriggerSlope = "POS" Or sTriggerSlope = "NEG") Then
                        WriteMsg(OSCOPE, "TRIG:SLOP " & sTriggerSlope)
                    Else
                        Echo("OSCOPE TRIGGER SLOPE PARAMETER ERROR")
                        dMeasNWID = 0
                        Exit Function
                    End If

                    'set trigger source
                    Select Case sTriggerSource
                        Case "INP1", "INP2", "EXT", "ECLT0", "ECLT1"
                            WriteMsg(OSCOPE, "TRIG:SOUR " & sTriggerSource)

                        Case Else
                            Echo("OSCOPE TRIGGER SOURCE PARAMETER ERROR")
                            dMeasNWID = 0
                            Exit Function
                    End Select

                Case Else
                    Echo("OSCOPE AUTO SCALE PARAMETER ERROR")
                    dMeasNWID = 0
                    Exit Function
            End Select
        End If

        'Make Mesurement
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        If Not bSimulation Then
            Select Case Left(UCase(APROBE), 1)
                Case "O" ' Off
                    WriteMsg(OSCOPE, "MEAS:VOLT:NWID? (@" & sINP & ")")
                    LongDelay((1))
                    nErr = ReadMsg(OSCOPE, sData)
                    WriteMsg(OSCOPE, "*CLS")
                    If nErr <> 0 Then
                        Echo("VISA ERROR: " & nErr)
                        dMeasNWID = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(nErr)
                        dMeasNWID = 0
                        Exit Function
                    End If
                    dMeasNWID = CDbl(sData)
                    gFrmMain.txtMeasured.Text = CStr(dMeasNWID)
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
                        '5/14/03 by Soon Nam''''''''''''''''''''''''''''''
                        'set auto scale with analog probe measurements
                        If (sAutoScale = "ON" Or sAutoScale = "AUTO") Then
                            WriteMsg(OSCOPE, "SYST:AUT")
                            LongDelay((1))
                        End If
                        'end add''''''''''''''''''''''''''''''''''''''''''
                        gFrmMain.lblStatus.Text = "Making Measurement ..."
                        WriteMsg(OSCOPE, "MEAS:VOLT:NWID? (@" & sINP & ")")
                        LongDelay((1))
                        nErr = ReadMsg(OSCOPE, sData)
                        WriteMsg(OSCOPE, "*CLS")
                        If nErr <> 0 Then
                            Echo("VISA ERROR: " & nErr)
                            dMeasNWID = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(nErr)
                            dMeasNWID = 0
                            Exit Function
                        End If
                        dMeasNWID = CDbl(sData)
                        gFrmMain.txtMeasured.Text = CStr(dMeasNWID)
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

                Case "C" ' Continuous
                    MisProbe = MsgBoxResult.Yes
                    Do While MisProbe = MsgBoxResult.Yes
                        UserEvent = 0
                        Failed = False
                        gFrmMain.TimerProbe.Enabled = True : gFrmMain.fraInstructions.Visible = True
                        Do While (UserEvent = 0) And (Not bProbeClosed)
                            '5/14/03 by Soon Nam''''''''''''''''''''''''''''''
                            'set auto scale with analog probe measurements
                            If (sAutoScale = "ON" Or sAutoScale = "AUTO") Then
                                WriteMsg(OSCOPE, "SYST:AUT")
                                LongDelay((1))
                            End If
                            'end add''''''''''''''''''''''''''''''''''''''''''
                            gFrmMain.lblStatus.Text = "Making Measurement ..."
                            WriteMsg(OSCOPE, "MEAS:VOLT:NWID? (@" & sINP & ")")
                            LongDelay((1))
                            nErr = ReadMsg(OSCOPE, sData)
                            WriteMsg(OSCOPE, "*CLS")
                            If nErr <> 0 Then
                                Echo("VISA ERROR: " & nErr)
                                dMeasNWID = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(nErr)
                                dMeasNWID = 0
                                Exit Function
                            End If
                            dMeasNWID = CDbl(sData)
                            gFrmMain.txtMeasured.Text = CStr(dMeasNWID)
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
            dMeasNWID = CDbl(InputBox("Command cmdOSCOPE.dMeasNWID peformed." & vbCrLf & "Enter Negative Pulse Width Value:", "SIMULATION MODE"))
            gFrmMain.txtMeasured.Text = CStr(dMeasNWID)
            If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON)
        End If
        gFrmMain.lblStatus.Text = sCurrentMsg

    End Function
	
    ''' <summary>
    ''' perform positive pulse width measurement
    ''' </summary>
    ''' <param name="sAutoScale">"ON" or "OFF"</param>
    ''' <param name="sINP">select INP1 or INP2 (OSCOPE input channel)</param>
    ''' <param name="dVoltRange">set the full scale vertical range;  min = 8mV, max = 40V, at reset = 4V</param>
    ''' <param name="dTimeRange">set the full scale horizontal range for sweep;  min = 10 ns, max = 50 s, at reset = 1 ms</param>
    ''' <param name="dTriggerLevel">set trigger level voltage (0V at reset)</param>
    ''' <param name="sTriggerSlope">set trigger slope ("POS" at reset)</param>
    ''' <param name="sTriggerSource">set trigger source ("INP1" at reset)</param>
    ''' <param name="nInputImp">select input impedance 50 or 1M (1M at reset)</param>
    ''' <param name="sInputCoupling">select input coupling for input channels ("DC" at reset)</param>
    ''' <param name="sLPAS">select low-pass filter ("OFF" at reset)</param>
    ''' <param name="sHPAS">select high-pass filter ("OFF" at reset)</param>
    ''' <param name="sHYST">set noise rejection ("OFF" at reset)</param>
    ''' <param name="APROBE">Indicates AProbe usage; "OFF", "Single" Measurement, or "Contiuous" Measurement</param>
    ''' <returns>Positive pulse width measurement</returns>
    ''' <remarks>sAutoScale: 
    '''             auto scale can be used to determine vertical and horizontal parameters when sAutoScale = "ON", optional parameters are ignored.  
    ''' Autoscale should only be used with relatively stable input signals having a duty cycle of greater than 0.5% and a frequency greater than 50Hz.
    ''' sINP:
    '''        At reset, INP1 is on and INP2 if off.  Max input voltage that can be applied to the two input connectors is 5 Vrms at 50 ohms 
    ''' or +/-250V at 1Mohms.</remarks>
    Public Function dMeasPWID(ByRef sAutoScale As String, ByRef sINP As String, Optional ByRef dVoltRange As Double = 4,
                              Optional ByRef dTimeRange As Double = 0.001, Optional ByRef dTriggerLevel As Double = 0,
                              Optional ByRef sTriggerSlope As String = "POS", Optional ByRef sTriggerSource As String = "INP1",
                              Optional ByRef nInputImp As Integer = 1000000, Optional ByRef sInputCoupling As String = "DC",
                              Optional ByRef sLPAS As String = "OFF", Optional ByRef sHPAS As String = "OFF",
                              Optional ByRef sHYST As String = "OFF", Optional ByRef APROBE As String = "OFF") As Double
        Dim sData As String = ""
        Dim nErr As Integer
        Dim sTimeRange As String
        Dim sVoltRange As String
        Dim sTriggerLevel As String
        Dim sInputImp As String
        Dim sCurrentMsg As String

        dMeasPWID = 0.0

        sCurrentMsg = gFrmMain.lblStatus.Text
        If APROBE <> "OFF" Then
            gFrmMain.lblStatus.Text = "APPLY PROBE"
        End If

        'convert data type to string type
        sInputImp = CStr(nInputImp)
        sTimeRange = CStr(dTimeRange)
        sVoltRange = CStr(dVoltRange)
        sTriggerLevel = CStr(dTriggerLevel)

        'convert string data to upper case
        sAutoScale = UCase(sAutoScale)
        sINP = UCase(sINP)
        sInputCoupling = UCase(sInputCoupling)
        sLPAS = UCase(sLPAS)
        sHPAS = UCase(sHPAS)
        sHYST = UCase(sHYST)
        sTriggerSlope = UCase(sTriggerSlope)
        sTriggerSource = UCase(sTriggerSource)

        'display measurement info to TPS shell
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        gFrmMain.txtInstrument.Text = "OSCOPE"
        gFrmMain.txtCommand.Text = "dMeasPWID"

        'Setup OSCOPE for duty cycle Measurement
        If Not bSimulation Then
            WriteMsg(OSCOPE, "SYST:LANG SCPI")

            LongDelay((1))
            WriteMsg(OSCOPE, "*CLS")
            WriteMsg(OSCOPE, "*RST")

            'select auto scale
            Select Case sAutoScale
                Case "ON", "AUTO"
                    WriteMsg(OSCOPE, "SYST:AUT")
                    LongDelay((2))

                Case "OFF" 'manually set vertical and horizontal ranges
                    'set input channel: INP1 or INP2
                    If (sINP = "INP1" Or sINP = "INP2") Then
                        WriteMsg(OSCOPE, sINP & " ON")
                    Else
                        Echo("OSCOPE INPUT CHANNEL SELECTION ERROR")
                        dMeasPWID = 0
                        Exit Function
                    End If

                    'set input coupling: DC or AC
                    If (sInputCoupling = "DC" Or sInputCoupling = "AC") Then
                        WriteMsg(OSCOPE, sINP & ":COUP " & sInputCoupling)
                    Else
                        Echo("OSCOPE INPUT COUPLING PARAMETER ERROR")
                        dMeasPWID = 0
                        Exit Function
                    End If

                    'set input impedance: 1000000 or 50
                    If (nInputImp = 50 Or nInputImp = 1000000) Then
                        WriteMsg(OSCOPE, sINP & ":IMP " & sInputImp)
                    Else
                        Echo("OSCOPE INPUT IMPEDANCE PARAMETER ERROR")
                        dMeasPWID = 0
                        Exit Function
                    End If

                    'set input low-pass filter state: OFF or ON
                    If (sLPAS = "ON" Or sLPAS = "OFF") Then
                        WriteMsg(OSCOPE, sINP & ":FILT:LPAS " & sLPAS)
                    Else
                        Echo("OSCOPE INPUT FILTER STATE PARAMETER ERROR")
                        dMeasPWID = 0
                        Exit Function
                    End If

                    'set input high-pass filter state: OFF or ON
                    If (sHPAS = "ON" Or sHPAS = "OFF") Then
                        WriteMsg(OSCOPE, sINP & ":FILT:hPAS " & sHPAS)
                    Else
                        Echo("OSCOPE INPUT FILTER STATE PARAMETER ERROR")
                        dMeasPWID = 0
                        Exit Function
                    End If

                    'set volt range: 8mV to 40V
                    If (dVoltRange < 0.008 Or dVoltRange > 40) Then
                        Echo("OSCOPE VOLT RANGE ERROR")
                        dMeasPWID = 0
                        Exit Function
                    Else
                        Select Case sINP
                            Case "INP1"
                                WriteMsg(OSCOPE, "VOLT1:RANG " & sVoltRange)
                            Case "INP2"
                                WriteMsg(OSCOPE, "VOLT2:RANG " & sVoltRange)
                        End Select
                    End If

                    'set time range:10ns to 50s
                    If (dTimeRange < 0.00000001 Or dTimeRange > 50) Then
                        Echo("OSCOPE TIME RANGE ERROR")
                        dMeasPWID = 0
                        Exit Function
                    Else
                        WriteMsg(OSCOPE, "SWE:TIME:RANG " & sTimeRange)
                    End If

                    'set trigger level
                    WriteMsg(OSCOPE, "TRIG:LEV " & sTriggerLevel)

                    'set niose rejection: OFF or ON
                    If (sHYST = "ON" Or sHYST = "OFF") Then
                        WriteMsg(OSCOPE, "TRIG:HYST " & sHYST)
                    Else
                        Echo("OSCOPE TRIGGER INPUT PARAMETER ERROR")
                        dMeasPWID = 0
                        Exit Function
                    End If

                    'set trigger slope: POS or NEG
                    If (sTriggerSlope = "POS" Or sTriggerSlope = "NEG") Then
                        WriteMsg(OSCOPE, "TRIG:SLOP " & sTriggerSlope)
                    Else
                        Echo("OSCOPE TRIGGER SLOPE PARAMETER ERROR")
                        dMeasPWID = 0
                        Exit Function
                    End If

                    'set trigger source
                    Select Case sTriggerSource
                        Case "INP1", "INP2", "EXT", "ECLT0", "ECLT1"
                            WriteMsg(OSCOPE, "TRIG:SOUR " & sTriggerSource)

                        Case Else
                            Echo("OSCOPE TRIGGER SOURCE PARAMETER ERROR")
                            dMeasPWID = 0
                            Exit Function
                    End Select

                Case Else
                    Echo("OSCOPE AUTO SCALE PARAMETER ERROR")
                    dMeasPWID = 0
                    Exit Function
            End Select
        End If

        'Make Mesurement
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        If Not bSimulation Then
            Select Case Left(UCase(APROBE), 1)
                Case "O" ' Off
                    WriteMsg(OSCOPE, "MEAS:VOLT:PWID? (@" & sINP & ")")
                    LongDelay((1))
                    nErr = ReadMsg(OSCOPE, sData)
                    WriteMsg(OSCOPE, "*CLS")
                    If nErr <> 0 Then
                        Echo("VISA ERROR: " & nErr)
                        dMeasPWID = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(nErr)
                        dMeasPWID = 0
                        Exit Function
                    End If
                    dMeasPWID = CDbl(sData)
                    gFrmMain.txtMeasured.Text = CStr(dMeasPWID)
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
                        '5/14/03 by Soon Nam''''''''''''''''''''''''''''''
                        'set auto scale with analog probe measurements
                        If (sAutoScale = "ON" Or sAutoScale = "AUTO") Then
                            WriteMsg(OSCOPE, "SYST:AUT")
                            LongDelay((1))
                        End If
                        'end add''''''''''''''''''''''''''''''''''''''''''
                        gFrmMain.lblStatus.Text = "Making Measurement ..."
                        WriteMsg(OSCOPE, "MEAS:VOLT:PWID? (@" & sINP & ")")
                        LongDelay((1))
                        nErr = ReadMsg(OSCOPE, sData)
                        WriteMsg(OSCOPE, "*CLS")
                        If nErr <> 0 Then
                            Echo("VISA ERROR: " & nErr)
                            dMeasPWID = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(nErr)
                            dMeasPWID = 0
                            Exit Function
                        End If
                        dMeasPWID = CDbl(sData)
                        gFrmMain.txtMeasured.Text = CStr(dMeasPWID)
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

                Case "C" ' Continuous
                    MisProbe = MsgBoxResult.Yes
                    Do While MisProbe = MsgBoxResult.Yes
                        UserEvent = 0
                        Failed = False
                        gFrmMain.TimerProbe.Enabled = True : gFrmMain.fraInstructions.Visible = True
                        Do While (UserEvent = 0) And (Not bProbeClosed)
                            '5/14/03 by Soon Nam''''''''''''''''''''''''''''''
                            'set auto scale with analog probe measurements
                            If (sAutoScale = "ON" Or sAutoScale = "AUTO") Then
                                WriteMsg(OSCOPE, "SYST:AUT")
                                LongDelay((1))
                            End If
                            'end add''''''''''''''''''''''''''''''''''''''''''
                            gFrmMain.lblStatus.Text = "Making Measurement ..."
                            WriteMsg(OSCOPE, "MEAS:VOLT:PWID? (@" & sINP & ")")
                            LongDelay((1))
                            nErr = ReadMsg(OSCOPE, sData)
                            WriteMsg(OSCOPE, "*CLS")
                            If nErr <> 0 Then
                                Echo("VISA ERROR: " & nErr)
                                dMeasPWID = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(nErr)
                                dMeasPWID = 0
                                Exit Function
                            End If
                            dMeasPWID = CDbl(sData)
                            gFrmMain.txtMeasured.Text = CStr(dMeasPWID)
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
            dMeasPWID = CDbl(InputBox("Command cmdOSCOPE.dMeasPWID peformed." & vbCrLf & "Enter Positive Pulse Width Value:", "SIMULATION MODE"))
            gFrmMain.txtMeasured.Text = CStr(dMeasPWID)
            If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON)
        End If

        gFrmMain.lblStatus.Text = sCurrentMsg
    End Function

    ''' <summary>
    ''' perform period measurement
    ''' </summary>
    ''' <param name="sAutoScale">"ON" or "OFF"</param>
    ''' <param name="sINP">select INP1 or INP2 (OSCOPE input channel)</param>
    ''' <param name="dVoltRange">set the full scale vertical range;  min = 8mV, max = 40V, at reset = 4V</param>
    ''' <param name="dTimeRange">set the full scale horizontal range for sweep;  min = 10 ns, max = 50 s, at reset = 1 ms</param>
    ''' <param name="dTriggerLevel">set trigger level voltage (0V at reset)</param>
    ''' <param name="sTriggerSlope">set trigger slope ("POS" at reset)</param>
    ''' <param name="sTriggerSource">set trigger source ("INP1" at reset)</param>
    ''' <param name="nInputImp">select input impedance 50 or 1M (1M at reset)</param>
    ''' <param name="sInputCoupling">select input coupling for input channels ("DC" at reset)</param>
    ''' <param name="sLPAS">select low-pass filter ("OFF" at reset)</param>
    ''' <param name="sHPAS">select high-pass filter ("OFF" at reset)</param>
    ''' <param name="sHYST">set noise rejection ("OFF" at reset)</param>
    ''' <param name="APROBE">Indicates AProbe usage; "OFF", "Single" Measurement, or "Contiuous" Measurement</param>
    ''' <returns>Measured period</returns>
    ''' <remarks>sAutoScale: 
    '''             auto scale can be used to determine vertical and horizontal parameters when sAutoScale = "ON", optional parameters are ignored.  
    ''' Autoscale should only be used with relatively stable input signals having a duty cycle of greater than 0.5% and a frequency greater than 50Hz.
    ''' sINP:
    '''        At reset, INP1 is on and INP2 if off.  Max input voltage that can be applied to the two input connectors is 5 Vrms at 50 ohms 
    ''' or +/-250V at 1Mohms.</remarks>
    Public Function dMeasPeriod(ByRef sAutoScale As String, ByRef sINP As String, Optional ByRef dVoltRange As Double = 4,
                                Optional ByRef dTimeRange As Double = 0.001, Optional ByRef dTriggerLevel As Double = 0,
                                Optional ByRef sTriggerSlope As String = "POS", Optional ByRef sTriggerSource As String = "INP1",
                                Optional ByRef nInputImp As Integer = 1000000, Optional ByRef sInputCoupling As String = "DC",
                                Optional ByRef sLPAS As String = "OFF", Optional ByRef sHPAS As String = "OFF",
                                Optional ByRef sHYST As String = "OFF", Optional ByRef APROBE As String = "OFF") As Double
        Dim sData As String = ""
        Dim nErr As Integer
        Dim sTimeRange As String
        Dim sVoltRange As String
        Dim sTriggerLevel As String
        Dim sInputImp As String
        Dim sCurrentMsg As String

        dMeasPeriod = 0.0

        sCurrentMsg = gFrmMain.lblStatus.Text
        If APROBE <> "OFF" Then
            gFrmMain.lblStatus.Text = "APPLY PROBE"
        End If

        'convert data type to string type
        sInputImp = CStr(nInputImp)
        sTimeRange = CStr(dTimeRange)
        sVoltRange = CStr(dVoltRange)
        sTriggerLevel = CStr(dTriggerLevel)

        'convert string data to upper case
        sAutoScale = UCase(sAutoScale)
        sINP = UCase(sINP)
        sInputCoupling = UCase(sInputCoupling)
        sLPAS = UCase(sLPAS)
        sHPAS = UCase(sHPAS)
        sHYST = UCase(sHYST)
        sTriggerSlope = UCase(sTriggerSlope)
        sTriggerSource = UCase(sTriggerSource)

        'display measurement info to TPS shell
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        gFrmMain.txtInstrument.Text = "OSCOPE"
        gFrmMain.txtCommand.Text = "dMeasPeriod"

        'Setup OSCOPE for duty cycle Measurement
        If Not bSimulation Then
            WriteMsg(OSCOPE, "SYST:LANG SCPI")

            LongDelay((1))
            WriteMsg(OSCOPE, "*CLS")
            WriteMsg(OSCOPE, "*RST")

            'select auto scale
            Select Case sAutoScale
                Case "ON", "AUTO"
                    WriteMsg(OSCOPE, "SYST:AUT")
                    LongDelay((2))

                Case "OFF" 'manually set vertical and horizontal ranges
                    'set input channel: INP1 or INP2
                    If (sINP = "INP1" Or sINP = "INP2") Then
                        WriteMsg(OSCOPE, sINP & " ON")
                    Else
                        Echo("OSCOPE INPUT CHANNEL SELECTION ERROR")
                        dMeasPeriod = 0
                        Exit Function
                    End If

                    'set input coupling: DC or AC
                    If (sInputCoupling = "DC" Or sInputCoupling = "AC") Then
                        WriteMsg(OSCOPE, sINP & ":COUP " & sInputCoupling)
                    Else
                        Echo("OSCOPE INPUT COUPLING PARAMETER ERROR")
                        dMeasPeriod = 0
                        Exit Function
                    End If

                    'set input impedance: 1000000 or 50
                    If (nInputImp = 50 Or nInputImp = 1000000) Then
                        WriteMsg(OSCOPE, sINP & ":IMP " & sInputImp)
                    Else
                        Echo("OSCOPE INPUT IMPEDANCE PARAMETER ERROR")
                        dMeasPeriod = 0
                        Exit Function
                    End If

                    'set input low-pass filter state: OFF or ON
                    If (sLPAS = "ON" Or sLPAS = "OFF") Then
                        WriteMsg(OSCOPE, sINP & ":FILT:LPAS " & sLPAS)
                    Else
                        Echo("OSCOPE INPUT FILTER STATE PARAMETER ERROR")
                        dMeasPeriod = 0
                        Exit Function
                    End If

                    'set input high-pass filter state: OFF or ON
                    If (sHPAS = "ON" Or sHPAS = "OFF") Then
                        WriteMsg(OSCOPE, sINP & ":FILT:hPAS " & sHPAS)
                    Else
                        Echo("OSCOPE INPUT FILTER STATE PARAMETER ERROR")
                        dMeasPeriod = 0
                        Exit Function
                    End If

                    'set volt range: 8mV to 40V
                    If (dVoltRange < 0.008 Or dVoltRange > 40) Then
                        Echo("OSCOPE VOLT RANGE ERROR")
                        dMeasPeriod = 0
                        Exit Function
                    Else
                        Select Case sINP
                            Case "INP1"
                                WriteMsg(OSCOPE, "VOLT1:RANG " & sVoltRange)
                            Case "INP2"
                                WriteMsg(OSCOPE, "VOLT2:RANG " & sVoltRange)
                        End Select
                    End If

                    'set time range:10ns to 50s
                    If (dTimeRange < 0.00000001 Or dTimeRange > 50) Then
                        Echo("OSCOPE TIME RANGE ERROR")
                        dMeasPeriod = 0
                        Exit Function
                    Else
                        WriteMsg(OSCOPE, "SWE:TIME:RANG " & sTimeRange)
                    End If

                    'set trigger level
                    WriteMsg(OSCOPE, "TRIG:LEV " & sTriggerLevel)

                    'set niose rejection: OFF or ON
                    If (sHYST = "ON" Or sHYST = "OFF") Then
                        WriteMsg(OSCOPE, "TRIG:HYST " & sHYST)
                    Else
                        Echo("OSCOPE TRIGGER INPUT PARAMETER ERROR")
                        dMeasPeriod = 0
                        Exit Function
                    End If

                    'set trigger slope: POS or NEG
                    If (sTriggerSlope = "POS" Or sTriggerSlope = "NEG") Then
                        WriteMsg(OSCOPE, "TRIG:SLOP " & sTriggerSlope)
                    Else
                        Echo("OSCOPE TRIGGER SLOPE PARAMETER ERROR")
                        dMeasPeriod = 0
                        Exit Function
                    End If

                    'set trigger source
                    Select Case sTriggerSource
                        Case "INP1", "INP2", "EXT", "ECLT0", "ECLT1"
                            WriteMsg(OSCOPE, "TRIG:SOUR " & sTriggerSource)

                        Case Else
                            Echo("OSCOPE TRIGGER SOURCE PARAMETER ERROR")
                            dMeasPeriod = 0
                            Exit Function
                    End Select

                Case Else
                    Echo("OSCOPE AUTO SCALE PARAMETER ERROR")
                    dMeasPeriod = 0
                    Exit Function
            End Select
        End If

        'Make Mesurement
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        If Not bSimulation Then
            Select Case Left(UCase(APROBE), 1)
                Case "O" ' Off
                    WriteMsg(OSCOPE, "MEAS:VOLT:PER? (@" & sINP & ")")
                    LongDelay((1))
                    nErr = ReadMsg(OSCOPE, sData)
                    WriteMsg(OSCOPE, "*CLS")
                    If nErr <> 0 Then
                        Echo("VISA ERROR: " & nErr)
                        dMeasPeriod = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(nErr)
                        dMeasPeriod = 0
                        Exit Function
                    End If
                    dMeasPeriod = CDbl(sData)
                    gFrmMain.txtMeasured.Text = CStr(dMeasPeriod)
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
                        '5/14/03 by Soon Nam''''''''''''''''''''''''''''''
                        'set auto scale with analog probe measurements
                        If (sAutoScale = "ON" Or sAutoScale = "AUTO") Then
                            WriteMsg(OSCOPE, "SYST:AUT")
                            LongDelay((1))
                        End If
                        'end add''''''''''''''''''''''''''''''''''''''''''
                        gFrmMain.lblStatus.Text = "Making Measurement ..."
                        WriteMsg(OSCOPE, "MEAS:VOLT:PER? (@" & sINP & ")")
                        LongDelay((1))
                        nErr = ReadMsg(OSCOPE, sData)
                        WriteMsg(OSCOPE, "*CLS")
                        If nErr <> 0 Then
                            Echo("VISA ERROR: " & nErr)
                            dMeasPeriod = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(nErr)
                            dMeasPeriod = 0
                            Exit Function
                        End If
                        dMeasPeriod = CDbl(sData)
                        gFrmMain.txtMeasured.Text = CStr(dMeasPeriod)
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

                Case "C" ' Continuous
                    MisProbe = MsgBoxResult.Yes
                    Do While MisProbe = MsgBoxResult.Yes
                        UserEvent = 0
                        Failed = False
                        gFrmMain.TimerProbe.Enabled = True : gFrmMain.fraInstructions.Visible = True
                        Do While (UserEvent = 0) And (Not bProbeClosed)
                            '5/14/03 by Soon Nam''''''''''''''''''''''''''''''
                            'set auto scale with analog probe measurements
                            If (sAutoScale = "ON" Or sAutoScale = "AUTO") Then
                                WriteMsg(OSCOPE, "SYST:AUT")
                                LongDelay((1))
                            End If
                            'end add''''''''''''''''''''''''''''''''''''''''''
                            gFrmMain.lblStatus.Text = "Making Measurement ..."
                            WriteMsg(OSCOPE, "MEAS:VOLT:PER? (@" & sINP & ")")
                            LongDelay((1))
                            nErr = ReadMsg(OSCOPE, sData)
                            WriteMsg(OSCOPE, "*CLS")
                            If nErr <> 0 Then
                                Echo("VISA ERROR: " & nErr)
                                dMeasPeriod = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(nErr)
                                dMeasPeriod = 0
                                Exit Function
                            End If
                            dMeasPeriod = CDbl(sData)
                            gFrmMain.txtMeasured.Text = CStr(dMeasPeriod)
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
            dMeasPeriod = CDbl(InputBox("Command cmdOSCOPE.dMeasPeriod peformed." & vbCrLf & "Enter Period Value:", "SIMULATION MODE"))
            gFrmMain.txtMeasured.Text = CStr(dMeasPeriod)
            If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON)
        End If

        gFrmMain.lblStatus.Text = sCurrentMsg
    End Function

    ''' <summary>
    ''' perform Amplitude voltage measurement
    ''' </summary>
    ''' <param name="sAutoScale">"ON" or "OFF"</param>
    ''' <param name="sINP">select INP1 or INP2 (OSCOPE input channel)</param>
    ''' <param name="dVoltRange">set the full scale vertical range;  min = 8mV, max = 40V, at reset = 4V</param>
    ''' <param name="dTimeRange">set the full scale horizontal range for sweep;  min = 10 ns, max = 50 s, at reset = 1 ms</param>
    ''' <param name="dTriggerLevel">set trigger level voltage (0V at reset)</param>
    ''' <param name="sTriggerSlope">set trigger slope ("POS" at reset)</param>
    ''' <param name="sTriggerSource">set trigger source ("INP1" at reset)</param>
    ''' <param name="nInputImp">select input impedance 50 or 1M (1M at reset)</param>
    ''' <param name="sInputCoupling">select input coupling for input channels ("DC" at reset)</param>
    ''' <param name="sLPAS">select low-pass filter ("OFF" at reset)</param>
    ''' <param name="sHPAS">select high-pass filter ("OFF" at reset)</param>
    ''' <param name="sHYST">set noise rejection ("OFF" at reset)</param>
    ''' <param name="APROBE">Indicates AProbe usage; "OFF", "Single" Measurement, or "Contiuous" Measurement</param>
    ''' <param name="dOffSet">voltage offset</param>
    ''' <returns>Measured amplitude</returns>
    ''' <remarks>sAutoScale: 
    '''             auto scale can be used to determine vertical and horizontal parameters when sAutoScale = "ON", optional parameters are ignored.  
    ''' Autoscale should only be used with relatively stable input signals having a duty cycle of greater than 0.5% and a frequency greater than 50Hz.
    ''' sINP:
    '''        At reset, INP1 is on and INP2 if off.  Max input voltage that can be applied to the two input connectors is 5 Vrms at 50 ohms 
    ''' or +/-250V at 1Mohms.</remarks>
    Public Function dMeasAmpl(ByRef sAutoScale As String, ByRef sINP As String, Optional ByRef dVoltRange As Double = 4,
                              Optional ByRef dTimeRange As Double = 0.001, Optional ByRef dTriggerLevel As Double = 0,
                              Optional ByRef sTriggerSlope As String = "POS", Optional ByRef sTriggerSource As String = "INP1",
                              Optional ByRef nInputImp As Integer = 1000000, Optional ByRef sInputCoupling As String = "DC",
                              Optional ByRef sLPAS As String = "OFF", Optional ByRef sHPAS As String = "OFF",
                              Optional ByRef sHYST As String = "OFF", Optional ByRef APROBE As String = "OFF",
                              Optional ByRef dOffSet As Double = 0) As Double
        Dim sData As String = ""
        Dim nErr As Integer
        Dim sTimeRange As String
        Dim sVoltRange As String
        Dim sOffset As String
        Dim sTriggerLevel As String
        Dim sInputImp As String
        Dim sCurrentMsg As String

        dMeasAmpl = 0.0

        sCurrentMsg = gFrmMain.lblStatus.Text
        If APROBE <> "OFF" Then
            gFrmMain.lblStatus.Text = "APPLY PROBE"
        End If

        'convert data type to string type
        sInputImp = CStr(nInputImp)
        sTimeRange = CStr(dTimeRange)
        sVoltRange = CStr(dVoltRange)
        sOffset = CStr(dOffSet)
        sTriggerLevel = CStr(dTriggerLevel)

        'convert string data to upper case
        sAutoScale = UCase(sAutoScale)
        sINP = UCase(sINP)
        sInputCoupling = UCase(sInputCoupling)
        sLPAS = UCase(sLPAS)
        sHPAS = UCase(sHPAS)
        sHYST = UCase(sHYST)
        sTriggerSlope = UCase(sTriggerSlope)
        sTriggerSource = UCase(sTriggerSource)

        'display measurement info to TPS shell
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        gFrmMain.txtInstrument.Text = "OSCOPE"
        gFrmMain.txtCommand.Text = "dMeasAmpl"

        'Setup OSCOPE for Amplitude Measurement
        If Not bSimulation Then
            WriteMsg(OSCOPE, "SYST:LANG SCPI")

            LongDelay((1))
            WriteMsg(OSCOPE, "*CLS")
            WriteMsg(OSCOPE, "*RST")

            'select auto scale
            Select Case sAutoScale
                Case "ON", "AUTO"
                    WriteMsg(OSCOPE, "SYST:AUT")
                    LongDelay((2))

                Case "OFF" 'manually set vertical and horizontal ranges
                    'set input channel: INP1 or INP2
                    If (sINP = "INP1" Or sINP = "INP2") Then
                        WriteMsg(OSCOPE, sINP & " ON")
                    Else
                        Echo("OSCOPE INPUT CHANNEL SELECTION ERROR")
                        dMeasAmpl = 0
                        Exit Function
                    End If

                    'set input impedance: 1000000 or 50
                    If (nInputImp = 50 Or nInputImp = 1000000) Then
                        WriteMsg(OSCOPE, sINP & ":IMP " & sInputImp)
                    Else
                        Echo("OSCOPE INPUT IMPEDANCE PARAMETER ERROR")
                        dMeasAmpl = 0
                        Exit Function
                    End If

                    'set input coupling: DC or AC
                    If (sInputCoupling = "DC" Or sInputCoupling = "AC") Then
                        WriteMsg(OSCOPE, sINP & ":COUP " & sInputCoupling)
                    Else
                        Echo("OSCOPE INPUT COUPLING PARAMETER ERROR")
                        dMeasAmpl = 0
                        Exit Function
                    End If

                    'set input low-pass filter state: OFF or ON
                    If (sLPAS = "ON" Or sLPAS = "OFF") Then
                        WriteMsg(OSCOPE, sINP & ":FILT:LPAS " & sLPAS)
                    Else
                        Echo("OSCOPE INPUT FILTER STATE PARAMETER ERROR")
                        dMeasAmpl = 0
                        Exit Function
                    End If

                    'set input high-pass filter state: OFF or ON
                    If (sHPAS = "ON" Or sHPAS = "OFF") Then
                        WriteMsg(OSCOPE, sINP & ":FILT:hPAS " & sHPAS)
                    Else
                        Echo("OSCOPE INPUT FILTER STATE PARAMETER ERROR")
                        dMeasAmpl = 0
                        Exit Function
                    End If

                    'set volt range: 8mV to 40V
                    If (dVoltRange < 0.008 Or dVoltRange > 40) Then
                        Echo("OSCOPE VOLT RANGE ERROR")
                        dMeasAmpl = 0
                        Exit Function
                    Else
                        Select Case sINP
                            Case "INP1"
                                WriteMsg(OSCOPE, "VOLT1:RANG " & sVoltRange)
                            Case "INP2"
                                WriteMsg(OSCOPE, "VOLT2:RANG " & sVoltRange)
                        End Select
                    End If

                    'set vertical range offset
                    If dOffSet <> 0 Then
                        If dVoltRange > 10 Then
                            If dOffSet < -250 Or dOffSet > 250 Then
                                Echo("OSCOPE dOffset range for dVoltRange of >10 V is +/-250 V")
                                dMeasAmpl = 0
                                Exit Function
                            Else
                                Select Case sINP
                                    Case "INP1"
                                        WriteMsg(OSCOPE, "VOLT1:RANG:OFFS " & sOffset)
                                    Case "INP2"
                                        WriteMsg(OSCOPE, "VOLT1:RANG:OFFS " & sOffset)
                                End Select
                            End If
                        ElseIf dVoltRange > 2 Then
                            If dOffSet < -50 Or dOffSet > 50 Then
                                Echo("O")
                                Echo("OSCOPE dOffset range for dVoltRange of >50 V is +/-50 V")
                                Exit Function
                            Else
                                Select Case sINP
                                    Case "INP1"
                                        WriteMsg(OSCOPE, "VOLT1:RANG:OFFS " & sOffset)
                                    Case "INP2"
                                        WriteMsg(OSCOPE, "VOLT1:RANG:OFFS " & sOffset)
                                End Select
                            End If
                        ElseIf dVoltRange > 0.4 Then
                            If dOffSet < -10 Or dOffSet > 10 Then
                                Echo("OSCOPE dOffset range for dVoltRange of >400 mV is +/-10 V")
                                dMeasAmpl = 0
                                Exit Function
                            Else
                                Select Case sINP
                                    Case "INP1"
                                        WriteMsg(OSCOPE, "VOLT1:RANG:OFFS " & sOffset)
                                    Case "INP2"
                                        WriteMsg(OSCOPE, "VOLT1:RANG:OFFS " & sOffset)
                                End Select
                            End If
                        Else 'If dVoltRange > 0.008 Then
                            If dOffSet < -2 Or dOffSet > 2 Then
                                Echo("OSCOPE dOffset range for dVoltRange of >8 mV is +/-2 V")
                                dMeasAmpl = 0
                                Exit Function
                            Else
                                Select Case sINP
                                    Case "INP1"
                                        WriteMsg(OSCOPE, "VOLT1:RANG:OFFS " & sOffset)
                                    Case "INP2"
                                        WriteMsg(OSCOPE, "VOLT1:RANG:OFFS " & sOffset)
                                End Select
                            End If
                        End If
                    End If

                    'set time range:10ns to 50s
                    If (dTimeRange < 0.00000001 Or dTimeRange > 50) Then
                        Echo("OSCOPE TIME RANGE ERROR")
                        dMeasAmpl = 0
                        Exit Function
                    Else
                        WriteMsg(OSCOPE, "SWE:TIME:RANG " & sTimeRange)
                    End If

                    'set trigger level
                    WriteMsg(OSCOPE, "TRIG:LEV " & sTriggerLevel)

                    'set niose rejection: OFF or ON
                    If (sHYST = "ON" Or sHYST = "OFF") Then
                        WriteMsg(OSCOPE, "TRIG:HYST " & sHYST)
                    Else
                        Echo("OSCOPE TRIGGER INPUT PARAMETER ERROR")
                        dMeasAmpl = 0
                        Exit Function
                    End If

                    'set trigger slope: POS or NEG
                    If (sTriggerSlope = "POS" Or sTriggerSlope = "NEG") Then
                        WriteMsg(OSCOPE, "TRIG:SLOP " & sTriggerSlope)
                    Else
                        Echo("OSCOPE TRIGGER SLOPE PARAMETER ERROR")
                        dMeasAmpl = 0
                        Exit Function
                    End If

                    'set trigger source
                    Select Case sTriggerSource
                        Case "INP1", "INP2", "EXT", "ECLT0", "ECLT1"
                            WriteMsg(OSCOPE, "TRIG:SOUR " & sTriggerSource)

                        Case Else
                            Echo("OSCOPE TRIGGER SOURCE PARAMETER ERROR")
                            dMeasAmpl = 0
                            Exit Function
                    End Select

                Case Else
                    Echo("OSCOPE AUTO SCALE PARAMETER ERROR")
                    dMeasAmpl = 0
                    Exit Function
            End Select
        End If

        'Make Mesurement
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        If Not bSimulation Then
            Select Case Left(UCase(APROBE), 1)
                Case "O" ' Off
                    WriteMsg(OSCOPE, "MEAS:VOLT:AMPL? (@" & sINP & ")")
                    LongDelay((1))
                    nErr = ReadMsg(OSCOPE, sData)
                    WriteMsg(OSCOPE, "*CLS")
                    If nErr <> 0 Then
                        Echo("VISA ERROR: " & nErr)
                        dMeasAmpl = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(nErr)
                        dMeasAmpl = 0
                        Exit Function
                    End If
                    dMeasAmpl = CDbl(sData)
                    gFrmMain.txtMeasured.Text = CStr(dMeasAmpl)
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
                        '5/14/03 by Soon Nam''''''''''''''''''''''''''''''
                        'set auto scale with analog probe measurements
                        If (sAutoScale = "ON" Or sAutoScale = "AUTO") Then
                            WriteMsg(OSCOPE, "SYST:AUT")
                            LongDelay((1))
                        End If
                        'end add''''''''''''''''''''''''''''''''''''''''''
                        gFrmMain.lblStatus.Text = "Making Measurement ..."
                        WriteMsg(OSCOPE, "MEAS:VOLT:AMPL? (@" & sINP & ")")
                        LongDelay((1))
                        nErr = ReadMsg(OSCOPE, sData)
                        WriteMsg(OSCOPE, "*CLS")
                        If nErr <> 0 Then
                            Echo("VISA ERROR: " & nErr)
                            dMeasAmpl = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(nErr)
                            dMeasAmpl = 0
                            Exit Function
                        End If
                        dMeasAmpl = CDbl(sData)
                        gFrmMain.txtMeasured.Text = CStr(dMeasAmpl)
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

                Case "C" ' Continuous
                    MisProbe = MsgBoxResult.Yes
                    Do While MisProbe = MsgBoxResult.Yes
                        UserEvent = 0
                        Failed = False
                        gFrmMain.TimerProbe.Enabled = True : gFrmMain.fraInstructions.Visible = True
                        Do While (UserEvent = 0) And (Not bProbeClosed)
                            '5/14/03 by Soon Nam''''''''''''''''''''''''''''''
                            'set auto scale with analog probe measurements
                            If (sAutoScale = "ON" Or sAutoScale = "AUTO") Then
                                WriteMsg(OSCOPE, "SYST:AUT")
                                LongDelay((1))
                            End If
                            'end add''''''''''''''''''''''''''''''''''''''''''
                            gFrmMain.lblStatus.Text = "Making Measurement ..."
                            WriteMsg(OSCOPE, "MEAS:VOLT:AMPL? (@" & sINP & ")")
                            LongDelay((1))
                            nErr = ReadMsg(OSCOPE, sData)
                            WriteMsg(OSCOPE, "*CLS")
                            If nErr <> 0 Then
                                Echo("VISA ERROR: " & nErr)
                                dMeasAmpl = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(nErr)
                                dMeasAmpl = 0
                                Exit Function
                            End If
                            dMeasAmpl = CDbl(sData)
                            gFrmMain.txtMeasured.Text = CStr(dMeasAmpl)
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
            dMeasAmpl = CDbl(InputBox("Command cmdOSCOPE.dMeasAmpl peformed." & vbCrLf & "Enter Amplitude Value:", "SIMULATION MODE"))
            gFrmMain.txtMeasured.Text = CStr(dMeasAmpl)
            If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON)
        End If

        gFrmMain.lblStatus.Text = sCurrentMsg
    End Function
	
    ''' <summary>
    ''' perform AC RMS voltage measurement (with 0 volts as the reference)
    ''' </summary>
    ''' <param name="sAutoScale">"ON" or "OFF"</param>
    ''' <param name="sINP">select INP1 or INP2 (OSCOPE input channel)</param>
    ''' <param name="dVoltRange">set the full scale vertical range;  min = 8mV, max = 40V, at reset = 4V</param>
    ''' <param name="dTimeRange">set the full scale horizontal range for sweep;  min = 10 ns, max = 50 s, at reset = 1 ms</param>
    ''' <param name="dTriggerLevel">set trigger level voltage (0V at reset)</param>
    ''' <param name="sTriggerSlope">set trigger slope ("POS" at reset)</param>
    ''' <param name="sTriggerSource">set trigger source ("INP1" at reset)</param>
    ''' <param name="nInputImp">select input impedance 50 or 1M (1M at reset)</param>
    ''' <param name="sInputCoupling">select input coupling for input channels ("DC" at reset)</param>
    ''' <param name="sLPAS">select low-pass filter ("OFF" at reset)</param>
    ''' <param name="sHPAS">select high-pass filter ("OFF" at reset)</param>
    ''' <param name="sHYST">set noise rejection ("OFF" at reset)</param>
    ''' <param name="APROBE">Indicates AProbe usage; "OFF", "Single" Measurement, or "Contiuous" Measurement</param>
    ''' <returns>AC RMS voltage measurement</returns>
    ''' <remarks>sAutoScale: 
    '''             auto scale can be used to determine vertical and horizontal parameters when sAutoScale = "ON", optional parameters are ignored.  
    ''' Autoscale should only be used with relatively stable input signals having a duty cycle of greater than 0.5% and a frequency greater than 50Hz.
    ''' sINP:
    '''        At reset, INP1 is on and INP2 if off.  Max input voltage that can be applied to the two input connectors is 5 Vrms at 50 ohms 
    ''' or +/-250V at 1Mohms.</remarks>
    Public Function dMeasAC(ByRef sAutoScale As String, ByRef sINP As String, Optional ByRef dVoltRange As Double = 4,
                            Optional ByRef dTimeRange As Double = 0.001, Optional ByRef dTriggerLevel As Double = 0,
                            Optional ByRef sTriggerSlope As String = "POS", Optional ByRef sTriggerSource As String = "INP1",
                            Optional ByRef nInputImp As Integer = 1000000, Optional ByRef sInputCoupling As String = "DC",
                            Optional ByRef sLPAS As String = "OFF", Optional ByRef sHPAS As String = "OFF",
                            Optional ByRef sHYST As String = "OFF", Optional ByRef APROBE As String = "OFF") As Double
        Dim sData As String = ""
        Dim nErr As Integer
        Dim sTimeRange As String
        Dim sVoltRange As String
        Dim sTriggerLevel As String
        Dim sInputImp As String
        Dim sCurrentMsg As String

        dMeasAC = 0.0

        sCurrentMsg = gFrmMain.lblStatus.Text
        If APROBE <> "OFF" Then
            gFrmMain.lblStatus.Text = "APPLY PROBE"
        End If

        'convert data type to string type
        sInputImp = CStr(nInputImp)
        sTimeRange = CStr(dTimeRange)
        sVoltRange = CStr(dVoltRange)
        sTriggerLevel = CStr(dTriggerLevel)

        'convert string data to upper case
        sAutoScale = UCase(sAutoScale)
        sINP = UCase(sINP)
        sInputCoupling = UCase(sInputCoupling)
        sLPAS = UCase(sLPAS)
        sHPAS = UCase(sHPAS)
        sHYST = UCase(sHYST)
        sTriggerSlope = UCase(sTriggerSlope)
        sTriggerSource = UCase(sTriggerSource)

        'display measurement info to TPS shell
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        gFrmMain.txtInstrument.Text = "OSCOPE"
        gFrmMain.txtCommand.Text = "dMeasAC"

        'Setup OSCOPE for AC RMS voltage Measurement
        If Not bSimulation Then
            WriteMsg(OSCOPE, "SYST:LANG SCPI")

            LongDelay((1))
            WriteMsg(OSCOPE, "*CLS")
            WriteMsg(OSCOPE, "*RST")

            'select auto scale
            Select Case sAutoScale
                Case "ON", "AUTO"
                    WriteMsg(OSCOPE, "SYST:AUT")
                    LongDelay((2))

                Case "OFF" 'manually set vertical and horizontal ranges
                    'set input channel: INP1 or INP2
                    If (sINP = "INP1" Or sINP = "INP2") Then
                        WriteMsg(OSCOPE, sINP & " ON")
                    Else
                        Echo("OSCOPE INPUT CHANNEL SELECTION ERROR")
                        dMeasAC = 0
                        Exit Function
                    End If

                    'set input coupling: DC or AC
                    If (sInputCoupling = "DC" Or sInputCoupling = "AC") Then
                        WriteMsg(OSCOPE, sINP & ":COUP " & sInputCoupling)
                    Else
                        Echo("OSCOPE INPUT COUPLING PARAMETER ERROR")
                        dMeasAC = 0
                        Exit Function
                    End If

                    'set input impedance: 1000000 or 50
                    If (nInputImp = 50 Or nInputImp = 1000000) Then
                        WriteMsg(OSCOPE, sINP & ":IMP " & sInputImp)
                    Else
                        Echo("OSCOPE INPUT IMPEDANCE PARAMETER ERROR")
                        dMeasAC = 0
                        Exit Function
                    End If

                    'set input low-pass filter state: OFF or ON
                    If (sLPAS = "ON" Or sLPAS = "OFF") Then
                        WriteMsg(OSCOPE, sINP & ":FILT:LPAS " & sLPAS)
                    Else
                        Echo("OSCOPE INPUT FILTER STATE PARAMETER ERROR")
                        dMeasAC = 0
                        Exit Function
                    End If

                    'set input high-pass filter state: OFF or ON
                    If (sHPAS = "ON" Or sHPAS = "OFF") Then
                        WriteMsg(OSCOPE, sINP & ":FILT:hPAS " & sHPAS)
                    Else
                        Echo("OSCOPE INPUT FILTER STATE PARAMETER ERROR")
                        dMeasAC = 0
                        Exit Function
                    End If

                    'set volt range: 8mV to 40V
                    If (dVoltRange < 0.008 Or dVoltRange > 40) Then
                        Echo("OSCOPE VOLT RANGE ERROR")
                        dMeasAC = 0
                        Exit Function
                    Else
                        Select Case sINP
                            Case "INP1"
                                WriteMsg(OSCOPE, "VOLT1:RANG " & sVoltRange)
                            Case "INP2"
                                WriteMsg(OSCOPE, "VOLT2:RANG " & sVoltRange)
                        End Select
                    End If

                    'set time range:10ns to 50s
                    If (dTimeRange < 0.00000001 Or dTimeRange > 50) Then
                        Echo("OSCOPE TIME RANGE ERROR")
                        dMeasAC = 0
                        Exit Function
                    Else
                        WriteMsg(OSCOPE, "SWE:TIME:RANG " & sTimeRange)
                    End If

                    'set trigger level
                    WriteMsg(OSCOPE, "TRIG:LEV " & sTriggerLevel)

                    'set niose rejection: OFF or ON
                    If (sHYST = "ON" Or sHYST = "OFF") Then
                        WriteMsg(OSCOPE, "TRIG:HYST " & sHYST)
                    Else
                        Echo("OSCOPE TRIGGER INPUT PARAMETER ERROR")
                        dMeasAC = 0
                        Exit Function
                    End If

                    'set trigger slope: POS or NEG
                    If (sTriggerSlope = "POS" Or sTriggerSlope = "NEG") Then
                        WriteMsg(OSCOPE, "TRIG:SLOP " & sTriggerSlope)
                    Else
                        Echo("OSCOPE TRIGGER SLOPE PARAMETER ERROR")
                        dMeasAC = 0
                        Exit Function
                    End If

                    'set trigger source
                    Select Case sTriggerSource
                        Case "INP1", "INP2", "EXT", "ECLT0", "ECLT1"
                            WriteMsg(OSCOPE, "TRIG:SOUR " & sTriggerSource)

                        Case Else
                            Echo("OSCOPE TRIGGER SOURCE PARAMETER ERROR")
                            dMeasAC = 0
                            Exit Function
                    End Select

                Case Else
                    Echo("OSCOPE AUTO SCALE PARAMETER ERROR")
                    dMeasAC = 0
                    Exit Function
            End Select
        End If

        'Make Mesurement
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        If Not bSimulation Then
            Select Case Left(UCase(APROBE), 1)
                Case "O" ' Off
                    WriteMsg(OSCOPE, "MEAS:VOLT:AC? (@" & sINP & ")")
                    LongDelay((1))
                    nErr = ReadMsg(OSCOPE, sData)
                    WriteMsg(OSCOPE, "*CLS")
                    If nErr <> 0 Then
                        Echo("VISA ERROR: " & nErr)
                        dMeasAC = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(nErr)
                        dMeasAC = 0
                        Exit Function
                    End If
                    dMeasAC = CDbl(sData)
                    gFrmMain.txtMeasured.Text = CStr(dMeasAC)
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
                        '5/14/03 by Soon Nam''''''''''''''''''''''''''''''
                        'set auto scale with analog probe measurements
                        If (sAutoScale = "ON" Or sAutoScale = "AUTO") Then
                            WriteMsg(OSCOPE, "SYST:AUT")
                            LongDelay((1))
                        End If
                        'end add''''''''''''''''''''''''''''''''''''''''''
                        gFrmMain.lblStatus.Text = "Making Measurement ..."
                        WriteMsg(OSCOPE, "MEAS:VOLT:AC? (@" & sINP & ")")
                        LongDelay((1))
                        nErr = ReadMsg(OSCOPE, sData)
                        WriteMsg(OSCOPE, "*CLS")
                        If nErr <> 0 Then
                            Echo("VISA ERROR: " & nErr)
                            dMeasAC = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(nErr)
                            dMeasAC = 0
                            Exit Function
                        End If
                        dMeasAC = CDbl(sData)
                        gFrmMain.txtMeasured.Text = CStr(dMeasAC)
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

                Case "C" ' Continuous
                    MisProbe = MsgBoxResult.Yes
                    Do While MisProbe = MsgBoxResult.Yes
                        UserEvent = 0
                        Failed = False
                        gFrmMain.TimerProbe.Enabled = True : gFrmMain.fraInstructions.Visible = True
                        Do While (UserEvent = 0) And (Not bProbeClosed)
                            '5/14/03 by Soon Nam''''''''''''''''''''''''''''''
                            'set auto scale with analog probe measurements
                            If (sAutoScale = "ON" Or sAutoScale = "AUTO") Then
                                WriteMsg(OSCOPE, "SYST:AUT")
                                LongDelay((1))
                            End If
                            'end add''''''''''''''''''''''''''''''''''''''''''
                            gFrmMain.lblStatus.Text = "Making Measurement ..."
                            WriteMsg(OSCOPE, "MEAS:VOLT:AC? (@" & sINP & ")")
                            LongDelay((1))
                            nErr = ReadMsg(OSCOPE, sData)
                            WriteMsg(OSCOPE, "*CLS")
                            If nErr <> 0 Then
                                Echo("VISA ERROR: " & nErr)
                                dMeasAC = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(nErr)
                                dMeasAC = 0
                                Exit Function
                            End If
                            dMeasAC = CDbl(sData)
                            gFrmMain.txtMeasured.Text = CStr(dMeasAC)
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
            dMeasAC = CDbl(InputBox("Command cmdOSCOPE.dMeasAC peformed." & vbCrLf & "Enter AC Voltage Value:", "SIMULATION MODE"))
            gFrmMain.txtMeasured.Text = CStr(dMeasAC)
            If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON)
        End If

        gFrmMain.lblStatus.Text = sCurrentMsg
    End Function

    ''' <summary>
    ''' perform DC voltage measurement (with 0 volts as the reference)
    ''' </summary>
    ''' <param name="sAutoScale">"ON" or "OFF"</param>
    ''' <param name="sINP">select INP1 or INP2 (OSCOPE input channel)</param>
    ''' <param name="dVoltRange">set the full scale vertical range;  min = 8mV, max = 40V, at reset = 4V</param>
    ''' <param name="dTimeRange">set the full scale horizontal range for sweep;  min = 10 ns, max = 50 s, at reset = 1 ms</param>
    ''' <param name="nInputImp">select input impedance 50 or 1M (1M at reset)</param>
    ''' <param name="APROBE">Indicates AProbe usage; "OFF", "Single" Measurement, or "Contiuous" Measurement</param>
    ''' <returns>Negative pulse width measurement</returns>
    ''' <remarks>sAutoScale: 
    '''             auto scale can be used to determine vertical and horizontal parameters when sAutoScale = "ON", optional parameters are ignored.  
    ''' Autoscale should only be used with relatively stable input signals having a duty cycle of greater than 0.5% and a frequency greater than 50Hz.
    ''' sINP:
    '''        At reset, INP1 is on and INP2 if off.  Max input voltage that can be applied to the two input connectors is 5 Vrms at 50 ohms 
    ''' or +/-250V at 1Mohms.</remarks>
    Public Function dMeasDC(ByRef sAutoScale As String, ByRef sINP As String, Optional ByRef dVoltRange As Double = 4,
                            Optional ByRef dTimeRange As Double = 0.001, Optional ByRef nInputImp As Integer = 1000000,
                            Optional ByRef APROBE As String = "OFF") As Double
        Dim sData As String = ""
        Dim nErr As Integer
        Dim sTimeRange As String
        Dim sVoltRange As String
        Dim sInputImp As String
        Dim sCurrentMsg As String

        dMeasDC = 0.0

        sCurrentMsg = gFrmMain.lblStatus.Text
        If APROBE <> "OFF" Then
            gFrmMain.lblStatus.Text = "APPLY PROBE"
        End If

        'convert data type to string type
        sInputImp = CStr(nInputImp)
        sTimeRange = CStr(dTimeRange)
        sVoltRange = CStr(dVoltRange)

        'convert string data to upper case
        sAutoScale = UCase(sAutoScale)
        sINP = UCase(sINP)

        'display measurement info to TPS shell
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        gFrmMain.txtInstrument.Text = "OSCOPE"
        gFrmMain.txtCommand.Text = "dMeasDC"

        'Setup OSCOPE for DC voltage Measurement
        If Not bSimulation Then
            WriteMsg(OSCOPE, "SYST:LANG SCPI")

            LongDelay((1))
            WriteMsg(OSCOPE, "*CLS")
            WriteMsg(OSCOPE, "*RST")

            'select auto scale
            Select Case sAutoScale
                Case "ON", "AUTO"
                    WriteMsg(OSCOPE, "SYST:AUT")
                    LongDelay((2))

                Case "OFF" 'manually set vertical and horizontal ranges
                    'set input channel: INP1 or INP2
                    If (sINP = "INP1" Or sINP = "INP2") Then
                        WriteMsg(OSCOPE, sINP & " ON")
                    Else
                        Echo("OSCOPE INPUT CHANNEL SELECTION ERROR")
                        dMeasDC = 0
                        Exit Function
                    End If

                    'set input impedance: 1000000 or 50
                    If (nInputImp = 50 Or nInputImp = 1000000) Then
                        WriteMsg(OSCOPE, sINP & ":IMP " & sInputImp)
                    Else
                        Echo("OSCOPE INPUT IMPEDANCE PARAMETER ERROR")
                        dMeasDC = 0
                        Exit Function
                    End If

                    'set volt range: 8mV to 40V
                    If (dVoltRange < 0.008 Or dVoltRange > 40) Then
                        Echo("OSCOPE VOLT RANGE ERROR")
                        dMeasDC = 0
                        Exit Function
                    Else
                        Select Case sINP
                            Case "INP1"
                                WriteMsg(OSCOPE, "VOLT1:RANG " & sVoltRange)
                            Case "INP2"
                                WriteMsg(OSCOPE, "VOLT2:RANG " & sVoltRange)
                        End Select
                    End If

                    'set time range:10ns to 50s
                    If (dTimeRange < 0.00000001 Or dTimeRange > 50) Then
                        Echo("OSCOPE TIME RANGE ERROR")
                        dMeasDC = 0
                        Exit Function
                    Else
                        WriteMsg(OSCOPE, "SWE:TIME:RANG " & sTimeRange)
                    End If

                Case Else
                    Echo("OSCOPE AUTO SCALE PARAMETER ERROR")
                    dMeasDC = 0
                    Exit Function
            End Select
        End If

        'Make Mesurement
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        If Not bSimulation Then
            Select Case Left(UCase(APROBE), 1)
                Case "O" ' Off
                    WriteMsg(OSCOPE, "MEAS:VOLT? (@" & sINP & ")")
                    LongDelay((1))
                    nErr = ReadMsg(OSCOPE, sData)
                    WriteMsg(OSCOPE, "*CLS")
                    If nErr <> 0 Then
                        Echo("VISA ERROR: " & nErr)
                        dMeasDC = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(nErr)
                        dMeasDC = 0
                        Exit Function
                    End If
                    dMeasDC = CDbl(sData)
                    gFrmMain.txtMeasured.Text = CStr(dMeasDC)
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
                        '5/14/03 by Soon Nam''''''''''''''''''''''''''''''
                        'set auto scale with analog probe measurements
                        If (sAutoScale = "ON" Or sAutoScale = "AUTO") Then
                            WriteMsg(OSCOPE, "SYST:AUT")
                            LongDelay((1))
                        End If
                        'end add''''''''''''''''''''''''''''''''''''''''''
                        gFrmMain.lblStatus.Text = "Making Measurement ..."
                        WriteMsg(OSCOPE, "MEAS:VOLT? (@" & sINP & ")")
                        LongDelay((1))
                        nErr = ReadMsg(OSCOPE, sData)
                        WriteMsg(OSCOPE, "*CLS")
                        If nErr <> 0 Then
                            Echo("VISA ERROR: " & nErr)
                            dMeasDC = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(nErr)
                            dMeasDC = 0
                            Exit Function
                        End If
                        dMeasDC = CDbl(sData)
                        gFrmMain.txtMeasured.Text = CStr(dMeasDC)
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

                Case "C" ' Continuous
                    MisProbe = MsgBoxResult.Yes
                    Do While MisProbe = MsgBoxResult.Yes
                        UserEvent = 0
                        Failed = False
                        gFrmMain.TimerProbe.Enabled = True : gFrmMain.fraInstructions.Visible = True
                        Do While (UserEvent = 0) And (Not bProbeClosed)
                            '5/14/03 by Soon Nam''''''''''''''''''''''''''''''
                            'set auto scale with analog probe measurements
                            If (sAutoScale = "ON" Or sAutoScale = "AUTO") Then
                                WriteMsg(OSCOPE, "SYST:AUT")
                                LongDelay((1))
                            End If
                            'end add''''''''''''''''''''''''''''''''''''''''''
                            gFrmMain.lblStatus.Text = "Making Measurement ..."
                            WriteMsg(OSCOPE, "MEAS:VOLT? (@" & sINP & ")")
                            LongDelay((1))
                            nErr = ReadMsg(OSCOPE, sData)
                            WriteMsg(OSCOPE, "*CLS")
                            If nErr <> 0 Then
                                Echo("VISA ERROR: " & nErr)
                                dMeasDC = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(nErr)
                                dMeasDC = 0
                                Exit Function
                            End If
                            dMeasDC = CDbl(sData)
                            gFrmMain.txtMeasured.Text = CStr(dMeasDC)
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
            dMeasDC = CDbl(InputBox("Command cmdOSCOPE.dMeasDC peformed." & vbCrLf & "Enter DC Voltage Value:", "SIMULATION MODE"))
            gFrmMain.txtMeasured.Text = CStr(dMeasDC)
            If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON)
        End If

        gFrmMain.lblStatus.Text = sCurrentMsg
    End Function
End Class