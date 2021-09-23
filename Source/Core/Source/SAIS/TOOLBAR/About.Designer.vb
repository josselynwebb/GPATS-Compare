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
	Public WithEvents AppPicture As System.Windows.Forms.PictureBox
	Public WithEvents _lblAttribute_0 As System.Windows.Forms.Label
	Public WithEvents _lblAttribute_1 As System.Windows.Forms.Label
	Public WithEvents _lblInstrument_0 As System.Windows.Forms.Label
	Public WithEvents _lblInstrument_1 As System.Windows.Forms.Label
	Public WithEvents lblAttribute As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
	Public WithEvents lblInstrument As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAbout))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.AppPicture = New System.Windows.Forms.PictureBox()
        Me._lblAttribute_0 = New System.Windows.Forms.Label()
        Me._lblAttribute_1 = New System.Windows.Forms.Label()
        Me._lblInstrument_0 = New System.Windows.Forms.Label()
        Me._lblInstrument_1 = New System.Windows.Forms.Label()
        Me.lblAttribute = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.lblInstrument = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        CType(Me.AppPicture, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAttribute, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblInstrument, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'AppPicture
        '
        Me.AppPicture.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.AppPicture.Cursor = System.Windows.Forms.Cursors.Default
        Me.AppPicture.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AppPicture.ForeColor = System.Drawing.SystemColors.WindowText
        Me.AppPicture.Image = CType(resources.GetObject("AppPicture.Image"), System.Drawing.Image)
        Me.AppPicture.Location = New System.Drawing.Point(8, 8)
        Me.AppPicture.Name = "AppPicture"
        Me.AppPicture.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.AppPicture.Size = New System.Drawing.Size(32, 32)
        Me.AppPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.AppPicture.TabIndex = 7
        Me.AppPicture.TabStop = False
        '
        '_lblAttribute_0
        '
        Me._lblAttribute_0.AutoSize = True
        Me._lblAttribute_0.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me._lblAttribute_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblAttribute_0.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblAttribute_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAttribute.SetIndex(Me._lblAttribute_0, CType(0, Short))
        Me._lblAttribute_0.Location = New System.Drawing.Point(8, 1)
        Me._lblAttribute_0.Name = "_lblAttribute_0"
        Me._lblAttribute_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblAttribute_0.Size = New System.Drawing.Size(46, 14)
        Me._lblAttribute_0.TabIndex = 6
        Me._lblAttribute_0.Text = "System:"
        '
        '_lblAttribute_1
        '
        Me._lblAttribute_1.AutoSize = True
        Me._lblAttribute_1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me._lblAttribute_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblAttribute_1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblAttribute_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAttribute.SetIndex(Me._lblAttribute_1, CType(1, Short))
        Me._lblAttribute_1.Location = New System.Drawing.Point(8, 17)
        Me._lblAttribute_1.Name = "_lblAttribute_1"
        Me._lblAttribute_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblAttribute_1.Size = New System.Drawing.Size(47, 14)
        Me._lblAttribute_1.TabIndex = 5
        Me._lblAttribute_1.Text = "Version:"
        '
        '_lblInstrument_0
        '
        Me._lblInstrument_0.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me._lblInstrument_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblInstrument_0.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblInstrument_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblInstrument.SetIndex(Me._lblInstrument_0, CType(0, Short))
        Me._lblInstrument_0.Location = New System.Drawing.Point(68, 1)
        Me._lblInstrument_0.Name = "_lblInstrument_0"
        Me._lblInstrument_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblInstrument_0.Size = New System.Drawing.Size(169, 17)
        Me._lblInstrument_0.TabIndex = 4
        Me._lblInstrument_0.Text = "VIPERT"
        '
        '_lblInstrument_1
        '
        Me._lblInstrument_1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me._lblInstrument_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblInstrument_1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblInstrument_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblInstrument.SetIndex(Me._lblInstrument_1, CType(1, Short))
        Me._lblInstrument_1.Location = New System.Drawing.Point(68, 16)
        Me._lblInstrument_1.Name = "_lblInstrument_1"
        Me._lblInstrument_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblInstrument_1.Size = New System.Drawing.Size(133, 17)
        Me._lblInstrument_1.TabIndex = 3
        Me._lblInstrument_1.Text = "102.00"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(47, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(116, 20)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "SAIS Toolbar"
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Location = New System.Drawing.Point(8, 46)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(235, 48)
        Me.Panel1.TabIndex = 9
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(63, 26)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(40, 14)
        Me.Label5.TabIndex = 3
        Me.Label5.Text = "2.0.0.0"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(63, 7)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(59, 14)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "GPATS CIC"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(4, 26)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(45, 13)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Version:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(4, 7)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(44, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "System:"
        '
        'frmAbout
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(266, 137)
        Me.ControlBox = False
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.AppPicture)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(291, 123)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAbout"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "About"
        CType(Me.AppPicture, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAttribute, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblInstrument, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
#End Region 
End Class