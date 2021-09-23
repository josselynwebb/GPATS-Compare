VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "cRFCounter"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
DefInt A-Z

'bbbb 4-15-2011
'changed all routines to run on ViperT RFcounter

'bbbb 5-23-2011
'Made the following changes in order to maintain compatability with tets
'created a new dRFMeasFreq2 with added parameter dMaxTimeOut
'created a new dRFMeasPulsePeriod2 with added parameters dMaxTimeOut,dTriglevel
'created a new dRFMeasPulseWidth2 with added parameters dMaxTimeOut,dTriglevel,APROBE

'bbbb 2012-03-07 STR10606 #12
'Made the following changes to dRFMeasFreq2 to use the RFmeas system to measure freq
'  under 100Mhz (10Mhz to 100Mhz) to match Tets Rfcounter.
'Tets can measure 10Mhz to 8.5Ghz where Viper can measure 100Mhz to 18Ghz.
'Both RFmeas, RFcounter use same path, should be transparent to user.

'bbbb 2012-03-08
'Added a Ref10Mhz reference On/Off/Ext command
'Vipert needs about 1 minute to lock onto the 10Mhz ref
'Needed to provide accuracy for cmdRFcounter.dRFMeasFreq command
'Not needed for Tets


Private ihDC&
Private instrumentHandle&
Private instrumentHandle2&
Private sDC_Type As String
Private nErr As Long
Private useRFctr As Integer


Const AUTO = 0
Const MANUAL = -1
Const DETOUT = 0
Const IFOUT = 1
Const NO_MODEL = -254
Const BUSY = -255

Public Sub SendMsg(sMsg As String)
    MsgBox "RFCTR SendMsg " + sMsg + " not implemented!", vbOKOnly
End Sub

Public Function sReadMsg(sMsg As String) As String
    MsgBox "RFCTR sReadMsg " + sMsg + " not implemented!", vbOKOnly
End Function

Public Sub Ref10Mhz(Optional OnOff As String = "ON")

  'OnOff = "ON", "EXT", or "OFF"
  ' Allow at least one minute for LO to lockon to the 10Mhz ref
                
   If OnOff = "ON" Then
     WriteMsg RFSYN, ":SOUR:ROSC:SOUR INT"
     WriteMsg RFSYN, ":OUTP:ROSC:STAT ON"
   ElseIf OnOff = "EXT" Then
     WriteMsg RFSYN, ":SOUR:ROSC:SOUR EXT"
     WriteMsg RFSYN, ":OUTP:ROSC:STAT ON"
   Else
     WriteMsg RFSYN, ":SOUR:ROSC:SOUR INT"
     WriteMsg RFSYN, ":OUTP:ROSC:STAT OFF"
   End If
       
End Sub

Public Function dRFMeasFreq(dExpectedFreq As Double, Optional dPowerRange As Double = 0, Optional APROBE As String = "OFF") As Double
                
       dRFMeasFreq = dRFMeasFreq2(dExpectedFreq, dPowerRange, APROBE)
       
End Function

