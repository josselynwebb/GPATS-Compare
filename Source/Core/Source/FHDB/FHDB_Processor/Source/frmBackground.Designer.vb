<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmBackground
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
    Public WithEvents _stbMain_Panel1 As System.Windows.Forms.ToolStripStatusLabel
	Public WithEvents stbMain As System.Windows.Forms.StatusStrip
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.stbMain = New System.Windows.Forms.StatusStrip()
        Me._stbMain_Panel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.stbMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'stbMain
        '
        Me.stbMain.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.stbMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me._stbMain_Panel1})
        Me.stbMain.Location = New System.Drawing.Point(0, 546)
        Me.stbMain.Name = "stbMain"
        Me.stbMain.Size = New System.Drawing.Size(726, 22)
        Me.stbMain.TabIndex = 0
        '
        '_stbMain_Panel1
        '
        Me._stbMain_Panel1.AutoSize = False
        Me._stbMain_Panel1.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me._stbMain_Panel1.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me._stbMain_Panel1.Margin = New System.Windows.Forms.Padding(0)
        Me._stbMain_Panel1.Name = "_stbMain_Panel1"
        Me._stbMain_Panel1.Size = New System.Drawing.Size(690, 22)
        Me._stbMain_Panel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(141, 520)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(425, 23)
        Me.ProgressBar1.TabIndex = 2
        '
        'frmBackground
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(726, 568)
        Me.ControlBox = False
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.stbMain)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Location = New System.Drawing.Point(3, 3)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(732, 574)
        Me.Name = "frmBackground"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.stbMain.ResumeLayout(False)
        Me.stbMain.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
#End Region 
End Class