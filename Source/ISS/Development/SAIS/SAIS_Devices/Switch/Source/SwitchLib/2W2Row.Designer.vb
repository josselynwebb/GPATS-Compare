<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctl2W2Row
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ctl2W2Row))
        Me.Iml2W2Row = New System.Windows.Forms.ImageList(Me.components)
        Me.lblPin1 = New System.Windows.Forms.Label()
        Me.lblPin2 = New System.Windows.Forms.Label()
        Me.picTopSwitch = New System.Windows.Forms.PictureBox()
        Me.picBottomSwitch = New System.Windows.Forms.PictureBox()
        CType(Me.picTopSwitch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picBottomSwitch, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Iml2W2Row
        '
        Me.Iml2W2Row.ImageStream = CType(resources.GetObject("Iml2W2Row.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.Iml2W2Row.TransparentColor = System.Drawing.Color.Transparent
        Me.Iml2W2Row.Images.SetKeyName(0, "2W2RowTopOpen.BMP")
        Me.Iml2W2Row.Images.SetKeyName(1, "2W2RowTopClosed.BMP")
        Me.Iml2W2Row.Images.SetKeyName(2, "2W2RowBottomOpen.BMP")
        Me.Iml2W2Row.Images.SetKeyName(3, "2W2RowBottomClosed.BMP")
        Me.Iml2W2Row.Images.SetKeyName(4, "2W2RowTopCornerOpen.BMP")
        Me.Iml2W2Row.Images.SetKeyName(5, "2W2RowTopCornerClosed.BMP")
        Me.Iml2W2Row.Images.SetKeyName(6, "2W2RowBottomCornerOpen.BMP")
        Me.Iml2W2Row.Images.SetKeyName(7, "2W2RowBottomCornerClosed.BMP")
        '
        'lblPin1
        '
        Me.lblPin1.Font = New System.Drawing.Font("Microsoft Sans Serif", 5.0!)
        Me.lblPin1.Location = New System.Drawing.Point(0, 0)
        Me.lblPin1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblPin1.Name = "lblPin1"
        Me.lblPin1.Size = New System.Drawing.Size(18, 15)
        Me.lblPin1.TabIndex = 0
        Me.lblPin1.Text = "10"
        Me.lblPin1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblPin2
        '
        Me.lblPin2.Font = New System.Drawing.Font("Microsoft Sans Serif", 5.0!)
        Me.lblPin2.Location = New System.Drawing.Point(18, 0)
        Me.lblPin2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblPin2.Name = "lblPin2"
        Me.lblPin2.Size = New System.Drawing.Size(18, 15)
        Me.lblPin2.TabIndex = 1
        Me.lblPin2.Text = "20"
        Me.lblPin2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'picTopSwitch
        '
        Me.picTopSwitch.Location = New System.Drawing.Point(0, 15)
        Me.picTopSwitch.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.picTopSwitch.Name = "picTopSwitch"
        Me.picTopSwitch.Size = New System.Drawing.Size(36, 34)
        Me.picTopSwitch.TabIndex = 2
        Me.picTopSwitch.TabStop = False
        '
        'picBottomSwitch
        '
        Me.picBottomSwitch.Location = New System.Drawing.Point(0, 49)
        Me.picBottomSwitch.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.picBottomSwitch.Name = "picBottomSwitch"
        Me.picBottomSwitch.Size = New System.Drawing.Size(36, 34)
        Me.picBottomSwitch.TabIndex = 3
        Me.picBottomSwitch.TabStop = False
        '
        'ctl2W2Row
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.picBottomSwitch)
        Me.Controls.Add(Me.picTopSwitch)
        Me.Controls.Add(Me.lblPin2)
        Me.Controls.Add(Me.lblPin1)
        Me.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Name = "ctl2W2Row"
        Me.Size = New System.Drawing.Size(36, 83)
        CType(Me.picTopSwitch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picBottomSwitch, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

End Sub
    Friend WithEvents Iml2W2Row As System.Windows.Forms.ImageList
    Friend WithEvents lblPin1 As System.Windows.Forms.Label
    Friend WithEvents lblPin2 As System.Windows.Forms.Label
    Friend WithEvents picTopSwitch As System.Windows.Forms.PictureBox
    Friend WithEvents picBottomSwitch As System.Windows.Forms.PictureBox

End Class
