Option Strict Off
Option Explicit On

<System.Runtime.InteropServices.ProgId("S101_NET.S101")> Public Class S101
	'**************************************************************
	'* TETS Software Module                                       *
	'*                                                            *
	'* Nomenclature   : S101 Switch Utility                       *
	'* Written By     : G. Johnson                                *
	'* Purpose        : This Clase module contains definitions    *
	'*                  for switch names                          *
	'**************************************************************

	Public Sub swClose(ByRef nPinA As Short, ByRef nPinB As Short)
		Dim sSwitch As String
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		'Set Display
		gFrmMain.txtInstrument.Text = "Switch S101"
		gFrmMain.txtCommand.Text = "swClose"
		
		'Error Check Pin Numbers
		If nPinA > 40 Or nPinB < 1 Then
			Echo("SWITCH S101 PROGRAMMING ERROR:  Command S101.swClose nPinA argument out of range.")
			Err.Raise(-1000)
			Exit Sub
		End If
		If nPinB > 40 Or nPinB < 1 Then
			Echo("SWITCH S101 PROGRAMMING ERROR:  Command S101.swClose nPinB argument out of range.")
			Err.Raise(-1000)
			Exit Sub
		End If
		If nPinA > nPinB Then
			Echo("SWITCH S101 PROGRAMMING ERROR:  Command S101.swClose nPinA argument must be less than nPinB argument.")
			Err.Raise(-1000)
			Exit Sub
		End If
		If (nPinA + 1) <> nPinB Then
			Echo("SWITCH S101 PROGRAMMING ERROR:  Command S101.swClose invalid switch closure. Check Pin arguments.")
			Err.Raise(-1000)
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
				Echo("SWITCH S101 PROGRAMMING ERROR:  Command S101.swClose invalid switch closure. Check Pin arguments.")
				Err.Raise(-1000)
				Exit Sub
		End Select
		
		If Not bSimulation Then WriteSW("CLOSE " & sSwitch)
	End Sub
	
	Public Sub swOpen(Optional ByRef nPinA As Short = 0, Optional ByRef nPinB As Short = 0)
		Dim sSwitch As String
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		'Set Display
		gFrmMain.txtInstrument.Text = "Switch S101"
		gFrmMain.txtCommand.Text = "swOpen"
		
		'Error Check Pin Numbers
		If nPinA > 39 Or nPinA < 1 Then ' bbbb was >40
			Echo("SWITCH S101 PROGRAMMING ERROR:  Command S101.swOpen nPinA argument out of range.")
			Err.Raise(-1000)
			Exit Sub
		End If
		If nPinB > 40 Or nPinB < 2 Then ' bbbb was >1
			Echo("SWITCH S101 PROGRAMMING ERROR:  Command S101.swOpen nPinB argument out of range.")
			Err.Raise(-1000)
			Exit Sub
		End If
		If nPinA > nPinB Then
			Echo("SWITCH S101 PROGRAMMING ERROR:  Command S101.swOpen nPinA argument must be less than nPinB argument.")
			Err.Raise(-1000)
			Exit Sub
		End If
		If (nPinA + 1) <> nPinB Then
			Echo("SWITCH S101 PROGRAMMING ERROR:  Command S101.swOpen invalid switch closure. Check Pin arguments.")
			Err.Raise(-1000)
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
				Echo("SWITCH S101 PROGRAMMING ERROR:  Command S101.swClose invalid switch closure. Check Pin arguments.")
				Err.Raise(-1000)
				Exit Sub
		End Select
		If Not bSimulation Then WriteSW("OPEN " & sSwitch)
    End Sub
	
	Public Sub swOpenAll()
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		'Set Display
		gFrmMain.txtInstrument.Text = "Switch S101"
		gFrmMain.txtCommand.Text = "swOpenAll"
		
		If Not bSimulation Then WriteSW("OPEN 1.0000-0004;2.0000-0004")
    End Sub
End Class