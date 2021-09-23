VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "cRFPWRMETER"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
DefInt A-Z

'bbbb This module is almost a total rewrite for ViperT.
'bbbb 4-15-2011
'changed all routines to run on ViperT RF power meter

'bbbb 5-23-2011
'Made the following changes in order to maintain compatability with tets
'created a new dMeasPower2 with added parameter dFrequency

'bbbb 8-18-2011
'STR10606 #3 added parameter for SetExpFreq
'STR10606 #5 fixed problem in dMeasPower2 when in simulation mode



Private instrumentHandle&
Private sSensorName As String   'sensor name
Private sSensorFREQ As String   'sensor calibration freq data
Private sSensorCFAC As String   'sensor calibration CF% data
Private sSensorRCF As String    'sensor calibration ref. CF data
Private nErr As Long


Public Sub SendMsg(sMsg As String)
    MsgBox "RFPM SendMsg " + sMsg + " not implemented!", vbOKOnly
    
End Sub

Public Function sReadMsg(sMsg As String) As String
    MsgBox "RFPM ReadMsg not implemented!", vbOKOnly
End Function

Public Function dMeasPower(sAuto As String, _
                           Optional dOffSet As Double = 0, _
                           Optional dPowerRange As Double = -100, _
                           Optional sSensorType As String = "HP8481D", _
                           Optional sPowerUnit As String = "DBM", _
                           Optional bRFPowerRef As Boolean = False, _
                           Optional APROBE As String = "OFF", _
                           Optional bInstrReset As Boolean = False) As Double
                           
       dMeasPower = dMeasPower2(sAuto, dOffSet, dPowerRange, sSensorType, _
                              sPowerUnit, bRFPowerRef, APROBE, bInstrReset)
                              
End Function


Public Function dMeasPower2(sAuto As String, _
                           Optional dOffSet As Double = 0, _
                           Optional dPowerRange As Double = -100, _
                           Optional sSensorType As String = "HP8481D", _
                           Optional sPowerUnit As String = "DBM", _
                           Optional bRFPowerRef As Boolean = False, _
                           Optional APROBE As String = "OFF", _
                           Optional bInstrReset As Boolean = False, _
                           Optional dFrequency As Single = 1000000000) As Double
