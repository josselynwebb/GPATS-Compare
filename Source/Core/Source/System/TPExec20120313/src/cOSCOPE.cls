VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "cOSCOPE"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
DefInt A-Z

'bbbb 12-19-2011 STR10606 #8
'Added AcqWaveform()
'Added sGetDsoBlock()

'bbbb 2012-03-07 STR10606 #9
'Added loop in Aprobe routine to wait on Probe button release

' M.Keyler 8-25-2011
' Added the following functions:
' 1. Scope_Setup_Initiate - Sets up the scope for triggering.
' 2. Query - Reads the scope buffer for data.
' The reason for this is that currently the ArmVpp and ArmVpp2 functions arm the scope
' and then immediately take a reading.  The need exists, where it is necessary to arm the
' scope, perfrom an action, then read the data from the buffer.
' sample usage in *.bas file
' dMeasured = cmdOscope.AcqWaveform(1, DataArray())
' dMeasured = cmdOscope.AcqWaveform(1, DataArray(), 500, 0.016, 5, 2.5, 1.4, "POS", "CHAN1", 0.001, 50, "DC", "OFF", "OFF")
    
Public Enum InputSelect
    INPUT_OFF = 0
    INPUT_ON = 1
End Enum

Public Enum MAN_OR_AUTO
    MANUAL_SETUP = 0
    AUTO_SETUP = 1
End Enum

Public Enum InputSource
    INPUT1 = 1
    INPUT2 = 2
    INPUT1_AND_INPUT2 = 3
    WMEM1 = 4
    WMEM2 = 5
    WMEM3 = 6
    WMEM4 = 7
End Enum

Public Enum TriggerSlope
    POS = 0
    NEG = 1
End Enum

Public Enum TriggerSource
    TRG_SRC_INPUT1 = 0
    TRG_SRC_INPUT2 = 1
    TRG_SRC_EXTERNAL = 2
    TRG_SRC_ECLT0 = 3
    TRG_SRC_ECLT1 = 4
End Enum

Public Enum InputImpedance
    OHM_50 = 0
    OHM_1M = 1
End Enum

Public Enum LowPass
    LP_ACTIVE = 0
    LP_INACTIVE = 1
End Enum

Public Enum HighPass
    HP_ACTIVE = 0
    HP_INACTIVE = 1
End Enum

Public Enum Hysteresis
    HYST_ACTIVE = 0
    HYST_INACTIVE = 1
End Enum

Public Enum InputCoupling
    AC = 0
    DC = 1
End Enum

Public Enum ATE_PROBE
    PROBE_OFF = 0
    PROBE_SINGLE_SHOT = 1
    PROBE_CONTINUOUS = 2
End Enum

Public Enum ScopeSamples
    SAMPLES_500 = 0
    SAMPLES_8000 = 1
End Enum

Public Enum TimeMode
    TRIG_NORMAL = 0
    TRIG_SINGLE_SHOT = 1
    TRIG_CONTINUOUS = 2
End Enum

Public Enum MeasurementFunctionLayer
    SIGNAL_PERIOD = 0
    SIGNAL_FREQUENCY = 1
    SIGNAL_AMPLITUDE = 2
    SIGNAL_AC = 3
    SIGNAL_DC = 4 ' error
    SIGNAL_DUTY_CYCLE = 5
    SIGNAL_FALL_OVERSHOOT = 6
    SIGNAL_FALL_PRESHOOT = 7
    SIGNAL_FALL_TIME = 8
    SIGNAL_FTIME = 9
    SIGNAL_HIGH_VOLT = 10
    SIGNAL_LOW_VOLT = 11
    SIGNAL_MAX_VOLT = 12
    SIGNAL_MIN_VOLT = 13
    SIGNAL_NEG_DUTY = 14
    SIGNAL_NEG_PULSE_WIDTH = 15
    SIGNAL_POS_DUTY = 16
    SIGNAL_POS_PULSE_WIDTH = 17
    SIGNAL_RISE_OVERSHOOT = 18
    SIGNAL_RISE_PRESHOOT = 19
    SIGNAL_RISE_TIME = 20
    SIGNAL_RTIME = 21
    SIGNAL_TIME_AT_MAX_VOLTAGE = 22
    SIGNAL_TIME_AT_MIN_VOLTAGE = 23
    SIGNAL_BUFFER_DATA = 24
    LAST_FUNCTION = SIGNAL_BUFFER_DATA + 1
End Enum

Public Enum TimeDelayLink
    TRIGGER_IN_CENTER = 1
    TRIGGER_IN_BEGINNING = 2
    TRIGGER_IN_END = 3
End Enum

'Define Scope variable
' ** Waveform is a user defined type which can be used in conjunction with
'    the UnpackWaveform command.  The time of each measurement point can be
'    calculated with respect to the trigger can be calculated with the following
'    formula:  [(data point number - reference) x Interval] + Origin
'    E.g.
'                               Dim SinWave As Waveform
Type WaveForm
    Points          As Integer
    WFType          As Integer
    Resolution      As Integer
    Magnitude()     As Single
    XInterval       As Double
    xOrigin         As Double
    xReference      As Integer
    XUnits          As String
    YInterval       As Double
    yOrigin         As Double
    yReference      As Integer
    YUnits          As String
End Type

Public WaveFormPoints As Integer

Const DATA_FORMAT = 1
Const WAVE_FORM_TYPE = 2
Const DATA_POINTS = 3
Const X_INCREMENT = 5
Const XORIGN = 6
Const XREFERENC = 7
Const Y_INCREMENT = 8
Const YORIGN = 9
Const YREFERENC = 10

Const SIXTEEN_BITS = 2

Dim blnScopeSetupFirstTime As Boolean
Dim PreambleList$(10)
Dim sMeasurementFunction(LAST_FUNCTION) As String

' M.Keyler 8-25-2011
' Added the following functions:
' 1. Scope_Setup_Initiate - Sets up the scope for triggering.
' 2. Query - Reads the scope buffer for data.
' The reason for this is that currently the ArmVpp and ArmVpp2 functions arm the scope
' and then immediately take a reading.  The need exists, where it is necessary to arm the
' scope, perfrom an action, then read the data from the buffer.
'  ******************************************************************
'  **                       Sub Class_Initialize
'  **
'  **  Parameters
'  **    none
'  **
'  **  Returns
'  **    none
'  **
'  **  Description
'  **    Initializes the oSCOPE object.
'  ******************************************************************
Private Sub Class_Initialize()
 
    ' Initialize the array that holds SCPI commands to perform various scope measurements
    sMeasurementFunction(MeasurementFunctionLayer.SIGNAL_AC) = "MEAS:VOLT:AC? (@WMEM"
    sMeasurementFunction(MeasurementFunctionLayer.SIGNAL_AMPLITUDE) = "MEAS:VOLT:AMPL? (@WMEM"
    sMeasurementFunction(MeasurementFunctionLayer.SIGNAL_DC) = "MEAS:VOLT:DC? (@WMEM"
    sMeasurementFunction(MeasurementFunctionLayer.SIGNAL_DUTY_CYCLE) = "MEAS:VOLT:DCYC? (@WMEM"
    sMeasurementFunction(MeasurementFunctionLayer.SIGNAL_FALL_OVERSHOOT) = "MEAS:VOLT:FALL:OVER? (@WMEM"
    sMeasurementFunction(MeasurementFunctionLayer.SIGNAL_FALL_PRESHOOT) = "MEAS:VOLT:FALL:PRES? (@WMEM"
    sMeasurementFunction(MeasurementFunctionLayer.SIGNAL_FALL_TIME) = "MEAS:VOLT:FALL:TIME? (@WMEM"
    sMeasurementFunction(MeasurementFunctionLayer.SIGNAL_FREQUENCY) = "MEAS:VOLT:FREQ? (@WMEM"
    sMeasurementFunction(MeasurementFunctionLayer.SIGNAL_FTIME) = "MEAS:VOLT:FTIM? (@WMEM"
    sMeasurementFunction(MeasurementFunctionLayer.SIGNAL_HIGH_VOLT) = "MEAS:VOLT:HIGH? (@WMEM"
    sMeasurementFunction(MeasurementFunctionLayer.SIGNAL_LOW_VOLT) = "MEAS:VOLT:LOW? (@WMEM"
    sMeasurementFunction(MeasurementFunctionLayer.SIGNAL_MAX_VOLT) = "MEAS:VOLT:MAX? (@WMEM"
    sMeasurementFunction(MeasurementFunctionLayer.SIGNAL_MIN_VOLT) = "MEAS:VOLT:MIN? (@WMEM"
    sMeasurementFunction(MeasurementFunctionLayer.SIGNAL_NEG_DUTY) = "MEAS:VOLT:NDUT? (@WMEM"
    sMeasurementFunction(MeasurementFunctionLayer.SIGNAL_NEG_PULSE_WIDTH) = "MEAS:VOLT:NWID? (@WMEM"
    sMeasurementFunction(MeasurementFunctionLayer.SIGNAL_PERIOD) = "MEAS:VOLT:PER? (@WMEM"
    sMeasurementFunction(MeasurementFunctionLayer.SIGNAL_POS_DUTY) = "MEAS:VOLT:PDUT? (@WMEM"
    sMeasurementFunction(MeasurementFunctionLayer.SIGNAL_POS_PULSE_WIDTH) = "MEAS:VOLT:PWID? (@WMEM"
    sMeasurementFunction(MeasurementFunctionLayer.SIGNAL_RISE_OVERSHOOT) = "MEAS:VOLT:RISE:OVER? (@WMEM"
    sMeasurementFunction(MeasurementFunctionLayer.SIGNAL_RISE_PRESHOOT) = "MEAS:VOLT:RISE:PRES? (@WMEM"
    sMeasurementFunction(MeasurementFunctionLayer.SIGNAL_RISE_TIME) = "MEAS:VOLT:RISE:TIME? (@WMEM"
    sMeasurementFunction(MeasurementFunctionLayer.SIGNAL_RTIME) = "MEAS:VOLT:RTIM? (@WMEM"
    sMeasurementFunction(MeasurementFunctionLayer.SIGNAL_TIME_AT_MAX_VOLTAGE) = "MEAS:VOLT:TMAX? (@WMEM"
    sMeasurementFunction(MeasurementFunctionLayer.SIGNAL_TIME_AT_MIN_VOLTAGE) = "MEAS:VOLT:TMIN? (@WMEM"

End Sub

