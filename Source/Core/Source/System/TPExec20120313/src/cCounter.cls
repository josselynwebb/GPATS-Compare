VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "cCounter"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
DefInt A-Z

'bbbb STR10606 #4
'dMeasFreq Checked for empty string, returned 9.91E37

'bbbb 2012-03-07 STR10606 #9
'Added loop in Aprobe routine to wait on Probe button release

'bbbb 2012-03-08 STR10606 #13
'Added new dMeasTimeInterval2 command to both ViperT and TetsB.
'Changed dMeasTimeInterval to select common routing
' if that was specified in the header

Public Sub SendMsg(sMsg As String)
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    frmMain.txtInstrument.Text = "COUNTER"
    frmMain.txtCommand.Text = "SendMsg"
    WriteMsg COUNTER, sMsg
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
End Sub

Public Function sReadMsg(sMsg As String) As String
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    frmMain.txtInstrument.Text = "COUNTER"
    frmMain.txtCommand.Text = "ReadMsg"
    ReadMsg COUNTER, sReadMsg
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
End Function

Public Function dMeasFetch(Optional nchannel As Integer = 1) As Double
    
    Dim sData As String
    Dim nErr As Long
    Dim Q As String
    Q = Chr$(34)

    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    frmMain.txtInstrument.Text = "Counter"
    frmMain.txtCommand.Text = "dMeasFetch"
    Dim sCurrentMsg As String
        
    'Error Check Arguments
    If nchannel < 1 Or nchannel > 2 Then
        Echo "COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasFreq nChannel argument out of range."
        Echo "RANGE: 1 TO 2"
        dMeasFetch = 9.9E+37
        Err.Raise -1001
        Exit Function
    End If
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    
    If Not bSimulation Then
        WriteMsg COUNTER, "FETCH" & CStr(nchannel) & "?"
        nErr = ReadMsg(COUNTER, sData)
        If nErr <> 0 Then
            dMeasFetch = 9.9E+37: frmMain.txtMeasured.Text = 9.9E+37
            Err.Raise nErr
            Exit Function
        End If
        dMeasFetch = CDbl(sData)
        frmMain.txtMeasured.Text = CSng(dMeasFetch)
        If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    Else
        dMeasFetch = CDbl(InputBox("Command cmdCOUNTER.dMeasFetch peformed." & vbCrLf & "Enter Value:", "SIMULATION MODE"))
        frmMain.txtMeasured.Text = CSng(dMeasFetch)
        If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON
    End If
End Function

Public Function dMeasFallTime(Optional dRange As Double = 0.0000001, Optional bChOne10XAttenuation As Boolean = False, _
                              Optional bChTwo10XAttenuation As Boolean = False, Optional bChOneACCoupling As Boolean = False, _
                              Optional bChTwoACCoupling As Boolean = False, Optional bChOne50ohms As Boolean = False, _
                              Optional bChTwo50ohms As Boolean = False, Optional sHysteresis As String = "DEF", _
                              Optional bAutoTrigger As Boolean = False, Optional nRelativeTriggerLevel As Long = 50, _
                              Optional bTriggerSlopeNegative As Boolean = False, Optional dAbsoluteTrigger As Double = 0#, _
                              Optional bTriggerOutputOn As Boolean = False, Optional nTTLTriggerOutput As Long = 0, _
                              Optional sArmStartSource As String = "IMMEDIATE", Optional bArmStartSlopNegative As Boolean = False, _
                              Optional sArmStartLevel As String = "TTL", Optional SArmStopSource As String = "IMMEDIATE", _
                              Optional bArmStopSlopNegative As Boolean = False, Optional sArmStopLevel As String = "TTL", _
                              Optional sTimeBaseSource As String = "INTERNAL", Optional bOutputTimeBase As Boolean = False, _
                              Optional bTotalizeGateMeasure As Boolean = False, Optional bTotalizeGatePolarityInverted As Boolean = False, _
                              Optional bTimeIntervalDelay As Boolean = False, Optional dTimeIntervalDelay As Double = 0.1, _
                              Optional sAcquisitionTimeOutSetting As String = "ON", Optional dAcquisitionTimeOut As Double = 5#, _
                              Optional APROBE As String = "OFF") As Double

    Dim nchannel As Integer
    Dim sCurrentMsg As String
    Dim dCheckReturn As Double
    
    nchannel = 1
        
    sCurrentMsg = frmMain.lblStatus.Caption
    
    dCheckReturn = CheckArguments(dRange, "RANGE: 15E-9 TO 1E-3", 0.000000015, 0.03, _
                                  nchannel, sHysteresis, nRelativeTriggerLevel, _
                                  dAbsoluteTrigger, nTTLTriggerOutput, sArmStartSource, _
                                  sArmStartLevel, SArmStopSource, sArmStopLevel, _
                                  sTimeBaseSource, dTimeIntervalDelay, 0.1, _
                                  sAcquisitionTimeOutSetting, dAcquisitionTimeOut, _
                                  APROBE, "dMeasFallTime", True)
                                     
    If dCheckReturn = 9.9E+37 Then
        dMeasFallTime = dCheckReturn
        Exit Function
    End If
        
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
        
    If Not bSimulation Then
      
        dCheckReturn = dSetupInstrument(nchannel, dRange, 0.1, _
                                        bChOne10XAttenuation, bChTwo10XAttenuation, _
                                        bChOneACCoupling, bChTwoACCoupling, _
                                        bChOne50ohms, bChTwo50ohms, _
                                        sHysteresis, bAutoTrigger, _
                                        nRelativeTriggerLevel, bTriggerSlopeNegative, _
                                        dAbsoluteTrigger, bTriggerOutputOn, _
                                        nTTLTriggerOutput, sArmStartSource, _
                                        bArmStartSlopNegative, sArmStartLevel, _
                                        SArmStopSource, bArmStopSlopNegative, _
                                        sArmStopLevel, sTimeBaseSource, _
                                        bTotalizeGateMeasure, bTotalizeGatePolarityInverted, _
                                        bTimeIntervalDelay, dTimeIntervalDelay, _
                                        sAcquisitionTimeOutSetting, dAcquisitionTimeOut, _
                                        True, "FTIM 90, 10, " & CStr(dRange) & "", _
                                        "FTIM", False, True, True)
                                  
        If dCheckReturn = 9.9E+37 Then
            dMeasFallTime = dCheckReturn
            Exit Function
        End If
        
        If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
            
        dMeasFallTime = dDoTheMeasurement(APROBE, nchannel)
        
        frmMain.lblStatus.Caption = sCurrentMsg
    Else
        dMeasFallTime = CDbl(InputBox("Command cmdCOUNTER.dMeasFallTime peformed." & _
        vbCrLf & "Enter Value:", "SIMULATION MODE"))
        frmMain.txtMeasured.Text = CSng(dMeasFallTime)
        
        If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON
        
    End If
    
End Function

Public Function dMeasTotalize(Optional nchannel As Integer = 1, Optional bChOne10XAttenuation As Boolean = False, _
                              Optional bChTwo10XAttenuation As Boolean = False, Optional bChOneACCoupling As Boolean = False, _
                              Optional bChTwoACCoupling As Boolean = False, Optional bChOne50ohms As Boolean = False, _
                              Optional bChTwo50ohms As Boolean = False, Optional sHysteresis As String = "DEF", _
                              Optional bAutoTrigger As Boolean = False, Optional nRelativeTriggerLevel As Long = 50, _
                              Optional bTriggerSlopeNegative As Boolean = False, Optional dAbsoluteTrigger As Double = 0#, _
                              Optional bTriggerOutputOn As Boolean = False, Optional nTTLTriggerOutput As Long = 0, _
                              Optional sArmStartSource As String = "IMMEDIATE", Optional bArmStartSlopNegative As Boolean = False, _
                              Optional sArmStartLevel As String = "TTL", Optional SArmStopSource As String = "IMMEDIATE", _
                              Optional bArmStopSlopNegative As Boolean = False, Optional sArmStopLevel As String = "TTL", _
                              Optional sTimeBaseSource As String = "INTERNAL", Optional bOutputTimeBase As Boolean = False, _
                              Optional bTotalizeGateMeasure As Boolean = False, Optional bTotalizeGatePolarityInverted As Boolean = False, _
                              Optional bTimeIntervalDelay As Boolean = False, Optional dTimeIntervalDelay As Double = 0.1, _
                              Optional sAcquisitionTimeOutSetting As String = "ON", Optional dAcquisitionTimeOut As Double = 5#, _
                              Optional APROBE As String = "OFF") As Double

    Dim sCurrentMsg As String
    Dim dCheckReturn As Double
    
    sCurrentMsg = frmMain.lblStatus.Caption
    
    dCheckReturn = CheckArguments(-1#, "NONE", -1#, -1#, _
                                  nchannel, sHysteresis, nRelativeTriggerLevel, _
                                  dAbsoluteTrigger, nTTLTriggerOutput, sArmStartSource, _
                                  sArmStartLevel, SArmStopSource, sArmStopLevel, _
                                  sTimeBaseSource, dTimeIntervalDelay, 0.1, _
                                  sAcquisitionTimeOutSetting, dAcquisitionTimeOut, _
                                  APROBE, "dMeasTotalize", True)
    
    If dCheckReturn = 9.9E+37 Then
        dMeasTotalize = dCheckReturn
        Exit Function
    End If
        
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
        
    If Not bSimulation Then
      
        dCheckReturn = dSetupInstrument(nchannel, -1#, 0.1, _
                                        bChOne10XAttenuation, bChTwo10XAttenuation, _
                                        bChOneACCoupling, bChTwoACCoupling, _
                                        bChOne50ohms, bChTwo50ohms, _
                                        sHysteresis, bAutoTrigger, _
                                        nRelativeTriggerLevel, bTriggerSlopeNegative, _
                                        dAbsoluteTrigger, bTriggerOutputOn, _
                                        nTTLTriggerOutput, sArmStartSource, _
                                        bArmStartSlopNegative, sArmStartLevel, _
                                        SArmStopSource, bArmStopSlopNegative, _
                                        sArmStopLevel, sTimeBaseSource, _
                                        bTotalizeGateMeasure, bTotalizeGatePolarityInverted, _
                                        bTimeIntervalDelay, dTimeIntervalDelay, _
                                        sAcquisitionTimeOutSetting, dAcquisitionTimeOut, _
                                        False, "TOT", "TOT", False, True, True)
                                  
        If dCheckReturn = 9.9E+37 Then
            dMeasTotalize = dCheckReturn
            Exit Function
        End If
        
        If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
        
        dMeasTotalize = dDoTheMeasurement(APROBE, nchannel)
        
        frmMain.lblStatus.Caption = sCurrentMsg
    Else
        dMeasTotalize = CDbl(InputBox("Command cmdCOUNTER.dMeasTotalize peformed." & _
        vbCrLf & "Enter Value:", "SIMULATION MODE"))
        frmMain.txtMeasured.Text = CSng(dMeasTotalize)
        
        If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON
        
    End If
    
End Function

