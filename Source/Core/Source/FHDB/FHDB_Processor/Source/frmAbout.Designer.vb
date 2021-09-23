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
    Public WithEvents _lblInstrument_1 As System.Windows.Forms.Label
	Public WithEvents _lblInstrument_0 As System.Windows.Forms.Label
	Public WithEvents _lblAttribute_1 As System.Windows.Forms.Label
	Public WithEvents _lblAttribute_0 As System.Windows.Forms.Label
    Public WithEvents Panel1 As Panel
    Public WithEvents cmdOk As Button
    Public WithEvents lblAttribute As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    Public WithEvents lblInstrument As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAbout))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip()
        Me.AppPicture = New System.Windows.Forms.PictureBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me._lblInstrument_1 = New System.Windows.Forms.Label()
        Me._lblInstrument_0 = New System.Windows.Forms.Label()
        Me._lblAttribute_1 = New System.Windows.Forms.Label()
        Me._lblAttribute_0 = New System.Windows.Forms.Label()
        Me.cmdOk = New System.Windows.Forms.Button()
        Me.lblAttribute = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray()
        Me.lblInstrument = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray()
        CType(Me.AppPicture, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.lblAttribute, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblInstrument, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'AppPicture
        '
        Me.AppPicture.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.AppPicture.Cursor = System.Windows.Forms.Cursors.Default
        Me.AppPicture.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AppPicture.ForeColor = System.Drawing.SystemColors.WindowText
        Me.AppPicture.Image = CType(resources.GetObject("AppPicture.Image"), System.Drawing.Image)
        Me.AppPicture.Location = New System.Drawing.Point(52, 66)
        Me.AppPicture.Name = "AppPicture"
        Me.AppPicture.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.AppPicture.Size = New System.Drawing.Size(32, 32)
        Me.AppPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.AppPicture.TabIndex = 0
        Me.AppPicture.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.Controls.Add(Me._lblInstrument_1)
        Me.Panel1.Controls.Add(Me._lblInstrument_0)
        Me.Panel1.Controls.Add(Me._lblAttribute_1)
        Me.Panel1.Controls.Add(Me._lblAttribute_0)
        Me.Panel1.Location = New System.Drawing.Point(4, 12)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(238, 48)
        Me.Panel1.TabIndex = 2
        '
        '_lblInstrument_1
        '
        Me._lblInstrument_1.BackColor = System.Drawing.SystemColors.Control
        Me._lblInstrument_1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me._lblInstrument_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblInstrument_1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblInstrument_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblInstrument.SetIndex(Me._lblInstrument_1, CType(1, Short))
        Me._lblInstrument_1.Location = New System.Drawing.Point(64, 26)
        Me._lblInstrument_1.Name = "_lblInstrument_1"
        Me._lblInstrument_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblInstrument_1.Size = New System.Drawing.Size(170, 17)
        Me._lblInstrument_1.TabIndex = 6
        Me._lblInstrument_1.Text = "App.Version"
        '
        '_lblInstrument_0
        '
        Me._lblInstrument_0.BackColor = System.Drawing.SystemColors.Control
        Me._lblInstrument_0.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me._lblInstrument_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblInstrument_0.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblInstrument_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblInstrument.SetIndex(Me._lblInstrument_0, CType(0, Short))
        Me._lblInstrument_0.Location = New System.Drawing.Point(65, 4)
        Me._lblInstrument_0.Name = "_lblInstrument_0"
        Me._lblInstrument_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblInstrument_0.Size = New System.Drawing.Size(169, 17)
        Me._lblInstrument_0.TabIndex = 5
        Me._lblInstrument_0.Text = "Portable Automated Tester"
        '
        '_lblAttribute_1
        '
        Me._lblAttribute_1.AutoSize = True
        Me._lblAttribute_1.BackColor = System.Drawing.SystemColors.Control
        Me._lblAttribute_1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me._lblAttribute_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblAttribute_1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblAttribute_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAttribute.SetIndex(Me._lblAttribute_1, CType(1, Short))
        Me._lblAttribute_1.Location = New System.Drawing.Point(8, 26)
        Me._lblAttribute_1.Name = "_lblAttribute_1"
        Me._lblAttribute_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblAttribute_1.Size = New System.Drawing.Size(49, 16)
        Me._lblAttribute_1.TabIndex = 4
        Me._lblAttribute_1.Text = "Version:"
        '
        '_lblAttribute_0
        '
        Me._lblAttribute_0.AutoSize = True
        Me._lblAttribute_0.BackColor = System.Drawing.SystemColors.Control
        Me._lblAttribute_0.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me._lblAttribute_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblAttribute_0.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblAttribute_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAttribute.SetIndex(Me._lblAttribute_0, CType(0, Short))
        Me._lblAttribute_0.Location = New System.Drawing.Point(9, 4)
        Me._lblAttribute_0.Name = "_lblAttribute_0"
        Me._lblAttribute_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblAttribute_0.Size = New System.Drawing.Size(48, 16)
        Me._lblAttribute_0.TabIndex = 3
        Me._lblAttribute_0.Text = "System:"
        '
        'cmdOk
        '
        Me.cmdOk.Location = New System.Drawing.Point(156, 73)
        Me.cmdOk.Name = "cmdOk"
        Me.cmdOk.Size = New System.Drawing.Size(82, 25)
        Me.cmdOk.TabIndex = 7
        Me.cmdOk.Text = "OK"
        Me.cmdOk.Visible = False
        '
        'frmAbout
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(250, 113)
        Me.ControlBox = False
        Me.Controls.Add(Me.AppPicture)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.cmdOk)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(3, 19)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAbout"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "About"
        CType(Me.AppPicture, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.lblAttribute, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblInstrument, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
#End Region 
End Class