Public Function AcqWaveform(nchannel As Long, _
                          Dout() As Double, _
                          Optional nSampleCount As Long = 500, _
                          Optional dSampleTime As Double = 0.000016, _
                          Optional dVoltRange As Double = 10, _
                          Optional dVoltOffSet As Double = 0, _
                          Optional dTriggerLevel As Double = 0, _
                          Optional sTriggerSlope As String = "POS", _
                          Optional sTriggerSource As String = "CHAN1", _
                          Optional dTriggerDelay As Double = 0.00000003, _
                          Optional nInputImp As Long = 1000000#, _
                          Optional sInputCoupling As String = "DC", _
                          Optional sLPAS As String = "OFF", _
                          Optional sHPAS As String = "OFF") As Integer

    Dim i As Integer
    Dim xx As Integer
    Dim sData As String
    Dim dMeasAmpl1 As Double
    Dim dMeasAmpl2 As Double
    Dim sINP$
    Dim nErr As Long
    Dim Byte1 As Integer
    Dim Byte2 As Integer
    
    Dim xValue As Double
    Dim xValue2 As Double
    Dim xFormat As Integer
    Dim xType As Integer
    Dim xPoints As Integer
    Dim xCount As Integer
    Dim xIncrement As Double
    Dim xOrigin As Double
    Dim xReference As Integer
    Dim yIncrement As Double
    Dim yOrigin As Double
    Dim yReference As Integer
    
    Dim S$
    Dim PREAMBLE$
    Dim NBR&
    Dim PtrX&
    Dim sCurrentMsg As String
    Dim sScopeError As String
    
    sCurrentMsg = frmMain.lblStatus.Caption

    frmMain.lblStatus.Caption = "Waveform Capture ..."

    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    frmMain.txtInstrument.Text = "OSCOPE"
    frmMain.txtCommand.Text = "AcquireWaveform"
    
    If CheckParameters(nchannel, dVoltRange, dVoltOffSet, dTriggerLevel, UCase(sTriggerSlope), _
                       UCase(sTriggerSource), dTriggerDelay, nInputImp, UCase(sInputCoupling), UCase(sLPAS), _
                       UCase(sHPAS), "SAMPLE", dSampleTime, "AcqWaveForm", nSampleCount) <> True Then
        Exit Function
    End If
        
    ReDim Dout(1 To nSampleCount) ' 500 or 8000

    'Setup OSCOPE for Waveform Acquistion
    If Not bSimulation Then
        nSystErr = atxmlDF_viClear(ResourceName(OSCOPE), 0&)
        WriteMsg OSCOPE, "SYST:LANG COMP"
        LongDelay (1)
        WriteMsg OSCOPE, "*CLS"
        WriteMsg OSCOPE, "*RST"
        LongDelay (1)
       
        WriteMsg OSCOPE, "TIM:RANG " & CStr(dSampleTime)
        WriteMsg OSCOPE, "TIM:DEL 0"
        WriteMsg OSCOPE, "TIM:mode TRIG"
        WriteMsg OSCOPE, "CHAN" & CStr(nchannel) & ":RANG " & CStr(dVoltRange)
        WriteMsg OSCOPE, "CHAN" & CStr(nchannel) & ":OFFS " & CStr(dVoltOffSet)
        If nInputImp = 50 Then
            WriteMsg OSCOPE, "CHAN" & CStr(nchannel) & ":COUP DCF"
        Else
            WriteMsg OSCOPE, "CHAN" & CStr(nchannel) & ":COUP " & sInputCoupling
        End If
        WriteMsg OSCOPE, "CHAN" & CStr(nchannel) & ":LFR " & sLPAS
        WriteMsg OSCOPE, "CHAN" & CStr(nchannel) & ":HFR " & sHPAS
        WriteMsg OSCOPE, "TRIG:SOUR " & sTriggerSource
        WriteMsg OSCOPE, "TRIG:LEV " & CStr(dTriggerLevel)
        WriteMsg OSCOPE, "TRIG:SLOPE " & sTriggerSlope
        If dTriggerDelay > 0.00000003 And dTriggerDelay < 0.16 Then
          WriteMsg OSCOPE, "TRIG:DEL " & CStr(dTriggerDelay)
        End If
        'Acquire, Digitize
        WriteMsg OSCOPE, "ACQ:POIN " + CStr(nSampleCount)
        WriteMsg OSCOPE, "ACQ:TYPE NORM"
        WriteMsg OSCOPE, "ACQ:COMP 100"
        Delay 0.2
        WriteMsg OSCOPE, "DIG CHAN1, CHAN2"
        For i = 1 To 100
          WriteMsg OSCOPE, "ACQ:COMP?"
          nErr = ReadMsg(OSCOPE, S$)
          If Val(S$) > 80 Then Exit For
          LongDelay 0.5
        Next i
             
    'Get the data from the DSO and put it into the Dout() array
        If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: GoTo ExitFunction
    
       Erase Dout
       WriteMsg OSCOPE, "WAV:SOUR CHAN" + CStr(nchannel) ' channel 1 or 2
       WriteMsg OSCOPE, "WAV:FORM BYTE"
       WriteMsg OSCOPE, "WAV:PRE?"
       LongDelay (2)
       PREAMBLE$ = sGetDsoBlock() ' 1st one is trash
       WriteMsg OSCOPE, "WAV:PRE?"
       PREAMBLE$ = sGetDsoBlock()

       If PREAMBLE$ <> "" Then
         For i = 1 To 10
           xx = InStr(PREAMBLE$, ",")
           If xx > 0 Then
             S$ = Left(PREAMBLE, xx - 1)
             PREAMBLE = Mid$(PREAMBLE, xx + 1)
           Else
             S$ = PREAMBLE
             PREAMBLE = ""
           End If
           Select Case i
             Case 1: xFormat = CInt(S$)
             Case 2: xType = CInt(S$)
             Case 3: xPoints = CInt(S$)
             Case 4: xCount = CInt(S$)
             Case 5: xIncrement = CDbl(S$)
             Case 6: xOrigin = CDbl(S$)
             Case 7: xReference = CInt(S$)
             Case 8: yIncrement = CDbl(S$)
             Case 9: yOrigin = CDbl(S$)
            Case 10: yReference = CInt(S$)
           End Select
           If PREAMBLE$ = "" Then Exit For
         Next i
       End If

       If xPoints > 0 Then
         WriteMsg OSCOPE, "WAV:DATA?"
         S$ = sGetDsoBlock()
         If Len(S$) > 0 Then
            ReDim Dout(xPoints)
            If xFormat = 1 Then ' byte format, 1 byte per point
              PtrX = 11 ' skip the first 10 bytes
              For i = 1 To xPoints
                xValue = Asc(Mid$(S$, PtrX, 1))
                If xValue = 255 Then
                  Dout(i) = 9.9999E+37
                Else
                  Dout(i) = ((xValue - yReference) * yIncrement) + yOrigin
                End If
                PtrX = PtrX + 1
              Next i
            ElseIf xFormat = 2 Then 'word format, 2 bytes per point
              PtrX = 11
              For i = 1 To xPoints
                If PtrX + 1 > Len(S$) Then Exit For
                xValue = Asc(Mid$(S$, PtrX, 1))      ' msbyte
                xValue2 = Asc(Mid$(S$, PtrX + 1, 1)) ' lsbyte
                If xValue = 255 And xValue2 = 255 Then
                  Dout(i) = 9.9999E+37
                Else
                  xValue = (xValue * 256) + xValue2
                  Dout(i) = ((xValue - yReference) * yIncrement) + yOrigin
                End If
                PtrX = PtrX + 2
              Next i

            End If
          End If
        Else
          ReDim Dout(nSampleCount)
          For i = 1 To nSampleCount
            Dout(i) = 9.9999E+37
          Next i
        End If
        WriteMsg OSCOPE, "*CLS"
        WriteMsg OSCOPE, "*RST"
        nSystErr = atxmlDF_viClear(ResourceName(OSCOPE), 0&)
      
        If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: GoTo ExitFunction
            
    Else
        'for simulation, get low/high data from operator (ie -5.0 to +5.0)
        dMeasAmpl1 = CDbl(InputBox("Command cmdOSCOPE.AcqWaveform peformed." & vbCrLf & "Enter First Input Value:", "SIMULATION MODE"))
        dMeasAmpl2 = CDbl(InputBox("Command cmdOSCOPE.AcqWaveform peformed." & vbCrLf & "Enter Last Input Value:", "SIMULATION MODE"))
        frmMain.txtMeasured.Text = dMeasAmpl2 - dMeasAmpl1
        'fill the array with simulated data
        Erase Dout
        ReDim Dout(nSampleCount)
        dMeasAmpl2 = (dMeasAmpl2 - dMeasAmpl1) / nSampleCount
        'fill array with straight line data, ie 8000 pts from -5.0 to +5.0
        For i = 1 To nSampleCount
          Dout(i) = dMeasAmpl1
          dMeasAmpl1 = dMeasAmpl1 + dMeasAmpl2
        Next i
        
        If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON
    End If

    AcqWaveform = False
    
    Exit Function

ExitFunction:
   frmMain.lblStatus.Caption = sCurrentMsg
   AcqWaveform = True
    
End Function

Private Function sGetDsoBlock() As String

  Dim nNbr As Long
  Dim ReadBuffer As String * 8100
  Dim MsgRespReg As Long

  sGetDsoBlock$ = ""
  
  nSystErr = atxml_ReadCmds("DSO_1", ReadBuffer, 8100, nNbr)

  Delay 0.5
  nSystErr = atxmlDF_viClear(ResourceName(OSCOPE), 0&)
  WriteMsg OSCOPE, "*CLS"
  If nSystErr <> 0 Then
      Exit Function
  End If
  
  sGetDsoBlock$ = Left(ReadBuffer$, nNbr&)     'Return only the instrument data
  
  If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function

End Function
                                                   
Public Sub ArmVPP(Optional nchannel As Long = 1, _
                          Optional dVoltRange As Double = 4, _
                          Optional dVoltOffSet As Double = 0, _
                          Optional dTimeRange As Double = 0.001, _
                          Optional dTriggerLevel As Double = 0, _
                          Optional sTriggerSlope As String = "POS", _
                          Optional sTriggerSource As String = "INP1", _
                          Optional nInputImp As Long = 1000000#, _
                          Optional sInputCoupling As String = "DC", _
                          Optional sLPAS As String = "OFF", _
                          Optional sHPAS As String = "OFF")
                          
     Call ArmVPP2(nchannel, dVoltRange, dVoltOffSet, dTimeRange, _
                  dTriggerLevel, sTriggerSlope, sTriggerSource, _
                  nInputImp, sInputCoupling, sLPAS, sHPAS)

End Sub

Public Sub ArmVPP2(Optional nchannel As Long = 1, _
                          Optional dVoltRange As Double = 4, _
                          Optional dVoltOffSet As Double = 0, _
                          Optional dTimeRange As Double = 0.001, _
                          Optional dTriggerLevel As Double = 0, _
                          Optional sTriggerSlope As String = "POS", _
                          Optional sTriggerSource As String = "INP1", _
                          Optional nInputImp As Long = 1000000#, _
                          Optional sInputCoupling As String = "DC", _
                          Optional sLPAS As String = "OFF", _
                          Optional sHPAS As String = "OFF", _
                          Optional dTriggerDelay As Double = 0.00000003)

    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    frmMain.txtInstrument.Text = "OSCOPE"
    frmMain.txtCommand.Text = "ArmVPP"
    
    If CheckParameters(nchannel, dVoltRange, dVoltOffSet, dTriggerLevel, _
                       UCase(sTriggerSlope), UCase(sTriggerSource), dTriggerDelay, nInputImp, _
                       UCase(sInputCoupling), UCase(sLPAS), UCase(sHPAS), "TIME", dTimeRange, "ArmVPP") <> True Then
        Exit Sub
    End If
        
    If Not bSimulation Then
        nSystErr = atxmlDF_viClear(ResourceName(OSCOPE), 0&)
        WriteMsg OSCOPE, "SYST:LANG COMP"
        Delay 1
        WriteMsg OSCOPE, "*CLS"
        WriteMsg OSCOPE, "*RST"
        
        'added to change the output buffer from the last measured value
        'otherwise, scope returns last measured value if there is no trigger
        WriteMsg OSCOPE, "CHAN" & CStr(nchannel) & ":RANG " & CStr(dVoltRange)
        WriteMsg OSCOPE, "CHAN" & CStr(nchannel) & ":OFFS " & CStr(dVoltOffSet)
        WriteMsg OSCOPE, "MEAS:VOLT:AC? (@INP1)"
        Delay 0.2
        WriteMsg OSCOPE, "*CLS"
        WriteMsg OSCOPE, "*RST"
        
        WriteMsg OSCOPE, "TIM:RANG " & CStr(dTimeRange)
        WriteMsg OSCOPE, "TIM:DEL 0"
        WriteMsg OSCOPE, "TIM:mode TRIG"
        WriteMsg OSCOPE, "CHAN" & CStr(nchannel) & ":RANG " & CStr(dVoltRange)
        WriteMsg OSCOPE, "CHAN" & CStr(nchannel) & ":OFFS " & CStr(dVoltOffSet)
        
        If nInputImp = 50 Then
            WriteMsg OSCOPE, "CHAN" & CStr(nchannel) & ":COUP DCF"
        Else
            WriteMsg OSCOPE, "CHAN" & CStr(nchannel) & ":COUP " & sInputCoupling
        End If
        
        WriteMsg OSCOPE, "CHAN" & CStr(nchannel) & ":LFR " & sLPAS
        WriteMsg OSCOPE, "CHAN" & CStr(nchannel) & ":HFR " & sHPAS
        WriteMsg OSCOPE, "TRIG:LEV " & CStr(dTriggerLevel)
        WriteMsg OSCOPE, "TRIG:SLOPE " & sTriggerSlope
        
        If dTriggerDelay > 0.00000003 And dTriggerDelay < 0.16 Then
          WriteMsg OSCOPE, "TRIG:DEL " & CStr(dTriggerDelay)
        End If
        
        WriteMsg OSCOPE, "MEAS:SOUR CHAN" & CStr(nchannel)
        WriteMsg OSCOPE, "MEAS:mode STAN"
        WriteMsg OSCOPE, "RUN"
        WriteMsg OSCOPE, "DIG CHAN1, CHAN2"
        Delay 0.5
        WriteMsg OSCOPE, "MEAS:SOUR CHAN" & CStr(nchannel)
        WriteMsg OSCOPE, "MEAS:VPP?"
    End If

End Sub

Private Function CheckParameters(nchannel As Long, dVoltRange As Double, _
                                 dVoltOffSet As Double, dTriggerLevel As Double, _
                                 sTriggerSlope As String, sTriggerSource As String, _
                                 dTriggerDelay As Double, nInputImp As Long, _
                                 sInputCoupling As String, sLPAS As String, _
                                 sHPAS As String, sTimeOrSample As String, _
                                 dTimeSampleRange As Double, sMeasureType As String, _
                                 Optional nSampleCount As Long = 500) As Boolean
                                 
    Dim sScopeError As String
    
    CheckParameters = True

    If nchannel < 1 Or nchannel > 2 Then
        sScopeError = " nChannel argument out of range." & vbCrLf & "RANGE: 1 TO 2"
        GoTo ExitFunction
    End If
    
    If dVoltRange < 0.008 Or dVoltRange > 40 Then
        sScopeError = " dVoltRange argument out of range." & vbCrLf & "RANGE: .008 TO 40"
        GoTo ExitFunction
    End If
    
    If sTimeOrSample = "SAMPLE" Then
        
        If nSampleCount = 500 Then
            
            If dTimeSampleRange < 0.00000001 Or dTimeSampleRange > 50 Then
                sScopeError = " dSampleTime argument out of range." & vbCrLf & "RANGE: 10E-9 TO 50"
                GoTo ExitFunction
            End If
            
        ElseIf nSampleCount = 8000 Then
            
            If dTimeSampleRange < 0.00000016 Or dTimeSampleRange > 50 Then
                sScopeError = " dSampleTime argument out of range." & vbCrLf & "RANGE: 160E-9 TO 800"
                GoTo ExitFunction
            End If
        Else
            sScopeError = " nSampleCount argument out of range." & vbCrLf & "RANGE: 500, 8000"
            GoTo ExitFunction
        End If
    Else
    
        If dTimeSampleRange < 0.00000005 Or dTimeSampleRange > 50 Then
            sScopeError = " dTimeRange argument out of range." & vbCrLf & "RANGE: 50E-9 TO 50"
            GoTo ExitFunction
        End If
    End If
    
    If dVoltOffSet <> 0 Then
        
        If dVoltRange > 10 Then
            
            If dVoltOffSet < -250 Or dVoltOffSet > 250 Then
                sScopeError = " dVoltOffSet argument out of range." & vbCrLf & "RANGE: -250 TO 250"
                GoTo ExitFunction
            End If
        ElseIf dVoltRange > 2 Then
            
            If dVoltOffSet < -50 Or dVoltOffSet > 50 Then
                sScopeError = " dVoltOffSet argument out of range." & vbCrLf & "RANGE: -50 TO 50"
                GoTo ExitFunction
            End If
        ElseIf dVoltRange > 0.4 Then
            
            If dVoltOffSet < -10 Or dVoltOffSet > 10 Then
                sScopeError = " dVoltOffSet argument out of range." & vbCrLf & "RANGE: -10 TO 10"
                GoTo ExitFunction
            End If
        ElseIf dVoltRange > 0.008 Then
            
            If dVoltOffSet < -2 Or dVoltOffSet > 2 Then
                sScopeError = " dVoltOffSet argument out of range." & vbCrLf & "RANGE: -2 TO 2"
                GoTo ExitFunction
            End If
        End If
    End If
    
    If sTriggerSlope <> "POS" And sTriggerSlope <> "NEG" Then
        sScopeError = " sTriggerSlope argument out of range." & vbCrLf & "RANGE: POS or NEG"
        GoTo ExitFunction
    End If
    
    If sTriggerSource <> "INP1" And sTriggerSource <> "INP2" And sTriggerSource <> "EXT" Then
        sScopeError = " sTriggerSource argument out of range." & vbCrLf & "RANGE: INP1 or INP2 or EXT"
        GoTo ExitFunction
    End If
    
    If dTriggerDelay < 0.00000003 Or dTriggerDelay > 0.16 Then
        sScopeError = " sTriggerDelay argument out of range." & vbCrLf & "RANGE: 0.000000030 to 0.160"
        GoTo ExitFunction
    End If
    
    If nInputImp <> 50 And nInputImp <> 1000000 Then
        sScopeError = " sTriggerSource argument out of range." & vbCrLf & "RANGE: 50 or 1E6"
        GoTo ExitFunction
    End If
    
    If sInputCoupling <> "DC" And sInputCoupling <> "AC" Then
        sScopeError = " sInputCoupling argument out of range." & vbCrLf & "RANGE: DC or AC"
        GoTo ExitFunction
    End If
    
    sLPAS = UCase(sLPAS)
    
    If sLPAS <> "OFF" And sLPAS <> "ON" Then
        sScopeError = " sLPAS argument out of range." & vbCrLf & "RANGE: OFF or ON"
        GoTo ExitFunction
    End If
    
    sHPAS = UCase(sHPAS)
    
    If sHPAS <> "OFF" And sHPAS <> "ON" Then
        sScopeError = " sHPAS argument out of range." & vbCrLf & "RANGE: OFF or ON"
        GoTo ExitFunction
    End If
    
    Exit Function

