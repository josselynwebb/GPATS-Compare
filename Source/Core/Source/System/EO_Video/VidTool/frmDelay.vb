Option Strict Off
Option Explicit On

Friend Class frmDelay
    Inherits System.Windows.Forms.Form

    Public Sub frmDelay_Load(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles MyBase.Load
        Me.Height = 92
        Text1.Text = "Initializing the VEO2" & vbCrLf & "180 sec"
    End Sub
End Class
