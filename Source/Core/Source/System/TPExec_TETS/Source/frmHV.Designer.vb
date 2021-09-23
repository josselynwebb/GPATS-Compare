<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmHV
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
    Public WithEvents MouseControl_1 As System.Windows.Forms.Button
    Public WithEvents SplashTimer As System.Windows.Forms.Timer
    Public WithEvents Picture1 As System.Windows.Forms.Panel
    Public WithEvents Picture3 As System.Windows.Forms.PictureBox
    Public WithEvents Picture2 As System.Windows.Forms.PictureBox
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents MouseControl_0 As System.Windows.Forms.Button
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents SSPanel1 As System.Windows.Forms.Panel
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmHV))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.MouseControl_1 = New System.Windows.Forms.Button()
        Me.SSPanel1 = New System.Windows.Forms.Panel()
        Me.Picture1 = New System.Windows.Forms.Panel()
        Me.Picture3 = New System.Windows.Forms.PictureBox()
        Me.Picture2 = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.MouseControl_0 = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SplashTimer = New System.Windows.Forms.Timer(Me.components)
        Me.SSPanel1.SuspendLayout()
        Me.Picture1.SuspendLayout()
        CType(Me.Picture3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Picture2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MouseControl_1
        '
        Me.MouseControl_1.BackColor = System.Drawing.Color.Silver
        Me.MouseControl_1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MouseControl_1.ForeColor = System.Drawing.Color.Red
        Me.MouseControl_1.Location = New System.Drawing.Point(376, 368)
        Me.MouseControl_1.Name = "MouseControl_1"
        Me.MouseControl_1.Size = New System.Drawing.Size(124, 23)
        Me.MouseControl_1.TabIndex = 9
        Me.MouseControl_1.Text = "No"
        Me.MouseControl_1.UseVisualStyleBackColor = False
        '
        'SSPanel1
        '
        Me.SSPanel1.BackColor = System.Drawing.Color.LightGray
        Me.SSPanel1.Controls.Add(Me.Picture1)
        Me.SSPanel1.Controls.Add(Me.MouseControl_0)
        Me.SSPanel1.Controls.Add(Me.Label4)
        Me.SSPanel1.Controls.Add(Me.Label3)
        Me.SSPanel1.Controls.Add(Me.Label2)
        Me.SSPanel1.Location = New System.Drawing.Point(8, 8)
        Me.SSPanel1.Name = "SSPanel1"
        Me.SSPanel1.Size = New System.Drawing.Size(497, 393)
        Me.SSPanel1.TabIndex = 1
        Me.SSPanel1.Text = "SSPanel1"
        '
        'Picture1
        '
        Me.Picture1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Picture1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Picture1.Controls.Add(Me.Picture3)
        Me.Picture1.Controls.Add(Me.Picture2)
        Me.Picture1.Controls.Add(Me.Label1)
        Me.Picture1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Picture1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Picture1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Picture1.Location = New System.Drawing.Point(8, 8)
        Me.Picture1.Name = "Picture1"
        Me.Picture1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Picture1.Size = New System.Drawing.Size(481, 297)
        Me.Picture1.TabIndex = 4
        Me.Picture1.TabStop = True
        '
        'Picture3
        '
        Me.Picture3.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Picture3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Picture3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Picture3.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Picture3.Image = CType(resources.GetObject("Picture3.Image"), System.Drawing.Image)
        Me.Picture3.Location = New System.Drawing.Point(32, 184)
        Me.Picture3.Name = "Picture3"
        Me.Picture3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Picture3.Size = New System.Drawing.Size(425, 57)
        Me.Picture3.TabIndex = 6
        Me.Picture3.TabStop = False
        '
        'Picture2
        '
        Me.Picture2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Picture2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Picture2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Picture2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Picture2.Image = CType(resources.GetObject("Picture2.Image"), System.Drawing.Image)
        Me.Picture2.Location = New System.Drawing.Point(56, 16)
        Me.Picture2.Name = "Picture2"
        Me.Picture2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Picture2.Size = New System.Drawing.Size(361, 169)
        Me.Picture2.TabIndex = 5
        Me.Picture2.TabStop = False
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 13.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(16, 248)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(449, 25)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "*** UUT POWER IS ABOUT TO BE APPLIED ***"
        '
        'MouseControl_0
        '
        Me.MouseControl_0.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MouseControl_0.Location = New System.Drawing.Point(368, 336)
        Me.MouseControl_0.Name = "MouseControl_0"
        Me.MouseControl_0.Size = New System.Drawing.Size(124, 23)
        Me.MouseControl_0.TabIndex = 0
        Me.MouseControl_0.Text = "Yes"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.LightGray
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Red
        Me.Label4.Location = New System.Drawing.Point(374, 312)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(113, 25)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Acknowledge"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.LightGray
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(16, 360)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(313, 25)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Press ""Yes"" to continue.  ""No"" to exit."
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.LightGray
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(16, 312)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(337, 57)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Voltages dangerous to life will be present on the UUT/test fixture.  Observe all " & _
    "applicable safety precautions to avoid severe electrical shock."
        '
        'SplashTimer
        '
        Me.SplashTimer.Enabled = True
        '
        'frmHV
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Red
        Me.ClientSize = New System.Drawing.Size(511, 408)
        Me.Controls.Add(Me.MouseControl_1)
        Me.Controls.Add(Me.SSPanel1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmHV"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.Text = "Form1"
        Me.SSPanel1.ResumeLayout(False)
        Me.Picture1.ResumeLayout(False)
        CType(Me.Picture3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Picture2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

End Sub
#End Region 
End Class