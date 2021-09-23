Option Strict Off
Option Explicit On

<System.Runtime.InteropServices.ProgId("S202_NET.S202")> Public Class S202
    '**************************************************************
    '* Third Echelon Test Set (TETS) Software Module              *
    '*                                                            *
    '* Nomenclature   : S202 Switch Utility                       *
    '* Written By     : G. Johnson                                *
    '* Purpose        : This Clase module contains definitions    *
    '*                  for switch names                          *
    '**************************************************************

    Public Sub swClose(ByRef nPinA As Short, ByRef nPinB As Short)
        Dim sSwitch As String

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        'Set Display
        gFrmMain.txtInstrument.Text = "Switch S202"
        gFrmMain.txtCommand.Text = "swClose"

        'Error Check Pin Numbers
        If nPinA < 1 Or nPinA > 4 Then
            Echo("SWITCH S202 PROGRAMMING ERROR:  Command S202.swClose nPinA argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If

        If nPinB < 5 Or nPinB > 52 Then
            Echo("SWITCH S202 PROGRAMMING ERROR:  Command S202.swClose nPinB argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If

        Select Case nPinA
            Case 1, 3
                If (nPinB Mod 2) = 0 Then
                    Echo("SWITCH S202 PROGRAMMING ERROR:  Command S202.swClose invalid switch closure. Check Pin arguments.")
                    Err.Raise(-1000)
                    Exit Sub
                End If
            Case Else
                If (nPinB Mod 2) <> 0 Then
                    Echo("SWITCH S202 PROGRAMMING ERROR:  Command S202.swClose invalid switch closure. Check Pin arguments.")
                    Err.Raise(-1000)
                    Exit Sub
                End If
        End Select

        Select Case nPinB
            Case 5, 6
                If nPinA < 3 Then sSwitch = "3.0100" Else sSwitch = "3.0110"
            Case 7, 8
                If nPinA < 3 Then sSwitch = "3.0101" Else sSwitch = "3.0111"
            Case 9, 10
                If nPinA < 3 Then sSwitch = "3.0102" Else sSwitch = "3.0112"
            Case 11, 12
                If nPinA < 3 Then sSwitch = "3.0103" Else sSwitch = "3.0113"
            Case 13, 14
                If nPinA < 3 Then sSwitch = "3.0104" Else sSwitch = "3.0114"
            Case 15, 16
                If nPinA < 3 Then sSwitch = "3.0105" Else sSwitch = "3.0115"
            Case 17, 18
                If nPinA < 3 Then sSwitch = "3.0106" Else sSwitch = "3.0116"
            Case 19, 20
                If nPinA < 3 Then sSwitch = "3.0107" Else sSwitch = "3.0117"
            Case 21, 22
                If nPinA < 3 Then sSwitch = "3.0120" Else sSwitch = "3.0130"
            Case 23, 24
                If nPinA < 3 Then sSwitch = "3.0121" Else sSwitch = "3.0131"
            Case 25, 26
                If nPinA < 3 Then sSwitch = "3.0122" Else sSwitch = "3.0132"
            Case 27, 28
                If nPinA < 3 Then sSwitch = "3.0123" Else sSwitch = "3.0133"
            Case 29, 30
                If nPinA < 3 Then sSwitch = "3.0124" Else sSwitch = "3.0134"
            Case 31, 32
                If nPinA < 3 Then sSwitch = "3.0125" Else sSwitch = "3.0135"
            Case 33, 34
                If nPinA < 3 Then sSwitch = "3.0126" Else sSwitch = "3.0136"
            Case 35, 36
                If nPinA < 3 Then sSwitch = "3.0127" Else sSwitch = "3.0137"
            Case 37, 38
                If nPinA < 3 Then sSwitch = "3.0140" Else sSwitch = "3.0150"
            Case 39, 40
                If nPinA < 3 Then sSwitch = "3.0141" Else sSwitch = "3.0151"
            Case 41, 42
                If nPinA < 3 Then sSwitch = "3.0142" Else sSwitch = "3.0152"
            Case 43, 44
                If nPinA < 3 Then sSwitch = "3.0143" Else sSwitch = "3.0153"
            Case 45, 46
                If nPinA < 3 Then sSwitch = "3.0144" Else sSwitch = "3.0154"
            Case 47, 48
                If nPinA < 3 Then sSwitch = "3.0145" Else sSwitch = "3.0155"
            Case 49, 50
                If nPinA < 3 Then sSwitch = "3.0146" Else sSwitch = "3.0156"
            Case 51, 52
                If nPinA < 3 Then sSwitch = "3.0147" Else sSwitch = "3.0157"
            Case Else
                If nPinA < 3 Then sSwitch = "3.0147" Else sSwitch = "3.0157"
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
        gFrmMain.txtInstrument.Text = "Switch S202"
        gFrmMain.txtCommand.Text = "swOpen"

        'Error Check Pin Numbers
        If nPinA < 1 Or nPinA > 4 Then
            Echo("SWITCH S202 PROGRAMMING ERROR:  Command S202.swOpen nPinA argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If

        If nPinB < 5 Or nPinB > 52 Then
            Echo("SWITCH S202 PROGRAMMING ERROR:  Command S202.swOpen nPinB argument out of range.")
            Err.Raise(-1000)
            Exit Sub
        End If

        Select Case nPinA
            Case 1, 3
                If (nPinB Mod 2) = 0 Then
                    Echo("SWITCH S202 PROGRAMMING ERROR:  Command S202.swOpen invalid switch closure. Check Pin arguments.")
                    Err.Raise(-1000)
                    Exit Sub
                End If
            Case Else
                If (nPinB Mod 2) <> 0 Then
                    Echo("SWITCH S202 PROGRAMMING ERROR:  Command S202.swOpen invalid switch closure. Check Pin arguments.")
                    Err.Raise(-1000)
                    Exit Sub
                End If
        End Select

        Select Case nPinB
            Case 5, 6
                If nPinA < 3 Then sSwitch = "3.0100" Else sSwitch = "3.0110"
            Case 7, 8
                If nPinA < 3 Then sSwitch = "3.0101" Else sSwitch = "3.0111"
            Case 9, 10
                If nPinA < 3 Then sSwitch = "3.0102" Else sSwitch = "3.0112"
            Case 11, 12
                If nPinA < 3 Then sSwitch = "3.0103" Else sSwitch = "3.0113"
            Case 13, 14
                If nPinA < 3 Then sSwitch = "3.0104" Else sSwitch = "3.0114"
            Case 15, 16
                If nPinA < 3 Then sSwitch = "3.0105" Else sSwitch = "3.0115"
            Case 17, 18
                If nPinA < 3 Then sSwitch = "3.0106" Else sSwitch = "3.0116"
            Case 19, 20
                If nPinA < 3 Then sSwitch = "3.0107" Else sSwitch = "3.0117"
            Case 21, 22
                If nPinA < 3 Then sSwitch = "3.0120" Else sSwitch = "3.0130"
            Case 23, 24
                If nPinA < 3 Then sSwitch = "3.0121" Else sSwitch = "3.0131"
            Case 25, 26
                If nPinA < 3 Then sSwitch = "3.0122" Else sSwitch = "3.0132"
            Case 27, 28
                If nPinA < 3 Then sSwitch = "3.0123" Else sSwitch = "3.0133"
            Case 29, 30
                If nPinA < 3 Then sSwitch = "3.0124" Else sSwitch = "3.0134"
            Case 31, 32
                If nPinA < 3 Then sSwitch = "3.0125" Else sSwitch = "3.0135"
            Case 33, 34
                If nPinA < 3 Then sSwitch = "3.0126" Else sSwitch = "3.0136"
            Case 35, 36
                If nPinA < 3 Then sSwitch = "3.0127" Else sSwitch = "3.0137"
            Case 37, 38
                If nPinA < 3 Then sSwitch = "3.0140" Else sSwitch = "3.0150"
            Case 39, 40
                If nPinA < 3 Then sSwitch = "3.0141" Else sSwitch = "3.0151"
            Case 41, 42
                If nPinA < 3 Then sSwitch = "3.0142" Else sSwitch = "3.0152"
            Case 43, 44
                If nPinA < 3 Then sSwitch = "3.0143" Else sSwitch = "3.0153"
            Case 45, 46
                If nPinA < 3 Then sSwitch = "3.0144" Else sSwitch = "3.0154"
            Case 47, 48
                If nPinA < 3 Then sSwitch = "3.0145" Else sSwitch = "3.0155"
            Case 49, 50
                If nPinA < 3 Then sSwitch = "3.0146" Else sSwitch = "3.0156"
            Case 51, 52
                If nPinA < 3 Then sSwitch = "3.0147" Else sSwitch = "3.0157"
            Case Else
                If nPinA < 3 Then sSwitch = "3.0147" Else sSwitch = "3.0157"
        End Select

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        'Set the necessary -38 interconnect relays
        If Not bSimulation Then WriteSW("CLOSE 3.1001,2002,3000,3001,4000,4001,6000,6001,7000,7001")
        If Not bSimulation Then WriteSW("OPEN " & sSwitch)
    End Sub

    Public Sub swOpenAll()

        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Sub
        'Set Display
        gFrmMain.txtInstrument.Text = "Switch S202"
        gFrmMain.txtCommand.Text = "swOpenAll"

        'Set the necessary -38 interconnect relays
        If Not bSimulation Then WriteSW("CLOSE 3.1001,2002,3000,3001,4000,4001,6000,6001,7000,7001")
        If Not bSimulation Then WriteSW("OPEN 3.0100-0157")
    End Sub
End Class