ExitFunction:
    Echo "OSCOPE PROGRAMMING ERROR:  Command cmdOSCOPE." & sMeasureType & sScopeError
    Err.Raise -1001
    CheckParameters = False
    
End Function

Public Function dMeasFetch(Optional nchannel As Integer = 1) As Double
    
    Dim sData As String
    Dim ReadBuffer As String * 8100
    Dim NBR As Long

    Dim nErr As Long
    Dim Q As String
    Q = Chr$(34)

    sData = ""

    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function

    frmMain.txtInstrument.Text = "Oscope"
    frmMain.txtCommand.Text = "dMeasFetch"
    Dim sCurrentMsg As String

    If nchannel < 1 Or nchannel > 2 Then
        Echo "Oscope PROGRAMMING ERROR:  Command cmdOSCOPE.dMeasFreq nChannel argument out of range."
        Echo "RANGE: 1 TO 2"
        dMeasFetch = 9.9E+37
        Err.Raise -1001
        Exit Function
    End If

    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function

    If Not bSimulation Then
        nErr = ReadMsg(OSCOPE, sData)
        Delay 0.5
        nSystErr = atxmlDF_viClear(ResourceName(OSCOPE), 0&)
        WriteMsg OSCOPE, "*CLS"

        If nErr <> 0 Then
            dMeasFetch = 9.9E+37: frmMain.txtMeasured.Text = 9.9E+37
            Err.Raise nErr
            Exit Function
        End If

        If sData = "" Then sData = "0"
        dMeasFetch = CDbl(sData)
        frmMain.txtMeasured.Text = CSng(dMeasFetch)

        If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    Else
        dMeasFetch = CDbl(InputBox("Command cmdOscope.dMeasFetch peformed." & vbCrLf & "Enter Value:", "SIMULATION MODE"))
        frmMain.txtMeasured.Text = CSng(dMeasFetch)

        If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON
    End If

End Function

Public Sub SendMsg(sMsg As String)

    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub

    frmMain.txtInstrument.Text = "OSCOPE"
    frmMain.txtCommand.Text = "SendMsg"
    WriteMsg OSCOPE, sMsg

    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON

End Sub

Public Function sReadMsg(sMsg As String) As String

    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function

    frmMain.txtInstrument.Text = "OSCOPE"
    frmMain.txtCommand.Text = "ReadMsg"
    ReadMsg OSCOPE, sReadMsg

    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON

End Function

Public Function dMeasFreq(sAutoScale As String, sINP As String, _
                          Optional dVoltRange As Double = 4, _
                          Optional dTimeRange As Double = 0.001, _
                          Optional dTriggerLevel As Double = 0, _
                          Optional sTriggerSlope As String = "POS", _
                          Optional sTriggerSource As String = "INP1", _
                          Optional nInputImp As Long = 1000000#, _
                          Optional sInputCoupling As String = "DC", _
                          Optional sLPAS As String = "OFF", _
                          Optional sHPAS As String = "OFF", _
                          Optional sHYST As String = "OFF", _
                          Optional APROBE As String = "OFF") As Double
    
    Dim sCurrentMsg As String

    sCurrentMsg = frmMain.lblStatus.Caption
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    
    frmMain.txtInstrument.Text = "OSCOPE"
    frmMain.txtCommand.Text = "dMeasFreq"
    
    If Not bSimulation Then
    
        dMeasFreq = dMeasureSignal(sAutoScale, sINP, ":FREQ?", dVoltRange, dTimeRange, _
                                   dTriggerLevel, sTriggerSlope, sTriggerSource, _
                                   nInputImp, sInputCoupling, sLPAS, sHPAS, sHYST, APROBE)
    Else
        dMeasFreq = CDbl(InputBox("Command cmdOSCOPE.dMeasFreq peformed." & vbCrLf & "Enter Frequency Value:", "SIMULATION MODE"))
        frmMain.txtMeasured.Text = dMeasFreq
        If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON
    End If

    frmMain.lblStatus.Caption = sCurrentMsg
    
End Function

Public Function dMeasVoltHigh(sAutoScale As String, sINP As String, _
                              Optional dVoltRange As Double = 4, _
                              Optional dTimeRange As Double = 0.001, _
                              Optional dTriggerLevel As Double = 0, _
                              Optional sTriggerSlope As String = "POS", _
                              Optional sTriggerSource As String = "INP1", _
                              Optional nInputImp As Long = 1000000#, _
                              Optional sInputCoupling As String = "DC", _
                              Optional sLPAS As String = "OFF", _
                              Optional sHPAS As String = "OFF", _
                              Optional sHYST As String = "OFF", _
                              Optional APROBE As String = "OFF") As Double
    
    Dim sCurrentMsg As String
                          
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    
    frmMain.txtInstrument.Text = "OSCOPE"
    frmMain.txtCommand.Text = "dMeasVoltHigh"
    
    If Not bSimulation Then
        
        dMeasVoltHigh = dMeasureSignal(sAutoScale, sINP, ":HIGH?", dVoltRange, dTimeRange, _
                                       dTriggerLevel, sTriggerSlope, sTriggerSource, _
                                       nInputImp, sInputCoupling, sLPAS, sHPAS, sHYST, APROBE)

    Else
        dMeasVoltHigh = CDbl(InputBox("Command cmdOSCOPE.dMeasVoltHigh peformed." & vbCrLf & "Enter Voltage Value:", "SIMULATION MODE"))
        frmMain.txtMeasured.Text = dMeasVoltHigh
        If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON
    End If
    
    frmMain.lblStatus.Caption = sCurrentMsg
    
End Function

Public Function dMeasVoltLow(sAutoScale As String, sINP As String, _
                             Optional dVoltRange As Double = 4, _
                             Optional dTimeRange As Double = 0.001, _
                             Optional dTriggerLevel As Double = 0, _
                             Optional sTriggerSlope As String = "POS", _
                             Optional sTriggerSource As String = "INP1", _
                             Optional nInputImp As Long = 1000000#, _
                             Optional sInputCoupling As String = "DC", _
                             Optional sLPAS As String = "OFF", _
                             Optional sHPAS As String = "OFF", _
                             Optional sHYST As String = "OFF", _
                             Optional APROBE As String = "OFF") As Double
                          
    Dim sCurrentMsg As String
    
    sCurrentMsg = frmMain.lblStatus.Caption
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    
    frmMain.txtInstrument.Text = "OSCOPE"
    frmMain.txtCommand.Text = "dMeasVoltLow"
    
    If Not bSimulation Then
        
        dMeasVoltLow = dMeasureSignal(sAutoScale, sINP, ":LOW?", dVoltRange, dTimeRange, _
                                      dTriggerLevel, sTriggerSlope, sTriggerSource, _
                                      nInputImp, sInputCoupling, sLPAS, sHPAS, sHYST, APROBE)

    Else
        dMeasVoltLow = CDbl(InputBox("Command cmdOSCOPE.dMeasVoltLow peformed." & vbCrLf & "Enter Voltage Value:", "SIMULATION MODE"))
        frmMain.txtMeasured.Text = dMeasVoltLow
        If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON
    End If
    
    frmMain.lblStatus.Caption = sCurrentMsg
    
End Function

Public Function dMeasVoltMax(sAutoScale As String, sINP As String, _
                             Optional dVoltRange As Double = 4, _
                             Optional dTimeRange As Double = 0.001, _
                             Optional dTriggerLevel As Double = 0, _
                             Optional sTriggerSlope As String = "POS", _
                             Optional sTriggerSource As String = "INP1", _
                             Optional nInputImp As Long = 1000000#, _
                             Optional sInputCoupling As String = "DC", _
                             Optional sLPAS As String = "OFF", _
                             Optional sHPAS As String = "OFF", _
                             Optional sHYST As String = "OFF", _
                             Optional APROBE As String = "OFF") As Double

    Dim sCurrentMsg As String
    
    sCurrentMsg = frmMain.lblStatus.Caption
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    
    frmMain.txtInstrument.Text = "OSCOPE"
    frmMain.txtCommand.Text = "dMeasVoltMax"
    
    If Not bSimulation Then
    
        dMeasVoltMax = dMeasureSignal(sAutoScale, sINP, ":MAX?", dVoltRange, dTimeRange, _
                                      dTriggerLevel, sTriggerSlope, sTriggerSource, _
                                      nInputImp, sInputCoupling, sLPAS, sHPAS, sHYST, APROBE)

    Else
        dMeasVoltMax = CDbl(InputBox("Command cmdOSCOPE.dMeasVoltMax peformed." & vbCrLf & "Enter Voltage Value:", "SIMULATION MODE"))
        frmMain.txtMeasured.Text = dMeasVoltMax
        If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON
    End If

    frmMain.lblStatus.Caption = sCurrentMsg
    
End Function

Public Function dMeasVoltMin(sAutoScale As String, sINP As String, _
                             Optional dVoltRange As Double = 4, _
                             Optional dTimeRange As Double = 0.001, _
                             Optional dTriggerLevel As Double = 0, _
                             Optional sTriggerSlope As String = "POS", _
                             Optional sTriggerSource As String = "INP1", _
                             Optional nInputImp As Long = 1000000#, _
                             Optional sInputCoupling As String = "DC", _
                             Optional sLPAS As String = "OFF", _
                             Optional sHPAS As String = "OFF", _
                             Optional sHYST As String = "OFF", _
                             Optional APROBE As String = "OFF") As Double

    Dim sCurrentMsg As String
    
    sCurrentMsg = frmMain.lblStatus.Caption
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    
    frmMain.txtInstrument.Text = "OSCOPE"
    frmMain.txtCommand.Text = "dMeasVoltMin"
    
    If Not bSimulation Then
    
        dMeasVoltMin = dMeasureSignal(sAutoScale, sINP, ":MIN?", dVoltRange, dTimeRange, _
                                      dTriggerLevel, sTriggerSlope, sTriggerSource, _
                                      nInputImp, sInputCoupling, sLPAS, sHPAS, sHYST, APROBE)

    Else
        dMeasVoltMin = CDbl(InputBox("Command cmdOSCOPE.dMeasVoltMin peformed." & vbCrLf & "Enter Voltage Value:", "SIMULATION MODE"))
        frmMain.txtMeasured.Text = dMeasVoltMin
        If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON
    End If

    frmMain.lblStatus.Caption = sCurrentMsg
    
End Function

Public Function dMeasFallOver(sAutoScale As String, sINP As String, _
                              Optional dVoltRange As Double = 4, _
                              Optional dTimeRange As Double = 0.001, _
                              Optional dTriggerLevel As Double = 0, _
                              Optional sTriggerSlope As String = "POS", _
                              Optional sTriggerSource As String = "INP1", _
                              Optional nInputImp As Long = 1000000#, _
                              Optional sInputCoupling As String = "DC", _
                              Optional sLPAS As String = "OFF", _
                              Optional sHPAS As String = "OFF", _
                              Optional sHYST As String = "OFF", _
                              Optional APROBE As String = "OFF") As Double

    Dim sCurrentMsg As String
    
    sCurrentMsg = frmMain.lblStatus.Caption
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    
    frmMain.txtInstrument.Text = "OSCOPE"
    frmMain.txtCommand.Text = "dMeasFallOver"
    
    If Not bSimulation Then
    
        dMeasFallOver = dMeasureSignal(sAutoScale, sINP, ":FALL:OVER?", dVoltRange, dTimeRange, _
                                      dTriggerLevel, sTriggerSlope, sTriggerSource, _
                                      nInputImp, sInputCoupling, sLPAS, sHPAS, sHYST, APROBE)

    Else
        dMeasFallOver = CDbl(InputBox("Command cmdOSCOPE.dMeasFallOver peformed." & vbCrLf & "Enter Voltage Value:", "SIMULATION MODE"))
        frmMain.txtMeasured.Text = dMeasFallOver
        If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON
    End If

    frmMain.lblStatus.Caption = sCurrentMsg
    
End Function

Public Function dMeasRiseOver(sAutoScale As String, sINP As String, _
                              Optional dVoltRange As Double = 4, _
                              Optional dTimeRange As Double = 0.001, _
                              Optional dTriggerLevel As Double = 0, _
                              Optional sTriggerSlope As String = "POS", _
                              Optional sTriggerSource As String = "INP1", _
                              Optional nInputImp As Long = 1000000#, _
                              Optional sInputCoupling As String = "DC", _
                              Optional sLPAS As String = "OFF", _
                              Optional sHPAS As String = "OFF", _
                              Optional sHYST As String = "OFF", _
                              Optional APROBE As String = "OFF") As Double

    Dim sCurrentMsg As String
    
    sCurrentMsg = frmMain.lblStatus.Caption
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    
    frmMain.txtInstrument.Text = "OSCOPE"
    frmMain.txtCommand.Text = "dMeasRiseOver"
    
    If Not bSimulation Then
    
        dMeasRiseOver = dMeasureSignal(sAutoScale, sINP, ":RISE:OVER?", dVoltRange, dTimeRange, _
                                      dTriggerLevel, sTriggerSlope, sTriggerSource, _
                                      nInputImp, sInputCoupling, sLPAS, sHPAS, sHYST, APROBE)

    Else
        dMeasRiseOver = CDbl(InputBox("Command cmdOSCOPE.dMeasRiseOver peformed." & vbCrLf & "Enter Voltage Value:", "SIMULATION MODE"))
        frmMain.txtMeasured.Text = dMeasRiseOver
        If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON
    End If

    frmMain.lblStatus.Caption = sCurrentMsg
    
End Function

