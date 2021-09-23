<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmToolBar
#Region "Windows Form Designer generated code "
	<System.Diagnostics.DebuggerNonUserCode()> Public Sub New()
		MyBase.New()
		'This call is required by the Windows Form Designer.
		InitializeComponent()
	End Sub
	'Form overrides dispose to clean up the component list.
	<System.Diagnostics.DebuggerNonUserCode()> Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
		If Disposing Then
			Static fTerminateCalled As Boolean
			If Not fTerminateCalled Then
				Form_Terminate_renamed()
				fTerminateCalled = True
			End If
			If Not components Is Nothing Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(Disposing)
	End Sub
	'Required by the Windows Form Designer
	Private components As System.ComponentModel.IContainer
	Public ToolTip1 As System.Windows.Forms.ToolTip
	Public WithEvents tbrInstruments2 As AxComctlLib.AxToolbar
	Public WithEvents tbrInstruments As AxComctlLib.AxToolbar
	Public WithEvents Timer1 As System.Windows.Forms.Timer
    Public WithEvents img16 As AxComctlLib.AxImageList
	Public WithEvents img32 As AxComctlLib.AxImageList
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmToolBar))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.tbrInstruments2 = New AxComctlLib.AxToolbar()
        Me.tbrInstruments = New AxComctlLib.AxToolbar()
        Me.img16 = New AxComctlLib.AxImageList()
        Me.img32 = New AxComctlLib.AxImageList()
        CType(Me.tbrInstruments2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbrInstruments, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img32, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 250
        '
        'tbrInstruments2
        '
        Me.tbrInstruments2.Dock = System.Windows.Forms.DockStyle.Top
        Me.tbrInstruments2.Location = New System.Drawing.Point(0, 0)
        Me.tbrInstruments2.Name = "tbrInstruments2"
        Me.tbrInstruments2.OcxState = CType(resources.GetObject("tbrInstruments2.OcxState"), System.Windows.Forms.AxHost.State)
        Me.tbrInstruments2.Size = New System.Drawing.Size(708, 26)
        Me.tbrInstruments2.TabIndex = 1
        Me.tbrInstruments2.Visible = False
        '
        'tbrInstruments
        '
        Me.tbrInstruments.Dock = System.Windows.Forms.DockStyle.Top
        Me.tbrInstruments.Location = New System.Drawing.Point(0, 26)
        Me.tbrInstruments.Name = "tbrInstruments"
        Me.tbrInstruments.OcxState = CType(resources.GetObject("tbrInstruments.OcxState"), System.Windows.Forms.AxHost.State)
        Me.tbrInstruments.Size = New System.Drawing.Size(708, 42)
        Me.tbrInstruments.TabIndex = 0
        '
        'img16
        '
        Me.img16.Enabled = True
        Me.img16.Location = New System.Drawing.Point(124, 72)
        Me.img16.Name = "img16"
        Me.img16.OcxState = CType(resources.GetObject("img16.OcxState"), System.Windows.Forms.AxHost.State)
        Me.img16.Size = New System.Drawing.Size(38, 38)
        Me.img16.TabIndex = 3
        '
        'img32
        '
        Me.img32.Enabled = True
        Me.img32.Location = New System.Drawing.Point(0, 72)
        Me.img32.Name = "img32"
        Me.img32.OcxState = CType(resources.GetObject("img32.OcxState"), System.Windows.Forms.AxHost.State)
        Me.img32.Size = New System.Drawing.Size(38, 38)
        Me.img32.TabIndex = 4
        '
        'frmToolBar
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(708, 118)
        Me.Controls.Add(Me.tbrInstruments)
        Me.Controls.Add(Me.tbrInstruments2)
        Me.Controls.Add(Me.img16)
        Me.Controls.Add(Me.img32)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(132, 147)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmToolBar"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "SAIS (Stand Alone Instrument Software)"
        CType(Me.tbrInstruments2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbrInstruments, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img32, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

End Sub
#End Region 
End Class