Public Function dMeasPhase(Optional bChOne10XAttenuation As Boolean = False, Optional bChTwo10XAttenuation As Boolean = False, Optional bChOneACCoupling As Boolean = False, _
                           Optional bChTwoACCoupling As Boolean = False, Optional bChOne50ohms As Boolean = False, _
                           Optional bChTwo50ohms As Boolean = False, Optional sHysteresis As String = "DEF", _
                           Optional bAutoTrigger As Boolean = False, Optional nRelativeTriggerLevel As Long = 50, _
                           Optional bTriggerSlopeNegative As Boolean = False, Optional dAbsoluteTrigger As Double = 0#, _
                           Optional bTriggerOutputOn As Boolean = False, Optional nTTLTriggerOutput As Long = 0, _
                           Optional sArmStartSource As String = "IMMEDIATE", Optional bArmStartSlopNegative As Boolean = False, _
                           Optional sArmStartLevel As String = "TTL", Optional SArmStopSource As String = "IMMEDIATE", _
                           Optional bArmStopSlopNegative As Boolean = False, Optional sArmStopLevel As String = "TTL", _
                           Optional sTimeBaseSource As String = "INTERNAL", Optional bOutputTimeBase As Boolean = False, _
                           Optional bTotalizeGateMeasure As Boolean = False, Optional bTotalizeGatePolarityInverted As Boolean = False, _
                           Optional bTimeIntervalDelay As Boolean = False, Optional dTimeIntervalDelay As Double = 0.1, _
                           Optional sAcquisitionTimeOutSetting As String = "ON", Optional dAcquisitionTimeOut As Double = 5#, _
                           Optional APROBE As String = "OFF") As Double

    Dim sCurrentMsg As String
    Dim dCheckReturn As Double
    Dim nchannel As Integer
    
    nchannel = 1
    
    sCurrentMsg = frmMain.lblStatus.Caption
    
    dCheckReturn = CheckArguments(-1#, "NONE", -1#, -1#, _
                                  nchannel, sHysteresis, nRelativeTriggerLevel, _
                                  dAbsoluteTrigger, nTTLTriggerOutput, sArmStartSource, _
                                  sArmStartLevel, SArmStopSource, sArmStopLevel, _
                                  sTimeBaseSource, dTimeIntervalDelay, 0.1, _
                                  sAcquisitionTimeOutSetting, dAcquisitionTimeOut, _
                                  APROBE, "dMeasPhase", True)
        
    If dCheckReturn = 9.9E+37 Then
        dMeasPhase = dCheckReturn
        Exit Function
    End If
        
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
        
    If Not bSimulation Then
      
        dCheckReturn = dSetupInstrument(nchannel, -1#, 0.1, _
                                        bChOne10XAttenuation, bChTwo10XAttenuation, _
                                        bChOneACCoupling, bChTwoACCoupling, _
                                        bChOne50ohms, bChTwo50ohms, _
                                        sHysteresis, bAutoTrigger, _
                                        nRelativeTriggerLevel, bTriggerSlopeNegative, _
                                        dAbsoluteTrigger, bTriggerOutputOn, _
                                        nTTLTriggerOutput, sArmStartSource, _
                                        bArmStartSlopNegative, sArmStartLevel, _
                                        SArmStopSource, bArmStopSlopNegative, _
                                        sArmStopLevel, sTimeBaseSource, _
                                        bTotalizeGateMeasure, bTotalizeGatePolarityInverted, _
                                        bTimeIntervalDelay, dTimeIntervalDelay, _
                                        sAcquisitionTimeOutSetting, dAcquisitionTimeOut, _
                                        True, "PHAS  1", "PHAS", False, True, True)
                                  
        If dCheckReturn = 9.9E+37 Then
            dMeasPhase = dCheckReturn
            Exit Function
        End If
        
        If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
        
        dMeasPhase = dDoTheMeasurement(APROBE, nchannel)
        
        frmMain.lblStatus.Caption = sCurrentMsg
    Else
        dMeasPhase = CDbl(InputBox("Command cmdCOUNTER.dMeasPhase peformed." & _
        vbCrLf & "Enter Value:", "SIMULATION MODE"))
        frmMain.txtMeasured.Text = CSng(dMeasPhase)
        
        If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON
        
    End If
    
End Function

Public Function dMeasRiseTime(Optional dRange As Double = 0.0000001, Optional bChOne10XAttenuation As Boolean = False, _
                              Optional bChTwo10XAttenuation As Boolean = False, Optional bChOneACCoupling As Boolean = False, _
                              Optional bChTwoACCoupling As Boolean = False, Optional bChOne50ohms As Boolean = False, _
                              Optional bChTwo50ohms As Boolean = False, Optional sHysteresis As String = "DEF", _
                              Optional bAutoTrigger As Boolean = False, Optional nRelativeTriggerLevel As Long = 50, _
                              Optional bTriggerSlopeNegative As Boolean = False, Optional dAbsoluteTrigger As Double = 0#, _
                              Optional bTriggerOutputOn As Boolean = False, Optional nTTLTriggerOutput As Long = 0, _
                              Optional sArmStartSource As String = "IMMEDIATE", Optional bArmStartSlopNegative As Boolean = False, _
                              Optional sArmStartLevel As String = "TTL", Optional SArmStopSource As String = "IMMEDIATE", _
                              Optional bArmStopSlopNegative As Boolean = False, Optional sArmStopLevel As String = "TTL", _
                              Optional sTimeBaseSource As String = "INTERNAL", Optional bOutputTimeBase As Boolean = False, _
                              Optional bTotalizeGateMeasure As Boolean = False, Optional bTotalizeGatePolarityInverted As Boolean = False, _
                              Optional bTimeIntervalDelay As Boolean = False, Optional dTimeIntervalDelay As Double = 0.1, _
                              Optional sAcquisitionTimeOutSetting As String = "ON", Optional dAcquisitionTimeOut As Double = 5#, _
                              Optional APROBE As String = "OFF") As Double

    Dim nchannel As Integer
    Dim sCurrentMsg As String
    Dim dCheckReturn As Double
    
    nchannel = 1
                
    sCurrentMsg = frmMain.lblStatus.Caption
    
    dCheckReturn = CheckArguments(dRange, "RANGE: 15E-9 TO 1E-3", 0.000000015, 0.03, _
                                  nchannel, sHysteresis, nRelativeTriggerLevel, _
                                  dAbsoluteTrigger, nTTLTriggerOutput, sArmStartSource, _
                                  sArmStartLevel, SArmStopSource, sArmStopLevel, _
                                  sTimeBaseSource, dTimeIntervalDelay, 0.1, _
                                  sAcquisitionTimeOutSetting, dAcquisitionTimeOut, _
                                  APROBE, "dMeasRiseTime", True)
        
    If dCheckReturn = 9.9E+37 Then
        dMeasRiseTime = dCheckReturn
        Exit Function
    End If
        
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
        
    If Not bSimulation Then
      
        dCheckReturn = dSetupInstrument(nchannel, dRange, 0.1, _
                                        bChOne10XAttenuation, bChTwo10XAttenuation, _
                                        bChOneACCoupling, bChTwoACCoupling, _
                                        bChOne50ohms, bChTwo50ohms, _
                                        sHysteresis, bAutoTrigger, _
                                        nRelativeTriggerLevel, bTriggerSlopeNegative, _
                                        dAbsoluteTrigger, bTriggerOutputOn, _
                                        nTTLTriggerOutput, sArmStartSource, _
                                        bArmStartSlopNegative, sArmStartLevel, _
                                        SArmStopSource, bArmStopSlopNegative, _
                                        sArmStopLevel, sTimeBaseSource, _
                                        bTotalizeGateMeasure, bTotalizeGatePolarityInverted, _
                                        bTimeIntervalDelay, dTimeIntervalDelay, _
                                        sAcquisitionTimeOutSetting, dAcquisitionTimeOut, _
                                        True, "RTIM 90, 10, " & CStr(dRange) & "", _
                                        "RTIM", False, True, True)
                                  
        If dCheckReturn = 9.9E+37 Then
            dMeasRiseTime = dCheckReturn
            Exit Function
        End If
        
        If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
            
        dMeasRiseTime = dDoTheMeasurement(APROBE, nchannel)
        
        frmMain.lblStatus.Caption = sCurrentMsg
    Else
        dMeasRiseTime = CDbl(InputBox("Command cmdCOUNTER.dMeasRiseTime peformed." & _
        vbCrLf & "Enter Value:", "SIMULATION MODE"))
        frmMain.txtMeasured.Text = CSng(dMeasRiseTime)
        
        If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON
        
    End If
    
End Function

Public Function dMeasPeriod(Optional nchannel As Integer = 1, Optional dRange As Double = 0.0000001, _
                            Optional dAperature As Double = 0.1, Optional bChOne10XAttenuation As Boolean = False, _
                            Optional bChTwo10XAttenuation As Boolean = False, Optional bChOneACCoupling As Boolean = False, _
                            Optional bChTwoACCoupling As Boolean = False, Optional bChOne50ohms As Boolean = False, _
                            Optional bChTwo50ohms As Boolean = False, Optional sHysteresis As String = "DEF", _
                            Optional bAutoTrigger As Boolean = False, Optional nRelativeTriggerLevel As Long = 50, _
                            Optional bTriggerSlopeNegative As Boolean = False, Optional dAbsoluteTrigger As Double = 0#, _
                            Optional bTriggerOutputOn As Boolean = False, Optional nTTLTriggerOutput As Long = 0, _
                            Optional sArmStartSource As String = "IMMEDIATE", Optional bArmStartSlopNegative As Boolean = False, _
                            Optional sArmStartLevel As String = "TTL", Optional SArmStopSource As String = "IMMEDIATE", _
                            Optional bArmStopSlopNegative As Boolean = False, Optional sArmStopLevel As String = "TTL", _
                            Optional sTimeBaseSource As String = "INTERNAL", Optional bOutputTimeBase As Boolean = False, _
                            Optional bTotalizeGateMeasure As Boolean = False, Optional bTotalizeGatePolarityInverted As Boolean = False, _
                            Optional bTimeIntervalDelay As Boolean = False, Optional dTimeIntervalDelay As Double = 0.1, _
                            Optional sAcquisitionTimeOutSetting As String = "ON", Optional dAcquisitionTimeOut As Double = 5#, _
                            Optional APROBE As String = "OFF") As Double

    Dim sCurrentMsg As String
    Dim dCheckReturn As Double
    
        
    sCurrentMsg = frmMain.lblStatus.Caption
    
    dCheckReturn = CheckArguments(dRange, "RANGE: 5E-9 TO 1000", 0.000000005, 1000, _
                                  nchannel, sHysteresis, nRelativeTriggerLevel, _
                                  dAbsoluteTrigger, nTTLTriggerOutput, sArmStartSource, _
                                  sArmStartLevel, SArmStopSource, sArmStopLevel, _
                                  sTimeBaseSource, dTimeIntervalDelay, dAperature, _
                                  sAcquisitionTimeOutSetting, dAcquisitionTimeOut, _
                                  APROBE, "dMeasPeriod", True)
        
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
        
    If dCheckReturn = 9.9E+37 Then
        dMeasPeriod = dCheckReturn
        Exit Function
    End If
        
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
        
    If Not bSimulation Then
      
        dCheckReturn = dSetupInstrument(nchannel, dRange, dAperature, _
                                        bChOne10XAttenuation, bChTwo10XAttenuation, _
                                        bChOneACCoupling, bChTwoACCoupling, _
                                        bChOne50ohms, bChTwo50ohms, _
                                        sHysteresis, bAutoTrigger, _
                                        nRelativeTriggerLevel, bTriggerSlopeNegative, _
                                        dAbsoluteTrigger, bTriggerOutputOn, _
                                        nTTLTriggerOutput, sArmStartSource, _
                                        bArmStartSlopNegative, sArmStartLevel, _
                                        SArmStopSource, bArmStopSlopNegative, _
                                        sArmStopLevel, sTimeBaseSource, _
                                        bTotalizeGateMeasure, bTotalizeGatePolarityInverted, _
                                        bTimeIntervalDelay, dTimeIntervalDelay, _
                                        sAcquisitionTimeOutSetting, dAcquisitionTimeOut, _
                                        True, "PER " & CStr(dRange) & "", "PER", False, True, True)
                                  
        If dCheckReturn = 9.9E+37 Then
            dMeasPeriod = dCheckReturn
            Exit Function
        End If
        
        If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
            
        dMeasPeriod = dDoTheMeasurement(APROBE, nchannel)
        
        frmMain.lblStatus.Caption = sCurrentMsg
    Else
        dMeasPeriod = CDbl(InputBox("Command cmdCOUNTER.dMeasPeriod peformed." & _
        vbCrLf & "Enter Value:", "SIMULATION MODE"))
        frmMain.txtMeasured.Text = CSng(dMeasPeriod)
        
        If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON
        
    End If
    
