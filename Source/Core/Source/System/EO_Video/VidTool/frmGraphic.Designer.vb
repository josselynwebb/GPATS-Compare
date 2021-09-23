<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmGraphic
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
    Public WithEvents txtGraphic As System.Windows.Forms.TextBox
    Public WithEvents imgHelp As System.Windows.Forms.PictureBox
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGraphic))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.txtGraphic = New System.Windows.Forms.TextBox()
        Me.imgHelp = New System.Windows.Forms.PictureBox()
        Me.pboxViewDir = New System.Windows.Forms.PictureBox()
        CType(Me.imgHelp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pboxViewDir, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtGraphic
        '
        Me.txtGraphic.AcceptsReturn = True
        Me.txtGraphic.BackColor = System.Drawing.SystemColors.Window
        Me.txtGraphic.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtGraphic.Font = New System.Drawing.Font("Courier New", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGraphic.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtGraphic.Location = New System.Drawing.Point(0, 0)
        Me.txtGraphic.MaxLength = 0
        Me.txtGraphic.Name = "txtGraphic"
        Me.txtGraphic.ReadOnly = True
        Me.txtGraphic.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGraphic.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtGraphic.Size = New System.Drawing.Size(577, 21)
        Me.txtGraphic.TabIndex = 1
        Me.txtGraphic.Text = "txtGraphic"
        Me.txtGraphic.Visible = False
        '
        'imgHelp
        '
        Me.imgHelp.Cursor = System.Windows.Forms.Cursors.Default
        Me.imgHelp.Image = CType(resources.GetObject("imgHelp.Image"), System.Drawing.Image)
        Me.imgHelp.Location = New System.Drawing.Point(-6, 0)
        Me.imgHelp.Name = "imgHelp"
        Me.imgHelp.Size = New System.Drawing.Size(571, 280)
        Me.imgHelp.TabIndex = 2
        Me.imgHelp.TabStop = False
        Me.imgHelp.Visible = False
        '
        'pboxViewDir
        '
        Me.pboxViewDir.Location = New System.Drawing.Point(0, 44)
        Me.pboxViewDir.Name = "pboxViewDir"
        Me.pboxViewDir.Size = New System.Drawing.Size(577, 244)
        Me.pboxViewDir.TabIndex = 3
        Me.pboxViewDir.TabStop = False
        '
        'frmGraphic
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.ClientSize = New System.Drawing.Size(570, 310)
        Me.Controls.Add(Me.pboxViewDir)
        Me.Controls.Add(Me.txtGraphic)
        Me.Controls.Add(Me.imgHelp)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(4, 23)
        Me.Name = "frmGraphic"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Text = "Video Display Help"
        CType(Me.imgHelp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pboxViewDir, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pboxViewDir As System.Windows.Forms.PictureBox
#End Region
End Class