VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "S906"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
'**************************************************************
'* Third Echelon Test Set (TETS) Software Module              *
'*                                                            *
'* Nomenclature   : S906 Switch Utility                       *
'* Written By     : G. Johnson                                *
'* Purpose        : This Clase module contains definitions    *
'*                  for switch names                          *
'**************************************************************
Option Explicit
DefInt A-Z

Public Sub swClose(nPinA As Integer, nPinB As Integer)
    
    DoSwitchS906 nPinA, nPinB, "CLOSE", "swClose", "906"
    
End Sub

Public Sub swOpen()
    
    DoSwitchS906 0, 0, "OPEN", "swOpen", "906", "ALL", "5.50-55"
    
End Sub


Private Sub DoSwitchS906(nPinA As Integer, nPinB As Integer, sMethod As String, _
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
    
            If Switch800900ErrorCheck(sSwitchNumber, sCall, nPinA, nPinB, 7) = False Then Exit Sub
    
            sSwitch = "5.5" & CStr(nPinB - 2)
            
    End Select
    
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    If Not bSimulation Then WriteSW " " & sMethod & " " & sSwitch

End Sub


