VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "cFGEN"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
DefInt A-Z

'bbbb added 2012-05-08
' Problem: Using external trigger caused error messages from the fgen instrument.
' dTrigDelay that is passed to the instrument is number of sample clocks between 10 and 2e6.
' dTrigDelay setup in Sine/Exponential/Pulse/Gaussian/Triangle/Square/Ramp is in seconds
'   between 0.0000001 and 0.02 seconds.
' In order to avoid changing the public functions, added new variable xTrigDelay and set it
'   equal to dTrigDelay * dFrequency, then checked to make sure it is between 10 and 2e6
'   in each command.
' Default 0.00000001 seconds with a frequency of 10Khz would be changed to 0.0001 which is too
'   low a number, so it is changed to minimum value of 10 (10 samples = 0.001 seconds)

Public Sub SendMsg(sMsg As String)
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    frmMain.txtInstrument.Text = "FGEN"
    frmMain.txtCommand.Text = "SendMsg"
    WriteMsg FGEN, sMsg
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON
End Sub

Public Function sReadMsg(sMsg As String) As String
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    frmMain.txtInstrument.Text = "FGEN"
    frmMain.txtCommand.Text = "ReadMsg"
    ReadMsg FGEN, sReadMsg
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON
End Function

Public Sub Sine(dFrequency As Double, dAmplitude As Double, _
                    Optional dDCOffset As Double = 0, _
                    Optional iFunctionalExponent As Integer = 1, _
                    Optional iPhaseShift As Integer = 0, _
                    Optional sTriggerMode As String = "CONTINUOUS", _
                    Optional sTriggerSource As String = "External", _
                    Optional bTrigSlopPositive As Boolean = True, _
                    Optional dTrigLevel As Double = 1.6, _
                    Optional dIntTrigDelay As Double = 0.0001, _
                    Optional dTrigDelay As Double = 0.0000001, _
                    Optional iBurstCount As Integer = 1, _
                    Optional bActiveGateLevel As Boolean = True, _
                    Optional bSyncOutputOn As Boolean = False, _
                    Optional bSyncOutputWaveForm As Boolean = True, _
                    Optional iTTLTrig As Integer = 0, _
                    Optional bTTLMarkerOutputOn As Boolean = False, _
                    Optional sTTLMarkerSource As String = "WAVEFORM", _
                    Optional dTTLMarkerSourceDelay As Double = 0.0001, _
                    Optional iECLTrig As Integer = 0, _
                    Optional bECLMarkerOutputOn As Boolean = False, _
                    Optional b50ohmsOutputLoadRef As Boolean = True)
                    
    If Setup("cmdFGEN.Sine", "Sine", "SIN", dFrequency, dAmplitude, dDCOffset, _
             sTriggerMode, sTriggerSource, bTrigSlopPositive, dTrigLevel, dIntTrigDelay, _
             dTrigDelay, iBurstCount, bActiveGateLevel, bSyncOutputOn, bSyncOutputWaveForm, _
             iTTLTrig, bTTLMarkerOutputOn, sTTLMarkerSource, dTTLMarkerSourceDelay, _
             iECLTrig, bECLMarkerOutputOn, b50ohmsOutputLoadRef, False, False, False) = False Then
        Exit Sub
    End If
    
    If iFunctionalExponent < 1 Or iFunctionalExponent > 9 Then
        Echo "FGEN PROGRAMMING ERROR:  Command cmdFGEN.Sine ifunctionalexponent argument out of range."
        Err.Raise -1000
        Exit Sub
    End If
    
    If iPhaseShift < 0 Or iPhaseShift > 360 Then
        Echo "FGEN PROGRAMMING ERROR:  Command cmdFGEN.Sine iPhaseShift argument out of range."
        Err.Raise -1000
        Exit Sub
    End If
        
    'Program Instrument
    WriteMsg FGEN, ":SIN:POW " & CStr(iFunctionalExponent)
    WriteMsg FGEN, ":SIN:PHAS " & CStr(iPhaseShift)
    'Turn on the output
    WriteMsg FGEN, ":OUTP ON"
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub

End Sub

