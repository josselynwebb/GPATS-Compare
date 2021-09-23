<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmEoPwrUpStatus
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
	Public WithEvents pbrEoPower As AxComctlLib.AxProgressBar
	Public WithEvents lblProgress As System.Windows.Forms.Label
	Public WithEvents Image1 As System.Windows.Forms.PictureBox
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEoPwrUpStatus))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.pbrEoPower = New AxComctlLib.AxProgressBar()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.Image1 = New System.Windows.Forms.PictureBox()
        CType(Me.pbrEoPower, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Image1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pbrEoPower
        '
        Me.pbrEoPower.Location = New System.Drawing.Point(8, 64)
        Me.pbrEoPower.Name = "pbrEoPower"
        Me.pbrEoPower.OcxState = CType(resources.GetObject("pbrEoPower.OcxState"), System.Windows.Forms.AxHost.State)
        Me.pbrEoPower.Size = New System.Drawing.Size(241, 25)
        Me.pbrEoPower.TabIndex = 0
        '
        'lblProgress
        '
        Me.lblProgress.BackColor = System.Drawing.SystemColors.Control
        Me.lblProgress.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblProgress.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProgress.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.lblProgress.Location = New System.Drawing.Point(48, 16)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblProgress.Size = New System.Drawing.Size(209, 33)
        Me.lblProgress.TabIndex = 1
        Me.lblProgress.Text = "EO Power-On in Progress Please Wait"
        Me.lblProgress.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Image1
        '
        Me.Image1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Image1.Image = CType(resources.GetObject("Image1.Image"), System.Drawing.Image)
        Me.Image1.Location = New System.Drawing.Point(8, 16)
        Me.Image1.Name = "Image1"
        Me.Image1.Size = New System.Drawing.Size(32, 32)
        Me.Image1.TabIndex = 2
        Me.Image1.TabStop = False
        '
        'frmEoPwrUpStatus
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(263, 95)
        Me.ControlBox = False
        Me.Controls.Add(Me.pbrEoPower)
        Me.Controls.Add(Me.lblProgress)
        Me.Controls.Add(Me.Image1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(4, 23)
        Me.Name = "frmEoPwrUpStatus"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "SAIS (Stand Alone Instrument Software)"
        CType(Me.pbrEoPower, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Image1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region 
End Class