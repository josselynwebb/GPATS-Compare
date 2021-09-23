<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmMain
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
	Public WithEvents cmdClose As System.Windows.Forms.Button
	Public WithEvents cmdAbout As System.Windows.Forms.Button
	Public WithEvents cmdAcquire As System.Windows.Forms.Button
    Public WithEvents txtInstructions As System.Windows.Forms.TextBox
    Public WithEvents pnlDisplay As System.Windows.Forms.Panel
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.cmdAbout = New System.Windows.Forms.Button()
        Me.cmdAcquire = New System.Windows.Forms.Button()
        Me.txtInstructions = New System.Windows.Forms.TextBox()
        Me.pnlDisplay = New System.Windows.Forms.Panel()
        Me.pboxDisplay = New System.Windows.Forms.PictureBox()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.tsStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.pnlDisplay.SuspendLayout()
        CType(Me.pboxDisplay, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Location = New System.Drawing.Point(472, 301)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(73, 25)
        Me.cmdClose.TabIndex = 6
        Me.cmdClose.Text = "&Close"
        Me.cmdClose.UseVisualStyleBackColor = False
        '
        'cmdAbout
        '
        Me.cmdAbout.BackColor = System.Drawing.SystemColors.Control
        Me.cmdAbout.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdAbout.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAbout.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAbout.Location = New System.Drawing.Point(373, 301)
        Me.cmdAbout.Name = "cmdAbout"
        Me.cmdAbout.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAbout.Size = New System.Drawing.Size(73, 25)
        Me.cmdAbout.TabIndex = 5
        Me.cmdAbout.Text = "&About"
        Me.cmdAbout.UseVisualStyleBackColor = False
        '
        'cmdAcquire
        '
        Me.cmdAcquire.BackColor = System.Drawing.SystemColors.Control
        Me.cmdAcquire.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdAcquire.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAcquire.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAcquire.Location = New System.Drawing.Point(269, 301)
        Me.cmdAcquire.Name = "cmdAcquire"
        Me.cmdAcquire.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAcquire.Size = New System.Drawing.Size(73, 25)
        Me.cmdAcquire.TabIndex = 4
        Me.cmdAcquire.Text = "Aquire"
        Me.cmdAcquire.UseVisualStyleBackColor = False
        '
        'txtInstructions
        '
        Me.txtInstructions.AcceptsReturn = True
        Me.txtInstructions.BackColor = System.Drawing.SystemColors.Window
        Me.txtInstructions.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtInstructions.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInstructions.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtInstructions.Location = New System.Drawing.Point(13, 229)
        Me.txtInstructions.MaxLength = 0
        Me.txtInstructions.Multiline = True
        Me.txtInstructions.Name = "txtInstructions"
        Me.txtInstructions.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtInstructions.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtInstructions.Size = New System.Drawing.Size(529, 55)
        Me.txtInstructions.TabIndex = 1
        Me.txtInstructions.Visible = False
        '
        'pnlDisplay
        '
        Me.pnlDisplay.AutoScroll = True
        Me.pnlDisplay.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.pnlDisplay.Controls.Add(Me.pboxDisplay)
        Me.pnlDisplay.Cursor = System.Windows.Forms.Cursors.Default
        Me.pnlDisplay.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlDisplay.ForeColor = System.Drawing.SystemColors.ControlText
        Me.pnlDisplay.Location = New System.Drawing.Point(0, 0)
        Me.pnlDisplay.Name = "pnlDisplay"
        Me.pnlDisplay.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.pnlDisplay.Size = New System.Drawing.Size(477, 214)
        Me.pnlDisplay.TabIndex = 0
        '
        'pboxDisplay
        '
        Me.pboxDisplay.Location = New System.Drawing.Point(0, 0)
        Me.pboxDisplay.Name = "pboxDisplay"
        Me.pboxDisplay.Size = New System.Drawing.Size(411, 147)
        Me.pboxDisplay.TabIndex = 0
        Me.pboxDisplay.TabStop = False
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsStatusLabel})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 332)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(565, 22)
        Me.StatusStrip1.TabIndex = 34
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'tsStatusLabel
        '
        Me.tsStatusLabel.Name = "tsStatusLabel"
        Me.tsStatusLabel.Size = New System.Drawing.Size(76, 17)
        Me.tsStatusLabel.Text = "tsStatusLabel"
        Me.tsStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(565, 354)
        Me.Controls.Add(Me.cmdAcquire)
        Me.Controls.Add(Me.cmdAbout)
        Me.Controls.Add(Me.cmdClose)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.txtInstructions)
        Me.Controls.Add(Me.pnlDisplay)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(10, 10)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMain"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Video Capture Display"
        Me.pnlDisplay.ResumeLayout(False)
        CType(Me.pboxDisplay, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

End Sub
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents tsStatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents pboxDisplay As System.Windows.Forms.PictureBox
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
#End Region
End Class