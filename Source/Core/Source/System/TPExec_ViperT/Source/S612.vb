Option Strict Off
Option Explicit On

<System.Runtime.InteropServices.ProgId("S612_NET.S612")> Public Class S612
	'**************************************************************
	'* Third Echelon Test Set (TETS) Software Module              *
	'*                                                            *
	'* Nomenclature   : S612 Switch Utility                       *
	'* Written By     : G. Johnson                                *
	'* Purpose        : This Clase module contains definitions    *
	'*                  for switch names                          *
	'**************************************************************

	Public Sub swClose(ByRef nPinA As Short, ByRef nPinB As Short)
		Dim sSwitch As String
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		'Set Display
		gFrmMain.txtInstrument.Text = "Switch S612"
		gFrmMain.txtCommand.Text = "swClose"
		
		'Error Check Pin Numbers
		If nPinA < 1 Or nPinA > 1 Then
			Echo("SWITCH S612 PROGRAMMING ERROR:  Command S612.swClose nPinA argument out of range.")
			Err.Raise(-1000)
			Exit Sub
		End If
		If nPinB < 2 Or nPinB > 3 Then
			Echo("SWITCH S612 PROGRAMMING ERROR:  Command S612.swClose nPinB argument out of range.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		sSwitch = "2.250"
		
		sSwitch = sSwitch & CStr(nPinB - 2)
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		If Not bSimulation Then WriteSW("CLOSE " & sSwitch)
    End Sub
	
	Public Sub swOpen(Optional ByRef nPinA As Short = 0, Optional ByRef nPinB As Short = 0)
		Dim sSwitch As String
		
		'Error Check Pin Numbers
		If nPinA < 1 Or nPinA > 1 Then
			Echo("SWITCH S612 PROGRAMMING ERROR:  Command S612.swOpen nPinA argument out of range.")
			Err.Raise(-1000)
			Exit Sub
		End If
		If nPinB < 2 Or nPinB > 3 Then
			Echo("SWITCH S612 PROGRAMMING ERROR:  Command S612.swOpen nPinB argument out of range.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		sSwitch = "2.250"
		
		sSwitch = sSwitch & CStr(nPinB - 2)
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		If Not bSimulation Then WriteSW("OPEN " & sSwitch)
    End Sub
	
	Public Sub swOpenAll()
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		'Set Display
		gFrmMain.txtInstrument.Text = "Switch S612"
		gFrmMain.txtCommand.Text = "swOpenAll"
		
		If Not bSimulation Then WriteSW("OPEN 2.2500-2501")
    End Sub
End Class