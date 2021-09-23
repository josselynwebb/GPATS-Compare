<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmAbout
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
	Public WithEvents cmdOK As System.Windows.Forms.Button
    Public WithEvents panTitle As System.Windows.Forms.Panel
    Public WithEvents lblAttributeVersion As System.Windows.Forms.Label
    Public WithEvents lblAttributeSystem As System.Windows.Forms.Label
    Public WithEvents lblInstrumentSystem As System.Windows.Forms.Label
    Public WithEvents lblInstrumentVersion As System.Windows.Forms.Label
    Public WithEvents SSPanel1 As System.Windows.Forms.Panel
    Public WithEvents Image1 As System.Windows.Forms.PictureBox
    Public WithEvents lblAttribute As System.Windows.Forms.Label
    Public WithEvents lblInstrument As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAbout))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdOK = New System.Windows.Forms.Button()
        Me.panTitle = New System.Windows.Forms.Panel()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.SSPanel1 = New System.Windows.Forms.Panel()
        Me.lblAttributeVersion = New System.Windows.Forms.Label()
        Me.lblAttributeSystem = New System.Windows.Forms.Label()
        Me.lblInstrumentSystem = New System.Windows.Forms.Label()
        Me.lblInstrumentVersion = New System.Windows.Forms.Label()
        Me.lblAttribute = New System.Windows.Forms.Label()
        Me.lblInstrument = New System.Windows.Forms.Label()
        Me.sbrInformation = New System.Windows.Forms.StatusStrip()
        Me.tsStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Image1 = New System.Windows.Forms.PictureBox()
        Me.panTitle.SuspendLayout()
        Me.SSPanel1.SuspendLayout()
        Me.sbrInformation.SuspendLayout()
        CType(Me.Image1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdOK
        '
        Me.cmdOK.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOK.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOK.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdOK.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOK.Location = New System.Drawing.Point(131, 89)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOK.Size = New System.Drawing.Size(75, 23)
        Me.cmdOK.TabIndex = 10
        Me.cmdOK.Text = "OK"
        Me.cmdOK.UseVisualStyleBackColor = False
        '
        'panTitle
        '
        Me.panTitle.AutoSize = True
        Me.panTitle.Controls.Add(Me.lblTitle)
        Me.panTitle.Location = New System.Drawing.Point(48, 4)
        Me.panTitle.Name = "panTitle"
        Me.panTitle.Size = New System.Drawing.Size(267, 32)
        Me.panTitle.TabIndex = 0
        '
        'lblTitle
        '
        Me.lblTitle.Enabled = False
        Me.lblTitle.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitle.Location = New System.Drawing.Point(0, 0)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(264, 32)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "Video Display Tools"
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'SSPanel1
        '
        Me.SSPanel1.Controls.Add(Me.lblAttributeVersion)
        Me.SSPanel1.Controls.Add(Me.lblAttributeSystem)
        Me.SSPanel1.Controls.Add(Me.lblInstrumentSystem)
        Me.SSPanel1.Controls.Add(Me.lblInstrumentVersion)
        Me.SSPanel1.Location = New System.Drawing.Point(8, 40)
        Me.SSPanel1.Name = "SSPanel1"
        Me.SSPanel1.Size = New System.Drawing.Size(307, 43)
        Me.SSPanel1.TabIndex = 1
        '
        'lblAttributeVersion
        '
        Me.lblAttributeVersion.AutoSize = True
        Me.lblAttributeVersion.BackColor = System.Drawing.SystemColors.Control
        Me.lblAttributeVersion.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAttributeVersion.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAttributeVersion.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAttributeVersion.Location = New System.Drawing.Point(8, 17)
        Me.lblAttributeVersion.Name = "lblAttributeVersion"
        Me.lblAttributeVersion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAttributeVersion.Size = New System.Drawing.Size(47, 14)
        Me.lblAttributeVersion.TabIndex = 8
        Me.lblAttributeVersion.Text = "Version:"
        '
        'lblAttributeSystem
        '
        Me.lblAttributeSystem.AutoSize = True
        Me.lblAttributeSystem.BackColor = System.Drawing.SystemColors.Control
        Me.lblAttributeSystem.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAttributeSystem.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAttributeSystem.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAttributeSystem.Location = New System.Drawing.Point(8, 1)
        Me.lblAttributeSystem.Name = "lblAttributeSystem"
        Me.lblAttributeSystem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAttributeSystem.Size = New System.Drawing.Size(46, 14)
        Me.lblAttributeSystem.TabIndex = 7
        Me.lblAttributeSystem.Text = "System:"
        '
        'lblInstrumentSystem
        '
        Me.lblInstrumentSystem.BackColor = System.Drawing.SystemColors.Control
        Me.lblInstrumentSystem.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblInstrumentSystem.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInstrumentSystem.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblInstrumentSystem.Location = New System.Drawing.Point(92, 1)
        Me.lblInstrumentSystem.Name = "lblInstrumentSystem"
        Me.lblInstrumentSystem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblInstrumentSystem.Size = New System.Drawing.Size(212, 17)
        Me.lblInstrumentSystem.TabIndex = 5
        Me.lblInstrumentSystem.Text = "GPATS"
        '
        'lblInstrumentVersion
        '
        Me.lblInstrumentVersion.BackColor = System.Drawing.SystemColors.Control
        Me.lblInstrumentVersion.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblInstrumentVersion.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInstrumentVersion.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblInstrumentVersion.Location = New System.Drawing.Point(92, 17)
        Me.lblInstrumentVersion.Name = "lblInstrumentVersion"
        Me.lblInstrumentVersion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblInstrumentVersion.Size = New System.Drawing.Size(212, 15)
        Me.lblInstrumentVersion.TabIndex = 2
        Me.lblInstrumentVersion.Text = "App. Version"
        '
        'lblAttribute
        '
        Me.lblAttribute.Location = New System.Drawing.Point(0, 0)
        Me.lblAttribute.Name = "lblAttribute"
        Me.lblAttribute.Size = New System.Drawing.Size(100, 23)
        Me.lblAttribute.TabIndex = 0
        '
        'lblInstrument
        '
        Me.lblInstrument.Location = New System.Drawing.Point(0, 0)
        Me.lblInstrument.Name = "lblInstrument"
        Me.lblInstrument.Size = New System.Drawing.Size(100, 23)
        Me.lblInstrument.TabIndex = 0
        '
        'sbrInformation
        '
        Me.sbrInformation.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsStatusLabel1})
        Me.sbrInformation.Location = New System.Drawing.Point(0, 125)
        Me.sbrInformation.Name = "sbrInformation"
        Me.sbrInformation.Size = New System.Drawing.Size(324, 22)
        Me.sbrInformation.TabIndex = 12
        Me.sbrInformation.Text = "StatusStrip1"
        '
        'tsStatusLabel1
        '
        Me.tsStatusLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tsStatusLabel1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.tsStatusLabel1.Name = "tsStatusLabel1"
        Me.tsStatusLabel1.Size = New System.Drawing.Size(0, 17)
        Me.tsStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Image1
        '
        Me.Image1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.Image1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Image1.Image = CType(resources.GetObject("Image1.Image"), System.Drawing.Image)
        Me.Image1.Location = New System.Drawing.Point(8, 4)
        Me.Image1.Name = "Image1"
        Me.Image1.Size = New System.Drawing.Size(32, 32)
        Me.Image1.TabIndex = 11
        Me.Image1.TabStop = False
        '
        'frmAbout
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(324, 147)
        Me.ControlBox = False
        Me.Controls.Add(Me.sbrInformation)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.panTitle)
        Me.Controls.Add(Me.SSPanel1)
        Me.Controls.Add(Me.Image1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Location = New System.Drawing.Point(95, 225)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAbout"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "SAIS"
        Me.panTitle.ResumeLayout(False)
        Me.SSPanel1.ResumeLayout(False)
        Me.SSPanel1.PerformLayout()
        Me.sbrInformation.ResumeLayout(False)
        Me.sbrInformation.PerformLayout()
        CType(Me.Image1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

End Sub
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents sbrInformation As System.Windows.Forms.StatusStrip
    Friend WithEvents tsStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
#End Region 
End Class