VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "S509"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
'**************************************************************
'* Third Echelon Test Set (TETS) Software Module              *
'*                                                            *
'* Nomenclature   : S509 Switch Utility                       *
'* Written By     : G. Johnson                                *
'* Purpose        : This Clase module contains definitions    *
'*                  for switch names                          *
'**************************************************************
Option Explicit
DefInt A-Z


Public Sub swClose(nPinA As Integer, nPinB As Integer)
    
    DoSwitchS509 nPinA, nPinB, "CLOSE", "swClose", "509"
    
End Sub

Public Sub swOpen(Optional nPinA As Integer, Optional nPinB As Integer)
    
    DoSwitchS509 nPinA, nPinB, "OPEN", "swOpen", "509"
    
End Sub

Public Sub swOpenAll()
    
    DoSwitchS509 0, 0, "OPEN", "swOpenAll", "509", "ALL", "2.4300-4317"
    
End Sub

Private Sub DoSwitchS509(nPinA As Integer, nPinB As Integer, sMethod As String, _
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
                sSwitch = "2.430"
            Else
                sSwitch = "2.431"
            End If
    
            sSwitch = sSwitch & CStr(nPinB - 3)
            
    End Select
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    If Not bSimulation Then WriteSW " " & sMethod & " " & sSwitch

End Sub


