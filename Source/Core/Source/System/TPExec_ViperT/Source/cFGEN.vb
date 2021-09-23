Option Strict Off
Option Explicit On
<System.Runtime.InteropServices.ProgId("cFGEN_NET.cFGEN")> Public Class cFGEN

	Public Sub SendMsg(ByRef sMsg As String)
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		gFrmMain.txtInstrument.Text = "FGEN"
		gFrmMain.txtCommand.Text = "SendMsg"
		WriteMsg(FGEN, sMsg)
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON)
	End Sub
	
	Public Function sReadMsg(ByRef sMsg As String) As String

        sReadMsg = ""
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function

		gFrmMain.txtInstrument.Text = "FGEN"
		gFrmMain.txtCommand.Text = "ReadMsg"
		ReadMsg(FGEN, sReadMsg)
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON)
	End Function
	
    Public Sub Sine(ByRef dFrequency As Double, ByRef dAmplitude As Double,
                    Optional ByRef dDCOffset As Double = 0, Optional ByRef iFunctionalExponent As Short = 1,
                    Optional ByRef iPhaseShift As Short = 0, Optional ByRef sTriggerMode As String = "CONTINUOUS",
                    Optional ByRef sTriggerSource As String = "External", Optional ByRef bTrigSlopPositive As Boolean = True,
                    Optional ByRef dTrigLevel As Double = 1.6, Optional ByRef dIntTrigDelay As Double = 0.0001,
                    Optional ByRef dTrigDelay As Double = 0.0000001, Optional ByRef iBurstCount As Short = 1,
                    Optional ByRef bActiveGateLevel As Boolean = True, Optional ByRef bSyncOutputOn As Boolean = False,
                    Optional ByRef bSyncOutputWaveForm As Boolean = True, Optional ByRef iTTLTrig As Short = 0,
                    Optional ByRef bTTLMarkerOutputOn As Boolean = False, Optional ByRef sTTLMarkerSource As String = "WAVEFORM",
                    Optional ByRef dTTLMarkerSourceDelay As Double = 0.0001, Optional ByRef iECLTrig As Short = 0,
                    Optional ByRef bECLMarkerOutputOn As Boolean = False, Optional ByRef b50ohmsOutputLoadRef As Boolean = True)

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub

        gFrmMain.txtInstrument.Text = "FGEN"
        gFrmMain.txtCommand.Text = "Sine"
        'Error Checking on Arguments
        If dFrequency > 50000000.0 Or dFrequency < 0.0001 Then
            Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Sine dFrequency argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If

        If Not b50ohmsOutputLoadRef Then
            dAmplitude = dAmplitude / 2
            dDCOffset = dDCOffset / 2
        End If

        If dAmplitude > 16 Or dAmplitude < 0.01 Then
            Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Sine dAmplitude argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If

        Select Case dAmplitude
            Case 0.01 To 0.159
                If dDCOffset > (0.08 - (dAmplitude / 2)) Then
                    Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Sine dDCOffSet argument out of range.")
                    Err.Raise(-1000)
                    Exit Sub
                End If
            Case 0.16 To 1.599
                If dDCOffset > (0.799 - (dAmplitude / 2)) Then
                    Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Sine dDCOffSet argument out of range.")
                    Err.Raise(-1000)
                    Exit Sub
                End If
            Case 1.6 To 16
                If dDCOffset > (7.99 - (dAmplitude / 2)) Then
                    Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Sine dDCOffSet argument out of range.")
                    Err.Raise(-1000)
                    Exit Sub
                End If
        End Select

        If iFunctionalExponent < 1 Or iFunctionalExponent > 9 Then
            Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Sine ifunctionalexponent argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If

        If iPhaseShift < 0 Or iPhaseShift > 360 Then
            Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Sine iPhaseShift argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        Select Case Left(UCase(sTriggerMode), 1)
            Case "C" ' Continuous
            Case "T", "B" ' Trigger, Burst
                Select Case Left(UCase(sTriggerSource), 3)
                    Case "EXT", "INT"
                    Case "TTL"
                        Select Case CShort(Right(UCase(sTriggerSource), 1))
                            Case 0 To 7
                            Case Else
                                Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Sine sTriggerSource argument out of range.")
                                Err.Raise(-1000)
                                Exit Sub
                        End Select
                    Case Else
                        Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Sine sTriggerSource argument out of range.")
                        Err.Raise(-1000)
                        Exit Sub
                End Select

                If dTrigLevel < -10 Or dTrigLevel > 10 Then
                    Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Sine dTrigLevel argument out of range.")
                    Err.Raise(-1000)
                    Exit Sub
                End If

                If dIntTrigDelay < 0.000015 Or dIntTrigDelay > 1000 Then
                    Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Sine dInitTrigDelay argument out of range.")
                    Err.Raise(-1000)
                    Exit Sub
                End If

                If dTrigDelay < 0.0000001 Or dTrigDelay > 0.02 Then
                    Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Sine dTrigDelay argument out of range.")
                    Err.Raise(-1000)
                    Exit Sub
                End If
                If Left(UCase(sTriggerMode), 1) = "B" Then ' Burst
                    If iBurstCount < 1 Or iBurstCount > 1000000.0 Then
                        Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Sine iBurstCount argument out of range.")
                        Err.Raise(-1000)
                        Exit Sub
                    End If
                End If

            Case "G" ' Gate
                If dTrigLevel < -10 Or dTrigLevel > 10 Then
                    Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Sine dTrigLevel argument out of range.")
                    Err.Raise(-1000)
                    Exit Sub
                End If

            Case Else
                Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Sine sTriggerMode argument out of range.")
                Err.Raise(-1000)
                Exit Sub
        End Select

        If iTTLTrig < 0 Or iTTLTrig > 7 Then
            Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Sine iTTLTrig argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        Select Case Left(UCase(sTTLMarkerSource), 1)
            Case "W", "E" ' Bit, External
            Case "I" ' Internal
                If dTTLMarkerSourceDelay < 0.000015 Or dTTLMarkerSourceDelay > 1000 Then
                    Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Sine dTTLMarkerSourceDelay argument out of range.")
                    Err.Raise(-1000)
                    Exit Sub
                End If
            Case Else
                Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Sine sTTLMarkerSource argument out of range.")
                Err.Raise(-1000)
                Exit Sub
        End Select

        If iECLTrig < 0 Or iECLTrig > 1 Then
            Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Sine iECLTrig argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub

        If bSimulation Then Exit Sub

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        'Program Instrument
        WriteMsg(FGEN, ":FUNC:SHAP SIN")
        WriteMsg(FGEN, ":FREQ " & CStr(dFrequency))
        WriteMsg(FGEN, ":VOLT " & CStr(dAmplitude))
        WriteMsg(FGEN, ":VOLT:OFFS " & CStr(dDCOffset))
        WriteMsg(FGEN, ":SIN:POW " & CStr(iFunctionalExponent))
        WriteMsg(FGEN, ":SIN:PHAS " & CStr(iPhaseShift))
        Select Case Left(UCase(sTriggerMode), 1)
            Case "C" ' Continuous
                WriteMsg(FGEN, ":INIT:CONT ON")
            Case "T" ' Trigger
                WriteMsg(FGEN, ":INIT:CONT OFF")
                WriteMsg(FGEN, ":TRIG:BURS OFF")
                WriteMsg(FGEN, ":TRIG:GATE OFF")
                Select Case Left(UCase(sTriggerSource), 3)
                    Case "INT"
                        WriteMsg(FGEN, ":TRIG:SOUR:ADV Internal")
                        WriteMsg(FGEN, ":TRIG:TIM " & CStr(dIntTrigDelay))
                        WriteMsg(FGEN, ":TRIG:DEL " & CStr(dTrigDelay))
                    Case "EXT"
                        WriteMsg(FGEN, ":TRIG:SOUR:ADV External")
                        If bTrigSlopPositive Then
                            WriteMsg(FGEN, ":TRIG:SLOP POSITIVE")
                        Else
                            WriteMsg(FGEN, ":TRIG:SLOP NEGATIVE")
                        End If
                        WriteMsg(FGEN, ":TRIG:LEV " & CStr(dTrigLevel))
                        WriteMsg(FGEN, ":TRIG:DEL " & CStr(dTrigDelay))
                    Case "TTL"
                        WriteMsg(FGEN, ":TRIG:SOUR:ADV TTLTrg" & Right(UCase(sTriggerSource), 1))
                        WriteMsg(FGEN, ":TRIG:DEL " & CStr(dTrigDelay))
                End Select
            Case "B" ' Burst
                WriteMsg(FGEN, ":INIT:CONT OFF")
                WriteMsg(FGEN, ":TRIG:GATE OFF")
                WriteMsg(FGEN, ":TRIG:COUN " & CStr(iBurstCount))
                WriteMsg(FGEN, ":TRIG:BURS ON")
                Select Case Left(UCase(sTriggerSource), 3)
                    Case "INT"
                        WriteMsg(FGEN, ":TRIG:SOUR:ADV Internal")
                        WriteMsg(FGEN, ":TRIG:TIM " & CStr(dIntTrigDelay))
                        WriteMsg(FGEN, ":TRIG:DEL " & CStr(dTrigDelay))
                    Case "EXT"
                        WriteMsg(FGEN, ":TRIG:SOUR:ADV External")
                        If bTrigSlopPositive Then ' was BTRIGSLOPPOSITIVE
                            WriteMsg(FGEN, ":TRIG:SLOP POSITIVE")
                        Else
                            WriteMsg(FGEN, ":TRIG:SLOP NEGATIVE")
                        End If
                        WriteMsg(FGEN, ":TRIG:LEV " & CStr(dTrigLevel))
                        WriteMsg(FGEN, ":TRIG:DEL " & CStr(dTrigDelay))
                    Case "TTL"
                        WriteMsg(FGEN, ":TRIG:SOUR:ADV TTLTrg" & Right(UCase(sTriggerSource), 1))
                        WriteMsg(FGEN, ":TRIG:DEL " & CStr(dTrigDelay))
                End Select
            Case "G" ' Gate
                WriteMsg(FGEN, ":INIT:CONT OFF")
                WriteMsg(FGEN, ":TRIG:BURS OFF")
                WriteMsg(FGEN, ":TRIG:GATE ON")
                WriteMsg(FGEN, ":TRIG:SOUR:ADV External")
                If bActiveGateLevel Then
                    WriteMsg(FGEN, ":TRIG:SLOP POSITIVE")
                Else
                    WriteMsg(FGEN, ":TRIG:SLOP NEGATIVE")
                End If
                WriteMsg(FGEN, ":TRIG:LEV " & CStr(dTrigLevel))
        End Select

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        If bSyncOutputWaveForm Then
            WriteMsg(FGEN, ":OUTP:SYNC:SOUR BIT")
        Else
            WriteMsg(FGEN, ":OUTP:SYNC:SOUR HCLock")
        End If

        If bSyncOutputOn Then
            WriteMsg(FGEN, ":OUTP:SYNC ON")
        Else
            WriteMsg(FGEN, ":OUTP:SYNC OFF")
        End If

        Select Case Left(UCase(sTTLMarkerSource), 1)
            Case "W"
                WriteMsg(FGEN, ":OUTP:TRIG:SOUR BIT")
            Case "E"
                WriteMsg(FGEN, ":OUTP:TRIG:SOUR EXTernal")
            Case "I"
                WriteMsg(FGEN, ":OUTP:TRIG:SOUR INTernal")
                WriteMsg(FGEN, ":TRIG:TIM " & CStr(dTTLMarkerSourceDelay))
        End Select

        If bTTLMarkerOutputOn Then
            WriteMsg(FGEN, ":OUTP:TTLTrg" & CStr(iTTLTrig) & " ON")
        Else
            WriteMsg(FGEN, ":OUTP:TTLTrg" & CStr(iTTLTrig) & " OFF")
        End If

        If bECLMarkerOutputOn Then
            WriteMsg(FGEN, ":OUTP:ECLTrg" & CStr(iECLTrig) & " ON")
        Else
            WriteMsg(FGEN, ":OUTP:ECLTrg" & CStr(iECLTrig) & " OFF")
        End If
        'Turn on the output
        WriteMsg(FGEN, ":OUTP ON")

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
    End Sub

    Public Sub Triangle(ByRef dFrequency As Double, ByRef dAmplitude As Double,
                        Optional ByRef dDCOffset As Double = 0, Optional ByRef iFunctionalExponent As Short = 1,
                        Optional ByRef iPhaseShift As Short = 0, Optional ByRef sTriggerMode As String = "CONTINUOUS",
                        Optional ByRef sTriggerSource As String = "External", Optional ByRef bTrigSlopPositive As Boolean = True,
                        Optional ByRef dTrigLevel As Double = 1.6, Optional ByRef dIntTrigDelay As Double = 0.0001,
                        Optional ByRef dTrigDelay As Double = 0.0000001, Optional ByRef iBurstCount As Short = 1,
                        Optional ByRef bActiveGateLevel As Boolean = True, Optional ByRef bSyncOutputOn As Boolean = False,
                        Optional ByRef bSyncOutputWaveForm As Boolean = True, Optional ByRef iTTLTrig As Short = 0,
                        Optional ByRef bTTLMarkerOutputOn As Boolean = False, Optional ByRef sTTLMarkerSource As String = "WAVEFORM",
                        Optional ByRef dTTLMarkerSourceDelay As Double = 0.0001, Optional ByRef iECLTrig As Short = 0,
                        Optional ByRef bECLMarkerOutputOn As Boolean = False, Optional ByRef b50ohmsOutputLoadRef As Boolean = True,
                        Optional ByRef b20MhzLowPassFilter As Boolean = False, Optional ByRef b25MhzLowPassFilter As Boolean = False,
                        Optional ByRef b50MhzLowPassFilter As Boolean = False)

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub

        gFrmMain.txtInstrument.Text = "FGEN"
        gFrmMain.txtCommand.Text = "Triangle"
        'Error Checking on Arguments
        If dFrequency > 1000000.0 Or dFrequency < 0.0001 Then
            Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Triangle dFrequency argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If

        If Not b50ohmsOutputLoadRef Then
            dAmplitude = dAmplitude / 2
            dDCOffset = dDCOffset / 2
        End If

        If dAmplitude > 16 Or dAmplitude < 0.01 Then
            Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Triangle dAmplitude argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If

        Select Case dAmplitude
            Case 0.01 To 0.159
                If dDCOffset > (0.08 - (dAmplitude / 2)) Then
                    Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Triangle dDCOffSet argument out of range.")
                    Err.Raise(-1000)
                    Exit Sub
                End If
            Case 0.16 To 1.599
                If dDCOffset > (0.799 - (dAmplitude / 2)) Then
                    Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Triangle dDCOffSet argument out of range.")
                    Err.Raise(-1000)
                    Exit Sub
                End If
            Case 1.6 To 16
                If dDCOffset > (7.99 - (dAmplitude / 2)) Then
                    Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Triangle dDCOffSet argument out of range.")
                    Err.Raise(-1000)
                    Exit Sub
                End If
        End Select

        If iFunctionalExponent < 1 Or iFunctionalExponent > 9 Then
            Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Triangle ifunctionalexponent argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If

        If iPhaseShift < 0 Or iPhaseShift > 360 Then
            Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Triangle iPhaseShift argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        Select Case Left(UCase(sTriggerMode), 1)
            Case "C" ' Continuous
            Case "T", "B" ' Trigger, Burst
                Select Case Left(UCase(sTriggerSource), 3)
                    Case "EXT", "INT"
                    Case "TTL"
                        Select Case CShort(Right(UCase(sTriggerSource), 1))
                            Case 0 To 7
                            Case Else
                                Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Triangle sTriggerSource argument out of range.")
                                Err.Raise(-1000)
                                Exit Sub
                        End Select
                    Case Else
                        Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Triangle sTriggerSource argument out of range.")
                        Err.Raise(-1000)
                        Exit Sub
                End Select

                If dTrigLevel < -10 Or dTrigLevel > 10 Then
                    Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Triangle dTrigLevel argument out of range.")
                    Err.Raise(-1000)
                    Exit Sub
                End If

                If dIntTrigDelay < 0.000015 Or dIntTrigDelay > 1000 Then
                    Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Triangle dInitTrigDelay argument out of range.")
                    Err.Raise(-1000)
                    Exit Sub
                End If

                If dTrigDelay < 0.0000001 Or dTrigDelay > 0.02 Then
                    Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Triangle dTrigDelay argument out of range.")
                    Err.Raise(-1000)
                    Exit Sub
                End If
                If Left(UCase(sTriggerMode), 1) = "B" Then ' Burst
                    If iBurstCount < 1 Or iBurstCount > 1000000.0 Then
                        Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Triangle iBurstCount argument out of range.")
                        Err.Raise(-1000)
                        Exit Sub
                    End If
                End If

            Case "G" ' Gate
                If dTrigLevel < -10 Or dTrigLevel > 10 Then
                    Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Triangle dTrigLevel argument out of range.")
                    Err.Raise(-1000)
                    Exit Sub
                End If

            Case Else
                Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Triangle sTriggerMode argument out of range.")
                Err.Raise(-1000)
                Exit Sub
        End Select

        If iTTLTrig < 0 Or iTTLTrig > 7 Then
            Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Triangle iTTLTrig argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        Select Case Left(UCase(sTTLMarkerSource), 1)
            Case "W", "E" ' Bit, External
            Case "I" ' Internal
                If dTTLMarkerSourceDelay < 0.000015 Or dTTLMarkerSourceDelay > 1000 Then
                    Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Triangle dTTLMarkerSourceDelay argument out of range.")
                    Err.Raise(-1000)
                    Exit Sub
                End If
            Case Else
                Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Triangle sTTLMarkerSource argument out of range.")
                Err.Raise(-1000)
                Exit Sub
        End Select

        If iECLTrig < 0 Or iECLTrig > 1 Then
            Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Triangle iECLTrig argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub

        If bSimulation Then Exit Sub

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        'Program Instrument
        WriteMsg(FGEN, ":FUNC:SHAP TRI")
        WriteMsg(FGEN, ":FREQ " & CStr(dFrequency))
        WriteMsg(FGEN, ":VOLT " & CStr(dAmplitude))
        WriteMsg(FGEN, ":VOLT:OFFS " & CStr(dDCOffset))
        WriteMsg(FGEN, ":TRI:POW " & CStr(iFunctionalExponent))
        WriteMsg(FGEN, ":TRI:PHAS " & CStr(iPhaseShift))
        Select Case Left(UCase(sTriggerMode), 1)
            Case "C" ' Continuous
                WriteMsg(FGEN, ":INIT:CONT ON")
            Case "T" ' Trigger
                WriteMsg(FGEN, ":INIT:CONT OFF")
                WriteMsg(FGEN, ":TRIG:BURS OFF")
                WriteMsg(FGEN, ":TRIG:GATE OFF")
                Select Case Left(UCase(sTriggerSource), 3)
                    Case "INT"
                        WriteMsg(FGEN, ":TRIG:SOUR:ADV Internal")
                        WriteMsg(FGEN, ":TRIG:TIM " & CStr(dIntTrigDelay))
                        WriteMsg(FGEN, ":TRIG:DEL " & CStr(dTrigDelay))
                    Case "EXT"
                        WriteMsg(FGEN, ":TRIG:SOUR:ADV External")
                        If bTrigSlopPositive Then
                            WriteMsg(FGEN, ":TRIG:SLOP POSITIVE")
                        Else
                            WriteMsg(FGEN, ":TRIG:SLOP NEGATIVE")
                        End If
                        WriteMsg(FGEN, ":TRIG:LEV " & CStr(dTrigLevel))
                        WriteMsg(FGEN, ":TRIG:DEL " & CStr(dTrigDelay))
                    Case "TTL"
                        WriteMsg(FGEN, ":TRIG:SOUR:ADV TTLTrg" & Right(UCase(sTriggerSource), 1))
                        WriteMsg(FGEN, ":TRIG:DEL " & CStr(dTrigDelay))
                End Select
            Case "B" ' Burst
                WriteMsg(FGEN, ":INIT:CONT OFF")
                WriteMsg(FGEN, ":TRIG:GATE OFF")
                WriteMsg(FGEN, ":TRIG:COUN " & CStr(iBurstCount))
                WriteMsg(FGEN, ":TRIG:BURS ON")
                Select Case Left(UCase(sTriggerSource), 3)
                    Case "INT"
                        WriteMsg(FGEN, ":TRIG:SOUR:ADV Internal")
                        WriteMsg(FGEN, ":TRIG:TIM " & CStr(dIntTrigDelay))
                        WriteMsg(FGEN, ":TRIG:DEL " & CStr(dTrigDelay))
                    Case "EXT"
                        WriteMsg(FGEN, ":TRIG:SOUR:ADV External")
                        If bTrigSlopPositive Then ' was BTRIGSLOPPOSITIVE
                            WriteMsg(FGEN, ":TRIG:SLOP POSITIVE")
                        Else
                            WriteMsg(FGEN, ":TRIG:SLOP NEGATIVE")
                        End If
                        WriteMsg(FGEN, ":TRIG:LEV " & CStr(dTrigLevel))
                        WriteMsg(FGEN, ":TRIG:DEL " & CStr(dTrigDelay))
                    Case "TTL"
                        WriteMsg(FGEN, ":TRIG:SOUR:ADV TTLTrg" & Right(UCase(sTriggerSource), 1))
                        WriteMsg(FGEN, ":TRIG:DEL " & CStr(dTrigDelay))
                End Select
            Case "G" ' Gate
                WriteMsg(FGEN, ":INIT:CONT OFF")
                WriteMsg(FGEN, ":TRIG:BURS OFF")
                WriteMsg(FGEN, ":TRIG:GATE ON")
                WriteMsg(FGEN, ":TRIG:SOUR:ADV External")
                If bActiveGateLevel Then
                    WriteMsg(FGEN, ":TRIG:SLOP POSITIVE")
                Else
                    WriteMsg(FGEN, ":TRIG:SLOP NEGATIVE")
                End If
                WriteMsg(FGEN, ":TRIG:LEV " & CStr(dTrigLevel))
        End Select

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        If bSyncOutputWaveForm Then
            WriteMsg(FGEN, ":OUTP:SYNC:SOUR BIT")
        Else
            WriteMsg(FGEN, ":OUTP:SYNC:SOUR HCLock")
        End If

        If bSyncOutputOn Then
            WriteMsg(FGEN, ":OUTP:SYNC ON")
        Else
            WriteMsg(FGEN, ":OUTP:SYNC OFF")
        End If

        Select Case Left(UCase(sTTLMarkerSource), 1)
            Case "W"
                WriteMsg(FGEN, ":OUTP:TRIG:SOUR BIT")
            Case "E"
                WriteMsg(FGEN, ":OUTP:TRIG:SOUR EXTernal")
            Case "I"
                WriteMsg(FGEN, ":OUTP:TRIG:SOUR INTernal")
                WriteMsg(FGEN, ":TRIG:TIM " & CStr(dTTLMarkerSourceDelay))
        End Select

        If bTTLMarkerOutputOn Then
            WriteMsg(FGEN, ":OUTP:TTLTrg" & CStr(iTTLTrig) & " ON")
        Else
            WriteMsg(FGEN, ":OUTP:TTLTrg" & CStr(iTTLTrig) & " OFF")
        End If

        If bECLMarkerOutputOn Then
            WriteMsg(FGEN, ":OUTP:ECLTrg" & CStr(iECLTrig) & " ON")
        Else
            WriteMsg(FGEN, ":OUTP:ECLTrg" & CStr(iECLTrig) & " OFF")
        End If

        If b20MhzLowPassFilter Then
            WriteMsg(FGEN, ":OUTP:FILT:FREQ 20MHz")
            WriteMsg(FGEN, ":OUTP:FILT ON")
        End If

        If b25MhzLowPassFilter Then
            WriteMsg(FGEN, ":OUTP:FILT:FREQ 25MHz")
            WriteMsg(FGEN, ":OUTP:FILT ON")
        End If

        If b50MhzLowPassFilter Then
            WriteMsg(FGEN, ":OUTP:FILT:FREQ 25MHz")
            WriteMsg(FGEN, ":OUTP:FILT ON")
        End If

        If b20MhzLowPassFilter Or b25MhzLowPassFilter Or b50MhzLowPassFilter Then
            WriteMsg(FGEN, ":OUTP:FILT ON")
        Else
            WriteMsg(FGEN, ":OUTP:FILT OFF")
        End If

        'Turn on the output
        WriteMsg(FGEN, ":OUTP ON")

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
    End Sub
	
	Public Sub DC(ByRef dAmplitude As Double, Optional ByRef b50ohmsOutputLoadRef As Boolean = False)
		
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub

		gFrmMain.txtInstrument.Text = "FGEN"
		gFrmMain.txtCommand.Text = "DC"

        'Error Checking on Arguments
        If b50ohmsOutputLoadRef Then
            dAmplitude = dAmplitude * 2
        End If
		
		If dAmplitude < -16 Or dAmplitude > 16 Then
			Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Square dAmplitude argument out of range.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
        'Exit Sub if simulation
		If bSimulation Then Exit Sub
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		'Program Instrument
		WriteMsg(FGEN, ":FUNC:SHAP DC")
		If dAmplitude = 0 Then
			WriteMsg(FGEN, ":DC 0")
		ElseIf dAmplitude < 0 Then 
			WriteMsg(FGEN, ":DC -100")
		Else
			WriteMsg(FGEN, ":DC 100")
		End If
		WriteMsg(FGEN, ":VOLT " & CStr(dAmplitude))
		
		'Turn on the output
		WriteMsg(FGEN, ":OUTP ON")
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
    End Sub

    Public Sub Square(ByRef dFrequency As Double, ByRef dAmplitude As Double,
                      Optional ByRef dDCOffset As Double = 0, Optional ByRef iDutyCycle As Short = 50,
                      Optional ByRef sTriggerMode As String = "CONTINUOUS", Optional ByRef sTriggerSource As String = "External",
                      Optional ByRef bTrigSlopPositive As Boolean = True, Optional ByRef dTrigLevel As Double = 1.6,
                      Optional ByRef dIntTrigDelay As Double = 0.0001, Optional ByRef dTrigDelay As Double = 0.0000001,
                      Optional ByRef iBurstCount As Short = 1, Optional ByRef bActiveGateLevel As Boolean = True,
                      Optional ByRef bSyncOutputOn As Boolean = False, Optional ByRef bSyncOutputWaveForm As Boolean = True,
                      Optional ByRef iTTLTrig As Short = 0, Optional ByRef bTTLMarkerOutputOn As Boolean = False,
                      Optional ByRef sTTLMarkerSource As String = "WAVEFORM", Optional ByRef dTTLMarkerSourceDelay As Double = 0.0001,
                      Optional ByRef iECLTrig As Short = 0, Optional ByRef bECLMarkerOutputOn As Boolean = False,
                      Optional ByRef b50ohmsOutputLoadRef As Boolean = True, Optional ByRef b20MhzLowPassFilter As Boolean = False,
                      Optional ByRef b25MhzLowPassFilter As Boolean = False, Optional ByRef b50MhzLowPassFilter As Boolean = False)

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub

        gFrmMain.txtInstrument.Text = "FGEN"
        gFrmMain.txtCommand.Text = "Square"

        'Error Checking on Arguments
        If dFrequency > 50000000.0 Or dFrequency < 0.0001 Then
            Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Square dFrequency argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If

        If Not b50ohmsOutputLoadRef Then
            dAmplitude = dAmplitude / 2
            dDCOffset = dDCOffset / 2
        End If

        If dAmplitude > 16 Or dAmplitude < 0.01 Then
            Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Square dAmplitude argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If

        Select Case dAmplitude
            Case 0.01 To 0.159
                If dDCOffset > (0.08 - (dAmplitude / 2)) Then
                    Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Square dDCOffSet argument out of range.")
                    Err.Raise(-1000)
                    Exit Sub
                End If
            Case 0.16 To 1.599
                If dDCOffset > (0.799 - (dAmplitude / 2)) Then
                    Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Square dDCOffSet argument out of range.")
                    Err.Raise(-1000)
                    Exit Sub
                End If
            Case 1.6 To 16
                If dDCOffset > (7.99 - (dAmplitude / 2)) Then
                    Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Square dDCOffSet argument out of range.")
                    Err.Raise(-1000)
                    Exit Sub
                End If
        End Select

        If iDutyCycle < 1 Or iDutyCycle > 99 Then
            Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Square iDutyCycle argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        Select Case Left(UCase(sTriggerMode), 1)
            Case "C" ' Continuous
            Case "T", "B" ' Trigger, Burst
                Select Case Left(UCase(sTriggerSource), 3)
                    Case "EXT", "INT"
                    Case "TTL"
                        Select Case CShort(Right(UCase(sTriggerSource), 1))
                            Case 0 To 7
                            Case Else
                                Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Square sTriggerSource argument out of range.")
                                Err.Raise(-1000)
                                Exit Sub
                        End Select
                    Case Else
                        Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Square sTriggerSource argument out of range.")
                        Err.Raise(-1000)
                        Exit Sub
                End Select

                If dTrigLevel < -10 Or dTrigLevel > 10 Then
                    Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Square dTrigLevel argument out of range.")
                    Err.Raise(-1000)
                    Exit Sub
                End If

                If dIntTrigDelay < 0.000015 Or dIntTrigDelay > 1000 Then
                    Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Square dInitTrigDelay argument out of range.")
                    Err.Raise(-1000)
                    Exit Sub
                End If

                If dTrigDelay < 0.0000001 Or dTrigDelay > 0.02 Then
                    Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Square dTrigDelay argument out of range.")
                    Err.Raise(-1000)
                    Exit Sub
                End If
                If Left(UCase(sTriggerMode), 1) = "B" Then ' Burst
                    If iBurstCount < 1 Or iBurstCount > 1000000.0 Then
                        Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Square iBurstCount argument out of range.")
                        Err.Raise(-1000)
                        Exit Sub
                    End If
                End If

            Case "G" ' Gate
                If dTrigLevel < -10 Or dTrigLevel > 10 Then
                    Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Square dTrigLevel argument out of range.")
                    Err.Raise(-1000)
                    Exit Sub
                End If

            Case Else
                Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Square sTriggerMode argument out of range.")
                Err.Raise(-1000)
                Exit Sub
        End Select

        If iTTLTrig < 0 Or iTTLTrig > 7 Then
            Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Square iTTLTrig argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        Select Case Left(UCase(sTTLMarkerSource), 1)
            Case "W", "E" ' Bit, External
            Case "I" ' Internal
                If dTTLMarkerSourceDelay < 0.000015 Or dTTLMarkerSourceDelay > 1000 Then
                    Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Square dTTLMarkerSourceDelay argument out of range.")
                    Err.Raise(-1000)
                    Exit Sub
                End If
            Case Else
                Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Square sTTLMarkerSource argument out of range.")
                Err.Raise(-1000)
                Exit Sub
        End Select

        If iECLTrig < 0 Or iECLTrig > 1 Then
            Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Square iECLTrig argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If

        If (b20MhzLowPassFilter And b25MhzLowPassFilter) Or (b20MhzLowPassFilter And b50MhzLowPassFilter) Or (b25MhzLowPassFilter And b50MhzLowPassFilter) Then
            Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Square multiple LowPassFilters selected.")
            Err.Raise(-1000)
            Exit Sub
        End If

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub

        'Exit Sub if simulation
        If bSimulation Then Exit Sub

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        'Program Instrument
        WriteMsg(FGEN, ":FUNC:SHAP SQU")
        WriteMsg(FGEN, ":FREQ " & CStr(dFrequency))
        WriteMsg(FGEN, ":VOLT " & CStr(dAmplitude))
        WriteMsg(FGEN, ":VOLT:OFFS " & CStr(dDCOffset))
        WriteMsg(FGEN, ":SQU:DCYC " & CStr(iDutyCycle))
        Select Case Left(UCase(sTriggerMode), 1)
            Case "C" ' Continuous
                WriteMsg(FGEN, ":INIT:CONT ON")
            Case "T" ' Trigger
                WriteMsg(FGEN, ":INIT:CONT OFF")
                WriteMsg(FGEN, ":TRIG:BURS OFF")
                WriteMsg(FGEN, ":TRIG:GATE OFF")
                Select Case Left(UCase(sTriggerSource), 3)
                    Case "INT"
                        WriteMsg(FGEN, ":TRIG:SOUR:ADV Internal")
                        WriteMsg(FGEN, ":TRIG:TIM " & CStr(dIntTrigDelay))
                        WriteMsg(FGEN, ":TRIG:DEL " & CStr(dTrigDelay))
                    Case "EXT"
                        WriteMsg(FGEN, ":TRIG:SOUR:ADV External")
                        If bTrigSlopPositive Then
                            WriteMsg(FGEN, ":TRIG:SLOP POSITIVE")
                        Else
                            WriteMsg(FGEN, ":TRIG:SLOP NEGATIVE")
                        End If
                        WriteMsg(FGEN, ":TRIG:LEV " & CStr(dTrigLevel))
                        WriteMsg(FGEN, ":TRIG:DEL " & CStr(dTrigDelay))
                    Case "TTL"
                        WriteMsg(FGEN, ":TRIG:SOUR:ADV TTLTrg" & Right(UCase(sTriggerSource), 1))
                        WriteMsg(FGEN, ":TRIG:DEL " & CStr(dTrigDelay))
                End Select
            Case "B" ' Burst
                WriteMsg(FGEN, ":INIT:CONT OFF")
                WriteMsg(FGEN, ":TRIG:GATE OFF")
                WriteMsg(FGEN, ":TRIG:COUN " & CStr(iBurstCount))
                WriteMsg(FGEN, ":TRIG:BURS ON")
                Select Case Left(UCase(sTriggerSource), 3)
                    Case "INT"
                        WriteMsg(FGEN, ":TRIG:SOUR:ADV Internal")
                        WriteMsg(FGEN, ":TRIG:TIM " & CStr(dIntTrigDelay))
                        WriteMsg(FGEN, ":TRIG:DEL " & CStr(dTrigDelay))
                    Case "EXT"
                        WriteMsg(FGEN, ":TRIG:SOUR:ADV External")
                        If bTrigSlopPositive Then ' was BTRIGLSLOPPOSITIVE
                            WriteMsg(FGEN, ":TRIG:SLOP POSITIVE")
                        Else
                            WriteMsg(FGEN, ":TRIG:SLOP NEGATIVE")
                        End If
                        WriteMsg(FGEN, ":TRIG:LEV " & CStr(dTrigLevel))
                        WriteMsg(FGEN, ":TRIG:DEL " & CStr(dTrigDelay))
                    Case "TTL"
                        WriteMsg(FGEN, ":TRIG:SOUR:ADV TTLTrg" & Right(UCase(sTriggerSource), 1))
                        WriteMsg(FGEN, ":TRIG:DEL " & CStr(dTrigDelay))
                End Select
            Case "G" ' Gate
                WriteMsg(FGEN, ":INIT:CONT OFF")
                WriteMsg(FGEN, ":TRIG:BURS OFF")
                WriteMsg(FGEN, ":TRIG:GATE ON")
                WriteMsg(FGEN, ":TRIG:SOUR:ADV External")
                If bActiveGateLevel Then
                    WriteMsg(FGEN, ":TRIG:SLOP POSITIVE")
                Else
                    WriteMsg(FGEN, ":TRIG:SLOP NEGATIVE")
                End If
                WriteMsg(FGEN, ":TRIG:LEV " & CStr(dTrigLevel))
        End Select

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        If bSyncOutputWaveForm Then
            WriteMsg(FGEN, ":OUTP:SYNC:SOUR BIT")
        Else
            WriteMsg(FGEN, ":OUTP:SYNC:SOUR HCLock")
        End If

        If bSyncOutputOn Then
            WriteMsg(FGEN, ":OUTP:SYNC ON")
        Else
            WriteMsg(FGEN, ":OUTP:SYNC OFF")
        End If

        Select Case Left(UCase(sTTLMarkerSource), 1)
            Case "W"
                WriteMsg(FGEN, ":OUTP:TRIG:SOUR BIT")
            Case "E"
                WriteMsg(FGEN, ":OUTP:TRIG:SOUR EXTernal")
            Case "I"
                WriteMsg(FGEN, ":OUTP:TRIG:SOUR INTernal")
                WriteMsg(FGEN, ":TRIG:TIM " & CStr(dTTLMarkerSourceDelay))
        End Select

        If bTTLMarkerOutputOn Then
            WriteMsg(FGEN, ":OUTP:TTLTrg" & CStr(iTTLTrig) & " ON")
        Else
            WriteMsg(FGEN, ":OUTP:TTLTrg" & CStr(iTTLTrig) & " OFF")
        End If

        If bECLMarkerOutputOn Then
            WriteMsg(FGEN, ":OUTP:ECLTrg" & CStr(iECLTrig) & " ON")
        Else
            WriteMsg(FGEN, ":OUTP:ECLTrg" & CStr(iECLTrig) & " OFF")
        End If

        If b20MhzLowPassFilter Then
            WriteMsg(FGEN, ":OUTP:FILT:FREQ 20MHz")
            WriteMsg(FGEN, ":OUTP:FILT ON")
        End If

        If b25MhzLowPassFilter Then
            WriteMsg(FGEN, ":OUTP:FILT:FREQ 25MHz")
            WriteMsg(FGEN, ":OUTP:FILT ON")
        End If

        If b50MhzLowPassFilter Then
            WriteMsg(FGEN, ":OUTP:FILT:FREQ 25MHz")
            WriteMsg(FGEN, ":OUTP:FILT ON")
        End If

        If b20MhzLowPassFilter Or b25MhzLowPassFilter Or b50MhzLowPassFilter Then
            WriteMsg(FGEN, ":OUTP:FILT ON")
        Else
            WriteMsg(FGEN, ":OUTP:FILT OFF")
        End If

        'Turn on the output
        WriteMsg(FGEN, ":OUTP ON")

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
    End Sub
	
    Public Sub Sinc(ByRef dFrequency As Double, ByRef dAmplitude As Double, Optional ByRef dDCOffset As Double = 0,
                    Optional ByRef iSinc As Short = 10, Optional ByRef sTriggerMode As String = "CONTINUOUS",
                    Optional ByRef sTriggerSource As String = "External", Optional ByRef bTrigSlopPositive As Boolean = True,
                    Optional ByRef dTrigLevel As Double = 1.6, Optional ByRef dIntTrigDelay As Double = 0.0001,
                    Optional ByRef dTrigDelay As Double = 0.0000001, Optional ByRef iBurstCount As Short = 1,
                    Optional ByRef bActiveGateLevel As Boolean = True, Optional ByRef bSyncOutputOn As Boolean = False,
                    Optional ByRef bSyncOutputWaveForm As Boolean = True, Optional ByRef iTTLTrig As Short = 0,
                    Optional ByRef bTTLMarkerOutputOn As Boolean = False, Optional ByRef sTTLMarkerSource As String = "WAVEFORM",
                    Optional ByRef dTTLMarkerSourceDelay As Double = 0.0001, Optional ByRef iECLTrig As Short = 0,
                    Optional ByRef bECLMarkerOutputOn As Boolean = False, Optional ByRef b50ohmsOutputLoadRef As Boolean = True,
                    Optional ByRef b20MhzLowPassFilter As Boolean = False, Optional ByRef b25MhzLowPassFilter As Boolean = False,
                    Optional ByRef b50MhzLowPassFilter As Boolean = False)

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub

        gFrmMain.txtInstrument.Text = "FGEN"
        gFrmMain.txtCommand.Text = "Square"

        'Error Checking on Arguments
        If dFrequency > 1000000.0 Or dFrequency < 0.0001 Then
            Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Square dFrequency argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If

        If Not b50ohmsOutputLoadRef Then
            dAmplitude = dAmplitude / 2
            dDCOffset = dDCOffset / 2
        End If

        If dAmplitude > 16 Or dAmplitude < 0.01 Then
            Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Square dAmplitude argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If

        Select Case dAmplitude
            Case 0.01 To 0.159
                If dDCOffset > (0.08 - (dAmplitude / 2)) Then
                    Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Square dDCOffSet argument out of range.")
                    Err.Raise(-1000)
                    Exit Sub
                End If
            Case 0.16 To 1.599
                If dDCOffset > (0.799 - (dAmplitude / 2)) Then
                    Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Square dDCOffSet argument out of range.")
                    Err.Raise(-1000)
                    Exit Sub
                End If
            Case 1.6 To 16
                If dDCOffset > (7.99 - (dAmplitude / 2)) Then
                    Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Square dDCOffSet argument out of range.")
                    Err.Raise(-1000)
                    Exit Sub
                End If
        End Select

        If iSinc < 4 Or iSinc > 100 Then
            Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Square iSinc argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        Select Case Left(UCase(sTriggerMode), 1)
            Case "C" ' Continuous
            Case "T", "B" ' Trigger,  Burst
                Select Case Left(UCase(sTriggerSource), 3)
                    Case "EXT", "INT"
                    Case "TTL"
                        Select Case CShort(Right(UCase(sTriggerSource), 1))
                            Case 0 To 7
                            Case Else
                                Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Square sTriggerSource argument out of range.")
                                Err.Raise(-1000)
                                Exit Sub
                        End Select
                    Case Else
                        Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Square sTriggerSource argument out of range.")
                        Err.Raise(-1000)
                        Exit Sub
                End Select

                If dTrigLevel < -10 Or dTrigLevel > 10 Then
                    Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Square dTrigLevel argument out of range.")
                    Err.Raise(-1000)
                    Exit Sub
                End If

                If dIntTrigDelay < 0.000015 Or dIntTrigDelay > 1000 Then
                    Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Square dInitTrigDelay argument out of range.")
                    Err.Raise(-1000)
                    Exit Sub
                End If

                If dTrigDelay < 0.0000001 Or dTrigDelay > 0.02 Then
                    Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Square dTrigDelay argument out of range.")
                    Err.Raise(-1000)
                    Exit Sub
                End If
                If Left(UCase(sTriggerMode), 1) = "B" Then ' Burst
                    If iBurstCount < 1 Or iBurstCount > 1000000.0 Then
                        Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Square iBurstCount argument out of range.")
                        Err.Raise(-1000)
                        Exit Sub
                    End If
                End If

            Case "G" ' Gate
                If dTrigLevel < -10 Or dTrigLevel > 10 Then
                    Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Square dTrigLevel argument out of range.")
                    Err.Raise(-1000)
                    Exit Sub
                End If

            Case Else
                Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Square sTriggerMode argument out of range.")
                Err.Raise(-1000)
                Exit Sub
        End Select

        If iTTLTrig < 0 Or iTTLTrig > 7 Then
            Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Square iTTLTrig argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        Select Case Left(UCase(sTTLMarkerSource), 1)
            Case "W", "E" ' Bit, External
            Case "I" ' Internal
                If dTTLMarkerSourceDelay < 0.000015 Or dTTLMarkerSourceDelay > 1000 Then
                    Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Square dTTLMarkerSourceDelay argument out of range.")
                    Err.Raise(-1000)
                    Exit Sub
                End If
            Case Else
                Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Square sTTLMarkerSource argument out of range.")
                Err.Raise(-1000)
                Exit Sub
        End Select

        If iECLTrig < 0 Or iECLTrig > 1 Then
            Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Square iECLTrig argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If

        If (b20MhzLowPassFilter And b25MhzLowPassFilter) Or (b20MhzLowPassFilter And b50MhzLowPassFilter) Or (b25MhzLowPassFilter And b50MhzLowPassFilter) Then
            Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Square multiple LowPassFilters selected.")
            Err.Raise(-1000)
            Exit Sub
        End If


        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub

        'Exit Sub if simulation
        If bSimulation Then Exit Sub

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        'Program Instrument
        WriteMsg(FGEN, ":FUNC:SHAP SINC")
        WriteMsg(FGEN, ":FREQ " & CStr(dFrequency))
        WriteMsg(FGEN, ":VOLT " & CStr(dAmplitude))
        WriteMsg(FGEN, ":VOLT:OFFS " & CStr(dDCOffset))
        WriteMsg(FGEN, ":SINC:NCYC " & CStr(iSinc))
        Select Case Left(UCase(sTriggerMode), 1)
            Case "C" ' Continuous
                WriteMsg(FGEN, ":INIT:CONT ON")
            Case "T" ' Trigger
                WriteMsg(FGEN, ":INIT:CONT OFF")
                WriteMsg(FGEN, ":TRIG:BURS OFF")
                WriteMsg(FGEN, ":TRIG:GATE OFF")
                Select Case Left(UCase(sTriggerSource), 3)
                    Case "INT"
                        WriteMsg(FGEN, ":TRIG:SOUR:ADV Internal")
                        WriteMsg(FGEN, ":TRIG:TIM " & CStr(dIntTrigDelay))
                        WriteMsg(FGEN, ":TRIG:DEL " & CStr(dTrigDelay))
                    Case "EXT"
                        WriteMsg(FGEN, ":TRIG:SOUR:ADV External")
                        If bTrigSlopPositive Then
                            WriteMsg(FGEN, ":TRIG:SLOP POSITIVE")
                        Else
                            WriteMsg(FGEN, ":TRIG:SLOP NEGATIVE")
                        End If
                        WriteMsg(FGEN, ":TRIG:LEV " & CStr(dTrigLevel))
                        WriteMsg(FGEN, ":TRIG:DEL " & CStr(dTrigDelay))
                    Case "TTL"
                        WriteMsg(FGEN, ":TRIG:SOUR:ADV TTLTrg" & Right(UCase(sTriggerSource), 1))
                        WriteMsg(FGEN, ":TRIG:DEL " & CStr(dTrigDelay))
                End Select
            Case "B" ' Burst
                WriteMsg(FGEN, ":INIT:CONT OFF")
                WriteMsg(FGEN, ":TRIG:GATE OFF")
                WriteMsg(FGEN, ":TRIG:COUN " & CStr(iBurstCount))
                WriteMsg(FGEN, ":TRIG:BURS ON")
                Select Case Left(UCase(sTriggerSource), 3)
                    Case "INT"
                        WriteMsg(FGEN, ":TRIG:SOUR:ADV Internal")
                        WriteMsg(FGEN, ":TRIG:TIM " & CStr(dIntTrigDelay))
                        WriteMsg(FGEN, ":TRIG:DEL " & CStr(dTrigDelay))
                    Case "EXT"
                        WriteMsg(FGEN, ":TRIG:SOUR:ADV External")
                        If bTrigSlopPositive Then ' was BTRIGLSLOPPOSITIVE
                            WriteMsg(FGEN, ":TRIG:SLOP POSITIVE")
                        Else
                            WriteMsg(FGEN, ":TRIG:SLOP NEGATIVE")
                        End If
                        WriteMsg(FGEN, ":TRIG:LEV " & CStr(dTrigLevel))
                        WriteMsg(FGEN, ":TRIG:DEL " & CStr(dTrigDelay))
                    Case "TTL"
                        WriteMsg(FGEN, ":TRIG:SOUR:ADV TTLTrg" & Right(UCase(sTriggerSource), 1))
                        WriteMsg(FGEN, ":TRIG:DEL " & CStr(dTrigDelay))
                End Select
            Case "G" ' Gate
                WriteMsg(FGEN, ":INIT:CONT OFF")
                WriteMsg(FGEN, ":TRIG:BURS OFF")
                WriteMsg(FGEN, ":TRIG:GATE ON")
                WriteMsg(FGEN, ":TRIG:SOUR:ADV External")
                If bActiveGateLevel Then
                    WriteMsg(FGEN, ":TRIG:SLOP POSITIVE")
                Else
                    WriteMsg(FGEN, ":TRIG:SLOP NEGATIVE")
                End If
                WriteMsg(FGEN, ":TRIG:LEV " & CStr(dTrigLevel))
        End Select

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        If bSyncOutputWaveForm Then
            WriteMsg(FGEN, ":OUTP:SYNC:SOUR BIT")
        Else
            WriteMsg(FGEN, ":OUTP:SYNC:SOUR HCLock")
        End If

        If bSyncOutputOn Then
            WriteMsg(FGEN, ":OUTP:SYNC ON")
        Else
            WriteMsg(FGEN, ":OUTP:SYNC OFF")
        End If

        Select Case Left(UCase(sTTLMarkerSource), 1)
            Case "W" ' Bit
                WriteMsg(FGEN, ":OUTP:TRIG:SOUR BIT")
            Case "E" ' External
                WriteMsg(FGEN, ":OUTP:TRIG:SOUR EXTernal")
            Case "I" ' Internal
                WriteMsg(FGEN, ":OUTP:TRIG:SOUR INTernal")
                WriteMsg(FGEN, ":TRIG:TIM " & CStr(dTTLMarkerSourceDelay))
        End Select

        If bTTLMarkerOutputOn Then
            WriteMsg(FGEN, ":OUTP:TTLTrg" & CStr(iTTLTrig) & " ON")
        Else
            WriteMsg(FGEN, ":OUTP:TTLTrg" & CStr(iTTLTrig) & " OFF")
        End If

        If bECLMarkerOutputOn Then
            WriteMsg(FGEN, ":OUTP:ECLTrg" & CStr(iECLTrig) & " ON")
        Else
            WriteMsg(FGEN, ":OUTP:ECLTrg" & CStr(iECLTrig) & " OFF")
        End If

        If b20MhzLowPassFilter Then
            WriteMsg(FGEN, ":OUTP:FILT:FREQ 20MHz")
            WriteMsg(FGEN, ":OUTP:FILT ON")
        End If

        If b25MhzLowPassFilter Then
            WriteMsg(FGEN, ":OUTP:FILT:FREQ 25MHz")
            WriteMsg(FGEN, ":OUTP:FILT ON")
        End If

        If b50MhzLowPassFilter Then
            WriteMsg(FGEN, ":OUTP:FILT:FREQ 25MHz")
            WriteMsg(FGEN, ":OUTP:FILT ON")
        End If

        If b20MhzLowPassFilter Or b25MhzLowPassFilter Or b50MhzLowPassFilter Then
            WriteMsg(FGEN, ":OUTP:FILT ON")
        Else
            WriteMsg(FGEN, ":OUTP:FILT OFF")
        End If

        'Turn on the output
        WriteMsg(FGEN, ":OUTP ON")

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
    End Sub
	
    Public Sub Ramp(ByRef dFrequency As Double, ByRef dAmplitude As Double, Optional ByRef dDCOffset As Double = 0,
                    Optional ByRef dDelay As Double = 10, Optional ByRef dLeadingEdge As Double = 10,
                    Optional ByRef dTrailingEdge As Double = 10, Optional ByRef sTriggerMode As String = "CONTINUOUS",
                    Optional ByRef sTriggerSource As String = "External", Optional ByRef bTrigSlopPositive As Boolean = True,
                    Optional ByRef dTrigLevel As Double = 1.6, Optional ByRef dIntTrigDelay As Double = 0.0001,
                    Optional ByRef dTrigDelay As Double = 0.0000001, Optional ByRef iBurstCount As Short = 1,
                    Optional ByRef bActiveGateLevel As Boolean = True, Optional ByRef bSyncOutputOn As Boolean = False,
                    Optional ByRef bSyncOutputWaveForm As Boolean = True, Optional ByRef iTTLTrig As Short = 0,
                    Optional ByRef bTTLMarkerOutputOn As Boolean = False, Optional ByRef sTTLMarkerSource As String = "WAVEFORM",
                    Optional ByRef dTTLMarkerSourceDelay As Double = 0.0001, Optional ByRef iECLTrig As Short = 0,
                    Optional ByRef bECLMarkerOutputOn As Boolean = False, Optional ByRef b50ohmsOutputLoadRef As Boolean = True,
                    Optional ByRef b20MhzLowPassFilter As Boolean = False, Optional ByRef b25MhzLowPassFilter As Boolean = False,
                    Optional ByRef b50MhzLowPassFilter As Boolean = False)

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub

        gFrmMain.txtInstrument.Text = "FGEN"
        gFrmMain.txtCommand.Text = "Ramp"

        'Error Checking on Arguments
        If dFrequency > 1000000.0 Or dFrequency < 0.0001 Then
            Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Ramp dFrequency argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If

        If Not b50ohmsOutputLoadRef Then
            dAmplitude = dAmplitude / 2
            dDCOffset = dDCOffset / 2
        End If

        If dAmplitude > 16 Or dAmplitude < 0.01 Then
            Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Ramp dAmplitude argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If

        Select Case dAmplitude
            Case 0.01 To 0.159
                If dDCOffset > (0.08 - (dAmplitude / 2)) Then
                    Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Ramp dDCOffSet argument out of range.")
                    Err.Raise(-1000)
                    Exit Sub
                End If
            Case 0.16 To 1.599
                If dDCOffset > (0.799 - (dAmplitude / 2)) Then
                    Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Ramp dDCOffSet argument out of range.")
                    Err.Raise(-1000)
                    Exit Sub
                End If
            Case 1.6 To 16
                If dDCOffset > (7.99 - (dAmplitude / 2)) Then
                    Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Ramp dDCOffSet argument out of range.")
                    Err.Raise(-1000)
                    Exit Sub
                End If
        End Select


        If dDelay < 0 Or dDelay > 69.9 Then
            Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Ramp dDelay argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If

        If dLeadingEdge < 0 Or dLeadingEdge > 69.9 Then
            Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Ramp dLeadingEdge argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If
        If dTrailingEdge < 0 Or dTrailingEdge > 69.9 Then
            Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Ramp dTrailingEdge argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        Select Case Left(UCase(sTriggerMode), 1)
            Case "C" ' Continuous
            Case "T", "B" ' Trigger, Burst
                Select Case Left(UCase(sTriggerSource), 3)
                    Case "EXT", "INT"
                    Case "TTL"
                        Select Case CShort(Right(UCase(sTriggerSource), 1))
                            Case 0 To 7
                            Case Else
                                Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Ramp sTriggerSource argument out of range.")
                                Err.Raise(-1000)
                                Exit Sub
                        End Select
                    Case Else
                        Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Ramp sTriggerSource argument out of range.")
                        Err.Raise(-1000)
                        Exit Sub
                End Select

                If dTrigLevel < -10 Or dTrigLevel > 10 Then
                    Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Ramp dTrigLevel argument out of range.")
                    Err.Raise(-1000)
                    Exit Sub
                End If

                If dIntTrigDelay < 0.000015 Or dIntTrigDelay > 1000 Then
                    Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Ramp dInitTrigDelay argument out of range.")
                    Err.Raise(-1000)
                    Exit Sub
                End If

                If dTrigDelay < 0.0000001 Or dTrigDelay > 0.02 Then
                    Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Ramp dTrigDelay argument out of range.")
                    Err.Raise(-1000)
                    Exit Sub
                End If
                If Left(UCase(sTriggerMode), 1) = "B" Then ' Burst
                    If iBurstCount < 1 Or iBurstCount > 1000000.0 Then
                        Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Ramp iBurstCount argument out of range.")
                        Err.Raise(-1000)
                        Exit Sub
                    End If
                End If

            Case "G" ' Gate
                If dTrigLevel < -10 Or dTrigLevel > 10 Then
                    Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Ramp dTrigLevel argument out of range.")
                    Err.Raise(-1000)
                    Exit Sub
                End If

            Case Else
                Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Ramp sTriggerMode argument out of range.")
                Err.Raise(-1000)
                Exit Sub
        End Select

        If iTTLTrig < 0 Or iTTLTrig > 7 Then
            Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Ramp iTTLTrig argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        Select Case Left(UCase(sTTLMarkerSource), 1)
            Case "W", "E" ' Bit, External
            Case "I" ' Internal
                If dTTLMarkerSourceDelay < 0.000015 Or dTTLMarkerSourceDelay > 1000 Then
                    Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Ramp dTTLMarkerSourceDelay argument out of range.")
                    Err.Raise(-1000)
                    Exit Sub
                End If
            Case Else
                Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Ramp sTTLMarkerSource argument out of range.")
                Err.Raise(-1000)
                Exit Sub
        End Select

        If iECLTrig < 0 Or iECLTrig > 1 Then
            Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Ramp iECLTrig argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If

        If (b20MhzLowPassFilter And b25MhzLowPassFilter) Or (b20MhzLowPassFilter And b50MhzLowPassFilter) Or (b25MhzLowPassFilter And b50MhzLowPassFilter) Then
            Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Ramp multiple LowPassFilters selected.")
            Err.Raise(-1000)
            Exit Sub
        End If

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub

        'Exit Sub if simulation
        If bSimulation Then Exit Sub

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        'Program Instrument
        WriteMsg(FGEN, ":FUNC:SHAPE RAMP")
        WriteMsg(FGEN, ":FREQ " & CStr(dFrequency))
        WriteMsg(FGEN, ":VOLT " & CStr(dAmplitude))
        WriteMsg(FGEN, ":VOLT:OFFS " & CStr(dDCOffset))
        WriteMsg(FGEN, ":RAMP:DEL " & CStr(dDelay))
        WriteMsg(FGEN, ":RAMP:TRAN " & CStr(dLeadingEdge))
        WriteMsg(FGEN, ":RAMP:TRAN:TRA " & CStr(dTrailingEdge))
        Select Case Left(UCase(sTriggerMode), 1)
            Case "C" ' Continuous
                WriteMsg(FGEN, ":INIT:CONT ON")
            Case "T" ' Trigger
                WriteMsg(FGEN, ":INIT:CONT OFF")
                WriteMsg(FGEN, ":TRIG:BURS OFF")
                WriteMsg(FGEN, ":TRIG:GATE OFF")
                Select Case Left(UCase(sTriggerSource), 3)
                    Case "INT"
                        WriteMsg(FGEN, ":TRIG:SOUR:ADV Internal")
                        WriteMsg(FGEN, ":TRIG:TIM " & CStr(dIntTrigDelay))
                        WriteMsg(FGEN, ":TRIG:DEL " & CStr(dTrigDelay))
                    Case "EXT"
                        WriteMsg(FGEN, ":TRIG:SOUR:ADV External")
                        If bTrigSlopPositive Then
                            WriteMsg(FGEN, ":TRIG:SLOP POSITIVE")
                        Else
                            WriteMsg(FGEN, ":TRIG:SLOP NEGATIVE")
                        End If
                        WriteMsg(FGEN, ":TRIG:LEV " & CStr(dTrigLevel))
                        WriteMsg(FGEN, ":TRIG:DEL " & CStr(dTrigDelay))
                    Case "TTL"
                        WriteMsg(FGEN, ":TRIG:SOUR:ADV TTLTrg" & Right(UCase(sTriggerSource), 1))
                        WriteMsg(FGEN, ":TRIG:DEL " & CStr(dTrigDelay))
                End Select
            Case "B" ' Burst
                WriteMsg(FGEN, ":INIT:CONT OFF")
                WriteMsg(FGEN, ":TRIG:GATE OFF")
                WriteMsg(FGEN, ":TRIG:COUN " & CStr(iBurstCount))
                WriteMsg(FGEN, ":TRIG:BURS ON")
                Select Case Left(UCase(sTriggerSource), 3)
                    Case "INT"
                        WriteMsg(FGEN, ":TRIG:SOUR:ADV Internal")
                        WriteMsg(FGEN, ":TRIG:TIM " & CStr(dIntTrigDelay))
                        WriteMsg(FGEN, ":TRIG:DEL " & CStr(dTrigDelay))
                    Case "EXT"
                        WriteMsg(FGEN, ":TRIG:SOUR:ADV External")
                        If bTrigSlopPositive Then ' was BTRIGLSLOPPOSITIVE
                            WriteMsg(FGEN, ":TRIG:SLOP POSITIVE")
                        Else
                            WriteMsg(FGEN, ":TRIG:SLOP NEGATIVE")
                        End If
                        WriteMsg(FGEN, ":TRIG:LEV " & CStr(dTrigLevel))
                        WriteMsg(FGEN, ":TRIG:DEL " & CStr(dTrigDelay))
                    Case "TTL"
                        WriteMsg(FGEN, ":TRIG:SOUR:ADV TTLTrg" & Right(UCase(sTriggerSource), 1))
                        WriteMsg(FGEN, ":TRIG:DEL " & CStr(dTrigDelay))
                End Select
            Case "G" ' Gate
                WriteMsg(FGEN, ":INIT:CONT OFF")
                WriteMsg(FGEN, ":TRIG:BURS OFF")
                WriteMsg(FGEN, ":TRIG:GATE ON")
                WriteMsg(FGEN, ":TRIG:SOUR:ADV External")
                If bActiveGateLevel Then
                    WriteMsg(FGEN, ":TRIG:SLOP POSITIVE")
                Else
                    WriteMsg(FGEN, ":TRIG:SLOP NEGATIVE")
                End If
                WriteMsg(FGEN, ":TRIG:LEV " & CStr(dTrigLevel))
        End Select

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        If bSyncOutputWaveForm Then
            WriteMsg(FGEN, ":OUTP:SYNC:SOUR BIT")
        Else
            WriteMsg(FGEN, ":OUTP:SYNC:SOUR HCLock")
        End If

        If bSyncOutputOn Then
            WriteMsg(FGEN, ":OUTP:SYNC ON")
        Else
            WriteMsg(FGEN, ":OUTP:SYNC OFF")
        End If

        Select Case Left(UCase(sTTLMarkerSource), 1)
            Case "W"
                WriteMsg(FGEN, ":OUTP:TRIG:SOUR BIT")
            Case "E"
                WriteMsg(FGEN, ":OUTP:TRIG:SOUR EXTernal")
            Case "I"
                WriteMsg(FGEN, ":OUTP:TRIG:SOUR INTernal")
                WriteMsg(FGEN, ":TRIG:TIM " & CStr(dTTLMarkerSourceDelay))
        End Select

        If bTTLMarkerOutputOn Then
            WriteMsg(FGEN, ":OUTP:TTLTrg" & CStr(iTTLTrig) & " ON")
        Else
            WriteMsg(FGEN, ":OUTP:TTLTrg" & CStr(iTTLTrig) & " OFF")
        End If

        If bECLMarkerOutputOn Then
            WriteMsg(FGEN, ":OUTP:ECLTrg" & CStr(iECLTrig) & " ON")
        Else
            WriteMsg(FGEN, ":OUTP:ECLTrg" & CStr(iECLTrig) & " OFF")
        End If

        If b20MhzLowPassFilter Then
            WriteMsg(FGEN, ":OUTP:FILT:FREQ 20MHz")
            WriteMsg(FGEN, ":OUTP:FILT ON")
        End If

        If b25MhzLowPassFilter Then
            WriteMsg(FGEN, ":OUTP:FILT:FREQ 25MHz")
            WriteMsg(FGEN, ":OUTP:FILT ON")
        End If

        If b50MhzLowPassFilter Then
            WriteMsg(FGEN, ":OUTP:FILT:FREQ 25MHz")
            WriteMsg(FGEN, ":OUTP:FILT ON")
        End If

        If b20MhzLowPassFilter Or b25MhzLowPassFilter Or b50MhzLowPassFilter Then
            WriteMsg(FGEN, ":OUTP:FILT ON")
        Else
            WriteMsg(FGEN, ":OUTP:FILT OFF")
        End If

        'Turn on the output
        WriteMsg(FGEN, ":OUTP ON")

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
    End Sub
	
    Public Sub Pulse(ByRef dFrequency As Double, ByRef dAmplitude As Double, Optional ByRef dDCOffset As Double = 0,
                     Optional ByRef dDelay As Double = 10, Optional ByRef dPulseWidth As Double = 10,
                     Optional ByRef dLeadingEdge As Double = 10, Optional ByRef dTrailingEdge As Double = 10,
                     Optional ByRef sTriggerMode As String = "CONTINUOUS", Optional ByRef sTriggerSource As String = "External",
                     Optional ByRef bTrigSlopPositive As Boolean = True, Optional ByRef dTrigLevel As Double = 1.6,
                     Optional ByRef dIntTrigDelay As Double = 0.0001, Optional ByRef dTrigDelay As Double = 0.0000001,
                     Optional ByRef iBurstCount As Short = 1, Optional ByRef bActiveGateLevel As Boolean = True,
                     Optional ByRef bSyncOutputOn As Boolean = False, Optional ByRef bSyncOutputWaveForm As Boolean = True,
                     Optional ByRef iTTLTrig As Short = 0, Optional ByRef bTTLMarkerOutputOn As Boolean = False,
                     Optional ByRef sTTLMarkerSource As String = "WAVEFORM", Optional ByRef dTTLMarkerSourceDelay As Double = 0.0001,
                     Optional ByRef iECLTrig As Short = 0, Optional ByRef bECLMarkerOutputOn As Boolean = False,
                     Optional ByRef b50ohmsOutputLoadRef As Boolean = True, Optional ByRef b20MhzLowPassFilter As Boolean = False,
                     Optional ByRef b25MhzLowPassFilter As Boolean = False, Optional ByRef b50MhzLowPassFilter As Boolean = False)

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub

        gFrmMain.txtInstrument.Text = "FGEN"
        gFrmMain.txtCommand.Text = "Pulse"

        'Error Checking on Arguments
        If dFrequency > 1000000.0 Or dFrequency < 0.0001 Then
            Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Pulse dFrequency argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If

        If Not b50ohmsOutputLoadRef Then
            dAmplitude = dAmplitude / 2
            dDCOffset = dDCOffset / 2
        End If

        If dAmplitude > 16 Or dAmplitude < 0.01 Then
            Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Pulse dAmplitude argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If

        Select Case dAmplitude
            Case 0.01 To 0.159
                If dDCOffset > (0.08 - (dAmplitude / 2)) Then
                    Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Pulse dDCOffSet argument out of range.")
                    Err.Raise(-1000)
                    Exit Sub
                End If
            Case 0.16 To 1.599
                If dDCOffset > (0.799 - (dAmplitude / 2)) Then
                    Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Pulse dDCOffSet argument out of range.")
                    Err.Raise(-1000)
                    Exit Sub
                End If
            Case 1.6 To 16
                If dDCOffset > (7.99 - (dAmplitude / 2)) Then
                    Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Pulse dDCOffSet argument out of range.")
                    Err.Raise(-1000)
                    Exit Sub
                End If
        End Select

        If dDelay < 0 Or dDelay > 69.9 Then
            Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Pulse dDelay argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If

        If dPulseWidth < 0 Or dPulseWidth > 69.9 Then
            Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Pulse dPulseWidth argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If
        If dLeadingEdge < 0 Or dLeadingEdge > 69.9 Then
            Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Pulse dLeadingEdge argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If
        If dTrailingEdge < 0 Or dTrailingEdge > 69.9 Then
            Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Pulse dTrailingEdge argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        Select Case Left(UCase(sTriggerMode), 1)
            Case "C" ' Continuous
            Case "T", "B" ' Trigger, Burst
                Select Case Left(UCase(sTriggerSource), 3)
                    Case "EXT", "INT"
                    Case "TTL"
                        Select Case CShort(Right(UCase(sTriggerSource), 1))
                            Case 0 To 7
                            Case Else
                                Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Pulse sTriggerSource argument out of range.")
                                Err.Raise(-1000)
                                Exit Sub
                        End Select
                    Case Else
                        Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Pulse sTriggerSource argument out of range.")
                        Err.Raise(-1000)
                        Exit Sub
                End Select

                If dTrigLevel < -10 Or dTrigLevel > 10 Then
                    Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Pulse dTrigLevel argument out of range.")
                    Err.Raise(-1000)
                    Exit Sub
                End If

                If dIntTrigDelay < 0.000015 Or dIntTrigDelay > 1000 Then
                    Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Pulse dInitTrigDelay argument out of range.")
                    Err.Raise(-1000)
                    Exit Sub
                End If

                If dTrigDelay < 0.0000001 Or dTrigDelay > 0.02 Then
                    Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Pulse dTrigDelay argument out of range.")
                    Err.Raise(-1000)
                    Exit Sub
                End If
                If Left(UCase(sTriggerMode), 1) = "B" Then ' Burst
                    If iBurstCount < 1 Or iBurstCount > 1000000.0 Then
                        Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Pulse iBurstCount argument out of range.")
                        Err.Raise(-1000)
                        Exit Sub
                    End If
                End If

            Case "G" ' Gate
                If dTrigLevel < -10 Or dTrigLevel > 10 Then
                    Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Pulse dTrigLevel argument out of range.")
                    Err.Raise(-1000)
                    Exit Sub
                End If

            Case Else
                Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Pulse sTriggerMode argument out of range.")
                Err.Raise(-1000)
                Exit Sub
        End Select

        If iTTLTrig < 0 Or iTTLTrig > 7 Then
            Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Pulse iTTLTrig argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        Select Case Left(UCase(sTTLMarkerSource), 1)
            Case "W", "E" ' Bit, External
            Case "I" ' Internal
                If dTTLMarkerSourceDelay < 0.000015 Or dTTLMarkerSourceDelay > 1000 Then
                    Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Pulse dTTLMarkerSourceDelay argument out of range.")
                    Err.Raise(-1000)
                    Exit Sub
                End If
            Case Else
                Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Pulse sTTLMarkerSource argument out of range.")
                Err.Raise(-1000)
                Exit Sub
        End Select

        If iECLTrig < 0 Or iECLTrig > 1 Then
            Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Pulse iECLTrig argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If

        If (b20MhzLowPassFilter And b25MhzLowPassFilter) Or (b20MhzLowPassFilter And b50MhzLowPassFilter) Or (b25MhzLowPassFilter And b50MhzLowPassFilter) Then
            Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Pulse multiple LowPassFilters selected.")
            Err.Raise(-1000)
            Exit Sub
        End If

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub

        'Exit Sub if simulation
        If bSimulation Then Exit Sub

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        'Program Instrument
        WriteMsg(FGEN, ":FUNC:SHAP PULS")
        WriteMsg(FGEN, ":FREQ " & CStr(dFrequency))
        WriteMsg(FGEN, ":VOLT " & CStr(dAmplitude))
        WriteMsg(FGEN, ":VOLT:OFFS " & CStr(dDCOffset))
        WriteMsg(FGEN, ":PULS:DEL " & CStr(dDelay))
        WriteMsg(FGEN, ":PULS:WIDT " & CStr(dPulseWidth))
        WriteMsg(FGEN, ":PULS:TRAN " & CStr(dLeadingEdge))
        WriteMsg(FGEN, ":PULS:TRAN:TRA " & CStr(dTrailingEdge))
        Select Case Left(UCase(sTriggerMode), 1)
            Case "C" ' Continuous
                WriteMsg(FGEN, ":INIT:CONT ON")
            Case "T" ' Trigger
                WriteMsg(FGEN, ":INIT:CONT OFF")
                WriteMsg(FGEN, ":TRIG:BURS OFF")
                WriteMsg(FGEN, ":TRIG:GATE OFF")
                Select Case Left(UCase(sTriggerSource), 3)
                    Case "INT"
                        WriteMsg(FGEN, ":TRIG:SOUR:ADV Internal")
                        WriteMsg(FGEN, ":TRIG:TIM " & CStr(dIntTrigDelay))
                        WriteMsg(FGEN, ":TRIG:DEL " & CStr(dTrigDelay))
                    Case "EXT"
                        WriteMsg(FGEN, ":TRIG:SOUR:ADV External")
                        If bTrigSlopPositive Then
                            WriteMsg(FGEN, ":TRIG:SLOP POSITIVE")
                        Else
                            WriteMsg(FGEN, ":TRIG:SLOP NEGATIVE")
                        End If
                        WriteMsg(FGEN, ":TRIG:LEV " & CStr(dTrigLevel))
                        WriteMsg(FGEN, ":TRIG:DEL " & CStr(dTrigDelay))
                    Case "TTL"
                        WriteMsg(FGEN, ":TRIG:SOUR:ADV TTLTrg" & Right(UCase(sTriggerSource), 1))
                        WriteMsg(FGEN, ":TRIG:DEL " & CStr(dTrigDelay))
                End Select
            Case "B" ' Burst
                WriteMsg(FGEN, ":INIT:CONT OFF")
                WriteMsg(FGEN, ":TRIG:GATE OFF")
                WriteMsg(FGEN, ":TRIG:COUN " & CStr(iBurstCount))
                WriteMsg(FGEN, ":TRIG:BURS ON")
                Select Case Left(UCase(sTriggerSource), 3)
                    Case "INT"
                        WriteMsg(FGEN, ":TRIG:SOUR:ADV Internal")
                        WriteMsg(FGEN, ":TRIG:TIM " & CStr(dIntTrigDelay))
                        WriteMsg(FGEN, ":TRIG:DEL " & CStr(dTrigDelay))
                    Case "EXT"
                        WriteMsg(FGEN, ":TRIG:SOUR:ADV External")
                        If bTrigSlopPositive Then ' was BTRIGLSLOPPOSITIVE bbbb
                            WriteMsg(FGEN, ":TRIG:SLOP POSITIVE")
                        Else
                            WriteMsg(FGEN, ":TRIG:SLOP NEGATIVE")
                        End If
                        WriteMsg(FGEN, ":TRIG:LEV " & CStr(dTrigLevel))
                        WriteMsg(FGEN, ":TRIG:DEL " & CStr(dTrigDelay))
                    Case "TTL"
                        WriteMsg(FGEN, ":TRIG:SOUR:ADV TTLTrg" & Right(UCase(sTriggerSource), 1))
                        WriteMsg(FGEN, ":TRIG:DEL " & CStr(dTrigDelay))
                End Select
            Case "G" ' Gate
                WriteMsg(FGEN, ":INIT:CONT OFF")
                WriteMsg(FGEN, ":TRIG:BURS OFF")
                WriteMsg(FGEN, ":TRIG:GATE ON")
                WriteMsg(FGEN, ":TRIG:SOUR:ADV External")
                If bActiveGateLevel Then
                    WriteMsg(FGEN, ":TRIG:SLOP POSITIVE")
                Else
                    WriteMsg(FGEN, ":TRIG:SLOP NEGATIVE")
                End If
                WriteMsg(FGEN, ":TRIG:LEV " & CStr(dTrigLevel))
        End Select

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        If bSyncOutputWaveForm Then
            WriteMsg(FGEN, ":OUTP:SYNC:SOUR BIT")
        Else
            WriteMsg(FGEN, ":OUTP:SYNC:SOUR HCLock")
        End If

        If bSyncOutputOn Then
            WriteMsg(FGEN, ":OUTP:SYNC ON")
        Else
            WriteMsg(FGEN, ":OUTP:SYNC OFF")
        End If

        Select Case Left(UCase(sTTLMarkerSource), 1)
            Case "W"
                WriteMsg(FGEN, ":OUTP:TRIG:SOUR BIT")
            Case "E"
                WriteMsg(FGEN, ":OUTP:TRIG:SOUR EXTernal")
            Case "I"
                WriteMsg(FGEN, ":OUTP:TRIG:SOUR INTernal")
                WriteMsg(FGEN, ":TRIG:TIM " & CStr(dTTLMarkerSourceDelay))
        End Select

        If bTTLMarkerOutputOn Then
            WriteMsg(FGEN, ":OUTP:TTLTrg" & CStr(iTTLTrig) & " ON")
        Else
            WriteMsg(FGEN, ":OUTP:TTLTrg" & CStr(iTTLTrig) & " OFF")
        End If

        If bECLMarkerOutputOn Then
            WriteMsg(FGEN, ":OUTP:ECLTrg" & CStr(iECLTrig) & " ON")
        Else
            WriteMsg(FGEN, ":OUTP:ECLTrg" & CStr(iECLTrig) & " OFF")
        End If

        If b20MhzLowPassFilter Then
            WriteMsg(FGEN, ":OUTP:FILT:FREQ 20MHz")
            WriteMsg(FGEN, ":OUTP:FILT ON")
        End If

        If b25MhzLowPassFilter Then
            WriteMsg(FGEN, ":OUTP:FILT:FREQ 25MHz")
            WriteMsg(FGEN, ":OUTP:FILT ON")
        End If

        If b50MhzLowPassFilter Then
            WriteMsg(FGEN, ":OUTP:FILT:FREQ 25MHz")
            WriteMsg(FGEN, ":OUTP:FILT ON")
        End If

        If b20MhzLowPassFilter Or b25MhzLowPassFilter Or b50MhzLowPassFilter Then
            WriteMsg(FGEN, ":OUTP:FILT ON")
        Else
            WriteMsg(FGEN, ":OUTP:FILT OFF")
        End If

        'Turn on the output
        WriteMsg(FGEN, ":OUTP ON")

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
    End Sub

    Public Sub Exponential(ByRef dFrequency As Double, ByRef dAmplitude As Double, Optional ByRef dDCOffset As Double = 0,
                           Optional ByRef iFunctionalExponent As Short = -10, Optional ByRef sTriggerMode As String = "CONTINUOUS",
                           Optional ByRef sTriggerSource As String = "External", Optional ByRef bTrigSlopPositive As Boolean = True,
                           Optional ByRef dTrigLevel As Double = 1.6, Optional ByRef dIntTrigDelay As Double = 0.0001,
                           Optional ByRef dTrigDelay As Double = 0.0000001, Optional ByRef iBurstCount As Short = 1,
                           Optional ByRef bActiveGateLevel As Boolean = True, Optional ByRef bSyncOutputOn As Boolean = False,
                           Optional ByRef bSyncOutputWaveForm As Boolean = True, Optional ByRef iTTLTrig As Short = 0,
                           Optional ByRef bTTLMarkerOutputOn As Boolean = False, Optional ByRef sTTLMarkerSource As String = "WAVEFORM",
                           Optional ByRef dTTLMarkerSourceDelay As Double = 0.0001, Optional ByRef iECLTrig As Short = 0,
                           Optional ByRef bECLMarkerOutputOn As Boolean = False, Optional ByRef b50ohmsOutputLoadRef As Boolean = True,
                           Optional ByRef b20MhzLowPassFilter As Boolean = False, Optional ByRef b25MhzLowPassFilter As Boolean = False,
                           Optional ByRef b50MhzLowPassFilter As Boolean = False)

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub

        gFrmMain.txtInstrument.Text = "FGEN"
        gFrmMain.txtCommand.Text = "Exponential"

        'Error Checking on Arguments
        If dFrequency > 1000000.0 Or dFrequency < 0.0001 Then
            Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Exponential dFrequency argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If

        If Not b50ohmsOutputLoadRef Then
            dAmplitude = dAmplitude / 2
            dDCOffset = dDCOffset / 2
        End If

        If dAmplitude > 16 Or dAmplitude < 0.01 Then
            Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Exponential dAmplitude argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If

        Select Case dAmplitude
            Case 0.01 To 0.159
                If dDCOffset > (0.08 - (dAmplitude / 2)) Then
                    Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Exponential dDCOffSet argument out of range.")
                    Err.Raise(-1000)
                    Exit Sub
                End If
            Case 0.16 To 1.599
                If dDCOffset > (0.799 - (dAmplitude / 2)) Then
                    Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Exponential dDCOffSet argument out of range.")
                    Err.Raise(-1000)
                    Exit Sub
                End If
            Case 1.6 To 16
                If dDCOffset > (7.99 - (dAmplitude / 2)) Then
                    Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Exponential dDCOffSet argument out of range.")
                    Err.Raise(-1000)
                    Exit Sub
                End If
        End Select

        If iFunctionalExponent < -200 Or iFunctionalExponent > 200 Then
            Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Exponential ifunctionalexponent argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If


        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        Select Case Left(UCase(sTriggerMode), 1)
            Case "C" ' Continuous
            Case "T", "B" ' Trigger, Burst
                Select Case Left(UCase(sTriggerSource), 3)
                    Case "EXT", "INT"
                    Case "TTL"
                        Select Case CShort(Right(UCase(sTriggerSource), 1))
                            Case 0 To 7
                            Case Else
                                Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Exponential sTriggerSource argument out of range.")
                                Err.Raise(-1000)
                                Exit Sub
                        End Select
                    Case Else
                        Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Exponential sTriggerSource argument out of range.")
                        Err.Raise(-1000)
                        Exit Sub
                End Select

                If dTrigLevel < -10 Or dTrigLevel > 10 Then
                    Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Exponential dTrigLevel argument out of range.")
                    Err.Raise(-1000)
                    Exit Sub
                End If

                If dIntTrigDelay < 0.000015 Or dIntTrigDelay > 1000 Then
                    Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Exponential dInitTrigDelay argument out of range.")
                    Err.Raise(-1000)
                    Exit Sub
                End If

                If dTrigDelay < 0.0000001 Or dTrigDelay > 0.02 Then
                    Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Exponential dTrigDelay argument out of range.")
                    Err.Raise(-1000)
                    Exit Sub
                End If
                If Left(UCase(sTriggerMode), 1) = "B" Then ' Burst
                    If iBurstCount < 1 Or iBurstCount > 1000000.0 Then
                        Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Exponential iBurstCount argument out of range.")
                        Err.Raise(-1000)
                        Exit Sub
                    End If
                End If

            Case "G" ' Gate
                If dTrigLevel < -10 Or dTrigLevel > 10 Then
                    Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Exponential dTrigLevel argument out of range.")
                    Err.Raise(-1000)
                    Exit Sub
                End If

            Case Else
                Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Exponential sTriggerMode argument out of range.")
                Err.Raise(-1000)
                Exit Sub
        End Select

        If iTTLTrig < 0 Or iTTLTrig > 7 Then
            Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Exponential iTTLTrig argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        Select Case Left(UCase(sTTLMarkerSource), 1)
            Case "W", "E" ' Bit, External
            Case "I" ' Internal
                If dTTLMarkerSourceDelay < 0.000015 Or dTTLMarkerSourceDelay > 1000 Then
                    Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Exponential dTTLMarkerSourceDelay argument out of range.")
                    Err.Raise(-1000)
                    Exit Sub
                End If
            Case Else
                Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Exponential sTTLMarkerSource argument out of range.")
                Err.Raise(-1000)
                Exit Sub
        End Select

        If iECLTrig < 0 Or iECLTrig > 1 Then
            Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Exponential iECLTrig argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub

        If bSimulation Then Exit Sub

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        'Program Instrument
        WriteMsg(FGEN, ":FUNC:SHAP EXP")
        WriteMsg(FGEN, ":FREQ " & CStr(dFrequency))
        WriteMsg(FGEN, ":VOLT " & CStr(dAmplitude))
        WriteMsg(FGEN, ":VOLT:OFFS " & CStr(dDCOffset))
        WriteMsg(FGEN, ":EXP:EXP " & CStr(iFunctionalExponent))
        Select Case Left(UCase(sTriggerMode), 1)
            Case "C" ' Continuous
                WriteMsg(FGEN, ":INIT:CONT ON")
            Case "T" ' Trigger
                WriteMsg(FGEN, ":INIT:CONT OFF")
                WriteMsg(FGEN, ":TRIG:BURS OFF")
                WriteMsg(FGEN, ":TRIG:GATE OFF")
                Select Case Left(UCase(sTriggerSource), 3)
                    Case "INT"
                        WriteMsg(FGEN, ":TRIG:SOUR:ADV Internal")
                        WriteMsg(FGEN, ":TRIG:TIM " & CStr(dIntTrigDelay))
                        WriteMsg(FGEN, ":TRIG:DEL " & CStr(dTrigDelay))
                    Case "EXT"
                        WriteMsg(FGEN, ":TRIG:SOUR:ADV External")
                        If bTrigSlopPositive Then
                            WriteMsg(FGEN, ":TRIG:SLOP POSITIVE")
                        Else
                            WriteMsg(FGEN, ":TRIG:SLOP NEGATIVE")
                        End If
                        WriteMsg(FGEN, ":TRIG:LEV " & CStr(dTrigLevel))
                        WriteMsg(FGEN, ":TRIG:DEL " & CStr(dTrigDelay))
                    Case "TTL"
                        WriteMsg(FGEN, ":TRIG:SOUR:ADV TTLTrg" & Right(UCase(sTriggerSource), 1))
                        WriteMsg(FGEN, ":TRIG:DEL " & CStr(dTrigDelay))
                End Select
            Case "B" ' Burst
                WriteMsg(FGEN, ":INIT:CONT OFF")
                WriteMsg(FGEN, ":TRIG:GATE OFF")
                WriteMsg(FGEN, ":TRIG:COUN " & CStr(iBurstCount))
                WriteMsg(FGEN, ":TRIG:BURS ON")
                Select Case Left(UCase(sTriggerSource), 3)
                    Case "INT"
                        WriteMsg(FGEN, ":TRIG:SOUR:ADV Internal")
                        WriteMsg(FGEN, ":TRIG:TIM " & CStr(dIntTrigDelay))
                        WriteMsg(FGEN, ":TRIG:DEL " & CStr(dTrigDelay))
                    Case "EXT"
                        WriteMsg(FGEN, ":TRIG:SOUR:ADV External")
                        If bTrigSlopPositive Then ' was BTRIGLSLOPPOSITIVE bbbb
                            WriteMsg(FGEN, ":TRIG:SLOP POSITIVE")
                        Else
                            WriteMsg(FGEN, ":TRIG:SLOP NEGATIVE")
                        End If
                        WriteMsg(FGEN, ":TRIG:LEV " & CStr(dTrigLevel))
                        WriteMsg(FGEN, ":TRIG:DEL " & CStr(dTrigDelay))
                    Case "TTL"
                        WriteMsg(FGEN, ":TRIG:SOUR:ADV TTLTrg" & Right(UCase(sTriggerSource), 1))
                        WriteMsg(FGEN, ":TRIG:DEL " & CStr(dTrigDelay))
                End Select
            Case "G" ' Gate
                WriteMsg(FGEN, ":INIT:CONT OFF")
                WriteMsg(FGEN, ":TRIG:BURS OFF")
                WriteMsg(FGEN, ":TRIG:GATE ON")
                WriteMsg(FGEN, ":TRIG:SOUR:ADV External")
                If bActiveGateLevel Then
                    WriteMsg(FGEN, ":TRIG:SLOP POSITIVE")
                Else
                    WriteMsg(FGEN, ":TRIG:SLOP NEGATIVE")
                End If
                WriteMsg(FGEN, ":TRIG:LEV " & CStr(dTrigLevel))
        End Select

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        If bSyncOutputWaveForm Then
            WriteMsg(FGEN, ":OUTP:SYNC:SOUR BIT")
        Else
            WriteMsg(FGEN, ":OUTP:SYNC:SOUR HCLock")
        End If

        If bSyncOutputOn Then
            WriteMsg(FGEN, ":OUTP:SYNC ON")
        Else
            WriteMsg(FGEN, ":OUTP:SYNC OFF")
        End If

        Select Case Left(UCase(sTTLMarkerSource), 1)
            Case "W"
                WriteMsg(FGEN, ":OUTP:TRIG:SOUR BIT")
            Case "E"
                WriteMsg(FGEN, ":OUTP:TRIG:SOUR EXTernal")
            Case "I"
                WriteMsg(FGEN, ":OUTP:TRIG:SOUR INTernal")
                WriteMsg(FGEN, ":TRIG:TIM " & CStr(dTTLMarkerSourceDelay))
        End Select

        If bTTLMarkerOutputOn Then
            WriteMsg(FGEN, ":OUTP:TTLTrg" & CStr(iTTLTrig) & " ON")
        Else
            WriteMsg(FGEN, ":OUTP:TTLTrg" & CStr(iTTLTrig) & " OFF")
        End If

        If bECLMarkerOutputOn Then
            WriteMsg(FGEN, ":OUTP:ECLTrg" & CStr(iECLTrig) & " ON")
        Else
            WriteMsg(FGEN, ":OUTP:ECLTrg" & CStr(iECLTrig) & " OFF")
        End If

        If b20MhzLowPassFilter Then
            WriteMsg(FGEN, ":OUTP:FILT:FREQ 20MHz")
            WriteMsg(FGEN, ":OUTP:FILT ON")
        End If

        If b25MhzLowPassFilter Then
            WriteMsg(FGEN, ":OUTP:FILT:FREQ 25MHz")
            WriteMsg(FGEN, ":OUTP:FILT ON")
        End If

        If b50MhzLowPassFilter Then
            WriteMsg(FGEN, ":OUTP:FILT:FREQ 25MHz")
            WriteMsg(FGEN, ":OUTP:FILT ON")
        End If

        If b20MhzLowPassFilter Or b25MhzLowPassFilter Or b50MhzLowPassFilter Then
            WriteMsg(FGEN, ":OUTP:FILT ON")
        Else
            WriteMsg(FGEN, ":OUTP:FILT OFF")
        End If

        'Turn on the output
        WriteMsg(FGEN, ":OUTP ON")

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
    End Sub

    Public Sub Gaussian(ByRef dFrequency As Double, ByRef dAmplitude As Double, Optional ByRef dDCOffset As Double = 0,
                        Optional ByRef iFunctionalExponent As Short = 10, Optional ByRef sTriggerMode As String = "CONTINUOUS",
                        Optional ByRef sTriggerSource As String = "External", Optional ByRef bTrigSlopPositive As Boolean = True,
                        Optional ByRef dTrigLevel As Double = 1.6, Optional ByRef dIntTrigDelay As Double = 0.0001,
                        Optional ByRef dTrigDelay As Double = 0.0000001, Optional ByRef iBurstCount As Short = 1,
                        Optional ByRef bActiveGateLevel As Boolean = True, Optional ByRef bSyncOutputOn As Boolean = False,
                        Optional ByRef bSyncOutputWaveForm As Boolean = True, Optional ByRef iTTLTrig As Short = 0,
                        Optional ByRef bTTLMarkerOutputOn As Boolean = False, Optional ByRef sTTLMarkerSource As String = "WAVEFORM",
                        Optional ByRef dTTLMarkerSourceDelay As Double = 0.0001, Optional ByRef iECLTrig As Short = 0,
                        Optional ByRef bECLMarkerOutputOn As Boolean = False, Optional ByRef b50ohmsOutputLoadRef As Boolean = True,
                        Optional ByRef b20MhzLowPassFilter As Boolean = False, Optional ByRef b25MhzLowPassFilter As Boolean = False,
                        Optional ByRef b50MhzLowPassFilter As Boolean = False)

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub

        gFrmMain.txtInstrument.Text = "FGEN"
        gFrmMain.txtCommand.Text = "Gaussian"

        'Error Checking on Arguments
        If dFrequency > 1000000.0 Or dFrequency < 0.0001 Then
            Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Gaussian dFrequency argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If

        If Not b50ohmsOutputLoadRef Then
            dAmplitude = dAmplitude / 2
            dDCOffset = dDCOffset / 2
        End If

        If dAmplitude > 16 Or dAmplitude < 0.01 Then
            Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Gaussian dAmplitude argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If

        Select Case dAmplitude
            Case 0.01 To 0.159
                If dDCOffset > (0.08 - (dAmplitude / 2)) Then
                    Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Gaussian dDCOffSet argument out of range.")
                    Err.Raise(-1000)
                    Exit Sub
                End If
            Case 0.16 To 1.599
                If dDCOffset > (0.799 - (dAmplitude / 2)) Then
                    Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Gaussian dDCOffSet argument out of range.")
                    Err.Raise(-1000)
                    Exit Sub
                End If
            Case 1.6 To 16
                If dDCOffset > (7.99 - (dAmplitude / 2)) Then
                    Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Gaussian dDCOffSet argument out of range.")
                    Err.Raise(-1000)
                    Exit Sub
                End If
        End Select

        If iFunctionalExponent < 1 Or iFunctionalExponent > 200 Then
            Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Gaussian iFunctionalExponent argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If


        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        Select Case Left(UCase(sTriggerMode), 1)
            Case "C" ' Continuous
            Case "T", "B" ' Trigger, Burst
                Select Case Left(UCase(sTriggerSource), 3)
                    Case "EXT", "INT"
                    Case "TTL"
                        Select Case CShort(Right(UCase(sTriggerSource), 1))
                            Case 0 To 7
                            Case Else
                                Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Gaussian sTriggerSource argument out of range.")
                                Err.Raise(-1000)
                                Exit Sub
                        End Select
                    Case Else
                        Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Gaussian sTriggerSource argument out of range.")
                        Err.Raise(-1000)
                        Exit Sub
                End Select

                If dTrigLevel < -10 Or dTrigLevel > 10 Then
                    Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Gaussian dTrigLevel argument out of range.")
                    Err.Raise(-1000)
                    Exit Sub
                End If

                If dIntTrigDelay < 0.000015 Or dIntTrigDelay > 1000 Then
                    Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Gaussian dInitTrigDelay argument out of range.")
                    Err.Raise(-1000)
                    Exit Sub
                End If

                If dTrigDelay < 0.0000001 Or dTrigDelay > 0.02 Then
                    Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Gaussian dTrigDelay argument out of range.")
                    Err.Raise(-1000)
                    Exit Sub
                End If
                If Left(UCase(sTriggerMode), 1) = "B" Then ' Burst
                    If iBurstCount < 1 Or iBurstCount > 1000000.0 Then
                        Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Gaussian iBurstCount argument out of range.")
                        Err.Raise(-1000)
                        Exit Sub
                    End If
                End If

            Case "G" ' Gate
                If dTrigLevel < -10 Or dTrigLevel > 10 Then
                    Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Gaussian dTrigLevel argument out of range.")
                    Err.Raise(-1000)
                    Exit Sub
                End If

            Case Else
                Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Gaussian sTriggerMode argument out of range.")
                Err.Raise(-1000)
                Exit Sub
        End Select

        If iTTLTrig < 0 Or iTTLTrig > 7 Then
            Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Gaussian iTTLTrig argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        Select Case Left(UCase(sTTLMarkerSource), 1)
            Case "W", "E" ' Bit, External
            Case "I" ' Internal
                If dTTLMarkerSourceDelay < 0.000015 Or dTTLMarkerSourceDelay > 1000 Then
                    Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Gaussian dTTLMarkerSourceDelay argument out of range.")
                    Err.Raise(-1000)
                    Exit Sub
                End If
            Case Else
                Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Gaussian sTTLMarkerSource argument out of range.")
                Err.Raise(-1000)
                Exit Sub
        End Select

        If iECLTrig < 0 Or iECLTrig > 1 Then
            Echo("FGEN PROGRAMMING ERROR:  Command cmdFGEN.Gaussian iECLTrig argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub

        If bSimulation Then Exit Sub

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        'Program Instrument
        WriteMsg(FGEN, ":FUNC:SHAP GAUS")
        WriteMsg(FGEN, ":FREQ " & CStr(dFrequency))
        WriteMsg(FGEN, ":VOLT " & CStr(dAmplitude))
        WriteMsg(FGEN, ":VOLT:OFFS " & CStr(dDCOffset))
        WriteMsg(FGEN, ":GAUS:EXP " & CStr(iFunctionalExponent))
        Select Case Left(UCase(sTriggerMode), 1)
            Case "C" ' Continuous
                WriteMsg(FGEN, ":INIT:CONT ON")
            Case "T" ' Trigger
                WriteMsg(FGEN, ":INIT:CONT OFF")
                WriteMsg(FGEN, ":TRIG:BURS OFF")
                WriteMsg(FGEN, ":TRIG:GATE OFF")
                Select Case Left(UCase(sTriggerSource), 3)
                    Case "INT"
                        WriteMsg(FGEN, ":TRIG:SOUR:ADV Internal")
                        WriteMsg(FGEN, ":TRIG:TIM " & CStr(dIntTrigDelay))
                        WriteMsg(FGEN, ":TRIG:DEL " & CStr(dTrigDelay))
                    Case "EXT"
                        WriteMsg(FGEN, ":TRIG:SOUR:ADV External")
                        If bTrigSlopPositive Then
                            WriteMsg(FGEN, ":TRIG:SLOP POSITIVE")
                        Else
                            WriteMsg(FGEN, ":TRIG:SLOP NEGATIVE")
                        End If
                        WriteMsg(FGEN, ":TRIG:LEV " & CStr(dTrigLevel))
                        WriteMsg(FGEN, ":TRIG:DEL " & CStr(dTrigDelay))
                    Case "TTL"
                        WriteMsg(FGEN, ":TRIG:SOUR:ADV TTLTrg" & Right(UCase(sTriggerSource), 1))
                        WriteMsg(FGEN, ":TRIG:DEL " & CStr(dTrigDelay))
                End Select
            Case "B" ' Burst
                WriteMsg(FGEN, ":INIT:CONT OFF")
                WriteMsg(FGEN, ":TRIG:GATE OFF")
                WriteMsg(FGEN, ":TRIG:COUN " & CStr(iBurstCount))
                WriteMsg(FGEN, ":TRIG:BURS ON")
                Select Case Left(UCase(sTriggerSource), 3)
                    Case "INT"
                        WriteMsg(FGEN, ":TRIG:SOUR:ADV Internal")
                        WriteMsg(FGEN, ":TRIG:TIM " & CStr(dIntTrigDelay))
                        WriteMsg(FGEN, ":TRIG:DEL " & CStr(dTrigDelay))
                    Case "EXT"
                        WriteMsg(FGEN, ":TRIG:SOUR:ADV External")
                        If bTrigSlopPositive Then 'was BTRIGLSLOPPOSITIVE bbbb
                            WriteMsg(FGEN, ":TRIG:SLOP POSITIVE")
                        Else
                            WriteMsg(FGEN, ":TRIG:SLOP NEGATIVE")
                        End If
                        WriteMsg(FGEN, ":TRIG:LEV " & CStr(dTrigLevel))
                        WriteMsg(FGEN, ":TRIG:DEL " & CStr(dTrigDelay))
                    Case "TTL"
                        WriteMsg(FGEN, ":TRIG:SOUR:ADV TTLTrg" & Right(UCase(sTriggerSource), 1))
                        WriteMsg(FGEN, ":TRIG:DEL " & CStr(dTrigDelay))
                End Select
            Case "G" ' Gate
                WriteMsg(FGEN, ":INIT:CONT OFF")
                WriteMsg(FGEN, ":TRIG:BURS OFF")
                WriteMsg(FGEN, ":TRIG:GATE ON")
                WriteMsg(FGEN, ":TRIG:SOUR:ADV External")
                If bActiveGateLevel Then
                    WriteMsg(FGEN, ":TRIG:SLOP POSITIVE")
                Else
                    WriteMsg(FGEN, ":TRIG:SLOP NEGATIVE")
                End If
                WriteMsg(FGEN, ":TRIG:LEV " & CStr(dTrigLevel))
        End Select

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        If bSyncOutputWaveForm Then
            WriteMsg(FGEN, ":OUTP:SYNC:SOUR BIT")
        Else
            WriteMsg(FGEN, ":OUTP:SYNC:SOUR HCLock")
        End If

        If bSyncOutputOn Then
            WriteMsg(FGEN, ":OUTP:SYNC ON")
        Else
            WriteMsg(FGEN, ":OUTP:SYNC OFF")
        End If

        Select Case Left(UCase(sTTLMarkerSource), 1)
            Case "W"
                WriteMsg(FGEN, ":OUTP:TRIG:SOUR BIT")
            Case "E"
                WriteMsg(FGEN, ":OUTP:TRIG:SOUR EXTernal")
            Case "I"
                WriteMsg(FGEN, ":OUTP:TRIG:SOUR INTernal")
                WriteMsg(FGEN, ":TRIG:TIM " & CStr(dTTLMarkerSourceDelay))
        End Select

        If bTTLMarkerOutputOn Then
            WriteMsg(FGEN, ":OUTP:TTLTrg" & CStr(iTTLTrig) & " ON")
        Else
            WriteMsg(FGEN, ":OUTP:TTLTrg" & CStr(iTTLTrig) & " OFF")
        End If

        If bECLMarkerOutputOn Then
            WriteMsg(FGEN, ":OUTP:ECLTrg" & CStr(iECLTrig) & " ON")
        Else
            WriteMsg(FGEN, ":OUTP:ECLTrg" & CStr(iECLTrig) & " OFF")
        End If

        If b20MhzLowPassFilter Then
            WriteMsg(FGEN, ":OUTP:FILT:FREQ 20MHz")
            WriteMsg(FGEN, ":OUTP:FILT ON")
        End If

        If b25MhzLowPassFilter Then
            WriteMsg(FGEN, ":OUTP:FILT:FREQ 25MHz")
            WriteMsg(FGEN, ":OUTP:FILT ON")
        End If

        If b50MhzLowPassFilter Then
            WriteMsg(FGEN, ":OUTP:FILT:FREQ 25MHz")
            WriteMsg(FGEN, ":OUTP:FILT ON")
        End If

        If b20MhzLowPassFilter Or b25MhzLowPassFilter Or b50MhzLowPassFilter Then
            WriteMsg(FGEN, ":OUTP:FILT ON")
        Else
            WriteMsg(FGEN, ":OUTP:FILT OFF")
        End If

        'Turn on the output
        WriteMsg(FGEN, ":OUTP ON")

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
    End Sub
	
    Public Sub Reset()
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        gFrmMain.txtInstrument.Text = "FGEN"
        gFrmMain.txtCommand.Text = "Reset"

        If Not bSimulation Then
            WriteMsg(FGEN, "*CLS;*RST")
        End If
    End Sub
End Class