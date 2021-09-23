Option Strict Off
Option Explicit On

Friend Class frmHV
	Inherits System.Windows.Forms.Form

	Private Sub frmHV_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
		iAcknowledge = 2
        Me.Top = (System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height / 2) - (Me.Height / 2) ' - 600
        Me.Left = (System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width / 2) - (Me.Width / 2)
        If gFrmMain.SeqTextWindow.Visible Then Me.Left = Me.Left '- 700
    End Sub

    Private Sub MouseControl_0_Click(sender As Object, e As EventArgs) Handles MouseControl_0.Click
        MouseControl_Click(0)
    End Sub

    Private Sub MouseControl_1_Click(sender As Object, e As EventArgs) Handles MouseControl_1.Click
        MouseControl_Click(1)
    End Sub

	Private Sub MouseControl_Click(ByRef index As Short)
		If index = 0 Then iAcknowledge = 0 Else iAcknowledge = 1
    End Sub

	Private Sub SplashTimer_Tick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SplashTimer.Tick
		Do 
			Me.Label1.ForeColor = System.Drawing.Color.Black
			System.Windows.Forms.Application.DoEvents()
			If iAcknowledge = 0 Then Exit Do
			Delay(0.5)
			Me.Label1.ForeColor = System.Drawing.Color.Red
			System.Windows.Forms.Application.DoEvents()
			If iAcknowledge = 0 Then Exit Do
			Delay(0.5)
		Loop While iAcknowledge = 2
        Me.Hide()
	End Sub
End Class