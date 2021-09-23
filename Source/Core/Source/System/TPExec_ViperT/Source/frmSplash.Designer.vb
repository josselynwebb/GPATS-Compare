<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmSplash
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
	Public WithEvents SplashTimer As System.Windows.Forms.Timer
    Public WithEvents cmdOk As System.Windows.Forms.Button
	Public WithEvents lblTPCCN As System.Windows.Forms.Label
	Public WithEvents lblTPExecVersion As System.Windows.Forms.Label
	Public WithEvents lblWeaponSystemNomenclature As System.Windows.Forms.Label
	Public WithEvents Label3 As System.Windows.Forms.Label
	Public WithEvents lblUUTPartNo As System.Windows.Forms.Label
	Public WithEvents lblUUTName As System.Windows.Forms.Label
	Public WithEvents lblUUT As System.Windows.Forms.Label
	Public WithEvents lblWeaponSystem As System.Windows.Forms.Label
	Public WithEvents lblTPName As System.Windows.Forms.Label
	Public WithEvents imgLogo As System.Windows.Forms.PictureBox
	Public WithEvents lblAddress As System.Windows.Forms.Label
	Public WithEvents lblCompany As System.Windows.Forms.Label
	Public WithEvents lblTPVersion As System.Windows.Forms.Label
	Public WithEvents lblPlatform As System.Windows.Forms.Label
    Public WithEvents Frame1 As System.Windows.Forms.Panel
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSplash))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Frame1 = New System.Windows.Forms.Panel()
        Me.cmdOk = New System.Windows.Forms.Button()
        Me.lblTPCCN = New System.Windows.Forms.Label()
        Me.lblTPExecVersion = New System.Windows.Forms.Label()
        Me.lblWeaponSystemNomenclature = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblUUTPartNo = New System.Windows.Forms.Label()
        Me.lblUUTName = New System.Windows.Forms.Label()
        Me.lblUUT = New System.Windows.Forms.Label()
        Me.lblWeaponSystem = New System.Windows.Forms.Label()
        Me.lblTPName = New System.Windows.Forms.Label()
        Me.lblAddress = New System.Windows.Forms.Label()
        Me.lblCompany = New System.Windows.Forms.Label()
        Me.lblTPVersion = New System.Windows.Forms.Label()
        Me.lblPlatform = New System.Windows.Forms.Label()
        Me.ShapeContainer1 = New Microsoft.VisualBasic.PowerPacks.ShapeContainer()
        Me.LineShape1 = New Microsoft.VisualBasic.PowerPacks.LineShape()
        Me.SplashTimer = New System.Windows.Forms.Timer(Me.components)
        Me.imgLogo = New System.Windows.Forms.PictureBox()
        Me.Frame1.SuspendLayout()
        CType(Me.imgLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(151, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Frame1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Frame1.Controls.Add(Me.cmdOk)
        Me.Frame1.Controls.Add(Me.lblTPCCN)
        Me.Frame1.Controls.Add(Me.lblTPExecVersion)
        Me.Frame1.Controls.Add(Me.lblWeaponSystemNomenclature)
        Me.Frame1.Controls.Add(Me.Label3)
        Me.Frame1.Controls.Add(Me.lblUUTPartNo)
        Me.Frame1.Controls.Add(Me.lblUUTName)
        Me.Frame1.Controls.Add(Me.lblUUT)
        Me.Frame1.Controls.Add(Me.lblWeaponSystem)
        Me.Frame1.Controls.Add(Me.lblTPName)
        Me.Frame1.Controls.Add(Me.imgLogo)
        Me.Frame1.Controls.Add(Me.lblAddress)
        Me.Frame1.Controls.Add(Me.lblCompany)
        Me.Frame1.Controls.Add(Me.lblTPVersion)
        Me.Frame1.Controls.Add(Me.lblPlatform)
        Me.Frame1.Controls.Add(Me.ShapeContainer1)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(8, 5)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(486, 278)
        Me.Frame1.TabIndex = 0
        '
        'cmdOk
        '
        Me.cmdOk.BackColor = System.Drawing.Color.Silver
        Me.cmdOk.Enabled = False
        Me.cmdOk.Location = New System.Drawing.Point(16, 248)
        Me.cmdOk.Name = "cmdOk"
        Me.cmdOk.Size = New System.Drawing.Size(76, 21)
        Me.cmdOk.TabIndex = 14
        Me.cmdOk.Text = "&Ok"
        Me.cmdOk.UseVisualStyleBackColor = True
        '
        'lblTPCCN
        '
        Me.lblTPCCN.AutoSize = True
        Me.lblTPCCN.BackColor = System.Drawing.Color.Transparent
        Me.lblTPCCN.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTPCCN.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTPCCN.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTPCCN.Location = New System.Drawing.Point(304, 89)
        Me.lblTPCCN.Name = "lblTPCCN"
        Me.lblTPCCN.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTPCCN.Size = New System.Drawing.Size(56, 16)
        Me.lblTPCCN.TabIndex = 15
        Me.lblTPCCN.Text = "TPCCN:"
        '
        'lblTPExecVersion
        '
        Me.lblTPExecVersion.AutoSize = True
        Me.lblTPExecVersion.BackColor = System.Drawing.Color.Transparent
        Me.lblTPExecVersion.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTPExecVersion.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTPExecVersion.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTPExecVersion.Location = New System.Drawing.Point(304, 108)
        Me.lblTPExecVersion.Name = "lblTPExecVersion"
        Me.lblTPExecVersion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTPExecVersion.Size = New System.Drawing.Size(123, 16)
        Me.lblTPExecVersion.TabIndex = 13
        Me.lblTPExecVersion.Text = "TP Executive Ver.:"
        '
        'lblWeaponSystemNomenclature
        '
        Me.lblWeaponSystemNomenclature.AutoSize = True
        Me.lblWeaponSystemNomenclature.BackColor = System.Drawing.Color.Transparent
        Me.lblWeaponSystemNomenclature.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblWeaponSystemNomenclature.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWeaponSystemNomenclature.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblWeaponSystemNomenclature.Location = New System.Drawing.Point(16, 224)
        Me.lblWeaponSystemNomenclature.Name = "lblWeaponSystemNomenclature"
        Me.lblWeaponSystemNomenclature.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblWeaponSystemNomenclature.Size = New System.Drawing.Size(116, 19)
        Me.lblWeaponSystemNomenclature.TabIndex = 12
        Me.lblWeaponSystemNomenclature.Text = "Nomenclature"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(329, 214)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(114, 15)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "Developed By:"
        '
        'lblUUTPartNo
        '
        Me.lblUUTPartNo.BackColor = System.Drawing.Color.Transparent
        Me.lblUUTPartNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblUUTPartNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUUTPartNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUUTPartNo.Location = New System.Drawing.Point(304, 190)
        Me.lblUUTPartNo.Name = "lblUUTPartNo"
        Me.lblUUTPartNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblUUTPartNo.Size = New System.Drawing.Size(158, 15)
        Me.lblUUTPartNo.TabIndex = 9
        Me.lblUUTPartNo.Text = "Part No.: #########"
        '
        'lblUUTName
        '
        Me.lblUUTName.AutoSize = True
        Me.lblUUTName.BackColor = System.Drawing.Color.Transparent
        Me.lblUUTName.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblUUTName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUUTName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUUTName.Location = New System.Drawing.Point(304, 163)
        Me.lblUUTName.Name = "lblUUTName"
        Me.lblUUTName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblUUTName.Size = New System.Drawing.Size(84, 16)
        Me.lblUUTName.TabIndex = 8
        Me.lblUUTName.Text = "<uut name>"
        '
        'lblUUT
        '
        Me.lblUUT.AutoSize = True
        Me.lblUUT.BackColor = System.Drawing.Color.Transparent
        Me.lblUUT.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblUUT.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUUT.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUUT.Location = New System.Drawing.Point(305, 138)
        Me.lblUUT.Name = "lblUUT"
        Me.lblUUT.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblUUT.Size = New System.Drawing.Size(159, 22)
        Me.lblUUT.TabIndex = 7
        Me.lblUUT.Text = "Unit Under Test:"
        '
        'lblWeaponSystem
        '
        Me.lblWeaponSystem.AutoSize = True
        Me.lblWeaponSystem.BackColor = System.Drawing.Color.Transparent
        Me.lblWeaponSystem.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblWeaponSystem.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWeaponSystem.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblWeaponSystem.Location = New System.Drawing.Point(16, 200)
        Me.lblWeaponSystem.Name = "lblWeaponSystem"
        Me.lblWeaponSystem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblWeaponSystem.Size = New System.Drawing.Size(133, 19)
        Me.lblWeaponSystem.TabIndex = 6
        Me.lblWeaponSystem.Text = "Weapon System"
        '
        'lblTPName
        '
        Me.lblTPName.AutoSize = True
        Me.lblTPName.BackColor = System.Drawing.Color.Transparent
        Me.lblTPName.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTPName.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTPName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTPName.Location = New System.Drawing.Point(304, 49)
        Me.lblTPName.Name = "lblTPName"
        Me.lblTPName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTPName.Size = New System.Drawing.Size(86, 19)
        Me.lblTPName.TabIndex = 5
        Me.lblTPName.Text = "TPS#####"
        '
        'lblAddress
        '
        Me.lblAddress.AutoSize = True
        Me.lblAddress.BackColor = System.Drawing.Color.Transparent
        Me.lblAddress.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAddress.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddress.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAddress.Location = New System.Drawing.Point(329, 244)
        Me.lblAddress.Name = "lblAddress"
        Me.lblAddress.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAddress.Size = New System.Drawing.Size(55, 14)
        Me.lblAddress.TabIndex = 2
        Me.lblAddress.Text = "City, State"
        '
        'lblCompany
        '
        Me.lblCompany.AutoSize = True
        Me.lblCompany.BackColor = System.Drawing.Color.Transparent
        Me.lblCompany.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCompany.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCompany.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCompany.Location = New System.Drawing.Point(329, 230)
        Me.lblCompany.Name = "lblCompany"
        Me.lblCompany.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCompany.Size = New System.Drawing.Size(82, 14)
        Me.lblCompany.TabIndex = 1
        Me.lblCompany.Text = "Company Name"
        '
        'lblTPVersion
        '
        Me.lblTPVersion.AutoSize = True
        Me.lblTPVersion.BackColor = System.Drawing.Color.Transparent
        Me.lblTPVersion.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTPVersion.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTPVersion.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTPVersion.Location = New System.Drawing.Point(304, 68)
        Me.lblTPVersion.Name = "lblTPVersion"
        Me.lblTPVersion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTPVersion.Size = New System.Drawing.Size(99, 19)
        Me.lblTPVersion.TabIndex = 3
        Me.lblTPVersion.Text = "Version: #.#"
        '
        'lblPlatform
        '
        Me.lblPlatform.AutoSize = True
        Me.lblPlatform.BackColor = System.Drawing.Color.Transparent
        Me.lblPlatform.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPlatform.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPlatform.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPlatform.Location = New System.Drawing.Point(305, 19)
        Me.lblPlatform.Name = "lblPlatform"
        Me.lblPlatform.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPlatform.Size = New System.Drawing.Size(143, 22)
        Me.lblPlatform.TabIndex = 4
        Me.lblPlatform.Text = "Test Program:"
        '
        'ShapeContainer1
        '
        Me.ShapeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.ShapeContainer1.Margin = New System.Windows.Forms.Padding(0)
        Me.ShapeContainer1.Name = "ShapeContainer1"
        Me.ShapeContainer1.Shapes.AddRange(New Microsoft.VisualBasic.PowerPacks.Shape() {Me.LineShape1})
        Me.ShapeContainer1.Size = New System.Drawing.Size(482, 274)
        Me.ShapeContainer1.TabIndex = 17
        Me.ShapeContainer1.TabStop = False
        '
        'LineShape1
        '
        Me.LineShape1.BorderColor = System.Drawing.Color.DimGray
        Me.LineShape1.BorderWidth = 3
        Me.LineShape1.Name = "LineShape1"
        Me.LineShape1.X1 = 304
        Me.LineShape1.X2 = 470
        Me.LineShape1.Y1 = 136
        Me.LineShape1.Y2 = 136
        '
        'SplashTimer
        '
        Me.SplashTimer.Enabled = True
        Me.SplashTimer.Interval = 5000
        '
        'imgLogo
        '
        Me.imgLogo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.imgLogo.Cursor = System.Windows.Forms.Cursors.Default
        Me.imgLogo.Image = CType(resources.GetObject("imgLogo.Image"), System.Drawing.Image)
        Me.imgLogo.Location = New System.Drawing.Point(9, 15)
        Me.imgLogo.Name = "imgLogo"
        Me.imgLogo.Size = New System.Drawing.Size(279, 183)
        Me.imgLogo.TabIndex = 16
        Me.imgLogo.TabStop = False
        '
        'frmSplash
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(151, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(500, 289)
        Me.ControlBox = False
        Me.Controls.Add(Me.Frame1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(17, 94)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSplash"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        CType(Me.imgLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

End Sub
 Friend WithEvents ShapeContainer1 As Microsoft.VisualBasic.PowerPacks.ShapeContainer
 Friend WithEvents LineShape1 As Microsoft.VisualBasic.PowerPacks.LineShape
#End Region 
End Class