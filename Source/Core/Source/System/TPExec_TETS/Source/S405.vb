Option Strict Off
Option Explicit On

<System.Runtime.InteropServices.ProgId("S405_NET.S405")> Public Class S405
    '**************************************************************
    '* Third Echelon Test Set (TETS) Software Module              *
    '*                                                            *
    '* Nomenclature   : S405 Switch Utility                       *
    '* Written By     : G. Johnson                                *
    '* Purpose        : This Clase module contains definitions    *
    '*                  for switch names                          *
    '**************************************************************

    Public Sub swClose(ByRef nPinA As Short, ByRef nPinB As Short)
        Dim sSwitch As String

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        'Set Display
        gFrmMain.txtInstrument.Text = "Switch S405"
        gFrmMain.txtCommand.Text = "swClose"

        'Error Check Pin Numbers
        If nPinA < 1 Or nPinA > 1 Then
            Echo("SWITCH S405 PROGRAMMING ERROR:  Command S405.swClose nPinA argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If
        If nPinB < 2 Or nPinB > 5 Then
            Echo("SWITCH S405 PROGRAMMING ERROR:  Command S405.swClose nPinB argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If
        If nPinA > nPinB Then
            Echo("SWITCH S405 PROGRAMMING ERROR:  Command S405.swOpen nPinA argument must be less than nPinB argument.")
            Err.Raise(-1000)
            Exit Sub
        End If

        Select Case nPinB
            Case 2
                sSwitch = "2.3100"
            Case 3
                sSwitch = "2.3101"
            Case 4
                sSwitch = "2.3102"
            Case 5
                sSwitch = "2.3103"
            Case Else
                sSwitch = "2.3103"
        End Select

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        If Not bSimulation Then WriteSW("CLOSE " & sSwitch)
    End Sub

    Public Sub swOpen(Optional ByRef nPinA As Short = 0, Optional ByRef nPinB As Short = 0)
        Dim sSwitch As String

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        'Set Display
        gFrmMain.txtInstrument.Text = "Switch S405"
        gFrmMain.txtCommand.Text = "swOpen"

        'Error Check Pin Numbers
        If nPinA < 1 Or nPinA > 1 Then
            Echo("SWITCH S405 PROGRAMMING ERROR:  Command S405.swOpen nPinA argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If
        If nPinB < 2 Or nPinB > 5 Then
            Echo("SWITCH S405 PROGRAMMING ERROR:  Command S405.swOpen nPinB argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If
        If nPinA > nPinB Then
            Echo("SWITCH S405 PROGRAMMING ERROR:  Command S405.swOpen nPinA argument must be less than nPinB argument.")
            Err.Raise(-1000)
            Exit Sub
        End If

        Select Case nPinB
            Case 2
                sSwitch = "2.3100"
            Case 3
                sSwitch = "2.3101"
            Case 4
                sSwitch = "2.3102"
            Case 5
                sSwitch = "2.3103"
            Case Else
                sSwitch = "2.3103"
        End Select

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        If Not bSimulation Then WriteSW("OPEN " & sSwitch)
    End Sub

    Public Sub swOpenAll()

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        'Set Display
        gFrmMain.txtInstrument.Text = "Switch S405"
        gFrmMain.txtCommand.Text = "swOpenAll"

        If Not bSimulation Then WriteSW("OPEN 2.3100-3103")
    End Sub
End Class