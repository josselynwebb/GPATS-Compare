Option Strict Off
Option Explicit On
Friend Class frmFHDBComment
    Inherits System.Windows.Forms.Form

    Private Sub _cmdOperatorComment_0_Click(sender As Object, e As EventArgs) Handles cmdOperatorComment_0.Click
        TestData.sComment = cmdOperatorComment_0.Text
        Me.Hide()
    End Sub

    Private Sub _cmdOperatorComment_1_Click(sender As Object, e As EventArgs) Handles cmdOperatorComment_1.Click
        TestData.sComment = cmdOperatorComment_1.Text
        Me.Hide()
    End Sub

    Private Sub _cmdOperatorComment_2_Click(sender As Object, e As EventArgs) Handles cmdOperatorComment_2.Click
        TestData.sComment = cmdOperatorComment_2.Text
        Me.Hide()
    End Sub

    Private Sub _cmdOperatorComment_3_Click(sender As Object, e As EventArgs) Handles cmdOperatorComment_3.Click
        TestData.sComment = cmdOperatorComment_3.Text
        Me.Hide()
    End Sub

    Private Sub _cmdOperatorComment_4_Click(sender As Object, e As EventArgs) Handles cmdOperatorComment_4.Click
        TestData.sComment = cmdOperatorComment_4.Text
        Me.Hide()
    End Sub

    Private Sub _cmdOperatorComment_5_Click(sender As Object, e As EventArgs) Handles cmdOperatorComment_5.Click
        TestData.sComment = cmdOperatorComment_5.Text
        Me.Hide()
    End Sub

    Private Sub _cmdOperatorComment_6_Click(sender As Object, e As EventArgs) Handles cmdOperatorComment_6.Click
        TestData.sComment = cmdOperatorComment_6.Text
        Me.Hide()
    End Sub

    Private Sub _cmdOperatorComment_7_Click(sender As Object, e As EventArgs) Handles cmdOperatorComment_7.Click
        TestData.sComment = cmdOperatorComment_7.Text
        Me.Hide()
    End Sub

    Private Sub _cmdOperatorComment_8_Click(sender As Object, e As EventArgs) Handles cmdOperatorComment_8.Click
        TestData.sComment = cmdOperatorComment_8.Text
        Me.Hide()
    End Sub

    Private Sub _cmdOperatorComment_9_Click(sender As Object, e As EventArgs) Handles cmdOperatorComment_9.Click
        CenterForm(gFrmOperatorComment)
        Me.Hide()
        gFrmOperatorComment.ShowDialog()
    End Sub
End Class