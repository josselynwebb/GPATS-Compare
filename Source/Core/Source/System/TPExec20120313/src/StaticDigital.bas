Attribute VB_Name = "StaticDigital"
'************************************************************
'* Third Echelon Test Set (TETS) Software Module              *
'*                                                            *
'* Nomenclature   : DTS UTILITY FUNCTIONS                     *
'* Written By     : G. Johnson                                *
'* Purpose        : This module contains code for the DTS     *
'*                  DO NOT MODIFY                             *
'**************************************************************
'***********************Digital Declarations***************************
Sub IOX(nCh As Variant, Optional nLevelSet As Long = 0)
    'Driver and Detector Tristate for a single Channel or group of pins
    'Example:
    '           IOX P1_3
    
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
    
    cmdDTS.IOX nChArray, nLevelSet
End Sub

Sub DISCONNECT(nCh As Variant, Optional PerformVerify As Long = 0)
    'Digital relay open for a single or group of pins.
    'Also verification can be performed for safety or omitted to save
    'run time if the purpose is not critical.
    'Example:
    '           DISCONNECT P1_3
    '               -OR-
    '           DISCONNECT PinList()
    
    
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
        "Attention! In command DISCONNECT the argument for nCh is not understood!" + vbCrLf + _
        "The procedure will attempt to open all relays to prevent damage to the DTI."
        ReDim nChArray(0 To 191) As Long
        For iCount = 0 To 191
          nChArray(iCount) = iCount
        Next iCount
    End If
    
    cmdDTS.DISCONNECT nChArray, True
    
End Sub
Sub IL(nCh As Variant, Optional nLevelSet As Long = 0)
    'Input Low for a single Channel or group of pins
    'Example:
    '           IL P1_3
    
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
    
    cmdDTS.IL nChArray, nLevelSet
End Sub
Sub IH(nCh As Variant, Optional nLevelSet As Long = 0)
    'Input High for a single Channel
    'Example:
    '           IH P1_3
    
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
    
    cmdDTS.IH nChArray, nLevelSet
End Sub
Sub ML(nCh As Variant, Optional nLevelSet As Long = 0)
    'Monitor Low for a single Channel
    'Example:
    '           ML P1_3
    
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
    
    cmdDTS.ML nChArray, nLevelSet
End Sub
Sub MH(nCh As Variant, Optional nLevelSet As Long = 0)
    'Monitor High for a single Channel
    'Example:
    '           MH P1_3
    
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
    
    cmdDTS.MH nChArray, nLevelSet
End Sub
Sub OL(nCh As Variant, Optional nLevelSet As Long = 0)
    'Output Low for a single Channel
    'Example:
    '           OL P1_3
    
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
    
    cmdDTS.OL nChArray, nLevelSet
End Sub
Sub OH(nCh As Variant, Optional nLevelSet As Long = 0)
    'Output High for a single Channel
    'Example:
    '           OH P1_3
    
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
    
    cmdDTS.OH nChArray, nLevelSet
End Sub
Sub OB(nCh As Variant, Optional nLevelSet As Long = 0)
    'Output Between for a single Channel
    'Example:
    '           OB P1_3
    
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
    
    cmdDTS.OB nChArray, nLevelSet
End Sub
Sub OK(nCh As Variant, Optional nLevelSet As Long = 0)
    'Output not Between for a single Channel
    'Example:
    '           OK P1_3
    
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
    
    cmdDTS.OK nChArray, nLevelSet
End Sub
Sub ILOH(nCh As Variant, Optional nLevelSet As Long = 0)
    'Input Low Output High for a single Channel
    'Example:
    '           ILOH P1_3
    
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
    
    cmdDTS.ILOH nChArray, nLevelSet
End Sub
Sub IHOL(nCh As Variant, Optional nLevelSet As Long = 0)
    'Input High Output low for a single Channel
    'Example:
    '           IHOL P1_3
    
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
    
    cmdDTS.IHOL nChArray, nLevelSet
End Sub
Sub KEEP(nCh As Variant, Optional nLevelSet As Long = 0)
    'Keep pin state for a single Channel
    'Example:
    '           KEEP P1_3
    
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
    
    cmdDTS.KEEP nChArray, nLevelSet