Public Function dMeasFallPre(sAutoScale As String, sINP As String, _
                             Optional dVoltRange As Double = 4, _
                             Optional dTimeRange As Double = 0.001, _
                             Optional dTriggerLevel As Double = 0, _
                             Optional sTriggerSlope As String = "POS", _
                             Optional sTriggerSource As String = "INP1", _
                             Optional nInputImp As Long = 1000000#, _
                             Optional sInputCoupling As String = "DC", _
                             Optional sLPAS As String = "OFF", _
                             Optional sHPAS As String = "OFF", _
                             Optional sHYST As String = "OFF", _
                             Optional APROBE As String = "OFF") As Double

    Dim sCurrentMsg As String
    
    sCurrentMsg = frmMain.lblStatus.Caption
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    
    frmMain.txtInstrument.Text = "OSCOPE"
    frmMain.txtCommand.Text = "dMeasFallPre"
    
    If Not bSimulation Then
    
        dMeasFallPre = dMeasureSignal(sAutoScale, sINP, ":FALL:PRES?", dVoltRange, dTimeRange, _
                                      dTriggerLevel, sTriggerSlope, sTriggerSource, _
                                      nInputImp, sInputCoupling, sLPAS, sHPAS, sHYST, APROBE)

    Else
        dMeasFallPre = CDbl(InputBox("Command cmdOSCOPE.dMeasFallPre peformed." & vbCrLf & "Enter Voltage Value:", "SIMULATION MODE"))
        frmMain.txtMeasured.Text = dMeasFallPre
        If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON
    End If

    frmMain.lblStatus.Caption = sCurrentMsg
    
End Function

Public Function dMeasRisePre(sAutoScale As String, sINP As String, _
                             Optional dVoltRange As Double = 4, _
                             Optional dTimeRange As Double = 0.001, _
                             Optional dTriggerLevel As Double = 0, _
                             Optional sTriggerSlope As String = "POS", _
                             Optional sTriggerSource As String = "INP1", _
                             Optional nInputImp As Long = 1000000#, _
                             Optional sInputCoupling As String = "DC", _
                             Optional sLPAS As String = "OFF", _
                             Optional sHPAS As String = "OFF", _
                             Optional sHYST As String = "OFF", _
                             Optional APROBE As String = "OFF") As Double

    Dim sCurrentMsg As String
    
    sCurrentMsg = frmMain.lblStatus.Caption
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    
    frmMain.txtInstrument.Text = "OSCOPE"
    frmMain.txtCommand.Text = "dMeasRisePre"
    
    If Not bSimulation Then
    
        dMeasRisePre = dMeasureSignal(sAutoScale, sINP, ":RISE:PRES?", dVoltRange, dTimeRange, _
                                      dTriggerLevel, sTriggerSlope, sTriggerSource, _
                                      nInputImp, sInputCoupling, sLPAS, sHPAS, sHYST, APROBE)

    Else
        dMeasRisePre = CDbl(InputBox("Command cmdOSCOPE.dMeasRisePre peformed." & vbCrLf & "Enter Voltage Value:", "SIMULATION MODE"))
        frmMain.txtMeasured.Text = dMeasRisePre
        If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON
    End If

    frmMain.lblStatus.Caption = sCurrentMsg
    
End Function

Public Function dMeasFallTime(sAutoScale As String, sINP As String, _
                              Optional dVoltRange As Double = 4, _
                              Optional dTimeRange As Double = 0.001, _
                              Optional dTriggerLevel As Double = 0, _
                              Optional sTriggerSlope As String = "POS", _
                              Optional sTriggerSource As String = "INP1", _
                              Optional sLowerLimit As String = "10%", _
                              Optional sUpperLimit As String = "90%", _
                              Optional nInputImp As Long = 1000000#, _
                              Optional sInputCoupling As String = "DC", _
                              Optional sLPAS As String = "OFF", _
                              Optional sHPAS As String = "OFF", _
                              Optional sHYST As String = "OFF", _
                              Optional APROBE As String = "OFF") As Double

    Dim sCurrentMsg As String
    Dim sMeasStatement As String
    
    sCurrentMsg = frmMain.lblStatus.Caption
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    
    frmMain.txtInstrument.Text = "OSCOPE"
    frmMain.txtCommand.Text = "dMeasFallTime"
    
    If Not bSimulation Then
    
        If (sLowerLimit = "10%" And sUpperLimit = "90%") Then
            sMeasStatement = ":FALL:TIME?"
        ElseIf Not (sLowerLimit = "10%") Then
            sMeasStatement = ":FALL:TIME? " & sLowerLimit & ", " & " 90%,"
        Else
            sMeasStatement = ":FALL:TIME? " & sLowerLimit & ", " & sUpperLimit & ","
        End If
        
        dMeasFallTime = dMeasureSignal(sAutoScale, sINP, sMeasStatement, dVoltRange, dTimeRange, _
                                       dTriggerLevel, sTriggerSlope, sTriggerSource, _
                                       nInputImp, sInputCoupling, sLPAS, sHPAS, sHYST, APROBE)

    Else
        dMeasFallTime = CDbl(InputBox("Command cmdOSCOPE.dMeasFallTime peformed." & vbCrLf & "Enter Voltage Value:", "SIMULATION MODE"))
        frmMain.txtMeasured.Text = dMeasFallTime
        If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON
    End If

    frmMain.lblStatus.Caption = sCurrentMsg
    
End Function

Public Function dMeasRiseTime(sAutoScale As String, sINP As String, _
                              Optional dVoltRange As Double = 4, _
                              Optional dTimeRange As Double = 0.001, _
                              Optional dTriggerLevel As Double = 0, _
                              Optional sTriggerSlope As String = "POS", _
                              Optional sTriggerSource As String = "INP1", _
                              Optional sLowerLimit As String = "10%", _
                              Optional sUpperLimit As String = "90%", _
                              Optional nInputImp As Long = 1000000#, _
                              Optional sInputCoupling As String = "DC", _
                              Optional sLPAS As String = "OFF", _
                              Optional sHPAS As String = "OFF", _
                              Optional sHYST As String = "OFF", _
                              Optional APROBE As String = "OFF") As Double

    Dim sCurrentMsg As String
    Dim sMeasStatement As String
    
    sCurrentMsg = frmMain.lblStatus.Caption
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    
    frmMain.txtInstrument.Text = "OSCOPE"
    frmMain.txtCommand.Text = "dMeasRiseTime"
    
    If Not bSimulation Then
    
        If (sLowerLimit = "10%" And sUpperLimit = "90%") Then
            sMeasStatement = ":RISE:TIME?"
        ElseIf Not (sLowerLimit = "10%") Then
            sMeasStatement = ":RISE:TIME? " & sLowerLimit & ", " & " 90%,"
        Else
            sMeasStatement = ":RISE:TIME? " & sLowerLimit & ", " & sUpperLimit & ","
        End If
        
        dMeasRiseTime = dMeasureSignal(sAutoScale, sINP, sMeasStatement, dVoltRange, dTimeRange, _
                                       dTriggerLevel, sTriggerSlope, sTriggerSource, _
                                       nInputImp, sInputCoupling, sLPAS, sHPAS, sHYST, APROBE)

    Else
        dMeasRiseTime = CDbl(InputBox("Command cmdOSCOPE.dMeasRiseTime peformed." & vbCrLf & "Enter Voltage Value:", "SIMULATION MODE"))
        frmMain.txtMeasured.Text = dMeasRiseTime
        If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON
    End If

    frmMain.lblStatus.Caption = sCurrentMsg
    
End Function

Public Function dMeasTimeMax(sAutoScale As String, sINP As String, _
                             Optional dVoltRange As Double = 4, _
                             Optional dTimeRange As Double = 0.001, _
                             Optional dTriggerLevel As Double = 0, _
                             Optional sTriggerSlope As String = "POS", _
                             Optional sTriggerSource As String = "INP1", _
                             Optional sLowerLimit As String = "10%", _
                             Optional sUpperLimit As String = "90%", _
                             Optional nInputImp As Long = 1000000#, _
                             Optional sInputCoupling As String = "DC", _
                             Optional sLPAS As String = "OFF", _
                             Optional sHPAS As String = "OFF", _
                             Optional sHYST As String = "OFF", _
                             Optional APROBE As String = "OFF") As Double

    Dim sCurrentMsg As String
    
    sCurrentMsg = frmMain.lblStatus.Caption
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    
    frmMain.txtInstrument.Text = "OSCOPE"
    frmMain.txtCommand.Text = "dMeasTimeMax"
    
    If Not bSimulation Then
    
        dMeasTimeMax = dMeasureSignal(sAutoScale, sINP, ":TMAX?", dVoltRange, dTimeRange, _
                                      dTriggerLevel, sTriggerSlope, sTriggerSource, _
                                      nInputImp, sInputCoupling, sLPAS, sHPAS, sHYST, APROBE)

    Else
        dMeasTimeMax = CDbl(InputBox("Command cmdOSCOPE.dMeasTimeMax peformed." & vbCrLf & "Enter Voltage Value:", "SIMULATION MODE"))
        frmMain.txtMeasured.Text = dMeasTimeMax
        If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON
    End If

    frmMain.lblStatus.Caption = sCurrentMsg
    
End Function

Public Function dMeasTimeMin(sAutoScale As String, sINP As String, _
                             Optional dVoltRange As Double = 4, _
                             Optional dTimeRange As Double = 0.001, _
                             Optional dTriggerLevel As Double = 0, _
                             Optional sTriggerSlope As String = "POS", _
                             Optional sTriggerSource As String = "INP1", _
                             Optional sLowerLimit As String = "10%", _
                             Optional sUpperLimit As String = "90%", _
                             Optional nInputImp As Long = 1000000#, _
                             Optional sInputCoupling As String = "DC", _
                             Optional sLPAS As String = "OFF", _
                             Optional sHPAS As String = "OFF", _
                             Optional sHYST As String = "OFF", _
                             Optional APROBE As String = "OFF") As Double

    Dim sCurrentMsg As String
    
    sCurrentMsg = frmMain.lblStatus.Caption
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    
    frmMain.txtInstrument.Text = "OSCOPE"
    frmMain.txtCommand.Text = "dMeasTimeMin"
    
    If Not bSimulation Then
    
        dMeasTimeMin = dMeasureSignal(sAutoScale, sINP, ":TMIN?", dVoltRange, dTimeRange, _
                                      dTriggerLevel, sTriggerSlope, sTriggerSource, _
                                      nInputImp, sInputCoupling, sLPAS, sHPAS, sHYST, APROBE)

    Else
        dMeasTimeMin = CDbl(InputBox("Command cmdOSCOPE.dMeasTimeMin peformed." & vbCrLf & "Enter Voltage Value:", "SIMULATION MODE"))
        frmMain.txtMeasured.Text = dMeasTimeMin
        If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON
    End If

    frmMain.lblStatus.Caption = sCurrentMsg
    
End Function

Public Function dMeasDCYC(sAutoScale As String, sINP As String, _
                          Optional dVoltRange As Double = 4, _
                          Optional dTimeRange As Double = 0.001, _
                          Optional dTriggerLevel As Double = 0, _
                          Optional sTriggerSlope As String = "POS", _
                          Optional sTriggerSource As String = "INP1", _
                          Optional nInputImp As Long = 1000000#, _
                          Optional sInputCoupling As String = "DC", _
                          Optional sLPAS As String = "OFF", _
                          Optional sHPAS As String = "OFF", _
                          Optional sHYST As String = "OFF", _
                          Optional APROBE As String = "OFF") As Double

    Dim sCurrentMsg As String
    
    sCurrentMsg = frmMain.lblStatus.Caption
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    
    frmMain.txtInstrument.Text = "OSCOPE"
    frmMain.txtCommand.Text = "dMeasDCYC"
    
    If Not bSimulation Then
    
        dMeasDCYC = dMeasureSignal(sAutoScale, sINP, ":DCYC?", dVoltRange, dTimeRange, _
                                   dTriggerLevel, sTriggerSlope, sTriggerSource, _
                                   nInputImp, sInputCoupling, sLPAS, sHPAS, sHYST, APROBE)

    Else
        dMeasDCYC = CDbl(InputBox("Command cmdOSCOPE.dMeasDCYC peformed." & vbCrLf & "Enter Voltage Value:", "SIMULATION MODE"))
        frmMain.txtMeasured.Text = dMeasDCYC
        If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON
    End If

    frmMain.lblStatus.Caption = sCurrentMsg
    
End Function

Public Function dMeasNDUT(sAutoScale As String, sINP As String, _
                          Optional dVoltRange As Double = 4, _
                          Optional dTimeRange As Double = 0.001, _
                          Optional dTriggerLevel As Double = 0, _
                          Optional sTriggerSlope As String = "POS", _
                          Optional sTriggerSource As String = "INP1", _
                          Optional nInputImp As Long = 1000000#, _
                          Optional sInputCoupling As String = "DC", _
                          Optional sLPAS As String = "OFF", _
                          Optional sHPAS As String = "OFF", _
                          Optional sHYST As String = "OFF", _
                          Optional APROBE As String = "OFF") As Double

    Dim sCurrentMsg As String
    
    sCurrentMsg = frmMain.lblStatus.Caption
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    
    frmMain.txtInstrument.Text = "OSCOPE"
    frmMain.txtCommand.Text = "dMeasNDUT"
    
    If Not bSimulation Then
    
        dMeasNDUT = dMeasureSignal(sAutoScale, sINP, ":NDUT?", dVoltRange, dTimeRange, _
                                   dTriggerLevel, sTriggerSlope, sTriggerSource, _
                                   nInputImp, sInputCoupling, sLPAS, sHPAS, sHYST, APROBE)

    Else
        dMeasNDUT = CDbl(InputBox("Command cmdOSCOPE.dMeasNDUT peformed." & vbCrLf & "Enter Voltage Value:", "SIMULATION MODE"))
        frmMain.txtMeasured.Text = dMeasNDUT
        If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON
    End If

    frmMain.lblStatus.Caption = sCurrentMsg
    
End Function

Public Function dMeasNWID(sAutoScale As String, sINP As String, _
                          Optional dVoltRange As Double = 4, _
                          Optional dTimeRange As Double = 0.001, _
                          Optional dTriggerLevel As Double = 0, _
                          Optional sTriggerSlope As String = "POS", _
                          Optional sTriggerSource As String = "INP1", _
                          Optional nInputImp As Long = 1000000#, _
                          Optional sInputCoupling As String = "DC", _
                          Optional sLPAS As String = "OFF", _
                          Optional sHPAS As String = "OFF", _
                          Optional sHYST As String = "OFF", _
                          Optional APROBE As String = "OFF") As Double

    Dim sCurrentMsg As String
    
    sCurrentMsg = frmMain.lblStatus.Caption
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    
    frmMain.txtInstrument.Text = "OSCOPE"
    frmMain.txtCommand.Text = "dMeasNWID"
    
    If Not bSimulation Then
    
        dMeasNWID = dMeasureSignal(sAutoScale, sINP, ":NWID?", dVoltRange, dTimeRange, _
                                   dTriggerLevel, sTriggerSlope, sTriggerSource, _
                                   nInputImp, sInputCoupling, sLPAS, sHPAS, sHYST, APROBE)

    Else
        dMeasNWID = CDbl(InputBox("Command cmdOSCOPE.dMeasNWID peformed." & vbCrLf & "Enter Voltage Value:", "SIMULATION MODE"))
        frmMain.txtMeasured.Text = dMeasNWID
        If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON
    End If

    frmMain.lblStatus.Caption = sCurrentMsg
    
