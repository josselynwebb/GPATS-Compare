VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "S101"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
'**************************************************************
'* TETS Software Module                                       *
'*                                                            *
'* Nomenclature   : S101 Switch Utility                       *
'* Written By     : G. Johnson                                *
'* Purpose        : This Clase module contains definitions    *
'*                  for switch names                          *
'**************************************************************
Option Explicit
DefInt A-Z

Public Sub swClose(nPinA As Integer, nPinB As Integer)
        
    DoSwitchS101 nPinA, nPinB, "CLOSE", "swClose"
    
End Sub

Public Sub swOpen(Optional nPinA As Integer, Optional nPinB As Integer)
    
    DoSwitchS101 nPinA, nPinB, "OPEN", "swOpen"
    
End Sub

Public Sub swOpenAll()
    
    DoSwitchS101 0, 0, "OPEN", "swOpenAll", "ALL", "1.0000-0004;2.0000-0004"
    
End Sub

Private Sub DoSwitchS101(nPinA As Integer, nPinB As Integer, sMethod As String, sCall As String, _
                         Optional sSwitchType As String = "Single", Optional sAllSwitches As String)
    
    Dim sSwitch As String

    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    frmMain.txtInstrument.Text = "Switch S101"
    frmMain.txtCommand.Text = sCall
    
    Select Case sSwitchType
    
        Case "ALL"
        
            sSwitch = sAllSwitches
            
        Case Else
    
            If nPinA > 39 Or nPinA < 1 Then
                Echo "SWITCH S101 PROGRAMMING ERROR:  Command S101." & sCall & " nPinA argument out of range."
                Err.Raise -1000
                Exit Sub
            End If
            If nPinB > 40 Or nPinB < 2 Then
                Echo "SWITCH S101 PROGRAMMING ERROR:  Command S101." & sCall & " nPinB argument out of range."
                Err.Raise -1000
                Exit Sub
            End If
            If nPinA > nPinB Then
                Echo "SWITCH S101 PROGRAMMING ERROR:  Command S101." & sCall & " nPinA argument must be less than nPinB argument."
                Err.Raise -1000
                Exit Sub
            End If
            If (nPinA + 1) <> nPinB Then
                Echo "SWITCH S101 PROGRAMMING ERROR:  Command S101." & sCall & " invalid switch closure. Check Pin arguments."
                Err.Raise -1000
                Exit Sub
            End If
            
            Select Case nPinA
                Case 1, 3
                    sSwitch = "1.0000"
                Case 5, 7
                    sSwitch = "1.0001"
                Case 9, 11
                    sSwitch = "1.0002"
                Case 13, 15
                    sSwitch = "1.0003"
                Case 17, 19
                    sSwitch = "1.0004"
                Case 21, 23
                    sSwitch = "2.0000"
                Case 25, 27
                    sSwitch = "2.0001"
                Case 29, 31
                    sSwitch = "2.0002"
                Case 33, 35
                    sSwitch = "2.0003"
                Case 37, 39
                    sSwitch = "2.0004"
                Case Else
                    Echo "SWITCH S101 PROGRAMMING ERROR:  Command S101." & sCall & " invalid switch closure. Check Pin arguments."
                    Err.Raise -1000
                    Exit Sub
            End Select
    End Select
    
    If Not bSimulation Then WriteSW " " & sMethod & " " & sSwitch

End Sub

