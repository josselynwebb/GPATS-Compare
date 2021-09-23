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
    Public WithEvents Image1 As System.Windows.Forms.PictureBox
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAbout))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdOK = New System.Windows.Forms.Button()
        Me.Image1 = New System.Windows.Forms.PictureBox()
        Me.panTitle = New System.Windows.Forms.Label()
        Me.panSais = New System.Windows.Forms.Panel()
        Me._lblInstrument_2 = New System.Windows.Forms.Label()
        Me._lblAttribute_3 = New System.Windows.Forms.Label()
        Me._lblAttribute_0 = New System.Windows.Forms.Label()
        Me._lblAttribute_1 = New System.Windows.Forms.Label()
        Me._lblInstrument_0 = New System.Windows.Forms.Label()
        Me._lblInstrument_1 = New System.Windows.Forms.Label()
        Me._lblAttribute_2 = New System.Windows.Forms.Label()
        Me._lblInstrument_3 = New System.Windows.Forms.Label()
        CType(Me.Image1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panSais.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdOK
        '
        Me.cmdOK.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOK.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOK.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdOK.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOK.Location = New System.Drawing.Point(174, 120)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOK.Size = New System.Drawing.Size(75, 23)
        Me.cmdOK.TabIndex = 10
        Me.cmdOK.Text = "OK"
        Me.cmdOK.UseVisualStyleBackColor = False
        '
        'Image1
        '
        Me.Image1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Image1.Image = CType(resources.GetObject("Image1.Image"), System.Drawing.Image)
        Me.Image1.Location = New System.Drawing.Point(8, 4)
        Me.Image1.Name = "Image1"
        Me.Image1.Size = New System.Drawing.Size(32, 32)
        Me.Image1.TabIndex = 12
        Me.Image1.TabStop = False
        '
        'panTitle
        '
        Me.panTitle.AutoSize = True
        Me.panTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panTitle.ForeColor = System.Drawing.Color.Navy
        Me.panTitle.Location = New System.Drawing.Point(48, 8)
        Me.panTitle.Name = "panTitle"
        Me.panTitle.Size = New System.Drawing.Size(189, 20)
        Me.panTitle.TabIndex = 13
        Me.panTitle.Text = "Digitizing Oscilloscope"
        '
        'panSais
        '
        Me.panSais.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panSais.Controls.Add(Me._lblInstrument_2)
        Me.panSais.Controls.Add(Me._lblAttribute_3)
        Me.panSais.Controls.Add(Me._lblAttribute_0)
        Me.panSais.Controls.Add(Me._lblAttribute_1)
        Me.panSais.Controls.Add(Me._lblInstrument_0)
        Me.panSais.Controls.Add(Me._lblInstrument_1)
        Me.panSais.Controls.Add(Me._lblAttribute_2)
        Me.panSais.Controls.Add(Me._lblInstrument_3)
        Me.panSais.Location = New System.Drawing.Point(8, 40)
        Me.panSais.Name = "panSais"
        Me.panSais.Size = New System.Drawing.Size(241, 66)
        Me.panSais.TabIndex = 14
        '
        '_lblInstrument_2
        '
        Me._lblInstrument_2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me._lblInstrument_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblInstrument_2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblInstrument_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblInstrument_2.Location = New System.Drawing.Point(100, 32)
        Me._lblInstrument_2.Name = "_lblInstrument_2"
        Me._lblInstrument_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblInstrument_2.Size = New System.Drawing.Size(133, 17)
        Me._lblInstrument_2.TabIndex = 17
        Me._lblInstrument_2.Text = "lblInstrument Information"
        '
        '_lblAttribute_3
        '
        Me._lblAttribute_3.AutoSize = True
        Me._lblAttribute_3.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me._lblAttribute_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblAttribute_3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblAttribute_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblAttribute_3.Location = New System.Drawing.Point(4, 48)
        Me._lblAttribute_3.Name = "_lblAttribute_3"
        Me._lblAttribute_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblAttribute_3.Size = New System.Drawing.Size(72, 13)
        Me._lblAttribute_3.TabIndex = 16
        Me._lblAttribute_3.Text = "SAIS Version:"
        '
        '_lblAttribute_0
        '
        Me._lblAttribute_0.AutoSize = True
        Me._lblAttribute_0.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me._lblAttribute_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblAttribute_0.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblAttribute_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblAttribute_0.Location = New System.Drawing.Point(4, 0)
        Me._lblAttribute_0.Name = "_lblAttribute_0"
        Me._lblAttribute_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblAttribute_0.Size = New System.Drawing.Size(73, 13)
        Me._lblAttribute_0.TabIndex = 15
        Me._lblAttribute_0.Text = "Manufacturer:"
        '
        '_lblAttribute_1
        '
        Me._lblAttribute_1.AutoSize = True
        Me._lblAttribute_1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me._lblAttribute_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblAttribute_1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblAttribute_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblAttribute_1.Location = New System.Drawing.Point(4, 16)
        Me._lblAttribute_1.Name = "_lblAttribute_1"
        Me._lblAttribute_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblAttribute_1.Size = New System.Drawing.Size(39, 13)
        Me._lblAttribute_1.TabIndex = 14
        Me._lblAttribute_1.Text = "Model:"
        '
        '_lblInstrument_0
        '
        Me._lblInstrument_0.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me._lblInstrument_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblInstrument_0.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblInstrument_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblInstrument_0.Location = New System.Drawing.Point(100, 0)
        Me._lblInstrument_0.Name = "_lblInstrument_0"
        Me._lblInstrument_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblInstrument_0.Size = New System.Drawing.Size(133, 17)
        Me._lblInstrument_0.TabIndex = 13
        Me._lblInstrument_0.Text = "lblInstrument Information"
        '
        '_lblInstrument_1
        '
        Me._lblInstrument_1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me._lblInstrument_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblInstrument_1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblInstrument_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblInstrument_1.Location = New System.Drawing.Point(100, 16)
        Me._lblInstrument_1.Name = "_lblInstrument_1"
        Me._lblInstrument_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblInstrument_1.Size = New System.Drawing.Size(133, 17)
        Me._lblInstrument_1.TabIndex = 12
        Me._lblInstrument_1.Text = "lblInstrument Information"
        '
        '_lblAttribute_2
        '
        Me._lblAttribute_2.AutoSize = True
        Me._lblAttribute_2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me._lblAttribute_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblAttribute_2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblAttribute_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblAttribute_2.Location = New System.Drawing.Point(4, 32)
        Me._lblAttribute_2.Name = "_lblAttribute_2"
        Me._lblAttribute_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblAttribute_2.Size = New System.Drawing.Size(87, 13)
        Me._lblAttribute_2.TabIndex = 11
        Me._lblAttribute_2.Text = "Resource Name:"
        '
        '_lblInstrument_3
        '
        Me._lblInstrument_3.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me._lblInstrument_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblInstrument_3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblInstrument_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblInstrument_3.Location = New System.Drawing.Point(100, 48)
        Me._lblInstrument_3.Name = "_lblInstrument_3"
        Me._lblInstrument_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblInstrument_3.Size = New System.Drawing.Size(133, 15)
        Me._lblInstrument_3.TabIndex = 10
        Me._lblInstrument_3.Text = "lblInstrument Information"
        '
        'frmAbout
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(255, 154)
        Me.ControlBox = False
        Me.Controls.Add(Me.panSais)
        Me.Controls.Add(Me.panTitle)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.Image1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Location = New System.Drawing.Point(348, 191)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAbout"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "SAIS"
        CType(Me.Image1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panSais.ResumeLayout(False)
        Me.panSais.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

End Sub
 Friend WithEvents panTitle As System.Windows.Forms.Label
 Friend WithEvents panSais As System.Windows.Forms.Panel
 Public WithEvents _lblInstrument_2 As System.Windows.Forms.Label
 Public WithEvents _lblAttribute_3 As System.Windows.Forms.Label
 Public WithEvents _lblAttribute_0 As System.Windows.Forms.Label
 Public WithEvents _lblAttribute_1 As System.Windows.Forms.Label
 Public WithEvents _lblInstrument_0 As System.Windows.Forms.Label
 Public WithEvents _lblInstrument_1 As System.Windows.Forms.Label
 Public WithEvents _lblAttribute_2 As System.Windows.Forms.Label
 Public WithEvents _lblInstrument_3 As System.Windows.Forms.Label
#End Region 
End Class