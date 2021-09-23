Option Strict Off
Option Explicit On

<System.Runtime.InteropServices.ProgId("S401_NET.S401")> Public Class S401
	'**************************************************************
	'* Third Echelon Test Set (TETS) Software Module              *
	'*                                                            *
	'* Nomenclature   : S401 Switch Utility                       *
	'* Written By     : G. Johnson                                *
	'* Purpose        : This Clase module contains definitions    *
	'*                  for switch names                          *
	'**************************************************************

	Public Sub swClose(ByRef nPinA As Short, ByRef nPinB As Short)
		Dim sSwitch As String
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		'Set Display
		gFrmMain.txtInstrument.Text = "Switch S401"
		gFrmMain.txtCommand.Text = "swClose"
		
		'Error Check Pin Numbers
		If nPinA < 1 Or nPinA > 1 Then
			Echo("SWITCH S401 PROGRAMMING ERROR:  Command S401.swClose nPinA argument out of range.")
			Err.Raise(-1000)
			Exit Sub
		End If
		If nPinB < 2 Or nPinB > 5 Then
			Echo("SWITCH S401 PROGRAMMING ERROR:  Command S401.swClose nPinB argument out of range.")
			Err.Raise(-1000)
			Exit Sub
		End If
		If nPinA > nPinB Then
			Echo("SWITCH S401 PROGRAMMING ERROR:  Command S401.swOpen nPinA argument must be less than nPinB argument.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		Select Case nPinB
			Case 2
				sSwitch = "1.3000"
			Case 3
				sSwitch = "1.3001"
			Case 4
				sSwitch = "1.3002"
			Case 5
				sSwitch = "1.3003"
            Case Else
                sSwitch = "1.3003"
        End Select
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		If Not bSimulation Then WriteSW("CLOSE " & sSwitch)
    End Sub
	
	Public Sub swOpen(Optional ByRef nPinA As Short = 0, Optional ByRef nPinB As Short = 0)
		Dim sSwitch As String
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		'Set Display
		gFrmMain.txtInstrument.Text = "Switch S401"
		gFrmMain.txtCommand.Text = "swOpen"
		
		'Error Check Pin Numbers
		If nPinA < 1 Or nPinA > 1 Then
			Echo("SWITCH S401 PROGRAMMING ERROR:  Command S401.swOpen nPinA argument out of range.")
			Err.Raise(-1000)
			Exit Sub
		End If
		If nPinB < 2 Or nPinB > 5 Then
			Echo("SWITCH S401 PROGRAMMING ERROR:  Command S401.swOpen nPinB argument out of range.")
			Err.Raise(-1000)
			Exit Sub
		End If
		If nPinA > nPinB Then
			Echo("SWITCH S401 PROGRAMMING ERROR:  Command S401.swOpen nPinA argument must be less than nPinB argument.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		Select Case nPinB
			Case 2
				sSwitch = "1.3000"
			Case 3
				sSwitch = "1.3001"
			Case 4
				sSwitch = "1.3002"
			Case 5
				sSwitch = "1.3003"
            Case Else
                sSwitch = "1.3003"
        End Select
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		If Not bSimulation Then WriteSW("OPEN " & sSwitch)
    End Sub
	
	Public Sub swOpenAll()
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		'Set Display
		gFrmMain.txtInstrument.Text = "Switch S401"
		gFrmMain.txtCommand.Text = "swOpenAll"
		
		If Not bSimulation Then WriteSW("OPEN 1.3000-3003")
    End Sub
End Class