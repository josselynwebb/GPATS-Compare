Option Strict Off
Option Explicit On
Friend Class frmHelp
    Inherits System.Windows.Forms.Form

    Private Const SW_SHOWNORMAL As Short = 1
    Private Const SW_SHOWMINIMIZED As Short = 2
    Private Const SW_SHOWMAXIMIZED As Short = 3

    Private Declare Function ShellExecute Lib "shell32.dll" Alias "ShellExecuteA" (ByVal hwnd As Integer, ByVal lpOperation As String, ByVal lpFile As String, ByVal lpParameters As String, ByVal lpDirectory As String, ByVal nShowCmd As Integer) As Integer
	
	Private Sub cmdOk_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdOk.Click
		
		Me.Close()
		
	End Sub

    Private Sub picHelp_Click(sender As Object, e As EventArgs) Handles picHelp.Click
        Dim helpFile As String = Application.StartupPath + "\..\Config\Help\Dso_ZT1428VXI\zt1428.pdf"
        Dim helpFile2 As String = Application.StartupPath + "\..\Config\Help\Dso_ZT1428VXI\E1428SER.pdf"
        Dim Version As String
        Dim SystemType As String
        Version = GatherIniFileInformation("System Startup", "SWR", "Unknown")
        SystemType = GatherIniFileInformation("System Startup", "SYSTEM_TYPE", "Unknown")

        If (InStr(SystemType, "AN/USM-657(V)2")) Then
            ShellExecute(0, "Open", helpFile2, 0, 0, SW_SHOWNORMAL)
        Else
            ShellExecute(0, "Open", helpFile, 0, 0, SW_SHOWNORMAL)
        End If
    End Sub
End Class