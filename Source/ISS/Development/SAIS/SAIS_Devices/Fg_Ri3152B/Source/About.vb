
Imports System.Windows.Forms
Imports System.Windows.Forms.Screen

Public Class frmAbout
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents Image1 As System.Windows.Forms.PictureBox
    Friend WithEvents cmdOK As System.Windows.Forms.Button
    Friend WithEvents panTitle As System.Windows.Forms.Panel
    Friend WithEvents SSPanel1 As System.Windows.Forms.Panel
    Friend WithEvents lblInstrumentResourceName As System.Windows.Forms.Label
    Friend WithEvents lblAttributeSAISVersion As System.Windows.Forms.Label
    Friend WithEvents lblAttributeManufacturer As System.Windows.Forms.Label
    Friend WithEvents lblAttributeModel As System.Windows.Forms.Label
    Friend WithEvents lblInstrumentManufacturer As System.Windows.Forms.Label
    Friend WithEvents lblInstrumentModel As System.Windows.Forms.Label
    Friend WithEvents lblAttributeResourceName As System.Windows.Forms.Label
    Friend WithEvents lblInstrumentSAISVersion As System.Windows.Forms.Label
    Friend WithEvents Image2 As System.Windows.Forms.PictureBox
    Friend WithEvents Image1_0 As System.Windows.Forms.PictureBox

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAbout))
        Me.Image1 = New System.Windows.Forms.PictureBox()
        Me.cmdOK = New System.Windows.Forms.Button()
        Me.panTitle = New System.Windows.Forms.Panel()
        Me.SSPanel1 = New System.Windows.Forms.Panel()
        Me.lblInstrumentResourceName = New System.Windows.Forms.Label()
        Me.lblAttributeSAISVersion = New System.Windows.Forms.Label()
        Me.lblAttributeManufacturer = New System.Windows.Forms.Label()
        Me.lblAttributeModel = New System.Windows.Forms.Label()
        Me.lblInstrumentManufacturer = New System.Windows.Forms.Label()
        Me.lblInstrumentModel = New System.Windows.Forms.Label()
        Me.lblAttributeResourceName = New System.Windows.Forms.Label()
        Me.lblInstrumentSAISVersion = New System.Windows.Forms.Label()
        Me.Image2 = New System.Windows.Forms.PictureBox()
        Me.Image1_0 = New System.Windows.Forms.PictureBox()
        CType(Me.Image1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SSPanel1.SuspendLayout()
        CType(Me.Image2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Image1_0, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Image1
        '
        Me.Image1.Location = New System.Drawing.Point(0, 0)
        Me.Image1.Name = "Image1"
        Me.Image1.Size = New System.Drawing.Size(100, 50)
        Me.Image1.TabIndex = 0
        Me.Image1.TabStop = False
        '
        'cmdOK
        '
        Me.cmdOK.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOK.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOK.Location = New System.Drawing.Point(186, 131)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.Size = New System.Drawing.Size(76, 23)
        Me.cmdOK.TabIndex = 10
        Me.cmdOK.Text = "OK"
        Me.cmdOK.UseVisualStyleBackColor = False
        '
        'panTitle
        '
        Me.panTitle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.01!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.panTitle.Location = New System.Drawing.Point(46, 8)
        Me.panTitle.Name = "panTitle"
        Me.panTitle.Size = New System.Drawing.Size(227, 21)
        Me.panTitle.TabIndex = 0
        '
        'SSPanel1
        '
        Me.SSPanel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SSPanel1.Controls.Add(Me.lblInstrumentResourceName)
        Me.SSPanel1.Controls.Add(Me.lblAttributeSAISVersion)
        Me.SSPanel1.Controls.Add(Me.lblAttributeManufacturer)
        Me.SSPanel1.Controls.Add(Me.lblAttributeModel)
        Me.SSPanel1.Controls.Add(Me.lblInstrumentManufacturer)
        Me.SSPanel1.Controls.Add(Me.lblInstrumentModel)
        Me.SSPanel1.Controls.Add(Me.lblAttributeResourceName)
        Me.SSPanel1.Controls.Add(Me.lblInstrumentSAISVersion)
        Me.SSPanel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SSPanel1.Location = New System.Drawing.Point(8, 40)
        Me.SSPanel1.Name = "SSPanel1"
        Me.SSPanel1.Size = New System.Drawing.Size(277, 75)
        Me.SSPanel1.TabIndex = 1
        '
        'lblInstrumentResourceName
        '
        Me.lblInstrumentResourceName.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblInstrumentResourceName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblInstrumentResourceName.Location = New System.Drawing.Point(125, 33)
        Me.lblInstrumentResourceName.Name = "lblInstrumentResourceName"
        Me.lblInstrumentResourceName.Size = New System.Drawing.Size(150, 17)
        Me.lblInstrumentResourceName.TabIndex = 9
        Me.lblInstrumentResourceName.Text = "lblInstrument Information"
        '
        'lblAttributeSAISVersion
        '
        Me.lblAttributeSAISVersion.AutoSize = True
        Me.lblAttributeSAISVersion.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblAttributeSAISVersion.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAttributeSAISVersion.Location = New System.Drawing.Point(8, 50)
        Me.lblAttributeSAISVersion.Name = "lblAttributeSAISVersion"
        Me.lblAttributeSAISVersion.Size = New System.Drawing.Size(72, 13)
        Me.lblAttributeSAISVersion.TabIndex = 8
        Me.lblAttributeSAISVersion.Text = "SAIS Version:"
        '
        'lblAttributeManufacturer
        '
        Me.lblAttributeManufacturer.AutoSize = True
        Me.lblAttributeManufacturer.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblAttributeManufacturer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAttributeManufacturer.Location = New System.Drawing.Point(8, 1)
        Me.lblAttributeManufacturer.Name = "lblAttributeManufacturer"
        Me.lblAttributeManufacturer.Size = New System.Drawing.Size(73, 13)
        Me.lblAttributeManufacturer.TabIndex = 7
        Me.lblAttributeManufacturer.Text = "Manufacturer:"
        '
        'lblAttributeModel
        '
        Me.lblAttributeModel.AutoSize = True
        Me.lblAttributeModel.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblAttributeModel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAttributeModel.Location = New System.Drawing.Point(8, 17)
        Me.lblAttributeModel.Name = "lblAttributeModel"
        Me.lblAttributeModel.Size = New System.Drawing.Size(39, 13)
        Me.lblAttributeModel.TabIndex = 6
        Me.lblAttributeModel.Text = "Model:"
        '
        'lblInstrumentManufacturer
        '
        Me.lblInstrumentManufacturer.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblInstrumentManufacturer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblInstrumentManufacturer.Location = New System.Drawing.Point(125, 1)
        Me.lblInstrumentManufacturer.Name = "lblInstrumentManufacturer"
        Me.lblInstrumentManufacturer.Size = New System.Drawing.Size(150, 17)
        Me.lblInstrumentManufacturer.TabIndex = 5
        Me.lblInstrumentManufacturer.Text = "lblInstrument Information"
        '
        'lblInstrumentModel
        '
        Me.lblInstrumentModel.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblInstrumentModel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblInstrumentModel.Location = New System.Drawing.Point(125, 17)
        Me.lblInstrumentModel.Name = "lblInstrumentModel"
        Me.lblInstrumentModel.Size = New System.Drawing.Size(150, 17)
        Me.lblInstrumentModel.TabIndex = 4
        Me.lblInstrumentModel.Text = "lblInstrument Information"
        '
        'lblAttributeResourceName
        '
        Me.lblAttributeResourceName.AutoSize = True
        Me.lblAttributeResourceName.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblAttributeResourceName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAttributeResourceName.Location = New System.Drawing.Point(8, 33)
        Me.lblAttributeResourceName.Name = "lblAttributeResourceName"
        Me.lblAttributeResourceName.Size = New System.Drawing.Size(87, 13)
        Me.lblAttributeResourceName.TabIndex = 3
        Me.lblAttributeResourceName.Text = "Resource Name:"
        '
        'lblInstrumentSAISVersion
        '
        Me.lblInstrumentSAISVersion.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblInstrumentSAISVersion.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblInstrumentSAISVersion.Location = New System.Drawing.Point(125, 49)
        Me.lblInstrumentSAISVersion.Name = "lblInstrumentSAISVersion"
        Me.lblInstrumentSAISVersion.Size = New System.Drawing.Size(150, 15)
        Me.lblInstrumentSAISVersion.TabIndex = 2
        Me.lblInstrumentSAISVersion.Text = "lblInstrument Information"
        '
        'Image2
        '
        Me.Image2.BackColor = System.Drawing.SystemColors.Control
        Me.Image2.Image = CType(resources.GetObject("Image2.Image"), System.Drawing.Image)
        Me.Image2.Location = New System.Drawing.Point(16, 65)
        Me.Image2.Name = "Image2"
        Me.Image2.Size = New System.Drawing.Size(122, 33)
        Me.Image2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.Image2.TabIndex = 12
        Me.Image2.TabStop = False
        '
        'Image1_0
        '
        Me.Image1_0.BackColor = System.Drawing.SystemColors.Control
        Me.Image1_0.Image = CType(resources.GetObject("Image1_0.Image"), System.Drawing.Image)
        Me.Image1_0.Location = New System.Drawing.Point(8, 4)
        Me.Image1_0.Name = "Image1_0"
        Me.Image1_0.Size = New System.Drawing.Size(32, 32)
        Me.Image1_0.TabIndex = 13
        Me.Image1_0.TabStop = False
        '
        'frmAbout
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(297, 170)
        Me.ControlBox = False
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.panTitle)
        Me.Controls.Add(Me.SSPanel1)
        Me.Controls.Add(Me.Image2)
        Me.Controls.Add(Me.Image1_0)
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAbout"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "SAIS"
        CType(Me.Image1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SSPanel1.ResumeLayout(False)
        Me.SSPanel1.PerformLayout()
        CType(Me.Image2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Image1_0, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

End Sub

#End Region

	'=========================================================
    '************************************************************
    '* Third Echelon Test System (TETS) Software Module         *
    '*                                                          *
    '* Nomenclature   : SAIS: About Box Template                *
    '* Written By     : David W. Hartley                        *
    '* Purpose        : This module displays the SAIS ABOUT BOX *
    '************************************************************

    Private Sub cmdOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        Me.Close()
    End Sub

    Private Sub frmAbout_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim Version As String
        Dim SystemType As String
        'Center this form

        Me.Top = PrimaryScreen.Bounds.Height / 2 - Me.Height / 2
        Me.Left = PrimaryScreen.Bounds.Width/2-Me.Width/2

        SystemType = GatherIniFileInformation("System Startup", "SYSTEM_TYPE", "Unknown")
        Version = GatherIniFileInformation("System Startup", "SWR", "Unknown")

        If (InStr(SystemType, "AN/USM-657(V)2")) Then
            lblInstrumentModel.Text = MODEL_CODE_TETS
        Else
            lblInstrumentModel.Text = MODEL_CODE_VIPER
        End If

        panTitle.Text = INSTRUMENT_NAME
        lblInstrumentManufacturer.Text = MANUF
        lblInstrumentResourceName.Text = RESOURCE_NAME
        lblInstrumentSAISVersion.Text = Version

    End Sub
End Class