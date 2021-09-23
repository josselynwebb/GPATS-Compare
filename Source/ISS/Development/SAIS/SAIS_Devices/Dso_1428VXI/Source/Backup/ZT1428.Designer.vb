<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmZT1428
#Region "Windows Form Designer generated code "
	<System.Diagnostics.DebuggerNonUserCode()> Public Sub New()
		MyBase.New()
		'This call is required by the Windows Form Designer.
		InitializeComponent()
	End Sub
	'Form overrides dispose to clean up the component list.
	<System.Diagnostics.DebuggerNonUserCode()> Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
		If Disposing Then
			Static fTerminateCalled As Boolean
			If Not fTerminateCalled Then
				Form_Terminate_renamed()
				fTerminateCalled = True
			End If
			If Not components Is Nothing Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(Disposing)
	End Sub
	'Required by the Windows Form Designer
	Private components As System.ComponentModel.IContainer
	Public ToolTip1 As System.Windows.Forms.ToolTip
	Public dlgSegmentDataOpen As System.Windows.Forms.OpenFileDialog
	Public dlgSegmentDataSave As System.Windows.Forms.SaveFileDialog
	Public dlgSegmentDataFont As System.Windows.Forms.FontDialog
	Public dlgSegmentDataColor As System.Windows.Forms.ColorDialog
	Public dlgSegmentDataPrint As System.Windows.Forms.PrintDialog
	Public WithEvents cmdDisplaySize As System.Windows.Forms.Button
	Public WithEvents txtHoldOff As System.Windows.Forms.TextBox
	Public WithEvents spnHoldOff As AxSpin.AxSpinButton
	Public WithEvents SSPanel7 As AxThreed.AxSSPanel
	Public WithEvents _optHoldOff_1 As AxThreed.AxSSOption
	Public WithEvents _optHoldOff_0 As AxThreed.AxSSOption
	Public WithEvents lblEvents As System.Windows.Forms.Label
	Public WithEvents fraHoldOff As AxThreed.AxSSFrame
	Public WithEvents txtDelay As System.Windows.Forms.TextBox
	Public WithEvents spnDelay As AxSpin.AxSpinButton
	Public WithEvents SSPanel6 As AxThreed.AxSSPanel
	Public WithEvents fraDealy As AxThreed.AxSSFrame
	Public WithEvents cboHorizRange As System.Windows.Forms.ComboBox
	Public WithEvents fraHorizRange As AxThreed.AxSSFrame
	Public WithEvents txtCompletion As System.Windows.Forms.TextBox
	Public WithEvents spnCompletion As AxSpin.AxSpinButton
	Public WithEvents SSPanel9 As AxThreed.AxSSPanel
	Public WithEvents fraCompletion As AxThreed.AxSSFrame
	Public WithEvents _optDataPoints_0 As AxThreed.AxSSOption
	Public WithEvents _optDataPoints_1 As AxThreed.AxSSOption
	Public WithEvents fraDataPoints As AxThreed.AxSSFrame
	Public WithEvents _optReference_2 As AxThreed.AxSSOption
	Public WithEvents _optReference_0 As AxThreed.AxSSOption
	Public WithEvents _optReference_1 As AxThreed.AxSSOption
	Public WithEvents fraReference As AxThreed.AxSSFrame
	Public WithEvents txtAvgCount As System.Windows.Forms.TextBox
	Public WithEvents spnAvgCount As AxSpin.AxSpinButton
	Public WithEvents SSPanel8 As AxThreed.AxSSPanel
	Public WithEvents fraAvgCount As AxThreed.AxSSFrame
	Public WithEvents cboSampleType As System.Windows.Forms.ComboBox
	Public WithEvents fraSampleType As AxThreed.AxSSFrame
	Public WithEvents cboAcqMode As System.Windows.Forms.ComboBox
	Public WithEvents fraAcqMode As AxThreed.AxSSFrame
	Public WithEvents _chkDisplayTrace_0 As AxThreed.AxSSCheck
	Public WithEvents _chkDisplayTrace_1 As AxThreed.AxSSCheck
	Public WithEvents _chkDisplayTrace_2 As AxThreed.AxSSCheck
	Public WithEvents _chkDisplayTrace_3 As AxThreed.AxSSCheck
	Public WithEvents fraDisplay As AxThreed.AxSSFrame
	Public WithEvents _tabMainFunctions_TabPage0 As System.Windows.Forms.TabPage
	Public WithEvents _cmdSaveToDisk_0 As System.Windows.Forms.Button
	Public WithEvents _cmdSaveToMem_0 As System.Windows.Forms.Button
	Public WithEvents chkLFReject1 As AxThreed.AxSSCheck
	Public WithEvents chkHFReject1 As AxThreed.AxSSCheck
	Public WithEvents _fraSignalFilters_0 As AxThreed.AxSSFrame
	Public WithEvents _optProbe1_3 As AxThreed.AxSSOption
	Public WithEvents _optProbe1_2 As AxThreed.AxSSOption
	Public WithEvents _optProbe1_1 As AxThreed.AxSSOption
	Public WithEvents _optProbe1_0 As AxThreed.AxSSOption
	Public WithEvents _fraProbeAttenuation_0 As AxThreed.AxSSFrame
	Public WithEvents _cboVertRange_0 As System.Windows.Forms.ComboBox
	Public WithEvents _fraVerticalRange_0 As AxThreed.AxSSFrame
	Public WithEvents _optCoupling1_1 As AxThreed.AxSSOption
	Public WithEvents _optCoupling1_0 As AxThreed.AxSSOption
	Public WithEvents _optCoupling1_2 As AxThreed.AxSSOption
	Public WithEvents _fraCoupling_0 As AxThreed.AxSSFrame
	Public WithEvents txtVOffset1 As System.Windows.Forms.TextBox
	Public WithEvents spnVOffset1 As AxSpin.AxSpinButton
	Public WithEvents SSPanel10 As AxThreed.AxSSPanel
	Public WithEvents fraOffset1 As AxThreed.AxSSFrame
	Public WithEvents _tabMainFunctions_TabPage1 As System.Windows.Forms.TabPage
	Public WithEvents txtVOffset2 As System.Windows.Forms.TextBox
	Public WithEvents spnVOffset2 As AxSpin.AxSpinButton
	Public WithEvents SSPanel11 As AxThreed.AxSSPanel
	Public WithEvents fraOffset2 As AxThreed.AxSSFrame
	Public WithEvents _optCoupling2_2 As AxThreed.AxSSOption
	Public WithEvents _optCoupling2_0 As AxThreed.AxSSOption
	Public WithEvents _optCoupling2_1 As AxThreed.AxSSOption
	Public WithEvents _fraCoupling_1 As AxThreed.AxSSFrame
	Public WithEvents _cboVertRange_1 As System.Windows.Forms.ComboBox
	Public WithEvents _fraVerticalRange_1 As AxThreed.AxSSFrame
	Public WithEvents _optProbe2_0 As AxThreed.AxSSOption
	Public WithEvents _optProbe2_1 As AxThreed.AxSSOption
	Public WithEvents _optProbe2_2 As AxThreed.AxSSOption
	Public WithEvents _optProbe2_3 As AxThreed.AxSSOption
	Public WithEvents _fraProbeAttenuation_1 As AxThreed.AxSSFrame
	Public WithEvents chkHFReject2 As AxThreed.AxSSCheck
	Public WithEvents chkLFReject2 As AxThreed.AxSSCheck
	Public WithEvents _fraSignalFilters_1 As AxThreed.AxSSFrame
	Public WithEvents _cmdSaveToMem_1 As System.Windows.Forms.Button
	Public WithEvents _cmdSaveToDisk_1 As System.Windows.Forms.Button
	Public WithEvents _tabMainFunctions_TabPage2 As System.Windows.Forms.TabPage
	Public WithEvents tbrMeasFunction As AxComctlLib.AxToolbar
	Public WithEvents cboMeasSour As System.Windows.Forms.ComboBox
	Public WithEvents cboStopSource As System.Windows.Forms.ComboBox
	Public WithEvents cboStartSource As System.Windows.Forms.ComboBox
	Public WithEvents _optStartSlope_0 As AxThreed.AxSSOption
	Public WithEvents _optStartSlope_1 As AxThreed.AxSSOption
	Public WithEvents lblStartSlope As System.Windows.Forms.Label
	Public WithEvents Picture2 As System.Windows.Forms.Panel
	Public WithEvents _optStopSlope_0 As AxThreed.AxSSOption
	Public WithEvents _optStopSlope_1 As AxThreed.AxSSOption
	Public WithEvents lblStopSlope As System.Windows.Forms.Label
	Public WithEvents Picture3 As System.Windows.Forms.Panel
	Public WithEvents _optStartLevel_0 As AxThreed.AxSSOption
	Public WithEvents _optStartLevel_1 As AxThreed.AxSSOption
	Public WithEvents lblStartLevelCap As System.Windows.Forms.Label
	Public WithEvents Picture4 As System.Windows.Forms.Panel
	Public WithEvents txtStartEdge As System.Windows.Forms.TextBox
	Public WithEvents spnStartEdge As AxSpin.AxSpinButton
	Public WithEvents SSPanel1 As AxThreed.AxSSPanel
	Public WithEvents txtStartLevel As System.Windows.Forms.TextBox
	Public WithEvents spnStartLevel As AxSpin.AxSpinButton
	Public WithEvents SSPanel3 As AxThreed.AxSSPanel
	Public WithEvents txtStopEdge As System.Windows.Forms.TextBox
	Public WithEvents spnStopEdge As AxSpin.AxSpinButton
	Public WithEvents SSPanel4 As AxThreed.AxSSPanel
	Public WithEvents txtStopLevel As System.Windows.Forms.TextBox
	Public WithEvents spnStopLevel As AxSpin.AxSpinButton
	Public WithEvents SSPanel5 As AxThreed.AxSSPanel
	Public WithEvents Label1 As System.Windows.Forms.Label
	Public WithEvents lblStopEdgeCap As System.Windows.Forms.Label
	Public WithEvents lblStopSource As System.Windows.Forms.Label
	Public WithEvents lblStartEdgeCap As System.Windows.Forms.Label
	Public WithEvents Line1 As System.Windows.Forms.Label
	Public WithEvents lblStop As System.Windows.Forms.Label
	Public WithEvents lblStar As System.Windows.Forms.Label
	Public WithEvents lblStartSource As System.Windows.Forms.Label
	Public WithEvents fraDelayParam As AxThreed.AxSSPanel
	Public WithEvents _fraFunction_2 As AxThreed.AxSSFrame
	Public WithEvents cmdHelp As System.Windows.Forms.Button
	Public WithEvents _tabMainFunctions_TabPage3 As System.Windows.Forms.TabPage
	Public WithEvents txtLine As System.Windows.Forms.TextBox
	Public WithEvents spnLine As AxSpin.AxSpinButton
	Public WithEvents SSPanel14 As AxThreed.AxSSPanel
	Public WithEvents fraLine As AxThreed.AxSSFrame
	Public WithEvents _optField_0 As AxThreed.AxSSOption
	Public WithEvents _optField_1 As AxThreed.AxSSOption
	Public WithEvents fraField As AxThreed.AxSSFrame
	Public WithEvents _optStandard_0 As AxThreed.AxSSOption
	Public WithEvents _optStandard_1 As AxThreed.AxSSOption
	Public WithEvents fraStandard As AxThreed.AxSSFrame
	Public WithEvents txtOccurrence As System.Windows.Forms.TextBox
	Public WithEvents spnOccurrence As AxSpin.AxSpinButton
	Public WithEvents SSPanel15 As AxThreed.AxSSPanel
	Public WithEvents fraOccurrence As AxThreed.AxSSFrame
	Public WithEvents _optDelaySlope_0 As AxThreed.AxSSOption
	Public WithEvents _optDelaySlope_1 As AxThreed.AxSSOption
	Public WithEvents fraDelaySlope As AxThreed.AxSSFrame
	Public WithEvents cboTrigDelaySour As System.Windows.Forms.ComboBox
	Public WithEvents fraTrigDelaySource As AxThreed.AxSSFrame
	Public WithEvents txtDelEvents As System.Windows.Forms.TextBox
	Public WithEvents spnDelEvents As AxSpin.AxSpinButton
	Public WithEvents SSPanel13 As AxThreed.AxSSPanel
	Public WithEvents _optTrigDelay_1 As AxThreed.AxSSOption
	Public WithEvents _optTrigDelay_0 As AxThreed.AxSSOption
	Public WithEvents lblTrigDelayEvents As System.Windows.Forms.Label
	Public WithEvents fraTrigDelay As AxThreed.AxSSFrame
	Public WithEvents txtTrigLevel As System.Windows.Forms.TextBox
	Public WithEvents spnTrigLevel As AxSpin.AxSpinButton
	Public WithEvents SSPanel12 As AxThreed.AxSSPanel
	Public WithEvents fraTrigLevel As AxThreed.AxSSFrame
	Public WithEvents _optNoiseRej_1 As AxThreed.AxSSOption
	Public WithEvents _optNoiseRej_0 As AxThreed.AxSSOption
	Public WithEvents fraNoiseRej As AxThreed.AxSSFrame
	Public WithEvents _optTrigSlope_1 As AxThreed.AxSSOption
	Public WithEvents _optTrigSlope_0 As AxThreed.AxSSOption
	Public WithEvents fraTrigSlope As AxThreed.AxSSFrame
	Public WithEvents cboTrigSour As System.Windows.Forms.ComboBox
	Public WithEvents fraTrigSour As AxThreed.AxSSFrame
	Public WithEvents _optTrigMode_1 As AxThreed.AxSSOption
	Public WithEvents _optTrigMode_0 As AxThreed.AxSSOption
	Public WithEvents _optTrigMode_2 As AxThreed.AxSSOption
	Public WithEvents fraTrigMode As AxThreed.AxSSFrame
	Public WithEvents _tabMainFunctions_TabPage4 As System.Windows.Forms.TabPage
	Public WithEvents cmdLoadFromDisk As System.Windows.Forms.Button
	Public WithEvents _cmdSaveToDisk_3 As System.Windows.Forms.Button
	Public WithEvents txtMemoryRange As System.Windows.Forms.TextBox
	Public WithEvents SpinButton2 As AxSpin.AxSpinButton
	Public WithEvents SSPanel19 As AxThreed.AxSSPanel
	Public WithEvents fraMemVerticalRange As AxThreed.AxSSFrame
	Public WithEvents txtMemVoltOffset As System.Windows.Forms.TextBox
	Public WithEvents spnMemVOffset As AxSpin.AxSpinButton
	Public WithEvents SSPanel17 As AxThreed.AxSSPanel
	Public WithEvents SSFrame2 As AxThreed.AxSSFrame
	Public WithEvents Line5 As System.Windows.Forms.Label
	Public WithEvents Line4 As System.Windows.Forms.Label
	Public WithEvents Label3 As System.Windows.Forms.Label
	Public WithEvents picMemOptions As System.Windows.Forms.Panel
	Public WithEvents _cmdSaveToMem_2 As System.Windows.Forms.Button
	Public WithEvents _cmdSaveToDisk_2 As System.Windows.Forms.Button
	Public WithEvents cboMathSourceA As System.Windows.Forms.ComboBox
	Public WithEvents fraRange As AxThreed.AxSSFrame
	Public WithEvents cboMathSourceB As System.Windows.Forms.ComboBox
	Public WithEvents fraMathSourceB As AxThreed.AxSSFrame
	Public WithEvents cboMathFunction As System.Windows.Forms.ComboBox
	Public WithEvents fraOperation As AxThreed.AxSSFrame
	Public WithEvents txtMathRange As System.Windows.Forms.TextBox
	Public WithEvents SpinButton1 As AxSpin.AxSpinButton
	Public WithEvents SSPanel18 As AxThreed.AxSSPanel
	Public WithEvents fraMemVRange As AxThreed.AxSSFrame
	Public WithEvents txtMathVOffset As System.Windows.Forms.TextBox
	Public WithEvents spnMathVOffset As AxSpin.AxSpinButton
	Public WithEvents SSPanel16 As AxThreed.AxSSPanel
	Public WithEvents SSFrame1 As AxThreed.AxSSFrame
	Public WithEvents Line3 As System.Windows.Forms.Label
	Public WithEvents Line2 As System.Windows.Forms.Label
	Public WithEvents Label2 As System.Windows.Forms.Label
	Public WithEvents picMathOptions As System.Windows.Forms.Panel
	Public WithEvents _tabMainFunctions_TabPage5 As System.Windows.Forms.TabPage
	Public WithEvents imgProbeComp As System.Windows.Forms.PictureBox
	Public WithEvents panWaveDisplay As AxThreed.AxSSPanel
	Public WithEvents ribCompOFF As AxThreed.AxSSRibbon
	Public WithEvents ribCompON As AxThreed.AxSSRibbon
	Public WithEvents fraOnOff As AxThreed.AxSSFrame
	Public WithEvents fraProbeComp As AxThreed.AxSSFrame
	Public WithEvents chkECL0 As AxThreed.AxSSCheck
	Public WithEvents chkExternalOutput As AxThreed.AxSSCheck
	Public WithEvents chkECL1 As AxThreed.AxSSCheck
	Public WithEvents fraOutputTrig As AxThreed.AxSSFrame
	Public WithEvents _optExtTrigCoup_0 As AxThreed.AxSSOption
	Public WithEvents _optExtTrigCoup_1 As AxThreed.AxSSOption
	Public WithEvents fraExtTriggerCoup As AxThreed.AxSSFrame
	Public WithEvents cboTraceColor As System.Windows.Forms.ComboBox
	Public WithEvents picTraceColor As System.Windows.Forms.PictureBox
	Public WithEvents fraTraceColor As AxThreed.AxSSFrame
	Public WithEvents cboTraceThickness As System.Windows.Forms.ComboBox
	Public WithEvents fraTraceThickness As AxThreed.AxSSFrame
	Public WithEvents cmdAbout As System.Windows.Forms.Button
	Public WithEvents cmdUpdateTIP As System.Windows.Forms.Button
	Public WithEvents cmdPrint As System.Windows.Forms.Button
	Public WithEvents cmdSelfTest As System.Windows.Forms.Button
	Public WithEvents Panel_Conifg As AxVIPERT_Common_Controls.AxPanel_Conifg
	Public WithEvents _tabMainFunctions_TabPage6 As System.Windows.Forms.TabPage
	Public WithEvents Atlas_SFP As AxVIPERT_Common_Controls.AxAtlas_SFP
	Public WithEvents _tabMainFunctions_TabPage7 As System.Windows.Forms.TabPage
	Public WithEvents tabMainFunctions As System.Windows.Forms.TabControl
	Public WithEvents cmdAutoscale As System.Windows.Forms.Button
	Public WithEvents cmdMeasure As System.Windows.Forms.Button
	Public WithEvents cmdQuit As System.Windows.Forms.Button
	Public WithEvents cmdReset As System.Windows.Forms.Button
	Public WithEvents txtDataDisplay As System.Windows.Forms.Label
	Public WithEvents panDmmDisplay As AxThreed.AxSSPanel
	Public WithEvents cwgScopeDisplay As AxCWUIControlsLib.AxCWGraph
	Public WithEvents panScopeDisplay As AxThreed.AxSSPanel
	Public WithEvents StatusBar1 As AxComctlLib.AxStatusBar
	Public WithEvents chkContinuous As AxThreed.AxSSCheck
	Public CommonDialog1Open As System.Windows.Forms.OpenFileDialog
	Public CommonDialog1Save As System.Windows.Forms.SaveFileDialog
	Public CommonDialog1Font As System.Windows.Forms.FontDialog
	Public CommonDialog1Color As System.Windows.Forms.ColorDialog
	Public CommonDialog1Print As System.Windows.Forms.PrintDialog
	Public dlgFileIOOpen As System.Windows.Forms.OpenFileDialog
	Public dlgFileIOSave As System.Windows.Forms.SaveFileDialog
	Public dlgFileIOFont As System.Windows.Forms.FontDialog
	Public dlgFileIOColor As System.Windows.Forms.ColorDialog
	Public dlgFileIOPrint As System.Windows.Forms.PrintDialog
	Public WithEvents imgLogo As System.Windows.Forms.PictureBox
	Public WithEvents imgFunctions As AxComctlLib.AxImageList
	Public WithEvents cboVertRange As Microsoft.VisualBasic.Compatibility.VB6.ComboBoxArray
	Public WithEvents chkDisplayTrace As AxSSCheckArray.AxSSCheckArray
	Public WithEvents cmdSaveToDisk As Microsoft.VisualBasic.Compatibility.VB6.ButtonArray
	Public WithEvents cmdSaveToMem As Microsoft.VisualBasic.Compatibility.VB6.ButtonArray
	Public WithEvents fraCoupling As AxSSFrameArray.AxSSFrameArray
	Public WithEvents fraFunction As AxSSFrameArray.AxSSFrameArray
	Public WithEvents fraProbeAttenuation As AxSSFrameArray.AxSSFrameArray
	Public WithEvents fraSignalFilters As AxSSFrameArray.AxSSFrameArray
	Public WithEvents fraVerticalRange As AxSSFrameArray.AxSSFrameArray
	Public WithEvents optCoupling1 As AxSSOptionArray.AxSSOptionArray
	Public WithEvents optCoupling2 As AxSSOptionArray.AxSSOptionArray
	Public WithEvents optDataPoints As AxSSOptionArray.AxSSOptionArray
	Public WithEvents optDelaySlope As AxSSOptionArray.AxSSOptionArray
	Public WithEvents optExtTrigCoup As AxSSOptionArray.AxSSOptionArray
	Public WithEvents optField As AxSSOptionArray.AxSSOptionArray
	Public WithEvents optHoldOff As AxSSOptionArray.AxSSOptionArray
	Public WithEvents optNoiseRej As AxSSOptionArray.AxSSOptionArray
	Public WithEvents optProbe1 As AxSSOptionArray.AxSSOptionArray
	Public WithEvents optProbe2 As AxSSOptionArray.AxSSOptionArray
	Public WithEvents optReference As AxSSOptionArray.AxSSOptionArray
	Public WithEvents optStandard As AxSSOptionArray.AxSSOptionArray
	Public WithEvents optStartLevel As AxSSOptionArray.AxSSOptionArray
	Public WithEvents optStartSlope As AxSSOptionArray.AxSSOptionArray
	Public WithEvents optStopSlope As AxSSOptionArray.AxSSOptionArray
	Public WithEvents optTrigDelay As AxSSOptionArray.AxSSOptionArray
	Public WithEvents optTrigMode As AxSSOptionArray.AxSSOptionArray
	Public WithEvents optTrigSlope As AxSSOptionArray.AxSSOptionArray
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
		Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmZT1428))
		Me.components = New System.ComponentModel.Container()
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(components)
		Me.dlgSegmentDataOpen = New System.Windows.Forms.OpenFileDialog
		Me.dlgSegmentDataSave = New System.Windows.Forms.SaveFileDialog
		Me.dlgSegmentDataFont = New System.Windows.Forms.FontDialog
		Me.dlgSegmentDataColor = New System.Windows.Forms.ColorDialog
		Me.dlgSegmentDataPrint = New System.Windows.Forms.PrintDialog
		Me.cmdDisplaySize = New System.Windows.Forms.Button
		Me.tabMainFunctions = New System.Windows.Forms.TabControl
		Me._tabMainFunctions_TabPage0 = New System.Windows.Forms.TabPage
		Me.fraHoldOff = New AxThreed.AxSSFrame
		Me.SSPanel7 = New AxThreed.AxSSPanel
		Me.txtHoldOff = New System.Windows.Forms.TextBox
		Me.spnHoldOff = New AxSpin.AxSpinButton
		Me._optHoldOff_1 = New AxThreed.AxSSOption
		Me._optHoldOff_0 = New AxThreed.AxSSOption
		Me.lblEvents = New System.Windows.Forms.Label
		Me.fraDealy = New AxThreed.AxSSFrame
		Me.SSPanel6 = New AxThreed.AxSSPanel
		Me.txtDelay = New System.Windows.Forms.TextBox
		Me.spnDelay = New AxSpin.AxSpinButton
		Me.fraHorizRange = New AxThreed.AxSSFrame
		Me.cboHorizRange = New System.Windows.Forms.ComboBox
		Me.fraCompletion = New AxThreed.AxSSFrame
		Me.SSPanel9 = New AxThreed.AxSSPanel
		Me.txtCompletion = New System.Windows.Forms.TextBox
		Me.spnCompletion = New AxSpin.AxSpinButton
		Me.fraDataPoints = New AxThreed.AxSSFrame
		Me._optDataPoints_0 = New AxThreed.AxSSOption
		Me._optDataPoints_1 = New AxThreed.AxSSOption
		Me.fraReference = New AxThreed.AxSSFrame
		Me._optReference_2 = New AxThreed.AxSSOption
		Me._optReference_0 = New AxThreed.AxSSOption
		Me._optReference_1 = New AxThreed.AxSSOption
		Me.fraAvgCount = New AxThreed.AxSSFrame
		Me.SSPanel8 = New AxThreed.AxSSPanel
		Me.txtAvgCount = New System.Windows.Forms.TextBox
		Me.spnAvgCount = New AxSpin.AxSpinButton
		Me.fraSampleType = New AxThreed.AxSSFrame
		Me.cboSampleType = New System.Windows.Forms.ComboBox
		Me.fraAcqMode = New AxThreed.AxSSFrame
		Me.cboAcqMode = New System.Windows.Forms.ComboBox
		Me.fraDisplay = New AxThreed.AxSSFrame
		Me._chkDisplayTrace_0 = New AxThreed.AxSSCheck
		Me._chkDisplayTrace_1 = New AxThreed.AxSSCheck
		Me._chkDisplayTrace_2 = New AxThreed.AxSSCheck
		Me._chkDisplayTrace_3 = New AxThreed.AxSSCheck
		Me._tabMainFunctions_TabPage1 = New System.Windows.Forms.TabPage
		Me._cmdSaveToDisk_0 = New System.Windows.Forms.Button
		Me._cmdSaveToMem_0 = New System.Windows.Forms.Button
		Me._fraSignalFilters_0 = New AxThreed.AxSSFrame
		Me.chkLFReject1 = New AxThreed.AxSSCheck
		Me.chkHFReject1 = New AxThreed.AxSSCheck
		Me._fraProbeAttenuation_0 = New AxThreed.AxSSFrame
		Me._optProbe1_3 = New AxThreed.AxSSOption
		Me._optProbe1_2 = New AxThreed.AxSSOption
		Me._optProbe1_1 = New AxThreed.AxSSOption
		Me._optProbe1_0 = New AxThreed.AxSSOption
		Me._fraVerticalRange_0 = New AxThreed.AxSSFrame
		Me._cboVertRange_0 = New System.Windows.Forms.ComboBox
		Me._fraCoupling_0 = New AxThreed.AxSSFrame
		Me._optCoupling1_1 = New AxThreed.AxSSOption
		Me._optCoupling1_0 = New AxThreed.AxSSOption
		Me._optCoupling1_2 = New AxThreed.AxSSOption
		Me.fraOffset1 = New AxThreed.AxSSFrame
		Me.SSPanel10 = New AxThreed.AxSSPanel
		Me.txtVOffset1 = New System.Windows.Forms.TextBox
		Me.spnVOffset1 = New AxSpin.AxSpinButton
		Me._tabMainFunctions_TabPage2 = New System.Windows.Forms.TabPage
		Me.fraOffset2 = New AxThreed.AxSSFrame
		Me.SSPanel11 = New AxThreed.AxSSPanel
		Me.txtVOffset2 = New System.Windows.Forms.TextBox
		Me.spnVOffset2 = New AxSpin.AxSpinButton
		Me._fraCoupling_1 = New AxThreed.AxSSFrame
		Me._optCoupling2_2 = New AxThreed.AxSSOption
		Me._optCoupling2_0 = New AxThreed.AxSSOption
		Me._optCoupling2_1 = New AxThreed.AxSSOption
		Me._fraVerticalRange_1 = New AxThreed.AxSSFrame
		Me._cboVertRange_1 = New System.Windows.Forms.ComboBox
		Me._fraProbeAttenuation_1 = New AxThreed.AxSSFrame
		Me._optProbe2_0 = New AxThreed.AxSSOption
		Me._optProbe2_1 = New AxThreed.AxSSOption
		Me._optProbe2_2 = New AxThreed.AxSSOption
		Me._optProbe2_3 = New AxThreed.AxSSOption
		Me._fraSignalFilters_1 = New AxThreed.AxSSFrame
		Me.chkHFReject2 = New AxThreed.AxSSCheck
		Me.chkLFReject2 = New AxThreed.AxSSCheck
		Me._cmdSaveToMem_1 = New System.Windows.Forms.Button
		Me._cmdSaveToDisk_1 = New System.Windows.Forms.Button
		Me._tabMainFunctions_TabPage3 = New System.Windows.Forms.TabPage
		Me._fraFunction_2 = New AxThreed.AxSSFrame
		Me.tbrMeasFunction = New AxComctlLib.AxToolbar
		Me.cboMeasSour = New System.Windows.Forms.ComboBox
		Me.fraDelayParam = New AxThreed.AxSSPanel
		Me.cboStopSource = New System.Windows.Forms.ComboBox
		Me.cboStartSource = New System.Windows.Forms.ComboBox
		Me.Picture2 = New System.Windows.Forms.Panel
		Me._optStartSlope_0 = New AxThreed.AxSSOption
		Me._optStartSlope_1 = New AxThreed.AxSSOption
		Me.lblStartSlope = New System.Windows.Forms.Label
		Me.Picture3 = New System.Windows.Forms.Panel
		Me._optStopSlope_0 = New AxThreed.AxSSOption
		Me._optStopSlope_1 = New AxThreed.AxSSOption
		Me.lblStopSlope = New System.Windows.Forms.Label
		Me.Picture4 = New System.Windows.Forms.Panel
		Me._optStartLevel_0 = New AxThreed.AxSSOption
		Me._optStartLevel_1 = New AxThreed.AxSSOption
		Me.lblStartLevelCap = New System.Windows.Forms.Label
		Me.SSPanel1 = New AxThreed.AxSSPanel
		Me.txtStartEdge = New System.Windows.Forms.TextBox
		Me.spnStartEdge = New AxSpin.AxSpinButton
		Me.SSPanel3 = New AxThreed.AxSSPanel
		Me.txtStartLevel = New System.Windows.Forms.TextBox
		Me.spnStartLevel = New AxSpin.AxSpinButton
		Me.SSPanel4 = New AxThreed.AxSSPanel
		Me.txtStopEdge = New System.Windows.Forms.TextBox
		Me.spnStopEdge = New AxSpin.AxSpinButton
		Me.SSPanel5 = New AxThreed.AxSSPanel
		Me.txtStopLevel = New System.Windows.Forms.TextBox
		Me.spnStopLevel = New AxSpin.AxSpinButton
		Me.Label1 = New System.Windows.Forms.Label
		Me.lblStopEdgeCap = New System.Windows.Forms.Label
		Me.lblStopSource = New System.Windows.Forms.Label
		Me.lblStartEdgeCap = New System.Windows.Forms.Label
		Me.Line1 = New System.Windows.Forms.Label
		Me.lblStop = New System.Windows.Forms.Label
		Me.lblStar = New System.Windows.Forms.Label
		Me.lblStartSource = New System.Windows.Forms.Label
		Me.cmdHelp = New System.Windows.Forms.Button
		Me._tabMainFunctions_TabPage4 = New System.Windows.Forms.TabPage
		Me.fraLine = New AxThreed.AxSSFrame
		Me.SSPanel14 = New AxThreed.AxSSPanel
		Me.txtLine = New System.Windows.Forms.TextBox
		Me.spnLine = New AxSpin.AxSpinButton
		Me.fraField = New AxThreed.AxSSFrame
		Me._optField_0 = New AxThreed.AxSSOption
		Me._optField_1 = New AxThreed.AxSSOption
		Me.fraStandard = New AxThreed.AxSSFrame
		Me._optStandard_0 = New AxThreed.AxSSOption
		Me._optStandard_1 = New AxThreed.AxSSOption
		Me.fraOccurrence = New AxThreed.AxSSFrame
		Me.SSPanel15 = New AxThreed.AxSSPanel
		Me.txtOccurrence = New System.Windows.Forms.TextBox
		Me.spnOccurrence = New AxSpin.AxSpinButton
		Me.fraDelaySlope = New AxThreed.AxSSFrame
		Me._optDelaySlope_0 = New AxThreed.AxSSOption
		Me._optDelaySlope_1 = New AxThreed.AxSSOption
		Me.fraTrigDelaySource = New AxThreed.AxSSFrame
		Me.cboTrigDelaySour = New System.Windows.Forms.ComboBox
		Me.fraTrigDelay = New AxThreed.AxSSFrame
		Me.SSPanel13 = New AxThreed.AxSSPanel
		Me.txtDelEvents = New System.Windows.Forms.TextBox
		Me.spnDelEvents = New AxSpin.AxSpinButton
		Me._optTrigDelay_1 = New AxThreed.AxSSOption
		Me._optTrigDelay_0 = New AxThreed.AxSSOption
		Me.lblTrigDelayEvents = New System.Windows.Forms.Label
		Me.fraTrigLevel = New AxThreed.AxSSFrame
		Me.SSPanel12 = New AxThreed.AxSSPanel
		Me.txtTrigLevel = New System.Windows.Forms.TextBox
		Me.spnTrigLevel = New AxSpin.AxSpinButton
		Me.fraNoiseRej = New AxThreed.AxSSFrame
		Me._optNoiseRej_1 = New AxThreed.AxSSOption
		Me._optNoiseRej_0 = New AxThreed.AxSSOption
		Me.fraTrigSlope = New AxThreed.AxSSFrame
		Me._optTrigSlope_1 = New AxThreed.AxSSOption
		Me._optTrigSlope_0 = New AxThreed.AxSSOption
		Me.fraTrigSour = New AxThreed.AxSSFrame
		Me.cboTrigSour = New System.Windows.Forms.ComboBox
		Me.fraTrigMode = New AxThreed.AxSSFrame
		Me._optTrigMode_1 = New AxThreed.AxSSOption
		Me._optTrigMode_0 = New AxThreed.AxSSOption
		Me._optTrigMode_2 = New AxThreed.AxSSOption
		Me._tabMainFunctions_TabPage5 = New System.Windows.Forms.TabPage
		Me.picMemOptions = New System.Windows.Forms.Panel
		Me.cmdLoadFromDisk = New System.Windows.Forms.Button
		Me._cmdSaveToDisk_3 = New System.Windows.Forms.Button
		Me.fraMemVerticalRange = New AxThreed.AxSSFrame
		Me.SSPanel19 = New AxThreed.AxSSPanel
		Me.txtMemoryRange = New System.Windows.Forms.TextBox
		Me.SpinButton2 = New AxSpin.AxSpinButton
		Me.SSFrame2 = New AxThreed.AxSSFrame
		Me.SSPanel17 = New AxThreed.AxSSPanel
		Me.txtMemVoltOffset = New System.Windows.Forms.TextBox
		Me.spnMemVOffset = New AxSpin.AxSpinButton
		Me.Line5 = New System.Windows.Forms.Label
		Me.Line4 = New System.Windows.Forms.Label
		Me.Label3 = New System.Windows.Forms.Label
		Me.picMathOptions = New System.Windows.Forms.Panel
		Me._cmdSaveToMem_2 = New System.Windows.Forms.Button
		Me._cmdSaveToDisk_2 = New System.Windows.Forms.Button
		Me.fraRange = New AxThreed.AxSSFrame
		Me.cboMathSourceA = New System.Windows.Forms.ComboBox
		Me.fraMathSourceB = New AxThreed.AxSSFrame
		Me.cboMathSourceB = New System.Windows.Forms.ComboBox
		Me.fraOperation = New AxThreed.AxSSFrame
		Me.cboMathFunction = New System.Windows.Forms.ComboBox
		Me.fraMemVRange = New AxThreed.AxSSFrame
		Me.SSPanel18 = New AxThreed.AxSSPanel
		Me.txtMathRange = New System.Windows.Forms.TextBox
		Me.SpinButton1 = New AxSpin.AxSpinButton
		Me.SSFrame1 = New AxThreed.AxSSFrame
		Me.SSPanel16 = New AxThreed.AxSSPanel
		Me.txtMathVOffset = New System.Windows.Forms.TextBox
		Me.spnMathVOffset = New AxSpin.AxSpinButton
		Me.Line3 = New System.Windows.Forms.Label
		Me.Line2 = New System.Windows.Forms.Label
		Me.Label2 = New System.Windows.Forms.Label
		Me._tabMainFunctions_TabPage6 = New System.Windows.Forms.TabPage
		Me.fraProbeComp = New AxThreed.AxSSFrame
		Me.fraOnOff = New AxThreed.AxSSFrame
		Me.panWaveDisplay = New AxThreed.AxSSPanel
		Me.imgProbeComp = New System.Windows.Forms.PictureBox
		Me.ribCompOFF = New AxThreed.AxSSRibbon
		Me.ribCompON = New AxThreed.AxSSRibbon
		Me.fraOutputTrig = New AxThreed.AxSSFrame
		Me.chkECL0 = New AxThreed.AxSSCheck
		Me.chkExternalOutput = New AxThreed.AxSSCheck
		Me.chkECL1 = New AxThreed.AxSSCheck
		Me.fraExtTriggerCoup = New AxThreed.AxSSFrame
		Me._optExtTrigCoup_0 = New AxThreed.AxSSOption
		Me._optExtTrigCoup_1 = New AxThreed.AxSSOption
		Me.fraTraceColor = New AxThreed.AxSSFrame
		Me.cboTraceColor = New System.Windows.Forms.ComboBox
		Me.picTraceColor = New System.Windows.Forms.PictureBox
		Me.fraTraceThickness = New AxThreed.AxSSFrame
		Me.cboTraceThickness = New System.Windows.Forms.ComboBox
		Me.cmdAbout = New System.Windows.Forms.Button
		Me.cmdUpdateTIP = New System.Windows.Forms.Button
		Me.cmdPrint = New System.Windows.Forms.Button
		Me.cmdSelfTest = New System.Windows.Forms.Button
		Me.Panel_Conifg = New AxVIPERT_Common_Controls.AxPanel_Conifg
		Me._tabMainFunctions_TabPage7 = New System.Windows.Forms.TabPage
		Me.Atlas_SFP = New AxVIPERT_Common_Controls.AxAtlas_SFP
		Me.cmdAutoscale = New System.Windows.Forms.Button
		Me.cmdMeasure = New System.Windows.Forms.Button
		Me.cmdQuit = New System.Windows.Forms.Button
		Me.cmdReset = New System.Windows.Forms.Button
		Me.panDmmDisplay = New AxThreed.AxSSPanel
		Me.txtDataDisplay = New System.Windows.Forms.Label
		Me.panScopeDisplay = New AxThreed.AxSSPanel
		Me.cwgScopeDisplay = New AxCWUIControlsLib.AxCWGraph
		Me.StatusBar1 = New AxComctlLib.AxStatusBar
		Me.chkContinuous = New AxThreed.AxSSCheck
		Me.CommonDialog1Open = New System.Windows.Forms.OpenFileDialog
		Me.CommonDialog1Save = New System.Windows.Forms.SaveFileDialog
		Me.CommonDialog1Font = New System.Windows.Forms.FontDialog
		Me.CommonDialog1Color = New System.Windows.Forms.ColorDialog
		Me.CommonDialog1Print = New System.Windows.Forms.PrintDialog
		Me.dlgFileIOOpen = New System.Windows.Forms.OpenFileDialog
		Me.dlgFileIOSave = New System.Windows.Forms.SaveFileDialog
		Me.dlgFileIOFont = New System.Windows.Forms.FontDialog
		Me.dlgFileIOColor = New System.Windows.Forms.ColorDialog
		Me.dlgFileIOPrint = New System.Windows.Forms.PrintDialog
		Me.imgLogo = New System.Windows.Forms.PictureBox
		Me.imgFunctions = New AxComctlLib.AxImageList
		Me.cboVertRange = New Microsoft.VisualBasic.Compatibility.VB6.ComboBoxArray(components)
		Me.chkDisplayTrace = New AxSSCheckArray.AxSSCheckArray(components)
		Me.cmdSaveToDisk = New Microsoft.VisualBasic.Compatibility.VB6.ButtonArray(components)
		Me.cmdSaveToMem = New Microsoft.VisualBasic.Compatibility.VB6.ButtonArray(components)
		Me.fraCoupling = New AxSSFrameArray.AxSSFrameArray(components)
		Me.fraFunction = New AxSSFrameArray.AxSSFrameArray(components)
		Me.fraProbeAttenuation = New AxSSFrameArray.AxSSFrameArray(components)
		Me.fraSignalFilters = New AxSSFrameArray.AxSSFrameArray(components)
		Me.fraVerticalRange = New AxSSFrameArray.AxSSFrameArray(components)
		Me.optCoupling1 = New AxSSOptionArray.AxSSOptionArray(components)
		Me.optCoupling2 = New AxSSOptionArray.AxSSOptionArray(components)
		Me.optDataPoints = New AxSSOptionArray.AxSSOptionArray(components)
		Me.optDelaySlope = New AxSSOptionArray.AxSSOptionArray(components)
		Me.optExtTrigCoup = New AxSSOptionArray.AxSSOptionArray(components)
		Me.optField = New AxSSOptionArray.AxSSOptionArray(components)
		Me.optHoldOff = New AxSSOptionArray.AxSSOptionArray(components)
		Me.optNoiseRej = New AxSSOptionArray.AxSSOptionArray(components)
		Me.optProbe1 = New AxSSOptionArray.AxSSOptionArray(components)
		Me.optProbe2 = New AxSSOptionArray.AxSSOptionArray(components)
		Me.optReference = New AxSSOptionArray.AxSSOptionArray(components)
		Me.optStandard = New AxSSOptionArray.AxSSOptionArray(components)
		Me.optStartLevel = New AxSSOptionArray.AxSSOptionArray(components)
		Me.optStartSlope = New AxSSOptionArray.AxSSOptionArray(components)
		Me.optStopSlope = New AxSSOptionArray.AxSSOptionArray(components)
		Me.optTrigDelay = New AxSSOptionArray.AxSSOptionArray(components)
		Me.optTrigMode = New AxSSOptionArray.AxSSOptionArray(components)
		Me.optTrigSlope = New AxSSOptionArray.AxSSOptionArray(components)
		Me.tabMainFunctions.SuspendLayout()
		Me._tabMainFunctions_TabPage0.SuspendLayout()
		Me.fraHoldOff.SuspendLayout()
		Me.SSPanel7.SuspendLayout()
		Me.fraDealy.SuspendLayout()
		Me.SSPanel6.SuspendLayout()
		Me.fraHorizRange.SuspendLayout()
		Me.fraCompletion.SuspendLayout()
		Me.SSPanel9.SuspendLayout()
		Me.fraDataPoints.SuspendLayout()
		Me.fraReference.SuspendLayout()
		Me.fraAvgCount.SuspendLayout()
		Me.SSPanel8.SuspendLayout()
		Me.fraSampleType.SuspendLayout()
		Me.fraAcqMode.SuspendLayout()
		Me.fraDisplay.SuspendLayout()
		Me._tabMainFunctions_TabPage1.SuspendLayout()
		Me._fraSignalFilters_0.SuspendLayout()
		Me._fraProbeAttenuation_0.SuspendLayout()
		Me._fraVerticalRange_0.SuspendLayout()
		Me._fraCoupling_0.SuspendLayout()
		Me.fraOffset1.SuspendLayout()
		Me.SSPanel10.SuspendLayout()
		Me._tabMainFunctions_TabPage2.SuspendLayout()
		Me.fraOffset2.SuspendLayout()
		Me.SSPanel11.SuspendLayout()
		Me._fraCoupling_1.SuspendLayout()
		Me._fraVerticalRange_1.SuspendLayout()
		Me._fraProbeAttenuation_1.SuspendLayout()
		Me._fraSignalFilters_1.SuspendLayout()
		Me._tabMainFunctions_TabPage3.SuspendLayout()
		Me._fraFunction_2.SuspendLayout()
		Me.fraDelayParam.SuspendLayout()
		Me.Picture2.SuspendLayout()
		Me.Picture3.SuspendLayout()
		Me.Picture4.SuspendLayout()
		Me.SSPanel1.SuspendLayout()
		Me.SSPanel3.SuspendLayout()
		Me.SSPanel4.SuspendLayout()
		Me.SSPanel5.SuspendLayout()
		Me._tabMainFunctions_TabPage4.SuspendLayout()
		Me.fraLine.SuspendLayout()
		Me.SSPanel14.SuspendLayout()
		Me.fraField.SuspendLayout()
		Me.fraStandard.SuspendLayout()
		Me.fraOccurrence.SuspendLayout()
		Me.SSPanel15.SuspendLayout()
		Me.fraDelaySlope.SuspendLayout()
		Me.fraTrigDelaySource.SuspendLayout()
		Me.fraTrigDelay.SuspendLayout()
		Me.SSPanel13.SuspendLayout()
		Me.fraTrigLevel.SuspendLayout()
		Me.SSPanel12.SuspendLayout()
		Me.fraNoiseRej.SuspendLayout()
		Me.fraTrigSlope.SuspendLayout()
		Me.fraTrigSour.SuspendLayout()
		Me.fraTrigMode.SuspendLayout()
		Me._tabMainFunctions_TabPage5.SuspendLayout()
		Me.picMemOptions.SuspendLayout()
		Me.fraMemVerticalRange.SuspendLayout()
		Me.SSPanel19.SuspendLayout()
		Me.SSFrame2.SuspendLayout()
		Me.SSPanel17.SuspendLayout()
		Me.picMathOptions.SuspendLayout()
		Me.fraRange.SuspendLayout()
		Me.fraMathSourceB.SuspendLayout()
		Me.fraOperation.SuspendLayout()
		Me.fraMemVRange.SuspendLayout()
		Me.SSPanel18.SuspendLayout()
		Me.SSFrame1.SuspendLayout()
		Me.SSPanel16.SuspendLayout()
		Me._tabMainFunctions_TabPage6.SuspendLayout()
		Me.fraProbeComp.SuspendLayout()
		Me.fraOnOff.SuspendLayout()
		Me.panWaveDisplay.SuspendLayout()
		Me.fraOutputTrig.SuspendLayout()
		Me.fraExtTriggerCoup.SuspendLayout()
		Me.fraTraceColor.SuspendLayout()
		Me.fraTraceThickness.SuspendLayout()
		Me._tabMainFunctions_TabPage7.SuspendLayout()
		Me.panDmmDisplay.SuspendLayout()
		Me.panScopeDisplay.SuspendLayout()
		Me.SuspendLayout()
		Me.ToolTip1.Active = True
		CType(Me.spnHoldOff, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.SSPanel7, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me._optHoldOff_1, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me._optHoldOff_0, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.fraHoldOff, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.spnDelay, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.SSPanel6, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.fraDealy, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.fraHorizRange, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.spnCompletion, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.SSPanel9, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.fraCompletion, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me._optDataPoints_0, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me._optDataPoints_1, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.fraDataPoints, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me._optReference_2, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me._optReference_0, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me._optReference_1, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.fraReference, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.spnAvgCount, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.SSPanel8, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.fraAvgCount, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.fraSampleType, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.fraAcqMode, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me._chkDisplayTrace_0, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me._chkDisplayTrace_1, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me._chkDisplayTrace_2, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me._chkDisplayTrace_3, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.fraDisplay, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.chkLFReject1, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.chkHFReject1, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me._fraSignalFilters_0, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me._optProbe1_3, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me._optProbe1_2, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me._optProbe1_1, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me._optProbe1_0, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me._fraProbeAttenuation_0, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me._fraVerticalRange_0, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me._optCoupling1_1, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me._optCoupling1_0, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me._optCoupling1_2, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me._fraCoupling_0, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.spnVOffset1, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.SSPanel10, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.fraOffset1, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.spnVOffset2, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.SSPanel11, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.fraOffset2, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me._optCoupling2_2, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me._optCoupling2_0, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me._optCoupling2_1, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me._fraCoupling_1, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me._fraVerticalRange_1, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me._optProbe2_0, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me._optProbe2_1, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me._optProbe2_2, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me._optProbe2_3, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me._fraProbeAttenuation_1, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.chkHFReject2, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.chkLFReject2, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me._fraSignalFilters_1, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.tbrMeasFunction, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me._optStartSlope_0, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me._optStartSlope_1, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me._optStopSlope_0, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me._optStopSlope_1, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me._optStartLevel_0, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me._optStartLevel_1, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.spnStartEdge, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.SSPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.spnStartLevel, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.SSPanel3, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.spnStopEdge, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.SSPanel4, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.spnStopLevel, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.SSPanel5, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.fraDelayParam, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me._fraFunction_2, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.spnLine, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.SSPanel14, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.fraLine, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me._optField_0, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me._optField_1, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.fraField, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me._optStandard_0, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me._optStandard_1, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.fraStandard, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.spnOccurrence, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.SSPanel15, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.fraOccurrence, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me._optDelaySlope_0, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me._optDelaySlope_1, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.fraDelaySlope, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.fraTrigDelaySource, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.spnDelEvents, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.SSPanel13, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me._optTrigDelay_1, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me._optTrigDelay_0, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.fraTrigDelay, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.spnTrigLevel, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.SSPanel12, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.fraTrigLevel, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me._optNoiseRej_1, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me._optNoiseRej_0, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.fraNoiseRej, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me._optTrigSlope_1, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me._optTrigSlope_0, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.fraTrigSlope, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.fraTrigSour, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me._optTrigMode_1, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me._optTrigMode_0, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me._optTrigMode_2, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.fraTrigMode, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.SpinButton2, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.SSPanel19, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.fraMemVerticalRange, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.spnMemVOffset, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.SSPanel17, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.SSFrame2, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.fraRange, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.fraMathSourceB, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.fraOperation, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.SpinButton1, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.SSPanel18, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.fraMemVRange, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.spnMathVOffset, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.SSPanel16, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.SSFrame1, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.panWaveDisplay, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.ribCompOFF, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.ribCompON, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.fraOnOff, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.fraProbeComp, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.chkECL0, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.chkExternalOutput, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.chkECL1, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.fraOutputTrig, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me._optExtTrigCoup_0, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me._optExtTrigCoup_1, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.fraExtTriggerCoup, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.fraTraceColor, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.fraTraceThickness, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.Panel_Conifg, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.Atlas_SFP, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.panDmmDisplay, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.cwgScopeDisplay, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.panScopeDisplay, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.StatusBar1, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.chkContinuous, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.imgFunctions, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.cboVertRange, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.chkDisplayTrace, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.cmdSaveToDisk, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.cmdSaveToMem, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.fraCoupling, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.fraFunction, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.fraProbeAttenuation, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.fraSignalFilters, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.fraVerticalRange, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.optCoupling1, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.optCoupling2, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.optDataPoints, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.optDelaySlope, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.optExtTrigCoup, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.optField, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.optHoldOff, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.optNoiseRej, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.optProbe1, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.optProbe2, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.optReference, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.optStandard, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.optStartLevel, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.optStartSlope, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.optStopSlope, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.optTrigDelay, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.optTrigMode, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.optTrigSlope, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
		Me.Text = "Digitizing Oscilloscope"
		Me.ClientSize = New System.Drawing.Size(758, 338)
		Me.Location = New System.Drawing.Point(54, 118)
		Me.ForeColor = System.Drawing.Color.Black
		Me.Icon = CType(resources.GetObject("frmZT1428.Icon"), System.Drawing.Icon)
		Me.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.SystemColors.Control
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable
		Me.ControlBox = True
		Me.Enabled = True
		Me.KeyPreview = False
		Me.MaximizeBox = True
		Me.MinimizeBox = True
		Me.Cursor = System.Windows.Forms.Cursors.Default
		Me.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.ShowInTaskbar = True
		Me.HelpButton = False
		Me.WindowState = System.Windows.Forms.FormWindowState.Normal
		Me.Name = "frmZT1428"
		Me.dlgSegmentDataOpen.DefaultExt = "seg"
		Me.dlgSegmentDataOpen.FileName = "*.seg"
		Me.dlgSegmentDataOpen.Filter = "*.seg"
		Me.dlgSegmentDataOpen.FilterIndex = 1
		Me.cmdDisplaySize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		Me.cmdDisplaySize.Text = "&Full Size"
		Me.cmdDisplaySize.Size = New System.Drawing.Size(67, 23)
		Me.cmdDisplaySize.Location = New System.Drawing.Point(392, 292)
		Me.cmdDisplaySize.TabIndex = 210
		Me.cmdDisplaySize.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdDisplaySize.BackColor = System.Drawing.SystemColors.Control
		Me.cmdDisplaySize.CausesValidation = True
		Me.cmdDisplaySize.Enabled = True
		Me.cmdDisplaySize.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdDisplaySize.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdDisplaySize.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdDisplaySize.TabStop = True
		Me.cmdDisplaySize.Name = "cmdDisplaySize"
		Me.tabMainFunctions.Size = New System.Drawing.Size(436, 275)
		Me.tabMainFunctions.Location = New System.Drawing.Point(312, 4)
		Me.tabMainFunctions.TabIndex = 0
		Me.tabMainFunctions.Appearance = System.Windows.Forms.TabAppearance.FlatButtons
		Me.tabMainFunctions.ItemSize = New System.Drawing.Size(42, 18)
		Me.tabMainFunctions.HotTrack = False
		Me.tabMainFunctions.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.tabMainFunctions.Name = "tabMainFunctions"
		Me._tabMainFunctions_TabPage0.Text = "Timebase/Digitize "
		fraHoldOff.OcxState = CType(resources.GetObject("fraHoldOff.OcxState"), System.Windows.Forms.AxHost.State)
		Me.fraHoldOff.Size = New System.Drawing.Size(97, 129)
		Me.fraHoldOff.Location = New System.Drawing.Point(192, 136)
		Me.fraHoldOff.TabIndex = 125
		Me.fraHoldOff.Name = "fraHoldOff"
		SSPanel7.OcxState = CType(resources.GetObject("SSPanel7.OcxState"), System.Windows.Forms.AxHost.State)
		Me.SSPanel7.Size = New System.Drawing.Size(81, 19)
		Me.SSPanel7.Location = New System.Drawing.Point(8, 96)
		Me.SSPanel7.TabIndex = 126
		Me.SSPanel7.Name = "SSPanel7"
		Me.txtHoldOff.AutoSize = False
		Me.txtHoldOff.Size = New System.Drawing.Size(57, 17)
		Me.txtHoldOff.Location = New System.Drawing.Point(2, 1)
		Me.txtHoldOff.TabIndex = 127
		Me.txtHoldOff.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtHoldOff.AcceptsReturn = True
		Me.txtHoldOff.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtHoldOff.BackColor = System.Drawing.SystemColors.Window
		Me.txtHoldOff.CausesValidation = True
		Me.txtHoldOff.Enabled = True
		Me.txtHoldOff.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtHoldOff.HideSelection = True
		Me.txtHoldOff.ReadOnly = False
		Me.txtHoldOff.Maxlength = 0
		Me.txtHoldOff.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtHoldOff.MultiLine = False
		Me.txtHoldOff.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtHoldOff.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtHoldOff.TabStop = True
		Me.txtHoldOff.Visible = True
		Me.txtHoldOff.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.txtHoldOff.Name = "txtHoldOff"
		spnHoldOff.OcxState = CType(resources.GetObject("spnHoldOff.OcxState"), System.Windows.Forms.AxHost.State)
		Me.spnHoldOff.Size = New System.Drawing.Size(21, 17)
		Me.spnHoldOff.Location = New System.Drawing.Point(59, 1)
		Me.spnHoldOff.TabIndex = 128
		Me.spnHoldOff.Name = "spnHoldOff"
		_optHoldOff_1.OcxState = CType(resources.GetObject("_optHoldOff_1.OcxState"), System.Windows.Forms.AxHost.State)
		Me._optHoldOff_1.Size = New System.Drawing.Size(65, 17)
		Me._optHoldOff_1.Location = New System.Drawing.Point(8, 40)
		Me._optHoldOff_1.TabIndex = 131
		Me._optHoldOff_1.Name = "_optHoldOff_1"
		_optHoldOff_0.OcxState = CType(resources.GetObject("_optHoldOff_0.OcxState"), System.Windows.Forms.AxHost.State)
		Me._optHoldOff_0.Size = New System.Drawing.Size(65, 17)
		Me._optHoldOff_0.Location = New System.Drawing.Point(8, 24)
		Me._optHoldOff_0.TabIndex = 130
		Me._optHoldOff_0.Name = "_optHoldOff_0"
		Me.lblEvents.Text = "Time"
		Me.lblEvents.Size = New System.Drawing.Size(38, 15)
		Me.lblEvents.Location = New System.Drawing.Point(8, 80)
		Me.lblEvents.TabIndex = 129
		Me.lblEvents.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblEvents.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.lblEvents.BackColor = System.Drawing.SystemColors.Control
		Me.lblEvents.Enabled = True
		Me.lblEvents.ForeColor = System.Drawing.SystemColors.ControlText
		Me.lblEvents.Cursor = System.Windows.Forms.Cursors.Default
		Me.lblEvents.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.lblEvents.UseMnemonic = True
		Me.lblEvents.Visible = True
		Me.lblEvents.AutoSize = False
		Me.lblEvents.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.lblEvents.Name = "lblEvents"
		fraDealy.OcxState = CType(resources.GetObject("fraDealy.OcxState"), System.Windows.Forms.AxHost.State)
		Me.fraDealy.Size = New System.Drawing.Size(97, 45)
		Me.fraDealy.Location = New System.Drawing.Point(192, 92)
		Me.fraDealy.TabIndex = 119
		Me.fraDealy.Name = "fraDealy"
		SSPanel6.OcxState = CType(resources.GetObject("SSPanel6.OcxState"), System.Windows.Forms.AxHost.State)
		Me.SSPanel6.Size = New System.Drawing.Size(81, 19)
		Me.SSPanel6.Location = New System.Drawing.Point(7, 18)
		Me.SSPanel6.TabIndex = 120
		Me.SSPanel6.Name = "SSPanel6"
		Me.txtDelay.AutoSize = False
		Me.txtDelay.Size = New System.Drawing.Size(57, 17)
		Me.txtDelay.Location = New System.Drawing.Point(2, 1)
		Me.txtDelay.TabIndex = 121
		Me.txtDelay.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtDelay.AcceptsReturn = True
		Me.txtDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtDelay.BackColor = System.Drawing.SystemColors.Window
		Me.txtDelay.CausesValidation = True
		Me.txtDelay.Enabled = True
		Me.txtDelay.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtDelay.HideSelection = True
		Me.txtDelay.ReadOnly = False
		Me.txtDelay.Maxlength = 0
		Me.txtDelay.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtDelay.MultiLine = False
		Me.txtDelay.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtDelay.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtDelay.TabStop = True
		Me.txtDelay.Visible = True
		Me.txtDelay.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.txtDelay.Name = "txtDelay"
		spnDelay.OcxState = CType(resources.GetObject("spnDelay.OcxState"), System.Windows.Forms.AxHost.State)
		Me.spnDelay.Size = New System.Drawing.Size(21, 17)
		Me.spnDelay.Location = New System.Drawing.Point(59, 1)
		Me.spnDelay.TabIndex = 122
		Me.spnDelay.Name = "spnDelay"
		fraHorizRange.OcxState = CType(resources.GetObject("fraHorizRange.OcxState"), System.Windows.Forms.AxHost.State)
		Me.fraHorizRange.Size = New System.Drawing.Size(97, 49)
		Me.fraHorizRange.Location = New System.Drawing.Point(192, 44)
		Me.fraHorizRange.TabIndex = 117
		Me.fraHorizRange.Name = "fraHorizRange"
		Me.cboHorizRange.BackColor = System.Drawing.Color.White
		Me.cboHorizRange.Size = New System.Drawing.Size(79, 21)
		Me.cboHorizRange.Location = New System.Drawing.Point(9, 19)
		Me.cboHorizRange.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cboHorizRange.TabIndex = 118
		Me.cboHorizRange.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cboHorizRange.CausesValidation = True
		Me.cboHorizRange.Enabled = True
		Me.cboHorizRange.ForeColor = System.Drawing.SystemColors.WindowText
		Me.cboHorizRange.IntegralHeight = True
		Me.cboHorizRange.Cursor = System.Windows.Forms.Cursors.Default
		Me.cboHorizRange.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cboHorizRange.Sorted = False
		Me.cboHorizRange.TabStop = True
		Me.cboHorizRange.Visible = True
		Me.cboHorizRange.Name = "cboHorizRange"
		fraCompletion.OcxState = CType(resources.GetObject("fraCompletion.OcxState"), System.Windows.Forms.AxHost.State)
		Me.fraCompletion.Size = New System.Drawing.Size(73, 53)
		Me.fraCompletion.Location = New System.Drawing.Point(116, 212)
		Me.fraCompletion.TabIndex = 100
		Me.fraCompletion.Name = "fraCompletion"
		SSPanel9.OcxState = CType(resources.GetObject("SSPanel9.OcxState"), System.Windows.Forms.AxHost.State)
		Me.SSPanel9.Size = New System.Drawing.Size(56, 19)
		Me.SSPanel9.Location = New System.Drawing.Point(7, 20)
		Me.SSPanel9.TabIndex = 101
		Me.SSPanel9.Name = "SSPanel9"
		Me.txtCompletion.AutoSize = False
		Me.txtCompletion.Size = New System.Drawing.Size(31, 17)
		Me.txtCompletion.Location = New System.Drawing.Point(2, 1)
		Me.txtCompletion.TabIndex = 102
		Me.txtCompletion.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtCompletion.AcceptsReturn = True
		Me.txtCompletion.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtCompletion.BackColor = System.Drawing.SystemColors.Window
		Me.txtCompletion.CausesValidation = True
		Me.txtCompletion.Enabled = True
		Me.txtCompletion.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtCompletion.HideSelection = True
		Me.txtCompletion.ReadOnly = False
		Me.txtCompletion.Maxlength = 0
		Me.txtCompletion.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtCompletion.MultiLine = False
		Me.txtCompletion.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtCompletion.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtCompletion.TabStop = True
		Me.txtCompletion.Visible = True
		Me.txtCompletion.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.txtCompletion.Name = "txtCompletion"
		spnCompletion.OcxState = CType(resources.GetObject("spnCompletion.OcxState"), System.Windows.Forms.AxHost.State)
		Me.spnCompletion.Size = New System.Drawing.Size(21, 17)
		Me.spnCompletion.Location = New System.Drawing.Point(35, 1)
		Me.spnCompletion.TabIndex = 103
		Me.spnCompletion.Name = "spnCompletion"
		fraDataPoints.OcxState = CType(resources.GetObject("fraDataPoints.OcxState"), System.Windows.Forms.AxHost.State)
		Me.fraDataPoints.Size = New System.Drawing.Size(73, 77)
		Me.fraDataPoints.Location = New System.Drawing.Point(116, 136)
		Me.fraDataPoints.TabIndex = 95
		Me.fraDataPoints.Name = "fraDataPoints"
		_optDataPoints_0.OcxState = CType(resources.GetObject("_optDataPoints_0.OcxState"), System.Windows.Forms.AxHost.State)
		Me._optDataPoints_0.Size = New System.Drawing.Size(49, 17)
		Me._optDataPoints_0.Location = New System.Drawing.Point(8, 24)
		Me._optDataPoints_0.TabIndex = 97
		Me._optDataPoints_0.Name = "_optDataPoints_0"
		_optDataPoints_1.OcxState = CType(resources.GetObject("_optDataPoints_1.OcxState"), System.Windows.Forms.AxHost.State)
		Me._optDataPoints_1.Size = New System.Drawing.Size(49, 17)
		Me._optDataPoints_1.Location = New System.Drawing.Point(8, 41)
		Me._optDataPoints_1.TabIndex = 96
		Me._optDataPoints_1.Name = "_optDataPoints_1"
		fraReference.OcxState = CType(resources.GetObject("fraReference.OcxState"), System.Windows.Forms.AxHost.State)
		Me.fraReference.Size = New System.Drawing.Size(73, 93)
		Me.fraReference.Location = New System.Drawing.Point(116, 44)
		Me.fraReference.TabIndex = 113
		Me.fraReference.Name = "fraReference"
		_optReference_2.OcxState = CType(resources.GetObject("_optReference_2.OcxState"), System.Windows.Forms.AxHost.State)
		Me._optReference_2.Size = New System.Drawing.Size(49, 17)
		Me._optReference_2.Location = New System.Drawing.Point(8, 60)
		Me._optReference_2.TabIndex = 116
		Me._optReference_2.Name = "_optReference_2"
		_optReference_0.OcxState = CType(resources.GetObject("_optReference_0.OcxState"), System.Windows.Forms.AxHost.State)
		Me._optReference_0.Size = New System.Drawing.Size(49, 17)
		Me._optReference_0.Location = New System.Drawing.Point(8, 44)
		Me._optReference_0.TabIndex = 115
		Me._optReference_0.Name = "_optReference_0"
		_optReference_1.OcxState = CType(resources.GetObject("_optReference_1.OcxState"), System.Windows.Forms.AxHost.State)
		Me._optReference_1.Size = New System.Drawing.Size(49, 17)
		Me._optReference_1.Location = New System.Drawing.Point(8, 28)
		Me._optReference_1.TabIndex = 114
		Me._optReference_1.Name = "_optReference_1"
		fraAvgCount.OcxState = CType(resources.GetObject("fraAvgCount.OcxState"), System.Windows.Forms.AxHost.State)
		Me.fraAvgCount.Size = New System.Drawing.Size(105, 53)
		Me.fraAvgCount.Location = New System.Drawing.Point(8, 212)
		Me.fraAvgCount.TabIndex = 104
		Me.fraAvgCount.Name = "fraAvgCount"
		SSPanel8.OcxState = CType(resources.GetObject("SSPanel8.OcxState"), System.Windows.Forms.AxHost.State)
		Me.SSPanel8.Size = New System.Drawing.Size(89, 19)
		Me.SSPanel8.Location = New System.Drawing.Point(7, 22)
		Me.SSPanel8.TabIndex = 105
		Me.SSPanel8.Name = "SSPanel8"
		Me.txtAvgCount.AutoSize = False
		Me.txtAvgCount.Size = New System.Drawing.Size(64, 17)
		Me.txtAvgCount.Location = New System.Drawing.Point(2, 1)
		Me.txtAvgCount.TabIndex = 106
		Me.txtAvgCount.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtAvgCount.AcceptsReturn = True
		Me.txtAvgCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtAvgCount.BackColor = System.Drawing.SystemColors.Window
		Me.txtAvgCount.CausesValidation = True
		Me.txtAvgCount.Enabled = True
		Me.txtAvgCount.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtAvgCount.HideSelection = True
		Me.txtAvgCount.ReadOnly = False
		Me.txtAvgCount.Maxlength = 0
		Me.txtAvgCount.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtAvgCount.MultiLine = False
		Me.txtAvgCount.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtAvgCount.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtAvgCount.TabStop = True
		Me.txtAvgCount.Visible = True
		Me.txtAvgCount.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.txtAvgCount.Name = "txtAvgCount"
		spnAvgCount.OcxState = CType(resources.GetObject("spnAvgCount.OcxState"), System.Windows.Forms.AxHost.State)
		Me.spnAvgCount.Size = New System.Drawing.Size(21, 17)
		Me.spnAvgCount.Location = New System.Drawing.Point(67, 1)
		Me.spnAvgCount.TabIndex = 107
		Me.spnAvgCount.Name = "spnAvgCount"
		fraSampleType.OcxState = CType(resources.GetObject("fraSampleType.OcxState"), System.Windows.Forms.AxHost.State)
		Me.fraSampleType.Size = New System.Drawing.Size(105, 53)
		Me.fraSampleType.Location = New System.Drawing.Point(8, 160)
		Me.fraSampleType.TabIndex = 98
		Me.fraSampleType.Name = "fraSampleType"
		Me.cboSampleType.BackColor = System.Drawing.Color.White
		Me.cboSampleType.Size = New System.Drawing.Size(91, 21)
		Me.cboSampleType.Location = New System.Drawing.Point(8, 16)
		Me.cboSampleType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cboSampleType.TabIndex = 99
		Me.cboSampleType.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cboSampleType.CausesValidation = True
		Me.cboSampleType.Enabled = True
		Me.cboSampleType.ForeColor = System.Drawing.SystemColors.WindowText
		Me.cboSampleType.IntegralHeight = True
		Me.cboSampleType.Cursor = System.Windows.Forms.Cursors.Default
		Me.cboSampleType.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cboSampleType.Sorted = False
		Me.cboSampleType.TabStop = True
		Me.cboSampleType.Visible = True
		Me.cboSampleType.Name = "cboSampleType"
		fraAcqMode.OcxState = CType(resources.GetObject("fraAcqMode.OcxState"), System.Windows.Forms.AxHost.State)
		Me.fraAcqMode.Size = New System.Drawing.Size(105, 53)
		Me.fraAcqMode.Location = New System.Drawing.Point(8, 108)
		Me.fraAcqMode.TabIndex = 123
		Me.fraAcqMode.Name = "fraAcqMode"
		Me.cboAcqMode.BackColor = System.Drawing.Color.White
		Me.cboAcqMode.Size = New System.Drawing.Size(90, 21)
		Me.cboAcqMode.Location = New System.Drawing.Point(8, 18)
		Me.cboAcqMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cboAcqMode.TabIndex = 124
		Me.cboAcqMode.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cboAcqMode.CausesValidation = True
		Me.cboAcqMode.Enabled = True
		Me.cboAcqMode.ForeColor = System.Drawing.SystemColors.WindowText
		Me.cboAcqMode.IntegralHeight = True
		Me.cboAcqMode.Cursor = System.Windows.Forms.Cursors.Default
		Me.cboAcqMode.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cboAcqMode.Sorted = False
		Me.cboAcqMode.TabStop = True
		Me.cboAcqMode.Visible = True
		Me.cboAcqMode.Name = "cboAcqMode"
		fraDisplay.OcxState = CType(resources.GetObject("fraDisplay.OcxState"), System.Windows.Forms.AxHost.State)
		Me.fraDisplay.Size = New System.Drawing.Size(105, 65)
		Me.fraDisplay.Location = New System.Drawing.Point(8, 44)
		Me.fraDisplay.TabIndex = 108
		Me.fraDisplay.Name = "fraDisplay"
		_chkDisplayTrace_0.OcxState = CType(resources.GetObject("_chkDisplayTrace_0.OcxState"), System.Windows.Forms.AxHost.State)
		Me._chkDisplayTrace_0.Size = New System.Drawing.Size(41, 17)
		Me._chkDisplayTrace_0.Location = New System.Drawing.Point(8, 20)
		Me._chkDisplayTrace_0.TabIndex = 112
		Me._chkDisplayTrace_0.Name = "_chkDisplayTrace_0"
		_chkDisplayTrace_1.OcxState = CType(resources.GetObject("_chkDisplayTrace_1.OcxState"), System.Windows.Forms.AxHost.State)
		Me._chkDisplayTrace_1.Size = New System.Drawing.Size(41, 17)
		Me._chkDisplayTrace_1.Location = New System.Drawing.Point(8, 40)
		Me._chkDisplayTrace_1.TabIndex = 111
		Me._chkDisplayTrace_1.Name = "_chkDisplayTrace_1"
		_chkDisplayTrace_2.OcxState = CType(resources.GetObject("_chkDisplayTrace_2.OcxState"), System.Windows.Forms.AxHost.State)
		Me._chkDisplayTrace_2.Size = New System.Drawing.Size(49, 17)
		Me._chkDisplayTrace_2.Location = New System.Drawing.Point(50, 21)
		Me._chkDisplayTrace_2.TabIndex = 110
		Me._chkDisplayTrace_2.Name = "_chkDisplayTrace_2"
		_chkDisplayTrace_3.OcxState = CType(resources.GetObject("_chkDisplayTrace_3.OcxState"), System.Windows.Forms.AxHost.State)
		Me._chkDisplayTrace_3.Size = New System.Drawing.Size(49, 17)
		Me._chkDisplayTrace_3.Location = New System.Drawing.Point(50, 41)
		Me._chkDisplayTrace_3.TabIndex = 109
		Me._chkDisplayTrace_3.Name = "_chkDisplayTrace_3"
		Me._tabMainFunctions_TabPage1.Text = "Channel 1 "
		Me._cmdSaveToDisk_0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		Me._cmdSaveToDisk_0.Text = "Save to Disk ..."
		Me._cmdSaveToDisk_0.Size = New System.Drawing.Size(100, 23)
		Me._cmdSaveToDisk_0.Location = New System.Drawing.Point(188, 244)
		Me._cmdSaveToDisk_0.TabIndex = 132
		Me._cmdSaveToDisk_0.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me._cmdSaveToDisk_0.BackColor = System.Drawing.SystemColors.Control
		Me._cmdSaveToDisk_0.CausesValidation = True
		Me._cmdSaveToDisk_0.Enabled = True
		Me._cmdSaveToDisk_0.ForeColor = System.Drawing.SystemColors.ControlText
		Me._cmdSaveToDisk_0.Cursor = System.Windows.Forms.Cursors.Default
		Me._cmdSaveToDisk_0.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me._cmdSaveToDisk_0.TabStop = True
		Me._cmdSaveToDisk_0.Name = "_cmdSaveToDisk_0"
		Me._cmdSaveToMem_0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		Me._cmdSaveToMem_0.Text = "Save to Memory"
		Me._cmdSaveToMem_0.Size = New System.Drawing.Size(100, 23)
		Me._cmdSaveToMem_0.Location = New System.Drawing.Point(188, 216)
		Me._cmdSaveToMem_0.TabIndex = 133
		Me._cmdSaveToMem_0.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me._cmdSaveToMem_0.BackColor = System.Drawing.SystemColors.Control
		Me._cmdSaveToMem_0.CausesValidation = True
		Me._cmdSaveToMem_0.Enabled = True
		Me._cmdSaveToMem_0.ForeColor = System.Drawing.SystemColors.ControlText
		Me._cmdSaveToMem_0.Cursor = System.Windows.Forms.Cursors.Default
		Me._cmdSaveToMem_0.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me._cmdSaveToMem_0.TabStop = True
		Me._cmdSaveToMem_0.Name = "_cmdSaveToMem_0"
		_fraSignalFilters_0.OcxState = CType(resources.GetObject("_fraSignalFilters_0.OcxState"), System.Windows.Forms.AxHost.State)
		Me._fraSignalFilters_0.Size = New System.Drawing.Size(145, 69)
		Me._fraSignalFilters_0.Location = New System.Drawing.Point(8, 196)
		Me._fraSignalFilters_0.TabIndex = 138
		Me._fraSignalFilters_0.Name = "_fraSignalFilters_0"
		chkLFReject1.OcxState = CType(resources.GetObject("chkLFReject1.OcxState"), System.Windows.Forms.AxHost.State)
		Me.chkLFReject1.Size = New System.Drawing.Size(121, 17)
		Me.chkLFReject1.Location = New System.Drawing.Point(9, 24)
		Me.chkLFReject1.TabIndex = 140
		Me.chkLFReject1.Name = "chkLFReject1"
		chkHFReject1.OcxState = CType(resources.GetObject("chkHFReject1.OcxState"), System.Windows.Forms.AxHost.State)
		Me.chkHFReject1.Size = New System.Drawing.Size(117, 17)
		Me.chkHFReject1.Location = New System.Drawing.Point(9, 40)
		Me.chkHFReject1.TabIndex = 139
		Me.chkHFReject1.Name = "chkHFReject1"
		_fraProbeAttenuation_0.OcxState = CType(resources.GetObject("_fraProbeAttenuation_0.OcxState"), System.Windows.Forms.AxHost.State)
		Me._fraProbeAttenuation_0.Size = New System.Drawing.Size(145, 105)
		Me._fraProbeAttenuation_0.Location = New System.Drawing.Point(8, 92)
		Me._fraProbeAttenuation_0.TabIndex = 141
		Me._fraProbeAttenuation_0.Name = "_fraProbeAttenuation_0"
		_optProbe1_3.OcxState = CType(resources.GetObject("_optProbe1_3.OcxState"), System.Windows.Forms.AxHost.State)
		Me._optProbe1_3.Size = New System.Drawing.Size(49, 17)
		Me._optProbe1_3.Location = New System.Drawing.Point(8, 72)
		Me._optProbe1_3.TabIndex = 145
		Me._optProbe1_3.Name = "_optProbe1_3"
		_optProbe1_2.OcxState = CType(resources.GetObject("_optProbe1_2.OcxState"), System.Windows.Forms.AxHost.State)
		Me._optProbe1_2.Size = New System.Drawing.Size(49, 17)
		Me._optProbe1_2.Location = New System.Drawing.Point(8, 56)
		Me._optProbe1_2.TabIndex = 144
		Me._optProbe1_2.Name = "_optProbe1_2"
		_optProbe1_1.OcxState = CType(resources.GetObject("_optProbe1_1.OcxState"), System.Windows.Forms.AxHost.State)
		Me._optProbe1_1.Size = New System.Drawing.Size(49, 17)
		Me._optProbe1_1.Location = New System.Drawing.Point(8, 40)
		Me._optProbe1_1.TabIndex = 143
		Me._optProbe1_1.Name = "_optProbe1_1"
		_optProbe1_0.OcxState = CType(resources.GetObject("_optProbe1_0.OcxState"), System.Windows.Forms.AxHost.State)
		Me._optProbe1_0.Size = New System.Drawing.Size(49, 17)
		Me._optProbe1_0.Location = New System.Drawing.Point(8, 24)
		Me._optProbe1_0.TabIndex = 142
		Me._optProbe1_0.Name = "_optProbe1_0"
		_fraVerticalRange_0.OcxState = CType(resources.GetObject("_fraVerticalRange_0.OcxState"), System.Windows.Forms.AxHost.State)
		Me._fraVerticalRange_0.Size = New System.Drawing.Size(145, 49)
		Me._fraVerticalRange_0.Location = New System.Drawing.Point(8, 44)
		Me._fraVerticalRange_0.TabIndex = 146
		Me._fraVerticalRange_0.Name = "_fraVerticalRange_0"
		Me._cboVertRange_0.BackColor = System.Drawing.Color.White
		Me._cboVertRange_0.Size = New System.Drawing.Size(124, 21)
		Me._cboVertRange_0.Location = New System.Drawing.Point(9, 19)
		Me._cboVertRange_0.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me._cboVertRange_0.TabIndex = 147
		Me._cboVertRange_0.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me._cboVertRange_0.CausesValidation = True
		Me._cboVertRange_0.Enabled = True
		Me._cboVertRange_0.ForeColor = System.Drawing.SystemColors.WindowText
		Me._cboVertRange_0.IntegralHeight = True
		Me._cboVertRange_0.Cursor = System.Windows.Forms.Cursors.Default
		Me._cboVertRange_0.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me._cboVertRange_0.Sorted = False
		Me._cboVertRange_0.TabStop = True
		Me._cboVertRange_0.Visible = True
		Me._cboVertRange_0.Name = "_cboVertRange_0"
		_fraCoupling_0.OcxState = CType(resources.GetObject("_fraCoupling_0.OcxState"), System.Windows.Forms.AxHost.State)
		Me._fraCoupling_0.Size = New System.Drawing.Size(133, 105)
		Me._fraCoupling_0.Location = New System.Drawing.Point(156, 92)
		Me._fraCoupling_0.TabIndex = 148
		Me._fraCoupling_0.Name = "_fraCoupling_0"
		_optCoupling1_1.OcxState = CType(resources.GetObject("_optCoupling1_1.OcxState"), System.Windows.Forms.AxHost.State)
		Me._optCoupling1_1.Size = New System.Drawing.Size(89, 17)
		Me._optCoupling1_1.Location = New System.Drawing.Point(8, 40)
		Me._optCoupling1_1.TabIndex = 151
		Me._optCoupling1_1.Name = "_optCoupling1_1"
		_optCoupling1_0.OcxState = CType(resources.GetObject("_optCoupling1_0.OcxState"), System.Windows.Forms.AxHost.State)
		Me._optCoupling1_0.Size = New System.Drawing.Size(97, 17)
		Me._optCoupling1_0.Location = New System.Drawing.Point(8, 24)
		Me._optCoupling1_0.TabIndex = 150
		Me._optCoupling1_0.Name = "_optCoupling1_0"
		_optCoupling1_2.OcxState = CType(resources.GetObject("_optCoupling1_2.OcxState"), System.Windows.Forms.AxHost.State)
		Me._optCoupling1_2.Size = New System.Drawing.Size(89, 17)
		Me._optCoupling1_2.Location = New System.Drawing.Point(8, 56)
		Me._optCoupling1_2.TabIndex = 149
		Me._optCoupling1_2.Name = "_optCoupling1_2"
		fraOffset1.OcxState = CType(resources.GetObject("fraOffset1.OcxState"), System.Windows.Forms.AxHost.State)
		Me.fraOffset1.Size = New System.Drawing.Size(133, 49)
		Me.fraOffset1.Location = New System.Drawing.Point(156, 44)
		Me.fraOffset1.TabIndex = 134
		Me.fraOffset1.Name = "fraOffset1"
		SSPanel10.OcxState = CType(resources.GetObject("SSPanel10.OcxState"), System.Windows.Forms.AxHost.State)
		Me.SSPanel10.Size = New System.Drawing.Size(104, 19)
		Me.SSPanel10.Location = New System.Drawing.Point(9, 19)
		Me.SSPanel10.TabIndex = 135
		Me.SSPanel10.Name = "SSPanel10"
		Me.txtVOffset1.AutoSize = False
		Me.txtVOffset1.Size = New System.Drawing.Size(79, 17)
		Me.txtVOffset1.Location = New System.Drawing.Point(2, 1)
		Me.txtVOffset1.TabIndex = 136
		Me.txtVOffset1.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtVOffset1.AcceptsReturn = True
		Me.txtVOffset1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtVOffset1.BackColor = System.Drawing.SystemColors.Window
		Me.txtVOffset1.CausesValidation = True
		Me.txtVOffset1.Enabled = True
		Me.txtVOffset1.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtVOffset1.HideSelection = True
		Me.txtVOffset1.ReadOnly = False
		Me.txtVOffset1.Maxlength = 0
		Me.txtVOffset1.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtVOffset1.MultiLine = False
		Me.txtVOffset1.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtVOffset1.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtVOffset1.TabStop = True
		Me.txtVOffset1.Visible = True
		Me.txtVOffset1.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.txtVOffset1.Name = "txtVOffset1"
		spnVOffset1.OcxState = CType(resources.GetObject("spnVOffset1.OcxState"), System.Windows.Forms.AxHost.State)
		Me.spnVOffset1.Size = New System.Drawing.Size(21, 17)
		Me.spnVOffset1.Location = New System.Drawing.Point(82, 1)
		Me.spnVOffset1.TabIndex = 137
		Me.spnVOffset1.Name = "spnVOffset1"
		Me._tabMainFunctions_TabPage2.Text = "Channel 2 "
		fraOffset2.OcxState = CType(resources.GetObject("fraOffset2.OcxState"), System.Windows.Forms.AxHost.State)
		Me.fraOffset2.Size = New System.Drawing.Size(133, 49)
		Me.fraOffset2.Location = New System.Drawing.Point(156, 44)
		Me.fraOffset2.TabIndex = 154
		Me.fraOffset2.Name = "fraOffset2"
		SSPanel11.OcxState = CType(resources.GetObject("SSPanel11.OcxState"), System.Windows.Forms.AxHost.State)
		Me.SSPanel11.Size = New System.Drawing.Size(104, 19)
		Me.SSPanel11.Location = New System.Drawing.Point(9, 19)
		Me.SSPanel11.TabIndex = 155
		Me.SSPanel11.Name = "SSPanel11"
		Me.txtVOffset2.AutoSize = False
		Me.txtVOffset2.Size = New System.Drawing.Size(79, 17)
		Me.txtVOffset2.Location = New System.Drawing.Point(2, 1)
		Me.txtVOffset2.TabIndex = 156
		Me.txtVOffset2.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtVOffset2.AcceptsReturn = True
		Me.txtVOffset2.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtVOffset2.BackColor = System.Drawing.SystemColors.Window
		Me.txtVOffset2.CausesValidation = True
		Me.txtVOffset2.Enabled = True
		Me.txtVOffset2.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtVOffset2.HideSelection = True
		Me.txtVOffset2.ReadOnly = False
		Me.txtVOffset2.Maxlength = 0
		Me.txtVOffset2.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtVOffset2.MultiLine = False
		Me.txtVOffset2.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtVOffset2.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtVOffset2.TabStop = True
		Me.txtVOffset2.Visible = True
		Me.txtVOffset2.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.txtVOffset2.Name = "txtVOffset2"
		spnVOffset2.OcxState = CType(resources.GetObject("spnVOffset2.OcxState"), System.Windows.Forms.AxHost.State)
		Me.spnVOffset2.Size = New System.Drawing.Size(21, 17)
		Me.spnVOffset2.Location = New System.Drawing.Point(82, 1)
		Me.spnVOffset2.TabIndex = 157
		Me.spnVOffset2.Name = "spnVOffset2"
		_fraCoupling_1.OcxState = CType(resources.GetObject("_fraCoupling_1.OcxState"), System.Windows.Forms.AxHost.State)
		Me._fraCoupling_1.Size = New System.Drawing.Size(133, 105)
		Me._fraCoupling_1.Location = New System.Drawing.Point(156, 92)
		Me._fraCoupling_1.TabIndex = 163
		Me._fraCoupling_1.Name = "_fraCoupling_1"
		_optCoupling2_2.OcxState = CType(resources.GetObject("_optCoupling2_2.OcxState"), System.Windows.Forms.AxHost.State)
		Me._optCoupling2_2.Size = New System.Drawing.Size(89, 17)
		Me._optCoupling2_2.Location = New System.Drawing.Point(8, 56)
		Me._optCoupling2_2.TabIndex = 166
		Me._optCoupling2_2.Name = "_optCoupling2_2"
		_optCoupling2_0.OcxState = CType(resources.GetObject("_optCoupling2_0.OcxState"), System.Windows.Forms.AxHost.State)
		Me._optCoupling2_0.Size = New System.Drawing.Size(89, 17)
		Me._optCoupling2_0.Location = New System.Drawing.Point(8, 24)
		Me._optCoupling2_0.TabIndex = 165
		Me._optCoupling2_0.Name = "_optCoupling2_0"
		_optCoupling2_1.OcxState = CType(resources.GetObject("_optCoupling2_1.OcxState"), System.Windows.Forms.AxHost.State)
		Me._optCoupling2_1.Size = New System.Drawing.Size(89, 17)
		Me._optCoupling2_1.Location = New System.Drawing.Point(8, 40)
		Me._optCoupling2_1.TabIndex = 164
		Me._optCoupling2_1.Name = "_optCoupling2_1"
		_fraVerticalRange_1.OcxState = CType(resources.GetObject("_fraVerticalRange_1.OcxState"), System.Windows.Forms.AxHost.State)
		Me._fraVerticalRange_1.Size = New System.Drawing.Size(145, 49)
		Me._fraVerticalRange_1.Location = New System.Drawing.Point(8, 44)
		Me._fraVerticalRange_1.TabIndex = 161
		Me._fraVerticalRange_1.Name = "_fraVerticalRange_1"
		Me._cboVertRange_1.BackColor = System.Drawing.Color.White
		Me._cboVertRange_1.Size = New System.Drawing.Size(124, 21)
		Me._cboVertRange_1.Location = New System.Drawing.Point(9, 19)
		Me._cboVertRange_1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me._cboVertRange_1.TabIndex = 162
		Me._cboVertRange_1.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me._cboVertRange_1.CausesValidation = True
		Me._cboVertRange_1.Enabled = True
		Me._cboVertRange_1.ForeColor = System.Drawing.SystemColors.WindowText
		Me._cboVertRange_1.IntegralHeight = True
		Me._cboVertRange_1.Cursor = System.Windows.Forms.Cursors.Default
		Me._cboVertRange_1.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me._cboVertRange_1.Sorted = False
		Me._cboVertRange_1.TabStop = True
		Me._cboVertRange_1.Visible = True
		Me._cboVertRange_1.Name = "_cboVertRange_1"
		_fraProbeAttenuation_1.OcxState = CType(resources.GetObject("_fraProbeAttenuation_1.OcxState"), System.Windows.Forms.AxHost.State)
		Me._fraProbeAttenuation_1.Size = New System.Drawing.Size(145, 105)
		Me._fraProbeAttenuation_1.Location = New System.Drawing.Point(8, 92)
		Me._fraProbeAttenuation_1.TabIndex = 167
		Me._fraProbeAttenuation_1.Name = "_fraProbeAttenuation_1"
		_optProbe2_0.OcxState = CType(resources.GetObject("_optProbe2_0.OcxState"), System.Windows.Forms.AxHost.State)
		Me._optProbe2_0.Size = New System.Drawing.Size(49, 17)
		Me._optProbe2_0.Location = New System.Drawing.Point(8, 24)
		Me._optProbe2_0.TabIndex = 171
		Me._optProbe2_0.Name = "_optProbe2_0"
		_optProbe2_1.OcxState = CType(resources.GetObject("_optProbe2_1.OcxState"), System.Windows.Forms.AxHost.State)
		Me._optProbe2_1.Size = New System.Drawing.Size(49, 17)
		Me._optProbe2_1.Location = New System.Drawing.Point(8, 40)
		Me._optProbe2_1.TabIndex = 170
		Me._optProbe2_1.Name = "_optProbe2_1"
		_optProbe2_2.OcxState = CType(resources.GetObject("_optProbe2_2.OcxState"), System.Windows.Forms.AxHost.State)
		Me._optProbe2_2.Size = New System.Drawing.Size(49, 17)
		Me._optProbe2_2.Location = New System.Drawing.Point(8, 56)
		Me._optProbe2_2.TabIndex = 169
		Me._optProbe2_2.Name = "_optProbe2_2"
		_optProbe2_3.OcxState = CType(resources.GetObject("_optProbe2_3.OcxState"), System.Windows.Forms.AxHost.State)
		Me._optProbe2_3.Size = New System.Drawing.Size(49, 17)
		Me._optProbe2_3.Location = New System.Drawing.Point(8, 72)
		Me._optProbe2_3.TabIndex = 168
		Me._optProbe2_3.Name = "_optProbe2_3"
		_fraSignalFilters_1.OcxState = CType(resources.GetObject("_fraSignalFilters_1.OcxState"), System.Windows.Forms.AxHost.State)
		Me._fraSignalFilters_1.Size = New System.Drawing.Size(145, 69)
		Me._fraSignalFilters_1.Location = New System.Drawing.Point(8, 196)
		Me._fraSignalFilters_1.TabIndex = 158
		Me._fraSignalFilters_1.Name = "_fraSignalFilters_1"
		chkHFReject2.OcxState = CType(resources.GetObject("chkHFReject2.OcxState"), System.Windows.Forms.AxHost.State)
		Me.chkHFReject2.Size = New System.Drawing.Size(121, 17)
		Me.chkHFReject2.Location = New System.Drawing.Point(9, 40)
		Me.chkHFReject2.TabIndex = 160
		Me.chkHFReject2.Name = "chkHFReject2"
		chkLFReject2.OcxState = CType(resources.GetObject("chkLFReject2.OcxState"), System.Windows.Forms.AxHost.State)
		Me.chkLFReject2.Size = New System.Drawing.Size(121, 17)
		Me.chkLFReject2.Location = New System.Drawing.Point(9, 24)
		Me.chkLFReject2.TabIndex = 159
		Me.chkLFReject2.Name = "chkLFReject2"
		Me._cmdSaveToMem_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		Me._cmdSaveToMem_1.Text = "Save to Memory"
		Me._cmdSaveToMem_1.Size = New System.Drawing.Size(100, 23)
		Me._cmdSaveToMem_1.Location = New System.Drawing.Point(188, 216)
		Me._cmdSaveToMem_1.TabIndex = 153
		Me._cmdSaveToMem_1.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me._cmdSaveToMem_1.BackColor = System.Drawing.SystemColors.Control
		Me._cmdSaveToMem_1.CausesValidation = True
		Me._cmdSaveToMem_1.Enabled = True
		Me._cmdSaveToMem_1.ForeColor = System.Drawing.SystemColors.ControlText
		Me._cmdSaveToMem_1.Cursor = System.Windows.Forms.Cursors.Default
		Me._cmdSaveToMem_1.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me._cmdSaveToMem_1.TabStop = True
		Me._cmdSaveToMem_1.Name = "_cmdSaveToMem_1"
		Me._cmdSaveToDisk_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		Me._cmdSaveToDisk_1.Text = "Save to Disk ..."
		Me._cmdSaveToDisk_1.Size = New System.Drawing.Size(100, 23)
		Me._cmdSaveToDisk_1.Location = New System.Drawing.Point(188, 244)
		Me._cmdSaveToDisk_1.TabIndex = 152
		Me._cmdSaveToDisk_1.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me._cmdSaveToDisk_1.BackColor = System.Drawing.SystemColors.Control
		Me._cmdSaveToDisk_1.CausesValidation = True
		Me._cmdSaveToDisk_1.Enabled = True
		Me._cmdSaveToDisk_1.ForeColor = System.Drawing.SystemColors.ControlText
		Me._cmdSaveToDisk_1.Cursor = System.Windows.Forms.Cursors.Default
		Me._cmdSaveToDisk_1.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me._cmdSaveToDisk_1.TabStop = True
		Me._cmdSaveToDisk_1.Name = "_cmdSaveToDisk_1"
		Me._tabMainFunctions_TabPage3.Text = "Measure "
		_fraFunction_2.OcxState = CType(resources.GetObject("_fraFunction_2.OcxState"), System.Windows.Forms.AxHost.State)
		Me._fraFunction_2.Size = New System.Drawing.Size(292, 193)
		Me._fraFunction_2.Location = New System.Drawing.Point(8, 44)
		Me._fraFunction_2.TabIndex = 173
		Me._fraFunction_2.Name = "_fraFunction_2"
		tbrMeasFunction.OcxState = CType(resources.GetObject("tbrMeasFunction.OcxState"), System.Windows.Forms.AxHost.State)
		Me.tbrMeasFunction.Size = New System.Drawing.Size(127, 34)
		Me.tbrMeasFunction.Location = New System.Drawing.Point(8, 24)
		Me.tbrMeasFunction.TabIndex = 174
		Me.tbrMeasFunction.Name = "tbrMeasFunction"
		Me.cboMeasSour.BackColor = System.Drawing.Color.White
		Me.cboMeasSour.Size = New System.Drawing.Size(134, 21)
		Me.cboMeasSour.Location = New System.Drawing.Point(140, 28)
		Me.cboMeasSour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cboMeasSour.TabIndex = 209
		Me.cboMeasSour.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cboMeasSour.CausesValidation = True
		Me.cboMeasSour.Enabled = True
		Me.cboMeasSour.ForeColor = System.Drawing.SystemColors.WindowText
		Me.cboMeasSour.IntegralHeight = True
		Me.cboMeasSour.Cursor = System.Windows.Forms.Cursors.Default
		Me.cboMeasSour.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cboMeasSour.Sorted = False
		Me.cboMeasSour.TabStop = True
		Me.cboMeasSour.Visible = True
		Me.cboMeasSour.Name = "cboMeasSour"
		fraDelayParam.OcxState = CType(resources.GetObject("fraDelayParam.OcxState"), System.Windows.Forms.AxHost.State)
		Me.fraDelayParam.Size = New System.Drawing.Size(141, 177)
		Me.fraDelayParam.Location = New System.Drawing.Point(136, 8)
		Me.fraDelayParam.TabIndex = 175
		Me.fraDelayParam.Visible = False
		Me.fraDelayParam.Name = "fraDelayParam"
		Me.cboStopSource.BackColor = System.Drawing.Color.White
		Me.cboStopSource.ForeColor = System.Drawing.Color.Black
		Me.cboStopSource.Size = New System.Drawing.Size(62, 21)
		Me.cboStopSource.Location = New System.Drawing.Point(74, 27)
		Me.cboStopSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cboStopSource.TabIndex = 189
		Me.cboStopSource.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cboStopSource.CausesValidation = True
		Me.cboStopSource.Enabled = True
		Me.cboStopSource.IntegralHeight = True
		Me.cboStopSource.Cursor = System.Windows.Forms.Cursors.Default
		Me.cboStopSource.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cboStopSource.Sorted = False
		Me.cboStopSource.TabStop = True
		Me.cboStopSource.Visible = True
		Me.cboStopSource.Name = "cboStopSource"
		Me.cboStartSource.BackColor = System.Drawing.Color.White
		Me.cboStartSource.ForeColor = System.Drawing.Color.Black
		Me.cboStartSource.Size = New System.Drawing.Size(62, 21)
		Me.cboStartSource.Location = New System.Drawing.Point(0, 27)
		Me.cboStartSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cboStartSource.TabIndex = 188
		Me.cboStartSource.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cboStartSource.CausesValidation = True
		Me.cboStartSource.Enabled = True
		Me.cboStartSource.IntegralHeight = True
		Me.cboStartSource.Cursor = System.Windows.Forms.Cursors.Default
		Me.cboStartSource.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cboStartSource.Sorted = False
		Me.cboStartSource.TabStop = True
		Me.cboStartSource.Visible = True
		Me.cboStartSource.Name = "cboStartSource"
		Me.Picture2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Picture2.ForeColor = System.Drawing.SystemColors.WindowText
		Me.Picture2.Size = New System.Drawing.Size(59, 32)
		Me.Picture2.Location = New System.Drawing.Point(0, 52)
		Me.Picture2.TabIndex = 184
		Me.Picture2.Dock = System.Windows.Forms.DockStyle.None
		Me.Picture2.BackColor = System.Drawing.SystemColors.Control
		Me.Picture2.CausesValidation = True
		Me.Picture2.Enabled = True
		Me.Picture2.Cursor = System.Windows.Forms.Cursors.Default
		Me.Picture2.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Picture2.TabStop = True
		Me.Picture2.Visible = True
		Me.Picture2.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Picture2.Name = "Picture2"
		_optStartSlope_0.OcxState = CType(resources.GetObject("_optStartSlope_0.OcxState"), System.Windows.Forms.AxHost.State)
		Me._optStartSlope_0.Size = New System.Drawing.Size(28, 17)
		Me._optStartSlope_0.Location = New System.Drawing.Point(0, 12)
		Me._optStartSlope_0.TabIndex = 187
		Me._optStartSlope_0.Name = "_optStartSlope_0"
		_optStartSlope_1.OcxState = CType(resources.GetObject("_optStartSlope_1.OcxState"), System.Windows.Forms.AxHost.State)
		Me._optStartSlope_1.Size = New System.Drawing.Size(24, 17)
		Me._optStartSlope_1.Location = New System.Drawing.Point(33, 12)
		Me._optStartSlope_1.TabIndex = 186
		Me._optStartSlope_1.Name = "_optStartSlope_1"
		Me.lblStartSlope.Text = "Slope"
		Me.lblStartSlope.Size = New System.Drawing.Size(46, 14)
		Me.lblStartSlope.Location = New System.Drawing.Point(0, 0)
		Me.lblStartSlope.TabIndex = 185
		Me.lblStartSlope.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblStartSlope.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.lblStartSlope.BackColor = System.Drawing.SystemColors.Control
		Me.lblStartSlope.Enabled = True
		Me.lblStartSlope.ForeColor = System.Drawing.SystemColors.ControlText
		Me.lblStartSlope.Cursor = System.Windows.Forms.Cursors.Default
		Me.lblStartSlope.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.lblStartSlope.UseMnemonic = True
		Me.lblStartSlope.Visible = True
		Me.lblStartSlope.AutoSize = False
		Me.lblStartSlope.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.lblStartSlope.Name = "lblStartSlope"
		Me.Picture3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Picture3.ForeColor = System.Drawing.SystemColors.WindowText
		Me.Picture3.Size = New System.Drawing.Size(58, 33)
		Me.Picture3.Location = New System.Drawing.Point(76, 52)
		Me.Picture3.TabIndex = 180
		Me.Picture3.Dock = System.Windows.Forms.DockStyle.None
		Me.Picture3.BackColor = System.Drawing.SystemColors.Control
		Me.Picture3.CausesValidation = True
		Me.Picture3.Enabled = True
		Me.Picture3.Cursor = System.Windows.Forms.Cursors.Default
		Me.Picture3.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Picture3.TabStop = True
		Me.Picture3.Visible = True
		Me.Picture3.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Picture3.Name = "Picture3"
		_optStopSlope_0.OcxState = CType(resources.GetObject("_optStopSlope_0.OcxState"), System.Windows.Forms.AxHost.State)
		Me._optStopSlope_0.Size = New System.Drawing.Size(28, 17)
		Me._optStopSlope_0.Location = New System.Drawing.Point(0, 12)
		Me._optStopSlope_0.TabIndex = 183
		Me._optStopSlope_0.Name = "_optStopSlope_0"
		_optStopSlope_1.OcxState = CType(resources.GetObject("_optStopSlope_1.OcxState"), System.Windows.Forms.AxHost.State)
		Me._optStopSlope_1.Size = New System.Drawing.Size(24, 17)
		Me._optStopSlope_1.Location = New System.Drawing.Point(33, 12)
		Me._optStopSlope_1.TabIndex = 182
		Me._optStopSlope_1.Name = "_optStopSlope_1"
		Me.lblStopSlope.Text = "Slope"
		Me.lblStopSlope.Size = New System.Drawing.Size(46, 14)
		Me.lblStopSlope.Location = New System.Drawing.Point(0, 0)
		Me.lblStopSlope.TabIndex = 181
		Me.lblStopSlope.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblStopSlope.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.lblStopSlope.BackColor = System.Drawing.SystemColors.Control
		Me.lblStopSlope.Enabled = True
		Me.lblStopSlope.ForeColor = System.Drawing.SystemColors.ControlText
		Me.lblStopSlope.Cursor = System.Windows.Forms.Cursors.Default
		Me.lblStopSlope.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.lblStopSlope.UseMnemonic = True
		Me.lblStopSlope.Visible = True
		Me.lblStopSlope.AutoSize = False
		Me.lblStopSlope.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.lblStopSlope.Name = "lblStopSlope"
		Me.Picture4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Picture4.ForeColor = System.Drawing.SystemColors.WindowText
		Me.Picture4.Size = New System.Drawing.Size(63, 30)
		Me.Picture4.Location = New System.Drawing.Point(0, 120)
		Me.Picture4.TabIndex = 176
		Me.Picture4.Dock = System.Windows.Forms.DockStyle.None
		Me.Picture4.BackColor = System.Drawing.SystemColors.Control
		Me.Picture4.CausesValidation = True
		Me.Picture4.Enabled = True
		Me.Picture4.Cursor = System.Windows.Forms.Cursors.Default
		Me.Picture4.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Picture4.TabStop = True
		Me.Picture4.Visible = True
		Me.Picture4.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Picture4.Name = "Picture4"
		_optStartLevel_0.OcxState = CType(resources.GetObject("_optStartLevel_0.OcxState"), System.Windows.Forms.AxHost.State)
		Me._optStartLevel_0.Size = New System.Drawing.Size(28, 17)
		Me._optStartLevel_0.Location = New System.Drawing.Point(0, 12)
		Me._optStartLevel_0.TabIndex = 178
		Me._optStartLevel_0.Name = "_optStartLevel_0"
		_optStartLevel_1.OcxState = CType(resources.GetObject("_optStartLevel_1.OcxState"), System.Windows.Forms.AxHost.State)
		Me._optStartLevel_1.Size = New System.Drawing.Size(25, 17)
		Me._optStartLevel_1.Location = New System.Drawing.Point(33, 12)
		Me._optStartLevel_1.TabIndex = 177
		Me._optStartLevel_1.Name = "_optStartLevel_1"
		Me.lblStartLevelCap.Text = "Level"
		Me.lblStartLevelCap.Size = New System.Drawing.Size(46, 14)
		Me.lblStartLevelCap.Location = New System.Drawing.Point(0, 0)
		Me.lblStartLevelCap.TabIndex = 179
		Me.lblStartLevelCap.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblStartLevelCap.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.lblStartLevelCap.BackColor = System.Drawing.SystemColors.Control
		Me.lblStartLevelCap.Enabled = True
		Me.lblStartLevelCap.ForeColor = System.Drawing.SystemColors.ControlText
		Me.lblStartLevelCap.Cursor = System.Windows.Forms.Cursors.Default
		Me.lblStartLevelCap.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.lblStartLevelCap.UseMnemonic = True
		Me.lblStartLevelCap.Visible = True
		Me.lblStartLevelCap.AutoSize = False
		Me.lblStartLevelCap.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.lblStartLevelCap.Name = "lblStartLevelCap"
		SSPanel1.OcxState = CType(resources.GetObject("SSPanel1.OcxState"), System.Windows.Forms.AxHost.State)
		Me.SSPanel1.Size = New System.Drawing.Size(61, 19)
		Me.SSPanel1.Location = New System.Drawing.Point(0, 98)
		Me.SSPanel1.TabIndex = 190
		Me.SSPanel1.Name = "SSPanel1"
		Me.txtStartEdge.AutoSize = False
		Me.txtStartEdge.Size = New System.Drawing.Size(35, 16)
		Me.txtStartEdge.Location = New System.Drawing.Point(2, 1)
		Me.txtStartEdge.TabIndex = 191
		Me.txtStartEdge.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtStartEdge.AcceptsReturn = True
		Me.txtStartEdge.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtStartEdge.BackColor = System.Drawing.SystemColors.Window
		Me.txtStartEdge.CausesValidation = True
		Me.txtStartEdge.Enabled = True
		Me.txtStartEdge.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtStartEdge.HideSelection = True
		Me.txtStartEdge.ReadOnly = False
		Me.txtStartEdge.Maxlength = 0
		Me.txtStartEdge.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtStartEdge.MultiLine = False
		Me.txtStartEdge.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtStartEdge.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtStartEdge.TabStop = True
		Me.txtStartEdge.Visible = True
		Me.txtStartEdge.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.txtStartEdge.Name = "txtStartEdge"
		spnStartEdge.OcxState = CType(resources.GetObject("spnStartEdge.OcxState"), System.Windows.Forms.AxHost.State)
		Me.spnStartEdge.Size = New System.Drawing.Size(21, 17)
		Me.spnStartEdge.Location = New System.Drawing.Point(39, 1)
		Me.spnStartEdge.TabIndex = 192
		Me.spnStartEdge.Name = "spnStartEdge"
		SSPanel3.OcxState = CType(resources.GetObject("SSPanel3.OcxState"), System.Windows.Forms.AxHost.State)
		Me.SSPanel3.Size = New System.Drawing.Size(61, 19)
		Me.SSPanel3.Location = New System.Drawing.Point(0, 154)
		Me.SSPanel3.TabIndex = 193
		Me.SSPanel3.Name = "SSPanel3"
		Me.txtStartLevel.AutoSize = False
		Me.txtStartLevel.Size = New System.Drawing.Size(35, 17)
		Me.txtStartLevel.Location = New System.Drawing.Point(2, 1)
		Me.txtStartLevel.TabIndex = 194
		Me.txtStartLevel.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtStartLevel.AcceptsReturn = True
		Me.txtStartLevel.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtStartLevel.BackColor = System.Drawing.SystemColors.Window
		Me.txtStartLevel.CausesValidation = True
		Me.txtStartLevel.Enabled = True
		Me.txtStartLevel.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtStartLevel.HideSelection = True
		Me.txtStartLevel.ReadOnly = False
		Me.txtStartLevel.Maxlength = 0
		Me.txtStartLevel.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtStartLevel.MultiLine = False
		Me.txtStartLevel.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtStartLevel.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtStartLevel.TabStop = True
		Me.txtStartLevel.Visible = True
		Me.txtStartLevel.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.txtStartLevel.Name = "txtStartLevel"
		spnStartLevel.OcxState = CType(resources.GetObject("spnStartLevel.OcxState"), System.Windows.Forms.AxHost.State)
		Me.spnStartLevel.Size = New System.Drawing.Size(21, 17)
		Me.spnStartLevel.Location = New System.Drawing.Point(39, 1)
		Me.spnStartLevel.TabIndex = 195
		Me.spnStartLevel.Name = "spnStartLevel"
		SSPanel4.OcxState = CType(resources.GetObject("SSPanel4.OcxState"), System.Windows.Forms.AxHost.State)
		Me.SSPanel4.Size = New System.Drawing.Size(61, 19)
		Me.SSPanel4.Location = New System.Drawing.Point(74, 98)
		Me.SSPanel4.TabIndex = 196
		Me.SSPanel4.Name = "SSPanel4"
		Me.txtStopEdge.AutoSize = False
		Me.txtStopEdge.Size = New System.Drawing.Size(35, 17)
		Me.txtStopEdge.Location = New System.Drawing.Point(2, 1)
		Me.txtStopEdge.TabIndex = 197
		Me.txtStopEdge.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtStopEdge.AcceptsReturn = True
		Me.txtStopEdge.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtStopEdge.BackColor = System.Drawing.SystemColors.Window
		Me.txtStopEdge.CausesValidation = True
		Me.txtStopEdge.Enabled = True
		Me.txtStopEdge.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtStopEdge.HideSelection = True
		Me.txtStopEdge.ReadOnly = False
		Me.txtStopEdge.Maxlength = 0
		Me.txtStopEdge.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtStopEdge.MultiLine = False
		Me.txtStopEdge.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtStopEdge.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtStopEdge.TabStop = True
		Me.txtStopEdge.Visible = True
		Me.txtStopEdge.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.txtStopEdge.Name = "txtStopEdge"
		spnStopEdge.OcxState = CType(resources.GetObject("spnStopEdge.OcxState"), System.Windows.Forms.AxHost.State)
		Me.spnStopEdge.Size = New System.Drawing.Size(21, 17)
		Me.spnStopEdge.Location = New System.Drawing.Point(39, 1)
		Me.spnStopEdge.TabIndex = 198
		Me.spnStopEdge.Name = "spnStopEdge"
		SSPanel5.OcxState = CType(resources.GetObject("SSPanel5.OcxState"), System.Windows.Forms.AxHost.State)
		Me.SSPanel5.Size = New System.Drawing.Size(61, 19)
		Me.SSPanel5.Location = New System.Drawing.Point(74, 154)
		Me.SSPanel5.TabIndex = 199
		Me.SSPanel5.Name = "SSPanel5"
		Me.txtStopLevel.AutoSize = False
		Me.txtStopLevel.Size = New System.Drawing.Size(35, 17)
		Me.txtStopLevel.Location = New System.Drawing.Point(2, 1)
		Me.txtStopLevel.TabIndex = 200
		Me.txtStopLevel.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtStopLevel.AcceptsReturn = True
		Me.txtStopLevel.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtStopLevel.BackColor = System.Drawing.SystemColors.Window
		Me.txtStopLevel.CausesValidation = True
		Me.txtStopLevel.Enabled = True
		Me.txtStopLevel.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtStopLevel.HideSelection = True
		Me.txtStopLevel.ReadOnly = False
		Me.txtStopLevel.Maxlength = 0
		Me.txtStopLevel.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtStopLevel.MultiLine = False
		Me.txtStopLevel.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtStopLevel.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtStopLevel.TabStop = True
		Me.txtStopLevel.Visible = True
		Me.txtStopLevel.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.txtStopLevel.Name = "txtStopLevel"
		spnStopLevel.OcxState = CType(resources.GetObject("spnStopLevel.OcxState"), System.Windows.Forms.AxHost.State)
		Me.spnStopLevel.Size = New System.Drawing.Size(21, 17)
		Me.spnStopLevel.Location = New System.Drawing.Point(39, 1)
		Me.spnStopLevel.TabIndex = 201
		Me.spnStopLevel.Name = "spnStopLevel"
		Me.Label1.Text = "Level"
		Me.Label1.Size = New System.Drawing.Size(60, 33)
		Me.Label1.Location = New System.Drawing.Point(74, 121)
		Me.Label1.TabIndex = 208
		Me.Label1.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label1.BackColor = System.Drawing.SystemColors.Control
		Me.Label1.Enabled = True
		Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label1.UseMnemonic = True
		Me.Label1.Visible = True
		Me.Label1.AutoSize = False
		Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label1.Name = "Label1"
		Me.lblStopEdgeCap.Text = "Edge #"
		Me.lblStopEdgeCap.Size = New System.Drawing.Size(46, 14)
		Me.lblStopEdgeCap.Location = New System.Drawing.Point(74, 84)
		Me.lblStopEdgeCap.TabIndex = 207
		Me.lblStopEdgeCap.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblStopEdgeCap.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.lblStopEdgeCap.BackColor = System.Drawing.SystemColors.Control
		Me.lblStopEdgeCap.Enabled = True
		Me.lblStopEdgeCap.ForeColor = System.Drawing.SystemColors.ControlText
		Me.lblStopEdgeCap.Cursor = System.Windows.Forms.Cursors.Default
		Me.lblStopEdgeCap.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.lblStopEdgeCap.UseMnemonic = True
		Me.lblStopEdgeCap.Visible = True
		Me.lblStopEdgeCap.AutoSize = False
		Me.lblStopEdgeCap.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.lblStopEdgeCap.Name = "lblStopEdgeCap"
		Me.lblStopSource.Text = "Source"
		Me.lblStopSource.Size = New System.Drawing.Size(46, 14)
		Me.lblStopSource.Location = New System.Drawing.Point(74, 13)
		Me.lblStopSource.TabIndex = 206
		Me.lblStopSource.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblStopSource.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.lblStopSource.BackColor = System.Drawing.SystemColors.Control
		Me.lblStopSource.Enabled = True
		Me.lblStopSource.ForeColor = System.Drawing.SystemColors.ControlText
		Me.lblStopSource.Cursor = System.Windows.Forms.Cursors.Default
		Me.lblStopSource.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.lblStopSource.UseMnemonic = True
		Me.lblStopSource.Visible = True
		Me.lblStopSource.AutoSize = False
		Me.lblStopSource.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.lblStopSource.Name = "lblStopSource"
		Me.lblStartEdgeCap.Text = "Edge #"
		Me.lblStartEdgeCap.Size = New System.Drawing.Size(46, 14)
		Me.lblStartEdgeCap.Location = New System.Drawing.Point(0, 84)
		Me.lblStartEdgeCap.TabIndex = 205
		Me.lblStartEdgeCap.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblStartEdgeCap.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.lblStartEdgeCap.BackColor = System.Drawing.SystemColors.Control
		Me.lblStartEdgeCap.Enabled = True
		Me.lblStartEdgeCap.ForeColor = System.Drawing.SystemColors.ControlText
		Me.lblStartEdgeCap.Cursor = System.Windows.Forms.Cursors.Default
		Me.lblStartEdgeCap.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.lblStartEdgeCap.UseMnemonic = True
		Me.lblStartEdgeCap.Visible = True
		Me.lblStartEdgeCap.AutoSize = False
		Me.lblStartEdgeCap.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.lblStartEdgeCap.Name = "lblStartEdgeCap"
		Me.Line1.BackColor = System.Drawing.SystemColors.WindowText
		Me.Line1.Visible = True
		Me.Line1.Location = New System.Drawing.Point(67, 5)
		Me.Line1.Size = New System.Drawing.Size(1, 169)
		Me.Line1.Name = "Line1"
		Me.lblStop.Text = "Stop"
		Me.lblStop.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Underline Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblStop.Size = New System.Drawing.Size(26, 14)
		Me.lblStop.Location = New System.Drawing.Point(86, 0)
		Me.lblStop.TabIndex = 204
		Me.lblStop.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.lblStop.BackColor = System.Drawing.SystemColors.Control
		Me.lblStop.Enabled = True
		Me.lblStop.ForeColor = System.Drawing.SystemColors.ControlText
		Me.lblStop.Cursor = System.Windows.Forms.Cursors.Default
		Me.lblStop.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.lblStop.UseMnemonic = True
		Me.lblStop.Visible = True
		Me.lblStop.AutoSize = False
		Me.lblStop.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.lblStop.Name = "lblStop"
		Me.lblStar.Text = "Start"
		Me.lblStar.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Underline Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblStar.Size = New System.Drawing.Size(26, 14)
		Me.lblStar.Location = New System.Drawing.Point(15, 0)
		Me.lblStar.TabIndex = 203
		Me.lblStar.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.lblStar.BackColor = System.Drawing.SystemColors.Control
		Me.lblStar.Enabled = True
		Me.lblStar.ForeColor = System.Drawing.SystemColors.ControlText
		Me.lblStar.Cursor = System.Windows.Forms.Cursors.Default
		Me.lblStar.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.lblStar.UseMnemonic = True
		Me.lblStar.Visible = True
		Me.lblStar.AutoSize = False
		Me.lblStar.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.lblStar.Name = "lblStar"
		Me.lblStartSource.Text = "Source"
		Me.lblStartSource.Size = New System.Drawing.Size(46, 14)
		Me.lblStartSource.Location = New System.Drawing.Point(0, 13)
		Me.lblStartSource.TabIndex = 202
		Me.lblStartSource.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblStartSource.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.lblStartSource.BackColor = System.Drawing.SystemColors.Control
		Me.lblStartSource.Enabled = True
		Me.lblStartSource.ForeColor = System.Drawing.SystemColors.ControlText
		Me.lblStartSource.Cursor = System.Windows.Forms.Cursors.Default
		Me.lblStartSource.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.lblStartSource.UseMnemonic = True
		Me.lblStartSource.Visible = True
		Me.lblStartSource.AutoSize = False
		Me.lblStartSource.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.lblStartSource.Name = "lblStartSource"
		Me.cmdHelp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		Me.cmdHelp.Text = "Help"
		Me.cmdHelp.Size = New System.Drawing.Size(100, 23)
		Me.cmdHelp.Location = New System.Drawing.Point(188, 244)
		Me.cmdHelp.TabIndex = 172
		Me.cmdHelp.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdHelp.BackColor = System.Drawing.SystemColors.Control
		Me.cmdHelp.CausesValidation = True
		Me.cmdHelp.Enabled = True
		Me.cmdHelp.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdHelp.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdHelp.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdHelp.TabStop = True
		Me.cmdHelp.Name = "cmdHelp"
		Me._tabMainFunctions_TabPage4.Text = "Trigger       "
		fraLine.OcxState = CType(resources.GetObject("fraLine.OcxState"), System.Windows.Forms.AxHost.State)
		Me.fraLine.Size = New System.Drawing.Size(89, 53)
		Me.fraLine.Location = New System.Drawing.Point(200, 212)
		Me.fraLine.TabIndex = 28
		Me.fraLine.Visible = False
		Me.fraLine.Name = "fraLine"
		SSPanel14.OcxState = CType(resources.GetObject("SSPanel14.OcxState"), System.Windows.Forms.AxHost.State)
		Me.SSPanel14.Size = New System.Drawing.Size(73, 19)
		Me.SSPanel14.Location = New System.Drawing.Point(9, 24)
		Me.SSPanel14.TabIndex = 42
		Me.SSPanel14.Name = "SSPanel14"
		Me.txtLine.AutoSize = False
		Me.txtLine.Size = New System.Drawing.Size(50, 17)
		Me.txtLine.Location = New System.Drawing.Point(2, 1)
		Me.txtLine.TabIndex = 43
		Me.txtLine.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtLine.AcceptsReturn = True
		Me.txtLine.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtLine.BackColor = System.Drawing.SystemColors.Window
		Me.txtLine.CausesValidation = True
		Me.txtLine.Enabled = True
		Me.txtLine.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtLine.HideSelection = True
		Me.txtLine.ReadOnly = False
		Me.txtLine.Maxlength = 0
		Me.txtLine.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtLine.MultiLine = False
		Me.txtLine.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtLine.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtLine.TabStop = True
		Me.txtLine.Visible = True
		Me.txtLine.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.txtLine.Name = "txtLine"
		spnLine.OcxState = CType(resources.GetObject("spnLine.OcxState"), System.Windows.Forms.AxHost.State)
		Me.spnLine.Size = New System.Drawing.Size(21, 17)
		Me.spnLine.Location = New System.Drawing.Point(51, 1)
		Me.spnLine.TabIndex = 44
		Me.spnLine.Name = "spnLine"
		fraField.OcxState = CType(resources.GetObject("fraField.OcxState"), System.Windows.Forms.AxHost.State)
		Me.fraField.Size = New System.Drawing.Size(89, 65)
		Me.fraField.Location = New System.Drawing.Point(200, 148)
		Me.fraField.TabIndex = 25
		Me.fraField.Visible = False
		Me.fraField.Name = "fraField"
		_optField_0.OcxState = CType(resources.GetObject("_optField_0.OcxState"), System.Windows.Forms.AxHost.State)
		Me._optField_0.Size = New System.Drawing.Size(65, 17)
		Me._optField_0.Location = New System.Drawing.Point(8, 20)
		Me._optField_0.TabIndex = 27
		Me._optField_0.Name = "_optField_0"
		_optField_1.OcxState = CType(resources.GetObject("_optField_1.OcxState"), System.Windows.Forms.AxHost.State)
		Me._optField_1.Size = New System.Drawing.Size(65, 17)
		Me._optField_1.Location = New System.Drawing.Point(8, 36)
		Me._optField_1.TabIndex = 26
		Me._optField_1.Name = "_optField_1"
		fraStandard.OcxState = CType(resources.GetObject("fraStandard.OcxState"), System.Windows.Forms.AxHost.State)
		Me.fraStandard.Size = New System.Drawing.Size(89, 65)
		Me.fraStandard.Location = New System.Drawing.Point(200, 84)
		Me.fraStandard.TabIndex = 22
		Me.fraStandard.Visible = False
		Me.fraStandard.Name = "fraStandard"
		_optStandard_0.OcxState = CType(resources.GetObject("_optStandard_0.OcxState"), System.Windows.Forms.AxHost.State)
		Me._optStandard_0.Size = New System.Drawing.Size(77, 17)
		Me._optStandard_0.Location = New System.Drawing.Point(8, 20)
		Me._optStandard_0.TabIndex = 24
		Me._optStandard_0.Name = "_optStandard_0"
		_optStandard_1.OcxState = CType(resources.GetObject("_optStandard_1.OcxState"), System.Windows.Forms.AxHost.State)
		Me._optStandard_1.Size = New System.Drawing.Size(65, 17)
		Me._optStandard_1.Location = New System.Drawing.Point(8, 36)
		Me._optStandard_1.TabIndex = 23
		Me._optStandard_1.Name = "_optStandard_1"
		fraOccurrence.OcxState = CType(resources.GetObject("fraOccurrence.OcxState"), System.Windows.Forms.AxHost.State)
		Me.fraOccurrence.Size = New System.Drawing.Size(89, 41)
		Me.fraOccurrence.Location = New System.Drawing.Point(200, 44)
		Me.fraOccurrence.TabIndex = 29
		Me.fraOccurrence.Visible = False
		Me.fraOccurrence.Name = "fraOccurrence"
		SSPanel15.OcxState = CType(resources.GetObject("SSPanel15.OcxState"), System.Windows.Forms.AxHost.State)
		Me.SSPanel15.Size = New System.Drawing.Size(73, 19)
		Me.SSPanel15.Location = New System.Drawing.Point(9, 17)
		Me.SSPanel15.TabIndex = 45
		Me.SSPanel15.Name = "SSPanel15"
		Me.txtOccurrence.AutoSize = False
		Me.txtOccurrence.Size = New System.Drawing.Size(50, 17)
		Me.txtOccurrence.Location = New System.Drawing.Point(2, 1)
		Me.txtOccurrence.TabIndex = 46
		Me.txtOccurrence.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtOccurrence.AcceptsReturn = True
		Me.txtOccurrence.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtOccurrence.BackColor = System.Drawing.SystemColors.Window
		Me.txtOccurrence.CausesValidation = True
		Me.txtOccurrence.Enabled = True
		Me.txtOccurrence.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtOccurrence.HideSelection = True
		Me.txtOccurrence.ReadOnly = False
		Me.txtOccurrence.Maxlength = 0
		Me.txtOccurrence.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtOccurrence.MultiLine = False
		Me.txtOccurrence.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtOccurrence.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtOccurrence.TabStop = True
		Me.txtOccurrence.Visible = True
		Me.txtOccurrence.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.txtOccurrence.Name = "txtOccurrence"
		spnOccurrence.OcxState = CType(resources.GetObject("spnOccurrence.OcxState"), System.Windows.Forms.AxHost.State)
		Me.spnOccurrence.Size = New System.Drawing.Size(21, 17)
		Me.spnOccurrence.Location = New System.Drawing.Point(51, 1)
		Me.spnOccurrence.TabIndex = 47
		Me.spnOccurrence.Name = "spnOccurrence"
		fraDelaySlope.OcxState = CType(resources.GetObject("fraDelaySlope.OcxState"), System.Windows.Forms.AxHost.State)
		Me.fraDelaySlope.Size = New System.Drawing.Size(97, 53)
		Me.fraDelaySlope.Location = New System.Drawing.Point(100, 212)
		Me.fraDelaySlope.TabIndex = 32
		Me.fraDelaySlope.Visible = False
		Me.fraDelaySlope.Name = "fraDelaySlope"
		_optDelaySlope_0.OcxState = CType(resources.GetObject("_optDelaySlope_0.OcxState"), System.Windows.Forms.AxHost.State)
		Me._optDelaySlope_0.Size = New System.Drawing.Size(65, 17)
		Me._optDelaySlope_0.Location = New System.Drawing.Point(8, 16)
		Me._optDelaySlope_0.TabIndex = 34
		Me._optDelaySlope_0.Name = "_optDelaySlope_0"
		_optDelaySlope_1.OcxState = CType(resources.GetObject("_optDelaySlope_1.OcxState"), System.Windows.Forms.AxHost.State)
		Me._optDelaySlope_1.Size = New System.Drawing.Size(65, 17)
		Me._optDelaySlope_1.Location = New System.Drawing.Point(8, 32)
		Me._optDelaySlope_1.TabIndex = 33
		Me._optDelaySlope_1.Name = "_optDelaySlope_1"
		fraTrigDelaySource.OcxState = CType(resources.GetObject("fraTrigDelaySource.OcxState"), System.Windows.Forms.AxHost.State)
		Me.fraTrigDelaySource.Size = New System.Drawing.Size(97, 45)
		Me.fraTrigDelaySource.Location = New System.Drawing.Point(100, 168)
		Me.fraTrigDelaySource.TabIndex = 30
		Me.fraTrigDelaySource.Visible = False
		Me.fraTrigDelaySource.Name = "fraTrigDelaySource"
		Me.cboTrigDelaySour.BackColor = System.Drawing.Color.White
		Me.cboTrigDelaySour.Size = New System.Drawing.Size(80, 21)
		Me.cboTrigDelaySour.Location = New System.Drawing.Point(8, 16)
		Me.cboTrigDelaySour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cboTrigDelaySour.TabIndex = 31
		Me.cboTrigDelaySour.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cboTrigDelaySour.CausesValidation = True
		Me.cboTrigDelaySour.Enabled = True
		Me.cboTrigDelaySour.ForeColor = System.Drawing.SystemColors.WindowText
		Me.cboTrigDelaySour.IntegralHeight = True
		Me.cboTrigDelaySour.Cursor = System.Windows.Forms.Cursors.Default
		Me.cboTrigDelaySour.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cboTrigDelaySour.Sorted = False
		Me.cboTrigDelaySour.TabStop = True
		Me.cboTrigDelaySour.Visible = True
		Me.cboTrigDelaySour.Name = "cboTrigDelaySour"
		fraTrigDelay.OcxState = CType(resources.GetObject("fraTrigDelay.OcxState"), System.Windows.Forms.AxHost.State)
		Me.fraTrigDelay.Size = New System.Drawing.Size(97, 85)
		Me.fraTrigDelay.Location = New System.Drawing.Point(100, 84)
		Me.fraTrigDelay.TabIndex = 18
		Me.fraTrigDelay.Visible = False
		Me.fraTrigDelay.Name = "fraTrigDelay"
		SSPanel13.OcxState = CType(resources.GetObject("SSPanel13.OcxState"), System.Windows.Forms.AxHost.State)
		Me.SSPanel13.Size = New System.Drawing.Size(78, 19)
		Me.SSPanel13.Location = New System.Drawing.Point(9, 60)
		Me.SSPanel13.TabIndex = 39
		Me.SSPanel13.Name = "SSPanel13"
		Me.txtDelEvents.AutoSize = False
		Me.txtDelEvents.Size = New System.Drawing.Size(54, 17)
		Me.txtDelEvents.Location = New System.Drawing.Point(2, 1)
		Me.txtDelEvents.TabIndex = 40
		Me.txtDelEvents.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtDelEvents.AcceptsReturn = True
		Me.txtDelEvents.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtDelEvents.BackColor = System.Drawing.SystemColors.Window
		Me.txtDelEvents.CausesValidation = True
		Me.txtDelEvents.Enabled = True
		Me.txtDelEvents.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtDelEvents.HideSelection = True
		Me.txtDelEvents.ReadOnly = False
		Me.txtDelEvents.Maxlength = 0
		Me.txtDelEvents.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtDelEvents.MultiLine = False
		Me.txtDelEvents.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtDelEvents.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtDelEvents.TabStop = True
		Me.txtDelEvents.Visible = True
		Me.txtDelEvents.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.txtDelEvents.Name = "txtDelEvents"
		spnDelEvents.OcxState = CType(resources.GetObject("spnDelEvents.OcxState"), System.Windows.Forms.AxHost.State)
		Me.spnDelEvents.Size = New System.Drawing.Size(21, 17)
		Me.spnDelEvents.Location = New System.Drawing.Point(56, 1)
		Me.spnDelEvents.TabIndex = 41
		Me.spnDelEvents.Name = "spnDelEvents"
		_optTrigDelay_1.OcxState = CType(resources.GetObject("_optTrigDelay_1.OcxState"), System.Windows.Forms.AxHost.State)
		Me._optTrigDelay_1.Size = New System.Drawing.Size(65, 17)
		Me._optTrigDelay_1.Location = New System.Drawing.Point(8, 32)
		Me._optTrigDelay_1.TabIndex = 21
		Me._optTrigDelay_1.Name = "_optTrigDelay_1"
		_optTrigDelay_0.OcxState = CType(resources.GetObject("_optTrigDelay_0.OcxState"), System.Windows.Forms.AxHost.State)
		Me._optTrigDelay_0.Size = New System.Drawing.Size(65, 17)
		Me._optTrigDelay_0.Location = New System.Drawing.Point(8, 16)
		Me._optTrigDelay_0.TabIndex = 20
		Me._optTrigDelay_0.Name = "_optTrigDelay_0"
		Me.lblTrigDelayEvents.Text = "Time"
		Me.lblTrigDelayEvents.Size = New System.Drawing.Size(38, 15)
		Me.lblTrigDelayEvents.Location = New System.Drawing.Point(8, 48)
		Me.lblTrigDelayEvents.TabIndex = 19
		Me.lblTrigDelayEvents.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblTrigDelayEvents.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.lblTrigDelayEvents.BackColor = System.Drawing.SystemColors.Control
		Me.lblTrigDelayEvents.Enabled = True
		Me.lblTrigDelayEvents.ForeColor = System.Drawing.SystemColors.ControlText
		Me.lblTrigDelayEvents.Cursor = System.Windows.Forms.Cursors.Default
		Me.lblTrigDelayEvents.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.lblTrigDelayEvents.UseMnemonic = True
		Me.lblTrigDelayEvents.Visible = True
		Me.lblTrigDelayEvents.AutoSize = False
		Me.lblTrigDelayEvents.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.lblTrigDelayEvents.Name = "lblTrigDelayEvents"
		fraTrigLevel.OcxState = CType(resources.GetObject("fraTrigLevel.OcxState"), System.Windows.Forms.AxHost.State)
		Me.fraTrigLevel.Size = New System.Drawing.Size(97, 41)
		Me.fraTrigLevel.Location = New System.Drawing.Point(100, 44)
		Me.fraTrigLevel.TabIndex = 17
		Me.fraTrigLevel.Name = "fraTrigLevel"
		SSPanel12.OcxState = CType(resources.GetObject("SSPanel12.OcxState"), System.Windows.Forms.AxHost.State)
		Me.SSPanel12.Size = New System.Drawing.Size(78, 19)
		Me.SSPanel12.Location = New System.Drawing.Point(9, 16)
		Me.SSPanel12.TabIndex = 36
		Me.SSPanel12.Name = "SSPanel12"
		Me.txtTrigLevel.AutoSize = False
		Me.txtTrigLevel.Size = New System.Drawing.Size(54, 17)
		Me.txtTrigLevel.Location = New System.Drawing.Point(2, 1)
		Me.txtTrigLevel.TabIndex = 37
		Me.txtTrigLevel.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtTrigLevel.AcceptsReturn = True
		Me.txtTrigLevel.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtTrigLevel.BackColor = System.Drawing.SystemColors.Window
		Me.txtTrigLevel.CausesValidation = True
		Me.txtTrigLevel.Enabled = True
		Me.txtTrigLevel.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtTrigLevel.HideSelection = True
		Me.txtTrigLevel.ReadOnly = False
		Me.txtTrigLevel.Maxlength = 0
		Me.txtTrigLevel.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtTrigLevel.MultiLine = False
		Me.txtTrigLevel.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtTrigLevel.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtTrigLevel.TabStop = True
		Me.txtTrigLevel.Visible = True
		Me.txtTrigLevel.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.txtTrigLevel.Name = "txtTrigLevel"
		spnTrigLevel.OcxState = CType(resources.GetObject("spnTrigLevel.OcxState"), System.Windows.Forms.AxHost.State)
		Me.spnTrigLevel.Size = New System.Drawing.Size(21, 17)
		Me.spnTrigLevel.Location = New System.Drawing.Point(56, 1)
		Me.spnTrigLevel.TabIndex = 38
		Me.spnTrigLevel.Name = "spnTrigLevel"
		fraNoiseRej.OcxState = CType(resources.GetObject("fraNoiseRej.OcxState"), System.Windows.Forms.AxHost.State)
		Me.fraNoiseRej.Size = New System.Drawing.Size(89, 53)
		Me.fraNoiseRej.Location = New System.Drawing.Point(8, 212)
		Me.fraNoiseRej.TabIndex = 14
		Me.fraNoiseRej.Name = "fraNoiseRej"
		_optNoiseRej_1.OcxState = CType(resources.GetObject("_optNoiseRej_1.OcxState"), System.Windows.Forms.AxHost.State)
		Me._optNoiseRej_1.Size = New System.Drawing.Size(49, 17)
		Me._optNoiseRej_1.Location = New System.Drawing.Point(8, 32)
		Me._optNoiseRej_1.TabIndex = 16
		Me._optNoiseRej_1.Name = "_optNoiseRej_1"
		_optNoiseRej_0.OcxState = CType(resources.GetObject("_optNoiseRej_0.OcxState"), System.Windows.Forms.AxHost.State)
		Me._optNoiseRej_0.Size = New System.Drawing.Size(73, 17)
		Me._optNoiseRej_0.Location = New System.Drawing.Point(8, 16)
		Me._optNoiseRej_0.TabIndex = 15
		Me._optNoiseRej_0.Name = "_optNoiseRej_0"
		fraTrigSlope.OcxState = CType(resources.GetObject("fraTrigSlope.OcxState"), System.Windows.Forms.AxHost.State)
		Me.fraTrigSlope.Size = New System.Drawing.Size(89, 57)
		Me.fraTrigSlope.Location = New System.Drawing.Point(8, 156)
		Me.fraTrigSlope.TabIndex = 11
		Me.fraTrigSlope.Name = "fraTrigSlope"
		_optTrigSlope_1.OcxState = CType(resources.GetObject("_optTrigSlope_1.OcxState"), System.Windows.Forms.AxHost.State)
		Me._optTrigSlope_1.Size = New System.Drawing.Size(65, 17)
		Me._optTrigSlope_1.Location = New System.Drawing.Point(8, 32)
		Me._optTrigSlope_1.TabIndex = 13
		Me._optTrigSlope_1.Name = "_optTrigSlope_1"
		_optTrigSlope_0.OcxState = CType(resources.GetObject("_optTrigSlope_0.OcxState"), System.Windows.Forms.AxHost.State)
		Me._optTrigSlope_0.Size = New System.Drawing.Size(65, 17)
		Me._optTrigSlope_0.Location = New System.Drawing.Point(8, 15)
		Me._optTrigSlope_0.TabIndex = 12
		Me._optTrigSlope_0.Name = "_optTrigSlope_0"
		fraTrigSour.OcxState = CType(resources.GetObject("fraTrigSour.OcxState"), System.Windows.Forms.AxHost.State)
		Me.fraTrigSour.Size = New System.Drawing.Size(89, 45)
		Me.fraTrigSour.Location = New System.Drawing.Point(8, 112)
		Me.fraTrigSour.TabIndex = 9
		Me.fraTrigSour.Name = "fraTrigSour"
		Me.cboTrigSour.BackColor = System.Drawing.Color.White
		Me.cboTrigSour.Size = New System.Drawing.Size(75, 21)
		Me.cboTrigSour.Location = New System.Drawing.Point(7, 16)
		Me.cboTrigSour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cboTrigSour.TabIndex = 10
		Me.cboTrigSour.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cboTrigSour.CausesValidation = True
		Me.cboTrigSour.Enabled = True
		Me.cboTrigSour.ForeColor = System.Drawing.SystemColors.WindowText
		Me.cboTrigSour.IntegralHeight = True
		Me.cboTrigSour.Cursor = System.Windows.Forms.Cursors.Default
		Me.cboTrigSour.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cboTrigSour.Sorted = False
		Me.cboTrigSour.TabStop = True
		Me.cboTrigSour.Visible = True
		Me.cboTrigSour.Name = "cboTrigSour"
		fraTrigMode.OcxState = CType(resources.GetObject("fraTrigMode.OcxState"), System.Windows.Forms.AxHost.State)
		Me.fraTrigMode.Size = New System.Drawing.Size(89, 69)
		Me.fraTrigMode.Location = New System.Drawing.Point(8, 44)
		Me.fraTrigMode.TabIndex = 5
		Me.fraTrigMode.Name = "fraTrigMode"
		_optTrigMode_1.OcxState = CType(resources.GetObject("_optTrigMode_1.OcxState"), System.Windows.Forms.AxHost.State)
		Me._optTrigMode_1.Size = New System.Drawing.Size(74, 17)
		Me._optTrigMode_1.Location = New System.Drawing.Point(6, 48)
		Me._optTrigMode_1.TabIndex = 8
		Me._optTrigMode_1.Visible = False
		Me._optTrigMode_1.Name = "_optTrigMode_1"
		_optTrigMode_0.OcxState = CType(resources.GetObject("_optTrigMode_0.OcxState"), System.Windows.Forms.AxHost.State)
		Me._optTrigMode_0.Size = New System.Drawing.Size(50, 17)
		Me._optTrigMode_0.Location = New System.Drawing.Point(6, 16)
		Me._optTrigMode_0.TabIndex = 7
		Me._optTrigMode_0.Name = "_optTrigMode_0"
		_optTrigMode_2.OcxState = CType(resources.GetObject("_optTrigMode_2.OcxState"), System.Windows.Forms.AxHost.State)
		Me._optTrigMode_2.Size = New System.Drawing.Size(41, 17)
		Me._optTrigMode_2.Location = New System.Drawing.Point(6, 32)
		Me._optTrigMode_2.TabIndex = 6
		Me._optTrigMode_2.Name = "_optTrigMode_2"
		Me._tabMainFunctions_TabPage5.Text = "Math/Memory      "
		Me.picMemOptions.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.picMemOptions.Size = New System.Drawing.Size(287, 98)
		Me.picMemOptions.Location = New System.Drawing.Point(7, 174)
		Me.picMemOptions.TabIndex = 80
		Me.picMemOptions.Visible = False
		Me.picMemOptions.Dock = System.Windows.Forms.DockStyle.None
		Me.picMemOptions.BackColor = System.Drawing.SystemColors.Control
		Me.picMemOptions.CausesValidation = True
		Me.picMemOptions.Enabled = True
		Me.picMemOptions.ForeColor = System.Drawing.SystemColors.ControlText
		Me.picMemOptions.Cursor = System.Windows.Forms.Cursors.Default
		Me.picMemOptions.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.picMemOptions.TabStop = True
		Me.picMemOptions.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.picMemOptions.Name = "picMemOptions"
		Me.cmdLoadFromDisk.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		Me.cmdLoadFromDisk.Text = "Load from Disk ..."
		Me.cmdLoadFromDisk.Size = New System.Drawing.Size(122, 23)
		Me.cmdLoadFromDisk.Location = New System.Drawing.Point(8, 68)
		Me.cmdLoadFromDisk.TabIndex = 87
		Me.cmdLoadFromDisk.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdLoadFromDisk.BackColor = System.Drawing.SystemColors.Control
		Me.cmdLoadFromDisk.CausesValidation = True
		Me.cmdLoadFromDisk.Enabled = True
		Me.cmdLoadFromDisk.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdLoadFromDisk.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdLoadFromDisk.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdLoadFromDisk.TabStop = True
		Me.cmdLoadFromDisk.Name = "cmdLoadFromDisk"
		Me._cmdSaveToDisk_3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		Me._cmdSaveToDisk_3.Text = "Save to Disk ..."
		Me._cmdSaveToDisk_3.Size = New System.Drawing.Size(122, 23)
		Me._cmdSaveToDisk_3.Location = New System.Drawing.Point(156, 68)
		Me._cmdSaveToDisk_3.TabIndex = 86
		Me._cmdSaveToDisk_3.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me._cmdSaveToDisk_3.BackColor = System.Drawing.SystemColors.Control
		Me._cmdSaveToDisk_3.CausesValidation = True
		Me._cmdSaveToDisk_3.Enabled = True
		Me._cmdSaveToDisk_3.ForeColor = System.Drawing.SystemColors.ControlText
		Me._cmdSaveToDisk_3.Cursor = System.Windows.Forms.Cursors.Default
		Me._cmdSaveToDisk_3.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me._cmdSaveToDisk_3.TabStop = True
		Me._cmdSaveToDisk_3.Name = "_cmdSaveToDisk_3"
		fraMemVerticalRange.OcxState = CType(resources.GetObject("fraMemVerticalRange.OcxState"), System.Windows.Forms.AxHost.State)
		Me.fraMemVerticalRange.Size = New System.Drawing.Size(138, 44)
		Me.fraMemVerticalRange.Location = New System.Drawing.Point(0, 15)
		Me.fraMemVerticalRange.TabIndex = 82
		Me.fraMemVerticalRange.Name = "fraMemVerticalRange"
		SSPanel19.OcxState = CType(resources.GetObject("SSPanel19.OcxState"), System.Windows.Forms.AxHost.State)
		Me.SSPanel19.Size = New System.Drawing.Size(104, 19)
		Me.SSPanel19.Location = New System.Drawing.Point(9, 19)
		Me.SSPanel19.TabIndex = 83
		Me.SSPanel19.Name = "SSPanel19"
		Me.txtMemoryRange.AutoSize = False
		Me.txtMemoryRange.Size = New System.Drawing.Size(79, 17)
		Me.txtMemoryRange.Location = New System.Drawing.Point(3, 2)
		Me.txtMemoryRange.TabIndex = 84
		Me.txtMemoryRange.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtMemoryRange.AcceptsReturn = True
		Me.txtMemoryRange.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtMemoryRange.BackColor = System.Drawing.SystemColors.Window
		Me.txtMemoryRange.CausesValidation = True
		Me.txtMemoryRange.Enabled = True
		Me.txtMemoryRange.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtMemoryRange.HideSelection = True
		Me.txtMemoryRange.ReadOnly = False
		Me.txtMemoryRange.Maxlength = 0
		Me.txtMemoryRange.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtMemoryRange.MultiLine = False
		Me.txtMemoryRange.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtMemoryRange.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtMemoryRange.TabStop = True
		Me.txtMemoryRange.Visible = True
		Me.txtMemoryRange.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.txtMemoryRange.Name = "txtMemoryRange"
		SpinButton2.OcxState = CType(resources.GetObject("SpinButton2.OcxState"), System.Windows.Forms.AxHost.State)
		Me.SpinButton2.Size = New System.Drawing.Size(21, 17)
		Me.SpinButton2.Location = New System.Drawing.Point(82, 1)
		Me.SpinButton2.TabIndex = 85
		Me.SpinButton2.Name = "SpinButton2"
		SSFrame2.OcxState = CType(resources.GetObject("SSFrame2.OcxState"), System.Windows.Forms.AxHost.State)
		Me.SSFrame2.Size = New System.Drawing.Size(138, 44)
		Me.SSFrame2.Location = New System.Drawing.Point(144, 15)
		Me.SSFrame2.TabIndex = 88
		Me.SSFrame2.Name = "SSFrame2"
		SSPanel17.OcxState = CType(resources.GetObject("SSPanel17.OcxState"), System.Windows.Forms.AxHost.State)
		Me.SSPanel17.Size = New System.Drawing.Size(104, 19)
		Me.SSPanel17.Location = New System.Drawing.Point(9, 19)
		Me.SSPanel17.TabIndex = 89
		Me.SSPanel17.Name = "SSPanel17"
		Me.txtMemVoltOffset.AutoSize = False
		Me.txtMemVoltOffset.Size = New System.Drawing.Size(79, 17)
		Me.txtMemVoltOffset.Location = New System.Drawing.Point(2, 1)
		Me.txtMemVoltOffset.TabIndex = 90
		Me.txtMemVoltOffset.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtMemVoltOffset.AcceptsReturn = True
		Me.txtMemVoltOffset.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtMemVoltOffset.BackColor = System.Drawing.SystemColors.Window
		Me.txtMemVoltOffset.CausesValidation = True
		Me.txtMemVoltOffset.Enabled = True
		Me.txtMemVoltOffset.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtMemVoltOffset.HideSelection = True
		Me.txtMemVoltOffset.ReadOnly = False
		Me.txtMemVoltOffset.Maxlength = 0
		Me.txtMemVoltOffset.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtMemVoltOffset.MultiLine = False
		Me.txtMemVoltOffset.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtMemVoltOffset.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtMemVoltOffset.TabStop = True
		Me.txtMemVoltOffset.Visible = True
		Me.txtMemVoltOffset.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.txtMemVoltOffset.Name = "txtMemVoltOffset"
		spnMemVOffset.OcxState = CType(resources.GetObject("spnMemVOffset.OcxState"), System.Windows.Forms.AxHost.State)
		Me.spnMemVOffset.Size = New System.Drawing.Size(21, 17)
		Me.spnMemVOffset.Location = New System.Drawing.Point(82, 1)
		Me.spnMemVOffset.TabIndex = 91
		Me.spnMemVOffset.Name = "spnMemVOffset"
		Me.Line5.BackColor = System.Drawing.SystemColors.WindowText
		Me.Line5.Visible = True
		Me.Line5.Location = New System.Drawing.Point(168, 7)
		Me.Line5.Size = New System.Drawing.Size(109, 1)
		Me.Line5.Name = "Line5"
		Me.Line4.BackColor = System.Drawing.SystemColors.WindowText
		Me.Line4.Visible = True
		Me.Line4.Location = New System.Drawing.Point(0, 6)
		Me.Line4.Size = New System.Drawing.Size(109, 1)
		Me.Line4.Name = "Line4"
		Me.Label3.Text = "Memory"
		Me.Label3.Size = New System.Drawing.Size(42, 15)
		Me.Label3.Location = New System.Drawing.Point(119, 0)
		Me.Label3.TabIndex = 81
		Me.Label3.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label3.BackColor = System.Drawing.SystemColors.Control
		Me.Label3.Enabled = True
		Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label3.UseMnemonic = True
		Me.Label3.Visible = True
		Me.Label3.AutoSize = False
		Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label3.Name = "Label3"
		Me.picMathOptions.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.picMathOptions.Size = New System.Drawing.Size(286, 130)
		Me.picMathOptions.Location = New System.Drawing.Point(6, 44)
		Me.picMathOptions.TabIndex = 62
		Me.picMathOptions.Visible = False
		Me.picMathOptions.Dock = System.Windows.Forms.DockStyle.None
		Me.picMathOptions.BackColor = System.Drawing.SystemColors.Control
		Me.picMathOptions.CausesValidation = True
		Me.picMathOptions.Enabled = True
		Me.picMathOptions.ForeColor = System.Drawing.SystemColors.ControlText
		Me.picMathOptions.Cursor = System.Windows.Forms.Cursors.Default
		Me.picMathOptions.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.picMathOptions.TabStop = True
		Me.picMathOptions.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.picMathOptions.Name = "picMathOptions"
		Me._cmdSaveToMem_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		Me._cmdSaveToMem_2.Text = "Save to Memory"
		Me._cmdSaveToMem_2.Size = New System.Drawing.Size(122, 23)
		Me._cmdSaveToMem_2.Location = New System.Drawing.Point(8, 104)
		Me._cmdSaveToMem_2.TabIndex = 74
		Me._cmdSaveToMem_2.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me._cmdSaveToMem_2.BackColor = System.Drawing.SystemColors.Control
		Me._cmdSaveToMem_2.CausesValidation = True
		Me._cmdSaveToMem_2.Enabled = True
		Me._cmdSaveToMem_2.ForeColor = System.Drawing.SystemColors.ControlText
		Me._cmdSaveToMem_2.Cursor = System.Windows.Forms.Cursors.Default
		Me._cmdSaveToMem_2.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me._cmdSaveToMem_2.TabStop = True
		Me._cmdSaveToMem_2.Name = "_cmdSaveToMem_2"
		Me._cmdSaveToDisk_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		Me._cmdSaveToDisk_2.Text = "Save to Disk ..."
		Me._cmdSaveToDisk_2.Size = New System.Drawing.Size(122, 23)
		Me._cmdSaveToDisk_2.Location = New System.Drawing.Point(156, 104)
		Me._cmdSaveToDisk_2.TabIndex = 73
		Me._cmdSaveToDisk_2.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me._cmdSaveToDisk_2.BackColor = System.Drawing.SystemColors.Control
		Me._cmdSaveToDisk_2.CausesValidation = True
		Me._cmdSaveToDisk_2.Enabled = True
		Me._cmdSaveToDisk_2.ForeColor = System.Drawing.SystemColors.ControlText
		Me._cmdSaveToDisk_2.Cursor = System.Windows.Forms.Cursors.Default
		Me._cmdSaveToDisk_2.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me._cmdSaveToDisk_2.TabStop = True
		Me._cmdSaveToDisk_2.Name = "_cmdSaveToDisk_2"
		fraRange.OcxState = CType(resources.GetObject("fraRange.OcxState"), System.Windows.Forms.AxHost.State)
		Me.fraRange.Size = New System.Drawing.Size(90, 44)
		Me.fraRange.Location = New System.Drawing.Point(95, 12)
		Me.fraRange.TabIndex = 63
		Me.fraRange.Name = "fraRange"
		Me.cboMathSourceA.BackColor = System.Drawing.Color.White
		Me.cboMathSourceA.Size = New System.Drawing.Size(75, 21)
		Me.cboMathSourceA.Location = New System.Drawing.Point(8, 16)
		Me.cboMathSourceA.Items.AddRange(New Object(){"Channel 1", "Channel 2", "Memory"})
		Me.cboMathSourceA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cboMathSourceA.TabIndex = 64
		Me.cboMathSourceA.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cboMathSourceA.CausesValidation = True
		Me.cboMathSourceA.Enabled = True
		Me.cboMathSourceA.ForeColor = System.Drawing.SystemColors.WindowText
		Me.cboMathSourceA.IntegralHeight = True
		Me.cboMathSourceA.Cursor = System.Windows.Forms.Cursors.Default
		Me.cboMathSourceA.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cboMathSourceA.Sorted = False
		Me.cboMathSourceA.TabStop = True
		Me.cboMathSourceA.Visible = True
		Me.cboMathSourceA.Name = "cboMathSourceA"
		fraMathSourceB.OcxState = CType(resources.GetObject("fraMathSourceB.OcxState"), System.Windows.Forms.AxHost.State)
		Me.fraMathSourceB.Size = New System.Drawing.Size(90, 44)
		Me.fraMathSourceB.Location = New System.Drawing.Point(190, 12)
		Me.fraMathSourceB.TabIndex = 65
		Me.fraMathSourceB.Name = "fraMathSourceB"
		Me.cboMathSourceB.BackColor = System.Drawing.Color.White
		Me.cboMathSourceB.Size = New System.Drawing.Size(75, 21)
		Me.cboMathSourceB.Location = New System.Drawing.Point(8, 16)
		Me.cboMathSourceB.Items.AddRange(New Object(){"Channel 1", "Channel 2", "Memory"})
		Me.cboMathSourceB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cboMathSourceB.TabIndex = 66
		Me.cboMathSourceB.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cboMathSourceB.CausesValidation = True
		Me.cboMathSourceB.Enabled = True
		Me.cboMathSourceB.ForeColor = System.Drawing.SystemColors.WindowText
		Me.cboMathSourceB.IntegralHeight = True
		Me.cboMathSourceB.Cursor = System.Windows.Forms.Cursors.Default
		Me.cboMathSourceB.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cboMathSourceB.Sorted = False
		Me.cboMathSourceB.TabStop = True
		Me.cboMathSourceB.Visible = True
		Me.cboMathSourceB.Name = "cboMathSourceB"
		fraOperation.OcxState = CType(resources.GetObject("fraOperation.OcxState"), System.Windows.Forms.AxHost.State)
		Me.fraOperation.Size = New System.Drawing.Size(90, 44)
		Me.fraOperation.Location = New System.Drawing.Point(0, 12)
		Me.fraOperation.TabIndex = 67
		Me.fraOperation.Name = "fraOperation"
		Me.cboMathFunction.BackColor = System.Drawing.Color.White
		Me.cboMathFunction.Size = New System.Drawing.Size(75, 21)
		Me.cboMathFunction.Location = New System.Drawing.Point(8, 16)
		Me.cboMathFunction.Items.AddRange(New Object(){"Add", "Subtract", "Multiply", "Differentiate", "Integrate", "Invert"})
		Me.cboMathFunction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cboMathFunction.TabIndex = 68
		Me.cboMathFunction.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cboMathFunction.CausesValidation = True
		Me.cboMathFunction.Enabled = True
		Me.cboMathFunction.ForeColor = System.Drawing.SystemColors.WindowText
		Me.cboMathFunction.IntegralHeight = True
		Me.cboMathFunction.Cursor = System.Windows.Forms.Cursors.Default
		Me.cboMathFunction.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cboMathFunction.Sorted = False
		Me.cboMathFunction.TabStop = True
		Me.cboMathFunction.Visible = True
		Me.cboMathFunction.Name = "cboMathFunction"
		fraMemVRange.OcxState = CType(resources.GetObject("fraMemVRange.OcxState"), System.Windows.Forms.AxHost.State)
		Me.fraMemVRange.Size = New System.Drawing.Size(138, 44)
		Me.fraMemVRange.Location = New System.Drawing.Point(0, 56)
		Me.fraMemVRange.TabIndex = 69
		Me.fraMemVRange.Name = "fraMemVRange"
		SSPanel18.OcxState = CType(resources.GetObject("SSPanel18.OcxState"), System.Windows.Forms.AxHost.State)
		Me.SSPanel18.Size = New System.Drawing.Size(104, 19)
		Me.SSPanel18.Location = New System.Drawing.Point(10, 19)
		Me.SSPanel18.TabIndex = 70
		Me.SSPanel18.Name = "SSPanel18"
		Me.txtMathRange.AutoSize = False
		Me.txtMathRange.Size = New System.Drawing.Size(79, 17)
		Me.txtMathRange.Location = New System.Drawing.Point(2, 1)
		Me.txtMathRange.TabIndex = 71
		Me.txtMathRange.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtMathRange.AcceptsReturn = True
		Me.txtMathRange.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtMathRange.BackColor = System.Drawing.SystemColors.Window
		Me.txtMathRange.CausesValidation = True
		Me.txtMathRange.Enabled = True
		Me.txtMathRange.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtMathRange.HideSelection = True
		Me.txtMathRange.ReadOnly = False
		Me.txtMathRange.Maxlength = 0
		Me.txtMathRange.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtMathRange.MultiLine = False
		Me.txtMathRange.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtMathRange.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtMathRange.TabStop = True
		Me.txtMathRange.Visible = True
		Me.txtMathRange.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.txtMathRange.Name = "txtMathRange"
		SpinButton1.OcxState = CType(resources.GetObject("SpinButton1.OcxState"), System.Windows.Forms.AxHost.State)
		Me.SpinButton1.Size = New System.Drawing.Size(21, 17)
		Me.SpinButton1.Location = New System.Drawing.Point(82, 1)
		Me.SpinButton1.TabIndex = 72
		Me.SpinButton1.Name = "SpinButton1"
		SSFrame1.OcxState = CType(resources.GetObject("SSFrame1.OcxState"), System.Windows.Forms.AxHost.State)
		Me.SSFrame1.Size = New System.Drawing.Size(138, 44)
		Me.SSFrame1.Location = New System.Drawing.Point(144, 56)
		Me.SSFrame1.TabIndex = 75
		Me.SSFrame1.Name = "SSFrame1"
		SSPanel16.OcxState = CType(resources.GetObject("SSPanel16.OcxState"), System.Windows.Forms.AxHost.State)
		Me.SSPanel16.Size = New System.Drawing.Size(104, 19)
		Me.SSPanel16.Location = New System.Drawing.Point(9, 19)
		Me.SSPanel16.TabIndex = 76
		Me.SSPanel16.Name = "SSPanel16"
		Me.txtMathVOffset.AutoSize = False
		Me.txtMathVOffset.Size = New System.Drawing.Size(79, 17)
		Me.txtMathVOffset.Location = New System.Drawing.Point(2, 1)
		Me.txtMathVOffset.TabIndex = 77
		Me.txtMathVOffset.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtMathVOffset.AcceptsReturn = True
		Me.txtMathVOffset.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtMathVOffset.BackColor = System.Drawing.SystemColors.Window
		Me.txtMathVOffset.CausesValidation = True
		Me.txtMathVOffset.Enabled = True
		Me.txtMathVOffset.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtMathVOffset.HideSelection = True
		Me.txtMathVOffset.ReadOnly = False
		Me.txtMathVOffset.Maxlength = 0
		Me.txtMathVOffset.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtMathVOffset.MultiLine = False
		Me.txtMathVOffset.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtMathVOffset.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtMathVOffset.TabStop = True
		Me.txtMathVOffset.Visible = True
		Me.txtMathVOffset.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.txtMathVOffset.Name = "txtMathVOffset"
		spnMathVOffset.OcxState = CType(resources.GetObject("spnMathVOffset.OcxState"), System.Windows.Forms.AxHost.State)
		Me.spnMathVOffset.Size = New System.Drawing.Size(21, 17)
		Me.spnMathVOffset.Location = New System.Drawing.Point(82, 1)
		Me.spnMathVOffset.TabIndex = 78
		Me.spnMathVOffset.Name = "spnMathVOffset"
		Me.Line3.BackColor = System.Drawing.SystemColors.WindowText
		Me.Line3.Visible = True
		Me.Line3.Location = New System.Drawing.Point(169, 7)
		Me.Line3.Size = New System.Drawing.Size(109, 1)
		Me.Line3.Name = "Line3"
		Me.Line2.BackColor = System.Drawing.SystemColors.WindowText
		Me.Line2.Visible = True
		Me.Line2.Location = New System.Drawing.Point(1, 6)
		Me.Line2.Size = New System.Drawing.Size(109, 1)
		Me.Line2.Name = "Line2"
		Me.Label2.Text = "Math"
		Me.Label2.Size = New System.Drawing.Size(31, 15)
		Me.Label2.Location = New System.Drawing.Point(126, 0)
		Me.Label2.TabIndex = 79
		Me.Label2.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label2.BackColor = System.Drawing.SystemColors.Control
		Me.Label2.Enabled = True
		Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label2.UseMnemonic = True
		Me.Label2.Visible = True
		Me.Label2.AutoSize = False
		Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label2.Name = "Label2"
		Me._tabMainFunctions_TabPage6.Text = "Options       "
		fraProbeComp.OcxState = CType(resources.GetObject("fraProbeComp.OcxState"), System.Windows.Forms.AxHost.State)
		Me.fraProbeComp.Size = New System.Drawing.Size(116, 69)
		Me.fraProbeComp.Location = New System.Drawing.Point(8, 192)
		Me.fraProbeComp.TabIndex = 215
		Me.fraProbeComp.Name = "fraProbeComp"
		fraOnOff.OcxState = CType(resources.GetObject("fraOnOff.OcxState"), System.Windows.Forms.AxHost.State)
		Me.fraOnOff.Size = New System.Drawing.Size(103, 40)
		Me.fraOnOff.Location = New System.Drawing.Point(8, 20)
		Me.fraOnOff.TabIndex = 216
		Me.fraOnOff.Name = "fraOnOff"
		panWaveDisplay.OcxState = CType(resources.GetObject("panWaveDisplay.OcxState"), System.Windows.Forms.AxHost.State)
		Me.panWaveDisplay.Size = New System.Drawing.Size(40, 32)
		Me.panWaveDisplay.Location = New System.Drawing.Point(2, 6)
		Me.panWaveDisplay.TabIndex = 217
		Me.panWaveDisplay.Name = "panWaveDisplay"
		Me.imgProbeComp.Size = New System.Drawing.Size(32, 24)
		Me.imgProbeComp.Location = New System.Drawing.Point(4, 4)
		Me.imgProbeComp.Image = CType(resources.GetObject("imgProbeComp.Image"), System.Drawing.Image)
		Me.imgProbeComp.Visible = False
		Me.imgProbeComp.Enabled = True
		Me.imgProbeComp.Cursor = System.Windows.Forms.Cursors.Default
		Me.imgProbeComp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Normal
		Me.imgProbeComp.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.imgProbeComp.Name = "imgProbeComp"
		ribCompOFF.OcxState = CType(resources.GetObject("ribCompOFF.OcxState"), System.Windows.Forms.AxHost.State)
		Me.ribCompOFF.Size = New System.Drawing.Size(30, 30)
		Me.ribCompOFF.Location = New System.Drawing.Point(70, 7)
		Me.ribCompOFF.TabIndex = 218
		Me.ribCompOFF.Name = "ribCompOFF"
		ribCompON.OcxState = CType(resources.GetObject("ribCompON.OcxState"), System.Windows.Forms.AxHost.State)
		Me.ribCompON.Size = New System.Drawing.Size(30, 30)
		Me.ribCompON.Location = New System.Drawing.Point(41, 7)
		Me.ribCompON.TabIndex = 219
		Me.ribCompON.Name = "ribCompON"
		fraOutputTrig.OcxState = CType(resources.GetObject("fraOutputTrig.OcxState"), System.Windows.Forms.AxHost.State)
		Me.fraOutputTrig.Size = New System.Drawing.Size(127, 77)
		Me.fraOutputTrig.Location = New System.Drawing.Point(8, 108)
		Me.fraOutputTrig.TabIndex = 58
		Me.fraOutputTrig.Name = "fraOutputTrig"
		chkECL0.OcxState = CType(resources.GetObject("chkECL0.OcxState"), System.Windows.Forms.AxHost.State)
		Me.chkECL0.Size = New System.Drawing.Size(105, 17)
		Me.chkECL0.Location = New System.Drawing.Point(8, 32)
		Me.chkECL0.TabIndex = 61
		Me.chkECL0.Name = "chkECL0"
		chkExternalOutput.OcxState = CType(resources.GetObject("chkExternalOutput.OcxState"), System.Windows.Forms.AxHost.State)
		Me.chkExternalOutput.Size = New System.Drawing.Size(105, 17)
		Me.chkExternalOutput.Location = New System.Drawing.Point(8, 16)
		Me.chkExternalOutput.TabIndex = 60
		Me.chkExternalOutput.Name = "chkExternalOutput"
		chkECL1.OcxState = CType(resources.GetObject("chkECL1.OcxState"), System.Windows.Forms.AxHost.State)
		Me.chkECL1.Size = New System.Drawing.Size(105, 17)
		Me.chkECL1.Location = New System.Drawing.Point(8, 50)
		Me.chkECL1.TabIndex = 59
		Me.chkECL1.Name = "chkECL1"
		fraExtTriggerCoup.OcxState = CType(resources.GetObject("fraExtTriggerCoup.OcxState"), System.Windows.Forms.AxHost.State)
		Me.fraExtTriggerCoup.Size = New System.Drawing.Size(127, 65)
		Me.fraExtTriggerCoup.Location = New System.Drawing.Point(8, 44)
		Me.fraExtTriggerCoup.TabIndex = 55
		Me.fraExtTriggerCoup.Name = "fraExtTriggerCoup"
		_optExtTrigCoup_0.OcxState = CType(resources.GetObject("_optExtTrigCoup_0.OcxState"), System.Windows.Forms.AxHost.State)
		Me._optExtTrigCoup_0.Size = New System.Drawing.Size(85, 17)
		Me._optExtTrigCoup_0.Location = New System.Drawing.Point(8, 21)
		Me._optExtTrigCoup_0.TabIndex = 57
		Me._optExtTrigCoup_0.Name = "_optExtTrigCoup_0"
		_optExtTrigCoup_1.OcxState = CType(resources.GetObject("_optExtTrigCoup_1.OcxState"), System.Windows.Forms.AxHost.State)
		Me._optExtTrigCoup_1.Size = New System.Drawing.Size(88, 17)
		Me._optExtTrigCoup_1.Location = New System.Drawing.Point(8, 41)
		Me._optExtTrigCoup_1.TabIndex = 56
		Me._optExtTrigCoup_1.Name = "_optExtTrigCoup_1"
		fraTraceColor.OcxState = CType(resources.GetObject("fraTraceColor.OcxState"), System.Windows.Forms.AxHost.State)
		Me.fraTraceColor.Size = New System.Drawing.Size(139, 77)
		Me.fraTraceColor.Location = New System.Drawing.Point(142, 108)
		Me.fraTraceColor.TabIndex = 52
		Me.fraTraceColor.Name = "fraTraceColor"
		Me.cboTraceColor.BackColor = System.Drawing.Color.White
		Me.cboTraceColor.Size = New System.Drawing.Size(89, 21)
		Me.cboTraceColor.Location = New System.Drawing.Point(8, 32)
		Me.cboTraceColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cboTraceColor.TabIndex = 54
		Me.cboTraceColor.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cboTraceColor.CausesValidation = True
		Me.cboTraceColor.Enabled = True
		Me.cboTraceColor.ForeColor = System.Drawing.SystemColors.WindowText
		Me.cboTraceColor.IntegralHeight = True
		Me.cboTraceColor.Cursor = System.Windows.Forms.Cursors.Default
		Me.cboTraceColor.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cboTraceColor.Sorted = False
		Me.cboTraceColor.TabStop = True
		Me.cboTraceColor.Visible = True
		Me.cboTraceColor.Name = "cboTraceColor"
		Me.picTraceColor.BackColor = System.Drawing.Color.Yellow
		Me.picTraceColor.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.picTraceColor.Size = New System.Drawing.Size(20, 21)
		Me.picTraceColor.Location = New System.Drawing.Point(108, 32)
		Me.picTraceColor.TabIndex = 53
		Me.picTraceColor.Dock = System.Windows.Forms.DockStyle.None
		Me.picTraceColor.CausesValidation = True
		Me.picTraceColor.Enabled = True
		Me.picTraceColor.ForeColor = System.Drawing.SystemColors.ControlText
		Me.picTraceColor.Cursor = System.Windows.Forms.Cursors.Default
		Me.picTraceColor.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.picTraceColor.TabStop = True
		Me.picTraceColor.Visible = True
		Me.picTraceColor.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Normal
		Me.picTraceColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.picTraceColor.Name = "picTraceColor"
		fraTraceThickness.OcxState = CType(resources.GetObject("fraTraceThickness.OcxState"), System.Windows.Forms.AxHost.State)
		Me.fraTraceThickness.Size = New System.Drawing.Size(139, 65)
		Me.fraTraceThickness.Location = New System.Drawing.Point(142, 44)
		Me.fraTraceThickness.TabIndex = 50
		Me.fraTraceThickness.Name = "fraTraceThickness"
		Me.cboTraceThickness.BackColor = System.Drawing.Color.White
		Me.cboTraceThickness.Size = New System.Drawing.Size(121, 21)
		Me.cboTraceThickness.Location = New System.Drawing.Point(8, 28)
		Me.cboTraceThickness.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cboTraceThickness.TabIndex = 51
		Me.cboTraceThickness.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cboTraceThickness.CausesValidation = True
		Me.cboTraceThickness.Enabled = True
		Me.cboTraceThickness.ForeColor = System.Drawing.SystemColors.WindowText
		Me.cboTraceThickness.IntegralHeight = True
		Me.cboTraceThickness.Cursor = System.Windows.Forms.Cursors.Default
		Me.cboTraceThickness.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cboTraceThickness.Sorted = False
		Me.cboTraceThickness.TabStop = True
		Me.cboTraceThickness.Visible = True
		Me.cboTraceThickness.Name = "cboTraceThickness"
		Me.cmdAbout.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		Me.cmdAbout.Text = "About"
		Me.cmdAbout.Size = New System.Drawing.Size(75, 23)
		Me.cmdAbout.Location = New System.Drawing.Point(132, 237)
		Me.cmdAbout.TabIndex = 211
		Me.cmdAbout.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdAbout.BackColor = System.Drawing.SystemColors.Control
		Me.cmdAbout.CausesValidation = True
		Me.cmdAbout.Enabled = True
		Me.cmdAbout.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdAbout.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdAbout.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdAbout.TabStop = True
		Me.cmdAbout.Name = "cmdAbout"
		Me.cmdUpdateTIP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		Me.cmdUpdateTIP.Text = "&Update TIP"
		Me.cmdUpdateTIP.Size = New System.Drawing.Size(75, 23)
		Me.cmdUpdateTIP.Location = New System.Drawing.Point(213, 204)
		Me.cmdUpdateTIP.TabIndex = 212
		Me.cmdUpdateTIP.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdUpdateTIP.BackColor = System.Drawing.SystemColors.Control
		Me.cmdUpdateTIP.CausesValidation = True
		Me.cmdUpdateTIP.Enabled = True
		Me.cmdUpdateTIP.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdUpdateTIP.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdUpdateTIP.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdUpdateTIP.TabStop = True
		Me.cmdUpdateTIP.Name = "cmdUpdateTIP"
		Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		Me.cmdPrint.Text = "Print Display"
		Me.cmdPrint.Size = New System.Drawing.Size(75, 23)
		Me.cmdPrint.Location = New System.Drawing.Point(213, 237)
		Me.cmdPrint.TabIndex = 213
		Me.cmdPrint.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
		Me.cmdPrint.CausesValidation = True
		Me.cmdPrint.Enabled = True
		Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdPrint.TabStop = True
		Me.cmdPrint.Name = "cmdPrint"
		Me.cmdSelfTest.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		Me.cmdSelfTest.Text = "Built-In Test"
		Me.cmdSelfTest.Size = New System.Drawing.Size(75, 23)
		Me.cmdSelfTest.Location = New System.Drawing.Point(132, 204)
		Me.cmdSelfTest.TabIndex = 214
		Me.cmdSelfTest.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdSelfTest.BackColor = System.Drawing.SystemColors.Control
		Me.cmdSelfTest.CausesValidation = True
		Me.cmdSelfTest.Enabled = True
		Me.cmdSelfTest.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdSelfTest.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdSelfTest.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdSelfTest.TabStop = True
		Me.cmdSelfTest.Name = "cmdSelfTest"
		Panel_Conifg.OcxState = CType(resources.GetObject("Panel_Conifg.OcxState"), System.Windows.Forms.AxHost.State)
		Me.Panel_Conifg.Size = New System.Drawing.Size(129, 137)
		Me.Panel_Conifg.Location = New System.Drawing.Point(288, 44)
		Me.Panel_Conifg.TabIndex = 220
		Me.Panel_Conifg.Name = "Panel_Conifg"
		Me._tabMainFunctions_TabPage7.Text = "ATLAS    "
		Atlas_SFP.OcxState = CType(resources.GetObject("Atlas_SFP.OcxState"), System.Windows.Forms.AxHost.State)
		Me.Atlas_SFP.Size = New System.Drawing.Size(393, 209)
		Me.Atlas_SFP.Location = New System.Drawing.Point(16, 56)
		Me.Atlas_SFP.TabIndex = 221
		Me.Atlas_SFP.Name = "Atlas_SFP"
		Me.cmdAutoscale.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		Me.cmdAutoscale.Text = "&Autoscale"
		Me.cmdAutoscale.Size = New System.Drawing.Size(75, 23)
		Me.cmdAutoscale.Location = New System.Drawing.Point(312, 292)
		Me.cmdAutoscale.TabIndex = 93
		Me.cmdAutoscale.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdAutoscale.BackColor = System.Drawing.SystemColors.Control
		Me.cmdAutoscale.CausesValidation = True
		Me.cmdAutoscale.Enabled = True
		Me.cmdAutoscale.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdAutoscale.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdAutoscale.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdAutoscale.TabStop = True
		Me.cmdAutoscale.Name = "cmdAutoscale"
		Me.cmdMeasure.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		Me.cmdMeasure.Text = "&Measure"
		Me.cmdMeasure.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdMeasure.Size = New System.Drawing.Size(75, 23)
		Me.cmdMeasure.Location = New System.Drawing.Point(228, 292)
		Me.cmdMeasure.TabIndex = 92
		Me.cmdMeasure.BackColor = System.Drawing.SystemColors.Control
		Me.cmdMeasure.CausesValidation = True
		Me.cmdMeasure.Enabled = True
		Me.cmdMeasure.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdMeasure.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdMeasure.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdMeasure.TabStop = True
		Me.cmdMeasure.Name = "cmdMeasure"
		Me.cmdQuit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		Me.cmdQuit.Text = "&Quit"
		Me.cmdQuit.Size = New System.Drawing.Size(75, 23)
		Me.cmdQuit.Location = New System.Drawing.Point(545, 292)
		Me.cmdQuit.TabIndex = 49
		Me.cmdQuit.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdQuit.BackColor = System.Drawing.SystemColors.Control
		Me.cmdQuit.CausesValidation = True
		Me.cmdQuit.Enabled = True
		Me.cmdQuit.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdQuit.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdQuit.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdQuit.TabStop = True
		Me.cmdQuit.Name = "cmdQuit"
		Me.cmdReset.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		Me.cmdReset.Text = "&Reset"
		Me.cmdReset.Size = New System.Drawing.Size(75, 23)
		Me.cmdReset.Location = New System.Drawing.Point(464, 292)
		Me.cmdReset.TabIndex = 48
		Me.cmdReset.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdReset.BackColor = System.Drawing.SystemColors.Control
		Me.cmdReset.CausesValidation = True
		Me.cmdReset.Enabled = True
		Me.cmdReset.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdReset.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdReset.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdReset.TabStop = True
		Me.cmdReset.Name = "cmdReset"
		panDmmDisplay.OcxState = CType(resources.GetObject("panDmmDisplay.OcxState"), System.Windows.Forms.AxHost.State)
		Me.panDmmDisplay.Size = New System.Drawing.Size(297, 42)
		Me.panDmmDisplay.Location = New System.Drawing.Point(8, 8)
		Me.panDmmDisplay.TabIndex = 2
		Me.panDmmDisplay.Name = "panDmmDisplay"
		Me.txtDataDisplay.BackColor = System.Drawing.Color.Black
		Me.txtDataDisplay.Text = "Ready..."
		Me.txtDataDisplay.Font = New System.Drawing.Font("Arial", 18!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtDataDisplay.ForeColor = System.Drawing.Color.Green
		Me.txtDataDisplay.Size = New System.Drawing.Size(287, 32)
		Me.txtDataDisplay.Location = New System.Drawing.Point(5, 5)
		Me.txtDataDisplay.TabIndex = 35
		Me.txtDataDisplay.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.txtDataDisplay.Enabled = True
		Me.txtDataDisplay.Cursor = System.Windows.Forms.Cursors.Default
		Me.txtDataDisplay.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtDataDisplay.UseMnemonic = True
		Me.txtDataDisplay.Visible = True
		Me.txtDataDisplay.AutoSize = False
		Me.txtDataDisplay.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.txtDataDisplay.Name = "txtDataDisplay"
		panScopeDisplay.OcxState = CType(resources.GetObject("panScopeDisplay.OcxState"), System.Windows.Forms.AxHost.State)
		Me.panScopeDisplay.Size = New System.Drawing.Size(297, 225)
		Me.panScopeDisplay.Location = New System.Drawing.Point(8, 56)
		Me.panScopeDisplay.TabIndex = 3
		Me.panScopeDisplay.Name = "panScopeDisplay"
		cwgScopeDisplay.OcxState = CType(resources.GetObject("cwgScopeDisplay.OcxState"), System.Windows.Forms.AxHost.State)
		Me.cwgScopeDisplay.Size = New System.Drawing.Size(287, 215)
		Me.cwgScopeDisplay.Location = New System.Drawing.Point(5, 5)
		Me.cwgScopeDisplay.TabIndex = 4
		Me.cwgScopeDisplay.Name = "cwgScopeDisplay"
		StatusBar1.OcxState = CType(resources.GetObject("StatusBar1.OcxState"), System.Windows.Forms.AxHost.State)
		Me.StatusBar1.Dock = System.Windows.Forms.DockStyle.Bottom
		Me.StatusBar1.Size = New System.Drawing.Size(758, 17)
		Me.StatusBar1.Location = New System.Drawing.Point(0, 321)
		Me.StatusBar1.TabIndex = 1
		Me.StatusBar1.Name = "StatusBar1"
		chkContinuous.OcxState = CType(resources.GetObject("chkContinuous.OcxState"), System.Windows.Forms.AxHost.State)
		Me.chkContinuous.Size = New System.Drawing.Size(73, 17)
		Me.chkContinuous.Location = New System.Drawing.Point(148, 296)
		Me.chkContinuous.TabIndex = 94
		Me.chkContinuous.Name = "chkContinuous"
		Me.CommonDialog1Open.DefaultExt = "dat"
		Me.CommonDialog1Open.Title = "SFP File I/O"
		Me.CommonDialog1Open.FileName = "*.dat"
		Me.CommonDialog1Open.Filter = "*.dat"
		Me.CommonDialog1Open.FilterIndex = 1
		Me.imgLogo.Size = New System.Drawing.Size(137, 33)
		Me.imgLogo.Location = New System.Drawing.Point(8, 283)
		Me.imgLogo.Image = CType(resources.GetObject("imgLogo.Image"), System.Drawing.Image)
		Me.imgLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
		Me.imgLogo.Enabled = True
		Me.imgLogo.Cursor = System.Windows.Forms.Cursors.Default
		Me.imgLogo.Visible = True
		Me.imgLogo.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.imgLogo.Name = "imgLogo"
		imgFunctions.OcxState = CType(resources.GetObject("imgFunctions.OcxState"), System.Windows.Forms.AxHost.State)
		Me.imgFunctions.Location = New System.Drawing.Point(0, 0)
		Me.imgFunctions.Name = "imgFunctions"
		Me.Controls.Add(cmdDisplaySize)
		Me.Controls.Add(tabMainFunctions)
		Me.Controls.Add(cmdAutoscale)
		Me.Controls.Add(cmdMeasure)
		Me.Controls.Add(cmdQuit)
		Me.Controls.Add(cmdReset)
		Me.Controls.Add(panDmmDisplay)
		Me.Controls.Add(panScopeDisplay)
		Me.Controls.Add(StatusBar1)
		Me.Controls.Add(chkContinuous)
		Me.Controls.Add(imgLogo)
		Me.Controls.Add(imgFunctions)
		Me.tabMainFunctions.Controls.Add(_tabMainFunctions_TabPage0)
		Me.tabMainFunctions.Controls.Add(_tabMainFunctions_TabPage1)
		Me.tabMainFunctions.Controls.Add(_tabMainFunctions_TabPage2)
		Me.tabMainFunctions.Controls.Add(_tabMainFunctions_TabPage3)
		Me.tabMainFunctions.Controls.Add(_tabMainFunctions_TabPage4)
		Me.tabMainFunctions.Controls.Add(_tabMainFunctions_TabPage5)
		Me.tabMainFunctions.Controls.Add(_tabMainFunctions_TabPage6)
		Me.tabMainFunctions.Controls.Add(_tabMainFunctions_TabPage7)
		Me._tabMainFunctions_TabPage0.Controls.Add(fraHoldOff)
		Me._tabMainFunctions_TabPage0.Controls.Add(fraDealy)
		Me._tabMainFunctions_TabPage0.Controls.Add(fraHorizRange)
		Me._tabMainFunctions_TabPage0.Controls.Add(fraCompletion)
		Me._tabMainFunctions_TabPage0.Controls.Add(fraDataPoints)
		Me._tabMainFunctions_TabPage0.Controls.Add(fraReference)
		Me._tabMainFunctions_TabPage0.Controls.Add(fraAvgCount)
		Me._tabMainFunctions_TabPage0.Controls.Add(fraSampleType)
		Me._tabMainFunctions_TabPage0.Controls.Add(fraAcqMode)
		Me._tabMainFunctions_TabPage0.Controls.Add(fraDisplay)
		Me.fraHoldOff.Controls.Add(SSPanel7)
		Me.fraHoldOff.Controls.Add(_optHoldOff_1)
		Me.fraHoldOff.Controls.Add(_optHoldOff_0)
		Me.fraHoldOff.Controls.Add(lblEvents)
		Me.SSPanel7.Controls.Add(txtHoldOff)
		Me.SSPanel7.Controls.Add(spnHoldOff)
		Me.fraDealy.Controls.Add(SSPanel6)
		Me.SSPanel6.Controls.Add(txtDelay)
		Me.SSPanel6.Controls.Add(spnDelay)
		Me.fraHorizRange.Controls.Add(cboHorizRange)
		Me.fraCompletion.Controls.Add(SSPanel9)
		Me.SSPanel9.Controls.Add(txtCompletion)
		Me.SSPanel9.Controls.Add(spnCompletion)
		Me.fraDataPoints.Controls.Add(_optDataPoints_0)
		Me.fraDataPoints.Controls.Add(_optDataPoints_1)
		Me.fraReference.Controls.Add(_optReference_2)
		Me.fraReference.Controls.Add(_optReference_0)
		Me.fraReference.Controls.Add(_optReference_1)
		Me.fraAvgCount.Controls.Add(SSPanel8)
		Me.SSPanel8.Controls.Add(txtAvgCount)
		Me.SSPanel8.Controls.Add(spnAvgCount)
		Me.fraSampleType.Controls.Add(cboSampleType)
		Me.fraAcqMode.Controls.Add(cboAcqMode)
		Me.fraDisplay.Controls.Add(_chkDisplayTrace_0)
		Me.fraDisplay.Controls.Add(_chkDisplayTrace_1)
		Me.fraDisplay.Controls.Add(_chkDisplayTrace_2)
		Me.fraDisplay.Controls.Add(_chkDisplayTrace_3)
		Me._tabMainFunctions_TabPage1.Controls.Add(_cmdSaveToDisk_0)
		Me._tabMainFunctions_TabPage1.Controls.Add(_cmdSaveToMem_0)
		Me._tabMainFunctions_TabPage1.Controls.Add(_fraSignalFilters_0)
		Me._tabMainFunctions_TabPage1.Controls.Add(_fraProbeAttenuation_0)
		Me._tabMainFunctions_TabPage1.Controls.Add(_fraVerticalRange_0)
		Me._tabMainFunctions_TabPage1.Controls.Add(_fraCoupling_0)
		Me._tabMainFunctions_TabPage1.Controls.Add(fraOffset1)
		Me._fraSignalFilters_0.Controls.Add(chkLFReject1)
		Me._fraSignalFilters_0.Controls.Add(chkHFReject1)
		Me._fraProbeAttenuation_0.Controls.Add(_optProbe1_3)
		Me._fraProbeAttenuation_0.Controls.Add(_optProbe1_2)
		Me._fraProbeAttenuation_0.Controls.Add(_optProbe1_1)
		Me._fraProbeAttenuation_0.Controls.Add(_optProbe1_0)
		Me._fraVerticalRange_0.Controls.Add(_cboVertRange_0)
		Me._fraCoupling_0.Controls.Add(_optCoupling1_1)
		Me._fraCoupling_0.Controls.Add(_optCoupling1_0)
		Me._fraCoupling_0.Controls.Add(_optCoupling1_2)
		Me.fraOffset1.Controls.Add(SSPanel10)
		Me.SSPanel10.Controls.Add(txtVOffset1)
		Me.SSPanel10.Controls.Add(spnVOffset1)
		Me._tabMainFunctions_TabPage2.Controls.Add(fraOffset2)
		Me._tabMainFunctions_TabPage2.Controls.Add(_fraCoupling_1)
		Me._tabMainFunctions_TabPage2.Controls.Add(_fraVerticalRange_1)
		Me._tabMainFunctions_TabPage2.Controls.Add(_fraProbeAttenuation_1)
		Me._tabMainFunctions_TabPage2.Controls.Add(_fraSignalFilters_1)
		Me._tabMainFunctions_TabPage2.Controls.Add(_cmdSaveToMem_1)
		Me._tabMainFunctions_TabPage2.Controls.Add(_cmdSaveToDisk_1)
		Me.fraOffset2.Controls.Add(SSPanel11)
		Me.SSPanel11.Controls.Add(txtVOffset2)
		Me.SSPanel11.Controls.Add(spnVOffset2)
		Me._fraCoupling_1.Controls.Add(_optCoupling2_2)
		Me._fraCoupling_1.Controls.Add(_optCoupling2_0)
		Me._fraCoupling_1.Controls.Add(_optCoupling2_1)
		Me._fraVerticalRange_1.Controls.Add(_cboVertRange_1)
		Me._fraProbeAttenuation_1.Controls.Add(_optProbe2_0)
		Me._fraProbeAttenuation_1.Controls.Add(_optProbe2_1)
		Me._fraProbeAttenuation_1.Controls.Add(_optProbe2_2)
		Me._fraProbeAttenuation_1.Controls.Add(_optProbe2_3)
		Me._fraSignalFilters_1.Controls.Add(chkHFReject2)
		Me._fraSignalFilters_1.Controls.Add(chkLFReject2)
		Me._tabMainFunctions_TabPage3.Controls.Add(_fraFunction_2)
		Me._tabMainFunctions_TabPage3.Controls.Add(cmdHelp)
		Me._fraFunction_2.Controls.Add(tbrMeasFunction)
		Me._fraFunction_2.Controls.Add(cboMeasSour)
		Me._fraFunction_2.Controls.Add(fraDelayParam)
		Me.fraDelayParam.Controls.Add(cboStopSource)
		Me.fraDelayParam.Controls.Add(cboStartSource)
		Me.fraDelayParam.Controls.Add(Picture2)
		Me.fraDelayParam.Controls.Add(Picture3)
		Me.fraDelayParam.Controls.Add(Picture4)
		Me.fraDelayParam.Controls.Add(SSPanel1)
		Me.fraDelayParam.Controls.Add(SSPanel3)
		Me.fraDelayParam.Controls.Add(SSPanel4)
		Me.fraDelayParam.Controls.Add(SSPanel5)
		Me.fraDelayParam.Controls.Add(Label1)
		Me.fraDelayParam.Controls.Add(lblStopEdgeCap)
		Me.fraDelayParam.Controls.Add(lblStopSource)
		Me.fraDelayParam.Controls.Add(lblStartEdgeCap)
		Me.fraDelayParam.Controls.Add(Line1)
		Me.fraDelayParam.Controls.Add(lblStop)
		Me.fraDelayParam.Controls.Add(lblStar)
		Me.fraDelayParam.Controls.Add(lblStartSource)
		Me.Picture2.Controls.Add(_optStartSlope_0)
		Me.Picture2.Controls.Add(_optStartSlope_1)
		Me.Picture2.Controls.Add(lblStartSlope)
		Me.Picture3.Controls.Add(_optStopSlope_0)
		Me.Picture3.Controls.Add(_optStopSlope_1)
		Me.Picture3.Controls.Add(lblStopSlope)
		Me.Picture4.Controls.Add(_optStartLevel_0)
		Me.Picture4.Controls.Add(_optStartLevel_1)
		Me.Picture4.Controls.Add(lblStartLevelCap)
		Me.SSPanel1.Controls.Add(txtStartEdge)
		Me.SSPanel1.Controls.Add(spnStartEdge)
		Me.SSPanel3.Controls.Add(txtStartLevel)
		Me.SSPanel3.Controls.Add(spnStartLevel)
		Me.SSPanel4.Controls.Add(txtStopEdge)
		Me.SSPanel4.Controls.Add(spnStopEdge)
		Me.SSPanel5.Controls.Add(txtStopLevel)
		Me.SSPanel5.Controls.Add(spnStopLevel)
		Me._tabMainFunctions_TabPage4.Controls.Add(fraLine)
		Me._tabMainFunctions_TabPage4.Controls.Add(fraField)
		Me._tabMainFunctions_TabPage4.Controls.Add(fraStandard)
		Me._tabMainFunctions_TabPage4.Controls.Add(fraOccurrence)
		Me._tabMainFunctions_TabPage4.Controls.Add(fraDelaySlope)
		Me._tabMainFunctions_TabPage4.Controls.Add(fraTrigDelaySource)
		Me._tabMainFunctions_TabPage4.Controls.Add(fraTrigDelay)
		Me._tabMainFunctions_TabPage4.Controls.Add(fraTrigLevel)
		Me._tabMainFunctions_TabPage4.Controls.Add(fraNoiseRej)
		Me._tabMainFunctions_TabPage4.Controls.Add(fraTrigSlope)
		Me._tabMainFunctions_TabPage4.Controls.Add(fraTrigSour)
		Me._tabMainFunctions_TabPage4.Controls.Add(fraTrigMode)
		Me.fraLine.Controls.Add(SSPanel14)
		Me.SSPanel14.Controls.Add(txtLine)
		Me.SSPanel14.Controls.Add(spnLine)
		Me.fraField.Controls.Add(_optField_0)
		Me.fraField.Controls.Add(_optField_1)
		Me.fraStandard.Controls.Add(_optStandard_0)
		Me.fraStandard.Controls.Add(_optStandard_1)
		Me.fraOccurrence.Controls.Add(SSPanel15)
		Me.SSPanel15.Controls.Add(txtOccurrence)
		Me.SSPanel15.Controls.Add(spnOccurrence)
		Me.fraDelaySlope.Controls.Add(_optDelaySlope_0)
		Me.fraDelaySlope.Controls.Add(_optDelaySlope_1)
		Me.fraTrigDelaySource.Controls.Add(cboTrigDelaySour)
		Me.fraTrigDelay.Controls.Add(SSPanel13)
		Me.fraTrigDelay.Controls.Add(_optTrigDelay_1)
		Me.fraTrigDelay.Controls.Add(_optTrigDelay_0)
		Me.fraTrigDelay.Controls.Add(lblTrigDelayEvents)
		Me.SSPanel13.Controls.Add(txtDelEvents)
		Me.SSPanel13.Controls.Add(spnDelEvents)
		Me.fraTrigLevel.Controls.Add(SSPanel12)
		Me.SSPanel12.Controls.Add(txtTrigLevel)
		Me.SSPanel12.Controls.Add(spnTrigLevel)
		Me.fraNoiseRej.Controls.Add(_optNoiseRej_1)
		Me.fraNoiseRej.Controls.Add(_optNoiseRej_0)
		Me.fraTrigSlope.Controls.Add(_optTrigSlope_1)
		Me.fraTrigSlope.Controls.Add(_optTrigSlope_0)
		Me.fraTrigSour.Controls.Add(cboTrigSour)
		Me.fraTrigMode.Controls.Add(_optTrigMode_1)
		Me.fraTrigMode.Controls.Add(_optTrigMode_0)
		Me.fraTrigMode.Controls.Add(_optTrigMode_2)
		Me._tabMainFunctions_TabPage5.Controls.Add(picMemOptions)
		Me._tabMainFunctions_TabPage5.Controls.Add(picMathOptions)
		Me.picMemOptions.Controls.Add(cmdLoadFromDisk)
		Me.picMemOptions.Controls.Add(_cmdSaveToDisk_3)
		Me.picMemOptions.Controls.Add(fraMemVerticalRange)
		Me.picMemOptions.Controls.Add(SSFrame2)
		Me.picMemOptions.Controls.Add(Line5)
		Me.picMemOptions.Controls.Add(Line4)
		Me.picMemOptions.Controls.Add(Label3)
		Me.fraMemVerticalRange.Controls.Add(SSPanel19)
		Me.SSPanel19.Controls.Add(txtMemoryRange)
		Me.SSPanel19.Controls.Add(SpinButton2)
		Me.SSFrame2.Controls.Add(SSPanel17)
		Me.SSPanel17.Controls.Add(txtMemVoltOffset)
		Me.SSPanel17.Controls.Add(spnMemVOffset)
		Me.picMathOptions.Controls.Add(_cmdSaveToMem_2)
		Me.picMathOptions.Controls.Add(_cmdSaveToDisk_2)
		Me.picMathOptions.Controls.Add(fraRange)
		Me.picMathOptions.Controls.Add(fraMathSourceB)
		Me.picMathOptions.Controls.Add(fraOperation)
		Me.picMathOptions.Controls.Add(fraMemVRange)
		Me.picMathOptions.Controls.Add(SSFrame1)
		Me.picMathOptions.Controls.Add(Line3)
		Me.picMathOptions.Controls.Add(Line2)
		Me.picMathOptions.Controls.Add(Label2)
		Me.fraRange.Controls.Add(cboMathSourceA)
		Me.fraMathSourceB.Controls.Add(cboMathSourceB)
		Me.fraOperation.Controls.Add(cboMathFunction)
		Me.fraMemVRange.Controls.Add(SSPanel18)
		Me.SSPanel18.Controls.Add(txtMathRange)
		Me.SSPanel18.Controls.Add(SpinButton1)
		Me.SSFrame1.Controls.Add(SSPanel16)
		Me.SSPanel16.Controls.Add(txtMathVOffset)
		Me.SSPanel16.Controls.Add(spnMathVOffset)
		Me._tabMainFunctions_TabPage6.Controls.Add(fraProbeComp)
		Me._tabMainFunctions_TabPage6.Controls.Add(fraOutputTrig)
		Me._tabMainFunctions_TabPage6.Controls.Add(fraExtTriggerCoup)
		Me._tabMainFunctions_TabPage6.Controls.Add(fraTraceColor)
		Me._tabMainFunctions_TabPage6.Controls.Add(fraTraceThickness)
		Me._tabMainFunctions_TabPage6.Controls.Add(cmdAbout)
		Me._tabMainFunctions_TabPage6.Controls.Add(cmdUpdateTIP)
		Me._tabMainFunctions_TabPage6.Controls.Add(cmdPrint)
		Me._tabMainFunctions_TabPage6.Controls.Add(cmdSelfTest)
		Me._tabMainFunctions_TabPage6.Controls.Add(Panel_Conifg)
		Me.fraProbeComp.Controls.Add(fraOnOff)
		Me.fraOnOff.Controls.Add(panWaveDisplay)
		Me.fraOnOff.Controls.Add(ribCompOFF)
		Me.fraOnOff.Controls.Add(ribCompON)
		Me.panWaveDisplay.Controls.Add(imgProbeComp)
		Me.fraOutputTrig.Controls.Add(chkECL0)
		Me.fraOutputTrig.Controls.Add(chkExternalOutput)
		Me.fraOutputTrig.Controls.Add(chkECL1)
		Me.fraExtTriggerCoup.Controls.Add(_optExtTrigCoup_0)
		Me.fraExtTriggerCoup.Controls.Add(_optExtTrigCoup_1)
		Me.fraTraceColor.Controls.Add(cboTraceColor)
		Me.fraTraceColor.Controls.Add(picTraceColor)
		Me.fraTraceThickness.Controls.Add(cboTraceThickness)
		Me._tabMainFunctions_TabPage7.Controls.Add(Atlas_SFP)
		Me.panDmmDisplay.Controls.Add(txtDataDisplay)
		Me.panScopeDisplay.Controls.Add(cwgScopeDisplay)
		Me.cboVertRange.SetIndex(_cboVertRange_1, CType(1, Short))
		Me.cboVertRange.SetIndex(_cboVertRange_0, CType(0, Short))
		Me.chkDisplayTrace.SetIndex(_chkDisplayTrace_0, CType(0, Short))
		Me.chkDisplayTrace.SetIndex(_chkDisplayTrace_1, CType(1, Short))
		Me.chkDisplayTrace.SetIndex(_chkDisplayTrace_2, CType(2, Short))
		Me.chkDisplayTrace.SetIndex(_chkDisplayTrace_3, CType(3, Short))
		Me.cmdSaveToDisk.SetIndex(_cmdSaveToDisk_1, CType(1, Short))
		Me.cmdSaveToDisk.SetIndex(_cmdSaveToDisk_0, CType(0, Short))
		Me.cmdSaveToDisk.SetIndex(_cmdSaveToDisk_3, CType(3, Short))
		Me.cmdSaveToDisk.SetIndex(_cmdSaveToDisk_2, CType(2, Short))
		Me.cmdSaveToMem.SetIndex(_cmdSaveToMem_1, CType(1, Short))
		Me.cmdSaveToMem.SetIndex(_cmdSaveToMem_0, CType(0, Short))
		Me.cmdSaveToMem.SetIndex(_cmdSaveToMem_2, CType(2, Short))
		Me.fraCoupling.SetIndex(_fraCoupling_1, CType(1, Short))
		Me.fraCoupling.SetIndex(_fraCoupling_0, CType(0, Short))
		Me.fraFunction.SetIndex(_fraFunction_2, CType(2, Short))
		Me.fraProbeAttenuation.SetIndex(_fraProbeAttenuation_1, CType(1, Short))
		Me.fraProbeAttenuation.SetIndex(_fraProbeAttenuation_0, CType(0, Short))
		Me.fraSignalFilters.SetIndex(_fraSignalFilters_1, CType(1, Short))
		Me.fraSignalFilters.SetIndex(_fraSignalFilters_0, CType(0, Short))
		Me.fraVerticalRange.SetIndex(_fraVerticalRange_1, CType(1, Short))
		Me.fraVerticalRange.SetIndex(_fraVerticalRange_0, CType(0, Short))
		Me.optCoupling1.SetIndex(_optCoupling1_1, CType(1, Short))
		Me.optCoupling1.SetIndex(_optCoupling1_0, CType(0, Short))
		Me.optCoupling1.SetIndex(_optCoupling1_2, CType(2, Short))
		Me.optCoupling2.SetIndex(_optCoupling2_2, CType(2, Short))
		Me.optCoupling2.SetIndex(_optCoupling2_0, CType(0, Short))
		Me.optCoupling2.SetIndex(_optCoupling2_1, CType(1, Short))
		Me.optDataPoints.SetIndex(_optDataPoints_0, CType(0, Short))
		Me.optDataPoints.SetIndex(_optDataPoints_1, CType(1, Short))
		Me.optDelaySlope.SetIndex(_optDelaySlope_0, CType(0, Short))
		Me.optDelaySlope.SetIndex(_optDelaySlope_1, CType(1, Short))
		Me.optExtTrigCoup.SetIndex(_optExtTrigCoup_0, CType(0, Short))
		Me.optExtTrigCoup.SetIndex(_optExtTrigCoup_1, CType(1, Short))
		Me.optField.SetIndex(_optField_0, CType(0, Short))
		Me.optField.SetIndex(_optField_1, CType(1, Short))
		Me.optHoldOff.SetIndex(_optHoldOff_1, CType(1, Short))
		Me.optHoldOff.SetIndex(_optHoldOff_0, CType(0, Short))
		Me.optNoiseRej.SetIndex(_optNoiseRej_1, CType(1, Short))
		Me.optNoiseRej.SetIndex(_optNoiseRej_0, CType(0, Short))
		Me.optProbe1.SetIndex(_optProbe1_3, CType(3, Short))
		Me.optProbe1.SetIndex(_optProbe1_2, CType(2, Short))
		Me.optProbe1.SetIndex(_optProbe1_1, CType(1, Short))
		Me.optProbe1.SetIndex(_optProbe1_0, CType(0, Short))
		Me.optProbe2.SetIndex(_optProbe2_0, CType(0, Short))
		Me.optProbe2.SetIndex(_optProbe2_1, CType(1, Short))
		Me.optProbe2.SetIndex(_optProbe2_2, CType(2, Short))
		Me.optProbe2.SetIndex(_optProbe2_3, CType(3, Short))
		Me.optReference.SetIndex(_optReference_2, CType(2, Short))
		Me.optReference.SetIndex(_optReference_0, CType(0, Short))
		Me.optReference.SetIndex(_optReference_1, CType(1, Short))
		Me.optStandard.SetIndex(_optStandard_0, CType(0, Short))
		Me.optStandard.SetIndex(_optStandard_1, CType(1, Short))
		Me.optStartLevel.SetIndex(_optStartLevel_0, CType(0, Short))
		Me.optStartLevel.SetIndex(_optStartLevel_1, CType(1, Short))
		Me.optStartSlope.SetIndex(_optStartSlope_0, CType(0, Short))
		Me.optStartSlope.SetIndex(_optStartSlope_1, CType(1, Short))
		Me.optStopSlope.SetIndex(_optStopSlope_0, CType(0, Short))
		Me.optStopSlope.SetIndex(_optStopSlope_1, CType(1, Short))
		Me.optTrigDelay.SetIndex(_optTrigDelay_1, CType(1, Short))
		Me.optTrigDelay.SetIndex(_optTrigDelay_0, CType(0, Short))
		Me.optTrigMode.SetIndex(_optTrigMode_1, CType(1, Short))
		Me.optTrigMode.SetIndex(_optTrigMode_0, CType(0, Short))
		Me.optTrigMode.SetIndex(_optTrigMode_2, CType(2, Short))
		Me.optTrigSlope.SetIndex(_optTrigSlope_1, CType(1, Short))
		Me.optTrigSlope.SetIndex(_optTrigSlope_0, CType(0, Short))
		CType(Me.optTrigSlope, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.optTrigMode, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.optTrigDelay, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.optStopSlope, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.optStartSlope, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.optStartLevel, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.optStandard, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.optReference, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.optProbe2, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.optProbe1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.optNoiseRej, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.optHoldOff, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.optField, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.optExtTrigCoup, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.optDelaySlope, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.optDataPoints, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.optCoupling2, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.optCoupling1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.fraVerticalRange, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.fraSignalFilters, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.fraProbeAttenuation, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.fraFunction, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.fraCoupling, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.cmdSaveToMem, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.cmdSaveToDisk, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.chkDisplayTrace, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.cboVertRange, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.imgFunctions, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.chkContinuous, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.StatusBar1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.panScopeDisplay, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.cwgScopeDisplay, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.panDmmDisplay, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.Atlas_SFP, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.Panel_Conifg, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.fraTraceThickness, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.fraTraceColor, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.fraExtTriggerCoup, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me._optExtTrigCoup_1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me._optExtTrigCoup_0, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.fraOutputTrig, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.chkECL1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.chkExternalOutput, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.chkECL0, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.fraProbeComp, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.fraOnOff, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.ribCompON, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.ribCompOFF, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.panWaveDisplay, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.SSFrame1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.SSPanel16, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.spnMathVOffset, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.fraMemVRange, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.SSPanel18, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.SpinButton1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.fraOperation, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.fraMathSourceB, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.fraRange, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.SSFrame2, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.SSPanel17, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.spnMemVOffset, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.fraMemVerticalRange, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.SSPanel19, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.SpinButton2, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.fraTrigMode, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me._optTrigMode_2, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me._optTrigMode_0, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me._optTrigMode_1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.fraTrigSour, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.fraTrigSlope, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me._optTrigSlope_0, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me._optTrigSlope_1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.fraNoiseRej, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me._optNoiseRej_0, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me._optNoiseRej_1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.fraTrigLevel, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.SSPanel12, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.spnTrigLevel, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.fraTrigDelay, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me._optTrigDelay_0, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me._optTrigDelay_1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.SSPanel13, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.spnDelEvents, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.fraTrigDelaySource, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.fraDelaySlope, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me._optDelaySlope_1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me._optDelaySlope_0, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.fraOccurrence, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.SSPanel15, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.spnOccurrence, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.fraStandard, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me._optStandard_1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me._optStandard_0, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.fraField, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me._optField_1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me._optField_0, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.fraLine, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.SSPanel14, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.spnLine, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me._fraFunction_2, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.fraDelayParam, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.SSPanel5, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.spnStopLevel, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.SSPanel4, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.spnStopEdge, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.SSPanel3, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.spnStartLevel, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.SSPanel1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.spnStartEdge, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me._optStartLevel_1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me._optStartLevel_0, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me._optStopSlope_1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me._optStopSlope_0, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me._optStartSlope_1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me._optStartSlope_0, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.tbrMeasFunction, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me._fraSignalFilters_1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.chkLFReject2, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.chkHFReject2, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me._fraProbeAttenuation_1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me._optProbe2_3, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me._optProbe2_2, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me._optProbe2_1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me._optProbe2_0, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me._fraVerticalRange_1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me._fraCoupling_1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me._optCoupling2_1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me._optCoupling2_0, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me._optCoupling2_2, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.fraOffset2, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.SSPanel11, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.spnVOffset2, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.fraOffset1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.SSPanel10, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.spnVOffset1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me._fraCoupling_0, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me._optCoupling1_2, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me._optCoupling1_0, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me._optCoupling1_1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me._fraVerticalRange_0, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me._fraProbeAttenuation_0, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me._optProbe1_0, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me._optProbe1_1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me._optProbe1_2, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me._optProbe1_3, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me._fraSignalFilters_0, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.chkHFReject1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.chkLFReject1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.fraDisplay, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me._chkDisplayTrace_3, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me._chkDisplayTrace_2, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me._chkDisplayTrace_1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me._chkDisplayTrace_0, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.fraAcqMode, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.fraSampleType, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.fraAvgCount, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.SSPanel8, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.spnAvgCount, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.fraReference, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me._optReference_1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me._optReference_0, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me._optReference_2, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.fraDataPoints, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me._optDataPoints_1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me._optDataPoints_0, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.fraCompletion, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.SSPanel9, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.spnCompletion, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.fraHorizRange, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.fraDealy, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.SSPanel6, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.spnDelay, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.fraHoldOff, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me._optHoldOff_0, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me._optHoldOff_1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.SSPanel7, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.spnHoldOff, System.ComponentModel.ISupportInitialize).EndInit()
		Me.tabMainFunctions.ResumeLayout(False)
		Me._tabMainFunctions_TabPage0.ResumeLayout(False)
		Me.fraHoldOff.ResumeLayout(False)
		Me.SSPanel7.ResumeLayout(False)
		Me.fraDealy.ResumeLayout(False)
		Me.SSPanel6.ResumeLayout(False)
		Me.fraHorizRange.ResumeLayout(False)
		Me.fraCompletion.ResumeLayout(False)
		Me.SSPanel9.ResumeLayout(False)
		Me.fraDataPoints.ResumeLayout(False)
		Me.fraReference.ResumeLayout(False)
		Me.fraAvgCount.ResumeLayout(False)
		Me.SSPanel8.ResumeLayout(False)
		Me.fraSampleType.ResumeLayout(False)
		Me.fraAcqMode.ResumeLayout(False)
		Me.fraDisplay.ResumeLayout(False)
		Me._tabMainFunctions_TabPage1.ResumeLayout(False)
		Me._fraSignalFilters_0.ResumeLayout(False)
		Me._fraProbeAttenuation_0.ResumeLayout(False)
		Me._fraVerticalRange_0.ResumeLayout(False)
		Me._fraCoupling_0.ResumeLayout(False)
		Me.fraOffset1.ResumeLayout(False)
		Me.SSPanel10.ResumeLayout(False)
		Me._tabMainFunctions_TabPage2.ResumeLayout(False)
		Me.fraOffset2.ResumeLayout(False)
		Me.SSPanel11.ResumeLayout(False)
		Me._fraCoupling_1.ResumeLayout(False)
		Me._fraVerticalRange_1.ResumeLayout(False)
		Me._fraProbeAttenuation_1.ResumeLayout(False)
		Me._fraSignalFilters_1.ResumeLayout(False)
		Me._tabMainFunctions_TabPage3.ResumeLayout(False)
		Me._fraFunction_2.ResumeLayout(False)
		Me.fraDelayParam.ResumeLayout(False)
		Me.Picture2.ResumeLayout(False)
		Me.Picture3.ResumeLayout(False)
		Me.Picture4.ResumeLayout(False)
		Me.SSPanel1.ResumeLayout(False)
		Me.SSPanel3.ResumeLayout(False)
		Me.SSPanel4.ResumeLayout(False)
		Me.SSPanel5.ResumeLayout(False)
		Me._tabMainFunctions_TabPage4.ResumeLayout(False)
		Me.fraLine.ResumeLayout(False)
		Me.SSPanel14.ResumeLayout(False)
		Me.fraField.ResumeLayout(False)
		Me.fraStandard.ResumeLayout(False)
		Me.fraOccurrence.ResumeLayout(False)
		Me.SSPanel15.ResumeLayout(False)
		Me.fraDelaySlope.ResumeLayout(False)
		Me.fraTrigDelaySource.ResumeLayout(False)
		Me.fraTrigDelay.ResumeLayout(False)
		Me.SSPanel13.ResumeLayout(False)
		Me.fraTrigLevel.ResumeLayout(False)
		Me.SSPanel12.ResumeLayout(False)
		Me.fraNoiseRej.ResumeLayout(False)
		Me.fraTrigSlope.ResumeLayout(False)
		Me.fraTrigSour.ResumeLayout(False)
		Me.fraTrigMode.ResumeLayout(False)
		Me._tabMainFunctions_TabPage5.ResumeLayout(False)
		Me.picMemOptions.ResumeLayout(False)
		Me.fraMemVerticalRange.ResumeLayout(False)
		Me.SSPanel19.ResumeLayout(False)
		Me.SSFrame2.ResumeLayout(False)
		Me.SSPanel17.ResumeLayout(False)
		Me.picMathOptions.ResumeLayout(False)
		Me.fraRange.ResumeLayout(False)
		Me.fraMathSourceB.ResumeLayout(False)
		Me.fraOperation.ResumeLayout(False)
		Me.fraMemVRange.ResumeLayout(False)
		Me.SSPanel18.ResumeLayout(False)
		Me.SSFrame1.ResumeLayout(False)
		Me.SSPanel16.ResumeLayout(False)
		Me._tabMainFunctions_TabPage6.ResumeLayout(False)
		Me.fraProbeComp.ResumeLayout(False)
		Me.fraOnOff.ResumeLayout(False)
		Me.panWaveDisplay.ResumeLayout(False)
		Me.fraOutputTrig.ResumeLayout(False)
		Me.fraExtTriggerCoup.ResumeLayout(False)
		Me.fraTraceColor.ResumeLayout(False)
		Me.fraTraceThickness.ResumeLayout(False)
		Me._tabMainFunctions_TabPage7.ResumeLayout(False)
		Me.panDmmDisplay.ResumeLayout(False)
		Me.panScopeDisplay.ResumeLayout(False)
		Me.ResumeLayout(False)
		Me.PerformLayout()
	End Sub
#End Region 
End Class