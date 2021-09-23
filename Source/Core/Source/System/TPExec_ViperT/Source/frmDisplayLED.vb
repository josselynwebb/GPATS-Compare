Option Strict Off
Option Explicit On
Friend Class frmDisplayLED
	Inherits System.Windows.Forms.Form

    Private Sub cmdLEDPicture_Click()

    End Sub
	
	Private Sub cmdNO_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdNO.Click
		LEDUserResponse = True
		LEDButtonPressed = "NO"
		Me.Hide()
	End Sub
	
	Private Sub cmdYES_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdYES.Click
		LEDUserResponse = True
		LEDButtonPressed = "YES"
		Me.Hide()
	End Sub
	
	Private Sub Text1_Change()
		
	End Sub
End Class