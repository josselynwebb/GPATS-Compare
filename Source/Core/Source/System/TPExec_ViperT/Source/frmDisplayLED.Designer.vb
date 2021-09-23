<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmDisplayLED
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
	Public WithEvents PictureWindow As System.Windows.Forms.PictureBox
	Public WithEvents cmdTextBox As System.Windows.Forms.TextBox
	Public WithEvents cmdNO As System.Windows.Forms.Button
	Public WithEvents cmdYES As System.Windows.Forms.Button
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
		Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmDisplayLED))
		Me.components = New System.ComponentModel.Container()
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(components)
		Me.PictureWindow = New System.Windows.Forms.PictureBox
		Me.cmdTextBox = New System.Windows.Forms.TextBox
		Me.cmdNO = New System.Windows.Forms.Button
		Me.cmdYES = New System.Windows.Forms.Button
		Me.SuspendLayout()
		Me.ToolTip1.Active = True
		Me.Text = "LED Test"
		Me.ClientSize = New System.Drawing.Size(561, 409)
		Me.Location = New System.Drawing.Point(4, 30)
		Me.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultLocation
		Me.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.SystemColors.Control
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable
		Me.ControlBox = True
		Me.Enabled = True
		Me.KeyPreview = False
		Me.MaximizeBox = True
		Me.MinimizeBox = True
		Me.Cursor = System.Windows.Forms.Cursors.Default
		Me.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.ShowInTaskbar = True
		Me.HelpButton = False
		Me.WindowState = System.Windows.Forms.FormWindowState.Normal
		Me.Name = "frmDisplayLED"
		Me.PictureWindow.BackColor = System.Drawing.SystemColors.Window
		Me.PictureWindow.ForeColor = System.Drawing.SystemColors.WindowText
		Me.PictureWindow.Size = New System.Drawing.Size(561, 305)
		Me.PictureWindow.Location = New System.Drawing.Point(0, 0)
		Me.PictureWindow.TabIndex = 3
		Me.PictureWindow.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.PictureWindow.Dock = System.Windows.Forms.DockStyle.None
		Me.PictureWindow.CausesValidation = True
		Me.PictureWindow.Enabled = True
		Me.PictureWindow.Cursor = System.Windows.Forms.Cursors.Default
		Me.PictureWindow.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.PictureWindow.TabStop = True
		Me.PictureWindow.Visible = True
		Me.PictureWindow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Normal
		Me.PictureWindow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.PictureWindow.Name = "PictureWindow"
		Me.cmdTextBox.AutoSize = False
		Me.cmdTextBox.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdTextBox.Size = New System.Drawing.Size(449, 89)
		Me.cmdTextBox.Location = New System.Drawing.Point(104, 312)
		Me.cmdTextBox.ReadOnly = True
		Me.cmdTextBox.MultiLine = True
		Me.cmdTextBox.TabIndex = 2
		Me.cmdTextBox.AcceptsReturn = True
		Me.cmdTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.cmdTextBox.BackColor = System.Drawing.SystemColors.Window
		Me.cmdTextBox.CausesValidation = True
		Me.cmdTextBox.Enabled = True
		Me.cmdTextBox.ForeColor = System.Drawing.SystemColors.WindowText
		Me.cmdTextBox.HideSelection = True
		Me.cmdTextBox.Maxlength = 0
		Me.cmdTextBox.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.cmdTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.cmdTextBox.TabStop = True
		Me.cmdTextBox.Visible = True
		Me.cmdTextBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.cmdTextBox.Name = "cmdTextBox"
		Me.cmdNO.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.cmdNO.Size = New System.Drawing.Size(81, 33)
		Me.cmdNO.Location = New System.Drawing.Point(8, 360)
		Me.cmdNO.Image = CType(resources.GetObject("cmdNO.Image"), System.Drawing.Image)
		Me.cmdNO.TabIndex = 1
		Me.cmdNO.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdNO.BackColor = System.Drawing.SystemColors.Control
		Me.cmdNO.CausesValidation = True
		Me.cmdNO.Enabled = True
		Me.cmdNO.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdNO.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdNO.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdNO.TabStop = True
		Me.cmdNO.Name = "cmdNO"
		Me.cmdYES.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.AcceptButton = Me.cmdYES
		Me.cmdYES.Size = New System.Drawing.Size(81, 33)
		Me.cmdYES.Location = New System.Drawing.Point(8, 320)
		Me.cmdYES.Image = CType(resources.GetObject("cmdYES.Image"), System.Drawing.Image)
		Me.cmdYES.TabIndex = 0
		Me.cmdYES.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdYES.BackColor = System.Drawing.SystemColors.Control
		Me.cmdYES.CausesValidation = True
		Me.cmdYES.Enabled = True
		Me.cmdYES.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdYES.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdYES.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdYES.TabStop = True
		Me.cmdYES.Name = "cmdYES"
		Me.Controls.Add(PictureWindow)
		Me.Controls.Add(cmdTextBox)
		Me.Controls.Add(cmdNO)
		Me.Controls.Add(cmdYES)
		Me.ResumeLayout(False)
		Me.PerformLayout()
	End Sub
#End Region 
End Class