Public Sub Triangle(dFrequency As Double, dAmplitude As Double, _
                    Optional dDCOffset As Double = 0, _
                    Optional iFunctionalExponent As Integer = 1, _
                    Optional iPhaseShift As Integer = 0, _
                    Optional sTriggerMode As String = "CONTINUOUS", _
                    Optional sTriggerSource As String = "External", _
                    Optional bTrigSlopPositive As Boolean = True, _
                    Optional dTrigLevel As Double = 1.6, _
                    Optional dIntTrigDelay As Double = 0.0001, _
                    Optional dTrigDelay As Double = 0.0000001, _
                    Optional iBurstCount As Integer = 1, _
                    Optional bActiveGateLevel As Boolean = True, _
                    Optional bSyncOutputOn As Boolean = False, _
                    Optional bSyncOutputWaveForm As Boolean = True, _
                    Optional iTTLTrig As Integer = 0, _
                    Optional bTTLMarkerOutputOn As Boolean = False, _
                    Optional sTTLMarkerSource As String = "WAVEFORM", _
                    Optional dTTLMarkerSourceDelay As Double = 0.0001, _
                    Optional iECLTrig As Integer = 0, _
                    Optional bECLMarkerOutputOn As Boolean = False, _
                    Optional b50ohmsOutputLoadRef As Boolean = True, _
                    Optional b20MhzLowPassFilter As Boolean = False, _
                    Optional b25MhzLowPassFilter As Boolean = False, _
                    Optional b50MhzLowPassFilter As Boolean = False)
                    
    If Setup("cmdFGEN.Triangle", "Triangle", "TRI", dFrequency, dAmplitude, dDCOffset, _
             sTriggerMode, sTriggerSource, bTrigSlopPositive, dTrigLevel, dIntTrigDelay, _
             dTrigDelay, iBurstCount, bActiveGateLevel, bSyncOutputOn, bSyncOutputWaveForm, _
             iTTLTrig, bTTLMarkerOutputOn, sTTLMarkerSource, dTTLMarkerSourceDelay, _
             iECLTrig, bECLMarkerOutputOn, b50ohmsOutputLoadRef, b20MhzLowPassFilter, _
             b25MhzLowPassFilter, b50MhzLowPassFilter) = False Then
        Exit Sub
    End If
    
    If iFunctionalExponent < 1 Or iFunctionalExponent > 9 Then
        Echo "FGEN PROGRAMMING ERROR:  Command cmdFGEN.Triangle ifunctionalexponent argument out of range."
        Err.Raise -1000
        Exit Sub
    End If
    
    If iPhaseShift < 0 Or iPhaseShift > 360 Then
        Echo "FGEN PROGRAMMING ERROR:  Command cmdFGEN.Triangle iPhaseShift argument out of range."
        Err.Raise -1000
        Exit Sub
    End If
    
    'Program Instrument
    WriteMsg FGEN, ":TRI:POW " & CStr(iFunctionalExponent)
    WriteMsg FGEN, ":TRI:PHAS " & CStr(iPhaseShift)
    
    'Turn on the output
    WriteMsg FGEN, ":OUTP ON"
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub

End Sub

Public Sub DC(dAmplitude As Double, Optional b50ohmsOutputLoadRef As Boolean = False)
                    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    frmMain.txtInstrument.Text = "FGEN"
    frmMain.txtCommand.Text = "DC"
    'Error Checking on Arguments

    If b50ohmsOutputLoadRef Then
        dAmplitude = dAmplitude * 2
    End If
    
    If dAmplitude < -16 Or dAmplitude > 16 Then
        Echo "FGEN PROGRAMMING ERROR:  Command cmdFGEN.Square dAmplitude argument out of range."
        Err.Raise -1000
        Exit Sub
    End If
    
    
    'Exit Sub if simulation
    If bSimulation Then Exit Sub

    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    'Program Instrument
    WriteMsg FGEN, ":OUTP 1"
    WriteMsg FGEN, ":FUNC:SHAP DC"
    If dAmplitude = 0 Then
        WriteMsg FGEN, ":DC 0"
    ElseIf dAmplitude < 0 Then
        WriteMsg FGEN, ":DC -100"
        dAmplitude = -dAmplitude
    Else
        WriteMsg FGEN, ":DC 100"
    End If
    WriteMsg FGEN, ":VOLT " & CStr(dAmplitude)
    
    'Turn on the output
    WriteMsg FGEN, ":OUTP ON"
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
End Sub

Public Sub Square(dFrequency As Double, dAmplitude As Double, _
                    Optional dDCOffset As Double = 0, _
                    Optional iDutyCycle As Integer = 50, _
                    Optional sTriggerMode As String = "CONTINUOUS", _
                    Optional sTriggerSource As String = "External", _
                    Optional bTrigSlopPositive As Boolean = True, _
                    Optional dTrigLevel As Double = 1.6, _
                    Optional dIntTrigDelay As Double = 0.0001, _
                    Optional dTrigDelay As Double = 0.0000001, _
                    Optional iBurstCount As Integer = 1, _
                    Optional bActiveGateLevel As Boolean = True, _
                    Optional bSyncOutputOn As Boolean = False, _
                    Optional bSyncOutputWaveForm As Boolean = True, _
                    Optional iTTLTrig As Integer = 0, _
                    Optional bTTLMarkerOutputOn As Boolean = False, _
                    Optional sTTLMarkerSource As String = "WAVEFORM", _
                    Optional dTTLMarkerSourceDelay As Double = 0.0001, _
                    Optional iECLTrig As Integer = 0, _
                    Optional bECLMarkerOutputOn As Boolean = False, _
                    Optional b50ohmsOutputLoadRef As Boolean = True, _
                    Optional b20MhzLowPassFilter As Boolean = False, _
                    Optional b25MhzLowPassFilter As Boolean = False, _
                    Optional b50MhzLowPassFilter As Boolean = False)
                    
    If Setup("cmdFGEN.Square", "Square", "SQU", dFrequency, dAmplitude, dDCOffset, _
             sTriggerMode, sTriggerSource, bTrigSlopPositive, dTrigLevel, dIntTrigDelay, _
             dTrigDelay, iBurstCount, bActiveGateLevel, bSyncOutputOn, bSyncOutputWaveForm, _
             iTTLTrig, bTTLMarkerOutputOn, sTTLMarkerSource, dTTLMarkerSourceDelay, _
             iECLTrig, bECLMarkerOutputOn, b50ohmsOutputLoadRef, b20MhzLowPassFilter, _
             b25MhzLowPassFilter, b50MhzLowPassFilter) = False Then
        Exit Sub
    End If
    
    If iDutyCycle < 1 Or iDutyCycle > 99 Then
        Echo "FGEN PROGRAMMING ERROR:  Command cmdFGEN.Square iDutyCycle argument out of range."
        Err.Raise -1000
        Exit Sub
    End If
    
    'Program Instrument
    WriteMsg FGEN, ":SQU:DCYC " & CStr(iDutyCycle)
   
    'Turn on the output
    WriteMsg FGEN, ":OUTP ON"
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub

