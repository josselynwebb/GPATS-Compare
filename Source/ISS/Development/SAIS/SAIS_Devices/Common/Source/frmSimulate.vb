
Imports System.Windows.Forms

Public Class frmSimulate
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
    Friend WithEvents cboSimCmds As System.Windows.Forms.ComboBox
    Friend Shadows WithEvents CancelButton As System.Windows.Forms.Button
    Friend WithEvents OKButton As System.Windows.Forms.Button
    Friend WithEvents lblCaption As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmSimulate))
        Me.cboSimCmds = New System.Windows.Forms.ComboBox()
        Me.CancelButton = New System.Windows.Forms.Button()
        Me.OKButton = New System.Windows.Forms.Button()
        Me.lblCaption = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'cboSimCmds
        '
        Me.cboSimCmds.Name = "cboSimCmds"
        Me.cboSimCmds.TabIndex = 2
        Me.cboSimCmds.Location = New System.Drawing.Point(8, 32)
        Me.cboSimCmds.Size = New System.Drawing.Size(575, 21)
        Me.cboSimCmds.Text = "Select an item"
        Me.cboSimCmds.BackColor = System.Drawing.SystemColors.Window
        Me.cboSimCmds.ForeColor = System.Drawing.SystemColors.WindowText
        '
        'CancelButton
        '
        Me.CancelButton.Name = "CancelButton"
        Me.CancelButton.TabIndex = 1
        Me.CancelButton.Location = New System.Drawing.Point(502, 65)
        Me.CancelButton.Size = New System.Drawing.Size(82, 25)
        Me.CancelButton.Text = "Cancel"
        Me.CancelButton.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton.ForeColor = System.Drawing.SystemColors.ControlText
        '
        'OKButton
        '
        Me.OKButton.Name = "OKButton"
        Me.OKButton.TabIndex = 0
        Me.OKButton.Location = New System.Drawing.Point(421, 65)
        Me.OKButton.Size = New System.Drawing.Size(82, 25)
        Me.OKButton.Text = "OK"
        Me.OKButton.BackColor = System.Drawing.SystemColors.Control
        Me.OKButton.ForeColor = System.Drawing.SystemColors.ControlText
        '
        'lblCaption
        '
        Me.lblCaption.Name = "lblCaption"
        Me.lblCaption.TabIndex = 3
        Me.lblCaption.Location = New System.Drawing.Point(16, 8)
        Me.lblCaption.Size = New System.Drawing.Size(252, 17)
        Me.lblCaption.Text = "Select simulation response:"
        Me.lblCaption.BackColor = System.Drawing.SystemColors.Control
        Me.lblCaption.ForeColor = System.Drawing.SystemColors.ControlText
        '
        'frmSimulate
        '
        Me.ClientSize = New System.Drawing.Size(606, 100)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.cboSimCmds, Me.CancelButton, Me.OKButton, Me.lblCaption})
        Me.Name = "frmSimulate"
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ShowInTaskbar = False
        Me.MinimizeBox = False
        Me.MaximizeBox = False
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Simulate"
        Me.ResumeLayout(False)

    End Sub

#End Region

	'=========================================================


    Private Sub OKButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles OKButton.Click

        Me.Hide()

    End Sub

End Class