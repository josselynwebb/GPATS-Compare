
Imports System.Windows.Forms

Public Class frmEOPower
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
    Friend WithEvents Text1 As System.Windows.Forms.TextBox
    Friend WithEvents btnYes As System.Windows.Forms.Button
    Friend WithEvents btnNo As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmEOPower))
        Me.Text1 = New System.Windows.Forms.TextBox()
        Me.btnYes = New System.Windows.Forms.Button()
        Me.btnNo = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Text1
        '
        Me.Text1.Name = "Text1"
        Me.Text1.TabIndex = 3
        Me.Text1.Location = New System.Drawing.Point(210, 105)
        Me.Text1.Size = New System.Drawing.Size(66, 19)
        Me.Text1.Text = "Text1"
        Me.Text1.BackColor = System.Drawing.SystemColors.Control
        Me.Text1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Text1.BorderStyle = System.Windows.Forms.BorderStyle.None
        '
        'btnYes
        '
        Me.btnYes.Name = "btnYes"
        Me.btnYes.TabIndex = 2
        Me.btnYes.Location = New System.Drawing.Point(32, 65)
        Me.btnYes.Size = New System.Drawing.Size(82, 33)
        Me.btnYes.Text = "Yes"
        Me.btnYes.BackColor = System.Drawing.SystemColors.Control
        Me.btnYes.ForeColor = System.Drawing.SystemColors.ControlText
        '
        'btnNo
        '
        Me.btnNo.Name = "btnNo"
        Me.btnNo.TabIndex = 1
        Me.btnNo.Location = New System.Drawing.Point(162, 65)
        Me.btnNo.Size = New System.Drawing.Size(82, 33)
        Me.btnNo.Text = "No"
        Me.btnNo.BackColor = System.Drawing.SystemColors.Control
        Me.btnNo.ForeColor = System.Drawing.SystemColors.ControlText
        '
        'Label1
        '
        Me.Label1.Name = "Label1"
        Me.Label1.TabIndex = 0
        Me.Label1.Location = New System.Drawing.Point(8, 8)
        Me.Label1.Size = New System.Drawing.Size(268, 41)
        Me.Label1.Text = "If you would like to apply EO power during this session then click ""YES"" and connect the Power and Ethernet Cables"
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        '
        'frmEOPower
        '
        Me.ClientSize = New System.Drawing.Size(281, 122)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.Text1, Me.btnYes, Me.btnNo, Me.Label1})
        Me.Name = "frmEOPower"
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.MinimizeBox = True
        Me.MaximizeBox = True
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultLocation
        Me.Text = "EO Power"
        Me.ResumeLayout(False)

    End Sub

#End Region

	'=========================================================
    Public btnClick As Short

    Private Sub btnNo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNo.Click
        btnClick = 1
    End Sub

    Private Sub btnYes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnYes.Click
        btnClick = 2
    End Sub

    Private Sub frmEOPower_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Text1.Text = 10
        btnClick = 0
    End Sub

End Class