End Function

Public Function dMeasPWID(sAutoScale As String, sINP As String, _
                          Optional dVoltRange As Double = 4, _
                          Optional dTimeRange As Double = 0.001, _
                          Optional dTriggerLevel As Double = 0, _
                          Optional sTriggerSlope As String = "POS", _
                          Optional sTriggerSource As String = "INP1", _
                          Optional nInputImp As Long = 1000000#, _
                          Optional sInputCoupling As String = "DC", _
                          Optional sLPAS As String = "OFF", _
                          Optional sHPAS As String = "OFF", _
                          Optional sHYST As String = "OFF", _
                          Optional APROBE As String = "OFF") As Double

    Dim sCurrentMsg As String
    
    sCurrentMsg = frmMain.lblStatus.Caption
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    
    frmMain.txtInstrument.Text = "OSCOPE"
    frmMain.txtCommand.Text = "dMeasPWID"
    
    If Not bSimulation Then
    
        dMeasPWID = dMeasureSignal(sAutoScale, sINP, ":PWID?", dVoltRange, dTimeRange, _
                                   dTriggerLevel, sTriggerSlope, sTriggerSource, _
                                   nInputImp, sInputCoupling, sLPAS, sHPAS, sHYST, APROBE)

    Else
        dMeasPWID = CDbl(InputBox("Command cmdOSCOPE.dMeasPWID peformed." & vbCrLf & "Enter Voltage Value:", "SIMULATION MODE"))
        frmMain.txtMeasured.Text = dMeasPWID
        If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON
    End If

    frmMain.lblStatus.Caption = sCurrentMsg
    
End Function

Public Function dMeasPeriod(sAutoScale As String, sINP As String, _
                            Optional dVoltRange As Double = 4, _
                            Optional dTimeRange As Double = 0.001, _
                            Optional dTriggerLevel As Double = 0, _
                            Optional sTriggerSlope As String = "POS", _
                            Optional sTriggerSource As String = "INP1", _
                            Optional nInputImp As Long = 1000000#, _
                            Optional sInputCoupling As String = "DC", _
                            Optional sLPAS As String = "OFF", _
                            Optional sHPAS As String = "OFF", _
                            Optional sHYST As String = "OFF", _
                            Optional APROBE As String = "OFF") As Double

    Dim sCurrentMsg As String
    
    sCurrentMsg = frmMain.lblStatus.Caption
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    
    frmMain.txtInstrument.Text = "OSCOPE"
    frmMain.txtCommand.Text = "dMeasPeriod"
    
    If Not bSimulation Then
    
        dMeasPeriod = dMeasureSignal(sAutoScale, sINP, ":PER?", dVoltRange, dTimeRange, _
                                     dTriggerLevel, sTriggerSlope, sTriggerSource, _
                                     nInputImp, sInputCoupling, sLPAS, sHPAS, sHYST, APROBE)

    Else
        dMeasPeriod = CDbl(InputBox("Command cmdOSCOPE.dMeasPeriod peformed." & vbCrLf & "Enter Voltage Value:", "SIMULATION MODE"))
        frmMain.txtMeasured.Text = dMeasPeriod
        If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON
    End If

    frmMain.lblStatus.Caption = sCurrentMsg
    
End Function

Public Function dMeasAmpl(sAutoScale As String, sINP As String, _
                          Optional dVoltRange As Double = 4, _
                          Optional dTimeRange As Double = 0.001, _
                          Optional dTriggerLevel As Double = 0, _
                          Optional sTriggerSlope As String = "POS", _
                          Optional sTriggerSource As String = "INP1", _
                          Optional nInputImp As Long = 1000000#, _
                          Optional sInputCoupling As String = "DC", _
                          Optional sLPAS As String = "OFF", _
                          Optional sHPAS As String = "OFF", _
                          Optional sHYST As String = "OFF", _
                          Optional APROBE As String = "OFF", _
                          Optional dOffSet As Double = 0) As Double

    Dim sCurrentMsg As String
    
    sCurrentMsg = frmMain.lblStatus.Caption
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    
    frmMain.txtInstrument.Text = "OSCOPE"
    frmMain.txtCommand.Text = "dMeasAmpl"
    
    If Not bSimulation Then
    
        dMeasAmpl = dMeasureSignal(sAutoScale, sINP, ":AMPL?", dVoltRange, dTimeRange, _
                                   dTriggerLevel, sTriggerSlope, sTriggerSource, _
                                   nInputImp, sInputCoupling, sLPAS, sHPAS, sHYST, APROBE, 1, dOffSet)

    Else
        dMeasAmpl = CDbl(InputBox("Command cmdOSCOPE.dMeasAmpl peformed." & vbCrLf & "Enter Voltage Value:", "SIMULATION MODE"))
        frmMain.txtMeasured.Text = dMeasAmpl
        If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON
    End If

    frmMain.lblStatus.Caption = sCurrentMsg
    
End Function

Public Function dMeasAC(sAutoScale As String, sINP As String, _
                          Optional dVoltRange As Double = 4, _
                          Optional dTimeRange As Double = 0.001, _
                          Optional dTriggerLevel As Double = 0, _
                          Optional sTriggerSlope As String = "POS", _
                          Optional sTriggerSource As String = "INP1", _
                          Optional nInputImp As Long = 1000000#, _
                          Optional sInputCoupling As String = "DC", _
                          Optional sLPAS As String = "OFF", _
                          Optional sHPAS As String = "OFF", _
                          Optional sHYST As String = "OFF", _
                          Optional APROBE As String = "OFF") As Double

    Dim sCurrentMsg As String
    
    sCurrentMsg = frmMain.lblStatus.Caption
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    
    frmMain.txtInstrument.Text = "OSCOPE"
    frmMain.txtCommand.Text = "dMeasAC"
    
    If Not bSimulation Then
    
        dMeasAC = dMeasureSignal(sAutoScale, sINP, ":AC?", dVoltRange, dTimeRange, _
                                   dTriggerLevel, sTriggerSlope, sTriggerSource, _
                                   nInputImp, sInputCoupling, sLPAS, sHPAS, sHYST, APROBE)

    Else
        dMeasAC = CDbl(InputBox("Command cmdOSCOPE.dMeasAC peformed." & vbCrLf & "Enter Voltage Value:", "SIMULATION MODE"))
        frmMain.txtMeasured.Text = dMeasAC
        If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON
    End If

    frmMain.lblStatus.Caption = sCurrentMsg
    
End Function

Public Function dMeasDC(sAutoScale As String, _
                          sINP As String, _
                          Optional dVoltRange As Double = 4, _
                          Optional dTimeRange As Double = 0.001, _
                          Optional nInputImp As Long = 1000000#, _
                          Optional APROBE As String = "OFF") As Double
                          
    Dim sData As String
    Dim nErr As Long
    Dim sEchoMessage As String
    Dim sCurrentMsg As String
    
    sCurrentMsg = frmMain.lblStatus.Caption
    
    If APROBE <> "OFF" Then
        frmMain.lblStatus.Caption = "APPLY PROBE"
    End If
    
    sAutoScale = UCase(sAutoScale)
    sINP = UCase(sINP)
        
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: GoTo ExitFunction
    
    frmMain.txtInstrument.Text = "OSCOPE"
    frmMain.txtCommand.Text = "dMeasDC"
    
    If Not bSimulation Then
        WriteMsg OSCOPE, "SYST:LANG SCPI"
        LongDelay (1)
        WriteMsg OSCOPE, "*CLS"
        WriteMsg OSCOPE, "*RST"
        
        Select Case sAutoScale
        
            Case "ON", "AUTO"
                WriteMsg OSCOPE, "VOLT1:RANG " & CStr(dVoltRange) 'Scope not setting the amplitude properly
                WriteMsg OSCOPE, "SYST:AUT"
                LongDelay (2)
                
            Case "OFF"
            
                If TestSendString(sINP, "INP1", "INP2", sINP & " ON") = 1 Then
                    sEchoMessage = "OSCOPE INPUT CHANNEL SELECTION ERROR"
                    GoTo ExitFunction
                End If
            
                If TestSendString(CStr(nInputImp), "50", "1000000", sINP & ":IMP " & CStr(nInputImp)) = 1 Then
                    sEchoMessage = "OSCOPE INPUT IMPEDANCE PARAMETER ERROR"
                    GoTo ExitFunction
                End If
            
                If (dVoltRange < 0.008 Or dVoltRange > 40) Then
                    sEchoMessage = "OSCOPE VOLT RANGE ERROR"
                    GoTo ExitFunction
                Else
                    
                    Select Case sINP
                        
                        Case "INP1"
                            WriteMsg OSCOPE, "VOLT1:RANG " & CStr(dVoltRange)
                        
                        Case "INP2"
                            WriteMsg OSCOPE, "VOLT2:RANG " & CStr(dVoltRange)
                    
                    End Select
                End If
            
                If (dTimeRange < 0.00000001 Or dTimeRange > 50) Then
                    sEchoMessage = "OSCOPE TIME RANGE ERROR"
                    GoTo ExitFunction
                Else
                    WriteMsg OSCOPE, "SWE:TIME:RANG " & CStr(dTimeRange)
                End If

            Case Else
                Echo "OSCOPE AUTO SCALE PARAMETER ERROR"
                dMeasDC = 0
                GoTo ExitFunction

        End Select
       
        dMeasDC = MeasurementStart(sAutoScale, sINP, "?", APROBE)
    
    Else
        dMeasDC = CDbl(InputBox("Command cmdOSCOPE.dMeasDC peformed." & vbCrLf & "Enter DC Voltage Value:", "SIMULATION MODE"))
        frmMain.txtMeasured.Text = dMeasDC
        
        If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON
        
    End If

    frmMain.lblStatus.Caption = sCurrentMsg
    
    Exit Function
    
ExitFunction:
   dMeasDC = 9.9E+37
   Echo sEchoMessage
   WriteMsg OSCOPE, "SYST:LANG COMP"

End Function

'cmd: "MIN" or "MAX"
'            Query the digitizer buffer for either the max measured value or the min measured
'            value.
Public Function Query(cmd As String, Optional defaultvalue As Double = 28.123) As Double
    
    Dim sData$
    Dim nErr As Long
    
    Select Case cmd
        
        Case "MIN"
            WriteMsg OSCOPE, "MEAS:VMIN?"
            Delay 1
            nErr = ReadMsg(OSCOPE, sData$)
        
        Case "MAX"
            WriteMsg OSCOPE, "MEAS:VMAX?"
            Delay 1
            nErr = ReadMsg(OSCOPE, sData$)
        
        Case Else:
            MsgBox "DMM PROGRAMMING ERROR:  Command TETSAPI.Query invalid VISA command."
            Exit Function
    
    End Select
    
    If sData$ = "" Then sData$ = defaultvalue 'The TETS scope sometimes returns "" if no reading is taken
    
    Query = CDbl(sData$)

End Function

Public Sub Scope_Setup_Initiate(ByVal Sweep_Range As Double, ByVal Voltage_Range As Double, _
                                ByVal Voltage_Offset As String, ByVal Sweep_Delay As Double, _
                                ByVal TriggerLevel As Double, ByVal Samples As String, _
                                ByVal Coupling As String, ByVal Slope As String, _
                                ByVal TrigSource As String, ByVal TimeMode As String)
        
    Dim strCoupling As String, strSlope As String, strTrigSource As String, strTimeMode As String
    Dim strSweepRange As String, strVoltageRange As String, strSamples As String
    
    Select Case Coupling
        
        Case "AC"
            strCoupling = "CHAN1:COUP AC;"
        
        Case "DC"
            strCoupling = "CHAN1:COUP DC;"
        
        Case "DC50"
            strCoupling = "CHAN1:COUP DCF;"
    
    End Select
    
    Select Case TrigSource
        
        Case "CHAN1"
            strTrigSource = "TRIG:SOUR CHAN1"
        
        Case "CHAN2"
            strTrigSource = "TRIG:SOUR CHAN2"
        
        Case "EXT"
            strTrigSource = "TRIG:SOUR EXT"
        
        Case "IMM"
            strTrigSource = "TRIG:SOUR IMM"
    
    End Select
    
    Select Case TimeMode
        
        Case "TRIG"
            strTimeMode = "TIM:MODE TRIG"
        
        Case "IMM"
            strTimeMode = "TIM:MODE IMM"
    
    End Select
    
    Select Case Slope
        
        Case "POS"
            strSlope = "TRIG:SLOP POS"
        
        Case "NEG"
            strSlope = "TRIG:SLOP NEG"
        
        Case "EITH"
            strSlope = "TRIG:SLOP EITH"
    
    End Select
    
    Select Case Samples
        
        Case "SAMPLES_500"
            strSamples = "ACQ:POIN 500"
        
        Case "SAMPLES_8000"
            strSamples = "ACQ:POIN 8000"
    
    End Select
        
    If Sweep_Range >= 0.000000001 And Sweep_Range <= 50 Then
        strSweepRange = "TIM:RANG " + (CStr(Sweep_Range))
    Else
        strSweepRange = "TIM:RANG .001"
    End If
        
    If Voltage_Range >= 0.008 And Voltage_Range <= 40 Then
        strVoltageRange = "CHAN1:RANG " + (CStr(Voltage_Range))
    Else
        strVoltageRange = "CHAN1:RANG 4"
    End If
    
    nSystErr = atxmlDF_viClear(ResourceName(OSCOPE), 0&)
    Delay 1
    WriteMsg OSCOPE, "*RST"
    Delay 0.2
    WriteMsg OSCOPE, "*CLS"
    Delay 0.2
    WriteMsg OSCOPE, (strCoupling)
    Delay 0.2
    WriteMsg OSCOPE, "CHAN2:COUP DC"
    Delay 0.2
    WriteMsg OSCOPE, (strVoltageRange)
    Delay 0.2
    WriteMsg OSCOPE, "CHAN1:OFFS " & Voltage_Offset
    Delay 0.2
    WriteMsg OSCOPE, "CHAN2:RANG 40"
    Delay 0.2
    WriteMsg OSCOPE, "CHAN2:OFFS " & Voltage_Offset
    Delay 0.2
    WriteMsg OSCOPE, (strSweepRange)
    Delay 0.2
    WriteMsg OSCOPE, "TIM:DEL " + (CStr(Sweep_Delay))
    Delay 0.2
    WriteMsg OSCOPE, "TIM:SAMP REAL"
    Delay 0.2
    WriteMsg OSCOPE, (strTimeMode)
    Delay 0.2
    WriteMsg OSCOPE, "TRIG:MODE EDGE"
    Delay 0.2
    WriteMsg OSCOPE, (strTrigSource)
    Delay 0.2
    WriteMsg OSCOPE, "TRIG:LEV " + (CStr(TriggerLevel))
    Delay 0.2
    WriteMsg OSCOPE, (strSlope)
    Delay 0.2
    WriteMsg OSCOPE, (strSamples)
    Delay 0.2
    WriteMsg OSCOPE, "MEAS:SOUR CHAN1"
    Delay 0.2
    WriteMsg OSCOPE, "DIG CHAN1"
    Delay 0.2

