Option Strict Off
Option Explicit On
<System.Runtime.InteropServices.ProgId("S802_NET.S802")> Public Class S802
    '**************************************************************
    '* Third Echelon Test Set (TETS) Software Module              *
    '*                                                            *
    '* Nomenclature   : S802 Switch Utility                       *
    '* Written By     : G. Johnson                                *
    '* Purpose        : This Clase module contains definitions    *
    '*                  for switch names                          *
    '**************************************************************

    Public Sub swClose(ByRef nPinA As Short, ByRef nPinB As Short)
        Dim sSwitch As String

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        'Set Display
        gFrmMain.txtInstrument.Text = "Switch S802"
        gFrmMain.txtCommand.Text = "swClose"

        'Error Check Pin Numbers
        If nPinA < 1 Or nPinA > 1 Then
            Echo("SWITCH S802 PROGRAMMING ERROR:  Command S802.swClose nPinA argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If
        If nPinB < 2 Or nPinB > 9 Then
            Echo("SWITCH S802 PROGRAMMING ERROR:  Command S802.swClose nPinB argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If

        sSwitch = "4.1" & CStr(nPinB - 2)

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        If Not bSimulation Then WriteSW("CLOSE " & sSwitch)
    End Sub

    Public Sub swOpen()
        Dim sSwitch As String

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        'Set Display
        gFrmMain.txtInstrument.Text = "Switch S802"
        gFrmMain.txtCommand.Text = "swOpen"

        sSwitch = "4.10-17"

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        If Not bSimulation Then WriteSW("OPEN " & sSwitch)
    End Sub
End Class