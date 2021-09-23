'Option Explicit On

Imports System
Imports Microsoft.VisualBasic

Public Class Common
    '=========================================================
    Public Function Simulate(ByVal Commands As Collection) As String
        Simulate = ""
        Dim vCmd As Object
        Dim frmSimulate As New frmSimulate

        '   Load Simulation form
        '! Load frmSimulate
        frmSimulate.cboSimCmds.Items.Clear()

        '   Populate combo box
        For Each vCmd In Commands
            If InStr(1, Convert.ToString(vCmd), "Simulated response for:", CompareMethod.Text)<>0 Then
                frmSimulate.lblCaption.Text = Convert.ToString(vCmd)
            Else
                frmSimulate.cboSimCmds.Items.Add(vCmd)
            End If
        Next

        '   Set index to display first item
        frmSimulate.cboSimCmds.SelectedIndex = 0

        '   Display form
        frmSimulate.ShowDialog()

        '   Pass simulation selection back
        Simulate = frmSimulate.cboSimCmds.Text

    End Function

    Public Function ParseAvailiability(ByVal sXML As String, Optional ByVal MultiResource As Boolean = False) As Short
        Dim iStatus As Short
        Dim lPos As Integer
        Dim sTag As String = ""
        Dim sAttrib As String

        'Parse XML response for availability
        'Returns: 0 = invalid XML tag or missing attribute
        '              1 = Instrument in use Debug Mode
        '              2 = Instrument Ready Local Mode
        '              4 = CalExpired
        '              8 = Communication Error
        '             16 = Unknown or other error

        If sXML <> "" Then
            If MultiResource = True Then
                If sXML.IndexOf("InUse") > 0 Then
                    iStatus = 1 'Remote
                Else
                    iStatus = 2 'Local
                End If
            Else
                'Strip prior to tag
                lPos = InStr(sXML, "<ResourceAvailability")
                If lPos > 0 Then sTag = Mid(sXML, lPos)
                'Strip after tag
                lPos = InStr(sTag, ">")
                If lPos > 0 Then sTag = Strings.Left(sTag, lPos)

                'look for availability attribuite
                lPos = InStr(sTag, "availability=") + 14
                If lPos > 14 Then
                    sAttrib = Mid(sTag, lPos)
                    sAttrib = Strings.Left(sAttrib, InStr(sAttrib, Convert.ToString(Chr(34))) - 1)

                    'MsgBox sAttrib, vbInformation, "Aval Value"
                    Select Case sAttrib
                        Case "AvailableInUse", "SimulatedInUse"
                            iStatus = 1 'Debug
                        Case "Available", "Simulated"
                            iStatus = 2 'Local
                        Case "CalExpired"
                            iStatus = 4 'Calibration
                        Case "NoResponse", "NotFound"
                            iStatus = 8 'Communication

                        Case Else
                            iStatus = 16 'Error or other status
                    End Select
                End If
            End If
        End If

        ParseAvailiability = iStatus
    End Function

End Class