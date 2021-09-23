Option Strict Off
Option Explicit On
Friend Class frmAbout
	Inherits System.Windows.Forms.Form
	'************************************************************
	'* Third Echelon Test Set (TETS) Software Module            *
	'*                                                          *
	'* Nomenclature   : SAIS: About Box Template                *
	'* Version        : 1.0a                                    *
	'* Written By     : David W. Hartley                        *
	'* Last Update    : 7/11/96                                 *
	'* Purpose        : This module displays the SAIS ABOUT BOX *
	'************************************************************
	'
	'*Put the following items to your MAIN.BAS (Customize it to your instrument):
	'!!!!!!!!!!ENTER INSTRUMENT ATTRIBUTES BELOW!!!!!!
	'-----------------Global Constant Values------------------------------
	'Global Const INSTRUMENT_NAME$ = "RF Signal Generator"
	'Global Const MANUF$ = "EIP Microwave Inc."
	'Global Const MODEL_CODE$ = "EIP1143A"
	'Global Const RESOURCE_NAME$ = "VXI::144"
	'Global Const SAIS_VERSION$ = "1.0"
	'Global frmParent As Object
	'
	'*Add following line to your Sub Main:
	'   Set frmParent = frmRFSG   'Name of your Main Form
	'
	'*Example:
	'Sub cmdAbout_Click()
	'    frmAbout.Show 1
	'End Sub
	'
	'*For Splash Screen purpose, use following two commands (Available in TETSLIB.BAS):
	'   SplashStart
	'   SplashEnd
	
	
	
	
	
	Private Sub cmdOk_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdOk.Click
		
		Me.Close()
		
	End Sub
	
	
	Private Sub frmAbout_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
		
		CenterForm(Me)
		panTitle.Text = INSTRUMENT_NAME
		
		'JRC Added 033009
		Dim Version As String
		Version = GatherIniFileInformation("System Startup", "SWR", "Unknown")
		
		lblInstrument(0).Text = MANUF
		lblInstrument(1).Text = MODEL_CODE
		lblInstrument(2).Text = RESOURCE_NAME
		'Updates Version number to reflect Project Properties.  DJoiner  01/14/2002
		'lblInstrument(3).Caption = App.Major & "." & App.Minor
		lblInstrument(3).Text = Version
		'UPGRADE_WARNING: Couldn't resolve default property of object frmParent.Icon. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		Image1.Image = frmParent.Icon.ToBitmap
		
	End Sub
End Class