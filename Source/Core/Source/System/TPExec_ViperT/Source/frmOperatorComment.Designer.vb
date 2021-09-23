<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmOperatorComment
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
	Public WithEvents txtOperatorComment As System.Windows.Forms.TextBox
    Public WithEvents SSPanel1 As System.Windows.Forms.Panel
    Public WithEvents cmdContinue As System.Windows.Forms.Button
	Public WithEvents Label2 As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.SSPanel1 = New System.Windows.Forms.Panel()
        Me.txtOperatorComment = New System.Windows.Forms.TextBox()
        Me.cmdContinue = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SSPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'SSPanel1
        '
        Me.SSPanel1.Controls.Add(Me.txtOperatorComment)
        Me.SSPanel1.Location = New System.Drawing.Point(8, 8)
        Me.SSPanel1.Name = "SSPanel1"
        Me.SSPanel1.Size = New System.Drawing.Size(561, 121)
        Me.SSPanel1.TabIndex = 0
        Me.SSPanel1.Text = "SSPanel1"
        '
        'txtOperatorComment
        '
        Me.txtOperatorComment.AcceptsReturn = True
        Me.txtOperatorComment.BackColor = System.Drawing.SystemColors.Window
        Me.txtOperatorComment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOperatorComment.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOperatorComment.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOperatorComment.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtOperatorComment.Location = New System.Drawing.Point(8, 8)
        Me.txtOperatorComment.MaxLength = 255
        Me.txtOperatorComment.Multiline = True
        Me.txtOperatorComment.Name = "txtOperatorComment"
        Me.txtOperatorComment.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOperatorComment.Size = New System.Drawing.Size(545, 105)
        Me.txtOperatorComment.TabIndex = 1
        '
        'cmdContinue
        '
        Me.cmdContinue.Location = New System.Drawing.Point(472, 136)
        Me.cmdContinue.Name = "cmdContinue"
        Me.cmdContinue.Size = New System.Drawing.Size(100, 29)
        Me.cmdContinue.TabIndex = 2
        Me.cmdContinue.Text = "Continue"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(144, 144)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(313, 25)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Enter Comment above and select Continue"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'frmOperatorComment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(576, 183)
        Me.ControlBox = False
        Me.Controls.Add(Me.SSPanel1)
        Me.Controls.Add(Me.cmdContinue)
        Me.Controls.Add(Me.Label2)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Location = New System.Drawing.Point(4, 30)
        Me.Name = "frmOperatorComment"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.Text = "** Manually Generate Operator Comment **"
        Me.SSPanel1.ResumeLayout(False)
        Me.SSPanel1.PerformLayout()
        Me.ResumeLayout(False)

End Sub
#End Region 
End Class