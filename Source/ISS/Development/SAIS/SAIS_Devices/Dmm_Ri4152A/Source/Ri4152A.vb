
Imports System.Windows.Forms
Imports Microsoft.VisualBasic

Public Class frmRi4152A
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
    Friend WithEvents tabUserOptions As System.Windows.Forms.TabControl
    Friend WithEvents tabUserOptions_Page1 As System.Windows.Forms.TabPage
    Friend WithEvents chkContinuous As System.Windows.Forms.CheckBox
    Friend WithEvents fraMath As System.Windows.Forms.GroupBox
    Friend WithEvents cboMath As System.Windows.Forms.ComboBox
    Friend WithEvents panMath As System.Windows.Forms.Panel
    Friend WithEvents txtMath As System.Windows.Forms.TextBox
    Friend WithEvents spnMath As NationalInstruments.UI.WindowsForms.NumericEdit
    Friend WithEvents panMathFunction As System.Windows.Forms.Panel
    Friend WithEvents fraRange As System.Windows.Forms.GroupBox
    Friend WithEvents cboRange As System.Windows.Forms.ComboBox
    Friend WithEvents cmdMeasure As System.Windows.Forms.Button
    Friend WithEvents fraFunction As System.Windows.Forms.GroupBox
    Friend WithEvents tbrDmmFunctions As System.Windows.Forms.ToolBar
    Friend WithEvents DCV As System.Windows.Forms.ToolBarButton
    Friend WithEvents ACV As System.Windows.Forms.ToolBarButton
    Friend WithEvents DCVR As System.Windows.Forms.ToolBarButton
    Friend WithEvents DCC As System.Windows.Forms.ToolBarButton
    Friend WithEvents ACC As System.Windows.Forms.ToolBarButton
    Friend WithEvents _2WR As System.Windows.Forms.ToolBarButton
    Friend WithEvents FREQ As System.Windows.Forms.ToolBarButton
    Friend WithEvents PER As System.Windows.Forms.ToolBarButton
    Friend WithEvents _4WR As System.Windows.Forms.ToolBarButton
    Friend WithEvents tabUserOptions_Page2 As System.Windows.Forms.TabPage
    Friend WithEvents fraAcFilter As System.Windows.Forms.GroupBox
    Friend WithEvents optAcFilterFast As System.Windows.Forms.RadioButton
    Friend WithEvents optAcFilterMedium As System.Windows.Forms.RadioButton
    Friend WithEvents optAcFilterSlow As System.Windows.Forms.RadioButton
    Friend WithEvents fraAutoZero As System.Windows.Forms.GroupBox
    Friend WithEvents optAutoZeroOn As System.Windows.Forms.RadioButton
    Friend WithEvents optAutoZeroOff As System.Windows.Forms.RadioButton
    Friend WithEvents optAutoZeroOnce As System.Windows.Forms.RadioButton
    Friend WithEvents fraInputResistance As System.Windows.Forms.GroupBox
    Friend WithEvents optInputResistanceAuto As System.Windows.Forms.RadioButton
    Friend WithEvents optInputResistance10MOhm As System.Windows.Forms.RadioButton
    Friend WithEvents fraTrigger As System.Windows.Forms.GroupBox
    Friend WithEvents cboTrigDelayUnits As System.Windows.Forms.ComboBox
    Friend WithEvents panTriggerDelay As System.Windows.Forms.Panel
    Friend WithEvents txtTriggerDelay As System.Windows.Forms.TextBox
    Friend WithEvents spnTriggerDelay As NationalInstruments.UI.WindowsForms.NumericEdit
    Friend WithEvents chkDelay As System.Windows.Forms.CheckBox
    Friend WithEvents fraTriggerCount As System.Windows.Forms.GroupBox
    Friend WithEvents chkTriggerCount As System.Windows.Forms.CheckBox
    Friend WithEvents panTriggerCount As System.Windows.Forms.Panel
    Friend WithEvents txtTriggerCount As System.Windows.Forms.TextBox
    Friend WithEvents spnTriggerCount As NationalInstruments.UI.WindowsForms.NumericEdit
    Friend WithEvents fraSampleCount As System.Windows.Forms.GroupBox
    Friend WithEvents panSampleCount As System.Windows.Forms.Panel
    Friend WithEvents txtSampleCount As System.Windows.Forms.TextBox
    Friend WithEvents spnSampleCount As NationalInstruments.UI.WindowsForms.NumericEdit
    Friend WithEvents tabUserOptions_Page3 As System.Windows.Forms.TabPage
    Friend WithEvents fraTrigOut As System.Windows.Forms.GroupBox
    Friend WithEvents cboTriggerOutput As System.Windows.Forms.ComboBox
    Friend WithEvents optTriggerOutputOff As System.Windows.Forms.RadioButton
    Friend WithEvents optTriggerOutputOn As System.Windows.Forms.RadioButton
    Friend WithEvents fraSampleTimePLCycles As System.Windows.Forms.GroupBox
    Friend WithEvents cboNPLC As System.Windows.Forms.ComboBox
    Friend WithEvents fraSampleTimeAperture As System.Windows.Forms.GroupBox
    Friend WithEvents cboAperture As System.Windows.Forms.ComboBox
    Friend WithEvents fraModeTrigParm As System.Windows.Forms.GroupBox
    Friend WithEvents cboTriggerSource As System.Windows.Forms.ComboBox
    Friend WithEvents cmdSelfTest As System.Windows.Forms.Button
    Friend WithEvents cmdAbout As System.Windows.Forms.Button
    Friend WithEvents cmdUpdateTIP As System.Windows.Forms.Button
    Friend WithEvents Panel_Conifg As VIPERT_Common_Controls.Panel_Conifg
    Friend WithEvents tabUserOptions_Page4 As System.Windows.Forms.TabPage
    Friend WithEvents Atlas_SFP As VIPERT_Common_Controls.Atlas_SFP
    Friend WithEvents cmdReset As System.Windows.Forms.Button
    Friend WithEvents cmdQuit As System.Windows.Forms.Button
    Friend WithEvents panDmmDisplay As System.Windows.Forms.Panel
    Friend WithEvents txtDmmDisplay As System.Windows.Forms.TextBox
    Friend WithEvents sbrUserInformation As System.Windows.Forms.StatusBar
    Friend WithEvents sbrUserInformation_Panel1 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents imgDmmFunctions As System.Windows.Forms.ImageList

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRi4152A))
        Me.txtMath = New System.Windows.Forms.TextBox()
        Me.txtTriggerDelay = New System.Windows.Forms.TextBox()
        Me.txtTriggerCount = New System.Windows.Forms.TextBox()
        Me.txtSampleCount = New System.Windows.Forms.TextBox()
        Me.cboTrigDelayUnits = New System.Windows.Forms.ComboBox()
        Me.cmdHelp = New System.Windows.Forms.Button()
        Me.CommonDialog1 = New System.Windows.Forms.PrintDialog()
        Me.tabUserOptions = New System.Windows.Forms.TabControl()
        Me.tabUserOptions_Page1 = New System.Windows.Forms.TabPage()
        Me.chkContinuous = New System.Windows.Forms.CheckBox()
        Me.fraMath = New System.Windows.Forms.GroupBox()
        Me.spnMath = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.cboMath = New System.Windows.Forms.ComboBox()
        Me.panMath = New System.Windows.Forms.Panel()
        Me.panMathFunction = New System.Windows.Forms.Panel()
        Me.fraRange = New System.Windows.Forms.GroupBox()
        Me.cboRange = New System.Windows.Forms.ComboBox()
        Me.cmdMeasure = New System.Windows.Forms.Button()
        Me.fraFunction = New System.Windows.Forms.GroupBox()
        Me.tbrDmmFunctions = New System.Windows.Forms.ToolBar()
        Me.DCV = New System.Windows.Forms.ToolBarButton()
        Me.ACV = New System.Windows.Forms.ToolBarButton()
        Me.DCVR = New System.Windows.Forms.ToolBarButton()
        Me.DCC = New System.Windows.Forms.ToolBarButton()
        Me.ACC = New System.Windows.Forms.ToolBarButton()
        Me._2WR = New System.Windows.Forms.ToolBarButton()
        Me.FREQ = New System.Windows.Forms.ToolBarButton()
        Me.PER = New System.Windows.Forms.ToolBarButton()
        Me._4WR = New System.Windows.Forms.ToolBarButton()
        Me.imgDmmFunctions = New System.Windows.Forms.ImageList(Me.components)
        Me.tabUserOptions_Page2 = New System.Windows.Forms.TabPage()
        Me.fraAcFilter = New System.Windows.Forms.GroupBox()
        Me.optAcFilterFast = New System.Windows.Forms.RadioButton()
        Me.optAcFilterMedium = New System.Windows.Forms.RadioButton()
        Me.optAcFilterSlow = New System.Windows.Forms.RadioButton()
        Me.fraAutoZero = New System.Windows.Forms.GroupBox()
        Me.optAutoZeroOn = New System.Windows.Forms.RadioButton()
        Me.optAutoZeroOff = New System.Windows.Forms.RadioButton()
        Me.optAutoZeroOnce = New System.Windows.Forms.RadioButton()
        Me.fraInputResistance = New System.Windows.Forms.GroupBox()
        Me.optInputResistanceAuto = New System.Windows.Forms.RadioButton()
        Me.optInputResistance10MOhm = New System.Windows.Forms.RadioButton()
        Me.fraTrigger = New System.Windows.Forms.GroupBox()
        Me.panTriggerDelay = New System.Windows.Forms.Panel()
        Me.spnTriggerDelay = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.chkDelay = New System.Windows.Forms.CheckBox()
        Me.fraTriggerCount = New System.Windows.Forms.GroupBox()
        Me.chkTriggerCount = New System.Windows.Forms.CheckBox()
        Me.panTriggerCount = New System.Windows.Forms.Panel()
        Me.spnTriggerCount = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.fraSampleCount = New System.Windows.Forms.GroupBox()
        Me.panSampleCount = New System.Windows.Forms.Panel()
        Me.spnSampleCount = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.tabUserOptions_Page3 = New System.Windows.Forms.TabPage()
        Me.fraTrigOut = New System.Windows.Forms.GroupBox()
        Me.cboTriggerOutput = New System.Windows.Forms.ComboBox()
        Me.optTriggerOutputOff = New System.Windows.Forms.RadioButton()
        Me.optTriggerOutputOn = New System.Windows.Forms.RadioButton()
        Me.fraSampleTimePLCycles = New System.Windows.Forms.GroupBox()
        Me.cboNPLC = New System.Windows.Forms.ComboBox()
        Me.fraSampleTimeAperture = New System.Windows.Forms.GroupBox()
        Me.cboAperture = New System.Windows.Forms.ComboBox()
        Me.fraModeTrigParm = New System.Windows.Forms.GroupBox()
        Me.cboTriggerSource = New System.Windows.Forms.ComboBox()
        Me.cmdSelfTest = New System.Windows.Forms.Button()
        Me.cmdAbout = New System.Windows.Forms.Button()
        Me.cmdUpdateTIP = New System.Windows.Forms.Button()
        Me.Panel_Conifg = New VIPERT_Common_Controls.Panel_Conifg()
        Me.tabUserOptions_Page4 = New System.Windows.Forms.TabPage()
        Me.Atlas_SFP = New VIPERT_Common_Controls.Atlas_SFP()
        Me.cmdReset = New System.Windows.Forms.Button()
        Me.cmdQuit = New System.Windows.Forms.Button()
        Me.panDmmDisplay = New System.Windows.Forms.Panel()
        Me.txtDmmDisplay = New System.Windows.Forms.TextBox()
        Me.sbrUserInformation = New System.Windows.Forms.StatusBar()
        Me.sbrUserInformation_Panel1 = New System.Windows.Forms.StatusBarPanel()
        Me.tabUserOptions.SuspendLayout()
        Me.tabUserOptions_Page1.SuspendLayout()
        Me.fraMath.SuspendLayout()
        CType(Me.spnMath, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panMath.SuspendLayout()
        Me.fraRange.SuspendLayout()
        Me.fraFunction.SuspendLayout()
        Me.tabUserOptions_Page2.SuspendLayout()
        Me.fraAcFilter.SuspendLayout()
        Me.fraAutoZero.SuspendLayout()
        Me.fraInputResistance.SuspendLayout()
        Me.fraTrigger.SuspendLayout()
        Me.panTriggerDelay.SuspendLayout()
        CType(Me.spnTriggerDelay, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraTriggerCount.SuspendLayout()
        Me.panTriggerCount.SuspendLayout()
        CType(Me.spnTriggerCount, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraSampleCount.SuspendLayout()
        Me.panSampleCount.SuspendLayout()
        CType(Me.spnSampleCount, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabUserOptions_Page3.SuspendLayout()
        Me.fraTrigOut.SuspendLayout()
        Me.fraSampleTimePLCycles.SuspendLayout()
        Me.fraSampleTimeAperture.SuspendLayout()
        Me.fraModeTrigParm.SuspendLayout()
        Me.tabUserOptions_Page4.SuspendLayout()
        Me.panDmmDisplay.SuspendLayout()
        CType(Me.sbrUserInformation_Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtMath
        '
        Me.txtMath.BackColor = System.Drawing.SystemColors.Window
        Me.txtMath.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtMath.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMath.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMath.Location = New System.Drawing.Point(2, 2)
        Me.txtMath.Name = "txtMath"
        Me.txtMath.Size = New System.Drawing.Size(46, 13)
        Me.txtMath.TabIndex = 32
        Me.txtMath.Text = "99.9"
        Me.txtMath.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTriggerDelay
        '
        Me.txtTriggerDelay.BackColor = System.Drawing.SystemColors.Window
        Me.txtTriggerDelay.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtTriggerDelay.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTriggerDelay.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTriggerDelay.Location = New System.Drawing.Point(2, 2)
        Me.txtTriggerDelay.Name = "txtTriggerDelay"
        Me.txtTriggerDelay.Size = New System.Drawing.Size(53, 13)
        Me.txtTriggerDelay.TabIndex = 38
        Me.txtTriggerDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTriggerCount
        '
        Me.txtTriggerCount.BackColor = System.Drawing.SystemColors.Window
        Me.txtTriggerCount.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtTriggerCount.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTriggerCount.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTriggerCount.Location = New System.Drawing.Point(2, 2)
        Me.txtTriggerCount.Name = "txtTriggerCount"
        Me.txtTriggerCount.Size = New System.Drawing.Size(65, 13)
        Me.txtTriggerCount.TabIndex = 55
        Me.txtTriggerCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtSampleCount
        '
        Me.txtSampleCount.BackColor = System.Drawing.SystemColors.Window
        Me.txtSampleCount.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSampleCount.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSampleCount.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSampleCount.Location = New System.Drawing.Point(2, 2)
        Me.txtSampleCount.Name = "txtSampleCount"
        Me.txtSampleCount.Size = New System.Drawing.Size(65, 13)
        Me.txtSampleCount.TabIndex = 59
        Me.txtSampleCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cboTrigDelayUnits
        '
        Me.cboTrigDelayUnits.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cboTrigDelayUnits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTrigDelayUnits.Enabled = False
        Me.cboTrigDelayUnits.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboTrigDelayUnits.Location = New System.Drawing.Point(96, 42)
        Me.cboTrigDelayUnits.Name = "cboTrigDelayUnits"
        Me.cboTrigDelayUnits.Size = New System.Drawing.Size(59, 21)
        Me.cboTrigDelayUnits.TabIndex = 47
        Me.cboTrigDelayUnits.Visible = False
        '
        'cmdHelp
        '
        Me.cmdHelp.BackColor = System.Drawing.SystemColors.Control
        Me.cmdHelp.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdHelp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdHelp.Location = New System.Drawing.Point(214, 307)
        Me.cmdHelp.Name = "cmdHelp"
        Me.cmdHelp.Size = New System.Drawing.Size(76, 23)
        Me.cmdHelp.TabIndex = 50
        Me.cmdHelp.Text = "Help"
        Me.cmdHelp.UseVisualStyleBackColor = False
        '
        'tabUserOptions
        '
        Me.tabUserOptions.Controls.Add(Me.tabUserOptions_Page1)
        Me.tabUserOptions.Controls.Add(Me.tabUserOptions_Page2)
        Me.tabUserOptions.Controls.Add(Me.tabUserOptions_Page3)
        Me.tabUserOptions.Controls.Add(Me.tabUserOptions_Page4)
        Me.tabUserOptions.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tabUserOptions.Location = New System.Drawing.Point(13, 51)
        Me.tabUserOptions.Name = "tabUserOptions"
        Me.tabUserOptions.SelectedIndex = 0
        Me.tabUserOptions.Size = New System.Drawing.Size(506, 245)
        Me.tabUserOptions.TabIndex = 3
        '
        'tabUserOptions_Page1
        '
        Me.tabUserOptions_Page1.Controls.Add(Me.chkContinuous)
        Me.tabUserOptions_Page1.Controls.Add(Me.fraMath)
        Me.tabUserOptions_Page1.Controls.Add(Me.fraRange)
        Me.tabUserOptions_Page1.Controls.Add(Me.cmdMeasure)
        Me.tabUserOptions_Page1.Controls.Add(Me.fraFunction)
        Me.tabUserOptions_Page1.Location = New System.Drawing.Point(4, 22)
        Me.tabUserOptions_Page1.Name = "tabUserOptions_Page1"
        Me.tabUserOptions_Page1.Size = New System.Drawing.Size(498, 219)
        Me.tabUserOptions_Page1.TabIndex = 0
        Me.tabUserOptions_Page1.Text = "Functions"
        '
        'chkContinuous
        '
        Me.chkContinuous.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkContinuous.Location = New System.Drawing.Point(169, 162)
        Me.chkContinuous.Name = "chkContinuous"
        Me.chkContinuous.Size = New System.Drawing.Size(101, 22)
        Me.chkContinuous.TabIndex = 11
        Me.chkContinuous.Text = "Continuous"
        '
        'fraMath
        '
        Me.fraMath.Controls.Add(Me.spnMath)
        Me.fraMath.Controls.Add(Me.cboMath)
        Me.fraMath.Controls.Add(Me.panMath)
        Me.fraMath.Controls.Add(Me.panMathFunction)
        Me.fraMath.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraMath.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraMath.Location = New System.Drawing.Point(138, 69)
        Me.fraMath.Name = "fraMath"
        Me.fraMath.Size = New System.Drawing.Size(139, 86)
        Me.fraMath.TabIndex = 9
        Me.fraMath.TabStop = False
        Me.fraMath.Text = "Math Function"
        '
        'spnMath
        '
        Me.spnMath.Location = New System.Drawing.Point(62, 55)
        Me.spnMath.Name = "spnMath"
        Me.spnMath.Size = New System.Drawing.Size(18, 20)
        Me.spnMath.TabIndex = 33
        '
        'cboMath
        '
        Me.cboMath.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cboMath.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMath.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboMath.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboMath.Location = New System.Drawing.Point(8, 20)
        Me.cboMath.Name = "cboMath"
        Me.cboMath.Size = New System.Drawing.Size(122, 21)
        Me.cboMath.TabIndex = 10
        '
        'panMath
        '
        Me.panMath.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.panMath.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panMath.Controls.Add(Me.txtMath)
        Me.panMath.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panMath.Location = New System.Drawing.Point(8, 55)
        Me.panMath.Name = "panMath"
        Me.panMath.Size = New System.Drawing.Size(55, 21)
        Me.panMath.TabIndex = 31
        '
        'panMathFunction
        '
        Me.panMathFunction.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panMathFunction.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panMathFunction.Location = New System.Drawing.Point(93, 53)
        Me.panMathFunction.Name = "panMathFunction"
        Me.panMathFunction.Size = New System.Drawing.Size(41, 25)
        Me.panMathFunction.TabIndex = 34
        '
        'fraRange
        '
        Me.fraRange.Controls.Add(Me.cboRange)
        Me.fraRange.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraRange.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraRange.Location = New System.Drawing.Point(138, 8)
        Me.fraRange.Name = "fraRange"
        Me.fraRange.Size = New System.Drawing.Size(139, 58)
        Me.fraRange.TabIndex = 7
        Me.fraRange.TabStop = False
        Me.fraRange.Text = "Range"
        '
        'cboRange
        '
        Me.cboRange.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cboRange.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboRange.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboRange.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboRange.Location = New System.Drawing.Point(8, 24)
        Me.cboRange.Name = "cboRange"
        Me.cboRange.Size = New System.Drawing.Size(122, 21)
        Me.cboRange.TabIndex = 8
        '
        'cmdMeasure
        '
        Me.cmdMeasure.BackColor = System.Drawing.SystemColors.Control
        Me.cmdMeasure.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdMeasure.Location = New System.Drawing.Point(177, 188)
        Me.cmdMeasure.Name = "cmdMeasure"
        Me.cmdMeasure.Size = New System.Drawing.Size(87, 23)
        Me.cmdMeasure.TabIndex = 0
        Me.cmdMeasure.Text = "&Measure"
        Me.cmdMeasure.UseVisualStyleBackColor = False
        '
        'fraFunction
        '
        Me.fraFunction.Controls.Add(Me.tbrDmmFunctions)
        Me.fraFunction.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraFunction.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraFunction.Location = New System.Drawing.Point(8, 8)
        Me.fraFunction.Name = "fraFunction"
        Me.fraFunction.Size = New System.Drawing.Size(122, 147)
        Me.fraFunction.TabIndex = 42
        Me.fraFunction.TabStop = False
        Me.fraFunction.Text = "Function"
        '
        'tbrDmmFunctions
        '
        Me.tbrDmmFunctions.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.DCV, Me.ACV, Me.DCVR, Me.DCC, Me.ACC, Me._2WR, Me.FREQ, Me.PER, Me._4WR})
        Me.tbrDmmFunctions.ButtonSize = New System.Drawing.Size(24, 24)
        Me.tbrDmmFunctions.Dock = System.Windows.Forms.DockStyle.None
        Me.tbrDmmFunctions.DropDownArrows = True
        Me.tbrDmmFunctions.ImageList = Me.imgDmmFunctions
        Me.tbrDmmFunctions.Location = New System.Drawing.Point(13, 32)
        Me.tbrDmmFunctions.Name = "tbrDmmFunctions"
        Me.tbrDmmFunctions.ShowToolTips = True
        Me.tbrDmmFunctions.Size = New System.Drawing.Size(98, 96)
        Me.tbrDmmFunctions.TabIndex = 49
        '
        'DCV
        '
        Me.DCV.ImageIndex = 0
        Me.DCV.Name = "DCV"
        Me.DCV.ToolTipText = "DC Volts"
        '
        'ACV
        '
        Me.ACV.ImageIndex = 1
        Me.ACV.Name = "ACV"
        Me.ACV.ToolTipText = "AC Volts"
        '
        'DCVR
        '
        Me.DCVR.ImageIndex = 2
        Me.DCVR.Name = "DCVR"
        Me.DCVR.ToolTipText = "DC Voltage Ratio"
        '
        'DCC
        '
        Me.DCC.ImageIndex = 3
        Me.DCC.Name = "DCC"
        Me.DCC.ToolTipText = "DC Amps"
        '
        'ACC
        '
        Me.ACC.ImageIndex = 4
        Me.ACC.Name = "ACC"
        Me.ACC.ToolTipText = "AC Amps"
        '
        '_2WR
        '
        Me._2WR.ImageIndex = 5
        Me._2WR.Name = "_2WR"
        Me._2WR.ToolTipText = "2-Wire Ohms"
        '
        'FREQ
        '
        Me.FREQ.ImageIndex = 6
        Me.FREQ.Name = "FREQ"
        Me.FREQ.ToolTipText = "Frequency"
        '
        'PER
        '
        Me.PER.ImageIndex = 7
        Me.PER.Name = "PER"
        Me.PER.ToolTipText = "Period"
        '
        '_4WR
        '
        Me._4WR.ImageIndex = 8
        Me._4WR.Name = "_4WR"
        Me._4WR.ToolTipText = "4-Wire Ohms"
        '
        'imgDmmFunctions
        '
        Me.imgDmmFunctions.ImageStream = CType(resources.GetObject("imgDmmFunctions.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgDmmFunctions.TransparentColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.imgDmmFunctions.Images.SetKeyName(0, "DCV")
        Me.imgDmmFunctions.Images.SetKeyName(1, "ACV")
        Me.imgDmmFunctions.Images.SetKeyName(2, "DCVR")
        Me.imgDmmFunctions.Images.SetKeyName(3, "dcc")
        Me.imgDmmFunctions.Images.SetKeyName(4, "ACC")
        Me.imgDmmFunctions.Images.SetKeyName(5, "2WR")
        Me.imgDmmFunctions.Images.SetKeyName(6, "FREQ")
        Me.imgDmmFunctions.Images.SetKeyName(7, "PER")
        Me.imgDmmFunctions.Images.SetKeyName(8, "4WR")
        '
        'tabUserOptions_Page2
        '
        Me.tabUserOptions_Page2.Controls.Add(Me.fraAcFilter)
        Me.tabUserOptions_Page2.Controls.Add(Me.fraAutoZero)
        Me.tabUserOptions_Page2.Controls.Add(Me.fraInputResistance)
        Me.tabUserOptions_Page2.Controls.Add(Me.fraTrigger)
        Me.tabUserOptions_Page2.Controls.Add(Me.fraTriggerCount)
        Me.tabUserOptions_Page2.Controls.Add(Me.fraSampleCount)
        Me.tabUserOptions_Page2.Location = New System.Drawing.Point(4, 22)
        Me.tabUserOptions_Page2.Name = "tabUserOptions_Page2"
        Me.tabUserOptions_Page2.Size = New System.Drawing.Size(498, 219)
        Me.tabUserOptions_Page2.TabIndex = 1
        Me.tabUserOptions_Page2.Text = "Input"
        '
        'fraAcFilter
        '
        Me.fraAcFilter.Controls.Add(Me.optAcFilterFast)
        Me.fraAcFilter.Controls.Add(Me.optAcFilterMedium)
        Me.fraAcFilter.Controls.Add(Me.optAcFilterSlow)
        Me.fraAcFilter.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraAcFilter.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraAcFilter.Location = New System.Drawing.Point(8, 85)
        Me.fraAcFilter.Name = "fraAcFilter"
        Me.fraAcFilter.Size = New System.Drawing.Size(170, 86)
        Me.fraAcFilter.TabIndex = 19
        Me.fraAcFilter.TabStop = False
        Me.fraAcFilter.Text = "AC Filter"
        '
        'optAcFilterFast
        '
        Me.optAcFilterFast.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optAcFilterFast.Location = New System.Drawing.Point(12, 18)
        Me.optAcFilterFast.Name = "optAcFilterFast"
        Me.optAcFilterFast.Size = New System.Drawing.Size(118, 19)
        Me.optAcFilterFast.TabIndex = 22
        Me.optAcFilterFast.Text = "Fast (200 Hz)"
        '
        'optAcFilterMedium
        '
        Me.optAcFilterMedium.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optAcFilterMedium.Location = New System.Drawing.Point(12, 39)
        Me.optAcFilterMedium.Name = "optAcFilterMedium"
        Me.optAcFilterMedium.Size = New System.Drawing.Size(102, 19)
        Me.optAcFilterMedium.TabIndex = 21
        Me.optAcFilterMedium.Text = "Medium (20 Hz)"
        '
        'optAcFilterSlow
        '
        Me.optAcFilterSlow.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optAcFilterSlow.Location = New System.Drawing.Point(12, 60)
        Me.optAcFilterSlow.Name = "optAcFilterSlow"
        Me.optAcFilterSlow.Size = New System.Drawing.Size(90, 19)
        Me.optAcFilterSlow.TabIndex = 20
        Me.optAcFilterSlow.Text = "Slow (3Hz)"
        '
        'fraAutoZero
        '
        Me.fraAutoZero.Controls.Add(Me.optAutoZeroOn)
        Me.fraAutoZero.Controls.Add(Me.optAutoZeroOff)
        Me.fraAutoZero.Controls.Add(Me.optAutoZeroOnce)
        Me.fraAutoZero.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraAutoZero.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraAutoZero.Location = New System.Drawing.Point(188, 85)
        Me.fraAutoZero.Name = "fraAutoZero"
        Me.fraAutoZero.Size = New System.Drawing.Size(162, 86)
        Me.fraAutoZero.TabIndex = 15
        Me.fraAutoZero.TabStop = False
        Me.fraAutoZero.Text = "Auto Zero"
        '
        'optAutoZeroOn
        '
        Me.optAutoZeroOn.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optAutoZeroOn.Location = New System.Drawing.Point(8, 18)
        Me.optAutoZeroOn.Name = "optAutoZeroOn"
        Me.optAutoZeroOn.Size = New System.Drawing.Size(50, 19)
        Me.optAutoZeroOn.TabIndex = 18
        Me.optAutoZeroOn.Text = "On"
        '
        'optAutoZeroOff
        '
        Me.optAutoZeroOff.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optAutoZeroOff.Location = New System.Drawing.Point(8, 39)
        Me.optAutoZeroOff.Name = "optAutoZeroOff"
        Me.optAutoZeroOff.Size = New System.Drawing.Size(82, 19)
        Me.optAutoZeroOff.TabIndex = 17
        Me.optAutoZeroOff.Text = "Off"
        '
        'optAutoZeroOnce
        '
        Me.optAutoZeroOnce.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optAutoZeroOnce.Location = New System.Drawing.Point(8, 60)
        Me.optAutoZeroOnce.Name = "optAutoZeroOnce"
        Me.optAutoZeroOnce.Size = New System.Drawing.Size(63, 19)
        Me.optAutoZeroOnce.TabIndex = 16
        Me.optAutoZeroOnce.Text = "Once"
        '
        'fraInputResistance
        '
        Me.fraInputResistance.Controls.Add(Me.optInputResistanceAuto)
        Me.fraInputResistance.Controls.Add(Me.optInputResistance10MOhm)
        Me.fraInputResistance.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraInputResistance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraInputResistance.Location = New System.Drawing.Point(8, 8)
        Me.fraInputResistance.Name = "fraInputResistance"
        Me.fraInputResistance.Size = New System.Drawing.Size(170, 74)
        Me.fraInputResistance.TabIndex = 12
        Me.fraInputResistance.TabStop = False
        Me.fraInputResistance.Text = "Auto Input Impedance"
        '
        'optInputResistanceAuto
        '
        Me.optInputResistanceAuto.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optInputResistanceAuto.Location = New System.Drawing.Point(12, 22)
        Me.optInputResistanceAuto.Name = "optInputResistanceAuto"
        Me.optInputResistanceAuto.Size = New System.Drawing.Size(139, 19)
        Me.optInputResistanceAuto.TabIndex = 14
        Me.optInputResistanceAuto.Text = "Auto (10 G Ohm)"
        '
        'optInputResistance10MOhm
        '
        Me.optInputResistance10MOhm.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optInputResistance10MOhm.Location = New System.Drawing.Point(12, 49)
        Me.optInputResistance10MOhm.Name = "optInputResistance10MOhm"
        Me.optInputResistance10MOhm.Size = New System.Drawing.Size(78, 19)
        Me.optInputResistance10MOhm.TabIndex = 13
        Me.optInputResistance10MOhm.Text = "10 M Ohm"
        '
        'fraTrigger
        '
        Me.fraTrigger.Controls.Add(Me.cboTrigDelayUnits)
        Me.fraTrigger.Controls.Add(Me.panTriggerDelay)
        Me.fraTrigger.Controls.Add(Me.chkDelay)
        Me.fraTrigger.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraTrigger.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraTrigger.Location = New System.Drawing.Point(188, 8)
        Me.fraTrigger.Name = "fraTrigger"
        Me.fraTrigger.Size = New System.Drawing.Size(162, 74)
        Me.fraTrigger.TabIndex = 35
        Me.fraTrigger.TabStop = False
        Me.fraTrigger.Text = "Trigger Delay"
        '
        'panTriggerDelay
        '
        Me.panTriggerDelay.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.panTriggerDelay.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panTriggerDelay.Controls.Add(Me.txtTriggerDelay)
        Me.panTriggerDelay.Controls.Add(Me.spnTriggerDelay)
        Me.panTriggerDelay.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panTriggerDelay.Location = New System.Drawing.Point(8, 42)
        Me.panTriggerDelay.Name = "panTriggerDelay"
        Me.panTriggerDelay.Size = New System.Drawing.Size(77, 21)
        Me.panTriggerDelay.TabIndex = 37
        Me.panTriggerDelay.Visible = False
        '
        'spnTriggerDelay
        '
        Me.spnTriggerDelay.Location = New System.Drawing.Point(59, -2)
        Me.spnTriggerDelay.Name = "spnTriggerDelay"
        Me.spnTriggerDelay.Size = New System.Drawing.Size(16, 20)
        Me.spnTriggerDelay.TabIndex = 39
        Me.spnTriggerDelay.Visible = False
        '
        'chkDelay
        '
        Me.chkDelay.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDelay.Location = New System.Drawing.Point(8, 22)
        Me.chkDelay.Name = "chkDelay"
        Me.chkDelay.Size = New System.Drawing.Size(93, 19)
        Me.chkDelay.TabIndex = 36
        Me.chkDelay.Text = "Automatic"
        '
        'fraTriggerCount
        '
        Me.fraTriggerCount.Controls.Add(Me.chkTriggerCount)
        Me.fraTriggerCount.Controls.Add(Me.panTriggerCount)
        Me.fraTriggerCount.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraTriggerCount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraTriggerCount.Location = New System.Drawing.Point(360, 8)
        Me.fraTriggerCount.Name = "fraTriggerCount"
        Me.fraTriggerCount.Size = New System.Drawing.Size(125, 74)
        Me.fraTriggerCount.TabIndex = 53
        Me.fraTriggerCount.TabStop = False
        Me.fraTriggerCount.Text = "Trigger Count"
        Me.fraTriggerCount.Visible = False
        '
        'chkTriggerCount
        '
        Me.chkTriggerCount.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTriggerCount.Location = New System.Drawing.Point(3, 22)
        Me.chkTriggerCount.Name = "chkTriggerCount"
        Me.chkTriggerCount.Size = New System.Drawing.Size(114, 20)
        Me.chkTriggerCount.TabIndex = 52
        Me.chkTriggerCount.Text = "Max (50,000)"
        '
        'panTriggerCount
        '
        Me.panTriggerCount.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.panTriggerCount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panTriggerCount.Controls.Add(Me.txtTriggerCount)
        Me.panTriggerCount.Controls.Add(Me.spnTriggerCount)
        Me.panTriggerCount.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panTriggerCount.Location = New System.Drawing.Point(8, 42)
        Me.panTriggerCount.Name = "panTriggerCount"
        Me.panTriggerCount.Size = New System.Drawing.Size(90, 21)
        Me.panTriggerCount.TabIndex = 54
        '
        'spnTriggerCount
        '
        Me.spnTriggerCount.Location = New System.Drawing.Point(72, -2)
        Me.spnTriggerCount.Name = "spnTriggerCount"
        Me.spnTriggerCount.Size = New System.Drawing.Size(16, 20)
        Me.spnTriggerCount.TabIndex = 56
        '
        'fraSampleCount
        '
        Me.fraSampleCount.Controls.Add(Me.panSampleCount)
        Me.fraSampleCount.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraSampleCount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraSampleCount.Location = New System.Drawing.Point(360, 85)
        Me.fraSampleCount.Name = "fraSampleCount"
        Me.fraSampleCount.Size = New System.Drawing.Size(125, 58)
        Me.fraSampleCount.TabIndex = 57
        Me.fraSampleCount.TabStop = False
        Me.fraSampleCount.Text = "Sample Count"
        '
        'panSampleCount
        '
        Me.panSampleCount.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.panSampleCount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panSampleCount.Controls.Add(Me.txtSampleCount)
        Me.panSampleCount.Controls.Add(Me.spnSampleCount)
        Me.panSampleCount.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panSampleCount.Location = New System.Drawing.Point(8, 24)
        Me.panSampleCount.Name = "panSampleCount"
        Me.panSampleCount.Size = New System.Drawing.Size(90, 21)
        Me.panSampleCount.TabIndex = 58
        '
        'spnSampleCount
        '
        Me.spnSampleCount.Location = New System.Drawing.Point(72, -2)
        Me.spnSampleCount.Name = "spnSampleCount"
        Me.spnSampleCount.Size = New System.Drawing.Size(16, 20)
        Me.spnSampleCount.TabIndex = 51
        '
        'tabUserOptions_Page3
        '
        Me.tabUserOptions_Page3.Controls.Add(Me.fraTrigOut)
        Me.tabUserOptions_Page3.Controls.Add(Me.fraSampleTimePLCycles)
        Me.tabUserOptions_Page3.Controls.Add(Me.fraSampleTimeAperture)
        Me.tabUserOptions_Page3.Controls.Add(Me.fraModeTrigParm)
        Me.tabUserOptions_Page3.Controls.Add(Me.cmdSelfTest)
        Me.tabUserOptions_Page3.Controls.Add(Me.cmdAbout)
        Me.tabUserOptions_Page3.Controls.Add(Me.cmdUpdateTIP)
        Me.tabUserOptions_Page3.Controls.Add(Me.Panel_Conifg)
        Me.tabUserOptions_Page3.Location = New System.Drawing.Point(4, 22)
        Me.tabUserOptions_Page3.Name = "tabUserOptions_Page3"
        Me.tabUserOptions_Page3.Size = New System.Drawing.Size(498, 219)
        Me.tabUserOptions_Page3.TabIndex = 2
        Me.tabUserOptions_Page3.Text = "Options"
        '
        'fraTrigOut
        '
        Me.fraTrigOut.Controls.Add(Me.cboTriggerOutput)
        Me.fraTrigOut.Controls.Add(Me.optTriggerOutputOff)
        Me.fraTrigOut.Controls.Add(Me.optTriggerOutputOn)
        Me.fraTrigOut.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraTrigOut.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraTrigOut.Location = New System.Drawing.Point(8, 65)
        Me.fraTrigOut.Name = "fraTrigOut"
        Me.fraTrigOut.Size = New System.Drawing.Size(130, 77)
        Me.fraTrigOut.TabIndex = 29
        Me.fraTrigOut.TabStop = False
        Me.fraTrigOut.Text = "Trigger Output"
        '
        'cboTriggerOutput
        '
        Me.cboTriggerOutput.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cboTriggerOutput.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTriggerOutput.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboTriggerOutput.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboTriggerOutput.Items.AddRange(New Object() {"TTL Trigger 0", "TTL Trigger 1", "TTL Trigger 2", "TTL Trigger 3", "TTL Trigger 4", "TTL Trigger 5", "TTL Trigger 6", "TTL Trigger 7"})
        Me.cboTriggerOutput.Location = New System.Drawing.Point(8, 40)
        Me.cboTriggerOutput.Name = "cboTriggerOutput"
        Me.cboTriggerOutput.Size = New System.Drawing.Size(114, 21)
        Me.cboTriggerOutput.TabIndex = 30
        '
        'optTriggerOutputOff
        '
        Me.optTriggerOutputOff.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optTriggerOutputOff.Location = New System.Drawing.Point(63, 20)
        Me.optTriggerOutputOff.Name = "optTriggerOutputOff"
        Me.optTriggerOutputOff.Size = New System.Drawing.Size(50, 18)
        Me.optTriggerOutputOff.TabIndex = 44
        Me.optTriggerOutputOff.Text = "Off"
        '
        'optTriggerOutputOn
        '
        Me.optTriggerOutputOn.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optTriggerOutputOn.Location = New System.Drawing.Point(8, 20)
        Me.optTriggerOutputOn.Name = "optTriggerOutputOn"
        Me.optTriggerOutputOn.Size = New System.Drawing.Size(49, 18)
        Me.optTriggerOutputOn.TabIndex = 43
        Me.optTriggerOutputOn.Text = "On"
        '
        'fraSampleTimePLCycles
        '
        Me.fraSampleTimePLCycles.Controls.Add(Me.cboNPLC)
        Me.fraSampleTimePLCycles.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraSampleTimePLCycles.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraSampleTimePLCycles.Location = New System.Drawing.Point(146, 65)
        Me.fraSampleTimePLCycles.Name = "fraSampleTimePLCycles"
        Me.fraSampleTimePLCycles.Size = New System.Drawing.Size(144, 77)
        Me.fraSampleTimePLCycles.TabIndex = 26
        Me.fraSampleTimePLCycles.TabStop = False
        Me.fraSampleTimePLCycles.Text = "Power-Line Cycles"
        '
        'cboNPLC
        '
        Me.cboNPLC.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cboNPLC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboNPLC.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboNPLC.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboNPLC.Items.AddRange(New Object() {"0.2 Cycles", "1 Cycle", "10 Cycles", "100 Cycles"})
        Me.cboNPLC.Location = New System.Drawing.Point(8, 41)
        Me.cboNPLC.Name = "cboNPLC"
        Me.cboNPLC.Size = New System.Drawing.Size(121, 21)
        Me.cboNPLC.TabIndex = 41
        '
        'fraSampleTimeAperture
        '
        Me.fraSampleTimeAperture.Controls.Add(Me.cboAperture)
        Me.fraSampleTimeAperture.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraSampleTimeAperture.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraSampleTimeAperture.Location = New System.Drawing.Point(146, 8)
        Me.fraSampleTimeAperture.Name = "fraSampleTimeAperture"
        Me.fraSampleTimeAperture.Size = New System.Drawing.Size(144, 50)
        Me.fraSampleTimeAperture.TabIndex = 25
        Me.fraSampleTimeAperture.TabStop = False
        Me.fraSampleTimeAperture.Text = "Aperture"
        '
        'cboAperture
        '
        Me.cboAperture.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cboAperture.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboAperture.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboAperture.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboAperture.Location = New System.Drawing.Point(8, 20)
        Me.cboAperture.Name = "cboAperture"
        Me.cboAperture.Size = New System.Drawing.Size(114, 21)
        Me.cboAperture.TabIndex = 40
        '
        'fraModeTrigParm
        '
        Me.fraModeTrigParm.Controls.Add(Me.cboTriggerSource)
        Me.fraModeTrigParm.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraModeTrigParm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraModeTrigParm.Location = New System.Drawing.Point(8, 8)
        Me.fraModeTrigParm.Name = "fraModeTrigParm"
        Me.fraModeTrigParm.Size = New System.Drawing.Size(130, 50)
        Me.fraModeTrigParm.TabIndex = 23
        Me.fraModeTrigParm.TabStop = False
        Me.fraModeTrigParm.Text = "Trigger Source"
        '
        'cboTriggerSource
        '
        Me.cboTriggerSource.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cboTriggerSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTriggerSource.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboTriggerSource.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboTriggerSource.Items.AddRange(New Object() {"TTL Trigger 0", "TTL Trigger 1", "TTL Trigger 2", "TTL Trigger 3", "TTL Trigger 4", "TTL Trigger 5", "TTL Trigger 6", "TTL Trigger 7", "VXI Bus Trigger", "External Trigger", "Immediate Trigger"})
        Me.cboTriggerSource.Location = New System.Drawing.Point(8, 16)
        Me.cboTriggerSource.Name = "cboTriggerSource"
        Me.cboTriggerSource.Size = New System.Drawing.Size(114, 21)
        Me.cboTriggerSource.TabIndex = 24
        '
        'cmdSelfTest
        '
        Me.cmdSelfTest.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSelfTest.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSelfTest.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSelfTest.Location = New System.Drawing.Point(88, 186)
        Me.cmdSelfTest.Name = "cmdSelfTest"
        Me.cmdSelfTest.Size = New System.Drawing.Size(92, 23)
        Me.cmdSelfTest.TabIndex = 27
        Me.cmdSelfTest.Text = "Built-In &Test"
        Me.cmdSelfTest.UseVisualStyleBackColor = False
        '
        'cmdAbout
        '
        Me.cmdAbout.BackColor = System.Drawing.SystemColors.Control
        Me.cmdAbout.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAbout.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAbout.Location = New System.Drawing.Point(8, 186)
        Me.cmdAbout.Name = "cmdAbout"
        Me.cmdAbout.Size = New System.Drawing.Size(76, 23)
        Me.cmdAbout.TabIndex = 28
        Me.cmdAbout.Text = "&About"
        Me.cmdAbout.UseVisualStyleBackColor = False
        '
        'cmdUpdateTIP
        '
        Me.cmdUpdateTIP.BackColor = System.Drawing.SystemColors.Control
        Me.cmdUpdateTIP.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdUpdateTIP.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdUpdateTIP.Location = New System.Drawing.Point(186, 186)
        Me.cmdUpdateTIP.Name = "cmdUpdateTIP"
        Me.cmdUpdateTIP.Size = New System.Drawing.Size(88, 23)
        Me.cmdUpdateTIP.TabIndex = 45
        Me.cmdUpdateTIP.Text = "&Update TIP"
        Me.cmdUpdateTIP.UseVisualStyleBackColor = False
        Me.cmdUpdateTIP.Visible = False
        '
        'Panel_Conifg
        '
        Me.Panel_Conifg.BackColor = System.Drawing.SystemColors.Control
        Me.Panel_Conifg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Panel_Conifg.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Panel_Conifg.Location = New System.Drawing.Point(296, 64)
        Me.Panel_Conifg.Name = "Panel_Conifg"
        Me.Panel_Conifg.Parent_Object = Nothing
        Me.Panel_Conifg.Refresh = CType(0, Short)
        Me.Panel_Conifg.Size = New System.Drawing.Size(171, 140)
        Me.Panel_Conifg.TabIndex = 46
        '
        'tabUserOptions_Page4
        '
        Me.tabUserOptions_Page4.Controls.Add(Me.Atlas_SFP)
        Me.tabUserOptions_Page4.Location = New System.Drawing.Point(4, 22)
        Me.tabUserOptions_Page4.Name = "tabUserOptions_Page4"
        Me.tabUserOptions_Page4.Size = New System.Drawing.Size(498, 219)
        Me.tabUserOptions_Page4.TabIndex = 3
        Me.tabUserOptions_Page4.Text = "ATLAS"
        '
        'Atlas_SFP
        '
        Me.Atlas_SFP.ATLAS = Nothing
        Me.Atlas_SFP.BackColor = System.Drawing.SystemColors.Control
        Me.Atlas_SFP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Atlas_SFP.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Atlas_SFP.Location = New System.Drawing.Point(0, 0)
        Me.Atlas_SFP.Name = "Atlas_SFP"
        Me.Atlas_SFP.Parent_Object = Nothing
        Me.Atlas_SFP.Size = New System.Drawing.Size(398, 210)
        Me.Atlas_SFP.TabIndex = 0
        '
        'cmdReset
        '
        Me.cmdReset.BackColor = System.Drawing.SystemColors.Control
        Me.cmdReset.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdReset.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdReset.Location = New System.Drawing.Point(320, 307)
        Me.cmdReset.Name = "cmdReset"
        Me.cmdReset.Size = New System.Drawing.Size(76, 23)
        Me.cmdReset.TabIndex = 6
        Me.cmdReset.Text = "&Reset"
        Me.cmdReset.UseVisualStyleBackColor = False
        '
        'cmdQuit
        '
        Me.cmdQuit.BackColor = System.Drawing.SystemColors.Control
        Me.cmdQuit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdQuit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdQuit.Location = New System.Drawing.Point(426, 307)
        Me.cmdQuit.Name = "cmdQuit"
        Me.cmdQuit.Size = New System.Drawing.Size(76, 23)
        Me.cmdQuit.TabIndex = 5
        Me.cmdQuit.Text = "&Quit"
        Me.cmdQuit.UseVisualStyleBackColor = False
        '
        'panDmmDisplay
        '
        Me.panDmmDisplay.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panDmmDisplay.Controls.Add(Me.txtDmmDisplay)
        Me.panDmmDisplay.Enabled = False
        Me.panDmmDisplay.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panDmmDisplay.ForeColor = System.Drawing.SystemColors.Control
        Me.panDmmDisplay.Location = New System.Drawing.Point(8, 0)
        Me.panDmmDisplay.Name = "panDmmDisplay"
        Me.panDmmDisplay.Size = New System.Drawing.Size(284, 50)
        Me.panDmmDisplay.TabIndex = 2
        '
        'txtDmmDisplay
        '
        Me.txtDmmDisplay.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtDmmDisplay.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDmmDisplay.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtDmmDisplay.Location = New System.Drawing.Point(5, 5)
        Me.txtDmmDisplay.Name = "txtDmmDisplay"
        Me.txtDmmDisplay.Size = New System.Drawing.Size(274, 28)
        Me.txtDmmDisplay.TabIndex = 4
        Me.txtDmmDisplay.Text = "Ready"
        '
        'sbrUserInformation
        '
        Me.sbrUserInformation.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.sbrUserInformation.Location = New System.Drawing.Point(0, 342)
        Me.sbrUserInformation.Name = "sbrUserInformation"
        Me.sbrUserInformation.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.sbrUserInformation_Panel1})
        Me.sbrUserInformation.ShowPanels = True
        Me.sbrUserInformation.Size = New System.Drawing.Size(529, 17)
        Me.sbrUserInformation.SizingGrip = False
        Me.sbrUserInformation.TabIndex = 1
        '
        'sbrUserInformation_Panel1
        '
        Me.sbrUserInformation_Panel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
        Me.sbrUserInformation_Panel1.Name = "sbrUserInformation_Panel1"
        Me.sbrUserInformation_Panel1.Text = "Status Bar / User Information"
        Me.sbrUserInformation_Panel1.Width = 529
        '
        'frmRi4152A
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(529, 359)
        Me.Controls.Add(Me.cmdHelp)
        Me.Controls.Add(Me.tabUserOptions)
        Me.Controls.Add(Me.cmdReset)
        Me.Controls.Add(Me.cmdQuit)
        Me.Controls.Add(Me.panDmmDisplay)
        Me.Controls.Add(Me.sbrUserInformation)
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmRi4152A"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Digital Multimeter"
        Me.tabUserOptions.ResumeLayout(False)
        Me.tabUserOptions_Page1.ResumeLayout(False)
        Me.fraMath.ResumeLayout(False)
        CType(Me.spnMath, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panMath.ResumeLayout(False)
        Me.panMath.PerformLayout()
        Me.fraRange.ResumeLayout(False)
        Me.fraFunction.ResumeLayout(False)
        Me.fraFunction.PerformLayout()
        Me.tabUserOptions_Page2.ResumeLayout(False)
        Me.fraAcFilter.ResumeLayout(False)
        Me.fraAutoZero.ResumeLayout(False)
        Me.fraInputResistance.ResumeLayout(False)
        Me.fraTrigger.ResumeLayout(False)
        Me.panTriggerDelay.ResumeLayout(False)
        Me.panTriggerDelay.PerformLayout()
        CType(Me.spnTriggerDelay, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraTriggerCount.ResumeLayout(False)
        Me.panTriggerCount.ResumeLayout(False)
        Me.panTriggerCount.PerformLayout()
        CType(Me.spnTriggerCount, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraSampleCount.ResumeLayout(False)
        Me.panSampleCount.ResumeLayout(False)
        Me.panSampleCount.PerformLayout()
        CType(Me.spnSampleCount, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabUserOptions_Page3.ResumeLayout(False)
        Me.fraTrigOut.ResumeLayout(False)
        Me.fraSampleTimePLCycles.ResumeLayout(False)
        Me.fraSampleTimeAperture.ResumeLayout(False)
        Me.fraModeTrigParm.ResumeLayout(False)
        Me.tabUserOptions_Page4.ResumeLayout(False)
        Me.panDmmDisplay.ResumeLayout(False)
        Me.panDmmDisplay.PerformLayout()
        CType(Me.sbrUserInformation_Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

End Sub

#End Region

    '=========================================================
    Private Const SW_SHOWNORMAL As Short = 1
    Private Const SW_SHOWMINIMIZED As Short = 2
    Private Const SW_SHOWMAXIMIZED As Short = 3

    Private Declare Function ShellExecute Lib "shell32.dll" Alias "ShellExecuteA" (ByVal hwnd As Integer, ByVal lpOperation As String, ByVal lpFile As String, ByVal lpParameters As String, ByVal lpDirectory As String, ByVal nShowCmd As Integer) As Integer

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

    Sub DispTxt(ByVal SetIndex As Short)
        Dim sValue As String

        'Set the TextBox displayed text
        If SetIndex = 2 Then
            sValue = TimeNotate(SetCur(SetIndex), SetUOM(SetIndex))
        Else
            'sValue = strings.format$(Val(SetCur$(SetIndex%)) / Val(SetUOM$(SetIndex%)), SetRes$(SetIndex%))
            sValue = Val(SetCur(SetIndex)).ToString()
        End If

        Select Case SetIndex
            Case 1
                txtMath.Text = sValue

            Case 2
                txtTriggerDelay.Text = sValue

            Case 3
                txtTriggerCount.Text = sValue

            Case 4
                txtSampleCount.Text = sValue
        End Select
    End Sub

    Private Sub cboAperture_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboAperture.SelectedIndexChanged
        Me.chkContinuous.Checked = False
        Aperture = TranslateApertureChoice(Me.cboAperture.Text)
        Select Case InstrumentMode
            Case 1 To 6, 9
                cboNPLC.SelectedIndex = cboAperture.SelectedIndex
        End Select
    End Sub

    Private Sub cboMath_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboMath.SelectedIndexChanged
        Me.chkContinuous.Checked = False
        AdjustMathComboBox()
    End Sub

    Private Sub cboNPLC_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboNPLC.SelectedIndexChanged
        Me.chkContinuous.Checked = False
        NPLC = TranslateNPLCChoice(Me.cboNPLC.Text)
        cboAperture.SelectedIndex = cboNPLC.SelectedIndex
    End Sub

    Private Sub cboRange_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboRange.SelectedIndexChanged
        Me.chkContinuous.Checked = False
        RANGE = TranslateRangeChoice(Me.cboRange.Text)
    End Sub

    Private Sub cboTriggerOutput_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboTriggerOutput.SelectedIndexChanged
        AdjustTriggerOutput()
    End Sub

    Private Sub cboTriggerSource_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboTriggerSource.SelectedIndexChanged
        Me.chkContinuous.Checked = False

        'Trigger Count and Sample Count
        If cboTriggerSource.SelectedIndex = 10 Then
            fraTriggerCount.Visible = False
            fraSampleCount.Visible = False
        Else
            fraTriggerCount.Visible = True
            fraSampleCount.Visible = True
        End If

        TRIGGER = TranslateTriggerChoice(Me.cboTriggerSource.Text)
    End Sub

    Private Sub chkContinuous_Click(ByVal sender As Object, ByVal e As EventArgs) Handles chkContinuous.Click
        If chkContinuous.Checked = False Then
            If sTIP_Mode = "TIP_RUNPERSIST" Then ExitDmm()
            cmdMeasure.Enabled = True
        End If
    End Sub

    Private Sub chkContinuous_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles chkContinuous.MouseMove
        SendStatusBarMessage("Measure data continuously.")
    End Sub

    Private Sub chkDelay_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkDelay.CheckedChanged
        Me.chkContinuous.Checked = False

        Me.spnTriggerDelay.Visible = Not chkDelay.Checked
        Me.panTriggerDelay.Visible = Not chkDelay.Checked
        ' ??    frmRi4152A.panTdUnit.Visible = Not Value%

        If chkDelay.Checked Then
            Me.txtTriggerDelay.Text = ""
        Else
            Me.txtTriggerDelay.Text = TimeNotate(SetCur(TRIGGER_DELAY), SetUOM(TRIGGER_DELAY))
        End If
    End Sub

    Private Sub chkTriggerCount_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTriggerCount.CheckedChanged
        Me.chkContinuous.Checked = False

        Me.panTriggerCount.Visible = Not chkTriggerCount.Checked

        If chkTriggerCount.Checked Then
            SetCur(TRIG_COUN) = "50000"
            Me.txtTriggerCount.Text = SetCur(TRIG_COUN)
        Else
            SetCur(TRIG_COUN) = "1"
            Me.txtTriggerCount.Text = SetCur(TRIG_COUN)
        End If
        TRIGGER = TranslateTriggerChoice(Me.cboTriggerSource.Text)
    End Sub

    Private Sub cmdAbout_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdAbout.Click
        Me.chkContinuous.Checked = False
        frmAbout.cmdOk.Visible = True
        frmAbout.ShowDialog()
    End Sub

    Private Sub cmdAbout_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles cmdAbout.MouseMove
        SendStatusBarMessage("Get instrument information.")
    End Sub

    Public Function Build_Atlas() As String
        Build_Atlas = ""
        ' first, determine what button is currently pushed
        'InstrumentMode% is used instead of tbrDmmFunctions.Index

        Dim sTestString As String = ""

        'MEASURE, (VOLTAGE),
        'DC SIGNAL,
        'VOLTAGE
        '       RANGE$ = TranslateRangeChoice(frmRi4152A.cboRange.Text)
        Select Case InstrumentMode
            Case 1
                '"DCV"
                If cboMath.Text = "Average" Then
                    sTestString = "MEASURE, (AV-VOLTAGE INTO 'MEASUREMENT'), DC SIGNAL," & vbCrLf
                    If Me.cboRange.Text = "AUTO Range" Then
                        sTestString = sTestString + "AV-VOLTAGE MAX 300 V," & vbCrLf
                    Else
                        sTestString = sTestString + "AV-VOLTAGE MAX " & Me.cboRange.Text & "," & vbCrLf
                    End If

                    'sample count
                    If Me.cboTriggerSource.SelectedIndex <> 0 Then
                        sTestString = sTestString + "SAMPLE-COUNT " & Me.txtSampleCount.Text & "," & vbCrLf
                    End If
                Else
                    sTestString = "MEASURE, (VOLTAGE INTO 'MEASUREMENT'), DC SIGNAL," & vbCrLf
                    If Me.cboRange.Text = "AUTO Range" Then
                        sTestString = sTestString + "VOLTAGE MAX 300 V," & vbCrLf
                    Else
                        sTestString = sTestString + "VOLTAGE MAX " & Me.cboRange.Text & "," & vbCrLf
                    End If
                End If

            Case 2
                '"ACV"
                If cboMath.Text = "Average" Then
                    sTestString = "MEASURE, (AV-VOLTAGE INTO 'MEASUREMENT'), AC SIGNAL," & vbCrLf
                    If Me.cboRange.Text = "AUTO Range" Then
                        sTestString = sTestString + "AV-VOLTAGE MAX 300 V," & vbCrLf
                    Else
                        sTestString = sTestString + "AV-VOLTAGE MAX " & Me.cboRange.Text & "," & vbCrLf
                    End If

                    'sample count
                    If Me.cboTriggerSource.SelectedIndex <> 0 Then
                        sTestString = sTestString + "SAMPLE-COUNT " & Me.txtSampleCount.Text & "," & vbCrLf
                    End If
                Else
                    sTestString = "MEASURE, (VOLTAGE INTO 'MEASUREMENT'), AC SIGNAL," & vbCrLf
                    If Me.cboRange.Text = "AUTO Range" Then
                        sTestString = sTestString + "VOLTAGE MAX 300 V," & vbCrLf
                    Else
                        sTestString = sTestString + "VOLTAGE MAX " & Me.cboRange.Text & "," & vbCrLf
                    End If
                End If

                'bandwidth
                sTestString &= BuildBandwidth()

            Case 3
                '"DCVR"
                sTestString = "MEASURE, (VOLTAGE-RATIO INTO 'MEASUREMENT'), DC SIGNAL," & vbCrLf
                If Me.cboRange.Text = "AUTO Range" Then
                    sTestString = sTestString + "VOLTAGE-RATIO MAX 3000," & vbCrLf
                Else
                    sTestString = sTestString + "VOLTAGE-RATIO MAX " & Me.cboRange.Text & "," & vbCrLf
                End If

            Case 4
                '"DCC"
                If cboMath.Text = "Average" Then
                    sTestString = "MEASURE, (AV-CURRENT INTO 'MEASUREMENT'), DC SIGNAL," & vbCrLf
                    If Me.cboRange.Text = "AUTO Range" Then
                        sTestString = sTestString + "AV-CURRENT MAX 3 A," & vbCrLf
                    Else
                        sTestString = sTestString + "AV-CURRENT MAX " & Me.cboRange.Text & "," & vbCrLf

                    End If
                Else
                    sTestString = "MEASURE, (CURRENT INTO 'MEASUREMENT'), DC SIGNAL," & vbCrLf
                    If Me.cboRange.Text = "AUTO Range" Then
                        sTestString = sTestString + "CURRENT MAX 3 A," & vbCrLf
                    Else
                        sTestString = sTestString + "CURRENT MAX " & Me.cboRange.Text & "," & vbCrLf

                    End If
                End If

            Case 5
                '"ACC"
                If cboMath.Text = "Average" Then
                    sTestString = "MEASURE, (AV-CURRENT INTO 'MEASUREMENT'), AC SIGNAL " & vbCrLf
                    If Me.cboRange.Text = "AUTO Range" Then
                        sTestString = sTestString + "AV-CURRENT MAX 3 A," & vbCrLf
                    Else
                        sTestString = sTestString + "AV-CURRENT MAX " & Me.cboRange.Text & "," & vbCrLf
                    End If
                Else
                    sTestString = "MEASURE, (CURRENT INTO 'MEASUREMENT'), AC SIGNAL " & vbCrLf
                    If Me.cboRange.Text = "AUTO Range" Then
                        sTestString = sTestString + "CURRENT MAX 3 A," & vbCrLf
                    Else
                        sTestString = sTestString + "CURRENT MAX " & Me.cboRange.Text & "," & vbCrLf
                    End If
                End If

                'bandwidth
                sTestString &= BuildBandwidth()

            Case 6
                '"2WR"
                sTestString = "MEASURE, (RES INTO 'MEASUREMENT'), IMPEDANCE," & vbCrLf
                If Me.cboRange.Text = "AUTO Range" Then
                    sTestString = sTestString + "RES MAX 100E+6 OHM," & vbCrLf
                Else
                    sTestString = sTestString + "RES MAX " & Me.cboRange.Text & "," & vbCrLf
                End If

                'ref-res
                If cboMath.Text = "dB" Or cboMath.Text = "dBm" Then
                    sTestString = sTestString + "REF-RES " & txtMath.Text & " OHM," & vbCrLf
                End If

            Case 7
                '"FREQ"
                sTestString = "MEASURE, (FREQ INTO 'MEASUREMENT'), AC SIGNAL," & vbCrLf & "FREQ MAX 300E+3 HZ, " & vbCrLf

                'voltage
                ' NOTE: no control to populate this field

                'bandwidth
                sTestString &= BuildBandwidth()

            Case 8
                '"PER"
                sTestString = "MEASURE, (PERIOD INTO 'MEASUREMENT'), AC SIGNAL," & vbCrLf & "PERIOD MAX 0.333 SEC," & vbCrLf

                'voltage
                ' NOTE: no control to populate this field

                'bandwidth
                sTestString &= BuildBandwidth()

            Case 9
                '"4WR"
                sTestString = "MEASURE, (RES INTO 'MEASUREMENT'), IMPEDANCE," & vbCrLf
                If Me.cboRange.Text = "AUTO Range" Then
                    sTestString = sTestString + "RES MAX 100E+6 OHM," & vbCrLf
                Else
                    sTestString = sTestString + "RES MAX " & Me.cboRange.Text & "," & vbCrLf
                End If

                'ref-res
                If cboMath.Text = "dB" Or cboMath.Text = "dBm" Then
                    sTestString = sTestString + "REF-RES " & txtMath.Text & " OHM," & vbCrLf
                End If

        End Select

        'Strob to Event
        If cboTriggerSource.SelectedIndex = 9 Then
            sTestString &= "STROBE-TO-EVENT <event lable> MAX-TIME <dec value> SEC," & vbCrLf
        End If

        sTestString &= "CNX HI LO $" & vbCrLf

        Build_Atlas = sTestString
    End Function

    Private Function BuildBandwidth() As String
        BuildBandwidth = ""
        Dim sTestString As String = ""

        'bandwidth
        If optAcFilterFast.Checked = True Then
            sTestString &= "BANDWIDTH RANGE 200 HZ TO 300 KHZ," & vbCrLf
        ElseIf optAcFilterMedium.Checked = True Then
            sTestString &= "BANDWIDTH RANGE 20 HZ TO 300 KHZ," & vbCrLf
        ElseIf optAcFilterSlow.Checked = True Then
            sTestString &= "BANDWIDTH RANGE 3 HZ TO 300 KHZ," & vbCrLf
        End If

        BuildBandwidth = sTestString
    End Function

    Public Sub SetMode(ByVal sMode As String)
        If sMode <> InstrumentMode Then
            InstrumentMode = sMode
            'Apply the instrument to the control
            ChangeInstrumentMode()
            For Each Button In tbrDmmFunctions.Buttons
                Button.pushed = False
            Next
            'Depress the corresponding button on the toolbar
            Select Case InstrumentMode
                Case 1, 2, 3, 4, 5, 6, 7, 8
                    Me.tbrDmmFunctions.Buttons(InstrumentMode - 1).Pushed = True
                Case 9
                    Me.tbrDmmFunctions.Buttons(8).Pushed = True
                Case Else
                    Me.tbrDmmFunctions.Buttons(0).Pushed = True
            End Select
        End If
    End Sub

    Public Function GetMode() As String
        GetMode = InstrumentMode
    End Function

    Public Sub ConfigGetCurrent()
        Const curOnErrorGoToLabel_Default As Integer = 0
        Const curOnErrorGoToLabel_ErrorHandle As Integer = 1
        Dim vOnErrorGoToLabel As Integer = curOnErrorGoToLabel_Default

        Try
            Dim sInstrumentCmds As String
            Dim sReadBuffer As String
            'Dim lActWtiteLen As Integer
            Dim iLoopIndex As Short
            'Dim iBufferSize As Short
            'Dim iReadLen As Short
            Dim sMode As String
            Dim sRange As String
            Dim sMath As String
            Dim sMathEnabled As String

            'Dim vItem As Object
            Dim vParameters() As String
            Dim sParameter As String

            If LiveMode Then
                sReadBuffer = ""

                '       Items to Query
                '       Reference ChangeInstrumentMode
                '       Reference TakeMeasurement Sub

                vOnErrorGoToLabel = curOnErrorGoToLabel_ErrorHandle ' On Error GoTo ErrorHandle
                '       *********
                '       Get MODE
                '       *********
                sInstrumentCmds = "CONF?"
                SendScpiCommand(sInstrumentCmds)

                '       ##########################################
                '       Action Required: Remove debug code
                '       Fill return value selections
                '  ' gcolCmds.Add "Simulated response for: " & sInstrumentCmds
                '  ' gcolCmds.Add "CURR:AC +1.000000E+00,1.000000E-05"
                '  ' gcolCmds.Add "CURR +1.000000E+00,1.000000E-05"
                '  ' gcolCmds.Add "VOLT:AC +3.000000E+02,1.000000E-06"
                '  ' gcolCmds.Add "VOLT +3.000000E+02,1.000000E-06"
                '  ' gcolCmds.Add "VOLT:RAT +3.000000E+02,1.000000E-06"
                '  ' gcolCmds.Add "FRES +100.000000E+03,1.000000E-05"
                '  ' gcolCmds.Add "RES +1.000000E+03,1.000000E-03"
                '  ' gcolCmds.Add "FREQ +3.000000E+00,3.000000E-05"
                '  ' gcolCmds.Add "PER +3.333333E-01,3.333330E-06"
                '       ##########################################

                '       Retrieve instrument output buffer
                sReadBuffer = ReadInstrumentBuffer()

                '       Parse out Mode information
                If Len(sReadBuffer) > 0 Then

                    sMode = Strings.Mid(sReadBuffer, 1, InStr(1, sReadBuffer, " ", CompareMethod.Text) - 1)
                    sMode = Strings.Right(sMode, Len(sMode) - 1)
                    sParameter = Strings.Mid(sReadBuffer, InStr(1, sReadBuffer, " ", CompareMethod.Text) + 1)
                    vParameters = Split(sParameter, ",", , CompareMethod.Text)

                    '           Extract Range
                    '           (0.1V|1V|10V|100V|300V|MIN|MAX|DEF|AUTO)
                    sRange = vParameters(0)

                    '           Set Resolution
                    '           (resolution|MIN|MAX|DEF)
                    'sResolution = CStr(vParameters(1))

                    '*************************************************
                    'Config SFP
                    '*************************************************
                    'Allow use of ActivateControl
                    sTIP_Mode = "GET CURR CONFIG"

                    'Convert abreviated DC modes
                    If sMode = "CURR" Then
                        sMode = "CURR:DC"
                    ElseIf sMode = "VOLT" Then
                        sMode = "VOLT:DC"
                    ElseIf sMode = "VOLT:RAT" Then
                        sMode = "VOLT:DC:RAT"
                    End If

                    'Set Mode
                    DmmMain.ActivateControl(":SENS:FUNC " & Q & sMode & Q)

                    'Convert Ratio to set range
                    If sMode = "VOLT:DC:RAT" Then
                        sMode = "VOLT:DC"
                    ElseIf sMode = "PER" Then
                        sMode = "PER:VOLT"
                    ElseIf sMode = "FREQ" Then
                        sMode = "FREQ:VOLT"
                    End If

                    'see if instr is auto ranging
                    sInstrumentCmds = sMode & ":RANG:AUTO?"
                    SendScpiCommand(sInstrumentCmds)
                    '       Retrieve instrument output buffer
                    sReadBuffer = ReadInstrumentBuffer()

                    If sReadBuffer = "1" Then
                        sRange = "ON" 'yes auto ranging is on
                    Else
                        'see what the range is set to
                        sInstrumentCmds = sMode & ":RANG?"
                        SendScpiCommand(sInstrumentCmds)
                        '       Retrieve instrument output buffer
                        sReadBuffer = ReadInstrumentBuffer()
                        sRange = sReadBuffer
                    End If
                    'Set Range
                    If IsNumeric(sRange) Then
                        DmmMain.ActivateControl(":SENS:" & sMode & ":RANG " & sRange)
                    Else
                        DmmMain.ActivateControl(":SENS:" & sMode & ":RANG:AUTO ON")
                    End If
                    'Resolution is not displayed/set

                    '           *********
                    '           Get MATH
                    '           *********
                    ' first see if the math functions are "on" or "off"
                    ' then we will determine math functions.
                    sInstrumentCmds = "CALC:STAT?"
                    SendScpiCommand(sInstrumentCmds)

                    '   Retrieve instrument output buffer
                    sReadBuffer = ReadInstrumentBuffer() 'Returns "1" or "0"
                    sMathEnabled = sReadBuffer

                    'if not enabled then set math function to "none" else go and query type of
                    ' function
                    If sMathEnabled = "0" Then
                        DmmMain.ActivateControl(":CALC:FUNC " & "NONE")
                    Else

                        sInstrumentCmds = "CALC:FUNC?"
                        SendScpiCommand(sInstrumentCmds)

                        '           ##########################################
                        '           Action Required: Remove debug code
                        '           Fill return value selections
                        '            ' gcolCmds.Add "Simulated response for: " & sInstrumentCmds
                        '            ' gcolCmds.Add "NULL"
                        '            ' gcolCmds.Add "AVER"
                        '            ' gcolCmds.Add "DB"
                        '            ' gcolCmds.Add "DBM"
                        '            ' gcolCmds.Add "LIM"
                        '           ##########################################

                        '           Retrieve instrument output buffer
                        sReadBuffer = ReadInstrumentBuffer() 'Returns AVER, DB, DBM, LIM, or NULL
                        sMath = sReadBuffer

                        DmmMain.ActivateControl(":CALC:FUNC " & sMath)


                        If sMath = "DB" Or sMath = "DBM" Then
                            '*********
                            'Get Ref DBm or Ohm
                            '*********
                            sInstrumentCmds = "CALC:" & sMath & ":REF?"
                            SendScpiCommand(sInstrumentCmds)

                            'Retrieve instrument output buffer
                            sReadBuffer = ReadInstrumentBuffer() 'Returns Value

                            'Set Ref DBm or Ohm
                            DmmMain.ActivateControl(":CALC:" & sMath & ":REF " & sReadBuffer)
                        End If
                    End If

                    '           *******************
                    '           Get Impedance:Auto
                    '           *******************
                    sInstrumentCmds = "INP:IMP:AUTO?"
                    SendScpiCommand(sInstrumentCmds)

                    '           ##########################################
                    '           Action Required: Remove debug code
                    '           Fill return value selections
                    '            ' gcolCmds.Add "Simulated response for: " & sInstrumentCmds
                    '            ' gcolCmds.Add "0" ' Off
                    '            ' gcolCmds.Add "1" ' On
                    '           ##########################################

                    '           Retrieve instrument output buffer
                    sReadBuffer = ReadInstrumentBuffer()

                    'Set Impedance
                    If sReadBuffer = "1" Then
                        DmmMain.ActivateControl(":INP:IMP:AUTO ON")
                    Else
                        DmmMain.ActivateControl(":INP:IMP:AUTO OFF")
                    End If

                    '           *******************
                    '           Get AC Filter
                    '           *******************
                    sInstrumentCmds = "SENS:DET:BAND?"
                    SendScpiCommand(sInstrumentCmds)

                    '           ##########################################
                    '           Action Required: Remove debug code
                    '           Fill return value selections
                    '' gcolCmds.Add "Simulated response for: " & sInstrumentCmds
                    '' gcolCmds.Add "200"
                    '' gcolCmds.Add "20"
                    '' gcolCmds.Add "3"
                    '           ##########################################

                    '           Retrieve instrument output buffer
                    sReadBuffer = ReadInstrumentBuffer()

                    'Set Bandwidth
                    DmmMain.ActivateControl(":SENS:DET:BAND " & sReadBuffer)


                    '           *******************
                    '           Get Auto Zero
                    '           *******************
                    sInstrumentCmds = "ZERO:AUTO?"
                    SendScpiCommand(sInstrumentCmds)

                    '           ##########################################
                    '           Action Required: Remove debug code
                    '           Fill return value selections
                    '            ' gcolCmds.Add "Simulated response for: " & sInstrumentCmds
                    '            ' gcolCmds.Add "0"
                    '            ' gcolCmds.Add "1"
                    '           ##########################################

                    '           Retrieve instrument output buffer
                    sReadBuffer = ReadInstrumentBuffer()

                    'Set Auto Zero
                    If sReadBuffer = "1" Then
                        DmmMain.ActivateControl(":SENS:ZERO:AUTO ON")
                    Else
                        DmmMain.ActivateControl(":SENS:ZERO:AUTO OFF") 'Off or Once
                    End If


                    '           *************************
                    '           Get Trigger Delay (Auto)
                    '           *************************
                    sInstrumentCmds = "TRIG:DEL:AUTO?"
                    SendScpiCommand(sInstrumentCmds)

                    '           ##########################################
                    '           Action Required: Remove debug code
                    '           Fill return value selections
                    '            ' gcolCmds.Add "Simulated response for: " & sInstrumentCmds
                    '            ' gcolCmds.Add "0" ' Off
                    '            ' gcolCmds.Add "1" ' On
                    '           ##########################################

                    '           Retrieve instrument output buffer
                    sReadBuffer = ReadInstrumentBuffer()
                    'Set Auto Zero
                    Me.chkDelay.Checked = CInt(sReadBuffer) 'Not Set in ActivateControl

                    '           If not auto get Value
                    If sReadBuffer = "0" Then
                        '               ******************
                        '               Get Trigger Delay
                        '               ******************
                        sInstrumentCmds = "TRIG:DEL?"
                        SendScpiCommand(sInstrumentCmds)

                        '               ##########################################
                        '               Action Required: Remove debug code
                        '               Fill return value selections
                        '                ' gcolCmds.Add "Simulated response for: " & sInstrumentCmds
                        '                ' gcolCmds.Add "0.002" ' 0 - 3600 Seconds
                        '                ' gcolCmds.Add "10.000" ' 0 - 3600 Seconds
                        '                ' gcolCmds.Add "200.000" ' 0 - 3600 Seconds
                        '                ' gcolCmds.Add "600.000" ' 0 - 3600 Seconds
                        '                ' gcolCmds.Add "1500.000" ' 0 - 3600 Seconds
                        '                ' gcolCmds.Add "3600.000" ' 0 - 3600 Seconds
                        '               ##########################################

                        '               Retrieve instrument output buffer
                        sReadBuffer = ReadInstrumentBuffer()
                        'Set Delay
                        DmmMain.ActivateControl(":TRIG:DEL " & sReadBuffer)
                    End If

                    '           ******************
                    '           Get Trigger Source
                    '           ******************
                    sInstrumentCmds = "TRIG:SOUR?"
                    SendScpiCommand(sInstrumentCmds)

                    '           ##########################################
                    '           Action Required: Remove debug code
                    '           Fill return value selections
                    '            ' gcolCmds.Add "Simulated response for: " & sInstrumentCmds
                    '            ' gcolCmds.Add "IMM"
                    '            ' gcolCmds.Add "EXT"
                    '            ' gcolCmds.Add "BUS"
                    '            ' gcolCmds.Add "TTLT7"
                    '            ' gcolCmds.Add "TTLT6"
                    '            ' gcolCmds.Add "TTLT5"
                    '            ' gcolCmds.Add "TTLT4"
                    '            ' gcolCmds.Add "TTLT3"
                    '            ' gcolCmds.Add "TTLT2"
                    '            ' gcolCmds.Add "TTLT1"
                    '            ' gcolCmds.Add "TTLT0"
                    '           ##########################################

                    '           Retrieve instrument output buffer
                    sReadBuffer = ReadInstrumentBuffer()
                    'Set Trigger Source
                    DmmMain.ActivateControl(":TRIG:SOUR " & sReadBuffer)

                    '           ******************
                    '           Get Trigger Count
                    '           ******************
                    sInstrumentCmds = "TRIG:COUN?"
                    SendScpiCommand(sInstrumentCmds)

                    '           Retrieve instrument output buffer
                    sReadBuffer = ReadInstrumentBuffer()
                    'Set Trigger Count
                    DmmMain.ActivateControl(":TRIG:COUN " & sReadBuffer)


                    'Return Period sMode for remaining commands
                    If sMode = "PER:VOLT" Then
                        sMode = "PER"
                    ElseIf sMode = "FREQ:VOLT" Then
                        sMode = "FREQ"
                    End If
                    '           ******************
                    '           Get Aperature
                    '           mh:  only if sMode is not VOLT:AC or CURR:AC
                    '           ******************
                    sInstrumentCmds = sMode & ":APER?"
                    If sInstrumentCmds = "VOLT:AC:APER?" Or sInstrumentCmds = "CURR:AC:APER?" Then
                    Else
                        SendScpiCommand(sInstrumentCmds)

                        '           ##########################################
                        '           Action Required: Remove debug code
                        '           Fill return value selections
                        ' gcolCmds.Add "Simulated response for: " & sInstrumentCmds
                        ' gcolCmds.Add "3"
                        ' gcolCmds.Add "0.167"
                        ' gcolCmds.Add "0.01"
                        ' gcolCmds.Add "0.00333"
                        '           ##########################################

                        '               Retrieve instrument output buffer
                        sReadBuffer = ReadInstrumentBuffer()
                        'Set Aperature
                        DmmMain.ActivateControl(":SENS:" & sMode & ":APER " & sReadBuffer)
                    End If

                    '           ******************
                    '           Get NPLC
                    '           mh:  only if sMode is not VOLT:AC or CURR:AC
                    '           ******************
                    sInstrumentCmds = sMode & ":NPLC?"
                    If sInstrumentCmds = "VOLT:AC:NPLC?" Or sInstrumentCmds = "CURR:AC:NPLC?" Or sInstrumentCmds = "FREQ:NPLC?" Or sInstrumentCmds = "PER:NPLC?" Then
                    Else
                        SendScpiCommand(sInstrumentCmds)

                        '           ##########################################
                        '           Action Required: Remove debug code
                        '           Fill return value selections
                        ' gcolCmds.Add "Simulated response for: " & sInstrumentCmds
                        ' gcolCmds.Add "0.02"
                        ' gcolCmds.Add "0.2"
                        ' gcolCmds.Add "1"
                        ' gcolCmds.Add "10"
                        ' gcolCmds.Add "100"
                        '           ##########################################

                        '               Retrieve instrument output buffer
                        sReadBuffer = ReadInstrumentBuffer()
                        'Set NPLC
                        DmmMain.ActivateControl(":SENS:" & sMode & ":NPLC " & sReadBuffer)
                    End If


                    '           *******************
                    '           Get Output TTLTrg
                    '           *******************
                    For iLoopIndex = 0 To 7
                        sInstrumentCmds = "OUTP:TTLT" & iLoopIndex & ":STAT?"
                        SendScpiCommand(sInstrumentCmds)

                        '               ##########################################
                        '               Action Required: Remove debug code
                        '               Fill return value selections
                        ' gcolCmds.Add "Simulated response for: " & sInstrumentCmds
                        ' gcolCmds.Add "0" ' Off
                        ' gcolCmds.Add "1" ' On
                        '               ##########################################

                        '               Retrieve instrument output buffer
                        sReadBuffer = ReadInstrumentBuffer()
                        'Set Outputs
                        If sReadBuffer = "1" Then
                            DmmMain.ActivateControl(":OUTP:TTLT" & iLoopIndex & ":STAT ON")
                        Else
                            DmmMain.ActivateControl(":OUTP:TTLT" & iLoopIndex & ":STAT OFF")
                        End If
                    Next

                    '***************************************************
                End If
            End If

        Catch
            Dim Msg As String
            Select Case vOnErrorGoToLabel
                Case curOnErrorGoToLabel_ErrorHandle
                    If Err.Number = 13 Then
                        Msg = "Type Mismatch: Data received is not what was expected"
                        MessageBox.Show(Msg, "4152A", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        'Type Mismatch
                    Else
                        ' Display unanticipated error message.
                        Msg = "Unanticipated error " & Err.Number & ": " & Err.Description
                        MessageBox.Show(Msg, "4152A", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                Case curOnErrorGoToLabel_Default
                    ' ...
                Case Else
                    ' ...
            End Select
        End Try
    End Sub

    Private Sub cmdMeasure_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdMeasure.Click
        If (Panel_Conifg.DebugMode = True) Then 'Only perform switch operation if in Local Mode
            Exit Sub
        End If
        cmdMeasure.Enabled = False
        TakeMeasurement()
        cmdMeasure.Enabled = True
    End Sub

    Private Sub cmdMeasure_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles cmdMeasure.MouseMove
        SendStatusBarMessage("Acquire measurement data.")
    End Sub

    Private Sub cmdQuit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdQuit.Click
        QuitProgram = True
        Me.Close()
    End Sub

    Private Sub cmdQuit_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles cmdQuit.MouseMove
        SendStatusBarMessage("Quit instrument application.")
    End Sub

    Public Sub cmdReset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdReset.Click
        QuitProgram = False
        ResetDigitalMultimeter()
    End Sub

    Private Sub cmdReset_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles cmdReset.MouseMove
        SendStatusBarMessage("Reset instrument and settings.")
    End Sub

    Private Sub cmdSelfTest_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdSelfTest.Click
        Dim TestResult As String

        'Reset Instrument and GUI
        ResetDigitalMultimeter()
        'Allow for a full reset
        Delay(1)
        SendStatusBarMessage("Performing Built-In Test...")
        'Allow Message to be written to the toolbar

        'Disable user interaction
        Me.Enabled = False

        If LiveMode Then 'If VISA Communication has been verified
            SendScpiCommand("*OPC")
            SendScpiCommand("*TST?")
            'Changed delay to 15 Sec to allow time for the instriment to perform BIT
            'EADS 072309
            Delay(15)
            TestResult = ReadInstrumentBuffer()
            If TestResult = "0" Then 'Pass Condition
                MessageBox.Show("Built-In Test Passed", "4152A", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else                'Fail Condition
                SendScpiCommand("SYST:ERR?")
                TestResult = ReadInstrumentBuffer()
                MessageBox.Show("Built-In Test Failed. ", "4152A", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            'Notify user of BIT completion
            SendStatusBarMessage("Built-In Test completed.")
            'Allow message to be written to the status bar control
            Delay(2)
        Else            'Communication error condition
            MessageBox.Show("Instrument Is Not Responding", "4152A", MessageBoxButtons.OK, MessageBoxIcon.Information)
            SendStatusBarMessage("")
        End If

        'Enable user interaction
        Me.Enabled = True
    End Sub

    Private Sub cmdSelfTest_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles cmdSelfTest.MouseMove
        SendStatusBarMessage("Perform instrument Built-In Test function.")
    End Sub

    Private Sub cmdUpdateTIP_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdUpdateTIP.Click
        'Disable user interaction
        Me.Enabled = False
        bBuildingTIPstr = True
        sTIP_CMDstring = ""

        Try
            'Program Instrument and build TIP CMD string
            SetSENSeOptions()
            If Not bGetInstrumentStatus() Then Throw New SystemException()
            SetINPutOptions()
            If Not bGetInstrumentStatus() Then Throw New SystemException()
            SetCALCulateOptions()
            If Not bGetInstrumentStatus() Then Throw New SystemException()
            SetOUTPutOptions()
            If Not bGetInstrumentStatus() Then Throw New SystemException()
            SetTRIGgerOptions()
            If Not bGetInstrumentStatus() Then Throw New SystemException()
            bBuildingTIPstr = False

            'Now send entire TIP command string and check for errors such as setting conflicts
            SendScpiCommand("*RST;*CLS")
            SendScpiCommand(sTIP_CMDstring)
            If Not bGetInstrumentStatus() Then Throw New SystemException()

            'Don't exit if error, allow user opportunity to correct
            'If no errors, send TIP CMD string to VIPERT.INI file and close
            SetKey("TIPS", "CMD", sTIP_CMDstring)
            SetKey("TIPS", "STATUS", "Ready")
            ExitDmm()

        Catch
            Me.Enabled = True
            SendStatusBarMessage("Error occured. Click RESET and re-try.")
            sTIP_CMDstring = ""
            ResetDigitalMultimeter()
        End Try
    End Sub

    Private Sub cmdUpdateTIP_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles cmdUpdateTIP.MouseMove
        SendStatusBarMessage("Send instrument setup to TIP Studio.")
    End Sub

    Private Sub frmRi4152A_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '   Set Common Controls parent properties
        Atlas_SFP.Parent_Object = Me
        Panel_Conifg.Parent_Object = Me
        Main()
    End Sub

    'Private Sub Form_QueryUnload(ByRef Cancel As Short, ByVal UnloadMode As Short)
    '    ExitDmm()
    'End Sub

    Private Sub frmRi4152A_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If chkContinuous.Checked Then
            Dim i As Integer

            chkContinuous.Checked = False
            LiveMode = False
            For i = 0 To 10
                Application.DoEvents()
                System.Threading.Thread.Sleep(10)
            Next
        End If
    End Sub

    Private Sub frmRi4152A_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        ExitDmm()
    End Sub

    'Private Sub Form_Unload(ByRef Cancel As Short)
    '    ExitDmm()
    'End Sub

    Public Sub fraAcFilter_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles fraAcFilter.MouseMove
        SendStatusBarMessage("Set instrument AC input filter.")
    End Sub

    Private Sub fraAutoZero_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles fraAutoZero.MouseMove
        SendStatusBarMessage("Set instrument automatic zeroing function.")
    End Sub

    Private Sub fraFunction_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles fraFunction.MouseMove
        SendStatusBarMessage("Set instrument measurement function.")
    End Sub

    Private Sub fraInputResistance_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles fraInputResistance.MouseMove
        SendStatusBarMessage("Set instrument input impedance.")
    End Sub

    Public Sub fraMath_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles fraMath.MouseMove
        SendStatusBarMessage("Set math measurement function.")
    End Sub

    Private Sub fraModeTrigParm_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles fraModeTrigParm.MouseMove
        SendStatusBarMessage("Set instrument external triggering source.")
    End Sub

    Private Sub fraRange_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles fraRange.MouseMove
        SendStatusBarMessage("Set instrument measurement range.")
    End Sub

    Private Sub fraSampleTimeAperture_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles fraSampleTimeAperture.MouseMove
        SendStatusBarMessage("Set instrument measurement aperature.")
    End Sub

    Private Sub fraSampleTimePLCycles_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles fraSampleTimePLCycles.MouseMove
        SendStatusBarMessage("Set instrument number of power line cycles.")
    End Sub

    Private Sub fraTrigger_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles fraTrigger.MouseMove
        SendStatusBarMessage("Set instrument input trigger.")
    End Sub

    Private Sub fraTrigOut_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles fraTrigOut.MouseMove
        SendStatusBarMessage("Set instrument trigger output functions.")
    End Sub

    Private Sub cmdHelp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdHelp.Click
        Dim helpFile As String = Application.StartupPath + "\..\Config\Help\Dmm_Ri4152A\m4152a.pdf"
        Dim helpFile2 As String = Application.StartupPath + "\..\Config\Help\Dmm_Ri4152A\E1412A.pdf"
        Dim Version As String
        Dim SystemType As String
        Version = GatherIniFileInformation("System Startup", "SWR", "Unknown")
        SystemType = GatherIniFileInformation("System Startup", "SYSTEM_TYPE", "Unknown")

        If (InStr(SystemType, "AN/USM-657(V)2")) Then
            ShellExecute(0, "Open", helpFile2, 0, 0, SW_SHOWNORMAL)
        Else
            ShellExecute(0, "Open", helpFile, 0, 0, SW_SHOWNORMAL)
        End If

    End Sub

    Private Sub optAcFilterFast_CheckedChanged(sender As Object, e As EventArgs) Handles optAcFilterFast.CheckedChanged
        Me.chkContinuous.Checked = False
    End Sub

    Private Sub optAcFilterMedium_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optAcFilterMedium.CheckedChanged
        Me.chkContinuous.Checked = False
    End Sub

    Private Sub optAcFilterSlow_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optAcFilterSlow.CheckedChanged
        Me.chkContinuous.Checked = False
    End Sub

    Private Sub optAutoZeroOn_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optAutoZeroOn.CheckedChanged
        Me.chkContinuous.Checked = False
    End Sub

    Private Sub optAutoZeroOff_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optAutoZeroOff.CheckedChanged
        Me.chkContinuous.Checked = False
    End Sub

    Private Sub optAutoZeroOnce_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optAutoZeroOnce.CheckedChanged
        Me.chkContinuous.Checked = False
    End Sub

    Private Sub optInputResistanceAuto_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optInputResistanceAuto.CheckedChanged
        Me.chkContinuous.Checked = False
    End Sub

    Private Sub optInputResistance10MOhm_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optInputResistance10MOhm.CheckedChanged
        Me.chkContinuous.Checked = False
    End Sub

    Private Sub optTriggerOutputOff_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optTriggerOutputOff.CheckedChanged
        optTriggerOutput_Click()
    End Sub

    Private Sub optTriggerOutputOn_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optTriggerOutputOn.CheckedChanged
        optTriggerOutput_Click()
    End Sub

    Public Sub optTriggerOutput_Click()
        Me.chkContinuous.Checked = False

        Select Case Me.cboTriggerOutput.Text
            Case "TTL Trigger 7"
                OUTPUT_TRIGGER_STATE(7) = optTriggerOutputOn.Checked
            Case "TTL Trigger 6"
                OUTPUT_TRIGGER_STATE(6) = optTriggerOutputOn.Checked
            Case "TTL Trigger 5"
                OUTPUT_TRIGGER_STATE(5) = optTriggerOutputOn.Checked
            Case "TTL Trigger 4"
                OUTPUT_TRIGGER_STATE(4) = optTriggerOutputOn.Checked
            Case "TTL Trigger 3"
                OUTPUT_TRIGGER_STATE(3) = optTriggerOutputOn.Checked
            Case "TTL Trigger 2"
                OUTPUT_TRIGGER_STATE(2) = optTriggerOutputOn.Checked
            Case "TTL Trigger 1"
                OUTPUT_TRIGGER_STATE(1) = optTriggerOutputOn.Checked
            Case "TTL Trigger 0"
                OUTPUT_TRIGGER_STATE(0) = optTriggerOutputOn.Checked
        End Select
    End Sub

    Private Sub spnMath_DownButtonClicked(sender As Object, e As EventArgs) Handles spnMath.DownButtonClicked
        Dim OldVal As Double
        Dim NewVal As Double

        Me.chkContinuous.Checked = False

        OldVal = Val(SetCur(MATHINPUT))
        NewVal = Val(SetCur(MATHINPUT)) - Val(SetInc(MATHINPUT))
        If NewVal >= Val(SetMin(MATHINPUT)) Then
            SetCur(MATHINPUT) = Conversion.Str(NewVal)
        Else
            SetCur(MATHINPUT) = SetMin(MATHINPUT)
        End If
        DispTxt(MATHINPUT)
    End Sub

    Private Sub spnMath_UpButtonClicked(sender As Object, e As EventArgs) Handles spnMath.UpButtonClicked
        Dim OldVal As Double
        Dim NewVal As Double

        Me.chkContinuous.Checked = False

        OldVal = Val(SetCur(MATHINPUT))
        NewVal = Val(SetCur(MATHINPUT)) + Val(SetInc(MATHINPUT))
        If NewVal <= Val(SetMax(MATHINPUT)) Then
            SetCur(MATHINPUT) = Conversion.Str(NewVal)
        Else
            SetCur(MATHINPUT) = SetMax(MATHINPUT)
        End If
        DispTxt(MATHINPUT)
    End Sub

    Private Sub spnTriggerDelay_DownButtonClicked(sender As Object, e As EventArgs) Handles spnTriggerDelay.DownButtonClicked

        Me.chkContinuous.Checked = False

        Spin10Pct(txtTriggerDelay, "Down", TRIGGER_DELAY)
    End Sub

    Private Sub spnTriggerDelay_UpButtonClicked(sender As Object, e As EventArgs) Handles spnTriggerDelay.UpButtonClicked

        Me.chkContinuous.Checked = False

        Spin10Pct(txtTriggerDelay, "Up", TRIGGER_DELAY)
    End Sub

    Private Sub spnTriggerCount_DownButtonClicked(sender As Object, e As EventArgs) Handles spnTriggerCount.DownButtonClicked
        Dim OldVal As Double
        Dim NewVal As Double

        Me.chkContinuous.Checked = False

        OldVal = Val(SetCur(TRIG_COUN))
        NewVal = Val(SetCur(TRIG_COUN)) - Val(SetInc(TRIG_COUN))
        If NewVal >= Val(SetMin(TRIG_COUN)) Then
            SetCur(TRIG_COUN) = Conversion.Str(NewVal)
        Else
            SetCur(TRIG_COUN) = SetMin(TRIG_COUN)
        End If
        DispTxt(TRIG_COUN)
    End Sub

    Private Sub spnTriggerCount_UpButtonClicked(sender As Object, e As EventArgs) Handles spnTriggerCount.UpButtonClicked
        Dim OldVal As Double
        Dim NewVal As Double

        Me.chkContinuous.Checked = False

        OldVal = Val(SetCur(TRIG_COUN))
        NewVal = Val(SetCur(TRIG_COUN)) + Val(SetInc(TRIG_COUN))
        If NewVal <= Val(SetMax(TRIG_COUN)) Then
            SetCur(TRIG_COUN) = Conversion.Str(NewVal)
        Else
            SetCur(TRIG_COUN) = SetMax(TRIG_COUN)
        End If
        DispTxt(TRIG_COUN)
    End Sub

    Private Sub spnSampleCount_DownButtonClicked(sender As Object, e As EventArgs) Handles spnSampleCount.DownButtonClicked
        Dim OldVal As Double
        Dim NewVal As Double

        Me.chkContinuous.Checked = False

        OldVal = Val(SetCur(SAMP_COUN))
        NewVal = Val(SetCur(SAMP_COUN)) - Val(SetInc(SAMP_COUN))
        If NewVal >= Val(SetMin(SAMP_COUN)) Then
            SetCur(SAMP_COUN) = Conversion.Str(NewVal)
        Else
            SetCur(SAMP_COUN) = SetMin(SAMP_COUN)
        End If
        DispTxt(SAMP_COUN)
    End Sub

    Private Sub spnSampleCount_UpButtonClicked(sender As Object, e As EventArgs) Handles spnSampleCount.UpButtonClicked
        Dim OldVal As Double
        Dim NewVal As Double

        Me.chkContinuous.Checked = False

        OldVal = Val(SetCur(SAMP_COUN))
        NewVal = Val(SetCur(SAMP_COUN)) + Val(SetInc(SAMP_COUN))
        If NewVal <= Val(SetMax(SAMP_COUN)) Then
            SetCur(SAMP_COUN) = Conversion.Str(NewVal)
        Else
            SetCur(SAMP_COUN) = SetMax(SAMP_COUN)
        End If
        DispTxt(SAMP_COUN)
    End Sub

    Private tabUserOptions_PreviousTab As Integer
    Private Sub tabUserOptions_Deselecting(ByVal sender As System.Object, ByVal e As TabControlCancelEventArgs) Handles tabUserOptions.Deselecting
        tabUserOptions_PreviousTab = e.TabPageIndex
    End Sub

    Private Sub tabUserOptions_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tabUserOptions.SelectedIndexChanged
        Dim PreviousTab As Integer = tabUserOptions_PreviousTab

        Select Case tabUserOptions.TabPages(tabUserOptions.SelectedIndex).Text
            Case "ATLAS"
                Atlas_SFP.Parent_Object = Me
            Case Else
                If Panel_Conifg.DebugMode Then
                    ConfigGetCurrent()
                End If
        End Select
    End Sub

    Private Sub tbrDmmFunctions_ButtonClick(ByVal sender As Object, ByVal e As ToolBarButtonClickEventArgs) Handles tbrDmmFunctions.ButtonClick
        tbrDmmFunctions.Focus()
        Me.chkContinuous.Checked = False
        txtDmmDisplay.Text = "Ready"
        Select Case e.Button.Name
            Case "DCV"
                InstrumentMode = 1
                ChangeInstrumentMode()
            Case "ACV"
                InstrumentMode = 2
                ChangeInstrumentMode()
            Case "DCVR"
                InstrumentMode = 3
                ChangeInstrumentMode()
            Case "DCC"
                InstrumentMode = 4
                ChangeInstrumentMode()
            Case "ACC"
                InstrumentMode = 5
                ChangeInstrumentMode()
            Case "_2WR"
                InstrumentMode = 6
                ChangeInstrumentMode()
            Case "FREQ"
                InstrumentMode = 7
                ChangeInstrumentMode()
            Case "PER"
                InstrumentMode = 8
                ChangeInstrumentMode()
            Case "_4WR"
                InstrumentMode = 9
                ChangeInstrumentMode()
        End Select
        For Each Button In tbrDmmFunctions.Buttons
            Button.pushed = False
        Next
        e.Button.Pushed = True
    End Sub

    Private Sub txtMath_TextChanged(sender As Object, e As EventArgs) Handles txtMath.TextChanged
        If Not sender.Created() Then Exit Sub
        Dim NewVal As Single
        Dim Unit As String = ""

        NewVal = Val(sender.Text) * Val(SetUOM(MATHINPUT))

        'Update for restore
        If Val(SetCur(MATHINPUT)) <> NewVal Then
            SetCur(MATHINPUT) = NewVal
        End If
    End Sub

    Private Sub txtTriggerDelay_TextChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles txtTriggerDelay.TextChanged
        If Not sender.Created() Then Exit Sub
        Dim NewVal As Single
        Dim Unit As String

        NewVal = Val(txtTriggerDelay.Text)
        Unit = Strings.Mid(txtTriggerDelay.Text, InStr(txtTriggerDelay.Text, " ") + 1, 1)
        Select Case Unit
            Case "m"
                NewVal *= 0.001
            Case Strings.Chr(181)
                NewVal *= 0.000001
            Case "n"
                NewVal *= 0.000000001
        End Select

        'Update for restore
        If Val(SetCur(TRIGGER_DELAY)) <> NewVal Then
            SetCur(TRIGGER_DELAY) = NewVal
        End If
    End Sub

    Private Sub txtTriggerCount_TextChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles txtTriggerCount.TextChanged
        If Not sender.Created() Then Exit Sub
        Dim NewVal As Single

        NewVal = Val(txtTriggerCount.Text) * Val(SetUOM(TRIG_COUN))

        If Me.cboTriggerSource.SelectedIndex > 0 Then TRIGGER = TranslateTriggerChoice(Me.cboTriggerSource.Text)
        'Update for restore
        If Val(SetCur(TRIG_COUN)) <> NewVal Then
            SetCur(TRIG_COUN) = NewVal
        End If
    End Sub

    Private Sub txtSampleCount_TextChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles txtSampleCount.TextChanged
        If Not sender.Created() Then Exit Sub
        Dim NewVal As Single

        NewVal = Val(txtSampleCount.Text) * Val(SetUOM(SAMP_COUN))

        If Me.cboTriggerSource.SelectedIndex > 0 Then
            SAMPLE = SetCmd(SAMP_COUN) & " " & txtSampleCount.Text
        Else
            SAMPLE = ""
        End If

        'Update for restore
        If Val(SetCur(SAMP_COUN)) <> NewVal Then
            SetCur(SAMP_COUN) = NewVal
        End If
    End Sub

    Private Sub txtMath_Click(ByVal sender As Object, ByVal e As EventArgs) Handles txtMath.Click
        txtMath.SelectionStart = 0
        txtMath.SelectionLength = Len(txtMath.Text)
    End Sub

    Private Sub txtTriggerDelay_Click(ByVal sender As Object, ByVal e As EventArgs) Handles txtTriggerDelay.Click
        txtTriggerDelay.SelectionStart = 0
        txtTriggerDelay.SelectionLength = Len(txtTriggerDelay.Text)
    End Sub

    Private Sub txtTriggerCount_Click(ByVal sender As Object, ByVal e As EventArgs) Handles txtTriggerCount.Click
        txtTriggerCount.SelectionStart = 0
        txtTriggerCount.SelectionLength = Len(txtTriggerCount.Text)
    End Sub

    Private Sub txtSampleCount_Click(ByVal sender As Object, ByVal e As EventArgs) Handles txtSampleCount.Click
        txtSampleCount.SelectionStart = 0
        txtSampleCount.SelectionLength = Len(txtSampleCount.Text)
    End Sub

    Private Sub txtMath_GotFocus(sender As Object, e As EventArgs) Handles txtMath.GotFocus
        RangeDisplay = True
        SendStatusBarMessage(SetRngMsg(MATHINPUT))
    End Sub

    Private Sub txtTriggerDelay_GotFocus(sender As Object, e As EventArgs) Handles txtTriggerDelay.GotFocus
        RangeDisplay = True
        SendStatusBarMessage(SetRngMsg(TRIGGER_DELAY))
    End Sub

    Private Sub txtTriggerCount_GotFocus(ByVal sender As Object, ByVal e As EventArgs) Handles txtTriggerCount.GotFocus
        RangeDisplay = True
        SendStatusBarMessage(SetRngMsg(TRIG_COUN))
    End Sub

    Private Sub txtSampleCount_GotFocus(ByVal sender As Object, ByVal e As EventArgs) Handles txtSampleCount.GotFocus
        RangeDisplay = True
        SendStatusBarMessage(SetRngMsg(SAMP_COUN))
    End Sub

    Private Sub txtMath_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles txtMath.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)

        Me.chkContinuous.Checked = False

        If KeyAscii = 13 Or KeyAscii = 9 Then
            KeyAscii = 0
            tabUserOptions.Focus()
        ElseIf KeyAscii = 27 Then
            KeyAscii = 0
            txtMath.Text = Conversion.Str(Val(SetCur(1)) / Val(SetUOM(1)))
            tabUserOptions.Focus()
        End If

        e.KeyChar = Chr(KeyAscii)
    End Sub

    Private Sub txtTriggerDelay_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles txtTriggerDelay.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)

        Me.chkContinuous.Checked = False

        If KeyAscii = 13 Or KeyAscii = 9 Then
            KeyAscii = 0
            tabUserOptions.Focus()
        ElseIf KeyAscii = 27 Then
            KeyAscii = 0
            txtTriggerDelay.Text = Conversion.Str(Val(SetCur(2)) / Val(SetUOM(2)))
            tabUserOptions.Focus()
        End If

        e.KeyChar = Chr(KeyAscii)
    End Sub

    Private Sub txtTriggerCount_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles txtTriggerCount.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)

        Me.chkContinuous.Checked = False

        If KeyAscii = 13 Or KeyAscii = 9 Then
            KeyAscii = 0
            tabUserOptions.Focus()
        ElseIf KeyAscii = 27 Then
            KeyAscii = 0
            txtTriggerCount.Text = Conversion.Str(Val(SetCur(3)) / Val(SetUOM(3)))
            tabUserOptions.Focus()
        End If

        e.KeyChar = Chr(KeyAscii)
    End Sub

    Private Sub txtSampleCount_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles txtSampleCount.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)

        Me.chkContinuous.Checked = False

        If KeyAscii = 13 Or KeyAscii = 9 Then
            KeyAscii = 0
            tabUserOptions.Focus()
        ElseIf KeyAscii = 27 Then
            KeyAscii = 0
            txtSampleCount.Text = Conversion.Str(Val(SetCur(4)) / Val(SetUOM(4)))
            tabUserOptions.Focus()
        End If

        e.KeyChar = Chr(KeyAscii)
    End Sub

    Private Sub txtMath_Lostfocus(sender As Object, e As EventArgs) Handles txtMath.LostFocus
        Dim Index As Short = 1 ' Math
        Dim First3 As String
        Dim NewVal As Double

        First3 = UCase(Strings.Left(txtMath.Text, 3))
        Select Case First3
            Case "MIN"
                NewVal = Val(SetMin(Index))
            Case "MAX"
                NewVal = Val(SetMax(Index))
            Case "DEF"
                NewVal = Val(SetDef(Index))

            Case Else
                NewVal = Val(txtMath.Text) '* Val(SetUOM$(Index%))
        End Select

        If NewVal < Val(SetMin(Index)) Then
            MessageBox.Show(SetRngMsg(Index), "4152A", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtMath.Focus()
            txtMath_Click(sender, e)
        ElseIf NewVal > Val(SetMax(Index)) Then
            MessageBox.Show(SetRngMsg(Index), "4152A", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtMath.Focus()
            txtMath_Click(sender, e)
        Else
            SetCur(Index) = Conversion.Str(NewVal)
            DispTxt(Index)
            RangeDisplay = False
        End If
    End Sub

    Private Sub txtTriggerDelay_LostFocus(ByVal sender As Object, ByVal e As EventArgs) Handles txtTriggerDelay.LostFocus
        Dim Index As Short = 2 ' TrigDelay
        Dim First3 As String
        Dim NewVal As Double

        First3 = UCase(Strings.Left(txtTriggerDelay.Text, 3))
        Select Case First3
            Case "MIN"
                NewVal = Val(SetMin(Index))
            Case "MAX"
                NewVal = Val(SetMax(Index))
            Case "DEF"
                NewVal = Val(SetDef(Index))

            Case Else
                NewVal = Val(txtTriggerDelay.Text) '* Val(SetUOM$(Index%))
        End Select

        If NewVal < Val(SetMin(Index)) Then
            MessageBox.Show(SetRngMsg(Index), "4152A", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtTriggerDelay.Focus()
            txtTriggerDelay_Click(sender, e)
        ElseIf NewVal > Val(SetMax(Index)) Then
            MessageBox.Show(SetRngMsg(Index), "4152A", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtTriggerDelay.Focus()
            txtTriggerDelay_Click(sender, e)
        Else
            SetCur(Index) = Conversion.Str(NewVal)
            DispTxt(Index)
            RangeDisplay = False
        End If
    End Sub

    Private Sub txtTriggerCount_LostFocus(ByVal sender As Object, ByVal e As EventArgs) Handles txtTriggerCount.LostFocus
        Dim Index As Short = 3 ' TrigCount
        Dim First3 As String
        Dim NewVal As Double

        First3 = UCase(Strings.Left(txtTriggerCount.Text, 3))
        Select Case First3
            Case "MIN"
                NewVal = Val(SetMin(Index))
            Case "MAX"
                NewVal = Val(SetMax(Index))
            Case "DEF"
                NewVal = Val(SetDef(Index))

            Case Else
                NewVal = Val(txtTriggerCount.Text) '* Val(SetUOM$(Index%))
        End Select

        If NewVal < Val(SetMin(Index)) Then
            MessageBox.Show(SetRngMsg(Index), "4152A", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtTriggerCount.Focus()
            txtTriggerCount_Click(sender, e)
        ElseIf NewVal > Val(SetMax(Index)) Then
            MessageBox.Show(SetRngMsg(Index), "4152A", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtTriggerCount.Focus()
            txtTriggerCount_Click(sender, e)
        Else
            SetCur(Index) = Conversion.Str(NewVal)
            DispTxt(Index)
            RangeDisplay = False
        End If
    End Sub

    Private Sub txtSampleCount_LostFocus(ByVal sender As Object, ByVal e As EventArgs) Handles txtSampleCount.LostFocus
        Dim Index As Short = 4 ' SampleCount

        Dim First3 As String
        Dim NewVal As Double

        First3 = UCase(Strings.Left(txtSampleCount.Text, 3))
        Select Case First3
            Case "MIN"
                NewVal = Val(SetMin(Index))
            Case "MAX"
                NewVal = Val(SetMax(Index))
            Case "DEF"
                NewVal = Val(SetDef(Index))

            Case Else
                NewVal = Val(txtSampleCount.Text) '* Val(SetUOM$(Index%))
        End Select

        If NewVal < Val(SetMin(Index)) Then
            MessageBox.Show(SetRngMsg(Index), "4152A", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtSampleCount.Focus()
            txtSampleCount_Click(sender, e)
        ElseIf NewVal > Val(SetMax(Index)) Then
            MessageBox.Show(SetRngMsg(Index), "4152A", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtSampleCount.Focus()
            txtSampleCount_Click(sender, e)
        Else
            SetCur(Index) = Conversion.Str(NewVal)
            DispTxt(Index)
            RangeDisplay = False
        End If
    End Sub
End Class