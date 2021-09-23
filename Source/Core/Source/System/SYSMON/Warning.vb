
Imports System.Windows.Forms

Public Class frmWarning
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
    Friend WithEvents cmdClear As System.Windows.Forms.Button
    Friend WithEvents cmdNext As System.Windows.Forms.Button
    Friend WithEvents cmdPrevious As System.Windows.Forms.Button
    Friend WithEvents lblComponentDescription As System.Windows.Forms.Label
    Friend WithEvents lblComponent As System.Windows.Forms.Label
    Friend WithEvents lblCondDesc As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Image1 As System.Windows.Forms.PictureBox
    Friend WithEvents lblAlarmType As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmWarning))
        Me.cmdClear = New System.Windows.Forms.Button()
        Me.cmdNext = New System.Windows.Forms.Button()
        Me.cmdPrevious = New System.Windows.Forms.Button()
        Me.lblComponentDescription = New System.Windows.Forms.Label()
        Me.lblComponent = New System.Windows.Forms.Label()
        Me.lblCondDesc = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Image1 = New System.Windows.Forms.PictureBox()
        Me.lblAlarmType = New System.Windows.Forms.Label()
        CType(Me.Image1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdClear
        '
        Me.cmdClear.BackColor = System.Drawing.SystemColors.Control
        Me.cmdClear.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClear.Location = New System.Drawing.Point(316, 210)
        Me.cmdClear.Name = "cmdClear"
        Me.cmdClear.Size = New System.Drawing.Size(76, 23)
        Me.cmdClear.TabIndex = 0
        Me.cmdClear.Text = "&Clear All"
        Me.cmdClear.UseVisualStyleBackColor = False
        '
        'cmdNext
        '
        Me.cmdNext.BackColor = System.Drawing.SystemColors.Control
        Me.cmdNext.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdNext.Location = New System.Drawing.Point(235, 210)
        Me.cmdNext.Name = "cmdNext"
        Me.cmdNext.Size = New System.Drawing.Size(76, 23)
        Me.cmdNext.TabIndex = 7
        Me.cmdNext.Text = "&Next >>"
        Me.cmdNext.UseVisualStyleBackColor = False
        '
        'cmdPrevious
        '
        Me.cmdPrevious.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrevious.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrevious.Location = New System.Drawing.Point(154, 210)
        Me.cmdPrevious.Name = "cmdPrevious"
        Me.cmdPrevious.Size = New System.Drawing.Size(76, 23)
        Me.cmdPrevious.TabIndex = 6
        Me.cmdPrevious.Text = "<< &Previous"
        Me.cmdPrevious.UseVisualStyleBackColor = False
        '
        'lblComponentDescription
        '
        Me.lblComponentDescription.AutoSize = True
        Me.lblComponentDescription.BackColor = System.Drawing.SystemColors.Control
        Me.lblComponentDescription.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblComponentDescription.Location = New System.Drawing.Point(77, 53)
        Me.lblComponentDescription.Name = "lblComponentDescription"
        Me.lblComponentDescription.Size = New System.Drawing.Size(207, 13)
        Me.lblComponentDescription.TabIndex = 5
        Me.lblComponentDescription.Text = "Failure Device (Primary VXI Chassis Slot 4)"
        '
        'lblComponent
        '
        Me.lblComponent.AutoSize = True
        Me.lblComponent.BackColor = System.Drawing.SystemColors.Control
        Me.lblComponent.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblComponent.Location = New System.Drawing.Point(12, 53)
        Me.lblComponent.Name = "lblComponent"
        Me.lblComponent.Size = New System.Drawing.Size(64, 13)
        Me.lblComponent.TabIndex = 4
        Me.lblComponent.Text = "Component:"
        '
        'lblCondDesc
        '
        Me.lblCondDesc.AutoEllipsis = True
        Me.lblCondDesc.AutoSize = True
        Me.lblCondDesc.BackColor = System.Drawing.SystemColors.Control
        Me.lblCondDesc.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCondDesc.Location = New System.Drawing.Point(77, 69)
        Me.lblCondDesc.Name = "lblCondDesc"
        Me.lblCondDesc.Size = New System.Drawing.Size(135, 13)
        Me.lblCondDesc.TabIndex = 3
        Me.lblCondDesc.Text = "Long Description Of Failure"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(12, 69)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(54, 13)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Condition:"
        '
        'Image1
        '
        Me.Image1.BackColor = System.Drawing.SystemColors.Control
        Me.Image1.Image = CType(resources.GetObject("Image1.Image"), System.Drawing.Image)
        Me.Image1.Location = New System.Drawing.Point(16, 8)
        Me.Image1.Name = "Image1"
        Me.Image1.Size = New System.Drawing.Size(32, 32)
        Me.Image1.TabIndex = 8
        Me.Image1.TabStop = False
        '
        'lblAlarmType
        '
        Me.lblAlarmType.AutoSize = True
        Me.lblAlarmType.BackColor = System.Drawing.SystemColors.Control
        Me.lblAlarmType.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAlarmType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAlarmType.Location = New System.Drawing.Point(54, 16)
        Me.lblAlarmType.Name = "lblAlarmType"
        Me.lblAlarmType.Size = New System.Drawing.Size(183, 20)
        Me.lblAlarmType.TabIndex = 1
        Me.lblAlarmType.Text = "Caution: Temperature"
        '
        'frmWarning
        '
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(436, 257)
        Me.Controls.Add(Me.cmdClear)
        Me.Controls.Add(Me.cmdNext)
        Me.Controls.Add(Me.cmdPrevious)
        Me.Controls.Add(Me.lblComponentDescription)
        Me.Controls.Add(Me.lblComponent)
        Me.Controls.Add(Me.lblCondDesc)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Image1)
        Me.Controls.Add(Me.lblAlarmType)
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmWarning"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "ATS System Monitor Warning"
        CType(Me.Image1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

	'=========================================================
    '************************************************************
    '* Virtual Instrument Portable Equipment Repair/Test(VIPERT)*
    '*                                                          *
    '* Nomenclature   : SYSTEM: System Monitor Warning Form     *
    '* Purpose        : Display a System Warning along with     *
    '*                  component and condition.                *
    '************************************************************
    
    Dim UnloadFlag As Short 'Determines if the form can unload

    Private Sub cmdClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdClear.Click

        Application.DoEvents()
        UnloadFlag = True
        Close()

    End Sub

    Private Sub cmdNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNext.Click

        If QueuePointer<>QueueCounter Then
            QueuePointer += 1
        End If
        UpdateWarningGui(QueuePointer)

    End Sub

    Private Sub cmdPrevious_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPrevious.Click

        If QueuePointer<>1 Then
            QueuePointer -= 1
        End If
        UpdateWarningGui(QueuePointer)

    End Sub


    Private Sub frmWarning_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim TopMost As Integer

        BringToTop(Me.Handle.ToInt32)
        UnloadFlag = False
        TopMost = SetWindowPos(Me.Handle.ToInt32, HWND_TOPMOST, 0, 0, 0, 0, FLAGS)

    End Sub


    Private Sub frmWarning_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Leave

        BringToTop(Me.Handle.ToInt32)

    End Sub


    
    Private Sub Form_Unload(ByRef Cancel As Short)

        Dim Queue As Short

        If UnloadFlag=True Then
            Cancel = False
            For Queue = 1 To 26
                WarningQueue(Queue).Topic = ""
                WarningQueue(Queue).Component = ""
                WarningQueue(Queue).Condition = ""
            Next Queue
            QueuePointer = 0
        Else
            Cancel = True
            Application.DoEvents()
            Me.WindowState = FormWindowState.Minimized 'Minimize
            Application.DoEvents()
        End If

    End Sub

    Private Sub frmWarning_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Dim Cancel As Short = 0
        Form_Unload(Cancel)
        If Cancel <> 0 Then e.Cancel = True
    End Sub

    Private Sub frmWarning_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Me.BringToFront()
    End Sub
End Class