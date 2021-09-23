
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
    Friend WithEvents lblAttribute As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    Friend WithEvents lblInstrument As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    Friend WithEvents AppPicture As System.Windows.Forms.PictureBox
    Friend WithEvents panTitle As System.Windows.Forms.Panel
    Friend WithEvents SSPanel1 As System.Windows.Forms.Panel
    Friend WithEvents lblAttribute_0 As System.Windows.Forms.Label
    Friend WithEvents lblAttribute_1 As System.Windows.Forms.Label
    Friend WithEvents lblInstrument_0 As System.Windows.Forms.Label
    Friend WithEvents lblInstrument_1 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAbout))
        Me.lblAttribute = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.lblAttribute_0 = New System.Windows.Forms.Label()
        Me.lblAttribute_1 = New System.Windows.Forms.Label()
        Me.lblInstrument = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.lblInstrument_0 = New System.Windows.Forms.Label()
        Me.lblInstrument_1 = New System.Windows.Forms.Label()
        Me.AppPicture = New System.Windows.Forms.PictureBox()
        Me.panTitle = New System.Windows.Forms.Panel()
        Me.SSPanel1 = New System.Windows.Forms.Panel()
        CType(Me.lblAttribute, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblInstrument, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AppPicture, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SSPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblAttribute_0
        '
        Me.lblAttribute_0.AutoSize = True
        Me.lblAttribute_0.BackColor = System.Drawing.SystemColors.Control
        Me.lblAttribute_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAttribute.SetIndex(Me.lblAttribute_0, CType(0, Short))
        Me.lblAttribute_0.Location = New System.Drawing.Point(8, 1)
        Me.lblAttribute_0.Name = "lblAttribute_0"
        Me.lblAttribute_0.Size = New System.Drawing.Size(44, 13)
        Me.lblAttribute_0.TabIndex = 5
        Me.lblAttribute_0.Text = "System:"
        '
        'lblAttribute_1
        '
        Me.lblAttribute_1.AutoSize = True
        Me.lblAttribute_1.BackColor = System.Drawing.SystemColors.Control
        Me.lblAttribute_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAttribute.SetIndex(Me.lblAttribute_1, CType(1, Short))
        Me.lblAttribute_1.Location = New System.Drawing.Point(8, 17)
        Me.lblAttribute_1.Name = "lblAttribute_1"
        Me.lblAttribute_1.Size = New System.Drawing.Size(45, 13)
        Me.lblAttribute_1.TabIndex = 4
        Me.lblAttribute_1.Text = "Version:"
        '
        'lblInstrument_0
        '
        Me.lblInstrument_0.BackColor = System.Drawing.SystemColors.Control
        Me.lblInstrument_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblInstrument.SetIndex(Me.lblInstrument_0, CType(0, Short))
        Me.lblInstrument_0.Location = New System.Drawing.Point(69, 1)
        Me.lblInstrument_0.Name = "lblInstrument_0"
        Me.lblInstrument_0.Size = New System.Drawing.Size(171, 17)
        Me.lblInstrument_0.TabIndex = 3
        Me.lblInstrument_0.Text = "ATS"
        '
        'lblInstrument_1
        '
        Me.lblInstrument_1.BackColor = System.Drawing.SystemColors.Control
        Me.lblInstrument_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblInstrument.SetIndex(Me.lblInstrument_1, CType(1, Short))
        Me.lblInstrument_1.Location = New System.Drawing.Point(69, 17)
        Me.lblInstrument_1.Name = "lblInstrument_1"
        Me.lblInstrument_1.Size = New System.Drawing.Size(134, 17)
        Me.lblInstrument_1.TabIndex = 2
        Me.lblInstrument_1.Text = "102.00"
        '
        'AppPicture
        '
        Me.AppPicture.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.AppPicture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.AppPicture.Image = CType(resources.GetObject("AppPicture.Image"), System.Drawing.Image)
        Me.AppPicture.Location = New System.Drawing.Point(8, 2)
        Me.AppPicture.Name = "AppPicture"
        Me.AppPicture.Size = New System.Drawing.Size(34, 34)
        Me.AppPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.AppPicture.TabIndex = 6
        Me.AppPicture.TabStop = False
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
        Me.SSPanel1.Controls.Add(Me.lblAttribute_0)
        Me.SSPanel1.Controls.Add(Me.lblAttribute_1)
        Me.SSPanel1.Controls.Add(Me.lblInstrument_0)
        Me.SSPanel1.Controls.Add(Me.lblInstrument_1)
        Me.SSPanel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.16!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SSPanel1.Location = New System.Drawing.Point(8, 40)
        Me.SSPanel1.Name = "SSPanel1"
        Me.SSPanel1.Size = New System.Drawing.Size(244, 38)
        Me.SSPanel1.TabIndex = 1
        '
        'frmAbout
        '
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(258, 88)
        Me.Controls.Add(Me.AppPicture)
        Me.Controls.Add(Me.panTitle)
        Me.Controls.Add(Me.SSPanel1)
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAbout"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "About"
        CType(Me.lblAttribute, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblInstrument, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AppPicture, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SSPanel1.ResumeLayout(False)
        Me.SSPanel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

	'=========================================================
    '************************************************************
    '* Virtual Instrument Portable Equipment Repair/Test(VIPERT)*
    '*                                                          *
    '* Nomenclature   : SAIS: About Box Template                *
    '* Version        : 1.0                                     *
    '* Written By     : David W. Hartley                        *
    '* Last Update    : 11/13/01                                *
    '* Purpose        : This module displays the ABOUT BOX      *
    '************************************************************


    Private Sub frmAbout_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim Version As String
        Version = GatherIniFileInformation("System Startup", "SWR", "Unknown")

        'Updates Version number to reflect Project Properties.  DJoiner  11/13/01
        lblInstrument(1).Text = Version
    End Sub

End Class