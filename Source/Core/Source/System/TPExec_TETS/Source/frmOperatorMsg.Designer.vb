<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Public Class frmOperatorMsg
#Region "Windows Form Designer generated code "
    <System.Diagnostics.DebuggerNonUserCode()> Public Sub New()
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
    End Sub
    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
        If Disposing Then
            If Not components Is Nothing Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(Disposing)
    End Sub
    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    Public ToolTip1 As System.Windows.Forms.ToolTip
    Public WithEvents InputData As System.Windows.Forms.TextBox
    Public WithEvents lblMsg_1 As System.Windows.Forms.Label
    Public WithEvents msgInBox As System.Windows.Forms.Panel
    Public WithEvents SSPanel1 As System.Windows.Forms.Panel
    Public WithEvents cmdContinue As System.Windows.Forms.Button
    Public WithEvents lblInput As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.InputData = New System.Windows.Forms.TextBox()
        Me.SSPanel1 = New System.Windows.Forms.Panel()
        Me.msgInBox = New System.Windows.Forms.Panel()
        Me.lblMsg_1 = New System.Windows.Forms.Label()
        Me.cmdContinue = New System.Windows.Forms.Button()
        Me.lblInput = New System.Windows.Forms.Label()
        Me.SSPanel1.SuspendLayout()
        Me.msgInBox.SuspendLayout()
        Me.SuspendLayout()
        '
        'InputData
        '
        Me.InputData.AcceptsReturn = True
        Me.InputData.BackColor = System.Drawing.Color.White
        Me.InputData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.InputData.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.InputData.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InputData.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.InputData.Location = New System.Drawing.Point(200, 374)
        Me.InputData.MaxLength = 0
        Me.InputData.Name = "InputData"
        Me.InputData.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.InputData.Size = New System.Drawing.Size(209, 26)
        Me.InputData.TabIndex = 0
        '
        'SSPanel1
        '
        Me.SSPanel1.Controls.Add(Me.msgInBox)
        Me.SSPanel1.Location = New System.Drawing.Point(8, 8)
        Me.SSPanel1.Name = "SSPanel1"
        Me.SSPanel1.Size = New System.Drawing.Size(707, 353)
        Me.SSPanel1.TabIndex = 2
        Me.SSPanel1.Text = "SSPanel1"
        '
        'msgInBox
        '
        Me.msgInBox.AutoScroll = True
        Me.msgInBox.AutoScrollMinSize = New System.Drawing.Size(490, 290)
        Me.msgInBox.BackColor = System.Drawing.SystemColors.Window
        Me.msgInBox.Controls.Add(Me.lblMsg_1)
        Me.msgInBox.Cursor = System.Windows.Forms.Cursors.Default
        Me.msgInBox.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.msgInBox.ForeColor = System.Drawing.SystemColors.WindowText
        Me.msgInBox.Location = New System.Drawing.Point(0, 0)
        Me.msgInBox.Name = "msgInBox"
        Me.msgInBox.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.msgInBox.Size = New System.Drawing.Size(646, 310)
        Me.msgInBox.TabIndex = 4
        Me.msgInBox.TabStop = True
        '
        'lblMsg_1
        '
        Me.lblMsg_1.BackColor = System.Drawing.Color.Transparent
        Me.lblMsg_1.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMsg_1.Font = New System.Drawing.Font("Courier New", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMsg_1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.lblMsg_1.Location = New System.Drawing.Point(0, 8)
        Me.lblMsg_1.Name = "lblMsg_1"
        Me.lblMsg_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMsg_1.Size = New System.Drawing.Size(505, 17)
        Me.lblMsg_1.TabIndex = 5
        Me.lblMsg_1.Text = "12345678901234567890123456789012345678901234567890"
        Me.lblMsg_1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'cmdContinue
        '
        Me.cmdContinue.Location = New System.Drawing.Point(601, 373)
        Me.cmdContinue.Name = "cmdContinue"
        Me.cmdContinue.Size = New System.Drawing.Size(100, 29)
        Me.cmdContinue.TabIndex = 1
        Me.cmdContinue.Text = "Continue"
        '
        'lblInput
        '
        Me.lblInput.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblInput.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblInput.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInput.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblInput.Location = New System.Drawing.Point(144, 375)
        Me.lblInput.Name = "lblInput"
        Me.lblInput.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblInput.Size = New System.Drawing.Size(57, 25)
        Me.lblInput.TabIndex = 7
        Me.lblInput.Text = "&Input:"
        '
        'frmOperatorMsg
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(741, 414)
        Me.ControlBox = False
        Me.Controls.Add(Me.InputData)
        Me.Controls.Add(Me.SSPanel1)
        Me.Controls.Add(Me.cmdContinue)
        Me.Controls.Add(Me.lblInput)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Location = New System.Drawing.Point(4, 30)
        Me.MinimumSize = New System.Drawing.Size(748, 216)
        Me.Name = "frmOperatorMsg"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.Text = "** Operator Action **"
        Me.SSPanel1.ResumeLayout(False)
        Me.msgInBox.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub
#End Region
End Class