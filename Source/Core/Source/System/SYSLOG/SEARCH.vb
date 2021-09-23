
Imports System.Windows.Forms
Imports Microsoft.VisualBasic

Public Class frmSearch
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
    Friend WithEvents cmdFindNext As System.Windows.Forms.Button
    Friend WithEvents txtFindWhat As System.Windows.Forms.TextBox
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents cmdFind As System.Windows.Forms.Button
    Friend WithEvents chkCase As System.Windows.Forms.CheckBox
    Friend WithEvents chkWord As System.Windows.Forms.CheckBox
    Friend WithEvents lblOptions As System.Windows.Forms.Label
    Friend WithEvents lblFindWhat As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSearch))
        Me.cmdFindNext = New System.Windows.Forms.Button()
        Me.txtFindWhat = New System.Windows.Forms.TextBox()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.cmdFind = New System.Windows.Forms.Button()
        Me.chkCase = New System.Windows.Forms.CheckBox()
        Me.chkWord = New System.Windows.Forms.CheckBox()
        Me.lblOptions = New System.Windows.Forms.Label()
        Me.lblFindWhat = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'cmdFindNext
        '
        Me.cmdFindNext.BackColor = System.Drawing.SystemColors.Control
        Me.cmdFindNext.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdFindNext.Location = New System.Drawing.Point(235, 40)
        Me.cmdFindNext.Name = "cmdFindNext"
        Me.cmdFindNext.Size = New System.Drawing.Size(82, 25)
        Me.cmdFindNext.TabIndex = 6
        Me.cmdFindNext.Text = "Find &Next"
        Me.cmdFindNext.UseVisualStyleBackColor = False
        '
        'txtFindWhat
        '
        Me.txtFindWhat.BackColor = System.Drawing.SystemColors.Window
        Me.txtFindWhat.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFindWhat.Location = New System.Drawing.Point(73, 8)
        Me.txtFindWhat.Name = "txtFindWhat"
        Me.txtFindWhat.Size = New System.Drawing.Size(147, 20)
        Me.txtFindWhat.TabIndex = 1
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Location = New System.Drawing.Point(235, 73)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(82, 25)
        Me.cmdCancel.TabIndex = 7
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = False
        '
        'cmdFind
        '
        Me.cmdFind.BackColor = System.Drawing.SystemColors.Control
        Me.cmdFind.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdFind.Location = New System.Drawing.Point(235, 8)
        Me.cmdFind.Name = "cmdFind"
        Me.cmdFind.Size = New System.Drawing.Size(82, 25)
        Me.cmdFind.TabIndex = 5
        Me.cmdFind.Text = "&Find"
        Me.cmdFind.UseVisualStyleBackColor = False
        '
        'chkCase
        '
        Me.chkCase.BackColor = System.Drawing.SystemColors.Control
        Me.chkCase.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkCase.Location = New System.Drawing.Point(73, 73)
        Me.chkCase.Name = "chkCase"
        Me.chkCase.Size = New System.Drawing.Size(200, 17)
        Me.chkCase.TabIndex = 4
        Me.chkCase.Text = "Match &Case"
        Me.chkCase.UseVisualStyleBackColor = False
        '
        'chkWord
        '
        Me.chkWord.BackColor = System.Drawing.SystemColors.Control
        Me.chkWord.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkWord.Location = New System.Drawing.Point(73, 49)
        Me.chkWord.Name = "chkWord"
        Me.chkWord.Size = New System.Drawing.Size(200, 17)
        Me.chkWord.TabIndex = 3
        Me.chkWord.Text = "Find Whole Word &Only"
        Me.chkWord.UseVisualStyleBackColor = False
        '
        'lblOptions
        '
        Me.lblOptions.AutoSize = True
        Me.lblOptions.BackColor = System.Drawing.SystemColors.Control
        Me.lblOptions.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblOptions.Location = New System.Drawing.Point(8, 49)
        Me.lblOptions.Name = "lblOptions"
        Me.lblOptions.Size = New System.Drawing.Size(46, 13)
        Me.lblOptions.TabIndex = 2
        Me.lblOptions.Text = "Options:"
        '
        'lblFindWhat
        '
        Me.lblFindWhat.AutoSize = True
        Me.lblFindWhat.BackColor = System.Drawing.SystemColors.Control
        Me.lblFindWhat.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblFindWhat.Location = New System.Drawing.Point(8, 16)
        Me.lblFindWhat.Name = "lblFindWhat"
        Me.lblFindWhat.Size = New System.Drawing.Size(59, 13)
        Me.lblFindWhat.TabIndex = 0
        Me.lblFindWhat.Text = "&Find What:"
        '
        'frmSearch
        '
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(324, 107)
        Me.Controls.Add(Me.cmdFindNext)
        Me.Controls.Add(Me.txtFindWhat)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdFind)
        Me.Controls.Add(Me.chkCase)
        Me.Controls.Add(Me.chkWord)
        Me.Controls.Add(Me.lblOptions)
        Me.Controls.Add(Me.lblFindWhat)
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSearch"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Find"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

	'=========================================================

    Private Sub cmdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Close()
    End Sub


    Public Sub cmdFind_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdFind.Click

        CurrentSearch = txtFindWhat.Text
        MatchCase = IIf(chkCase.Checked, 1, 0)
        FindWholeWordOnly = IIf(chkWord.Checked, 1, 0)
        SearchLog(True)
    End Sub


    Public Sub cmdFindNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdFindNext.Click
        cmdFindNext_Click()
    End Sub
    Public Sub cmdFindNext_Click()
        CurrentSearch = txtFindWhat.Text
        MatchCase = IIf(chkCase.Checked, 1, 0)
        FindWholeWordOnly = IIf(chkWord.Checked, 1, 0)
        SearchLog(False)

    End Sub


    Private Sub frmSearch_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        txtFindWhat.Text = CurrentSearch
        chkCase.Checked = MatchCase
        chkWord.Checked = FindWholeWordOnly
    End Sub


    Private Sub txtFindWhat_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFindWhat.Enter
        txtFindWhat.SelectionLength = Len(txtFindWhat.Text)
    End Sub


    Private Sub txtFindWhat_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFindWhat.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)

        If KeyAscii=13 Then
            cmdFindNext_Click()
        End If


        e.KeyChar = Chr(KeyAscii)
    End Sub



End Class