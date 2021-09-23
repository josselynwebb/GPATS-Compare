Option Strict Off
Option Explicit On
Friend Class frmEoPwrUpStatus
    Inherits System.Windows.Forms.Form

    Dim timerIndex As Integer = 0

    Private Sub frmEoPwrUpStatus_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        pbrEoPower.Value = 0
        pbrEoPower.Maximum = 180
    End Sub

    Private Sub frmEoPwrUpStatus_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown

        Me.lblProgress.Text = "Please Wait 3 minutes for the EO to warm up and Initialize"
        Me.Timer1.Enabled = True

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        timerIndex = timerIndex + 1

        Me.pbrEoPower.Value = timerIndex

        If (timerIndex = 180) Then
            Me.Timer1.Enabled = False
            Me.timerIndex = 0
            Me.Close()
        End If

    End Sub

End Class