
Imports System.Windows.Forms

Public Class FrmlaserWarning
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
    Friend WithEvents CMSBYPASS As System.Windows.Forms.Button
    Friend WithEvents CMDCONT As System.Windows.Forms.Button
    Friend WithEvents Image1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmlaserWarning))
        Me.CMSBYPASS = New System.Windows.Forms.Button()
        Me.CMDCONT = New System.Windows.Forms.Button()
        Me.Image1 = New System.Windows.Forms.PictureBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.Image1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CMSBYPASS
        '
        Me.CMSBYPASS.BackColor = System.Drawing.SystemColors.Control
        Me.CMSBYPASS.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CMSBYPASS.Location = New System.Drawing.Point(210, 129)
        Me.CMSBYPASS.Name = "CMSBYPASS"
        Me.CMSBYPASS.Size = New System.Drawing.Size(82, 41)
        Me.CMSBYPASS.TabIndex = 1
        Me.CMSBYPASS.Text = "BYPASS TEST"
        Me.CMSBYPASS.UseVisualStyleBackColor = False
        '
        'CMDCONT
        '
        Me.CMDCONT.BackColor = System.Drawing.SystemColors.Control
        Me.CMDCONT.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CMDCONT.Location = New System.Drawing.Point(105, 129)
        Me.CMDCONT.Name = "CMDCONT"
        Me.CMDCONT.Size = New System.Drawing.Size(82, 41)
        Me.CMDCONT.TabIndex = 0
        Me.CMDCONT.Text = "CONTINUE"
        Me.CMDCONT.UseVisualStyleBackColor = False
        '
        'Image1
        '
        Me.Image1.BackColor = System.Drawing.SystemColors.Control
        Me.Image1.Image = CType(resources.GetObject("Image1.Image"), System.Drawing.Image)
        Me.Image1.Location = New System.Drawing.Point(3, 8)
        Me.Image1.Name = "Image1"
        Me.Image1.Size = New System.Drawing.Size(98, 86)
        Me.Image1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.Image1.TabIndex = 2
        Me.Image1.TabStop = False
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(8, 113)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(93, 58)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Test will automatically be bypassed in 30 sec."
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(113, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(195, 114)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = resources.GetString("Label2.Text")
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(154, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(1, 12)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "W"
        '
        'FrmlaserWarning
        '
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(316, 175)
        Me.Controls.Add(Me.CMSBYPASS)
        Me.Controls.Add(Me.CMDCONT)
        Me.Controls.Add(Me.Image1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FrmlaserWarning"
        Me.Text = "Laser Bypass"
        CType(Me.Image1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

End Sub

#End Region

    '**************************************************************
    '* Nomenclature   : ATS-ViperT SYSTEM SELF TEST               *
    '*                  Laser Warning Message                     *
    '* Version        : 2.0                                       *
    '* Last Update    : Mar 1, 2017                               *
    '* Purpose        : This form is used to show Laser warning   *
    '*                  message to the user.                      *
    '**************************************************************
    Private Sub CMDCONT_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMDCONT.Click

        LaserContinue = True
        Me.Close()

    End Sub

    Private Sub CMSBYPASS_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMSBYPASS.Click

        LaserContinue = False
        Me.Close()

    End Sub

    Private Sub FrmlaserWarning_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim sMsg As String

        Label3.Visible = False
        sMsg = WarningMsg
        Label2.Text = sMsg

    End Sub


End Class