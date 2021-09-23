<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmQuery
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
    Public WithEvents _ssoptPassFail_1 As CheckBox
    Public WithEvents _ssoptPassFail_0 As CheckBox
    Public WithEvents ssfraPassFail As GroupBox
    Public WithEvents cmdSearch As Button
	Public WithEvents _txtValue_1 As System.Windows.Forms.TextBox
	Public WithEvents _txtValue_2 As System.Windows.Forms.TextBox
	Public WithEvents _txtValue_3 As System.Windows.Forms.TextBox
	Public WithEvents cboValue As System.Windows.Forms.ComboBox
    Public WithEvents cmdByValue As Button
    Public WithEvents cmdByRange As Button
    Public WithEvents cmdCancel As Button
	Public WithEvents _lblDeg_1 As System.Windows.Forms.Label
	Public WithEvents _lblDeg_0 As System.Windows.Forms.Label
	Public WithEvents lblCriteria As System.Windows.Forms.Label
	Public WithEvents lblFrom As System.Windows.Forms.Label
	Public WithEvents lblTo As System.Windows.Forms.Label
	Public WithEvents lblDeg As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    Public WithEvents ssoptPassFail As RadioButton
	Public WithEvents txtValue As Microsoft.VisualBasic.Compatibility.VB6.TextBoxArray
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmQuery))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.ssfraPassFail = New System.Windows.Forms.GroupBox()
        Me._ssoptPassFail_1 = New System.Windows.Forms.CheckBox()
        Me._ssoptPassFail_0 = New System.Windows.Forms.CheckBox()
        Me.cmdSearch = New System.Windows.Forms.Button()
        Me._txtValue_1 = New System.Windows.Forms.TextBox()
        Me._txtValue_2 = New System.Windows.Forms.TextBox()
        Me._txtValue_3 = New System.Windows.Forms.TextBox()
        Me.cboValue = New System.Windows.Forms.ComboBox()
        Me.cmdByValue = New System.Windows.Forms.Button()
        Me.cmdByRange = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me._lblDeg_1 = New System.Windows.Forms.Label()
        Me._lblDeg_0 = New System.Windows.Forms.Label()
        Me.lblCriteria = New System.Windows.Forms.Label()
        Me.lblFrom = New System.Windows.Forms.Label()
        Me.lblTo = New System.Windows.Forms.Label()
        Me.lblDeg = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.ssoptPassFail = New System.Windows.Forms.RadioButton()
        Me.txtValue = New Microsoft.VisualBasic.Compatibility.VB6.TextBoxArray(Me.components)
        Me.ssfraPassFail.SuspendLayout()
        CType(Me.lblDeg, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtValue, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ssfraPassFail
        '
        Me.ssfraPassFail.Controls.Add(Me._ssoptPassFail_1)
        Me.ssfraPassFail.Controls.Add(Me._ssoptPassFail_0)
        Me.ssfraPassFail.Location = New System.Drawing.Point(122, 64)
        Me.ssfraPassFail.Name = "ssfraPassFail"
        Me.ssfraPassFail.Size = New System.Drawing.Size(81, 65)
        Me.ssfraPassFail.TabIndex = 3
        Me.ssfraPassFail.TabStop = False
        Me.ssfraPassFail.Visible = False
        '
        '_ssoptPassFail_1
        '
        Me._ssoptPassFail_1.Location = New System.Drawing.Point(8, 40)
        Me._ssoptPassFail_1.Name = "_ssoptPassFail_1"
        Me._ssoptPassFail_1.Size = New System.Drawing.Size(59, 17)
        Me._ssoptPassFail_1.TabIndex = 1
        Me._ssoptPassFail_1.Text = "&Fail"
        '
        '_ssoptPassFail_0
        '
        Me._ssoptPassFail_0.Location = New System.Drawing.Point(8, 16)
        Me._ssoptPassFail_0.Name = "_ssoptPassFail_0"
        Me._ssoptPassFail_0.Size = New System.Drawing.Size(59, 17)
        Me._ssoptPassFail_0.TabIndex = 2
        Me._ssoptPassFail_0.Text = "&Pass"
        '
        'cmdSearch
        '
        Me.cmdSearch.Location = New System.Drawing.Point(195, 168)
        Me.cmdSearch.Name = "cmdSearch"
        Me.cmdSearch.Size = New System.Drawing.Size(75, 23)
        Me.cmdSearch.TabIndex = 7
        Me.cmdSearch.Text = "&Search"
        '
        '_txtValue_1
        '
        Me._txtValue_1.AcceptsReturn = True
        Me._txtValue_1.BackColor = System.Drawing.SystemColors.Window
        Me._txtValue_1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me._txtValue_1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._txtValue_1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtValue.SetIndex(Me._txtValue_1, CType(1, Short))
        Me._txtValue_1.Location = New System.Drawing.Point(112, 64)
        Me._txtValue_1.MaxLength = 0
        Me._txtValue_1.Name = "_txtValue_1"
        Me._txtValue_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._txtValue_1.Size = New System.Drawing.Size(145, 20)
        Me._txtValue_1.TabIndex = 4
        Me._txtValue_1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me._txtValue_1.Visible = False
        '
        '_txtValue_2
        '
        Me._txtValue_2.AcceptsReturn = True
        Me._txtValue_2.BackColor = System.Drawing.SystemColors.Window
        Me._txtValue_2.Cursor = System.Windows.Forms.Cursors.IBeam
        Me._txtValue_2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._txtValue_2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtValue.SetIndex(Me._txtValue_2, CType(2, Short))
        Me._txtValue_2.Location = New System.Drawing.Point(112, 64)
        Me._txtValue_2.MaxLength = 0
        Me._txtValue_2.Name = "_txtValue_2"
        Me._txtValue_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._txtValue_2.Size = New System.Drawing.Size(145, 20)
        Me._txtValue_2.TabIndex = 5
        Me._txtValue_2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me._txtValue_2.Visible = False
        '
        '_txtValue_3
        '
        Me._txtValue_3.AcceptsReturn = True
        Me._txtValue_3.BackColor = System.Drawing.SystemColors.Window
        Me._txtValue_3.Cursor = System.Windows.Forms.Cursors.IBeam
        Me._txtValue_3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._txtValue_3.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtValue.SetIndex(Me._txtValue_3, CType(3, Short))
        Me._txtValue_3.Location = New System.Drawing.Point(112, 96)
        Me._txtValue_3.MaxLength = 0
        Me._txtValue_3.Name = "_txtValue_3"
        Me._txtValue_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._txtValue_3.Size = New System.Drawing.Size(145, 20)
        Me._txtValue_3.TabIndex = 6
        Me._txtValue_3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me._txtValue_3.Visible = False
        '
        'cboValue
        '
        Me.cboValue.BackColor = System.Drawing.SystemColors.Window
        Me.cboValue.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboValue.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboValue.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboValue.Location = New System.Drawing.Point(66, 24)
        Me.cboValue.Name = "cboValue"
        Me.cboValue.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboValue.Size = New System.Drawing.Size(193, 22)
        Me.cboValue.TabIndex = 0
        Me.cboValue.Visible = False
        '
        'cmdByValue
        '
        Me.cmdByValue.Location = New System.Drawing.Point(195, 48)
        Me.cmdByValue.Name = "cmdByValue"
        Me.cmdByValue.Size = New System.Drawing.Size(75, 23)
        Me.cmdByValue.TabIndex = 12
        Me.cmdByValue.Text = "By &Value"
        '
        'cmdByRange
        '
        Me.cmdByRange.Location = New System.Drawing.Point(54, 48)
        Me.cmdByRange.Name = "cmdByRange"
        Me.cmdByRange.Size = New System.Drawing.Size(75, 23)
        Me.cmdByRange.TabIndex = 13
        Me.cmdByRange.Text = "By &Range"
        '
        'cmdCancel
        '
        Me.cmdCancel.Location = New System.Drawing.Point(54, 168)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(75, 23)
        Me.cmdCancel.TabIndex = 8
        Me.cmdCancel.Text = "&Cancel"
        '
        '_lblDeg_1
        '
        Me._lblDeg_1.AutoSize = True
        Me._lblDeg_1.BackColor = System.Drawing.SystemColors.Control
        Me._lblDeg_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblDeg_1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblDeg_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDeg.SetIndex(Me._lblDeg_1, CType(1, Short))
        Me._lblDeg_1.Location = New System.Drawing.Point(264, 104)
        Me._lblDeg_1.Name = "_lblDeg_1"
        Me._lblDeg_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblDeg_1.Size = New System.Drawing.Size(14, 14)
        Me._lblDeg_1.TabIndex = 15
        Me._lblDeg_1.Text = "C"
        Me._lblDeg_1.Visible = False
        '
        '_lblDeg_0
        '
        Me._lblDeg_0.AutoSize = True
        Me._lblDeg_0.BackColor = System.Drawing.SystemColors.Control
        Me._lblDeg_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblDeg_0.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblDeg_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDeg.SetIndex(Me._lblDeg_0, CType(0, Short))
        Me._lblDeg_0.Location = New System.Drawing.Point(264, 72)
        Me._lblDeg_0.Name = "_lblDeg_0"
        Me._lblDeg_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblDeg_0.Size = New System.Drawing.Size(14, 14)
        Me._lblDeg_0.TabIndex = 14
        Me._lblDeg_0.Text = "C"
        Me._lblDeg_0.Visible = False
        '
        'lblCriteria
        '
        Me.lblCriteria.AutoSize = True
        Me.lblCriteria.BackColor = System.Drawing.SystemColors.Control
        Me.lblCriteria.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCriteria.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCriteria.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCriteria.Location = New System.Drawing.Point(72, 74)
        Me.lblCriteria.Name = "lblCriteria"
        Me.lblCriteria.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCriteria.Size = New System.Drawing.Size(44, 14)
        Me.lblCriteria.TabIndex = 11
        Me.lblCriteria.Text = "Criteria:"
        Me.lblCriteria.Visible = False
        '
        'lblFrom
        '
        Me.lblFrom.AutoSize = True
        Me.lblFrom.BackColor = System.Drawing.SystemColors.Control
        Me.lblFrom.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblFrom.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFrom.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblFrom.Location = New System.Drawing.Point(80, 72)
        Me.lblFrom.Name = "lblFrom"
        Me.lblFrom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblFrom.Size = New System.Drawing.Size(31, 14)
        Me.lblFrom.TabIndex = 10
        Me.lblFrom.Text = "From"
        Me.lblFrom.Visible = False
        '
        'lblTo
        '
        Me.lblTo.AutoSize = True
        Me.lblTo.BackColor = System.Drawing.SystemColors.Control
        Me.lblTo.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTo.Location = New System.Drawing.Point(88, 104)
        Me.lblTo.Name = "lblTo"
        Me.lblTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTo.Size = New System.Drawing.Size(18, 14)
        Me.lblTo.TabIndex = 9
        Me.lblTo.Text = "To"
        Me.lblTo.Visible = False
        '
        'ssoptPassFail
        '
        Me.ssoptPassFail.Location = New System.Drawing.Point(0, 0)
        Me.ssoptPassFail.Name = "ssoptPassFail"
        Me.ssoptPassFail.Size = New System.Drawing.Size(104, 24)
        Me.ssoptPassFail.TabIndex = 0
        '
        'txtValue
        '
        '
        'frmQuery
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(318, 246)
        Me.Controls.Add(Me.ssfraPassFail)
        Me.Controls.Add(Me.cmdSearch)
        Me.Controls.Add(Me._txtValue_1)
        Me.Controls.Add(Me._txtValue_2)
        Me.Controls.Add(Me._txtValue_3)
        Me.Controls.Add(Me.cboValue)
        Me.Controls.Add(Me.cmdByValue)
        Me.Controls.Add(Me.cmdByRange)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me._lblDeg_1)
        Me.Controls.Add(Me._lblDeg_0)
        Me.Controls.Add(Me.lblCriteria)
        Me.Controls.Add(Me.lblFrom)
        Me.Controls.Add(Me.lblTo)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(3, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmQuery"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Search FHDB"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ssfraPassFail.ResumeLayout(False)
        CType(Me.lblDeg, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtValue, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
#End Region 
End Class