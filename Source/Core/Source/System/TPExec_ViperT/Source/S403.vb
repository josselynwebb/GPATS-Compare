Option Strict Off
Option Explicit On

<System.Runtime.InteropServices.ProgId("S403_NET.S403")> Public Class S403
	'**************************************************************
	'* Third Echelon Test Set (TETS) Software Module              *
	'*                                                            *
	'* Nomenclature   : S403 Switch Utility                       *
	'* Written By     : G. Johnson                                *
	'* Purpose        : This Clase module contains definitions    *
	'*                  for switch names                          *
	'**************************************************************

	Public Sub swClose(ByRef nPinA As Short, ByRef nPinB As Short)
		Dim sSwitch As String
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		'Set Display
		gFrmMain.txtInstrument.Text = "Switch S403"
		gFrmMain.txtCommand.Text = "swClose"
		
		'Error Check Pin Numbers
		If nPinA < 1 Or nPinA > 1 Then
			Echo("SWITCH S403 PROGRAMMING ERROR:  Command S403.swClose nPinA argument out of range.")
			Err.Raise(-1000)
			Exit Sub
		End If
		If nPinB < 2 Or nPinB > 5 Then
			Echo("SWITCH S403 PROGRAMMING ERROR:  Command S403.swClose nPinB argument out of range.")
			Err.Raise(-1000)
			Exit Sub
		End If
		If nPinA > nPinB Then
			Echo("SWITCH S403 PROGRAMMING ERROR:  Command S403.swOpen nPinA argument must be less than nPinB argument.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		Select Case nPinB
			Case 2
				sSwitch = "1.3200"
			Case 3
				sSwitch = "1.3201"
			Case 4
				sSwitch = "1.3202"
			Case 5
				sSwitch = "1.3203"
            Case Else
                sSwitch = "1.3203"
        End Select
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		If Not bSimulation Then WriteSW("CLOSE " & sSwitch)
    End Sub
	
	Public Sub swOpen(Optional ByRef nPinA As Short = 0, Optional ByRef nPinB As Short = 0)
		Dim sSwitch As String
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		'Set Display
		gFrmMain.txtInstrument.Text = "Switch S403"
		gFrmMain.txtCommand.Text = "swOpen"
		
		'Error Check Pin Numbers
		If nPinA < 1 Or nPinA > 1 Then
			Echo("SWITCH S403 PROGRAMMING ERROR:  Command S403.swOpen nPinA argument out of range.")
			Err.Raise(-1000)
			Exit Sub
		End If
		If nPinB < 2 Or nPinB > 5 Then
			Echo("SWITCH S403 PROGRAMMING ERROR:  Command S403.swOpen nPinB argument out of range.")
			Err.Raise(-1000)
			Exit Sub
		End If
		If nPinA > nPinB Then
			Echo("SWITCH S403 PROGRAMMING ERROR:  Command S403.swOpen nPinA argument must be less than nPinB argument.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		Select Case nPinB
			Case 2
				sSwitch = "1.3200"
			Case 3
				sSwitch = "1.3201"
			Case 4
				sSwitch = "1.3202"
			Case 5
				sSwitch = "1.3203"
            Case Else
                sSwitch = "1.3203"
        End Select
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		If Not bSimulation Then WriteSW("OPEN " & sSwitch)
    End Sub
	
	Public Sub swOpenAll()
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		'Set Display
		gFrmMain.txtInstrument.Text = "Switch S403"
		gFrmMain.txtCommand.Text = "swOpenAll"
		
		If Not bSimulation Then WriteSW("OPEN 1.3200-3203")
    End Sub
End Class