<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctl1W2Row
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ctl1W2Row))
        Me.lblPin1 = New System.Windows.Forms.Label()
        Me.picTopSwitch = New System.Windows.Forms.PictureBox()
        Me.Iml1W2Row = New System.Windows.Forms.ImageList(Me.components)
        Me.picBottomSwitch = New System.Windows.Forms.PictureBox()
        CType(Me.picTopSwitch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picBottomSwitch, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblPin1
        '
        Me.lblPin1.Font = New System.Drawing.Font("Microsoft Sans Serif", 5.0!)
        Me.lblPin1.Location = New System.Drawing.Point(0, 0)
        Me.lblPin1.Margin = New System.Windows.Forms.Padding(0)
        Me.lblPin1.Name = "lblPin1"
        Me.lblPin1.Size = New System.Drawing.Size(18, 15)
        Me.lblPin1.TabIndex = 0
        Me.lblPin1.Text = "3"
        Me.lblPin1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'picTopSwitch
        '
        Me.picTopSwitch.Location = New System.Drawing.Point(0, 15)
        Me.picTopSwitch.Margin = New System.Windows.Forms.Padding(0)
        Me.picTopSwitch.Name = "picTopSwitch"
        Me.picTopSwitch.Size = New System.Drawing.Size(18, 17)
        Me.picTopSwitch.TabIndex = 1
        Me.picTopSwitch.TabStop = False
        '
        'Iml1W2Row
        '
        Me.Iml1W2Row.ImageStream = CType(resources.GetObject("Iml1W2Row.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.Iml1W2Row.TransparentColor = System.Drawing.Color.Transparent
        Me.Iml1W2Row.Images.SetKeyName(0, "1W2RowTopOpen.BMP")
        Me.Iml1W2Row.Images.SetKeyName(1, "1W2RowTopClosed.BMP")
        Me.Iml1W2Row.Images.SetKeyName(2, "1W2RowBottomOpen.BMP")
        Me.Iml1W2Row.Images.SetKeyName(3, "1W2RowBottomClosed.BMP")
        Me.Iml1W2Row.Images.SetKeyName(4, "1W2RowTopCornerOpen.BMP")
        Me.Iml1W2Row.Images.SetKeyName(5, "1W2RowTopCornerClosed.BMP")
        Me.Iml1W2Row.Images.SetKeyName(6, "1W2RowBottomCornerOpen.BMP")
        Me.Iml1W2Row.Images.SetKeyName(7, "1W2RowBottomCornerClosed.BMP")
        '
        'picBottomSwitch
        '
        Me.picBottomSwitch.Location = New System.Drawing.Point(0, 32)
        Me.picBottomSwitch.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.picBottomSwitch.Name = "picBottomSwitch"
        Me.picBottomSwitch.Size = New System.Drawing.Size(18, 17)
        Me.picBottomSwitch.TabIndex = 2
        Me.picBottomSwitch.TabStop = False
        '
        'ctl1W2Row
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.picBottomSwitch)
        Me.Controls.Add(Me.picTopSwitch)
        Me.Controls.Add(Me.lblPin1)
        Me.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Name = "ctl1W2Row"
        Me.Size = New System.Drawing.Size(18, 49)
        CType(Me.picTopSwitch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picBottomSwitch, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

End Sub
    Friend WithEvents lblPin1 As System.Windows.Forms.Label
    Friend WithEvents picTopSwitch As System.Windows.Forms.PictureBox
    Friend WithEvents Iml1W2Row As System.Windows.Forms.ImageList
    Friend WithEvents picBottomSwitch As System.Windows.Forms.PictureBox

End Class
