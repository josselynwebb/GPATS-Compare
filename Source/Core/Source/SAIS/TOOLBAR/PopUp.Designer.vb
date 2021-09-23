<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmPopUp
#Region "Windows Form Designer generated code "
	<System.Diagnostics.DebuggerNonUserCode()> Public Sub New()
		MyBase.New()
		'This call is required by the Windows Form Designer.
		InitializeComponent()
	End Sub
	'Form overrides dispose to clean up the component list.
	<System.Diagnostics.DebuggerNonUserCode()> Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
		If Disposing Then
			If Not components Is Nothing Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(Disposing)
	End Sub
	'Required by the Windows Form Designer
	Private components As System.ComponentModel.IContainer
	Public ToolTip1 As System.Windows.Forms.ToolTip
	Public WithEvents mnuDefault As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents mnuDivider As System.Windows.Forms.ToolStripSeparator
	Public WithEvents mnuLarge As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents mnuSmall As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents mnuDivider3 As System.Windows.Forms.ToolStripSeparator
	Public WithEvents mnuOrientationHorizontal As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents mnuOrientationVertical As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents mnuDivider1 As System.Windows.Forms.ToolStripSeparator
	Public WithEvents mnuTopRight As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents mnuCenter As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents mnuAlignTopLeft As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents mnuDivider2 As System.Windows.Forms.ToolStripSeparator
	Public WithEvents mnuSave As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents mnuAbout As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents mnuDivider5 As System.Windows.Forms.ToolStripSeparator
	Public WithEvents mnuExitSAIS As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents mnuBar As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents MainMenu1 As System.Windows.Forms.MenuStrip
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
		Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmPopUp))
		Me.components = New System.ComponentModel.Container()
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(components)
		Me.MainMenu1 = New System.Windows.Forms.MenuStrip
		Me.mnuBar = New System.Windows.Forms.ToolStripMenuItem
		Me.mnuDefault = New System.Windows.Forms.ToolStripMenuItem
		Me.mnuDivider = New System.Windows.Forms.ToolStripSeparator
		Me.mnuLarge = New System.Windows.Forms.ToolStripMenuItem
		Me.mnuSmall = New System.Windows.Forms.ToolStripMenuItem
		Me.mnuDivider3 = New System.Windows.Forms.ToolStripSeparator
		Me.mnuOrientationHorizontal = New System.Windows.Forms.ToolStripMenuItem
		Me.mnuOrientationVertical = New System.Windows.Forms.ToolStripMenuItem
		Me.mnuDivider1 = New System.Windows.Forms.ToolStripSeparator
		Me.mnuTopRight = New System.Windows.Forms.ToolStripMenuItem
		Me.mnuCenter = New System.Windows.Forms.ToolStripMenuItem
		Me.mnuAlignTopLeft = New System.Windows.Forms.ToolStripMenuItem
		Me.mnuDivider2 = New System.Windows.Forms.ToolStripSeparator
		Me.mnuSave = New System.Windows.Forms.ToolStripMenuItem
		Me.mnuAbout = New System.Windows.Forms.ToolStripMenuItem
		Me.mnuDivider5 = New System.Windows.Forms.ToolStripSeparator
		Me.mnuExitSAIS = New System.Windows.Forms.ToolStripMenuItem
		Me.MainMenu1.SuspendLayout()
		Me.SuspendLayout()
		Me.ToolTip1.Active = True
		Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
		Me.Text = "Pop Up Menu Container"
		Me.ClientSize = New System.Drawing.Size(237, 84)
		Me.Location = New System.Drawing.Point(297, 442)
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.ShowInTaskbar = False
		Me.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.SystemColors.Control
		Me.ControlBox = True
		Me.Enabled = True
		Me.KeyPreview = False
		Me.Cursor = System.Windows.Forms.Cursors.Default
		Me.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.HelpButton = False
		Me.WindowState = System.Windows.Forms.FormWindowState.Normal
		Me.Name = "frmPopUp"
		Me.mnuBar.Name = "mnuBar"
		Me.mnuBar.Text = "PopUpMenu"
		Me.mnuBar.Checked = False
		Me.mnuBar.Enabled = True
		Me.mnuBar.Visible = True
		Me.mnuDefault.Name = "mnuDefault"
		Me.mnuDefault.Text = "Set Defaults"
		Me.mnuDefault.Checked = False
		Me.mnuDefault.Enabled = True
		Me.mnuDefault.Visible = True
		Me.mnuDivider.Enabled = True
		Me.mnuDivider.Visible = True
		Me.mnuDivider.Name = "mnuDivider"
		Me.mnuLarge.Name = "mnuLarge"
		Me.mnuLarge.Text = "Large Buttons"
		Me.mnuLarge.Checked = True
		Me.mnuLarge.Enabled = True
		Me.mnuLarge.Visible = True
		Me.mnuSmall.Name = "mnuSmall"
		Me.mnuSmall.Text = "Small Buttons"
		Me.mnuSmall.Checked = False
		Me.mnuSmall.Enabled = True
		Me.mnuSmall.Visible = True
		Me.mnuDivider3.Enabled = True
		Me.mnuDivider3.Visible = True
		Me.mnuDivider3.Name = "mnuDivider3"
		Me.mnuOrientationHorizontal.Name = "mnuOrientationHorizontal"
		Me.mnuOrientationHorizontal.Text = "Horizontal"
		Me.mnuOrientationHorizontal.Checked = True
		Me.mnuOrientationHorizontal.Enabled = True
		Me.mnuOrientationHorizontal.Visible = True
		Me.mnuOrientationVertical.Name = "mnuOrientationVertical"
		Me.mnuOrientationVertical.Text = "Vertical"
		Me.mnuOrientationVertical.Checked = False
		Me.mnuOrientationVertical.Enabled = True
		Me.mnuOrientationVertical.Visible = True
		Me.mnuDivider1.Enabled = True
		Me.mnuDivider1.Visible = True
		Me.mnuDivider1.Name = "mnuDivider1"
		Me.mnuTopRight.Name = "mnuTopRight"
		Me.mnuTopRight.Text = "Align Right"
		Me.mnuTopRight.Checked = False
		Me.mnuTopRight.Enabled = True
		Me.mnuTopRight.Visible = True
		Me.mnuCenter.Name = "mnuCenter"
		Me.mnuCenter.Text = "Align Center"
		Me.mnuCenter.Checked = False
		Me.mnuCenter.Enabled = True
		Me.mnuCenter.Visible = True
		Me.mnuAlignTopLeft.Name = "mnuAlignTopLeft"
		Me.mnuAlignTopLeft.Text = "Align Left"
		Me.mnuAlignTopLeft.Checked = False
		Me.mnuAlignTopLeft.Enabled = True
		Me.mnuAlignTopLeft.Visible = True
		Me.mnuDivider2.Enabled = True
		Me.mnuDivider2.Visible = True
		Me.mnuDivider2.Name = "mnuDivider2"
		Me.mnuSave.Name = "mnuSave"
		Me.mnuSave.Text = "Save Settings"
		Me.mnuSave.Checked = False
		Me.mnuSave.Enabled = True
		Me.mnuSave.Visible = True
		Me.mnuAbout.Name = "mnuAbout"
		Me.mnuAbout.Text = "About SAIS"
		Me.mnuAbout.Checked = False
		Me.mnuAbout.Enabled = True
		Me.mnuAbout.Visible = True
		Me.mnuDivider5.Enabled = True
		Me.mnuDivider5.Visible = True
		Me.mnuDivider5.Name = "mnuDivider5"
		Me.mnuExitSAIS.Name = "mnuExitSAIS"
		Me.mnuExitSAIS.Text = "Exit SAIS"
		Me.mnuExitSAIS.Checked = False
		Me.mnuExitSAIS.Enabled = True
		Me.mnuExitSAIS.Visible = True
		MainMenu1.Items.AddRange(New System.Windows.Forms.ToolStripItem(){Me.mnuBar})
		mnuBar.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem(){Me.mnuDefault, Me.mnuDivider, Me.mnuLarge, Me.mnuSmall, Me.mnuDivider3, Me.mnuOrientationHorizontal, Me.mnuOrientationVertical, Me.mnuDivider1, Me.mnuTopRight, Me.mnuCenter, Me.mnuAlignTopLeft, Me.mnuDivider2, Me.mnuSave, Me.mnuAbout, Me.mnuDivider5, Me.mnuExitSAIS})
		Me.Controls.Add(MainMenu1)
		Me.MainMenu1.ResumeLayout(False)
		Me.ResumeLayout(False)
		Me.PerformLayout()
	End Sub
#End Region 
End Class