<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmImage
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
    Public WithEvents PrintButton As System.Windows.Forms.Button
    Public WithEvents Quit As System.Windows.Forms.Button
    Public WithEvents NPage As System.Windows.Forms.Button
    Public WithEvents PPage As System.Windows.Forms.Button
	Public WithEvents ImageNavigationlPanel As System.Windows.Forms.GroupBox
    Public WithEvents MouseControl_0 As System.Windows.Forms.Button
    Public WithEvents ResetView As System.Windows.Forms.Button
    Public WithEvents MouseControl_2 As System.Windows.Forms.Button
    Public WithEvents MouseControl_1 As System.Windows.Forms.Button
    Public WithEvents ZoomOut50 As System.Windows.Forms.Button
    Public WithEvents ImageControlPanel As System.Windows.Forms.GroupBox
    Public WithEvents SSPanel1 As System.Windows.Forms.Panel
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmImage))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.ImageNavigationlPanel = New System.Windows.Forms.GroupBox()
        Me.PrintButton = New System.Windows.Forms.Button()
        Me.Quit = New System.Windows.Forms.Button()
        Me.NPage = New System.Windows.Forms.Button()
        Me.PPage = New System.Windows.Forms.Button()
        Me.ImageControlPanel = New System.Windows.Forms.GroupBox()
        Me.MouseControl_0 = New System.Windows.Forms.Button()
        Me.ButtonImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.ResetView = New System.Windows.Forms.Button()
        Me.MouseControl_2 = New System.Windows.Forms.Button()
        Me.MouseControl_1 = New System.Windows.Forms.Button()
        Me.ZoomOut50 = New System.Windows.Forms.Button()
        Me.SSPanel1 = New System.Windows.Forms.Panel()
        Me.ViewDirX1 = New AxVIEWDIRXLib.AxViewDirX()
        Me.ImageNavigationlPanel.SuspendLayout()
        Me.ImageControlPanel.SuspendLayout()
        CType(Me.ViewDirX1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ImageNavigationlPanel
        '
        Me.ImageNavigationlPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ImageNavigationlPanel.Controls.Add(Me.PrintButton)
        Me.ImageNavigationlPanel.Controls.Add(Me.Quit)
        Me.ImageNavigationlPanel.Controls.Add(Me.NPage)
        Me.ImageNavigationlPanel.Controls.Add(Me.PPage)
        Me.ImageNavigationlPanel.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ImageNavigationlPanel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ImageNavigationlPanel.Location = New System.Drawing.Point(8, 472)
        Me.ImageNavigationlPanel.Name = "ImageNavigationlPanel"
        Me.ImageNavigationlPanel.Padding = New System.Windows.Forms.Padding(0)
        Me.ImageNavigationlPanel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ImageNavigationlPanel.Size = New System.Drawing.Size(217, 81)
        Me.ImageNavigationlPanel.TabIndex = 1
        Me.ImageNavigationlPanel.TabStop = False
        Me.ImageNavigationlPanel.Text = "Navigation"
        '
        'PrintButton
        '
        Me.PrintButton.Enabled = False
        Me.PrintButton.Location = New System.Drawing.Point(112, 48)
        Me.PrintButton.Name = "PrintButton"
        Me.PrintButton.Size = New System.Drawing.Size(100, 29)
        Me.PrintButton.TabIndex = 5
        Me.PrintButton.Text = "&Print"
        '
        'Quit
        '
        Me.Quit.Enabled = False
        Me.Quit.Location = New System.Drawing.Point(8, 48)
        Me.Quit.Name = "Quit"
        Me.Quit.Size = New System.Drawing.Size(100, 29)
        Me.Quit.TabIndex = 6
        Me.Quit.Text = "&Quit"
        '
        'NPage
        '
        Me.NPage.Location = New System.Drawing.Point(112, 16)
        Me.NPage.Name = "NPage"
        Me.NPage.Size = New System.Drawing.Size(100, 29)
        Me.NPage.TabIndex = 2
        Me.NPage.Text = "&Next Page"
        Me.NPage.Visible = False
        '
        'PPage
        '
        Me.PPage.Location = New System.Drawing.Point(8, 16)
        Me.PPage.Name = "PPage"
        Me.PPage.Size = New System.Drawing.Size(100, 29)
        Me.PPage.TabIndex = 3
        Me.PPage.Text = "P&rev Page"
        Me.PPage.Visible = False
        '
        'ImageControlPanel
        '
        Me.ImageControlPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ImageControlPanel.Controls.Add(Me.MouseControl_0)
        Me.ImageControlPanel.Controls.Add(Me.ResetView)
        Me.ImageControlPanel.Controls.Add(Me.MouseControl_2)
        Me.ImageControlPanel.Controls.Add(Me.MouseControl_1)
        Me.ImageControlPanel.Controls.Add(Me.ZoomOut50)
        Me.ImageControlPanel.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ImageControlPanel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ImageControlPanel.Location = New System.Drawing.Point(232, 472)
        Me.ImageControlPanel.Name = "ImageControlPanel"
        Me.ImageControlPanel.Padding = New System.Windows.Forms.Padding(0)
        Me.ImageControlPanel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ImageControlPanel.Size = New System.Drawing.Size(545, 81)
        Me.ImageControlPanel.TabIndex = 8
        Me.ImageControlPanel.TabStop = False
        Me.ImageControlPanel.Text = "Image Control Options"
        '
        'MouseControl_0
        '
        Me.MouseControl_0.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.MouseControl_0.ImageKey = "pan.ICO"
        Me.MouseControl_0.ImageList = Me.ButtonImageList
        Me.MouseControl_0.Location = New System.Drawing.Point(8, 16)
        Me.MouseControl_0.Name = "MouseControl_0"
        Me.MouseControl_0.Size = New System.Drawing.Size(100, 60)
        Me.MouseControl_0.TabIndex = 10
        Me.MouseControl_0.Text = "P&an"
        Me.MouseControl_0.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.MouseControl_0.Visible = False
        '
        'ButtonImageList
        '
        Me.ButtonImageList.ImageStream = CType(resources.GetObject("ButtonImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ButtonImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.ButtonImageList.Images.SetKeyName(0, "pan.ICO")
        Me.ButtonImageList.Images.SetKeyName(1, "disfail.ico")
        Me.ButtonImageList.Images.SetKeyName(2, "drawings.ico")
        Me.ButtonImageList.Images.SetKeyName(3, "reset.ICO")
        Me.ButtonImageList.Images.SetKeyName(4, "zoomout.ICO")
        '
        'ResetView
        '
        Me.ResetView.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.ResetView.ImageKey = "reset.ICO"
        Me.ResetView.ImageList = Me.ButtonImageList
        Me.ResetView.Location = New System.Drawing.Point(320, 16)
        Me.ResetView.Name = "ResetView"
        Me.ResetView.Size = New System.Drawing.Size(100, 60)
        Me.ResetView.TabIndex = 0
        Me.ResetView.Text = "Reset &View"
        Me.ResetView.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ResetView.Visible = False
        '
        'MouseControl_2
        '
        Me.MouseControl_2.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.MouseControl_2.ImageKey = "drawings.ico"
        Me.MouseControl_2.ImageList = Me.ButtonImageList
        Me.MouseControl_2.Location = New System.Drawing.Point(216, 16)
        Me.MouseControl_2.Name = "MouseControl_2"
        Me.MouseControl_2.Size = New System.Drawing.Size(100, 60)
        Me.MouseControl_2.TabIndex = 11
        Me.MouseControl_2.Text = "&Magnify"
        Me.MouseControl_2.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.MouseControl_2.Visible = False
        '
        'MouseControl_1
        '
        Me.MouseControl_1.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.MouseControl_1.ImageKey = "disfail.ico"
        Me.MouseControl_1.ImageList = Me.ButtonImageList
        Me.MouseControl_1.Location = New System.Drawing.Point(112, 16)
        Me.MouseControl_1.Name = "MouseControl_1"
        Me.MouseControl_1.Size = New System.Drawing.Size(100, 60)
        Me.MouseControl_1.TabIndex = 12
        Me.MouseControl_1.Text = "&Zoom"
        Me.MouseControl_1.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.MouseControl_1.Visible = False
        '
        'ZoomOut50
        '
        Me.ZoomOut50.AutoSize = True
        Me.ZoomOut50.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.ZoomOut50.ImageKey = "zoomout.ICO"
        Me.ZoomOut50.ImageList = Me.ButtonImageList
        Me.ZoomOut50.Location = New System.Drawing.Point(424, 16)
        Me.ZoomOut50.Name = "ZoomOut50"
        Me.ZoomOut50.Size = New System.Drawing.Size(107, 60)
        Me.ZoomOut50.TabIndex = 13
        Me.ZoomOut50.Text = "Zoom &Out 50%"
        Me.ZoomOut50.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ZoomOut50.Visible = False
        '
        'SSPanel1
        '
        Me.SSPanel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SSPanel1.Location = New System.Drawing.Point(8, 8)
        Me.SSPanel1.Name = "SSPanel1"
        Me.SSPanel1.Size = New System.Drawing.Size(766, 457)
        Me.SSPanel1.TabIndex = 4
        Me.SSPanel1.Text = "SSPanel1"
        '
        'ViewDirX1
        '
        Me.ViewDirX1.Enabled = True
        Me.ViewDirX1.Location = New System.Drawing.Point(8, 8)
        Me.ViewDirX1.Name = "ViewDirX1"
        Me.ViewDirX1.OcxState = CType(resources.GetObject("ViewDirX1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.ViewDirX1.Size = New System.Drawing.Size(766, 458)
        Me.ViewDirX1.TabIndex = 9
        '
        'frmImage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(782, 559)
        Me.Controls.Add(Me.ViewDirX1)
        Me.Controls.Add(Me.ImageNavigationlPanel)
        Me.Controls.Add(Me.ImageControlPanel)
        Me.Controls.Add(Me.SSPanel1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(4, 30)
        Me.Name = "frmImage"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Text = "Form1"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ImageNavigationlPanel.ResumeLayout(False)
        Me.ImageControlPanel.ResumeLayout(False)
        Me.ImageControlPanel.PerformLayout()
        CType(Me.ViewDirX1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

End Sub
 Friend WithEvents ButtonImageList As System.Windows.Forms.ImageList
 Friend WithEvents ViewDirX1 As AxVIEWDIRXLib.AxViewDirX
#End Region 
End Class