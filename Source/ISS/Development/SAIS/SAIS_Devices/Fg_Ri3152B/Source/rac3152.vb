
Imports System.Windows.Forms
Imports System.Windows.Forms.Screen
Imports Microsoft.VisualBasic

Public Class frmRac3152
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
    Friend WithEvents cmdHelp As System.Windows.Forms.Button
    Friend WithEvents CommonDialog1 As System.Windows.Forms.PrintDialog
    Friend WithEvents cmdQuit As System.Windows.Forms.Button
    Friend WithEvents cmdReset As System.Windows.Forms.Button
    Friend WithEvents fraOnOff As System.Windows.Forms.GroupBox
    Friend WithEvents panFGDisplay As System.Windows.Forms.Panel
    Friend WithEvents imgFGDisplay As System.Windows.Forms.PictureBox
    Friend WithEvents tabOptions As System.Windows.Forms.TabControl
    Friend WithEvents tabOptions_Page1 As System.Windows.Forms.TabPage
    Friend WithEvents fraSinc As System.Windows.Forms.GroupBox
    Friend WithEvents panSinc As System.Windows.Forms.Panel
    Friend WithEvents txtSinc As System.Windows.Forms.TextBox
    Friend WithEvents spnSinc As NationalInstruments.UI.WindowsForms.NumericEdit
    Friend WithEvents fraTrai As System.Windows.Forms.GroupBox
    Friend WithEvents panTrai As System.Windows.Forms.Panel
    Friend WithEvents txtTrai As System.Windows.Forms.TextBox
    Friend WithEvents spnTrai As NationalInstruments.UI.WindowsForms.NumericEdit
    Friend WithEvents fraLead As System.Windows.Forms.GroupBox
    Friend WithEvents panLead As System.Windows.Forms.Panel
    Friend WithEvents txtLead As System.Windows.Forms.TextBox
    Friend WithEvents spnLead As NationalInstruments.UI.WindowsForms.NumericEdit
    Friend WithEvents fraDuty As System.Windows.Forms.GroupBox
    Friend WithEvents panDuty As System.Windows.Forms.Panel
    Friend WithEvents txtDuty As System.Windows.Forms.TextBox
    Friend WithEvents spnDuty As NationalInstruments.UI.WindowsForms.NumericEdit
    Friend WithEvents fraWidt As System.Windows.Forms.GroupBox
    Friend WithEvents panWidt As System.Windows.Forms.Panel
    Friend WithEvents txtWidt As System.Windows.Forms.TextBox
    Friend WithEvents spnWidt As NationalInstruments.UI.WindowsForms.NumericEdit
    Friend WithEvents fraDela As System.Windows.Forms.GroupBox
    Friend WithEvents panDela As System.Windows.Forms.Panel
    Friend WithEvents txtDela As System.Windows.Forms.TextBox
    Friend WithEvents spnDela As NationalInstruments.UI.WindowsForms.NumericEdit
    Friend WithEvents fraPhas As System.Windows.Forms.GroupBox
    Friend WithEvents panPhas As System.Windows.Forms.Panel
    Friend WithEvents txtPhas As System.Windows.Forms.TextBox
    Friend WithEvents spnPhas As NationalInstruments.UI.WindowsForms.NumericEdit
    Friend WithEvents fraExpo As System.Windows.Forms.GroupBox
    Friend WithEvents panExpo As System.Windows.Forms.Panel
    Friend WithEvents txtExpo As System.Windows.Forms.TextBox
    Friend WithEvents spnExpo As NationalInstruments.UI.WindowsForms.NumericEdit
    Friend WithEvents fraDcOffs As System.Windows.Forms.GroupBox
    Friend WithEvents panDcOffs As System.Windows.Forms.Panel
    Friend WithEvents txtDcOffs As System.Windows.Forms.TextBox
    Friend WithEvents spnDcOffs As NationalInstruments.UI.WindowsForms.NumericEdit
    Friend WithEvents fraFreq As System.Windows.Forms.GroupBox
    Friend WithEvents panFreq As System.Windows.Forms.Panel
    Friend WithEvents txtFreq As System.Windows.Forms.TextBox
    Friend WithEvents spnFreq As NationalInstruments.UI.WindowsForms.NumericEdit
    Friend WithEvents fraAmpl As System.Windows.Forms.GroupBox
    Friend WithEvents panAmpl As System.Windows.Forms.Panel
    Friend WithEvents txtAmpl As System.Windows.Forms.TextBox
    Friend WithEvents spnAmpl As NationalInstruments.UI.WindowsForms.NumericEdit
    Friend WithEvents fraFunc As System.Windows.Forms.GroupBox
    Friend WithEvents tolFunc As System.Windows.Forms.ToolBar
    Friend WithEvents Sine As System.Windows.Forms.ToolBarButton
    Friend WithEvents Square As System.Windows.Forms.ToolBarButton
    Friend WithEvents Pulse As System.Windows.Forms.ToolBarButton
    Friend WithEvents Ramp As System.Windows.Forms.ToolBarButton
    Friend WithEvents Triangle As System.Windows.Forms.ToolBarButton
    Friend WithEvents Exponential As System.Windows.Forms.ToolBarButton
    Friend WithEvents Gaussian As System.Windows.Forms.ToolBarButton
    Friend WithEvents Sinc As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnDC As System.Windows.Forms.ToolBarButton
    Friend WithEvents tabOptions_Page2 As System.Windows.Forms.TabPage
    Friend WithEvents fraModeTrigLevel As System.Windows.Forms.GroupBox
    Friend WithEvents panModeTrigLevel As System.Windows.Forms.Panel
    Friend WithEvents txtModeTrigLevel As System.Windows.Forms.TextBox
    Friend WithEvents spnModeTrigLevel As NationalInstruments.UI.WindowsForms.NumericEdit
    Friend WithEvents fraModeSelect As System.Windows.Forms.GroupBox
    Friend WithEvents optModeGated As System.Windows.Forms.RadioButton
    Friend WithEvents optModeBurst As System.Windows.Forms.RadioButton
    Friend WithEvents optModeTrig As System.Windows.Forms.RadioButton
    Friend WithEvents optModeCont As System.Windows.Forms.RadioButton
    Friend WithEvents fraModeTrigDela As System.Windows.Forms.GroupBox
    Friend WithEvents panModeTrigDela As System.Windows.Forms.Panel
    Friend WithEvents txtModeTrigDela As System.Windows.Forms.TextBox
    Friend WithEvents spnModeTrigDela As NationalInstruments.UI.WindowsForms.NumericEdit
    Friend WithEvents fraModeTrigBurst As System.Windows.Forms.GroupBox
    Friend WithEvents panModeTrigBurst As System.Windows.Forms.Panel
    Friend WithEvents txtModeTrigBurst As System.Windows.Forms.TextBox
    Friend WithEvents spnModeTrigBurst As NationalInstruments.UI.WindowsForms.NumericEdit
    Friend WithEvents fraModeTrigTime As System.Windows.Forms.GroupBox
    Friend WithEvents panModeTrigTime As System.Windows.Forms.Panel
    Friend WithEvents txtModeTrigTime As System.Windows.Forms.TextBox
    Friend WithEvents spnModeTrigTime As NationalInstruments.UI.WindowsForms.NumericEdit
    Friend WithEvents fraModeTrigSlop As System.Windows.Forms.GroupBox
    Friend WithEvents cboModeTrigSlop As System.Windows.Forms.ComboBox
    Friend WithEvents fraModeTrigSour As System.Windows.Forms.GroupBox
    Friend WithEvents cboModeTrigSour As System.Windows.Forms.ComboBox
    Friend WithEvents cmdModeSoftTrig As System.Windows.Forms.Button
    Friend WithEvents tabOptions_Page3 As System.Windows.Forms.TabPage
    Friend WithEvents fraTrigOutVXI As System.Windows.Forms.GroupBox
    Friend WithEvents panIntTimer As System.Windows.Forms.Panel
    Friend WithEvents txtIntTimer As System.Windows.Forms.TextBox
    Friend WithEvents spnIntTimer As NationalInstruments.UI.WindowsForms.NumericEdit
    Friend WithEvents optTrigSourceWave As System.Windows.Forms.RadioButton
    Friend WithEvents optTrigSourceExte As System.Windows.Forms.RadioButton
    Friend WithEvents optTrigSourceTimer As System.Windows.Forms.RadioButton
    Friend WithEvents fraTrigOutSyncSour As System.Windows.Forms.GroupBox
    Friend WithEvents optSyncSourceHalfSamp As System.Windows.Forms.RadioButton
    Friend WithEvents optSyncSourceWave As System.Windows.Forms.RadioButton
    Friend WithEvents fraTrigOutSyncOut As System.Windows.Forms.GroupBox
    Friend WithEvents chkSyncOutStateOn As System.Windows.Forms.CheckBox
    Friend WithEvents fraVXIECLTrig As System.Windows.Forms.GroupBox
    Friend WithEvents cboTrigOutECL As System.Windows.Forms.ComboBox
    Friend WithEvents chkTrigOutECLOn As System.Windows.Forms.CheckBox
    Friend WithEvents fraVXITTLTrig As System.Windows.Forms.GroupBox
    Friend WithEvents cboTrigOutTTL As System.Windows.Forms.ComboBox
    Friend WithEvents chkTrigOutTTLOn As System.Windows.Forms.CheckBox
    Friend WithEvents tabOptions_Page4 As System.Windows.Forms.TabPage
    Friend WithEvents Panel_Conifg As VIPERT_Common_Controls.Panel_Conifg
    Friend WithEvents cmdUpdateTIP As System.Windows.Forms.Button
    Friend WithEvents cmdRunWaveCAD As System.Windows.Forms.Button
    Friend WithEvents cmdAbout As System.Windows.Forms.Button
    Friend WithEvents cmdTest As System.Windows.Forms.Button
    Friend WithEvents fraOutLoadImpe As System.Windows.Forms.GroupBox
    Friend WithEvents optLoadImp50 As System.Windows.Forms.RadioButton
    Friend WithEvents optLoadImpHigh As System.Windows.Forms.RadioButton
    Friend WithEvents fraOutLowPassFilt As System.Windows.Forms.GroupBox
    Friend WithEvents optCutOff50 As System.Windows.Forms.RadioButton
    Friend WithEvents optCutOff25 As System.Windows.Forms.RadioButton
    Friend WithEvents optCutOff20 As System.Windows.Forms.RadioButton
    Friend WithEvents chkFilterStateOn As System.Windows.Forms.CheckBox
    Friend WithEvents tabOptions_Page5 As System.Windows.Forms.TabPage
    Friend WithEvents Atlas_SFP As VIPERT_Common_Controls.Atlas_SFP
    Friend WithEvents sbrUserInformation As System.Windows.Forms.StatusBar
    Friend WithEvents imlFunc As System.Windows.Forms.ImageList
    Friend WithEvents btnOn As System.Windows.Forms.Button
    Friend WithEvents pcpFGDisplay As System.Windows.Forms.ImageList
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRac3152))
        Me.cmdHelp = New System.Windows.Forms.Button()
        Me.CommonDialog1 = New System.Windows.Forms.PrintDialog()
        Me.cmdQuit = New System.Windows.Forms.Button()
        Me.cmdReset = New System.Windows.Forms.Button()
        Me.fraOnOff = New System.Windows.Forms.GroupBox()
        Me.panFGDisplay = New System.Windows.Forms.Panel()
        Me.imgFGDisplay = New System.Windows.Forms.PictureBox()
        Me.btnOn = New System.Windows.Forms.Button()
        Me.tabOptions = New System.Windows.Forms.TabControl()
        Me.tabOptions_Page1 = New System.Windows.Forms.TabPage()
        Me.fraSinc = New System.Windows.Forms.GroupBox()
        Me.panSinc = New System.Windows.Forms.Panel()
        Me.txtSinc = New System.Windows.Forms.TextBox()
        Me.spnSinc = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.fraTrai = New System.Windows.Forms.GroupBox()
        Me.panTrai = New System.Windows.Forms.Panel()
        Me.txtTrai = New System.Windows.Forms.TextBox()
        Me.spnTrai = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.fraLead = New System.Windows.Forms.GroupBox()
        Me.panLead = New System.Windows.Forms.Panel()
        Me.txtLead = New System.Windows.Forms.TextBox()
        Me.spnLead = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.fraDuty = New System.Windows.Forms.GroupBox()
        Me.panDuty = New System.Windows.Forms.Panel()
        Me.txtDuty = New System.Windows.Forms.TextBox()
        Me.spnDuty = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.fraWidt = New System.Windows.Forms.GroupBox()
        Me.panWidt = New System.Windows.Forms.Panel()
        Me.txtWidt = New System.Windows.Forms.TextBox()
        Me.spnWidt = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.fraDela = New System.Windows.Forms.GroupBox()
        Me.panDela = New System.Windows.Forms.Panel()
        Me.txtDela = New System.Windows.Forms.TextBox()
        Me.spnDela = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.fraPhas = New System.Windows.Forms.GroupBox()
        Me.panPhas = New System.Windows.Forms.Panel()
        Me.txtPhas = New System.Windows.Forms.TextBox()
        Me.spnPhas = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.fraExpo = New System.Windows.Forms.GroupBox()
        Me.panExpo = New System.Windows.Forms.Panel()
        Me.txtExpo = New System.Windows.Forms.TextBox()
        Me.spnExpo = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.fraDcOffs = New System.Windows.Forms.GroupBox()
        Me.panDcOffs = New System.Windows.Forms.Panel()
        Me.txtDcOffs = New System.Windows.Forms.TextBox()
        Me.spnDcOffs = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.fraFreq = New System.Windows.Forms.GroupBox()
        Me.panFreq = New System.Windows.Forms.Panel()
        Me.txtFreq = New System.Windows.Forms.TextBox()
        Me.spnFreq = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.fraAmpl = New System.Windows.Forms.GroupBox()
        Me.panAmpl = New System.Windows.Forms.Panel()
        Me.txtAmpl = New System.Windows.Forms.TextBox()
        Me.spnAmpl = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.fraFunc = New System.Windows.Forms.GroupBox()
        Me.tolFunc = New System.Windows.Forms.ToolBar()
        Me.Sine = New System.Windows.Forms.ToolBarButton()
        Me.Square = New System.Windows.Forms.ToolBarButton()
        Me.Pulse = New System.Windows.Forms.ToolBarButton()
        Me.Ramp = New System.Windows.Forms.ToolBarButton()
        Me.Triangle = New System.Windows.Forms.ToolBarButton()
        Me.Exponential = New System.Windows.Forms.ToolBarButton()
        Me.Gaussian = New System.Windows.Forms.ToolBarButton()
        Me.Sinc = New System.Windows.Forms.ToolBarButton()
        Me.btnDC = New System.Windows.Forms.ToolBarButton()
        Me.imlFunc = New System.Windows.Forms.ImageList()
        Me.tabOptions_Page2 = New System.Windows.Forms.TabPage()
        Me.fraModeTrigLevel = New System.Windows.Forms.GroupBox()
        Me.panModeTrigLevel = New System.Windows.Forms.Panel()
        Me.txtModeTrigLevel = New System.Windows.Forms.TextBox()
        Me.spnModeTrigLevel = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.fraModeSelect = New System.Windows.Forms.GroupBox()
        Me.optModeGated = New System.Windows.Forms.RadioButton()
        Me.optModeBurst = New System.Windows.Forms.RadioButton()
        Me.optModeTrig = New System.Windows.Forms.RadioButton()
        Me.optModeCont = New System.Windows.Forms.RadioButton()
        Me.fraModeTrigDela = New System.Windows.Forms.GroupBox()
        Me.panModeTrigDela = New System.Windows.Forms.Panel()
        Me.txtModeTrigDela = New System.Windows.Forms.TextBox()
        Me.spnModeTrigDela = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.fraModeTrigBurst = New System.Windows.Forms.GroupBox()
        Me.panModeTrigBurst = New System.Windows.Forms.Panel()
        Me.txtModeTrigBurst = New System.Windows.Forms.TextBox()
        Me.spnModeTrigBurst = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.fraModeTrigTime = New System.Windows.Forms.GroupBox()
        Me.panModeTrigTime = New System.Windows.Forms.Panel()
        Me.txtModeTrigTime = New System.Windows.Forms.TextBox()
        Me.spnModeTrigTime = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.fraModeTrigSlop = New System.Windows.Forms.GroupBox()
        Me.cboModeTrigSlop = New System.Windows.Forms.ComboBox()
        Me.fraModeTrigSour = New System.Windows.Forms.GroupBox()
        Me.cboModeTrigSour = New System.Windows.Forms.ComboBox()
        Me.cmdModeSoftTrig = New System.Windows.Forms.Button()
        Me.tabOptions_Page3 = New System.Windows.Forms.TabPage()
        Me.fraTrigOutVXI = New System.Windows.Forms.GroupBox()
        Me.panIntTimer = New System.Windows.Forms.Panel()
        Me.txtIntTimer = New System.Windows.Forms.TextBox()
        Me.spnIntTimer = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.optTrigSourceWave = New System.Windows.Forms.RadioButton()
        Me.optTrigSourceExte = New System.Windows.Forms.RadioButton()
        Me.optTrigSourceTimer = New System.Windows.Forms.RadioButton()
        Me.fraTrigOutSyncSour = New System.Windows.Forms.GroupBox()
        Me.optSyncSourceHalfSamp = New System.Windows.Forms.RadioButton()
        Me.optSyncSourceWave = New System.Windows.Forms.RadioButton()
        Me.fraTrigOutSyncOut = New System.Windows.Forms.GroupBox()
        Me.chkSyncOutStateOn = New System.Windows.Forms.CheckBox()
        Me.fraVXIECLTrig = New System.Windows.Forms.GroupBox()
        Me.cboTrigOutECL = New System.Windows.Forms.ComboBox()
        Me.chkTrigOutECLOn = New System.Windows.Forms.CheckBox()
        Me.fraVXITTLTrig = New System.Windows.Forms.GroupBox()
        Me.cboTrigOutTTL = New System.Windows.Forms.ComboBox()
        Me.chkTrigOutTTLOn = New System.Windows.Forms.CheckBox()
        Me.tabOptions_Page4 = New System.Windows.Forms.TabPage()
        Me.Panel_Conifg = New VIPERT_Common_Controls.Panel_Conifg()
        Me.cmdUpdateTIP = New System.Windows.Forms.Button()
        Me.cmdRunWaveCAD = New System.Windows.Forms.Button()
        Me.cmdAbout = New System.Windows.Forms.Button()
        Me.cmdTest = New System.Windows.Forms.Button()
        Me.fraOutLoadImpe = New System.Windows.Forms.GroupBox()
        Me.optLoadImp50 = New System.Windows.Forms.RadioButton()
        Me.optLoadImpHigh = New System.Windows.Forms.RadioButton()
        Me.fraOutLowPassFilt = New System.Windows.Forms.GroupBox()
        Me.optCutOff50 = New System.Windows.Forms.RadioButton()
        Me.optCutOff25 = New System.Windows.Forms.RadioButton()
        Me.optCutOff20 = New System.Windows.Forms.RadioButton()
        Me.chkFilterStateOn = New System.Windows.Forms.CheckBox()
        Me.tabOptions_Page5 = New System.Windows.Forms.TabPage()
        Me.Atlas_SFP = New VIPERT_Common_Controls.Atlas_SFP()
        Me.sbrUserInformation = New System.Windows.Forms.StatusBar()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip()
        Me.pcpFGDisplay = New System.Windows.Forms.ImageList()
        Me.fraOnOff.SuspendLayout()
        Me.panFGDisplay.SuspendLayout()
        CType(Me.imgFGDisplay, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabOptions.SuspendLayout()
        Me.tabOptions_Page1.SuspendLayout()
        Me.fraSinc.SuspendLayout()
        Me.panSinc.SuspendLayout()
        CType(Me.spnSinc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraTrai.SuspendLayout()
        Me.panTrai.SuspendLayout()
        CType(Me.spnTrai, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraLead.SuspendLayout()
        Me.panLead.SuspendLayout()
        CType(Me.spnLead, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraDuty.SuspendLayout()
        Me.panDuty.SuspendLayout()
        CType(Me.spnDuty, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraWidt.SuspendLayout()
        Me.panWidt.SuspendLayout()
        CType(Me.spnWidt, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraDela.SuspendLayout()
        Me.panDela.SuspendLayout()
        CType(Me.spnDela, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraPhas.SuspendLayout()
        Me.panPhas.SuspendLayout()
        CType(Me.spnPhas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraExpo.SuspendLayout()
        Me.panExpo.SuspendLayout()
        CType(Me.spnExpo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraDcOffs.SuspendLayout()
        Me.panDcOffs.SuspendLayout()
        CType(Me.spnDcOffs, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraFreq.SuspendLayout()
        Me.panFreq.SuspendLayout()
        CType(Me.spnFreq, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraAmpl.SuspendLayout()
        Me.panAmpl.SuspendLayout()
        CType(Me.spnAmpl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraFunc.SuspendLayout()
        Me.tabOptions_Page2.SuspendLayout()
        Me.fraModeTrigLevel.SuspendLayout()
        Me.panModeTrigLevel.SuspendLayout()
        CType(Me.spnModeTrigLevel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraModeSelect.SuspendLayout()
        Me.fraModeTrigDela.SuspendLayout()
        Me.panModeTrigDela.SuspendLayout()
        CType(Me.spnModeTrigDela, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraModeTrigBurst.SuspendLayout()
        Me.panModeTrigBurst.SuspendLayout()
        CType(Me.spnModeTrigBurst, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraModeTrigTime.SuspendLayout()
        Me.panModeTrigTime.SuspendLayout()
        CType(Me.spnModeTrigTime, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraModeTrigSlop.SuspendLayout()
        Me.fraModeTrigSour.SuspendLayout()
        Me.tabOptions_Page3.SuspendLayout()
        Me.fraTrigOutVXI.SuspendLayout()
        Me.panIntTimer.SuspendLayout()
        CType(Me.spnIntTimer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraTrigOutSyncSour.SuspendLayout()
        Me.fraTrigOutSyncOut.SuspendLayout()
        Me.fraVXIECLTrig.SuspendLayout()
        Me.fraVXITTLTrig.SuspendLayout()
        Me.tabOptions_Page4.SuspendLayout()
        Me.fraOutLoadImpe.SuspendLayout()
        Me.fraOutLowPassFilt.SuspendLayout()
        Me.tabOptions_Page5.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdHelp
        '
        Me.cmdHelp.BackColor = System.Drawing.SystemColors.Control
        Me.cmdHelp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdHelp.Location = New System.Drawing.Point(235, 328)
        Me.cmdHelp.Name = "cmdHelp"
        Me.cmdHelp.Size = New System.Drawing.Size(76, 23)
        Me.cmdHelp.TabIndex = 110
        Me.cmdHelp.Text = "Help"
        Me.cmdHelp.UseVisualStyleBackColor = False
        '
        'cmdQuit
        '
        Me.cmdQuit.BackColor = System.Drawing.SystemColors.Control
        Me.cmdQuit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdQuit.Location = New System.Drawing.Point(401, 328)
        Me.cmdQuit.Name = "cmdQuit"
        Me.cmdQuit.Size = New System.Drawing.Size(76, 23)
        Me.cmdQuit.TabIndex = 48
        Me.cmdQuit.Text = "&Quit"
        Me.cmdQuit.UseVisualStyleBackColor = False
        '
        'cmdReset
        '
        Me.cmdReset.BackColor = System.Drawing.SystemColors.Control
        Me.cmdReset.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdReset.Location = New System.Drawing.Point(316, 328)
        Me.cmdReset.Name = "cmdReset"
        Me.cmdReset.Size = New System.Drawing.Size(76, 23)
        Me.cmdReset.TabIndex = 47
        Me.cmdReset.Text = "&Reset"
        Me.cmdReset.UseVisualStyleBackColor = False
        '
        'fraOnOff
        '
        Me.fraOnOff.Controls.Add(Me.panFGDisplay)
        Me.fraOnOff.Controls.Add(Me.btnOn)
        Me.fraOnOff.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraOnOff.Location = New System.Drawing.Point(327, 259)
        Me.fraOnOff.Name = "fraOnOff"
        Me.fraOnOff.Size = New System.Drawing.Size(105, 40)
        Me.fraOnOff.TabIndex = 1
        Me.fraOnOff.TabStop = False
        '
        'panFGDisplay
        '
        Me.panFGDisplay.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.panFGDisplay.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panFGDisplay.Controls.Add(Me.imgFGDisplay)
        Me.panFGDisplay.Enabled = False
        Me.panFGDisplay.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panFGDisplay.ForeColor = System.Drawing.SystemColors.ControlText
        Me.panFGDisplay.Location = New System.Drawing.Point(3, 3)
        Me.panFGDisplay.Margin = New System.Windows.Forms.Padding(0)
        Me.panFGDisplay.Name = "panFGDisplay"
        Me.panFGDisplay.Size = New System.Drawing.Size(40, 32)
        Me.panFGDisplay.TabIndex = 4
        '
        'imgFGDisplay
        '
        Me.imgFGDisplay.BackColor = System.Drawing.SystemColors.Control
        Me.imgFGDisplay.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.imgFGDisplay.Location = New System.Drawing.Point(1, 1)
        Me.imgFGDisplay.MaximumSize = New System.Drawing.Size(34, 26)
        Me.imgFGDisplay.MinimumSize = New System.Drawing.Size(34, 26)
        Me.imgFGDisplay.Name = "imgFGDisplay"
        Me.imgFGDisplay.Size = New System.Drawing.Size(34, 26)
        Me.imgFGDisplay.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.imgFGDisplay.TabIndex = 0
        Me.imgFGDisplay.TabStop = False
        '
        'btnOn
        '
        Me.btnOn.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOn.Location = New System.Drawing.Point(44, 1)
        Me.btnOn.Margin = New System.Windows.Forms.Padding(0)
        Me.btnOn.Name = "btnOn"
        Me.btnOn.Size = New System.Drawing.Size(62, 36)
        Me.btnOn.TabIndex = 5
        Me.btnOn.Text = "On"
        Me.btnOn.UseVisualStyleBackColor = True
        '
        'tabOptions
        '
        Me.tabOptions.Controls.Add(Me.tabOptions_Page1)
        Me.tabOptions.Controls.Add(Me.tabOptions_Page2)
        Me.tabOptions.Controls.Add(Me.tabOptions_Page3)
        Me.tabOptions.Controls.Add(Me.tabOptions_Page4)
        Me.tabOptions.Controls.Add(Me.tabOptions_Page5)
        Me.tabOptions.Location = New System.Drawing.Point(8, 0)
        Me.tabOptions.Name = "tabOptions"
        Me.tabOptions.SelectedIndex = 0
        Me.tabOptions.Size = New System.Drawing.Size(469, 318)
        Me.tabOptions.TabIndex = 0
        '
        'tabOptions_Page1
        '
        Me.tabOptions_Page1.Controls.Add(Me.fraSinc)
        Me.tabOptions_Page1.Controls.Add(Me.fraTrai)
        Me.tabOptions_Page1.Controls.Add(Me.fraLead)
        Me.tabOptions_Page1.Controls.Add(Me.fraDuty)
        Me.tabOptions_Page1.Controls.Add(Me.fraWidt)
        Me.tabOptions_Page1.Controls.Add(Me.fraDela)
        Me.tabOptions_Page1.Controls.Add(Me.fraPhas)
        Me.tabOptions_Page1.Controls.Add(Me.fraExpo)
        Me.tabOptions_Page1.Controls.Add(Me.fraDcOffs)
        Me.tabOptions_Page1.Controls.Add(Me.fraFreq)
        Me.tabOptions_Page1.Controls.Add(Me.fraAmpl)
        Me.tabOptions_Page1.Controls.Add(Me.fraFunc)
        Me.tabOptions_Page1.Location = New System.Drawing.Point(4, 22)
        Me.tabOptions_Page1.Name = "tabOptions_Page1"
        Me.tabOptions_Page1.Size = New System.Drawing.Size(461, 292)
        Me.tabOptions_Page1.TabIndex = 0
        Me.tabOptions_Page1.Text = "Functions"
        '
        'fraSinc
        '
        Me.fraSinc.Controls.Add(Me.panSinc)
        Me.fraSinc.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraSinc.Location = New System.Drawing.Point(67, 231)
        Me.fraSinc.Name = "fraSinc"
        Me.fraSinc.Size = New System.Drawing.Size(149, 43)
        Me.fraSinc.TabIndex = 105
        Me.fraSinc.TabStop = False
        Me.fraSinc.Text = "Sinc [ Sine(x) / x ]"
        '
        'panSinc
        '
        Me.panSinc.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.panSinc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panSinc.Controls.Add(Me.txtSinc)
        Me.panSinc.Controls.Add(Me.spnSinc)
        Me.panSinc.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panSinc.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.panSinc.Location = New System.Drawing.Point(5, 18)
        Me.panSinc.Name = "panSinc"
        Me.panSinc.Size = New System.Drawing.Size(139, 21)
        Me.panSinc.TabIndex = 106
        '
        'txtSinc
        '
        Me.txtSinc.BackColor = System.Drawing.SystemColors.Window
        Me.txtSinc.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSinc.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSinc.Location = New System.Drawing.Point(3, 2)
        Me.txtSinc.Name = "txtSinc"
        Me.txtSinc.Size = New System.Drawing.Size(114, 13)
        Me.txtSinc.TabIndex = 107
        '
        'spnSinc
        '
        Me.spnSinc.Location = New System.Drawing.Point(121, -2)
        Me.spnSinc.Name = "spnSinc"
        Me.spnSinc.Size = New System.Drawing.Size(16, 20)
        Me.spnSinc.TabIndex = 108
        '
        'fraTrai
        '
        Me.fraTrai.Controls.Add(Me.panTrai)
        Me.fraTrai.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraTrai.Location = New System.Drawing.Point(304, 140)
        Me.fraTrai.Name = "fraTrai"
        Me.fraTrai.Size = New System.Drawing.Size(149, 43)
        Me.fraTrai.TabIndex = 101
        Me.fraTrai.TabStop = False
        Me.fraTrai.Text = "Trailing Edge"
        '
        'panTrai
        '
        Me.panTrai.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.panTrai.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panTrai.Controls.Add(Me.txtTrai)
        Me.panTrai.Controls.Add(Me.spnTrai)
        Me.panTrai.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panTrai.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.panTrai.Location = New System.Drawing.Point(5, 17)
        Me.panTrai.Name = "panTrai"
        Me.panTrai.Size = New System.Drawing.Size(139, 21)
        Me.panTrai.TabIndex = 102
        '
        'txtTrai
        '
        Me.txtTrai.BackColor = System.Drawing.SystemColors.Window
        Me.txtTrai.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtTrai.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTrai.Location = New System.Drawing.Point(3, 2)
        Me.txtTrai.Name = "txtTrai"
        Me.txtTrai.Size = New System.Drawing.Size(114, 13)
        Me.txtTrai.TabIndex = 103
        '
        'spnTrai
        '
        Me.spnTrai.Location = New System.Drawing.Point(121, -2)
        Me.spnTrai.Name = "spnTrai"
        Me.spnTrai.Size = New System.Drawing.Size(16, 20)
        Me.spnTrai.TabIndex = 104
        '
        'fraLead
        '
        Me.fraLead.Controls.Add(Me.panLead)
        Me.fraLead.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraLead.Location = New System.Drawing.Point(304, 94)
        Me.fraLead.Name = "fraLead"
        Me.fraLead.Size = New System.Drawing.Size(149, 43)
        Me.fraLead.TabIndex = 97
        Me.fraLead.TabStop = False
        Me.fraLead.Text = "Leading Edge"
        '
        'panLead
        '
        Me.panLead.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.panLead.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panLead.Controls.Add(Me.txtLead)
        Me.panLead.Controls.Add(Me.spnLead)
        Me.panLead.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panLead.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.panLead.Location = New System.Drawing.Point(5, 17)
        Me.panLead.Name = "panLead"
        Me.panLead.Size = New System.Drawing.Size(139, 21)
        Me.panLead.TabIndex = 98
        '
        'txtLead
        '
        Me.txtLead.BackColor = System.Drawing.SystemColors.Window
        Me.txtLead.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtLead.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtLead.Location = New System.Drawing.Point(3, 2)
        Me.txtLead.Name = "txtLead"
        Me.txtLead.Size = New System.Drawing.Size(114, 13)
        Me.txtLead.TabIndex = 99
        '
        'spnLead
        '
        Me.spnLead.Location = New System.Drawing.Point(121, -2)
        Me.spnLead.Name = "spnLead"
        Me.spnLead.Size = New System.Drawing.Size(16, 20)
        Me.spnLead.TabIndex = 100
        '
        'fraDuty
        '
        Me.fraDuty.Controls.Add(Me.panDuty)
        Me.fraDuty.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraDuty.Location = New System.Drawing.Point(304, 185)
        Me.fraDuty.Name = "fraDuty"
        Me.fraDuty.Size = New System.Drawing.Size(149, 43)
        Me.fraDuty.TabIndex = 93
        Me.fraDuty.TabStop = False
        Me.fraDuty.Text = "Duty Cycle"
        '
        'panDuty
        '
        Me.panDuty.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.panDuty.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panDuty.Controls.Add(Me.txtDuty)
        Me.panDuty.Controls.Add(Me.spnDuty)
        Me.panDuty.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panDuty.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.panDuty.Location = New System.Drawing.Point(5, 17)
        Me.panDuty.Name = "panDuty"
        Me.panDuty.Size = New System.Drawing.Size(139, 21)
        Me.panDuty.TabIndex = 94
        '
        'txtDuty
        '
        Me.txtDuty.BackColor = System.Drawing.SystemColors.Window
        Me.txtDuty.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtDuty.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDuty.Location = New System.Drawing.Point(3, 2)
        Me.txtDuty.Name = "txtDuty"
        Me.txtDuty.Size = New System.Drawing.Size(114, 13)
        Me.txtDuty.TabIndex = 95
        '
        'spnDuty
        '
        Me.spnDuty.Location = New System.Drawing.Point(121, -2)
        Me.spnDuty.Name = "spnDuty"
        Me.spnDuty.Size = New System.Drawing.Size(16, 20)
        Me.spnDuty.TabIndex = 96
        '
        'fraWidt
        '
        Me.fraWidt.Controls.Add(Me.panWidt)
        Me.fraWidt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraWidt.Location = New System.Drawing.Point(304, 49)
        Me.fraWidt.Name = "fraWidt"
        Me.fraWidt.Size = New System.Drawing.Size(149, 43)
        Me.fraWidt.TabIndex = 89
        Me.fraWidt.TabStop = False
        Me.fraWidt.Text = "Pulse Width"
        '
        'panWidt
        '
        Me.panWidt.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.panWidt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panWidt.Controls.Add(Me.txtWidt)
        Me.panWidt.Controls.Add(Me.spnWidt)
        Me.panWidt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panWidt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.panWidt.Location = New System.Drawing.Point(5, 17)
        Me.panWidt.Name = "panWidt"
        Me.panWidt.Size = New System.Drawing.Size(139, 21)
        Me.panWidt.TabIndex = 90
        '
        'txtWidt
        '
        Me.txtWidt.BackColor = System.Drawing.SystemColors.Window
        Me.txtWidt.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtWidt.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtWidt.Location = New System.Drawing.Point(3, 2)
        Me.txtWidt.Name = "txtWidt"
        Me.txtWidt.Size = New System.Drawing.Size(114, 13)
        Me.txtWidt.TabIndex = 91
        '
        'spnWidt
        '
        Me.spnWidt.Location = New System.Drawing.Point(121, -1)
        Me.spnWidt.Name = "spnWidt"
        Me.spnWidt.Size = New System.Drawing.Size(16, 20)
        Me.spnWidt.TabIndex = 92
        '
        'fraDela
        '
        Me.fraDela.Controls.Add(Me.panDela)
        Me.fraDela.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraDela.Location = New System.Drawing.Point(304, 4)
        Me.fraDela.Name = "fraDela"
        Me.fraDela.Size = New System.Drawing.Size(149, 43)
        Me.fraDela.TabIndex = 85
        Me.fraDela.TabStop = False
        Me.fraDela.Text = "Delay"
        '
        'panDela
        '
        Me.panDela.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.panDela.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panDela.Controls.Add(Me.txtDela)
        Me.panDela.Controls.Add(Me.spnDela)
        Me.panDela.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panDela.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.panDela.Location = New System.Drawing.Point(5, 17)
        Me.panDela.Name = "panDela"
        Me.panDela.Size = New System.Drawing.Size(139, 21)
        Me.panDela.TabIndex = 86
        '
        'txtDela
        '
        Me.txtDela.BackColor = System.Drawing.SystemColors.Window
        Me.txtDela.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtDela.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDela.Location = New System.Drawing.Point(3, 2)
        Me.txtDela.Name = "txtDela"
        Me.txtDela.Size = New System.Drawing.Size(114, 13)
        Me.txtDela.TabIndex = 87
        '
        'spnDela
        '
        Me.spnDela.Location = New System.Drawing.Point(121, -2)
        Me.spnDela.Name = "spnDela"
        Me.spnDela.Size = New System.Drawing.Size(16, 20)
        Me.spnDela.TabIndex = 88
        '
        'fraPhas
        '
        Me.fraPhas.Controls.Add(Me.panPhas)
        Me.fraPhas.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraPhas.Location = New System.Drawing.Point(67, 185)
        Me.fraPhas.Name = "fraPhas"
        Me.fraPhas.Size = New System.Drawing.Size(149, 43)
        Me.fraPhas.TabIndex = 81
        Me.fraPhas.TabStop = False
        Me.fraPhas.Text = "Phase Shift"
        '
        'panPhas
        '
        Me.panPhas.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.panPhas.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panPhas.Controls.Add(Me.txtPhas)
        Me.panPhas.Controls.Add(Me.spnPhas)
        Me.panPhas.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panPhas.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.panPhas.Location = New System.Drawing.Point(5, 17)
        Me.panPhas.Name = "panPhas"
        Me.panPhas.Size = New System.Drawing.Size(139, 21)
        Me.panPhas.TabIndex = 82
        '
        'txtPhas
        '
        Me.txtPhas.BackColor = System.Drawing.SystemColors.Window
        Me.txtPhas.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtPhas.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPhas.Location = New System.Drawing.Point(3, 2)
        Me.txtPhas.Name = "txtPhas"
        Me.txtPhas.Size = New System.Drawing.Size(114, 13)
        Me.txtPhas.TabIndex = 83
        '
        'spnPhas
        '
        Me.spnPhas.Location = New System.Drawing.Point(121, -2)
        Me.spnPhas.Name = "spnPhas"
        Me.spnPhas.Size = New System.Drawing.Size(16, 20)
        Me.spnPhas.TabIndex = 84
        '
        'fraExpo
        '
        Me.fraExpo.Controls.Add(Me.panExpo)
        Me.fraExpo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraExpo.Location = New System.Drawing.Point(67, 140)
        Me.fraExpo.Name = "fraExpo"
        Me.fraExpo.Size = New System.Drawing.Size(149, 43)
        Me.fraExpo.TabIndex = 77
        Me.fraExpo.TabStop = False
        Me.fraExpo.Text = "Function Exponent"
        '
        'panExpo
        '
        Me.panExpo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.panExpo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panExpo.Controls.Add(Me.txtExpo)
        Me.panExpo.Controls.Add(Me.spnExpo)
        Me.panExpo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panExpo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.panExpo.Location = New System.Drawing.Point(5, 17)
        Me.panExpo.Name = "panExpo"
        Me.panExpo.Size = New System.Drawing.Size(139, 21)
        Me.panExpo.TabIndex = 78
        '
        'txtExpo
        '
        Me.txtExpo.BackColor = System.Drawing.SystemColors.Window
        Me.txtExpo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtExpo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtExpo.Location = New System.Drawing.Point(2, 2)
        Me.txtExpo.Name = "txtExpo"
        Me.txtExpo.Size = New System.Drawing.Size(114, 13)
        Me.txtExpo.TabIndex = 79
        '
        'spnExpo
        '
        Me.spnExpo.Location = New System.Drawing.Point(121, -2)
        Me.spnExpo.Name = "spnExpo"
        Me.spnExpo.Size = New System.Drawing.Size(16, 20)
        Me.spnExpo.TabIndex = 80
        '
        'fraDcOffs
        '
        Me.fraDcOffs.Controls.Add(Me.panDcOffs)
        Me.fraDcOffs.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraDcOffs.Location = New System.Drawing.Point(67, 94)
        Me.fraDcOffs.Name = "fraDcOffs"
        Me.fraDcOffs.Size = New System.Drawing.Size(149, 43)
        Me.fraDcOffs.TabIndex = 73
        Me.fraDcOffs.TabStop = False
        Me.fraDcOffs.Text = "DC Offset"
        '
        'panDcOffs
        '
        Me.panDcOffs.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.panDcOffs.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panDcOffs.Controls.Add(Me.txtDcOffs)
        Me.panDcOffs.Controls.Add(Me.spnDcOffs)
        Me.panDcOffs.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panDcOffs.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.panDcOffs.Location = New System.Drawing.Point(5, 17)
        Me.panDcOffs.Name = "panDcOffs"
        Me.panDcOffs.Size = New System.Drawing.Size(139, 21)
        Me.panDcOffs.TabIndex = 74
        '
        'txtDcOffs
        '
        Me.txtDcOffs.BackColor = System.Drawing.SystemColors.Window
        Me.txtDcOffs.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtDcOffs.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDcOffs.Location = New System.Drawing.Point(2, 2)
        Me.txtDcOffs.Name = "txtDcOffs"
        Me.txtDcOffs.Size = New System.Drawing.Size(114, 13)
        Me.txtDcOffs.TabIndex = 75
        '
        'spnDcOffs
        '
        Me.spnDcOffs.Location = New System.Drawing.Point(121, -2)
        Me.spnDcOffs.Name = "spnDcOffs"
        Me.spnDcOffs.Size = New System.Drawing.Size(16, 20)
        Me.spnDcOffs.TabIndex = 76
        '
        'fraFreq
        '
        Me.fraFreq.Controls.Add(Me.panFreq)
        Me.fraFreq.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraFreq.Location = New System.Drawing.Point(67, 4)
        Me.fraFreq.Name = "fraFreq"
        Me.fraFreq.Size = New System.Drawing.Size(149, 43)
        Me.fraFreq.TabIndex = 69
        Me.fraFreq.TabStop = False
        Me.fraFreq.Text = "Frequency"
        '
        'panFreq
        '
        Me.panFreq.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.panFreq.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panFreq.Controls.Add(Me.txtFreq)
        Me.panFreq.Controls.Add(Me.spnFreq)
        Me.panFreq.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panFreq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.panFreq.Location = New System.Drawing.Point(4, 17)
        Me.panFreq.Name = "panFreq"
        Me.panFreq.Size = New System.Drawing.Size(139, 21)
        Me.panFreq.TabIndex = 70
        '
        'txtFreq
        '
        Me.txtFreq.BackColor = System.Drawing.SystemColors.Window
        Me.txtFreq.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtFreq.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFreq.Location = New System.Drawing.Point(3, 2)
        Me.txtFreq.Name = "txtFreq"
        Me.txtFreq.Size = New System.Drawing.Size(114, 13)
        Me.txtFreq.TabIndex = 71
        '
        'spnFreq
        '
        Me.spnFreq.Location = New System.Drawing.Point(121, -2)
        Me.spnFreq.Name = "spnFreq"
        Me.spnFreq.Size = New System.Drawing.Size(16, 20)
        Me.spnFreq.TabIndex = 72
        '
        'fraAmpl
        '
        Me.fraAmpl.Controls.Add(Me.panAmpl)
        Me.fraAmpl.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraAmpl.Location = New System.Drawing.Point(67, 49)
        Me.fraAmpl.Name = "fraAmpl"
        Me.fraAmpl.Size = New System.Drawing.Size(149, 43)
        Me.fraAmpl.TabIndex = 65
        Me.fraAmpl.TabStop = False
        Me.fraAmpl.Text = "Amplitude"
        '
        'panAmpl
        '
        Me.panAmpl.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.panAmpl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panAmpl.Controls.Add(Me.txtAmpl)
        Me.panAmpl.Controls.Add(Me.spnAmpl)
        Me.panAmpl.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panAmpl.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.panAmpl.Location = New System.Drawing.Point(5, 17)
        Me.panAmpl.Name = "panAmpl"
        Me.panAmpl.Size = New System.Drawing.Size(139, 21)
        Me.panAmpl.TabIndex = 66
        '
        'txtAmpl
        '
        Me.txtAmpl.BackColor = System.Drawing.SystemColors.Window
        Me.txtAmpl.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtAmpl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAmpl.Location = New System.Drawing.Point(3, 2)
        Me.txtAmpl.Name = "txtAmpl"
        Me.txtAmpl.Size = New System.Drawing.Size(114, 13)
        Me.txtAmpl.TabIndex = 67
        '
        'spnAmpl
        '
        Me.spnAmpl.Location = New System.Drawing.Point(121, -1)
        Me.spnAmpl.Name = "spnAmpl"
        Me.spnAmpl.Size = New System.Drawing.Size(16, 20)
        Me.spnAmpl.TabIndex = 68
        '
        'fraFunc
        '
        Me.fraFunc.Controls.Add(Me.tolFunc)
        Me.fraFunc.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraFunc.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.fraFunc.Location = New System.Drawing.Point(8, -3)
        Me.fraFunc.Name = "fraFunc"
        Me.fraFunc.Size = New System.Drawing.Size(43, 284)
        Me.fraFunc.TabIndex = 5
        Me.fraFunc.TabStop = False
        '
        'tolFunc
        '
        Me.tolFunc.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.Sine, Me.Square, Me.Pulse, Me.Ramp, Me.Triangle, Me.Exponential, Me.Gaussian, Me.Sinc, Me.btnDC})
        Me.tolFunc.ButtonSize = New System.Drawing.Size(32, 24)
        Me.tolFunc.Dock = System.Windows.Forms.DockStyle.None
        Me.tolFunc.DropDownArrows = True
        Me.tolFunc.ImageList = Me.imlFunc
        Me.tolFunc.Location = New System.Drawing.Point(0, 8)
        Me.tolFunc.Name = "tolFunc"
        Me.tolFunc.ShowToolTips = True
        Me.tolFunc.Size = New System.Drawing.Size(40, 276)
        Me.tolFunc.TabIndex = 6
        '
        'Sine
        '
        Me.Sine.ImageIndex = 0
        Me.Sine.Name = "Sine"
        Me.Sine.Pushed = True
        Me.Sine.ToolTipText = "Sine Wave"
        '
        'Square
        '
        Me.Square.ImageIndex = 1
        Me.Square.Name = "Square"
        Me.Square.ToolTipText = "Square Wave"
        '
        'Pulse
        '
        Me.Pulse.ImageIndex = 2
        Me.Pulse.Name = "Pulse"
        Me.Pulse.ToolTipText = "Pulse"
        '
        'Ramp
        '
        Me.Ramp.ImageIndex = 3
        Me.Ramp.Name = "Ramp"
        Me.Ramp.ToolTipText = "Ramp Wave"
        '
        'Triangle
        '
        Me.Triangle.ImageIndex = 4
        Me.Triangle.Name = "Triangle"
        Me.Triangle.ToolTipText = "Triangle Wave"
        '
        'Exponential
        '
        Me.Exponential.ImageIndex = 5
        Me.Exponential.Name = "Exponential"
        Me.Exponential.ToolTipText = "Exponential Decay Pulse"
        '
        'Gaussian
        '
        Me.Gaussian.ImageIndex = 6
        Me.Gaussian.Name = "Gaussian"
        Me.Gaussian.ToolTipText = "Gaussian Pulse"
        '
        'Sinc
        '
        Me.Sinc.ImageIndex = 7
        Me.Sinc.Name = "Sinc"
        Me.Sinc.ToolTipText = "Sinc Wave [Sine(x)/x]"
        '
        'btnDC
        '
        Me.btnDC.ImageIndex = 8
        Me.btnDC.Name = "btnDC"
        Me.btnDC.ToolTipText = "DC Level"
        '
        'imlFunc
        '
        Me.imlFunc.ImageStream = CType(resources.GetObject("imlFunc.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imlFunc.TransparentColor = System.Drawing.Color.Transparent
        Me.imlFunc.Images.SetKeyName(0, "")
        Me.imlFunc.Images.SetKeyName(1, "")
        Me.imlFunc.Images.SetKeyName(2, "")
        Me.imlFunc.Images.SetKeyName(3, "")
        Me.imlFunc.Images.SetKeyName(4, "")
        Me.imlFunc.Images.SetKeyName(5, "")
        Me.imlFunc.Images.SetKeyName(6, "")
        Me.imlFunc.Images.SetKeyName(7, "")
        Me.imlFunc.Images.SetKeyName(8, "")
        '
        'tabOptions_Page2
        '
        Me.tabOptions_Page2.Controls.Add(Me.fraModeTrigLevel)
        Me.tabOptions_Page2.Controls.Add(Me.fraModeSelect)
        Me.tabOptions_Page2.Controls.Add(Me.fraModeTrigDela)
        Me.tabOptions_Page2.Controls.Add(Me.fraModeTrigBurst)
        Me.tabOptions_Page2.Controls.Add(Me.fraModeTrigTime)
        Me.tabOptions_Page2.Controls.Add(Me.fraModeTrigSlop)
        Me.tabOptions_Page2.Controls.Add(Me.fraModeTrigSour)
        Me.tabOptions_Page2.Controls.Add(Me.cmdModeSoftTrig)
        Me.tabOptions_Page2.Location = New System.Drawing.Point(4, 22)
        Me.tabOptions_Page2.Name = "tabOptions_Page2"
        Me.tabOptions_Page2.Size = New System.Drawing.Size(461, 292)
        Me.tabOptions_Page2.TabIndex = 1
        Me.tabOptions_Page2.Text = "Triggers"
        '
        'fraModeTrigLevel
        '
        Me.fraModeTrigLevel.Controls.Add(Me.panModeTrigLevel)
        Me.fraModeTrigLevel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraModeTrigLevel.Location = New System.Drawing.Point(8, 166)
        Me.fraModeTrigLevel.Name = "fraModeTrigLevel"
        Me.fraModeTrigLevel.Size = New System.Drawing.Size(149, 43)
        Me.fraModeTrigLevel.TabIndex = 54
        Me.fraModeTrigLevel.TabStop = False
        Me.fraModeTrigLevel.Text = "Level"
        '
        'panModeTrigLevel
        '
        Me.panModeTrigLevel.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.panModeTrigLevel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panModeTrigLevel.Controls.Add(Me.txtModeTrigLevel)
        Me.panModeTrigLevel.Controls.Add(Me.spnModeTrigLevel)
        Me.panModeTrigLevel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panModeTrigLevel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.panModeTrigLevel.Location = New System.Drawing.Point(5, 17)
        Me.panModeTrigLevel.Name = "panModeTrigLevel"
        Me.panModeTrigLevel.Size = New System.Drawing.Size(139, 21)
        Me.panModeTrigLevel.TabIndex = 55
        '
        'txtModeTrigLevel
        '
        Me.txtModeTrigLevel.BackColor = System.Drawing.SystemColors.Window
        Me.txtModeTrigLevel.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtModeTrigLevel.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtModeTrigLevel.Location = New System.Drawing.Point(3, 2)
        Me.txtModeTrigLevel.Name = "txtModeTrigLevel"
        Me.txtModeTrigLevel.Size = New System.Drawing.Size(113, 13)
        Me.txtModeTrigLevel.TabIndex = 57
        '
        'spnModeTrigLevel
        '
        Me.spnModeTrigLevel.Location = New System.Drawing.Point(121, -2)
        Me.spnModeTrigLevel.Name = "spnModeTrigLevel"
        Me.spnModeTrigLevel.Size = New System.Drawing.Size(16, 20)
        Me.spnModeTrigLevel.TabIndex = 56
        '
        'fraModeSelect
        '
        Me.fraModeSelect.Controls.Add(Me.optModeGated)
        Me.fraModeSelect.Controls.Add(Me.optModeBurst)
        Me.fraModeSelect.Controls.Add(Me.optModeTrig)
        Me.fraModeSelect.Controls.Add(Me.optModeCont)
        Me.fraModeSelect.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraModeSelect.Location = New System.Drawing.Point(8, 4)
        Me.fraModeSelect.Name = "fraModeSelect"
        Me.fraModeSelect.Size = New System.Drawing.Size(437, 57)
        Me.fraModeSelect.TabIndex = 25
        Me.fraModeSelect.TabStop = False
        Me.fraModeSelect.Text = "Mode"
        '
        'optModeGated
        '
        Me.optModeGated.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optModeGated.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optModeGated.Location = New System.Drawing.Point(342, 24)
        Me.optModeGated.Name = "optModeGated"
        Me.optModeGated.Size = New System.Drawing.Size(75, 21)
        Me.optModeGated.TabIndex = 37
        Me.optModeGated.Text = "Gated"
        '
        'optModeBurst
        '
        Me.optModeBurst.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optModeBurst.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optModeBurst.Location = New System.Drawing.Point(210, 24)
        Me.optModeBurst.Name = "optModeBurst"
        Me.optModeBurst.Size = New System.Drawing.Size(131, 21)
        Me.optModeBurst.TabIndex = 36
        Me.optModeBurst.Text = "Triggered Burst"
        '
        'optModeTrig
        '
        Me.optModeTrig.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optModeTrig.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optModeTrig.Location = New System.Drawing.Point(113, 24)
        Me.optModeTrig.Name = "optModeTrig"
        Me.optModeTrig.Size = New System.Drawing.Size(96, 21)
        Me.optModeTrig.TabIndex = 35
        Me.optModeTrig.Text = "Triggered"
        '
        'optModeCont
        '
        Me.optModeCont.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optModeCont.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optModeCont.Location = New System.Drawing.Point(10, 24)
        Me.optModeCont.Name = "optModeCont"
        Me.optModeCont.Size = New System.Drawing.Size(102, 21)
        Me.optModeCont.TabIndex = 34
        Me.optModeCont.Text = "Continuous"
        '
        'fraModeTrigDela
        '
        Me.fraModeTrigDela.Controls.Add(Me.panModeTrigDela)
        Me.fraModeTrigDela.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraModeTrigDela.Location = New System.Drawing.Point(223, 166)
        Me.fraModeTrigDela.Name = "fraModeTrigDela"
        Me.fraModeTrigDela.Size = New System.Drawing.Size(149, 43)
        Me.fraModeTrigDela.TabIndex = 22
        Me.fraModeTrigDela.TabStop = False
        Me.fraModeTrigDela.Text = "Delay"
        '
        'panModeTrigDela
        '
        Me.panModeTrigDela.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.panModeTrigDela.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panModeTrigDela.Controls.Add(Me.txtModeTrigDela)
        Me.panModeTrigDela.Controls.Add(Me.spnModeTrigDela)
        Me.panModeTrigDela.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panModeTrigDela.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.panModeTrigDela.Location = New System.Drawing.Point(5, 17)
        Me.panModeTrigDela.Name = "panModeTrigDela"
        Me.panModeTrigDela.Size = New System.Drawing.Size(139, 21)
        Me.panModeTrigDela.TabIndex = 23
        '
        'txtModeTrigDela
        '
        Me.txtModeTrigDela.BackColor = System.Drawing.SystemColors.Window
        Me.txtModeTrigDela.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtModeTrigDela.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtModeTrigDela.Location = New System.Drawing.Point(3, 2)
        Me.txtModeTrigDela.Name = "txtModeTrigDela"
        Me.txtModeTrigDela.Size = New System.Drawing.Size(113, 13)
        Me.txtModeTrigDela.TabIndex = 60
        '
        'spnModeTrigDela
        '
        Me.spnModeTrigDela.Location = New System.Drawing.Point(121, -2)
        Me.spnModeTrigDela.Name = "spnModeTrigDela"
        Me.spnModeTrigDela.Size = New System.Drawing.Size(16, 20)
        Me.spnModeTrigDela.TabIndex = 24
        '
        'fraModeTrigBurst
        '
        Me.fraModeTrigBurst.Controls.Add(Me.panModeTrigBurst)
        Me.fraModeTrigBurst.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraModeTrigBurst.Location = New System.Drawing.Point(223, 69)
        Me.fraModeTrigBurst.Name = "fraModeTrigBurst"
        Me.fraModeTrigBurst.Size = New System.Drawing.Size(149, 43)
        Me.fraModeTrigBurst.TabIndex = 19
        Me.fraModeTrigBurst.TabStop = False
        Me.fraModeTrigBurst.Text = "Burst Count"
        '
        'panModeTrigBurst
        '
        Me.panModeTrigBurst.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.panModeTrigBurst.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panModeTrigBurst.Controls.Add(Me.txtModeTrigBurst)
        Me.panModeTrigBurst.Controls.Add(Me.spnModeTrigBurst)
        Me.panModeTrigBurst.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panModeTrigBurst.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.panModeTrigBurst.Location = New System.Drawing.Point(5, 17)
        Me.panModeTrigBurst.Name = "panModeTrigBurst"
        Me.panModeTrigBurst.Size = New System.Drawing.Size(139, 21)
        Me.panModeTrigBurst.TabIndex = 20
        '
        'txtModeTrigBurst
        '
        Me.txtModeTrigBurst.BackColor = System.Drawing.SystemColors.Window
        Me.txtModeTrigBurst.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtModeTrigBurst.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtModeTrigBurst.Location = New System.Drawing.Point(3, 2)
        Me.txtModeTrigBurst.Name = "txtModeTrigBurst"
        Me.txtModeTrigBurst.Size = New System.Drawing.Size(113, 13)
        Me.txtModeTrigBurst.TabIndex = 58
        '
        'spnModeTrigBurst
        '
        Me.spnModeTrigBurst.Location = New System.Drawing.Point(121, -2)
        Me.spnModeTrigBurst.Name = "spnModeTrigBurst"
        Me.spnModeTrigBurst.Size = New System.Drawing.Size(16, 20)
        Me.spnModeTrigBurst.TabIndex = 21
        '
        'fraModeTrigTime
        '
        Me.fraModeTrigTime.Controls.Add(Me.panModeTrigTime)
        Me.fraModeTrigTime.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraModeTrigTime.Location = New System.Drawing.Point(223, 117)
        Me.fraModeTrigTime.Name = "fraModeTrigTime"
        Me.fraModeTrigTime.Size = New System.Drawing.Size(149, 43)
        Me.fraModeTrigTime.TabIndex = 16
        Me.fraModeTrigTime.TabStop = False
        Me.fraModeTrigTime.Text = "Internal Trigger Interval"
        '
        'panModeTrigTime
        '
        Me.panModeTrigTime.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.panModeTrigTime.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panModeTrigTime.Controls.Add(Me.txtModeTrigTime)
        Me.panModeTrigTime.Controls.Add(Me.spnModeTrigTime)
        Me.panModeTrigTime.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panModeTrigTime.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.panModeTrigTime.Location = New System.Drawing.Point(5, 17)
        Me.panModeTrigTime.Name = "panModeTrigTime"
        Me.panModeTrigTime.Size = New System.Drawing.Size(139, 21)
        Me.panModeTrigTime.TabIndex = 17
        '
        'txtModeTrigTime
        '
        Me.txtModeTrigTime.BackColor = System.Drawing.SystemColors.Window
        Me.txtModeTrigTime.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtModeTrigTime.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtModeTrigTime.Location = New System.Drawing.Point(3, 2)
        Me.txtModeTrigTime.Name = "txtModeTrigTime"
        Me.txtModeTrigTime.Size = New System.Drawing.Size(113, 13)
        Me.txtModeTrigTime.TabIndex = 59
        '
        'spnModeTrigTime
        '
        Me.spnModeTrigTime.Location = New System.Drawing.Point(121, -2)
        Me.spnModeTrigTime.Name = "spnModeTrigTime"
        Me.spnModeTrigTime.Size = New System.Drawing.Size(16, 20)
        Me.spnModeTrigTime.TabIndex = 18
        '
        'fraModeTrigSlop
        '
        Me.fraModeTrigSlop.Controls.Add(Me.cboModeTrigSlop)
        Me.fraModeTrigSlop.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraModeTrigSlop.Location = New System.Drawing.Point(8, 117)
        Me.fraModeTrigSlop.Name = "fraModeTrigSlop"
        Me.fraModeTrigSlop.Size = New System.Drawing.Size(149, 43)
        Me.fraModeTrigSlop.TabIndex = 14
        Me.fraModeTrigSlop.TabStop = False
        Me.fraModeTrigSlop.Text = "Slope"
        '
        'cboModeTrigSlop
        '
        Me.cboModeTrigSlop.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cboModeTrigSlop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboModeTrigSlop.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboModeTrigSlop.Location = New System.Drawing.Point(5, 17)
        Me.cboModeTrigSlop.Name = "cboModeTrigSlop"
        Me.cboModeTrigSlop.Size = New System.Drawing.Size(139, 21)
        Me.cboModeTrigSlop.TabIndex = 15
        '
        'fraModeTrigSour
        '
        Me.fraModeTrigSour.Controls.Add(Me.cboModeTrigSour)
        Me.fraModeTrigSour.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraModeTrigSour.Location = New System.Drawing.Point(8, 69)
        Me.fraModeTrigSour.Name = "fraModeTrigSour"
        Me.fraModeTrigSour.Size = New System.Drawing.Size(149, 43)
        Me.fraModeTrigSour.TabIndex = 12
        Me.fraModeTrigSour.TabStop = False
        Me.fraModeTrigSour.Text = "Source"
        '
        'cboModeTrigSour
        '
        Me.cboModeTrigSour.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cboModeTrigSour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboModeTrigSour.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboModeTrigSour.Location = New System.Drawing.Point(5, 17)
        Me.cboModeTrigSour.Name = "cboModeTrigSour"
        Me.cboModeTrigSour.Size = New System.Drawing.Size(139, 21)
        Me.cboModeTrigSour.TabIndex = 13
        '
        'cmdModeSoftTrig
        '
        Me.cmdModeSoftTrig.BackColor = System.Drawing.SystemColors.Control
        Me.cmdModeSoftTrig.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdModeSoftTrig.Location = New System.Drawing.Point(8, 214)
        Me.cmdModeSoftTrig.Name = "cmdModeSoftTrig"
        Me.cmdModeSoftTrig.Size = New System.Drawing.Size(101, 24)
        Me.cmdModeSoftTrig.TabIndex = 52
        Me.cmdModeSoftTrig.Text = "&Software Trig"
        Me.cmdModeSoftTrig.UseVisualStyleBackColor = False
        '
        'tabOptions_Page3
        '
        Me.tabOptions_Page3.Controls.Add(Me.fraTrigOutVXI)
        Me.tabOptions_Page3.Controls.Add(Me.fraTrigOutSyncSour)
        Me.tabOptions_Page3.Controls.Add(Me.fraTrigOutSyncOut)
        Me.tabOptions_Page3.Controls.Add(Me.fraVXIECLTrig)
        Me.tabOptions_Page3.Controls.Add(Me.fraVXITTLTrig)
        Me.tabOptions_Page3.Location = New System.Drawing.Point(4, 22)
        Me.tabOptions_Page3.Name = "tabOptions_Page3"
        Me.tabOptions_Page3.Size = New System.Drawing.Size(461, 292)
        Me.tabOptions_Page3.TabIndex = 2
        Me.tabOptions_Page3.Text = "Markers"
        '
        'fraTrigOutVXI
        '
        Me.fraTrigOutVXI.Controls.Add(Me.panIntTimer)
        Me.fraTrigOutVXI.Controls.Add(Me.optTrigSourceWave)
        Me.fraTrigOutVXI.Controls.Add(Me.optTrigSourceExte)
        Me.fraTrigOutVXI.Controls.Add(Me.optTrigSourceTimer)
        Me.fraTrigOutVXI.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraTrigOutVXI.Location = New System.Drawing.Point(8, 74)
        Me.fraTrigOutVXI.Name = "fraTrigOutVXI"
        Me.fraTrigOutVXI.Size = New System.Drawing.Size(178, 119)
        Me.fraTrigOutVXI.TabIndex = 8
        Me.fraTrigOutVXI.TabStop = False
        Me.fraTrigOutVXI.Text = "TTL Marker Source"
        '
        'panIntTimer
        '
        Me.panIntTimer.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.panIntTimer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panIntTimer.Controls.Add(Me.txtIntTimer)
        Me.panIntTimer.Controls.Add(Me.spnIntTimer)
        Me.panIntTimer.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panIntTimer.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.panIntTimer.Location = New System.Drawing.Point(8, 85)
        Me.panIntTimer.Name = "panIntTimer"
        Me.panIntTimer.Size = New System.Drawing.Size(122, 21)
        Me.panIntTimer.TabIndex = 62
        '
        'txtIntTimer
        '
        Me.txtIntTimer.BackColor = System.Drawing.SystemColors.Window
        Me.txtIntTimer.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtIntTimer.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtIntTimer.Location = New System.Drawing.Point(3, 2)
        Me.txtIntTimer.Name = "txtIntTimer"
        Me.txtIntTimer.Size = New System.Drawing.Size(98, 13)
        Me.txtIntTimer.TabIndex = 63
        '
        'spnIntTimer
        '
        Me.spnIntTimer.Location = New System.Drawing.Point(104, -2)
        Me.spnIntTimer.Name = "spnIntTimer"
        Me.spnIntTimer.Size = New System.Drawing.Size(16, 20)
        Me.spnIntTimer.TabIndex = 64
        '
        'optTrigSourceWave
        '
        Me.optTrigSourceWave.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optTrigSourceWave.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optTrigSourceWave.Location = New System.Drawing.Point(8, 16)
        Me.optTrigSourceWave.Name = "optTrigSourceWave"
        Me.optTrigSourceWave.Size = New System.Drawing.Size(155, 20)
        Me.optTrigSourceWave.TabIndex = 61
        Me.optTrigSourceWave.Text = "Waveform"
        '
        'optTrigSourceExte
        '
        Me.optTrigSourceExte.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optTrigSourceExte.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optTrigSourceExte.Location = New System.Drawing.Point(8, 36)
        Me.optTrigSourceExte.Name = "optTrigSourceExte"
        Me.optTrigSourceExte.Size = New System.Drawing.Size(155, 20)
        Me.optTrigSourceExte.TabIndex = 39
        Me.optTrigSourceExte.Text = "External Trigger In"
        '
        'optTrigSourceTimer
        '
        Me.optTrigSourceTimer.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optTrigSourceTimer.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optTrigSourceTimer.Location = New System.Drawing.Point(8, 56)
        Me.optTrigSourceTimer.Name = "optTrigSourceTimer"
        Me.optTrigSourceTimer.Size = New System.Drawing.Size(155, 20)
        Me.optTrigSourceTimer.TabIndex = 38
        Me.optTrigSourceTimer.Text = "Internal Trigger"
        '
        'fraTrigOutSyncSour
        '
        Me.fraTrigOutSyncSour.Controls.Add(Me.optSyncSourceHalfSamp)
        Me.fraTrigOutSyncSour.Controls.Add(Me.optSyncSourceWave)
        Me.fraTrigOutSyncSour.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraTrigOutSyncSour.Location = New System.Drawing.Point(8, 4)
        Me.fraTrigOutSyncSour.Name = "fraTrigOutSyncSour"
        Me.fraTrigOutSyncSour.Size = New System.Drawing.Size(178, 67)
        Me.fraTrigOutSyncSour.TabIndex = 7
        Me.fraTrigOutSyncSour.TabStop = False
        Me.fraTrigOutSyncSour.Text = "Sync Output Source"
        '
        'optSyncSourceHalfSamp
        '
        Me.optSyncSourceHalfSamp.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optSyncSourceHalfSamp.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optSyncSourceHalfSamp.Location = New System.Drawing.Point(8, 40)
        Me.optSyncSourceHalfSamp.Name = "optSyncSourceHalfSamp"
        Me.optSyncSourceHalfSamp.Size = New System.Drawing.Size(155, 20)
        Me.optSyncSourceHalfSamp.TabIndex = 41
        Me.optSyncSourceHalfSamp.Text = "1/2 Sample Clock"
        '
        'optSyncSourceWave
        '
        Me.optSyncSourceWave.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optSyncSourceWave.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optSyncSourceWave.Location = New System.Drawing.Point(8, 19)
        Me.optSyncSourceWave.Name = "optSyncSourceWave"
        Me.optSyncSourceWave.Size = New System.Drawing.Size(155, 20)
        Me.optSyncSourceWave.TabIndex = 40
        Me.optSyncSourceWave.Text = "Waveform"
        '
        'fraTrigOutSyncOut
        '
        Me.fraTrigOutSyncOut.Controls.Add(Me.chkSyncOutStateOn)
        Me.fraTrigOutSyncOut.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraTrigOutSyncOut.Location = New System.Drawing.Point(194, 4)
        Me.fraTrigOutSyncOut.Name = "fraTrigOutSyncOut"
        Me.fraTrigOutSyncOut.Size = New System.Drawing.Size(178, 67)
        Me.fraTrigOutSyncOut.TabIndex = 32
        Me.fraTrigOutSyncOut.TabStop = False
        Me.fraTrigOutSyncOut.Text = "Sync Output"
        '
        'chkSyncOutStateOn
        '
        Me.chkSyncOutStateOn.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkSyncOutStateOn.Location = New System.Drawing.Point(119, 27)
        Me.chkSyncOutStateOn.Name = "chkSyncOutStateOn"
        Me.chkSyncOutStateOn.Size = New System.Drawing.Size(51, 20)
        Me.chkSyncOutStateOn.TabIndex = 33
        Me.chkSyncOutStateOn.Text = "On"
        '
        'fraVXIECLTrig
        '
        Me.fraVXIECLTrig.Controls.Add(Me.cboTrigOutECL)
        Me.fraVXIECLTrig.Controls.Add(Me.chkTrigOutECLOn)
        Me.fraVXIECLTrig.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraVXIECLTrig.Location = New System.Drawing.Point(8, 199)
        Me.fraVXIECLTrig.Name = "fraVXIECLTrig"
        Me.fraVXIECLTrig.Size = New System.Drawing.Size(178, 53)
        Me.fraVXIECLTrig.TabIndex = 26
        Me.fraVXIECLTrig.TabStop = False
        Me.fraVXIECLTrig.Text = "ECL Marker Output"
        '
        'cboTrigOutECL
        '
        Me.cboTrigOutECL.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cboTrigOutECL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTrigOutECL.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboTrigOutECL.Location = New System.Drawing.Point(8, 21)
        Me.cboTrigOutECL.Name = "cboTrigOutECL"
        Me.cboTrigOutECL.Size = New System.Drawing.Size(90, 21)
        Me.cboTrigOutECL.TabIndex = 27
        '
        'chkTrigOutECLOn
        '
        Me.chkTrigOutECLOn.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTrigOutECLOn.Location = New System.Drawing.Point(121, 20)
        Me.chkTrigOutECLOn.Name = "chkTrigOutECLOn"
        Me.chkTrigOutECLOn.Size = New System.Drawing.Size(51, 20)
        Me.chkTrigOutECLOn.TabIndex = 28
        Me.chkTrigOutECLOn.Text = "On"
        '
        'fraVXITTLTrig
        '
        Me.fraVXITTLTrig.Controls.Add(Me.cboTrigOutTTL)
        Me.fraVXITTLTrig.Controls.Add(Me.chkTrigOutTTLOn)
        Me.fraVXITTLTrig.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraVXITTLTrig.Location = New System.Drawing.Point(194, 74)
        Me.fraVXITTLTrig.Name = "fraVXITTLTrig"
        Me.fraVXITTLTrig.Size = New System.Drawing.Size(178, 119)
        Me.fraVXITTLTrig.TabIndex = 29
        Me.fraVXITTLTrig.TabStop = False
        Me.fraVXITTLTrig.Text = "TTL Marker Output"
        '
        'cboTrigOutTTL
        '
        Me.cboTrigOutTTL.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cboTrigOutTTL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTrigOutTTL.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboTrigOutTTL.Location = New System.Drawing.Point(8, 49)
        Me.cboTrigOutTTL.Name = "cboTrigOutTTL"
        Me.cboTrigOutTTL.Size = New System.Drawing.Size(90, 21)
        Me.cboTrigOutTTL.TabIndex = 30
        '
        'chkTrigOutTTLOn
        '
        Me.chkTrigOutTTLOn.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTrigOutTTLOn.Location = New System.Drawing.Point(119, 51)
        Me.chkTrigOutTTLOn.Name = "chkTrigOutTTLOn"
        Me.chkTrigOutTTLOn.Size = New System.Drawing.Size(51, 20)
        Me.chkTrigOutTTLOn.TabIndex = 31
        Me.chkTrigOutTTLOn.Text = "On"
        '
        'tabOptions_Page4
        '
        Me.tabOptions_Page4.Controls.Add(Me.Panel_Conifg)
        Me.tabOptions_Page4.Controls.Add(Me.cmdUpdateTIP)
        Me.tabOptions_Page4.Controls.Add(Me.cmdRunWaveCAD)
        Me.tabOptions_Page4.Controls.Add(Me.cmdAbout)
        Me.tabOptions_Page4.Controls.Add(Me.cmdTest)
        Me.tabOptions_Page4.Controls.Add(Me.fraOutLoadImpe)
        Me.tabOptions_Page4.Controls.Add(Me.fraOutLowPassFilt)
        Me.tabOptions_Page4.Location = New System.Drawing.Point(4, 22)
        Me.tabOptions_Page4.Name = "tabOptions_Page4"
        Me.tabOptions_Page4.Size = New System.Drawing.Size(461, 292)
        Me.tabOptions_Page4.TabIndex = 3
        Me.tabOptions_Page4.Text = "Options"
        '
        'Panel_Conifg
        '
        Me.Panel_Conifg.BackColor = System.Drawing.SystemColors.Control
        Me.Panel_Conifg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Panel_Conifg.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Panel_Conifg.Location = New System.Drawing.Point(8, 91)
        Me.Panel_Conifg.Name = "Panel_Conifg"
        Me.Panel_Conifg.Parent_Object = Nothing
        Me.Panel_Conifg.Refresh = CType(0, Short)
        Me.Panel_Conifg.Size = New System.Drawing.Size(192, 139)
        Me.Panel_Conifg.TabIndex = 111
        '
        'cmdUpdateTIP
        '
        Me.cmdUpdateTIP.BackColor = System.Drawing.SystemColors.Control
        Me.cmdUpdateTIP.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdUpdateTIP.Location = New System.Drawing.Point(212, 249)
        Me.cmdUpdateTIP.Name = "cmdUpdateTIP"
        Me.cmdUpdateTIP.Size = New System.Drawing.Size(97, 25)
        Me.cmdUpdateTIP.TabIndex = 109
        Me.cmdUpdateTIP.Text = "&Update TIP"
        Me.cmdUpdateTIP.UseVisualStyleBackColor = False
        Me.cmdUpdateTIP.Visible = False
        '
        'cmdRunWaveCAD
        '
        Me.cmdRunWaveCAD.BackColor = System.Drawing.SystemColors.Control
        Me.cmdRunWaveCAD.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdRunWaveCAD.Location = New System.Drawing.Point(206, 170)
        Me.cmdRunWaveCAD.Name = "cmdRunWaveCAD"
        Me.cmdRunWaveCAD.Size = New System.Drawing.Size(93, 24)
        Me.cmdRunWaveCAD.TabIndex = 51
        Me.cmdRunWaveCAD.Text = "&WaveCAD"
        Me.cmdRunWaveCAD.UseVisualStyleBackColor = False
        '
        'cmdAbout
        '
        Me.cmdAbout.BackColor = System.Drawing.SystemColors.Control
        Me.cmdAbout.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAbout.Location = New System.Drawing.Point(6, 249)
        Me.cmdAbout.Name = "cmdAbout"
        Me.cmdAbout.Size = New System.Drawing.Size(97, 25)
        Me.cmdAbout.TabIndex = 50
        Me.cmdAbout.Text = "&About"
        Me.cmdAbout.UseVisualStyleBackColor = False
        '
        'cmdTest
        '
        Me.cmdTest.BackColor = System.Drawing.SystemColors.Control
        Me.cmdTest.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdTest.Location = New System.Drawing.Point(106, 249)
        Me.cmdTest.Name = "cmdTest"
        Me.cmdTest.Size = New System.Drawing.Size(97, 25)
        Me.cmdTest.TabIndex = 49
        Me.cmdTest.Text = "Built-In &Test"
        Me.cmdTest.UseVisualStyleBackColor = False
        '
        'fraOutLoadImpe
        '
        Me.fraOutLoadImpe.Controls.Add(Me.optLoadImp50)
        Me.fraOutLoadImpe.Controls.Add(Me.optLoadImpHigh)
        Me.fraOutLoadImpe.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraOutLoadImpe.Location = New System.Drawing.Point(8, 4)
        Me.fraOutLoadImpe.Name = "fraOutLoadImpe"
        Me.fraOutLoadImpe.Size = New System.Drawing.Size(178, 81)
        Me.fraOutLoadImpe.TabIndex = 9
        Me.fraOutLoadImpe.TabStop = False
        Me.fraOutLoadImpe.Text = "Output Load Reference"
        '
        'optLoadImp50
        '
        Me.optLoadImp50.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optLoadImp50.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optLoadImp50.Location = New System.Drawing.Point(8, 21)
        Me.optLoadImp50.Name = "optLoadImp50"
        Me.optLoadImp50.Size = New System.Drawing.Size(87, 20)
        Me.optLoadImp50.TabIndex = 43
        Me.optLoadImp50.Text = "50 Ohm"
        '
        'optLoadImpHigh
        '
        Me.optLoadImpHigh.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optLoadImpHigh.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optLoadImpHigh.Location = New System.Drawing.Point(8, 45)
        Me.optLoadImpHigh.Name = "optLoadImpHigh"
        Me.optLoadImpHigh.Size = New System.Drawing.Size(138, 21)
        Me.optLoadImpHigh.TabIndex = 42
        Me.optLoadImpHigh.Text = "High Impedance"
        '
        'fraOutLowPassFilt
        '
        Me.fraOutLowPassFilt.Controls.Add(Me.optCutOff50)
        Me.fraOutLowPassFilt.Controls.Add(Me.optCutOff25)
        Me.fraOutLowPassFilt.Controls.Add(Me.optCutOff20)
        Me.fraOutLowPassFilt.Controls.Add(Me.chkFilterStateOn)
        Me.fraOutLowPassFilt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraOutLowPassFilt.Location = New System.Drawing.Point(194, 4)
        Me.fraOutLowPassFilt.Name = "fraOutLowPassFilt"
        Me.fraOutLowPassFilt.Size = New System.Drawing.Size(226, 81)
        Me.fraOutLowPassFilt.TabIndex = 10
        Me.fraOutLowPassFilt.TabStop = False
        Me.fraOutLowPassFilt.Text = "Low Pass Filters"
        '
        'optCutOff50
        '
        Me.optCutOff50.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optCutOff50.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optCutOff50.Location = New System.Drawing.Point(8, 57)
        Me.optCutOff50.Name = "optCutOff50"
        Me.optCutOff50.Size = New System.Drawing.Size(141, 20)
        Me.optCutOff50.TabIndex = 46
        Me.optCutOff50.Text = "50 MHz Elliptical"
        '
        'optCutOff25
        '
        Me.optCutOff25.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optCutOff25.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optCutOff25.Location = New System.Drawing.Point(8, 37)
        Me.optCutOff25.Name = "optCutOff25"
        Me.optCutOff25.Size = New System.Drawing.Size(141, 20)
        Me.optCutOff25.TabIndex = 45
        Me.optCutOff25.Text = "25 MHz Elliptical"
        '
        'optCutOff20
        '
        Me.optCutOff20.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optCutOff20.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optCutOff20.Location = New System.Drawing.Point(8, 17)
        Me.optCutOff20.Name = "optCutOff20"
        Me.optCutOff20.Size = New System.Drawing.Size(141, 20)
        Me.optCutOff20.TabIndex = 44
        Me.optCutOff20.Text = "20 MHz Gaussian"
        '
        'chkFilterStateOn
        '
        Me.chkFilterStateOn.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFilterStateOn.Location = New System.Drawing.Point(166, 37)
        Me.chkFilterStateOn.Name = "chkFilterStateOn"
        Me.chkFilterStateOn.Size = New System.Drawing.Size(51, 20)
        Me.chkFilterStateOn.TabIndex = 11
        Me.chkFilterStateOn.Text = "On"
        '
        'tabOptions_Page5
        '
        Me.tabOptions_Page5.Controls.Add(Me.Atlas_SFP)
        Me.tabOptions_Page5.Location = New System.Drawing.Point(4, 22)
        Me.tabOptions_Page5.Name = "tabOptions_Page5"
        Me.tabOptions_Page5.Size = New System.Drawing.Size(461, 292)
        Me.tabOptions_Page5.TabIndex = 4
        Me.tabOptions_Page5.Text = "ATLAS"
        '
        'Atlas_SFP
        '
        Me.Atlas_SFP.ATLAS = Nothing
        Me.Atlas_SFP.BackColor = System.Drawing.SystemColors.Control
        Me.Atlas_SFP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Atlas_SFP.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Atlas_SFP.Location = New System.Drawing.Point(32, 12)
        Me.Atlas_SFP.Name = "Atlas_SFP"
        Me.Atlas_SFP.Parent_Object = Nothing
        Me.Atlas_SFP.Size = New System.Drawing.Size(397, 211)
        Me.Atlas_SFP.TabIndex = 112
        '
        'sbrUserInformation
        '
        Me.sbrUserInformation.Location = New System.Drawing.Point(0, 359)
        Me.sbrUserInformation.Name = "sbrUserInformation"
        Me.sbrUserInformation.Size = New System.Drawing.Size(492, 17)
        Me.sbrUserInformation.SizingGrip = False
        Me.sbrUserInformation.TabIndex = 53
        '
        'pcpFGDisplay
        '
        Me.pcpFGDisplay.ImageStream = CType(resources.GetObject("pcpFGDisplay.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.pcpFGDisplay.TransparentColor = System.Drawing.Color.Transparent
        Me.pcpFGDisplay.Images.SetKeyName(0, "Off.bmp")
        Me.pcpFGDisplay.Images.SetKeyName(1, "Sine1.bmp")
        Me.pcpFGDisplay.Images.SetKeyName(2, "Square1.bmp")
        Me.pcpFGDisplay.Images.SetKeyName(3, "Pulse1.bmp")
        Me.pcpFGDisplay.Images.SetKeyName(4, "Ramp1.bmp")
        Me.pcpFGDisplay.Images.SetKeyName(5, "Triangle1.bmp")
        Me.pcpFGDisplay.Images.SetKeyName(6, "Exponent1.BMP")
        Me.pcpFGDisplay.Images.SetKeyName(7, "Gaussian1.bmp")
        Me.pcpFGDisplay.Images.SetKeyName(8, "Sinc1.bmp")
        Me.pcpFGDisplay.Images.SetKeyName(9, "DC1.bmp")
        '
        'frmRac3152
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(492, 376)
        Me.Controls.Add(Me.cmdHelp)
        Me.Controls.Add(Me.cmdQuit)
        Me.Controls.Add(Me.cmdReset)
        Me.Controls.Add(Me.fraOnOff)
        Me.Controls.Add(Me.tabOptions)
        Me.Controls.Add(Me.sbrUserInformation)
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmRac3152"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Function Generator"
        Me.fraOnOff.ResumeLayout(False)
        Me.panFGDisplay.ResumeLayout(False)
        CType(Me.imgFGDisplay, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabOptions.ResumeLayout(False)
        Me.tabOptions_Page1.ResumeLayout(False)
        Me.fraSinc.ResumeLayout(False)
        Me.panSinc.ResumeLayout(False)
        Me.panSinc.PerformLayout()
        CType(Me.spnSinc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraTrai.ResumeLayout(False)
        Me.panTrai.ResumeLayout(False)
        Me.panTrai.PerformLayout()
        CType(Me.spnTrai, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraLead.ResumeLayout(False)
        Me.panLead.ResumeLayout(False)
        Me.panLead.PerformLayout()
        CType(Me.spnLead, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraDuty.ResumeLayout(False)
        Me.panDuty.ResumeLayout(False)
        Me.panDuty.PerformLayout()
        CType(Me.spnDuty, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraWidt.ResumeLayout(False)
        Me.panWidt.ResumeLayout(False)
        Me.panWidt.PerformLayout()
        CType(Me.spnWidt, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraDela.ResumeLayout(False)
        Me.panDela.ResumeLayout(False)
        Me.panDela.PerformLayout()
        CType(Me.spnDela, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraPhas.ResumeLayout(False)
        Me.panPhas.ResumeLayout(False)
        Me.panPhas.PerformLayout()
        CType(Me.spnPhas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraExpo.ResumeLayout(False)
        Me.panExpo.ResumeLayout(False)
        Me.panExpo.PerformLayout()
        CType(Me.spnExpo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraDcOffs.ResumeLayout(False)
        Me.panDcOffs.ResumeLayout(False)
        Me.panDcOffs.PerformLayout()
        CType(Me.spnDcOffs, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraFreq.ResumeLayout(False)
        Me.panFreq.ResumeLayout(False)
        Me.panFreq.PerformLayout()
        CType(Me.spnFreq, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraAmpl.ResumeLayout(False)
        Me.panAmpl.ResumeLayout(False)
        Me.panAmpl.PerformLayout()
        CType(Me.spnAmpl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraFunc.ResumeLayout(False)
        Me.fraFunc.PerformLayout()
        Me.tabOptions_Page2.ResumeLayout(False)
        Me.fraModeTrigLevel.ResumeLayout(False)
        Me.panModeTrigLevel.ResumeLayout(False)
        Me.panModeTrigLevel.PerformLayout()
        CType(Me.spnModeTrigLevel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraModeSelect.ResumeLayout(False)
        Me.fraModeTrigDela.ResumeLayout(False)
        Me.panModeTrigDela.ResumeLayout(False)
        Me.panModeTrigDela.PerformLayout()
        CType(Me.spnModeTrigDela, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraModeTrigBurst.ResumeLayout(False)
        Me.panModeTrigBurst.ResumeLayout(False)
        Me.panModeTrigBurst.PerformLayout()
        CType(Me.spnModeTrigBurst, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraModeTrigTime.ResumeLayout(False)
        Me.panModeTrigTime.ResumeLayout(False)
        Me.panModeTrigTime.PerformLayout()
        CType(Me.spnModeTrigTime, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraModeTrigSlop.ResumeLayout(False)
        Me.fraModeTrigSour.ResumeLayout(False)
        Me.tabOptions_Page3.ResumeLayout(False)
        Me.fraTrigOutVXI.ResumeLayout(False)
        Me.panIntTimer.ResumeLayout(False)
        Me.panIntTimer.PerformLayout()
        CType(Me.spnIntTimer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraTrigOutSyncSour.ResumeLayout(False)
        Me.fraTrigOutSyncOut.ResumeLayout(False)
        Me.fraVXIECLTrig.ResumeLayout(False)
        Me.fraVXITTLTrig.ResumeLayout(False)
        Me.tabOptions_Page4.ResumeLayout(False)
        Me.fraOutLoadImpe.ResumeLayout(False)
        Me.fraOutLowPassFilt.ResumeLayout(False)
        Me.tabOptions_Page5.ResumeLayout(False)
        Me.ResumeLayout(False)

End Sub
#End Region

    '=========================================================

    Private _updatingFromFile As Boolean
    Property UpdatingFromFile() As Boolean
        Get
            Return _updatingFromFile
        End Get
        Set(value As Boolean)
            _updatingFromFile = value
            If value Then
                Me.Cursor = Cursors.WaitCursor
            Else
                Me.Cursor = Cursors.Default
            End If
        End Set
    End Property

    Public Sub cboModeTrigSlop_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboModeTrigSlop.SelectedIndexChanged
        cboModeTrigSlop_Click()
    End Sub
    Public Sub cboModeTrigSlop_Click()
        SetCur(TRIG_SLOP) = cboModeTrigSlop.Text
        SendCommand(TRIG_SLOP)
    End Sub

    Public Sub cboModeTrigSour_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboModeTrigSour.SelectedIndexChanged
        cboModeTrigSour_Click()
    End Sub

    Public Sub cboModeTrigSour_Click()
        If cboModeTrigSour.Text = "Internal" Then
            fraModeTrigTime.Visible = True
        Else
            fraModeTrigTime.Visible = False
        End If
        If cboModeTrigSour.Text = "External" Then
            fraModeTrigSlop.Visible = True
            fraModeTrigLevel.Visible = True
        Else
            fraModeTrigSlop.Visible = False
            fraModeTrigLevel.Visible = False
        End If
        SetCur(TRIG_SOUR_ADV) = cboModeTrigSour.Text
        SendCommand(TRIG_SOUR_ADV)
    End Sub

    Private Sub cboTrigOutECL_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboTrigOutECL.SelectedIndexChanged
        SetCod(OUTP_ECLT) = ":OUTP:" & cboTrigOutECL.Text & " "
    End Sub

    Public Sub cboTrigOutTTL_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboTrigOutTTL.Leave
        SetCod(OUTP_TTLT) = ":OUTP:" & cboTrigOutTTL.Text & " "
    End Sub

    Public Sub chkFilterStateOn_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkFilterStateOn.CheckedChanged
        chkFilterStateOn_Click(chkFilterStateOn.Checked)
    End Sub

    Public Sub chkFilterStateOn_Click(ByVal Value As Boolean)
        If chkFilterStateOn.Checked = True Then
            SetCur(OUTP_FILT) = "ON"
        Else
            SetCur(OUTP_FILT) = "OFF"
        End If
        SendCommand(OUTP_FILT)
    End Sub

    Public Sub chkSyncOutStateOn_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkSyncOutStateOn.CheckedChanged
        chkSyncOutStateOn_Click(chkSyncOutStateOn.Checked)
    End Sub

    Public Sub chkSyncOutStateOn_Click(ByVal Value As Boolean)
        If Value = True Then
            SetCur(OUTP_SYNC) = "ON"
        Else
            SetCur(OUTP_SYNC) = "OFF"
        End If
        SendCommand(OUTP_SYNC)
    End Sub

    Public Sub chkTrigOutECLOn_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTrigOutECLOn.CheckedChanged
        chkTrigOutECLOn_Click(chkTrigOutECLOn.Checked)
    End Sub

    Public Sub chkTrigOutECLOn_Click(ByVal Value As Boolean)
        If Value = True Then
            SetCur(OUTP_ECLT) = "ON"
            cboTrigOutECL.Enabled = False
        Else
            SetCur(OUTP_ECLT) = "OFF"
            cboTrigOutECL.Enabled = True
        End If
        SendCommand(OUTP_ECLT)
    End Sub

    Public Sub chkTrigOutTTLOn_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTrigOutTTLOn.CheckedChanged
        chkTrigOutTTLOn_Click(chkTrigOutTTLOn.Checked)
    End Sub

    Public Sub chkTrigOutTTLOn_Click(ByVal Value As Boolean)
        If chkTrigOutTTLOn.Checked = True Then
            SetCur(OUTP_TTLT) = "ON"
            cboTrigOutTTL.Enabled = False
        Else
            SetCur(OUTP_TTLT) = "OFF"
            cboTrigOutTTL.Enabled = True
        End If
        SendCommand(OUTP_TTLT)
    End Sub

    Private Sub cmdAbout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAbout.Click
        frmAbout.cmdOK.Visible = True
        frmAbout.ShowDialog()
    End Sub

    Private Sub cmdHelp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdHelp.Click
        frmHelp.ShowDialog()
    End Sub

    Public Function Build_Atlas() As String
        Build_Atlas = ""

        Dim sTestString As String = ""
        Dim tmpFreq As Double
        Dim tmpNumber As Double
        Dim tmpString As String

        tmpFreq = Val(txtFreq.Text)
        If InStr(txtFreq.Text, "MHz") Then
            tmpFreq = (tmpFreq * 1000000)
        ElseIf InStr(txtFreq.Text, "kHz") Then
            tmpFreq = (tmpFreq * 1000)
        End If

        Select Case tolFunc.Buttons(WaveIdx - 1).Name
            Case "Sine"
                sTestString = "APPLY, AC SIGNAL," & vbCrLf & "VOLTAGE-PP " & txtAmpl.Text & "," & vbCrLf & "FREQ " & txtFreq.Text & "," & vbCrLf
                'dc-offset
                If Not (txtDcOffs.Text = "0 V") Then
                    sTestString &= "DC-OFFSET " & txtDcOffs.Text & "," & vbCrLf
                End If

                'sync to event and gated options
                sTestString &= OptionSyncGate()

                sTestString &= "CNX HI LO $"

            Case "Square"

                sTestString = "APPLY, SQUARE WAVE," & vbCrLf & "VOLTAGE-PP " & txtAmpl.Text & "," & vbCrLf & "FREQ " & txtFreq.Text & "," & vbCrLf
                'dc-offset
                If Not (txtDcOffs.Text = "0 V") Then
                    sTestString &= "DC-OFFSET " & txtDcOffs.Text & "," & vbCrLf
                End If

                'duty cycle
                If Not (txtDuty.Text = "") Then
                    sTestString &= "DUTY-CYCLE " & txtDuty.Text & "," & vbCrLf
                End If

                'sync to event and gated options
                sTestString &= OptionSyncGate()

                'bandwidth
                sTestString &= Bandwidth()


                sTestString &= "CNX HI LO $"

            Case "Triangle"
                sTestString = "APPLY, TRIANGULAR WAVE SIGNAL," & vbCrLf & "VOLTAGE-PP " & txtAmpl.Text & "," & vbCrLf & "FREQ " & txtFreq.Text & "," & vbCrLf
                'dc-offset
                If Not (txtDcOffs.Text = "0 V") Then
                    sTestString &= "DC-OFFSET " & txtDcOffs.Text & "," & vbCrLf
                End If

                'sync to event and gated options
                sTestString &= OptionSyncGate()

                'bandwidth
                sTestString &= Bandwidth()

                sTestString &= "CNX HI LO $"

            Case "Ramp"

                sTestString = "APPLY, RAMP SIGNAL," & vbCrLf & "VOLTAGE-PP " & txtAmpl.Text & "," & vbCrLf & "FREQ " & txtFreq.Text & "," & vbCrLf
                'dc-offset
                If Not (txtDcOffs.Text = "0 V") Then
                    sTestString &= "DC-OFFSET " & txtDcOffs.Text & "," & vbCrLf
                End If

                'Rise/Fall Time
                If Not (txtLead.Text = "0 %") Then
                    ' convert the percentage into seconds
                    tmpNumber = Val(txtLead.Text)
                    tmpNumber = (tmpNumber / tmpFreq)
                    tmpString = CStr(tmpNumber)
                    sTestString &= "RISE-TIME " & tmpString & " SEC," & vbCrLf
                End If
                If Not (txtTrai.Text = "0 %") Then
                    ' convert the percentage into seconds
                    tmpNumber = Val(txtTrai.Text)
                    tmpNumber = (tmpNumber / tmpFreq)
                    tmpString = CStr(tmpNumber)
                    sTestString &= "FALL-TIME " & tmpString & " SEC," & vbCrLf
                End If

                'sync to event and gated options
                sTestString &= OptionSyncGate()

                'bandwidth
                sTestString &= Bandwidth()

                sTestString &= "CNX HI LO $"

            Case "Pulse"
                sTestString = "APPLY, PULSED DC," & vbCrLf & "VOLTAGE-PP " & txtAmpl.Text & "," & vbCrLf & "PRF " & txtFreq.Text & "," & vbCrLf
                'dc-offset
                If Not (txtDcOffs.Text = "0 V") Then
                    sTestString &= "DC-OFFSET " & txtDcOffs.Text & "," & vbCrLf
                End If

                'duty-cycle or rise/width/fall times
                If Not (txtLead.Text = "0 %") Or Not (txtWidt.Text = "0 %") Or Not (txtTrai.Text = "0 %") Then
                    If Not (txtLead.Text = "0 %") Then
                        ' convert the percentage into seconds
                        tmpNumber = Val(txtLead.Text)
                        tmpNumber = (tmpNumber / tmpFreq)
                        tmpString = CStr(tmpNumber)
                        sTestString &= "RISE-TIME " & tmpString & " SEC, "
                    End If
                    If Not (txtWidt.Text = "0 %") Then
                        ' convert the percentage into seconds
                        tmpNumber = Val(txtWidt.Text)
                        tmpNumber = (tmpNumber / tmpFreq)
                        tmpString = CStr(tmpNumber)
                        sTestString &= "PULSE-WIDTH " & tmpString & " SEC, "
                    End If
                    If Not (txtTrai.Text = "0 %") Then
                        ' convert the percentage into seconds
                        tmpNumber = Val(txtTrai.Text)
                        tmpNumber = (tmpNumber / tmpFreq)
                        tmpString = CStr(tmpNumber)
                        sTestString &= "FALL-TIME " & tmpString & " SEC,"
                    End If

                    sTestString &= vbCrLf

                ElseIf Not (txtDuty.Text = "") Then
                    sTestString &= "DUTY-CYCLE " & txtDuty.Text & "," & vbCrLf
                End If

                'sync to event and gated options
                sTestString &= OptionSyncGate()

                'bandwidth
                sTestString &= Bandwidth()

                sTestString &= "CNX HI LO $"

            Case "Sinc"
                sTestString = "APPLY, SINC WAVE," & vbCrLf & "VOLTAGE-PP " & txtAmpl.Text & "," & vbCrLf & "FREQ " & txtFreq.Text & "," & vbCrLf
                'dc-offset
                If Not (txtDcOffs.Text = "0 V") Then
                    sTestString &= "DC-OFFSET " & txtDcOffs.Text & "," & vbCrLf
                End If

                'sync to event and gated options
                sTestString &= OptionSyncGate()

                'bandwidth
                sTestString &= Bandwidth()

                sTestString &= "CNX HI LO $"

            Case "Exponential"
                sTestString = "APPLY, EXPONENTIAL PULSE WAVE," & vbCrLf & "VOLTAGE-PP " & txtAmpl.Text & "," & vbCrLf & "FREQ " & txtFreq.Text & "," & vbCrLf
                'dc-offset
                If Not (txtDcOffs.Text = "0 V") Then
                    sTestString &= "DC-OFFSET " & txtDcOffs.Text & "," & vbCrLf
                End If

                'exponent
                sTestString &= "EXPONENT " & txtExpo.Text & "," & vbCrLf

                'sync to event and gated options
                sTestString &= OptionSyncGate()

                'bandwidth
                sTestString &= Bandwidth()

                sTestString &= "CNX HI LO $"

            Case "Gaussian"
                sTestString = "APPLY, GAUSSIAN PULSE WAVE," & vbCrLf & "VOLTAGE-PP " & txtAmpl.Text & "," & vbCrLf & "FREQ " & txtFreq.Text & "," & vbCrLf
                'dc-offset
                If Not (txtDcOffs.Text = "0 V") Then
                    sTestString &= "DC-OFFSET " & txtDcOffs.Text & "," & vbCrLf
                End If

                'exponent
                sTestString &= "EXPONENT " & txtExpo.Text & "," & vbCrLf

                'sync to event and gated options
                sTestString &= OptionSyncGate()

                'bandwidth
                sTestString &= Bandwidth()

                sTestString &= "CNX HI LO $"

            Case "DC"
                sTestString = "APPLY, DC SIGNAL," & vbCrLf & "VOLTAGE " & txtAmpl.Text & "," & vbCrLf

                sTestString &= "CNX HI LO $"
        End Select

        '   Return ATLAS Statement
        Build_Atlas = sTestString
    End Function

    Private Function OptionSyncGate() As String
        OptionSyncGate = ""
        Dim sTestString As String = ""

        'sync to event
        If optModeTrig.Checked = True Or optModeBurst.Checked = True Then
            sTestString = "SYNC TO EVENT " & "<event>" & " MAX-TIME " & "<dec value>" & "," & vbCrLf
        End If

        'gated
        If optModeGated.Checked = True Then
            sTestString &= "GATED FROM " & "<event-1>" & " TO " & "<event-2>" & "," & vbCrLf
        End If

        OptionSyncGate = sTestString
    End Function

    Private Function Bandwidth() As String
        Bandwidth = ""
        Dim sTestString As String = ""

        'bandwidth
        If chkFilterStateOn.Checked = True Then
            sTestString &= "BANDWIDTH "
            If optCutOff20.Checked = True Then
                sTestString &= "20 MHz," & vbCrLf
            ElseIf optCutOff25.Checked = True Then
                sTestString &= "25 MHz," & vbCrLf
            ElseIf optCutOff50.Checked = True Then
                sTestString &= "50 MHz," & vbCrLf
            End If
        End If

        Bandwidth = sTestString
    End Function

    Private Sub TextChange(ByVal nChang As Double, ByVal vCur As Short)
        If Val(SetCur(vCur)) <> nChang Then
            SetCur(vCur) = nChang
            SendCommand(vCur)
        End If
    End Sub

    Public Sub SetMode(ByRef sMode As String)
        Dim btn As System.Windows.Forms.ToolBarButton

        For Each btn In Me.tolFunc.Buttons
            btn.Pushed = False
        Next

        Select Case sMode
            Case "SIN"
                Me.tolFunc.Buttons("Sine").Pushed = True
                tolFunc_ButtonClick(Sine)

            Case "SQU"
                Me.tolFunc.Buttons("Square").Pushed = True
                tolFunc_ButtonClick(Square)

            Case "PULS"
                Me.tolFunc.Buttons("Pulse").Pushed = True
                tolFunc_ButtonClick(Pulse)

            Case "RAMP"
                Me.tolFunc.Buttons("Ramp").Pushed = True
                tolFunc_ButtonClick(Ramp)

            Case "TRI"
                Me.tolFunc.Buttons("Triangle").Pushed = True
                tolFunc_ButtonClick(Triangle)

            Case "EXP"
                Me.tolFunc.Buttons("Exponential").Pushed = True
                tolFunc_ButtonClick(Exponential)

            Case "GAU"
                Me.tolFunc.Buttons("Gaussian").Pushed = True
                tolFunc_ButtonClick(Gaussian)

            Case "SINC"
                Me.tolFunc.Buttons("Sinc").Pushed = True
                tolFunc_ButtonClick(Sinc)

            Case "DC"
                Me.tolFunc.Buttons("btnDC").Pushed = True
                tolFunc_ButtonClick(btnDC)
        End Select
    End Sub

    Public Function GetMode() As String
        GetMode = SetCur(FUNC_SHAPE)
    End Function

    Public Sub ConfigGetCurrent()
        Const curOnErrorGoToLabel_Default As Integer = 0
        Const curOnErrorGoToLabel_ErrorHandle As Integer = 1
        Dim vOnErrorGoToLabel As Integer = curOnErrorGoToLabel_Default
        Dim sInstrumentCmds As String
        Dim sReadBuffer As String
        Dim sShape As String
        Dim bTriggerCont As Boolean
        Dim sOutTrigSource As String
        Dim nLoop As Short
        Dim Msg As String

        Try
            If LiveMode Then ' change to Not LiveMode% to run simulator
                sReadBuffer = ""
                bInstrumentMode = True
                '*************************************************
                'Config SFP
                '*************************************************
                'Allow use of ActivateControl
                'sTIP_Mode = "GET CURR CONFIG"

                vOnErrorGoToLabel = curOnErrorGoToLabel_ErrorHandle ' On Error GoTo ErrorHandle

                '***********************Options Tab*************************
                '       NOTE: This tab is done first since the :HI command adjusts MIN/MAX
                '               for :DC and :VOLT commands
                '       *********
                '       Get Output Load Reference
                '       *********
                'EADS - removed 5/11/2006 not a valid command
                'sInstrumentCmds = "HI?"
                'WriteInstrument sInstrumentCmds

                '       ##########################################
                '       Action Required: Remove debug code
                '       Fill return value selections
                'gcolCmds.Add "Simulated response for: " & sInstrumentCmds
                'gcolCmds.Add "0"
                'gcolCmds.Add "1"
                '       ##########################################

                '       Retrieve instrument output buffer
                'sReadBuffer = ReadFG

                'Set Function Mode
                'If sReadBuffer = "1" Then
                '    FuncGenMain.ActivateControl (":HI ")
                'End If

                '       *********
                '       Get Low Pass Filter Frequency
                '       *********
                sInstrumentCmds = "OUTP:FILT:FREQ?"
                WriteInstrument(sInstrumentCmds)

                '       ##########################################
                '       Action Required: Remove debug code
                '       Fill return value selections
                'gcolCmds.Add "Simulated response for: " & sInstrumentCmds
                'gcolCmds.Add "20MHz"
                'gcolCmds.Add "25MHz"
                'gcolCmds.Add "50MHz"
                '       ##########################################

                '       Retrieve instrument output buffer
                sReadBuffer = ReadFG()
                If Len(sReadBuffer) > 2 Then
                    sReadBuffer = Strings.Left(sReadBuffer, Len(sReadBuffer) - 2)
                End If
                'Set Function Mode
                FuncGenMain.ActivateControl(":OUTP:FILT:FREQ " & sReadBuffer)

                '       *********
                '       Get Filter Enabled
                '       *********
                sInstrumentCmds = "OUTP:FILT?"
                WriteInstrument(sInstrumentCmds)

                '       ##########################################
                '       Action Required: Remove debug code
                '       Fill return value selections
                'gcolCmds.Add "Simulated response for: " & sInstrumentCmds
                'gcolCmds.Add "0"
                'gcolCmds.Add "1"
                '       ##########################################

                '       Retrieve instrument output buffer
                sReadBuffer = ReadFG()
                If Len(sReadBuffer) > 2 Then
                    sReadBuffer = Strings.Left(sReadBuffer, Len(sReadBuffer) - 2)
                End If


                'Set Function Mode
                If sReadBuffer = "1" Then
                    FuncGenMain.ActivateControl(":OUTP:FILT ON")
                Else
                    FuncGenMain.ActivateControl(":OUTP:FILT OFF")
                End If

                '**********************Functions Tab************************
                '       *********
                '       Get Function Shape
                '       *********
                sInstrumentCmds = "FUNC:SHAPE?"
                WriteInstrument(sInstrumentCmds)

                '       ##########################################
                '       Action Required: Remove debug code
                '       Fill return value selections
                'gcolCmds.Add "Simulated response for: " & sInstrumentCmds
                'gcolCmds.Add "SIN"
                'gcolCmds.Add "SQU"
                'gcolCmds.Add "PULS"
                'gcolCmds.Add "RAMP"
                'gcolCmds.Add "TRI"
                'gcolCmds.Add "EXP"
                'gcolCmds.Add "GAUS"
                'gcolCmds.Add "SINC"
                'gcolCmds.Add "DC"
                '       ##########################################

                '       Retrieve instrument output buffer
                sReadBuffer = ReadFG()
                If Len(sReadBuffer) > 2 Then
                    sReadBuffer = Strings.Left(sReadBuffer, Len(sReadBuffer) - 2)
                End If
                sShape = sReadBuffer ' Save for later use

                'Set Function Mode
                FuncGenMain.ActivateControl(":FUNC:SHAPE " & sShape)


                '       *********
                '       Get Shape specific info
                '       *********
                If sShape = "SIN" Or sShape = "TRI" Then
                    ' Phase
                    sInstrumentCmds = sShape & ":PHAS?"
                    WriteInstrument(sInstrumentCmds)

                    '           ##########################################
                    '           Action Required: Remove debug code
                    '           Fill return value selections
                    'gcolCmds.Add "Simulated response for: " & sInstrumentCmds
                    'gcolCmds.Add "0"
                    'gcolCmds.Add "90"
                    'gcolCmds.Add "180"
                    'gcolCmds.Add "240"
                    'gcolCmds.Add "320"
                    'gcolCmds.Add "360"
                    '           ##########################################

                    '           Retrieve instrument output buffer
                    sReadBuffer = ReadFG()
                    If Len(sReadBuffer) > 2 Then
                        sReadBuffer = Strings.Left(sReadBuffer, Len(sReadBuffer) - 2)
                    End If

                    'Set Function Mode
                    FuncGenMain.ActivateControl(":" & sShape & ":PHAS " & sReadBuffer)

                    'Power
                    sInstrumentCmds = sShape & ":POW?"
                    WriteInstrument(sInstrumentCmds)

                    '           ##########################################
                    '           Action Required: Remove debug code
                    '           Fill return value selections
                    'gcolCmds.Add "Simulated response for: " & sInstrumentCmds
                    'gcolCmds.Add "1"
                    'gcolCmds.Add "2"
                    'gcolCmds.Add "3"
                    'gcolCmds.Add "4"
                    'gcolCmds.Add "5"
                    'gcolCmds.Add "6"
                    'gcolCmds.Add "7"
                    'gcolCmds.Add "8"
                    'gcolCmds.Add "9"
                    '           ##########################################

                    '           Retrieve instrument output buffer
                    sReadBuffer = ReadFG()
                    If Len(sReadBuffer) > 2 Then
                        sReadBuffer = Strings.Left(sReadBuffer, Len(sReadBuffer) - 2)
                    End If

                    'Set Function Mode
                    FuncGenMain.ActivateControl(":" & sShape & ":POW " & sReadBuffer)

                ElseIf sShape = "SQU" Then
                    'Duty Cycle
                    sInstrumentCmds = sShape & ":DCYC?"
                    WriteInstrument(sInstrumentCmds)

                    '           ##########################################
                    '           Action Required: Remove debug code
                    '           Fill return value selections
                    'gcolCmds.Add "Simulated response for: " & sInstrumentCmds
                    'gcolCmds.Add "1"
                    'gcolCmds.Add "5"
                    'gcolCmds.Add "15"
                    'gcolCmds.Add "30"
                    'gcolCmds.Add "50"
                    'gcolCmds.Add "65"
                    'gcolCmds.Add "73"
                    'gcolCmds.Add "87"
                    'gcolCmds.Add "99"
                    '           ##########################################

                    '           Retrieve instrument output buffer
                    sReadBuffer = ReadFG()
                    If Len(sReadBuffer) > 2 Then
                        sReadBuffer = Strings.Left(sReadBuffer, Len(sReadBuffer) - 2)
                    End If

                    'Set Function Mode
                    FuncGenMain.ActivateControl(":" & sShape & ":DCYC " & sReadBuffer)

                ElseIf sShape = "PULS" Or sShape = "RAMP" Then

                    'Delay
                    sInstrumentCmds = sShape & ":DEL?"
                    WriteInstrument(sInstrumentCmds)

                    '           ##########################################
                    '           Action Required: Remove debug code
                    '           Fill return value selections
                    'gcolCmds.Add "Simulated response for: " & sInstrumentCmds
                    'gcolCmds.Add "1"
                    'gcolCmds.Add "5"
                    'gcolCmds.Add "8"
                    'gcolCmds.Add "10"
                    'gcolCmds.Add "17"
                    '           ##########################################

                    '           Retrieve instrument output buffer
                    sReadBuffer = ReadFG()
                    If Len(sReadBuffer) > 2 Then
                        sReadBuffer = Strings.Left(sReadBuffer, Len(sReadBuffer) - 2)
                    End If

                    'Set Function Mode
                    FuncGenMain.ActivateControl(":" & sShape & ":DEL " & sReadBuffer)

                    'Transition
                    sInstrumentCmds = sShape & ":TRAN?"
                    WriteInstrument(sInstrumentCmds)

                    '           ##########################################
                    '           Action Required: Remove debug code
                    '           Fill return value selections
                    'gcolCmds.Add "Simulated response for: " & sInstrumentCmds
                    'gcolCmds.Add "1"
                    'gcolCmds.Add "6"
                    'gcolCmds.Add "17"
                    'gcolCmds.Add "25"
                    'gcolCmds.Add "33"
                    'gcolCmds.Add "38"
                    'gcolCmds.Add "49.9"
                    '           ##########################################

                    '           Retrieve instrument output buffer
                    sReadBuffer = ReadFG()
                    If Len(sReadBuffer) > 2 Then
                        sReadBuffer = Strings.Left(sReadBuffer, Len(sReadBuffer) - 2)
                    End If

                    'Set Function Mode
                    FuncGenMain.ActivateControl(":" & sShape & ":TRAN " & sReadBuffer)

                    'Transition Traling
                    sInstrumentCmds = sShape & ":TRAN:TRA?"
                    WriteInstrument(sInstrumentCmds)

                    '           ##########################################
                    '           Action Required: Remove debug code
                    '           Fill return value selections
                    'gcolCmds.Add "Simulated response for: " & sInstrumentCmds
                    'gcolCmds.Add "1"
                    'gcolCmds.Add "5"
                    'gcolCmds.Add "10"
                    'gcolCmds.Add "15"
                    'gcolCmds.Add "23"
                    'gcolCmds.Add "27"
                    'gcolCmds.Add "33"
                    '           ##########################################

                    '           Retrieve instrument output buffer
                    sReadBuffer = ReadFG()

                    'Set Function Mode
                    FuncGenMain.ActivateControl(":" & sShape & ":TRAN:TRA " & sReadBuffer)

                    If sShape = "PULS" Then
                        'Width
                        sInstrumentCmds = sShape & ":WIDT?"
                        WriteInstrument(sInstrumentCmds)

                        '               ##########################################
                        '               Action Required: Remove debug code
                        '               Fill return value selections
                        'gcolCmds.Add "Simulated response for: " & sInstrumentCmds
                        'gcolCmds.Add "1"
                        'gcolCmds.Add "5"
                        'gcolCmds.Add "10"
                        'gcolCmds.Add "15"
                        'gcolCmds.Add "23"
                        'gcolCmds.Add "35"
                        'gcolCmds.Add "49.9"
                        '               ##########################################

                        '               Retrieve instrument output buffer
                        sReadBuffer = ReadFG()

                        'Set Function Mode
                        FuncGenMain.ActivateControl(":" & sShape & ":WIDT " & sReadBuffer)

                    End If

                ElseIf sShape = "SINC" Then
                    'N Cycle
                    sInstrumentCmds = sShape & ":NCYC?"
                    WriteInstrument(sInstrumentCmds)

                    '           ##########################################
                    '           Action Required: Remove debug code
                    '           Fill return value selections
                    'gcolCmds.Add "Simulated response for: " & sInstrumentCmds
                    'gcolCmds.Add "4"
                    'gcolCmds.Add "9"
                    'gcolCmds.Add "15"
                    'gcolCmds.Add "30"
                    'gcolCmds.Add "50"
                    'gcolCmds.Add "65"
                    'gcolCmds.Add "73"
                    'gcolCmds.Add "87"
                    'gcolCmds.Add "100"
                    '           ##########################################

                    '           Retrieve instrument output buffer
                    sReadBuffer = ReadFG()

                    'Set Function Mode
                    FuncGenMain.ActivateControl(":" & sShape & ":NCYC " & sReadBuffer)

                ElseIf sShape = "GAUS" Or sShape = "EXP" Then
                    'Exponent
                    sInstrumentCmds = sShape & ":EXP?"
                    WriteInstrument(sInstrumentCmds)

                    '           ##########################################
                    '           Action Required: Remove debug code
                    '           Fill return value selections
                    'gcolCmds.Add "Simulated response for: " & sInstrumentCmds
                    'gcolCmds.Add "1"
                    'gcolCmds.Add "30"
                    'gcolCmds.Add "50"
                    'gcolCmds.Add "65"
                    'gcolCmds.Add "73"
                    'gcolCmds.Add "150"
                    'gcolCmds.Add "200"
                    '           ##########################################

                    '           Retrieve instrument output buffer
                    sReadBuffer = ReadFG()

                    'Set Function Mode
                    FuncGenMain.ActivateControl(":" & sShape & ":EXP " & sReadBuffer)

                End If

                If sShape = "DC" Then ' Only amplitude needed

                    'DC
                    'sInstrumentCmds = sShape & "?"
                    sInstrumentCmds = ":VOLT?"
                    WriteInstrument(sInstrumentCmds)

                    '           ##########################################
                    '           Action Required: Remove debug code
                    '           Fill return value selections
                    'gcolCmds.Add "Simulated response for: " & sInstrumentCmds
                    'gcolCmds.Add "-8.0"
                    'gcolCmds.Add "-2.5"
                    'gcolCmds.Add "0.0"
                    'gcolCmds.Add "3.0"
                    'gcolCmds.Add "5.0"
                    'gcolCmds.Add "6.5"
                    'gcolCmds.Add "7.3"
                    'gcolCmds.Add "8.0"
                    '           ##########################################

                    '           Retrieve instrument output buffer
                    sReadBuffer = ReadFG()

                    'Set Function Mode
                    FuncGenMain.ActivateControl(":DC " & sReadBuffer)

                Else

                    'Frequency
                    sInstrumentCmds = "FREQ?"
                    WriteInstrument(sInstrumentCmds)

                    '           ##########################################
                    '           Action Required: Remove debug code
                    '           Fill return value selections
                    'gcolCmds.Add "Simulated response for: " & sInstrumentCmds
                    'gcolCmds.Add "0.0001"
                    'gcolCmds.Add "0.05"
                    'gcolCmds.Add "0.6"
                    'gcolCmds.Add "30"
                    'gcolCmds.Add "50000"
                    'gcolCmds.Add "650000"
                    'gcolCmds.Add "7300000"
                    'gcolCmds.Add "50000000"
                    '           ##########################################

                    '           Retrieve instrument output buffer
                    sReadBuffer = ReadFG()
                    If Len(sReadBuffer) > 2 Then
                        sReadBuffer = Strings.Left(sReadBuffer, Len(sReadBuffer) - 2)
                    End If

                    'Set Function Mode
                    FuncGenMain.ActivateControl(":FREQ " & sReadBuffer)

                    'Amplitude
                    sInstrumentCmds = "VOLT?"
                    WriteInstrument(sInstrumentCmds)

                    '           ##########################################
                    '           Action Required: Remove debug code
                    '           Fill return value selections
                    'gcolCmds.Add "Simulated response for: " & sInstrumentCmds
                    'gcolCmds.Add "0.01"
                    'gcolCmds.Add "0.5"
                    'gcolCmds.Add "1.2"
                    'gcolCmds.Add "3.0"
                    'gcolCmds.Add "5.0"
                    'gcolCmds.Add "6.5"
                    'gcolCmds.Add "7.3"
                    'gcolCmds.Add "16.0"
                    '           ##########################################

                    '           Retrieve instrument output buffer
                    sReadBuffer = ReadFG()
                    If Len(sReadBuffer) > 2 Then
                        sReadBuffer = Strings.Left(sReadBuffer, Len(sReadBuffer) - 2)
                    End If

                    'Set Function Mode
                    FuncGenMain.ActivateControl(":VOLT " & sReadBuffer)

                    'DC Offset
                    sInstrumentCmds = "VOLT:OFFS?"
                    WriteInstrument(sInstrumentCmds)

                    '           ##########################################
                    '           Action Required: Remove debug code
                    '           Fill return value selections
                    'gcolCmds.Add "Simulated response for: " & sInstrumentCmds
                    'gcolCmds.Add "-4.19"
                    'gcolCmds.Add "-2.5"
                    'gcolCmds.Add "0.0"
                    'gcolCmds.Add "0.65"
                    'gcolCmds.Add "1.0"
                    'gcolCmds.Add "2.5"
                    'gcolCmds.Add "3.3"
                    'gcolCmds.Add "4.19"
                    '           ##########################################

                    '           Retrieve instrument output buffer
                    sReadBuffer = ReadFG()
                    If Len(sReadBuffer) > 2 Then
                        sReadBuffer = Strings.Left(sReadBuffer, Len(sReadBuffer) - 2)
                    End If

                    'Set Function Mode
                    FuncGenMain.ActivateControl(":VOLT:OFFS " & sReadBuffer)

                End If

                '       *********
                '       Get Output
                '       *********
                sInstrumentCmds = "OUTP?"
                WriteInstrument(sInstrumentCmds)

                '       ##########################################
                '       Action Required: Remove debug code
                '       Fill return value selections
                'gcolCmds.Add "Simulated response for: " & sInstrumentCmds
                'gcolCmds.Add "0"
                'gcolCmds.Add "1"
                '       ##########################################

                '       Retrieve instrument output buffer
                sReadBuffer = ReadFG()
                If Len(sReadBuffer) > 2 Then
                    sReadBuffer = Strings.Left(sReadBuffer, Len(sReadBuffer) - 2)
                End If

                'Set Function Mode
                If sReadBuffer = "1" Then
                    FuncGenMain.ActivateControl(":OUTP ON")
                Else
                    FuncGenMain.ActivateControl(":OUTP OFF")
                End If

                '**********************Triggers Tab*************************

                '       *********
                '       Get Trigger Continous
                '       *********
                sInstrumentCmds = "INIT:CONT?"
                WriteInstrument(sInstrumentCmds)

                '       ##########################################
                '       Action Required: Remove debug code
                '       Fill return value selections
                'gcolCmds.Add "Simulated response for: " & sInstrumentCmds
                'gcolCmds.Add "0"
                'gcolCmds.Add "1"
                '       ##########################################

                '       Retrieve instrument output buffer
                sReadBuffer = ReadFG()
                If Len(sReadBuffer) > 2 Then
                    sReadBuffer = Strings.Left(sReadBuffer, Len(sReadBuffer) - 2)
                End If

                If sReadBuffer <> "" Then
                    bTriggerCont = CInt(sReadBuffer)

                    'Set Function Mode
                    If bTriggerCont Then
                        FuncGenMain.ActivateControl(":INIT:CONT ON")
                    Else
                        FuncGenMain.ActivateControl(":INIT:CONT OFF")

                        'Triggered Input: Normal/Burst/Gated
                        ConfigGetTrig()
                    End If
                End If
                '***********************Markers Tab*************************

                '       *********
                '       Get Output Sync Source
                '       *********
                sInstrumentCmds = "OUTP:SYNC:SOUR?"
                WriteInstrument(sInstrumentCmds)

                '       ##########################################
                '       Action Required: Remove debug code
                '       Fill return value selections
                'gcolCmds.Add "Simulated response for: " & sInstrumentCmds
                'gcolCmds.Add "BIT"
                'gcolCmds.Add "HCLock"
                '       ##########################################

                '       Retrieve instrument output buffer
                sReadBuffer = ReadFG()
                If Len(sReadBuffer) > 2 Then
                    sReadBuffer = Strings.Left(sReadBuffer, Len(sReadBuffer) - 2)
                End If

                'Set Function Mode
                FuncGenMain.ActivateControl(":OUTP:SYNC:SOUR " & sReadBuffer)

                '       *********
                '       Get Output Sync Enable
                '       *********
                sInstrumentCmds = "OUTP:SYNC?"
                WriteInstrument(sInstrumentCmds)

                '       ##########################################
                '       Action Required: Remove debug code
                '       Fill return value selections
                'gcolCmds.Add "Simulated response for: " & sInstrumentCmds
                'gcolCmds.Add "0"
                'gcolCmds.Add "1"
                '       ##########################################

                '       Retrieve instrument output buffer
                sReadBuffer = ReadFG()
                If Len(sReadBuffer) > 2 Then
                    sReadBuffer = Strings.Left(sReadBuffer, Len(sReadBuffer) - 2)
                End If

                'Set Function Mode
                If sReadBuffer = "1" Then
                    FuncGenMain.ActivateControl(":OUTP:SYNC ON")
                Else
                    FuncGenMain.ActivateControl(":OUTP:SYNC OFF")
                End If

                '       *********
                '       Get Output TTL Source
                '       *********
                sInstrumentCmds = "OUTP:TRIG:SOUR?"
                WriteInstrument(sInstrumentCmds)

                '       ##########################################
                '       Action Required: Remove debug code
                '       Fill return value selections
                '        gcolCmds.Add "Simulated response for: " & sInstrumentCmds
                '        gcolCmds.Add "BIT"
                '        gcolCmds.Add "External"
                '        gcolCmds.Add "Internal"
                '       ##########################################

                '       Retrieve instrument output buffer
                sReadBuffer = ReadFG()
                If Len(sReadBuffer) > 2 Then
                    sReadBuffer = Strings.Left(sReadBuffer, Len(sReadBuffer) - 2)
                End If

                sOutTrigSource = sReadBuffer
                'Set Function Mode
                FuncGenMain.ActivateControl(":OUTP:TRIG:SOUR " & sOutTrigSource)

                If sOutTrigSource = "Internal" Then
                    '*********
                    'Get Output TTL Time
                    '*********
                    sInstrumentCmds = "TRIG:TIM?"
                    WriteInstrument(sInstrumentCmds)

                    '##########################################
                    'Action Required: Remove debug code
                    'Fill return value selections
                    'gcolCmds.Add "Simulated response for: " & sInstrumentCmds
                    'gcolCmds.Add "0.000015"
                    'gcolCmds.Add "0.003"
                    'gcolCmds.Add "0.6"
                    'gcolCmds.Add "10"
                    'gcolCmds.Add "160"
                    'gcolCmds.Add "420"
                    'gcolCmds.Add "1000"
                    '##########################################

                    'Retrieve instrument output buffer
                    sReadBuffer = ReadFG()
                    'Set Function Mode
                    FuncGenMain.ActivateControl(":TRIG:TIM " & sReadBuffer)

                End If

                For nLoop = 0 To 7
                    '*********
                    'Get TTL Marker Output
                    '*********
                    sInstrumentCmds = "OUTP:TTLT" & nLoop & "?"
                    WriteInstrument(sInstrumentCmds)

                    '##########################################
                    'Action Required: Remove debug code
                    'Fill return value selections
                    'gcolCmds.Add "Simulated response for: " & sInstrumentCmds
                    'gcolCmds.Add "0"
                    'gcolCmds.Add "1"
                    '##########################################

                    'Retrieve instrument output buffer
                    sReadBuffer = ReadFG()
                    If Len(sReadBuffer) > 2 Then
                        sReadBuffer = Strings.Left(sReadBuffer, Len(sReadBuffer) - 2)
                    End If
                    'Set Function Mode
                    If sReadBuffer = "1" Then
                        FuncGenMain.ActivateControl(":OUTP:TTLTrg" & nLoop & " ON")
                        Exit For
                    End If
                Next nLoop

                For nLoop = 0 To 1
                    '*********
                    'Get ECL Marker Output
                    '*********
                    sInstrumentCmds = "OUTP:ECLT" & nLoop & "?"
                    WriteInstrument(sInstrumentCmds)

                    '##########################################
                    'Action Required: Remove debug code
                    'Fill return value selections
                    'gcolCmds.Add "Simulated response for: " & sInstrumentCmds
                    'gcolCmds.Add "0"
                    'gcolCmds.Add "1"
                    '##########################################

                    'Retrieve instrument output buffer
                    sReadBuffer = ReadFG()
                    'Set Function Mode
                    If sReadBuffer = "1" Then
                        FuncGenMain.ActivateControl(":OUTP:ECLTrg" & nLoop & " ON")
                        Exit For
                    End If
                Next nLoop
                '***********************************************************
            bInstrumentMode = False 'Clear this out before exiting
            End If
        Catch
            Select Case vOnErrorGoToLabel
                Case curOnErrorGoToLabel_ErrorHandle
                    If Err.Number = 13 Then
                        Msg = "Type Mismatch: Data received is not what was expected"
                        MsgBox(Msg, MsgBoxStyle.Exclamation)
                        'Type Mismatch
                    Else
                        ' Display unanticipated error message.
                        Msg = "Unanticipated error " & Err.Number & ": " & Err.Description
                        MsgBox(Msg, MsgBoxStyle.Critical)
                    End If
                Case curOnErrorGoToLabel_Default
                    ' ...
                Case Else
                    ' ...
            End Select
            bInstrumentMode = False 'Clear this out before exiting
        End Try
    End Sub

    Private Sub ConfigGetTrig()
        Dim sInstrumentCmds As String
        Dim sReadBuffer As String
        Dim bTrigGate As Boolean
        Dim bTrigBurst As Boolean
        Dim sTrigSource As String = ""

        '   *********
        '   Get Trigger Burst
        '   *********
        sInstrumentCmds = "TRIG:BURS?"
        WriteInstrument(sInstrumentCmds)

        '   ##########################################
        '   Action Required: Remove debug code
        '   Fill return value selections
        '    gcolCmds.Add "Simulated response for: " & sInstrumentCmds
        '    gcolCmds.Add "0"
        '    gcolCmds.Add "1"
        '   ##########################################

        '   Retrieve instrument output buffer
        sReadBuffer = ReadFG()
        bTrigBurst = CInt(sReadBuffer)

        'Set Function Mode
        If bTrigBurst Then
            FuncGenMain.ActivateControl(":TRIG:BURS ON")
        Else
            FuncGenMain.ActivateControl(":TRIG:BURS OFF")

            '*********
            'Get Trigger Gated
            '*********
            sInstrumentCmds = "TRIG:GATE?"
            WriteInstrument(sInstrumentCmds)

            '##########################################
            'Action Required: Remove debug code
            'Fill return value selections
            'gcolCmds.Add "Simulated response for: " & sInstrumentCmds
            'gcolCmds.Add "0"
            'gcolCmds.Add "1"
            '##########################################

            'Retrieve instrument output buffer
            sReadBuffer = ReadFG()
            bTrigGate = CInt(sReadBuffer)

            'Set Function Mode
            If bTrigGate Then
                FuncGenMain.ActivateControl(":TRIG:GATE ON")
            Else
                FuncGenMain.ActivateControl(":TRIG:GATE OFF")
            End If
        End If

        If bTrigGate = False Then
            'Normal/Burst Trigger

            '*********
            'Get Source
            '*********
            sInstrumentCmds = "TRIG:SOUR:ADV?"
            WriteInstrument(sInstrumentCmds)

            '       ##########################################
            '       Action Required: Remove debug code
            '       Fill return value selections
            'gcolCmds.Add "Simulated response for: " & sInstrumentCmds
            'gcolCmds.Add "External"
            'gcolCmds.Add "Internal"
            'gcolCmds.Add "TTLTrg0"
            ' gcolCmds.Add "TTLTrg1"
            'gcolCmds.Add "TTLTrg2"
            ' gcolCmds.Add "TTLTrg3"
            'gcolCmds.Add "TTLTrg4"
            'gcolCmds.Add "TTLTrg5"
            'gcolCmds.Add "TTLTrg6"
            'gcolCmds.Add "TTLTrg7"
            '       ##########################################

            '       Retrieve instrument output buffer
            sReadBuffer = ReadFG()
            If Len(sReadBuffer) > 2 Then
                sReadBuffer = Strings.Left(sReadBuffer, Len(sReadBuffer) - 2)
            End If
            sTrigSource = sReadBuffer

            FuncGenMain.ActivateControl(":TRIG:SOUR:ADV " & sTrigSource)
        End If

        'Triggered Input: Normal/Burst/Gated

        If sTrigSource = "" Or sTrigSource = "External" Then
            '*********
            'Get Slope
            '*********
            sInstrumentCmds = "TRIG:SLOP?"
            WriteInstrument(sInstrumentCmds)

            '##########################################
            'Action Required: Remove debug code
            'Fill return value selections
            'gcolCmds.Add "Simulated response for: " & sInstrumentCmds
            'gcolCmds.Add "Positive"
            'gcolCmds.Add "Negative"
            '##########################################

            'Retrieve instrument output buffer
            sReadBuffer = ReadFG()
            If Len(sReadBuffer) > 2 Then
                sReadBuffer = Strings.Left(sReadBuffer, Len(sReadBuffer) - 2)
            End If

            FuncGenMain.ActivateControl(":TRIG:SLOP " & sReadBuffer)

            '*********
            'Get Level
            '*********
            sInstrumentCmds = "TRIG:LEV?"
            WriteInstrument(sInstrumentCmds)

            '##########################################
            'Action Required: Remove debug code
            'Fill return value selections
            'gcolCmds.Add "Simulated response for: " & sInstrumentCmds
            'gcolCmds.Add "-10"
            'gcolCmds.Add "-5.3"
            'gcolCmds.Add "-1.6"
            'gcolCmds.Add "0"
            'gcolCmds.Add "1.6"
            'gcolCmds.Add "4.2"
            'gcolCmds.Add "10"
            '##########################################

            'Retrieve instrument output buffer
            sReadBuffer = ReadFG()

            FuncGenMain.ActivateControl(":TRIG:LEV " & sReadBuffer)

        ElseIf sTrigSource = "Internal" Then
            '*********
            'Get Time
            '*********
            sInstrumentCmds = "TRIG:TIM?"
            WriteInstrument(sInstrumentCmds)

            '##########################################
            'Action Required: Remove debug code
            'Fill return value selections
            'gcolCmds.Add "Simulated response for: " & sInstrumentCmds
            'gcolCmds.Add "0.000015"
            'gcolCmds.Add "0.003"
            'gcolCmds.Add "0.6"
            'gcolCmds.Add "10"
            'gcolCmds.Add "160"
            'gcolCmds.Add "420"
            'gcolCmds.Add "1000"
            '##########################################

            'Retrieve instrument output buffer
            sReadBuffer = ReadFG()

            FuncGenMain.ActivateControl(":TRIG:TIM " & sReadBuffer)
        End If

        If bTrigGate = False Then
            'Normal/Burst Trigger

            '*********
            'Get Delay
            '*********
            sInstrumentCmds = "TRIG:DEL?"
            WriteInstrument(sInstrumentCmds)

            '       ##########################################
            '       Action Required: Remove debug code
            '       Fill return value selections
            'gcolCmds.Add "Simulated response for: " & sInstrumentCmds
            'gcolCmds.Add "0.0000001"
            'gcolCmds.Add "0.000002"
            'gcolCmds.Add "0.00007"
            'gcolCmds.Add "0.00015"
            'gcolCmds.Add "0.0005"
            'gcolCmds.Add "0.005"
            'gcolCmds.Add "0.02"
            '       ##########################################

            '       Retrieve instrument output buffer
            sReadBuffer = ReadFG()

            FuncGenMain.ActivateControl(":TRIG:DEL " & sReadBuffer)

            If bTrigBurst = True Then 'Burst

                '*********
                'Get Burst Count
                '*********
                sInstrumentCmds = "TRIG:COUN?"
                WriteInstrument(sInstrumentCmds)

                '##########################################
                'Action Required: Remove debug code
                'Fill return value selections
                'gcolCmds.Add "Simulated response for: " & sInstrumentCmds
                'gcolCmds.Add "1"
                'gcolCmds.Add "60"
                'gcolCmds.Add "900"
                'gcolCmds.Add "2000"
                'gcolCmds.Add "60000"
                'gcolCmds.Add "500000"
                'gcolCmds.Add "1000000"
                '##########################################

                'Retrieve instrument output buffer
                sReadBuffer = ReadFG()

                FuncGenMain.ActivateControl(":TRIG:COUN " & sReadBuffer)
            End If
        End If
    End Sub

    Private Sub cmdModeSoftTrig_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdModeSoftTrig.Click
        cboModeTrigSour.SelectedIndex = 0 'Select External
        WriteInstrument("*TRG")
    End Sub

    Public Sub cmdQuit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdQuit.Click
        cmdQuit_Click()
    End Sub

    Public Sub cmdQuit_Click()
        '  SetCur$(OUTP) = "OFF"
        '  SendCommand OUTP
        'just clear the instrument buffers, do not reset
        ' If the SFP is up in remote mode, should it reset on exit?
        'WriteInstrument("*CLS")
        If LiveMode Then
            ErrorStatus = atxml_Close()
        End If
        Me.Close()
    End Sub

    Public Sub cmdReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdReset.Click
        cmdReset_Click()
    End Sub

    Public Sub cmdReset_Click()
        'this indicates there is to be no cmds sent
        DoNotTalk = True
        SetControlsToReset()
        DoNotTalk = False
        WriteInstrument("*CLS;*RST")
        'this indicates that we are only to query
        bInstrumentMode = True
        ConfigGetCurrent()
        bInstrumentMode = False
        tabOptions.SelectedIndex = TAB_FUNCTIONS
    End Sub

    Private Sub cmdRunWaveCAD_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRunWaveCAD.Click
        Dim WaveCad As String

        WaveCad = Application.StartupPath & "\WCAD34.EXE" ' USED TO BE WCAD3032.EXE"
        ChDir(Application.StartupPath)
        ChDrive(Strings.Left(Application.StartupPath, 1))
        If System.IO.File.Exists(WaveCad) Then
            DoNotTalk = True
            SetControlsToReset()
            DoNotTalk = False
            WriteInstrument("*CLS;*RST")
            Me.WindowState = FormWindowState.Minimized
            ExecCmd(WaveCad)
            Me.WindowState = FormWindowState.Normal
        Else
            MsgBox("Can not run WaveCAD." & vbCrLf & "Can not find file " & WaveCad & ".", MsgBoxStyle.Exclamation)
        End If
    End Sub

    Private Sub cmdTest_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdTest.Click
        Dim ReturnCode As Short
        Dim message As String

        HelpPanel("Performing Built-In Test...")
        If Not LiveMode Then
            MsgBox("Built-In Test is not available.  Live mode is disabled.", MsgBoxStyle.Information)
            HelpPanel("")
            Exit Sub
        End If

        Application.DoEvents()
        Me.Enabled = False 'Disable user interaction
        WriteInstrument("TEST?")
        ReturnCode = Val(ReadFG())
        HelpPanel("Built-In Test completed.")
        If ReturnCode = 0 Then
            MsgBox("Built-In Test Passed", MsgBoxStyle.Information)
        Else
            message = "Built-In Test Failed." & vbCrLf
            If ReturnCode And 1 Then
                message &= "DAC, DAC control, output amplifier or amplitude control failure."
            End If
            If ReturnCode And 2 Then
                message &= "Offset amplifier or offset control failure."
            End If
            If ReturnCode And 4 Then
                message &= "CPU to peripheral communication failure."
            End If
            If ReturnCode And 8 Then
                message &= "Trigger circuit or internal trigger failure."
            End If
            If ReturnCode And 16 Then
                message &= "Sequence or burst generator failure."
            End If
            If ReturnCode And 32 Then
                message &= "Clock generator failure."
            End If
            If ReturnCode > 63 Then
                message &= "Unknown failure code:" & Str(ReturnCode)
            End If
            MsgBox(message, MsgBoxStyle.Critical)
        End If
        HelpPanel("")
        Me.Enabled = True 'Enable user interaction
    End Sub

    Private Sub cmdUpdateTIP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdUpdateTIP.Click
        Dim sTIP_CMDstring As String

        'Disable user interaction
        Me.Enabled = False

        sTIP_CMDstring = sGetCommandString()
        SetKey("TIPS", "CMD", sTIP_CMDstring)
        SetKey("TIPS", "STATUS", "Ready")
        Me.cmdQuit_Click()
    End Sub

    Private Sub frmRac3152_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Center this form
        Me.Top = PrimaryScreen.Bounds.Height / 2 - Me.Height / 2
        Me.Left = PrimaryScreen.Bounds.Width / 2 - Me.Width / 2

        '   Set Common Controls parent properties
        Atlas_SFP.Parent_Object = Me
        Panel_Conifg.Parent_Object = Me

        Main()
    End Sub

    Private Sub frmRac3152_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseMove
        HelpPanel("")
    End Sub

    Private Sub frmRac3152_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        Form_Resize()
    End Sub

    Public Sub Form_Resize()
        If DoNotTalk Then Exit Sub
        If Me.WindowState = MINIMIZED Then Exit Sub

        tabOptions.SetBounds(8, 0, Limit(Me.Width - 39, 0), Limit(Me.Height - 96, 0), BoundsSpecified.Location Or BoundsSpecified.Size)
        cmdHelp.SetBounds(Limit(Me.Width - 273, 0), Limit(Me.Height - 86, 0), 0, 0, BoundsSpecified.Location)
        cmdReset.SetBounds(Limit(Me.Width - 192, 0), Limit(Me.Height - 86, 0), 0, 0, BoundsSpecified.Location)
        cmdQuit.SetBounds(Limit(Me.Width - 107, 0), Limit(Me.Height - 86, 0), 0, 0, BoundsSpecified.Location)
        fraOnOff.SetBounds(Limit(Me.Width - 161, 0), Limit(Me.Height - 155, 0), 0, 0, BoundsSpecified.Location)
    End Sub

    Private Sub frmRac3152_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Dim Cancel As Short = 0
        If Cancel <> 0 Then e.Cancel = True
    End Sub

    Private Sub fraAmpl_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles fraAmpl.MouseMove
        If tolFunc.Buttons(WaveIdx - 1).Name = "btnDC" Then
            ShowVals(DC)
        Else
            ShowVals(VOLT)
        End If
    End Sub

    Private Sub fraDcOffs_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles fraDcOffs.MouseMove
        ShowVals(VOLT_OFFS)
    End Sub

    Private Sub fraDela_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles fraDela.MouseMove
        Dim SetIdx As Short

        Select Case tolFunc.Buttons(WaveIdx - 1).Name
            Case "Pulse"
                SetIdx = PULS_DEL
            Case "Ramp"
                SetIdx = RAMP_DEL
        End Select
        SetMax(SetIdx) = GetMaxTime(SetIdx)
        ShowVals(SetIdx)
    End Sub

    Private Sub fraDuty_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles fraDuty.MouseMove
        ShowVals(SQU_DCYC)
    End Sub

    Private Sub fraExpo_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles fraExpo.MouseMove
        Select Case Me.tolFunc.Buttons(WaveIdx - 1).Name
            Case "Sine"
                ShowVals(SIN_POW)
            Case "Triangle"
                ShowVals(TRI_POW)
            Case "Exponential"
                ShowVals(EXP_EXP)
            Case "Gaussian"
                ShowVals(GAUS_EXP)
        End Select
    End Sub

    Private Sub fraFreq_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles fraFreq.MouseMove
        ShowVals(FREQ)
    End Sub

    Private Sub fraLead_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles fraLead.MouseMove
        Dim SetIdx As Short

        Select Case tolFunc.Buttons(WaveIdx - 1).Name
            Case "Pulse"
                SetIdx = PULS_TRAN
            Case "Ramp"
                SetIdx = RAMP_TRAN
        End Select
        SetMax(SetIdx) = GetMaxTime(SetIdx)
        ShowVals(SetIdx)
    End Sub

    Private Sub fraModeTrigBurst_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles fraModeTrigBurst.MouseMove
        ShowVals(TRIG_COUN)
    End Sub

    Private Sub fraModeTrigDela_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles fraModeTrigDela.MouseMove
        ShowVals(TRIG_DEL)
    End Sub

    Private Sub fraModeTrigLevel_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles fraModeTrigLevel.MouseMove
        ShowVals(TRIG_LEV)
    End Sub

    Private Sub fraModeTrigTime_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles fraModeTrigTime.MouseMove
        ShowVals(TRIG_TIM)
    End Sub

    Private Sub fraPhas_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles fraPhas.MouseMove
        ShowVals(SIN_PHAS)
    End Sub

    Private Sub fraSinc_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles fraSinc.MouseMove
        ShowVals(SINC_NCYC)
    End Sub

    Private Sub fraTrai_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles fraTrai.MouseMove
        Dim SetIdx As Short

        Select Case tolFunc.Buttons(WaveIdx - 1).Name
            Case "Pulse"
                SetIdx = PULS_TRAN_TRA
            Case "Ramp"
                SetIdx = RAMP_TRAN_TRA
        End Select
        SetMax(SetIdx) = GetMaxTime(SetIdx)
        ShowVals(SetIdx)
    End Sub

    Private Sub fraTrigOutVXI_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles fraTrigOutVXI.MouseMove
        HelpPanel("")
    End Sub

    Private Sub fraWidt_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles fraWidt.MouseMove
        SetMax(PULS_WIDT) = GetMaxTime(PULS_WIDT)
        ShowVals(PULS_WIDT)
    End Sub

    '#Const defHas_imgLogo = True
#If defHas_imgLogo Then
    Private Sub imgLogo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles imgLogo.Click
        Me.WindowState = FormWindowState.Normal
'The appropriate value for these in pixels instead of twips will have to be calculated once the need for this function is determined
'        Me.Width = 600
'        Me.Height = 600
        Form_Resize()
    End Sub

    Private Sub imgLogo_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles imgLogo.MouseDown
        Dim Button As Short = e.Button \ &H100000
        Dim Shift As Short = Control.ModifierKeys \ &H10000

        Static Prompt As String = ""
        Dim ReturnString As String

        If Button = 1 And Shift = 2 Then ' If Ctrl-LEFT button
            Prompt = InputBox("Enter an instrument command:", , Prompt)
            If InStr(UCase(Prompt), "DEBUG ON") Then
                LiveMode = False
            ElseIf InStr(UCase(Prompt), "DEBUG OFF") Then
                LiveMode = True
            Else
                WriteInstrument(Prompt)
            End If
            If InStr(Prompt, "?") Then
                ReturnString = ReadFG()
                If ReturnString <> "" Then
                    MsgBox(ReturnString, MsgBoxStyle.Information, "Read from instrument:")
                End If
            End If
        ElseIf Button = 1 And Shift = 4 Then            ' If Alt-LEFT button
            ReturnString = ReadFG()
            If ReturnString <> "" Then
                MsgBox(ReturnString, MsgBoxStyle.Information, "Read from instrument:")
            End If
        ElseIf Button = 2 Then            ' If Right button
            WriteInstrument("SYST:ERR?")
            ReturnString = ReadFG()
            If ReturnString <> "" Then
                MsgBox(ReturnString, MsgBoxStyle.Information, "Read from instrument:")
            End If
        End If
    End Sub
#End If

    Public Sub optCutOff20_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optCutOff20.CheckedChanged
        optCutOff20_Click(optCutOff20.Checked)
    End Sub

    Public Sub optCutOff20_Click(ByVal Value As Boolean)
        SetCur(OUTP_FILT_FREQ) = "20MHz"
        SendCommand(OUTP_FILT_FREQ)
    End Sub

    Public Sub optCutOff25_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optCutOff25.CheckedChanged
        optCutOff25_Click(optCutOff25.Checked)
    End Sub

    Public Sub optCutOff25_Click(ByVal Value As Boolean)
        SetCur(OUTP_FILT_FREQ) = "25MHz"
        SendCommand(OUTP_FILT_FREQ)
    End Sub

    Public Sub optCutOff50_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optCutOff50.CheckedChanged
        optCutOff50_Click(optCutOff50.Checked)
    End Sub

    Public Sub optCutOff50_Click(ByVal Value As Boolean)
        SetCur(OUTP_FILT_FREQ) = "50MHz"
        SendCommand(OUTP_FILT_FREQ)
    End Sub

    Public Sub optLoadImp50_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optLoadImp50.CheckedChanged
        If optLoadImp50.Checked = True Then
            bHi = False

            SetDef(DC) = "2.5"
            SetMin(DC) = "-8"
            SetMax(DC) = "8"
            SetCur(DC) = Val(SetCur(DC)) / 2

            SetDef(VOLT) = "5"
            SetMin(VOLT) = "0.01"
            SetMax(VOLT) = "16"
            SetCur(VOLT) = Val(SetCur(VOLT)) / 2

            If tolFunc.Buttons(WaveIdx - 1).Name = "btnDC" Then
                txtAmpl.Text = EngNotate(Val(SetCur(DC)), DC)
            Else
                txtAmpl.Text = EngNotate(Val(SetCur(VOLT)), VOLT)
                SetCur(VOLT_OFFS) = Val(SetCur(VOLT_OFFS)) / 2
                txtDcOffs.Text = EngNotate(Val(SetCur(VOLT_OFFS)), VOLT_OFFS)
                AdjustVoltOffs()
            End If
        End If
    End Sub

    Public Sub optLoadImpHigh_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optLoadImpHigh.CheckedChanged
        If optLoadImpHigh.Checked = True Then
            bHi = True
            SetDef(DC) = "5"
            SetMin(DC) = "-16"
            SetMax(DC) = "16"
            SetCur(DC) = Val(SetCur(DC)) * 2

            SetDef(VOLT) = "10"
            SetMin(VOLT) = "0.02"
            SetMax(VOLT) = "32"
            SetCur(VOLT) = Val(SetCur(VOLT)) * 2


            If tolFunc.Buttons(WaveIdx - 1).Name = "btnDC" Then
                txtAmpl.Text = EngNotate(Val(SetCur(DC)), DC)
            Else
                txtAmpl.Text = EngNotate(Val(SetCur(VOLT)), VOLT)
                SetCur(VOLT_OFFS) = Val(SetCur(VOLT_OFFS)) * 2
                txtDcOffs.Text = EngNotate(Val(SetCur(VOLT_OFFS)), VOLT_OFFS)
                AdjustVoltOffs()
            End If
        End If
    End Sub

    Public Sub optModeBurst_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optModeBurst.CheckedChanged
        optModeBurst_Click(optModeBurst.Checked)
    End Sub

    Public Sub optModeBurst_Click(ByVal Value As Boolean)
        SetCur(INIT_CONT) = "OFF"
        SendCommand(INIT_CONT)
        SetCur(TRIG_GATE) = "OFF"
        SendCommand(TRIG_GATE)
        SendCommand(TRIG_COUN)
        SetCur(TRIG_BURS) = "ON"
        SendCommand(TRIG_BURS)
        fraModeTrigSour.Visible = True
        If cboModeTrigSour.Text = "External" Then
            fraModeTrigSlop.Visible = True
            fraModeTrigLevel.Visible = True
        End If
        fraModeTrigSlop.Text = "Slope"
        If cboModeTrigSour.Text = "Internal" Then
            fraModeTrigTime.Visible = True
        End If
        fraModeTrigDela.Visible = True
        cmdModeSoftTrig.Visible = True
        fraModeTrigBurst.Visible = True
    End Sub

    Public Sub optModeCont_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optModeCont.CheckedChanged
        optModeCont_Click(optModeCont.Checked)
    End Sub

    Public Sub optModeCont_Click(ByVal Value As Boolean)
        SetCur(TRIG_BURS) = "OFF"
        SendCommand(TRIG_BURS)
        SetCur(TRIG_GATE) = "OFF"
        SendCommand(TRIG_GATE)
        SetCur(INIT_CONT) = "ON"
        SendCommand(INIT_CONT)
        fraModeTrigSour.Visible = False
        fraModeTrigSlop.Visible = False
        fraModeTrigLevel.Visible = False
        fraModeTrigTime.Visible = False
        fraModeTrigBurst.Visible = False
        fraModeTrigDela.Visible = False

        cmdModeSoftTrig.Visible = False
    End Sub

    Public Sub optModeGated_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optModeGated.CheckedChanged
        optModeGated_Click(optModeGated.Checked)
    End Sub

    Public Sub optModeGated_Click(ByVal Value As Boolean)
        SetCur(INIT_CONT) = "OFF"
        SendCommand(INIT_CONT)
        SetCur(TRIG_BURS) = "OFF"
        SendCommand(TRIG_BURS)
        SetCur(TRIG_GATE) = "ON"
        SendCommand(TRIG_GATE)
        cboModeTrigSour.Text = "External"
        fraModeTrigSour.Visible = False
        fraModeTrigSlop.Visible = True
        fraModeTrigLevel.Visible = True
        fraModeTrigSlop.Text = "Active Gate Level"
        fraModeTrigTime.Visible = False
        fraModeTrigDela.Visible = False
        fraModeTrigBurst.Visible = False
        cmdModeSoftTrig.Visible = False
    End Sub

    Public Sub optModeTrig_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optModeTrig.CheckedChanged
        optModeTrig_Click(optModeTrig.Checked)
    End Sub

    Public Sub optModeTrig_Click(ByVal Value As Boolean)
        SetCur(INIT_CONT) = "OFF"
        SendCommand(INIT_CONT)
        SetCur(TRIG_BURS) = "OFF"
        SendCommand(TRIG_BURS)
        SetCur(TRIG_GATE) = "OFF"
        SendCommand(TRIG_GATE)
        fraModeTrigSour.Visible = True
        If cboModeTrigSour.Text = "External" Then
            fraModeTrigSlop.Visible = True
            fraModeTrigLevel.Visible = True
        End If
        fraModeTrigSlop.Text = "Slope"
        If cboModeTrigSour.Text = "Internal" Then
            fraModeTrigTime.Visible = True
        End If
        fraModeTrigDela.Visible = True
        fraModeTrigBurst.Visible = False

        cmdModeSoftTrig.Visible = True
    End Sub

    Public Sub optSyncSourceHalfSamp_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optSyncSourceHalfSamp.CheckedChanged
        optSyncSourceHalfSamp_Click(optSyncSourceHalfSamp.Checked)
    End Sub

    Public Sub optSyncSourceHalfSamp_Click(ByVal Value As Boolean)
        SetCur(OUTP_SYNC_SOUR) = "HCLock"
        SendCommand(OUTP_SYNC_SOUR)
    End Sub

    Public Sub optSyncSourceWave_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optSyncSourceWave.CheckedChanged
        optSyncSourceWave_Click(optSyncSourceWave.Checked)
    End Sub

    Public Sub optSyncSourceWave_Click(ByVal Value As Boolean)
        SetCur(OUTP_SYNC_SOUR) = "BIT"
        SendCommand(OUTP_SYNC_SOUR)
    End Sub

    Public Sub optTrigSourceExte_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optTrigSourceExte.CheckedChanged
        optTrigSourceExte_Click(optTrigSourceExte.Checked)
    End Sub

    Public Sub optTrigSourceExte_Click(ByVal Value As Boolean)
        SetCur(OUTP_TRIG_SOUR) = "External"
        SendCommand(OUTP_TRIG_SOUR)
        panIntTimer.Visible = False
        txtIntTimer.Visible = False
    End Sub

    Public Sub optTrigSourceTimer_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optTrigSourceTimer.CheckedChanged
        optTrigSourceTimer_Click(optTrigSourceTimer.Checked)
    End Sub

    Public Sub optTrigSourceTimer_Click(ByVal Value As Boolean)
        SetCur(OUTP_TRIG_SOUR) = "Internal"
        SendCommand(OUTP_TRIG_SOUR)
        panIntTimer.Visible = True
        txtIntTimer.Visible = True
    End Sub

    Public Sub optTrigSourceWave_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optTrigSourceWave.CheckedChanged
        optTrigSourceWave_Click(optTrigSourceWave.Checked)
    End Sub

    Public Sub optTrigSourceWave_Click(ByVal Value As Boolean)
        SetCur(OUTP_TRIG_SOUR) = "BIT"
        SendCommand(OUTP_TRIG_SOUR)
        panIntTimer.Visible = False
        txtIntTimer.Visible = False
    End Sub

    Private Sub panIntTimer_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles panIntTimer.MouseMove
        ShowVals(TRIG_TIM)
    End Sub

    Private Sub spnAmpl_SpinDown(sender As Object, e As EventArgs) Handles spnAmpl.DownButtonClicked
        If tolFunc.Buttons(WaveIdx - 1).Name = "btnDC" Then
            SpinAbs(txtAmpl, "Down", DC, 0.5)
        Else
            SpinAbs(txtAmpl, "Down", VOLT, 0.5)
            AdjustVoltOffs()
        End If
    End Sub

    Private Sub spnAmpl_SpinUp(sender As Object, e As EventArgs) Handles spnAmpl.UpButtonClicked
        If tolFunc.Buttons(WaveIdx - 1).Name = "btnDC" Then
            SpinAbs(txtAmpl, "Up", DC, 0.5)
        Else
            SpinAbs(txtAmpl, "Up", VOLT, 0.5)
            AdjustVoltOffs()
        End If
    End Sub

    Private Sub spnDcOffs_SpinDown(sender As Object, e As EventArgs) Handles spnDcOffs.DownButtonClicked
        Spin10Pct(txtDcOffs, "Down", VOLT_OFFS)
    End Sub

    Private Sub spnDcOffs_SpinUp(sender As Object, e As EventArgs) Handles spnDcOffs.UpButtonClicked
        Spin10Pct(txtDcOffs, "Up", VOLT_OFFS)
    End Sub

    Private Sub spnDela_SpinDown(sender As Object, e As EventArgs) Handles spnDela.DownButtonClicked
        Dim SetIdx As Short

        Select Case tolFunc.Buttons(WaveIdx - 1).Name
            Case "Pulse"
                SetIdx = PULS_DEL
            Case "Ramp"
                SetIdx = RAMP_DEL
        End Select
        Spin10Pct(txtDela, "Down", SetIdx)
    End Sub

    Private Sub spnDela_SpinUp(sender As Object, e As EventArgs) Handles spnDela.UpButtonClicked
        Dim SetIdx As Short

        Select Case tolFunc.Buttons(WaveIdx - 1).Name
            Case "Pulse"
                SetIdx = PULS_DEL
            Case "Ramp"
                SetIdx = RAMP_DEL
        End Select
        SetMax(SetIdx) = GetMaxTime(SetIdx)
        Spin10Pct(txtDela, "Up", SetIdx)
    End Sub

    Private Sub spnDuty_SpinDown(sender As Object, e As EventArgs) Handles spnDuty.DownButtonClicked
        SpinAbs(txtDuty, "Down", SQU_DCYC, 5)
    End Sub

    Private Sub spnDuty_SpinUp(sender As Object, e As EventArgs) Handles spnDuty.UpButtonClicked
        SpinAbs(txtDuty, "Up", SQU_DCYC, 5)
    End Sub

    Private Sub spnExpo_SpinDown(sender As Object, e As EventArgs) Handles spnExpo.DownButtonClicked
        Select Case Me.tolFunc.Buttons(WaveIdx - 1).Name
            Case "Sine"
                SpinAbs(txtExpo, "Down", SIN_POW, 1)
            Case "Triangle"
                SpinAbs(txtExpo, "Down", TRI_POW, 1)
            Case "Exponential"
                Spin10Pct(txtExpo, "Down", EXP_EXP)
            Case "Gaussian"
                Spin10Pct(txtExpo, "Down", GAUS_EXP)
        End Select
    End Sub

    Private Sub spnExpo_SpinUp(sender As Object, e As EventArgs) Handles spnExpo.UpButtonClicked
        Select Case Me.tolFunc.Buttons(WaveIdx - 1).Name
            Case "Sine"
                SpinAbs(txtExpo, "Up", SIN_POW, 1)
            Case "Triangle"
                SpinAbs(txtExpo, "Up", TRI_POW, 1)
            Case "Exponential"
                Spin10Pct(txtExpo, "Up", EXP_EXP)
            Case "Gaussian"
                Spin10Pct(txtExpo, "Up", GAUS_EXP)
        End Select
    End Sub

    Private Sub spnFreq_SpinDown(sender As Object, e As EventArgs) Handles spnFreq.DownButtonClicked
        Spin10Pct(txtFreq, "Down", FREQ)
        SetMaxTrigDelay()
    End Sub

    Private Sub spnFreq_SpinUp(sender As Object, e As EventArgs) Handles spnFreq.UpButtonClicked
        Spin10Pct(txtFreq, "Up", FREQ)
        SetMaxTrigDelay()
    End Sub

    Private Sub spnIntTimer_SpinDown(sender As Object, e As EventArgs) Handles spnIntTimer.DownButtonClicked
        Spin10Pct(txtIntTimer, "Down", TRIG_TIM)
        'txtModeTrigTime.Text = txtIntTimer.Text
    End Sub

    Private Sub spnIntTimer_SpinUp(sender As Object, e As EventArgs) Handles spnIntTimer.UpButtonClicked
        Spin10Pct(txtIntTimer, "Up", TRIG_TIM)
        'txtModeTrigTime.Text = txtIntTimer.Text
    End Sub

    Private Sub spnLead_SpinDown(sender As Object, e As EventArgs) Handles spnLead.DownButtonClicked
        Dim SetIdx As Short

        Select Case tolFunc.Buttons(WaveIdx - 1).Name
            Case "Pulse"
                SetIdx = PULS_TRAN
            Case "Ramp"
                SetIdx = RAMP_TRAN
        End Select
        Spin10Pct(txtLead, "Down", SetIdx)
    End Sub

    Private Sub spnLead_SpinUp(sender As Object, e As EventArgs) Handles spnLead.UpButtonClicked
        Dim SetIdx As Short

        Select Case tolFunc.Buttons(WaveIdx - 1).Name
            Case "Pulse"
                SetIdx = PULS_TRAN
            Case "Ramp"
                SetIdx = RAMP_TRAN
        End Select
        SetMax(SetIdx) = GetMaxTime(SetIdx)
        Spin10Pct(txtLead, "Up", SetIdx)
    End Sub

    Private Sub spnModeTrigBurst_SpinDown(sender As Object, e As EventArgs) Handles spnModeTrigBurst.DownButtonClicked
        Spin10Pct(txtModeTrigBurst, "Down", TRIG_COUN)
    End Sub

    Private Sub spnModeTrigBurst_SpinUp(sender As Object, e As EventArgs) Handles spnModeTrigBurst.UpButtonClicked
        Spin10Pct(txtModeTrigBurst, "Up", TRIG_COUN)
    End Sub

    Private Sub spnModeTrigDela_SpinDown(sender As Object, e As EventArgs) Handles spnModeTrigDela.DownButtonClicked
        'SetMaxTrigDelay()
        DoNotTalk = True
        Spin10Pct(txtModeTrigDela, "Down", TRIG_DEL)
        DoNotTalk = False
        'CheckTriggerDelay()
        SendCommand(TRIG_DEL)
    End Sub

    Private Sub spnModeTrigDela_SpinUp(sender As Object, e As EventArgs) Handles spnModeTrigDela.UpButtonClicked
        'SetMaxTrigDelay()
        If SetCur(TRIG_DEL) = "0" Then
            TrigDelayInClocks = 10
            SetCur(TRIG_DEL) = 9 * SampleClockTime
        End If
        DoNotTalk = True
        Spin10Pct(txtModeTrigDela, "Up", TRIG_DEL)
        DoNotTalk = False
        'CheckTriggerDelay()
        SendCommand(TRIG_DEL)
    End Sub

    Private Sub spnModeTrigLevel_SpinDown(sender As Object, e As EventArgs) Handles spnModeTrigLevel.DownButtonClicked
        Spin10Pct(txtModeTrigLevel, "Down", TRIG_LEV)
    End Sub

    Private Sub spnModeTrigLevel_SpinUp(sender As Object, e As EventArgs) Handles spnModeTrigLevel.UpButtonClicked
        Spin10Pct(txtModeTrigLevel, "Up", TRIG_LEV)
    End Sub

    Private Sub spnModeTrigTime_SpinDown(sender As Object, e As EventArgs) Handles spnModeTrigTime.DownButtonClicked
        Spin10Pct(txtModeTrigTime, "Down", TRIG_TIM)
        txtIntTimer.Text = txtModeTrigTime.Text
    End Sub

    Private Sub spnModeTrigTime_SpinUp(sender As Object, e As EventArgs) Handles spnModeTrigTime.UpButtonClicked
        Spin10Pct(txtModeTrigTime, "Up", TRIG_TIM)
        txtIntTimer.Text = txtModeTrigTime.Text
    End Sub

    Private Sub spnPhas_SpinDown(sender As Object, e As EventArgs) Handles spnPhas.DownButtonClicked
        Select Case Me.tolFunc.Buttons(WaveIdx - 1).Name
            Case "Sine"
                SpinAbs(txtPhas, "Down", SIN_PHAS, 15)
            Case "Triangle"
                SpinAbs(txtPhas, "Down", TRI_PHAS, 15)
        End Select
    End Sub

    Private Sub spnPhas_SpinUp(sender As Object, e As EventArgs) Handles spnPhas.UpButtonClicked
        Select Case Me.tolFunc.Buttons(WaveIdx - 1).Name
            Case "Sine"
                SpinAbs(txtPhas, "Up", SIN_PHAS, 15)
            Case "Triangle"
                SpinAbs(txtPhas, "Up", TRI_PHAS, 15)
        End Select
    End Sub


    Private Sub spnSinc_SpinDown(sender As Object, e As EventArgs) Handles spnSinc.DownButtonClicked
        Spin10Pct(txtSinc, "Down", SINC_NCYC)
    End Sub

    Private Sub spnSinc_SpinUp(sender As Object, e As EventArgs) Handles spnSinc.UpButtonClicked
        Spin10Pct(txtSinc, "Up", SINC_NCYC)
    End Sub

    Private Sub spnTrai_SpinDown(sender As Object, e As EventArgs) Handles spnTrai.DownButtonClicked
        Dim SetIdx As Short

        Select Case tolFunc.Buttons(WaveIdx - 1).Name
            Case "Pulse"
                SetIdx = PULS_TRAN_TRA
            Case "Ramp"
                SetIdx = RAMP_TRAN_TRA
        End Select
        Spin10Pct(txtTrai, "Down", SetIdx)
    End Sub

    Private Sub spnTrai_SpinUp(sender As Object, e As EventArgs) Handles spnTrai.UpButtonClicked
        Dim SetIdx As Short

        Select Case tolFunc.Buttons(WaveIdx - 1).Name
            Case "Pulse"
                SetIdx = PULS_TRAN_TRA
            Case "Ramp"
                SetIdx = RAMP_TRAN_TRA
        End Select
        SetMax(SetIdx) = GetMaxTime(SetIdx)
        Spin10Pct(txtTrai, "Up", SetIdx)
    End Sub

    Private Sub spnWidt_SpinDown(sender As Object, e As EventArgs) Handles spnWidt.DownButtonClicked
        Spin10Pct(txtWidt, "Down", PULS_WIDT)
    End Sub

    Private Sub spnWidt_SpinUp(sender As Object, e As EventArgs) Handles spnWidt.UpButtonClicked
        SetMax(PULS_WIDT) = GetMaxTime(PULS_WIDT)
        Spin10Pct(txtWidt, "Up", PULS_WIDT)
    End Sub

    Private Sub tabOptions_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles tabOptions.MouseMove
        HelpPanel("")
    End Sub

    Public Sub tolFunc_ButtonClick(ByVal sender As Object, ByVal e As ToolBarButtonClickEventArgs) Handles tolFunc.ButtonClick
        SetCur(FUNC_MODE) = "FIX"
        Select Case e.Button.Name
            Case "Sine"
                tolFunc_ButtonClick(Sine)

            Case "Square"
                tolFunc_ButtonClick(Square)

            Case "Pulse"
                tolFunc_ButtonClick(Pulse)

            Case "Ramp"
                tolFunc_ButtonClick(Ramp)

            Case "Triangle"
                tolFunc_ButtonClick(Triangle)

            Case "Exponential"
                tolFunc_ButtonClick(Exponential)

            Case "Gaussian"
                tolFunc_ButtonClick(Gaussian)

            Case "Sinc"
                tolFunc_ButtonClick(Sinc)

            Case "btnDC"
                tolFunc_ButtonClick(btnDC)
        End Select

        For Each Button In tolFunc.Buttons
            Button.pushed = False
        Next
        e.Button.Pushed = True
    End Sub

    Public Sub tolFunc_ButtonClick(ByVal Button As System.Windows.Forms.ToolBarButton)
        SetCur(FUNC_MODE) = "FIX"
        Select Case Button.Name
            Case "Sine"
                SetCur(FUNC_SHAPE) = "SIN"
                SetMin(FREQ) = "0.0001"
                SetMax(FREQ) = "50000000"
                txtPhas.Text = EngNotate(SetCur(SIN_PHAS), SIN_PHAS)
                txtExpo.Text = EngNotate(SetCur(SIN_POW), SIN_POW)
                txtAmpl.Text = EngNotate(SetCur(VOLT), VOLT)
                SendCommand(VOLT)
                fraFreq.Visible = True
                fraAmpl.Visible = True
                fraDcOffs.Visible = True
                fraPhas.Visible = True
                fraExpo.Visible = True
                fraSinc.Visible = False
                fraDela.Visible = False
                fraWidt.Visible = False
                fraLead.Visible = False
                fraTrai.Visible = False
                fraDuty.Visible = False
                fraOutLowPassFilt.Visible = False

            Case "Square"
                SetCur(FUNC_SHAPE) = "SQU"
                SetMin(FREQ) = "0.0001"
                SetMax(FREQ) = "50000000"
                fraFreq.Visible = True
                fraAmpl.Visible = True
                txtAmpl.Text = EngNotate(SetCur(VOLT), VOLT)
                SendCommand(VOLT)
                fraDcOffs.Visible = True
                fraPhas.Visible = False
                fraExpo.Visible = False
                fraSinc.Visible = False
                fraDela.Visible = False
                fraWidt.Visible = False
                fraLead.Visible = False
                fraTrai.Visible = False
                If Val(SetCur(FREQ)) > 10000000.0 Then
                    fraDuty.Visible = False
                    txtDuty.Text = "50 %"
                    SetCur(SQU_DCYC) = "50"
                Else
                    fraDuty.Visible = True
                End If
                fraOutLowPassFilt.Visible = True

            Case "Pulse"
                SetCur(FUNC_SHAPE) = "PULS"
                SetMin(FREQ) = "0.0001"
                SetMax(FREQ) = "1000000"
                If Val(SetCur(FREQ)) > Val(SetMax(FREQ)) Then
                    SetCur(FREQ) = Val(SetMax(FREQ))
                    txtFreq.Text = EngNotate(Val(SetMax(FREQ)), FREQ)
                    SendCommand(FREQ)
                End If
                txtAmpl.Text = EngNotate(SetCur(VOLT), VOLT)
                txtLead.Text = EngNotate(SetCur(PULS_TRAN), PULS_TRAN)
                txtTrai.Text = EngNotate(SetCur(PULS_TRAN_TRA), PULS_TRAN_TRA)
                SendCommand(VOLT)
                fraFreq.Visible = True
                fraAmpl.Visible = True
                fraDcOffs.Visible = True
                fraPhas.Visible = False
                fraExpo.Visible = False
                fraSinc.Visible = False
                fraDela.Visible = True
                txtDela.Text = EngNotate(SetCur(PULS_DEL), PULS_DEL)
                fraWidt.Visible = True
                fraLead.Visible = True
                fraTrai.Visible = True
                fraDuty.Visible = False
                fraOutLowPassFilt.Visible = True

            Case "Ramp"
                SetCur(FUNC_SHAPE) = "RAMP"
                SetMin(FREQ) = "0.0001"
                SetMax(FREQ) = "1000000"
                If Val(SetCur(FREQ)) > Val(SetMax(FREQ)) Then
                    SetCur(FREQ) = Val(SetMax(FREQ))
                    txtFreq.Text = EngNotate(Val(SetMax(FREQ)), FREQ)
                    SendCommand(FREQ)
                End If
                txtAmpl.Text = EngNotate(SetCur(VOLT), VOLT)
                txtLead.Text = EngNotate(SetCur(RAMP_TRAN), RAMP_TRAN)
                txtTrai.Text = EngNotate(SetCur(RAMP_TRAN_TRA), RAMP_TRAN_TRA)
                SendCommand(VOLT)
                fraFreq.Visible = True
                fraAmpl.Visible = True
                fraDcOffs.Visible = True
                fraPhas.Visible = False
                fraExpo.Visible = False
                fraSinc.Visible = False
                fraDela.Visible = True
                txtDela.Text = EngNotate(SetCur(RAMP_DEL), RAMP_DEL)
                fraWidt.Visible = False
                fraLead.Visible = True
                fraTrai.Visible = True
                fraDuty.Visible = False
                fraOutLowPassFilt.Visible = True

            Case "Triangle"
                SetCur(FUNC_SHAPE) = "TRI"
                SetMin(FREQ) = "0.0001"
                SetMax(FREQ) = "1000000"
                If Val(SetCur(FREQ)) > Val(SetMax(FREQ)) Then
                    SetCur(FREQ) = Val(SetMax(FREQ))
                    txtFreq.Text = EngNotate(Val(SetMax(FREQ)), FREQ)
                    SendCommand(FREQ)
                End If
                txtAmpl.Text = EngNotate(SetCur(VOLT), VOLT)
                SendCommand(VOLT)
                txtPhas.Text = EngNotate(SetCur(TRI_PHAS), TRI_PHAS)
                txtExpo.Text = EngNotate(SetCur(TRI_POW), TRI_POW)
                fraFreq.Visible = True
                fraAmpl.Visible = True
                fraDcOffs.Visible = True
                fraPhas.Visible = True
                fraExpo.Visible = True
                fraSinc.Visible = False
                fraDela.Visible = False
                fraWidt.Visible = False
                fraLead.Visible = False
                fraTrai.Visible = False
                fraDuty.Visible = False
                fraOutLowPassFilt.Visible = True

            Case "Exponential"
                SetCur(FUNC_SHAPE) = "EXP"
                SetMin(FREQ) = "0.0001"
                SetMax(FREQ) = "1000000"
                If Val(SetCur(FREQ)) > Val(SetMax(FREQ)) Then
                    SetCur(FREQ) = Val(SetMax(FREQ))
                    txtFreq.Text = EngNotate(Val(SetMax(FREQ)), FREQ)
                    SendCommand(FREQ)
                End If
                txtExpo.Text = EngNotate(SetCur(EXP_EXP), EXP_EXP)
                txtAmpl.Text = EngNotate(SetCur(VOLT), VOLT)
                SendCommand(VOLT)
                fraFreq.Visible = True
                fraAmpl.Visible = True
                fraDcOffs.Visible = True
                fraPhas.Visible = False
                fraExpo.Visible = True
                fraSinc.Visible = False
                fraDela.Visible = False
                fraWidt.Visible = False
                fraLead.Visible = False
                fraTrai.Visible = False
                fraDuty.Visible = False
                fraOutLowPassFilt.Visible = True

            Case "Gaussian"
                SetCur(FUNC_SHAPE) = "GAUS"
                SetMin(FREQ) = "0.0001"
                SetMax(FREQ) = "1000000"
                If Val(SetCur(FREQ)) > Val(SetMax(FREQ)) Then
                    SetCur(FREQ) = Val(SetMax(FREQ))
                    txtFreq.Text = EngNotate(Val(SetMax(FREQ)), FREQ)
                    SendCommand(FREQ)
                End If
                txtAmpl.Text = EngNotate(SetCur(VOLT), VOLT)
                SendCommand(VOLT)
                txtExpo.Text = EngNotate(SetCur(GAUS_EXP), GAUS_EXP)
                fraFreq.Visible = True
                fraAmpl.Visible = True
                fraDcOffs.Visible = True
                fraPhas.Visible = False
                fraExpo.Visible = True
                fraSinc.Visible = False
                fraDela.Visible = False
                fraWidt.Visible = False
                fraLead.Visible = False
                fraTrai.Visible = False
                fraDuty.Visible = False
                fraOutLowPassFilt.Visible = True

            Case "Sinc"
                SetCur(FUNC_SHAPE) = "SINC"
                SetMin(FREQ) = "0.0001"
                SetMax(FREQ) = "1000000"
                If Val(SetCur(FREQ)) > Val(SetMax(FREQ)) Then
                    SetCur(FREQ) = Val(SetMax(FREQ))
                    txtFreq.Text = EngNotate(Val(SetMax(FREQ)), FREQ)
                    SendCommand(FREQ)
                End If
                txtAmpl.Text = EngNotate(SetCur(VOLT), VOLT)
                SendCommand(VOLT)
                fraFreq.Visible = True
                fraAmpl.Visible = True
                fraDcOffs.Visible = True
                fraPhas.Visible = False
                fraExpo.Visible = False
                fraSinc.Visible = True
                fraDela.Visible = False
                fraWidt.Visible = False
                fraLead.Visible = False
                fraTrai.Visible = False
                fraDuty.Visible = False
                fraOutLowPassFilt.Visible = True

            Case "btnDC"
                SetCur(FUNC_SHAPE) = "DC"
                fraFreq.Visible = False
                fraAmpl.Visible = True
                fraDcOffs.Visible = False
                SetCur(VOLT_OFFS) = "0"
                txtDcOffs.Text = EngNotate(0, VOLT_OFFS)
                SendCommand(VOLT_OFFS)
                txtAmpl.Text = EngNotate(SetCur(DC), DC)
                SendCommand(DC)
                fraPhas.Visible = False
                fraExpo.Visible = False
                fraSinc.Visible = False
                fraDela.Visible = False
                fraWidt.Visible = False
                fraLead.Visible = False
                fraTrai.Visible = False
                fraDuty.Visible = False
                fraOutLowPassFilt.Visible = False
        End Select

        If Button.Name = "btnDC" Then
            tabOptions.TabPages.Item(TAB_TRIGGERS).Enabled = False
            tabOptions.TabPages.Item(TAB_MARKERS).Enabled = False
        Else
            tabOptions.TabPages.Item(TAB_TRIGGERS).Enabled = True
            tabOptions.TabPages.Item(TAB_MARKERS).Enabled = True
        End If

        WaveIdx = tolFunc.Buttons.IndexOf(Button) + 1
        If IsOn = True Then
            imgFGDisplay.Image = pcpFGDisplay.Images(WaveIdx)
        End If
        SendCommand(FUNC_SHAPE)

        SetMaxTrigDelay()
    End Sub

    Private Sub txtAmpl_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAmpl.Enter
        UserEnteringData = True
        txtAmpl.SelectionStart = 0
        txtAmpl.SelectionLength = txtAmpl.Text.Length
    End Sub

    Public Sub txtAmpl_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAmpl.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)

        txtAmpl_KeyPress(KeyAscii)
        e.KeyChar = Chr(KeyAscii)
    End Sub

    Public Sub txtAmpl_KeyPress(ByRef KeyAscii As Short)
        If KeyAscii <> Keys.Return Then Exit Sub
        KeyAscii = 0
        If tolFunc.Buttons(WaveIdx - 1).Name = "btnDC" Then
            FormatEntry(txtAmpl, DC)
        Else
            FormatEntry(txtAmpl, VOLT)
            AdjustVoltOffs()
        End If
    End Sub

    Private Sub txtAmpl_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAmpl.Leave
        txtAmpl_KeyPress(Keys.Return)
    End Sub

    Private Sub txtDcOffs_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDcOffs.Enter
        UserEnteringData = True
        txtDcOffs.SelectionStart = 0
        txtDcOffs.SelectionLength = txtDcOffs.Text.Length
    End Sub

    Public Sub txtDcOffs_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDcOffs.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)

        txtDcOffs_KeyPress(KeyAscii)

        e.KeyChar = Chr(KeyAscii)
    End Sub

    Public Sub txtDcOffs_KeyPress(ByRef KeyAscii As Short)
        If KeyAscii <> Keys.Return Then Exit Sub
        KeyAscii = 0
        FormatEntry(txtDcOffs, VOLT_OFFS)
    End Sub

    Private Sub txtDcOffs_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDcOffs.Leave
        txtDcOffs_KeyPress(Keys.Return)
    End Sub

    'Private Sub txtDela_Change()
    '    Dim SetIdx%
    '    Select Case tolFunc.Buttons(WaveIdx%).Key
    '       Case "Pulse": SetIdx% = PULS_DEL
    '       Case "Ramp": SetIdx% = RAMP_DEL
    '       Case Else: Exit Sub
    '   End Select

    '   TextChange Val(txtDela.Text), SetIdx%
    'End Sub

    Private Sub txtDela_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDela.Enter
        UserEnteringData = True
        txtDela.SelectionStart = 0
        txtDela.SelectionLength = txtDela.Text.Length
    End Sub

    Public Sub txtDela_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDela.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)

        txtDela_KeyPress(KeyAscii)

        e.KeyChar = Chr(KeyAscii)
    End Sub

    Public Sub txtDela_KeyPress(ByRef KeyAscii As Short)
        Dim SetIdx As Short

        If KeyAscii <> Keys.Return Then Exit Sub
        KeyAscii = 0
        Select Case tolFunc.Buttons(WaveIdx - 1).Name
            Case "Pulse"
                SetIdx = PULS_DEL
            Case "Ramp"
                SetIdx = RAMP_DEL

            Case Else
                Exit Sub
        End Select
        SetMax(SetIdx) = GetMaxTime(SetIdx)
        FormatEntry(txtDela, SetIdx)
    End Sub

    Private Sub txtDela_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDela.Leave
        txtDela_KeyPress(Keys.Return)
    End Sub

    Private Sub txtDuty_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDuty.TextChanged
        If Not sender.Created() Then Exit Sub
        TextChange(Val(txtDuty.Text), SQU_DCYC)
    End Sub

    Private Sub txtDuty_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDuty.Enter
        UserEnteringData = True
        txtDuty.SelectionStart = 0
        txtDuty.SelectionLength = txtDuty.Text.Length
    End Sub

    Public Sub txtDuty_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDuty.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)

        txtDuty_KeyPress(KeyAscii)
        e.KeyChar = Chr(KeyAscii)
    End Sub

    Public Sub txtDuty_KeyPress(ByRef KeyAscii As Short)
        If KeyAscii <> Keys.Return Then Exit Sub
        KeyAscii = 0
        FormatEntry(txtDuty, SQU_DCYC)
    End Sub

    Private Sub txtDuty_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDuty.Leave
        txtDuty_KeyPress(Keys.Return)
    End Sub

    Private Sub txtExpo_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtExpo.Enter
        UserEnteringData = True
        txtExpo.SelectionStart = 0
        txtExpo.SelectionLength = txtExpo.Text.Length
    End Sub

    Public Sub txtExpo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtExpo.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)

        txtExpo_KeyPress(KeyAscii)
        e.KeyChar = Chr(KeyAscii)
    End Sub

    Public Sub txtExpo_KeyPress(ByRef KeyAscii As Short)
        If KeyAscii <> Keys.Return Then Exit Sub
        KeyAscii = 0
        Select Case Me.tolFunc.Buttons(WaveIdx - 1).Name
            Case "Sine"
                FormatEntry(txtExpo, SIN_POW)
            Case "Triangle"
                FormatEntry(txtExpo, TRI_POW)
            Case "Exponential"
                FormatEntry(txtExpo, EXP_EXP)
            Case "Gaussian"
                FormatEntry(txtExpo, GAUS_EXP)
        End Select
    End Sub

    Private Sub txtExpo_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtExpo.Leave
        txtExpo_KeyPress(Keys.Return)
    End Sub

    Private Sub txtFreq_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFreq.Enter
        UserEnteringData = True
        txtFreq.SelectionStart = 0
        txtFreq.SelectionLength = txtFreq.Text.Length
    End Sub

    Public Sub txtFreq_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFreq.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)

        txtFreq_KeyPress(KeyAscii)
        e.KeyChar = Chr(KeyAscii)
    End Sub

    Public Sub txtFreq_KeyPress(ByRef KeyAscii As Short)
        If KeyAscii <> Keys.Return Then Exit Sub
        KeyAscii = 0
        FormatEntry(txtFreq, FREQ)
        SetMaxTrigDelay()
    End Sub

    Private Sub txtFreq_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFreq.Leave
        txtFreq_KeyPress(Keys.Return)
    End Sub

    Private Sub txtIntTimer_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtIntTimer.TextChanged
        'If Not sender.Created() Then Exit Sub
        'TextChange(Val(txtIntTimer.Text), TRIG_TIM)
    End Sub

    Private Sub txtIntTimer_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtIntTimer.Enter
        UserEnteringData = True
        txtIntTimer.SelectionStart = 0
        txtIntTimer.SelectionLength = txtIntTimer.Text.Length
    End Sub

    Public Sub txtIntTimer_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtIntTimer.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)

        txtIntTimer_KeyPress(KeyAscii)
        e.KeyChar = Chr(KeyAscii)
    End Sub

    Public Sub txtIntTimer_KeyPress(ByRef KeyAscii As Short)
        If KeyAscii <> Keys.Return Then Exit Sub
        KeyAscii = 0
        FormatEntry(txtIntTimer, TRIG_TIM)
        txtModeTrigTime.Text = txtIntTimer.Text
    End Sub

    Private Sub txtIntTimer_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtIntTimer.Leave
        txtIntTimer_KeyPress(Keys.Return)
    End Sub

    'Private Sub txtLead_Change()
    '    Dim SetIdx%
    '   Select Case tolFunc.Buttons(WaveIdx%).Key
    '       Case "Pulse": SetIdx% = PULS_TRAN
    '      Case "Ramp": SetIdx% = RAMP_TRAN
    '      Case Else: Exit Sub
    '  End Select

    '  TextChange Val(txtLead.Text), SetIdx%
    'End Sub

    Private Sub txtLead_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtLead.Enter
        UserEnteringData = True
        txtLead.SelectionStart = 0
        txtLead.SelectionLength = txtLead.Text.Length
    End Sub

    Public Sub txtLead_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtLead.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)

        txtLead_KeyPress(KeyAscii)
        e.KeyChar = Chr(KeyAscii)
    End Sub

    Public Sub txtLead_KeyPress(ByRef KeyAscii As Short)
        Dim SetIdx As Short

        If KeyAscii <> Keys.Return Then Exit Sub
        KeyAscii = 0
        Select Case tolFunc.Buttons(WaveIdx - 1).Name
            Case "Pulse"
                SetIdx = PULS_TRAN
            Case "Ramp"
                SetIdx = RAMP_TRAN

            Case Else
                Exit Sub
        End Select
        SetMax(SetIdx) = GetMaxTime(SetIdx)
        FormatEntry(txtLead, SetIdx)
    End Sub

    Private Sub txtLead_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtLead.Leave
        txtLead_KeyPress(Keys.Return)
    End Sub

    Private Sub txtModeTrigBurst_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtModeTrigBurst.TextChanged
        'If Not sender.Created() Then Exit Sub
        'TextChange(Val(txtModeTrigBurst.Text), TRIG_COUN)
    End Sub

    Private Sub txtModeTrigBurst_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtModeTrigBurst.Enter
        UserEnteringData = True
        txtModeTrigBurst.SelectionStart = 0
        txtModeTrigBurst.SelectionLength = txtModeTrigBurst.Text.Length
    End Sub

    Public Sub txtModeTrigBurst_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtModeTrigBurst.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)

        txtModeTrigBurst_KeyPress(KeyAscii)
        e.KeyChar = Chr(KeyAscii)
    End Sub

    Public Sub txtModeTrigBurst_KeyPress(ByRef KeyAscii As Short)
        If KeyAscii <> Keys.Return Then Exit Sub
        KeyAscii = 0
        FormatEntry(txtModeTrigBurst, TRIG_COUN)
    End Sub

    Private Sub txtModeTrigBurst_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtModeTrigBurst.Leave
        txtModeTrigBurst_KeyPress(Keys.Return)
        TextChange(Val(txtModeTrigDela.Text), TRIG_DEL)
    End Sub

    'Private Sub txtModeTrigDela_Change()
    '    TextChange Val(txtModeTrigDela.Text), TRIG_DEL
    'End Sub

    Private Sub txtModeTrigDela_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtModeTrigDela.Enter
        UserEnteringData = True
        txtModeTrigDela.SelectionStart = 0
        txtModeTrigDela.SelectionLength = txtModeTrigDela.Text.Length
    End Sub

    Public Sub txtModeTrigDela_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtModeTrigDela.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)

        txtModeTrigDela_KeyPress(KeyAscii)
        e.KeyChar = Chr(KeyAscii)
    End Sub

    Public Sub txtModeTrigDela_KeyPress(ByRef KeyAscii As Short)
        If KeyAscii <> Keys.Return Then Exit Sub
        KeyAscii = 0
        SetMaxTrigDelay()
        DoNotTalk = True
        FormatEntry(txtModeTrigDela, TRIG_DEL)
        DoNotTalk = False
        CheckTriggerDelay()
        SendCommand(TRIG_DEL)
    End Sub

    Private Sub txtModeTrigDela_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtModeTrigDela.Leave
        txtModeTrigDela_KeyPress(Keys.Return)
    End Sub

    'Private Sub txtModeTrigLevel_Change()
    ' TextChange Val(txtModeTrigLevel.Text), TRIG_LEV
    'End Sub

    Private Sub txtModeTrigLevel_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtModeTrigLevel.Enter
        UserEnteringData = True
        txtModeTrigLevel.SelectionStart = 0
        txtModeTrigLevel.SelectionLength = txtModeTrigLevel.Text.Length
    End Sub

    Public Sub txtModeTrigLevel_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtModeTrigLevel.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)

        txtModeTrigLevel_KeyPress(KeyAscii)
        e.KeyChar = Chr(KeyAscii)
    End Sub

    Public Sub txtModeTrigLevel_KeyPress(ByRef KeyAscii As Short)
        If KeyAscii <> Keys.Return Then Exit Sub
        KeyAscii = 0
        FormatEntry(txtModeTrigLevel, TRIG_LEV)
    End Sub

    Private Sub txtModeTrigLevel_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtModeTrigLevel.Leave
        txtModeTrigLevel_KeyPress(Keys.Return)
        TextChange(Val(txtModeTrigLevel.Text), TRIG_LEV)
    End Sub

    Private Sub txtModeTrigTime_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtModeTrigTime.Enter
        UserEnteringData = True
        txtModeTrigTime.SelectionStart = 0
        txtModeTrigTime.SelectionLength = txtModeTrigTime.Text.Length
    End Sub

    Public Sub txtModeTrigTime_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtModeTrigTime.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)

        txtModeTrigTime_KeyPress(KeyAscii)
        e.KeyChar = Chr(KeyAscii)
    End Sub

    Public Sub txtModeTrigTime_KeyPress(ByRef KeyAscii As Short)
        If KeyAscii <> Keys.Return Then Exit Sub
        KeyAscii = 0
        FormatEntry(txtModeTrigTime, TRIG_TIM)
        txtIntTimer.Text = txtModeTrigTime.Text
    End Sub

    Private Sub txtModeTrigTime_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtModeTrigTime.Leave
        txtModeTrigTime_KeyPress(Keys.Return)
    End Sub

    Private Sub txtPhas_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPhas.TextChanged
        If Not sender.Created() Then Exit Sub
        Dim SetIdx As Short
        Select Case tolFunc.Buttons(WaveIdx - 1).Name
            Case "Pulse"
                SetIdx = SIN_PHAS
            Case "Ramp"
                SetIdx = TRI_PHAS

            Case Else
                Exit Sub
        End Select

        TextChange(Val(txtPhas.Text), SetIdx)
    End Sub

    Private Sub txtPhas_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPhas.Enter
        UserEnteringData = True
        txtPhas.SelectionStart = 0
        txtPhas.SelectionLength = txtPhas.Text.Length
    End Sub

    Public Sub txtPhas_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPhas.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)

        txtPhas_KeyPress(KeyAscii)
        e.KeyChar = Chr(KeyAscii)
    End Sub

    Public Sub txtPhas_KeyPress(ByRef KeyAscii As Short)
        If KeyAscii <> Keys.Return Then Exit Sub
        KeyAscii = 0
        Select Case Me.tolFunc.Buttons(WaveIdx - 1).Name
            Case "Sine"
                FormatEntry(txtPhas, SIN_PHAS)
            Case "Triangle"
                FormatEntry(txtPhas, TRI_PHAS)
        End Select
    End Sub

    Private Sub txtPhas_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPhas.Leave
        txtPhas_KeyPress(Keys.Return)
    End Sub

    Private Sub txtSinc_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSinc.TextChanged
        If Not sender.Created() Then Exit Sub
        TextChange(Val(txtSinc.Text), SINC_NCYC)
    End Sub

    Private Sub txtSinc_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSinc.Enter
        UserEnteringData = True
        txtSinc.SelectionStart = 0
        txtSinc.SelectionLength = txtSinc.Text.Length
    End Sub

    Public Sub txtSinc_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSinc.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)

        txtSinc_KeyPress(KeyAscii)
        e.KeyChar = Chr(KeyAscii)
    End Sub

    Public Sub txtSinc_KeyPress(ByRef KeyAscii As Short)
        If KeyAscii <> Keys.Return Then Exit Sub
        KeyAscii = 0
        FormatEntry(txtSinc, SINC_NCYC)
    End Sub

    Private Sub txtSinc_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSinc.Leave
        txtSinc_KeyPress(Keys.Return)
    End Sub

    'Private Sub txtTrai_Change()
    '   Dim SetIdx%
    '   Select Case tolFunc.Buttons(WaveIdx%).Key
    '       Case "Pulse": SetIdx% = PULS_TRAN_TRA
    '       Case "Ramp": SetIdx% = RAMP_TRAN_TRA
    '       Case Else: Exit Sub
    '   End Select

    '   TextChange Val(txtTrai.Text), SetIdx%
    'End Sub

    Private Sub txtTrai_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTrai.Enter
        UserEnteringData = True
        txtTrai.SelectionStart = 0
        txtTrai.SelectionLength = txtTrai.Text.Length
    End Sub

    Public Sub txtTrai_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTrai.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)

        txtTrai_KeyPress(KeyAscii)
        e.KeyChar = Chr(KeyAscii)
    End Sub

    Public Sub txtTrai_KeyPress(ByRef KeyAscii As Short)
        Dim SetIdx As Short

        If KeyAscii <> Keys.Return Then Exit Sub
        KeyAscii = 0
        Select Case tolFunc.Buttons(WaveIdx - 1).Name
            Case "Pulse"
                SetIdx = PULS_TRAN_TRA
            Case "Ramp"
                SetIdx = RAMP_TRAN_TRA

            Case Else
                Exit Sub
        End Select
        SetMax(SetIdx) = GetMaxTime(SetIdx)
        FormatEntry(txtTrai, SetIdx)
    End Sub

    Private Sub txtTrai_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTrai.Leave
        txtTrai_KeyPress(Keys.Return)
    End Sub

    'mh Private Sub txtWidt_Change()
    '  SetCur$(PULS_WIDT) = Val(txtWidt.Text)

    '  TextChange txtWidt.Text, PULS_WIDT
    'End Sub

    Private Sub txtWidt_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtWidt.Enter
        UserEnteringData = True
        txtWidt.SelectionStart = 0
        txtWidt.SelectionLength = txtWidt.Text.Length
    End Sub

    Public Sub txtWidt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtWidt.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)

        txtWidt_KeyPress(KeyAscii)
        e.KeyChar = Chr(KeyAscii)
    End Sub

    Public Sub txtWidt_KeyPress(ByRef KeyAscii As Short)
        If KeyAscii <> Keys.Return Then Exit Sub
        KeyAscii = 0
        SetMax(PULS_WIDT) = GetMaxTime(PULS_WIDT)
        FormatEntry(txtWidt, PULS_WIDT)
    End Sub

    Private Sub txtWidt_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtWidt.Leave
        txtWidt_KeyPress(Keys.Return)
    End Sub

    Private Sub btnOn_Click(sender As Object, e As EventArgs) Handles btnOn.Click
        If IsOn Then
            btnOn.Text = "On"
            btnOn.Refresh()
            IsOn = False
            cmdOff_Click()
        Else
            btnOn.Text = "Off"
            btnOn.Refresh()
            IsOn = True
            cmdOn_Click()
        End If
    End Sub

    Public Sub cmdOff_Click()
        imgFGDisplay.Image = pcpFGDisplay.Images(0)
        SetCur(OUTP) = "OFF"
        SendCommand(OUTP)
    End Sub

    Public Sub cmdOn_Click()
        imgFGDisplay.Image = pcpFGDisplay.Images(WaveIdx)
        SetCur(OUTP) = "ON"
        SendCommand(OUTP)
    End Sub
End Class