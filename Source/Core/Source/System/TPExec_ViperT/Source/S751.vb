Option Strict Off
Option Explicit On
<System.Runtime.InteropServices.ProgId("S751_NET.S751")> Public Class S751
	'**************************************************************
	'* Third Echelon Test Set (TETS) Software Module              *
	'*                                                            *
	'* Nomenclature   : S751 Switch Utility                       *
	'* Written By     : G. Johnson                                *
	'* Purpose        : This Clase module contains definitions    *
	'*                  for switch names                          *
	'**************************************************************

	Public Sub swClose(ByRef nPinA As Short, ByRef nPinB As Short)
		Dim sSwitch As String
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		'Set Display
		gFrmMain.txtInstrument.Text = "Switch S751"
		gFrmMain.txtCommand.Text = "swClose"
		
		'Error Check Pin Numbers
		If nPinA < 1 Or nPinA > 2 Then
			Echo("SWITCH S751 PROGRAMMING ERROR:  Command S751.swClose nPinA argument out of range.")
			Err.Raise(-1000)
			Exit Sub
		End If
		If nPinB < 3 Or nPinB > 18 Then
			Echo("SWITCH S751 PROGRAMMING ERROR:  Command S751.swClose nPinB argument out of range.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		Select Case nPinA
			Case 1
				If (nPinB Mod 2) = 0 Then
					Echo("SWITCH S751 PROGRAMMING ERROR:  Command S751.swClose invalid switch closure. Check Pin arguments.")
					Err.Raise(-1000)
					Exit Sub
				End If
			Case Else
				If (nPinB Mod 2) <> 0 Then
					Echo("SWITCH S751 PROGRAMMING ERROR:  Command S751.swClose invalid switch closure. Check Pin arguments.")
					Err.Raise(-1000)
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
            Case Else
                sSwitch = "3.0007"
        End Select
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		If Not bSimulation Then WriteSW("CLOSE " & sSwitch)
		
	End Sub
	
	Public Sub swOpen(Optional ByRef nPinA As Short = 0, Optional ByRef nPinB As Short = 0)
		Dim sSwitch As String
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		'Set Display
		gFrmMain.txtInstrument.Text = "Switch S751"
		gFrmMain.txtCommand.Text = "swOpen"
		
		'Error Check Pin Numbers
		If nPinA < 1 Or nPinA > 2 Then
			Echo("SWITCH S751 PROGRAMMING ERROR:  Command S751.swOpen nPinA argument out of range.")
			Err.Raise(-1000)
			Exit Sub
		End If
		If nPinB < 3 Or nPinB > 18 Then
			Echo("SWITCH S751 PROGRAMMING ERROR:  Command S751.swOpen nPinB argument out of range.")
			Err.Raise(-1000)
			Exit Sub
		End If
		
		Select Case nPinA
			Case 1
				If (nPinB Mod 2) = 0 Then
					Echo("SWITCH S751 PROGRAMMING ERROR:  Command S751.swOpen invalid switch closure. Check Pin arguments.")
					Err.Raise(-1000)
					Exit Sub
				End If
			Case Else
				If (nPinB Mod 2) <> 0 Then
					Echo("SWITCH S751 PROGRAMMING ERROR:  Command S751.swOpen invalid switch closure. Check Pin arguments.")
					Err.Raise(-1000)
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
            Case Else
                sSwitch = "3.0007"
        End Select
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		If Not bSimulation Then WriteSW("OPEN " & sSwitch)
	End Sub
	
	Public Sub swOpenAll()
		
		If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
		'Set Display
		gFrmMain.txtInstrument.Text = "Switch S751"
		gFrmMain.txtCommand.Text = "swOpenAll"
		
		If Not bSimulation Then WriteSW("OPEN 3.0001-0007")
		
	End Sub
End Class