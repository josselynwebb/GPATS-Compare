
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
    Friend WithEvents lblAttribute As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    Friend WithEvents lblInstrument As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    Friend WithEvents panTitle As System.Windows.Forms.PictureBox
    Friend WithEvents SSPanel1 As System.Windows.Forms.PictureBox
    Friend WithEvents lblAttribute_3 As System.Windows.Forms.Label
    Friend WithEvents lblAttribute_0 As System.Windows.Forms.Label
    Friend WithEvents lblInstrument_0 As System.Windows.Forms.Label
    Friend WithEvents lblInstrument_3 As System.Windows.Forms.Label
    Friend WithEvents Image1 As System.Windows.Forms.PictureBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAbout))
        Me.lblAttribute = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.lblAttribute_3 = New System.Windows.Forms.Label()
        Me.lblAttribute_0 = New System.Windows.Forms.Label()
        Me.lblInstrument = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.lblInstrument_0 = New System.Windows.Forms.Label()
        Me.lblInstrument_3 = New System.Windows.Forms.Label()
        Me.panTitle = New System.Windows.Forms.PictureBox()
        Me.SSPanel1 = New System.Windows.Forms.PictureBox()
        Me.Image1 = New System.Windows.Forms.PictureBox()
        CType(Me.lblAttribute, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblInstrument, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.panTitle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SSPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SSPanel1.SuspendLayout()
        CType(Me.Image1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblAttribute_3
        '
        Me.lblAttribute_3.AutoSize = True
        Me.lblAttribute_3.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblAttribute_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAttribute.SetIndex(Me.lblAttribute_3, CType(3, Short))
        Me.lblAttribute_3.Location = New System.Drawing.Point(9, 18)
        Me.lblAttribute_3.Name = "lblAttribute_3"
        Me.lblAttribute_3.Size = New System.Drawing.Size(45, 13)
        Me.lblAttribute_3.TabIndex = 5
        Me.lblAttribute_3.Text = "Version:"
        '
        'lblAttribute_0
        '
        Me.lblAttribute_0.AutoSize = True
        Me.lblAttribute_0.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblAttribute_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAttribute.SetIndex(Me.lblAttribute_0, CType(0, Short))
        Me.lblAttribute_0.Location = New System.Drawing.Point(8, 2)
        Me.lblAttribute_0.Name = "lblAttribute_0"
        Me.lblAttribute_0.Size = New System.Drawing.Size(44, 13)
        Me.lblAttribute_0.TabIndex = 4
        Me.lblAttribute_0.Text = "System:"
        '
        'lblInstrument_0
        '
        Me.lblInstrument_0.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblInstrument_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblInstrument.SetIndex(Me.lblInstrument_0, CType(0, Short))
        Me.lblInstrument_0.Location = New System.Drawing.Point(67, 3)
        Me.lblInstrument_0.Name = "lblInstrument_0"
        Me.lblInstrument_0.Size = New System.Drawing.Size(169, 16)
        Me.lblInstrument_0.TabIndex = 3
        Me.lblInstrument_0.Text = "VIPERT"
        '
        'lblInstrument_3
        '
        Me.lblInstrument_3.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblInstrument_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblInstrument.SetIndex(Me.lblInstrument_3, CType(3, Short))
        Me.lblInstrument_3.Location = New System.Drawing.Point(67, 17)
        Me.lblInstrument_3.Name = "lblInstrument_3"
        Me.lblInstrument_3.Size = New System.Drawing.Size(134, 15)
        Me.lblInstrument_3.TabIndex = 2
        Me.lblInstrument_3.Text = "App.Version"
        '
        'panTitle
        '
        Me.panTitle.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.panTitle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panTitle.Location = New System.Drawing.Point(49, 8)
        Me.panTitle.Name = "panTitle"
        Me.panTitle.Size = New System.Drawing.Size(205, 21)
        Me.panTitle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.panTitle.TabIndex = 0
        Me.panTitle.TabStop = False
        '
        'SSPanel1
        '
        Me.SSPanel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.SSPanel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SSPanel1.Controls.Add(Me.lblAttribute_3)
        Me.SSPanel1.Controls.Add(Me.lblAttribute_0)
        Me.SSPanel1.Controls.Add(Me.lblInstrument_0)
        Me.SSPanel1.Controls.Add(Me.lblInstrument_3)
        Me.SSPanel1.Location = New System.Drawing.Point(8, 40)
        Me.SSPanel1.Name = "SSPanel1"
        Me.SSPanel1.Size = New System.Drawing.Size(244, 35)
        Me.SSPanel1.TabIndex = 1
        Me.SSPanel1.TabStop = False
        '
        'Image1
        '
        Me.Image1.BackColor = System.Drawing.SystemColors.Control
        Me.Image1.Image = CType(resources.GetObject("Image1.Image"), System.Drawing.Image)
        Me.Image1.Location = New System.Drawing.Point(8, 3)
        Me.Image1.Name = "Image1"
        Me.Image1.Size = New System.Drawing.Size(32, 32)
        Me.Image1.TabIndex = 2
        Me.Image1.TabStop = False
        '
        'frmAbout
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(259, 125)
        Me.ControlBox = False
        Me.Controls.Add(Me.panTitle)
        Me.Controls.Add(Me.SSPanel1)
        Me.Controls.Add(Me.Image1)
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAbout"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "System Survey"
        CType(Me.lblAttribute, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblInstrument, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.panTitle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SSPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SSPanel1.ResumeLayout(False)
        Me.SSPanel1.PerformLayout()
        CType(Me.Image1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

	'=========================================================
    '*************************************************************
    '* Virutal Instrument Portable Equipment Repair/Test (VIPERT)*
    '*                                                           *
    '* Nomenclature   : System Confidence Test: About Box        *
    '* Written By     : Michael McCabe                           *
    '* Purpose        : This module displays the ABOUT BOX       *
    '*************************************************************



    Private Sub frmAbout_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim Version As String
        Version = GatherIniFileInformation("System Startup", "SWR", "Unknown")

        'Center this form
        Me.Top = PrimaryScreen.Bounds.Height/2-Me.Height/2
        Me.Left = PrimaryScreen.Bounds.Width/2-Me.Width/2

        'Updates Version number to reflect Project Properties.  DJoiner  07/19/2001
        lblInstrument(3).Text = Version
    End Sub



End Class