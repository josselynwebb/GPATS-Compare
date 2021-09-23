
Imports System.Windows.Forms

Public Class frmSetup
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
    Friend WithEvents picPass As System.Windows.Forms.PictureBox
    Friend WithEvents picFail As System.Windows.Forms.PictureBox
    Friend WithEvents pCEP As System.Windows.Forms.PictureBox
    Friend WithEvents pSDT As System.Windows.Forms.PictureBox
    Friend WithEvents pHDB As System.Windows.Forms.PictureBox
    Friend WithEvents Text1 As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSetup))
        Me.picPass = New System.Windows.Forms.PictureBox()
        Me.picFail = New System.Windows.Forms.PictureBox()
        Me.pCEP = New System.Windows.Forms.PictureBox()
        Me.pSDT = New System.Windows.Forms.PictureBox()
        Me.pHDB = New System.Windows.Forms.PictureBox()
        Me.Text1 = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.picPass, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picFail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pCEP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pSDT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pHDB, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'picPass
        '
        Me.picPass.BackColor = System.Drawing.SystemColors.Control
        Me.picPass.Image = CType(resources.GetObject("picPass.Image"), System.Drawing.Image)
        Me.picPass.Location = New System.Drawing.Point(105, 105)
        Me.picPass.Name = "picPass"
        Me.picPass.Size = New System.Drawing.Size(32, 16)
        Me.picPass.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picPass.TabIndex = 9
        Me.picPass.TabStop = False
        Me.picPass.Visible = False
        '
        'picFail
        '
        Me.picFail.BackColor = System.Drawing.SystemColors.Control
        Me.picFail.Image = CType(resources.GetObject("picFail.Image"), System.Drawing.Image)
        Me.picFail.Location = New System.Drawing.Point(138, 105)
        Me.picFail.Name = "picFail"
        Me.picFail.Size = New System.Drawing.Size(32, 16)
        Me.picFail.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picFail.TabIndex = 8
        Me.picFail.TabStop = False
        Me.picFail.Visible = False
        '
        'pCEP
        '
        Me.pCEP.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.pCEP.Image = CType(resources.GetObject("pCEP.Image"), System.Drawing.Image)
        Me.pCEP.Location = New System.Drawing.Point(133, 81)
        Me.pCEP.Name = "pCEP"
        Me.pCEP.Size = New System.Drawing.Size(32, 15)
        Me.pCEP.TabIndex = 7
        Me.pCEP.TabStop = False
        Me.pCEP.Visible = False
        '
        'pSDT
        '
        Me.pSDT.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.pSDT.Image = CType(resources.GetObject("pSDT.Image"), System.Drawing.Image)
        Me.pSDT.Location = New System.Drawing.Point(133, 49)
        Me.pSDT.Name = "pSDT"
        Me.pSDT.Size = New System.Drawing.Size(32, 15)
        Me.pSDT.TabIndex = 6
        Me.pSDT.TabStop = False
        '
        'pHDB
        '
        Me.pHDB.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.pHDB.Image = CType(resources.GetObject("pHDB.Image"), System.Drawing.Image)
        Me.pHDB.Location = New System.Drawing.Point(133, 16)
        Me.pHDB.Name = "pHDB"
        Me.pHDB.Size = New System.Drawing.Size(32, 15)
        Me.pHDB.TabIndex = 5
        Me.pHDB.TabStop = False
        '
        'Text1
        '
        Me.Text1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Text1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Text1.Location = New System.Drawing.Point(8, 129)
        Me.Text1.Multiline = True
        Me.Text1.Name = "Text1"
        Me.Text1.Size = New System.Drawing.Size(163, 60)
        Me.Text1.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(8, 81)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(126, 17)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Check Ethernet Ports: "
        Me.Label4.Visible = False
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(8, 49)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(126, 17)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Setup Date and Time:"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(8, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(126, 17)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Hardware Dialog Boxes:"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(8, 113)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(82, 17)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Status: "
        '
        'frmSetup
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(178, 197)
        Me.ControlBox = False
        Me.Controls.Add(Me.picPass)
        Me.Controls.Add(Me.picFail)
        Me.Controls.Add(Me.pCEP)
        Me.Controls.Add(Me.pSDT)
        Me.Controls.Add(Me.pHDB)
        Me.Controls.Add(Me.Text1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Name = "frmSetup"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Setup"
        CType(Me.picPass, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picFail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pCEP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pSDT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pHDB, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

	'=========================================================
    Private Sub frmSetup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.pCEP.Visible = False
        Me.pHDB.Visible = True
        Me.pSDT.Visible = True
    End Sub

End Class