Option Strict Off
Option Explicit On
Friend Class frmPopUp
	Inherits System.Windows.Forms.Form
	
	Public Sub mnuAbout_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuAbout.Click
		
		CenterForm(frmAbout)
		frmAbout.ShowDialog()
		
	End Sub
	
	
	Public Sub mnuAlignTopLeft_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuAlignTopLeft.Click
		
		frmToolBar.Left = VB6.TwipsToPixelsX(25)
		frmToolBar.Top = VB6.TwipsToPixelsY(25)
		
		frmToolBar.Refresh()
		
	End Sub
	
	
	Public Sub mnuCenter_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuCenter.Click
		
		CenterForm(frmToolBar)
		frmToolBar.Refresh()
		
	End Sub
	
	Public Sub mnuDefault_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuDefault.Click
		
        Me.mnuOrientationHorizontal_Click(eventSender, eventArgs)
		System.Windows.Forms.Application.DoEvents()
        Me.mnuTopRight_Click(eventSender, eventArgs)
		System.Windows.Forms.Application.DoEvents()
        Me.mnuLarge_Click(eventSender, eventArgs)
		System.Windows.Forms.Application.DoEvents()
		frmToolBar.Refresh()
		
	End Sub
	
	Public Sub mnuExitSAIS_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuExitSAIS.Click
		
		frmToolBar.Close()
		
	End Sub
	
	
	Public Sub mnuLarge_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuLarge.Click
		
		frmToolBar.tbrInstruments.Visible = True
		frmToolBar.tbrInstruments2.Visible = False
		
		LargeButtonSize = True
		
		AdjustToolbar()
		
		mnuLarge.Checked = True
		mnuSmall.Checked = False
		
		
		frmToolBar.Refresh()
		
	End Sub
	
	Public Sub mnuOrientationHorizontal_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuOrientationHorizontal.Click
		
		Horizontal = True
		AdjustToolbar()
		
		frmToolBar.Text = "SAIS (Stand Alone Instrument Software)"
		
		mnuOrientationHorizontal.Checked = True
		mnuOrientationVertical.Checked = False
		
		frmToolBar.Refresh()
		
	End Sub
	
	Public Sub mnuOrientationVertical_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuOrientationVertical.Click
		
		Horizontal = False
		AdjustToolbar()
		
		frmToolBar.Text = ""
		
		mnuOrientationHorizontal.Checked = False
		mnuOrientationVertical.Checked = True
		
		frmToolBar.Refresh()
		
	End Sub
	
	Public Sub mnuSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuSave.Click
		
		Dim ReturnValue As Integer
		Dim lpApplicationName As String
		Dim lpKeyName As String
		Dim lpString As String
		Dim lpFileName As String
		
		lpApplicationName = "SAIS"
		lpFileName = "C:\Users\Public\Documents\ATS\ATS.ini"
		
		lpKeyName = "SIZE"
		If LargeButtonSize = True Then
			lpString = "1"
		Else
			lpString = "2"
		End If
		ReturnValue = WritePrivateProfileString(lpApplicationName, lpKeyName, lpString, lpFileName)
		
		lpKeyName = "HV"
		If Horizontal = True Then
			lpString = "1"
		Else
			lpString = "2"
		End If
		ReturnValue = WritePrivateProfileString(lpApplicationName, lpKeyName, lpString, lpFileName)
		
	End Sub
	
	Public Sub mnuSmall_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuSmall.Click
		
		frmToolBar.tbrInstruments.Visible = False
		frmToolBar.tbrInstruments2.Visible = True
		
		LargeButtonSize = False
		
		AdjustToolbar()
		
		mnuLarge.Checked = False
		mnuSmall.Checked = True
		
		frmToolBar.Refresh()
		
	End Sub
	
	Public Sub mnuTopRight_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuTopRight.Click
		
        frmToolBar.Left = Screen.PrimaryScreen.Bounds.Right - (frmToolBar.Width) 'VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) - VB6.PixelsToTwipsX(frmToolBar.Width) - 25)
        frmToolBar.Top = Screen.PrimaryScreen.Bounds.Top + 15 'VB6.TwipsToPixelsY(25)
		
		frmToolBar.Refresh()
		
	End Sub
End Class