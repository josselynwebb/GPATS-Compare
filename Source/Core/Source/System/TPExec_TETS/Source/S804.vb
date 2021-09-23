Option Strict Off
Option Explicit On
<System.Runtime.InteropServices.ProgId("S804_NET.S804")> Public Class S804
    '**************************************************************
    '* Third Echelon Test Set (TETS) Software Module              *
    '*                                                            *
    '* Nomenclature   : S804 Switch Utility                       *
    '* Written By     : G. Johnson                                *
    '* Purpose        : This Clase module contains definitions    *
    '*                  for switch names                          *
    '**************************************************************

    Public Sub swClose(ByRef nPinA As Short, ByRef nPinB As Short)
        Dim sSwitch As String

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        'Set Display
        gFrmMain.txtInstrument.Text = "Switch S804"
        gFrmMain.txtCommand.Text = "swClose"

        'Error Check Pin Numbers
        If nPinA < 1 Or nPinA > 1 Then
            Echo("SWITCH S804 PROGRAMMING ERROR:  Command S804.swClose nPinA argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If
        If nPinB < 2 Or nPinB > 9 Then
            Echo("SWITCH S804 PROGRAMMING ERROR:  Command S804.swClose nPinB argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If

        sSwitch = "4.3" & CStr(nPinB - 2)

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        If Not bSimulation Then WriteSW("CLOSE " & sSwitch)
    End Sub

    Public Sub swOpen()
        Dim sSwitch As String

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        'Set Display
        gFrmMain.txtInstrument.Text = "Switch S804"
        gFrmMain.txtCommand.Text = "swOpen"

        sSwitch = "4.30-37"

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        If Not bSimulation Then WriteSW("OPEN " & sSwitch)
    End Sub
End Class