'DESCRIPTION:
'   This function measures RF power level in W or DBM.
'PARAMETERS:
'   sAuto:          "AUTO" or "ON" will use auto power range
'                   "OFF" will use dPowerRange value for power range
'   dOffSet:        pathloss (default to 0)
'   dPowerRange:    -70dBm to +44dBm or 100pW to 25W (sensor dependent)
'                   -100 is default range
'   sSensorType:    HP8481D or HP8481A (default HP8481D)
'   sPowerUnit:     selects the unit of power to be measured "DBM" or "W"
'                   factory default unit is "W" (Watts)
'   bRFPowerRef:    1mW, 50Mhz On (true) or Off *=(false), defaults Off
'   APROBE:         "OFF" normal measurement
'                   "S"   single measurement triggered by analog probe button
'                   "C"   continuous measurement sent to display
'   bInstrReset:    True (Reset first), False (No reset)
'bbbb
'   dFrequency:     50Mhz to 18Ghz, default 1Ghz' (used for power factor accuracy)
'
'RETURNS:
'   measured power level as double

    'create string data type
    Dim sData As String
    Dim sPowerRange As String
    Dim numI As Integer
    Dim sCurrentMsg As String
    Dim nMeasResults As Single
    Dim i

    sCurrentMsg = frmMain.lblStatus.Caption
    If APROBE <> "OFF" Then
        frmMain.lblStatus.Caption = "APPLY PROBE"
    End If
    
    sAuto = UCase$(sAuto) 'convert string data to upper case
    sPowerUnit = UCase$(sPowerUnit)
    dPowerRange = dPowerRange - dOffSet ' adjust for offset(path loss?)
    sPowerRange = CStr(dPowerRange) 'convert double data type to string data type
    
    'reset instrument if needed
    frmMain.txtInstrument.Text = "RFPM"
    frmMain.lblStatus.Caption = "Init RFPM"
    frmMain.txtCommand.Text = "dMeasPower2"
    If bInstrReset Then
      InitInstrument
    End If
    
    If Not bSimulation Then
      nErr = gRFPM.Close() 'end session
      nErr = gRFPM.Open(0) 'start a new session
      Delay 3
      If nErr <> 0 Then
        checkPMError (True) ' open session execution mode
        Echo "Error: Power Meter failed BIT tests on open statement.  Starting simulation mode."
        GoTo TestComplete ' no point in continuing
      End If
      nErr = gRFPM.setPwrMeterOn_Off(1): checkPMError (False) ' On
      
      'Select one of two power heads
      If sSensorType = "HP8481D" Then
        nErr = gRFPM.setPowerHead("8481D"): checkPMError (True)
      ElseIf sSensorType = "HP8481A" Then
        nErr = gRFPM.setPowerHead("8481A"): checkPMError (True)
      End If
    
      'Power Sensor reference on/off
      If bRFPowerRef Then
        nErr = gRFPM.setRefOsc(1): checkPMError (True) ' on
        nErr = gRFPM.setExpFreq(CDbl(50), "MHz"): checkPMError (True)
        Delay 5 'Allow Signal Settling Time
      Else
        nErr = gRFPM.setRefOsc(0): checkPMError (True) ' off
        nErr = gRFPM.setExpFreq(CDbl(dFrequency), "Hz"): checkPMError (True)
      End If
    
      'set power unit
      If (UCase(sPowerUnit) = "DBM") Then
        If Not bSimulation Then
           nErr = gRFPM.setMeasureMode(0): checkPMError (True) ' absolute Mode
           nErr = gRFPM.setMeasureUnits("dBm"): checkPMError (True)
        End If
 
        'select auto range option
        If (sAuto = "AUTO" Or sAuto = "ON") Then 'using AUTO range
            If Not bSimulation Then
              nErr = gRFPM.setAutoRange(): checkPMError (True)
              Delay 3
            End If
        ElseIf (sAuto = "OFF" And dPowerRange = -100) Then 'NOT using AUTO but invalid dPowerRange
          If Not bSimulation Then
            nErr = gRFPM.setAutoRange(): checkPMError (True)
            Delay 3
          End If
        ElseIf (sAuto = "OFF" And dPowerRange > -77 And dPowerRange < 44) Then
          'NOT using AUTO, specified power range
          'check power range per sensor type
          Select Case sSensorType
            Case "HP8481A"
                If (dPowerRange > -30 And dPowerRange < 20) Then
                    If Not bSimulation Then
                      Delay 3
                      nErr = gRFPM.setRangeLower(CSng(sPowerRange), "dBm"): checkPMError (True)
                      nErr = gRFPM.setRangeUpper(CSng(20), "dBm"): checkPMError (True)
                    End If
                Else
                    Echo "OUT OF RANGE FOR SENSOR TYPE: " & sSensorType
                    dMeasPower2 = 9.9E+37: frmMain.txtMeasured.Text = 9.9E+37: Err.Raise nErr
                    Exit Function
                End If

            Case "HP8481D"
                If (dPowerRange > -70 And dPowerRange < -20) Then
                    If Not bSimulation Then
                      nErr = gRFPM.setRangeLower(CSng(sPowerRange), "dBm"): checkPMError (True)
                      nErr = gRFPM.setRangeUpper(CSng(-20), "dBm"): checkPMError (True)
                    End If
                Else
                    Echo "OUT OF RANGE FOR SENSOR TYPE: " & sSensorType
                    dMeasPower2 = 9.9E+37: frmMain.txtMeasured.Text = 9.9E+37: Err.Raise nErr
                    Exit Function
                End If

            Case Else
                Echo "INVALID SENSOR TYPE"
                dMeasPower2 = 9.9E+37: frmMain.txtMeasured.Text = 9.9E+37: Err.Raise nErr
                Exit Function
           End Select
 
         Else
             'ERROR
             Echo "INVALID POWER RANGE"
             dMeasPower2 = 9.9E+37: frmMain.txtMeasured.Text = 9.9E+37: Err.Raise nErr
             Exit Function
         End If
       
      Else    'power unit in Watts
         sPowerUnit = "W"
        If Not bSimulation Then
          nErr = gRFPM.setMeasureMode(0): checkPMError (True) ' absolute Mode
          nErr = gRFPM.setMeasureUnits("W"): checkPMError (True)
        End If

        'select auto range option
        If (sAuto = "AUTO" Or sAuto = "ON") Then 'using AUTO range
            If Not bSimulation Then
              nErr = gRFPM.setAutoRange(): checkPMError (True)
              Delay 3
            End If

        ElseIf (sAuto = "OFF" And dPowerRange = -100) Then 'NOT using AUTO but invalid dPowerRange
            If Not bSimulation Then
              nErr = gRFPM.setAutoRange(): checkPMError (True)
              Delay 3
            End If

        ElseIf (sAuto = "OFF" And dPowerRange > 0.0000000001 And dPowerRange < 25) Then
          'NOT using AUTO, specified power range (Watts)
          'check power range per sensor type
          Select Case sSensorType
            Case "HP8481A"
              If (dPowerRange > 0.000001 And dPowerRange < 0.1) Then
                  If Not bSimulation Then
                    nErr = gRFPM.setRangeLower(CSng(sPowerRange), "W"): checkPMError (True)
                    nErr = gRFPM.setRangeUpper(CSng(0.1), "W"): checkPMError (True)
                  End If
              Else
                  Echo "OUT OF RANGE FOR SENSOR TYPE: " & sSensorType
                  dMeasPower2 = 9.9E+37: frmMain.txtMeasured.Text = 9.9E+37: Err.Raise nErr
                  Exit Function
              End If
            Case "HP8481D"
              If (dPowerRange > 0.0000000001 And dPowerRange < 0.00001) Then
                  nErr = gRFPM.setRangeLower(CSng(sPowerRange), "dBm"): checkPMError (True)
                  nErr = gRFPM.setRangeUpper(CSng(0.00001), "W"): checkPMError (True)
              Else
                  Echo "OUT OF RANGE FOR SENSOR TYPE: " & sSensorType
                  dMeasPower2 = 9.9E+37: frmMain.txtMeasured.Text = 9.9E+37: Err.Raise nErr
                  Exit Function
              End If
            Case Else
              Echo "INVALID SENSOR TYPE"
              dMeasPower2 = 9.9E+37: frmMain.txtMeasured.Text = 9.9E+37: Err.Raise nErr
              Exit Function
          End Select

        Else
          'ERROR
          Echo "INVALID POWER RANGE"
          dMeasPower2 = 9.9E+37: frmMain.txtMeasured.Text = 9.9E+37: Err.Raise nErr
          Exit Function
        End If
      End If
    End If

    'Make Measurement
    If Not bSimulation Then
        Select Case Left(UCase(APROBE), 1)
            Case "O" 'Aprobe OFF, immediate measure
                If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
                For i = 1 To 3
                  nErr = gRFPM.getMeasurement(nMeasResults, sPowerUnit): checkPMError (True)
                  If nErr = 0 Then Exit For
               Next i
            Case "S" 'Aprobe Sync Measurement, wait on probe
                frmMain.lblStatus.Caption = "Waiting for Probe ..."
                If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function

                'measure, then fetch result
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
                    frmMain.lblStatus.Caption = "Making Measurement ..."

                    'select auto range option
                    If (sAuto = "AUTO" Or sAuto = "ON") Then 'using AUTO range
                        nErr = gRFPM.setAutoRange(): checkPMError (True)
                        Delay 3
                    End If

                    'measure, and fetch result
                    For i = 1 To 3
                      nErr = gRFPM.getMeasurement(nMeasResults, sPowerUnit)
                      checkPMError (True)
                      If nErr = 0 Then Exit For
                    Next i

                    dMeasPower2 = CDbl(nMeasResults)
                    frmMain.txtMeasured.Text = dMeasPower2 + dOffSet
                    frmMain.lblStatus.Caption = "Measurement Complete ..."
                    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
                    If Failed = True Then
                        MisProbe = MsgBox("Analog Probe Measurement Failed. " & vbCrLf & _
                            "Do you wish to re-probe?", vbCritical + vbYesNo + vbSystemModal, "Analog Probe Failure")
                    Else
                        MisProbe = vbNo
                    End If
                Loop

            Case "C" 'Aprobe continuous Measurement

                If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
                'measure, then fetch result
                MisProbe = vbYes
                frmMain.TimerProbe.Enabled = True: frmMain.fraInstructions.Visible = True
                Do While MisProbe = vbYes
                    UserEvent = 0
                    Failed = False
                    Do While (UserEvent = 0) And (Not bProbeClosed)
                        frmMain.lblStatus.Caption = "Making Measurement ..."

                        'select auto range option
                        If (sAuto = "AUTO" Or sAuto = "ON") Then 'using AUTO range
                            nErr = gRFPM.setAutoRange(): checkPMError (True)
                            Delay 3
                        End If

                        'measure, and fetch result
                        nErr = gRFPM.getMeasurement(nMeasResults, sPowerUnit)
                        checkPMError (True)
                        dMeasPower2 = CDbl(nMeasResults)
                        frmMain.txtMeasured.Text = dMeasPower2 + dOffSet
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
    End If

