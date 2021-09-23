Option Strict Off
Option Explicit On

<System.Runtime.InteropServices.ProgId("S701_NET.S701")> Public Class S701
    '**************************************************************
    '* Third Echelon Test Set (TETS) Software Module              *
    '*                                                            *
    '* Nomenclature   : S701 Switch Utility                       *
    '* Written By     : G. Johnson                                *
    '* Purpose        : This Clase module contains definitions    *
    '*                  for switch names                          *
    '**************************************************************

    Public Sub swClose(ByRef nPinA As Short, ByRef nPinB As Short)
        Dim sSwitch As String

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        'Set Display
        gFrmMain.txtInstrument.Text = "Switch S701"
        gFrmMain.txtCommand.Text = "swClose"

        'Error Check Pin Numbers
        If nPinA < 1 Or nPinA > 2 Then
            Echo("SWITCH S701 PROGRAMMING ERROR:  Command S701.swClose nPinA argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If
        If nPinB < 3 Or nPinB > 50 Then
            Echo("SWITCH S701 PROGRAMMING ERROR:  Command S701.swClose nPinB argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If

        Select Case nPinA
            Case 1
                If (nPinB Mod 2) = 0 Then
                    Echo("SWITCH S701 PROGRAMMING ERROR:  Command S701.swClose invalid switch closure. Check Pin arguments.")
                    Err.Raise(-1000)
                    Exit Sub
                End If
            Case Else
                If (nPinB Mod 2) <> 0 Then
                    Echo("SWITCH S701 PROGRAMMING ERROR:  Command S701.swClose invalid switch closure. Check Pin arguments.")
                    Err.Raise(-1000)
                    Exit Sub
                End If
        End Select

        Select Case nPinB
            Case 3, 4
                sSwitch = "3.0010"
            Case 5, 6
                sSwitch = "3.0011"
            Case 7, 8
                sSwitch = "3.0012"
            Case 9, 10
                sSwitch = "3.0013"
            Case 11, 12
                sSwitch = "3.0014"
            Case 13, 14
                sSwitch = "3.0015"
            Case 15, 16
                sSwitch = "3.0016"
            Case 17, 18
                sSwitch = "3.0017"
            Case 19, 20
                sSwitch = "3.0020"
            Case 21, 22
                sSwitch = "3.0021"
            Case 23, 24
                sSwitch = "3.0022"
            Case 25, 26
                sSwitch = "3.0023"
            Case 27, 28
                sSwitch = "3.0024"
            Case 29, 30
                sSwitch = "3.0025"
            Case 31, 32
                sSwitch = "3.0026"
            Case 33, 34
                sSwitch = "3.0027"
            Case 35, 36
                sSwitch = "3.0030"
            Case 37, 38
                sSwitch = "3.0031"
            Case 39, 40
                sSwitch = "3.0032"
            Case 41, 42
                sSwitch = "3.0033"
            Case 43, 44
                sSwitch = "3.0034"
            Case 45, 46
                sSwitch = "3.0035"
            Case 47, 48
                sSwitch = "3.0036"
            Case 49, 50
                sSwitch = "3.0037"
            Case Else
                sSwitch = "3.0037"
        End Select

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        If Not bSimulation Then WriteSW("CLOSE " & sSwitch)
    End Sub

    Public Sub swOpen(Optional ByRef nPinA As Short = 0, Optional ByRef nPinB As Short = 0)
        Dim sSwitch As String

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        'Set Display
        gFrmMain.txtInstrument.Text = "Switch S701"
        gFrmMain.txtCommand.Text = "swOpen"

        'Error Check Pin Numbers
        If nPinA < 1 Or nPinA > 2 Then
            Echo("SWITCH S701 PROGRAMMING ERROR:  Command S701.swOpen nPinA argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If
        If nPinB < 3 Or nPinB > 50 Then
            Echo("SWITCH S701 PROGRAMMING ERROR:  Command S701.swOpen nPinB argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If

        Select Case nPinA
            Case 1
                If (nPinB Mod 2) = 0 Then
                    Echo("SWITCH S701 PROGRAMMING ERROR:  Command S701.swOpen invalid switch closure. Check Pin arguments.")
                    Err.Raise(-1000)
                    Exit Sub
                End If
            Case Else
                If (nPinB Mod 2) <> 0 Then
                    Echo("SWITCH S701 PROGRAMMING ERROR:  Command S701.swOpen invalid switch closure. Check Pin arguments.")
                    Err.Raise(-1000)
                    Exit Sub
                End If
        End Select

        Select Case nPinB
            Case 3, 4
                sSwitch = "3.0010"
            Case 5, 6
                sSwitch = "3.0011"
            Case 7, 8
                sSwitch = "3.0012"
            Case 9, 10
                sSwitch = "3.0013"
            Case 11, 12
                sSwitch = "3.0014"
            Case 13, 14
                sSwitch = "3.0015"
            Case 15, 16
                sSwitch = "3.0016"
            Case 17, 18
                sSwitch = "3.0017"
            Case 19, 20
                sSwitch = "3.0020"
            Case 21, 22
                sSwitch = "3.0021"
            Case 23, 24
                sSwitch = "3.0022"
            Case 25, 26
                sSwitch = "3.0023"
            Case 27, 28
                sSwitch = "3.0024"
            Case 29, 30
                sSwitch = "3.0025"
            Case 31, 32
                sSwitch = "3.0026"
            Case 33, 34
                sSwitch = "3.0027"
            Case 35, 36
                sSwitch = "3.0030"
            Case 37, 38
                sSwitch = "3.0031"
            Case 39, 40
                sSwitch = "3.0032"
            Case 41, 42
                sSwitch = "3.0033"
            Case 43, 44
                sSwitch = "3.0034"
            Case 45, 46
                sSwitch = "3.0035"
            Case 47, 48
                sSwitch = "3.0036"
            Case 49, 50
                sSwitch = "3.0037"
            Case Else
                sSwitch = "3.0037"
        End Select

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        If Not bSimulation Then WriteSW("OPEN " & sSwitch)
    End Sub

    Public Sub swOpenAll()
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        'Set Display
        gFrmMain.txtInstrument.Text = "Switch S701"
        gFrmMain.txtCommand.Text = "swOpenAll"

        If Not bSimulation Then WriteSW("OPEN 3.0010-0037")
    End Sub
End Class