End Sub

Public Sub Sinc(dFrequency As Double, dAmplitude As Double, _
                    Optional dDCOffset As Double = 0, _
                    Optional iSinc As Integer = 10, _
                    Optional sTriggerMode As String = "CONTINUOUS", _
                    Optional sTriggerSource As String = "External", _
                    Optional bTrigSlopPositive As Boolean = True, _
                    Optional dTrigLevel As Double = 1.6, _
                    Optional dIntTrigDelay As Double = 0.0001, _
                    Optional dTrigDelay As Double = 0.0000001, _
                    Optional iBurstCount As Integer = 1, _
                    Optional bActiveGateLevel As Boolean = True, _
                    Optional bSyncOutputOn As Boolean = False, _
                    Optional bSyncOutputWaveForm As Boolean = True, _
                    Optional iTTLTrig As Integer = 0, _
                    Optional bTTLMarkerOutputOn As Boolean = False, _
                    Optional sTTLMarkerSource As String = "WAVEFORM", _
                    Optional dTTLMarkerSourceDelay As Double = 0.0001, _
                    Optional iECLTrig As Integer = 0, _
                    Optional bECLMarkerOutputOn As Boolean = False, _
                    Optional b50ohmsOutputLoadRef As Boolean = True, _
                    Optional b20MhzLowPassFilter As Boolean = False, _
                    Optional b25MhzLowPassFilter As Boolean = False, _
                    Optional b50MhzLowPassFilter As Boolean = False)
                    
    If Setup("cmdFGEN.Sinc", "Sinc", "SINC", dFrequency, dAmplitude, dDCOffset, _
             sTriggerMode, sTriggerSource, bTrigSlopPositive, dTrigLevel, dIntTrigDelay, _
             dTrigDelay, iBurstCount, bActiveGateLevel, bSyncOutputOn, bSyncOutputWaveForm, _
             iTTLTrig, bTTLMarkerOutputOn, sTTLMarkerSource, dTTLMarkerSourceDelay, _
             iECLTrig, bECLMarkerOutputOn, b50ohmsOutputLoadRef, b20MhzLowPassFilter, _
             b25MhzLowPassFilter, b50MhzLowPassFilter) = False Then
        Exit Sub
    End If
    
    If iSinc < 4 Or iSinc > 100 Then
        Echo "FGEN PROGRAMMING ERROR:  Command cmdFGEN.Square iSinc argument out of range."
        Err.Raise -1000
        Exit Sub
    End If
    
    'Program Instrument
    WriteMsg FGEN, ":SINC:NCYC " & CStr(iSinc)
    
    'Turn on the output
    WriteMsg FGEN, ":OUTP ON"
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub

End Sub