OFF_SET:

    If Not bSimulation Then
        dMeasPower2 = CDbl(nMeasResults) + dOffSet
    Else
        dMeasPower2 = CDbl(InputBox("Command cmdRFPM.dMeasPower2 peformed." & vbCrLf & "Enter Power value:", "SIMULATION MODE"))
    End If
    frmMain.txtMeasured.Text = dMeasPower2

    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function

    frmMain.lblStatus.Caption = sCurrentMsg
    
TestComplete:
    If Not bSimulation Then
      nErr = gRFPM.reset(): checkPMError (False)
      nErr = gRFPM.setRefOsc(0): checkPMError (False) 'off
      nErr = gRFPM.setPwrMeterOn_Off(0): checkPMError (False) 'off
      nErr = gRFPM.Close() ' end session
    End If
    
End Function

Public Function bZeroSet() As Boolean
'DESCRIPTION:
'       This function perform zero calibration.
'Returns:
'       True if successful
'       False if failed
    
    If bSimulation Then
        bZeroSet = True
        Exit Function
    End If

    bZeroSet = False

    'display message to disconnect power sensor from signal sources
    frmMain.txtInstrument.Text = "RFPM"
    frmMain.lblStatus.Caption = "Zero RFPM"
    If MsgBox("This routine will take approximately 30 seconds." & vbCrLf & _
              "Please disconnect the Power Sensor from any power source.", _
              vbOKCancel + vbInformation, "Performing Zero Set") = vbOK Then

        'perform zero cal
        frmMain.txtCommand.Text = "doZero"
        nErr = gRFPM.doZero(): checkPMError (False)

        'allow 30 seconds to perform zero cal
        LongDelay (30)

        'zero cal complete
        bZeroSet = True
    End If
      
