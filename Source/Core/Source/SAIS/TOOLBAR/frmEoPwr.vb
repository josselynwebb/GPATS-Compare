Option Strict Off
Option Explicit On
Friend Class frmEoPwrUpStatus
	Inherits System.Windows.Forms.Form
	Private Sub frmEoPwrUpStatus_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
		pbrEoPower.Value = 0
        'CenterForm(Me)
		Me.Show()
		Me.Refresh()
	End Sub
End Class