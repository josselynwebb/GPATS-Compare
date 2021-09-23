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
        'panTitle.Text = INSTRUMENT_NAME
		
		'JRC Added 033009
		Dim Version As String
        Version = GatherIniFileInformation("System Startup", "SWR", "Unknown")

        If (DscopeMain.bIsHPE1428A = True) Then
            _lblInstrument_0.Text = "Hewlett Packard"
            _lblInstrument_1.Text = "E1428A"
        Else
            _lblInstrument_0.Text = MANUF
            _lblInstrument_1.Text = MODEL_CODE
        End If

        _lblInstrument_2.Text = RESOURCE_NAME
		'Updates Version number to reflect Project Properties.  DJoiner  01/14/2002
		'lblInstrument(3).Caption = App.Major & "." & App.Minor
        _lblInstrument_3.Text = Version
		
		Image1.Image = frmParent.Icon.ToBitmap
		
	End Sub
End Class