End Sub

'*************************************************************************************
'dMeasureSignal
'
'sAutoScale: "ON" or "OFF"
'            auto scale can be used to determine vertical and horizontal parameters
'            when sAutoScale = "ON", optional parameters are ignored.
'           Autoscale should only be used with relatively stable input signals having
'           a duty cycle of greater than 0.5% and a frequency greater than 50Hz.

'sINP:  select INP1 or INP2 (OSCOPE input channel)
'       At reset, INP1 is on and INP2 if off.
'       Max input voltage that can be applied to the two input connectors is 5 Vrms
'       at 50 ohms or +/-250V at 1Mohms.

'sInputCoupling: select input coupling for input channels ("DC" at reset)

'nInputImp: select input impedance 50 or 1M (1M at reset)

'sLPAS: select low-pass filter ("OFF" at reset)
'sHPAS: select high-pass filter ("OFF" at reset)

'dVoltRange: set the full scale vertical range
'            min = 8mV, max = 40V, at reset = 4V

'dTimeRange: set the full scale horizontal range for sweep
'            min = 10 ns, max = 50 s, at reset = 1 ms

'dTriggerLevel: set trigger level voltage (0V at reset)
'sHYST: set noise rejection ("OFF" at reset)
'sTriggerSlope: set trigger slope ("POS" at reset)
'sTriggerSource: set trigger source ("INP1" at reset)
'*************************************************************************************

Private Function dMeasureSignal(sAutoScale As String, sINP As String, _
                                sSignalType As String, dVoltRange As Double, _
                                dTimeRange As Double, dTriggerLevel As Double, _
                                sTriggerSlope As String, sTriggerSource As String, _
                                nInputImp As Long, sInputCoupling As String, _
                                sLPAS As String, sHPAS As String, _
                                sHYST As String, APROBE As String, _
                                Optional iOffSetUsed As Integer = 0, _
                                Optional dOffSet As Double = 0) As Double

    Dim sEchoMessage As String
    
    If APROBE <> "OFF" Then
        frmMain.lblStatus.Caption = "APPLY PROBE"
    End If
    
    sINP = UCase(sINP)
    sLPAS = UCase(sLPAS)
    sHPAS = UCase(sHPAS)
    sHYST = UCase(sHYST)
    sAutoScale = UCase(sAutoScale)
    sInputCoupling = UCase(sInputCoupling)
    sTriggerSlope = UCase(sTriggerSlope)
    sTriggerSource = UCase(sTriggerSource)
        
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: GoTo ExitFunction
    
    WriteMsg OSCOPE, "SYST:LANG SCPI"
    LongDelay (1)
    WriteMsg OSCOPE, "*CLS"
    WriteMsg OSCOPE, "*RST"
        
    Select Case sAutoScale
        Case "ON", "AUTO"
        
            WriteMsg OSCOPE, "VOLT1:RANG " & CStr(dVoltRange)
            WriteMsg OSCOPE, "SYST:AUT"
            LongDelay (2)
                
        Case "OFF"
        
            If TestSendString(sINP, "INP1", "INP2", sINP & " ON") = 1 Then
                sEchoMessage = "OSCOPE INPUT CHANNEL SELECTION ERROR"
                GoTo ExitFunction
            End If
            
            If TestSendString(sInputCoupling, "DC", "AC", sINP & ":COUP " & sInputCoupling) = 1 Then
                sEchoMessage = "OSCOPE INPUT COUPLING PARAMETER ERROR"
                GoTo ExitFunction
            End If
            
            If TestSendString(CStr(nInputImp), "50", "1000000", sINP & ":IMP " & CStr(nInputImp)) = 1 Then
                sEchoMessage = "OSCOPE INPUT IMPEDANCE PARAMETER ERROR"
                GoTo ExitFunction
            End If
            
            If TestSendString(sLPAS, "ON", "OFF", sINP & ":FILT:LPAS " & sLPAS) = 1 Then
                sEchoMessage = "OSCOPE INPUT FILTER STATE PARAMETER ERROR"
                GoTo ExitFunction
            End If
            
            If TestSendString(sHPAS, "ON", "OFF", sINP & ":FILT:hPAS " & sHPAS) = 1 Then
                sEchoMessage = "OSCOPE INPUT FILTER STATE PARAMETER ERROR"
                GoTo ExitFunction
            End If
            
            If (dVoltRange < 0.008 Or dVoltRange > 50) Then
                sEchoMessage = "OSCOPE VOLT RANGE ERROR"
                GoTo ExitFunction
            Else
            
                Select Case sINP
                    Case "INP1"
                        WriteMsg OSCOPE, "VOLT1:RANG " & CStr(dVoltRange)
                    Case "INP2"
                        WriteMsg OSCOPE, "VOLT2:RANG " & CStr(dVoltRange)
                End Select
            End If
            
            If iOffSetUsed = 1 Then
            
                If dOffSet <> 0 Then
                
                    If dVoltRange > 10 Then
                    
                        If dOffSet < -250 Or dOffSet > 250 Then
                            sEchoMessage = "OSCOPE dOffset range for dVoltRange of >10 V is +/-250 V"
                            GoTo ExitFunction
                        End If
                        
                    ElseIf dVoltRange > 2 Then
                    
                        If dOffSet < -50 Or dOffSet > 50 Then
                            sEchoMessage = "OSCOPE dOffset range for dVoltRange of >50 V is +/-50 V"
                            GoTo ExitFunction
                        End If
                        
                    ElseIf dVoltRange > 0.4 Then
                    
                        If dOffSet < -10 Or dOffSet > 10 Then
                            sEchoMessage = "OSCOPE dOffset range for dVoltRange of >400 mV is +/-10 V"
                            GoTo ExitFunction
                        End If
                        
                    Else
                        If dOffSet < -2 Or dOffSet > 2 Then
                            sEchoMessage = "OSCOPE dOffset range for dVoltRange of >8 mV is +/-2 V"
                            GoTo ExitFunction
                        End If
                    End If
                End If
                
                Select Case sINP
                    
                    Case "INP1"
                        WriteMsg OSCOPE, "VOLT1:RANG:OFFS " & CStr(dOffSet)
                    Case "INP2"
                        WriteMsg OSCOPE, "VOLT2:RANG:OFFS " & CStr(dOffSet)
                End Select
            End If
            
            If (dTimeRange < 0.00000001 Or dTimeRange > 50) Then
                sEchoMessage = "OSCOPE TIME RANGE ERROR"
                GoTo ExitFunction
            Else
                WriteMsg OSCOPE, "SWE:TIME:RANG " & CStr(dTimeRange)
            End If
                
            WriteMsg OSCOPE, "TRIG:LEV " & CStr(dTriggerLevel)
                
            If TestSendString(sHYST, "ON", "OFF", "TRIG:HYST " & sHYST) = 1 Then
                sEchoMessage = "OSCOPE TRIGGER INPUT PARAMETER ERROR"
                GoTo ExitFunction
            End If
            
            If TestSendString(sTriggerSlope, "POS", "NEG", "TRIG:SLOP " & sTriggerSlope) = 1 Then
                sEchoMessage = "OSCOPE TRIGGER SLOPE PARAMETER ERROR"
                GoTo ExitFunction
            End If
            
            Select Case sTriggerSource
            
                Case "INP1", "INP2", "EXT", "ECLT0", "ECLT1"
                    WriteMsg OSCOPE, "TRIG:SOUR " & sTriggerSource
                        
                Case Else
                    Echo "OSCOPE TRIGGER SOURCE PARAMETER ERROR"
                    dMeasureSignal = 0
                    
                    GoTo ExitFunction
            End Select

        Case Else
            Echo "OSCOPE AUTO SCALE PARAMETER ERROR"
            dMeasureSignal = 0
            
            GoTo ExitFunction
    End Select
    
    dMeasureSignal = MeasurementStart(sAutoScale, sINP, sSignalType, APROBE)
    
    Exit Function
               
ExitFunction:
   dMeasureSignal = 9.9E+37
   Echo sEchoMessage
   WriteMsg OSCOPE, "SYST:LANG COMP"

End Function

Private Function dDoTheMeasurement(sSignalType As String, sINP As String) As Double

    Dim sData As String
    Dim nErr As Long
    
    frmMain.lblStatus.Caption = "Making Measurement ..."
    WriteMsg OSCOPE, "MEAS:VOLT" & sSignalType & " (@" & sINP & ")"
    LongDelay (2)
    nErr = ReadMsg(OSCOPE, sData)
    WriteMsg OSCOPE, "*CLS"
                        
    If nErr <> 0 Then
        Echo "VISA ERROR: " & nErr
        dDoTheMeasurement = 9.9E+37: Err.Raise nErr
        Exit Function
        
    End If
                        
    dDoTheMeasurement = CDbl(sData)
    frmMain.txtMeasured.Text = dDoTheMeasurement

End Function

Private Function TestSendString(sTestString As String, _
                                sTest1 As String, _
                                sTest2 As String, _
                                sStringToSend As String) As Integer
    TestSendString = 0
    
    If (sTestString = sTest1 Or sTestString = sTest2) Then
        WriteMsg OSCOPE, sStringToSend
    Else
        TestSendString = 1
    End If
                                
End Function

Private Function MeasurementStart(sAutoScale As String, sINP As String, _
                                     sSignalType As String, APROBE As String) As Double

    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: GoTo ExitFunction
        
    Select Case Left(UCase(APROBE), 1)
    
        Case "O"    ' Off
                
            MeasurementStart = dDoTheMeasurement(sSignalType, sINP)
            frmMain.lblStatus.Caption = "Measurement Complete ..."
                
            If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: GoTo ExitFunction
            
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
                    
                If (sAutoScale = "ON" Or sAutoScale = "AUTO") Then
                    WriteMsg OSCOPE, "SYST:AUT"
                    LongDelay (1)
                End If
                    
                MeasurementStart = dDoTheMeasurement(sSignalType, sINP)
                frmMain.lblStatus.Caption = "Measurement Complete ..."
                    
                If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: GoTo ExitFunction
                
                If Failed = True Then
                    MisProbe = MsgBox("Analog Probe Measurement Failed. " & vbCrLf & _
                        "Do you wish to re-probe?", vbCritical + vbYesNo + vbSystemModal, "Analog Probe Failure")
                Else
                    MisProbe = vbNo
                End If
            Loop
                                    
        Case "C"    ' Continuous
            
            MisProbe = vbYes
                
            Do While MisProbe = vbYes
            
                UserEvent = 0
                Failed = False
                frmMain.TimerProbe.Enabled = True: frmMain.fraInstructions.Visible = True
                    
                Do While (UserEvent = 0) And (Not bProbeClosed)
                    
                    If (sAutoScale = "ON" Or sAutoScale = "AUTO") Then
                        WriteMsg OSCOPE, "SYST:AUT"
                        LongDelay (1)
                    End If
                    
                    MeasurementStart = dDoTheMeasurement(sSignalType, sINP)
                        
                    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: GoTo ExitFunction
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
    
    Exit Function
    
ExitFunction:
   MeasurementStart = 0
   WriteMsg OSCOPE, "SYST:LANG COMP"

End Function

