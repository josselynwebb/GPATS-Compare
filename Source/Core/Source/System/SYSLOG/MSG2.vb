
Imports System.Windows.Forms

Public Class frmMess
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
    Friend WithEvents AppPicture As System.Windows.Forms.PictureBox
    Friend WithEvents txtMessage As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMess))
        Me.AppPicture = New System.Windows.Forms.PictureBox()
        Me.txtMessage = New System.Windows.Forms.Label()
        CType(Me.AppPicture, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'AppPicture
        '
        Me.AppPicture.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.AppPicture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.AppPicture.Image = CType(resources.GetObject("AppPicture.Image"), System.Drawing.Image)
        Me.AppPicture.Location = New System.Drawing.Point(8, 8)
        Me.AppPicture.Name = "AppPicture"
        Me.AppPicture.Size = New System.Drawing.Size(18, 18)
        Me.AppPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.AppPicture.TabIndex = 0
        Me.AppPicture.TabStop = False
        '
        'txtMessage
        '
        Me.txtMessage.AutoSize = True
        Me.txtMessage.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtMessage.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMessage.ForeColor = System.Drawing.SystemColors.ControlText
        Me.txtMessage.Location = New System.Drawing.Point(74, 16)
        Me.txtMessage.Name = "txtMessage"
        Me.txtMessage.Size = New System.Drawing.Size(146, 16)
        Me.txtMessage.TabIndex = 1
        Me.txtMessage.Text = "Updating System Log..."
        Me.txtMessage.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'frmMess
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(251, 50)
        Me.ControlBox = False
        Me.Controls.Add(Me.AppPicture)
        Me.Controls.Add(Me.txtMessage)
        Me.ForeColor = System.Drawing.SystemColors.WindowText
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMess"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "VIPERT System Log"
        CType(Me.AppPicture, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

	'=========================================================


End Class