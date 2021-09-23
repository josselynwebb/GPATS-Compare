Option Strict Off
Option Explicit On
<System.Runtime.InteropServices.ProgId("S905_NET.S905")> Public Class S905
	'**************************************************************
	'* Third Echelon Test Set (TETS) Software Module              *
	'*                                                            *
	'* Nomenclature   : S905 Switch Utility                       *
	'* Written By     : G. Johnson                                *
	'* Purpose        : This Clase module contains definitions    *
	'*                  for switch names                          *
	'**************************************************************

	Public Sub swClose(ByRef nPinA As Short, ByRef nPinB As Short)
		Dim sSwitch As String
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		'Set Display
		gFrmMain.txtInstrument.Text = "Switch S905"
		gFrmMain.txtCommand.Text = "swClose"
		
		'Error Check Pin Numbers
		If nPinA < 1 Or nPinA > 1 Then
			Echo("SWITCH S905 PROGRAMMING ERROR:  Command S905.swClose nPinA argument out of range.")
			Err.Raise(-1000)
			Exit Sub
		End If
		If nPinB < 2 Or nPinB > 7 Then ' bbbb was > 9
			Echo("SWITCH S905 PROGRAMMING ERROR:  Command S905.swClose nPinB argument out of range.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		sSwitch = "5.4" & CStr(nPinB - 2)
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		If Not bSimulation Then WriteSW("CLOSE " & sSwitch)
		
	End Sub
	
	Public Sub swOpen()
		Dim sSwitch As String
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		'Set Display
		gFrmMain.txtInstrument.Text = "Switch S905"
		gFrmMain.txtCommand.Text = "swOpen"
		
		sSwitch = "5.40-45"
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		If Not bSimulation Then WriteSW("OPEN " & sSwitch)
	End Sub
End Class