'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' SetupScopeMeasure()
'
' Setup the Scope to measure period, frequency, amplitude, etc
' inpMode - automatic or manual scope setup
' ScopeInput - INPUT1, INPUT2, INPUT1_AND_INPUT2
' dTimeRange - resolution of scope capture window
' dVoltageRange - amplitude range of the expected signal
' dTrigLevel - trigger level
' Samples - SAMPLES_500 or SAMPLES_8000
' Coupling - use enum InputCoupling to determine parameter
' Slope - POS or NEG
' triggerPoint -TRIGGER_IN_BEGINNING, TRIGGER_IN_MIDDLE, TRIGGER_IN_END - show data after trigger, before trigger,
'                                                                         or put the trigger in the middle of the
'                                                                         data captured
' InpImpedance - Input impedance
' nLPASS - low pass filter on/off
' nHPASS - high pass filter on/off
' nHYST - hysteresis on/off
' ON_OFF_AUTO - trigger normal, single trigger, auto trigger
' APROBE - analog probe
' delayAFterSetupOfScope - delay in seconds after the scope is initiated.
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Public Function SetupScopeMeasure(Optional ByVal inpMode As MAN_OR_AUTO = MAN_OR_AUTO.AUTO_SETUP, _
                                  Optional ByVal ScopeInput As InputSource = InputSource.INPUT1, _
                                  Optional ByVal dTimeRange As Double = 0.1, _
                                  Optional ByVal dVoltageRange As Double = 10, _
                                  Optional ByVal dTrigLevel As Double = 2, _
                                  Optional ByVal Samples As ScopeSamples = ScopeSamples.SAMPLES_500, _
                                  Optional ByVal Coupling As InputCoupling = InputCoupling.DC, _
                                  Optional ByVal Slope As TriggerSlope = TriggerSlope.POS, _
                                  Optional ByVal TrigSource As TriggerSource = TriggerSource.TRG_SRC_INPUT1, _
                                  Optional ByVal triggerPoint As TimeDelayLink = TimeDelayLink.TRIGGER_IN_BEGINNING, _
                                  Optional ByVal InpImpedance As InputImpedance = InputImpedance.OHM_1M, _
                                  Optional ByVal nLPASS As LowPass = LowPass.LP_INACTIVE, _
                                  Optional ByVal nHPASS As HighPass = HighPass.HP_INACTIVE, _
                                  Optional ByVal nHYST As Hysteresis = Hysteresis.HYST_INACTIVE, _
                                  Optional ByVal ON_OFF_AUTO As TimeMode = TimeMode.TRIG_CONTINUOUS, _
                                  Optional ByVal APROBE As ATE_PROBE = ATE_PROBE.PROBE_OFF, _
                                  Optional ByVal delayAfterSetupOfScope As Single = 0) As Double
    
    Dim Read As Double
    Dim sMsg As String
    Dim sData As String
    Dim nErr As Long
    Dim sTimeRange As String
    Dim sVoltRange As String
    Dim sTrigLevel As String
    
    sTimeRange = CStr(dTimeRange)
    sVoltRange = CStr(dVoltageRange)
    sTrigLevel = CStr(dTrigLevel)
    blnScopeSetupFirstTime = True
    
    WriteMsg OSCOPE, "SYST:LANG SCPI"
    Delay 1
    WriteMsg OSCOPE, "*CLS"
    WriteMsg OSCOPE, "*RST"
    
    '''   'baseline the WMEM1. This sets the memory to essentially noise coming into input channel 1. mk 9-28-2012
    '''   ' Doing this will be used later when we need to get waveform descriptors such as period, frequency, etc.
    WriteMsg OSCOPE, "INIT:CONT AUTO"
    Delay 0.1
    WriteMsg OSCOPE, "TRAC:DATA WMEM1, INP1" 'copy the input 1 data into scope memory
    'end baseline
 
    If inpMode = AUTO_SETUP Then
        WriteMsg OSCOPE, "SYST:AUT"
        LongDelay (2)
    Else
 
        'Activate the appropriate input(s)
        If ScopeInput < INPUT1 Or ScopeInput > INPUT1_AND_INPUT2 Then
            Echo "OSCOPE INPUT SOURCE ERROR"
            SetupScopeMeasure = -1
            Exit Function
        End If
        
        If ScopeInput = INPUT1 Or ScopeInput = INPUT1_AND_INPUT2 Then
                WriteMsg OSCOPE, "INP1 ON"
        End If
        
        If ScopeInput = INPUT2 Or ScopeInput = INPUT1_AND_INPUT2 Then
                WriteMsg OSCOPE, "INP2 ON"
        End If
        
        'set time range:10ns to 50s
        If (dTimeRange < 0.00000001 Or dTimeRange > 50) Then
            Echo "OSCOPE TIME RANGE ERROR"
            MsgBox "Time range error. Time range has to be between 10ns and 50s."
            SetupScopeMeasure = -2
            Exit Function
        Else
            WriteMsg OSCOPE, "SWE:TIME:RANG " & sTimeRange
        End If
        
        If (dVoltageRange < 0.008 Or dVoltageRange > 40) Then
            Echo "OSCOPE VOLT RANGE ERROR"
            MsgBox "Error setting the voltage range. It needs to be between .008 and 40V"
            SetupScopeMeasure = -3
            Exit Function
        End If
        
        If ScopeInput = INPUT1 Or ScopeInput = INPUT1_AND_INPUT2 Then
                WriteMsg OSCOPE, "VOLT1:RANG " & sVoltRange
        End If
        
        If ScopeInput = INPUT2 Or ScopeInput = INPUT1_AND_INPUT2 Then
                WriteMsg OSCOPE, "VOLT2:RANG " & sVoltRange
        End If
        
        'set the trigger level
        WriteMsg OSCOPE, "TRIG:LEV " & sTrigLevel
        
        Select Case Samples
        
            Case ScopeSamples.SAMPLES_500
                WriteMsg OSCOPE, "SWE:POIN 500"
                
            Case ScopeSamples.SAMPLES_8000
                WriteMsg OSCOPE, "SWE:POIN 8000"
                
            Case Else
                WriteMsg OSCOPE, "SWE:POIN 500"
                
        End Select
        
        Select Case Coupling
        
            Case InputCoupling.DC
                If ScopeInput = INPUT1 Or ScopeInput = INPUT1_AND_INPUT2 Then
                    WriteMsg OSCOPE, "INP1:COUP DC"
                End If
                
                If ScopeInput = INPUT2 Or ScopeInput = INPUT1_AND_INPUT2 Then
                    WriteMsg OSCOPE, "INP2:COUP DC"
                End If
                
            Case Else
                If ScopeInput = INPUT1 Or ScopeInput = INPUT1_AND_INPUT2 Then
                    WriteMsg OSCOPE, "INP1:COUP AC"
                End If
                
                If ScopeInput = INPUT2 Or ScopeInput = INPUT1_AND_INPUT2 Then
                    WriteMsg OSCOPE, "INP2:COUP AC"
                End If
                
        End Select
            
        Select Case Slope
        
            Case TriggerSlope.NEG
                WriteMsg OSCOPE, "TRIG:SLOP NEG"
                
            Case Else
                WriteMsg OSCOPE, "TRIG:SLOP POS"
                
        End Select
        
        Select Case TrigSource
        
            Case TriggerSource.TRG_SRC_INPUT1
                WriteMsg OSCOPE, "TRIG:SOUR INP1"
                
            Case TriggerSource.TRG_SRC_INPUT2
                WriteMsg OSCOPE, "TRIG:SOUR INP2"
                
            Case TriggerSource.TRG_SRC_EXTERNAL
                WriteMsg OSCOPE, "TRIG:SOUR EXT"
                
            Case TriggerSource.TRG_SRC_ECLT0
                WriteMsg OSCOPE, "TRIG:SOUR ECLT0"
                
            Case TriggerSource.TRG_SRC_ECLT1
                WriteMsg OSCOPE, "TRIG:SOUR ECLT1"
                
            Case Else
                Echo "OSCOPE TRIGGER SOURCE ERROR"
                SetupScopeMeasure = -1
                Exit Function
                
        End Select
        
        Select Case triggerPoint
        
            Case TimeDelayLink.TRIGGER_IN_CENTER
                WriteMsg OSCOPE, "SWE:TIME:DEL:LINK CENTER"
                
            Case TimeDelayLink.TRIGGER_IN_END
                WriteMsg OSCOPE, "SWE:TIME:DEL:LINK STOP"
                
            Case Else
                WriteMsg OSCOPE, "SWE:TIME:DEL:LINK START"
                
        End Select
        
        Select Case InpImpedance
        
            Case InputImpedance.OHM_50
                WriteMsg OSCOPE, "INP1:IMP 50"
                
            Case Else
                WriteMsg OSCOPE, "INP1:IMP 1000000"
                
        End Select
        
        Select Case nLPASS
        
            Case LowPass.LP_ACTIVE
              WriteMsg OSCOPE, "INP1:FILT:LPAS ON"
              
            Case Else
              WriteMsg OSCOPE, "INP1:FILT:LPAS OFF"
              
        End Select
        
        Select Case nHPASS
        
            Case HighPass.HP_ACTIVE
             WriteMsg OSCOPE, "INP1:FILT:hPAS ON"
             
            Case Else
             WriteMsg OSCOPE, "INP1:FILT:hPAS OFF"
             
        End Select
        
        Select Case nHYST
        
            Case Hysteresis.HYST_ACTIVE
             WriteMsg OSCOPE, "TRIG:HYST ON"
             
            Case Else
             WriteMsg OSCOPE, "TRIG:HYST OFF"
             
        End Select
        
        Select Case ON_OFF_AUTO
        
            Case TimeMode.TRIG_SINGLE_SHOT
                WriteMsg OSCOPE, "INIT:CONT OFF" 'trigger once on a single
                MsgBox "INIT:CONT OFF does not seem to work as predicted."
                
            Case TimeMode.TRIG_NORMAL
                WriteMsg OSCOPE, "INIT:CONT ON"  'keep triggering on a trigger. if no trigger then error
                
            Case Else
                WriteMsg OSCOPE, "INIT:CONT AUTO"
                
        End Select
    
    End If
    
    LongDelay (delayAfterSetupOfScope)
   
End Function

Private Function dCopyFromInputToMemory(Optional ByVal ScopeInput As InputSource = InputSource.INPUT1)
   
    If ScopeInput = INPUT1 Or ScopeInput = INPUT1_AND_INPUT2 Then
        WriteMsg OSCOPE, "TRAC:DATA WMEM1, INP1"
        Delay 0.3
    End If
        
    If ScopeInput = INPUT2 Or ScopeInput = INPUT1_AND_INPUT2 Then
        WriteMsg OSCOPE, "TRAC:DATA WMEM2, INP2"
        Delay 0.3
    End If
        
End Function

Public Function dGetWaveInfo(ByVal waveInput As MeasurementFunctionLayer, dInput1Result As Double, Optional dInput2Result As Double, _
                             Optional ByVal ScopeInput As InputSource = InputSource.INPUT1, _
                             Optional sDataFromCh1Buffer As String = "", _
                             Optional sDataFromCh2Buffer As String) As Double
   Dim Read As Double
   Dim sData As String
   Dim sReadMsg As String
   Dim nErr As Long
   Dim sCommand As String ' wave descriptor
   
   'waveInput - what type of measurement is desired ie Period, Frequency, etc. See
   '            the enum MeasurementFunctionLayer for full details.
   'dInput1Result, dInput2Result - Measurement from channel input 1,2 based on
   '                                the scope setup in SetupScopeMeasure
   'scopeInput - from which input on the scope should measurements be taken from
   '             INPUT1, INPUT2, or both
                

    sCommand = sMeasurementFunction(waveInput) ' Get appropriate SCPI string
                                              ' which represents a meausure query.
                                              ' This is setup during class initialization
                                              ' EX: sMeasurementFunction(SIGNAL_PERIOD) = "MEAS:VOLT:PER? (@WMEM"

    'The following overwrites the contents of WMEM1 with what is in INP1. This only needs
    'to be done the first time any measurement is made after the scope is setup.
    'If this is done more than once, then the scope needs to be setup every time
    'a measurement such as period, frequency, etc is made. This way, the scope is setup to trigger.
    'The TPS can then get the resulting waveform from the buffer and store it in memory. Then
    'the TPS can get various wave parameters such as period, frequency, amplitude without having to reset the scope.
   
    If blnScopeSetupFirstTime Then
        dCopyFromInputToMemory ScopeInput ' copies the contents from scope input(s) to memory
        Delay 1
        blnScopeSetupFirstTime = False
    End If
    
    WriteMsg OSCOPE, "INIT:CONT AUTO"  'change the scope to auto (ztec on the viper/t has a weird quirk)
    
    If ScopeInput = InputSource.INPUT1 Then
        WriteMsg OSCOPE, sCommand & "1)" '"MEAS:VOLT:xxx? (@WMEM1)"
        
        nErr = ReadMsg(OSCOPE, sData)
        
        If nErr = VI_ERROR_TMO Or sData = "" Then
            dGetWaveInfo = nErr
            sData = "9.9E39"
        ElseIf nErr <> 0 Then
            Echo "visa error: " & nErr
            MsgBox "Exiting function visa error: " & nErr
            dGetWaveInfo = nErr
            Exit Function
        End If
        
        dInput1Result = CDbl(sData)
        dInput2Result = 123456789
    
    ElseIf ScopeInput = InputSource.INPUT2 Then
        WriteMsg OSCOPE, sMeasurementFunction(waveInput) & "2)" '"MEAS:VOLT:xxx? (@WMEM2)"
        nErr = ReadMsg(OSCOPE, sData)
        
        If nErr = VI_ERROR_TMO Or sData = "" Then
            dGetWaveInfo = nErr
            sData = "9.9E39"
        ElseIf nErr <> 0 Then
            Echo "visa error: " & nErr
            MsgBox "Exiting function visa error: " & nErr
            dGetWaveInfo = nErr
            Exit Function
        End If
        
        dInput2Result = CDbl(sData)
        dInput1Result = 123456789
    
    ElseIf ScopeInput = InputSource.INPUT1_AND_INPUT2 Then
        WriteMsg OSCOPE, sMeasurementFunction(waveInput) & "1)" '"MEAS:VOLT:xxx? (@WMEM1)"
        Delay 0.1
        nErr = ReadMsg(OSCOPE, sData)
        
        If nErr = VI_ERROR_TMO Then
            dGetWaveInfo = nErr
            sData = "9.9E39"
        ElseIf nErr <> 0 Then
            Echo "visa error: " & nErr
            MsgBox "Exiting function visa error: " & nErr
            dGetWaveInfo = nErr
            Exit Function
        End If
        
        dInput1Result = CDbl(sData)
        WriteMsg OSCOPE, sMeasurementFunction(waveInput) & "2)" '"MEAS:VOLT:xxx? (@WMEM2)"
        Delay 0.1
        nErr = ReadMsg(OSCOPE, sData)
        
        If sData = "" Then sData = "9.9E39"
        
        If nErr = VI_ERROR_TMO Then
            dGetWaveInfo = nErr
            sData = "9.9E39"
        ElseIf nErr <> 0 Then
            Echo "visa error: " & nErr
            MsgBox "Exiting function visa error: " & nErr
            dGetWaveInfo = nErr
            Exit Function
        End If
        
        dInput2Result = CDbl(sData)
    End If
    
    WriteMsg OSCOPE, "*CLS"
    dGetWaveInfo = 0

End Function