End Function

Public Function bCalPM(sSensorType As String) As Boolean
'DESCRIPTION:
'       This function performs ALL calibrations including zero and power meter calibration.
'Parameters:
'       sSensorType:    "HP8481A" or "HP8481D"
'Returns:
'       True if successful
'       False if failed


    'delcare local variables
    Dim T As Single
    Dim sData As String
    Dim dTime As Double
    Dim bSensorInTxt As Boolean
    Dim calstat As Long
    Dim i As Integer

    If bSimulation Then
        bCalPM = True
        Exit Function
    End If

    bCalPM = False
    bSensorInTxt = False
    sSensorType = UCase(sSensorType)

    'init RFPM
    frmMain.txtInstrument.Text = "RFPM"
    frmMain.lblStatus.Caption = "Cal RFPM"
    frmMain.txtCommand.Text = "bCalPM"
    InitInstrument
    If Not bSimulation Then
      Echo "Calibrating " & sSensorType
      nErr = gRFPM.Open(0) 'start a new session
      Delay 3
      If nErr <> 0 Then
        checkPMError (True) ' open session execution mode
        Echo "Error: Power Meter failed BIT tests on open statement.  Starting simulation mode."
        Exit Function ' no point in continuing
      End If
      nErr = gRFPM.setPwrMeterOn_Off(1): checkPMError (False) ' On
    End If


    dTime = Timer

   'power meter cal
   If sSensorType = "HP8481A" Then
     nErr = gRFPM.setPowerHead("8481A"): checkPMError (True)
   Else
      nErr = gRFPM.setPowerHead("8481D"): checkPMError (True)
   End If
        
   nErr = gRFPM.setMeasureMode(0): checkPMError (True)
   nErr = gRFPM.setMeasureUnits("W"): checkPMError (True)
   Sleep 3000
   nErr = gRFPM.setPwrMeterOn_Off(1): checkPMError (True) ' turns on power meter
   Sleep 5000
    
   frmMain.txtCommand.Text = "doZeroAndCal"
   nErr = gRFPM.doZeroAndCal
   frmMain.lblStatus.Caption = "Busy"
   Sleep 1000
   
   If nErr = 0 Then
      For i = 1 To 100
          nErr = gRFPM.getZeroCalStatus(calstat)
            
          If calstat = 0 Or nErr <> 0 Then
              frmMain.lblStatus.Caption = "Ready"
              Exit For
          End If
            
          DoEvents
          
          If UserEvent = ABORT_BUTTON Then
              Err.Raise USER_EVENT + ABORT_BUTTON:
              Exit Function
          End If
            
          Sleep 500
      Next i
   End If
        
   If (i > 100) Then
       MsgBox sSensorType & " Calibration Failed! Timeout", vbCritical, "Calibrate All"
       frmMain.txtMeasured.Text = 0
   ElseIf nErr <> 0 Then
       checkPMError (True)
       frmMain.txtMeasured.Text = 0
   Else    ' passed calibration
       bCalPM = True
       frmMain.txtMeasured.Text = 1
   End If
    
