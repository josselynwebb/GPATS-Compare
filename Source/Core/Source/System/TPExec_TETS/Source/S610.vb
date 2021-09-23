Option Strict Off
Option Explicit On

<System.Runtime.InteropServices.ProgId("S610_NET.S610")> Public Class S610
    '**************************************************************
    '* Third Echelon Test Set (TETS) Software Module              *
    '*                                                            *
    '* Nomenclature   : S610 Switch Utility                       *
    '* Written By     : G. Johnson                                *
    '* Purpose        : This Clase module contains definitions    *
    '*                  for switch names                          *
    '**************************************************************

    Public Sub swClose(ByRef nPinA As Short, ByRef nPinB As Short)
        Dim sSwitch As String

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        'Set Display
        gFrmMain.txtInstrument.Text = "Switch S610"
        gFrmMain.txtCommand.Text = "swClose"

        'Error Check Pin Numbers
        If nPinA < 1 Or nPinA > 1 Then
            Echo("SWITCH S610 PROGRAMMING ERROR:  Command S610.swClose nPinA argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If
        If nPinB < 2 Or nPinB > 3 Then
            Echo("SWITCH S610 PROGRAMMING ERROR:  Command S610.swClose nPinB argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If

        sSwitch = "2.230"

        sSwitch = sSwitch & CStr(nPinB - 2)

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        If Not bSimulation Then WriteSW("CLOSE " & sSwitch)
    End Sub

    Public Sub swOpen(Optional ByRef nPinA As Short = 0, Optional ByRef nPinB As Short = 0)
        Dim sSwitch As String

        'Error Check Pin Numbers
        If nPinA < 1 Or nPinA > 1 Then
            Echo("SWITCH S610 PROGRAMMING ERROR:  Command S610.swOpen nPinA argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If
        If nPinB < 2 Or nPinB > 3 Then
            Echo("SWITCH S610 PROGRAMMING ERROR:  Command S610.swOpen nPinB argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If

        sSwitch = "2.230"

        sSwitch = sSwitch & CStr(nPinB - 2)

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        If Not bSimulation Then WriteSW("OPEN " & sSwitch)
    End Sub

    Public Sub swOpenAll()

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        'Set Display
        gFrmMain.txtInstrument.Text = "Switch S610"
        gFrmMain.txtCommand.Text = "swOpenAll"

        If Not bSimulation Then WriteSW("OPEN 2.2300-2301")
    End Sub
End Class