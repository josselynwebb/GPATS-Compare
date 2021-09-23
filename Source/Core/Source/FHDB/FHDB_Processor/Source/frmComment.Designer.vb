<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmComment
#Region "Windows Form Designer generated code "
	<System.Diagnostics.DebuggerNonUserCode()> Public Sub New()
		MyBase.New()
		'This call is required by the Windows Form Designer.
		InitializeComponent()
		'This form is an MDI child.
		'This code simulates the VB6 
		' functionality of automatically
		' loading and showing an MDI
		' child's parent.
		Me.MDIParent = FHDB_Processor.MDIMain
		FHDB_Processor.MDIMain.Show
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
	Public WithEvents txtRecNum As System.Windows.Forms.TextBox
    Public WithEvents _cmdAction_2 As Button
	Public WithEvents txtComment As System.Windows.Forms.TextBox
    Public WithEvents _cmdAction_0 As Button
    Public WithEvents _cmdAction_1 As Button
    Public WithEvents lblRecNum As System.Windows.Forms.Label
    ' Public WithEvents cmdAction As Button
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmComment))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.txtRecNum = New System.Windows.Forms.TextBox()
        Me._cmdAction_2 = New System.Windows.Forms.Button()
        Me.txtComment = New System.Windows.Forms.TextBox()
        Me._cmdAction_0 = New System.Windows.Forms.Button()
        Me._cmdAction_1 = New System.Windows.Forms.Button()
        Me.lblRecNum = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'txtRecNum
        '
        Me.txtRecNum.AcceptsReturn = True
        Me.txtRecNum.BackColor = System.Drawing.SystemColors.Window
        Me.txtRecNum.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRecNum.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRecNum.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRecNum.Location = New System.Drawing.Point(632, 33)
        Me.txtRecNum.MaxLength = 0
        Me.txtRecNum.Name = "txtRecNum"
        Me.txtRecNum.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRecNum.Size = New System.Drawing.Size(70, 20)
        Me.txtRecNum.TabIndex = 1
        Me.txtRecNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        '_cmdAction_2
        '
        Me._cmdAction_2.Location = New System.Drawing.Point(629, 408)
        Me._cmdAction_2.Name = "_cmdAction_2"
        Me._cmdAction_2.Size = New System.Drawing.Size(76, 23)
        Me._cmdAction_2.TabIndex = 3
        Me._cmdAction_2.Text = "C&lose"
        '
        'txtComment
        '
        Me.txtComment.AcceptsReturn = True
        Me.txtComment.BackColor = System.Drawing.SystemColors.Window
        Me.txtComment.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtComment.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtComment.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtComment.Location = New System.Drawing.Point(14, 16)
        Me.txtComment.MaxLength = 0
        Me.txtComment.Multiline = True
        Me.txtComment.Name = "txtComment"
        Me.txtComment.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtComment.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtComment.Size = New System.Drawing.Size(592, 417)
        Me.txtComment.TabIndex = 0
        Me.txtComment.WordWrap = False
        '
        '_cmdAction_0
        '
        Me._cmdAction_0.Location = New System.Drawing.Point(630, 336)
        Me._cmdAction_0.Name = "_cmdAction_0"
        Me._cmdAction_0.Size = New System.Drawing.Size(75, 23)
        Me._cmdAction_0.TabIndex = 4
        Me._cmdAction_0.Text = "Sa&ve"
        '
        '_cmdAction_1
        '
        Me._cmdAction_1.Location = New System.Drawing.Point(630, 372)
        Me._cmdAction_1.Name = "_cmdAction_1"
        Me._cmdAction_1.Size = New System.Drawing.Size(75, 23)
        Me._cmdAction_1.TabIndex = 5
        Me._cmdAction_1.Text = "&Clear"
        '
        'lblRecNum
        '
        Me.lblRecNum.AutoSize = True
        Me.lblRecNum.BackColor = System.Drawing.SystemColors.Control
        Me.lblRecNum.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblRecNum.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRecNum.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblRecNum.Location = New System.Drawing.Point(626, 16)
        Me.lblRecNum.Name = "lblRecNum"
        Me.lblRecNum.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblRecNum.Size = New System.Drawing.Size(82, 14)
        Me.lblRecNum.TabIndex = 2
        Me.lblRecNum.Text = "Record Number"
        '
        'frmComment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(726, 449)
        Me.ControlBox = False
        Me.Controls.Add(Me.txtRecNum)
        Me.Controls.Add(Me._cmdAction_2)
        Me.Controls.Add(Me.txtComment)
        Me.Controls.Add(Me._cmdAction_0)
        Me.Controls.Add(Me._cmdAction_1)
        Me.Controls.Add(Me.lblRecNum)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(4, 23)
        Me.Name = "frmComment"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds
        Me.Text = "Operator Comments"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
#End Region 
End Class