End Function

Private Function StringToList(sStr As String, iLower As Integer, List$(), sDelimiter As String) As Integer
   MsgBox ("UStringToList not implemented!")

End Function

Private Sub UpdateDataTable(sSensorType As String)
    

End Sub

Private Sub InitInstrument()

    If Not bSimulation Then
        DoEvents
        frmMain.txtCommand.Text = "init PM"
        nErr = gRFPM.Close() ' end session
        nErr = gRFPM.Open(0) ' start a new session
        nErr = gRFPM.reset(): checkPMError (False)
        nErr = gRFPM.setRefOsc(0): checkPMError (False)
        nErr = gRFPM.setPwrMeterOn_Off(0): checkPMError (False) 'Off
        nErr = gRFPM.Close() ' end session
        If nErr < 0 Then
            Echo "RFPM ERROR: " & nErr
            MsgBox "UNABLE TO INITIALIZE RF POWER METER"
            Exit Sub
        End If
    End If
  
End Sub

Private Function PMReadMsg(instrumentHandle As Long, Msg$, Optional TimeOutSeconds As Double = 11) As Long
   MsgBox ("PMReadMsg not implemented!")
    
End Function

Private Sub checkPMError(EchoErrMsg As Integer)
    Dim errorcode As Long
    Dim ErrorSeverity As Long
    Dim ErrorDescr As String
    Dim MoreErrorInfo As String
    Dim S$

    If nErr <> 0 Then
      frmMain.txtCommand.Text = "getError"
      nErr = gRFPM.getError(errorcode, ErrorSeverity, ErrorDescr, 256, MoreErrorInfo, 256)
      S$ = "Error:  " & errorcode & " --" & ErrorDescr & ". " & vbCrLf & MoreErrorInfo & " "
      If nErr <> 0 Then
        nErr = gRFPM.getError(errorcode, ErrorSeverity, ErrorDescr, 256, MoreErrorInfo, 256)
        S$ = S$ + vbCrLf + "Error:  " & errorcode & " --" & ErrorDescr & ". " & vbCrLf & MoreErrorInfo & " "
      End If
      If EchoErrMsg = True Then
        Echo S$
      End If
    End If
    
End Sub



