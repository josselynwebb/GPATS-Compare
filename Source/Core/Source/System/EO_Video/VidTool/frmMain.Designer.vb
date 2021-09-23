<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmMain
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
    Public WithEvents optSrc0 As System.Windows.Forms.RadioButton
    Public WithEvents optSrc1 As System.Windows.Forms.RadioButton
    Public WithEvents optSrc2 As System.Windows.Forms.RadioButton
    Public WithEvents gboxSrcStage As System.Windows.Forms.GroupBox
    Public WithEvents optSens2 As System.Windows.Forms.RadioButton
    Public WithEvents optSens1 As System.Windows.Forms.RadioButton
    Public WithEvents optSens0 As System.Windows.Forms.RadioButton
    Public WithEvents gboxSensStage As System.Windows.Forms.GroupBox
    Public WithEvents pboxDisplay As System.Windows.Forms.PictureBox
    Public WithEvents pnlDispHolder As System.Windows.Forms.Panel
    Public WithEvents chkLiveDisplay As System.Windows.Forms.CheckBox
    Public WithEvents pboxLight0 As System.Windows.Forms.PictureBox
    Public WithEvents pboxLight1 As System.Windows.Forms.PictureBox
    Public WithEvents pboxLight2 As System.Windows.Forms.PictureBox
    Public WithEvents gboxEOPower As System.Windows.Forms.GroupBox
    Public WithEvents txtInstructions As System.Windows.Forms.TextBox
    Public WithEvents tabpVisible As System.Windows.Forms.TabPage
    Public WithEvents tabpInfrared As System.Windows.Forms.TabPage
    Public WithEvents chkCameraPower As System.Windows.Forms.CheckBox
    Public WithEvents tabpLaser As System.Windows.Forms.TabPage
    Public WithEvents tabAsset As System.Windows.Forms.TabControl
    Public WithEvents ImageList1 As System.Windows.Forms.ImageList
    Public WithEvents TargetImageList As System.Windows.Forms.ImageList
    Public WithEvents lblInstructions As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.chkCameraPower = New System.Windows.Forms.CheckBox()
        Me.gboxSrcStage = New System.Windows.Forms.GroupBox()
        Me.optSrc0 = New System.Windows.Forms.RadioButton()
        Me.optSrc1 = New System.Windows.Forms.RadioButton()
        Me.optSrc2 = New System.Windows.Forms.RadioButton()
        Me.gboxSensStage = New System.Windows.Forms.GroupBox()
        Me.optSens2 = New System.Windows.Forms.RadioButton()
        Me.optSens1 = New System.Windows.Forms.RadioButton()
        Me.optSens0 = New System.Windows.Forms.RadioButton()
        Me.pnlDispHolder = New System.Windows.Forms.Panel()
        Me.pboxDisplay = New System.Windows.Forms.PictureBox()
        Me.chkLiveDisplay = New System.Windows.Forms.CheckBox()
        Me.gboxEOPower = New System.Windows.Forms.GroupBox()
        Me.pboxLight0 = New System.Windows.Forms.PictureBox()
        Me.pboxLight1 = New System.Windows.Forms.PictureBox()
        Me.pboxLight2 = New System.Windows.Forms.PictureBox()
        Me.txtInstructions = New System.Windows.Forms.TextBox()
        Me.tabAsset = New System.Windows.Forms.TabControl()
        Me.tabpVisible = New System.Windows.Forms.TabPage()
        Me.gboxRadiance = New System.Windows.Forms.GroupBox()
        Me.lblRequestRadiance = New System.Windows.Forms.Label()
        Me.PanSetRadiance = New System.Windows.Forms.Panel()
        Me.txtSetRadiance = New System.Windows.Forms.TextBox()
        Me.SpinRadiance = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.txtCurrRadiance = New System.Windows.Forms.TextBox()
        Me.cmdSetRadiance = New System.Windows.Forms.Button()
        Me.lblRadiance = New System.Windows.Forms.Label()
        Me.gboxVisTgtPos = New System.Windows.Forms.GroupBox()
        Me.cmdSetVisPos = New System.Windows.Forms.Button()
        Me.cboVisWheel = New System.Windows.Forms.ComboBox()
        Me.pboxVisTarget = New System.Windows.Forms.PictureBox()
        Me.txtVisWheel = New System.Windows.Forms.TextBox()
        Me.lblVisTgtPos = New System.Windows.Forms.Label()
        Me.tabpInfrared = New System.Windows.Forms.TabPage()
        Me.gboxDiffTemp = New System.Windows.Forms.GroupBox()
        Me.lblReqDelta = New System.Windows.Forms.Label()
        Me.panSetDT = New System.Windows.Forms.Panel()
        Me.txtSetDT = New System.Windows.Forms.TextBox()
        Me.spinSetDT = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.txtBLB_Out = New System.Windows.Forms.TextBox()
        Me.lblDeltaT2 = New System.Windows.Forms.Label()
        Me.cmdSetDT = New System.Windows.Forms.Button()
        Me.txtAMT_Out = New System.Windows.Forms.TextBox()
        Me.lblDeltaT1 = New System.Windows.Forms.Label()
        Me.gboxIrTgt = New System.Windows.Forms.GroupBox()
        Me.cmdSetIRTarget = New System.Windows.Forms.Button()
        Me.cboIR_TargetWheel = New System.Windows.Forms.ComboBox()
        Me.pboxIRTarget = New System.Windows.Forms.PictureBox()
        Me.txtCurrIRTargPos = New System.Windows.Forms.TextBox()
        Me.lblIRTargPos = New System.Windows.Forms.Label()
        Me.tabpLaser = New System.Windows.Forms.TabPage()
        Me.gboxSources = New System.Windows.Forms.GroupBox()
        Me.panSetPA = New System.Windows.Forms.Panel()
        Me.txtSetPA = New System.Windows.Forms.TextBox()
        Me.SpinSetPA = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.panSetElevation = New System.Windows.Forms.Panel()
        Me.txtSetElevation = New System.Windows.Forms.TextBox()
        Me.SpinSetElevation = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.panSetAzimuth = New System.Windows.Forms.Panel()
        Me.txtSetAzimuth = New System.Windows.Forms.TextBox()
        Me.SpinSetAzimuth = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.cmdSetLARRS = New System.Windows.Forms.Button()
        Me.lblLPolarization = New System.Windows.Forms.Label()
        Me.lblElevation = New System.Windows.Forms.Label()
        Me.lblAzimuth = New System.Windows.Forms.Label()
        Me.txtVal4 = New System.Windows.Forms.TextBox()
        Me.lblCValueLPA = New System.Windows.Forms.Label()
        Me.txtVal2 = New System.Windows.Forms.TextBox()
        Me.lblLCurrentEL = New System.Windows.Forms.Label()
        Me.txtVal3 = New System.Windows.Forms.TextBox()
        Me.lblLCurrentAZ = New System.Windows.Forms.Label()
        Me.gboxLaser = New System.Windows.Forms.GroupBox()
        Me.panSetPP = New System.Windows.Forms.Panel()
        Me.txtSetPP = New System.Windows.Forms.TextBox()
        Me.SpinSetPP = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.panSetAmplitude = New System.Windows.Forms.Panel()
        Me.txtSetAmplitude = New System.Windows.Forms.TextBox()
        Me.SpinSetSetAmplitude = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.cmdDiodePower = New System.Windows.Forms.Button()
        Me.lblPower = New System.Windows.Forms.Label()
        Me.pboxBeam = New System.Windows.Forms.PictureBox()
        Me.cmdLaserSet = New System.Windows.Forms.Button()
        Me.lblPulsePeriod = New System.Windows.Forms.Label()
        Me.lblLaserTrigger = New System.Windows.Forms.Label()
        Me.lblAmplitudeLaser = New System.Windows.Forms.Label()
        Me.cboLaserTrigger = New System.Windows.Forms.ComboBox()
        Me.lblWaveLength = New System.Windows.Forms.Label()
        Me.cboDiode = New System.Windows.Forms.ComboBox()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.TargetImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.lblInstructions = New System.Windows.Forms.Label()
        Me.lblAsset = New System.Windows.Forms.Label()
        Me.cmdAlign = New System.Windows.Forms.Button()
        Me.cmdCapture = New System.Windows.Forms.Button()
        Me.cmdAbout = New System.Windows.Forms.Button()
        Me.cmdEOPower = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.tsStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.gboxSrcStage.SuspendLayout()
        Me.gboxSensStage.SuspendLayout()
        Me.pnlDispHolder.SuspendLayout()
        CType(Me.pboxDisplay, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gboxEOPower.SuspendLayout()
        CType(Me.pboxLight0, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pboxLight1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pboxLight2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabAsset.SuspendLayout()
        Me.tabpVisible.SuspendLayout()
        Me.gboxRadiance.SuspendLayout()
        Me.PanSetRadiance.SuspendLayout()
        CType(Me.SpinRadiance, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gboxVisTgtPos.SuspendLayout()
        CType(Me.pboxVisTarget, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabpInfrared.SuspendLayout()
        Me.gboxDiffTemp.SuspendLayout()
        Me.panSetDT.SuspendLayout()
        CType(Me.spinSetDT, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gboxIrTgt.SuspendLayout()
        CType(Me.pboxIRTarget, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabpLaser.SuspendLayout()
        Me.gboxSources.SuspendLayout()
        Me.panSetPA.SuspendLayout()
        CType(Me.SpinSetPA, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panSetElevation.SuspendLayout()
        CType(Me.SpinSetElevation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panSetAzimuth.SuspendLayout()
        CType(Me.SpinSetAzimuth, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gboxLaser.SuspendLayout()
        Me.panSetPP.SuspendLayout()
        CType(Me.SpinSetPP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panSetAmplitude.SuspendLayout()
        CType(Me.SpinSetSetAmplitude, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pboxBeam, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'chkCameraPower
        '
        Me.chkCameraPower.BackColor = System.Drawing.Color.Silver
        Me.chkCameraPower.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCameraPower.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCameraPower.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkCameraPower.Location = New System.Drawing.Point(8, 312)
        Me.chkCameraPower.Name = "chkCameraPower"
        Me.chkCameraPower.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCameraPower.Size = New System.Drawing.Size(195, 25)
        Me.chkCameraPower.TabIndex = 73
        Me.chkCameraPower.Text = "VEO2 Internal Camera Power On"
        Me.ToolTip1.SetToolTip(Me.chkCameraPower, "Power On/Off for the internal VEO2 Camera")
        Me.chkCameraPower.UseVisualStyleBackColor = False
        '
        'gboxSrcStage
        '
        Me.gboxSrcStage.BackColor = System.Drawing.Color.Silver
        Me.gboxSrcStage.Controls.Add(Me.optSrc0)
        Me.gboxSrcStage.Controls.Add(Me.optSrc1)
        Me.gboxSrcStage.Controls.Add(Me.optSrc2)
        Me.gboxSrcStage.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gboxSrcStage.ForeColor = System.Drawing.SystemColors.Highlight
        Me.gboxSrcStage.Location = New System.Drawing.Point(109, 640)
        Me.gboxSrcStage.Name = "gboxSrcStage"
        Me.gboxSrcStage.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gboxSrcStage.Size = New System.Drawing.Size(164, 57)
        Me.gboxSrcStage.TabIndex = 68
        Me.gboxSrcStage.TabStop = False
        Me.gboxSrcStage.Text = "Source Stage"
        '
        'optSrc0
        '
        Me.optSrc0.BackColor = System.Drawing.Color.Silver
        Me.optSrc0.Cursor = System.Windows.Forms.Cursors.Default
        Me.optSrc0.Enabled = False
        Me.optSrc0.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optSrc0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optSrc0.Location = New System.Drawing.Point(8, 16)
        Me.optSrc0.Name = "optSrc0"
        Me.optSrc0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optSrc0.Size = New System.Drawing.Size(81, 17)
        Me.optSrc0.TabIndex = 71
        Me.optSrc0.TabStop = True
        Me.optSrc0.Text = "Blackbody"
        Me.optSrc0.UseVisualStyleBackColor = False
        '
        'optSrc1
        '
        Me.optSrc1.BackColor = System.Drawing.Color.Silver
        Me.optSrc1.Cursor = System.Windows.Forms.Cursors.Default
        Me.optSrc1.Enabled = False
        Me.optSrc1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optSrc1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optSrc1.Location = New System.Drawing.Point(8, 32)
        Me.optSrc1.Name = "optSrc1"
        Me.optSrc1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optSrc1.Size = New System.Drawing.Size(129, 17)
        Me.optSrc1.TabIndex = 70
        Me.optSrc1.TabStop = True
        Me.optSrc1.Text = "Visible Sphere"
        Me.optSrc1.UseVisualStyleBackColor = False
        '
        'optSrc2
        '
        Me.optSrc2.BackColor = System.Drawing.Color.Silver
        Me.optSrc2.Checked = True
        Me.optSrc2.Cursor = System.Windows.Forms.Cursors.Default
        Me.optSrc2.Enabled = False
        Me.optSrc2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optSrc2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optSrc2.Location = New System.Drawing.Point(96, 16)
        Me.optSrc2.Name = "optSrc2"
        Me.optSrc2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optSrc2.Size = New System.Drawing.Size(62, 17)
        Me.optSrc2.TabIndex = 69
        Me.optSrc2.TabStop = True
        Me.optSrc2.Text = "Laser"
        Me.optSrc2.UseVisualStyleBackColor = False
        '
        'gboxSensStage
        '
        Me.gboxSensStage.BackColor = System.Drawing.Color.Silver
        Me.gboxSensStage.Controls.Add(Me.optSens2)
        Me.gboxSensStage.Controls.Add(Me.optSens1)
        Me.gboxSensStage.Controls.Add(Me.optSens0)
        Me.gboxSensStage.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gboxSensStage.ForeColor = System.Drawing.SystemColors.Highlight
        Me.gboxSensStage.Location = New System.Drawing.Point(295, 640)
        Me.gboxSensStage.Name = "gboxSensStage"
        Me.gboxSensStage.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gboxSensStage.Size = New System.Drawing.Size(241, 57)
        Me.gboxSensStage.TabIndex = 64
        Me.gboxSensStage.TabStop = False
        Me.gboxSensStage.Text = "Sensor Stage"
        '
        'optSens2
        '
        Me.optSens2.BackColor = System.Drawing.Color.Silver
        Me.optSens2.Cursor = System.Windows.Forms.Cursors.Default
        Me.optSens2.Enabled = False
        Me.optSens2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optSens2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optSens2.Location = New System.Drawing.Point(8, 32)
        Me.optSens2.Name = "optSens2"
        Me.optSens2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optSens2.Size = New System.Drawing.Size(160, 17)
        Me.optSens2.TabIndex = 67
        Me.optSens2.TabStop = True
        Me.optSens2.Text = "Camera (Beam Splitter)"
        Me.optSens2.UseVisualStyleBackColor = False
        '
        'optSens1
        '
        Me.optSens1.BackColor = System.Drawing.Color.Silver
        Me.optSens1.Cursor = System.Windows.Forms.Cursors.Default
        Me.optSens1.Enabled = False
        Me.optSens1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optSens1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optSens1.Location = New System.Drawing.Point(144, 16)
        Me.optSens1.Name = "optSens1"
        Me.optSens1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optSens1.Size = New System.Drawing.Size(89, 17)
        Me.optSens1.TabIndex = 66
        Me.optSens1.TabStop = True
        Me.optSens1.Text = "Energy Meter"
        Me.optSens1.UseVisualStyleBackColor = False
        '
        'optSens0
        '
        Me.optSens0.BackColor = System.Drawing.Color.Silver
        Me.optSens0.Checked = True
        Me.optSens0.Cursor = System.Windows.Forms.Cursors.Default
        Me.optSens0.Enabled = False
        Me.optSens0.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optSens0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optSens0.Location = New System.Drawing.Point(8, 16)
        Me.optSens0.Name = "optSens0"
        Me.optSens0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optSens0.Size = New System.Drawing.Size(130, 17)
        Me.optSens0.TabIndex = 65
        Me.optSens0.TabStop = True
        Me.optSens0.Text = "None (Pass Through)"
        Me.optSens0.UseVisualStyleBackColor = False
        '
        'pnlDispHolder
        '
        Me.pnlDispHolder.AutoScroll = True
        Me.pnlDispHolder.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.pnlDispHolder.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.pnlDispHolder.Controls.Add(Me.pboxDisplay)
        Me.pnlDispHolder.Cursor = System.Windows.Forms.Cursors.Default
        Me.pnlDispHolder.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlDispHolder.ForeColor = System.Drawing.SystemColors.ControlText
        Me.pnlDispHolder.Location = New System.Drawing.Point(0, 0)
        Me.pnlDispHolder.Name = "pnlDispHolder"
        Me.pnlDispHolder.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.pnlDispHolder.Size = New System.Drawing.Size(640, 480)
        Me.pnlDispHolder.TabIndex = 50
        '
        'pboxDisplay
        '
        Me.pboxDisplay.BackColor = System.Drawing.SystemColors.Window
        Me.pboxDisplay.Cursor = System.Windows.Forms.Cursors.Default
        Me.pboxDisplay.Dock = System.Windows.Forms.DockStyle.Top
        Me.pboxDisplay.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pboxDisplay.ForeColor = System.Drawing.SystemColors.WindowText
        Me.pboxDisplay.Location = New System.Drawing.Point(0, 0)
        Me.pboxDisplay.Name = "pboxDisplay"
        Me.pboxDisplay.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.pboxDisplay.Size = New System.Drawing.Size(640, 73)
        Me.pboxDisplay.TabIndex = 51
        Me.pboxDisplay.TabStop = False
        '
        'chkLiveDisplay
        '
        Me.chkLiveDisplay.BackColor = System.Drawing.Color.Silver
        Me.chkLiveDisplay.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkLiveDisplay.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkLiveDisplay.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkLiveDisplay.Location = New System.Drawing.Point(658, 640)
        Me.chkLiveDisplay.Name = "chkLiveDisplay"
        Me.chkLiveDisplay.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkLiveDisplay.Size = New System.Drawing.Size(84, 17)
        Me.chkLiveDisplay.TabIndex = 44
        Me.chkLiveDisplay.Text = "Live Display"
        Me.chkLiveDisplay.UseVisualStyleBackColor = False
        Me.chkLiveDisplay.Visible = False
        '
        'gboxEOPower
        '
        Me.gboxEOPower.BackColor = System.Drawing.Color.Silver
        Me.gboxEOPower.Controls.Add(Me.pboxLight0)
        Me.gboxEOPower.Controls.Add(Me.pboxLight1)
        Me.gboxEOPower.Controls.Add(Me.pboxLight2)
        Me.gboxEOPower.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gboxEOPower.ForeColor = System.Drawing.SystemColors.ControlText
        Me.gboxEOPower.Location = New System.Drawing.Point(553, 656)
        Me.gboxEOPower.Name = "gboxEOPower"
        Me.gboxEOPower.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gboxEOPower.Size = New System.Drawing.Size(89, 41)
        Me.gboxEOPower.TabIndex = 15
        Me.gboxEOPower.TabStop = False
        Me.gboxEOPower.Text = "EO Power On"
        '
        'pboxLight0
        '
        Me.pboxLight0.Cursor = System.Windows.Forms.Cursors.Default
        Me.pboxLight0.Location = New System.Drawing.Point(8, 15)
        Me.pboxLight0.Name = "pboxLight0"
        Me.pboxLight0.Size = New System.Drawing.Size(16, 16)
        Me.pboxLight0.TabIndex = 0
        Me.pboxLight0.TabStop = False
        '
        'pboxLight1
        '
        Me.pboxLight1.Cursor = System.Windows.Forms.Cursors.Default
        Me.pboxLight1.Location = New System.Drawing.Point(36, 15)
        Me.pboxLight1.Name = "pboxLight1"
        Me.pboxLight1.Size = New System.Drawing.Size(16, 16)
        Me.pboxLight1.TabIndex = 1
        Me.pboxLight1.TabStop = False
        '
        'pboxLight2
        '
        Me.pboxLight2.Cursor = System.Windows.Forms.Cursors.Default
        Me.pboxLight2.Location = New System.Drawing.Point(64, 15)
        Me.pboxLight2.Name = "pboxLight2"
        Me.pboxLight2.Size = New System.Drawing.Size(16, 16)
        Me.pboxLight2.TabIndex = 2
        Me.pboxLight2.TabStop = False
        '
        'txtInstructions
        '
        Me.txtInstructions.AcceptsReturn = True
        Me.txtInstructions.BackColor = System.Drawing.SystemColors.Window
        Me.txtInstructions.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtInstructions.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInstructions.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtInstructions.Location = New System.Drawing.Point(13, 519)
        Me.txtInstructions.MaxLength = 0
        Me.txtInstructions.Multiline = True
        Me.txtInstructions.Name = "txtInstructions"
        Me.txtInstructions.ReadOnly = True
        Me.txtInstructions.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtInstructions.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtInstructions.Size = New System.Drawing.Size(345, 54)
        Me.txtInstructions.TabIndex = 3
        Me.txtInstructions.Visible = False
        '
        'tabAsset
        '
        Me.tabAsset.Controls.Add(Me.tabpVisible)
        Me.tabAsset.Controls.Add(Me.tabpInfrared)
        Me.tabAsset.Controls.Add(Me.tabpLaser)
        Me.tabAsset.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tabAsset.ItemSize = New System.Drawing.Size(42, 18)
        Me.tabAsset.Location = New System.Drawing.Point(955, 8)
        Me.tabAsset.Name = "tabAsset"
        Me.tabAsset.SelectedIndex = 0
        Me.tabAsset.Size = New System.Drawing.Size(238, 540)
        Me.tabAsset.TabIndex = 5
        '
        'tabpVisible
        '
        Me.tabpVisible.BackColor = System.Drawing.Color.Silver
        Me.tabpVisible.Controls.Add(Me.gboxRadiance)
        Me.tabpVisible.Controls.Add(Me.gboxVisTgtPos)
        Me.tabpVisible.Location = New System.Drawing.Point(4, 22)
        Me.tabpVisible.Name = "tabpVisible"
        Me.tabpVisible.Size = New System.Drawing.Size(230, 514)
        Me.tabpVisible.TabIndex = 0
        Me.tabpVisible.Text = "Visible"
        '
        'gboxRadiance
        '
        Me.gboxRadiance.Controls.Add(Me.lblRequestRadiance)
        Me.gboxRadiance.Controls.Add(Me.PanSetRadiance)
        Me.gboxRadiance.Controls.Add(Me.txtCurrRadiance)
        Me.gboxRadiance.Controls.Add(Me.cmdSetRadiance)
        Me.gboxRadiance.Controls.Add(Me.lblRadiance)
        Me.gboxRadiance.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gboxRadiance.ForeColor = System.Drawing.SystemColors.Highlight
        Me.gboxRadiance.Location = New System.Drawing.Point(13, 310)
        Me.gboxRadiance.Name = "gboxRadiance"
        Me.gboxRadiance.Size = New System.Drawing.Size(209, 186)
        Me.gboxRadiance.TabIndex = 1
        Me.gboxRadiance.TabStop = False
        Me.gboxRadiance.Text = "Radiance"
        '
        'lblRequestRadiance
        '
        Me.lblRequestRadiance.AutoSize = True
        Me.lblRequestRadiance.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRequestRadiance.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblRequestRadiance.Location = New System.Drawing.Point(26, 93)
        Me.lblRequestRadiance.Name = "lblRequestRadiance"
        Me.lblRequestRadiance.Size = New System.Drawing.Size(158, 14)
        Me.lblRequestRadiance.TabIndex = 79
        Me.lblRequestRadiance.Text = "Request Radiance (uW/cm2/sr)"
        '
        'PanSetRadiance
        '
        Me.PanSetRadiance.Controls.Add(Me.txtSetRadiance)
        Me.PanSetRadiance.Controls.Add(Me.SpinRadiance)
        Me.PanSetRadiance.Location = New System.Drawing.Point(35, 110)
        Me.PanSetRadiance.Name = "PanSetRadiance"
        Me.PanSetRadiance.Size = New System.Drawing.Size(140, 20)
        Me.PanSetRadiance.TabIndex = 78
        '
        'txtSetRadiance
        '
        Me.txtSetRadiance.Location = New System.Drawing.Point(0, 0)
        Me.txtSetRadiance.Name = "txtSetRadiance"
        Me.txtSetRadiance.Size = New System.Drawing.Size(122, 20)
        Me.txtSetRadiance.TabIndex = 1
        Me.txtSetRadiance.Text = "0000.0000"
        Me.txtSetRadiance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'SpinRadiance
        '
        Me.SpinRadiance.Location = New System.Drawing.Point(122, 0)
        Me.SpinRadiance.Name = "SpinRadiance"
        Me.SpinRadiance.Size = New System.Drawing.Size(18, 20)
        Me.SpinRadiance.TabIndex = 0
        '
        'txtCurrRadiance
        '
        Me.txtCurrRadiance.BackColor = System.Drawing.Color.Gainsboro
        Me.txtCurrRadiance.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCurrRadiance.Location = New System.Drawing.Point(33, 47)
        Me.txtCurrRadiance.Name = "txtCurrRadiance"
        Me.txtCurrRadiance.ReadOnly = True
        Me.txtCurrRadiance.Size = New System.Drawing.Size(140, 20)
        Me.txtCurrRadiance.TabIndex = 77
        Me.txtCurrRadiance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cmdSetRadiance
        '
        Me.cmdSetRadiance.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSetRadiance.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSetRadiance.Location = New System.Drawing.Point(64, 147)
        Me.cmdSetRadiance.Name = "cmdSetRadiance"
        Me.cmdSetRadiance.Size = New System.Drawing.Size(84, 30)
        Me.cmdSetRadiance.TabIndex = 76
        Me.cmdSetRadiance.Text = "Set Radiance"
        Me.cmdSetRadiance.UseVisualStyleBackColor = True
        '
        'lblRadiance
        '
        Me.lblRadiance.AutoSize = True
        Me.lblRadiance.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRadiance.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblRadiance.Location = New System.Drawing.Point(30, 30)
        Me.lblRadiance.Name = "lblRadiance"
        Me.lblRadiance.Size = New System.Drawing.Size(154, 14)
        Me.lblRadiance.TabIndex = 0
        Me.lblRadiance.Text = "Current Radiance (uW/cm2/sr)"
        '
        'gboxVisTgtPos
        '
        Me.gboxVisTgtPos.Controls.Add(Me.cmdSetVisPos)
        Me.gboxVisTgtPos.Controls.Add(Me.cboVisWheel)
        Me.gboxVisTgtPos.Controls.Add(Me.pboxVisTarget)
        Me.gboxVisTgtPos.Controls.Add(Me.txtVisWheel)
        Me.gboxVisTgtPos.Controls.Add(Me.lblVisTgtPos)
        Me.gboxVisTgtPos.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gboxVisTgtPos.ForeColor = System.Drawing.SystemColors.Highlight
        Me.gboxVisTgtPos.Location = New System.Drawing.Point(13, 11)
        Me.gboxVisTgtPos.Name = "gboxVisTgtPos"
        Me.gboxVisTgtPos.Size = New System.Drawing.Size(209, 293)
        Me.gboxVisTgtPos.TabIndex = 0
        Me.gboxVisTgtPos.TabStop = False
        Me.gboxVisTgtPos.Text = "Target Position"
        '
        'cmdSetVisPos
        '
        Me.cmdSetVisPos.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSetVisPos.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSetVisPos.Location = New System.Drawing.Point(64, 244)
        Me.cmdSetVisPos.Name = "cmdSetVisPos"
        Me.cmdSetVisPos.Size = New System.Drawing.Size(84, 30)
        Me.cmdSetVisPos.TabIndex = 75
        Me.cmdSetVisPos.Text = "Set Position"
        Me.cmdSetVisPos.UseVisualStyleBackColor = True
        '
        'cboVisWheel
        '
        Me.cboVisWheel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboVisWheel.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboVisWheel.FormattingEnabled = True
        Me.cboVisWheel.Location = New System.Drawing.Point(6, 205)
        Me.cboVisWheel.Name = "cboVisWheel"
        Me.cboVisWheel.Size = New System.Drawing.Size(197, 22)
        Me.cboVisWheel.TabIndex = 3
        '
        'pboxVisTarget
        '
        Me.pboxVisTarget.Location = New System.Drawing.Point(45, 86)
        Me.pboxVisTarget.Name = "pboxVisTarget"
        Me.pboxVisTarget.Size = New System.Drawing.Size(113, 113)
        Me.pboxVisTarget.TabIndex = 2
        Me.pboxVisTarget.TabStop = False
        '
        'txtVisWheel
        '
        Me.txtVisWheel.BackColor = System.Drawing.Color.Gainsboro
        Me.txtVisWheel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVisWheel.Location = New System.Drawing.Point(6, 48)
        Me.txtVisWheel.Name = "txtVisWheel"
        Me.txtVisWheel.ReadOnly = True
        Me.txtVisWheel.Size = New System.Drawing.Size(197, 20)
        Me.txtVisWheel.TabIndex = 1
        Me.txtVisWheel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblVisTgtPos
        '
        Me.lblVisTgtPos.AutoSize = True
        Me.lblVisTgtPos.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVisTgtPos.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblVisTgtPos.Location = New System.Drawing.Point(26, 30)
        Me.lblVisTgtPos.Name = "lblVisTgtPos"
        Me.lblVisTgtPos.Size = New System.Drawing.Size(149, 14)
        Me.lblVisTgtPos.TabIndex = 0
        Me.lblVisTgtPos.Text = "Current Target Wheel Position"
        '
        'tabpInfrared
        '
        Me.tabpInfrared.BackColor = System.Drawing.Color.Silver
        Me.tabpInfrared.Controls.Add(Me.gboxDiffTemp)
        Me.tabpInfrared.Controls.Add(Me.gboxIrTgt)
        Me.tabpInfrared.Location = New System.Drawing.Point(4, 22)
        Me.tabpInfrared.Name = "tabpInfrared"
        Me.tabpInfrared.Size = New System.Drawing.Size(230, 514)
        Me.tabpInfrared.TabIndex = 1
        Me.tabpInfrared.Text = "Infrared "
        '
        'gboxDiffTemp
        '
        Me.gboxDiffTemp.Controls.Add(Me.lblReqDelta)
        Me.gboxDiffTemp.Controls.Add(Me.panSetDT)
        Me.gboxDiffTemp.Controls.Add(Me.txtBLB_Out)
        Me.gboxDiffTemp.Controls.Add(Me.lblDeltaT2)
        Me.gboxDiffTemp.Controls.Add(Me.cmdSetDT)
        Me.gboxDiffTemp.Controls.Add(Me.txtAMT_Out)
        Me.gboxDiffTemp.Controls.Add(Me.lblDeltaT1)
        Me.gboxDiffTemp.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gboxDiffTemp.ForeColor = System.Drawing.SystemColors.Highlight
        Me.gboxDiffTemp.Location = New System.Drawing.Point(13, 310)
        Me.gboxDiffTemp.Name = "gboxDiffTemp"
        Me.gboxDiffTemp.Size = New System.Drawing.Size(209, 188)
        Me.gboxDiffTemp.TabIndex = 1
        Me.gboxDiffTemp.TabStop = False
        Me.gboxDiffTemp.Text = "Differential Temperature"
        '
        'lblReqDelta
        '
        Me.lblReqDelta.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReqDelta.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblReqDelta.Location = New System.Drawing.Point(32, 109)
        Me.lblReqDelta.Name = "lblReqDelta"
        Me.lblReqDelta.Size = New System.Drawing.Size(148, 14)
        Me.lblReqDelta.TabIndex = 80
        Me.lblReqDelta.Text = "Requested Delta (C)"
        Me.lblReqDelta.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'panSetDT
        '
        Me.panSetDT.Controls.Add(Me.txtSetDT)
        Me.panSetDT.Controls.Add(Me.spinSetDT)
        Me.panSetDT.Location = New System.Drawing.Point(38, 126)
        Me.panSetDT.Name = "panSetDT"
        Me.panSetDT.Size = New System.Drawing.Size(143, 20)
        Me.panSetDT.TabIndex = 79
        '
        'txtSetDT
        '
        Me.txtSetDT.Location = New System.Drawing.Point(0, 0)
        Me.txtSetDT.Name = "txtSetDT"
        Me.txtSetDT.Size = New System.Drawing.Size(125, 20)
        Me.txtSetDT.TabIndex = 0
        Me.txtSetDT.Text = "0.000"
        Me.txtSetDT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtSetDT.WordWrap = False
        '
        'spinSetDT
        '
        Me.spinSetDT.Location = New System.Drawing.Point(125, 0)
        Me.spinSetDT.Name = "spinSetDT"
        Me.spinSetDT.Size = New System.Drawing.Size(18, 20)
        Me.spinSetDT.TabIndex = 1
        '
        'txtBLB_Out
        '
        Me.txtBLB_Out.BackColor = System.Drawing.Color.Gainsboro
        Me.txtBLB_Out.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBLB_Out.Location = New System.Drawing.Point(34, 80)
        Me.txtBLB_Out.Name = "txtBLB_Out"
        Me.txtBLB_Out.ReadOnly = True
        Me.txtBLB_Out.Size = New System.Drawing.Size(143, 20)
        Me.txtBLB_Out.TabIndex = 1
        Me.txtBLB_Out.Text = "0.000"
        Me.txtBLB_Out.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblDeltaT2
        '
        Me.lblDeltaT2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDeltaT2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDeltaT2.Location = New System.Drawing.Point(33, 63)
        Me.lblDeltaT2.Name = "lblDeltaT2"
        Me.lblDeltaT2.Size = New System.Drawing.Size(148, 14)
        Me.lblDeltaT2.TabIndex = 77
        Me.lblDeltaT2.Text = "BlackBody Temperature (C)"
        Me.lblDeltaT2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmdSetDT
        '
        Me.cmdSetDT.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSetDT.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSetDT.Location = New System.Drawing.Point(65, 152)
        Me.cmdSetDT.Name = "cmdSetDT"
        Me.cmdSetDT.Size = New System.Drawing.Size(84, 30)
        Me.cmdSetDT.TabIndex = 2
        Me.cmdSetDT.Text = "Set Delta T"
        Me.cmdSetDT.UseVisualStyleBackColor = True
        '
        'txtAMT_Out
        '
        Me.txtAMT_Out.BackColor = System.Drawing.Color.Gainsboro
        Me.txtAMT_Out.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAMT_Out.Location = New System.Drawing.Point(34, 34)
        Me.txtAMT_Out.Name = "txtAMT_Out"
        Me.txtAMT_Out.ReadOnly = True
        Me.txtAMT_Out.Size = New System.Drawing.Size(143, 20)
        Me.txtAMT_Out.TabIndex = 0
        Me.txtAMT_Out.Text = "0.000"
        Me.txtAMT_Out.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblDeltaT1
        '
        Me.lblDeltaT1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDeltaT1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDeltaT1.Location = New System.Drawing.Point(31, 17)
        Me.lblDeltaT1.Name = "lblDeltaT1"
        Me.lblDeltaT1.Size = New System.Drawing.Size(139, 14)
        Me.lblDeltaT1.TabIndex = 0
        Me.lblDeltaT1.Text = "Ambient Temperature (C)"
        Me.lblDeltaT1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'gboxIrTgt
        '
        Me.gboxIrTgt.Controls.Add(Me.cmdSetIRTarget)
        Me.gboxIrTgt.Controls.Add(Me.cboIR_TargetWheel)
        Me.gboxIrTgt.Controls.Add(Me.pboxIRTarget)
        Me.gboxIrTgt.Controls.Add(Me.txtCurrIRTargPos)
        Me.gboxIrTgt.Controls.Add(Me.lblIRTargPos)
        Me.gboxIrTgt.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gboxIrTgt.ForeColor = System.Drawing.SystemColors.Highlight
        Me.gboxIrTgt.Location = New System.Drawing.Point(13, 11)
        Me.gboxIrTgt.Name = "gboxIrTgt"
        Me.gboxIrTgt.Size = New System.Drawing.Size(209, 293)
        Me.gboxIrTgt.TabIndex = 0
        Me.gboxIrTgt.TabStop = False
        Me.gboxIrTgt.Text = "Target Position"
        '
        'cmdSetIRTarget
        '
        Me.cmdSetIRTarget.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSetIRTarget.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSetIRTarget.Location = New System.Drawing.Point(65, 246)
        Me.cmdSetIRTarget.Name = "cmdSetIRTarget"
        Me.cmdSetIRTarget.Size = New System.Drawing.Size(84, 30)
        Me.cmdSetIRTarget.TabIndex = 2
        Me.cmdSetIRTarget.Text = "Set Position"
        Me.cmdSetIRTarget.UseVisualStyleBackColor = True
        '
        'cboIR_TargetWheel
        '
        Me.cboIR_TargetWheel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboIR_TargetWheel.DropDownWidth = 167
        Me.cboIR_TargetWheel.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboIR_TargetWheel.FormattingEnabled = True
        Me.cboIR_TargetWheel.Location = New System.Drawing.Point(6, 203)
        Me.cboIR_TargetWheel.Name = "cboIR_TargetWheel"
        Me.cboIR_TargetWheel.Size = New System.Drawing.Size(197, 22)
        Me.cboIR_TargetWheel.TabIndex = 1
        '
        'pboxIRTarget
        '
        Me.pboxIRTarget.Location = New System.Drawing.Point(51, 83)
        Me.pboxIRTarget.Name = "pboxIRTarget"
        Me.pboxIRTarget.Size = New System.Drawing.Size(113, 113)
        Me.pboxIRTarget.TabIndex = 2
        Me.pboxIRTarget.TabStop = False
        '
        'txtCurrIRTargPos
        '
        Me.txtCurrIRTargPos.BackColor = System.Drawing.Color.Gainsboro
        Me.txtCurrIRTargPos.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCurrIRTargPos.Location = New System.Drawing.Point(6, 45)
        Me.txtCurrIRTargPos.Name = "txtCurrIRTargPos"
        Me.txtCurrIRTargPos.ReadOnly = True
        Me.txtCurrIRTargPos.Size = New System.Drawing.Size(197, 20)
        Me.txtCurrIRTargPos.TabIndex = 0
        Me.txtCurrIRTargPos.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblIRTargPos
        '
        Me.lblIRTargPos.AutoSize = True
        Me.lblIRTargPos.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIRTargPos.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblIRTargPos.Location = New System.Drawing.Point(32, 27)
        Me.lblIRTargPos.Name = "lblIRTargPos"
        Me.lblIRTargPos.Size = New System.Drawing.Size(149, 14)
        Me.lblIRTargPos.TabIndex = 0
        Me.lblIRTargPos.Text = "Current Target Wheel Position"
        '
        'tabpLaser
        '
        Me.tabpLaser.BackColor = System.Drawing.Color.Silver
        Me.tabpLaser.Controls.Add(Me.gboxSources)
        Me.tabpLaser.Controls.Add(Me.gboxLaser)
        Me.tabpLaser.Controls.Add(Me.chkCameraPower)
        Me.tabpLaser.Location = New System.Drawing.Point(4, 22)
        Me.tabpLaser.Name = "tabpLaser"
        Me.tabpLaser.Size = New System.Drawing.Size(230, 514)
        Me.tabpLaser.TabIndex = 2
        Me.tabpLaser.Text = "Laser"
        '
        'gboxSources
        '
        Me.gboxSources.Controls.Add(Me.panSetPA)
        Me.gboxSources.Controls.Add(Me.panSetElevation)
        Me.gboxSources.Controls.Add(Me.panSetAzimuth)
        Me.gboxSources.Controls.Add(Me.cmdSetLARRS)
        Me.gboxSources.Controls.Add(Me.lblLPolarization)
        Me.gboxSources.Controls.Add(Me.lblElevation)
        Me.gboxSources.Controls.Add(Me.lblAzimuth)
        Me.gboxSources.Controls.Add(Me.txtVal4)
        Me.gboxSources.Controls.Add(Me.lblCValueLPA)
        Me.gboxSources.Controls.Add(Me.txtVal2)
        Me.gboxSources.Controls.Add(Me.lblLCurrentEL)
        Me.gboxSources.Controls.Add(Me.txtVal3)
        Me.gboxSources.Controls.Add(Me.lblLCurrentAZ)
        Me.gboxSources.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gboxSources.ForeColor = System.Drawing.SystemColors.Highlight
        Me.gboxSources.Location = New System.Drawing.Point(8, 333)
        Me.gboxSources.Name = "gboxSources"
        Me.gboxSources.Size = New System.Drawing.Size(195, 178)
        Me.gboxSources.TabIndex = 75
        Me.gboxSources.TabStop = False
        Me.gboxSources.Text = "LARRS Position"
        '
        'panSetPA
        '
        Me.panSetPA.Controls.Add(Me.txtSetPA)
        Me.panSetPA.Controls.Add(Me.SpinSetPA)
        Me.panSetPA.Location = New System.Drawing.Point(89, 113)
        Me.panSetPA.Name = "panSetPA"
        Me.panSetPA.Size = New System.Drawing.Size(95, 20)
        Me.panSetPA.TabIndex = 50
        '
        'txtSetPA
        '
        Me.txtSetPA.Location = New System.Drawing.Point(0, 0)
        Me.txtSetPA.Name = "txtSetPA"
        Me.txtSetPA.Size = New System.Drawing.Size(77, 20)
        Me.txtSetPA.TabIndex = 1
        Me.txtSetPA.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'SpinSetPA
        '
        Me.SpinSetPA.Location = New System.Drawing.Point(77, 0)
        Me.SpinSetPA.Name = "SpinSetPA"
        Me.SpinSetPA.Size = New System.Drawing.Size(18, 20)
        Me.SpinSetPA.TabIndex = 0
        '
        'panSetElevation
        '
        Me.panSetElevation.Controls.Add(Me.txtSetElevation)
        Me.panSetElevation.Controls.Add(Me.SpinSetElevation)
        Me.panSetElevation.Location = New System.Drawing.Point(89, 72)
        Me.panSetElevation.Name = "panSetElevation"
        Me.panSetElevation.Size = New System.Drawing.Size(95, 20)
        Me.panSetElevation.TabIndex = 49
        '
        'txtSetElevation
        '
        Me.txtSetElevation.Location = New System.Drawing.Point(0, 0)
        Me.txtSetElevation.Name = "txtSetElevation"
        Me.txtSetElevation.Size = New System.Drawing.Size(77, 20)
        Me.txtSetElevation.TabIndex = 1
        Me.txtSetElevation.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'SpinSetElevation
        '
        Me.SpinSetElevation.Location = New System.Drawing.Point(77, 0)
        Me.SpinSetElevation.Name = "SpinSetElevation"
        Me.SpinSetElevation.Size = New System.Drawing.Size(18, 20)
        Me.SpinSetElevation.TabIndex = 0
        '
        'panSetAzimuth
        '
        Me.panSetAzimuth.Controls.Add(Me.txtSetAzimuth)
        Me.panSetAzimuth.Controls.Add(Me.SpinSetAzimuth)
        Me.panSetAzimuth.Location = New System.Drawing.Point(89, 33)
        Me.panSetAzimuth.Name = "panSetAzimuth"
        Me.panSetAzimuth.Size = New System.Drawing.Size(95, 20)
        Me.panSetAzimuth.TabIndex = 48
        '
        'txtSetAzimuth
        '
        Me.txtSetAzimuth.Location = New System.Drawing.Point(0, 0)
        Me.txtSetAzimuth.Name = "txtSetAzimuth"
        Me.txtSetAzimuth.Size = New System.Drawing.Size(77, 20)
        Me.txtSetAzimuth.TabIndex = 1
        Me.txtSetAzimuth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'SpinSetAzimuth
        '
        Me.SpinSetAzimuth.Location = New System.Drawing.Point(77, 0)
        Me.SpinSetAzimuth.Name = "SpinSetAzimuth"
        Me.SpinSetAzimuth.Size = New System.Drawing.Size(18, 20)
        Me.SpinSetAzimuth.TabIndex = 0
        '
        'cmdSetLARRS
        '
        Me.cmdSetLARRS.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSetLARRS.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSetLARRS.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSetLARRS.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSetLARRS.Location = New System.Drawing.Point(115, 140)
        Me.cmdSetLARRS.Name = "cmdSetLARRS"
        Me.cmdSetLARRS.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSetLARRS.Size = New System.Drawing.Size(69, 22)
        Me.cmdSetLARRS.TabIndex = 47
        Me.cmdSetLARRS.Text = "Set LaRRS"
        Me.cmdSetLARRS.UseVisualStyleBackColor = False
        '
        'lblLPolarization
        '
        Me.lblLPolarization.AutoSize = True
        Me.lblLPolarization.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLPolarization.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLPolarization.Location = New System.Drawing.Point(97, 97)
        Me.lblLPolarization.Name = "lblLPolarization"
        Me.lblLPolarization.Size = New System.Drawing.Size(92, 14)
        Me.lblLPolarization.TabIndex = 25
        Me.lblLPolarization.Text = "Polarization Angle"
        Me.lblLPolarization.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblElevation
        '
        Me.lblElevation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblElevation.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblElevation.Location = New System.Drawing.Point(97, 57)
        Me.lblElevation.Name = "lblElevation"
        Me.lblElevation.Size = New System.Drawing.Size(92, 14)
        Me.lblElevation.TabIndex = 22
        Me.lblElevation.Text = "Elevation"
        Me.lblElevation.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblAzimuth
        '
        Me.lblAzimuth.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAzimuth.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAzimuth.Location = New System.Drawing.Point(97, 16)
        Me.lblAzimuth.Name = "lblAzimuth"
        Me.lblAzimuth.Size = New System.Drawing.Size(92, 14)
        Me.lblAzimuth.TabIndex = 19
        Me.lblAzimuth.Text = "Azimuth"
        Me.lblAzimuth.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtVal4
        '
        Me.txtVal4.BackColor = System.Drawing.Color.Gainsboro
        Me.txtVal4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVal4.Location = New System.Drawing.Point(17, 113)
        Me.txtVal4.Name = "txtVal4"
        Me.txtVal4.ReadOnly = True
        Me.txtVal4.Size = New System.Drawing.Size(49, 20)
        Me.txtVal4.TabIndex = 18
        Me.txtVal4.Text = "0"
        Me.txtVal4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblCValueLPA
        '
        Me.lblCValueLPA.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCValueLPA.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCValueLPA.Location = New System.Drawing.Point(6, 96)
        Me.lblCValueLPA.Name = "lblCValueLPA"
        Me.lblCValueLPA.Size = New System.Drawing.Size(68, 14)
        Me.lblCValueLPA.TabIndex = 17
        Me.lblCValueLPA.Text = "Current POL"
        Me.lblCValueLPA.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtVal2
        '
        Me.txtVal2.BackColor = System.Drawing.Color.Gainsboro
        Me.txtVal2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVal2.Location = New System.Drawing.Point(17, 73)
        Me.txtVal2.Name = "txtVal2"
        Me.txtVal2.ReadOnly = True
        Me.txtVal2.Size = New System.Drawing.Size(49, 20)
        Me.txtVal2.TabIndex = 16
        Me.txtVal2.Text = "0"
        Me.txtVal2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblLCurrentEL
        '
        Me.lblLCurrentEL.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLCurrentEL.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLCurrentEL.Location = New System.Drawing.Point(6, 57)
        Me.lblLCurrentEL.Name = "lblLCurrentEL"
        Me.lblLCurrentEL.Size = New System.Drawing.Size(66, 14)
        Me.lblLCurrentEL.TabIndex = 15
        Me.lblLCurrentEL.Text = "Current EL"
        Me.lblLCurrentEL.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtVal3
        '
        Me.txtVal3.BackColor = System.Drawing.Color.Gainsboro
        Me.txtVal3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVal3.Location = New System.Drawing.Point(17, 33)
        Me.txtVal3.Name = "txtVal3"
        Me.txtVal3.ReadOnly = True
        Me.txtVal3.Size = New System.Drawing.Size(49, 20)
        Me.txtVal3.TabIndex = 14
        Me.txtVal3.Text = "0"
        Me.txtVal3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblLCurrentAZ
        '
        Me.lblLCurrentAZ.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLCurrentAZ.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLCurrentAZ.Location = New System.Drawing.Point(6, 16)
        Me.lblLCurrentAZ.Name = "lblLCurrentAZ"
        Me.lblLCurrentAZ.Size = New System.Drawing.Size(66, 14)
        Me.lblLCurrentAZ.TabIndex = 13
        Me.lblLCurrentAZ.Text = "Current AZ"
        Me.lblLCurrentAZ.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'gboxLaser
        '
        Me.gboxLaser.Controls.Add(Me.panSetPP)
        Me.gboxLaser.Controls.Add(Me.panSetAmplitude)
        Me.gboxLaser.Controls.Add(Me.cmdDiodePower)
        Me.gboxLaser.Controls.Add(Me.lblPower)
        Me.gboxLaser.Controls.Add(Me.pboxBeam)
        Me.gboxLaser.Controls.Add(Me.cmdLaserSet)
        Me.gboxLaser.Controls.Add(Me.lblPulsePeriod)
        Me.gboxLaser.Controls.Add(Me.lblLaserTrigger)
        Me.gboxLaser.Controls.Add(Me.lblAmplitudeLaser)
        Me.gboxLaser.Controls.Add(Me.cboLaserTrigger)
        Me.gboxLaser.Controls.Add(Me.lblWaveLength)
        Me.gboxLaser.Controls.Add(Me.cboDiode)
        Me.gboxLaser.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gboxLaser.ForeColor = System.Drawing.SystemColors.Highlight
        Me.gboxLaser.Location = New System.Drawing.Point(8, 4)
        Me.gboxLaser.Name = "gboxLaser"
        Me.gboxLaser.Size = New System.Drawing.Size(194, 302)
        Me.gboxLaser.TabIndex = 74
        Me.gboxLaser.TabStop = False
        Me.gboxLaser.Text = "Laser Settings"
        '
        'panSetPP
        '
        Me.panSetPP.Controls.Add(Me.txtSetPP)
        Me.panSetPP.Controls.Add(Me.SpinSetPP)
        Me.panSetPP.Location = New System.Drawing.Point(89, 189)
        Me.panSetPP.Name = "panSetPP"
        Me.panSetPP.Size = New System.Drawing.Size(88, 20)
        Me.panSetPP.TabIndex = 51
        '
        'txtSetPP
        '
        Me.txtSetPP.Location = New System.Drawing.Point(0, 0)
        Me.txtSetPP.Name = "txtSetPP"
        Me.txtSetPP.Size = New System.Drawing.Size(70, 20)
        Me.txtSetPP.TabIndex = 1
        Me.txtSetPP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'SpinSetPP
        '
        Me.SpinSetPP.Location = New System.Drawing.Point(70, 0)
        Me.SpinSetPP.Name = "SpinSetPP"
        Me.SpinSetPP.Size = New System.Drawing.Size(18, 20)
        Me.SpinSetPP.TabIndex = 0
        '
        'panSetAmplitude
        '
        Me.panSetAmplitude.Controls.Add(Me.txtSetAmplitude)
        Me.panSetAmplitude.Controls.Add(Me.SpinSetSetAmplitude)
        Me.panSetAmplitude.Location = New System.Drawing.Point(87, 90)
        Me.panSetAmplitude.Name = "panSetAmplitude"
        Me.panSetAmplitude.Size = New System.Drawing.Size(90, 20)
        Me.panSetAmplitude.TabIndex = 50
        '
        'txtSetAmplitude
        '
        Me.txtSetAmplitude.Location = New System.Drawing.Point(0, 0)
        Me.txtSetAmplitude.Name = "txtSetAmplitude"
        Me.txtSetAmplitude.Size = New System.Drawing.Size(72, 20)
        Me.txtSetAmplitude.TabIndex = 1
        Me.txtSetAmplitude.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtSetAmplitude.WordWrap = False
        '
        'SpinSetSetAmplitude
        '
        Me.SpinSetSetAmplitude.Location = New System.Drawing.Point(72, 0)
        Me.SpinSetSetAmplitude.Name = "SpinSetSetAmplitude"
        Me.SpinSetSetAmplitude.Size = New System.Drawing.Size(18, 20)
        Me.SpinSetSetAmplitude.TabIndex = 0
        '
        'cmdDiodePower
        '
        Me.cmdDiodePower.BackColor = System.Drawing.SystemColors.Control
        Me.cmdDiodePower.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdDiodePower.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDiodePower.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDiodePower.Location = New System.Drawing.Point(120, 241)
        Me.cmdDiodePower.Name = "cmdDiodePower"
        Me.cmdDiodePower.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDiodePower.Size = New System.Drawing.Size(68, 45)
        Me.cmdDiodePower.TabIndex = 49
        Me.cmdDiodePower.Text = "Turn Diode Power On"
        Me.cmdDiodePower.UseVisualStyleBackColor = False
        '
        'lblPower
        '
        Me.lblPower.AutoSize = True
        Me.lblPower.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPower.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPower.Location = New System.Drawing.Point(6, 276)
        Me.lblPower.Name = "lblPower"
        Me.lblPower.Size = New System.Drawing.Size(69, 14)
        Me.lblPower.TabIndex = 48
        Me.lblPower.Text = "Diode Power"
        Me.lblPower.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pboxBeam
        '
        Me.pboxBeam.Image = Global.VidTool.My.Resources.Resources.DiodePower
        Me.pboxBeam.InitialImage = CType(resources.GetObject("pboxBeam.InitialImage"), System.Drawing.Image)
        Me.pboxBeam.Location = New System.Drawing.Point(26, 242)
        Me.pboxBeam.Name = "pboxBeam"
        Me.pboxBeam.Size = New System.Drawing.Size(37, 31)
        Me.pboxBeam.TabIndex = 47
        Me.pboxBeam.TabStop = False
        Me.pboxBeam.Visible = False
        '
        'cmdLaserSet
        '
        Me.cmdLaserSet.BackColor = System.Drawing.SystemColors.Control
        Me.cmdLaserSet.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdLaserSet.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdLaserSet.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdLaserSet.Location = New System.Drawing.Point(128, 214)
        Me.cmdLaserSet.Name = "cmdLaserSet"
        Me.cmdLaserSet.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdLaserSet.Size = New System.Drawing.Size(49, 22)
        Me.cmdLaserSet.TabIndex = 46
        Me.cmdLaserSet.Text = "Set"
        Me.cmdLaserSet.UseVisualStyleBackColor = False
        '
        'lblPulsePeriod
        '
        Me.lblPulsePeriod.AutoSize = True
        Me.lblPulsePeriod.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPulsePeriod.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPulsePeriod.Location = New System.Drawing.Point(91, 170)
        Me.lblPulsePeriod.Name = "lblPulsePeriod"
        Me.lblPulsePeriod.Size = New System.Drawing.Size(88, 14)
        Me.lblPulsePeriod.TabIndex = 11
        Me.lblPulsePeriod.Text = "Pulse Period(ms)"
        Me.lblPulsePeriod.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblLaserTrigger
        '
        Me.lblLaserTrigger.AutoSize = True
        Me.lblLaserTrigger.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLaserTrigger.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLaserTrigger.Location = New System.Drawing.Point(97, 113)
        Me.lblLaserTrigger.Name = "lblLaserTrigger"
        Me.lblLaserTrigger.Size = New System.Drawing.Size(79, 14)
        Me.lblLaserTrigger.TabIndex = 8
        Me.lblLaserTrigger.Text = "Trigger Source"
        Me.lblLaserTrigger.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblAmplitudeLaser
        '
        Me.lblAmplitudeLaser.AutoSize = True
        Me.lblAmplitudeLaser.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAmplitudeLaser.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAmplitudeLaser.Location = New System.Drawing.Point(59, 73)
        Me.lblAmplitudeLaser.Name = "lblAmplitudeLaser"
        Me.lblAmplitudeLaser.Size = New System.Drawing.Size(117, 14)
        Me.lblAmplitudeLaser.TabIndex = 6
        Me.lblAmplitudeLaser.Text = "Amplitude (nW/cm2/sr)"
        Me.lblAmplitudeLaser.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboLaserTrigger
        '
        Me.cboLaserTrigger.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboLaserTrigger.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboLaserTrigger.FormattingEnabled = True
        Me.cboLaserTrigger.Location = New System.Drawing.Point(66, 134)
        Me.cboLaserTrigger.Name = "cboLaserTrigger"
        Me.cboLaserTrigger.Size = New System.Drawing.Size(111, 22)
        Me.cboLaserTrigger.TabIndex = 7
        '
        'lblWaveLength
        '
        Me.lblWaveLength.AutoSize = True
        Me.lblWaveLength.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWaveLength.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblWaveLength.Location = New System.Drawing.Point(87, 14)
        Me.lblWaveLength.Name = "lblWaveLength"
        Me.lblWaveLength.Size = New System.Drawing.Size(89, 14)
        Me.lblWaveLength.TabIndex = 4
        Me.lblWaveLength.Text = "Wavelength (nm)"
        Me.lblWaveLength.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboDiode
        '
        Me.cboDiode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDiode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDiode.FormattingEnabled = True
        Me.cboDiode.Location = New System.Drawing.Point(111, 33)
        Me.cboDiode.Name = "cboDiode"
        Me.cboDiode.Size = New System.Drawing.Size(66, 22)
        Me.cboDiode.TabIndex = 5
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ImageList1.Images.SetKeyName(0, "")
        Me.ImageList1.Images.SetKeyName(1, "")
        '
        'TargetImageList
        '
        Me.TargetImageList.ImageStream = CType(resources.GetObject("TargetImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.TargetImageList.TransparentColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TargetImageList.Images.SetKeyName(0, "")
        Me.TargetImageList.Images.SetKeyName(1, "")
        Me.TargetImageList.Images.SetKeyName(2, "")
        Me.TargetImageList.Images.SetKeyName(3, "")
        Me.TargetImageList.Images.SetKeyName(4, "")
        Me.TargetImageList.Images.SetKeyName(5, "")
        Me.TargetImageList.Images.SetKeyName(6, "")
        Me.TargetImageList.Images.SetKeyName(7, "")
        Me.TargetImageList.Images.SetKeyName(8, "")
        '
        'lblInstructions
        '
        Me.lblInstructions.BackColor = System.Drawing.Color.Transparent
        Me.lblInstructions.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblInstructions.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInstructions.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblInstructions.Location = New System.Drawing.Point(88, 503)
        Me.lblInstructions.Name = "lblInstructions"
        Me.lblInstructions.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblInstructions.Size = New System.Drawing.Size(204, 13)
        Me.lblInstructions.TabIndex = 21
        Me.lblInstructions.Text = "*** OPERATOR INSTRUCTIONS ***"
        Me.lblInstructions.Visible = False
        '
        'lblAsset
        '
        Me.lblAsset.BackColor = System.Drawing.Color.Yellow
        Me.lblAsset.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAsset.Location = New System.Drawing.Point(759, 33)
        Me.lblAsset.Name = "lblAsset"
        Me.lblAsset.Size = New System.Drawing.Size(190, 40)
        Me.lblAsset.TabIndex = 70
        Me.lblAsset.Text = "Apply Power to Expose EO Asset Controls"
        Me.lblAsset.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblAsset.Visible = False
        '
        'cmdAlign
        '
        Me.cmdAlign.Location = New System.Drawing.Point(746, 634)
        Me.cmdAlign.Name = "cmdAlign"
        Me.cmdAlign.Size = New System.Drawing.Size(75, 23)
        Me.cmdAlign.TabIndex = 71
        Me.cmdAlign.Text = "Align On"
        Me.cmdAlign.UseVisualStyleBackColor = True
        '
        'cmdCapture
        '
        Me.cmdCapture.Location = New System.Drawing.Point(746, 672)
        Me.cmdCapture.Name = "cmdCapture"
        Me.cmdCapture.Size = New System.Drawing.Size(75, 23)
        Me.cmdCapture.TabIndex = 72
        Me.cmdCapture.Text = "&Capture"
        Me.cmdCapture.UseVisualStyleBackColor = True
        '
        'cmdAbout
        '
        Me.cmdAbout.Location = New System.Drawing.Point(827, 634)
        Me.cmdAbout.Name = "cmdAbout"
        Me.cmdAbout.Size = New System.Drawing.Size(75, 23)
        Me.cmdAbout.TabIndex = 73
        Me.cmdAbout.Text = "&Refresh"
        Me.cmdAbout.UseVisualStyleBackColor = True
        Me.cmdAbout.Visible = False
        '
        'cmdEOPower
        '
        Me.cmdEOPower.Location = New System.Drawing.Point(658, 674)
        Me.cmdEOPower.Name = "cmdEOPower"
        Me.cmdEOPower.Size = New System.Drawing.Size(75, 23)
        Me.cmdEOPower.TabIndex = 74
        Me.cmdEOPower.Text = "Power On"
        Me.cmdEOPower.UseVisualStyleBackColor = True
        '
        'cmdClose
        '
        Me.cmdClose.Location = New System.Drawing.Point(827, 671)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(75, 23)
        Me.cmdClose.TabIndex = 75
        Me.cmdClose.Text = "C&lose"
        Me.cmdClose.UseVisualStyleBackColor = True
        '
        'Timer1
        '
        Me.Timer1.Interval = 500
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsStatusLabel})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 700)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(1193, 22)
        Me.StatusStrip1.SizingGrip = False
        Me.StatusStrip1.TabIndex = 79
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'tsStatusLabel
        '
        Me.tsStatusLabel.Name = "tsStatusLabel"
        Me.tsStatusLabel.Size = New System.Drawing.Size(1178, 17)
        Me.tsStatusLabel.Spring = True
        Me.tsStatusLabel.Text = "tsStatusLabel"
        Me.tsStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1193, 722)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.cmdClose)
        Me.Controls.Add(Me.cmdEOPower)
        Me.Controls.Add(Me.cmdAbout)
        Me.Controls.Add(Me.cmdCapture)
        Me.Controls.Add(Me.cmdAlign)
        Me.Controls.Add(Me.lblAsset)
        Me.Controls.Add(Me.gboxSrcStage)
        Me.Controls.Add(Me.gboxSensStage)
        Me.Controls.Add(Me.pnlDispHolder)
        Me.Controls.Add(Me.chkLiveDisplay)
        Me.Controls.Add(Me.gboxEOPower)
        Me.Controls.Add(Me.txtInstructions)
        Me.Controls.Add(Me.tabAsset)
        Me.Controls.Add(Me.lblInstructions)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(10, 10)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMain"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Video Display"
        Me.gboxSrcStage.ResumeLayout(False)
        Me.gboxSensStage.ResumeLayout(False)
        Me.pnlDispHolder.ResumeLayout(False)
        CType(Me.pboxDisplay, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gboxEOPower.ResumeLayout(False)
        CType(Me.pboxLight0, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pboxLight1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pboxLight2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabAsset.ResumeLayout(False)
        Me.tabpVisible.ResumeLayout(False)
        Me.gboxRadiance.ResumeLayout(False)
        Me.gboxRadiance.PerformLayout()
        Me.PanSetRadiance.ResumeLayout(False)
        Me.PanSetRadiance.PerformLayout()
        CType(Me.SpinRadiance, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gboxVisTgtPos.ResumeLayout(False)
        Me.gboxVisTgtPos.PerformLayout()
        CType(Me.pboxVisTarget, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabpInfrared.ResumeLayout(False)
        Me.gboxDiffTemp.ResumeLayout(False)
        Me.gboxDiffTemp.PerformLayout()
        Me.panSetDT.ResumeLayout(False)
        Me.panSetDT.PerformLayout()
        CType(Me.spinSetDT, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gboxIrTgt.ResumeLayout(False)
        Me.gboxIrTgt.PerformLayout()
        CType(Me.pboxIRTarget, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabpLaser.ResumeLayout(False)
        Me.gboxSources.ResumeLayout(False)
        Me.gboxSources.PerformLayout()
        Me.panSetPA.ResumeLayout(False)
        Me.panSetPA.PerformLayout()
        CType(Me.SpinSetPA, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panSetElevation.ResumeLayout(False)
        Me.panSetElevation.PerformLayout()
        CType(Me.SpinSetElevation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panSetAzimuth.ResumeLayout(False)
        Me.panSetAzimuth.PerformLayout()
        CType(Me.SpinSetAzimuth, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gboxLaser.ResumeLayout(False)
        Me.gboxLaser.PerformLayout()
        Me.panSetPP.ResumeLayout(False)
        Me.panSetPP.PerformLayout()
        CType(Me.SpinSetPP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panSetAmplitude.ResumeLayout(False)
        Me.panSetAmplitude.PerformLayout()
        CType(Me.SpinSetSetAmplitude, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pboxBeam, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

End Sub
    Friend WithEvents lblAsset As System.Windows.Forms.Label
    Friend WithEvents cmdAlign As System.Windows.Forms.Button
    Friend WithEvents cmdCapture As System.Windows.Forms.Button
    Friend WithEvents cmdAbout As System.Windows.Forms.Button
    Friend WithEvents cmdEOPower As System.Windows.Forms.Button
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    Friend WithEvents gboxVisTgtPos As System.Windows.Forms.GroupBox
    Friend WithEvents lblVisTgtPos As System.Windows.Forms.Label
    Friend WithEvents txtVisWheel As System.Windows.Forms.TextBox
    Friend WithEvents pboxVisTarget As System.Windows.Forms.PictureBox
    Friend WithEvents cboVisWheel As System.Windows.Forms.ComboBox
    Friend WithEvents cmdSetVisPos As System.Windows.Forms.Button
    Friend WithEvents gboxIrTgt As System.Windows.Forms.GroupBox
    Friend WithEvents cmdSetIRTarget As System.Windows.Forms.Button
    Friend WithEvents cboIR_TargetWheel As System.Windows.Forms.ComboBox
    Friend WithEvents pboxIRTarget As System.Windows.Forms.PictureBox
    Friend WithEvents txtCurrIRTargPos As System.Windows.Forms.TextBox
    Friend WithEvents lblIRTargPos As System.Windows.Forms.Label
    Friend WithEvents gboxRadiance As System.Windows.Forms.GroupBox
    Friend WithEvents cmdSetRadiance As System.Windows.Forms.Button
    Friend WithEvents lblRadiance As System.Windows.Forms.Label
    Friend WithEvents gboxDiffTemp As System.Windows.Forms.GroupBox
    Friend WithEvents cmdSetDT As System.Windows.Forms.Button
    Friend WithEvents txtAMT_Out As System.Windows.Forms.TextBox
    Friend WithEvents lblDeltaT1 As System.Windows.Forms.Label
    Friend WithEvents txtBLB_Out As System.Windows.Forms.TextBox
    Friend WithEvents lblDeltaT2 As System.Windows.Forms.Label
    Friend WithEvents gboxLaser As System.Windows.Forms.GroupBox
    Friend WithEvents lblWaveLength As System.Windows.Forms.Label
    Friend WithEvents cboDiode As System.Windows.Forms.ComboBox
    Friend WithEvents lblAmplitudeLaser As System.Windows.Forms.Label
    Friend WithEvents cboLaserTrigger As System.Windows.Forms.ComboBox
    Friend WithEvents lblLaserTrigger As System.Windows.Forms.Label
    Friend WithEvents lblPulsePeriod As System.Windows.Forms.Label
    Public WithEvents cmdLaserSet As System.Windows.Forms.Button
    Friend WithEvents pboxBeam As System.Windows.Forms.PictureBox
    Friend WithEvents lblPower As System.Windows.Forms.Label
    Public WithEvents cmdDiodePower As System.Windows.Forms.Button
    Friend WithEvents gboxSources As System.Windows.Forms.GroupBox
    Friend WithEvents txtVal3 As System.Windows.Forms.TextBox
    Friend WithEvents lblLCurrentAZ As System.Windows.Forms.Label
    Friend WithEvents txtVal2 As System.Windows.Forms.TextBox
    Friend WithEvents lblLCurrentEL As System.Windows.Forms.Label
    Friend WithEvents txtVal4 As System.Windows.Forms.TextBox
    Friend WithEvents lblCValueLPA As System.Windows.Forms.Label
    Friend WithEvents lblAzimuth As System.Windows.Forms.Label
    Friend WithEvents lblElevation As System.Windows.Forms.Label
    Friend WithEvents lblLPolarization As System.Windows.Forms.Label
    Public WithEvents cmdSetLARRS As System.Windows.Forms.Button
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents tsStatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents txtCurrRadiance As System.Windows.Forms.TextBox
    Friend WithEvents PanSetRadiance As System.Windows.Forms.Panel
    Friend WithEvents txtSetRadiance As System.Windows.Forms.TextBox
    Friend WithEvents SpinRadiance As NationalInstruments.UI.WindowsForms.NumericEdit
    Friend WithEvents panSetDT As System.Windows.Forms.Panel
    Friend WithEvents spinSetDT As NationalInstruments.UI.WindowsForms.NumericEdit
    Friend WithEvents panSetAmplitude As System.Windows.Forms.Panel
    Friend WithEvents txtSetAmplitude As System.Windows.Forms.TextBox
    Friend WithEvents SpinSetSetAmplitude As NationalInstruments.UI.WindowsForms.NumericEdit
    Friend WithEvents panSetPP As System.Windows.Forms.Panel
    Friend WithEvents txtSetPP As System.Windows.Forms.TextBox
    Friend WithEvents SpinSetPP As NationalInstruments.UI.WindowsForms.NumericEdit
    Friend WithEvents panSetAzimuth As System.Windows.Forms.Panel
    Friend WithEvents txtSetAzimuth As System.Windows.Forms.TextBox
    Friend WithEvents SpinSetAzimuth As NationalInstruments.UI.WindowsForms.NumericEdit
    Friend WithEvents panSetElevation As System.Windows.Forms.Panel
    Friend WithEvents txtSetElevation As System.Windows.Forms.TextBox
    Friend WithEvents SpinSetElevation As NationalInstruments.UI.WindowsForms.NumericEdit
    Friend WithEvents panSetPA As System.Windows.Forms.Panel
    Friend WithEvents txtSetPA As System.Windows.Forms.TextBox
    Friend WithEvents SpinSetPA As NationalInstruments.UI.WindowsForms.NumericEdit
    Friend WithEvents txtSetDT As System.Windows.Forms.TextBox
    Friend WithEvents lblReqDelta As System.Windows.Forms.Label
    Friend WithEvents lblRequestRadiance As System.Windows.Forms.Label
#End Region
End Class