Public Function UnpackWaveform(PREAMBLE As String, WaveData As String, waveFrm As WaveForm, XUnit$, YUnit$) As Variant

    'DESCRIPTION:
    ' This routine takes string data of a digitized wave form from an
    ' oscilloscope, and translates it to usable values.
    'PARAMETERS:
    ' Preamble$ = Preable data pertaining to the waveform in question.
    ' WaveData$ = Strind list of digitized waveform points
    ' Wave      = Variable to which the formatted data will be returned of the
    '             user defined type "Waveform" (defined in this module).  See
    '             declarations section form more information about this user
    '             defined type.
    'EXAMPLE:
    '               UnpackWaveform Preamble$, "#40010&^%KNBGFCC", Wave

    Dim Items%
    Dim SkippedBytes%
    Dim VPoint%
    Dim StringPosition%
    Dim MSByte!
    Dim LSByte%
    Dim Byte1%
    Dim BYTE_AFTER As Integer
    Dim TERMINATOR As String
    
    BYTE_AFTER = 0
    TERMINATOR = vbLf
    
    ReDim waveFrm.Magnitude(Len(WaveData$))
    WaveFormPoints = 0

    Items% = StringToList(PREAMBLE$, PreambleList$(), ",")

    waveFrm.Points% = Val(PreambleList$(DATA_POINTS))
    waveFrm.WFType% = Val(PreambleList$(WAVE_FORM_TYPE))
    waveFrm.Resolution% = Val(PreambleList$(DATA_FORMAT))

    waveFrm.XInterval# = Val(PreambleList$(X_INCREMENT))
    waveFrm.xOrigin# = Val(PreambleList$(XORIGN))
    waveFrm.xReference% = Val(PreambleList$(XREFERENC))
    waveFrm.XUnits$ = XUnit$

    waveFrm.YInterval# = Val(PreambleList$(Y_INCREMENT))
    waveFrm.yOrigin# = Val(PreambleList$(YORIGN))
    waveFrm.yReference% = Val(PreambleList$(YREFERENC))
    waveFrm.YUnits$ = YUnit$

    SkippedBytes% = 2 + Val(Mid$(WaveData$, 2, 1))
    VPoint% = 0

    If waveFrm.Resolution% = SIXTEEN_BITS Then
    
        For StringPosition% = BYTE_AFTER + SkippedBytes% To Len(WaveData$) - TERMINATOR - 1 Step 2
            MSByte! = Asc(Mid$(WaveData$, StringPosition%, 1))
            LSByte% = Asc(Mid$(WaveData$, StringPosition% + 1, 1))
            VPoint% = VPoint% + 1
            waveFrm.Magnitude!(VPoint%) = ((((256 * MSByte!) + LSByte%) - waveFrm.yReference%) * waveFrm.YInterval#) + waveFrm.yOrigin#
        Next StringPosition%
    Else
    
        For StringPosition% = BYTE_AFTER + SkippedBytes% To Len(WaveData$) - Len(TERMINATOR)
            Byte1% = Asc(Mid$(WaveData$, StringPosition%, 1))
            VPoint% = VPoint% + 1
            waveFrm.Magnitude!(VPoint%) = ((Byte1% - waveFrm.yReference) * waveFrm.YInterval#) + waveFrm.yOrigin#
        Next StringPosition%
    End If
    
    WaveFormPoints = VPoint%
    
End Function

Public Function dFetchFromBuffer(Optional ByVal ScopeInput As InputSource = InputSource.INPUT1, _
                        Optional sScopeBuffer As String, _
                        Optional sXunit As String = "s", _
                        Optional sYunit As String = "V") As WaveForm

   Dim Read As Double
   Dim sData As String
   Dim sReadMsg As String
   Dim nErr As Long
   Dim iArray() As Integer
   Dim readBuff$
   Dim strPreAmble As String
   Dim waveFrm As WaveForm
   Dim errorMessage$, iHandle&, Dummy&, seconds&, lRetCount&
   Dim sReadBuffer As String * 8100
   Dim sDataFromMem1$
   Dim sDataFromInp1$
   Dim i As Integer

  If ScopeInput = InputSource.WMEM1 Then
    iHandle& = nInstrumentHandle(OSCOPE)
    sReadBuffer = ""
    WriteMsg OSCOPE, "TRACE:PRE? WMEM1"
    Delay 0.1
    nErr = ReadMsg(OSCOPE, strPreAmble) '
    Delay 0.1
    WriteMsg OSCOPE, "TRACE:DATA? WMEM1"
    Delay 0.1
    nErr = viRead(iHandle&, sReadBuffer, 8100, lRetCount)
    sDataFromMem1 = Left(sReadBuffer, lRetCount)
    
    If nErr = 0 And lRetCount > 0 Then
       UnpackWaveform strPreAmble, sDataFromMem1$, waveFrm, sXunit, sYunit
    Else
        ReDim waveFrm.Magnitude(WaveFormPoints + 10)
        
        For i = 1 To WaveFormPoints
            waveFrm.Magnitude(i) = 99
        Next
    End If
    
    If nErr <> 0 Then
        Echo "visa error: " & nErr
        MsgBox "Exiting function visa error: " & nErr
        Exit Function
    End If
    
    dFetchFromBuffer = waveFrm
    
  ElseIf ScopeInput = InputSource.WMEM2 Then
    iHandle& = nInstrumentHandle(OSCOPE)
    sReadBuffer = ""
    WriteMsg OSCOPE, "TRACE:PRE? WMEM2"
    Delay 0.1
    nErr = ReadMsg(OSCOPE, strPreAmble) '
    Delay 0.1
    WriteMsg OSCOPE, "TRACE:DATA WMEM1, WMEM2"
    Delay 0.1
    WriteMsg OSCOPE, "TRACE:DATA? WMEM1"
    Delay 0.1
    nErr = viRead(iHandle&, sReadBuffer, 8100, lRetCount)
    sDataFromMem1 = Left(sReadBuffer, lRetCount)
    
    If nErr = 0 And lRetCount > 0 Then
       UnpackWaveform strPreAmble, sDataFromMem1$, waveFrm, sXunit, sYunit
    Else
        ReDim waveFrm.Magnitude(WaveFormPoints + 10)
        
        For i = 1 To WaveFormPoints
            waveFrm.Magnitude(i) = 99
        Next
    End If
    
    If nErr <> 0 Then
        Echo "visa error: " & nErr
        MsgBox "Exiting function visa error: " & nErr
        Exit Function
    End If
    
    dFetchFromBuffer = waveFrm
    
  ElseIf ScopeInput = InputSource.INPUT1 Then
    iHandle& = nInstrumentHandle(OSCOPE)
    sReadBuffer = ""
    WriteMsg OSCOPE, "TRACE:PRE? INP1"
    Delay 0.1
    nErr = ReadMsg(OSCOPE, strPreAmble) '
    Delay 0.1
    WriteMsg OSCOPE, "TRACE:DATA WMEM1, INP1"
    Delay 0.1
    WriteMsg OSCOPE, "TRACE:DATA? WMEM1"
    Delay 0.1
    nErr = viRead(iHandle&, sReadBuffer, 8100, lRetCount)
    sDataFromInp1 = Left(sReadBuffer, lRetCount)
    
    If nErr = 0 And lRetCount > 0 Then
       UnpackWaveform strPreAmble, sDataFromInp1$, waveFrm, sXunit, sYunit
    Else
        ReDim waveFrm.Magnitude(WaveFormPoints + 10)
        For i = 1 To WaveFormPoints
            waveFrm.Magnitude(i) = 99
        Next
    End If
    
    If nErr <> 0 Then
        Echo "visa error: " & nErr
        MsgBox "Exiting function visa error: " & nErr
        Exit Function
    End If
    
    dFetchFromBuffer = waveFrm
    
   ElseIf ScopeInput = InputSource.INPUT2 Then
    iHandle& = nInstrumentHandle(OSCOPE)
    sReadBuffer = ""
    WriteMsg OSCOPE, "TRACE:PRE? INP2"
    Delay 0.1
    nErr = ReadMsg(OSCOPE, strPreAmble) '
    Delay 0.1
    WriteMsg OSCOPE, "TRACE:DATA WMEM1, INP2"
    Delay 0.1
    WriteMsg OSCOPE, "TRACE:DATA? WMEM1"
    Delay 0.1
    nErr = viRead(iHandle&, sReadBuffer, 8100, lRetCount)
    sDataFromInp1 = Left(sReadBuffer, lRetCount)
    
    If nErr = 0 And lRetCount > 0 Then
       UnpackWaveform strPreAmble, sDataFromInp1$, waveFrm, sXunit, sYunit
    Else
        ReDim waveFrm.Magnitude(WaveFormPoints + 10)
        For i = 1 To WaveFormPoints
            waveFrm.Magnitude(i) = 99
        Next
    End If
    
    If nErr <> 0 Then
        Echo "visa error: " & nErr
        MsgBox "Exiting function visa error: " & nErr
        Exit Function
    End If
    
    dFetchFromBuffer = waveFrm
  End If
  
  sScopeBuffer = sReadBuffer
  WriteMsg OSCOPE, "*CLS"
  
End Function

Public Function dPeriod(dInput1Result As Double, Optional dInput2Result As Double, Optional ByVal ScopeInput As InputSource = InputSource.INPUT1) As Double

    dPeriod = dGetWaveInfo(SIGNAL_PERIOD, dInput1Result, dInput2Result, ScopeInput)

End Function

Public Function dFrequency(dInput1Result As Double, Optional dInput2Result As Double, Optional ByVal ScopeInput As InputSource = InputSource.INPUT1) As Double

    dFrequency = dGetWaveInfo(SIGNAL_FREQUENCY, dInput1Result, dInput2Result, ScopeInput)

End Function

Public Function dAC(dInput1Result As Double, Optional dInput2Result As Double, Optional ByVal ScopeInput As InputSource = InputSource.INPUT1) As Double

    dAC = dGetWaveInfo(SIGNAL_AC, dInput1Result, dInput2Result, ScopeInput)

End Function

Public Function dAmplitude(dInput1Result As Double, Optional dInput2Result As Double, Optional ByVal ScopeInput As InputSource = InputSource.INPUT1) As Double

    dAmplitude = dGetWaveInfo(SIGNAL_AMPLITUDE, dInput1Result, dInput2Result, ScopeInput)

End Function

Public Function dDC(dInput1Result As Double, Optional dInput2Result As Double = 0, Optional ByVal ScopeInput As InputSource = InputSource.INPUT1) As Double

    Dim reading As Double
    'dDC = dGetWaveInfo(SIGNAL_DC, dInput1Result, dInput2Result, ScopeInput)
    'TODO: This is a temporary implementation. Will need to eventually update
    
    dInput1Result = dMeasDC("AUT", "INP1", 40, 0.1, 1000000, "OFF")
    dInput2Result = dMeasDC("AUT", "INP2", 40, 0.1, 1000000, "OFF")

End Function

Public Function dDutyCycle(dInput1Result As Double, Optional dInput2Result As Double, Optional ByVal ScopeInput As InputSource = InputSource.INPUT1) As Double

    dDutyCycle = dGetWaveInfo(SIGNAL_DUTY_CYCLE, dInput1Result, dInput2Result, ScopeInput)

End Function

Public Function dFallOvershoot(dInput1Result As Double, Optional dInput2Result As Double, Optional ByVal ScopeInput As InputSource = InputSource.INPUT1) As Double

    dFallOvershoot = dGetWaveInfo(SIGNAL_FALL_OVERSHOOT, dInput1Result, dInput2Result, ScopeInput)

End Function

Public Function dFallPreShoot(dInput1Result As Double, Optional dInput2Result As Double, Optional ByVal ScopeInput As InputSource = InputSource.INPUT1) As Double

    dFallPreShoot = dGetWaveInfo(SIGNAL_FALL_PRESHOOT, dInput1Result, dInput2Result, ScopeInput)

End Function

Public Function dFallTime(dInput1Result As Double, Optional dInput2Result As Double, Optional ByVal ScopeInput As InputSource = InputSource.INPUT1) As Double

    dFallTime = dGetWaveInfo(SIGNAL_FALL_TIME, dInput1Result, dInput2Result, ScopeInput)

End Function

Public Function dHighVoltage(dInput1Result As Double, Optional dInput2Result As Double, Optional ByVal ScopeInput As InputSource = InputSource.INPUT1) As Double

    dHighVoltage = dGetWaveInfo(SIGNAL_HIGH_VOLT, dInput1Result, dInput2Result, ScopeInput)

End Function

Public Function dLowVoltage(dInput1Result As Double, Optional dInput2Result As Double, Optional ByVal ScopeInput As InputSource = InputSource.INPUT1) As Double

    dLowVoltage = dGetWaveInfo(SIGNAL_LOW_VOLT, dInput1Result, dInput2Result, ScopeInput)

End Function

Public Function dVoltageMax(dInput1Result As Double, Optional dInput2Result As Double, Optional ByVal ScopeInput As InputSource = InputSource.INPUT1) As Double

    dVoltageMax = dGetWaveInfo(SIGNAL_MAX_VOLT, dInput1Result, dInput2Result, ScopeInput)

End Function

Public Function dVoltageMin(dInput1Result As Double, Optional dInput2Result As Double, Optional ByVal ScopeInput As InputSource = InputSource.INPUT1) As Double

    dVoltageMin = dGetWaveInfo(SIGNAL_MIN_VOLT, dInput1Result, dInput2Result, ScopeInput)

End Function

Public Function dNegDutyCycle(dInput1Result As Double, Optional dInput2Result As Double, Optional ByVal ScopeInput As InputSource = InputSource.INPUT1) As Double

    dNegDutyCycle = dGetWaveInfo(SIGNAL_NEG_DUTY, dInput1Result, dInput2Result, ScopeInput)

End Function

Public Function dNegativePulseWidth(dInput1Result As Double, Optional dInput2Result As Double, Optional ByVal ScopeInput As InputSource = InputSource.INPUT1) As Double

    dNegativePulseWidth = dGetWaveInfo(SIGNAL_NEG_PULSE_WIDTH, dInput1Result, dInput2Result, ScopeInput)

End Function

Public Function dPositiveDutyCycle(dInput1Result As Double, Optional dInput2Result As Double, Optional ByVal ScopeInput As InputSource = InputSource.INPUT1) As Double

    dPositiveDutyCycle = dGetWaveInfo(SIGNAL_POS_DUTY, dInput1Result, dInput2Result, ScopeInput)

End Function

Public Function dPositivePulseWidth(dInput1Result As Double, Optional dInput2Result As Double, Optional ByVal ScopeInput As InputSource = InputSource.INPUT1) As Double

    dPositivePulseWidth = dGetWaveInfo(SIGNAL_POS_PULSE_WIDTH, dInput1Result, dInput2Result, ScopeInput)

End Function

Public Function dRiseOvershoot(dInput1Result As Double, Optional dInput2Result As Double, Optional ByVal ScopeInput As InputSource = InputSource.INPUT1) As Double

    dRiseOvershoot = dGetWaveInfo(SIGNAL_RISE_OVERSHOOT, dInput1Result, dInput2Result, ScopeInput)

End Function

Public Function dRisePreShoot(dInput1Result As Double, Optional dInput2Result As Double, Optional ByVal ScopeInput As InputSource = InputSource.INPUT1) As Double

    dRisePreShoot = dGetWaveInfo(SIGNAL_RISE_PRESHOOT, dInput1Result, dInput2Result, ScopeInput)

End Function

Public Function dRiseTime(dInput1Result As Double, Optional dInput2Result As Double, Optional ByVal ScopeInput As InputSource = InputSource.INPUT1) As Double

    dRiseTime = dGetWaveInfo(SIGNAL_RISE_TIME, dInput1Result, dInput2Result, ScopeInput)

End Function

Public Function dTimeAtMax(dInput1Result As Double, Optional dInput2Result As Double, Optional ByVal ScopeInput As InputSource = InputSource.INPUT1) As Double

    dTimeAtMax = dGetWaveInfo(SIGNAL_TIME_AT_MAX_VOLTAGE, dInput1Result, dInput2Result, ScopeInput)

End Function

Public Function dTimeAtMin(dInput1Result As Double, Optional dInput2Result As Double, Optional ByVal ScopeInput As InputSource = InputSource.INPUT1) As Double

    dTimeAtMin = dGetWaveInfo(SIGNAL_TIME_AT_MIN_VOLTAGE, dInput1Result, dInput2Result, ScopeInput)

End Function
