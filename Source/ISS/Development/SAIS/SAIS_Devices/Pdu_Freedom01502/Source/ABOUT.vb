
Imports System.Windows.Forms

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
    Friend WithEvents panTitle As System.Windows.Forms.Panel
    Friend WithEvents SSPanel1 As System.Windows.Forms.Panel
    Friend WithEvents lblAttributeStatus As System.Windows.Forms.Label
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents lblInstrumentResourceName As System.Windows.Forms.Label
    Friend WithEvents lblAttributeSAISVersion As System.Windows.Forms.Label
    Friend WithEvents lblAttributeManufacturer As System.Windows.Forms.Label
    Friend WithEvents lblAttributeModel As System.Windows.Forms.Label
    Friend WithEvents lblInstrumentManufacturer As System.Windows.Forms.Label
    Friend WithEvents lblInstrumentModel As System.Windows.Forms.Label
    Friend WithEvents lblAttributeResourceName As System.Windows.Forms.Label
    Friend WithEvents lblInstrumentSAISVersion As System.Windows.Forms.Label
    Friend WithEvents cmdOk As System.Windows.Forms.Button
    Friend WithEvents Image1 As System.Windows.Forms.PictureBox

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAbout))
        Me.panTitle = New System.Windows.Forms.Panel()
        Me.SSPanel1 = New System.Windows.Forms.Panel()
        Me.lblAttributeStatus = New System.Windows.Forms.Label()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.lblInstrumentResourceName = New System.Windows.Forms.Label()
        Me.lblAttributeSAISVersion = New System.Windows.Forms.Label()
        Me.lblAttributeManufacturer = New System.Windows.Forms.Label()
        Me.lblAttributeModel = New System.Windows.Forms.Label()
        Me.lblInstrumentManufacturer = New System.Windows.Forms.Label()
        Me.lblInstrumentModel = New System.Windows.Forms.Label()
        Me.lblAttributeResourceName = New System.Windows.Forms.Label()
        Me.lblInstrumentSAISVersion = New System.Windows.Forms.Label()
        Me.cmdOk = New System.Windows.Forms.Button()
        Me.Image1 = New System.Windows.Forms.PictureBox()
        Me.SSPanel1.SuspendLayout()
        CType(Me.Image1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'panTitle
        '
        Me.panTitle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.01!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.panTitle.Location = New System.Drawing.Point(49, 4)
        Me.panTitle.Name = "panTitle"
        Me.panTitle.Size = New System.Drawing.Size(204, 21)
        Me.panTitle.TabIndex = 0
        '
        'SSPanel1
        '
        Me.SSPanel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SSPanel1.Controls.Add(Me.lblAttributeStatus)
        Me.SSPanel1.Controls.Add(Me.lblStatus)
        Me.SSPanel1.Controls.Add(Me.lblInstrumentResourceName)
        Me.SSPanel1.Controls.Add(Me.lblAttributeSAISVersion)
        Me.SSPanel1.Controls.Add(Me.lblAttributeManufacturer)
        Me.SSPanel1.Controls.Add(Me.lblAttributeModel)
        Me.SSPanel1.Controls.Add(Me.lblInstrumentManufacturer)
        Me.SSPanel1.Controls.Add(Me.lblInstrumentModel)
        Me.SSPanel1.Controls.Add(Me.lblAttributeResourceName)
        Me.SSPanel1.Controls.Add(Me.lblInstrumentSAISVersion)
        Me.SSPanel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SSPanel1.Location = New System.Drawing.Point(8, 53)
        Me.SSPanel1.Name = "SSPanel1"
        Me.SSPanel1.Size = New System.Drawing.Size(358, 134)
        Me.SSPanel1.TabIndex = 2
        '
        'lblAttributeStatus
        '
        Me.lblAttributeStatus.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblAttributeStatus.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAttributeStatus.Location = New System.Drawing.Point(8, 106)
        Me.lblAttributeStatus.Name = "lblAttributeStatus"
        Me.lblAttributeStatus.Size = New System.Drawing.Size(122, 19)
        Me.lblAttributeStatus.TabIndex = 12
        Me.lblAttributeStatus.Text = "Instrument Status:"
        '
        'lblStatus
        '
        Me.lblStatus.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblStatus.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblStatus.Location = New System.Drawing.Point(130, 108)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(220, 15)
        Me.lblStatus.TabIndex = 11
        Me.lblStatus.Text = "Initializing PPU..."
        '
        'lblInstrumentResourceName
        '
        Me.lblInstrumentResourceName.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblInstrumentResourceName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblInstrumentResourceName.Location = New System.Drawing.Point(130, 65)
        Me.lblInstrumentResourceName.Name = "lblInstrumentResourceName"
        Me.lblInstrumentResourceName.Size = New System.Drawing.Size(220, 17)
        Me.lblInstrumentResourceName.TabIndex = 10
        Me.lblInstrumentResourceName.Text = "lblInstrument Information"
        '
        'lblAttributeSAISVersion
        '
        Me.lblAttributeSAISVersion.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblAttributeSAISVersion.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAttributeSAISVersion.Location = New System.Drawing.Point(8, 85)
        Me.lblAttributeSAISVersion.Name = "lblAttributeSAISVersion"
        Me.lblAttributeSAISVersion.Size = New System.Drawing.Size(121, 19)
        Me.lblAttributeSAISVersion.TabIndex = 9
        Me.lblAttributeSAISVersion.Text = "SAIS Version:"
        '
        'lblAttributeManufacturer
        '
        Me.lblAttributeManufacturer.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblAttributeManufacturer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAttributeManufacturer.Location = New System.Drawing.Point(8, 3)
        Me.lblAttributeManufacturer.Name = "lblAttributeManufacturer"
        Me.lblAttributeManufacturer.Size = New System.Drawing.Size(121, 19)
        Me.lblAttributeManufacturer.TabIndex = 8
        Me.lblAttributeManufacturer.Text = "Manufacturer:"
        '
        'lblAttributeModel
        '
        Me.lblAttributeModel.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblAttributeModel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAttributeModel.Location = New System.Drawing.Point(8, 43)
        Me.lblAttributeModel.Name = "lblAttributeModel"
        Me.lblAttributeModel.Size = New System.Drawing.Size(121, 19)
        Me.lblAttributeModel.TabIndex = 7
        Me.lblAttributeModel.Text = "Model:"
        '
        'lblInstrumentManufacturer
        '
        Me.lblInstrumentManufacturer.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblInstrumentManufacturer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblInstrumentManufacturer.Location = New System.Drawing.Point(130, 3)
        Me.lblInstrumentManufacturer.Name = "lblInstrumentManufacturer"
        Me.lblInstrumentManufacturer.Size = New System.Drawing.Size(220, 40)
        Me.lblInstrumentManufacturer.TabIndex = 6
        Me.lblInstrumentManufacturer.Text = "lblInstrument Information"
        '
        'lblInstrumentModel
        '
        Me.lblInstrumentModel.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblInstrumentModel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblInstrumentModel.Location = New System.Drawing.Point(130, 44)
        Me.lblInstrumentModel.Name = "lblInstrumentModel"
        Me.lblInstrumentModel.Size = New System.Drawing.Size(220, 17)
        Me.lblInstrumentModel.TabIndex = 5
        Me.lblInstrumentModel.Text = "lblInstrument Information"
        '
        'lblAttributeResourceName
        '
        Me.lblAttributeResourceName.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblAttributeResourceName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAttributeResourceName.Location = New System.Drawing.Point(8, 64)
        Me.lblAttributeResourceName.Name = "lblAttributeResourceName"
        Me.lblAttributeResourceName.Size = New System.Drawing.Size(121, 19)
        Me.lblAttributeResourceName.TabIndex = 4
        Me.lblAttributeResourceName.Text = "Resource Names:"
        '
        'lblInstrumentSAISVersion
        '
        Me.lblInstrumentSAISVersion.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblInstrumentSAISVersion.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblInstrumentSAISVersion.Location = New System.Drawing.Point(130, 87)
        Me.lblInstrumentSAISVersion.Name = "lblInstrumentSAISVersion"
        Me.lblInstrumentSAISVersion.Size = New System.Drawing.Size(220, 15)
        Me.lblInstrumentSAISVersion.TabIndex = 3
        Me.lblInstrumentSAISVersion.Text = "lblInstrument Information"
        '
        'cmdOk
        '
        Me.cmdOk.Location = New System.Drawing.Point(244, 198)
        Me.cmdOk.Name = "cmdOk"
        Me.cmdOk.Size = New System.Drawing.Size(90, 25)
        Me.cmdOk.TabIndex = 1
        Me.cmdOk.Text = "Ok"
        '
        'Image1
        '
        Me.Image1.BackColor = System.Drawing.SystemColors.Control
        Me.Image1.Image = CType(resources.GetObject("Image1.Image"), System.Drawing.Image)
        Me.Image1.Location = New System.Drawing.Point(8, 8)
        Me.Image1.Name = "Image1"
        Me.Image1.Size = New System.Drawing.Size(32, 32)
        Me.Image1.TabIndex = 4
        Me.Image1.TabStop = False
        '
        'frmAbout
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(376, 234)
        Me.ControlBox = False
        Me.Controls.Add(Me.panTitle)
        Me.Controls.Add(Me.SSPanel1)
        Me.Controls.Add(Me.cmdOk)
        Me.Controls.Add(Me.Image1)
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAbout"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "SAIS"
        Me.SSPanel1.ResumeLayout(False)
        CType(Me.Image1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

End Sub

#End Region

	'=========================================================

    Private Sub cmdOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdOk.Click
        Me.Close()
    End Sub

    Private Sub frmAbout_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CenterForm(Me)
        panTitle.Text = INSTRUMENT_NAME

        'JRC Added 033009
        Dim Version As String
        Version = GatherIniFileInformation("System Startup", "SWR", "Unknown")

        lblInstrumentManufacturer.Text = MANUF
        lblInstrumentModel.Text = MODEL_CODE
        lblInstrumentResourceName.Text = RESOURCE_NAME
        lblInstrumentSAISVersion.Text = Version
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub lblInstrument_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblInstrumentManufacturer.DoubleClick, lblInstrumentModel.DoubleClick, lblInstrumentResourceName.DoubleClick, lblInstrumentSAISVersion.DoubleClick
        On Error Resume Next 'To prevent resize error while maximized
        With frmAPS6062
            If .SSPanelAuxCMD.Visible = False Then
                .SSPanelAuxCMD.Visible = True
                .Width = 775
            Else
                .SSPanelAuxCMD.Visible = False
                .Width = 627
                .fraEEWriteData.Visible = False
                .fraEEWriteAdd.Visible = False
                .fraEEReadData.Visible = False
                .fraEEReadAdd.Visible = False
                .fraCurrentSN.Visible = False
                .fraLastCalDate.Visible = False
                .fraNewSN.Visible = False
            End If
        End With
    End Sub
End Class