
Imports System.Windows.Forms
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility

Public Class frmSysWait
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
    Friend WithEvents lblFormat As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    Friend WithEvents lblValue As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    Friend WithEvents tmrTimeout As System.Windows.Forms.Timer
    Friend WithEvents picTimer As System.Windows.Forms.PictureBox
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    Friend WithEvents sbrTimeout As System.Windows.Forms.StatusBar
    Friend WithEvents sbrTimeout_Panel1 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents sbrTimeout_Panel2 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents proCountdown As System.Windows.Forms.ProgressBar
    Friend WithEvents lblFormat_3 As System.Windows.Forms.Label
    Friend WithEvents lblFormat_2 As System.Windows.Forms.Label
    Friend WithEvents lblValue_0 As System.Windows.Forms.Label
    Friend WithEvents lblComment As System.Windows.Forms.Label
    Friend WithEvents lblValue_1 As System.Windows.Forms.Label
    Friend WithEvents lblFormat_1 As System.Windows.Forms.Label
    Friend WithEvents dataDumpTimer As System.Windows.Forms.Timer
    Friend WithEvents dataDumpWorker As System.ComponentModel.BackgroundWorker
    Friend WithEvents lblFormat_0 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSysWait))
        Me.lblFormat = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.lblFormat_3 = New System.Windows.Forms.Label()
        Me.lblFormat_2 = New System.Windows.Forms.Label()
        Me.lblFormat_1 = New System.Windows.Forms.Label()
        Me.lblFormat_0 = New System.Windows.Forms.Label()
        Me.lblValue = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.lblValue_0 = New System.Windows.Forms.Label()
        Me.lblValue_1 = New System.Windows.Forms.Label()
        Me.tmrTimeout = New System.Windows.Forms.Timer(Me.components)
        Me.picTimer = New System.Windows.Forms.PictureBox()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.sbrTimeout = New System.Windows.Forms.StatusBar()
        Me.sbrTimeout_Panel1 = New System.Windows.Forms.StatusBarPanel()
        Me.sbrTimeout_Panel2 = New System.Windows.Forms.StatusBarPanel()
        Me.proCountdown = New System.Windows.Forms.ProgressBar()
        Me.lblComment = New System.Windows.Forms.Label()
        Me.dataDumpTimer = New System.Windows.Forms.Timer(Me.components)
        Me.dataDumpWorker = New System.ComponentModel.BackgroundWorker()
        CType(Me.lblFormat, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picTimer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sbrTimeout_Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sbrTimeout_Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblFormat_3
        '
        Me.lblFormat_3.AutoSize = True
        Me.lblFormat_3.BackColor = System.Drawing.SystemColors.Control
        Me.lblFormat_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblFormat.SetIndex(Me.lblFormat_3, CType(3, Short))
        Me.lblFormat_3.Location = New System.Drawing.Point(186, 124)
        Me.lblFormat_3.Name = "lblFormat_3"
        Me.lblFormat_3.Size = New System.Drawing.Size(26, 13)
        Me.lblFormat_3.TabIndex = 9
        Me.lblFormat_3.Text = "Unit"
        '
        'lblFormat_2
        '
        Me.lblFormat_2.AutoSize = True
        Me.lblFormat_2.BackColor = System.Drawing.SystemColors.Control
        Me.lblFormat_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblFormat.SetIndex(Me.lblFormat_2, CType(2, Short))
        Me.lblFormat_2.Location = New System.Drawing.Point(186, 104)
        Me.lblFormat_2.Name = "lblFormat_2"
        Me.lblFormat_2.Size = New System.Drawing.Size(26, 13)
        Me.lblFormat_2.TabIndex = 8
        Me.lblFormat_2.Text = "Unit"
        '
        'lblFormat_1
        '
        Me.lblFormat_1.AutoSize = True
        Me.lblFormat_1.BackColor = System.Drawing.SystemColors.Control
        Me.lblFormat_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblFormat.SetIndex(Me.lblFormat_1, CType(1, Short))
        Me.lblFormat_1.Location = New System.Drawing.Point(12, 124)
        Me.lblFormat_1.Name = "lblFormat_1"
        Me.lblFormat_1.Size = New System.Drawing.Size(87, 13)
        Me.lblFormat_1.TabIndex = 3
        Me.lblFormat_1.Text = "Measured Value:"
        '
        'lblFormat_0
        '
        Me.lblFormat_0.AutoSize = True
        Me.lblFormat_0.BackColor = System.Drawing.SystemColors.Control
        Me.lblFormat_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblFormat.SetIndex(Me.lblFormat_0, CType(0, Short))
        Me.lblFormat_0.Location = New System.Drawing.Point(12, 104)
        Me.lblFormat_0.Name = "lblFormat_0"
        Me.lblFormat_0.Size = New System.Drawing.Size(71, 13)
        Me.lblFormat_0.TabIndex = 2
        Me.lblFormat_0.Text = "Target Value:"
        '
        'lblValue_0
        '
        Me.lblValue_0.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblValue_0.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblValue_0.ForeColor = System.Drawing.Color.White
        Me.lblValue.SetIndex(Me.lblValue_0, CType(0, Short))
        Me.lblValue_0.Location = New System.Drawing.Point(109, 104)
        Me.lblValue_0.Name = "lblValue_0"
        Me.lblValue_0.Size = New System.Drawing.Size(61, 17)
        Me.lblValue_0.TabIndex = 7
        '
        'lblValue_1
        '
        Me.lblValue_1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblValue_1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblValue_1.ForeColor = System.Drawing.Color.White
        Me.lblValue.SetIndex(Me.lblValue_1, CType(1, Short))
        Me.lblValue_1.Location = New System.Drawing.Point(109, 124)
        Me.lblValue_1.Name = "lblValue_1"
        Me.lblValue_1.Size = New System.Drawing.Size(61, 17)
        Me.lblValue_1.TabIndex = 4
        '
        'tmrTimeout
        '
        Me.tmrTimeout.Interval = 1000
        '
        'picTimer
        '
        Me.picTimer.BackColor = System.Drawing.SystemColors.Control
        Me.picTimer.Image = CType(resources.GetObject("picTimer.Image"), System.Drawing.Image)
        Me.picTimer.Location = New System.Drawing.Point(12, 8)
        Me.picTimer.Name = "picTimer"
        Me.picTimer.Size = New System.Drawing.Size(33, 33)
        Me.picTimer.TabIndex = 1
        Me.picTimer.TabStop = False
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Location = New System.Drawing.Point(242, 180)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(90, 23)
        Me.cmdClose.TabIndex = 0
        Me.cmdClose.Text = "BUTTON TEXT"
        Me.cmdClose.UseVisualStyleBackColor = False
        '
        'sbrTimeout
        '
        Me.sbrTimeout.Location = New System.Drawing.Point(0, 251)
        Me.sbrTimeout.Name = "sbrTimeout"
        Me.sbrTimeout.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.sbrTimeout_Panel1, Me.sbrTimeout_Panel2})
        Me.sbrTimeout.ShowPanels = True
        Me.sbrTimeout.Size = New System.Drawing.Size(357, 21)
        Me.sbrTimeout.SizingGrip = False
        Me.sbrTimeout.TabIndex = 10
        '
        'sbrTimeout_Panel1
        '
        Me.sbrTimeout_Panel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
        Me.sbrTimeout_Panel1.Name = "sbrTimeout_Panel1"
        Me.sbrTimeout_Panel1.Text = "Please wait for target value or timeout."
        Me.sbrTimeout_Panel1.Width = 299
        '
        'sbrTimeout_Panel2
        '
        Me.sbrTimeout_Panel2.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents
        Me.sbrTimeout_Panel2.MinWidth = 0
        Me.sbrTimeout_Panel2.Name = "sbrTimeout_Panel2"
        Me.sbrTimeout_Panel2.Text = "00:00:00 "
        Me.sbrTimeout_Panel2.Width = 58
        '
        'proCountdown
        '
        Me.proCountdown.Location = New System.Drawing.Point(8, 147)
        Me.proCountdown.Name = "proCountdown"
        Me.proCountdown.Size = New System.Drawing.Size(324, 25)
        Me.proCountdown.TabIndex = 5
        '
        'lblComment
        '
        Me.lblComment.BackColor = System.Drawing.SystemColors.Control
        Me.lblComment.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblComment.Location = New System.Drawing.Point(57, 4)
        Me.lblComment.Name = "lblComment"
        Me.lblComment.Size = New System.Drawing.Size(280, 100)
        Me.lblComment.TabIndex = 6
        Me.lblComment.Text = "Proper warm-up time of one half hour has not yet expired.  Please wait until the " & _
    "count down indicates that the tester is ready to conduct self test procedures."
        '
        'dataDumpTimer
        '
        '
        'dataDumpWorker
        '
        '
        'frmSysWait
        '
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(357, 272)
        Me.ControlBox = False
        Me.Controls.Add(Me.picTimer)
        Me.Controls.Add(Me.cmdClose)
        Me.Controls.Add(Me.sbrTimeout)
        Me.Controls.Add(Me.proCountdown)
        Me.Controls.Add(Me.lblFormat_3)
        Me.Controls.Add(Me.lblFormat_2)
        Me.Controls.Add(Me.lblValue_0)
        Me.Controls.Add(Me.lblComment)
        Me.Controls.Add(Me.lblValue_1)
        Me.Controls.Add(Me.lblFormat_1)
        Me.Controls.Add(Me.lblFormat_0)
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSysWait"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Automated Test System (ATS)"
        Me.TopMost = True
        CType(Me.lblFormat, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picTimer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sbrTimeout_Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sbrTimeout_Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    '=========================================================
    '************************************************************
    '* Virtual Instrument Portable Equipment Repair/Test(VIPERT)*
    '*                                                          *
    '* Nomenclature   : SYSTEM: System Monitor Wait Form        *
    '* Purpose        : This module displays environmental delay*
    '*                  conditions to the user during startup   *
    '*                  and shutdown procedures                 *
    '************************************************************

    Public TempToWaitFor As String

    Private Sub cmdClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdClose.Click

        Me.Visible = False
        SysWaitTimeout = 0
        Me.Close()

    End Sub



    Private Sub frmSysWait_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        BringToTop(Me.Handle.ToInt32)
        Me.Refresh()

    End Sub


    Private Sub tmrTimeout_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmrTimeout.Tick


        
        Dim Min As Integer
        Dim Sec As Short

        SysWaitTimeout -= 1
        If SysWaitTimeout <= 0 Then
            Me.tmrTimeout.Enabled = False
            Me.Visible = False
        End If
        Min = Fix(SysWaitTimeout / 60)
        Sec = SysWaitTimeout Mod 60

        Me.sbrTimeout.Panels(2 - 1).Text = VB6.Format(CStr(Min), "00") & ":" & VB6.Format(CStr(Sec), "00") & "  "

        If (Me.TempToWaitFor = "MaximumTemperatureRise") Then
            Dim MaximumRiseTemp = GetMaximumTemperatureRise()
            Me.lblValue(1).Text = VB6.Format(MaximumRiseTemp, "0.0")
            If MaximumRiseTemp <= 0 Then MaximumRiseTemp = 1 'Div by zero trap
            Me.proCountdown.Value = (100 - (((MaximumRiseTemp - 3) / 27) * 100))

            If (MaximumRiseTemp <= 3) Then
                Me.tmrTimeout.Enabled = False
                Me.dataDumpTimer.Enabled = False
                Me.Close()
            End If

        ElseIf (Me.TempToWaitFor = "MaximumIntakeTemperature") Then
            Dim MaxIntake = GetMaximumIntakeTemperature()
            Dim countdownValue
            Me.lblValue(1).Text = VB6.Format(MaxIntake, "0.0")
            If MaxIntake <= 0 Then MaxIntake = 1 'Div by zero trap
            countdownValue = (100 - (((MaxIntake - 55) / (82.5 - 55)) * 100))
            If (countdownValue < 0) Then
                countdownValue = 0
            ElseIf countdownValue > 100 Then
                countdownValue = 100
            End If
            Me.proCountdown.Value = countdownValue
            If (MaxIntake <= 55) Then
                Me.tmrTimeout.Enabled = False
                Me.dataDumpTimer.Enabled = False
                Me.Close()
            End If

 
        ElseIf (Me.TempToWaitFor = "MinimumIntakeTemperature") Then
            Dim MinIntake = GetMinimumIntakeTemperature()
            Me.lblValue(1).Text = VB6.Format(MinIntake, "0.0")
            Me.proCountdown.Value = 100 - ((Math.Abs(MinIntake)) * 10)

            If (MinIntake >= 5) Then
                Me.tmrTimeout.Enabled = False
                Me.dataDumpTimer.Enabled = False
                Me.Close()
            End If

        End If


    End Sub



    Private Sub dataDumpTimer_Tick(sender As Object, e As EventArgs) Handles dataDumpTimer.Tick
        If dataDumpWorker.IsBusy <> True Then
            dataDumpWorker.RunWorkerAsync()
        End If
    End Sub

    Private Sub dataDumpWorker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles dataDumpWorker.DoWork
        GetOneDataDumpFromBothChassis()
    End Sub

    Private Sub frmSysWait_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Me.BringToFront()
    End Sub
End Class