Public Sub Ramp(dFrequency As Double, dAmplitude As Double, _
                    Optional dDCOffset As Double = 0, _
                    Optional dDelay As Double = 10, _
                    Optional dLeadingEdge As Double = 10, Optional dTrailingEdge As Double = 10, _
                    Optional sTriggerMode As String = "CONTINUOUS", _
                    Optional sTriggerSource As String = "External", _
                    Optional bTrigSlopPositive As Boolean = True, _
                    Optional dTrigLevel As Double = 1.6, _
                    Optional dIntTrigDelay As Double = 0.0001, _
                    Optional dTrigDelay As Double = 0.0000001, _
                    Optional iBurstCount As Integer = 1, _
                    Optional bActiveGateLevel As Boolean = True, _
                    Optional bSyncOutputOn As Boolean = False, _
                    Optional bSyncOutputWaveForm As Boolean = True, _
                    Optional iTTLTrig As Integer = 0, _
                    Optional bTTLMarkerOutputOn As Boolean = False, _
                    Optional sTTLMarkerSource As String = "WAVEFORM", _
                    Optional dTTLMarkerSourceDelay As Double = 0.0001, _
                    Optional iECLTrig As Integer = 0, _
                    Optional bECLMarkerOutputOn As Boolean = False, _
                    Optional b50ohmsOutputLoadRef As Boolean = True, _
                    Optional b20MhzLowPassFilter As Boolean = False, _
                    Optional b25MhzLowPassFilter As Boolean = False, _
                    Optional b50MhzLowPassFilter As Boolean = False)
                    
    If Setup("cmdFGEN.Ramp", "Ramp", "RAMP", dFrequency, dAmplitude, dDCOffset, _
             sTriggerMode, sTriggerSource, bTrigSlopPositive, dTrigLevel, dIntTrigDelay, _
             dTrigDelay, iBurstCount, bActiveGateLevel, bSyncOutputOn, bSyncOutputWaveForm, _
             iTTLTrig, bTTLMarkerOutputOn, sTTLMarkerSource, dTTLMarkerSourceDelay, _
             iECLTrig, bECLMarkerOutputOn, b50ohmsOutputLoadRef, b20MhzLowPassFilter, _
             b25MhzLowPassFilter, b50MhzLowPassFilter) = False Then
        Exit Sub
    End If
    
    If dDelay < 0 Or dDelay > 69.9 Then
        Echo "FGEN PROGRAMMING ERROR:  Command cmdFGEN.Ramp dDelay argument out of range."
        Err.Raise -1000
        Exit Sub
    End If
    
    If dLeadingEdge < 0 Or dLeadingEdge > 69.9 Then
        Echo "FGEN PROGRAMMING ERROR:  Command cmdFGEN.Ramp dLeadingEdge argument out of range."
        Err.Raise -1000
        Exit Sub
    End If
    
    If dTrailingEdge < 0 Or dTrailingEdge > 69.9 Then
        Echo "FGEN PROGRAMMING ERROR:  Command cmdFGEN.Ramp dTrailingEdge argument out of range."
        Err.Raise -1000
        Exit Sub
    End If
    
    'Program Instrument
    WriteMsg FGEN, ":RAMP:DEL " & CStr(dDelay)
    WriteMsg FGEN, ":RAMP:TRAN " & CStr(dLeadingEdge)
    WriteMsg FGEN, ":RAMP:TRAN:TRA " & CStr(dTrailingEdge)
    
    'Turn on the output
    WriteMsg FGEN, ":OUTP ON"
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub

End Sub

Public Sub Pulse(dFrequency As Double, dAmplitude As Double, _
                    Optional dDCOffset As Double = 0, _
                    Optional dDelay As Double = 10, _
                    Optional dPulseWidth As Double = 10, _
                    Optional dLeadingEdge As Double = 10, Optional dTrailingEdge As Double = 10, _
                    Optional sTriggerMode As String = "CONTINUOUS", _
                    Optional sTriggerSource As String = "External", _
                    Optional bTrigSlopPositive As Boolean = True, _
                    Optional dTrigLevel As Double = 1.6, _
                    Optional dIntTrigDelay As Double = 0.0001, _
                    Optional dTrigDelay As Double = 0.0000001, _
                    Optional iBurstCount As Integer = 1, _
                    Optional bActiveGateLevel As Boolean = True, _
                    Optional bSyncOutputOn As Boolean = False, _
                    Optional bSyncOutputWaveForm As Boolean = True, _
                    Optional iTTLTrig As Integer = 0, _
                    Optional bTTLMarkerOutputOn As Boolean = False, _
                    Optional sTTLMarkerSource As String = "WAVEFORM", _
                    Optional dTTLMarkerSourceDelay As Double = 0.0001, _
                    Optional iECLTrig As Integer = 0, _
                    Optional bECLMarkerOutputOn As Boolean = False, _
                    Optional b50ohmsOutputLoadRef As Boolean = True, _
                    Optional b20MhzLowPassFilter As Boolean = False, _
                    Optional b25MhzLowPassFilter As Boolean = False, _
                    Optional b50MhzLowPassFilter As Boolean = False)
                    
    If Setup("cmdFGEN.Pulse", "Pulse", "PULS", dFrequency, dAmplitude, dDCOffset, _
             sTriggerMode, sTriggerSource, bTrigSlopPositive, dTrigLevel, dIntTrigDelay, _
             dTrigDelay, iBurstCount, bActiveGateLevel, bSyncOutputOn, bSyncOutputWaveForm, _
             iTTLTrig, bTTLMarkerOutputOn, sTTLMarkerSource, dTTLMarkerSourceDelay, _
             iECLTrig, bECLMarkerOutputOn, b50ohmsOutputLoadRef, b20MhzLowPassFilter, _
             b25MhzLowPassFilter, b50MhzLowPassFilter) = False Then
        Exit Sub
    End If
    
    If dDelay < 0 Or dDelay > 69.9 Then
        Echo "FGEN PROGRAMMING ERROR:  Command cmdFGEN.Pulse dDelay argument out of range."
        Err.Raise -1000
        Exit Sub
    End If
    
    If dPulseWidth < 0 Or dPulseWidth > 69.9 Then
        Echo "FGEN PROGRAMMING ERROR:  Command cmdFGEN.Pulse dPulseWidth argument out of range."
        Err.Raise -1000
        Exit Sub
    End If
    
    If dLeadingEdge < 0 Or dLeadingEdge > 69.9 Then
        Echo "FGEN PROGRAMMING ERROR:  Command cmdFGEN.Pulse dLeadingEdge argument out of range."
        Err.Raise -1000
        Exit Sub
    End If
    
    If dTrailingEdge < 0 Or dTrailingEdge > 69.9 Then
        Echo "FGEN PROGRAMMING ERROR:  Command cmdFGEN.Pulse dTrailingEdge argument out of range."
        Err.Raise -1000
        Exit Sub
    End If
    
    'Program Instrument
    WriteMsg FGEN, ":PULS:DEL " & CStr(dDelay)
    WriteMsg FGEN, ":PULS:WIDT " & CStr(dPulseWidth)
    WriteMsg FGEN, ":PULS:TRAN " & CStr(dLeadingEdge)
    WriteMsg FGEN, ":PULS:TRAN:TRA " & CStr(dTrailingEdge)
    
    'Turn on the output
    WriteMsg FGEN, ":OUTP ON"
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub

End Sub

Public Sub Exponential(dFrequency As Double, dAmplitude As Double, _
                    Optional dDCOffset As Double = 0, _
                    Optional iFunctionalExponent As Integer = -10, _
                    Optional sTriggerMode As String = "CONTINUOUS", _
                    Optional sTriggerSource As String = "External", _
                    Optional bTrigSlopPositive As Boolean = True, _
                    Optional dTrigLevel As Double = 1.6, _
                    Optional dIntTrigDelay As Double = 0.0001, _
                    Optional dTrigDelay As Double = 0.0000001, _
                    Optional iBurstCount As Integer = 1, _
                    Optional bActiveGateLevel As Boolean = True, _
                    Optional bSyncOutputOn As Boolean = False, _
                    Optional bSyncOutputWaveForm As Boolean = True, _
                    Optional iTTLTrig As Integer = 0, _
                    Optional bTTLMarkerOutputOn As Boolean = False, _
                    Optional sTTLMarkerSource As String = "WAVEFORM", _
                    Optional dTTLMarkerSourceDelay As Double = 0.0001, _
                    Optional iECLTrig As Integer = 0, _
                    Optional bECLMarkerOutputOn As Boolean = False, _
                    Optional b50ohmsOutputLoadRef As Boolean = True, _
                    Optional b20MhzLowPassFilter As Boolean = False, _
                    Optional b25MhzLowPassFilter As Boolean = False, _
                    Optional b50MhzLowPassFilter As Boolean = False)
                    
    If Setup("cmdFGEN.Exponential", "Exponential", "EXP", dFrequency, dAmplitude, dDCOffset, _
             sTriggerMode, sTriggerSource, bTrigSlopPositive, dTrigLevel, dIntTrigDelay, _
             dTrigDelay, iBurstCount, bActiveGateLevel, bSyncOutputOn, bSyncOutputWaveForm, _
             iTTLTrig, bTTLMarkerOutputOn, sTTLMarkerSource, dTTLMarkerSourceDelay, _
             iECLTrig, bECLMarkerOutputOn, b50ohmsOutputLoadRef, b20MhzLowPassFilter, _
             b25MhzLowPassFilter, b50MhzLowPassFilter) = False Then
        Exit Sub
    End If
    
    If iFunctionalExponent < -200 Or iFunctionalExponent > 200 Then
        Echo "FGEN PROGRAMMING ERROR:  Command cmdFGEN.Exponential ifunctionalexponent argument out of range."
        Err.Raise -1000
        Exit Sub
    End If
    
    'Program Instrument
    WriteMsg FGEN, ":EXP:EXP " & CStr(iFunctionalExponent)
    
    'Turn on the output
    WriteMsg FGEN, ":OUTP ON"
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub

End Sub

Public Sub Gaussian(dFrequency As Double, dAmplitude As Double, _
                    Optional dDCOffset As Double = 0, _
                    Optional iFunctionalExponent As Integer = 10, _
                    Optional sTriggerMode As String = "CONTINUOUS", _
                    Optional sTriggerSource As String = "External", _
                    Optional bTrigSlopPositive As Boolean = True, _
                    Optional dTrigLevel As Double = 1.6, _
                    Optional dIntTrigDelay As Double = 0.0001, _
                    Optional dTrigDelay As Double = 0.0000001, _
                    Optional iBurstCount As Integer = 1, _
                    Optional bActiveGateLevel As Boolean = True, _
                    Optional bSyncOutputOn As Boolean = False, _
                    Optional bSyncOutputWaveForm As Boolean = True, _
                    Optional iTTLTrig As Integer = 0, _
                    Optional bTTLMarkerOutputOn As Boolean = False, _
                    Optional sTTLMarkerSource As String = "WAVEFORM", _
                    Optional dTTLMarkerSourceDelay As Double = 0.0001, _
                    Optional iECLTrig As Integer = 0, _
                    Optional bECLMarkerOutputOn As Boolean = False, _
                    Optional b50ohmsOutputLoadRef As Boolean = True, _
                    Optional b20MhzLowPassFilter As Boolean = False, _
                    Optional b25MhzLowPassFilter As Boolean = False, _
                    Optional b50MhzLowPassFilter As Boolean = False)
                    
    If Setup("cmdFGEN.Gaussian", "Gaussian", "GAUS", dFrequency, dAmplitude, dDCOffset, _
             sTriggerMode, sTriggerSource, bTrigSlopPositive, dTrigLevel, dIntTrigDelay, _
             dTrigDelay, iBurstCount, bActiveGateLevel, bSyncOutputOn, bSyncOutputWaveForm, _
             iTTLTrig, bTTLMarkerOutputOn, sTTLMarkerSource, dTTLMarkerSourceDelay, _
             iECLTrig, bECLMarkerOutputOn, b50ohmsOutputLoadRef, b20MhzLowPassFilter, _
             b25MhzLowPassFilter, b50MhzLowPassFilter) = False Then
        Exit Sub
    End If
    
    If iFunctionalExponent < 1 Or iFunctionalExponent > 200 Then
        Echo "FGEN PROGRAMMING ERROR:  Command cmdFGEN.Gaussian iFunctionalExponent argument out of range."
        Err.Raise -1000
        Exit Sub
    End If
    
    'Program Instrument
    WriteMsg FGEN, ":GAUS:EXP " & CStr(iFunctionalExponent)
    
    'Turn on the output
    WriteMsg FGEN, ":OUTP ON"
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub

