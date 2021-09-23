<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DPST
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DPST))
        Me.lblPin1 = New System.Windows.Forms.Label()
        Me.lblPin3 = New System.Windows.Forms.Label()
        Me.picSwitch = New System.Windows.Forms.PictureBox()
        Me.lblPin2 = New System.Windows.Forms.Label()
        Me.lblPin4 = New System.Windows.Forms.Label()
        Me.ImlDPST = New System.Windows.Forms.ImageList()
        CType(Me.picSwitch, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblPin1
        '
        Me.lblPin1.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!)
        Me.lblPin1.Location = New System.Drawing.Point(-1, 3)
        Me.lblPin1.Margin = New System.Windows.Forms.Padding(0, 0, 10, 0)
        Me.lblPin1.Name = "lblPin1"
        Me.lblPin1.Size = New System.Drawing.Size(25, 15)
        Me.lblPin1.TabIndex = 0
        Me.lblPin1.Text = "1"
        Me.lblPin1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPin3
        '
        Me.lblPin3.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!)
        Me.lblPin3.Location = New System.Drawing.Point(0, 20)
        Me.lblPin3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblPin3.Name = "lblPin3"
        Me.lblPin3.Size = New System.Drawing.Size(25, 15)
        Me.lblPin3.TabIndex = 3
        Me.lblPin3.Text = "333"
        Me.lblPin3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'picSwitch
        '
        Me.picSwitch.Location = New System.Drawing.Point(25, 0)
        Me.picSwitch.Margin = New System.Windows.Forms.Padding(2)
        Me.picSwitch.Name = "picSwitch"
        Me.picSwitch.Size = New System.Drawing.Size(46, 36)
        Me.picSwitch.TabIndex = 2
        Me.picSwitch.TabStop = False
        '
        'lblPin2
        '
        Me.lblPin2.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!)
        Me.lblPin2.Location = New System.Drawing.Point(71, 4)
        Me.lblPin2.Margin = New System.Windows.Forms.Padding(0, 0, 10, 0)
        Me.lblPin2.Name = "lblPin2"
        Me.lblPin2.Size = New System.Drawing.Size(25, 15)
        Me.lblPin2.TabIndex = 3
        Me.lblPin2.Text = "2"
        Me.lblPin2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblPin4
        '
        Me.lblPin4.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!)
        Me.lblPin4.Location = New System.Drawing.Point(71, 21)
        Me.lblPin4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblPin4.Name = "lblPin4"
        Me.lblPin4.Size = New System.Drawing.Size(25, 15)
        Me.lblPin4.TabIndex = 400
        Me.lblPin4.Text = "400"
        Me.lblPin4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ImlDPST
        '
        Me.ImlDPST.ImageStream = CType(resources.GetObject("ImlDPST.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImlDPST.TransparentColor = System.Drawing.Color.Transparent
        Me.ImlDPST.Images.SetKeyName(0, "DPSTOpen.BMP")
        Me.ImlDPST.Images.SetKeyName(1, "DPSTClosed.BMP")
        '
        'DPST
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.lblPin4)
        Me.Controls.Add(Me.lblPin2)
        Me.Controls.Add(Me.picSwitch)
        Me.Controls.Add(Me.lblPin3)
        Me.Controls.Add(Me.lblPin1)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "DPST"
        Me.Size = New System.Drawing.Size(96, 36)
        CType(Me.picSwitch, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

End Sub
    Friend WithEvents lblPin1 As System.Windows.Forms.Label
    Friend WithEvents lblPin3 As System.Windows.Forms.Label
    Friend WithEvents picSwitch As System.Windows.Forms.PictureBox
    Friend WithEvents lblPin2 As System.Windows.Forms.Label
    Friend WithEvents lblPin4 As System.Windows.Forms.Label
    Friend WithEvents ImlDPST As System.Windows.Forms.ImageList

End Class
