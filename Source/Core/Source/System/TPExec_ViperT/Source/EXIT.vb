Option Strict Off
Option Explicit On
Friend Class ExitForm
	Inherits System.Windows.Forms.Form

    Private Sub Cancel_Click(sender As Object, e As EventArgs) Handles Cancel.Click
        Cancel_Click()
    End Sub

	Private Sub Cancel_Click()
		Me.Hide()
		UserEvent = 0
	End Sub

	Private Sub Cancel_KeyDown(ByRef KeyCode As Short, ByRef Shift As Short)
		Select Case KeyCode
			Case System.Windows.Forms.Keys.C Or System.Windows.Forms.Keys.Return
				If Shift = 2 Then Cancel_Click()
			Case System.Windows.Forms.Keys.O
				If Shift = 2 Then VerifyExit_Click()
		End Select
	End Sub
	
    Private Sub ExitForm_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        'Added 4/4/00 SJM
        CenterForm(Me)
    End Sub

    Private Sub VerifyExit_Click(sender As Object, e As EventArgs) Handles VerifyExit.Click
        VerifyExit_Click()
    End Sub

	Private Sub VerifyExit_Click()
        Me.VerifyExit.Enabled = False
        Me.Cancel.Enabled = False
		gFrmMain.Timer1.Enabled = False
		gFrmMain.Timer1b.Enabled = False
		gFrmMain.Timer2.Enabled = False
		gFrmMain.Timer2b.Enabled = False
		gFrmMain.Timer3.Enabled = False
		gFrmMain.Close()
    End Sub
End Class