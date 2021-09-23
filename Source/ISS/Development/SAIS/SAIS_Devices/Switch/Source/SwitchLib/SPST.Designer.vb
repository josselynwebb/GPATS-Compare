<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SPST
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SPST))
        Me.lblPin1 = New System.Windows.Forms.Label()
        Me.lblPin2 = New System.Windows.Forms.Label()
        Me.picSwitch = New System.Windows.Forms.PictureBox()
        Me.ImlSPST = New System.Windows.Forms.ImageList(Me.components)
        CType(Me.picSwitch, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblPin1
        '
        Me.lblPin1.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!)
        Me.lblPin1.Location = New System.Drawing.Point(0, 0)
        Me.lblPin1.Margin = New System.Windows.Forms.Padding(0, 0, 10, 0)
        Me.lblPin1.Name = "lblPin1"
        Me.lblPin1.Size = New System.Drawing.Size(25, 18)
        Me.lblPin1.TabIndex = 0
        Me.lblPin1.Text = "666"
        Me.lblPin1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPin2
        '
        Me.lblPin2.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!)
        Me.lblPin2.Location = New System.Drawing.Point(69, 0)
        Me.lblPin2.Margin = New System.Windows.Forms.Padding(0, 0, 10, 0)
        Me.lblPin2.Name = "lblPin2"
        Me.lblPin2.Size = New System.Drawing.Size(25, 18)
        Me.lblPin2.TabIndex = 1
        Me.lblPin2.Text = "200"
        Me.lblPin2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'picSwitch
        '
        Me.picSwitch.Location = New System.Drawing.Point(25, 0)
        Me.picSwitch.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.picSwitch.Name = "picSwitch"
        Me.picSwitch.Size = New System.Drawing.Size(44, 18)
        Me.picSwitch.TabIndex = 2
        Me.picSwitch.TabStop = False
        '
        'ImlSPST
        '
        Me.ImlSPST.ImageStream = CType(resources.GetObject("ImlSPST.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImlSPST.TransparentColor = System.Drawing.Color.Transparent
        Me.ImlSPST.Images.SetKeyName(0, "SPSTOpen.bmp")
        Me.ImlSPST.Images.SetKeyName(1, "SPSTClosed.bmp")
        '
        'SPST
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.picSwitch)
        Me.Controls.Add(Me.lblPin2)
        Me.Controls.Add(Me.lblPin1)
        Me.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Name = "SPST"
        Me.Size = New System.Drawing.Size(94, 18)
        CType(Me.picSwitch, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

End Sub
    Friend WithEvents lblPin1 As System.Windows.Forms.Label
    Friend WithEvents lblPin2 As System.Windows.Forms.Label
    Friend WithEvents picSwitch As System.Windows.Forms.PictureBox
    Friend WithEvents ImlSPST As System.Windows.Forms.ImageList

End Class
