<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmHelp
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
	Public WithEvents cmdOk As System.Windows.Forms.Button
	Public WithEvents picHelp As System.Windows.Forms.PictureBox
	Public WithEvents imgLogo As System.Windows.Forms.PictureBox
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
		Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmHelp))
		Me.components = New System.ComponentModel.Container()
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(components)
		Me.cmdOk = New System.Windows.Forms.Button
		Me.picHelp = New System.Windows.Forms.PictureBox
		Me.imgLogo = New System.Windows.Forms.PictureBox
		Me.SuspendLayout()
		Me.ToolTip1.Active = True
		Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
		Me.Text = "Digitizing Oscilloscope Help"
		Me.ClientSize = New System.Drawing.Size(342, 300)
		Me.Location = New System.Drawing.Point(74, 233)
		Me.Icon = CType(resources.GetObject("frmHelp.Icon"), System.Drawing.Icon)
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.SystemColors.Control
		Me.ControlBox = True
		Me.Enabled = True
		Me.KeyPreview = False
		Me.Cursor = System.Windows.Forms.Cursors.Default
		Me.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.ShowInTaskbar = True
		Me.HelpButton = False
		Me.WindowState = System.Windows.Forms.FormWindowState.Normal
		Me.Name = "frmHelp"
		Me.cmdOk.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		Me.cmdOk.Text = "&Close"
		Me.cmdOk.Size = New System.Drawing.Size(75, 23)
		Me.cmdOk.Location = New System.Drawing.Point(260, 272)
		Me.cmdOk.TabIndex = 1
		Me.cmdOk.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdOk.BackColor = System.Drawing.SystemColors.Control
		Me.cmdOk.CausesValidation = True
		Me.cmdOk.Enabled = True
		Me.cmdOk.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdOk.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdOk.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdOk.TabStop = True
		Me.cmdOk.Name = "cmdOk"
		Me.picHelp.Size = New System.Drawing.Size(332, 254)
		Me.picHelp.Location = New System.Drawing.Point(5, 2)
		Me.picHelp.Image = CType(resources.GetObject("picHelp.Image"), System.Drawing.Image)
		Me.picHelp.TabIndex = 0
		Me.picHelp.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.picHelp.Dock = System.Windows.Forms.DockStyle.None
		Me.picHelp.BackColor = System.Drawing.SystemColors.Control
		Me.picHelp.CausesValidation = True
		Me.picHelp.Enabled = True
		Me.picHelp.ForeColor = System.Drawing.SystemColors.ControlText
		Me.picHelp.Cursor = System.Windows.Forms.Cursors.Default
		Me.picHelp.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.picHelp.TabStop = True
		Me.picHelp.Visible = True
		Me.picHelp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
		Me.picHelp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.picHelp.Name = "picHelp"
		Me.imgLogo.Size = New System.Drawing.Size(121, 33)
		Me.imgLogo.Location = New System.Drawing.Point(4, 260)
		Me.imgLogo.Image = CType(resources.GetObject("imgLogo.Image"), System.Drawing.Image)
		Me.imgLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
		Me.imgLogo.Enabled = True
		Me.imgLogo.Cursor = System.Windows.Forms.Cursors.Default
		Me.imgLogo.Visible = True
		Me.imgLogo.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.imgLogo.Name = "imgLogo"
		Me.Controls.Add(cmdOk)
		Me.Controls.Add(picHelp)
		Me.Controls.Add(imgLogo)
		Me.ResumeLayout(False)
		Me.PerformLayout()
	End Sub
#End Region 
End Class