VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "S301"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
'**************************************************************
'* Third Echelon Test Set (TETS) Software Module              *
'*                                                            *
'* Nomenclature   : S301 Switch Utility                       *
'* Written By     : G. Johnson                                *
'* Purpose        : This Clase module contains definitions    *
'*                  for switch names                          *
'**************************************************************
Option Explicit
DefInt A-Z

Public Sub swClose(nPinA As Integer, nPinB As Integer)
        
    DoSwitchS301 nPinA, nPinB, "CLOSE", "swClose"
    
End Sub

Public Sub swOpen(Optional nPinA As Integer, Optional nPinB As Integer)
    
    DoSwitchS301 nPinA, nPinB, "OPEN", "swOpen"
    
End Sub

Public Sub swOpenAll()
    
    DoSwitchS301 0, 0, "OPEN", "swOpenAll", "ALL", "1.1000-1047;2.1000-1047"
    
End Sub

Private Sub DoSwitchS301(nPinA As Integer, nPinB As Integer, sMethod As String, sCall As String, _
                         Optional sSwitchType As String = "Single", Optional sAllSwitches As String)
    
    Dim sSwitch As String

    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    frmMain.txtInstrument.Text = "Switch S301"
    frmMain.txtCommand.Text = sCall
    
    Select Case sSwitchType
    
        Case "ALL"
        
            sSwitch = sAllSwitches
            
        Case Else
    
            If nPinA < 1 Or nPinA > 191 Then
                Echo "SWITCH S301 PROGRAMMING ERROR:  Command S301." & sCall & " nPinA argument out of range."
                Err.Raise -1000
                Exit Sub
            End If
            If nPinB < 2 Or nPinB > 192 Then
                Echo "SWITCH S301 PROGRAMMING ERROR:  Command S301." & sCall & " nPinB argument out of range."
                Err.Raise -1000
                Exit Sub
            End If
            If nPinA > nPinB Then
                Echo "SWITCH S301 PROGRAMMING ERROR:  Command S301." & sCall & " nPinA argument must be less than nPinB argument."
                Err.Raise -1000
                Exit Sub
            End If
            If (nPinA Mod 2) = 0 Then
                Echo "SWITCH S301 PROGRAMMING ERROR:  Command S301." & sCall & " nPinA argument out of range."
                Err.Raise -1000
                Exit Sub
            End If
            If (nPinB Mod 2) <> 0 Then
                Echo "SWITCH S301 PROGRAMMING ERROR:  Command S301." & sCall & " nPinB argument out of range."
                Err.Raise -1000
                Exit Sub
            End If
            If (nPinA + 1) <> nPinB Then
                Echo "SWITCH S301 PROGRAMMING ERROR:  Command S301." & sCall & " nPinA argument must be adjacent nPinB argument."
                Err.Raise -1000
                Exit Sub
            End If
            
            Select Case nPinB
                Case 2 To 96
                    sSwitch = "1." & CStr((nPinB / 2) + 999)         ' 1.1000, 1.1001,,,1.1047
                Case 98 To 192
                    sSwitch = "2." & CStr(((nPinB - 98) / 2) + 1000) ' 2.1000, 2.1001,,,2.1047
            End Select
    End Select
    
    If Not bSimulation Then WriteSW sMethod & " " & sSwitch

End Sub




