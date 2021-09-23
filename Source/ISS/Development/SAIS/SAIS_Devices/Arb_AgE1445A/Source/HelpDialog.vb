
Imports System.Windows.Forms

Public Class frmHelpDialog
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
    Friend WithEvents Command3 As System.Windows.Forms.Button
    Friend WithEvents Command2 As System.Windows.Forms.Button
    Friend WithEvents Command1 As System.Windows.Forms.Button
    Friend WithEvents CancelBtn As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmHelpDialog))
        Me.Command3 = New System.Windows.Forms.Button()
        Me.Command2 = New System.Windows.Forms.Button()
        Me.Command1 = New System.Windows.Forms.Button()
        Me.CancelBtn = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Command3
        '
        Me.Command3.BackColor = System.Drawing.SystemColors.Control
        Me.Command3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Command3.Image = CType(resources.GetObject("Command3.Image"), System.Drawing.Image)
        Me.Command3.Location = New System.Drawing.Point(275, 16)
        Me.Command3.Name = "Command3"
        Me.Command3.Size = New System.Drawing.Size(114, 171)
        Me.Command3.TabIndex = 3
        Me.Command3.Text = "VXI_user_man_HP"
        Me.Command3.UseVisualStyleBackColor = False
        '
        'Command2
        '
        Me.Command2.BackColor = System.Drawing.SystemColors.Control
        Me.Command2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Command2.Image = CType(resources.GetObject("Command2.Image"), System.Drawing.Image)
        Me.Command2.Location = New System.Drawing.Point(146, 16)
        Me.Command2.Name = "Command2"
        Me.Command2.Size = New System.Drawing.Size(106, 171)
        Me.Command2.TabIndex = 2
        Me.Command2.Text = "5989-0154EN"
        Me.Command2.UseVisualStyleBackColor = False
        '
        'Command1
        '
        Me.Command1.BackColor = System.Drawing.SystemColors.Control
        Me.Command1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Command1.Image = CType(resources.GetObject("Command1.Image"), System.Drawing.Image)
        Me.Command1.Location = New System.Drawing.Point(16, 16)
        Me.Command1.Name = "Command1"
        Me.Command1.Size = New System.Drawing.Size(106, 171)
        Me.Command1.TabIndex = 1
        Me.Command1.Text = "5965-5548E"
        Me.Command1.UseVisualStyleBackColor = False
        '
        'CancelBtn
        '
        Me.CancelBtn.BackColor = System.Drawing.SystemColors.Control
        Me.CancelBtn.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CancelBtn.Location = New System.Drawing.Point(160, 213)
        Me.CancelBtn.Name = "CancelBtn"
        Me.CancelBtn.Size = New System.Drawing.Size(82, 25)
        Me.CancelBtn.TabIndex = 0
        Me.CancelBtn.Text = "Cancel"
        Me.CancelBtn.UseVisualStyleBackColor = False
        '
        'frmHelpDialog
        '
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(403, 274)
        Me.Controls.Add(Me.Command3)
        Me.Controls.Add(Me.Command2)
        Me.Controls.Add(Me.Command1)
        Me.Controls.Add(Me.CancelBtn)
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmHelpDialog"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Help"
        Me.ResumeLayout(False)

    End Sub

#End Region

    '=========================================================
    Private Const SW_SHOWNORMAL As Short = 1
    Private Const SW_SHOWMINIMIZED As Short = 2
    Private Const SW_SHOWMAXIMIZED As Short = 3

    Private Declare Function ShellExecute Lib "shell32.dll" Alias "ShellExecuteA" (ByVal hwnd As Integer, ByVal lpOperation As String, ByVal lpFile As String, ByVal lpParameters As String, ByVal lpDirectory As String, ByVal nShowCmd As Integer) As Integer

    Private Sub CancelBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CancelBtn.Click
        Me.Close()
    End Sub

    Private Sub Command1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Command1.Click
        Dim helpFile As String = Application.StartupPath + "\..\Config\Help\Arb_AgE1445A\5965-5542E.pdf"
        ShellExecute(0, "Open", helpFile, 0, 0, SW_SHOWNORMAL)
        '        ShellExecute(0, "Open", "\Program Files\VIPERT\ISS\Config\help\Arb_AgE1445A\5965-5542E.pdf", 0, 0, SW_SHOWNORMAL)
    End Sub

    Private Sub Command2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Command2.Click
        Dim helpFile As String = Application.StartupPath + "\..\Config\Help\Arb_AgE1445A\5989-0154EN.pdf"
        ShellExecute(0, "Open", helpFile, 0, 0, SW_SHOWNORMAL)
        '        ShellExecute(0, "Open", "\Program Files\VIPERT\ISS\Config\help\Arb_AgE1445A\5989-0154EN.pdf", 0, 0, SW_SHOWNORMAL)
    End Sub

    Private Sub Command3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Command3.Click
        Dim helpFile As String = Application.StartupPath + "\..\Config\Help\Arb_AgE1445A\VXI_user_man_HP_E1445A.pdf"
        ShellExecute(0, "Open", helpFile, 0, 0, SW_SHOWNORMAL)
        '        ShellExecute(0, "Open", "\Program Files\VIPERT\ISS\Config\help\Arb_AgE1445A\VXI_user_man_HP_E1445A.pdf", 0, 0, SW_SHOWNORMAL)
    End Sub

    'Private Sub Command4_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Dim helpFile As String = Application.StartupPath + "\..\Config\Help\Arb_AgE1445A\E1445USR.pdf"
    '    ShellExecute(0, "Open", helpFile, 0, 0, SW_SHOWNORMAL)
    '    ShellExecute(0, "Open", "\Program Files\VIPERT\ISS\Config\help\Arb_AgE1445A\E1445USR.PDF", 0, 0, SW_SHOWNORMAL)
    '    http://www.gelsana.com/COTS%20Manuals/HP1445A/E1445USR.PDF
    'End Sub

End Class