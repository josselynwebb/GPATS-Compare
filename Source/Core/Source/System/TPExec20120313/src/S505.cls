VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "S505"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
'**************************************************************
'* Third Echelon Test Set (TETS) Software Module              *
'*                                                            *
'* Nomenclature   : S505 Switch Utility                       *
'* Written By     : G. Johnson                                *
'* Purpose        : This Clase module contains definitions    *
'*                  for switch names                          *
'**************************************************************
Option Explicit
DefInt A-Z


Public Sub swClose(nPinA As Integer, nPinB As Integer)
    
    DoSwitchS505 nPinA, nPinB, "CLOSE", "swClose", "505"
    
End Sub

Public Sub swOpen(Optional nPinA As Integer, Optional nPinB As Integer)
    
    DoSwitchS505 nPinA, nPinB, "OPEN", "swOpen", "505"
    
End Sub

Public Sub swOpenAll()
    
    DoSwitchS505 0, 0, "OPEN", "swOpenAll", "505", "ALL", "1.4400-4417"
    
End Sub

Private Sub DoSwitchS505(nPinA As Integer, nPinB As Integer, sMethod As String, _
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
    
            If Switch500ErrorCheck(sSwitchNumber, sCall, nPinA, nPinB) = False Then Exit Sub
    
            If nPinA = 1 Then
                sSwitch = "1.440"
            Else
                sSwitch = "1.441"
            End If
    
            sSwitch = sSwitch & CStr(nPinB - 3)
            
    End Select
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    If Not bSimulation Then WriteSW " " & sMethod & " " & sSwitch

End Sub