End Sub
Sub TOG(nCh As Variant, Optional nLevelSet As Long = 0)
    'Toggle pin state for a single Channel
    'Example:
    '           TOG P1_3
    
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
    
    cmdDTS.TOG nChArray, nLevelSet
End Sub
Sub SIG(nCh As Variant, Optional nLevelSet As Long = 0)
    'Clock Signature generator drive tristate for a single Channel
    'Example:
    '           SIG P1_3
    
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
    
    cmdDTS.SIG nChArray, nLevelSet
End Sub
Sub SetState(nCh As Variant, nState As Long, Optional nLevelSet As Long = 0, Optional bOneBase As Boolean = False)
    'Drive a Group of Channel Pins to a defined state
    'Example:                    LSB                 MSB
    '           SetState_ Array(P1_33, P1_34, P1_35, P1_36), &HF
    
    Dim nChArray() As Long
    Dim iCount As Integer
    
    If IsArray(nCh) Then
        If Not bOneBase Then
            ReDim nChArray(0 To UBound(nCh)) As Long
            For iCount = 0 To UBound(nCh)
                nChArray(iCount) = Val(nCh(iCount))
            Next iCount
        Else
            ReDim nChArray(0 To (UBound(nCh) - 1)) As Long
            For iCount = 0 To (UBound(nCh) - 1)
                nChArray(iCount) = Val(nCh(iCount + 1))
            Next iCount
        End If
    ElseIf IsNumeric(nCh) Then
        ReDim nChArray(0) As Long
        nChArray(0) = Val(nCh)
    Else
        nChArray(0) = -1
    End If

    cmdDTS.SetState nChArray, nState, nLevelSet
End Sub
Function State(nCh As Variant, Optional nLevelSet As Long = 0, _
                Optional APROBE As String = "OFF", _
                Optional bMisProbeMsg As Boolean = True, _
                Optional bOneBase As Boolean = False) As Long
    'Measures a single Chanel Pins State
    'Example:
    '           dMeasured = State(P1_33)
    Dim nChArray() As Long
    Dim iCount As Integer
    
    If IsArray(nCh) Then
        If Not bOneBase Then
            ReDim nChArray(0 To UBound(nCh)) As Long
            For iCount = 0 To UBound(nCh)
                nChArray(iCount) = Val(nCh(iCount))
            Next iCount
        Else
            ReDim nChArray(0 To (UBound(nCh) - 1)) As Long
            For iCount = 0 To (UBound(nCh) - 1)
                nChArray(iCount) = Val(nCh(iCount + 1))
            Next iCount
        End If
    ElseIf IsNumeric(nCh) Then
        ReDim nChArray(0) As Long
        nChArray(0) = Val(nCh)
    Else
        nChArray(0) = -1
    End If
    
    State = cmdDTS.State(nChArray, nLevelSet, APROBE, bMisProbeMsg)
End Function
Sub SetActiveLoad(nCh As Variant, Optional bState As Boolean = True, Optional nLevelSet As Long = 0, Optional bOneBase As Boolean = False)
    'Measures a single Chanel Pins State
    'Example:
    '           dMeasured = State(P1_33)
    Dim nChArray() As Long
    Dim iCount As Integer
    
    If IsArray(nCh) Then
        If Not bOneBase Then
            ReDim nChArray(0 To UBound(nCh)) As Long
            For iCount = 0 To UBound(nCh)
                nChArray(iCount) = Val(nCh(iCount))
            Next iCount
        Else
            ReDim nChArray(0 To (UBound(nCh) - 1)) As Long
            For iCount = 0 To (UBound(nCh) - 1)
                nChArray(iCount) = Val(nCh(iCount + 1))
            Next iCount
        End If
    ElseIf IsNumeric(nCh) Then
        ReDim nChArray(0) As Long
        nChArray(0) = Val(nCh)
    Else
        nChArray(0) = -1
    End If
    
    cmdDTS.SetActiveLoad nChArray, bState, nLevelSet
End Sub

