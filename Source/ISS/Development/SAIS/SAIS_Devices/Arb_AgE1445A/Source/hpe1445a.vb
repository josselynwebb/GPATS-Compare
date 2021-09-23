
Imports System
Imports System.Windows.Forms
Imports System.Windows.Forms.Screen
Imports System.Math
Imports Microsoft.VisualBasic
Imports NationalInstruments.Analysis.SignalGeneration
Imports Microsoft.VisualBasic.Compatibility.VB6

Public Class frmHpE1445a
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
    Friend WithEvents dlgFileIO As System.Windows.Forms.OpenFileDialog
    Friend WithEvents cmdHelp As System.Windows.Forms.Button
    Friend WithEvents cmdQuit As System.Windows.Forms.Button
    Friend WithEvents cmdReset As System.Windows.Forms.Button
    Friend WithEvents panMainControl As System.Windows.Forms.Panel
    Friend WithEvents fraOnOff As System.Windows.Forms.GroupBox
    Friend WithEvents imgWaveDisplay As System.Windows.Forms.PictureBox
    Friend WithEvents ribAbor As System.Windows.Forms.ToolBarButton
    Friend WithEvents ribInit As System.Windows.Forms.ToolBarButton
    Friend WithEvents tolOnOff As System.Windows.Forms.ToolBar
    Friend WithEvents chkOutpStat As System.Windows.Forms.CheckBox
    Friend WithEvents txtVoid As System.Windows.Forms.TextBox
    Friend WithEvents sbrUserInformation As System.Windows.Forms.StatusBar
    Friend WithEvents tabOptions As System.Windows.Forms.TabControl
    Friend WithEvents tabOptions_Page1 As System.Windows.Forms.TabPage
    Friend WithEvents SSFrame2 As System.Windows.Forms.GroupBox
    Friend WithEvents fraAmplUnits As System.Windows.Forms.GroupBox
    Friend WithEvents cboUnits As System.Windows.Forms.ComboBox
    Friend WithEvents fraAmplDiv7 As System.Windows.Forms.GroupBox
    Friend WithEvents panAmplDiv7 As System.Windows.Forms.Panel
    Friend WithEvents txtAmplDiv7 As System.Windows.Forms.TextBox
    Friend WithEvents spnAmplDiv7 As NationalInstruments.UI.WindowsForms.NumericEdit
    Friend WithEvents fraRoscSour As System.Windows.Forms.GroupBox
    Friend WithEvents cboRoscSour As System.Windows.Forms.ComboBox
    Friend WithEvents panRoscFreqExt As System.Windows.Forms.Panel
    Friend WithEvents txtRoscFreqExt As System.Windows.Forms.TextBox
    Friend WithEvents spnRoscFreqExt As NationalInstruments.UI.WindowsForms.NumericEdit
    Friend WithEvents lblRoscFreqExt As System.Windows.Forms.Label
    Friend WithEvents lblRoscSour As System.Windows.Forms.Label
    Friend WithEvents fraWavePoin As System.Windows.Forms.GroupBox
    Friend WithEvents panWavePoin As System.Windows.Forms.Panel
    Friend WithEvents txtWavePoin As System.Windows.Forms.TextBox
    Friend WithEvents spnWavePoin As NationalInstruments.UI.WindowsForms.NumericEdit
    Friend WithEvents fraWavePol As System.Windows.Forms.GroupBox
    Friend WithEvents cboWavePol As System.Windows.Forms.ComboBox
    Friend WithEvents fraFreq As System.Windows.Forms.GroupBox
    Friend WithEvents panFreq As System.Windows.Forms.Panel
    Friend WithEvents txtFreq As System.Windows.Forms.TextBox
    Friend WithEvents spnFreq As NationalInstruments.UI.WindowsForms.NumericEdit
    Friend WithEvents fraDcOffs As System.Windows.Forms.GroupBox
    Friend WithEvents panDcOffs As System.Windows.Forms.Panel
    Friend WithEvents txtDcOffs As System.Windows.Forms.TextBox
    Friend WithEvents spnDcOffs As NationalInstruments.UI.WindowsForms.NumericEdit
    Friend WithEvents fraAmpl As System.Windows.Forms.GroupBox
    Friend WithEvents panAmpl As System.Windows.Forms.Panel
    Friend WithEvents txtAmpl As System.Windows.Forms.TextBox
    Friend WithEvents spnAmpl As NationalInstruments.UI.WindowsForms.NumericEdit
    Friend WithEvents fraFuncWave As System.Windows.Forms.GroupBox
    Friend WithEvents tolFuncWave As System.Windows.Forms.ToolBar
    Friend WithEvents Sine As System.Windows.Forms.ToolBarButton
    Friend WithEvents Square As System.Windows.Forms.ToolBarButton
    Friend WithEvents Ramp As System.Windows.Forms.ToolBarButton
    Friend WithEvents Triangle As System.Windows.Forms.ToolBarButton
    Friend WithEvents DC As System.Windows.Forms.ToolBarButton
    Friend WithEvents User As System.Windows.Forms.ToolBarButton
    Friend WithEvents tabOptions_Page2 As System.Windows.Forms.TabPage
    Friend WithEvents fraTrigStop As System.Windows.Forms.GroupBox
    Friend WithEvents cboTrigStopSlop As System.Windows.Forms.ComboBox
    Friend WithEvents cboTrigStopSour As System.Windows.Forms.ComboBox
    Friend WithEvents cmdTrigStop As System.Windows.Forms.Button
    Friend WithEvents lblTrigStopSlop As System.Windows.Forms.Label
    Friend WithEvents lblTrigStopSour As System.Windows.Forms.Label
    Friend WithEvents fraTrigStart As System.Windows.Forms.GroupBox
    Friend WithEvents cboTrigSlop As System.Windows.Forms.ComboBox
    Friend WithEvents cboTrigGatePol As System.Windows.Forms.ComboBox
    Friend WithEvents cboTrigGateSour As System.Windows.Forms.ComboBox
    Friend WithEvents cboTrigSour As System.Windows.Forms.ComboBox
    Friend WithEvents chkTrigGateStat As System.Windows.Forms.CheckBox
    Friend WithEvents cmdTrig As System.Windows.Forms.Button
    Friend WithEvents lblTrigSlop As System.Windows.Forms.Label
    Friend WithEvents lblTrigGateStat As System.Windows.Forms.Label
    Friend WithEvents lblTrigGateSour As System.Windows.Forms.Label
    Friend WithEvents lblTrigGatePol As System.Windows.Forms.Label
    Friend WithEvents lblTrigSour As System.Windows.Forms.Label
    Friend WithEvents fraArmLay As System.Windows.Forms.GroupBox
    Friend WithEvents cboArmLay2Sour As System.Windows.Forms.ComboBox
    Friend WithEvents cboArmLay2Slop As System.Windows.Forms.ComboBox
    Friend WithEvents panArmCoun As System.Windows.Forms.Panel
    Friend WithEvents txtArmCoun As System.Windows.Forms.TextBox
    Friend WithEvents spnArmCount As NationalInstruments.UI.WindowsForms.NumericEdit
    Friend WithEvents panArmLay2Coun As System.Windows.Forms.Panel
    Friend WithEvents txtArmLay2Coun As System.Windows.Forms.TextBox
    Friend WithEvents spnArmLay2Coun As NationalInstruments.UI.WindowsForms.NumericEdit
    Friend WithEvents cmdArmCounInf_2 As System.Windows.Forms.Button
    Friend WithEvents cmdArmLay2 As System.Windows.Forms.Button
    Friend WithEvents cmdArmCounInf_1 As System.Windows.Forms.Button
    Friend WithEvents lblArmLay2Sour As System.Windows.Forms.Label
    Friend WithEvents lblArmLay2Coun As System.Windows.Forms.Label
    Friend WithEvents lblArmLay2Slop As System.Windows.Forms.Label
    Friend WithEvents lblArmCoun As System.Windows.Forms.Label
    Friend WithEvents tabOptions_Page3 As System.Windows.Forms.TabPage
    Friend WithEvents fraMarkEclt1 As System.Windows.Forms.GroupBox
    Friend WithEvents cboMarkEclt1Feed As System.Windows.Forms.ComboBox
    Friend WithEvents chkMarkEclt1 As System.Windows.Forms.CheckBox
    Friend WithEvents lblMarkEclt1Feed As System.Windows.Forms.Label
    Friend WithEvents fraMarkEclt0 As System.Windows.Forms.GroupBox
    Friend WithEvents cboMarkEclt0Feed As System.Windows.Forms.ComboBox
    Friend WithEvents chkMarkEclt0 As System.Windows.Forms.CheckBox
    Friend WithEvents lblMarkEclt0Feed As System.Windows.Forms.Label
    Friend WithEvents fraMarkExt As System.Windows.Forms.GroupBox
    Friend WithEvents cboMarkFeed As System.Windows.Forms.ComboBox
    Friend WithEvents cboMarkPol As System.Windows.Forms.ComboBox
    Friend WithEvents chkMark As System.Windows.Forms.CheckBox
    Friend WithEvents lblMarkFeed As System.Windows.Forms.Label
    Friend WithEvents lblMarkPol As System.Windows.Forms.Label
    Friend WithEvents fraMarkSpo As System.Windows.Forms.GroupBox
    Friend WithEvents panMarkSpo As System.Windows.Forms.Panel
    Friend WithEvents txtMarkSpo As System.Windows.Forms.TextBox
    Friend WithEvents spnMarkSpo As NationalInstruments.UI.WindowsForms.NumericEdit
    Friend WithEvents cmdMarkDelete As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblListSel As System.Windows.Forms.Label
    Friend WithEvents tabOptions_Page4 As System.Windows.Forms.TabPage
    Friend WithEvents fraAgileFsk As System.Windows.Forms.GroupBox
    Friend WithEvents cboAgileFskSour As System.Windows.Forms.ComboBox
    Friend WithEvents panAgileFskKeyH As System.Windows.Forms.Panel
    Friend WithEvents txtAgileFskKeyH As System.Windows.Forms.TextBox
    Friend WithEvents spnAgileFskKeyH As NationalInstruments.UI.WindowsForms.NumericEdit
    Friend WithEvents panAgileFskKeyL As System.Windows.Forms.Panel
    Friend WithEvents txtAgileFskKeyL As System.Windows.Forms.TextBox
    Friend WithEvents spnAgileFskKeyL As NationalInstruments.UI.WindowsForms.NumericEdit
    Friend WithEvents lblAgileFskKeyL As System.Windows.Forms.Label
    Friend WithEvents lblAgileFskSour As System.Windows.Forms.Label
    Friend WithEvents lblAgileFskKeyH As System.Windows.Forms.Label
    Friend WithEvents fraFreqModeSwe As System.Windows.Forms.GroupBox
    Friend WithEvents cboAgileSweepSpac As System.Windows.Forms.ComboBox
    Friend WithEvents cboAgileSweepDir As System.Windows.Forms.ComboBox
    Friend WithEvents cboAgileSweepTrigSour As System.Windows.Forms.ComboBox
    Friend WithEvents cboAgileSweepAdvSour As System.Windows.Forms.ComboBox
    Friend WithEvents panAgileSweepStart As System.Windows.Forms.Panel
    Friend WithEvents txtAgileSweepStart As System.Windows.Forms.TextBox
    Friend WithEvents spnAgileSweepStart As NationalInstruments.UI.WindowsForms.NumericEdit
    Friend WithEvents panAgileSweepStop As System.Windows.Forms.Panel
    Friend WithEvents txtAgileSweepStop As System.Windows.Forms.TextBox
    Friend WithEvents spnAgileSweepStop As NationalInstruments.UI.WindowsForms.NumericEdit
    Friend WithEvents panAgileSweepSteps As System.Windows.Forms.Panel
    Friend WithEvents txtAgileSweepSteps As System.Windows.Forms.TextBox
    Friend WithEvents spnAgileSweepSteps As NationalInstruments.UI.WindowsForms.NumericEdit
    Friend WithEvents panAgileSweepDur As System.Windows.Forms.Panel
    Friend WithEvents txtAgileSweepDur As System.Windows.Forms.TextBox
    Friend WithEvents spnAgileSweepDur As NationalInstruments.UI.WindowsForms.NumericEdit
    Friend WithEvents panSweCoun As System.Windows.Forms.Panel
    Friend WithEvents txtSweCoun As System.Windows.Forms.TextBox
    Friend WithEvents spnSweCoun As NationalInstruments.UI.WindowsForms.NumericEdit
    Friend WithEvents cmdSweCounInf As System.Windows.Forms.Button
    Friend WithEvents lblSweCoun As System.Windows.Forms.Label
    Friend WithEvents lblAgileSweepDur As System.Windows.Forms.Label
    Friend WithEvents lblAgileSweepSpac As System.Windows.Forms.Label
    Friend WithEvents lblAgileSweepDir As System.Windows.Forms.Label
    Friend WithEvents lblAgileSweepSteps As System.Windows.Forms.Label
    Friend WithEvents lblAgileSweepStop As System.Windows.Forms.Label
    Friend WithEvents lblAgileSweepTrigSour As System.Windows.Forms.Label
    Friend WithEvents lblAgileSweepStart As System.Windows.Forms.Label
    Friend WithEvents lblAgileSweepAdvSour As System.Windows.Forms.Label
    Friend WithEvents fraFreqMode As System.Windows.Forms.GroupBox
    Friend WithEvents optFreqModeFix As System.Windows.Forms.RadioButton
    Friend WithEvents optFreqModeFsk As System.Windows.Forms.RadioButton
    Friend WithEvents optFreqModeSwe As System.Windows.Forms.RadioButton
    Friend WithEvents tabOptions_Page5 As System.Windows.Forms.TabPage
    Friend WithEvents lblModFreq As System.Windows.Forms.Label
    Friend WithEvents lblModFreq1 As System.Windows.Forms.Label
    Friend WithEvents lblWrf1 As System.Windows.Forms.Label
    Friend WithEvents lblWrf As System.Windows.Forms.Label
    Friend WithEvents lblUserName As System.Windows.Forms.Label
    Friend WithEvents lblUserPoints As System.Windows.Forms.Label
    Friend WithEvents lblSampleFreq As System.Windows.Forms.Label
    Friend WithEvents prgDownLoad As System.Windows.Forms.ProgressBar
    Friend WithEvents G1 As NationalInstruments.UI.WindowsForms.WaveformGraph
    Friend WithEvents CursorMarker As NationalInstruments.UI.XYCursor
    Friend WithEvents Plot1 As NationalInstruments.UI.WaveformPlot
    Friend WithEvents XAxis As NationalInstruments.UI.XAxis
    Friend WithEvents YAxis1 As NationalInstruments.UI.YAxis
    Friend WithEvents Plot2 As NationalInstruments.UI.WaveformPlot
    Friend WithEvents Plot3 As NationalInstruments.UI.WaveformPlot
    Friend WithEvents Plot4 As NationalInstruments.UI.WaveformPlot
    Friend WithEvents YAxis2 As NationalInstruments.UI.YAxis
    Friend WithEvents YAxis3 As NationalInstruments.UI.YAxis
    Friend WithEvents YAxis4 As NationalInstruments.UI.YAxis
    Friend WithEvents cmdZoomOut As System.Windows.Forms.Button
    Friend WithEvents cmdPan As System.Windows.Forms.Button
    Friend WithEvents cmdSnap As System.Windows.Forms.Button
    Friend WithEvents cmdUserClear As System.Windows.Forms.Button
    Friend WithEvents cmdUserSine As System.Windows.Forms.Button
    Friend WithEvents cmdUserSquare As System.Windows.Forms.Button
    Friend WithEvents cmdUserRamp As System.Windows.Forms.Button
    Friend WithEvents cmdUserPm As System.Windows.Forms.Button
    Friend WithEvents cmdZoom As System.Windows.Forms.Button
    Friend WithEvents cmdUserUndo As System.Windows.Forms.Button
    Friend WithEvents cmdDownLoad As System.Windows.Forms.Button
    Friend WithEvents cmdUserLoad As System.Windows.Forms.Button
    Friend WithEvents cmdUserSave As System.Windows.Forms.Button
    Friend WithEvents panSampleFreq As System.Windows.Forms.Panel
    Friend WithEvents txtSampleFreq As System.Windows.Forms.TextBox
    Friend WithEvents spnSampleFreq As NationalInstruments.UI.WindowsForms.NumericEdit
    Friend WithEvents panUserPoints As System.Windows.Forms.Panel
    Friend WithEvents txtUserPoints As System.Windows.Forms.TextBox
    Friend WithEvents spnUserPoints As NationalInstruments.UI.WindowsForms.NumericEdit
    Friend WithEvents txtUserName As System.Windows.Forms.TextBox
    Friend WithEvents tabOptions_Page6 As System.Windows.Forms.TabPage
    Friend WithEvents fraListCat As System.Windows.Forms.GroupBox
    Friend WithEvents lstListCat As System.Windows.Forms.ListBox
    Friend WithEvents cmdSegDel As System.Windows.Forms.Button
    Friend WithEvents cmdSegAdd As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents fraFreeMem As System.Windows.Forms.GroupBox
    Friend WithEvents lblListFreeLbl As System.Windows.Forms.Label
    Friend WithEvents lblListSseqFreeLbl As System.Windows.Forms.Label
    Friend WithEvents lblListFree As System.Windows.Forms.Label
    Friend WithEvents lblListSseqFree As System.Windows.Forms.Label
    Friend WithEvents fraSeqName As System.Windows.Forms.GroupBox
    Friend WithEvents lstListSseqSeq As System.Windows.Forms.ListBox
    Friend WithEvents panDwelCoun As System.Windows.Forms.Panel
    Friend WithEvents txtDwelCoun As System.Windows.Forms.TextBox
    Friend WithEvents spnDwelCoun As NationalInstruments.UI.WindowsForms.NumericEdit
    Friend WithEvents cmdSeqListDown As System.Windows.Forms.Button
    Friend WithEvents cmdSeqListUp As System.Windows.Forms.Button
    Friend WithEvents cmdSeqListRemove As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents tabOptions_Page7 As System.Windows.Forms.TabPage
    Friend WithEvents fraOutLowPassFilt As System.Windows.Forms.GroupBox
    Friend WithEvents chkFilterStateOn As System.Windows.Forms.CheckBox
    Friend WithEvents optCutOff250 As System.Windows.Forms.RadioButton
    Friend WithEvents optCutOff10 As System.Windows.Forms.RadioButton
    Friend WithEvents fraOutLoadImpe As System.Windows.Forms.GroupBox
    Friend WithEvents optLoadImp75 As System.Windows.Forms.RadioButton
    Friend WithEvents optLoadImpHigh As System.Windows.Forms.RadioButton
    Friend WithEvents optLoadImp50 As System.Windows.Forms.RadioButton
    Friend WithEvents cmdAbout As System.Windows.Forms.Button
    Friend WithEvents cmdTest As System.Windows.Forms.Button
    Friend WithEvents cmdUpdateTip As System.Windows.Forms.Button
    Friend WithEvents fraPanelConfig As System.Windows.Forms.GroupBox
    Friend WithEvents cmdLocal As System.Windows.Forms.Button
    Friend WithEvents cmdLoadFromInstrument As System.Windows.Forms.Button
    Friend WithEvents cmdSaveToFile As System.Windows.Forms.Button
    Friend WithEvents cmdLoadFromFile As System.Windows.Forms.Button
    Friend WithEvents timDebug As System.Windows.Forms.Timer
    Friend WithEvents picLeds As System.Windows.Forms.PictureBox
    Friend WithEvents tabOptions_Page8 As System.Windows.Forms.TabPage
    Friend WithEvents Atlas_SFP As VIPERT_Common_Controls.Atlas_SFP
    Friend WithEvents CommonDialog1 As System.Windows.Forms.PrintDialog
    Friend WithEvents imlFuncWave As System.Windows.Forms.ImageList
    Friend WithEvents imlOutRelayWave As System.Windows.Forms.ImageList
    Friend WithEvents imlOnOff As System.Windows.Forms.ImageList
    Friend WithEvents CursorTo As NationalInstruments.UI.XYCursor
    Friend WithEvents CursorFrom As NationalInstruments.UI.XYCursor
    Friend WithEvents pctMode As NationalInstruments.UI.WindowsForms.Led
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmHpE1445a))
        Me.dlgFileIO = New System.Windows.Forms.OpenFileDialog()
        Me.cmdHelp = New System.Windows.Forms.Button()
        Me.cmdQuit = New System.Windows.Forms.Button()
        Me.cmdReset = New System.Windows.Forms.Button()
        Me.panMainControl = New System.Windows.Forms.Panel()
        Me.fraOnOff = New System.Windows.Forms.GroupBox()
        Me.imgWaveDisplay = New System.Windows.Forms.PictureBox()
        Me.tolOnOff = New System.Windows.Forms.ToolBar()
        Me.ribInit = New System.Windows.Forms.ToolBarButton()
        Me.ribAbor = New System.Windows.Forms.ToolBarButton()
        Me.imlOnOff = New System.Windows.Forms.ImageList(Me.components)
        Me.chkOutpStat = New System.Windows.Forms.CheckBox()
        Me.txtVoid = New System.Windows.Forms.TextBox()
        Me.sbrUserInformation = New System.Windows.Forms.StatusBar()
        Me.tabOptions = New System.Windows.Forms.TabControl()
        Me.tabOptions_Page1 = New System.Windows.Forms.TabPage()
        Me.fraFuncWave = New System.Windows.Forms.GroupBox()
        Me.tolFuncWave = New System.Windows.Forms.ToolBar()
        Me.Sine = New System.Windows.Forms.ToolBarButton()
        Me.Square = New System.Windows.Forms.ToolBarButton()
        Me.Ramp = New System.Windows.Forms.ToolBarButton()
        Me.Triangle = New System.Windows.Forms.ToolBarButton()
        Me.DC = New System.Windows.Forms.ToolBarButton()
        Me.User = New System.Windows.Forms.ToolBarButton()
        Me.imlFuncWave = New System.Windows.Forms.ImageList(Me.components)
        Me.fraFreq = New System.Windows.Forms.GroupBox()
        Me.panFreq = New System.Windows.Forms.Panel()
        Me.txtFreq = New System.Windows.Forms.TextBox()
        Me.spnFreq = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.fraWavePol = New System.Windows.Forms.GroupBox()
        Me.cboWavePol = New System.Windows.Forms.ComboBox()
        Me.fraWavePoin = New System.Windows.Forms.GroupBox()
        Me.panWavePoin = New System.Windows.Forms.Panel()
        Me.txtWavePoin = New System.Windows.Forms.TextBox()
        Me.spnWavePoin = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.fraRoscSour = New System.Windows.Forms.GroupBox()
        Me.lblRoscSour = New System.Windows.Forms.Label()
        Me.cboRoscSour = New System.Windows.Forms.ComboBox()
        Me.panRoscFreqExt = New System.Windows.Forms.Panel()
        Me.txtRoscFreqExt = New System.Windows.Forms.TextBox()
        Me.spnRoscFreqExt = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.lblRoscFreqExt = New System.Windows.Forms.Label()
        Me.SSFrame2 = New System.Windows.Forms.GroupBox()
        Me.fraAmpl = New System.Windows.Forms.GroupBox()
        Me.panAmpl = New System.Windows.Forms.Panel()
        Me.txtAmpl = New System.Windows.Forms.TextBox()
        Me.spnAmpl = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.fraAmplDiv7 = New System.Windows.Forms.GroupBox()
        Me.panAmplDiv7 = New System.Windows.Forms.Panel()
        Me.txtAmplDiv7 = New System.Windows.Forms.TextBox()
        Me.spnAmplDiv7 = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.fraDcOffs = New System.Windows.Forms.GroupBox()
        Me.panDcOffs = New System.Windows.Forms.Panel()
        Me.txtDcOffs = New System.Windows.Forms.TextBox()
        Me.spnDcOffs = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.fraAmplUnits = New System.Windows.Forms.GroupBox()
        Me.cboUnits = New System.Windows.Forms.ComboBox()
        Me.tabOptions_Page2 = New System.Windows.Forms.TabPage()
        Me.fraArmLay = New System.Windows.Forms.GroupBox()
        Me.cboArmLay2Sour = New System.Windows.Forms.ComboBox()
        Me.cboArmLay2Slop = New System.Windows.Forms.ComboBox()
        Me.panArmCoun = New System.Windows.Forms.Panel()
        Me.txtArmCoun = New System.Windows.Forms.TextBox()
        Me.spnArmCount = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.panArmLay2Coun = New System.Windows.Forms.Panel()
        Me.txtArmLay2Coun = New System.Windows.Forms.TextBox()
        Me.spnArmLay2Coun = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.cmdArmCounInf_2 = New System.Windows.Forms.Button()
        Me.cmdArmLay2 = New System.Windows.Forms.Button()
        Me.cmdArmCounInf_1 = New System.Windows.Forms.Button()
        Me.lblArmLay2Sour = New System.Windows.Forms.Label()
        Me.lblArmLay2Slop = New System.Windows.Forms.Label()
        Me.lblArmLay2Coun = New System.Windows.Forms.Label()
        Me.lblArmCoun = New System.Windows.Forms.Label()
        Me.fraTrigStart = New System.Windows.Forms.GroupBox()
        Me.lblTrigSlop = New System.Windows.Forms.Label()
        Me.cboTrigSlop = New System.Windows.Forms.ComboBox()
        Me.cboTrigGatePol = New System.Windows.Forms.ComboBox()
        Me.lblTrigSour = New System.Windows.Forms.Label()
        Me.cboTrigGateSour = New System.Windows.Forms.ComboBox()
        Me.cboTrigSour = New System.Windows.Forms.ComboBox()
        Me.chkTrigGateStat = New System.Windows.Forms.CheckBox()
        Me.cmdTrig = New System.Windows.Forms.Button()
        Me.lblTrigGateStat = New System.Windows.Forms.Label()
        Me.lblTrigGateSour = New System.Windows.Forms.Label()
        Me.lblTrigGatePol = New System.Windows.Forms.Label()
        Me.fraTrigStop = New System.Windows.Forms.GroupBox()
        Me.cboTrigStopSlop = New System.Windows.Forms.ComboBox()
        Me.cboTrigStopSour = New System.Windows.Forms.ComboBox()
        Me.cmdTrigStop = New System.Windows.Forms.Button()
        Me.lblTrigStopSour = New System.Windows.Forms.Label()
        Me.lblTrigStopSlop = New System.Windows.Forms.Label()
        Me.tabOptions_Page3 = New System.Windows.Forms.TabPage()
        Me.fraMarkEclt0 = New System.Windows.Forms.GroupBox()
        Me.cboMarkEclt0Feed = New System.Windows.Forms.ComboBox()
        Me.chkMarkEclt0 = New System.Windows.Forms.CheckBox()
        Me.lblMarkEclt0Feed = New System.Windows.Forms.Label()
        Me.fraMarkEclt1 = New System.Windows.Forms.GroupBox()
        Me.cboMarkEclt1Feed = New System.Windows.Forms.ComboBox()
        Me.chkMarkEclt1 = New System.Windows.Forms.CheckBox()
        Me.lblMarkEclt1Feed = New System.Windows.Forms.Label()
        Me.fraMarkExt = New System.Windows.Forms.GroupBox()
        Me.cboMarkFeed = New System.Windows.Forms.ComboBox()
        Me.cboMarkPol = New System.Windows.Forms.ComboBox()
        Me.chkMark = New System.Windows.Forms.CheckBox()
        Me.lblMarkFeed = New System.Windows.Forms.Label()
        Me.lblMarkPol = New System.Windows.Forms.Label()
        Me.fraMarkSpo = New System.Windows.Forms.GroupBox()
        Me.panMarkSpo = New System.Windows.Forms.Panel()
        Me.txtMarkSpo = New System.Windows.Forms.TextBox()
        Me.spnMarkSpo = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.cmdMarkDelete = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblListSel = New System.Windows.Forms.Label()
        Me.tabOptions_Page4 = New System.Windows.Forms.TabPage()
        Me.fraFreqMode = New System.Windows.Forms.GroupBox()
        Me.optFreqModeFix = New System.Windows.Forms.RadioButton()
        Me.optFreqModeSwe = New System.Windows.Forms.RadioButton()
        Me.optFreqModeFsk = New System.Windows.Forms.RadioButton()
        Me.fraFreqModeSwe = New System.Windows.Forms.GroupBox()
        Me.cboAgileSweepSpac = New System.Windows.Forms.ComboBox()
        Me.cboAgileSweepDir = New System.Windows.Forms.ComboBox()
        Me.cboAgileSweepTrigSour = New System.Windows.Forms.ComboBox()
        Me.cboAgileSweepAdvSour = New System.Windows.Forms.ComboBox()
        Me.panAgileSweepStart = New System.Windows.Forms.Panel()
        Me.txtAgileSweepStart = New System.Windows.Forms.TextBox()
        Me.spnAgileSweepStart = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.panAgileSweepStop = New System.Windows.Forms.Panel()
        Me.txtAgileSweepStop = New System.Windows.Forms.TextBox()
        Me.spnAgileSweepStop = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.panAgileSweepSteps = New System.Windows.Forms.Panel()
        Me.txtAgileSweepSteps = New System.Windows.Forms.TextBox()
        Me.spnSweCoun = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.panAgileSweepDur = New System.Windows.Forms.Panel()
        Me.txtAgileSweepDur = New System.Windows.Forms.TextBox()
        Me.spnAgileSweepDur = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.panSweCoun = New System.Windows.Forms.Panel()
        Me.txtSweCoun = New System.Windows.Forms.TextBox()
        Me.spnAgileSweepSteps = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.cmdSweCounInf = New System.Windows.Forms.Button()
        Me.lblSweCoun = New System.Windows.Forms.Label()
        Me.lblAgileSweepDur = New System.Windows.Forms.Label()
        Me.lblAgileSweepSpac = New System.Windows.Forms.Label()
        Me.lblAgileSweepDir = New System.Windows.Forms.Label()
        Me.lblAgileSweepSteps = New System.Windows.Forms.Label()
        Me.lblAgileSweepStop = New System.Windows.Forms.Label()
        Me.lblAgileSweepTrigSour = New System.Windows.Forms.Label()
        Me.lblAgileSweepStart = New System.Windows.Forms.Label()
        Me.lblAgileSweepAdvSour = New System.Windows.Forms.Label()
        Me.fraAgileFsk = New System.Windows.Forms.GroupBox()
        Me.cboAgileFskSour = New System.Windows.Forms.ComboBox()
        Me.lblAgileFskSour = New System.Windows.Forms.Label()
        Me.lblAgileFskKeyH = New System.Windows.Forms.Label()
        Me.panAgileFskKeyH = New System.Windows.Forms.Panel()
        Me.txtAgileFskKeyH = New System.Windows.Forms.TextBox()
        Me.spnAgileFskKeyH = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.lblAgileFskKeyL = New System.Windows.Forms.Label()
        Me.panAgileFskKeyL = New System.Windows.Forms.Panel()
        Me.txtAgileFskKeyL = New System.Windows.Forms.TextBox()
        Me.spnAgileFskKeyL = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.tabOptions_Page5 = New System.Windows.Forms.TabPage()
        Me.lblUserName = New System.Windows.Forms.Label()
        Me.lblUserPoints = New System.Windows.Forms.Label()
        Me.lblSampleFreq = New System.Windows.Forms.Label()
        Me.prgDownLoad = New System.Windows.Forms.ProgressBar()
        Me.G1 = New NationalInstruments.UI.WindowsForms.WaveformGraph()
        Me.CursorMarker = New NationalInstruments.UI.XYCursor()
        Me.Plot1 = New NationalInstruments.UI.WaveformPlot()
        Me.XAxis = New NationalInstruments.UI.XAxis()
        Me.YAxis1 = New NationalInstruments.UI.YAxis()
        Me.CursorTo = New NationalInstruments.UI.XYCursor()
        Me.CursorFrom = New NationalInstruments.UI.XYCursor()
        Me.Plot2 = New NationalInstruments.UI.WaveformPlot()
        Me.YAxis2 = New NationalInstruments.UI.YAxis()
        Me.Plot3 = New NationalInstruments.UI.WaveformPlot()
        Me.YAxis3 = New NationalInstruments.UI.YAxis()
        Me.Plot4 = New NationalInstruments.UI.WaveformPlot()
        Me.YAxis4 = New NationalInstruments.UI.YAxis()
        Me.cmdZoomOut = New System.Windows.Forms.Button()
        Me.cmdPan = New System.Windows.Forms.Button()
        Me.cmdSnap = New System.Windows.Forms.Button()
        Me.cmdUserClear = New System.Windows.Forms.Button()
        Me.cmdUserSine = New System.Windows.Forms.Button()
        Me.cmdUserSquare = New System.Windows.Forms.Button()
        Me.cmdUserRamp = New System.Windows.Forms.Button()
        Me.cmdUserPm = New System.Windows.Forms.Button()
        Me.cmdZoom = New System.Windows.Forms.Button()
        Me.cmdUserUndo = New System.Windows.Forms.Button()
        Me.cmdDownLoad = New System.Windows.Forms.Button()
        Me.cmdUserLoad = New System.Windows.Forms.Button()
        Me.cmdUserSave = New System.Windows.Forms.Button()
        Me.panSampleFreq = New System.Windows.Forms.Panel()
        Me.txtSampleFreq = New System.Windows.Forms.TextBox()
        Me.spnSampleFreq = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.panUserPoints = New System.Windows.Forms.Panel()
        Me.txtUserPoints = New System.Windows.Forms.TextBox()
        Me.spnUserPoints = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.txtUserName = New System.Windows.Forms.TextBox()
        Me.lblModFreq1 = New System.Windows.Forms.Label()
        Me.lblModFreq = New System.Windows.Forms.Label()
        Me.lblWrf1 = New System.Windows.Forms.Label()
        Me.lblWrf = New System.Windows.Forms.Label()
        Me.tabOptions_Page6 = New System.Windows.Forms.TabPage()
        Me.fraListCat = New System.Windows.Forms.GroupBox()
        Me.lstListCat = New System.Windows.Forms.ListBox()
        Me.cmdSegDel = New System.Windows.Forms.Button()
        Me.cmdSegAdd = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.fraSeqName = New System.Windows.Forms.GroupBox()
        Me.lstListSseqSeq = New System.Windows.Forms.ListBox()
        Me.panDwelCoun = New System.Windows.Forms.Panel()
        Me.txtDwelCoun = New System.Windows.Forms.TextBox()
        Me.spnDwelCoun = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.cmdSeqListDown = New System.Windows.Forms.Button()
        Me.cmdSeqListUp = New System.Windows.Forms.Button()
        Me.cmdSeqListRemove = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.fraFreeMem = New System.Windows.Forms.GroupBox()
        Me.lblListFreeLbl = New System.Windows.Forms.Label()
        Me.lblListSseqFreeLbl = New System.Windows.Forms.Label()
        Me.lblListFree = New System.Windows.Forms.Label()
        Me.lblListSseqFree = New System.Windows.Forms.Label()
        Me.tabOptions_Page7 = New System.Windows.Forms.TabPage()
        Me.fraOutLowPassFilt = New System.Windows.Forms.GroupBox()
        Me.chkFilterStateOn = New System.Windows.Forms.CheckBox()
        Me.optCutOff250 = New System.Windows.Forms.RadioButton()
        Me.optCutOff10 = New System.Windows.Forms.RadioButton()
        Me.fraOutLoadImpe = New System.Windows.Forms.GroupBox()
        Me.optLoadImp50 = New System.Windows.Forms.RadioButton()
        Me.optLoadImp75 = New System.Windows.Forms.RadioButton()
        Me.optLoadImpHigh = New System.Windows.Forms.RadioButton()
        Me.cmdAbout = New System.Windows.Forms.Button()
        Me.cmdTest = New System.Windows.Forms.Button()
        Me.cmdUpdateTip = New System.Windows.Forms.Button()
        Me.fraPanelConfig = New System.Windows.Forms.GroupBox()
        Me.pctMode = New NationalInstruments.UI.WindowsForms.Led()
        Me.cmdLoadFromFile = New System.Windows.Forms.Button()
        Me.cmdSaveToFile = New System.Windows.Forms.Button()
        Me.cmdLoadFromInstrument = New System.Windows.Forms.Button()
        Me.cmdLocal = New System.Windows.Forms.Button()
        Me.picLeds = New System.Windows.Forms.PictureBox()
        Me.tabOptions_Page8 = New System.Windows.Forms.TabPage()
        Me.Atlas_SFP = New VIPERT_Common_Controls.Atlas_SFP()
        Me.timDebug = New System.Windows.Forms.Timer(Me.components)
        Me.CommonDialog1 = New System.Windows.Forms.PrintDialog()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.imlOutRelayWave = New System.Windows.Forms.ImageList(Me.components)
        Me.panMainControl.SuspendLayout()
        Me.fraOnOff.SuspendLayout()
        CType(Me.imgWaveDisplay, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabOptions.SuspendLayout()
        Me.tabOptions_Page1.SuspendLayout()
        Me.fraFuncWave.SuspendLayout()
        Me.fraFreq.SuspendLayout()
        Me.panFreq.SuspendLayout()
        CType(Me.spnFreq, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraWavePol.SuspendLayout()
        Me.fraWavePoin.SuspendLayout()
        Me.panWavePoin.SuspendLayout()
        CType(Me.spnWavePoin, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraRoscSour.SuspendLayout()
        Me.panRoscFreqExt.SuspendLayout()
        CType(Me.spnRoscFreqExt, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SSFrame2.SuspendLayout()
        Me.fraAmpl.SuspendLayout()
        Me.panAmpl.SuspendLayout()
        CType(Me.spnAmpl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraAmplDiv7.SuspendLayout()
        Me.panAmplDiv7.SuspendLayout()
        CType(Me.spnAmplDiv7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraDcOffs.SuspendLayout()
        Me.panDcOffs.SuspendLayout()
        CType(Me.spnDcOffs, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraAmplUnits.SuspendLayout()
        Me.tabOptions_Page2.SuspendLayout()
        Me.fraArmLay.SuspendLayout()
        Me.panArmCoun.SuspendLayout()
        CType(Me.spnArmCount, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panArmLay2Coun.SuspendLayout()
        CType(Me.spnArmLay2Coun, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraTrigStart.SuspendLayout()
        Me.fraTrigStop.SuspendLayout()
        Me.tabOptions_Page3.SuspendLayout()
        Me.fraMarkEclt0.SuspendLayout()
        Me.fraMarkEclt1.SuspendLayout()
        Me.fraMarkExt.SuspendLayout()
        Me.fraMarkSpo.SuspendLayout()
        Me.panMarkSpo.SuspendLayout()
        CType(Me.spnMarkSpo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabOptions_Page4.SuspendLayout()
        Me.fraFreqMode.SuspendLayout()
        Me.fraFreqModeSwe.SuspendLayout()
        Me.panAgileSweepStart.SuspendLayout()
        CType(Me.spnAgileSweepStart, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panAgileSweepStop.SuspendLayout()
        CType(Me.spnAgileSweepStop, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panAgileSweepSteps.SuspendLayout()
        CType(Me.spnSweCoun, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panAgileSweepDur.SuspendLayout()
        CType(Me.spnAgileSweepDur, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panSweCoun.SuspendLayout()
        CType(Me.spnAgileSweepSteps, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraAgileFsk.SuspendLayout()
        Me.panAgileFskKeyH.SuspendLayout()
        CType(Me.spnAgileFskKeyH, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panAgileFskKeyL.SuspendLayout()
        CType(Me.spnAgileFskKeyL, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabOptions_Page5.SuspendLayout()
        CType(Me.G1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CursorMarker, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CursorTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CursorFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panSampleFreq.SuspendLayout()
        CType(Me.spnSampleFreq, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panUserPoints.SuspendLayout()
        CType(Me.spnUserPoints, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabOptions_Page6.SuspendLayout()
        Me.fraListCat.SuspendLayout()
        Me.fraSeqName.SuspendLayout()
        Me.panDwelCoun.SuspendLayout()
        CType(Me.spnDwelCoun, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraFreeMem.SuspendLayout()
        Me.tabOptions_Page7.SuspendLayout()
        Me.fraOutLowPassFilt.SuspendLayout()
        Me.fraOutLoadImpe.SuspendLayout()
        Me.fraPanelConfig.SuspendLayout()
        CType(Me.pctMode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picLeds, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabOptions_Page8.SuspendLayout()
        Me.SuspendLayout()
        '
        'dlgFileIO
        '
        Me.dlgFileIO.FileName = "*.seg"
        Me.dlgFileIO.Filter = "*.seg|"
        '
        'cmdHelp
        '
        Me.cmdHelp.BackColor = System.Drawing.SystemColors.Control
        Me.cmdHelp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdHelp.Location = New System.Drawing.Point(447, 384)
        Me.cmdHelp.Name = "cmdHelp"
        Me.cmdHelp.Size = New System.Drawing.Size(82, 23)
        Me.cmdHelp.TabIndex = 206
        Me.cmdHelp.Text = "Help"
        Me.cmdHelp.UseVisualStyleBackColor = False
        '
        'cmdQuit
        '
        Me.cmdQuit.BackColor = System.Drawing.SystemColors.Control
        Me.cmdQuit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdQuit.Location = New System.Drawing.Point(652, 384)
        Me.cmdQuit.Name = "cmdQuit"
        Me.cmdQuit.Size = New System.Drawing.Size(76, 23)
        Me.cmdQuit.TabIndex = 9
        Me.cmdQuit.Text = "&Quit"
        Me.cmdQuit.UseVisualStyleBackColor = False
        '
        'cmdReset
        '
        Me.cmdReset.BackColor = System.Drawing.SystemColors.Control
        Me.cmdReset.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdReset.Location = New System.Drawing.Point(556, 384)
        Me.cmdReset.Name = "cmdReset"
        Me.cmdReset.Size = New System.Drawing.Size(76, 23)
        Me.cmdReset.TabIndex = 8
        Me.cmdReset.Text = "&Reset"
        Me.cmdReset.UseVisualStyleBackColor = False
        '
        'panMainControl
        '
        Me.panMainControl.BackColor = System.Drawing.SystemColors.Control
        Me.panMainControl.Controls.Add(Me.fraOnOff)
        Me.panMainControl.Controls.Add(Me.chkOutpStat)
        Me.panMainControl.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panMainControl.Location = New System.Drawing.Point(443, 303)
        Me.panMainControl.Name = "panMainControl"
        Me.panMainControl.Size = New System.Drawing.Size(146, 61)
        Me.panMainControl.TabIndex = 2
        '
        'fraOnOff
        '
        Me.fraOnOff.Controls.Add(Me.imgWaveDisplay)
        Me.fraOnOff.Controls.Add(Me.tolOnOff)
        Me.fraOnOff.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraOnOff.Location = New System.Drawing.Point(19, 20)
        Me.fraOnOff.Name = "fraOnOff"
        Me.fraOnOff.Size = New System.Drawing.Size(113, 34)
        Me.fraOnOff.TabIndex = 4
        Me.fraOnOff.TabStop = False
        '
        'imgWaveDisplay
        '
        Me.imgWaveDisplay.BackColor = System.Drawing.SystemColors.Control
        Me.imgWaveDisplay.InitialImage = CType(resources.GetObject("imgWaveDisplay.InitialImage"), System.Drawing.Image)
        Me.imgWaveDisplay.Location = New System.Drawing.Point(1, 7)
        Me.imgWaveDisplay.Name = "imgWaveDisplay"
        Me.imgWaveDisplay.Size = New System.Drawing.Size(34, 26)
        Me.imgWaveDisplay.TabIndex = 0
        Me.imgWaveDisplay.TabStop = False
        '
        'tolOnOff
        '
        Me.tolOnOff.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.ribInit, Me.ribAbor})
        Me.tolOnOff.ButtonSize = New System.Drawing.Size(32, 24)
        Me.tolOnOff.Dock = System.Windows.Forms.DockStyle.None
        Me.tolOnOff.DropDownArrows = True
        Me.tolOnOff.ImageList = Me.imlOnOff
        Me.tolOnOff.Location = New System.Drawing.Point(36, -1)
        Me.tolOnOff.Name = "tolOnOff"
        Me.tolOnOff.ShowToolTips = True
        Me.tolOnOff.Size = New System.Drawing.Size(78, 42)
        Me.tolOnOff.TabIndex = 1
        '
        'ribInit
        '
        Me.ribInit.ImageIndex = 0
        Me.ribInit.Name = "ribInit"
        Me.ribInit.ToolTipText = "Initialize"
        '
        'ribAbor
        '
        Me.ribAbor.ImageIndex = 1
        Me.ribAbor.Name = "ribAbor"
        Me.ribAbor.Pushed = True
        Me.ribAbor.ToolTipText = "Abort"
        '
        'imlOnOff
        '
        Me.imlOnOff.ImageStream = CType(resources.GetObject("imlOnOff.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imlOnOff.TransparentColor = System.Drawing.Color.Transparent
        Me.imlOnOff.Images.SetKeyName(0, "30_INIT.BMP")
        Me.imlOnOff.Images.SetKeyName(1, "30_ABOR.BMP")
        '
        'chkOutpStat
        '
        Me.chkOutpStat.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkOutpStat.Location = New System.Drawing.Point(7, 1)
        Me.chkOutpStat.Name = "chkOutpStat"
        Me.chkOutpStat.Size = New System.Drawing.Size(132, 21)
        Me.chkOutpStat.TabIndex = 0
        Me.chkOutpStat.Text = "&Output Relay On"
        '
        'txtVoid
        '
        Me.txtVoid.BackColor = System.Drawing.SystemColors.Window
        Me.txtVoid.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtVoid.Location = New System.Drawing.Point(184, 384)
        Me.txtVoid.Name = "txtVoid"
        Me.txtVoid.Size = New System.Drawing.Size(33, 20)
        Me.txtVoid.TabIndex = 1
        Me.txtVoid.Text = "Void"
        Me.txtVoid.Visible = False
        '
        'sbrUserInformation
        '
        Me.sbrUserInformation.Location = New System.Drawing.Point(0, 420)
        Me.sbrUserInformation.Name = "sbrUserInformation"
        Me.sbrUserInformation.Size = New System.Drawing.Size(743, 17)
        Me.sbrUserInformation.SizingGrip = False
        Me.sbrUserInformation.TabIndex = 0
        '
        'tabOptions
        '
        Me.tabOptions.Controls.Add(Me.tabOptions_Page1)
        Me.tabOptions.Controls.Add(Me.tabOptions_Page2)
        Me.tabOptions.Controls.Add(Me.tabOptions_Page3)
        Me.tabOptions.Controls.Add(Me.tabOptions_Page4)
        Me.tabOptions.Controls.Add(Me.tabOptions_Page5)
        Me.tabOptions.Controls.Add(Me.tabOptions_Page6)
        Me.tabOptions.Controls.Add(Me.tabOptions_Page7)
        Me.tabOptions.Controls.Add(Me.tabOptions_Page8)
        Me.tabOptions.Location = New System.Drawing.Point(0, 6)
        Me.tabOptions.Name = "tabOptions"
        Me.tabOptions.SelectedIndex = 0
        Me.tabOptions.Size = New System.Drawing.Size(731, 372)
        Me.tabOptions.TabIndex = 10
        '
        'tabOptions_Page1
        '
        Me.tabOptions_Page1.Controls.Add(Me.fraFuncWave)
        Me.tabOptions_Page1.Controls.Add(Me.fraFreq)
        Me.tabOptions_Page1.Controls.Add(Me.fraWavePol)
        Me.tabOptions_Page1.Controls.Add(Me.fraWavePoin)
        Me.tabOptions_Page1.Controls.Add(Me.fraRoscSour)
        Me.tabOptions_Page1.Controls.Add(Me.SSFrame2)
        Me.tabOptions_Page1.Location = New System.Drawing.Point(4, 22)
        Me.tabOptions_Page1.Name = "tabOptions_Page1"
        Me.tabOptions_Page1.Size = New System.Drawing.Size(723, 346)
        Me.tabOptions_Page1.TabIndex = 0
        Me.tabOptions_Page1.Text = "Main"
        '
        'fraFuncWave
        '
        Me.fraFuncWave.Controls.Add(Me.tolFuncWave)
        Me.fraFuncWave.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraFuncWave.Location = New System.Drawing.Point(8, 4)
        Me.fraFuncWave.Name = "fraFuncWave"
        Me.fraFuncWave.Size = New System.Drawing.Size(43, 193)
        Me.fraFuncWave.TabIndex = 15
        Me.fraFuncWave.TabStop = False
        '
        'tolFuncWave
        '
        Me.tolFuncWave.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.Sine, Me.Square, Me.Ramp, Me.Triangle, Me.DC, Me.User})
        Me.tolFuncWave.ButtonSize = New System.Drawing.Size(30, 30)
        Me.tolFuncWave.Dock = System.Windows.Forms.DockStyle.None
        Me.tolFuncWave.DropDownArrows = True
        Me.tolFuncWave.ImageList = Me.imlFuncWave
        Me.tolFuncWave.Location = New System.Drawing.Point(2, 6)
        Me.tolFuncWave.Name = "tolFuncWave"
        Me.tolFuncWave.ShowToolTips = True
        Me.tolFuncWave.Size = New System.Drawing.Size(40, 186)
        Me.tolFuncWave.TabIndex = 16
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
        'Ramp
        '
        Me.Ramp.ImageIndex = 2
        Me.Ramp.Name = "Ramp"
        Me.Ramp.ToolTipText = "Ramp Wave"
        '
        'Triangle
        '
        Me.Triangle.ImageIndex = 3
        Me.Triangle.Name = "Triangle"
        Me.Triangle.ToolTipText = "Triangle Wave"
        '
        'DC
        '
        Me.DC.ImageIndex = 4
        Me.DC.Name = "DC"
        Me.DC.ToolTipText = "DC Level"
        '
        'User
        '
        Me.User.ImageIndex = 5
        Me.User.Name = "User"
        Me.User.ToolTipText = "User Defined Complex Waveform"
        '
        'imlFuncWave
        '
        Me.imlFuncWave.ImageStream = CType(resources.GetObject("imlFuncWave.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imlFuncWave.TransparentColor = System.Drawing.Color.Transparent
        Me.imlFuncWave.Images.SetKeyName(0, "Sine")
        Me.imlFuncWave.Images.SetKeyName(1, "Square")
        Me.imlFuncWave.Images.SetKeyName(2, "Ramp")
        Me.imlFuncWave.Images.SetKeyName(3, "Triangle")
        Me.imlFuncWave.Images.SetKeyName(4, "DC")
        Me.imlFuncWave.Images.SetKeyName(5, "User")
        '
        'fraFreq
        '
        Me.fraFreq.Controls.Add(Me.panFreq)
        Me.fraFreq.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraFreq.Location = New System.Drawing.Point(65, 16)
        Me.fraFreq.Name = "fraFreq"
        Me.fraFreq.Size = New System.Drawing.Size(265, 43)
        Me.fraFreq.TabIndex = 48
        Me.fraFreq.TabStop = False
        Me.fraFreq.Text = "Frequency"
        '
        'panFreq
        '
        Me.panFreq.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.panFreq.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panFreq.Controls.Add(Me.txtFreq)
        Me.panFreq.Controls.Add(Me.spnFreq)
        Me.panFreq.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panFreq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.panFreq.Location = New System.Drawing.Point(16, 17)
        Me.panFreq.Name = "panFreq"
        Me.panFreq.Size = New System.Drawing.Size(139, 21)
        Me.panFreq.TabIndex = 49
        '
        'txtFreq
        '
        Me.txtFreq.BackColor = System.Drawing.SystemColors.Window
        Me.txtFreq.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtFreq.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFreq.Location = New System.Drawing.Point(2, 2)
        Me.txtFreq.Name = "txtFreq"
        Me.txtFreq.Size = New System.Drawing.Size(116, 13)
        Me.txtFreq.TabIndex = 50
        '
        'spnFreq
        '
        Me.spnFreq.Location = New System.Drawing.Point(121, -2)
        Me.spnFreq.Name = "spnFreq"
        Me.spnFreq.Size = New System.Drawing.Size(16, 20)
        Me.spnFreq.TabIndex = 52
        '
        'fraWavePol
        '
        Me.fraWavePol.Controls.Add(Me.cboWavePol)
        Me.fraWavePol.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraWavePol.Location = New System.Drawing.Point(351, 119)
        Me.fraWavePol.Name = "fraWavePol"
        Me.fraWavePol.Size = New System.Drawing.Size(145, 51)
        Me.fraWavePol.TabIndex = 92
        Me.fraWavePol.TabStop = False
        Me.fraWavePol.Text = "Waveform Polarity"
        '
        'cboWavePol
        '
        Me.cboWavePol.BackColor = System.Drawing.SystemColors.Window
        Me.cboWavePol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboWavePol.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboWavePol.Location = New System.Drawing.Point(5, 17)
        Me.cboWavePol.Name = "cboWavePol"
        Me.cboWavePol.Size = New System.Drawing.Size(98, 21)
        Me.cboWavePol.TabIndex = 93
        '
        'fraWavePoin
        '
        Me.fraWavePoin.Controls.Add(Me.panWavePoin)
        Me.fraWavePoin.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraWavePoin.Location = New System.Drawing.Point(351, 176)
        Me.fraWavePoin.Name = "fraWavePoin"
        Me.fraWavePoin.Size = New System.Drawing.Size(145, 54)
        Me.fraWavePoin.TabIndex = 149
        Me.fraWavePoin.TabStop = False
        Me.fraWavePoin.Text = "Waveform Points"
        '
        'panWavePoin
        '
        Me.panWavePoin.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.panWavePoin.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panWavePoin.Controls.Add(Me.txtWavePoin)
        Me.panWavePoin.Controls.Add(Me.spnWavePoin)
        Me.panWavePoin.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panWavePoin.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.panWavePoin.Location = New System.Drawing.Point(9, 20)
        Me.panWavePoin.Name = "panWavePoin"
        Me.panWavePoin.Size = New System.Drawing.Size(102, 21)
        Me.panWavePoin.TabIndex = 150
        '
        'txtWavePoin
        '
        Me.txtWavePoin.BackColor = System.Drawing.SystemColors.Window
        Me.txtWavePoin.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtWavePoin.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtWavePoin.Location = New System.Drawing.Point(2, 2)
        Me.txtWavePoin.Name = "txtWavePoin"
        Me.txtWavePoin.Size = New System.Drawing.Size(79, 13)
        Me.txtWavePoin.TabIndex = 151
        '
        'spnWavePoin
        '
        Me.spnWavePoin.Location = New System.Drawing.Point(84, -1)
        Me.spnWavePoin.Name = "spnWavePoin"
        Me.spnWavePoin.Size = New System.Drawing.Size(16, 20)
        Me.spnWavePoin.TabIndex = 152
        '
        'fraRoscSour
        '
        Me.fraRoscSour.Controls.Add(Me.lblRoscSour)
        Me.fraRoscSour.Controls.Add(Me.cboRoscSour)
        Me.fraRoscSour.Controls.Add(Me.panRoscFreqExt)
        Me.fraRoscSour.Controls.Add(Me.lblRoscFreqExt)
        Me.fraRoscSour.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraRoscSour.Location = New System.Drawing.Point(516, 109)
        Me.fraRoscSour.Name = "fraRoscSour"
        Me.fraRoscSour.Size = New System.Drawing.Size(184, 133)
        Me.fraRoscSour.TabIndex = 153
        Me.fraRoscSour.TabStop = False
        Me.fraRoscSour.Text = "Reference Oscillator"
        '
        'lblRoscSour
        '
        Me.lblRoscSour.AutoSize = True
        Me.lblRoscSour.BackColor = System.Drawing.SystemColors.Control
        Me.lblRoscSour.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblRoscSour.Location = New System.Drawing.Point(18, 18)
        Me.lblRoscSour.Name = "lblRoscSour"
        Me.lblRoscSour.Size = New System.Drawing.Size(41, 13)
        Me.lblRoscSour.TabIndex = 158
        Me.lblRoscSour.Text = "Source"
        Me.lblRoscSour.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'cboRoscSour
        '
        Me.cboRoscSour.BackColor = System.Drawing.SystemColors.Window
        Me.cboRoscSour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboRoscSour.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboRoscSour.Location = New System.Drawing.Point(16, 36)
        Me.cboRoscSour.Name = "cboRoscSour"
        Me.cboRoscSour.Size = New System.Drawing.Size(139, 21)
        Me.cboRoscSour.TabIndex = 154
        '
        'panRoscFreqExt
        '
        Me.panRoscFreqExt.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.panRoscFreqExt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panRoscFreqExt.Controls.Add(Me.txtRoscFreqExt)
        Me.panRoscFreqExt.Controls.Add(Me.spnRoscFreqExt)
        Me.panRoscFreqExt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panRoscFreqExt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.panRoscFreqExt.Location = New System.Drawing.Point(22, 98)
        Me.panRoscFreqExt.Name = "panRoscFreqExt"
        Me.panRoscFreqExt.Size = New System.Drawing.Size(131, 21)
        Me.panRoscFreqExt.TabIndex = 155
        '
        'txtRoscFreqExt
        '
        Me.txtRoscFreqExt.BackColor = System.Drawing.SystemColors.Window
        Me.txtRoscFreqExt.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtRoscFreqExt.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRoscFreqExt.Location = New System.Drawing.Point(2, 2)
        Me.txtRoscFreqExt.Name = "txtRoscFreqExt"
        Me.txtRoscFreqExt.Size = New System.Drawing.Size(108, 13)
        Me.txtRoscFreqExt.TabIndex = 156
        '
        'spnRoscFreqExt
        '
        Me.spnRoscFreqExt.Location = New System.Drawing.Point(113, -2)
        Me.spnRoscFreqExt.Name = "spnRoscFreqExt"
        Me.spnRoscFreqExt.Size = New System.Drawing.Size(16, 20)
        Me.spnRoscFreqExt.TabIndex = 157
        '
        'lblRoscFreqExt
        '
        Me.lblRoscFreqExt.BackColor = System.Drawing.SystemColors.Control
        Me.lblRoscFreqExt.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblRoscFreqExt.Location = New System.Drawing.Point(1, 77)
        Me.lblRoscFreqExt.Name = "lblRoscFreqExt"
        Me.lblRoscFreqExt.Size = New System.Drawing.Size(170, 18)
        Me.lblRoscFreqExt.TabIndex = 159
        Me.lblRoscFreqExt.Text = "Reference Oscillator Freq"
        Me.lblRoscFreqExt.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'SSFrame2
        '
        Me.SSFrame2.Controls.Add(Me.fraAmpl)
        Me.SSFrame2.Controls.Add(Me.fraAmplDiv7)
        Me.SSFrame2.Controls.Add(Me.fraDcOffs)
        Me.SSFrame2.Controls.Add(Me.fraAmplUnits)
        Me.SSFrame2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SSFrame2.Location = New System.Drawing.Point(65, 65)
        Me.SSFrame2.Name = "SSFrame2"
        Me.SSFrame2.Size = New System.Drawing.Size(265, 177)
        Me.SSFrame2.TabIndex = 197
        Me.SSFrame2.TabStop = False
        Me.SSFrame2.Text = "Amplitude"
        '
        'fraAmpl
        '
        Me.fraAmpl.Controls.Add(Me.panAmpl)
        Me.fraAmpl.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraAmpl.Location = New System.Drawing.Point(17, 17)
        Me.fraAmpl.Name = "fraAmpl"
        Me.fraAmpl.Size = New System.Drawing.Size(155, 43)
        Me.fraAmpl.TabIndex = 40
        Me.fraAmpl.TabStop = False
        Me.fraAmpl.Text = "Amplitude"
        '
        'panAmpl
        '
        Me.panAmpl.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.panAmpl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panAmpl.Controls.Add(Me.txtAmpl)
        Me.panAmpl.Controls.Add(Me.spnAmpl)
        Me.panAmpl.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panAmpl.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.panAmpl.Location = New System.Drawing.Point(5, 17)
        Me.panAmpl.Name = "panAmpl"
        Me.panAmpl.Size = New System.Drawing.Size(131, 21)
        Me.panAmpl.TabIndex = 41
        '
        'txtAmpl
        '
        Me.txtAmpl.BackColor = System.Drawing.SystemColors.Window
        Me.txtAmpl.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtAmpl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAmpl.Location = New System.Drawing.Point(0, 2)
        Me.txtAmpl.Name = "txtAmpl"
        Me.txtAmpl.Size = New System.Drawing.Size(110, 13)
        Me.txtAmpl.TabIndex = 42
        '
        'spnAmpl
        '
        Me.spnAmpl.Location = New System.Drawing.Point(113, -2)
        Me.spnAmpl.Name = "spnAmpl"
        Me.spnAmpl.Size = New System.Drawing.Size(16, 20)
        Me.spnAmpl.TabIndex = 43
        '
        'fraAmplDiv7
        '
        Me.fraAmplDiv7.Controls.Add(Me.panAmplDiv7)
        Me.fraAmplDiv7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraAmplDiv7.Location = New System.Drawing.Point(17, 68)
        Me.fraAmplDiv7.Name = "fraAmplDiv7"
        Me.fraAmplDiv7.Size = New System.Drawing.Size(155, 43)
        Me.fraAmplDiv7.TabIndex = 176
        Me.fraAmplDiv7.TabStop = False
        Me.fraAmplDiv7.Text = "/ 7 Amplitude"
        '
        'panAmplDiv7
        '
        Me.panAmplDiv7.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.panAmplDiv7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panAmplDiv7.Controls.Add(Me.txtAmplDiv7)
        Me.panAmplDiv7.Controls.Add(Me.spnAmplDiv7)
        Me.panAmplDiv7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panAmplDiv7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.panAmplDiv7.Location = New System.Drawing.Point(5, 17)
        Me.panAmplDiv7.Name = "panAmplDiv7"
        Me.panAmplDiv7.Size = New System.Drawing.Size(131, 21)
        Me.panAmplDiv7.TabIndex = 177
        '
        'txtAmplDiv7
        '
        Me.txtAmplDiv7.BackColor = System.Drawing.SystemColors.Window
        Me.txtAmplDiv7.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtAmplDiv7.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAmplDiv7.Location = New System.Drawing.Point(0, 2)
        Me.txtAmplDiv7.Name = "txtAmplDiv7"
        Me.txtAmplDiv7.Size = New System.Drawing.Size(110, 13)
        Me.txtAmplDiv7.TabIndex = 178
        '
        'spnAmplDiv7
        '
        Me.spnAmplDiv7.Location = New System.Drawing.Point(113, -2)
        Me.spnAmplDiv7.Name = "spnAmplDiv7"
        Me.spnAmplDiv7.Size = New System.Drawing.Size(16, 20)
        Me.spnAmplDiv7.TabIndex = 179
        '
        'fraDcOffs
        '
        Me.fraDcOffs.Controls.Add(Me.panDcOffs)
        Me.fraDcOffs.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraDcOffs.Location = New System.Drawing.Point(17, 120)
        Me.fraDcOffs.Name = "fraDcOffs"
        Me.fraDcOffs.Size = New System.Drawing.Size(155, 43)
        Me.fraDcOffs.TabIndex = 44
        Me.fraDcOffs.TabStop = False
        Me.fraDcOffs.Text = "DC Offset"
        '
        'panDcOffs
        '
        Me.panDcOffs.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.panDcOffs.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panDcOffs.Controls.Add(Me.txtDcOffs)
        Me.panDcOffs.Controls.Add(Me.spnDcOffs)
        Me.panDcOffs.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panDcOffs.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.panDcOffs.Location = New System.Drawing.Point(5, 17)
        Me.panDcOffs.Name = "panDcOffs"
        Me.panDcOffs.Size = New System.Drawing.Size(131, 21)
        Me.panDcOffs.TabIndex = 45
        '
        'txtDcOffs
        '
        Me.txtDcOffs.BackColor = System.Drawing.SystemColors.Window
        Me.txtDcOffs.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtDcOffs.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDcOffs.Location = New System.Drawing.Point(0, 2)
        Me.txtDcOffs.Name = "txtDcOffs"
        Me.txtDcOffs.Size = New System.Drawing.Size(110, 13)
        Me.txtDcOffs.TabIndex = 46
        '
        'spnDcOffs
        '
        Me.spnDcOffs.Location = New System.Drawing.Point(113, -2)
        Me.spnDcOffs.Name = "spnDcOffs"
        Me.spnDcOffs.Size = New System.Drawing.Size(16, 20)
        Me.spnDcOffs.TabIndex = 47
        '
        'fraAmplUnits
        '
        Me.fraAmplUnits.Controls.Add(Me.cboUnits)
        Me.fraAmplUnits.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraAmplUnits.Location = New System.Drawing.Point(193, 68)
        Me.fraAmplUnits.Name = "fraAmplUnits"
        Me.fraAmplUnits.Size = New System.Drawing.Size(60, 43)
        Me.fraAmplUnits.TabIndex = 180
        Me.fraAmplUnits.TabStop = False
        Me.fraAmplUnits.Text = "Units"
        '
        'cboUnits
        '
        Me.cboUnits.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cboUnits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboUnits.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboUnits.Items.AddRange(New Object() {"dBmW"})
        Me.cboUnits.Location = New System.Drawing.Point(5, 17)
        Me.cboUnits.Name = "cboUnits"
        Me.cboUnits.Size = New System.Drawing.Size(50, 21)
        Me.cboUnits.TabIndex = 181
        '
        'tabOptions_Page2
        '
        Me.tabOptions_Page2.Controls.Add(Me.fraArmLay)
        Me.tabOptions_Page2.Controls.Add(Me.fraTrigStart)
        Me.tabOptions_Page2.Controls.Add(Me.fraTrigStop)
        Me.tabOptions_Page2.Location = New System.Drawing.Point(4, 22)
        Me.tabOptions_Page2.Name = "tabOptions_Page2"
        Me.tabOptions_Page2.Size = New System.Drawing.Size(723, 346)
        Me.tabOptions_Page2.TabIndex = 1
        Me.tabOptions_Page2.Text = "Arm-Trig"
        '
        'fraArmLay
        '
        Me.fraArmLay.Controls.Add(Me.cboArmLay2Sour)
        Me.fraArmLay.Controls.Add(Me.cboArmLay2Slop)
        Me.fraArmLay.Controls.Add(Me.panArmCoun)
        Me.fraArmLay.Controls.Add(Me.panArmLay2Coun)
        Me.fraArmLay.Controls.Add(Me.cmdArmCounInf_2)
        Me.fraArmLay.Controls.Add(Me.cmdArmLay2)
        Me.fraArmLay.Controls.Add(Me.cmdArmCounInf_1)
        Me.fraArmLay.Controls.Add(Me.lblArmLay2Sour)
        Me.fraArmLay.Controls.Add(Me.lblArmLay2Slop)
        Me.fraArmLay.Controls.Add(Me.lblArmLay2Coun)
        Me.fraArmLay.Controls.Add(Me.lblArmCoun)
        Me.fraArmLay.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraArmLay.Location = New System.Drawing.Point(8, 4)
        Me.fraArmLay.Name = "fraArmLay"
        Me.fraArmLay.Size = New System.Drawing.Size(144, 317)
        Me.fraArmLay.TabIndex = 17
        Me.fraArmLay.TabStop = False
        Me.fraArmLay.Text = "Arm Start"
        '
        'cboArmLay2Sour
        '
        Me.cboArmLay2Sour.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cboArmLay2Sour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboArmLay2Sour.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboArmLay2Sour.Location = New System.Drawing.Point(8, 40)
        Me.cboArmLay2Sour.Name = "cboArmLay2Sour"
        Me.cboArmLay2Sour.Size = New System.Drawing.Size(115, 21)
        Me.cboArmLay2Sour.TabIndex = 19
        '
        'cboArmLay2Slop
        '
        Me.cboArmLay2Slop.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cboArmLay2Slop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboArmLay2Slop.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboArmLay2Slop.Location = New System.Drawing.Point(8, 86)
        Me.cboArmLay2Slop.Name = "cboArmLay2Slop"
        Me.cboArmLay2Slop.Size = New System.Drawing.Size(115, 21)
        Me.cboArmLay2Slop.TabIndex = 18
        '
        'panArmCoun
        '
        Me.panArmCoun.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.panArmCoun.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panArmCoun.Controls.Add(Me.txtArmCoun)
        Me.panArmCoun.Controls.Add(Me.spnArmCount)
        Me.panArmCoun.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panArmCoun.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.panArmCoun.Location = New System.Drawing.Point(8, 222)
        Me.panArmCoun.Name = "panArmCoun"
        Me.panArmCoun.Size = New System.Drawing.Size(110, 21)
        Me.panArmCoun.TabIndex = 20
        '
        'txtArmCoun
        '
        Me.txtArmCoun.BackColor = System.Drawing.SystemColors.Window
        Me.txtArmCoun.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtArmCoun.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtArmCoun.Location = New System.Drawing.Point(2, 2)
        Me.txtArmCoun.Name = "txtArmCoun"
        Me.txtArmCoun.Size = New System.Drawing.Size(87, 13)
        Me.txtArmCoun.TabIndex = 21
        '
        'spnArmCount
        '
        Me.spnArmCount.Location = New System.Drawing.Point(92, -2)
        Me.spnArmCount.Name = "spnArmCount"
        Me.spnArmCount.Size = New System.Drawing.Size(16, 20)
        Me.spnArmCount.TabIndex = 25
        '
        'panArmLay2Coun
        '
        Me.panArmLay2Coun.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.panArmLay2Coun.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panArmLay2Coun.Controls.Add(Me.txtArmLay2Coun)
        Me.panArmLay2Coun.Controls.Add(Me.spnArmLay2Coun)
        Me.panArmLay2Coun.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panArmLay2Coun.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.panArmLay2Coun.Location = New System.Drawing.Point(8, 142)
        Me.panArmLay2Coun.Name = "panArmLay2Coun"
        Me.panArmLay2Coun.Size = New System.Drawing.Size(110, 21)
        Me.panArmLay2Coun.TabIndex = 23
        '
        'txtArmLay2Coun
        '
        Me.txtArmLay2Coun.BackColor = System.Drawing.SystemColors.Window
        Me.txtArmLay2Coun.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtArmLay2Coun.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtArmLay2Coun.Location = New System.Drawing.Point(2, 2)
        Me.txtArmLay2Coun.Name = "txtArmLay2Coun"
        Me.txtArmLay2Coun.Size = New System.Drawing.Size(87, 13)
        Me.txtArmLay2Coun.TabIndex = 24
        '
        'spnArmLay2Coun
        '
        Me.spnArmLay2Coun.Location = New System.Drawing.Point(92, -2)
        Me.spnArmLay2Coun.Name = "spnArmLay2Coun"
        Me.spnArmLay2Coun.Size = New System.Drawing.Size(16, 20)
        Me.spnArmLay2Coun.TabIndex = 25
        '
        'cmdArmCounInf_2
        '
        Me.cmdArmCounInf_2.Location = New System.Drawing.Point(8, 167)
        Me.cmdArmCounInf_2.Margin = New System.Windows.Forms.Padding(0)
        Me.cmdArmCounInf_2.Name = "cmdArmCounInf_2"
        Me.cmdArmCounInf_2.Size = New System.Drawing.Size(115, 25)
        Me.cmdArmCounInf_2.TabIndex = 26
        Me.cmdArmCounInf_2.Text = "Set to Infinity"
        '
        'cmdArmLay2
        '
        Me.cmdArmLay2.Location = New System.Drawing.Point(8, 281)
        Me.cmdArmLay2.Margin = New System.Windows.Forms.Padding(0)
        Me.cmdArmLay2.Name = "cmdArmLay2"
        Me.cmdArmLay2.Size = New System.Drawing.Size(115, 25)
        Me.cmdArmLay2.TabIndex = 27
        Me.cmdArmLay2.Text = "Arm Immediate"
        '
        'cmdArmCounInf_1
        '
        Me.cmdArmCounInf_1.Location = New System.Drawing.Point(8, 245)
        Me.cmdArmCounInf_1.Margin = New System.Windows.Forms.Padding(0)
        Me.cmdArmCounInf_1.Name = "cmdArmCounInf_1"
        Me.cmdArmCounInf_1.Size = New System.Drawing.Size(115, 25)
        Me.cmdArmCounInf_1.TabIndex = 28
        Me.cmdArmCounInf_1.Text = "Set to Infinity"
        '
        'lblArmLay2Sour
        '
        Me.lblArmLay2Sour.AutoSize = True
        Me.lblArmLay2Sour.BackColor = System.Drawing.SystemColors.Control
        Me.lblArmLay2Sour.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblArmLay2Sour.Location = New System.Drawing.Point(14, 23)
        Me.lblArmLay2Sour.Name = "lblArmLay2Sour"
        Me.lblArmLay2Sour.Size = New System.Drawing.Size(79, 13)
        Me.lblArmLay2Sour.TabIndex = 32
        Me.lblArmLay2Sour.Text = "Layer 2 Source"
        Me.lblArmLay2Sour.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblArmLay2Slop
        '
        Me.lblArmLay2Slop.AutoSize = True
        Me.lblArmLay2Slop.BackColor = System.Drawing.SystemColors.Control
        Me.lblArmLay2Slop.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblArmLay2Slop.Location = New System.Drawing.Point(14, 66)
        Me.lblArmLay2Slop.Name = "lblArmLay2Slop"
        Me.lblArmLay2Slop.Size = New System.Drawing.Size(72, 13)
        Me.lblArmLay2Slop.TabIndex = 30
        Me.lblArmLay2Slop.Text = "Layer 2 Slope"
        Me.lblArmLay2Slop.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblArmLay2Coun
        '
        Me.lblArmLay2Coun.AutoSize = True
        Me.lblArmLay2Coun.BackColor = System.Drawing.SystemColors.Control
        Me.lblArmLay2Coun.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblArmLay2Coun.Location = New System.Drawing.Point(8, 124)
        Me.lblArmLay2Coun.Name = "lblArmLay2Coun"
        Me.lblArmLay2Coun.Size = New System.Drawing.Size(73, 13)
        Me.lblArmLay2Coun.TabIndex = 31
        Me.lblArmLay2Coun.Text = "Layer 2 Count"
        Me.lblArmLay2Coun.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblArmCoun
        '
        Me.lblArmCoun.AutoSize = True
        Me.lblArmCoun.BackColor = System.Drawing.SystemColors.Control
        Me.lblArmCoun.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblArmCoun.Location = New System.Drawing.Point(8, 204)
        Me.lblArmCoun.Name = "lblArmCoun"
        Me.lblArmCoun.Size = New System.Drawing.Size(73, 13)
        Me.lblArmCoun.TabIndex = 29
        Me.lblArmCoun.Text = "Layer 1 Count"
        Me.lblArmCoun.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'fraTrigStart
        '
        Me.fraTrigStart.Controls.Add(Me.lblTrigSlop)
        Me.fraTrigStart.Controls.Add(Me.cboTrigSlop)
        Me.fraTrigStart.Controls.Add(Me.cboTrigGatePol)
        Me.fraTrigStart.Controls.Add(Me.lblTrigSour)
        Me.fraTrigStart.Controls.Add(Me.cboTrigGateSour)
        Me.fraTrigStart.Controls.Add(Me.cboTrigSour)
        Me.fraTrigStart.Controls.Add(Me.chkTrigGateStat)
        Me.fraTrigStart.Controls.Add(Me.cmdTrig)
        Me.fraTrigStart.Controls.Add(Me.lblTrigGateStat)
        Me.fraTrigStart.Controls.Add(Me.lblTrigGateSour)
        Me.fraTrigStart.Controls.Add(Me.lblTrigGatePol)
        Me.fraTrigStart.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraTrigStart.Location = New System.Drawing.Point(166, 4)
        Me.fraTrigStart.Name = "fraTrigStart"
        Me.fraTrigStart.Size = New System.Drawing.Size(157, 317)
        Me.fraTrigStart.TabIndex = 60
        Me.fraTrigStart.TabStop = False
        Me.fraTrigStart.Text = "Trigger Start"
        '
        'lblTrigSlop
        '
        Me.lblTrigSlop.BackColor = System.Drawing.SystemColors.Control
        Me.lblTrigSlop.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTrigSlop.Location = New System.Drawing.Point(7, 69)
        Me.lblTrigSlop.Name = "lblTrigSlop"
        Me.lblTrigSlop.Size = New System.Drawing.Size(132, 20)
        Me.lblTrigSlop.TabIndex = 71
        Me.lblTrigSlop.Text = "Trigger Start Slope"
        Me.lblTrigSlop.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'cboTrigSlop
        '
        Me.cboTrigSlop.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cboTrigSlop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTrigSlop.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboTrigSlop.Location = New System.Drawing.Point(16, 90)
        Me.cboTrigSlop.Name = "cboTrigSlop"
        Me.cboTrigSlop.Size = New System.Drawing.Size(115, 21)
        Me.cboTrigSlop.TabIndex = 64
        '
        'cboTrigGatePol
        '
        Me.cboTrigGatePol.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cboTrigGatePol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTrigGatePol.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboTrigGatePol.Location = New System.Drawing.Point(16, 192)
        Me.cboTrigGatePol.Name = "cboTrigGatePol"
        Me.cboTrigGatePol.Size = New System.Drawing.Size(115, 21)
        Me.cboTrigGatePol.TabIndex = 63
        '
        'lblTrigSour
        '
        Me.lblTrigSour.AutoSize = True
        Me.lblTrigSour.BackColor = System.Drawing.SystemColors.Control
        Me.lblTrigSour.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTrigSour.Location = New System.Drawing.Point(6, 20)
        Me.lblTrigSour.Name = "lblTrigSour"
        Me.lblTrigSour.Size = New System.Drawing.Size(102, 13)
        Me.lblTrigSour.TabIndex = 67
        Me.lblTrigSour.Text = "Trigger Start Source"
        Me.lblTrigSour.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'cboTrigGateSour
        '
        Me.cboTrigGateSour.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cboTrigGateSour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTrigGateSour.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboTrigGateSour.Location = New System.Drawing.Point(16, 147)
        Me.cboTrigGateSour.Name = "cboTrigGateSour"
        Me.cboTrigGateSour.Size = New System.Drawing.Size(115, 21)
        Me.cboTrigGateSour.TabIndex = 62
        '
        'cboTrigSour
        '
        Me.cboTrigSour.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cboTrigSour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTrigSour.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboTrigSour.Location = New System.Drawing.Point(16, 40)
        Me.cboTrigSour.Name = "cboTrigSour"
        Me.cboTrigSour.Size = New System.Drawing.Size(115, 21)
        Me.cboTrigSour.TabIndex = 61
        '
        'chkTrigGateStat
        '
        Me.chkTrigGateStat.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTrigGateStat.Location = New System.Drawing.Point(42, 243)
        Me.chkTrigGateStat.Name = "chkTrigGateStat"
        Me.chkTrigGateStat.Size = New System.Drawing.Size(59, 22)
        Me.chkTrigGateStat.TabIndex = 65
        Me.chkTrigGateStat.Text = "On"
        '
        'cmdTrig
        '
        Me.cmdTrig.Location = New System.Drawing.Point(16, 278)
        Me.cmdTrig.Margin = New System.Windows.Forms.Padding(0)
        Me.cmdTrig.Name = "cmdTrig"
        Me.cmdTrig.Size = New System.Drawing.Size(115, 25)
        Me.cmdTrig.TabIndex = 0
        Me.cmdTrig.Text = "Start Immediate"
        Me.cmdTrig.UseVisualStyleBackColor = True
        '
        'lblTrigGateStat
        '
        Me.lblTrigGateStat.AutoSize = True
        Me.lblTrigGateStat.BackColor = System.Drawing.SystemColors.Control
        Me.lblTrigGateStat.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTrigGateStat.Location = New System.Drawing.Point(13, 225)
        Me.lblTrigGateStat.Name = "lblTrigGateStat"
        Me.lblTrigGateStat.Size = New System.Drawing.Size(94, 13)
        Me.lblTrigGateStat.TabIndex = 70
        Me.lblTrigGateStat.Text = "Trigger Gate State"
        Me.lblTrigGateStat.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblTrigGateSour
        '
        Me.lblTrigGateSour.AutoSize = True
        Me.lblTrigGateSour.BackColor = System.Drawing.SystemColors.Control
        Me.lblTrigGateSour.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.lblTrigGateSour.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTrigGateSour.Location = New System.Drawing.Point(5, 128)
        Me.lblTrigGateSour.Name = "lblTrigGateSour"
        Me.lblTrigGateSour.Size = New System.Drawing.Size(103, 13)
        Me.lblTrigGateSour.TabIndex = 69
        Me.lblTrigGateSour.Text = "Trigger Gate Source"
        Me.lblTrigGateSour.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblTrigGatePol
        '
        Me.lblTrigGatePol.AutoSize = True
        Me.lblTrigGatePol.BackColor = System.Drawing.SystemColors.Control
        Me.lblTrigGatePol.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTrigGatePol.Location = New System.Drawing.Point(8, 173)
        Me.lblTrigGatePol.Name = "lblTrigGatePol"
        Me.lblTrigGatePol.Size = New System.Drawing.Size(103, 13)
        Me.lblTrigGatePol.TabIndex = 68
        Me.lblTrigGatePol.Text = "Trigger Gate Polarity"
        Me.lblTrigGatePol.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'fraTrigStop
        '
        Me.fraTrigStop.Controls.Add(Me.cboTrigStopSlop)
        Me.fraTrigStop.Controls.Add(Me.cboTrigStopSour)
        Me.fraTrigStop.Controls.Add(Me.cmdTrigStop)
        Me.fraTrigStop.Controls.Add(Me.lblTrigStopSour)
        Me.fraTrigStop.Controls.Add(Me.lblTrigStopSlop)
        Me.fraTrigStop.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraTrigStop.Location = New System.Drawing.Point(338, 4)
        Me.fraTrigStop.Name = "fraTrigStop"
        Me.fraTrigStop.Size = New System.Drawing.Size(143, 165)
        Me.fraTrigStop.TabIndex = 72
        Me.fraTrigStop.TabStop = False
        Me.fraTrigStop.Text = "Trigger Stop"
        '
        'cboTrigStopSlop
        '
        Me.cboTrigStopSlop.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cboTrigStopSlop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTrigStopSlop.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboTrigStopSlop.Location = New System.Drawing.Point(14, 91)
        Me.cboTrigStopSlop.Name = "cboTrigStopSlop"
        Me.cboTrigStopSlop.Size = New System.Drawing.Size(115, 21)
        Me.cboTrigStopSlop.TabIndex = 74
        '
        'cboTrigStopSour
        '
        Me.cboTrigStopSour.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cboTrigStopSour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTrigStopSour.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboTrigStopSour.Location = New System.Drawing.Point(14, 40)
        Me.cboTrigStopSour.Name = "cboTrigStopSour"
        Me.cboTrigStopSour.Size = New System.Drawing.Size(115, 21)
        Me.cboTrigStopSour.TabIndex = 73
        '
        'cmdTrigStop
        '
        Me.cmdTrigStop.Location = New System.Drawing.Point(14, 122)
        Me.cmdTrigStop.Margin = New System.Windows.Forms.Padding(0)
        Me.cmdTrigStop.Name = "cmdTrigStop"
        Me.cmdTrigStop.Size = New System.Drawing.Size(115, 25)
        Me.cmdTrigStop.TabIndex = 75
        Me.cmdTrigStop.Text = "Stop Immediate"
        '
        'lblTrigStopSour
        '
        Me.lblTrigStopSour.AutoSize = True
        Me.lblTrigStopSour.BackColor = System.Drawing.SystemColors.Control
        Me.lblTrigStopSour.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTrigStopSour.Location = New System.Drawing.Point(3, 22)
        Me.lblTrigStopSour.Name = "lblTrigStopSour"
        Me.lblTrigStopSour.Size = New System.Drawing.Size(102, 13)
        Me.lblTrigStopSour.TabIndex = 76
        Me.lblTrigStopSour.Text = "Trigger Stop Source"
        Me.lblTrigStopSour.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblTrigStopSlop
        '
        Me.lblTrigStopSlop.AutoSize = True
        Me.lblTrigStopSlop.BackColor = System.Drawing.SystemColors.Control
        Me.lblTrigStopSlop.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTrigStopSlop.Location = New System.Drawing.Point(5, 72)
        Me.lblTrigStopSlop.Name = "lblTrigStopSlop"
        Me.lblTrigStopSlop.Size = New System.Drawing.Size(95, 13)
        Me.lblTrigStopSlop.TabIndex = 77
        Me.lblTrigStopSlop.Text = "Trigger Stop Slope"
        Me.lblTrigStopSlop.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'tabOptions_Page3
        '
        Me.tabOptions_Page3.Controls.Add(Me.fraMarkEclt0)
        Me.tabOptions_Page3.Controls.Add(Me.fraMarkEclt1)
        Me.tabOptions_Page3.Controls.Add(Me.fraMarkExt)
        Me.tabOptions_Page3.Controls.Add(Me.fraMarkSpo)
        Me.tabOptions_Page3.Location = New System.Drawing.Point(4, 22)
        Me.tabOptions_Page3.Name = "tabOptions_Page3"
        Me.tabOptions_Page3.Size = New System.Drawing.Size(723, 346)
        Me.tabOptions_Page3.TabIndex = 2
        Me.tabOptions_Page3.Text = "Markers"
        '
        'fraMarkEclt0
        '
        Me.fraMarkEclt0.Controls.Add(Me.cboMarkEclt0Feed)
        Me.fraMarkEclt0.Controls.Add(Me.chkMarkEclt0)
        Me.fraMarkEclt0.Controls.Add(Me.lblMarkEclt0Feed)
        Me.fraMarkEclt0.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraMarkEclt0.Location = New System.Drawing.Point(8, 4)
        Me.fraMarkEclt0.Name = "fraMarkEclt0"
        Me.fraMarkEclt0.Size = New System.Drawing.Size(186, 75)
        Me.fraMarkEclt0.TabIndex = 84
        Me.fraMarkEclt0.TabStop = False
        Me.fraMarkEclt0.Text = "ECL Trigger 0"
        '
        'cboMarkEclt0Feed
        '
        Me.cboMarkEclt0Feed.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cboMarkEclt0Feed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMarkEclt0Feed.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboMarkEclt0Feed.Location = New System.Drawing.Point(8, 42)
        Me.cboMarkEclt0Feed.Name = "cboMarkEclt0Feed"
        Me.cboMarkEclt0Feed.Size = New System.Drawing.Size(108, 21)
        Me.cboMarkEclt0Feed.TabIndex = 85
        '
        'chkMarkEclt0
        '
        Me.chkMarkEclt0.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMarkEclt0.Location = New System.Drawing.Point(124, 43)
        Me.chkMarkEclt0.Name = "chkMarkEclt0"
        Me.chkMarkEclt0.Size = New System.Drawing.Size(54, 22)
        Me.chkMarkEclt0.TabIndex = 86
        Me.chkMarkEclt0.Text = "On"
        '
        'lblMarkEclt0Feed
        '
        Me.lblMarkEclt0Feed.AutoSize = True
        Me.lblMarkEclt0Feed.BackColor = System.Drawing.SystemColors.Control
        Me.lblMarkEclt0Feed.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMarkEclt0Feed.Location = New System.Drawing.Point(19, 22)
        Me.lblMarkEclt0Feed.Name = "lblMarkEclt0Feed"
        Me.lblMarkEclt0Feed.Size = New System.Drawing.Size(68, 13)
        Me.lblMarkEclt0Feed.TabIndex = 87
        Me.lblMarkEclt0Feed.Text = "Feed Source"
        Me.lblMarkEclt0Feed.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'fraMarkEclt1
        '
        Me.fraMarkEclt1.Controls.Add(Me.cboMarkEclt1Feed)
        Me.fraMarkEclt1.Controls.Add(Me.chkMarkEclt1)
        Me.fraMarkEclt1.Controls.Add(Me.lblMarkEclt1Feed)
        Me.fraMarkEclt1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraMarkEclt1.Location = New System.Drawing.Point(203, 4)
        Me.fraMarkEclt1.Name = "fraMarkEclt1"
        Me.fraMarkEclt1.Size = New System.Drawing.Size(186, 75)
        Me.fraMarkEclt1.TabIndex = 88
        Me.fraMarkEclt1.TabStop = False
        Me.fraMarkEclt1.Text = "ECL Trigger 1"
        '
        'cboMarkEclt1Feed
        '
        Me.cboMarkEclt1Feed.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cboMarkEclt1Feed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMarkEclt1Feed.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboMarkEclt1Feed.Location = New System.Drawing.Point(8, 42)
        Me.cboMarkEclt1Feed.Name = "cboMarkEclt1Feed"
        Me.cboMarkEclt1Feed.Size = New System.Drawing.Size(108, 21)
        Me.cboMarkEclt1Feed.TabIndex = 89
        '
        'chkMarkEclt1
        '
        Me.chkMarkEclt1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMarkEclt1.Location = New System.Drawing.Point(130, 43)
        Me.chkMarkEclt1.Name = "chkMarkEclt1"
        Me.chkMarkEclt1.Size = New System.Drawing.Size(49, 22)
        Me.chkMarkEclt1.TabIndex = 90
        Me.chkMarkEclt1.Text = "On"
        '
        'lblMarkEclt1Feed
        '
        Me.lblMarkEclt1Feed.AutoSize = True
        Me.lblMarkEclt1Feed.BackColor = System.Drawing.SystemColors.Control
        Me.lblMarkEclt1Feed.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMarkEclt1Feed.Location = New System.Drawing.Point(18, 22)
        Me.lblMarkEclt1Feed.Name = "lblMarkEclt1Feed"
        Me.lblMarkEclt1Feed.Size = New System.Drawing.Size(68, 13)
        Me.lblMarkEclt1Feed.TabIndex = 91
        Me.lblMarkEclt1Feed.Text = "Feed Source"
        Me.lblMarkEclt1Feed.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'fraMarkExt
        '
        Me.fraMarkExt.Controls.Add(Me.cboMarkFeed)
        Me.fraMarkExt.Controls.Add(Me.cboMarkPol)
        Me.fraMarkExt.Controls.Add(Me.chkMark)
        Me.fraMarkExt.Controls.Add(Me.lblMarkFeed)
        Me.fraMarkExt.Controls.Add(Me.lblMarkPol)
        Me.fraMarkExt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraMarkExt.Location = New System.Drawing.Point(8, 85)
        Me.fraMarkExt.Name = "fraMarkExt"
        Me.fraMarkExt.Size = New System.Drawing.Size(186, 127)
        Me.fraMarkExt.TabIndex = 78
        Me.fraMarkExt.TabStop = False
        Me.fraMarkExt.Text = "External Marker Out"
        '
        'cboMarkFeed
        '
        Me.cboMarkFeed.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cboMarkFeed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMarkFeed.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboMarkFeed.Location = New System.Drawing.Point(8, 41)
        Me.cboMarkFeed.Name = "cboMarkFeed"
        Me.cboMarkFeed.Size = New System.Drawing.Size(108, 21)
        Me.cboMarkFeed.TabIndex = 80
        '
        'cboMarkPol
        '
        Me.cboMarkPol.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cboMarkPol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMarkPol.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboMarkPol.Location = New System.Drawing.Point(8, 90)
        Me.cboMarkPol.Name = "cboMarkPol"
        Me.cboMarkPol.Size = New System.Drawing.Size(68, 21)
        Me.cboMarkPol.TabIndex = 79
        '
        'chkMark
        '
        Me.chkMark.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMark.Location = New System.Drawing.Point(124, 42)
        Me.chkMark.Name = "chkMark"
        Me.chkMark.Size = New System.Drawing.Size(54, 22)
        Me.chkMark.TabIndex = 81
        Me.chkMark.Text = "On"
        '
        'lblMarkFeed
        '
        Me.lblMarkFeed.AutoSize = True
        Me.lblMarkFeed.BackColor = System.Drawing.SystemColors.Control
        Me.lblMarkFeed.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMarkFeed.Location = New System.Drawing.Point(19, 21)
        Me.lblMarkFeed.Name = "lblMarkFeed"
        Me.lblMarkFeed.Size = New System.Drawing.Size(68, 13)
        Me.lblMarkFeed.TabIndex = 83
        Me.lblMarkFeed.Text = "Feed Source"
        Me.lblMarkFeed.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblMarkPol
        '
        Me.lblMarkPol.AutoSize = True
        Me.lblMarkPol.BackColor = System.Drawing.SystemColors.Control
        Me.lblMarkPol.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMarkPol.Location = New System.Drawing.Point(16, 73)
        Me.lblMarkPol.Name = "lblMarkPol"
        Me.lblMarkPol.Size = New System.Drawing.Size(41, 13)
        Me.lblMarkPol.TabIndex = 82
        Me.lblMarkPol.Text = "Polarity"
        Me.lblMarkPol.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'fraMarkSpo
        '
        Me.fraMarkSpo.Controls.Add(Me.panMarkSpo)
        Me.fraMarkSpo.Controls.Add(Me.cmdMarkDelete)
        Me.fraMarkSpo.Controls.Add(Me.Label4)
        Me.fraMarkSpo.Controls.Add(Me.lblListSel)
        Me.fraMarkSpo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraMarkSpo.Location = New System.Drawing.Point(203, 85)
        Me.fraMarkSpo.Name = "fraMarkSpo"
        Me.fraMarkSpo.Size = New System.Drawing.Size(186, 127)
        Me.fraMarkSpo.TabIndex = 33
        Me.fraMarkSpo.TabStop = False
        Me.fraMarkSpo.Text = "Waveform Point"
        '
        'panMarkSpo
        '
        Me.panMarkSpo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.panMarkSpo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panMarkSpo.Controls.Add(Me.txtMarkSpo)
        Me.panMarkSpo.Controls.Add(Me.spnMarkSpo)
        Me.panMarkSpo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panMarkSpo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.panMarkSpo.Location = New System.Drawing.Point(24, 61)
        Me.panMarkSpo.Name = "panMarkSpo"
        Me.panMarkSpo.Size = New System.Drawing.Size(106, 21)
        Me.panMarkSpo.TabIndex = 34
        '
        'txtMarkSpo
        '
        Me.txtMarkSpo.BackColor = System.Drawing.SystemColors.Window
        Me.txtMarkSpo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtMarkSpo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMarkSpo.Location = New System.Drawing.Point(2, 2)
        Me.txtMarkSpo.Name = "txtMarkSpo"
        Me.txtMarkSpo.Size = New System.Drawing.Size(83, 13)
        Me.txtMarkSpo.TabIndex = 35
        '
        'spnMarkSpo
        '
        Me.spnMarkSpo.Location = New System.Drawing.Point(88, -2)
        Me.spnMarkSpo.Name = "spnMarkSpo"
        Me.spnMarkSpo.Size = New System.Drawing.Size(16, 20)
        Me.spnMarkSpo.TabIndex = 36
        '
        'cmdMarkDelete
        '
        Me.cmdMarkDelete.Location = New System.Drawing.Point(40, 91)
        Me.cmdMarkDelete.Name = "cmdMarkDelete"
        Me.cmdMarkDelete.Size = New System.Drawing.Size(76, 23)
        Me.cmdMarkDelete.TabIndex = 37
        Me.cmdMarkDelete.Text = "Delete Mark"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(21, 18)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(123, 16)
        Me.Label4.TabIndex = 39
        Me.Label4.Text = "Selected Segment"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblListSel
        '
        Me.lblListSel.BackColor = System.Drawing.SystemColors.Control
        Me.lblListSel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblListSel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblListSel.Location = New System.Drawing.Point(24, 36)
        Me.lblListSel.Name = "lblListSel"
        Me.lblListSel.Size = New System.Drawing.Size(106, 17)
        Me.lblListSel.TabIndex = 38
        '
        'tabOptions_Page4
        '
        Me.tabOptions_Page4.Controls.Add(Me.fraFreqMode)
        Me.tabOptions_Page4.Controls.Add(Me.fraFreqModeSwe)
        Me.tabOptions_Page4.Controls.Add(Me.fraAgileFsk)
        Me.tabOptions_Page4.Location = New System.Drawing.Point(4, 22)
        Me.tabOptions_Page4.Name = "tabOptions_Page4"
        Me.tabOptions_Page4.Size = New System.Drawing.Size(723, 346)
        Me.tabOptions_Page4.TabIndex = 3
        Me.tabOptions_Page4.Text = "Freq. Agility"
        '
        'fraFreqMode
        '
        Me.fraFreqMode.Controls.Add(Me.optFreqModeFix)
        Me.fraFreqMode.Controls.Add(Me.optFreqModeSwe)
        Me.fraFreqMode.Controls.Add(Me.optFreqModeFsk)
        Me.fraFreqMode.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraFreqMode.Location = New System.Drawing.Point(8, 4)
        Me.fraFreqMode.Name = "fraFreqMode"
        Me.fraFreqMode.Size = New System.Drawing.Size(408, 48)
        Me.fraFreqMode.TabIndex = 94
        Me.fraFreqMode.TabStop = False
        Me.fraFreqMode.Text = "Frequency Mode"
        '
        'optFreqModeFix
        '
        Me.optFreqModeFix.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optFreqModeFix.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optFreqModeFix.Location = New System.Drawing.Point(12, 15)
        Me.optFreqModeFix.Name = "optFreqModeFix"
        Me.optFreqModeFix.Size = New System.Drawing.Size(51, 23)
        Me.optFreqModeFix.TabIndex = 95
        Me.optFreqModeFix.Text = "Fixed"
        '
        'optFreqModeSwe
        '
        Me.optFreqModeSwe.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optFreqModeSwe.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optFreqModeSwe.Location = New System.Drawing.Point(115, 15)
        Me.optFreqModeSwe.Name = "optFreqModeSwe"
        Me.optFreqModeSwe.Size = New System.Drawing.Size(73, 23)
        Me.optFreqModeSwe.TabIndex = 97
        Me.optFreqModeSwe.Text = "Sweep"
        '
        'optFreqModeFsk
        '
        Me.optFreqModeFsk.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optFreqModeFsk.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optFreqModeFsk.Location = New System.Drawing.Point(221, 15)
        Me.optFreqModeFsk.Name = "optFreqModeFsk"
        Me.optFreqModeFsk.Size = New System.Drawing.Size(185, 23)
        Me.optFreqModeFsk.TabIndex = 96
        Me.optFreqModeFsk.Text = "FSK (Freq. Shift Keying)"
        '
        'fraFreqModeSwe
        '
        Me.fraFreqModeSwe.Controls.Add(Me.cboAgileSweepSpac)
        Me.fraFreqModeSwe.Controls.Add(Me.cboAgileSweepDir)
        Me.fraFreqModeSwe.Controls.Add(Me.cboAgileSweepTrigSour)
        Me.fraFreqModeSwe.Controls.Add(Me.cboAgileSweepAdvSour)
        Me.fraFreqModeSwe.Controls.Add(Me.panAgileSweepStart)
        Me.fraFreqModeSwe.Controls.Add(Me.panAgileSweepStop)
        Me.fraFreqModeSwe.Controls.Add(Me.panAgileSweepSteps)
        Me.fraFreqModeSwe.Controls.Add(Me.panAgileSweepDur)
        Me.fraFreqModeSwe.Controls.Add(Me.panSweCoun)
        Me.fraFreqModeSwe.Controls.Add(Me.cmdSweCounInf)
        Me.fraFreqModeSwe.Controls.Add(Me.lblSweCoun)
        Me.fraFreqModeSwe.Controls.Add(Me.lblAgileSweepDur)
        Me.fraFreqModeSwe.Controls.Add(Me.lblAgileSweepSpac)
        Me.fraFreqModeSwe.Controls.Add(Me.lblAgileSweepDir)
        Me.fraFreqModeSwe.Controls.Add(Me.lblAgileSweepSteps)
        Me.fraFreqModeSwe.Controls.Add(Me.lblAgileSweepStop)
        Me.fraFreqModeSwe.Controls.Add(Me.lblAgileSweepTrigSour)
        Me.fraFreqModeSwe.Controls.Add(Me.lblAgileSweepStart)
        Me.fraFreqModeSwe.Controls.Add(Me.lblAgileSweepAdvSour)
        Me.fraFreqModeSwe.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraFreqModeSwe.Location = New System.Drawing.Point(8, 57)
        Me.fraFreqModeSwe.Name = "fraFreqModeSwe"
        Me.fraFreqModeSwe.Size = New System.Drawing.Size(265, 264)
        Me.fraFreqModeSwe.TabIndex = 98
        Me.fraFreqModeSwe.TabStop = False
        Me.fraFreqModeSwe.Text = "Sweep Parameters"
        '
        'cboAgileSweepSpac
        '
        Me.cboAgileSweepSpac.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cboAgileSweepSpac.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboAgileSweepSpac.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboAgileSweepSpac.Location = New System.Drawing.Point(135, 130)
        Me.cboAgileSweepSpac.Name = "cboAgileSweepSpac"
        Me.cboAgileSweepSpac.Size = New System.Drawing.Size(116, 21)
        Me.cboAgileSweepSpac.TabIndex = 102
        '
        'cboAgileSweepDir
        '
        Me.cboAgileSweepDir.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cboAgileSweepDir.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboAgileSweepDir.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboAgileSweepDir.Location = New System.Drawing.Point(156, 81)
        Me.cboAgileSweepDir.Name = "cboAgileSweepDir"
        Me.cboAgileSweepDir.Size = New System.Drawing.Size(95, 21)
        Me.cboAgileSweepDir.TabIndex = 101
        '
        'cboAgileSweepTrigSour
        '
        Me.cboAgileSweepTrigSour.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cboAgileSweepTrigSour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboAgileSweepTrigSour.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboAgileSweepTrigSour.Location = New System.Drawing.Point(5, 174)
        Me.cboAgileSweepTrigSour.Name = "cboAgileSweepTrigSour"
        Me.cboAgileSweepTrigSour.Size = New System.Drawing.Size(116, 21)
        Me.cboAgileSweepTrigSour.TabIndex = 100
        '
        'cboAgileSweepAdvSour
        '
        Me.cboAgileSweepAdvSour.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cboAgileSweepAdvSour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboAgileSweepAdvSour.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboAgileSweepAdvSour.Location = New System.Drawing.Point(135, 174)
        Me.cboAgileSweepAdvSour.Name = "cboAgileSweepAdvSour"
        Me.cboAgileSweepAdvSour.Size = New System.Drawing.Size(116, 21)
        Me.cboAgileSweepAdvSour.TabIndex = 99
        '
        'panAgileSweepStart
        '
        Me.panAgileSweepStart.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.panAgileSweepStart.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panAgileSweepStart.Controls.Add(Me.txtAgileSweepStart)
        Me.panAgileSweepStart.Controls.Add(Me.spnAgileSweepStart)
        Me.panAgileSweepStart.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panAgileSweepStart.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.panAgileSweepStart.Location = New System.Drawing.Point(8, 42)
        Me.panAgileSweepStart.Name = "panAgileSweepStart"
        Me.panAgileSweepStart.Size = New System.Drawing.Size(113, 21)
        Me.panAgileSweepStart.TabIndex = 103
        '
        'txtAgileSweepStart
        '
        Me.txtAgileSweepStart.BackColor = System.Drawing.SystemColors.Window
        Me.txtAgileSweepStart.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtAgileSweepStart.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAgileSweepStart.Location = New System.Drawing.Point(2, 2)
        Me.txtAgileSweepStart.Name = "txtAgileSweepStart"
        Me.txtAgileSweepStart.Size = New System.Drawing.Size(90, 13)
        Me.txtAgileSweepStart.TabIndex = 104
        '
        'spnAgileSweepStart
        '
        Me.spnAgileSweepStart.Location = New System.Drawing.Point(95, -2)
        Me.spnAgileSweepStart.Name = "spnAgileSweepStart"
        Me.spnAgileSweepStart.Size = New System.Drawing.Size(16, 20)
        Me.spnAgileSweepStart.TabIndex = 105
        '
        'panAgileSweepStop
        '
        Me.panAgileSweepStop.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.panAgileSweepStop.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panAgileSweepStop.Controls.Add(Me.txtAgileSweepStop)
        Me.panAgileSweepStop.Controls.Add(Me.spnAgileSweepStop)
        Me.panAgileSweepStop.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panAgileSweepStop.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.panAgileSweepStop.Location = New System.Drawing.Point(136, 42)
        Me.panAgileSweepStop.Name = "panAgileSweepStop"
        Me.panAgileSweepStop.Size = New System.Drawing.Size(92, 21)
        Me.panAgileSweepStop.TabIndex = 106
        '
        'txtAgileSweepStop
        '
        Me.txtAgileSweepStop.BackColor = System.Drawing.SystemColors.Window
        Me.txtAgileSweepStop.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtAgileSweepStop.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAgileSweepStop.Location = New System.Drawing.Point(2, 2)
        Me.txtAgileSweepStop.Name = "txtAgileSweepStop"
        Me.txtAgileSweepStop.Size = New System.Drawing.Size(69, 13)
        Me.txtAgileSweepStop.TabIndex = 107
        '
        'spnAgileSweepStop
        '
        Me.spnAgileSweepStop.Location = New System.Drawing.Point(74, -1)
        Me.spnAgileSweepStop.Name = "spnAgileSweepStop"
        Me.spnAgileSweepStop.Size = New System.Drawing.Size(16, 20)
        Me.spnAgileSweepStop.TabIndex = 108
        '
        'panAgileSweepSteps
        '
        Me.panAgileSweepSteps.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.panAgileSweepSteps.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panAgileSweepSteps.Controls.Add(Me.txtAgileSweepSteps)
        Me.panAgileSweepSteps.Controls.Add(Me.spnSweCoun)
        Me.panAgileSweepSteps.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panAgileSweepSteps.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.panAgileSweepSteps.Location = New System.Drawing.Point(8, 131)
        Me.panAgileSweepSteps.Name = "panAgileSweepSteps"
        Me.panAgileSweepSteps.Size = New System.Drawing.Size(113, 21)
        Me.panAgileSweepSteps.TabIndex = 109
        '
        'txtAgileSweepSteps
        '
        Me.txtAgileSweepSteps.BackColor = System.Drawing.SystemColors.Window
        Me.txtAgileSweepSteps.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtAgileSweepSteps.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAgileSweepSteps.Location = New System.Drawing.Point(2, 2)
        Me.txtAgileSweepSteps.Name = "txtAgileSweepSteps"
        Me.txtAgileSweepSteps.Size = New System.Drawing.Size(90, 13)
        Me.txtAgileSweepSteps.TabIndex = 110
        '
        'spnSweCoun
        '
        Me.spnSweCoun.Location = New System.Drawing.Point(95, -2)
        Me.spnSweCoun.Name = "spnSweCoun"
        Me.spnSweCoun.Size = New System.Drawing.Size(16, 20)
        Me.spnSweCoun.TabIndex = 117
        '
        'panAgileSweepDur
        '
        Me.panAgileSweepDur.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.panAgileSweepDur.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panAgileSweepDur.Controls.Add(Me.txtAgileSweepDur)
        Me.panAgileSweepDur.Controls.Add(Me.spnAgileSweepDur)
        Me.panAgileSweepDur.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panAgileSweepDur.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.panAgileSweepDur.Location = New System.Drawing.Point(8, 81)
        Me.panAgileSweepDur.Name = "panAgileSweepDur"
        Me.panAgileSweepDur.Size = New System.Drawing.Size(132, 21)
        Me.panAgileSweepDur.TabIndex = 112
        '
        'txtAgileSweepDur
        '
        Me.txtAgileSweepDur.BackColor = System.Drawing.SystemColors.Window
        Me.txtAgileSweepDur.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtAgileSweepDur.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAgileSweepDur.Location = New System.Drawing.Point(2, 2)
        Me.txtAgileSweepDur.Name = "txtAgileSweepDur"
        Me.txtAgileSweepDur.Size = New System.Drawing.Size(109, 13)
        Me.txtAgileSweepDur.TabIndex = 113
        '
        'spnAgileSweepDur
        '
        Me.spnAgileSweepDur.Location = New System.Drawing.Point(114, -2)
        Me.spnAgileSweepDur.Name = "spnAgileSweepDur"
        Me.spnAgileSweepDur.Size = New System.Drawing.Size(16, 20)
        Me.spnAgileSweepDur.TabIndex = 114
        '
        'panSweCoun
        '
        Me.panSweCoun.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.panSweCoun.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panSweCoun.Controls.Add(Me.txtSweCoun)
        Me.panSweCoun.Controls.Add(Me.spnAgileSweepSteps)
        Me.panSweCoun.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panSweCoun.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.panSweCoun.Location = New System.Drawing.Point(5, 228)
        Me.panSweCoun.Name = "panSweCoun"
        Me.panSweCoun.Size = New System.Drawing.Size(116, 21)
        Me.panSweCoun.TabIndex = 115
        '
        'txtSweCoun
        '
        Me.txtSweCoun.BackColor = System.Drawing.SystemColors.Window
        Me.txtSweCoun.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSweCoun.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSweCoun.Location = New System.Drawing.Point(2, 2)
        Me.txtSweCoun.Name = "txtSweCoun"
        Me.txtSweCoun.Size = New System.Drawing.Size(93, 13)
        Me.txtSweCoun.TabIndex = 116
        '
        'spnAgileSweepSteps
        '
        Me.spnAgileSweepSteps.Location = New System.Drawing.Point(98, -2)
        Me.spnAgileSweepSteps.Name = "spnAgileSweepSteps"
        Me.spnAgileSweepSteps.Size = New System.Drawing.Size(16, 20)
        Me.spnAgileSweepSteps.TabIndex = 111
        '
        'cmdSweCounInf
        '
        Me.cmdSweCounInf.Location = New System.Drawing.Point(141, 226)
        Me.cmdSweCounInf.Name = "cmdSweCounInf"
        Me.cmdSweCounInf.Size = New System.Drawing.Size(110, 24)
        Me.cmdSweCounInf.TabIndex = 118
        Me.cmdSweCounInf.Text = "Set to Infinity"
        '
        'lblSweCoun
        '
        Me.lblSweCoun.BackColor = System.Drawing.SystemColors.Control
        Me.lblSweCoun.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSweCoun.Location = New System.Drawing.Point(67, 206)
        Me.lblSweCoun.Name = "lblSweCoun"
        Me.lblSweCoun.Size = New System.Drawing.Size(102, 19)
        Me.lblSweCoun.TabIndex = 127
        Me.lblSweCoun.Text = "Sweep Count"
        Me.lblSweCoun.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblAgileSweepDur
        '
        Me.lblAgileSweepDur.BackColor = System.Drawing.SystemColors.Control
        Me.lblAgileSweepDur.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAgileSweepDur.Location = New System.Drawing.Point(8, 65)
        Me.lblAgileSweepDur.Name = "lblAgileSweepDur"
        Me.lblAgileSweepDur.Size = New System.Drawing.Size(130, 16)
        Me.lblAgileSweepDur.TabIndex = 126
        Me.lblAgileSweepDur.Text = "Sweep Duration"
        Me.lblAgileSweepDur.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblAgileSweepSpac
        '
        Me.lblAgileSweepSpac.BackColor = System.Drawing.SystemColors.Control
        Me.lblAgileSweepSpac.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAgileSweepSpac.Location = New System.Drawing.Point(143, 111)
        Me.lblAgileSweepSpac.Name = "lblAgileSweepSpac"
        Me.lblAgileSweepSpac.Size = New System.Drawing.Size(98, 19)
        Me.lblAgileSweepSpac.TabIndex = 125
        Me.lblAgileSweepSpac.Text = "Step Spacing"
        Me.lblAgileSweepSpac.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblAgileSweepDir
        '
        Me.lblAgileSweepDir.BackColor = System.Drawing.SystemColors.Control
        Me.lblAgileSweepDir.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAgileSweepDir.Location = New System.Drawing.Point(146, 65)
        Me.lblAgileSweepDir.Name = "lblAgileSweepDir"
        Me.lblAgileSweepDir.Size = New System.Drawing.Size(82, 16)
        Me.lblAgileSweepDir.TabIndex = 124
        Me.lblAgileSweepDir.Text = "Direction"
        Me.lblAgileSweepDir.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblAgileSweepSteps
        '
        Me.lblAgileSweepSteps.BackColor = System.Drawing.SystemColors.Control
        Me.lblAgileSweepSteps.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAgileSweepSteps.Location = New System.Drawing.Point(-4, 110)
        Me.lblAgileSweepSteps.Name = "lblAgileSweepSteps"
        Me.lblAgileSweepSteps.Size = New System.Drawing.Size(130, 19)
        Me.lblAgileSweepSteps.TabIndex = 123
        Me.lblAgileSweepSteps.Text = "Number of Steps"
        Me.lblAgileSweepSteps.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblAgileSweepStop
        '
        Me.lblAgileSweepStop.BackColor = System.Drawing.SystemColors.Control
        Me.lblAgileSweepStop.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAgileSweepStop.Location = New System.Drawing.Point(162, 24)
        Me.lblAgileSweepStop.Name = "lblAgileSweepStop"
        Me.lblAgileSweepStop.Size = New System.Drawing.Size(61, 19)
        Me.lblAgileSweepStop.TabIndex = 122
        Me.lblAgileSweepStop.Text = "Stop Frequency"
        Me.lblAgileSweepStop.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblAgileSweepTrigSour
        '
        Me.lblAgileSweepTrigSour.BackColor = System.Drawing.SystemColors.Control
        Me.lblAgileSweepTrigSour.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAgileSweepTrigSour.Location = New System.Drawing.Point(-2, 156)
        Me.lblAgileSweepTrigSour.Name = "lblAgileSweepTrigSour"
        Me.lblAgileSweepTrigSour.Size = New System.Drawing.Size(129, 19)
        Me.lblAgileSweepTrigSour.TabIndex = 121
        Me.lblAgileSweepTrigSour.Text = "Sweep Arm Source"
        Me.lblAgileSweepTrigSour.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblAgileSweepStart
        '
        Me.lblAgileSweepStart.BackColor = System.Drawing.SystemColors.Control
        Me.lblAgileSweepStart.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAgileSweepStart.Location = New System.Drawing.Point(11, 24)
        Me.lblAgileSweepStart.Name = "lblAgileSweepStart"
        Me.lblAgileSweepStart.Size = New System.Drawing.Size(89, 19)
        Me.lblAgileSweepStart.TabIndex = 120
        Me.lblAgileSweepStart.Text = "Start Frequency"
        Me.lblAgileSweepStart.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblAgileSweepAdvSour
        '
        Me.lblAgileSweepAdvSour.BackColor = System.Drawing.SystemColors.Control
        Me.lblAgileSweepAdvSour.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAgileSweepAdvSour.Location = New System.Drawing.Point(131, 155)
        Me.lblAgileSweepAdvSour.Name = "lblAgileSweepAdvSour"
        Me.lblAgileSweepAdvSour.Size = New System.Drawing.Size(121, 19)
        Me.lblAgileSweepAdvSour.TabIndex = 119
        Me.lblAgileSweepAdvSour.Text = "Step Adv. Source"
        Me.lblAgileSweepAdvSour.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'fraAgileFsk
        '
        Me.fraAgileFsk.Controls.Add(Me.cboAgileFskSour)
        Me.fraAgileFsk.Controls.Add(Me.lblAgileFskSour)
        Me.fraAgileFsk.Controls.Add(Me.lblAgileFskKeyH)
        Me.fraAgileFsk.Controls.Add(Me.panAgileFskKeyH)
        Me.fraAgileFsk.Controls.Add(Me.lblAgileFskKeyL)
        Me.fraAgileFsk.Controls.Add(Me.panAgileFskKeyL)
        Me.fraAgileFsk.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraAgileFsk.Location = New System.Drawing.Point(279, 66)
        Me.fraAgileFsk.Name = "fraAgileFsk"
        Me.fraAgileFsk.Size = New System.Drawing.Size(179, 166)
        Me.fraAgileFsk.TabIndex = 128
        Me.fraAgileFsk.TabStop = False
        Me.fraAgileFsk.Text = "FSK Parameters"
        '
        'cboAgileFskSour
        '
        Me.cboAgileFskSour.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cboAgileFskSour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboAgileFskSour.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboAgileFskSour.Location = New System.Drawing.Point(16, 40)
        Me.cboAgileFskSour.Name = "cboAgileFskSour"
        Me.cboAgileFskSour.Size = New System.Drawing.Size(121, 21)
        Me.cboAgileFskSour.TabIndex = 129
        '
        'lblAgileFskSour
        '
        Me.lblAgileFskSour.BackColor = System.Drawing.SystemColors.Control
        Me.lblAgileFskSour.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAgileFskSour.Location = New System.Drawing.Point(27, 22)
        Me.lblAgileFskSour.Name = "lblAgileFskSour"
        Me.lblAgileFskSour.Size = New System.Drawing.Size(91, 19)
        Me.lblAgileFskSour.TabIndex = 137
        Me.lblAgileFskSour.Text = "Source"
        Me.lblAgileFskSour.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblAgileFskKeyH
        '
        Me.lblAgileFskKeyH.BackColor = System.Drawing.SystemColors.Control
        Me.lblAgileFskKeyH.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAgileFskKeyH.Location = New System.Drawing.Point(27, 65)
        Me.lblAgileFskKeyH.Name = "lblAgileFskKeyH"
        Me.lblAgileFskKeyH.Size = New System.Drawing.Size(91, 19)
        Me.lblAgileFskKeyH.TabIndex = 136
        Me.lblAgileFskKeyH.Text = "Key High Freq."
        Me.lblAgileFskKeyH.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'panAgileFskKeyH
        '
        Me.panAgileFskKeyH.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.panAgileFskKeyH.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panAgileFskKeyH.Controls.Add(Me.txtAgileFskKeyH)
        Me.panAgileFskKeyH.Controls.Add(Me.spnAgileFskKeyH)
        Me.panAgileFskKeyH.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panAgileFskKeyH.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.panAgileFskKeyH.Location = New System.Drawing.Point(11, 85)
        Me.panAgileFskKeyH.Name = "panAgileFskKeyH"
        Me.panAgileFskKeyH.Size = New System.Drawing.Size(142, 21)
        Me.panAgileFskKeyH.TabIndex = 130
        '
        'txtAgileFskKeyH
        '
        Me.txtAgileFskKeyH.BackColor = System.Drawing.SystemColors.Window
        Me.txtAgileFskKeyH.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtAgileFskKeyH.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAgileFskKeyH.Location = New System.Drawing.Point(2, 2)
        Me.txtAgileFskKeyH.Name = "txtAgileFskKeyH"
        Me.txtAgileFskKeyH.Size = New System.Drawing.Size(119, 13)
        Me.txtAgileFskKeyH.TabIndex = 131
        '
        'spnAgileFskKeyH
        '
        Me.spnAgileFskKeyH.Location = New System.Drawing.Point(124, -2)
        Me.spnAgileFskKeyH.Name = "spnAgileFskKeyH"
        Me.spnAgileFskKeyH.Size = New System.Drawing.Size(16, 20)
        Me.spnAgileFskKeyH.TabIndex = 132
        '
        'lblAgileFskKeyL
        '
        Me.lblAgileFskKeyL.BackColor = System.Drawing.SystemColors.Control
        Me.lblAgileFskKeyL.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.lblAgileFskKeyL.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAgileFskKeyL.Location = New System.Drawing.Point(26, 111)
        Me.lblAgileFskKeyL.Name = "lblAgileFskKeyL"
        Me.lblAgileFskKeyL.Size = New System.Drawing.Size(91, 15)
        Me.lblAgileFskKeyL.TabIndex = 138
        Me.lblAgileFskKeyL.Text = "Key Low Freq."
        Me.lblAgileFskKeyL.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'panAgileFskKeyL
        '
        Me.panAgileFskKeyL.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.panAgileFskKeyL.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panAgileFskKeyL.Controls.Add(Me.txtAgileFskKeyL)
        Me.panAgileFskKeyL.Controls.Add(Me.spnAgileFskKeyL)
        Me.panAgileFskKeyL.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panAgileFskKeyL.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.panAgileFskKeyL.Location = New System.Drawing.Point(11, 129)
        Me.panAgileFskKeyL.Name = "panAgileFskKeyL"
        Me.panAgileFskKeyL.Size = New System.Drawing.Size(142, 21)
        Me.panAgileFskKeyL.TabIndex = 133
        '
        'txtAgileFskKeyL
        '
        Me.txtAgileFskKeyL.BackColor = System.Drawing.SystemColors.Window
        Me.txtAgileFskKeyL.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtAgileFskKeyL.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAgileFskKeyL.Location = New System.Drawing.Point(2, 2)
        Me.txtAgileFskKeyL.Name = "txtAgileFskKeyL"
        Me.txtAgileFskKeyL.Size = New System.Drawing.Size(119, 13)
        Me.txtAgileFskKeyL.TabIndex = 134
        '
        'spnAgileFskKeyL
        '
        Me.spnAgileFskKeyL.Location = New System.Drawing.Point(124, -1)
        Me.spnAgileFskKeyL.Name = "spnAgileFskKeyL"
        Me.spnAgileFskKeyL.Size = New System.Drawing.Size(16, 20)
        Me.spnAgileFskKeyL.TabIndex = 135
        '
        'tabOptions_Page5
        '
        Me.tabOptions_Page5.Controls.Add(Me.lblUserName)
        Me.tabOptions_Page5.Controls.Add(Me.lblUserPoints)
        Me.tabOptions_Page5.Controls.Add(Me.lblSampleFreq)
        Me.tabOptions_Page5.Controls.Add(Me.prgDownLoad)
        Me.tabOptions_Page5.Controls.Add(Me.G1)
        Me.tabOptions_Page5.Controls.Add(Me.cmdZoomOut)
        Me.tabOptions_Page5.Controls.Add(Me.cmdPan)
        Me.tabOptions_Page5.Controls.Add(Me.cmdSnap)
        Me.tabOptions_Page5.Controls.Add(Me.cmdUserClear)
        Me.tabOptions_Page5.Controls.Add(Me.cmdUserSine)
        Me.tabOptions_Page5.Controls.Add(Me.cmdUserSquare)
        Me.tabOptions_Page5.Controls.Add(Me.cmdUserRamp)
        Me.tabOptions_Page5.Controls.Add(Me.cmdUserPm)
        Me.tabOptions_Page5.Controls.Add(Me.cmdZoom)
        Me.tabOptions_Page5.Controls.Add(Me.cmdUserUndo)
        Me.tabOptions_Page5.Controls.Add(Me.cmdDownLoad)
        Me.tabOptions_Page5.Controls.Add(Me.cmdUserLoad)
        Me.tabOptions_Page5.Controls.Add(Me.cmdUserSave)
        Me.tabOptions_Page5.Controls.Add(Me.panSampleFreq)
        Me.tabOptions_Page5.Controls.Add(Me.panUserPoints)
        Me.tabOptions_Page5.Controls.Add(Me.txtUserName)
        Me.tabOptions_Page5.Controls.Add(Me.lblModFreq1)
        Me.tabOptions_Page5.Controls.Add(Me.lblModFreq)
        Me.tabOptions_Page5.Controls.Add(Me.lblWrf1)
        Me.tabOptions_Page5.Controls.Add(Me.lblWrf)
        Me.tabOptions_Page5.Location = New System.Drawing.Point(4, 22)
        Me.tabOptions_Page5.Name = "tabOptions_Page5"
        Me.tabOptions_Page5.Size = New System.Drawing.Size(723, 346)
        Me.tabOptions_Page5.TabIndex = 4
        Me.tabOptions_Page5.Text = "User"
        '
        'lblUserName
        '
        Me.lblUserName.BackColor = System.Drawing.SystemColors.Control
        Me.lblUserName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUserName.Location = New System.Drawing.Point(8, 5)
        Me.lblUserName.Name = "lblUserName"
        Me.lblUserName.Size = New System.Drawing.Size(98, 18)
        Me.lblUserName.TabIndex = 202
        Me.lblUserName.Text = "Segment Name"
        Me.lblUserName.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblUserPoints
        '
        Me.lblUserPoints.BackColor = System.Drawing.SystemColors.Control
        Me.lblUserPoints.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUserPoints.Location = New System.Drawing.Point(258, 4)
        Me.lblUserPoints.Name = "lblUserPoints"
        Me.lblUserPoints.Size = New System.Drawing.Size(66, 14)
        Me.lblUserPoints.TabIndex = 203
        Me.lblUserPoints.Text = "Points"
        Me.lblUserPoints.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblSampleFreq
        '
        Me.lblSampleFreq.BackColor = System.Drawing.SystemColors.Control
        Me.lblSampleFreq.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSampleFreq.Location = New System.Drawing.Point(362, 4)
        Me.lblSampleFreq.Name = "lblSampleFreq"
        Me.lblSampleFreq.Size = New System.Drawing.Size(102, 14)
        Me.lblSampleFreq.TabIndex = 204
        Me.lblSampleFreq.Text = "Sample Frequency"
        Me.lblSampleFreq.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'prgDownLoad
        '
        Me.prgDownLoad.Location = New System.Drawing.Point(7, 54)
        Me.prgDownLoad.Name = "prgDownLoad"
        Me.prgDownLoad.Size = New System.Drawing.Size(456, 14)
        Me.prgDownLoad.TabIndex = 196
        '
        'G1
        '
        Me.G1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.G1.Cursors.AddRange(New NationalInstruments.UI.XYCursor() {Me.CursorMarker, Me.CursorTo, Me.CursorFrom})
        Me.G1.Location = New System.Drawing.Point(72, 71)
        Me.G1.Name = "G1"
        Me.G1.Plots.AddRange(New NationalInstruments.UI.WaveformPlot() {Me.Plot1, Me.Plot2, Me.Plot3, Me.Plot4})
        Me.G1.Size = New System.Drawing.Size(392, 262)
        Me.G1.TabIndex = 0
        Me.G1.UseColorGenerator = True
        Me.G1.XAxes.AddRange(New NationalInstruments.UI.XAxis() {Me.XAxis})
        Me.G1.YAxes.AddRange(New NationalInstruments.UI.YAxis() {Me.YAxis1, Me.YAxis2, Me.YAxis3, Me.YAxis4})
        '
        'CursorMarker
        '
        Me.CursorMarker.Color = System.Drawing.Color.LightGreen
        Me.CursorMarker.Plot = Me.Plot1
        Me.CursorMarker.PointStyle = NationalInstruments.UI.PointStyle.Cross
        Me.CursorMarker.SnapMode = NationalInstruments.UI.CursorSnapMode.Floating
        Me.CursorMarker.Visible = False
        Me.CursorMarker.XPosition = 500.0R
        Me.CursorMarker.YPosition = 0.0R
        '
        'Plot1
        '
        Me.Plot1.CanScaleXAxis = False
        Me.Plot1.CanScaleYAxis = False
        Me.Plot1.LineColor = System.Drawing.Color.LawnGreen
        Me.Plot1.LineColorPrecedence = NationalInstruments.UI.ColorPrecedence.UserDefinedColor
        Me.Plot1.PointColor = System.Drawing.Color.LawnGreen
        Me.Plot1.PointSize = New System.Drawing.Size(1, 1)
        Me.Plot1.PointStyle = NationalInstruments.UI.PointStyle.SolidCircle
        Me.Plot1.XAxis = Me.XAxis
        Me.Plot1.YAxis = Me.YAxis1
        '
        'XAxis
        '
        Me.XAxis.MajorDivisions.GridColor = System.Drawing.Color.DarkGreen
        Me.XAxis.MajorDivisions.Interval = 100.0R
        Me.XAxis.MinorDivisions.GridColor = System.Drawing.Color.DarkGreen
        Me.XAxis.MinorDivisions.Interval = 20.0R
        Me.XAxis.MinorDivisions.TickVisible = True
        Me.XAxis.Range = New NationalInstruments.UI.Range(0.0R, 1000.0R)
        '
        'YAxis1
        '
        Me.YAxis1.AutoSpacing = False
        Me.YAxis1.MajorDivisions.GridColor = System.Drawing.Color.DarkGreen
        Me.YAxis1.MajorDivisions.Interval = 1.0R
        Me.YAxis1.MinorDivisions.GridColor = System.Drawing.Color.DarkGreen
        Me.YAxis1.MinorDivisions.Interval = 0.2R
        Me.YAxis1.MinorDivisions.TickVisible = True
        Me.YAxis1.Range = New NationalInstruments.UI.Range(-2.0R, 2.0R)
        '
        'CursorTo
        '
        Me.CursorTo.Color = System.Drawing.Color.Red
        Me.CursorTo.Plot = Me.Plot1
        Me.CursorTo.PointStyle = NationalInstruments.UI.PointStyle.Plus
        '
        'CursorFrom
        '
        Me.CursorFrom.Color = System.Drawing.Color.Yellow
        Me.CursorFrom.HorizontalCrosshairMode = NationalInstruments.UI.CursorCrosshairMode.Custom
        Me.CursorFrom.Plot = Me.Plot1
        Me.CursorFrom.PointStyle = NationalInstruments.UI.PointStyle.Plus
        Me.CursorFrom.VerticalCrosshairMode = NationalInstruments.UI.CursorCrosshairMode.Custom
        '
        'Plot2
        '
        Me.Plot2.AntiAliased = True
        Me.Plot2.CanScaleXAxis = False
        Me.Plot2.CanScaleYAxis = False
        Me.Plot2.LineColor = System.Drawing.Color.Cyan
        Me.Plot2.LineColorPrecedence = NationalInstruments.UI.ColorPrecedence.UserDefinedColor
        Me.Plot2.LineStyle = NationalInstruments.UI.LineStyle.None
        Me.Plot2.PointColor = System.Drawing.Color.Cyan
        Me.Plot2.PointSize = New System.Drawing.Size(1, 1)
        Me.Plot2.PointStyle = NationalInstruments.UI.PointStyle.SolidCircle
        Me.Plot2.XAxis = Me.XAxis
        Me.Plot2.YAxis = Me.YAxis2
        '
        'YAxis2
        '
        Me.YAxis2.AutoMinorDivisionFrequency = 5
        Me.YAxis2.AutoSpacing = False
        Me.YAxis2.CaptionPosition = CType((NationalInstruments.UI.YAxisPosition.Left Or NationalInstruments.UI.YAxisPosition.Right), NationalInstruments.UI.YAxisPosition)
        Me.YAxis2.CaptionVisible = False
        Me.YAxis2.EndLabelsAlwaysVisible = False
        Me.YAxis2.Inverted = True
        Me.YAxis2.MajorDivisions.GridColor = System.Drawing.Color.DarkGreen
        Me.YAxis2.MajorDivisions.LabelVisible = False
        Me.YAxis2.MajorDivisions.TickVisible = False
        Me.YAxis2.MinorDivisions.GridColor = System.Drawing.Color.DarkGreen
        Me.YAxis2.MinorDivisions.Interval = 0.2R
        Me.YAxis2.Mode = NationalInstruments.UI.AxisMode.Fixed
        Me.YAxis2.Position = CType((NationalInstruments.UI.YAxisPosition.Left Or NationalInstruments.UI.YAxisPosition.Right), NationalInstruments.UI.YAxisPosition)
        Me.YAxis2.Range = New NationalInstruments.UI.Range(-5.0R, 5.0R)
        Me.YAxis2.Visible = False
        '
        'Plot3
        '
        Me.Plot3.AntiAliased = True
        Me.Plot3.CanScaleXAxis = False
        Me.Plot3.CanScaleYAxis = False
        Me.Plot3.LineColor = System.Drawing.Color.Magenta
        Me.Plot3.LineColorPrecedence = NationalInstruments.UI.ColorPrecedence.UserDefinedColor
        Me.Plot3.LineStyle = NationalInstruments.UI.LineStyle.None
        Me.Plot3.PointColor = System.Drawing.Color.Magenta
        Me.Plot3.PointSize = New System.Drawing.Size(1, 1)
        Me.Plot3.PointStyle = NationalInstruments.UI.PointStyle.SolidCircle
        Me.Plot3.XAxis = Me.XAxis
        Me.Plot3.YAxis = Me.YAxis3
        '
        'YAxis3
        '
        Me.YAxis3.AutoMinorDivisionFrequency = 5
        Me.YAxis3.AutoSpacing = False
        Me.YAxis3.CaptionPosition = CType((NationalInstruments.UI.YAxisPosition.Left Or NationalInstruments.UI.YAxisPosition.Right), NationalInstruments.UI.YAxisPosition)
        Me.YAxis3.CaptionVisible = False
        Me.YAxis3.EndLabelsAlwaysVisible = False
        Me.YAxis3.Inverted = True
        Me.YAxis3.MajorDivisions.GridColor = System.Drawing.Color.DarkGreen
        Me.YAxis3.MajorDivisions.LabelVisible = False
        Me.YAxis3.MajorDivisions.TickVisible = False
        Me.YAxis3.MinorDivisions.GridColor = System.Drawing.Color.DarkGreen
        Me.YAxis3.MinorDivisions.Interval = 0.2R
        Me.YAxis3.Mode = NationalInstruments.UI.AxisMode.Fixed
        Me.YAxis3.Position = CType((NationalInstruments.UI.YAxisPosition.Left Or NationalInstruments.UI.YAxisPosition.Right), NationalInstruments.UI.YAxisPosition)
        Me.YAxis3.Visible = False
        '
        'Plot4
        '
        Me.Plot4.AntiAliased = True
        Me.Plot4.CanScaleXAxis = False
        Me.Plot4.CanScaleYAxis = False
        Me.Plot4.LineColor = System.Drawing.Color.Lime
        Me.Plot4.LineColorPrecedence = NationalInstruments.UI.ColorPrecedence.UserDefinedColor
        Me.Plot4.LineStyle = NationalInstruments.UI.LineStyle.None
        Me.Plot4.PointColor = System.Drawing.Color.Lime
        Me.Plot4.PointSize = New System.Drawing.Size(1, 1)
        Me.Plot4.PointStyle = NationalInstruments.UI.PointStyle.SolidCircle
        Me.Plot4.XAxis = Me.XAxis
        Me.Plot4.YAxis = Me.YAxis4
        '
        'YAxis4
        '
        Me.YAxis4.AutoMinorDivisionFrequency = 5
        Me.YAxis4.AutoSpacing = False
        Me.YAxis4.CaptionPosition = CType((NationalInstruments.UI.YAxisPosition.Left Or NationalInstruments.UI.YAxisPosition.Right), NationalInstruments.UI.YAxisPosition)
        Me.YAxis4.CaptionVisible = False
        Me.YAxis4.EndLabelsAlwaysVisible = False
        Me.YAxis4.Inverted = True
        Me.YAxis4.MajorDivisions.GridColor = System.Drawing.Color.DarkGreen
        Me.YAxis4.MajorDivisions.LabelVisible = False
        Me.YAxis4.MajorDivisions.TickVisible = False
        Me.YAxis4.MinorDivisions.GridColor = System.Drawing.Color.DarkGreen
        Me.YAxis4.MinorDivisions.Interval = 0.2R
        Me.YAxis4.Mode = NationalInstruments.UI.AxisMode.Fixed
        Me.YAxis4.Position = CType((NationalInstruments.UI.YAxisPosition.Left Or NationalInstruments.UI.YAxisPosition.Right), NationalInstruments.UI.YAxisPosition)
        Me.YAxis4.Visible = False
        '
        'cmdZoomOut
        '
        Me.cmdZoomOut.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdZoomOut.Location = New System.Drawing.Point(20, 93)
        Me.cmdZoomOut.Margin = New System.Windows.Forms.Padding(0)
        Me.cmdZoomOut.Name = "cmdZoomOut"
        Me.cmdZoomOut.Size = New System.Drawing.Size(52, 22)
        Me.cmdZoomOut.TabIndex = 194
        Me.cmdZoomOut.Text = "Out"
        '
        'cmdPan
        '
        Me.cmdPan.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPan.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdPan.Location = New System.Drawing.Point(20, 115)
        Me.cmdPan.Margin = New System.Windows.Forms.Padding(0)
        Me.cmdPan.Name = "cmdPan"
        Me.cmdPan.Size = New System.Drawing.Size(52, 22)
        Me.cmdPan.TabIndex = 193
        Me.cmdPan.Text = "Pan"
        Me.cmdPan.UseVisualStyleBackColor = False
        '
        'cmdSnap
        '
        Me.cmdSnap.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdSnap.Location = New System.Drawing.Point(20, 150)
        Me.cmdSnap.Margin = New System.Windows.Forms.Padding(0)
        Me.cmdSnap.Name = "cmdSnap"
        Me.cmdSnap.Size = New System.Drawing.Size(52, 22)
        Me.cmdSnap.TabIndex = 192
        Me.cmdSnap.Text = "Snap"
        '
        'cmdUserClear
        '
        Me.cmdUserClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdUserClear.Location = New System.Drawing.Point(20, 185)
        Me.cmdUserClear.Margin = New System.Windows.Forms.Padding(0)
        Me.cmdUserClear.Name = "cmdUserClear"
        Me.cmdUserClear.Size = New System.Drawing.Size(52, 22)
        Me.cmdUserClear.TabIndex = 191
        Me.cmdUserClear.Text = "Clear"
        '
        'cmdUserSine
        '
        Me.cmdUserSine.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdUserSine.Location = New System.Drawing.Point(20, 245)
        Me.cmdUserSine.Margin = New System.Windows.Forms.Padding(0)
        Me.cmdUserSine.Name = "cmdUserSine"
        Me.cmdUserSine.Size = New System.Drawing.Size(52, 22)
        Me.cmdUserSine.TabIndex = 190
        Me.cmdUserSine.Text = "Sine"
        '
        'cmdUserSquare
        '
        Me.cmdUserSquare.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdUserSquare.Location = New System.Drawing.Point(20, 267)
        Me.cmdUserSquare.Margin = New System.Windows.Forms.Padding(0)
        Me.cmdUserSquare.Name = "cmdUserSquare"
        Me.cmdUserSquare.Size = New System.Drawing.Size(52, 22)
        Me.cmdUserSquare.TabIndex = 189
        Me.cmdUserSquare.Text = "Square"
        '
        'cmdUserRamp
        '
        Me.cmdUserRamp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdUserRamp.Location = New System.Drawing.Point(20, 289)
        Me.cmdUserRamp.Margin = New System.Windows.Forms.Padding(0)
        Me.cmdUserRamp.Name = "cmdUserRamp"
        Me.cmdUserRamp.Size = New System.Drawing.Size(52, 22)
        Me.cmdUserRamp.TabIndex = 188
        Me.cmdUserRamp.Text = "Ramp"
        '
        'cmdUserPm
        '
        Me.cmdUserPm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdUserPm.Location = New System.Drawing.Point(20, 311)
        Me.cmdUserPm.Margin = New System.Windows.Forms.Padding(0)
        Me.cmdUserPm.Name = "cmdUserPm"
        Me.cmdUserPm.Size = New System.Drawing.Size(52, 22)
        Me.cmdUserPm.TabIndex = 187
        Me.cmdUserPm.Text = "P.M."
        '
        'cmdZoom
        '
        Me.cmdZoom.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdZoom.Location = New System.Drawing.Point(20, 71)
        Me.cmdZoom.Margin = New System.Windows.Forms.Padding(0)
        Me.cmdZoom.Name = "cmdZoom"
        Me.cmdZoom.Size = New System.Drawing.Size(52, 22)
        Me.cmdZoom.TabIndex = 186
        Me.cmdZoom.Text = "Zoom"
        '
        'cmdUserUndo
        '
        Me.cmdUserUndo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdUserUndo.Location = New System.Drawing.Point(20, 207)
        Me.cmdUserUndo.Margin = New System.Windows.Forms.Padding(0)
        Me.cmdUserUndo.Name = "cmdUserUndo"
        Me.cmdUserUndo.Size = New System.Drawing.Size(52, 22)
        Me.cmdUserUndo.TabIndex = 185
        Me.cmdUserUndo.Text = "Undo"
        '
        'cmdDownLoad
        '
        Me.cmdDownLoad.Location = New System.Drawing.Point(129, 4)
        Me.cmdDownLoad.Margin = New System.Windows.Forms.Padding(0)
        Me.cmdDownLoad.Name = "cmdDownLoad"
        Me.cmdDownLoad.Size = New System.Drawing.Size(108, 22)
        Me.cmdDownLoad.TabIndex = 184
        Me.cmdDownLoad.Text = "Download"
        '
        'cmdUserLoad
        '
        Me.cmdUserLoad.Location = New System.Drawing.Point(129, 25)
        Me.cmdUserLoad.Name = "cmdUserLoad"
        Me.cmdUserLoad.Size = New System.Drawing.Size(52, 22)
        Me.cmdUserLoad.TabIndex = 183
        Me.cmdUserLoad.Text = "Load"
        '
        'cmdUserSave
        '
        Me.cmdUserSave.Location = New System.Drawing.Point(178, 25)
        Me.cmdUserSave.Name = "cmdUserSave"
        Me.cmdUserSave.Size = New System.Drawing.Size(59, 22)
        Me.cmdUserSave.TabIndex = 182
        Me.cmdUserSave.Text = "Save"
        '
        'panSampleFreq
        '
        Me.panSampleFreq.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.panSampleFreq.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panSampleFreq.Controls.Add(Me.txtSampleFreq)
        Me.panSampleFreq.Controls.Add(Me.spnSampleFreq)
        Me.panSampleFreq.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panSampleFreq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.panSampleFreq.Location = New System.Drawing.Point(362, 21)
        Me.panSampleFreq.Name = "panSampleFreq"
        Me.panSampleFreq.Size = New System.Drawing.Size(101, 21)
        Me.panSampleFreq.TabIndex = 173
        '
        'txtSampleFreq
        '
        Me.txtSampleFreq.BackColor = System.Drawing.SystemColors.Window
        Me.txtSampleFreq.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSampleFreq.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSampleFreq.Location = New System.Drawing.Point(2, 2)
        Me.txtSampleFreq.Name = "txtSampleFreq"
        Me.txtSampleFreq.Size = New System.Drawing.Size(79, 13)
        Me.txtSampleFreq.TabIndex = 174
        '
        'spnSampleFreq
        '
        Me.spnSampleFreq.Location = New System.Drawing.Point(83, -2)
        Me.spnSampleFreq.Name = "spnSampleFreq"
        Me.spnSampleFreq.Size = New System.Drawing.Size(16, 20)
        Me.spnSampleFreq.TabIndex = 175
        '
        'panUserPoints
        '
        Me.panUserPoints.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.panUserPoints.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panUserPoints.Controls.Add(Me.txtUserPoints)
        Me.panUserPoints.Controls.Add(Me.spnUserPoints)
        Me.panUserPoints.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panUserPoints.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.panUserPoints.Location = New System.Drawing.Point(258, 21)
        Me.panUserPoints.Name = "panUserPoints"
        Me.panUserPoints.Size = New System.Drawing.Size(79, 21)
        Me.panUserPoints.TabIndex = 170
        '
        'txtUserPoints
        '
        Me.txtUserPoints.BackColor = System.Drawing.SystemColors.Window
        Me.txtUserPoints.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtUserPoints.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtUserPoints.Location = New System.Drawing.Point(2, 2)
        Me.txtUserPoints.Name = "txtUserPoints"
        Me.txtUserPoints.Size = New System.Drawing.Size(56, 13)
        Me.txtUserPoints.TabIndex = 171
        Me.txtUserPoints.Text = "262,144"
        '
        'spnUserPoints
        '
        Me.spnUserPoints.Location = New System.Drawing.Point(61, -2)
        Me.spnUserPoints.Name = "spnUserPoints"
        Me.spnUserPoints.Size = New System.Drawing.Size(16, 20)
        Me.spnUserPoints.TabIndex = 172
        '
        'txtUserName
        '
        Me.txtUserName.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtUserName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtUserName.Location = New System.Drawing.Point(8, 23)
        Me.txtUserName.Name = "txtUserName"
        Me.txtUserName.Size = New System.Drawing.Size(98, 20)
        Me.txtUserName.TabIndex = 11
        '
        'lblModFreq1
        '
        Me.lblModFreq1.AutoSize = True
        Me.lblModFreq1.BackColor = System.Drawing.SystemColors.Control
        Me.lblModFreq1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblModFreq1.Location = New System.Drawing.Point(26, 53)
        Me.lblModFreq1.Name = "lblModFreq1"
        Me.lblModFreq1.Size = New System.Drawing.Size(58, 13)
        Me.lblModFreq1.TabIndex = 199
        Me.lblModFreq1.Text = "Mod. Freq."
        Me.lblModFreq1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblModFreq
        '
        Me.lblModFreq.BackColor = System.Drawing.SystemColors.Control
        Me.lblModFreq.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblModFreq.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblModFreq.Location = New System.Drawing.Point(86, 52)
        Me.lblModFreq.Name = "lblModFreq"
        Me.lblModFreq.Size = New System.Drawing.Size(87, 17)
        Me.lblModFreq.TabIndex = 198
        Me.lblModFreq.Text = "1.12345678 MHz"
        '
        'lblWrf1
        '
        Me.lblWrf1.BackColor = System.Drawing.SystemColors.Control
        Me.lblWrf1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblWrf1.Location = New System.Drawing.Point(247, 53)
        Me.lblWrf1.Name = "lblWrf1"
        Me.lblWrf1.Size = New System.Drawing.Size(90, 13)
        Me.lblWrf1.TabIndex = 200
        Me.lblWrf1.Text = "Segment Freq."
        Me.lblWrf1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblWrf
        '
        Me.lblWrf.BackColor = System.Drawing.SystemColors.Control
        Me.lblWrf.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblWrf.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblWrf.Location = New System.Drawing.Point(342, 52)
        Me.lblWrf.Name = "lblWrf"
        Me.lblWrf.Size = New System.Drawing.Size(121, 17)
        Me.lblWrf.TabIndex = 201
        Me.lblWrf.Text = "1.12345678 MHz"
        '
        'tabOptions_Page6
        '
        Me.tabOptions_Page6.Controls.Add(Me.fraListCat)
        Me.tabOptions_Page6.Controls.Add(Me.fraSeqName)
        Me.tabOptions_Page6.Controls.Add(Me.fraFreeMem)
        Me.tabOptions_Page6.Location = New System.Drawing.Point(4, 22)
        Me.tabOptions_Page6.Name = "tabOptions_Page6"
        Me.tabOptions_Page6.Size = New System.Drawing.Size(723, 346)
        Me.tabOptions_Page6.TabIndex = 5
        Me.tabOptions_Page6.Text = "Sequence"
        '
        'fraListCat
        '
        Me.fraListCat.Controls.Add(Me.lstListCat)
        Me.fraListCat.Controls.Add(Me.cmdSegDel)
        Me.fraListCat.Controls.Add(Me.cmdSegAdd)
        Me.fraListCat.Controls.Add(Me.Label2)
        Me.fraListCat.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraListCat.Location = New System.Drawing.Point(8, 4)
        Me.fraListCat.Name = "fraListCat"
        Me.fraListCat.Size = New System.Drawing.Size(155, 217)
        Me.fraListCat.TabIndex = 165
        Me.fraListCat.TabStop = False
        Me.fraListCat.Text = "Downloaded Segments"
        '
        'lstListCat
        '
        Me.lstListCat.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lstListCat.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lstListCat.Location = New System.Drawing.Point(8, 40)
        Me.lstListCat.Name = "lstListCat"
        Me.lstListCat.Size = New System.Drawing.Size(138, 147)
        Me.lstListCat.TabIndex = 166
        '
        'cmdSegDel
        '
        Me.cmdSegDel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.cmdSegDel.Location = New System.Drawing.Point(7, 186)
        Me.cmdSegDel.Name = "cmdSegDel"
        Me.cmdSegDel.Size = New System.Drawing.Size(58, 24)
        Me.cmdSegDel.TabIndex = 167
        Me.cmdSegDel.Text = "Delete"
        '
        'cmdSegAdd
        '
        Me.cmdSegAdd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.cmdSegAdd.Location = New System.Drawing.Point(63, 186)
        Me.cmdSegAdd.Name = "cmdSegAdd"
        Me.cmdSegAdd.Size = New System.Drawing.Size(83, 24)
        Me.cmdSegAdd.TabIndex = 168
        Me.cmdSegAdd.Text = "Add to List"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(8, 22)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(138, 19)
        Me.Label2.TabIndex = 169
        Me.Label2.Text = " Name,  Length"
        '
        'fraSeqName
        '
        Me.fraSeqName.Controls.Add(Me.lstListSseqSeq)
        Me.fraSeqName.Controls.Add(Me.panDwelCoun)
        Me.fraSeqName.Controls.Add(Me.cmdSeqListDown)
        Me.fraSeqName.Controls.Add(Me.cmdSeqListUp)
        Me.fraSeqName.Controls.Add(Me.cmdSeqListRemove)
        Me.fraSeqName.Controls.Add(Me.Label3)
        Me.fraSeqName.Controls.Add(Me.Label1)
        Me.fraSeqName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraSeqName.Location = New System.Drawing.Point(170, 4)
        Me.fraSeqName.Name = "fraSeqName"
        Me.fraSeqName.Size = New System.Drawing.Size(203, 217)
        Me.fraSeqName.TabIndex = 139
        Me.fraSeqName.TabStop = False
        Me.fraSeqName.Text = "Sequence List Definition"
        '
        'lstListSseqSeq
        '
        Me.lstListSseqSeq.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lstListSseqSeq.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lstListSseqSeq.Location = New System.Drawing.Point(8, 40)
        Me.lstListSseqSeq.Name = "lstListSseqSeq"
        Me.lstListSseqSeq.Size = New System.Drawing.Size(187, 95)
        Me.lstListSseqSeq.TabIndex = 140
        '
        'panDwelCoun
        '
        Me.panDwelCoun.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.panDwelCoun.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panDwelCoun.Controls.Add(Me.txtDwelCoun)
        Me.panDwelCoun.Controls.Add(Me.spnDwelCoun)
        Me.panDwelCoun.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.26!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panDwelCoun.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.panDwelCoun.Location = New System.Drawing.Point(97, 179)
        Me.panDwelCoun.Name = "panDwelCoun"
        Me.panDwelCoun.Size = New System.Drawing.Size(81, 21)
        Me.panDwelCoun.TabIndex = 141
        '
        'txtDwelCoun
        '
        Me.txtDwelCoun.BackColor = System.Drawing.SystemColors.Window
        Me.txtDwelCoun.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtDwelCoun.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDwelCoun.Location = New System.Drawing.Point(2, 2)
        Me.txtDwelCoun.Name = "txtDwelCoun"
        Me.txtDwelCoun.Size = New System.Drawing.Size(58, 13)
        Me.txtDwelCoun.TabIndex = 142
        '
        'spnDwelCoun
        '
        Me.spnDwelCoun.Location = New System.Drawing.Point(63, -2)
        Me.spnDwelCoun.Name = "spnDwelCoun"
        Me.spnDwelCoun.Size = New System.Drawing.Size(16, 20)
        Me.spnDwelCoun.TabIndex = 143
        '
        'cmdSeqListDown
        '
        Me.cmdSeqListDown.Location = New System.Drawing.Point(134, 138)
        Me.cmdSeqListDown.Name = "cmdSeqListDown"
        Me.cmdSeqListDown.Size = New System.Drawing.Size(62, 24)
        Me.cmdSeqListDown.TabIndex = 144
        Me.cmdSeqListDown.Text = "Down"
        '
        'cmdSeqListUp
        '
        Me.cmdSeqListUp.Location = New System.Drawing.Point(73, 138)
        Me.cmdSeqListUp.Name = "cmdSeqListUp"
        Me.cmdSeqListUp.Size = New System.Drawing.Size(62, 24)
        Me.cmdSeqListUp.TabIndex = 145
        Me.cmdSeqListUp.Text = "Up"
        '
        'cmdSeqListRemove
        '
        Me.cmdSeqListRemove.Location = New System.Drawing.Point(7, 138)
        Me.cmdSeqListRemove.Name = "cmdSeqListRemove"
        Me.cmdSeqListRemove.Size = New System.Drawing.Size(68, 24)
        Me.cmdSeqListRemove.TabIndex = 146
        Me.cmdSeqListRemove.Text = "Remove"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(14, 181)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(64, 13)
        Me.Label3.TabIndex = 148
        Me.Label3.Text = "Dwell Count"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(8, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(187, 19)
        Me.Label1.TabIndex = 147
        Me.Label1.Text = " Segment Name,  Dwell Count"
        '
        'fraFreeMem
        '
        Me.fraFreeMem.Controls.Add(Me.lblListFreeLbl)
        Me.fraFreeMem.Controls.Add(Me.lblListSseqFreeLbl)
        Me.fraFreeMem.Controls.Add(Me.lblListFree)
        Me.fraFreeMem.Controls.Add(Me.lblListSseqFree)
        Me.fraFreeMem.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraFreeMem.Location = New System.Drawing.Point(8, 230)
        Me.fraFreeMem.Name = "fraFreeMem"
        Me.fraFreeMem.Size = New System.Drawing.Size(155, 59)
        Me.fraFreeMem.TabIndex = 160
        Me.fraFreeMem.TabStop = False
        Me.fraFreeMem.Text = "Available Memory"
        '
        'lblListFreeLbl
        '
        Me.lblListFreeLbl.BackColor = System.Drawing.SystemColors.Control
        Me.lblListFreeLbl.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblListFreeLbl.Location = New System.Drawing.Point(8, 16)
        Me.lblListFreeLbl.Name = "lblListFreeLbl"
        Me.lblListFreeLbl.Size = New System.Drawing.Size(66, 19)
        Me.lblListFreeLbl.TabIndex = 164
        Me.lblListFreeLbl.Text = "Segment"
        Me.lblListFreeLbl.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblListSseqFreeLbl
        '
        Me.lblListSseqFreeLbl.BackColor = System.Drawing.SystemColors.Control
        Me.lblListSseqFreeLbl.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblListSseqFreeLbl.Location = New System.Drawing.Point(81, 16)
        Me.lblListSseqFreeLbl.Name = "lblListSseqFreeLbl"
        Me.lblListSseqFreeLbl.Size = New System.Drawing.Size(72, 19)
        Me.lblListSseqFreeLbl.TabIndex = 163
        Me.lblListSseqFreeLbl.Text = "Sequence"
        Me.lblListSseqFreeLbl.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblListFree
        '
        Me.lblListFree.BackColor = System.Drawing.SystemColors.Control
        Me.lblListFree.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblListFree.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblListFree.Location = New System.Drawing.Point(8, 35)
        Me.lblListFree.Name = "lblListFree"
        Me.lblListFree.Size = New System.Drawing.Size(66, 17)
        Me.lblListFree.TabIndex = 162
        Me.lblListFree.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblListSseqFree
        '
        Me.lblListSseqFree.BackColor = System.Drawing.SystemColors.Control
        Me.lblListSseqFree.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblListSseqFree.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblListSseqFree.Location = New System.Drawing.Point(81, 35)
        Me.lblListSseqFree.Name = "lblListSseqFree"
        Me.lblListSseqFree.Size = New System.Drawing.Size(66, 17)
        Me.lblListSseqFree.TabIndex = 161
        Me.lblListSseqFree.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'tabOptions_Page7
        '
        Me.tabOptions_Page7.Controls.Add(Me.fraOutLowPassFilt)
        Me.tabOptions_Page7.Controls.Add(Me.fraOutLoadImpe)
        Me.tabOptions_Page7.Controls.Add(Me.cmdAbout)
        Me.tabOptions_Page7.Controls.Add(Me.cmdTest)
        Me.tabOptions_Page7.Controls.Add(Me.cmdUpdateTip)
        Me.tabOptions_Page7.Controls.Add(Me.fraPanelConfig)
        Me.tabOptions_Page7.Controls.Add(Me.picLeds)
        Me.tabOptions_Page7.Location = New System.Drawing.Point(4, 22)
        Me.tabOptions_Page7.Name = "tabOptions_Page7"
        Me.tabOptions_Page7.Size = New System.Drawing.Size(723, 346)
        Me.tabOptions_Page7.TabIndex = 6
        Me.tabOptions_Page7.Text = "Options"
        '
        'fraOutLowPassFilt
        '
        Me.fraOutLowPassFilt.Controls.Add(Me.chkFilterStateOn)
        Me.fraOutLowPassFilt.Controls.Add(Me.optCutOff250)
        Me.fraOutLowPassFilt.Controls.Add(Me.optCutOff10)
        Me.fraOutLowPassFilt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraOutLowPassFilt.Location = New System.Drawing.Point(202, 4)
        Me.fraOutLowPassFilt.Name = "fraOutLowPassFilt"
        Me.fraOutLowPassFilt.Size = New System.Drawing.Size(178, 109)
        Me.fraOutLowPassFilt.TabIndex = 56
        Me.fraOutLowPassFilt.TabStop = False
        Me.fraOutLowPassFilt.Text = "Low Pass Filters"
        '
        'chkFilterStateOn
        '
        Me.chkFilterStateOn.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFilterStateOn.Location = New System.Drawing.Point(10, 78)
        Me.chkFilterStateOn.Name = "chkFilterStateOn"
        Me.chkFilterStateOn.Size = New System.Drawing.Size(54, 18)
        Me.chkFilterStateOn.TabIndex = 57
        Me.chkFilterStateOn.Text = "On"
        '
        'optCutOff250
        '
        Me.optCutOff250.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optCutOff250.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optCutOff250.Location = New System.Drawing.Point(10, 22)
        Me.optCutOff250.Name = "optCutOff250"
        Me.optCutOff250.Size = New System.Drawing.Size(150, 18)
        Me.optCutOff250.TabIndex = 58
        Me.optCutOff250.Text = "250 KHz 5th Order Bessel"
        '
        'optCutOff10
        '
        Me.optCutOff10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optCutOff10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optCutOff10.Location = New System.Drawing.Point(10, 50)
        Me.optCutOff10.Name = "optCutOff10"
        Me.optCutOff10.Size = New System.Drawing.Size(146, 18)
        Me.optCutOff10.TabIndex = 59
        Me.optCutOff10.Text = "10 MHz 7th Order Bessel"
        '
        'fraOutLoadImpe
        '
        Me.fraOutLoadImpe.Controls.Add(Me.optLoadImp50)
        Me.fraOutLoadImpe.Controls.Add(Me.optLoadImp75)
        Me.fraOutLoadImpe.Controls.Add(Me.optLoadImpHigh)
        Me.fraOutLoadImpe.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraOutLoadImpe.Location = New System.Drawing.Point(16, 4)
        Me.fraOutLoadImpe.Name = "fraOutLoadImpe"
        Me.fraOutLoadImpe.Size = New System.Drawing.Size(178, 109)
        Me.fraOutLoadImpe.TabIndex = 52
        Me.fraOutLoadImpe.TabStop = False
        Me.fraOutLoadImpe.Text = "Output Load Reference"
        '
        'optLoadImp50
        '
        Me.optLoadImp50.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optLoadImp50.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optLoadImp50.Location = New System.Drawing.Point(8, 22)
        Me.optLoadImp50.Name = "optLoadImp50"
        Me.optLoadImp50.Size = New System.Drawing.Size(66, 18)
        Me.optLoadImp50.TabIndex = 55
        Me.optLoadImp50.Text = "50 Ohm"
        '
        'optLoadImp75
        '
        Me.optLoadImp75.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optLoadImp75.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optLoadImp75.Location = New System.Drawing.Point(8, 50)
        Me.optLoadImp75.Name = "optLoadImp75"
        Me.optLoadImp75.Size = New System.Drawing.Size(66, 18)
        Me.optLoadImp75.TabIndex = 53
        Me.optLoadImp75.Text = "75 Ohm"
        '
        'optLoadImpHigh
        '
        Me.optLoadImpHigh.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optLoadImpHigh.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optLoadImpHigh.Location = New System.Drawing.Point(8, 78)
        Me.optLoadImpHigh.Name = "optLoadImpHigh"
        Me.optLoadImpHigh.Size = New System.Drawing.Size(106, 18)
        Me.optLoadImpHigh.TabIndex = 54
        Me.optLoadImpHigh.Text = "High Impedance"
        '
        'cmdAbout
        '
        Me.cmdAbout.BackColor = System.Drawing.SystemColors.Control
        Me.cmdAbout.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAbout.Location = New System.Drawing.Point(8, 288)
        Me.cmdAbout.Name = "cmdAbout"
        Me.cmdAbout.Size = New System.Drawing.Size(76, 23)
        Me.cmdAbout.TabIndex = 12
        Me.cmdAbout.Text = "&About"
        Me.cmdAbout.UseVisualStyleBackColor = False
        '
        'cmdTest
        '
        Me.cmdTest.BackColor = System.Drawing.SystemColors.Control
        Me.cmdTest.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdTest.Location = New System.Drawing.Point(89, 288)
        Me.cmdTest.Name = "cmdTest"
        Me.cmdTest.Size = New System.Drawing.Size(91, 23)
        Me.cmdTest.TabIndex = 13
        Me.cmdTest.Text = "Built-In &Test"
        Me.cmdTest.UseVisualStyleBackColor = False
        '
        'cmdUpdateTip
        '
        Me.cmdUpdateTip.BackColor = System.Drawing.SystemColors.Control
        Me.cmdUpdateTip.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdUpdateTip.Location = New System.Drawing.Point(186, 288)
        Me.cmdUpdateTip.Name = "cmdUpdateTip"
        Me.cmdUpdateTip.Size = New System.Drawing.Size(97, 23)
        Me.cmdUpdateTip.TabIndex = 14
        Me.cmdUpdateTip.Text = "&Update TIP"
        Me.cmdUpdateTip.UseVisualStyleBackColor = False
        Me.cmdUpdateTip.Visible = False
        '
        'fraPanelConfig
        '
        Me.fraPanelConfig.BackColor = System.Drawing.SystemColors.Control
        Me.fraPanelConfig.Controls.Add(Me.pctMode)
        Me.fraPanelConfig.Controls.Add(Me.cmdLoadFromFile)
        Me.fraPanelConfig.Controls.Add(Me.cmdSaveToFile)
        Me.fraPanelConfig.Controls.Add(Me.cmdLoadFromInstrument)
        Me.fraPanelConfig.Controls.Add(Me.cmdLocal)
        Me.fraPanelConfig.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraPanelConfig.Location = New System.Drawing.Point(16, 119)
        Me.fraPanelConfig.Name = "fraPanelConfig"
        Me.fraPanelConfig.Size = New System.Drawing.Size(149, 146)
        Me.fraPanelConfig.TabIndex = 208
        Me.fraPanelConfig.TabStop = False
        Me.fraPanelConfig.Text = "Panel Configuration"
        '
        'pctMode
        '
        Me.pctMode.LedStyle = NationalInstruments.UI.LedStyle.Round3D
        Me.pctMode.Location = New System.Drawing.Point(19, 113)
        Me.pctMode.Name = "pctMode"
        Me.pctMode.OffColor = System.Drawing.Color.Lime
        Me.pctMode.OnColor = System.Drawing.Color.Red
        Me.pctMode.Size = New System.Drawing.Size(26, 26)
        Me.pctMode.TabIndex = 214
        '
        'cmdLoadFromFile
        '
        Me.cmdLoadFromFile.BackColor = System.Drawing.SystemColors.Control
        Me.cmdLoadFromFile.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdLoadFromFile.Location = New System.Drawing.Point(8, 22)
        Me.cmdLoadFromFile.Name = "cmdLoadFromFile"
        Me.cmdLoadFromFile.Size = New System.Drawing.Size(123, 25)
        Me.cmdLoadFromFile.TabIndex = 209
        Me.cmdLoadFromFile.Text = "Load from File"
        Me.cmdLoadFromFile.UseVisualStyleBackColor = False
        '
        'cmdSaveToFile
        '
        Me.cmdSaveToFile.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSaveToFile.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSaveToFile.Location = New System.Drawing.Point(8, 51)
        Me.cmdSaveToFile.Name = "cmdSaveToFile"
        Me.cmdSaveToFile.Size = New System.Drawing.Size(123, 25)
        Me.cmdSaveToFile.TabIndex = 210
        Me.cmdSaveToFile.Text = "Save to File"
        Me.cmdSaveToFile.UseVisualStyleBackColor = False
        '
        'cmdLoadFromInstrument
        '
        Me.cmdLoadFromInstrument.BackColor = System.Drawing.SystemColors.Control
        Me.cmdLoadFromInstrument.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdLoadFromInstrument.Location = New System.Drawing.Point(8, 81)
        Me.cmdLoadFromInstrument.Name = "cmdLoadFromInstrument"
        Me.cmdLoadFromInstrument.Size = New System.Drawing.Size(123, 25)
        Me.cmdLoadFromInstrument.TabIndex = 211
        Me.cmdLoadFromInstrument.Text = "Load from Instrument"
        Me.cmdLoadFromInstrument.UseVisualStyleBackColor = False
        '
        'cmdLocal
        '
        Me.cmdLocal.BackColor = System.Drawing.SystemColors.Control
        Me.cmdLocal.Enabled = False
        Me.cmdLocal.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdLocal.Location = New System.Drawing.Point(65, 114)
        Me.cmdLocal.Name = "cmdLocal"
        Me.cmdLocal.Size = New System.Drawing.Size(66, 25)
        Me.cmdLocal.TabIndex = 212
        Me.cmdLocal.Text = "Local"
        Me.cmdLocal.UseVisualStyleBackColor = False
        '
        'picLeds
        '
        Me.picLeds.Location = New System.Drawing.Point(171, 185)
        Me.picLeds.Name = "picLeds"
        Me.picLeds.Size = New System.Drawing.Size(49, 24)
        Me.picLeds.TabIndex = 209
        Me.picLeds.TabStop = False
        '
        'tabOptions_Page8
        '
        Me.tabOptions_Page8.Controls.Add(Me.Atlas_SFP)
        Me.tabOptions_Page8.Location = New System.Drawing.Point(4, 22)
        Me.tabOptions_Page8.Name = "tabOptions_Page8"
        Me.tabOptions_Page8.Size = New System.Drawing.Size(723, 346)
        Me.tabOptions_Page8.TabIndex = 7
        Me.tabOptions_Page8.Text = "ATLAS"
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
        'timDebug
        '
        Me.timDebug.Interval = 10000
        '
        'imlOutRelayWave
        '
        Me.imlOutRelayWave.ImageStream = CType(resources.GetObject("imlOutRelayWave.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imlOutRelayWave.TransparentColor = System.Drawing.Color.Transparent
        Me.imlOutRelayWave.Images.SetKeyName(0, "OutRelayWaveBlack.bmp")
        Me.imlOutRelayWave.Images.SetKeyName(1, "OutRelayWaveSine.bmp")
        Me.imlOutRelayWave.Images.SetKeyName(2, "OutRelayWaveSquare.bmp")
        Me.imlOutRelayWave.Images.SetKeyName(3, "OutRelayWaveRamp.bmp")
        Me.imlOutRelayWave.Images.SetKeyName(4, "OutRelayWaveTriangle.bmp")
        Me.imlOutRelayWave.Images.SetKeyName(5, "OutRelayWaveDC.bmp")
        Me.imlOutRelayWave.Images.SetKeyName(6, "OutRelayWaveUser.bmp")
        '
        'frmHpE1445a
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(743, 437)
        Me.Controls.Add(Me.panMainControl)
        Me.Controls.Add(Me.cmdHelp)
        Me.Controls.Add(Me.cmdQuit)
        Me.Controls.Add(Me.cmdReset)
        Me.Controls.Add(Me.txtVoid)
        Me.Controls.Add(Me.sbrUserInformation)
        Me.Controls.Add(Me.tabOptions)
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmHpE1445a"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Arbitrary Waveform Generator"
        Me.panMainControl.ResumeLayout(False)
        Me.fraOnOff.ResumeLayout(False)
        Me.fraOnOff.PerformLayout()
        CType(Me.imgWaveDisplay, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabOptions.ResumeLayout(False)
        Me.tabOptions_Page1.ResumeLayout(False)
        Me.fraFuncWave.ResumeLayout(False)
        Me.fraFuncWave.PerformLayout()
        Me.fraFreq.ResumeLayout(False)
        Me.panFreq.ResumeLayout(False)
        Me.panFreq.PerformLayout()
        CType(Me.spnFreq, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraWavePol.ResumeLayout(False)
        Me.fraWavePoin.ResumeLayout(False)
        Me.panWavePoin.ResumeLayout(False)
        Me.panWavePoin.PerformLayout()
        CType(Me.spnWavePoin, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraRoscSour.ResumeLayout(False)
        Me.fraRoscSour.PerformLayout()
        Me.panRoscFreqExt.ResumeLayout(False)
        Me.panRoscFreqExt.PerformLayout()
        CType(Me.spnRoscFreqExt, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SSFrame2.ResumeLayout(False)
        Me.fraAmpl.ResumeLayout(False)
        Me.panAmpl.ResumeLayout(False)
        Me.panAmpl.PerformLayout()
        CType(Me.spnAmpl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraAmplDiv7.ResumeLayout(False)
        Me.panAmplDiv7.ResumeLayout(False)
        Me.panAmplDiv7.PerformLayout()
        CType(Me.spnAmplDiv7, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraDcOffs.ResumeLayout(False)
        Me.panDcOffs.ResumeLayout(False)
        Me.panDcOffs.PerformLayout()
        CType(Me.spnDcOffs, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraAmplUnits.ResumeLayout(False)
        Me.tabOptions_Page2.ResumeLayout(False)
        Me.fraArmLay.ResumeLayout(False)
        Me.fraArmLay.PerformLayout()
        Me.panArmCoun.ResumeLayout(False)
        Me.panArmCoun.PerformLayout()
        CType(Me.spnArmCount, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panArmLay2Coun.ResumeLayout(False)
        Me.panArmLay2Coun.PerformLayout()
        CType(Me.spnArmLay2Coun, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraTrigStart.ResumeLayout(False)
        Me.fraTrigStart.PerformLayout()
        Me.fraTrigStop.ResumeLayout(False)
        Me.fraTrigStop.PerformLayout()
        Me.tabOptions_Page3.ResumeLayout(False)
        Me.fraMarkEclt0.ResumeLayout(False)
        Me.fraMarkEclt0.PerformLayout()
        Me.fraMarkEclt1.ResumeLayout(False)
        Me.fraMarkEclt1.PerformLayout()
        Me.fraMarkExt.ResumeLayout(False)
        Me.fraMarkExt.PerformLayout()
        Me.fraMarkSpo.ResumeLayout(False)
        Me.panMarkSpo.ResumeLayout(False)
        Me.panMarkSpo.PerformLayout()
        CType(Me.spnMarkSpo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabOptions_Page4.ResumeLayout(False)
        Me.fraFreqMode.ResumeLayout(False)
        Me.fraFreqModeSwe.ResumeLayout(False)
        Me.panAgileSweepStart.ResumeLayout(False)
        Me.panAgileSweepStart.PerformLayout()
        CType(Me.spnAgileSweepStart, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panAgileSweepStop.ResumeLayout(False)
        Me.panAgileSweepStop.PerformLayout()
        CType(Me.spnAgileSweepStop, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panAgileSweepSteps.ResumeLayout(False)
        Me.panAgileSweepSteps.PerformLayout()
        CType(Me.spnSweCoun, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panAgileSweepDur.ResumeLayout(False)
        Me.panAgileSweepDur.PerformLayout()
        CType(Me.spnAgileSweepDur, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panSweCoun.ResumeLayout(False)
        Me.panSweCoun.PerformLayout()
        CType(Me.spnAgileSweepSteps, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraAgileFsk.ResumeLayout(False)
        Me.panAgileFskKeyH.ResumeLayout(False)
        Me.panAgileFskKeyH.PerformLayout()
        CType(Me.spnAgileFskKeyH, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panAgileFskKeyL.ResumeLayout(False)
        Me.panAgileFskKeyL.PerformLayout()
        CType(Me.spnAgileFskKeyL, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabOptions_Page5.ResumeLayout(False)
        Me.tabOptions_Page5.PerformLayout()
        CType(Me.G1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CursorMarker, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CursorTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CursorFrom, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panSampleFreq.ResumeLayout(False)
        Me.panSampleFreq.PerformLayout()
        CType(Me.spnSampleFreq, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panUserPoints.ResumeLayout(False)
        Me.panUserPoints.PerformLayout()
        CType(Me.spnUserPoints, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabOptions_Page6.ResumeLayout(False)
        Me.fraListCat.ResumeLayout(False)
        Me.fraSeqName.ResumeLayout(False)
        Me.fraSeqName.PerformLayout()
        Me.panDwelCoun.ResumeLayout(False)
        Me.panDwelCoun.PerformLayout()
        CType(Me.spnDwelCoun, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraFreeMem.ResumeLayout(False)
        Me.tabOptions_Page7.ResumeLayout(False)
        Me.fraOutLowPassFilt.ResumeLayout(False)
        Me.fraOutLoadImpe.ResumeLayout(False)
        Me.fraPanelConfig.ResumeLayout(False)
        CType(Me.pctMode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picLeds, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabOptions_Page8.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

End Sub

#End Region

    '=========================================================

    Dim bHaveFocusPoints As Boolean
    Dim sConfigFilePath As String
    Dim bDoNotTalkStore As Boolean
    Dim bTimerEnable As Boolean

    Const PANEL_SAVED_CONFIGURATION As Short = 1
    Const PRE_SEGMENT_LOAD_COMMANDS As Short = 2
    Const SEGMENT_LIST As Short = 3
    Const POST_SEGMENT_LOAD_COMMANDS As Short = 4
    Const END_ARB_CONFIG As Short = 5

    Public ControlList As New System.Collections.Generic.List(Of System.Windows.Forms.Control)

    Private Sub cboAgileFskSour_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboAgileFskSour.SelectedIndexChanged
        SetSend(FREQ_FSK_SOUR, cboAgileFskSour.Text)
    End Sub

    Private Sub cboAgileSweepAdvSour_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboAgileSweepAdvSour.SelectedIndexChanged
        SetSend(TRIG_SWE_SOUR, cboAgileSweepAdvSour.Text)
    End Sub

    Private Sub cboAgileSweepDir_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboAgileSweepDir.SelectedIndexChanged
        SetSend(SWE_DIR, cboAgileSweepDir.Text)
    End Sub

    Private Sub cboAgileSweepSpac_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboAgileSweepSpac.SelectedIndexChanged
        SetSend(SWE_SPAC, cboAgileSweepSpac.Text)
        AdjustAgileLimits()
    End Sub

    Private Sub cboAgileSweepTrigSour_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboAgileSweepTrigSour.SelectedIndexChanged
        SetSend(ARM_SWE_SOUR, cboAgileSweepTrigSour.Text)
    End Sub

    Private Sub cboMarkEclt0Feed_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboMarkEclt0Feed.SelectedIndexChanged
        SetSend(MARK_ECLT0_FEED, cboMarkEclt0Feed.Text)
        ProcessMarker()
    End Sub

    Private Sub cboMarkEclt1Feed_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboMarkEclt1Feed.SelectedIndexChanged
        SetSend(MARK_ECLT1_FEED, cboMarkEclt1Feed.Text)
        ProcessMarker()
    End Sub

    Private Sub cboMarkPol_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboMarkPol.SelectedIndexChanged
        SetSend(MARK_POL, cboMarkPol.Text)
    End Sub

    Private Sub cboMarkFeed_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboMarkFeed.SelectedIndexChanged
        SetSend(MARK_FEED, cboMarkFeed.Text)
        ProcessMarker()
    End Sub

    Private Sub cboArmLay2Slop_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboArmLay2Slop.SelectedIndexChanged
        '    Select Case cboArmLay2Slop.Text
        '        Case "Negative": SetCur$(ARM_LAY2_SLOP) = "NEG"
        '        Case "Positive": SetCur$(ARM_LAY2_SLOP) = "POS"
        '        Case Else: MsgBox "Run time error.  Notify maintenance.", 16
        '    End Select
        '    SendCommand ARM_LAY2_SLOP
        SetSend(ARM_LAY2_SLOP, cboArmLay2Slop.Text)
    End Sub

    Private Sub cboArmLay2Sour_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboArmLay2Sour.SelectedIndexChanged

        If cboArmLay2Sour.Text = "External" Then
            lblArmLay2Slop.Visible = True
            cboArmLay2Slop.Visible = True
        Else
            lblArmLay2Slop.Visible = False
            cboArmLay2Slop.Visible = False
        End If
        SetSend(ARM_LAY2_SOUR, cboArmLay2Sour.Text)
    End Sub

    Public Sub cboRoscSour_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboRoscSour.SelectedIndexChanged
        Select Case cboRoscSour.Text
            Case "Clk10"
                RefOscFreq = 10000000
                panRoscFreqExt.Enabled = False
                txtRoscFreqExt.Enabled = False
                spnRoscFreqExt.Visible = False
                lblRoscFreqExt.Text = "Reference Oscillator Freq"
            Case "External", "ECLTrg0", "ECLTrg1"
                RefOscFreq = Val(SetCur(ROSC_FREQ_EXT))
                panRoscFreqExt.Enabled = True
                txtRoscFreqExt.Enabled = True
                spnRoscFreqExt.Visible = True
                SetSend(ROSC_FREQ_EXT, SetCur(ROSC_FREQ_EXT))
                lblRoscFreqExt.Text = "External Oscillator Freq"
            Case "Internal1"
                RefOscFreq = 42949672.96
                panRoscFreqExt.Enabled = False
                txtRoscFreqExt.Enabled = False
                spnRoscFreqExt.Visible = False
                lblRoscFreqExt.Text = "Reference Oscillator Freq"
            Case "Internal2"
                RefOscFreq = 40000000
                panRoscFreqExt.Enabled = False
                txtRoscFreqExt.Enabled = False
                spnRoscFreqExt.Visible = False
                lblRoscFreqExt.Text = "Reference Oscillator Freq"

            Case Else
                MsgBox("Unrecognized ROSC SOUR.  Notify maintenance.", MsgBoxStyle.Critical)
        End Select
        lblRoscFreqExt.Text = cboRoscSour.Text & " Oscillator Freq"
        txtRoscFreqExt.Text = EngNotate(RefOscFreq, ROSC_FREQ_EXT)
        AdjustFreqLimits()
        SetSend(ROSC_SOUR, cboRoscSour.Text)
    End Sub

    Private Sub cboTrigSlop_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboTrigSlop.SelectedIndexChanged
        '    Select Case cboTrigSlop.Text
        '        Case "Negative": SetCur$(TRIG_SLOP) = "NEG"
        '        Case "Positive": SetCur$(TRIG_SLOP) = "POS"
        '        Case Else: MsgBox "Run time error.  Notify maintenance.", 16
        '    End Select
        '    SendCommand TRIG_SLOP
        SetSend(TRIG_SLOP, cboTrigSlop.Text)
    End Sub

    Public Sub cboTrigSour_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboTrigSour.SelectedIndexChanged
        Select Case cboTrigSour.Text
            Case "Internal1"
                'Freq. Gen 1
                CurFreqIdx = FREQ
                lblTrigSlop.Visible = False
                cboTrigSlop.Visible = False
                If optFreqModeFix.Checked Then fraFreq.Visible = True
                fraRoscSour.Visible = True
            Case "Internal2"
                'Freq. Gen 2
                CurFreqIdx = FREQ2
                lblTrigSlop.Visible = False
                cboTrigSlop.Visible = False
                If optFreqModeFix.Checked Then fraFreq.Visible = True
                fraRoscSour.Visible = True
            Case "External"
                'Ref/Smpl In
                lblTrigSlop.Visible = True
                cboTrigSlop.Visible = True
                fraFreq.Visible = False
                fraRoscSour.Visible = False
            Case "Bus", "ECLTrg0", "ECLTrg1", "Hold", "TTLTrg0", "TTLTrg1", "TTLTrg2", "TTLTrg3", "TTLTrg4", "TTLTrg5", "TTLTrg6", "TTLTrg7"
                CurFreqIdx = ROSC_FREQ_EXT
                lblTrigSlop.Visible = False
                cboTrigSlop.Visible = False
                fraFreq.Visible = False
                fraRoscSour.Visible = False

            Case Else
                MsgBox("Run time error.  Notify maintenance.", MsgBoxStyle.Critical)
        End Select
        If cboTrigSour.Text = "Internal1" Then
            optFreqModeSwe.Enabled = True
            optFreqModeFsk.Enabled = True
        Else
            optFreqModeFix.Checked = True
            optFreqModeFix_Click(True)
            optFreqModeSwe.Enabled = False
            optFreqModeFsk.Enabled = False
        End If

        AdjustFreqLimits()
        txtFreq.Text = EngNotate(SetCur(CurFreqIdx), CurFreqIdx)
        SetSend(TRIG_SOUR, cboTrigSour.Text)
    End Sub

    Private Sub cboTrigStopSlop_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboTrigStopSlop.SelectedIndexChanged
        '    Select Case cboTrigStopSlop.Text
        '        Case "Negative": SetCur$(TRIG_STOP_SLOP) = "NEG"
        '        Case "Positive": SetCur$(TRIG_STOP_SLOP) = "POS"
        '        Case Else: MsgBox "Run time error.  Notify maintenance.", 16
        '    End Select
        '    SendCommand TRIG_STOP_SLOP
        SetSend(TRIG_STOP_SLOP, cboTrigStopSlop.Text)
    End Sub

    Private Sub cboTrigStopSour_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboTrigStopSour.SelectedIndexChanged
        If cboTrigStopSour.Text = "External" Then
            lblTrigStopSlop.Visible = True
            cboTrigStopSlop.Visible = True
        Else
            lblTrigStopSlop.Visible = False
            cboTrigStopSlop.Visible = False
        End If

        SetSend(TRIG_STOP_SOUR, cboTrigStopSour.Text)
    End Sub


    Private Sub cboTrigGatePol_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboTrigGatePol.SelectedIndexChanged
        SetSend(TRIG_GATE_POL, cboTrigGatePol.Text)
    End Sub

    Private Sub cboTrigGateSour_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboTrigGateSour.SelectedIndexChanged
        cboTrigGateSour_Click()
    End Sub

    Public Sub cboTrigGateSour_Click()
        If cboTrigGateSour.Text = "External" Then
            lblTrigGatePol.Visible = True
            cboTrigGatePol.Visible = True
        Else
            lblTrigGatePol.Visible = False
            cboTrigGatePol.Visible = False
        End If
        SetSend(TRIG_GATE_SOUR, cboTrigGateSour.Text)
    End Sub

    Private Sub cboUnits_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboUnits.SelectedIndexChanged
        SetCur(VOLT_UNIT) = cboUnits.Text
        SetUOM(VOLT) = cboUnits.Text
        SetUOM(VOLT_DIV_7) = cboUnits.Text
        AdjustVoltLimits(VOLT_UNIT)
        SendCommand(VOLT_UNIT)
        If cboUnits.Text = "W" Then
            optLoadImpHigh.Enabled = False
        Else
            optLoadImpHigh.Enabled = True
        End If

        ' Removed, V1.4
        '    If cboUnits <> "V" Then
        '        SetDef$(VOLT) = "0"
        '    End If
    End Sub

    Private Sub cboWavePol_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboWavePol.SelectedIndexChanged
        SetSend(RAMP_POL, cboWavePol.Text)
    End Sub

    Public Sub chkFilterStateOn_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkFilterStateOn.CheckedChanged
        chkFilterStateOn_Click(chkFilterStateOn.Checked)
    End Sub

    Public Sub chkFilterStateOn_Click(ByVal Value As Short)
        If Value = True Then
            SetCur(OUTP_FILT) = "ON"
            Plot1.LineStyle = NationalInstruments.UI.LineStyle.Solid
        Else
            SetCur(OUTP_FILT) = "OFF"
            Plot1.LineStyle = NationalInstruments.UI.LineStyle.Dash
        End If
        SendCommand(OUTP_FILT)
    End Sub

    Public Sub chkMarkEclt0_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMarkEclt0.CheckedChanged
        chkMarkEclt0_Click(chkMarkEclt0.Checked)
    End Sub

    Public Sub chkMarkEclt0_Click(ByVal Value As Short)
        If Value = True Then
            SetSend(MARK_ECLT0, "ON")
        Else
            SetSend(MARK_ECLT0, "OFF")
        End If
    End Sub

    Public Sub chkMarkEclt1_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMarkEclt1.CheckedChanged
        chkMarkEclt1_Click(chkMarkEclt1.Checked)
    End Sub

    Public Sub chkMarkEclt1_Click(ByVal Value As Short)
        If Value = True Then
            SetSend(MARK_ECLT1, "ON")
        Else
            SetSend(MARK_ECLT1, "OFF")
        End If
    End Sub

    Public Sub chkMark_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMark.CheckedChanged
        chkMark_Click(chkMark.Checked)
    End Sub

    Public Sub chkMark_Click(ByVal Value As Short)
        If Value = True Then
            SetSend(MARK, "ON")
        Else
            SetSend(MARK, "OFF")
        End If
    End Sub

    Public Sub chkOutpStat_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkOutpStat.CheckedChanged
        chkOutpStat_Click(chkOutpStat.Checked)
    End Sub
    Public Sub chkOutpStat_Click(ByVal Value As Boolean)
        If Value = True Then
            If Initialized Or SetCur(FUNC) = "DC" Then
                imgWaveDisplay.Image = imlOutRelayWave.Images(WaveIdx)
            End If
            SetSend(OUTP, "ON")
            If SetCur(FUNC) = "USER" Then
                imgWaveDisplay.Image = imlOutRelayWave.Images(WaveIdx)
                SendCommand(INIT)
            End If
        Else
            imgWaveDisplay.Image = imlOutRelayWave.Images(0)
            SetSend(OUTP, "OFF")
        End If

    End Sub

    Public Sub chkTrigGateStat_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTrigGateStat.CheckedChanged
        chkTrigGateStat_Click(chkTrigGateStat.Checked)
    End Sub

    Public Sub chkTrigGateStat_Click(ByVal Value As Short)
        If Value = True Then
            lblTrigGateSour.Visible = True
            cboTrigGateSour.Visible = True
            cboTrigGateSour_Click()
            SetSend(TRIG_GATE_STAT, "ON")
        Else
            lblTrigGateSour.Visible = False
            cboTrigGateSour.Visible = False
            lblTrigGatePol.Visible = False
            cboTrigGatePol.Visible = False
            SetSend(TRIG_GATE_STAT, "OFF")
        End If
    End Sub

    Private Sub cmdAbout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAbout.Click
        frmAbout.cmdOK.Visible = True
        frmAbout.ShowDialog()
    End Sub

    Private Sub cmdArmCounInf_1_MouseClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdArmCounInf_1.Click
        SetSend(ARM_COUN, "INF")
        txtArmCoun.Text = "INF"
    End Sub

    Private Sub cmdArmCounInf_2_MouseClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdArmCounInf_2.Click
        SetSend(ARM_LAY2_COUN, "INF")
        txtArmLay2Coun.Text = "INF"
    End Sub

    Private Sub cmdArmLay2_ClickEvent(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdArmLay2.Click
        SendCommand(ARM_LAY2)
    End Sub

    Public Function Build_Atlas() As String
        Build_Atlas = ""
        Dim sTestString As String = ""

        Select Case tolFuncWave.Buttons(WaveIdx - 1).Name
            Case "Sine"
                sTestString = "APPLY, AC SIGNAL," & vbCrLf & "VOLTAGE-PP " & txtAmpl.Text & "," & vbCrLf & "FREQ " & txtFreq.Text & "," & vbCrLf

                'dc-offset
                If Not (txtDcOffs.Text = "0 V") Then
                    sTestString &= "DC-OFFSET " & txtDcOffs.Text & "," & vbCrLf
                End If

                'bandwidth
                sTestString &= BuildBandwidth()

                'test impedance
                sTestString &= BuildTestEqImp()

                'SyncToEvent
                sTestString &= BuildSyncToEvent()

                sTestString &= "CNX VIA $"

            Case "Square"
                sTestString = "APPLY, SQUARE WAVE," & vbCrLf & "VOLTAGE-PP " & txtAmpl.Text & "," & vbCrLf & "FREQ " & txtFreq.Text & "," & vbCrLf

                'dc-offset
                If Not (txtDcOffs.Text = "0 V") Then
                    sTestString &= "DC-OFFSET " & txtDcOffs.Text & "," & vbCrLf
                End If

                'event slope
                If cboWavePol.Text = "Normal" Then
                    sTestString &= "EVENT-SLOPE POS," & vbCrLf

                ElseIf cboWavePol.Text = "Inverted" Then
                    sTestString &= "EVENT-SLOPE NEG," & vbCrLf
                End If

                'bandwidth
                sTestString &= BuildBandwidth()

                'test impedance
                sTestString &= BuildTestEqImp()

                'SyncToEvent
                sTestString &= BuildSyncToEvent()

                sTestString &= "CNX VIA $"

            Case "Ramp"
                sTestString = "APPLY, RAMP SIGNAL," & vbCrLf & "VOLTAGE-PP " & txtAmpl.Text & "," & vbCrLf & "FREQ " & txtFreq.Text & "," & vbCrLf

                'dc-offset
                If Not (txtDcOffs.Text = "0 V") Then
                    sTestString &= "DC-OFFSET " & txtDcOffs.Text & "," & vbCrLf
                End If

                'event slope
                If cboWavePol.Text = "Normal" Then
                    sTestString &= "EVENT-SLOPE POS," & vbCrLf

                ElseIf cboWavePol.Text = "Inverted" Then
                    sTestString &= "EVENT-SLOPE NEG," & vbCrLf
                End If

                'bandwidth
                sTestString &= BuildBandwidth()

                'test impedance
                sTestString &= BuildTestEqImp()

                'SyncToEvent
                sTestString &= BuildSyncToEvent()

                sTestString &= "CNX VIA $"

            Case "Triangle"
                sTestString = "APPLY, TRIANGULAR WAVE SIGNAL," & vbCrLf & "VOLTAGE-PP " & txtAmpl.Text & "," & vbCrLf & "FREQ " & txtFreq.Text & "," & vbCrLf
                'dc-offset
                If Not (txtDcOffs.Text = "0 V") Then
                    sTestString &= "DC-OFFSET " & txtDcOffs.Text & "," & vbCrLf
                End If

                'event slope
                If cboWavePol.Text = "Normal" Then
                    sTestString &= "EVENT-SLOPE POS," & vbCrLf

                ElseIf cboWavePol.Text = "Inverted" Then
                    sTestString &= "EVENT-SLOPE NEG," & vbCrLf
                End If

                'bandwidth
                sTestString &= BuildBandwidth()

                'test impedance
                sTestString &= BuildTestEqImp()

                'SyncToEvent
                sTestString &= BuildSyncToEvent()

                sTestString &= "CNX VIA $"

            Case "User"
                'the same thing as waveform
                sTestString = "APPLY, WAVEFORM," & vbCrLf & "STIM " & "<lable> (" & txtUserPoints.Text & ")" & "," & vbCrLf & "SAMPLE-SPACING " & "<dec-value>" & " SEC," & vbCrLf

                'test impedance
                sTestString &= BuildTestEqImp()

                'dc-offset
                If Not (txtDcOffs.Text = "0 V") Then
                    sTestString &= "DC-OFFSET " & txtDcOffs.Text & "," & vbCrLf
                End If

                'bandwidth
                sTestString &= BuildBandwidth()

                'SyncToEvent
                sTestString &= BuildSyncToEvent()

                sTestString &= "CNX VIA $"

            Case "DC"
                sTestString = "APPLY, DC SIGNAL," & vbCrLf & "VOLTAGE " & txtAmpl.Text & "," & vbCrLf

                'test impedance
                sTestString &= BuildTestEqImp()

                sTestString &= "CNX VIA $"
        End Select

        '   Return ATLAS Statement
        Build_Atlas = sTestString
    End Function

    Private Function BuildBandwidth() As String
        BuildBandwidth = ""
        Dim sTestString As String = ""

        If chkFilterStateOn.Checked = True Then
            sTestString = "BANDWIDTH "
            If optCutOff250.Checked = True Then
                sTestString &= "250 KHz" & vbCrLf
            Else
                sTestString &= "10 MHz" & vbCrLf
            End If
        End If

        BuildBandwidth = sTestString
    End Function

    Private Function BuildTestEqImp() As String
        BuildTestEqImp = ""
        Dim sTestString As String

        sTestString = "TEST-EQUIP-IMP "
        If optLoadImp50.Checked = True Then
            sTestString &= "50 Ohm" & vbCrLf

        ElseIf optLoadImp75.Checked = True Then
            sTestString &= "75 Ohm" & vbCrLf

        Else
            sTestString &= "1 MOhm" & vbCrLf
        End If

        BuildTestEqImp = sTestString
    End Function

    Private Function BuildSyncToEvent() As String
        BuildSyncToEvent = ""
        Dim sTestString As String = ""

        If Not (cboArmLay2Sour.Text = "Immediate") Then
            sTestString = "SYNC TO EVENT " & "<event>" & " MAX-TIME " & "<dec value>" & " SEC," & vbCrLf
        End If
        BuildSyncToEvent = sTestString
    End Function

    Public Sub SetMode(ByRef sMode As String)
        Dim btn As System.Windows.Forms.ToolBarButton

        'remove the tag name
        sMode = Strings.Right(sMode, Len(sMode) - InStr(sMode, "="))

        For Each btn In Me.tolFuncWave.Buttons
            btn.Pushed = False
        Next

        Select Case sMode
            Case "SIN"
                Me.tolFuncWave.Buttons("Sine").Pushed = True
                tolFuncWave_ButtonClick(Me, New ToolBarButtonClickEventArgs(tolFuncWave.Buttons("Sine")))

            Case "SQU"
                Me.tolFuncWave.Buttons("Square").Pushed = True
                tolFuncWave_ButtonClick(Me, New ToolBarButtonClickEventArgs(tolFuncWave.Buttons("Square")))

            Case "RAMP"
                Me.tolFuncWave.Buttons("Ramp").Pushed = True
                tolFuncWave_ButtonClick(Me, New ToolBarButtonClickEventArgs(tolFuncWave.Buttons("RAMP")))

            Case "TRI"
                Me.tolFuncWave.Buttons("Triangle").Pushed = True
                tolFuncWave_ButtonClick(Me, New ToolBarButtonClickEventArgs(tolFuncWave.Buttons("Triangle")))

            Case "DC"
                Me.tolFuncWave.Buttons("btnDC").Pushed = True
                tolFuncWave_ButtonClick(Me, New ToolBarButtonClickEventArgs(tolFuncWave.Buttons("DC")))

            Case "USER"
                Me.tolFuncWave.Buttons("Sinc").Pushed = True
                tolFuncWave_ButtonClick(Me, New ToolBarButtonClickEventArgs(tolFuncWave.Buttons("User")))
        End Select
    End Sub

    Public Function GetMode() As String
        GetMode = SetCur(FUNC)
    End Function

    Public Sub ConfigGetCurrent()
        Const curOnErrorGoToLabel_Default As Integer = 0
        Const curOnErrorGoToLabel_ErrorHandle As Integer = 1
        Dim vOnErrorGoToLabel As Integer = curOnErrorGoToLabel_Default
        Dim sInstrumentCmds As String
        Dim sReadBuffer As String
        Dim sRemOscSour As String 'Remote Oscillator Source
        Dim bWavePoint As Boolean
        Dim Msg As String 'Used in error messages

        Try
            If LiveMode Then 'use Not to use simulator
                sReadBuffer = ""
                'Allow use of ActivateControl
                sTipMode = "GET CURR CONFIG"

                '       Items to Query
                '       Reference ChangeInstrumentMode
                '       Reference TakeMeasurement Sub

                vOnErrorGoToLabel = curOnErrorGoToLabel_ErrorHandle ' On Error GoTo ErrorHandle
                '       *********
                '       Get MODE
                '       *********
                sInstrumentCmds = "FUNC?"
                WriteInstrument(sInstrumentCmds)

                '##########################################
                'Action Required: Remove debug code
                'Fill return value selections
                ' '         '         gcolCmds..Add "Simulated response for: " & sInstrumentCmds
                ' '         '         gcolCmds..Add "SIN"
                ' '         '         gcolCmds..Add "SQU"
                ' '         '         gcolCmds..Add "RAMP"
                ' '         '         gcolCmds..Add "TRI"
                ' '         '         gcolCmds..Add "DC"
                ' '         '         gcolCmds..Add "USER"
                '##########################################

                'Retrieve instrument output buffer
                sReadBuffer = ReadARB()

                ArbMain.bActivateControl(":FUNC " & sReadBuffer)


                '       *************
                '       Get Output Relay
                '       *************
                sInstrumentCmds = "OUTP?"
                WriteInstrument(sInstrumentCmds)

                '##########################################
                'Action Required: Remove debug code
                'Fill return value selections
                ' '         '         gcolCmds..Add "Simulated response for: " & sInstrumentCmds
                ' '         '         gcolCmds..Add "0"
                ' '         '         gcolCmds..Add "1"
                '##########################################

                'Retrieve instrument output buffer
                sReadBuffer = ReadARB()

                If sReadBuffer = "1" Then
                    ArbMain.bActivateControl(":OUTP ON")
                Else
                    ArbMain.bActivateControl(":OUTP OFF")
                End If

                '*************
                'Get Amplitude Units
                '*************
                sInstrumentCmds = "VOLT:UNIT?"
                WriteInstrument(sInstrumentCmds)

                '##########################################
                'Action Required: Remove debug code
                'Fill return value selections
                ' '         '         gcolCmds..Add "Simulated response for: " & sInstrumentCmds
                ' '         '         gcolCmds..Add "V"
                ' '         '         gcolCmds..Add "VPK"
                ' '         '         gcolCmds..Add "VPP"
                ' '         '         gcolCmds..Add "VRMS"
                ' '         '         gcolCmds..Add "W"
                '##########################################

                'Retrieve instrument output buffer
                sReadBuffer = ReadARB()

                ArbMain.bActivateControl(":VOLT:UNIT " & sReadBuffer)

                '*************
                'Get Amplitude
                '*************
                sInstrumentCmds = "VOLT?"
                WriteInstrument(sInstrumentCmds)

                '##########################################
                'Action Required: Remove debug code
                'Fill return value selections
                '         '         gcolCmds..Add "Simulated response for: " & sInstrumentCmds
                '         '         gcolCmds..Add "5.11875"
                '         '         gcolCmds..Add "3.00000"
                '         '         gcolCmds..Add "2.00000"
                '         '         gcolCmds..Add "1.00000"
                '         '         gcolCmds..Add "0.50000"
                '         '         gcolCmds..Add "0.16187"
                '##########################################

                'Retrieve instrument output buffer
                sReadBuffer = ReadARB()

                ArbMain.bActivateControl(":VOLT " & sReadBuffer)

                '************************************************************************
                If WaveIdx <> 5 Then ' DC Does not need these

                    '*************
                    'Get DC Offset
                    '*************
                    sInstrumentCmds = "VOLT:OFFS?"
                    WriteInstrument(sInstrumentCmds)

                    '##########################################
                    'Action Required: Remove debug code
                    'Fill return value selections
                    ' '         '         gcolCmds..Add "Simulated response for: " & sInstrumentCmds
                    ' '         '         gcolCmds..Add "1.02250"
                    ' '         '         gcolCmds..Add "0.50000"
                    ' '         '         gcolCmds..Add "0.09000"
                    ' '         '         gcolCmds..Add "0.00300"
                    ' '         '         gcolCmds..Add "0.00000"
                    ' '         '         gcolCmds..Add "-0.00100"
                    ' '         '         gcolCmds..Add "-0.06000"
                    ' '         '         gcolCmds..Add "-0.40000"
                    ' '         '         gcolCmds..Add "-1.02250"
                    '##########################################

                    'Retrieve instrument output buffer
                    sReadBuffer = ReadARB()

                    ArbMain.bActivateControl(":VOLT:OFFS " & sReadBuffer)

                    If WaveIdx <> 1 Then ' SIN Does not need this

                        '*************
                        'Get Polarity
                        '*************
                        sInstrumentCmds = "RAMP:POL?"
                        WriteInstrument(sInstrumentCmds)

                        '##########################################
                        'Action Required: Remove debug code
                        'Fill return value selections
                        ' '         '         gcolCmds..Add "Simulated response for: " & sInstrumentCmds
                        ' '         '         gcolCmds..Add "INV"
                        ' '         '         gcolCmds..Add "NORM"
                        '##########################################

                        'Retrieve instrument output buffer
                        sReadBuffer = ReadARB()

                        ArbMain.bActivateControl(":RAMP:POL " & sReadBuffer)

                    End If

                    If WaveIdx = 3 Or WaveIdx = 4 Then ' RAMP or TRI

                        '*************
                        'Get Points
                        '*************
                        sInstrumentCmds = "RAMP:POIN?"
                        WriteInstrument(sInstrumentCmds)

                        '##########################################
                        'Action Required: Remove debug code
                        'Fill return value selections
                        ' '         '         gcolCmds..Add "Simulated response for: " & sInstrumentCmds
                        ' '         '         gcolCmds..Add "4"
                        ' '         '         gcolCmds..Add "16"
                        ' '         '         gcolCmds..Add "64"
                        ' '         '         gcolCmds..Add "224"
                        ' '         '         gcolCmds..Add "6128"
                        ' '         '         gcolCmds..Add "262144"
                        '##########################################

                        'Retrieve instrument output buffer
                        sReadBuffer = ReadARB()

                        ArbMain.bActivateControl(":RAMP:POIN " & sReadBuffer)

                    End If

                    '*************
                    'Get Frequency
                    '*************
                    sInstrumentCmds = "FREQ?"
                    WriteInstrument(sInstrumentCmds)

                    '##########################################
                    'Action Required: Remove debug code
                    'Fill return value selections
                    ' '         '         gcolCmds..Add "Simulated response for: " & sInstrumentCmds
                    ' '         '         gcolCmds..Add "0"
                    ' '         '         gcolCmds..Add "0.12320000"
                    ' '         '         gcolCmds..Add "0.56450000"
                    ' '         '         gcolCmds..Add "2"
                    ' '         '         gcolCmds..Add "20"
                    ' '         '         gcolCmds..Add "500"
                    ' '         '         gcolCmds..Add "2E3"
                    ' '         '         gcolCmds..Add "30E3"
                    ' '         '         gcolCmds..Add "500E3"
                    ' '         '         gcolCmds..Add "2E6"
                    ' '         '         gcolCmds..Add "2147483648"
                    '##########################################

                    'Retrieve instrument output buffer
                    sReadBuffer = ReadARB()

                    ArbMain.bActivateControl(":FREQ " & sReadBuffer)

                    '*************
                    'Get Reference Oscillator Source
                    '*************
                    sInstrumentCmds = "ROSC:SOUR?"
                    WriteInstrument(sInstrumentCmds)

                    '##########################################
                    'Action Required: Remove debug code
                    'Fill return value selections
                    ' '         '         gcolCmds..Add "Simulated response for: " & sInstrumentCmds
                    ' '         '         gcolCmds..Add "CLK10"
                    ' '         '         gcolCmds..Add "INT"
                    ' '         '         gcolCmds..Add "INT2"
                    ' '         '         gcolCmds..Add "EXT"
                    ' '         '         gcolCmds..Add "ECLT0"
                    ' '         '         gcolCmds..Add "ECLT1"
                    '##########################################

                    'Retrieve instrument output buffer
                    sReadBuffer = ReadARB()
                    sRemOscSour = sReadBuffer

                    ArbMain.bActivateControl(":ROSC:SOUR " & sRemOscSour)

                    If sRemOscSour = "EXT" Or InStr(sRemOscSour, "ECLT") Then
                        '*************
                        'Get Reference Oscillator Frequency
                        '*************
                        sInstrumentCmds = "ROSC:FREQ:EXT?"
                        WriteInstrument(sInstrumentCmds)

                        '##########################################
                        'Action Required: Remove debug code
                        'Fill return value selections
                        ' '         '         gcolCmds..Add "Simulated response for: " & sInstrumentCmds
                        ' '         '         gcolCmds..Add "1"
                        ' '         '         gcolCmds..Add "50"
                        ' '         '         gcolCmds..Add "700"
                        ' '         '         gcolCmds..Add "6E3"
                        ' '         '         gcolCmds..Add "90E3"
                        ' '         '         gcolCmds..Add "100E3"
                        ' '         '         gcolCmds..Add "40E6"
                        '##########################################

                        'Retrieve instrument output buffer
                        sReadBuffer = ReadARB()

                        ArbMain.bActivateControl(":ROSC:FREQ:EXT " & sReadBuffer)

                    End If

                    ' NOTE: INIT and ABOR SCPI commands can NOT be queried
                    ' Read Operation Status Register to get status
                    'status register bit0=cal, bit3=sweep, bit6=waitARM, bit8=init

                    sInstrumentCmds = "STAT:OPER:COND?"
                    WriteInstrument(sInstrumentCmds)

                    '##########################################
                    'Action Required: Remove debug code
                    'Fill return value selections
                    ' '         '         gcolCmds..Add "Simulated response for: " & sInstrumentCmds
                    ' '         '         gcolCmds..Add "0"
                    ' '         '         gcolCmds..Add "128"
                    ' '         '         gcolCmds..Add "329"
                    '##########################################

                    'Retrieve instrument output buffer
                    sReadBuffer = ReadARB()

                    ArbMain.bActivateControl(":STAT:OPER:COND? " & sReadBuffer)


                End If
                '**************************Aim-Trig******************************
                If WaveIdx <> 5 Then ' DC Does not need these
                    'ARM
                    ConfigGetArm()

                    If WaveIdx <> 1 Then ' SIN Does not need this
                        ConfigGetTrig("") '[STARt]
                    End If

                    ConfigGetTrig(":GATE")
                    ConfigGetTrig(":STOP")

                    '************************Markers****************************
                    bWavePoint = False 'If a marker is set to LIST then it returns True

                    If ConfigGetMarker("") Then
                        bWavePoint = True
                    End If
                    If ConfigGetMarker(":ECLT0") Then
                        bWavePoint = True
                    End If
                    If ConfigGetMarker(":ECLT1") Then
                        bWavePoint = True
                    End If

                    'Waveform Point
                    If bWavePoint Then
                        ConfigGetWavePoint()
                    End If

                    '*********************Freq. Agility*************************
                    If WaveIdx <> 6 Then ' User Does not use these
                        '*************
                        'Get Frequency Mode
                        '*************
                        sInstrumentCmds = "FREQ:MODE?"
                        WriteInstrument(sInstrumentCmds)

                        '##########################################
                        'Action Required: Remove debug code
                        'Fill return value selections
                        ' '         '         gcolCmds..Add "Simulated response for: " & sInstrumentCmds
                        ' '         '         gcolCmds..Add "FIX"
                        ' '         '         gcolCmds..Add "FSK"
                        ' '         '         gcolCmds..Add "SWE"
                        '##########################################

                        'Retrieve instrument output buffer
                        sReadBuffer = ReadARB()

                        ArbMain.bActivateControl(":FREQ:MODE " & sReadBuffer)

                        'Get additional data if needed
                        If sReadBuffer = "FSK" Then
                            ReadCurFSKey()
                        ElseIf sReadBuffer = "SWE" Then
                            ReadCurSweep()
                        End If
                    End If
                    '**************************User*****************************
                    'TBD
                    '***********************Sequence****************************
                    'TBD

                End If
                '***************************Options******************************
                '*************
                'Get Output Load
                '*************
                sInstrumentCmds = "OUTP:LOAD?"
                WriteInstrument(sInstrumentCmds)

                '##########################################
                'Action Required: Remove debug code
                'Fill return value selections
                '         '         gcolCmds..Add "Simulated response for: " & sInstrumentCmds
                '         '         gcolCmds..Add "50"
                '         '         gcolCmds..Add "75"
                '         '         gcolCmds..Add "9.9E+37"
                '##########################################

                'Retrieve instrument output buffer
                sReadBuffer = ReadARB()

                If Val(sReadBuffer) = 9.9E+37 Then
                    sReadBuffer = "INF"
                End If

                ArbMain.bActivateControl(":OUTP:LOAD " & sReadBuffer)

                '*************
                'Get Output Filter Frequency
                '*************
                sInstrumentCmds = "OUTP:FILT:FREQ?"
                WriteInstrument(sInstrumentCmds)

                '##########################################
                'Action Required: Remove debug code
                'Fill return value selections
                '         '         gcolCmds..Add "Simulated response for: " & sInstrumentCmds
                '         '         gcolCmds..Add "250E3"
                '         '         gcolCmds..Add "10E6"
                '##########################################

                'Retrieve instrument output buffer
                sReadBuffer = ReadARB()

                If IsNumeric(sReadBuffer) Then
                    sReadBuffer = EngNotate(sReadBuffer, FREQ)
                End If

                ArbMain.bActivateControl(":OUTP:FILT:FREQ " & sReadBuffer)

                '*************
                'Get Output Filter Enabled
                '*************
                sInstrumentCmds = "OUTP:FILT?"
                WriteInstrument(sInstrumentCmds)

                '##########################################
                'Action Required: Remove debug code
                'Fill return value selections
                '         '         gcolCmds..Add "Simulated response for: " & sInstrumentCmds
                '         '         gcolCmds..Add "0"
                '         '         gcolCmds..Add "1"
                '##########################################

                'Retrieve instrument output buffer
                sReadBuffer = ReadARB()

                If sReadBuffer = "1" Then
                    ArbMain.bActivateControl(":OUTP:FILT ON")
                Else
                    ArbMain.bActivateControl(":OUTP:FILT OFF")
                End If

                '****************************************************************
                'clean-up
                sTipMode = ""

            End If
        Catch ex As SystemException
            Select Case vOnErrorGoToLabel
                Case curOnErrorGoToLabel_ErrorHandle
                    If Err.Number = 13 Then
                        Msg = "Type Mismatch: Data received is not what was expected"
                        MsgBox(Msg, MsgBoxStyle.Exclamation)
                    Else
                        ' Display unanticipated error message.
                        Msg = "Unanticipated error " & Err.Number & ": " & Err.Description
                        MsgBox(Msg, MsgBoxStyle.Critical)
                        sTipMode = ""
                    End If
                Case curOnErrorGoToLabel_Default
                    ' ...
                Case Else
                    ' ...
            End Select
        End Try
    End Sub

    Private Sub ConfigGetTrig(ByVal sSeq As String)
        Dim sInstrumentCmds As String
        Dim sReadBuffer As String
        Dim sSource As String

        'Gate is slightly different
        If sSeq = ":GATE" Then
            '********************
            'Get State
            '********************
            sInstrumentCmds = "TRIG" & sSeq & ":STAT?"
            WriteInstrument(sInstrumentCmds)
            '##########################################
            'Action Required: Remove debug code
            'Fill return value selections
            '         '         gcolCmds..Add "Simulated response for: " & sInstrumentCmds
            '         '         gcolCmds..Add "0"
            '         '         gcolCmds..Add "1"
            '##########################################

            'Retrieve instrument output buffer
            sReadBuffer = ReadARB()

            If sReadBuffer = "1" Then
                ArbMain.bActivateControl(":TRIG" & sSeq & ":STAT ON")
            Else
                ArbMain.bActivateControl(":TRIG" & sSeq & ":STAT OFF")
                Exit Sub
            End If

        End If

        '   ********************
        '   Get Source
        '   ********************

        sInstrumentCmds = "TRIG" & sSeq & ":SOUR?"
        WriteInstrument(sInstrumentCmds)

        '##########################################
        'Action Required: Remove debug code
        'Fill return value selections
        '         gcolCmds..Add "Simulated response for: " & sInstrumentCmds
        '         gcolCmds..Add "INT"
        '         gcolCmds..Add "INT2"
        '         gcolCmds..Add "BUS"
        '         gcolCmds..Add "HOLD"
        '         gcolCmds..Add "ECLT0"
        '         gcolCmds..Add "ECLT1"
        '         gcolCmds..Add "EXT"
        '         gcolCmds..Add "TTLT0"
        '         gcolCmds..Add "TTLT1"
        '         gcolCmds..Add "TTLT2"
        '         gcolCmds..Add "TTLT3"
        '         gcolCmds..Add "TTLT4"
        '         gcolCmds..Add "TTLT5"
        '         gcolCmds..Add "TTLT6"
        '         gcolCmds..Add "TTLT7"
        '##########################################

        'Retrieve instrument output buffer
        sReadBuffer = ReadARB()
        sSource = Trim(sReadBuffer)

        ArbMain.bActivateControl(":TRIG" & sSeq & ":SOUR " & sSource)

        If sSource = "EXT" Then

            '********************
            'Get Slope/Polarity
            '********************

            'Gate is slightly different
            If sSeq = ":GATE" Then
                sInstrumentCmds = "TRIG" & sSeq & ":POL?"
                '##########################################
                'Action Required: Remove debug code
                'Fill return value selections
                ' '         '         gcolCmds..Add "Simulated response for: " & sInstrumentCmds
                ' '         '         gcolCmds..Add "INV"
                ' '         '         gcolCmds..Add "NORM"
                '##########################################
            Else
                sInstrumentCmds = "TRIG" & sSeq & ":SLOP?"
                '##########################################
                'Action Required: Remove debug code
                'Fill return value selections
                ' '         '         gcolCmds..Add "Simulated response for: " & sInstrumentCmds
                ' '         '         gcolCmds..Add "POS"
                ' '         '         gcolCmds..Add "NEG"
                '##########################################
            End If
            WriteInstrument(sInstrumentCmds)

            'Retrieve instrument output buffer
            sReadBuffer = ReadARB()

            'Gate is slightly different
            If sSeq = ":GATE" Then
                ArbMain.bActivateControl(":TRIG" & sSeq & ":POL " & sReadBuffer)
            Else
                ArbMain.bActivateControl(":TRIG" & sSeq & ":SLOP " & sReadBuffer)
            End If
        End If
    End Sub

    Private Sub ConfigGetArm()
        Dim sInstrumentCmds As String
        Dim sReadBuffer As String
        Dim sSource As String

        '   ********************
        '   Get Source
        '   ********************

        sInstrumentCmds = "ARM:LAY2:SOUR?"
        WriteInstrument(sInstrumentCmds)

        '##########################################
        'Action Required: Remove debug code
        'Fill return value selections
        '         gcolCmds..Add "Simulated response for: " & sInstrumentCmds
        '         gcolCmds..Add "IMM"
        '         gcolCmds..Add "EXT"
        '         gcolCmds..Add "ECLT0"
        '         gcolCmds..Add "ECLT1"
        '         gcolCmds..Add "BUS"
        '         gcolCmds..Add "HOLD"
        '         gcolCmds..Add "TTLT0"
        '         gcolCmds..Add "TTLT1"
        '         gcolCmds..Add "TTLT2"
        '         gcolCmds..Add "TTLT3"
        '         gcolCmds..Add "TTLT4"
        '         gcolCmds..Add "TTLT5"
        '         gcolCmds..Add "TTLT6"
        '         gcolCmds..Add "TTLT7"
        '##########################################

        'Retrieve instrument output buffer
        sReadBuffer = ReadARB()
        sSource = Trim(sReadBuffer)

        ArbMain.bActivateControl(":ARM:LAY2:SOUR " & sSource)

        If sSource = "EXT" Then
            '********************
            'Get Slope
            '********************

            sInstrumentCmds = "ARM:LAY2:SLOP?"
            WriteInstrument(sInstrumentCmds)

            '##########################################
            'Action Required: Remove debug code
            'Fill return value selections
            '         '         gcolCmds..Add "Simulated response for: " & sInstrumentCmds
            '         '         gcolCmds..Add "POS"
            '         '         gcolCmds..Add "NEG"
            '##########################################

            'Retrieve instrument output buffer
            sReadBuffer = ReadARB()

            ArbMain.bActivateControl(":ARM:LAY2:SLOP " & sReadBuffer)
        End If

        '********************
        'Get Layer2 Count
        '********************

        sInstrumentCmds = "ARM:LAY2:COUN?"
        WriteInstrument(sInstrumentCmds)

        '##########################################
        'Action Required: Remove debug code
        'Fill return value selections
        '         gcolCmds..Add "Simulated response for: " & sInstrumentCmds
        '         gcolCmds..Add "1"
        '         gcolCmds..Add "10"
        '         gcolCmds..Add "345"
        '         gcolCmds..Add "9235"
        '         gcolCmds..Add "65535"
        '         gcolCmds..Add "9.9E+37"
        '##########################################

        'Retrieve instrument output buffer
        sReadBuffer = ReadARB()

        'If sReadBuffer = "9.9E+37" Then
        If Val(sReadBuffer) = 9.9E+37 Then
            sReadBuffer = "INF"
        End If

        ArbMain.bActivateControl(":ARM:LAY2:COUN " & sReadBuffer)


        '********************
        'Get Layer1 Count
        '********************

        sInstrumentCmds = "ARM:COUN?"
        WriteInstrument(sInstrumentCmds)

        '##########################################
        'Action Required: Remove debug code
        'Fill return value selections
        '         gcolCmds..Add "Simulated response for: " & sInstrumentCmds
        '         gcolCmds..Add "1"
        '         gcolCmds..Add "10"
        '         gcolCmds..Add "345"
        '         gcolCmds..Add "9235"
        '         gcolCmds..Add "65536"
        '         gcolCmds..Add "9.9E+37"
        '##########################################

        'Retrieve instrument output buffer
        sReadBuffer = ReadARB()

        'If sReadBuffer = "9.9E+37" Then
        If Val(sReadBuffer) = 9.9E+37 Then
            sReadBuffer = "INF"
        End If

        ArbMain.bActivateControl(":ARM:COUN " & sReadBuffer)
    End Sub

    Private Function ConfigGetMarker(ByVal sSeq As String) As Boolean
        Dim sInstrumentCmds As String
        Dim sReadBuffer As String
        Dim sSource As String

        '********************
        'Get State
        '********************
        sInstrumentCmds = "MARK" & sSeq & "?"
        WriteInstrument(sInstrumentCmds)
        '##########################################
        'Action Required: Remove debug code
        'Fill return value selections
        '         gcolCmds..Add "Simulated response for: " & sInstrumentCmds
        '         gcolCmds..Add "0"
        '         gcolCmds..Add "1"
        '##########################################

        'Retrieve instrument output buffer
        sReadBuffer = ReadARB()

        If sReadBuffer = "1" Then
            ArbMain.bActivateControl(":MARK" & sSeq & " ON")
        Else
            ArbMain.bActivateControl(":MARK" & sSeq & " OFF")
            ConfigGetMarker = False
            Exit Function
        End If


        '   ********************
        '   Get Feed
        '   ********************

        sInstrumentCmds = "MARK" & sSeq & ":FEED?"
        WriteInstrument(sInstrumentCmds)

        '##########################################
        'Action Required: Remove debug code
        'Fill return value selections
        'ARM,ARM:LAY2,FREQ:CHAN,LIST,PM:DEV:CHAN,ROSC,TRIG:SEQ
        '         gcolCmds..Add "Simulated response for: " & sInstrumentCmds
        '         gcolCmds..Add "ARM"
        '         gcolCmds..Add "ARM:LAY2"
        '         gcolCmds..Add "FREQ:CHAN"
        '         gcolCmds..Add "LIST"
        '         gcolCmds..Add "PM:DEV:CHAN"
        '         gcolCmds..Add "ROSC"
        '         gcolCmds..Add "TRIG:SEQ"
        '##########################################

        'Retrieve instrument output buffer
        sReadBuffer = ReadARB()
        sReadBuffer = Strings.Right(sReadBuffer, Len(sReadBuffer) - 1)
        sReadBuffer = Strings.Left(sReadBuffer, Len(sReadBuffer) - 1)

        sSource = Trim(sReadBuffer)

        ArbMain.bActivateControl(":MARK" & sSeq & ":FEED " & sSource)

        If sSeq = "" Then
            '********************
            'Get Polarity
            '********************

            sInstrumentCmds = "MARK" & sSeq & ":POL?"
            WriteInstrument(sInstrumentCmds)

            '##########################################
            'Action Required: Remove debug code
            'Fill return value selections
            '         '         gcolCmds..Add "Simulated response for: " & sInstrumentCmds
            '         '         gcolCmds..Add "INV"
            '         '         gcolCmds..Add "NORM"
            '##########################################

            '       WriteInstrument sInstrumentCmds

            'Retrieve instrument output buffer
            sReadBuffer = ReadARB()

            ArbMain.bActivateControl(":MARK" & sSeq & ":POL " & sReadBuffer)

        End If

        'Return status of finding LIST Feed
        ConfigGetMarker = False
        If sSource = "LIST" Then
            ConfigGetMarker = True
        End If
    End Function

    Private Sub ConfigGetWavePoint()
        Dim sInstrumentCmds As String
        Dim sReadBuffer As String

        '   ********************
        '   Get Waveform Segment Name
        '   ********************
        sInstrumentCmds = "LIST:SEL?"
        WriteInstrument(sInstrumentCmds)

        '##########################################
        'Action Required: Remove debug code
        'Fill return value selections
        '         gcolCmds..Add "Simulated response for: " & sInstrumentCmds
        '         gcolCmds..Add "NONE"
        '         gcolCmds..Add "TESTFORM1"
        '         gcolCmds..Add "SAMPLEWAVE"
        '         gcolCmds..Add "GENUSERWAVE"
        '         gcolCmds..Add "USER0"
        '##########################################

        'Retrieve instrument output buffer
        sReadBuffer = ReadARB()

        ArbMain.bActivateControl(":LIST:SEL " & sReadBuffer)

        If sReadBuffer <> "" And sReadBuffer <> "NONE" Then
            '   ********************
            '   Get Single Point
            '   ********************
            sInstrumentCmds = "LIST:MARK:SPO?"
            WriteInstrument(sInstrumentCmds)

            '##########################################
            'Action Required: Remove debug code
            'Fill return value selections
            '         '         gcolCmds..Add "Simulated response for: " & sInstrumentCmds
            '         '         gcolCmds..Add "1"
            '         '         gcolCmds..Add "5"
            '         '         gcolCmds..Add "30"
            '         '         gcolCmds..Add "400"
            '         '         gcolCmds..Add "1000"
            '##########################################

            'Retrieve instrument output buffer
            sReadBuffer = ReadARB()

            ArbMain.bActivateControl(":LIST:MARK:SPO " & sReadBuffer)
        End If
    End Sub

    Private Sub ReadCurFSKey()
        Dim sInstrumentCmds As String
        Dim sReadBuffer As String

        '   ********************
        '   Get FSKey Source
        '   ********************
        sInstrumentCmds = "FREQ:FSK:SOUR?"
        WriteInstrument(sInstrumentCmds)

        '##########################################
        'Action Required: Remove debug code
        'Fill return value selections
        '         gcolCmds..Add "Simulated response for: " & sInstrumentCmds
        '         gcolCmds..Add "EXT"
        '         gcolCmds..Add "TTLT0"
        '         gcolCmds..Add "TTLT1"
        '         gcolCmds..Add "TTLT2"
        '         gcolCmds..Add "TTLT3"
        '         gcolCmds..Add "TTLT4"
        '         gcolCmds..Add "TTLT5"
        '         gcolCmds..Add "TTLT6"
        '         gcolCmds..Add "TTLT7"
        '##########################################

        'Retrieve instrument output buffer
        sReadBuffer = ReadARB()

        ArbMain.bActivateControl(":FREQ:FSK:SOUR " & sReadBuffer)

        '   ********************
        '   Get FSKey Low and High Freq
        '   ********************
        sInstrumentCmds = "FREQ:FSK?"
        WriteInstrument(sInstrumentCmds)

        '##########################################
        'Action Required: Remove debug code
        'Fill return value selections
        '         gcolCmds..Add "Simulated response for: " & sInstrumentCmds
        '         gcolCmds..Add "100,20E3"
        '         gcolCmds..Add "900,9E3"
        '         gcolCmds..Add "5E3,3E6"
        '         gcolCmds..Add "24E3,100E3"
        '         gcolCmds..Add "20E3,5E6"
        '         gcolCmds..Add "0,10.737E6"
        '##########################################

        'Retrieve instrument output buffer
        sReadBuffer = ReadARB()

        ArbMain.bActivateControl(":FREQ:FSK " & sReadBuffer)
    End Sub

    Private Sub ReadCurSweep()
        Dim sInstrumentCmds As String
        Dim sReadBuffer As String

        '   ********************
        '   Get Start Frequency
        '   ********************
        sInstrumentCmds = "FREQ:STAR?"
        WriteInstrument(sInstrumentCmds)

        '##########################################
        'Action Required: Remove debug code
        'Fill return value selections
        '         gcolCmds..Add "Simulated response for: " & sInstrumentCmds
        '         gcolCmds..Add "100"
        '         gcolCmds..Add "900"
        '         gcolCmds..Add "5E3"
        '         gcolCmds..Add "24E3"
        '         gcolCmds..Add "5E6"
        '         gcolCmds..Add "10.737E6"
        '##########################################

        'Retrieve instrument output buffer
        sReadBuffer = ReadARB()

        ArbMain.bActivateControl(":FREQ:STAR " & sReadBuffer)

        '   ********************
        '   Get Stop Frequency
        '   ********************
        sInstrumentCmds = "FREQ:STOP?"
        WriteInstrument(sInstrumentCmds)

        '##########################################
        'Action Required: Remove debug code
        'Fill return value selections
        '         gcolCmds..Add "Simulated response for: " & sInstrumentCmds
        '         gcolCmds..Add "300"
        '         gcolCmds..Add "900"
        '         gcolCmds..Add "5E3"
        '         gcolCmds..Add "24E3"
        '         gcolCmds..Add "5E6"
        '         gcolCmds..Add "10.737E6"
        '##########################################

        'Retrieve instrument output buffer
        sReadBuffer = ReadARB()

        ArbMain.bActivateControl(":FREQ:STOP " & sReadBuffer)

        '   ********************
        '   Get Steps/Points
        '   ********************
        sInstrumentCmds = "SWE:POIN?"
        WriteInstrument(sInstrumentCmds)

        '##########################################
        'Action Required: Remove debug code
        'Fill return value selections
        '         gcolCmds..Add "Simulated response for: " & sInstrumentCmds
        '         gcolCmds..Add "2"
        '         gcolCmds..Add "576"
        '         gcolCmds..Add "12.3E3"
        '         gcolCmds..Add "8E6"
        '         gcolCmds..Add "14.5E6"
        '         gcolCmds..Add "1073741824"
        '##########################################

        'Retrieve instrument output buffer
        sReadBuffer = ReadARB()

        ArbMain.bActivateControl(":SWE:POIN " & sReadBuffer)

        '   ********************
        '   Get Spacing
        '   ********************
        sInstrumentCmds = "SWE:SPAC?"
        WriteInstrument(sInstrumentCmds)

        '##########################################
        'Action Required: Remove debug code
        'Fill return value selections
        '         gcolCmds..Add "Simulated response for: " & sInstrumentCmds
        '         gcolCmds..Add "LIN"
        '         gcolCmds..Add "LOG"
        '##########################################

        'Retrieve instrument output buffer
        sReadBuffer = ReadARB()

        ArbMain.bActivateControl(":SWE:SPAC " & sReadBuffer)

        '   ********************
        '   Get Duration
        '   ********************
        sInstrumentCmds = "SWE:TIME?"
        WriteInstrument(sInstrumentCmds)

        '##########################################
        'Action Required: Remove debug code
        'Fill return value selections
        '         gcolCmds..Add "Simulated response for: " & sInstrumentCmds
        '         gcolCmds..Add "0.00125"
        '         gcolCmds..Add "0.0576"
        '         gcolCmds..Add "0.123"
        '         gcolCmds..Add "0.8"
        '         gcolCmds..Add "1.45"
        '         gcolCmds..Add "4.19430375"
        '##########################################

        'Retrieve instrument output buffer
        sReadBuffer = ReadARB()

        ArbMain.bActivateControl(":SWE:TIME " & sReadBuffer)

        '   ********************
        '   Get Direction
        '   ********************
        sInstrumentCmds = "SWE:DIR?"
        WriteInstrument(sInstrumentCmds)

        '##########################################
        'Action Required: Remove debug code
        'Fill return value selections
        '         gcolCmds..Add "Simulated response for: " & sInstrumentCmds
        '         gcolCmds..Add "UP"
        '         gcolCmds..Add "DOWN"
        '##########################################

        'Retrieve instrument output buffer
        sReadBuffer = ReadARB()

        ArbMain.bActivateControl(":SWE:DIR " & sReadBuffer)

        '   ********************
        '   Get Sweep Source
        '   ********************
        sInstrumentCmds = "ARM:SWE:SOUR?"
        WriteInstrument(sInstrumentCmds)

        '##########################################
        'Action Required: Remove debug code
        'Fill return value selections
        '         gcolCmds..Add "Simulated response for: " & sInstrumentCmds
        '         gcolCmds..Add "BUS"
        '         gcolCmds..Add "HOLD"
        '         gcolCmds..Add "IMM"
        '         gcolCmds..Add "LINK"
        '         gcolCmds..Add "TTLT0"
        '         gcolCmds..Add "TTLT1"
        '         gcolCmds..Add "TTLT2"
        '         gcolCmds..Add "TTLT3"
        '         gcolCmds..Add "TTLT4"
        '         gcolCmds..Add "TTLT5"
        '         gcolCmds..Add "TTLT6"
        '         gcolCmds..Add "TTLT7"
        '##########################################

        'Retrieve instrument output buffer
        sReadBuffer = ReadARB()

        ArbMain.bActivateControl(":ARM:SWE:SOUR " & sReadBuffer)

        '   ********************
        '   Get Step Advance Source
        '   ********************
        sInstrumentCmds = "TRIG:SWE:SOUR?"
        WriteInstrument(sInstrumentCmds)

        '##########################################
        'Action Required: Remove debug code
        'Fill return value selections
        '         gcolCmds..Add "Simulated response for: " & sInstrumentCmds
        '         gcolCmds..Add "BUS"
        '         gcolCmds..Add "HOLD"
        '         gcolCmds..Add "LINK"
        '         gcolCmds..Add "TIM"
        '         gcolCmds..Add "TTLT0"
        '         gcolCmds..Add "TTLT1"
        '         gcolCmds..Add "TTLT2"
        '         gcolCmds..Add "TTLT3"
        '         gcolCmds..Add "TTLT4"
        '         gcolCmds..Add "TTLT5"
        '         gcolCmds..Add "TTLT6"
        '         gcolCmds..Add "TTLT7"
        '##########################################

        'Retrieve instrument output buffer
        sReadBuffer = ReadARB()

        ArbMain.bActivateControl(":TRIG:SWE:SOUR " & sReadBuffer)

        '   ********************
        '   Get Sweep Count
        '   ********************
        sInstrumentCmds = "SWE:COUN?"
        WriteInstrument(sInstrumentCmds)

        '##########################################
        'Action Required: Remove debug code
        'Fill return value selections
        '         gcolCmds..Add "Simulated response for: " & sInstrumentCmds
        '         gcolCmds..Add "1"
        '         gcolCmds..Add "345"
        '         gcolCmds..Add "56756"
        '         gcolCmds..Add "983385"
        '         gcolCmds..Add "56292384"
        '         gcolCmds..Add "2147483647"
        '         gcolCmds..Add "9.9E+37"
        '##########################################

        'Retrieve instrument output buffer
        sReadBuffer = ReadARB()

        If Val(sReadBuffer) = 9.9E+37 Then
            sReadBuffer = "INF"
        End If

        ArbMain.bActivateControl(":SWE:COUN " & sReadBuffer)
    End Sub

    Public Sub cmdDownLoad_MouseClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDownLoad.Click
        cmdDownLoad_Click()
    End Sub

    Private Sub tolOnOff_Click(sender As Object, e As ToolBarButtonClickEventArgs) Handles tolOnOff.ButtonClick
        If tolOnOff.Buttons.IndexOf(e.Button) = 0 Then
            DisplayErrorMessage(atxmlDF_viSetAttribute(resourceName, InstrumentHandle, VI_ATTR_TMO_VALUE, 10000))
            ribAbor.Pushed = False
            ribInit.Pushed = True
            cmdTrig.Enabled = True
            cmdTrigStop.Enabled = True
            If lstListSseqSeq.Items.Count = 0 And SetCur$(FUNC) = "USER" Then
                If lstListCat.Items.Count = 0 Then    'If nothing downloaded
                    MsgBox("You must down load a User waveform segment and define a sequence list to initiate a User waveform.", vbInformation)
                    tabOptions.SelectedIndex = TAB_USER
                Else
                    MsgBox("You must define a sequence list to initiate a User waveform.", vbInformation)
                    tabOptions.SelectedIndex = TAB_USER
                End If
                Exit Sub
            End If
            If chkOutpStat.Checked = True Then
                imgWaveDisplay.Image = imlOutRelayWave.Images(WaveIdx)
            End If

            Select Case UCase(SetCur$(FUNC))
                Case "USER"
                    fraListCat.Enabled = False
                    fraSeqName.Enabled = False
                    Initialized = True
                    SendSequence()
                    tolFuncWave_ButtonClick(Me, New ToolBarButtonClickEventArgs(tolFuncWave.Buttons("User")))
                Case Else
                    Initialized = True
            End Select

            ' Force initial configuration to be sent to the hardware
            UserEnteringData = True
            txtAmpl_KeyPress(Keys.Enter)
            UserEnteringData = False
            If chkOutpStat.Checked Then
              SendCommand(INIT)
            End If
        Else
            ribInit.Pushed = False
            ribAbor.Pushed = True
            cmdTrig.Enabled = False
            cmdTrigStop.Enabled = False
            If UCase(SetCur$(FUNC)) = "USER" Then
                fraListCat.Enabled = True
                fraSeqName.Enabled = True
            End If
            imgWaveDisplay.Image = imlOutRelayWave.Images(0)
            SendCommand(TRIG_STOP)
            SendCommand(ABOR)
        End If
    End Sub

    Public Sub ribAbor_Click()
        tolOnOff_Click(Me, New ToolBarButtonClickEventArgs(tolOnOff.Buttons(1)))
    End Sub

    Public Sub cmdDownLoad_Click()
        'HP E1445A User's Manual page 3-5
        Dim n As Integer, NumSamps As Integer, S As String
        Dim iDacCode As Short, i As Integer
        Dim offset As Double, dScale As Double
        Dim status As Integer
        Dim InstDrvrHndl As Integer

        'initiate direct driver handle
        status = hpe1445a_init("VXI::48", VI_TRUE, VI_TRUE, InstDrvrHndl)

        NumSamps = CLng(SetCur(LIST_DEF))

        If Not bActivatingControls Then
            S = " Do you want to save this segment to disk now (this is necessary for"
            S &= " ARB NAM execution and for complete Configuration restoring)?"
            If MsgBox(S, MsgBoxStyle.YesNo + MsgBoxStyle.Question) = DialogResult.Yes Then
                cmdUserSave_Click()
            End If
        End If

        txtUserName.Text = UCase(txtUserName.Text)
        If txtUserName.Text = "" Then
            MsgBox("You must enter a segment name to download.", MsgBoxStyle.Exclamation)
            txtUserName.Focus()
            Exit Sub
        ElseIf Len(txtUserName.Text) > 12 Then
            MsgBox("The segment name cannot be more than 12 characters.  Please re-enter.", MsgBoxStyle.Critical)
            txtUserName.Focus()
            Exit Sub
        ElseIf IsNumeric(Strings.Left(txtUserName.Text, 1)) Then
            MsgBox("The segment name cannot start with a number.  Please re-enter.", MsgBoxStyle.Critical)
            txtUserName.Focus()
            Exit Sub
        End If
        For n = 0 To lstListCat.Items.Count - 1
            If txtUserName.Text = Strings.Left(lstListCat.Items.Item(n).ToString(), InStr(lstListCat.Items.Item(n).ToString(), ",") - 1) Then
                MsgBox("Name '" & txtUserName.Text & "' is already used. Delete or re-name.", MsgBoxStyle.Critical)
                txtUserName.Focus()
                Exit Sub
            End If
        Next n

        'OK, the error checking is done. We can continue.

        UserError = False
        SetSend(LIST_SEL, txtUserName.Text)
        lblListSel.Text = txtUserName.Text
        SendCommand(LIST_DEF)

        HelpPanel("Please wait, Downloading segment")
        Me.Cursor = Cursors.WaitCursor
        Me.Enabled = False

        If LiveMode Then
            offset = CDbl(SetCur(VOLT_OFFS))
            dScale = CDbl(SetCur(VOLT)) / 4095.0

            SetSend(ARB_DOWN, "VXI," & txtUserName.Text & "," & CStr(NumSamps))
            ' execute *OPC? to prevent the device from accepting commands until the
            ' download is complete
            WriteInstrument("*OPC?")
            S = ReadARB()

            For n = 0 To NumSamps - 4 'Go up to the third to last element as fast as possible
                iDacCode = CInt((Wave(n) - offset) / dScale) * 8 'Calculate DAC code and shift left 3 bits
                'Write the DAC code list to register 38 (decimal) in A24 address space

                'status = atxmlDF_viOut16(ResourceName, InstrumentHandle&, VI_A24_SPACE, 38, iDacCode)
                status = viOut16(InstDrvrHndl, VI_A24_SPACE, 38, iDacCode)

                If status <> 0 Then
                    UserError = True
                    lpString = Space(512)
                    atxmlDF_viStatusDesc(resourceName, InstrumentHandle, ErrorStatus, lpString)

                    MsgBox("Segment download, viOut16(38) Failed." & vbCrLf & "VISA error message: " & lpString)
                    Exit For
                End If
            Next n

            'Now do last 3
            If Not UserError Then
                For n = n To NumSamps - 1
                    'Set "last point" bit at NumSamps& - 3
                    iDacCode = (CInt((Wave(n) - offset) / dScale) * 8) 'Calculate DAC code and shift left 3 bits
                    'Set bit in 3rd to last only
                    If n = NumSamps - 3 Then iDacCode += 1
                    'Write the DAC code list to register 38 (decimal) in A24 address space

                    'status = atxmlDF_viOut16(resourceName, InstrumentHandle&, VI_A24_SPACE, 38, iDacCode)
                    status = viOut16(InstDrvrHndl, VI_A24_SPACE, 38, iDacCode)

                    If status <> 0 Then
                        UserError = True
                        lpString = Space(512)
                        atxmlDF_viStatusDesc(resourceName, InstrumentHandle, ErrorStatus, lpString)

                        MsgBox("Segment download, viOut16(38) Failed." & vbCrLf & "VISA error message: " & lpString)
                        Exit For
                    End If
                Next n
            End If

            'Download is complete
            SendCommand(ARB_DOWN_COMP)
        End If

        HelpPanel("")
        Me.Cursor = Cursors.Default
        Me.Enabled = True
        ' DoEvents
        If Not UserError Then
            i = lstListCat.Items.Count
            AddToList(lstListCat, txtUserName.Text & ", " & SetCur(LIST_DEF), i)
        Else
            WriteInstrument(":LIST:DEL")
            UserError = False
            Exit Sub
        End If
        WriteInstrument(":LIST:FREE?")
        S = ReadARB()
        If S <> "" Then
            lblListFree.Text = Val(S)
        Else
            lblListFree.Text = SetDef(LIST_FREE)
        End If

        hpe1445a_close(InstDrvrHndl)

        tabOptions.SelectedIndex = TAB_SEQUENCE 'Goto Sequence Tab
        lstListCat.Focus()
        lstListCat.SelectedIndex = i
    End Sub

    Private Sub cmdLoadFromFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdLoadFromFile.Click
        Dim sNextLine As String = ""
        Dim OpenDialog As Windows.Forms.OpenFileDialog = New Windows.Forms.OpenFileDialog()
        Dim reader As System.IO.StreamReader = Nothing

        Try
            OpenDialog.Filter = "Arb Files (*.arb)|*.arb|All Files (*.*)|*.*"
            OpenDialog.DefaultExt = "*.arb"
            OpenDialog.FileName = ""
            OpenDialog.FilterIndex = 1

            If OpenDialog.ShowDialog() = DialogResult.OK Then

                reader = New System.IO.StreamReader(OpenDialog.FileName)
                'Instrument mode is always first
                sNextLine = reader.ReadLine()
                SetMode(sNextLine)

                ' Build the list of controls on the parent window
                GetWindowControls()

                Do Until reader.EndOfStream
                    sNextLine = reader.ReadLine()

                    'Save to correct controls
                    SetFormData(sNextLine)
                Loop
            End If
        Catch ex As SystemException
            If Err.Number = 71 Or Err.Number = 31037 Then
                'Disk Full or can not save
                MsgBox("Error reading from disk")
            ElseIf Err.Number = 53 Or Err.Number = 75 Then
                'Path/File access or file not found
                MsgBox("File Not Found")
            ElseIf Err.Number = 70 Then
                'Object is not an array
                MsgBox("Permission denied")
            ElseIf Err.Number = 32755 Then
                'User pressed the cancel button
            Else
                ' Display exception error message
                MsgBox(ex.Message, MsgBoxStyle.Exclamation)
            End If
        Finally
            If Not reader Is Nothing Then
                reader.Close()
            End If
        End Try
    End Sub

    Private Sub SetFormData(ByVal sNextLine As String)
        Dim sValue As String
        Dim sControl As String = ""
        Dim nIndex As Short
        Dim Ctrl As Control = New Control()
        Static nMode As Short
        Dim i As Short
        Dim Cmds() As String

        Ctrl.Name = "Unknown Control"
        ' Grab the value from the string
        sValue = Strings.Right(sNextLine, Len(sNextLine) - InStr(sNextLine, "="))
        If InStr(sNextLine, "=") Then
            sControl = Strings.Left(sNextLine, InStr(sNextLine, "=") - 1)
        End If
        If InStr(sControl, "(") Then
            sControl = Strings.Left(sControl, InStr(sNextLine, "(") - 1)
        End If

        Try ' On Error GoTo ErrHandle

            'Determine file section
            If sNextLine = "[PANEL_SAVED_CONFIGURATION]" Then
                nMode = PANEL_SAVED_CONFIGURATION
                Exit Sub
            ElseIf sNextLine = "[PRE_SEGMENT_LOAD_COMMANDS]" Then
                nMode = PRE_SEGMENT_LOAD_COMMANDS
                Exit Sub
            ElseIf sNextLine = "[SEGMENT_LIST]" Then
                nMode = SEGMENT_LIST
                HelpPanel("Please wait, loading waveform segments")
                Exit Sub
            ElseIf sNextLine = "[POST_SEGMENT_LOAD_COMMANDS]" Then
                nMode = POST_SEGMENT_LOAD_COMMANDS
                Exit Sub
            ElseIf sNextLine = "[END_ARB_CONFIG]" Then
                nMode = END_ARB_CONFIG
                Exit Sub
            End If

            Select Case nMode
                Case PANEL_SAVED_CONFIGURATION
                    For Each ctl As System.Windows.Forms.Control In ControlList
                        If sControl = ctl.Name Then
                            'get the index if any
                            If InStr(sNextLine, "(") Then
                                nIndex = CInt(Mid(sNextLine, InStr(sNextLine, "(") + 1, InStr(sNextLine, ")") - InStr(sNextLine, "(") - 1))
                            Else
                                nIndex = -1
                            End If

                            'Start looking for matching type
                            If TypeOf ctl Is TextBox Then
                                ctl.Text = sValue
                                Exit Sub
                            ElseIf TypeOf ctl Is Windows.Forms.ComboBox Then
                                Dim cbobox As ComboBox = CType(ctl, ComboBox)
                                cbobox.SelectedIndex = Convert.ToInt16(sValue)
                                Exit Sub
                            ElseIf TypeOf ctl Is Windows.Forms.CheckBox Then
                                Dim chkbox As CheckBox = CType(ctl, CheckBox)
                                chkbox.Checked = Convert.ToBoolean(sValue)
                                Exit Sub
                            ElseIf TypeOf ctl Is Windows.Forms.RadioButton Then
                                Dim rdobtn As RadioButton = CType(ctl, RadioButton)
                                rdobtn.Checked = Convert.ToBoolean(sValue)
                                Exit Sub
                            ElseIf TypeOf ctl Is Windows.Forms.TabPage Then
                                ctl.Select()
                                Exit Sub
                            End If
                        End If
                    Next ctl

                    'checked all controls for form
                Case PRE_SEGMENT_LOAD_COMMANDS
                    WriteInstrument(sNextLine)
                    DoNotTalk = True
                    Cmds = Split(sNextLine, ";")
                    For i = 0 To Cmds.Length - 1
                        If bActivateControl(Cmds(i)) = False Then Exit For
                    Next i
                    DoNotTalk = False
                Case SEGMENT_LIST
                    bActivatingControls = True
                    txtUserName.Text = sNextLine
                    cmdUserLoad_Click()
                    If Err.Number = 0 Then
                        tabOptions.SelectedIndex = TAB_USER 'Goto User Tab
                        cmdDownLoad_Click()
                    End If
                Case POST_SEGMENT_LOAD_COMMANDS
                    WriteInstrument(sNextLine)
                    DoNotTalk = True
                    Cmds = Split(sNextLine, ";")
                    For i = 0 To Cmds.Length - 1
                        If bActivateControl(Cmds(i)) = False Then Exit For
                    Next i
                    DoNotTalk = False
                Case END_ARB_CONFIG
                    nMode = 0
            End Select
        Catch   ' ErrHandle:
            MsgBox("Error Processing: " & sNextLine & " with " & Ctrl.Name & vbCrLf & Err.Description, MsgBoxStyle.OkOnly, "SetFormData Error")
        End Try
    End Sub

    Private Sub GetWindowControls()
        Dim ctl As System.Windows.Forms.Control
        Dim objParent As Object = New Object

        ControlList.Clear()

        For Each ctl In Me.Controls
            BuildControlList(ctl)
        Next
    End Sub

    Private Sub BuildControlList(InCtl As System.Windows.Forms.Control)
        Dim ctl As System.Windows.Forms.Control
        If TypeOf InCtl Is Windows.Forms.UserControl Then
            ControlList.Add(InCtl)
        ElseIf InCtl.HasChildren Then
            If TypeOf InCtl Is TabPage Then
                ControlList.Add(InCtl)
            End If
            For Each ctl In InCtl.Controls
                BuildControlList(ctl)
            Next
        Else
            ControlList.Add(InCtl)
        End If
    End Sub

    Private Sub cmdLoadFromInstrument_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdLoadFromInstrument.Click
        ConfigGetCurrent()
    End Sub

    'Function cmdLocal_Click() switches between local and remote modes
    ' and changes the led between green and red, and switches the
    ' button caption between local and remote.
    Private Sub cmdLocal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdLocal.Click
        'Switch mode
        bDebugMode = Not bDebugMode

        'Start/Stop Timer
        timDebug.Enabled = bDebugMode

        If bDebugMode = True Then
            cmdLocal.Text = "Local"
        Else
            cmdLocal.Text = "Remote"
        End If
        pctMode.Value = bDebugMode
    End Sub

    Private Sub cmdMarkDelete_MouseClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdMarkDelete.Click
        SetSend(LIST_MARK, "0")
        SetCur(LIST_MARK_SPO) = "1"
        txtMarkSpo.Text = ""
    End Sub

    Private Sub cmdPan_MouseClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPan.Click
        If Not G1.InteractionModeDefault = GraphDefaultInteractionMode.PanXY Then
            G1.InteractionModeDefault = GraphDefaultInteractionMode.PanXY
            cmdPan.BackColor = Color.Silver
            cmdZoom.BackColor = Color.White
            cmdSnap.BackColor = Color.White
            Me.Cursor = Cursors.SizeAll
            HelpPanel("LeftMouseDrag to move graph, Ctl-RightMouseClick to undo last move")
        Else
            G1.InteractionModeDefault = GraphDefaultInteractionMode.None
            cmdPan.BackColor = Color.White
            Me.Cursor = Cursors.Default
            HelpPanel("")
        End If
    End Sub

    Public Sub cmdQuit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdQuit.Click
        cmdQuit_Click()
    End Sub

    Public Sub cmdQuit_Click()
        If LiveMode Then
            ErrorStatus = atxml_Close()
        End If
        Application.Exit()
    End Sub

    Public Sub cmdReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdReset.Click
        If Not bDebugMode Then
            cmdReset_Click()
        End If
    End Sub
    Public Sub cmdReset_Click()
        DoNotTalk = True
        SetControlsToReset()
        DoNotTalk = False
        tabOptions.SelectedIndex = TAB_MAIN
        SetSend(OUTP, "OFF")
        WriteInstrument("*CLS")
        WriteInstrument("*RST;:OUTP OFF")
    End Sub

    Private Sub cmdSaveToFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSaveToFile.Click
        'Local variables
        Dim sName As String
        Dim sValue As String
        Dim nIndex As Short
        Dim Ctl As Control
        Dim nIdx As Integer
        Dim sLine As String
        Dim SaveDialog As SaveFileDialog = New SaveFileDialog()
        Dim writer As System.IO.StreamWriter = Nothing

        Try
            'Set Filters
            SaveDialog.Filter = "Arb Files (*.arb)|*.arb|All Files (*.*)|*.*"
            'Specify default filter
            SaveDialog.FilterIndex = 1
            SaveDialog.FileName = ""
            'Display the dialog box
            If SaveDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
                'Show name of selected file
                MsgBox(SaveDialog.FileName)

                'Load data into file
                writer = New IO.StreamWriter(SaveDialog.FileName)

                'Get the instrument mode
                writer.WriteLine(GetMode())
                writer.WriteLine("[PANEL_SAVED_CONFIGURATION]")

                ' Build the list of controls on the parent window
                GetWindowControls()

                'Iterate through all of the controls
                 For Each Ctl In ControlList
                    'clear values
                    sName = ""
                    sValue = ""
                    nIndex = -1

                    If TypeOf Ctl Is TextBox Then
                        Dim txtbox As TextBox = CType(Ctl, TextBox)
                        sName = txtbox.Name
                        sValue = txtbox.Text
                    ElseIf TypeOf Ctl Is ComboBox Then
                        Dim cbbox As ComboBox = CType(Ctl, ComboBox)
                        sName = cbbox.Name
                        sValue = cbbox.SelectedIndex.ToString()
                    ElseIf TypeOf Ctl Is CheckBox Then
                        Dim chkbox As CheckBox = CType(Ctl, CheckBox)
                        sName = chkbox.Name
                        sValue = chkbox.Checked.ToString()
                    ElseIf TypeOf Ctl Is RadioButton Then
                        Dim rdobtn As RadioButton = CType(Ctl, RadioButton)
                        sName = rdobtn.Name
                        sValue = rdobtn.Checked.ToString()
                    ElseIf TypeOf Ctl Is TabPage Then
                        sName = Ctl.Name
                        sValue = ""
                    End If

                    'Build save string and add index if present
                    If nIndex <> -1 Then
                        sName += "(" + CStr(nIndex) + ")"
                    End If

                    If sName <> "" Then
                        'seperator then value
                        sName += "=" + sValue
                        writer.WriteLine(sName)
                    End If
                Next Ctl
                ' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                ' new code for handling user waveforms
                ' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                writer.WriteLine("[PRE_SEGMENT_LOAD_COMMANDS]")
                writer.WriteLine("*RST;*CLS")

                For nIdx = LAST_COUP_GROUP_3_COMMAND + 1 To MAX_SETTINGS
                    Select Case nIdx
                        Case LIST_SEL, LIST_DEF, LIST_VOLT, LIST_SSEQ_DEF, LIST_SSEQ_SEL, LIST_SSEQ_DWEL_COUN, LIST_SSEQ_SEQ, FUNC_USER, MARK_ECLT0_FEED, MARK_ECLT1_FEED, MARK_FEED, LIST_MARK, LIST_MARK_SPO
                            'These require quotes around the arguments
                        Case MARK_ECLT0_FEED, MARK_ECLT1_FEED, MARK_FEED
                            If SetCur(nIdx) <> SetDef(nIdx) Then
                                writer.WriteLine(SetCod(nIdx) & Quote & SetCur(nIdx) & Quote)
                            End If
                            'Don't write these commands because they are not 'STATE' commands
                        Case ARB_DOWN, ARB_DOWN_COMP
                            'Do nothing

                        Case Else
                            'OK, process these
                            If SetCod(nIdx) <> "" And (SetCur(nIdx) <> SetDef(nIdx)) Then
                                writer.WriteLine(SetCod(nIdx) & SetCur(nIdx))
                            End If
                    End Select
                Next nIdx

                sLine = ""
                For nIdx = LAST_COUP_GROUP_1_COMMAND + 1 To LAST_COUP_GROUP_2_COMMAND
                    If SetCur(nIdx) <> SetDef(nIdx) Then
                        sLine &= SetCod(nIdx) & SetCur(nIdx) & ";"
                    End If
                Next nIdx
                For nIdx = 1 To LAST_COUP_GROUP_1_COMMAND
                    If SetCur(nIdx) <> SetDef(nIdx) Then
                        If nIdx = FREQ_FSK_L Or nIdx = FREQ_FSK_H Then
                            sLine &= SetCod(nIdx) & SetCur(FREQ_FSK_L) & "," & SetCur(FREQ_FSK_H) & ";"
                        Else
                            sLine &= SetCod(nIdx) & SetCur(nIdx) & ";"
                        End If
                    End If
                Next nIdx
                If sLine <> "" Then
                    writer.WriteLine(Strings.Left(sLine, Len(sLine) - 1)) 'Strip last ';'
                End If

                'Loop through Group 3 commands
                sLine = ""
                For nIdx = LAST_COUP_GROUP_2_COMMAND + 1 To LAST_COUP_GROUP_3_COMMAND
                    If nIdx = VOLT Then 'Always add Volt in case output impedance is high and volt=def
                        sLine &= SetCod(nIdx) & SetCur(nIdx) & ";"
                    ElseIf SetCur(nIdx) <> SetDef(nIdx) Then
                        sLine &= SetCod(nIdx) & SetCur(nIdx) & ";"
                    End If
                Next nIdx
                If sLine <> "" Then
                    writer.WriteLine(Strings.Left(sLine, Len(sLine) - 1)) 'Strip last ';'
                End If

                'Send commands to setup loading segments
                writer.WriteLine(SetCod(LIST_SSEQ_DEL_ALL)) 'Delete all sequences
                writer.WriteLine(SetCod(LIST_DEL_ALL)) 'Delete all segments

                'Write name of each Segment (to be read from file and down-loaded to ARB)
                writer.WriteLine("[SEGMENT_LIST]")
                For nIdx = 0 To lstListCat.Items.Count - 1
                    writer.WriteLine(Strings.Left(lstListCat.Items.Item(nIdx).ToString(), InStr(lstListCat.Items.Item(nIdx).ToString(), ",") - 1))
                Next nIdx

                'Write Sequence definition commands (to be executed after segments are down-loaded to ARB)
                writer.WriteLine("[POST_SEGMENT_LOAD_COMMANDS]")
                If lstListSseqSeq.Items.Count > 0 Then 'If sequence list not empty
                    writer.WriteLine(SetCod(LIST_SSEQ_SEL) & SetCur(LIST_SSEQ_SEL))
                    SetCur(LIST_SSEQ_DEF) = lstListSseqSeq.Items.Count
                    writer.WriteLine(SetCod(LIST_SSEQ_DEF) & SetCur(LIST_SSEQ_DEF))
                    writer.WriteLine(SetCod(LIST_SSEQ_SEQ) & SetCur(LIST_SSEQ_SEQ))
                    writer.WriteLine(SetCod(LIST_SSEQ_DWEL_COUN) & SetCur(LIST_SSEQ_DWEL_COUN))
                    writer.WriteLine(SetCod(FUNC_USER) & SetCur(FUNC_USER))
                End If

                'Write Marker Feed commands last in case the feed source is a waveform point.
                For nIdx = LAST_COUP_GROUP_3_COMMAND + 1 To MAX_SETTINGS
                    Select Case nIdx
                        Case MARK_ECLT0_FEED, MARK_ECLT1_FEED, MARK_FEED
                            If SetCur(nIdx) <> SetDef(nIdx) Then
                                writer.WriteLine(SetCod(nIdx) & Quote & SetCur(nIdx) & Quote)
                            End If
                        Case LIST_MARK, LIST_MARK_SPO
                            If SetCur(nIdx) <> SetDef(nIdx) Then
                                writer.WriteLine(SetCod(nIdx) & SetCur(nIdx))
                            End If
                    End Select
                Next nIdx
                writer.WriteLine("[END_ARB_CONFIG]")
            End If
        Catch ex As SystemException
            If Err.Number = 343 Then
                'object is not an array
            End If

            If Err.Number = 61 Or Err.Number = 31036 Then
                'disk full or cannot save
                MsgBox("Error writing to Disk")
            ElseIf Err.Number = 75 Or Err.Number = 76 Then
                'Path/file access or path not found
                MsgBox("Unable to save File")
            ElseIf Err.Number = 70 Then
                'object is not an array
                MsgBox("Permission denied")
            ElseIf Err.Number = 32755 Then
                'User pressed teh Cancel button
            Else
                ' Display exception message
                MsgBox(ex.Message, MsgBoxStyle.Exclamation)
            End If
        Finally
            If Not writer Is Nothing Then
                writer.Close()
            End If
        End Try
    End Sub

    Public Sub cmdSegAdd_MouseClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSegAdd.Click
        cmdSegAdd_Click()
    End Sub

    Public Sub cmdSegAdd_Click()
        If lstListCat.Items.Count = 0 Then 'If empty list
            Exit Sub
        ElseIf lstListCat.SelectedIndex = -1 Then            'If none selected
            lstListCat.SelectedIndex = 0 'then select the 1st one
        End If
        lstListSseqSeq.Items.Add(Strings.Left(lstListCat.Text, InStr(lstListCat.Text, ",") - 1) & ", 1")
        SendSequence()
    End Sub

    Private Sub cmdSegDel_MouseClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSegDel.Click
        Dim i As Integer, S As String

        If lstListCat.Text = "" Then Exit Sub
        S = Strings.Left(lstListCat.Text, InStr(lstListCat.Text, ",") - 1)
        For i = 0 To lstListSseqSeq.Items.Count - 1
            If S = Strings.Left(lstListSseqSeq.Items.Item(i).ToString(), InStr(lstListSseqSeq.Items.Item(i).ToString(), ",") - 1) Then
                MsgBox("Cannot delete segment " & S & " until it is removed from the current sequence.", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
        Next i
        SetSend(LIST_SEL, S)
        If lstListSseqSeq.Items.Count = 0 Then 'If no seq list defined
            ribInit.Pushed = False
            ribAbor.Pushed = True
            ribAbor_Click()
            SetSend(FUNC_USER, "NONE")
            SendCommand(LIST_SSEQ_DEL_ALL)
        End If
        SendCommand(LIST_DEL)
        i = lstListCat.SelectedIndex
        lstListCat.Items.RemoveAt(i)
        lstListCat.SelectedIndex = i - 1
        WriteInstrument(":LIST:FREE?")
        S = ReadARB()
        If S <> "" Then
            lblListFree.Text = Val(S)
        Else
            lblListFree.Text = SetDef(LIST_FREE)
        End If
    End Sub

    Private Sub cmdSeqListDown_MouserClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSeqListDown.Click
        Dim i As Integer

        i = lstListSseqSeq.SelectedIndex
        If i >= lstListSseqSeq.Items.Count - 1 Then Exit Sub
        lstListSseqSeq.Items.Insert(i + 2, lstListSseqSeq.Items.Item(i).ToString())
        lstListSseqSeq.Items.RemoveAt(i)
        lstListSseqSeq.SelectedIndex = i + 1
        SendSequence()
    End Sub

    Private Sub cmdSeqListRemove_MouseClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSeqListRemove.Click
        Dim i As Integer

        i = lstListSseqSeq.SelectedIndex
        If i = -1 Then Exit Sub
        lstListSseqSeq.Items.RemoveAt(i)
        lstListSseqSeq.SelectedIndex = i - 1
        SendSequence()
    End Sub

    Private Sub cmdSeqListUp_MouseClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSeqListUp.Click
        Dim i As Integer

        i = lstListSseqSeq.SelectedIndex
        If i < 1 Then Exit Sub
        lstListSseqSeq.Items.Insert(i - 1, lstListSseqSeq.Items.Item(i).ToString())
        lstListSseqSeq.Items.RemoveAt(i + 1)
        lstListSseqSeq.SelectedIndex = i - 1
        SendSequence()
    End Sub

    Private Sub cmdSnap_MouseClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSnap.Click
        If G1.InteractionModeDefault = GraphDefaultInteractionMode.PanXY Then
            cmdPan_MouseClick(sender, e)
        End If
        If G1.InteractionModeDefault = GraphDefaultInteractionMode.ZoomXY Then
            cmdZoom_Click(sender, e)
        End If
        If CursorTo.SnapMode = CursorSnapMode.NearestPoint Then
            CursorTo.SnapMode = CursorSnapMode.Floating
            cmdSnap.BackColor = Color.White
        Else
            CursorTo.SnapMode = CursorSnapMode.NearestPoint
            cmdSnap.BackColor = Color.Silver
        End If
    End Sub

    Private Sub cmdSweCounInf_MouseClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSweCounInf.Click
        SetSend(SWE_COUN, "INF")
        txtSweCoun.Text = "INF"
    End Sub

    Private Sub cmdTest_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdTest.Click
        Dim Message As String, TmpString As String

        If Not LiveMode Then
            MsgBox("Built-In Test is not available.  Live mode is disabled.", MsgBoxStyle.Information)
            Exit Sub
        End If

        HelpPanel("Performing Built-In Test...")
        Me.Enabled = False 'Disable user interaction
        WriteInstrument("*CLS;*RST")
        DoNotTalk = True
        SetControlsToReset()
        DoNotTalk = False
        WriteInstrument("*TST?")
        TmpString = ReadARB()
        HelpPanel("Built-In Test completed.")
        If Val(TmpString) = 0 Then
            MsgBox("Built-In Test Passed", MsgBoxStyle.Information)
        Else
            Message = "Built-In Test Failed."
            Do
                Message &= vbCrLf & TmpString
                WriteInstrument("SYST:ERR?")
                TmpString = ReadARB()
            Loop While Val(TmpString) <> 0
            MsgBox(Message, MsgBoxStyle.Critical)
        End If

        Me.Enabled = True 'Enable user interaction
        HelpPanel("")
    End Sub

    Private Sub cmdTrig_ClickEvent(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdTrig.Click
        SendCommand(TRIG)
    End Sub

    Private Sub cmdTrigStop_ClickEvent(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdTrigStop.Click
        SendCommand(TRIG_STOP)
    End Sub

    Private Sub cmdUpdateTip_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdUpdateTip.Click
        sTipCmds = sGetTipCmds()
        SetKey("TIPS", "CMD", sTipCmds)
        SetKey("TIPS", "STATUS", "Ready")
        cmdQuit_Click()
    End Sub

    Public Sub cmdUserClear_MouseClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdUserClear.Click
        cmdUserClear_Click()
    End Sub

    Public Sub cmdUserClear_Click()
        ReDim Back(Wave.Length)
        Wave.CopyTo(Back, 0)
        cmdUserUndo.Enabled = True
        ReDim Wave(Convert.ToInt32(SetCur(LIST_DEF)))
        For i = 0 To Wave.Length - 1
            Wave(i) = 0
        Next

        G1.InteractionModeDefault = GraphDefaultInteractionMode.None
        cmdZoom.BackColor = Color.White
        cmdPan.BackColor = Color.White
        G1.PlotY(Wave, 1, 1)
        CursorTo.XPosition = 1
        CursorTo.YPosition = 0
        CursorFrom.XPosition = 1
        CursorFrom.YPosition = 0
        lblWrf1.Text = "Segment Freq."
        lblModFreq.Visible = False
        lblModFreq1.Visible = False
        UpdateWrf()
    End Sub

    Public Sub cmdUserLoad_MouseClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdUserLoad.Click
        cmdUserLoad_Click()
    End Sub

    Public Sub cmdUserLoad_Click()
        'DESCRIPTION:
        ' Loads a segment file from disk into the segment editor.

        Dim FileId As Integer, TmpName As String = "", NumBytes As Integer, FileSize As Integer, MaxFileSize As Integer
        Dim VMax As Double, VMin As Double
        Dim TyxStupidMagicNumber As Integer
        Dim TmpStr As String = "", i As Short
        Dim mChar As Byte
        Dim List() As String = {""}
        Dim OpenDialog As Windows.Forms.OpenFileDialog = New Windows.Forms.OpenFileDialog()
        Dim results As System.Windows.Forms.DialogResult
        Dim Reader As System.IO.BinaryReader = Nothing
        Dim fi As System.IO.FileInfo

        Try
            OpenDialog.DefaultExt = "Seg"
            OpenDialog.Filter = "Segment File (*.Seg)|*.Seg|All Files (*.*)|*.*"
            OpenDialog.DefaultExt = "Seg"
            If Not bActivatingControls Then
                OpenDialog.FileName = Extract(txtUserName.Text, "FileName") & "." & OpenDialog.DefaultExt
                results = OpenDialog.ShowDialog()
                If results <> Windows.Forms.DialogResult.OK Or OpenDialog.FileName = "" Then
                    Exit Sub
                End If

                If Len(TmpName) > 12 Then
                    MsgBox("The segment name cannot be more than 12 characters.  Truncating...", MsgBoxStyle.Critical)
                    TmpName = Strings.Left(TmpName, 12)
                ElseIf IsNumeric(Strings.Left(TmpName, 1)) Then
                    MsgBox("The segment name cannot start with a number.  Please re-name.", MsgBoxStyle.Critical)
                    Exit Sub
                End If
            Else
                TmpName = txtUserName.Text
                OpenDialog.FileName = sConfigFilePath & TmpName & ".Seg"
                fi = New IO.FileInfo(sConfigFilePath & TmpName & ".Seg")
                If Not fi.Exists Then
                    dlgFileIO.ShowDialog()
                    results = OpenDialog.ShowDialog()
                    If results <> Windows.Forms.DialogResult.OK Or OpenDialog.FileName = "" Then
                        Exit Sub
                    End If
                    TmpName = OpenDialog.FileName
                End If
            End If

            fi = New IO.FileInfo(OpenDialog.FileName)
            FileSize = fi.Length
            MaxFileSize = (8 * Val(SetMax(LIST_DEF))) + 511 '256 Byte Header Block plus up to 255 filler bytes Added Version 1.2
            If FileSize > MaxFileSize Then
                MsgBox("File " & OpenDialog.FileName & " is too big." & "Cannot be more than " & MaxFileSize + 511 & " bytes.", MsgBoxStyle.Critical)
                Exit Sub
            End If
            If FileSize Mod (8) <> 0 Or FileSize < 512 Then
                MsgBox("File " & OpenDialog.FileName & " is not the correct size.", MsgBoxStyle.Critical)
                Exit Sub
            End If

            '---------------Version 1.2 Modification By DHartley---------------
            '[Re]Format Wave for TETS-ATLAS Compatability (256 Byte File Blocks)
            Reader = New IO.BinaryReader(System.IO.File.Open(OpenDialog.FileName, IO.FileMode.Open))

            'Added for V1.3 JHill
            TyxStupidMagicNumber = Reader.ReadInt32()
            If TyxStupidMagicNumber <> &HABCD4567 Then
                MsgBox("File " & OpenDialog.FileName & " does not have the correct format.", MsgBoxStyle.Critical)
                FileClose(FileId)
                Exit Sub
            End If

            'OK, enough error checking
            txtUserName.Text = UCase(TmpName)

            NumBytes = Reader.ReadInt32()
            SetCur(LIST_DEF) = NumBytes / 8
            txtUserPoints.Text = EngNotate(SetCur(LIST_DEF), LIST_DEF)

            'If a user-clicked Load, then do this, else its a Configuration load, so don't.
            If Not bActivatingControls Then
                'Dig out the embedded Trace Preamble data
                For i = 9 To 256
                    mChar = Reader.ReadByte()
                    If mChar > 0 Then
                        TmpStr &= Convert.ToString(Chr(mChar))
                    Else
                        Exit For
                    End If
                Next i

                'If found an IEEE Definite Length Block...
                If Strings.Left(TmpStr, 1) = "#" Then
                    TmpStr = sIEEEDefinite("From", TmpStr)
                    'If correct number of arguments, process Preamble data, else don't
                    If StringToList(TmpStr, 1, List, ",") = 10 Then
                        Trace.Format = List(1)
                        Trace.Type = List(2)
                        Trace.Points = List(3)
                        Trace.count = List(4)
                        Trace.XInc = List(5)
                        Trace.XOrig = List(6)
                        Trace.XRef = List(7)
                        Trace.YInc = List(8)
                        Trace.YOrig = List(9)
                        Trace.YRef = List(10)

                        'Set sample clock (FREQ)
                        txtFreq.Text = 1 / Trace.XInc
                        UserEnteringData = True
                        txtFreq_KeyPress(Keys.Return)

                        'Set VOLT (Units is always V - equiv to Vpk)
                        txtAmpl.Text = Round(Trace.YInc * Trace.YRef, 5)
                        UserEnteringData = True
                        txtAmpl_KeyPress(Keys.Return)

                        'Set VOLT_OFFS
                        txtDcOffs.Text = CStr(Trace.YOrig)
                        UserEnteringData = True
                        txtDcOffs_KeyPress(Keys.Return)
                        LastOffs = Val(SetCur(VOLT_OFFS))
                    End If
                End If
            End If
            LastOffs = Val(SetCur(VOLT_OFFS))

            'Read the segment voltage points
            Reader.BaseStream.Position = 256
            i = 0
            ReDim Wave(10000)
            While Reader.BaseStream.Position < Reader.BaseStream.Length
                Wave(i) = Reader.ReadDouble()
                i += 1
            End While
            ReDim Preserve Wave(i)

            'If a preamble was NOT found and NOT loading a configuration
            If (Trace.YInc = 0) And Not bActivatingControls Then
                If VMax > Math.Abs(VMin) Then
                    SetCur(VOLT) = VMax
                Else
                    SetCur(VOLT) = Math.Abs(VMin)
                End If
                txtAmpl.Text = EngNotate(SetCur(VOLT), VOLT)
            End If

            If AdjustToLimit(VOLT, txtAmpl) = 1 Then
                MsgBox("File " & OpenDialog.FileName & " contains over-range voltage points.  It cannot be downloaded without modification to the segment or instrument settings.", MsgBoxStyle.Critical)
            End If

            SizeYAxis()
            UpdateGraph()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            If Not Reader Is Nothing Then
                Reader.Close()
            End If
        End Try
    End Sub

    Private Sub cmdUserPm_MouseClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdUserPm.Click
        Dim NumSamps As Integer, Vpk As Double, Phase As Double, NextPhase As Double, S As String
        Dim Info() As Double
        Dim Seg() As Double
        Dim Dif As Double, i As Integer, n As Integer, j As Integer, toggle As Short
        Dim InfoPts As Integer, MinInfoPts As Integer, MaxInfoPts As Integer, CarrierPts As Integer, MinCarrierPts As Integer, MaxCarrierPts As Integer
        Dim PosDev As Double, NegDev As Double, HalfRange As Double, Middle As Double, SmpFreq As Double
        Dim dfreq As Double

        Try
            'Function SineWave (n As Long, amp As Double, f As Double, phase As Double, x( ) As Double ) As Long
            Phase = 0
            toggle = 10
            NumSamps = CLng(SetCur(LIST_DEF))
            Vpk = CDbl(SetCur(VOLT))
            MinCarrierPts = 8 ' = 5 Mhz carrier @ 40MHz sample freq
            MaxCarrierPts = 128 ' yields .7mV step @ flattest point (DAC res = 1.25mV)
            MinInfoPts = 10 ' = 500 kHz mod freq @ max carrier freq
            MaxInfoPts = 2048 ' 262144/128 memory limitation

            SmpFreq = Val(SetCur(CurFreqIdx))
            SetMax(JDH_PM_CARR) = (SmpFreq / MinCarrierPts).ToString()
            SetMin(JDH_PM_CARR) = (SmpFreq / MaxCarrierPts).ToString()
            SetDef(JDH_PM_CARR) = SetMax(JDH_PM_CARR)
            If SetCur(JDH_PM_CARR) = "" Then
                SetCur(JDH_PM_CARR) = SetMax(JDH_PM_CARR) 'For the default prompt
            Else
                Void = AdjustToLimit(JDH_PM_CARR, Me.txtVoid)
            End If
            S = UserEnter(JDH_PM_CARR, "Carrier frequency")
            If UserCancel Then Exit Sub
            If SetCur(JDH_PM_CARR) = "" Then Exit Sub
            CarrierPts = SmpFreq / Val(SetCur(JDH_PM_CARR))

            SetMax(JDH_PM_MOD) = ((SmpFreq / CarrierPts) / MinInfoPts).ToString()
            SetMin(JDH_PM_MOD) = (SmpFreq / 262144).ToString()
            SetDef(JDH_PM_MOD) = SetMax(JDH_PM_MOD)
            If SetCur(JDH_PM_MOD) = "" Then
                SetCur(JDH_PM_MOD) = SetMax(JDH_PM_MOD) 'For the default prompt
            Else
                Void = AdjustToLimit(JDH_PM_MOD, Me.txtVoid)
            End If
            S = UserEnter(JDH_PM_MOD, "Modulating frequency")
            If UserCancel Then Exit Sub
            If SetCur(JDH_PM_MOD) = "" Then Exit Sub

            If CarrierPts > MaxCarrierPts Then Stop
            InfoPts = (SmpFreq / Val(SetCur(JDH_PM_MOD)) / CarrierPts)

            ' Here is single entry for deviation
            S = UserEnter(JDH_PM_POS_DEV, "Maximum deviation")
            If UserCancel Then Exit Sub
            PosDev = Val(SetCur(JDH_PM_POS_DEV))
            SetCur(JDH_PM_NEG_DEV) = -PosDev


            lListDefine = Val(SetCur(LIST_DEF)) 'Save for Undo
            txtUserName.Text = "PM"
            ReDim Back(Wave.Length)
            Wave.CopyTo(Back, 0)
            cmdUserUndo.Enabled = True
            NegDev = Val(SetCur(JDH_PM_NEG_DEV))
            HalfRange = (PosDev - NegDev) / 2
            Middle = HalfRange + NegDev
            NumSamps = InfoPts * CarrierPts
            SetCur(JDH_CYCLES) = InfoPts.ToString()
            SetCur(LIST_DEF) = NumSamps
            SetMax(LIST_MARK_SPO) = SetCur(LIST_DEF)
            lblWrf1.Text = "Carrier Freq."
            UpdateWrf()
            txtUserPoints.Text = EngNotate(SetCur(LIST_DEF), LIST_DEF)
            HelpPanel("Please Wait: Updating display")
            UpdateGraph()
            cmdZoomOut_Click()

            ReDim Wave(NumSamps)
            dfreq = GetFlatValue(txtSampleFreq.Text)
            Info = BasicFunctionGenerator.GenerateSineWave(1, 1, 0, 0.0, (1 * NumSamps) / InfoPts, NumSamps)
            ReDim Preserve Info(InfoPts)

            NextPhase = (Info(0) * HalfRange) + Middle
            For i = 0 To InfoPts - 1
                Phase = NextPhase
                NextPhase = (Info(i + 1) * HalfRange) + Middle
                Dif = (NextPhase - Phase) / 360

                Seg = BasicFunctionGenerator.GenerateSineWave(dfreq, Vpk, Phase, 0.0, (dfreq * NumSamps) / InfoPts, NumSamps)

                toggle = -toggle
                j = i * CarrierPts
                For n = 1 To CarrierPts
                    Wave(j + n) = Seg(n - 1)
                Next n
            Next i

            YAxis1.Range = New Range(-1 * (GetFlatValue(txtAmpl.Text) + GetFlatValue(txtDcOffs.Text)), GetFlatValue(txtAmpl.Text) + GetFlatValue(txtDcOffs.Text))
            XAxis.Range = New Range(0, Wave.Length)
            CursorMarker.XPosition = Wave.Length / 2
            AdjustUserWave()
            HelpPanel("")
            lblModFreq.Visible = True
            lblModFreq1.Visible = True
        Catch ex As Exception
            MessageBox.Show("cmdUserPm_MouseClick: " + ex.Message)
        End Try
    End Sub

    Private Sub cmdUserRamp_MouseClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdUserRamp.Click
        Dim NumSamps As Integer, Vpk As Double, Phase As Double, S As String, NumCycles As Double

        Try
            'Function Ramp& (NumSamps&, FirstAmp#, LastAmp#, Array#)
            NumSamps = CLng(SetCur(LIST_DEF))
            Vpk = CDbl(SetCur(VOLT))
            SetMin(JDH_CYCLES) = "0.1"
            SetMax(JDH_CYCLES) = NumSamps / 2
            S = UserEnter(JDH_CYCLES, "Number of cycles to generate")
            If UserCancel Then Exit Sub
            NumCycles = CDbl(Val(SetCur(JDH_CYCLES)))
            S = UserEnter(JDH_PHASE, "Phase Shift")
            If UserCancel Then Exit Sub

            lListDefine = Val(SetCur(LIST_DEF)) 'Save for Undo
            txtUserName.Text = "RAMP"
            ReDim Back(Wave.Length)
            Wave.CopyTo(Back, 0)
            cmdUserUndo.Enabled = True
            lblWrf1.Text = "Output Freq."
            UpdateWrf()
            Phase = CDbl(Val(SetCur(JDH_PHASE)))

            Dim dfreq As Double = GetFlatValue(txtSampleFreq.Text)
            Wave = BasicFunctionGenerator.GenerateSawtoothWave(dfreq, Vpk, Phase, 0.0, (dfreq * NumSamps) / NumCycles, NumSamps)

            YAxis1.Range = New Range(-1 * (GetFlatValue(txtAmpl.Text) + GetFlatValue(txtDcOffs.Text)), GetFlatValue(txtAmpl.Text) + GetFlatValue(txtDcOffs.Text))
            XAxis.Range = New Range(0, Wave.Length)
            CursorMarker.XPosition = Wave.Length / 2
            AdjustUserWave()
            lblModFreq.Visible = False
            lblModFreq1.Visible = False
        Catch ex As Exception
            MessageBox.Show("cmdUserRamp_MouseClick: " + ex.Message)
        End Try
    End Sub

    Private Sub cmdUserSave_MouseClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdUserSave.Click
        cmdUserSave_Click()
    End Sub

    Public Sub cmdUserSave_Click()
        Dim i As Integer
        Dim TmpStr As String
        Dim NullByte As Byte
        Dim PadLength As Short
        Dim lNumber As Integer
        Dim SaveDialog As Windows.Forms.SaveFileDialog = New Windows.Forms.SaveFileDialog()
        Dim writer As System.IO.BinaryWriter = Nothing
        Dim fi As System.IO.FileInfo

        Try
            SaveDialog.DefaultExt = "Seg"
            SaveDialog.Filter = "Segment File (*.Seg)|*.seg|All Files (*.*)|*.*"
            SaveDialog.FileName = txtUserName.Text & ".Seg"

            If SaveDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
                fi = New IO.FileInfo(SaveDialog.FileName)
                txtUserName.Text = fi.Name.Substring(0, fi.Name.IndexOf("."))

                If txtUserName.Text = "" Then
                    MsgBox("Enter a segment file name to save (without extension).", MsgBoxStyle.Exclamation)
                    txtUserName.Focus()
                    Exit Sub
                ElseIf Len(txtUserName.Text) > 12 Then
                    MsgBox("The segment name cannot be more than 12 characters.  Please re-enter.", MsgBoxStyle.Critical)
                    txtUserName.Focus()
                    Exit Sub
                ElseIf IsNumeric(Strings.Left(txtUserName.Text, 1)) Then
                    MsgBox("The segment name cannot start with a number.  Please re-enter.", MsgBoxStyle.Critical)
                    txtUserName.Focus()
                    Exit Sub
                End If

                writer = New IO.BinaryWriter(System.IO.File.Open(SaveDialog.FileName, IO.FileMode.Create))

                '---------------Version 1.2 Modification By By DHartley---------------
                '[Re]Format Wave for TETS-ATLAS Compatability (256 Byte File Blocks)

                'Start Header Block
                lNumber = CLng(&HABCD4567) 'TYX 'Magic' number
                writer.Write(lNumber)

                lNumber = CLng(Val(SetCur(LIST_DEF)) * 8) 'Number of bytes for segment data
                writer.Write(lNumber)

                'Calculate number of post-fill zero bytes
                PadLength = (256 - (lNumber Mod 256)) Mod 256

                'Write E1428A Scope Trace Preamble data
                Trace.Format = 2 '16-bit resolution
                Trace.Type = 1 'Scalar
                Trace.Points = CLng(SetCur(LIST_DEF))
                Trace.count = 1
                Trace.XInc = 1.0 / CDbl(SetCur(CurFreqIdx))
                Trace.XOrig = 0
                Trace.XRef = 0
                Trace.YInc = CDbl(SetCur(VOLT) / 16384)
                Trace.YOrig = CDbl(SetCur(VOLT_OFFS))
                Trace.YRef = 16384

                TmpStr = CStr(Trace.Format) & "," & CStr(Trace.Type) & "," & CStr(Trace.Points) & "," & CStr(Trace.count) & "," & CStr(Trace.XInc) & "," & CStr(Trace.XOrig) & "," & CStr(Trace.XRef) & "," & CStr(Trace.YInc) & "," & CStr(Trace.YOrig) & "," & CStr(Trace.YRef) & vbLf
                TmpStr = sIEEEDefinite("To", TmpStr)

                writer.Write(TmpStr)

                'Fill out the first block with Nulls
                NullByte = &H0
                For i = 8 + TmpStr.Length To 255
                    writer.Write(NullByte)
                Next i

                'Make Wave array real size to go to file (JHill V1.2)
                ReDim Preserve Wave(Val(SetCur(LIST_DEF)))
                writer.BaseStream.Position = 256
                Dim dataPoint As Double
                For Each dataPoint In Wave
                    writer.Write(dataPoint)
                Next

                For i = 1 To PadLength 'Write NULL to Pad Last Block
                    writer.Write(NullByte)
                Next i

                ''!!!!!!!!!!!!!Debug-Remove-Before-Compiling!!!!!!!!!!!!!
                ''Uncomment This Section For Debugging.  This Code will save
                ''the waveform in the origional (V1.1 or earlier) format under
                ''[Filename In File Dialog].OLD
                ''This File is basically the waveform without the header and footer
                'Dim File2
                'File2 = FreeFile(0)
                'Dim FileName As String
                'FileName$ = Mid$(dlgFileIO.FileName, 1, Len(dlgFileIO.FileName) - 4) & ".old"
                'If FileExists(FileName$) Then
                '    Kill (FileName$)     'Must be done to resize the file
                'End If
                'Open FileName$ For Binary Access Write As File2
                'Put #File2, , Wave 'Write Data
                'Close File2
                ''!!!!!!!!!!!!!Debug-Remove-Before-Compiling!!!!!!!!!!!!!
            End If
        Catch ex As Exception
            MessageBox.Show("Exception caught in cmdUserSave_Click: " + ex.Message)
        Finally
            If Not writer Is Nothing Then
                writer.Close()
            End If
        End Try
        '----------------End Version 1.2 Modification By DHartley------------
    End Sub

    Private Sub cmdUserSine_MouseClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdUserSine.Click
        Dim NumSamps As Integer, Vpk As Double, Phase As Double, NumCycles As Double, S As String

        Try
            NumSamps = CLng(SetCur(LIST_DEF))
            Vpk = CDbl(SetCur(VOLT))
            SetMin(JDH_CYCLES) = "0.1"
            SetMax(JDH_CYCLES) = NumSamps / 2
            S = UserEnter(JDH_CYCLES, "Number of cycles to generate")
            If UserCancel Then Exit Sub
            NumCycles = Val(SetCur(JDH_CYCLES))
            S = UserEnter(JDH_PHASE, "Phase Shift")
            If UserCancel Then Exit Sub

            lListDefine = Val(SetCur(LIST_DEF)) 'Save for Undo
            txtUserName.Text = "SINE"
            ReDim Back(Wave.Length)
            Wave.CopyTo(Back, 0)
            cmdUserUndo.Enabled = True
            lblWrf1.Text = "Output Freq."
            UpdateWrf()
            Phase = CDbl(Val(SetCur(JDH_PHASE)))

            Dim dfreq As Double = GetFlatValue(txtSampleFreq.Text)
            Wave = BasicFunctionGenerator.GenerateSineWave(dfreq, Vpk, Phase, 0.0, (dfreq * NumSamps) / NumCycles, NumSamps)

            YAxis1.Range = New Range(-1 * (GetFlatValue(txtAmpl.Text) + GetFlatValue(txtDcOffs.Text)), GetFlatValue(txtAmpl.Text) + GetFlatValue(txtDcOffs.Text))
            XAxis.Range = New Range(0, Wave.Length)
            CursorMarker.XPosition = Wave.Length / 2
            AdjustUserWave()
            lblModFreq.Visible = False
            lblModFreq1.Visible = False
        Catch ex As Exception
            MessageBox.Show("cmdUserSine_MouseClick: " + ex.Message)
        End Try
    End Sub

    Private Sub cmdUserSquare_MouseClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdUserSquare.Click
        Dim NumSamps As Integer, Vpk As Double, Phase As Double, dutyCycle As Double, S As String, NumCycles As Double

        Try
            NumSamps = CLng(SetCur(LIST_DEF))
            SetMin(JDH_CYCLES) = "0.1"
            SetMax(JDH_CYCLES) = NumSamps / 2
            Vpk = CDbl(SetCur(VOLT))
            S = UserEnter(JDH_CYCLES, "Number of cycles to generate")
            If UserCancel Then Exit Sub
            NumCycles = CDbl(Val(SetCur(JDH_CYCLES)))
            SetMin(JDH_DCYC) = (1 / (NumSamps / NumCycles)) * 100 ' 1 point
            SetMax(JDH_DCYC) = (((NumSamps / NumCycles) - 1) / (NumSamps / NumCycles)) * 100
            SetMinInc(JDH_DCYC) = SetMin(JDH_DCYC)
            S = UserEnter(JDH_DCYC, "Duty Cycle")
            If UserCancel Then Exit Sub
            dutyCycle = CDbl(Val(SetCur(JDH_DCYC)))
            S = UserEnter(JDH_PHASE, "Phase Shift")
            If UserCancel Then Exit Sub

            lListDefine = Val(SetCur(LIST_DEF)) 'Save for Undo
            txtUserName.Text = "SQUARE"
            ReDim Back(Wave.Length)
            Wave.CopyTo(Back, 0)
            cmdUserUndo.Enabled = True
            lblWrf1.Text = "Output Freq."
            UpdateWrf()
            Phase = CDbl(Val(SetCur(JDH_PHASE)))

            Dim dfreq As Double = GetFlatValue(txtSampleFreq.Text)
            Wave = BasicFunctionGenerator.GenerateSquareWave(dfreq, Vpk, Phase, 0.0, (dfreq * NumSamps) / NumCycles, NumSamps, dutyCycle)

            YAxis1.Range = New Range(-1 * (GetFlatValue(txtAmpl.Text) + GetFlatValue(txtDcOffs.Text)), GetFlatValue(txtAmpl.Text) + GetFlatValue(txtDcOffs.Text))
            XAxis.Range = New Range(0, Wave.Length)
            CursorMarker.XPosition = Wave.Length / 2
            AdjustUserWave()
            lblModFreq.Visible = False
            lblModFreq1.Visible = False
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub cmdUserUndo_MouseClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdUserUndo.Click
        If Val(SetCur(LIST_DEF)) <> lListDefine Then
            SetCur(LIST_DEF) = lListDefine
            cmdZoomOut_Click()
            txtUserPoints.Text = EngNotate(Val(SetCur(LIST_DEF)), LIST_DEF)
            UpdateWrf()
            ReDim Wave(UBound(Back))
        Else
            SetCur(LIST_DEF) = lListDefine
        End If

        ReDim Wave(Back.Length)
        Back.CopyTo(Wave, 0)

        YAxis1.Range = New Range(-1 * (GetFlatValue(txtAmpl.Text) + GetFlatValue(txtDcOffs.Text)), GetFlatValue(txtAmpl.Text) + GetFlatValue(txtDcOffs.Text))
        XAxis.Range = New Range(0, Wave.Length)
        XSpan = XAxis.Range.Maximum - XAxis.Range.Minimum
        YSpan = YAxis1.Range.Maximum - YAxis1.Range.Minimum
        CursorMarker.XPosition = Wave.Length / 2
        G1.InteractionModeDefault = GraphDefaultInteractionMode.None
        cmdZoom.BackColor = Color.White
        cmdPan.BackColor = Color.White
        AdjustUserWave()
        cmdUserUndo.Enabled = False
    End Sub

    Private Sub cmdZoom_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdZoom.Click
        If Not G1.InteractionModeDefault = GraphDefaultInteractionMode.ZoomXY Then
            G1.InteractionModeDefault = GraphDefaultInteractionMode.ZoomXY
            cmdZoom.BackColor = Color.Silver
            cmdPan.BackColor = Color.White
            cmdSnap.BackColor = Color.White
            Me.Cursor = Cursors.SizeAll
            CursorTo.Visible = False
            HelpPanel("Box to zoom-in, Ctl-RightMouseClick to undo last zoom")
        Else
            G1.InteractionModeDefault = GraphDefaultInteractionMode.None
            cmdZoom.BackColor = Color.White
            Me.Cursor = Cursors.Default
            CursorTo.Visible = True
            HelpPanel("")
        End If
    End Sub

    Public Sub cmdZoomOut_MouseClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdZoomOut.Click
        cmdZoomOut_Click()
    End Sub

    Public Sub cmdZoomOut_Click()
        G1.ClearData()
        G1.PlotY(Wave, 1, 1)
        G1.InteractionModeDefault = GraphDefaultInteractionMode.None
        cmdZoom.BackColor = Color.White
        cmdPan.BackColor = Color.White
        If Not bActivatingControls Then Me.Cursor = Cursors.Default
        SizeYAxis()
        XAxis.Range = New Range(1, Val(SetCur(LIST_DEF)))
        XSpan = XAxis.Range.Maximum - XAxis.Range.Minimum
        HelpPanel("")
    End Sub

    Private Sub frmHpE1445a_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If InStr(sTipMode, "TIP_") Then
            Me.Top = 0
            Me.Left = PrimaryScreen.Bounds.Width - Me.Width
        Else
            'Center this form
            Me.Top = PrimaryScreen.Bounds.Height / 2 - Me.Height / 2
            Me.Left = PrimaryScreen.Bounds.Width / 2 - Me.Width / 2
        End If

        'These were added because the toolbar doesn't always stay where
        ' you set it at design time.
        tolFuncWave.Top = 6
        tolFuncWave.Left = 2
        'Note - common controls were deleted to accommodate unique arb requirements
        '   Set Common Controls parent properties
        Atlas_SFP.Parent_Object = Me
        '    Set Panel_Conifg.Parent_Object = Me

        ArbMain.Main()
    End Sub

    Private Sub frmHpE1445a_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseMove
        If Not bActivatingControls Then
            Me.Cursor = Cursors.Default
            HelpPanel("")
        End If
    End Sub

    Private Sub frmHpE1445a_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        Form_Resize()
    End Sub
    Public Sub Form_Resize()
        If DoNotTalk Then Exit Sub
        If Me.WindowState = MINIMIZED Then Exit Sub
        ' based on window size of 784, 476
        tabOptions.SetBounds(0, 6, Limit(Me.Width - 28, 0), Limit(Me.Height - 103, 0), BoundsSpecified.Location Or BoundsSpecified.Size)
        cmdHelp.SetBounds(Limit(Me.Width - 312, 0), Limit(Me.Height - 91, 0), 0, 0, BoundsSpecified.Location)
        cmdReset.SetBounds(Limit(Me.Width - 203, 0), Limit(Me.Height - 91, 0), 0, 0, BoundsSpecified.Location)
        cmdQuit.SetBounds(Limit(Me.Width - 107, 0), Limit(Me.Height - 91, 0), 0, 0, BoundsSpecified.Location)
        panMainControl.SetBounds(Limit(Me.Width - 341, 0), Limit(Me.Height - 205, 0), 0, 0, BoundsSpecified.Location)
        G1.SetBounds(72, 71, Limit(tabOptions.Width - 340, 0), Limit(tabOptions.Height - 110, 0), BoundsSpecified.Location Or BoundsSpecified.Size)
    End Sub

    Private Sub Form_Unload(ByRef Cancel As Short)
        'call this function because user used "X" to close out of program
        cmdQuit_Click()
    End Sub

    Private Sub frmHpE1445a_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Dim Cancel As Short = 0
        Form_Unload(Cancel)
        If Cancel <> 0 Then e.Cancel = True
    End Sub

    Private Sub fraAgileFsk_MouseHover(sender As Object, e As EventArgs) Handles fraAgileFsk.MouseHover
        HelpPanel("")
    End Sub

    Private Sub fraFreqMode_MouseHover(sender As Object, e As EventArgs) Handles fraFreqMode.MouseHover
        HelpPanel("")
    End Sub

    Private Sub fraAmpl_MouseHover(sender As Object, e As EventArgs) Handles fraAmpl.MouseHover
        HelpPanel("")
    End Sub

    Private Sub fraDcOffs_MouseHover(sender As Object, e As EventArgs) Handles fraDcOffs.MouseHover
        ShowVals(VOLT_OFFS)
    End Sub

    Private Sub fraFreq_MouseHover(sender As Object, e As EventArgs) Handles fraFreq.MouseHover
    End Sub

    Private Sub fraFuncWave_MouseHover(sender As Object, e As EventArgs) Handles fraFuncWave.MouseHover
        HelpPanel("")
    End Sub

    Private Sub fraMarkEclt0_MouseHover(sender As Object, e As EventArgs) Handles fraMarkEclt0.MouseHover
        HelpPanel("")
    End Sub

    Private Sub fraMarkEclt1_MouseHover(sender As Object, e As EventArgs) Handles fraMarkEclt1.MouseHover
        HelpPanel("")
    End Sub

    Private Sub fraMarkExt_MouseHover(sender As Object, e As EventArgs) Handles fraMarkExt.MouseHover
        HelpPanel("")
    End Sub

    Private Sub fraMarkSpo_MouseHover(sender As Object, e As EventArgs) Handles fraMarkSpo.MouseHover
        ShowVals(LIST_MARK_SPO)
    End Sub

    Private Sub fraArmLay_MouseHover(sender As Object, e As EventArgs) Handles fraArmLay.MouseHover
        HelpPanel("")
    End Sub

    Private Sub fraTrigStart_MouseHover(sender As Object, e As EventArgs) Handles fraTrigStart.MouseHover
        HelpPanel("")
    End Sub

    Private Sub fraTrigStop_MouseHover(sender As Object, e As EventArgs) Handles fraTrigStop.MouseHover
        HelpPanel("")
    End Sub

    Private Sub fraWavePoin_MouseHover(sender As Object, e As EventArgs) Handles fraWavePoin.MouseHover
        ShowVals(RAMP_POIN)
    End Sub

    Private Sub fraWavePol_MouseHover(sender As Object, e As EventArgs) Handles fraWavePol.MouseHover
        HelpPanel("")
    End Sub

    Private Sub G1_MouseDownEvent(ByVal sender As Object, ByVal e As MouseEventArgs) Handles G1.MouseDown
        If (e.Button = Windows.Forms.MouseButtons.Right) And
           Not (Control.ModifierKeys = Keys.Control) And
           (G1.InteractionModeDefault <> GraphDefaultInteractionMode.None) Then ' NI Waveform uses a control  right click to undo the last zoom or pan from the stack
            G1.InteractionModeDefault = GraphDefaultInteractionMode.None
            cmdZoom.BackColor = Color.White
            cmdPan.BackColor = Color.White
            HelpPanel("")
        End If
    End Sub

    Private Sub G1_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles G1.MouseMove
        If G1.InteractionModeDefault = GraphDefaultInteractionMode.ZoomXY Then
            Cursor = Cursors.Cross
        ElseIf G1.InteractionModeDefault = GraphDefaultInteractionMode.PanXY Then
            Cursor = Cursors.SizeAll
        End If
    End Sub

    Private Sub G1_PlotAreaMouseDown(sender As Object, e As MouseEventArgs) Handles G1.PlotAreaMouseDown
        If G1.InteractionModeDefault = GraphDefaultInteractionMode.None Then
            If (e.Button = Windows.Forms.MouseButtons.Right) And (Me.Cursor <> Cursors.Default) Then
                Me.Cursor = Cursors.Default
            ElseIf e.Button = Windows.Forms.MouseButtons.Right Then
                Me.Cursor = Cursors.Default
                CursorFrom.XPosition = CursorTo.XPosition
                CursorFrom.YPosition = CursorTo.YPosition
            Else
                lListDefine = Val(SetCur(LIST_DEF))
                ReDim Back(Wave.Length)
                Array.Copy(Wave, Back, Wave.Length)
                cmdUserUndo.Enabled = True
                PlotLineInWave(CursorTo.XPosition, CursorTo.YPosition)
                lblWrf1.Text = "Segment Freq."
                lblModFreq.Visible = False
                lblModFreq1.Visible = False
                UpdateWrf()
            End If
        End If
    End Sub

    Private Sub G1_PlotAreaMouseMove(sender As Object, e As MouseEventArgs) Handles G1.PlotAreaMouseMove
        Dim Frequency As Single
        Dim pt As PointF = G1.PointToVirtual(New Point(e.X, e.Y))

        If G1.InteractionModeDefault = GraphDefaultInteractionMode.None Then
            CursorTo.SnapMode = CursorSnapMode.Floating
            CursorTo.Visible = True
            CursorTo.XPosition = XSpan * pt.X
            If pt.Y = 0.5 Then
                CursorTo.YPosition = 0.0 + GetFlatValue(txtDcOffs.Text)
            ElseIf pt.Y > 0.5 Then
                CursorTo.YPosition = ((pt.Y - 0.5) * 2 * YAxis1.Range.Maximum) + GetFlatValue(txtDcOffs.Text)
            Else
                CursorTo.YPosition = ((0.5 - pt.Y) * 2 * YAxis1.Range.Minimum) + GetFlatValue(txtDcOffs.Text)
            End If
            If cmdSnap.BackColor = Color.Silver Then
                CursorTo.SnapMode = CursorSnapMode.NearestPoint
            End If
            G1.Refresh()
            Frequency = Val(SetCur(FREQ))

            HelpPanel("Voltage: " & EngNotate(CursorTo.YPosition, VOLT) & ",       Time: " & EngNotate((1 / Frequency * CursorTo.XPosition), JDH_TIME))
            If e.Button = Windows.Forms.MouseButtons.Left Then
                PlotLineInWave(CursorTo.XPosition, CursorTo.YPosition)
            End If
        End If
    End Sub

    Private Sub Help_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdHelp.Click
        frmHelpDialog.ShowDialog()
        'ShellExecute 0, "Open", "http://gelsana.com/Arb_AgE1445A_BETA/Vendor/Doc/", 0, 0, SW_SHOWNORMAL
    End Sub

    '#Const defHas_imgLogo = True
#If defHas_imgLogo Then
    Private Sub imgLogo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles imgLogo.Click
        Me.WindowState = FormWindowState.Normal
'The appropriate value for these in pixels instead of twips will have to be calculated once the need for this function is determined
'        Me.Width = 6000
'        Me.Height = 6000
        Form_Resize()
    End Sub

    Private Sub imgLogo_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles imgLogo.MouseDown
        Static Prompt As String = ""
        Dim ReturnString As String

        If e.Button = Windows.Forms.MouseButtons.Left And Control.ModifierKeys = Keys.Control Then ' If Ctrl-LEFT button
            Prompt = InputBox("Enter an instrument command:", , Prompt)
            If InStr(UCase(Prompt), "DEBUG ON") Then
                LiveMode = False
            ElseIf InStr(UCase(Prompt), "DEBUG OFF") Then
                LiveMode = True
            Else
                WriteInstrument(Prompt)
            End If
            If InStr(Prompt, "?") Then
                ReturnString = ReadARB()
                If ReturnString <> "" Then
                    MsgBox(ReturnString, MsgBoxStyle.Information, "Read from instrument:")
                End If
            End If
        ElseIf e.Button = Windows.Forms.MouseButtons.Left And Control.ModifierKeys = Keys.Alt Then            ' If Alt-LEFT button
            ReturnString = ReadARB()
            If ReturnString <> "" Then
                MsgBox(ReturnString, MsgBoxStyle.Information, "Read from instrument:")
            End If
        ElseIf e.Button = Windows.Forms.MouseButtons.Right Then            ' If Right button
            WriteInstrument("SYST:ERR?")
            ReturnString = ReadARB()
            If ReturnString <> "" Then
                MsgBox(ReturnString, MsgBoxStyle.Information, "Read from instrument:")
            End If
        End If
    End Sub
#End If

    Private Sub lstListCat_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstListCat.SelectedIndexChanged
        If lstListCat.Text.Length > 0 Then
            SetSend((LIST_SEL), Strings.Left(lstListCat.Text, InStr(lstListCat.Text, ",") - 1))
            lblListSel.Text = SetCur(LIST_SEL)
        End If
    End Sub

    Private Sub lstListCat_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstListCat.DoubleClick
        cmdSegAdd_Click()
    End Sub

    Private Sub lstListSseqSeq_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lstListSseqSeq.MouseDown
        Index = lstListSseqSeq.SelectedIndex
        txtDwelCoun.Text = Mid(lstListSseqSeq.Text, InStr(lstListSseqSeq.Text, ",") + 2)
        SetCur(JDH_DWEL_COUN) = txtDwelCoun.Text
        panDwelCoun.Enabled = True
    End Sub

    Private Sub optClipboard_Click()
        '   'cmdAtlas.Enabled = True
    End Sub

    Public Sub optFreqModeFix_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optFreqModeFix.CheckedChanged
        optFreqModeFix_Click(True)
    End Sub
    Public Sub optFreqModeFix_Click(ByVal Value As Boolean)
        fraFreqModeSwe.Visible = False
        fraAgileFsk.Visible = False
        fraFreq.Visible = True
        SetSend(FREQ_MODE, "FIX")
    End Sub

    Public Sub optFreqModeFsk_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optFreqModeFsk.CheckedChanged
        optFreqModeFsk_Click(True)
    End Sub

    Public Sub optFreqModeFsk_Click(ByVal Value As Boolean)
        fraFreqModeSwe.Visible = False
        fraAgileFsk.Visible = True
        fraFreq.Visible = False
        AdjustAgileLimits()
        SetSend(FREQ_MODE, "FSK")
    End Sub

    Public Sub optFreqModeSwe_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optFreqModeSwe.CheckedChanged
        optFreqModeSwe_Click(True)
    End Sub
    Public Sub optFreqModeSwe_Click(ByVal Value As Boolean)
        fraFreqModeSwe.Visible = True
        fraAgileFsk.Visible = False
        fraFreq.Visible = False
        AdjustAgileLimits()
        SetSend(FREQ_MODE, "SWE")
    End Sub

    Public Sub optCutOff10_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optCutOff10.CheckedChanged
        optCutOff10_Click(True)
    End Sub
    Public Sub optCutOff10_Click(ByVal Value As Boolean)
        SetCur(OUTP_FILT_FREQ) = "10 MHz"
        SendCommand(OUTP_FILT_FREQ)
    End Sub

    Public Sub optCutOff250_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optCutOff250.CheckedChanged
        optCutOff250_Click(True)
    End Sub
    Public Sub optCutOff250_Click(ByVal Value As Boolean)
        SetCur(OUTP_FILT_FREQ) = "250 kHz"
        SendCommand(OUTP_FILT_FREQ)
    End Sub

    Public Sub optLoadImp50_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optLoadImp50.CheckedChanged
        optLoadImp50_Click(True)
    End Sub
    Public Sub optLoadImp50_Click(ByVal Value As Boolean)
        SetCur(OUTP_LOAD_AUTO) = "ON"
        SetCur(OUTP_LOAD) = "50"
        SetSend(OUTP_IMP, "50")
        If cboUnits.Items.Count = 5 Then
            cboUnits.Items.Add("W")
        End If
    End Sub

    Public Sub optLoadImp75_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optLoadImp75.CheckedChanged
        optLoadImp75_Click(True)
    End Sub
    Public Sub optLoadImp75_Click(ByVal Value As Boolean)
        SetCur(OUTP_LOAD_AUTO) = "ON"
        SetCur(OUTP_LOAD) = "75"
        SetSend(OUTP_IMP, "75")
        If cboUnits.Items.Count = 5 Then
            cboUnits.Items.Add("W")
        End If
    End Sub

    Public Sub optLoadImpHigh_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optLoadImpHigh.CheckedChanged
        optLoadImpHigh_Click(True)
    End Sub
    Public Sub optLoadImpHigh_Click(ByVal Value As Boolean)
        SetCur(OUTP_LOAD_AUTO) = "OFF"
        SetCur(OUTP_LOAD) = "INF"
        SetSend(OUTP_LOAD, "INF")
        If cboUnits.Items.Count > 5 Then
            cboUnits.Items.RemoveAt(5) '"W"
        End If
    End Sub

    Private Sub optTextFile_Click()
        'cmdAtlas.Enabled = True
    End Sub

    Private Sub panAgileSweepDur_MouseMove(sender As Object, e As MouseEventArgs) Handles panAgileSweepDur.MouseMove
        ShowVals(SWE_TIME)
    End Sub

    Private Sub panAmpl_MouseMove(sender As Object, e As MouseEventArgs) Handles panAmpl.MouseMove
        ShowVals(VOLT)
    End Sub

    Private Sub panAmplDiv7_MouseMove(sender As Object, e As MouseEventArgs) Handles panAmplDiv7.MouseMove
        ShowVals(VOLT_DIV_7)
    End Sub

    Private Sub panRoscFreqExt_MouseMove(sender As Object, e As MouseEventArgs) Handles panRoscFreqExt.MouseMove
        ShowVals(ROSC_FREQ_EXT)
    End Sub

    Private Sub spnAgileFskKeyH_DownButtonClicked(sender As Object, e As EventArgs) Handles spnAgileFskKeyH.DownButtonClicked
        Spin10Pct(txtAgileFskKeyH, "Down", FREQ_FSK_H)
    End Sub

    Private Sub spnAgileFskKeyH_UpButtonClicked(sender As Object, e As EventArgs) Handles spnAgileFskKeyH.UpButtonClicked
        Spin10Pct(txtAgileFskKeyH, "Up", FREQ_FSK_H)
    End Sub

    Private Sub spnAgileFskKeyL_DownButtonClicked(sender As Object, e As EventArgs) Handles spnAgileFskKeyL.DownButtonClicked
        Spin10Pct(txtAgileFskKeyL, "Down", FREQ_FSK_L)
    End Sub

    Private Sub spnAgileFskKeyL_UpButtonClicked(sender As Object, e As EventArgs) Handles spnAgileFskKeyL.UpButtonClicked
        Spin10Pct(txtAgileFskKeyL, "Up", FREQ_FSK_L)
    End Sub

    Private Sub spnAgileSweepDur_DownButtonClicked(sender As Object, e As EventArgs) Handles spnAgileFskKeyL.DownButtonClicked
        Spin10Pct(txtAgileSweepDur, "Down", SWE_TIME)
    End Sub

    Private Sub spnAgileSweepDur_UpButtonClicked(sender As Object, e As EventArgs) Handles spnAgileSweepDur.UpButtonClicked
        Spin10Pct(txtAgileSweepDur, "Up", SWE_TIME)
    End Sub

    Private Sub spnAgileSweepDur_DownButtonClicked_1(sender As Object, e As EventArgs) Handles spnAgileSweepDur.DownButtonClicked
        Spin10Pct(txtAgileSweepDur, "Down", SWE_TIME)
    End Sub


    Private Sub spnAgileSweepStart_DownButtonClicked(sender As Object, e As EventArgs) Handles spnAgileSweepStart.DownButtonClicked
        Spin10Pct(txtAgileSweepStart, "Down", FREQ_STAR)
        AdjustAgileLimits()
    End Sub

    Private Sub spnAgileSweepStart_UpButtonClicked(sender As Object, e As EventArgs) Handles spnAgileSweepStart.UpButtonClicked
        Spin10Pct(txtAgileSweepStart, "Up", FREQ_STAR)
        AdjustAgileLimits()
    End Sub

    Private Sub spnAgileSweepSteps_DownButtonClicked(sender As Object, e As EventArgs) Handles spnAgileSweepSteps.DownButtonClicked
        Spin10Pct(txtSweCoun, "Down", SWE_COUN)
    End Sub

    Private Sub spnAgileSweepSteps_UpButtonClicked(sender As Object, e As EventArgs) Handles spnAgileSweepSteps.UpButtonClicked
        Spin10Pct(txtSweCoun, "Up", SWE_COUN)
    End Sub

    Private Sub spnAgileSweepStop_DownButtonClicked(sender As Object, e As EventArgs) Handles spnAgileSweepStop.DownButtonClicked
        Spin10Pct(txtAgileSweepStop, "Down", FREQ_STOP)
        AdjustAgileLimits()
    End Sub

    Private Sub spnAgileSweepStop_UpButtonClicked(sender As Object, e As EventArgs) Handles spnAgileSweepStop.UpButtonClicked
        Spin10Pct(txtAgileSweepStop, "Up", FREQ_STOP)
        AdjustAgileLimits()
    End Sub

    Private Sub spnAmpl_DownButtonClicked(sender As Object, e As EventArgs) Handles spnAmpl.DownButtonClicked
        Spin10Pct(txtAmpl, "Down", VOLT)
    End Sub

    Private Sub spnAmpl_UpButtonClicked(sender As Object, e As EventArgs) Handles spnAmpl.UpButtonClicked
        Spin10Pct(txtAmpl, "Up", VOLT)
    End Sub

    Private Sub spnAmplDiv7_DownButtonClicked(sender As Object, e As EventArgs) Handles spnAmplDiv7.DownButtonClicked
        Spin10Pct(txtAmplDiv7, "Down", VOLT_DIV_7)
        SetAmplFromDiv7()
    End Sub

    Private Sub spnAmplDiv7_UpButtonClicked(sender As Object, e As EventArgs) Handles spnAmplDiv7.UpButtonClicked
        Spin10Pct(txtAmplDiv7, "Up", VOLT_DIV_7)
        SetAmplFromDiv7()
    End Sub

    Private Sub spnArmLay2Coun_DownButtonClicked(sender As Object, e As EventArgs) Handles spnArmLay2Coun.DownButtonClicked
        Spin10Pct(txtArmLay2Coun, "Down", ARM_LAY2_COUN)
    End Sub

    Private Sub spnArmLay2Coun_UpButtonClicked(sender As Object, e As EventArgs) Handles spnArmLay2Coun.UpButtonClicked
        Spin10Pct(txtArmLay2Coun, "Up", ARM_LAY2_COUN)
    End Sub

    Private Sub spnDcOffs_DownButtonClicked(sender As Object, e As EventArgs) Handles spnDcOffs.DownButtonClicked
        Spin10Pct(txtDcOffs, "Down", VOLT_OFFS)
        AdjustVoltLimits(VOLT_OFFS)
    End Sub

    Private Sub spnDcOffs_UpButtonClicked(sender As Object, e As EventArgs) Handles spnDcOffs.UpButtonClicked
        Spin10Pct(txtDcOffs, "Up", VOLT_OFFS)
        AdjustVoltLimits(VOLT_OFFS)
    End Sub

    Private Sub spnDwelCoun_DownButtonClicked(sender As Object, e As EventArgs) Handles spnDwelCoun.DownButtonClicked
        Dim i As Short, S As String

        Index = lstListSseqSeq.SelectedIndex
        If Index = -1 Or Index > lstListSseqSeq.Items.Count - 1 Then Exit Sub
        i = InStr(lstListSseqSeq.Items.Item(Index).ToString(), ",")
        S = Strings.Left(lstListSseqSeq.Items.Item(Index).ToString(), i + 1)
        DoNotTalk = True
        Spin10Pct(txtDwelCoun, "Down", JDH_DWEL_COUN)
        DoNotTalk = False
        lstListSseqSeq.Items.Item(Index) = S & txtDwelCoun.Text
        SendSequence()
    End Sub

    Private Sub spnDwelCoun_UpButtonClicked(sender As Object, e As EventArgs) Handles spnDwelCoun.UpButtonClicked
        Dim i As Short, S As String

        Index = lstListSseqSeq.SelectedIndex
        If Index = -1 Or Index > lstListSseqSeq.Items.Count - 1 Then Exit Sub
        i = InStr(lstListSseqSeq.Items.Item(Index).ToString(), ",")
        S = Strings.Left(lstListSseqSeq.Items.Item(Index).ToString(), i + 1)
        DoNotTalk = True
        Spin10Pct(txtDwelCoun, "Up", JDH_DWEL_COUN)
        DoNotTalk = False
        lstListSseqSeq.Items.Item(Index) = S & txtDwelCoun.Text
        SendSequence()
    End Sub

    Private Sub spnFreq_DownButtonClicked(sender As Object, e As EventArgs) Handles spnFreq.DownButtonClicked
        Dim OrigVal As String, NewVal As String

        OrigVal = SetCur(FREQ2)
        NewVal = SetCur(FREQ2)
        Spin10Pct(txtFreq, "Down", CurFreqIdx)
        If CurFreqIdx = FREQ2 And SetCur(FREQ2) <> SetMin(FREQ2) Then
            While SetCur(FREQ2) = OrigVal
                NewVal = Val(NewVal) * 0.9
                SetCur(FREQ2) = NewVal
                Spin10Pct(txtFreq, "Down", FREQ2)
            End While
        End If
        txtSampleFreq.Text = txtFreq.Text
        UpdateWrf()
    End Sub

    Private Sub spnFreq__UpButtonClicked(sender As Object, e As EventArgs) Handles spnFreq.UpButtonClicked
        Dim OrigVal As String, NewVal As String

        OrigVal = SetCur(FREQ2)
        NewVal = SetCur(FREQ2)
        Spin10Pct(txtFreq, "Up", CurFreqIdx)
        If CurFreqIdx = FREQ2 And SetCur(FREQ2) <> SetMax(FREQ2) Then
            While SetCur(FREQ2) = OrigVal
                NewVal = Val(NewVal) * 1.1
                SetCur(FREQ2) = NewVal
                Spin10Pct(txtFreq, "Up", FREQ2)
            End While
        End If
        txtSampleFreq.Text = txtFreq.Text
        UpdateWrf()
    End Sub

    Private Sub spnMarkSpo_DownButtonClicked(sender As Object, e As EventArgs) Handles spnMarkSpo.DownButtonClicked
        Spin10Pct(txtMarkSpo, "Down", LIST_MARK_SPO)
        ProcessMarker()
    End Sub

    Private Sub spnMarkSpo_UpButtonClicked(sender As Object, e As EventArgs) Handles spnMarkSpo.UpButtonClicked
        Spin10Pct(txtMarkSpo, "Up", LIST_MARK_SPO)
        ProcessMarker()
    End Sub

    Private Sub spnRoscFreqExt_DownButtonClicked(sender As Object, e As EventArgs) Handles spnRoscFreqExt.DownButtonClicked
        DoNotTalk = True
        Spin10Pct(txtRoscFreqExt, "Down", ROSC_FREQ_EXT)
        DoNotTalk = False
        RefOscFreq = SetCur(ROSC_FREQ_EXT)
        SendCommand(ROSC_FREQ_EXT)
        AdjustFreqLimits()
        UpdateWrf()
    End Sub

    Private Sub spnRoscFreqExt_UpButtonClicked(sender As Object, e As EventArgs) Handles spnRoscFreqExt.UpButtonClicked
        DoNotTalk = True
        Spin10Pct(txtRoscFreqExt, "Up", ROSC_FREQ_EXT)
        DoNotTalk = False
        RefOscFreq = SetCur(ROSC_FREQ_EXT)
        SendCommand(ROSC_FREQ_EXT)
        AdjustFreqLimits()
        UpdateWrf()
    End Sub

    Private Sub spnSampleFreq__DownButtonClicked(sender As Object, e As EventArgs) Handles spnSampleFreq.DownButtonClicked
        spnFreq_DownButtonClicked(sender, e)
    End Sub

    Private Sub spnSampleFreq__UpButtonClicked(sender As Object, e As EventArgs) Handles spnSampleFreq.UpButtonClicked
        spnFreq__UpButtonClicked(sender, e)
    End Sub


    Private Sub spnArmCount_DownButtonClicked(sender As Object, e As EventArgs) Handles spnArmCount.DownButtonClicked
        Spin10Pct(txtArmCoun, "Down", ARM_COUN)
    End Sub

    Private Sub spnArmCount_UpButtonClicked(sender As Object, e As EventArgs) Handles spnArmCount.UpButtonClicked
        Spin10Pct(txtArmCoun, "Up", ARM_COUN)
    End Sub

    Private Sub spnSweCoun_DownButtonClicked(sender As Object, e As EventArgs) Handles spnSweCoun.DownButtonClicked
        Spin10Pct(txtAgileSweepSteps, "Down", SWE_POIN)
    End Sub

    Private Sub spnSweCoun_UpButtonClicked(sender As Object, e As EventArgs) Handles spnSweCoun.UpButtonClicked
        Spin10Pct(txtAgileSweepSteps, "Up", SWE_POIN)
    End Sub

    Private Sub spnUserPoints_DownButtonClicked(sender As Object, e As EventArgs) Handles spnUserPoints.DownButtonClicked
        lListDefine = Val(SetCur(LIST_DEF))
        DoNotTalk = True
        Spin10Pct(txtUserPoints, "Down", LIST_DEF)
        DoNotTalk = False
        SetMax(LIST_MARK_SPO) = SetCur(LIST_DEF)
        lblWrf1.Text = "Segment Freq."
        lblModFreq.Visible = False
        lblModFreq1.Visible = False
        UpdateGraph()
    End Sub

    Private Sub spnUserPoints_UpButtonClicked(sender As Object, e As EventArgs) Handles spnUserPoints.UpButtonClicked
        lListDefine = Val(SetCur(LIST_DEF))
        DoNotTalk = True
        Spin10Pct(txtUserPoints, "Up", LIST_DEF)
        DoNotTalk = False
        SetMax(LIST_MARK_SPO) = SetCur(LIST_DEF)
        lblWrf1.Text = "Segment Freq."
        lblModFreq.Visible = False
        lblModFreq1.Visible = False
        UpdateGraph()
    End Sub

    Private Sub spnWavePoin_DownButtonClicked(sender As Object, e As EventArgs) Handles spnWavePoin.DownButtonClicked
        Spin10Pct(txtWavePoin, "Down", RAMP_POIN)
    End Sub

    Private Sub spnWavePoin_UpButtonClicked(sender As Object, e As EventArgs) Handles spnWavePoin.UpButtonClicked
        Spin10Pct(txtWavePoin, "Up", RAMP_POIN)
    End Sub

    Private tabOptions_PreviousTab As Integer
    Private Sub tabOptions_Deselecting(ByVal sender As System.Object, ByVal e As TabControlCancelEventArgs) Handles tabOptions.Deselecting
        tabOptions_PreviousTab = e.TabPageIndex
    End Sub
    Private Sub tabOptions_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tabOptions.SelectedIndexChanged
        Dim PreviousTab As Integer = tabOptions_PreviousTab

        Select Case tabOptions.TabPages(PreviousTab).Text
            Case "User"
        End Select

        If tabOptions.SelectedTab.Text <> "User" Then
            Me.Cursor = Cursors.Default
            G1.Visible = False
            cmdZoomOut_Click()
        Else
            G1.Visible = True
            ApplyDcOffsToWave()
            SizeYAxis()
            UpdateWrf()
        End If

        Select Case tabOptions.SelectedTab.Text
            Case "User"
                panMainControl.Visible = False

            Case Else
                panMainControl.Visible = True
        End Select
    End Sub

    Private Sub tabOptions_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles tabOptions.MouseMove
        If Not bActivatingControls Then
            Me.Cursor = Cursors.Default
            HelpPanel("")
        End If
    End Sub

    Private Sub timDebug_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles timDebug.Tick
        timDebug.Enabled = False
        ConfigGetCurrent()
        If bTimerEnable = True Then
            timDebug.Enabled = True
        End If
    End Sub

    Public Sub tolFuncWave_ButtonClick(ByVal sender As Object, ByVal e As ToolBarButtonClickEventArgs) Handles tolFuncWave.ButtonClick

        WaveIdx = tolFuncWave.Buttons.IndexOf(e.Button) + 1

        For Each Button As ToolBarButton In tolFuncWave.Buttons
            Button.Pushed = False
        Next
        e.Button.Pushed = True

       Select Case e.Button.Name
            Case "Sine"
                SetCur(FUNC) = "SIN"
                If ribInit.Pushed = True Then SendCommand(ABOR)
                cboTrigSour.Text = cboTrigSour.Items.Item(5).ToString() '"Internal1"
                cboTrigSour_SelectedIndexChanged(Me, New System.EventArgs())
                lblTrigSour.Visible = False
                cboTrigSour.Visible = False
                If optFreqModeFix.Checked Then fraFreq.Visible = True
                fraRoscSour.Visible = True
                fraAmpl.Visible = True
                SetUOM(VOLT) = cboUnits.Text
                SetUOM(VOLT_DIV_7) = cboUnits.Text
                fraDcOffs.Visible = True
                fraWavePol.Visible = False
                fraWavePoin.Visible = False
                cboUnits.Visible = True
                fraAmplUnits.Visible = True
                fraFreq.Text = "Frequency"
                If ribInit.Pushed Then
                    AdjustVoltLimits(FUNC)
                    SendCommand(FUNC)
                    SendCommand(INIT)
                End If
                fraFreqMode.Visible = True
                ribInit.Visible = True
                ribAbor.Visible = True
                fraOutLowPassFilt.Visible = True
                tabOptions.TabPages.Item(TAB_ARM).Enabled = True
                tabOptions.TabPages.Item(TAB_MARKERS).Enabled = True
                tabOptions.TabPages.Item(TAB_AGILE).Enabled = True
                tabOptions.TabPages.Item(TAB_USER).Enabled = False
                tabOptions.TabPages.Item(TAB_SEQUENCE).Enabled = False
            Case "Square"
                SetCur(FUNC) = "SQU"
                AdjustFreqLimits()
                If optFreqModeFix.Checked Then fraFreq.Visible = True
                fraAmpl.Visible = True
                SetUOM(VOLT) = cboUnits.Text
                SetUOM(VOLT_DIV_7) = cboUnits.Text
                fraDcOffs.Visible = True
                fraWavePol.Visible = True
                fraWavePoin.Visible = False
                cboUnits.Visible = True
                fraAmplUnits.Visible = True
                fraFreq.Text = "Frequency"
                fraRoscSour.Visible = True
                lblTrigSour.Visible = True
                cboTrigSour.Visible = True
                fraFreqMode.Visible = True
                ribInit.Visible = True
                ribAbor.Visible = True
                fraOutLowPassFilt.Visible = True
                tabOptions.TabPages.Item(TAB_ARM).Enabled = True
                tabOptions.TabPages.Item(TAB_MARKERS).Enabled = True
                tabOptions.TabPages.Item(TAB_AGILE).Enabled = True
                tabOptions.TabPages.Item(TAB_USER).Enabled = False
                tabOptions.TabPages.Item(TAB_SEQUENCE).Enabled = False
            Case "Ramp", "Triangle"
                If e.Button.Name = "Ramp" Then
                    SetCur(FUNC) = "RAMP"
                Else
                    SetCur(FUNC) = "TRI"
                End If
                AdjustFreqLimits()
                If optFreqModeFix.Checked Then fraFreq.Visible = True
                fraRoscSour.Visible = True
                fraAmpl.Visible = True
                SetUOM(VOLT) = cboUnits.Text
                SetUOM(VOLT_DIV_7) = cboUnits.Text
                fraDcOffs.Visible = True
                fraWavePol.Visible = True
                fraWavePoin.Visible = True
                cboUnits.Visible = True
                fraAmplUnits.Visible = True
                fraFreq.Text = "Frequency"
                lblTrigSour.Visible = True
                cboTrigSour.Visible = True
                fraFreqMode.Visible = True
                ribInit.Visible = True
                ribAbor.Visible = True
                fraOutLowPassFilt.Visible = True
                tabOptions.TabPages.Item(TAB_ARM).Enabled = True
                tabOptions.TabPages.Item(TAB_MARKERS).Enabled = True
                tabOptions.TabPages.Item(TAB_AGILE).Enabled = True
                tabOptions.TabPages.Item(TAB_USER).Enabled = False
                tabOptions.TabPages.Item(TAB_SEQUENCE).Enabled = False
            Case "DC"
                SetCur(FUNC) = "DC"
                fraFreq.Visible = False
                fraRoscSour.Visible = False
                fraAmpl.Visible = True
                fraDcOffs.Visible = False
                fraWavePol.Visible = False
                fraWavePoin.Visible = False
                cboUnits.Visible = False
                fraAmplUnits.Visible = False
                cboUnits.Text = "V"
                txtAmpl.Text = EngNotate(SetCur(VOLT), VOLT)
                cboTrigSour.Visible = False
                fraFreqMode.Visible = False
                ribInit.Visible = False
                ribAbor.Visible = False
                If chkOutpStat.Checked = True Then
                    imgWaveDisplay.Image = imlOutRelayWave.Images(WaveIdx)
                End If
                AdjustVoltLimits(FUNC)
                SendCommand(FUNC)
                fraOutLowPassFilt.Visible = False
                tabOptions.TabPages.Item(TAB_ARM).Enabled = False
                tabOptions.TabPages.Item(TAB_MARKERS).Enabled = False
                tabOptions.TabPages.Item(TAB_AGILE).Enabled = False
                tabOptions.TabPages.Item(TAB_USER).Enabled = False
                tabOptions.TabPages.Item(TAB_SEQUENCE).Enabled = False
                Exit Sub
            Case "User"
                cboUnits.Text = "V" 'Added V1.4
                '      SetSend VOLT_UNIT, "V"     Removed V1.4
                '      SetUOM$(VOLT) = "V"        Removed V1.4
                '      SetUOM$(VOLT_DIV_7) = "V"  Removed V1.4
                SetCur(FUNC) = "USER"
                AdjustFreqLimits()
                If optFreqModeFix.Checked Then fraFreq.Visible = True
                fraRoscSour.Visible = True
                fraAmpl.Visible = True
                fraDcOffs.Visible = True
                fraWavePol.Visible = True
                fraWavePoin.Visible = False
                cboUnits.Visible = False
                fraAmplUnits.Visible = False
                fraFreq.Text = "Sample Frequency"
                lblTrigSour.Visible = True
                cboTrigSour.Visible = True
                fraFreqMode.Visible = False
                ribInit.Visible = True
                ribAbor.Visible = True
                If Initialized And lstListSseqSeq.Items.Count = 0 Then
                    ribInit.Pushed = False
                    ribAbor.Pushed = True
                   ribAbor_Click()
                    If lstListCat.Items.Count = 0 Then 'If nothing downloaded
                        MsgBox("You must down load a User waveform segment and sequence to initiate a User waveform.", MsgBoxStyle.Information)
                        tabOptions.SelectedIndex = TAB_USER
                    Else
                        MsgBox("You must define a segment sequence to initiate a User waveform.", MsgBoxStyle.Information)
                        tabOptions.SelectedIndex = TAB_SEQUENCE
                    End If
                End If
                fraOutLowPassFilt.Visible = True
                tabOptions.TabPages.Item(TAB_ARM).Enabled = True
                tabOptions.TabPages.Item(TAB_MARKERS).Enabled = True
                tabOptions.TabPages.Item(TAB_AGILE).Enabled = False
                tabOptions.TabPages.Item(TAB_USER).Enabled = True
                tabOptions.TabPages.Item(TAB_SEQUENCE).Enabled = True
        End Select
        AdjustVoltLimits(FUNC)
        SendCommand(FUNC)

        If ribInit.Pushed = True And chkOutpStat.Checked = True Then
            imgWaveDisplay.Image = imlOutRelayWave.Images(WaveIdx)
        Else
            imgWaveDisplay.Image = imlOutRelayWave.Images(0)
        End If
    End Sub

    Private Sub txtAgileFskKeyH_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAgileFskKeyH.Enter
        GotFocusSelect()
    End Sub

    Public Sub txtAgileFskKeyH_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAgileFskKeyH.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)

        txtAgileFskKeyH_KeyPress(KeyAscii)
        e.KeyChar = Chr(KeyAscii)
    End Sub

    Public Sub txtAgileFskKeyH_KeyPress(ByRef KeyAscii As Short)
        If KeyAscii <> Keys.Return Then Exit Sub
        KeyAscii = 0
        FormatEntry(txtAgileFskKeyH, FREQ_FSK_H)
    End Sub

    Private Sub txtAgileFskKeyH_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAgileFskKeyH.Leave
        txtAgileFskKeyH_KeyPress(Keys.Return)
        UserEnteringData = False
    End Sub

    Private Sub txtAgileFskKeyH_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtAgileFskKeyH.MouseMove
        ShowVals(FREQ_FSK_H)
    End Sub

    Private Sub txtAgileFskKeyL_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAgileFskKeyL.Enter
        GotFocusSelect()
    End Sub

    Public Sub txtAgileFskKeyL_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAgileFskKeyL.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)

        txtAgileFskKeyL_KeyPress(KeyAscii)
        e.KeyChar = Chr(KeyAscii)
    End Sub
    Public Sub txtAgileFskKeyL_KeyPress(ByRef KeyAscii As Short)
        If KeyAscii <> Keys.Return Then Exit Sub
        KeyAscii = 0
        FormatEntry(txtAgileFskKeyL, FREQ_FSK_L)
    End Sub

    Private Sub txtAgileFskKeyL_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAgileFskKeyL.Leave
        txtAgileFskKeyL_KeyPress(Keys.Return)
        UserEnteringData = False
    End Sub

    Private Sub txtAgileFskKeyL_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtAgileFskKeyL.MouseMove
        ShowVals(FREQ_FSK_L)
    End Sub

    Private Sub txtAgileSweepDur_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAgileSweepDur.Enter
        GotFocusSelect()
    End Sub

    Public Sub txtAgileSweepDur_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAgileSweepDur.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)

        txtAgileSweepDur_KeyPress(KeyAscii)
        e.KeyChar = Chr(KeyAscii)
    End Sub
    Public Sub txtAgileSweepDur_KeyPress(ByRef KeyAscii As Short)
        If KeyAscii <> Keys.Return Then Exit Sub
        KeyAscii = 0
        FormatEntry(txtAgileSweepDur, SWE_TIME)
    End Sub

    Private Sub txtAgileSweepDur_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAgileSweepDur.Leave
        txtAgileSweepDur_KeyPress(Keys.Return)
        UserEnteringData = False
    End Sub

    Private Sub txtAgileSweepDur_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtAgileSweepDur.MouseMove
        ShowVals(SWE_TIME)
    End Sub

    Private Sub txtAgileSweepStart_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAgileSweepStart.Enter
        GotFocusSelect()
    End Sub

    Public Sub txtAgileSweepStart_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAgileSweepStart.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)

        txtAgileSweepStart_KeyPress(KeyAscii)
        e.KeyChar = Chr(KeyAscii)
    End Sub
    Public Sub txtAgileSweepStart_KeyPress(ByRef KeyAscii As Short)
        If KeyAscii <> Keys.Return Then Exit Sub
        KeyAscii = 0
        FormatEntry(txtAgileSweepStart, FREQ_STAR)
        AdjustAgileLimits()
    End Sub

    Private Sub txtAgileSweepStart_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAgileSweepStart.Leave
        txtAgileSweepStart_KeyPress(Keys.Return)
        UserEnteringData = False
    End Sub

    Private Sub txtAgileSweepStart_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtAgileSweepStart.MouseMove
        ShowVals(FREQ_STAR)
    End Sub

    Private Sub txtAgileSweepSteps_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAgileSweepSteps.Enter
        GotFocusSelect()
    End Sub

    Public Sub txtAgileSweepSteps_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAgileSweepSteps.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)

        txtAgileSweepSteps_KeyPress(KeyAscii)
        e.KeyChar = Chr(KeyAscii)
    End Sub
    Public Sub txtAgileSweepSteps_KeyPress(ByRef KeyAscii As Short)
        If KeyAscii <> Keys.Return Then Exit Sub
        KeyAscii = 0
        FormatEntry(txtAgileSweepSteps, SWE_POIN)
    End Sub

    Private Sub txtAgileSweepSteps_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAgileSweepSteps.Leave
        txtAgileSweepSteps_KeyPress(Keys.Return)
        UserEnteringData = False
    End Sub

    Private Sub txtAgileSweepSteps_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtAgileSweepSteps.MouseMove
        ShowVals(SWE_POIN)
    End Sub

    Private Sub txtAgileSweepStop_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAgileSweepStop.Enter
        GotFocusSelect()
    End Sub

    Public Sub txtAgileSweepStop_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAgileSweepStop.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)

        txtAgileSweepStop_KeyPress(KeyAscii)
        e.KeyChar = Chr(KeyAscii)
    End Sub
    Public Sub txtAgileSweepStop_KeyPress(ByRef KeyAscii As Short)
        If KeyAscii <> Keys.Return Then Exit Sub
        KeyAscii = 0
        FormatEntry(txtAgileSweepStop, FREQ_STOP)
        AdjustAgileLimits()
    End Sub

    Private Sub txtAgileSweepStop_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAgileSweepStop.Leave
        txtAgileSweepStop_KeyPress(Keys.Return)
        UserEnteringData = False
    End Sub

    Private Sub txtAgileSweepStop_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtAgileSweepStop.MouseMove
        ShowVals(FREQ_STOP)
    End Sub

    Private Sub txtAmpl_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAmpl.Enter
        GotFocusSelect()
    End Sub

    Public Sub txtAmpl_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAmpl.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)

        txtAmpl_KeyPress(KeyAscii)
        e.KeyChar = Chr(KeyAscii)
    End Sub
    Public Sub txtAmpl_KeyPress(ByRef KeyAscii As Short)
        If KeyAscii <> Keys.Return Then Exit Sub
        KeyAscii = 0
        FormatEntry(txtAmpl, VOLT)
    End Sub

    Private Sub txtAmpl_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAmpl.Leave
        txtAmpl_KeyPress(Keys.Return)
        UserEnteringData = False
    End Sub

    Private Sub txtAmpl_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtAmpl.MouseMove
        ShowVals(VOLT)
    End Sub

    Private Sub txtAmplDiv7_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAmplDiv7.Enter
        GotFocusSelect()
    End Sub

    Private Sub txtAmplDiv7_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAmplDiv7.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)

        txtAmplDiv7_KeyPress(KeyAscii)
        e.KeyChar = Chr(KeyAscii)
    End Sub
    Public Sub txtAmplDiv7_KeyPress(ByRef KeyAscii As Short)
        If KeyAscii <> Keys.Return Then Exit Sub
        KeyAscii = 0
        FormatEntry(txtAmplDiv7, VOLT_DIV_7)
        SetAmplFromDiv7()
    End Sub

    Private Sub txtAmplDiv7_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAmplDiv7.Leave
        txtAmplDiv7_KeyPress(Keys.Return)
        UserEnteringData = False
    End Sub

    Private Sub txtAmplDiv7_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtAmplDiv7.MouseMove
        ShowVals(VOLT_DIV_7)
    End Sub

    Private Sub txtArmCoun_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtArmCoun.MouseMove
        ShowVals(ARM_COUN)
    End Sub

    Private Sub txtArmLay2Coun_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtArmLay2Coun.MouseMove
        ShowVals(ARM_LAY2_COUN)
    End Sub

    Private Sub txtDcOffs_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDcOffs.Enter
        GotFocusSelect()
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
        AdjustVoltLimits(VOLT_OFFS)
    End Sub

    Private Sub txtDcOffs_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDcOffs.Leave
        txtDcOffs_KeyPress(Keys.Return)
        UserEnteringData = False
    End Sub

    Private Sub txtDcOffs_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtDcOffs.MouseMove
        fraDcOffs_MouseHover(Me, New EventArgs())
    End Sub

    Private Sub txtDwelCoun_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDwelCoun.Enter
        GotFocusSelect()
    End Sub

    Private Sub txtDwelCoun_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDwelCoun.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)

        txtDwelCoun_KeyPress(KeyAscii)
        e.KeyChar = Chr(KeyAscii)
    End Sub
    Public Sub txtDwelCoun_KeyPress(ByRef KeyAscii As Short)
        Dim S As String, i As Short

        If KeyAscii <> Keys.Return Then Exit Sub
        KeyAscii = 0

        Index = lstListSseqSeq.SelectedIndex
        If Index = -1 Or Index > lstListSseqSeq.Items.Count - 1 Then Exit Sub
        i = InStr(lstListSseqSeq.Items.Item(Index).ToString(), ",")
        S = Strings.Left(lstListSseqSeq.Items.Item(Index).ToString(), i + 1)
        bDoNotTalkStore = DoNotTalk
        DoNotTalk = True
        FormatEntry(txtDwelCoun, JDH_DWEL_COUN)
        DoNotTalk = bDoNotTalkStore
        lstListSseqSeq.Items.Item(Index) = S & txtDwelCoun.Text
        tabOptions.Focus()
        SendSequence()
    End Sub

    Private Sub txtDwelCoun_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDwelCoun.Leave
        txtDwelCoun_KeyPress(Keys.Return)
        panDwelCoun.Enabled = False
        UserEnteringData = False

    End Sub

    Private Sub txtDwelCoun_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtDwelCoun.MouseMove
        ShowVals(JDH_DWEL_COUN)
    End Sub

    Private Sub txtFreq_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFreq.Enter
        GotFocusSelect()
    End Sub

    Public Sub txtFreq_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFreq.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)

        txtFreq_KeyPress(KeyAscii)
        e.KeyChar = Chr(KeyAscii)
    End Sub
    Public Sub txtFreq_KeyPress(ByRef KeyAscii As Short)
        If KeyAscii <> Keys.Return Then Exit Sub
        KeyAscii = 0
        FormatEntry(txtFreq, CurFreqIdx)
        txtSampleFreq.Text = txtFreq.Text
        UpdateWrf()
    End Sub

    Private Sub txtFreq_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFreq.Leave
        txtFreq_KeyPress(Keys.Return)
        UserEnteringData = False
    End Sub

    Private Sub txtFreq_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtFreq.MouseMove
        fraFreq_MouseHover(Me, New EventArgs())
    End Sub

    Private Sub txtMarkSpo_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMarkSpo.Enter
        GotFocusSelect()
    End Sub

    Public Sub txtMarkSpo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMarkSpo.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)

        txtMarkSpo_KeyPress(KeyAscii)
        e.KeyChar = Chr(KeyAscii)
    End Sub
    Public Sub txtMarkSpo_KeyPress(ByRef KeyAscii As Short)
        'for SCPI, use :LIST:MARKer:SPOint

        If KeyAscii <> Keys.Return Then Exit Sub
        KeyAscii = 0

        FormatEntry(txtMarkSpo, LIST_MARK_SPO)
        ProcessMarker()
    End Sub

    Private Sub txtMarkSpo_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMarkSpo.Leave
        txtMarkSpo_KeyPress(Keys.Return)
        UserEnteringData = False
    End Sub

    Private Sub txtMarkSpo_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtMarkSpo.MouseMove
        ShowVals(LIST_MARK_SPO)
    End Sub

    Private Sub txtRoscFreqExt_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRoscFreqExt.Enter
        GotFocusSelect()
    End Sub

    Public Sub txtRoscFreqExt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRoscFreqExt.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)

        txtRoscFreqExt_KeyPress(KeyAscii)
        e.KeyChar = Chr(KeyAscii)
    End Sub
    Public Sub txtRoscFreqExt_KeyPress(ByRef KeyAscii As Short)
        If KeyAscii <> Keys.Return Then Exit Sub
        KeyAscii = 0
        bDoNotTalkStore = DoNotTalk
        DoNotTalk = True
        FormatEntry(txtRoscFreqExt, ROSC_FREQ_EXT)
        DoNotTalk = bDoNotTalkStore
        RefOscFreq = SetCur(ROSC_FREQ_EXT)
        SendCommand(ROSC_FREQ_EXT)
        AdjustFreqLimits()
        UpdateWrf()
    End Sub

    Private Sub txtRoscFreqExt_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRoscFreqExt.Leave
        txtRoscFreqExt_KeyPress(Keys.Return)
        UserEnteringData = False
    End Sub

    Private Sub txtRoscFreqExt_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtRoscFreqExt.MouseMove
        ShowVals(ROSC_FREQ_EXT)
    End Sub

    Private Sub txtSampleFreq_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSampleFreq.Enter
        GotFocusSelect()
    End Sub

    Private Sub txtSampleFreq_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSampleFreq.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)

        txtSampleFreq_KeyPress(KeyAscii)
        e.KeyChar = Chr(KeyAscii)
    End Sub

    Public Sub txtSampleFreq_KeyPress(ByRef KeyAscii As Short)
        If KeyAscii <> Keys.Return Then Exit Sub
        KeyAscii = 0
        txtFreq.Text = txtSampleFreq.Text
        txtFreq_KeyPress(Keys.Return)
    End Sub

    Private Sub txtSampleFreq_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSampleFreq.Leave
        txtSampleFreq_KeyPress(Keys.Return)
        UserEnteringData = False
    End Sub

    Private Sub txtArmLay2Coun_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtArmLay2Coun.Enter
        GotFocusSelect()
    End Sub

    Public Sub txtArmLay2Coun_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtArmLay2Coun.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)

        txtArmLay2Coun_KeyPress(KeyAscii)
        e.KeyChar = Chr(KeyAscii)
    End Sub
    Public Sub txtArmLay2Coun_KeyPress(ByRef KeyAscii As Short)
        If KeyAscii <> Keys.Return Then Exit Sub
        KeyAscii = 0
        FormatEntry(txtArmLay2Coun, ARM_LAY2_COUN)
    End Sub

    Private Sub txtArmLay2Coun_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtArmLay2Coun.Leave
        txtArmLay2Coun_KeyPress(Keys.Return)
        UserEnteringData = False
    End Sub

    Private Sub txtArmCoun_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtArmCoun.Enter
        GotFocusSelect()
    End Sub

    Public Sub txtArmCoun_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtArmCoun.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)

        txtArmCoun_KeyPress(KeyAscii)
        e.KeyChar = Chr(KeyAscii)
    End Sub
    Public Sub txtArmCoun_KeyPress(ByRef KeyAscii As Short)
        If KeyAscii <> Keys.Return Then Exit Sub
        KeyAscii = 0
        FormatEntry(txtArmCoun, ARM_COUN)
    End Sub

    Private Sub txtArmCoun_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtArmCoun.Leave
        txtArmCoun_KeyPress(Keys.Return)
        UserEnteringData = False
    End Sub

    Private Sub txtSampleFreq_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtSampleFreq.MouseMove
        fraFreq_MouseHover(Me, New EventArgs())
    End Sub

    Private Sub txtSweCoun_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSweCoun.Enter
        GotFocusSelect()
    End Sub

    Public Sub txtSweCoun_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSweCoun.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)

        txtSweCoun_KeyPress(KeyAscii)
        e.KeyChar = Chr(KeyAscii)
    End Sub
    Public Sub txtSweCoun_KeyPress(ByRef KeyAscii As Short)
        If KeyAscii <> Keys.Return Then Exit Sub
        KeyAscii = 0
        FormatEntry(txtSweCoun, SWE_COUN)
    End Sub

    Private Sub txtSweCoun_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSweCoun.Leave
        txtSweCoun_KeyPress(Keys.Return)
        UserEnteringData = False
    End Sub

    Private Sub txtSweCoun_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtSweCoun.MouseMove
        ShowVals(SWE_COUN)
    End Sub

    Private Sub txtUserName_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtUserName.Enter
        GotFocusSelect()
    End Sub

    Private Sub txtUserName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtUserName.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)

        If KeyAscii <> Keys.Return Then Exit Sub
        KeyAscii = 0
        UserEnteringData = False
        tabOptions.Focus()
        e.KeyChar = Chr(KeyAscii)
    End Sub

    Private Sub txtUserName_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtUserName.Leave
        UserEnteringData = False
    End Sub

    Private Sub txtUserPoints_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtUserPoints.Enter
        bHaveFocusPoints = True
        GotFocusSelect()
        lListDefine = Val(SetCur(LIST_DEF))
    End Sub

    Public Sub txtUserPoints_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtUserPoints.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)

        txtUserPoints_KeyPress(KeyAscii)
        e.KeyChar = Chr(KeyAscii)
    End Sub
    Public Sub txtUserPoints_KeyPress(ByRef KeyAscii As Short)
        If KeyAscii <> Keys.Return Then Exit Sub
        KeyAscii = 0
        If Not bHaveFocusPoints Then
            Exit Sub
        Else
            bHaveFocusPoints = False
        End If
        bDoNotTalkStore = DoNotTalk
        DoNotTalk = True
        FormatEntry(txtUserPoints, LIST_DEF)
        DoNotTalk = bDoNotTalkStore
        SetMax(LIST_MARK_SPO) = SetCur(LIST_DEF)
        lblWrf1.Text = "Segment Freq."
        lblModFreq.Visible = False
        lblModFreq1.Visible = False
        UpdateGraph()
    End Sub

    Private Sub txtUserPoints_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtUserPoints.Leave
        txtUserPoints_KeyPress(Keys.Return)
        UserEnteringData = False
    End Sub

    Private Sub txtUserPoints_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtUserPoints.MouseMove
        ShowVals(LIST_DEF)
    End Sub

    Private Sub txtWavePoin_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtWavePoin.Enter
        GotFocusSelect()
    End Sub

    Public Sub txtWavePoin_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtWavePoin.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)

        txtWavePoin_KeyPress(KeyAscii)
        e.KeyChar = Chr(KeyAscii)
    End Sub
    Public Sub txtWavePoin_KeyPress(ByRef KeyAscii As Short)
        If KeyAscii <> Keys.Return Then Exit Sub
        KeyAscii = 0
        FormatEntry(txtWavePoin, RAMP_POIN)
    End Sub

    Private Sub txtWavePoin_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtWavePoin.Leave
        txtWavePoin_KeyPress(Keys.Return)
        UserEnteringData = False
    End Sub

    Private Sub txtWavePoin_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtWavePoin.MouseMove
        fraWavePoin_MouseHover(Me, New EventArgs())
    End Sub

End Class