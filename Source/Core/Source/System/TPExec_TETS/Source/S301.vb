Option Strict Off
Option Explicit On

<System.Runtime.InteropServices.ProgId("S301_NET.S301")> Public Class S301
    '**************************************************************
    '* Third Echelon Test Set (TETS) Software Module              *
    '*                                                            *
    '* Nomenclature   : S301 Switch Utility                       *
    '* Written By     : G. Johnson                                *
    '* Purpose        : This Clase module contains definitions    *
    '*                  for switch names                          *
    '**************************************************************

    Public Sub swClose(ByRef nPinA As Short, ByRef nPinB As Short)
        Dim sSwitch As String

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        'Set Display
        gFrmMain.txtInstrument.Text = "Switch S301"
        gFrmMain.txtCommand.Text = "swClose"

        'Error Check Pin Numbers
        If nPinA < 1 Or nPinA > 191 Then
            Echo("SWITCH S301 PROGRAMMING ERROR:  Command S301.swClose nPinA argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If
        If nPinB < 2 Or nPinB > 192 Then
            Echo("SWITCH S301 PROGRAMMING ERROR:  Command S301.swClose nPinB argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If
        If nPinA > nPinB Then
            Echo("SWITCH S301 PROGRAMMING ERROR:  Command S301.swOpen nPinA argument must be less than nPinB argument.")
            Err.Raise(-1000)
            Exit Sub
        End If
        If (nPinA Mod 2) = 0 Then
            Echo("SWITCH S301 PROGRAMMING ERROR:  Command S301.swClose nPinA argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If
        If (nPinB Mod 2) <> 0 Then
            Echo("SWITCH S301 PROGRAMMING ERROR:  Command S301.swClose nPinB argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If
        If (nPinA + 1) <> nPinB Then
            Echo("SWITCH S301 PROGRAMMING ERROR:  Command S301.swClose nPinA argument must be adjacent nPinB argument.")
            Err.Raise(-1000)
            Exit Sub
        End If

        Select Case nPinB
            Case 2 To 96
                sSwitch = "1." & CStr((nPinB / 2) + 999) ' 1.1000, 1.1001,,,1.1047
            Case 98 To 192
                sSwitch = "2." & CStr(((nPinB - 98) / 2) + 1000) ' 2.1000, 2.1001,,,2.1047
            Case Else
                sSwitch = "2." & CStr(((nPinB - 98) / 2) + 1000) ' 2.1000, 2.1001,,,2.1047
        End Select

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        If Not bSimulation Then WriteSW("CLOSE " & sSwitch)
    End Sub

    Public Sub swOpen(Optional ByRef nPinA As Short = 0, Optional ByRef nPinB As Short = 0)
        Dim sSwitch As String

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        'Set Display
        gFrmMain.txtInstrument.Text = "Switch S301"
        gFrmMain.txtCommand.Text = "swOpen"

        'Error Check Pin Numbers
        If nPinA < 1 Or nPinA > 191 Then
            Echo("SWITCH S301 PROGRAMMING ERROR:  Command S301.swOpen nPinA argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If
        If nPinB < 2 Or nPinB > 192 Then
            Echo("SWITCH S301 PROGRAMMING ERROR:  Command S301.swOpen nPinB argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If
        If nPinA > nPinB Then
            Echo("SWITCH S301 PROGRAMMING ERROR:  Command S301.swOpen nPinA argument must be less than nPinB argument.")
            Err.Raise(-1000)
            Exit Sub
        End If
        If (nPinA Mod 2) = 0 Then
            Echo("SWITCH S301 PROGRAMMING ERROR:  Command S301.swOpen nPinA argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If
        If (nPinB Mod 2) <> 0 Then
            Echo("SWITCH S301 PROGRAMMING ERROR:  Command S301.swOpen nPinB argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If
        If (nPinA + 1) <> nPinB Then
            Echo("SWITCH S301 PROGRAMMING ERROR:  Command S301.swOpen nPinA argument must be adjacent nPinB argument.")
            Err.Raise(-1000)
            Exit Sub
        End If

        Select Case nPinB
            Case 2 To 96
                sSwitch = "1." & CStr((nPinB / 2) + 999)
            Case 98 To 192
                sSwitch = "2." & CStr(((nPinB - 98) / 2) + 1000)
            Case Else
                sSwitch = "2." & CStr(((nPinB - 98) / 2) + 1000)
        End Select

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        If Not bSimulation Then WriteSW("OPEN " & sSwitch)
    End Sub

    Public Sub swOpenAll()

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        'Set Display
        gFrmMain.txtInstrument.Text = "Switch S301"
        gFrmMain.txtCommand.Text = "swOpenAll"

        If Not bSimulation Then WriteSW("OPEN 1.1000-1047;2.1000-1047")
    End Sub
End Class