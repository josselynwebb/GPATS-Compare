Option Strict Off
Option Explicit On
Friend Class frmAbout
    Inherits System.Windows.Forms.Form

    Private Sub cmdOk_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdOK.Click
        Me.Hide()
    End Sub

    Private Sub frmAbout_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        Dim Version As String

        Version = GatherIniFileInformation(ATS_INI, "System Startup", "SWR")
        lblInstrumentVersion.Text = Version
        Me.Refresh()
    End Sub
End Class