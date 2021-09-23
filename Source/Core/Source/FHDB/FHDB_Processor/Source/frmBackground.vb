Option Strict Off
Option Explicit On
Friend Class frmBackground
	Inherits System.Windows.Forms.Form
	'**********************************************************************
	'***    United States Marine Corps                                  ***
	'***                                                                ***
	'***    Nomenclature:   Form "frmBackground" : FHDB_Processor       ***
	'***    Written By:     Dave Joiner                                 ***
	'***    Purpose:                                                    ***
	'***  ECP-TETS-023                                                  ***
	'***    Provides a Background form to hold the TETS Logo and        ***
	'***    Status Bar for the FHDB_Processor.                          ***
	'**********************************************************************
	
	
	
	Private Sub frmBackground_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Click
		
		MDIMain.Activate() 'Don't let the Background get focus
		
	End Sub
	
	
	Private Sub frmBackground_GotFocus(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.GotFocus
		
		
		MDIMain.BringToFront() 'Keep Background behind Main Form
		
	End Sub
	
	Private Sub frmBackground_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
		
		Width = MDIMain.Width '11010        ' Set width of form.
		Height = VB6.TwipsToPixelsY((VB6.PixelsToTwipsY(MDIMain.Height) + 940)) '8650  ' Set height of form.
		Left = MDIMain.Left '   (Screen.Width - Width) / 2   ' Center form horizontally.
        Top = MDIMain.Top '(Screen.Height - Height) / 2   ' Center form vertically.


		
	End Sub
End Class