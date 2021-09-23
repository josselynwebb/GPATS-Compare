
Imports System.Windows.Forms
Imports System.Drawing
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility

Public Class frmAlarm
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
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents cmdShutDown As System.Windows.Forms.Button
    Friend WithEvents sbrUserInformation As System.Windows.Forms.StatusBar
    Friend WithEvents sbrUserInformation_Panel1 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents sbrUserInformation_Panel2 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblAlarmType As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblCondDesc As System.Windows.Forms.Label
    Friend WithEvents lblComponent As System.Windows.Forms.Label
    Friend WithEvents lblComponentDescription As System.Windows.Forms.Label
    Friend WithEvents imgAlarm As System.Windows.Forms.PictureBox
    Friend WithEvents lblAction As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.cmdShutDown = New System.Windows.Forms.Button()
        Me.sbrUserInformation = New System.Windows.Forms.StatusBar()
        Me.sbrUserInformation_Panel1 = New System.Windows.Forms.StatusBarPanel()
        Me.sbrUserInformation_Panel2 = New System.Windows.Forms.StatusBarPanel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblAlarmType = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblCondDesc = New System.Windows.Forms.Label()
        Me.lblComponent = New System.Windows.Forms.Label()
        Me.lblComponentDescription = New System.Windows.Forms.Label()
        Me.imgAlarm = New System.Windows.Forms.PictureBox()
        Me.lblAction = New System.Windows.Forms.Label()
        CType(Me.sbrUserInformation_Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sbrUserInformation_Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgAlarm, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Location = New System.Drawing.Point(243, 182)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(76, 23)
        Me.cmdCancel.TabIndex = 1
        Me.cmdCancel.Text = "&Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = False
        '
        'Timer1
        '
        Me.Timer1.Interval = 1000
        '
        'cmdShutDown
        '
        Me.cmdShutDown.BackColor = System.Drawing.SystemColors.Control
        Me.cmdShutDown.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdShutDown.Location = New System.Drawing.Point(324, 182)
        Me.cmdShutDown.Name = "cmdShutDown"
        Me.cmdShutDown.Size = New System.Drawing.Size(76, 23)
        Me.cmdShutDown.TabIndex = 0
        Me.cmdShutDown.Text = "&Shut Down"
        Me.cmdShutDown.UseVisualStyleBackColor = False
        '
        'sbrUserInformation
        '
        Me.sbrUserInformation.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.sbrUserInformation.Location = New System.Drawing.Point(0, 262)
        Me.sbrUserInformation.Name = "sbrUserInformation"
        Me.sbrUserInformation.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.sbrUserInformation_Panel1, Me.sbrUserInformation_Panel2})
        Me.sbrUserInformation.ShowPanels = True
        Me.sbrUserInformation.Size = New System.Drawing.Size(433, 17)
        Me.sbrUserInformation.SizingGrip = False
        Me.sbrUserInformation.TabIndex = 9
        '
        'sbrUserInformation_Panel1
        '
        Me.sbrUserInformation_Panel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
        Me.sbrUserInformation_Panel1.MinWidth = 34
        Me.sbrUserInformation_Panel1.Name = "sbrUserInformation_Panel1"
        Me.sbrUserInformation_Panel1.Text = "Time Remaining Until System Shutdown:"
        Me.sbrUserInformation_Panel1.Width = 387
        '
        'sbrUserInformation_Panel2
        '
        Me.sbrUserInformation_Panel2.Alignment = System.Windows.Forms.HorizontalAlignment.Center
        Me.sbrUserInformation_Panel2.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents
        Me.sbrUserInformation_Panel2.MinWidth = 0
        Me.sbrUserInformation_Panel2.Name = "sbrUserInformation_Panel2"
        Me.sbrUserInformation_Panel2.Text = " 00:00 "
        Me.sbrUserInformation_Panel2.Width = 46
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(16, 73)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(328, 13)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Exit All Open Applications to Prevent Possible Data Loss"
        '
        'lblAlarmType
        '
        Me.lblAlarmType.AutoSize = True
        Me.lblAlarmType.BackColor = System.Drawing.SystemColors.Control
        Me.lblAlarmType.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAlarmType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAlarmType.Location = New System.Drawing.Point(53, 12)
        Me.lblAlarmType.Name = "lblAlarmType"
        Me.lblAlarmType.Size = New System.Drawing.Size(120, 20)
        Me.lblAlarmType.TabIndex = 7
        Me.lblAlarmType.Text = "Alarm: TYPE$"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(16, 113)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(54, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Condition:"
        '
        'lblCondDesc
        '
        Me.lblCondDesc.AutoEllipsis = True
        Me.lblCondDesc.AutoSize = True
        Me.lblCondDesc.BackColor = System.Drawing.SystemColors.Control
        Me.lblCondDesc.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCondDesc.Location = New System.Drawing.Point(81, 113)
        Me.lblCondDesc.Name = "lblCondDesc"
        Me.lblCondDesc.Size = New System.Drawing.Size(135, 13)
        Me.lblCondDesc.TabIndex = 5
        Me.lblCondDesc.Text = "Long Description Of Failure"
        '
        'lblComponent
        '
        Me.lblComponent.AutoSize = True
        Me.lblComponent.BackColor = System.Drawing.SystemColors.Control
        Me.lblComponent.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblComponent.Location = New System.Drawing.Point(16, 97)
        Me.lblComponent.Name = "lblComponent"
        Me.lblComponent.Size = New System.Drawing.Size(64, 13)
        Me.lblComponent.TabIndex = 4
        Me.lblComponent.Text = "Component:"
        '
        'lblComponentDescription
        '
        Me.lblComponentDescription.AutoSize = True
        Me.lblComponentDescription.BackColor = System.Drawing.SystemColors.Control
        Me.lblComponentDescription.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblComponentDescription.Location = New System.Drawing.Point(81, 97)
        Me.lblComponentDescription.Name = "lblComponentDescription"
        Me.lblComponentDescription.Size = New System.Drawing.Size(207, 13)
        Me.lblComponentDescription.TabIndex = 3
        Me.lblComponentDescription.Text = "Failure Device (Primary VXI Chassis Slot 4)"
        '
        'imgAlarm
        '
        Me.imgAlarm.BackColor = System.Drawing.SystemColors.Control
        Me.imgAlarm.Image = Global.sysmon.My.Resources.Resources.frmAlarm_Icon
        Me.imgAlarm.Location = New System.Drawing.Point(16, 8)
        Me.imgAlarm.Name = "imgAlarm"
        Me.imgAlarm.Size = New System.Drawing.Size(32, 32)
        Me.imgAlarm.TabIndex = 10
        Me.imgAlarm.TabStop = False
        '
        'lblAction
        '
        Me.lblAction.AutoSize = True
        Me.lblAction.BackColor = System.Drawing.SystemColors.Control
        Me.lblAction.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAction.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAction.Location = New System.Drawing.Point(16, 49)
        Me.lblAction.Name = "lblAction"
        Me.lblAction.Size = New System.Drawing.Size(158, 20)
        Me.lblAction.TabIndex = 2
        Me.lblAction.Text = "System Shutdown "
        Me.lblAction.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'frmAlarm
        '
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(433, 279)
        Me.ControlBox = False
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdShutDown)
        Me.Controls.Add(Me.sbrUserInformation)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblAlarmType)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.lblCondDesc)
        Me.Controls.Add(Me.lblComponent)
        Me.Controls.Add(Me.lblComponentDescription)
        Me.Controls.Add(Me.imgAlarm)
        Me.Controls.Add(Me.lblAction)
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAlarm"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "System Monitor Alarm"
        CType(Me.sbrUserInformation_Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sbrUserInformation_Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgAlarm, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

	'=========================================================
    '************************************************************
    '* Virtual Instrument Portable Equipment Repair/Test(VIPERT)*
    '*                                                          *
    '* Nomenclature   : SYSTEM: System Monitor Alarm Form       *
    '* Purpose        : Display a System Alarm type and         *
    '*                  description also time left until an     *
    '*                  action is taken.                        *
    '************************************************************

    Private Sub cmdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancel.Click

        Me.Timer1.Enabled = False
        Me.Close()

    End Sub

    Private Sub cmdShutDown_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdShutDown.Click

        Me.Timer1.Enabled = False
        SetFpu(False) 'Make Sure FPU is OFF
        ExitWindowsNT()

    End Sub




    Private Sub frmAlarm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        Me.lblAction.ForeColor = Color.Black

    End Sub


    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick

        
        Dim Min As Integer
        Dim Sec As Short
        Dim TimeLeft As String
        
        Static ColorRed As Short

        'BringToTop Me.hWnd
        Me.Refresh()

        If ColorRed Then
            Beep()
            Me.lblAction.ForeColor = ColorTranslator.FromOle(RGB(255, 0, 0))
            ColorRed = False
        Else
            Me.lblAction.ForeColor = ColorTranslator.FromOle(RGB(255, 255, 0))
            ColorRed = True
        End If
        Min = Fix(UserTimeout/60)
        Sec = UserTimeout Mod 60
        TimeLeft = VB6.Format(Str(Min), "00") & ":" & VB6.Format(Str(Sec), "00")
        Me.sbrUserInformation.Panels(2 - 1).Text = TimeLeft
        Me.sbrUserInformation.Refresh()
        UserTimeout -= 1
        If UserTimeout<=0 Then
            'End
            SetFpu(False) 'Make Sure FPU is OFF
            ExitWindowsNT
        End If

        Me.Refresh()

    End Sub



    Private Sub frmAlarm_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Me.BringToFront()
    End Sub
End Class