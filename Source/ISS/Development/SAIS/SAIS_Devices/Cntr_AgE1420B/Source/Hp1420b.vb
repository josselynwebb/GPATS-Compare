
Imports System.Windows.Forms
Imports Microsoft.VisualBasic

Public Class frmHPE1420B
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
    Friend WithEvents CommonDialog1 As System.Windows.Forms.PrintDialog
    Friend WithEvents dlgFileIO As System.Windows.Forms.PrintDialog
    Friend WithEvents cmdHelp As System.Windows.Forms.Button
    Friend WithEvents cmdQuit As System.Windows.Forms.Button
    Friend WithEvents cmdReset As System.Windows.Forms.Button
    Friend WithEvents tabUserOptions As System.Windows.Forms.TabControl
    Friend WithEvents tabUserOptions_Page1 As System.Windows.Forms.TabPage
    Friend WithEvents fraChannel As System.Windows.Forms.GroupBox
    Friend WithEvents optInputChannel2 As System.Windows.Forms.RadioButton
    Friend WithEvents optInputChannel1 As System.Windows.Forms.RadioButton
    Friend WithEvents fraCh1Range As System.Windows.Forms.GroupBox
    Friend WithEvents panRange As System.Windows.Forms.Panel
    Friend WithEvents txtRange As System.Windows.Forms.TextBox
    Friend WithEvents spnRange As NationalInstruments.UI.WindowsForms.NumericEdit
    Friend WithEvents chkRange As System.Windows.Forms.CheckBox
    Friend WithEvents fraAperture As System.Windows.Forms.GroupBox
    Friend WithEvents panAperture As System.Windows.Forms.Panel
    Friend WithEvents txtAperture As System.Windows.Forms.TextBox
    Friend WithEvents spnAperture As NationalInstruments.UI.WindowsForms.NumericEdit
    Friend WithEvents cmdMeasure As System.Windows.Forms.Button
    Friend WithEvents fraFunction As System.Windows.Forms.GroupBox
    Friend WithEvents tbrCtFunctions As System.Windows.Forms.ToolBar
    Friend WithEvents btnFuncFrequency As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnFuncPeriod As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnFuncFrequencyRatio As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnFuncRiseTime As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnFuncFallTime As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnFuncTimeInterval As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnFuncPosPulseWidth As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnFuncNegPulseWidth As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnFuncTotalize As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnFuncPhase As System.Windows.Forms.ToolBarButton
    Friend WithEvents Button11 As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnFuncMinVoltage As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnFuncMaxVoltage As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnFuncMh As System.Windows.Forms.ToolBarButton
    Friend WithEvents chkContinuous As System.Windows.Forms.CheckBox
    Friend WithEvents fraRouting As System.Windows.Forms.GroupBox
    Friend WithEvents optRoutingCommon As System.Windows.Forms.RadioButton
    Friend WithEvents optRoutingSeparate As System.Windows.Forms.RadioButton
    Friend WithEvents tabUserOptions_Page2 As System.Windows.Forms.TabPage
    Friend WithEvents fraCh2Impedance As System.Windows.Forms.GroupBox
    Friend WithEvents optCh2Impedance50Ohm As System.Windows.Forms.RadioButton
    Friend WithEvents optCh2Impedance1MOhm As System.Windows.Forms.RadioButton
    Friend WithEvents fraCh1Impedance As System.Windows.Forms.GroupBox
    Friend WithEvents optCh1Impedance50Ohm As System.Windows.Forms.RadioButton
    Friend WithEvents optCh1Impedance1MOhm As System.Windows.Forms.RadioButton
    Friend WithEvents fraCh1Coupling As System.Windows.Forms.GroupBox
    Friend WithEvents optCh1CouplingAC As System.Windows.Forms.RadioButton
    Friend WithEvents optCh1CouplingDC As System.Windows.Forms.RadioButton
    Friend WithEvents fraCh2Coupling As System.Windows.Forms.GroupBox
    Friend WithEvents optCh2CouplingAC As System.Windows.Forms.RadioButton
    Friend WithEvents optCh2CouplingDC As System.Windows.Forms.RadioButton
    Friend WithEvents fraCh2Attenuation As System.Windows.Forms.GroupBox
    Friend WithEvents optCh2Attenuation10X As System.Windows.Forms.RadioButton
    Friend WithEvents optCh2Attenuation1X As System.Windows.Forms.RadioButton
    Friend WithEvents fraCh1Attenuation As System.Windows.Forms.GroupBox
    Friend WithEvents optCh1Attenuation10X As System.Windows.Forms.RadioButton
    Friend WithEvents optCh1Attenuation1X As System.Windows.Forms.RadioButton
    Friend WithEvents tabUserOptions_Page3 As System.Windows.Forms.TabPage
    Friend WithEvents fraTriggerOutput As System.Windows.Forms.GroupBox
    Friend WithEvents cboTriggerOutput As System.Windows.Forms.ComboBox
    Friend WithEvents optTriggerOutputOn As System.Windows.Forms.RadioButton
    Friend WithEvents optTriggerOutputOff As System.Windows.Forms.RadioButton
    Friend WithEvents FrameChannel1TriggerSetup As System.Windows.Forms.GroupBox
    Friend WithEvents fraCh1Hysteresis As System.Windows.Forms.GroupBox
    Friend WithEvents optCh1HysteresisDef As System.Windows.Forms.RadioButton
    Friend WithEvents optCh1HysteresisMin As System.Windows.Forms.RadioButton
    Friend WithEvents optCh1HysteresisMax As System.Windows.Forms.RadioButton
    Friend WithEvents fraCh1AbsoluteTrigger As System.Windows.Forms.GroupBox
    Friend WithEvents panCh1AbsoluteTrigger As System.Windows.Forms.Panel
    Friend WithEvents txtCh1AbsoluteTrigger As System.Windows.Forms.TextBox
    Friend WithEvents spnCh1AbsoluteTrigger As NationalInstruments.UI.WindowsForms.NumericEdit
    Friend WithEvents fraCh1TriggerSlope As System.Windows.Forms.GroupBox
    Friend WithEvents optCh1TriggerSlopeNegative As System.Windows.Forms.RadioButton
    Friend WithEvents optCh1TriggerSlopePositive As System.Windows.Forms.RadioButton
    Friend WithEvents fraCh1RelativeTrigger As System.Windows.Forms.GroupBox
    Friend WithEvents panCh1RelativeTrigger As System.Windows.Forms.Panel
    Friend WithEvents txtCh1RelativeTrigger As System.Windows.Forms.TextBox
    Friend WithEvents spnCh1RelativeTrigger As NationalInstruments.UI.WindowsForms.NumericEdit
    Friend WithEvents fraCh1TriggeringMethod As System.Windows.Forms.GroupBox
    Friend WithEvents chkCh1AbsTrigAuto As System.Windows.Forms.CheckBox
    Friend WithEvents FrameChannel2TriggerSetup As System.Windows.Forms.GroupBox
    Friend WithEvents fraCh2Hysteresis As System.Windows.Forms.GroupBox
    Friend WithEvents optCh2HysteresisDef As System.Windows.Forms.RadioButton
    Friend WithEvents optCh2HysteresisMin As System.Windows.Forms.RadioButton
    Friend WithEvents optCh2HysteresisMax As System.Windows.Forms.RadioButton
    Friend WithEvents fraCh2TriggerSlope As System.Windows.Forms.GroupBox
    Friend WithEvents optCh2TriggerSlopeNegative As System.Windows.Forms.RadioButton
    Friend WithEvents optCh2TriggerSlopePositive As System.Windows.Forms.RadioButton
    Friend WithEvents fraCh2AbsoluteTrigger As System.Windows.Forms.GroupBox
    Friend WithEvents panCh2AbsoluteTrigger As System.Windows.Forms.Panel
    Friend WithEvents txtCh2AbsoluteTrigger As System.Windows.Forms.TextBox
    Friend WithEvents spnCh2AbsoluteTrigger As NationalInstruments.UI.WindowsForms.NumericEdit
    Friend WithEvents fraCh2RelativeTrigger As System.Windows.Forms.GroupBox
    Friend WithEvents panCh2RelativeTrigger As System.Windows.Forms.Panel
    Friend WithEvents txtCh2RelativeTrigger As System.Windows.Forms.TextBox
    Friend WithEvents spnCh2RelativeTrigger As NationalInstruments.UI.WindowsForms.NumericEdit
    Friend WithEvents fraCh2TriggeringMethod As System.Windows.Forms.GroupBox
    Friend WithEvents chkCh2AbsTrigAuto As System.Windows.Forms.CheckBox
    Friend WithEvents tabUserOptions_Page4 As System.Windows.Forms.TabPage
    Friend WithEvents fraArmStartLevel As System.Windows.Forms.GroupBox
    Friend WithEvents optArmStartLevelGND As System.Windows.Forms.RadioButton
    Friend WithEvents optArmStartLevelECL As System.Windows.Forms.RadioButton
    Friend WithEvents optArmStartLevelTTL As System.Windows.Forms.RadioButton
    Friend WithEvents fraArmStopLevel As System.Windows.Forms.GroupBox
    Friend WithEvents optArmStopLevelGND As System.Windows.Forms.RadioButton
    Friend WithEvents optArmStopLevelECL As System.Windows.Forms.RadioButton
    Friend WithEvents optArmStopLevelTTL As System.Windows.Forms.RadioButton
    Friend WithEvents fraArmStopSlope As System.Windows.Forms.GroupBox
    Friend WithEvents optArmStopSlopeNegative As System.Windows.Forms.RadioButton
    Friend WithEvents optArmStopSlopePositive As System.Windows.Forms.RadioButton
    Friend WithEvents fraArmStartSlope As System.Windows.Forms.GroupBox
    Friend WithEvents optArmStartSlopeNegative As System.Windows.Forms.RadioButton
    Friend WithEvents optArmStartSlopePositive As System.Windows.Forms.RadioButton
    Friend WithEvents fraArmStartSource As System.Windows.Forms.GroupBox
    Friend WithEvents cboArmStartSource As System.Windows.Forms.ComboBox
    Friend WithEvents fraArmStopSource As System.Windows.Forms.GroupBox
    Friend WithEvents cboArmStopSource As System.Windows.Forms.ComboBox
    Friend WithEvents cmdArmStart As System.Windows.Forms.Button
    Friend WithEvents cmdArmStop As System.Windows.Forms.Button
    Friend WithEvents tabUserOptions_Page5 As System.Windows.Forms.TabPage
    Friend WithEvents fraAcquisitionTimeout As System.Windows.Forms.GroupBox
    Friend WithEvents optAcquisitionTimeoutOff As System.Windows.Forms.RadioButton
    Friend WithEvents optAcquisitionTimeoutOn As System.Windows.Forms.RadioButton
    Friend WithEvents optAcquisitionTimeoutStart As System.Windows.Forms.RadioButton
    Friend WithEvents cmdSelfTest As System.Windows.Forms.Button
    Friend WithEvents cmdAbout As System.Windows.Forms.Button
    Friend WithEvents fraTiDelay As System.Windows.Forms.GroupBox
    Friend WithEvents panTiDelay As System.Windows.Forms.Panel
    Friend WithEvents txtTiDelay As System.Windows.Forms.TextBox
    Friend WithEvents spnTiDelay As NationalInstruments.UI.WindowsForms.NumericEdit
    Friend WithEvents chkTIDelay As System.Windows.Forms.CheckBox
    Friend WithEvents fraTiGateAvg As System.Windows.Forms.GroupBox
    Friend WithEvents optTiGateAvgOff As System.Windows.Forms.RadioButton
    Friend WithEvents optTiGateAvgOn As System.Windows.Forms.RadioButton
    Friend WithEvents fraTimeOut As System.Windows.Forms.GroupBox
    Friend WithEvents panTimeOut As System.Windows.Forms.Panel
    Friend WithEvents txtTimeOut As System.Windows.Forms.TextBox
    Friend WithEvents spnTimeOut As NationalInstruments.UI.WindowsForms.NumericEdit
    Friend WithEvents fraTotalizeGatePolarity As System.Windows.Forms.GroupBox
    Friend WithEvents optTotalizeGatePolarityInvert As System.Windows.Forms.RadioButton
    Friend WithEvents optTotalizeGatePolarityNormal As System.Windows.Forms.RadioButton
    Friend WithEvents fraTotalizeGateState As System.Windows.Forms.GroupBox
    Friend WithEvents chkTotalizeGateState As System.Windows.Forms.CheckBox
    Friend WithEvents fraTimebaseSource As System.Windows.Forms.GroupBox
    Friend WithEvents chkOutputTimebase As System.Windows.Forms.CheckBox
    Friend WithEvents optTimebaseSourceExtern As System.Windows.Forms.RadioButton
    Friend WithEvents optTimebaseSourceIntern As System.Windows.Forms.RadioButton
    Friend WithEvents optTimebaseSourceVXIClk As System.Windows.Forms.RadioButton
    Friend WithEvents cmdUpdateTIP As System.Windows.Forms.Button
    Friend WithEvents Panel_Conifg As VIPERT_Common_Controls.Panel_Conifg
    Friend WithEvents tabUserOptions_Page6 As System.Windows.Forms.TabPage
    Friend WithEvents Atlas_SFP As VIPERT_Common_Controls.Atlas_SFP
    Friend WithEvents sbrUserInformation As System.Windows.Forms.StatusBar
    Friend WithEvents sbrUserInformation_Panel1 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents panDmmDisplay As System.Windows.Forms.Panel
    Friend WithEvents txtCtDisplay As System.Windows.Forms.TextBox
    Friend WithEvents ImageList As System.Windows.Forms.ImageList
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmHPE1420B))
        Me.CommonDialog1 = New System.Windows.Forms.PrintDialog()
        Me.dlgFileIO = New System.Windows.Forms.PrintDialog()
        Me.cmdHelp = New System.Windows.Forms.Button()
        Me.cmdQuit = New System.Windows.Forms.Button()
        Me.cmdReset = New System.Windows.Forms.Button()
        Me.tabUserOptions = New System.Windows.Forms.TabControl()
        Me.tabUserOptions_Page1 = New System.Windows.Forms.TabPage()
        Me.fraChannel = New System.Windows.Forms.GroupBox()
        Me.optInputChannel2 = New System.Windows.Forms.RadioButton()
        Me.optInputChannel1 = New System.Windows.Forms.RadioButton()
        Me.fraCh1Range = New System.Windows.Forms.GroupBox()
        Me.panRange = New System.Windows.Forms.Panel()
        Me.txtRange = New System.Windows.Forms.TextBox()
        Me.spnRange = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.chkRange = New System.Windows.Forms.CheckBox()
        Me.fraAperture = New System.Windows.Forms.GroupBox()
        Me.panAperture = New System.Windows.Forms.Panel()
        Me.txtAperture = New System.Windows.Forms.TextBox()
        Me.spnAperture = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.cmdMeasure = New System.Windows.Forms.Button()
        Me.fraFunction = New System.Windows.Forms.GroupBox()
        Me.tbrCtFunctions = New System.Windows.Forms.ToolBar()
        Me.btnFuncFrequency = New System.Windows.Forms.ToolBarButton()
        Me.btnFuncPeriod = New System.Windows.Forms.ToolBarButton()
        Me.btnFuncFrequencyRatio = New System.Windows.Forms.ToolBarButton()
        Me.btnFuncRiseTime = New System.Windows.Forms.ToolBarButton()
        Me.btnFuncFallTime = New System.Windows.Forms.ToolBarButton()
        Me.btnFuncTimeInterval = New System.Windows.Forms.ToolBarButton()
        Me.btnFuncPosPulseWidth = New System.Windows.Forms.ToolBarButton()
        Me.btnFuncNegPulseWidth = New System.Windows.Forms.ToolBarButton()
        Me.btnFuncTotalize = New System.Windows.Forms.ToolBarButton()
        Me.btnFuncPhase = New System.Windows.Forms.ToolBarButton()
        Me.ImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.chkContinuous = New System.Windows.Forms.CheckBox()
        Me.fraRouting = New System.Windows.Forms.GroupBox()
        Me.optRoutingCommon = New System.Windows.Forms.RadioButton()
        Me.optRoutingSeparate = New System.Windows.Forms.RadioButton()
        Me.tabUserOptions_Page2 = New System.Windows.Forms.TabPage()
        Me.fraCh2Impedance = New System.Windows.Forms.GroupBox()
        Me.optCh2Impedance50Ohm = New System.Windows.Forms.RadioButton()
        Me.optCh2Impedance1MOhm = New System.Windows.Forms.RadioButton()
        Me.fraCh1Impedance = New System.Windows.Forms.GroupBox()
        Me.optCh1Impedance50Ohm = New System.Windows.Forms.RadioButton()
        Me.optCh1Impedance1MOhm = New System.Windows.Forms.RadioButton()
        Me.fraCh1Coupling = New System.Windows.Forms.GroupBox()
        Me.optCh1CouplingAC = New System.Windows.Forms.RadioButton()
        Me.optCh1CouplingDC = New System.Windows.Forms.RadioButton()
        Me.fraCh2Coupling = New System.Windows.Forms.GroupBox()
        Me.optCh2CouplingAC = New System.Windows.Forms.RadioButton()
        Me.optCh2CouplingDC = New System.Windows.Forms.RadioButton()
        Me.fraCh2Attenuation = New System.Windows.Forms.GroupBox()
        Me.optCh2Attenuation10X = New System.Windows.Forms.RadioButton()
        Me.optCh2Attenuation1X = New System.Windows.Forms.RadioButton()
        Me.fraCh1Attenuation = New System.Windows.Forms.GroupBox()
        Me.optCh1Attenuation10X = New System.Windows.Forms.RadioButton()
        Me.optCh1Attenuation1X = New System.Windows.Forms.RadioButton()
        Me.tabUserOptions_Page3 = New System.Windows.Forms.TabPage()
        Me.fraTriggerOutput = New System.Windows.Forms.GroupBox()
        Me.cboTriggerOutput = New System.Windows.Forms.ComboBox()
        Me.optTriggerOutputOn = New System.Windows.Forms.RadioButton()
        Me.optTriggerOutputOff = New System.Windows.Forms.RadioButton()
        Me.FrameChannel1TriggerSetup = New System.Windows.Forms.GroupBox()
        Me.fraCh1Hysteresis = New System.Windows.Forms.GroupBox()
        Me.optCh1HysteresisDef = New System.Windows.Forms.RadioButton()
        Me.optCh1HysteresisMin = New System.Windows.Forms.RadioButton()
        Me.optCh1HysteresisMax = New System.Windows.Forms.RadioButton()
        Me.fraCh1AbsoluteTrigger = New System.Windows.Forms.GroupBox()
        Me.panCh1AbsoluteTrigger = New System.Windows.Forms.Panel()
        Me.txtCh1AbsoluteTrigger = New System.Windows.Forms.TextBox()
        Me.spnCh1AbsoluteTrigger = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.fraCh1TriggerSlope = New System.Windows.Forms.GroupBox()
        Me.optCh1TriggerSlopeNegative = New System.Windows.Forms.RadioButton()
        Me.optCh1TriggerSlopePositive = New System.Windows.Forms.RadioButton()
        Me.fraCh1RelativeTrigger = New System.Windows.Forms.GroupBox()
        Me.panCh1RelativeTrigger = New System.Windows.Forms.Panel()
        Me.txtCh1RelativeTrigger = New System.Windows.Forms.TextBox()
        Me.spnCh1RelativeTrigger = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.fraCh1TriggeringMethod = New System.Windows.Forms.GroupBox()
        Me.chkCh1AbsTrigAuto = New System.Windows.Forms.CheckBox()
        Me.FrameChannel2TriggerSetup = New System.Windows.Forms.GroupBox()
        Me.fraCh2Hysteresis = New System.Windows.Forms.GroupBox()
        Me.optCh2HysteresisDef = New System.Windows.Forms.RadioButton()
        Me.optCh2HysteresisMin = New System.Windows.Forms.RadioButton()
        Me.optCh2HysteresisMax = New System.Windows.Forms.RadioButton()
        Me.fraCh2TriggerSlope = New System.Windows.Forms.GroupBox()
        Me.optCh2TriggerSlopeNegative = New System.Windows.Forms.RadioButton()
        Me.optCh2TriggerSlopePositive = New System.Windows.Forms.RadioButton()
        Me.fraCh2AbsoluteTrigger = New System.Windows.Forms.GroupBox()
        Me.panCh2AbsoluteTrigger = New System.Windows.Forms.Panel()
        Me.txtCh2AbsoluteTrigger = New System.Windows.Forms.TextBox()
        Me.spnCh2AbsoluteTrigger = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.fraCh2RelativeTrigger = New System.Windows.Forms.GroupBox()
        Me.panCh2RelativeTrigger = New System.Windows.Forms.Panel()
        Me.txtCh2RelativeTrigger = New System.Windows.Forms.TextBox()
        Me.spnCh2RelativeTrigger = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.fraCh2TriggeringMethod = New System.Windows.Forms.GroupBox()
        Me.chkCh2AbsTrigAuto = New System.Windows.Forms.CheckBox()
        Me.tabUserOptions_Page4 = New System.Windows.Forms.TabPage()
        Me.fraArmStartLevel = New System.Windows.Forms.GroupBox()
        Me.optArmStartLevelGND = New System.Windows.Forms.RadioButton()
        Me.optArmStartLevelECL = New System.Windows.Forms.RadioButton()
        Me.optArmStartLevelTTL = New System.Windows.Forms.RadioButton()
        Me.fraArmStopLevel = New System.Windows.Forms.GroupBox()
        Me.optArmStopLevelGND = New System.Windows.Forms.RadioButton()
        Me.optArmStopLevelECL = New System.Windows.Forms.RadioButton()
        Me.optArmStopLevelTTL = New System.Windows.Forms.RadioButton()
        Me.fraArmStopSlope = New System.Windows.Forms.GroupBox()
        Me.optArmStopSlopeNegative = New System.Windows.Forms.RadioButton()
        Me.optArmStopSlopePositive = New System.Windows.Forms.RadioButton()
        Me.fraArmStartSlope = New System.Windows.Forms.GroupBox()
        Me.optArmStartSlopeNegative = New System.Windows.Forms.RadioButton()
        Me.optArmStartSlopePositive = New System.Windows.Forms.RadioButton()
        Me.fraArmStartSource = New System.Windows.Forms.GroupBox()
        Me.cboArmStartSource = New System.Windows.Forms.ComboBox()
        Me.fraArmStopSource = New System.Windows.Forms.GroupBox()
        Me.cboArmStopSource = New System.Windows.Forms.ComboBox()
        Me.cmdArmStart = New System.Windows.Forms.Button()
        Me.cmdArmStop = New System.Windows.Forms.Button()
        Me.tabUserOptions_Page5 = New System.Windows.Forms.TabPage()
        Me.fraAcquisitionTimeout = New System.Windows.Forms.GroupBox()
        Me.optAcquisitionTimeoutOff = New System.Windows.Forms.RadioButton()
        Me.optAcquisitionTimeoutOn = New System.Windows.Forms.RadioButton()
        Me.optAcquisitionTimeoutStart = New System.Windows.Forms.RadioButton()
        Me.cmdSelfTest = New System.Windows.Forms.Button()
        Me.cmdAbout = New System.Windows.Forms.Button()
        Me.fraTiDelay = New System.Windows.Forms.GroupBox()
        Me.panTiDelay = New System.Windows.Forms.Panel()
        Me.txtTiDelay = New System.Windows.Forms.TextBox()
        Me.spnTiDelay = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.chkTIDelay = New System.Windows.Forms.CheckBox()
        Me.fraTiGateAvg = New System.Windows.Forms.GroupBox()
        Me.optTiGateAvgOff = New System.Windows.Forms.RadioButton()
        Me.optTiGateAvgOn = New System.Windows.Forms.RadioButton()
        Me.fraTimeOut = New System.Windows.Forms.GroupBox()
        Me.panTimeOut = New System.Windows.Forms.Panel()
        Me.txtTimeOut = New System.Windows.Forms.TextBox()
        Me.spnTimeOut = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.fraTotalizeGatePolarity = New System.Windows.Forms.GroupBox()
        Me.optTotalizeGatePolarityInvert = New System.Windows.Forms.RadioButton()
        Me.optTotalizeGatePolarityNormal = New System.Windows.Forms.RadioButton()
        Me.fraTotalizeGateState = New System.Windows.Forms.GroupBox()
        Me.chkTotalizeGateState = New System.Windows.Forms.CheckBox()
        Me.fraTimebaseSource = New System.Windows.Forms.GroupBox()
        Me.chkOutputTimebase = New System.Windows.Forms.CheckBox()
        Me.optTimebaseSourceExtern = New System.Windows.Forms.RadioButton()
        Me.optTimebaseSourceIntern = New System.Windows.Forms.RadioButton()
        Me.optTimebaseSourceVXIClk = New System.Windows.Forms.RadioButton()
        Me.cmdUpdateTIP = New System.Windows.Forms.Button()
        Me.Panel_Conifg = New VIPERT_Common_Controls.Panel_Conifg()
        Me.tabUserOptions_Page6 = New System.Windows.Forms.TabPage()
        Me.Atlas_SFP = New VIPERT_Common_Controls.Atlas_SFP()
        Me.Button11 = New System.Windows.Forms.ToolBarButton()
        Me.btnFuncMinVoltage = New System.Windows.Forms.ToolBarButton()
        Me.btnFuncMaxVoltage = New System.Windows.Forms.ToolBarButton()
        Me.btnFuncMh = New System.Windows.Forms.ToolBarButton()
        Me.sbrUserInformation = New System.Windows.Forms.StatusBar()
        Me.sbrUserInformation_Panel1 = New System.Windows.Forms.StatusBarPanel()
        Me.panDmmDisplay = New System.Windows.Forms.Panel()
        Me.txtCtDisplay = New System.Windows.Forms.TextBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.tabUserOptions.SuspendLayout()
        Me.tabUserOptions_Page1.SuspendLayout()
        Me.fraChannel.SuspendLayout()
        Me.fraCh1Range.SuspendLayout()
        Me.panRange.SuspendLayout()
        CType(Me.spnRange, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraAperture.SuspendLayout()
        Me.panAperture.SuspendLayout()
        CType(Me.spnAperture, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraFunction.SuspendLayout()
        Me.fraRouting.SuspendLayout()
        Me.tabUserOptions_Page2.SuspendLayout()
        Me.fraCh2Impedance.SuspendLayout()
        Me.fraCh1Impedance.SuspendLayout()
        Me.fraCh1Coupling.SuspendLayout()
        Me.fraCh2Coupling.SuspendLayout()
        Me.fraCh2Attenuation.SuspendLayout()
        Me.fraCh1Attenuation.SuspendLayout()
        Me.tabUserOptions_Page3.SuspendLayout()
        Me.fraTriggerOutput.SuspendLayout()
        Me.FrameChannel1TriggerSetup.SuspendLayout()
        Me.fraCh1Hysteresis.SuspendLayout()
        Me.fraCh1AbsoluteTrigger.SuspendLayout()
        Me.panCh1AbsoluteTrigger.SuspendLayout()
        CType(Me.spnCh1AbsoluteTrigger, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraCh1TriggerSlope.SuspendLayout()
        Me.fraCh1RelativeTrigger.SuspendLayout()
        Me.panCh1RelativeTrigger.SuspendLayout()
        CType(Me.spnCh1RelativeTrigger, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraCh1TriggeringMethod.SuspendLayout()
        Me.FrameChannel2TriggerSetup.SuspendLayout()
        Me.fraCh2Hysteresis.SuspendLayout()
        Me.fraCh2TriggerSlope.SuspendLayout()
        Me.fraCh2AbsoluteTrigger.SuspendLayout()
        Me.panCh2AbsoluteTrigger.SuspendLayout()
        CType(Me.spnCh2AbsoluteTrigger, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraCh2RelativeTrigger.SuspendLayout()
        Me.panCh2RelativeTrigger.SuspendLayout()
        CType(Me.spnCh2RelativeTrigger, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraCh2TriggeringMethod.SuspendLayout()
        Me.tabUserOptions_Page4.SuspendLayout()
        Me.fraArmStartLevel.SuspendLayout()
        Me.fraArmStopLevel.SuspendLayout()
        Me.fraArmStopSlope.SuspendLayout()
        Me.fraArmStartSlope.SuspendLayout()
        Me.fraArmStartSource.SuspendLayout()
        Me.fraArmStopSource.SuspendLayout()
        Me.tabUserOptions_Page5.SuspendLayout()
        Me.fraAcquisitionTimeout.SuspendLayout()
        Me.fraTiDelay.SuspendLayout()
        Me.panTiDelay.SuspendLayout()
        CType(Me.spnTiDelay, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraTiGateAvg.SuspendLayout()
        Me.fraTimeOut.SuspendLayout()
        Me.panTimeOut.SuspendLayout()
        CType(Me.spnTimeOut, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraTotalizeGatePolarity.SuspendLayout()
        Me.fraTotalizeGateState.SuspendLayout()
        Me.fraTimebaseSource.SuspendLayout()
        Me.tabUserOptions_Page6.SuspendLayout()
        CType(Me.sbrUserInformation_Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panDmmDisplay.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdHelp
        '
        Me.cmdHelp.BackColor = System.Drawing.SystemColors.Control
        Me.cmdHelp.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdHelp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdHelp.Location = New System.Drawing.Point(334, 461)
        Me.cmdHelp.Name = "cmdHelp"
        Me.cmdHelp.Size = New System.Drawing.Size(76, 23)
        Me.cmdHelp.TabIndex = 96
        Me.cmdHelp.Text = "Help"
        Me.cmdHelp.UseVisualStyleBackColor = False
        '
        'cmdQuit
        '
        Me.cmdQuit.BackColor = System.Drawing.SystemColors.Control
        Me.cmdQuit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdQuit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdQuit.Location = New System.Drawing.Point(497, 461)
        Me.cmdQuit.Name = "cmdQuit"
        Me.cmdQuit.Size = New System.Drawing.Size(76, 23)
        Me.cmdQuit.TabIndex = 3
        Me.cmdQuit.Text = "&Quit"
        Me.cmdQuit.UseVisualStyleBackColor = False
        '
        'cmdReset
        '
        Me.cmdReset.BackColor = System.Drawing.SystemColors.Control
        Me.cmdReset.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdReset.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdReset.Location = New System.Drawing.Point(416, 461)
        Me.cmdReset.Name = "cmdReset"
        Me.cmdReset.Size = New System.Drawing.Size(76, 23)
        Me.cmdReset.TabIndex = 2
        Me.cmdReset.Text = "&Reset"
        Me.cmdReset.UseVisualStyleBackColor = False
        '
        'tabUserOptions
        '
        Me.tabUserOptions.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tabUserOptions.Controls.Add(Me.tabUserOptions_Page1)
        Me.tabUserOptions.Controls.Add(Me.tabUserOptions_Page2)
        Me.tabUserOptions.Controls.Add(Me.tabUserOptions_Page3)
        Me.tabUserOptions.Controls.Add(Me.tabUserOptions_Page4)
        Me.tabUserOptions.Controls.Add(Me.tabUserOptions_Page5)
        Me.tabUserOptions.Controls.Add(Me.tabUserOptions_Page6)
        Me.tabUserOptions.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tabUserOptions.Location = New System.Drawing.Point(8, 57)
        Me.tabUserOptions.Name = "tabUserOptions"
        Me.tabUserOptions.SelectedIndex = 0
        Me.tabUserOptions.Size = New System.Drawing.Size(600, 393)
        Me.tabUserOptions.TabIndex = 1
        '
        'tabUserOptions_Page1
        '
        Me.tabUserOptions_Page1.Controls.Add(Me.fraChannel)
        Me.tabUserOptions_Page1.Controls.Add(Me.fraCh1Range)
        Me.tabUserOptions_Page1.Controls.Add(Me.fraAperture)
        Me.tabUserOptions_Page1.Controls.Add(Me.cmdMeasure)
        Me.tabUserOptions_Page1.Controls.Add(Me.fraFunction)
        Me.tabUserOptions_Page1.Controls.Add(Me.chkContinuous)
        Me.tabUserOptions_Page1.Controls.Add(Me.fraRouting)
        Me.tabUserOptions_Page1.Location = New System.Drawing.Point(4, 22)
        Me.tabUserOptions_Page1.Name = "tabUserOptions_Page1"
        Me.tabUserOptions_Page1.Size = New System.Drawing.Size(592, 367)
        Me.tabUserOptions_Page1.TabIndex = 0
        Me.tabUserOptions_Page1.Text = "Measurement"
        '
        'fraChannel
        '
        Me.fraChannel.Controls.Add(Me.optInputChannel2)
        Me.fraChannel.Controls.Add(Me.optInputChannel1)
        Me.fraChannel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraChannel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraChannel.Location = New System.Drawing.Point(133, 4)
        Me.fraChannel.Name = "fraChannel"
        Me.fraChannel.Size = New System.Drawing.Size(99, 66)
        Me.fraChannel.TabIndex = 6
        Me.fraChannel.TabStop = False
        Me.fraChannel.Text = "Channel"
        '
        'optInputChannel2
        '
        Me.optInputChannel2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optInputChannel2.Location = New System.Drawing.Point(12, 39)
        Me.optInputChannel2.Name = "optInputChannel2"
        Me.optInputChannel2.Size = New System.Drawing.Size(81, 20)
        Me.optInputChannel2.TabIndex = 8
        Me.optInputChannel2.Text = "Input 2"
        '
        'optInputChannel1
        '
        Me.optInputChannel1.Checked = True
        Me.optInputChannel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optInputChannel1.Location = New System.Drawing.Point(12, 19)
        Me.optInputChannel1.Name = "optInputChannel1"
        Me.optInputChannel1.Size = New System.Drawing.Size(81, 20)
        Me.optInputChannel1.TabIndex = 7
        Me.optInputChannel1.TabStop = True
        Me.optInputChannel1.Text = "Input 1"
        '
        'fraCh1Range
        '
        Me.fraCh1Range.Controls.Add(Me.panRange)
        Me.fraCh1Range.Controls.Add(Me.chkRange)
        Me.fraCh1Range.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraCh1Range.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraCh1Range.Location = New System.Drawing.Point(133, 69)
        Me.fraCh1Range.Name = "fraCh1Range"
        Me.fraCh1Range.Size = New System.Drawing.Size(209, 74)
        Me.fraCh1Range.TabIndex = 9
        Me.fraCh1Range.TabStop = False
        Me.fraCh1Range.Text = "Range"
        '
        'panRange
        '
        Me.panRange.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.panRange.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panRange.Controls.Add(Me.txtRange)
        Me.panRange.Controls.Add(Me.spnRange)
        Me.panRange.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panRange.Location = New System.Drawing.Point(8, 24)
        Me.panRange.Name = "panRange"
        Me.panRange.Size = New System.Drawing.Size(158, 22)
        Me.panRange.TabIndex = 72
        Me.panRange.Visible = False
        '
        'txtRange
        '
        Me.txtRange.BackColor = System.Drawing.SystemColors.Window
        Me.txtRange.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtRange.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRange.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRange.Location = New System.Drawing.Point(2, 2)
        Me.txtRange.Name = "txtRange"
        Me.txtRange.Size = New System.Drawing.Size(134, 13)
        Me.txtRange.TabIndex = 73
        Me.txtRange.Text = "99.9"
        Me.txtRange.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'spnRange
        '
        Me.spnRange.Location = New System.Drawing.Point(140, -1)
        Me.spnRange.Name = "spnRange"
        Me.spnRange.Range = New NationalInstruments.UI.Range(0.0R, 10.0R)
        Me.spnRange.Size = New System.Drawing.Size(16, 20)
        Me.spnRange.TabIndex = 74
        '
        'chkRange
        '
        Me.chkRange.Checked = True
        Me.chkRange.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkRange.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRange.Location = New System.Drawing.Point(8, 49)
        Me.chkRange.Name = "chkRange"
        Me.chkRange.Size = New System.Drawing.Size(114, 21)
        Me.chkRange.TabIndex = 10
        Me.chkRange.Text = "Auto Range"
        '
        'fraAperture
        '
        Me.fraAperture.Controls.Add(Me.panAperture)
        Me.fraAperture.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraAperture.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraAperture.Location = New System.Drawing.Point(133, 142)
        Me.fraAperture.Name = "fraAperture"
        Me.fraAperture.Size = New System.Drawing.Size(209, 74)
        Me.fraAperture.TabIndex = 11
        Me.fraAperture.TabStop = False
        Me.fraAperture.Text = " Aperture"
        '
        'panAperture
        '
        Me.panAperture.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.panAperture.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panAperture.Controls.Add(Me.txtAperture)
        Me.panAperture.Controls.Add(Me.spnAperture)
        Me.panAperture.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panAperture.Location = New System.Drawing.Point(8, 24)
        Me.panAperture.Name = "panAperture"
        Me.panAperture.Size = New System.Drawing.Size(158, 22)
        Me.panAperture.TabIndex = 75
        '
        'txtAperture
        '
        Me.txtAperture.BackColor = System.Drawing.SystemColors.Window
        Me.txtAperture.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtAperture.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAperture.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAperture.Location = New System.Drawing.Point(2, 2)
        Me.txtAperture.Name = "txtAperture"
        Me.txtAperture.Size = New System.Drawing.Size(134, 13)
        Me.txtAperture.TabIndex = 76
        Me.txtAperture.Text = "99.9"
        Me.txtAperture.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'spnAperture
        '
        Me.spnAperture.Location = New System.Drawing.Point(140, -1)
        Me.spnAperture.Name = "spnAperture"
        Me.spnAperture.Size = New System.Drawing.Size(16, 20)
        Me.spnAperture.TabIndex = 77
        '
        'cmdMeasure
        '
        Me.cmdMeasure.BackColor = System.Drawing.SystemColors.Control
        Me.cmdMeasure.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdMeasure.Location = New System.Drawing.Point(464, 324)
        Me.cmdMeasure.Name = "cmdMeasure"
        Me.cmdMeasure.Size = New System.Drawing.Size(76, 23)
        Me.cmdMeasure.TabIndex = 79
        Me.cmdMeasure.Text = "&Measure"
        Me.cmdMeasure.UseVisualStyleBackColor = False
        '
        'fraFunction
        '
        Me.fraFunction.Controls.Add(Me.tbrCtFunctions)
        Me.fraFunction.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraFunction.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraFunction.Location = New System.Drawing.Point(8, 4)
        Me.fraFunction.Name = "fraFunction"
        Me.fraFunction.Size = New System.Drawing.Size(118, 211)
        Me.fraFunction.TabIndex = 4
        Me.fraFunction.TabStop = False
        Me.fraFunction.Text = "Function"
        '
        'tbrCtFunctions
        '
        Me.tbrCtFunctions.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.btnFuncFrequency, Me.btnFuncPeriod, Me.btnFuncFrequencyRatio, Me.btnFuncRiseTime, Me.btnFuncFallTime, Me.btnFuncTimeInterval, Me.btnFuncPosPulseWidth, Me.btnFuncNegPulseWidth, Me.btnFuncTotalize, Me.btnFuncPhase})
        Me.tbrCtFunctions.ButtonSize = New System.Drawing.Size(24, 24)
        Me.tbrCtFunctions.Dock = System.Windows.Forms.DockStyle.None
        Me.tbrCtFunctions.DropDownArrows = True
        Me.tbrCtFunctions.ImageList = Me.ImageList
        Me.tbrCtFunctions.Location = New System.Drawing.Point(16, 24)
        Me.tbrCtFunctions.Name = "tbrCtFunctions"
        Me.tbrCtFunctions.ShowToolTips = True
        Me.tbrCtFunctions.Size = New System.Drawing.Size(74, 156)
        Me.tbrCtFunctions.TabIndex = 5
        '
        'btnFuncFrequency
        '
        Me.btnFuncFrequency.ImageIndex = 0
        Me.btnFuncFrequency.Name = "btnFuncFrequency"
        Me.btnFuncFrequency.Pushed = True
        Me.btnFuncFrequency.ToolTipText = "Frequency"
        '
        'btnFuncPeriod
        '
        Me.btnFuncPeriod.ImageIndex = 1
        Me.btnFuncPeriod.Name = "btnFuncPeriod"
        Me.btnFuncPeriod.ToolTipText = "Period"
        '
        'btnFuncFrequencyRatio
        '
        Me.btnFuncFrequencyRatio.ImageIndex = 2
        Me.btnFuncFrequencyRatio.Name = "btnFuncFrequencyRatio"
        Me.btnFuncFrequencyRatio.ToolTipText = "Frequency Ratio"
        '
        'btnFuncRiseTime
        '
        Me.btnFuncRiseTime.ImageIndex = 3
        Me.btnFuncRiseTime.Name = "btnFuncRiseTime"
        Me.btnFuncRiseTime.ToolTipText = "Rise Time"
        '
        'btnFuncFallTime
        '
        Me.btnFuncFallTime.ImageIndex = 4
        Me.btnFuncFallTime.Name = "btnFuncFallTime"
        Me.btnFuncFallTime.ToolTipText = "Fall Time"
        '
        'btnFuncTimeInterval
        '
        Me.btnFuncTimeInterval.ImageIndex = 5
        Me.btnFuncTimeInterval.Name = "btnFuncTimeInterval"
        Me.btnFuncTimeInterval.ToolTipText = "Time Interval"
        '
        'btnFuncPosPulseWidth
        '
        Me.btnFuncPosPulseWidth.ImageIndex = 6
        Me.btnFuncPosPulseWidth.Name = "btnFuncPosPulseWidth"
        Me.btnFuncPosPulseWidth.ToolTipText = "+ Pulse Width"
        '
        'btnFuncNegPulseWidth
        '
        Me.btnFuncNegPulseWidth.ImageIndex = 7
        Me.btnFuncNegPulseWidth.Name = "btnFuncNegPulseWidth"
        Me.btnFuncNegPulseWidth.ToolTipText = "- Pulse Width"
        '
        'btnFuncTotalize
        '
        Me.btnFuncTotalize.ImageIndex = 8
        Me.btnFuncTotalize.Name = "btnFuncTotalize"
        Me.btnFuncTotalize.ToolTipText = "Totalize"
        '
        'btnFuncPhase
        '
        Me.btnFuncPhase.ImageIndex = 9
        Me.btnFuncPhase.Name = "btnFuncPhase"
        Me.btnFuncPhase.ToolTipText = "Phase"
        '
        'ImageList
        '
        Me.ImageList.ImageStream = CType(resources.GetObject("ImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList.TransparentColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ImageList.Images.SetKeyName(0, "")
        Me.ImageList.Images.SetKeyName(1, "")
        Me.ImageList.Images.SetKeyName(2, "")
        Me.ImageList.Images.SetKeyName(3, "")
        Me.ImageList.Images.SetKeyName(4, "")
        Me.ImageList.Images.SetKeyName(5, "")
        Me.ImageList.Images.SetKeyName(6, "")
        Me.ImageList.Images.SetKeyName(7, "")
        Me.ImageList.Images.SetKeyName(8, "")
        Me.ImageList.Images.SetKeyName(9, "")
        '
        'chkContinuous
        '
        Me.chkContinuous.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkContinuous.Location = New System.Drawing.Point(464, 299)
        Me.chkContinuous.Name = "chkContinuous"
        Me.chkContinuous.Size = New System.Drawing.Size(106, 19)
        Me.chkContinuous.TabIndex = 78
        Me.chkContinuous.Text = "Continuous"
        '
        'fraRouting
        '
        Me.fraRouting.Controls.Add(Me.optRoutingCommon)
        Me.fraRouting.Controls.Add(Me.optRoutingSeparate)
        Me.fraRouting.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraRouting.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraRouting.Location = New System.Drawing.Point(239, 4)
        Me.fraRouting.Name = "fraRouting"
        Me.fraRouting.Size = New System.Drawing.Size(99, 66)
        Me.fraRouting.TabIndex = 90
        Me.fraRouting.TabStop = False
        Me.fraRouting.Text = "Routing"
        '
        'optRoutingCommon
        '
        Me.optRoutingCommon.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optRoutingCommon.Location = New System.Drawing.Point(12, 39)
        Me.optRoutingCommon.Name = "optRoutingCommon"
        Me.optRoutingCommon.Size = New System.Drawing.Size(81, 20)
        Me.optRoutingCommon.TabIndex = 91
        Me.optRoutingCommon.Text = "Comm."
        '
        'optRoutingSeparate
        '
        Me.optRoutingSeparate.Checked = True
        Me.optRoutingSeparate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optRoutingSeparate.Location = New System.Drawing.Point(13, 19)
        Me.optRoutingSeparate.Name = "optRoutingSeparate"
        Me.optRoutingSeparate.Size = New System.Drawing.Size(81, 20)
        Me.optRoutingSeparate.TabIndex = 92
        Me.optRoutingSeparate.TabStop = True
        Me.optRoutingSeparate.Text = "Sep."
        '
        'tabUserOptions_Page2
        '
        Me.tabUserOptions_Page2.Controls.Add(Me.fraCh2Impedance)
        Me.tabUserOptions_Page2.Controls.Add(Me.fraCh1Impedance)
        Me.tabUserOptions_Page2.Controls.Add(Me.fraCh1Coupling)
        Me.tabUserOptions_Page2.Controls.Add(Me.fraCh2Coupling)
        Me.tabUserOptions_Page2.Controls.Add(Me.fraCh2Attenuation)
        Me.tabUserOptions_Page2.Controls.Add(Me.fraCh1Attenuation)
        Me.tabUserOptions_Page2.Location = New System.Drawing.Point(4, 22)
        Me.tabUserOptions_Page2.Name = "tabUserOptions_Page2"
        Me.tabUserOptions_Page2.Size = New System.Drawing.Size(592, 367)
        Me.tabUserOptions_Page2.TabIndex = 1
        Me.tabUserOptions_Page2.Text = "Input"
        '
        'fraCh2Impedance
        '
        Me.fraCh2Impedance.Controls.Add(Me.optCh2Impedance50Ohm)
        Me.fraCh2Impedance.Controls.Add(Me.optCh2Impedance1MOhm)
        Me.fraCh2Impedance.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraCh2Impedance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraCh2Impedance.Location = New System.Drawing.Point(195, 138)
        Me.fraCh2Impedance.Name = "fraCh2Impedance"
        Me.fraCh2Impedance.Size = New System.Drawing.Size(165, 61)
        Me.fraCh2Impedance.TabIndex = 27
        Me.fraCh2Impedance.TabStop = False
        Me.fraCh2Impedance.Text = "Input 2 Impedance"
        '
        'optCh2Impedance50Ohm
        '
        Me.optCh2Impedance50Ohm.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optCh2Impedance50Ohm.Location = New System.Drawing.Point(12, 40)
        Me.optCh2Impedance50Ohm.Name = "optCh2Impedance50Ohm"
        Me.optCh2Impedance50Ohm.Size = New System.Drawing.Size(88, 19)
        Me.optCh2Impedance50Ohm.TabIndex = 29
        Me.optCh2Impedance50Ohm.Text = "50 Ohm"
        '
        'optCh2Impedance1MOhm
        '
        Me.optCh2Impedance1MOhm.Checked = True
        Me.optCh2Impedance1MOhm.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optCh2Impedance1MOhm.Location = New System.Drawing.Point(12, 17)
        Me.optCh2Impedance1MOhm.Name = "optCh2Impedance1MOhm"
        Me.optCh2Impedance1MOhm.Size = New System.Drawing.Size(88, 26)
        Me.optCh2Impedance1MOhm.TabIndex = 28
        Me.optCh2Impedance1MOhm.TabStop = True
        Me.optCh2Impedance1MOhm.Text = "1M Ohm"
        '
        'fraCh1Impedance
        '
        Me.fraCh1Impedance.Controls.Add(Me.optCh1Impedance50Ohm)
        Me.fraCh1Impedance.Controls.Add(Me.optCh1Impedance1MOhm)
        Me.fraCh1Impedance.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraCh1Impedance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraCh1Impedance.Location = New System.Drawing.Point(8, 138)
        Me.fraCh1Impedance.Name = "fraCh1Impedance"
        Me.fraCh1Impedance.Size = New System.Drawing.Size(165, 61)
        Me.fraCh1Impedance.TabIndex = 24
        Me.fraCh1Impedance.TabStop = False
        Me.fraCh1Impedance.Text = "Input 1 Impedance"
        '
        'optCh1Impedance50Ohm
        '
        Me.optCh1Impedance50Ohm.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optCh1Impedance50Ohm.Location = New System.Drawing.Point(12, 40)
        Me.optCh1Impedance50Ohm.Name = "optCh1Impedance50Ohm"
        Me.optCh1Impedance50Ohm.Size = New System.Drawing.Size(88, 19)
        Me.optCh1Impedance50Ohm.TabIndex = 26
        Me.optCh1Impedance50Ohm.Text = "50 Ohm"
        '
        'optCh1Impedance1MOhm
        '
        Me.optCh1Impedance1MOhm.Checked = True
        Me.optCh1Impedance1MOhm.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optCh1Impedance1MOhm.Location = New System.Drawing.Point(12, 17)
        Me.optCh1Impedance1MOhm.Name = "optCh1Impedance1MOhm"
        Me.optCh1Impedance1MOhm.Size = New System.Drawing.Size(88, 26)
        Me.optCh1Impedance1MOhm.TabIndex = 25
        Me.optCh1Impedance1MOhm.TabStop = True
        Me.optCh1Impedance1MOhm.Text = "1M Ohm"
        '
        'fraCh1Coupling
        '
        Me.fraCh1Coupling.Controls.Add(Me.optCh1CouplingAC)
        Me.fraCh1Coupling.Controls.Add(Me.optCh1CouplingDC)
        Me.fraCh1Coupling.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraCh1Coupling.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraCh1Coupling.Location = New System.Drawing.Point(8, 69)
        Me.fraCh1Coupling.Name = "fraCh1Coupling"
        Me.fraCh1Coupling.Size = New System.Drawing.Size(165, 61)
        Me.fraCh1Coupling.TabIndex = 18
        Me.fraCh1Coupling.TabStop = False
        Me.fraCh1Coupling.Text = "Input 1 Coupling"
        '
        'optCh1CouplingAC
        '
        Me.optCh1CouplingAC.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optCh1CouplingAC.Location = New System.Drawing.Point(12, 37)
        Me.optCh1CouplingAC.Name = "optCh1CouplingAC"
        Me.optCh1CouplingAC.Size = New System.Drawing.Size(58, 19)
        Me.optCh1CouplingAC.TabIndex = 20
        Me.optCh1CouplingAC.Text = "AC"
        '
        'optCh1CouplingDC
        '
        Me.optCh1CouplingDC.Checked = True
        Me.optCh1CouplingDC.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optCh1CouplingDC.Location = New System.Drawing.Point(12, 18)
        Me.optCh1CouplingDC.Name = "optCh1CouplingDC"
        Me.optCh1CouplingDC.Size = New System.Drawing.Size(58, 19)
        Me.optCh1CouplingDC.TabIndex = 19
        Me.optCh1CouplingDC.TabStop = True
        Me.optCh1CouplingDC.Text = "DC"
        '
        'fraCh2Coupling
        '
        Me.fraCh2Coupling.Controls.Add(Me.optCh2CouplingAC)
        Me.fraCh2Coupling.Controls.Add(Me.optCh2CouplingDC)
        Me.fraCh2Coupling.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraCh2Coupling.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraCh2Coupling.Location = New System.Drawing.Point(195, 69)
        Me.fraCh2Coupling.Name = "fraCh2Coupling"
        Me.fraCh2Coupling.Size = New System.Drawing.Size(165, 61)
        Me.fraCh2Coupling.TabIndex = 21
        Me.fraCh2Coupling.TabStop = False
        Me.fraCh2Coupling.Text = "Input 2 Coupling"
        '
        'optCh2CouplingAC
        '
        Me.optCh2CouplingAC.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optCh2CouplingAC.Location = New System.Drawing.Point(12, 38)
        Me.optCh2CouplingAC.Name = "optCh2CouplingAC"
        Me.optCh2CouplingAC.Size = New System.Drawing.Size(58, 19)
        Me.optCh2CouplingAC.TabIndex = 23
        Me.optCh2CouplingAC.Text = "AC"
        '
        'optCh2CouplingDC
        '
        Me.optCh2CouplingDC.Checked = True
        Me.optCh2CouplingDC.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optCh2CouplingDC.Location = New System.Drawing.Point(12, 19)
        Me.optCh2CouplingDC.Name = "optCh2CouplingDC"
        Me.optCh2CouplingDC.Size = New System.Drawing.Size(58, 19)
        Me.optCh2CouplingDC.TabIndex = 22
        Me.optCh2CouplingDC.TabStop = True
        Me.optCh2CouplingDC.Text = "DC"
        '
        'fraCh2Attenuation
        '
        Me.fraCh2Attenuation.Controls.Add(Me.optCh2Attenuation10X)
        Me.fraCh2Attenuation.Controls.Add(Me.optCh2Attenuation1X)
        Me.fraCh2Attenuation.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraCh2Attenuation.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraCh2Attenuation.Location = New System.Drawing.Point(195, 4)
        Me.fraCh2Attenuation.Name = "fraCh2Attenuation"
        Me.fraCh2Attenuation.Size = New System.Drawing.Size(165, 61)
        Me.fraCh2Attenuation.TabIndex = 15
        Me.fraCh2Attenuation.TabStop = False
        Me.fraCh2Attenuation.Text = "Input 2 Attenuation"
        '
        'optCh2Attenuation10X
        '
        Me.optCh2Attenuation10X.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optCh2Attenuation10X.Location = New System.Drawing.Point(12, 38)
        Me.optCh2Attenuation10X.Name = "optCh2Attenuation10X"
        Me.optCh2Attenuation10X.Size = New System.Drawing.Size(58, 19)
        Me.optCh2Attenuation10X.TabIndex = 17
        Me.optCh2Attenuation10X.Text = "X 10"
        '
        'optCh2Attenuation1X
        '
        Me.optCh2Attenuation1X.Checked = True
        Me.optCh2Attenuation1X.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optCh2Attenuation1X.Location = New System.Drawing.Point(12, 18)
        Me.optCh2Attenuation1X.Name = "optCh2Attenuation1X"
        Me.optCh2Attenuation1X.Size = New System.Drawing.Size(70, 17)
        Me.optCh2Attenuation1X.TabIndex = 16
        Me.optCh2Attenuation1X.TabStop = True
        Me.optCh2Attenuation1X.Text = "X 1"
        '
        'fraCh1Attenuation
        '
        Me.fraCh1Attenuation.Controls.Add(Me.optCh1Attenuation10X)
        Me.fraCh1Attenuation.Controls.Add(Me.optCh1Attenuation1X)
        Me.fraCh1Attenuation.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraCh1Attenuation.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraCh1Attenuation.Location = New System.Drawing.Point(8, 4)
        Me.fraCh1Attenuation.Name = "fraCh1Attenuation"
        Me.fraCh1Attenuation.Size = New System.Drawing.Size(165, 61)
        Me.fraCh1Attenuation.TabIndex = 12
        Me.fraCh1Attenuation.TabStop = False
        Me.fraCh1Attenuation.Text = "Input 1 Attenuation"
        '
        'optCh1Attenuation10X
        '
        Me.optCh1Attenuation10X.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optCh1Attenuation10X.Location = New System.Drawing.Point(12, 37)
        Me.optCh1Attenuation10X.Name = "optCh1Attenuation10X"
        Me.optCh1Attenuation10X.Size = New System.Drawing.Size(58, 19)
        Me.optCh1Attenuation10X.TabIndex = 14
        Me.optCh1Attenuation10X.Text = "X 10"
        '
        'optCh1Attenuation1X
        '
        Me.optCh1Attenuation1X.Checked = True
        Me.optCh1Attenuation1X.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optCh1Attenuation1X.Location = New System.Drawing.Point(12, 18)
        Me.optCh1Attenuation1X.Name = "optCh1Attenuation1X"
        Me.optCh1Attenuation1X.Size = New System.Drawing.Size(70, 17)
        Me.optCh1Attenuation1X.TabIndex = 13
        Me.optCh1Attenuation1X.TabStop = True
        Me.optCh1Attenuation1X.Text = "X 1"
        '
        'tabUserOptions_Page3
        '
        Me.tabUserOptions_Page3.Controls.Add(Me.fraTriggerOutput)
        Me.tabUserOptions_Page3.Controls.Add(Me.FrameChannel1TriggerSetup)
        Me.tabUserOptions_Page3.Controls.Add(Me.FrameChannel2TriggerSetup)
        Me.tabUserOptions_Page3.Location = New System.Drawing.Point(4, 22)
        Me.tabUserOptions_Page3.Name = "tabUserOptions_Page3"
        Me.tabUserOptions_Page3.Size = New System.Drawing.Size(592, 367)
        Me.tabUserOptions_Page3.TabIndex = 2
        Me.tabUserOptions_Page3.Text = "Event Trigger"
        '
        'fraTriggerOutput
        '
        Me.fraTriggerOutput.Controls.Add(Me.cboTriggerOutput)
        Me.fraTriggerOutput.Controls.Add(Me.optTriggerOutputOn)
        Me.fraTriggerOutput.Controls.Add(Me.optTriggerOutputOff)
        Me.fraTriggerOutput.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraTriggerOutput.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraTriggerOutput.Location = New System.Drawing.Point(8, 77)
        Me.fraTriggerOutput.Name = "fraTriggerOutput"
        Me.fraTriggerOutput.Size = New System.Drawing.Size(153, 74)
        Me.fraTriggerOutput.TabIndex = 30
        Me.fraTriggerOutput.TabStop = False
        Me.fraTriggerOutput.Text = "Trigger Output"
        '
        'cboTriggerOutput
        '
        Me.cboTriggerOutput.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cboTriggerOutput.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTriggerOutput.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboTriggerOutput.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboTriggerOutput.Location = New System.Drawing.Point(8, 24)
        Me.cboTriggerOutput.Name = "cboTriggerOutput"
        Me.cboTriggerOutput.Size = New System.Drawing.Size(139, 21)
        Me.cboTriggerOutput.TabIndex = 31
        '
        'optTriggerOutputOn
        '
        Me.optTriggerOutputOn.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optTriggerOutputOn.Location = New System.Drawing.Point(22, 49)
        Me.optTriggerOutputOn.Name = "optTriggerOutputOn"
        Me.optTriggerOutputOn.Size = New System.Drawing.Size(51, 21)
        Me.optTriggerOutputOn.TabIndex = 33
        Me.optTriggerOutputOn.Text = "On"
        '
        'optTriggerOutputOff
        '
        Me.optTriggerOutputOff.Checked = True
        Me.optTriggerOutputOff.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optTriggerOutputOff.Location = New System.Drawing.Point(82, 49)
        Me.optTriggerOutputOff.Name = "optTriggerOutputOff"
        Me.optTriggerOutputOff.Size = New System.Drawing.Size(51, 21)
        Me.optTriggerOutputOff.TabIndex = 32
        Me.optTriggerOutputOff.TabStop = True
        Me.optTriggerOutputOff.Text = "Off"
        '
        'FrameChannel1TriggerSetup
        '
        Me.FrameChannel1TriggerSetup.BackColor = System.Drawing.SystemColors.Control
        Me.FrameChannel1TriggerSetup.Controls.Add(Me.fraCh1Hysteresis)
        Me.FrameChannel1TriggerSetup.Controls.Add(Me.fraCh1AbsoluteTrigger)
        Me.FrameChannel1TriggerSetup.Controls.Add(Me.fraCh1TriggerSlope)
        Me.FrameChannel1TriggerSetup.Controls.Add(Me.fraCh1RelativeTrigger)
        Me.FrameChannel1TriggerSetup.Controls.Add(Me.fraCh1TriggeringMethod)
        Me.FrameChannel1TriggerSetup.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FrameChannel1TriggerSetup.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.FrameChannel1TriggerSetup.Location = New System.Drawing.Point(167, 4)
        Me.FrameChannel1TriggerSetup.Name = "FrameChannel1TriggerSetup"
        Me.FrameChannel1TriggerSetup.Size = New System.Drawing.Size(185, 356)
        Me.FrameChannel1TriggerSetup.TabIndex = 99
        Me.FrameChannel1TriggerSetup.TabStop = False
        Me.FrameChannel1TriggerSetup.Text = "Channel 1"
        '
        'fraCh1Hysteresis
        '
        Me.fraCh1Hysteresis.Controls.Add(Me.optCh1HysteresisDef)
        Me.fraCh1Hysteresis.Controls.Add(Me.optCh1HysteresisMin)
        Me.fraCh1Hysteresis.Controls.Add(Me.optCh1HysteresisMax)
        Me.fraCh1Hysteresis.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraCh1Hysteresis.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraCh1Hysteresis.Location = New System.Drawing.Point(8, 16)
        Me.fraCh1Hysteresis.Name = "fraCh1Hysteresis"
        Me.fraCh1Hysteresis.Size = New System.Drawing.Size(171, 77)
        Me.fraCh1Hysteresis.TabIndex = 100
        Me.fraCh1Hysteresis.TabStop = False
        Me.fraCh1Hysteresis.Text = "Hysteresis"
        '
        'optCh1HysteresisDef
        '
        Me.optCh1HysteresisDef.Checked = True
        Me.optCh1HysteresisDef.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optCh1HysteresisDef.Location = New System.Drawing.Point(12, 35)
        Me.optCh1HysteresisDef.Name = "optCh1HysteresisDef"
        Me.optCh1HysteresisDef.Size = New System.Drawing.Size(107, 19)
        Me.optCh1HysteresisDef.TabIndex = 101
        Me.optCh1HysteresisDef.TabStop = True
        Me.optCh1HysteresisDef.Text = "Def (60mV)"
        '
        'optCh1HysteresisMin
        '
        Me.optCh1HysteresisMin.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optCh1HysteresisMin.Location = New System.Drawing.Point(12, 54)
        Me.optCh1HysteresisMin.Name = "optCh1HysteresisMin"
        Me.optCh1HysteresisMin.Size = New System.Drawing.Size(110, 19)
        Me.optCh1HysteresisMin.TabIndex = 102
        Me.optCh1HysteresisMin.Text = "Min (35mV)"
        '
        'optCh1HysteresisMax
        '
        Me.optCh1HysteresisMax.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optCh1HysteresisMax.Location = New System.Drawing.Point(12, 16)
        Me.optCh1HysteresisMax.Name = "optCh1HysteresisMax"
        Me.optCh1HysteresisMax.Size = New System.Drawing.Size(129, 19)
        Me.optCh1HysteresisMax.TabIndex = 103
        Me.optCh1HysteresisMax.Text = "Max (100mV)"
        '
        'fraCh1AbsoluteTrigger
        '
        Me.fraCh1AbsoluteTrigger.Controls.Add(Me.panCh1AbsoluteTrigger)
        Me.fraCh1AbsoluteTrigger.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraCh1AbsoluteTrigger.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraCh1AbsoluteTrigger.Location = New System.Drawing.Point(8, 212)
        Me.fraCh1AbsoluteTrigger.Name = "fraCh1AbsoluteTrigger"
        Me.fraCh1AbsoluteTrigger.Size = New System.Drawing.Size(171, 58)
        Me.fraCh1AbsoluteTrigger.TabIndex = 104
        Me.fraCh1AbsoluteTrigger.TabStop = False
        Me.fraCh1AbsoluteTrigger.Text = "Absolute Trigger Level"
        '
        'panCh1AbsoluteTrigger
        '
        Me.panCh1AbsoluteTrigger.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.panCh1AbsoluteTrigger.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panCh1AbsoluteTrigger.Controls.Add(Me.txtCh1AbsoluteTrigger)
        Me.panCh1AbsoluteTrigger.Controls.Add(Me.spnCh1AbsoluteTrigger)
        Me.panCh1AbsoluteTrigger.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panCh1AbsoluteTrigger.Location = New System.Drawing.Point(16, 27)
        Me.panCh1AbsoluteTrigger.Name = "panCh1AbsoluteTrigger"
        Me.panCh1AbsoluteTrigger.Size = New System.Drawing.Size(135, 22)
        Me.panCh1AbsoluteTrigger.TabIndex = 105
        '
        'txtCh1AbsoluteTrigger
        '
        Me.txtCh1AbsoluteTrigger.BackColor = System.Drawing.SystemColors.Window
        Me.txtCh1AbsoluteTrigger.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtCh1AbsoluteTrigger.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCh1AbsoluteTrigger.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCh1AbsoluteTrigger.Location = New System.Drawing.Point(2, 2)
        Me.txtCh1AbsoluteTrigger.Name = "txtCh1AbsoluteTrigger"
        Me.txtCh1AbsoluteTrigger.Size = New System.Drawing.Size(112, 13)
        Me.txtCh1AbsoluteTrigger.TabIndex = 106
        Me.txtCh1AbsoluteTrigger.Text = "99.9"
        Me.txtCh1AbsoluteTrigger.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'spnCh1AbsoluteTrigger
        '
        Me.spnCh1AbsoluteTrigger.Location = New System.Drawing.Point(117, -1)
        Me.spnCh1AbsoluteTrigger.Name = "spnCh1AbsoluteTrigger"
        Me.spnCh1AbsoluteTrigger.Size = New System.Drawing.Size(16, 20)
        Me.spnCh1AbsoluteTrigger.TabIndex = 107
        '
        'fraCh1TriggerSlope
        '
        Me.fraCh1TriggerSlope.Controls.Add(Me.optCh1TriggerSlopeNegative)
        Me.fraCh1TriggerSlope.Controls.Add(Me.optCh1TriggerSlopePositive)
        Me.fraCh1TriggerSlope.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraCh1TriggerSlope.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraCh1TriggerSlope.Location = New System.Drawing.Point(8, 96)
        Me.fraCh1TriggerSlope.Name = "fraCh1TriggerSlope"
        Me.fraCh1TriggerSlope.Size = New System.Drawing.Size(171, 61)
        Me.fraCh1TriggerSlope.TabIndex = 108
        Me.fraCh1TriggerSlope.TabStop = False
        Me.fraCh1TriggerSlope.Text = "Trigger Slope"
        '
        'optCh1TriggerSlopeNegative
        '
        Me.optCh1TriggerSlopeNegative.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optCh1TriggerSlopeNegative.Location = New System.Drawing.Point(12, 36)
        Me.optCh1TriggerSlopeNegative.Name = "optCh1TriggerSlopeNegative"
        Me.optCh1TriggerSlopeNegative.Size = New System.Drawing.Size(95, 22)
        Me.optCh1TriggerSlopeNegative.TabIndex = 109
        Me.optCh1TriggerSlopeNegative.Text = "Negative"
        '
        'optCh1TriggerSlopePositive
        '
        Me.optCh1TriggerSlopePositive.Checked = True
        Me.optCh1TriggerSlopePositive.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optCh1TriggerSlopePositive.Location = New System.Drawing.Point(12, 18)
        Me.optCh1TriggerSlopePositive.Name = "optCh1TriggerSlopePositive"
        Me.optCh1TriggerSlopePositive.Size = New System.Drawing.Size(82, 19)
        Me.optCh1TriggerSlopePositive.TabIndex = 110
        Me.optCh1TriggerSlopePositive.TabStop = True
        Me.optCh1TriggerSlopePositive.Text = "Positive"
        '
        'fraCh1RelativeTrigger
        '
        Me.fraCh1RelativeTrigger.Controls.Add(Me.panCh1RelativeTrigger)
        Me.fraCh1RelativeTrigger.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraCh1RelativeTrigger.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraCh1RelativeTrigger.Location = New System.Drawing.Point(8, 276)
        Me.fraCh1RelativeTrigger.Name = "fraCh1RelativeTrigger"
        Me.fraCh1RelativeTrigger.Size = New System.Drawing.Size(171, 70)
        Me.fraCh1RelativeTrigger.TabIndex = 111
        Me.fraCh1RelativeTrigger.TabStop = False
        Me.fraCh1RelativeTrigger.Text = "Relative Trigger Level"
        Me.fraCh1RelativeTrigger.Visible = False
        '
        'panCh1RelativeTrigger
        '
        Me.panCh1RelativeTrigger.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.panCh1RelativeTrigger.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panCh1RelativeTrigger.Controls.Add(Me.txtCh1RelativeTrigger)
        Me.panCh1RelativeTrigger.Controls.Add(Me.spnCh1RelativeTrigger)
        Me.panCh1RelativeTrigger.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panCh1RelativeTrigger.Location = New System.Drawing.Point(16, 28)
        Me.panCh1RelativeTrigger.Name = "panCh1RelativeTrigger"
        Me.panCh1RelativeTrigger.Size = New System.Drawing.Size(135, 22)
        Me.panCh1RelativeTrigger.TabIndex = 112
        '
        'txtCh1RelativeTrigger
        '
        Me.txtCh1RelativeTrigger.BackColor = System.Drawing.SystemColors.Window
        Me.txtCh1RelativeTrigger.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtCh1RelativeTrigger.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCh1RelativeTrigger.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCh1RelativeTrigger.Location = New System.Drawing.Point(2, 2)
        Me.txtCh1RelativeTrigger.Name = "txtCh1RelativeTrigger"
        Me.txtCh1RelativeTrigger.Size = New System.Drawing.Size(112, 13)
        Me.txtCh1RelativeTrigger.TabIndex = 113
        Me.txtCh1RelativeTrigger.Text = "99.9"
        Me.txtCh1RelativeTrigger.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'spnCh1RelativeTrigger
        '
        Me.spnCh1RelativeTrigger.Location = New System.Drawing.Point(117, -1)
        Me.spnCh1RelativeTrigger.Name = "spnCh1RelativeTrigger"
        Me.spnCh1RelativeTrigger.Size = New System.Drawing.Size(16, 20)
        Me.spnCh1RelativeTrigger.TabIndex = 114
        '
        'fraCh1TriggeringMethod
        '
        Me.fraCh1TriggeringMethod.Controls.Add(Me.chkCh1AbsTrigAuto)
        Me.fraCh1TriggeringMethod.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraCh1TriggeringMethod.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraCh1TriggeringMethod.Location = New System.Drawing.Point(8, 161)
        Me.fraCh1TriggeringMethod.Name = "fraCh1TriggeringMethod"
        Me.fraCh1TriggeringMethod.Size = New System.Drawing.Size(171, 46)
        Me.fraCh1TriggeringMethod.TabIndex = 131
        Me.fraCh1TriggeringMethod.TabStop = False
        Me.fraCh1TriggeringMethod.Text = "Triggering Method"
        '
        'chkCh1AbsTrigAuto
        '
        Me.chkCh1AbsTrigAuto.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCh1AbsTrigAuto.Location = New System.Drawing.Point(12, 20)
        Me.chkCh1AbsTrigAuto.Name = "chkCh1AbsTrigAuto"
        Me.chkCh1AbsTrigAuto.Size = New System.Drawing.Size(129, 22)
        Me.chkCh1AbsTrigAuto.TabIndex = 132
        Me.chkCh1AbsTrigAuto.Text = "Auto Triggering"
        Me.ToolTip1.SetToolTip(Me.chkCh1AbsTrigAuto, "Enable or disable autotrigger on channel 1")
        '
        'FrameChannel2TriggerSetup
        '
        Me.FrameChannel2TriggerSetup.BackColor = System.Drawing.SystemColors.Control
        Me.FrameChannel2TriggerSetup.Controls.Add(Me.fraCh2Hysteresis)
        Me.FrameChannel2TriggerSetup.Controls.Add(Me.fraCh2TriggerSlope)
        Me.FrameChannel2TriggerSetup.Controls.Add(Me.fraCh2AbsoluteTrigger)
        Me.FrameChannel2TriggerSetup.Controls.Add(Me.fraCh2RelativeTrigger)
        Me.FrameChannel2TriggerSetup.Controls.Add(Me.fraCh2TriggeringMethod)
        Me.FrameChannel2TriggerSetup.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FrameChannel2TriggerSetup.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.FrameChannel2TriggerSetup.Location = New System.Drawing.Point(362, 4)
        Me.FrameChannel2TriggerSetup.Name = "FrameChannel2TriggerSetup"
        Me.FrameChannel2TriggerSetup.Size = New System.Drawing.Size(185, 356)
        Me.FrameChannel2TriggerSetup.TabIndex = 115
        Me.FrameChannel2TriggerSetup.TabStop = False
        Me.FrameChannel2TriggerSetup.Text = "Channel 2"
        Me.FrameChannel2TriggerSetup.Visible = False
        '
        'fraCh2Hysteresis
        '
        Me.fraCh2Hysteresis.Controls.Add(Me.optCh2HysteresisDef)
        Me.fraCh2Hysteresis.Controls.Add(Me.optCh2HysteresisMin)
        Me.fraCh2Hysteresis.Controls.Add(Me.optCh2HysteresisMax)
        Me.fraCh2Hysteresis.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraCh2Hysteresis.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraCh2Hysteresis.Location = New System.Drawing.Point(8, 16)
        Me.fraCh2Hysteresis.Name = "fraCh2Hysteresis"
        Me.fraCh2Hysteresis.Size = New System.Drawing.Size(171, 77)
        Me.fraCh2Hysteresis.TabIndex = 116
        Me.fraCh2Hysteresis.TabStop = False
        Me.fraCh2Hysteresis.Text = "Hysteresis"
        '
        'optCh2HysteresisDef
        '
        Me.optCh2HysteresisDef.Checked = True
        Me.optCh2HysteresisDef.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optCh2HysteresisDef.Location = New System.Drawing.Point(12, 35)
        Me.optCh2HysteresisDef.Name = "optCh2HysteresisDef"
        Me.optCh2HysteresisDef.Size = New System.Drawing.Size(107, 19)
        Me.optCh2HysteresisDef.TabIndex = 117
        Me.optCh2HysteresisDef.TabStop = True
        Me.optCh2HysteresisDef.Text = "Def (60mV)"
        '
        'optCh2HysteresisMin
        '
        Me.optCh2HysteresisMin.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optCh2HysteresisMin.Location = New System.Drawing.Point(12, 54)
        Me.optCh2HysteresisMin.Name = "optCh2HysteresisMin"
        Me.optCh2HysteresisMin.Size = New System.Drawing.Size(110, 19)
        Me.optCh2HysteresisMin.TabIndex = 118
        Me.optCh2HysteresisMin.Text = "Min (35mV)"
        '
        'optCh2HysteresisMax
        '
        Me.optCh2HysteresisMax.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optCh2HysteresisMax.Location = New System.Drawing.Point(12, 16)
        Me.optCh2HysteresisMax.Name = "optCh2HysteresisMax"
        Me.optCh2HysteresisMax.Size = New System.Drawing.Size(129, 19)
        Me.optCh2HysteresisMax.TabIndex = 119
        Me.optCh2HysteresisMax.Text = "Max (100mV)"
        '
        'fraCh2TriggerSlope
        '
        Me.fraCh2TriggerSlope.Controls.Add(Me.optCh2TriggerSlopeNegative)
        Me.fraCh2TriggerSlope.Controls.Add(Me.optCh2TriggerSlopePositive)
        Me.fraCh2TriggerSlope.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraCh2TriggerSlope.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraCh2TriggerSlope.Location = New System.Drawing.Point(8, 96)
        Me.fraCh2TriggerSlope.Name = "fraCh2TriggerSlope"
        Me.fraCh2TriggerSlope.Size = New System.Drawing.Size(171, 61)
        Me.fraCh2TriggerSlope.TabIndex = 120
        Me.fraCh2TriggerSlope.TabStop = False
        Me.fraCh2TriggerSlope.Text = "Trigger Slope"
        '
        'optCh2TriggerSlopeNegative
        '
        Me.optCh2TriggerSlopeNegative.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optCh2TriggerSlopeNegative.Location = New System.Drawing.Point(12, 36)
        Me.optCh2TriggerSlopeNegative.Name = "optCh2TriggerSlopeNegative"
        Me.optCh2TriggerSlopeNegative.Size = New System.Drawing.Size(95, 22)
        Me.optCh2TriggerSlopeNegative.TabIndex = 121
        Me.optCh2TriggerSlopeNegative.Text = "Negative"
        '
        'optCh2TriggerSlopePositive
        '
        Me.optCh2TriggerSlopePositive.Checked = True
        Me.optCh2TriggerSlopePositive.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optCh2TriggerSlopePositive.Location = New System.Drawing.Point(12, 18)
        Me.optCh2TriggerSlopePositive.Name = "optCh2TriggerSlopePositive"
        Me.optCh2TriggerSlopePositive.Size = New System.Drawing.Size(82, 19)
        Me.optCh2TriggerSlopePositive.TabIndex = 122
        Me.optCh2TriggerSlopePositive.TabStop = True
        Me.optCh2TriggerSlopePositive.Text = "Positive"
        '
        'fraCh2AbsoluteTrigger
        '
        Me.fraCh2AbsoluteTrigger.Controls.Add(Me.panCh2AbsoluteTrigger)
        Me.fraCh2AbsoluteTrigger.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraCh2AbsoluteTrigger.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraCh2AbsoluteTrigger.Location = New System.Drawing.Point(8, 212)
        Me.fraCh2AbsoluteTrigger.Name = "fraCh2AbsoluteTrigger"
        Me.fraCh2AbsoluteTrigger.Size = New System.Drawing.Size(171, 58)
        Me.fraCh2AbsoluteTrigger.TabIndex = 123
        Me.fraCh2AbsoluteTrigger.TabStop = False
        Me.fraCh2AbsoluteTrigger.Text = "Absolute Trigger Level"
        '
        'panCh2AbsoluteTrigger
        '
        Me.panCh2AbsoluteTrigger.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.panCh2AbsoluteTrigger.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panCh2AbsoluteTrigger.Controls.Add(Me.txtCh2AbsoluteTrigger)
        Me.panCh2AbsoluteTrigger.Controls.Add(Me.spnCh2AbsoluteTrigger)
        Me.panCh2AbsoluteTrigger.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panCh2AbsoluteTrigger.Location = New System.Drawing.Point(19, 27)
        Me.panCh2AbsoluteTrigger.Name = "panCh2AbsoluteTrigger"
        Me.panCh2AbsoluteTrigger.Size = New System.Drawing.Size(135, 22)
        Me.panCh2AbsoluteTrigger.TabIndex = 124
        '
        'txtCh2AbsoluteTrigger
        '
        Me.txtCh2AbsoluteTrigger.BackColor = System.Drawing.SystemColors.Window
        Me.txtCh2AbsoluteTrigger.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtCh2AbsoluteTrigger.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCh2AbsoluteTrigger.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCh2AbsoluteTrigger.Location = New System.Drawing.Point(2, 2)
        Me.txtCh2AbsoluteTrigger.Name = "txtCh2AbsoluteTrigger"
        Me.txtCh2AbsoluteTrigger.Size = New System.Drawing.Size(112, 13)
        Me.txtCh2AbsoluteTrigger.TabIndex = 125
        Me.txtCh2AbsoluteTrigger.Text = "99.9"
        Me.txtCh2AbsoluteTrigger.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'spnCh2AbsoluteTrigger
        '
        Me.spnCh2AbsoluteTrigger.Location = New System.Drawing.Point(117, -1)
        Me.spnCh2AbsoluteTrigger.Name = "spnCh2AbsoluteTrigger"
        Me.spnCh2AbsoluteTrigger.Size = New System.Drawing.Size(16, 20)
        Me.spnCh2AbsoluteTrigger.TabIndex = 126
        '
        'fraCh2RelativeTrigger
        '
        Me.fraCh2RelativeTrigger.Controls.Add(Me.panCh2RelativeTrigger)
        Me.fraCh2RelativeTrigger.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraCh2RelativeTrigger.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraCh2RelativeTrigger.Location = New System.Drawing.Point(8, 276)
        Me.fraCh2RelativeTrigger.Name = "fraCh2RelativeTrigger"
        Me.fraCh2RelativeTrigger.Size = New System.Drawing.Size(171, 70)
        Me.fraCh2RelativeTrigger.TabIndex = 127
        Me.fraCh2RelativeTrigger.TabStop = False
        Me.fraCh2RelativeTrigger.Text = "Relative Trigger Level"
        Me.fraCh2RelativeTrigger.Visible = False
        '
        'panCh2RelativeTrigger
        '
        Me.panCh2RelativeTrigger.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.panCh2RelativeTrigger.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panCh2RelativeTrigger.Controls.Add(Me.txtCh2RelativeTrigger)
        Me.panCh2RelativeTrigger.Controls.Add(Me.spnCh2RelativeTrigger)
        Me.panCh2RelativeTrigger.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panCh2RelativeTrigger.Location = New System.Drawing.Point(19, 28)
        Me.panCh2RelativeTrigger.Name = "panCh2RelativeTrigger"
        Me.panCh2RelativeTrigger.Size = New System.Drawing.Size(135, 22)
        Me.panCh2RelativeTrigger.TabIndex = 128
        '
        'txtCh2RelativeTrigger
        '
        Me.txtCh2RelativeTrigger.BackColor = System.Drawing.SystemColors.Window
        Me.txtCh2RelativeTrigger.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtCh2RelativeTrigger.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCh2RelativeTrigger.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCh2RelativeTrigger.Location = New System.Drawing.Point(2, 2)
        Me.txtCh2RelativeTrigger.Name = "txtCh2RelativeTrigger"
        Me.txtCh2RelativeTrigger.Size = New System.Drawing.Size(112, 13)
        Me.txtCh2RelativeTrigger.TabIndex = 129
        Me.txtCh2RelativeTrigger.Text = "99.9"
        Me.txtCh2RelativeTrigger.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'spnCh2RelativeTrigger
        '
        Me.spnCh2RelativeTrigger.Location = New System.Drawing.Point(117, -1)
        Me.spnCh2RelativeTrigger.Name = "spnCh2RelativeTrigger"
        Me.spnCh2RelativeTrigger.Size = New System.Drawing.Size(16, 20)
        Me.spnCh2RelativeTrigger.TabIndex = 130
        '
        'fraCh2TriggeringMethod
        '
        Me.fraCh2TriggeringMethod.Controls.Add(Me.chkCh2AbsTrigAuto)
        Me.fraCh2TriggeringMethod.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraCh2TriggeringMethod.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraCh2TriggeringMethod.Location = New System.Drawing.Point(8, 161)
        Me.fraCh2TriggeringMethod.Name = "fraCh2TriggeringMethod"
        Me.fraCh2TriggeringMethod.Size = New System.Drawing.Size(171, 46)
        Me.fraCh2TriggeringMethod.TabIndex = 133
        Me.fraCh2TriggeringMethod.TabStop = False
        Me.fraCh2TriggeringMethod.Text = "Triggering Method"
        '
        'chkCh2AbsTrigAuto
        '
        Me.chkCh2AbsTrigAuto.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCh2AbsTrigAuto.Location = New System.Drawing.Point(12, 20)
        Me.chkCh2AbsTrigAuto.Name = "chkCh2AbsTrigAuto"
        Me.chkCh2AbsTrigAuto.Size = New System.Drawing.Size(129, 22)
        Me.chkCh2AbsTrigAuto.TabIndex = 134
        Me.chkCh2AbsTrigAuto.Text = "Auto Triggering"
        Me.ToolTip1.SetToolTip(Me.chkCh2AbsTrigAuto, "Enable or disable autotrigger on channel 2")
        '
        'tabUserOptions_Page4
        '
        Me.tabUserOptions_Page4.Controls.Add(Me.fraArmStartLevel)
        Me.tabUserOptions_Page4.Controls.Add(Me.fraArmStopLevel)
        Me.tabUserOptions_Page4.Controls.Add(Me.fraArmStopSlope)
        Me.tabUserOptions_Page4.Controls.Add(Me.fraArmStartSlope)
        Me.tabUserOptions_Page4.Controls.Add(Me.fraArmStartSource)
        Me.tabUserOptions_Page4.Controls.Add(Me.fraArmStopSource)
        Me.tabUserOptions_Page4.Controls.Add(Me.cmdArmStart)
        Me.tabUserOptions_Page4.Controls.Add(Me.cmdArmStop)
        Me.tabUserOptions_Page4.Location = New System.Drawing.Point(4, 22)
        Me.tabUserOptions_Page4.Name = "tabUserOptions_Page4"
        Me.tabUserOptions_Page4.Size = New System.Drawing.Size(592, 367)
        Me.tabUserOptions_Page4.TabIndex = 3
        Me.tabUserOptions_Page4.Text = "Arm"
        '
        'fraArmStartLevel
        '
        Me.fraArmStartLevel.Controls.Add(Me.optArmStartLevelGND)
        Me.fraArmStartLevel.Controls.Add(Me.optArmStartLevelECL)
        Me.fraArmStartLevel.Controls.Add(Me.optArmStartLevelTTL)
        Me.fraArmStartLevel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraArmStartLevel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraArmStartLevel.Location = New System.Drawing.Point(8, 134)
        Me.fraArmStartLevel.Name = "fraArmStartLevel"
        Me.fraArmStartLevel.Size = New System.Drawing.Size(172, 81)
        Me.fraArmStartLevel.TabIndex = 48
        Me.fraArmStartLevel.TabStop = False
        Me.fraArmStartLevel.Text = "Arm Start Level"
        '
        'optArmStartLevelGND
        '
        Me.optArmStartLevelGND.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optArmStartLevelGND.Location = New System.Drawing.Point(12, 58)
        Me.optArmStartLevelGND.Name = "optArmStartLevelGND"
        Me.optArmStartLevelGND.Size = New System.Drawing.Size(98, 21)
        Me.optArmStartLevelGND.TabIndex = 51
        Me.optArmStartLevelGND.Text = "GND (0 V)"
        '
        'optArmStartLevelECL
        '
        Me.optArmStartLevelECL.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optArmStartLevelECL.Location = New System.Drawing.Point(12, 39)
        Me.optArmStartLevelECL.Name = "optArmStartLevelECL"
        Me.optArmStartLevelECL.Size = New System.Drawing.Size(110, 21)
        Me.optArmStartLevelECL.TabIndex = 50
        Me.optArmStartLevelECL.Text = "ECL (-1.3 V)"
        '
        'optArmStartLevelTTL
        '
        Me.optArmStartLevelTTL.Checked = True
        Me.optArmStartLevelTTL.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optArmStartLevelTTL.Location = New System.Drawing.Point(12, 20)
        Me.optArmStartLevelTTL.Name = "optArmStartLevelTTL"
        Me.optArmStartLevelTTL.Size = New System.Drawing.Size(112, 21)
        Me.optArmStartLevelTTL.TabIndex = 49
        Me.optArmStartLevelTTL.TabStop = True
        Me.optArmStartLevelTTL.Text = "TTL (+1.6 V)"
        '
        'fraArmStopLevel
        '
        Me.fraArmStopLevel.Controls.Add(Me.optArmStopLevelGND)
        Me.fraArmStopLevel.Controls.Add(Me.optArmStopLevelECL)
        Me.fraArmStopLevel.Controls.Add(Me.optArmStopLevelTTL)
        Me.fraArmStopLevel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraArmStopLevel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraArmStopLevel.Location = New System.Drawing.Point(202, 134)
        Me.fraArmStopLevel.Name = "fraArmStopLevel"
        Me.fraArmStopLevel.Size = New System.Drawing.Size(172, 81)
        Me.fraArmStopLevel.TabIndex = 44
        Me.fraArmStopLevel.TabStop = False
        Me.fraArmStopLevel.Text = "Arm Stop Level"
        '
        'optArmStopLevelGND
        '
        Me.optArmStopLevelGND.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optArmStopLevelGND.Location = New System.Drawing.Point(12, 58)
        Me.optArmStopLevelGND.Name = "optArmStopLevelGND"
        Me.optArmStopLevelGND.Size = New System.Drawing.Size(98, 21)
        Me.optArmStopLevelGND.TabIndex = 47
        Me.optArmStopLevelGND.Text = "GND (0 V)"
        '
        'optArmStopLevelECL
        '
        Me.optArmStopLevelECL.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optArmStopLevelECL.Location = New System.Drawing.Point(12, 39)
        Me.optArmStopLevelECL.Name = "optArmStopLevelECL"
        Me.optArmStopLevelECL.Size = New System.Drawing.Size(110, 21)
        Me.optArmStopLevelECL.TabIndex = 46
        Me.optArmStopLevelECL.Text = "ECL (-1.3 V)"
        '
        'optArmStopLevelTTL
        '
        Me.optArmStopLevelTTL.Checked = True
        Me.optArmStopLevelTTL.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optArmStopLevelTTL.Location = New System.Drawing.Point(12, 20)
        Me.optArmStopLevelTTL.Name = "optArmStopLevelTTL"
        Me.optArmStopLevelTTL.Size = New System.Drawing.Size(112, 21)
        Me.optArmStopLevelTTL.TabIndex = 45
        Me.optArmStopLevelTTL.TabStop = True
        Me.optArmStopLevelTTL.Text = "TTL (+1.6 V)"
        '
        'fraArmStopSlope
        '
        Me.fraArmStopSlope.Controls.Add(Me.optArmStopSlopeNegative)
        Me.fraArmStopSlope.Controls.Add(Me.optArmStopSlopePositive)
        Me.fraArmStopSlope.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraArmStopSlope.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraArmStopSlope.Location = New System.Drawing.Point(202, 68)
        Me.fraArmStopSlope.Name = "fraArmStopSlope"
        Me.fraArmStopSlope.Size = New System.Drawing.Size(172, 64)
        Me.fraArmStopSlope.TabIndex = 41
        Me.fraArmStopSlope.TabStop = False
        Me.fraArmStopSlope.Text = "Arm Stop Slope"
        '
        'optArmStopSlopeNegative
        '
        Me.optArmStopSlopeNegative.Checked = True
        Me.optArmStopSlopeNegative.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optArmStopSlopeNegative.Location = New System.Drawing.Point(12, 40)
        Me.optArmStopSlopeNegative.Name = "optArmStopSlopeNegative"
        Me.optArmStopSlopeNegative.Size = New System.Drawing.Size(98, 21)
        Me.optArmStopSlopeNegative.TabIndex = 43
        Me.optArmStopSlopeNegative.TabStop = True
        Me.optArmStopSlopeNegative.Text = "Negative"
        '
        'optArmStopSlopePositive
        '
        Me.optArmStopSlopePositive.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optArmStopSlopePositive.Location = New System.Drawing.Point(12, 20)
        Me.optArmStopSlopePositive.Name = "optArmStopSlopePositive"
        Me.optArmStopSlopePositive.Size = New System.Drawing.Size(80, 19)
        Me.optArmStopSlopePositive.TabIndex = 42
        Me.optArmStopSlopePositive.Text = "Positive"
        '
        'fraArmStartSlope
        '
        Me.fraArmStartSlope.Controls.Add(Me.optArmStartSlopeNegative)
        Me.fraArmStartSlope.Controls.Add(Me.optArmStartSlopePositive)
        Me.fraArmStartSlope.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraArmStartSlope.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraArmStartSlope.Location = New System.Drawing.Point(8, 65)
        Me.fraArmStartSlope.Name = "fraArmStartSlope"
        Me.fraArmStartSlope.Size = New System.Drawing.Size(172, 64)
        Me.fraArmStartSlope.TabIndex = 38
        Me.fraArmStartSlope.TabStop = False
        Me.fraArmStartSlope.Text = "Arm Start Slope"
        '
        'optArmStartSlopeNegative
        '
        Me.optArmStartSlopeNegative.Checked = True
        Me.optArmStartSlopeNegative.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optArmStartSlopeNegative.Location = New System.Drawing.Point(12, 40)
        Me.optArmStartSlopeNegative.Name = "optArmStartSlopeNegative"
        Me.optArmStartSlopeNegative.Size = New System.Drawing.Size(98, 21)
        Me.optArmStartSlopeNegative.TabIndex = 40
        Me.optArmStartSlopeNegative.TabStop = True
        Me.optArmStartSlopeNegative.Text = "Negative"
        '
        'optArmStartSlopePositive
        '
        Me.optArmStartSlopePositive.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optArmStartSlopePositive.Location = New System.Drawing.Point(12, 20)
        Me.optArmStartSlopePositive.Name = "optArmStartSlopePositive"
        Me.optArmStartSlopePositive.Size = New System.Drawing.Size(80, 19)
        Me.optArmStartSlopePositive.TabIndex = 39
        Me.optArmStartSlopePositive.Text = "Positive"
        '
        'fraArmStartSource
        '
        Me.fraArmStartSource.Controls.Add(Me.cboArmStartSource)
        Me.fraArmStartSource.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraArmStartSource.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraArmStartSource.Location = New System.Drawing.Point(8, 4)
        Me.fraArmStartSource.Name = "fraArmStartSource"
        Me.fraArmStartSource.Size = New System.Drawing.Size(172, 61)
        Me.fraArmStartSource.TabIndex = 36
        Me.fraArmStartSource.TabStop = False
        Me.fraArmStartSource.Text = "Arm Start Source"
        '
        'cboArmStartSource
        '
        Me.cboArmStartSource.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cboArmStartSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboArmStartSource.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboArmStartSource.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboArmStartSource.Location = New System.Drawing.Point(8, 24)
        Me.cboArmStartSource.Name = "cboArmStartSource"
        Me.cboArmStartSource.Size = New System.Drawing.Size(147, 21)
        Me.cboArmStartSource.TabIndex = 37
        '
        'fraArmStopSource
        '
        Me.fraArmStopSource.Controls.Add(Me.cboArmStopSource)
        Me.fraArmStopSource.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraArmStopSource.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraArmStopSource.Location = New System.Drawing.Point(202, 4)
        Me.fraArmStopSource.Name = "fraArmStopSource"
        Me.fraArmStopSource.Size = New System.Drawing.Size(172, 61)
        Me.fraArmStopSource.TabIndex = 34
        Me.fraArmStopSource.TabStop = False
        Me.fraArmStopSource.Text = "Arm Stop Source"
        '
        'cboArmStopSource
        '
        Me.cboArmStopSource.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cboArmStopSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboArmStopSource.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboArmStopSource.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboArmStopSource.Location = New System.Drawing.Point(8, 24)
        Me.cboArmStopSource.Name = "cboArmStopSource"
        Me.cboArmStopSource.Size = New System.Drawing.Size(147, 21)
        Me.cboArmStopSource.TabIndex = 35
        '
        'cmdArmStart
        '
        Me.cmdArmStart.BackColor = System.Drawing.SystemColors.Control
        Me.cmdArmStart.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdArmStart.Location = New System.Drawing.Point(24, 226)
        Me.cmdArmStart.Name = "cmdArmStart"
        Me.cmdArmStart.Size = New System.Drawing.Size(76, 23)
        Me.cmdArmStart.TabIndex = 85
        Me.cmdArmStart.Text = "&Start"
        Me.cmdArmStart.UseVisualStyleBackColor = False
        Me.cmdArmStart.Visible = False
        '
        'cmdArmStop
        '
        Me.cmdArmStop.BackColor = System.Drawing.SystemColors.Control
        Me.cmdArmStop.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdArmStop.Location = New System.Drawing.Point(226, 226)
        Me.cmdArmStop.Name = "cmdArmStop"
        Me.cmdArmStop.Size = New System.Drawing.Size(76, 23)
        Me.cmdArmStop.TabIndex = 86
        Me.cmdArmStop.Text = "St&op"
        Me.cmdArmStop.UseVisualStyleBackColor = False
        Me.cmdArmStop.Visible = False
        '
        'tabUserOptions_Page5
        '
        Me.tabUserOptions_Page5.Controls.Add(Me.fraAcquisitionTimeout)
        Me.tabUserOptions_Page5.Controls.Add(Me.cmdSelfTest)
        Me.tabUserOptions_Page5.Controls.Add(Me.cmdAbout)
        Me.tabUserOptions_Page5.Controls.Add(Me.fraTiDelay)
        Me.tabUserOptions_Page5.Controls.Add(Me.fraTiGateAvg)
        Me.tabUserOptions_Page5.Controls.Add(Me.fraTimeOut)
        Me.tabUserOptions_Page5.Controls.Add(Me.fraTotalizeGatePolarity)
        Me.tabUserOptions_Page5.Controls.Add(Me.fraTotalizeGateState)
        Me.tabUserOptions_Page5.Controls.Add(Me.fraTimebaseSource)
        Me.tabUserOptions_Page5.Controls.Add(Me.cmdUpdateTIP)
        Me.tabUserOptions_Page5.Controls.Add(Me.Panel_Conifg)
        Me.tabUserOptions_Page5.Location = New System.Drawing.Point(4, 22)
        Me.tabUserOptions_Page5.Name = "tabUserOptions_Page5"
        Me.tabUserOptions_Page5.Size = New System.Drawing.Size(592, 367)
        Me.tabUserOptions_Page5.TabIndex = 4
        Me.tabUserOptions_Page5.Text = "Options"
        '
        'fraAcquisitionTimeout
        '
        Me.fraAcquisitionTimeout.Controls.Add(Me.optAcquisitionTimeoutOff)
        Me.fraAcquisitionTimeout.Controls.Add(Me.optAcquisitionTimeoutOn)
        Me.fraAcquisitionTimeout.Controls.Add(Me.optAcquisitionTimeoutStart)
        Me.fraAcquisitionTimeout.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraAcquisitionTimeout.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraAcquisitionTimeout.Location = New System.Drawing.Point(7, 230)
        Me.fraAcquisitionTimeout.Name = "fraAcquisitionTimeout"
        Me.fraAcquisitionTimeout.Size = New System.Drawing.Size(198, 80)
        Me.fraAcquisitionTimeout.TabIndex = 81
        Me.fraAcquisitionTimeout.TabStop = False
        Me.fraAcquisitionTimeout.Text = "Acquisition Timeout Setting"
        '
        'optAcquisitionTimeoutOff
        '
        Me.optAcquisitionTimeoutOff.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optAcquisitionTimeoutOff.Location = New System.Drawing.Point(12, 36)
        Me.optAcquisitionTimeoutOff.Name = "optAcquisitionTimeoutOff"
        Me.optAcquisitionTimeoutOff.Size = New System.Drawing.Size(67, 20)
        Me.optAcquisitionTimeoutOff.TabIndex = 82
        Me.optAcquisitionTimeoutOff.Text = "Off"
        '
        'optAcquisitionTimeoutOn
        '
        Me.optAcquisitionTimeoutOn.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optAcquisitionTimeoutOn.Location = New System.Drawing.Point(12, 16)
        Me.optAcquisitionTimeoutOn.Name = "optAcquisitionTimeoutOn"
        Me.optAcquisitionTimeoutOn.Size = New System.Drawing.Size(55, 20)
        Me.optAcquisitionTimeoutOn.TabIndex = 83
        Me.optAcquisitionTimeoutOn.Text = "On"
        '
        'optAcquisitionTimeoutStart
        '
        Me.optAcquisitionTimeoutStart.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optAcquisitionTimeoutStart.Location = New System.Drawing.Point(12, 56)
        Me.optAcquisitionTimeoutStart.Name = "optAcquisitionTimeoutStart"
        Me.optAcquisitionTimeoutStart.Size = New System.Drawing.Size(88, 20)
        Me.optAcquisitionTimeoutStart.TabIndex = 84
        Me.optAcquisitionTimeoutStart.Text = "Start"
        '
        'cmdSelfTest
        '
        Me.cmdSelfTest.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSelfTest.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSelfTest.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSelfTest.Location = New System.Drawing.Point(437, 279)
        Me.cmdSelfTest.Name = "cmdSelfTest"
        Me.cmdSelfTest.Size = New System.Drawing.Size(96, 23)
        Me.cmdSelfTest.TabIndex = 52
        Me.cmdSelfTest.Text = "Built-In &Test"
        Me.cmdSelfTest.UseVisualStyleBackColor = False
        '
        'cmdAbout
        '
        Me.cmdAbout.BackColor = System.Drawing.SystemColors.Control
        Me.cmdAbout.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAbout.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAbout.Location = New System.Drawing.Point(437, 247)
        Me.cmdAbout.Name = "cmdAbout"
        Me.cmdAbout.Size = New System.Drawing.Size(96, 23)
        Me.cmdAbout.TabIndex = 53
        Me.cmdAbout.Text = "About"
        Me.cmdAbout.UseVisualStyleBackColor = False
        '
        'fraTiDelay
        '
        Me.fraTiDelay.Controls.Add(Me.panTiDelay)
        Me.fraTiDelay.Controls.Add(Me.chkTIDelay)
        Me.fraTiDelay.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraTiDelay.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraTiDelay.Location = New System.Drawing.Point(212, 51)
        Me.fraTiDelay.Name = "fraTiDelay"
        Me.fraTiDelay.Size = New System.Drawing.Size(182, 77)
        Me.fraTiDelay.TabIndex = 66
        Me.fraTiDelay.TabStop = False
        Me.fraTiDelay.Text = "Time Interval Delay"
        '
        'panTiDelay
        '
        Me.panTiDelay.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.panTiDelay.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panTiDelay.Controls.Add(Me.txtTiDelay)
        Me.panTiDelay.Controls.Add(Me.spnTiDelay)
        Me.panTiDelay.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panTiDelay.Location = New System.Drawing.Point(21, 24)
        Me.panTiDelay.Name = "panTiDelay"
        Me.panTiDelay.Size = New System.Drawing.Size(148, 22)
        Me.panTiDelay.TabIndex = 69
        '
        'txtTiDelay
        '
        Me.txtTiDelay.BackColor = System.Drawing.SystemColors.Window
        Me.txtTiDelay.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtTiDelay.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTiDelay.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTiDelay.Location = New System.Drawing.Point(2, 2)
        Me.txtTiDelay.Name = "txtTiDelay"
        Me.txtTiDelay.Size = New System.Drawing.Size(125, 13)
        Me.txtTiDelay.TabIndex = 70
        Me.txtTiDelay.Text = "99.9"
        Me.txtTiDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'spnTiDelay
        '
        Me.spnTiDelay.Location = New System.Drawing.Point(130, -1)
        Me.spnTiDelay.Name = "spnTiDelay"
        Me.spnTiDelay.Size = New System.Drawing.Size(16, 20)
        Me.spnTiDelay.TabIndex = 71
        '
        'chkTIDelay
        '
        Me.chkTIDelay.Checked = True
        Me.chkTIDelay.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkTIDelay.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTIDelay.Location = New System.Drawing.Point(21, 49)
        Me.chkTIDelay.Name = "chkTIDelay"
        Me.chkTIDelay.Size = New System.Drawing.Size(74, 20)
        Me.chkTIDelay.TabIndex = 67
        Me.chkTIDelay.Text = "Enabled"
        '
        'fraTiGateAvg
        '
        Me.fraTiGateAvg.Controls.Add(Me.optTiGateAvgOff)
        Me.fraTiGateAvg.Controls.Add(Me.optTiGateAvgOn)
        Me.fraTiGateAvg.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraTiGateAvg.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraTiGateAvg.Location = New System.Drawing.Point(212, 4)
        Me.fraTiGateAvg.Name = "fraTiGateAvg"
        Me.fraTiGateAvg.Size = New System.Drawing.Size(182, 41)
        Me.fraTiGateAvg.TabIndex = 60
        Me.fraTiGateAvg.TabStop = False
        Me.fraTiGateAvg.Text = "100 Gate Average Mode"
        '
        'optTiGateAvgOff
        '
        Me.optTiGateAvgOff.Checked = True
        Me.optTiGateAvgOff.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optTiGateAvgOff.Location = New System.Drawing.Point(83, 16)
        Me.optTiGateAvgOff.Name = "optTiGateAvgOff"
        Me.optTiGateAvgOff.Size = New System.Drawing.Size(58, 20)
        Me.optTiGateAvgOff.TabIndex = 62
        Me.optTiGateAvgOff.TabStop = True
        Me.optTiGateAvgOff.Text = "Off"
        '
        'optTiGateAvgOn
        '
        Me.optTiGateAvgOn.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optTiGateAvgOn.Location = New System.Drawing.Point(25, 16)
        Me.optTiGateAvgOn.Name = "optTiGateAvgOn"
        Me.optTiGateAvgOn.Size = New System.Drawing.Size(50, 20)
        Me.optTiGateAvgOn.TabIndex = 61
        Me.optTiGateAvgOn.Text = "On"
        '
        'fraTimeOut
        '
        Me.fraTimeOut.Controls.Add(Me.panTimeOut)
        Me.fraTimeOut.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraTimeOut.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraTimeOut.Location = New System.Drawing.Point(212, 134)
        Me.fraTimeOut.Name = "fraTimeOut"
        Me.fraTimeOut.Size = New System.Drawing.Size(182, 58)
        Me.fraTimeOut.TabIndex = 68
        Me.fraTimeOut.TabStop = False
        Me.fraTimeOut.Text = "Timeout Duration"
        '
        'panTimeOut
        '
        Me.panTimeOut.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.panTimeOut.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panTimeOut.Controls.Add(Me.txtTimeOut)
        Me.panTimeOut.Controls.Add(Me.spnTimeOut)
        Me.panTimeOut.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panTimeOut.Location = New System.Drawing.Point(21, 24)
        Me.panTimeOut.Name = "panTimeOut"
        Me.panTimeOut.Size = New System.Drawing.Size(148, 22)
        Me.panTimeOut.TabIndex = 87
        '
        'txtTimeOut
        '
        Me.txtTimeOut.BackColor = System.Drawing.SystemColors.Window
        Me.txtTimeOut.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtTimeOut.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTimeOut.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTimeOut.Location = New System.Drawing.Point(2, 2)
        Me.txtTimeOut.Name = "txtTimeOut"
        Me.txtTimeOut.Size = New System.Drawing.Size(125, 13)
        Me.txtTimeOut.TabIndex = 88
        Me.txtTimeOut.Text = "5.0 Sec"
        Me.txtTimeOut.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'spnTimeOut
        '
        Me.spnTimeOut.Location = New System.Drawing.Point(130, -1)
        Me.spnTimeOut.Name = "spnTimeOut"
        Me.spnTimeOut.Size = New System.Drawing.Size(16, 20)
        Me.spnTimeOut.TabIndex = 89
        '
        'fraTotalizeGatePolarity
        '
        Me.fraTotalizeGatePolarity.Controls.Add(Me.optTotalizeGatePolarityInvert)
        Me.fraTotalizeGatePolarity.Controls.Add(Me.optTotalizeGatePolarityNormal)
        Me.fraTotalizeGatePolarity.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraTotalizeGatePolarity.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraTotalizeGatePolarity.Location = New System.Drawing.Point(7, 165)
        Me.fraTotalizeGatePolarity.Name = "fraTotalizeGatePolarity"
        Me.fraTotalizeGatePolarity.Size = New System.Drawing.Size(198, 59)
        Me.fraTotalizeGatePolarity.TabIndex = 63
        Me.fraTotalizeGatePolarity.TabStop = False
        Me.fraTotalizeGatePolarity.Text = "Totalize Gate Polarity"
        '
        'optTotalizeGatePolarityInvert
        '
        Me.optTotalizeGatePolarityInvert.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optTotalizeGatePolarityInvert.Location = New System.Drawing.Point(12, 35)
        Me.optTotalizeGatePolarityInvert.Name = "optTotalizeGatePolarityInvert"
        Me.optTotalizeGatePolarityInvert.Size = New System.Drawing.Size(88, 20)
        Me.optTotalizeGatePolarityInvert.TabIndex = 65
        Me.optTotalizeGatePolarityInvert.Text = "Inverted"
        '
        'optTotalizeGatePolarityNormal
        '
        Me.optTotalizeGatePolarityNormal.Checked = True
        Me.optTotalizeGatePolarityNormal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optTotalizeGatePolarityNormal.Location = New System.Drawing.Point(12, 16)
        Me.optTotalizeGatePolarityNormal.Name = "optTotalizeGatePolarityNormal"
        Me.optTotalizeGatePolarityNormal.Size = New System.Drawing.Size(106, 20)
        Me.optTotalizeGatePolarityNormal.TabIndex = 64
        Me.optTotalizeGatePolarityNormal.TabStop = True
        Me.optTotalizeGatePolarityNormal.Text = "Normal"
        '
        'fraTotalizeGateState
        '
        Me.fraTotalizeGateState.Controls.Add(Me.chkTotalizeGateState)
        Me.fraTotalizeGateState.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraTotalizeGateState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraTotalizeGateState.Location = New System.Drawing.Point(7, 117)
        Me.fraTotalizeGateState.Name = "fraTotalizeGateState"
        Me.fraTotalizeGateState.Size = New System.Drawing.Size(198, 41)
        Me.fraTotalizeGateState.TabIndex = 54
        Me.fraTotalizeGateState.TabStop = False
        Me.fraTotalizeGateState.Text = "Totalize Gate State"
        '
        'chkTotalizeGateState
        '
        Me.chkTotalizeGateState.Checked = True
        Me.chkTotalizeGateState.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkTotalizeGateState.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTotalizeGateState.Location = New System.Drawing.Point(12, 16)
        Me.chkTotalizeGateState.Name = "chkTotalizeGateState"
        Me.chkTotalizeGateState.Size = New System.Drawing.Size(166, 20)
        Me.chkTotalizeGateState.TabIndex = 55
        Me.chkTotalizeGateState.Text = "Gate Measurements"
        '
        'fraTimebaseSource
        '
        Me.fraTimebaseSource.Controls.Add(Me.chkOutputTimebase)
        Me.fraTimebaseSource.Controls.Add(Me.optTimebaseSourceExtern)
        Me.fraTimebaseSource.Controls.Add(Me.optTimebaseSourceIntern)
        Me.fraTimebaseSource.Controls.Add(Me.optTimebaseSourceVXIClk)
        Me.fraTimebaseSource.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraTimebaseSource.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraTimebaseSource.Location = New System.Drawing.Point(7, 4)
        Me.fraTimebaseSource.Name = "fraTimebaseSource"
        Me.fraTimebaseSource.Size = New System.Drawing.Size(198, 110)
        Me.fraTimebaseSource.TabIndex = 56
        Me.fraTimebaseSource.TabStop = False
        Me.fraTimebaseSource.Text = "Timebase Source"
        '
        'chkOutputTimebase
        '
        Me.chkOutputTimebase.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkOutputTimebase.Location = New System.Drawing.Point(12, 84)
        Me.chkOutputTimebase.Name = "chkOutputTimebase"
        Me.chkOutputTimebase.Size = New System.Drawing.Size(147, 20)
        Me.chkOutputTimebase.TabIndex = 80
        Me.chkOutputTimebase.Text = "Output Timebase"
        '
        'optTimebaseSourceExtern
        '
        Me.optTimebaseSourceExtern.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optTimebaseSourceExtern.Location = New System.Drawing.Point(12, 56)
        Me.optTimebaseSourceExtern.Name = "optTimebaseSourceExtern"
        Me.optTimebaseSourceExtern.Size = New System.Drawing.Size(147, 20)
        Me.optTimebaseSourceExtern.TabIndex = 59
        Me.optTimebaseSourceExtern.Text = "External Connector"
        '
        'optTimebaseSourceIntern
        '
        Me.optTimebaseSourceIntern.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optTimebaseSourceIntern.Location = New System.Drawing.Point(12, 36)
        Me.optTimebaseSourceIntern.Name = "optTimebaseSourceIntern"
        Me.optTimebaseSourceIntern.Size = New System.Drawing.Size(147, 20)
        Me.optTimebaseSourceIntern.TabIndex = 58
        Me.optTimebaseSourceIntern.Text = "Internal TCXO"
        '
        'optTimebaseSourceVXIClk
        '
        Me.optTimebaseSourceVXIClk.Checked = True
        Me.optTimebaseSourceVXIClk.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optTimebaseSourceVXIClk.Location = New System.Drawing.Point(12, 16)
        Me.optTimebaseSourceVXIClk.Name = "optTimebaseSourceVXIClk"
        Me.optTimebaseSourceVXIClk.Size = New System.Drawing.Size(147, 20)
        Me.optTimebaseSourceVXIClk.TabIndex = 57
        Me.optTimebaseSourceVXIClk.TabStop = True
        Me.optTimebaseSourceVXIClk.Text = "VXI CLK 10"
        '
        'cmdUpdateTIP
        '
        Me.cmdUpdateTIP.BackColor = System.Drawing.SystemColors.Control
        Me.cmdUpdateTIP.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdUpdateTIP.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdUpdateTIP.Location = New System.Drawing.Point(437, 214)
        Me.cmdUpdateTIP.Name = "cmdUpdateTIP"
        Me.cmdUpdateTIP.Size = New System.Drawing.Size(96, 23)
        Me.cmdUpdateTIP.TabIndex = 93
        Me.cmdUpdateTIP.Text = "&Update TIP"
        Me.cmdUpdateTIP.UseVisualStyleBackColor = False
        Me.cmdUpdateTIP.Visible = False
        '
        'Panel_Conifg
        '
        Me.Panel_Conifg.BackColor = System.Drawing.SystemColors.Control
        Me.Panel_Conifg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Panel_Conifg.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Panel_Conifg.Location = New System.Drawing.Point(400, 4)
        Me.Panel_Conifg.Name = "Panel_Conifg"
        Me.Panel_Conifg.Parent_Object = Nothing
        Me.Panel_Conifg.Refresh = CType(0, Short)
        Me.Panel_Conifg.Size = New System.Drawing.Size(190, 139)
        Me.Panel_Conifg.TabIndex = 97
        '
        'tabUserOptions_Page6
        '
        Me.tabUserOptions_Page6.Controls.Add(Me.Atlas_SFP)
        Me.tabUserOptions_Page6.Location = New System.Drawing.Point(4, 22)
        Me.tabUserOptions_Page6.Name = "tabUserOptions_Page6"
        Me.tabUserOptions_Page6.Size = New System.Drawing.Size(592, 367)
        Me.tabUserOptions_Page6.TabIndex = 5
        Me.tabUserOptions_Page6.Text = "Atlas"
        '
        'Atlas_SFP
        '
        Me.Atlas_SFP.ATLAS = Nothing
        Me.Atlas_SFP.BackColor = System.Drawing.SystemColors.Control
        Me.Atlas_SFP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Atlas_SFP.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Atlas_SFP.Location = New System.Drawing.Point(16, 12)
        Me.Atlas_SFP.Name = "Atlas_SFP"
        Me.Atlas_SFP.Parent_Object = Nothing
        Me.Atlas_SFP.Size = New System.Drawing.Size(408, 219)
        Me.Atlas_SFP.TabIndex = 98
        '
        'Button11
        '
        Me.Button11.Enabled = False
        Me.Button11.Name = "Button11"
        Me.Button11.ToolTipText = "AC Voltage"
        Me.Button11.Visible = False
        '
        'btnFuncMinVoltage
        '
        Me.btnFuncMinVoltage.Enabled = False
        Me.btnFuncMinVoltage.Name = "btnFuncMinVoltage"
        Me.btnFuncMinVoltage.ToolTipText = "Minimum Voltage"
        Me.btnFuncMinVoltage.Visible = False
        '
        'btnFuncMaxVoltage
        '
        Me.btnFuncMaxVoltage.Enabled = False
        Me.btnFuncMaxVoltage.Name = "btnFuncMaxVoltage"
        Me.btnFuncMaxVoltage.ToolTipText = "Maximum Voltage"
        Me.btnFuncMaxVoltage.Visible = False
        '
        'btnFuncMh
        '
        Me.btnFuncMh.Name = "btnFuncMh"
        Me.btnFuncMh.ToolTipText = "Phase"
        '
        'sbrUserInformation
        '
        Me.sbrUserInformation.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.sbrUserInformation.Location = New System.Drawing.Point(0, 500)
        Me.sbrUserInformation.Name = "sbrUserInformation"
        Me.sbrUserInformation.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.sbrUserInformation_Panel1})
        Me.sbrUserInformation.ShowPanels = True
        Me.sbrUserInformation.Size = New System.Drawing.Size(621, 17)
        Me.sbrUserInformation.SizingGrip = False
        Me.sbrUserInformation.TabIndex = 0
        '
        'sbrUserInformation_Panel1
        '
        Me.sbrUserInformation_Panel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
        Me.sbrUserInformation_Panel1.Name = "sbrUserInformation_Panel1"
        Me.sbrUserInformation_Panel1.Width = 621
        '
        'panDmmDisplay
        '
        Me.panDmmDisplay.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.panDmmDisplay.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panDmmDisplay.Controls.Add(Me.txtCtDisplay)
        Me.panDmmDisplay.Enabled = False
        Me.panDmmDisplay.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panDmmDisplay.ForeColor = System.Drawing.SystemColors.ControlText
        Me.panDmmDisplay.Location = New System.Drawing.Point(8, 0)
        Me.panDmmDisplay.Name = "panDmmDisplay"
        Me.panDmmDisplay.Size = New System.Drawing.Size(596, 50)
        Me.panDmmDisplay.TabIndex = 94
        '
        'txtCtDisplay
        '
        Me.txtCtDisplay.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCtDisplay.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCtDisplay.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCtDisplay.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCtDisplay.Location = New System.Drawing.Point(5, 5)
        Me.txtCtDisplay.Name = "txtCtDisplay"
        Me.txtCtDisplay.Size = New System.Drawing.Size(586, 35)
        Me.txtCtDisplay.TabIndex = 95
        Me.txtCtDisplay.Text = "Ready"
        '
        'frmHPE1420B
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(621, 517)
        Me.Controls.Add(Me.cmdHelp)
        Me.Controls.Add(Me.cmdQuit)
        Me.Controls.Add(Me.cmdReset)
        Me.Controls.Add(Me.tabUserOptions)
        Me.Controls.Add(Me.sbrUserInformation)
        Me.Controls.Add(Me.panDmmDisplay)
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmHPE1420B"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Counter/Timer"
        Me.tabUserOptions.ResumeLayout(False)
        Me.tabUserOptions_Page1.ResumeLayout(False)
        Me.fraChannel.ResumeLayout(False)
        Me.fraCh1Range.ResumeLayout(False)
        Me.panRange.ResumeLayout(False)
        Me.panRange.PerformLayout()
        CType(Me.spnRange, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraAperture.ResumeLayout(False)
        Me.panAperture.ResumeLayout(False)
        Me.panAperture.PerformLayout()
        CType(Me.spnAperture, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraFunction.ResumeLayout(False)
        Me.fraFunction.PerformLayout()
        Me.fraRouting.ResumeLayout(False)
        Me.tabUserOptions_Page2.ResumeLayout(False)
        Me.fraCh2Impedance.ResumeLayout(False)
        Me.fraCh1Impedance.ResumeLayout(False)
        Me.fraCh1Coupling.ResumeLayout(False)
        Me.fraCh2Coupling.ResumeLayout(False)
        Me.fraCh2Attenuation.ResumeLayout(False)
        Me.fraCh1Attenuation.ResumeLayout(False)
        Me.tabUserOptions_Page3.ResumeLayout(False)
        Me.fraTriggerOutput.ResumeLayout(False)
        Me.FrameChannel1TriggerSetup.ResumeLayout(False)
        Me.fraCh1Hysteresis.ResumeLayout(False)
        Me.fraCh1AbsoluteTrigger.ResumeLayout(False)
        Me.panCh1AbsoluteTrigger.ResumeLayout(False)
        Me.panCh1AbsoluteTrigger.PerformLayout()
        CType(Me.spnCh1AbsoluteTrigger, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraCh1TriggerSlope.ResumeLayout(False)
        Me.fraCh1RelativeTrigger.ResumeLayout(False)
        Me.panCh1RelativeTrigger.ResumeLayout(False)
        Me.panCh1RelativeTrigger.PerformLayout()
        CType(Me.spnCh1RelativeTrigger, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraCh1TriggeringMethod.ResumeLayout(False)
        Me.FrameChannel2TriggerSetup.ResumeLayout(False)
        Me.fraCh2Hysteresis.ResumeLayout(False)
        Me.fraCh2TriggerSlope.ResumeLayout(False)
        Me.fraCh2AbsoluteTrigger.ResumeLayout(False)
        Me.panCh2AbsoluteTrigger.ResumeLayout(False)
        Me.panCh2AbsoluteTrigger.PerformLayout()
        CType(Me.spnCh2AbsoluteTrigger, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraCh2RelativeTrigger.ResumeLayout(False)
        Me.panCh2RelativeTrigger.ResumeLayout(False)
        Me.panCh2RelativeTrigger.PerformLayout()
        CType(Me.spnCh2RelativeTrigger, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraCh2TriggeringMethod.ResumeLayout(False)
        Me.tabUserOptions_Page4.ResumeLayout(False)
        Me.fraArmStartLevel.ResumeLayout(False)
        Me.fraArmStopLevel.ResumeLayout(False)
        Me.fraArmStopSlope.ResumeLayout(False)
        Me.fraArmStartSlope.ResumeLayout(False)
        Me.fraArmStartSource.ResumeLayout(False)
        Me.fraArmStopSource.ResumeLayout(False)
        Me.tabUserOptions_Page5.ResumeLayout(False)
        Me.fraAcquisitionTimeout.ResumeLayout(False)
        Me.fraTiDelay.ResumeLayout(False)
        Me.panTiDelay.ResumeLayout(False)
        Me.panTiDelay.PerformLayout()
        CType(Me.spnTiDelay, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraTiGateAvg.ResumeLayout(False)
        Me.fraTimeOut.ResumeLayout(False)
        Me.panTimeOut.ResumeLayout(False)
        Me.panTimeOut.PerformLayout()
        CType(Me.spnTimeOut, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraTotalizeGatePolarity.ResumeLayout(False)
        Me.fraTotalizeGateState.ResumeLayout(False)
        Me.fraTimebaseSource.ResumeLayout(False)
        Me.tabUserOptions_Page6.ResumeLayout(False)
        CType(Me.sbrUserInformation_Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panDmmDisplay.ResumeLayout(False)
        Me.panDmmDisplay.PerformLayout()
        Me.ResumeLayout(False)

End Sub
#End Region

    '=========================================================
    '**************************************************************
    '**************************************************************
    '** ManTech Test Systems Software Module                     **
    '**                                                          **
    '** Nomenclature   : SAIS:Universal Counter/Timer Front Panel**
    '** Purpose        : This form displays the user interface   **
    '**                  for the Universal Counter / Timer SAIS  **
    '** Program Begins Executing Instructions In Sub:MAIN        **
    '**************************************************************
    '**************************************************************
    Private Const SW_SHOWNORMAL As Short = 1
    Private Const SW_SHOWMINIMIZED As Short = 2
    Private Const SW_SHOWMAXIMIZED As Short = 3

    Public UpdatingFromFile As Boolean = False

    Private Declare Function ShellExecute Lib "shell32.dll" Alias "ShellExecuteA" (ByVal hwnd As Integer, ByVal lpOperation As String, ByVal lpFile As String, ByVal lpParameters As String, ByVal lpDirectory As String, ByVal nShowCmd As Integer) As Integer

    Public Sub DispTxt(ByVal SetIndex As Short)
        Dim Unit As String
        Dim Number As Double
        Dim Res As Short

        Number = (Val(SetCur(SetIndex)) / Val(SetUOM(SetIndex)))
        Select Case SetIndex
            Case RANGE
                Unit = ModeUnit
                Res = 3
                txtRange.Text = EngNotate(Number, Res, Unit)
            Case APERTURE
                Unit = "Sec"
                Res = 3
                txtAperture.Text = EngNotate(Number, Res, Unit)
            Case ABS_TRIGGER
                Unit = "Volts"
                Res = 4
                txtCh1AbsoluteTrigger.Text = EngNotate(Number, Res, Unit)
            Case REL_TRIGGER
                Unit = "%"
                Res = 0
                txtCh1RelativeTrigger.Text = EngNotate(Number, Res, Unit)
            Case TI_DELAY
                Unit = "Sec"
                Res = 3
                txtTiDelay.Text = EngNotate(Number, Res, Unit)
            Case DUR_TIMEOUT
                Unit = "Sec"
                Res = 1
                txtTimeOut.Text = EngNotate(Number, Res, Unit)
            Case ABS_TRIG_CHAN2
                Unit = "Volts"
                Res = 4
                txtCh2AbsoluteTrigger.Text = EngNotate(Number, Res, Unit)
            Case REL_TRIG_CHAN2
                Unit = "%"
                Res = 0
                txtCh2RelativeTrigger.Text = EngNotate(Number, Res, Unit)
        End Select

        Select Case SetIndex
            Case Is = RANGE
                SetCONFigureOptions()
            Case Is = APERTURE, ABS_TRIGGER, REL_TRIGGER, TI_DELAY, DUR_TIMEOUT, ABS_TRIG_CHAN2, REL_TRIG_CHAN2
                SetSENSeOptions()
        End Select
    End Sub

    Public Sub cboArmStartSource_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboArmStartSource.SelectedIndexChanged
        Dim strArm1 As String 'Added 01/04/01 DJoiner
        Dim strArm2 As String

        'Added 01/03/01 DJoiner
        strArm1 = Me.cboArmStartSource.Text 'Assign cbo values to variables
        strArm2 = Me.cboArmStopSource.Text
        'If changing an ARM setting from "Hold", Change other ARM setting to "Immediate Trigger"
        'To prevent INIT1 or INIT2 Error Number -213
        If strArm1 <> "Hold" And strArm2 = "Hold" And Me.cmdArmStart.Visible = True Then
            Me.cboArmStopSource.Text = "Immediate Trigger"
        End If
        If strArm2 <> "Hold" And strArm1 = "Hold" And Me.cmdArmStart.Visible = True Then
            Me.cboArmStartSource.Text = "Immediate Trigger"
        End If

        cboArmStartSource.Enabled = False

        Select Case Me.cboArmStartSource.Text
            Case "Immediate Trigger"
                Me.fraArmStartLevel.Visible = False
                Me.fraArmStartSlope.Visible = False
            Case "External Trigger"
                Me.fraArmStartLevel.Visible = True
                Me.fraArmStartSlope.Visible = True
            Case "VXI Bus Trigger"
                Me.fraArmStartLevel.Visible = False
                Me.fraArmStartSlope.Visible = False
            Case "Hold"
                'Code changed DJoiner 11/22/00
                If Me.cboArmStartSource.Text = "Hold" And Me.cboArmStopSource.Text <> "Hold" Then                    'If "Hold" is selected as Start source
                    Me.cboArmStopSource.Text = "Hold" 'then also select Stop source as "Hold"
                    cboArmStartSource.Enabled = True
                    fraArmStartLevel.Visible = False 'Added to reset Level and Slope Frames
                    fraArmStartSlope.Visible = False 'DJoiner 01/04/01
                    Exit Sub
                End If 'Code changed DJoiner 11/22/00
                If Me.cboArmStartSource.Text <> "Hold" And Me.cboArmStopSource.Text = "Hold" Then                    'If "Hold" is selected as Stop source
                    Me.cboArmStartSource.Text = "Hold" 'then also select Start source as "Hold"
                    cboArmStartSource.Enabled = True
                    fraArmStartLevel.Visible = False 'Added to reset Level and Slope Frames
                    fraArmStartSlope.Visible = False 'DJoiner 01/04/01
                    Exit Sub
                End If

                fraArmStartLevel.Visible = False
                fraArmStartSlope.Visible = False
            Case "TTL Trigger 7"
                fraArmStartLevel.Visible = False
                fraArmStartSlope.Visible = True
            Case "TTL Trigger 6"
                fraArmStartLevel.Visible = False
                fraArmStartSlope.Visible = True
            Case "TTL Trigger 5"
                fraArmStartLevel.Visible = False
                fraArmStartSlope.Visible = True
            Case "TTL Trigger 4"
                fraArmStartLevel.Visible = False
                fraArmStartSlope.Visible = True
            Case "TTL Trigger 3"
               fraArmStartLevel.Visible = False
                fraArmStartSlope.Visible = True
            Case "TTL Trigger 2"
                fraArmStartLevel.Visible = False
                fraArmStartSlope.Visible = True
            Case "TTL Trigger 1"
                fraArmStartLevel.Visible = False
                fraArmStartSlope.Visible = True
            Case "TTL Trigger 0"
                fraArmStartLevel.Visible = False
                fraArmStartSlope.Visible = True
                'Place holder for 'hold" when not available
            Case "Not Available"
                'Added DJoiner 11/29/00
                Me.cboArmStartSource.SelectedIndex = 11
                MsgBox("This selection is not available in selected Mode.", MsgBoxStyle.Information, "Selection Not Available")
        End Select

        cboArmStartSource.Enabled = True
        'Fix Overlaping Frames
        Me.fraArmStartSource.BringToFront()

        SetARMOptions()

        If Me.cboArmStartSource.Text = "Hold" And Not bTIP_Running And Me.cboArmStopSource.Text = "Hold" Then
            'Set Timeout Duration to 60 Sec when using Manual Gating
            ' except in a TIP mode, use designed timeout value
            txtTimeOut.Text = 60 'Code changed DJoiner 11/28/00
            Me.optAcquisitionTimeoutOn.Checked = True
            txtTimeOut_Leave(Me, New EventArgs())
            MsgBox("The Acquisition TimeOut Duration has been set to 60 Sec. " & vbCrLf & "If this is not enough time to complete your Measurement, " & vbCrLf & "Please Set the TimeOut Duration Higher.", MsgBoxStyle.Information, "TimeOut Duration has been set")
            optAcquisitionTimeoutOff.Enabled = False
            optAcquisitionTimeoutStart.Enabled = False
        Else
            optAcquisitionTimeoutOff.Enabled = True
            optAcquisitionTimeoutStart.Enabled = True
        End If
    End Sub

    Public Sub cboArmStopSource_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboArmStopSource.SelectedIndexChanged
        Dim strArm1 As String 'Added 01/04/01 DJoiner
        Dim strArm2 As String

        'Added 01/03/01 DJoiner
        strArm1 = Me.cboArmStartSource.Text 'Assign cbo values to variables
        strArm2 = Me.cboArmStopSource.Text
        'If changing an ARM setting from "Hold", Change other ARM setting to "Immediate Trigger"
        'To prevent INIT1 or INIT2 Error Number -213
        If strArm1 <> "Hold" And strArm2 = "Hold" And Me.cmdArmStart.Visible = True Then
            Me.cboArmStopSource.Text = "Immediate Trigger"
        End If
        If strArm2 <> "Hold" And strArm1 = "Hold" And Me.cmdArmStart.Visible = True Then
            Me.cboArmStartSource.Text = "Immediate Trigger"
        End If

        cboArmStopSource.Enabled = False

        Select Case Me.cboArmStopSource.Text
            Case "Immediate Trigger"
                Me.fraArmStopLevel.Visible = False
                Me.fraArmStopSlope.Visible = False
            Case "External Trigger"
                Me.fraArmStopLevel.Visible = True
                Me.fraArmStopSlope.Visible = True
            Case "VXI Bus Trigger"
                Me.fraArmStopLevel.Visible = False
                Me.fraArmStopSlope.Visible = False
            Case "Hold"
                'Code changed DJoiner 11/22/00
                If Me.cboArmStartSource.Text = "Hold" And Me.cboArmStopSource.Text <> "Hold" Then                    'If "Hold" is selected as Start source
                    Me.cboArmStopSource.Text = "Hold" 'then also select Stop source as "Hold"
                    cboArmStopSource.Enabled = True
                    Me.fraArmStopLevel.Visible = False 'Added to reset Level and Slope Frames
                    Me.fraArmStopSlope.Visible = False 'DJoiner 01/04/01
                    Exit Sub
                End If 'Code changed DJoiner 11/22/00
                If Me.cboArmStartSource.Text <> "Hold" And Me.cboArmStopSource.Text = "Hold" Then                    'If "Hold" is selected as Stop source
                    Me.cboArmStartSource.Text = "Hold" 'then also select Start source as "Hold"
                    cboArmStopSource.Enabled = True
                    Me.fraArmStopLevel.Visible = False 'Added to reset Level and Slope Frames
                    Me.fraArmStopSlope.Visible = False 'DJoiner 01/04/01
                    Exit Sub
                End If

                Me.fraArmStopLevel.Visible = False
                Me.fraArmStopSlope.Visible = False
            Case "TTL Trigger 7"
                Me.fraArmStopLevel.Visible = False
                Me.fraArmStopSlope.Visible = True
            Case "TTL Trigger 6"
                Me.fraArmStopLevel.Visible = False
                Me.fraArmStopSlope.Visible = True
            Case "TTL Trigger 5"
                Me.fraArmStopLevel.Visible = False
                Me.fraArmStopSlope.Visible = True
            Case "TTL Trigger 4"
                Me.fraArmStopLevel.Visible = False
                Me.fraArmStopSlope.Visible = True
            Case "TTL Trigger 3"
                Me.fraArmStopLevel.Visible = False
                Me.fraArmStopSlope.Visible = True
            Case "TTL Trigger 2"
                Me.fraArmStopLevel.Visible = False
                Me.fraArmStopSlope.Visible = True
            Case "TTL Trigger 1"
                Me.fraArmStopLevel.Visible = False
                Me.fraArmStopSlope.Visible = True
            Case "TTL Trigger 0"
                Me.fraArmStopLevel.Visible = False
                Me.fraArmStopSlope.Visible = True
                'Place holder for 'hold" when not available
            Case "Not Available"
                'Added DJoiner 11/29/00
                Me.cboArmStopSource.SelectedIndex = 11
                MsgBox("This selection is not available in selected Mode.", MsgBoxStyle.Information, "Selection Not Available")
        End Select

        cboArmStopSource.Enabled = True
        'Fix Overlaping Frames
        Me.fraArmStartSource.BringToFront()

        SetARMOptions()

        If Me.cboArmStartSource.Text = "Hold" And Not bTIP_Running And Me.cboArmStopSource.Text = "Hold" Then
            'Set Timeout Duration to 60 Sec when using Manual Gating
            ' except in a TIP mode, use designed timeout value
            txtTimeOut.Text = 60 'Code changed DJoiner 11/28/00
            Me.optAcquisitionTimeoutOn.Checked = True
            Me.txtTimeOut_Leave(Me, New EventArgs())
            MsgBox("The Acquisition TimeOut Duration has been set to 60 Sec. " & vbCrLf & "If this is not enough time to complete your Measurement, " & vbCrLf & "Please Set the TimeOut Duration Higher.", MsgBoxStyle.Information, "TimeOut Duration has been set")
            optAcquisitionTimeoutOff.Enabled = False
            optAcquisitionTimeoutStart.Enabled = False
        Else
            optAcquisitionTimeoutOff.Enabled = True
            optAcquisitionTimeoutStart.Enabled = True
        End If
    End Sub

    Private Sub cboTriggerOutput_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboTriggerOutput.SelectedIndexChanged
        AdjustTriggerOutput()
    End Sub

    Public Sub chkCh1AbsTrigAuto_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkCh1AbsTrigAuto.CheckedChanged
        'Only act on control if enabled (when changing values with software in TIPs)
        If Not UpdatingFromFile And (chkCh1AbsTrigAuto.Enabled = False Or chkCh1AbsTrigAuto.Visible = False) Then Exit Sub

        chkCh1AbsTrigAuto.Enabled = False

        If chkCh1AbsTrigAuto.Checked Then
            fraCh1AbsoluteTrigger.Visible = False
            fraCh1RelativeTrigger.Visible = True
        Else
            fraCh1AbsoluteTrigger.Visible = True
            fraCh1RelativeTrigger.Visible = False
        End If
        'This Fixes Overlaping Frames
        Me.fraCh1RelativeTrigger.BringToFront()
        Me.fraCh1AbsoluteTrigger.BringToFront()
        Me.fraCh1TriggeringMethod.BringToFront()

        SetSENSeOptions()
        chkCh1AbsTrigAuto.Enabled = True 'Reset value to true
    End Sub

    Private Sub chkCh2AbsTrigAuto_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkCh2AbsTrigAuto.CheckedChanged
        'Only act on control if enabled (when changing values with software in TIPs)
        If Not UpdatingFromFile And (chkCh2AbsTrigAuto.Enabled = False Or chkCh2AbsTrigAuto.Visible = False) Then Exit Sub

        chkCh2AbsTrigAuto.Enabled = False

        If chkCh2AbsTrigAuto.Checked Then
            fraCh2AbsoluteTrigger.Visible = False ' Added D.Masters 3/26/2009
            fraCh2RelativeTrigger.Visible = True ' Added D. Masters 3/26/2009
        Else
            fraCh2AbsoluteTrigger.Visible = True ' Added D.Masters 3/26/2009
            fraCh2RelativeTrigger.Visible = False ' Added D. Masters 3/26/2009
        End If

        'This Fixes Overlaping Frames
        Me.fraCh2RelativeTrigger.BringToFront()
        Me.fraCh2AbsoluteTrigger.BringToFront()
        Me.fraCh2TriggeringMethod.BringToFront()

        SetSENSeOptions()
        chkCh2AbsTrigAuto.Enabled = True 'Reset value to true
    End Sub

    Private Sub chkContinuous_Click(ByVal sender As Object, ByVal e As EventArgs) Handles chkContinuous.Click
        If chkContinuous.Checked = False Then
            If sTIP_Mode = "TIP_RUNPERSIST" And Not bolErrorReset Then ExitCounterTimer()
            cmdMeasure.Enabled = True
        End If

        If sTIP_Mode = "TIP_RUNPERSIST" And bolErrorReset Then
            '        chkContinuous.Value = True
            cmdMeasure_Click()
        End If
    End Sub

    Private Sub chkContinuous_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles chkContinuous.MouseMove
        SendStatusBarMessage("Measure data continuously.")
    End Sub

    Private Sub chkOutputTimebase_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkOutputTimebase.CheckedChanged
        'Only act on control if enabled (when changing values with software in TIPs)
        If Not UpdatingFromFile And (chkOutputTimebase.Enabled = False Or chkOutputTimebase.Visible = False) Then Exit Sub
        SetOUTPutOptions()
    End Sub

    Public Sub chkRange_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkRange.CheckedChanged
        'Only act on control if enabled (when changing values with software in TIPs)
        If Not UpdatingFromFile And (chkRange.Enabled = False Or chkRange.Visible = False) Then Exit Sub

        chkRange.Enabled = False

        If chkRange.Checked Then
            panRange.Visible = False
        Else
            panRange.Visible = True
        End If

        SetSENSeOptions()

        chkRange.Enabled = True
    End Sub

    Public Sub chkTIDelay_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTIDelay.CheckedChanged
        'Only act on control if enabled (when changing values with software in TIPs)
        If Not UpdatingFromFile And (chkTIDelay.Enabled = False Or chkTIDelay.Visible = False) Then Exit Sub
        If chkTIDelay.Checked Then
            panTiDelay.Visible = True
        Else
            panTiDelay.Visible = False
        End If
        SetSENSeOptions()
    End Sub

    Public Sub chkTotalizeGateState_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTotalizeGateState.CheckedChanged
        'Only act on control if enabled (when changing values with software in TIPs)
        If Not UpdatingFromFile And (chkTotalizeGateState.Enabled = False Or chkTotalizeGateState.Visible = False) Then Exit Sub
        chkTotalizeGateState.Enabled = False

        If chkTotalizeGateState.Checked Then
            fraTotalizeGatePolarity.Visible = True
        Else
            fraTotalizeGatePolarity.Visible = False
        End If
        'Fix Overlaping Frames
        Me.fraTotalizeGatePolarity.BringToFront()
        Me.fraTotalizeGateState.BringToFront()
        Me.fraTimebaseSource.BringToFront()

        SetSENSeOptions()
        chkTotalizeGateState.Enabled = True
    End Sub

    Private Sub cmdAbout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAbout.Click
        Me.chkContinuous.Checked = False
        frmAbout.cmdOk.Visible = True
        frmAbout.ShowDialog()
    End Sub

    Private Sub cmdAbout_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmdAbout.MouseMove
        SendStatusBarMessage("Get instrument information.")
    End Sub

    Public Function Build_Atlas() As String
        Build_Atlas = ""

        Dim sTestString As String = ""
        Dim nVolt As Single

        Select Case InstrumentMode
            Case 1
                'Frequency

                sTestString = "MEASURE, (FREQ INTO MEASUREMENT), AC SIGNAL," & vbCrLf

                If chkRange.Checked = True Then
                    If optInputChannel1.Checked = True Then
                        sTestString &= "FREQ MAX 200.000 MHz" & vbCrLf
                    Else
                        sTestString &= "FREQ MAX 100.000 MHz" & vbCrLf
                    End If
                Else
                    sTestString &= "FREQ MAX " & txtRange.Text & vbCrLf
                End If

                'voltage
                'NOTE: Where is this value?

                'Options
                sTestString &= BuildOptions()

                sTestString &= "CNX HI $"

                'SupplyData(EditSupply%).FuncVoltVal & "," & vbCrLf & "CURRENT MAX " & SupplyData(EditSupply%).FuncAmpVal & "," & vbCrLf & "CNX HI J1-1 LO J1-2 $"

            Case 2
                'Peroid
                sTestString = "MEASURE, (PERIOD INTO MEASUREMENT) AC SIGNAL," & vbCrLf & "PERIOD MAX " & txtRange.Text & vbCrLf
                'voltage
                'NOTE: Where is this value?

                'Options
                sTestString &= BuildOptions()

                sTestString &= "CNX HI $"

                '"  '"CURRENT MAX " & SupplyData(EditSupply%).FuncAmpVal & "," & vbCrLf & "CNX HI J1-1 LO J1-2 $"

            Case 3
                'Frequency Ratio
                sTestString = "MEASURE, (FREQ-RATIO INTO MEASUREMENT) AC SIGNAL" & vbCrLf & "FREQ-RATIO MAX, " & txtRange.Text & vbCrLf
                'voltage
                'NOTE: Where is this value?

                'Options
                sTestString &= BuildOptions()

                sTestString &= "CNX HI $"

            Case 4
                'Rise Time
                sTestString = "MEASURE, (RISE-TIME INTO MEASUREMENT) PULSED CD," & vbCrLf & "RISE-TIME MAX, " & txtRange.Text & vbCrLf

                'voltage-P
                'NOTE: Where is this value?

                'Options
                sTestString &= BuildOptions()

                sTestString &= "CNX VIA $"

            Case 5
                'Fall Time
                sTestString = "MEASURE, (FALL-TIME INTO MEASUREMENT), PULSED CD" & vbCrLf & "FALL-TIME MAX " & txtRange.Text & vbCrLf

                'voltage-P
                'NOTE: Where is this value?

                'Options
                sTestString &= BuildOptions()

                sTestString &= "CNX VIA $"

            Case 6
                'Time interval
                sTestString = "MEASURE, (TIME INTO MEASUREMENT), TIME INTERVAL" & vbCrLf & "TIME MAX " & txtRange.Text & vbCrLf

                sTestString &= "FROM " & "<event lable-1>" & " TO " & "<event lable-2>" & "," & " MAX-TIME " & txtTimeOut.Text & " $"

            Case 7
                'POS-PULSE-WIDTH
                sTestString = "MEASURE, (POS-PULSE-WIDTH INTO MEASUREMENT), PULSED DC" & vbCrLf & "POS-PULSE-WIDTH MAX, " & txtRange.Text & vbCrLf

                'voltage-P
                'NOTE: Where is this value?

                'Options
                sTestString &= BuildOptions()

                sTestString &= "CNX VIA $"

            Case 8
                'NEG-PULSE-WIDTH
                sTestString = "MEASURE, (NEG-PULSE-WIDTH INTO MEASUREMENT), PULSED DC" & vbCrLf & "NEG-PULSE-WIDTH MAX, " & txtRange.Text & vbCrLf

                'voltage-P
                'NOTE: Where is this value?

                'Options
                sTestString &= BuildOptions()

                sTestString &= "CNX VIA $"

            Case 9
                'TZ
                sTestString = "MEASURE, (COUNT INTO MEASUREMENT), EVENTS" & vbCrLf & "COUNT MAX " & "<dec-value>" & "TIMES," & vbCrLf

                'trig/voltage
                If chkCh1AbsTrigAuto.Checked = False Then
                    Dim nMult As Short
                    nMult = 1

                    nVolt = Val(SetCur(ABS_TRIGGER))
                    If optInputChannel1.Checked = True Then
                        If optCh1Attenuation1X.Checked = True Then nMult = 10
                    Else
                        If optCh2Attenuation1X.Checked = True Then nMult = 10
                    End If
                    nVolt *= nMult
                    sTestString &= "TRIG " & nVolt & " V," & vbCrLf
                End If

                'Options
                sTestString &= BuildOptions()

                sTestString &= "CNX VIA $"

            Case 10
                'PHASE-ANGLE
                sTestString = "MEASURE, (PHASE-ANGLE INTO MEASUREMENT), AC SIGNAL" & vbCrLf & "PHASE-ANGLE MAX" & txtRange.Text & vbCrLf

                'voltage
                'NOTE: Where is this value?

                'Options
                sTestString &= BuildOptions()

                sTestString &= "CNX HI $"

        End Select
        ' ADVINT REMOVED
        ' ADVINT REMOVED    If optImpedanceCh1(1).Value = True Then
        ' ADVINT REMOVED        steststring = steststring & "TEST-EQUIP-IMP 1 MOHM," & vbCrLf
        ' ADVINT REMOVED    End If
        ' ADVINT REMOVED
        ' ADVINT REMOVED    If optImpedanceCh1(2).Value = True Then
        ' ADVINT REMOVED            steststring = steststring & "TEST-EQUIP-IMP 50 OHM," & vbCrLf
        ' ADVINT REMOVED    End If

        '
        ' ADVINT REMOVED    If optCouplingCh1(1).Value = True Then
        ' ADVINT REMOVED        steststring = steststring & "COUPLING DC," & vbCrLf
        ' ADVINT REMOVED    End If

        ' ADVINT REMOVED    If optCouplingCh1(2).Value = True Then
        ' ADVINT REMOVED            steststring = steststring & "COUPLING AC," & vbCrLf
        ' ADVINT REMOVED    End If
        '
        ' ADVINT REMOVED    If optInputChannel(1).Value = True Then
        ' ADVINT REMOVED           steststring = steststring & "CNX HI CNTR-CH1 $"
        ' ADVINT REMOVED    End If
        ' ADVINT REMOVED
        ' ADVINT REMOVED    If optInputChannel(2).Value = True Then
        ' ADVINT REMOVED           steststring = steststring & "CNX HI CNTR-CH2 $"
        ' ADVINT REMOVED    End If
        '
        '        MEASURE, (NEG-PULSE-WIDTH), PULSED DC,
        '          NEG-PULSE-WIDTH MAX 'M-MAX',
        '          TEST-EQUIP-IMP 50 OHM,
        '          Max -Time 'MAX-T' RANGE 0 SEC TO 2000 SEC,
        '          CNX HI LO $'''
        '
        '        MEASURE, (FREQ), AC SIGNAL,
        '          FREQ Max 'M-MAX',
        '          VOLTAGE 'AC-VOLTS-RMS' RANGE 0.1 V TO 100 V,
        '          DC -OFFSET 'DC-VOLTS' RANGE -100 V TO 100 V,
        '          TEST-EQUIP-IMP 50 OHM,
        '          Max -Time 'MAX-T' RANGE 0 SEC TO 2000 SEC,
        '          CNX HI CNTR-CH1 $'
        '
        '(IF YOU USE THE AUTO RANGE,USE THE HIGHEST RANGE) (200
        '
        '     30 VERIFY, (PERIOD INTO 'MEASUREMENT'), AC SIGNAL,
        '          UL 'VER-UL' SEC LL 'VER-LL' SEC,
        '          PERIOD Max 'M-MAX' RANGE .000000005 SEC TO 1000 SEC,
        '          VOLTAGE -P 'AC-VOLTS-P' RANGE 0.1414 V TO 141.42 V,
        '          TEST-EQUIP-IMP 50 OHM,
        '          Max -Time 'MAX-T' RANGE 0 SEC TO 2000 SEC,
        '          COUPLING AC,
        '          CNX HI CNTR-CH1 $'
        ''
        '
        '     40 VERIFY, (FREQ-RATIO INTO 'MEASUREMENT'), AC SIGNAL,
        '          UL 'VER-UL' LL 'VER-LL',
        '          FREQ-RATIO MAX 'M-MAX' RANGE .00000000001 TO 100000000000,
        '          VOLTAGE -PP 'AC-VOLTS-PP' RANGE 0.2828 V TO 282.84 V,
        '          TEST-EQUIP-IMP 50 OHM,
        '          Max -Time 'MAX-T' RANGE 0 SEC TO 2000 SEC,
        '          CNX HI CNTR-CH1 REF VIA CNTR-CH2 $'
        '
        '
        '     50 MEASURE, (PHASE-ANGLE INTO 'MEASUREMENT'), AC SIGNAL,
        '          PHASE-ANGLE MAX 'M-MAX' RANGE 0 DEG TO 359.9 DEG,
        '          TEST-EQUIP-IMP 50 OHM,
        '          Max -Time 'MAX-T' RANGE 0 SEC TO 2000 SEC,
        '          CNX HI CNTR-CH1 REF VIA CNTR-CH2 $'
        ''
        '
        '???? -->
        '     20 VERIFY, (PRF INTO 'MEASUREMENT'), PULSED DC,
        '          UL 'VER-UL' HZ LL 'VER-LL' HZ,
        '          PRF Max 'M-MAX' RANGE 0.001 HZ TO 200000000 HZ,
        '          VOLTAGE -P 'PULSE-VOLTS' RANGE 0.1 V TO 100 V,
        '          DC -OFFSET 'DC-VOLTS' RANGE -100 V TO 100 V,
        '          TEST-EQUIP-IMP 50 OHM,
        '          Max -Time 'MAX-T' RANGE 0 SEC TO 2000 SEC,
        '          CNX HI CNTR-CH1 $'
        ''
        '
        '(TZ) (totalize)
        '     20 VERIFY, (COUNT INTO 'MEASUREMENT'), EVENTS,
        '          UL 'VER-UL' TIMES LL 'VER-LL' TIMES,
        '          Count Max 'M-MAX' RANGE 0 TIMES TO 999999999999 TIMES,
        '          TRIG 'TRIG-VOLTS' RANGE 0.1 V TO 100 V,
        '          VOLTAGE -P 'SQ-VOLTS-P' RANGE 0.1 V TO 100 V,
        '          TEST-EQUIP-IMP 50 OHM,
        '          Max -Time 'MAX-T' RANGE 0 SEC TO 2000 SEC,
        '          POS-SLOPE,
        '          CNX VIA CNTR-CH1 GATE-IN CNTR-CH2 $
        '(TI)
        '        ENABLE, EVENT 'STARTTIME', 'STOPTIME' $
        '     30 VERIFY, (TIME INTO 'MEASUREMENT'), TIME INTERVAL,
        '          UL 'VER-UL' SEC LL 'VER-LL' SEC,
        '          Time Max 'M-MAX' RANGE .000000001 SEC TO 1000 SEC,
        '          From 'STARTTIME' TO 'STOPTIME',
        '          Max -Time 'MAX-T' RANGE 0 SEC TO 2000 SEC $
        '        DISABLE, EVENT 'STARTTIME', 'STOPTIME' $'
        '
        '
        '
        '   Return ATLAS Statement
        Build_Atlas = sTestString
    End Function

    Private Function BuildOptions() As String
        BuildOptions = ""
        Dim sTestString As String

        'test equip imp
        sTestString = "TEST-EQUIP-IMP "
        If optInputChannel1.Checked = True Then
            If optCh1Impedance50Ohm.Checked = True Then
                sTestString &= "50 OHM "
            Else
                sTestString &= "1 MOHM "
            End If

            If optCh1CouplingDC.Checked = True Then
                sTestString &= "COUPLING DC," & vbCrLf
            Else
                sTestString &= "COUPLING AC," & vbCrLf
            End If
        Else
            If optCh2Impedance50Ohm.Checked = True Then
                sTestString &= "50 OHM "
            Else
                sTestString &= "1 MOHM "
            End If

            If optCh2CouplingDC.Checked = True Then
                sTestString &= "COUPLING DC," & vbCrLf
            Else
                sTestString &= "COUPLING AC," & vbCrLf
            End If
        End If

        'max time
        If optAcquisitionTimeoutOff.Checked = False Then
            sTestString &= "MAX-TIME " & txtTimeOut.Text & "," & vbCrLf
        End If

        'strobe to event
        If optTriggerOutputOn.Checked = True Then
            sTestString &= "STROBE-TO-EVENT" & "<event lable>" & " MAX-TIME " & "<dec value>" & " SEC," & vbCrLf
        End If

        'gated from
        If Not (cboArmStartSource.Text = "Immediate Trigger") Or Not (cboArmStopSource.Text = "Immediate Trigger") Then
            sTestString &= "GATED FROM " & "<event lable-1>" & " TO " & "<event lable-2>" & "," & vbCrLf
        End If
        'ADVINT REMOVED INPUT CODE THAT HAD GONE HERE...
        BuildOptions = sTestString
    End Function

    Public Sub SetMode(ByRef sMode As String)
        'Dim btn As System.Windows.Forms.ToolBarButton

        If sMode <> InstrumentMode Then
            InstrumentMode = Convert.ToInt16(sMode)
            For Each Button In tbrCtFunctions.Buttons
                Button.pushed = False
            Next
            'Depress the corresponding button on the toolbar
            If InstrumentMode > 0 And InstrumentMode < 11 Then
                Me.tbrCtFunctions.Buttons(InstrumentMode - 1).Pushed = True
            Else
                Me.tbrCtFunctions.Buttons(0).Pushed = True
            End If

            'Apply the instrument to the control
            ChangeInstrumentMode()
        End If
    End Sub

    Public Function GetMode() As String
        GetMode = InstrumentMode.ToString()
    End Function

    Public Sub ConfigGetCurrent()
        Const curOnErrorGoToLabel_Default As Integer = 0
        Const curOnErrorGoToLabel_ErrorHandle As Integer = 1
        Dim vOnErrorGoToLabel As Integer = curOnErrorGoToLabel_Default
        Dim sInstrumentCmds As String
        Dim sReadBuffer As String
        Dim nLoop As Short 'Temp looping variable
        Dim sMode As String = "" 'Measure mode
        Dim sChan As String = "" 'Active Channel
        Dim Msg As String 'Used in error messages

        Try
            If LiveMode Then 'use Not to use simulator
                sReadBuffer = ""
                'Allow use of ActivateControl
                sTIP_Mode = "GET CURR CONFIG"

                '       Items to Query
                '       Reference ChangeInstrumentMode
                '       Reference TakeMeasurement Sub

                vOnErrorGoToLabel = curOnErrorGoToLabel_ErrorHandle ' On Error GoTo ErrorHandle
                '       *********
                '       Get MODE
                '       *********
                For nLoop = 1 To 2 ' For the two inputs
                    sInstrumentCmds = "CONF" & nLoop & "?"
                    SendScpiCommand(sInstrumentCmds)

                    '##########################################
                    'Action Required: Remove debug code
                    'Fill return value selections
                    'gcolCmds.Add "Simulated response for: " & sInstrumentCmds
                    'gcolCmds.Add ""
                    'gcolCmds.Add "FREQ 3E5, 10"
                    'gcolCmds.Add "PER , "
                    'gcolCmds.Add "TOT"
                    'gcolCmds.Add "PHAS , "
                    'gcolCmds.Add "TINT , "
                    'gcolCmds.Add "RTIM 90, 10, 0.0000001"
                    'gcolCmds.Add "PWID 50, 0.0000001"
                    '##########################################

                    'Retrieve instrument output buffer
                    sReadBuffer = ReadInstrumentBuffer()

                    'Parse out Config information
                    If Len(sReadBuffer) > 0 Then

                        'Parse out Mode information
                        sMode = Mid(sReadBuffer, 1, InStr(1, sReadBuffer, " ", CompareMethod.Text) - 1)

                        If sMode <> "" Then
                            sChan = CStr(nLoop)
                            'Set Channel/Mode using ConfigureFrontPanel since this
                            ' is not availiable using ActivateControl
                            sTIP_CMDstring = ":INP" & sChan & ":SENS" & sChan & ":FUNC " & Q & sMode & Q
                            CtMain.ConfigureFrontPanel()

                            'Set Range/Freq
                            CtMain.ActivateControl(":CONF" & sChan & ":" & sReadBuffer)

                            Exit For 'If configured channel then done
                        End If
                    End If
                Next nLoop

                'Verify Mode is alowed for Channel 2
                Select Case InstrumentMode
                    Case 4 To 8
                        If sChan = "2" Then
                            MsgBox("Invalid Mode for channel 2", MsgBoxStyle.Exclamation)
                            Exit Sub
                        End If

                    Case Else
                End Select

                'Verify a Channel and Mode was found
                If sChan <> "1" And sChan <> "2" Then
                    MsgBox("No Mode for the available channels found", MsgBoxStyle.Exclamation)
                    Exit Sub
                End If

                '       ******************************
                '       Get Amplitude/Routing
                '       ******************************
                If InStr(sMode, "FREQ") Or sMode = "PER" Then

                    '**********************
                    ' SET Amplitude
                    '**********************
                    sMode = "FREQ" 'mh added because instr. returns ""FREQ so then sMode holds ""FREQ instead of "FREQ"
                    sInstrumentCmds = "SENS" & sChan & ":" & sMode & ":APER?"
                    SendScpiCommand(sInstrumentCmds)

                    '##########################################
                    'Action Required: Remove debug code
                    'Fill return value selections
                    'gcolCmds.Add "Simulated response for: " & sInstrumentCmds
                    'gcolCmds.Add "0.100"
                    'gcolCmds.Add "0.256"
                    'gcolCmds.Add "1.000"
                    'gcolCmds.Add "8.000"
                    'gcolCmds.Add "10.000"
                    'gcolCmds.Add "40.000"
                    'gcolCmds.Add "99.999"
                    '##########################################

                    'Retrieve instrument output buffer
                    sReadBuffer = ReadInstrumentBuffer()

                    CtMain.ActivateControl(":SENS" & sChan & ":" & sMode & ":APER " & Trim(sReadBuffer))

                    '**********************
                    ' SET AUTO Ranging
                    '**********************
                    If sMode = "FREQ" Then
                        sInstrumentCmds = "SENS" & sChan & ":FREQ:RANG:AUTO?"
                        SendScpiCommand(sInstrumentCmds)

                        '##########################################
                        'Action Required: Remove debug code
                        'Fill return value selections
                        'gcolCmds.Add "Simulated response for: " & sInstrumentCmds
                        'gcolCmds.Add "0"
                        'gcolCmds.Add "1"
                        '##########################################

                        'Retrieve instrument output buffer
                        sReadBuffer = ReadInstrumentBuffer()

                        If Len(sReadBuffer) > 0 Then
                            chkRange.Checked = CInt(sReadBuffer)
                            If chkRange.Checked = True Then
                                panRange.Visible = False
                            Else
                                panRange.Visible = True
                            End If
                        End If
                    End If

                ElseIf sMode = "TINT" Then

                    '**********************
                    ' SET Routing
                    '**********************
                    sInstrumentCmds = "INP" & sChan & ":ROUT?"
                    SendScpiCommand(sInstrumentCmds)

                    '##########################################
                    'Action Required: Remove debug code
                    'Fill return value selections
                    'gcolCmds.Add "Simulated response for: " & sInstrumentCmds
                    'gcolCmds.Add "COMM"
                    'gcolCmds.Add "SEP"
                    '##########################################

                    'Retrieve instrument output buffer
                    sReadBuffer = ReadInstrumentBuffer()

                    CtMain.ActivateControl(":INP" & sChan & ":ROUT " & Trim(sReadBuffer))

                End If

                '**********************
                ' SET Continuous
                '**********************
                sInstrumentCmds = "INIT" & sChan & ":CONT?"
                SendScpiCommand(sInstrumentCmds)

                '##########################################
                'Action Required: Remove debug code
                'Fill return value selections
                'gcolCmds.Add "Simulated response for: " & sInstrumentCmds
                'gcolCmds.Add "0"
                'gcolCmds.Add "1"
                '##########################################

                'Retrieve instrument output buffer
                sReadBuffer = ReadInstrumentBuffer()

                If Len(sReadBuffer) > 0 Then
                    chkContinuous.Checked = CInt(sReadBuffer)
                End If


                '*****************************Input******************************
                ConfigGetInput(sChan)

                '************************Event Trigger***************************
                ConfigGetTrigger(sChan)

                '****************************Arm*********************************
                ConfigGetArm("STAR")
                ConfigGetArm("STOP")

                '**************************Options*******************************
                ConfigGetOptions(sChan)

                '****************************************************************
                'clean-up
                sTIP_CMDstring = ""
                sTIP_Mode = ""
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
                        sTIP_CMDstring = ""
                        sTIP_Mode = ""
                    End If
                Case curOnErrorGoToLabel_Default
                    ' ...
                Case Else
                    ' ...
            End Select
        End Try
    End Sub

    Private Sub ConfigGetInput(ByVal sChan As String)
        Dim sInstrumentCmds As String
        Dim sReadBuffer As String
        Dim sTemp As String

        '   ******************************
        '   Disable the inactive channel inputs
        '   ******************************
        If InstrumentMode = 3 Then
            bDualChannel = True
        Else
            bDualChannel = False
        End If

        If sChan = "1" Then
            optCh1Attenuation1X.Enabled = True
            optCh1CouplingDC.Enabled = True
            optCh1Impedance1MOhm.Enabled = True
            optCh1Attenuation10X.Enabled = True
            optCh1CouplingAC.Enabled = True
            optCh1Impedance50Ohm.Enabled = True
            If Not bDualChannel Then
                optCh2Attenuation1X.Enabled = False
                optCh2CouplingDC.Enabled = False
                optCh2Impedance1MOhm.Enabled = False
                optCh2Attenuation10X.Enabled = False
                optCh2CouplingAC.Enabled = False
                optCh2Impedance50Ohm.Enabled = False
            End If
        Else
            optCh2Attenuation1X.Enabled = False
            optCh2CouplingDC.Enabled = False
            optCh2Impedance1MOhm.Enabled = False
            optCh2Attenuation10X.Enabled = False
            optCh2CouplingAC.Enabled = False
            optCh2Impedance50Ohm.Enabled = False
            If Not bDualChannel Then
                optCh1Attenuation1X.Enabled = True
                optCh1CouplingDC.Enabled = True
                optCh1Impedance1MOhm.Enabled = True
                optCh1Attenuation10X.Enabled = True
                optCh1CouplingAC.Enabled = True
                optCh1Impedance50Ohm.Enabled = True
            End If
        End If

        '   ******************************
        '   Get Input Attenuation
        '   ******************************

        sInstrumentCmds = "INP" & sChan & ":ATT?"
        SendScpiCommand(sInstrumentCmds)

        '##########################################
        'Action Required: Remove debug code
        'Fill return value selections
        'gcolCmds.Add "Simulated response for: " & sInstrumentCmds
        'gcolCmds.Add "10"
        'gcolCmds.Add "1"
        '##########################################

        'Retrieve instrument output buffer
        sReadBuffer = ReadInstrumentBuffer()
        sTemp = Trim(sReadBuffer)
        'Store Attenuation value for channel for later use
        ChanAttenuation(CInt(sChan)) = CInt(sTemp)

        CtMain.ActivateControl(":INP" & sChan & ":ATT " & sTemp)

        '   ******************************
        '   Get Input Coupling
        '   ******************************

        sInstrumentCmds = "INP" & sChan & ":COUP?"
        SendScpiCommand(sInstrumentCmds)

        '##########################################
        'Action Required: Remove debug code
        'Fill return value selections
        'gcolCmds.Add "Simulated response for: " & sInstrumentCmds
        'gcolCmds.Add "AC"
        'gcolCmds.Add "DC"
        '##########################################

        'Retrieve instrument output buffer
        sReadBuffer = ReadInstrumentBuffer()
        sTemp = Trim(sReadBuffer)

        CtMain.ActivateControl(":INP" & sChan & ":COUP " & sTemp)

        '   ******************************
        '   Get Input Impedance
        '   ******************************

        sInstrumentCmds = "INP" & sChan & ":IMP?"
        SendScpiCommand(sInstrumentCmds)

        '##########################################
        'Action Required: Remove debug code
        'Fill return value selections
        'gcolCmds.Add "Simulated response for: " & sInstrumentCmds
        'gcolCmds.Add "1E6"
        'gcolCmds.Add "50"
        '##########################################

        'Retrieve instrument output buffer
        sReadBuffer = ReadInstrumentBuffer()
        sTemp = Trim(sReadBuffer)

        CtMain.ActivateControl(":INP" & sChan & ":IMP " & sTemp)
    End Sub

    Private Sub ConfigGetTrigger(ByVal sChan As String)
        Dim sInstrumentCmds As String
        Dim sReadBuffer As String
        Dim sTemp As String
        Dim bAutoTrig As Boolean
        Dim nLoop As Short

        '   ********************
        '   Get Event Hysteresis
        '   ********************

        sInstrumentCmds = "SENS" & sChan & ":EVEN:HYST?"
        SendScpiCommand(sInstrumentCmds)

        '##########################################
        'Action Required: Remove debug code
        'Fill return value selections
        'gcolCmds.Add "Simulated response for: " & sInstrumentCmds
        'gcolCmds.Add "MAX"
        'gcolCmds.Add "DEF"
        'gcolCmds.Add "MIN"
        '##########################################

        'Retrieve instrument output buffer
        sReadBuffer = ReadInstrumentBuffer()
        sTemp = Trim(sReadBuffer)

        CtMain.ActivateControl(":SENS" & sChan & ":EVEN:HYST " & sTemp)

        '   ********************
        '   Get Trigger Slope
        '   ********************

        sInstrumentCmds = "SENS" & sChan & ":EVEN:SLOP?"
        SendScpiCommand(sInstrumentCmds)

        '##########################################
        'Action Required: Remove debug code
        'Fill return value selections
        'gcolCmds.Add "Simulated response for: " & sInstrumentCmds
        'gcolCmds.Add "POS"
        'gcolCmds.Add "NEG"
        '##########################################

        'Retrieve instrument output buffer
        sReadBuffer = ReadInstrumentBuffer()
        sTemp = Trim(sReadBuffer)

        CtMain.ActivateControl(":SENS" & sChan & ":EVEN:SLOP " & sTemp)

        '   ********************
        '   Get Trigger Output
        '   ********************

        For nLoop = 0 To 7
            sInstrumentCmds = "OUTP:TTLT" & nLoop & "?"
            SendScpiCommand(sInstrumentCmds)

            '##########################################
            'Action Required: Remove debug code
            'Fill return value selections
            'gcolCmds.Add "Simulated response for: " & sInstrumentCmds
            'gcolCmds.Add "0"
            'gcolCmds.Add "1"
            '##########################################

            'Retrieve instrument output buffer
            sReadBuffer = ReadInstrumentBuffer()

            If CInt(sReadBuffer) = 1 Then
                CtMain.ActivateControl(":OUTP:TTLT" & nLoop & ":STAT ON")
            Else
                CtMain.ActivateControl(":OUTP:TTLT" & nLoop & ":STAT OFF")
            End If
        Next nLoop

        Select Case InstrumentMode
            Case 4, 5, 7, 8
                'Don't get for R/T, F/T, -PW, or +PW

            Case Else

                '********************
                'Get Trigger Method
                '********************
                sInstrumentCmds = "SENS" & sChan & ":EVEN:LEV:AUTO?"
                SendScpiCommand(sInstrumentCmds)

                '##########################################
                'Action Required: Remove debug code
                'Fill return value selections
                'gcolCmds.Add "Simulated response for: " & sInstrumentCmds
                'gcolCmds.Add "0"
                'gcolCmds.Add "1"
                '##########################################

                'Retrieve instrument output buffer
                sReadBuffer = ReadInstrumentBuffer()
                bAutoTrig = CInt(sReadBuffer)

                If bAutoTrig = True Then
                    CtMain.ActivateControl(":SENS" & sChan & ":EVEN:LEV:ABS:AUTO ON")
                Else
                    CtMain.ActivateControl(":SENS" & sChan & ":EVEN:LEV:ABS:AUTO OFF")
                End If

                '********************
                ' Relative/Absolute Level
                '********************
                If bAutoTrig = True Then
                    '********************
                    'Get Relative Trigger Level
                    '********************
                    sInstrumentCmds = "SENS" & sChan & ":EVEN:LEV:REL?"
                    SendScpiCommand(sInstrumentCmds)

                    '##########################################
                    'Action Required: Remove debug code
                    'Fill return value selections
                    'gcolCmds.Add "Simulated response for: " & sInstrumentCmds
                    'gcolCmds.Add "10"
                    'gcolCmds.Add "20"
                    'gcolCmds.Add "30"
                    'gcolCmds.Add "40"
                    'gcolCmds.Add "50"
                    'gcolCmds.Add "60"
                    'gcolCmds.Add "70"
                    'gcolCmds.Add "80"
                    'gcolCmds.Add "90"
                    '##########################################

                    'Retrieve instrument output buffer
                    sReadBuffer = ReadInstrumentBuffer()
                    sTemp = Trim(sReadBuffer)

                    CtMain.ActivateControl(":SENS" & sChan & ":EVEN:LEV:REL " & sTemp)
                Else
                    '********************
                    'Get Absolute Trigger Level
                    '********************
                    sInstrumentCmds = "SENS" & sChan & ":EVEN:LEV?"
                    SendScpiCommand(sInstrumentCmds)

                    '##########################################
                    'Action Required: Remove debug code
                    'Fill return value selections
                    'gcolCmds.Add "Simulated response for: " & sInstrumentCmds
                    'gcolCmds.Add "-1.02375"
                    'gcolCmds.Add "-0.5"
                    'gcolCmds.Add "-0.2"
                    'gcolCmds.Add "-0.01"
                    'gcolCmds.Add "0"
                    'gcolCmds.Add "0.05"
                    'gcolCmds.Add "0.3"
                    'gcolCmds.Add "0.6"
                    'gcolCmds.Add "1.02375"
                    '##########################################

                    'Retrieve instrument output buffer
                    sReadBuffer = ReadInstrumentBuffer()
                    sTemp = CStr(CSng(sReadBuffer) * ChanAttenuation(CInt(sChan)))

                    CtMain.ActivateControl(":SENS" & sChan & ":EVEN:LEV:ABS " & sTemp)
                End If
        End Select
    End Sub

    Private Sub ConfigGetArm(ByVal sSeq As String)
        Dim sInstrumentCmds As String
        Dim sReadBuffer As String
        Dim sSource As String

        '   ********************
        '   Get Source
        '   ********************

        sInstrumentCmds = "ARM:" & sSeq & ":SOUR?"
        SendScpiCommand(sInstrumentCmds)

        '##########################################
        'Action Required: Remove debug code
        'Fill return value selections
        'gcolCmds.Add "Simulated response for: " & sInstrumentCmds
        'gcolCmds.Add "IMM"
        'gcolCmds.Add "EXT"
        'gcolCmds.Add "BUS"
        'gcolCmds.Add "HOLD"
        'gcolCmds.Add "TTLT0"
        'gcolCmds.Add "TTLT1"
        'gcolCmds.Add "TTLT2"
        'gcolCmds.Add "TTLT3"
        'gcolCmds.Add "TTLT4"
        'gcolCmds.Add "TTLT5"
        'gcolCmds.Add "TTLT6"
        'gcolCmds.Add "TTLT7"
        '##########################################

        'Retrieve instrument output buffer
        sReadBuffer = ReadInstrumentBuffer()
        sSource = Trim(sReadBuffer)

        CtMain.ActivateControl(":ARM:" & sSeq & ":SOUR " & sSource)

        If sSource = "EXT" Or InStr(sSource, "TTLT") Then

            '********************
            'Get Slope
            '********************

            sInstrumentCmds = "ARM:" & sSeq & ":SLOP?"
            SendScpiCommand(sInstrumentCmds)

            '##########################################
            'Action Required: Remove debug code
            'Fill return value selections
            'gcolCmds.Add "Simulated response for: " & sInstrumentCmds
            'gcolCmds.Add "POS"
            'gcolCmds.Add "NEG"
            '##########################################

            'Retrieve instrument output buffer
            sReadBuffer = ReadInstrumentBuffer()

            CtMain.ActivateControl(":ARM:" & sSeq & ":SLOP " & Trim(sReadBuffer))

            If sSource = "EXT" Then
                '********************
                'Get Level
                '********************

                sInstrumentCmds = "ARM:" & sSeq & ":LEV?"
                SendScpiCommand(sInstrumentCmds)

                '##########################################
                'Action Required: Remove debug code
                'Fill return value selections
                'gcolCmds.Add "Simulated response for: " & sInstrumentCmds
                'gcolCmds.Add "MAX"
                'gcolCmds.Add "MIN"
                'gcolCmds.Add "DEF"
                '##########################################

                'Retrieve instrument output buffer
                sReadBuffer = ReadInstrumentBuffer()

                CtMain.ActivateControl(":ARM:" & sSeq & ":LEV " & Trim(sReadBuffer))

            End If
        End If
    End Sub

    Private Sub ConfigGetOptions(ByVal sChan As String)
        Dim sInstrumentCmds As String
        Dim sReadBuffer As String
        Dim sTimebase As String
        Dim sAquisition As String
        Dim sTotGate As String
        Dim sTimIntDelay As String

        '   ********************
        '   Get Timebase
        '   ********************

        sInstrumentCmds = "SENS" & sChan & ":ROSC:SOUR?"
        SendScpiCommand(sInstrumentCmds)

        '##########################################
        'Action Required: Remove debug code
        'Fill return value selections
        'gcolCmds.Add "Simulated response for: " & sInstrumentCmds
        'gcolCmds.Add "CLK10"
        'gcolCmds.Add "INT"
        'gcolCmds.Add "EXT"
        '##########################################

        'Retrieve instrument output buffer
        sReadBuffer = ReadInstrumentBuffer()
        sTimebase = Trim(sReadBuffer)

        CtMain.ActivateControl(":SENS" & sChan & ":ROSC:SOUR " & sTimebase)

        If sTimebase = "INT" Then
            '********************
            'Get Output Timebase
            '********************

            sInstrumentCmds = "OUTP:ROSC:STAT?"
            SendScpiCommand(sInstrumentCmds)

            '##########################################
            'Action Required: Remove debug code
            'Fill return value selections
            'gcolCmds.Add "Simulated response for: " & sInstrumentCmds
            'gcolCmds.Add "0"
            'gcolCmds.Add "1"
            '##########################################

            'Retrieve instrument output buffer
            sReadBuffer = ReadInstrumentBuffer()

            If CInt(sReadBuffer) = 1 Then
                CtMain.ActivateControl(":OUTP:ROSC:STAT ON")
            Else
                CtMain.ActivateControl(":OUTP:ROSC:STAT OFF")
            End If
        End If

        '   ********************
        '   Get Aquisition Timeout
        '   ********************

        sInstrumentCmds = "SENS:ATIM:CHEC?"
        SendScpiCommand(sInstrumentCmds)

        '##########################################
        'Action Required: Remove debug code
        'Fill return value selections
        'gcolCmds.Add "Simulated response for: " & sInstrumentCmds
        'gcolCmds.Add "ON"
        'gcolCmds.Add "OFF"
        'gcolCmds.Add "STAR"
        '##########################################

        'Retrieve instrument output buffer
        sReadBuffer = ReadInstrumentBuffer()
        sAquisition = Trim(sReadBuffer)

        CtMain.ActivateControl(":SENS:ATIM:CHEC " & sAquisition)

        If sAquisition <> "OFF" Then
            '********************
            'Get Timout Duration
            '********************

            sInstrumentCmds = "SENS:ATIM:TIME?"
            SendScpiCommand(sInstrumentCmds)

            '##########################################
            'Action Required: Remove debug code
            'Fill return value selections
            'gcolCmds.Add "Simulated response for: " & sInstrumentCmds
            'gcolCmds.Add "0.1"
            'gcolCmds.Add "1.0"
            'gcolCmds.Add "5.0"
            'gcolCmds.Add "15.0"
            'gcolCmds.Add "100.0"
            'gcolCmds.Add "500.0"
            'gcolCmds.Add "900.0"
            'gcolCmds.Add "1500.0"
            '##########################################

            'Retrieve instrument output buffer
            sReadBuffer = ReadInstrumentBuffer()

            CtMain.ActivateControl(":SENS:ATIM:TIME " & sReadBuffer)
        End If

        If InstrumentMode = 6 Or InstrumentMode = 7 Or InstrumentMode = 8 Then
            '********************
            'Get 100 Gate Average Mode
            '********************

            sInstrumentCmds = "SENS" & sChan & ":AVER:STAT?"
            SendScpiCommand(sInstrumentCmds)

            '##########################################
            'Action Required: Remove debug code
            'Fill return value selections
            'gcolCmds.Add "Simulated response for: " & sInstrumentCmds
            'gcolCmds.Add "0"
            'gcolCmds.Add "1"
            '##########################################

            'Retrieve instrument output buffer
            sReadBuffer = ReadInstrumentBuffer()

            If CInt(sReadBuffer) = 1 Then
                CtMain.ActivateControl(":SENS" & sChan & ":AVER:STAT ON")
            Else
                CtMain.ActivateControl(":SENS" & sChan & ":AVER:STAT OFF")
            End If

        ElseIf InstrumentMode = 9 Then
            '*********************
            'Get Totalize Gate State
            '*********************

            sInstrumentCmds = "SENS" & sChan & ":TOT:GATE:STAT?"
            SendScpiCommand(sInstrumentCmds)

            '##########################################
            'Action Required: Remove debug code
            'Fill return value selections
            'gcolCmds.Add "Simulated response for: " & sInstrumentCmds
            'gcolCmds.Add "0"
            'gcolCmds.Add "1"
            '##########################################

            'Retrieve instrument output buffer
            sReadBuffer = ReadInstrumentBuffer()
            sTotGate = Trim(sReadBuffer)

            If CInt(sTotGate) = 1 Then
                CtMain.ActivateControl(":SENS" & sChan & ":TOT:GATE:STAT ON")
            Else
                CtMain.ActivateControl(":SENS" & sChan & ":TOT:GATE:STAT OFF")
            End If

            If sTotGate = "1" Then
                '*********************
                'Get Totalize Polarity
                '*********************

                sInstrumentCmds = "SENS" & sChan & ":TOT:GATE:POL?"
                SendScpiCommand(sInstrumentCmds)

                '##########################################
                'Action Required: Remove debug code
                'Fill return value selections
                'gcolCmds.Add "Simulated response for: " & sInstrumentCmds
                'gcolCmds.Add "NORM"
                'gcolCmds.Add "INV"
                '##########################################

                'Retrieve instrument output buffer
                sReadBuffer = ReadInstrumentBuffer()

                CtMain.ActivateControl(":SENS" & sChan & ":TOT:GATE:POL " & Trim(sReadBuffer))
            End If
        End If

        If InstrumentMode = 6 Then
            '*********************
            'Get Tine Interval Enable
            '*********************

            sInstrumentCmds = "SENS" & sChan & ":TINT:DEL:STAT?"
            SendScpiCommand(sInstrumentCmds)

            '##########################################
            'Action Required: Remove debug code
            'Fill return value selections
            'gcolCmds.Add "Simulated response for: " & sInstrumentCmds
            'gcolCmds.Add "0"
            'gcolCmds.Add "1"
            '##########################################

            'Retrieve instrument output buffer
            sReadBuffer = ReadInstrumentBuffer()
            sTimIntDelay = Trim(sReadBuffer)

            If CInt(sTimIntDelay) = 1 Then
                CtMain.ActivateControl(":SENS" & sChan & ":TINT:DEL:STAT ON")
            Else
                CtMain.ActivateControl(":SENS" & sChan & ":TINT:DEL:STAT OFF")
            End If

            If sTimIntDelay = "1" Then
                '*********************
                'Get Time Interval Delay
                '*********************

                sInstrumentCmds = "SENS" & sChan & ":TINT:DEL:TIME?"
                SendScpiCommand(sInstrumentCmds)

                '##########################################
                'Action Required: Remove debug code
                'Fill return value selections
                'gcolCmds.Add "Simulated response for: " & sInstrumentCmds
                'gcolCmds.Add "0"
                'gcolCmds.Add "3.100"
                'gcolCmds.Add "16.200"
                'gcolCmds.Add "30.300"
                'gcolCmds.Add "50"
                'gcolCmds.Add "68"
                'gcolCmds.Add "99.999"
                '##########################################

                'Retrieve instrument output buffer
                sReadBuffer = ReadInstrumentBuffer()

                CtMain.ActivateControl(":SENS" & sChan & ":TINT:DEL:TIME " & Trim(sReadBuffer))
            End If
        End If
    End Sub

    Private Sub cmdHelp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdHelp.Click
        frmHelp.ShowDialog()
    End Sub

    Private Sub cmdMeasure_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdMeasure.Click
        If (Panel_Conifg.DebugMode = True) Then 'Only perform switch operation if in Local Mode
            Exit Sub
        End If
        cmdMeasure_Click()
    End Sub

    Public Sub cmdMeasure_Click()
        Me.txtCtDisplay.Text = "Reading..."
        SendScpiCommand("*CLS") 'Clear any stale error states prior to measurement
        bChangeMode = False 'Reset Mode Changed Flag
        cmdMeasure.Enabled = False
        SendStatusBarMessage("Setting Counter Timer...")
        TakeMeasurement()
        SendStatusBarMessage("")
        cmdMeasure.Enabled = True

        If Not bTIP_Running Then EnableControls()
        If InstrumentMode = 9 Then
            If Me.cmdArmStop.Visible = False Then 'Code changed DJoiner 12/01/00
                Delay(0.05) 'Delay to keep from receiving errors
                SendScpiCommand("ABOR")
            End If
        End If
        If bolErrorReset = True Then 'If Reset is selected from MsgBox, Reset
            If LiveMode Then
                '' viClear (instrumentHandle&)
            End If
            Main()
        End If
    End Sub

    Private Sub cmdMeasure_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmdMeasure.MouseMove
        SendStatusBarMessage("Acquire measurement data.")

    End Sub

    '***** Totalize Measurement: Manual Gating  'Dave Joiner 11/29/00 *****
    Private Sub cmdArmStart_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdArmStart.Click
        'Totalize counts the number of events of the selected Channel signal
        'The measurement is imitated, the Gate is Opened and Closed under program control
        'The intermediate count can be queried by pressing "Measure" while the gate is open
        'The Final count can be queried by pressing "Measure" after the Gate is closed

        '****************************************************
        '*******************   OPEN GATE   ******************
        '****************************************************
        bolArm = True

        DisableControls() 'Disable User from changing Modes or parameters while Gate is "Open"

        cmdArmStop.Enabled = False 'Prevent User from "Closing" Gate before it is "Opened"
        cmdArmStart.Enabled = False 'Prevent User from "Opening" Gate when it is "Open"

        If LiveMode Then
            '  ' viClear (instrumentHandle&)   'Clear the output buffer
        End If
        ResetHolders() 'Reset Command Holders

        'Reset and initialize instrument
        SendScpiCommand("*RST ; *CLS") 'Select default configuration

        If Not bTIP_Running Then
            'Clear event registers, Error Queue
            SetINPutOptions() 'Set Input parameters
            SetSENSeOptions() 'Set parameters and Initiate Measurement
            SetCONFigureOptions() 'Configure a Totalize measurement
            SetARMOptions() 'Open and Close Gate manually
        Else
            'Program Instrument per saved TIP settings settings
            SendScpiCommand(sTIP_CMDstring)
            If Not bGetInstrumentStatus() Then ExitCounterTimer()
            sTIP_Mode = "TIP_HOLDOFF"
        End If

        SendStatusBarMessage("Acquiring Total Events......") 'Set Status Bar Message
        SendScpiCommand("ARM:STAR:IMM") 'Open the Gate to start counting
        cmdArmStop.Enabled = True 'Allow the User to "Close" Gate
    End Sub

    Private Sub cmdArmStop_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdArmStop.Click
        'Totalize counts the number of events of the selected Channel signal
        'The measurement is imitated, the Gate is Opened and Closed under program control
        'The intermediate count can be queried by pressing "Measure" while the gate is open
        'The Final count can be queried by pressing "Measure" after the Gate is closed

        '****************************************************
        '*******************   OPEN GATE   ******************
        '****************************************************
        '****************************************************
        '******************   CLOSE GATE   ******************
        '****************************************************
        bolArm = False

        cmdArmStart.Enabled = True 'Allow the User to "Open" Gate
        cmdArmStop.Enabled = False 'Prevent User from "Closing" Gate
        EnableControls() 'Allow User to change Modes or parameters after Gate is "Closed"
        SendScpiCommand("ARM:STOP:IMM") 'Close the Gate to Stop counting
        chkContinuous.Checked = False 'If making a continuous measurement, Stop Measurement
        Delay(1)
        If sTIP_Mode = "TIP_HOLDOFF" Then
            cmdMeasure_Click()
            ExitCounterTimer()
        End If
        SendStatusBarMessage("Click on START to initiate Events Count") 'Set Status Bar Message
    End Sub

    Private Sub cmdArmStart_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmdArmStart.MouseMove
        SendStatusBarMessage("Start Events Count Measurement.")
    End Sub

    Private Sub cmdArmStop_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmdArmStop.MouseMove
        If Not cmdArmStop.Visible Then Exit Sub
        SendStatusBarMessage("Stop Events Count Measurement.")
    End Sub

    Private Sub cmdQuit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdQuit.Click
        ' If the SFP is up in remote mode, should it reset on exit?
        'SendScpiCommand("*CLS")
        If LiveMode Then
            ErrorStatus = atxml_Close()
        End If
        Me.Close()
    End Sub

    Private Sub cmdQuit_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmdQuit.MouseMove
        SendStatusBarMessage("Quit instrument application.")
    End Sub

    Private Sub cmdReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdReset.Click
        If Me.chkContinuous.Checked = True Or cmdMeasure.Enabled = False Then 'Code changed DJoiner 11/16/00
            Me.chkContinuous.Checked = False
            bolReset = True
        Else
            ResetCounterTimer()
        End If
    End Sub

    Private Sub cmdReset_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmdReset.MouseMove
        SendStatusBarMessage("Reset instrument and settings.")
    End Sub

    Private Sub cmdSelfTest_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSelfTest.Click
        Dim TestResult As String

        'Reset Instrument and GUI
        ResetCounterTimer()
        'Allow for a full reset
        Delay(1)
        SendStatusBarMessage("Performing Built-In Test...")
        'Allow Message to be written to the toolbar
        'Disable user interaction
        Me.Enabled = False

        Me.chkContinuous.Checked = False
        If LiveMode Then
            SendScpiCommand("*OPC")
            SendScpiCommand("*TST?")
            'Set VISA TMO to 11 Seconds     DJoiner 05/14/02
            '     ErrorStatus& = viSetAttribute(instrumentHandle&, VI_ATTR_TMO_VALUE, 11000)
            TestResult = ReadInstrumentBuffer()
            If InStr(TestResult, "+0") > 0 Then
                MsgBox("Built-In Test Passed", MsgBoxStyle.Information)
            Else
                MsgBox("Built-In Test Failed", MsgBoxStyle.Critical)
            End If
            SendStatusBarMessage("Built-In Test completed.")
        Else
            MsgBox("Instrument Is Not Responding", MsgBoxStyle.Information)
        End If

        'Enable user interaction
        Me.Enabled = True
    End Sub

    Private Sub cmdSelfTest_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmdSelfTest.MouseMove
        SendStatusBarMessage("Perform instrument Built-In Test function.")
    End Sub

    Private Sub cmdUpdateTIP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdUpdateTIP.Click
        Me.Cursor = Cursors.WaitCursor
        SetKey("TIPS", "CMD", "")
        SetKey("TIPS", "STATUS", "")
        bBuildingTIPstr = True
        ResetHolders()
        sTIP_CMDstring = ":"

        Try
            'Program Instrument and build TIP CMD string
            If Channel = 1 Then
                TipBuildINPutString(1)
                TipBuildINPutString(2)
            Else
                TipBuildINPutString(2)
                TipBuildINPutString(1)
            End If
            If Me.optRoutingSeparate.Checked Then
                sTIP_CMDstring &= "INP1:ROUT SEP" 'Separate
            Else
                sTIP_CMDstring &= "INP1:ROUT COMM" 'Common
            End If
            SetSENSeOptions()

            If UCase(cboArmStartSource.Text) = "HOLD" Then
                SetCONFigureOptions()
                SetARMOptions()
            Else
                SetARMOptions()
                SetOUTPutOptions()
                SetCONFigureOptions()
            End If
            bBuildingTIPstr = False

            'Now send entire TIP command string and check for errors such as setting conflicts
            SendScpiCommand("*RST;*CLS")
            SendScpiCommand(sTIP_CMDstring)
            If Not bGetInstrumentStatus() Then Throw (New SystemException("Error"))

            'Don't exit if error, allow user opportunity to correct
            'If no errors, send TIP CMD string to VIPERT.INI file and close
            SetKey("TIPS", "CMD", sTIP_CMDstring)
            SetKey("TIPS", "STATUS", "Ready")
            ExitCounterTimer()

        Catch
            SendStatusBarMessage("Error occured. Click RESET and re-try.")
            sTIP_CMDstring = ""
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub cmdUpdateTIP_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmdUpdateTIP.MouseMove
        SendStatusBarMessage("Send instrument setup to TIP Studio.")
    End Sub

    Private Sub frmHPE1420B_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If sTIP_Mode = "TIP_DESIGN" Then
            Me.cmdUpdateTIP.Visible = True
        End If

        '   Set Common Controls parent properties
        Atlas_SFP.Parent_Object = Me
        Panel_Conifg.Parent_Object = Me

        Main()
    End Sub

    Private Sub Form_QueryUnload(ByRef Cancel As Short, ByVal UnloadMode As Short)
        ExitCounterTimer()
    End Sub

    Private Sub frmHPE1420B_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Dim Cancel As Short = 0
        Form_QueryUnload(Cancel, 0)
        If Cancel <> 0 Then
            e.Cancel = True
            Exit Sub
        End If
        ExitCounterTimer()
        If Cancel <> 0 Then e.Cancel = True
    End Sub

    Private Sub frmHPE1420B_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        If Me.WindowState = FormWindowState.Minimized Then Exit Sub

        panDmmDisplay.SetBounds(8, 0, Limit(Me.Width - 41, 0), 50, BoundsSpecified.Location Or BoundsSpecified.Size)
        tabUserOptions.SetBounds(8, 57, Limit(Me.Width - 37, 600), Limit(Me.Height - 162, 393), BoundsSpecified.Location Or BoundsSpecified.Size)
        cmdHelp.SetBounds(Limit(Me.Width - 303, 334), Limit(Me.Height - 94, 461), 0, 0, BoundsSpecified.Location)
        cmdReset.SetBounds(Limit(Me.Width - 221, 416), Limit(Me.Height - 94, 461), 0, 0, BoundsSpecified.Location)
        cmdQuit.SetBounds(Limit(Me.Width - 140, 497), Limit(Me.Height - 94, 461), 0, 0, BoundsSpecified.Location)

        If tabUserOptions.SelectedIndex = 0 Then
            'Do only if current tab is "Measurement" tab, otherwise these two
            'controls will appear on other tabs when screen is normalized.
            chkContinuous.SetBounds(Limit(tabUserOptions.Width - 132, 464), Limit(tabUserOptions.Height - 94, 299), 0, 0, BoundsSpecified.Location)
            cmdMeasure.SetBounds(Limit(tabUserOptions.Width - 132, 464), Limit(tabUserOptions.Height - 69, 324), 0, 0, BoundsSpecified.Location)
        End If
    End Sub

    Function Limit(ByVal CheckValue As Short, ByVal LimitValue As Short) As Short
        If CheckValue < LimitValue Then
            Limit = LimitValue
        Else
            Limit = CheckValue
        End If
    End Function

    Private Sub frmHPE1420B_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        ExitCounterTimer()
    End Sub

    Private Sub fraCh1AbsoluteTrigger_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles fraCh1AbsoluteTrigger.MouseMove
        SendStatusBarMessage("Set instrument triggering level.")
    End Sub

    'Code added DJoiner 11/27/00
    Private Sub fraAcquisitionTimeout_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles fraAcquisitionTimeout.MouseMove
        SendStatusBarMessage("Set instrument Acquisition Timeout Setting")
    End Sub

    Private Sub fraAperture_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles fraAperture.MouseMove
        SendStatusBarMessage("Set instrument measurement Aperture.")
    End Sub

    Private Sub fraAttenuation_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles fraCh1Attenuation.MouseMove, fraCh2Attenuation.MouseMove
        SendStatusBarMessage("Set instrument input channel attenuation factor.")
    End Sub

    Private Sub fraCh1Range_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles fraCh1Range.MouseMove
        SendStatusBarMessage("Set instrument measurement range.")
    End Sub

    Private Sub fraChannel_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles fraChannel.MouseMove
        SendStatusBarMessage("Set instrument measurement input channel.")
    End Sub

    Private Sub fraCoupling_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles fraCh1Coupling.MouseMove, fraCh2Coupling.MouseMove
        SendStatusBarMessage("Set instrument input coupling.")
    End Sub

    Private Sub fraFunction_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles fraFunction.MouseMove
        SendStatusBarMessage("Set instrument measurement function.")
    End Sub

    Private Sub fraHysteresis_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles fraCh1Hysteresis.MouseMove, fraCh2Hysteresis.MouseMove
        SendStatusBarMessage("Set instrument hysteresis level.")
    End Sub

    Private Sub fraImpedance_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles fraCh1Impedance.MouseMove, fraCh2Impedance.MouseMove
        SendStatusBarMessage("Set instrument input impedance.")
    End Sub

    Private Sub fraArmStartLevel_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles fraArmStartLevel.MouseMove
        SendStatusBarMessage("Set instrument arming start level.")
    End Sub

    Private Sub fraArmStopLevel_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles fraArmStopLevel.MouseMove
        SendStatusBarMessage("Set instrument arming stop level.")
    End Sub

    Private Sub fraCh1RelativeTrigger_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles fraCh1RelativeTrigger.MouseMove
        SendStatusBarMessage("Set instrument triggering level releative to input signal.")
    End Sub

    Private Sub fraCh1TriggerSlope_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles fraCh1TriggerSlope.MouseMove
        SendStatusBarMessage("Set instrument arming start slope level.")
    End Sub

    Private Sub fraCh2TriggerSlope_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles fraCh2TriggerSlope.MouseMove
        SendStatusBarMessage("Set instrument arming stop slope level.")
    End Sub

    Private Sub fraArmStartSource_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles fraArmStartSource.MouseMove
        SendStatusBarMessage("Set instrument arming start conditions.")
    End Sub

    Private Sub fraArmStopSource_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles fraArmStopSource.MouseMove
        SendStatusBarMessage("Set instrument arming stop conditions.")
    End Sub

    Private Sub fraTiDelay_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles fraTiDelay.MouseMove
        SendStatusBarMessage("Set instrument time interval delay time.")
    End Sub

    Private Sub fraTiGateAvg_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles fraTiGateAvg.MouseMove
        SendStatusBarMessage("Set instrument time interval averaging mode.")
    End Sub

    Private Sub fraTimebaseSource_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles fraTimebaseSource.MouseMove
        SendStatusBarMessage("Set instrument timebase reference source.")
    End Sub

    'Code added DJoiner 11/27/00
    Private Sub fraTimeOut_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles fraTimeOut.MouseMove
        SendStatusBarMessage("Set instrument Acquisition Timeout Duration.")
    End Sub

    Private Sub fraTotalizeGateState_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles fraTotalizeGateState.MouseMove
        SendStatusBarMessage("Set instrument totalize by gate option.")
    End Sub

    Private Sub fraCh1TriggeringMethod_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles fraCh1TriggeringMethod.MouseMove
        SendStatusBarMessage("Set instrument triggering method.")
    End Sub

    Private Sub fraTriggerOutput_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles fraTriggerOutput.MouseMove
        SendStatusBarMessage("Set instrument triggering outputs.")
    End Sub

    Private Sub fraTriggerSlope_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles fraCh1TriggerSlope.MouseMove, fraCh2TriggerSlope.MouseMove
        SendStatusBarMessage("Set instrument triggering slope level.")
    End Sub

    '#Const defHas_imgLogo = True
#If defHas_imgLogo Then
    Private Sub imgLogo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles imgLogo.Click
        If Me.WindowState = FormWindowState.Maximized Then Exit Sub
'The appropriate value for these in pixels instead of twips will have to be calculated once the need for this function is determined
'        Me.Width = 4575
'        Me.Height = 6420
    End Sub

    Private Sub imgLogo_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)
        'DebugDwh% = Not (DebugDwh%)
    End Sub
#End If
    Private Sub optAcquisitionTimeoutOff_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optAcquisitionTimeoutOff.CheckedChanged
        optAcquisition_CheckedChanged()
    End Sub

    Private Sub optAcquisitionTimeoutOn_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optAcquisitionTimeoutOn.CheckedChanged
        optAcquisition_CheckedChanged()
    End Sub

    Private Sub optAcquisitionTimeoutStart_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optAcquisitionTimeoutStart.CheckedChanged
        optAcquisition_CheckedChanged()
    End Sub

    Private Sub optAcquisition_CheckedChanged()
        If optAcquisitionTimeoutOff.Checked = True Then 'DJoiner 11/24/00
            fraTimeOut.Visible = False
        Else
            fraTimeOut.Visible = True
        End If

        SetSENSeOptions()
    End Sub

    Private Sub optAcquisitionTimeoutOn_Click(ByVal sender As Object, ByVal e As EventArgs) Handles optAcquisitionTimeoutOn.Click
        fraTimeOut.Visible = True
        SetSENSeOptions()
    End Sub

    Private Sub optAcquisitionTimeoutStart_Click(ByVal sender As Object, ByVal e As EventArgs) Handles optAcquisitionTimeoutStart.Click
        fraTimeOut.Visible = True
        SetSENSeOptions()
    End Sub

    Private Sub optArmStartLevelGND_Click(ByVal sender As Object, ByVal e As EventArgs) Handles optArmStartLevelGND.Click, optArmStartLevelECL.Click, optArmStartLevelTTL.Click
        SetARMOptions()
    End Sub

    Private Sub optArmStartSlopeNegative_Click(ByVal sender As Object, ByVal e As EventArgs) Handles optArmStartSlopeNegative.Click, optArmStartSlopePositive.Click
        SetARMOptions()
    End Sub

    Private Sub optArmStopLevelGND_Click(ByVal sender As Object, ByVal e As EventArgs) Handles optArmStopLevelGND.Click, optArmStopLevelECL.Click, optArmStopLevelTTL.Click
        SetARMOptions()
    End Sub

    Private Sub optArmStopSlopeNegative_Click(ByVal sender As Object, ByVal e As EventArgs) Handles optArmStopSlopeNegative.Click, optArmStopSlopePositive.Click
        SetARMOptions()
    End Sub

    Private Sub optCh1Attenuation10X_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optCh1Attenuation10X.CheckedChanged
        'Only act on control if enabled (when changing values with software in TIPs)
        If Not UpdatingFromFile And (optCh1Attenuation10X.Enabled = False Or optCh1Attenuation10X.Visible = False) Then Exit Sub

        If sTIP_Mode <> "TIP_DESIGN" Then SetINPutOptions()
    End Sub

    Private Sub optCh1Attenuation1X_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optCh1Attenuation1X.CheckedChanged
        'Only act on control if enabled (when changing values with software in TIPs)
        If Not UpdatingFromFile And (optCh1Attenuation1X.Enabled = False Or optCh1Attenuation1X.Visible = False) Then Exit Sub

        If sTIP_Mode <> "TIP_DESIGN" Then SetINPutOptions()
    End Sub

    Private Sub optCh2Attenuation10X_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optCh2Attenuation10X.CheckedChanged
        'Only act on control if enabled (when changing values with software in TIPs)
        If Not UpdatingFromFile And (optCh2Attenuation10X.Enabled = False Or optCh2Attenuation10X.Visible = False) Then Exit Sub

        If sTIP_Mode <> "TIP_DESIGN" Then SetINPutOptions()
    End Sub

    Private Sub optCh2Attenuation1X_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optCh2Attenuation1X.CheckedChanged
        'Only act on control if enabled (when changing values with software in TIPs)
        If Not UpdatingFromFile And (optCh2Attenuation1X.Enabled = False Or optCh2Attenuation1X.Visible = False) Then Exit Sub

        If sTIP_Mode <> "TIP_DESIGN" Then SetINPutOptions()
    End Sub

'#Const defUse_optClipboard_Click = True
#If defUse_optClipboard_Click Then
    Private Sub optClipboard_Click(ByVal Value As Short)
        '   cmdAtlas.Enabled = True
    End Sub
#End If

    Private Sub optCh1CouplingAC_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optCh1CouplingAC.CheckedChanged
        'Only act on control if enabled (when changing values with software in TIPs)
        If Not UpdatingFromFile And (optCh1CouplingAC.Enabled = False Or optCh1CouplingAC.Visible = False) Then Exit Sub

        If sTIP_Mode <> "TIP_DESIGN" Then SetINPutOptions()
    End Sub
    Private Sub optCh1CouplingDC_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optCh1CouplingDC.CheckedChanged
        'Only act on control if enabled (when changing values with software in TIPs)
        If Not UpdatingFromFile And (optCh1CouplingDC.Enabled = False Or optCh1CouplingDC.Visible = False) Then Exit Sub

        If sTIP_Mode <> "TIP_DESIGN" Then SetINPutOptions()
    End Sub

    Private Sub optCh2CouplingAC_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optCh2CouplingAC.CheckedChanged
        'Only act on control if enabled (when changing values with software in TIPs)
        If Not UpdatingFromFile And (optCh2CouplingAC.Enabled = False Or optCh2CouplingAC.Visible = False) Then Exit Sub

        If sTIP_Mode <> "TIP_DESIGN" Then SetINPutOptions()
    End Sub

    Private Sub optCh2CouplingDC_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optCh2CouplingDC.CheckedChanged
        'Only act on control if enabled (when changing values with software in TIPs)
        If Not UpdatingFromFile And (optCh2CouplingDC.Enabled = False Or optCh2CouplingDC.Visible = False) Then Exit Sub

        If sTIP_Mode <> "TIP_DESIGN" Then SetINPutOptions()
    End Sub

    Private Sub optTiGateAvgOff_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optTiGateAvgOff.CheckedChanged
        'Only act on control if enabled (when changing values with software in TIPs)
        If Not UpdatingFromFile And (optTiGateAvgOff.Enabled = False Or optTiGateAvgOff.Visible = False) Then Exit Sub

        SetSENSeOptions()
    End Sub

    Private Sub optTiGateAvgOn_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optTiGateAvgOn.CheckedChanged
        'Only act on control if enabled (when changing values with software in TIPs)
        If Not UpdatingFromFile And (optTiGateAvgOn.Enabled = False Or optTiGateAvgOn.Visible = False) Then Exit Sub

        SetSENSeOptions()
    End Sub

    Private Sub optCh1HysteresisDef_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optCh1HysteresisDef.Click, optCh1HysteresisMin.Click, optCh1HysteresisMax.CheckedChanged
        SetSENSeOptions()
    End Sub

    Private Sub optCh2HysteresisDef_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optCh2HysteresisDef.Click, optCh2HysteresisMin.Click, optCh2HysteresisMax.CheckedChanged
        SetSENSeOptions()
    End Sub

    Private Sub optCh1Impedance50Ohm_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optCh1Impedance50Ohm.CheckedChanged
        'Only act on control if enabled (when changing values with software in TIPs)
        If Not UpdatingFromFile And (optCh1Impedance50Ohm.Enabled = False Or optCh1Impedance50Ohm.Visible = False) Then Exit Sub

        If sTIP_Mode <> "TIP_DESIGN" Then SetINPutOptions()
    End Sub

    Private Sub optCh1Impedance1MOhm_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optCh1Impedance1MOhm.CheckedChanged
        'Only act on control if enabled (when changing values with software in TIPs)
        If Not UpdatingFromFile And (optCh1Impedance1MOhm.Enabled = False Or optCh1Impedance1MOhm.Visible) = False Then Exit Sub

        If sTIP_Mode <> "TIP_DESIGN" Then SetINPutOptions()
    End Sub

    Private Sub optCh2Impedance50Ohm_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optCh2Impedance50Ohm.CheckedChanged
        'Only act on control if enabled (when changing values with software in TIPs)
        If Not UpdatingFromFile And (optCh2Impedance50Ohm.Enabled = False Or optCh2Impedance50Ohm.Visible = False) Then Exit Sub
        If bTIP_Running Then Exit Sub

        If sTIP_Mode <> "TIP_DESIGN" Then SetINPutOptions()
    End Sub

    Private Sub optCh2Impedance1MOhm_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optCh2Impedance1MOhm.CheckedChanged
        'Only act on control if enabled (when changing values with software in TIPs)
        If Not UpdatingFromFile And (optCh2Impedance1MOhm.Enabled = False Or optCh2Impedance1MOhm.Visible = False) Then Exit Sub

        If sTIP_Mode <> "TIP_DESIGN" Then SetINPutOptions()
    End Sub

    Private Sub optInputChannel2_CheckedChanged(sender As Object, e As EventArgs) Handles optInputChannel2.CheckedChanged
        'Only act on control if enabled (when changing values with software in TIPs)
        If Not UpdatingFromFile And (optInputChannel2.Enabled = False Or optInputChannel2.Visible = False) Then Exit Sub

        'Set INPUT TAB controls to Default values
        Me.optCh1Attenuation1X.Checked = True
        Me.optCh1CouplingDC.Checked = True
        Me.optCh1Impedance1MOhm.Checked = True
        Me.optCh2Attenuation1X.Checked = True
        Me.optCh2CouplingDC.Checked = True
        Me.optCh2Impedance1MOhm.Checked = True
        Me.chkContinuous.Checked = False

        If optInputChannel2.Checked Then
            Channel = 2
            SetMax(RANGE) = "100000000" 'This sets channel 2 max frequencey at 100 MHz Jay Joiner 5/8/00.
            SetRangeMessage() 'Code added to have to correct values in the message box Jay Joiner 5/8/00.
            'Disable CH 1 / Enable CH 2 Controls on the Input Tab
            For iChIndex = 1 To 2
                Me.optCh2Attenuation1X.Enabled = True
                Me.optCh2CouplingDC.Enabled = True
                Me.optCh2Impedance1MOhm.Enabled = True
                Me.optCh2Attenuation10X.Enabled = True
                Me.optCh2CouplingAC.Enabled = True
                Me.optCh2Impedance50Ohm.Enabled = True
                If Not bDualChannel Then
                    Me.optCh1Attenuation1X.Enabled = False
                    Me.optCh1CouplingDC.Enabled = False
                    Me.optCh1Impedance1MOhm.Enabled = False
                    Me.optCh1Attenuation10X.Enabled = False
                    Me.optCh1CouplingAC.Enabled = False
                    Me.optCh1Impedance50Ohm.Enabled = False
                End If
                'Set Totalize Gate State ON if input channel is 2 (requires gate input on CH1)
                If Channel = 2 Then
                    Me.chkTotalizeGateState.Checked = True
                    Me.chkTotalizeGateState.Enabled = False
                End If
            Next iChIndex

            'Added D.Masters 03/26/2009
            'Update the frame text in the Event Trigger window to reflect the channel
            FrameChannel1TriggerSetup.Text = "Channel 2"

            txtRange_Leave(Me, New EventArgs())
        End If
    End Sub

    Private Sub optInputChannel1_CheckedChanged(sender As Object, e As EventArgs) Handles optInputChannel1.CheckedChanged
        'Only act on control if enabled (when changing values with software in TIPs)
        If Not UpdatingFromFile And (optInputChannel1.Enabled = False Or optInputChannel1.Visible = False) Then Exit Sub

        'Set INPUT TAB controls to Default values
        Me.optCh1Attenuation1X.Checked = True
        Me.optCh1CouplingDC.Checked = True
        Me.optCh1Impedance1MOhm.Checked = True
        Me.optCh2Attenuation1X.Checked = True
        Me.optCh2CouplingDC.Checked = True
        Me.optCh2Impedance1MOhm.Checked = True
        Me.chkContinuous.Checked = False

        If optInputChannel1.Checked Then
            Channel = 1
            SetMax(RANGE) = "200000000" 'This sets channel 1 max frequencey at 200 MHz Jay Joiner 5/8/00.
            SetRangeMessage() 'Code added to have to correct values in the message box Jay Joiner 5/8/00.
            'Disable CH 2 / Enable CH 1 Controls on the Input Tab
            For iChIndex = 1 To 2
                Me.optCh1Attenuation1X.Enabled = True
                Me.optCh1CouplingDC.Enabled = True
                Me.optCh1Impedance1MOhm.Enabled = True
                Me.optCh1Attenuation10X.Enabled = True
                Me.optCh1CouplingAC.Enabled = True
                Me.optCh1Impedance50Ohm.Enabled = True
                If Not bDualChannel Then
                    Me.optCh2Attenuation1X.Enabled = False
                    Me.optCh2CouplingDC.Enabled = False
                    Me.optCh2Impedance1MOhm.Enabled = False
                    Me.optCh2Attenuation10X.Enabled = False
                    Me.optCh2CouplingAC.Enabled = False
                    Me.optCh2Impedance50Ohm.Enabled = False
                End If
                Me.chkTotalizeGateState.Enabled = True
            Next iChIndex

            'Added D.Masters 03/26/2009
            'Update the frame text in the Event Trigger window to reflect the channel
            FrameChannel1TriggerSetup.Text = "Channel 1"

            txtRange_Leave(Me, New EventArgs())
        End If
    End Sub

    Private Sub optRoutingCommon_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optRoutingCommon.CheckedChanged
        'Only act on control if enabled (when changing values with software in TIPs)
        If Not UpdatingFromFile And (optRoutingCommon.Enabled = False Or optRoutingCommon.Visible = False Or tabUserOptions.SelectedIndex <> 0) Then Exit Sub

        DisableCh2()

        If sTIP_Mode <> "TIP_DESIGN" Then SetINPutOptions()
    End Sub

    Private Sub optRoutingSeparate_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optRoutingSeparate.CheckedChanged
        'Only act on control if enabled (when changing values with software in TIPs)
        If Not UpdatingFromFile And (optRoutingSeparate.Enabled = False Or optRoutingSeparate.Visible = False Or tabUserOptions.SelectedIndex <> 0) Then Exit Sub

        If optRoutingSeparate.Checked Then 'Separate
            EnableCh2() 'Enable CH2 controls
        Else
            DisableCh2()
        End If

        If sTIP_Mode <> "TIP_DESIGN" Then SetINPutOptions()
    End Sub

'#Const defUse_optTextFile_Click = True
#If defUse_optTextFile_Click Then
    Private Sub optTextFile_Click(ByVal Index As Short, ByVal Value As Short)
        ' cmdAtlas.Enabled = True
    End Sub
#End If

    Private Sub optTimebaseSourceExtern_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optTimebaseSourceExtern.CheckedChanged
        'Only act on control if enabled (when changing values with software in TIPs)
        If Not UpdatingFromFile And (optTimebaseSourceExtern.Enabled = False Or optTimebaseSourceExtern.Visible = False) Then Exit Sub

        chkOutputTimebase.Checked = False
        chkOutputTimebase.Visible = False
        SetSENSeOptions()
    End Sub

    Private Sub optTimebaseSourceIntern_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optTimebaseSourceIntern.CheckedChanged
        'Only act on control if enabled (when changing values with software in TIPs)
        If Not UpdatingFromFile And (optTimebaseSourceIntern.Enabled = False Or optTimebaseSourceIntern.Visible = False) Then Exit Sub

        If optTimebaseSourceIntern.Checked = True Then
            chkOutputTimebase.Visible = True
        Else
            chkOutputTimebase.Checked = False
            chkOutputTimebase.Visible = False
        End If
        SetSENSeOptions()
    End Sub

    Private Sub optTimebaseSourceVXIClk_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optTimebaseSourceVXIClk.CheckedChanged
        'Only act on control if enabled (when changing values with software in TIPs)
        If Not UpdatingFromFile And (optTimebaseSourceVXIClk.Enabled = False Or optTimebaseSourceVXIClk.Visible = False) Then Exit Sub

        chkOutputTimebase.Checked = False
        chkOutputTimebase.Visible = False
        SetSENSeOptions()
    End Sub

    Private Sub optTotalizeGatePolarityInvert_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optTotalizeGatePolarityInvert.CheckedChanged
        'Only act on control if enabled (when changing values with software in TIPs)
        If Not UpdatingFromFile And (optTotalizeGatePolarityInvert.Enabled = False Or optTotalizeGatePolarityInvert.Visible = False) Then Exit Sub

        SetSENSeOptions()
    End Sub

    Private Sub optTotalizeGatePolarityNormal_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optTotalizeGatePolarityNormal.CheckedChanged
        'Only act on control if enabled (when changing values with software in TIPs)
        If Not UpdatingFromFile And (optTotalizeGatePolarityNormal.Enabled = False Or optTotalizeGatePolarityNormal.Visible = False) Then Exit Sub

        SetSENSeOptions()
    End Sub

    Private Sub optTriggerOutputOn_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optTriggerOutputOn.CheckedChanged
        'Only act on control if enabled (when changing values with software in TIPs)
        If Not UpdatingFromFile And (optTriggerOutputOn.Enabled = False Or optTriggerOutputOn.Visible = False) Then Exit Sub

        optTriggerOutput_Click()
    End Sub

    Private Sub optTriggerOutputOff_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optTriggerOutputOff.CheckedChanged
        'Only act on control if enabled (when changing values with software in TIPs)
        If Not UpdatingFromFile And (optTriggerOutputOff.Enabled = False Or optTriggerOutputOff.Visible = False) Then Exit Sub

        optTriggerOutput_Click()
    End Sub

    Public Sub optTriggerOutput_Click()
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
        SetOUTPutOptions()
    End Sub

    Private Sub optCh1TriggerSlopeNegative_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optCh1TriggerSlopeNegative.CheckedChanged
        'Only act on control if enabled (when changing values with software in TIPs)
        If Not UpdatingFromFile And (optCh1TriggerSlopeNegative.Enabled = False Or optCh1TriggerSlopeNegative.Visible = False) Then Exit Sub

        SetSENSeOptions()
    End Sub

    Private Sub optCh1TriggerSlopePositive_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optCh1TriggerSlopePositive.CheckedChanged
        'Only act on control if enabled (when changing values with software in TIPs)
        If Not UpdatingFromFile And (optCh1TriggerSlopePositive.Enabled = False Or optCh1TriggerSlopePositive.Visible = False) Then Exit Sub

        SetSENSeOptions()
    End Sub

    Private Sub optCh2TriggerSlopeNegative_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optCh2TriggerSlopeNegative.CheckedChanged
        'Only act on control if enabled (when changing values with software in TIPs)
        If Not UpdatingFromFile And (optCh2TriggerSlopeNegative.Enabled = False Or optCh2TriggerSlopeNegative.Visible = False) Then Exit Sub

        SetSENSeOptions()
    End Sub

    Private Sub optCh2TriggerSlopePositive_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optCh2TriggerSlopePositive.CheckedChanged
        'Only act on control if enabled (when changing values with software in TIPs)
        If Not UpdatingFromFile And (optCh2TriggerSlopePositive.Enabled = False Or optCh2TriggerSlopePositive.Visible = False) Then Exit Sub

        SetSENSeOptions()
    End Sub

    Private Sub SpinButton_SpinDown(ByVal Index As Short)
        Dim OldVal As Double
        Dim Direction As String
        Dim NewVal As Double
        Static ProcessingSpinDown As Boolean = False

        If ProcessingSpinDown = False Then
            ProcessingSpinDown = True
            OldVal = Val(SetCur(Index))
            SendStatusBarMessage(SetRngMsg(Index))
            bSpin = True
            Direction = "Down"
            'Spin10Pct(TextBox(Index%), "Down", Index%)
            Dim Tmp As Double, Negative As Boolean
            Dim LastDirection As String
            Negative = False
            Direction = UCase(Direction)
            LastDirection = Direction
            If SetCur(Index) = "INF" And UCase(Direction) = "DOWN" Then
                Tmp = Val(SetMax(Index))
            Else
                Tmp = Val(SetCur(Index))
            End If
            If Tmp < 0 Or ((Tmp = 0) And (Val(SetMin(Index)) < 0) And (Direction = "DOWN")) Then 'If negative
                Tmp = Math.Abs(Tmp) 'Make it positive for now
                Negative = True 'Set flag
                If Direction = "UP" Then
                    Direction = "DOWN"
                Else
                    Direction = "UP"
                End If
            End If
            If Direction = "UP" Then
                If Tmp = 0 Then Tmp = 0.9 * Val(SetMinInc(Index))
                Tmp = RoundMSD(Direction, Tmp + (0.1 * Tmp))
                If Tmp > Val(SetMax(Index)) Then
                    Tmp = Val(SetMax(Index))
                    Beep()
                End If
            Else
                Tmp = RoundMSD(Direction, Tmp - (0.1 * Tmp))
                If (Tmp < Val(SetMin(Index))) Then
                    Tmp = Val(SetMin(Index))
                    Beep()
                End If
            End If
            If Negative Then Tmp = -Tmp
            If Tmp < Val(SetMin(Index)) Then
                Tmp = Val(SetMin(Index))
                Beep()
            End If

            Tmp = Microsoft.VisualBasic.Format(Tmp, SetRes(Index))
            If (Tmp = OldVal And Tmp <> SetMin(Index)) Then
                Tmp = 0.0
            End If
            NewVal = Tmp
            If NewVal >= Val(SetMin(Index)) Then
                SetCur(Index) = NewVal.ToString()
            Else
                SetCur(Index) = SetMin(Index)
            End If

            DispTxt(Index)

            If bolErrorReset = True Then 'If Reset is selected from MsgBox, Reset
                If LiveMode Then
                    '    ' viClear (instrumentHandle&)
                End If
                Main() 'Code changed DJoiner 12/04/00
            End If
            bSpin = False
            ProcessingSpinDown = False
        End If
    End Sub

    Private Sub SpinButton_SpinUp(ByVal Index As Short)
        Dim OldVal As Double
        Dim Direction As String
        Dim NewVal As Double
        Static ProcessingSpinUp As Boolean = False

        If ProcessingSpinUp = False Then
            ProcessingSpinUp = True
            OldVal = Val(SetCur(Index))
            SendStatusBarMessage(SetRngMsg(Index))
            bSpin = True
            Direction = "Up"
            'Spin10Pct TextBox(Index%), "Down", Index%
            Dim Tmp As Double, Negative As Short
            Dim LastDirection As String
            Negative = False
            Direction = UCase(Direction)
            LastDirection = Direction
            If SetCur(Index) = "INF" And UCase(Direction) = "DOWN" Then
                Tmp = Val(SetMax(Index))
            Else
                Tmp = Val(SetCur(Index))
            End If
            If Tmp < 0 Or ((Tmp = 0) And (Val(SetMin(Index)) < 0) And (Direction = "DOWN")) Then 'If negative
                Tmp = Math.Abs(Tmp) 'Make it positive for now
                Negative = True 'Set flag
                If Direction = "UP" Then
                    Direction = "DOWN"
                Else
                    Direction = "UP"
                End If
            End If
            If Direction = "UP" Then
                If Tmp = 0 Then Tmp = 0.9 * Val(SetMinInc(Index))
                Tmp = RoundMSD(Direction, Tmp + (0.1 * Tmp))
                If Tmp > Val(SetMax(Index)) Then
                    Tmp = Val(SetMax(Index))
                    Beep()
                End If
            Else
                Tmp = RoundMSD(Direction, Tmp - (0.1 * Tmp))
                If (Tmp < Val(SetMin(Index))) Then
                    Tmp = Val(SetMin(Index))
                    Beep()
                End If
            End If
            If Negative Then Tmp = -Tmp
            If Tmp < Val(SetMin(Index)) Then
                Tmp = Val(SetMin(Index))
                Beep()
            End If

            Tmp = Microsoft.VisualBasic.Format(Tmp, SetRes(Index))
            If (Tmp = OldVal And Tmp <> SetMax(Index)) Then
                Tmp = 0.0
            End If
            NewVal = Tmp
            If NewVal <= Val(SetMax(Index)) Then
                SetCur(Index) = Str(NewVal)
            Else
                SetCur(Index) = SetMax(Index)
            End If
            DispTxt(Index)

            tabUserOptions.Focus()
            If bolErrorReset = True Then 'If Reset is selected from MsgBox, Reset
                If LiveMode Then
                    ' viClear (instrumentHandle&)
                End If
                Main() 'Code changed DJoiner 12/04/00
            End If
            bSpin = False
            ProcessingSpinUp = False
        End If
    End Sub

    Private tabUserOptions_PreviousTab As Integer
    Private Sub tabUserOptions_Deselecting(ByVal sender As System.Object, ByVal e As TabControlCancelEventArgs) Handles tabUserOptions.Deselecting
        tabUserOptions_PreviousTab = e.TabPageIndex
    End Sub

    Private Sub tabUserOptions_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tabUserOptions.SelectedIndexChanged
        Dim PreviousTab As Integer = tabUserOptions_PreviousTab

        'If screen is maximized and Measurement tab is selected,
        'move cmdMeasure and chkContinuous to lower righthand part of screen
        'See also: Sub Form_Resize
        If Me.WindowState = FormWindowState.Maximized And tabUserOptions.SelectedIndex = 0 Then
            chkContinuous.SetBounds(Limit(tabUserOptions.Width - 1275, 2940), Limit(tabUserOptions.Height - 855, 3360), 0, 0, BoundsSpecified.Location)
            cmdMeasure.SetBounds(Limit(tabUserOptions.Width - 1275, 2940), Limit(tabUserOptions.Height - 495, 3720), 0, 0, BoundsSpecified.Location)
        End If
    End Sub

    Private Sub tbrCtFunctions_ButtonClick(ByVal sender As Object, ByVal e As ToolBarButtonClickEventArgs) Handles tbrCtFunctions.ButtonClick
        Me.chkContinuous.Checked = False 'Stop continuous measurement mode
        bChangeMode = True 'Set Mode Changed Flag

        'Reset and initialize instrument
        If LiveMode Then
            '  ' viClear (instrumentHandle&)     'Clear the output buffer
        End If
        SendScpiCommand("*RST ; *CLS") 'Select default configuration

        Select Case e.Button.Name
            Case "btnFuncFrequency"
                InstrumentMode = 1
            Case "btnFuncPeriod"
                InstrumentMode = 2
            Case "btnFuncFrequencyRatio"
                InstrumentMode = 3
            Case "btnFuncRiseTime"
                InstrumentMode = 4
            Case "btnFuncFallTime"
                InstrumentMode = 5
            Case "btnFuncTimeInterval"
                InstrumentMode = 6
            Case "btnFuncPosPulseWidth"
                InstrumentMode = 7
            Case "btnFuncNegPulseWidth"
                InstrumentMode = 8
            Case "btnFuncTotalize"
                InstrumentMode = 9
            Case "btnFuncPhase"
                InstrumentMode = 10
        End Select

        ResetHolders() 'DJoiner 11/24/00 Reset Command holders
        ChangeInstrumentMode()

        For Each Button In tbrCtFunctions.Buttons
            Button.pushed = False
        Next
        e.Button.Pushed = True

        tbrCtFunctions.Enabled = True
        CheckForManGate() 'If in "TZ" Mode, make "Hold" available
    End Sub

    Public Sub txtRange_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRange.Click
        Dim Index As Short = 1
        TextBox_Click(Index, txtRange)
    End Sub

    Public Sub txtAperture_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAperture.Click
        Dim Index As Short = 2
        TextBox_Click(Index, txtAperture)
    End Sub

     Public Sub txtCh1AbsoluteTrigger_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCh1AbsoluteTrigger.Click
       Dim Index As Short = 3
        TextBox_Click(Index, txtCh1AbsoluteTrigger)
    End Sub

    Public Sub txtCh1RelativeTrigger_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCh1RelativeTrigger.Click
        Dim Index As Short = 4
        TextBox_Click(Index, txtCh1RelativeTrigger)
    End Sub

    Public Sub txtTiDelay_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTiDelay.Click
        Dim Index As Short = 5
        TextBox_Click(Index, txtTiDelay)
    End Sub

    Public Sub txtTimeOut_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTimeOut.Click
        Dim Index As Short = 6
        TextBox_Click(Index, txtTimeOut)
    End Sub

    Public Sub txtCh2AbsoluteTrigger_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCh2AbsoluteTrigger.Click
        Dim Index As Short = 7
        TextBox_Click(Index, txtCh2AbsoluteTrigger)
    End Sub

    Public Sub txtCh2RelativeTrigger_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCh2RelativeTrigger.Click
        Dim Index As Short = 8
        TextBox_Click(Index, txtCh2RelativeTrigger)
    End Sub

    Public Sub TextBox_Click(ByVal Index As Integer, ByRef txtBox As TextBox)
        txtBox.SelectionStart = 0
        txtBox.SelectionLength = Len(txtBox.Text)
    End Sub

    Private Sub txtRange_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRange.Enter
        Dim Index As Short = 1

        If bSpin = True Then Exit Sub
        If InstrumentMode = 9 And Me.cboArmStartSource.Text = "Hold" Then
            Exit Sub 'Prevent wrong message in status bar  DJoiner 11/28/00
        End If
        RangeDisplay = True
        SendStatusBarMessage(SetRngMsg(Index))
    End Sub

    Private Sub txtAperture_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAperture.Enter
        Dim Index As Short = 2

        If bSpin = True Then Exit Sub
        If InstrumentMode = 9 And Me.cboArmStartSource.Text = "Hold" Then
            Exit Sub 'Prevent wrong message in status bar  DJoiner 11/28/00
        End If
        RangeDisplay = True
        SendStatusBarMessage(SetRngMsg(Index))
    End Sub

    Private Sub txtCh1AbsoluteTrigger_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCh1AbsoluteTrigger.Enter
        Dim Index As Short = 3

        If bSpin = True Then Exit Sub
        If InstrumentMode = 9 And Me.cboArmStartSource.Text = "Hold" Then
            Exit Sub 'Prevent wrong message in status bar  DJoiner 11/28/00
        End If
        RangeDisplay = True
        SendStatusBarMessage(SetRngMsg(Index))
    End Sub

    Private Sub txtCh1RelativeTrigger_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCh1RelativeTrigger.Enter
        Dim Index As Short = 4

        If bSpin = True Then Exit Sub
        If InstrumentMode = 9 And Me.cboArmStartSource.Text = "Hold" Then
            Exit Sub 'Prevent wrong message in status bar  DJoiner 11/28/00
        End If
        RangeDisplay = True
        SendStatusBarMessage(SetRngMsg(Index))
    End Sub

    Private Sub txtTiDelay_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTiDelay.Enter
        Dim Index As Short = 5

        If bSpin = True Then Exit Sub
        If InstrumentMode = 9 And Me.cboArmStartSource.Text = "Hold" Then
            Exit Sub 'Prevent wrong message in status bar  DJoiner 11/28/00
        End If
        RangeDisplay = True
        SendStatusBarMessage(SetRngMsg(Index))
    End Sub

    Private Sub txtTimeOut_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTimeOut.Enter
        Dim Index As Short = 6

        If bSpin = True Then Exit Sub
        If InstrumentMode = 9 And Me.cboArmStartSource.Text = "Hold" Then
            Exit Sub 'Prevent wrong message in status bar  DJoiner 11/28/00
        End If
        RangeDisplay = True
        SendStatusBarMessage(SetRngMsg(Index))
    End Sub

    Private Sub txtCh2AbsoluteTrigger_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCh2AbsoluteTrigger.Enter
        Dim Index As Short = 7

        If bSpin = True Then Exit Sub
        If InstrumentMode = 9 And Me.cboArmStartSource.Text = "Hold" Then
            Exit Sub 'Prevent wrong message in status bar  DJoiner 11/28/00
        End If
        RangeDisplay = True
        SendStatusBarMessage(SetRngMsg(Index))
    End Sub

    Private Sub txtCh2RelativeTrigger_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCh2RelativeTrigger.Enter
        Dim Index As Short = 8

        If bSpin = True Then Exit Sub
        If InstrumentMode = 9 And Me.cboArmStartSource.Text = "Hold" Then
            Exit Sub 'Prevent wrong message in status bar  DJoiner 11/28/00
        End If
        RangeDisplay = True
        SendStatusBarMessage(SetRngMsg(Index))
    End Sub

    Private Sub txtRange_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRange.KeyPress
        Dim Index As Short = 1
        Dim KeyAscii As Short = Asc(e.KeyChar)

        TextBox_KeyPress(Index, txtRange, KeyAscii)

        e.KeyChar = Chr(KeyAscii)
    End Sub

    Private Sub txtAperture_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAperture.KeyPress
        Dim Index As Short = 2
        Dim KeyAscii As Short = Asc(e.KeyChar)

        TextBox_KeyPress(Index, txtAperture, KeyAscii)

        e.KeyChar = Chr(KeyAscii)
    End Sub

    Private Sub txtCh1AbsoluteTrigger_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCh1AbsoluteTrigger.KeyPress
        Dim Index As Short = 3
        Dim KeyAscii As Short = Asc(e.KeyChar)

        TextBox_KeyPress(Index, txtCh1AbsoluteTrigger, KeyAscii)

        e.KeyChar = Chr(KeyAscii)
    End Sub

    Private Sub txtCh1RelativeTrigger_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCh1RelativeTrigger.KeyPress
        Dim Index As Short = 4
        Dim KeyAscii As Short = Asc(e.KeyChar)

        TextBox_KeyPress(Index, txtCh1RelativeTrigger, KeyAscii)

        e.KeyChar = Chr(KeyAscii)
    End Sub

    Private Sub txtTiDelay_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTiDelay.KeyPress
        Dim Index As Short = 5
        Dim KeyAscii As Short = Asc(e.KeyChar)

        TextBox_KeyPress(Index, txtTiDelay, KeyAscii)

        e.KeyChar = Chr(KeyAscii)
    End Sub

    Private Sub txtTimeOut_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTimeOut.KeyPress
        Dim Index As Short = 6
        Dim KeyAscii As Short = Asc(e.KeyChar)

        TextBox_KeyPress(Index, txtTimeOut, KeyAscii)

        e.KeyChar = Chr(KeyAscii)
    End Sub

    Private Sub txtCh2AbsoluteTrigger_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCh2AbsoluteTrigger.KeyPress
        Dim Index As Short = 7
        Dim KeyAscii As Short = Asc(e.KeyChar)

        TextBox_KeyPress(Index, txtCh2AbsoluteTrigger, KeyAscii)

        e.KeyChar = Chr(KeyAscii)
    End Sub

    Private Sub txtCh2RelativeTrigger_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCh2RelativeTrigger.KeyPress
        Dim Index As Short = 8
        Dim KeyAscii As Short = Asc(e.KeyChar)

        TextBox_KeyPress(Index, txtCh2RelativeTrigger, KeyAscii)

        e.KeyChar = Chr(KeyAscii)
    End Sub

    Public Sub TextBox_KeyPress(ByVal Index As Integer, ByRef txtBox As TextBox, ByRef KeyAscii As Short)
        If KeyAscii = 13 Or KeyAscii = 9 Then
            KeyAscii = 0
            If Me.Visible Then
                tabUserOptions.Focus()
            End If
        ElseIf KeyAscii = 27 Then
            KeyAscii = 0
            txtBox.Text = Str(Val(SetCur(Index)) / Val(SetUOM(Index)))
            If Me.Visible Then
                tabUserOptions.Focus()
            End If
        End If
    End Sub

    Public Sub txtRange_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRange.Leave
        Dim Index As Short = 1
        TextBox_LostFocus(Index, txtRange)
    End Sub

    Public Sub txtAperture_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAperture.Leave
        Dim Index As Short = 2
        TextBox_LostFocus(Index, txtAperture)
    End Sub

    Public Sub txtCh1AbsoluteTrigger_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCh1AbsoluteTrigger.Leave
        Dim Index As Short = 3
        TextBox_LostFocus(Index, txtCh1AbsoluteTrigger)
    End Sub

    Public Sub txtCh1RelativeTrigger_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCh1RelativeTrigger.Leave
        Dim Index As Short = 4
        TextBox_LostFocus(Index, txtCh1RelativeTrigger)
    End Sub

    Public Sub txtTiDelay_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTiDelay.Leave
        Dim Index As Short = 5
        TextBox_LostFocus(Index, txtTiDelay)
    End Sub

    Public Sub txtTimeOut_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTimeOut.Leave
        Dim Index As Short = 6
        TextBox_LostFocus(Index, txtTimeOut)
    End Sub

    Public Sub txtCh2AbsoluteTrigger_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCh2AbsoluteTrigger.Leave
        Dim Index As Short = 7
        TextBox_LostFocus(Index, txtCh2AbsoluteTrigger)
    End Sub

    Public Sub txtCh2RelativeTrigger_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCh2RelativeTrigger.Leave
        Dim Index As Short = 8
        TextBox_LostFocus(Index, txtCh2RelativeTrigger)
    End Sub

    Public Sub TextBox_LostFocus(ByVal Index As Integer, ByRef txtBox As TextBox)
        Dim First3 As String
        Dim NewVal As Double
        Dim X As Integer
        Dim A As String
        Dim NumLen As Short
        Dim Unit As String
        Dim Number As Double
        Dim Res As Short

        Number = Val(SetCur(Index))
        Select Case Index
            Case RANGE
                Unit = ModeUnit
                Res = 3
            Case APERTURE
                Unit = "Sec"
                Res = 3
            Case ABS_TRIGGER
                Unit = "Volts"
                Res = 4
            Case REL_TRIGGER
                Unit = "%"
                Res = 0
            Case TI_DELAY
                Unit = "Sec"
                Res = 3
            Case DUR_TIMEOUT
                Unit = "Sec"
                Res = 1
        End Select

        'If EngNotate(Number#, Res%, Unit$) <> TextBox(Index).Text And
        'Trim(SetCur$(Index)) = TextBox(Index).Text Then
        'since value is same exit sub
        'TextBox_LostFocus (Index)
        '    Exit Sub
        ' End If

        If sTIP_Mode = "TIP_HOLDOFF" Then Exit Sub

        First3 = UCase(Strings.Left(txtBox.Text, 3))
        Select Case First3
            Case "MIN", "N"
                NewVal = Val(SetMin(Index))
            Case "MAX", "X"
                NewVal = Val(SetMax(Index))
            Case "DEF", "D"
                NewVal = Val(SetDef(Index))

            Case Else
                If Not IsNumeric(SetCur(Index)) Then
                    MsgBox(SetRngMsg(Index), MsgBoxStyle.Exclamation, "Invalid Value")
                    If Me.Visible Then
                        txtBox.Focus()
                    End If
                    TextBox_Click(Index, txtBox)
                    Exit Sub
                End If

                For X = 1 To Len(txtBox.Text)
                    A = (Mid(txtBox.Text, X, 1))
                    If IsNumeric(A) Or A = "+" Or A = "-" Or A = "." Or A = "e" Or A = "E" Or A = " " Then
                        NumLen += 1
                    Else
                        Exit For
                    End If
                Next X
                NewVal = Val(Mid(txtBox.Text, 1, NumLen)) * Val(SetUOM(Index))
                If Channel = 2 Then 'Code changed DJoiner 11/21/00
                    If NewVal > SetMax(RANGE) / 1000000 Then
                        NewVal = SetMax(RANGE) / 1000000
                    End If
                End If
                If NumLen <> 0 Then
                    Select Case Mid(txtBox.Text, NumLen + 1, 1)
                        Case "T"
                            NewVal *= 1000000000000.0
                        Case "G"
                            NewVal *= 1000000000.0
                        Case "M"
                            NewVal *= 1000000.0
                        Case "K"
                            NewVal *= 1000.0
                        Case "m"
                            NewVal *= 0.001
                        Case "u"
                            NewVal *= 0.000001
                        Case "n"
                            NewVal *= 0.000000001
                        Case "p"
                            NewVal *= 0.000000000001
                    End Select
                End If
        End Select

        If NewVal < Val(SetMin(Index)) Then
            MsgBox(SetRngMsg(Index), MsgBoxStyle.Exclamation, "Invalid Value")
            If txtBox.Visible Then
                txtBox.Focus()
                SetCur(Index) = Val(SetMin(Index)) 'DJoiner 11/24/00
                DispTxt(Index)
            End If
            TextBox_Click(Index, txtBox)
        ElseIf NewVal > Val(SetMax(Index)) Then
            MsgBox(SetRngMsg(Index), MsgBoxStyle.Exclamation, "Invalid Value")
            If txtBox.Visible Then
                txtBox.Focus()
                SetCur(Index) = Val(SetMax(Index)) 'DJoiner 11/24/00
                DispTxt(Index)
            End If
            TextBox_Click(Index, txtBox)
        Else
            SetCur(Index) = Str(NewVal)
            DispTxt(Index)
            RangeDisplay = False
        End If

        SendStatusBarMessage("")
    End Sub

    Private Sub spnRange_DownButtonClicked(sender As Object, e As EventArgs) Handles spnRange.DownButtonClicked
        SpinButton_SpinDown(RANGE)
    End Sub

    Private Sub spnRange_UpButtonClicked(sender As Object, e As EventArgs) Handles spnRange.UpButtonClicked
        SpinButton_SpinUp(RANGE)
    End Sub

    Private Sub spnAperture_DownButtonClicked(sender As Object, e As EventArgs) Handles spnAperture.DownButtonClicked
        SpinButton_SpinDown(APERTURE)
    End Sub

    Private Sub spnAperture_UpButtonClicked(sender As Object, e As EventArgs) Handles spnAperture.UpButtonClicked
        SpinButton_SpinUp(APERTURE)
    End Sub

    Private Sub spnCh1AbsoluteTrigger_DownButtonClicked(sender As Object, e As EventArgs) Handles spnCh1AbsoluteTrigger.DownButtonClicked
        SpinButton_SpinDown(ABS_TRIGGER)
    End Sub

    Private Sub spnCh1AbsoluteTrigger_UpButtonClicked(sender As Object, e As EventArgs) Handles spnCh1AbsoluteTrigger.UpButtonClicked
        SpinButton_SpinUp(ABS_TRIGGER)
    End Sub

    Private Sub spnCh1RelativeTrigger_DownButtonClicked(sender As Object, e As EventArgs) Handles spnCh1RelativeTrigger.DownButtonClicked
        SpinButton_SpinDown(REL_TRIGGER)
    End Sub

    Private Sub spnCh1RelativeTrigger_UpButtonClicked(sender As Object, e As EventArgs) Handles spnCh1RelativeTrigger.UpButtonClicked
        SpinButton_SpinUp(REL_TRIGGER)
    End Sub

    Private Sub spnTiDelay_DownButtonClicked(sender As Object, e As EventArgs) Handles spnTiDelay.DownButtonClicked
        SpinButton_SpinDown(TI_DELAY)
    End Sub

    Private Sub spnTiDelay_UpButtonClicked(sender As Object, e As EventArgs) Handles spnTiDelay.UpButtonClicked
        SpinButton_SpinUp(TI_DELAY)
    End Sub

    Private Sub spnTimeOut_DownButtonClicked(sender As Object, e As EventArgs) Handles spnTimeOut.DownButtonClicked
        SpinButton_SpinDown(DUR_TIMEOUT)
    End Sub

    Private Sub spnTimeOut_UpButtonClicked(sender As Object, e As EventArgs) Handles spnTimeOut.UpButtonClicked
        SpinButton_SpinUp(DUR_TIMEOUT)
    End Sub

    Private Sub spnCh2AbsoluteTrigger_DownButtonClicked(sender As Object, e As EventArgs) Handles spnCh2AbsoluteTrigger.DownButtonClicked
        SpinButton_SpinDown(ABS_TRIG_CHAN2)
    End Sub

    Private Sub spnCh2AbsoluteTrigger_UpButtonClicked(sender As Object, e As EventArgs) Handles spnCh2AbsoluteTrigger.UpButtonClicked
        SpinButton_SpinUp(ABS_TRIG_CHAN2)
    End Sub

    Private Sub spnCh2RelativeTrigger_DownButtonClicked(sender As Object, e As EventArgs) Handles spnCh2RelativeTrigger.DownButtonClicked
        SpinButton_SpinDown(REL_TRIG_CHAN2)
    End Sub

    Private Sub spnCh2RelativeTrigger_UpButtonClicked(sender As Object, e As EventArgs) Handles spnCh2RelativeTrigger.UpButtonClicked
        SpinButton_SpinUp(REL_TRIG_CHAN2)
    End Sub
End Class