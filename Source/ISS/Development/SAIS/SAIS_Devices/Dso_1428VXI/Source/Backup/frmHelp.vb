Option Strict Off
Option Explicit On
Friend Class frmHelp
	Inherits System.Windows.Forms.Form
	
	Private Sub cmdOk_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdOk.Click
		
		Me.Close()
		
	End Sub
End Class