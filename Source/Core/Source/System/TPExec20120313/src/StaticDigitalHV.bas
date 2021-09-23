Attribute VB_Name = "StaticDigitalHV"
'**************************************************************
'* Third Echelon Test Set (TETS) Software Module              *
'*                                                            *
'* Nomenclature   : DTS HV PIN FUNCTIONS                      *
'* Written By     : B. Bamford, Astronics, DME Corporation    *
'* Purpose        : This module contains code for the DTS     *
'*                  HV Pins.     DO NOT MODIFY                *
'* Required code for all TP that use HV pins.                 *
'**************************************************************
Option Explicit
DefInt A-Z


'***********************Digital Declarations***************************
Sub HVDISCONNECT(nCh As Variant, Optional PerformVerify As Boolean = True)
    'Digital relay open for a single pin or group of pins.
    'Also verification can be performed for safety or omitted to save
    'run time if the purpose is not critical.
    'Example:
    ' HVDISCONNECT HVP1_3
    ' HVDISCONNECT HVPinList()
    
    Dim nChArray() As Long
    Dim iCount As Integer
    
    If IsArray(nCh) Then
        ReDim nChArray(LBound(nCh) To UBound(nCh)) As Long
        For iCount = LBound(nCh) To UBound(nCh)
            nChArray(iCount) = Val(nCh(iCount))
        Next iCount
    ElseIf IsNumeric(nCh) Then
        ReDim nChArray(0) As Long
        nChArray(0) = Val(nCh)
    Else
        MsgBox _
        "Attention! In command HVDISCONNECT the argument for nCh is not understood!" + vbCrLf + _
        "The procedure will attempt to open all relays to prevent damage to the DTI."
        ReDim nChArray(0 To 191) As Long
        For iCount = 0 To 191
          nChArray(iCount) = iCount
        Next iCount
    End If
    
    cmdDTS.HVDISCONNECT nChArray, PerformVerify
    
End Sub

Sub HVIOX(nCh As Variant)
    'Driver and Detector Off for a single Pin or group of pins
    'Example:
    '  HVIOX P1_3
    '  HVIOX HVPinList()
    
    Dim nChArray() As Long
    Dim iCount As Integer
    
    If IsArray(nCh) Then
        ReDim nChArray(0 To UBound(nCh)) As Long
        For iCount = 0 To UBound(nCh)
            nChArray(iCount) = Val(nCh(iCount))
        Next iCount
    ElseIf IsNumeric(nCh) Then
        ReDim nChArray(0) As Long
        nChArray(0) = Val(nCh)
    Else
        nChArray(0) = -1
    End If
    
    cmdDTS.HVIOX nChArray
 
End Sub

Sub HVIH(nCh As Variant)
    'Input High for a single Pins or Group of Pins
    'Example:
    '  HVIH HVP1_3
    '  HVIH HVPinList()
    
    Dim nChArray() As Long
    Dim iCount As Integer
    
    If IsArray(nCh) Then
        ReDim nChArray(0 To UBound(nCh)) As Long
        For iCount = 0 To UBound(nCh)
            nChArray(iCount) = Val(nCh(iCount))
        Next iCount
    ElseIf IsNumeric(nCh) Then
        ReDim nChArray(0) As Long
        nChArray(0) = Val(nCh)
    Else
        nChArray(0) = -1
    End If
    
    cmdDTS.HVIH nChArray
 
End Sub

Sub HVIL(nCh As Variant)
    'Input Low for a single Pin or group of pins
    'Example:
    '  HVIL HVP1_3
    '  HVIL HVPinList()
    
    Dim nChArray() As Long
    Dim iCount As Integer
    
    If IsArray(nCh) Then
        ReDim nChArray(0 To UBound(nCh)) As Long
        For iCount = 0 To UBound(nCh)
            nChArray(iCount) = Val(nCh(iCount))
        Next iCount
    ElseIf IsNumeric(nCh) Then
        ReDim nChArray(0) As Long
        nChArray(0) = Val(nCh)
    Else
        nChArray(0) = -1
    End If
    
    cmdDTS.HVIL nChArray
 
