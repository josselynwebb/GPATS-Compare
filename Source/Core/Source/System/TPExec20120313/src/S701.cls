VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "S701"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
'**************************************************************
'* Third Echelon Test Set (TETS) Software Module              *
'*                                                            *
'* Nomenclature   : S701 Switch Utility                       *
'* Written By     : G. Johnson                                *
'* Purpose        : This Clase module contains definitions    *
'*                  for switch names                          *
'**************************************************************
Option Explicit
DefInt A-Z

Public Sub swClose(nPinA As Integer, nPinB As Integer)
        
    DoSwitchS701 nPinA, nPinB, "CLOSE", "swClose"
    
End Sub

Public Sub swOpen(Optional nPinA As Integer, Optional nPinB As Integer)
    
    DoSwitchS701 nPinA, nPinB, "OPEN", "swOpen"
    
End Sub

Public Sub swOpenAll()
    
    DoSwitchS701 0, 0, "OPEN", "swOpenAll", "ALL", "3.0010-0037"
    
End Sub

Private Sub DoSwitchS701(nPinA As Integer, nPinB As Integer, sMethod As String, sCall As String, _
                         Optional sSwitchType As String = "Single", Optional sAllSwitches As String)
    
    Dim sSwitch As String

    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Sub
    
    frmMain.txtInstrument.Text = "Switch S701"
    frmMain.txtCommand.Text = sCall
    
    Select Case sSwitchType
    
        Case "ALL"
        
            sSwitch = sAllSwitches
            
        Case Else
    
            If nPinA < 1 Or nPinA > 2 Then
                Echo "SWITCH S701 PROGRAMMING ERROR:  Command S701." & sCall & " nPinA argument out of range."
                Err.Raise -1000
                Exit Sub
            End If
            If nPinB < 3 Or nPinB > 50 Then
                Echo "SWITCH S701 PROGRAMMING ERROR:  Command S701." & sCall & " nPinB argument out of range."
                Err.Raise -1000
                Exit Sub
            End If
            
            Select Case nPinA
                Case 1
                    If (nPinB Mod 2) = 0 Then
                        Echo "SWITCH S701 PROGRAMMING ERROR:  Command S701." & sCall & " invalid switch closure. Check Pin arguments."
                        Err.Raise -1000
                        Exit Sub
                    End If
                Case Else
                    If (nPinB Mod 2) <> 0 Then
                        Echo "SWITCH S701 PROGRAMMING ERROR:  Command S701." & sCall & " invalid switch closure. Check Pin arguments."
                        Err.Raise -1000
                        Exit Sub
                    End If
            End Select
            
            
            Select Case nPinB
                Case 3, 4
                    sSwitch = "3.0010"
                Case 5, 6
                    sSwitch = "3.0011"
                Case 7, 8
                    sSwitch = "3.0012"
                Case 9, 10
                    sSwitch = "3.0013"
                Case 11, 12
                    sSwitch = "3.0014"
                Case 13, 14
                    sSwitch = "3.0015"
                Case 15, 16
                    sSwitch = "3.0016"
                Case 17, 18
                    sSwitch = "3.0017"
                Case 19, 20
                    sSwitch = "3.0020"
                Case 21, 22
                    sSwitch = "3.0021"
                Case 23, 24
                    sSwitch = "3.0022"
                Case 25, 26
                    sSwitch = "3.0023"
                Case 27, 28
                    sSwitch = "3.0024"
                Case 29, 30
                    sSwitch = "3.0025"
                Case 31, 32
                    sSwitch = "3.0026"
                Case 33, 34
                    sSwitch = "3.0027"
                Case 35, 36
                    sSwitch = "3.0030"
                Case 37, 38
                    sSwitch = "3.0031"
                Case 39, 40
                    sSwitch = "3.0032"
                Case 41, 42
                    sSwitch = "3.0033"
                Case 43, 44
                    sSwitch = "3.0034"
                Case 45, 46
                    sSwitch = "3.0035"
                Case 47, 48
                    sSwitch = "3.0036"
                Case 49, 50
                    sSwitch = "3.0037"
            End Select
    End Select
    
    If Not bSimulation Then WriteSW " " & sMethod & " " & sSwitch

End Sub

