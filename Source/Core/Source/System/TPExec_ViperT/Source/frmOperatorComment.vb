Option Strict Off
Option Explicit On

Friend Class frmOperatorComment
	Inherits System.Windows.Forms.Form

    Private Sub cmdContinue_Click(sender As Object, e As EventArgs) Handles cmdContinue.Click
        TestData.sComment = txtOperatorComment.Text
        Me.Hide()
    End Sub
	
	Private Sub frmOperatorComment_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
		txtOperatorComment.Text = ""
	End Sub

    Private Sub frmOperatorComment_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        Me.cmdContinue.SetBounds(Limit(Me.Width - 120, 0), Limit(Me.Height - 85, 0), 100, 29, BoundsSpecified.Location)
        Me.txtOperatorComment.SetBounds(0, 0, Limit(Me.Height - 47, 545), 105, BoundsSpecified.Height)
        Me.SSPanel1.SetBounds(0, 0, Limit(Me.Height - 31, 561), 121, BoundsSpecified.Height)
    End Sub
End Class