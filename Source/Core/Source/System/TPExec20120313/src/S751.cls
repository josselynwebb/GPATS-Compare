VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "S751"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
'**************************************************************
'* Third Echelon Test Set (TETS) Software Module              *
'*                                                            *
'* Nomenclature   : S751 Switch Utility                       *
'* Written By     : G. Johnson                                *
'* Purpose        : This Clase module contains definitions    *
'*                  for switch names                          *
'**************************************************************
Option Explicit
DefInt A-Z

Public Sub swClose(nPinA As Integer, nPinB As Integer)
        
    DoSwitchS751 nPinA, nPinB, "CLOSE", "swClose"
    
End Sub

Public Sub swOpen(Optional nPinA As Integer, Optional nPinB As Integer)
    
    DoSwitchS751 nPinA, nPinB, "OPEN", "swOpen"
    
End Sub

Public Sub swOpenAll()
    
    DoSwitchS751 0, 0, "OPEN", "swOpenAll", "ALL", "3.0000-0007"
    
End Sub

Private Sub DoSwitchS751(nPinA As Integer, nPinB As Integer, sMethod As String, sCall As String, _
                         Optional sSwitchType As String = "Single", Optional sAllSwitches As String)
    
    Dim sSwitch As String

    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    frmMain.txtInstrument.Text = "Switch S751"
    frmMain.txtCommand.Text = sCall
    
    Select Case sSwitchType
    
        Case "ALL"
        
            sSwitch = sAllSwitches
            
        Case Else
    
            If nPinA < 1 Or nPinA > 2 Then
                Echo "SWITCH S751 PROGRAMMING ERROR:  Command S751.swOpen nPinA argument out of range."
                Err.Raise -1000
                Exit Sub
            End If
            If nPinB < 3 Or nPinB > 18 Then
                Echo "SWITCH S751 PROGRAMMING ERROR:  Command S751.swOpen nPinB argument out of range."
                Err.Raise -1000
                Exit Sub
            End If
            
            Select Case nPinA
                Case 1
                    If (nPinB Mod 2) = 0 Then
                        Echo "SWITCH S751 PROGRAMMING ERROR:  Command S751.swOpen invalid switch closure. Check Pin arguments."
                        Err.Raise -1000
                        Exit Sub
                    End If
                Case Else
                    If (nPinB Mod 2) <> 0 Then
                        Echo "SWITCH S751 PROGRAMMING ERROR:  Command S751.swOpen invalid switch closure. Check Pin arguments."
                        Err.Raise -1000
                        Exit Sub
                    End If
            End Select
            
            
            Select Case nPinB
                Case 3, 4
                    sSwitch = "3.0000"
                Case 5, 6
                    sSwitch = "3.0001"
                Case 7, 8
                    sSwitch = "3.0002"
                Case 9, 10
                    sSwitch = "3.0003"
                Case 11, 12
                    sSwitch = "3.0004"
                Case 13, 14
                    sSwitch = "3.0005"
                Case 15, 16
                    sSwitch = "3.0006"
                Case 17, 18
                    sSwitch = "3.0007"
            End Select
    End Select
    
    If Not bSimulation Then WriteSW " " & sMethod & " " & sSwitch

End Sub