End Sub

Public Sub reset()
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    frmMain.txtInstrument.Text = "FGEN"
    frmMain.txtCommand.Text = "Reset"
    
    If Not bSimulation Then
        WriteMsg FGEN, "*CLS;*RST"
    End If

End Sub

Private Function Setup(sSubName As String, sSignalName As String, sSignalShape As String, _
                    dFrequency As Double, dAmplitude As Double, _
                    Optional dDCOffset As Double = 0, _
                    Optional sTriggerMode As String = "CONTINUOUS", _
                    Optional sTriggerSource As String = "External", _
                    Optional bTrigSlopPositive As Boolean = True, _
                    Optional dTrigLevel As Double = 1.6, _
                    Optional dIntTrigDelay As Double = 0.0001, _
                    Optional dTrigDelay As Double = 0.0000001, _
                    Optional iBurstCount As Integer = 1, _
                    Optional bActiveGateLevel As Boolean = True, _
                    Optional bSyncOutputOn As Boolean = False, _
                    Optional bSyncOutputWaveForm As Boolean = True, _
                    Optional iTTLTrig As Integer = 0, _
                    Optional bTTLMarkerOutputOn As Boolean = False, _
                    Optional sTTLMarkerSource As String = "WAVEFORM", _
                    Optional dTTLMarkerSourceDelay As Double = 0.0001, _
                    Optional iECLTrig As Integer = 0, _
                    Optional bECLMarkerOutputOn As Boolean = False, _
                    Optional b50ohmsOutputLoadRef As Boolean = True, _
                    Optional b20MhzLowPassFilter As Boolean = False, _
                    Optional b25MhzLowPassFilter As Boolean = False, _
                    Optional b50MhzLowPassFilter As Boolean = False) As Boolean
                    
    Dim sErrorMessage As String
    Dim xTrigDelay As Double
                    
    If UserEvent = ABORT_BUTTON Then
        Err.Raise USER_EVENT + ABORT_BUTTON:
        Setup = False
        Exit Function
    End If
    
    frmMain.txtInstrument.Text = "FGEN"
    frmMain.txtCommand.Text = sSignalName
    
    'Error Checking on Arguments
    If sSignalName <> "Sine" And sSignalName <> "Square" Then
    
        If dFrequency > 1000000# Or dFrequency < 0.0001 Then
            sErrorMessage = " dFrequency argument out of range."
            GoTo ExitFunction
        End If
    Else

        If dFrequency > 50000000# Or dFrequency < 0.0001 Then
            sErrorMessage = " dFrequency argument out of range."
            GoTo ExitFunction
        End If
    End If

    If Not b50ohmsOutputLoadRef Then
        dAmplitude = dAmplitude / 2
        dDCOffset = dDCOffset / 2
    End If
    
    If dAmplitude > 16 Or dAmplitude < 0.01 Then
        sErrorMessage = " dAmplitude argument out of range."
        GoTo ExitFunction
    End If
    
    Select Case dAmplitude
        Case 0.01 To 0.159
            If dDCOffset > (0.08 - (dAmplitude / 2)) Then
                sErrorMessage = " dDCOffSet argument out of range."
                GoTo ExitFunction
            End If
        Case 0.16 To 1.599
            If dDCOffset > (0.799 - (dAmplitude / 2)) Then
                sErrorMessage = " dDCOffSet argument out of range."
                GoTo ExitFunction
            End If
        Case 1.6 To 16
            If dDCOffset > (7.99 - (dAmplitude / 2)) Then
                sErrorMessage = " dDCOffSet argument out of range."
        GoTo ExitFunction
            End If
    End Select
        
    If UserEvent = ABORT_BUTTON Then
        Err.Raise USER_EVENT + ABORT_BUTTON:
        Setup = False
        Exit Function
    End If
    
    Select Case Left(UCase(sTriggerMode), 1)
        
        Case "C" ' Continuous
        Case "T", "B" ' Trigger, Burst
            
            Select Case Left(UCase(sTriggerSource), 3)
                
                Case "EXT", "INT"
                Case "TTL"
                    
                    Select Case CInt(Right(UCase(sTriggerSource), 1))
                        
                        Case 0 To 7
                        Case Else
                            sErrorMessage = " sTriggerSource argument out of range."
                            GoTo ExitFunction
                    End Select
                
                Case Else
                    sErrorMessage = " sTriggerSource argument out of range."
                    GoTo ExitFunction
            End Select
            
            If dTrigLevel < -10 Or dTrigLevel > 10 Then
                sErrorMessage = " dTrigLevel argument out of range."
                GoTo ExitFunction
            End If
            
            If dIntTrigDelay < 0.000015 Or dIntTrigDelay > 1000 Then
                sErrorMessage = " dInitTrigDelay argument out of range."
                GoTo ExitFunction
            End If
            
            If dTrigDelay < 0.0000001 Or dTrigDelay > 0.02 Then
                sErrorMessage = " dTrigDelay argument out of range."
                GoTo ExitFunction
            End If
            
            'fix xTrigDelay to be between 10 and 2,000,000
            xTrigDelay = dTrigDelay * dFrequency
            
            If xTrigDelay < 10 Then
              xTrigDelay = 10
            ElseIf xTrigDelay > 2000000 Then
              xTrigDelay = 2000000
            End If
            
            If Left(UCase(sTriggerMode), 1) = "B" Then ' Burst
               
               If iBurstCount < 1 Or iBurstCount > 1000000# Then
                    sErrorMessage = " iBurstCount argument out of range."
                    GoTo ExitFunction
               End If
            End If
            
        Case "G" ' Gate
            If dTrigLevel < -10 Or dTrigLevel > 10 Then
                sErrorMessage = " dTrigLevel argument out of range."
                GoTo ExitFunction
            End If
        
        Case Else
            sErrorMessage = " sTriggerMode argument out of range."
            GoTo ExitFunction
    End Select
    
    If iTTLTrig < 0 Or iTTLTrig > 7 Then
        sErrorMessage = " iTTLTrig argument out of range."
        GoTo ExitFunction
    End If
    
    If UserEvent = ABORT_BUTTON Then
        Err.Raise USER_EVENT + ABORT_BUTTON:
        Setup = False
        Exit Function
    End If
    
    Select Case Left(UCase(sTTLMarkerSource), 1)
        
        Case "W", "E" ' Bit, External
        Case "I"      ' Internal
            If dTTLMarkerSourceDelay < 0.000015 Or dTTLMarkerSourceDelay > 1000 Then
                sErrorMessage = " dTTLMarkerSourceDelay argument out of range."
                GoTo ExitFunction
            End If
        Case Else
            sErrorMessage = " sTTLMarkerSource argument out of range."
            GoTo ExitFunction
    End Select
     
    If iECLTrig < 0 Or iECLTrig > 1 Then
        sErrorMessage = " iECLTrig argument out of range."
        GoTo ExitFunction
    End If
    
    If bSimulation Then Exit Function

    If UserEvent = ABORT_BUTTON Then
        Err.Raise USER_EVENT + ABORT_BUTTON:
        Setup = False
        Exit Function
    End If
    
    'Program Instrument
    WriteMsg FGEN, ":OUTP 1"
    WriteMsg FGEN, ":FUNC:SHAP " & sSignalShape
    WriteMsg FGEN, ":FREQ " & CStr(dFrequency)
    WriteMsg FGEN, ":VOLT " & CStr(dAmplitude)
    WriteMsg FGEN, ":VOLT:OFFS " & CStr(dDCOffset)
    
    Select Case Left(UCase(sTriggerMode), 1)
        
        Case "C" ' Continuous
            WriteMsg FGEN, ":INIT:CONT ON"
        Case "T" ' Trigger
            WriteMsg FGEN, ":INIT:CONT OFF"
            WriteMsg FGEN, ":TRIG:BURS OFF"
            WriteMsg FGEN, ":TRIG:GATE OFF"
            
            Select Case Left(UCase(sTriggerSource), 3)
                
                Case "INT"
                    WriteMsg FGEN, ":TRIG:SOUR:ADV Internal"
                    WriteMsg FGEN, ":TRIG:TIM " & CStr(dIntTrigDelay)
                    WriteMsg FGEN, ":TRIG:DEL " & CStr(xTrigDelay)
                Case "EXT"
                    WriteMsg FGEN, ":TRIG:SOUR:ADV External"
                    If bTrigSlopPositive Then
                        WriteMsg FGEN, ":TRIG:SLOP POSITIVE"
                    Else
                        WriteMsg FGEN, ":TRIG:SLOP NEGATIVE"
                    End If
                    
                    WriteMsg FGEN, ":TRIG:LEV " & CStr(dTrigLevel)
                    WriteMsg FGEN, ":TRIG:DEL " & CStr(xTrigDelay)
                Case "TTL"
                    WriteMsg FGEN, ":TRIG:SOUR:ADV TTLTrg" & Right(UCase(sTriggerSource), 1)
                    WriteMsg FGEN, ":TRIG:DEL " & CStr(xTrigDelay)
            End Select
        
        Case "B" ' Burst
            WriteMsg FGEN, ":INIT:CONT OFF"
            WriteMsg FGEN, ":TRIG:GATE OFF"
            WriteMsg FGEN, ":TRIG:COUN " & CStr(iBurstCount)
            WriteMsg FGEN, ":TRIG:BURS ON"
            
            Select Case Left(UCase(sTriggerSource), 3)
                
                Case "INT"
                    WriteMsg FGEN, ":TRIG:SOUR:ADV Internal"
                    WriteMsg FGEN, ":TRIG:TIM " & CStr(dIntTrigDelay)
                    WriteMsg FGEN, ":TRIG:DEL " & CStr(xTrigDelay)
                Case "EXT"
                    WriteMsg FGEN, ":TRIG:SOUR:ADV External"
                    If bTrigSlopPositive Then
                        WriteMsg FGEN, ":TRIG:SLOP POSITIVE"
                    Else
                        WriteMsg FGEN, ":TRIG:SLOP NEGATIVE"
                    End If
                    WriteMsg FGEN, ":TRIG:LEV " & CStr(dTrigLevel)
                    WriteMsg FGEN, ":TRIG:DEL " & CStr(xTrigDelay)
                Case "TTL"
                    WriteMsg FGEN, ":TRIG:SOUR:ADV TTLTrg" & Right(UCase(sTriggerSource), 1)
                    WriteMsg FGEN, ":TRIG:DEL " & CStr(xTrigDelay)
            End Select
        
        Case "G" ' Gate
            WriteMsg FGEN, ":INIT:CONT OFF"
            WriteMsg FGEN, ":TRIG:BURS OFF"
            WriteMsg FGEN, ":TRIG:GATE ON"
            WriteMsg FGEN, ":TRIG:SOUR:ADV External"
            
            If bActiveGateLevel Then
                WriteMsg FGEN, ":TRIG:SLOP POSITIVE"
            Else
                WriteMsg FGEN, ":TRIG:SLOP NEGATIVE"
            End If
            
            WriteMsg FGEN, ":TRIG:LEV " & CStr(dTrigLevel)
   End Select
   
    If UserEvent = ABORT_BUTTON Then
        Err.Raise USER_EVENT + ABORT_BUTTON:
        Setup = False
        Exit Function
    End If
   
   If bSyncOutputWaveForm Then
        WriteMsg FGEN, ":OUTP:SYNC:SOUR BIT"
   Else
        WriteMsg FGEN, ":OUTP:SYNC:SOUR HCLock"
   End If
   
   If bSyncOutputOn Then
        WriteMsg FGEN, ":OUTP:SYNC ON"
   Else
        WriteMsg FGEN, ":OUTP:SYNC OFF"
   End If
   
    Select Case Left(UCase(sTTLMarkerSource), 1)
        
        Case "W"
            WriteMsg FGEN, ":OUTP:TRIG:SOUR BIT"
        Case "E"
            WriteMsg FGEN, ":OUTP:TRIG:SOUR EXTernal"
        Case "I"
            WriteMsg FGEN, ":OUTP:TRIG:SOUR INTernal"
            WriteMsg FGEN, ":TRIG:TIM " & CStr(dTTLMarkerSourceDelay)
    End Select
    
    If bTTLMarkerOutputOn Then
        WriteMsg FGEN, ":OUTP:TTLTrg" & CStr(iTTLTrig) & " ON"
    Else
        WriteMsg FGEN, ":OUTP:TTLTrg" & CStr(iTTLTrig) & " OFF"
    End If
    
    If bECLMarkerOutputOn Then
        WriteMsg FGEN, ":OUTP:ECLTrg" & CStr(iECLTrig) & " ON"
    Else
        WriteMsg FGEN, ":OUTP:ECLTrg" & CStr(iECLTrig) & " OFF"
    End If
    
    If b20MhzLowPassFilter Then
        WriteMsg FGEN, ":OUTP:FILT:FREQ 20MHz"
        WriteMsg FGEN, ":OUTP:FILT ON"
    End If
    
    If b25MhzLowPassFilter Then
        WriteMsg FGEN, ":OUTP:FILT:FREQ 25MHz"
        WriteMsg FGEN, ":OUTP:FILT ON"
    End If
    
    If b50MhzLowPassFilter Then
        WriteMsg FGEN, ":OUTP:FILT:FREQ 50MHz"
        WriteMsg FGEN, ":OUTP:FILT ON"
    End If
    
    If b20MhzLowPassFilter Or b25MhzLowPassFilter Or b50MhzLowPassFilter Then
        WriteMsg FGEN, ":OUTP:FILT ON"
    Else
        If sSignalName <> "Sine" Then
            WriteMsg FGEN, ":OUTP:FILT OFF"
        End If
    End If
    
    If UserEvent = ABORT_BUTTON Then
        Err.Raise USER_EVENT + ABORT_BUTTON:
        Setup = False
        Exit Function
    End If
    
    Setup = True
    
    Exit Function
    
ExitFunction:
    Echo "FGEN PROGRAMMING ERROR:  Command " & sSubName & sErrorMessage
    Err.Raise USER_EVENT + ABORT_BUTTON:
    Setup = False


End Function

