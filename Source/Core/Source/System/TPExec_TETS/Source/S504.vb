Option Strict Off
Option Explicit On

<System.Runtime.InteropServices.ProgId("S504_NET.S504")> Public Class S504
    '**************************************************************
    '* Third Echelon Test Set (TETS) Software Module              *
    '*                                                            *
    '* Nomenclature   : S504 Switch Utility                       *
    '* Written By     : G. Johnson                                *
    '* Purpose        : This Clase module contains definitions    *
    '*                  for switch names                          *
    '**************************************************************

    Public Sub swClose(ByRef nPinA As Short, ByRef nPinB As Short)
        Dim sSwitch As String

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        'Set Display
        gFrmMain.txtInstrument.Text = "Switch S504"
        gFrmMain.txtCommand.Text = "swClose"

        'Error Check Pin Numbers
        If nPinA < 1 Or nPinA > 2 Then
            Echo("SWITCH S504 PROGRAMMING ERROR:  Command S504.swClose nPinA argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If
        If nPinB < 3 Or nPinB > 10 Then
            Echo("SWITCH S504 PROGRAMMING ERROR:  Command S504.swClose nPinB argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If

        If nPinA = 1 Then
            sSwitch = "1.430"
        Else
            sSwitch = "1.431"
        End If

        sSwitch = sSwitch & CStr(nPinB - 3)

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        If Not bSimulation Then WriteSW("CLOSE " & sSwitch)
    End Sub

    Public Sub swOpen(Optional ByRef nPinA As Short = 0, Optional ByRef nPinB As Short = 0)
        Dim sSwitch As String

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        'Set Display
        gFrmMain.txtInstrument.Text = "Switch S504"
        gFrmMain.txtCommand.Text = "swOpen"

        'Error Check Pin Numbers
        If nPinA < 1 Or nPinA > 2 Then
            Echo("SWITCH S504 PROGRAMMING ERROR:  Command S504.swOpen nPinA argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If
        If nPinB < 3 Or nPinB > 10 Then
            Echo("SWITCH S504 PROGRAMMING ERROR:  Command S504.swOpen nPinB argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If

        If nPinA = 1 Then
            sSwitch = "1.430"
        Else
            sSwitch = "1.431"
        End If

        sSwitch = sSwitch & CStr(nPinB - 3)

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        If Not bSimulation Then WriteSW("OPEN " & sSwitch)
    End Sub

    Public Sub swOpenAll()

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        'Set Display
        gFrmMain.txtInstrument.Text = "Switch S504"
        gFrmMain.txtCommand.Text = "swOpenAll"

        If Not bSimulation Then WriteSW("OPEN 1.4300-4317")
    End Sub
End Class