End Function

Public Function dMeasFreqRatio(Optional nchannel As Integer = 1, Optional dRange As Double = 1, _
                               Optional dAperature As Double = 0.1, Optional bChOne10XAttenuation As Boolean = False, _
                               Optional bChTwo10XAttenuation As Boolean = False, Optional bChOneACCoupling As Boolean = False, _
                               Optional bChTwoACCoupling As Boolean = False, Optional bChOne50ohms As Boolean = False, _
                               Optional bChTwo50ohms As Boolean = False, Optional sHysteresis As String = "DEF", _
                               Optional bAutoTrigger As Boolean = False, Optional nRelativeTriggerLevel As Long = 50, _
                               Optional bTriggerSlopeNegative As Boolean = False, Optional dAbsoluteTrigger As Double = 0#, _
                               Optional bTriggerOutputOn As Boolean = False, Optional nTTLTriggerOutput As Long = 0, _
                               Optional sArmStartSource As String = "IMMEDIATE", Optional bArmStartSlopNegative As Boolean = False, _
                               Optional sArmStartLevel As String = "TTL", Optional SArmStopSource As String = "IMMEDIATE", _
                               Optional bArmStopSlopNegative As Boolean = False, Optional sArmStopLevel As String = "TTL", _
                               Optional sTimeBaseSource As String = "INTERNAL", Optional bOutputTimeBase As Boolean = False, _
                               Optional bTotalizeGateMeasure As Boolean = False, Optional bTotalizeGatePolarityInverted As Boolean = False, _
                               Optional bTimeIntervalDelay As Boolean = False, Optional dTimeIntervalDelay As Double = 0.1, _
                               Optional sAcquisitionTimeOutSetting As String = "ON", Optional dAcquisitionTimeOut As Double = 5#, _
                               Optional APROBE As String = "OFF") As Double

    Dim sCurrentMsg As String
    Dim dCheckReturn As Double
        
    sCurrentMsg = frmMain.lblStatus.Caption
    
    dCheckReturn = CheckArguments(dRange, "RANGE: 1E-10 TO 1E-12", 0.0000000001, 1000000000000#, _
                                  nchannel, sHysteresis, nRelativeTriggerLevel, _
                                  dAbsoluteTrigger, nTTLTriggerOutput, sArmStartSource, _
                                  sArmStartLevel, SArmStopSource, sArmStopLevel, _
                                  sTimeBaseSource, dTimeIntervalDelay, dAperature, _
                                  sAcquisitionTimeOutSetting, dAcquisitionTimeOut, _
                                  APROBE, "dMeasFreqRatio", True)
        
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
        
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
        
    If dCheckReturn = 9.9E+37 Then
        dMeasFreqRatio = dCheckReturn
        Exit Function
    End If
        
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
        
    If Not bSimulation Then
      
        dCheckReturn = dSetupInstrument(nchannel, dRange, dAperature, _
                                        bChOne10XAttenuation, bChTwo10XAttenuation, _
                                        bChOneACCoupling, bChTwoACCoupling, _
                                        bChOne50ohms, bChTwo50ohms, _
                                        sHysteresis, bAutoTrigger, _
                                        nRelativeTriggerLevel, bTriggerSlopeNegative, _
                                        dAbsoluteTrigger, bTriggerOutputOn, _
                                        nTTLTriggerOutput, sArmStartSource, _
                                        bArmStartSlopNegative, sArmStartLevel, _
                                        SArmStopSource, bArmStopSlopNegative, _
                                        sArmStopLevel, sTimeBaseSource, _
                                        bTotalizeGateMeasure, bTotalizeGatePolarityInverted, _
                                        bTimeIntervalDelay, dTimeIntervalDelay, _
                                        sAcquisitionTimeOutSetting, dAcquisitionTimeOut, _
                                        True, "FREQ:RAT " & CStr(dRange) & "", _
                                        "FREQ:RAT", False, True, True)
                                  
        If dCheckReturn = 9.9E+37 Then
            dMeasFreqRatio = dCheckReturn
            Exit Function
        End If
        
        If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
            
        dMeasFreqRatio = dDoTheMeasurement(APROBE, nchannel)
        
        frmMain.lblStatus.Caption = sCurrentMsg
    Else
        dMeasFreqRatio = CDbl(InputBox("Command cmdCOUNTER.dMeasFreqRatio peformed." & _
        vbCrLf & "Enter Value:", "SIMULATION MODE"))
        frmMain.txtMeasured.Text = CSng(dMeasFreqRatio)
        
        If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON
        
    End If
    
End Function

