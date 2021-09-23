
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
    Friend WithEvents lblInstrument_2 As System.Windows.Forms.Label
    Friend WithEvents lblAttribute_3 As System.Windows.Forms.Label
    Friend WithEvents lblAttribute_0 As System.Windows.Forms.Label
    Friend WithEvents lblAttribute_1 As System.Windows.Forms.Label
    Friend WithEvents lblInstrument_0 As System.Windows.Forms.Label
    Friend WithEvents lblInstrument_1 As System.Windows.Forms.Label
    Friend WithEvents lblAttribute_2 As System.Windows.Forms.Label
    Friend WithEvents lblInstrument_3 As System.Windows.Forms.Label
    Friend WithEvents cmdOk As System.Windows.Forms.Button
    Friend WithEvents Image1 As System.Windows.Forms.PictureBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAbout))
        Me.panTitle = New System.Windows.Forms.Panel()
        Me.SSPanel1 = New System.Windows.Forms.Panel()
        Me.lblInstrument_2 = New System.Windows.Forms.Label()
        Me.lblAttribute_3 = New System.Windows.Forms.Label()
        Me.lblAttribute_0 = New System.Windows.Forms.Label()
        Me.lblAttribute_1 = New System.Windows.Forms.Label()
        Me.lblInstrument_0 = New System.Windows.Forms.Label()
        Me.lblInstrument_1 = New System.Windows.Forms.Label()
        Me.lblAttribute_2 = New System.Windows.Forms.Label()
        Me.lblInstrument_3 = New System.Windows.Forms.Label()
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
        Me.panTitle.Location = New System.Drawing.Point(49, 8)
        Me.panTitle.Name = "panTitle"
        Me.panTitle.Size = New System.Drawing.Size(204, 21)
        Me.panTitle.TabIndex = 0
        '
        'SSPanel1
        '
        Me.SSPanel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SSPanel1.Controls.Add(Me.lblInstrument_2)
        Me.SSPanel1.Controls.Add(Me.lblAttribute_3)
        Me.SSPanel1.Controls.Add(Me.lblAttribute_0)
        Me.SSPanel1.Controls.Add(Me.lblAttribute_1)
        Me.SSPanel1.Controls.Add(Me.lblInstrument_0)
        Me.SSPanel1.Controls.Add(Me.lblInstrument_1)
        Me.SSPanel1.Controls.Add(Me.lblAttribute_2)
        Me.SSPanel1.Controls.Add(Me.lblInstrument_3)
        Me.SSPanel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SSPanel1.Location = New System.Drawing.Point(8, 40)
        Me.SSPanel1.Name = "SSPanel1"
        Me.SSPanel1.Size = New System.Drawing.Size(265, 75)
        Me.SSPanel1.TabIndex = 2
        '
        'lblInstrument_2
        '
        Me.lblInstrument_2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblInstrument_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblInstrument_2.Location = New System.Drawing.Point(126, 33)
        Me.lblInstrument_2.Name = "lblInstrument_2"
        Me.lblInstrument_2.Size = New System.Drawing.Size(134, 17)
        Me.lblInstrument_2.TabIndex = 10
        Me.lblInstrument_2.Text = "lblInstrument Information"
        '
        'lblAttribute_3
        '
        Me.lblAttribute_3.AutoSize = True
        Me.lblAttribute_3.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblAttribute_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAttribute_3.Location = New System.Drawing.Point(8, 50)
        Me.lblAttribute_3.Name = "lblAttribute_3"
        Me.lblAttribute_3.Size = New System.Drawing.Size(72, 13)
        Me.lblAttribute_3.TabIndex = 9
        Me.lblAttribute_3.Text = "SAIS Version:"
        '
        'lblAttribute_0
        '
        Me.lblAttribute_0.AutoSize = True
        Me.lblAttribute_0.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblAttribute_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAttribute_0.Location = New System.Drawing.Point(8, 1)
        Me.lblAttribute_0.Name = "lblAttribute_0"
        Me.lblAttribute_0.Size = New System.Drawing.Size(73, 13)
        Me.lblAttribute_0.TabIndex = 8
        Me.lblAttribute_0.Text = "Manufacturer:"
        '
        'lblAttribute_1
        '
        Me.lblAttribute_1.AutoSize = True
        Me.lblAttribute_1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblAttribute_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAttribute_1.Location = New System.Drawing.Point(8, 17)
        Me.lblAttribute_1.Name = "lblAttribute_1"
        Me.lblAttribute_1.Size = New System.Drawing.Size(39, 13)
        Me.lblAttribute_1.TabIndex = 7
        Me.lblAttribute_1.Text = "Model:"
        '
        'lblInstrument_0
        '
        Me.lblInstrument_0.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblInstrument_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblInstrument_0.Location = New System.Drawing.Point(126, 1)
        Me.lblInstrument_0.Name = "lblInstrument_0"
        Me.lblInstrument_0.Size = New System.Drawing.Size(134, 17)
        Me.lblInstrument_0.TabIndex = 6
        Me.lblInstrument_0.Text = "lblInstrument Information"
        '
        'lblInstrument_1
        '
        Me.lblInstrument_1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblInstrument_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblInstrument_1.Location = New System.Drawing.Point(126, 17)
        Me.lblInstrument_1.Name = "lblInstrument_1"
        Me.lblInstrument_1.Size = New System.Drawing.Size(134, 17)
        Me.lblInstrument_1.TabIndex = 5
        Me.lblInstrument_1.Text = "lblInstrument Information"
        '
        'lblAttribute_2
        '
        Me.lblAttribute_2.AutoSize = True
        Me.lblAttribute_2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblAttribute_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAttribute_2.Location = New System.Drawing.Point(8, 33)
        Me.lblAttribute_2.Name = "lblAttribute_2"
        Me.lblAttribute_2.Size = New System.Drawing.Size(87, 13)
        Me.lblAttribute_2.TabIndex = 4
        Me.lblAttribute_2.Text = "Resource Name:"
        '
        'lblInstrument_3
        '
        Me.lblInstrument_3.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblInstrument_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblInstrument_3.Location = New System.Drawing.Point(126, 50)
        Me.lblInstrument_3.Name = "lblInstrument_3"
        Me.lblInstrument_3.Size = New System.Drawing.Size(134, 15)
        Me.lblInstrument_3.TabIndex = 3
        Me.lblInstrument_3.Text = "lblInstrument Information"
        '
        'cmdOk
        '
        Me.cmdOk.Location = New System.Drawing.Point(176, 128)
        Me.cmdOk.Name = "cmdOk"
        Me.cmdOk.Size = New System.Drawing.Size(90, 25)
        Me.cmdOk.TabIndex = 1
        Me.cmdOk.Text = "&Ok"
        '
        'Image1
        '
        Me.Image1.BackColor = System.Drawing.SystemColors.Control
        Me.Image1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Image1.Image = CType(resources.GetObject("Image1.Image"), System.Drawing.Image)
        Me.Image1.Location = New System.Drawing.Point(-1, 0)
        Me.Image1.Name = "Image1"
        Me.Image1.Size = New System.Drawing.Size(32, 32)
        Me.Image1.TabIndex = 12
        Me.Image1.TabStop = False
        '
        'frmAbout
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(285, 159)
        Me.ControlBox = False
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
        Me.SSPanel1.PerformLayout()
        CType(Me.Image1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

End Sub

#End Region

	'=========================================================
    '*Put the following items to your MAIN.BAS (Customize it to your instrument):
    '-----------------Global Constant Values------------------------------
    'Global Const INSTRUMENT_NAME$ = "RF Signal Generator"
    'Global Const MANUF$ = "EIP Microwave Inc."
    'Global Const MODEL_CODE$ = "EIP1143A"
    'Global Const RESOURCE_NAME$ = "VXI::144"
    'Global Const SAIS_VERSION$ = "1.0"
    'Global frmParent As Object
    '
    '*Add following line to your Sub Main:
    '   Set frmParent = frmRFSG   'Name of your Main Form
    '
    '*Example:
    'Sub cmdAbout_Click()
    '    frmAbout.Show 1
    'End Sub
    '
    '*For Splash Screen purpose, use following two commands (Available in TETSLIB.BAS):
    '   SplashStart
    '   SplashEnd

    Private Sub cmdOk_MouseClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdOk.Click
        Me.Close()
    End Sub

    Private Sub frmAbout_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CenterForm(Me)
        panTitle.Text = INSTRUMENT_NAME

        'JRC Added 033009
        Dim Version As String
        Dim SystemType As String
        Version = GatherIniFileInformation("System Startup", "SWR", "Unknown")
        SystemType = GatherIniFileInformation("System Startup", "SYSTEM_TYPE", "Unknown")

        If (InStr(SystemType, "AN/USM-657(V)2")) Then
            lblInstrument_0.Text = "Hewlett Packard"
            lblInstrument_1.Text = "E1412A"
            lblInstrument_2.Text = RESOURCE_NAME
            'lblInstrument(3).Caption = App.Major & "." & App.Minor
            lblInstrument_3.Text = Version
        Else
            lblInstrument_0.Text = MANUF
            lblInstrument_1.Text = MODEL_CODE
            lblInstrument_2.Text = RESOURCE_NAME
            'lblInstrument(3).Caption = App.Major & "." & App.Minor
            lblInstrument_3.Text = Version
        End If


    End Sub
End Class