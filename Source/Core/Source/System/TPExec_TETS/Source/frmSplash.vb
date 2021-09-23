Option Strict Off
Option Explicit On

Friend Class frmSplash
    Inherits System.Windows.Forms.Form

    Private Sub frmSplash_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        Me.Hide()
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmSplash_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        'Set test program specific information on Splash Screen
        lblTPName.Text = TPName
        lblTPVersion.Text = "Version: " & TPVersion
        lblUUTName.Text = UUTName
        lblUUTPartNo.Text = "Part No.: " & UUTPartNo
        lblTPCCN.Text = "TPCCN: " & TestData.sTPCCN
        lblTPExecVersion.Text = "TP Executive Ver.: " & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Revision
    End Sub

    Private Sub SplashTimer_Tick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SplashTimer.Tick
        Me.Hide() 'Unload splash after 5 seconds
    End Sub

    Private Sub Frame1_Click(sender As Object, e As EventArgs) Handles Frame1.Click
        Me.Hide()
    End Sub

    Private Sub cmdOk_Click(sender As Object, e As EventArgs) Handles cmdOk.Click
        Me.Hide()
    End Sub
End Class