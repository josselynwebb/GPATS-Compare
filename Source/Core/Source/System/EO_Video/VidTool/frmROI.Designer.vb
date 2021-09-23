<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmROI
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
    Public WithEvents pboxROI As System.Windows.Forms.PictureBox
    Public WithEvents pnlScrollBox As System.Windows.Forms.Panel
    Public WithEvents cmdBack As System.Windows.Forms.Button
    Public WithEvents rtbFoVText As System.Windows.Forms.RichTextBox
    Public WithEvents cmdHelp As System.Windows.Forms.Button
    Public WithEvents tsStatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Public WithEvents StatusStripUserInfo As System.Windows.Forms.StatusStrip
    Public WithEvents cmdClose As System.Windows.Forms.Button
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmROI))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.pnlScrollBox = New System.Windows.Forms.Panel()
        Me.pboxROI = New System.Windows.Forms.PictureBox()
        Me.cmdBack = New System.Windows.Forms.Button()
        Me.rtbFoVText = New System.Windows.Forms.RichTextBox()
        Me.cmdHelp = New System.Windows.Forms.Button()
        Me.StatusStripUserInfo = New System.Windows.Forms.StatusStrip()
        Me.tsStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.gboxImage = New System.Windows.Forms.GroupBox()
        Me.lblPixel1 = New System.Windows.Forms.Label()
        Me.lblPixel0 = New System.Windows.Forms.Label()
        Me.txtImageHeight = New System.Windows.Forms.TextBox()
        Me.txtImageWidth = New System.Windows.Forms.TextBox()
        Me.lblImageW = New System.Windows.Forms.Label()
        Me.lblImageH = New System.Windows.Forms.Label()
        Me.gboxTarget = New System.Windows.Forms.GroupBox()
        Me.txtTargetHeight = New System.Windows.Forms.TextBox()
        Me.txtTargetWidth = New System.Windows.Forms.TextBox()
        Me.cmdClear = New System.Windows.Forms.Button()
        Me.cmdAccept = New System.Windows.Forms.Button()
        Me.lblTargHUom = New System.Windows.Forms.Label()
        Me.lblTargWUom = New System.Windows.Forms.Label()
        Me.lblTargWidth = New System.Windows.Forms.Label()
        Me.lblTargHeight = New System.Windows.Forms.Label()
        Me.gboxROI = New System.Windows.Forms.GroupBox()
        Me.gboxSize = New System.Windows.Forms.GroupBox()
        Me.txtHeight = New System.Windows.Forms.TextBox()
        Me.lblHeight = New System.Windows.Forms.Label()
        Me.txtWidth = New System.Windows.Forms.TextBox()
        Me.lblWidth = New System.Windows.Forms.Label()
        Me.gboxEnd = New System.Windows.Forms.GroupBox()
        Me.txtEndY = New System.Windows.Forms.TextBox()
        Me.lblEndY = New System.Windows.Forms.Label()
        Me.txtEndX = New System.Windows.Forms.TextBox()
        Me.lblEndX = New System.Windows.Forms.Label()
        Me.gboxStart = New System.Windows.Forms.GroupBox()
        Me.txtY = New System.Windows.Forms.TextBox()
        Me.lblStartY = New System.Windows.Forms.Label()
        Me.txtX = New System.Windows.Forms.TextBox()
        Me.lblStartX = New System.Windows.Forms.Label()
        Me.lblOpInstr = New System.Windows.Forms.Label()
        Me.pnlScrollBox.SuspendLayout()
        CType(Me.pboxROI, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStripUserInfo.SuspendLayout()
        Me.gboxImage.SuspendLayout()
        Me.gboxTarget.SuspendLayout()
        Me.gboxROI.SuspendLayout()
        Me.gboxSize.SuspendLayout()
        Me.gboxEnd.SuspendLayout()
        Me.gboxStart.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlScrollBox
        '
        Me.pnlScrollBox.AutoScroll = True
        Me.pnlScrollBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.pnlScrollBox.Controls.Add(Me.pboxROI)
        Me.pnlScrollBox.Cursor = System.Windows.Forms.Cursors.Default
        Me.pnlScrollBox.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlScrollBox.ForeColor = System.Drawing.SystemColors.WindowText
        Me.pnlScrollBox.Location = New System.Drawing.Point(12, 12)
        Me.pnlScrollBox.Name = "pnlScrollBox"
        Me.pnlScrollBox.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.pnlScrollBox.Size = New System.Drawing.Size(640, 480)
        Me.pnlScrollBox.TabIndex = 37
        Me.pnlScrollBox.TabStop = True
        '
        'pboxROI
        '
        Me.pboxROI.BackColor = System.Drawing.Color.White
        Me.pboxROI.Cursor = System.Windows.Forms.Cursors.Default
        Me.pboxROI.Dock = System.Windows.Forms.DockStyle.Top
        Me.pboxROI.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pboxROI.ForeColor = System.Drawing.SystemColors.ControlText
        Me.pboxROI.Location = New System.Drawing.Point(0, 0)
        Me.pboxROI.Name = "pboxROI"
        Me.pboxROI.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.pboxROI.Size = New System.Drawing.Size(640, 92)
        Me.pboxROI.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.pboxROI.TabIndex = 38
        Me.pboxROI.TabStop = False
        '
        'cmdBack
        '
        Me.cmdBack.BackColor = System.Drawing.SystemColors.Control
        Me.cmdBack.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdBack.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdBack.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdBack.Location = New System.Drawing.Point(682, 552)
        Me.cmdBack.Name = "cmdBack"
        Me.cmdBack.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdBack.Size = New System.Drawing.Size(57, 25)
        Me.cmdBack.TabIndex = 33
        Me.cmdBack.Text = "&Back"
        Me.cmdBack.UseVisualStyleBackColor = False
        '
        'rtbFoVText
        '
        Me.rtbFoVText.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtbFoVText.Location = New System.Drawing.Point(12, 552)
        Me.rtbFoVText.Name = "rtbFoVText"
        Me.rtbFoVText.ReadOnly = True
        Me.rtbFoVText.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical
        Me.rtbFoVText.Size = New System.Drawing.Size(514, 47)
        Me.rtbFoVText.TabIndex = 32
        Me.rtbFoVText.Text = ""
        Me.rtbFoVText.Visible = False
        '
        'cmdHelp
        '
        Me.cmdHelp.BackColor = System.Drawing.SystemColors.Control
        Me.cmdHelp.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdHelp.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdHelp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdHelp.Location = New System.Drawing.Point(758, 583)
        Me.cmdHelp.Name = "cmdHelp"
        Me.cmdHelp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdHelp.Size = New System.Drawing.Size(57, 25)
        Me.cmdHelp.TabIndex = 31
        Me.cmdHelp.Text = "&Help"
        Me.cmdHelp.UseVisualStyleBackColor = False
        Me.cmdHelp.Visible = False
        '
        'StatusStripUserInfo
        '
        Me.StatusStripUserInfo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.StatusStripUserInfo.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsStatusLabel})
        Me.StatusStripUserInfo.Location = New System.Drawing.Point(0, 620)
        Me.StatusStripUserInfo.Name = "StatusStripUserInfo"
        Me.StatusStripUserInfo.Size = New System.Drawing.Size(842, 22)
        Me.StatusStripUserInfo.TabIndex = 15
        '
        'tsStatusLabel
        '
        Me.tsStatusLabel.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.tsStatusLabel.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me.tsStatusLabel.Margin = New System.Windows.Forms.Padding(0)
        Me.tsStatusLabel.Name = "tsStatusLabel"
        Me.tsStatusLabel.Size = New System.Drawing.Size(827, 22)
        Me.tsStatusLabel.Spring = True
        Me.tsStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Location = New System.Drawing.Point(758, 552)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(57, 25)
        Me.cmdClose.TabIndex = 3
        Me.cmdClose.Text = "C&lose"
        Me.cmdClose.UseVisualStyleBackColor = False
        '
        'gboxImage
        '
        Me.gboxImage.Controls.Add(Me.lblPixel1)
        Me.gboxImage.Controls.Add(Me.lblPixel0)
        Me.gboxImage.Controls.Add(Me.txtImageHeight)
        Me.gboxImage.Controls.Add(Me.txtImageWidth)
        Me.gboxImage.Controls.Add(Me.lblImageW)
        Me.gboxImage.Controls.Add(Me.lblImageH)
        Me.gboxImage.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gboxImage.Location = New System.Drawing.Point(674, 15)
        Me.gboxImage.Name = "gboxImage"
        Me.gboxImage.Size = New System.Drawing.Size(149, 84)
        Me.gboxImage.TabIndex = 39
        Me.gboxImage.TabStop = False
        Me.gboxImage.Text = "Image Properties"
        '
        'lblPixel1
        '
        Me.lblPixel1.AutoSize = True
        Me.lblPixel1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPixel1.Location = New System.Drawing.Point(108, 54)
        Me.lblPixel1.Name = "lblPixel1"
        Me.lblPixel1.Size = New System.Drawing.Size(35, 14)
        Me.lblPixel1.TabIndex = 5
        Me.lblPixel1.Text = "Pixels"
        '
        'lblPixel0
        '
        Me.lblPixel0.AutoSize = True
        Me.lblPixel0.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPixel0.Location = New System.Drawing.Point(108, 28)
        Me.lblPixel0.Name = "lblPixel0"
        Me.lblPixel0.Size = New System.Drawing.Size(35, 14)
        Me.lblPixel0.TabIndex = 4
        Me.lblPixel0.Text = "Pixels"
        '
        'txtImageHeight
        '
        Me.txtImageHeight.Location = New System.Drawing.Point(54, 48)
        Me.txtImageHeight.Name = "txtImageHeight"
        Me.txtImageHeight.Size = New System.Drawing.Size(48, 20)
        Me.txtImageHeight.TabIndex = 3
        Me.txtImageHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtImageWidth
        '
        Me.txtImageWidth.Location = New System.Drawing.Point(53, 22)
        Me.txtImageWidth.Name = "txtImageWidth"
        Me.txtImageWidth.Size = New System.Drawing.Size(48, 20)
        Me.txtImageWidth.TabIndex = 2
        Me.txtImageWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblImageW
        '
        Me.lblImageW.AutoSize = True
        Me.lblImageW.Location = New System.Drawing.Point(6, 25)
        Me.lblImageW.Name = "lblImageW"
        Me.lblImageW.Size = New System.Drawing.Size(38, 14)
        Me.lblImageW.TabIndex = 1
        Me.lblImageW.Text = "Width"
        '
        'lblImageH
        '
        Me.lblImageH.AutoSize = True
        Me.lblImageH.Location = New System.Drawing.Point(6, 51)
        Me.lblImageH.Name = "lblImageH"
        Me.lblImageH.Size = New System.Drawing.Size(42, 14)
        Me.lblImageH.TabIndex = 0
        Me.lblImageH.Text = "Height"
        '
        'gboxTarget
        '
        Me.gboxTarget.Controls.Add(Me.txtTargetHeight)
        Me.gboxTarget.Controls.Add(Me.txtTargetWidth)
        Me.gboxTarget.Controls.Add(Me.cmdClear)
        Me.gboxTarget.Controls.Add(Me.cmdAccept)
        Me.gboxTarget.Controls.Add(Me.lblTargHUom)
        Me.gboxTarget.Controls.Add(Me.lblTargWUom)
        Me.gboxTarget.Controls.Add(Me.lblTargWidth)
        Me.gboxTarget.Controls.Add(Me.lblTargHeight)
        Me.gboxTarget.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gboxTarget.Location = New System.Drawing.Point(674, 105)
        Me.gboxTarget.Name = "gboxTarget"
        Me.gboxTarget.Size = New System.Drawing.Size(149, 113)
        Me.gboxTarget.TabIndex = 40
        Me.gboxTarget.TabStop = False
        Me.gboxTarget.Text = "Target Dimensions"
        '
        'txtTargetHeight
        '
        Me.txtTargetHeight.Location = New System.Drawing.Point(54, 48)
        Me.txtTargetHeight.Name = "txtTargetHeight"
        Me.txtTargetHeight.Size = New System.Drawing.Size(48, 20)
        Me.txtTargetHeight.TabIndex = 39
        Me.txtTargetHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtTargetWidth
        '
        Me.txtTargetWidth.Location = New System.Drawing.Point(53, 22)
        Me.txtTargetWidth.Name = "txtTargetWidth"
        Me.txtTargetWidth.Size = New System.Drawing.Size(48, 20)
        Me.txtTargetWidth.TabIndex = 38
        Me.txtTargetWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'cmdClear
        '
        Me.cmdClear.BackColor = System.Drawing.SystemColors.Control
        Me.cmdClear.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClear.Enabled = False
        Me.cmdClear.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClear.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClear.Location = New System.Drawing.Point(102, 74)
        Me.cmdClear.Name = "cmdClear"
        Me.cmdClear.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClear.Size = New System.Drawing.Size(41, 25)
        Me.cmdClear.TabIndex = 35
        Me.cmdClear.Text = "Clea&r"
        Me.cmdClear.UseVisualStyleBackColor = False
        '
        'cmdAccept
        '
        Me.cmdAccept.BackColor = System.Drawing.SystemColors.Control
        Me.cmdAccept.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdAccept.Enabled = False
        Me.cmdAccept.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAccept.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAccept.Location = New System.Drawing.Point(9, 74)
        Me.cmdAccept.Name = "cmdAccept"
        Me.cmdAccept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAccept.Size = New System.Drawing.Size(84, 25)
        Me.cmdAccept.TabIndex = 34
        Me.cmdAccept.Text = "&Calculate FoV"
        Me.cmdAccept.UseVisualStyleBackColor = False
        '
        'lblTargHUom
        '
        Me.lblTargHUom.AutoSize = True
        Me.lblTargHUom.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTargHUom.Location = New System.Drawing.Point(108, 54)
        Me.lblTargHUom.Name = "lblTargHUom"
        Me.lblTargHUom.Size = New System.Drawing.Size(34, 14)
        Me.lblTargHUom.TabIndex = 5
        Me.lblTargHUom.Text = "mRad"
        '
        'lblTargWUom
        '
        Me.lblTargWUom.AutoSize = True
        Me.lblTargWUom.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTargWUom.Location = New System.Drawing.Point(108, 28)
        Me.lblTargWUom.Name = "lblTargWUom"
        Me.lblTargWUom.Size = New System.Drawing.Size(34, 14)
        Me.lblTargWUom.TabIndex = 4
        Me.lblTargWUom.Text = "mRad"
        '
        'lblTargWidth
        '
        Me.lblTargWidth.AutoSize = True
        Me.lblTargWidth.Location = New System.Drawing.Point(6, 25)
        Me.lblTargWidth.Name = "lblTargWidth"
        Me.lblTargWidth.Size = New System.Drawing.Size(35, 14)
        Me.lblTargWidth.TabIndex = 1
        Me.lblTargWidth.Text = "Horiz"
        '
        'lblTargHeight
        '
        Me.lblTargHeight.AutoSize = True
        Me.lblTargHeight.Location = New System.Drawing.Point(6, 51)
        Me.lblTargHeight.Name = "lblTargHeight"
        Me.lblTargHeight.Size = New System.Drawing.Size(30, 14)
        Me.lblTargHeight.TabIndex = 0
        Me.lblTargHeight.Text = "Vert"
        '
        'gboxROI
        '
        Me.gboxROI.Controls.Add(Me.gboxSize)
        Me.gboxROI.Controls.Add(Me.gboxEnd)
        Me.gboxROI.Controls.Add(Me.gboxStart)
        Me.gboxROI.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gboxROI.Location = New System.Drawing.Point(674, 224)
        Me.gboxROI.Name = "gboxROI"
        Me.gboxROI.Size = New System.Drawing.Size(149, 179)
        Me.gboxROI.TabIndex = 41
        Me.gboxROI.TabStop = False
        Me.gboxROI.Text = "ROI Properties"
        '
        'gboxSize
        '
        Me.gboxSize.Controls.Add(Me.txtHeight)
        Me.gboxSize.Controls.Add(Me.lblHeight)
        Me.gboxSize.Controls.Add(Me.txtWidth)
        Me.gboxSize.Controls.Add(Me.lblWidth)
        Me.gboxSize.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gboxSize.Location = New System.Drawing.Point(9, 127)
        Me.gboxSize.Name = "gboxSize"
        Me.gboxSize.Size = New System.Drawing.Size(133, 47)
        Me.gboxSize.TabIndex = 2
        Me.gboxSize.TabStop = False
        Me.gboxSize.Text = "Size"
        '
        'txtHeight
        '
        Me.txtHeight.Location = New System.Drawing.Point(93, 19)
        Me.txtHeight.Name = "txtHeight"
        Me.txtHeight.Size = New System.Drawing.Size(27, 20)
        Me.txtHeight.TabIndex = 6
        Me.txtHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblHeight
        '
        Me.lblHeight.AutoSize = True
        Me.lblHeight.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeight.Location = New System.Drawing.Point(73, 22)
        Me.lblHeight.Name = "lblHeight"
        Me.lblHeight.Size = New System.Drawing.Size(14, 14)
        Me.lblHeight.TabIndex = 5
        Me.lblHeight.Text = "Y"
        '
        'txtWidth
        '
        Me.txtWidth.Location = New System.Drawing.Point(32, 19)
        Me.txtWidth.Name = "txtWidth"
        Me.txtWidth.Size = New System.Drawing.Size(27, 20)
        Me.txtWidth.TabIndex = 4
        Me.txtWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblWidth
        '
        Me.lblWidth.AutoSize = True
        Me.lblWidth.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWidth.Location = New System.Drawing.Point(6, 22)
        Me.lblWidth.Name = "lblWidth"
        Me.lblWidth.Size = New System.Drawing.Size(14, 14)
        Me.lblWidth.TabIndex = 3
        Me.lblWidth.Text = "X"
        '
        'gboxEnd
        '
        Me.gboxEnd.Controls.Add(Me.txtEndY)
        Me.gboxEnd.Controls.Add(Me.lblEndY)
        Me.gboxEnd.Controls.Add(Me.txtEndX)
        Me.gboxEnd.Controls.Add(Me.lblEndX)
        Me.gboxEnd.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gboxEnd.Location = New System.Drawing.Point(8, 74)
        Me.gboxEnd.Name = "gboxEnd"
        Me.gboxEnd.Size = New System.Drawing.Size(133, 47)
        Me.gboxEnd.TabIndex = 1
        Me.gboxEnd.TabStop = False
        Me.gboxEnd.Text = "Lower Right"
        '
        'txtEndY
        '
        Me.txtEndY.Location = New System.Drawing.Point(93, 19)
        Me.txtEndY.Name = "txtEndY"
        Me.txtEndY.Size = New System.Drawing.Size(27, 20)
        Me.txtEndY.TabIndex = 6
        Me.txtEndY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblEndY
        '
        Me.lblEndY.AutoSize = True
        Me.lblEndY.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEndY.Location = New System.Drawing.Point(73, 22)
        Me.lblEndY.Name = "lblEndY"
        Me.lblEndY.Size = New System.Drawing.Size(14, 14)
        Me.lblEndY.TabIndex = 5
        Me.lblEndY.Text = "Y"
        '
        'txtEndX
        '
        Me.txtEndX.Location = New System.Drawing.Point(32, 19)
        Me.txtEndX.Name = "txtEndX"
        Me.txtEndX.Size = New System.Drawing.Size(27, 20)
        Me.txtEndX.TabIndex = 4
        Me.txtEndX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblEndX
        '
        Me.lblEndX.AutoSize = True
        Me.lblEndX.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEndX.Location = New System.Drawing.Point(6, 22)
        Me.lblEndX.Name = "lblEndX"
        Me.lblEndX.Size = New System.Drawing.Size(14, 14)
        Me.lblEndX.TabIndex = 3
        Me.lblEndX.Text = "X"
        '
        'gboxStart
        '
        Me.gboxStart.Controls.Add(Me.txtY)
        Me.gboxStart.Controls.Add(Me.lblStartY)
        Me.gboxStart.Controls.Add(Me.txtX)
        Me.gboxStart.Controls.Add(Me.lblStartX)
        Me.gboxStart.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gboxStart.Location = New System.Drawing.Point(9, 19)
        Me.gboxStart.Name = "gboxStart"
        Me.gboxStart.Size = New System.Drawing.Size(133, 47)
        Me.gboxStart.TabIndex = 0
        Me.gboxStart.TabStop = False
        Me.gboxStart.Text = "Upper Left"
        '
        'txtY
        '
        Me.txtY.Location = New System.Drawing.Point(93, 19)
        Me.txtY.Name = "txtY"
        Me.txtY.Size = New System.Drawing.Size(27, 20)
        Me.txtY.TabIndex = 6
        Me.txtY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblStartY
        '
        Me.lblStartY.AutoSize = True
        Me.lblStartY.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStartY.Location = New System.Drawing.Point(73, 22)
        Me.lblStartY.Name = "lblStartY"
        Me.lblStartY.Size = New System.Drawing.Size(14, 14)
        Me.lblStartY.TabIndex = 5
        Me.lblStartY.Text = "Y"
        '
        'txtX
        '
        Me.txtX.Location = New System.Drawing.Point(32, 19)
        Me.txtX.Name = "txtX"
        Me.txtX.Size = New System.Drawing.Size(27, 20)
        Me.txtX.TabIndex = 4
        Me.txtX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblStartX
        '
        Me.lblStartX.AutoSize = True
        Me.lblStartX.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStartX.Location = New System.Drawing.Point(6, 22)
        Me.lblStartX.Name = "lblStartX"
        Me.lblStartX.Size = New System.Drawing.Size(14, 14)
        Me.lblStartX.TabIndex = 3
        Me.lblStartX.Text = "X"
        '
        'lblOpInstr
        '
        Me.lblOpInstr.AutoSize = True
        Me.lblOpInstr.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOpInstr.Location = New System.Drawing.Point(170, 535)
        Me.lblOpInstr.Name = "lblOpInstr"
        Me.lblOpInstr.Size = New System.Drawing.Size(175, 14)
        Me.lblOpInstr.TabIndex = 42
        Me.lblOpInstr.Text = "*** OPERATOR INSTRUCTIONS ***"
        Me.lblOpInstr.Visible = False
        '
        'frmROI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(842, 642)
        Me.Controls.Add(Me.lblOpInstr)
        Me.Controls.Add(Me.gboxROI)
        Me.Controls.Add(Me.gboxTarget)
        Me.Controls.Add(Me.gboxImage)
        Me.Controls.Add(Me.pnlScrollBox)
        Me.Controls.Add(Me.cmdBack)
        Me.Controls.Add(Me.rtbFoVText)
        Me.Controls.Add(Me.cmdHelp)
        Me.Controls.Add(Me.StatusStripUserInfo)
        Me.Controls.Add(Me.cmdClose)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.Black
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(4, 23)
        Me.Name = "frmROI"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Video Display"
        Me.pnlScrollBox.ResumeLayout(False)
        Me.pnlScrollBox.PerformLayout()
        CType(Me.pboxROI, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusStripUserInfo.ResumeLayout(False)
        Me.StatusStripUserInfo.PerformLayout()
        Me.gboxImage.ResumeLayout(False)
        Me.gboxImage.PerformLayout()
        Me.gboxTarget.ResumeLayout(False)
        Me.gboxTarget.PerformLayout()
        Me.gboxROI.ResumeLayout(False)
        Me.gboxSize.ResumeLayout(False)
        Me.gboxSize.PerformLayout()
        Me.gboxEnd.ResumeLayout(False)
        Me.gboxEnd.PerformLayout()
        Me.gboxStart.ResumeLayout(False)
        Me.gboxStart.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

End Sub
    Friend WithEvents gboxImage As System.Windows.Forms.GroupBox
    Friend WithEvents lblImageH As System.Windows.Forms.Label
    Friend WithEvents lblImageW As System.Windows.Forms.Label
    Friend WithEvents txtImageWidth As System.Windows.Forms.TextBox
    Friend WithEvents lblPixel0 As System.Windows.Forms.Label
    Friend WithEvents txtImageHeight As System.Windows.Forms.TextBox
    Friend WithEvents lblPixel1 As System.Windows.Forms.Label
    Friend WithEvents gboxTarget As System.Windows.Forms.GroupBox
    Friend WithEvents lblTargHUom As System.Windows.Forms.Label
    Friend WithEvents lblTargWUom As System.Windows.Forms.Label
    Friend WithEvents lblTargWidth As System.Windows.Forms.Label
    Friend WithEvents lblTargHeight As System.Windows.Forms.Label
    Public WithEvents cmdClear As System.Windows.Forms.Button
    Public WithEvents cmdAccept As System.Windows.Forms.Button
    Friend WithEvents gboxROI As System.Windows.Forms.GroupBox
    Friend WithEvents gboxStart As System.Windows.Forms.GroupBox
    Friend WithEvents txtX As System.Windows.Forms.TextBox
    Friend WithEvents lblStartX As System.Windows.Forms.Label
    Friend WithEvents txtY As System.Windows.Forms.TextBox
    Friend WithEvents lblStartY As System.Windows.Forms.Label
    Friend WithEvents gboxEnd As System.Windows.Forms.GroupBox
    Friend WithEvents txtEndY As System.Windows.Forms.TextBox
    Friend WithEvents lblEndY As System.Windows.Forms.Label
    Friend WithEvents txtEndX As System.Windows.Forms.TextBox
    Friend WithEvents lblEndX As System.Windows.Forms.Label
    Friend WithEvents gboxSize As System.Windows.Forms.GroupBox
    Friend WithEvents txtHeight As System.Windows.Forms.TextBox
    Friend WithEvents lblHeight As System.Windows.Forms.Label
    Friend WithEvents txtWidth As System.Windows.Forms.TextBox
    Friend WithEvents lblWidth As System.Windows.Forms.Label
    Friend WithEvents lblOpInstr As System.Windows.Forms.Label
    Friend WithEvents txtTargetWidth As System.Windows.Forms.TextBox
    Friend WithEvents txtTargetHeight As System.Windows.Forms.TextBox
#End Region
End Class