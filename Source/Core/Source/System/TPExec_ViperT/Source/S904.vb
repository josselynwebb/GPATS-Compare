Option Strict Off
Option Explicit On
<System.Runtime.InteropServices.ProgId("S904_NET.S904")> Public Class S904
	'**************************************************************
	'* Third Echelon Test Set (TETS) Software Module              *
	'*                                                            *
	'* Nomenclature   : S904 Switch Utility                       *
	'* Written By     : G. Johnson                                *
	'* Purpose        : This Clase module contains definitions    *
	'*                  for switch names                          *
	'**************************************************************

	Public Sub swClose(ByRef nPinA As Short, ByRef nPinB As Short)
		Dim sSwitch As String
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		'Set Display
		gFrmMain.txtInstrument.Text = "Switch S904"
		gFrmMain.txtCommand.Text = "swClose"
		
		'Error Check Pin Numbers
		If nPinA < 1 Or nPinA > 1 Then
			Echo("SWITCH S904 PROGRAMMING ERROR:  Command S904.swClose nPinA argument out of range.")
			Err.Raise(-1000)
			Exit Sub
		End If
		If nPinB < 2 Or nPinB > 7 Then ' bbbb was > 9
			Echo("SWITCH S904 PROGRAMMING ERROR:  Command S904.swClose nPinB argument out of range.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		sSwitch = "5.3" & CStr(nPinB - 2)
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		If Not bSimulation Then WriteSW("CLOSE " & sSwitch)
		
	End Sub
	
	Public Sub swOpen()
		Dim sSwitch As String
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		'Set Display
		gFrmMain.txtInstrument.Text = "Switch S904"
		gFrmMain.txtCommand.Text = "swOpen"
		
		sSwitch = "5.30-35"
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		If Not bSimulation Then WriteSW("OPEN " & sSwitch)
	End Sub
End Class