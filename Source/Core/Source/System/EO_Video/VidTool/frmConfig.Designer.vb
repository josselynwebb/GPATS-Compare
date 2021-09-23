<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmConfig
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
    Public WithEvents cmdHelp As System.Windows.Forms.Button
    Public WithEvents cmdBrowse As System.Windows.Forms.Button
    Public WithEvents cmdOK As System.Windows.Forms.Button
    Public WithEvents cboCamConfig As System.Windows.Forms.ComboBox
    Public WithEvents lblConfig As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmConfig))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdHelp = New System.Windows.Forms.Button()
        Me.cmdBrowse = New System.Windows.Forms.Button()
        Me.cmdOK = New System.Windows.Forms.Button()
        Me.cboCamConfig = New System.Windows.Forms.ComboBox()
        Me.lblConfig = New System.Windows.Forms.Label()
        Me.SuspendLayout
        '
        'cmdHelp
        '
        Me.cmdHelp.BackColor = System.Drawing.SystemColors.Control
        Me.cmdHelp.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdHelp.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cmdHelp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdHelp.Location = New System.Drawing.Point(48, 104)
        Me.cmdHelp.Name = "cmdHelp"
        Me.cmdHelp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdHelp.Size = New System.Drawing.Size(75, 25)
        Me.cmdHelp.TabIndex = 4
        Me.cmdHelp.Text = "Help"
        Me.cmdHelp.UseVisualStyleBackColor = false
        '
        'cmdBrowse
        '
        Me.cmdBrowse.BackColor = System.Drawing.SystemColors.Control
        Me.cmdBrowse.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdBrowse.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cmdBrowse.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdBrowse.Location = New System.Drawing.Point(136, 104)
        Me.cmdBrowse.Name = "cmdBrowse"
        Me.cmdBrowse.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdBrowse.Size = New System.Drawing.Size(75, 25)
        Me.cmdBrowse.TabIndex = 3
        Me.cmdBrowse.Text = "Browse"
        Me.cmdBrowse.UseVisualStyleBackColor = false
        '
        'cmdOK
        '
        Me.cmdOK.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOK.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOK.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cmdOK.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOK.Location = New System.Drawing.Point(224, 104)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOK.Size = New System.Drawing.Size(75, 25)
        Me.cmdOK.TabIndex = 1
        Me.cmdOK.Text = "OK"
        Me.cmdOK.UseVisualStyleBackColor = false
        '
        'cboCamConfig
        '
        Me.cboCamConfig.BackColor = System.Drawing.SystemColors.Window
        Me.cboCamConfig.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboCamConfig.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cboCamConfig.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboCamConfig.Location = New System.Drawing.Point(16, 72)
        Me.cboCamConfig.Name = "cboCamConfig"
        Me.cboCamConfig.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboCamConfig.Size = New System.Drawing.Size(281, 22)
        Me.cboCamConfig.TabIndex = 0
        Me.cboCamConfig.Text = "RS170_640x480"
        '
        'lblConfig
        '
        Me.lblConfig.BackColor = System.Drawing.SystemColors.Control
        Me.lblConfig.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblConfig.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblConfig.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblConfig.Location = New System.Drawing.Point(16, 8)
        Me.lblConfig.Name = "lblConfig"
        Me.lblConfig.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblConfig.Size = New System.Drawing.Size(281, 57)
        Me.lblConfig.TabIndex = 2
        Me.lblConfig.Text = resources.GetString("lblConfig.Text")
        '
        'frmConfig
        '
        Me.AcceptButton = Me.cmdHelp
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 14!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(312, 137)
        Me.Controls.Add(Me.cmdHelp)
        Me.Controls.Add(Me.cmdBrowse)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.cboCamConfig)
        Me.Controls.Add(Me.lblConfig)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(4, 23)
        Me.Name = "frmConfig"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Text = "Video Configuration"
        Me.ResumeLayout(false)

End Sub
#End Region
End Class