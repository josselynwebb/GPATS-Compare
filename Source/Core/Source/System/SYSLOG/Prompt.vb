
Imports System.Windows.Forms

Public Class frmPrompt
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
    Friend WithEvents txtMessage As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    Friend WithEvents cmdYes As System.Windows.Forms.Button
    Friend WithEvents cmdNo As System.Windows.Forms.Button
    Friend WithEvents AppPicture As System.Windows.Forms.PictureBox
    Friend WithEvents txtMessage2 As System.Windows.Forms.Label
    Friend WithEvents txtMessage_0 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPrompt))
        Me.txtMessage = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.txtMessage_0 = New System.Windows.Forms.Label()
        Me.cmdYes = New System.Windows.Forms.Button()
        Me.cmdNo = New System.Windows.Forms.Button()
        Me.AppPicture = New System.Windows.Forms.PictureBox()
        Me.txtMessage2 = New System.Windows.Forms.Label()
        CType(Me.txtMessage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AppPicture, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtMessage_0
        '
        Me.txtMessage_0.AutoSize = True
        Me.txtMessage_0.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtMessage_0.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMessage_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.txtMessage.SetIndex(Me.txtMessage_0, CType(0, Short))
        Me.txtMessage_0.Location = New System.Drawing.Point(61, 20)
        Me.txtMessage_0.Name = "txtMessage_0"
        Me.txtMessage_0.Size = New System.Drawing.Size(159, 16)
        Me.txtMessage_0.TabIndex = 1
        Me.txtMessage_0.Text = "Archive System Log File?"
        Me.txtMessage_0.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'cmdYes
        '
        Me.cmdYes.BackColor = System.Drawing.SystemColors.Control
        Me.cmdYes.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdYes.Location = New System.Drawing.Point(73, 158)
        Me.cmdYes.Name = "cmdYes"
        Me.cmdYes.Size = New System.Drawing.Size(82, 25)
        Me.cmdYes.TabIndex = 4
        Me.cmdYes.Text = "&Yes"
        Me.cmdYes.UseVisualStyleBackColor = False
        '
        'cmdNo
        '
        Me.cmdNo.BackColor = System.Drawing.SystemColors.Control
        Me.cmdNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdNo.Location = New System.Drawing.Point(166, 158)
        Me.cmdNo.Name = "cmdNo"
        Me.cmdNo.Size = New System.Drawing.Size(82, 25)
        Me.cmdNo.TabIndex = 3
        Me.cmdNo.Text = "&No"
        Me.cmdNo.UseVisualStyleBackColor = False
        '
        'AppPicture
        '
        Me.AppPicture.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.AppPicture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.AppPicture.Image = CType(resources.GetObject("AppPicture.Image"), System.Drawing.Image)
        Me.AppPicture.Location = New System.Drawing.Point(8, 12)
        Me.AppPicture.Name = "AppPicture"
        Me.AppPicture.Size = New System.Drawing.Size(18, 18)
        Me.AppPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.AppPicture.TabIndex = 0
        Me.AppPicture.TabStop = False
        '
        'txtMessage2
        '
        Me.txtMessage2.AutoSize = True
        Me.txtMessage2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtMessage2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMessage2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.txtMessage2.Location = New System.Drawing.Point(12, 65)
        Me.txtMessage2.Name = "txtMessage2"
        Me.txtMessage2.Size = New System.Drawing.Size(84, 16)
        Me.txtMessage2.TabIndex = 2
        Me.txtMessage2.Text = "txtMessage2"
        '
        'frmPrompt
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(316, 189)
        Me.ControlBox = False
        Me.Controls.Add(Me.cmdYes)
        Me.Controls.Add(Me.cmdNo)
        Me.Controls.Add(Me.AppPicture)
        Me.Controls.Add(Me.txtMessage2)
        Me.Controls.Add(Me.txtMessage_0)
        Me.ForeColor = System.Drawing.SystemColors.WindowText
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPrompt"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "VIPERT System Log"
        CType(Me.txtMessage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AppPicture, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

	'=========================================================

    Private Sub cmdNo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNo.Click
        YesNoResponse = 7 '(vbNo)
        Close()
    End Sub

    Private Sub cmdYes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdYes.Click
        YesNoResponse = 6 '(vbYes)
        Close()
    End Sub


End Class