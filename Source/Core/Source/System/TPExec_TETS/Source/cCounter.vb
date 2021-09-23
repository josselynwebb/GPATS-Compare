Option Strict Off
Option Explicit On
<System.Runtime.InteropServices.ProgId("cCounter_NET.cCounter")> Public Class cCounter

    Dim dAperature As Double ' bbbbb find out why this is needed
    Dim dRange As Double ' bbbbb find out why this is needed

    Public Sub SendMsg(ByRef sMsg As String)
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        gFrmMain.txtInstrument.Text = "COUNTER"
        gFrmMain.txtCommand.Text = "SendMsg"
        WriteMsg(COUNTER, sMsg)
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
    End Sub

    Public Function sReadMsg(ByRef sMsg As String) As String
        sReadMsg = ""
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        gFrmMain.txtInstrument.Text = "COUNTER"
        gFrmMain.txtCommand.Text = "ReadMsg"
        ReadMsg(COUNTER, sReadMsg)
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
    End Function

    Public Function dMeasFetch(Optional ByRef nChannel As Short = 1) As Double
        Dim sData As String = ""
        Dim nErr As Integer
        Dim Q As String
        Q = Chr(34)

        dMeasFetch = 0.0
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        gFrmMain.txtInstrument.Text = "Counter"
        gFrmMain.txtCommand.Text = "dMeasFetch"

        'Error Check Arguments
        If nChannel < 1 Or nChannel > 2 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasFreq nChannel argument out of range.")
            Echo("RANGE: 1 TO 2")
            dMeasFetch = 9.9E+37 'bbbb was dMeasFretch
            Err.Raise(-1001)
            Exit Function
        End If

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function

        If Not bSimulation Then
            WriteMsg(COUNTER, "FETCH" & CStr(nChannel) & "?")
            nErr = ReadMsg(COUNTER, sData)
            If nErr <> 0 Then
                dMeasFetch = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37)
                Err.Raise(nErr)
                Exit Function
            End If
            dMeasFetch = CDbl(sData)
            gFrmMain.txtMeasured.Text = CStr(CSng(dMeasFetch))
            If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        Else
            dMeasFetch = CDbl(InputBox("Command cmdCOUNTER.dMeasFetch peformed." & vbCrLf & "Enter Value:", "SIMULATION MODE"))
            gFrmMain.txtMeasured.Text = CStr(CSng(dMeasFetch))
            If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON)
        End If
    End Function

    Public Function dMeasFallTime(Optional ByRef dRange As Double = 0.0000001, Optional ByRef bChOne10XAttenuation As Boolean = False,
                                  Optional ByRef bChTwo10XAttenuation As Boolean = False, Optional ByRef bChOneACCoupling As Boolean = False,
                                  Optional ByRef bChTwoACCoupling As Boolean = False, Optional ByRef bChOne50ohms As Boolean = False,
                                  Optional ByRef bChTwo50ohms As Boolean = False, Optional ByRef sHysteresis As String = "DEF",
                                  Optional ByRef bAutoTrigger As Boolean = False, Optional ByRef nRelativeTriggerLevel As Integer = 50,
                                  Optional ByRef bTriggerSlopeNegative As Boolean = False, Optional ByRef dAbsoluteTrigger As Double = 0,
                                  Optional ByRef bTriggerOutputOn As Boolean = False, Optional ByRef nTTLTriggerOutput As Integer = 0,
                                  Optional ByRef sArmStartSource As String = "IMMEDIATE", Optional ByRef bArmStartSlopNegative As Boolean = False,
                                  Optional ByRef sArmStartLevel As String = "TTL", Optional ByRef SArmStopSource As String = "IMMEDIATE",
                                  Optional ByRef bArmStopSlopNegative As Boolean = False, Optional ByRef sArmStopLevel As String = "TTL",
                                  Optional ByRef sTimeBaseSource As String = "INTERNAL", Optional ByRef bOutputTimeBase As Boolean = False,
                                  Optional ByRef bTotalizeGateMeasure As Boolean = False,
                                  Optional ByRef bTotalizeGatePolarityInverted As Boolean = False, Optional ByRef bTimeIntervalDelay As Boolean = False,
                                  Optional ByRef dTimeIntervalDelay As Double = 0.1, Optional ByRef sAcquisitionTimeOutSetting As String = "ON",
                                  Optional ByRef dAcquisitionTimeOut As Double = 5.0, Optional ByRef APROBE As String = "OFF") As Double
        Dim i As Short
        Dim sData As String = ""
        Dim nErr As Integer
        Dim sCommand As String
        Dim Q As String
        Dim nChannel As Short
        Dim sCurrentMsg As String = ""

        dMeasFallTime = 0.0
        nChannel = 1
        Q = Chr(34)

        Dim iHandle As Integer
        iHandle = nInstrumentHandle(COUNTER)

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        gFrmMain.txtInstrument.Text = "Counter"
        gFrmMain.txtCommand.Text = "dMeasFallTime"
        If APROBE <> "OFF" Then
            sCurrentMsg = gFrmMain.lblStatus.Text
            gFrmMain.lblStatus.Text = "APPLY PROBE"
        End If

        'Clear Counter
        If Not bSimulation Then
            '      viClear (nInstrumentHandle(COUNTER))
            nSystErr = atxmlDF_viClear(ResourceName(COUNTER), 0)
            WriteMsg(COUNTER, "*RST ; *CLS")
        End If

        'Error Check Arguments
        If nChannel < 1 Or nChannel > 2 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasFallTime nChannel argument out of range.")
            Echo("RANGE: 1 TO 2")
            dMeasFallTime = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
            Exit Function
        End If

        If dRange < 0.000000015 Or dRange > 0.001 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasFallTime dRange argument out of range.")
            Echo("RANGE: 15E-9 TO 1E-3")
            dMeasFallTime = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
            Exit Function
        End If

        Select Case UCase(sHysteresis)
            Case "DEF", "MAX", "MIN"
            Case Else
                Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasFallTime sHysteresis argument out of range.")
                Echo("RANGE: DEF, MAX or MIN")
                dMeasFallTime = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                Exit Function
        End Select

        If nRelativeTriggerLevel < 10 Or nRelativeTriggerLevel > 90 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasFallTime nRelativeTriggerLevel argument out of range.")
            Echo("RANGE: 10 to 90")
            dMeasFallTime = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
            Exit Function
        End If

        If dAbsoluteTrigger < -10.238 Or dAbsoluteTrigger > 10.238 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasFallTime dAbsoluteTrigger argument out of range.")
            Echo("RANGE: -10.238 to 10.238")
            dMeasFallTime = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
            Exit Function
        End If

        If nTTLTriggerOutput < 0 Or nTTLTriggerOutput > 7 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasFallTime nTTLTriggerOutput argument out of range.")
            Echo("RANGE: 0 to 7")
            dMeasFallTime = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
            Exit Function
        End If

        Select Case UCase(sArmStartSource)
            Case "IMMEDIATE", "VXI"
            Case "TTL0", "TTL1", "TTL2", "TTL3", "TTL4", "TTL5", "TTL6", "TTL7"
            Case "EXTERNAL"
                Select Case UCase(sArmStartLevel)
                    Case "TTL", "ECL", "GND"
                    Case Else
                        Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasFallTime sArmStartLevel argument out of range.")
                        Echo("RANGE: TTL, ECL or GND")
                        dMeasFallTime = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                End Select
            Case Else
                Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasFallTime sArmStartSource argument out of range.")
                Echo("RANGE: IMMEDIATE, VXI, TTL0, TTL1, TTL2, TTL3, TTL4, TTL5, TTL6, TTL7 or EXTERNAL")
                dMeasFallTime = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                Exit Function
        End Select

        Select Case UCase(SArmStopSource)
            Case "IMMEDIATE", "VXI"
            Case "TTL0", "TTL1", "TTL2", "TTL3", "TTL4", "TTL5", "TTL6", "TTL7"
            Case "EXTERNAL"
                Select Case UCase(sArmStopLevel)
                    Case "TTL", "ECL", "GND"
                    Case Else
                        Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasFallTime sArmStopLevel argument out of range.")
                        Echo("RANGE: TTL, ECL or GND")
                        dMeasFallTime = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                End Select
            Case Else
                Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasFallTime sArmStopSource argument out of range.")
                Echo("RANGE: IMMEDIATE, VXI, TTL0, TTL1, TTL2, TTL3, TTL4, TTL5, TTL6, TTL7 or EXTERNAL")
                dMeasFallTime = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                Exit Function
        End Select

        Select Case UCase(sTimeBaseSource)
            Case "VXI", "INTERNAL", "EXTERNAL"
            Case Else
                Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasFallTime sTimeBaseSource argument out of range.")
                Echo("RANGE: VXI, INTERNAL or EXTERNAL")
                dMeasFallTime = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                Exit Function
        End Select

        If dTimeIntervalDelay < 0.001 Or dTimeIntervalDelay > 99.999 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasFallTime dTimeIntervalDelay argument out of range.")
            Echo("RANGE: 0.001 to 99.999")
            dMeasFallTime = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
            Exit Function
        End If

        Select Case UCase(sAcquisitionTimeOutSetting)
            Case "ON", "OFF", "START"
            Case Else
                Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasFallTime sAcquisitionTimeOutSetting argument out of range.")
                Echo("RANGE: ON, OFF, or START")
                dMeasFallTime = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                Exit Function
        End Select

        If dAcquisitionTimeOut < 0.1 Or dAcquisitionTimeOut > 1500 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasFallTime dAcquisitionTimeOut argument out of range.")
            Echo("RANGE: 0.1 to 1500")
            dMeasFallTime = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
            Exit Function
        End If

        Select Case Left(UCase(APROBE), 1)
            Case "S", "O", "C" ' Single, Off, Continuous
            Case Else
                Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasFallTime APROBE argument out of range.")
                Echo("RANGE: SINGLE or OFF")
                dMeasFallTime = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                Exit Function
        End Select

        'Configure Counter for Measurement
        'Set Input Options
        'Clear Counter
        If Not bSimulation Then
            'viClear (nInstrumentHandle(COUNTER))
            nSystErr = atxmlDF_viClear(ResourceName(COUNTER), 0)
            WriteMsg(COUNTER, "*RST ; *CLS")
        End If

        sCommand = ""
        'Configure INPut Subsystem
        If nChannel = 1 Then
            'Set Attenuation
            If Not bChOne10XAttenuation Then
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:ATT 1;")
                Debug.Print(":INP1:ATT 1;")
            Else
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:ATT 10;")
                Debug.Print(":INP1:ATT 10;")
            End If
            'Set Coupling

            If Not bChOneACCoupling Then 'bbbb was Not simulation
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:COUP DC;")
                Debug.Print(":INP1:COUP DC;")
            Else
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:COUP AC;")
                Debug.Print(":INP1:COUP AC;")
            End If

            'Set Impedance
            If Not bChOne50ohms Then 'bbbb was Not simulation
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:IMP 1E6;")
                Debug.Print(":INP1:IMP 1E6;")
            Else
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:IMP 50;")
                Debug.Print(":INP1:IMP 50;")
            End If

            'Set Routing (Always separate for Period)
            If Not bSimulation Then WriteMsg(COUNTER, ":INP1:ROUT SEP;")
            Debug.Print(":INP1:ROUT SEP;")
        Else ' Channel 2
            'Set Attenuation
            If Not bChTwo10XAttenuation Then
                If Not bSimulation Then WriteMsg(COUNTER, ":INP2:ATT 1;")
                Debug.Print(":INP2:ATT 1;")
            Else
                If Not bSimulation Then WriteMsg(COUNTER, ":INP2:ATT 10;")
                Debug.Print(":INP2:ATT 10;")
            End If

            'Set Coupling
            If Not bChTwoACCoupling Then 'bbbb was Not simulation
                If Not bSimulation Then WriteMsg(COUNTER, ":INP2:COUP DC;")
                Debug.Print(":INP2:COUP DC;")
            Else
                If Not bSimulation Then WriteMsg(COUNTER, ":INP2:COUP AC;")
                Debug.Print(":INP2:COUP AC;")
            End If

            'Set Impedance
            If Not bChTwo50ohms Then 'bbbb was Not simulation
                If Not bSimulation Then WriteMsg(COUNTER, ":INP2:IMP 1E6;")
                Debug.Print(":INP2:IMP 1E6;")
            Else
                If Not bSimulation Then WriteMsg(COUNTER, ":INP2:IMP 50;")
                Debug.Print(":INP2:IMP 50;")
            End If
            'Set Routing (Always separate for Period)
            'If Not simulation Then WriteMsg COUNTER, ":INP2:ROUT SEP;"
            'Debug.Print ":INP2:ROUT SEP;"
        End If

        'Configure SENSe Options
        'Function PERIOD
        sCommand = ":SENS" & nChannel & ":FUNC " & Q & "FTIM" & Q & ";"
        'Gate Average always off for Frequency
        sCommand = sCommand & ":SENS" & nChannel & ":AVER:STAT OFF;"
        'Set Aquisition Time Out
        sCommand = sCommand & ":SENS:ATIM:TIME " & CStr(dAcquisitionTimeOut) & ";"
        'Set Visa TMO value based on dAcqusitionTimeOut or dAperature setting + 1 Second
        If dAcquisitionTimeOut > dAperature Then
            If Not bSimulation Then lSystErr = viSetAttribute(nInstrumentHandle(COUNTER), VI_ATTR_TMO_VALUE, (dAcquisitionTimeOut * 1000) + 1000)
        Else
            If Not bSimulation Then lSystErr = viSetAttribute(nInstrumentHandle(COUNTER), VI_ATTR_TMO_VALUE, (dAperature * 1000) + 1000)
        End If

        If Not bSimulation And lSystErr Then
            lSystErr = viStatusDesc(iHandle, lSystErr, lpBuffer)
            Echo("WRITEMSG ERROR: Error sending command to " & sInstrumentDescription(COUNTER) & ".")
            Echo("COMMAND: viSetAttribute")
            MsgBox("Error sending: viSetAttribute VI_ATTR_TMO_VALUE" & vbCrLf & "ERROR: " & lpBuffer, MsgBoxStyle.Exclamation, "VISA Error")
            Exit Function
        End If

        'Set Aquisition Time Out Mode
        Select Case UCase(sAcquisitionTimeOutSetting)
            Case "ON", "OFF"
                sCommand = sCommand & ":SENS:ATIM:CHEC " & UCase(sAcquisitionTimeOutSetting) & ";"
            Case "START"
                sCommand = sCommand & ":SENS:ATIM:CHEC STAR;"
        End Select

        'Set Auto Trigger or absolute
        If bAutoTrigger Then
            sCommand = sCommand & ":SENS" & nChannel & ":EVEN:LEV:ABS:AUTO ON;"
            sCommand = sCommand & ":SENS" & nChannel & ":EVEN:LEV:REL " & CStr(nRelativeTriggerLevel) & ";"
        Else
            sCommand = sCommand & ":SENS" & nChannel & ":EVEN:LEV:ABS " & CStr(dAbsoluteTrigger) & ";"
            sCommand = sCommand & ":SENS" & nChannel & ":EVEN:LEV:ABS:AUTO OFF;"
        End If

        'Set Trigger Slop
        If Not bTriggerSlopeNegative Then
            sCommand = sCommand & ":SENS" & nChannel & ":EVEN:SLOP POS;"
        Else
            sCommand = sCommand & ":SENS" & nChannel & ":EVEN:SLOP NEG;"
        End If

        'Set Hysteresis
        sCommand = sCommand & ":SENS" & nChannel & ":EVEN:HYST " & UCase(sHysteresis) & ";"
        'Set Aperature
        sCommand = sCommand & ":SENS" & nChannel & ":FREQ:APER " & CStr(dAperature) & ";"
        sCommand = sCommand & ":SENS" & nChannel & ":PER:APER " & CStr(dAperature) & ";"
        sCommand = sCommand & ":SENS" & nChannel & ":RAT:APER " & CStr(dAperature) & ";"
        'Set Range
        sCommand = sCommand & ":SENS" & nChannel & ":FREQ:RANG:AUTO OFF;"
        'Set Timebase source
        Select Case UCase(sTimeBaseSource)
            Case "VXI"
                sCommand = sCommand & ":SENS" & nChannel & ":ROSC:SOUR CLK10;"
            Case "INTERNAL"
                sCommand = sCommand & ":SENS" & nChannel & ":ROSC:SOUR INT;"
            Case "EXTERNAL"
                sCommand = sCommand & ":SENS" & nChannel & ":ROSC:SOUR EXT;"
        End Select

        If bTimeIntervalDelay Then
            sCommand = sCommand & ":SENS" & nChannel & ":TINT:DEL:STAT ON;"
            sCommand = sCommand & ":SENS" & nChannel & ":TINT:DEL:TIME " & CStr(dTimeIntervalDelay) & ";"
        Else
            sCommand = sCommand & ":SENS" & nChannel & ":TINT:DEL:STAT OFF;"
        End If

        If bTotalizeGateMeasure Then
            sCommand = sCommand & ":SENS" & nChannel & ":TOT:GATE:STAT ON;"
            If bTotalizeGatePolarityInverted Then
                sCommand = sCommand & ":SENS" & nChannel & ":TOT:GATE:POL INV;"
            Else
                sCommand = sCommand & ":SENS" & nChannel & ":TOT:GATE:POL NORM;"
            End If
        Else
            sCommand = sCommand & ":SENS" & nChannel & ":TOT:GATE:STAT OFF;"
            sCommand = sCommand & ":SENS" & nChannel & ":TOT:GATE:POL NORM;"
        End If

        'Write SENSe Subset to instrument
        If Not bSimulation Then WriteMsg(COUNTER, sCommand)
        Debug.Print(sCommand)

        'Configure ARM Options
        sCommand = ""
        Select Case UCase(sArmStartSource)
            Case "IMMEDIATE"
                sCommand = sCommand & ":ARM:STAR:SOUR IMM;"
            Case "VXI"
                sCommand = sCommand & ":ARM:STAR:SOUR BUS;"
            Case "TTL0", "TTL1", "TTL2", "TTL3", "TTL4", "TTL5", "TTL6", "TTL7"
                sCommand = sCommand & ":ARM:STAR:SOUR TTLT" & Right(sArmStartSource, 1) & ";"
                If Not bArmStartSlopNegative Then
                    sCommand = sCommand & ":ARM:STAR:SLOP POS;"
                Else
                    sCommand = sCommand & ":ARM:STAR:SLOP NEG;"
                End If
            Case "EXTERNAL"
                sCommand = sCommand & ":ARM:STAR:SOUR EXT;"
                Select Case UCase(sArmStartLevel)
                    Case "TTL"
                        sCommand = sCommand & ":ARM:STAR:LEV MAX"
                    Case "ECL"
                        sCommand = sCommand & ":ARM:STAR:LEV MIN;"
                    Case "GND"
                        sCommand = sCommand & ":ARM:STAR:LEV DEF;"
                End Select

                If Not bArmStartSlopNegative Then
                    sCommand = sCommand & ":ARM:STAR:SLOP POS;"
                Else
                    sCommand = sCommand & ":ARM:STAR:SLOP NEG;"
                End If
        End Select

        Select Case UCase(SArmStopSource)
            Case "IMMEDIATE"
                sCommand = sCommand & ":ARM:STOP:SOUR IMM;"
            Case "VXI"
                sCommand = sCommand & ":ARM:STOP:SOUR BUS;"
            Case "TTL0", "TTL1", "TTL2", "TTL3", "TTL4", "TTL5", "TTL6", "TTL7"
                sCommand = sCommand & ":ARM:STOP:SOUR TTLT" & Right(SArmStopSource, 1) & ";"
                If Not bArmStopSlopNegative Then
                    sCommand = sCommand & ":ARM:STOP:SLOP POS;"
                Else
                    sCommand = sCommand & ":ARM:STOP:SLOP NEG;"
                End If
            Case "EXTERNAL"
                sCommand = sCommand & ":ARM:STOP:SOUR EXT;"
                Select Case UCase(sArmStopLevel)
                    Case "TTL"
                        sCommand = sCommand & ":ARM:STOP:LEV MAX"
                    Case "ECL"
                        sCommand = sCommand & ":ARM:STOP:LEV MIN;"
                    Case "GND"
                        sCommand = sCommand & ":ARM:STOP:LEV DEF;"
                End Select

                If Not bArmStartSlopNegative Then
                    sCommand = sCommand & ":ARM:STOP:SLOP POS;"
                Else
                    sCommand = sCommand & ":ARM:STOP:SLOP NEG;"
                End If
        End Select

        'Write ARM Subset to instrument
        If Not bSimulation Then WriteMsg(COUNTER, sCommand)
        Debug.Print(sCommand)

        'Configure OUTPut
        sCommand = ""
        If Not bTriggerOutputOn Then
            For i = 0 To 7
                sCommand = sCommand & ":OUTP:TTLT" & i & ":STAT OFF;"
            Next i
            sCommand = sCommand & ":OUTP:ROSC:STAT OFF;"
        Else
            For i = 0 To 7
                If i = nTTLTriggerOutput Then
                    sCommand = sCommand & ":OUTP:TTLT" & i & ":STAT ON;"
                Else
                    sCommand = sCommand & ":OUTP:TTLT" & i & ":STAT OFF;"
                End If
            Next i
            sCommand = sCommand & ":OUTP:ROSC:STAT ON;"
        End If

        'Write OUTPut Subset to instrument
        If Not bSimulation Then WriteMsg(COUNTER, sCommand)
        Debug.Print(sCommand)
        'Configure CONFigure Options
        sCommand = ""
        If dRange <> -1 Then
            sCommand = ":CONF" & nChannel & ":FTIM 90, 10, " & CStr(dRange) & ";"
            Debug.Print(sCommand)
        End If

        'Make Measurement
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        If Not bSimulation Then
            Select Case Left(UCase(APROBE), 1)
                Case "O" ' Off
                    WriteMsg(COUNTER, "INIT" & CStr(nChannel) & ";FETCH" & CStr(nChannel) & "?")
                    Debug.Print("INIT" & CStr(nChannel) & ";FETCH" & CStr(nChannel) & "?")
                    nErr = ReadMsg(COUNTER, sData)
                    If nErr <> 0 Then
                        dMeasFallTime = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37)
                        Err.Raise(nErr)
                        Exit Function
                    End If

                    dMeasFallTime = CDbl(sData)
                    gFrmMain.txtMeasured.Text = CStr(CSng(dMeasFallTime))
                    If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
                Case "S" ' Single
                    MisProbe = MsgBoxResult.Yes
                    Do While MisProbe = MsgBoxResult.Yes
                        gFrmMain.lblStatus.Text = "Waiting for Probe ..."
                        Failed = False
                        UserEvent = 0
                        gFrmMain.TimerProbe.Enabled = True : gFrmMain.fraInstructions.Visible = True
                        Do While (UserEvent = 0) And (Not bProbeClosed)
                            System.Windows.Forms.Application.DoEvents()
                        Loop

                        gFrmMain.TimerProbe.Enabled = False
                        bProbeClosed = False
                        gFrmMain.lblStatus.Text = "Making Measurement ..."
                        WriteMsg(COUNTER, "INIT" & CStr(nChannel) & ";FETCH" & CStr(nChannel) & "?")
                        nErr = ReadMsg(COUNTER, sData)
                        If nErr <> 0 Then
                            dMeasFallTime = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37)
                            Err.Raise(nErr)
                            Exit Function
                        End If

                        dMeasFallTime = CDbl(sData)
                        gFrmMain.txtMeasured.Text = CStr(CSng(dMeasFallTime))
                        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
                        gFrmMain.lblStatus.Text = "Measurement Complete ..."
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
                        Failed = False
                        UserEvent = 0
                        gFrmMain.TimerProbe.Enabled = True : gFrmMain.fraInstructions.Visible = True
                        Do While (UserEvent = 0) And (Not bProbeClosed)
                            gFrmMain.lblStatus.Text = "Making Measurement ..."
                            WriteMsg(COUNTER, "INIT" & CStr(nChannel) & ";FETCH" & CStr(nChannel) & "?")
                            nErr = ReadMsg(COUNTER, sData)
                            If nErr <> 0 Then
                                dMeasFallTime = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37)
                                Err.Raise(nErr)
                                Exit Function
                            End If

                            dMeasFallTime = CDbl(sData)
                            gFrmMain.txtMeasured.Text = CStr(CSng(dMeasFallTime))
                            If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
                        Loop

                        gFrmMain.TimerProbe.Enabled = False
                        bProbeClosed = False
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
            End Select
        Else
            dMeasFallTime = CDbl(InputBox("Command cmdCOUNTER.dMeasFallTime peformed." & vbCrLf & "Enter Value:", "SIMULATION MODE"))
            gFrmMain.txtMeasured.Text = CStr(CSng(dMeasFallTime))
            If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON)
        End If
    End Function

    Public Function dMeasTotalize(Optional ByRef nChannel As Short = 1, Optional ByRef bChOne10XAttenuation As Boolean = False,
                                  Optional ByRef bChTwo10XAttenuation As Boolean = False, Optional ByRef bChOneACCoupling As Boolean = False,
                                  Optional ByRef bChTwoACCoupling As Boolean = False, Optional ByRef bChOne50ohms As Boolean = False,
                                  Optional ByRef bChTwo50ohms As Boolean = False, Optional ByRef sHysteresis As String = "DEF",
                                  Optional ByRef bAutoTrigger As Boolean = False, Optional ByRef nRelativeTriggerLevel As Integer = 50,
                                  Optional ByRef bTriggerSlopeNegative As Boolean = False, Optional ByRef dAbsoluteTrigger As Double = 0,
                                  Optional ByRef bTriggerOutputOn As Boolean = False, Optional ByRef nTTLTriggerOutput As Integer = 0,
                                  Optional ByRef sArmStartSource As String = "IMMEDIATE", Optional ByRef bArmStartSlopNegative As Boolean = False,
                                  Optional ByRef sArmStartLevel As String = "TTL", Optional ByRef SArmStopSource As String = "IMMEDIATE",
                                  Optional ByRef bArmStopSlopNegative As Boolean = False, Optional ByRef sArmStopLevel As String = "TTL",
                                  Optional ByRef sTimeBaseSource As String = "INTERNAL", Optional ByRef bOutputTimeBase As Boolean = False,
                                  Optional ByRef bTotalizeGateMeasure As Boolean = False, Optional ByRef bTotalizeGatePolarityInverted As Boolean = False,
                                  Optional ByRef bTimeIntervalDelay As Boolean = False, Optional ByRef dTimeIntervalDelay As Double = 0.1,
                                  Optional ByRef sAcquisitionTimeOutSetting As String = "ON", Optional ByRef dAcquisitionTimeOut As Double = 5,
                                  Optional ByRef APROBE As String = "OFF") As Double
        Dim i As Short
        Dim sData As String = ""
        Dim nErr As Integer
        Dim sCommand As String
        Dim Q As String
        Q = Chr(34)
        Dim sCurrentMsg As String = ""

        dMeasTotalize = 0.0
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        gFrmMain.txtInstrument.Text = "Counter"
        gFrmMain.txtCommand.Text = "dMeasTotalize"
        If APROBE <> "OFF" Then
            sCurrentMsg = gFrmMain.lblStatus.Text
            gFrmMain.lblStatus.Text = "APPLY PROBE"
        End If

        'Clear Counter
        If Not bSimulation Then
            'viClear (nInstrumentHandle(COUNTER))
            nSystErr = atxmlDF_viClear(ResourceName(COUNTER), 0)
            WriteMsg(COUNTER, "*RST ; *CLS")
        End If

        'Error Check Arguments
        If nChannel < 1 Or nChannel > 2 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasTotalize nChannel argument out of range.")
            Echo("RANGE: 1 TO 2")
            dMeasTotalize = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
            Exit Function
        End If

        Select Case UCase(sHysteresis)
            Case "DEF", "MAX", "MIN"
            Case Else
                Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasTotalize sHysteresis argument out of range.")
                Echo("RANGE: DEF, MAX or MIN")
                dMeasTotalize = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                Exit Function
        End Select

        If nRelativeTriggerLevel < 10 Or nRelativeTriggerLevel > 90 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasTotalize nRelativeTriggerLevel argument out of range.")
            Echo("RANGE: 10 to 90")
            dMeasTotalize = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
            Exit Function
        End If

        If dAbsoluteTrigger < -10.238 Or dAbsoluteTrigger > 10.238 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasTotalize dAbsoluteTrigger argument out of range.")
            Echo("RANGE: -10.238 to 10.238")
            dMeasTotalize = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
            Exit Function
        End If

        If nTTLTriggerOutput < 0 Or nTTLTriggerOutput > 7 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasTotalize nTTLTriggerOutput argument out of range.")
            Echo("RANGE: 0 to 7")
            dMeasTotalize = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)

            Exit Function
        End If

        Select Case UCase(sArmStartSource)
            Case "IMMEDIATE", "VXI"
            Case "TTL0", "TTL1", "TTL2", "TTL3", "TTL4", "TTL5", "TTL6", "TTL7"
            Case "EXTERNAL"
                Select Case UCase(sArmStartLevel)
                    Case "TTL", "ECL", "GND"
                    Case Else
                        Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasTotalize sArmStartLevel argument out of range.")
                        Echo("RANGE: TTL, ECL or GND")
                        dMeasTotalize = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                End Select
            Case Else
                Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasTotalize sArmStartSource argument out of range.")
                Echo("RANGE: IMMEDIATE, VXI, TTL0, TTL1, TTL2, TTL3, TTL4, TTL5, TTL6, TTL7 or EXTERNAL")
                dMeasTotalize = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                Exit Function
        End Select

        Select Case UCase(SArmStopSource)
            Case "IMMEDIATE", "VXI"
            Case "TTL0", "TTL1", "TTL2", "TTL3", "TTL4", "TTL5", "TTL6", "TTL7"
            Case "EXTERNAL"
                Select Case UCase(sArmStopLevel)
                    Case "TTL", "ECL", "GND"
                    Case Else
                        Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasTotalize sArmStopLevel argument out of range.")
                        Echo("RANGE: TTL, ECL or GND")
                        dMeasTotalize = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)

                End Select
            Case Else
                Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasTotalize sArmStopSource argument out of range.")
                Echo("RANGE: IMMEDIATE, VXI, TTL0, TTL1, TTL2, TTL3, TTL4, TTL5, TTL6, TTL7 or EXTERNAL")
                dMeasTotalize = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)

                Exit Function
        End Select

        Select Case UCase(sTimeBaseSource)
            Case "VXI", "INTERNAL", "EXTERNAL"
            Case Else
                Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasTotalize sTimeBaseSource argument out of range.")
                Echo("RANGE: VXI, INTERNAL or EXTERNAL")
                dMeasTotalize = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)

                Exit Function
        End Select

        If dTimeIntervalDelay < 0.001 Or dTimeIntervalDelay > 99.999 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasTotalize dTimeIntervalDelay argument out of range.")
            Echo("RANGE: 0.001 to 99.999")
            dMeasTotalize = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
            Exit Function
        End If

        Select Case UCase(sAcquisitionTimeOutSetting)
            Case "ON", "OFF", "START"
            Case Else
                Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasTotalize sAcquisitionTimeOutSetting argument out of range.")
                Echo("RANGE: ON, OFF, or START")
                dMeasTotalize = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                Exit Function
        End Select
        '    If dAcquisitionTimeOut < 0.1 Or dAcquisitionTimeOut > 1500 Then
        '        Echo "COUNTER PROGRAMMING ERROR:  Command cmdCounter.bStartTotalize dAcquisitionTimeOut argument out of range."
        '        Echo "RANGE: 0.1 to 1500"
        '        Err.Raise -1001
        '
        '        Exit Sub
        '    End If
        If Not bSimulation Then lSystErr = viSetAttribute(nInstrumentHandle(COUNTER), VI_ATTR_TMO_VALUE, (dAcquisitionTimeOut * 1000) + 1000)

        Select Case Left(UCase(APROBE), 1)
            Case "S", "O", "C" ' Single, Off, Continuous
            Case Else
                Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasTotalize APROBE argument out of range.")
                Echo("RANGE: SINGLE or OFF")
                dMeasTotalize = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                Exit Function
        End Select

        'Configure Counter for Measurement
        'Set Input Options
        'Clear Counter
        If Not bSimulation Then
            '  viClear (nInstrumentHandle(COUNTER))
            nSystErr = atxmlDF_viClear(ResourceName(COUNTER), 0)
            WriteMsg(COUNTER, "*RST ; *CLS")
        End If

        sCommand = ""
        'Configure INPut Subsystem
        If nChannel = 1 Then
            'Set Attenuation
            If Not bChOne10XAttenuation Then
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:ATT 1;")
                Debug.Print(":INP1:ATT 1;")
            Else
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:ATT 10;")
                Debug.Print(":INP1:ATT 10;")
            End If

            'Set Coupling
            If Not bChOneACCoupling Then 'bbbb was simulation
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:COUP DC;")
                Debug.Print(":INP1:COUP DC;")
            Else
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:COUP AC;")
                Debug.Print(":INP1:COUP AC;")
            End If

            'Set Impedance
            If Not bChOne50ohms Then 'bbbb was simulation
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:IMP 1E6;")
                Debug.Print(":INP1:IMP 1E6;")
            Else
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:IMP 50;")
                Debug.Print(":INP1:IMP 50;")
            End If

            'Set Routing (Always separate for Period)
            If Not bSimulation Then WriteMsg(COUNTER, ":INP1:ROUT SEP;")
            Debug.Print(":INP1:ROUT SEP;")
        Else ' Channel 2
            'Set Attenuation
            If Not bChTwo10XAttenuation Then
                If Not bSimulation Then WriteMsg(COUNTER, ":INP2:ATT 1;")
                Debug.Print(":INP2:ATT 1;")
            Else
                If Not bSimulation Then WriteMsg(COUNTER, ":INP2:ATT 10;")
                Debug.Print(":INP2:ATT 10;")
            End If

            'Set Coupling
            If Not bChTwoACCoupling Then 'bbbb was simulation
                If Not bSimulation Then WriteMsg(COUNTER, ":INP2:COUP DC;")
                Debug.Print(":INP2:COUP DC;")
            Else
                If Not bSimulation Then WriteMsg(COUNTER, ":INP2:COUP AC;")
                Debug.Print(":INP2:COUP AC;")
            End If

            'Set Impedance
            If Not bChTwo50ohms Then 'bbbb was simulation
                If Not bSimulation Then WriteMsg(COUNTER, ":INP2:IMP 1E6;")
                Debug.Print(":INP2:IMP 1E6;")
            Else
                If Not bSimulation Then WriteMsg(COUNTER, ":INP2:IMP 50;")
                Debug.Print(":INP2:IMP 50;")
            End If
            'Set Routing (Always separate for Period)
            'If Not simulation Then WriteMsg COUNTER, ":INP2:ROUT SEP;"
            'Debug.Print ":INP2:ROUT SEP;"
        End If

        'Configure SENSe Options
        'Function PERIOD
        sCommand = ":SENS" & nChannel & ":FUNC " & Q & "TOT" & Q & ";"
        'Gate Average always off for Frequency
        sCommand = sCommand & ":SENS" & nChannel & ":AVER:STAT OFF;"
        'Set Aquisition Time Out
        sCommand = sCommand & ":SENS:ATIM:TIME " & CStr(dAcquisitionTimeOut) & ";"
        'Set Visa TMO value based on dAcqusitionTimeOut or dAperature setting + 1 Second
        '        If dAcquisitionTimeOut > dAperature Then
        '            If Not bSimulation Then lSystErr = viSetAttribute(nInstrumentHandle(COUNTER), VI_ATTR_TMO_VALUE, (dAcquisitionTimeOut * 1000) + 1000)
        '        Else
        '            If Not bSimulation Then lSystErr = viSetAttribute(nInstrumentHandle(COUNTER), VI_ATTR_TMO_VALUE, (dAperature * 1000) + 1000)
        '        End If
        '        If Not bsimulaton And lSystErr Then
        '          lSystErr = viStatusDesc(ihandle, lSystErr, lpBuffer)
        '          Echo "WRITEMSG ERROR: Error sending command to " & sInstrumentDescription(COUNTER) & "."
        '          Echo "COMMAND: viSetAttribute"
        '          MsgBox "Error sending: viSetAttribute VI_ATTR_TMO_VALUE" & vbCrLf & "ERROR: " & lpBuffer, vbExclamation, "VISA Error"
        '
        '          Exit Function
        '        End If
        'Set Aquisition Time Out Mode
        Select Case UCase(sAcquisitionTimeOutSetting)
            Case "ON", "OFF"
                sCommand = sCommand & ":SENS:ATIM:CHEC " & UCase(sAcquisitionTimeOutSetting) & ";"
            Case "START"
                sCommand = sCommand & ":SENS:ATIM:CHEC STAR;"
        End Select

        'Set Auto Trigger or absolute
        If bAutoTrigger Then
            sCommand = sCommand & ":SENS" & nChannel & ":EVEN:LEV:ABS:AUTO ON;"
            sCommand = sCommand & ":SENS" & nChannel & ":EVEN:LEV:REL " & CStr(nRelativeTriggerLevel) & ";"
        Else
            sCommand = sCommand & ":SENS" & nChannel & ":EVEN:LEV:ABS " & CStr(dAbsoluteTrigger) & ";"
            sCommand = sCommand & ":SENS" & nChannel & ":EVEN:LEV:ABS:AUTO OFF;"
        End If

        'Set Trigger Slop
        If Not bTriggerSlopeNegative Then
            sCommand = sCommand & ":SENS" & nChannel & ":EVEN:SLOP POS;"
        Else
            sCommand = sCommand & ":SENS" & nChannel & ":EVEN:SLOP NEG;"
        End If

        'Set Hysteresis
        sCommand = sCommand & ":SENS" & nChannel & ":EVEN:HYST " & UCase(sHysteresis) & ";"
        'Set Aperature
        '        sCommand = sCommand & ":SENS" & nChannel & ":FREQ:APER " & CStr(dAperature) & ";"
        '        sCommand = sCommand & ":SENS" & nChannel & ":PER:APER " & CStr(dAperature) & ";"
        '        sCommand = sCommand & ":SENS" & nChannel & ":RAT:APER " & CStr(dAperature) & ";"
        'Set Range
        sCommand = sCommand & ":SENS" & nChannel & ":FREQ:RANG:AUTO OFF;"
        'Set Timebase source
        Select Case UCase(sTimeBaseSource)
            Case "VXI"
                sCommand = sCommand & ":SENS" & nChannel & ":ROSC:SOUR CLK10;"
            Case "INTERNAL"
                sCommand = sCommand & ":SENS" & nChannel & ":ROSC:SOUR INT;"
            Case "EXTERNAL"
                sCommand = sCommand & ":SENS" & nChannel & ":ROSC:SOUR EXT;"
        End Select

        If bTimeIntervalDelay Then
            sCommand = sCommand & ":SENS" & nChannel & ":TINT:DEL:STAT ON;"
            sCommand = sCommand & ":SENS" & nChannel & ":TINT:DEL:TIME " & CStr(dTimeIntervalDelay) & ";"
        Else
            sCommand = sCommand & ":SENS" & nChannel & ":TINT:DEL:STAT OFF;"
        End If

        If bTotalizeGateMeasure Then
            sCommand = sCommand & ":SENS" & nChannel & ":TOT:GATE:STAT ON;"
            If bTotalizeGatePolarityInverted Then
                sCommand = sCommand & ":SENS" & nChannel & ":TOT:GATE:POL INV;"
            Else
                sCommand = sCommand & ":SENS" & nChannel & ":TOT:GATE:POL NORM;"
            End If
        Else
            sCommand = sCommand & ":SENS" & nChannel & ":TOT:GATE:STAT OFF;"
            sCommand = sCommand & ":SENS" & nChannel & ":TOT:GATE:POL NORM;"
        End If

        'Write SENSe Subset to instrument
        If Not bSimulation Then WriteMsg(COUNTER, sCommand)
        Debug.Print(sCommand)

        'Configure ARM Options
        sCommand = ""
        Select Case UCase(sArmStartSource)
            Case "IMMEDIATE"
                sCommand = sCommand & ":ARM:STAR:SOUR IMM;"
            Case "VXI"
                sCommand = sCommand & ":ARM:STAR:SOUR BUS;"
            Case "TTL0", "TTL1", "TTL2", "TTL3", "TTL4", "TTL5", "TTL6", "TTL7"
                sCommand = sCommand & ":ARM:STAR:SOUR TTLT" & Right(sArmStartSource, 1) & ";"
                If Not bArmStartSlopNegative Then
                    sCommand = sCommand & ":ARM:STAR:SLOP POS;"
                Else
                    sCommand = sCommand & ":ARM:STAR:SLOP NEG;"
                End If
            Case "EXTERNAL"
                sCommand = sCommand & ":ARM:STAR:SOUR EXT;"
                Select Case UCase(sArmStartLevel)
                    Case "TTL"
                        sCommand = sCommand & ":ARM:STAR:LEV MAX"
                    Case "ECL"
                        sCommand = sCommand & ":ARM:STAR:LEV MIN;"
                    Case "GND"
                        sCommand = sCommand & ":ARM:STAR:LEV DEF;"
                End Select

                If Not bArmStartSlopNegative Then
                    sCommand = sCommand & ":ARM:STAR:SLOP POS;"
                Else
                    sCommand = sCommand & ":ARM:STAR:SLOP NEG;"
                End If
        End Select

        Select Case UCase(SArmStopSource)
            Case "IMMEDIATE"
                sCommand = sCommand & ":ARM:STOP:SOUR IMM;"
            Case "VXI"
                sCommand = sCommand & ":ARM:STOP:SOUR BUS;"
            Case "TTL0", "TTL1", "TTL2", "TTL3", "TTL4", "TTL5", "TTL6", "TTL7"
                sCommand = sCommand & ":ARM:STOP:SOUR TTLT" & Right(SArmStopSource, 1) & ";"
                If Not bArmStopSlopNegative Then
                    sCommand = sCommand & ":ARM:STOP:SLOP POS;"
                Else
                    sCommand = sCommand & ":ARM:STOP:SLOP NEG;"
                End If
            Case "EXTERNAL"
                sCommand = sCommand & ":ARM:STOP:SOUR EXT;"
                Select Case UCase(sArmStopLevel)
                    Case "TTL"
                        sCommand = sCommand & ":ARM:STOP:LEV MAX"
                    Case "ECL"
                        sCommand = sCommand & ":ARM:STOP:LEV MIN;"
                    Case "GND"
                        sCommand = sCommand & ":ARM:STOP:LEV DEF;"
                End Select

                If Not bArmStartSlopNegative Then
                    sCommand = sCommand & ":ARM:STOP:SLOP POS;"
                Else
                    sCommand = sCommand & ":ARM:STOP:SLOP NEG;"
                End If
        End Select
        'Write ARM Subset to instrument
        If Not bSimulation Then WriteMsg(COUNTER, sCommand)
        Debug.Print(sCommand)

        'Configure OUTPut
        sCommand = ""
        If Not bTriggerOutputOn Then
            For i = 0 To 7
                sCommand = sCommand & ":OUTP:TTLT" & i & ":STAT OFF;"
            Next i
            sCommand = sCommand & ":OUTP:ROSC:STAT OFF;"
        Else
            For i = 0 To 7
                If i = nTTLTriggerOutput Then
                    sCommand = sCommand & ":OUTP:TTLT" & i & ":STAT ON;"
                Else
                    sCommand = sCommand & ":OUTP:TTLT" & i & ":STAT OFF;"
                End If
            Next i
            sCommand = sCommand & ":OUTP:ROSC:STAT ON;"
        End If
        'Write OUTPut Subset to instrument
        If Not bSimulation Then WriteMsg(COUNTER, sCommand)
        Debug.Print(sCommand)
        'Configure CONFigure Options
        sCommand = ""
        If dRange <> -1 Then
            sCommand = ":CONF" & nChannel & ":TOT;"
            Debug.Print(sCommand)
        End If

        'Make Measurement
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        If Not bSimulation Then
            Select Case Left(UCase(APROBE), 1)
                Case "O" ' Off
                    WriteMsg(COUNTER, "INIT" & CStr(nChannel) & ";FETCH" & CStr(nChannel) & "?")
                    Debug.Print("INIT" & CStr(nChannel) & ";FETCH" & CStr(nChannel) & "?")

                    nErr = ReadMsg(COUNTER, sData)
                    If nErr <> 0 Then
                        dMeasTotalize = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(nErr)
                        Exit Function
                    End If

                    dMeasTotalize = CDbl(sData)
                    gFrmMain.txtMeasured.Text = CStr(CSng(dMeasTotalize))
                    If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
                Case "S" ' Single
                    MisProbe = MsgBoxResult.Yes
                    Do While MisProbe = MsgBoxResult.Yes
                        gFrmMain.lblStatus.Text = "Waiting for Probe ..."
                        Failed = False
                        UserEvent = 0
                        gFrmMain.TimerProbe.Enabled = True : gFrmMain.fraInstructions.Visible = True
                        Do While (UserEvent = 0) And (Not bProbeClosed)
                            System.Windows.Forms.Application.DoEvents()
                        Loop

                        gFrmMain.TimerProbe.Enabled = False
                        bProbeClosed = False
                        gFrmMain.lblStatus.Text = "Making Measurement ..."
                        WriteMsg(COUNTER, "INIT" & CStr(nChannel) & ";FETCH" & CStr(nChannel) & "?")
                        nErr = ReadMsg(COUNTER, sData)
                        If nErr <> 0 Then
                            dMeasTotalize = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(nErr)
                            Exit Function
                        End If

                        dMeasTotalize = CDbl(sData)
                        gFrmMain.txtMeasured.Text = CStr(CSng(dMeasTotalize))
                        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function

                        gFrmMain.lblStatus.Text = "Measurement Complete ..."
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
                        Failed = False
                        UserEvent = 0
                        gFrmMain.TimerProbe.Enabled = True : gFrmMain.fraInstructions.Visible = True
                        Do While (UserEvent = 0) And (Not bProbeClosed)
                            gFrmMain.lblStatus.Text = "Making Measurement ..."
                            WriteMsg(COUNTER, "INIT" & CStr(nChannel) & ";FETCH" & CStr(nChannel) & "?")
                            nErr = ReadMsg(COUNTER, sData)
                            If nErr <> 0 Then
                                dMeasTotalize = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(nErr)
                                Exit Function
                            End If

                            dMeasTotalize = CDbl(sData)
                            gFrmMain.txtMeasured.Text = CStr(CSng(dMeasTotalize))
                            If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
                        Loop

                        gFrmMain.TimerProbe.Enabled = False
                        bProbeClosed = False
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
            End Select
        Else
            dMeasTotalize = CDbl(InputBox("Command cmdCOUNTER.dMeasTotalize peformed." & vbCrLf & "Enter Value:", "SIMULATION MODE"))
            gFrmMain.txtMeasured.Text = CStr(CSng(dMeasTotalize))
            If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON)
        End If
    End Function

    Public Function dMeasPhase(Optional ByRef bChOne10XAttenuation As Boolean = False, Optional ByRef bChTwo10XAttenuation As Boolean = False,
                               Optional ByRef bChOneACCoupling As Boolean = False, Optional ByRef bChTwoACCoupling As Boolean = False,
                               Optional ByRef bChOne50ohms As Boolean = False, Optional ByRef bChTwo50ohms As Boolean = False,
                               Optional ByRef sHysteresis As String = "DEF", Optional ByRef bAutoTrigger As Boolean = False,
                               Optional ByRef nRelativeTriggerLevel As Integer = 50, Optional ByRef bTriggerSlopeNegative As Boolean = False,
                               Optional ByRef dAbsoluteTrigger As Double = 0.0, Optional ByRef bTriggerOutputOn As Boolean = False,
                               Optional ByRef nTTLTriggerOutput As Integer = 0, Optional ByRef sArmStartSource As String = "IMMEDIATE",
                               Optional ByRef bArmStartSlopNegative As Boolean = False, Optional ByRef sArmStartLevel As String = "TTL",
                               Optional ByRef SArmStopSource As String = "IMMEDIATE", Optional ByRef bArmStopSlopNegative As Boolean = False,
                               Optional ByRef sArmStopLevel As String = "TTL", Optional ByRef sTimeBaseSource As String = "INTERNAL",
                               Optional ByRef bOutputTimeBase As Boolean = False, Optional ByRef bTotalizeGateMeasure As Boolean = False,
                               Optional ByRef bTotalizeGatePolarityInverted As Boolean = False, Optional ByRef bTimeIntervalDelay As Boolean = False,
                               Optional ByRef dTimeIntervalDelay As Double = 0.1, Optional ByRef sAcquisitionTimeOutSetting As String = "ON",
                               Optional ByRef dAcquisitionTimeOut As Double = 5.0, Optional ByRef APROBE As String = "OFF") As Double
        Dim i As Short
        Dim sData As String = ""
        Dim nErr As Integer
        Dim sCommand As String
        Dim Q As String
        Dim nChannel As Short
        Dim sCurrentMsg As String = ""
        nChannel = 1
        Q = Chr(34)

        Dim iHandle As Integer
        iHandle = nInstrumentHandle(COUNTER)

        dMeasPhase = 0.0
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        gFrmMain.txtInstrument.Text = "Counter"
        gFrmMain.txtCommand.Text = "dMeasPhase"
        If APROBE <> "OFF" Then
            sCurrentMsg = gFrmMain.lblStatus.Text
            gFrmMain.lblStatus.Text = "APPLY PROBE"
        End If

        'Clear Counter
        If Not bSimulation Then
            '  viClear (nInstrumentHandle(COUNTER))
            nSystErr = atxmlDF_viClear(ResourceName(COUNTER), 0)
            WriteMsg(COUNTER, "*RST ; *CLS")
        End If

        Select Case UCase(sHysteresis)
            Case "DEF", "MAX", "MIN"
            Case Else
                Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasPhase sHysteresis argument out of range.")
                Echo("RANGE: DEF, MAX or MIN")
                dMeasPhase = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                dMeasPhase = 0
                Exit Function
        End Select

        If nRelativeTriggerLevel < 10 Or nRelativeTriggerLevel > 90 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasPhase nRelativeTriggerLevel argument out of range.")
            Echo("RANGE: 10 to 90")
            dMeasPhase = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
            dMeasPhase = 0
            Exit Function
        End If

        If dAbsoluteTrigger < -10.238 Or dAbsoluteTrigger > 10.238 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasPhase dAbsoluteTrigger argument out of range.")
            Echo("RANGE: -10.238 to 10.238")
            dMeasPhase = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
            dMeasPhase = 0
            Exit Function
        End If

        If nTTLTriggerOutput < 0 Or nTTLTriggerOutput > 7 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasPhase nTTLTriggerOutput argument out of range.")
            Echo("RANGE: 0 to 7")
            dMeasPhase = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
            dMeasPhase = 0
            Exit Function
        End If

        Select Case UCase(sArmStartSource)
            Case "IMMEDIATE", "VXI"
            Case "TTL0", "TTL1", "TTL2", "TTL3", "TTL4", "TTL5", "TTL6", "TTL7"
            Case "EXTERNAL"
                Select Case UCase(sArmStartLevel)
                    Case "TTL", "ECL", "GND"
                    Case Else
                        Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasPhase sArmStartLevel argument out of range.")
                        Echo("RANGE: TTL, ECL or GND")
                        dMeasPhase = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                        dMeasPhase = 0
                End Select
            Case Else
                Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasPhase sArmStartSource argument out of range.")
                Echo("RANGE: IMMEDIATE, VXI, TTL0, TTL1, TTL2, TTL3, TTL4, TTL5, TTL6, TTL7 or EXTERNAL")
                dMeasPhase = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                dMeasPhase = 0
                Exit Function
        End Select

        Select Case UCase(SArmStopSource)
            Case "IMMEDIATE", "VXI"
            Case "TTL0", "TTL1", "TTL2", "TTL3", "TTL4", "TTL5", "TTL6", "TTL7"
            Case "EXTERNAL"
                Select Case UCase(sArmStopLevel)
                    Case "TTL", "ECL", "GND"
                    Case Else
                        Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasPhase sArmStopLevel argument out of range.")
                        Echo("RANGE: TTL, ECL or GND")
                        dMeasPhase = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                        dMeasPhase = 0
                End Select
            Case Else
                Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasPhase sArmStopSource argument out of range.")
                Echo("RANGE: IMMEDIATE, VXI, TTL0, TTL1, TTL2, TTL3, TTL4, TTL5, TTL6, TTL7 or EXTERNAL")
                dMeasPhase = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                dMeasPhase = 0
                Exit Function
        End Select

        Select Case UCase(sTimeBaseSource)
            Case "VXI", "INTERNAL", "EXTERNAL"
            Case Else
                Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasPhase sTimeBaseSource argument out of range.")
                Echo("RANGE: VXI, INTERNAL or EXTERNAL")
                dMeasPhase = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                dMeasPhase = 0
                Exit Function
        End Select

        If dTimeIntervalDelay < 0.001 Or dTimeIntervalDelay > 99.999 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasPhase dTimeIntervalDelay argument out of range.")
            Echo("RANGE: 0.001 to 99.999")
            dMeasPhase = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
            dMeasPhase = 0
            Exit Function
        End If

        Select Case UCase(sAcquisitionTimeOutSetting)
            Case "ON", "OFF", "START"
            Case Else
                Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasPhase sAcquisitionTimeOutSetting argument out of range.")
                Echo("RANGE: ON, OFF, or START")
                dMeasPhase = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                dMeasPhase = 0
                Exit Function
        End Select

        If dAcquisitionTimeOut < 0.1 Or dAcquisitionTimeOut > 1500 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasPhase dAcquisitionTimeOut argument out of range.")
            Echo("RANGE: 0.1 to 1500")
            dMeasPhase = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
            dMeasPhase = 0
            Exit Function
        End If

        Select Case Left(UCase(APROBE), 1)
            Case "S", "O", "C" ' Single, Off, Continuous
            Case Else
                Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasPhase APROBE argument out of range.")
                Echo("RANGE: SINGLE or OFF")
                dMeasPhase = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                dMeasPhase = 0
                Exit Function
        End Select

        'Configure Counter for Measurement
        'Set Input Options
        'Clear Counter
        If Not bSimulation Then
            '  viClear (nInstrumentHandle(COUNTER))
            nSystErr = atxmlDF_viClear(ResourceName(COUNTER), 0)
            WriteMsg(COUNTER, "*RST ; *CLS")
        End If

        sCommand = ""
        'Configure INPut Subsystem
        If nChannel = 1 Then
            'Set Attenuation
            If Not bChOne10XAttenuation Then
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:ATT 1;")
                Debug.Print(":INP1:ATT 1;")
            Else
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:ATT 10;")
                Debug.Print(":INP1:ATT 10;")
            End If

            'Set Coupling
            If Not bChOneACCoupling Then 'bbbb was simulation
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:COUP DC;")
                Debug.Print(":INP1:COUP DC;")
            Else
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:COUP AC;")
                Debug.Print(":INP1:COUP AC;")
            End If

            'Set Impedance
            If Not bChOne50ohms Then 'bbbb was simulation
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:IMP 1E6;")
                Debug.Print(":INP1:IMP 1E6;")
            Else
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:IMP 50;")
                Debug.Print(":INP1:IMP 50;")
            End If

            'Set Routing (Always separate for Period)
            If Not bSimulation Then WriteMsg(COUNTER, ":INP1:ROUT SEP;")
            Debug.Print(":INP1:ROUT SEP;")
        Else ' Channel 2
            'Set Attenuation
            If Not bChTwo10XAttenuation Then
                If Not bSimulation Then WriteMsg(COUNTER, ":INP2:ATT 1;")
                Debug.Print(":INP2:ATT 1;")
            Else
                If Not bSimulation Then WriteMsg(COUNTER, ":INP2:ATT 10;")
                Debug.Print(":INP2:ATT 10;")
            End If

            'Set Coupling
            If Not bChTwoACCoupling Then 'bbbb was simulation
                If Not bSimulation Then WriteMsg(COUNTER, ":INP2:COUP DC;")
                Debug.Print(":INP2:COUP DC;")
            Else
                If Not bSimulation Then WriteMsg(COUNTER, ":INP2:COUP AC;")
                Debug.Print(":INP2:COUP AC;")
            End If

            'Set Impedance
            If Not bChTwo50ohms Then 'bbbb was simulation
                If Not bSimulation Then WriteMsg(COUNTER, ":INP2:IMP 1E6;")
                Debug.Print(":INP2:IMP 1E6;")
            Else
                If Not bSimulation Then WriteMsg(COUNTER, ":INP2:IMP 50;")
                Debug.Print(":INP2:IMP 50;")
            End If
            'Set Routing (Always separate for Period)
            'If Not simulation Then WriteMsg COUNTER, ":INP2:ROUT SEP;"
            'Debug.Print ":INP2:ROUT SEP;"
        End If

        'Configure SENSe Options
        'Function PERIOD
        sCommand = ":SENS" & nChannel & ":FUNC " & Q & "PHAS" & Q & ";"
        'Gate Average always off for Frequency
        sCommand = sCommand & ":SENS" & nChannel & ":AVER:STAT OFF;"
        'Set Aquisition Time Out
        sCommand = sCommand & ":SENS:ATIM:TIME " & CStr(dAcquisitionTimeOut) & ";"
        'Set Visa TMO value based on dAcqusitionTimeOut or dAperature setting + 1 Second
        If dAcquisitionTimeOut > dAperature Then
            If Not bSimulation Then lSystErr = viSetAttribute(nInstrumentHandle(COUNTER), VI_ATTR_TMO_VALUE, (dAcquisitionTimeOut * 1000) + 1000)
        Else
            If Not bSimulation Then lSystErr = viSetAttribute(nInstrumentHandle(COUNTER), VI_ATTR_TMO_VALUE, (dAperature * 1000) + 1000)
        End If

        If Not bSimulation And lSystErr Then 'bbbb was bsimulatin
            lSystErr = viStatusDesc(iHandle, lSystErr, lpBuffer)
            Echo("WRITEMSG ERROR: Error sending command to " & sInstrumentDescription(COUNTER) & ".")
            Echo("COMMAND: viSetAttribute")
            MsgBox("Error sending: viSetAttribute VI_ATTR_TMO_VALUE" & vbCrLf & "ERROR: " & lpBuffer, MsgBoxStyle.Exclamation, "VISA Error")
            dMeasPhase = 0
            Exit Function
        End If

        'Set Aquisition Time Out Mode
        Select Case UCase(sAcquisitionTimeOutSetting)
            Case "ON", "OFF"
                sCommand = sCommand & ":SENS:ATIM:CHEC " & UCase(sAcquisitionTimeOutSetting) & ";"
            Case "START"
                sCommand = sCommand & ":SENS:ATIM:CHEC STAR;"
        End Select

        'Set Auto Trigger or absolute
        If bAutoTrigger Then
            sCommand = sCommand & ":SENS" & nChannel & ":EVEN:LEV:ABS:AUTO ON;"
            sCommand = sCommand & ":SENS" & nChannel & ":EVEN:LEV:REL " & CStr(nRelativeTriggerLevel) & ";"
        Else
            sCommand = sCommand & ":SENS" & nChannel & ":EVEN:LEV:ABS " & CStr(dAbsoluteTrigger) & ";"
            sCommand = sCommand & ":SENS" & nChannel & ":EVEN:LEV:ABS:AUTO OFF;"
        End If

        'Set Trigger Slop
        If Not bTriggerSlopeNegative Then
            sCommand = sCommand & ":SENS" & nChannel & ":EVEN:SLOP POS;"
        Else
            sCommand = sCommand & ":SENS" & nChannel & ":EVEN:SLOP NEG;"
        End If
        'Set Hysteresis

        sCommand = sCommand & ":SENS" & nChannel & ":EVEN:HYST " & UCase(sHysteresis) & ";"
        'Set Aperature
        sCommand = sCommand & ":SENS" & nChannel & ":FREQ:APER " & CStr(dAperature) & ";"
        sCommand = sCommand & ":SENS" & nChannel & ":PER:APER " & CStr(dAperature) & ";"
        sCommand = sCommand & ":SENS" & nChannel & ":RAT:APER " & CStr(dAperature) & ";"
        'Set Range
        sCommand = sCommand & ":SENS" & nChannel & ":FREQ:RANG:AUTO OFF;"
        'Set Timebase source
        Select Case UCase(sTimeBaseSource)
            Case "VXI"
                sCommand = sCommand & ":SENS" & nChannel & ":ROSC:SOUR CLK10;"
            Case "INTERNAL"
                sCommand = sCommand & ":SENS" & nChannel & ":ROSC:SOUR INT;"
            Case "EXTERNAL"
                sCommand = sCommand & ":SENS" & nChannel & ":ROSC:SOUR EXT;"
        End Select

        If bTimeIntervalDelay Then
            sCommand = sCommand & ":SENS" & nChannel & ":TINT:DEL:STAT ON;"
            sCommand = sCommand & ":SENS" & nChannel & ":TINT:DEL:TIME " & CStr(dTimeIntervalDelay) & ";"
        Else
            sCommand = sCommand & ":SENS" & nChannel & ":TINT:DEL:STAT OFF;"
        End If

        If bTotalizeGateMeasure Then
            sCommand = sCommand & ":SENS" & nChannel & ":TOT:GATE:STAT ON;"
            If bTotalizeGatePolarityInverted Then
                sCommand = sCommand & ":SENS" & nChannel & ":TOT:GATE:POL INV;"
            Else
                sCommand = sCommand & ":SENS" & nChannel & ":TOT:GATE:POL NORM;"
            End If
        Else
            sCommand = sCommand & ":SENS" & nChannel & ":TOT:GATE:STAT OFF;"
            sCommand = sCommand & ":SENS" & nChannel & ":TOT:GATE:POL NORM;"
        End If

        'Write SENSe Subset to instrument
        If Not bSimulation Then WriteMsg(COUNTER, sCommand)
        Debug.Print(sCommand)

        'Configure ARM Options
        sCommand = ""
        Select Case UCase(sArmStartSource)
            Case "IMMEDIATE"
                sCommand = sCommand & ":ARM:STAR:SOUR IMM;"
            Case "VXI"
                sCommand = sCommand & ":ARM:STAR:SOUR BUS;"
            Case "TTL0", "TTL1", "TTL2", "TTL3", "TTL4", "TTL5", "TTL6", "TTL7"
                sCommand = sCommand & ":ARM:STAR:SOUR TTLT" & Right(sArmStartSource, 1) & ";"
                If Not bArmStartSlopNegative Then
                    sCommand = sCommand & ":ARM:STAR:SLOP POS;"
                Else
                    sCommand = sCommand & ":ARM:STAR:SLOP NEG;"
                End If
            Case "EXTERNAL"
                sCommand = sCommand & ":ARM:STAR:SOUR EXT;"
                Select Case UCase(sArmStartLevel)
                    Case "TTL"
                        sCommand = sCommand & ":ARM:STAR:LEV MAX"
                    Case "ECL"
                        sCommand = sCommand & ":ARM:STAR:LEV MIN;"
                    Case "GND"
                        sCommand = sCommand & ":ARM:STAR:LEV DEF;"
                End Select

                If Not bArmStartSlopNegative Then
                    sCommand = sCommand & ":ARM:STAR:SLOP POS;"
                Else
                    sCommand = sCommand & ":ARM:STAR:SLOP NEG;"
                End If
        End Select

        Select Case UCase(SArmStopSource)
            Case "IMMEDIATE"
                sCommand = sCommand & ":ARM:STOP:SOUR IMM;"
            Case "VXI"
                sCommand = sCommand & ":ARM:STOP:SOUR BUS;"
            Case "TTL0", "TTL1", "TTL2", "TTL3", "TTL4", "TTL5", "TTL6", "TTL7"
                sCommand = sCommand & ":ARM:STOP:SOUR TTLT" & Right(SArmStopSource, 1) & ";"
                If Not bArmStopSlopNegative Then
                    sCommand = sCommand & ":ARM:STOP:SLOP POS;"
                Else
                    sCommand = sCommand & ":ARM:STOP:SLOP NEG;"
                End If
            Case "EXTERNAL"
                sCommand = sCommand & ":ARM:STOP:SOUR EXT;"
                Select Case UCase(sArmStopLevel)
                    Case "TTL"
                        sCommand = sCommand & ":ARM:STOP:LEV MAX"
                    Case "ECL"
                        sCommand = sCommand & ":ARM:STOP:LEV MIN;"
                    Case "GND"
                        sCommand = sCommand & ":ARM:STOP:LEV DEF;"
                End Select

                If Not bArmStartSlopNegative Then
                    sCommand = sCommand & ":ARM:STOP:SLOP POS;"
                Else
                    sCommand = sCommand & ":ARM:STOP:SLOP NEG;"
                End If
        End Select

        'Write ARM Subset to instrument
        If Not bSimulation Then WriteMsg(COUNTER, sCommand)
        Debug.Print(sCommand)

        'Configure OUTPut
        sCommand = ""
        If Not bTriggerOutputOn Then
            For i = 0 To 7
                sCommand = sCommand & ":OUTP:TTLT" & i & ":STAT OFF;"
            Next i
            sCommand = sCommand & ":OUTP:ROSC:STAT OFF;"
        Else
            For i = 0 To 7
                If i = nTTLTriggerOutput Then
                    sCommand = sCommand & ":OUTP:TTLT" & i & ":STAT ON;"
                Else
                    sCommand = sCommand & ":OUTP:TTLT" & i & ":STAT OFF;"
                End If
            Next i
            sCommand = sCommand & ":OUTP:ROSC:STAT ON;"
        End If
        'Write OUTPut Subset to instrument
        If Not bSimulation Then WriteMsg(COUNTER, sCommand)
        Debug.Print(sCommand)
        'Configure CONFigure Options
        sCommand = ""
        If dRange <> -1 Then
            sCommand = ":CONF" & nChannel & ":PHAS  1;"
            Debug.Print(sCommand)
        End If

        'Make Measurement
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        If Not bSimulation Then
            Select Case Left(UCase(APROBE), 1)
                Case "O" ' Off
                    WriteMsg(COUNTER, "INIT" & CStr(nChannel) & ";FETCH" & CStr(nChannel) & "?")
                    Debug.Print("INIT" & CStr(nChannel) & ";FETCH" & CStr(nChannel) & "?")

                    nErr = ReadMsg(COUNTER, sData)
                    If nErr <> 0 Then
                        dMeasPhase = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37)
                        Err.Raise(nErr)
                        Exit Function
                    End If

                    dMeasPhase = CDbl(sData)
                    gFrmMain.txtMeasured.Text = CStr(CSng(dMeasPhase))
                    If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
                Case "S" ' Single
                    MisProbe = MsgBoxResult.Yes
                    Do While MisProbe = MsgBoxResult.Yes
                        gFrmMain.lblStatus.Text = "Waiting for Probe ..."
                        Failed = False
                        UserEvent = 0
                        gFrmMain.TimerProbe.Enabled = True : gFrmMain.fraInstructions.Visible = True
                        Do While (UserEvent = 0) And (Not bProbeClosed)
                            System.Windows.Forms.Application.DoEvents()
                        Loop

                        gFrmMain.TimerProbe.Enabled = False
                        bProbeClosed = False
                        gFrmMain.lblStatus.Text = "Making Measurement ..."
                        WriteMsg(COUNTER, "INIT" & CStr(nChannel) & ";FETCH" & CStr(nChannel) & "?")
                        nErr = ReadMsg(COUNTER, sData)
                        If nErr <> 0 Then
                            dMeasPhase = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37)
                            Err.Raise(nErr)
                            Exit Function
                        End If

                        dMeasPhase = CDbl(sData)
                        gFrmMain.txtMeasured.Text = CStr(CSng(dMeasPhase))
                        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
                        gFrmMain.lblStatus.Text = "Measurement Complete ..."
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
                        Failed = False
                        UserEvent = 0
                        gFrmMain.TimerProbe.Enabled = True : gFrmMain.fraInstructions.Visible = True
                        Do While (UserEvent = 0) And (Not bProbeClosed)
                            gFrmMain.lblStatus.Text = "Making Measurement ..."
                            WriteMsg(COUNTER, "INIT" & CStr(nChannel) & ";FETCH" & CStr(nChannel) & "?")
                            nErr = ReadMsg(COUNTER, sData)
                            If nErr <> 0 Then
                                dMeasPhase = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37)
                                Err.Raise(nErr)
                                Exit Function
                            End If

                            dMeasPhase = CDbl(sData)
                            gFrmMain.txtMeasured.Text = CStr(CSng(dMeasPhase))
                            If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
                        Loop
                        gFrmMain.TimerProbe.Enabled = False
                        bProbeClosed = False
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
            End Select
        Else
            dMeasPhase = CDbl(InputBox("Command cmdCOUNTER.dMeasPhase peformed." & vbCrLf & "Enter Value:", "SIMULATION MODE"))
            gFrmMain.txtMeasured.Text = CStr(CSng(dMeasPhase))
            If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON)
        End If
    End Function

    Public Function dMeasRiseTime(Optional ByRef dRange As Double = 0.0000001, Optional ByRef bChOne10XAttenuation As Boolean = False,
                                  Optional ByRef bChTwo10XAttenuation As Boolean = False, Optional ByRef bChOneACCoupling As Boolean = False,
                                  Optional ByRef bChTwoACCoupling As Boolean = False, Optional ByRef bChOne50ohms As Boolean = False,
                                  Optional ByRef bChTwo50ohms As Boolean = False, Optional ByRef sHysteresis As String = "DEF",
                                  Optional ByRef bAutoTrigger As Boolean = False, Optional ByRef nRelativeTriggerLevel As Integer = 50,
                                  Optional ByRef bTriggerSlopeNegative As Boolean = False, Optional ByRef dAbsoluteTrigger As Double = 0.0,
                                  Optional ByRef bTriggerOutputOn As Boolean = False, Optional ByRef nTTLTriggerOutput As Integer = 0,
                                  Optional ByRef sArmStartSource As String = "IMMEDIATE", Optional ByRef bArmStartSlopNegative As Boolean = False,
                                  Optional ByRef sArmStartLevel As String = "TTL", Optional ByRef SArmStopSource As String = "IMMEDIATE",
                                  Optional ByRef bArmStopSlopNegative As Boolean = False, Optional ByRef sArmStopLevel As String = "TTL",
                                  Optional ByRef sTimeBaseSource As String = "INTERNAL", Optional ByRef bOutputTimeBase As Boolean = False,
                                  Optional ByRef bTotalizeGateMeasure As Boolean = False, Optional ByRef bTotalizeGatePolarityInverted As Boolean = False,
                                  Optional ByRef bTimeIntervalDelay As Boolean = False, Optional ByRef dTimeIntervalDelay As Double = 0.1,
                                  Optional ByRef sAcquisitionTimeOutSetting As String = "ON", Optional ByRef dAcquisitionTimeOut As Double = 5.0,
                                  Optional ByRef APROBE As String = "OFF") As Double
        Dim i As Short
        Dim sData As String = ""
        Dim nErr As Integer
        Dim sCommand As String
        Dim Q As String
        Dim nChannel As Short
        Dim sCurrentMsg As String = ""
        nChannel = 1
        Q = Chr(34)

        Dim iHandle As Integer
        iHandle = nInstrumentHandle(COUNTER)

        dMeasRiseTime = 0.0
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        gFrmMain.txtInstrument.Text = "Counter"
        gFrmMain.txtCommand.Text = "dMeasRiseTime"
        If APROBE <> "OFF" Then
            sCurrentMsg = gFrmMain.lblStatus.Text
            gFrmMain.lblStatus.Text = "APPLY PROBE"
        End If

        'Clear Counter
        If Not bSimulation Then
            '  viClear (nInstrumentHandle(COUNTER))
            nSystErr = atxmlDF_viClear(ResourceName(COUNTER), 0)
            WriteMsg(COUNTER, "*RST ; *CLS")
        End If

        'Error Check Arguments
        If nChannel < 1 Or nChannel > 2 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasRiseTime nChannel argument out of range.")
            Echo("RANGE: 1 TO 2")
            dMeasRiseTime = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
            dMeasRiseTime = 0
            Exit Function
        End If

        If dRange < 0.000000015 Or dRange > 0.001 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasRiseTime dRange argument out of range.")
            Echo("RANGE: 15E-9 TO 1E-3")
            dMeasRiseTime = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
            dMeasRiseTime = 0
            Exit Function
        End If

        Select Case UCase(sHysteresis)
            Case "DEF", "MAX", "MIN"
            Case Else
                Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasRiseTime sHysteresis argument out of range.")
                Echo("RANGE: DEF, MAX or MIN")
                dMeasRiseTime = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                dMeasRiseTime = 0
                Exit Function
        End Select

        If nRelativeTriggerLevel < 10 Or nRelativeTriggerLevel > 90 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasRiseTime nRelativeTriggerLevel argument out of range.")
            Echo("RANGE: 10 to 90")
            dMeasRiseTime = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
            dMeasRiseTime = 0
            Exit Function
        End If

        If dAbsoluteTrigger < -10.238 Or dAbsoluteTrigger > 10.238 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasRiseTime dAbsoluteTrigger argument out of range.")
            Echo("RANGE: -10.238 to 10.238")
            dMeasRiseTime = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
            dMeasRiseTime = 0
            Exit Function
        End If

        If nTTLTriggerOutput < 0 Or nTTLTriggerOutput > 7 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasRiseTime nTTLTriggerOutput argument out of range.")
            Echo("RANGE: 0 to 7")
            dMeasRiseTime = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
            dMeasRiseTime = 0
            Exit Function
        End If

        Select Case UCase(sArmStartSource)
            Case "IMMEDIATE", "VXI"
            Case "TTL0", "TTL1", "TTL2", "TTL3", "TTL4", "TTL5", "TTL6", "TTL7"
            Case "EXTERNAL"
                Select Case UCase(sArmStartLevel)
                    Case "TTL", "ECL", "GND"
                    Case Else
                        Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasRiseTime sArmStartLevel argument out of range.")
                        Echo("RANGE: TTL, ECL or GND")
                        dMeasRiseTime = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                        dMeasRiseTime = 0
                End Select
            Case Else
                Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasRiseTime sArmStartSource argument out of range.")
                Echo("RANGE: IMMEDIATE, VXI, TTL0, TTL1, TTL2, TTL3, TTL4, TTL5, TTL6, TTL7 or EXTERNAL")
                dMeasRiseTime = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                dMeasRiseTime = 0
                Exit Function
        End Select

        Select Case UCase(SArmStopSource)
            Case "IMMEDIATE", "VXI"
            Case "TTL0", "TTL1", "TTL2", "TTL3", "TTL4", "TTL5", "TTL6", "TTL7"
            Case "EXTERNAL"
                Select Case UCase(sArmStopLevel)
                    Case "TTL", "ECL", "GND"
                    Case Else
                        Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasRiseTime sArmStopLevel argument out of range.")
                        Echo("RANGE: TTL, ECL or GND")
                        dMeasRiseTime = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                        dMeasRiseTime = 0
                End Select
            Case Else
                Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasRiseTime sArmStopSource argument out of range.")
                Echo("RANGE: IMMEDIATE, VXI, TTL0, TTL1, TTL2, TTL3, TTL4, TTL5, TTL6, TTL7 or EXTERNAL")
                dMeasRiseTime = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                dMeasRiseTime = 0
                Exit Function
        End Select

        Select Case UCase(sTimeBaseSource)
            Case "VXI", "INTERNAL", "EXTERNAL"
            Case Else
                Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasRiseTime sTimeBaseSource argument out of range.")
                Echo("RANGE: VXI, INTERNAL or EXTERNAL")
                dMeasRiseTime = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                dMeasRiseTime = 0
                Exit Function
        End Select

        If dTimeIntervalDelay < 0.001 Or dTimeIntervalDelay > 99.999 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasRiseTime dTimeIntervalDelay argument out of range.")
            Echo("RANGE: 0.001 to 99.999")
            dMeasRiseTime = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
            dMeasRiseTime = 0
            Exit Function
        End If

        Select Case UCase(sAcquisitionTimeOutSetting)
            Case "ON", "OFF", "START"
            Case Else
                Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasRiseTime sAcquisitionTimeOutSetting argument out of range.")
                Echo("RANGE: ON, OFF, or START")
                dMeasRiseTime = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                dMeasRiseTime = 0
                Exit Function
        End Select

        If dAcquisitionTimeOut < 0.1 Or dAcquisitionTimeOut > 1500 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasRiseTime dAcquisitionTimeOut argument out of range.")
            Echo("RANGE: 0.1 to 1500")
            dMeasRiseTime = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
            dMeasRiseTime = 0
            Exit Function
        End If

        Select Case Left(UCase(APROBE), 1)
            Case "S", "O", "C" ' Single, Off, Continuous
            Case Else
                Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasRiseTime APROBE argument out of range.")
                Echo("RANGE: SINGLE or OFF")
                dMeasRiseTime = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                dMeasRiseTime = 0
                Exit Function
        End Select

        'Configure Counter for Measurement
        'Set Input Options
        'Clear Counter
        If Not bSimulation Then
            '  viClear (nInstrumentHandle(COUNTER))
            nSystErr = atxmlDF_viClear(ResourceName(COUNTER), 0)
            WriteMsg(COUNTER, "*RST ; *CLS")
        End If

        sCommand = ""
        'Configure INPut Subsystem
        If nChannel = 1 Then
            'Set Attenuation
            If Not bChOne10XAttenuation Then
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:ATT 1;")
                Debug.Print(":INP1:ATT 1;")
            Else
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:ATT 10;")
                Debug.Print(":INP1:ATT 10;")
            End If

            'Set Coupling
            If Not bChOneACCoupling Then 'bbbb was simulation
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:COUP DC;")
                Debug.Print(":INP1:COUP DC;")
            Else
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:COUP AC;")
                Debug.Print(":INP1:COUP AC;")
            End If

            'Set Impedance
            If Not bChOne50ohms Then 'bbbb was simulation
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:IMP 1E6;")
                Debug.Print(":INP1:IMP 1E6;")
            Else
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:IMP 50;")
                Debug.Print(":INP1:IMP 50;")
            End If

            'Set Routing (Always separate for Period) 'bbbb was simulation
            If Not bSimulation Then WriteMsg(COUNTER, ":INP1:ROUT SEP;")
            Debug.Print(":INP1:ROUT SEP;")
        Else ' Channel 2
            'Set Attenuation
            If Not bChTwo10XAttenuation Then
                If Not bSimulation Then WriteMsg(COUNTER, ":INP2:ATT 1;")
                Debug.Print(":INP2:ATT 1;")
            Else
                If Not bSimulation Then WriteMsg(COUNTER, ":INP2:ATT 10;")
                Debug.Print(":INP2:ATT 10;")
            End If

            'Set Coupling
            If Not bChTwoACCoupling Then 'bbbb was simulation
                If Not bSimulation Then WriteMsg(COUNTER, ":INP2:COUP DC;")
                Debug.Print(":INP2:COUP DC;")
            Else
                If Not bSimulation Then WriteMsg(COUNTER, ":INP2:COUP AC;")
                Debug.Print(":INP2:COUP AC;")
            End If

            'Set Impedance
            If Not bChTwo50ohms Then 'bbbb was simulation
                If Not bSimulation Then WriteMsg(COUNTER, ":INP2:IMP 1E6;")
                Debug.Print(":INP2:IMP 1E6;")
            Else
                If Not bSimulation Then WriteMsg(COUNTER, ":INP2:IMP 50;")
                Debug.Print(":INP2:IMP 50;")
            End If
            'Set Routing (Always separate for Period)
            'If Not bSimulation Then WriteMsg COUNTER, ":INP2:ROUT SEP;"
            'Debug.Print ":INP2:ROUT SEP;"
        End If

        'Configure SENSe Options
        'Function PERIOD
        sCommand = ":SENS" & nChannel & ":FUNC " & Q & "RTIM" & Q & ";"
        'Gate Average always off for Frequency
        sCommand = sCommand & ":SENS" & nChannel & ":AVER:STAT OFF;"
        'Set Aquisition Time Out
        sCommand = sCommand & ":SENS:ATIM:TIME " & CStr(dAcquisitionTimeOut) & ";"
        'Set Visa TMO value based on dAcqusitionTimeOut or dAperature setting + 1 Second
        If dAcquisitionTimeOut > dAperature Then
            If Not bSimulation Then lSystErr = viSetAttribute(nInstrumentHandle(COUNTER), VI_ATTR_TMO_VALUE, (dAcquisitionTimeOut * 1000) + 1000)
        Else
            If Not bSimulation Then lSystErr = viSetAttribute(nInstrumentHandle(COUNTER), VI_ATTR_TMO_VALUE, (dAperature * 1000) + 1000)
        End If

        If Not bSimulation And lSystErr Then 'bbbb was bsimulaton
            lSystErr = viStatusDesc(iHandle, lSystErr, lpBuffer)
            Echo("WRITEMSG ERROR: Error sending command to " & sInstrumentDescription(COUNTER) & ".")
            Echo("COMMAND: viSetAttribute")
            MsgBox("Error sending: viSetAttribute VI_ATTR_TMO_VALUE" & vbCrLf & "ERROR: " & lpBuffer, MsgBoxStyle.Exclamation, "VISA Error")
            dMeasRiseTime = 0
            Exit Function
        End If

        'Set Aquisition Time Out Mode
        Select Case UCase(sAcquisitionTimeOutSetting)
            Case "ON", "OFF"
                sCommand = sCommand & ":SENS:ATIM:CHEC " & UCase(sAcquisitionTimeOutSetting) & ";"
            Case "START"
                sCommand = sCommand & ":SENS:ATIM:CHEC STAR;"
        End Select

        'Set Auto Trigger or absolute
        If bAutoTrigger Then
            sCommand = sCommand & ":SENS" & nChannel & ":EVEN:LEV:ABS:AUTO ON;"
            sCommand = sCommand & ":SENS" & nChannel & ":EVEN:LEV:REL " & CStr(nRelativeTriggerLevel) & ";"
        Else
            sCommand = sCommand & ":SENS" & nChannel & ":EVEN:LEV:ABS " & CStr(dAbsoluteTrigger) & ";"
            sCommand = sCommand & ":SENS" & nChannel & ":EVEN:LEV:ABS:AUTO OFF;"
        End If

        'Set Trigger Slop
        If Not bTriggerSlopeNegative Then
            sCommand = sCommand & ":SENS" & nChannel & ":EVEN:SLOP POS;"
        Else
            sCommand = sCommand & ":SENS" & nChannel & ":EVEN:SLOP NEG;"
        End If

        'Set Hysteresis
        sCommand = sCommand & ":SENS" & nChannel & ":EVEN:HYST " & UCase(sHysteresis) & ";"
        'Set Aperature
        sCommand = sCommand & ":SENS" & nChannel & ":FREQ:APER " & CStr(dAperature) & ";"
        sCommand = sCommand & ":SENS" & nChannel & ":PER:APER " & CStr(dAperature) & ";"
        sCommand = sCommand & ":SENS" & nChannel & ":RAT:APER " & CStr(dAperature) & ";"
        'Set Range
        sCommand = sCommand & ":SENS" & nChannel & ":FREQ:RANG:AUTO OFF;"
        'Set Timebase source
        Select Case UCase(sTimeBaseSource)
            Case "VXI"
                sCommand = sCommand & ":SENS" & nChannel & ":ROSC:SOUR CLK10;"
            Case "INTERNAL"
                sCommand = sCommand & ":SENS" & nChannel & ":ROSC:SOUR INT;"
            Case "EXTERNAL"
                sCommand = sCommand & ":SENS" & nChannel & ":ROSC:SOUR EXT;"
        End Select

        If bTimeIntervalDelay Then
            sCommand = sCommand & ":SENS" & nChannel & ":TINT:DEL:STAT ON;"
            sCommand = sCommand & ":SENS" & nChannel & ":TINT:DEL:TIME " & CStr(dTimeIntervalDelay) & ";"
        Else
            sCommand = sCommand & ":SENS" & nChannel & ":TINT:DEL:STAT OFF;"
        End If

        If bTotalizeGateMeasure Then
            sCommand = sCommand & ":SENS" & nChannel & ":TOT:GATE:STAT ON;"
            If bTotalizeGatePolarityInverted Then
                sCommand = sCommand & ":SENS" & nChannel & ":TOT:GATE:POL INV;"
            Else
                sCommand = sCommand & ":SENS" & nChannel & ":TOT:GATE:POL NORM;"
            End If
        Else
            sCommand = sCommand & ":SENS" & nChannel & ":TOT:GATE:STAT OFF;"
            sCommand = sCommand & ":SENS" & nChannel & ":TOT:GATE:POL NORM;"
        End If

        'Write SENSe Subset to instrument
        If Not bSimulation Then WriteMsg(COUNTER, sCommand)
        Debug.Print(sCommand)
        'Configure ARM Options
        sCommand = ""
        Select Case UCase(sArmStartSource)
            Case "IMMEDIATE"
                sCommand = sCommand & ":ARM:STAR:SOUR IMM;"
            Case "VXI"
                sCommand = sCommand & ":ARM:STAR:SOUR BUS;"
            Case "TTL0", "TTL1", "TTL2", "TTL3", "TTL4", "TTL5", "TTL6", "TTL7"
                sCommand = sCommand & ":ARM:STAR:SOUR TTLT" & Right(sArmStartSource, 1) & ";"
                If Not bArmStartSlopNegative Then
                    sCommand = sCommand & ":ARM:STAR:SLOP POS;"
                Else
                    sCommand = sCommand & ":ARM:STAR:SLOP NEG;"
                End If
            Case "EXTERNAL"
                sCommand = sCommand & ":ARM:STAR:SOUR EXT;"
                Select Case UCase(sArmStartLevel)
                    Case "TTL"
                        sCommand = sCommand & ":ARM:STAR:LEV MAX"
                    Case "ECL"
                        sCommand = sCommand & ":ARM:STAR:LEV MIN;"
                    Case "GND"
                        sCommand = sCommand & ":ARM:STAR:LEV DEF;"
                End Select
                If Not bArmStartSlopNegative Then
                    sCommand = sCommand & ":ARM:STAR:SLOP POS;"
                Else
                    sCommand = sCommand & ":ARM:STAR:SLOP NEG;"
                End If
        End Select

        Select Case UCase(SArmStopSource)
            Case "IMMEDIATE"
                sCommand = sCommand & ":ARM:STOP:SOUR IMM;"
            Case "VXI"
                sCommand = sCommand & ":ARM:STOP:SOUR BUS;"
            Case "TTL0", "TTL1", "TTL2", "TTL3", "TTL4", "TTL5", "TTL6", "TTL7"
                sCommand = sCommand & ":ARM:STOP:SOUR TTLT" & Right(SArmStopSource, 1) & ";"
                If Not bArmStopSlopNegative Then
                    sCommand = sCommand & ":ARM:STOP:SLOP POS;"
                Else
                    sCommand = sCommand & ":ARM:STOP:SLOP NEG;"
                End If
            Case "EXTERNAL"
                sCommand = sCommand & ":ARM:STOP:SOUR EXT;"
                Select Case UCase(sArmStopLevel)
                    Case "TTL"
                        sCommand = sCommand & ":ARM:STOP:LEV MAX"
                    Case "ECL"
                        sCommand = sCommand & ":ARM:STOP:LEV MIN;"
                    Case "GND"
                        sCommand = sCommand & ":ARM:STOP:LEV DEF;"
                End Select
                If Not bArmStartSlopNegative Then
                    sCommand = sCommand & ":ARM:STOP:SLOP POS;"
                Else
                    sCommand = sCommand & ":ARM:STOP:SLOP NEG;"
                End If
        End Select
        'Write ARM Subset to instrument
        If Not bSimulation Then WriteMsg(COUNTER, sCommand)
        Debug.Print(sCommand)

        'Configure OUTPut
        sCommand = ""
        If Not bTriggerOutputOn Then
            For i = 0 To 7
                sCommand = sCommand & ":OUTP:TTLT" & i & ":STAT OFF;"
            Next i
            sCommand = sCommand & ":OUTP:ROSC:STAT OFF;"
        Else
            For i = 0 To 7
                If i = nTTLTriggerOutput Then
                    sCommand = sCommand & ":OUTP:TTLT" & i & ":STAT ON;"
                Else
                    sCommand = sCommand & ":OUTP:TTLT" & i & ":STAT OFF;"
                End If
            Next i
            sCommand = sCommand & ":OUTP:ROSC:STAT ON;"
        End If
        'Write OUTPut Subset to instrument
        If Not bSimulation Then WriteMsg(COUNTER, sCommand)
        Debug.Print(sCommand)

        'Configure CONFigure Options
        sCommand = ""
        If dRange <> -1 Then
            sCommand = ":CONF" & nChannel & ":RTIM 90, 10, " & CStr(dRange) & ";"
            Debug.Print(sCommand)
        End If

        'Make Measurement
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        If Not bSimulation Then
            Select Case Left(UCase(APROBE), 1)
                Case "O" ' Off
                    WriteMsg(COUNTER, "INIT" & CStr(nChannel) & ";FETCH" & CStr(nChannel) & "?")
                    Debug.Print("INIT" & CStr(nChannel) & ";FETCH" & CStr(nChannel) & "?")
                    nErr = ReadMsg(COUNTER, sData)
                    If nErr <> 0 Then
                        dMeasRiseTime = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37)
                        Err.Raise(nErr)
                        Exit Function
                    End If

                    dMeasRiseTime = CDbl(sData)
                    gFrmMain.txtMeasured.Text = CStr(CSng(dMeasRiseTime))
                    If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
                Case "S" ' Single
                    MisProbe = MsgBoxResult.Yes
                    Do While MisProbe = MsgBoxResult.Yes
                        gFrmMain.lblStatus.Text = "Waiting for Probe ..."
                        Failed = False
                        UserEvent = 0
                        gFrmMain.TimerProbe.Enabled = True : gFrmMain.fraInstructions.Visible = True
                        Do While (UserEvent = 0) And (Not bProbeClosed)
                            System.Windows.Forms.Application.DoEvents()
                        Loop

                        gFrmMain.TimerProbe.Enabled = False
                        bProbeClosed = False
                        gFrmMain.lblStatus.Text = "Making Measurement ..."
                        WriteMsg(COUNTER, "INIT" & CStr(nChannel) & ";FETCH" & CStr(nChannel) & "?")
                        nErr = ReadMsg(COUNTER, sData)
                        If nErr <> 0 Then
                            dMeasRiseTime = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37)
                            Err.Raise(nErr)
                            Exit Function
                        End If

                        dMeasRiseTime = CDbl(sData)
                        gFrmMain.txtMeasured.Text = CStr(CSng(dMeasRiseTime))
                        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
                        gFrmMain.lblStatus.Text = "Measurement Complete ..."
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
                        Failed = False
                        UserEvent = 0
                        gFrmMain.TimerProbe.Enabled = True : gFrmMain.fraInstructions.Visible = True
                        Do While (UserEvent = 0) And (Not bProbeClosed)
                            gFrmMain.lblStatus.Text = "Making Measurement ..."
                            WriteMsg(COUNTER, "INIT" & CStr(nChannel) & ";FETCH" & CStr(nChannel) & "?")
                            nErr = ReadMsg(COUNTER, sData)
                            If nErr <> 0 Then
                                dMeasRiseTime = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37)
                                Err.Raise(nErr)
                                Exit Function
                            End If

                            dMeasRiseTime = CDbl(sData)
                            gFrmMain.txtMeasured.Text = CStr(CSng(dMeasRiseTime))
                            If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
                        Loop
                        gFrmMain.TimerProbe.Enabled = False
                        bProbeClosed = False
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
            End Select
        Else
            dMeasRiseTime = CDbl(InputBox("Command cmdCOUNTER.dMeasRiseTime peformed." & vbCrLf & "Enter Value:", "SIMULATION MODE"))
            gFrmMain.txtMeasured.Text = CStr(CSng(dMeasRiseTime))
            If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON)
        End If
    End Function

    Public Function dMeasPeriod(Optional ByRef nChannel As Short = 1, Optional ByRef dRange As Double = 0.0000001,
                                Optional ByRef dAperature As Double = 0.1, Optional ByRef bChOne10XAttenuation As Boolean = False,
                                Optional ByRef bChTwo10XAttenuation As Boolean = False, Optional ByRef bChOneACCoupling As Boolean = False,
                                Optional ByRef bChTwoACCoupling As Boolean = False, Optional ByRef bChOne50ohms As Boolean = False,
                                Optional ByRef bChTwo50ohms As Boolean = False, Optional ByRef sHysteresis As String = "DEF",
                                Optional ByRef bAutoTrigger As Boolean = False, Optional ByRef nRelativeTriggerLevel As Integer = 50,
                                Optional ByRef bTriggerSlopeNegative As Boolean = False, Optional ByRef dAbsoluteTrigger As Double = 0.0,
                                Optional ByRef bTriggerOutputOn As Boolean = False, Optional ByRef nTTLTriggerOutput As Integer = 0,
                                Optional ByRef sArmStartSource As String = "IMMEDIATE", Optional ByRef bArmStartSlopNegative As Boolean = False,
                                Optional ByRef sArmStartLevel As String = "TTL", Optional ByRef SArmStopSource As String = "IMMEDIATE",
                                Optional ByRef bArmStopSlopNegative As Boolean = False, Optional ByRef sArmStopLevel As String = "TTL",
                                Optional ByRef sTimeBaseSource As String = "INTERNAL", Optional ByRef bOutputTimeBase As Boolean = False,
                                Optional ByRef bTotalizeGateMeasure As Boolean = False, Optional ByRef bTotalizeGatePolarityInverted As Boolean = False,
                                Optional ByRef bTimeIntervalDelay As Boolean = False, Optional ByRef dTimeIntervalDelay As Double = 0.1,
                                Optional ByRef sAcquisitionTimeOutSetting As String = "ON", Optional ByRef dAcquisitionTimeOut As Double = 5.0,
                                Optional ByRef APROBE As String = "OFF") As Double
        Dim i As Short
        Dim sData As String = ""
        Dim nErr As Integer
        Dim sCommand As String
        Dim Q As String
        Dim sCurrentMsg As String = ""
        Q = Chr(34)

        Dim iHandle As Integer
        iHandle = nInstrumentHandle(COUNTER)

        dMeasPeriod = -0.0
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        gFrmMain.txtInstrument.Text = "Counter"
        gFrmMain.txtCommand.Text = "dMeasPeriod"
        If APROBE <> "OFF" Then
            sCurrentMsg = gFrmMain.lblStatus.Text
            gFrmMain.lblStatus.Text = "APPLY PROBE"
        End If

        'Clear Counter
        If Not bSimulation Then
            '  viClear (nInstrumentHandle(COUNTER))
            nSystErr = atxmlDF_viClear(ResourceName(COUNTER), 0)
            WriteMsg(COUNTER, "*RST ; *CLS")
        End If

        'Error Check Arguments
        If nChannel < 1 Or nChannel > 2 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasPeriod nChannel argument out of range.")
            Echo("RANGE: 1 TO 2")
            dMeasPeriod = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
            dMeasPeriod = 0
            Exit Function
        End If

        If dRange < 0.000000005 Or dRange > 1000 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasPeriod dRange argument out of range.")
            Echo("RANGE: 5E-9 TO 1000")
            dMeasPeriod = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
            dMeasPeriod = 0
            Exit Function
        End If

        If dAperature < 0.1 Or dAperature > 99.999 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasPeriod dAperature argument out of range.")
            Echo("RANGE: 0.1 TO 99.999")
            dMeasPeriod = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
            dMeasPeriod = 0
            Exit Function
        End If
        Select Case UCase(sHysteresis)
            Case "DEF", "MAX", "MIN"
            Case Else
                Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasPeriod sHysteresis argument out of range.")
                Echo("RANGE: DEF, MAX or MIN")
                dMeasPeriod = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                dMeasPeriod = 0
                Exit Function
        End Select

        If nRelativeTriggerLevel < 10 Or nRelativeTriggerLevel > 90 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasPeriod nRelativeTriggerLevel argument out of range.")
            Echo("RANGE: 10 to 90")
            dMeasPeriod = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
            dMeasPeriod = 0
            Exit Function
        End If

        If dAbsoluteTrigger < -10.238 Or dAbsoluteTrigger > 10.238 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasPeriod dAbsoluteTrigger argument out of range.")
            Echo("RANGE: -10.238 to 10.238")
            dMeasPeriod = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
            dMeasPeriod = 0
            Exit Function
        End If

        If nTTLTriggerOutput < 0 Or nTTLTriggerOutput > 7 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasPeriod nTTLTriggerOutput argument out of range.")
            Echo("RANGE: 0 to 7")
            dMeasPeriod = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
            dMeasPeriod = 0
            Exit Function
        End If

        Select Case UCase(sArmStartSource)
            Case "IMMEDIATE", "VXI"
            Case "TTL0", "TTL1", "TTL2", "TTL3", "TTL4", "TTL5", "TTL6", "TTL7"
            Case "EXTERNAL"
                Select Case UCase(sArmStartLevel)
                    Case "TTL", "ECL", "GND"
                    Case Else
                        Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasPeriod sArmStartLevel argument out of range.")
                        Echo("RANGE: TTL, ECL or GND")
                        dMeasPeriod = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                        dMeasPeriod = 0
                End Select
            Case Else
                Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasPeriod sArmStartSource argument out of range.")
                Echo("RANGE: IMMEDIATE, VXI, TTL0, TTL1, TTL2, TTL3, TTL4, TTL5, TTL6, TTL7 or EXTERNAL")
                dMeasPeriod = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                dMeasPeriod = 0
                Exit Function
        End Select

        Select Case UCase(SArmStopSource)
            Case "IMMEDIATE", "VXI"
            Case "TTL0", "TTL1", "TTL2", "TTL3", "TTL4", "TTL5", "TTL6", "TTL7"
            Case "EXTERNAL"
                Select Case UCase(sArmStopLevel)
                    Case "TTL", "ECL", "GND"
                    Case Else
                        Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasPeriod sArmStopLevel argument out of range.")
                        Echo("RANGE: TTL, ECL or GND")
                        dMeasPeriod = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                        dMeasPeriod = 0
                End Select
            Case Else
                Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasPeriod sArmStopSource argument out of range.")
                Echo("RANGE: IMMEDIATE, VXI, TTL0, TTL1, TTL2, TTL3, TTL4, TTL5, TTL6, TTL7 or EXTERNAL")
                dMeasPeriod = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                dMeasPeriod = 0
                Exit Function
        End Select

        Select Case UCase(sTimeBaseSource)
            Case "VXI", "INTERNAL", "EXTERNAL"
            Case Else
                Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasPeriod sTimeBaseSource argument out of range.")
                Echo("RANGE: VXI, INTERNAL or EXTERNAL")
                dMeasPeriod = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                dMeasPeriod = 0
                Exit Function
        End Select

        If dTimeIntervalDelay < 0.001 Or dTimeIntervalDelay > 99.999 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasPeriod dTimeIntervalDelay argument out of range.")
            Echo("RANGE: 0.001 to 99.999")
            dMeasPeriod = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
            dMeasPeriod = 0
            Exit Function
        End If

        Select Case UCase(sAcquisitionTimeOutSetting)
            Case "ON", "OFF", "START"
            Case Else
                Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasPeriod sAcquisitionTimeOutSetting argument out of range.")
                Echo("RANGE: ON, OFF, or START")
                dMeasPeriod = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                dMeasPeriod = 0
                Exit Function
        End Select

        If dAcquisitionTimeOut < 0.1 Or dAcquisitionTimeOut > 1500 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasPeriod dAcquisitionTimeOut argument out of range.")
            Echo("RANGE: 0.1 to 1500")
            dMeasPeriod = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
            dMeasPeriod = 0
            Exit Function
        End If

        Select Case Left(UCase(APROBE), 1)
            Case "S", "O", "C" ' Single, Off, Continuous
            Case Else
                Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasPeriod APROBE argument out of range.")
                Echo("RANGE: SINGLE or OFF")
                dMeasPeriod = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                dMeasPeriod = 0
                Exit Function
        End Select

        'Configure Counter for Measurement
        'Set Input Options
        'Clear Counter
        If Not bSimulation Then
            '  viClear (nInstrumentHandle(COUNTER))
            nSystErr = atxmlDF_viClear(ResourceName(COUNTER), 0)
            WriteMsg(COUNTER, "*RST ; *CLS")
        End If

        sCommand = ""
        'Configure INPut Subsystem
        If nChannel = 1 Then
            'Set Attenuation
            If Not bChOne10XAttenuation Then
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:ATT 1;")
                Debug.Print(":INP1:ATT 1;")
            Else
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:ATT 10;")
                Debug.Print(":INP1:ATT 10;")
            End If

            'Set Coupling
            If Not bChOneACCoupling Then 'bbbb was simulation
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:COUP DC;")
                Debug.Print(":INP1:COUP DC;")
            Else
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:COUP AC;")
                Debug.Print(":INP1:COUP AC;")
            End If

            'Set Impedance
            If Not bChOne50ohms Then 'bbbb was simulation
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:IMP 1E6;")
                Debug.Print(":INP1:IMP 1E6;")
            Else
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:IMP 50;")
                Debug.Print(":INP1:IMP 50;")
            End If

            'Set Routing (Always separate for Period) 'bbbb was simulation
            If Not bSimulation Then WriteMsg(COUNTER, ":INP1:ROUT SEP;")
            Debug.Print(":INP1:ROUT SEP;")
        Else ' Channel 2
            'Set Attenuation
            If Not bChTwo10XAttenuation Then
                If Not bSimulation Then WriteMsg(COUNTER, ":INP2:ATT 1;")
                Debug.Print(":INP2:ATT 1;")
            Else
                If Not bSimulation Then WriteMsg(COUNTER, ":INP2:ATT 10;")
                Debug.Print(":INP2:ATT 10;")
            End If

            'Set Coupling
            If Not bChTwoACCoupling Then 'bbbb was simulation
                If Not bSimulation Then WriteMsg(COUNTER, ":INP2:COUP DC;")
                Debug.Print(":INP2:COUP DC;")
            Else
                If Not bSimulation Then WriteMsg(COUNTER, ":INP2:COUP AC;")
                Debug.Print(":INP2:COUP AC;")
            End If

            'Set Impedance
            If Not bChTwo50ohms Then 'bbbb was simulation
                If Not bSimulation Then WriteMsg(COUNTER, ":INP2:IMP 1E6;")
                Debug.Print(":INP2:IMP 1E6;")
            Else
                If Not bSimulation Then WriteMsg(COUNTER, ":INP2:IMP 50;")
                Debug.Print(":INP2:IMP 50;")
            End If
            'Set Routing (Always separate for Period)
            'If Not simulation Then WriteMsg COUNTER, ":INP2:ROUT SEP;"
            'Debug.Print ":INP2:ROUT SEP;"
        End If

        'Configure SENSe Options
        'Function PERIOD
        sCommand = ":SENS" & nChannel & ":FUNC " & Q & "PER" & Q & ";"
        'Gate Average always off for Frequency
        sCommand = sCommand & ":SENS" & nChannel & ":AVER:STAT OFF;"
        'Set Aquisition Time Out
        sCommand = sCommand & ":SENS:ATIM:TIME " & CStr(dAcquisitionTimeOut) & ";"
        'Set Visa TMO value based on dAcqusitionTimeOut or dAperature setting + 1 Second
        If dAcquisitionTimeOut > dAperature Then
            If Not bSimulation Then lSystErr = viSetAttribute(nInstrumentHandle(COUNTER), VI_ATTR_TMO_VALUE, (dAcquisitionTimeOut * 1000) + 1000)
        Else
            If Not bSimulation Then lSystErr = viSetAttribute(nInstrumentHandle(COUNTER), VI_ATTR_TMO_VALUE, (dAperature * 1000) + 1000)
        End If

        If Not bSimulation And lSystErr Then 'bbbb was bsimulaton
            lSystErr = viStatusDesc(iHandle, lSystErr, lpBuffer)
            Echo("WRITEMSG ERROR: Error sending command to " & sInstrumentDescription(COUNTER) & ".")
            Echo("COMMAND: viSetAttribute")
            MsgBox("Error sending: viSetAttribute VI_ATTR_TMO_VALUE" & vbCrLf & "ERROR: " & lpBuffer, MsgBoxStyle.Exclamation, "VISA Error")
            dMeasPeriod = 0
            Exit Function
        End If

        'Set Aquisition Time Out Mode
        Select Case UCase(sAcquisitionTimeOutSetting)
            Case "ON", "OFF"
                sCommand = sCommand & ":SENS:ATIM:CHEC " & UCase(sAcquisitionTimeOutSetting) & ";"
            Case "START"
                sCommand = sCommand & ":SENS:ATIM:CHEC STAR;"
        End Select

        'Set Auto Trigger or absolute
        If bAutoTrigger Then
            sCommand = sCommand & ":SENS" & nChannel & ":EVEN:LEV:ABS:AUTO ON;"
            sCommand = sCommand & ":SENS" & nChannel & ":EVEN:LEV:REL " & CStr(nRelativeTriggerLevel) & ";"
        Else
            sCommand = sCommand & ":SENS" & nChannel & ":EVEN:LEV:ABS " & CStr(dAbsoluteTrigger) & ";"
            sCommand = sCommand & ":SENS" & nChannel & ":EVEN:LEV:ABS:AUTO OFF;"
        End If

        'Set Trigger Slop
        If Not bTriggerSlopeNegative Then
            sCommand = sCommand & ":SENS" & nChannel & ":EVEN:SLOP POS;"
        Else
            sCommand = sCommand & ":SENS" & nChannel & ":EVEN:SLOP NEG;"
        End If

        'Set Hysteresis
        sCommand = sCommand & ":SENS" & nChannel & ":EVEN:HYST " & UCase(sHysteresis) & ";"
        'Set Aperature
        sCommand = sCommand & ":SENS" & nChannel & ":FREQ:APER " & CStr(dAperature) & ";"
        sCommand = sCommand & ":SENS" & nChannel & ":PER:APER " & CStr(dAperature) & ";"
        sCommand = sCommand & ":SENS" & nChannel & ":RAT:APER " & CStr(dAperature) & ";"
        'Set Range
        sCommand = sCommand & ":SENS" & nChannel & ":FREQ:RANG:AUTO OFF;"
        'Set Timebase source
        Select Case UCase(sTimeBaseSource)
            Case "VXI"
                sCommand = sCommand & ":SENS" & nChannel & ":ROSC:SOUR CLK10;"
            Case "INTERNAL"
                sCommand = sCommand & ":SENS" & nChannel & ":ROSC:SOUR INT;"
            Case "EXTERNAL"
                sCommand = sCommand & ":SENS" & nChannel & ":ROSC:SOUR EXT;"
        End Select

        If bTimeIntervalDelay Then
            sCommand = sCommand & ":SENS" & nChannel & ":TINT:DEL:STAT ON;"
            sCommand = sCommand & ":SENS" & nChannel & ":TINT:DEL:TIME " & CStr(dTimeIntervalDelay) & ";"
        Else
            sCommand = sCommand & ":SENS" & nChannel & ":TINT:DEL:STAT OFF;"
        End If

        If bTotalizeGateMeasure Then
            sCommand = sCommand & ":SENS" & nChannel & ":TOT:GATE:STAT ON;"
            If bTotalizeGatePolarityInverted Then
                sCommand = sCommand & ":SENS" & nChannel & ":TOT:GATE:POL INV;"
            Else
                sCommand = sCommand & ":SENS" & nChannel & ":TOT:GATE:POL NORM;"
            End If
        Else
            sCommand = sCommand & ":SENS" & nChannel & ":TOT:GATE:STAT OFF;"
            sCommand = sCommand & ":SENS" & nChannel & ":TOT:GATE:POL NORM;"
        End If

        'Write SENSe Subset to instrument
        If Not bSimulation Then WriteMsg(COUNTER, sCommand)
        Debug.Print(sCommand)

        'Configure ARM Options
        sCommand = ""
        Select Case UCase(sArmStartSource)
            Case "IMMEDIATE"
                sCommand = sCommand & ":ARM:STAR:SOUR IMM;"
            Case "VXI"
                sCommand = sCommand & ":ARM:STAR:SOUR BUS;"
            Case "TTL0", "TTL1", "TTL2", "TTL3", "TTL4", "TTL5", "TTL6", "TTL7"
                sCommand = sCommand & ":ARM:STAR:SOUR TTLT" & Right(sArmStartSource, 1) & ";"
                If Not bArmStartSlopNegative Then
                    sCommand = sCommand & ":ARM:STAR:SLOP POS;"
                Else
                    sCommand = sCommand & ":ARM:STAR:SLOP NEG;"
                End If
            Case "EXTERNAL"
                sCommand = sCommand & ":ARM:STAR:SOUR EXT;"
                Select Case UCase(sArmStartLevel)
                    Case "TTL"
                        sCommand = sCommand & ":ARM:STAR:LEV MAX"
                    Case "ECL"
                        sCommand = sCommand & ":ARM:STAR:LEV MIN;"
                    Case "GND"
                        sCommand = sCommand & ":ARM:STAR:LEV DEF;"
                End Select
                If Not bArmStartSlopNegative Then
                    sCommand = sCommand & ":ARM:STAR:SLOP POS;"
                Else
                    sCommand = sCommand & ":ARM:STAR:SLOP NEG;"
                End If
        End Select

        Select Case UCase(SArmStopSource)
            Case "IMMEDIATE"
                sCommand = sCommand & ":ARM:STOP:SOUR IMM;"
            Case "VXI"
                sCommand = sCommand & ":ARM:STOP:SOUR BUS;"
            Case "TTL0", "TTL1", "TTL2", "TTL3", "TTL4", "TTL5", "TTL6", "TTL7"
                sCommand = sCommand & ":ARM:STOP:SOUR TTLT" & Right(SArmStopSource, 1) & ";"
                If Not bArmStopSlopNegative Then
                    sCommand = sCommand & ":ARM:STOP:SLOP POS;"
                Else
                    sCommand = sCommand & ":ARM:STOP:SLOP NEG;"
                End If
            Case "EXTERNAL"
                sCommand = sCommand & ":ARM:STOP:SOUR EXT;"
                Select Case UCase(sArmStopLevel)
                    Case "TTL"
                        sCommand = sCommand & ":ARM:STOP:LEV MAX"
                    Case "ECL"
                        sCommand = sCommand & ":ARM:STOP:LEV MIN;"
                    Case "GND"
                        sCommand = sCommand & ":ARM:STOP:LEV DEF;"
                End Select

                If Not bArmStartSlopNegative Then
                    sCommand = sCommand & ":ARM:STOP:SLOP POS;"
                Else
                    sCommand = sCommand & ":ARM:STOP:SLOP NEG;"
                End If
        End Select

        'Write ARM Subset to instrument
        If Not bSimulation Then WriteMsg(COUNTER, sCommand)
        Debug.Print(sCommand)

        'Configure OUTPut
        sCommand = ""
        If Not bTriggerOutputOn Then
            For i = 0 To 7
                sCommand = sCommand & ":OUTP:TTLT" & i & ":STAT OFF;"
            Next i
            sCommand = sCommand & ":OUTP:ROSC:STAT OFF;"
        Else
            For i = 0 To 7
                If i = nTTLTriggerOutput Then
                    sCommand = sCommand & ":OUTP:TTLT" & i & ":STAT ON;"
                Else
                    sCommand = sCommand & ":OUTP:TTLT" & i & ":STAT OFF;"
                End If
            Next i
            sCommand = sCommand & ":OUTP:ROSC:STAT ON;"
        End If

        'Write OUTPut Subset to instrument
        If Not bSimulation Then WriteMsg(COUNTER, sCommand)
        Debug.Print(sCommand)

        'Configure CONFigure Options
        sCommand = ""
        If dRange <> -1 Then
            sCommand = ":CONF" & nChannel & ":PER " & CStr(dRange) & ";"
            Debug.Print(sCommand)
        End If

        'Make Measurement
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function

        If Not bSimulation Then
            Select Case Left(UCase(APROBE), 1)
                Case "O" ' Off
                    WriteMsg(COUNTER, "INIT" & CStr(nChannel) & ";FETCH" & CStr(nChannel) & "?")
                    Debug.Print("INIT" & CStr(nChannel) & ";FETCH" & CStr(nChannel) & "?")
                    nErr = ReadMsg(COUNTER, sData)
                    If nErr <> 0 Then
                        dMeasPeriod = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37)
                        Err.Raise(nErr)
                        Exit Function
                    End If

                    dMeasPeriod = CDbl(sData)
                    gFrmMain.txtMeasured.Text = CStr(CSng(dMeasPeriod))
                    If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
                Case "S" ' Single
                    MisProbe = MsgBoxResult.Yes
                    Do While MisProbe = MsgBoxResult.Yes
                        gFrmMain.lblStatus.Text = "Waiting for Probe ..."
                        Failed = False
                        UserEvent = 0
                        gFrmMain.TimerProbe.Enabled = True : gFrmMain.fraInstructions.Visible = True
                        Do While (UserEvent = 0) And (Not bProbeClosed)
                            System.Windows.Forms.Application.DoEvents()
                        Loop

                        gFrmMain.TimerProbe.Enabled = False
                        bProbeClosed = False
                        gFrmMain.lblStatus.Text = "Making Measurement ..."
                        WriteMsg(COUNTER, "INIT" & CStr(nChannel) & ";FETCH" & CStr(nChannel) & "?")
                        nErr = ReadMsg(COUNTER, sData)
                        If nErr <> 0 Then
                            dMeasPeriod = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37)
                            Err.Raise(nErr)
                            Exit Function
                        End If

                        dMeasPeriod = CDbl(sData)
                        gFrmMain.txtMeasured.Text = CStr(CSng(dMeasPeriod))
                        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
                        gFrmMain.lblStatus.Text = "Measurement Complete ..."
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
                        Failed = False
                        UserEvent = 0
                        gFrmMain.TimerProbe.Enabled = True : gFrmMain.fraInstructions.Visible = True
                        Do While (UserEvent = 0) And (Not bProbeClosed)
                            gFrmMain.lblStatus.Text = "Making Measurement ..."
                            WriteMsg(COUNTER, "INIT" & CStr(nChannel) & ";FETCH" & CStr(nChannel) & "?")
                            nErr = ReadMsg(COUNTER, sData)
                            If nErr <> 0 Then
                                dMeasPeriod = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37)
                                Err.Raise(nErr)
                                Exit Function
                            End If

                            dMeasPeriod = CDbl(sData)
                            gFrmMain.txtMeasured.Text = CStr(CSng(dMeasPeriod))
                            If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
                        Loop
                        gFrmMain.TimerProbe.Enabled = False
                        bProbeClosed = False
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
            End Select
        Else
            dMeasPeriod = CDbl(InputBox("Command cmdCOUNTER.dMeasPeriod peformed." & vbCrLf & "Enter Value:", "SIMULATION MODE"))
            gFrmMain.txtMeasured.Text = CStr(CSng(dMeasPeriod))
            If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON)
        End If
    End Function

    Public Function dMeasFreqRatio(Optional ByRef nChannel As Short = 1, Optional ByRef dRange As Double = 1, Optional ByRef dAperature As Double = 0.1,
                                   Optional ByRef bChOne10XAttenuation As Boolean = False, Optional ByRef bChTwo10XAttenuation As Boolean = False,
                                   Optional ByRef bChOneACCoupling As Boolean = False, Optional ByRef bChTwoACCoupling As Boolean = False,
                                   Optional ByRef bChOne50ohms As Boolean = False, Optional ByRef bChTwo50ohms As Boolean = False,
                                   Optional ByRef sHysteresis As String = "DEF", Optional ByRef bAutoTrigger As Boolean = False,
                                   Optional ByRef nRelativeTriggerLevel As Integer = 50, Optional ByRef bTriggerSlopeNegative As Boolean = False,
                                   Optional ByRef dAbsoluteTrigger As Double = 0.0, Optional ByRef bTriggerOutputOn As Boolean = False,
                                   Optional ByRef nTTLTriggerOutput As Integer = 0, Optional ByRef sArmStartSource As String = "IMMEDIATE",
                                   Optional ByRef bArmStartSlopNegative As Boolean = False, Optional ByRef sArmStartLevel As String = "TTL",
                                   Optional ByRef SArmStopSource As String = "IMMEDIATE", Optional ByRef bArmStopSlopNegative As Boolean = False,
                                   Optional ByRef sArmStopLevel As String = "TTL", Optional ByRef sTimeBaseSource As String = "INTERNAL",
                                   Optional ByRef bOutputTimeBase As Boolean = False, Optional ByRef bTotalizeGateMeasure As Boolean = False,
                                   Optional ByRef bTotalizeGatePolarityInverted As Boolean = False, Optional ByRef bTimeIntervalDelay As Boolean = False,
                                   Optional ByRef dTimeIntervalDelay As Double = 0.1, Optional ByRef sAcquisitionTimeOutSetting As String = "ON",
                                   Optional ByRef dAcquisitionTimeOut As Double = 5.0, Optional ByRef APROBE As String = "OFF") As Double
        Dim i As Short
        Dim sData As String = ""
        Dim nErr As Integer
        Dim sCommand As String
        Dim Q As String
        Dim sCurrentMsg As String = ""
        Q = Chr(34)

        Dim iHandle As Integer
        iHandle = nInstrumentHandle(COUNTER)

        dMeasFreqRatio = 0.0
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        gFrmMain.txtInstrument.Text = "Counter"
        gFrmMain.txtCommand.Text = "dMeasFreqRatio"
        If APROBE <> "OFF" Then
            sCurrentMsg = gFrmMain.lblStatus.Text
            gFrmMain.lblStatus.Text = "APPLY PROBE"
        End If

        'Clear Counter
        If Not bSimulation Then
            '  viClear (nInstrumentHandle(COUNTER))
            nSystErr = atxmlDF_viClear(ResourceName(COUNTER), 0)
            WriteMsg(COUNTER, "*RST ; *CLS")
        End If

        'Error Check Arguments
        If nChannel < 1 Or nChannel > 2 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasFreqRatio nChannel argument out of range.")
            Echo("RANGE: 1 TO 2")
            dMeasFreqRatio = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
            dMeasFreqRatio = 0
            Exit Function
        End If

        If dRange < 0.0000000001 Or dRange > 1000000000000.0 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasFreqRatio dRange argument out of range.")
            Echo("RANGE: 1E-10 TO 1E-12")
            dMeasFreqRatio = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
            dMeasFreqRatio = 0
            Exit Function
        End If

        If dAperature < 0.1 Or dAperature > 99.999 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasFreqRatio dAperature argument out of range.")
            Echo("RANGE: 0.1 TO 99.999")
            dMeasFreqRatio = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
            dMeasFreqRatio = 0
            Exit Function
        End If

        Select Case UCase(sHysteresis)
            Case "DEF", "MAX", "MIN"
            Case Else
                Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasFreqRatio sHysteresis argument out of range.")
                Echo("RANGE: DEF, MAX or MIN")
                dMeasFreqRatio = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                dMeasFreqRatio = 0
                Exit Function
        End Select

        If nRelativeTriggerLevel < 10 Or nRelativeTriggerLevel > 90 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasFreqRatio nRelativeTriggerLevel argument out of range.")
            Echo("RANGE: 10 to 90")
            dMeasFreqRatio = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
            dMeasFreqRatio = 0
            Exit Function
        End If

        If dAbsoluteTrigger < -10.238 Or dAbsoluteTrigger > 10.238 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasFreqRatio dAbsoluteTrigger argument out of range.")
            Echo("RANGE: -10.238 to 10.238")
            dMeasFreqRatio = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
            dMeasFreqRatio = 0
            Exit Function
        End If

        If nTTLTriggerOutput < 0 Or nTTLTriggerOutput > 7 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasFreqRatio nTTLTriggerOutput argument out of range.")
            Echo("RANGE: 0 to 7")
            dMeasFreqRatio = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
            dMeasFreqRatio = 0
            Exit Function
        End If

        Select Case UCase(sArmStartSource)
            Case "IMMEDIATE", "VXI"
            Case "TTL0", "TTL1", "TTL2", "TTL3", "TTL4", "TTL5", "TTL6", "TTL7"
            Case "EXTERNAL"
                Select Case UCase(sArmStartLevel)
                    Case "TTL", "ECL", "GND"
                    Case Else
                        Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasFreqRatio sArmStartLevel argument out of range.")
                        Echo("RANGE: TTL, ECL or GND")
                        dMeasFreqRatio = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                        dMeasFreqRatio = 0
                End Select
            Case Else
                Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasFreqRatio sArmStartSource argument out of range.")
                Echo("RANGE: IMMEDIATE, VXI, TTL0, TTL1, TTL2, TTL3, TTL4, TTL5, TTL6, TTL7 or EXTERNAL")
                dMeasFreqRatio = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                dMeasFreqRatio = 0
                Exit Function
        End Select

        Select Case UCase(SArmStopSource)
            Case "IMMEDIATE", "VXI"
            Case "TTL0", "TTL1", "TTL2", "TTL3", "TTL4", "TTL5", "TTL6", "TTL7"
            Case "EXTERNAL"
                Select Case UCase(sArmStopLevel)
                    Case "TTL", "ECL", "GND"
                    Case Else
                        Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasFreqRatio sArmStopLevel argument out of range.")
                        Echo("RANGE: TTL, ECL or GND")
                        dMeasFreqRatio = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                        dMeasFreqRatio = 0
                End Select
            Case Else
                Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasFreqRatio sArmStopSource argument out of range.")
                Echo("RANGE: IMMEDIATE, VXI, TTL0, TTL1, TTL2, TTL3, TTL4, TTL5, TTL6, TTL7 or EXTERNAL")
                dMeasFreqRatio = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                dMeasFreqRatio = 0
                Exit Function
        End Select

        Select Case UCase(sTimeBaseSource)
            Case "VXI", "INTERNAL", "EXTERNAL"
            Case Else
                Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasFreqRatio sTimeBaseSource argument out of range.")
                Echo("RANGE: VXI, INTERNAL or EXTERNAL")
                dMeasFreqRatio = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                dMeasFreqRatio = 0
                Exit Function
        End Select

        If dTimeIntervalDelay < 0.001 Or dTimeIntervalDelay > 99.999 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasFreqRatio dTimeIntervalDelay argument out of range.")
            Echo("RANGE: 0.001 to 99.999")
            dMeasFreqRatio = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
            dMeasFreqRatio = 0
            Exit Function
        End If

        Select Case UCase(sAcquisitionTimeOutSetting)
            Case "ON", "OFF", "START"
            Case Else
                Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasFreqRatio sAcquisitionTimeOutSetting argument out of range.")
                Echo("RANGE: ON, OFF, or START")
                dMeasFreqRatio = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                dMeasFreqRatio = 0
                Exit Function
        End Select

        If dAcquisitionTimeOut < 0.1 Or dAcquisitionTimeOut > 1500 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasFreqRatio dAcquisitionTimeOut argument out of range.")
            Echo("RANGE: 0.1 to 1500")
            dMeasFreqRatio = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
            dMeasFreqRatio = 0
            Exit Function
        End If

        Select Case Left(UCase(APROBE), 1)
            Case "S", "O", "C" ' Single, Off, Continuous
            Case Else
                Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasFreqRatio APROBE argument out of range.")
                Echo("RANGE: SINGLE or OFF")
                dMeasFreqRatio = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                dMeasFreqRatio = 0
                Exit Function
        End Select

        'Configure Counter for Measurement
        'Set Input Options
        'Clear Counter
        If Not bSimulation Then
            '  viClear (nInstrumentHandle(COUNTER))
            nSystErr = atxmlDF_viClear(ResourceName(COUNTER), 0)
            WriteMsg(COUNTER, "*RST ; *CLS")
        End If

        sCommand = ""
        'Configure INPut Subsystem
        If nChannel = 1 Then
            'Set Attenuation
            If Not bChOne10XAttenuation Then
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:ATT 1;")
                Debug.Print(":INP1:ATT 1;")
            Else
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:ATT 10;")
                Debug.Print(":INP1:ATT 10;")
            End If

            'Set Coupling
            If Not bChOneACCoupling Then 'bbbb was simulation
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:COUP DC;")
                Debug.Print(":INP1:COUP DC;")
            Else
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:COUP AC;")
                Debug.Print(":INP1:COUP AC;")
            End If

            'Set Impedance
            If Not bChOne50ohms Then 'bbbb was simulation
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:IMP 1E6;")
                Debug.Print(":INP1:IMP 1E6;")
            Else
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:IMP 50;")
                Debug.Print(":INP1:IMP 50;")
            End If

            'Set Routing (Always separate for Period) 'bbbb was simulation
            If Not bSimulation Then WriteMsg(COUNTER, ":INP1:ROUT SEP;")
            Debug.Print(":INP1:ROUT SEP;")
        Else ' Channel 2
            'Set Attenuation
            If Not bChTwo10XAttenuation Then
                If Not bSimulation Then WriteMsg(COUNTER, ":INP2:ATT 1;")
                Debug.Print(":INP2:ATT 1;")
            Else
                If Not bSimulation Then WriteMsg(COUNTER, ":INP2:ATT 10;")
                Debug.Print(":INP2:ATT 10;")
            End If

            'Set Coupling
            If Not bChTwoACCoupling Then 'bbbb was simulation
                If Not bSimulation Then WriteMsg(COUNTER, ":INP2:COUP DC;")
                Debug.Print(":INP2:COUP DC;")
            Else
                If Not bSimulation Then WriteMsg(COUNTER, ":INP2:COUP AC;")
                Debug.Print(":INP2:COUP AC;")
            End If

            'Set Impedance
            If Not bChTwo50ohms Then 'bbbb was simulation
                If Not bSimulation Then WriteMsg(COUNTER, ":INP2:IMP 1E6;")
                Debug.Print(":INP2:IMP 1E6;")
            Else
                If Not bSimulation Then WriteMsg(COUNTER, ":INP2:IMP 50;")
                Debug.Print(":INP2:IMP 50;")
            End If
            'Set Routing (Always separate for Period)
            'If Not simulation Then WriteMsg COUNTER, ":INP2:ROUT SEP;"
            'Debug.Print ":INP2:ROUT SEP;"
        End If

        'Configure SENSe Options
        'Function PERIOD
        sCommand = ":SENS" & nChannel & ":FUNC " & Q & "FREQ:RAT" & Q & ";"
        'Gate Average always off for Frequency
        sCommand = sCommand & ":SENS" & nChannel & ":AVER:STAT OFF;"
        'Set Aquisition Time Out
        sCommand = sCommand & ":SENS:ATIM:TIME " & CStr(dAcquisitionTimeOut) & ";"
        'Set Visa TMO value based on dAcqusitionTimeOut or dAperature setting + 1 Second
        If dAcquisitionTimeOut > dAperature Then
            If Not bSimulation Then lSystErr = viSetAttribute(nInstrumentHandle(COUNTER), VI_ATTR_TMO_VALUE, (dAcquisitionTimeOut * 1000) + 1000)
        Else
            If Not bSimulation Then lSystErr = viSetAttribute(nInstrumentHandle(COUNTER), VI_ATTR_TMO_VALUE, (dAperature * 1000) + 1000)
        End If

        If Not bSimulation And lSystErr Then 'bbbb was bsimulaton
            lSystErr = viStatusDesc(iHandle, lSystErr, lpBuffer)
            Echo("WRITEMSG ERROR: Error sending command to " & sInstrumentDescription(COUNTER) & ".")
            Echo("COMMAND: viSetAttribute")
            MsgBox("Error sending: viSetAttribute VI_ATTR_TMO_VALUE" & vbCrLf & "ERROR: " & lpBuffer, MsgBoxStyle.Exclamation, "VISA Error")
            dMeasFreqRatio = 0
            Exit Function
        End If

        'Set Aquisition Time Out Mode
        Select Case UCase(sAcquisitionTimeOutSetting)
            Case "ON", "OFF"
                sCommand = sCommand & ":SENS:ATIM:CHEC " & UCase(sAcquisitionTimeOutSetting) & ";"
            Case "START"
                sCommand = sCommand & ":SENS:ATIM:CHEC STAR;"
        End Select

        'Set Auto Trigger or absolute
        If bAutoTrigger Then
            sCommand = sCommand & ":SENS" & nChannel & ":EVEN:LEV:ABS:AUTO ON;"
            sCommand = sCommand & ":SENS" & nChannel & ":EVEN:LEV:REL " & CStr(nRelativeTriggerLevel) & ";"
        Else
            sCommand = sCommand & ":SENS" & nChannel & ":EVEN:LEV:ABS " & CStr(dAbsoluteTrigger) & ";"
            sCommand = sCommand & ":SENS" & nChannel & ":EVEN:LEV:ABS:AUTO OFF;"
        End If

        'Set Trigger Slop
        If Not bTriggerSlopeNegative Then
            sCommand = sCommand & ":SENS" & nChannel & ":EVEN:SLOP POS;"
        Else
            sCommand = sCommand & ":SENS" & nChannel & ":EVEN:SLOP NEG;"
        End If

        'Set Hysteresis
        sCommand = sCommand & ":SENS" & nChannel & ":EVEN:HYST " & UCase(sHysteresis) & ";"
        'Set Aperature
        sCommand = sCommand & ":SENS" & nChannel & ":FREQ:APER " & CStr(dAperature) & ";"
        sCommand = sCommand & ":SENS" & nChannel & ":PER:APER " & CStr(dAperature) & ";"
        sCommand = sCommand & ":SENS" & nChannel & ":RAT:APER " & CStr(dAperature) & ";"
        'Set Range
        sCommand = sCommand & ":SENS" & nChannel & ":FREQ:RANG:AUTO OFF;"
        'Set Timebase source
        Select Case UCase(sTimeBaseSource)
            Case "VXI"
                sCommand = sCommand & ":SENS" & nChannel & ":ROSC:SOUR CLK10;"
            Case "INTERNAL"
                sCommand = sCommand & ":SENS" & nChannel & ":ROSC:SOUR INT;"
            Case "EXTERNAL"
                sCommand = sCommand & ":SENS" & nChannel & ":ROSC:SOUR EXT;"
        End Select

        If bTimeIntervalDelay Then
            sCommand = sCommand & ":SENS" & nChannel & ":TINT:DEL:STAT ON;"
            sCommand = sCommand & ":SENS" & nChannel & ":TINT:DEL:TIME " & CStr(dTimeIntervalDelay) & ";"
        Else
            sCommand = sCommand & ":SENS" & nChannel & ":TINT:DEL:STAT OFF;"
        End If

        If bTotalizeGateMeasure Then
            sCommand = sCommand & ":SENS" & nChannel & ":TOT:GATE:STAT ON;"
            If bTotalizeGatePolarityInverted Then
                sCommand = sCommand & ":SENS" & nChannel & ":TOT:GATE:POL INV;"
            Else
                sCommand = sCommand & ":SENS" & nChannel & ":TOT:GATE:POL NORM;"
            End If
        Else
            sCommand = sCommand & ":SENS" & nChannel & ":TOT:GATE:STAT OFF;"
            sCommand = sCommand & ":SENS" & nChannel & ":TOT:GATE:POL NORM;"
        End If

        'Write SENSe Subset to instrument
        If Not bSimulation Then WriteMsg(COUNTER, sCommand)
        Debug.Print(sCommand)

        'Configure ARM Options
        sCommand = ""
        Select Case UCase(sArmStartSource)
            Case "IMMEDIATE"
                sCommand = sCommand & ":ARM:STAR:SOUR IMM;"
            Case "VXI"
                sCommand = sCommand & ":ARM:STAR:SOUR BUS;"
            Case "TTL0", "TTL1", "TTL2", "TTL3", "TTL4", "TTL5", "TTL6", "TTL7"
                sCommand = sCommand & ":ARM:STAR:SOUR TTLT" & Right(sArmStartSource, 1) & ";"
                If Not bArmStartSlopNegative Then
                    sCommand = sCommand & ":ARM:STAR:SLOP POS;"
                Else
                    sCommand = sCommand & ":ARM:STAR:SLOP NEG;"
                End If
            Case "EXTERNAL"
                sCommand = sCommand & ":ARM:STAR:SOUR EXT;"
                Select Case UCase(sArmStartLevel)
                    Case "TTL"
                        sCommand = sCommand & ":ARM:STAR:LEV MAX"
                    Case "ECL"
                        sCommand = sCommand & ":ARM:STAR:LEV MIN;"
                    Case "GND"
                        sCommand = sCommand & ":ARM:STAR:LEV DEF;"
                End Select
                If Not bArmStartSlopNegative Then
                    sCommand = sCommand & ":ARM:STAR:SLOP POS;"
                Else
                    sCommand = sCommand & ":ARM:STAR:SLOP NEG;"
                End If
        End Select

        Select Case UCase(SArmStopSource)
            Case "IMMEDIATE"
                sCommand = sCommand & ":ARM:STOP:SOUR IMM;"
            Case "VXI"
                sCommand = sCommand & ":ARM:STOP:SOUR BUS;"
            Case "TTL0", "TTL1", "TTL2", "TTL3", "TTL4", "TTL5", "TTL6", "TTL7"
                sCommand = sCommand & ":ARM:STOP:SOUR TTLT" & Right(SArmStopSource, 1) & ";"
                If Not bArmStopSlopNegative Then
                    sCommand = sCommand & ":ARM:STOP:SLOP POS;"
                Else
                    sCommand = sCommand & ":ARM:STOP:SLOP NEG;"
                End If
            Case "EXTERNAL"
                sCommand = sCommand & ":ARM:STOP:SOUR EXT;"
                Select Case UCase(sArmStopLevel)
                    Case "TTL"
                        sCommand = sCommand & ":ARM:STOP:LEV MAX"
                    Case "ECL"
                        sCommand = sCommand & ":ARM:STOP:LEV MIN;"
                    Case "GND"
                        sCommand = sCommand & ":ARM:STOP:LEV DEF;"
                End Select
                If Not bArmStartSlopNegative Then
                    sCommand = sCommand & ":ARM:STOP:SLOP POS;"
                Else
                    sCommand = sCommand & ":ARM:STOP:SLOP NEG;"
                End If
        End Select

        'Write ARM Subset to instrument
        If Not bSimulation Then WriteMsg(COUNTER, sCommand)
        Debug.Print(sCommand)

        'Configure OUTPut
        sCommand = ""
        If Not bTriggerOutputOn Then
            For i = 0 To 7
                sCommand = sCommand & ":OUTP:TTLT" & i & ":STAT OFF;"
            Next i

            sCommand = sCommand & ":OUTP:ROSC:STAT OFF;"
        Else
            For i = 0 To 7
                If i = nTTLTriggerOutput Then
                    sCommand = sCommand & ":OUTP:TTLT" & i & ":STAT ON;"
                Else
                    sCommand = sCommand & ":OUTP:TTLT" & i & ":STAT OFF;"
                End If
            Next i
            sCommand = sCommand & ":OUTP:ROSC:STAT ON;"
        End If

        'Write OUTPut Subset to instrument
        If Not bSimulation Then WriteMsg(COUNTER, sCommand)
        Debug.Print(sCommand)

        'Configure CONFigure Options
        sCommand = ""
        If dRange <> -1 Then
            sCommand = ":CONF" & nChannel & ":FREQ:RAT " & CStr(dRange) & ";"
            Debug.Print(sCommand)
        End If

        'Make Measurement
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function

        If Not bSimulation Then
            Select Case Left(UCase(APROBE), 1)
                Case "O" ' Off
                    WriteMsg(COUNTER, "INIT" & CStr(nChannel) & ";FETCH" & CStr(nChannel) & "?")
                    Debug.Print("INIT" & CStr(nChannel) & ";FETCH" & CStr(nChannel) & "?")
                    nErr = ReadMsg(COUNTER, sData)
                    If nErr <> 0 Then
                        dMeasFreqRatio = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37)
                        Err.Raise(nErr)
                        Exit Function
                    End If

                    dMeasFreqRatio = CDbl(sData)
                    gFrmMain.txtMeasured.Text = CStr(CSng(dMeasFreqRatio))
                    If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
                Case "S" ' Single
                    MisProbe = MsgBoxResult.Yes
                    Do While MisProbe = MsgBoxResult.Yes
                        gFrmMain.lblStatus.Text = "Waiting for Probe ..."
                        Failed = False
                        UserEvent = 0
                        gFrmMain.TimerProbe.Enabled = True : gFrmMain.fraInstructions.Visible = True
                        Do While (UserEvent = 0) And (Not bProbeClosed)
                            System.Windows.Forms.Application.DoEvents()
                        Loop

                        gFrmMain.TimerProbe.Enabled = False
                        bProbeClosed = False
                        gFrmMain.lblStatus.Text = "Making Measurement ..."
                        WriteMsg(COUNTER, "INIT" & CStr(nChannel) & ";FETCH" & CStr(nChannel) & "?")
                        nErr = ReadMsg(COUNTER, sData)
                        If nErr <> 0 Then
                            dMeasFreqRatio = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37)
                            Err.Raise(nErr)
                            Exit Function
                        End If

                        dMeasFreqRatio = CDbl(sData)
                        gFrmMain.txtMeasured.Text = CStr(CSng(dMeasFreqRatio))
                        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
                        gFrmMain.lblStatus.Text = "Measurement Complete ..."
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
                        Failed = False
                        UserEvent = 0
                        gFrmMain.TimerProbe.Enabled = True : gFrmMain.fraInstructions.Visible = True
                        Do While (UserEvent = 0) And (Not bProbeClosed)
                            gFrmMain.lblStatus.Text = "Making Measurement ..."
                            WriteMsg(COUNTER, "INIT" & CStr(nChannel) & ";FETCH" & CStr(nChannel) & "?")
                            nErr = ReadMsg(COUNTER, sData)
                            If nErr <> 0 Then
                                dMeasFreqRatio = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37)
                                Err.Raise(nErr)
                                Exit Function
                            End If

                            dMeasFreqRatio = CDbl(sData)
                            gFrmMain.txtMeasured.Text = CStr(CSng(dMeasFreqRatio))
                            If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
                        Loop
                        gFrmMain.TimerProbe.Enabled = False
                        bProbeClosed = False
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
            End Select
        Else
            dMeasFreqRatio = CDbl(InputBox("Command cmdCOUNTER.dMeasFreqRatio peformed." & vbCrLf & "Enter Value:", "SIMULATION MODE"))
            gFrmMain.txtMeasured.Text = CStr(CSng(dMeasFreqRatio))
            If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON)
        End If
    End Function

    Public Sub ArmTimeInterval(Optional ByRef nChannel As Short = 1, Optional ByRef dRange As Double = 0.0000001,
                               Optional ByRef bRouteSeparate As Boolean = True, Optional ByRef bChOne10XAttenuation As Boolean = False,
                               Optional ByRef bChTwo10XAttenuation As Boolean = False, Optional ByRef bChOneACCoupling As Boolean = False,
                               Optional ByRef bChTwoACCoupling As Boolean = False, Optional ByRef bChOne50ohms As Boolean = False,
                               Optional ByRef bChTwo50ohms As Boolean = False, Optional ByRef sHysteresis As String = "DEF",
                               Optional ByRef bAutoTrigger As Boolean = False, Optional ByRef nRelativeTriggerLevel As Integer = 50,
                               Optional ByRef bTriggerSlopeNegative As Boolean = False, Optional ByRef dAbsoluteTrigger As Double = 0.0,
                               Optional ByRef bTriggerOutputOn As Boolean = False, Optional ByRef nTTLTriggerOutput As Integer = 0,
                               Optional ByRef sArmStartSource As String = "IMMEDIATE", Optional ByRef bArmStartSlopNegative As Boolean = False,
                               Optional ByRef sArmStartLevel As String = "TTL", Optional ByRef SArmStopSource As String = "IMMEDIATE",
                               Optional ByRef bArmStopSlopNegative As Boolean = False, Optional ByRef sArmStopLevel As String = "TTL",
                               Optional ByRef sTimeBaseSource As String = "INTERNAL", Optional ByRef bOutputTimeBase As Boolean = False,
                               Optional ByRef bTotalizeGateMeasure As Boolean = False, Optional ByRef bTotalizeGatePolarityInverted As Boolean = False,
                               Optional ByRef bTimeIntervalDelay As Boolean = False, Optional ByRef dTimeIntervalDelay As Double = 0.1,
                               Optional ByRef sAcquisitionTimeOutSetting As String = "ON", Optional ByRef dAcquisitionTimeOut As Double = 5.0,
                               Optional ByRef bGateAverage As Boolean = True)
        Dim i As Short
        Dim sCommand As String
        Dim Q As String
        Q = Chr(34)

        Dim iHandle As Integer
        iHandle = nInstrumentHandle(COUNTER)

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub

        gFrmMain.txtInstrument.Text = "Counter"
        gFrmMain.txtCommand.Text = "dMeasTimeInterval"
        Dim sCurrentMsg As String
        If APROBE <> CDbl("OFF") Then
            sCurrentMsg = gFrmMain.lblStatus.Text
            gFrmMain.lblStatus.Text = "APPLY PROBE"
        End If

        'Clear Counter
        If Not bSimulation Then
            '  viClear (nInstrumentHandle(COUNTER))
            nSystErr = atxmlDF_viClear(ResourceName(COUNTER), 0)
            WriteMsg(COUNTER, "*RST ; *CLS")
        End If

        'Error Check Arguments
        If nChannel < 1 Or nChannel > 2 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasTimeInterval nChannel argument out of range.")
            Echo("RANGE: 1 TO 2")
            Err.Raise(-1001)
            Exit Sub
        End If

        If dRange < 0.000000005 Or dRange > 1000 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasTimeInterval dRange argument out of range.")
            Echo("RANGE: 5E-9 TO 1000")
            Err.Raise(-1001)

            Exit Sub
        End If

        Select Case UCase(sHysteresis)
            Case "DEF", "MAX", "MIN"
            Case Else
                Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasTimeInterval sHysteresis argument out of range.")
                Echo("RANGE: DEF, MAX or MIN")
                Err.Raise(-1001)

                Exit Sub
        End Select

        If nRelativeTriggerLevel < 10 Or nRelativeTriggerLevel > 90 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasTimeInterval nRelativeTriggerLevel argument out of range.")
            Echo("RANGE: 10 to 90")
            Err.Raise(-1001)

            Exit Sub
        End If

        If dAbsoluteTrigger < -10.238 Or dAbsoluteTrigger > 10.238 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasTimeInterval dAbsoluteTrigger argument out of range.")
            Echo("RANGE: -10.238 to 10.238")
            Err.Raise(-1001)

            Exit Sub
        End If

        If nTTLTriggerOutput < 0 Or nTTLTriggerOutput > 7 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasTimeInterval nTTLTriggerOutput argument out of range.")
            Echo("RANGE: 0 to 7")
            Err.Raise(-1001)

            Exit Sub
        End If

        Select Case UCase(sArmStartSource)
            Case "IMMEDIATE", "VXI"
            Case "TTL0", "TTL1", "TTL2", "TTL3", "TTL4", "TTL5", "TTL6", "TTL7"
            Case "EXTERNAL"
                Select Case UCase(sArmStartLevel)
                    Case "TTL", "ECL", "GND"
                    Case Else
                        Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasTimeInterval sArmStartLevel argument out of range.")
                        Echo("RANGE: TTL, ECL or GND")
                        Err.Raise(-1001)

                End Select
            Case Else
                Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasTimeInterval sArmStartSource argument out of range.")
                Echo("RANGE: IMMEDIATE, VXI, TTL0, TTL1, TTL2, TTL3, TTL4, TTL5, TTL6, TTL7 or EXTERNAL")
                Err.Raise(-1001)

                Exit Sub
        End Select

        Select Case UCase(SArmStopSource)
            Case "IMMEDIATE", "VXI"
            Case "TTL0", "TTL1", "TTL2", "TTL3", "TTL4", "TTL5", "TTL6", "TTL7"
            Case "EXTERNAL"
                Select Case UCase(sArmStopLevel)
                    Case "TTL", "ECL", "GND"
                    Case Else
                        Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasTimeInterval sArmStopLevel argument out of range.")
                        Echo("RANGE: TTL, ECL or GND")
                        Err.Raise(-1001)

                End Select
            Case Else
                Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasTimeInterval sArmStopSource argument out of range.")
                Echo("RANGE: IMMEDIATE, VXI, TTL0, TTL1, TTL2, TTL3, TTL4, TTL5, TTL6, TTL7 or EXTERNAL")
                Err.Raise(-1001)

                Exit Sub
        End Select

        Select Case UCase(sTimeBaseSource)
            Case "VXI", "INTERNAL", "EXTERNAL"
            Case Else
                Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasTimeInterval sTimeBaseSource argument out of range.")
                Echo("RANGE: VXI, INTERNAL or EXTERNAL")
                Err.Raise(-1001)

                Exit Sub
        End Select

        If dTimeIntervalDelay < 0.001 Or dTimeIntervalDelay > 99.999 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasTimeInterval dTimeIntervalDelay argument out of range.")
            Echo("RANGE: 0.001 to 99.999")
            Err.Raise(-1001)

            Exit Sub
        End If

        Select Case UCase(sAcquisitionTimeOutSetting)
            Case "ON", "OFF", "START"
            Case Else
                Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasTimeInterval sAcquisitionTimeOutSetting argument out of range.")
                Echo("RANGE: ON, OFF, or START")
                Err.Raise(-1001)

                Exit Sub
        End Select

        If dAcquisitionTimeOut < 0.1 Or dAcquisitionTimeOut > 1500 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasTimeInterval dAcquisitionTimeOut argument out of range.")
            Echo("RANGE: 0.1 to 1500")
            Err.Raise(-1001)

            Exit Sub
        End If

        Select Case Left(UCase(CStr(APROBE)), 1)
            Case "S", "O", "C" ' Single, Off, Continuous
            Case Else
                Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasTimeInterval APROBE argument out of range.")
                Echo("RANGE: SINGLE, CONTINUOUS or OFF")
                Err.Raise(-1001)
                Exit Sub
        End Select

        'Configure Counter for Measurement
        'Set Input Options
        'Clear Counter
        If Not bSimulation Then
            '  viClear (nInstrumentHandle(COUNTER))
            nSystErr = atxmlDF_viClear(ResourceName(COUNTER), 0)
            WriteMsg(COUNTER, "*RST ; *CLS")
        End If

        'Configure CONFigure Options
        sCommand = ""
        If dRange <> -1 Then
            sCommand = ":CONF" & nChannel & ":TINT " & CStr(dRange) & ";"
            If Not bSimulation Then WriteMsg(COUNTER, sCommand)
            Debug.Print(sCommand)
        Else
            sCommand = ":CONF" & nChannel & ":TINT"
            If Not bSimulation Then WriteMsg(COUNTER, sCommand)
            Debug.Print(sCommand)
        End If

        sCommand = ""
        'Configure INPut Subsystem
        If nChannel = 1 Then
            'Set Routing (Always separate for Period)
            If bRouteSeparate Then 'bbbb was simulation
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:ROUT SEP;")
                Debug.Print(":INP1:ROUT SEP;")
            Else
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:ROUT COMM;")
                Debug.Print(":INP1:ROUT COMM;")
            End If

            'Set Impedance
            If Not bChOne50ohms Then 'bbbb was simulation
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:IMP 1000000;")
                Debug.Print(":INP1:IMP 1E6;")
            Else
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:IMP 50;")
                Debug.Print(":INP1:IMP 50;")
            End If

            'Set Attenuation
            If Not bChOne10XAttenuation Then
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:ATT 1;")
                Debug.Print(":INP1:ATT 1;")
            Else
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:ATT 10;")
                Debug.Print(":INP1:ATT 10;")
            End If

            'Set Coupling
            If Not bChOneACCoupling Then 'bbbb was simulation
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:COUP DC;")
                Debug.Print(":INP1:COUP DC;")
            Else
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:COUP AC;")
                Debug.Print(":INP1:COUP AC;")
            End If
        Else ' Channel 2
            'Set Attenuation
            If Not bChTwo10XAttenuation Then
                If Not bSimulation Then WriteMsg(COUNTER, ":INP2:ATT 1;")
                Debug.Print(":INP2:ATT 1;")
            Else
                If Not bSimulation Then WriteMsg(COUNTER, ":INP2:ATT 10;")
                Debug.Print(":INP2:ATT 10;")
            End If

            'Set Coupling
            If Not bChTwoACCoupling Then 'bbbb was simulation
                If Not bSimulation Then WriteMsg(COUNTER, ":INP2:COUP DC;")
                Debug.Print(":INP2:COUP DC;")
            Else
                If Not bSimulation Then WriteMsg(COUNTER, ":INP2:COUP AC;")
                Debug.Print(":INP2:COUP AC;")
            End If

            'Set Impedance
            If Not bChTwo50ohms Then 'bbbb was simulation
                If Not bSimulation Then WriteMsg(COUNTER, ":INP2:IMP 1E6;")
                Debug.Print(":INP2:IMP 1E6;")
            Else
                If Not bSimulation Then WriteMsg(COUNTER, ":INP2:IMP 50;")
                Debug.Print(":INP2:IMP 50;")
            End If
            'Set Routing (Always separate for Period)
            'If Not simulation Then WriteMsg COUNTER, ":INP2:ROUT SEP;"
            'Debug.Print ":INP2:ROUT SEP;"
        End If
        'Configure SENSe Options
        'Function PERIOD
        sCommand = ":SENS" & nChannel & ":FUNC " & Q & "TINT" & Q & ";"
        'Gate Average always off for Frequency
        If bGateAverage Then
            sCommand = sCommand & ":SENS" & nChannel & ":AVER:STAT ON;"
        Else
            sCommand = sCommand & ":SENS" & nChannel & ":AVER:STAT OFF;"
        End If

        'Set Aquisition Time Out
        sCommand = sCommand & ":SENS:ATIM:TIME " & CStr(dAcquisitionTimeOut) & ";"
        'Set Visa TMO value based on dAcqusitionTimeOut or dAperature setting + 1 Second
        If dAcquisitionTimeOut > dAperature Then
            If Not bSimulation Then lSystErr = viSetAttribute(nInstrumentHandle(COUNTER), VI_ATTR_TMO_VALUE, (dAcquisitionTimeOut * 1000) + 1000)
        Else
            If Not bSimulation Then lSystErr = viSetAttribute(nInstrumentHandle(COUNTER), VI_ATTR_TMO_VALUE, (dAperature * 1000) + 1000)
        End If

        If Not bSimulation And lSystErr Then 'bbbb was bsimulaton
            lSystErr = viStatusDesc(iHandle, lSystErr, lpBuffer)
            Echo("WRITEMSG ERROR: Error sending command to " & sInstrumentDescription(COUNTER) & ".")
            Echo("COMMAND: viSetAttribute")
            MsgBox("Error sending: viSetAttribute VI_ATTR_TMO_VALUE" & vbCrLf & "ERROR: " & lpBuffer, MsgBoxStyle.Exclamation, "VISA Error")

            Exit Sub
        End If

        'Set Aquisition Time Out Mode
        Select Case UCase(sAcquisitionTimeOutSetting)
            Case "ON", "OFF"
                sCommand = sCommand & ":SENS:ATIM:CHEC " & UCase(sAcquisitionTimeOutSetting) & ";"
            Case "START"
                sCommand = sCommand & ":SENS:ATIM:CHEC STAR;"
        End Select

        'Set Auto Trigger or absolute
        If bAutoTrigger Then
            sCommand = sCommand & ":SENS" & nChannel & ":EVEN:LEV:ABS:AUTO ON;"
            sCommand = sCommand & ":SENS" & nChannel & ":EVEN:LEV:REL " & CStr(nRelativeTriggerLevel) & ";"
        Else
            sCommand = sCommand & ":SENS" & nChannel & ":EVEN:LEV " & CStr(dAbsoluteTrigger) & ";"
            sCommand = sCommand & ":SENS" & nChannel & ":EVEN:LEV:ABS:AUTO OFF;"
        End If

        'Set Trigger Slop
        If Not bTriggerSlopeNegative Then
            sCommand = sCommand & ":SENS" & nChannel & ":EVEN:SLOP POS;"
        Else
            sCommand = sCommand & ":SENS" & nChannel & ":EVEN:SLOP NEG;"
        End If

        'Set Hysteresis
        sCommand = sCommand & ":SENS" & nChannel & ":EVEN:HYST " & UCase(sHysteresis) & ";"
        'Set Timebase source
        Select Case UCase(sTimeBaseSource)
            Case "VXI"
                sCommand = sCommand & ":SENS" & nChannel & ":ROSC:SOUR CLK10;"
            Case "INTERNAL"
                sCommand = sCommand & ":SENS" & nChannel & ":ROSC:SOUR INT;"
            Case "EXTERNAL"
                sCommand = sCommand & ":SENS" & nChannel & ":ROSC:SOUR EXT;"
        End Select

        If bTimeIntervalDelay Then
            sCommand = sCommand & ":SENS" & nChannel & ":TINT:DEL:STAT ON;"
            sCommand = sCommand & ":SENS" & nChannel & ":TINT:DEL:TIME " & CStr(dTimeIntervalDelay) & ";"
        Else
            sCommand = sCommand & ":SENS" & nChannel & ":TINT:DEL:STAT OFF;"
        End If

        If bTotalizeGateMeasure Then
            sCommand = sCommand & ":SENS" & nChannel & ":TOT:GATE:STAT ON;"
            If bTotalizeGatePolarityInverted Then
                sCommand = sCommand & ":SENS" & nChannel & ":TOT:GATE:POL INV;"
            Else
                sCommand = sCommand & ":SENS" & nChannel & ":TOT:GATE:POL NORM;"
            End If
        Else
            sCommand = sCommand & ":SENS" & nChannel & ":TOT:GATE:STAT OFF;"
            sCommand = sCommand & ":SENS" & nChannel & ":TOT:GATE:POL NORM;"
        End If

        'Write SENSe Subset to instrument
        If Not bSimulation Then WriteMsg(COUNTER, sCommand)
        Debug.Print(sCommand)

        'Configure ARM Options
        sCommand = ""
        Select Case UCase(sArmStartSource)
            Case "IMMEDIATE"
                sCommand = sCommand & ":ARM:STAR:SOUR IMM;"
            Case "VXI"
                sCommand = sCommand & ":ARM:STAR:SOUR BUS;"
            Case "TTL0", "TTL1", "TTL2", "TTL3", "TTL4", "TTL5", "TTL6", "TTL7"
                sCommand = sCommand & ":ARM:STAR:SOUR TTLT" & Right(sArmStartSource, 1) & ";"
                If Not bArmStartSlopNegative Then
                    sCommand = sCommand & ":ARM:STAR:SLOP POS;"
                Else
                    sCommand = sCommand & ":ARM:STAR:SLOP NEG;"
                End If
            Case "EXTERNAL"
                sCommand = sCommand & ":ARM:STAR:SOUR EXT;"
                Select Case UCase(sArmStartLevel)
                    Case "TTL"
                        sCommand = sCommand & ":ARM:STAR:LEV MAX"
                    Case "ECL"
                        sCommand = sCommand & ":ARM:STAR:LEV MIN;"
                    Case "GND"
                        sCommand = sCommand & ":ARM:STAR:LEV DEF;"
                End Select
                If Not bArmStartSlopNegative Then
                    sCommand = sCommand & ":ARM:STAR:SLOP POS;"
                Else
                    sCommand = sCommand & ":ARM:STAR:SLOP NEG;"
                End If
        End Select

        Select Case UCase(SArmStopSource)
            Case "IMMEDIATE"
                sCommand = sCommand & ":ARM:STOP:SOUR IMM;"
            Case "VXI"
                sCommand = sCommand & ":ARM:STOP:SOUR BUS;"
            Case "TTL0", "TTL1", "TTL2", "TTL3", "TTL4", "TTL5", "TTL6", "TTL7"
                sCommand = sCommand & ":ARM:STOP:SOUR TTLT" & Right(SArmStopSource, 1) & ";"
                If Not bArmStopSlopNegative Then
                    sCommand = sCommand & ":ARM:STOP:SLOP POS;"
                Else
                    sCommand = sCommand & ":ARM:STOP:SLOP NEG;"
                End If
            Case "EXTERNAL"
                sCommand = sCommand & ":ARM:STOP:SOUR EXT;"
                Select Case UCase(sArmStopLevel)
                    Case "TTL"
                        sCommand = sCommand & ":ARM:STOP:LEV MAX"
                    Case "ECL"
                        sCommand = sCommand & ":ARM:STOP:LEV MIN;"
                    Case "GND"
                        sCommand = sCommand & ":ARM:STOP:LEV DEF;"
                End Select
                If Not bArmStartSlopNegative Then
                    sCommand = sCommand & ":ARM:STOP:SLOP POS;"
                Else
                    sCommand = sCommand & ":ARM:STOP:SLOP NEG;"
                End If
        End Select

        'Write ARM Subset to instrument
        If Not bSimulation Then WriteMsg(COUNTER, sCommand)
        Debug.Print(sCommand)

        'Configure OUTPut
        sCommand = ""
        If Not bTriggerOutputOn Then
            For i = 0 To 7
                sCommand = sCommand & ":OUTP:TTLT" & i & ":STAT OFF;"
            Next i
            sCommand = sCommand & ":OUTP:ROSC:STAT OFF;"
        Else
            For i = 0 To 7
                If i = nTTLTriggerOutput Then
                    sCommand = sCommand & ":OUTP:TTLT" & i & ":STAT ON;"
                Else
                    sCommand = sCommand & ":OUTP:TTLT" & i & ":STAT OFF;"
                End If
            Next i
            sCommand = sCommand & ":OUTP:ROSC:STAT ON;"
        End If

        'Write OUTPut Subset to instrument
        If Not bSimulation Then WriteMsg(COUNTER, sCommand)
        Debug.Print(sCommand)

        'Configure CONFigure Options
        sCommand = ""
        If dRange <> -1 Then
            sCommand = ":CONF" & nChannel & ":TINT " & CStr(dRange) & ";"
            If Not bSimulation Then WriteMsg(COUNTER, sCommand)
            Debug.Print(sCommand)
        End If

        'Make Measurement
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub

        If Not bSimulation Then
            WriteMsg(COUNTER, ":INIT" & CStr(nChannel))
            Debug.Print("INIT" & CStr(nChannel))
            If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        End If
    End Sub

    Public Function dMeasPPulseWidth(Optional ByRef nChannel As Short = 1, Optional ByRef dRange As Double = 0.0000001,
                                     Optional ByRef bChOne10XAttenuation As Boolean = False, Optional ByRef bChTwo10XAttenuation As Boolean = False,
                                     Optional ByRef bChOneACCoupling As Boolean = False, Optional ByRef bChTwoACCoupling As Boolean = False,
                                     Optional ByRef bChOne50ohms As Boolean = False, Optional ByRef bChTwo50ohms As Boolean = False,
                                     Optional ByRef sHysteresis As String = "DEF", Optional ByRef bAutoTrigger As Boolean = False,
                                     Optional ByRef nRelativeTriggerLevel As Integer = 50, Optional ByRef bTriggerSlopeNegative As Boolean = False,
                                     Optional ByRef dAbsoluteTrigger As Double = 0.0, Optional ByRef bTriggerOutputOn As Boolean = False,
                                     Optional ByRef nTTLTriggerOutput As Integer = 0, Optional ByRef sArmStartSource As String = "IMMEDIATE",
                                     Optional ByRef bArmStartSlopNegative As Boolean = False, Optional ByRef sArmStartLevel As String = "TTL",
                                     Optional ByRef SArmStopSource As String = "IMMEDIATE", Optional ByRef bArmStopSlopNegative As Boolean = False,
                                     Optional ByRef sArmStopLevel As String = "TTL", Optional ByRef sTimeBaseSource As String = "INTERNAL",
                                     Optional ByRef bOutputTimeBase As Boolean = False, Optional ByRef bTotalizeGateMeasure As Boolean = False,
                                     Optional ByRef bTotalizeGatePolarityInverted As Boolean = False, Optional ByRef bTimeIntervalDelay As Boolean = False,
                                     Optional ByRef dTimeIntervalDelay As Double = 0.1, Optional ByRef sAcquisitionTimeOutSetting As String = "ON",
                                     Optional ByRef dAcquisitionTimeOut As Double = 5.0, Optional ByRef bGateAverage As Boolean = True,
                                     Optional ByRef APROBE As String = "OFF") As Double
        Dim i As Short
        Dim sData As String = ""
        Dim nErr As Integer
        Dim sCommand As String
        Dim Q As String
        Dim sCurrentMsg As String = ""
        Q = Chr(34)

        Dim iHandle As Integer
        iHandle = nInstrumentHandle(COUNTER)

        dMeasPPulseWidth = 0.0
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function

        gFrmMain.txtInstrument.Text = "Counter"
        gFrmMain.txtCommand.Text = "dMeasPPulseWidth"
        If APROBE <> "OFF" Then
            sCurrentMsg = gFrmMain.lblStatus.Text
            gFrmMain.lblStatus.Text = "APPLY PROBE"
        End If

        'Clear Counter
        If Not bSimulation Then
            '  viClear (nInstrumentHandle(COUNTER))
            nSystErr = atxmlDF_viClear(ResourceName(COUNTER), 0)
            WriteMsg(COUNTER, "*RST ; *CLS")
        End If

        'Error Check Arguments
        If nChannel < 1 Or nChannel > 2 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasPPulseWidth nChannel argument out of range.")
            Echo("RANGE: 1 TO 2")
            dMeasPPulseWidth = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
            dMeasPPulseWidth = 0
            Exit Function
        End If

        If dRange < 0.000000005 Or dRange > 0.01 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasPPulseWidth dRange argument out of range.")
            Echo("RANGE: 5E-9 TO 10E-3")
            dMeasPPulseWidth = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
            dMeasPPulseWidth = 0
            Exit Function
        End If

        Select Case UCase(sHysteresis)
            Case "DEF", "MAX", "MIN"
            Case Else
                Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasPPulseWidth sHysteresis argument out of range.")
                Echo("RANGE: DEF, MAX or MIN")
                dMeasPPulseWidth = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                dMeasPPulseWidth = 0
                Exit Function
        End Select

        If nRelativeTriggerLevel < 10 Or nRelativeTriggerLevel > 90 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasPPulseWidth nRelativeTriggerLevel argument out of range.")
            Echo("RANGE: 10 to 90")
            dMeasPPulseWidth = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
            dMeasPPulseWidth = 0
            Exit Function
        End If

        If dAbsoluteTrigger < -10.238 Or dAbsoluteTrigger > 10.238 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasPPulseWidth dAbsoluteTrigger argument out of range.")
            Echo("RANGE: -10.238 to 10.238")
            dMeasPPulseWidth = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
            dMeasPPulseWidth = 0
            Exit Function
        End If

        If nTTLTriggerOutput < 0 Or nTTLTriggerOutput > 7 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasPPulseWidth nTTLTriggerOutput argument out of range.")
            Echo("RANGE: 0 to 7")
            dMeasPPulseWidth = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
            dMeasPPulseWidth = 0
            Exit Function
        End If

        Select Case UCase(sArmStartSource)
            Case "IMMEDIATE", "VXI"
            Case "TTL0", "TTL1", "TTL2", "TTL3", "TTL4", "TTL5", "TTL6", "TTL7"
            Case "EXTERNAL"
                Select Case UCase(sArmStartLevel)
                    Case "TTL", "ECL", "GND"
                    Case Else
                        Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasPPulseWidth sArmStartLevel argument out of range.")
                        Echo("RANGE: TTL, ECL or GND")
                        dMeasPPulseWidth = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                        dMeasPPulseWidth = 0
                End Select
            Case Else
                Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasPPulseWidth sArmStartSource argument out of range.")
                Echo("RANGE: IMMEDIATE, VXI, TTL0, TTL1, TTL2, TTL3, TTL4, TTL5, TTL6, TTL7 or EXTERNAL")
                dMeasPPulseWidth = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                dMeasPPulseWidth = 0
                Exit Function
        End Select

        Select Case UCase(SArmStopSource)
            Case "IMMEDIATE", "VXI"
            Case "TTL0", "TTL1", "TTL2", "TTL3", "TTL4", "TTL5", "TTL6", "TTL7"
            Case "EXTERNAL"
                Select Case UCase(sArmStopLevel)
                    Case "TTL", "ECL", "GND"
                    Case Else
                        Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasPPulseWidth sArmStopLevel argument out of range.")
                        Echo("RANGE: TTL, ECL or GND")
                        dMeasPPulseWidth = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                        dMeasPPulseWidth = 0
                End Select
            Case Else
                Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasPPulseWidth sArmStopSource argument out of range.")
                Echo("RANGE: IMMEDIATE, VXI, TTL0, TTL1, TTL2, TTL3, TTL4, TTL5, TTL6, TTL7 or EXTERNAL")
                dMeasPPulseWidth = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                dMeasPPulseWidth = 0
                Exit Function
        End Select

        Select Case UCase(sTimeBaseSource)
            Case "VXI", "INTERNAL", "EXTERNAL"
            Case Else
                Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasPPulseWidth sTimeBaseSource argument out of range.")
                Echo("RANGE: VXI, INTERNAL or EXTERNAL")
                dMeasPPulseWidth = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                dMeasPPulseWidth = 0
                Exit Function
        End Select

        If dTimeIntervalDelay < 0.001 Or dTimeIntervalDelay > 99.999 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasPPulseWidth dTimeIntervalDelay argument out of range.")
            Echo("RANGE: 0.001 to 99.999")
            dMeasPPulseWidth = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
            dMeasPPulseWidth = 0
            Exit Function
        End If

        Select Case UCase(sAcquisitionTimeOutSetting)
            Case "ON", "OFF", "START"
            Case Else
                Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasPPulseWidth sAcquisitionTimeOutSetting argument out of range.")
                Echo("RANGE: ON, OFF, or START")
                dMeasPPulseWidth = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                dMeasPPulseWidth = 0
                Exit Function
        End Select

        If dAcquisitionTimeOut < 0.1 Or dAcquisitionTimeOut > 1500 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasPPulseWidth dAcquisitionTimeOut argument out of range.")
            Echo("RANGE: 0.1 to 1500")
            dMeasPPulseWidth = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
            dMeasPPulseWidth = 0
            Exit Function
        End If

        Select Case Left(UCase(APROBE), 1)
            Case "S", "O", "C" ' Single, Off, Continuous
            Case Else
                Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasPPulseWidth APROBE argument out of range.")
                Echo("RANGE: SINGLE, CONTINUOUS or OFF")
                dMeasPPulseWidth = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                dMeasPPulseWidth = 0
                Exit Function
        End Select

        'Configure Counter for Measurement
        'Set Input Options
        'Clear Counter
        If Not bSimulation Then
            '  viClear (nInstrumentHandle(COUNTER))
            nSystErr = atxmlDF_viClear(ResourceName(COUNTER), 0)
            WriteMsg(COUNTER, "*RST ; *CLS")
        End If

        sCommand = ""
        'Configure INPut Subsystem
        If nChannel = 1 Then
            'Set Attenuation
            If Not bChOne10XAttenuation Then
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:ATT 1;")
                Debug.Print(":INP1:ATT 1;")
            Else
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:ATT 10;")
                Debug.Print(":INP1:ATT 10;")
            End If

            'Set Coupling
            If Not bChOneACCoupling Then 'bbbb was simulation
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:COUP DC;")
                Debug.Print(":INP1:COUP DC;")
            Else
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:COUP AC;")
                Debug.Print(":INP1:COUP AC;")
            End If

            'Set Impedance
            If Not bChOne50ohms Then 'bbbb was simulation
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:IMP 1E6;")
                Debug.Print(":INP1:IMP 1E6;")
            Else
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:IMP 50;")
                Debug.Print(":INP1:IMP 50;")
            End If

            'Set Routing (Always separate for Period) 'bbbb was simulation
            If Not bSimulation Then WriteMsg(COUNTER, ":INP1:ROUT SEP;")
            Debug.Print(":INP1:ROUT SEP;")
        Else ' Channel 2
            'Set Attenuation
            If Not bChTwo10XAttenuation Then
                If Not bSimulation Then WriteMsg(COUNTER, ":INP2:ATT 1;")
                Debug.Print(":INP2:ATT 1;")
            Else
                If Not bSimulation Then WriteMsg(COUNTER, ":INP2:ATT 10;")
                Debug.Print(":INP2:ATT 10;")
            End If

            'Set Coupling
            If Not bChTwoACCoupling Then 'bbbb was simulation
                If Not bSimulation Then WriteMsg(COUNTER, ":INP2:COUP DC;")
                Debug.Print(":INP2:COUP DC;")
            Else
                If Not bSimulation Then WriteMsg(COUNTER, ":INP2:COUP AC;")
                Debug.Print(":INP2:COUP AC;")
            End If

            'Set Impedance
            If Not bChTwo50ohms Then 'bbbb was simulation
                If Not bSimulation Then WriteMsg(COUNTER, ":INP2:IMP 1E6;")
                Debug.Print(":INP2:IMP 1E6;")
            Else
                If Not bSimulation Then WriteMsg(COUNTER, ":INP2:IMP 50;")
                Debug.Print(":INP2:IMP 50;")
            End If
            'Set Routing (Always separate for Period)
            'If Not simulation Then WriteMsg COUNTER, ":INP2:ROUT SEP;"
            'Debug.Print ":INP2:ROUT SEP;"
        End If

        'Configure SENSe Options
        'Function PERIOD
        sCommand = ":SENS" & nChannel & ":FUNC " & Q & "PWID" & Q & ";"
        'Gate Average always off for Frequency
        If bGateAverage Then
            sCommand = sCommand & ":SENS" & nChannel & ":AVER:STAT ON;"
        Else
            sCommand = sCommand & ":SENS" & nChannel & ":AVER:STAT OFF;"
        End If

        'Set Aquisition Time Out
        sCommand = sCommand & ":SENS:ATIM:TIME " & CStr(dAcquisitionTimeOut) & ";"
        'Set Visa TMO value based on dAcqusitionTimeOut or dAperature setting + 1 Second
        If dAcquisitionTimeOut > dAperature Then
            If Not bSimulation Then lSystErr = viSetAttribute(nInstrumentHandle(COUNTER), VI_ATTR_TMO_VALUE, (dAcquisitionTimeOut * 1000) + 1000)
        Else
            If Not bSimulation Then lSystErr = viSetAttribute(nInstrumentHandle(COUNTER), VI_ATTR_TMO_VALUE, (dAperature * 1000) + 1000)
        End If

        If Not bSimulation And lSystErr Then 'bbbb was bsimulaton
            lSystErr = viStatusDesc(iHandle, lSystErr, lpBuffer)
            Echo("WRITEMSG ERROR: Error sending command to " & sInstrumentDescription(COUNTER) & ".")
            Echo("COMMAND: viSetAttribute")
            MsgBox("Error sending: viSetAttribute VI_ATTR_TMO_VALUE" & vbCrLf & "ERROR: " & lpBuffer, MsgBoxStyle.Exclamation, "VISA Error")
            dMeasPPulseWidth = 0
            Exit Function
        End If

        'Set Aquisition Time Out Mode
        Select Case UCase(sAcquisitionTimeOutSetting)
            Case "ON", "OFF"
                sCommand = sCommand & ":SENS:ATIM:CHEC " & UCase(sAcquisitionTimeOutSetting) & ";"
            Case "START"
                sCommand = sCommand & ":SENS:ATIM:CHEC STAR;"
        End Select

        'Set Auto Trigger or absolute
        If bAutoTrigger Then
            sCommand = sCommand & ":SENS" & nChannel & ":EVEN:LEV:ABS:AUTO ON;"
            sCommand = sCommand & ":SENS" & nChannel & ":EVEN:LEV:REL " & CStr(nRelativeTriggerLevel) & ";"
        Else
            sCommand = sCommand & ":SENS" & nChannel & ":EVEN:LEV:ABS " & CStr(dAbsoluteTrigger) & ";"
            sCommand = sCommand & ":SENS" & nChannel & ":EVEN:LEV:ABS:AUTO OFF;"
        End If

        'Set Trigger Slop
        If Not bTriggerSlopeNegative Then
            sCommand = sCommand & ":SENS" & nChannel & ":EVEN:SLOP POS;"
        Else
            sCommand = sCommand & ":SENS" & nChannel & ":EVEN:SLOP NEG;"
        End If

        'Set Hysteresis
        sCommand = sCommand & ":SENS" & nChannel & ":EVEN:HYST " & UCase(sHysteresis) & ";"
        'Set Timebase source
        Select Case UCase(sTimeBaseSource)
            Case "VXI"
                sCommand = sCommand & ":SENS" & nChannel & ":ROSC:SOUR CLK10;"
            Case "INTERNAL"
                sCommand = sCommand & ":SENS" & nChannel & ":ROSC:SOUR INT;"
            Case "EXTERNAL"
                sCommand = sCommand & ":SENS" & nChannel & ":ROSC:SOUR EXT;"
        End Select

        If bTimeIntervalDelay Then
            sCommand = sCommand & ":SENS" & nChannel & ":TINT:DEL:STAT ON;"
            sCommand = sCommand & ":SENS" & nChannel & ":TINT:DEL:TIME " & CStr(dTimeIntervalDelay) & ";"
        Else
            sCommand = sCommand & ":SENS" & nChannel & ":TINT:DEL:STAT OFF;"
        End If

        If bTotalizeGateMeasure Then
            sCommand = sCommand & ":SENS" & nChannel & ":TOT:GATE:STAT ON;"
            If bTotalizeGatePolarityInverted Then
                sCommand = sCommand & ":SENS" & nChannel & ":TOT:GATE:POL INV;"
            Else
                sCommand = sCommand & ":SENS" & nChannel & ":TOT:GATE:POL NORM;"
            End If
        Else
            sCommand = sCommand & ":SENS" & nChannel & ":TOT:GATE:STAT OFF;"
            sCommand = sCommand & ":SENS" & nChannel & ":TOT:GATE:POL NORM;"
        End If

        'Write SENSe Subset to instrument
        If Not bSimulation Then WriteMsg(COUNTER, sCommand)
        Debug.Print(sCommand)

        'Configure ARM Options
        sCommand = ""
        Select Case UCase(sArmStartSource)
            Case "IMMEDIATE"
                sCommand = sCommand & ":ARM:STAR:SOUR IMM;"
            Case "VXI"
                sCommand = sCommand & ":ARM:STAR:SOUR BUS;"
            Case "TTL0", "TTL1", "TTL2", "TTL3", "TTL4", "TTL5", "TTL6", "TTL7"
                sCommand = sCommand & ":ARM:STAR:SOUR TTLT" & Right(sArmStartSource, 1) & ";"
                If Not bArmStartSlopNegative Then
                    sCommand = sCommand & ":ARM:STAR:SLOP POS;"
                Else
                    sCommand = sCommand & ":ARM:STAR:SLOP NEG;"
                End If
            Case "EXTERNAL"
                sCommand = sCommand & ":ARM:STAR:SOUR EXT;"
                Select Case UCase(sArmStartLevel)
                    Case "TTL"
                        sCommand = sCommand & ":ARM:STAR:LEV MAX"
                    Case "ECL"
                        sCommand = sCommand & ":ARM:STAR:LEV MIN;"
                    Case "GND"
                        sCommand = sCommand & ":ARM:STAR:LEV DEF;"
                End Select

                If Not bArmStartSlopNegative Then
                    sCommand = sCommand & ":ARM:STAR:SLOP POS;"
                Else
                    sCommand = sCommand & ":ARM:STAR:SLOP NEG;"
                End If
        End Select

        Select Case UCase(SArmStopSource)
            Case "IMMEDIATE"
                sCommand = sCommand & ":ARM:STOP:SOUR IMM;"
            Case "VXI"
                sCommand = sCommand & ":ARM:STOP:SOUR BUS;"
            Case "TTL0", "TTL1", "TTL2", "TTL3", "TTL4", "TTL5", "TTL6", "TTL7"
                sCommand = sCommand & ":ARM:STOP:SOUR TTLT" & Right(SArmStopSource, 1) & ";"
                If Not bArmStopSlopNegative Then
                    sCommand = sCommand & ":ARM:STOP:SLOP POS;"
                Else
                    sCommand = sCommand & ":ARM:STOP:SLOP NEG;"
                End If
            Case "EXTERNAL"
                sCommand = sCommand & ":ARM:STOP:SOUR EXT;"
                Select Case UCase(sArmStopLevel)
                    Case "TTL"
                        sCommand = sCommand & ":ARM:STOP:LEV MAX"
                    Case "ECL"
                        sCommand = sCommand & ":ARM:STOP:LEV MIN;"
                    Case "GND"
                        sCommand = sCommand & ":ARM:STOP:LEV DEF;"
                End Select

                If Not bArmStartSlopNegative Then
                    sCommand = sCommand & ":ARM:STOP:SLOP POS;"
                Else
                    sCommand = sCommand & ":ARM:STOP:SLOP NEG;"
                End If
        End Select

        'Write ARM Subset to instrument
        If Not bSimulation Then WriteMsg(COUNTER, sCommand)
        Debug.Print(sCommand)

        'Configure OUTPut
        sCommand = ""
        If Not bTriggerOutputOn Then
            For i = 0 To 7
                sCommand = sCommand & ":OUTP:TTLT" & i & ":STAT OFF;"
            Next i
            sCommand = sCommand & ":OUTP:ROSC:STAT OFF;"
        Else
            For i = 0 To 7
                If i = nTTLTriggerOutput Then
                    sCommand = sCommand & ":OUTP:TTLT" & i & ":STAT ON;"
                Else
                    sCommand = sCommand & ":OUTP:TTLT" & i & ":STAT OFF;"
                End If
            Next i
            sCommand = sCommand & ":OUTP:ROSC:STAT ON;"
        End If
        'Write OUTPut Subset to instrument
        If Not bSimulation Then WriteMsg(COUNTER, sCommand)
        Debug.Print(sCommand)

        'Configure CONFigure Options
        sCommand = ""
        If dRange <> -1 Then
            sCommand = ":CONF" & nChannel & ":PWID 50, " & CStr(dRange) & ";"
            Debug.Print(sCommand)
        End If

        'Make Measurement
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        If Not bSimulation Then
            Select Case Left(UCase(APROBE), 1)
                Case "O" ' Off
                    WriteMsg(COUNTER, "INIT" & CStr(nChannel) & ";FETCH" & CStr(nChannel) & "?")
                    Debug.Print("INIT" & CStr(nChannel) & ";FETCH" & CStr(nChannel) & "?")
                    nErr = ReadMsg(COUNTER, sData)
                    If nErr <> 0 Then
                        dMeasPPulseWidth = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37)
                        Err.Raise(nErr)
                        Exit Function
                    End If

                    dMeasPPulseWidth = CDbl(sData)
                    gFrmMain.txtMeasured.Text = CStr(CSng(dMeasPPulseWidth))
                    If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
                Case "S" ' Single
                    MisProbe = MsgBoxResult.Yes
                    Do While MisProbe = MsgBoxResult.Yes
                        gFrmMain.lblStatus.Text = "Waiting for Probe ..."
                        Failed = False
                        UserEvent = 0
                        gFrmMain.TimerProbe.Enabled = True : gFrmMain.fraInstructions.Visible = True
                        Do While (UserEvent = 0) And (Not bProbeClosed)
                            System.Windows.Forms.Application.DoEvents()
                        Loop

                        gFrmMain.TimerProbe.Enabled = False
                        bProbeClosed = False
                        gFrmMain.lblStatus.Text = "Making Measurement ..."
                        WriteMsg(COUNTER, "INIT" & CStr(nChannel) & ";FETCH" & CStr(nChannel) & "?")
                        nErr = ReadMsg(COUNTER, sData)
                        If nErr <> 0 Then
                            dMeasPPulseWidth = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37)
                            Err.Raise(nErr)
                            Exit Function
                        End If

                        dMeasPPulseWidth = CDbl(sData)
                        gFrmMain.txtMeasured.Text = CStr(CSng(dMeasPPulseWidth))
                        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
                        gFrmMain.lblStatus.Text = "Measurement Complete ..."
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
                        Failed = False
                        UserEvent = 0
                        gFrmMain.TimerProbe.Enabled = True : gFrmMain.fraInstructions.Visible = True
                        Do While (UserEvent = 0) And (Not bProbeClosed)
                            gFrmMain.lblStatus.Text = "Making Measurement ..."
                            WriteMsg(COUNTER, "INIT" & CStr(nChannel) & ";FETCH" & CStr(nChannel) & "?")
                            nErr = ReadMsg(COUNTER, sData)
                            If nErr <> 0 Then
                                dMeasPPulseWidth = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37)
                                Err.Raise(nErr)
                                Exit Function
                            End If

                            dMeasPPulseWidth = CDbl(sData)
                            gFrmMain.txtMeasured.Text = CStr(CSng(dMeasPPulseWidth))
                            If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
                        Loop

                        gFrmMain.TimerProbe.Enabled = False
                        bProbeClosed = False
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
            End Select
        Else
            dMeasPPulseWidth = CDbl(InputBox("Command cmdCOUNTER.dMeasPPulseWidth peformed." & vbCrLf & "Enter Value:", "SIMULATION MODE"))
            gFrmMain.txtMeasured.Text = CStr(CSng(dMeasPPulseWidth))
            If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON)
        End If
    End Function

    Public Function dMeasNPulseWidth(Optional ByRef nChannel As Short = 1, Optional ByRef dRange As Double = 0.0000001,
                                     Optional ByRef bRouteSeparate As Boolean = True, Optional ByRef bChOne10XAttenuation As Boolean = False,
                                     Optional ByRef bChTwo10XAttenuation As Boolean = False, Optional ByRef bChOneACCoupling As Boolean = False,
                                     Optional ByRef bChTwoACCoupling As Boolean = False, Optional ByRef bChOne50ohms As Boolean = False,
                                     Optional ByRef bChTwo50ohms As Boolean = False, Optional ByRef sHysteresis As String = "DEF",
                                     Optional ByRef bAutoTrigger As Boolean = False, Optional ByRef nRelativeTriggerLevel As Integer = 50,
                                     Optional ByRef bTriggerSlopeNegative As Boolean = False, Optional ByRef dAbsoluteTrigger As Double = 0.0,
                                     Optional ByRef bTriggerOutputOn As Boolean = False, Optional ByRef nTTLTriggerOutput As Integer = 0,
                                     Optional ByRef sArmStartSource As String = "IMMEDIATE", Optional ByRef bArmStartSlopNegative As Boolean = False,
                                     Optional ByRef sArmStartLevel As String = "TTL", Optional ByRef SArmStopSource As String = "IMMEDIATE",
                                     Optional ByRef bArmStopSlopNegative As Boolean = False, Optional ByRef sArmStopLevel As String = "TTL",
                                     Optional ByRef sTimeBaseSource As String = "INTERNAL", Optional ByRef bOutputTimeBase As Boolean = False,
                                     Optional ByRef bTotalizeGateMeasure As Boolean = False,
                                     Optional ByRef bTotalizeGatePolarityInverted As Boolean = False, Optional ByRef bTimeIntervalDelay As Boolean = False,
                                     Optional ByRef dTimeIntervalDelay As Double = 0.1, Optional ByRef sAcquisitionTimeOutSetting As String = "ON",
                                     Optional ByRef dAcquisitionTimeOut As Double = 5.0, Optional ByRef bGateAverage As Boolean = True,
                                     Optional ByRef APROBE As String = "OFF") As Double
        Dim i As Short
        Dim sData As String = ""
        Dim nErr As Integer
        Dim sCommand As String
        Dim Q As String
        Dim sCurrentMsg As String = ""
        Q = Chr(34)

        Dim iHandle As Integer
        iHandle = nInstrumentHandle(COUNTER)

        dMeasNPulseWidth = 0.0
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function

        gFrmMain.txtInstrument.Text = "Counter"
        gFrmMain.txtCommand.Text = "dMeasNPulseWidth"
        If APROBE <> "OFF" Then
            sCurrentMsg = gFrmMain.lblStatus.Text
            gFrmMain.lblStatus.Text = "APPLY PROBE"
        End If

        'Clear Counter
        If Not bSimulation Then
            '  viClear (nInstrumentHandle(COUNTER))
            nSystErr = atxmlDF_viClear(ResourceName(COUNTER), 0)
            WriteMsg(COUNTER, "*RST ; *CLS")
        End If

        'Error Check Arguments
        If nChannel < 1 Or nChannel > 2 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasNPulseWidth nChannel argument out of range.")
            Echo("RANGE: 1 TO 2")
            dMeasNPulseWidth = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
            dMeasNPulseWidth = 0
            Exit Function
        End If

        If dRange < 0.000000005 Or dRange > 0.01 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasNPulseWidth dRange argument out of range.")
            Echo("RANGE: 5E-9 TO 10E-3")
            dMeasNPulseWidth = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
            dMeasNPulseWidth = 0
            Exit Function
        End If

        Select Case UCase(sHysteresis)
            Case "DEF", "MAX", "MIN"
            Case Else
                Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasNPulseWidth sHysteresis argument out of range.")
                Echo("RANGE: DEF, MAX or MIN")
                dMeasNPulseWidth = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                dMeasNPulseWidth = 0
                Exit Function
        End Select

        If nRelativeTriggerLevel < 10 Or nRelativeTriggerLevel > 90 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasNPulseWidth nRelativeTriggerLevel argument out of range.")
            Echo("RANGE: 10 to 90")
            dMeasNPulseWidth = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
            dMeasNPulseWidth = 0
            Exit Function
        End If

        If dAbsoluteTrigger < -10.238 Or dAbsoluteTrigger > 10.238 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasNPulseWidth dAbsoluteTrigger argument out of range.")
            Echo("RANGE: -10.238 to 10.238")
            dMeasNPulseWidth = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
            dMeasNPulseWidth = 0
            Exit Function
        End If

        If nTTLTriggerOutput < 0 Or nTTLTriggerOutput > 7 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasNPulseWidth nTTLTriggerOutput argument out of range.")
            Echo("RANGE: 0 to 7")
            dMeasNPulseWidth = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
            dMeasNPulseWidth = 0
            Exit Function
        End If

        Select Case UCase(sArmStartSource)
            Case "IMMEDIATE", "VXI"
            Case "TTL0", "TTL1", "TTL2", "TTL3", "TTL4", "TTL5", "TTL6", "TTL7"
            Case "EXTERNAL"
                Select Case UCase(sArmStartLevel)
                    Case "TTL", "ECL", "GND"
                    Case Else
                        Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasNPulseWidth sArmStartLevel argument out of range.")
                        Echo("RANGE: TTL, ECL or GND")
                        dMeasNPulseWidth = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                        dMeasNPulseWidth = 0
                End Select
            Case Else
                Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasNPulseWidth sArmStartSource argument out of range.")
                Echo("RANGE: IMMEDIATE, VXI, TTL0, TTL1, TTL2, TTL3, TTL4, TTL5, TTL6, TTL7 or EXTERNAL")
                dMeasNPulseWidth = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                dMeasNPulseWidth = 0
                Exit Function
        End Select
        Select Case UCase(SArmStopSource)
            Case "IMMEDIATE", "VXI"
            Case "TTL0", "TTL1", "TTL2", "TTL3", "TTL4", "TTL5", "TTL6", "TTL7"
            Case "EXTERNAL"
                Select Case UCase(sArmStopLevel)
                    Case "TTL", "ECL", "GND"
                    Case Else
                        Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasNPulseWidth sArmStopLevel argument out of range.")
                        Echo("RANGE: TTL, ECL or GND")
                        dMeasNPulseWidth = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                        dMeasNPulseWidth = 0
                End Select
            Case Else
                Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasNPulseWidth sArmStopSource argument out of range.")
                Echo("RANGE: IMMEDIATE, VXI, TTL0, TTL1, TTL2, TTL3, TTL4, TTL5, TTL6, TTL7 or EXTERNAL")
                dMeasNPulseWidth = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                dMeasNPulseWidth = 0
                Exit Function
        End Select
        Select Case UCase(sTimeBaseSource)
            Case "VXI", "INTERNAL", "EXTERNAL"
            Case Else
                Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasNPulseWidth sTimeBaseSource argument out of range.")
                Echo("RANGE: VXI, INTERNAL or EXTERNAL")
                dMeasNPulseWidth = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                dMeasNPulseWidth = 0
                Exit Function
        End Select
        If dTimeIntervalDelay < 0.001 Or dTimeIntervalDelay > 99.999 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasNPulseWidth dTimeIntervalDelay argument out of range.")
            Echo("RANGE: 0.001 to 99.999")
            dMeasNPulseWidth = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
            dMeasNPulseWidth = 0
            Exit Function
        End If
        Select Case UCase(sAcquisitionTimeOutSetting)
            Case "ON", "OFF", "START"
            Case Else
                Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasNPulseWidth sAcquisitionTimeOutSetting argument out of range.")
                Echo("RANGE: ON, OFF, or START")
                dMeasNPulseWidth = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                dMeasNPulseWidth = 0
                Exit Function
        End Select
        If dAcquisitionTimeOut < 0.1 Or dAcquisitionTimeOut > 1500 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasNPulseWidth dAcquisitionTimeOut argument out of range.")
            Echo("RANGE: 0.1 to 1500")
            dMeasNPulseWidth = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
            dMeasNPulseWidth = 0
            Exit Function
        End If
        Select Case Left(UCase(APROBE), 1)
            Case "S", "O", "C" ' Single, Off, Continuous
            Case Else
                Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasNPulseWidth APROBE argument out of range.")
                Echo("RANGE: SINGLE, CONTINUOUS or OFF")
                dMeasNPulseWidth = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                dMeasNPulseWidth = 0
                Exit Function
        End Select

        'Configure Counter for Measurement
        'Set Input Options
        'Clear Counter
        If Not bSimulation Then
            '  viClear (nInstrumentHandle(COUNTER))
            nSystErr = atxmlDF_viClear(ResourceName(COUNTER), 0)
            WriteMsg(COUNTER, "*RST ; *CLS")
        End If

        sCommand = ""
        'Configure INPut Subsystem
        If nChannel = 1 Then
            'Set Attenuation
            If Not bChOne10XAttenuation Then
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:ATT 1;")
                Debug.Print(":INP1:ATT 1;")
            Else
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:ATT 10;")
                Debug.Print(":INP1:ATT 10;")
            End If

            'Set Coupling
            If Not bChOneACCoupling Then 'bbbb was simulation
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:COUP DC;")
                Debug.Print(":INP1:COUP DC;")
            Else
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:COUP AC;")
                Debug.Print(":INP1:COUP AC;")
            End If

            'Set Impedance
            If Not bChOne50ohms Then 'bbbb was simulation
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:IMP 1E6;")
                Debug.Print(":INP1:IMP 1E6;")
            Else
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:IMP 50;")
                Debug.Print(":INP1:IMP 50;")
            End If

            'Set Routing (Always separate for Period) 'bbbb was simulation
            If Not bSimulation Then WriteMsg(COUNTER, ":INP1:ROUT SEP;")
            Debug.Print(":INP1:ROUT SEP;")
        Else ' Channel 2
            'Set Attenuation
            If Not bChTwo10XAttenuation Then
                If Not bSimulation Then WriteMsg(COUNTER, ":INP2:ATT 1;")
                Debug.Print(":INP2:ATT 1;")
            Else
                If Not bSimulation Then WriteMsg(COUNTER, ":INP2:ATT 10;")
                Debug.Print(":INP2:ATT 10;")
            End If

            'Set Coupling
            If Not bChTwoACCoupling Then 'bbbb was simulation
                If Not bSimulation Then WriteMsg(COUNTER, ":INP2:COUP DC;")
                Debug.Print(":INP2:COUP DC;")
            Else
                If Not bSimulation Then WriteMsg(COUNTER, ":INP2:COUP AC;")
                Debug.Print(":INP2:COUP AC;")
            End If

            'Set Impedance
            If Not bChTwo50ohms Then 'bbbb was simulation
                If Not bSimulation Then WriteMsg(COUNTER, ":INP2:IMP 1E6;")
                Debug.Print(":INP2:IMP 1E6;")
            Else
                If Not bSimulation Then WriteMsg(COUNTER, ":INP2:IMP 50;")
                Debug.Print(":INP2:IMP 50;")
            End If
            'Set Routing (Always separate for Period)
            'If Not bsimulation Then WriteMsg COUNTER, ":INP2:ROUT SEP;"
            'Debug.Print ":INP2:ROUT SEP;"
        End If

        'Configure SENSe Options
        'Function PERIOD
        sCommand = ":SENS" & nChannel & ":FUNC " & Q & "NWID" & Q & ";"
        'Gate Average always off for Frequency
        If bGateAverage Then
            sCommand = sCommand & ":SENS" & nChannel & ":AVER:STAT ON;"
        Else
            sCommand = sCommand & ":SENS" & nChannel & ":AVER:STAT OFF;"
        End If

        'Set Aquisition Time Out
        sCommand = sCommand & ":SENS:ATIM:TIME " & CStr(dAcquisitionTimeOut) & ";"
        'Set Visa TMO value based on dAcqusitionTimeOut or dAperature setting + 1 Second
        If dAcquisitionTimeOut > dAperature Then
            If Not bSimulation Then lSystErr = viSetAttribute(nInstrumentHandle(COUNTER), VI_ATTR_TMO_VALUE, (dAcquisitionTimeOut * 1000) + 1000)
        Else
            If Not bSimulation Then lSystErr = viSetAttribute(nInstrumentHandle(COUNTER), VI_ATTR_TMO_VALUE, (dAperature * 1000) + 1000)
        End If

        If Not bSimulation And lSystErr Then 'bbbb was bsimulaton
            lSystErr = viStatusDesc(iHandle, lSystErr, lpBuffer)
            Echo("WRITEMSG ERROR: Error sending command to " & sInstrumentDescription(COUNTER) & ".")
            Echo("COMMAND: viSetAttribute")
            MsgBox("Error sending: viSetAttribute VI_ATTR_TMO_VALUE" & vbCrLf & "ERROR: " & lpBuffer, MsgBoxStyle.Exclamation, "VISA Error")
            dMeasNPulseWidth = 0
            Exit Function
        End If

        'Set Aquisition Time Out Mode
        Select Case UCase(sAcquisitionTimeOutSetting)
            Case "ON", "OFF"
                sCommand = sCommand & ":SENS:ATIM:CHEC " & UCase(sAcquisitionTimeOutSetting) & ";"
            Case "START"
                sCommand = sCommand & ":SENS:ATIM:CHEC STAR;"
        End Select

        'Set Auto Trigger or absolute
        If bAutoTrigger Then
            sCommand = sCommand & ":SENS" & nChannel & ":EVEN:LEV:ABS:AUTO ON;"
            sCommand = sCommand & ":SENS" & nChannel & ":EVEN:LEV:REL " & CStr(nRelativeTriggerLevel) & ";"
        Else
            sCommand = sCommand & ":SENS" & nChannel & ":EVEN:LEV:ABS " & CStr(dAbsoluteTrigger) & ";"
            sCommand = sCommand & ":SENS" & nChannel & ":EVEN:LEV:ABS:AUTO OFF;"
        End If

        'Set Trigger Slop
        If Not bTriggerSlopeNegative Then
            sCommand = sCommand & ":SENS" & nChannel & ":EVEN:SLOP POS;"
        Else
            sCommand = sCommand & ":SENS" & nChannel & ":EVEN:SLOP NEG;"
        End If

        'Set Hysteresis
        sCommand = sCommand & ":SENS" & nChannel & ":EVEN:HYST " & UCase(sHysteresis) & ";"
        'Set Timebase source
        Select Case UCase(sTimeBaseSource)
            Case "VXI"
                sCommand = sCommand & ":SENS" & nChannel & ":ROSC:SOUR CLK10;"
            Case "INTERNAL"
                sCommand = sCommand & ":SENS" & nChannel & ":ROSC:SOUR INT;"
            Case "EXTERNAL"
                sCommand = sCommand & ":SENS" & nChannel & ":ROSC:SOUR EXT;"
        End Select

        If bTimeIntervalDelay Then
            sCommand = sCommand & ":SENS" & nChannel & ":TINT:DEL:STAT ON;"
            sCommand = sCommand & ":SENS" & nChannel & ":TINT:DEL:TIME " & CStr(dTimeIntervalDelay) & ";"
        Else
            sCommand = sCommand & ":SENS" & nChannel & ":TINT:DEL:STAT OFF;"
        End If

        If bTotalizeGateMeasure Then
            sCommand = sCommand & ":SENS" & nChannel & ":TOT:GATE:STAT ON;"
            If bTotalizeGatePolarityInverted Then
                sCommand = sCommand & ":SENS" & nChannel & ":TOT:GATE:POL INV;"
            Else
                sCommand = sCommand & ":SENS" & nChannel & ":TOT:GATE:POL NORM;"
            End If
        Else
            sCommand = sCommand & ":SENS" & nChannel & ":TOT:GATE:STAT OFF;"
            sCommand = sCommand & ":SENS" & nChannel & ":TOT:GATE:POL NORM;"
        End If

        'Write SENSe Subset to instrument
        If Not bSimulation Then WriteMsg(COUNTER, sCommand)
        Debug.Print(sCommand)

        'Configure ARM Options
        sCommand = ""
        Select Case UCase(sArmStartSource)
            Case "IMMEDIATE"
                sCommand = sCommand & ":ARM:STAR:SOUR IMM;"
            Case "VXI"
                sCommand = sCommand & ":ARM:STAR:SOUR BUS;"
            Case "TTL0", "TTL1", "TTL2", "TTL3", "TTL4", "TTL5", "TTL6", "TTL7"
                sCommand = sCommand & ":ARM:STAR:SOUR TTLT" & Right(sArmStartSource, 1) & ";"
                If Not bArmStartSlopNegative Then
                    sCommand = sCommand & ":ARM:STAR:SLOP POS;"
                Else
                    sCommand = sCommand & ":ARM:STAR:SLOP NEG;"
                End If
            Case "EXTERNAL"
                sCommand = sCommand & ":ARM:STAR:SOUR EXT;"
                Select Case UCase(sArmStartLevel)
                    Case "TTL"
                        sCommand = sCommand & ":ARM:STAR:LEV MAX"
                    Case "ECL"
                        sCommand = sCommand & ":ARM:STAR:LEV MIN;"
                    Case "GND"
                        sCommand = sCommand & ":ARM:STAR:LEV DEF;"
                End Select
                If Not bArmStartSlopNegative Then
                    sCommand = sCommand & ":ARM:STAR:SLOP POS;"
                Else
                    sCommand = sCommand & ":ARM:STAR:SLOP NEG;"
                End If
        End Select

        Select Case UCase(SArmStopSource)
            Case "IMMEDIATE"
                sCommand = sCommand & ":ARM:STOP:SOUR IMM;"
            Case "VXI"
                sCommand = sCommand & ":ARM:STOP:SOUR BUS;"
            Case "TTL0", "TTL1", "TTL2", "TTL3", "TTL4", "TTL5", "TTL6", "TTL7"
                sCommand = sCommand & ":ARM:STOP:SOUR TTLT" & Right(SArmStopSource, 1) & ";"
                If Not bArmStopSlopNegative Then
                    sCommand = sCommand & ":ARM:STOP:SLOP POS;"
                Else
                    sCommand = sCommand & ":ARM:STOP:SLOP NEG;"
                End If
            Case "EXTERNAL"
                sCommand = sCommand & ":ARM:STOP:SOUR EXT;"
                Select Case UCase(sArmStopLevel)
                    Case "TTL"
                        sCommand = sCommand & ":ARM:STOP:LEV MAX"
                    Case "ECL"
                        sCommand = sCommand & ":ARM:STOP:LEV MIN;"
                    Case "GND"
                        sCommand = sCommand & ":ARM:STOP:LEV DEF;"
                End Select
                If Not bArmStartSlopNegative Then
                    sCommand = sCommand & ":ARM:STOP:SLOP POS;"
                Else
                    sCommand = sCommand & ":ARM:STOP:SLOP NEG;"
                End If
        End Select

        'Write ARM Subset to instrument
        If Not bSimulation Then WriteMsg(COUNTER, sCommand)
        Debug.Print(sCommand)

        'Configure OUTPut
        sCommand = ""
        If Not bTriggerOutputOn Then
            For i = 0 To 7
                sCommand = sCommand & ":OUTP:TTLT" & i & ":STAT OFF;"
            Next i
            sCommand = sCommand & ":OUTP:ROSC:STAT OFF;"
        Else
            For i = 0 To 7
                If i = nTTLTriggerOutput Then
                    sCommand = sCommand & ":OUTP:TTLT" & i & ":STAT ON;"
                Else
                    sCommand = sCommand & ":OUTP:TTLT" & i & ":STAT OFF;"
                End If
            Next i
            sCommand = sCommand & ":OUTP:ROSC:STAT ON;"
        End If

        'Write OUTPut Subset to instrument
        If Not bSimulation Then WriteMsg(COUNTER, sCommand)
        Debug.Print(sCommand)
        'Configure CONFigure Options
        sCommand = ""
        If dRange <> -1 Then
            sCommand = ":CONF" & nChannel & ":NWID 50, " & CStr(dRange) & ";"
            Debug.Print(sCommand)
        End If

        'Make Measurement
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        If Not bSimulation Then
            Select Case Left(UCase(APROBE), 1)
                Case "O" ' Off
                    WriteMsg(COUNTER, "INIT" & CStr(nChannel) & ";FETCH" & CStr(nChannel) & "?")
                    Debug.Print("INIT" & CStr(nChannel) & ";FETCH" & CStr(nChannel) & "?")
                    nErr = ReadMsg(COUNTER, sData)
                    If nErr <> 0 Then
                        dMeasNPulseWidth = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37)
                        Err.Raise(nErr)
                        Exit Function
                    End If

                    dMeasNPulseWidth = CDbl(sData)
                    gFrmMain.txtMeasured.Text = CStr(CSng(dMeasNPulseWidth))
                    If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
                Case "S" ' Single
                    MisProbe = MsgBoxResult.Yes
                    Do While MisProbe = MsgBoxResult.Yes
                        gFrmMain.lblStatus.Text = "Waiting for Probe ..."
                        Failed = False
                        UserEvent = 0
                        gFrmMain.TimerProbe.Enabled = True : gFrmMain.fraInstructions.Visible = True
                        Do While (UserEvent = 0) And (Not bProbeClosed)
                            System.Windows.Forms.Application.DoEvents()
                        Loop

                        gFrmMain.TimerProbe.Enabled = False
                        bProbeClosed = False
                        gFrmMain.lblStatus.Text = "Making Measurement ..."
                        WriteMsg(COUNTER, "INIT" & CStr(nChannel) & ";FETCH" & CStr(nChannel) & "?")
                        nErr = ReadMsg(COUNTER, sData)
                        If nErr <> 0 Then
                            dMeasNPulseWidth = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37)
                            Err.Raise(nErr)
                            Exit Function
                        End If

                        dMeasNPulseWidth = CDbl(sData)
                        gFrmMain.txtMeasured.Text = CStr(CSng(dMeasNPulseWidth))
                        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
                        gFrmMain.lblStatus.Text = "Measurement Complete ..."
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
                        Failed = False
                        UserEvent = 0
                        gFrmMain.TimerProbe.Enabled = True : gFrmMain.fraInstructions.Visible = True
                        Do While (UserEvent = 0) And (Not bProbeClosed)
                            gFrmMain.lblStatus.Text = "Making Measurement ..."
                            WriteMsg(COUNTER, "INIT" & CStr(nChannel) & ";FETCH" & CStr(nChannel) & "?")
                            nErr = ReadMsg(COUNTER, sData)
                            If nErr <> 0 Then
                                dMeasNPulseWidth = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37)
                                Err.Raise(nErr)
                                Exit Function
                            End If

                            dMeasNPulseWidth = CDbl(sData)
                            gFrmMain.txtMeasured.Text = CStr(CSng(dMeasNPulseWidth))
                            If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
                        Loop
                        gFrmMain.TimerProbe.Enabled = False
                        bProbeClosed = False
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
            End Select
        Else
            dMeasNPulseWidth = CDbl(InputBox("Command cmdCOUNTER.dMeasNPulseWidth peformed." & vbCrLf & "Enter Value:", "SIMULATION MODE"))
            gFrmMain.txtMeasured.Text = CStr(CSng(dMeasNPulseWidth))
            If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON)
        End If
    End Function

    Public Function dMeasTimeInterval(Optional ByRef nChannel As Short = 1, Optional ByRef dRange As Double = 0.0000001,
                                      Optional ByRef bRouteSeparate As Boolean = True, Optional ByRef bChOne10XAttenuation As Boolean = False,
                                      Optional ByRef bChTwo10XAttenuation As Boolean = False, Optional ByRef bChOneACCoupling As Boolean = False,
                                      Optional ByRef bChTwoACCoupling As Boolean = False, Optional ByRef bChOne50ohms As Boolean = False,
                                      Optional ByRef bChTwo50ohms As Boolean = False, Optional ByRef sHysteresis As String = "DEF",
                                      Optional ByRef bAutoTrigger As Boolean = False, Optional ByRef nRelativeTriggerLevel As Integer = 50,
                                      Optional ByRef bTriggerSlopeNegative As Boolean = False, Optional ByRef dAbsoluteTrigger As Double = 0.0,
                                      Optional ByRef bTriggerOutputOn As Boolean = False, Optional ByRef nTTLTriggerOutput As Integer = 0,
                                      Optional ByRef sArmStartSource As String = "IMMEDIATE", Optional ByRef bArmStartSlopNegative As Boolean = False,
                                      Optional ByRef sArmStartLevel As String = "TTL", Optional ByRef SArmStopSource As String = "IMMEDIATE",
                                      Optional ByRef bArmStopSlopNegative As Boolean = False, Optional ByRef sArmStopLevel As String = "TTL",
                                      Optional ByRef sTimeBaseSource As String = "INTERNAL", Optional ByRef bOutputTimeBase As Boolean = False,
                                      Optional ByRef bTotalizeGateMeasure As Boolean = False,
                                      Optional ByRef bTotalizeGatePolarityInverted As Boolean = False, Optional ByRef bTimeIntervalDelay As Boolean = False,
                                      Optional ByRef dTimeIntervalDelay As Double = 0.1, Optional ByRef sAcquisitionTimeOutSetting As String = "ON",
                                      Optional ByRef dAcquisitionTimeOut As Double = 5.0, Optional ByRef bGateAverage As Boolean = True,
                                      Optional ByRef APROBE As String = "OFF") As Double
        Dim i As Short
        Dim sData As String = ""
        Dim nErr As Integer
        Dim sCommand As String
        Dim Q As String
        Dim sCurrentMsg As String = ""
        Q = Chr(34)

        Dim iHandle As Integer
        iHandle = nInstrumentHandle(COUNTER)

        dMeasTimeInterval = 0.0
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function

        gFrmMain.txtInstrument.Text = "Counter"
        gFrmMain.txtCommand.Text = "dMeasTimeInterval"
        If APROBE <> "OFF" Then
            sCurrentMsg = gFrmMain.lblStatus.Text
            gFrmMain.lblStatus.Text = "APPLY PROBE"
        End If

        'Clear Counter
        If Not bSimulation Then
            '  viClear (nInstrumentHandle(COUNTER))
            nSystErr = atxmlDF_viClear(ResourceName(COUNTER), 0)
            WriteMsg(COUNTER, "*RST ; *CLS")
        End If

        'Error Check Arguments
        If nChannel < 1 Or nChannel > 2 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasTimeInterval nChannel argument out of range.")
            Echo("RANGE: 1 TO 2")
            dMeasTimeInterval = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
            dMeasTimeInterval = 0
            Exit Function
        End If

        If dRange < 0.000000005 Or dRange > 1000 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasTimeInterval dRange argument out of range.")
            Echo("RANGE: 5E-9 TO 1000")
            dMeasTimeInterval = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
            dMeasTimeInterval = 0
            Exit Function
        End If

        Select Case UCase(sHysteresis)
            Case "DEF", "MAX", "MIN"
            Case Else
                Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasTimeInterval sHysteresis argument out of range.")
                Echo("RANGE: DEF, MAX or MIN")
                dMeasTimeInterval = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                dMeasTimeInterval = 0
                Exit Function
        End Select

        If nRelativeTriggerLevel < 10 Or nRelativeTriggerLevel > 90 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasTimeInterval nRelativeTriggerLevel argument out of range.")
            Echo("RANGE: 10 to 90")
            dMeasTimeInterval = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
            dMeasTimeInterval = 0
            Exit Function
        End If

        If dAbsoluteTrigger < -10.238 Or dAbsoluteTrigger > 10.238 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasTimeInterval dAbsoluteTrigger argument out of range.")
            Echo("RANGE: -10.238 to 10.238")
            dMeasTimeInterval = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
            dMeasTimeInterval = 0
            Exit Function
        End If

        If nTTLTriggerOutput < 0 Or nTTLTriggerOutput > 7 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasTimeInterval nTTLTriggerOutput argument out of range.")
            Echo("RANGE: 0 to 7")
            dMeasTimeInterval = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
            dMeasTimeInterval = 0
            Exit Function
        End If

        Select Case UCase(sArmStartSource)
            Case "IMMEDIATE", "VXI"
            Case "TTL0", "TTL1", "TTL2", "TTL3", "TTL4", "TTL5", "TTL6", "TTL7"
            Case "EXTERNAL"
                Select Case UCase(sArmStartLevel)
                    Case "TTL", "ECL", "GND"
                    Case Else
                        Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasTimeInterval sArmStartLevel argument out of range.")
                        Echo("RANGE: TTL, ECL or GND")
                        dMeasTimeInterval = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                        dMeasTimeInterval = 0
                End Select
            Case Else
                Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasTimeInterval sArmStartSource argument out of range.")
                Echo("RANGE: IMMEDIATE, VXI, TTL0, TTL1, TTL2, TTL3, TTL4, TTL5, TTL6, TTL7 or EXTERNAL")
                dMeasTimeInterval = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                dMeasTimeInterval = 0
                Exit Function
        End Select

        Select Case UCase(SArmStopSource)
            Case "IMMEDIATE", "VXI"
            Case "TTL0", "TTL1", "TTL2", "TTL3", "TTL4", "TTL5", "TTL6", "TTL7"
            Case "EXTERNAL"
                Select Case UCase(sArmStopLevel)
                    Case "TTL", "ECL", "GND"
                    Case Else
                        Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasTimeInterval sArmStopLevel argument out of range.")
                        Echo("RANGE: TTL, ECL or GND")
                        dMeasTimeInterval = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                        dMeasTimeInterval = 0
                End Select
            Case Else
                Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasTimeInterval sArmStopSource argument out of range.")
                Echo("RANGE: IMMEDIATE, VXI, TTL0, TTL1, TTL2, TTL3, TTL4, TTL5, TTL6, TTL7 or EXTERNAL")
                dMeasTimeInterval = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                dMeasTimeInterval = 0
                Exit Function
        End Select

        Select Case UCase(sTimeBaseSource)
            Case "VXI", "INTERNAL", "EXTERNAL"
            Case Else
                Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasTimeInterval sTimeBaseSource argument out of range.")
                Echo("RANGE: VXI, INTERNAL or EXTERNAL")
                dMeasTimeInterval = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                dMeasTimeInterval = 0
                Exit Function
        End Select

        If dTimeIntervalDelay < 0.001 Or dTimeIntervalDelay > 99.999 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasTimeInterval dTimeIntervalDelay argument out of range.")
            Echo("RANGE: 0.001 to 99.999")
            dMeasTimeInterval = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
            dMeasTimeInterval = 0
            Exit Function
        End If

        Select Case UCase(sAcquisitionTimeOutSetting)
            Case "ON", "OFF", "START"
            Case Else
                Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasTimeInterval sAcquisitionTimeOutSetting argument out of range.")
                Echo("RANGE: ON, OFF, or START")
                dMeasTimeInterval = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                dMeasTimeInterval = 0
                Exit Function
        End Select

        If dAcquisitionTimeOut < 0.1 Or dAcquisitionTimeOut > 1500 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasTimeInterval dAcquisitionTimeOut argument out of range.")
            Echo("RANGE: 0.1 to 1500")
            dMeasTimeInterval = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
            dMeasTimeInterval = 0
            Exit Function
        End If

        Select Case Left(UCase(APROBE), 1)
            Case "S", "O", "C" ' Single, Off, Continuous
            Case Else
                Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasTimeInterval APROBE argument out of range.")
                Echo("RANGE: SINGLE, CONTINUOUS or OFF")
                dMeasTimeInterval = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                dMeasTimeInterval = 0
                Exit Function
        End Select

        'Configure Counter for Measurement
        'Set Input Options
        'Clear Counter
        If Not bSimulation Then
            '  viClear (nInstrumentHandle(COUNTER))
            nSystErr = atxmlDF_viClear(ResourceName(COUNTER), 0)
            WriteMsg(COUNTER, "*RST ; *CLS")
        End If

        sCommand = ""
        'Configure INPut Subsystem
        If nChannel = 1 Then
            'Set Attenuation
            If Not bChOne10XAttenuation Then
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:ATT 1;")
                Debug.Print(":INP1:ATT 1;")
            Else
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:ATT 10;")
                Debug.Print(":INP1:ATT 10;")
            End If

            'Set Coupling
            If Not bChOneACCoupling Then 'bbbb was simulation
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:COUP DC;")
                Debug.Print(":INP1:COUP DC;")
            Else
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:COUP AC;")
                Debug.Print(":INP1:COUP AC;")
            End If

            'Set Impedance
            If Not bChOne50ohms Then 'bbbb was simulation
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:IMP 1E6;")
                Debug.Print(":INP1:IMP 1E6;")
            Else
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:IMP 50;")
                Debug.Print(":INP1:IMP 50;")
            End If

            'Set Routing (Always separate for Period)
            If bRouteSeparate Then 'bbbb was simulation
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:ROUT SEP;")
                Debug.Print(":INP1:ROUT SEP;")
            Else
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:ROUT SEP;")
                Debug.Print(":INP1:ROUT COMM;")
            End If
        Else ' Channel 2
            'Set Attenuation
            If Not bChTwo10XAttenuation Then
                If Not bSimulation Then WriteMsg(COUNTER, ":INP2:ATT 1;")
                Debug.Print(":INP2:ATT 1;")
            Else
                If Not bSimulation Then WriteMsg(COUNTER, ":INP2:ATT 10;")
                Debug.Print(":INP2:ATT 10;")
            End If

            'Set Coupling
            If Not bChTwoACCoupling Then 'bbbb was simulation
                If Not bSimulation Then WriteMsg(COUNTER, ":INP2:COUP DC;")
                Debug.Print(":INP2:COUP DC;")
            Else
                If Not bSimulation Then WriteMsg(COUNTER, ":INP2:COUP AC;")
                Debug.Print(":INP2:COUP AC;")
            End If

            'Set Impedance
            If Not bChTwo50ohms Then 'bbbb was simulation
                If Not bSimulation Then WriteMsg(COUNTER, ":INP2:IMP 1E6;")
                Debug.Print(":INP2:IMP 1E6;")
            Else
                If Not bSimulation Then WriteMsg(COUNTER, ":INP2:IMP 50;")
                Debug.Print(":INP2:IMP 50;")
            End If
            'Set Routing (Always separate for Period)
            'If Not bsimulation Then WriteMsg COUNTER, ":INP2:ROUT SEP;"
            'Debug.Print ":INP2:ROUT SEP;"
        End If

        'Configure SENSe Options
        'Function PERIOD
        sCommand = ":SENS" & nChannel & ":FUNC " & Q & "TINT" & Q & ";"
        'Gate Average always off for Frequency
        If bGateAverage Then
            sCommand = sCommand & ":SENS" & nChannel & ":AVER:STAT ON;"
        Else
            sCommand = sCommand & ":SENS" & nChannel & ":AVER:STAT OFF;"
        End If

        'Set Aquisition Time Out
        sCommand = sCommand & ":SENS:ATIM:TIME " & CStr(dAcquisitionTimeOut) & ";"
        'Set Visa TMO value based on dAcqusitionTimeOut or dAperature setting + 1 Second
        If dAcquisitionTimeOut > dAperature Then
            If Not bSimulation Then lSystErr = viSetAttribute(nInstrumentHandle(COUNTER), VI_ATTR_TMO_VALUE, (dAcquisitionTimeOut * 1000) + 1000)
        Else
            If Not bSimulation Then lSystErr = viSetAttribute(nInstrumentHandle(COUNTER), VI_ATTR_TMO_VALUE, (dAperature * 1000) + 1000)
        End If

        If Not bSimulation And lSystErr Then 'bbbb was bsimulaton
            lSystErr = viStatusDesc(iHandle, lSystErr, lpBuffer)
            Echo("WRITEMSG ERROR: Error sending command to " & sInstrumentDescription(COUNTER) & ".")
            Echo("COMMAND: viSetAttribute")
            MsgBox("Error sending: viSetAttribute VI_ATTR_TMO_VALUE" & vbCrLf & "ERROR: " & lpBuffer, MsgBoxStyle.Exclamation, "VISA Error")
            dMeasTimeInterval = 0
            Exit Function
        End If

        'Set Aquisition Time Out Mode
        Select Case UCase(sAcquisitionTimeOutSetting)
            Case "ON", "OFF"
                sCommand = sCommand & ":SENS:ATIM:CHEC " & UCase(sAcquisitionTimeOutSetting) & ";"
            Case "START"
                sCommand = sCommand & ":SENS:ATIM:CHEC STAR;"
        End Select

        'Set Auto Trigger or absolute
        If bAutoTrigger Then
            sCommand = sCommand & ":SENS" & nChannel & ":EVEN:LEV:ABS:AUTO ON;"
            sCommand = sCommand & ":SENS" & nChannel & ":EVEN:LEV:REL " & CStr(nRelativeTriggerLevel) & ";"
        Else
            sCommand = sCommand & ":SENS" & nChannel & ":EVEN:LEV:ABS " & CStr(dAbsoluteTrigger) & ";"
            sCommand = sCommand & ":SENS" & nChannel & ":EVEN:LEV:ABS:AUTO OFF;"
        End If

        'Set Trigger Slop
        If Not bTriggerSlopeNegative Then
            sCommand = sCommand & ":SENS" & nChannel & ":EVEN:SLOP POS;"
        Else
            sCommand = sCommand & ":SENS" & nChannel & ":EVEN:SLOP NEG;"
        End If

        'Set Hysteresis
        sCommand = sCommand & ":SENS" & nChannel & ":EVEN:HYST " & UCase(sHysteresis) & ";"
        'Set Timebase source
        Select Case UCase(sTimeBaseSource)
            Case "VXI"
                sCommand = sCommand & ":SENS" & nChannel & ":ROSC:SOUR CLK10;"
            Case "INTERNAL"
                sCommand = sCommand & ":SENS" & nChannel & ":ROSC:SOUR INT;"
            Case "EXTERNAL"
                sCommand = sCommand & ":SENS" & nChannel & ":ROSC:SOUR EXT;"
        End Select

        If bTimeIntervalDelay Then
            sCommand = sCommand & ":SENS" & nChannel & ":TINT:DEL:STAT ON;"
            sCommand = sCommand & ":SENS" & nChannel & ":TINT:DEL:TIME " & CStr(dTimeIntervalDelay) & ";"
        Else
            sCommand = sCommand & ":SENS" & nChannel & ":TINT:DEL:STAT OFF;"
        End If

        If bTotalizeGateMeasure Then
            sCommand = sCommand & ":SENS" & nChannel & ":TOT:GATE:STAT ON;"
            If bTotalizeGatePolarityInverted Then
                sCommand = sCommand & ":SENS" & nChannel & ":TOT:GATE:POL INV;"
            Else
                sCommand = sCommand & ":SENS" & nChannel & ":TOT:GATE:POL NORM;"
            End If
        Else
            sCommand = sCommand & ":SENS" & nChannel & ":TOT:GATE:STAT OFF;"
            sCommand = sCommand & ":SENS" & nChannel & ":TOT:GATE:POL NORM;"
        End If

        'Write SENSe Subset to instrument
        If Not bSimulation Then WriteMsg(COUNTER, sCommand)
        Debug.Print(sCommand)

        'Configure ARM Options
        sCommand = ""
        Select Case UCase(sArmStartSource)
            Case "IMMEDIATE"
                sCommand = sCommand & ":ARM:STAR:SOUR IMM;"
            Case "VXI"
                sCommand = sCommand & ":ARM:STAR:SOUR BUS;"
            Case "TTL0", "TTL1", "TTL2", "TTL3", "TTL4", "TTL5", "TTL6", "TTL7"
                sCommand = sCommand & ":ARM:STAR:SOUR TTLT" & Right(sArmStartSource, 1) & ";"
                If Not bArmStartSlopNegative Then
                    sCommand = sCommand & ":ARM:STAR:SLOP POS;"
                Else
                    sCommand = sCommand & ":ARM:STAR:SLOP NEG;"
                End If
            Case "EXTERNAL"
                sCommand = sCommand & ":ARM:STAR:SOUR EXT;"
                Select Case UCase(sArmStartLevel)
                    Case "TTL"
                        sCommand = sCommand & ":ARM:STAR:LEV MAX"
                    Case "ECL"
                        sCommand = sCommand & ":ARM:STAR:LEV MIN;"
                    Case "GND"
                        sCommand = sCommand & ":ARM:STAR:LEV DEF;"
                End Select
                If Not bArmStartSlopNegative Then
                    sCommand = sCommand & ":ARM:STAR:SLOP POS;"
                Else
                    sCommand = sCommand & ":ARM:STAR:SLOP NEG;"
                End If
        End Select

        Select Case UCase(SArmStopSource)
            Case "IMMEDIATE"
                sCommand = sCommand & ":ARM:STOP:SOUR IMM;"
            Case "VXI"
                sCommand = sCommand & ":ARM:STOP:SOUR BUS;"
            Case "TTL0", "TTL1", "TTL2", "TTL3", "TTL4", "TTL5", "TTL6", "TTL7"
                sCommand = sCommand & ":ARM:STOP:SOUR TTLT" & Right(SArmStopSource, 1) & ";"
                If Not bArmStopSlopNegative Then
                    sCommand = sCommand & ":ARM:STOP:SLOP POS;"
                Else
                    sCommand = sCommand & ":ARM:STOP:SLOP NEG;"
                End If
            Case "EXTERNAL"
                sCommand = sCommand & ":ARM:STOP:SOUR EXT;"
                Select Case UCase(sArmStopLevel)
                    Case "TTL"
                        sCommand = sCommand & ":ARM:STOP:LEV MAX"
                    Case "ECL"
                        sCommand = sCommand & ":ARM:STOP:LEV MIN;"
                    Case "GND"
                        sCommand = sCommand & ":ARM:STOP:LEV DEF;"
                End Select
                If Not bArmStartSlopNegative Then
                    sCommand = sCommand & ":ARM:STOP:SLOP POS;"
                Else
                    sCommand = sCommand & ":ARM:STOP:SLOP NEG;"
                End If
        End Select

        'Write ARM Subset to instrument
        If Not bSimulation Then WriteMsg(COUNTER, sCommand)
        Debug.Print(sCommand)

        'Configure OUTPut
        sCommand = ""
        If Not bTriggerOutputOn Then
            For i = 0 To 7
                sCommand = sCommand & ":OUTP:TTLT" & i & ":STAT OFF;"
            Next i
            sCommand = sCommand & ":OUTP:ROSC:STAT OFF;"
        Else
            For i = 0 To 7
                If i = nTTLTriggerOutput Then
                    sCommand = sCommand & ":OUTP:TTLT" & i & ":STAT ON;"
                Else
                    sCommand = sCommand & ":OUTP:TTLT" & i & ":STAT OFF;"
                End If
            Next i
            sCommand = sCommand & ":OUTP:ROSC:STAT ON;"
        End If

        'Write OUTPut Subset to instrument
        If Not bSimulation Then WriteMsg(COUNTER, sCommand)
        Debug.Print(sCommand)

        'Configure CONFigure Options
        sCommand = ""
        If dRange <> -1 Then
            sCommand = ":CONF" & nChannel & ":TINT " & CStr(dRange) & ";"
            If Not bSimulation Then WriteMsg(COUNTER, sCommand)
            Debug.Print(sCommand)
        End If

        'Make Measurement
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        If Not bSimulation Then
            Select Case Left(UCase(APROBE), 1)
                Case "O" ' Off
                    WriteMsg(COUNTER, "INIT" & CStr(nChannel) & ";FETCH" & CStr(nChannel) & "?")
                    Debug.Print("INIT" & CStr(nChannel) & ";FETCH" & CStr(nChannel) & "?")
                    nErr = ReadMsg(COUNTER, sData)
                    If nErr <> 0 Then
                        dMeasTimeInterval = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37)
                        Err.Raise(nErr)
                        Exit Function
                    End If

                    dMeasTimeInterval = CDbl(sData)
                    gFrmMain.txtMeasured.Text = CStr(CSng(dMeasTimeInterval))
                    If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
                Case "S" ' Single
                    MisProbe = MsgBoxResult.Yes
                    Do While MisProbe = MsgBoxResult.Yes
                        gFrmMain.lblStatus.Text = "Waiting for Probe ..."
                        Failed = False
                        UserEvent = 0
                        gFrmMain.TimerProbe.Enabled = True : gFrmMain.fraInstructions.Visible = True
                        Do While (UserEvent = 0) And (Not bProbeClosed)
                            System.Windows.Forms.Application.DoEvents()
                        Loop

                        gFrmMain.TimerProbe.Enabled = False
                        bProbeClosed = False
                        gFrmMain.lblStatus.Text = "Making Measurement ..."
                        WriteMsg(COUNTER, "INIT" & CStr(nChannel) & ";FETCH" & CStr(nChannel) & "?")
                        nErr = ReadMsg(COUNTER, sData)
                        If nErr <> 0 Then
                            dMeasTimeInterval = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37)
                            Err.Raise(nErr)
                            Exit Function
                        End If

                        dMeasTimeInterval = CDbl(sData)
                        gFrmMain.txtMeasured.Text = CStr(CSng(dMeasTimeInterval))
                        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
                        gFrmMain.lblStatus.Text = "Measurement Complete ..."
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
                        Failed = False
                        UserEvent = 0
                        gFrmMain.TimerProbe.Enabled = True : gFrmMain.fraInstructions.Visible = True
                        Do While (UserEvent = 0) And (Not bProbeClosed)
                            gFrmMain.lblStatus.Text = "Making Measurement ..."
                            WriteMsg(COUNTER, "INIT" & CStr(nChannel) & ";FETCH" & CStr(nChannel) & "?")
                            nErr = ReadMsg(COUNTER, sData)
                            If nErr <> 0 Then
                                dMeasTimeInterval = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37)
                                Err.Raise(nErr)
                                Exit Function
                            End If

                            dMeasTimeInterval = CDbl(sData)
                            gFrmMain.txtMeasured.Text = CStr(CSng(dMeasTimeInterval))
                            If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
                        Loop

                        gFrmMain.TimerProbe.Enabled = False
                        bProbeClosed = False
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
            End Select
        Else
            dMeasTimeInterval = CDbl(InputBox("Command cmdCOUNTER.dMeasTimeInterval peformed." & vbCrLf & "Enter Value:", "SIMULATION MODE"))
            gFrmMain.txtMeasured.Text = CStr(CSng(dMeasTimeInterval))
            If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON)
        End If
    End Function

    Public Function dMeasFreq(Optional ByRef nChannel As Short = 1, Optional ByRef dRange As Double = -1, Optional ByRef dAperature As Double = 0.1,
                              Optional ByRef bChOne10XAttenuation As Boolean = False, Optional ByRef bChTwo10XAttenuation As Boolean = False,
                              Optional ByRef bChOneACCoupling As Boolean = False, Optional ByRef bChTwoACCoupling As Boolean = False,
                              Optional ByRef bChOne50ohms As Boolean = False, Optional ByRef bChTwo50ohms As Boolean = False,
                              Optional ByRef sHysteresis As String = "DEF", Optional ByRef bAutoTrigger As Boolean = False,
                              Optional ByRef nRelativeTriggerLevel As Integer = 50, Optional ByRef bTriggerSlopeNegative As Boolean = False,
                              Optional ByRef dAbsoluteTrigger As Double = 0.0, Optional ByRef bTriggerOutputOn As Boolean = False,
                              Optional ByRef nTTLTriggerOutput As Integer = 0, Optional ByRef sArmStartSource As String = "IMMEDIATE",
                              Optional ByRef bArmStartSlopNegative As Boolean = False, Optional ByRef sArmStartLevel As String = "TTL",
                              Optional ByRef SArmStopSource As String = "IMMEDIATE", Optional ByRef bArmStopSlopNegative As Boolean = False,
                              Optional ByRef sArmStopLevel As String = "TTL", Optional ByRef sTimeBaseSource As String = "INTERNAL",
                              Optional ByRef bOutputTimeBase As Boolean = False, Optional ByRef bTotalizeGateMeasure As Boolean = False,
                              Optional ByRef bTotalizeGatePolarityInverted As Boolean = False, Optional ByRef bTimeIntervalDelay As Boolean = False,
                              Optional ByRef dTimeIntervalDelay As Double = 0.1, Optional ByRef sAcquisitionTimeOutSetting As String = "ON",
                              Optional ByRef dAcquisitionTimeOut As Double = 5.0, Optional ByRef APROBE As String = "OFF",
                              Optional ByRef bReset As Boolean = True) As Double
        Dim i As Short
        Dim sData As String = ""
        Dim nErr As Integer
        Dim bAutoRange As Boolean
        Dim sCommand As String
        Dim Q As String
        Dim sCurrentMsg As String = ""
        Q = Chr(34)

        Dim iHandle As Integer
        iHandle = nInstrumentHandle(COUNTER)

        dMeasFreq = 0.0
        If dRange = -1 Then bAutoRange = True Else bAutoRange = False
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function

        gFrmMain.txtInstrument.Text = "Counter"
        gFrmMain.txtCommand.Text = "dMeasFreq"
        If APROBE <> "OFF" Then
            sCurrentMsg = gFrmMain.lblStatus.Text
            gFrmMain.lblStatus.Text = "APPLY PROBE"
        End If

        'Clear Counter
        If bReset Then
            If Not bSimulation Then
                '  viClear (nInstrumentHandle(COUNTER))
                nSystErr = atxmlDF_viClear(ResourceName(COUNTER), 0)
                WriteMsg(COUNTER, "*RST ; *CLS")
            End If
        Else
            If Not bSimulation Then WriteMsg(COUNTER, "*CLS")
        End If

        'Error Check Arguments
        If nChannel < 1 Or nChannel > 2 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasFreq nChannel argument out of range.")
            Echo("RANGE: 1 TO 2")
            dMeasFreq = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
            dMeasFreq = 0
            Exit Function
        End If

        If Not bAutoRange Then
            If dRange < 0.1 Or dRange > 200000000.0 Then
                Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasFreq dRange argument out of range.")
                Echo("RANGE: 0.1 TO 200e6 OR -1 FOR AUTORANGE")
                dMeasFreq = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                dMeasFreq = 0
                Exit Function
            End If
        End If

        If dAperature < 0.1 Or dAperature > 99.999 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasFreq dAperature argument out of range.")
            Echo("RANGE: 0.1 TO 99.999")
            dMeasFreq = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
            dMeasFreq = 0
            Exit Function
        End If

        Select Case UCase(sHysteresis)
            Case "DEF", "MAX", "MIN"
            Case Else
                Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasFreq sHysteresis argument out of range.")
                Echo("RANGE: DEF, MAX or MIN")
                dMeasFreq = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                dMeasFreq = 0
                Exit Function
        End Select

        If nRelativeTriggerLevel < 10 Or nRelativeTriggerLevel > 90 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasFreq nRelativeTriggerLevel argument out of range.")
            Echo("RANGE: 10 to 90")
            dMeasFreq = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
            dMeasFreq = 0
            Exit Function
        End If

        If dAbsoluteTrigger < -10.238 Or dAbsoluteTrigger > 10.238 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasFreq dAbsoluteTrigger argument out of range.")
            Echo("RANGE: -10.238 to 10.238")
            dMeasFreq = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
            dMeasFreq = 0
            Exit Function
        End If

        If nTTLTriggerOutput < 0 Or nTTLTriggerOutput > 7 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasFreq nTTLTriggerOutput argument out of range.")
            Echo("RANGE: 0 to 7")
            dMeasFreq = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
            dMeasFreq = 0
            Exit Function
        End If

        Select Case UCase(sArmStartSource)
            Case "IMMEDIATE", "VXI"
            Case "TTL0", "TTL1", "TTL2", "TTL3", "TTL4", "TTL5", "TTL6", "TTL7"
            Case "EXTERNAL"
                Select Case UCase(sArmStartLevel)
                    Case "TTL", "ECL", "GND"
                    Case Else
                        Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasFreq sArmStartLevel argument out of range.")
                        Echo("RANGE: TTL, ECL or GND")
                        dMeasFreq = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                        dMeasFreq = 0
                End Select
            Case Else
                Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasFreq sArmStartSource argument out of range.")
                Echo("RANGE: IMMEDIATE, VXI, TTL0, TTL1, TTL2, TTL3, TTL4, TTL5, TTL6, TTL7 or EXTERNAL")
                dMeasFreq = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                dMeasFreq = 0
                Exit Function
        End Select

        Select Case UCase(SArmStopSource)
            Case "IMMEDIATE", "VXI"
            Case "TTL0", "TTL1", "TTL2", "TTL3", "TTL4", "TTL5", "TTL6", "TTL7"
            Case "EXTERNAL"
                Select Case UCase(sArmStopLevel)
                    Case "TTL", "ECL", "GND"
                    Case Else
                        Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasFreq sArmStopLevel argument out of range.")
                        Echo("RANGE: TTL, ECL or GND")
                        dMeasFreq = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                        dMeasFreq = 0
                End Select
            Case Else
                Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasFreq sArmStopSource argument out of range.")
                Echo("RANGE: IMMEDIATE, VXI, TTL0, TTL1, TTL2, TTL3, TTL4, TTL5, TTL6, TTL7 or EXTERNAL")
                dMeasFreq = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                dMeasFreq = 0
                Exit Function
        End Select

        Select Case UCase(sTimeBaseSource)
            Case "VXI", "INTERNAL", "EXTERNAL"
            Case Else
                Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasFreq sTimeBaseSource argument out of range.")
                Echo("RANGE: VXI, INTERNAL or EXTERNAL")
                dMeasFreq = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                dMeasFreq = 0
                Exit Function
        End Select

        If dTimeIntervalDelay < 0.001 Or dTimeIntervalDelay > 99.999 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasFreq dTimeIntervalDelay argument out of range.")
            Echo("RANGE: 0.001 to 99.999")
            dMeasFreq = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
            dMeasFreq = 0
            Exit Function
        End If

        Select Case UCase(sAcquisitionTimeOutSetting)
            Case "ON", "OFF", "START"
            Case Else
                Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasFreq sAcquisitionTimeOutSetting argument out of range.")
                Echo("RANGE: ON, OFF, or START")
                dMeasFreq = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                dMeasFreq = 0
                Exit Function
        End Select

        If dAcquisitionTimeOut < 0.1 Or dAcquisitionTimeOut > 1500 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasFreq dAcquisitionTimeOut argument out of range.")
            Echo("RANGE: 0.1 to 1500")
            dMeasFreq = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
            dMeasFreq = 0
            Exit Function
        End If

        Select Case Left(UCase(APROBE), 1)
            Case "S", "O", "C" ' Single, Off, Continuous
            Case Else
                Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasFreq APROBE argument out of range.")
                Echo("RANGE: SINGLE or OFF")
                dMeasFreq = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37) : Err.Raise(-1001)
                dMeasFreq = 0
                Exit Function
        End Select

        'Configure Counter for Measurement
        'Set Input Options
        'Clear Counter

        If bReset Then
            If Not bSimulation Then
                '  viClear (nInstrumentHandle(COUNTER))
                nSystErr = atxmlDF_viClear(ResourceName(COUNTER), 0)
                WriteMsg(COUNTER, "*RST ; *CLS")
            End If
        Else
            If Not bSimulation Then WriteMsg(COUNTER, "*CLS")
        End If

        sCommand = ""
        'Configure INPut Subsystem
        If nChannel = 1 Then
            'Set Attenuation
            If Not bChOne10XAttenuation Then
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:ATT 1;")
                Debug.Print(":INP1:ATT 1;")
            Else
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:ATT 10;")
                Debug.Print(":INP1:ATT 10;")
            End If

            'Set Coupling
            If Not bChOneACCoupling Then 'bbbb was simulation
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:COUP DC;")
                Debug.Print(":INP1:COUP DC;")
            Else
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:COUP AC;")
                Debug.Print(":INP1:COUP AC;")
            End If

            'Set Impedance
            If Not bChOne50ohms Then 'bbbb was simulation
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:IMP 1E6;")
                Debug.Print(":INP1:IMP 1E6;")
            Else
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:IMP 50;")
                Debug.Print(":INP1:IMP 50;")

            End If

            'Set Routing (Always separate for Frequency) 'bbbb was simulation
            If Not bSimulation Then WriteMsg(COUNTER, ":INP1:ROUT SEP;")
            Debug.Print(":INP1:ROUT SEP;")
        Else ' Channel 2
            'Set Attenuation
            If Not bChTwo10XAttenuation Then
                If Not bSimulation Then WriteMsg(COUNTER, ":INP2:ATT 1;")
                Debug.Print(":INP2:ATT 1;")
            Else
                If Not bSimulation Then WriteMsg(COUNTER, ":INP2:ATT 10;")
                Debug.Print(":INP2:ATT 10;")
            End If

            'Set Coupling
            If Not bChTwoACCoupling Then 'bbbb was simulation
                If Not bSimulation Then WriteMsg(COUNTER, ":INP2:COUP DC;")
                Debug.Print(":INP2:COUP DC;")
            Else
                If Not bSimulation Then WriteMsg(COUNTER, ":INP2:COUP AC;")
                Debug.Print(":INP2:COUP AC;")
            End If

            'Set Impedance
            If Not bChTwo50ohms Then 'bbbb was simulation
                If Not bSimulation Then WriteMsg(COUNTER, ":INP2:IMP 1E6;")
                Debug.Print(":INP2:IMP 1E6;")
            Else
                If Not bSimulation Then WriteMsg(COUNTER, ":INP2:IMP 50;")
                Debug.Print(":INP2:IMP 50;")
            End If
            'Set Routing (Always separate for Frequency)
            'If Not bsimulation Then WriteMsg COUNTER, ":INP2:ROUT SEP;"
            'Debug.Print ":INP2:ROUT SEP;"
        End If

        'Configure SENSe Options
        'Function FREQUENCY
        sCommand = ":SENS" & nChannel & ":FUNC " & Q & "FREQ" & Q & ";"
        'Gate Average always off for Frequency
        sCommand = sCommand & ":SENS" & nChannel & ":AVER:STAT OFF;"
        'Set Aquisition Time Out
        sCommand = sCommand & ":SENS:ATIM:TIME " & CStr(dAcquisitionTimeOut) & ";"
        'Set Visa TMO value based on dAcqusitionTimeOut or dAperature setting + 1 Second
        If dAcquisitionTimeOut > dAperature Then
            If Not bSimulation Then
                lSystErr = viSetAttribute(nInstrumentHandle(COUNTER), VI_ATTR_TMO_VALUE, (dAcquisitionTimeOut * 1000) + 1000)
            End If
        Else
            If Not bSimulation Then
                lSystErr = viSetAttribute(nInstrumentHandle(COUNTER), VI_ATTR_TMO_VALUE, (dAperature * 1000) + 1000)
            End If
        End If

        If Not bSimulation And lSystErr Then 'bbbb was bsimulaton
            lSystErr = viStatusDesc(iHandle, lSystErr, lpBuffer)
            Echo("WRITEMSG ERROR: Error sending command to " & sInstrumentDescription(COUNTER) & ".")
            Echo("COMMAND: viSetAttribute")
            MsgBox("Error sending: viSetAttribute VI_ATTR_TMO_VALUE" & vbCrLf & "ERROR: " & lpBuffer, MsgBoxStyle.Exclamation, "VISA Error")
            dMeasFreq = 0
            Exit Function
        End If

        'Set Aquisition Time Out Mode
        Select Case UCase(sAcquisitionTimeOutSetting)
            Case "ON", "OFF"
                sCommand = sCommand & ":SENS:ATIM:CHEC " & UCase(sAcquisitionTimeOutSetting) & ";"
            Case "START"
                sCommand = sCommand & ":SENS:ATIM:CHEC STAR;"
        End Select

        'Set Auto Trigger or absolute
        If bAutoTrigger Then
            sCommand = sCommand & ":SENS" & nChannel & ":EVEN:LEV:ABS:AUTO ON;"
            sCommand = sCommand & ":SENS" & nChannel & ":EVEN:LEV:REL " & CStr(nRelativeTriggerLevel) & ";"
        Else
            sCommand = sCommand & ":SENS" & nChannel & ":EVEN:LEV:ABS " & CStr(dAbsoluteTrigger) & ";"
            sCommand = sCommand & ":SENS" & nChannel & ":EVEN:LEV:ABS:AUTO OFF;"
        End If

        'Set Trigger Slop
        If Not bTriggerSlopeNegative Then
            sCommand = sCommand & ":SENS" & nChannel & ":EVEN:SLOP POS;"
        Else
            sCommand = sCommand & ":SENS" & nChannel & ":EVEN:SLOP NEG;"
        End If

        'Set Hysteresis
        sCommand = sCommand & ":SENS" & nChannel & ":EVEN:HYST " & UCase(sHysteresis) & ";"
        'Set Aperature
        sCommand = sCommand & ":SENS" & nChannel & ":FREQ:APER " & CStr(dAperature) & ";"
        'Set Range
        If dRange = -1 Then
            sCommand = sCommand & ":SENS" & nChannel & ":FREQ:RANG:AUTO ON;"
        Else
            sCommand = sCommand & ":SENS" & nChannel & ":FREQ:RANG:AUTO OFF;"
        End If

        'Set Timebase source
        Select Case UCase(sTimeBaseSource)
            Case "VXI"
                sCommand = sCommand & ":SENS" & nChannel & ":ROSC:SOUR CLK10;"
            Case "INTERNAL"
                sCommand = sCommand & ":SENS" & nChannel & ":ROSC:SOUR INT;"
            Case "EXTERNAL"
                sCommand = sCommand & ":SENS" & nChannel & ":ROSC:SOUR EXT;"
        End Select

        If bTimeIntervalDelay Then
            sCommand = sCommand & ":SENS" & nChannel & ":TINT:DEL:STAT ON;"
            sCommand = sCommand & ":SENS" & nChannel & ":TINT:DEL:TIME " & CStr(dTimeIntervalDelay) & ";"
        Else
            sCommand = sCommand & ":SENS" & nChannel & ":TINT:DEL:STAT OFF;"
        End If

        If bTotalizeGateMeasure Then
            sCommand = sCommand & ":SENS" & nChannel & ":TOT:GATE:STAT ON;"
            If bTotalizeGatePolarityInverted Then
                sCommand = sCommand & ":SENS" & nChannel & ":TOT:GATE:POL INV;"
            Else
                sCommand = sCommand & ":SENS" & nChannel & ":TOT:GATE:POL NORM;"
            End If
        Else
            sCommand = sCommand & ":SENS" & nChannel & ":TOT:GATE:STAT OFF;"
            sCommand = sCommand & ":SENS" & nChannel & ":TOT:GATE:POL NORM;"
        End If

        Delay(0.5) 'bbbb added delay to let the instrument settle
        'Write SENSe Subset to instrument
        If Not bSimulation Then WriteMsg(COUNTER, sCommand)
        Debug.Print(sCommand)

        'Configure ARM Options
        sCommand = ""
        Select Case UCase(sArmStartSource)
            Case "IMMEDIATE"
                sCommand = sCommand & ":ARM:STAR:SOUR IMM;"
            Case "VXI"
                sCommand = sCommand & ":ARM:STAR:SOUR BUS;"
            Case "TTL0", "TTL1", "TTL2", "TTL3", "TTL4", "TTL5", "TTL6", "TTL7"
                sCommand = sCommand & ":ARM:STAR:SOUR TTLT" & Right(sArmStartSource, 1) & ";"
                If Not bArmStartSlopNegative Then
                    sCommand = sCommand & ":ARM:STAR:SLOP POS;"
                Else
                    sCommand = sCommand & ":ARM:STAR:SLOP NEG;"
                End If
            Case "EXTERNAL"
                sCommand = sCommand & ":ARM:STAR:SOUR EXT;"
                Select Case UCase(sArmStartLevel)
                    Case "TTL"
                        sCommand = sCommand & ":ARM:STAR:LEV MAX"
                    Case "ECL"
                        sCommand = sCommand & ":ARM:STAR:LEV MIN;"
                    Case "GND"
                        sCommand = sCommand & ":ARM:STAR:LEV DEF;"
                End Select
                If Not bArmStartSlopNegative Then
                    sCommand = sCommand & ":ARM:STAR:SLOP POS;"
                Else
                    sCommand = sCommand & ":ARM:STAR:SLOP NEG;"
                End If
        End Select

        Select Case UCase(SArmStopSource)
            Case "IMMEDIATE"
                sCommand = sCommand & ":ARM:STOP:SOUR IMM;"
            Case "VXI"
                sCommand = sCommand & ":ARM:STOP:SOUR BUS;"
            Case "TTL0", "TTL1", "TTL2", "TTL3", "TTL4", "TTL5", "TTL6", "TTL7"
                sCommand = sCommand & ":ARM:STOP:SOUR TTLT" & Right(SArmStopSource, 1) & ";"
                If Not bArmStopSlopNegative Then
                    sCommand = sCommand & ":ARM:STOP:SLOP POS;"
                Else
                    sCommand = sCommand & ":ARM:STOP:SLOP NEG;"
                End If
            Case "EXTERNAL"
                sCommand = sCommand & ":ARM:STOP:SOUR EXT;"
                Select Case UCase(sArmStopLevel)
                    Case "TTL"
                        sCommand = sCommand & ":ARM:STOP:LEV MAX"
                    Case "ECL"
                        sCommand = sCommand & ":ARM:STOP:LEV MIN;"
                    Case "GND"
                        sCommand = sCommand & ":ARM:STOP:LEV DEF;"
                End Select
                If Not bArmStartSlopNegative Then
                    sCommand = sCommand & ":ARM:STOP:SLOP POS;"
                Else
                    sCommand = sCommand & ":ARM:STOP:SLOP NEG;"
                End If
        End Select

        'Write ARM Subset to instrument
        If Not bSimulation Then WriteMsg(COUNTER, sCommand)
        Debug.Print(sCommand)

        'Configure OUTPut
        sCommand = ""
        If Not bTriggerOutputOn Then
            For i = 0 To 7
                sCommand = sCommand & ":OUTP:TTLT" & i & ":STAT OFF;"
            Next i
            sCommand = sCommand & ":OUTP:ROSC:STAT OFF;"
        Else
            For i = 0 To 7
                If i = nTTLTriggerOutput Then
                    sCommand = sCommand & ":OUTP:TTLT" & i & ":STAT ON;"
                Else
                    sCommand = sCommand & ":OUTP:TTLT" & i & ":STAT OFF;"
                End If
            Next i
            sCommand = sCommand & ":OUTP:ROSC:STAT ON;"
        End If

        'Write OUTPut Subset to instrument
        If Not bSimulation Then WriteMsg(COUNTER, sCommand)
        Debug.Print(sCommand)

        'Configure CONFigure Options
        sCommand = ""
        If dRange <> -1 Then
            sCommand = ":CONF" & nChannel & ":FREQ " & CStr(dRange) & ";"
            If Not bSimulation Then WriteMsg(COUNTER, sCommand)
            Debug.Print(sCommand)
        End If

        'Make Measurement
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        If Not bSimulation Then
            Select Case Left(UCase(APROBE), 1)
                Case "O" ' Off
                    WriteMsg(COUNTER, "INIT" & CStr(nChannel) & ";FETCH" & CStr(nChannel) & "?")
                    nErr = ReadMsg(COUNTER, sData)
                    If nErr <> 0 Then
                        dMeasFreq = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37)
                        Err.Raise(nErr)
                        Exit Function
                    End If

                    dMeasFreq = CDbl(sData)
                    gFrmMain.txtMeasured.Text = CStr(CSng(dMeasFreq))
                    If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
                Case "S" ' Single
                    MisProbe = MsgBoxResult.Yes
                    Do While MisProbe = MsgBoxResult.Yes
                        gFrmMain.lblStatus.Text = "Waiting for Probe ..."
                        Failed = False
                        UserEvent = 0
                        gFrmMain.TimerProbe.Enabled = True : gFrmMain.fraInstructions.Visible = True
                        Do While (UserEvent = 0) And (Not bProbeClosed)
                            System.Windows.Forms.Application.DoEvents()
                        Loop

                        gFrmMain.TimerProbe.Enabled = False
                        bProbeClosed = False
                        gFrmMain.lblStatus.Text = "Making Measurement ..."
                        WriteMsg(COUNTER, "INIT" & CStr(nChannel) & ";FETCH" & CStr(nChannel) & "?")
                        nErr = ReadMsg(COUNTER, sData)
                        If nErr <> 0 Then
                            dMeasFreq = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37)
                            Err.Raise(nErr)
                            Exit Function
                        End If

                        dMeasFreq = CDbl(sData)
                        gFrmMain.txtMeasured.Text = CStr(CSng(dMeasFreq))
                        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
                        gFrmMain.lblStatus.Text = "Measurement Complete ..."
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
                        Failed = False
                        UserEvent = 0
                        gFrmMain.TimerProbe.Enabled = True : gFrmMain.fraInstructions.Visible = True
                        Do While (UserEvent = 0) And (Not bProbeClosed)
                            gFrmMain.lblStatus.Text = "Making Measurement ..."
                            WriteMsg(COUNTER, "INIT" & CStr(nChannel) & ";FETCH" & CStr(nChannel) & "?")
                            nErr = ReadMsg(COUNTER, sData)
                            If nErr <> 0 Then
                                dMeasFreq = 9.9E+37 : gFrmMain.txtMeasured.Text = CStr(9.9E+37)
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
            End Select
        Else
            dMeasFreq = CDbl(InputBox("Command cmdCOUNTER.dMeasFreq peformed." & vbCrLf & "Enter Value:", "SIMULATION MODE"))
            gFrmMain.txtMeasured.Text = CStr(CSng(dMeasFreq))
            If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON)
        End If
    End Function

    Public Sub ArmTotalize(Optional ByRef nChannel As Short = 1,
                           Optional ByRef bChOne10XAttenuation As Boolean = False, Optional ByRef bChTwo10XAttenuation As Boolean = False,
                           Optional ByRef bChOneACCoupling As Boolean = False, Optional ByRef bChTwoACCoupling As Boolean = False,
                           Optional ByRef bChOne50ohms As Boolean = False, Optional ByRef bChTwo50ohms As Boolean = False,
                           Optional ByRef sHysteresis As String = "DEF", Optional ByRef bAutoTrigger As Boolean = False,
                           Optional ByRef nRelativeTriggerLevel As Integer = 50, Optional ByRef bTriggerSlopeNegative As Boolean = False,
                           Optional ByRef dAbsoluteTrigger As Double = 0.0, Optional ByRef bTriggerOutputOn As Boolean = False,
                           Optional ByRef nTTLTriggerOutput As Integer = 0, Optional ByRef sArmStartSource As String = "IMMEDIATE",
                           Optional ByRef bArmStartSlopNegative As Boolean = False, Optional ByRef sArmStartLevel As String = "TTL",
                           Optional ByRef SArmStopSource As String = "IMMEDIATE", Optional ByRef bArmStopSlopNegative As Boolean = False,
                           Optional ByRef sArmStopLevel As String = "TTL", Optional ByRef sTimeBaseSource As String = "INTERNAL",
                           Optional ByRef bOutputTimeBase As Boolean = False, Optional ByRef bTotalizeGateMeasure As Boolean = False,
                           Optional ByRef bTotalizeGatePolarityInverted As Boolean = False, Optional ByRef bTimeIntervalDelay As Boolean = False,
                           Optional ByRef dTimeIntervalDelay As Double = 0.1, Optional ByRef sAcquisitionTimeOutSetting As String = "ON",
                           Optional ByRef dAcquisitionTimeOut As Double = 5.0, Optional ByRef APROBE As String = "OFF")
        Dim i As Short
        Dim sCommand As String
        Dim Q As String
        Q = Chr(34)

        gFrmMain.txtMeasured.Text = ""

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub

        gFrmMain.txtInstrument.Text = "Counter"
        gFrmMain.txtCommand.Text = "bStartTotalize"
        Dim sCurrentMsg As String
        If APROBE <> "OFF" Then
            sCurrentMsg = gFrmMain.lblStatus.Text
            gFrmMain.lblStatus.Text = "APPLY PROBE"
        End If

        'Clear Counter
        If Not bSimulation Then
            '  viClear (nInstrumentHandle(COUNTER))
            nSystErr = atxmlDF_viClear(ResourceName(COUNTER), 0)
            WriteMsg(COUNTER, "*RST ; *CLS")
        End If

        'Error Check Arguments
        If nChannel < 1 Or nChannel > 2 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.bStartTotalize nChannel argument out of range.")
            Echo("RANGE: 1 TO 2")
            Err.Raise(-1001)

            Exit Sub
        End If

        Select Case UCase(sHysteresis)
            Case "DEF", "MAX", "MIN"
            Case Else
                Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.bStartTotalize sHysteresis argument out of range.")
                Echo("RANGE: DEF, MAX or MIN")
                Err.Raise(-1001)

                Exit Sub
        End Select

        If nRelativeTriggerLevel < 10 Or nRelativeTriggerLevel > 90 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.bStartTotalize nRelativeTriggerLevel argument out of range.")
            Echo("RANGE: 10 to 90")
            Err.Raise(-1001)

            Exit Sub
        End If

        If dAbsoluteTrigger < -10.238 Or dAbsoluteTrigger > 10.238 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.bStartTotalize dAbsoluteTrigger argument out of range.")
            Echo("RANGE: -10.238 to 10.238")
            Err.Raise(-1001)

            Exit Sub
        End If

        If nTTLTriggerOutput < 0 Or nTTLTriggerOutput > 7 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.bStartTotalize nTTLTriggerOutput argument out of range.")
            Echo("RANGE: 0 to 7")
            Err.Raise(-1001)

            Exit Sub
        End If

        Select Case UCase(sArmStartSource)
            Case "IMMEDIATE", "VXI"
            Case "TTL0", "TTL1", "TTL2", "TTL3", "TTL4", "TTL5", "TTL6", "TTL7"
            Case "EXTERNAL"
                Select Case UCase(sArmStartLevel)
                    Case "TTL", "ECL", "GND"
                    Case Else
                        Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.bStartTotalize sArmStartLevel argument out of range.")
                        Echo("RANGE: TTL, ECL or GND")
                        Err.Raise(-1001)
                End Select
            Case Else
                Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.bStartTotalize sArmStartSource argument out of range.")
                Echo("RANGE: IMMEDIATE, VXI, TTL0, TTL1, TTL2, TTL3, TTL4, TTL5, TTL6, TTL7 or EXTERNAL")
                Err.Raise(-1001)

                Exit Sub
        End Select

        Select Case UCase(SArmStopSource)
            Case "IMMEDIATE", "VXI"
            Case "TTL0", "TTL1", "TTL2", "TTL3", "TTL4", "TTL5", "TTL6", "TTL7"
            Case "EXTERNAL"
                Select Case UCase(sArmStopLevel)
                    Case "TTL", "ECL", "GND"
                    Case Else
                        Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.bStartTotalize sArmStopLevel argument out of range.")
                        Echo("RANGE: TTL, ECL or GND")
                        Err.Raise(-1001)
                End Select
            Case Else
                Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.bStartTotalize sArmStopSource argument out of range.")
                Echo("RANGE: IMMEDIATE, VXI, TTL0, TTL1, TTL2, TTL3, TTL4, TTL5, TTL6, TTL7 or EXTERNAL")
                Err.Raise(-1001)

                Exit Sub
        End Select

        Select Case UCase(sTimeBaseSource)
            Case "VXI", "INTERNAL", "EXTERNAL"
            Case Else
                Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.bStartTotalize sTimeBaseSource argument out of range.")
                Echo("RANGE: VXI, INTERNAL or EXTERNAL")
                Err.Raise(-1001)
                Exit Sub
        End Select

        If dTimeIntervalDelay < 0.001 Or dTimeIntervalDelay > 99.999 Then
            Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.bStartTotalize dTimeIntervalDelay argument out of range.")
            Echo("RANGE: 0.001 to 99.999")
            Err.Raise(-1001)
            Exit Sub
        End If

        Select Case UCase(sAcquisitionTimeOutSetting)
            Case "ON", "OFF", "START"
            Case Else
                Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.bStartTotalize sAcquisitionTimeOutSetting argument out of range.")
                Echo("RANGE: ON, OFF, or START")

                Exit Sub
        End Select

        '    If dAcquisitionTimeOut < 0.1 Or dAcquisitionTimeOut > 1500 Then
        '        Echo "COUNTER PROGRAMMING ERROR:  Command cmdCounter.bStartTotalize dAcquisitionTimeOut argument out of range."
        '        Echo "RANGE: 0.1 to 1500"
        '        Err.Raise -1001
        '
        '        Exit Sub
        '    End If
        If Not bSimulation Then lSystErr = viSetAttribute(nInstrumentHandle(COUNTER), VI_ATTR_TMO_VALUE, (dAcquisitionTimeOut * 1000) + 1000)

        Select Case Left(UCase(APROBE), 1)
            Case "S", "O", "C" ' Single, Off, Continuous
            Case Else
                Echo("COUNTER PROGRAMMING ERROR:  Command cmdCounter.bStartTotalize APROBE argument out of range.")
                Echo("RANGE: SINGLE or OFF")
                Err.Raise(-1001)
                Exit Sub
        End Select

        'Configure Counter for Measurement
        'Set Input Options
        'Clear Counter
        If Not bSimulation Then
            '  viClear (nInstrumentHandle(COUNTER))
            nSystErr = atxmlDF_viClear(ResourceName(COUNTER), 0)
            WriteMsg(COUNTER, "*RST ; *CLS")
        End If

        sCommand = ""
        'Configure INPut Subsystem
        If nChannel = 1 Then
            'Set Attenuation
            If Not bChOne10XAttenuation Then
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:ATT 1;")
                Debug.Print(":INP1:ATT 1;")
            Else
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:ATT 10;")
                Debug.Print(":INP1:ATT 10;")
            End If

            'Set Coupling
            If Not bChOneACCoupling Then 'bbbb was simulation
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:COUP DC;")
                Debug.Print(":INP1:COUP DC;")
            Else
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:COUP AC;")
                Debug.Print(":INP1:COUP AC;")
            End If

            'Set Impedance
            If Not bChOne50ohms Then 'bbbb was simulation
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:IMP 1E6;")
                Debug.Print(":INP1:IMP 1E6;")
            Else
                If Not bSimulation Then WriteMsg(COUNTER, ":INP1:IMP 50;")
                Debug.Print(":INP1:IMP 50;")
            End If

            'Set Routing (Always separate for Period) 'bbbb was simulation
            If Not bSimulation Then WriteMsg(COUNTER, ":INP1:ROUT SEP;")
            Debug.Print(":INP1:ROUT SEP;")
        Else ' Channel 2
            'Set Attenuation
            If Not bChTwo10XAttenuation Then
                If Not bSimulation Then WriteMsg(COUNTER, ":INP2:ATT 1;")
                Debug.Print(":INP2:ATT 1;")
            Else
                If Not bSimulation Then WriteMsg(COUNTER, ":INP2:ATT 10;")
                Debug.Print(":INP2:ATT 10;")
            End If

            'Set Coupling
            If Not bChTwoACCoupling Then 'bbbb was simulation
                If Not bSimulation Then WriteMsg(COUNTER, ":INP2:COUP DC;")
                Debug.Print(":INP2:COUP DC;")
            Else
                If Not bSimulation Then WriteMsg(COUNTER, ":INP2:COUP AC;")
                Debug.Print(":INP2:COUP AC;")
            End If

            'Set Impedance
            If Not bChTwo50ohms Then 'bbbb was simulation
                If Not bSimulation Then WriteMsg(COUNTER, ":INP2:IMP 1E6;")
                Debug.Print(":INP2:IMP 1E6;")
            Else
                If Not bSimulation Then WriteMsg(COUNTER, ":INP2:IMP 50;")
                Debug.Print(":INP2:IMP 50;")
            End If
            'Set Routing (Always separate for Period)
            'If Not bsimulation Then WriteMsg COUNTER, ":INP2:ROUT SEP;"
            'Debug.Print ":INP2:ROUT SEP;"
        End If

        'Configure SENSe Options
        'Function PERIOD
        sCommand = ":SENS" & nChannel & ":FUNC " & Q & "TOT" & Q & ";"
        'Gate Average always off for Frequency
        sCommand = sCommand & ":SENS" & nChannel & ":AVER:STAT OFF;"
        'Set Aquisition Time Out
        sCommand = sCommand & ":SENS:ATIM:TIME " & CStr(dAcquisitionTimeOut) & ";"
        'Set Visa TMO value based on dAcqusitionTimeOut or dAperature setting + 1 Second
        '        If dAcquisitionTimeOut > dAperature Then
        '            If Not bSimulation Then lSystErr = viSetAttribute(nInstrumentHandle(COUNTER), VI_ATTR_TMO_VALUE, (dAcquisitionTimeOut * 1000) + 1000)
        '        Else
        '            If Not bSimulation Then lSystErr = viSetAttribute(nInstrumentHandle(COUNTER), VI_ATTR_TMO_VALUE, (dAperature * 1000) + 1000)
        '        End If
        '        If Not bsimulation And lSystErr Then
        '          lSystErr = viStatusDesc(ihandle, lSystErr, lpBuffer)
        '          Echo "WRITEMSG ERROR: Error sending command to " & sInstrumentDescription(COUNTER) & "."
        '          Echo "COMMAND: viSetAttribute"
        '          MsgBox "Error sending: viSetAttribute VI_ATTR_TMO_VALUE" & vbCrLf & "ERROR: " & lpBuffer, vbExclamation, "VISA Error"
        '
        '          Exit Sub
        '        End If
        'Set Aquisition Time Out Mode
        Select Case UCase(sAcquisitionTimeOutSetting)
            Case "ON", "OFF"
                sCommand = sCommand & ":SENS:ATIM:CHEC " & UCase(sAcquisitionTimeOutSetting) & ";"
            Case "START"
                sCommand = sCommand & ":SENS:ATIM:CHEC STAR;"
        End Select

        'Set Auto Trigger or absolute
        If bAutoTrigger Then
            sCommand = sCommand & ":SENS" & nChannel & ":EVEN:LEV:ABS:AUTO ON;"
            sCommand = sCommand & ":SENS" & nChannel & ":EVEN:LEV:REL " & CStr(nRelativeTriggerLevel) & ";"
        Else
            sCommand = sCommand & ":SENS" & nChannel & ":EVEN:LEV:ABS " & CStr(dAbsoluteTrigger) & ";"
            sCommand = sCommand & ":SENS" & nChannel & ":EVEN:LEV:ABS:AUTO OFF;"
        End If

        'Set Trigger Slop
        If Not bTriggerSlopeNegative Then
            sCommand = sCommand & ":SENS" & nChannel & ":EVEN:SLOP POS;"
        Else
            sCommand = sCommand & ":SENS" & nChannel & ":EVEN:SLOP NEG;"
        End If

        'Set Hysteresis
        sCommand = sCommand & ":SENS" & nChannel & ":EVEN:HYST " & UCase(sHysteresis) & ";"
        'Set Aperature
        'sCommand = sCommand & ":SENS" & nChannel & ":FREQ:APER " & CStr(dAperature) & ";"
        'sCommand = sCommand & ":SENS" & nChannel & ":PER:APER " & CStr(dAperature) & ";"
        'sCommand = sCommand & ":SENS" & nChannel & ":RAT:APER " & CStr(dAperature) & ";"
        'Set Range
        sCommand = sCommand & ":SENS" & nChannel & ":FREQ:RANG:AUTO OFF;"
        'Set Timebase source
        Select Case UCase(sTimeBaseSource)
            Case "VXI"
                sCommand = sCommand & ":SENS" & nChannel & ":ROSC:SOUR CLK10;"
            Case "INTERNAL"
                sCommand = sCommand & ":SENS" & nChannel & ":ROSC:SOUR INT;"
            Case "EXTERNAL"
                sCommand = sCommand & ":SENS" & nChannel & ":ROSC:SOUR EXT;"
        End Select

        If bTimeIntervalDelay Then
            sCommand = sCommand & ":SENS" & nChannel & ":TINT:DEL:STAT ON;"
            sCommand = sCommand & ":SENS" & nChannel & ":TINT:DEL:TIME " & CStr(dTimeIntervalDelay) & ";"
        Else
            sCommand = sCommand & ":SENS" & nChannel & ":TINT:DEL:STAT OFF;"
        End If

        If bTotalizeGateMeasure Then
            sCommand = sCommand & ":SENS" & nChannel & ":TOT:GATE:STAT ON;"
            If bTotalizeGatePolarityInverted Then
                sCommand = sCommand & ":SENS" & nChannel & ":TOT:GATE:POL INV;"
            Else
                sCommand = sCommand & ":SENS" & nChannel & ":TOT:GATE:POL NORM;"
            End If
        Else
            sCommand = sCommand & ":SENS" & nChannel & ":TOT:GATE:STAT OFF;"
            sCommand = sCommand & ":SENS" & nChannel & ":TOT:GATE:POL NORM;"
        End If

        'Write SENSe Subset to instrument
        If Not bSimulation Then WriteMsg(COUNTER, sCommand)
        Debug.Print(sCommand)

        'Configure ARM Options
        sCommand = ""
        Select Case UCase(sArmStartSource)
            Case "IMMEDIATE"
                sCommand = sCommand & ":ARM:STAR:SOUR IMM;"
            Case "VXI"
                sCommand = sCommand & ":ARM:STAR:SOUR BUS;"
            Case "TTL0", "TTL1", "TTL2", "TTL3", "TTL4", "TTL5", "TTL6", "TTL7"
                sCommand = sCommand & ":ARM:STAR:SOUR TTLT" & Right(sArmStartSource, 1) & ";"
                If Not bArmStartSlopNegative Then
                    sCommand = sCommand & ":ARM:STAR:SLOP POS;"
                Else
                    sCommand = sCommand & ":ARM:STAR:SLOP NEG;"
                End If
            Case "EXTERNAL"
                sCommand = sCommand & ":ARM:STAR:SOUR EXT;"
                Select Case UCase(sArmStartLevel)
                    Case "TTL"
                        sCommand = sCommand & ":ARM:STAR:LEV MAX"
                    Case "ECL"
                        sCommand = sCommand & ":ARM:STAR:LEV MIN;"
                    Case "GND"
                        sCommand = sCommand & ":ARM:STAR:LEV DEF;"
                End Select

                If Not bArmStartSlopNegative Then
                    sCommand = sCommand & ":ARM:STAR:SLOP POS;"
                Else
                    sCommand = sCommand & ":ARM:STAR:SLOP NEG;"
                End If
        End Select

        Select Case UCase(SArmStopSource)
            Case "IMMEDIATE"
                sCommand = sCommand & ":ARM:STOP:SOUR IMM;"
            Case "VXI"
                sCommand = sCommand & ":ARM:STOP:SOUR BUS;"
            Case "TTL0", "TTL1", "TTL2", "TTL3", "TTL4", "TTL5", "TTL6", "TTL7"
                sCommand = sCommand & ":ARM:STOP:SOUR TTLT" & Right(SArmStopSource, 1) & ";"
                If Not bArmStopSlopNegative Then
                    sCommand = sCommand & ":ARM:STOP:SLOP POS;"
                Else
                    sCommand = sCommand & ":ARM:STOP:SLOP NEG;"
                End If
            Case "EXTERNAL"
                sCommand = sCommand & ":ARM:STOP:SOUR EXT;"
                Select Case UCase(sArmStopLevel)
                    Case "TTL"
                        sCommand = sCommand & ":ARM:STOP:LEV MAX"
                    Case "ECL"
                        sCommand = sCommand & ":ARM:STOP:LEV MIN;"
                    Case "GND"
                        sCommand = sCommand & ":ARM:STOP:LEV DEF;"
                End Select

                If Not bArmStartSlopNegative Then
                    sCommand = sCommand & ":ARM:STOP:SLOP POS;"
                Else
                    sCommand = sCommand & ":ARM:STOP:SLOP NEG;"
                End If
        End Select

        'Write ARM Subset to instrument
        If Not bSimulation Then WriteMsg(COUNTER, sCommand)
        Debug.Print(sCommand)

        'Configure OUTPut
        sCommand = ""
        If Not bTriggerOutputOn Then
            For i = 0 To 7
                sCommand = sCommand & ":OUTP:TTLT" & i & ":STAT OFF;"
            Next i
            sCommand = sCommand & ":OUTP:ROSC:STAT OFF;"
        Else
            For i = 0 To 7
                If i = nTTLTriggerOutput Then
                    sCommand = sCommand & ":OUTP:TTLT" & i & ":STAT ON;"
                Else
                    sCommand = sCommand & ":OUTP:TTLT" & i & ":STAT OFF;"
                End If
            Next i
            sCommand = sCommand & ":OUTP:ROSC:STAT ON;"
        End If

        'Write OUTPut Subset to instrument
        If Not bSimulation Then WriteMsg(COUNTER, sCommand)
        Debug.Print(sCommand)

        'Configure CONFigure Options
        sCommand = ""
        If dRange <> -1 Then
            sCommand = ":CONF" & nChannel & ":TOT;"
            Debug.Print(sCommand)
        End If

        'Make Measurement
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        If Not bSimulation Then
            WriteMsg(COUNTER, ":INIT" & CStr(nChannel))
            Debug.Print("INIT" & CStr(nChannel))
            If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        End If
    End Sub

    Public Sub SetChannelImpedance(Optional ByRef bChOne50ohms As Boolean = False, Optional ByRef bChTwo50ohms As Boolean = False)
        'To allow the instrument to be preped prior to connection.
        'For signal drivers that could be damaged by applying a 50 ohm load
        'The counter must not be reset after this is called or the inputs
        'will default back to 50 ohm.

        'Also can be used to make the counter into a very expensive 50-ohm
        'terminator.  However, beware also of input AC/DC coupling and
        'input routing condition.

        'Set Impedance
        If Not bChOne50ohms Then 'bbbb was simulation
            If Not bSimulation Then WriteMsg(COUNTER, ":INP1:IMP 1E6;")
        Else
            If Not bSimulation Then WriteMsg(COUNTER, ":INP1:IMP 50;")
        End If

        If Not bChTwo50ohms Then 'bbbb was simulation
            If Not bSimulation Then WriteMsg(COUNTER, ":INP2:IMP 1E6;")
        Else
            If Not bSimulation Then WriteMsg(COUNTER, ":INP2:IMP 50;")
        End If
    End Sub
End Class