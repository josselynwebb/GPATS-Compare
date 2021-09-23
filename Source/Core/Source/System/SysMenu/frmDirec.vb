Option Explicit On

Imports System.Windows.Forms

Public Class frmDirections
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
    Friend WithEvents cmdBack As System.Windows.Forms.Button
    Friend WithEvents cmdAbortSetup As System.Windows.Forms.Button
    Friend WithEvents Picture1 As System.Windows.Forms.PictureBox
    Friend WithEvents txtDirections As System.Windows.Forms.TextBox
    Friend WithEvents cmdContinue As System.Windows.Forms.Button
    Friend WithEvents picGraphic As System.Windows.Forms.PictureBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDirections))
        Me.cmdBack = New System.Windows.Forms.Button()
        Me.cmdAbortSetup = New System.Windows.Forms.Button()
        Me.Picture1 = New System.Windows.Forms.PictureBox()
        Me.txtDirections = New System.Windows.Forms.TextBox()
        Me.cmdContinue = New System.Windows.Forms.Button()
        Me.picGraphic = New System.Windows.Forms.PictureBox()
        CType(Me.Picture1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Picture1.SuspendLayout()
        CType(Me.picGraphic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdBack
        '
        Me.cmdBack.BackColor = System.Drawing.SystemColors.Control
        Me.cmdBack.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdBack.Location = New System.Drawing.Point(418, 109)
        Me.cmdBack.Name = "cmdBack"
        Me.cmdBack.Size = New System.Drawing.Size(75, 24)
        Me.cmdBack.TabIndex = 5
        Me.cmdBack.Text = "&Back"
        Me.cmdBack.UseVisualStyleBackColor = False
        '
        'cmdAbortSetup
        '
        Me.cmdAbortSetup.BackColor = System.Drawing.SystemColors.Control
        Me.cmdAbortSetup.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAbortSetup.Location = New System.Drawing.Point(418, 81)
        Me.cmdAbortSetup.Name = "cmdAbortSetup"
        Me.cmdAbortSetup.Size = New System.Drawing.Size(75, 24)
        Me.cmdAbortSetup.TabIndex = 4
        Me.cmdAbortSetup.Text = "&Abort"
        Me.cmdAbortSetup.UseVisualStyleBackColor = False
        Me.cmdAbortSetup.Visible = False
        '
        'Picture1
        '
        Me.Picture1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Picture1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Picture1.Controls.Add(Me.txtDirections)
        Me.Picture1.Location = New System.Drawing.Point(9, 2)
        Me.Picture1.Name = "Picture1"
        Me.Picture1.Size = New System.Drawing.Size(395, 185)
        Me.Picture1.TabIndex = 2
        Me.Picture1.TabStop = False
        '
        'txtDirections
        '
        Me.txtDirections.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtDirections.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtDirections.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDirections.ForeColor = System.Drawing.SystemColors.Window
        Me.txtDirections.Location = New System.Drawing.Point(0, 0)
        Me.txtDirections.Multiline = True
        Me.txtDirections.Name = "txtDirections"
        Me.txtDirections.ReadOnly = True
        Me.txtDirections.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtDirections.Size = New System.Drawing.Size(388, 182)
        Me.txtDirections.TabIndex = 3
        Me.txtDirections.Text = "Install the SAIF on the ATS station."
        '
        'cmdContinue
        '
        Me.cmdContinue.BackColor = System.Drawing.SystemColors.Control
        Me.cmdContinue.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdContinue.Location = New System.Drawing.Point(418, 138)
        Me.cmdContinue.Name = "cmdContinue"
        Me.cmdContinue.Size = New System.Drawing.Size(75, 49)
        Me.cmdContinue.TabIndex = 0
        Me.cmdContinue.Text = "&Continue"
        Me.cmdContinue.UseVisualStyleBackColor = False
        '
        'picGraphic
        '
        Me.picGraphic.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.picGraphic.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.picGraphic.Location = New System.Drawing.Point(8, 194)
        Me.picGraphic.Name = "picGraphic"
        Me.picGraphic.Size = New System.Drawing.Size(490, 369)
        Me.picGraphic.TabIndex = 1
        Me.picGraphic.TabStop = False
        '
        'frmDirections
        '
        Me.BackColor = System.Drawing.SystemColors.ControlLight
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(508, 568)
        Me.Controls.Add(Me.cmdBack)
        Me.Controls.Add(Me.cmdAbortSetup)
        Me.Controls.Add(Me.Picture1)
        Me.Controls.Add(Me.cmdContinue)
        Me.Controls.Add(Me.picGraphic)
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmDirections"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Setup Instructions"
        Me.TopMost = True
        CType(Me.Picture1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Picture1.ResumeLayout(False)
        Me.Picture1.PerformLayout()
        CType(Me.picGraphic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

End Sub

#End Region

    '**************************************************************
    '* Nomenclature   : ATS-ViperT SYSTEM SELF TEST               *
    '*                  Direction Box                             *
    '* Version        : 2.0                                       *
    '* Last Update    : Apr 1, 2017                               *
    '* Purpose        : This form is used to show set-up          *
    '*                  instructions to the user                  *
    '**************************************************************

    Private Sub cmdBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBack.Click

        GoBack = True
        Me.Hide()

    End Sub

    Private Sub cmdContinue_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdContinue.Click

        Me.Hide()

    End Sub


    Private Sub cmdAbortSetup_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAbortSetup.Click

        Dim x As DialogResult
        x = MsgBox("Do you want to abort this test?", MsgBoxStyle.YesNo)
        If x = DialogResult.No Then Exit Sub

        Me.Hide() 

    End Sub

    Private Sub frmDirections_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated

        Me.cmdContinue.Focus()
        Application.DoEvents()

    End Sub

    Private Sub frmDirections_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        CenterForm(Me)

    End Sub



End Class