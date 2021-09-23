Option Strict Off
Option Explicit On

<System.Runtime.InteropServices.ProgId("S508_NET.S508")> Public Class S508
    '**************************************************************
    '* Third Echelon Test Set (TETS) Software Module              *
    '*                                                            *
    '* Nomenclature   : S508 Switch Utility                       *
    '* Written By     : G. Johnson                                *
    '* Purpose        : This Clase module contains definitions    *
    '*                  for switch names                          *
    '**************************************************************

    Public Sub swClose(ByRef nPinA As Short, ByRef nPinB As Short)
        Dim sSwitch As String

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        'Set Display
        gFrmMain.txtInstrument.Text = "Switch S508"
        gFrmMain.txtCommand.Text = "swClose"

        'Error Check Pin Numbers
        If nPinA < 1 Or nPinA > 2 Then
            Echo("SWITCH S508 PROGRAMMING ERROR:  Command S508.swClose nPinA argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If
        If nPinB < 3 Or nPinB > 10 Then
            Echo("SWITCH S508 PROGRAMMING ERROR:  Command S508.swClose nPinB argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If

        If nPinA = 1 Then
            sSwitch = "2.420"
        Else
            sSwitch = "2.421"
        End If

        sSwitch = sSwitch & CStr(nPinB - 3)

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        If Not bSimulation Then WriteSW("CLOSE " & sSwitch)
    End Sub

    Public Sub swOpen(Optional ByRef nPinA As Short = 0, Optional ByRef nPinB As Short = 0)
        Dim sSwitch As String

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        'Set Display
        gFrmMain.txtInstrument.Text = "Switch S508"
        gFrmMain.txtCommand.Text = "swOpen"

        'Error Check Pin Numbers
        If nPinA < 1 Or nPinA > 2 Then
            Echo("SWITCH S508 PROGRAMMING ERROR:  Command S508.swOpen nPinA argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If
        If nPinB < 3 Or nPinB > 10 Then
            Echo("SWITCH S508 PROGRAMMING ERROR:  Command S508.swOpen nPinB argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If

        If nPinA = 1 Then
            sSwitch = "2.420"
        Else
            sSwitch = "2.421"
        End If

        sSwitch = sSwitch & CStr(nPinB - 3)

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        If Not bSimulation Then WriteSW("OPEN " & sSwitch)
    End Sub

    Public Sub swOpenAll()

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        'Set Display
        gFrmMain.txtInstrument.Text = "Switch S508"
        gFrmMain.txtCommand.Text = "swOpenAll"

        If Not bSimulation Then WriteSW("OPEN 2.4200-4217")
    End Sub
End Class