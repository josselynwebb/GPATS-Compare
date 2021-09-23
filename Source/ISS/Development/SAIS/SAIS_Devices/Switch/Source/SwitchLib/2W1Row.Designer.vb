<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctl2W1Row
    Inherits System.Windows.Forms.UserControl

    'UserControl1 overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ctl2W1Row))
        Me.picSwitch = New System.Windows.Forms.PictureBox()
        Me.lblPin1 = New System.Windows.Forms.Label()
        Me.Iml2W1Row = New System.Windows.Forms.ImageList(Me.components)
        Me.lblPin2 = New System.Windows.Forms.Label()
        CType(Me.picSwitch, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'picSwitch
        '
        Me.picSwitch.Location = New System.Drawing.Point(0, 15)
        Me.picSwitch.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.picSwitch.Name = "picSwitch"
        Me.picSwitch.Size = New System.Drawing.Size(36, 34)
        Me.picSwitch.TabIndex = 0
        Me.picSwitch.TabStop = False
        '
        'lblPin1
        '
        Me.lblPin1.Font = New System.Drawing.Font("Microsoft Sans Serif", 5.0!)
        Me.lblPin1.Location = New System.Drawing.Point(0, 0)
        Me.lblPin1.Margin = New System.Windows.Forms.Padding(0)
        Me.lblPin1.Name = "lblPin1"
        Me.lblPin1.Size = New System.Drawing.Size(18, 15)
        Me.lblPin1.TabIndex = 3
        Me.lblPin1.Text = "2"
        Me.lblPin1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Iml2W1Row
        '
        Me.Iml2W1Row.ImageStream = CType(resources.GetObject("Iml2W1Row.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.Iml2W1Row.TransparentColor = System.Drawing.Color.Transparent
        Me.Iml2W1Row.Images.SetKeyName(0, "2W1RowOpen.BMP")
        Me.Iml2W1Row.Images.SetKeyName(1, "2W1RowClosed.BMP")
        Me.Iml2W1Row.Images.SetKeyName(2, "2W1RowCornerOpen.BMP")
        Me.Iml2W1Row.Images.SetKeyName(3, "2W1RowCornerClosed.BMP")
        '
        'lblPin2
        '
        Me.lblPin2.Font = New System.Drawing.Font("Microsoft Sans Serif", 5.0!)
        Me.lblPin2.Location = New System.Drawing.Point(18, 0)
        Me.lblPin2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblPin2.Name = "lblPin2"
        Me.lblPin2.Size = New System.Drawing.Size(18, 15)
        Me.lblPin2.TabIndex = 4
        Me.lblPin2.Text = "30"
        Me.lblPin2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ctl2W1Row
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.lblPin2)
        Me.Controls.Add(Me.lblPin1)
        Me.Controls.Add(Me.picSwitch)
        Me.Margin = New System.Windows.Forms.Padding(0)
        Me.Name = "ctl2W1Row"
        Me.Size = New System.Drawing.Size(36, 49)
        CType(Me.picSwitch, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

End Sub
    Friend WithEvents picSwitch As System.Windows.Forms.PictureBox
    Friend WithEvents lblPin1 As System.Windows.Forms.Label
    Friend WithEvents Iml2W1Row As System.Windows.Forms.ImageList
    Friend WithEvents lblPin2 As System.Windows.Forms.Label

End Class
