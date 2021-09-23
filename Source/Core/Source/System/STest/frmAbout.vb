
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
    Friend WithEvents Picture1 As System.Windows.Forms.PictureBox
    Friend WithEvents lblInstrument_3 As System.Windows.Forms.Label
    Friend WithEvents lblInstrument_0 As System.Windows.Forms.Label
    Friend WithEvents lblAttribute_3 As System.Windows.Forms.Label
    Friend WithEvents lblAttribute_0 As System.Windows.Forms.Label
    Friend WithEvents panTitle As System.Windows.Forms.Label
    Friend WithEvents Image1 As System.Windows.Forms.PictureBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAbout))
        Me.Picture1 = New System.Windows.Forms.PictureBox()
        Me.lblInstrument_3 = New System.Windows.Forms.Label()
        Me.lblInstrument_0 = New System.Windows.Forms.Label()
        Me.lblAttribute_3 = New System.Windows.Forms.Label()
        Me.lblAttribute_0 = New System.Windows.Forms.Label()
        Me.panTitle = New System.Windows.Forms.Label()
        Me.Image1 = New System.Windows.Forms.PictureBox()
        CType(Me.Picture1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Picture1.SuspendLayout()
        CType(Me.Image1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Picture1
        '
        Me.Picture1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Picture1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Picture1.Controls.Add(Me.lblInstrument_3)
        Me.Picture1.Controls.Add(Me.lblInstrument_0)
        Me.Picture1.Controls.Add(Me.lblAttribute_3)
        Me.Picture1.Controls.Add(Me.lblAttribute_0)
        Me.Picture1.Location = New System.Drawing.Point(8, 40)
        Me.Picture1.Name = "Picture1"
        Me.Picture1.Size = New System.Drawing.Size(260, 58)
        Me.Picture1.TabIndex = 1
        Me.Picture1.TabStop = False
        '
        'lblInstrument_3
        '
        Me.lblInstrument_3.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblInstrument_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblInstrument_3.Location = New System.Drawing.Point(69, 32)
        Me.lblInstrument_3.Name = "lblInstrument_3"
        Me.lblInstrument_3.Size = New System.Drawing.Size(134, 15)
        Me.lblInstrument_3.TabIndex = 5
        Me.lblInstrument_3.Text = "1.0 MM DD, YYYY"
        '
        'lblInstrument_0
        '
        Me.lblInstrument_0.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblInstrument_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblInstrument_0.Location = New System.Drawing.Point(69, 3)
        Me.lblInstrument_0.Name = "lblInstrument_0"
        Me.lblInstrument_0.Size = New System.Drawing.Size(187, 33)
        Me.lblInstrument_0.TabIndex = 4
        Me.lblInstrument_0.Text = "ATS-ViperT (Virtual Instrument Portable Equipment Repair/Test)"
        '
        'lblAttribute_3
        '
        Me.lblAttribute_3.AutoSize = True
        Me.lblAttribute_3.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblAttribute_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAttribute_3.Location = New System.Drawing.Point(9, 34)
        Me.lblAttribute_3.Name = "lblAttribute_3"
        Me.lblAttribute_3.Size = New System.Drawing.Size(45, 13)
        Me.lblAttribute_3.TabIndex = 3
        Me.lblAttribute_3.Text = "Version:"
        '
        'lblAttribute_0
        '
        Me.lblAttribute_0.AutoSize = True
        Me.lblAttribute_0.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblAttribute_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAttribute_0.Location = New System.Drawing.Point(9, 3)
        Me.lblAttribute_0.Name = "lblAttribute_0"
        Me.lblAttribute_0.Size = New System.Drawing.Size(44, 13)
        Me.lblAttribute_0.TabIndex = 2
        Me.lblAttribute_0.Text = "System:"
        '
        'panTitle
        '
        Me.panTitle.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.panTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panTitle.ForeColor = System.Drawing.SystemColors.ControlText
        Me.panTitle.Location = New System.Drawing.Point(49, 8)
        Me.panTitle.Name = "panTitle"
        Me.panTitle.Size = New System.Drawing.Size(204, 21)
        Me.panTitle.TabIndex = 0
        Me.panTitle.Text = "System Self Test"
        '
        'Image1
        '
        Me.Image1.BackColor = System.Drawing.SystemColors.Control
        Me.Image1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Image1.Image = CType(resources.GetObject("Image1.Image"), System.Drawing.Image)
        Me.Image1.Location = New System.Drawing.Point(8, 0)
        Me.Image1.Name = "Image1"
        Me.Image1.Size = New System.Drawing.Size(36, 36)
        Me.Image1.TabIndex = 2
        Me.Image1.TabStop = False
        Me.Image1.Visible = False
        '
        'frmAbout
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(272, 155)
        Me.ControlBox = False
        Me.Controls.Add(Me.Picture1)
        Me.Controls.Add(Me.panTitle)
        Me.Controls.Add(Me.Image1)
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAbout"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "System Self Test"
        CType(Me.Picture1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Picture1.ResumeLayout(False)
        Me.Picture1.PerformLayout()
        CType(Me.Image1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    '**************************************************************
    '* Nomenclature   : ATS-ViperT SYSTEM SELF TEST               *
    '*                  About Box                                 *
    '* Version        : 2.0                                       *
    '* Last Update    : Apr 1, 2017                               *
    '* Purpose        : This form displays the ABOUT BOX          *
    '**************************************************************

    Private Sub frmAbout_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim Version As String
        Version = GatherIniFileInformation("System Startup", "SWR", "Unknown")

        lblInstrument_3.Text = Version
        'Note: STversion is now changed in STestMain/Main subroutine

    End Sub


End Class