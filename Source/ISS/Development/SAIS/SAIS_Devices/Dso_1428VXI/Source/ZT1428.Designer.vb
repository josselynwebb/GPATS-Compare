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
    Public WithEvents cboSampleType As System.Windows.Forms.ComboBox
    Public WithEvents cboAcqMode As System.Windows.Forms.ComboBox
    Public WithEvents _tabMainFunctions_TabPage0 As System.Windows.Forms.TabPage
    Public WithEvents _cmdSaveToDisk_0 As System.Windows.Forms.Button
    Public WithEvents _cmdSaveToMem_0 As System.Windows.Forms.Button
    Public WithEvents _cboVertRange_0 As System.Windows.Forms.ComboBox
    Public WithEvents _tabMainFunctions_TabPage1 As System.Windows.Forms.TabPage
    Public WithEvents _cmdSaveToMem_1 As System.Windows.Forms.Button
    Public WithEvents _cmdSaveToDisk_1 As System.Windows.Forms.Button
    Public WithEvents _tabMainFunctions_TabPage2 As System.Windows.Forms.TabPage
    Public WithEvents cboMeasSour As System.Windows.Forms.ComboBox
    Public WithEvents cboStopSource As System.Windows.Forms.ComboBox
    Public WithEvents cboStartSource As System.Windows.Forms.ComboBox
    Public WithEvents cmdHelp As System.Windows.Forms.Button
    Public WithEvents _tabMainFunctions_TabPage3 As System.Windows.Forms.TabPage
    Public WithEvents cboTrigDelaySour As System.Windows.Forms.ComboBox
    Public WithEvents cboTrigSour As System.Windows.Forms.ComboBox
    Public WithEvents _tabMainFunctions_TabPage4 As System.Windows.Forms.TabPage
    Public WithEvents cmdLoadFromDisk As System.Windows.Forms.Button
    Public WithEvents _cmdSaveToDisk_3 As System.Windows.Forms.Button
    Public WithEvents _cmdSaveToMem_2 As System.Windows.Forms.Button
    Public WithEvents _cmdSaveToDisk_2 As System.Windows.Forms.Button
    Public WithEvents cboMathSourceA As System.Windows.Forms.ComboBox
    Public WithEvents cboMathSourceB As System.Windows.Forms.ComboBox
    Public WithEvents cboMathFunction As System.Windows.Forms.ComboBox
    Public WithEvents _tabMainFunctions_TabPage5 As System.Windows.Forms.TabPage
    Public WithEvents imgProbeComp As System.Windows.Forms.PictureBox
    Public WithEvents cboTraceColor As System.Windows.Forms.ComboBox
    Public WithEvents picTraceColor As System.Windows.Forms.PictureBox
    Public WithEvents cboTraceThickness As System.Windows.Forms.ComboBox
    Public WithEvents cmdAbout As System.Windows.Forms.Button
    Public WithEvents cmdUpdateTIP As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents cmdSelfTest As System.Windows.Forms.Button
    Public WithEvents _tabMainFunctions_TabPage6 As System.Windows.Forms.TabPage
    Public WithEvents _tabMainFunctions_TabPage7 As System.Windows.Forms.TabPage
    Public WithEvents tabMainFunctions As System.Windows.Forms.TabControl
    Public WithEvents cmdAutoscale As System.Windows.Forms.Button
    Public WithEvents cmdMeasure As System.Windows.Forms.Button
    Public WithEvents cmdQuit As System.Windows.Forms.Button
    Public WithEvents cmdReset As System.Windows.Forms.Button
    Public WithEvents txtDataDisplay As System.Windows.Forms.Label
    Public WithEvents chkContinuous As System.Windows.Forms.CheckBox
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
    Public WithEvents cboVertRange As Microsoft.VisualBasic.Compatibility.VB6.ComboBoxArray
    Public WithEvents cmdSaveToDisk As Microsoft.VisualBasic.Compatibility.VB6.ButtonArray
    Public WithEvents cmdSaveToMem As Microsoft.VisualBasic.Compatibility.VB6.ButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent() '
        'MessageBox.Show("Debug", "Debug", MessageBoxButtons.OK) ' leave for debugging
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmZT1428))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.dlgSegmentDataOpen = New System.Windows.Forms.OpenFileDialog()
        Me.dlgSegmentDataSave = New System.Windows.Forms.SaveFileDialog()
        Me.dlgSegmentDataFont = New System.Windows.Forms.FontDialog()
        Me.dlgSegmentDataColor = New System.Windows.Forms.ColorDialog()
        Me.dlgSegmentDataPrint = New System.Windows.Forms.PrintDialog()
        Me.cmdDisplaySize = New System.Windows.Forms.Button()
        Me.tabMainFunctions = New System.Windows.Forms.TabControl()
        Me._tabMainFunctions_TabPage0 = New System.Windows.Forms.TabPage()
        Me.fraHoldOff = New System.Windows.Forms.GroupBox()
        Me.lblEvents = New System.Windows.Forms.Label()
        Me.SSPanel7 = New System.Windows.Forms.Panel()
        Me.txtHoldOff = New System.Windows.Forms.TextBox()
        Me.spnHoldOff = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me._optHoldOff_1 = New System.Windows.Forms.RadioButton()
        Me._optHoldOff_0 = New System.Windows.Forms.RadioButton()
        Me.fraDealy = New System.Windows.Forms.GroupBox()
        Me.SSPanel6 = New System.Windows.Forms.Panel()
        Me.txtDelay = New System.Windows.Forms.TextBox()
        Me.NumericEdit1 = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.fraCompletion = New System.Windows.Forms.GroupBox()
        Me.SSPanel9 = New System.Windows.Forms.Panel()
        Me.txtCompletion = New System.Windows.Forms.TextBox()
        Me.spnCompletion = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.fraAvgCount = New System.Windows.Forms.GroupBox()
        Me.SSPanel8 = New System.Windows.Forms.Panel()
        Me.txtAvgCount = New System.Windows.Forms.TextBox()
        Me.spnAvgCount = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.fraReference = New System.Windows.Forms.GroupBox()
        Me._optReference_2 = New System.Windows.Forms.RadioButton()
        Me._optReference_0 = New System.Windows.Forms.RadioButton()
        Me._optReference_1 = New System.Windows.Forms.RadioButton()
        Me.fraSampleType = New System.Windows.Forms.GroupBox()
        Me.cboSampleType = New System.Windows.Forms.ComboBox()
        Me.fraAcqMode = New System.Windows.Forms.GroupBox()
        Me.cboAcqMode = New System.Windows.Forms.ComboBox()
        Me.fraHorizRange = New System.Windows.Forms.GroupBox()
        Me.cboHorizRange = New System.Windows.Forms.ComboBox()
        Me.fraDataPoints = New System.Windows.Forms.GroupBox()
        Me._optDataPoints_1 = New System.Windows.Forms.RadioButton()
        Me._optDataPoints_0 = New System.Windows.Forms.RadioButton()
        Me._chkDisplayTrace_2 = New System.Windows.Forms.CheckBox()
        Me._chkDisplayTrace_3 = New System.Windows.Forms.CheckBox()
        Me._chkDisplayTrace_1 = New System.Windows.Forms.CheckBox()
        Me._chkDisplayTrace_0 = New System.Windows.Forms.CheckBox()
        Me.fraDisplay = New System.Windows.Forms.GroupBox()
        Me._tabMainFunctions_TabPage1 = New System.Windows.Forms.TabPage()
        Me._fraSignalFilters_0 = New System.Windows.Forms.GroupBox()
        Me.chkHFReject1 = New System.Windows.Forms.CheckBox()
        Me.chkLFReject1 = New System.Windows.Forms.CheckBox()
        Me._fraCoupling_0 = New System.Windows.Forms.GroupBox()
        Me._optCoupling1_2 = New System.Windows.Forms.RadioButton()
        Me._optCoupling1_1 = New System.Windows.Forms.RadioButton()
        Me._optCoupling1_0 = New System.Windows.Forms.RadioButton()
        Me._fraProbeAttenuation_0 = New System.Windows.Forms.GroupBox()
        Me._optProbe1_3 = New System.Windows.Forms.RadioButton()
        Me._optProbe1_2 = New System.Windows.Forms.RadioButton()
        Me._optProbe1_1 = New System.Windows.Forms.RadioButton()
        Me._optProbe1_0 = New System.Windows.Forms.RadioButton()
        Me.fraOffset1 = New System.Windows.Forms.GroupBox()
        Me.SSPanel10 = New System.Windows.Forms.Panel()
        Me.txtVOffset1 = New System.Windows.Forms.TextBox()
        Me.spnVOffset1 = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me._fraVerticalRange_0 = New System.Windows.Forms.GroupBox()
        Me._cboVertRange_0 = New System.Windows.Forms.ComboBox()
        Me._cmdSaveToDisk_0 = New System.Windows.Forms.Button()
        Me._cmdSaveToMem_0 = New System.Windows.Forms.Button()
        Me._tabMainFunctions_TabPage2 = New System.Windows.Forms.TabPage()
        Me._fraCoupling_1 = New System.Windows.Forms.GroupBox()
        Me._optCoupling2_2 = New System.Windows.Forms.RadioButton()
        Me._optCoupling2_1 = New System.Windows.Forms.RadioButton()
        Me._optCoupling2_0 = New System.Windows.Forms.RadioButton()
        Me._fraSignalFilters_1 = New System.Windows.Forms.GroupBox()
        Me.chkHFReject2 = New System.Windows.Forms.CheckBox()
        Me.chkLFReject2 = New System.Windows.Forms.CheckBox()
        Me._fraProbeAttenuation_1 = New System.Windows.Forms.GroupBox()
        Me._optProbe2_3 = New System.Windows.Forms.RadioButton()
        Me._optProbe2_2 = New System.Windows.Forms.RadioButton()
        Me._optProbe2_1 = New System.Windows.Forms.RadioButton()
        Me._optProbe2_0 = New System.Windows.Forms.RadioButton()
        Me.fraOffset2 = New System.Windows.Forms.GroupBox()
        Me.SSPanel11 = New System.Windows.Forms.Panel()
        Me.txtVOffset2 = New System.Windows.Forms.TextBox()
        Me.spnVOffset2 = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me._fraVerticalRange_1 = New System.Windows.Forms.GroupBox()
        Me._cboVertRange_1 = New System.Windows.Forms.ComboBox()
        Me._cmdSaveToMem_1 = New System.Windows.Forms.Button()
        Me._cmdSaveToDisk_1 = New System.Windows.Forms.Button()
        Me._tabMainFunctions_TabPage3 = New System.Windows.Forms.TabPage()
        Me._fraFunction_2 = New System.Windows.Forms.GroupBox()
        Me.fraDelayParam = New System.Windows.Forms.Panel()
        Me.SSPanel5 = New System.Windows.Forms.Panel()
        Me.txtStopLevel = New System.Windows.Forms.TextBox()
        Me.spnStopLevel = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.SSPanel3 = New System.Windows.Forms.Panel()
        Me.txtStartLevel = New System.Windows.Forms.TextBox()
        Me.spnStartLevel = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.Picture4 = New System.Windows.Forms.Panel()
        Me._optStartLevel_1 = New System.Windows.Forms.RadioButton()
        Me._optStartLevel_0 = New System.Windows.Forms.RadioButton()
        Me.lblStartLevelCap = New System.Windows.Forms.Label()
        Me.SSPanel4 = New System.Windows.Forms.Panel()
        Me.txtStopEdge = New System.Windows.Forms.TextBox()
        Me.spnStopEdge = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.SSPanel1 = New System.Windows.Forms.Panel()
        Me.txtStartEdge = New System.Windows.Forms.TextBox()
        Me.spnStartEdge = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.lblStopEdgeCap = New System.Windows.Forms.Label()
        Me.lblStartEdgeCap = New System.Windows.Forms.Label()
        Me.Picture3 = New System.Windows.Forms.Panel()
        Me._optStopSlope_1 = New System.Windows.Forms.RadioButton()
        Me._optStopSlope_0 = New System.Windows.Forms.RadioButton()
        Me.lblStopSlope = New System.Windows.Forms.Label()
        Me.Line1 = New System.Windows.Forms.Label()
        Me.Picture2 = New System.Windows.Forms.Panel()
        Me._optStartSlope_1 = New System.Windows.Forms.RadioButton()
        Me._optStartSlope_0 = New System.Windows.Forms.RadioButton()
        Me.lblStartSlope = New System.Windows.Forms.Label()
        Me.cboStopSource = New System.Windows.Forms.ComboBox()
        Me.lblStopSource = New System.Windows.Forms.Label()
        Me.cboStartSource = New System.Windows.Forms.ComboBox()
        Me.lblStartSource = New System.Windows.Forms.Label()
        Me.lblStop = New System.Windows.Forms.Label()
        Me.lblStar = New System.Windows.Forms.Label()
        Me.tbrMeasFunction = New System.Windows.Forms.ToolBar()
        Me.Dummy = New System.Windows.Forms.ToolBarButton()
        Me.DCRMS = New System.Windows.Forms.ToolBarButton()
        Me.Button2 = New System.Windows.Forms.ToolBarButton()
        Me.Button3 = New System.Windows.Forms.ToolBarButton()
        Me.Button4 = New System.Windows.Forms.ToolBarButton()
        Me.Button5 = New System.Windows.Forms.ToolBarButton()
        Me.Button6 = New System.Windows.Forms.ToolBarButton()
        Me.Button7 = New System.Windows.Forms.ToolBarButton()
        Me.Button8 = New System.Windows.Forms.ToolBarButton()
        Me.Button9 = New System.Windows.Forms.ToolBarButton()
        Me.Button10 = New System.Windows.Forms.ToolBarButton()
        Me.Button11 = New System.Windows.Forms.ToolBarButton()
        Me.Button12 = New System.Windows.Forms.ToolBarButton()
        Me.Button13 = New System.Windows.Forms.ToolBarButton()
        Me.Button14 = New System.Windows.Forms.ToolBarButton()
        Me.Button15 = New System.Windows.Forms.ToolBarButton()
        Me.Button16 = New System.Windows.Forms.ToolBarButton()
        Me.Button17 = New System.Windows.Forms.ToolBarButton()
        Me.Button18 = New System.Windows.Forms.ToolBarButton()
        Me.Button19 = New System.Windows.Forms.ToolBarButton()
        Me.Button20 = New System.Windows.Forms.ToolBarButton()
        Me.imgFunctions = New System.Windows.Forms.ImageList(Me.components)
        Me.cboMeasSour = New System.Windows.Forms.ComboBox()
        Me.cmdHelp = New System.Windows.Forms.Button()
        Me._tabMainFunctions_TabPage4 = New System.Windows.Forms.TabPage()
        Me.fraLine = New System.Windows.Forms.GroupBox()
        Me.SSPanel14 = New System.Windows.Forms.Panel()
        Me.txtLine = New System.Windows.Forms.TextBox()
        Me.spnLine = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.fraField = New System.Windows.Forms.GroupBox()
        Me._optField_1 = New System.Windows.Forms.RadioButton()
        Me._optField_0 = New System.Windows.Forms.RadioButton()
        Me.fraStandard = New System.Windows.Forms.GroupBox()
        Me._optStandard_1 = New System.Windows.Forms.RadioButton()
        Me._optStandard_0 = New System.Windows.Forms.RadioButton()
        Me.fraOccurrence = New System.Windows.Forms.GroupBox()
        Me.SSPanel15 = New System.Windows.Forms.Panel()
        Me.txtOccurrence = New System.Windows.Forms.TextBox()
        Me.spnOccurrence = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.fraDelaySlope = New System.Windows.Forms.GroupBox()
        Me._optDelaySlope_1 = New System.Windows.Forms.RadioButton()
        Me._optDelaySlope_0 = New System.Windows.Forms.RadioButton()
        Me.fraTrigDelaySource = New System.Windows.Forms.GroupBox()
        Me.cboTrigDelaySour = New System.Windows.Forms.ComboBox()
        Me.fraTrigDelay = New System.Windows.Forms.GroupBox()
        Me.SSPanel13 = New System.Windows.Forms.Panel()
        Me.txtDelEvents = New System.Windows.Forms.TextBox()
        Me.spnDelEvents = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.lblTrigDelayEvents = New System.Windows.Forms.Label()
        Me._optTrigDelay_1 = New System.Windows.Forms.RadioButton()
        Me._optTrigDelay_0 = New System.Windows.Forms.RadioButton()
        Me.fraTrigLevel = New System.Windows.Forms.GroupBox()
        Me.SSPanel12 = New System.Windows.Forms.Panel()
        Me.txtTrigLevel = New System.Windows.Forms.TextBox()
        Me.spnTrigLevel = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.fraNoiseRej = New System.Windows.Forms.GroupBox()
        Me._optNoiseRej_1 = New System.Windows.Forms.RadioButton()
        Me._optNoiseRej_0 = New System.Windows.Forms.RadioButton()
        Me.fraTrigSlope = New System.Windows.Forms.GroupBox()
        Me._optTrigSlope_1 = New System.Windows.Forms.RadioButton()
        Me._optTrigSlope_0 = New System.Windows.Forms.RadioButton()
        Me.fraTrigSour = New System.Windows.Forms.GroupBox()
        Me.cboTrigSour = New System.Windows.Forms.ComboBox()
        Me.fraTrigMode = New System.Windows.Forms.GroupBox()
        Me._optTrigMode_1 = New System.Windows.Forms.RadioButton()
        Me._optTrigMode_2 = New System.Windows.Forms.RadioButton()
        Me._optTrigMode_0 = New System.Windows.Forms.RadioButton()
        Me._tabMainFunctions_TabPage5 = New System.Windows.Forms.TabPage()
        Me.picMemOptions = New System.Windows.Forms.Panel()
        Me.SSFrame2 = New System.Windows.Forms.GroupBox()
        Me.SSPanel17 = New System.Windows.Forms.Panel()
        Me.txtMemVoltOffset = New System.Windows.Forms.TextBox()
        Me.spnMemVOffset = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.fraMemVerticalRange = New System.Windows.Forms.GroupBox()
        Me.SSPanel19 = New System.Windows.Forms.Panel()
        Me.txtMemoryRange = New System.Windows.Forms.TextBox()
        Me.SpinButton2 = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me._cmdSaveToDisk_3 = New System.Windows.Forms.Button()
        Me.cmdLoadFromDisk = New System.Windows.Forms.Button()
        Me.Line4 = New System.Windows.Forms.Label()
        Me.Line5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.picMathOptions = New System.Windows.Forms.Panel()
        Me.SSFrame1 = New System.Windows.Forms.GroupBox()
        Me.SSPanel16 = New System.Windows.Forms.Panel()
        Me.txtMathVOffset = New System.Windows.Forms.TextBox()
        Me.spnMathVOffset = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.fraMemVRange = New System.Windows.Forms.GroupBox()
        Me.SSPanel18 = New System.Windows.Forms.Panel()
        Me.txtMathRange = New System.Windows.Forms.TextBox()
        Me.SpinButton1 = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me._cmdSaveToDisk_2 = New System.Windows.Forms.Button()
        Me._cmdSaveToMem_2 = New System.Windows.Forms.Button()
        Me.fraMathSourceB = New System.Windows.Forms.GroupBox()
        Me.cboMathSourceB = New System.Windows.Forms.ComboBox()
        Me.fraRange = New System.Windows.Forms.GroupBox()
        Me.cboMathSourceA = New System.Windows.Forms.ComboBox()
        Me.fraOperation = New System.Windows.Forms.GroupBox()
        Me.cboMathFunction = New System.Windows.Forms.ComboBox()
        Me.Line3 = New System.Windows.Forms.Label()
        Me.Line2 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me._tabMainFunctions_TabPage6 = New System.Windows.Forms.TabPage()
        Me.fraTraceColor = New System.Windows.Forms.GroupBox()
        Me.picTraceColor = New System.Windows.Forms.PictureBox()
        Me.cboTraceColor = New System.Windows.Forms.ComboBox()
        Me.fraTraceThickness = New System.Windows.Forms.GroupBox()
        Me.cboTraceThickness = New System.Windows.Forms.ComboBox()
        Me.fraOnOff = New System.Windows.Forms.GroupBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.ribCompOFF = New System.Windows.Forms.CheckBox()
        Me.ribCompON = New System.Windows.Forms.CheckBox()
        Me.panWaveDisplay = New System.Windows.Forms.Panel()
        Me.imgProbeComp = New System.Windows.Forms.PictureBox()
        Me.fraOutputTrig = New System.Windows.Forms.GroupBox()
        Me.chkExternalOutput = New System.Windows.Forms.CheckBox()
        Me.chkECL0 = New System.Windows.Forms.CheckBox()
        Me.chkECL1 = New System.Windows.Forms.CheckBox()
        Me.fraExtTriggerCoup = New System.Windows.Forms.GroupBox()
        Me._optExtTrigCoup_1 = New System.Windows.Forms.RadioButton()
        Me._optExtTrigCoup_0 = New System.Windows.Forms.RadioButton()
        Me.Panel_Conifg = New VIPERT_Common_Controls.Panel_Conifg()
        Me.cmdAbout = New System.Windows.Forms.Button()
        Me.cmdUpdateTIP = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdSelfTest = New System.Windows.Forms.Button()
        Me._tabMainFunctions_TabPage7 = New System.Windows.Forms.TabPage()
        Me.Atlas_SFP = New VIPERT_Common_Controls.Atlas_SFP()
        Me.cmdAutoscale = New System.Windows.Forms.Button()
        Me.cmdMeasure = New System.Windows.Forms.Button()
        Me.cmdQuit = New System.Windows.Forms.Button()
        Me.cmdReset = New System.Windows.Forms.Button()
        Me.txtDataDisplay = New System.Windows.Forms.Label()
        Me.chkContinuous = New System.Windows.Forms.CheckBox()
        Me.CommonDialog1Open = New System.Windows.Forms.OpenFileDialog()
        Me.CommonDialog1Save = New System.Windows.Forms.SaveFileDialog()
        Me.CommonDialog1Font = New System.Windows.Forms.FontDialog()
        Me.CommonDialog1Color = New System.Windows.Forms.ColorDialog()
        Me.CommonDialog1Print = New System.Windows.Forms.PrintDialog()
        Me.dlgFileIOOpen = New System.Windows.Forms.OpenFileDialog()
        Me.dlgFileIOSave = New System.Windows.Forms.SaveFileDialog()
        Me.dlgFileIOFont = New System.Windows.Forms.FontDialog()
        Me.dlgFileIOColor = New System.Windows.Forms.ColorDialog()
        Me.dlgFileIOPrint = New System.Windows.Forms.PrintDialog()
        Me.cboVertRange = New Microsoft.VisualBasic.Compatibility.VB6.ComboBoxArray(Me.components)
        Me.cmdSaveToDisk = New Microsoft.VisualBasic.Compatibility.VB6.ButtonArray(Me.components)
        Me.cmdSaveToMem = New Microsoft.VisualBasic.Compatibility.VB6.ButtonArray(Me.components)
        Me.panScopeDisplay = New System.Windows.Forms.Panel()
        Me.cwgScopeDisplay = New NationalInstruments.UI.WindowsForms.ScatterGraph()
        Me.XyCursor1 = New NationalInstruments.UI.XYCursor()
        Me.Plot1 = New NationalInstruments.UI.ScatterPlot()
        Me.XAxis1 = New NationalInstruments.UI.XAxis()
        Me.YAxis1 = New NationalInstruments.UI.YAxis()
        Me.Plot2 = New NationalInstruments.UI.ScatterPlot()
        Me.YAxis2 = New NationalInstruments.UI.YAxis()
        Me.Plot3 = New NationalInstruments.UI.ScatterPlot()
        Me.YAxis3 = New NationalInstruments.UI.YAxis()
        Me.Plot4 = New NationalInstruments.UI.ScatterPlot()
        Me.YAxis4 = New NationalInstruments.UI.YAxis()
        Me.XAxisTicks = New NationalInstruments.UI.XAxis()
        Me.YAxisTicks = New NationalInstruments.UI.YAxis()
        Me.panDmmDisplay = New System.Windows.Forms.Panel()
        Me.StatusBar1 = New System.Windows.Forms.StatusStrip()
        Me.DummyStrip1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel2 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel3 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.PrintForm1 = New Microsoft.VisualBasic.PowerPacks.Printing.PrintForm(Me.components)
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.RadioButton1 = New System.Windows.Forms.RadioButton()
        Me.RadioButton2 = New System.Windows.Forms.RadioButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tabMainFunctions.SuspendLayout()
        Me._tabMainFunctions_TabPage0.SuspendLayout()
        Me.fraHoldOff.SuspendLayout()
        Me.SSPanel7.SuspendLayout()
        CType(Me.spnHoldOff, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraDealy.SuspendLayout()
        Me.SSPanel6.SuspendLayout()
        CType(Me.NumericEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraCompletion.SuspendLayout()
        Me.SSPanel9.SuspendLayout()
        CType(Me.spnCompletion, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraAvgCount.SuspendLayout()
        Me.SSPanel8.SuspendLayout()
        CType(Me.spnAvgCount, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraReference.SuspendLayout()
        Me.fraSampleType.SuspendLayout()
        Me.fraAcqMode.SuspendLayout()
        Me.fraHorizRange.SuspendLayout()
        Me.fraDataPoints.SuspendLayout()
        Me._tabMainFunctions_TabPage1.SuspendLayout()
        Me._fraSignalFilters_0.SuspendLayout()
        Me._fraCoupling_0.SuspendLayout()
        Me._fraProbeAttenuation_0.SuspendLayout()
        Me.fraOffset1.SuspendLayout()
        Me.SSPanel10.SuspendLayout()
        CType(Me.spnVOffset1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._fraVerticalRange_0.SuspendLayout()
        Me._tabMainFunctions_TabPage2.SuspendLayout()
        Me._fraCoupling_1.SuspendLayout()
        Me._fraSignalFilters_1.SuspendLayout()
        Me._fraProbeAttenuation_1.SuspendLayout()
        Me.fraOffset2.SuspendLayout()
        Me.SSPanel11.SuspendLayout()
        CType(Me.spnVOffset2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._fraVerticalRange_1.SuspendLayout()
        Me._tabMainFunctions_TabPage3.SuspendLayout()
        Me._fraFunction_2.SuspendLayout()
        Me.fraDelayParam.SuspendLayout()
        Me.SSPanel5.SuspendLayout()
        CType(Me.spnStopLevel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SSPanel3.SuspendLayout()
        CType(Me.spnStartLevel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Picture4.SuspendLayout()
        Me.SSPanel4.SuspendLayout()
        CType(Me.spnStopEdge, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SSPanel1.SuspendLayout()
        CType(Me.spnStartEdge, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Picture3.SuspendLayout()
        Me.Picture2.SuspendLayout()
        Me._tabMainFunctions_TabPage4.SuspendLayout()
        Me.fraLine.SuspendLayout()
        Me.SSPanel14.SuspendLayout()
        CType(Me.spnLine, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraField.SuspendLayout()
        Me.fraStandard.SuspendLayout()
        Me.fraOccurrence.SuspendLayout()
        Me.SSPanel15.SuspendLayout()
        CType(Me.spnOccurrence, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraDelaySlope.SuspendLayout()
        Me.fraTrigDelaySource.SuspendLayout()
        Me.fraTrigDelay.SuspendLayout()
        Me.SSPanel13.SuspendLayout()
        CType(Me.spnDelEvents, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraTrigLevel.SuspendLayout()
        Me.SSPanel12.SuspendLayout()
        CType(Me.spnTrigLevel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraNoiseRej.SuspendLayout()
        Me.fraTrigSlope.SuspendLayout()
        Me.fraTrigSour.SuspendLayout()
        Me.fraTrigMode.SuspendLayout()
        Me._tabMainFunctions_TabPage5.SuspendLayout()
        Me.picMemOptions.SuspendLayout()
        Me.SSFrame2.SuspendLayout()
        Me.SSPanel17.SuspendLayout()
        CType(Me.spnMemVOffset, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraMemVerticalRange.SuspendLayout()
        Me.SSPanel19.SuspendLayout()
        CType(Me.SpinButton2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.picMathOptions.SuspendLayout()
        Me.SSFrame1.SuspendLayout()
        Me.SSPanel16.SuspendLayout()
        CType(Me.spnMathVOffset, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraMemVRange.SuspendLayout()
        Me.SSPanel18.SuspendLayout()
        CType(Me.SpinButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraMathSourceB.SuspendLayout()
        Me.fraRange.SuspendLayout()
        Me.fraOperation.SuspendLayout()
        Me._tabMainFunctions_TabPage6.SuspendLayout()
        Me.fraTraceColor.SuspendLayout()
        CType(Me.picTraceColor, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraTraceThickness.SuspendLayout()
        Me.fraOnOff.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.panWaveDisplay.SuspendLayout()
        CType(Me.imgProbeComp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraOutputTrig.SuspendLayout()
        Me.fraExtTriggerCoup.SuspendLayout()
        Me._tabMainFunctions_TabPage7.SuspendLayout()
        CType(Me.cboVertRange, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmdSaveToDisk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmdSaveToMem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panScopeDisplay.SuspendLayout()
        CType(Me.cwgScopeDisplay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XyCursor1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panDmmDisplay.SuspendLayout()
        Me.StatusBar1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'dlgSegmentDataOpen
        '
        Me.dlgSegmentDataOpen.DefaultExt = "seg"
        Me.dlgSegmentDataOpen.FileName = "*.seg"
        '
        'dlgSegmentDataSave
        '
        Me.dlgSegmentDataSave.DefaultExt = "seg"
        Me.dlgSegmentDataSave.Filter = "Segment Files (*.seg)|*.seg|All files (*.*)|*.*"
        '
        'cmdDisplaySize
        '
        Me.cmdDisplaySize.BackColor = System.Drawing.SystemColors.Control
        Me.cmdDisplaySize.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdDisplaySize.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDisplaySize.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDisplaySize.Location = New System.Drawing.Point(563, 296)
        Me.cmdDisplaySize.Name = "cmdDisplaySize"
        Me.cmdDisplaySize.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDisplaySize.Size = New System.Drawing.Size(67, 23)
        Me.cmdDisplaySize.TabIndex = 210
        Me.cmdDisplaySize.Text = "&Full Size"
        Me.cmdDisplaySize.UseVisualStyleBackColor = False
        '
        'tabMainFunctions
        '
        Me.tabMainFunctions.Controls.Add(Me._tabMainFunctions_TabPage0)
        Me.tabMainFunctions.Controls.Add(Me._tabMainFunctions_TabPage1)
        Me.tabMainFunctions.Controls.Add(Me._tabMainFunctions_TabPage2)
        Me.tabMainFunctions.Controls.Add(Me._tabMainFunctions_TabPage3)
        Me.tabMainFunctions.Controls.Add(Me._tabMainFunctions_TabPage4)
        Me.tabMainFunctions.Controls.Add(Me._tabMainFunctions_TabPage5)
        Me.tabMainFunctions.Controls.Add(Me._tabMainFunctions_TabPage6)
        Me.tabMainFunctions.Controls.Add(Me._tabMainFunctions_TabPage7)
        Me.tabMainFunctions.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tabMainFunctions.ItemSize = New System.Drawing.Size(42, 18)
        Me.tabMainFunctions.Location = New System.Drawing.Point(312, 6)
        Me.tabMainFunctions.Multiline = True
        Me.tabMainFunctions.Name = "tabMainFunctions"
        Me.tabMainFunctions.SelectedIndex = 0
        Me.tabMainFunctions.Size = New System.Drawing.Size(480, 286)
        Me.tabMainFunctions.TabIndex = 0
        '
        '_tabMainFunctions_TabPage0
        '
        Me._tabMainFunctions_TabPage0.Controls.Add(Me.fraHoldOff)
        Me._tabMainFunctions_TabPage0.Controls.Add(Me.fraDealy)
        Me._tabMainFunctions_TabPage0.Controls.Add(Me.fraCompletion)
        Me._tabMainFunctions_TabPage0.Controls.Add(Me.fraAvgCount)
        Me._tabMainFunctions_TabPage0.Controls.Add(Me.fraReference)
        Me._tabMainFunctions_TabPage0.Controls.Add(Me.fraSampleType)
        Me._tabMainFunctions_TabPage0.Controls.Add(Me.fraAcqMode)
        Me._tabMainFunctions_TabPage0.Controls.Add(Me.fraHorizRange)
        Me._tabMainFunctions_TabPage0.Controls.Add(Me.fraDataPoints)
        Me._tabMainFunctions_TabPage0.Controls.Add(Me._chkDisplayTrace_2)
        Me._tabMainFunctions_TabPage0.Controls.Add(Me._chkDisplayTrace_3)
        Me._tabMainFunctions_TabPage0.Controls.Add(Me._chkDisplayTrace_1)
        Me._tabMainFunctions_TabPage0.Controls.Add(Me._chkDisplayTrace_0)
        Me._tabMainFunctions_TabPage0.Controls.Add(Me.fraDisplay)
        Me._tabMainFunctions_TabPage0.Location = New System.Drawing.Point(4, 40)
        Me._tabMainFunctions_TabPage0.Name = "_tabMainFunctions_TabPage0"
        Me._tabMainFunctions_TabPage0.Size = New System.Drawing.Size(472, 242)
        Me._tabMainFunctions_TabPage0.TabIndex = 0
        Me._tabMainFunctions_TabPage0.Text = "Timebase/Digitize "
        '
        'fraHoldOff
        '
        Me.fraHoldOff.Controls.Add(Me.lblEvents)
        Me.fraHoldOff.Controls.Add(Me.SSPanel7)
        Me.fraHoldOff.Controls.Add(Me._optHoldOff_1)
        Me.fraHoldOff.Controls.Add(Me._optHoldOff_0)
        Me.fraHoldOff.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraHoldOff.ForeColor = System.Drawing.Color.Navy
        Me.fraHoldOff.Location = New System.Drawing.Point(210, 107)
        Me.fraHoldOff.Name = "fraHoldOff"
        Me.fraHoldOff.Size = New System.Drawing.Size(97, 127)
        Me.fraHoldOff.TabIndex = 138
        Me.fraHoldOff.TabStop = False
        Me.fraHoldOff.Text = "Hold Off"
        '
        'lblEvents
        '
        Me.lblEvents.BackColor = System.Drawing.SystemColors.Control
        Me.lblEvents.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblEvents.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEvents.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblEvents.Location = New System.Drawing.Point(8, 76)
        Me.lblEvents.Name = "lblEvents"
        Me.lblEvents.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblEvents.Size = New System.Drawing.Size(38, 15)
        Me.lblEvents.TabIndex = 130
        Me.lblEvents.Text = "Time"
        '
        'SSPanel7
        '
        Me.SSPanel7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SSPanel7.Controls.Add(Me.txtHoldOff)
        Me.SSPanel7.Controls.Add(Me.spnHoldOff)
        Me.SSPanel7.Location = New System.Drawing.Point(7, 93)
        Me.SSPanel7.Name = "SSPanel7"
        Me.SSPanel7.Size = New System.Drawing.Size(81, 20)
        Me.SSPanel7.TabIndex = 2
        '
        'txtHoldOff
        '
        Me.txtHoldOff.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtHoldOff.Location = New System.Drawing.Point(2, 1)
        Me.txtHoldOff.Name = "txtHoldOff"
        Me.txtHoldOff.Size = New System.Drawing.Size(59, 13)
        Me.txtHoldOff.TabIndex = 1
        '
        'spnHoldOff
        '
        Me.spnHoldOff.Location = New System.Drawing.Point(63, -2)
        Me.spnHoldOff.Name = "spnHoldOff"
        Me.spnHoldOff.Size = New System.Drawing.Size(16, 20)
        Me.spnHoldOff.TabIndex = 0
        Me.spnHoldOff.Value = 1.0R
        '
        '_optHoldOff_1
        '
        Me._optHoldOff_1.AutoSize = True
        Me._optHoldOff_1.ForeColor = System.Drawing.Color.Black
        Me._optHoldOff_1.Location = New System.Drawing.Point(7, 42)
        Me._optHoldOff_1.Name = "_optHoldOff_1"
        Me._optHoldOff_1.Size = New System.Drawing.Size(58, 17)
        Me._optHoldOff_1.TabIndex = 1
        Me._optHoldOff_1.Text = "Events"
        Me._optHoldOff_1.UseVisualStyleBackColor = True
        '
        '_optHoldOff_0
        '
        Me._optHoldOff_0.AutoSize = True
        Me._optHoldOff_0.Checked = True
        Me._optHoldOff_0.ForeColor = System.Drawing.Color.Black
        Me._optHoldOff_0.Location = New System.Drawing.Point(7, 25)
        Me._optHoldOff_0.Name = "_optHoldOff_0"
        Me._optHoldOff_0.Size = New System.Drawing.Size(48, 17)
        Me._optHoldOff_0.TabIndex = 0
        Me._optHoldOff_0.TabStop = True
        Me._optHoldOff_0.Text = "Time"
        Me._optHoldOff_0.UseVisualStyleBackColor = True
        '
        'fraDealy
        '
        Me.fraDealy.Controls.Add(Me.SSPanel6)
        Me.fraDealy.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraDealy.ForeColor = System.Drawing.Color.Navy
        Me.fraDealy.Location = New System.Drawing.Point(210, 62)
        Me.fraDealy.Name = "fraDealy"
        Me.fraDealy.Size = New System.Drawing.Size(97, 45)
        Me.fraDealy.TabIndex = 137
        Me.fraDealy.TabStop = False
        Me.fraDealy.Text = "Delay"
        '
        'SSPanel6
        '
        Me.SSPanel6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SSPanel6.Controls.Add(Me.txtDelay)
        Me.SSPanel6.Controls.Add(Me.NumericEdit1)
        Me.SSPanel6.Location = New System.Drawing.Point(8, 20)
        Me.SSPanel6.Name = "SSPanel6"
        Me.SSPanel6.Size = New System.Drawing.Size(81, 20)
        Me.SSPanel6.TabIndex = 1
        '
        'txtDelay
        '
        Me.txtDelay.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtDelay.Location = New System.Drawing.Point(2, 1)
        Me.txtDelay.Name = "txtDelay"
        Me.txtDelay.Size = New System.Drawing.Size(59, 13)
        Me.txtDelay.TabIndex = 1
        '
        'NumericEdit1
        '
        Me.NumericEdit1.Location = New System.Drawing.Point(63, -2)
        Me.NumericEdit1.Name = "NumericEdit1"
        Me.NumericEdit1.Size = New System.Drawing.Size(16, 20)
        Me.NumericEdit1.TabIndex = 0
        Me.NumericEdit1.Value = 1.0R
        '
        'fraCompletion
        '
        Me.fraCompletion.Controls.Add(Me.SSPanel9)
        Me.fraCompletion.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraCompletion.ForeColor = System.Drawing.Color.Navy
        Me.fraCompletion.Location = New System.Drawing.Point(123, 181)
        Me.fraCompletion.Name = "fraCompletion"
        Me.fraCompletion.Size = New System.Drawing.Size(78, 53)
        Me.fraCompletion.TabIndex = 136
        Me.fraCompletion.TabStop = False
        Me.fraCompletion.Text = "% Complete"
        '
        'SSPanel9
        '
        Me.SSPanel9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SSPanel9.Controls.Add(Me.txtCompletion)
        Me.SSPanel9.Controls.Add(Me.spnCompletion)
        Me.SSPanel9.Location = New System.Drawing.Point(8, 19)
        Me.SSPanel9.Name = "SSPanel9"
        Me.SSPanel9.Size = New System.Drawing.Size(55, 20)
        Me.SSPanel9.TabIndex = 1
        '
        'txtCompletion
        '
        Me.txtCompletion.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtCompletion.Location = New System.Drawing.Point(2, 1)
        Me.txtCompletion.Name = "txtCompletion"
        Me.txtCompletion.Size = New System.Drawing.Size(33, 13)
        Me.txtCompletion.TabIndex = 1
        '
        'spnCompletion
        '
        Me.spnCompletion.Location = New System.Drawing.Point(37, -2)
        Me.spnCompletion.Name = "spnCompletion"
        Me.spnCompletion.Range = New NationalInstruments.UI.Range(0.0R, 100.0R)
        Me.spnCompletion.Size = New System.Drawing.Size(16, 20)
        Me.spnCompletion.TabIndex = 0
        Me.spnCompletion.Value = 1.0R
        '
        'fraAvgCount
        '
        Me.fraAvgCount.Controls.Add(Me.SSPanel8)
        Me.fraAvgCount.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraAvgCount.ForeColor = System.Drawing.Color.Navy
        Me.fraAvgCount.Location = New System.Drawing.Point(8, 181)
        Me.fraAvgCount.Name = "fraAvgCount"
        Me.fraAvgCount.Size = New System.Drawing.Size(109, 53)
        Me.fraAvgCount.TabIndex = 135
        Me.fraAvgCount.TabStop = False
        Me.fraAvgCount.Text = "Average Count"
        '
        'SSPanel8
        '
        Me.SSPanel8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SSPanel8.Controls.Add(Me.txtAvgCount)
        Me.SSPanel8.Controls.Add(Me.spnAvgCount)
        Me.SSPanel8.Location = New System.Drawing.Point(8, 19)
        Me.SSPanel8.Name = "SSPanel8"
        Me.SSPanel8.Size = New System.Drawing.Size(90, 20)
        Me.SSPanel8.TabIndex = 1
        '
        'txtAvgCount
        '
        Me.txtAvgCount.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtAvgCount.Location = New System.Drawing.Point(2, 1)
        Me.txtAvgCount.Name = "txtAvgCount"
        Me.txtAvgCount.Size = New System.Drawing.Size(68, 13)
        Me.txtAvgCount.TabIndex = 1
        '
        'spnAvgCount
        '
        Me.spnAvgCount.Location = New System.Drawing.Point(72, -2)
        Me.spnAvgCount.Name = "spnAvgCount"
        Me.spnAvgCount.Range = New NationalInstruments.UI.Range(1.0R, 2048.0R)
        Me.spnAvgCount.Size = New System.Drawing.Size(16, 20)
        Me.spnAvgCount.TabIndex = 0
        Me.spnAvgCount.Value = 1.0R
        '
        'fraReference
        '
        Me.fraReference.Controls.Add(Me._optReference_2)
        Me.fraReference.Controls.Add(Me._optReference_0)
        Me.fraReference.Controls.Add(Me._optReference_1)
        Me.fraReference.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraReference.ForeColor = System.Drawing.Color.Navy
        Me.fraReference.Location = New System.Drawing.Point(123, 14)
        Me.fraReference.Name = "fraReference"
        Me.fraReference.Size = New System.Drawing.Size(78, 90)
        Me.fraReference.TabIndex = 134
        Me.fraReference.TabStop = False
        Me.fraReference.Text = "Reference"
        '
        '_optReference_2
        '
        Me._optReference_2.AutoSize = True
        Me._optReference_2.ForeColor = System.Drawing.Color.Black
        Me._optReference_2.Location = New System.Drawing.Point(7, 59)
        Me._optReference_2.Name = "_optReference_2"
        Me._optReference_2.Size = New System.Drawing.Size(47, 17)
        Me._optReference_2.TabIndex = 2
        Me._optReference_2.Text = "Stop"
        Me._optReference_2.UseVisualStyleBackColor = True
        '
        '_optReference_0
        '
        Me._optReference_0.AutoSize = True
        Me._optReference_0.Checked = True
        Me._optReference_0.ForeColor = System.Drawing.Color.Black
        Me._optReference_0.Location = New System.Drawing.Point(7, 43)
        Me._optReference_0.Name = "_optReference_0"
        Me._optReference_0.Size = New System.Drawing.Size(56, 17)
        Me._optReference_0.TabIndex = 1
        Me._optReference_0.TabStop = True
        Me._optReference_0.Text = "Center"
        Me._optReference_0.UseVisualStyleBackColor = True
        '
        '_optReference_1
        '
        Me._optReference_1.AutoSize = True
        Me._optReference_1.ForeColor = System.Drawing.Color.Black
        Me._optReference_1.Location = New System.Drawing.Point(7, 27)
        Me._optReference_1.Name = "_optReference_1"
        Me._optReference_1.Size = New System.Drawing.Size(47, 17)
        Me._optReference_1.TabIndex = 0
        Me._optReference_1.Text = "Start"
        Me._optReference_1.UseVisualStyleBackColor = True
        '
        'fraSampleType
        '
        Me.fraSampleType.Controls.Add(Me.cboSampleType)
        Me.fraSampleType.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraSampleType.ForeColor = System.Drawing.Color.Navy
        Me.fraSampleType.Location = New System.Drawing.Point(8, 131)
        Me.fraSampleType.Name = "fraSampleType"
        Me.fraSampleType.Size = New System.Drawing.Size(109, 50)
        Me.fraSampleType.TabIndex = 133
        Me.fraSampleType.TabStop = False
        Me.fraSampleType.Text = "Sample Type"
        '
        'cboSampleType
        '
        Me.cboSampleType.BackColor = System.Drawing.Color.White
        Me.cboSampleType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboSampleType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSampleType.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboSampleType.ForeColor = System.Drawing.Color.Black
        Me.cboSampleType.Location = New System.Drawing.Point(8, 19)
        Me.cboSampleType.Name = "cboSampleType"
        Me.cboSampleType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboSampleType.Size = New System.Drawing.Size(91, 21)
        Me.cboSampleType.TabIndex = 99
        '
        'fraAcqMode
        '
        Me.fraAcqMode.Controls.Add(Me.cboAcqMode)
        Me.fraAcqMode.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraAcqMode.ForeColor = System.Drawing.Color.Navy
        Me.fraAcqMode.Location = New System.Drawing.Point(8, 81)
        Me.fraAcqMode.Name = "fraAcqMode"
        Me.fraAcqMode.Size = New System.Drawing.Size(109, 50)
        Me.fraAcqMode.TabIndex = 132
        Me.fraAcqMode.TabStop = False
        Me.fraAcqMode.Text = "Acquisition Mode"
        '
        'cboAcqMode
        '
        Me.cboAcqMode.BackColor = System.Drawing.Color.White
        Me.cboAcqMode.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboAcqMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboAcqMode.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboAcqMode.ForeColor = System.Drawing.Color.Black
        Me.cboAcqMode.Location = New System.Drawing.Point(8, 19)
        Me.cboAcqMode.Name = "cboAcqMode"
        Me.cboAcqMode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboAcqMode.Size = New System.Drawing.Size(90, 21)
        Me.cboAcqMode.TabIndex = 124
        '
        'fraHorizRange
        '
        Me.fraHorizRange.Controls.Add(Me.cboHorizRange)
        Me.fraHorizRange.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraHorizRange.ForeColor = System.Drawing.Color.Navy
        Me.fraHorizRange.Location = New System.Drawing.Point(210, 14)
        Me.fraHorizRange.Name = "fraHorizRange"
        Me.fraHorizRange.Size = New System.Drawing.Size(97, 50)
        Me.fraHorizRange.TabIndex = 131
        Me.fraHorizRange.TabStop = False
        Me.fraHorizRange.Text = "Range (T/Div)"
        '
        'cboHorizRange
        '
        Me.cboHorizRange.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboHorizRange.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboHorizRange.ForeColor = System.Drawing.Color.Black
        Me.cboHorizRange.FormattingEnabled = True
        Me.cboHorizRange.Location = New System.Drawing.Point(8, 19)
        Me.cboHorizRange.Name = "cboHorizRange"
        Me.cboHorizRange.Size = New System.Drawing.Size(81, 21)
        Me.cboHorizRange.TabIndex = 0
        '
        'fraDataPoints
        '
        Me.fraDataPoints.Controls.Add(Me._optDataPoints_1)
        Me.fraDataPoints.Controls.Add(Me._optDataPoints_0)
        Me.fraDataPoints.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraDataPoints.ForeColor = System.Drawing.Color.Navy
        Me.fraDataPoints.Location = New System.Drawing.Point(123, 109)
        Me.fraDataPoints.Name = "fraDataPoints"
        Me.fraDataPoints.Size = New System.Drawing.Size(78, 72)
        Me.fraDataPoints.TabIndex = 130
        Me.fraDataPoints.TabStop = False
        Me.fraDataPoints.Text = "Data Points"
        '
        '_optDataPoints_1
        '
        Me._optDataPoints_1.AutoSize = True
        Me._optDataPoints_1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optDataPoints_1.ForeColor = System.Drawing.Color.Black
        Me._optDataPoints_1.Location = New System.Drawing.Point(6, 40)
        Me._optDataPoints_1.Name = "_optDataPoints_1"
        Me._optDataPoints_1.Size = New System.Drawing.Size(49, 17)
        Me._optDataPoints_1.TabIndex = 133
        Me._optDataPoints_1.Text = "8000"
        Me._optDataPoints_1.UseVisualStyleBackColor = True
        '
        '_optDataPoints_0
        '
        Me._optDataPoints_0.AutoSize = True
        Me._optDataPoints_0.Checked = True
        Me._optDataPoints_0.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optDataPoints_0.ForeColor = System.Drawing.Color.Black
        Me._optDataPoints_0.Location = New System.Drawing.Point(6, 19)
        Me._optDataPoints_0.Name = "_optDataPoints_0"
        Me._optDataPoints_0.Size = New System.Drawing.Size(43, 17)
        Me._optDataPoints_0.TabIndex = 132
        Me._optDataPoints_0.TabStop = True
        Me._optDataPoints_0.Text = "500"
        Me._optDataPoints_0.UseVisualStyleBackColor = True
        '
        '_chkDisplayTrace_2
        '
        Me._chkDisplayTrace_2.BackColor = System.Drawing.Color.Transparent
        Me._chkDisplayTrace_2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me._chkDisplayTrace_2.Location = New System.Drawing.Point(58, 33)
        Me._chkDisplayTrace_2.Name = "_chkDisplayTrace_2"
        Me._chkDisplayTrace_2.Size = New System.Drawing.Size(57, 17)
        Me._chkDisplayTrace_2.TabIndex = 127
        Me._chkDisplayTrace_2.Text = "MATH"
        Me._chkDisplayTrace_2.UseVisualStyleBackColor = False
        '
        '_chkDisplayTrace_3
        '
        Me._chkDisplayTrace_3.AutoSize = True
        Me._chkDisplayTrace_3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me._chkDisplayTrace_3.Location = New System.Drawing.Point(58, 56)
        Me._chkDisplayTrace_3.Name = "_chkDisplayTrace_3"
        Me._chkDisplayTrace_3.Size = New System.Drawing.Size(51, 17)
        Me._chkDisplayTrace_3.TabIndex = 128
        Me._chkDisplayTrace_3.Text = "MEM"
        Me._chkDisplayTrace_3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me._chkDisplayTrace_3.UseVisualStyleBackColor = True
        '
        '_chkDisplayTrace_1
        '
        Me._chkDisplayTrace_1.AutoSize = True
        Me._chkDisplayTrace_1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me._chkDisplayTrace_1.Location = New System.Drawing.Point(11, 56)
        Me._chkDisplayTrace_1.Name = "_chkDisplayTrace_1"
        Me._chkDisplayTrace_1.Size = New System.Drawing.Size(47, 17)
        Me._chkDisplayTrace_1.TabIndex = 126
        Me._chkDisplayTrace_1.Text = "CH2"
        Me._chkDisplayTrace_1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me._chkDisplayTrace_1.UseVisualStyleBackColor = True
        '
        '_chkDisplayTrace_0
        '
        Me._chkDisplayTrace_0.AutoSize = True
        Me._chkDisplayTrace_0.Checked = True
        Me._chkDisplayTrace_0.CheckState = System.Windows.Forms.CheckState.Checked
        Me._chkDisplayTrace_0.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me._chkDisplayTrace_0.Location = New System.Drawing.Point(11, 33)
        Me._chkDisplayTrace_0.Name = "_chkDisplayTrace_0"
        Me._chkDisplayTrace_0.Size = New System.Drawing.Size(47, 17)
        Me._chkDisplayTrace_0.TabIndex = 112
        Me._chkDisplayTrace_0.Text = "CH1"
        Me._chkDisplayTrace_0.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me._chkDisplayTrace_0.UseVisualStyleBackColor = True
        '
        'fraDisplay
        '
        Me.fraDisplay.BackColor = System.Drawing.Color.Transparent
        Me.fraDisplay.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraDisplay.ForeColor = System.Drawing.Color.Navy
        Me.fraDisplay.Location = New System.Drawing.Point(8, 14)
        Me.fraDisplay.Name = "fraDisplay"
        Me.fraDisplay.Size = New System.Drawing.Size(109, 65)
        Me.fraDisplay.TabIndex = 129
        Me.fraDisplay.TabStop = False
        Me.fraDisplay.Text = "Display"
        '
        '_tabMainFunctions_TabPage1
        '
        Me._tabMainFunctions_TabPage1.Controls.Add(Me._fraSignalFilters_0)
        Me._tabMainFunctions_TabPage1.Controls.Add(Me._fraCoupling_0)
        Me._tabMainFunctions_TabPage1.Controls.Add(Me._fraProbeAttenuation_0)
        Me._tabMainFunctions_TabPage1.Controls.Add(Me.fraOffset1)
        Me._tabMainFunctions_TabPage1.Controls.Add(Me._fraVerticalRange_0)
        Me._tabMainFunctions_TabPage1.Controls.Add(Me._cmdSaveToDisk_0)
        Me._tabMainFunctions_TabPage1.Controls.Add(Me._cmdSaveToMem_0)
        Me._tabMainFunctions_TabPage1.Location = New System.Drawing.Point(4, 40)
        Me._tabMainFunctions_TabPage1.Name = "_tabMainFunctions_TabPage1"
        Me._tabMainFunctions_TabPage1.Size = New System.Drawing.Size(472, 242)
        Me._tabMainFunctions_TabPage1.TabIndex = 1
        Me._tabMainFunctions_TabPage1.Text = "Channel 1 "
        '
        '_fraSignalFilters_0
        '
        Me._fraSignalFilters_0.Controls.Add(Me.chkHFReject1)
        Me._fraSignalFilters_0.Controls.Add(Me.chkLFReject1)
        Me._fraSignalFilters_0.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._fraSignalFilters_0.ForeColor = System.Drawing.Color.Navy
        Me._fraSignalFilters_0.Location = New System.Drawing.Point(8, 166)
        Me._fraSignalFilters_0.Name = "_fraSignalFilters_0"
        Me._fraSignalFilters_0.Size = New System.Drawing.Size(145, 69)
        Me._fraSignalFilters_0.TabIndex = 153
        Me._fraSignalFilters_0.TabStop = False
        Me._fraSignalFilters_0.Text = "Signal Filters"
        '
        'chkHFReject1
        '
        Me.chkHFReject1.AutoSize = True
        Me.chkHFReject1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkHFReject1.ForeColor = System.Drawing.Color.Black
        Me.chkHFReject1.Location = New System.Drawing.Point(9, 40)
        Me.chkHFReject1.Name = "chkHFReject1"
        Me.chkHFReject1.Size = New System.Drawing.Size(117, 17)
        Me.chkHFReject1.TabIndex = 1
        Me.chkHFReject1.Text = "HF Reject >30MHz"
        Me.chkHFReject1.UseVisualStyleBackColor = True
        '
        'chkLFReject1
        '
        Me.chkLFReject1.AutoSize = True
        Me.chkLFReject1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkLFReject1.ForeColor = System.Drawing.Color.Black
        Me.chkLFReject1.Location = New System.Drawing.Point(9, 24)
        Me.chkLFReject1.Name = "chkLFReject1"
        Me.chkLFReject1.Size = New System.Drawing.Size(112, 17)
        Me.chkLFReject1.TabIndex = 0
        Me.chkLFReject1.Text = "LF Reject <450Hz"
        Me.chkLFReject1.UseVisualStyleBackColor = True
        '
        '_fraCoupling_0
        '
        Me._fraCoupling_0.Controls.Add(Me._optCoupling1_2)
        Me._fraCoupling_0.Controls.Add(Me._optCoupling1_1)
        Me._fraCoupling_0.Controls.Add(Me._optCoupling1_0)
        Me._fraCoupling_0.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._fraCoupling_0.ForeColor = System.Drawing.Color.Navy
        Me._fraCoupling_0.Location = New System.Drawing.Point(156, 62)
        Me._fraCoupling_0.Name = "_fraCoupling_0"
        Me._fraCoupling_0.Size = New System.Drawing.Size(133, 104)
        Me._fraCoupling_0.TabIndex = 152
        Me._fraCoupling_0.TabStop = False
        Me._fraCoupling_0.Text = "Coupling"
        '
        '_optCoupling1_2
        '
        Me._optCoupling1_2.AutoSize = True
        Me._optCoupling1_2.ForeColor = System.Drawing.Color.Black
        Me._optCoupling1_2.Location = New System.Drawing.Point(8, 56)
        Me._optCoupling1_2.Name = "_optCoupling1_2"
        Me._optCoupling1_2.Size = New System.Drawing.Size(88, 17)
        Me._optCoupling1_2.TabIndex = 2
        Me._optCoupling1_2.Text = "AC (1M Ohm)"
        Me._optCoupling1_2.UseVisualStyleBackColor = True
        '
        '_optCoupling1_1
        '
        Me._optCoupling1_1.AutoSize = True
        Me._optCoupling1_1.ForeColor = System.Drawing.Color.Black
        Me._optCoupling1_1.Location = New System.Drawing.Point(8, 40)
        Me._optCoupling1_1.Name = "_optCoupling1_1"
        Me._optCoupling1_1.Size = New System.Drawing.Size(86, 17)
        Me._optCoupling1_1.TabIndex = 1
        Me._optCoupling1_1.Text = "DC (50 Ohm)"
        Me._optCoupling1_1.UseVisualStyleBackColor = True
        '
        '_optCoupling1_0
        '
        Me._optCoupling1_0.AutoSize = True
        Me._optCoupling1_0.Checked = True
        Me._optCoupling1_0.ForeColor = System.Drawing.Color.Black
        Me._optCoupling1_0.Location = New System.Drawing.Point(8, 24)
        Me._optCoupling1_0.Name = "_optCoupling1_0"
        Me._optCoupling1_0.Size = New System.Drawing.Size(89, 17)
        Me._optCoupling1_0.TabIndex = 0
        Me._optCoupling1_0.TabStop = True
        Me._optCoupling1_0.Text = "DC (1M Ohm)"
        Me._optCoupling1_0.UseVisualStyleBackColor = True
        '
        '_fraProbeAttenuation_0
        '
        Me._fraProbeAttenuation_0.Controls.Add(Me._optProbe1_3)
        Me._fraProbeAttenuation_0.Controls.Add(Me._optProbe1_2)
        Me._fraProbeAttenuation_0.Controls.Add(Me._optProbe1_1)
        Me._fraProbeAttenuation_0.Controls.Add(Me._optProbe1_0)
        Me._fraProbeAttenuation_0.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._fraProbeAttenuation_0.ForeColor = System.Drawing.Color.Navy
        Me._fraProbeAttenuation_0.Location = New System.Drawing.Point(8, 62)
        Me._fraProbeAttenuation_0.Name = "_fraProbeAttenuation_0"
        Me._fraProbeAttenuation_0.Size = New System.Drawing.Size(145, 104)
        Me._fraProbeAttenuation_0.TabIndex = 151
        Me._fraProbeAttenuation_0.TabStop = False
        Me._fraProbeAttenuation_0.Text = "Probe Attenuation"
        '
        '_optProbe1_3
        '
        Me._optProbe1_3.AutoSize = True
        Me._optProbe1_3.ForeColor = System.Drawing.Color.Black
        Me._optProbe1_3.Location = New System.Drawing.Point(8, 72)
        Me._optProbe1_3.Name = "_optProbe1_3"
        Me._optProbe1_3.Size = New System.Drawing.Size(58, 17)
        Me._optProbe1_3.TabIndex = 3
        Me._optProbe1_3.Text = "1000:1"
        Me._optProbe1_3.UseVisualStyleBackColor = True
        '
        '_optProbe1_2
        '
        Me._optProbe1_2.AutoSize = True
        Me._optProbe1_2.ForeColor = System.Drawing.Color.Black
        Me._optProbe1_2.Location = New System.Drawing.Point(8, 56)
        Me._optProbe1_2.Name = "_optProbe1_2"
        Me._optProbe1_2.Size = New System.Drawing.Size(52, 17)
        Me._optProbe1_2.TabIndex = 2
        Me._optProbe1_2.Text = "100:1"
        Me._optProbe1_2.UseVisualStyleBackColor = True
        '
        '_optProbe1_1
        '
        Me._optProbe1_1.AutoSize = True
        Me._optProbe1_1.ForeColor = System.Drawing.Color.Black
        Me._optProbe1_1.Location = New System.Drawing.Point(8, 40)
        Me._optProbe1_1.Name = "_optProbe1_1"
        Me._optProbe1_1.Size = New System.Drawing.Size(46, 17)
        Me._optProbe1_1.TabIndex = 1
        Me._optProbe1_1.Text = "10:1"
        Me._optProbe1_1.UseVisualStyleBackColor = True
        '
        '_optProbe1_0
        '
        Me._optProbe1_0.AutoSize = True
        Me._optProbe1_0.Checked = True
        Me._optProbe1_0.ForeColor = System.Drawing.Color.Black
        Me._optProbe1_0.Location = New System.Drawing.Point(8, 24)
        Me._optProbe1_0.Name = "_optProbe1_0"
        Me._optProbe1_0.Size = New System.Drawing.Size(40, 17)
        Me._optProbe1_0.TabIndex = 0
        Me._optProbe1_0.TabStop = True
        Me._optProbe1_0.Text = "1:1"
        Me._optProbe1_0.UseVisualStyleBackColor = True
        '
        'fraOffset1
        '
        Me.fraOffset1.Controls.Add(Me.SSPanel10)
        Me.fraOffset1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraOffset1.ForeColor = System.Drawing.Color.Navy
        Me.fraOffset1.Location = New System.Drawing.Point(156, 14)
        Me.fraOffset1.Name = "fraOffset1"
        Me.fraOffset1.Size = New System.Drawing.Size(133, 48)
        Me.fraOffset1.TabIndex = 150
        Me.fraOffset1.TabStop = False
        Me.fraOffset1.Text = "Voltage Offset"
        '
        'SSPanel10
        '
        Me.SSPanel10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SSPanel10.Controls.Add(Me.txtVOffset1)
        Me.SSPanel10.Controls.Add(Me.spnVOffset1)
        Me.SSPanel10.Location = New System.Drawing.Point(8, 19)
        Me.SSPanel10.Name = "SSPanel10"
        Me.SSPanel10.Size = New System.Drawing.Size(104, 20)
        Me.SSPanel10.TabIndex = 1
        '
        'txtVOffset1
        '
        Me.txtVOffset1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtVOffset1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVOffset1.Location = New System.Drawing.Point(2, 1)
        Me.txtVOffset1.Name = "txtVOffset1"
        Me.txtVOffset1.Size = New System.Drawing.Size(82, 13)
        Me.txtVOffset1.TabIndex = 1
        '
        'spnVOffset1
        '
        Me.spnVOffset1.Location = New System.Drawing.Point(86, -2)
        Me.spnVOffset1.Name = "spnVOffset1"
        Me.spnVOffset1.Range = New NationalInstruments.UI.Range(1.0R, 2048.0R)
        Me.spnVOffset1.Size = New System.Drawing.Size(16, 20)
        Me.spnVOffset1.TabIndex = 0
        Me.spnVOffset1.Value = 1.0R
        '
        '_fraVerticalRange_0
        '
        Me._fraVerticalRange_0.Controls.Add(Me._cboVertRange_0)
        Me._fraVerticalRange_0.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._fraVerticalRange_0.ForeColor = System.Drawing.Color.Navy
        Me._fraVerticalRange_0.Location = New System.Drawing.Point(8, 14)
        Me._fraVerticalRange_0.Name = "_fraVerticalRange_0"
        Me._fraVerticalRange_0.Size = New System.Drawing.Size(145, 48)
        Me._fraVerticalRange_0.TabIndex = 149
        Me._fraVerticalRange_0.TabStop = False
        Me._fraVerticalRange_0.Text = "Range (Volt/Division)"
        '
        '_cboVertRange_0
        '
        Me._cboVertRange_0.BackColor = System.Drawing.Color.White
        Me._cboVertRange_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._cboVertRange_0.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me._cboVertRange_0.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._cboVertRange_0.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboVertRange.SetIndex(Me._cboVertRange_0, CType(0, Short))
        Me._cboVertRange_0.Location = New System.Drawing.Point(10, 17)
        Me._cboVertRange_0.Name = "_cboVertRange_0"
        Me._cboVertRange_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cboVertRange_0.Size = New System.Drawing.Size(124, 21)
        Me._cboVertRange_0.TabIndex = 147
        '
        '_cmdSaveToDisk_0
        '
        Me._cmdSaveToDisk_0.BackColor = System.Drawing.SystemColors.Control
        Me._cmdSaveToDisk_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._cmdSaveToDisk_0.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._cmdSaveToDisk_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSaveToDisk.SetIndex(Me._cmdSaveToDisk_0, CType(0, Short))
        Me._cmdSaveToDisk_0.Location = New System.Drawing.Point(188, 214)
        Me._cmdSaveToDisk_0.Name = "_cmdSaveToDisk_0"
        Me._cmdSaveToDisk_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cmdSaveToDisk_0.Size = New System.Drawing.Size(100, 23)
        Me._cmdSaveToDisk_0.TabIndex = 132
        Me._cmdSaveToDisk_0.Text = "Save to Disk ..."
        Me._cmdSaveToDisk_0.UseVisualStyleBackColor = False
        '
        '_cmdSaveToMem_0
        '
        Me._cmdSaveToMem_0.BackColor = System.Drawing.SystemColors.Control
        Me._cmdSaveToMem_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._cmdSaveToMem_0.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._cmdSaveToMem_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSaveToMem.SetIndex(Me._cmdSaveToMem_0, CType(0, Short))
        Me._cmdSaveToMem_0.Location = New System.Drawing.Point(188, 186)
        Me._cmdSaveToMem_0.Name = "_cmdSaveToMem_0"
        Me._cmdSaveToMem_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cmdSaveToMem_0.Size = New System.Drawing.Size(100, 23)
        Me._cmdSaveToMem_0.TabIndex = 133
        Me._cmdSaveToMem_0.Text = "Save to Memory"
        Me._cmdSaveToMem_0.UseVisualStyleBackColor = False
        '
        '_tabMainFunctions_TabPage2
        '
        Me._tabMainFunctions_TabPage2.Controls.Add(Me._fraCoupling_1)
        Me._tabMainFunctions_TabPage2.Controls.Add(Me._fraSignalFilters_1)
        Me._tabMainFunctions_TabPage2.Controls.Add(Me._fraProbeAttenuation_1)
        Me._tabMainFunctions_TabPage2.Controls.Add(Me.fraOffset2)
        Me._tabMainFunctions_TabPage2.Controls.Add(Me._fraVerticalRange_1)
        Me._tabMainFunctions_TabPage2.Controls.Add(Me._cmdSaveToMem_1)
        Me._tabMainFunctions_TabPage2.Controls.Add(Me._cmdSaveToDisk_1)
        Me._tabMainFunctions_TabPage2.Location = New System.Drawing.Point(4, 40)
        Me._tabMainFunctions_TabPage2.Name = "_tabMainFunctions_TabPage2"
        Me._tabMainFunctions_TabPage2.Size = New System.Drawing.Size(472, 242)
        Me._tabMainFunctions_TabPage2.TabIndex = 2
        Me._tabMainFunctions_TabPage2.Text = "Channel 2 "
        '
        '_fraCoupling_1
        '
        Me._fraCoupling_1.Controls.Add(Me._optCoupling2_2)
        Me._fraCoupling_1.Controls.Add(Me._optCoupling2_1)
        Me._fraCoupling_1.Controls.Add(Me._optCoupling2_0)
        Me._fraCoupling_1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._fraCoupling_1.ForeColor = System.Drawing.Color.Navy
        Me._fraCoupling_1.Location = New System.Drawing.Point(156, 62)
        Me._fraCoupling_1.Name = "_fraCoupling_1"
        Me._fraCoupling_1.Size = New System.Drawing.Size(133, 104)
        Me._fraCoupling_1.TabIndex = 172
        Me._fraCoupling_1.TabStop = False
        Me._fraCoupling_1.Text = "Coupling"
        '
        '_optCoupling2_2
        '
        Me._optCoupling2_2.AutoSize = True
        Me._optCoupling2_2.ForeColor = System.Drawing.Color.Black
        Me._optCoupling2_2.Location = New System.Drawing.Point(8, 56)
        Me._optCoupling2_2.Name = "_optCoupling2_2"
        Me._optCoupling2_2.Size = New System.Drawing.Size(88, 17)
        Me._optCoupling2_2.TabIndex = 2
        Me._optCoupling2_2.Text = "AC (1M Ohm)"
        Me._optCoupling2_2.UseVisualStyleBackColor = True
        '
        '_optCoupling2_1
        '
        Me._optCoupling2_1.AutoSize = True
        Me._optCoupling2_1.ForeColor = System.Drawing.Color.Black
        Me._optCoupling2_1.Location = New System.Drawing.Point(8, 40)
        Me._optCoupling2_1.Name = "_optCoupling2_1"
        Me._optCoupling2_1.Size = New System.Drawing.Size(86, 17)
        Me._optCoupling2_1.TabIndex = 1
        Me._optCoupling2_1.Text = "DC (50 Ohm)"
        Me._optCoupling2_1.UseVisualStyleBackColor = True
        '
        '_optCoupling2_0
        '
        Me._optCoupling2_0.AutoSize = True
        Me._optCoupling2_0.Checked = True
        Me._optCoupling2_0.ForeColor = System.Drawing.Color.Black
        Me._optCoupling2_0.Location = New System.Drawing.Point(8, 24)
        Me._optCoupling2_0.Name = "_optCoupling2_0"
        Me._optCoupling2_0.Size = New System.Drawing.Size(89, 17)
        Me._optCoupling2_0.TabIndex = 0
        Me._optCoupling2_0.TabStop = True
        Me._optCoupling2_0.Text = "DC (1M Ohm)"
        Me._optCoupling2_0.UseVisualStyleBackColor = True
        '
        '_fraSignalFilters_1
        '
        Me._fraSignalFilters_1.Controls.Add(Me.chkHFReject2)
        Me._fraSignalFilters_1.Controls.Add(Me.chkLFReject2)
        Me._fraSignalFilters_1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._fraSignalFilters_1.ForeColor = System.Drawing.Color.Navy
        Me._fraSignalFilters_1.Location = New System.Drawing.Point(8, 166)
        Me._fraSignalFilters_1.Name = "_fraSignalFilters_1"
        Me._fraSignalFilters_1.Size = New System.Drawing.Size(145, 69)
        Me._fraSignalFilters_1.TabIndex = 171
        Me._fraSignalFilters_1.TabStop = False
        Me._fraSignalFilters_1.Text = "Signal Filters"
        '
        'chkHFReject2
        '
        Me.chkHFReject2.AutoSize = True
        Me.chkHFReject2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkHFReject2.ForeColor = System.Drawing.Color.Black
        Me.chkHFReject2.Location = New System.Drawing.Point(9, 40)
        Me.chkHFReject2.Name = "chkHFReject2"
        Me.chkHFReject2.Size = New System.Drawing.Size(117, 17)
        Me.chkHFReject2.TabIndex = 1
        Me.chkHFReject2.Text = "HF Reject >30MHz"
        Me.chkHFReject2.UseVisualStyleBackColor = True
        '
        'chkLFReject2
        '
        Me.chkLFReject2.AutoSize = True
        Me.chkLFReject2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkLFReject2.ForeColor = System.Drawing.Color.Black
        Me.chkLFReject2.Location = New System.Drawing.Point(9, 24)
        Me.chkLFReject2.Name = "chkLFReject2"
        Me.chkLFReject2.Size = New System.Drawing.Size(112, 17)
        Me.chkLFReject2.TabIndex = 0
        Me.chkLFReject2.Text = "LF Reject <450Hz"
        Me.chkLFReject2.UseVisualStyleBackColor = True
        '
        '_fraProbeAttenuation_1
        '
        Me._fraProbeAttenuation_1.Controls.Add(Me._optProbe2_3)
        Me._fraProbeAttenuation_1.Controls.Add(Me._optProbe2_2)
        Me._fraProbeAttenuation_1.Controls.Add(Me._optProbe2_1)
        Me._fraProbeAttenuation_1.Controls.Add(Me._optProbe2_0)
        Me._fraProbeAttenuation_1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._fraProbeAttenuation_1.ForeColor = System.Drawing.Color.Navy
        Me._fraProbeAttenuation_1.Location = New System.Drawing.Point(8, 62)
        Me._fraProbeAttenuation_1.Name = "_fraProbeAttenuation_1"
        Me._fraProbeAttenuation_1.Size = New System.Drawing.Size(145, 104)
        Me._fraProbeAttenuation_1.TabIndex = 170
        Me._fraProbeAttenuation_1.TabStop = False
        Me._fraProbeAttenuation_1.Text = "Probe Attenuation"
        '
        '_optProbe2_3
        '
        Me._optProbe2_3.AutoSize = True
        Me._optProbe2_3.ForeColor = System.Drawing.Color.Black
        Me._optProbe2_3.Location = New System.Drawing.Point(8, 72)
        Me._optProbe2_3.Name = "_optProbe2_3"
        Me._optProbe2_3.Size = New System.Drawing.Size(58, 17)
        Me._optProbe2_3.TabIndex = 3
        Me._optProbe2_3.Text = "1000:1"
        Me._optProbe2_3.UseVisualStyleBackColor = True
        '
        '_optProbe2_2
        '
        Me._optProbe2_2.AutoSize = True
        Me._optProbe2_2.ForeColor = System.Drawing.Color.Black
        Me._optProbe2_2.Location = New System.Drawing.Point(8, 56)
        Me._optProbe2_2.Name = "_optProbe2_2"
        Me._optProbe2_2.Size = New System.Drawing.Size(52, 17)
        Me._optProbe2_2.TabIndex = 2
        Me._optProbe2_2.Text = "100:1"
        Me._optProbe2_2.UseVisualStyleBackColor = True
        '
        '_optProbe2_1
        '
        Me._optProbe2_1.AutoSize = True
        Me._optProbe2_1.ForeColor = System.Drawing.Color.Black
        Me._optProbe2_1.Location = New System.Drawing.Point(8, 40)
        Me._optProbe2_1.Name = "_optProbe2_1"
        Me._optProbe2_1.Size = New System.Drawing.Size(46, 17)
        Me._optProbe2_1.TabIndex = 1
        Me._optProbe2_1.Text = "10:1"
        Me._optProbe2_1.UseVisualStyleBackColor = True
        '
        '_optProbe2_0
        '
        Me._optProbe2_0.AutoSize = True
        Me._optProbe2_0.Checked = True
        Me._optProbe2_0.ForeColor = System.Drawing.Color.Black
        Me._optProbe2_0.Location = New System.Drawing.Point(8, 24)
        Me._optProbe2_0.Name = "_optProbe2_0"
        Me._optProbe2_0.Size = New System.Drawing.Size(40, 17)
        Me._optProbe2_0.TabIndex = 0
        Me._optProbe2_0.TabStop = True
        Me._optProbe2_0.Text = "1:1"
        Me._optProbe2_0.UseVisualStyleBackColor = True
        '
        'fraOffset2
        '
        Me.fraOffset2.Controls.Add(Me.SSPanel11)
        Me.fraOffset2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraOffset2.ForeColor = System.Drawing.Color.Navy
        Me.fraOffset2.Location = New System.Drawing.Point(156, 14)
        Me.fraOffset2.Name = "fraOffset2"
        Me.fraOffset2.Size = New System.Drawing.Size(133, 48)
        Me.fraOffset2.TabIndex = 169
        Me.fraOffset2.TabStop = False
        Me.fraOffset2.Text = "Voltage Offset"
        '
        'SSPanel11
        '
        Me.SSPanel11.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SSPanel11.Controls.Add(Me.txtVOffset2)
        Me.SSPanel11.Controls.Add(Me.spnVOffset2)
        Me.SSPanel11.Location = New System.Drawing.Point(8, 19)
        Me.SSPanel11.Name = "SSPanel11"
        Me.SSPanel11.Size = New System.Drawing.Size(104, 20)
        Me.SSPanel11.TabIndex = 1
        '
        'txtVOffset2
        '
        Me.txtVOffset2.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtVOffset2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVOffset2.Location = New System.Drawing.Point(2, 1)
        Me.txtVOffset2.Name = "txtVOffset2"
        Me.txtVOffset2.Size = New System.Drawing.Size(82, 13)
        Me.txtVOffset2.TabIndex = 1
        '
        'spnVOffset2
        '
        Me.spnVOffset2.Location = New System.Drawing.Point(86, -2)
        Me.spnVOffset2.Name = "spnVOffset2"
        Me.spnVOffset2.Range = New NationalInstruments.UI.Range(1.0R, 2048.0R)
        Me.spnVOffset2.Size = New System.Drawing.Size(16, 20)
        Me.spnVOffset2.TabIndex = 0
        Me.spnVOffset2.Value = 1.0R
        '
        '_fraVerticalRange_1
        '
        Me._fraVerticalRange_1.Controls.Add(Me._cboVertRange_1)
        Me._fraVerticalRange_1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._fraVerticalRange_1.ForeColor = System.Drawing.Color.Navy
        Me._fraVerticalRange_1.Location = New System.Drawing.Point(8, 14)
        Me._fraVerticalRange_1.Name = "_fraVerticalRange_1"
        Me._fraVerticalRange_1.Size = New System.Drawing.Size(145, 48)
        Me._fraVerticalRange_1.TabIndex = 168
        Me._fraVerticalRange_1.TabStop = False
        Me._fraVerticalRange_1.Text = "Range (Volt/Division)"
        '
        '_cboVertRange_1
        '
        Me._cboVertRange_1.BackColor = System.Drawing.Color.White
        Me._cboVertRange_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._cboVertRange_1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me._cboVertRange_1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._cboVertRange_1.ForeColor = System.Drawing.SystemColors.WindowText
        Me._cboVertRange_1.Location = New System.Drawing.Point(10, 17)
        Me._cboVertRange_1.Name = "_cboVertRange_1"
        Me._cboVertRange_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cboVertRange_1.Size = New System.Drawing.Size(124, 21)
        Me._cboVertRange_1.TabIndex = 147
        '
        '_cmdSaveToMem_1
        '
        Me._cmdSaveToMem_1.BackColor = System.Drawing.SystemColors.Control
        Me._cmdSaveToMem_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._cmdSaveToMem_1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._cmdSaveToMem_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSaveToMem.SetIndex(Me._cmdSaveToMem_1, CType(1, Short))
        Me._cmdSaveToMem_1.Location = New System.Drawing.Point(188, 186)
        Me._cmdSaveToMem_1.Name = "_cmdSaveToMem_1"
        Me._cmdSaveToMem_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cmdSaveToMem_1.Size = New System.Drawing.Size(100, 23)
        Me._cmdSaveToMem_1.TabIndex = 153
        Me._cmdSaveToMem_1.Text = "Save to Memory"
        Me._cmdSaveToMem_1.UseVisualStyleBackColor = False
        '
        '_cmdSaveToDisk_1
        '
        Me._cmdSaveToDisk_1.BackColor = System.Drawing.SystemColors.Control
        Me._cmdSaveToDisk_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._cmdSaveToDisk_1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._cmdSaveToDisk_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSaveToDisk.SetIndex(Me._cmdSaveToDisk_1, CType(1, Short))
        Me._cmdSaveToDisk_1.Location = New System.Drawing.Point(188, 214)
        Me._cmdSaveToDisk_1.Name = "_cmdSaveToDisk_1"
        Me._cmdSaveToDisk_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cmdSaveToDisk_1.Size = New System.Drawing.Size(100, 23)
        Me._cmdSaveToDisk_1.TabIndex = 152
        Me._cmdSaveToDisk_1.Text = "Save to Disk ..."
        Me._cmdSaveToDisk_1.UseVisualStyleBackColor = False
        '
        '_tabMainFunctions_TabPage3
        '
        Me._tabMainFunctions_TabPage3.Controls.Add(Me._fraFunction_2)
        Me._tabMainFunctions_TabPage3.Controls.Add(Me.cmdHelp)
        Me._tabMainFunctions_TabPage3.Location = New System.Drawing.Point(4, 40)
        Me._tabMainFunctions_TabPage3.Name = "_tabMainFunctions_TabPage3"
        Me._tabMainFunctions_TabPage3.Size = New System.Drawing.Size(472, 242)
        Me._tabMainFunctions_TabPage3.TabIndex = 3
        Me._tabMainFunctions_TabPage3.Text = "Measure "
        '
        '_fraFunction_2
        '
        Me._fraFunction_2.Controls.Add(Me.fraDelayParam)
        Me._fraFunction_2.Controls.Add(Me.tbrMeasFunction)
        Me._fraFunction_2.Controls.Add(Me.cboMeasSour)
        Me._fraFunction_2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._fraFunction_2.ForeColor = System.Drawing.Color.Navy
        Me._fraFunction_2.Location = New System.Drawing.Point(8, 14)
        Me._fraFunction_2.Name = "_fraFunction_2"
        Me._fraFunction_2.Size = New System.Drawing.Size(292, 193)
        Me._fraFunction_2.TabIndex = 176
        Me._fraFunction_2.TabStop = False
        Me._fraFunction_2.Text = "Function"
        '
        'fraDelayParam
        '
        Me.fraDelayParam.Controls.Add(Me.Panel1)
        Me.fraDelayParam.Controls.Add(Me.SSPanel5)
        Me.fraDelayParam.Controls.Add(Me.SSPanel3)
        Me.fraDelayParam.Controls.Add(Me.Picture4)
        Me.fraDelayParam.Controls.Add(Me.SSPanel4)
        Me.fraDelayParam.Controls.Add(Me.SSPanel1)
        Me.fraDelayParam.Controls.Add(Me.lblStopEdgeCap)
        Me.fraDelayParam.Controls.Add(Me.lblStartEdgeCap)
        Me.fraDelayParam.Controls.Add(Me.Picture3)
        Me.fraDelayParam.Controls.Add(Me.Line1)
        Me.fraDelayParam.Controls.Add(Me.Picture2)
        Me.fraDelayParam.Controls.Add(Me.cboStopSource)
        Me.fraDelayParam.Controls.Add(Me.lblStopSource)
        Me.fraDelayParam.Controls.Add(Me.cboStartSource)
        Me.fraDelayParam.Controls.Add(Me.lblStartSource)
        Me.fraDelayParam.Controls.Add(Me.lblStop)
        Me.fraDelayParam.Controls.Add(Me.lblStar)
        Me.fraDelayParam.Location = New System.Drawing.Point(136, 8)
        Me.fraDelayParam.Name = "fraDelayParam"
        Me.fraDelayParam.Size = New System.Drawing.Size(141, 177)
        Me.fraDelayParam.TabIndex = 176
        Me.fraDelayParam.Visible = False
        '
        'SSPanel5
        '
        Me.SSPanel5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SSPanel5.Controls.Add(Me.txtStopLevel)
        Me.SSPanel5.Controls.Add(Me.spnStopLevel)
        Me.SSPanel5.Location = New System.Drawing.Point(74, 154)
        Me.SSPanel5.Name = "SSPanel5"
        Me.SSPanel5.Size = New System.Drawing.Size(55, 20)
        Me.SSPanel5.TabIndex = 219
        '
        'txtStopLevel
        '
        Me.txtStopLevel.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtStopLevel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStopLevel.Location = New System.Drawing.Point(2, 1)
        Me.txtStopLevel.Name = "txtStopLevel"
        Me.txtStopLevel.Size = New System.Drawing.Size(33, 13)
        Me.txtStopLevel.TabIndex = 1
        '
        'spnStopLevel
        '
        Me.spnStopLevel.Location = New System.Drawing.Point(37, -2)
        Me.spnStopLevel.Name = "spnStopLevel"
        Me.spnStopLevel.Range = New NationalInstruments.UI.Range(0.0R, 100.0R)
        Me.spnStopLevel.Size = New System.Drawing.Size(16, 20)
        Me.spnStopLevel.TabIndex = 0
        Me.spnStopLevel.Value = 1.0R
        '
        'SSPanel3
        '
        Me.SSPanel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SSPanel3.Controls.Add(Me.txtStartLevel)
        Me.SSPanel3.Controls.Add(Me.spnStartLevel)
        Me.SSPanel3.Location = New System.Drawing.Point(0, 154)
        Me.SSPanel3.Name = "SSPanel3"
        Me.SSPanel3.Size = New System.Drawing.Size(55, 20)
        Me.SSPanel3.TabIndex = 218
        '
        'txtStartLevel
        '
        Me.txtStartLevel.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtStartLevel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStartLevel.Location = New System.Drawing.Point(2, 1)
        Me.txtStartLevel.Name = "txtStartLevel"
        Me.txtStartLevel.Size = New System.Drawing.Size(33, 13)
        Me.txtStartLevel.TabIndex = 1
        '
        'spnStartLevel
        '
        Me.spnStartLevel.Location = New System.Drawing.Point(37, -2)
        Me.spnStartLevel.Name = "spnStartLevel"
        Me.spnStartLevel.Range = New NationalInstruments.UI.Range(0.0R, 100.0R)
        Me.spnStartLevel.Size = New System.Drawing.Size(16, 20)
        Me.spnStartLevel.TabIndex = 0
        Me.spnStartLevel.Value = 1.0R
        '
        'Picture4
        '
        Me.Picture4.Controls.Add(Me._optStartLevel_1)
        Me.Picture4.Controls.Add(Me._optStartLevel_0)
        Me.Picture4.Controls.Add(Me.lblStartLevelCap)
        Me.Picture4.Location = New System.Drawing.Point(0, 120)
        Me.Picture4.Name = "Picture4"
        Me.Picture4.Size = New System.Drawing.Size(63, 30)
        Me.Picture4.TabIndex = 217
        '
        '_optStartLevel_1
        '
        Me._optStartLevel_1.ForeColor = System.Drawing.Color.Black
        Me._optStartLevel_1.Location = New System.Drawing.Point(33, 12)
        Me._optStartLevel_1.Name = "_optStartLevel_1"
        Me._optStartLevel_1.Size = New System.Drawing.Size(28, 17)
        Me._optStartLevel_1.TabIndex = 215
        Me._optStartLevel_1.Text = "%"
        Me._optStartLevel_1.UseVisualStyleBackColor = True
        '
        '_optStartLevel_0
        '
        Me._optStartLevel_0.Checked = True
        Me._optStartLevel_0.ForeColor = System.Drawing.Color.Black
        Me._optStartLevel_0.Location = New System.Drawing.Point(0, 12)
        Me._optStartLevel_0.Name = "_optStartLevel_0"
        Me._optStartLevel_0.Size = New System.Drawing.Size(28, 17)
        Me._optStartLevel_0.TabIndex = 214
        Me._optStartLevel_0.TabStop = True
        Me._optStartLevel_0.Text = "V"
        Me._optStartLevel_0.UseVisualStyleBackColor = True
        '
        'lblStartLevelCap
        '
        Me.lblStartLevelCap.BackColor = System.Drawing.SystemColors.Control
        Me.lblStartLevelCap.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblStartLevelCap.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStartLevelCap.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblStartLevelCap.Location = New System.Drawing.Point(0, 0)
        Me.lblStartLevelCap.Name = "lblStartLevelCap"
        Me.lblStartLevelCap.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblStartLevelCap.Size = New System.Drawing.Size(46, 14)
        Me.lblStartLevelCap.TabIndex = 180
        Me.lblStartLevelCap.Text = "Level"
        '
        'SSPanel4
        '
        Me.SSPanel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SSPanel4.Controls.Add(Me.txtStopEdge)
        Me.SSPanel4.Controls.Add(Me.spnStopEdge)
        Me.SSPanel4.Location = New System.Drawing.Point(74, 98)
        Me.SSPanel4.Name = "SSPanel4"
        Me.SSPanel4.Size = New System.Drawing.Size(55, 20)
        Me.SSPanel4.TabIndex = 215
        '
        'txtStopEdge
        '
        Me.txtStopEdge.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtStopEdge.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStopEdge.Location = New System.Drawing.Point(2, 1)
        Me.txtStopEdge.Name = "txtStopEdge"
        Me.txtStopEdge.Size = New System.Drawing.Size(33, 13)
        Me.txtStopEdge.TabIndex = 1
        '
        'spnStopEdge
        '
        Me.spnStopEdge.Location = New System.Drawing.Point(37, -2)
        Me.spnStopEdge.Name = "spnStopEdge"
        Me.spnStopEdge.Range = New NationalInstruments.UI.Range(0.0R, 100.0R)
        Me.spnStopEdge.Size = New System.Drawing.Size(16, 20)
        Me.spnStopEdge.TabIndex = 0
        Me.spnStopEdge.Value = 1.0R
        '
        'SSPanel1
        '
        Me.SSPanel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SSPanel1.Controls.Add(Me.txtStartEdge)
        Me.SSPanel1.Controls.Add(Me.spnStartEdge)
        Me.SSPanel1.Location = New System.Drawing.Point(0, 98)
        Me.SSPanel1.Name = "SSPanel1"
        Me.SSPanel1.Size = New System.Drawing.Size(55, 20)
        Me.SSPanel1.TabIndex = 214
        '
        'txtStartEdge
        '
        Me.txtStartEdge.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtStartEdge.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStartEdge.Location = New System.Drawing.Point(2, 1)
        Me.txtStartEdge.Name = "txtStartEdge"
        Me.txtStartEdge.Size = New System.Drawing.Size(33, 13)
        Me.txtStartEdge.TabIndex = 1
        '
        'spnStartEdge
        '
        Me.spnStartEdge.Location = New System.Drawing.Point(37, -2)
        Me.spnStartEdge.Name = "spnStartEdge"
        Me.spnStartEdge.Range = New NationalInstruments.UI.Range(0.0R, 100.0R)
        Me.spnStartEdge.Size = New System.Drawing.Size(16, 20)
        Me.spnStartEdge.TabIndex = 0
        Me.spnStartEdge.Value = 1.0R
        '
        'lblStopEdgeCap
        '
        Me.lblStopEdgeCap.BackColor = System.Drawing.SystemColors.Control
        Me.lblStopEdgeCap.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblStopEdgeCap.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStopEdgeCap.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblStopEdgeCap.Location = New System.Drawing.Point(74, 84)
        Me.lblStopEdgeCap.Name = "lblStopEdgeCap"
        Me.lblStopEdgeCap.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblStopEdgeCap.Size = New System.Drawing.Size(46, 14)
        Me.lblStopEdgeCap.TabIndex = 213
        Me.lblStopEdgeCap.Text = "Edge #"
        '
        'lblStartEdgeCap
        '
        Me.lblStartEdgeCap.BackColor = System.Drawing.SystemColors.Control
        Me.lblStartEdgeCap.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblStartEdgeCap.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStartEdgeCap.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblStartEdgeCap.Location = New System.Drawing.Point(0, 84)
        Me.lblStartEdgeCap.Name = "lblStartEdgeCap"
        Me.lblStartEdgeCap.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblStartEdgeCap.Size = New System.Drawing.Size(46, 14)
        Me.lblStartEdgeCap.TabIndex = 212
        Me.lblStartEdgeCap.Text = "Edge #"
        '
        'Picture3
        '
        Me.Picture3.Controls.Add(Me._optStopSlope_1)
        Me.Picture3.Controls.Add(Me._optStopSlope_0)
        Me.Picture3.Controls.Add(Me.lblStopSlope)
        Me.Picture3.Location = New System.Drawing.Point(76, 52)
        Me.Picture3.Name = "Picture3"
        Me.Picture3.Size = New System.Drawing.Size(58, 33)
        Me.Picture3.TabIndex = 211
        '
        '_optStopSlope_1
        '
        Me._optStopSlope_1.ForeColor = System.Drawing.Color.Black
        Me._optStopSlope_1.Location = New System.Drawing.Point(33, 12)
        Me._optStopSlope_1.Name = "_optStopSlope_1"
        Me._optStopSlope_1.Size = New System.Drawing.Size(28, 17)
        Me._optStopSlope_1.TabIndex = 215
        Me._optStopSlope_1.TabStop = True
        Me._optStopSlope_1.Text = "-"
        Me._optStopSlope_1.UseVisualStyleBackColor = True
        '
        '_optStopSlope_0
        '
        Me._optStopSlope_0.ForeColor = System.Drawing.Color.Black
        Me._optStopSlope_0.Location = New System.Drawing.Point(0, 12)
        Me._optStopSlope_0.Name = "_optStopSlope_0"
        Me._optStopSlope_0.Size = New System.Drawing.Size(28, 17)
        Me._optStopSlope_0.TabIndex = 214
        Me._optStopSlope_0.TabStop = True
        Me._optStopSlope_0.Text = "+"
        Me._optStopSlope_0.UseVisualStyleBackColor = True
        '
        'lblStopSlope
        '
        Me.lblStopSlope.BackColor = System.Drawing.SystemColors.Control
        Me.lblStopSlope.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblStopSlope.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStopSlope.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblStopSlope.Location = New System.Drawing.Point(0, 0)
        Me.lblStopSlope.Name = "lblStopSlope"
        Me.lblStopSlope.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblStopSlope.Size = New System.Drawing.Size(46, 14)
        Me.lblStopSlope.TabIndex = 182
        Me.lblStopSlope.Text = "Slope"
        '
        'Line1
        '
        Me.Line1.BackColor = System.Drawing.SystemColors.WindowText
        Me.Line1.Location = New System.Drawing.Point(67, 5)
        Me.Line1.Name = "Line1"
        Me.Line1.Size = New System.Drawing.Size(1, 169)
        Me.Line1.TabIndex = 210
        '
        'Picture2
        '
        Me.Picture2.Controls.Add(Me._optStartSlope_1)
        Me.Picture2.Controls.Add(Me._optStartSlope_0)
        Me.Picture2.Controls.Add(Me.lblStartSlope)
        Me.Picture2.Location = New System.Drawing.Point(0, 52)
        Me.Picture2.Name = "Picture2"
        Me.Picture2.Size = New System.Drawing.Size(58, 33)
        Me.Picture2.TabIndex = 208
        '
        '_optStartSlope_1
        '
        Me._optStartSlope_1.ForeColor = System.Drawing.Color.Black
        Me._optStartSlope_1.Location = New System.Drawing.Point(33, 12)
        Me._optStartSlope_1.Name = "_optStartSlope_1"
        Me._optStartSlope_1.Size = New System.Drawing.Size(28, 17)
        Me._optStartSlope_1.TabIndex = 214
        Me._optStartSlope_1.Text = "-"
        Me._optStartSlope_1.UseVisualStyleBackColor = True
        '
        '_optStartSlope_0
        '
        Me._optStartSlope_0.Checked = True
        Me._optStartSlope_0.ForeColor = System.Drawing.Color.Black
        Me._optStartSlope_0.Location = New System.Drawing.Point(0, 12)
        Me._optStartSlope_0.Name = "_optStartSlope_0"
        Me._optStartSlope_0.Size = New System.Drawing.Size(28, 17)
        Me._optStartSlope_0.TabIndex = 213
        Me._optStartSlope_0.TabStop = True
        Me._optStartSlope_0.Text = "+"
        Me._optStartSlope_0.UseVisualStyleBackColor = True
        '
        'lblStartSlope
        '
        Me.lblStartSlope.BackColor = System.Drawing.SystemColors.Control
        Me.lblStartSlope.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblStartSlope.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStartSlope.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblStartSlope.Location = New System.Drawing.Point(0, 0)
        Me.lblStartSlope.Name = "lblStartSlope"
        Me.lblStartSlope.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblStartSlope.Size = New System.Drawing.Size(46, 14)
        Me.lblStartSlope.TabIndex = 212
        Me.lblStartSlope.Text = "Slope"
        '
        'cboStopSource
        '
        Me.cboStopSource.BackColor = System.Drawing.Color.White
        Me.cboStopSource.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboStopSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStopSource.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboStopSource.ForeColor = System.Drawing.Color.Black
        Me.cboStopSource.Location = New System.Drawing.Point(74, 27)
        Me.cboStopSource.Name = "cboStopSource"
        Me.cboStopSource.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboStopSource.Size = New System.Drawing.Size(62, 21)
        Me.cboStopSource.TabIndex = 189
        '
        'lblStopSource
        '
        Me.lblStopSource.BackColor = System.Drawing.SystemColors.Control
        Me.lblStopSource.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblStopSource.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStopSource.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblStopSource.Location = New System.Drawing.Point(74, 13)
        Me.lblStopSource.Name = "lblStopSource"
        Me.lblStopSource.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblStopSource.Size = New System.Drawing.Size(46, 14)
        Me.lblStopSource.TabIndex = 207
        Me.lblStopSource.Text = "Source"
        '
        'cboStartSource
        '
        Me.cboStartSource.BackColor = System.Drawing.Color.White
        Me.cboStartSource.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboStartSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStartSource.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboStartSource.ForeColor = System.Drawing.Color.Black
        Me.cboStartSource.Location = New System.Drawing.Point(0, 27)
        Me.cboStartSource.Name = "cboStartSource"
        Me.cboStartSource.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboStartSource.Size = New System.Drawing.Size(62, 21)
        Me.cboStartSource.TabIndex = 188
        '
        'lblStartSource
        '
        Me.lblStartSource.BackColor = System.Drawing.SystemColors.Control
        Me.lblStartSource.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblStartSource.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStartSource.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblStartSource.Location = New System.Drawing.Point(0, 13)
        Me.lblStartSource.Name = "lblStartSource"
        Me.lblStartSource.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblStartSource.Size = New System.Drawing.Size(46, 14)
        Me.lblStartSource.TabIndex = 206
        Me.lblStartSource.Text = "Source"
        '
        'lblStop
        '
        Me.lblStop.AutoSize = True
        Me.lblStop.BackColor = System.Drawing.SystemColors.Control
        Me.lblStop.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblStop.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStop.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblStop.Location = New System.Drawing.Point(86, 0)
        Me.lblStop.Name = "lblStop"
        Me.lblStop.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblStop.Size = New System.Drawing.Size(29, 13)
        Me.lblStop.TabIndex = 205
        Me.lblStop.Text = "Stop"
        '
        'lblStar
        '
        Me.lblStar.AutoSize = True
        Me.lblStar.BackColor = System.Drawing.SystemColors.Control
        Me.lblStar.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblStar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblStar.Location = New System.Drawing.Point(15, 0)
        Me.lblStar.Name = "lblStar"
        Me.lblStar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblStar.Size = New System.Drawing.Size(29, 13)
        Me.lblStar.TabIndex = 204
        Me.lblStar.Text = "Start"
        '
        'tbrMeasFunction
        '
        Me.tbrMeasFunction.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.Dummy, Me.DCRMS, Me.Button2, Me.Button3, Me.Button4, Me.Button5, Me.Button6, Me.Button7, Me.Button8, Me.Button9, Me.Button10, Me.Button11, Me.Button12, Me.Button13, Me.Button14, Me.Button15, Me.Button16, Me.Button17, Me.Button18, Me.Button19, Me.Button20})
        Me.tbrMeasFunction.ButtonSize = New System.Drawing.Size(24, 24)
        Me.tbrMeasFunction.Divider = False
        Me.tbrMeasFunction.Dock = System.Windows.Forms.DockStyle.None
        Me.tbrMeasFunction.DropDownArrows = True
        Me.tbrMeasFunction.ImageList = Me.imgFunctions
        Me.tbrMeasFunction.Location = New System.Drawing.Point(5, 20)
        Me.tbrMeasFunction.Name = "tbrMeasFunction"
        Me.tbrMeasFunction.ShowToolTips = True
        Me.tbrMeasFunction.Size = New System.Drawing.Size(128, 154)
        Me.tbrMeasFunction.TabIndex = 175
        '
        'Dummy
        '
        Me.Dummy.Name = "Dummy"
        Me.Dummy.Visible = False
        '
        'DCRMS
        '
        Me.DCRMS.ImageIndex = 20
        Me.DCRMS.Name = "DCRMS"
        Me.DCRMS.ToolTipText = "DC RMS Voltage"
        '
        'Button2
        '
        Me.Button2.ImageIndex = 13
        Me.Button2.Name = "Button2"
        Me.Button2.ToolTipText = "AC RMS Voltage"
        '
        'Button3
        '
        Me.Button3.ImageIndex = 9
        Me.Button3.Name = "Button3"
        Me.Button3.ToolTipText = "Period"
        '
        'Button4
        '
        Me.Button4.ImageIndex = 2
        Me.Button4.Name = "Button4"
        Me.Button4.ToolTipText = "Frequency"
        '
        'Button5
        '
        Me.Button5.ImageIndex = 10
        Me.Button5.Name = "Button5"
        Me.Button5.ToolTipText = "Positive Pulse Width (PWidth)"
        '
        'Button6
        '
        Me.Button6.ImageIndex = 6
        Me.Button6.Name = "Button6"
        Me.Button6.ToolTipText = "Negative Pulse Width (NWidth)"
        '
        'Button7
        '
        Me.Button7.ImageIndex = 1
        Me.Button7.Name = "Button7"
        Me.Button7.ToolTipText = "Duty Cycle"
        '
        'Button8
        '
        Me.Button8.ImageIndex = 0
        Me.Button8.Name = "Button8"
        Me.Button8.ToolTipText = "Delay"
        '
        'Button9
        '
        Me.Button9.ImageIndex = 12
        Me.Button9.Name = "Button9"
        Me.Button9.ToolTipText = "Rise Time"
        '
        'Button10
        '
        Me.Button10.ImageIndex = 3
        Me.Button10.Name = "Button10"
        Me.Button10.ToolTipText = "Fall Time"
        '
        'Button11
        '
        Me.Button11.ImageIndex = 11
        Me.Button11.Name = "Button11"
        Me.Button11.ToolTipText = "Preshoot"
        '
        'Button12
        '
        Me.Button12.ImageIndex = 8
        Me.Button12.Name = "Button12"
        Me.Button12.ToolTipText = "Overshoot"
        '
        'Button13
        '
        Me.Button13.ImageIndex = 16
        Me.Button13.Name = "Button13"
        Me.Button13.ToolTipText = "Voltage Peak-to-Peak"
        '
        'Button14
        '
        Me.Button14.ImageIndex = 4
        Me.Button14.Name = "Button14"
        Me.Button14.ToolTipText = "Minmum Voltage"
        '
        'Button15
        '
        Me.Button15.ImageIndex = 5
        Me.Button15.Name = "Button15"
        Me.Button15.ToolTipText = "Maximum Voltage"
        '
        'Button16
        '
        Me.Button16.ImageIndex = 19
        Me.Button16.Name = "Button16"
        Me.Button16.ToolTipText = "Average Voltage"
        '
        'Button17
        '
        Me.Button17.ImageIndex = 14
        Me.Button17.Name = "Button17"
        Me.Button17.ToolTipText = "Voltage Amplitude"
        '
        'Button18
        '
        Me.Button18.ImageIndex = 15
        Me.Button18.Name = "Button18"
        Me.Button18.ToolTipText = "Voltage Base"
        '
        'Button19
        '
        Me.Button19.ImageIndex = 17
        Me.Button19.Name = "Button19"
        Me.Button19.ToolTipText = "Voltage Top"
        '
        'Button20
        '
        Me.Button20.ImageIndex = 7
        Me.Button20.Name = "Button20"
        Me.Button20.Pushed = True
        Me.Button20.ToolTipText = "Measurement Off"
        '
        'imgFunctions
        '
        Me.imgFunctions.ImageStream = CType(resources.GetObject("imgFunctions.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgFunctions.TransparentColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.imgFunctions.Images.SetKeyName(0, "DL")
        Me.imgFunctions.Images.SetKeyName(1, "DU")
        Me.imgFunctions.Images.SetKeyName(2, "F")
        Me.imgFunctions.Images.SetKeyName(3, "")
        Me.imgFunctions.Images.SetKeyName(4, "MN")
        Me.imgFunctions.Images.SetKeyName(5, "MX")
        Me.imgFunctions.Images.SetKeyName(6, "")
        Me.imgFunctions.Images.SetKeyName(7, "OFF")
        Me.imgFunctions.Images.SetKeyName(8, "OS")
        Me.imgFunctions.Images.SetKeyName(9, "P")
        Me.imgFunctions.Images.SetKeyName(10, "PW")
        Me.imgFunctions.Images.SetKeyName(11, "PS")
        Me.imgFunctions.Images.SetKeyName(12, "RT")
        Me.imgFunctions.Images.SetKeyName(13, "ACV")
        Me.imgFunctions.Images.SetKeyName(14, "VAM")
        Me.imgFunctions.Images.SetKeyName(15, "")
        Me.imgFunctions.Images.SetKeyName(16, "")
        Me.imgFunctions.Images.SetKeyName(17, "VT")
        Me.imgFunctions.Images.SetKeyName(18, "")
        Me.imgFunctions.Images.SetKeyName(19, "")
        Me.imgFunctions.Images.SetKeyName(20, "")
        '
        'cboMeasSour
        '
        Me.cboMeasSour.BackColor = System.Drawing.Color.White
        Me.cboMeasSour.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboMeasSour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMeasSour.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboMeasSour.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboMeasSour.Location = New System.Drawing.Point(139, 20)
        Me.cboMeasSour.Name = "cboMeasSour"
        Me.cboMeasSour.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboMeasSour.Size = New System.Drawing.Size(134, 22)
        Me.cboMeasSour.TabIndex = 209
        '
        'cmdHelp
        '
        Me.cmdHelp.BackColor = System.Drawing.SystemColors.Control
        Me.cmdHelp.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdHelp.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdHelp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdHelp.Location = New System.Drawing.Point(188, 214)
        Me.cmdHelp.Name = "cmdHelp"
        Me.cmdHelp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdHelp.Size = New System.Drawing.Size(100, 23)
        Me.cmdHelp.TabIndex = 172
        Me.cmdHelp.Text = "Help"
        Me.cmdHelp.UseVisualStyleBackColor = False
        '
        '_tabMainFunctions_TabPage4
        '
        Me._tabMainFunctions_TabPage4.Controls.Add(Me.fraLine)
        Me._tabMainFunctions_TabPage4.Controls.Add(Me.fraField)
        Me._tabMainFunctions_TabPage4.Controls.Add(Me.fraStandard)
        Me._tabMainFunctions_TabPage4.Controls.Add(Me.fraOccurrence)
        Me._tabMainFunctions_TabPage4.Controls.Add(Me.fraDelaySlope)
        Me._tabMainFunctions_TabPage4.Controls.Add(Me.fraTrigDelaySource)
        Me._tabMainFunctions_TabPage4.Controls.Add(Me.fraTrigDelay)
        Me._tabMainFunctions_TabPage4.Controls.Add(Me.fraTrigLevel)
        Me._tabMainFunctions_TabPage4.Controls.Add(Me.fraNoiseRej)
        Me._tabMainFunctions_TabPage4.Controls.Add(Me.fraTrigSlope)
        Me._tabMainFunctions_TabPage4.Controls.Add(Me.fraTrigSour)
        Me._tabMainFunctions_TabPage4.Controls.Add(Me.fraTrigMode)
        Me._tabMainFunctions_TabPage4.Location = New System.Drawing.Point(4, 40)
        Me._tabMainFunctions_TabPage4.Name = "_tabMainFunctions_TabPage4"
        Me._tabMainFunctions_TabPage4.Size = New System.Drawing.Size(472, 242)
        Me._tabMainFunctions_TabPage4.TabIndex = 4
        Me._tabMainFunctions_TabPage4.Text = "Trigger       "
        '
        'fraLine
        '
        Me.fraLine.Controls.Add(Me.SSPanel14)
        Me.fraLine.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraLine.ForeColor = System.Drawing.Color.Navy
        Me.fraLine.Location = New System.Drawing.Point(200, 182)
        Me.fraLine.Name = "fraLine"
        Me.fraLine.Size = New System.Drawing.Size(89, 53)
        Me.fraLine.TabIndex = 143
        Me.fraLine.TabStop = False
        Me.fraLine.Text = "Line"
        Me.fraLine.Visible = False
        '
        'SSPanel14
        '
        Me.SSPanel14.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SSPanel14.Controls.Add(Me.txtLine)
        Me.SSPanel14.Controls.Add(Me.spnLine)
        Me.SSPanel14.Location = New System.Drawing.Point(9, 24)
        Me.SSPanel14.Name = "SSPanel14"
        Me.SSPanel14.Size = New System.Drawing.Size(78, 19)
        Me.SSPanel14.TabIndex = 1
        '
        'txtLine
        '
        Me.txtLine.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtLine.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLine.Location = New System.Drawing.Point(2, 1)
        Me.txtLine.Name = "txtLine"
        Me.txtLine.Size = New System.Drawing.Size(56, 13)
        Me.txtLine.TabIndex = 1
        '
        'spnLine
        '
        Me.spnLine.Location = New System.Drawing.Point(60, -2)
        Me.spnLine.Name = "spnLine"
        Me.spnLine.Range = New NationalInstruments.UI.Range(1.0R, 2048.0R)
        Me.spnLine.Size = New System.Drawing.Size(16, 20)
        Me.spnLine.TabIndex = 0
        Me.spnLine.Value = 1.0R
        '
        'fraField
        '
        Me.fraField.Controls.Add(Me._optField_1)
        Me.fraField.Controls.Add(Me._optField_0)
        Me.fraField.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraField.ForeColor = System.Drawing.Color.Navy
        Me.fraField.Location = New System.Drawing.Point(200, 118)
        Me.fraField.Name = "fraField"
        Me.fraField.Size = New System.Drawing.Size(89, 65)
        Me.fraField.TabIndex = 142
        Me.fraField.TabStop = False
        Me.fraField.Text = "Field"
        Me.fraField.Visible = False
        '
        '_optField_1
        '
        Me._optField_1.AutoSize = True
        Me._optField_1.ForeColor = System.Drawing.Color.Black
        Me._optField_1.Location = New System.Drawing.Point(8, 36)
        Me._optField_1.Name = "_optField_1"
        Me._optField_1.Size = New System.Drawing.Size(31, 17)
        Me._optField_1.TabIndex = 1
        Me._optField_1.Text = "2"
        Me._optField_1.UseVisualStyleBackColor = True
        '
        '_optField_0
        '
        Me._optField_0.AutoSize = True
        Me._optField_0.Checked = True
        Me._optField_0.ForeColor = System.Drawing.Color.Black
        Me._optField_0.Location = New System.Drawing.Point(8, 20)
        Me._optField_0.Name = "_optField_0"
        Me._optField_0.Size = New System.Drawing.Size(31, 17)
        Me._optField_0.TabIndex = 0
        Me._optField_0.TabStop = True
        Me._optField_0.Text = "1"
        Me._optField_0.UseVisualStyleBackColor = True
        '
        'fraStandard
        '
        Me.fraStandard.Controls.Add(Me._optStandard_1)
        Me.fraStandard.Controls.Add(Me._optStandard_0)
        Me.fraStandard.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraStandard.ForeColor = System.Drawing.Color.Navy
        Me.fraStandard.Location = New System.Drawing.Point(200, 54)
        Me.fraStandard.Name = "fraStandard"
        Me.fraStandard.Size = New System.Drawing.Size(89, 65)
        Me.fraStandard.TabIndex = 141
        Me.fraStandard.TabStop = False
        Me.fraStandard.Text = "Standard"
        Me.fraStandard.Visible = False
        '
        '_optStandard_1
        '
        Me._optStandard_1.AutoSize = True
        Me._optStandard_1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optStandard_1.ForeColor = System.Drawing.Color.Black
        Me._optStandard_1.Location = New System.Drawing.Point(8, 36)
        Me._optStandard_1.Name = "_optStandard_1"
        Me._optStandard_1.Size = New System.Drawing.Size(59, 17)
        Me._optStandard_1.TabIndex = 1
        Me._optStandard_1.Text = "Europe"
        Me._optStandard_1.UseVisualStyleBackColor = True
        '
        '_optStandard_0
        '
        Me._optStandard_0.AutoSize = True
        Me._optStandard_0.Checked = True
        Me._optStandard_0.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optStandard_0.ForeColor = System.Drawing.Color.Black
        Me._optStandard_0.Location = New System.Drawing.Point(8, 20)
        Me._optStandard_0.Name = "_optStandard_0"
        Me._optStandard_0.Size = New System.Drawing.Size(40, 17)
        Me._optStandard_0.TabIndex = 0
        Me._optStandard_0.TabStop = True
        Me._optStandard_0.Text = "US"
        Me._optStandard_0.UseVisualStyleBackColor = True
        '
        'fraOccurrence
        '
        Me.fraOccurrence.Controls.Add(Me.SSPanel15)
        Me.fraOccurrence.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraOccurrence.ForeColor = System.Drawing.Color.Navy
        Me.fraOccurrence.Location = New System.Drawing.Point(200, 14)
        Me.fraOccurrence.Name = "fraOccurrence"
        Me.fraOccurrence.Size = New System.Drawing.Size(89, 41)
        Me.fraOccurrence.TabIndex = 140
        Me.fraOccurrence.TabStop = False
        Me.fraOccurrence.Text = "Occurrence"
        Me.fraOccurrence.Visible = False
        '
        'SSPanel15
        '
        Me.SSPanel15.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SSPanel15.Controls.Add(Me.txtOccurrence)
        Me.SSPanel15.Controls.Add(Me.spnOccurrence)
        Me.SSPanel15.Location = New System.Drawing.Point(9, 17)
        Me.SSPanel15.Name = "SSPanel15"
        Me.SSPanel15.Size = New System.Drawing.Size(78, 19)
        Me.SSPanel15.TabIndex = 1
        '
        'txtOccurrence
        '
        Me.txtOccurrence.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtOccurrence.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOccurrence.Location = New System.Drawing.Point(2, 1)
        Me.txtOccurrence.Name = "txtOccurrence"
        Me.txtOccurrence.Size = New System.Drawing.Size(56, 13)
        Me.txtOccurrence.TabIndex = 1
        '
        'spnOccurrence
        '
        Me.spnOccurrence.Location = New System.Drawing.Point(60, -2)
        Me.spnOccurrence.Name = "spnOccurrence"
        Me.spnOccurrence.Range = New NationalInstruments.UI.Range(1.0R, 2048.0R)
        Me.spnOccurrence.Size = New System.Drawing.Size(16, 20)
        Me.spnOccurrence.TabIndex = 0
        Me.spnOccurrence.Value = 1.0R
        '
        'fraDelaySlope
        '
        Me.fraDelaySlope.Controls.Add(Me._optDelaySlope_1)
        Me.fraDelaySlope.Controls.Add(Me._optDelaySlope_0)
        Me.fraDelaySlope.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraDelaySlope.ForeColor = System.Drawing.Color.Navy
        Me.fraDelaySlope.Location = New System.Drawing.Point(100, 182)
        Me.fraDelaySlope.Name = "fraDelaySlope"
        Me.fraDelaySlope.Size = New System.Drawing.Size(97, 53)
        Me.fraDelaySlope.TabIndex = 139
        Me.fraDelaySlope.TabStop = False
        Me.fraDelaySlope.Text = "Delay Slope"
        Me.fraDelaySlope.Visible = False
        '
        '_optDelaySlope_1
        '
        Me._optDelaySlope_1.AutoSize = True
        Me._optDelaySlope_1.ForeColor = System.Drawing.Color.Black
        Me._optDelaySlope_1.Location = New System.Drawing.Point(8, 32)
        Me._optDelaySlope_1.Name = "_optDelaySlope_1"
        Me._optDelaySlope_1.Size = New System.Drawing.Size(68, 17)
        Me._optDelaySlope_1.TabIndex = 1
        Me._optDelaySlope_1.Text = "Negative"
        Me._optDelaySlope_1.UseVisualStyleBackColor = True
        '
        '_optDelaySlope_0
        '
        Me._optDelaySlope_0.AutoSize = True
        Me._optDelaySlope_0.Checked = True
        Me._optDelaySlope_0.ForeColor = System.Drawing.Color.Black
        Me._optDelaySlope_0.Location = New System.Drawing.Point(8, 16)
        Me._optDelaySlope_0.Name = "_optDelaySlope_0"
        Me._optDelaySlope_0.Size = New System.Drawing.Size(62, 17)
        Me._optDelaySlope_0.TabIndex = 0
        Me._optDelaySlope_0.TabStop = True
        Me._optDelaySlope_0.Text = "Positive"
        Me._optDelaySlope_0.UseVisualStyleBackColor = True
        '
        'fraTrigDelaySource
        '
        Me.fraTrigDelaySource.Controls.Add(Me.cboTrigDelaySour)
        Me.fraTrigDelaySource.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraTrigDelaySource.ForeColor = System.Drawing.Color.Navy
        Me.fraTrigDelaySource.Location = New System.Drawing.Point(100, 138)
        Me.fraTrigDelaySource.Name = "fraTrigDelaySource"
        Me.fraTrigDelaySource.Size = New System.Drawing.Size(97, 45)
        Me.fraTrigDelaySource.TabIndex = 138
        Me.fraTrigDelaySource.TabStop = False
        Me.fraTrigDelaySource.Text = "Delay Source"
        Me.fraTrigDelaySource.Visible = False
        '
        'cboTrigDelaySour
        '
        Me.cboTrigDelaySour.BackColor = System.Drawing.Color.White
        Me.cboTrigDelaySour.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboTrigDelaySour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTrigDelaySour.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboTrigDelaySour.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboTrigDelaySour.Location = New System.Drawing.Point(8, 16)
        Me.cboTrigDelaySour.Name = "cboTrigDelaySour"
        Me.cboTrigDelaySour.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboTrigDelaySour.Size = New System.Drawing.Size(80, 21)
        Me.cboTrigDelaySour.TabIndex = 31
        '
        'fraTrigDelay
        '
        Me.fraTrigDelay.Controls.Add(Me.SSPanel13)
        Me.fraTrigDelay.Controls.Add(Me.lblTrigDelayEvents)
        Me.fraTrigDelay.Controls.Add(Me._optTrigDelay_1)
        Me.fraTrigDelay.Controls.Add(Me._optTrigDelay_0)
        Me.fraTrigDelay.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraTrigDelay.ForeColor = System.Drawing.Color.Navy
        Me.fraTrigDelay.Location = New System.Drawing.Point(100, 54)
        Me.fraTrigDelay.Name = "fraTrigDelay"
        Me.fraTrigDelay.Size = New System.Drawing.Size(97, 85)
        Me.fraTrigDelay.TabIndex = 137
        Me.fraTrigDelay.TabStop = False
        Me.fraTrigDelay.Text = "Delay"
        Me.fraTrigDelay.Visible = False
        '
        'SSPanel13
        '
        Me.SSPanel13.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SSPanel13.Controls.Add(Me.txtDelEvents)
        Me.SSPanel13.Controls.Add(Me.spnDelEvents)
        Me.SSPanel13.Location = New System.Drawing.Point(9, 60)
        Me.SSPanel13.Name = "SSPanel13"
        Me.SSPanel13.Size = New System.Drawing.Size(78, 19)
        Me.SSPanel13.TabIndex = 2
        '
        'txtDelEvents
        '
        Me.txtDelEvents.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtDelEvents.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDelEvents.Location = New System.Drawing.Point(2, 1)
        Me.txtDelEvents.Name = "txtDelEvents"
        Me.txtDelEvents.Size = New System.Drawing.Size(56, 13)
        Me.txtDelEvents.TabIndex = 1
        '
        'spnDelEvents
        '
        Me.spnDelEvents.Location = New System.Drawing.Point(60, -2)
        Me.spnDelEvents.Name = "spnDelEvents"
        Me.spnDelEvents.Range = New NationalInstruments.UI.Range(1.0R, 2048.0R)
        Me.spnDelEvents.Size = New System.Drawing.Size(16, 20)
        Me.spnDelEvents.TabIndex = 0
        Me.spnDelEvents.Value = 1.0R
        '
        'lblTrigDelayEvents
        '
        Me.lblTrigDelayEvents.BackColor = System.Drawing.SystemColors.Control
        Me.lblTrigDelayEvents.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTrigDelayEvents.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTrigDelayEvents.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTrigDelayEvents.Location = New System.Drawing.Point(8, 48)
        Me.lblTrigDelayEvents.Name = "lblTrigDelayEvents"
        Me.lblTrigDelayEvents.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTrigDelayEvents.Size = New System.Drawing.Size(38, 15)
        Me.lblTrigDelayEvents.TabIndex = 20
        Me.lblTrigDelayEvents.Text = "Time"
        '
        '_optTrigDelay_1
        '
        Me._optTrigDelay_1.AutoSize = True
        Me._optTrigDelay_1.ForeColor = System.Drawing.Color.Black
        Me._optTrigDelay_1.Location = New System.Drawing.Point(8, 32)
        Me._optTrigDelay_1.Name = "_optTrigDelay_1"
        Me._optTrigDelay_1.Size = New System.Drawing.Size(58, 17)
        Me._optTrigDelay_1.TabIndex = 1
        Me._optTrigDelay_1.Text = "Events"
        Me._optTrigDelay_1.UseVisualStyleBackColor = True
        '
        '_optTrigDelay_0
        '
        Me._optTrigDelay_0.AutoSize = True
        Me._optTrigDelay_0.Checked = True
        Me._optTrigDelay_0.ForeColor = System.Drawing.Color.Black
        Me._optTrigDelay_0.Location = New System.Drawing.Point(8, 16)
        Me._optTrigDelay_0.Name = "_optTrigDelay_0"
        Me._optTrigDelay_0.Size = New System.Drawing.Size(48, 17)
        Me._optTrigDelay_0.TabIndex = 0
        Me._optTrigDelay_0.TabStop = True
        Me._optTrigDelay_0.Text = "Time"
        Me._optTrigDelay_0.UseVisualStyleBackColor = True
        '
        'fraTrigLevel
        '
        Me.fraTrigLevel.Controls.Add(Me.SSPanel12)
        Me.fraTrigLevel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraTrigLevel.ForeColor = System.Drawing.Color.Navy
        Me.fraTrigLevel.Location = New System.Drawing.Point(100, 14)
        Me.fraTrigLevel.Name = "fraTrigLevel"
        Me.fraTrigLevel.Size = New System.Drawing.Size(89, 41)
        Me.fraTrigLevel.TabIndex = 136
        Me.fraTrigLevel.TabStop = False
        Me.fraTrigLevel.Text = "Level"
        '
        'SSPanel12
        '
        Me.SSPanel12.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SSPanel12.Controls.Add(Me.txtTrigLevel)
        Me.SSPanel12.Controls.Add(Me.spnTrigLevel)
        Me.SSPanel12.Location = New System.Drawing.Point(9, 17)
        Me.SSPanel12.Name = "SSPanel12"
        Me.SSPanel12.Size = New System.Drawing.Size(78, 19)
        Me.SSPanel12.TabIndex = 1
        '
        'txtTrigLevel
        '
        Me.txtTrigLevel.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtTrigLevel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTrigLevel.Location = New System.Drawing.Point(2, 1)
        Me.txtTrigLevel.Name = "txtTrigLevel"
        Me.txtTrigLevel.Size = New System.Drawing.Size(56, 13)
        Me.txtTrigLevel.TabIndex = 1
        '
        'spnTrigLevel
        '
        Me.spnTrigLevel.Location = New System.Drawing.Point(60, -2)
        Me.spnTrigLevel.Name = "spnTrigLevel"
        Me.spnTrigLevel.Range = New NationalInstruments.UI.Range(1.0R, 2048.0R)
        Me.spnTrigLevel.Size = New System.Drawing.Size(16, 20)
        Me.spnTrigLevel.TabIndex = 0
        Me.spnTrigLevel.Value = 1.0R
        '
        'fraNoiseRej
        '
        Me.fraNoiseRej.Controls.Add(Me._optNoiseRej_1)
        Me.fraNoiseRej.Controls.Add(Me._optNoiseRej_0)
        Me.fraNoiseRej.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraNoiseRej.ForeColor = System.Drawing.Color.Navy
        Me.fraNoiseRej.Location = New System.Drawing.Point(8, 182)
        Me.fraNoiseRej.Name = "fraNoiseRej"
        Me.fraNoiseRej.Size = New System.Drawing.Size(89, 53)
        Me.fraNoiseRej.TabIndex = 36
        Me.fraNoiseRej.TabStop = False
        Me.fraNoiseRej.Text = "Sensitivity"
        '
        '_optNoiseRej_1
        '
        Me._optNoiseRej_1.AutoSize = True
        Me._optNoiseRej_1.ForeColor = System.Drawing.Color.Black
        Me._optNoiseRej_1.Location = New System.Drawing.Point(8, 32)
        Me._optNoiseRej_1.Name = "_optNoiseRej_1"
        Me._optNoiseRej_1.Size = New System.Drawing.Size(45, 17)
        Me._optNoiseRej_1.TabIndex = 1
        Me._optNoiseRej_1.Text = "Low"
        Me._optNoiseRej_1.UseVisualStyleBackColor = True
        '
        '_optNoiseRej_0
        '
        Me._optNoiseRej_0.AutoSize = True
        Me._optNoiseRej_0.Checked = True
        Me._optNoiseRej_0.ForeColor = System.Drawing.Color.Black
        Me._optNoiseRej_0.Location = New System.Drawing.Point(8, 16)
        Me._optNoiseRej_0.Name = "_optNoiseRej_0"
        Me._optNoiseRej_0.Size = New System.Drawing.Size(58, 17)
        Me._optNoiseRej_0.TabIndex = 0
        Me._optNoiseRej_0.TabStop = True
        Me._optNoiseRej_0.Text = "Normal"
        Me._optNoiseRej_0.UseVisualStyleBackColor = True
        '
        'fraTrigSlope
        '
        Me.fraTrigSlope.Controls.Add(Me._optTrigSlope_1)
        Me.fraTrigSlope.Controls.Add(Me._optTrigSlope_0)
        Me.fraTrigSlope.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraTrigSlope.ForeColor = System.Drawing.Color.Navy
        Me.fraTrigSlope.Location = New System.Drawing.Point(8, 126)
        Me.fraTrigSlope.Name = "fraTrigSlope"
        Me.fraTrigSlope.Size = New System.Drawing.Size(89, 57)
        Me.fraTrigSlope.TabIndex = 35
        Me.fraTrigSlope.TabStop = False
        Me.fraTrigSlope.Text = "Slope"
        '
        '_optTrigSlope_1
        '
        Me._optTrigSlope_1.AutoSize = True
        Me._optTrigSlope_1.ForeColor = System.Drawing.Color.Black
        Me._optTrigSlope_1.Location = New System.Drawing.Point(8, 32)
        Me._optTrigSlope_1.Name = "_optTrigSlope_1"
        Me._optTrigSlope_1.Size = New System.Drawing.Size(68, 17)
        Me._optTrigSlope_1.TabIndex = 1
        Me._optTrigSlope_1.Text = "Negative"
        Me._optTrigSlope_1.UseVisualStyleBackColor = True
        '
        '_optTrigSlope_0
        '
        Me._optTrigSlope_0.AutoSize = True
        Me._optTrigSlope_0.Checked = True
        Me._optTrigSlope_0.ForeColor = System.Drawing.Color.Black
        Me._optTrigSlope_0.Location = New System.Drawing.Point(8, 15)
        Me._optTrigSlope_0.Name = "_optTrigSlope_0"
        Me._optTrigSlope_0.Size = New System.Drawing.Size(62, 17)
        Me._optTrigSlope_0.TabIndex = 0
        Me._optTrigSlope_0.TabStop = True
        Me._optTrigSlope_0.Text = "Positive"
        Me._optTrigSlope_0.UseVisualStyleBackColor = True
        '
        'fraTrigSour
        '
        Me.fraTrigSour.Controls.Add(Me.cboTrigSour)
        Me.fraTrigSour.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraTrigSour.ForeColor = System.Drawing.Color.Navy
        Me.fraTrigSour.Location = New System.Drawing.Point(8, 82)
        Me.fraTrigSour.Name = "fraTrigSour"
        Me.fraTrigSour.Size = New System.Drawing.Size(89, 45)
        Me.fraTrigSour.TabIndex = 34
        Me.fraTrigSour.TabStop = False
        Me.fraTrigSour.Text = "Source"
        '
        'cboTrigSour
        '
        Me.cboTrigSour.BackColor = System.Drawing.Color.White
        Me.cboTrigSour.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboTrigSour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTrigSour.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboTrigSour.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboTrigSour.Location = New System.Drawing.Point(7, 16)
        Me.cboTrigSour.Name = "cboTrigSour"
        Me.cboTrigSour.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboTrigSour.Size = New System.Drawing.Size(75, 21)
        Me.cboTrigSour.TabIndex = 10
        '
        'fraTrigMode
        '
        Me.fraTrigMode.Controls.Add(Me._optTrigMode_1)
        Me.fraTrigMode.Controls.Add(Me._optTrigMode_2)
        Me.fraTrigMode.Controls.Add(Me._optTrigMode_0)
        Me.fraTrigMode.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraTrigMode.ForeColor = System.Drawing.Color.Navy
        Me.fraTrigMode.Location = New System.Drawing.Point(8, 14)
        Me.fraTrigMode.Name = "fraTrigMode"
        Me.fraTrigMode.Size = New System.Drawing.Size(89, 69)
        Me.fraTrigMode.TabIndex = 33
        Me.fraTrigMode.TabStop = False
        Me.fraTrigMode.Text = "Mode"
        '
        '_optTrigMode_1
        '
        Me._optTrigMode_1.AutoSize = True
        Me._optTrigMode_1.ForeColor = System.Drawing.Color.Black
        Me._optTrigMode_1.Location = New System.Drawing.Point(6, 48)
        Me._optTrigMode_1.Name = "_optTrigMode_1"
        Me._optTrigMode_1.Size = New System.Drawing.Size(80, 17)
        Me._optTrigMode_1.TabIndex = 2
        Me._optTrigMode_1.Text = "Edge Delay"
        Me._optTrigMode_1.UseVisualStyleBackColor = True
        Me._optTrigMode_1.Visible = False
        '
        '_optTrigMode_2
        '
        Me._optTrigMode_2.AutoSize = True
        Me._optTrigMode_2.ForeColor = System.Drawing.Color.Black
        Me._optTrigMode_2.Location = New System.Drawing.Point(6, 32)
        Me._optTrigMode_2.Name = "_optTrigMode_2"
        Me._optTrigMode_2.Size = New System.Drawing.Size(39, 17)
        Me._optTrigMode_2.TabIndex = 1
        Me._optTrigMode_2.Text = "TV"
        Me._optTrigMode_2.UseVisualStyleBackColor = True
        '
        '_optTrigMode_0
        '
        Me._optTrigMode_0.AutoSize = True
        Me._optTrigMode_0.Checked = True
        Me._optTrigMode_0.ForeColor = System.Drawing.Color.Black
        Me._optTrigMode_0.Location = New System.Drawing.Point(6, 16)
        Me._optTrigMode_0.Name = "_optTrigMode_0"
        Me._optTrigMode_0.Size = New System.Drawing.Size(50, 17)
        Me._optTrigMode_0.TabIndex = 0
        Me._optTrigMode_0.TabStop = True
        Me._optTrigMode_0.Text = "Edge"
        Me._optTrigMode_0.UseVisualStyleBackColor = True
        '
        '_tabMainFunctions_TabPage5
        '
        Me._tabMainFunctions_TabPage5.Controls.Add(Me.picMemOptions)
        Me._tabMainFunctions_TabPage5.Controls.Add(Me.picMathOptions)
        Me._tabMainFunctions_TabPage5.Location = New System.Drawing.Point(4, 40)
        Me._tabMainFunctions_TabPage5.Name = "_tabMainFunctions_TabPage5"
        Me._tabMainFunctions_TabPage5.Size = New System.Drawing.Size(472, 242)
        Me._tabMainFunctions_TabPage5.TabIndex = 5
        Me._tabMainFunctions_TabPage5.Text = "Math/Memory      "
        '
        'picMemOptions
        '
        Me.picMemOptions.Controls.Add(Me.SSFrame2)
        Me.picMemOptions.Controls.Add(Me.fraMemVerticalRange)
        Me.picMemOptions.Controls.Add(Me._cmdSaveToDisk_3)
        Me.picMemOptions.Controls.Add(Me.cmdLoadFromDisk)
        Me.picMemOptions.Controls.Add(Me.Line4)
        Me.picMemOptions.Controls.Add(Me.Line5)
        Me.picMemOptions.Controls.Add(Me.Label3)
        Me.picMemOptions.Location = New System.Drawing.Point(7, 144)
        Me.picMemOptions.Name = "picMemOptions"
        Me.picMemOptions.Size = New System.Drawing.Size(287, 98)
        Me.picMemOptions.TabIndex = 82
        '
        'SSFrame2
        '
        Me.SSFrame2.Controls.Add(Me.SSPanel17)
        Me.SSFrame2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SSFrame2.ForeColor = System.Drawing.Color.Navy
        Me.SSFrame2.Location = New System.Drawing.Point(144, 15)
        Me.SSFrame2.Name = "SSFrame2"
        Me.SSFrame2.Size = New System.Drawing.Size(138, 44)
        Me.SSFrame2.TabIndex = 93
        Me.SSFrame2.TabStop = False
        Me.SSFrame2.Text = "Voltage Offset"
        '
        'SSPanel17
        '
        Me.SSPanel17.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SSPanel17.Controls.Add(Me.txtMemVoltOffset)
        Me.SSPanel17.Controls.Add(Me.spnMemVOffset)
        Me.SSPanel17.Location = New System.Drawing.Point(10, 19)
        Me.SSPanel17.Name = "SSPanel17"
        Me.SSPanel17.Size = New System.Drawing.Size(104, 19)
        Me.SSPanel17.TabIndex = 2
        '
        'txtMemVoltOffset
        '
        Me.txtMemVoltOffset.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtMemVoltOffset.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMemVoltOffset.Location = New System.Drawing.Point(2, 1)
        Me.txtMemVoltOffset.Name = "txtMemVoltOffset"
        Me.txtMemVoltOffset.Size = New System.Drawing.Size(82, 13)
        Me.txtMemVoltOffset.TabIndex = 1
        '
        'spnMemVOffset
        '
        Me.spnMemVOffset.Location = New System.Drawing.Point(86, -2)
        Me.spnMemVOffset.Name = "spnMemVOffset"
        Me.spnMemVOffset.Range = New NationalInstruments.UI.Range(1.0R, 2048.0R)
        Me.spnMemVOffset.Size = New System.Drawing.Size(16, 20)
        Me.spnMemVOffset.TabIndex = 0
        Me.spnMemVOffset.Value = 1.0R
        '
        'fraMemVerticalRange
        '
        Me.fraMemVerticalRange.Controls.Add(Me.SSPanel19)
        Me.fraMemVerticalRange.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraMemVerticalRange.ForeColor = System.Drawing.Color.Navy
        Me.fraMemVerticalRange.Location = New System.Drawing.Point(0, 15)
        Me.fraMemVerticalRange.Name = "fraMemVerticalRange"
        Me.fraMemVerticalRange.Size = New System.Drawing.Size(138, 44)
        Me.fraMemVerticalRange.TabIndex = 92
        Me.fraMemVerticalRange.TabStop = False
        Me.fraMemVerticalRange.Text = "Range (V/Div)"
        '
        'SSPanel19
        '
        Me.SSPanel19.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SSPanel19.Controls.Add(Me.txtMemoryRange)
        Me.SSPanel19.Controls.Add(Me.SpinButton2)
        Me.SSPanel19.Location = New System.Drawing.Point(10, 19)
        Me.SSPanel19.Name = "SSPanel19"
        Me.SSPanel19.Size = New System.Drawing.Size(104, 19)
        Me.SSPanel19.TabIndex = 2
        '
        'txtMemoryRange
        '
        Me.txtMemoryRange.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtMemoryRange.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMemoryRange.Location = New System.Drawing.Point(2, 1)
        Me.txtMemoryRange.Name = "txtMemoryRange"
        Me.txtMemoryRange.Size = New System.Drawing.Size(82, 13)
        Me.txtMemoryRange.TabIndex = 1
        '
        'SpinButton2
        '
        Me.SpinButton2.Location = New System.Drawing.Point(86, -2)
        Me.SpinButton2.Name = "SpinButton2"
        Me.SpinButton2.Range = New NationalInstruments.UI.Range(1.0R, 2048.0R)
        Me.SpinButton2.Size = New System.Drawing.Size(16, 20)
        Me.SpinButton2.TabIndex = 0
        Me.SpinButton2.Value = 1.0R
        '
        '_cmdSaveToDisk_3
        '
        Me._cmdSaveToDisk_3.BackColor = System.Drawing.SystemColors.Control
        Me._cmdSaveToDisk_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._cmdSaveToDisk_3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._cmdSaveToDisk_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSaveToDisk.SetIndex(Me._cmdSaveToDisk_3, CType(3, Short))
        Me._cmdSaveToDisk_3.Location = New System.Drawing.Point(156, 68)
        Me._cmdSaveToDisk_3.Name = "_cmdSaveToDisk_3"
        Me._cmdSaveToDisk_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cmdSaveToDisk_3.Size = New System.Drawing.Size(122, 23)
        Me._cmdSaveToDisk_3.TabIndex = 86
        Me._cmdSaveToDisk_3.Text = "Save to Disk ..."
        Me._cmdSaveToDisk_3.UseVisualStyleBackColor = False
        '
        'cmdLoadFromDisk
        '
        Me.cmdLoadFromDisk.BackColor = System.Drawing.SystemColors.Control
        Me.cmdLoadFromDisk.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdLoadFromDisk.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdLoadFromDisk.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdLoadFromDisk.Location = New System.Drawing.Point(8, 68)
        Me.cmdLoadFromDisk.Name = "cmdLoadFromDisk"
        Me.cmdLoadFromDisk.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdLoadFromDisk.Size = New System.Drawing.Size(122, 23)
        Me.cmdLoadFromDisk.TabIndex = 87
        Me.cmdLoadFromDisk.Text = "Load from Disk ..."
        Me.cmdLoadFromDisk.UseVisualStyleBackColor = False
        '
        'Line4
        '
        Me.Line4.BackColor = System.Drawing.SystemColors.WindowText
        Me.Line4.Location = New System.Drawing.Point(0, 6)
        Me.Line4.Name = "Line4"
        Me.Line4.Size = New System.Drawing.Size(109, 1)
        Me.Line4.TabIndex = 91
        '
        'Line5
        '
        Me.Line5.BackColor = System.Drawing.SystemColors.WindowText
        Me.Line5.Location = New System.Drawing.Point(168, 6)
        Me.Line5.Name = "Line5"
        Me.Line5.Size = New System.Drawing.Size(109, 1)
        Me.Line5.TabIndex = 90
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(116, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(45, 15)
        Me.Label3.TabIndex = 82
        Me.Label3.Text = "Memory"
        '
        'picMathOptions
        '
        Me.picMathOptions.Controls.Add(Me.SSFrame1)
        Me.picMathOptions.Controls.Add(Me.fraMemVRange)
        Me.picMathOptions.Controls.Add(Me._cmdSaveToDisk_2)
        Me.picMathOptions.Controls.Add(Me._cmdSaveToMem_2)
        Me.picMathOptions.Controls.Add(Me.fraMathSourceB)
        Me.picMathOptions.Controls.Add(Me.fraRange)
        Me.picMathOptions.Controls.Add(Me.fraOperation)
        Me.picMathOptions.Controls.Add(Me.Line3)
        Me.picMathOptions.Controls.Add(Me.Line2)
        Me.picMathOptions.Controls.Add(Me.Label2)
        Me.picMathOptions.Location = New System.Drawing.Point(6, 14)
        Me.picMathOptions.Name = "picMathOptions"
        Me.picMathOptions.Size = New System.Drawing.Size(286, 130)
        Me.picMathOptions.TabIndex = 81
        '
        'SSFrame1
        '
        Me.SSFrame1.Controls.Add(Me.SSPanel16)
        Me.SSFrame1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SSFrame1.ForeColor = System.Drawing.Color.Navy
        Me.SSFrame1.Location = New System.Drawing.Point(144, 56)
        Me.SSFrame1.Name = "SSFrame1"
        Me.SSFrame1.Size = New System.Drawing.Size(138, 44)
        Me.SSFrame1.TabIndex = 87
        Me.SSFrame1.TabStop = False
        Me.SSFrame1.Text = "Voltage Offset"
        '
        'SSPanel16
        '
        Me.SSPanel16.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SSPanel16.Controls.Add(Me.txtMathVOffset)
        Me.SSPanel16.Controls.Add(Me.spnMathVOffset)
        Me.SSPanel16.Location = New System.Drawing.Point(10, 19)
        Me.SSPanel16.Name = "SSPanel16"
        Me.SSPanel16.Size = New System.Drawing.Size(104, 19)
        Me.SSPanel16.TabIndex = 2
        '
        'txtMathVOffset
        '
        Me.txtMathVOffset.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtMathVOffset.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMathVOffset.Location = New System.Drawing.Point(2, 1)
        Me.txtMathVOffset.Name = "txtMathVOffset"
        Me.txtMathVOffset.Size = New System.Drawing.Size(82, 13)
        Me.txtMathVOffset.TabIndex = 1
        '
        'spnMathVOffset
        '
        Me.spnMathVOffset.Location = New System.Drawing.Point(86, -2)
        Me.spnMathVOffset.Name = "spnMathVOffset"
        Me.spnMathVOffset.Range = New NationalInstruments.UI.Range(1.0R, 2048.0R)
        Me.spnMathVOffset.Size = New System.Drawing.Size(16, 20)
        Me.spnMathVOffset.TabIndex = 0
        Me.spnMathVOffset.Value = 1.0R
        '
        'fraMemVRange
        '
        Me.fraMemVRange.Controls.Add(Me.SSPanel18)
        Me.fraMemVRange.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraMemVRange.ForeColor = System.Drawing.Color.Navy
        Me.fraMemVRange.Location = New System.Drawing.Point(4, 57)
        Me.fraMemVRange.Name = "fraMemVRange"
        Me.fraMemVRange.Size = New System.Drawing.Size(138, 44)
        Me.fraMemVRange.TabIndex = 86
        Me.fraMemVRange.TabStop = False
        Me.fraMemVRange.Text = "Range (V/Div)"
        '
        'SSPanel18
        '
        Me.SSPanel18.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SSPanel18.Controls.Add(Me.txtMathRange)
        Me.SSPanel18.Controls.Add(Me.SpinButton1)
        Me.SSPanel18.Location = New System.Drawing.Point(10, 19)
        Me.SSPanel18.Name = "SSPanel18"
        Me.SSPanel18.Size = New System.Drawing.Size(104, 19)
        Me.SSPanel18.TabIndex = 2
        '
        'txtMathRange
        '
        Me.txtMathRange.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtMathRange.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMathRange.Location = New System.Drawing.Point(2, 1)
        Me.txtMathRange.Name = "txtMathRange"
        Me.txtMathRange.Size = New System.Drawing.Size(82, 13)
        Me.txtMathRange.TabIndex = 1
        '
        'SpinButton1
        '
        Me.SpinButton1.Location = New System.Drawing.Point(86, -2)
        Me.SpinButton1.Name = "SpinButton1"
        Me.SpinButton1.Range = New NationalInstruments.UI.Range(1.0R, 2048.0R)
        Me.SpinButton1.Size = New System.Drawing.Size(16, 20)
        Me.SpinButton1.TabIndex = 0
        Me.SpinButton1.Value = 1.0R
        '
        '_cmdSaveToDisk_2
        '
        Me._cmdSaveToDisk_2.BackColor = System.Drawing.SystemColors.Control
        Me._cmdSaveToDisk_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._cmdSaveToDisk_2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._cmdSaveToDisk_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSaveToDisk.SetIndex(Me._cmdSaveToDisk_2, CType(2, Short))
        Me._cmdSaveToDisk_2.Location = New System.Drawing.Point(156, 104)
        Me._cmdSaveToDisk_2.Name = "_cmdSaveToDisk_2"
        Me._cmdSaveToDisk_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cmdSaveToDisk_2.Size = New System.Drawing.Size(122, 23)
        Me._cmdSaveToDisk_2.TabIndex = 73
        Me._cmdSaveToDisk_2.Text = "Save to Disk ..."
        Me._cmdSaveToDisk_2.UseVisualStyleBackColor = False
        '
        '_cmdSaveToMem_2
        '
        Me._cmdSaveToMem_2.BackColor = System.Drawing.SystemColors.Control
        Me._cmdSaveToMem_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._cmdSaveToMem_2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._cmdSaveToMem_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSaveToMem.SetIndex(Me._cmdSaveToMem_2, CType(2, Short))
        Me._cmdSaveToMem_2.Location = New System.Drawing.Point(8, 104)
        Me._cmdSaveToMem_2.Name = "_cmdSaveToMem_2"
        Me._cmdSaveToMem_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cmdSaveToMem_2.Size = New System.Drawing.Size(122, 23)
        Me._cmdSaveToMem_2.TabIndex = 74
        Me._cmdSaveToMem_2.Text = "Save to Memory"
        Me._cmdSaveToMem_2.UseVisualStyleBackColor = False
        '
        'fraMathSourceB
        '
        Me.fraMathSourceB.Controls.Add(Me.cboMathSourceB)
        Me.fraMathSourceB.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraMathSourceB.ForeColor = System.Drawing.Color.Navy
        Me.fraMathSourceB.Location = New System.Drawing.Point(190, 12)
        Me.fraMathSourceB.Name = "fraMathSourceB"
        Me.fraMathSourceB.Size = New System.Drawing.Size(90, 44)
        Me.fraMathSourceB.TabIndex = 85
        Me.fraMathSourceB.TabStop = False
        Me.fraMathSourceB.Text = "Source B"
        '
        'cboMathSourceB
        '
        Me.cboMathSourceB.BackColor = System.Drawing.Color.White
        Me.cboMathSourceB.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboMathSourceB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMathSourceB.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboMathSourceB.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboMathSourceB.Items.AddRange(New Object() {"Channel 1", "Channel 2", "Memory"})
        Me.cboMathSourceB.Location = New System.Drawing.Point(8, 16)
        Me.cboMathSourceB.Name = "cboMathSourceB"
        Me.cboMathSourceB.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboMathSourceB.Size = New System.Drawing.Size(75, 21)
        Me.cboMathSourceB.TabIndex = 66
        '
        'fraRange
        '
        Me.fraRange.Controls.Add(Me.cboMathSourceA)
        Me.fraRange.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraRange.ForeColor = System.Drawing.Color.Navy
        Me.fraRange.Location = New System.Drawing.Point(95, 12)
        Me.fraRange.Name = "fraRange"
        Me.fraRange.Size = New System.Drawing.Size(90, 44)
        Me.fraRange.TabIndex = 84
        Me.fraRange.TabStop = False
        Me.fraRange.Text = "Source A"
        '
        'cboMathSourceA
        '
        Me.cboMathSourceA.BackColor = System.Drawing.Color.White
        Me.cboMathSourceA.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboMathSourceA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMathSourceA.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboMathSourceA.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboMathSourceA.Items.AddRange(New Object() {"Channel 1", "Channel 2", "Memory"})
        Me.cboMathSourceA.Location = New System.Drawing.Point(8, 16)
        Me.cboMathSourceA.Name = "cboMathSourceA"
        Me.cboMathSourceA.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboMathSourceA.Size = New System.Drawing.Size(75, 21)
        Me.cboMathSourceA.TabIndex = 64
        '
        'fraOperation
        '
        Me.fraOperation.Controls.Add(Me.cboMathFunction)
        Me.fraOperation.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraOperation.ForeColor = System.Drawing.Color.Navy
        Me.fraOperation.Location = New System.Drawing.Point(0, 12)
        Me.fraOperation.Name = "fraOperation"
        Me.fraOperation.Size = New System.Drawing.Size(90, 44)
        Me.fraOperation.TabIndex = 83
        Me.fraOperation.TabStop = False
        Me.fraOperation.Text = "Function"
        '
        'cboMathFunction
        '
        Me.cboMathFunction.BackColor = System.Drawing.Color.White
        Me.cboMathFunction.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboMathFunction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMathFunction.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboMathFunction.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboMathFunction.Items.AddRange(New Object() {"Add", "Subtract", "Multiply", "Differentiate", "Integrate", "Invert"})
        Me.cboMathFunction.Location = New System.Drawing.Point(8, 16)
        Me.cboMathFunction.Name = "cboMathFunction"
        Me.cboMathFunction.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboMathFunction.Size = New System.Drawing.Size(75, 21)
        Me.cboMathFunction.TabIndex = 68
        '
        'Line3
        '
        Me.Line3.BackColor = System.Drawing.SystemColors.WindowText
        Me.Line3.Location = New System.Drawing.Point(167, 6)
        Me.Line3.Name = "Line3"
        Me.Line3.Size = New System.Drawing.Size(109, 1)
        Me.Line3.TabIndex = 82
        '
        'Line2
        '
        Me.Line2.BackColor = System.Drawing.SystemColors.WindowText
        Me.Line2.Location = New System.Drawing.Point(1, 6)
        Me.Line2.Name = "Line2"
        Me.Line2.Size = New System.Drawing.Size(109, 1)
        Me.Line2.TabIndex = 81
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(126, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(31, 15)
        Me.Label2.TabIndex = 80
        Me.Label2.Text = "Math"
        '
        '_tabMainFunctions_TabPage6
        '
        Me._tabMainFunctions_TabPage6.Controls.Add(Me.fraTraceColor)
        Me._tabMainFunctions_TabPage6.Controls.Add(Me.fraTraceThickness)
        Me._tabMainFunctions_TabPage6.Controls.Add(Me.fraOnOff)
        Me._tabMainFunctions_TabPage6.Controls.Add(Me.fraOutputTrig)
        Me._tabMainFunctions_TabPage6.Controls.Add(Me.fraExtTriggerCoup)
        Me._tabMainFunctions_TabPage6.Controls.Add(Me.Panel_Conifg)
        Me._tabMainFunctions_TabPage6.Controls.Add(Me.cmdAbout)
        Me._tabMainFunctions_TabPage6.Controls.Add(Me.cmdUpdateTIP)
        Me._tabMainFunctions_TabPage6.Controls.Add(Me.cmdPrint)
        Me._tabMainFunctions_TabPage6.Controls.Add(Me.cmdSelfTest)
        Me._tabMainFunctions_TabPage6.Location = New System.Drawing.Point(4, 40)
        Me._tabMainFunctions_TabPage6.Name = "_tabMainFunctions_TabPage6"
        Me._tabMainFunctions_TabPage6.Size = New System.Drawing.Size(472, 242)
        Me._tabMainFunctions_TabPage6.TabIndex = 6
        Me._tabMainFunctions_TabPage6.Text = "Options       "
        '
        'fraTraceColor
        '
        Me.fraTraceColor.Controls.Add(Me.picTraceColor)
        Me.fraTraceColor.Controls.Add(Me.cboTraceColor)
        Me.fraTraceColor.ForeColor = System.Drawing.Color.Navy
        Me.fraTraceColor.Location = New System.Drawing.Point(142, 78)
        Me.fraTraceColor.Name = "fraTraceColor"
        Me.fraTraceColor.Size = New System.Drawing.Size(139, 76)
        Me.fraTraceColor.TabIndex = 221
        Me.fraTraceColor.TabStop = False
        Me.fraTraceColor.Text = "Trace Color"
        '
        'picTraceColor
        '
        Me.picTraceColor.BackColor = System.Drawing.Color.Yellow
        Me.picTraceColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.picTraceColor.Cursor = System.Windows.Forms.Cursors.Default
        Me.picTraceColor.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.picTraceColor.ForeColor = System.Drawing.SystemColors.ControlText
        Me.picTraceColor.Location = New System.Drawing.Point(108, 32)
        Me.picTraceColor.Name = "picTraceColor"
        Me.picTraceColor.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.picTraceColor.Size = New System.Drawing.Size(20, 21)
        Me.picTraceColor.TabIndex = 53
        Me.picTraceColor.TabStop = False
        '
        'cboTraceColor
        '
        Me.cboTraceColor.BackColor = System.Drawing.Color.White
        Me.cboTraceColor.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboTraceColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTraceColor.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboTraceColor.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboTraceColor.Location = New System.Drawing.Point(8, 32)
        Me.cboTraceColor.Name = "cboTraceColor"
        Me.cboTraceColor.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboTraceColor.Size = New System.Drawing.Size(89, 21)
        Me.cboTraceColor.TabIndex = 54
        '
        'fraTraceThickness
        '
        Me.fraTraceThickness.Controls.Add(Me.cboTraceThickness)
        Me.fraTraceThickness.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraTraceThickness.ForeColor = System.Drawing.Color.Navy
        Me.fraTraceThickness.Location = New System.Drawing.Point(142, 14)
        Me.fraTraceThickness.Name = "fraTraceThickness"
        Me.fraTraceThickness.Size = New System.Drawing.Size(139, 65)
        Me.fraTraceThickness.TabIndex = 220
        Me.fraTraceThickness.TabStop = False
        Me.fraTraceThickness.Text = "Trace Thickness"
        '
        'cboTraceThickness
        '
        Me.cboTraceThickness.BackColor = System.Drawing.Color.White
        Me.cboTraceThickness.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboTraceThickness.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTraceThickness.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboTraceThickness.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboTraceThickness.Location = New System.Drawing.Point(8, 28)
        Me.cboTraceThickness.Name = "cboTraceThickness"
        Me.cboTraceThickness.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboTraceThickness.Size = New System.Drawing.Size(121, 21)
        Me.cboTraceThickness.TabIndex = 51
        '
        'fraOnOff
        '
        Me.fraOnOff.Controls.Add(Me.Panel2)
        Me.fraOnOff.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraOnOff.ForeColor = System.Drawing.Color.Navy
        Me.fraOnOff.Location = New System.Drawing.Point(8, 162)
        Me.fraOnOff.Name = "fraOnOff"
        Me.fraOnOff.Size = New System.Drawing.Size(116, 69)
        Me.fraOnOff.TabIndex = 219
        Me.fraOnOff.TabStop = False
        Me.fraOnOff.Text = "Probe Comp"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.ribCompOFF)
        Me.Panel2.Controls.Add(Me.ribCompON)
        Me.Panel2.Controls.Add(Me.panWaveDisplay)
        Me.Panel2.Location = New System.Drawing.Point(8, 20)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(103, 40)
        Me.Panel2.TabIndex = 2
        '
        'ribCompOFF
        '
        Me.ribCompOFF.Appearance = System.Windows.Forms.Appearance.Button
        Me.ribCompOFF.Checked = True
        Me.ribCompOFF.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ribCompOFF.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ribCompOFF.ForeColor = System.Drawing.Color.Black
        Me.ribCompOFF.Location = New System.Drawing.Point(71, 4)
        Me.ribCompOFF.Name = "ribCompOFF"
        Me.ribCompOFF.Size = New System.Drawing.Size(31, 36)
        Me.ribCompOFF.TabIndex = 3
        Me.ribCompOFF.Text = "Off"
        Me.ribCompOFF.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ribCompOFF.UseVisualStyleBackColor = True
        '
        'ribCompON
        '
        Me.ribCompON.Appearance = System.Windows.Forms.Appearance.Button
        Me.ribCompON.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ribCompON.ForeColor = System.Drawing.Color.Black
        Me.ribCompON.Location = New System.Drawing.Point(41, 4)
        Me.ribCompON.Name = "ribCompON"
        Me.ribCompON.Size = New System.Drawing.Size(31, 36)
        Me.ribCompON.TabIndex = 2
        Me.ribCompON.Text = "On"
        Me.ribCompON.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ribCompON.UseVisualStyleBackColor = True
        '
        'panWaveDisplay
        '
        Me.panWaveDisplay.BackColor = System.Drawing.Color.Black
        Me.panWaveDisplay.Controls.Add(Me.imgProbeComp)
        Me.panWaveDisplay.Location = New System.Drawing.Point(2, 6)
        Me.panWaveDisplay.Name = "panWaveDisplay"
        Me.panWaveDisplay.Size = New System.Drawing.Size(40, 32)
        Me.panWaveDisplay.TabIndex = 1
        '
        'imgProbeComp
        '
        Me.imgProbeComp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.imgProbeComp.Cursor = System.Windows.Forms.Cursors.Default
        Me.imgProbeComp.Image = CType(resources.GetObject("imgProbeComp.Image"), System.Drawing.Image)
        Me.imgProbeComp.Location = New System.Drawing.Point(2, 2)
        Me.imgProbeComp.Name = "imgProbeComp"
        Me.imgProbeComp.Size = New System.Drawing.Size(35, 28)
        Me.imgProbeComp.TabIndex = 0
        Me.imgProbeComp.TabStop = False
        Me.imgProbeComp.Visible = False
        '
        'fraOutputTrig
        '
        Me.fraOutputTrig.Controls.Add(Me.chkExternalOutput)
        Me.fraOutputTrig.Controls.Add(Me.chkECL0)
        Me.fraOutputTrig.Controls.Add(Me.chkECL1)
        Me.fraOutputTrig.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraOutputTrig.ForeColor = System.Drawing.Color.Navy
        Me.fraOutputTrig.Location = New System.Drawing.Point(8, 78)
        Me.fraOutputTrig.Name = "fraOutputTrig"
        Me.fraOutputTrig.Size = New System.Drawing.Size(133, 76)
        Me.fraOutputTrig.TabIndex = 218
        Me.fraOutputTrig.TabStop = False
        Me.fraOutputTrig.Text = "Output Trigger Enable"
        '
        'chkExternalOutput
        '
        Me.chkExternalOutput.AutoSize = True
        Me.chkExternalOutput.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkExternalOutput.ForeColor = System.Drawing.Color.Black
        Me.chkExternalOutput.Location = New System.Drawing.Point(8, 16)
        Me.chkExternalOutput.Name = "chkExternalOutput"
        Me.chkExternalOutput.Size = New System.Drawing.Size(99, 17)
        Me.chkExternalOutput.TabIndex = 2
        Me.chkExternalOutput.Text = "External Output"
        Me.chkExternalOutput.UseVisualStyleBackColor = True
        '
        'chkECL0
        '
        Me.chkECL0.AutoSize = True
        Me.chkECL0.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkECL0.ForeColor = System.Drawing.Color.Black
        Me.chkECL0.Location = New System.Drawing.Point(8, 32)
        Me.chkECL0.Name = "chkECL0"
        Me.chkECL0.Size = New System.Drawing.Size(55, 17)
        Me.chkECL0.TabIndex = 1
        Me.chkECL0.Text = "ECL 0"
        Me.chkECL0.UseVisualStyleBackColor = True
        '
        'chkECL1
        '
        Me.chkECL1.AutoSize = True
        Me.chkECL1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkECL1.ForeColor = System.Drawing.Color.Black
        Me.chkECL1.Location = New System.Drawing.Point(8, 48)
        Me.chkECL1.Name = "chkECL1"
        Me.chkECL1.Size = New System.Drawing.Size(55, 17)
        Me.chkECL1.TabIndex = 0
        Me.chkECL1.Text = "ECL 1"
        Me.chkECL1.UseVisualStyleBackColor = True
        '
        'fraExtTriggerCoup
        '
        Me.fraExtTriggerCoup.Controls.Add(Me._optExtTrigCoup_1)
        Me.fraExtTriggerCoup.Controls.Add(Me._optExtTrigCoup_0)
        Me.fraExtTriggerCoup.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraExtTriggerCoup.ForeColor = System.Drawing.Color.Navy
        Me.fraExtTriggerCoup.Location = New System.Drawing.Point(8, 14)
        Me.fraExtTriggerCoup.Name = "fraExtTriggerCoup"
        Me.fraExtTriggerCoup.Size = New System.Drawing.Size(133, 65)
        Me.fraExtTriggerCoup.TabIndex = 217
        Me.fraExtTriggerCoup.TabStop = False
        Me.fraExtTriggerCoup.Text = "Ext. Trigger Impedance"
        '
        '_optExtTrigCoup_1
        '
        Me._optExtTrigCoup_1.AutoSize = True
        Me._optExtTrigCoup_1.ForeColor = System.Drawing.Color.Black
        Me._optExtTrigCoup_1.Location = New System.Drawing.Point(8, 41)
        Me._optExtTrigCoup_1.Name = "_optExtTrigCoup_1"
        Me._optExtTrigCoup_1.Size = New System.Drawing.Size(62, 17)
        Me._optExtTrigCoup_1.TabIndex = 1
        Me._optExtTrigCoup_1.Text = "50 Ohm"
        Me._optExtTrigCoup_1.UseVisualStyleBackColor = True
        '
        '_optExtTrigCoup_0
        '
        Me._optExtTrigCoup_0.AutoSize = True
        Me._optExtTrigCoup_0.Checked = True
        Me._optExtTrigCoup_0.ForeColor = System.Drawing.Color.Black
        Me._optExtTrigCoup_0.Location = New System.Drawing.Point(8, 20)
        Me._optExtTrigCoup_0.Name = "_optExtTrigCoup_0"
        Me._optExtTrigCoup_0.Size = New System.Drawing.Size(65, 17)
        Me._optExtTrigCoup_0.TabIndex = 0
        Me._optExtTrigCoup_0.TabStop = True
        Me._optExtTrigCoup_0.Text = "1M Ohm"
        Me._optExtTrigCoup_0.UseVisualStyleBackColor = True
        '
        'Panel_Conifg
        '
        Me.Panel_Conifg.BackColor = System.Drawing.SystemColors.Control
        Me.Panel_Conifg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Panel_Conifg.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Panel_Conifg.Location = New System.Drawing.Point(284, 14)
        Me.Panel_Conifg.Name = "Panel_Conifg"
        Me.Panel_Conifg.Parent_Object = Nothing
        Me.Panel_Conifg.Refresh = CType(0, Short)
        Me.Panel_Conifg.Size = New System.Drawing.Size(187, 140)
        Me.Panel_Conifg.TabIndex = 216
        '
        'cmdAbout
        '
        Me.cmdAbout.BackColor = System.Drawing.SystemColors.Control
        Me.cmdAbout.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdAbout.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAbout.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAbout.Location = New System.Drawing.Point(132, 207)
        Me.cmdAbout.Name = "cmdAbout"
        Me.cmdAbout.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAbout.Size = New System.Drawing.Size(75, 23)
        Me.cmdAbout.TabIndex = 211
        Me.cmdAbout.Text = "About"
        Me.cmdAbout.UseVisualStyleBackColor = False
        '
        'cmdUpdateTIP
        '
        Me.cmdUpdateTIP.BackColor = System.Drawing.SystemColors.Control
        Me.cmdUpdateTIP.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdUpdateTIP.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdUpdateTIP.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdUpdateTIP.Location = New System.Drawing.Point(213, 174)
        Me.cmdUpdateTIP.Name = "cmdUpdateTIP"
        Me.cmdUpdateTIP.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdUpdateTIP.Size = New System.Drawing.Size(75, 23)
        Me.cmdUpdateTIP.TabIndex = 212
        Me.cmdUpdateTIP.Text = "&Update TIP"
        Me.cmdUpdateTIP.UseVisualStyleBackColor = False
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Location = New System.Drawing.Point(213, 207)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(75, 23)
        Me.cmdPrint.TabIndex = 213
        Me.cmdPrint.Text = "Print Display"
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'cmdSelfTest
        '
        Me.cmdSelfTest.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSelfTest.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSelfTest.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSelfTest.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSelfTest.Location = New System.Drawing.Point(132, 174)
        Me.cmdSelfTest.Name = "cmdSelfTest"
        Me.cmdSelfTest.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSelfTest.Size = New System.Drawing.Size(75, 23)
        Me.cmdSelfTest.TabIndex = 214
        Me.cmdSelfTest.Text = "Built-In Test"
        Me.cmdSelfTest.UseVisualStyleBackColor = False
        '
        '_tabMainFunctions_TabPage7
        '
        Me._tabMainFunctions_TabPage7.Controls.Add(Me.Atlas_SFP)
        Me._tabMainFunctions_TabPage7.Location = New System.Drawing.Point(4, 40)
        Me._tabMainFunctions_TabPage7.Name = "_tabMainFunctions_TabPage7"
        Me._tabMainFunctions_TabPage7.Size = New System.Drawing.Size(472, 242)
        Me._tabMainFunctions_TabPage7.TabIndex = 7
        Me._tabMainFunctions_TabPage7.Text = "ATLAS    "
        '
        'Atlas_SFP
        '
        Me.Atlas_SFP.ATLAS = Nothing
        Me.Atlas_SFP.BackColor = System.Drawing.SystemColors.Control
        Me.Atlas_SFP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Atlas_SFP.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Atlas_SFP.Location = New System.Drawing.Point(15, 19)
        Me.Atlas_SFP.Name = "Atlas_SFP"
        Me.Atlas_SFP.Parent_Object = Nothing
        Me.Atlas_SFP.Size = New System.Drawing.Size(398, 210)
        Me.Atlas_SFP.TabIndex = 0
        '
        'cmdAutoscale
        '
        Me.cmdAutoscale.BackColor = System.Drawing.SystemColors.Control
        Me.cmdAutoscale.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdAutoscale.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAutoscale.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAutoscale.Location = New System.Drawing.Point(483, 296)
        Me.cmdAutoscale.Name = "cmdAutoscale"
        Me.cmdAutoscale.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAutoscale.Size = New System.Drawing.Size(75, 23)
        Me.cmdAutoscale.TabIndex = 93
        Me.cmdAutoscale.Text = "&Autoscale"
        Me.cmdAutoscale.UseVisualStyleBackColor = False
        '
        'cmdMeasure
        '
        Me.cmdMeasure.BackColor = System.Drawing.SystemColors.Control
        Me.cmdMeasure.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdMeasure.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdMeasure.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdMeasure.Location = New System.Drawing.Point(399, 296)
        Me.cmdMeasure.Name = "cmdMeasure"
        Me.cmdMeasure.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdMeasure.Size = New System.Drawing.Size(75, 23)
        Me.cmdMeasure.TabIndex = 92
        Me.cmdMeasure.Text = "&Measure"
        Me.cmdMeasure.UseVisualStyleBackColor = False
        '
        'cmdQuit
        '
        Me.cmdQuit.BackColor = System.Drawing.SystemColors.Control
        Me.cmdQuit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdQuit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdQuit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdQuit.Location = New System.Drawing.Point(716, 296)
        Me.cmdQuit.Name = "cmdQuit"
        Me.cmdQuit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdQuit.Size = New System.Drawing.Size(75, 23)
        Me.cmdQuit.TabIndex = 49
        Me.cmdQuit.Text = "&Quit"
        Me.cmdQuit.UseVisualStyleBackColor = False
        '
        'cmdReset
        '
        Me.cmdReset.BackColor = System.Drawing.SystemColors.Control
        Me.cmdReset.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdReset.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdReset.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdReset.Location = New System.Drawing.Point(635, 296)
        Me.cmdReset.Name = "cmdReset"
        Me.cmdReset.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdReset.Size = New System.Drawing.Size(75, 23)
        Me.cmdReset.TabIndex = 48
        Me.cmdReset.Text = "&Reset"
        Me.cmdReset.UseVisualStyleBackColor = False
        '
        'txtDataDisplay
        '
        Me.txtDataDisplay.BackColor = System.Drawing.Color.Black
        Me.txtDataDisplay.Cursor = System.Windows.Forms.Cursors.Default
        Me.txtDataDisplay.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDataDisplay.ForeColor = System.Drawing.Color.Lime
        Me.txtDataDisplay.Location = New System.Drawing.Point(4, 4)
        Me.txtDataDisplay.Name = "txtDataDisplay"
        Me.txtDataDisplay.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDataDisplay.Size = New System.Drawing.Size(287, 32)
        Me.txtDataDisplay.TabIndex = 35
        Me.txtDataDisplay.Text = "Ready..."
        '
        'chkContinuous
        '
        Me.chkContinuous.AutoSize = True
        Me.chkContinuous.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkContinuous.Location = New System.Drawing.Point(312, 299)
        Me.chkContinuous.Name = "chkContinuous"
        Me.chkContinuous.Size = New System.Drawing.Size(79, 17)
        Me.chkContinuous.TabIndex = 94
        Me.chkContinuous.Text = "Continuous"
        '
        'CommonDialog1Open
        '
        Me.CommonDialog1Open.DefaultExt = "dat"
        Me.CommonDialog1Open.FileName = "*.dat"
        Me.CommonDialog1Open.Title = "SFP File I/O"
        '
        'cboVertRange
        '
        '
        'cmdSaveToDisk
        '
        '
        'cmdSaveToMem
        '
        '
        'panScopeDisplay
        '
        Me.panScopeDisplay.Controls.Add(Me.cwgScopeDisplay)
        Me.panScopeDisplay.Location = New System.Drawing.Point(8, 56)
        Me.panScopeDisplay.Name = "panScopeDisplay"
        Me.panScopeDisplay.Size = New System.Drawing.Size(297, 236)
        Me.panScopeDisplay.TabIndex = 213
        '
        'cwgScopeDisplay
        '
        Me.cwgScopeDisplay.Cursor = System.Windows.Forms.Cursors.Cross
        Me.cwgScopeDisplay.Cursors.AddRange(New NationalInstruments.UI.XYCursor() {Me.XyCursor1})
        Me.cwgScopeDisplay.Location = New System.Drawing.Point(0, 0)
        Me.cwgScopeDisplay.Name = "cwgScopeDisplay"
        Me.cwgScopeDisplay.Plots.AddRange(New NationalInstruments.UI.ScatterPlot() {Me.Plot1, Me.Plot2, Me.Plot3, Me.Plot4})
        Me.cwgScopeDisplay.Size = New System.Drawing.Size(297, 236)
        Me.cwgScopeDisplay.TabIndex = 0
        Me.cwgScopeDisplay.UseColorGenerator = True
        Me.cwgScopeDisplay.XAxes.AddRange(New NationalInstruments.UI.XAxis() {Me.XAxis1, Me.XAxisTicks})
        Me.cwgScopeDisplay.YAxes.AddRange(New NationalInstruments.UI.YAxis() {Me.YAxis1, Me.YAxis2, Me.YAxis3, Me.YAxis4, Me.YAxisTicks})
        '
        'XyCursor1
        '
        Me.XyCursor1.Color = System.Drawing.Color.White
        Me.XyCursor1.Plot = Me.Plot1
        Me.XyCursor1.PointSize = New System.Drawing.Size(5, 5)
        Me.XyCursor1.PointStyle = NationalInstruments.UI.PointStyle.EmptySquare
        Me.XyCursor1.SnapMode = NationalInstruments.UI.CursorSnapMode.Floating
        Me.XyCursor1.XPosition = 0.0R
        Me.XyCursor1.YPosition = 0.0R
        '
        'Plot1
        '
        Me.Plot1.AntiAliased = True
        Me.Plot1.LineColor = System.Drawing.Color.Yellow
        Me.Plot1.LineColorPrecedence = NationalInstruments.UI.ColorPrecedence.UserDefinedColor
        Me.Plot1.LineStyle = NationalInstruments.UI.LineStyle.None
        Me.Plot1.PointSize = New System.Drawing.Size(1, 1)
        Me.Plot1.PointStyle = NationalInstruments.UI.PointStyle.SolidCircle
        Me.Plot1.SmoothUpdates = True
        Me.Plot1.XAxis = Me.XAxis1
        Me.Plot1.YAxis = Me.YAxis1
        '
        'XAxis1
        '
        Me.XAxis1.AutoSpacing = False
        Me.XAxis1.InteractionMode = NationalInstruments.UI.ScaleInteractionMode.None
        Me.XAxis1.MajorDivisions.GridColor = System.Drawing.Color.Green
        Me.XAxis1.MajorDivisions.Interval = 10.0R
        Me.XAxis1.MajorDivisions.LabelVisible = False
        Me.XAxis1.MajorDivisions.TickColor = System.Drawing.Color.Green
        Me.XAxis1.MajorDivisions.TickVisible = False
        Me.XAxis1.MinorDivisions.GridColor = System.Drawing.Color.Green
        Me.XAxis1.MinorDivisions.Interval = 5.0R
        Me.XAxis1.MinorDivisions.TickColor = System.Drawing.Color.Green
        Me.XAxis1.Mode = NationalInstruments.UI.AxisMode.Fixed
        Me.XAxis1.Range = New NationalInstruments.UI.Range(0.0R, 500.0R)
        Me.XAxis1.Visible = False
        '
        'YAxis1
        '
        Me.YAxis1.Mode = NationalInstruments.UI.AxisMode.Fixed
        Me.YAxis1.Range = New NationalInstruments.UI.Range(-5.0R, 5.0R)
        Me.YAxis1.Visible = False
        '
        'Plot2
        '
        Me.Plot2.AntiAliased = True
        Me.Plot2.LineColor = System.Drawing.Color.Cyan
        Me.Plot2.LineColorPrecedence = NationalInstruments.UI.ColorPrecedence.UserDefinedColor
        Me.Plot2.LineStyle = NationalInstruments.UI.LineStyle.None
        Me.Plot2.PointColor = System.Drawing.Color.Cyan
        Me.Plot2.PointSize = New System.Drawing.Size(1, 1)
        Me.Plot2.PointStyle = NationalInstruments.UI.PointStyle.SolidCircle
        Me.Plot2.XAxis = Me.XAxis1
        Me.Plot2.YAxis = Me.YAxis2
        '
        'YAxis2
        '
        Me.YAxis2.Mode = NationalInstruments.UI.AxisMode.Fixed
        Me.YAxis2.Range = New NationalInstruments.UI.Range(-5.0R, 5.0R)
        Me.YAxis2.Visible = False
        '
        'Plot3
        '
        Me.Plot3.AntiAliased = True
        Me.Plot3.LineColor = System.Drawing.Color.Magenta
        Me.Plot3.LineColorPrecedence = NationalInstruments.UI.ColorPrecedence.UserDefinedColor
        Me.Plot3.LineStyle = NationalInstruments.UI.LineStyle.None
        Me.Plot3.PointColor = System.Drawing.Color.Magenta
        Me.Plot3.PointSize = New System.Drawing.Size(1, 1)
        Me.Plot3.PointStyle = NationalInstruments.UI.PointStyle.SolidCircle
        Me.Plot3.XAxis = Me.XAxis1
        Me.Plot3.YAxis = Me.YAxis3
        '
        'YAxis3
        '
        Me.YAxis3.Mode = NationalInstruments.UI.AxisMode.Fixed
        Me.YAxis3.Visible = False
        '
        'Plot4
        '
        Me.Plot4.AntiAliased = True
        Me.Plot4.LineColor = System.Drawing.Color.Lime
        Me.Plot4.LineColorPrecedence = NationalInstruments.UI.ColorPrecedence.UserDefinedColor
        Me.Plot4.LineStyle = NationalInstruments.UI.LineStyle.None
        Me.Plot4.PointColor = System.Drawing.Color.Lime
        Me.Plot4.PointSize = New System.Drawing.Size(1, 1)
        Me.Plot4.PointStyle = NationalInstruments.UI.PointStyle.SolidCircle
        Me.Plot4.XAxis = Me.XAxis1
        Me.Plot4.YAxis = Me.YAxis4
        '
        'YAxis4
        '
        Me.YAxis4.Mode = NationalInstruments.UI.AxisMode.Fixed
        Me.YAxis4.Visible = False
        '
        'XAxisTicks
        '
        Me.XAxisTicks.AutoSpacing = False
        Me.XAxisTicks.InteractionMode = NationalInstruments.UI.ScaleInteractionMode.None
        Me.XAxisTicks.MajorDivisions.GridColor = System.Drawing.Color.Green
        Me.XAxisTicks.MajorDivisions.GridVisible = True
        Me.XAxisTicks.MajorDivisions.Interval = 10.0R
        Me.XAxisTicks.MajorDivisions.TickColor = System.Drawing.Color.Green
        Me.XAxisTicks.MinorDivisions.GridColor = System.Drawing.Color.Green
        Me.XAxisTicks.MinorDivisions.GridVisible = True
        Me.XAxisTicks.MinorDivisions.Interval = 5.0R
        Me.XAxisTicks.MinorDivisions.TickColor = System.Drawing.Color.Green
        Me.XAxisTicks.MinorDivisions.TickVisible = True
        Me.XAxisTicks.Mode = NationalInstruments.UI.AxisMode.Fixed
        Me.XAxisTicks.Range = New NationalInstruments.UI.Range(0.0R, 500.0R)
        Me.XAxisTicks.Visible = False
        '
        'YAxisTicks
        '
        Me.YAxisTicks.MajorDivisions.GridColor = System.Drawing.Color.Green
        Me.YAxisTicks.MajorDivisions.GridVisible = True
        Me.YAxisTicks.MajorDivisions.Interval = 10.0R
        Me.YAxisTicks.MajorDivisions.TickColor = System.Drawing.Color.Green
        Me.YAxisTicks.MinorDivisions.GridColor = System.Drawing.Color.Green
        Me.YAxisTicks.MinorDivisions.GridVisible = True
        Me.YAxisTicks.MinorDivisions.Interval = 5.0R
        Me.YAxisTicks.MinorDivisions.TickVisible = True
        Me.YAxisTicks.Mode = NationalInstruments.UI.AxisMode.Fixed
        Me.YAxisTicks.Range = New NationalInstruments.UI.Range(-5.0R, 5.0R)
        Me.YAxisTicks.Visible = False
        '
        'panDmmDisplay
        '
        Me.panDmmDisplay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.panDmmDisplay.Controls.Add(Me.txtDataDisplay)
        Me.panDmmDisplay.Location = New System.Drawing.Point(8, 8)
        Me.panDmmDisplay.Name = "panDmmDisplay"
        Me.panDmmDisplay.Size = New System.Drawing.Size(297, 42)
        Me.panDmmDisplay.TabIndex = 214
        '
        'StatusBar1
        '
        Me.StatusBar1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DummyStrip1, Me.ToolStripStatusLabel1, Me.ToolStripStatusLabel2, Me.ToolStripStatusLabel3})
        Me.StatusBar1.Location = New System.Drawing.Point(0, 323)
        Me.StatusBar1.Name = "StatusBar1"
        Me.StatusBar1.Size = New System.Drawing.Size(801, 22)
        Me.StatusBar1.TabIndex = 215
        Me.StatusBar1.Text = "StatusStrip1"
        '
        'DummyStrip1
        '
        Me.DummyStrip1.Name = "DummyStrip1"
        Me.DummyStrip1.Size = New System.Drawing.Size(121, 17)
        Me.DummyStrip1.Text = "ToolStripStatusLabel4"
        Me.DummyStrip1.Visible = False
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripStatusLabel1.ForeColor = System.Drawing.Color.Black
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(114, 17)
        Me.ToolStripStatusLabel1.Text = "X Axis = .005 mS / Div"
        '
        'ToolStripStatusLabel2
        '
        Me.ToolStripStatusLabel2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripStatusLabel2.Name = "ToolStripStatusLabel2"
        Me.ToolStripStatusLabel2.Size = New System.Drawing.Size(168, 17)
        Me.ToolStripStatusLabel2.Text = "Cursor Position =  .024 mS, .022 V"
        '
        'ToolStripStatusLabel3
        '
        Me.ToolStripStatusLabel3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripStatusLabel3.Name = "ToolStripStatusLabel3"
        Me.ToolStripStatusLabel3.Size = New System.Drawing.Size(0, 17)
        '
        'PrintForm1
        '
        Me.PrintForm1.DocumentName = "document"
        Me.PrintForm1.Form = Me
        Me.PrintForm1.PrintAction = System.Drawing.Printing.PrintAction.PrintToPrinter
        Me.PrintForm1.PrinterSettings = CType(resources.GetObject("PrintForm1.PrinterSettings"), System.Drawing.Printing.PrinterSettings)
        Me.PrintForm1.PrintFileName = Nothing
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.RadioButton1)
        Me.Panel1.Controls.Add(Me.RadioButton2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Location = New System.Drawing.Point(74, 119)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(63, 30)
        Me.Panel1.TabIndex = 220
        '
        'RadioButton1
        '
        Me.RadioButton1.ForeColor = System.Drawing.Color.Black
        Me.RadioButton1.Location = New System.Drawing.Point(33, 12)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(28, 17)
        Me.RadioButton1.TabIndex = 215
        Me.RadioButton1.Text = "%"
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'RadioButton2
        '
        Me.RadioButton2.Checked = True
        Me.RadioButton2.ForeColor = System.Drawing.Color.Black
        Me.RadioButton2.Location = New System.Drawing.Point(0, 12)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(28, 17)
        Me.RadioButton2.TabIndex = 214
        Me.RadioButton2.TabStop = True
        Me.RadioButton2.Text = "V"
        Me.RadioButton2.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(46, 14)
        Me.Label1.TabIndex = 180
        Me.Label1.Text = "Level"
        '
        'frmZT1428
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(801, 345)
        Me.Controls.Add(Me.StatusBar1)
        Me.Controls.Add(Me.panDmmDisplay)
        Me.Controls.Add(Me.panScopeDisplay)
        Me.Controls.Add(Me.cmdDisplaySize)
        Me.Controls.Add(Me.cmdAutoscale)
        Me.Controls.Add(Me.cmdMeasure)
        Me.Controls.Add(Me.cmdQuit)
        Me.Controls.Add(Me.cmdReset)
        Me.Controls.Add(Me.chkContinuous)
        Me.Controls.Add(Me.tabMainFunctions)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.Black
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(54, 118)
        Me.MaximizeBox = False
        Me.Name = "frmZT1428"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Digitizing Oscilloscope"
        Me.tabMainFunctions.ResumeLayout(False)
        Me._tabMainFunctions_TabPage0.ResumeLayout(False)
        Me._tabMainFunctions_TabPage0.PerformLayout()
        Me.fraHoldOff.ResumeLayout(False)
        Me.fraHoldOff.PerformLayout()
        Me.SSPanel7.ResumeLayout(False)
        Me.SSPanel7.PerformLayout()
        CType(Me.spnHoldOff, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraDealy.ResumeLayout(False)
        Me.SSPanel6.ResumeLayout(False)
        Me.SSPanel6.PerformLayout()
        CType(Me.NumericEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraCompletion.ResumeLayout(False)
        Me.SSPanel9.ResumeLayout(False)
        Me.SSPanel9.PerformLayout()
        CType(Me.spnCompletion, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraAvgCount.ResumeLayout(False)
        Me.SSPanel8.ResumeLayout(False)
        Me.SSPanel8.PerformLayout()
        CType(Me.spnAvgCount, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraReference.ResumeLayout(False)
        Me.fraReference.PerformLayout()
        Me.fraSampleType.ResumeLayout(False)
        Me.fraAcqMode.ResumeLayout(False)
        Me.fraHorizRange.ResumeLayout(False)
        Me.fraDataPoints.ResumeLayout(False)
        Me.fraDataPoints.PerformLayout()
        Me._tabMainFunctions_TabPage1.ResumeLayout(False)
        Me._fraSignalFilters_0.ResumeLayout(False)
        Me._fraSignalFilters_0.PerformLayout()
        Me._fraCoupling_0.ResumeLayout(False)
        Me._fraCoupling_0.PerformLayout()
        Me._fraProbeAttenuation_0.ResumeLayout(False)
        Me._fraProbeAttenuation_0.PerformLayout()
        Me.fraOffset1.ResumeLayout(False)
        Me.SSPanel10.ResumeLayout(False)
        Me.SSPanel10.PerformLayout()
        CType(Me.spnVOffset1, System.ComponentModel.ISupportInitialize).EndInit()
        Me._fraVerticalRange_0.ResumeLayout(False)
        Me._tabMainFunctions_TabPage2.ResumeLayout(False)
        Me._fraCoupling_1.ResumeLayout(False)
        Me._fraCoupling_1.PerformLayout()
        Me._fraSignalFilters_1.ResumeLayout(False)
        Me._fraSignalFilters_1.PerformLayout()
        Me._fraProbeAttenuation_1.ResumeLayout(False)
        Me._fraProbeAttenuation_1.PerformLayout()
        Me.fraOffset2.ResumeLayout(False)
        Me.SSPanel11.ResumeLayout(False)
        Me.SSPanel11.PerformLayout()
        CType(Me.spnVOffset2, System.ComponentModel.ISupportInitialize).EndInit()
        Me._fraVerticalRange_1.ResumeLayout(False)
        Me._tabMainFunctions_TabPage3.ResumeLayout(False)
        Me._fraFunction_2.ResumeLayout(False)
        Me._fraFunction_2.PerformLayout()
        Me.fraDelayParam.ResumeLayout(False)
        Me.fraDelayParam.PerformLayout()
        Me.SSPanel5.ResumeLayout(False)
        Me.SSPanel5.PerformLayout()
        CType(Me.spnStopLevel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SSPanel3.ResumeLayout(False)
        Me.SSPanel3.PerformLayout()
        CType(Me.spnStartLevel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Picture4.ResumeLayout(False)
        Me.SSPanel4.ResumeLayout(False)
        Me.SSPanel4.PerformLayout()
        CType(Me.spnStopEdge, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SSPanel1.ResumeLayout(False)
        Me.SSPanel1.PerformLayout()
        CType(Me.spnStartEdge, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Picture3.ResumeLayout(False)
        Me.Picture2.ResumeLayout(False)
        Me._tabMainFunctions_TabPage4.ResumeLayout(False)
        Me.fraLine.ResumeLayout(False)
        Me.SSPanel14.ResumeLayout(False)
        Me.SSPanel14.PerformLayout()
        CType(Me.spnLine, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraField.ResumeLayout(False)
        Me.fraField.PerformLayout()
        Me.fraStandard.ResumeLayout(False)
        Me.fraStandard.PerformLayout()
        Me.fraOccurrence.ResumeLayout(False)
        Me.SSPanel15.ResumeLayout(False)
        Me.SSPanel15.PerformLayout()
        CType(Me.spnOccurrence, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraDelaySlope.ResumeLayout(False)
        Me.fraDelaySlope.PerformLayout()
        Me.fraTrigDelaySource.ResumeLayout(False)
        Me.fraTrigDelay.ResumeLayout(False)
        Me.fraTrigDelay.PerformLayout()
        Me.SSPanel13.ResumeLayout(False)
        Me.SSPanel13.PerformLayout()
        CType(Me.spnDelEvents, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraTrigLevel.ResumeLayout(False)
        Me.SSPanel12.ResumeLayout(False)
        Me.SSPanel12.PerformLayout()
        CType(Me.spnTrigLevel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraNoiseRej.ResumeLayout(False)
        Me.fraNoiseRej.PerformLayout()
        Me.fraTrigSlope.ResumeLayout(False)
        Me.fraTrigSlope.PerformLayout()
        Me.fraTrigSour.ResumeLayout(False)
        Me.fraTrigMode.ResumeLayout(False)
        Me.fraTrigMode.PerformLayout()
        Me._tabMainFunctions_TabPage5.ResumeLayout(False)
        Me.picMemOptions.ResumeLayout(False)
        Me.SSFrame2.ResumeLayout(False)
        Me.SSPanel17.ResumeLayout(False)
        Me.SSPanel17.PerformLayout()
        CType(Me.spnMemVOffset, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraMemVerticalRange.ResumeLayout(False)
        Me.SSPanel19.ResumeLayout(False)
        Me.SSPanel19.PerformLayout()
        CType(Me.SpinButton2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.picMathOptions.ResumeLayout(False)
        Me.SSFrame1.ResumeLayout(False)
        Me.SSPanel16.ResumeLayout(False)
        Me.SSPanel16.PerformLayout()
        CType(Me.spnMathVOffset, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraMemVRange.ResumeLayout(False)
        Me.SSPanel18.ResumeLayout(False)
        Me.SSPanel18.PerformLayout()
        CType(Me.SpinButton1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraMathSourceB.ResumeLayout(False)
        Me.fraRange.ResumeLayout(False)
        Me.fraOperation.ResumeLayout(False)
        Me._tabMainFunctions_TabPage6.ResumeLayout(False)
        Me.fraTraceColor.ResumeLayout(False)
        CType(Me.picTraceColor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraTraceThickness.ResumeLayout(False)
        Me.fraOnOff.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.panWaveDisplay.ResumeLayout(False)
        CType(Me.imgProbeComp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraOutputTrig.ResumeLayout(False)
        Me.fraOutputTrig.PerformLayout()
        Me.fraExtTriggerCoup.ResumeLayout(False)
        Me.fraExtTriggerCoup.PerformLayout()
        Me._tabMainFunctions_TabPage7.ResumeLayout(False)
        CType(Me.cboVertRange, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmdSaveToDisk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmdSaveToMem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panScopeDisplay.ResumeLayout(False)
        CType(Me.cwgScopeDisplay, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XyCursor1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panDmmDisplay.ResumeLayout(False)
        Me.StatusBar1.ResumeLayout(False)
        Me.StatusBar1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
 Friend WithEvents panScopeDisplay As System.Windows.Forms.Panel
 Friend WithEvents cwgScopeDisplay As NationalInstruments.UI.WindowsForms.ScatterGraph
 Friend WithEvents XAxis1 As NationalInstruments.UI.XAxis
 Friend WithEvents YAxis1 As NationalInstruments.UI.YAxis
 Friend WithEvents Plot1 As NationalInstruments.UI.ScatterPlot
 Friend WithEvents Plot2 As NationalInstruments.UI.ScatterPlot
 Friend WithEvents Plot3 As NationalInstruments.UI.ScatterPlot
 Friend WithEvents Plot4 As NationalInstruments.UI.ScatterPlot
 Friend WithEvents XyCursor1 As NationalInstruments.UI.XYCursor
 Friend WithEvents YAxis2 As NationalInstruments.UI.YAxis
 Friend WithEvents YAxis3 As NationalInstruments.UI.YAxis
 Friend WithEvents YAxis4 As NationalInstruments.UI.YAxis
 Friend WithEvents YAxisTicks As NationalInstruments.UI.YAxis
 Friend WithEvents XAxisTicks As NationalInstruments.UI.XAxis
 Friend WithEvents _chkDisplayTrace_0 As System.Windows.Forms.CheckBox
 Friend WithEvents _chkDisplayTrace_3 As System.Windows.Forms.CheckBox
 Friend WithEvents _chkDisplayTrace_2 As System.Windows.Forms.CheckBox
 Friend WithEvents _chkDisplayTrace_1 As System.Windows.Forms.CheckBox
 Friend WithEvents fraDisplay As System.Windows.Forms.GroupBox
 Friend WithEvents Atlas_SFP As VIPERT_Common_Controls.Atlas_SFP
 Friend WithEvents Panel_Conifg As VIPERT_Common_Controls.Panel_Conifg
 Friend WithEvents fraDataPoints As System.Windows.Forms.GroupBox
 Friend WithEvents fraHorizRange As System.Windows.Forms.GroupBox
 Friend WithEvents cboHorizRange As System.Windows.Forms.ComboBox
 Friend WithEvents _optDataPoints_1 As System.Windows.Forms.RadioButton
 Friend WithEvents _optDataPoints_0 As System.Windows.Forms.RadioButton
 Friend WithEvents fraAcqMode As System.Windows.Forms.GroupBox
 Friend WithEvents fraSampleType As System.Windows.Forms.GroupBox
 Friend WithEvents fraReference As System.Windows.Forms.GroupBox
 Friend WithEvents _optReference_2 As System.Windows.Forms.RadioButton
 Friend WithEvents _optReference_0 As System.Windows.Forms.RadioButton
 Friend WithEvents _optReference_1 As System.Windows.Forms.RadioButton
 Friend WithEvents fraAvgCount As System.Windows.Forms.GroupBox
 Friend WithEvents SSPanel8 As System.Windows.Forms.Panel
 Friend WithEvents txtAvgCount As System.Windows.Forms.TextBox
 Friend WithEvents spnAvgCount As NationalInstruments.UI.WindowsForms.NumericEdit
 Friend WithEvents fraCompletion As System.Windows.Forms.GroupBox
 Friend WithEvents SSPanel9 As System.Windows.Forms.Panel
 Friend WithEvents txtCompletion As System.Windows.Forms.TextBox
 Friend WithEvents spnCompletion As NationalInstruments.UI.WindowsForms.NumericEdit
 Friend WithEvents fraDealy As System.Windows.Forms.GroupBox
 Friend WithEvents SSPanel6 As System.Windows.Forms.Panel
 Friend WithEvents txtDelay As System.Windows.Forms.TextBox
 Friend WithEvents NumericEdit1 As NationalInstruments.UI.WindowsForms.NumericEdit
 Friend WithEvents fraHoldOff As System.Windows.Forms.GroupBox
 Friend WithEvents _optHoldOff_1 As System.Windows.Forms.RadioButton
 Friend WithEvents _optHoldOff_0 As System.Windows.Forms.RadioButton
 Public WithEvents lblEvents As System.Windows.Forms.Label
 Friend WithEvents SSPanel7 As System.Windows.Forms.Panel
 Friend WithEvents txtHoldOff As System.Windows.Forms.TextBox
 Friend WithEvents spnHoldOff As NationalInstruments.UI.WindowsForms.NumericEdit
 Friend WithEvents _fraVerticalRange_0 As System.Windows.Forms.GroupBox
 Friend WithEvents fraOffset1 As System.Windows.Forms.GroupBox
 Friend WithEvents SSPanel10 As System.Windows.Forms.Panel
 Friend WithEvents txtVOffset1 As System.Windows.Forms.TextBox
 Friend WithEvents spnVOffset1 As NationalInstruments.UI.WindowsForms.NumericEdit
 Friend WithEvents _fraVerticalRange_1 As System.Windows.Forms.GroupBox
 Public WithEvents _cboVertRange_1 As System.Windows.Forms.ComboBox
 Friend WithEvents fraOffset2 As System.Windows.Forms.GroupBox
 Friend WithEvents SSPanel11 As System.Windows.Forms.Panel
 Friend WithEvents txtVOffset2 As System.Windows.Forms.TextBox
 Friend WithEvents spnVOffset2 As NationalInstruments.UI.WindowsForms.NumericEdit
 Friend WithEvents _fraProbeAttenuation_0 As System.Windows.Forms.GroupBox
 Friend WithEvents _optProbe1_3 As System.Windows.Forms.RadioButton
 Friend WithEvents _optProbe1_2 As System.Windows.Forms.RadioButton
 Friend WithEvents _optProbe1_1 As System.Windows.Forms.RadioButton
 Friend WithEvents _optProbe1_0 As System.Windows.Forms.RadioButton
 Friend WithEvents _fraCoupling_0 As System.Windows.Forms.GroupBox
 Friend WithEvents _optCoupling1_2 As System.Windows.Forms.RadioButton
 Friend WithEvents _optCoupling1_1 As System.Windows.Forms.RadioButton
 Friend WithEvents _optCoupling1_0 As System.Windows.Forms.RadioButton
 Friend WithEvents _fraSignalFilters_0 As System.Windows.Forms.GroupBox
 Friend WithEvents chkHFReject1 As System.Windows.Forms.CheckBox
 Friend WithEvents chkLFReject1 As System.Windows.Forms.CheckBox
 Friend WithEvents _fraProbeAttenuation_1 As System.Windows.Forms.GroupBox
 Friend WithEvents _optProbe2_3 As System.Windows.Forms.RadioButton
 Friend WithEvents _optProbe2_2 As System.Windows.Forms.RadioButton
 Friend WithEvents _optProbe2_1 As System.Windows.Forms.RadioButton
 Friend WithEvents _optProbe2_0 As System.Windows.Forms.RadioButton
 Friend WithEvents _fraSignalFilters_1 As System.Windows.Forms.GroupBox
 Friend WithEvents chkHFReject2 As System.Windows.Forms.CheckBox
 Friend WithEvents chkLFReject2 As System.Windows.Forms.CheckBox
 Friend WithEvents _fraCoupling_1 As System.Windows.Forms.GroupBox
 Friend WithEvents _optCoupling2_2 As System.Windows.Forms.RadioButton
 Friend WithEvents _optCoupling2_1 As System.Windows.Forms.RadioButton
 Friend WithEvents _optCoupling2_0 As System.Windows.Forms.RadioButton
 Friend WithEvents panDmmDisplay As System.Windows.Forms.Panel
 Friend WithEvents imgFunctions As System.Windows.Forms.ImageList
 Friend WithEvents tbrMeasFunction As System.Windows.Forms.ToolBar
 Friend WithEvents DCRMS As System.Windows.Forms.ToolBarButton
 Friend WithEvents Button2 As System.Windows.Forms.ToolBarButton
 Friend WithEvents Button3 As System.Windows.Forms.ToolBarButton
 Friend WithEvents Button4 As System.Windows.Forms.ToolBarButton
 Friend WithEvents Button5 As System.Windows.Forms.ToolBarButton
 Friend WithEvents Button6 As System.Windows.Forms.ToolBarButton
 Friend WithEvents Button7 As System.Windows.Forms.ToolBarButton
 Friend WithEvents Button8 As System.Windows.Forms.ToolBarButton
 Friend WithEvents Button9 As System.Windows.Forms.ToolBarButton
 Friend WithEvents Button10 As System.Windows.Forms.ToolBarButton
 Friend WithEvents Button11 As System.Windows.Forms.ToolBarButton
 Friend WithEvents Button12 As System.Windows.Forms.ToolBarButton
 Friend WithEvents Button13 As System.Windows.Forms.ToolBarButton
 Friend WithEvents Button14 As System.Windows.Forms.ToolBarButton
 Friend WithEvents Button15 As System.Windows.Forms.ToolBarButton
 Friend WithEvents Button16 As System.Windows.Forms.ToolBarButton
 Friend WithEvents Button17 As System.Windows.Forms.ToolBarButton
 Friend WithEvents Button18 As System.Windows.Forms.ToolBarButton
 Friend WithEvents Button19 As System.Windows.Forms.ToolBarButton
 Friend WithEvents Button20 As System.Windows.Forms.ToolBarButton
 Friend WithEvents Dummy As System.Windows.Forms.ToolBarButton
 Friend WithEvents fraTrigMode As System.Windows.Forms.GroupBox
 Friend WithEvents _optTrigMode_1 As System.Windows.Forms.RadioButton
 Friend WithEvents _optTrigMode_2 As System.Windows.Forms.RadioButton
 Friend WithEvents _optTrigMode_0 As System.Windows.Forms.RadioButton
 Friend WithEvents fraTrigSour As System.Windows.Forms.GroupBox
 Friend WithEvents fraTrigSlope As System.Windows.Forms.GroupBox
 Friend WithEvents _optTrigSlope_1 As System.Windows.Forms.RadioButton
 Friend WithEvents _optTrigSlope_0 As System.Windows.Forms.RadioButton
 Friend WithEvents fraNoiseRej As System.Windows.Forms.GroupBox
 Friend WithEvents _optNoiseRej_1 As System.Windows.Forms.RadioButton
 Friend WithEvents _optNoiseRej_0 As System.Windows.Forms.RadioButton
 Friend WithEvents fraTrigLevel As System.Windows.Forms.GroupBox
 Friend WithEvents SSPanel12 As System.Windows.Forms.Panel
 Friend WithEvents txtTrigLevel As System.Windows.Forms.TextBox
 Friend WithEvents spnTrigLevel As NationalInstruments.UI.WindowsForms.NumericEdit
 Friend WithEvents fraTrigDelay As System.Windows.Forms.GroupBox
 Friend WithEvents _optTrigDelay_1 As System.Windows.Forms.RadioButton
 Friend WithEvents _optTrigDelay_0 As System.Windows.Forms.RadioButton
 Friend WithEvents SSPanel13 As System.Windows.Forms.Panel
 Friend WithEvents txtDelEvents As System.Windows.Forms.TextBox
 Friend WithEvents spnDelEvents As NationalInstruments.UI.WindowsForms.NumericEdit
 Public WithEvents lblTrigDelayEvents As System.Windows.Forms.Label
 Friend WithEvents fraTrigDelaySource As System.Windows.Forms.GroupBox
 Friend WithEvents fraDelaySlope As System.Windows.Forms.GroupBox
 Friend WithEvents _optDelaySlope_1 As System.Windows.Forms.RadioButton
 Friend WithEvents _optDelaySlope_0 As System.Windows.Forms.RadioButton
 Friend WithEvents fraOccurrence As System.Windows.Forms.GroupBox
 Friend WithEvents SSPanel15 As System.Windows.Forms.Panel
 Friend WithEvents txtOccurrence As System.Windows.Forms.TextBox
 Friend WithEvents spnOccurrence As NationalInstruments.UI.WindowsForms.NumericEdit
 Friend WithEvents fraStandard As System.Windows.Forms.GroupBox
 Friend WithEvents _optStandard_1 As System.Windows.Forms.RadioButton
 Friend WithEvents _optStandard_0 As System.Windows.Forms.RadioButton
 Friend WithEvents fraField As System.Windows.Forms.GroupBox
 Friend WithEvents _optField_1 As System.Windows.Forms.RadioButton
 Friend WithEvents _optField_0 As System.Windows.Forms.RadioButton
 Friend WithEvents fraLine As System.Windows.Forms.GroupBox
 Friend WithEvents SSPanel14 As System.Windows.Forms.Panel
 Friend WithEvents txtLine As System.Windows.Forms.TextBox
 Friend WithEvents spnLine As NationalInstruments.UI.WindowsForms.NumericEdit
 Friend WithEvents picMathOptions As System.Windows.Forms.Panel
 Public WithEvents Line3 As System.Windows.Forms.Label
 Public WithEvents Line2 As System.Windows.Forms.Label
 Public WithEvents Label2 As System.Windows.Forms.Label
 Friend WithEvents fraOperation As System.Windows.Forms.GroupBox
 Friend WithEvents fraMathSourceB As System.Windows.Forms.GroupBox
 Friend WithEvents fraRange As System.Windows.Forms.GroupBox
 Friend WithEvents fraMemVRange As System.Windows.Forms.GroupBox
 Friend WithEvents SSPanel18 As System.Windows.Forms.Panel
 Friend WithEvents txtMathRange As System.Windows.Forms.TextBox
 Friend WithEvents SpinButton1 As NationalInstruments.UI.WindowsForms.NumericEdit
 Friend WithEvents SSFrame1 As System.Windows.Forms.GroupBox
 Friend WithEvents SSPanel16 As System.Windows.Forms.Panel
 Friend WithEvents txtMathVOffset As System.Windows.Forms.TextBox
 Friend WithEvents spnMathVOffset As NationalInstruments.UI.WindowsForms.NumericEdit
 Friend WithEvents picMemOptions As System.Windows.Forms.Panel
 Public WithEvents Line4 As System.Windows.Forms.Label
 Public WithEvents Line5 As System.Windows.Forms.Label
 Public WithEvents Label3 As System.Windows.Forms.Label
 Friend WithEvents SSFrame2 As System.Windows.Forms.GroupBox
 Friend WithEvents SSPanel17 As System.Windows.Forms.Panel
 Friend WithEvents txtMemVoltOffset As System.Windows.Forms.TextBox
 Friend WithEvents spnMemVOffset As NationalInstruments.UI.WindowsForms.NumericEdit
 Friend WithEvents fraMemVerticalRange As System.Windows.Forms.GroupBox
 Friend WithEvents SSPanel19 As System.Windows.Forms.Panel
 Friend WithEvents txtMemoryRange As System.Windows.Forms.TextBox
 Friend WithEvents SpinButton2 As NationalInstruments.UI.WindowsForms.NumericEdit
 Friend WithEvents fraExtTriggerCoup As System.Windows.Forms.GroupBox
 Friend WithEvents _optExtTrigCoup_1 As System.Windows.Forms.RadioButton
 Friend WithEvents _optExtTrigCoup_0 As System.Windows.Forms.RadioButton
 Friend WithEvents fraOutputTrig As System.Windows.Forms.GroupBox
 Friend WithEvents chkExternalOutput As System.Windows.Forms.CheckBox
 Friend WithEvents chkECL0 As System.Windows.Forms.CheckBox
 Friend WithEvents chkECL1 As System.Windows.Forms.CheckBox
 Friend WithEvents fraOnOff As System.Windows.Forms.GroupBox
 Friend WithEvents panWaveDisplay As System.Windows.Forms.Panel
 Friend WithEvents Panel2 As System.Windows.Forms.Panel
 Friend WithEvents ribCompOFF As System.Windows.Forms.CheckBox
 Friend WithEvents ribCompON As System.Windows.Forms.CheckBox
 Friend WithEvents fraTraceThickness As System.Windows.Forms.GroupBox
 Friend WithEvents fraTraceColor As System.Windows.Forms.GroupBox
 Friend WithEvents StatusBar1 As System.Windows.Forms.StatusStrip
 Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
 Friend WithEvents ToolStripStatusLabel2 As System.Windows.Forms.ToolStripStatusLabel
 Friend WithEvents ToolStripStatusLabel3 As System.Windows.Forms.ToolStripStatusLabel
 Friend WithEvents DummyStrip1 As System.Windows.Forms.ToolStripStatusLabel
 Friend WithEvents _fraFunction_2 As System.Windows.Forms.GroupBox
 Friend WithEvents fraDelayParam As System.Windows.Forms.Panel
 Public WithEvents lblStop As System.Windows.Forms.Label
 Public WithEvents lblStar As System.Windows.Forms.Label
 Public WithEvents lblStopSource As System.Windows.Forms.Label
 Public WithEvents lblStartSource As System.Windows.Forms.Label
 Friend WithEvents Picture2 As System.Windows.Forms.Panel
 Public WithEvents Line1 As System.Windows.Forms.Label
 Friend WithEvents Picture3 As System.Windows.Forms.Panel
 Public WithEvents lblStopSlope As System.Windows.Forms.Label
 Friend WithEvents _optStartSlope_1 As System.Windows.Forms.RadioButton
 Friend WithEvents _optStartSlope_0 As System.Windows.Forms.RadioButton
 Public WithEvents lblStartSlope As System.Windows.Forms.Label
 Friend WithEvents _optStopSlope_1 As System.Windows.Forms.RadioButton
 Friend WithEvents _optStopSlope_0 As System.Windows.Forms.RadioButton
 Public WithEvents lblStopEdgeCap As System.Windows.Forms.Label
 Public WithEvents lblStartEdgeCap As System.Windows.Forms.Label
 Friend WithEvents SSPanel4 As System.Windows.Forms.Panel
 Friend WithEvents txtStopEdge As System.Windows.Forms.TextBox
 Friend WithEvents spnStopEdge As NationalInstruments.UI.WindowsForms.NumericEdit
 Friend WithEvents SSPanel1 As System.Windows.Forms.Panel
 Friend WithEvents txtStartEdge As System.Windows.Forms.TextBox
 Friend WithEvents spnStartEdge As NationalInstruments.UI.WindowsForms.NumericEdit
 Friend WithEvents Picture4 As System.Windows.Forms.Panel
 Friend WithEvents _optStartLevel_0 As System.Windows.Forms.RadioButton
 Public WithEvents lblStartLevelCap As System.Windows.Forms.Label
    Friend WithEvents _optStartLevel_1 As System.Windows.Forms.RadioButton
 Friend WithEvents SSPanel5 As System.Windows.Forms.Panel
 Friend WithEvents txtStopLevel As System.Windows.Forms.TextBox
 Friend WithEvents spnStopLevel As NationalInstruments.UI.WindowsForms.NumericEdit
 Friend WithEvents SSPanel3 As System.Windows.Forms.Panel
 Friend WithEvents txtStartLevel As System.Windows.Forms.TextBox
 Friend WithEvents spnStartLevel As NationalInstruments.UI.WindowsForms.NumericEdit
    Friend WithEvents PrintForm1 As Microsoft.VisualBasic.PowerPacks.Printing.PrintForm
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents RadioButton1 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton2 As System.Windows.Forms.RadioButton
    Public WithEvents Label1 As System.Windows.Forms.Label

#End Region
End Class