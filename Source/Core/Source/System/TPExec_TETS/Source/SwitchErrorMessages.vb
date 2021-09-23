Option Strict Off
Option Explicit On
Module SwitchErrorMessages

    Public Function Switch200ErrorCheck(sSwitchNumber As String, sCall As String, nPinA As Integer, nPinB As Integer) As Boolean

        Switch200ErrorCheck = True

        If nPinA < 1 Or nPinA > 4 Then
            Echo("SWITCH " & sSwitchNumber & "PROGRAMMING ERROR:  Command S" & sSwitchNumber & "." & sCall & " nPinA argument out of range.")
            GoTo ExitFunction
        End If
        If nPinB < 5 Or nPinB > 52 Then
            Echo("SWITCH " & sSwitchNumber & "PROGRAMMING ERROR:  Command S" & sSwitchNumber & "." & sCall & " nPinB argument out of range.")
            GoTo ExitFunction
        End If

        Select Case nPinA
            Case 1, 3
                If (nPinB Mod 2) = 0 Then
                    Echo("SWITCH " & sSwitchNumber & "PROGRAMMING ERROR:  Command S" & sSwitchNumber & "." & sCall & " invalid switch closure. Check Pin arguments.")
                    GoTo ExitFunction
                End If
            Case Else
                If (nPinB Mod 2) <> 0 Then
                    Echo("SWITCH " & sSwitchNumber & "PROGRAMMING ERROR:  Command S" & sSwitchNumber & "." & sCall & " invalid switch closure. Check Pin arguments.")
                    GoTo ExitFunction
                End If
        End Select

        Exit Function

ExitFunction:
        Err.Raise(-1000)
        Switch200ErrorCheck = False

    End Function

    Public Function Switch400ErrorCheck(sSwitchNumber As String, sCall As String, nPinA As Integer, nPinB As Integer) As Boolean

        Switch400ErrorCheck = True

        If nPinA <> 1 Then
            Echo("SWITCH " & sSwitchNumber & "PROGRAMMING ERROR:  Command S" & sSwitchNumber & "." & sCall & " nPinA argument out of range.")
            GoTo ExitFunction
        End If
        If nPinB < 2 Or nPinB > 5 Then
            Echo("SWITCH " & sSwitchNumber & "PROGRAMMING ERROR:  Command S" & sSwitchNumber & "." & sCall & " nPinB argument out of range.")
            GoTo ExitFunction
        End If
        If nPinA > nPinB Then
            Echo("SWITCH " & sSwitchNumber & "PROGRAMMING ERROR:  Command S" & sSwitchNumber & "." & sCall & " nPinA argument must be less than nPinB argument.")
            GoTo ExitFunction
        End If

        Exit Function

ExitFunction:
        Err.Raise(-1000)
        Switch400ErrorCheck = False

    End Function

    Public Function Switch500ErrorCheck(sSwitchNumber As String, sCall As String, nPinA As Integer, nPinB As Integer) As Boolean

        Switch500ErrorCheck = True

        If nPinA < 1 Or nPinA > 2 Then
            Echo("SWITCH " & sSwitchNumber & "PROGRAMMING ERROR:  Command S" & sSwitchNumber & "." & sCall & " nPinA argument out of range.")
            GoTo ExitFunction
        End If
        If nPinB < 3 Or nPinB > 10 Then
            Echo("SWITCH " & sSwitchNumber & "PROGRAMMING ERROR:  Command S" & sSwitchNumber & "." & sCall & " nPinB argument out of range.")
            GoTo ExitFunction
        End If

        Exit Function

ExitFunction:
        Err.Raise(-1000)
        Switch500ErrorCheck = False

    End Function

    Public Function Switch600ErrorCheck(sSwitchNumber As String, sCall As String, nPinA As Integer, nPinB As Integer) As Boolean

        Switch600ErrorCheck = True

        If nPinA <> 1 Then
            Echo("SWITCH " & sSwitchNumber & "PROGRAMMING ERROR:  Command S" & sSwitchNumber & "." & sCall & " nPinA argument out of range.")
            GoTo ExitFunction
        End If
        If nPinB < 2 Or nPinB > 3 Then
            Echo("SWITCH " & sSwitchNumber & "PROGRAMMING ERROR:  Command S" & sSwitchNumber & "." & sCall & " nPinB argument out of range.")
            GoTo ExitFunction
        End If

        Exit Function

ExitFunction:
        Err.Raise(-1000)
        Switch600ErrorCheck = False

    End Function

    Public Function Switch800900ErrorCheck(sSwitchNumber As String, sCall As String, nPinA As Integer, nPinB As Integer, nTestValue As Integer) As Boolean

        Switch800900ErrorCheck = True

        If nPinA <> 1 Then
            Echo("SWITCH " & sSwitchNumber & "PROGRAMMING ERROR:  Command S" & sSwitchNumber & "." & sCall & " nPinA argument out of range.")
            GoTo ExitFunction
        End If
        If nPinB < 2 Or nPinB > nTestValue Then
            Echo("SWITCH " & sSwitchNumber & "PROGRAMMING ERROR:  Command S" & sSwitchNumber & "." & sCall & " nPinB argument out of range.")
            GoTo ExitFunction
        End If

        Exit Function

ExitFunction:
        Err.Raise(-1000)
        Switch800900ErrorCheck = False

    End Function



End Module
