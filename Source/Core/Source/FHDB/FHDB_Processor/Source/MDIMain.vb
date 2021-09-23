
Friend Class MDIMain
	Inherits System.Windows.Forms.Form
	'**********************************************************************
	'***    United States Marine Corps                                  ***
	'***                                                                ***
	'***    Nomenclature:   Form "MDIMain" : FHDB FHDB_Processor        ***
	'***    Written By:     Dave Joiner                                 ***
	'***    Purpose:                                                    ***
	'***  ECP-TETS-023                                                  ***
	'***    The Multiple Document Interface Form that serves as a       ***
	'***    holder for the different Function Forms.                    ***
	'***    Timer routine to check form size and alignment, to keep     ***
	'***    Background Synchronized with the MDIMain form.              ***
	'***  2.1.0                                                         ***
	'***    Sub Timer1_timer                                            ***
	'***    Removed the commented out lines                             ***
	'**********************************************************************
	
	
	
	
	Private Sub MDIMain_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
		
		Width = VB6.TwipsToPixelsX(11010) ' Set width of form.
        Height = VB6.TwipsToPixelsY(9000) ' Set height of form.
		Left = VB6.TwipsToPixelsX((VB6.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) - VB6.PixelsToTwipsX(Width)) / 2) ' Center form horizontally.
		Top = VB6.TwipsToPixelsY((VB6.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) - VB6.PixelsToTwipsY(Height)) / 2) ' Center form vertically.

        Main()
		
	End Sub
	
	
	
	Private Sub MDIMain_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
		End
	End Sub
	
	
	Public Sub mnuAbout_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuAbout.Click
		
		Call ShowAbout() 'Show About Form
		
	End Sub
	
	
	Public Sub mnuExit_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuExit.Click
		
		Me.Close()
		
	End Sub
	
	
	Private Sub Timer1_Tick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Timer1.Tick
		
		If WindowState = 2 Then
			iMax = iMax + 1
			If iMax = 2 Then
				WindowState = System.Windows.Forms.FormWindowState.Normal
				Width = VB6.TwipsToPixelsX(11010) ' Set width of form.
				Height = VB6.TwipsToPixelsY(7400) ' Set height of form.
				Left = VB6.TwipsToPixelsX((VB6.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) - VB6.PixelsToTwipsX(Width)) / 2) ' Center form horizontally.
				Top = VB6.TwipsToPixelsY((VB6.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) - VB6.PixelsToTwipsY(Height)) / 2) ' Center form vertically.
			Else
				WindowState = System.Windows.Forms.FormWindowState.Normal
				Me.Width = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width
				Me.Height = VB6.TwipsToPixelsY((VB6.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) - 1250))
				Me.Top = 0
				Me.Left = 0
			End If
			If iMax = 2 Then
				iMax = 0
			End If
		End If

		
	End Sub
	
	
	Sub ResizeMDI()
		
		On Error Resume Next
		
		If VB6.PixelsToTwipsX(Me.Width) < 11010 Then 'Restrict Form's minimum Width
			Me.Width = VB6.TwipsToPixelsX(11010)
		End If
		If VB6.PixelsToTwipsY(Me.Height) < 7400 Then 'Restrict Form's minimum Height
			Me.Height = VB6.TwipsToPixelsY(7400)
		End If 'Restrict Form's maximum Height
		If VB6.PixelsToTwipsY(Me.Height) > (VB6.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) - 940) Then
			Me.Height = VB6.TwipsToPixelsY((VB6.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) - 940))
		End If
		
	End Sub
	
	
	Sub ResizeTextBox()
		
		If VB6.PixelsToTwipsX(Me.Width) > 11010 Then
			
			frmViewer.ssfraSystem.Width = 10455 + (VB6.PixelsToTwipsX(Me.Width) - 11010)
			
            frmViewer.ssfraMeasurement.Width = 10455 + (VB6.PixelsToTwipsX(Me.Width) - 11010)
			
			frmViewer.ssfraComment.Width = 10455 + (VB6.PixelsToTwipsX(Me.Width) - 11010)
			frmViewer.txtData(10).Width = VB6.TwipsToPixelsX(3720 + (VB6.PixelsToTwipsX(Me.Width) - 11010))
			frmViewer.txtComments.Width = VB6.TwipsToPixelsX(10255 + (VB6.PixelsToTwipsX(Me.Width) - 11010))
		Else
			
			frmViewer.ssfraSystem.Width = 10455
			
            frmViewer.ssfraMeasurement.Width = 10455
			
			frmViewer.ssfraComment.Width = 10455
			frmViewer.txtData(10).Width = VB6.TwipsToPixelsX(3720)
			frmViewer.txtComments.Width = VB6.TwipsToPixelsX(10255)
		End If
		
	End Sub
	
	
	Public Sub mnuHelp_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuHelp.Click
		Call StatusMsg("FHDB Help.")
	End Sub
End Class