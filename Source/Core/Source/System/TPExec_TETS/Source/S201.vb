Option Strict Off
Option Explicit On

<System.Runtime.InteropServices.ProgId("S201_NET.S201")> Public Class S201
    '**************************************************************
    '* Third Echelon Test Set (TETS) Software Module              *
    '*                                                            *
    '* Nomenclature   : S201 Switch Utility                       *
    '* Written By     : G. Johnson                                *
    '* Purpose        : This Clase module contains definitions    *
    '*                  for switch names                          *
    '**************************************************************

    Public Sub swClose(ByRef nPinA As Short, ByRef nPinB As Short)
        Dim sSwitch As String

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        'Set Display
        gFrmMain.txtInstrument.Text = "Switch S201"
        gFrmMain.txtCommand.Text = "swClose"

        'Error Check Pin Numbers
        If nPinA < 1 Or nPinA > 4 Then
            Echo("SWITCH S201 PROGRAMMING ERROR:  Command S201.swClose nPinA argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If

        If nPinB < 5 Or nPinB > 52 Then
            Echo("SWITCH S201 PROGRAMMING ERROR:  Command S201.swClose nPinB argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If

        Select Case nPinA
            Case 1, 3
                If (nPinB Mod 2) = 0 Then
                    Echo("SWITCH S201 PROGRAMMING ERROR:  Command S201.swClose invalid switch closure. Check Pin arguments.")
                    Err.Raise(-1000)
                    Exit Sub
                End If
            Case Else
                If (nPinB Mod 2) <> 0 Then
                    Echo("SWITCH S201 PROGRAMMING ERROR:  Command S201.swClose invalid switch closure. Check Pin arguments.")
                    Err.Raise(-1000)
                    Exit Sub
                End If
        End Select

        Select Case nPinB
            Case 5, 6
                If nPinA < 3 Then sSwitch = "3.0040" Else sSwitch = "3.0050"
            Case 7, 8
                If nPinA < 3 Then sSwitch = "3.0041" Else sSwitch = "3.0051"
            Case 9, 10
                If nPinA < 3 Then sSwitch = "3.0042" Else sSwitch = "3.0052"
            Case 11, 12
                If nPinA < 3 Then sSwitch = "3.0043" Else sSwitch = "3.0053"
            Case 13, 14
                If nPinA < 3 Then sSwitch = "3.0044" Else sSwitch = "3.0054"
            Case 15, 16
                If nPinA < 3 Then sSwitch = "3.0045" Else sSwitch = "3.0055"
            Case 17, 18
                If nPinA < 3 Then sSwitch = "3.0046" Else sSwitch = "3.0056"
            Case 19, 20
                If nPinA < 3 Then sSwitch = "3.0047" Else sSwitch = "3.0057"
            Case 21, 22
                If nPinA < 3 Then sSwitch = "3.0060" Else sSwitch = "3.0070"
            Case 23, 24
                If nPinA < 3 Then sSwitch = "3.0061" Else sSwitch = "3.0071"
            Case 25, 26
                If nPinA < 3 Then sSwitch = "3.0062" Else sSwitch = "3.0072"
            Case 27, 28
                If nPinA < 3 Then sSwitch = "3.0063" Else sSwitch = "3.0073"
            Case 29, 30
                If nPinA < 3 Then sSwitch = "3.0064" Else sSwitch = "3.0074"
            Case 31, 32
                If nPinA < 3 Then sSwitch = "3.0065" Else sSwitch = "3.0075"
            Case 33, 34
                If nPinA < 3 Then sSwitch = "3.0066" Else sSwitch = "3.0076"
            Case 35, 36
                If nPinA < 3 Then sSwitch = "3.0067" Else sSwitch = "3.0077"
            Case 37, 38
                If nPinA < 3 Then sSwitch = "3.0080" Else sSwitch = "3.0090"
            Case 39, 40
                If nPinA < 3 Then sSwitch = "3.0081" Else sSwitch = "3.0091"
            Case 41, 42
                If nPinA < 3 Then sSwitch = "3.0082" Else sSwitch = "3.0092"
            Case 43, 44
                If nPinA < 3 Then sSwitch = "3.0083" Else sSwitch = "3.0093"
            Case 45, 46
                If nPinA < 3 Then sSwitch = "3.0084" Else sSwitch = "3.0094"
            Case 47, 48
                If nPinA < 3 Then sSwitch = "3.0085" Else sSwitch = "3.0095"
            Case 49, 50
                If nPinA < 3 Then sSwitch = "3.0086" Else sSwitch = "3.0096"
            Case 51, 52
                If nPinA < 3 Then sSwitch = "3.0087" Else sSwitch = "3.0097"
            Case Else
                If nPinA < 3 Then sSwitch = "3.0087" Else sSwitch = "3.0097"
        End Select

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        'Set the necessary -38 interconnect relays
        If Not bSimulation Then WriteSW("CLOSE 3.1001,2002,3000,3001,4000,4001,6000,6001,7000,7001")
        If Not bSimulation Then WriteSW("CLOSE " & sSwitch)
    End Sub

    Public Sub swOpen(Optional ByRef nPinA As Short = 0, Optional ByRef nPinB As Short = 0)
        Dim sSwitch As String

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        'Set Display
        gFrmMain.txtInstrument.Text = "Switch S201"
        gFrmMain.txtCommand.Text = "swOpen"

        'Error Check Pin Numbers
        If nPinA < 1 Or nPinA > 4 Then
            Echo("SWITCH S201 PROGRAMMING ERROR:  Command S201.swOpen nPinA argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If

        If nPinB < 5 Or nPinB > 52 Then
            Echo("SWITCH S201 PROGRAMMING ERROR:  Command S201.swOpen nPinB argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If

        Select Case nPinA
            Case 1, 3
                If (nPinB Mod 2) = 0 Then
                    Echo("SWITCH S201 PROGRAMMING ERROR:  Command S201.swOpen invalid switch closure. Check Pin arguments.")
                    Err.Raise(-1000)
                    Exit Sub
                End If
            Case Else
                If (nPinB Mod 2) <> 0 Then
                    Echo("SWITCH S201 PROGRAMMING ERROR:  Command S201.swOpen invalid switch closure. Check Pin arguments.")
                    Err.Raise(-1000)
                    Exit Sub
                End If
        End Select

        Select Case nPinB
            Case 5, 6
                If nPinA < 3 Then sSwitch = "3.0040" Else sSwitch = "3.0050"
            Case 7, 8
                If nPinA < 3 Then sSwitch = "3.0041" Else sSwitch = "3.0051"
            Case 9, 10
                If nPinA < 3 Then sSwitch = "3.0042" Else sSwitch = "3.0052"
            Case 11, 12
                If nPinA < 3 Then sSwitch = "3.0043" Else sSwitch = "3.0053"
            Case 13, 14
                If nPinA < 3 Then sSwitch = "3.0044" Else sSwitch = "3.0054"
            Case 15, 16
                If nPinA < 3 Then sSwitch = "3.0045" Else sSwitch = "3.0055"
            Case 17, 18
                If nPinA < 3 Then sSwitch = "3.0046" Else sSwitch = "3.0056"
            Case 19, 20
                If nPinA < 3 Then sSwitch = "3.0047" Else sSwitch = "3.0057"
            Case 21, 22
                If nPinA < 3 Then sSwitch = "3.0060" Else sSwitch = "3.0070"
            Case 23, 24
                If nPinA < 3 Then sSwitch = "3.0061" Else sSwitch = "3.0071"
            Case 25, 26
                If nPinA < 3 Then sSwitch = "3.0062" Else sSwitch = "3.0072"
            Case 27, 28
                If nPinA < 3 Then sSwitch = "3.0063" Else sSwitch = "3.0073"
            Case 29, 30
                If nPinA < 3 Then sSwitch = "3.0064" Else sSwitch = "3.0074"
            Case 31, 32
                If nPinA < 3 Then sSwitch = "3.0065" Else sSwitch = "3.0075"
            Case 33, 34
                If nPinA < 3 Then sSwitch = "3.0066" Else sSwitch = "3.0076"
            Case 35, 36
                If nPinA < 3 Then sSwitch = "3.0067" Else sSwitch = "3.0077"
            Case 37, 38
                If nPinA < 3 Then sSwitch = "3.0080" Else sSwitch = "3.0090"
            Case 39, 40
                If nPinA < 3 Then sSwitch = "3.0081" Else sSwitch = "3.0091"
            Case 41, 42
                If nPinA < 3 Then sSwitch = "3.0082" Else sSwitch = "3.0092"
            Case 43, 44
                If nPinA < 3 Then sSwitch = "3.0083" Else sSwitch = "3.0093"
            Case 45, 46
                If nPinA < 3 Then sSwitch = "3.0084" Else sSwitch = "3.0094"
            Case 47, 48
                If nPinA < 3 Then sSwitch = "3.0085" Else sSwitch = "3.0095"
            Case 49, 50
                If nPinA < 3 Then sSwitch = "3.0086" Else sSwitch = "3.0096"
            Case 51, 52
                If nPinA < 3 Then sSwitch = "3.0087" Else sSwitch = "3.0097"
            Case Else
                If nPinA < 3 Then sSwitch = "3.0087" Else sSwitch = "3.0097"
        End Select

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        If Not bSimulation Then WriteSW("CLOSE 3.1001,2002,3000,3001,4000,4001,6000,6001,7000,7001")
        If Not bSimulation Then WriteSW("OPEN " & sSwitch)
    End Sub

    Public Sub swOpenAll()

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        'Set Display
        gFrmMain.txtInstrument.Text = "Switch S201"
        gFrmMain.txtCommand.Text = "swOpenAll"

        If Not bSimulation Then WriteSW("CLOSE 3.1001,2002,3000,3001,4000,4001,6000,6001,7000,7001")
        If Not bSimulation Then WriteSW("OPEN 3.0040-0097")
    End Sub
End Class