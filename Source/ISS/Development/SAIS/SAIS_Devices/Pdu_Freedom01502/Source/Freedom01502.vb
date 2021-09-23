
Imports System
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Math
Imports Microsoft.VisualBasic

Public Class frmAPS6062
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
    Friend WithEvents fraFunction As System.Windows.Forms.GroupBox
    Friend WithEvents tbrUutPuFunctions As System.Windows.Forms.ToolBar
    Friend WithEvents ButtonPS1 As System.Windows.Forms.ToolBarButton
    Friend WithEvents ButtonPS2 As System.Windows.Forms.ToolBarButton
    Friend WithEvents ButtonPS3 As System.Windows.Forms.ToolBarButton
    Friend WithEvents ButtonPS4 As System.Windows.Forms.ToolBarButton
    Friend WithEvents ButtonPS5 As System.Windows.Forms.ToolBarButton
    Friend WithEvents ButtonPS6 As System.Windows.Forms.ToolBarButton
    Friend WithEvents ButtonPS7 As System.Windows.Forms.ToolBarButton
    Friend WithEvents ButtonPS8 As System.Windows.Forms.ToolBarButton
    Friend WithEvents ButtonPS9 As System.Windows.Forms.ToolBarButton
    Friend WithEvents ButtonPS10 As System.Windows.Forms.ToolBarButton
    Friend WithEvents lblMeasuredV As System.Windows.Forms.Label
    Friend WithEvents lblMeasPS10 As System.Windows.Forms.Label
    Friend WithEvents lblMeasPS9 As System.Windows.Forms.Label
    Friend WithEvents lblMeasPS8 As System.Windows.Forms.Label
    Friend WithEvents lblMeasPS7 As System.Windows.Forms.Label
    Friend WithEvents lblMeasPS6 As System.Windows.Forms.Label
    Friend WithEvents lblMeasPS5 As System.Windows.Forms.Label
    Friend WithEvents lblMeasPS4 As System.Windows.Forms.Label
    Friend WithEvents lblMeasPS3 As System.Windows.Forms.Label
    Friend WithEvents lblMeasPS2 As System.Windows.Forms.Label
    Friend WithEvents lblMeasPS1 As System.Windows.Forms.Label
    Friend WithEvents lblProgramV As System.Windows.Forms.Label
    Friend WithEvents lblVoltPS1 As System.Windows.Forms.Label
    Friend WithEvents lblVoltPS2 As System.Windows.Forms.Label
    Friend WithEvents lblVoltPS3 As System.Windows.Forms.Label
    Friend WithEvents lblVoltPS4 As System.Windows.Forms.Label
    Friend WithEvents lblVoltPS5 As System.Windows.Forms.Label
    Friend WithEvents lblVoltPS6 As System.Windows.Forms.Label
    Friend WithEvents lblVoltPS7 As System.Windows.Forms.Label
    Friend WithEvents lblVoltPS8 As System.Windows.Forms.Label
    Friend WithEvents lblVoltPS9 As System.Windows.Forms.Label
    Friend WithEvents lblVoltPS10 As System.Windows.Forms.Label
    Friend WithEvents tabUserOptions As System.Windows.Forms.TabControl
    Friend WithEvents tabUserOptions_Page1 As System.Windows.Forms.TabPage
    Friend WithEvents cmdAllRelaysOn As System.Windows.Forms.Button
    Friend WithEvents cmdAllRelaysOff As System.Windows.Forms.Button
    Friend WithEvents SSFrame6 As System.Windows.Forms.GroupBox
    Friend WithEvents optRelayClosed As System.Windows.Forms.RadioButton
    Friend WithEvents optRelayOpen As System.Windows.Forms.RadioButton
    Friend WithEvents fraCurrent As System.Windows.Forms.GroupBox
    Friend WithEvents panCurrent As System.Windows.Forms.Panel
    Friend WithEvents txtCurrent As System.Windows.Forms.TextBox
    Friend WithEvents SpnButtonCurrent As NationalInstruments.UI.WindowsForms.NumericEdit
    Friend WithEvents lblAmpsUnits As System.Windows.Forms.Label
    Friend WithEvents fraVoltage As System.Windows.Forms.GroupBox
    Friend WithEvents panVoltage As System.Windows.Forms.Panel
    Friend WithEvents txtVoltage As System.Windows.Forms.TextBox
    Friend WithEvents SpnButtonVoltage As NationalInstruments.UI.WindowsForms.NumericEdit
    Friend WithEvents lblVoltsUnits As System.Windows.Forms.Label
    Friend WithEvents fraMode As System.Windows.Forms.GroupBox
    Friend WithEvents optModeSlave As System.Windows.Forms.RadioButton
    Friend WithEvents optModeMaster As System.Windows.Forms.RadioButton
    Friend WithEvents fraOutReg As System.Windows.Forms.GroupBox
    Friend WithEvents optCurProtConstantVoltage As System.Windows.Forms.RadioButton
    Friend WithEvents optCurProtConstantCurrent As System.Windows.Forms.RadioButton
    Friend WithEvents fraSense As System.Windows.Forms.GroupBox
    Friend WithEvents optSenseRemote As System.Windows.Forms.RadioButton
    Friend WithEvents optSenseLocal As System.Windows.Forms.RadioButton
    Friend WithEvents cmdResetSingle As System.Windows.Forms.Button
    Friend WithEvents SSFrame1 As System.Windows.Forms.GroupBox
    Friend WithEvents optPolarityReverse As System.Windows.Forms.RadioButton
    Friend WithEvents optPolarityNormal As System.Windows.Forms.RadioButton
    Friend WithEvents tabUserOptions_Page2 As System.Windows.Forms.TabPage
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents chkContinuous As System.Windows.Forms.CheckBox
    Friend WithEvents SSPanel2 As System.Windows.Forms.Panel
    Friend WithEvents txtSenseDisplayA As System.Windows.Forms.TextBox
    Friend WithEvents panDmmDisplay As System.Windows.Forms.Panel
    Friend WithEvents txtSenseDisplayV As System.Windows.Forms.TextBox
    Friend WithEvents cmdMeasure As System.Windows.Forms.Button
    Friend WithEvents cmdUpdateTipCurrent As System.Windows.Forms.Button
    Friend WithEvents cmdUpdateTipVoltage As System.Windows.Forms.Button
    Friend WithEvents tabUserOptions_Page3 As System.Windows.Forms.TabPage
    Friend WithEvents cmdSelfTestAll As System.Windows.Forms.Button
    Friend WithEvents cmdAbout As System.Windows.Forms.Button
    Friend WithEvents cmdSelfTest As System.Windows.Forms.Button
    Friend WithEvents fraEEReadAdd As System.Windows.Forms.GroupBox
    Friend WithEvents txtEEReadAdd As System.Windows.Forms.TextBox
    Friend WithEvents fraEEReadData As System.Windows.Forms.GroupBox
    Friend WithEvents txtEEReadData As System.Windows.Forms.TextBox
    Friend WithEvents fraEEWriteAdd As System.Windows.Forms.GroupBox
    Friend WithEvents txtEEWriteAdd As System.Windows.Forms.TextBox
    Friend WithEvents fraEEWriteData As System.Windows.Forms.GroupBox
    Friend WithEvents txtEEWriteData As System.Windows.Forms.TextBox
    Friend WithEvents fraCurrentSN As System.Windows.Forms.GroupBox
    Friend WithEvents txtCurrentSN As System.Windows.Forms.TextBox
    Friend WithEvents fraNewSN As System.Windows.Forms.GroupBox
    Friend WithEvents txtNewSN As System.Windows.Forms.TextBox
    Friend WithEvents fraLastCalDate As System.Windows.Forms.GroupBox
    Friend WithEvents txtLastCalDate As System.Windows.Forms.TextBox
    Friend WithEvents cmdUpdateTip As System.Windows.Forms.Button
    Friend WithEvents Panel_Conifg As VIPERT_Common_Controls.Panel_Conifg
    Friend WithEvents tabUserOptions_Page4 As System.Windows.Forms.TabPage
    Friend WithEvents Atlas_SFP As VIPERT_Common_Controls.Atlas_SFP
    Friend WithEvents cmdHelp As System.Windows.Forms.Button
    Friend WithEvents CommonDialog1 As System.Windows.Forms.PrintDialog
    Friend WithEvents cmdReset As System.Windows.Forms.Button
    Friend WithEvents cmdQuit As System.Windows.Forms.Button
    Friend WithEvents SSPanelAuxCMD As System.Windows.Forms.Panel
    Friend WithEvents cmdDisableFailsafe As System.Windows.Forms.Button
    Friend WithEvents cmdLastCalDate As System.Windows.Forms.Button
    Friend WithEvents cmdStatusRead As System.Windows.Forms.Button
    Friend WithEvents cmdEERead As System.Windows.Forms.Button
    Friend WithEvents cmdNoFault As System.Windows.Forms.Button
    Friend WithEvents cmdClrOffsets As System.Windows.Forms.Button
    Friend WithEvents cmdCalADC As System.Windows.Forms.Button
    Friend WithEvents cmdSNRW As System.Windows.Forms.Button
    Friend WithEvents cmdEEWrite As System.Windows.Forms.Button
    Friend WithEvents cmdInternalCal As System.Windows.Forms.Button
    Friend WithEvents SSPanel5 As System.Windows.Forms.Panel
    Friend WithEvents txtStatusBox As System.Windows.Forms.TextBox
    Friend WithEvents sbrUserInformation As System.Windows.Forms.StatusBar
    Friend WithEvents sbrUserInformation_Panel1 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents imgSupply As System.Windows.Forms.ImageList
    Friend WithEvents PSIndicatorImageList As System.Windows.Forms.ImageList
    Friend WithEvents cwbRelay As NationalInstruments.UI.WindowsForms.LedArray
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAPS6062))
        Me.fraFunction = New System.Windows.Forms.GroupBox()
        Me.cwbRelay = New NationalInstruments.UI.WindowsForms.LedArray()
        Me.tbrUutPuFunctions = New System.Windows.Forms.ToolBar()
        Me.ButtonPS1 = New System.Windows.Forms.ToolBarButton()
        Me.ButtonPS2 = New System.Windows.Forms.ToolBarButton()
        Me.ButtonPS3 = New System.Windows.Forms.ToolBarButton()
        Me.ButtonPS4 = New System.Windows.Forms.ToolBarButton()
        Me.ButtonPS5 = New System.Windows.Forms.ToolBarButton()
        Me.ButtonPS6 = New System.Windows.Forms.ToolBarButton()
        Me.ButtonPS7 = New System.Windows.Forms.ToolBarButton()
        Me.ButtonPS8 = New System.Windows.Forms.ToolBarButton()
        Me.ButtonPS9 = New System.Windows.Forms.ToolBarButton()
        Me.ButtonPS10 = New System.Windows.Forms.ToolBarButton()
        Me.imgSupply = New System.Windows.Forms.ImageList(Me.components)
        Me.lblMeasuredV = New System.Windows.Forms.Label()
        Me.lblMeasPS10 = New System.Windows.Forms.Label()
        Me.lblMeasPS9 = New System.Windows.Forms.Label()
        Me.lblMeasPS8 = New System.Windows.Forms.Label()
        Me.lblMeasPS7 = New System.Windows.Forms.Label()
        Me.lblMeasPS6 = New System.Windows.Forms.Label()
        Me.lblMeasPS5 = New System.Windows.Forms.Label()
        Me.lblMeasPS4 = New System.Windows.Forms.Label()
        Me.lblMeasPS3 = New System.Windows.Forms.Label()
        Me.lblProgramV = New System.Windows.Forms.Label()
        Me.lblMeasPS2 = New System.Windows.Forms.Label()
        Me.lblMeasPS1 = New System.Windows.Forms.Label()
        Me.lblVoltPS1 = New System.Windows.Forms.Label()
        Me.lblVoltPS2 = New System.Windows.Forms.Label()
        Me.lblVoltPS3 = New System.Windows.Forms.Label()
        Me.lblVoltPS4 = New System.Windows.Forms.Label()
        Me.lblVoltPS5 = New System.Windows.Forms.Label()
        Me.lblVoltPS6 = New System.Windows.Forms.Label()
        Me.lblVoltPS7 = New System.Windows.Forms.Label()
        Me.lblVoltPS8 = New System.Windows.Forms.Label()
        Me.lblVoltPS9 = New System.Windows.Forms.Label()
        Me.lblVoltPS10 = New System.Windows.Forms.Label()
        Me.tabUserOptions = New System.Windows.Forms.TabControl()
        Me.tabUserOptions_Page1 = New System.Windows.Forms.TabPage()
        Me.cmdAllRelaysOn = New System.Windows.Forms.Button()
        Me.cmdAllRelaysOff = New System.Windows.Forms.Button()
        Me.SSFrame6 = New System.Windows.Forms.GroupBox()
        Me.optRelayClosed = New System.Windows.Forms.RadioButton()
        Me.optRelayOpen = New System.Windows.Forms.RadioButton()
        Me.fraCurrent = New System.Windows.Forms.GroupBox()
        Me.panCurrent = New System.Windows.Forms.Panel()
        Me.txtCurrent = New System.Windows.Forms.TextBox()
        Me.SpnButtonCurrent = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.lblAmpsUnits = New System.Windows.Forms.Label()
        Me.fraVoltage = New System.Windows.Forms.GroupBox()
        Me.panVoltage = New System.Windows.Forms.Panel()
        Me.txtVoltage = New System.Windows.Forms.TextBox()
        Me.SpnButtonVoltage = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.lblVoltsUnits = New System.Windows.Forms.Label()
        Me.fraMode = New System.Windows.Forms.GroupBox()
        Me.optModeSlave = New System.Windows.Forms.RadioButton()
        Me.optModeMaster = New System.Windows.Forms.RadioButton()
        Me.fraOutReg = New System.Windows.Forms.GroupBox()
        Me.optCurProtConstantVoltage = New System.Windows.Forms.RadioButton()
        Me.optCurProtConstantCurrent = New System.Windows.Forms.RadioButton()
        Me.fraSense = New System.Windows.Forms.GroupBox()
        Me.optSenseRemote = New System.Windows.Forms.RadioButton()
        Me.optSenseLocal = New System.Windows.Forms.RadioButton()
        Me.cmdResetSingle = New System.Windows.Forms.Button()
        Me.SSFrame1 = New System.Windows.Forms.GroupBox()
        Me.optPolarityReverse = New System.Windows.Forms.RadioButton()
        Me.optPolarityNormal = New System.Windows.Forms.RadioButton()
        Me.tabUserOptions_Page2 = New System.Windows.Forms.TabPage()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.chkContinuous = New System.Windows.Forms.CheckBox()
        Me.SSPanel2 = New System.Windows.Forms.Panel()
        Me.txtSenseDisplayA = New System.Windows.Forms.TextBox()
        Me.panDmmDisplay = New System.Windows.Forms.Panel()
        Me.txtSenseDisplayV = New System.Windows.Forms.TextBox()
        Me.cmdMeasure = New System.Windows.Forms.Button()
        Me.cmdUpdateTipCurrent = New System.Windows.Forms.Button()
        Me.cmdUpdateTipVoltage = New System.Windows.Forms.Button()
        Me.tabUserOptions_Page3 = New System.Windows.Forms.TabPage()
        Me.cmdSelfTestAll = New System.Windows.Forms.Button()
        Me.cmdAbout = New System.Windows.Forms.Button()
        Me.cmdSelfTest = New System.Windows.Forms.Button()
        Me.fraEEReadAdd = New System.Windows.Forms.GroupBox()
        Me.txtEEReadAdd = New System.Windows.Forms.TextBox()
        Me.fraEEReadData = New System.Windows.Forms.GroupBox()
        Me.txtEEReadData = New System.Windows.Forms.TextBox()
        Me.fraEEWriteAdd = New System.Windows.Forms.GroupBox()
        Me.txtEEWriteAdd = New System.Windows.Forms.TextBox()
        Me.fraEEWriteData = New System.Windows.Forms.GroupBox()
        Me.txtEEWriteData = New System.Windows.Forms.TextBox()
        Me.fraCurrentSN = New System.Windows.Forms.GroupBox()
        Me.txtCurrentSN = New System.Windows.Forms.TextBox()
        Me.fraNewSN = New System.Windows.Forms.GroupBox()
        Me.txtNewSN = New System.Windows.Forms.TextBox()
        Me.fraLastCalDate = New System.Windows.Forms.GroupBox()
        Me.txtLastCalDate = New System.Windows.Forms.TextBox()
        Me.cmdUpdateTip = New System.Windows.Forms.Button()
        Me.Panel_Conifg = New VIPERT_Common_Controls.Panel_Conifg()
        Me.tabUserOptions_Page4 = New System.Windows.Forms.TabPage()
        Me.Atlas_SFP = New VIPERT_Common_Controls.Atlas_SFP()
        Me.cmdHelp = New System.Windows.Forms.Button()
        Me.CommonDialog1 = New System.Windows.Forms.PrintDialog()
        Me.cmdReset = New System.Windows.Forms.Button()
        Me.cmdQuit = New System.Windows.Forms.Button()
        Me.SSPanelAuxCMD = New System.Windows.Forms.Panel()
        Me.cmdDisableFailsafe = New System.Windows.Forms.Button()
        Me.cmdLastCalDate = New System.Windows.Forms.Button()
        Me.cmdStatusRead = New System.Windows.Forms.Button()
        Me.cmdEERead = New System.Windows.Forms.Button()
        Me.cmdNoFault = New System.Windows.Forms.Button()
        Me.cmdClrOffsets = New System.Windows.Forms.Button()
        Me.cmdCalADC = New System.Windows.Forms.Button()
        Me.cmdSNRW = New System.Windows.Forms.Button()
        Me.cmdEEWrite = New System.Windows.Forms.Button()
        Me.cmdInternalCal = New System.Windows.Forms.Button()
        Me.SSPanel5 = New System.Windows.Forms.Panel()
        Me.txtStatusBox = New System.Windows.Forms.TextBox()
        Me.sbrUserInformation = New System.Windows.Forms.StatusBar()
        Me.sbrUserInformation_Panel1 = New System.Windows.Forms.StatusBarPanel()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.PSIndicatorImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.fraFunction.SuspendLayout()
        CType(Me.cwbRelay.ItemTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabUserOptions.SuspendLayout()
        Me.tabUserOptions_Page1.SuspendLayout()
        Me.SSFrame6.SuspendLayout()
        Me.fraCurrent.SuspendLayout()
        Me.panCurrent.SuspendLayout()
        CType(Me.SpnButtonCurrent, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraVoltage.SuspendLayout()
        Me.panVoltage.SuspendLayout()
        CType(Me.SpnButtonVoltage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraMode.SuspendLayout()
        Me.fraOutReg.SuspendLayout()
        Me.fraSense.SuspendLayout()
        Me.SSFrame1.SuspendLayout()
        Me.tabUserOptions_Page2.SuspendLayout()
        Me.SSPanel2.SuspendLayout()
        Me.panDmmDisplay.SuspendLayout()
        Me.tabUserOptions_Page3.SuspendLayout()
        Me.fraEEReadAdd.SuspendLayout()
        Me.fraEEReadData.SuspendLayout()
        Me.fraEEWriteAdd.SuspendLayout()
        Me.fraEEWriteData.SuspendLayout()
        Me.fraCurrentSN.SuspendLayout()
        Me.fraNewSN.SuspendLayout()
        Me.fraLastCalDate.SuspendLayout()
        Me.tabUserOptions_Page4.SuspendLayout()
        Me.SSPanelAuxCMD.SuspendLayout()
        Me.SSPanel5.SuspendLayout()
        CType(Me.sbrUserInformation_Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'fraFunction
        '
        Me.fraFunction.Controls.Add(Me.cwbRelay)
        Me.fraFunction.Controls.Add(Me.tbrUutPuFunctions)
        Me.fraFunction.Controls.Add(Me.lblMeasuredV)
        Me.fraFunction.Controls.Add(Me.lblMeasPS10)
        Me.fraFunction.Controls.Add(Me.lblMeasPS9)
        Me.fraFunction.Controls.Add(Me.lblMeasPS8)
        Me.fraFunction.Controls.Add(Me.lblMeasPS7)
        Me.fraFunction.Controls.Add(Me.lblMeasPS6)
        Me.fraFunction.Controls.Add(Me.lblMeasPS5)
        Me.fraFunction.Controls.Add(Me.lblMeasPS4)
        Me.fraFunction.Controls.Add(Me.lblMeasPS3)
        Me.fraFunction.Controls.Add(Me.lblProgramV)
        Me.fraFunction.Controls.Add(Me.lblMeasPS2)
        Me.fraFunction.Controls.Add(Me.lblMeasPS1)
        Me.fraFunction.Controls.Add(Me.lblVoltPS1)
        Me.fraFunction.Controls.Add(Me.lblVoltPS2)
        Me.fraFunction.Controls.Add(Me.lblVoltPS3)
        Me.fraFunction.Controls.Add(Me.lblVoltPS4)
        Me.fraFunction.Controls.Add(Me.lblVoltPS5)
        Me.fraFunction.Controls.Add(Me.lblVoltPS6)
        Me.fraFunction.Controls.Add(Me.lblVoltPS7)
        Me.fraFunction.Controls.Add(Me.lblVoltPS8)
        Me.fraFunction.Controls.Add(Me.lblVoltPS9)
        Me.fraFunction.Controls.Add(Me.lblVoltPS10)
        Me.fraFunction.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraFunction.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraFunction.Location = New System.Drawing.Point(16, 36)
        Me.fraFunction.Name = "fraFunction"
        Me.fraFunction.Size = New System.Drawing.Size(570, 134)
        Me.fraFunction.TabIndex = 36
        Me.fraFunction.TabStop = False
        Me.fraFunction.Text = "Supply"
        '
        'cwbRelay
        '
        Me.cwbRelay.ItemDistance = 16
        '
        '
        '
        Me.cwbRelay.ItemTemplate.LedStyle = NationalInstruments.UI.LedStyle.Round3D
        Me.cwbRelay.ItemTemplate.Location = New System.Drawing.Point(0, 0)
        Me.cwbRelay.ItemTemplate.Name = ""
        Me.cwbRelay.ItemTemplate.OffColor = System.Drawing.Color.DarkGray
        Me.cwbRelay.ItemTemplate.Size = New System.Drawing.Size(25, 25)
        Me.cwbRelay.ItemTemplate.TabIndex = 0
        Me.cwbRelay.ItemTemplate.TabStop = False
        Me.cwbRelay.LayoutMode = NationalInstruments.UI.ControlArrayLayoutMode.Horizontal
        Me.cwbRelay.Location = New System.Drawing.Point(16, 58)
        Me.cwbRelay.Name = "cwbRelay"
        Me.cwbRelay.ScaleMode = NationalInstruments.UI.ControlArrayScaleMode.CreateFixedMode(10)
        Me.cwbRelay.Size = New System.Drawing.Size(405, 27)
        Me.cwbRelay.TabIndex = 110
        '
        'tbrUutPuFunctions
        '
        Me.tbrUutPuFunctions.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.ButtonPS1, Me.ButtonPS2, Me.ButtonPS3, Me.ButtonPS4, Me.ButtonPS5, Me.ButtonPS6, Me.ButtonPS7, Me.ButtonPS8, Me.ButtonPS9, Me.ButtonPS10})
        Me.tbrUutPuFunctions.ButtonSize = New System.Drawing.Size(30, 30)
        Me.tbrUutPuFunctions.Divider = False
        Me.tbrUutPuFunctions.Dock = System.Windows.Forms.DockStyle.None
        Me.tbrUutPuFunctions.DropDownArrows = True
        Me.tbrUutPuFunctions.ImageList = Me.imgSupply
        Me.tbrUutPuFunctions.Location = New System.Drawing.Point(16, 16)
        Me.tbrUutPuFunctions.Name = "tbrUutPuFunctions"
        Me.tbrUutPuFunctions.ShowToolTips = True
        Me.tbrUutPuFunctions.Size = New System.Drawing.Size(405, 43)
        Me.tbrUutPuFunctions.TabIndex = 55
        '
        'ButtonPS1
        '
        Me.ButtonPS1.ImageIndex = 0
        Me.ButtonPS1.Name = "ButtonPS1"
        Me.ButtonPS1.Pushed = True
        Me.ButtonPS1.ToolTipText = "Power Supply 1    0-40V, 5A"
        '
        'ButtonPS2
        '
        Me.ButtonPS2.ImageIndex = 1
        Me.ButtonPS2.Name = "ButtonPS2"
        Me.ButtonPS2.ToolTipText = "Power Supply 2    0-40V, 5A"
        '
        'ButtonPS3
        '
        Me.ButtonPS3.ImageIndex = 2
        Me.ButtonPS3.Name = "ButtonPS3"
        Me.ButtonPS3.ToolTipText = "Power Supply 3    0-40V, 5A"
        '
        'ButtonPS4
        '
        Me.ButtonPS4.ImageIndex = 3
        Me.ButtonPS4.Name = "ButtonPS4"
        Me.ButtonPS4.ToolTipText = "Power Supply 4    0-40V, 5A"
        '
        'ButtonPS5
        '
        Me.ButtonPS5.ImageIndex = 4
        Me.ButtonPS5.Name = "ButtonPS5"
        Me.ButtonPS5.ToolTipText = "Power Supply 5    0-40V, 5A"
        '
        'ButtonPS6
        '
        Me.ButtonPS6.ImageIndex = 5
        Me.ButtonPS6.Name = "ButtonPS6"
        Me.ButtonPS6.ToolTipText = "Power Supply 6    0-40V, 5A"
        '
        'ButtonPS7
        '
        Me.ButtonPS7.ImageIndex = 6
        Me.ButtonPS7.Name = "ButtonPS7"
        Me.ButtonPS7.ToolTipText = "Power Supply 7    0-40V, 5A"
        '
        'ButtonPS8
        '
        Me.ButtonPS8.ImageIndex = 7
        Me.ButtonPS8.Name = "ButtonPS8"
        Me.ButtonPS8.ToolTipText = "Power Supply 8    0-40V, 5A"
        '
        'ButtonPS9
        '
        Me.ButtonPS9.ImageIndex = 8
        Me.ButtonPS9.Name = "ButtonPS9"
        Me.ButtonPS9.ToolTipText = "Power Supply 9    0-40V, 5A"
        '
        'ButtonPS10
        '
        Me.ButtonPS10.ImageIndex = 9
        Me.ButtonPS10.Name = "ButtonPS10"
        Me.ButtonPS10.ToolTipText = "Power Supply 10    0-65V, 5A"
        '
        'imgSupply
        '
        Me.imgSupply.ImageStream = CType(resources.GetObject("imgSupply.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgSupply.TransparentColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.imgSupply.Images.SetKeyName(0, "frmAPS6062_imgSupply0.bmp")
        Me.imgSupply.Images.SetKeyName(1, "frmAPS6062_imgSupply1.bmp")
        Me.imgSupply.Images.SetKeyName(2, "frmAPS6062_imgSupply2.bmp")
        Me.imgSupply.Images.SetKeyName(3, "frmAPS6062_imgSupply3.bmp")
        Me.imgSupply.Images.SetKeyName(4, "frmAPS6062_imgSupply4.bmp")
        Me.imgSupply.Images.SetKeyName(5, "frmAPS6062_imgSupply5.bmp")
        Me.imgSupply.Images.SetKeyName(6, "frmAPS6062_imgSupply6.bmp")
        Me.imgSupply.Images.SetKeyName(7, "frmAPS6062_imgSupply7.bmp")
        Me.imgSupply.Images.SetKeyName(8, "frmAPS6062_imgSupply8.bmp")
        Me.imgSupply.Images.SetKeyName(9, "frmAPS6062_imgSupply9.bmp")
        '
        'lblMeasuredV
        '
        Me.lblMeasuredV.BackColor = System.Drawing.SystemColors.Control
        Me.lblMeasuredV.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMeasuredV.Location = New System.Drawing.Point(424, 109)
        Me.lblMeasuredV.Name = "lblMeasuredV"
        Me.lblMeasuredV.Size = New System.Drawing.Size(141, 17)
        Me.lblMeasuredV.TabIndex = 109
        Me.lblMeasuredV.Text = "Measured Voltage"
        '
        'lblMeasPS10
        '
        Me.lblMeasPS10.BackColor = System.Drawing.SystemColors.Control
        Me.lblMeasPS10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMeasPS10.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!)
        Me.lblMeasPS10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMeasPS10.Location = New System.Drawing.Point(381, 109)
        Me.lblMeasPS10.Name = "lblMeasPS10"
        Me.lblMeasPS10.Size = New System.Drawing.Size(40, 17)
        Me.lblMeasPS10.TabIndex = 108
        Me.lblMeasPS10.Text = "00.00"
        Me.lblMeasPS10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblMeasPS9
        '
        Me.lblMeasPS9.BackColor = System.Drawing.SystemColors.Control
        Me.lblMeasPS9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMeasPS9.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!)
        Me.lblMeasPS9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMeasPS9.Location = New System.Drawing.Point(340, 109)
        Me.lblMeasPS9.Name = "lblMeasPS9"
        Me.lblMeasPS9.Size = New System.Drawing.Size(40, 17)
        Me.lblMeasPS9.TabIndex = 107
        Me.lblMeasPS9.Text = "00.00"
        Me.lblMeasPS9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblMeasPS8
        '
        Me.lblMeasPS8.BackColor = System.Drawing.SystemColors.Control
        Me.lblMeasPS8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMeasPS8.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!)
        Me.lblMeasPS8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMeasPS8.Location = New System.Drawing.Point(299, 109)
        Me.lblMeasPS8.Name = "lblMeasPS8"
        Me.lblMeasPS8.Size = New System.Drawing.Size(40, 17)
        Me.lblMeasPS8.TabIndex = 106
        Me.lblMeasPS8.Text = "00.00"
        Me.lblMeasPS8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblMeasPS7
        '
        Me.lblMeasPS7.BackColor = System.Drawing.SystemColors.Control
        Me.lblMeasPS7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMeasPS7.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!)
        Me.lblMeasPS7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMeasPS7.Location = New System.Drawing.Point(258, 109)
        Me.lblMeasPS7.Name = "lblMeasPS7"
        Me.lblMeasPS7.Size = New System.Drawing.Size(40, 17)
        Me.lblMeasPS7.TabIndex = 105
        Me.lblMeasPS7.Text = "00.00"
        Me.lblMeasPS7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblMeasPS6
        '
        Me.lblMeasPS6.BackColor = System.Drawing.SystemColors.Control
        Me.lblMeasPS6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMeasPS6.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!)
        Me.lblMeasPS6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMeasPS6.Location = New System.Drawing.Point(217, 109)
        Me.lblMeasPS6.Name = "lblMeasPS6"
        Me.lblMeasPS6.Size = New System.Drawing.Size(40, 17)
        Me.lblMeasPS6.TabIndex = 104
        Me.lblMeasPS6.Text = "00.00"
        Me.lblMeasPS6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblMeasPS5
        '
        Me.lblMeasPS5.BackColor = System.Drawing.SystemColors.Control
        Me.lblMeasPS5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMeasPS5.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!)
        Me.lblMeasPS5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMeasPS5.Location = New System.Drawing.Point(176, 109)
        Me.lblMeasPS5.Name = "lblMeasPS5"
        Me.lblMeasPS5.Size = New System.Drawing.Size(40, 17)
        Me.lblMeasPS5.TabIndex = 103
        Me.lblMeasPS5.Text = "00.00"
        Me.lblMeasPS5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblMeasPS4
        '
        Me.lblMeasPS4.BackColor = System.Drawing.SystemColors.Control
        Me.lblMeasPS4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMeasPS4.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!)
        Me.lblMeasPS4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMeasPS4.Location = New System.Drawing.Point(135, 109)
        Me.lblMeasPS4.Name = "lblMeasPS4"
        Me.lblMeasPS4.Size = New System.Drawing.Size(40, 17)
        Me.lblMeasPS4.TabIndex = 102
        Me.lblMeasPS4.Text = "00.00"
        Me.lblMeasPS4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblMeasPS3
        '
        Me.lblMeasPS3.BackColor = System.Drawing.SystemColors.Control
        Me.lblMeasPS3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMeasPS3.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!)
        Me.lblMeasPS3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMeasPS3.Location = New System.Drawing.Point(94, 109)
        Me.lblMeasPS3.Name = "lblMeasPS3"
        Me.lblMeasPS3.Size = New System.Drawing.Size(40, 17)
        Me.lblMeasPS3.TabIndex = 101
        Me.lblMeasPS3.Text = "00.00"
        Me.lblMeasPS3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblProgramV
        '
        Me.lblProgramV.BackColor = System.Drawing.SystemColors.Control
        Me.lblProgramV.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblProgramV.Location = New System.Drawing.Point(424, 90)
        Me.lblProgramV.Name = "lblProgramV"
        Me.lblProgramV.Size = New System.Drawing.Size(141, 17)
        Me.lblProgramV.TabIndex = 100
        Me.lblProgramV.Text = "Programmed Voltage"
        '
        'lblMeasPS2
        '
        Me.lblMeasPS2.BackColor = System.Drawing.SystemColors.Control
        Me.lblMeasPS2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMeasPS2.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!)
        Me.lblMeasPS2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMeasPS2.Location = New System.Drawing.Point(53, 109)
        Me.lblMeasPS2.Name = "lblMeasPS2"
        Me.lblMeasPS2.Size = New System.Drawing.Size(40, 17)
        Me.lblMeasPS2.TabIndex = 99
        Me.lblMeasPS2.Text = "00.00"
        Me.lblMeasPS2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblMeasPS1
        '
        Me.lblMeasPS1.BackColor = System.Drawing.SystemColors.Control
        Me.lblMeasPS1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMeasPS1.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!)
        Me.lblMeasPS1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMeasPS1.Location = New System.Drawing.Point(12, 109)
        Me.lblMeasPS1.Name = "lblMeasPS1"
        Me.lblMeasPS1.Size = New System.Drawing.Size(40, 17)
        Me.lblMeasPS1.TabIndex = 98
        Me.lblMeasPS1.Text = "00.00"
        Me.lblMeasPS1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblVoltPS1
        '
        Me.lblVoltPS1.BackColor = System.Drawing.SystemColors.Control
        Me.lblVoltPS1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblVoltPS1.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!)
        Me.lblVoltPS1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblVoltPS1.Location = New System.Drawing.Point(12, 90)
        Me.lblVoltPS1.Name = "lblVoltPS1"
        Me.lblVoltPS1.Size = New System.Drawing.Size(40, 17)
        Me.lblVoltPS1.TabIndex = 56
        Me.lblVoltPS1.Text = "00.00"
        Me.lblVoltPS1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblVoltPS2
        '
        Me.lblVoltPS2.BackColor = System.Drawing.SystemColors.Control
        Me.lblVoltPS2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblVoltPS2.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!)
        Me.lblVoltPS2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblVoltPS2.Location = New System.Drawing.Point(53, 90)
        Me.lblVoltPS2.Name = "lblVoltPS2"
        Me.lblVoltPS2.Size = New System.Drawing.Size(40, 17)
        Me.lblVoltPS2.TabIndex = 54
        Me.lblVoltPS2.Text = "00.00"
        Me.lblVoltPS2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblVoltPS3
        '
        Me.lblVoltPS3.BackColor = System.Drawing.SystemColors.Control
        Me.lblVoltPS3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblVoltPS3.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!)
        Me.lblVoltPS3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblVoltPS3.Location = New System.Drawing.Point(94, 90)
        Me.lblVoltPS3.Name = "lblVoltPS3"
        Me.lblVoltPS3.Size = New System.Drawing.Size(40, 17)
        Me.lblVoltPS3.TabIndex = 53
        Me.lblVoltPS3.Text = "00.00"
        Me.lblVoltPS3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblVoltPS4
        '
        Me.lblVoltPS4.BackColor = System.Drawing.SystemColors.Control
        Me.lblVoltPS4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblVoltPS4.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!)
        Me.lblVoltPS4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblVoltPS4.Location = New System.Drawing.Point(135, 90)
        Me.lblVoltPS4.Name = "lblVoltPS4"
        Me.lblVoltPS4.Size = New System.Drawing.Size(40, 17)
        Me.lblVoltPS4.TabIndex = 52
        Me.lblVoltPS4.Text = "00.00"
        Me.lblVoltPS4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblVoltPS5
        '
        Me.lblVoltPS5.BackColor = System.Drawing.SystemColors.Control
        Me.lblVoltPS5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblVoltPS5.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!)
        Me.lblVoltPS5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblVoltPS5.Location = New System.Drawing.Point(176, 90)
        Me.lblVoltPS5.Name = "lblVoltPS5"
        Me.lblVoltPS5.Size = New System.Drawing.Size(40, 17)
        Me.lblVoltPS5.TabIndex = 51
        Me.lblVoltPS5.Text = "00.00"
        Me.lblVoltPS5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblVoltPS6
        '
        Me.lblVoltPS6.BackColor = System.Drawing.SystemColors.Control
        Me.lblVoltPS6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblVoltPS6.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!)
        Me.lblVoltPS6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblVoltPS6.Location = New System.Drawing.Point(217, 90)
        Me.lblVoltPS6.Name = "lblVoltPS6"
        Me.lblVoltPS6.Size = New System.Drawing.Size(40, 17)
        Me.lblVoltPS6.TabIndex = 50
        Me.lblVoltPS6.Text = "00.00"
        Me.lblVoltPS6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblVoltPS7
        '
        Me.lblVoltPS7.BackColor = System.Drawing.SystemColors.Control
        Me.lblVoltPS7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblVoltPS7.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!)
        Me.lblVoltPS7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblVoltPS7.Location = New System.Drawing.Point(258, 90)
        Me.lblVoltPS7.Name = "lblVoltPS7"
        Me.lblVoltPS7.Size = New System.Drawing.Size(40, 17)
        Me.lblVoltPS7.TabIndex = 49
        Me.lblVoltPS7.Text = "00.00"
        Me.lblVoltPS7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblVoltPS8
        '
        Me.lblVoltPS8.BackColor = System.Drawing.SystemColors.Control
        Me.lblVoltPS8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblVoltPS8.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!)
        Me.lblVoltPS8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblVoltPS8.Location = New System.Drawing.Point(299, 90)
        Me.lblVoltPS8.Name = "lblVoltPS8"
        Me.lblVoltPS8.Size = New System.Drawing.Size(40, 17)
        Me.lblVoltPS8.TabIndex = 48
        Me.lblVoltPS8.Text = "00.00"
        Me.lblVoltPS8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblVoltPS9
        '
        Me.lblVoltPS9.BackColor = System.Drawing.SystemColors.Control
        Me.lblVoltPS9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblVoltPS9.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!)
        Me.lblVoltPS9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblVoltPS9.Location = New System.Drawing.Point(340, 90)
        Me.lblVoltPS9.Name = "lblVoltPS9"
        Me.lblVoltPS9.Size = New System.Drawing.Size(40, 17)
        Me.lblVoltPS9.TabIndex = 47
        Me.lblVoltPS9.Text = "00.00"
        Me.lblVoltPS9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblVoltPS10
        '
        Me.lblVoltPS10.BackColor = System.Drawing.SystemColors.Control
        Me.lblVoltPS10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblVoltPS10.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!)
        Me.lblVoltPS10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblVoltPS10.Location = New System.Drawing.Point(381, 90)
        Me.lblVoltPS10.Name = "lblVoltPS10"
        Me.lblVoltPS10.Size = New System.Drawing.Size(40, 17)
        Me.lblVoltPS10.TabIndex = 46
        Me.lblVoltPS10.Text = "00.00"
        Me.lblVoltPS10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'tabUserOptions
        '
        Me.tabUserOptions.Controls.Add(Me.tabUserOptions_Page1)
        Me.tabUserOptions.Controls.Add(Me.tabUserOptions_Page2)
        Me.tabUserOptions.Controls.Add(Me.tabUserOptions_Page3)
        Me.tabUserOptions.Controls.Add(Me.tabUserOptions_Page4)
        Me.tabUserOptions.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tabUserOptions.Location = New System.Drawing.Point(8, 8)
        Me.tabUserOptions.Name = "tabUserOptions"
        Me.tabUserOptions.SelectedIndex = 0
        Me.tabUserOptions.Size = New System.Drawing.Size(591, 366)
        Me.tabUserOptions.TabIndex = 2
        '
        'tabUserOptions_Page1
        '
        Me.tabUserOptions_Page1.Controls.Add(Me.cmdAllRelaysOn)
        Me.tabUserOptions_Page1.Controls.Add(Me.cmdAllRelaysOff)
        Me.tabUserOptions_Page1.Controls.Add(Me.SSFrame6)
        Me.tabUserOptions_Page1.Controls.Add(Me.fraCurrent)
        Me.tabUserOptions_Page1.Controls.Add(Me.fraVoltage)
        Me.tabUserOptions_Page1.Controls.Add(Me.fraMode)
        Me.tabUserOptions_Page1.Controls.Add(Me.fraOutReg)
        Me.tabUserOptions_Page1.Controls.Add(Me.fraSense)
        Me.tabUserOptions_Page1.Controls.Add(Me.cmdResetSingle)
        Me.tabUserOptions_Page1.Controls.Add(Me.SSFrame1)
        Me.tabUserOptions_Page1.Location = New System.Drawing.Point(4, 22)
        Me.tabUserOptions_Page1.Name = "tabUserOptions_Page1"
        Me.tabUserOptions_Page1.Size = New System.Drawing.Size(583, 340)
        Me.tabUserOptions_Page1.TabIndex = 0
        Me.tabUserOptions_Page1.Text = "Functions"
        '
        'cmdAllRelaysOn
        '
        Me.cmdAllRelaysOn.BackColor = System.Drawing.SystemColors.Control
        Me.cmdAllRelaysOn.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAllRelaysOn.Location = New System.Drawing.Point(396, 295)
        Me.cmdAllRelaysOn.Name = "cmdAllRelaysOn"
        Me.cmdAllRelaysOn.Size = New System.Drawing.Size(58, 23)
        Me.cmdAllRelaysOn.TabIndex = 95
        Me.cmdAllRelaysOn.Text = "All O&n"
        Me.cmdAllRelaysOn.UseVisualStyleBackColor = False
        '
        'cmdAllRelaysOff
        '
        Me.cmdAllRelaysOff.BackColor = System.Drawing.SystemColors.Control
        Me.cmdAllRelaysOff.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAllRelaysOff.Location = New System.Drawing.Point(332, 295)
        Me.cmdAllRelaysOff.Name = "cmdAllRelaysOff"
        Me.cmdAllRelaysOff.Size = New System.Drawing.Size(58, 23)
        Me.cmdAllRelaysOff.TabIndex = 94
        Me.cmdAllRelaysOff.Text = "All Of&f"
        Me.cmdAllRelaysOff.UseVisualStyleBackColor = False
        '
        'SSFrame6
        '
        Me.SSFrame6.Controls.Add(Me.optRelayClosed)
        Me.SSFrame6.Controls.Add(Me.optRelayOpen)
        Me.SSFrame6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SSFrame6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.SSFrame6.Location = New System.Drawing.Point(331, 142)
        Me.SSFrame6.Name = "SSFrame6"
        Me.SSFrame6.Size = New System.Drawing.Size(135, 66)
        Me.SSFrame6.TabIndex = 21
        Me.SSFrame6.TabStop = False
        Me.SSFrame6.Text = "Output Relay"
        '
        'optRelayClosed
        '
        Me.optRelayClosed.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optRelayClosed.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optRelayClosed.Location = New System.Drawing.Point(12, 36)
        Me.optRelayClosed.Name = "optRelayClosed"
        Me.optRelayClosed.Size = New System.Drawing.Size(65, 21)
        Me.optRelayClosed.TabIndex = 23
        Me.optRelayClosed.Text = "Closed"
        '
        'optRelayOpen
        '
        Me.optRelayOpen.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optRelayOpen.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optRelayOpen.Location = New System.Drawing.Point(12, 17)
        Me.optRelayOpen.Name = "optRelayOpen"
        Me.optRelayOpen.Size = New System.Drawing.Size(65, 21)
        Me.optRelayOpen.TabIndex = 22
        Me.optRelayOpen.Text = "Open"
        '
        'fraCurrent
        '
        Me.fraCurrent.Controls.Add(Me.panCurrent)
        Me.fraCurrent.Controls.Add(Me.lblAmpsUnits)
        Me.fraCurrent.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraCurrent.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraCurrent.Location = New System.Drawing.Point(173, 142)
        Me.fraCurrent.Name = "fraCurrent"
        Me.fraCurrent.Size = New System.Drawing.Size(152, 66)
        Me.fraCurrent.TabIndex = 8
        Me.fraCurrent.TabStop = False
        Me.fraCurrent.Text = "Current Limit"
        '
        'panCurrent
        '
        Me.panCurrent.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.panCurrent.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panCurrent.Controls.Add(Me.txtCurrent)
        Me.panCurrent.Controls.Add(Me.SpnButtonCurrent)
        Me.panCurrent.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panCurrent.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.panCurrent.Location = New System.Drawing.Point(8, 24)
        Me.panCurrent.Name = "panCurrent"
        Me.panCurrent.Size = New System.Drawing.Size(85, 21)
        Me.panCurrent.TabIndex = 9
        '
        'txtCurrent
        '
        Me.txtCurrent.BackColor = System.Drawing.SystemColors.Window
        Me.txtCurrent.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtCurrent.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCurrent.Location = New System.Drawing.Point(2, 2)
        Me.txtCurrent.Name = "txtCurrent"
        Me.txtCurrent.Size = New System.Drawing.Size(62, 13)
        Me.txtCurrent.TabIndex = 10
        Me.txtCurrent.Text = "99.99"
        Me.txtCurrent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'SpnButtonCurrent
        '
        Me.SpnButtonCurrent.Location = New System.Drawing.Point(67, -2)
        Me.SpnButtonCurrent.Name = "SpnButtonCurrent"
        Me.SpnButtonCurrent.Size = New System.Drawing.Size(16, 20)
        Me.SpnButtonCurrent.TabIndex = 11
        '
        'lblAmpsUnits
        '
        Me.lblAmpsUnits.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblAmpsUnits.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAmpsUnits.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAmpsUnits.Location = New System.Drawing.Point(92, 24)
        Me.lblAmpsUnits.Name = "lblAmpsUnits"
        Me.lblAmpsUnits.Size = New System.Drawing.Size(55, 21)
        Me.lblAmpsUnits.TabIndex = 35
        Me.lblAmpsUnits.Text = "(Amps)"
        Me.lblAmpsUnits.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'fraVoltage
        '
        Me.fraVoltage.Controls.Add(Me.panVoltage)
        Me.fraVoltage.Controls.Add(Me.lblVoltsUnits)
        Me.fraVoltage.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraVoltage.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraVoltage.Location = New System.Drawing.Point(8, 142)
        Me.fraVoltage.Name = "fraVoltage"
        Me.fraVoltage.Size = New System.Drawing.Size(159, 66)
        Me.fraVoltage.TabIndex = 4
        Me.fraVoltage.TabStop = False
        Me.fraVoltage.Text = "Voltage"
        '
        'panVoltage
        '
        Me.panVoltage.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.panVoltage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panVoltage.Controls.Add(Me.txtVoltage)
        Me.panVoltage.Controls.Add(Me.SpnButtonVoltage)
        Me.panVoltage.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panVoltage.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.panVoltage.Location = New System.Drawing.Point(8, 24)
        Me.panVoltage.Name = "panVoltage"
        Me.panVoltage.Size = New System.Drawing.Size(85, 21)
        Me.panVoltage.TabIndex = 5
        '
        'txtVoltage
        '
        Me.txtVoltage.BackColor = System.Drawing.SystemColors.Window
        Me.txtVoltage.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtVoltage.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtVoltage.Location = New System.Drawing.Point(2, 2)
        Me.txtVoltage.Name = "txtVoltage"
        Me.txtVoltage.Size = New System.Drawing.Size(62, 13)
        Me.txtVoltage.TabIndex = 6
        Me.txtVoltage.Text = "99.99"
        Me.txtVoltage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'SpnButtonVoltage
        '
        Me.SpnButtonVoltage.Location = New System.Drawing.Point(67, -2)
        Me.SpnButtonVoltage.Name = "SpnButtonVoltage"
        Me.SpnButtonVoltage.Size = New System.Drawing.Size(16, 20)
        Me.SpnButtonVoltage.TabIndex = 7
        '
        'lblVoltsUnits
        '
        Me.lblVoltsUnits.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblVoltsUnits.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblVoltsUnits.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVoltsUnits.Location = New System.Drawing.Point(99, 24)
        Me.lblVoltsUnits.Name = "lblVoltsUnits"
        Me.lblVoltsUnits.Size = New System.Drawing.Size(54, 21)
        Me.lblVoltsUnits.TabIndex = 34
        Me.lblVoltsUnits.Text = "(Volts)"
        Me.lblVoltsUnits.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'fraMode
        '
        Me.fraMode.Controls.Add(Me.optModeSlave)
        Me.fraMode.Controls.Add(Me.optModeMaster)
        Me.fraMode.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraMode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraMode.Location = New System.Drawing.Point(8, 206)
        Me.fraMode.Name = "fraMode"
        Me.fraMode.Size = New System.Drawing.Size(126, 66)
        Me.fraMode.TabIndex = 15
        Me.fraMode.TabStop = False
        Me.fraMode.Text = "Operating Mode"
        '
        'optModeSlave
        '
        Me.optModeSlave.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optModeSlave.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optModeSlave.Location = New System.Drawing.Point(8, 36)
        Me.optModeSlave.Name = "optModeSlave"
        Me.optModeSlave.Size = New System.Drawing.Size(74, 21)
        Me.optModeSlave.TabIndex = 17
        Me.optModeSlave.Text = "Slave"
        '
        'optModeMaster
        '
        Me.optModeMaster.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optModeMaster.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optModeMaster.Location = New System.Drawing.Point(8, 17)
        Me.optModeMaster.Name = "optModeMaster"
        Me.optModeMaster.Size = New System.Drawing.Size(74, 21)
        Me.optModeMaster.TabIndex = 16
        Me.optModeMaster.Text = "Master"
        '
        'fraOutReg
        '
        Me.fraOutReg.Controls.Add(Me.optCurProtConstantVoltage)
        Me.fraOutReg.Controls.Add(Me.optCurProtConstantCurrent)
        Me.fraOutReg.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraOutReg.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraOutReg.Location = New System.Drawing.Point(146, 206)
        Me.fraOutReg.Name = "fraOutReg"
        Me.fraOutReg.Size = New System.Drawing.Size(155, 66)
        Me.fraOutReg.TabIndex = 18
        Me.fraOutReg.TabStop = False
        Me.fraOutReg.Text = "Output Regulation"
        '
        'optCurProtConstantVoltage
        '
        Me.optCurProtConstantVoltage.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optCurProtConstantVoltage.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optCurProtConstantVoltage.Location = New System.Drawing.Point(8, 18)
        Me.optCurProtConstantVoltage.Name = "optCurProtConstantVoltage"
        Me.optCurProtConstantVoltage.Size = New System.Drawing.Size(141, 21)
        Me.optCurProtConstantVoltage.TabIndex = 20
        Me.optCurProtConstantVoltage.Text = "Constant Voltage"
        '
        'optCurProtConstantCurrent
        '
        Me.optCurProtConstantCurrent.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optCurProtConstantCurrent.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optCurProtConstantCurrent.Location = New System.Drawing.Point(8, 39)
        Me.optCurProtConstantCurrent.Name = "optCurProtConstantCurrent"
        Me.optCurProtConstantCurrent.Size = New System.Drawing.Size(135, 21)
        Me.optCurProtConstantCurrent.TabIndex = 19
        Me.optCurProtConstantCurrent.Text = "Constant Current"
        '
        'fraSense
        '
        Me.fraSense.Controls.Add(Me.optSenseRemote)
        Me.fraSense.Controls.Add(Me.optSenseLocal)
        Me.fraSense.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraSense.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraSense.Location = New System.Drawing.Point(307, 206)
        Me.fraSense.Name = "fraSense"
        Me.fraSense.Size = New System.Drawing.Size(103, 66)
        Me.fraSense.TabIndex = 12
        Me.fraSense.TabStop = False
        Me.fraSense.Text = "Sense"
        '
        'optSenseRemote
        '
        Me.optSenseRemote.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optSenseRemote.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optSenseRemote.Location = New System.Drawing.Point(12, 40)
        Me.optSenseRemote.Name = "optSenseRemote"
        Me.optSenseRemote.Size = New System.Drawing.Size(81, 21)
        Me.optSenseRemote.TabIndex = 14
        Me.optSenseRemote.Text = "Remote"
        '
        'optSenseLocal
        '
        Me.optSenseLocal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optSenseLocal.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optSenseLocal.Location = New System.Drawing.Point(12, 18)
        Me.optSenseLocal.Name = "optSenseLocal"
        Me.optSenseLocal.Size = New System.Drawing.Size(71, 21)
        Me.optSenseLocal.TabIndex = 13
        Me.optSenseLocal.Text = "Local"
        '
        'cmdResetSingle
        '
        Me.cmdResetSingle.BackColor = System.Drawing.SystemColors.Control
        Me.cmdResetSingle.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdResetSingle.Location = New System.Drawing.Point(170, 295)
        Me.cmdResetSingle.Name = "cmdResetSingle"
        Me.cmdResetSingle.Size = New System.Drawing.Size(58, 23)
        Me.cmdResetSingle.TabIndex = 24
        Me.cmdResetSingle.Text = "&Reset"
        Me.cmdResetSingle.UseVisualStyleBackColor = False
        '
        'SSFrame1
        '
        Me.SSFrame1.Controls.Add(Me.optPolarityReverse)
        Me.SSFrame1.Controls.Add(Me.optPolarityNormal)
        Me.SSFrame1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SSFrame1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.SSFrame1.Location = New System.Drawing.Point(8, 271)
        Me.SSFrame1.Name = "SSFrame1"
        Me.SSFrame1.Size = New System.Drawing.Size(127, 61)
        Me.SSFrame1.TabIndex = 86
        Me.SSFrame1.TabStop = False
        Me.SSFrame1.Text = "Output Polarity"
        '
        'optPolarityReverse
        '
        Me.optPolarityReverse.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optPolarityReverse.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optPolarityReverse.Location = New System.Drawing.Point(8, 36)
        Me.optPolarityReverse.Name = "optPolarityReverse"
        Me.optPolarityReverse.Size = New System.Drawing.Size(85, 21)
        Me.optPolarityReverse.TabIndex = 88
        Me.optPolarityReverse.Text = "Reverse"
        '
        'optPolarityNormal
        '
        Me.optPolarityNormal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optPolarityNormal.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optPolarityNormal.Location = New System.Drawing.Point(8, 16)
        Me.optPolarityNormal.Name = "optPolarityNormal"
        Me.optPolarityNormal.Size = New System.Drawing.Size(85, 21)
        Me.optPolarityNormal.TabIndex = 87
        Me.optPolarityNormal.Text = "Normal"
        '
        'tabUserOptions_Page2
        '
        Me.tabUserOptions_Page2.Controls.Add(Me.Label1)
        Me.tabUserOptions_Page2.Controls.Add(Me.Label2)
        Me.tabUserOptions_Page2.Controls.Add(Me.chkContinuous)
        Me.tabUserOptions_Page2.Controls.Add(Me.SSPanel2)
        Me.tabUserOptions_Page2.Controls.Add(Me.panDmmDisplay)
        Me.tabUserOptions_Page2.Controls.Add(Me.cmdMeasure)
        Me.tabUserOptions_Page2.Controls.Add(Me.cmdUpdateTipCurrent)
        Me.tabUserOptions_Page2.Controls.Add(Me.cmdUpdateTipVoltage)
        Me.tabUserOptions_Page2.Location = New System.Drawing.Point(4, 22)
        Me.tabUserOptions_Page2.Name = "tabUserOptions_Page2"
        Me.tabUserOptions_Page2.Size = New System.Drawing.Size(583, 340)
        Me.tabUserOptions_Page2.TabIndex = 1
        Me.tabUserOptions_Page2.Text = "Measurement"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(243, 158)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(84, 25)
        Me.Label1.TabIndex = 84
        Me.Label1.Text = "Voltage"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(243, 214)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(84, 25)
        Me.Label2.TabIndex = 85
        Me.Label2.Text = "Current"
        '
        'chkContinuous
        '
        Me.chkContinuous.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkContinuous.Location = New System.Drawing.Point(264, 247)
        Me.chkContinuous.Name = "chkContinuous"
        Me.chkContinuous.Size = New System.Drawing.Size(105, 21)
        Me.chkContinuous.TabIndex = 30
        Me.chkContinuous.Text = "Continuous"
        '
        'SSPanel2
        '
        Me.SSPanel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.SSPanel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SSPanel2.Controls.Add(Me.txtSenseDisplayA)
        Me.SSPanel2.Enabled = False
        Me.SSPanel2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SSPanel2.Location = New System.Drawing.Point(8, 206)
        Me.SSPanel2.Name = "SSPanel2"
        Me.SSPanel2.Size = New System.Drawing.Size(219, 42)
        Me.SSPanel2.TabIndex = 27
        '
        'txtSenseDisplayA
        '
        Me.txtSenseDisplayA.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSenseDisplayA.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSenseDisplayA.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSenseDisplayA.Location = New System.Drawing.Point(5, 5)
        Me.txtSenseDisplayA.Name = "txtSenseDisplayA"
        Me.txtSenseDisplayA.Size = New System.Drawing.Size(209, 26)
        Me.txtSenseDisplayA.TabIndex = 28
        Me.txtSenseDisplayA.Text = "Ready"
        '
        'panDmmDisplay
        '
        Me.panDmmDisplay.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.panDmmDisplay.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panDmmDisplay.Controls.Add(Me.txtSenseDisplayV)
        Me.panDmmDisplay.Enabled = False
        Me.panDmmDisplay.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panDmmDisplay.Location = New System.Drawing.Point(8, 150)
        Me.panDmmDisplay.Name = "panDmmDisplay"
        Me.panDmmDisplay.Size = New System.Drawing.Size(219, 42)
        Me.panDmmDisplay.TabIndex = 25
        '
        'txtSenseDisplayV
        '
        Me.txtSenseDisplayV.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSenseDisplayV.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSenseDisplayV.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSenseDisplayV.Location = New System.Drawing.Point(5, 5)
        Me.txtSenseDisplayV.Name = "txtSenseDisplayV"
        Me.txtSenseDisplayV.Size = New System.Drawing.Size(209, 26)
        Me.txtSenseDisplayV.TabIndex = 26
        Me.txtSenseDisplayV.Text = "Ready"
        '
        'cmdMeasure
        '
        Me.cmdMeasure.BackColor = System.Drawing.SystemColors.Control
        Me.cmdMeasure.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdMeasure.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdMeasure.Location = New System.Drawing.Point(262, 274)
        Me.cmdMeasure.Name = "cmdMeasure"
        Me.cmdMeasure.Size = New System.Drawing.Size(90, 32)
        Me.cmdMeasure.TabIndex = 29
        Me.cmdMeasure.Text = "&Measure"
        Me.cmdMeasure.UseVisualStyleBackColor = False
        '
        'cmdUpdateTipCurrent
        '
        Me.cmdUpdateTipCurrent.BackColor = System.Drawing.SystemColors.Control
        Me.cmdUpdateTipCurrent.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdUpdateTipCurrent.Location = New System.Drawing.Point(127, 269)
        Me.cmdUpdateTipCurrent.Name = "cmdUpdateTipCurrent"
        Me.cmdUpdateTipCurrent.Size = New System.Drawing.Size(100, 42)
        Me.cmdUpdateTipCurrent.TabIndex = 91
        Me.cmdUpdateTipCurrent.Text = "Update TIP Measure &Current"
        Me.cmdUpdateTipCurrent.UseVisualStyleBackColor = False
        '
        'cmdUpdateTipVoltage
        '
        Me.cmdUpdateTipVoltage.BackColor = System.Drawing.SystemColors.Control
        Me.cmdUpdateTipVoltage.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdUpdateTipVoltage.Location = New System.Drawing.Point(8, 269)
        Me.cmdUpdateTipVoltage.Name = "cmdUpdateTipVoltage"
        Me.cmdUpdateTipVoltage.Size = New System.Drawing.Size(113, 42)
        Me.cmdUpdateTipVoltage.TabIndex = 92
        Me.cmdUpdateTipVoltage.Text = "Update TIP Measure &Voltage"
        Me.cmdUpdateTipVoltage.UseVisualStyleBackColor = False
        '
        'tabUserOptions_Page3
        '
        Me.tabUserOptions_Page3.Controls.Add(Me.cmdSelfTestAll)
        Me.tabUserOptions_Page3.Controls.Add(Me.cmdAbout)
        Me.tabUserOptions_Page3.Controls.Add(Me.cmdSelfTest)
        Me.tabUserOptions_Page3.Controls.Add(Me.fraEEReadAdd)
        Me.tabUserOptions_Page3.Controls.Add(Me.fraEEReadData)
        Me.tabUserOptions_Page3.Controls.Add(Me.fraEEWriteAdd)
        Me.tabUserOptions_Page3.Controls.Add(Me.fraEEWriteData)
        Me.tabUserOptions_Page3.Controls.Add(Me.fraCurrentSN)
        Me.tabUserOptions_Page3.Controls.Add(Me.fraNewSN)
        Me.tabUserOptions_Page3.Controls.Add(Me.fraLastCalDate)
        Me.tabUserOptions_Page3.Controls.Add(Me.cmdUpdateTip)
        Me.tabUserOptions_Page3.Controls.Add(Me.Panel_Conifg)
        Me.tabUserOptions_Page3.Location = New System.Drawing.Point(4, 22)
        Me.tabUserOptions_Page3.Name = "tabUserOptions_Page3"
        Me.tabUserOptions_Page3.Size = New System.Drawing.Size(583, 340)
        Me.tabUserOptions_Page3.TabIndex = 2
        Me.tabUserOptions_Page3.Text = "Options"
        '
        'cmdSelfTestAll
        '
        Me.cmdSelfTestAll.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSelfTestAll.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSelfTestAll.Location = New System.Drawing.Point(191, 295)
        Me.cmdSelfTestAll.Name = "cmdSelfTestAll"
        Me.cmdSelfTestAll.Size = New System.Drawing.Size(111, 23)
        Me.cmdSelfTestAll.TabIndex = 31
        Me.cmdSelfTestAll.Text = "Built-In &Test All"
        Me.cmdSelfTestAll.UseVisualStyleBackColor = False
        '
        'cmdAbout
        '
        Me.cmdAbout.BackColor = System.Drawing.SystemColors.Control
        Me.cmdAbout.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAbout.Location = New System.Drawing.Point(12, 295)
        Me.cmdAbout.Name = "cmdAbout"
        Me.cmdAbout.Size = New System.Drawing.Size(76, 23)
        Me.cmdAbout.TabIndex = 32
        Me.cmdAbout.Text = "Ab&out"
        Me.cmdAbout.UseVisualStyleBackColor = False
        '
        'cmdSelfTest
        '
        Me.cmdSelfTest.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSelfTest.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSelfTest.Location = New System.Drawing.Point(92, 295)
        Me.cmdSelfTest.Name = "cmdSelfTest"
        Me.cmdSelfTest.Size = New System.Drawing.Size(95, 23)
        Me.cmdSelfTest.TabIndex = 33
        Me.cmdSelfTest.Text = "&Built-In Test"
        Me.cmdSelfTest.UseVisualStyleBackColor = False
        '
        'fraEEReadAdd
        '
        Me.fraEEReadAdd.BackColor = System.Drawing.SystemColors.Control
        Me.fraEEReadAdd.Controls.Add(Me.txtEEReadAdd)
        Me.fraEEReadAdd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraEEReadAdd.Location = New System.Drawing.Point(249, 142)
        Me.fraEEReadAdd.Name = "fraEEReadAdd"
        Me.fraEEReadAdd.Size = New System.Drawing.Size(135, 41)
        Me.fraEEReadAdd.TabIndex = 60
        Me.fraEEReadAdd.TabStop = False
        Me.fraEEReadAdd.Text = "EE Read Address"
        Me.fraEEReadAdd.Visible = False
        '
        'txtEEReadAdd
        '
        Me.txtEEReadAdd.BackColor = System.Drawing.SystemColors.Window
        Me.txtEEReadAdd.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtEEReadAdd.Location = New System.Drawing.Point(8, 15)
        Me.txtEEReadAdd.Name = "txtEEReadAdd"
        Me.txtEEReadAdd.Size = New System.Drawing.Size(121, 20)
        Me.txtEEReadAdd.TabIndex = 63
        '
        'fraEEReadData
        '
        Me.fraEEReadData.BackColor = System.Drawing.SystemColors.Control
        Me.fraEEReadData.Controls.Add(Me.txtEEReadData)
        Me.fraEEReadData.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraEEReadData.Location = New System.Drawing.Point(404, 142)
        Me.fraEEReadData.Name = "fraEEReadData"
        Me.fraEEReadData.Size = New System.Drawing.Size(135, 41)
        Me.fraEEReadData.TabIndex = 61
        Me.fraEEReadData.TabStop = False
        Me.fraEEReadData.Text = "EE Read Data"
        Me.fraEEReadData.Visible = False
        '
        'txtEEReadData
        '
        Me.txtEEReadData.BackColor = System.Drawing.SystemColors.Window
        Me.txtEEReadData.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtEEReadData.Location = New System.Drawing.Point(8, 15)
        Me.txtEEReadData.Name = "txtEEReadData"
        Me.txtEEReadData.Size = New System.Drawing.Size(121, 20)
        Me.txtEEReadData.TabIndex = 62
        '
        'fraEEWriteAdd
        '
        Me.fraEEWriteAdd.BackColor = System.Drawing.SystemColors.Control
        Me.fraEEWriteAdd.Controls.Add(Me.txtEEWriteAdd)
        Me.fraEEWriteAdd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraEEWriteAdd.Location = New System.Drawing.Point(249, 190)
        Me.fraEEWriteAdd.Name = "fraEEWriteAdd"
        Me.fraEEWriteAdd.Size = New System.Drawing.Size(135, 41)
        Me.fraEEWriteAdd.TabIndex = 71
        Me.fraEEWriteAdd.TabStop = False
        Me.fraEEWriteAdd.Text = "EE Write Address"
        Me.fraEEWriteAdd.Visible = False
        '
        'txtEEWriteAdd
        '
        Me.txtEEWriteAdd.BackColor = System.Drawing.SystemColors.Window
        Me.txtEEWriteAdd.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtEEWriteAdd.Location = New System.Drawing.Point(8, 15)
        Me.txtEEWriteAdd.Name = "txtEEWriteAdd"
        Me.txtEEWriteAdd.Size = New System.Drawing.Size(121, 20)
        Me.txtEEWriteAdd.TabIndex = 72
        '
        'fraEEWriteData
        '
        Me.fraEEWriteData.BackColor = System.Drawing.SystemColors.Control
        Me.fraEEWriteData.Controls.Add(Me.txtEEWriteData)
        Me.fraEEWriteData.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraEEWriteData.Location = New System.Drawing.Point(404, 190)
        Me.fraEEWriteData.Name = "fraEEWriteData"
        Me.fraEEWriteData.Size = New System.Drawing.Size(135, 41)
        Me.fraEEWriteData.TabIndex = 73
        Me.fraEEWriteData.TabStop = False
        Me.fraEEWriteData.Text = "EE Write Data"
        Me.fraEEWriteData.Visible = False
        '
        'txtEEWriteData
        '
        Me.txtEEWriteData.BackColor = System.Drawing.SystemColors.Window
        Me.txtEEWriteData.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtEEWriteData.Location = New System.Drawing.Point(8, 15)
        Me.txtEEWriteData.Name = "txtEEWriteData"
        Me.txtEEWriteData.Size = New System.Drawing.Size(121, 20)
        Me.txtEEWriteData.TabIndex = 74
        '
        'fraCurrentSN
        '
        Me.fraCurrentSN.BackColor = System.Drawing.SystemColors.Control
        Me.fraCurrentSN.Controls.Add(Me.txtCurrentSN)
        Me.fraCurrentSN.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraCurrentSN.Location = New System.Drawing.Point(249, 239)
        Me.fraCurrentSN.Name = "fraCurrentSN"
        Me.fraCurrentSN.Size = New System.Drawing.Size(135, 41)
        Me.fraCurrentSN.TabIndex = 77
        Me.fraCurrentSN.TabStop = False
        Me.fraCurrentSN.Text = "Current S/N"
        Me.fraCurrentSN.Visible = False
        '
        'txtCurrentSN
        '
        Me.txtCurrentSN.BackColor = System.Drawing.SystemColors.Window
        Me.txtCurrentSN.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCurrentSN.Location = New System.Drawing.Point(8, 14)
        Me.txtCurrentSN.Name = "txtCurrentSN"
        Me.txtCurrentSN.Size = New System.Drawing.Size(121, 20)
        Me.txtCurrentSN.TabIndex = 78
        '
        'fraNewSN
        '
        Me.fraNewSN.BackColor = System.Drawing.SystemColors.Control
        Me.fraNewSN.Controls.Add(Me.txtNewSN)
        Me.fraNewSN.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraNewSN.Location = New System.Drawing.Point(404, 239)
        Me.fraNewSN.Name = "fraNewSN"
        Me.fraNewSN.Size = New System.Drawing.Size(135, 41)
        Me.fraNewSN.TabIndex = 79
        Me.fraNewSN.TabStop = False
        Me.fraNewSN.Text = "New S/N"
        Me.fraNewSN.Visible = False
        '
        'txtNewSN
        '
        Me.txtNewSN.BackColor = System.Drawing.SystemColors.Window
        Me.txtNewSN.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNewSN.Location = New System.Drawing.Point(8, 14)
        Me.txtNewSN.Name = "txtNewSN"
        Me.txtNewSN.Size = New System.Drawing.Size(121, 20)
        Me.txtNewSN.TabIndex = 80
        '
        'fraLastCalDate
        '
        Me.fraLastCalDate.BackColor = System.Drawing.SystemColors.Control
        Me.fraLastCalDate.Controls.Add(Me.txtLastCalDate)
        Me.fraLastCalDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraLastCalDate.Location = New System.Drawing.Point(406, 287)
        Me.fraLastCalDate.Name = "fraLastCalDate"
        Me.fraLastCalDate.Size = New System.Drawing.Size(135, 41)
        Me.fraLastCalDate.TabIndex = 82
        Me.fraLastCalDate.TabStop = False
        Me.fraLastCalDate.Text = "Last Cal Date"
        Me.fraLastCalDate.Visible = False
        '
        'txtLastCalDate
        '
        Me.txtLastCalDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtLastCalDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtLastCalDate.Location = New System.Drawing.Point(8, 14)
        Me.txtLastCalDate.Name = "txtLastCalDate"
        Me.txtLastCalDate.Size = New System.Drawing.Size(121, 20)
        Me.txtLastCalDate.TabIndex = 83
        '
        'cmdUpdateTip
        '
        Me.cmdUpdateTip.BackColor = System.Drawing.SystemColors.Control
        Me.cmdUpdateTip.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdUpdateTip.Location = New System.Drawing.Point(306, 295)
        Me.cmdUpdateTip.Name = "cmdUpdateTip"
        Me.cmdUpdateTip.Size = New System.Drawing.Size(82, 23)
        Me.cmdUpdateTip.TabIndex = 90
        Me.cmdUpdateTip.Text = "&Update TIP"
        Me.cmdUpdateTip.UseVisualStyleBackColor = False
        '
        'Panel_Conifg
        '
        Me.Panel_Conifg.BackColor = System.Drawing.SystemColors.Control
        Me.Panel_Conifg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Panel_Conifg.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Panel_Conifg.Location = New System.Drawing.Point(16, 142)
        Me.Panel_Conifg.Name = "Panel_Conifg"
        Me.Panel_Conifg.Parent_Object = Nothing
        Me.Panel_Conifg.Refresh = CType(0, Short)
        Me.Panel_Conifg.Size = New System.Drawing.Size(191, 147)
        Me.Panel_Conifg.TabIndex = 96
        '
        'tabUserOptions_Page4
        '
        Me.tabUserOptions_Page4.Controls.Add(Me.Atlas_SFP)
        Me.tabUserOptions_Page4.Location = New System.Drawing.Point(4, 22)
        Me.tabUserOptions_Page4.Name = "tabUserOptions_Page4"
        Me.tabUserOptions_Page4.Size = New System.Drawing.Size(583, 340)
        Me.tabUserOptions_Page4.TabIndex = 3
        Me.tabUserOptions_Page4.Text = "ATLAS"
        '
        'Atlas_SFP
        '
        Me.Atlas_SFP.ATLAS = Nothing
        Me.Atlas_SFP.BackColor = System.Drawing.SystemColors.Control
        Me.Atlas_SFP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Atlas_SFP.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Atlas_SFP.Location = New System.Drawing.Point(32, 28)
        Me.Atlas_SFP.Name = "Atlas_SFP"
        Me.Atlas_SFP.Parent_Object = Nothing
        Me.Atlas_SFP.Size = New System.Drawing.Size(397, 211)
        Me.Atlas_SFP.TabIndex = 97
        '
        'cmdHelp
        '
        Me.cmdHelp.BackColor = System.Drawing.SystemColors.Control
        Me.cmdHelp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdHelp.Location = New System.Drawing.Point(324, 380)
        Me.cmdHelp.Name = "cmdHelp"
        Me.cmdHelp.Size = New System.Drawing.Size(74, 25)
        Me.cmdHelp.TabIndex = 93
        Me.cmdHelp.Text = "Help"
        Me.cmdHelp.UseVisualStyleBackColor = False
        '
        'cmdReset
        '
        Me.cmdReset.BackColor = System.Drawing.SystemColors.Control
        Me.cmdReset.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdReset.Location = New System.Drawing.Point(243, 380)
        Me.cmdReset.Name = "cmdReset"
        Me.cmdReset.Size = New System.Drawing.Size(74, 25)
        Me.cmdReset.TabIndex = 1
        Me.cmdReset.Text = "Reset &All"
        Me.cmdReset.UseVisualStyleBackColor = False
        '
        'cmdQuit
        '
        Me.cmdQuit.BackColor = System.Drawing.SystemColors.Control
        Me.cmdQuit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdQuit.Location = New System.Drawing.Point(404, 380)
        Me.cmdQuit.Name = "cmdQuit"
        Me.cmdQuit.Size = New System.Drawing.Size(74, 25)
        Me.cmdQuit.TabIndex = 0
        Me.cmdQuit.Text = "&Quit"
        Me.cmdQuit.UseVisualStyleBackColor = False
        '
        'SSPanelAuxCMD
        '
        Me.SSPanelAuxCMD.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.SSPanelAuxCMD.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SSPanelAuxCMD.Controls.Add(Me.cmdDisableFailsafe)
        Me.SSPanelAuxCMD.Controls.Add(Me.cmdLastCalDate)
        Me.SSPanelAuxCMD.Controls.Add(Me.cmdStatusRead)
        Me.SSPanelAuxCMD.Controls.Add(Me.cmdEERead)
        Me.SSPanelAuxCMD.Controls.Add(Me.cmdNoFault)
        Me.SSPanelAuxCMD.Controls.Add(Me.cmdClrOffsets)
        Me.SSPanelAuxCMD.Controls.Add(Me.cmdCalADC)
        Me.SSPanelAuxCMD.Controls.Add(Me.cmdSNRW)
        Me.SSPanelAuxCMD.Controls.Add(Me.cmdEEWrite)
        Me.SSPanelAuxCMD.Controls.Add(Me.cmdInternalCal)
        Me.SSPanelAuxCMD.Controls.Add(Me.SSPanel5)
        Me.SSPanelAuxCMD.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SSPanelAuxCMD.Location = New System.Drawing.Point(614, 8)
        Me.SSPanelAuxCMD.Name = "SSPanelAuxCMD"
        Me.SSPanelAuxCMD.Size = New System.Drawing.Size(129, 395)
        Me.SSPanelAuxCMD.TabIndex = 58
        Me.SSPanelAuxCMD.Visible = False
        '
        'cmdDisableFailsafe
        '
        Me.cmdDisableFailsafe.BackColor = System.Drawing.SystemColors.Control
        Me.cmdDisableFailsafe.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDisableFailsafe.Location = New System.Drawing.Point(11, 227)
        Me.cmdDisableFailsafe.Name = "cmdDisableFailsafe"
        Me.cmdDisableFailsafe.Size = New System.Drawing.Size(102, 23)
        Me.cmdDisableFailsafe.TabIndex = 89
        Me.cmdDisableFailsafe.Text = "DisablFailsaf"
        Me.cmdDisableFailsafe.UseVisualStyleBackColor = False
        '
        'cmdLastCalDate
        '
        Me.cmdLastCalDate.BackColor = System.Drawing.SystemColors.Control
        Me.cmdLastCalDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdLastCalDate.Location = New System.Drawing.Point(11, 105)
        Me.cmdLastCalDate.Name = "cmdLastCalDate"
        Me.cmdLastCalDate.Size = New System.Drawing.Size(102, 23)
        Me.cmdLastCalDate.TabIndex = 81
        Me.cmdLastCalDate.Text = "Last Cal Date"
        Me.cmdLastCalDate.UseVisualStyleBackColor = False
        '
        'cmdStatusRead
        '
        Me.cmdStatusRead.BackColor = System.Drawing.SystemColors.Control
        Me.cmdStatusRead.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdStatusRead.Location = New System.Drawing.Point(11, 202)
        Me.cmdStatusRead.Name = "cmdStatusRead"
        Me.cmdStatusRead.Size = New System.Drawing.Size(102, 23)
        Me.cmdStatusRead.TabIndex = 70
        Me.cmdStatusRead.Text = "Status Read"
        Me.cmdStatusRead.UseVisualStyleBackColor = False
        '
        'cmdEERead
        '
        Me.cmdEERead.BackColor = System.Drawing.SystemColors.Control
        Me.cmdEERead.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdEERead.Location = New System.Drawing.Point(11, 129)
        Me.cmdEERead.Name = "cmdEERead"
        Me.cmdEERead.Size = New System.Drawing.Size(102, 23)
        Me.cmdEERead.TabIndex = 69
        Me.cmdEERead.Text = "EE Read"
        Me.cmdEERead.UseVisualStyleBackColor = False
        '
        'cmdNoFault
        '
        Me.cmdNoFault.BackColor = System.Drawing.SystemColors.Control
        Me.cmdNoFault.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdNoFault.Location = New System.Drawing.Point(11, 81)
        Me.cmdNoFault.Name = "cmdNoFault"
        Me.cmdNoFault.Size = New System.Drawing.Size(102, 23)
        Me.cmdNoFault.TabIndex = 68
        Me.cmdNoFault.Text = "No Fault"
        Me.cmdNoFault.UseVisualStyleBackColor = False
        '
        'cmdClrOffsets
        '
        Me.cmdClrOffsets.BackColor = System.Drawing.SystemColors.Control
        Me.cmdClrOffsets.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClrOffsets.Location = New System.Drawing.Point(11, 57)
        Me.cmdClrOffsets.Name = "cmdClrOffsets"
        Me.cmdClrOffsets.Size = New System.Drawing.Size(102, 23)
        Me.cmdClrOffsets.TabIndex = 67
        Me.cmdClrOffsets.Text = "Clear Offsets"
        Me.cmdClrOffsets.UseVisualStyleBackColor = False
        '
        'cmdCalADC
        '
        Me.cmdCalADC.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCalADC.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCalADC.Location = New System.Drawing.Point(11, 32)
        Me.cmdCalADC.Name = "cmdCalADC"
        Me.cmdCalADC.Size = New System.Drawing.Size(102, 23)
        Me.cmdCalADC.TabIndex = 66
        Me.cmdCalADC.Text = "Cal ADC"
        Me.cmdCalADC.UseVisualStyleBackColor = False
        '
        'cmdSNRW
        '
        Me.cmdSNRW.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSNRW.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSNRW.Location = New System.Drawing.Point(11, 178)
        Me.cmdSNRW.Name = "cmdSNRW"
        Me.cmdSNRW.Size = New System.Drawing.Size(102, 23)
        Me.cmdSNRW.TabIndex = 65
        Me.cmdSNRW.Text = "S/N   R/W"
        Me.cmdSNRW.UseVisualStyleBackColor = False
        '
        'cmdEEWrite
        '
        Me.cmdEEWrite.BackColor = System.Drawing.SystemColors.Control
        Me.cmdEEWrite.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdEEWrite.Location = New System.Drawing.Point(11, 154)
        Me.cmdEEWrite.Name = "cmdEEWrite"
        Me.cmdEEWrite.Size = New System.Drawing.Size(102, 23)
        Me.cmdEEWrite.TabIndex = 64
        Me.cmdEEWrite.Text = "EE Write"
        Me.cmdEEWrite.UseVisualStyleBackColor = False
        '
        'cmdInternalCal
        '
        Me.cmdInternalCal.BackColor = System.Drawing.SystemColors.Control
        Me.cmdInternalCal.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdInternalCal.Location = New System.Drawing.Point(11, 8)
        Me.cmdInternalCal.Name = "cmdInternalCal"
        Me.cmdInternalCal.Size = New System.Drawing.Size(102, 23)
        Me.cmdInternalCal.TabIndex = 59
        Me.cmdInternalCal.Text = "Internal Cal"
        Me.cmdInternalCal.UseVisualStyleBackColor = False
        '
        'SSPanel5
        '
        Me.SSPanel5.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.SSPanel5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SSPanel5.Controls.Add(Me.txtStatusBox)
        Me.SSPanel5.Enabled = False
        Me.SSPanel5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.23!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SSPanel5.Location = New System.Drawing.Point(8, 267)
        Me.SSPanel5.Name = "SSPanel5"
        Me.SSPanel5.Size = New System.Drawing.Size(106, 90)
        Me.SSPanel5.TabIndex = 75
        '
        'txtStatusBox
        '
        Me.txtStatusBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtStatusBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStatusBox.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtStatusBox.Location = New System.Drawing.Point(5, 5)
        Me.txtStatusBox.Multiline = True
        Me.txtStatusBox.Name = "txtStatusBox"
        Me.txtStatusBox.Size = New System.Drawing.Size(96, 80)
        Me.txtStatusBox.TabIndex = 76
        '
        'sbrUserInformation
        '
        Me.sbrUserInformation.Location = New System.Drawing.Point(0, 423)
        Me.sbrUserInformation.Name = "sbrUserInformation"
        Me.sbrUserInformation.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.sbrUserInformation_Panel1})
        Me.sbrUserInformation.ShowPanels = True
        Me.sbrUserInformation.Size = New System.Drawing.Size(757, 17)
        Me.sbrUserInformation.SizingGrip = False
        Me.sbrUserInformation.TabIndex = 3
        '
        'sbrUserInformation_Panel1
        '
        Me.sbrUserInformation_Panel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
        Me.sbrUserInformation_Panel1.Name = "sbrUserInformation_Panel1"
        Me.sbrUserInformation_Panel1.Width = 757
        '
        'PSIndicatorImageList
        '
        Me.PSIndicatorImageList.ImageStream = CType(resources.GetObject("PSIndicatorImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.PSIndicatorImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.PSIndicatorImageList.Images.SetKeyName(0, "Greenled.bmp")
        Me.PSIndicatorImageList.Images.SetKeyName(1, "Redled.bmp")
        '
        'frmAPS6062
        '
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(757, 440)
        Me.Controls.Add(Me.fraFunction)
        Me.Controls.Add(Me.tabUserOptions)
        Me.Controls.Add(Me.cmdHelp)
        Me.Controls.Add(Me.cmdReset)
        Me.Controls.Add(Me.cmdQuit)
        Me.Controls.Add(Me.SSPanelAuxCMD)
        Me.Controls.Add(Me.sbrUserInformation)
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmAPS6062"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Programmable Power Sources"
        Me.fraFunction.ResumeLayout(False)
        Me.fraFunction.PerformLayout()
        CType(Me.cwbRelay.ItemTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabUserOptions.ResumeLayout(False)
        Me.tabUserOptions_Page1.ResumeLayout(False)
        Me.SSFrame6.ResumeLayout(False)
        Me.fraCurrent.ResumeLayout(False)
        Me.panCurrent.ResumeLayout(False)
        Me.panCurrent.PerformLayout()
        CType(Me.SpnButtonCurrent, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraVoltage.ResumeLayout(False)
        Me.panVoltage.ResumeLayout(False)
        Me.panVoltage.PerformLayout()
        CType(Me.SpnButtonVoltage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraMode.ResumeLayout(False)
        Me.fraOutReg.ResumeLayout(False)
        Me.fraSense.ResumeLayout(False)
        Me.SSFrame1.ResumeLayout(False)
        Me.tabUserOptions_Page2.ResumeLayout(False)
        Me.SSPanel2.ResumeLayout(False)
        Me.SSPanel2.PerformLayout()
        Me.panDmmDisplay.ResumeLayout(False)
        Me.panDmmDisplay.PerformLayout()
        Me.tabUserOptions_Page3.ResumeLayout(False)
        Me.fraEEReadAdd.ResumeLayout(False)
        Me.fraEEReadAdd.PerformLayout()
        Me.fraEEReadData.ResumeLayout(False)
        Me.fraEEReadData.PerformLayout()
        Me.fraEEWriteAdd.ResumeLayout(False)
        Me.fraEEWriteAdd.PerformLayout()
        Me.fraEEWriteData.ResumeLayout(False)
        Me.fraEEWriteData.PerformLayout()
        Me.fraCurrentSN.ResumeLayout(False)
        Me.fraCurrentSN.PerformLayout()
        Me.fraNewSN.ResumeLayout(False)
        Me.fraNewSN.PerformLayout()
        Me.fraLastCalDate.ResumeLayout(False)
        Me.fraLastCalDate.PerformLayout()
        Me.tabUserOptions_Page4.ResumeLayout(False)
        Me.SSPanelAuxCMD.ResumeLayout(False)
        Me.SSPanel5.ResumeLayout(False)
        Me.SSPanel5.PerformLayout()
        CType(Me.sbrUserInformation_Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    '=========================================================
    Private Const SW_SHOWNORMAL As Short = 1
    Private Declare Function ShellExecute Lib "shell32.dll" Alias "ShellExecuteA" (ByVal hwnd As Integer, ByVal lpOperation As String, ByVal lpFile As String, ByVal lpParameters As String, ByVal lpDirectory As String, ByVal nShowCmd As Integer) As Integer

    Public UpdatingFromFile As Boolean = False

    Public Function Build_Atlas() As String
        Build_Atlas = ""

        Dim sTestString As String

        sTestString = "Apply, DC SIGNAL," & vbCrLf

        If optCurProtConstantCurrent.Checked = True Then
            'constant current
            sTestString &= "CURRENT " & SupplyData(EditSupply).FuncAmpVal & "," & vbCrLf & "VOLTAGE-LIM MAX " & SupplyData(EditSupply).FuncVoltVal & "," & vbCrLf
        Else
            'constant voltage
            sTestString &= "VOLTAGE " & SupplyData(EditSupply).FuncVoltVal & "," & vbCrLf & "CURRENT-LIM MAX " & SupplyData(EditSupply).FuncAmpVal & "," & vbCrLf
        End If

        sTestString &= "CNX HI LO SENSE-HI SENSE-LO $"

        '   Return ATLAS Statement
        Build_Atlas = sTestString
    End Function

    Private Function TrimVal(ByRef sData As String) As String
        TrimVal = ""
        'Parse out the first value in a string of data
        sData = Strings.Right(sData, Len(sData) - InStr(sData, ":"))
        TrimVal = sData
    End Function

    Private Function GetVal(ByVal sData As String) As String
        GetVal = ""
        'Returns the first value in a string of data
        GetVal = Strings.Left(sData, InStr(sData, ":") - 1)
    End Function

    Public Sub SetMode(ByRef sMode As String)
        Dim d As Short
        Dim btn As System.Windows.Forms.ToolBarButton

        'get each PS values
        For d = 1 To 10
            sMode = Strings.Right(sMode, Len(sMode) - InStr(sMode, "="))
            SupplyData(d).FuncVoltVal = GetVal(sMode)
            SetCurVolt(d) = SupplyData(d).FuncVoltVal
            DcpMain.CommandSetVoltage(d, SetCurVolt(d))

            sMode = TrimVal(sMode)
            SupplyData(d).FuncAmpVal = GetVal(sMode)
            SetCurCurr(d) = SupplyData(d).FuncAmpVal
            DcpMain.CommandSetCurrent(d, SetCurCurr(d))

            sMode = TrimVal(sMode)
            SupplyData(d).SensLocal = CInt(GetVal(sMode))
            sMode = TrimVal(sMode)
            SupplyData(d).FuncRelayClosed = CInt(GetVal(sMode))
            sMode = TrimVal(sMode)
            SupplyData(d).Master = CInt(GetVal(sMode))
            sMode = TrimVal(sMode)
            SupplyData(d).CurProt = CInt(GetVal(sMode))
            sMode = TrimVal(sMode)
            SupplyData(d).RevPol = CInt(GetVal(sMode))
            sMode = TrimVal(sMode)

            'set display to show values and displays
            lblVolt(d).Text = SupplyData(d).FuncVoltVal
            lblMeas(d).Text = SupplyData(d).MeasVolt
            cwbRelay(d - 1).Value = SupplyData(d).FuncRelayClosed
            CommandSetOptions(d, Not (cwbRelay(d - 1).Value), 1, 1, 1, 1)
            If ((cwbRelay(d - 1).Value) = True) Then
                ResetAllSequence = ResetAllSequence & d & ","
            End If

            SupplyData(d).sCmdVolt = GetVal(sMode)
            sMode = TrimVal(sMode)
            SupplyData(d).sCmdAmp = GetVal(sMode)
            sMode = TrimVal(sMode)
            SupplyData(d).sCmdOpt = GetVal(sMode)
            sMode = TrimVal(sMode)
        Next d

        'remove the tag name
        sMode = Strings.Right(sMode, Len(sMode) - InStr(sMode, "="))

        For Each btn In Me.tbrUutPuFunctions.Buttons
            btn.Pushed = False
        Next

        Me.tbrUutPuFunctions.Buttons(0).Pushed = True
    End Sub

    Public Function GetMode() As String
        GetMode = ""
        Dim d As Short
        Dim sValue As String
        Dim sMasterVolt As String = "", sMasterAmp As String = ""
        sValue = ""

        'save each PS data
        For d = 1 To 10
            sValue += "PS" + d.ToString() + "="

            If SupplyData(d).Master = True Then
                sValue += SupplyData(d).FuncVoltVal + ":"
                sValue += SupplyData(d).FuncAmpVal + ":"
            Else
                sValue += sMasterVolt + ":"
                sValue += sMasterAmp + ":"
            End If

            sValue += CStr(SupplyData(d).SensLocal) + ":"
            sValue += CStr(SupplyData(d).FuncRelayClosed) + ":"
            sValue += CStr(SupplyData(d).Master) + ":"
            sValue += CStr(SupplyData(d).CurProt) + ":"
            sValue += CStr(SupplyData(d).RevPol) + ":"
            sValue += SupplyData(d).sCmdVolt + ":"
            sValue += SupplyData(d).sCmdAmp + ":"
            sValue += SupplyData(d).sCmdOpt + ":"

            'Fix Slave data
            If SupplyData(d).Master = True Then
                sMasterVolt = SupplyData(d).FuncVoltVal
                sMasterAmp = SupplyData(d).FuncAmpVal
            End If
        Next d

        'save the button mode
        sValue += "Mode=" + EditSupply.ToString()

        GetMode = sValue
    End Function

    Public Sub ConfigGetCurrent()
        Dim iLoop As Short
        Dim Data As String

        If EnableCommunication = False Or LiveMode = False Then Exit Sub

        ' Get data from each PS
        For iLoop = 1 To 10
            SendStatusBarMessage("Quering PPU for Status and Measurement on Supply " & iLoop)

            'Send Command to get Status Query
            SendGpibCommand(iLoop, Convert.ToString(Chr(iLoop)) & Convert.ToString(Chr(&H44)) & Convert.ToString(Chr(&H0)))

            Data = ReadInstrumentBuffer(iLoop)

            If Len(Data) > 2 Then
                ConfigMode((Asc(Mid(Data, 1, 1))), iLoop) 'Mode
                ConfigStatus((Asc(Mid(Data, 2, 1))), iLoop) 'Status
            End If
            'Do not need the last three bytes: Self Test, Rev., EEPROM

            'Query Supply For Sense Measurement
            SendGpibCommand(iLoop, Convert.ToString(Chr(iLoop)) & Convert.ToString(Chr(&H42)) & Convert.ToString(Chr(&H0)))

            'Read Sense Measurement
            Data = ReadInstrumentBuffer(iLoop)

            ConfigMeasure(Data, iLoop)
            Me.Refresh()
        Next iLoop

        'Update display for selected PS
        txtVoltage.Text = SetCurVolt(EditSupply)
        txtCurrent.Text = SetCurCurr(EditSupply)
        tbrUutPuFunctions.Buttons(EditSupply - 1).Pushed = True
        tbrUutPuFunctions_ButtonClick(tbrUutPuFunctions.Buttons(EditSupply - 1))
    End Sub

    Private Sub ConfigMode(ByVal iBinary As Integer, ByVal iSupply As Short)
        Dim msByte As String
        msByte = IntToBinary(iBinary)
        'EADS - DRB added 2/25/2008 to properly select supply for setting
        Me.tbrUutPuFunctions_ButtonClick(Me.tbrUutPuFunctions.Buttons(iSupply - 1))
        'Isolation Relay
        If Mid(msByte, 4, 1) = "1" Then
            SupplyData(iSupply).FuncRelayClosed = True
            optRelayClosed.Checked = True 'mh
        Else
            SupplyData(iSupply).FuncRelayClosed = False
            optRelayOpen.Checked = True 'mh
        End If
        'Master/Slave
        If Mid(msByte, 6, 1) = "1" Then
            SupplyData(iSupply).Master = False
           optModeSlave.Checked = True 'mh
        Else
            SupplyData(iSupply).Master = True
            optModeMaster.Checked = True 'mh
        End If
        'Sense Mode
        If Mid(msByte, 8, 1) = "1" Then
            SupplyData(iSupply).SensLocal = False
            optSenseRemote.Checked = True 'mh
        Else
            SupplyData(iSupply).SensLocal = True
            optSenseLocal.Checked = True 'mh
        End If

        'turn on LED to indicate the relay for that supply is closed
        cwbRelay(iSupply - 1).Value = SupplyData(iSupply).FuncRelayClosed

    End Sub

    Private Sub ConfigStatus(ByVal iBinary As Integer, ByVal iSupply As Short)
        Dim msByte As String

        msByte = IntToBinary(iBinary)

        'Constant Current Mode
        If Mid(msByte, 6, 1) = "1" Then
            SupplyData(iSupply).CurProt = False
            optCurProtConstantCurrent.Checked = True 'mh
        Else
            SupplyData(iSupply).CurProt = True 'constant voltage mode
            optCurProtConstantVoltage.Checked = True 'mh
        End If

    End Sub

    Private Sub ConfigMeasure(ByRef vData As String, ByVal iSupply As Short)
        Dim ReadBufferV As String = ""
        Dim ReadBufferA As String = ""
        Dim D1 As Single
        Dim D2 As Single

        If vData = "" Then vData = " "

        If Asc(Mid(vData, 1, 1)) > 64 Then
            'Voltage Measurement
            D1 = (Asc(Mid(vData, 3, 1))) And 15
            D1 *= 256
            D2 = (Asc(Mid(vData, 4, 1)))

            '65 Volt Supply Re-Scale Voltage Here
            If iSupply = 10 Then
                'check for negative measurements
                If ((Asc(Mid(vData, 3, 1))) And 128) = 128 Then
                    ReadBufferV = -(((D1 + D2) / 50.0))
                Else
                    ReadBufferV = ((D1 + D2) / 50.0)
                End If
            Else
                'check for negative measurements
                If ((Asc(Mid(vData, 3, 1))) And 128) = 128 Then
                    ReadBufferV = -(((D1 + D2) / 100.0))
                Else
                    ReadBufferV = ((D1 + D2) / 100.0)
                End If
            End If

            'Current Measurement
            D1 = (Asc(Mid(vData, 1, 1))) And 15
            D1 *= 256
            D2 = (Asc(Mid(vData, 2, 1)))
            ReadBufferA = Truncate((D1 + D2) / 500.0)
        End If

        'Voltage settings
        If ReadBufferV = "" Then
            ReadBufferV = "0.00"
        End If
        SupplyData(iSupply).MeasVolt = Convert.ToDouble(ReadBufferV).ToString("#0.0#")
        lblMeas(iSupply).Text = SupplyData(iSupply).MeasVolt
        If InStr(SupplyData(iSupply).MeasVolt, "-") Then
            SupplyData(iSupply).RevPol = True
        Else
            SupplyData(iSupply).RevPol = False
        End If
        SetCurVolt(iSupply) = SupplyData(iSupply).FuncVoltVal
        If SetCurVolt(iSupply) = "" Then
            lblVolt(iSupply).Text = "0.00"
        Else
            lblVolt(iSupply).Text = SetCurVolt(iSupply)
        End If
        'Current settings
        If ReadBufferA = "" Then
            ReadBufferA = "0.00"
        End If
        SupplyData(iSupply).MeasCurr = Convert.ToDouble(ReadBufferA).ToString("#0.00")
        SetCurCurr(iSupply) = SupplyData(iSupply).FuncAmpVal

    End Sub

    Private Sub cmdAbout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAbout.Click
        frmAbout.lblAttributeStatus.Visible = False
        frmAbout.lblStatus.Text = ""
        frmAbout.cmdOk.Visible = True
        Me.Cursor = Cursors.Default
        frmAbout.ShowDialog()
    End Sub

    Private Sub cmdAllRelaysOff_click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdAllRelaysOff.Click
        cmdAllRelays_Click(0)
    End Sub

    Private Sub cmdAllRelaysOn_click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdAllRelaysOn.Click
        cmdAllRelays_Click(1)
    End Sub

    Private Sub cmdAllRelays_Click(ByVal Index As Integer)
        Dim iSupply As Short
        Dim bOff As Boolean

        If Index = 0 Then bOff = True

        ' Set all Relays to open or closed positions
        For iSupply = 1 To 10
            ' Send command to instrument
            CommandSetOptions(iSupply, bOff, 1, 1, 1, 1)

            cwbRelay(iSupply - 1).Value = SupplyData(iSupply).FuncRelayClosed

            'Reset slaves if All Off
            If SupplyData(iSupply).Master = False And bOff = True Then
                SupplyData(iSupply).Master = True
                lblVolt(iSupply).Text = SupplyData(iSupply).FuncVoltVal
            End If
        Next

        ' Set local relay indicators
        optRelayOpen.Checked = bOff
        optRelayClosed.Checked = Not bOff
    End Sub

    Private Sub cmdCalADC_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCalADC.Click
        CalADC(EditSupply)
    End Sub

    Private Sub cmdClrOffsets_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdClrOffsets.Click
        CmdClearCalConstants(EditSupply)
    End Sub

    Private Sub cmdDisableFailsafe_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDisableFailsafe.Click
        DisableFailsafePowerSupply(EditSupply)
    End Sub

    Private Sub cmdEERead_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdEERead.Click
        fraEEReadData.Visible = True
        fraEEReadAdd.Visible = True
        fraEEWriteData.Visible = False
        fraEEWriteAdd.Visible = False
        Me.tabUserOptions.SelectedIndex = 2
    End Sub

    Private Sub cmdEEWrite_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdEEWrite.Click
        fraEEWriteData.Visible = True
        fraEEWriteAdd.Visible = True
        fraEEReadData.Visible = False
        fraEEReadAdd.Visible = False
        Me.tabUserOptions.SelectedIndex = 2
    End Sub

    Private Sub cmdHelp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdHelp.Click
        Dim helpFile As String = Application.StartupPath + "\..\Config\Help\Pdu_Freedom01502\PDU COMMANDS.pdf"
        ShellExecute(0, "Open", helpFile, 0, 0, SW_SHOWNORMAL)
    End Sub

    Private Sub cmdInternalCal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdInternalCal.Click
        CommandInternalCal(EditSupply)
    End Sub

    Private Sub cmdLastCalDate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdLastCalDate.Click
        Me.tabUserOptions.SelectedIndex = 2
        fraEEWriteData.Visible = False
        fraEEWriteAdd.Visible = False
        fraEEReadData.Visible = False
        fraEEReadAdd.Visible = False

        fraLastCalDate.Visible = True
        CommandReadCalDate()
        Me.Cursor = Cursors.Default
    End Sub

    Sub cmdMeasure_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdMeasure.Click
        If (Panel_Conifg.DebugMode = True) Then 'Only perform switch operation if in Local Mode
            Exit Sub
        End If
        CommandMeasure(EditSupply, "")
    End Sub

    Private Sub cmdNoFault_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNoFault.Click
        NoFaultPowerSupply(EditSupply)
    End Sub

    Public Sub cmdQuit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdQuit.Click
        Me.Close()
    End Sub

    Public Sub cmdQuit_Click()
        Me.Close()
    End Sub

    Private Sub cmdReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdReset.Click
        ResetPowerSupplies()
    End Sub

    Private Sub cmdResetSingle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdResetSingle.Click
        For Each Button As ToolBarButton In tbrUutPuFunctions.Buttons
            If (Button.Pushed = True) Then
                ResetPowerSupply(Button.ImageIndex + 1)
                Exit For
            End If
        Next
        'ResetPowerSupply(EditSupply)
    End Sub

    Private Sub cmdSelfTest_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSelfTest.Click
        If Me.Panel_Conifg.DebugMode = False Then
            CommandSelfTestSupply(EditSupply)
        Else
            MsgBox("Cannot execute BIT test while in 'Remote' mode." & vbCrLf & "Switch to 'Local' mode and try again", MsgBoxStyle.OkOnly)
        End If
    End Sub

    Private Sub cmdSelfTestAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSelfTestAll.Click
        If Me.Panel_Conifg.DebugMode = False Then
            CommandSelfTestAll()
        Else
            MsgBox("Cannot execute BIT test while in 'Remote' mode." & vbCrLf & "Switch to 'Local' mode and try again", MsgBoxStyle.OkOnly)
        End If
    End Sub

    Private Sub cmdSNRW_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSNRW.Click
        Me.tabUserOptions.SelectedIndex = 2
        fraEEWriteData.Visible = False
        fraEEWriteAdd.Visible = False
        fraEEReadData.Visible = False
        fraEEReadAdd.Visible = False
        fraCurrentSN.Visible = True
        fraNewSN.Visible = True
        txtCurrentSN.Text = ""
        txtNewSN.Text = ""

        CommandSNRead(EditSupply)
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub cmdStatusRead_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdStatusRead.Click
        bytCommandReadStatus(EditSupply)
    End Sub

    Private Sub cmdUpdateTip_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdUpdateTip.Click
        sTipCmds = sGetTipCmds("")
        SetKey("TIPS", "CMD", sTipCmds)
        SetKey("TIPS", "STATUS", "Ready")
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub cmdUpdateTipCurrent_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdUpdateTipCurrent.Click
        sTipCmds = sGetTipCmds("MA" & CStr(EditSupply))
        SetKey("TIPS", "CMD", sTipCmds)
        SetKey("TIPS", "STATUS", "Ready")
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub cmdUpdateTipVoltage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdUpdateTipVoltage.Click
        sTipCmds = sGetTipCmds("MV" & CStr(EditSupply))
        SetKey("TIPS", "CMD", sTipCmds)
        SetKey("TIPS", "STATUS", "Ready")
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub frmAPS6062_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '   Set Common Controls parent properties
        Atlas_SFP.Parent_Object = Me
        Panel_Conifg.Parent_Object = Me

        Main()
        Me.Focus()
    End Sub

    Private Sub frmAPS6062_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        Application.Exit()
    End Sub

    Private Sub optModeSlave_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optModeSlave.CheckedChanged
        Dim i As Short
        Dim iTempEditSupply As Short

        If Not EnableCommunication Or bInstrumentMode Then Exit Sub
        If optModeSlave.Checked Then
            tbrUutPuFunctions.Enabled = False
            If EditSupply = 10 Then Exit Sub
            'mh reset the supply
            CommandSupplyReset(EditSupply)
            fraVoltage.Visible = False
            fraCurrent.Visible = False
            fraSense.Visible = False
            fraOutReg.Visible = False
            SetCurVolt(EditSupply) = "00.00"
            SetCurCurr(EditSupply) = "0.076"
            SetMinVolt(EditSupply) = 0
            SetMinCurr(EditSupply) = 0
            Me.txtVoltage.Text = SetCurVolt(EditSupply)
            Me.txtCurrent.Text = SetCurCurr(EditSupply)
            SupplyData(EditSupply).FuncVoltVal = "00.00"
            SupplyData(EditSupply).MeasCurr = "0.076"
            SupplyData(EditSupply).MeasVolt = "0.00"
            SupplyData(EditSupply).FuncAmpVal = "0.076"
            SupplyData(EditSupply).SensLocal = True
            SupplyData(EditSupply).FuncRelayClosed = True
            SupplyData(EditSupply).CurProt = True
            Me.cwbRelay(EditSupply - 1).Value = True

            'mh CommandSupplyReset EditSupply%

            i = (EditSupply - 1)
            If SupplyData(i).RevPol = True Then
                SupplyData(EditSupply).RevPol = True
                optPolarityReverse.Checked = True
                CommandSetOptions(EditSupply, 1, 1, 1, 1, True) 'set Reverse mode
            Else
                SupplyData(EditSupply).RevPol = False
                optPolarityNormal.Checked = True
                CommandSetOptions(EditSupply, 1, 1, 1, 1, False) 'set Normal mode
            End If
            optPolarityNormal.Enabled = False
            optPolarityReverse.Enabled = False

            'if a master to the left, it gets reset to prevent premature control of the slave(s)
            i = (EditSupply - 1)
            If SupplyData(i).Master = True Then 'If a Master
                lblVolt(i).Text = "0.00"
                lblMeas(i).Text = "0.00"
                SupplyData(i).FuncVoltVal = "00.00"
                SupplyData(i).FuncAmpVal = "0.076"
                SupplyData(i).MeasCurr = "0.076"
                SupplyData(i).MeasVolt = "0.00"
                SetMinVolt(i) = 0
                SetMinCurr(i) = 0
                SupplyData(i).SensLocal = True
                SupplyData(i).FuncRelayClosed = False
                SupplyData(i).CurProt = True
                Me.cwbRelay(i - 1).Value = False
                CommandSupplyReset(i)
            End If

            'force Output relay closed jay check this out for slave problem
            Delay(5)
            optRelayClosed.Checked = False
            optRelayClosed.Checked = True
            optRelayOpen.Enabled = False
            optModeMaster.Enabled = False
            Delay(0.2)
            CommandSetOptions(EditSupply, 1, False, 1, 1, 1) 'set slave mode

            'if master already set up then use voltage setting for lblVolt
            For i = (EditSupply - 1) To 1 Step -1
                If SupplyData(i).Master = True Then 'If a Master
                    lblVolt(EditSupply).Text = lblVolt(i).Text
                    lblMeas(EditSupply).Text = lblMeas(i).Text
                    iTempEditSupply = EditSupply
                    EditSupply = i
                    optRelayClosed.Checked = False
                    optRelayClosed.Checked = True 'Close relay on Master
                    EditSupply = iTempEditSupply
                    Exit For
                End If
            Next i

            Me.Refresh()

            tbrUutPuFunctions.Enabled = True
            Me.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub optModeMaster_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optModeMaster.CheckedChanged
        If Not EnableCommunication Or bInstrumentMode Then Exit Sub

        If optModeMaster.Checked Then
            If EditSupply < 2 Then Exit Sub
            lblVolt(EditSupply).Text = "0.00"
            lblMeas(EditSupply).Text = "0.00"
            SetCurVolt(EditSupply) = "00.00"
            SetCurCurr(EditSupply) = "0.076"
            Me.txtVoltage.Text = SetCurVolt(EditSupply)
            Me.txtCurrent.Text = SetCurCurr(EditSupply)
            SupplyData(EditSupply).FuncVoltVal = "00.00"
            SupplyData(EditSupply).MeasVolt = "0.00"
            SupplyData(EditSupply).MeasCurr = "0.076"
            SupplyData(EditSupply).FuncAmpVal = "0.076"
            SupplyData(EditSupply).SensLocal = True
            SupplyData(EditSupply).FuncRelayClosed = False
            SupplyData(EditSupply).CurProt = True
            Me.cwbRelay(EditSupply - 1).Value = False
            optRelayOpen.Checked = True
            optRelayOpen.Enabled = True
            CommandSetOptions(EditSupply, 1, True, True, 1, 1)
            optCurProtConstantVoltage.Checked = True
            fraVoltage.Visible = True
            fraCurrent.Visible = True
            fraSense.Visible = True
            fraOutReg.Visible = True

            tbrUutPuFunctions.Enabled = True
            Me.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub optCurProtConstantVoltage_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optCurProtConstantVoltage.CheckedChanged
        If optCurProtConstantVoltage.Checked And Not bInstrumentMode Then
            CommandSetOptions(EditSupply, 1, 1, 1, True, 1)
            SetMinCurr(EditSupply) = 0
            SetMinVolt(EditSupply) = 0
        End If
    End Sub

    Private Sub optCurProtConstantCurrent_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optCurProtConstantCurrent.CheckedChanged
        If optCurProtConstantCurrent.Checked And Not bInstrumentMode Then
            SetMinCurr(EditSupply) = 1
            SetMinVolt(EditSupply) = 5
            If (Val(SetCurVolt(EditSupply)) < 1 Or Val(SetCurCurr(EditSupply)) < 1) And (Not bLoadingTipCmds) Then
                MsgBox("Constant Current Mode requires a current setting above" & vbCrLf & "1 Amp and a recommended voltage setting of 40V", MsgBoxStyle.Exclamation, "Invalid Settings")
                If Val(SetCurCurr(EditSupply)) < 1 Then
                    txtCurrent.Text = "1.0" 'Code added to set minimum current to 1 amp per ECP4 Jay Joiner 04/06/00
                    TextBox_LostFocus(CURRENT)
                    txtCurrent.Focus()
                End If
            End If
            CommandSetOptions(EditSupply, 1, 1, 1, False, 1)
        End If
    End Sub

    Private Sub optPolarityReverse_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optPolarityReverse.CheckedChanged
        optPolarity_Click(1, optPolarityReverse.Checked)
    End Sub

    Private Sub optPolarityNormal_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optPolarityNormal.CheckedChanged
        optPolarity_Click(0, optPolarityNormal.Checked)
    End Sub

    Public Sub optPolarity_Click(ByVal Index As Integer, ByVal Value As Boolean)
        Dim TempVolt As String
        Dim i As Short

        If Value And Not bInstrumentMode Then
            If EditSupply <> 10 Then
                If SupplyData(EditSupply + 1).Master = False Then 'Master/Slave config
                    TempVolt = SetCurVolt(EditSupply)
                    SetCurVolt(EditSupply) = Str(0) 'Clear voltage setting
                    DispTxt(VOLTAGE)
                Else
                    TempVolt = SetCurVolt(EditSupply)
                    lblVolt(EditSupply).Text = "-" & SetCurVolt(EditSupply)
                    SetCurVolt(EditSupply) = Str(0)
                    DispTxt(VOLTAGE)
                End If
            Else
                TempVolt = SetCurVolt(EditSupply)
                SetCurVolt(EditSupply) = Str(0)
                DispTxt(VOLTAGE)
            End If

            If EnableCommunication = True Then
                Delay(0.8)
                optRelayOpen.Checked = True
                optRelayOpen.Enabled = False
                optRelayClosed.Enabled = False
                Me.Cursor = Cursors.WaitCursor
                Delay(3)
                optRelayClosed.Checked = False
                For i = (EditSupply + 1) To 10 Step 1
                    If SupplyData(i).Master = False Then 'If a Slave
                        SupplyData(i).FuncRelayClosed = False
                        Me.cwbRelay(i - 1).Value = False
                        CommandSetOptions(i, True, 1, 1, 1, 1)
                    Else
                        Exit For
                    End If
                Next i
            End If
            Delay(0.4) 'required for supply to finish relay open cmd

            If Index = 1 Then 'Reversed
                CommandSetOptions(EditSupply, 1, 1, 1, 1, True)
                lblVolt(EditSupply).Text = Convert.ToDouble(SetCurVolt(EditSupply)).ToString("#0.00")
                lblVolt(EditSupply).ForeColor = SystemColors.Control
                lblVolt(EditSupply).BackColor = SystemColors.ControlText
                SupplyData(EditSupply).FuncVoltVal = lblVolt(EditSupply).Text

                For i = (EditSupply + 1) To 10 Step 1
                    If SupplyData(i).Master = False Then 'If a Slave
                        SupplyData(i).RevPol = True
                        CommandSetOptions(i, 1, 1, 1, 1, True)
                        lblVolt(i).Text = Convert.ToDouble(SetCurVolt(EditSupply)).ToString("#0.00")
                        lblVolt(i).ForeColor = SystemColors.Control
                        lblVolt(i).BackColor = SystemColors.ControlText
                        SupplyData(i).FuncVoltVal = lblVolt(EditSupply).Text
                    Else
                        Exit For
                    End If
                Next i
            Else                'Normal
                CommandSetOptions(EditSupply, 1, 1, 1, 1, False)
                lblVolt(EditSupply).Text = Convert.ToDouble(SetCurVolt(EditSupply)).ToString("#0.00")
                lblVolt(EditSupply).ForeColor = SystemColors.ControlText
                lblVolt(EditSupply).BackColor = SystemColors.Control
                SupplyData(EditSupply).FuncVoltVal = lblVolt(EditSupply).Text
                For i = (EditSupply + 1) To 10 Step 1
                    If SupplyData(i).Master = False Then 'If a Slave
                        SupplyData(i).RevPol = False
                        CommandSetOptions(i, 1, 1, 1, 1, False)
                        lblVolt(i).Text = Convert.ToDouble(SetCurVolt(EditSupply)).ToString("#0.00")
                        lblVolt(i).ForeColor = SystemColors.ControlText
                        lblVolt(i).BackColor = SystemColors.Control
                        SupplyData(i).FuncVoltVal = lblVolt(EditSupply).Text
                    Else
                        Exit For
                    End If
                Next i
            End If
            For i = (EditSupply + 1) To 10 Step 1
                If SupplyData(i).Master = False Then 'If a Slave
                    SupplyData(i).FuncRelayClosed = True 'close all slave relays
                    Me.cwbRelay(i - 1).Value = True
                    CommandSetOptions(i, False, 1, 1, 1, 1)
                Else
                    Exit For
                End If
            Next i

            If EditSupply <> 10 Then
                If SupplyData(EditSupply + 1).Master = False Then 'If a Slave
                    SupplyData(EditSupply).FuncRelayClosed = True 'close master's relay
                    Me.cwbRelay(EditSupply - 1).Value = True
                    optRelayOpen.Checked = False
                    optRelayClosed.Checked = True
                    CommandSetOptions(EditSupply, False, 1, 1, 1, 1)
                    Delay(0.3) ' for relay command to finish
                    SetCurVolt(EditSupply) = TempVolt 'restore voltage setting
                    Delay(0.8)
                    DispTxt(VOLTAGE)
                Else
                    Delay(0.3)
                    SetCurVolt(EditSupply) = TempVolt 'restore voltage setting
                    Delay(0.8)
                    DispTxt(VOLTAGE)
                    If optPolarityReverse.Checked Then
                        lblVolt(EditSupply).Text = "-" & Convert.ToDouble(SetCurVolt(EditSupply)).ToString("#0.00")
                    End If
                End If
            Else
                SupplyData(EditSupply).FuncRelayClosed = True 'close master's relay
                Delay(0.3)
                SetCurVolt(EditSupply) = TempVolt 'restore voltage setting
                Delay(1.3)
                DispTxt(VOLTAGE)
            End If

            Me.Cursor = Cursors.Default
            optRelayOpen.Enabled = True
            optRelayClosed.Enabled = True
        End If
    End Sub

    Private Sub optRelayClosed_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optRelayClosed.CheckedChanged
        If (UpdatingFromFile = True) Then
            Exit Sub
        End If

        If Not bInstrumentMode Then

            If Not UpdatingFromFile Then
                CommandSetOptions(EditSupply, False, 1, 1, 1, 1)
                optSenseLocal.Enabled = True
                optSenseRemote.Enabled = True
                cwbRelay(EditSupply - 1).Value = True
            End If

            Dim radioButton As RadioButton = sender

            'Add supply to reset all list
            If radioButton.Checked = True Then
                ResetAllSequence = ResetAllSequence & EditSupply & ","
            End If


        End If
    End Sub

    Private Sub optRelayOpen_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optRelayOpen.CheckedChanged
        If (UpdatingFromFile = True) Then
            Exit Sub
        End If
        If Not bInstrumentMode Then
            CommandSetOptions(EditSupply, True, 1, 1, 1, 1)
            'mh optSenseLocal.Value = True
            optSenseLocal.Enabled = False
            optSenseRemote.Enabled = False
            cwbRelay(EditSupply - 1).Value = False
        End If
    End Sub

    Private Sub optSenseRemote_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optSenseRemote.CheckedChanged
        If optSenseRemote.Checked And Not bInstrumentMode Then
            CommandSetOptions(EditSupply, 1, 1, False, 1, 1)
        End If
    End Sub

    Private Sub optSenseLocal_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optSenseLocal.CheckedChanged
        If optSenseLocal.Checked And Not bInstrumentMode Then
            CommandSetOptions(EditSupply, 1, 1, True, 1, 1)
        End If
    End Sub

    Sub DispTxt(ByVal SetIndex As Short)
        Dim i As Short
        Dim iCnt As Short

        If SetIndex = VOLTAGE Then
            If SetCurVolt(EditSupply) = "" Then
                SetCurVolt(EditSupply) = 0.0
            End If
            txtVoltage.Text = Convert.ToDouble(SetCurVolt(EditSupply)).ToString(SetRes(SetIndex))
            For i = (EditSupply + 1) To 10 Step 1
                If SupplyData(i).Master = False Then 'If a Slave
                    lblVolt(i).Text = Convert.ToDouble(SetCurVolt(EditSupply)).ToString("#0.00")
                Else
                    Exit For
                End If
            Next i
            CommandSetVoltage(EditSupply, Convert.ToDouble(txtVoltage.Text))
            lblVolt(EditSupply).Text = Convert.ToDouble(SetCurVolt(EditSupply)).ToString("#0.00")
            SupplyData(EditSupply).FuncVoltVal = lblVolt(EditSupply).Text
        Else
            txtCurrent.Text = Convert.ToDouble(SetCurCurr(EditSupply)).ToString(SetRes(SetIndex))
            iCnt = 1
            For i = (EditSupply + 1) To 10 Step 1
                If SupplyData(i).Master = False Then 'If a Slave
                    iCnt += 1
                Else
                    Exit For
                End If
            Next i
            CommandSetCurrent(EditSupply, (txtCurrent.Text / iCnt))
            SupplyData(EditSupply).FuncAmpVal = txtCurrent.Text
        End If
    End Sub

    Private Sub SpnButtonVoltage_SpinDown(sender As Object, e As EventArgs) Handles SpnButtonVoltage.DownButtonClicked
        Dim dOldVal As Double
        Dim dNewVal As Double

        dOldVal = Val(SetCurVolt(EditSupply))
        dNewVal = Val(SetCurVolt(EditSupply)) - Val(SetIncVolt(EditSupply))
        If dNewVal >= Val(SetMinVolt(EditSupply)) Then
            SetCurVolt(EditSupply) = Str(dNewVal)
        Else
            SetCurVolt(EditSupply) = SetMinVolt(EditSupply)
        End If
        DispTxt(VOLTAGE)
    End Sub

    Private Sub spnButtonCurrent_SpinDown(sender As Object, e As EventArgs) Handles SpnButtonCurrent.DownButtonClicked
        Dim dOldVal As Double
        Dim dNewVal As Double

        dOldVal = Val(SetCurCurr(EditSupply))
        dNewVal = Val(SetCurCurr(EditSupply)) - Val(SetIncCurr(EditSupply))
        If dNewVal >= Val(SetMinCurr(EditSupply)) Then
            SetCurCurr(EditSupply) = Str(dNewVal)
        Else
            SetCurCurr(EditSupply) = SetMinCurr(EditSupply)
        End If
        DispTxt(CURRENT)
    End Sub

    Private Sub SpnButtonVoltage_SpinUp(sender As Object, e As EventArgs) Handles SpnButtonVoltage.UpButtonClicked
        Dim dOldVal As Double
        Dim dNewVal As Double

        dOldVal = Val(SetCurVolt(EditSupply))
        dNewVal = Val(SetCurVolt(EditSupply)) + Val(SetIncVolt(EditSupply))
        If dNewVal <= Val(SetMaxVolt(EditSupply)) Then
            SetCurVolt(EditSupply) = Str(dNewVal)
        Else
            SetCurVolt(EditSupply) = SetMaxVolt(EditSupply)
        End If
        DispTxt(VOLTAGE)
    End Sub

    Private Sub spnButtonCurrent_SpinUp(sender As Object, e As EventArgs) Handles SpnButtonCurrent.UpButtonClicked
        Dim dOldVal As Double
        Dim dNewVal As Double

        dOldVal = Val(SetCurCurr(EditSupply))
        dNewVal = Val(SetCurCurr(EditSupply)) + Val(SetIncCurr(EditSupply))
        If dNewVal <= Val(SetMaxCurr(EditSupply)) Then
            SetCurCurr(EditSupply) = Str(dNewVal)
        Else
            SetCurCurr(EditSupply) = SetMaxCurr(EditSupply)
        End If
        DispTxt(CURRENT)
    End Sub

    Private tabUserOptions_PreviousTab As Integer
    Private Sub tabUserOptions_Deselecting(ByVal sender As System.Object, ByVal e As TabControlCancelEventArgs) Handles tabUserOptions.Deselecting
        tabUserOptions_PreviousTab = e.TabPageIndex
    End Sub

    Private Sub tabUserOptions_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tabUserOptions.SelectedIndexChanged
        Dim PreviousTab As Integer = tabUserOptions_PreviousTab

        chkContinuous.Checked = False
        SendStatusBarMessage("Supply " & EditSupply & " Selected")
        txtSenseDisplayA.Text = SupplyData(EditSupply).MeasCurr 'mh"Ready"
        txtSenseDisplayV.Text = SupplyData(EditSupply).MeasVolt 'mh"Ready"

        If tabUserOptions.SelectedIndex = 3 Then
            fraFunction.Visible = False
        Else
            fraFunction.Visible = True
        End If
    End Sub

    Sub tbrUutPuFunctions_ButtonClick(ByVal sender As Object, ByVal e As ToolBarButtonClickEventArgs) Handles tbrUutPuFunctions.ButtonClick
        tbrUutPuFunctions_ButtonClick(e.Button)
    End Sub

    Sub tbrUutPuFunctions_ButtonClick(ByVal Button As System.Windows.Forms.ToolBarButton)
        Dim i As Short

        'Going Away and Saving Data (Button Up)
        EnableCommunication = False
        'remove focus from TextBoxes
        tabUserOptions.Focus()
        txtLastCalDate.Text = ""
        txtCurrentSN.Text = ""
        txtNewSN.Text = ""

        '----- EXCEPTIONS (2)-------
        'E(1) Supply #1 and #10 CANNOT Be a slave
        EditSupply = tbrUutPuFunctions.Buttons.IndexOf(Button) + 1
        If EditSupply = 1 Or EditSupply = 10 Then
            fraMode.Visible = True
            optModeSlave.Enabled = False
            optModeMaster.Checked = True
        Else
            fraMode.Visible = True
            fraMode.BringToFront()
            fraVoltage.BringToFront()
            optModeSlave.Enabled = True
        End If
        'E(2) Supply#10 Cannot Has a Max range of 65 Volts
        If EditSupply = 10 Then
            optModeSlave.Enabled = False
            optModeMaster.Checked = True
        End If
        'Setting Up and Recalling Data (Button Down)

        txtSenseDisplayA.Text = SupplyData(EditSupply).MeasCurr 'mh "Ready"
        txtSenseDisplayV.Text = SupplyData(EditSupply).MeasVolt 'mh"Ready"

        SendStatusBarMessage("Supply " & EditSupply & " Selected")
        EnableCommunication = False
        optModeMaster.Checked = SupplyData(EditSupply).Master
        optModeSlave.Checked = Not (SupplyData(EditSupply).Master)
        optSenseLocal.Checked = SupplyData(EditSupply).SensLocal
        optSenseRemote.Checked = Not (SupplyData(EditSupply).SensLocal)
        optCurProtConstantVoltage.Checked = SupplyData(EditSupply).CurProt
        optCurProtConstantCurrent.Checked = Not (SupplyData(EditSupply).CurProt)

        optRelayClosed.Checked = SupplyData(EditSupply).FuncRelayClosed
        optRelayOpen.Checked = Not (SupplyData(EditSupply).FuncRelayClosed)
        optPolarityReverse.Checked = SupplyData(EditSupply).RevPol
        optPolarityNormal.Checked = Not (SupplyData(EditSupply).RevPol)
        lblVolt(EditSupply).ForeColor = IIf(SupplyData(EditSupply).RevPol, SystemColors.Control, SystemColors.ControlText)
        lblVolt(EditSupply).BackColor = IIf(SupplyData(EditSupply).RevPol, SystemColors.ControlText, SystemColors.Control)

        lblMeas(EditSupply).Text = SupplyData(EditSupply).MeasVolt

        cwbRelay(EditSupply - 1).Value = SupplyData(EditSupply).FuncRelayClosed

        txtVoltage.Text = SupplyData(EditSupply).FuncVoltVal
        txtCurrent.Text = SupplyData(EditSupply).FuncAmpVal
        SetCurVolt(EditSupply) = txtVoltage.Text
        SetCurCurr(EditSupply) = txtCurrent.Text

        If optModeMaster.Checked = True Then 'Master
            fraVoltage.Visible = True
            fraCurrent.Visible = True
            fraSense.Visible = True
            fraOutReg.Visible = True
            optModeMaster.Enabled = True
            optPolarityNormal.Enabled = True
            optPolarityReverse.Enabled = True
            If EditSupply <> 10 Then
                i = EditSupply + 1
                If SupplyData(i).Master = False Then 'If a Slave next door lock into CV mode, disable relay open
                    optCurProtConstantCurrent.Checked = False
                    optCurProtConstantCurrent.Enabled = False
                    optRelayOpen.Enabled = False
                Else
                    optCurProtConstantCurrent.Enabled = True
                    optRelayOpen.Enabled = True
                End If
            Else
                optCurProtConstantCurrent.Enabled = True
                optRelayOpen.Enabled = True
            End If
        Else
            fraVoltage.Visible = False
            fraCurrent.Visible = False
            fraSense.Visible = False
            fraOutReg.Visible = False
            optRelayOpen.Enabled = False
            optModeMaster.Enabled = False
            optPolarityNormal.Enabled = False
            optPolarityReverse.Enabled = False
        End If

        For Each btn In tbrUutPuFunctions.Buttons
            btn.Pushed = False
        Next
        Button.Pushed = True
        EnableCommunication = True
    End Sub

    Private Sub TextBox_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCurrent.Click, txtVoltage.Click
        If DirectCast(sender, TextBox).Name = "txtVoltage" Then
            TextBox_Click(1)
        Else
            TextBox_Click(2)
        End If
    End Sub

    Private Sub TextBox_Click(ByVal Index As Integer)
        Dim txt As TextBox

        If Index = 1 Then
            txt = txtVoltage
        Else
            txt = txtCurrent
        End If
        txt.SelectionStart = 0
        txt.SelectionLength = Len(txt.Text)
    End Sub

    Public Sub TextBox_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCurrent.Enter, txtVoltage.Enter
        Dim txt As TextBox = DirectCast(sender, TextBox)
        Dim iCnt As Short
        Dim i As Short

        txt.SelectionStart = 0
        txt.SelectionLength = Len(txt.Text)

        If txt.Name = "txtVoltage" Then
            If optCurProtConstantCurrent.Checked = True Then
                If EditSupply = &HA Then
                    SetRngMsgVolt(EditSupply) = "Min: 5 Volt    Def: 65 Volt   Max: 65 Volt"
                    SetDef(VOLTAGE) = "65.00"
                Else
                    SetRngMsgVolt(EditSupply) = "Min: 5 Volt    Def: 40 Volt   Max: 40 Volt"
                    SetDef(VOLTAGE) = "40.00"
                End If
            Else
                If EditSupply = &HA Then
                    SetRngMsgVolt(EditSupply) = "Min: 5 Volt    Def: 65 Volt   Max: 65 Volt"
                    SetDef(VOLTAGE) = "0.00"
                Else
                    SetRngMsgVolt(EditSupply) = "Min: 5 Volt    Def: 0 Volt   Max: 40 Volt"
                    SetDef(VOLTAGE) = "0.00"
                End If
            End If
            SendStatusBarMessage(SetRngMsgVolt(EditSupply))
        Else
            SetMaxCurr(EditSupply) = 5
            iCnt = 1
            For i = (EditSupply + 1) To 10 Step 1
                If SupplyData(i).Master = False Then 'If a Slave
                    iCnt += 1
                Else
                    Exit For
                End If
            Next i
            SetMaxCurr(EditSupply) = iCnt * SetMaxCurr(EditSupply)
            If optCurProtConstantCurrent.Checked = True Then
                SetRngMsgCurr(EditSupply) = "Min: 1 Amp    Def: 1 Amp    Max: " & SetMaxCurr(EditSupply) & " Amp"
            Else
                SetRngMsgCurr(EditSupply) = "Min: 0 Amp    Def: 76 mAmp   Max: " & SetMaxCurr(EditSupply) & " Amp"
            End If
            SendStatusBarMessage(SetRngMsgCurr(EditSupply))
        End If
    End Sub

    Private Sub TextBox_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCurrent.KeyPress, txtVoltage.KeyPress
        Dim Index As Short = 2
        Dim KeyAscii As Short = Asc(e.KeyChar)
        Dim txt As TextBox = DirectCast(sender, TextBox)

        If txt.Name = "txtVoltage" Then
            Index = 1
        End If

        TextBox_KeyPress(txt, Index, KeyAscii)

        e.KeyChar = Chr(KeyAscii)
    End Sub

    Public Sub TextBox_KeyPress(ByRef txt As TextBox, ByVal Index As Integer, ByRef KeyAscii As Short)
        Dim sErrMsg As String

        If Index = VOLTAGE Then
            If KeyAscii = Keys.Return Or KeyAscii = Keys.Tab Then
                KeyAscii = 0
                If (Val(txt.Text) < Val(SetMinVolt(EditSupply))) Or (Val(txt.Text) > Val(SetMaxVolt(EditSupply))) Then
                    sErrMsg = SetRngMsgVolt(EditSupply)
                    MsgBox(sErrMsg, MsgBoxStyle.Exclamation, "Invalid Value")
                    txt.Text = SetCurVolt(EditSupply)
                    tabUserOptions.Focus()
                Else
                    SetCurVolt(EditSupply) = txt.Text
                    DcpMain.CommandSetVoltage(EditSupply, Val(txt.Text))
                    tabUserOptions.Focus()
                    lblVolt(EditSupply).Text = SetCurVolt(EditSupply)
                    If InStr(SetCurVolt(EditSupply), "-") Then 'mh
                        lblVolt(EditSupply).ForeColor = SystemColors.Control
                        lblVolt(EditSupply).BackColor = SystemColors.ControlText
                    Else
                        lblVolt(EditSupply).ForeColor = SystemColors.ControlText
                        lblVolt(EditSupply).BackColor = SystemColors.Control
                    End If
                End If
            ElseIf KeyAscii = Keys.Escape Then
                KeyAscii = 0
                txt.Text = Str(Val(SetCurVolt(EditSupply)))
                tabUserOptions.Focus()
            End If
        Else
            If KeyAscii = Keys.Return Or KeyAscii = Keys.Tab Then
                KeyAscii = 0
                tabUserOptions.Focus()
            ElseIf KeyAscii = Keys.Escape Then
                KeyAscii = 0
                txt.Text = Str(Val(SetCurCurr(EditSupply)))
                tabUserOptions.Focus()
            End If
        End If
    End Sub

    Public Sub TextBox_TextChanged(sender As Object, e As EventArgs) Handles txtCurrent.TextChanged, txtVoltage.TextChanged
        Dim Index As Short = 2
        Dim txt As TextBox = DirectCast(sender, TextBox)

        If UpdatingFromFile Then
            Exit Sub
            TextBox_KeyPress(txt, Index, Keys.Return)
        End If
    End Sub

    Public Sub TextBox_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCurrent.Leave, txtVoltage.Leave
        Dim Index As Short = 2
        Dim txt As TextBox = DirectCast(sender, TextBox)

        If txt.Name = "txtVoltage" Then
            Index = 1
        End If

        TextBox_LostFocus(Index)
    End Sub

    Public Sub TextBox_LostFocus(ByVal Index As Integer)
        Dim dNewVal As Double
        Dim sFirst3 As String
        Dim sErrMsg As String = ""
        Dim txt As TextBox = txtCurrent

        If Index = 1 Then
            txt = txtVoltage
        End If

        If (Index < 1) Or (Index > 10) Then
            Exit Sub
        End If

        Try
            If Index = VOLTAGE Then
                sFirst3 = UCase(Strings.Left(txt.Text, 3))
                Select Case sFirst3
                    Case "MIN"
                        dNewVal = Val(SetMinVolt(EditSupply))
                    Case "MAX"
                        dNewVal = Val(SetMaxVolt(EditSupply))
                    Case "DEF"
                        dNewVal = Val(SetDef(Index))

                    Case Else
                        If Not IsNumeric(txt.Text) Then
                            MsgBox(SetRngMsgVolt(EditSupply), MsgBoxStyle.Exclamation, "Invalid Value")
                            txt.Focus()
                            TextBox_Click(Index)
                            Exit Sub
                        End If
                        dNewVal = Val(txt.Text)
                End Select

                If (dNewVal < Val(SetMinVolt(EditSupply))) Or (dNewVal > Val(SetMaxVolt(EditSupply))) Then
                    sErrMsg = txt.Text & ": " & SetRngMsgVolt(EditSupply)
                    MsgBox(sErrMsg, MsgBoxStyle.Exclamation, "Invalid Value")
                    If InStr(sTipMode, "TIP_Run") Then Throw (New SystemException())
                    txt.Focus()
                    TextBox_Click(Index)
                ElseIf (dNewVal < 40) And (optCurProtConstantCurrent.Checked = True) And (InStr(sTipMode, "TIP_Run") = 0) Then
                    MsgBox("Recommended Voltage setting is 40V in Constant Current mode. It" & vbCrLf & "is useable at settings down to 5V but current regulation is best at 40V", MsgBoxStyle.Exclamation, "Alert")
                    SetCurVolt(EditSupply) = Str(dNewVal)
                    DispTxt(Index)
                Else
                    SetCurVolt(EditSupply) = Str(dNewVal)
                    DispTxt(Index)
                End If
                SendStatusBarMessage("")
            Else
                sFirst3 = UCase(Strings.Left(txt.Text, 3))
                Select Case sFirst3
                    Case "MIN"
                        dNewVal = Val(SetMinCurr(EditSupply))
                    Case "MAX"
                        dNewVal = Val(SetMaxCurr(EditSupply))
                    Case "DEF"
                        dNewVal = Val(SetDef(Index))

                    Case Else
                        If Not IsNumeric(txt.Text) Then
                            MsgBox(SetRngMsgCurr(EditSupply), MsgBoxStyle.Exclamation, "Invalid Value")
                            txt.Focus()
                            TextBox_Click(Index)
                            Exit Sub
                        End If
                        dNewVal = Val(txt.Text)
                End Select

                If dNewVal < Val(SetMinCurr(EditSupply)) Then
                    sErrMsg = txt.Text & ": " & SetRngMsgCurr(EditSupply)
                    MsgBox(sErrMsg, MsgBoxStyle.Exclamation, "Invalid Value")
                    If InStr(sTipMode, "TIP_") Then Throw (New SystemException())
                    txtCurrent.Text = "1.0" 'Code changed to set current to minimum 1 amp Per ecp 4 Jay Joiner 04/06/00
                    txt.Focus()
                    TextBox_Click(Index)
                ElseIf dNewVal > Val(SetMaxCurr(EditSupply)) Then
                    sErrMsg = txt.Text & ": " & SetRngMsgCurr(EditSupply)
                    MsgBox(sErrMsg, MsgBoxStyle.Exclamation, "Invalid Value")
                    If InStr(sTipMode, "TIP_") Then Throw (New SystemException())
                    txt.Focus()
                    TextBox_Click(Index)
                ElseIf (dNewVal < 1) And (optCurProtConstantCurrent.Checked = True) And (Not bLoadingTipCmds) Then
                    MsgBox("Constant Current mode only useable above 1A", MsgBoxStyle.Exclamation, "Invalid Value")
                    txt.Focus()
                Else
                    SetCurCurr(EditSupply) = Str(dNewVal)
                    DispTxt(Index)
                End If
                SendStatusBarMessage("")
            End If

            Delay(0.3) 'Added to protect against dropped commands when a voltage is ramping

        Catch
            If InStr(sTipMode, "TIP_Run") Then
                SetKey("TIPS", "STATUS", "Error from DCPS SAIS: " & sErrMsg)
                cmdQuit_Click()
            Else
                MsgBox("DCPS " & EditSupply & " will be set to RESET state", MsgBoxStyle.Exclamation, "DCPS " & EditSupply & " Error Message")
                ResetPowerSupply(EditSupply)
            End If
        End Try
    End Sub

    Private Sub txtEEReadAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEEReadAdd.Click
        txtEEReadAdd.SelectionStart = 0
        txtEEReadAdd.SelectionLength = Len(txtEEReadAdd.Text)
    End Sub

    Private Sub txtEEReadAdd_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEEReadAdd.Enter
        SendStatusBarMessage("Enter address in decimal within the range of 0000-8191")
    End Sub

    Private Sub txtEEReadAdd_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtEEReadAdd.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)

        If KeyAscii = 13 Then
            KeyAscii = 0
            cmdEEPROMRead(txtEEReadAdd.Text)
            tabUserOptions.Focus()
        End If

        e.KeyChar = Chr(KeyAscii)
    End Sub

    Private Sub txtEEReadAdd_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEEReadAdd.Leave
        cmdEEPROMRead(txtEEReadAdd.Text)
    End Sub

    Private Sub txtEEWriteAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEEWriteAdd.Click
        txtEEWriteAdd.SelectionStart = 0
        txtEEWriteAdd.SelectionLength = Len(txtEEWriteAdd.Text)
    End Sub

    Private Sub txtEEWriteAdd_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEEWriteAdd.Enter
        SendStatusBarMessage("Enter address in decimal within the range of 0000-8191")
    End Sub

    Private Sub txtEEWriteAdd_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtEEWriteAdd.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)

        If KeyAscii = 13 Then
            KeyAscii = 0
            tabUserOptions.Focus()
        End If

        e.KeyChar = Chr(KeyAscii)
    End Sub

    Private Sub txtEEWriteAdd_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEEWriteAdd.Leave
        cmdEEPROMWriteAdd(txtEEWriteAdd.Text)
        txtEEWriteData.Focus()
    End Sub

    Private Sub txtEEWriteData_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEEWriteData.Click
        txtEEWriteData.SelectionStart = 0
        txtEEWriteData.SelectionLength = Len(txtEEWriteData.Text)
    End Sub

    Private Sub txtEEWriteData_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEEWriteData.Enter
        SendStatusBarMessage("Enter data in decimal within the range of 0-255")
    End Sub

    Private Sub txtEEWriteData_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtEEWriteData.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)

        If KeyAscii = 13 Then
            KeyAscii = 0
            tabUserOptions.Focus()
        End If

        e.KeyChar = Chr(KeyAscii)
    End Sub

    Private Sub txtEEWriteData_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEEWriteData.Leave
        cmdEEPROMWriteData(txtEEWriteData.Text)
        txtEEWriteAdd.Text = Str(Val(txtEEWriteAdd.Text) + 1)
        tabUserOptions.Focus()
    End Sub

    Private Sub txtNewSN_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNewSN.Enter
        NewSN = ""
        txtNewSN.Text = ""
    End Sub

    Private Sub txtNewSN_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNewSN.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)

        If KeyAscii = 13 Then
            If Len(NewSN) = 7 Then
                CommandSNWrite(NewSN)
                NewSN = ""
                Me.Cursor = Cursors.Default
                tabUserOptions.Focus()
            Else
                MsgBox("S/N should be 7 characters", MsgBoxStyle.Exclamation, "Invalid Settings")
            End If
        ElseIf (KeyAscii < 20) Or (KeyAscii > 122) Then
            NewSN = ""
            txtNewSN.Text = ""
        Else
            NewSN &= Convert.ToString(Chr(KeyAscii))
        End If

        e.KeyChar = Chr(KeyAscii)
    End Sub

    Public Function lblVolt(ByVal Index As Integer) As System.Windows.Forms.Label
        Select Case Index
            Case 1
                lblVolt = lblVoltPS1

            Case 2
                lblVolt = lblVoltPS2

            Case 3
                lblVolt = lblVoltPS3

            Case 4
                lblVolt = lblVoltPS4

            Case 5
                lblVolt = lblVoltPS5

            Case 6
                lblVolt = lblVoltPS6

            Case 7
                lblVolt = lblVoltPS7

            Case 8
                lblVolt = lblVoltPS8

            Case 9
                lblVolt = lblVoltPS9

            Case 10
                lblVolt = lblVoltPS10

            Case Else
                lblVolt = lblVoltPS1
        End Select
    End Function

    Public Function lblMeas(ByVal Index As Integer) As System.Windows.Forms.Label
        Select Case Index
            Case 1
                lblMeas = lblMeasPS1

            Case 2
                lblMeas = lblMeasPS2

            Case 3
                lblMeas = lblMeasPS3

            Case 4
                lblMeas = lblMeasPS4

            Case 5
                lblMeas = lblMeasPS5

            Case 6
                lblMeas = lblMeasPS6

            Case 7
                lblMeas = lblMeasPS7

            Case 8
                lblMeas = lblMeasPS8

            Case 9
                lblMeas = lblMeasPS9

            Case 10
                lblMeas = lblMeasPS10

            Case Else
                lblMeas = lblMeasPS1
        End Select
    End Function
End Class