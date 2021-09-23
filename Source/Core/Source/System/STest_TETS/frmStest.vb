Option Explicit On

Imports System
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Windows.Forms.Screen


Public Class frmSTest
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Private Shared _instrumentLabel As Object
    Friend WithEvents sscTest_7 As System.Windows.Forms.Button

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    Shared Property InstrumentLabel(iInst As Short) As Object
        Get
            Return _instrumentLabel
        End Get
        Set(value As Object)
            _instrumentLabel = value
        End Set
    End Property

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
    Friend WithEvents FrPPUStart As System.Windows.Forms.GroupBox
    Friend WithEvents cmdPPUstartOK As System.Windows.Forms.Button
    Friend WithEvents txtPPUstart As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents DetailsText As System.Windows.Forms.RichTextBox
    Friend WithEvents proProgress As System.Windows.Forms.ProgressBar
    Friend WithEvents cmdDetails As System.Windows.Forms.Button
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    Friend WithEvents Frame3 As System.Windows.Forms.GroupBox
    Friend WithEvents cmdAbort As System.Windows.Forms.Button
    Friend WithEvents cmdPause As System.Windows.Forms.Button
    Friend WithEvents cmdLOS As System.Windows.Forms.Button
    Friend WithEvents cmbSelectStep As System.Windows.Forms.ComboBox
    Friend WithEvents cmbSelectTest As System.Windows.Forms.ComboBox
    Friend WithEvents cmdCOF As System.Windows.Forms.Button
    Friend WithEvents cmdSOF As System.Windows.Forms.Button
    Friend WithEvents cmdLOE As System.Windows.Forms.Button
    Friend WithEvents cmdSOE As System.Windows.Forms.Button
    Friend WithEvents lblStepFailCount As System.Windows.Forms.Label
    Friend WithEvents lblFailCount As System.Windows.Forms.Label
    Friend WithEvents lblStepPassCount As System.Windows.Forms.Label
    Friend WithEvents lblPassCount As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents LblSelectStep As System.Windows.Forms.Label
    Friend WithEvents LblSelectTest As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblRunMode As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblTestMode As System.Windows.Forms.Label
    Friend WithEvents lblFaultMode As System.Windows.Forms.Label
    Friend WithEvents cmdHelp As System.Windows.Forms.Button
    Friend WithEvents Timer2 As System.Windows.Forms.Timer
    Friend WithEvents picNoTest As System.Windows.Forms.PictureBox
    Friend WithEvents picTestPassed As System.Windows.Forms.PictureBox
    Friend WithEvents picTestFailed As System.Windows.Forms.PictureBox
    Friend WithEvents picTestUnknown As System.Windows.Forms.PictureBox
    Friend WithEvents timTimer As System.Windows.Forms.Timer
    Friend WithEvents imlButtons As System.Windows.Forms.ImageList
    Friend WithEvents sbrUserInformation As System.Windows.Forms.StatusBar
    Friend WithEvents sbrUserInformation_Panel1 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents PassFrame_0 As System.Windows.Forms.PictureBox
    Friend WithEvents PassFrame_1 As System.Windows.Forms.PictureBox
    Friend WithEvents PassFrame_2 As System.Windows.Forms.PictureBox
    Friend WithEvents PassFrame_3 As System.Windows.Forms.PictureBox
    Friend WithEvents PassFrame_4 As System.Windows.Forms.PictureBox
    Friend WithEvents PassFrame_5 As System.Windows.Forms.PictureBox
    Friend WithEvents PassFrame_6 As System.Windows.Forms.PictureBox
    Friend WithEvents PassFrame_7 As System.Windows.Forms.PictureBox
    Friend WithEvents PassFrame_8 As System.Windows.Forms.PictureBox
    Friend WithEvents PassFrame_9 As System.Windows.Forms.PictureBox
    Friend WithEvents PassFrame_10 As System.Windows.Forms.PictureBox
    Friend WithEvents PassFrame_11 As System.Windows.Forms.PictureBox
    Friend WithEvents PassFrame_12 As System.Windows.Forms.PictureBox
    Friend WithEvents PassFrame_13 As System.Windows.Forms.PictureBox
    Friend WithEvents PassFrame_14 As System.Windows.Forms.PictureBox
    Friend WithEvents PassFrame_15 As System.Windows.Forms.PictureBox
    Friend WithEvents PassFrame_16 As System.Windows.Forms.PictureBox
    Friend WithEvents PassFrame_17 As System.Windows.Forms.PictureBox
    Friend WithEvents PassFrame_18 As System.Windows.Forms.PictureBox
    Friend WithEvents PassFrame_19 As System.Windows.Forms.PictureBox
    Friend WithEvents PassFrame_20 As System.Windows.Forms.PictureBox
    Friend WithEvents PassFrame_21 As System.Windows.Forms.PictureBox
    Friend WithEvents PassFrame_22 As System.Windows.Forms.PictureBox
    Friend WithEvents PassFrame_23 As System.Windows.Forms.PictureBox
    Friend WithEvents PassFrame_24 As System.Windows.Forms.PictureBox
    Friend WithEvents PassFrame_25 As System.Windows.Forms.PictureBox
    Friend WithEvents sscTest_0 As System.Windows.Forms.Button
    Friend WithEvents sscTest_1 As System.Windows.Forms.Button
    Friend WithEvents sscTest_2 As System.Windows.Forms.Button
    Friend WithEvents sscTest_3 As System.Windows.Forms.Button
    Friend WithEvents sscTest_4 As System.Windows.Forms.Button
    Friend WithEvents sscTest_5 As System.Windows.Forms.Button
    Friend WithEvents sscTest_6 As System.Windows.Forms.Button
    Friend WithEvents sscTest_8 As System.Windows.Forms.Button
    Friend WithEvents sscTest_9 As System.Windows.Forms.Button
    Friend WithEvents sscTest_10 As System.Windows.Forms.Button
    Friend WithEvents sscTest_11 As System.Windows.Forms.Button
    Friend WithEvents sscTest_12 As System.Windows.Forms.Button
    Friend WithEvents sscTest_13 As System.Windows.Forms.Button
    Friend WithEvents sscTest_14 As System.Windows.Forms.Button
    Friend WithEvents sscTest_15 As System.Windows.Forms.Button
    Friend WithEvents sscTest_16 As System.Windows.Forms.Button
    Friend WithEvents sscTest_17 As System.Windows.Forms.Button
    Friend WithEvents sscTest_18 As System.Windows.Forms.Button
    Friend WithEvents sscTest_19 As System.Windows.Forms.Button
    Friend WithEvents sscTest_20 As System.Windows.Forms.Button
    Friend WithEvents sscTest_21 As System.Windows.Forms.Button
    Friend WithEvents sscTest_22 As System.Windows.Forms.Button
    Friend WithEvents sscTest_23 As System.Windows.Forms.Button
    Friend WithEvents sscTest_24 As System.Windows.Forms.Button
    Friend WithEvents sscTest_25 As System.Windows.Forms.Button
    Friend WithEvents InstrumentLabel_0 As System.Windows.Forms.Label
    Friend WithEvents InstrumentLabel_1 As System.Windows.Forms.Label
    Friend WithEvents InstrumentLabel_2 As System.Windows.Forms.Label
    Friend WithEvents InstrumentLabel_3 As System.Windows.Forms.Label
    Friend WithEvents InstrumentLabel_4 As System.Windows.Forms.Label
    Friend WithEvents InstrumentLabel_5 As System.Windows.Forms.Label
    Friend WithEvents InstrumentLabel_6 As System.Windows.Forms.Label
    Friend WithEvents InstrumentLabel_7 As System.Windows.Forms.Label
    Friend WithEvents InstrumentLabel_9 As System.Windows.Forms.Label
    Friend WithEvents InstrumentLabel_8 As System.Windows.Forms.Label
    Friend WithEvents InstrumentLabel_10 As System.Windows.Forms.Label
    Friend WithEvents InstrumentLabel_11 As System.Windows.Forms.Label
    Friend WithEvents InstrumentLabel_12 As System.Windows.Forms.Label
    Friend WithEvents InstrumentLabel_13 As System.Windows.Forms.Label
    Friend WithEvents InstrumentLabel_14 As System.Windows.Forms.Label
    Friend WithEvents InstrumentLabel_15 As System.Windows.Forms.Label
    Friend WithEvents InstrumentLabel_16 As System.Windows.Forms.Label
    Friend WithEvents InstrumentLabel_17 As System.Windows.Forms.Label
    Friend WithEvents InstrumentLabel_18 As System.Windows.Forms.Label
    Friend WithEvents InstrumentLabel_19 As System.Windows.Forms.Label
    Friend WithEvents InstrumentLabel_20 As System.Windows.Forms.Label
    Friend WithEvents InstrumentLabel_21 As System.Windows.Forms.Label
    Friend WithEvents InstrumentLabel_22 As System.Windows.Forms.Label
    Friend WithEvents InstrumentLabel_23 As System.Windows.Forms.Label
    Friend WithEvents InstrumentLabel_24 As System.Windows.Forms.Label
    Friend WithEvents InstrumentLabel_25 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents TxtSearch As System.Windows.Forms.TextBox
    Friend WithEvents CmdSearchOK As System.Windows.Forms.Button
    Friend WithEvents CmdSearchCancel As System.Windows.Forms.Button
    Friend WithEvents FrSearch As System.Windows.Forms.GroupBox
    Friend WithEvents sspSTestPanel As System.Windows.Forms.PictureBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSTest))
        Me.PassFrame_0 = New System.Windows.Forms.PictureBox()
        Me.sscTest_0 = New System.Windows.Forms.Button()
        Me.imlButtons = New System.Windows.Forms.ImageList(Me.components)
        Me.InstrumentLabel_0 = New System.Windows.Forms.Label()
        Me.FrPPUStart = New System.Windows.Forms.GroupBox()
        Me.cmdPPUstartOK = New System.Windows.Forms.Button()
        Me.txtPPUstart = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.DetailsText = New System.Windows.Forms.RichTextBox()
        Me.proProgress = New System.Windows.Forms.ProgressBar()
        Me.cmdDetails = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.cmdAbort = New System.Windows.Forms.Button()
        Me.cmdPause = New System.Windows.Forms.Button()
        Me.cmdLOS = New System.Windows.Forms.Button()
        Me.cmbSelectStep = New System.Windows.Forms.ComboBox()
        Me.cmbSelectTest = New System.Windows.Forms.ComboBox()
        Me.cmdCOF = New System.Windows.Forms.Button()
        Me.cmdSOF = New System.Windows.Forms.Button()
        Me.cmdLOE = New System.Windows.Forms.Button()
        Me.cmdSOE = New System.Windows.Forms.Button()
        Me.lblStepFailCount = New System.Windows.Forms.Label()
        Me.lblFailCount = New System.Windows.Forms.Label()
        Me.lblStepPassCount = New System.Windows.Forms.Label()
        Me.lblPassCount = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.LblSelectStep = New System.Windows.Forms.Label()
        Me.LblSelectTest = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblRunMode = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblTestMode = New System.Windows.Forms.Label()
        Me.lblFaultMode = New System.Windows.Forms.Label()
        Me.cmdHelp = New System.Windows.Forms.Button()
        Me.picNoTest = New System.Windows.Forms.PictureBox()
        Me.picTestPassed = New System.Windows.Forms.PictureBox()
        Me.picTestFailed = New System.Windows.Forms.PictureBox()
        Me.picTestUnknown = New System.Windows.Forms.PictureBox()
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.timTimer = New System.Windows.Forms.Timer(Me.components)
        Me.sbrUserInformation = New System.Windows.Forms.StatusBar()
        Me.sbrUserInformation_Panel1 = New System.Windows.Forms.StatusBarPanel()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.sscTest_1 = New System.Windows.Forms.Button()
        Me.sscTest_2 = New System.Windows.Forms.Button()
        Me.sscTest_3 = New System.Windows.Forms.Button()
        Me.sscTest_4 = New System.Windows.Forms.Button()
        Me.sscTest_5 = New System.Windows.Forms.Button()
        Me.sscTest_6 = New System.Windows.Forms.Button()
        Me.sscTest_23 = New System.Windows.Forms.Button()
        Me.sscTest_13 = New System.Windows.Forms.Button()
        Me.sscTest_14 = New System.Windows.Forms.Button()
        Me.sscTest_15 = New System.Windows.Forms.Button()
        Me.sscTest_16 = New System.Windows.Forms.Button()
        Me.sscTest_17 = New System.Windows.Forms.Button()
        Me.sscTest_18 = New System.Windows.Forms.Button()
        Me.sscTest_19 = New System.Windows.Forms.Button()
        Me.sscTest_8 = New System.Windows.Forms.Button()
        Me.sscTest_21 = New System.Windows.Forms.Button()
        Me.sscTest_9 = New System.Windows.Forms.Button()
        Me.sscTest_20 = New System.Windows.Forms.Button()
        Me.sscTest_10 = New System.Windows.Forms.Button()
        Me.sscTest_11 = New System.Windows.Forms.Button()
        Me.sscTest_24 = New System.Windows.Forms.Button()
        Me.sscTest_25 = New System.Windows.Forms.Button()
        Me.sscTest_22 = New System.Windows.Forms.Button()
        Me.sscTest_12 = New System.Windows.Forms.Button()
        Me.sscTest_7 = New System.Windows.Forms.Button()
        Me.PassFrame_1 = New System.Windows.Forms.PictureBox()
        Me.InstrumentLabel_1 = New System.Windows.Forms.Label()
        Me.PassFrame_2 = New System.Windows.Forms.PictureBox()
        Me.InstrumentLabel_2 = New System.Windows.Forms.Label()
        Me.PassFrame_3 = New System.Windows.Forms.PictureBox()
        Me.InstrumentLabel_3 = New System.Windows.Forms.Label()
        Me.PassFrame_4 = New System.Windows.Forms.PictureBox()
        Me.InstrumentLabel_4 = New System.Windows.Forms.Label()
        Me.PassFrame_5 = New System.Windows.Forms.PictureBox()
        Me.InstrumentLabel_5 = New System.Windows.Forms.Label()
        Me.PassFrame_6 = New System.Windows.Forms.PictureBox()
        Me.InstrumentLabel_6 = New System.Windows.Forms.Label()
        Me.PassFrame_13 = New System.Windows.Forms.PictureBox()
        Me.InstrumentLabel_13 = New System.Windows.Forms.Label()
        Me.PassFrame_14 = New System.Windows.Forms.PictureBox()
        Me.InstrumentLabel_14 = New System.Windows.Forms.Label()
        Me.PassFrame_15 = New System.Windows.Forms.PictureBox()
        Me.InstrumentLabel_15 = New System.Windows.Forms.Label()
        Me.PassFrame_16 = New System.Windows.Forms.PictureBox()
        Me.InstrumentLabel_16 = New System.Windows.Forms.Label()
        Me.PassFrame_17 = New System.Windows.Forms.PictureBox()
        Me.InstrumentLabel_17 = New System.Windows.Forms.Label()
        Me.PassFrame_18 = New System.Windows.Forms.PictureBox()
        Me.InstrumentLabel_18 = New System.Windows.Forms.Label()
        Me.PassFrame_19 = New System.Windows.Forms.PictureBox()
        Me.InstrumentLabel_19 = New System.Windows.Forms.Label()
        Me.PassFrame_21 = New System.Windows.Forms.PictureBox()
        Me.InstrumentLabel_21 = New System.Windows.Forms.Label()
        Me.PassFrame_7 = New System.Windows.Forms.PictureBox()
        Me.InstrumentLabel_7 = New System.Windows.Forms.Label()
        Me.PassFrame_22 = New System.Windows.Forms.PictureBox()
        Me.InstrumentLabel_22 = New System.Windows.Forms.Label()
        Me.PassFrame_8 = New System.Windows.Forms.PictureBox()
        Me.InstrumentLabel_8 = New System.Windows.Forms.Label()
        Me.PassFrame_23 = New System.Windows.Forms.PictureBox()
        Me.InstrumentLabel_23 = New System.Windows.Forms.Label()
        Me.PassFrame_9 = New System.Windows.Forms.PictureBox()
        Me.InstrumentLabel_9 = New System.Windows.Forms.Label()
        Me.PassFrame_10 = New System.Windows.Forms.PictureBox()
        Me.InstrumentLabel_10 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TxtSearch = New System.Windows.Forms.TextBox()
        Me.CmdSearchOK = New System.Windows.Forms.Button()
        Me.CmdSearchCancel = New System.Windows.Forms.Button()
        Me.FrSearch = New System.Windows.Forms.GroupBox()
        Me.sspSTestPanel = New System.Windows.Forms.PictureBox()
        Me.PassFrame_20 = New System.Windows.Forms.PictureBox()
        Me.InstrumentLabel_20 = New System.Windows.Forms.Label()
        Me.PassFrame_25 = New System.Windows.Forms.PictureBox()
        Me.InstrumentLabel_25 = New System.Windows.Forms.Label()
        Me.PassFrame_12 = New System.Windows.Forms.PictureBox()
        Me.InstrumentLabel_12 = New System.Windows.Forms.Label()
        Me.PassFrame_24 = New System.Windows.Forms.PictureBox()
        Me.InstrumentLabel_24 = New System.Windows.Forms.Label()
        Me.PassFrame_11 = New System.Windows.Forms.PictureBox()
        Me.InstrumentLabel_11 = New System.Windows.Forms.Label()
        CType(Me.PassFrame_0, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FrPPUStart.SuspendLayout()
        Me.Frame3.SuspendLayout()
        CType(Me.picNoTest, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picTestPassed, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picTestFailed, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picTestUnknown, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sbrUserInformation_Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PassFrame_1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PassFrame_2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PassFrame_3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PassFrame_4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PassFrame_5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PassFrame_6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PassFrame_13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PassFrame_14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PassFrame_15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PassFrame_16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PassFrame_17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PassFrame_18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PassFrame_19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PassFrame_21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PassFrame_7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PassFrame_22, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PassFrame_8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PassFrame_23, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PassFrame_9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PassFrame_10, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FrSearch.SuspendLayout()
        CType(Me.sspSTestPanel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PassFrame_20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PassFrame_25, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PassFrame_12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PassFrame_24, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PassFrame_11, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PassFrame_0
        '
        Me.PassFrame_0.BackColor = System.Drawing.SystemColors.Control
        Me.PassFrame_0.Image = CType(resources.GetObject("PassFrame_0.Image"), System.Drawing.Image)
        Me.PassFrame_0.Location = New System.Drawing.Point(12, 63)
        Me.PassFrame_0.Name = "PassFrame_0"
        Me.PassFrame_0.Size = New System.Drawing.Size(32, 16)
        Me.PassFrame_0.TabIndex = 8
        Me.PassFrame_0.TabStop = False
        '
        'sscTest_0
        '
        Me.sscTest_0.BackColor = System.Drawing.Color.LightGray
        Me.sscTest_0.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.sscTest_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.sscTest_0.ImageKey = "E2E"
        Me.sscTest_0.ImageList = Me.imlButtons
        Me.sscTest_0.Location = New System.Drawing.Point(53, 63)
        Me.sscTest_0.Margin = New System.Windows.Forms.Padding(0)
        Me.sscTest_0.Name = "sscTest_0"
        Me.sscTest_0.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.sscTest_0.Size = New System.Drawing.Size(23, 23)
        Me.sscTest_0.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.sscTest_0, "Start End to End Test")
        Me.sscTest_0.UseVisualStyleBackColor = False
        '
        'imlButtons
        '
        Me.imlButtons.ImageStream = CType(resources.GetObject("imlButtons.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imlButtons.TransparentColor = System.Drawing.Color.Transparent
        Me.imlButtons.Images.SetKeyName(0, "E2E")
        Me.imlButtons.Images.SetKeyName(1, "M1553")
        Me.imlButtons.Images.SetKeyName(2, "DTS")
        Me.imlButtons.Images.SetKeyName(3, "LF1")
        Me.imlButtons.Images.SetKeyName(4, "LF2")
        Me.imlButtons.Images.SetKeyName(5, "LF3")
        Me.imlButtons.Images.SetKeyName(6, "MF")
        Me.imlButtons.Images.SetKeyName(7, "HF")
        Me.imlButtons.Images.SetKeyName(8, "DMM")
        Me.imlButtons.Images.SetKeyName(9, "DSO")
        Me.imlButtons.Images.SetKeyName(10, "CT")
        Me.imlButtons.Images.SetKeyName(11, "ARB")
        Me.imlButtons.Images.SetKeyName(12, "FGEN")
        Me.imlButtons.Images.SetKeyName(13, "PPU")
        Me.imlButtons.Images.SetKeyName(14, "S/R")
        Me.imlButtons.Images.SetKeyName(15, "Serial")
        Me.imlButtons.Images.SetKeyName(16, "Com1")
        Me.imlButtons.Images.SetKeyName(17, "Com2")
        Me.imlButtons.Images.SetKeyName(18, "Gigabit")
        Me.imlButtons.Images.SetKeyName(19, "CANbus")
        Me.imlButtons.Images.SetKeyName(20, "APROBE")
        Me.imlButtons.Images.SetKeyName(21, "RF Power Meter")
        Me.imlButtons.Images.SetKeyName(22, "RF Counter")
        Me.imlButtons.Images.SetKeyName(23, "RF Down Converter")
        Me.imlButtons.Images.SetKeyName(24, "RF Modulation Analyzer")
        Me.imlButtons.Images.SetKeyName(25, "RF Synthesizer")
        '
        'InstrumentLabel_0
        '
        Me.InstrumentLabel_0.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.InstrumentLabel_0.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InstrumentLabel_0.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.InstrumentLabel_0.Location = New System.Drawing.Point(85, 63)
        Me.InstrumentLabel_0.Name = "InstrumentLabel_0"
        Me.InstrumentLabel_0.Size = New System.Drawing.Size(209, 17)
        Me.InstrumentLabel_0.TabIndex = 12
        Me.InstrumentLabel_0.Text = "Run End To End Test"
        '
        'FrPPUStart
        '
        Me.FrPPUStart.BackColor = System.Drawing.SystemColors.ControlLight
        Me.FrPPUStart.Controls.Add(Me.cmdPPUstartOK)
        Me.FrPPUStart.Controls.Add(Me.txtPPUstart)
        Me.FrPPUStart.Controls.Add(Me.Label4)
        Me.FrPPUStart.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FrPPUStart.Location = New System.Drawing.Point(40, 458)
        Me.FrPPUStart.Name = "FrPPUStart"
        Me.FrPPUStart.Size = New System.Drawing.Size(206, 101)
        Me.FrPPUStart.TabIndex = 41
        Me.FrPPUStart.TabStop = False
        Me.FrPPUStart.Text = "Start PPU Test"
        Me.FrPPUStart.Visible = False
        '
        'cmdPPUstartOK
        '
        Me.cmdPPUstartOK.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPPUstartOK.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPPUstartOK.Location = New System.Drawing.Point(48, 66)
        Me.cmdPPUstartOK.Name = "cmdPPUstartOK"
        Me.cmdPPUstartOK.Size = New System.Drawing.Size(101, 23)
        Me.cmdPPUstartOK.TabIndex = 44
        Me.cmdPPUstartOK.Text = "OK"
        Me.cmdPPUstartOK.UseVisualStyleBackColor = False
        '
        'txtPPUstart
        '
        Me.txtPPUstart.BackColor = System.Drawing.SystemColors.Window
        Me.txtPPUstart.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPPUstart.Location = New System.Drawing.Point(158, 21)
        Me.txtPPUstart.Name = "txtPPUstart"
        Me.txtPPUstart.Size = New System.Drawing.Size(30, 20)
        Me.txtPPUstart.TabIndex = 42
        Me.txtPPUstart.Text = "1"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.SystemColors.ControlLight
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(12, 19)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(120, 31)
        Me.Label4.TabIndex = 43
        Me.Label4.Text = "Enter Starting PPU.  Then Press OK."
        '
        'DetailsText
        '
        Me.DetailsText.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DetailsText.Location = New System.Drawing.Point(-1, 418)
        Me.DetailsText.Name = "DetailsText"
        Me.DetailsText.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical
        Me.DetailsText.Size = New System.Drawing.Size(649, 22)
        Me.DetailsText.TabIndex = 1
        Me.DetailsText.Text = ""
        Me.DetailsText.Visible = False
        '
        'proProgress
        '
        Me.proProgress.Location = New System.Drawing.Point(96, 380)
        Me.proProgress.Name = "proProgress"
        Me.proProgress.Size = New System.Drawing.Size(328, 29)
        Me.proProgress.TabIndex = 4
        Me.proProgress.Visible = False
        '
        'cmdDetails
        '
        Me.cmdDetails.BackColor = System.Drawing.SystemColors.Control
        Me.cmdDetails.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDetails.Location = New System.Drawing.Point(550, 381)
        Me.cmdDetails.Name = "cmdDetails"
        Me.cmdDetails.Size = New System.Drawing.Size(90, 25)
        Me.cmdDetails.TabIndex = 0
        Me.cmdDetails.Text = "Details >>"
        Me.ToolTip1.SetToolTip(Me.cmdDetails, "Show/Hide Test Details")
        Me.cmdDetails.UseVisualStyleBackColor = False
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Location = New System.Drawing.Point(475, 381)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(70, 25)
        Me.cmdClose.TabIndex = 2
        Me.cmdClose.Text = "Close"
        Me.ToolTip1.SetToolTip(Me.cmdClose, "Close SelfTest Program")
        Me.cmdClose.UseVisualStyleBackColor = False
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.Color.SteelBlue
        Me.Frame3.Controls.Add(Me.cmdAbort)
        Me.Frame3.Controls.Add(Me.cmdPause)
        Me.Frame3.Controls.Add(Me.cmdLOS)
        Me.Frame3.Controls.Add(Me.cmbSelectStep)
        Me.Frame3.Controls.Add(Me.cmbSelectTest)
        Me.Frame3.Controls.Add(Me.cmdCOF)
        Me.Frame3.Controls.Add(Me.cmdSOF)
        Me.Frame3.Controls.Add(Me.cmdLOE)
        Me.Frame3.Controls.Add(Me.cmdSOE)
        Me.Frame3.Controls.Add(Me.lblStepFailCount)
        Me.Frame3.Controls.Add(Me.lblFailCount)
        Me.Frame3.Controls.Add(Me.lblStepPassCount)
        Me.Frame3.Controls.Add(Me.lblPassCount)
        Me.Frame3.Controls.Add(Me.Label6)
        Me.Frame3.Controls.Add(Me.LblSelectStep)
        Me.Frame3.Controls.Add(Me.LblSelectTest)
        Me.Frame3.Controls.Add(Me.Label1)
        Me.Frame3.Controls.Add(Me.lblRunMode)
        Me.Frame3.Controls.Add(Me.Label3)
        Me.Frame3.Controls.Add(Me.lblTestMode)
        Me.Frame3.Controls.Add(Me.lblFaultMode)
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(0, -6)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Size = New System.Drawing.Size(649, 66)
        Me.Frame3.TabIndex = 19
        Me.Frame3.TabStop = False
        '
        'cmdAbort
        '
        Me.cmdAbort.BackColor = System.Drawing.SystemColors.Control
        Me.cmdAbort.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAbort.Location = New System.Drawing.Point(387, 37)
        Me.cmdAbort.Name = "cmdAbort"
        Me.cmdAbort.Size = New System.Drawing.Size(118, 24)
        Me.cmdAbort.TabIndex = 40
        Me.cmdAbort.Text = "Abort Test"
        Me.cmdAbort.UseVisualStyleBackColor = False
        Me.cmdAbort.Visible = False
        '
        'cmdPause
        '
        Me.cmdPause.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPause.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPause.Location = New System.Drawing.Point(387, 12)
        Me.cmdPause.Name = "cmdPause"
        Me.cmdPause.Size = New System.Drawing.Size(118, 24)
        Me.cmdPause.TabIndex = 39
        Me.cmdPause.Text = "Pause Test"
        Me.cmdPause.UseVisualStyleBackColor = False
        Me.cmdPause.Visible = False
        '
        'cmdLOS
        '
        Me.cmdLOS.BackColor = System.Drawing.SystemColors.Control
        Me.cmdLOS.Font = New System.Drawing.Font("Arial", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdLOS.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdLOS.Location = New System.Drawing.Point(73, 40)
        Me.cmdLOS.Name = "cmdLOS"
        Me.cmdLOS.Size = New System.Drawing.Size(33, 23)
        Me.cmdLOS.TabIndex = 33
        Me.cmdLOS.Text = "LOS"
        Me.ToolTip1.SetToolTip(Me.cmdLOS, "Loop On Step")
        Me.cmdLOS.UseVisualStyleBackColor = False
        '
        'cmbSelectStep
        '
        Me.cmbSelectStep.BackColor = System.Drawing.SystemColors.Window
        Me.cmbSelectStep.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbSelectStep.Location = New System.Drawing.Point(243, 37)
        Me.cmbSelectStep.Name = "cmbSelectStep"
        Me.cmbSelectStep.Size = New System.Drawing.Size(137, 21)
        Me.cmbSelectStep.TabIndex = 32
        Me.cmbSelectStep.Text = "DSO-08-001"
        Me.cmbSelectStep.Visible = False
        '
        'cmbSelectTest
        '
        Me.cmbSelectTest.BackColor = System.Drawing.SystemColors.Window
        Me.cmbSelectTest.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbSelectTest.Location = New System.Drawing.Point(243, 12)
        Me.cmbSelectTest.Name = "cmbSelectTest"
        Me.cmbSelectTest.Size = New System.Drawing.Size(137, 21)
        Me.cmbSelectTest.TabIndex = 31
        Me.cmbSelectTest.Text = "DSO"
        Me.cmbSelectTest.Visible = False
        '
        'cmdCOF
        '
        Me.cmdCOF.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCOF.Font = New System.Drawing.Font("Arial", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCOF.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCOF.Location = New System.Drawing.Point(154, 40)
        Me.cmdCOF.Name = "cmdCOF"
        Me.cmdCOF.Size = New System.Drawing.Size(33, 23)
        Me.cmdCOF.TabIndex = 23
        Me.cmdCOF.Text = "COF"
        Me.ToolTip1.SetToolTip(Me.cmdCOF, "Continue On Fault")
        Me.cmdCOF.UseVisualStyleBackColor = False
        '
        'cmdSOF
        '
        Me.cmdSOF.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSOF.Font = New System.Drawing.Font("Arial", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSOF.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSOF.Location = New System.Drawing.Point(121, 40)
        Me.cmdSOF.Name = "cmdSOF"
        Me.cmdSOF.Size = New System.Drawing.Size(33, 23)
        Me.cmdSOF.TabIndex = 22
        Me.cmdSOF.Text = "SOF"
        Me.ToolTip1.SetToolTip(Me.cmdSOF, "Stop On Fault")
        Me.cmdSOF.UseVisualStyleBackColor = False
        '
        'cmdLOE
        '
        Me.cmdLOE.BackColor = System.Drawing.SystemColors.Control
        Me.cmdLOE.Font = New System.Drawing.Font("Arial", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdLOE.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdLOE.Location = New System.Drawing.Point(40, 40)
        Me.cmdLOE.Name = "cmdLOE"
        Me.cmdLOE.Size = New System.Drawing.Size(33, 23)
        Me.cmdLOE.TabIndex = 21
        Me.cmdLOE.Text = "LOE"
        Me.ToolTip1.SetToolTip(Me.cmdLOE, "Loop On End")
        Me.cmdLOE.UseVisualStyleBackColor = False
        '
        'cmdSOE
        '
        Me.cmdSOE.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSOE.Font = New System.Drawing.Font("Arial", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSOE.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSOE.Location = New System.Drawing.Point(8, 40)
        Me.cmdSOE.Name = "cmdSOE"
        Me.cmdSOE.Size = New System.Drawing.Size(33, 23)
        Me.cmdSOE.TabIndex = 20
        Me.cmdSOE.Text = "SOE"
        Me.ToolTip1.SetToolTip(Me.cmdSOE, "Stop On End")
        Me.cmdSOE.UseVisualStyleBackColor = False
        '
        'lblStepFailCount
        '
        Me.lblStepFailCount.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblStepFailCount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblStepFailCount.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStepFailCount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblStepFailCount.Location = New System.Drawing.Point(582, 42)
        Me.lblStepFailCount.Name = "lblStepFailCount"
        Me.lblStepFailCount.Size = New System.Drawing.Size(58, 20)
        Me.lblStepFailCount.TabIndex = 38
        Me.lblStepFailCount.Text = "0"
        Me.lblStepFailCount.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblFailCount
        '
        Me.lblFailCount.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblFailCount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblFailCount.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFailCount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblFailCount.Location = New System.Drawing.Point(517, 41)
        Me.lblFailCount.Name = "lblFailCount"
        Me.lblFailCount.Size = New System.Drawing.Size(58, 20)
        Me.lblFailCount.TabIndex = 26
        Me.lblFailCount.Text = "0"
        Me.lblFailCount.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblStepPassCount
        '
        Me.lblStepPassCount.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblStepPassCount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblStepPassCount.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStepPassCount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblStepPassCount.Location = New System.Drawing.Point(582, 24)
        Me.lblStepPassCount.Name = "lblStepPassCount"
        Me.lblStepPassCount.Size = New System.Drawing.Size(58, 20)
        Me.lblStepPassCount.TabIndex = 37
        Me.lblStepPassCount.Text = "0"
        Me.lblStepPassCount.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblPassCount
        '
        Me.lblPassCount.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblPassCount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblPassCount.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPassCount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblPassCount.Location = New System.Drawing.Point(517, 23)
        Me.lblPassCount.Name = "lblPassCount"
        Me.lblPassCount.Size = New System.Drawing.Size(58, 20)
        Me.lblPassCount.TabIndex = 27
        Me.lblPassCount.Text = "0"
        Me.lblPassCount.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(583, 8)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(58, 21)
        Me.Label6.TabIndex = 36
        Me.Label6.Text = "Steps"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'LblSelectStep
        '
        Me.LblSelectStep.BackColor = System.Drawing.Color.Transparent
        Me.LblSelectStep.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblSelectStep.ForeColor = System.Drawing.Color.Black
        Me.LblSelectStep.Location = New System.Drawing.Point(194, 37)
        Me.LblSelectStep.Name = "LblSelectStep"
        Me.LblSelectStep.Size = New System.Drawing.Size(41, 21)
        Me.LblSelectStep.TabIndex = 35
        Me.LblSelectStep.Text = "Step"
        Me.LblSelectStep.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.LblSelectStep.Visible = False
        '
        'LblSelectTest
        '
        Me.LblSelectTest.BackColor = System.Drawing.Color.Transparent
        Me.LblSelectTest.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblSelectTest.ForeColor = System.Drawing.Color.Black
        Me.LblSelectTest.Location = New System.Drawing.Point(194, 12)
        Me.LblSelectTest.Name = "LblSelectTest"
        Me.LblSelectTest.Size = New System.Drawing.Size(41, 21)
        Me.LblSelectTest.TabIndex = 34
        Me.LblSelectTest.Text = "Test"
        Me.LblSelectTest.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.LblSelectTest.Visible = False
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(516, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(58, 21)
        Me.Label1.TabIndex = 30
        Me.Label1.Text = "Tests"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblRunMode
        '
        Me.lblRunMode.BackColor = System.Drawing.Color.Transparent
        Me.lblRunMode.ForeColor = System.Drawing.Color.Black
        Me.lblRunMode.Location = New System.Drawing.Point(8, 11)
        Me.lblRunMode.Name = "lblRunMode"
        Me.lblRunMode.Size = New System.Drawing.Size(40, 27)
        Me.lblRunMode.TabIndex = 29
        Me.lblRunMode.Text = "Run Mode"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(121, 11)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(40, 27)
        Me.Label3.TabIndex = 28
        Me.Label3.Text = "Fault Mode"
        '
        'lblTestMode
        '
        Me.lblTestMode.BackColor = System.Drawing.Color.Transparent
        Me.lblTestMode.ForeColor = System.Drawing.Color.Black
        Me.lblTestMode.Location = New System.Drawing.Point(49, 16)
        Me.lblTestMode.Name = "lblTestMode"
        Me.lblTestMode.Size = New System.Drawing.Size(39, 19)
        Me.lblTestMode.TabIndex = 25
        Me.lblTestMode.Text = "SOE"
        Me.lblTestMode.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblFaultMode
        '
        Me.lblFaultMode.BackColor = System.Drawing.Color.Transparent
        Me.lblFaultMode.ForeColor = System.Drawing.Color.Black
        Me.lblFaultMode.Location = New System.Drawing.Point(155, 15)
        Me.lblFaultMode.Name = "lblFaultMode"
        Me.lblFaultMode.Size = New System.Drawing.Size(38, 19)
        Me.lblFaultMode.TabIndex = 24
        Me.lblFaultMode.Text = "SOF"
        Me.lblFaultMode.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'cmdHelp
        '
        Me.cmdHelp.BackColor = System.Drawing.SystemColors.Control
        Me.cmdHelp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdHelp.Location = New System.Drawing.Point(430, 381)
        Me.cmdHelp.Name = "cmdHelp"
        Me.cmdHelp.Size = New System.Drawing.Size(38, 25)
        Me.cmdHelp.TabIndex = 18
        Me.cmdHelp.Text = "Help"
        Me.ToolTip1.SetToolTip(Me.cmdHelp, "Open Help File")
        Me.cmdHelp.UseVisualStyleBackColor = False
        '
        'picNoTest
        '
        Me.picNoTest.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.picNoTest.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.picNoTest.Image = CType(resources.GetObject("picNoTest.Image"), System.Drawing.Image)
        Me.picNoTest.Location = New System.Drawing.Point(561, 348)
        Me.picNoTest.Name = "picNoTest"
        Me.picNoTest.Size = New System.Drawing.Size(33, 22)
        Me.picNoTest.TabIndex = 11
        Me.picNoTest.TabStop = False
        Me.picNoTest.Visible = False
        '
        'picTestPassed
        '
        Me.picTestPassed.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.picTestPassed.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.picTestPassed.Image = CType(resources.GetObject("picTestPassed.Image"), System.Drawing.Image)
        Me.picTestPassed.Location = New System.Drawing.Point(516, 350)
        Me.picTestPassed.Name = "picTestPassed"
        Me.picTestPassed.Size = New System.Drawing.Size(34, 21)
        Me.picTestPassed.TabIndex = 10
        Me.picTestPassed.TabStop = False
        Me.picTestPassed.Visible = False
        '
        'picTestFailed
        '
        Me.picTestFailed.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.picTestFailed.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.picTestFailed.Image = CType(resources.GetObject("picTestFailed.Image"), System.Drawing.Image)
        Me.picTestFailed.Location = New System.Drawing.Point(603, 348)
        Me.picTestFailed.Name = "picTestFailed"
        Me.picTestFailed.Size = New System.Drawing.Size(33, 22)
        Me.picTestFailed.TabIndex = 9
        Me.picTestFailed.TabStop = False
        Me.picTestFailed.Visible = False
        '
        'picTestUnknown
        '
        Me.picTestUnknown.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.picTestUnknown.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.picTestUnknown.Image = CType(resources.GetObject("picTestUnknown.Image"), System.Drawing.Image)
        Me.picTestUnknown.Location = New System.Drawing.Point(475, 350)
        Me.picTestUnknown.Name = "picTestUnknown"
        Me.picTestUnknown.Size = New System.Drawing.Size(33, 21)
        Me.picTestUnknown.TabIndex = 7
        Me.picTestUnknown.TabStop = False
        Me.picTestUnknown.Visible = False
        '
        'Timer2
        '
        Me.Timer2.Interval = 300
        '
        'timTimer
        '
        Me.timTimer.Interval = 300
        '
        'sbrUserInformation
        '
        Me.sbrUserInformation.Location = New System.Drawing.Point(0, 424)
        Me.sbrUserInformation.Name = "sbrUserInformation"
        Me.sbrUserInformation.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.sbrUserInformation_Panel1})
        Me.sbrUserInformation.Size = New System.Drawing.Size(648, 22)
        Me.sbrUserInformation.SizingGrip = False
        Me.sbrUserInformation.TabIndex = 3
        '
        'sbrUserInformation_Panel1
        '
        Me.sbrUserInformation_Panel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
        Me.sbrUserInformation_Panel1.Name = "sbrUserInformation_Panel1"
        Me.sbrUserInformation_Panel1.Text = "APS-6061 Fixed Power Unit Failed"
        Me.sbrUserInformation_Panel1.Width = 647
        '
        'sscTest_1
        '
        Me.sscTest_1.BackColor = System.Drawing.Color.LightGray
        Me.sscTest_1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.sscTest_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.sscTest_1.ImageKey = "M1553"
        Me.sscTest_1.ImageList = Me.imlButtons
        Me.sscTest_1.Location = New System.Drawing.Point(53, 86)
        Me.sscTest_1.Margin = New System.Windows.Forms.Padding(0)
        Me.sscTest_1.Name = "sscTest_1"
        Me.sscTest_1.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.sscTest_1.Size = New System.Drawing.Size(23, 23)
        Me.sscTest_1.TabIndex = 43
        Me.ToolTip1.SetToolTip(Me.sscTest_1, "Start 1553 Test")
        Me.sscTest_1.UseVisualStyleBackColor = False
        '
        'sscTest_2
        '
        Me.sscTest_2.BackColor = System.Drawing.Color.LightGray
        Me.sscTest_2.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.sscTest_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.sscTest_2.ImageKey = "DTS"
        Me.sscTest_2.ImageList = Me.imlButtons
        Me.sscTest_2.Location = New System.Drawing.Point(53, 109)
        Me.sscTest_2.Margin = New System.Windows.Forms.Padding(0)
        Me.sscTest_2.Name = "sscTest_2"
        Me.sscTest_2.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.sscTest_2.Size = New System.Drawing.Size(23, 23)
        Me.sscTest_2.TabIndex = 46
        Me.ToolTip1.SetToolTip(Me.sscTest_2, "Start DTS Test")
        Me.sscTest_2.UseVisualStyleBackColor = False
        '
        'sscTest_3
        '
        Me.sscTest_3.BackColor = System.Drawing.Color.LightGray
        Me.sscTest_3.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.sscTest_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.sscTest_3.ImageKey = "LF1"
        Me.sscTest_3.ImageList = Me.imlButtons
        Me.sscTest_3.Location = New System.Drawing.Point(53, 132)
        Me.sscTest_3.Margin = New System.Windows.Forms.Padding(0)
        Me.sscTest_3.Name = "sscTest_3"
        Me.sscTest_3.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.sscTest_3.Size = New System.Drawing.Size(23, 23)
        Me.sscTest_3.TabIndex = 49
        Me.ToolTip1.SetToolTip(Me.sscTest_3, "Start LF1 Switch Test")
        Me.sscTest_3.UseVisualStyleBackColor = False
        '
        'sscTest_4
        '
        Me.sscTest_4.BackColor = System.Drawing.Color.LightGray
        Me.sscTest_4.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.sscTest_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.sscTest_4.ImageKey = "LF2"
        Me.sscTest_4.ImageList = Me.imlButtons
        Me.sscTest_4.Location = New System.Drawing.Point(53, 155)
        Me.sscTest_4.Margin = New System.Windows.Forms.Padding(0)
        Me.sscTest_4.Name = "sscTest_4"
        Me.sscTest_4.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.sscTest_4.Size = New System.Drawing.Size(23, 23)
        Me.sscTest_4.TabIndex = 52
        Me.ToolTip1.SetToolTip(Me.sscTest_4, "Start LF2 Switch Test")
        Me.sscTest_4.UseVisualStyleBackColor = False
        '
        'sscTest_5
        '
        Me.sscTest_5.BackColor = System.Drawing.Color.LightGray
        Me.sscTest_5.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.sscTest_5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.sscTest_5.ImageKey = "LF3"
        Me.sscTest_5.ImageList = Me.imlButtons
        Me.sscTest_5.Location = New System.Drawing.Point(53, 178)
        Me.sscTest_5.Margin = New System.Windows.Forms.Padding(0)
        Me.sscTest_5.Name = "sscTest_5"
        Me.sscTest_5.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.sscTest_5.Size = New System.Drawing.Size(23, 23)
        Me.sscTest_5.TabIndex = 55
        Me.ToolTip1.SetToolTip(Me.sscTest_5, "Start LF3 Switch Test")
        Me.sscTest_5.UseVisualStyleBackColor = False
        '
        'sscTest_6
        '
        Me.sscTest_6.BackColor = System.Drawing.Color.LightGray
        Me.sscTest_6.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.sscTest_6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.sscTest_6.ImageKey = "MF"
        Me.sscTest_6.ImageList = Me.imlButtons
        Me.sscTest_6.Location = New System.Drawing.Point(53, 201)
        Me.sscTest_6.Margin = New System.Windows.Forms.Padding(0)
        Me.sscTest_6.Name = "sscTest_6"
        Me.sscTest_6.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.sscTest_6.Size = New System.Drawing.Size(23, 23)
        Me.sscTest_6.TabIndex = 58
        Me.ToolTip1.SetToolTip(Me.sscTest_6, "Start MF Switch Test")
        Me.sscTest_6.UseVisualStyleBackColor = False
        '
        'sscTest_23
        '
        Me.sscTest_23.BackColor = System.Drawing.Color.LightGray
        Me.sscTest_23.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.sscTest_23.ForeColor = System.Drawing.SystemColors.ControlText
        Me.sscTest_23.ImageKey = "RF Down Converter"
        Me.sscTest_23.ImageList = Me.imlButtons
        Me.sscTest_23.Location = New System.Drawing.Point(352, 293)
        Me.sscTest_23.Margin = New System.Windows.Forms.Padding(0)
        Me.sscTest_23.Name = "sscTest_23"
        Me.sscTest_23.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.sscTest_23.Size = New System.Drawing.Size(23, 23)
        Me.sscTest_23.TabIndex = 61
        Me.ToolTip1.SetToolTip(Me.sscTest_23, "Start RF COUNTER Test")
        Me.sscTest_23.UseVisualStyleBackColor = False
        '
        'sscTest_13
        '
        Me.sscTest_13.BackColor = System.Drawing.Color.LightGray
        Me.sscTest_13.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.sscTest_13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.sscTest_13.ImageKey = "PPU"
        Me.sscTest_13.ImageList = Me.imlButtons
        Me.sscTest_13.Location = New System.Drawing.Point(352, 63)
        Me.sscTest_13.Margin = New System.Windows.Forms.Padding(0)
        Me.sscTest_13.Name = "sscTest_13"
        Me.sscTest_13.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.sscTest_13.Size = New System.Drawing.Size(23, 23)
        Me.sscTest_13.TabIndex = 64
        Me.ToolTip1.SetToolTip(Me.sscTest_13, "Start PPU Test")
        Me.sscTest_13.UseVisualStyleBackColor = False
        '
        'sscTest_14
        '
        Me.sscTest_14.BackColor = System.Drawing.Color.LightGray
        Me.sscTest_14.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.sscTest_14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.sscTest_14.ImageKey = "S/R"
        Me.sscTest_14.ImageList = Me.imlButtons
        Me.sscTest_14.Location = New System.Drawing.Point(352, 86)
        Me.sscTest_14.Margin = New System.Windows.Forms.Padding(0)
        Me.sscTest_14.Name = "sscTest_14"
        Me.sscTest_14.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.sscTest_14.Size = New System.Drawing.Size(23, 23)
        Me.sscTest_14.TabIndex = 67
        Me.ToolTip1.SetToolTip(Me.sscTest_14, "Start S/R Test")
        Me.sscTest_14.UseVisualStyleBackColor = False
        '
        'sscTest_15
        '
        Me.sscTest_15.BackColor = System.Drawing.Color.LightGray
        Me.sscTest_15.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.sscTest_15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.sscTest_15.ImageKey = "Serial"
        Me.sscTest_15.ImageList = Me.imlButtons
        Me.sscTest_15.Location = New System.Drawing.Point(352, 109)
        Me.sscTest_15.Margin = New System.Windows.Forms.Padding(0)
        Me.sscTest_15.Name = "sscTest_15"
        Me.sscTest_15.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.sscTest_15.Size = New System.Drawing.Size(23, 23)
        Me.sscTest_15.TabIndex = 70
        Me.ToolTip1.SetToolTip(Me.sscTest_15, "Start COM3,4,5 Test")
        Me.sscTest_15.UseVisualStyleBackColor = False
        '
        'sscTest_16
        '
        Me.sscTest_16.BackColor = System.Drawing.Color.LightGray
        Me.sscTest_16.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.sscTest_16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.sscTest_16.ImageKey = "Com1"
        Me.sscTest_16.ImageList = Me.imlButtons
        Me.sscTest_16.Location = New System.Drawing.Point(352, 132)
        Me.sscTest_16.Margin = New System.Windows.Forms.Padding(0)
        Me.sscTest_16.Name = "sscTest_16"
        Me.sscTest_16.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.sscTest_16.Size = New System.Drawing.Size(23, 23)
        Me.sscTest_16.TabIndex = 73
        Me.ToolTip1.SetToolTip(Me.sscTest_16, "Start COM1 Test")
        Me.sscTest_16.UseVisualStyleBackColor = False
        '
        'sscTest_17
        '
        Me.sscTest_17.BackColor = System.Drawing.Color.LightGray
        Me.sscTest_17.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.sscTest_17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.sscTest_17.ImageKey = "Com2"
        Me.sscTest_17.ImageList = Me.imlButtons
        Me.sscTest_17.Location = New System.Drawing.Point(352, 155)
        Me.sscTest_17.Margin = New System.Windows.Forms.Padding(0)
        Me.sscTest_17.Name = "sscTest_17"
        Me.sscTest_17.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.sscTest_17.Size = New System.Drawing.Size(23, 23)
        Me.sscTest_17.TabIndex = 76
        Me.ToolTip1.SetToolTip(Me.sscTest_17, "Start COM2 Test")
        Me.sscTest_17.UseVisualStyleBackColor = False
        '
        'sscTest_18
        '
        Me.sscTest_18.BackColor = System.Drawing.Color.LightGray
        Me.sscTest_18.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.sscTest_18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.sscTest_18.ImageKey = "Gigabit"
        Me.sscTest_18.ImageList = Me.imlButtons
        Me.sscTest_18.Location = New System.Drawing.Point(352, 178)
        Me.sscTest_18.Margin = New System.Windows.Forms.Padding(0)
        Me.sscTest_18.Name = "sscTest_18"
        Me.sscTest_18.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.sscTest_18.Size = New System.Drawing.Size(23, 23)
        Me.sscTest_18.TabIndex = 79
        Me.ToolTip1.SetToolTip(Me.sscTest_18, "Start Ethernet Test")
        Me.sscTest_18.UseVisualStyleBackColor = False
        '
        'sscTest_19
        '
        Me.sscTest_19.BackColor = System.Drawing.Color.LightGray
        Me.sscTest_19.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.sscTest_19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.sscTest_19.ImageKey = "CANBUS"
        Me.sscTest_19.ImageList = Me.imlButtons
        Me.sscTest_19.Location = New System.Drawing.Point(352, 201)
        Me.sscTest_19.Margin = New System.Windows.Forms.Padding(0)
        Me.sscTest_19.Name = "sscTest_19"
        Me.sscTest_19.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.sscTest_19.Size = New System.Drawing.Size(23, 23)
        Me.sscTest_19.TabIndex = 85
        Me.ToolTip1.SetToolTip(Me.sscTest_19, "Start CAN Bus Test")
        Me.sscTest_19.UseVisualStyleBackColor = False
        '
        'sscTest_8
        '
        Me.sscTest_8.BackColor = System.Drawing.Color.LightGray
        Me.sscTest_8.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.sscTest_8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.sscTest_8.ImageKey = "DMM"
        Me.sscTest_8.ImageList = Me.imlButtons
        Me.sscTest_8.Location = New System.Drawing.Point(53, 247)
        Me.sscTest_8.Margin = New System.Windows.Forms.Padding(0)
        Me.sscTest_8.Name = "sscTest_8"
        Me.sscTest_8.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.sscTest_8.Size = New System.Drawing.Size(23, 23)
        Me.sscTest_8.TabIndex = 82
        Me.ToolTip1.SetToolTip(Me.sscTest_8, "Start DMM Test")
        Me.sscTest_8.UseVisualStyleBackColor = False
        '
        'sscTest_21
        '
        Me.sscTest_21.BackColor = System.Drawing.Color.LightGray
        Me.sscTest_21.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.sscTest_21.ForeColor = System.Drawing.SystemColors.ControlText
        Me.sscTest_21.ImageKey = "RF Power Meter"
        Me.sscTest_21.ImageList = Me.imlButtons
        Me.sscTest_21.Location = New System.Drawing.Point(352, 247)
        Me.sscTest_21.Margin = New System.Windows.Forms.Padding(0)
        Me.sscTest_21.Name = "sscTest_21"
        Me.sscTest_21.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.sscTest_21.Size = New System.Drawing.Size(23, 23)
        Me.sscTest_21.TabIndex = 91
        Me.ToolTip1.SetToolTip(Me.sscTest_21, "Start RFPM Test")
        Me.sscTest_21.UseVisualStyleBackColor = False
        '
        'sscTest_9
        '
        Me.sscTest_9.BackColor = System.Drawing.Color.LightGray
        Me.sscTest_9.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.sscTest_9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.sscTest_9.ImageKey = "DSO"
        Me.sscTest_9.ImageList = Me.imlButtons
        Me.sscTest_9.Location = New System.Drawing.Point(53, 270)
        Me.sscTest_9.Margin = New System.Windows.Forms.Padding(0)
        Me.sscTest_9.Name = "sscTest_9"
        Me.sscTest_9.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.sscTest_9.Size = New System.Drawing.Size(23, 23)
        Me.sscTest_9.TabIndex = 88
        Me.ToolTip1.SetToolTip(Me.sscTest_9, "Start DSO Test")
        Me.sscTest_9.UseVisualStyleBackColor = False
        '
        'sscTest_20
        '
        Me.sscTest_20.BackColor = System.Drawing.Color.LightGray
        Me.sscTest_20.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.sscTest_20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.sscTest_20.ImageKey = "APROBE"
        Me.sscTest_20.ImageList = Me.imlButtons
        Me.sscTest_20.Location = New System.Drawing.Point(352, 224)
        Me.sscTest_20.Margin = New System.Windows.Forms.Padding(0)
        Me.sscTest_20.Name = "sscTest_20"
        Me.sscTest_20.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.sscTest_20.Size = New System.Drawing.Size(23, 23)
        Me.sscTest_20.TabIndex = 97
        Me.ToolTip1.SetToolTip(Me.sscTest_20, "Start Analog Probe Test")
        Me.sscTest_20.UseVisualStyleBackColor = False
        '
        'sscTest_10
        '
        Me.sscTest_10.BackColor = System.Drawing.Color.LightGray
        Me.sscTest_10.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.sscTest_10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.sscTest_10.ImageKey = "CT"
        Me.sscTest_10.ImageList = Me.imlButtons
        Me.sscTest_10.Location = New System.Drawing.Point(53, 293)
        Me.sscTest_10.Margin = New System.Windows.Forms.Padding(0)
        Me.sscTest_10.Name = "sscTest_10"
        Me.sscTest_10.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.sscTest_10.Size = New System.Drawing.Size(23, 23)
        Me.sscTest_10.TabIndex = 94
        Me.ToolTip1.SetToolTip(Me.sscTest_10, "Start C/T Test")
        Me.sscTest_10.UseVisualStyleBackColor = False
        '
        'sscTest_11
        '
        Me.sscTest_11.BackColor = System.Drawing.Color.LightGray
        Me.sscTest_11.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.sscTest_11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.sscTest_11.ImageKey = "ARB"
        Me.sscTest_11.ImageList = Me.imlButtons
        Me.sscTest_11.Location = New System.Drawing.Point(53, 316)
        Me.sscTest_11.Margin = New System.Windows.Forms.Padding(0)
        Me.sscTest_11.Name = "sscTest_11"
        Me.sscTest_11.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.sscTest_11.Size = New System.Drawing.Size(23, 23)
        Me.sscTest_11.TabIndex = 100
        Me.ToolTip1.SetToolTip(Me.sscTest_11, "Start ARB Test")
        Me.sscTest_11.UseVisualStyleBackColor = False
        '
        'sscTest_24
        '
        Me.sscTest_24.BackColor = System.Drawing.Color.LightGray
        Me.sscTest_24.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.sscTest_24.ForeColor = System.Drawing.SystemColors.ControlText
        Me.sscTest_24.ImageKey = "RF Modulation Analyzer"
        Me.sscTest_24.ImageList = Me.imlButtons
        Me.sscTest_24.Location = New System.Drawing.Point(352, 316)
        Me.sscTest_24.Margin = New System.Windows.Forms.Padding(0)
        Me.sscTest_24.Name = "sscTest_24"
        Me.sscTest_24.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.sscTest_24.Size = New System.Drawing.Size(23, 23)
        Me.sscTest_24.TabIndex = 103
        Me.ToolTip1.SetToolTip(Me.sscTest_24, "RF Down Converter")
        Me.sscTest_24.UseVisualStyleBackColor = False
        '
        'sscTest_25
        '
        Me.sscTest_25.BackColor = System.Drawing.Color.LightGray
        Me.sscTest_25.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.sscTest_25.ForeColor = System.Drawing.SystemColors.ControlText
        Me.sscTest_25.ImageKey = "RF Synthesizer"
        Me.sscTest_25.ImageList = Me.imlButtons
        Me.sscTest_25.Location = New System.Drawing.Point(352, 339)
        Me.sscTest_25.Margin = New System.Windows.Forms.Padding(0)
        Me.sscTest_25.Name = "sscTest_25"
        Me.sscTest_25.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.sscTest_25.Size = New System.Drawing.Size(23, 23)
        Me.sscTest_25.TabIndex = 115
        Me.ToolTip1.SetToolTip(Me.sscTest_25, "RF Modulation Analyzer")
        Me.sscTest_25.UseVisualStyleBackColor = False
        '
        'sscTest_22
        '
        Me.sscTest_22.BackColor = System.Drawing.Color.LightGray
        Me.sscTest_22.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.sscTest_22.ForeColor = System.Drawing.SystemColors.ControlText
        Me.sscTest_22.ImageKey = "RF Counter"
        Me.sscTest_22.ImageList = Me.imlButtons
        Me.sscTest_22.Location = New System.Drawing.Point(352, 270)
        Me.sscTest_22.Margin = New System.Windows.Forms.Padding(0)
        Me.sscTest_22.Name = "sscTest_22"
        Me.sscTest_22.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.sscTest_22.Size = New System.Drawing.Size(23, 23)
        Me.sscTest_22.TabIndex = 112
        Me.ToolTip1.SetToolTip(Me.sscTest_22, "Start RFSTIM Test")
        Me.sscTest_22.UseVisualStyleBackColor = False
        '
        'sscTest_12
        '
        Me.sscTest_12.BackColor = System.Drawing.Color.LightGray
        Me.sscTest_12.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.sscTest_12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.sscTest_12.ImageKey = "FGEN"
        Me.sscTest_12.ImageList = Me.imlButtons
        Me.sscTest_12.Location = New System.Drawing.Point(53, 339)
        Me.sscTest_12.Margin = New System.Windows.Forms.Padding(0)
        Me.sscTest_12.Name = "sscTest_12"
        Me.sscTest_12.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.sscTest_12.Size = New System.Drawing.Size(23, 23)
        Me.sscTest_12.TabIndex = 106
        Me.ToolTip1.SetToolTip(Me.sscTest_12, "Start FG Test")
        Me.sscTest_12.UseVisualStyleBackColor = False
        '
        'sscTest_7
        '
        Me.sscTest_7.BackColor = System.Drawing.Color.LightGray
        Me.sscTest_7.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.sscTest_7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.sscTest_7.ImageKey = "HF"
        Me.sscTest_7.ImageList = Me.imlButtons
        Me.sscTest_7.Location = New System.Drawing.Point(53, 224)
        Me.sscTest_7.Margin = New System.Windows.Forms.Padding(0)
        Me.sscTest_7.Name = "sscTest_7"
        Me.sscTest_7.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.sscTest_7.Size = New System.Drawing.Size(23, 23)
        Me.sscTest_7.TabIndex = 118
        Me.ToolTip1.SetToolTip(Me.sscTest_7, "Start HF Switch Test")
        Me.sscTest_7.UseVisualStyleBackColor = False
        '
        'PassFrame_1
        '
        Me.PassFrame_1.BackColor = System.Drawing.SystemColors.Control
        Me.PassFrame_1.Image = CType(resources.GetObject("PassFrame_1.Image"), System.Drawing.Image)
        Me.PassFrame_1.Location = New System.Drawing.Point(12, 86)
        Me.PassFrame_1.Name = "PassFrame_1"
        Me.PassFrame_1.Size = New System.Drawing.Size(32, 16)
        Me.PassFrame_1.TabIndex = 44
        Me.PassFrame_1.TabStop = False
        '
        'InstrumentLabel_1
        '
        Me.InstrumentLabel_1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.InstrumentLabel_1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InstrumentLabel_1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.InstrumentLabel_1.Location = New System.Drawing.Point(85, 86)
        Me.InstrumentLabel_1.Name = "InstrumentLabel_1"
        Me.InstrumentLabel_1.Size = New System.Drawing.Size(209, 17)
        Me.InstrumentLabel_1.TabIndex = 45
        Me.InstrumentLabel_1.Text = "MIL-STD-1553 Interface (CIC Slot8)"
        '
        'PassFrame_2
        '
        Me.PassFrame_2.BackColor = System.Drawing.SystemColors.Control
        Me.PassFrame_2.Image = CType(resources.GetObject("PassFrame_2.Image"), System.Drawing.Image)
        Me.PassFrame_2.Location = New System.Drawing.Point(12, 109)
        Me.PassFrame_2.Name = "PassFrame_2"
        Me.PassFrame_2.Size = New System.Drawing.Size(32, 16)
        Me.PassFrame_2.TabIndex = 47
        Me.PassFrame_2.TabStop = False
        '
        'InstrumentLabel_2
        '
        Me.InstrumentLabel_2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.InstrumentLabel_2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InstrumentLabel_2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.InstrumentLabel_2.Location = New System.Drawing.Point(85, 109)
        Me.InstrumentLabel_2.Name = "InstrumentLabel_2"
        Me.InstrumentLabel_2.Size = New System.Drawing.Size(220, 17)
        Me.InstrumentLabel_2.TabIndex = 48
        Me.InstrumentLabel_2.Text = "Digital Test Subsystem (PC Slots 1-4)"
        '
        'PassFrame_3
        '
        Me.PassFrame_3.BackColor = System.Drawing.SystemColors.Control
        Me.PassFrame_3.Image = CType(resources.GetObject("PassFrame_3.Image"), System.Drawing.Image)
        Me.PassFrame_3.Location = New System.Drawing.Point(12, 132)
        Me.PassFrame_3.Name = "PassFrame_3"
        Me.PassFrame_3.Size = New System.Drawing.Size(32, 16)
        Me.PassFrame_3.TabIndex = 50
        Me.PassFrame_3.TabStop = False
        '
        'InstrumentLabel_3
        '
        Me.InstrumentLabel_3.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.InstrumentLabel_3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InstrumentLabel_3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.InstrumentLabel_3.Location = New System.Drawing.Point(85, 132)
        Me.InstrumentLabel_3.Name = "InstrumentLabel_3"
        Me.InstrumentLabel_3.Size = New System.Drawing.Size(209, 17)
        Me.InstrumentLabel_3.TabIndex = 51
        Me.InstrumentLabel_3.Text = "Low Frequency Switches #1 (PC Slot 5)"
        '
        'PassFrame_4
        '
        Me.PassFrame_4.BackColor = System.Drawing.SystemColors.Control
        Me.PassFrame_4.Image = CType(resources.GetObject("PassFrame_4.Image"), System.Drawing.Image)
        Me.PassFrame_4.Location = New System.Drawing.Point(12, 155)
        Me.PassFrame_4.Name = "PassFrame_4"
        Me.PassFrame_4.Size = New System.Drawing.Size(32, 16)
        Me.PassFrame_4.TabIndex = 53
        Me.PassFrame_4.TabStop = False
        '
        'InstrumentLabel_4
        '
        Me.InstrumentLabel_4.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.InstrumentLabel_4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InstrumentLabel_4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.InstrumentLabel_4.Location = New System.Drawing.Point(85, 155)
        Me.InstrumentLabel_4.Name = "InstrumentLabel_4"
        Me.InstrumentLabel_4.Size = New System.Drawing.Size(209, 17)
        Me.InstrumentLabel_4.TabIndex = 54
        Me.InstrumentLabel_4.Text = "Low Frequency Switches #2 (PC Slot 6)"
        '
        'PassFrame_5
        '
        Me.PassFrame_5.BackColor = System.Drawing.SystemColors.Control
        Me.PassFrame_5.Image = CType(resources.GetObject("PassFrame_5.Image"), System.Drawing.Image)
        Me.PassFrame_5.Location = New System.Drawing.Point(12, 178)
        Me.PassFrame_5.Name = "PassFrame_5"
        Me.PassFrame_5.Size = New System.Drawing.Size(32, 16)
        Me.PassFrame_5.TabIndex = 56
        Me.PassFrame_5.TabStop = False
        '
        'InstrumentLabel_5
        '
        Me.InstrumentLabel_5.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.InstrumentLabel_5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InstrumentLabel_5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.InstrumentLabel_5.Location = New System.Drawing.Point(85, 178)
        Me.InstrumentLabel_5.Name = "InstrumentLabel_5"
        Me.InstrumentLabel_5.Size = New System.Drawing.Size(209, 17)
        Me.InstrumentLabel_5.TabIndex = 57
        Me.InstrumentLabel_5.Text = "Low Frequency Switches $3 (PC Slot 7)"
        '
        'PassFrame_6
        '
        Me.PassFrame_6.BackColor = System.Drawing.SystemColors.Control
        Me.PassFrame_6.Image = CType(resources.GetObject("PassFrame_6.Image"), System.Drawing.Image)
        Me.PassFrame_6.Location = New System.Drawing.Point(12, 201)
        Me.PassFrame_6.Name = "PassFrame_6"
        Me.PassFrame_6.Size = New System.Drawing.Size(32, 16)
        Me.PassFrame_6.TabIndex = 59
        Me.PassFrame_6.TabStop = False
        '
        'InstrumentLabel_6
        '
        Me.InstrumentLabel_6.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.InstrumentLabel_6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InstrumentLabel_6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.InstrumentLabel_6.Location = New System.Drawing.Point(85, 201)
        Me.InstrumentLabel_6.Name = "InstrumentLabel_6"
        Me.InstrumentLabel_6.Size = New System.Drawing.Size(209, 17)
        Me.InstrumentLabel_6.TabIndex = 60
        Me.InstrumentLabel_6.Text = "Medium Frequency Switches (PC Slot 8)"
        '
        'PassFrame_13
        '
        Me.PassFrame_13.BackColor = System.Drawing.SystemColors.Control
        Me.PassFrame_13.Image = CType(resources.GetObject("PassFrame_13.Image"), System.Drawing.Image)
        Me.PassFrame_13.Location = New System.Drawing.Point(311, 63)
        Me.PassFrame_13.Name = "PassFrame_13"
        Me.PassFrame_13.Size = New System.Drawing.Size(32, 16)
        Me.PassFrame_13.TabIndex = 62
        Me.PassFrame_13.TabStop = False
        '
        'InstrumentLabel_13
        '
        Me.InstrumentLabel_13.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.InstrumentLabel_13.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InstrumentLabel_13.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.InstrumentLabel_13.Location = New System.Drawing.Point(384, 63)
        Me.InstrumentLabel_13.Name = "InstrumentLabel_13"
        Me.InstrumentLabel_13.Size = New System.Drawing.Size(240, 17)
        Me.InstrumentLabel_13.TabIndex = 63
        Me.InstrumentLabel_13.Text = "Programmable Power Unit (PPU Slot C,D)"
        '
        'PassFrame_14
        '
        Me.PassFrame_14.BackColor = System.Drawing.SystemColors.Control
        Me.PassFrame_14.Image = CType(resources.GetObject("PassFrame_14.Image"), System.Drawing.Image)
        Me.PassFrame_14.Location = New System.Drawing.Point(311, 86)
        Me.PassFrame_14.Name = "PassFrame_14"
        Me.PassFrame_14.Size = New System.Drawing.Size(32, 16)
        Me.PassFrame_14.TabIndex = 65
        Me.PassFrame_14.TabStop = False
        '
        'InstrumentLabel_14
        '
        Me.InstrumentLabel_14.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.InstrumentLabel_14.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InstrumentLabel_14.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.InstrumentLabel_14.Location = New System.Drawing.Point(384, 86)
        Me.InstrumentLabel_14.Name = "InstrumentLabel_14"
        Me.InstrumentLabel_14.Size = New System.Drawing.Size(209, 17)
        Me.InstrumentLabel_14.TabIndex = 66
        Me.InstrumentLabel_14.Text = "Synchro/Resolver (SC Slot 12)"
        '
        'PassFrame_15
        '
        Me.PassFrame_15.BackColor = System.Drawing.SystemColors.Control
        Me.PassFrame_15.Image = CType(resources.GetObject("PassFrame_15.Image"), System.Drawing.Image)
        Me.PassFrame_15.Location = New System.Drawing.Point(311, 109)
        Me.PassFrame_15.Name = "PassFrame_15"
        Me.PassFrame_15.Size = New System.Drawing.Size(32, 16)
        Me.PassFrame_15.TabIndex = 68
        Me.PassFrame_15.TabStop = False
        '
        'InstrumentLabel_15
        '
        Me.InstrumentLabel_15.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.InstrumentLabel_15.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InstrumentLabel_15.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.InstrumentLabel_15.Location = New System.Drawing.Point(384, 109)
        Me.InstrumentLabel_15.Name = "InstrumentLabel_15"
        Me.InstrumentLabel_15.Size = New System.Drawing.Size(209, 17)
        Me.InstrumentLabel_15.TabIndex = 69
        Me.InstrumentLabel_15.Text = "Serial Bus RS232/422/485 (CIC Slot 3)"
        '
        'PassFrame_16
        '
        Me.PassFrame_16.BackColor = System.Drawing.SystemColors.Control
        Me.PassFrame_16.Image = CType(resources.GetObject("PassFrame_16.Image"), System.Drawing.Image)
        Me.PassFrame_16.Location = New System.Drawing.Point(311, 132)
        Me.PassFrame_16.Name = "PassFrame_16"
        Me.PassFrame_16.Size = New System.Drawing.Size(32, 16)
        Me.PassFrame_16.TabIndex = 71
        Me.PassFrame_16.TabStop = False
        '
        'InstrumentLabel_16
        '
        Me.InstrumentLabel_16.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.InstrumentLabel_16.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InstrumentLabel_16.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.InstrumentLabel_16.Location = New System.Drawing.Point(384, 132)
        Me.InstrumentLabel_16.Name = "InstrumentLabel_16"
        Me.InstrumentLabel_16.Size = New System.Drawing.Size(209, 17)
        Me.InstrumentLabel_16.TabIndex = 72
        Me.InstrumentLabel_16.Text = "RS422 COM1 (CIC Slot 3)"
        '
        'PassFrame_17
        '
        Me.PassFrame_17.BackColor = System.Drawing.SystemColors.Control
        Me.PassFrame_17.Image = CType(resources.GetObject("PassFrame_17.Image"), System.Drawing.Image)
        Me.PassFrame_17.Location = New System.Drawing.Point(311, 155)
        Me.PassFrame_17.Name = "PassFrame_17"
        Me.PassFrame_17.Size = New System.Drawing.Size(32, 16)
        Me.PassFrame_17.TabIndex = 74
        Me.PassFrame_17.TabStop = False
        '
        'InstrumentLabel_17
        '
        Me.InstrumentLabel_17.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.InstrumentLabel_17.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InstrumentLabel_17.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.InstrumentLabel_17.Location = New System.Drawing.Point(384, 155)
        Me.InstrumentLabel_17.Margin = New System.Windows.Forms.Padding(0)
        Me.InstrumentLabel_17.Name = "InstrumentLabel_17"
        Me.InstrumentLabel_17.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.InstrumentLabel_17.Size = New System.Drawing.Size(209, 17)
        Me.InstrumentLabel_17.TabIndex = 75
        Me.InstrumentLabel_17.Text = "RS232 COM2 (CIC Slot 1)"
        '
        'PassFrame_18
        '
        Me.PassFrame_18.BackColor = System.Drawing.SystemColors.Control
        Me.PassFrame_18.Image = CType(resources.GetObject("PassFrame_18.Image"), System.Drawing.Image)
        Me.PassFrame_18.Location = New System.Drawing.Point(311, 178)
        Me.PassFrame_18.Name = "PassFrame_18"
        Me.PassFrame_18.Size = New System.Drawing.Size(32, 16)
        Me.PassFrame_18.TabIndex = 77
        Me.PassFrame_18.TabStop = False
        '
        'InstrumentLabel_18
        '
        Me.InstrumentLabel_18.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.InstrumentLabel_18.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InstrumentLabel_18.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.InstrumentLabel_18.Location = New System.Drawing.Point(384, 178)
        Me.InstrumentLabel_18.Margin = New System.Windows.Forms.Padding(0)
        Me.InstrumentLabel_18.Name = "InstrumentLabel_18"
        Me.InstrumentLabel_18.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.InstrumentLabel_18.Size = New System.Drawing.Size(209, 17)
        Me.InstrumentLabel_18.TabIndex = 78
        Me.InstrumentLabel_18.Text = "Gigabit Ethernet (CIC Slot10)"
        '
        'PassFrame_19
        '
        Me.PassFrame_19.BackColor = System.Drawing.SystemColors.Control
        Me.PassFrame_19.Image = CType(resources.GetObject("PassFrame_19.Image"), System.Drawing.Image)
        Me.PassFrame_19.Location = New System.Drawing.Point(311, 201)
        Me.PassFrame_19.Name = "PassFrame_19"
        Me.PassFrame_19.Size = New System.Drawing.Size(32, 16)
        Me.PassFrame_19.TabIndex = 80
        Me.PassFrame_19.TabStop = False
        '
        'InstrumentLabel_19
        '
        Me.InstrumentLabel_19.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.InstrumentLabel_19.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InstrumentLabel_19.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.InstrumentLabel_19.Location = New System.Drawing.Point(384, 201)
        Me.InstrumentLabel_19.Margin = New System.Windows.Forms.Padding(0)
        Me.InstrumentLabel_19.Name = "InstrumentLabel_19"
        Me.InstrumentLabel_19.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.InstrumentLabel_19.Size = New System.Drawing.Size(209, 17)
        Me.InstrumentLabel_19.TabIndex = 81
        Me.InstrumentLabel_19.Text = "CAN Bus (CIC Slot 4)"
        '
        'PassFrame_21
        '
        Me.PassFrame_21.BackColor = System.Drawing.SystemColors.Control
        Me.PassFrame_21.Image = CType(resources.GetObject("PassFrame_21.Image"), System.Drawing.Image)
        Me.PassFrame_21.Location = New System.Drawing.Point(311, 247)
        Me.PassFrame_21.Name = "PassFrame_21"
        Me.PassFrame_21.Size = New System.Drawing.Size(32, 16)
        Me.PassFrame_21.TabIndex = 86
        Me.PassFrame_21.TabStop = False
        '
        'InstrumentLabel_21
        '
        Me.InstrumentLabel_21.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.InstrumentLabel_21.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InstrumentLabel_21.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.InstrumentLabel_21.Location = New System.Drawing.Point(384, 247)
        Me.InstrumentLabel_21.Margin = New System.Windows.Forms.Padding(0)
        Me.InstrumentLabel_21.Name = "InstrumentLabel_21"
        Me.InstrumentLabel_21.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.InstrumentLabel_21.Size = New System.Drawing.Size(252, 17)
        Me.InstrumentLabel_21.TabIndex = 87
        Me.InstrumentLabel_21.Text = "RF Power Meter (SC Slot Slot 11)"
        '
        'PassFrame_7
        '
        Me.PassFrame_7.BackColor = System.Drawing.SystemColors.Control
        Me.PassFrame_7.Image = CType(resources.GetObject("PassFrame_7.Image"), System.Drawing.Image)
        Me.PassFrame_7.Location = New System.Drawing.Point(12, 224)
        Me.PassFrame_7.Name = "PassFrame_7"
        Me.PassFrame_7.Size = New System.Drawing.Size(32, 16)
        Me.PassFrame_7.TabIndex = 83
        Me.PassFrame_7.TabStop = False
        '
        'InstrumentLabel_7
        '
        Me.InstrumentLabel_7.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.InstrumentLabel_7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InstrumentLabel_7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.InstrumentLabel_7.Location = New System.Drawing.Point(85, 224)
        Me.InstrumentLabel_7.Name = "InstrumentLabel_7"
        Me.InstrumentLabel_7.Size = New System.Drawing.Size(209, 17)
        Me.InstrumentLabel_7.TabIndex = 84
        Me.InstrumentLabel_7.Text = "High Frequency Switches (PC Slot 9)"
        '
        'PassFrame_22
        '
        Me.PassFrame_22.BackColor = System.Drawing.SystemColors.Control
        Me.PassFrame_22.Image = CType(resources.GetObject("PassFrame_22.Image"), System.Drawing.Image)
        Me.PassFrame_22.Location = New System.Drawing.Point(311, 270)
        Me.PassFrame_22.Name = "PassFrame_22"
        Me.PassFrame_22.Size = New System.Drawing.Size(32, 16)
        Me.PassFrame_22.TabIndex = 92
        Me.PassFrame_22.TabStop = False
        '
        'InstrumentLabel_22
        '
        Me.InstrumentLabel_22.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.InstrumentLabel_22.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InstrumentLabel_22.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.InstrumentLabel_22.Location = New System.Drawing.Point(384, 270)
        Me.InstrumentLabel_22.Name = "InstrumentLabel_22"
        Me.InstrumentLabel_22.Size = New System.Drawing.Size(252, 17)
        Me.InstrumentLabel_22.TabIndex = 93
        Me.InstrumentLabel_22.Text = "RF Counter (SC Slot 9)"
        '
        'PassFrame_8
        '
        Me.PassFrame_8.BackColor = System.Drawing.SystemColors.Control
        Me.PassFrame_8.Image = CType(resources.GetObject("PassFrame_8.Image"), System.Drawing.Image)
        Me.PassFrame_8.Location = New System.Drawing.Point(12, 247)
        Me.PassFrame_8.Name = "PassFrame_8"
        Me.PassFrame_8.Size = New System.Drawing.Size(32, 16)
        Me.PassFrame_8.TabIndex = 89
        Me.PassFrame_8.TabStop = False
        '
        'InstrumentLabel_8
        '
        Me.InstrumentLabel_8.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.InstrumentLabel_8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InstrumentLabel_8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.InstrumentLabel_8.Location = New System.Drawing.Point(85, 247)
        Me.InstrumentLabel_8.Name = "InstrumentLabel_8"
        Me.InstrumentLabel_8.Size = New System.Drawing.Size(209, 17)
        Me.InstrumentLabel_8.TabIndex = 90
        Me.InstrumentLabel_8.Text = "Digital Multimeter (PC Slot 11)"
        '
        'PassFrame_23
        '
        Me.PassFrame_23.BackColor = System.Drawing.SystemColors.Control
        Me.PassFrame_23.Image = CType(resources.GetObject("PassFrame_23.Image"), System.Drawing.Image)
        Me.PassFrame_23.Location = New System.Drawing.Point(311, 293)
        Me.PassFrame_23.Name = "PassFrame_23"
        Me.PassFrame_23.Size = New System.Drawing.Size(32, 16)
        Me.PassFrame_23.TabIndex = 98
        Me.PassFrame_23.TabStop = False
        '
        'InstrumentLabel_23
        '
        Me.InstrumentLabel_23.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.InstrumentLabel_23.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InstrumentLabel_23.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.InstrumentLabel_23.Location = New System.Drawing.Point(384, 293)
        Me.InstrumentLabel_23.Name = "InstrumentLabel_23"
        Me.InstrumentLabel_23.Size = New System.Drawing.Size(252, 17)
        Me.InstrumentLabel_23.TabIndex = 99
        Me.InstrumentLabel_23.Text = "RF Down Converter (SC Slot 7)"
        '
        'PassFrame_9
        '
        Me.PassFrame_9.BackColor = System.Drawing.SystemColors.Control
        Me.PassFrame_9.Image = CType(resources.GetObject("PassFrame_9.Image"), System.Drawing.Image)
        Me.PassFrame_9.Location = New System.Drawing.Point(12, 270)
        Me.PassFrame_9.Name = "PassFrame_9"
        Me.PassFrame_9.Size = New System.Drawing.Size(32, 16)
        Me.PassFrame_9.TabIndex = 95
        Me.PassFrame_9.TabStop = False
        '
        'InstrumentLabel_9
        '
        Me.InstrumentLabel_9.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.InstrumentLabel_9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InstrumentLabel_9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.InstrumentLabel_9.Location = New System.Drawing.Point(85, 270)
        Me.InstrumentLabel_9.Name = "InstrumentLabel_9"
        Me.InstrumentLabel_9.Size = New System.Drawing.Size(209, 17)
        Me.InstrumentLabel_9.TabIndex = 96
        Me.InstrumentLabel_9.Text = "Digitizing Oscilloscope (SC Slot 1)"
        '
        'PassFrame_10
        '
        Me.PassFrame_10.BackColor = System.Drawing.SystemColors.Control
        Me.PassFrame_10.Image = CType(resources.GetObject("PassFrame_10.Image"), System.Drawing.Image)
        Me.PassFrame_10.Location = New System.Drawing.Point(12, 293)
        Me.PassFrame_10.Name = "PassFrame_10"
        Me.PassFrame_10.Size = New System.Drawing.Size(32, 16)
        Me.PassFrame_10.TabIndex = 101
        Me.PassFrame_10.TabStop = False
        '
        'InstrumentLabel_10
        '
        Me.InstrumentLabel_10.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.InstrumentLabel_10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InstrumentLabel_10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.InstrumentLabel_10.Location = New System.Drawing.Point(85, 293)
        Me.InstrumentLabel_10.Name = "InstrumentLabel_10"
        Me.InstrumentLabel_10.Size = New System.Drawing.Size(209, 17)
        Me.InstrumentLabel_10.TabIndex = 102
        Me.InstrumentLabel_10.Text = "Counter/Timer (SC Slot 2)"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.SystemColors.ControlLight
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(24, 16)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(130, 17)
        Me.Label7.TabIndex = 14
        Me.Label7.Text = "Enter Search Word:"
        '
        'TxtSearch
        '
        Me.TxtSearch.BackColor = System.Drawing.SystemColors.Window
        Me.TxtSearch.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtSearch.Location = New System.Drawing.Point(24, 40)
        Me.TxtSearch.Name = "TxtSearch"
        Me.TxtSearch.Size = New System.Drawing.Size(195, 20)
        Me.TxtSearch.TabIndex = 15
        '
        'CmdSearchOK
        '
        Me.CmdSearchOK.BackColor = System.Drawing.SystemColors.Control
        Me.CmdSearchOK.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSearchOK.Location = New System.Drawing.Point(136, 81)
        Me.CmdSearchOK.Name = "CmdSearchOK"
        Me.CmdSearchOK.Size = New System.Drawing.Size(66, 25)
        Me.CmdSearchOK.TabIndex = 16
        Me.CmdSearchOK.Text = "OK"
        Me.CmdSearchOK.UseVisualStyleBackColor = False
        '
        'CmdSearchCancel
        '
        Me.CmdSearchCancel.BackColor = System.Drawing.SystemColors.Control
        Me.CmdSearchCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSearchCancel.Location = New System.Drawing.Point(49, 81)
        Me.CmdSearchCancel.Name = "CmdSearchCancel"
        Me.CmdSearchCancel.Size = New System.Drawing.Size(66, 25)
        Me.CmdSearchCancel.TabIndex = 17
        Me.CmdSearchCancel.Text = "Cancel"
        Me.CmdSearchCancel.UseVisualStyleBackColor = False
        '
        'FrSearch
        '
        Me.FrSearch.BackColor = System.Drawing.SystemColors.ControlLight
        Me.FrSearch.Controls.Add(Me.CmdSearchCancel)
        Me.FrSearch.Controls.Add(Me.CmdSearchOK)
        Me.FrSearch.Controls.Add(Me.TxtSearch)
        Me.FrSearch.Controls.Add(Me.Label7)
        Me.FrSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FrSearch.Location = New System.Drawing.Point(349, 458)
        Me.FrSearch.Name = "FrSearch"
        Me.FrSearch.Size = New System.Drawing.Size(244, 114)
        Me.FrSearch.TabIndex = 13
        Me.FrSearch.TabStop = False
        Me.FrSearch.Visible = False
        '
        'sspSTestPanel
        '
        Me.sspSTestPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.sspSTestPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.sspSTestPanel.Location = New System.Drawing.Point(0, 57)
        Me.sspSTestPanel.Name = "sspSTestPanel"
        Me.sspSTestPanel.Size = New System.Drawing.Size(649, 314)
        Me.sspSTestPanel.TabIndex = 5
        Me.sspSTestPanel.TabStop = False
        '
        'PassFrame_20
        '
        Me.PassFrame_20.BackColor = System.Drawing.SystemColors.Control
        Me.PassFrame_20.Image = CType(resources.GetObject("PassFrame_20.Image"), System.Drawing.Image)
        Me.PassFrame_20.Location = New System.Drawing.Point(311, 224)
        Me.PassFrame_20.Name = "PassFrame_20"
        Me.PassFrame_20.Size = New System.Drawing.Size(32, 16)
        Me.PassFrame_20.TabIndex = 104
        Me.PassFrame_20.TabStop = False
        '
        'InstrumentLabel_20
        '
        Me.InstrumentLabel_20.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.InstrumentLabel_20.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InstrumentLabel_20.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.InstrumentLabel_20.Location = New System.Drawing.Point(384, 224)
        Me.InstrumentLabel_20.Margin = New System.Windows.Forms.Padding(0)
        Me.InstrumentLabel_20.Name = "InstrumentLabel_20"
        Me.InstrumentLabel_20.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.InstrumentLabel_20.Size = New System.Drawing.Size(209, 17)
        Me.InstrumentLabel_20.TabIndex = 105
        Me.InstrumentLabel_20.Text = "Analog Probe (Receiver P2)"
        '
        'PassFrame_25
        '
        Me.PassFrame_25.BackColor = System.Drawing.SystemColors.Control
        Me.PassFrame_25.Image = CType(resources.GetObject("PassFrame_25.Image"), System.Drawing.Image)
        Me.PassFrame_25.Location = New System.Drawing.Point(311, 339)
        Me.PassFrame_25.Name = "PassFrame_25"
        Me.PassFrame_25.Size = New System.Drawing.Size(32, 16)
        Me.PassFrame_25.TabIndex = 116
        Me.PassFrame_25.TabStop = False
        '
        'InstrumentLabel_25
        '
        Me.InstrumentLabel_25.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.InstrumentLabel_25.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InstrumentLabel_25.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.InstrumentLabel_25.Location = New System.Drawing.Point(384, 339)
        Me.InstrumentLabel_25.Name = "InstrumentLabel_25"
        Me.InstrumentLabel_25.Size = New System.Drawing.Size(209, 17)
        Me.InstrumentLabel_25.TabIndex = 117
        Me.InstrumentLabel_25.Text = "RF Synthesizer (SC Slot5,6)"
        '
        'PassFrame_12
        '
        Me.PassFrame_12.BackColor = System.Drawing.SystemColors.Control
        Me.PassFrame_12.Image = CType(resources.GetObject("PassFrame_12.Image"), System.Drawing.Image)
        Me.PassFrame_12.Location = New System.Drawing.Point(12, 339)
        Me.PassFrame_12.Name = "PassFrame_12"
        Me.PassFrame_12.Size = New System.Drawing.Size(32, 16)
        Me.PassFrame_12.TabIndex = 113
        Me.PassFrame_12.TabStop = False
        '
        'InstrumentLabel_12
        '
        Me.InstrumentLabel_12.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.InstrumentLabel_12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InstrumentLabel_12.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.InstrumentLabel_12.Location = New System.Drawing.Point(85, 339)
        Me.InstrumentLabel_12.Name = "InstrumentLabel_12"
        Me.InstrumentLabel_12.Size = New System.Drawing.Size(209, 17)
        Me.InstrumentLabel_12.TabIndex = 114
        Me.InstrumentLabel_12.Text = "Function Generator (SC Slot 3)"
        '
        'PassFrame_24
        '
        Me.PassFrame_24.BackColor = System.Drawing.SystemColors.Control
        Me.PassFrame_24.Image = CType(resources.GetObject("PassFrame_24.Image"), System.Drawing.Image)
        Me.PassFrame_24.Location = New System.Drawing.Point(311, 316)
        Me.PassFrame_24.Name = "PassFrame_24"
        Me.PassFrame_24.Size = New System.Drawing.Size(32, 16)
        Me.PassFrame_24.TabIndex = 110
        Me.PassFrame_24.TabStop = False
        '
        'InstrumentLabel_24
        '
        Me.InstrumentLabel_24.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.InstrumentLabel_24.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InstrumentLabel_24.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.InstrumentLabel_24.Location = New System.Drawing.Point(384, 316)
        Me.InstrumentLabel_24.Name = "InstrumentLabel_24"
        Me.InstrumentLabel_24.Size = New System.Drawing.Size(209, 17)
        Me.InstrumentLabel_24.TabIndex = 111
        Me.InstrumentLabel_24.Text = "RF Modulation Analyzer (Slot 10)"
        '
        'PassFrame_11
        '
        Me.PassFrame_11.BackColor = System.Drawing.SystemColors.Control
        Me.PassFrame_11.Image = CType(resources.GetObject("PassFrame_11.Image"), System.Drawing.Image)
        Me.PassFrame_11.Location = New System.Drawing.Point(12, 316)
        Me.PassFrame_11.Name = "PassFrame_11"
        Me.PassFrame_11.Size = New System.Drawing.Size(32, 16)
        Me.PassFrame_11.TabIndex = 107
        Me.PassFrame_11.TabStop = False
        '
        'InstrumentLabel_11
        '
        Me.InstrumentLabel_11.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.InstrumentLabel_11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InstrumentLabel_11.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.InstrumentLabel_11.Location = New System.Drawing.Point(85, 316)
        Me.InstrumentLabel_11.Name = "InstrumentLabel_11"
        Me.InstrumentLabel_11.Size = New System.Drawing.Size(209, 17)
        Me.InstrumentLabel_11.TabIndex = 108
        Me.InstrumentLabel_11.Text = "Arbitrary Waveform Gen (PC Slot 12)"
        '
        'frmSTest
        '
        Me.BackColor = System.Drawing.Color.SteelBlue
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(648, 446)
        Me.Controls.Add(Me.FrPPUStart)
        Me.Controls.Add(Me.FrSearch)
        Me.Controls.Add(Me.PassFrame_0)
        Me.Controls.Add(Me.PassFrame_1)
        Me.Controls.Add(Me.PassFrame_2)
        Me.Controls.Add(Me.PassFrame_3)
        Me.Controls.Add(Me.PassFrame_4)
        Me.Controls.Add(Me.PassFrame_5)
        Me.Controls.Add(Me.PassFrame_6)
        Me.Controls.Add(Me.PassFrame_7)
        Me.Controls.Add(Me.PassFrame_8)
        Me.Controls.Add(Me.PassFrame_9)
        Me.Controls.Add(Me.PassFrame_10)
        Me.Controls.Add(Me.PassFrame_11)
        Me.Controls.Add(Me.PassFrame_12)
        Me.Controls.Add(Me.PassFrame_13)
        Me.Controls.Add(Me.PassFrame_14)
        Me.Controls.Add(Me.PassFrame_15)
        Me.Controls.Add(Me.PassFrame_16)
        Me.Controls.Add(Me.PassFrame_17)
        Me.Controls.Add(Me.PassFrame_18)
        Me.Controls.Add(Me.PassFrame_19)
        Me.Controls.Add(Me.PassFrame_20)
        Me.Controls.Add(Me.PassFrame_21)
        Me.Controls.Add(Me.PassFrame_22)
        Me.Controls.Add(Me.PassFrame_23)
        Me.Controls.Add(Me.PassFrame_24)
        Me.Controls.Add(Me.PassFrame_25)
        Me.Controls.Add(Me.picNoTest)
        Me.Controls.Add(Me.picTestPassed)
        Me.Controls.Add(Me.picTestFailed)
        Me.Controls.Add(Me.picTestUnknown)
        Me.Controls.Add(Me.sscTest_0)
        Me.Controls.Add(Me.sscTest_1)
        Me.Controls.Add(Me.sscTest_2)
        Me.Controls.Add(Me.sscTest_3)
        Me.Controls.Add(Me.sscTest_4)
        Me.Controls.Add(Me.sscTest_5)
        Me.Controls.Add(Me.sscTest_6)
        Me.Controls.Add(Me.sscTest_7)
        Me.Controls.Add(Me.sscTest_8)
        Me.Controls.Add(Me.sscTest_9)
        Me.Controls.Add(Me.sscTest_10)
        Me.Controls.Add(Me.sscTest_11)
        Me.Controls.Add(Me.sscTest_12)
        Me.Controls.Add(Me.sscTest_13)
        Me.Controls.Add(Me.sscTest_14)
        Me.Controls.Add(Me.sscTest_15)
        Me.Controls.Add(Me.sscTest_16)
        Me.Controls.Add(Me.sscTest_17)
        Me.Controls.Add(Me.sscTest_18)
        Me.Controls.Add(Me.sscTest_19)
        Me.Controls.Add(Me.sscTest_20)
        Me.Controls.Add(Me.sscTest_21)
        Me.Controls.Add(Me.sscTest_22)
        Me.Controls.Add(Me.sscTest_23)
        Me.Controls.Add(Me.sscTest_24)
        Me.Controls.Add(Me.sscTest_25)
        Me.Controls.Add(Me.InstrumentLabel_0)
        Me.Controls.Add(Me.InstrumentLabel_1)
        Me.Controls.Add(Me.InstrumentLabel_2)
        Me.Controls.Add(Me.InstrumentLabel_3)
        Me.Controls.Add(Me.InstrumentLabel_4)
        Me.Controls.Add(Me.InstrumentLabel_5)
        Me.Controls.Add(Me.InstrumentLabel_6)
        Me.Controls.Add(Me.InstrumentLabel_7)
        Me.Controls.Add(Me.InstrumentLabel_8)
        Me.Controls.Add(Me.InstrumentLabel_9)
        Me.Controls.Add(Me.InstrumentLabel_10)
        Me.Controls.Add(Me.InstrumentLabel_11)
        Me.Controls.Add(Me.InstrumentLabel_12)
        Me.Controls.Add(Me.InstrumentLabel_13)
        Me.Controls.Add(Me.InstrumentLabel_14)
        Me.Controls.Add(Me.InstrumentLabel_15)
        Me.Controls.Add(Me.InstrumentLabel_16)
        Me.Controls.Add(Me.InstrumentLabel_17)
        Me.Controls.Add(Me.InstrumentLabel_18)
        Me.Controls.Add(Me.InstrumentLabel_19)
        Me.Controls.Add(Me.InstrumentLabel_20)
        Me.Controls.Add(Me.InstrumentLabel_21)
        Me.Controls.Add(Me.InstrumentLabel_22)
        Me.Controls.Add(Me.InstrumentLabel_23)
        Me.Controls.Add(Me.InstrumentLabel_24)
        Me.Controls.Add(Me.InstrumentLabel_25)
        Me.Controls.Add(Me.DetailsText)
        Me.Controls.Add(Me.proProgress)
        Me.Controls.Add(Me.cmdDetails)
        Me.Controls.Add(Me.cmdClose)
        Me.Controls.Add(Me.Frame3)
        Me.Controls.Add(Me.cmdHelp)
        Me.Controls.Add(Me.sbrUserInformation)
        Me.Controls.Add(Me.sspSTestPanel)
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmSTest"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ATS System Self Test"
        CType(Me.PassFrame_0, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FrPPUStart.ResumeLayout(False)
        Me.FrPPUStart.PerformLayout()
        Me.Frame3.ResumeLayout(False)
        CType(Me.picNoTest, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picTestPassed, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picTestFailed, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picTestUnknown, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sbrUserInformation_Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PassFrame_1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PassFrame_2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PassFrame_3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PassFrame_4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PassFrame_5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PassFrame_6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PassFrame_13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PassFrame_14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PassFrame_15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PassFrame_16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PassFrame_17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PassFrame_18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PassFrame_19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PassFrame_21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PassFrame_7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PassFrame_22, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PassFrame_8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PassFrame_23, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PassFrame_9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PassFrame_10, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FrSearch.ResumeLayout(False)
        Me.FrSearch.PerformLayout()
        CType(Me.sspSTestPanel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PassFrame_20, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PassFrame_25, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PassFrame_12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PassFrame_24, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PassFrame_11, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

End Sub

#End Region

    '**************************************************************
    '* Nomenclature   : ATS-TETS SYSTEM SELF TEST                 *
    '*                  Main Self Test Display                    *
    '* Version        : 2.0                                       *
    '* Last Update    : Apr 1, 2017                               *
    '* Purpose        : This form is the main self test display   *
    '                   This form is always open.                 *
    '**************************************************************

    Private Sub CmbSelectStep_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSelectStep.SelectedIndexChanged

        HelpContextID = 1010
        OptionStep = cmbSelectStep.SelectedIndex + 1

    End Sub

    Private Sub CmbSelectTest_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSelectTest.SelectedIndexChanged

        Dim i As Short, j As Short

        ' ''Added for Loop On Step
        ''sTestName(MIL_STD_1553) = "1553"
        ''sTestName(DIGITAL) = "DTS"
        ''sTestName(SWITCH1) = "LF1 Switch"
        ''sTestName(SWITCH2) = "LF2 Switch"
        ''sTestName(SWITCH3) = "LF3 Switch"
        ''sTestName(SWITCH4) = "MF Switch"
        ''sTestName(SWITCH5) = "HF Switch"
        ''sTestName(DMM) = "DMM"
        ''sTestName(OSCOPE) = "DSO"
        ''sTestName(COUNTER) = "C/T"
        ''sTestName(ARB) = "ARB"
        ''sTestName(FGEN) = "FGEN"
        ''sTestName(PPU) = "PPU"
        ''sTestName(SYN_RES) = "S/R"
        ''sTestName(SERIALCCA) = "PCI SERIAL"
        ''sTestName(RS422) = "RS422 COM1"
        ''sTestName(RS232) = "RS232 COM2"
        ''sTestName(GIGA) = "GIGABIT"
        ''sTestName(CANBUS) = "CANBUS"
        ''sTestName(APROBE) = "Analog Probe"
        ''sTestName(RFPM) = "RFPM"
        ''sTestName(RFCOUNTER) = "RFCT"
        ''sTestName(RFDOWN) = "RFDOWN"
        ''sTestName(RFMODANAL) = "RFMA"
        ''sTestName(RFSYN) = "RFSYN"

        ' Here, fill the cmdSelectStep combo box based on the particular Test
        HelpContextID = 1010
        cmbSelectStep.Items.Clear() ' clear select step combo box
        OptionTestName = cmbSelectTest.Items.Item(cmbSelectTest.SelectedIndex)

        Select Case OptionTestName
            Case "1553"
                cmbSelectStep.Items.Add("BUS-01-001")
                cmbSelectStep.Items.Add("BUS-02-001")
            Case "DTS" '29 steps
                'DTS-01-001 thru DTS-01-004
                For i = 1 To 4 : cmbSelectStep.Items.Add("DTS-01-" & Format(i, "000")) : Next i
                'DTS-02-001 thru DTS-12-001
                For i = 2 To 12 : cmbSelectStep.Items.Add("DTS-" & Format(i, "00") & "-001") : Next i

                cmbSelectStep.Items.Add("DTS-13-000 to 063") ' note: one step if all pass
                cmbSelectStep.Items.Add("DTS-13-064 to 127")
                cmbSelectStep.Items.Add("DTS-13-128 to 191")
                cmbSelectStep.Items.Add("DTS-14-001")
                ''note: remove saif

                cmbSelectStep.Items.Add("DTS-15-000 to 063")
                cmbSelectStep.Items.Add("DTS-16-000 to 063")
                cmbSelectStep.Items.Add("DTS-15-064 to 127")
                cmbSelectStep.Items.Add("DTS-16-064 to 127")
                cmbSelectStep.Items.Add("DTS-15-128 to 191")
                cmbSelectStep.Items.Add("DTS-16-128 to 191")
                cmbSelectStep.Items.Add("DTS-17-001")
                cmbSelectStep.Items.Add("DTS-18-001")
                cmbSelectStep.Items.Add("DTS-19-001")
                cmbSelectStep.Items.Add("DTS-20-001")
                ''note: reinstall saif

            Case "LF1 Switch" '207 steps
                For i = 1 To 6 : cmbSelectStep.Items.Add("LF1-01-" & Format(i, "000")) : Next i
                For i = 1 To 6 : cmbSelectStep.Items.Add("LF1-02-" & Format(i, "000")) : Next i
                For i = 1 To 21 : cmbSelectStep.Items.Add("LF1-03-" & Format(i, "000")) : Next i
                For i = 1 To 52 : cmbSelectStep.Items.Add("LF1-04-" & Format(i, "000")) : Next i
                For i = 1 To 127 : cmbSelectStep.Items.Add("LF1-05-" & Format(i, "000")) : Next i

            Case "LF2 Switch" ' 207 steps
                For i = 1 To 2 : cmbSelectStep.Items.Add("LF2-01-" & Format(i, "000")) : Next i
                For i = 1 To 5 : cmbSelectStep.Items.Add("LF2-02-" & Format(i, "000")) : Next i
                For i = 1 To 21 : cmbSelectStep.Items.Add("LF2-03-" & Format(i, "000")) : Next i
                For i = 1 To 52 : cmbSelectStep.Items.Add("LF2-04-" & Format(i, "000")) : Next i
                For i = 1 To 127 : cmbSelectStep.Items.Add("LF3-05-" & Format(i, "000")) : Next i

            Case "LF3 Switch" ' 258 steps
                For i = 1 To 2 : cmbSelectStep.Items.Add("LF3-01-" & Format(i, "000")) : Next i
                For i = 1 To 16 : cmbSelectStep.Items.Add("LF3-02-" & Format(i, "000")) : Next i
                For i = 1 To 48 : cmbSelectStep.Items.Add("LF3-03-" & Format(i, "000")) : Next i
                For i = 1 To 96 : cmbSelectStep.Items.Add("LF3-04-" & Format(i, "000")) : Next i
                For i = 1 To 96 : cmbSelectStep.Items.Add("LF3-05-" & Format(i, "000")) : Next i

            Case "MF Switch" '49 steps
                cmbSelectStep.Items.Add("MFS-01-001")
                For i = 1 To 24 : cmbSelectStep.Items.Add("MFS-02-" & Format(i, "000")) : Next i
                For i = 1 To 24 : cmbSelectStep.Items.Add("MFS-03-" & Format(i, "000")) : Next i

            Case "HF Switch" ' 56 steps
                cmbSelectStep.Items.Add("HFS-01-001")
                For i = 1 To 18 : cmbSelectStep.Items.Add("HFS-02-" & Format(i, "000")) : Next i
                For i = 1 To 18 : cmbSelectStep.Items.Add("HFS-03-" & Format(i, "000")) : Next i
                For i = 1 To 18 : cmbSelectStep.Items.Add("HFS-04-" & Format(i, "000")) : Next i

            Case "DMM"
                For i = 1 To 5 : cmbSelectStep.Items.Add("DMM-0" & Format(i, "0") & "-001") : Next i

            Case "DSO"
                For i = 1 To 8 : cmbSelectStep.Items.Add("DSO-0" & Format(i, "0") & "-001") : Next i

            Case "C/T"
                For i = 1 To 23 : cmbSelectStep.Items.Add("C/T-0" & Format(i, "0") & "-001") : Next i

            Case "ARB"
                For i = 1 To 9 : cmbSelectStep.Items.Add("ARB-0" & Format(i, "0") & "-001") : Next i

            Case "FGEN"
                For i = 1 To 5 : cmbSelectStep.Items.Add("FGN-0" & Format(i, "0") & "-001") : Next i

            Case "PPU"
                'P01-01-001 to P10-01-001 BIT test
                For i = 1 To 10
                    For j = 1 To 13
                        If j = 13 And (i = 1 Or i = 10) Then ' skip step 13 for DC1,DC10
                        Else
                            cmbSelectStep.Items.Add("P" & Format(i, "00") & "-" & Format(j, "00") & "-001")
                        End If
                    Next j
                Next i

            Case "S/R"
                cmbSelectStep.Items.Add("S/R-01-001")
                For i = 1 To 8 : cmbSelectStep.Items.Add("S/R-02-" & Format(i, "000")) : Next i
                For i = 1 To 8 : cmbSelectStep.Items.Add("S/R-03-" & Format(i, "000")) : Next i

            Case "PCI SERIAL"
                For i = 1 To 2 : cmbSelectStep.Items.Add("COM-03-" & Format(i, "000")) : Next i
                For i = 1 To 2 : cmbSelectStep.Items.Add("COM-04-" & Format(i, "000")) : Next i
                For i = 1 To 2 : cmbSelectStep.Items.Add("COM-05-" & Format(i, "000")) : Next i

            Case "RS422 COM1"
                For i = 1 To 2 : cmbSelectStep.Items.Add("COM-01-" & Format(i, "000")) : Next i

            Case "RS232 COM2"
                For i = 1 To 2 : cmbSelectStep.Items.Add("COM-02-" & Format(i, "000")) : Next i

            Case "GIGABIT"
                For i = 1 To 2 : cmbSelectStep.Items.Add("GIGA-01-" & Format(i, "000")) : Next i
                For i = 1 To 2 : cmbSelectStep.Items.Add("GIGA-02-" & Format(i, "000")) : Next i

            Case "CANBUS"
                For i = 1 To 7 : cmbSelectStep.Items.Add("CAN-01-" & Format(i, "000")) : Next i

            Case "Analog Probe"
                cmbSelectStep.Items.Add("Not Enabled")
                'NOT ENABLED For i = 1 To 4 : cmbSelectStep.Items.Add("APR-0" & Format(i, "0") & "-001") : Next i

            Case "RFPM"
                For i = 1 To 8 : cmbSelectStep.Items.Add("RFP-0" & Format(i, "0") & "-001") : Next i

            Case "RFCT"
                For i = 1 To 2 : cmbSelectStep.Items.Add("RFC-0" & Format(i, "0") & "-001") : Next i

            Case "RFDOWN"
                cmbSelectStep.Items.Add("RDC-01-001")
                For i = 1 To 53 : cmbSelectStep.Items.Add("RDC-02-" & Format(i, "000")) : Next i
                cmbSelectStep.Items.Add("RDC-03-001")
                cmbSelectStep.Items.Add("RDC-04-001")
                cmbSelectStep.Items.Add("RDC-05-001")

            Case "RFMA"
                For i = 1 To 5 : cmbSelectStep.Items.Add("RMA-0" & Format(i, "0") & "-001") : Next i

            Case "RFSYN"
                For i = 1 To 7 : cmbSelectStep.Items.Add("RST-0" & Format(i, "0") & "-001") : Next i

        End Select
        If cmbSelectStep.Items.Count > 0 Then
            cmbSelectStep.SelectedIndex = 0
        End If
    End Sub

    Private Sub CmdClose_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmdClose.MouseDown
        Dim Button As Short = e.Button \ &H100000
        Dim Shift As Short = Control.ModifierKeys \ &H10000
        Dim x As Integer
        Dim s As String

        HelpContextID = 1010
        x = MsgBox("Are you sure that you want to quit?", MsgBoxStyle.YesNo)
        If x <> 6 Then Exit Sub
        s = "If you are finished running selftest, then after the program closes," & vbCrLf
        s = s & " remove the SAIF and all selftest cables and adapters including" & vbCrLf
        s = s & " SP2, SP3, SP4, W206 and W209 on the back of the CIC." & vbCrLf & vbCrLf

        s = s & "Note: SP3 at CIC J5 can cause the station to restart after power down."
        x = MsgBox(s, MsgBoxStyle.OkOnly)
        CloseProgram = True ' This is needed after EndProgram completes 
        AbortTest = True    ' AbortTest will jump to the end of any instrument test and reset instruments

        cmdClose.Focus()
        cmdClose.Text = "Closing, Please Wait..."
        cmdClose.Font = New Font(cmdClose.Font.FontFamily, 24)
        cmdClose.Width = 200
        cmdClose.Height = 200
        cmdClose.Left = (Me.ClientRectangle.Width - 200) / 2
        cmdClose.Top = (Me.ClientRectangle.Height - 200) / 2
        cmdClose.BringToFront()
        cmdClose.Visible = True
        Application.DoEvents()
        PauseTest = False

        If TestRunning = False Then
            Call EndProgram()
            Application.Exit()
            End
        End If

    End Sub

    Private Sub cmdAbort_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmdAbort.MouseDown
        Dim Button As Short = e.Button \ &H100000
        Dim Shift As Short = Control.ModifierKeys \ &H10000

        Dim x5 As Integer
        HelpContextID = 1010

        x5 = MsgBox("Do you want to abort this test?", MsgBoxStyle.YesNo)
        If x5 = DialogResult.No Then Exit Sub

        AbortTest = True
        Me.cmdAbort.Text = "Aborting"
        PauseTest = False ' force continue, if paused

        Echo("********************")
        Echo("   Aborting Test")
        Echo("********************")
        Application.DoEvents()
        Application.DoEvents()


    End Sub

    Private Sub CmdCOF_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCOF.Click

        OptionFaultMode = COFmode
        HelpContextID = 1010
        lblFaultMode.Text = "COF"

    End Sub


    Private Sub CmdLOE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdLOE.Click

        OptionMode = LOEmode
        HelpContextID = 1010
        lblTestMode.Text = "LOE"

        LblSelectTest.Visible = False
        LblSelectStep.Visible = False
        cmbSelectTest.Visible = False
        cmbSelectStep.Visible = False

    End Sub

    Private Sub CmdLOS_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdLOS.Click

        OptionMode = LOSmode
        HelpContextID = 1010
        lblTestMode.Text = "LOS"

        LblSelectTest.Visible = True
        LblSelectStep.Visible = True
        cmbSelectTest.Visible = True
        cmbSelectStep.Visible = True

    End Sub

    Private Sub CmdPause_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmdPause.MouseDown
        Dim Button As Short = e.Button \ &H100000
        Dim Shift As Short = Control.ModifierKeys \ &H10000


        HelpContextID = 1010
        If cmdPause.Text = "Pause Test" Then
            cmdPause.Text = "Continue Test"
            PauseTest = True
        Else
            cmdPause.Text = "Pause Test"
            PauseTest = False
        End If
        Application.DoEvents()

    End Sub


    Private Sub cmdPPUstartOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPPUstartOK.Click

        FrPPUStart.Visible = False

    End Sub

    Private Sub CmdSearchCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdSearchCancel.Click

        FrSearch.Visible = False
        HelpContextID = 1010

    End Sub

    Private Sub CmdSearchOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdSearchOK.Click
        CmdSearchOK_Click()
    End Sub
    Public Sub CmdSearchOK_Click()

        HelpContextID = 1010
        FrSearch.Visible = False
        If Len(TxtSearch.Text) > 0 Then
            SearchWord = TxtSearch.Text
            FindWord()
        End If

    End Sub

    Private Sub CmdSOE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSOE.Click

        OptionMode = SOEmode '0
        HelpContextID = 1010
        lblTestMode.Text = "SOE"

        LblSelectTest.Visible = False
        LblSelectStep.Visible = False
        cmbSelectTest.Visible = False
        cmbSelectStep.Visible = False

    End Sub

    Private Sub CmdSOF_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSOF.Click

        OptionFaultMode = SOFmode
        HelpContextID = 1010
        lblFaultMode.Text = "SOF"

    End Sub

    Private Sub cmdDetails_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmdDetails.MouseDown

        HelpContextID = 1010
        HandleDetails()

    End Sub


    Private Sub DetailsText_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DetailsText.DoubleClick
        ' copy DetailsText.Text to a file
        ' use this event to save the data in Details Box to disk
        Dim x As Integer
        Dim sTemp As String

        If Dir(sLogFilePath & "SelfTest.txt", 17) <> "" Then
            Try
                If Dir(sLogFilePath & "SelfTest.bak", 17) <> "" Then
                    Kill(sLogFilePath & "SelfTest.bak")
                End If
            Catch
                MsgBox("Error deleting file " & sLogFilePath & "SelfTest.bak.")
                Exit Sub
            End Try

            Try
                Rename(sLogFilePath & "SelfTest.txt", sLogFilePath & "SelfTest.bak") ' rename existing file
            Catch
                MsgBox("Error renaming file " & sLogFilePath & "SelfTest.txt.")
                Exit Sub
            End Try

        End If

        Try
            x = FreeFile()
            FileOpen(x, sLogFilePath & "SelfTest.txt", OpenMode.Output)
            sTemp = Me.DetailsText.Text.Replace(vbLf, vbCrLf) ' fix crlf
            Print(x, sTemp)
            MsgBox("File saved as " & sLogFilePath & "SelfTest.txt.")
        Catch
            MsgBox("Error saving file " & sLogFilePath & "SelfTest.txt.")
        End Try
        FileClose(x)


    End Sub



    Private Sub frmSTest_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = e.KeyCode
        Dim Shift As Short = e.KeyData \ &H10000


        If KeyCode = key_F1 Then
            '' Can be used if only the old style ".hlp" file is available
            ''If Dir$(ProgramPath & "Stest1.hlp", 17) <> "" Then  ' works for "Stest1.hlp"
            ''    If HelpContextID = 0 Then
            ''        Process.Start("Stest1.hlp")
            ''    Else
            ''        Dim p As New ProcessStartInfo ' New ProcessStartInfo created
            ''        p.FileName = "WINHLP32.exe"
            ''        p.Arguments = "-N " & CStr(HelpContextID) & " " & ProgramPath & "Stest1.hlp"
            ''        Process.Start(p)
            ''    End If
            ''End If


            If Dir$(ProgramPath & "Stest_TETS_1.chm", 17) <> "" Then
                If HelpContextID = 0 Then  ' works for "Stest_TETS_1.chm"
                    Help.ShowHelp(Me, ProgramPath & "\Stest_TETS_1.chm", HelpNavigator.TableOfContents)
                Else
                    Help.ShowHelp(Me, ProgramPath & "\Stest_TETS_1.chm", HelpNavigator.TopicId, CStr(HelpContextID))
                End If
                KeyCode = 0
            End If


            ' Function Key F3 (Search details box)
        ElseIf KeyCode = key_F3 And DetailStatus = True Then
            If Shift <> 0 Or SearchWord = "" Then
                FrSearch.Top = (DetailsText.Top - FrSearch.Height)
                FrSearch.Left = (Me.Width - FrSearch.Width) / 2
                FrSearch.Visible = True
                ''   FrSearch.zorder(0)
                TxtSearch.Focus()
                Exit Sub
            Else
                FindWord()
            End If

        ElseIf KeyCode = key_F4 Then            ' function F4 (inc loop step to next test step)
            If OptionStep = 1 And cmbSelectStep.Items.Count = 1 Then
                cmbSelectStep.SelectedIndex = -1 ' not selected
            ElseIf OptionStep < cmbSelectStep.Items.Count Then
                cmbSelectStep.SelectedIndex = OptionStep
            Else
                cmbSelectStep.SelectedIndex = 0
            End If
            Application.DoEvents()
            Application.DoEvents()

        ElseIf KeyCode = key_F5 Then            ' function F5 (cont, change loop step to first test step)

            If cmbSelectStep.Items.Count = 0 Then
            ElseIf cmbSelectStep.Items.Count = 1 Then
                cmbSelectStep.SelectedIndex = -1 ' not selected
            Else
                cmbSelectStep.SelectedIndex = 0
            End If

            ' Alternate "F"
        ElseIf (KeyCode = Asc("F") Or KeyCode = Asc("f")) And Shift = 4 And DetailStatus = True Then
            If Shift <> 0 Or SearchWord = "" Then
                FrSearch.Top = (DetailsText.Top - FrSearch.Height)
                FrSearch.Left = (Me.Width - FrSearch.Width) / 2
                FrSearch.Visible = True
                '' FrSearch.zorder(0)
                TxtSearch.Focus()
                Exit Sub
            Else
                FindWord()
            End If

        ElseIf KeyCode = key_escape Then
            AbortDump = True
        End If

    End Sub



    Private Sub frmSTest_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        '************************************************************
        '* Nomenclature   : ATS-TETS SYSTEM SELF TEST               *
        '*    DESCRIPTION:                                          *
        '*     This module is the program entry (starting) point.   *
        '*     This module is always open and visible.              *
        '************************************************************

        Dim Ret As Integer
        Dim SystemDir As String
        Dim VisaLibrary As String
        Dim FileHandle As Integer
        Dim DataLine As String = ""
        Dim InstrumentToInit As Short
        Dim dProg As Double

        Dim status As Short
        Dim XmlBuf As String
        Dim Allocation As String
        Dim Response As String = Space(4096)
        Dim i As Integer
        Dim lpBuffer As String = Space(256)
        Dim x As Integer
        Dim ShowTest As Integer

        Dim iLeft(6) As Integer
        Dim iTop(13) As Integer

        'button locations
        iLeft(1) = 12 ' 6 columns and 13 rows max
        iLeft(2) = 53
        iLeft(3) = 85
        iLeft(4) = 311
        iLeft(5) = 352
        iLeft(6) = 384
        For i = 0 To 12
            iTop(i) = 63 + (i * 23)
        Next i



        StartReady = False ' This disables someone from clicking buttons too soon.
        HelpContextID = 0

        'Set Sytem "logfile.txt" path to same as ATS.ini file
        Try
            If sATS_INI = "" Then
                ' get sATS_INI path\filename from registry
                sATS_INI = Microsoft.Win32.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\ATS\", "IniFilePath", Nothing)
                If sATS_INI = "" Then
                    sATS_INI = "C:\Users\Public\Documents\ATS.ini" 'C:\Users\Public\Documents\ATS.ini
                End If
                x = InStr(sATS_INI, "\ATS.ini", CompareMethod.Text)
                If x > 0 Then
                    sLogFilePath = Strings.Left(sATS_INI, x)
                Else
                    sLogFilePath = "C:\Users\Public\Documents\"
                End If
            End If
        Catch
            MsgBox("Error getting path to ATS.ini file from registry. ")
            If sATS_INI = "" Then
                sATS_INI = "C:\Users\Public\Documents\ATS.ini" 'C:\Users\Public\Documents\ATS.ini
            End If
        End Try

        x = InStr(sATS_INI, "ATS.ini")
        If x > 1 Then
            sAts_INI_path = Strings.Left(sATS_INI, x - 1)
        Else
            sAts_INI_path = "C:\Users\Public\Documents\"
        End If

        'Note: version is gathered from the ini  file
        ProgramPath = Application.StartupPath
        ProgramPath &= "\"
        i = InStr(ProgramPath, "\bin")
        If i > 0 Then
            ProgramPath = Strings.Left(ProgramPath, i)
        End If

        sPathLossPath = "C:\Users\Public\Documents\ATS\PLC\PLC.dat"

        'TIR 3.0.0.0007-30 Self Test Version date.  Removed the hardcoded date
        STversion = GatherIniFileInformation("System Startup", "SWR", "Unknown")

        ''My.ApplicationOleRequestPendingMsgText = "The instrument is busy performing another operation, please wait."
        ''My.ApplicationOleRequestPendingMsgTitle = "Instrument is busy!"
        ''My.ApplicationOleRequestPendingTimeout = 10000 'set Time out for 10 seconds


        'Define command options.
        CloseRelay = 0 'false
        SetSlave = 0
        SenseRemote = 0
        CurrentConstant = 0

        OpenRelay = -1 'true
        SetMaster = -1
        SenseLocal = -1
        CurrentLimit = -1

        'Initialize Variables
        InitVar()

        FrSearch.Visible = False
        TxtSearch.Text = ""
        'Note: STversion$ is now changed in STestMain/Main subroutine
        ' bb removed Me.Text = "ATS System Self Test Version " & STversion
        Me.Text = "ATS-TETS System Self Test"

        'If ABORTED, Write an Ending Record.
        ' Note: Abort no longer restarts the program
        If Trim(Microsoft.VisualBasic.Command()) = "ABORT" Then EndData() ' 06/22/01

        sspSTestPanel.Enabled = False
        STestBusy = True
        TestingMessage = "Initializing System Instruments. . ."
        DetailsText.Text = ""
        DetailsText.Visible = False
        FrSearch.Visible = False
        Echo("")

        'Set-Up Main Form
        iNumOfTests = LAST_CORE_INST + 1

        Top = (PrimaryScreen.Bounds.Height - Height) / 8
        Left = (PrimaryScreen.Bounds.Width - Width) / 2


        AbortTest = False
        PauseTest = False
        cmdAbort.Text = "Abort Test"
        cmdPause.Text = "Pause Test"

        'Fill Button icons and labels and index
        ''sscTest(0).Tag = 0
        ' ''Core first
        i = 1
        cmbSelectTest.Items.Clear()
        sscTest_0.Image = imlButtons.Images(0)
        For iInst = 1 To 25
            ShowTest = True
            Select Case iInst
                Case 1 : sscTest_1.Tag = iInst
                Case 2 : sscTest_2.Tag = iInst
                Case 3 : sscTest_3.Tag = iInst
                Case 4 : sscTest_4.Tag = iInst
                Case 5 : sscTest_5.Tag = iInst
                Case 6 : sscTest_6.Tag = iInst
                Case 7 : sscTest_7.Tag = iInst
                Case 8 : sscTest_8.Tag = iInst
                Case 9 : sscTest_9.Tag = iInst
                Case 10 : sscTest_10.Tag = iInst
                Case 11 : sscTest_11.Tag = iInst
                Case 12 : sscTest_12.Tag = iInst
                Case 13 : sscTest_13.Tag = iInst
                Case 14 : sscTest_14.Tag = iInst
                Case 15 : sscTest_15.Tag = iInst
                Case 16 : sscTest_16.Tag = iInst
                Case 17 : sscTest_17.Tag = iInst
                Case 18 : sscTest_18.Tag = iInst
                Case 19 : sscTest_19.Tag = iInst
                Case 20 : sscTest_20.Tag = iInst
                Case 21 : sscTest_21.Tag = iInst
                Case 22 : sscTest_22.Tag = iInst
                Case 23 : sscTest_23.Tag = iInst
                Case 24 : sscTest_24.Tag = iInst
                    If sscTest_24.Visible = False Then
                        ShowTest = False
                    End If
                Case 25 : sscTest_25.Tag = iInst
                    If sscTest_25.Visible = False Then
                        ShowTest = False
                    End If
            End Select
            i += 1
            If ShowTest = True Then
                cmbSelectTest.Items.Add(sTestName(iInst))
            End If
        Next iInst

        cmbSelectTest.SelectedIndex = 0

        RFOptionInstalled = CheckOptionInstalled("RF") ' checks the ATS.ini file in the windows directory
        EoOptionInstalled = False ' CheckOptionInstalled("EO")

        iNumOfTests = LAST_CORE_INST + 1
        If RFOptionInstalled Then ' (add 3 RF instruments)
            iNumOfTests += 5
        End If
        With Me
            'bb 2016-11-28
            'Locate left column of GUI objects, always first 11 on left(0-10 at 0-10)
            'RunE2E, 1553, DTS, SW1-SW5, DMM, DSO, C/T
            'RunE2E
            .PassFrame_0.Left = iLeft(1)
            .sscTest_0.Left = iLeft(2)
            .InstrumentLabel_0.Left = iLeft(3)
            .PassFrame_0.Top = iTop(0)
            .sscTest_0.Top = iTop(0)
            .InstrumentLabel_0.Top = iTop(0)
            '1553
            .PassFrame_1.Left = iLeft(1)
            .sscTest_1.Left = iLeft(2)
            .InstrumentLabel_1.Left = iLeft(3)
            .PassFrame_1.Top = iTop(1)
            .sscTest_1.Top = iTop(1)
            .InstrumentLabel_1.Top = iTop(1)
            'DTS
            .PassFrame_2.Left = iLeft(1)
            .sscTest_2.Left = iLeft(2)
            .InstrumentLabel_2.Left = iLeft(3)
            .PassFrame_2.Top = iTop(2)
            .sscTest_2.Top = iTop(2)
            .InstrumentLabel_2.Top = iTop(2)
            'LF1
            .PassFrame_3.Left = iLeft(1)
            .sscTest_3.Left = iLeft(2)
            .InstrumentLabel_3.Left = iLeft(3)
            .PassFrame_3.Top = iTop(3)
            .sscTest_3.Top = iTop(3)
            .InstrumentLabel_3.Top = iTop(3)
            'LF2
            .PassFrame_4.Left = iLeft(1)
            .sscTest_4.Left = iLeft(2)
            .InstrumentLabel_4.Left = iLeft(3)
            .PassFrame_4.Top = iTop(4)
            .sscTest_4.Top = iTop(4)
            .InstrumentLabel_4.Top = iTop(4)
            'LF3
            .PassFrame_5.Left = iLeft(1)
            .InstrumentLabel_5.Left = iLeft(3)
            .sscTest_5.Left = iLeft(2)
            .PassFrame_5.Top = iTop(5)
            .sscTest_5.Top = iTop(5)
            .InstrumentLabel_5.Top = iTop(5)
            'MF
            .PassFrame_6.Left = iLeft(1)
            .sscTest_6.Left = iLeft(2)
            .InstrumentLabel_6.Left = iLeft(3)
            .PassFrame_6.Top = iTop(6)
            .sscTest_6.Top = iTop(6)
            .InstrumentLabel_6.Top = iTop(6)
            'HF
            .PassFrame_7.Left = iLeft(1)
            .sscTest_7.Left = iLeft(2)
            .InstrumentLabel_7.Left = iLeft(3)
            .PassFrame_7.Top = iTop(7)
            .sscTest_7.Top = iTop(7)
            .InstrumentLabel_7.Top = iTop(7)
            'DMM
            .PassFrame_8.Left = iLeft(1)
            .sscTest_8.Left = iLeft(2)
            .InstrumentLabel_8.Left = iLeft(3)
            .PassFrame_8.Top = iTop(8)
            .sscTest_8.Top = iTop(8)
            .InstrumentLabel_8.Top = iTop(8)

            'DSO
            .PassFrame_9.Left = iLeft(1)
            .sscTest_9.Left = iLeft(2)
            .InstrumentLabel_9.Left = iLeft(3)
            .PassFrame_9.Top = iTop(9)
            .sscTest_9.Top = iTop(9)
            .InstrumentLabel_9.Top = iTop(9)
            'C/T
            .PassFrame_10.Left = iLeft(1)
            .sscTest_10.Left = iLeft(2)
            .InstrumentLabel_10.Left = iLeft(3)
            .PassFrame_10.Top = iTop(10)
            .sscTest_10.Top = iTop(10)
            .InstrumentLabel_10.Top = iTop(10)


            If RFOptionInstalled Then
                'Add TWO on left(11,12 at 11,12), then 11 on right (13-25 at 0-11)
                'finish left column
                'ARB
                .PassFrame_11.Left = iLeft(1)
                .sscTest_11.Left = iLeft(2)
                .InstrumentLabel_11.Left = iLeft(3)
                .PassFrame_11.Top = iTop(11)
                .sscTest_11.Top = iTop(11)
                .InstrumentLabel_11.Top = iTop(11)

                'FGEN
                .PassFrame_12.Left = iLeft(1)
                .sscTest_12.Left = iLeft(2)
                .InstrumentLabel_12.Left = iLeft(3)
                .PassFrame_12.Top = iTop(12)
                .sscTest_12.Top = iTop(12)
                .InstrumentLabel_12.Top = iTop(12)

                'start right column
                'PPU
                .PassFrame_13.Left = iLeft(4)
                .sscTest_13.Left = iLeft(5)
                .InstrumentLabel_13.Left = iLeft(6)
                .PassFrame_13.Top = iTop(0)
                .sscTest_13.Top = iTop(0)
                .InstrumentLabel_13.Top = iTop(0)
                'S/R
                .PassFrame_14.Left = iLeft(4)
                .sscTest_14.Left = iLeft(5)
                .InstrumentLabel_14.Left = iLeft(6)
                .PassFrame_14.Top = iTop(1)
                .sscTest_14.Top = iTop(1)
                .InstrumentLabel_14.Top = iTop(1)
                'Serial
                .PassFrame_15.Left = iLeft(4)
                .sscTest_15.Left = iLeft(5)
                .InstrumentLabel_15.Left = iLeft(6)
                .PassFrame_15.Top = iTop(2)
                .sscTest_15.Top = iTop(2)
                .InstrumentLabel_15.Top = iTop(2)
                'COM1
                .PassFrame_16.Left = iLeft(4)
                .sscTest_16.Left = iLeft(5)
                .InstrumentLabel_16.Left = iLeft(6)
                .PassFrame_16.Top = iTop(3)
                .sscTest_16.Top = iTop(3)
                .InstrumentLabel_16.Top = iTop(3)
                'COM2
                .PassFrame_17.Left = iLeft(4)
                .sscTest_17.Left = iLeft(5)
                .InstrumentLabel_17.Left = iLeft(6)
                .PassFrame_17.Top = iTop(4)
                .sscTest_17.Top = iTop(4)
                .InstrumentLabel_17.Top = iTop(4)
                'Gigabit
                .PassFrame_18.Left = iLeft(4)
                .sscTest_18.Left = iLeft(5)
                .InstrumentLabel_18.Left = iLeft(6)
                .PassFrame_18.Top = iTop(5)
                .sscTest_18.Top = iTop(5)
                .InstrumentLabel_18.Top = iTop(5)
                'CAN
                .PassFrame_19.Left = iLeft(4)
                .sscTest_19.Left = iLeft(5)
                .InstrumentLabel_19.Left = iLeft(6)
                .PassFrame_19.Top = iTop(6)
                .sscTest_19.Top = iTop(6)
                .InstrumentLabel_19.Top = iTop(6)
                'Aprobe
                .PassFrame_20.Left = iLeft(4)
                .sscTest_20.Left = iLeft(5)
                .InstrumentLabel_20.Left = iLeft(6)
                .PassFrame_20.Top = iTop(7)
                .sscTest_20.Top = iTop(7)
                .InstrumentLabel_20.Top = iTop(7)

                ' only, ie add 5 on right(21-25 at 9-11)
                'RFPWR, RFSTIM and RFMODANAL
                'RFPWR
                'RFPWR
                .PassFrame_21.Left = iLeft(4)
                .sscTest_21.Left = iLeft(5)
                .InstrumentLabel_21.Left = iLeft(6)
                .PassFrame_21.Top = iTop(8)
                .sscTest_21.Top = iTop(8)
                .InstrumentLabel_21.Top = iTop(8)
                'RFSTIM
                .PassFrame_22.Left = iLeft(4)
                .sscTest_22.Left = iLeft(5)
                .InstrumentLabel_22.Left = iLeft(6)
                .PassFrame_22.Top = iTop(9)
                .sscTest_22.Top = iTop(9)
                .InstrumentLabel_22.Top = iTop(9)
                'RFSTIM
                .PassFrame_23.Left = iLeft(4)
                .sscTest_23.Left = iLeft(5)
                .InstrumentLabel_23.Left = iLeft(6)
                .PassFrame_23.Top = iTop(10)
                .sscTest_23.Top = iTop(10)
                .InstrumentLabel_23.Top = iTop(10)
                'RFMODANAL
                .PassFrame_24.Left = iLeft(4)
                .sscTest_24.Left = iLeft(5)
                .InstrumentLabel_24.Left = iLeft(6)
                .PassFrame_24.Top = iTop(11)
                .sscTest_24.Top = iTop(11)
                .InstrumentLabel_24.Top = iTop(11)
                'RFMODANAL
                .PassFrame_25.Left = iLeft(4)
                .sscTest_25.Left = iLeft(5)
                .InstrumentLabel_25.Left = iLeft(6)
                .PassFrame_25.Top = iTop(12)
                .sscTest_25.Top = iTop(12)
                .InstrumentLabel_25.Top = iTop(12)


            Else
                'start 2nd column
                ' No RF, ie add 10 on right(11-20 at 0-9), then hide test 21-25(RF)
                'ARB
                .PassFrame_11.Left = iLeft(4)
                .sscTest_11.Left = iLeft(5)
                .InstrumentLabel_11.Left = iLeft(6)
                .PassFrame_11.Top = iTop(0)
                .sscTest_11.Top = iTop(0)
                .InstrumentLabel_11.Top = iTop(0)
                'FGEN
                .PassFrame_12.Left = iLeft(4)
                .sscTest_12.Left = iLeft(5)
                .InstrumentLabel_12.Left = iLeft(6)
                .PassFrame_12.Top = iTop(1)
                .sscTest_12.Top = iTop(1)
                .InstrumentLabel_12.Top = iTop(1)
                'PPU
                .PassFrame_13.Left = iLeft(4)
                .sscTest_13.Left = iLeft(5)
                .InstrumentLabel_13.Left = iLeft(6)
                .PassFrame_13.Top = iTop(2)
                .sscTest_13.Top = iTop(2)
                .InstrumentLabel_13.Top = iTop(2)
                'S/R
                .PassFrame_14.Left = iLeft(4)
                .sscTest_14.Left = iLeft(5)
                .InstrumentLabel_14.Left = iLeft(6)
                .PassFrame_14.Top = iTop(3)
                .sscTest_14.Top = iTop(3)
                .InstrumentLabel_14.Top = iTop(3)
                'Serial
                .PassFrame_15.Left = iLeft(4)
                .sscTest_15.Left = iLeft(5)
                .InstrumentLabel_15.Left = iLeft(6)
                .PassFrame_15.Top = iTop(4)
                .sscTest_15.Top = iTop(4)
                .InstrumentLabel_15.Top = iTop(4)
                'COM1
                .PassFrame_16.Left = iLeft(4)
                .sscTest_16.Left = iLeft(5)
                .InstrumentLabel_16.Left = iLeft(6)
                .PassFrame_16.Top = iTop(5)
                .sscTest_16.Top = iTop(5)
                .InstrumentLabel_16.Top = iTop(5)
                'COM2
                .PassFrame_17.Left = iLeft(4)
                .sscTest_17.Left = iLeft(5)
                .InstrumentLabel_17.Left = iLeft(6)
                .PassFrame_17.Top = iTop(6)
                .sscTest_17.Top = iTop(6)
                .InstrumentLabel_17.Top = iTop(6)
                'Gigabit
                .PassFrame_18.Left = iLeft(4)
                .sscTest_18.Left = iLeft(5)
                .InstrumentLabel_18.Left = iLeft(6)
                .PassFrame_18.Top = iTop(7)
                .sscTest_18.Top = iTop(7)
                .InstrumentLabel_18.Top = iTop(7)
                'CAN
                .PassFrame_19.Left = iLeft(4)
                .sscTest_19.Left = iLeft(5)
                .InstrumentLabel_19.Left = iLeft(6)
                .PassFrame_19.Top = iTop(8)
                .sscTest_19.Top = iTop(8)
                .InstrumentLabel_19.Top = iTop(8)
                'Aprobe
                .PassFrame_20.Left = iLeft(4)
                .sscTest_20.Left = iLeft(5)
                .InstrumentLabel_20.Left = iLeft(6)
                .PassFrame_20.Top = iTop(9)
                .sscTest_20.Top = iTop(9)
                .InstrumentLabel_20.Top = iTop(9)

                'hide RF instruments
                .PassFrame_21.Visible = False
                .sscTest_21.Visible = False
                .InstrumentLabel_21.Visible = False
                .PassFrame_22.Visible = False
                .sscTest_22.Visible = False
                .InstrumentLabel_22.Visible = False
                .PassFrame_23.Visible = False
                .sscTest_23.Visible = False
                .InstrumentLabel_23.Visible = False
                .PassFrame_24.Visible = False
                .sscTest_24.Visible = False
                .InstrumentLabel_24.Visible = False
                .PassFrame_25.Visible = False
                .sscTest_25.Visible = False
                .InstrumentLabel_25.Visible = False
            End If

            'Size form and place bottom objects

            '   .imlButtons.ListImages(iInst + 1).Picture
            Dim iBase As Short
            iBase = .sscTest_11.Top + 18

            AbortTest = False
            PauseTest = False
            .cmdAbort.Text = "Abort Test"
            .cmdPause.Text = "Pause Test"
            '

        End With

        sbrUserInformation.Text = "Initializing System Instruments. . ."
        Application.DoEvents()

        Try
            'atxml stuff, Verify presence of system
            lpBuffer = Environment.SystemDirectory
            SystemDir = StripNullCharacters(lpBuffer)
            VisaLibrary = SystemDir & "\VISA32.DLL"

            'GEN-01-001
            If Not FnFileExists(VisaLibrary) Then
                Echo("GEN-01-001")
                sMsg = "Cannot find VISA Run-Time System. Unable to perform System Self Test."
                MsgBox(sMsg, MsgBoxStyle.Exclamation)
                Echo(sMsg)
                RegisterFailure(sfailstep:="GEN-01-001", sComment:=sMsg)
                Application.Exit()
                End
            End If
        Catch
            MsgBox("Error verifying Visa Run-Time system. ")
            Exit Sub
        End Try

        Try
            'cicl stuff, validate each instrument
            status = atxml_Initialize(proctype, guid)
        Catch
            ''  MsgBox("Error accessing atxml_Initialize," & vbcrlf & "CICL may not be running.")
            Echo("Error accessing atxml_initialize.")
            Echo("CICL may not be running.")
            Echo("Bypassing xml resource verification.")

            GoTo bypassCicl
            Application.Exit()
            End
        End Try


        Allocation = Microsoft.Win32.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\ATS\", "PAWSAllocationPath", Nothing)

        Try
            'Determine if the 1553 Bus is functioning
            Response = Space(4096)
            XmlBuf = "<AtXmlTestRequirements>"
            XmlBuf &= "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & _
                "  <SignalResourceName>MIL1553_1</SignalResourceName> " & "</ResourceRequirement> "
            XmlBuf &= "</AtXmlTestRequirements>"

            status = atxml_ValidateRequirements(XmlBuf, Allocation, Response, 4096)
            If status <> 0 Then
                MsgBox("The 1553 Bus is not responding.  Live mode is disabled.", MsgBoxStyle.Exclamation)
                LiveMode(MIL_STD_1553) = False
            Else
                LiveMode(MIL_STD_1553) = True
            End If

            'Determine if the Switch module is functioning
            Response = Space(4096)
            XmlBuf = "<AtXmlTestRequirements> " & "<ResourceRequirement> " & "   <ResourceType>Source</ResourceType> " & _
                "   <SignalResourceName>PAWS_SWITCH</SignalResourceName> " & "</ResourceRequirement> " & _
                "</AtXmlTestRequirements>"
            status = atxml_ValidateRequirements(XmlBuf, Allocation, Response, 4096)
            If status = conNoDLL Then
                MsgBox("Error in loading Ri1260.DLL.  Switch Live mode is disabled.", MsgBoxStyle.Information)
                LiveMode(SWITCH1) = False
            ElseIf status <> 0 Then
                MsgBox("The Switch Module is not responting.  Live mode is disabled.", MsgBoxStyle.Information)
                LiveMode(SWITCH1) = False
            Else
                LiveMode(SWITCH1) = True
            End If

            'Determine if the DMM is functioning
            Response = Space(4096)
            XmlBuf = "<AtXmlTestRequirements>" & "<ResourceRequirement>" & "   <ResourceType>Source</ResourceType>" & _
                "   <SignalResourceName>DMM_1</SignalResourceName> " & "</ResourceRequirement> " & _
                "</AtXmlTestRequirements>"                '
            status = atxml_ValidateRequirements(XmlBuf, Allocation, Response, 4096)
            If status <> 0 Then
                LiveMode(DMM) = False
                MsgBox("The DMM Is Not Responding.  Live Mode Disabled.", MsgBoxStyle.Information)
            Else
                LiveMode(DMM) = True
            End If

            'Determine If The DSCOPE Is Functioning
            Response = Space(4096)
            XmlBuf = "<AtXmlTestRequirements>" & "<ResourceRequirement>" & "  <ResourceType>Source</ResourceType>" & _
                "  <SignalResourceName>DSO_1</SignalResourceName> " & "</ResourceRequirement>" & _
                "</AtXmlTestRequirements>"
            status = atxml_ValidateRequirements(XmlBuf, Allocation, Response, 4096)
            If status <> 0 Then
                LiveMode(OSCOPE) = False
                MsgBox("The DSO Is Not Responding.  Live Mode Disabled.", MsgBoxStyle.Information)
            Else
                LiveMode(OSCOPE) = True
            End If

            ' Determine if the C/T is functioning
            Response = Space(4096)
            XmlBuf = "<AtXmlTestRequirements>" & "<ResourceRequirement>" & "   <ResourceType>Source</ResourceType>" & _
                "   <SignalResourceName>CNTR_1</SignalResourceName> " & "</ResourceRequirement> " & _
                "</AtXmlTestRequirements>"                '
            status = atxml_ValidateRequirements(XmlBuf, Allocation, Response, 4096)
            If status <> 0 Then
                LiveMode(COUNTER) = False
                MsgBox("The Counter/Timer Is Not Responding.  Live Mode Disabled.", MsgBoxStyle.Information)
            Else
                LiveMode(COUNTER) = True
            End If

            'Determine if the ARB is functioning
            Response = Space(4096)
            XmlBuf = "<AtXmlTestRequirements>" & "<ResourceRequirement>" & "  <ResourceType>Source</ResourceType>" & _
                "  <SignalResourceName>ARB_GEN_1</SignalResourceName> " & "</ResourceRequirement> " & _
                "</AtXmlTestRequirements>"
            status = atxml_ValidateRequirements(XmlBuf, Allocation, Response, 4096)
            If status <> 0 Then
                MsgBox("The ARB is not responding.  Live mode is disabled.", MsgBoxStyle.Exclamation)
                LiveMode(ARB) = False
                i = MsgBox("The CiclKernelC.exe may not be running!" & vbCrLf & "Do you want to quit?", MsgBoxStyle.YesNo)
                If i = 6 Then EndProgram()
            Else
                LiveMode(ARB) = True
            End If

            'Determine if the FG is functioning
            Response = Space(4096)
            XmlBuf = "<AtXmlTestRequirements>" & "<ResourceRequirement>" & "   <ResourceType>Source</ResourceType>" & _
                "   <SignalResourceName>FUNC_GEN_1</SignalResourceName> " & "</ResourceRequirement> " & _
                "</AtXmlTestRequirements>"
            status = atxml_ValidateRequirements(XmlBuf, Allocation, Response, 4096)
            If status <> 0 Then
                LiveMode(FGEN) = False
                MsgBox("The Function Generator is not responding.  Live mode is disabled.", MsgBoxStyle.Exclamation)
            Else
                LiveMode(FGEN) = True
            End If

            'Determine if the Freedom PS is functioning
            Response = Space(4096)
            XmlBuf = "<AtXmlTestRequirements>"
            XmlBuf &= "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & _
                "  <SignalResourceName>DCPS_40V5A_1</SignalResourceName> " & "</ResourceRequirement> "
            XmlBuf &= "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & _
                "  <SignalResourceName>DCPS_40V5A_2</SignalResourceName> " & "</ResourceRequirement>"
            XmlBuf &= "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & _
                "  <SignalResourceName>DCPS_40V5A_3</SignalResourceName> " & "</ResourceRequirement>"
            XmlBuf &= "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & _
                "  <SignalResourceName>DCPS_40V5A_4</SignalResourceName> " & "</ResourceRequirement>"
            XmlBuf &= "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & _
                "  <SignalResourceName>DCPS_40V5A_5</SignalResourceName> " & "</ResourceRequirement>"
            XmlBuf &= "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & _
                "  <SignalResourceName>DCPS_40V5A_6</SignalResourceName> " & "</ResourceRequirement>"
            XmlBuf &= "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & _
                "  <SignalResourceName>DCPS_40V5A_7</SignalResourceName> " & "</ResourceRequirement>"
            XmlBuf &= "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & _
                "  <SignalResourceName>DCPS_40V5A_8</SignalResourceName> " & "</ResourceRequirement>"
            XmlBuf &= "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & _
                "  <SignalResourceName>DCPS_40V5A_9</SignalResourceName> " & "</ResourceRequirement>"
            XmlBuf &= "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & _
                "  <SignalResourceName>DCPS_65V5A_10</SignalResourceName> " & "</ResourceRequirement>"
            XmlBuf &= "</AtXmlTestRequirements>"
            status = atxml_ValidateRequirements(XmlBuf, Allocation, Response, Len(XmlBuf))
            If status <> 0 Then
                LiveMode(PPU) = False
                MsgBox("The PPU is not responding.  Live mode is disabled.", MsgBoxStyle.Exclamation)
            Else
                LiveMode(PPU) = True
            End If

            'Determine if the S/R is functioning
            Response = Space(4096)
            XmlBuf = "<AtXmlTestRequirements>"
            XmlBuf &= "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & _
                "  <SignalResourceName>SRS_1_DS1</SignalResourceName> " & "</ResourceRequirement> "
            XmlBuf &= "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & _
                "  <SignalResourceName>SRS_1_DS2</SignalResourceName> " & "</ResourceRequirement>"
            XmlBuf &= "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & _
                "  <SignalResourceName>SRS_1_SD1</SignalResourceName> " & "</ResourceRequirement>"
            XmlBuf &= "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & _
                "  <SignalResourceName>SRS_1_SD2</SignalResourceName> " & "</ResourceRequirement>"
            XmlBuf &= "</AtXmlTestRequirements>"
            status = atxml_ValidateRequirements(XmlBuf, Allocation, Response, 4096)
            If status <> 0 Then
                MsgBox("The Syncro/Resolver is not responding.  Live mode is disabled.", MsgBoxStyle.Exclamation)
                LiveMode(SYN_RES) = False
            Else
                LiveMode(SYN_RES) = True
            End If

            'Determine if the PCI_Serial is functioning
            Response = Space(4096)
            XmlBuf = "<AtXmlTestRequirements>" & _
                "<ResourceRequirement>" & "  <ResourceType>Source</ResourceType>" & _
                "  <SignalResourceName>PCISERIAL_1</SignalResourceName> " & "  </ResourceRequirement> " & _
                "<ResourceRequirement>" & "  <ResourceType>Source</ResourceType>" & _
                "  <SignalResourceName>RS485_1</SignalResourceName> " & "  </ResourceRequirement> " & _
                "</AtXmlTestRequirements>"
            status = atxml_ValidateRequirements(XmlBuf, Allocation, Response, 4096)
            If status = conNoDLL Then
                MsgBox("Error in loading Serial Communications Driver.", MsgBoxStyle.Information)
                LiveMode(SERIALCCA) = False
            ElseIf status <> 0 Then
                MsgBox("The Bus is not responding.", MsgBoxStyle.Exclamation)
                LiveMode(SERIALCCA) = False
            Else
                LiveMode(SERIALCCA) = True
            End If

            'Determine if the COM ports are functioning
            Response = Space(4096)
            XmlBuf = "<AtXmlTestRequirements>" & _
                "<ResourceRequirement>" & "  <ResourceType>Source</ResourceType>" & _
                "  <SignalResourceName>COM_1</SignalResourceName> " & "  </ResourceRequirement> " & _
                "<ResourceRequirement>" & "  <ResourceType>Source</ResourceType>" & _
                "  <SignalResourceName>COM_2</SignalResourceName> " & "  </ResourceRequirement> " & _
                "<ResourceRequirement>" & "  <ResourceType>Source</ResourceType>" & _
                "  <SignalResourceName>ETHERNET_1</SignalResourceName> " & "  </ResourceRequirement> " & _
                "<ResourceRequirement>" & "  <ResourceType>Source</ResourceType>" & _
                "  <SignalResourceName>ETHERNET_2</SignalResourceName> " & "  </ResourceRequirement> " & _
                "</AtXmlTestRequirements>"
            status = atxml_ValidateRequirements(XmlBuf, Allocation, Response, 4096)
            If status = conNoDLL Then
                MsgBox("Error in loading Communication Bus Driver.", MsgBoxStyle.Information)
                LiveMode(RS232) = False
            ElseIf status <> 0 Then
                MsgBox("The Bus is not responding.", MsgBoxStyle.Exclamation)
                LiveMode(RS232) = False
            Else
                LiveMode(RS232) = True
            End If

            'Determine if the CAN Bus is functioning
            Response = Space(4096)
            XmlBuf = "<AtXmlTestRequirements>"
            XmlBuf &= "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & _
                "  <SignalResourceName>CAN_1</SignalResourceName> " & "</ResourceRequirement> "
            XmlBuf &= "</AtXmlTestRequirements>"
            status = atxml_ValidateRequirements(XmlBuf, Allocation, Response, 4096)
            If status <> 0 Then
                MsgBox("The CAN Bus is not responding.  Live mode is disabled.", MsgBoxStyle.Exclamation)
                LiveMode(CANBUS) = False
            Else
                LiveMode(CANBUS) = True
            End If

            'Test the RF
            If RFOptionInstalled = True Then
                'Determine if the RFPM is functioning
                Response = Space(4096) ' this was RFPM_1 ????
                XmlBuf = "<AtXmlTestRequirements> " & "<ResourceRequirement> " & _
                    "   <ResourceType>Source</ResourceType> " & "   <SignalResourceName>RF_PWR_1</SignalResourceName> " & _
                    "</ResourceRequirement> " & "</AtXmlTestRequirements>"
                Response = Space(4096)
                status = atxml_ValidateRequirements(XmlBuf, Allocation, Response, 4096)
                If status <> 0 Then
                    MsgBox("The RF Power Meter is not responding.  Live mode is disabled.", MsgBoxStyle.Exclamation)
                    LiveMode(RFPM) = False
                Else
                    LiveMode(RFPM) = True
                End If
                'Determine if the RF STIM is functioning
                Response = Space(4096)
                XmlBuf = "<AtXmlTestRequirements> " & "<ResourceRequirement> " & _
                    "   <ResourceType>Source</ResourceType> " & "   <SignalResourceName>RFGEN_1</SignalResourceName> " & _
                    "</ResourceRequirement> " & "</AtXmlTestRequirements>"
                Response = Space(4096)
                status = atxml_ValidateRequirements(XmlBuf, Allocation, Response, 4096)
                If status <> 0 Then
                    MsgBox("The RF Stimulus is not responding.  Live mode is disabled.", MsgBoxStyle.Exclamation)
                    LiveMode(RFSYN) = False
                Else
                    LiveMode(RFSYN) = True
                End If
                'Determine if the RF MEAS is functioning
                Response = Space(4096)
                XmlBuf = "<AtXmlTestRequirements> " & "<ResourceRequirement> " & _
                    "   <ResourceType>Source</ResourceType> " & "   <SignalResourceName>RFMEAS_1</SignalResourceName> " & _
                    "</ResourceRequirement> " & "</AtXmlTestRequirements>"
                Response = Space(4096)
                status = atxml_ValidateRequirements(XmlBuf, Allocation, Response, 4096)
                If status <> 0 Then
                    MsgBox("The RF Measure is not responding.  Live mode is disabled.", MsgBoxStyle.Exclamation)
                    LiveMode(RFMODANAL) = False
                Else
                    LiveMode(RFMODANAL) = True
                End If
            End If

            Delay(0.5)
        Catch
            MsgBox("Error verifying CICL resources.")
            Exit Sub

        End Try


        Try
            'GEN-01-002
            nErr = viOpenDefaultRM(SessionHandle)
            If nErr <> VI_SUCCESS Then
                Echo("GEN-01-002")
                sMsg = "VISA Resource Manager Error: " & GetVisaErrorMessage(ResourceName(InstrumentToInit), nErr) & vbCrLf & "Unable to perform System Self Test."
                Echo(sMsg)
                RegisterFailure(sfailstep:="GEN-01-002", sComment:=sMsg)
                Application.Exit()
                End
            End If
            EoOptionInstalled = False 'CheckOptionInstalled("EO")
        Catch
            MsgBox("Error getting Visa session handle.")
            Exit Sub
        End Try


        Me.Show()

        Try
            VXIPowerCheck()
            If Err.Number <> 0 Then
                MsgBox("Error: " & CStr(Err.Number) & Err.Description)
                Err.Clear()
            End If
        Catch
        End Try


        If Trim(Microsoft.VisualBasic.Command()) = "ABORT" Then
            sbrUserInformation.Text = "Retrieving log. . ."
            TestingMessage = "Retrieving log. . ."
            Application.DoEvents()
            DetailStatus = False
            FileHandle = FreeFile()
            Try
                FileOpen(FileHandle, ProgramPath & LOG_FILE, OpenMode.Input)
                'GEN-02-001
                If Err.Number <> 0 Then
                    Echo("GEN-02-001")
                    sMsg = "Could not retrieve data from aborted Self Test procedure."
                    Echo(sMsg)
                    RegisterFailure(sfailstep:="GEN-02-001", sComment:=sMsg)
                    Err.Clear()
                Else
                    Do
                        DataLine = LineInput(FileHandle)
                        Echo(DataLine)
                    Loop While Not EOF(FileHandle)
                    FileClose(FileHandle)
                    Delay(0.1)
                End If

            Catch ex As Exception

            End Try

            Echo("")
            Echo("")
            Echo("*******************************")
            Echo("* Self Test Session Restarted *")
            Echo("*   Due to Self Test Abort    *")
            Echo("*    " & Today & " " & TimeOfDay & "      *")
            Echo("*******************************")
            Echo("")
            Echo("Initializing System Instruments. . .")

            TestingMessage = "Initializing System Instruments. . ."
            Me.sbrUserInformation.Text = "Initializing System Instruments. . ."
            Application.DoEvents()

        Else

bypassCicl:
            'Check to see if a proper warm-up period has occurred since power-up
            Me.Show()
            CenterTextBox("Self Test Session Start Up", Today & " " & TimeOfDay)
            Echo("Self Test Version: " & STversion)
            Echo("Initializing System Instruments. . .")
            frmAbout.Top = Me.Top + (Me.Height - frmAbout.Height) \ 2
            frmAbout.Left = Me.Left + (Me.Width - frmAbout.Width) \ 2
            frmAbout.Show()
            For i = 1 To 30
                Application.DoEvents()
                Delay(0.1)
            Next
            frmAbout.Hide()
            AbortTest = False
            WaitForWarmUpTime()
            If AbortTest = True Then
                '  Call EndProgram()
                Application.Exit()
                End
            End If
        End If
        HandleDetails()
        StartReady = True


        'Delete old log and completion files
        Try
            If Dir(ProgramPath & LOG_FILE, 17) <> "" Then
                Kill(ProgramPath & LOG_FILE)
            End If
        Catch

        End Try


        '**** Initialize Instruments and get the handles ****
        '****************************************************
        Me.proProgress.Visible = True

        Me.proProgress.Value = 0
        dProg = 0

        Dim iTest As Integer
        For iTest = 1 To iNumOfTests
            iSelGUI = iTest
            InstrumentToInit = iSelGUI
            ''  frmSTest.InstrumentLabel(iSelGUI).ForeColor = Color.Red
            Select Case iSelGUI
                Case 1 : Me.InstrumentLabel_1.ForeColor = Color.Red
                Case 2 : InstrumentLabel_2.ForeColor = Color.Red
                Case 3 : InstrumentLabel_3.ForeColor = Color.Red
                Case 4 : InstrumentLabel_4.ForeColor = Color.Red
                Case 5 : InstrumentLabel_5.ForeColor = Color.Red
                Case 6 : InstrumentLabel_6.ForeColor = Color.Red
                Case 7 : InstrumentLabel_7.ForeColor = Color.Red
                Case 8 : InstrumentLabel_8.ForeColor = Color.Red
                Case 9 : InstrumentLabel_9.ForeColor = Color.Red
                Case 10 : InstrumentLabel_10.ForeColor = Color.Red
                Case 11 : InstrumentLabel_11.ForeColor = Color.Red
                Case 12 : InstrumentLabel_12.ForeColor = Color.Red
                Case 13 : InstrumentLabel_13.ForeColor = Color.Red
                Case 14 : InstrumentLabel_14.ForeColor = Color.Red
                Case 15 : InstrumentLabel_15.ForeColor = Color.Red
                Case 16 : InstrumentLabel_16.ForeColor = Color.Red
                Case 17 : InstrumentLabel_17.ForeColor = Color.Red
                Case 18 : InstrumentLabel_18.ForeColor = Color.Red
                Case 19 : InstrumentLabel_19.ForeColor = Color.Red
                Case 20 : InstrumentLabel_20.ForeColor = Color.Red
                Case 21 : InstrumentLabel_21.ForeColor = Color.Red
                Case 22 : InstrumentLabel_22.ForeColor = Color.Red
                Case 23 : InstrumentLabel_23.ForeColor = Color.Red
                Case 24 : InstrumentLabel_24.ForeColor = Color.Red
                Case 25 : InstrumentLabel_25.ForeColor = Color.Red
            End Select
            Select Case InstrumentToInit
                Case DMM, COUNTER, ARB, FGEN, OSCOPE
                    InitMessageBasedInstrument(InstrumentToInit)
                Case SYN_RES
                    InitMessageBasedInstrument(InstrumentToInit)
                Case SWITCH1, SWITCH2, SWITCH3, SWITCH4, SWITCH5
                    InstrumentInitialized(InstrumentToInit) = InitSwitching(InstrumentToInit)
                Case DIGITAL
                    InstrumentInitialized(InstrumentToInit) = InitDigital()
                Case MIL_STD_1553
                    InstrumentInitialized(InstrumentToInit) = True
                Case SERIALCCA, RS422, RS232
                Case GIGA
                Case CANBUS
                Case PPU
                    InstrumentInitialized(InstrumentToInit) = InitPowerSupplies()
                    'Case RFPM, RFSYN, RFCOUNTER
                    '    InitMessageBasedInstrument(InstrumentToInit)
                    'Case RFDOWN
                    '   InitRFReceiver()
                    'Case RFMODANAL
                    '  InitModAnal(instrumentHandle(RFMODANAL), InstrumentSpec(RFMODANAL), IDNResponse(RFMODANAL))
            End Select

            If ErrorDetected = True Then
                FormatResultLine(InstrumentDescription(InstrumentToInit) & " initialization ", False)
                InstrumentInitialized(InstrumentToInit) = False
                ReportFailure(InstrumentToInit)
                ErrorDetected = False
            End If

            ''frmSTest.InstrumentLabel(iSelGUI).ForeColor = Color.Black
            Select Case iSelGUI
                Case 0 : InstrumentLabel_0.ForeColor = Color.Black
                Case 1 : InstrumentLabel_1.ForeColor = Color.Black
                Case 2 : InstrumentLabel_2.ForeColor = Color.Black
                Case 3 : InstrumentLabel_3.ForeColor = Color.Black
                Case 4 : InstrumentLabel_4.ForeColor = Color.Black
                Case 5 : InstrumentLabel_5.ForeColor = Color.Black
                Case 6 : InstrumentLabel_6.ForeColor = Color.Black
                Case 7 : InstrumentLabel_7.ForeColor = Color.Black
                Case 8 : InstrumentLabel_8.ForeColor = Color.Black
                Case 9 : InstrumentLabel_9.ForeColor = Color.Black
                Case 10 : InstrumentLabel_10.ForeColor = Color.Black
                Case 11 : InstrumentLabel_11.ForeColor = Color.Black
                Case 12 : InstrumentLabel_12.ForeColor = Color.Black
                Case 13 : InstrumentLabel_13.ForeColor = Color.Black
                Case 14 : InstrumentLabel_14.ForeColor = Color.Black
                Case 15 : InstrumentLabel_15.ForeColor = Color.Black
                Case 16 : InstrumentLabel_16.ForeColor = Color.Black
                Case 17 : InstrumentLabel_17.ForeColor = Color.Black
                Case 18 : InstrumentLabel_18.ForeColor = Color.Black
                Case 19 : InstrumentLabel_19.ForeColor = Color.Black
                Case 20 : InstrumentLabel_20.ForeColor = Color.Black
                Case 21 : InstrumentLabel_21.ForeColor = Color.Black
                Case 22 : InstrumentLabel_22.ForeColor = Color.Black
                Case 23 : InstrumentLabel_23.ForeColor = Color.Black
                Case 24 : InstrumentLabel_24.ForeColor = Color.Black
                Case 25 : InstrumentLabel_25.ForeColor = Color.Black
            End Select

            dProg += (100 / iNumOfTests)
            proProgress.Value = Int(dProg)
            Application.DoEvents()
        Next iTest

        'Check for Massive failures
        If SystemStatus = FAILED Then
            CheckMassiveFailure()
        End If

        StartPPUTest = False
        StartDTSTest = False
        STestComplete = True
        sbrUserInformation.Text = "Ready."
        proProgress.Visible = False
        proProgress.Value = 0
        sspSTestPanel.Enabled = True
        STestBusy = False
    End Sub


    Private Sub frmSTest_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Dim Cancel As Short = 0
        Form_QueryUnload(Cancel, 0)
        If Cancel <> 0 Then
            e.Cancel = True
            Exit Sub
        End If
    End Sub

    Private Sub Form_QueryUnload(ByRef Cancel As Short, ByVal UnloadMode As Short)


        Dim x As Integer
        Dim s As String

        Cancel = True
        x = MsgBox("Are you sure that you want to quit?", MsgBoxStyle.YesNo)
        If x <> 6 Then Exit Sub
        s = "If you are finished running selftest, then after the program closes," & vbCrLf
        s = s & " remove the SAIF and all selftest cables and adapters including" & vbCrLf
        s = s & " SP2, SP3, SP4, W206 and W209 on the back of the CIC." & vbCrLf & vbCrLf
        s = s & "Note: SP3 at CIC J5 can cause the station to restart after power down."
        x = MsgBox(s, MsgBoxStyle.OkOnly)
        Cancel = False
        CloseProgram = True ' This is needed after EndProgram completes 
        AbortTest = True    ' AbortTest will jump to the end of any instrument test and reset instruments

        If cmdClose.Width <> 200 Then
            cmdClose.Focus()
            cmdClose.Text = "Closing, Please Wait..."
            cmdClose.Font = New Font(cmdClose.Font.FontFamily, 24)
            cmdClose.Width = 200
            cmdClose.Height = 200
            cmdClose.Left = (Me.ClientRectangle.Width - 200) / 2
            cmdClose.Top = (Me.ClientRectangle.Height - 200) / 2
            cmdClose.BringToFront()
            cmdClose.Visible = True
            Delay(1)
        End If
        Application.DoEvents()
        PauseTest = False

        Call EndProgram() ' needed to append the system log file
        Application.Exit()


    End Sub


    Private Sub InstrumentLabel_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles InstrumentLabel_0.MouseDown, _
        InstrumentLabel_1.MouseDown, InstrumentLabel_2.MouseDown, InstrumentLabel_3.MouseDown, InstrumentLabel_4.MouseDown, InstrumentLabel_5.MouseDown, _
        InstrumentLabel_6.MouseDown, InstrumentLabel_7.MouseDown, InstrumentLabel_8.MouseDown, InstrumentLabel_9.MouseDown, InstrumentLabel_10.MouseDown, _
        InstrumentLabel_11.MouseDown, InstrumentLabel_12.MouseDown, InstrumentLabel_13.MouseDown, InstrumentLabel_14.MouseDown, InstrumentLabel_15.MouseDown, _
        InstrumentLabel_16.MouseDown, InstrumentLabel_17.MouseDown, InstrumentLabel_18.MouseDown, InstrumentLabel_19.MouseDown, InstrumentLabel_20.MouseDown, _
        InstrumentLabel_21.MouseDown, InstrumentLabel_22.MouseDown, InstrumentLabel_23.MouseDown, InstrumentLabel_24.MouseDown, InstrumentLabel_25.MouseDown

    'This is used to show instrument dependancy (ie frmDepends) when operator right clicks on any instrument name
    'Uses the sscTest_MouseDown event handler

    Dim iLabel As Integer
    Dim ButtonX As Short = e.Button \ &H100000
    Dim ShiftX As Short = Control.ModifierKeys \ &H10000

    Dim senderlabel As Label = sender
        Select Case senderlabel.Name
            Case "InstrumentLabel_0" : Exit Sub
            Case "InstrumentLabel_1" : iLabel = 1 '1553
            Case "InstrumentLabel_2" : iLabel = 2 'DTS
            Case "InstrumentLabel_3" : iLabel = 3 'LF1
            Case "InstrumentLabel_4" : iLabel = 4 'LF2
            Case "InstrumentLabel_5" : iLabel = 5 'LF3
            Case "InstrumentLabel_6" : iLabel = 6 'MF
            Case "InstrumentLabel_7" : iLabel = 7 'HF
            Case "InstrumentLabel_8" : iLabel = 8 'DMM
            Case "InstrumentLabel_9" : iLabel = 9 'DSO
            Case "InstrumentLabel_10" : iLabel = 10 'C/T
            Case "InstrumentLabel_11" : iLabel = 11 'ARB
            Case "InstrumentLabel_12" : iLabel = 12 'FG
            Case "InstrumentLabel_13" : iLabel = 13 'PPU
            Case "InstrumentLabel_14" : iLabel = 14 'S/R
            Case "InstrumentLabel_15" : iLabel = 15 'SERIAL
            Case "InstrumentLabel_16" : iLabel = 16 'RS422
            Case "InstrumentLabel_17" : iLabel = 17 'RS232
            Case "InstrumentLabel_18" : iLabel = 18 'GIGI
            Case "InstrumentLabel_19" : iLabel = 19 'CAN
            Case "InstrumentLabel_20" : iLabel = 20 'APROBE
            Case "InstrumentLabel_21" : iLabel = 21 'RFPWR
            Case "InstrumentLabel_22" : iLabel = 22 'RFCOUNTER
            Case "InstrumentLabel_23" : iLabel = 23 'RFDOWNCONVERTER
            Case "InstrumentLabel_24" : iLabel = 24 'RFMODANAL
            Case "InstrumentLabel_25" : iLabel = 25 'RFSTIM
        End Select

    'InstrumentLabel_MouseDown(index, Button, Shift, x, Y)
        If (iLabel > 0) And (ButtonX = 2) Then
            sscTest_MouseDown(iLabel, ButtonX, ShiftX)
        End If

    End Sub

    Private Sub proProgress_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles proProgress.Click

        HelpContextID = 1010
        Me.Refresh()

    End Sub

    Private Sub sscTest_Click(sender As Object, e As EventArgs) Handles sscTest_0.Click, _
        sscTest_1.Click, sscTest_2.Click, sscTest_3.Click, sscTest_4.Click, sscTest_5.Click, _
        sscTest_6.Click, sscTest_7.Click, sscTest_8.Click, sscTest_9.Click, sscTest_10.Click, _
        sscTest_11.Click, sscTest_12.Click, sscTest_13.Click, sscTest_14.Click, sscTest_15.Click, _
        sscTest_16.Click, sscTest_17.Click, sscTest_18.Click, sscTest_19.Click, sscTest_20.Click, _
        sscTest_21.Click, sscTest_22.Click, sscTest_23.Click, sscTest_24.Click, sscTest_25.Click

    'This is used to start an E2E or any single test

    Dim senderButton As Button = sender
        If StartReady = False Then Exit Sub
        If TestRunning Then Exit Sub ' busy, don't allow starting a new test, if any test is running
        If proProgress.Visible = True Or PauseTest = True Then Exit Sub
        TestRunning = True
        RunningEndToEnd = False
        FirstPass = True
        StartDCPSTest = False

        If MouseButtonX = 1 Then
            Select Case senderButton.Name
                Case "sscTest_0" : HelpContextID = 1100 : TestIndex = 0 : RunningEndToEnd = True ' Exit Sub
                Case "sscTest_1" : HelpContextID = 1110 : TestIndex = 1 '1553
                Case "sscTest_2" : HelpContextID = 1120 : TestIndex = 2 'DTS
                Case "sscTest_3" : HelpContextID = 1130 : TestIndex = 3 'LF1
                Case "sscTest_4" : HelpContextID = 1140 : TestIndex = 4 'LF2
                Case "sscTest_5" : HelpContextID = 1150 : TestIndex = 5 'LF3
                Case "sscTest_6" : HelpContextID = 1160 : TestIndex = 6 'MF
                Case "sscTest_7" : HelpContextID = 1170 : TestIndex = 7 'HF
                Case "sscTest_8" : HelpContextID = 1180 : TestIndex = 8 'DMM
                Case "sscTest_9" : HelpContextID = 1190 : TestIndex = 9 'DSO
                Case "sscTest_10" : HelpContextID = 1200 : TestIndex = 10 'C/T
                Case "sscTest_11" : HelpContextID = 1210 : TestIndex = 11 'ARB
                Case "sscTest_12" : HelpContextID = 1220 : TestIndex = 12 'FG
                Case "sscTest_13" : HelpContextID = 1230 : TestIndex = 13 'PPU
                Case "sscTest_14" : HelpContextID = 1240 : TestIndex = 14 'S/R
                Case "sscTest_15" : HelpContextID = 1250 : TestIndex = 15 'SERIAL
                Case "sscTest_16" : HelpContextID = 1260 : TestIndex = 16 'RS422
                Case "sscTest_17" : HelpContextID = 1270 : TestIndex = 17 'RS232
                Case "sscTest_18" : HelpContextID = 1280 : TestIndex = 18 'GIGI
                Case "sscTest_19" : HelpContextID = 1290 : TestIndex = 19 'CANbus
                Case "sscTest_20" : HelpContextID = 1300 : TestIndex = 20 'APROBE
                Case "sscTest_21" : HelpContextID = 1310 : TestIndex = 21 'RFPWR
                Case "sscTest_22" : HelpContextID = 1320 : TestIndex = 22 'RFCOUNTER
                Case "sscTest_23" : HelpContextID = 1330 : TestIndex = 23 'RFDOWN
                Case "sscTest_24" : HelpContextID = 1340 : TestIndex = 24 'RFMODANAL
                Case "sscTest_25" : HelpContextID = 1350 : TestIndex = 25 'RFSTIM
            End Select
            PerformSelfTest(TestIndex)
        End If
        TestRunning = False
        If AbortTest = True Then
            AbortTest = False
            Me.cmdAbort.Text = "Abort Test"
        End If
        If PauseTest = True Then
            Me.cmdPause.Text = "Pause Test"
        End If
        StartReady = True

    End Sub

    Private Sub sscTest_0_MouseDown(sender As Object, e As MouseEventArgs) Handles sscTest_0.MouseDown, _
        sscTest_1.MouseDown, sscTest_2.MouseDown, sscTest_3.MouseDown, sscTest_4.MouseDown, sscTest_5.MouseDown, _
        sscTest_6.MouseDown, sscTest_7.MouseDown, sscTest_8.MouseDown, sscTest_9.MouseDown, sscTest_10.MouseDown, _
        sscTest_11.MouseDown, sscTest_12.MouseDown, sscTest_13.MouseDown, sscTest_14.MouseDown, sscTest_15.MouseDown, _
        sscTest_16.MouseDown, sscTest_17.MouseDown, sscTest_18.MouseDown, sscTest_19.MouseDown, sscTest_20.MouseDown, _
        sscTest_21.MouseDown, sscTest_22.MouseDown, sscTest_23.MouseDown, sscTest_24.MouseDown, sscTest_25.MouseDown

    Dim ShiftX As Short = Control.ModifierKeys \ &H10000
    Dim ButtonX As Button = sender
        MouseButtonX = e.Button \ &H100000 ' left=1, right=2


        Select Case ButtonX.Name
            Case "sscTest_0" : Exit Sub
            Case "sscTest_1" : TestIndex = 1 '1553
            Case "sscTest_2" : TestIndex = 2 'DTS
            Case "sscTest_3" : TestIndex = 3 'LF1
            Case "sscTest_4" : TestIndex = 4 'LF2
            Case "sscTest_5" : TestIndex = 5 'LF3
            Case "sscTest_6" : TestIndex = 6 'MF
            Case "sscTest_7" : TestIndex = 7 'HF
            Case "sscTest_8" : TestIndex = 8 'DMM
            Case "sscTest_9" : TestIndex = 9 'DSO
            Case "sscTest_10" : TestIndex = 10 'C/T
            Case "sscTest_11" : TestIndex = 11 'ARB
            Case "sscTest_12" : TestIndex = 12 'FG
            Case "sscTest_13" : TestIndex = 13 'PPU
            Case "sscTest_14" : TestIndex = 14 'S/R
            Case "sscTest_15" : TestIndex = 15 'SERIAL
            Case "sscTest_16" : TestIndex = 16 'RS422
            Case "sscTest_17" : TestIndex = 17 'RS232
            Case "sscTest_18" : TestIndex = 18 'GIGI
            Case "sscTest_19" : TestIndex = 19 'CANbus
            Case "sscTest_20" : TestIndex = 20 'APROBE
            Case "sscTest_21" : TestIndex = 21 'RFPWR
            Case "sscTest_22" : TestIndex = 22 'RFCOUNTER
            Case "sscTest_23" : TestIndex = 23 'RFDOWNCONV
            Case "sscTest_24" : TestIndex = 24 'RFMODANAL
            Case "sscTest_25" : TestIndex = 25 'RFSTIM
        End Select
        If MouseButtonX = MouseButtons.Right \ &H100000 Then
            MouseButtonX = 2
        End If
        sscTest_MouseDown(TestIndex, MouseButtonX, ShiftX)
    End Sub

    Private Sub sscTest_MouseDown(iTest As Integer, MouseButtonX As Integer, ShiftX As Integer)
        'This handles right click to show dependancy list (ie frmDepend), 
        '  and sets ControlStartTest when operator uses Ctrl left click to start an E2E on any test button,
        '  and sets StartPPUTest when operator uses Shift/Ctrl and left click on PPU Test to skip to a particular PS,
        '  and sets StartDTSTest when operator uses Shift/Ctrl and left click to skip DTS probe tests.

        Static InstrumentList As String

        ControlStartTest = 0
        StartPPUTest = False
        StartDTSTest = False
        If MouseButtonX = 1 And ShiftX = 3 Then
            If iTest = 13 Then               ' shift control and left click on PPU test
                StartPPUTest = True
            ElseIf iTest = 2 Then            ' shift control and left click on DTS test
                StartDTSTest = True
            End If                               ' shift control and left click to start E2E on any other test button
        ElseIf MouseButtonX = 1 And ShiftX = 2 Then
            ControlStartTest = iTest
        End If

        If MouseButtonX = 2 Then ' If Right button then show instrument dependency list
            InstrumentList = ""
            frmDepend.panTitle.Text = InstrumentDescription(iTest)

            Select Case iTest
                Case 1 'MIL-1553
                    HelpContextID = 1110
                    InstrumentList = "NONE"
                Case 2 'DTS
                    HelpContextID = 1120
                    InstrumentList &= sGuiLabel(OSCOPE) & vbCrLf
                    InstrumentList &= sGuiLabel(ARB) & vbCrLf
                Case 3, 4, 5, 6, 7 ' Switch 1-5
                    HelpContextID = 1100 + (iTest * 10) 'ie 1130,1140,,,1170
                    InstrumentList = sGuiLabel(DMM) & vbCrLf
                    InstrumentList &= sGuiLabel(PPU) & vbCrLf
                Case 8 'DMM
                    HelpContextID = 1180
                    InstrumentList &= sGuiLabel(ARB) & vbCrLf
                    InstrumentList &= sGuiLabel(FGEN) & vbCrLf
                    InstrumentList &= sGuiLabel(OSCOPE) & vbCrLf
                    InstrumentList &= sGuiLabel(COUNTER) & vbCrLf
                Case 9 'Scope
                    HelpContextID = 1190
                    InstrumentList &= sGuiLabel(ARB) & vbCrLf
                    InstrumentList &= sGuiLabel(FGEN) & vbCrLf
                    InstrumentList &= sGuiLabel(COUNTER) & vbCrLf
                Case 10 'Counter
                    HelpContextID = 1200
                    InstrumentList &= sGuiLabel(ARB) & vbCrLf
                    InstrumentList &= sGuiLabel(OSCOPE) & vbCrLf
                Case 11 'ARB
                    HelpContextID = 1210
                    InstrumentList &= sGuiLabel(FGEN) & vbCrLf
                    InstrumentList &= sGuiLabel(OSCOPE) & vbCrLf
                    InstrumentList &= sGuiLabel(COUNTER) & vbCrLf
                Case 12 'FGEN
                    HelpContextID = 1220
                    InstrumentList &= sGuiLabel(ARB) & vbCrLf
                    InstrumentList &= sGuiLabel(OSCOPE) & vbCrLf
                    InstrumentList &= sGuiLabel(COUNTER) & vbCrLf
                Case 13, 20 'PPU,Aprobe
                    HelpContextID = 1230
                    If (iTest = 20) Then HelpContextID = 1300
                    InstrumentList &= sGuiLabel(DMM) & vbCrLf
                Case 14, 15, 16, 17, 18, 19
                    'S/R,Serial,RS422,RS232,Giga,Canbus
                    InstrumentList = "NONE"
                    Select Case iTest
                        Case 14 : HelpContextID = 1240
                        Case 15 : HelpContextID = 1250
                        Case 16 : HelpContextID = 1260
                        Case 17 : HelpContextID = 1270
                        Case 18 : HelpContextID = 1280
                        Case 19 : HelpContextID = 1290
                    End Select

                Case 21 'RFPWR
                    InstrumentList = "NONE"
                    HelpContextID = 1310

                Case 22 'RFCOUNTER
                    HelpContextID = 1320
                    InstrumentList &= sGuiLabel(RFCOUNTER) & vbCrLf
                    InstrumentList &= sGuiLabel(RFSYN) & vbCrLf

                Case 23 'RFDOWN
                    HelpContextID = 1330
                    InstrumentList &= sGuiLabel(RFCOUNTER) & vbCrLf
                    InstrumentList &= sGuiLabel(FGEN) & vbCrLf
                    InstrumentList &= sGuiLabel(COUNTER) & vbCrLf
                    InstrumentList &= sGuiLabel(RFPM) & vbCrLf

                Case 24 'RFMA
                    HelpContextID = 1340
                    InstrumentList &= sGuiLabel(FGEN) & vbCrLf
                    InstrumentList &= sGuiLabel(RFCOUNTER) & vbCrLf
                    InstrumentList &= sGuiLabel(RFPM) & vbCrLf

                Case 25 'RFSTIM
                    HelpContextID = 1350
                    InstrumentList &= sGuiLabel(RFPM) & vbCrLf
                    InstrumentList &= sGuiLabel(RFCOUNTER) & vbCrLf
                    InstrumentList &= sGuiLabel(RFMODANAL) & vbCrLf
                    InstrumentList &= sGuiLabel(OSCOPE) & vbCrLf
                    InstrumentList &= sGuiLabel(FGEN) & vbCrLf

            End Select

            If iTest <> 0 Then
                frmDepend.Image1.Image = imlButtons.Images(iTest)
                frmDepend.txtInstrumentList.Text = InstrumentList
                CenterForm(frmDepend)
                If (Modal > 0) Then
                    frmDepend.ShowDialog()
                Else
                    ShowModeless(frmDepend)
                End If
            End If
        End If


    End Sub

    Public Sub ShowModeless(ByRef pForm As frmDepend)
        If pForm.IsDisposed Then pForm = New frmDepend()
        pForm.Show()
    End Sub

    Public Sub Unload(ByRef pForm As frmDepend)
        If Not pForm.IsDisposed Then pForm.Hide()
    End Sub


    Private Sub sscTest_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles sscTest_0.MouseMove,
        sscTest_1.MouseMove, sscTest_2.MouseMove, sscTest_3.MouseMove, sscTest_4.MouseMove, sscTest_5.MouseMove, _
        sscTest_6.MouseMove, sscTest_7.MouseMove, sscTest_8.MouseMove, sscTest_9.MouseMove, sscTest_10.MouseMove, _
        sscTest_11.MouseMove, sscTest_12.MouseMove, sscTest_13.MouseMove, sscTest_14.MouseMove, sscTest_15.MouseMove, _
        sscTest_16.MouseMove, sscTest_17.MouseMove, sscTest_18.MouseMove, sscTest_19.MouseMove, sscTest_20.MouseMove, _
        sscTest_21.MouseMove, sscTest_22.MouseMove, sscTest_23.MouseMove, sscTest_24.MouseMove, sscTest_25.MouseMove

    Dim InstIndex As Integer
    Dim Button As Short = e.Button \ &H100000
    Dim Shift As Short = Control.ModifierKeys \ &H10000
    Dim x As Single = e.X
    Dim Y As Single = e.Y
    Dim ButtonX As Button = sender

        Select Case ButtonX.Name
            Case "sscTest_0" : InstIndex = 0 ' E2E
            Case "sscTest_1" : InstIndex = 1 '1553
            Case "sscTest_2" : InstIndex = 2 'DTS
            Case "sscTest_3" : InstIndex = 3 'LF1
            Case "sscTest_4" : InstIndex = 4 'LF2
            Case "sscTest_5" : InstIndex = 5 'LF3
            Case "sscTest_6" : InstIndex = 6 'MF
            Case "sscTest_7" : InstIndex = 7 'HF

            Case "sscTest_8" : InstIndex = 8 'DMM
            Case "sscTest_9" : InstIndex = 9 'DSO
            Case "sscTest_10" : InstIndex = 10 'C/T
            Case "sscTest_11" : InstIndex = 11 'ARB
            Case "sscTest_12" : InstIndex = 12 'FG

            Case "sscTest_13" : InstIndex = 13 'PPU
            Case "sscTest_14" : InstIndex = 14 'S/R
            Case "sscTest_15" : InstIndex = 15 'SERIAL
            Case "sscTest_16" : InstIndex = 16 'RS422
            Case "sscTest_17" : InstIndex = 17 'RS232
            Case "sscTest_18" : InstIndex = 18 'GIGI
            Case "sscTest_19" : InstIndex = 19 'CANbus
            Case "sscTest_20" : InstIndex = 20 'APROBE
            Case "sscTest_21" : InstIndex = 21 'RFPWRMETER
            Case "sscTest_22" : InstIndex = 22 'RFCOUNTER
            Case "sscTest_23" : InstIndex = 23 'RFDOWNCONV
            Case "sscTest_24" : InstIndex = 24 'RFMODANAL
            Case "sscTest_25" : InstIndex = 25 'RFSTIM
        End Select
        UpdateStatus(InstIndex)
    End Sub

    Private Sub cmdAbort_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmdAbort.MouseMove

        UpdateStatus(ABORT_BUTTON)
    End Sub

    Private Sub CmdClose_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmdClose.MouseMove
        UpdateStatus(CLOSE_BUTTON)
    End Sub

    Private Sub CmdHelp_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmdHelp.MouseMove
        UpdateStatus(HELP_BUTTON)
    End Sub

    Private Sub CmdPause_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmdPause.MouseMove
        UpdateStatus(PAUSE_BUTTON)
    End Sub

    Private Sub DetailsText_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles DetailsText.MouseMove
        UpdateStatus(GENERAL_MESSAGE)
    End Sub

    Private Sub frmSTest_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseMove
        UpdateStatus(GENERAL_MESSAGE)
    End Sub

    Private Sub proProgress_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles proProgress.MouseMove
        UpdateStatus(PROGRESS_BAR)
    End Sub

    Private Sub sbrUserInformation_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles sbrUserInformation.MouseMove
        UpdateStatus(GENERAL_MESSAGE)
    End Sub

    Private Sub sspSTestPanel_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles sspSTestPanel.MouseMove
        UpdateStatus(GENERAL_MESSAGE)
    End Sub

    Private Sub cmdDetails_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmdDetails.MouseMove
        UpdateStatus(DETAILS_BUTTON)
    End Sub

    Private Sub CmdSOE_MouseMove(sender As Object, e As MouseEventArgs) Handles cmdSOE.MouseMove
        UpdateStatus(SOE_BUTTON)
    End Sub
    Private Sub CmdlOE_MouseMove(sender As Object, e As MouseEventArgs) Handles cmdLOE.MouseMove
        UpdateStatus(LOE_BUTTON)
    End Sub

    Private Sub CmdLOS_MouseMove(sender As Object, e As MouseEventArgs) Handles cmdLOS.MouseMove
        UpdateStatus(LOS_BUTTON)
    End Sub

    Private Sub CmdSOF_MouseMove(sender As Object, e As MouseEventArgs) Handles cmdSOF.MouseMove
        UpdateStatus(SOF_BUTTON)
    End Sub

    Private Sub CmdCOF_MouseMove(sender As Object, e As MouseEventArgs) Handles cmdCOF.MouseMove
        UpdateStatus(COF_BUTTON)
    End Sub

    Sub UpdateStatus(InstIndex As Integer)
        'DESCRIPTION:
        '   This routine updates the status bar at the bottom of the main panel
        'PARAMETERS:
        '   This is an index which references the message to be displayed
        '   on the main panel status bar


        Select Case InstIndex
            Case Is >= 0 ' (0 to 9)

                If Not STestBusy Then
                    If InstIndex = 0 Then
                        Me.sbrUserInformation.Text = "Press this button to run all System Self Tests."
                    Else
                        Me.sbrUserInformation.Text = "Press this button to run the " & InstrumentDescription(InstIndex) & " test. Right-click to view dependencies."
                    End If
                End If

            Case GENERAL_MESSAGE ' (-1)
                If Not STestBusy Then
                    Me.sbrUserInformation.Text = "Ready."
                Else
                    Me.sbrUserInformation.Text = TestingMessage
                End If
            Case CLOSE_BUTTON ' (-2)
                Me.sbrUserInformation.Text = "Press this button to shut down the self test program."
            Case ABORT_BUTTON ' (-3)
                Me.sbrUserInformation.Text = "Press this button to abort the test currently running."
            Case DETAILS_BUTTON ' (-4)
                If DetailStatus = True Then
                    Me.sbrUserInformation.Text = "Press this button to hide detailed testing information."
                Else
                    Me.sbrUserInformation.Text = "Press this button to view detailed testing information."
                End If
            Case DETAILS_TEXT ' (-5)
                '    frmSTest.sbrUserInformation.Text = "Detailed testing information log."
            Case PROGRESS_BAR ' (-6)
                '    frmSTest.sbrUserInformation.Text = "Progress of current test procedure."
            Case HELP_BUTTON ' (-7)
                Me.sbrUserInformation.Text = "Press this button to open the Selftest Help program."

            Case SOEmode ' (-20)
                Me.sbrUserInformation.Text = "Press this button to select ""SOE"" (Stop On End) RUN mode."
            Case LOEmode ' (-21)
                Me.sbrUserInformation.Text = "Press this button to select ""LOE"" (Loop On End) RUN mode."
            Case LOSmode ' (-22)
                Me.sbrUserInformation.Text = "Press this button to select ""LOS"" (Loop On Step) RUN mode."
            Case SOFmode ' (-23)
                Me.sbrUserInformation.Text = "Press this button to select ""SOF"" (Stop On Fault) FAULT mode."
            Case COFmode ' (-24)
                Me.sbrUserInformation.Text = "Press this button to select ""COF"" (Continue on Fault) FAULT mode."

            Case -25 ' cmbSelectTest
                Me.sbrUserInformation.Text = "Use this drop down box to select ""Test"" for LOS mode."
            Case -26 ' cmbSelectStep
                Me.sbrUserInformation.Text = "Use this drop down box to select ""Test Step"" for LOS mode."
        End Select

    End Sub



    Private Sub timTimer_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles timTimer.Tick
        'DESCRIPTION:
        '   This routine causes the label of the executing test to blink every .3 seconds

        Static ColorRed As Short
        Dim LableToBlink As Short
        If Strings.Right(Me.timTimer.Tag, 1) = "E" Then
            LableToBlink = CShort(Strings.Left(Me.timTimer.Tag, Len(Me.timTimer.Tag) - 1))
        Else
            LableToBlink = CShort(Me.timTimer.Tag)
        End If
        If ColorRed Then
            Select Case LableToBlink
                Case 1 : InstrumentLabel_1.ForeColor = Color.Red
                Case 2 : InstrumentLabel_2.ForeColor = Color.Red
                Case 3 : InstrumentLabel_3.ForeColor = Color.Red
                Case 4 : InstrumentLabel_4.ForeColor = Color.Red
                Case 5 : InstrumentLabel_5.ForeColor = Color.Red
                Case 6 : InstrumentLabel_6.ForeColor = Color.Red
                Case 7 : InstrumentLabel_7.ForeColor = Color.Red
                Case 8 : InstrumentLabel_8.ForeColor = Color.Red
                Case 9 : InstrumentLabel_9.ForeColor = Color.Red
                Case 10 : InstrumentLabel_10.ForeColor = Color.Red
                Case 11 : InstrumentLabel_11.ForeColor = Color.Red
                Case 12 : InstrumentLabel_12.ForeColor = Color.Red
                Case 13 : InstrumentLabel_13.ForeColor = Color.Red
                Case 14 : InstrumentLabel_14.ForeColor = Color.Red
                Case 15 : InstrumentLabel_15.ForeColor = Color.Red
                Case 16 : InstrumentLabel_16.ForeColor = Color.Red
                Case 17 : InstrumentLabel_17.ForeColor = Color.Red
                Case 18 : InstrumentLabel_18.ForeColor = Color.Red
                Case 19 : InstrumentLabel_19.ForeColor = Color.Red
                Case 20 : InstrumentLabel_20.ForeColor = Color.Red
                Case 21 : InstrumentLabel_21.ForeColor = Color.Red
                Case 22 : InstrumentLabel_22.ForeColor = Color.Red
                Case 23 : InstrumentLabel_23.ForeColor = Color.Red
                Case 24 : InstrumentLabel_24.ForeColor = Color.Red
                Case 25 : InstrumentLabel_25.ForeColor = Color.Red
            End Select

            If Strings.Right(Me.timTimer.Tag, 1) = "E" Then ' Check to see if an end-to-end test is running
                Select Case LableToBlink
                    Case 1 : InstrumentLabel_1.ForeColor = Color.Red
                    Case 2 : InstrumentLabel_2.ForeColor = Color.Red
                    Case 3 : InstrumentLabel_3.ForeColor = Color.Red
                    Case 4 : InstrumentLabel_4.ForeColor = Color.Red
                    Case 5 : InstrumentLabel_5.ForeColor = Color.Red
                    Case 6 : InstrumentLabel_6.ForeColor = Color.Red
                    Case 7 : InstrumentLabel_7.ForeColor = Color.Red
                    Case 8 : InstrumentLabel_8.ForeColor = Color.Red
                    Case 9 : InstrumentLabel_9.ForeColor = Color.Red
                    Case 10 : InstrumentLabel_10.ForeColor = Color.Red
                    Case 11 : InstrumentLabel_11.ForeColor = Color.Red
                    Case 12 : InstrumentLabel_12.ForeColor = Color.Red
                    Case 13 : InstrumentLabel_13.ForeColor = Color.Red
                    Case 14 : InstrumentLabel_14.ForeColor = Color.Red
                    Case 15 : InstrumentLabel_15.ForeColor = Color.Red
                    Case 16 : InstrumentLabel_16.ForeColor = Color.Red
                    Case 17 : InstrumentLabel_17.ForeColor = Color.Red
                    Case 18 : InstrumentLabel_18.ForeColor = Color.Red
                    Case 19 : InstrumentLabel_19.ForeColor = Color.Red
                    Case 20 : InstrumentLabel_20.ForeColor = Color.Red
                    Case 21 : InstrumentLabel_21.ForeColor = Color.Red
                    Case 22 : InstrumentLabel_22.ForeColor = Color.Red
                    Case 23 : InstrumentLabel_23.ForeColor = Color.Red
                    Case 24 : InstrumentLabel_24.ForeColor = Color.Red
                    Case 25 : InstrumentLabel_25.ForeColor = Color.Red
                End Select
            End If
            ColorRed = False
        Else
            Select Case LableToBlink
                Case 1 : InstrumentLabel_1.ForeColor = Color.Black
                Case 2 : InstrumentLabel_2.ForeColor = Color.Black
                Case 3 : InstrumentLabel_3.ForeColor = Color.Black
                Case 4 : InstrumentLabel_4.ForeColor = Color.Black
                Case 5 : InstrumentLabel_5.ForeColor = Color.Black
                Case 6 : InstrumentLabel_6.ForeColor = Color.Black
                Case 7 : InstrumentLabel_7.ForeColor = Color.Black
                Case 8 : InstrumentLabel_8.ForeColor = Color.Black
                Case 9 : InstrumentLabel_9.ForeColor = Color.Black
                Case 10 : InstrumentLabel_10.ForeColor = Color.Black
                Case 11 : InstrumentLabel_11.ForeColor = Color.Black
                Case 12 : InstrumentLabel_12.ForeColor = Color.Black
                Case 13 : InstrumentLabel_13.ForeColor = Color.Black
                Case 14 : InstrumentLabel_14.ForeColor = Color.Black
                Case 15 : InstrumentLabel_15.ForeColor = Color.Black
                Case 16 : InstrumentLabel_16.ForeColor = Color.Black
                Case 17 : InstrumentLabel_17.ForeColor = Color.Black
                Case 18 : InstrumentLabel_18.ForeColor = Color.Black
                Case 19 : InstrumentLabel_19.ForeColor = Color.Black
                Case 20 : InstrumentLabel_20.ForeColor = Color.Black
                Case 21 : InstrumentLabel_21.ForeColor = Color.Black
                Case 22 : InstrumentLabel_22.ForeColor = Color.Black
                Case 23 : InstrumentLabel_23.ForeColor = Color.Black
                Case 24 : InstrumentLabel_24.ForeColor = Color.Black
                Case 25 : InstrumentLabel_25.ForeColor = Color.Black
            End Select
            If Strings.Right(Me.timTimer.Tag, 1) = "E" Then ' Check to see if an end-to-end test is running
                InstrumentLabel_0.ForeColor = Color.Black
            End If
            ColorRed = True
        End If

        If AbortTest = True Then
            If Me.cmdAbort.Text <> "Aborting" Then
                Me.cmdAbort.Text = "Aborting"
            Else
                Me.cmdAbort.Text = "Abort Test"
            End If

        Else
            Me.cmdAbort.Text = "Abort Test"
        End If

        If PauseTest = True Then
            If Me.cmdPause.Text <> "Continue Test" Then
                Me.cmdPause.Text = "Continue Test"
            Else
                Me.cmdPause.Text = ""
            End If
        Else
            Me.cmdPause.Text = "Pause Test"
        End If

    End Sub

    Private Sub Timer2_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        'DESCRIPTION:
        '   This routine executes whenever Timer2 times out
        Timer2timeout = True

    End Sub

    Sub FindWord()

        Dim x2 As Integer
        If Len(SearchWord) = 0 Then Exit Sub

        'find next one
        x2 = DetailsText.SelectionStart
        x2 = InStr(x2 + 2, DetailsText.Text, SearchWord, CompareMethod.Text)
        If x2 > 0 Then ' found one
            DetailsText.SelectionStart = x2 - 1
            DetailsText.SelectionLength = Len(SearchWord)
            DetailsText.Focus()
        Else            ' wrap around
            x2 = InStr(1, DetailsText.Text, SearchWord, CompareMethod.Text)
            If x2 > 0 Then
                DetailsText.SelectionStart = x2 - 1
                DetailsText.SelectionLength = Len(SearchWord)
                If DetailsText.Visible = True And DetailsText.Enabled = True Then
                    DetailsText.Focus()
                End If
            Else                ' could not find one at all
                MsgBox("" & SearchWord & "" & " not found!")
            End If
        End If

    End Sub

    Private Sub TxtSearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtSearch.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)


        If KeyAscii = key_Enter Then
            CmdSearchOK_Click()
        End If


        e.KeyChar = Chr(KeyAscii)
    End Sub


    Private Sub cmdHelp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdHelp.Click

        '' This enables the old style help file (replaced by a chm file)
        '' Use this if only the old style help file is available
        ''If Dir$(ProgramPath & "Stest1.hlp", 17) <> "" Then
        ''  If HelpContextID = 0 Then
        ''      Process.Start(ProgramPath & "Stest1.hlp")
        ''  Else
        ''      Dim p As New ProcessStartInfo ' New ProcessStartInfo created
        ''      p.FileName = "WINHLP32.exe"
        ''      p.Arguments = "-N " & CStr(HelpContextID) & " " & ProgramPath & "Stest1.hlp"
        ''      Process.Start(p)
        ''  End If
        ''End if

        'used for new style help file
        If Dir$(ProgramPath & "Stest_TETS_1.chm", 17) <> "" Then
            If HelpContextID = 0 Then
                Help.ShowHelp(Me, ProgramPath & "\Stest_TETS_1.chm", HelpNavigator.TableOfContents)
            Else
                ' Help.ShowHelp(Me, ProgramPath & "\Stest_TETS_1.chm", HelpNavigator.TopicId, "1100")
                Help.ShowHelp(Me, ProgramPath & "\Stest_TETS_1.chm", HelpNavigator.TopicId, CStr(HelpContextID))
            End If
        End If


    End Sub

End Class