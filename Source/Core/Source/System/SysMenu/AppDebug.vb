Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading

Public Module AppDebug

    'Constants


    'Check for existence of debug output file
    Public Function IsDebug() As Boolean

        If (System.IO.File.Exists(DEBUG_FILE)) Then
            IsDebug = True
        Else
            IsDebug = False
        End If

    End Function

    'Create Output file for debug statements
    Public Function CreateDebugFile() As Integer

        Dim stat As Integer = 0

        Try

            Dim dbgFile As System.IO.FileStream = System.IO.File.Create(DEBUG_RECORD)
            dbgFile.Close()
            dbgFile.Dispose()

        Catch Ex As Exception

            Console.Write(Ex.Message)
            stat = -1

        End Try
        CreateDebugFile = stat

    End Function


    'Write debug strings to output file for troubleshooting purposes
    Public Function WriteDebugInfo(debugString As String)

        'Send text string to debug output file
        Using dbgFile As System.IO.StreamWriter = System.IO.File.AppendText(DEBUG_RECORD)

            Try

                dbgFile.Write(debugString + Environment.NewLine)

            Catch Ex As System.IO.IOException

                Throw

                dbgFile.Close()
            End Try
        End Using
    End Function

End Module
