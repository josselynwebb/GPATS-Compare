Option Explicit On

Imports System
Imports System.Windows.Forms

Public Class frmCalDate
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
    Friend WithEvents chkIsDueDate As System.Windows.Forms.CheckBox
    Friend WithEvents Command2 As System.Windows.Forms.Button
    Friend WithEvents Command1 As System.Windows.Forms.Button
    Friend WithEvents txtYear As System.Windows.Forms.TextBox
    Friend WithEvents txtMonth As System.Windows.Forms.TextBox
    Friend WithEvents txtDay As System.Windows.Forms.TextBox
    Friend WithEvents lblPrompt As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCalDate))
        Me.chkIsDueDate = New System.Windows.Forms.CheckBox()
        Me.Command2 = New System.Windows.Forms.Button()
        Me.Command1 = New System.Windows.Forms.Button()
        Me.txtYear = New System.Windows.Forms.TextBox()
        Me.txtMonth = New System.Windows.Forms.TextBox()
        Me.txtDay = New System.Windows.Forms.TextBox()
        Me.lblPrompt = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'chkIsDueDate
        '
        Me.chkIsDueDate.BackColor = System.Drawing.SystemColors.Control
        Me.chkIsDueDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkIsDueDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkIsDueDate.Location = New System.Drawing.Point(32, 77)
        Me.chkIsDueDate.Name = "chkIsDueDate"
        Me.chkIsDueDate.Size = New System.Drawing.Size(118, 17)
        Me.chkIsDueDate.TabIndex = 9
        Me.chkIsDueDate.Text = "This is a Due Date"
        Me.chkIsDueDate.UseVisualStyleBackColor = False
        '
        'Command2
        '
        Me.Command2.BackColor = System.Drawing.SystemColors.Control
        Me.Command2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Command2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Command2.Location = New System.Drawing.Point(8, 154)
        Me.Command2.Name = "Command2"
        Me.Command2.Size = New System.Drawing.Size(82, 23)
        Me.Command2.TabIndex = 8
        Me.Command2.Text = "&Cancel"
        Me.Command2.UseVisualStyleBackColor = False
        '
        'Command1
        '
        Me.Command1.BackColor = System.Drawing.SystemColors.Control
        Me.Command1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Command1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Command1.Location = New System.Drawing.Point(97, 154)
        Me.Command1.Name = "Command1"
        Me.Command1.Size = New System.Drawing.Size(82, 23)
        Me.Command1.TabIndex = 3
        Me.Command1.Text = "&Ok"
        Me.Command1.UseVisualStyleBackColor = False
        '
        'txtYear
        '
        Me.txtYear.BackColor = System.Drawing.SystemColors.Window
        Me.txtYear.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtYear.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtYear.Location = New System.Drawing.Point(125, 101)
        Me.txtYear.Multiline = True
        Me.txtYear.Name = "txtYear"
        Me.txtYear.Size = New System.Drawing.Size(50, 24)
        Me.txtYear.TabIndex = 2
        Me.txtYear.Text = "1998__"
        Me.txtYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtMonth
        '
        Me.txtMonth.BackColor = System.Drawing.SystemColors.Window
        Me.txtMonth.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMonth.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMonth.Location = New System.Drawing.Point(69, 101)
        Me.txtMonth.Multiline = True
        Me.txtMonth.Name = "txtMonth"
        Me.txtMonth.Size = New System.Drawing.Size(34, 24)
        Me.txtMonth.TabIndex = 1
        Me.txtMonth.Text = "8_"
        Me.txtMonth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtDay
        '
        Me.txtDay.BackColor = System.Drawing.SystemColors.Window
        Me.txtDay.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDay.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDay.Location = New System.Drawing.Point(12, 101)
        Me.txtDay.Multiline = True
        Me.txtDay.Name = "txtDay"
        Me.txtDay.Size = New System.Drawing.Size(34, 24)
        Me.txtDay.TabIndex = 0
        Me.txtDay.Text = "22__"
        Me.txtDay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblPrompt
        '
        Me.lblPrompt.BackColor = System.Drawing.SystemColors.Control
        Me.lblPrompt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPrompt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblPrompt.Location = New System.Drawing.Point(4, 4)
        Me.lblPrompt.Name = "lblPrompt"
        Me.lblPrompt.Size = New System.Drawing.Size(197, 66)
        Me.lblPrompt.TabIndex = 7
        Me.lblPrompt.Text = "Enter new calibration date:"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(125, 129)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(50, 17)
        Me.Label6.TabIndex = 6
        Me.Label6.Text = "Year"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(65, 129)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(54, 17)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "Month"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(12, 129)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(33, 17)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Day"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'frmCalDate
        '
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(222, 241)
        Me.ControlBox = False
        Me.Controls.Add(Me.chkIsDueDate)
        Me.Controls.Add(Me.Command2)
        Me.Controls.Add(Me.Command1)
        Me.Controls.Add(Me.txtYear)
        Me.Controls.Add(Me.txtMonth)
        Me.Controls.Add(Me.txtDay)
        Me.Controls.Add(Me.lblPrompt)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCalDate"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Calibration Date"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    '**************************************************************
    '* Nomenclature   : ATS-TETS SYSTEM SELF TEST                 *
    '*                  Calibration Date Entry Box                *
    '* Version        : 2.0                                       *
    '* Last Update    : Apr 1, 2017                               *
    '* Purpose        : This form displays the Calibration BOX    *
    '**************************************************************

    Private Sub Command1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Command1.Click
        Me.Hide()
    End Sub

    Private Sub Command2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Command2.Click
        bCalCancel = True
        Me.Hide()
    End Sub

    Private Sub txtDay_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDay.Enter
        txtDay.SelectionStart = 0
        txtDay.SelectionLength = Len(txtDay.Text)
    End Sub

    Private Sub txtDay_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDay.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)

        If InStr("0123456789" & Cstr(Chr(8)), Cstr(Chr(KeyAscii))) Then
        Else
            KeyAscii = 0
        End If

        e.KeyChar = Chr(KeyAscii)
    End Sub


    Private Sub txtMonth_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMonth.Enter
        txtMonth.SelectionStart = 0
        txtMonth.SelectionLength = Len(txtMonth.Text)
    End Sub

    Private Sub txtMonth_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMonth.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)

        If InStr("0123456789" & Cstr(Chr(8)), Cstr(Chr(KeyAscii))) Then
        Else
            KeyAscii = 0
        End If

        e.KeyChar = Chr(KeyAscii)
    End Sub


    Private Sub txtYear_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtYear.Enter
        txtYear.SelectionStart = 0
        txtYear.SelectionLength = Len(txtYear.Text)
    End Sub

    Private Sub txtYear_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtYear.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)

        If InStr("0123456789" & Cstr(Chr(8)), Cstr(Chr(KeyAscii))) Then
        Else
            KeyAscii = 0
        End If

        e.KeyChar = Chr(KeyAscii)
    End Sub



End Class