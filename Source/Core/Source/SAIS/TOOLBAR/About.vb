Option Strict Off
Option Explicit On
Friend Class frmAbout
	Inherits System.Windows.Forms.Form
	'**************************************************************
	'* Virutal Instrument Portable Equipment Repair/Test (VIPERT) *
	'*                                                            *
	'* Nomenclature   : SAIS: About Box Template                  *
	'* Written By     : David W. Hartley                          *
	'* Purpose        : This module displays the ABOUT BOX        *
	'**************************************************************
	
	
	Private Sub frmAbout_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
		Dim Version As String
		Version = GatherIniFileInformation("System Startup", "SWR", "Unknown")
		
        'Center this form
        Me.CenterToScreen()
        'Top = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) / 2 - VB6.PixelsToTwipsY(Height) / 2)
        'Left = VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) / 2 - VB6.PixelsToTwipsX(Width) / 2)
		
		'Updates Version number automatically to reflect Project Properties.
		lblInstrument(1).Text = Version
	End Sub
	
	
	
End Class