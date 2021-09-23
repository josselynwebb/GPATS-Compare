VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "S201"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
'**************************************************************
'* Third Echelon Test Set (TETS) Software Module              *
'*                                                            *
'* Nomenclature   : S201 Switch Utility                       *
'* Written By     : G. Johnson                                *
'* Purpose        : This Clase module contains definitions    *
'*                  for switch names                          *
'**************************************************************
Option Explicit
DefInt A-Z

Public Sub swClose(nPinA As Integer, nPinB As Integer)
        
    DoSwitchS201 nPinA, nPinB, "CLOSE", "swClose", "201"
    
End Sub

Public Sub swOpen(Optional nPinA As Integer, Optional nPinB As Integer)
    
    DoSwitchS201 nPinA, nPinB, "OPEN", "swOpen", "201"
    
End Sub

Public Sub swOpenAll()
    
    DoSwitchS201 0, 0, "OPEN", "swOpenAll", "201", "ALL", "3.0040-0097"
    
End Sub

Private Sub DoSwitchS201(nPinA As Integer, nPinB As Integer, sMethod As String, _
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
                    If nPinA < 3 Then sSwitch = "3.0040" Else sSwitch = "3.0050"
                Case 7, 8
                    If nPinA < 3 Then sSwitch = "3.0041" Else sSwitch = "3.0051"
                Case 9, 10
                    If nPinA < 3 Then sSwitch = "3.0042" Else sSwitch = "3.0052"
                Case 11, 12
                    If nPinA < 3 Then sSwitch = "3.0043" Else sSwitch = "3.0053"
                Case 13, 14
                    If nPinA < 3 Then sSwitch = "3.0044" Else sSwitch = "3.0054"
                Case 15, 16
                    If nPinA < 3 Then sSwitch = "3.0045" Else sSwitch = "3.0055"
                Case 17, 18
                    If nPinA < 3 Then sSwitch = "3.0046" Else sSwitch = "3.0056"
                Case 19, 20
                    If nPinA < 3 Then sSwitch = "3.0047" Else sSwitch = "3.0057"
                Case 21, 22
                    If nPinA < 3 Then sSwitch = "3.0060" Else sSwitch = "3.0070"
                Case 23, 24
                    If nPinA < 3 Then sSwitch = "3.0061" Else sSwitch = "3.0071"
                Case 25, 26
                    If nPinA < 3 Then sSwitch = "3.0062" Else sSwitch = "3.0072"
                Case 27, 28
                    If nPinA < 3 Then sSwitch = "3.0063" Else sSwitch = "3.0073"
                Case 29, 30
                    If nPinA < 3 Then sSwitch = "3.0064" Else sSwitch = "3.0074"
                Case 31, 32
                    If nPinA < 3 Then sSwitch = "3.0065" Else sSwitch = "3.0075"
                Case 33, 34
                    If nPinA < 3 Then sSwitch = "3.0066" Else sSwitch = "3.0076"
                Case 35, 36
                    If nPinA < 3 Then sSwitch = "3.0067" Else sSwitch = "3.0077"
                Case 37, 38
                    If nPinA < 3 Then sSwitch = "3.0080" Else sSwitch = "3.0090"
                Case 39, 40
                    If nPinA < 3 Then sSwitch = "3.0081" Else sSwitch = "3.0091"
                Case 41, 42
                    If nPinA < 3 Then sSwitch = "3.0082" Else sSwitch = "3.0092"
                Case 43, 44
                    If nPinA < 3 Then sSwitch = "3.0083" Else sSwitch = "3.0093"
                Case 45, 46
                    If nPinA < 3 Then sSwitch = "3.0084" Else sSwitch = "3.0094"
                Case 47, 48
                    If nPinA < 3 Then sSwitch = "3.0085" Else sSwitch = "3.0095"
                Case 49, 50
                    If nPinA < 3 Then sSwitch = "3.0086" Else sSwitch = "3.0096"
                Case 51, 52
                    If nPinA < 3 Then sSwitch = "3.0087" Else sSwitch = "3.0097"
            End Select
            
    End Select
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    If Not bSimulation Then WriteSW " " & sMethod & " " & sSwitch

End Sub