Public Sub ArmTimeInterval(Optional nchannel As Integer = 1, Optional dRange As Double = 0.0000001, _
                           Optional bRouteSeparate As Boolean = True, Optional bChOne10XAttenuation As Boolean = False, _
                           Optional bChTwo10XAttenuation As Boolean = False, Optional bChOneACCoupling As Boolean = False, _
                           Optional bChTwoACCoupling As Boolean = False, Optional bChOne50ohms As Boolean = False, _
                           Optional bChTwo50ohms As Boolean = False, Optional sHysteresis As String = "DEF", _
                           Optional bAutoTrigger As Boolean = False, Optional nRelativeTriggerLevel As Long = 50, _
                           Optional bTriggerSlopeNegative As Boolean = False, Optional dAbsoluteTrigger As Double = 0#, _
                           Optional bTriggerOutputOn As Boolean = False, Optional nTTLTriggerOutput As Long = 0, _
                           Optional sArmStartSource As String = "IMMEDIATE", Optional bArmStartSlopNegative As Boolean = False, _
                           Optional sArmStartLevel As String = "TTL", Optional SArmStopSource As String = "IMMEDIATE", _
                           Optional bArmStopSlopNegative As Boolean = False, Optional sArmStopLevel As String = "TTL", _
                           Optional sTimeBaseSource As String = "INTERNAL", Optional bOutputTimeBase As Boolean = False, _
                           Optional bTotalizeGateMeasure As Boolean = False, Optional bTotalizeGatePolarityInverted As Boolean = False, _
                           Optional bTimeIntervalDelay As Boolean = False, Optional dTimeIntervalDelay As Double = 0.1, _
                           Optional sAcquisitionTimeOutSetting As String = "ON", Optional dAcquisitionTimeOut As Double = 5#, Optional bGateAverage As Boolean = True)

    Dim sCurrentMsg As String
    Dim dCheckReturn As Double
        
    sCurrentMsg = frmMain.lblStatus.Caption
    
    dCheckReturn = CheckArguments(dRange, "RANGE: 5E-9 TO 1000", 0.000000005, 1000, _
                                  nchannel, sHysteresis, nRelativeTriggerLevel, _
                                  dAbsoluteTrigger, nTTLTriggerOutput, sArmStartSource, _
                                  sArmStartLevel, SArmStopSource, sArmStopLevel, _
                                  sTimeBaseSource, dTimeIntervalDelay, 0.1, _
                                  sAcquisitionTimeOutSetting, dAcquisitionTimeOut, _
                                  APROBE, "dMeasTimeInterval", False)
        
    If dCheckReturn = 9.9E+37 Then
        Exit Sub
    End If
        
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
        
    If Not bSimulation Then
      
        dCheckReturn = dSetupInstrument(nchannel, dRange, 0.1, _
                                        bChOne10XAttenuation, bChTwo10XAttenuation, _
                                        bChOneACCoupling, bChTwoACCoupling, _
                                        bChOne50ohms, bChTwo50ohms, _
                                        sHysteresis, bAutoTrigger, _
                                        nRelativeTriggerLevel, bTriggerSlopeNegative, _
                                        dAbsoluteTrigger, bTriggerOutputOn, _
                                        nTTLTriggerOutput, sArmStartSource, _
                                        bArmStartSlopNegative, sArmStartLevel, _
                                        SArmStopSource, bArmStopSlopNegative, _
                                        sArmStopLevel, sTimeBaseSource, _
                                        bTotalizeGateMeasure, bTotalizeGatePolarityInverted, _
                                        bTimeIntervalDelay, dTimeIntervalDelay, _
                                        sAcquisitionTimeOutSetting, dAcquisitionTimeOut, _
                                        False, "TINT " & CStr(dRange) & "", _
                                        "TINT", bGateAverage, bRouteSeparate, True)
                                  
        If dCheckReturn = 9.9E+37 Then
            Exit Sub
        End If
        
        If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
            
        WriteMsg COUNTER, ":INIT" & CStr(nchannel)
        Debug.Print "INIT" & CStr(nchannel)
        
        If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
        frmMain.lblStatus.Caption = sCurrentMsg
        
    End If
    
End Sub

Public Function dMeasPPulseWidth(Optional nchannel As Integer = 1, Optional dRange As Double = 0.0000001, _
                                 Optional bChOne10XAttenuation As Boolean = False, _
                                 Optional bChTwo10XAttenuation As Boolean = False, Optional bChOneACCoupling As Boolean = False, _
                                 Optional bChTwoACCoupling As Boolean = False, Optional bChOne50ohms As Boolean = False, _
                                 Optional bChTwo50ohms As Boolean = False, Optional sHysteresis As String = "DEF", _
                                 Optional bAutoTrigger As Boolean = False, Optional nRelativeTriggerLevel As Long = 50, _
                                 Optional bTriggerSlopeNegative As Boolean = False, Optional dAbsoluteTrigger As Double = 0#, _
                                 Optional bTriggerOutputOn As Boolean = False, Optional nTTLTriggerOutput As Long = 0, _
                                 Optional sArmStartSource As String = "IMMEDIATE", Optional bArmStartSlopNegative As Boolean = False, _
                                 Optional sArmStartLevel As String = "TTL", Optional SArmStopSource As String = "IMMEDIATE", _
                                 Optional bArmStopSlopNegative As Boolean = False, Optional sArmStopLevel As String = "TTL", _
                                 Optional sTimeBaseSource As String = "INTERNAL", Optional bOutputTimeBase As Boolean = False, _
                                 Optional bTotalizeGateMeasure As Boolean = False, Optional bTotalizeGatePolarityInverted As Boolean = False, _
                                 Optional bTimeIntervalDelay As Boolean = False, Optional dTimeIntervalDelay As Double = 0.1, _
                                 Optional sAcquisitionTimeOutSetting As String = "ON", Optional dAcquisitionTimeOut As Double = 5#, Optional bGateAverage As Boolean = True, _
                                 Optional APROBE As String = "OFF") As Double

    Dim sCurrentMsg As String
    Dim dCheckReturn As Double
        
    sCurrentMsg = frmMain.lblStatus.Caption
    
    dCheckReturn = CheckArguments(dRange, "RANGE: 5E-9 TO 10E-3", 0.000000005, 0.01, _
                                  nchannel, sHysteresis, nRelativeTriggerLevel, _
                                  dAbsoluteTrigger, nTTLTriggerOutput, sArmStartSource, _
                                  sArmStartLevel, SArmStopSource, sArmStopLevel, _
                                  sTimeBaseSource, dTimeIntervalDelay, 0.1, _
                                  sAcquisitionTimeOutSetting, dAcquisitionTimeOut, _
                                  APROBE, "dMeasPPulseWidth", True)
        
    If dCheckReturn = 9.9E+37 Then
        dMeasPPulseWidth = dCheckReturn
        Exit Function
    End If
        
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
        
    If Not bSimulation Then
      
        dCheckReturn = dSetupInstrument(nchannel, dRange, 0.1, _
                                        bChOne10XAttenuation, bChTwo10XAttenuation, _
                                        bChOneACCoupling, bChTwoACCoupling, _
                                        bChOne50ohms, bChTwo50ohms, _
                                        sHysteresis, bAutoTrigger, _
                                        nRelativeTriggerLevel, bTriggerSlopeNegative, _
                                        dAbsoluteTrigger, bTriggerOutputOn, _
                                        nTTLTriggerOutput, sArmStartSource, _
                                        bArmStartSlopNegative, sArmStartLevel, _
                                        SArmStopSource, bArmStopSlopNegative, _
                                        sArmStopLevel, sTimeBaseSource, _
                                        bTotalizeGateMeasure, bTotalizeGatePolarityInverted, _
                                        bTimeIntervalDelay, dTimeIntervalDelay, _
                                        sAcquisitionTimeOutSetting, dAcquisitionTimeOut, _
                                        False, "PWID 50, " & CStr(dRange) & "", _
                                        "PWID", bGateAverage, True, True)
                                  
        If dCheckReturn = 9.9E+37 Then
            dMeasPPulseWidth = dCheckReturn
            Exit Function
        End If
        
        If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
            
        dMeasPPulseWidth = dDoTheMeasurement(APROBE, nchannel)
        
        frmMain.lblStatus.Caption = sCurrentMsg
    Else
        dMeasPPulseWidth = CDbl(InputBox("Command cmdCOUNTER.dMeasPPulseWidth peformed." & _
        vbCrLf & "Enter Value:", "SIMULATION MODE"))
        frmMain.txtMeasured.Text = CSng(dMeasPPulseWidth)
        
        If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON
        
    End If
    
End Function

Public Function dMeasNPulseWidth(Optional nchannel As Integer = 1, Optional dRange As Double = 0.0000001, _
                                 Optional bRouteSeparate As Boolean = True, Optional bChOne10XAttenuation As Boolean = False, _
                                 Optional bChTwo10XAttenuation As Boolean = False, Optional bChOneACCoupling As Boolean = False, _
                                 Optional bChTwoACCoupling As Boolean = False, Optional bChOne50ohms As Boolean = False, _
                                 Optional bChTwo50ohms As Boolean = False, Optional sHysteresis As String = "DEF", _
                                 Optional bAutoTrigger As Boolean = False, Optional nRelativeTriggerLevel As Long = 50, _
                                 Optional bTriggerSlopeNegative As Boolean = False, Optional dAbsoluteTrigger As Double = 0#, _
                                 Optional bTriggerOutputOn As Boolean = False, Optional nTTLTriggerOutput As Long = 0, _
                                 Optional sArmStartSource As String = "IMMEDIATE", Optional bArmStartSlopNegative As Boolean = False, _
                                 Optional sArmStartLevel As String = "TTL", Optional SArmStopSource As String = "IMMEDIATE", _
                                 Optional bArmStopSlopNegative As Boolean = False, Optional sArmStopLevel As String = "TTL", _
                                 Optional sTimeBaseSource As String = "INTERNAL", Optional bOutputTimeBase As Boolean = False, _
                                 Optional bTotalizeGateMeasure As Boolean = False, Optional bTotalizeGatePolarityInverted As Boolean = False, _
                                 Optional bTimeIntervalDelay As Boolean = False, Optional dTimeIntervalDelay As Double = 0.1, _
                                 Optional sAcquisitionTimeOutSetting As String = "ON", Optional dAcquisitionTimeOut As Double = 5#, Optional bGateAverage As Boolean = True, _
                                 Optional APROBE As String = "OFF") As Double

    Dim sCurrentMsg As String
    Dim dCheckReturn As Double
    
    sCurrentMsg = frmMain.lblStatus.Caption
    
    dCheckReturn = CheckArguments(dRange, "RANGE: 5E-9 TO 10E-3", 0.000000005, 0.01, _
                                  nchannel, sHysteresis, nRelativeTriggerLevel, _
                                  dAbsoluteTrigger, nTTLTriggerOutput, sArmStartSource, _
                                  sArmStartLevel, SArmStopSource, sArmStopLevel, _
                                  sTimeBaseSource, dTimeIntervalDelay, 0.1, _
                                  sAcquisitionTimeOutSetting, dAcquisitionTimeOut, _
                                  APROBE, "dMeasNPulseWidth", True)
        
    If dCheckReturn = 9.9E+37 Then
        dMeasNPulseWidth = dCheckReturn
        Exit Function
    End If
        
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
        
    If Not bSimulation Then
      
        dCheckReturn = dSetupInstrument(nchannel, dRange, 0.1, _
                                        bChOne10XAttenuation, bChTwo10XAttenuation, _
                                        bChOneACCoupling, bChTwoACCoupling, _
                                        bChOne50ohms, bChTwo50ohms, _
                                        sHysteresis, bAutoTrigger, _
                                        nRelativeTriggerLevel, bTriggerSlopeNegative, _
                                        dAbsoluteTrigger, bTriggerOutputOn, _
                                        nTTLTriggerOutput, sArmStartSource, _
                                        bArmStartSlopNegative, sArmStartLevel, _
                                        SArmStopSource, bArmStopSlopNegative, _
                                        sArmStopLevel, sTimeBaseSource, _
                                        bTotalizeGateMeasure, bTotalizeGatePolarityInverted, _
                                        bTimeIntervalDelay, dTimeIntervalDelay, _
                                        sAcquisitionTimeOutSetting, dAcquisitionTimeOut, _
                                        False, "NWID 50, " & CStr(dRange) & "", _
                                        "NWID", bGateAverage, True, True)
                                  
        If dCheckReturn = 9.9E+37 Then
            dMeasNPulseWidth = dCheckReturn
            Exit Function
        End If
        
        If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
            
        dMeasNPulseWidth = dDoTheMeasurement(APROBE, nchannel)
        
        frmMain.lblStatus.Caption = sCurrentMsg
    Else
        dMeasNPulseWidth = CDbl(InputBox("Command cmdCOUNTER.dMeasNPulseWidth peformed." & _
        vbCrLf & "Enter Value:", "SIMULATION MODE"))
        frmMain.txtMeasured.Text = CSng(dMeasNPulseWidth)
        
        If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON
        
    End If
    
End Function

Public Function dMeasTimeInterval(Optional nchannel As Integer = 1, Optional dRange As Double = 0.0000001, _
                                  Optional bRouteSeparate As Boolean = True, Optional bChOne10XAttenuation As Boolean = False, _
                                  Optional bChTwo10XAttenuation As Boolean = False, Optional bChOneACCoupling As Boolean = False, _
                                  Optional bChTwoACCoupling As Boolean = False, Optional bChOne50ohms As Boolean = False, _
                                  Optional bChTwo50ohms As Boolean = False, Optional sHysteresis As String = "DEF", _
                                  Optional bAutoTrigger As Boolean = False, Optional nRelativeTriggerLevel As Long = 50, _
                                  Optional bTriggerSlopeNegative As Boolean = False, Optional dAbsoluteTrigger As Double = 0#, _
                                  Optional bTriggerOutputOn As Boolean = False, Optional nTTLTriggerOutput As Long = 0, _
                                  Optional sArmStartSource As String = "IMMEDIATE", Optional bArmStartSlopNegative As Boolean = False, _
                                  Optional sArmStartLevel As String = "TTL", Optional SArmStopSource As String = "IMMEDIATE", _
                                  Optional bArmStopSlopNegative As Boolean = False, Optional sArmStopLevel As String = "TTL", _
                                  Optional sTimeBaseSource As String = "INTERNAL", Optional bOutputTimeBase As Boolean = False, _
                                  Optional bTotalizeGateMeasure As Boolean = False, Optional bTotalizeGatePolarityInverted As Boolean = False, _
                                  Optional bTimeIntervalDelay As Boolean = False, Optional dTimeIntervalDelay As Double = 0.1, _
                                  Optional sAcquisitionTimeOutSetting As String = "ON", Optional dAcquisitionTimeOut As Double = 5#, Optional bGateAverage As Boolean = True, _
                                  Optional APROBE As String = "OFF") As Double

    Dim sCurrentMsg As String
    Dim dCheckReturn As Double
    
    sCurrentMsg = frmMain.lblStatus.Caption
    
    dCheckReturn = CheckArguments(dRange, "RANGE: 5E-9 TO 1000", 0.000000005, 1000, _
                                  nchannel, sHysteresis, nRelativeTriggerLevel, _
                                  dAbsoluteTrigger, nTTLTriggerOutput, sArmStartSource, _
                                  sArmStartLevel, SArmStopSource, sArmStopLevel, _
                                  sTimeBaseSource, dTimeIntervalDelay, 0.1, _
                                  sAcquisitionTimeOutSetting, dAcquisitionTimeOut, _
                                  APROBE, "dMeasTimeInterval", True)
        
    If dCheckReturn = 9.9E+37 Then
        dMeasTimeInterval = dCheckReturn
        Exit Function
    End If
        
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
        
    If Not bSimulation Then
      
        dCheckReturn = dSetupInstrument(nchannel, dRange, 0.1, _
                                        bChOne10XAttenuation, bChTwo10XAttenuation, _
                                        bChOneACCoupling, bChTwoACCoupling, _
                                        bChOne50ohms, bChTwo50ohms, _
                                        sHysteresis, bAutoTrigger, _
                                        nRelativeTriggerLevel, bTriggerSlopeNegative, _
                                        dAbsoluteTrigger, bTriggerOutputOn, _
                                        nTTLTriggerOutput, sArmStartSource, _
                                        bArmStartSlopNegative, sArmStartLevel, _
                                        SArmStopSource, bArmStopSlopNegative, _
                                        sArmStopLevel, sTimeBaseSource, _
                                        bTotalizeGateMeasure, bTotalizeGatePolarityInverted, _
                                        bTimeIntervalDelay, dTimeIntervalDelay, _
                                        sAcquisitionTimeOutSetting, dAcquisitionTimeOut, _
                                        False, "TINT " & CStr(dRange) & "", _
                                        "TINT", bGateAverage, bRouteSeparate, True)
                                  
        If dCheckReturn = 9.9E+37 Then
            dMeasTimeInterval = dCheckReturn
            Exit Function
        End If
        
        If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
            
        dMeasTimeInterval = dDoTheMeasurement(APROBE, nchannel)
        
        frmMain.lblStatus.Caption = sCurrentMsg
    Else
        dMeasTimeInterval = CDbl(InputBox("Command cmdCOUNTER.dMeasTimeInterval peformed." & _
        vbCrLf & "Enter Value:", "SIMULATION MODE"))
        frmMain.txtMeasured.Text = CSng(dMeasTimeInterval)
        
        If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON
        
    End If
    
End Function

Public Function dMeasTimeInterval2(Optional bRouteSeparate As Boolean = True, Optional bFetch As Boolean = True, _
                                    Optional bChOneTrigNeg As Boolean = False, Optional bChTwoTrigNeg As Boolean = False, _
                                    Optional dChOneTrigLev As Double = 1, Optional dChTwoTrigLev As Double = 1, _
                                    Optional bChOne10XAtt As Boolean = False, Optional bChTwo10XAtt As Boolean = False, _
                                    Optional bChOneACCoup As Boolean = False, Optional bChTwoACCoup As Boolean = False, _
                                    Optional bChOne50ohms As Boolean = False, Optional bChTwo50ohms As Boolean = False, _
                                    Optional sChOneHyster As String = "MIN", Optional sChTwoHyster As String = "MIN", _
                                    Optional bDelayStatOn As Boolean = False, Optional dDelayTimeSec As Double = 1, _
                                    Optional bAcqTimeOut As Boolean = False, Optional dAcqTimeSec As Double = 5, _
                                    Optional APROBE As String = "OFF") As Double

    'Notes
    'Channel 1 and 2 are always used for Time Interval Measurements.
    'Channel 2 Input is only used whenever bRouteSeparate=true.
    'If bFetch=false, then dMeasTimeInterval2 acts like an Arm command.
    '   Test program will need to execute following command to get result.
    '   dMeasured = cmdCounter.dMeasFetch
    'Aprobe = "CONT" will always execute Fetch.

    Dim i As Integer
    Dim sData As String
    Dim nErr As Long
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    frmMain.txtInstrument.Text = "Counter"
    frmMain.txtCommand.Text = "dMeasTimeInterval2"
    Dim sCurrentMsg As String
    If APROBE <> "OFF" Then
        sCurrentMsg = frmMain.lblStatus.Caption
        frmMain.lblStatus.Caption = "APPLY PROBE"
    End If
        
    'Error Check Arguments
    Select Case UCase(sChOneHyster)
      Case "DEF", "MAX", "MIN"
      Case Else
        Echo "COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasTimeInterval2 sChOneHyster argument out of range."
        Echo "RANGE: DEF, MAX or MIN"
        dMeasTimeInterval2 = 9.9E+37: frmMain.txtMeasured.Text = 9.9E+37: Err.Raise -1001
        dMeasTimeInterval2 = 0
        Exit Function
    End Select
    Select Case UCase(sChTwoHyster)
      Case "DEF", "MAX", "MIN"
      Case Else
        Echo "COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasTimeInterval2 sChTwoHyster argument out of range."
        Echo "RANGE: DEF, MAX or MIN"
        dMeasTimeInterval2 = 9.9E+37: frmMain.txtMeasured.Text = 9.9E+37: Err.Raise -1001
        dMeasTimeInterval2 = 0
        Exit Function
    End Select
    If dChOneTrigLev < -10 Or dChOneTrigLev > 10 Then
       Echo "COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasTimeInterval2 dChOneTrigLev argument out of range."
       Echo "RANGE: -10 to +10"
       dMeasTimeInterval2 = 9.9E+37: frmMain.txtMeasured.Text = 9.9E+37: Err.Raise -1001
       dMeasTimeInterval2 = 0
       Exit Function
    End If
    If dChTwoTrigLev < -10 Or dChTwoTrigLev > 10 Then
       Echo "COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasTimeInterval2 dChTwoTrigLev argument out of range."
       Echo "RANGE: -10 to +10"
       dMeasTimeInterval2 = 9.9E+37: frmMain.txtMeasured.Text = 9.9E+37: Err.Raise -1001
       dMeasTimeInterval2 = 0
       Exit Function
    End If
    Select Case Left(UCase(APROBE), 1)
      Case "S", "O", "C" ' Single, Off, Continuous
      Case Else
        Echo "COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasTimeInterval2 APROBE argument out of range."
        Echo "RANGE: SINGLE, CONTINUOUS or OFF"
        dMeasTimeInterval2 = 9.9E+37: frmMain.txtMeasured.Text = 9.9E+37: Err.Raise -1001
        dMeasTimeInterval2 = 0
        Exit Function
    End Select
    If dDelayTimeSec < 0.001 Or dDelayTimeSec >= 10 Then
        Echo "COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasTimeInterval2 dDelayTimeSec out of range."
        Echo "RANGE: 0.001 to 9.999 in 0.001 second increments"
        dMeasTimeInterval2 = 9.9E+37: frmMain.txtMeasured.Text = 9.9E+37: Err.Raise -1001
        dMeasTimeInterval2 = 0
        Exit Function
    End If
    If bAcqTimeOut Then
      If dAcqTimeSec < 0.1 Or dAcqTimeSec > 1500 Then
        Echo "COUNTER PROGRAMMING ERROR:  Command cmdCounter.dMeasTimeInterval2 dAcqTimeSec out of range."
        Echo "RANGE: 0.1 to 1500 in 0.1 second increments"
        dMeasTimeInterval2 = 9.9E+37: frmMain.txtMeasured.Text = 9.9E+37: Err.Raise -1001
        dMeasTimeInterval2 = 0
        Exit Function
      End If
    End If
    
    'Configure Counter for Measurement
    'Set Input Options
    'Configure INPut Subsystem
    If Not bSimulation Then
      'Clear Counter
      nSystErr = atxmlDF_viClear(ResourceName(COUNTER), 0&)
      WriteMsg COUNTER, "*RST ; *CLS"
      Delay 0.5
      WriteMsg COUNTER, "CONF1:TINT"
      
      'Set Routing (Always seperate for Period)
      If bRouteSeparate Then
        WriteMsg COUNTER, "INP1:ROUT SEP"
      Else
        WriteMsg COUNTER, "INP1:ROUT COMM"
      End If
      
      If bDelayStatOn Then
        WriteMsg COUNTER, "SENS1:TINT:DEL:STAT ON"
        WriteMsg COUNTER, "SENS1:TINT:DEL:TIME " & Format(dDelayTimeSec, "0.000") ' no more than 3 decimal places
      End If
      
      If bAcqTimeOut Then
        WriteMsg COUNTER, "SENS1:ATIM:CHEC ON"
        WriteMsg COUNTER, "SENS1:ATIM:TIME " + CStr(dAcqTimeSec)
      Else
        WriteMsg COUNTER, "SENS1:ATIM:CHEC OFF"
        WriteMsg COUNTER, "SENS1:ATIM:TIME 5.0" ' default
      End If
      
      
      'Channel 1
      'Set Attenuation
      If Not bChOne10XAtt Then
        WriteMsg COUNTER, "INP1:ATT 1"
      Else
        WriteMsg COUNTER, "INP1:ATT 10"
      End If
      'Set Coupling
      If Not bChOneACCoup Then
        WriteMsg COUNTER, "INP1:COUP DC"
      Else
        WriteMsg COUNTER, "INP1:COUP AC"
      End If
      'Set Impedance
      If Not bChOne50ohms Then
        WriteMsg COUNTER, "INP1:IMP 1E6"
      Else
        WriteMsg COUNTER, "INP1:IMP 50"
      End If
      
      ' Channel 2
      'Set Attenuation
      If Not bChTwo10XAtt Then
        WriteMsg COUNTER, "INP2:ATT 1"
      Else
        WriteMsg COUNTER, "INP2:ATT 10"
      End If
      'Set Coupling
      If Not bChTwoACCoup Then
        WriteMsg COUNTER, "INP2:COUP DC"
      Else
        WriteMsg COUNTER, "INP2:COUP AC"
      End If
      'Set Impedance
      If Not bChTwo50ohms Then
        WriteMsg COUNTER, "INP2:IMP 1E6"
      Else
        WriteMsg COUNTER, "INP2:IMP 50"
      End If
      
      'Configure SENSe Options
      'Channel 1
      If bChOneTrigNeg Then
        WriteMsg COUNTER, "SENS1:EVEN:SLOP NEG"
      Else
        WriteMsg COUNTER, "SENS1:EVEN:SLOP POS"
      End If
      WriteMsg COUNTER, "SENS1:EVEN:HYST " & UCase(sChOneHyster)
      WriteMsg COUNTER, "SENS1:EVEN:LEV " & UCase$(dChOneTrigLev)
      
      ' Channel 2
      If bChTwoTrigNeg Then
        WriteMsg COUNTER, "SENS2:EVEN:SLOP NEG"
      Else
        WriteMsg COUNTER, "SENS2:EVEN:SLOP POS"
      End If
      WriteMsg COUNTER, "SENS2:EVEN:HYST " & UCase(sChTwoHyster)
      WriteMsg COUNTER, "SENS2:EVEN:LEV " & UCase$(dChTwoTrigLev)
    End If
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
        
    'Make Measurement
    If Not bSimulation Then
      Select Case Left(UCase(APROBE), 1)
        Case "O"    ' Aprobe Off
          WriteMsg COUNTER, "INIT1"
                    If bFetch = False Then
            dMeasTimeInterval2 = -1
            Exit Function
          End If
          WriteMsg COUNTER, "FETCH1?"
          nErr = ReadMsg(COUNTER, sData)
          If nErr <> 0 Then
              dMeasTimeInterval2 = 9.9E+37
              frmMain.txtMeasured.Text = 9.9E+37
              Err.Raise nErr
              Exit Function
          End If
          dMeasTimeInterval2 = CDbl(sData)
          frmMain.txtMeasured.Text = CSng(dMeasTimeInterval2)
          If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
          
        Case "S"    ' Aprobe Single
            MisProbe = vbYes
            Do While MisProbe = vbYes
                frmMain.lblStatus.Caption = "Waiting for Probe ..."
                Failed = False
                UserEvent = 0
                frmMain.TimerProbe.Enabled = True
                frmMain.fraInstructions.Visible = True
                Do While (UserEvent = 0) And (Not bProbeClosed)
                    DoEvents
                Loop
                Do While (UserEvent = 0) And (bProbeClosed)
                    DoEvents
                Loop
                frmMain.TimerProbe.Enabled = False
                bProbeClosed = False
                frmMain.lblStatus.Caption = "Making Measurement ..."
                WriteMsg COUNTER, "INIT1"
                If bFetch = False Then
                   dMeasTimeInterval2 = -1
                   Exit Function
                End If
                WriteMsg COUNTER, "FETCH1?"
                nErr = ReadMsg(COUNTER, sData)
                If nErr <> 0 Then
                   dMeasTimeInterval2 = 9.9E+37: frmMain.txtMeasured.Text = 9.9E+37
                   Err.Raise nErr
                   Exit Function
                End If
                dMeasTimeInterval2 = CDbl(sData)
                frmMain.txtMeasured.Text = CSng(dMeasTimeInterval2)
                If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
                frmMain.lblStatus.Caption = "Measurement Complete ..."
                If Failed = True Then
                  MisProbe = MsgBox("Analog Probe Measurement Failed. " & vbCrLf & _
                    "Do you wish to re-probe?", vbCritical + vbYesNo + vbSystemModal, "Analog Probe Failure")
                Else
                  MisProbe = vbNo
                End If
            Loop
            frmMain.lblStatus.Caption = sCurrentMsg
            
        Case "C"    ' Continuous  (bFetch=False, ignored in continuous mode)
            MisProbe = vbYes
            Do While MisProbe = vbYes
                Failed = False
                UserEvent = 0
                frmMain.TimerProbe.Enabled = True: frmMain.fraInstructions.Visible = True
                Do While (UserEvent = 0) And (Not bProbeClosed)
                    frmMain.lblStatus.Caption = "Making Measurement ..."
                    WriteMsg COUNTER, "INIT1"
                    WriteMsg COUNTER, "FETCH1?"
                    nErr = ReadMsg(COUNTER, sData)
                    If nErr <> 0 Then
                      dMeasTimeInterval2 = 9.9E+37: frmMain.txtMeasured.Text = 9.9E+37
                      Err.Raise nErr
                      Exit Function
                    End If
                    dMeasTimeInterval2 = CDbl(sData)
                    frmMain.txtMeasured.Text = CSng(dMeasTimeInterval2)
                    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
                Loop
                Do While (UserEvent = 0) And (bProbeClosed)
                    DoEvents
                Loop
                frmMain.TimerProbe.Enabled = False
                bProbeClosed = False
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
        End Select
    Else
        dMeasTimeInterval2 = CDbl(InputBox("Command cmdCOUNTER.dMeasTimeInterval2 peformed." & vbCrLf & "Enter Value:", "SIMULATION MODE"))
        frmMain.txtMeasured.Text = CSng(dMeasTimeInterval2)
        If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON
    End If
    
End Function

Public Function dMeasFreq(Optional nchannel As Integer = 1, Optional dRange As Double = -1, _
                          Optional dAperature As Double = 0.1, Optional bChOne10XAttenuation As Boolean = False, _
                          Optional bChTwo10XAttenuation As Boolean = False, Optional bChOneACCoupling As Boolean = False, _
                          Optional bChTwoACCoupling As Boolean = False, Optional bChOne50ohms As Boolean = False, _
                          Optional bChTwo50ohms As Boolean = False, Optional sHysteresis As String = "DEF", _
                          Optional bAutoTrigger As Boolean = False, Optional nRelativeTriggerLevel As Long = 50, _
                          Optional bTriggerSlopeNegative As Boolean = False, Optional dAbsoluteTrigger As Double = 0#, _
                          Optional bTriggerOutputOn As Boolean = False, Optional nTTLTriggerOutput As Long = 0, _
                          Optional sArmStartSource As String = "IMMEDIATE", Optional bArmStartSlopNegative As Boolean = False, _
                          Optional sArmStartLevel As String = "TTL", Optional SArmStopSource As String = "IMMEDIATE", _
                          Optional bArmStopSlopNegative As Boolean = False, Optional sArmStopLevel As String = "TTL", _
                          Optional sTimeBaseSource As String = "INTERNAL", Optional bOutputTimeBase As Boolean = False, _
                          Optional bTotalizeGateMeasure As Boolean = False, Optional bTotalizeGatePolarityInverted As Boolean = False, _
                          Optional bTimeIntervalDelay As Boolean = False, Optional dTimeIntervalDelay As Double = 0.1, _
                          Optional sAcquisitionTimeOutSetting As String = "ON", Optional dAcquisitionTimeOut As Double = 5#, _
                          Optional APROBE As String = "OFF", Optional bReset As Boolean = True) As Double

    Dim sCurrentMsg As String
    Dim dCheckReturn As Double
        
    sCurrentMsg = frmMain.lblStatus.Caption
    
    dCheckReturn = CheckArguments(dRange, "RANGE: 0.1 TO 200e6 OR -1 FOR AUTORANGE", 0.1, 200000000#, _
                                  nchannel, sHysteresis, nRelativeTriggerLevel, _
                                  dAbsoluteTrigger, nTTLTriggerOutput, sArmStartSource, _
                                  sArmStartLevel, SArmStopSource, sArmStopLevel, _
                                  sTimeBaseSource, dTimeIntervalDelay, dAperature, _
                                  sAcquisitionTimeOutSetting, dAcquisitionTimeOut, _
                                  APROBE, "dMeasFreq", True)
        
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
        
    If dCheckReturn = 9.9E+37 Then
        dMeasFreq = dCheckReturn
        Exit Function
    End If
        
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
        
    If Not bSimulation Then
      
        dCheckReturn = dSetupInstrument(nchannel, dRange, dAperature, _
                                        bChOne10XAttenuation, bChTwo10XAttenuation, _
                                        bChOneACCoupling, bChTwoACCoupling, _
                                        bChOne50ohms, bChTwo50ohms, _
                                        sHysteresis, bAutoTrigger, _
                                        nRelativeTriggerLevel, bTriggerSlopeNegative, _
                                        dAbsoluteTrigger, bTriggerOutputOn, _
                                        nTTLTriggerOutput, sArmStartSource, _
                                        bArmStartSlopNegative, sArmStartLevel, _
                                        SArmStopSource, bArmStopSlopNegative, _
                                        sArmStopLevel, sTimeBaseSource, _
                                        bTotalizeGateMeasure, bTotalizeGatePolarityInverted, _
                                        bTimeIntervalDelay, dTimeIntervalDelay, _
                                        sAcquisitionTimeOutSetting, dAcquisitionTimeOut, _
                                        True, "FREQ " & CStr(dRange) & "", _
                                        "FREQ", False, True, False)
                                  
        If dCheckReturn = 9.9E+37 Then
            dMeasFreq = dCheckReturn
            Exit Function
        End If
        
        If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
        
        dMeasFreq = dDoTheMeasurement(APROBE, nchannel)
        
        frmMain.lblStatus.Caption = sCurrentMsg
    Else
        dMeasFreq = CDbl(InputBox("Command cmdCOUNTER.dMeasFreq peformed." & _
        vbCrLf & "Enter Value:", "SIMULATION MODE"))
        frmMain.txtMeasured.Text = CSng(dMeasFreq)
        
        If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON
        
    End If
    
End Function

Public Sub ArmTotalize(Optional nchannel As Integer = 1, Optional bChOne10XAttenuation As Boolean = False, _
                       Optional bChTwo10XAttenuation As Boolean = False, Optional bChOneACCoupling As Boolean = False, _
                       Optional bChTwoACCoupling As Boolean = False, Optional bChOne50ohms As Boolean = False, _
                       Optional bChTwo50ohms As Boolean = False, Optional sHysteresis As String = "DEF", _
                       Optional bAutoTrigger As Boolean = False, Optional nRelativeTriggerLevel As Long = 50, _
                       Optional bTriggerSlopeNegative As Boolean = False, Optional dAbsoluteTrigger As Double = 0#, _
                       Optional bTriggerOutputOn As Boolean = False, Optional nTTLTriggerOutput As Long = 0, _
                       Optional sArmStartSource As String = "IMMEDIATE", Optional bArmStartSlopNegative As Boolean = False, _
                       Optional sArmStartLevel As String = "TTL", Optional SArmStopSource As String = "IMMEDIATE", _
                       Optional bArmStopSlopNegative As Boolean = False, Optional sArmStopLevel As String = "TTL", _
                       Optional sTimeBaseSource As String = "INTERNAL", Optional bOutputTimeBase As Boolean = False, _
                       Optional bTotalizeGateMeasure As Boolean = False, Optional bTotalizeGatePolarityInverted As Boolean = False, _
                       Optional bTimeIntervalDelay As Boolean = False, Optional dTimeIntervalDelay As Double = 0.1, _
                       Optional sAcquisitionTimeOutSetting As String = "ON", Optional dAcquisitionTimeOut As Double = 5#, _
                       Optional APROBE As String = "OFF")

    Dim sData As String
    Dim nErr As Long
    Dim sCurrentMsg As String
    Dim dCheckReturn As Double
        
    dCheckReturn = CheckArguments(-1#, "None", 0#, 0#, _
                                  nchannel, sHysteresis, nRelativeTriggerLevel, _
                                  dAbsoluteTrigger, nTTLTriggerOutput, sArmStartSource, _
                                  sArmStartLevel, SArmStopSource, sArmStopLevel, _
                                  sTimeBaseSource, dTimeIntervalDelay, 0.1, _
                                  sAcquisitionTimeOutSetting, dAcquisitionTimeOut, _
                                  APROBE, "dMeasTimeInterval", False)
        
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
        
    If dCheckReturn = 9.9E+37 Then
        Exit Sub
    End If
        
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
        
    If Not bSimulation Then
      
        dCheckReturn = dSetupInstrument(nchannel, -1#, 0.1, _
                                        bChOne10XAttenuation, bChTwo10XAttenuation, _
                                        bChOneACCoupling, bChTwoACCoupling, _
                                        bChOne50ohms, bChTwo50ohms, _
                                        sHysteresis, bAutoTrigger, _
                                        nRelativeTriggerLevel, bTriggerSlopeNegative, _
                                        dAbsoluteTrigger, bTriggerOutputOn, _
                                        nTTLTriggerOutput, sArmStartSource, _
                                        bArmStartSlopNegative, sArmStartLevel, _
                                        SArmStopSource, bArmStopSlopNegative, _
                                        sArmStopLevel, sTimeBaseSource, _
                                        bTotalizeGateMeasure, bTotalizeGatePolarityInverted, _
                                        bTimeIntervalDelay, dTimeIntervalDelay, _
                                        sAcquisitionTimeOutSetting, dAcquisitionTimeOut, _
                                        False, "TOT", "TOT", False, True, True)
                                  
        If dCheckReturn = 9.9E+37 Then
            Exit Sub
        End If
        
        If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
        
        WriteMsg COUNTER, ":INIT" & CStr(nchannel)
        Debug.Print "INIT" & CStr(nchannel)
        
        If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    End If
    
End Sub


Public Sub SetChannelImpedance(Optional bChOne50ohms As Boolean = False, Optional bChTwo50ohms As Boolean = False)

    'To allow the instrument to be preped prior to connection.
    'For signal drivers that could be damaged by applying a 50 ohm load
    'The counter must not be reset after this is called or the inputs
    'will default back to 50 ohm.

    'Also can be used to make the counter into a very expensive 50-ohm
    'terminator.  However, beware also of input AC/DC coupling and
    'input routing condition.
    
    'Set Impedance
    If Not bChOne50ohms Then
        If Not bSimulation Then WriteMsg COUNTER, ":INP1:IMP 1E6;"
    Else
        If Not bSimulation Then WriteMsg COUNTER, ":INP1:IMP 50;"
    End If
      
    If Not bChTwo50ohms Then
        If Not bSimulation Then WriteMsg COUNTER, ":INP2:IMP 1E6;"
    Else
        If Not bSimulation Then WriteMsg COUNTER, ":INP2:IMP 50;"
    End If

End Sub

Private Function CheckArguments(dRange As Double, sRangeString As String, _
                                dRangeLow As Double, dRangeHigh As Double, _
                                nchannel As Integer, sHysteresis As String, _
                                nRelativeTriggerLevel As Long, dAbsoluteTrigger As Double, _
                                nTTLTriggerOutput As Long, sArmStartSource As String, _
                                sArmStartLevel As String, SArmStopSource As String, _
                                sArmStopLevel As String, sTimeBaseSource As String, _
                                dTimeIntervalDelay As Double, dAperature As Double, _
                                sAcquisitionTimeOutSetting As String, dAcquisitionTimeOut As Double, _
                                APROBE As String, sMeasureType As String, _
                                bSetMeasureValue As Boolean) As Double
    
    Dim sErrorMessage As String
    
    CheckArguments = 0#
    
    frmMain.txtInstrument.Text = "Counter"
    frmMain.txtCommand.Text = sMeasureType
    
    If nchannel < 1 Or nchannel > 2 Then
        sErrorMessage = sMeasureType & " nChannel argument out of range." & vbCrLf & "RANGE: 1 TO 2"
        GoTo ExitFunction
    End If
    
    If dRange <> -1 Then
        If dRange < dRangeLow Or dRange > dRangeHigh Then
            sErrorMessage = sMeasureType & " dRange argument out of range." & vbCrLf & sRangeString
            GoTo ExitFunction
        End If
    End If
    
    If dAperature < 0.1 Or dAperature > 99.999 Then
        sErrorMessage = sMeasureType & " nChannel argument out of range." & vbCrLf & "RANGE: 0.001 TO 99.999"
        GoTo ExitFunction
    End If
    
    Select Case UCase(sHysteresis)
        Case "DEF", "MAX", "MIN"
        Case Else
            sErrorMessage = sMeasureType & " sHysteresis argument out of range." & vbCrLf & "RANGE: DEF, MAX or MIN"
            GoTo ExitFunction
    End Select
    
    If nRelativeTriggerLevel < 10 Or nRelativeTriggerLevel > 90 Then
        sErrorMessage = sMeasureType & " nRelativeTriggerLevel argument out of range." & vbCrLf & "RANGE: 10 to 90"
        GoTo ExitFunction
    End If
    
    If dAbsoluteTrigger < -10.238 Or dAbsoluteTrigger > 10.238 Then
        sErrorMessage = sMeasureType & " dAbsoluteTrigger argument out of range." & vbCrLf & "RANGE: -10.238 to 10.238"
        GoTo ExitFunction
    End If
    
    If nTTLTriggerOutput < 0 Or nTTLTriggerOutput > 7 Then
        sErrorMessage = sMeasureType & " dMeasFallTime argument out of range." & vbCrLf & "RANGE: 0 to 7"
        GoTo ExitFunction
    End If
    
    Select Case UCase(sArmStartSource)
        
        Case "IMMEDIATE", "VXI"
        Case "TTL0", "TTL1", "TTL2", "TTL3", "TTL4", "TTL5", "TTL6", "TTL7"
        Case "EXTERNAL"
            
            Select Case UCase(sArmStartLevel)
                Case "TTL", "ECL", "GND"
                Case Else
                    sErrorMessage = sMeasureType & " sArmStartLevel argument out of range." & vbCrLf & "RANGE: TTL, ECL or GND"
                    GoTo ExitFunction
            End Select
        
        Case Else
            sErrorMessage = sMeasureType & " sArmStartSource argument out of range." & vbCrLf & _
            "RANGE: IMMEDIATE, VXI, TTL0, TTL1, TTL2, TTL3, TTL4, TTL5, TTL6, TTL7 or EXTERNAL"
            GoTo ExitFunction
    End Select
    
    Select Case UCase(SArmStopSource)
        
        Case "IMMEDIATE", "VXI"
        Case "TTL0", "TTL1", "TTL2", "TTL3", "TTL4", "TTL5", "TTL6", "TTL7"
        Case "EXTERNAL"
            
            Select Case UCase(sArmStopLevel)
                Case "TTL", "ECL", "GND"
                Case Else
                    sErrorMessage = sMeasureType & " sArmStopLevel argument out of range." & vbCrLf & "RANGE: TTL, ECL or GND"
                    GoTo ExitFunction
            End Select
        
        Case Else
            sErrorMessage = sMeasureType & " sArmStopSource argument out of range." & vbCrLf & _
            "RANGE: IMMEDIATE, VXI, TTL0, TTL1, TTL2, TTL3, TTL4, TTL5, TTL6, TTL7 or EXTERNAL"
            GoTo ExitFunction
    End Select
    
    Select Case UCase(sTimeBaseSource)
        
        Case "VXI", "INTERNAL", "EXTERNAL"
        Case Else
            sErrorMessage = sMeasureType & " sTimeBaseSource argument out of range." & vbCrLf & "RANGE: VXI, INTERNAL or EXTERNAL"
            GoTo ExitFunction
    End Select
    
    If dTimeIntervalDelay < 0.001 Or dTimeIntervalDelay > 99.999 Then
        sErrorMessage = sMeasureType & " dTimeIntervalDelay argument out of range." & vbCrLf & "RANGE: 0.001 to 99.999"
        GoTo ExitFunction
    End If
    
    Select Case UCase(sAcquisitionTimeOutSetting)
        Case "ON", "OFF", "START"
        Case Else
            sErrorMessage = sMeasureType & " sAcquisitionTimeOutSetting argument out of range." & vbCrLf & "RANGE: ON, OFF, or START"
            GoTo ExitFunction
    End Select
    
    If dAcquisitionTimeOut < 0.1 Or dAcquisitionTimeOut > 1500 Then
        sErrorMessage = sMeasureType & " dAcquisitionTimeOut argument out of range." & vbCrLf & "RANGE: 0.1 to 1500"
        GoTo ExitFunction
    End If
    
    Select Case Left(UCase(APROBE), 1)
        Case "S", "O", "C" ' Single, Off, Continuous
            
            If APROBE <> "OFF" Then
                frmMain.lblStatus.Caption = "APPLY PROBE"
            End If
        
        Case Else
            sErrorMessage = sMeasureType & " APROBE argument out of range." & vbCrLf & "RANGE: SINGLE, OFF, or CONTINUOUS"
            GoTo ExitFunction
    End Select
    
    Exit Function

ExitFunction:
    Err.Raise -1001
    Echo "COUNTER PROGRAMMING ERROR:  Command cmdCounter." & sErrorMessage

    If bSetMeasureValue = True Then
        CheckArguments = 9.9E+37
        frmMain.txtMeasured.Text = 9.9E+37
    Else
        CheckArguments = -1#
    End If
    
End Function

Private Function dSetupInstrument(nchannel As Integer, dRange As Double, dAperature As Double, _
                                  bChOne10XAttenuation As Boolean, bChTwo10XAttenuation As Boolean, _
                                  bChOneACCoupling As Boolean, bChTwoACCoupling As Boolean, _
                                  bChOne50ohms As Boolean, bChTwo50ohms As Boolean, _
                                  sHysteresis As String, bAutoTrigger As Boolean, _
                                  nRelativeTriggerLevel As Long, bTriggerSlopeNegative As Boolean, _
                                  dAbsoluteTrigger As Double, bTriggerOutputOn As Boolean, _
                                  nTTLTriggerOutput As Long, sArmStartSource As String, _
                                  bArmStartSlopNegative As Boolean, sArmStartLevel As String, _
                                  SArmStopSource As String, bArmStopSlopNegative As Boolean, _
                                  sArmStopLevel As String, sTimeBaseSource As String, _
                                  bTotalizeGateMeasure As Boolean, bTotalizeGatePolarityInverted As Boolean, _
                                  bTimeIntervalDelay As Boolean, dTimeIntervalDelay As Double, _
                                  sAcquisitionTimeOutSetting As String, dAcquisitionTimeOut As Double, _
                                  bSetupAperature As Boolean, sConfigString As String, _
                                  sSenseOptionString As String, bGateAverage As Boolean, _
                                  bRouteSeparate As Boolean, bNotFreq As Boolean) As Double
                                  
    Dim i As Integer
    Dim sData As String
    Dim nErr As Long
    Dim sCommand As String
    Dim Q As String
    Dim iHandle&
    Dim sCurrentMsg As String
    
    Q = Chr$(34)
    
    iHandle& = nInstrumentHandle(COUNTER)
    
    nSystErr = atxmlDF_viClear(ResourceName(COUNTER), 0&)
    
    If nSystErr <> 0 Then
        dSetupInstrument = 9.9E+37
        Exit Function
    End If
    
    WriteMsg COUNTER, "*RST ; *CLS"
    Delay 0.5
    
    sCommand = ""
    
    If nchannel = 1 Then
        
        If Not bChOne10XAttenuation Then
            WriteMsg COUNTER, ":INP1:ATT 1;"
            Debug.Print ":INP1:ATT 1;"
        Else
            WriteMsg COUNTER, ":INP1:ATT 10;"
            Debug.Print ":INP1:ATT 10;"
        End If
        
        If Not bChOneACCoupling Then
            WriteMsg COUNTER, ":INP1:COUP DC;"
            Debug.Print ":INP1:COUP DC;"
        Else
            WriteMsg COUNTER, ":INP1:COUP AC;"
            Debug.Print ":INP1:COUP AC;"
        End If
        
        If Not bChOne50ohms Then
            WriteMsg COUNTER, ":INP1:IMP 1E6;"
            Debug.Print ":INP1:IMP 1E6;"
        Else
            WriteMsg COUNTER, ":INP1:IMP 50;"
            Debug.Print ":INP1:IMP 50;"
        End If
        
        If bRouteSeparate Then
            WriteMsg COUNTER, ":INP1:ROUT SEP;"
            Debug.Print ":INP1:ROUT SEP;"
        Else
            WriteMsg COUNTER, ":INP1:ROUT COMM;"
            Debug.Print ":INP1:ROUT COMM;"
        End If
        
    Else ' Channel 2
        
        If Not bChTwo10XAttenuation Then
            WriteMsg COUNTER, ":INP2:ATT 1;"
            Debug.Print ":INP2:ATT 1;"
        Else
            WriteMsg COUNTER, ":INP2:ATT 10;"
            Debug.Print ":INP2:ATT 10;"
        End If
        
        If Not bChTwoACCoupling Then
            WriteMsg COUNTER, ":INP2:COUP DC;"
            Debug.Print ":INP2:COUP DC;"
        Else
            WriteMsg COUNTER, ":INP2:COUP AC;"
            Debug.Print ":INP2:COUP AC;"
        End If
        
        If Not bChTwo50ohms Then
            WriteMsg COUNTER, ":INP2:IMP 1E6;"
            Debug.Print ":INP2:IMP 1E6;"
        Else
            WriteMsg COUNTER, ":INP2:IMP 50;"
            Debug.Print ":INP2:IMP 50;"
        End If
    
    End If
    
    'Set the instrument's timeout
    If dAcquisitionTimeOut > dAperature Then
        lSystErr = viSetAttribute(nInstrumentHandle(COUNTER), VI_ATTR_TMO_VALUE, (dAcquisitionTimeOut * 1000) + 1000)
    Else
        lSystErr = viSetAttribute(nInstrumentHandle(COUNTER), VI_ATTR_TMO_VALUE, (dAperature * 1000) + 1000)
    End If
    
    If lSystErr Then
        lSystErr = viStatusDesc(iHandle&, lSystErr, lpBuffer$)
        Echo "WRITEMSG ERROR: Error sending command to " & sInstrumentDescription(COUNTER) & "."
        Echo "COMMAND: viSetAttribute"
        MsgBox "Error sending: viSetAttribute VI_ATTR_TMO_VALUE" & vbCrLf & "ERROR: " & lpBuffer, vbExclamation, "VISA Error"
        dSetupInstrument = 9.9E+37
        Exit Function
    End If
    
    'Configure SENSe Options
    sCommand = ":SENS" & nchannel & ":FUNC " & Q & sSenseOptionString & Q & ";"
    
    If bGateAverage Then
        sCommand = sCommand & ":SENS" & nchannel & ":AVER:STAT ON;"
    Else
        sCommand = sCommand & ":SENS" & nchannel & ":AVER:STAT OFF;"
    End If
    
    sCommand = sCommand & ":SENS:ATIM:TIME " & CStr(dAcquisitionTimeOut) & ";"
    
    Select Case UCase(sAcquisitionTimeOutSetting)
        Case "ON", "OFF"
            sCommand = sCommand & ":SENS:ATIM:CHEC " & UCase(sAcquisitionTimeOutSetting) & ";"
        Case "START"
            sCommand = sCommand & ":SENS:ATIM:CHEC STAR;"
    End Select
    
    If bAutoTrigger Then
        sCommand = sCommand & ":SENS" & nchannel & ":EVEN:LEV:ABS:AUTO ON;"
        sCommand = sCommand & ":SENS" & nchannel & ":EVEN:LEV:REL " & CStr(nRelativeTriggerLevel) & ";"
    Else
        sCommand = sCommand & ":SENS" & nchannel & ":EVEN:LEV:ABS " & CStr(dAbsoluteTrigger) & ";"
        sCommand = sCommand & ":SENS" & nchannel & ":EVEN:LEV:ABS:AUTO OFF;"
    End If
    
    If Not bTriggerSlopeNegative Then
        sCommand = sCommand & ":SENS" & nchannel & ":EVEN:SLOP POS;"
    Else
        sCommand = sCommand & ":SENS" & nchannel & ":EVEN:SLOP NEG;"
    End If
    
    sCommand = sCommand & ":SENS" & nchannel & ":EVEN:HYST " & UCase(sHysteresis) & ";"
    
    If bSetupAperature = True Then
        sCommand = sCommand & ":SENS" & nchannel & ":FREQ:APER " & CStr(dAperature) & ";"
        
        If bNotFreq Then
            sCommand = sCommand & ":SENS" & nchannel & ":PER:APER " & CStr(dAperature) & ";"
            sCommand = sCommand & ":SENS" & nchannel & ":RAT:APER " & CStr(dAperature) & ";"
        End If
    End If
    
    If dRange = -1 Then
        sCommand = sCommand & ":SENS" & nchannel & ":FREQ:RANG:AUTO ON;"
    Else
        sCommand = sCommand & ":SENS" & nchannel & ":FREQ:RANG:AUTO OFF;"
    End If
    
    Select Case UCase(sTimeBaseSource)
        Case "VXI"
            sCommand = sCommand & ":SENS" & nchannel & ":ROSC:SOUR CLK10;"
        Case "INTERNAL"
            sCommand = sCommand & ":SENS" & nchannel & ":ROSC:SOUR INT;"
        Case "EXTERNAL"
            sCommand = sCommand & ":SENS" & nchannel & ":ROSC:SOUR EXT;"
    End Select
    
    If bTimeIntervalDelay Then
        sCommand = sCommand & ":SENS" & nchannel & ":TINT:DEL:STAT ON;"
        sCommand = sCommand & ":SENS" & nchannel & ":TINT:DEL:TIME " & CStr(dTimeIntervalDelay) & ";"
    Else
        sCommand = sCommand & ":SENS" & nchannel & ":TINT:DEL:STAT OFF;"
    End If
    
    If bTotalizeGateMeasure Then
        sCommand = sCommand & ":SENS" & nchannel & ":TOT:GATE:STAT ON;"
        
        If bTotalizeGatePolarityInverted Then
            sCommand = sCommand & ":SENS" & nchannel & ":TOT:GATE:POL INV;"
        Else
            sCommand = sCommand & ":SENS" & nchannel & ":TOT:GATE:POL NORM;"
        End If
    Else
        sCommand = sCommand & ":SENS" & nchannel & ":TOT:GATE:STAT OFF;"
        sCommand = sCommand & ":SENS" & nchannel & ":TOT:GATE:POL NORM;"
    End If
    
    Delay 0.5
    
    'Write SENSe Subset to instrument
    WriteMsg COUNTER, sCommand
    Debug.Print sCommand
    
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
    
    Delay 0.2
    
    'Write ARM Subset to instrument
    WriteMsg COUNTER, sCommand
    Debug.Print sCommand

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
    
    Delay 0.1
    
    'Write OUTPut Subset to instrument
    WriteMsg COUNTER, sCommand
    Debug.Print sCommand
    
    'Configure CONFigure Options
    sCommand = ""
    If dRange <> -1 Then
        sCommand = ":CONF" & nchannel & ":" & sConfigString & ";"
        WriteMsg COUNTER, sCommand
        Debug.Print sCommand
    End If

End Function

Private Function dDoTheMeasurement(APROBE As String, nchannel As Integer) As Double

    Dim sData As String
    Dim nErr As Long
    
    Select Case Left(UCase(APROBE), 1)
    
        Case "O"    ' Off
        
            WriteMsg COUNTER, "INIT" & CStr(nchannel) & ";FETCH" & CStr(nchannel) & "?"
            nErr = ReadMsg(COUNTER, sData)
            
            If nErr <> 0 Then
                dDoTheMeasurement = 9.9E+37:
                frmMain.txtMeasured.Text = 9.9E+37
                Err.Raise nErr
                Exit Function
            End If
            
            If sData = "" Then
                sData = "9.91E+37"
            End If
            
            dDoTheMeasurement = CDbl(sData)
            frmMain.txtMeasured.Text = CSng(dDoTheMeasurement)
            
            If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
            
        Case "S"    ' Single
        
            MisProbe = vbYes
            
            Do While MisProbe = vbYes
            
                frmMain.lblStatus.Caption = "Waiting for Probe ..."
                Failed = False
                UserEvent = 0
                frmMain.TimerProbe.Enabled = True:
                frmMain.fraInstructions.Visible = True
                
                Do While (UserEvent = 0) And (Not bProbeClosed)
                    DoEvents
                Loop
                
                Do While (UserEvent = 0) And (bProbeClosed)
                    DoEvents
                Loop
                
                frmMain.TimerProbe.Enabled = False
                bProbeClosed = False
                frmMain.lblStatus.Caption = "Making Measurement ..."
                WriteMsg COUNTER, "INIT" & CStr(nchannel) & ";FETCH" & CStr(nchannel) & "?"
                nErr = ReadMsg(COUNTER, sData)
                
                If nErr <> 0 Then
                    dDoTheMeasurement = 9.9E+37:
                    frmMain.txtMeasured.Text = 9.9E+37
                    Err.Raise nErr
                    Exit Function
                End If
                
                dDoTheMeasurement = CDbl(sData)
                frmMain.txtMeasured.Text = CSng(dDoTheMeasurement)
                
                If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
                
                frmMain.lblStatus.Caption = "Measurement Complete ..."
                
                If Failed = True Then
                    MisProbe = MsgBox("Analog Probe Measurement Failed. " & vbCrLf & _
                                      "Do you wish to re-probe?", _
                                      vbCritical + vbYesNo + vbSystemModal, "Analog Probe Failure")
                Else
                    MisProbe = vbNo
                End If
            Loop
            
        Case "C"    ' Continuous
        
            MisProbe = vbYes
            
            Do While MisProbe = vbYes
            
                Failed = False
                UserEvent = 0
                frmMain.TimerProbe.Enabled = True:
                frmMain.fraInstructions.Visible = True
                
                Do While (UserEvent = 0) And (Not bProbeClosed)
                    frmMain.lblStatus.Caption = "Making Measurement ..."
                    WriteMsg COUNTER, "INIT" & CStr(nchannel) & ";FETCH" & CStr(nchannel) & "?"
                    nErr = ReadMsg(COUNTER, sData)
                    
                    If nErr <> 0 Then
                        dDoTheMeasurement = 9.9E+37: frmMain.txtMeasured.Text = 9.9E+37
                        Err.Raise nErr
                        Exit Function
                    End If
                    
                    dDoTheMeasurement = CDbl(sData)
                    frmMain.txtMeasured.Text = CSng(dDoTheMeasurement)
                    
                    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
                Loop
                
                Do While (UserEvent = 0) And (bProbeClosed)
                    DoEvents
                Loop
                
                frmMain.TimerProbe.Enabled = False
                bProbeClosed = False
                frmMain.lblStatus.Caption = "Measurement Complete ..."
                
                If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
                
                If Failed = True Then
                    MisProbe = MsgBox("Analog Probe Measurement Failed. " & vbCrLf & _
                                      "Do you wish to re-probe?", _
                                      vbCritical + vbYesNo + vbSystemModal, "Analog Probe Failure")
                Else
                    MisProbe = vbNo
                End If
            Loop
    End Select
    
End Function