End Sub

Sub HVOL(nCh As Variant, Optional ByRef dThreshold As Double = 2.5, _
          Optional ByRef nPinState As Long)
    'Output Low for a single Channel
    'Example:
    '  HVOL HVP1_3, 5.0
    '  HVOL HVPinList(), 5.0
    
    Dim nChArray() As Long
    Dim iCount As Integer
    
    If IsArray(nCh) Then
        ReDim nChArray(0 To UBound(nCh)) As Long
        For iCount = 0 To UBound(nCh)
            nChArray(iCount) = Val(nCh(iCount))
        Next iCount
    ElseIf IsNumeric(nCh) Then
        ReDim nChArray(0) As Long
        nChArray(0) = Val(nCh)
    Else
        nChArray(0) = -1
    End If
    
    cmdDTS.HVOL nChArray, dThreshold, nPinState
 
End Sub

Sub HVOH(nCh As Variant, Optional ByRef dThreshold As Double = 2.5, _
          Optional ByRef nPinState As Long)
    'Output High for a single Channel
    'Example:
    '  HVOH HVP1_3, 5.0
    '  HVOH HVPinList(), 5.0
    
    Dim nChArray() As Long
    Dim iCount As Integer
    
    If IsArray(nCh) Then
        ReDim nChArray(0 To UBound(nCh)) As Long
        For iCount = 0 To UBound(nCh)
            nChArray(iCount) = Val(nCh(iCount))
        Next iCount
    ElseIf IsNumeric(nCh) Then
        ReDim nChArray(0) As Long
        nChArray(0) = Val(nCh)
    Else
        nChArray(0) = -1
    End If
    
    cmdDTS.HVOH nChArray, dThreshold, nPinState
 
End Sub

Sub HVSetState(nCh As Variant, nState As Long)
    'Drive a Group of Channel Pins to a defined state
    'Example:
    '   HVSetState HVP1_33
    '                    LSB                        MSB
    '   HVSetState Array(HVP1_33, HVP1_34, HVP1_35, HVP1_36)
    
    Dim nChArray() As Long
    Dim iCount As Integer
    
    If IsArray(nCh) Then
       ReDim nChArray(0 To UBound(nCh)) As Long
       For iCount = 0 To UBound(nCh)
           nChArray(iCount) = Val(nCh(iCount))
       Next iCount
    ElseIf IsNumeric(nCh) Then
        ReDim nChArray(0) As Long
        nChArray(0) = Val(nCh)
    Else
        nChArray(0) = -1
    End If

    cmdDTS.HVSetState nChArray, nState

End Sub

Function HVState(nCh As Variant, Optional dThreshold As Double = 2.5, _
                Optional APROBE As String = "OFF", _
                Optional bMisProbeMsg As Boolean = True) As Long
    'Measures a single Chanel Pins State
    'Example:
    '  dMeasured = HVState HVP1_33
    '  dMeasured = HVState Array(HVP1_33, HVP1_34, HVP1_35, HVP1_36)
    Dim nChArray() As Long
    Dim iCount As Integer
    
    If IsArray(nCh) Then
      ReDim nChArray(0 To UBound(nCh)) As Long
      For iCount = 0 To UBound(nCh)
          nChArray(iCount) = Val(nCh(iCount))
      Next iCount
    ElseIf IsNumeric(nCh) Then
        ReDim nChArray(0) As Long
        nChArray(0) = Val(nCh)
    Else
        nChArray(0) = -1
    End If
    
    HVState = cmdDTS.HVState(nChArray, dThreshold, APROBE, bMisProbeMsg)
 
End Function

Sub HVreset()
    'Reset all 32 High Voltage Pins,  Sets all pins to OFF state
    'Example:
    '  HVReset
    
    cmdDTS.HVreset
 
End Sub



