VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "S202"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
'**************************************************************
'* Third Echelon Test Set (TETS) Software Module              *
'*                                                            *
'* Nomenclature   : S202 Switch Utility                       *
'* Written By     : G. Johnson                                *
'* Purpose        : This Clase module contains definitions    *
'*                  for switch names                          *
'**************************************************************
Option Explicit
DefInt A-Z

Public Sub swClose(nPinA As Integer, nPinB As Integer)
    
    DoSwitchS202 nPinA, nPinB, "CLOSE", "swClose", "202"
    
End Sub

Public Sub swOpen(Optional nPinA As Integer, Optional nPinB As Integer)
    
    DoSwitchS202 nPinA, nPinB, "OPEN", "swOpen", "202"
    
End Sub

Public Sub swOpenAll()
    
    DoSwitchS202 0, 0, "OPEN", "swOpenAll", "202", "ALL", "3.0100-0157"
    
End Sub

Private Sub DoSwitchS202(nPinA As Integer, nPinB As Integer, sMethod As String, _
                         sCall As String, sSwitchNumber As String, _
                         Optional sSwitchType As String = "Single", _
                         Optional sAllSwitches As String)
    Dim sSwitch As String

    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    frmMain.txtInstrument.Text = "Switch " & sSwitchNumber
    frmMain.txtCommand.Text = sCall
    
    Select Case sSwitchType
    
        Case "ALL"
        
            sSwitch = sAllSwitches
            
        Case Else
    
            If Switch200ErrorCheck(sSwitchNumber, sCall, nPinA, nPinB) = False Then Exit Sub
    
            Select Case nPinB
                Case 5, 6
                    If nPinA < 3 Then sSwitch = "3.0100" Else sSwitch = "3.0110"
                Case 7, 8
                    If nPinA < 3 Then sSwitch = "3.0101" Else sSwitch = "3.0111"
                Case 9, 10
                    If nPinA < 3 Then sSwitch = "3.0102" Else sSwitch = "3.0112"
                Case 11, 12
                    If nPinA < 3 Then sSwitch = "3.0103" Else sSwitch = "3.0113"
                Case 13, 14
                    If nPinA < 3 Then sSwitch = "3.0104" Else sSwitch = "3.0114"
                Case 15, 16
                    If nPinA < 3 Then sSwitch = "3.0105" Else sSwitch = "3.0115"
                Case 17, 18
                    If nPinA < 3 Then sSwitch = "3.0106" Else sSwitch = "3.0116"
                Case 19, 20
                    If nPinA < 3 Then sSwitch = "3.0107" Else sSwitch = "3.0117"
                Case 21, 22
                    If nPinA < 3 Then sSwitch = "3.0120" Else sSwitch = "3.0130"
                Case 23, 24
                    If nPinA < 3 Then sSwitch = "3.0121" Else sSwitch = "3.0131"
                Case 25, 26
                    If nPinA < 3 Then sSwitch = "3.0122" Else sSwitch = "3.0132"
                Case 27, 28
                    If nPinA < 3 Then sSwitch = "3.0123" Else sSwitch = "3.0133"
                Case 29, 30
                    If nPinA < 3 Then sSwitch = "3.0124" Else sSwitch = "3.0134"
                Case 31, 32
                    If nPinA < 3 Then sSwitch = "3.0125" Else sSwitch = "3.0135"
                Case 33, 34
                    If nPinA < 3 Then sSwitch = "3.0126" Else sSwitch = "3.0136"
                Case 35, 36
                    If nPinA < 3 Then sSwitch = "3.0127" Else sSwitch = "3.0137"
                Case 37, 38
                    If nPinA < 3 Then sSwitch = "3.0140" Else sSwitch = "3.0150"
                Case 39, 40
                    If nPinA < 3 Then sSwitch = "3.0141" Else sSwitch = "3.0151"
                Case 41, 42
                    If nPinA < 3 Then sSwitch = "3.0142" Else sSwitch = "3.0152"
                Case 43, 44
                    If nPinA < 3 Then sSwitch = "3.0143" Else sSwitch = "3.0153"
                Case 45, 46
                    If nPinA < 3 Then sSwitch = "3.0144" Else sSwitch = "3.0154"
                Case 47, 48
                    If nPinA < 3 Then sSwitch = "3.0145" Else sSwitch = "3.0155"
                Case 49, 50
                    If nPinA < 3 Then sSwitch = "3.0146" Else sSwitch = "3.0156"
                Case 51, 52
                    If nPinA < 3 Then sSwitch = "3.0147" Else sSwitch = "3.0157"
            End Select
            
    End Select
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    If Not bSimulation Then WriteSW " " & sMethod & " " & sSwitch

End Sub

