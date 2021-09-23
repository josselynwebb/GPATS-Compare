Option Explicit On

Imports System.Windows.Forms

Public Class frmWait
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
    Friend WithEvents picTimer As System.Windows.Forms.PictureBox
    Friend WithEvents cmdQuit As System.Windows.Forms.Button
    Friend WithEvents cmdIgnore As System.Windows.Forms.Button
    Friend WithEvents proCountdown As System.Windows.Forms.ProgressBar
    Friend WithEvents lblSec As System.Windows.Forms.Label
    Friend WithEvents lblS As System.Windows.Forms.Label
    Friend WithEvents lblM As System.Windows.Forms.Label
    Friend WithEvents lblMin As System.Windows.Forms.Label
    Friend WithEvents lblComment As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmWait))
        Me.picTimer = New System.Windows.Forms.PictureBox()
        Me.cmdQuit = New System.Windows.Forms.Button()
        Me.cmdIgnore = New System.Windows.Forms.Button()
        Me.proCountdown = New System.Windows.Forms.ProgressBar()
        Me.lblSec = New System.Windows.Forms.Label()
        Me.lblS = New System.Windows.Forms.Label()
        Me.lblM = New System.Windows.Forms.Label()
        Me.lblMin = New System.Windows.Forms.Label()
        Me.lblComment = New System.Windows.Forms.Label()
        CType(Me.picTimer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'picTimer
        '
        Me.picTimer.BackColor = System.Drawing.SystemColors.Control
        Me.picTimer.Image = CType(resources.GetObject("picTimer.Image"), System.Drawing.Image)
        Me.picTimer.Location = New System.Drawing.Point(16, 8)
        Me.picTimer.Name = "picTimer"
        Me.picTimer.Size = New System.Drawing.Size(33, 33)
        Me.picTimer.TabIndex = 5
        Me.picTimer.TabStop = False
        '
        'cmdQuit
        '
        Me.cmdQuit.BackColor = System.Drawing.SystemColors.Control
        Me.cmdQuit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdQuit.Location = New System.Drawing.Point(259, 162)
        Me.cmdQuit.Name = "cmdQuit"
        Me.cmdQuit.Size = New System.Drawing.Size(76, 25)
        Me.cmdQuit.TabIndex = 2
        Me.cmdQuit.Text = "&Quit"
        Me.cmdQuit.UseVisualStyleBackColor = False
        '
        'cmdIgnore
        '
        Me.cmdIgnore.BackColor = System.Drawing.SystemColors.Control
        Me.cmdIgnore.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdIgnore.Location = New System.Drawing.Point(170, 162)
        Me.cmdIgnore.Name = "cmdIgnore"
        Me.cmdIgnore.Size = New System.Drawing.Size(76, 25)
        Me.cmdIgnore.TabIndex = 1
        Me.cmdIgnore.Text = "&Ignore"
        Me.cmdIgnore.UseVisualStyleBackColor = False
        '
        'proCountdown
        '
        Me.proCountdown.Location = New System.Drawing.Point(8, 121)
        Me.proCountdown.Maximum = 1875
        Me.proCountdown.Name = "proCountdown"
        Me.proCountdown.Size = New System.Drawing.Size(325, 25)
        Me.proCountdown.TabIndex = 0
        '
        'lblSec
        '
        Me.lblSec.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblSec.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSec.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSec.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSec.Location = New System.Drawing.Point(275, 73)
        Me.lblSec.Name = "lblSec"
        Me.lblSec.Size = New System.Drawing.Size(58, 33)
        Me.lblSec.TabIndex = 8
        Me.lblSec.Text = "10"
        Me.lblSec.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblS
        '
        Me.lblS.BackColor = System.Drawing.SystemColors.Control
        Me.lblS.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblS.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblS.Location = New System.Drawing.Point(178, 73)
        Me.lblS.Name = "lblS"
        Me.lblS.Size = New System.Drawing.Size(98, 25)
        Me.lblS.TabIndex = 7
        Me.lblS.Text = "Seconds:"
        '
        'lblM
        '
        Me.lblM.BackColor = System.Drawing.SystemColors.Control
        Me.lblM.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblM.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblM.Location = New System.Drawing.Point(8, 73)
        Me.lblM.Name = "lblM"
        Me.lblM.Size = New System.Drawing.Size(82, 25)
        Me.lblM.TabIndex = 6
        Me.lblM.Text = "Minutes:"
        '
        'lblMin
        '
        Me.lblMin.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblMin.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMin.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMin.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMin.Location = New System.Drawing.Point(105, 73)
        Me.lblMin.Name = "lblMin"
        Me.lblMin.Size = New System.Drawing.Size(58, 33)
        Me.lblMin.TabIndex = 4
        Me.lblMin.Text = "10"
        Me.lblMin.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblComment
        '
        Me.lblComment.BackColor = System.Drawing.SystemColors.Control
        Me.lblComment.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblComment.Location = New System.Drawing.Point(65, 8)
        Me.lblComment.Name = "lblComment"
        Me.lblComment.Size = New System.Drawing.Size(260, 41)
        Me.lblComment.TabIndex = 3
        Me.lblComment.Text = "Proper warm-up time of one half hour has not yet expired.  Please wait until the " & _
    "count down indicates that the tester is ready to conduct self test procedures."
        Me.lblComment.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'frmWait
        '
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(341, 195)
        Me.ControlBox = False
        Me.Controls.Add(Me.picTimer)
        Me.Controls.Add(Me.cmdQuit)
        Me.Controls.Add(Me.cmdIgnore)
        Me.Controls.Add(Me.proCountdown)
        Me.Controls.Add(Me.lblSec)
        Me.Controls.Add(Me.lblS)
        Me.Controls.Add(Me.lblM)
        Me.Controls.Add(Me.lblMin)
        Me.Controls.Add(Me.lblComment)
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmWait"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        CType(Me.picTimer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    '**************************************************************
    '* Nomenclature   : ATS-TETS SYSTEM SELF TEST                 *
    '*                  System Warm Up Wait box                   *
    '* Version        : 2.0                                       *
    '* Last Update    : Apr 1, 2017                               *
    '* Purpose        : This form asks the user to wait until     *
    '*                  the system has "warmed up"                *
    '**************************************************************

    Private Sub cmdIgnore_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdIgnore.Click
        IgnorePressed = True
    End Sub



    Private Sub cmdQuit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdQuit.Click
        AbortTest = True
        Me.Close()
    End Sub


End Class