Public Function dRFMeasFreq2(dExpectedFreq As Double, _
                Optional dPowerRange As Double = 0, _
                Optional APROBE As String = "OFF", _
                Optional dMaxTimeOut As Double = 10) As Double
    'DESCRIPTION:
    '   This function measures RF frequency.
    'PARAMETERS:
    '   dExpectedFreq:      expected frequency of the RF signal
    '   dPowerRange (opt):  RF signal's power level in dBm
    'RETURNS:
    '   measured RF frequency
    '   0 if there is no signal, or timeout
    
    '************************************************************************
    '2012-03-07 bb modified code to use the RFmeas system to measure freq
    '  under 100Mhz (10Mhz to 100Mhz) to match Tets Rfcounter
    'Tets can measure 10Mhz to 8.5Ghz where Viper can measure 100Mhz to 18Ghz
    '************************************************************************
    
    Dim sData As String
    Dim iAttn As Integer
    Dim RFC_MeasResult As Double
    
    Dim sCurrentMsg As String
    Dim lMaxTimeOut As Long
    
    Dim errorMessage$
    Dim InstrumentX&
    
    Dim MA_MeasResult As Variant
    MA_MeasResult = CDbl(0)
    Dim MA_Capture As Variant
    MA_Capture = CDbl(0)
    
    lMaxTimeOut = CLng(dMaxTimeOut)
    If lMaxTimeOut < 1 Then
      lMaxTimeOut = 1
    End If
    
    sCurrentMsg = frmMain.lblStatus.Caption
    If APROBE <> "OFF" Then
        frmMain.lblStatus.Caption = "APPLY PROBE"
    End If

    'check frequency range
    If (dExpectedFreq < 10000000# Or dExpectedFreq > 8500000000#) Then ' 100M to 8.5G
        Echo "EXPECTED FREQUENCY OUT OF RANGE (10Mhz to 8.5Ghz)"
        dRFMeasFreq2 = 0
        Exit Function
    ElseIf dExpectedFreq >= 100000000 Then ' use RFmeas for frequency less than 100Mhz
      useRFctr = True
    Else
      useRFctr = False
    End If

    'initialize RFcounter
    frmMain.txtInstrument.Text = "RFCTR"
    frmMain.lblStatus.Caption = "Init RFCTR"
    If Not bSimulation Then
        If useRFctr = True Then
          Call InitRFctr(dExpectedFreq, dPowerRange)
          nErr = gRFCt.Open(0) ' Open also performs a BIT operation
        Else
          Call InitRFmeas(dExpectedFreq, dPowerRange)
          nErr = gRFMa.Open(0) ' Open also performs a BIT operation
        End If
        If nErr <> 0 Then 'FAILED BIT
          Echo "RF Counter Built-In Test Failed"
          EncodeRFMSErrors errorMessage$, InstrumentX
        End If

       ' Allow extra measurement time for slow pulses
       If useRFctr = True Then
         nErr = gRFCt.setMaxTime(lMaxTimeOut)   ' default 10 seconds
       Else
         nErr = gRFMa.setMaxTime(lMaxTimeOut)   ' default 10 seconds
       End If
    End If
    frmMain.txtCommand.Text = "dRFMeasFreq"
    frmMain.lblStatus.Caption = "Meas Freq"

    If Not bSimulation Then
        Select Case Left(UCase(APROBE), 1)
          Case "O" ' Off
            'Make Measurement
            If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
            If useRFctr = True Then ' use RFctr 100 to 8500 MHZ
                nErr = gRFCt.setTrigSource(CLng(9)): checkRFCounterError (True) '9=immediate trigger mode
                nErr = gRFCt.setMeasureUnits("HZ"): checkRFCounterError (True)
                nErr = gRFCt.setExpRFFreq(dExpectedFreq, "HZ"): checkRFCounterError (True)
                nErr = gRFCt.setExpRFLevel(dPowerRange, "dBm"): checkRFCounterError (True)
                nErr = gRFCt.setTrigLevel(0, "Vrms"): checkRFCounterError (True)
                nErr = gRFCt.setMeasureMode(2): checkRFCounterError (True) 'RF freq
                nErr = gRFCt.setMaxTime(lMaxTimeOut): checkRFCounterError (True) 'default 10 second timeout
                Sleep 1000
                nErr = gRFCt.setExpRFFreq(dExpectedFreq, "HZ"): checkRFCounterError (True)
                Sleep 3 'ms
                nErr = gRFCt.getMeasurement(RFC_MeasResult, "HZ"): checkRFCounterError (True)
            
            Else 'use RFmeas 10 Mhz to 100 Mhz
                nErr = gRFMa.setTrigSource(9): checkRFMAError (True) ' 9=immediate trigger mode
                nErr = gRFMa.setCenterFreq(dExpectedFreq, "HZ"): checkRFMAError (True)
                nErr = gRFMa.setSpan(100000, "HZ"): checkRFMAError (True)
                nErr = gRFMa.setRFLevel(dPowerRange, "dBm"): checkRFMAError (True)
                If dExpectedFreq > 11000000 Then ' resolution band width
                  nErr = gRFMa.setRBW(100, "HZ"): checkRFMAError (True)
                Else
                  nErr = gRFMa.setRBW(1, "HZ"): checkRFMAError (True)
                End If
                nErr = gRFMa.setMeasureUnits("HZ"): checkRFMAError (True)
                nErr = gRFMa.setMeasSignalType(0): checkRFMAError (True) '0 = AC unmodulated
                nErr = gRFMa.setMaxTime(lMaxTimeOut): checkRFMAError (True) '10 sec timeout
                
                nErr = gRFMa.setCenterFreq(dExpectedFreq, "HZ"): checkRFMAError (True)
                nErr = gRFMa.setMeasureMode(0): checkRFMAError (True) 'capture a waveform
                nErr = gRFMa.getMeasurement(MA_Capture, "HZ"): checkRFMAError (True)
                nErr = gRFMa.setMeasureMode(6): checkRFMAError (True) 'measure frequency
                nErr = gRFMa.setMeasureUnits("HZ"): checkRFMAError (True)
                Sleep 1000
                MA_MeasResult = CDbl(0) ' get the measurement
                nErr = gRFMa.getMeasurement(MA_MeasResult, "HZ"): checkRFMAError (True)
                RFC_MeasResult = MA_MeasResult
            End If
            
            If nErr <> 0 Then
                'Echo "RF Counter: Timed Out"
                dRFMeasFreq2 = -1E-99
                frmMain.txtMeasured.Text = dRFMeasFreq2
                If (nErr = -1) Then
                    nErr = -1500    'raise as instrument error
                End If
                dRFMeasFreq2 = 9.9E+37: frmMain.txtMeasured.Text = 9.9E+37:
                Exit Function
            End If
             
            dRFMeasFreq2 = RFC_MeasResult
            frmMain.txtMeasured.Text = dRFMeasFreq2

          Case "S" ' Single, triggered
                If useRFctr = True Then
                    nErr = gRFCt.setTrigSource(9): checkRFCounterError (True) '9=immediate trigger mode
                    nErr = gRFCt.setMeasureUnits("HZ"): checkRFCounterError (True)
                    nErr = gRFCt.setExpRFFreq(dExpectedFreq, "HZ"): checkRFCounterError (True)
                    nErr = gRFCt.setExpRFLevel(dPowerRange, "dBm"): checkRFCounterError (True)
                    nErr = gRFCt.setTrigLevel(0, "Vrms"): checkRFCounterError (True)
                    nErr = gRFCt.setMeasureMode(2): checkRFCounterError (True) 'RF freq
                Else
                    nErr = gRFMa.setTrigSource(9): checkRFMAError (True) '9=immediate trigger mode
                    nErr = gRFMa.setCenterFreq(dExpectedFreq, "HZ"): checkRFMAError (True)
                    nErr = gRFMa.setSpan(100000, "HZ"): checkRFMAError (True)
                    nErr = gRFMa.setRFLevel(dPowerRange, "dBm"): checkRFMAError (True)
                    If dExpectedFreq > 11000000 Then
                      nErr = gRFMa.setRBW(100, "HZ"): checkRFMAError (True) 'resolution band width
                    Else
                      nErr = gRFMa.setRBW(1, "HZ"): checkRFMAError (True) 'resolution band width
                    End If
                    nErr = gRFMa.setMeasureUnits("HZ"): checkRFMAError (True)
                    nErr = gRFMa.setMeasSignalType(0): checkRFMAError (True) '0 = AC unmodulated
                    nErr = gRFMa.setMaxTime(lMaxTimeOut): checkRFMAError (True) '10 second timeout
                End If
                
                Sleep 1000
                MisProbe = vbYes
                Do While MisProbe = vbYes
                    Failed = False
                    UserEvent = 0
                    frmMain.TimerProbe.Enabled = True
                    frmMain.fraInstructions.Visible = True
                    If useRFctr = True Then
                        nErr = gRFCt.setExpRFFreq(dExpectedFreq, "HZ"): checkRFCounterError (True)
                        nErr = gRFCt.setMaxTime(lMaxTimeOut): checkRFCounterError (True)
                        Sleep 3 'ms
                        nErr = gRFCt.setMeasureMode(2): checkRFCounterError (True) 'measure RF freq
                        nErr = gRFCt.setMeasureUnits("HZ"): checkRFCounterError (True)
                    Else
                        nErr = gRFMa.setCenterFreq(dExpectedFreq, "HZ"): checkRFMAError (True)
                        nErr = gRFMa.setMeasureMode(0): checkRFMAError (True) 'capture a waveform
                        nErr = gRFMa.getMeasurement(MA_Capture, "HZ"): checkRFMAError (True)
                        nErr = gRFMa.setMeasureMode(6): checkRFMAError (True) 'measure frequency
                        nErr = gRFMa.setMeasureUnits("HZ"): checkRFMAError (True)
                        Sleep 1000
                        MA_MeasResult = CDbl(0) ' get the measurement
                        nErr = gRFMa.getMeasurement(MA_MeasResult, "HZ"): checkRFMAError (True)
                        RFC_MeasResult = MA_MeasResult
                    End If
                    Do While (UserEvent = 0) And (Not bProbeClosed) 'wait on probe button
                        DoEvents
                    Loop
                    frmMain.TimerProbe.Enabled = False
                    bProbeClosed = False
                    If useRFctr = True Then
                      nErr = gRFCt.getMeasurement(RFC_MeasResult, "HZ"): checkRFCounterError (True)
                    Else
                      MA_MeasResult = CDbl(0)
                      nErr = gRFMa.getMeasurement(MA_MeasResult, "HZ"): checkRFMAError (True)
                      RFC_MeasResult = MA_MeasResult
                    End If
                    
                    If nErr <> 0 Then
                        'Echo "RF Counter: Timed Out"
                        dRFMeasFreq2 = -1E-99
                        frmMain.txtMeasured.Text = dRFMeasFreq2
                        dRFMeasFreq2 = 9.9E+37: frmMain.txtMeasured.Text = 9.9E+37
                    Else
                      dRFMeasFreq2 = RFC_MeasResult
                      frmMain.txtMeasured.Text = dRFMeasFreq2
                    End If
                    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
                    If Failed = True Then
                        MisProbe = MsgBox("Analog Probe Measurement Failed. " & vbCrLf & _
                            "Do you wish to re-probe?", vbCritical + vbYesNo + vbSystemModal, "Analog Probe Failure")
                    Else
                        MisProbe = vbNo
                    End If
                Loop

          Case "C"    ' Continuous
                If useRFctr = True Then
                    nErr = gRFCt.setTrigSource(9): checkRFCounterError (True) '9=immediate trigger mode
                    nErr = gRFCt.setMeasureUnits("HZ"): checkRFCounterError (True)
                    nErr = gRFCt.setExpRFFreq(dExpectedFreq, "HZ"): checkRFCounterError (True)
                    nErr = gRFCt.setExpRFLevel(dPowerRange, "dBm"): checkRFCounterError (True)
                    nErr = gRFCt.setTrigLevel(0, "Vrms"): checkRFCounterError (True)
                    nErr = gRFCt.setMeasureMode(2): checkRFCounterError (True) 'RF freq
                Else
                    nErr = gRFMa.setTrigSource(9): checkRFMAError (True) '9=immediate trigger mode
                    nErr = gRFMa.setCenterFreq(dExpectedFreq, "HZ"): checkRFMAError (True)
                    nErr = gRFMa.setSpan(100000, "HZ"): checkRFMAError (True)
                    nErr = gRFMa.setRFLevel(dPowerRange, "dBm"): checkRFMAError (True)
                    If dExpectedFreq > 11000000 Then
                      nErr = gRFMa.setRBW(100, "HZ"): checkRFMAError (True) 'resolution band width
                    Else
                      nErr = gRFMa.setRBW(1, "HZ"): checkRFMAError (True) 'resolution band width
                    End If
                    nErr = gRFMa.setMeasureUnits("HZ"): checkRFMAError (True)
                    nErr = gRFMa.setMeasSignalType(0): checkRFMAError (True) '0 = AC unmodulated
                    nErr = gRFMa.setMaxTime(lMaxTimeOut): checkRFMAError (True) '10 second timeout
                End If
                
                Sleep 1000
                MisProbe = vbYes
                Do While MisProbe = vbYes
                    UserEvent = 0
                    Failed = False
                    frmMain.TimerProbe.Enabled = True
                    frmMain.fraInstructions.Visible = True
                    Do While (UserEvent = 0) And (Not bProbeClosed)
                       frmMain.TimerProbe.Enabled = True
                       frmMain.fraInstructions.Visible = True
                       If useRFctr = True Then
                           nErr = gRFCt.setExpRFFreq(dExpectedFreq, "HZ"): checkRFCounterError (True)
                           nErr = gRFCt.setMaxTime(lMaxTimeOut): checkRFCounterError (True)  ' timeout in seconds
                           Sleep 3 'ms
                           nErr = gRFCt.setMeasureMode(2): checkRFCounterError (True) 'measure RF freq
                           nErr = gRFCt.setMeasureUnits("HZ"): checkRFCounterError (True)
                       Else
                           nErr = gRFMa.setCenterFreq(dExpectedFreq, "HZ"): checkRFMAError (True)
                           nErr = gRFMa.setMeasureMode(0): checkRFMAError (True) 'capture a waveform
                           nErr = gRFMa.getMeasurement(MA_Capture, "HZ"): checkRFMAError (True)
                           nErr = gRFMa.setMeasureMode(6): checkRFMAError (True) 'measure frequency
                           nErr = gRFMa.setMeasureUnits("HZ"): checkRFMAError (True)
                           Sleep 1000
                       End If
                       frmMain.lblStatus.Caption = "Making Measurement ..."
                       If useRFctr = True Then
                           nErr = gRFCt.getMeasurement(RFC_MeasResult, "HZ"): checkRFCounterError (True)
                       Else
                           MA_MeasResult = CDbl(0)
                           nErr = gRFMa.getMeasurement(MA_MeasResult, "HZ"): checkRFMAError (True)
                           RFC_MeasResult = MA_MeasResult
                       End If
                       If nErr <> 0 Then
                            'Echo "RF Counter: Timed Out"
                            dRFMeasFreq2 = 9.9E+37
                            frmMain.txtMeasured.Text = dRFMeasFreq2
                            dRFMeasFreq2 = 9.9E+37: frmMain.txtMeasured.Text = 9.9E+37
                       Else
                          dRFMeasFreq2 = RFC_MeasResult
                          frmMain.txtMeasured.Text = dRFMeasFreq2
                       End If
                       LongDelay 1 'Do not remove this delay
                       If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
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
    Else 'simulation mode
      dRFMeasFreq2 = CDbl(InputBox("Command cmdRFCounter.dRFMeasFreq2 peformed." & vbCrLf & "Enter RF Frequency Value:", "SIMULATION MODE"))
      frmMain.txtMeasured.Text = dRFMeasFreq2
    End If

    If Not bSimulation Then
      If useRFctr = True Then
        nErr = gRFCt.reset
        nErr = gRFCt.Close
      Else
        nErr = gRFMa.reset
        nErr = gRFMa.Close
      End If
    End If
    Delay 1

    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
End Function

Public Function dRFMeasPulsePeriod(dExpectedPP As Double, dExpectedFreq As Double, Optional dPowerRange As Double = 0, Optional APROBE As String = "OFF") As Double
                
       dRFMeasPulsePeriod = dRFMeasPulsePeriod2(dExpectedPP, dExpectedFreq, dPowerRange, APROBE)
       
End Function

Public Function dRFMeasPulsePeriod2( _
                dExpectedPP As Double, _
                dExpectedFreq As Double, _
                Optional dPowerRange As Double = 0, _
                Optional APROBE As String = "OFF", _
                Optional dMaxTimeOut As Double = 10, _
                Optional dTrigLevel As Double = 0) As Double
    'DESCRIPTION:
    '   This function measures RF pulse period.
    'PARAMETERS:
    '   dExpectedPP:        expected pulse period of the RF signal
    '   dExpectedFreq:      expected frequency of the RF signal
    '   dPowerRange (opt):  RF signal's power level in dBm
    '   dmaxTimeOut (opt):  Test Timeout in seconds
    '   APROBE:             OFF  (immediate measurement)
    '                       ON   (waits on probe button to start)
    '                       CONT (waits on probe button to stop)
    'RETURNS:
    '   measured RF pulse period
    '   0 if there is no signal, or timeout
    
    
    Dim sData As String
    Dim iAttn As Integer
    Dim ReadValue As Double
    Dim sCurrentMsg As String
    Dim RFC_MeasResult As Double
    Dim errorMessage$
    Dim InstrumentX&
    
    Dim lMaxTimeOut
    
    lMaxTimeOut = CLng(dMaxTimeOut)
    If lMaxTimeOut < 1 Then
      lMaxTimeOut = 1
    End If
    
    sCurrentMsg = frmMain.lblStatus.Caption
    If APROBE <> "OFF" Then
        frmMain.lblStatus.Caption = "APPLY PROBE"
    End If

    'check frequency range
    If (dExpectedFreq < 100000000# Or dExpectedFreq > 8500000000#) Then
        Echo "EXPECTED FREQUENCY OUT OF RANGE (100Mhz to 8.5Ghz)"
        dRFMeasPulsePeriod2 = 0
        Exit Function
    End If

    'check pulse period range (0.25us to 1 second)
    If (dExpectedPP < 0.00000025) Then
        dExpectedPP = 0.00000025
    ElseIf (dExpectedPP > 1) Then
        dExpectedPP = 1
    End If
    
        'initialize RFcounter
    frmMain.txtInstrument.Text = "RFCTR"
    frmMain.lblStatus.Caption = "Init RFCTR"
    If Not bSimulation Then
        Call InitRFctr(dExpectedFreq, dPowerRange)
        nErr = gRFCt.Open(0) ' Open also performs a BIT operation
        If nErr <> 0 Then 'FAILED BIT
          Echo "RF Counter Built-In Test Failed"
          EncodeRFMSErrors errorMessage$, InstrumentX
        End If
       ' Allow extra measurement time for slow pulses
       nErr = gRFCt.setMaxTime(lMaxTimeOut)
    End If
    frmMain.txtCommand.Text = "dRFMeasPulseWidth"

    'select input attenuation per power range
    If sDC_Type = "EIP" Then    'EIP down converter
        If (dPowerRange < -40) Then
            iAttn = 0
        ElseIf (dPowerRange >= -40 And dPowerRange < -10) Then
            iAttn = 0
        ElseIf (dPowerRange > -30 And dPowerRange < 0) Then
            iAttn = 10
        ElseIf (dPowerRange > -20 And dPowerRange < 10) Then
            iAttn = 20
        ElseIf (dPowerRange > -10 And dPowerRange < 20) Then
            iAttn = 30
        ElseIf (dPowerRange > 0 And dPowerRange < 30) Then
            iAttn = 40
        Else
            iAttn = 40
        End If

    Else    'sDC_Type ="Gig"
        If (dPowerRange < -40) Then
            iAttn = 0
        ElseIf (dPowerRange >= -40 And dPowerRange < -10) Then
            iAttn = 0
        ElseIf (dPowerRange > -20 And dPowerRange < 10) Then
            iAttn = 20
        ElseIf (dPowerRange > -10 And dPowerRange < 20) Then
            iAttn = 30
        Else
            iAttn = 30
        End If
    End If

    'Make Measurement

    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    If Not bSimulation Then
        Select Case Left(UCase(APROBE), 1)
            Case "O" ' Off
                'Immediate Measurement
                If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
                nErr = gRFCt.setMaxTime(lMaxTimeOut): checkRFCounterError (True)   ' default 10 second timeout
                nErr = gRFCt.setTrigSource(9): checkRFCounterError (True) ' 9=immediate trigger mode
                nErr = gRFCt.setMeasureUnits("S"): checkRFCounterError (True)
                nErr = gRFCt.setExpRFFreq(dExpectedFreq, "Hz"): checkRFCounterError (True)
                nErr = gRFCt.setExpRFLevel(dPowerRange, "dBm"): checkRFCounterError (True)
                nErr = gRFCt.setTrigLevel(dTrigLevel, "Vrms"): checkRFCounterError (True)
                nErr = gRFCt.setMeasureMode(0): checkRFCounterError (True)          ' RF pulse period
                nErr = gRFCt.setMaxTime(lMaxTimeOut): checkRFCounterError (True)    ' timeout in seconds
                Sleep 1000
                nErr = gRFCt.setExpRFFreq(dExpectedFreq, "Hz"): checkRFCounterError (True)
                Sleep 3 'ms
                nErr = gRFCt.getMeasurement(RFC_MeasResult, "S"): checkRFCounterError (True)
                
                If nErr <> 0 Then
                    Echo "RF Counter: Timed Out"
                    dRFMeasPulsePeriod2 = -1E-99
                    frmMain.txtMeasured.Text = dRFMeasPulsePeriod2
                    If (nErr = -1) Then
                        nErr = -1500    'raise as instrument error
                    End If
                    dRFMeasPulsePeriod2 = 9.9E+37: frmMain.txtMeasured.Text = 9.9E+37: Err.Raise nErr
                    Exit Function
                End If
                dRFMeasPulsePeriod2 = RFC_MeasResult
                frmMain.txtMeasured.Text = dRFMeasPulsePeriod2

            Case "S" ' Single
                MisProbe = vbYes
                Do While MisProbe = vbYes
                    Failed = False
                    UserEvent = 0
                    frmMain.TimerProbe.Enabled = True: frmMain.fraInstructions.Visible = True
                    Do While (UserEvent = 0) And (Not bProbeClosed)
                        DoEvents
                    Loop
                    frmMain.TimerProbe.Enabled = False
                    bProbeClosed = False
                    
                    nErr = gRFCt.setMaxTime(lMaxTimeOut): checkRFCounterError (True)   ' default 10 second timeout
                    nErr = gRFCt.setTrigSource(9): checkRFCounterError (True) ' 9=immediate trigger mode
                    nErr = gRFCt.setMeasureUnits("S"): checkRFCounterError (True)
                    nErr = gRFCt.setExpRFFreq(dExpectedFreq, "Hz"): checkRFCounterError (True)
                    nErr = gRFCt.setExpRFLevel(dPowerRange, "dBm"): checkRFCounterError (True)
                    nErr = gRFCt.setTrigLevel(dTrigLevel, "Vrms"): checkRFCounterError (True)
                    nErr = gRFCt.setMeasureMode(0): checkRFCounterError (True)          ' RF pulse period
                    nErr = gRFCt.setMaxTime(lMaxTimeOut): checkRFCounterError (True)    ' timeout in seconds
                    Sleep 1000
                    nErr = gRFCt.setExpRFFreq(dExpectedFreq, "Hz"): checkRFCounterError (True)
                    Sleep 3 'ms
                    nErr = gRFCt.getMeasurement(RFC_MeasResult, "S"): checkRFCounterError (True)
                    
                    If nErr <> 0 Then
                        Echo "RF Counter: Timed Out"
                        dRFMeasPulsePeriod2 = -1E-99
                        frmMain.txtMeasured.Text = dRFMeasPulsePeriod2
                        If (nErr = -1) Then
                            nErr = -1500    'raise as instrument error
                        End If
                        dRFMeasPulsePeriod2 = 9.9E+37: frmMain.txtMeasured.Text = 9.9E+37: Err.Raise nErr
                        Exit Function
                    End If
                    dRFMeasPulsePeriod2 = RFC_MeasResult
                    frmMain.txtMeasured.Text = dRFMeasPulsePeriod2
                    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
                    If Failed = True Then
                        MisProbe = MsgBox("Analog Probe Measurement Failed. " & vbCrLf & _
                            "Do you wish to re-probe?", vbCritical + vbYesNo + vbSystemModal, "Analog Probe Failure")
                    Else
                        MisProbe = vbNo
                    End If
                Loop
            Case "C" ' Continuous not implemented
              MsgBox ("Continuous RFMeasPulsePeriod not implimented!")
              Exit Function

        End Select
    Else
        dRFMeasPulsePeriod2 = CDbl(InputBox("Command cmdDMM.dRFMeasFreq peformed." & vbCrLf & "Enter Value:", "SIMULATION MODE"))
        frmMain.txtMeasured.Text = dRFMeasPulsePeriod2
    End If
    If Not bSimulation Then
      nErr = gRFCt.Close
    End If
    Delay 1
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
End Function

Public Function dRFMeasPulseWidth(dExpectedPW As Double, dExpectedFreq As Double, Optional dPowerRange As Double = 0) As Double
                
       dRFMeasPulseWidth = dRFMeasPulseWidth2(dExpectedPW, dExpectedFreq, dPowerRange)
       
End Function

Public Function dRFMeasPulseWidth2( _
                dExpectedPW As Double, _
                dExpectedFreq As Double, _
                Optional dPowerRange As Double = 0, _
                Optional dMaxTimeOut As Double = 10, _
                Optional APROBE As String = "OFF", _
                Optional dTrigLevel As Double = 0) As Double
    'DESCRIPTION:
    '   This function measures RF pulse width.
    'PARAMETERS:
    '   dExpectedPW:        expected pulse width of the RF signal
    '   dExpectedFreq:      expected frequency of the RF signal
    '   dPowerRange (opt):  RF signal's power level in dBm
    'RETURNS:
    '   measured RF pulse width
    '   0 if there is no signal, or timeout
    
    
    Dim sData As String
    Dim iAttn As Integer
    Dim ReadValue As Double
    Dim sCurrentMsg As String
    Dim RFC_MeasResult As Double
    Dim errorMessage$
    Dim InstrumentX&
    
    Dim lMaxTimeOut
    
    lMaxTimeOut = CLng(dMaxTimeOut)
    If lMaxTimeOut < 1 Then
      lMaxTimeOut = 1
    End If
    
    sCurrentMsg = frmMain.lblStatus.Caption
    If UCase$(APROBE) <> "OFF" Then
        frmMain.lblStatus.Caption = "APPLY PROBE"
    End If

    'check frequency range
    If (dExpectedFreq < 100000000# Or dExpectedFreq > 8500000000#) Then
        Echo "EXPECTED FREQUENCY OUT OF RANGE (100Mhz to 8.5Ghz)"
        dRFMeasPulseWidth2 = 0
        Exit Function
    End If

    'check pulse width range, if wrong just fix it
    If (dExpectedPW < 0.00000005) Then
        dExpectedPW = 0.00000005
    ElseIf (dExpectedPW > 1) Then
        dExpectedPW = 1
    End If

    'initialize RFcounter
    frmMain.txtInstrument.Text = "RFCTR"
    frmMain.lblStatus.Caption = "Init RFCTR"
    If Not bSimulation Then
        Call InitRFctr(dExpectedFreq, dPowerRange)
        nErr = gRFCt.Open(0) ' Open also performs a BIT operation
        If nErr <> 0 Then 'FAILED BIT
          Echo "RF Counter Built-In Test Failed"
          EncodeRFMSErrors errorMessage$, InstrumentX
        End If
        ' Allow extra measurement time for slow pulses
        nErr = gRFCt.setMaxTime(lMaxTimeOut)
    End If
    frmMain.txtCommand.Text = "dRFMeasPulseWidth2"
    
    'select input attenuation per power range
    If sDC_Type = "EIP" Then    'EIP down converter
        If (dPowerRange < -40) Then
            iAttn = 0
        ElseIf (dPowerRange >= -40 And dPowerRange < -10) Then
            iAttn = 0
        ElseIf (dPowerRange > -30 And dPowerRange < 0) Then
            iAttn = 10
        ElseIf (dPowerRange > -20 And dPowerRange < 10) Then
            iAttn = 20
        ElseIf (dPowerRange > -10 And dPowerRange < 20) Then
            iAttn = 30
        ElseIf (dPowerRange > 0 And dPowerRange < 30) Then
            iAttn = 40
        Else
            iAttn = 40
        End If

    Else    'sDC_Type ="Gig"
        If (dPowerRange < -40) Then
            iAttn = 0
        ElseIf (dPowerRange >= -40 And dPowerRange < -10) Then
            iAttn = 0
        ElseIf (dPowerRange > -20 And dPowerRange < 10) Then
            iAttn = 20
        ElseIf (dPowerRange > -10 And dPowerRange < 20) Then
            iAttn = 30
        Else
            iAttn = 30
        End If

    End If

    'Make Measurement
    If Not bSimulation Then
        Select Case Left(UCase(APROBE), 1)
            Case "O" ' Off
                'Make Measurement
                If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
                
                nErr = gRFCt.setMaxTime(lMaxTimeOut): checkRFCounterError (True)   ' default 10 second timeout
                nErr = gRFCt.setTrigSource(9): checkRFCounterError (True) ' 9=immediate trigger mode
                nErr = gRFCt.setMeasureUnits("S"): checkRFCounterError (True)
                nErr = gRFCt.setExpRFFreq(dExpectedFreq, "Hz"): checkRFCounterError (True)
                nErr = gRFCt.setExpRFLevel(dPowerRange, "dBm"): checkRFCounterError (True)
                nErr = gRFCt.setTrigLevel(dTrigLevel, "Vrms"): checkRFCounterError (True)
                nErr = gRFCt.setMeasureMode(1): checkRFCounterError (True) ' RF pulse width
                nErr = gRFCt.setMaxTime(lMaxTimeOut): checkRFCounterError (True)    ' timeout in seconds
                Sleep 1000
                nErr = gRFCt.setExpRFFreq(dExpectedFreq, "Hz"): checkRFCounterError (True)
                Sleep 3 'ms
                nErr = gRFCt.getMeasurement(RFC_MeasResult, "S"): checkRFCounterError (True)
    
                If nErr <> 0 Then
                    'Echo "RF Counter: Timed Out"
                    dRFMeasPulseWidth2 = -1E-99
                    frmMain.txtMeasured.Text = dRFMeasPulseWidth2
                    If (nErr = -1) Then
                        nErr = -1500    'raise as instrument error
                    End If
                    dRFMeasPulseWidth2 = 9.9E+37: frmMain.txtMeasured.Text = 9.9E+37:
                    Exit Function
                End If
                dRFMeasPulseWidth2 = RFC_MeasResult
                frmMain.txtMeasured.Text = dRFMeasPulseWidth2


            Case "S" ' Single
                MisProbe = vbYes
                Do While MisProbe = vbYes
                    Failed = False
                    UserEvent = 0
                    frmMain.TimerProbe.Enabled = True: frmMain.fraInstructions.Visible = True
                    Do While (UserEvent = 0) And (Not bProbeClosed)
                        DoEvents
                    Loop
                    frmMain.TimerProbe.Enabled = False
                    bProbeClosed = False
                    
                    'Make Measurement
                    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
                    nErr = gRFCt.setMaxTime(lMaxTimeOut): checkRFCounterError (True)   ' default 10 second timeout
                    nErr = gRFCt.setTrigSource(9): checkRFCounterError (True) ' 9=immediate trigger mode
                    nErr = gRFCt.setMeasureUnits("S"): checkRFCounterError (True)
                    nErr = gRFCt.setExpRFFreq(dExpectedFreq, "Hz"): checkRFCounterError (True)
                    nErr = gRFCt.setExpRFLevel(dPowerRange, "dBm"): checkRFCounterError (True)
                    nErr = gRFCt.setTrigLevel(dTrigLevel, "Vrms"): checkRFCounterError (True)
                    nErr = gRFCt.setMeasureMode(1): checkRFCounterError (True) ' RF pulse width
                    nErr = gRFCt.setMaxTime(lMaxTimeOut): checkRFCounterError (True)    ' timeout in seconds
                    Sleep 1000
                    nErr = gRFCt.setExpRFFreq(dExpectedFreq, "Hz"): checkRFCounterError (True)
                    Sleep 3 'ms
                    nErr = gRFCt.getMeasurement(RFC_MeasResult, "S"): checkRFCounterError (True)
                    If nErr <> 0 Then
                        Echo "RF Counter: Timed Out"
                        dRFMeasPulseWidth2 = -1E-99
                        frmMain.txtMeasured.Text = dRFMeasPulseWidth2
                        If (nErr = -1) Then
                            nErr = -1500    'raise as instrument error
                        End If
                        dRFMeasPulseWidth2 = 9.9E+37: frmMain.txtMeasured.Text = 9.9E+37: Err.Raise nErr
                        Exit Function
                    End If
                    dRFMeasPulseWidth2 = ReadValue
                    frmMain.txtMeasured.Text = dRFMeasPulseWidth2
                    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
                    If Failed = True Then
                        MisProbe = MsgBox("Analog Probe Measurement Failed. " & vbCrLf & _
                            "Do you wish to re-probe?", vbCritical + vbYesNo + vbSystemModal, "Analog Probe Failure")
                    Else
                        MisProbe = vbNo
                    End If
                Loop

            Case "C" ' Continuous not implemented
              MsgBox ("Continuous RFMeasPulseWidth not implimented!")
              Exit Function
''
        End Select
    Else
      dRFMeasPulseWidth2 = CDbl(InputBox("Command cmdDMM.dRFMeasPulseWidth2 peformed." & vbCrLf & "Enter Value:", "SIMULATION MODE"))
      frmMain.txtMeasured.Text = dRFMeasPulseWidth2
    End If
    If Not bSimulation Then
      nErr = gRFCt.Close
    End If
    Delay 1
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    
    
End Function

Private Sub InitRFctr(dExpectedFreq As Double, Optional dPowerRange As Double = 0)
    
   'init RFcounter
    nErr = gRFCt.Close
    Sleep 100
    frmMain.txtCommand.Text = "init RFCTR"
    nErr = gRFCt.Open(0) ' Open also performs a BIT operation
    Sleep 3
    nErr = gRFCt.setTrigSource(9): checkRFCounterError (True) ' 9=immediate trigger mode
    nErr = gRFCt.setMeasureUnits("Hz"): checkRFCounterError (True)
    nErr = gRFCt.setExpRFFreq(100, "MHz"): checkRFCounterError (True)
    nErr = gRFCt.setExpRFLevel(CDbl(0), "dBm"): checkRFCounterError (True)
    nErr = gRFCt.setTrigLevel(0, "Vrms"): checkRFCounterError (True)
    nErr = gRFCt.setMeasureMode(2): checkRFCounterError (True) 'RF freq
    nErr = gRFCt.setMaxTime(10): checkRFCounterError (True)  ' 10 second timeout
    Sleep 1000
    nErr = gRFCt.Close
    Sleep 100
    If nErr Then
        MsgBox "ERROR: communicating to RFcounter!"
    End If
End Sub
    
Private Sub InitRFmeas(dExpectedFreq As Double, Optional dPowerRange As Double = 0)

   'init RFmeas
    nErr = gRFMa.Close
    Sleep 100
    frmMain.txtCommand.Text = "init RFMA"
    nErr = gRFMa.Open(0)  ' Open also performs a BIT operation
    Sleep 3
    nErr = gRFMa.reset: checkRFMAError (True)
    nErr = gRFMa.setTrigSource(9): checkRFMAError (True)    ' 9=immediate trigger mode
    nErr = gRFMa.setCenterFreq(dExpectedFreq, "HZ"): checkRFMAError (True)
    nErr = gRFMa.setSpan(100000, "HZ"): checkRFMAError (True)
    nErr = gRFMa.setRFLevel(dPowerRange, "dBm"): checkRFMAError (True)
    If dExpectedFreq > 11000000 Then
      nErr = gRFMa.setRBW(100, "HZ"): checkRFMAError (True)    ' resolution band width
    Else
      nErr = gRFMa.setRBW(1, "HZ"): checkRFMAError (True)    ' resolution band width
    End If
    nErr = gRFMa.setMeasureUnits("HZ"): checkRFMAError (True)
    nErr = gRFMa.setMeasSignalType(0): checkRFMAError (True)    '0 = AC unmodulated
    nErr = gRFMa.setMaxTime(10): checkRFMAError (True)     ' 10 second timeout
    Sleep 1000
    nErr = gRFMa.Close
    Sleep 100
    If nErr Then
        MsgBox "ERROR: communicating to RFmeas!"
    End If
  
End Sub



Private Sub ResetInstrument()

  MsgBox ("RFCounter.Reset not implemented!")
  
End Sub

Private Sub checkRFCounterError(EchoErrMsg As Integer)

    Dim Message As String
    Dim errorcode As Long
    Dim ErrorSeverity As Long
    Dim ErrorDescr As String
    Dim MoreErrorInfo As String

    If nErr <> 0 Then
        frmMain.txtCommand.Text = "getError"
        gRFCt.getError errorcode, ErrorSeverity, ErrorDescr, 256, MoreErrorInfo, 256
        Message = "Error:  " & errorcode & " --" & ErrorDescr & ". " & MoreErrorInfo
        If EchoErrMsg = True Then
          Echo Message
        End If
    End If
    
End Sub

Private Sub checkRFMAError(EchoErrMsg As Integer)

    Dim Message As String
    Dim errorcode As Long
    Dim ErrorSeverity As Long
    Dim ErrorDescr As String
    Dim MoreErrorInfo As String

    If nErr <> 0 Then
        frmMain.txtCommand.Text = "getError"
        gRFMa.getError errorcode, ErrorSeverity, ErrorDescr, 256, MoreErrorInfo, 256
        Message = "Error:  " & errorcode & " --" & ErrorDescr & ". " & MoreErrorInfo
     '   MsgBox message, vbCritical, "RMFS Error"
        If EchoErrMsg = True Then
          Echo Message
        End If
    End If
    
End Sub

Sub EncodeRFMSErrors(errorMessage$, InstrumentX&)

   ' This routine returns errorMessage$ and InstrumentX
   
    Dim retVal As Long
    Dim errorcode As Long
    Dim ErrorSeverity As Long
    Dim ErrorDescr$     'Dim ErrorDescr As String * 256 ?
    Dim MoreErrorInfo$  'Dim MoreErrorInfo As String * 256 ?
    Dim temp$
   
    'attach error codes to the instrument that caused the error
    frmMain.txtCommand.Text = "getError"
    gRFMa.getError errorcode, ErrorSeverity, ErrorDescr, 256, MoreErrorInfo, 256
    InstrumentX = 0
    errorMessage$ = "RFMS error:" + Str$(errorcode) + vbCrLf
    errorMessage$ = errorMessage$ + ErrorDescr + vbCrLf
    If MoreErrorInfo <> "" Then
      errorMessage$ = errorMessage$ + MoreErrorInfo + vbCrLf
    End If
    Select Case errorcode
      Case 13 To 17: 'find which instrument failed  (power failures)
        Select Case errorcode ' either PM1313B or PM20309 voltage errors
          Case 13: temp$ = "+10V"
          Case 14: temp$ = "+21V"
          Case 15: temp$ = "-21V"
          Case 16: temp$ = "-10V"
          Case 17: temp$ = "+5V"
        End Select
        errorMessage$ = errorMessage$ + "VXI Chassis " + temp$ + " Power Failure!" + vbCrLf
      Case 25, 26, 27 'PM1313B errors (Down converter)
        errorMessage$ = errorMessage$ + "PM1313B (Down Converter) Failed BIT!" + vbCrLf
        InstrumentX = DOWNCONV
      Case 28, 29, 30 'PM20309 errors (local oscillator)
        errorMessage$ = errorMessage$ + "PM20309 (Local Oscillator) Failed BIT!" + vbCrLf
        InstrumentX = LOCALOSC
      Case 34         'VM2601 digitizer error
        errorMessage$ = errorMessage$ + "VM2601 (Digitizer) Failed BIT!" + vbCrLf
        InstrumentX = DIGITIZER
      Case 41, 42     'VM7510 calibration module errors (same card as Digitizer VM2601))
        errorMessage$ = errorMessage$ + "VM7510 (Calibrator) Failed BIT!" + vbCrLf
        InstrumentX = CALIBRATOR
      Case Else
    End Select
  
End Sub




