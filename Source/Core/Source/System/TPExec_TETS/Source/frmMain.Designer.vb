<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Public Class frmMain
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
    Public WithEvents TimerProbe As System.Windows.Forms.Timer
    Public WithEvents Timer3 As System.Windows.Forms.Timer
    Public WithEvents TimerStatusByteRec As System.Windows.Forms.Timer
    Public WithEvents Timer2b As System.Windows.Forms.Timer
    Public WithEvents Timer1b As System.Windows.Forms.Timer
    Public WithEvents ProgressBar As ProgressBar
    Public WithEvents Timer2 As System.Windows.Forms.Timer
    Public WithEvents Timer1 As System.Windows.Forms.Timer
    Public WithEvents ImageList2 As ImageList
    Public WithEvents picStatus_4 As System.Windows.Forms.PictureBox
    Public WithEvents picStatus_3 As System.Windows.Forms.PictureBox
    Public WithEvents picStatus_2 As System.Windows.Forms.PictureBox
    Public WithEvents picStatus_1 As System.Windows.Forms.PictureBox
    Public WithEvents picStatus_0 As System.Windows.Forms.PictureBox
    Public WithEvents Quit As System.Windows.Forms.Button
    Public WithEvents MainMenuButton As System.Windows.Forms.Button
    Public WithEvents fraProgramNavigation As System.Windows.Forms.GroupBox
    Public WithEvents NewUUT As System.Windows.Forms.Button
    Public WithEvents YesButton As System.Windows.Forms.Button
    Public WithEvents AbortButton As System.Windows.Forms.Button
    Public WithEvents cmdContinue As System.Windows.Forms.Button
    Public WithEvents RerunButton As System.Windows.Forms.Button
    Public WithEvents PrintButton As System.Windows.Forms.Button
    Public WithEvents NoButton As System.Windows.Forms.Button
    Public WithEvents fraProgramControl As System.Windows.Forms.GroupBox
    Public WithEvents ImageList1 As ImageList
    Public WithEvents txtMeasuredBig As System.Windows.Forms.TextBox
    Public WithEvents lblHigh As System.Windows.Forms.Label
    Public WithEvents lblLow As System.Windows.Forms.Label
    Public WithEvents frMeasContinuous As System.Windows.Forms.GroupBox
    Public WithEvents cboPartsList As System.Windows.Forms.ComboBox
    Public WithEvents cboAssembly As System.Windows.Forms.ComboBox
    Public WithEvents cboSchematic As System.Windows.Forms.ComboBox
    Public WithEvents cboITAPartsList As System.Windows.Forms.ComboBox
    Public WithEvents cboITAAssy As System.Windows.Forms.ComboBox
    Public WithEvents cboITAWiring As System.Windows.Forms.ComboBox
    Public WithEvents MenuOption_7 As System.Windows.Forms.Button
    Public WithEvents MenuOption_8 As System.Windows.Forms.Button
    Public WithEvents MenuOption_10 As System.Windows.Forms.Button
    Public WithEvents MenuOption_11 As System.Windows.Forms.Button
    Public WithEvents MenuOption_9 As System.Windows.Forms.Button
    Public WithEvents MenuOption_12 As System.Windows.Forms.Button
    Public WithEvents MenuOptionText_12 As System.Windows.Forms.Label
    Public WithEvents MenuOptionText_8 As System.Windows.Forms.Label
    Public WithEvents MenuOptionText_11 As System.Windows.Forms.Label
    Public WithEvents MenuOptionText_10 As System.Windows.Forms.Label
    Public WithEvents MenuOptionText_9 As System.Windows.Forms.Label
    Public WithEvents MenuOptionText_7 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents picTestDocumentation As System.Windows.Forms.Panel
    Public WithEvents MenuOption_4 As System.Windows.Forms.Button
    Public WithEvents MenuOption_2 As System.Windows.Forms.Button
    Public WithEvents MenuOption_3 As System.Windows.Forms.Button
    Public WithEvents MenuOption_1 As System.Windows.Forms.Button
    Public WithEvents MenuOption_5 As System.Windows.Forms.Button
    Public WithEvents MenuOption_6 As System.Windows.Forms.Button
    Public WithEvents MenuOption_13 As System.Windows.Forms.Button
    Public WithEvents MenuOption_14 As System.Windows.Forms.Button
    Public WithEvents MenuOptionText_14 As System.Windows.Forms.Label
    Public WithEvents lblAbout As System.Windows.Forms.Label
    Public WithEvents MenuOptionText_13 As System.Windows.Forms.Label
    Public WithEvents MenuOptionText_6 As System.Windows.Forms.Label
    Public WithEvents MenuOptionText_5 As System.Windows.Forms.Label
    Public WithEvents MenuOptionText_4 As System.Windows.Forms.Label
    Public WithEvents MenuOptionText_3 As System.Windows.Forms.Label
    Public WithEvents MenuOptionText_2 As System.Windows.Forms.Label
    Public WithEvents MenuOptionText_1 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents MainMenu As System.Windows.Forms.Panel
    Public WithEvents pinp As System.Windows.Forms.PictureBox
    Public WithEvents Message As System.Windows.Forms.Label
    Public WithEvents PictureWindow As System.Windows.Forms.Panel
    Public WithEvents cmdPwrOnModule As System.Windows.Forms.Button
    Public WithEvents cmdModule_1 As System.Windows.Forms.Button
    Public WithEvents cmdSTTOModule As System.Windows.Forms.Button
    Public WithEvents cmdEndToEnd As System.Windows.Forms.Button
    Public WithEvents lblModuleRunTime_1 As System.Windows.Forms.Label
    Public WithEvents lblPowerOnRunTime As System.Windows.Forms.Label
    Public WithEvents lblSTTORunTime As System.Windows.Forms.Label
    Public WithEvents lblEndToEndRunTime As System.Windows.Forms.Label
    Public WithEvents lblModuleName_1 As System.Windows.Forms.Label
    Public WithEvents lblModuleStatus_1 As System.Windows.Forms.Label
    Public WithEvents lblPwrOnStatus As System.Windows.Forms.Label
    Public WithEvents lblPwrOn As System.Windows.Forms.Label
    Public WithEvents lblSTTOStatus As System.Windows.Forms.Label
    Public WithEvents lblSTTO As System.Windows.Forms.Label
    Public WithEvents lblEndToEndStatus As System.Windows.Forms.Label
    Public WithEvents lblEndToEnd As System.Windows.Forms.Label
    Public WithEvents ModuleInner As System.Windows.Forms.Panel
    Public WithEvents lblModuleStatusTitle_3 As System.Windows.Forms.Label
    Public WithEvents lblStatusTitle As System.Windows.Forms.Label
    Public WithEvents lblModuleStatusTitle_2 As System.Windows.Forms.Label
    Public WithEvents lblModuleStatusTitle_1 As System.Windows.Forms.Label
    Public WithEvents ModuleMenu As System.Windows.Forms.Panel
    Public WithEvents cmdDiagnostics As System.Windows.Forms.Button
    Public WithEvents cmdFHDB As System.Windows.Forms.Button
    Public WithEvents picDanger As System.Windows.Forms.PictureBox
    Public WithEvents lblPressContinue As System.Windows.Forms.Label
    Public WithEvents lblInstruction_6 As System.Windows.Forms.Label
    Public WithEvents lblInstruction_5 As System.Windows.Forms.Label
    Public WithEvents lblInstruction_4 As System.Windows.Forms.Label
    Public WithEvents lblInstruction_3 As System.Windows.Forms.Label
    Public WithEvents lblInstruction_2 As System.Windows.Forms.Label
    Public WithEvents lblInstruction_1 As System.Windows.Forms.Label
    Public WithEvents lblInstructionType As System.Windows.Forms.Label
    Public WithEvents picInstructions As System.Windows.Forms.Panel
    Public WithEvents fraInstructions As System.Windows.Forms.GroupBox
    Public WithEvents txtInstrument As System.Windows.Forms.TextBox
    Public WithEvents lblCommand As System.Windows.Forms.Label
    Public WithEvents lblInstrument As System.Windows.Forms.Label
    Public WithEvents fraInstrument As System.Windows.Forms.GroupBox
    Public WithEvents txtUnit As System.Windows.Forms.TextBox
    Public WithEvents txtLowerLimit As System.Windows.Forms.TextBox
    Public WithEvents txtUpperLimit As System.Windows.Forms.TextBox
    Public WithEvents txtMeasured As System.Windows.Forms.TextBox
    Public WithEvents lblUnit As System.Windows.Forms.Label
    Public WithEvents lbLowerLimit As System.Windows.Forms.Label
    Public WithEvents lbMeasured As System.Windows.Forms.Label
    Public WithEvents lbUpperLimit As System.Windows.Forms.Label
    Public WithEvents fraMeasurement As System.Windows.Forms.GroupBox
    Public WithEvents txtModule As System.Windows.Forms.TextBox
    Public WithEvents txtTestName As System.Windows.Forms.TextBox
    Public WithEvents txtStep As System.Windows.Forms.TextBox
    Public WithEvents lblModule As System.Windows.Forms.Label
    Public WithEvents lblName As System.Windows.Forms.Label
    Public WithEvents lblStep As System.Windows.Forms.Label
    Public WithEvents fraTestInformation As System.Windows.Forms.GroupBox
    Public WithEvents TextWindow As System.Windows.Forms.TextBox
    Public WithEvents lblTestResults As System.Windows.Forms.Label
    Public WithEvents SeqTextWindow As System.Windows.Forms.Panel
    Public WithEvents lblPowerApplied As System.Windows.Forms.Label
    Public WithEvents MainPanel As System.Windows.Forms.Panel
    Public WithEvents lblMsg_1 As System.Windows.Forms.Label
    Public WithEvents msgInBox As System.Windows.Forms.Panel
    Public WithEvents SSPanel1 As System.Windows.Forms.Panel
    Public WithEvents picGraphic As System.Windows.Forms.PictureBox
    Public WithEvents SSPanel2 As System.Windows.Forms.Panel
    Public WithEvents lblStatus As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.MenuOption_8 = New System.Windows.Forms.Button()
        Me.MenuOption_5 = New System.Windows.Forms.Button()
        Me.cmdPwrOnModule = New System.Windows.Forms.Button()
        Me.ImageList3 = New System.Windows.Forms.ImageList(Me.components)
        Me.cmdModule_1 = New System.Windows.Forms.Button()
        Me.cmdSTTOModule = New System.Windows.Forms.Button()
        Me.cmdEndToEnd = New System.Windows.Forms.Button()
        Me.MenuOption_7 = New System.Windows.Forms.Button()
        Me.MenuOption_9 = New System.Windows.Forms.Button()
        Me.MenuOption_4 = New System.Windows.Forms.Button()
        Me.MenuOption_2 = New System.Windows.Forms.Button()
        Me.MenuOption_3 = New System.Windows.Forms.Button()
        Me.MenuOption_1 = New System.Windows.Forms.Button()
        Me.MenuOption_6 = New System.Windows.Forms.Button()
        Me.TimerProbe = New System.Windows.Forms.Timer(Me.components)
        Me.Timer3 = New System.Windows.Forms.Timer(Me.components)
        Me.TimerStatusByteRec = New System.Windows.Forms.Timer(Me.components)
        Me.Timer2b = New System.Windows.Forms.Timer(Me.components)
        Me.Timer1b = New System.Windows.Forms.Timer(Me.components)
        Me.ProgressBar = New System.Windows.Forms.ProgressBar()
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.ImageList2 = New System.Windows.Forms.ImageList(Me.components)
        Me.fraProgramNavigation = New System.Windows.Forms.GroupBox()
        Me.Quit = New System.Windows.Forms.Button()
        Me.MainMenuButton = New System.Windows.Forms.Button()
        Me.fraProgramControl = New System.Windows.Forms.GroupBox()
        Me.NewUUT = New System.Windows.Forms.Button()
        Me.YesButton = New System.Windows.Forms.Button()
        Me.AbortButton = New System.Windows.Forms.Button()
        Me.cmdContinue = New System.Windows.Forms.Button()
        Me.RerunButton = New System.Windows.Forms.Button()
        Me.PrintButton = New System.Windows.Forms.Button()
        Me.NoButton = New System.Windows.Forms.Button()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.MainPanel = New System.Windows.Forms.Panel()
        Me.pinp = New System.Windows.Forms.PictureBox()
        Me.pinp_ShapeContainer = New Microsoft.VisualBasic.PowerPacks.ShapeContainer()
        Me.pinp_Line = New Microsoft.VisualBasic.PowerPacks.LineShape()
        Me.MainMenu = New System.Windows.Forms.Panel()
        Me.cboPartsList = New System.Windows.Forms.ComboBox()
        Me.cboAssembly = New System.Windows.Forms.ComboBox()
        Me.cboSchematic = New System.Windows.Forms.ComboBox()
        Me.picTestDocumentation = New System.Windows.Forms.Panel()
        Me.frMeasContinuous = New System.Windows.Forms.GroupBox()
        Me.txtMeasuredBig = New System.Windows.Forms.TextBox()
        Me.lblHigh = New System.Windows.Forms.Label()
        Me.lblLow = New System.Windows.Forms.Label()
        Me.cboITAPartsList = New System.Windows.Forms.ComboBox()
        Me.cboITAAssy = New System.Windows.Forms.ComboBox()
        Me.cboITAWiring = New System.Windows.Forms.ComboBox()
        Me.MenuOption_10 = New System.Windows.Forms.Button()
        Me.MenuOption_11 = New System.Windows.Forms.Button()
        Me.MenuOption_12 = New System.Windows.Forms.Button()
        Me.MenuOptionText_12 = New System.Windows.Forms.Label()
        Me.MenuOptionText_8 = New System.Windows.Forms.Label()
        Me.MenuOptionText_11 = New System.Windows.Forms.Label()
        Me.MenuOptionText_10 = New System.Windows.Forms.Label()
        Me.MenuOptionText_9 = New System.Windows.Forms.Label()
        Me.MenuOptionText_7 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.MenuOption_13 = New System.Windows.Forms.Button()
        Me.MenuOption_14 = New System.Windows.Forms.Button()
        Me.MenuOptionText_14 = New System.Windows.Forms.Label()
        Me.lblAbout = New System.Windows.Forms.Label()
        Me.MenuOptionText_13 = New System.Windows.Forms.Label()
        Me.MenuOptionText_6 = New System.Windows.Forms.Label()
        Me.MenuOptionText_5 = New System.Windows.Forms.Label()
        Me.MenuOptionText_4 = New System.Windows.Forms.Label()
        Me.MenuOptionText_3 = New System.Windows.Forms.Label()
        Me.MenuOptionText_2 = New System.Windows.Forms.Label()
        Me.MenuOptionText_1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.PictureWindow = New System.Windows.Forms.Panel()
        Me.Message = New System.Windows.Forms.Label()
        Me.PictureWindow_ShapeContainer = New Microsoft.VisualBasic.PowerPacks.ShapeContainer()
        Me.PictureWindow_Line = New Microsoft.VisualBasic.PowerPacks.LineShape()
        Me.ModuleMenu = New System.Windows.Forms.Panel()
        Me.ModuleInner = New System.Windows.Forms.Panel()
        Me.lblModuleRunTime_1 = New System.Windows.Forms.Label()
        Me.lblPowerOnRunTime = New System.Windows.Forms.Label()
        Me.lblSTTORunTime = New System.Windows.Forms.Label()
        Me.lblEndToEndRunTime = New System.Windows.Forms.Label()
        Me.lblModuleName_1 = New System.Windows.Forms.Label()
        Me.lblModuleStatus_1 = New System.Windows.Forms.Label()
        Me.lblPwrOnStatus = New System.Windows.Forms.Label()
        Me.lblPwrOn = New System.Windows.Forms.Label()
        Me.lblSTTOStatus = New System.Windows.Forms.Label()
        Me.lblSTTO = New System.Windows.Forms.Label()
        Me.lblEndToEndStatus = New System.Windows.Forms.Label()
        Me.lblEndToEnd = New System.Windows.Forms.Label()
        Me.lblModuleStatusTitle_3 = New System.Windows.Forms.Label()
        Me.lblStatusTitle = New System.Windows.Forms.Label()
        Me.lblModuleStatusTitle_2 = New System.Windows.Forms.Label()
        Me.lblModuleStatusTitle_1 = New System.Windows.Forms.Label()
        Me.SeqTextWindow = New System.Windows.Forms.Panel()
        Me.SeqTextWindowLabel = New System.Windows.Forms.Label()
        Me.lblTestResults = New System.Windows.Forms.Label()
        Me.cmdDiagnostics = New System.Windows.Forms.Button()
        Me.cmdFHDB = New System.Windows.Forms.Button()
        Me.picDanger = New System.Windows.Forms.PictureBox()
        Me.fraInstructions = New System.Windows.Forms.GroupBox()
        Me.picInstructions = New System.Windows.Forms.Panel()
        Me.lblPressContinue = New System.Windows.Forms.Label()
        Me.lblInstruction_6 = New System.Windows.Forms.Label()
        Me.lblInstruction_5 = New System.Windows.Forms.Label()
        Me.lblInstruction_4 = New System.Windows.Forms.Label()
        Me.lblInstruction_3 = New System.Windows.Forms.Label()
        Me.lblInstruction_2 = New System.Windows.Forms.Label()
        Me.lblInstruction_1 = New System.Windows.Forms.Label()
        Me.lblInstructionType = New System.Windows.Forms.Label()
        Me.fraInstrument = New System.Windows.Forms.GroupBox()
        Me.txtCommand = New System.Windows.Forms.TextBox()
        Me.txtInstrument = New System.Windows.Forms.TextBox()
        Me.lblCommand = New System.Windows.Forms.Label()
        Me.lblInstrument = New System.Windows.Forms.Label()
        Me.fraMeasurement = New System.Windows.Forms.GroupBox()
        Me.txtUnit = New System.Windows.Forms.TextBox()
        Me.txtLowerLimit = New System.Windows.Forms.TextBox()
        Me.txtUpperLimit = New System.Windows.Forms.TextBox()
        Me.txtMeasured = New System.Windows.Forms.TextBox()
        Me.lblUnit = New System.Windows.Forms.Label()
        Me.lbLowerLimit = New System.Windows.Forms.Label()
        Me.lbMeasured = New System.Windows.Forms.Label()
        Me.lbUpperLimit = New System.Windows.Forms.Label()
        Me.fraTestInformation = New System.Windows.Forms.GroupBox()
        Me.txtModule = New System.Windows.Forms.TextBox()
        Me.txtTestName = New System.Windows.Forms.TextBox()
        Me.txtStep = New System.Windows.Forms.TextBox()
        Me.lblModule = New System.Windows.Forms.Label()
        Me.lblName = New System.Windows.Forms.Label()
        Me.lblStep = New System.Windows.Forms.Label()
        Me.TextWindow = New System.Windows.Forms.TextBox()
        Me.lblPowerApplied = New System.Windows.Forms.Label()
        Me.SSPanel1 = New System.Windows.Forms.Panel()
        Me.msgInBox = New System.Windows.Forms.Panel()
        Me.lblMsg_1 = New System.Windows.Forms.Label()
        Me.SSPanel2 = New System.Windows.Forms.Panel()
        Me.picGraphic = New System.Windows.Forms.PictureBox()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.picStatus_4 = New System.Windows.Forms.PictureBox()
        Me.picStatus_3 = New System.Windows.Forms.PictureBox()
        Me.picStatus_2 = New System.Windows.Forms.PictureBox()
        Me.picStatus_1 = New System.Windows.Forms.PictureBox()
        Me.picStatus_0 = New System.Windows.Forms.PictureBox()
        Me.fraProgramNavigation.SuspendLayout()
        Me.fraProgramControl.SuspendLayout()
        Me.MainPanel.SuspendLayout()
        CType(Me.pinp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pinp.SuspendLayout()
        Me.MainMenu.SuspendLayout()
        Me.picTestDocumentation.SuspendLayout()
        Me.frMeasContinuous.SuspendLayout()
        Me.PictureWindow.SuspendLayout()
        Me.ModuleMenu.SuspendLayout()
        Me.ModuleInner.SuspendLayout()
        Me.SeqTextWindow.SuspendLayout()
        CType(Me.picDanger, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraInstructions.SuspendLayout()
        Me.picInstructions.SuspendLayout()
        Me.fraInstrument.SuspendLayout()
        Me.fraMeasurement.SuspendLayout()
        Me.fraTestInformation.SuspendLayout()
        Me.SSPanel1.SuspendLayout()
        Me.msgInBox.SuspendLayout()
        Me.SSPanel2.SuspendLayout()
        CType(Me.picGraphic, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picStatus_4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picStatus_3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picStatus_2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picStatus_1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picStatus_0, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MenuOption_8
        '
        Me.MenuOption_8.AutoSize = True
        Me.MenuOption_8.Image = CType(resources.GetObject("MenuOption_8.Image"), System.Drawing.Image)
        Me.MenuOption_8.Location = New System.Drawing.Point(88, 176)
        Me.MenuOption_8.Name = "MenuOption_8"
        Me.MenuOption_8.Size = New System.Drawing.Size(74, 74)
        Me.MenuOption_8.TabIndex = 99
        Me.ToolTip1.SetToolTip(Me.MenuOption_8, "Launches ID Assembly View Software")
        '
        'MenuOption_5
        '
        Me.MenuOption_5.AutoSize = True
        Me.MenuOption_5.BackColor = System.Drawing.Color.Silver
        Me.MenuOption_5.Location = New System.Drawing.Point(432, 80)
        Me.MenuOption_5.Name = "MenuOption_5"
        Me.MenuOption_5.Size = New System.Drawing.Size(73, 73)
        Me.MenuOption_5.TabIndex = 38
        Me.ToolTip1.SetToolTip(Me.MenuOption_5, "Launches ID Survey")
        Me.MenuOption_5.UseVisualStyleBackColor = False
        '
        'cmdPwrOnModule
        '
        Me.cmdPwrOnModule.AutoSize = True
        Me.cmdPwrOnModule.BackColor = System.Drawing.Color.Silver
        Me.cmdPwrOnModule.ImageIndex = 2
        Me.cmdPwrOnModule.ImageList = Me.ImageList3
        Me.cmdPwrOnModule.Location = New System.Drawing.Point(144, 112)
        Me.cmdPwrOnModule.Name = "cmdPwrOnModule"
        Me.cmdPwrOnModule.Size = New System.Drawing.Size(73, 41)
        Me.cmdPwrOnModule.TabIndex = 25
        Me.ToolTip1.SetToolTip(Me.cmdPwrOnModule, "Click Here to Run PWR On Test ")
        Me.cmdPwrOnModule.UseVisualStyleBackColor = False
        '
        'ImageList3
        '
        Me.ImageList3.ImageStream = CType(resources.GetObject("ImageList3.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList3.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList3.Images.SetKeyName(0, "syscal.ico")
        Me.ImageList3.Images.SetKeyName(1, "tps.ICO")
        Me.ImageList3.Images.SetKeyName(2, "danger.ico")
        Me.ImageList3.Images.SetKeyName(3, "tpsd.ico")
        '
        'cmdModule_1
        '
        Me.cmdModule_1.AutoSize = True
        Me.cmdModule_1.BackColor = System.Drawing.Color.Silver
        Me.cmdModule_1.ImageIndex = 3
        Me.cmdModule_1.ImageList = Me.ImageList3
        Me.cmdModule_1.Location = New System.Drawing.Point(146, 163)
        Me.cmdModule_1.Name = "cmdModule_1"
        Me.cmdModule_1.Size = New System.Drawing.Size(73, 41)
        Me.cmdModule_1.TabIndex = 26
        Me.ToolTip1.SetToolTip(Me.cmdModule_1, "Click Here to Run Test Module")
        Me.cmdModule_1.UseVisualStyleBackColor = False
        '
        'cmdSTTOModule
        '
        Me.cmdSTTOModule.AutoSize = True
        Me.cmdSTTOModule.BackColor = System.Drawing.Color.Silver
        Me.cmdSTTOModule.ImageIndex = 1
        Me.cmdSTTOModule.ImageList = Me.ImageList3
        Me.cmdSTTOModule.Location = New System.Drawing.Point(144, 64)
        Me.cmdSTTOModule.Name = "cmdSTTOModule"
        Me.cmdSTTOModule.Size = New System.Drawing.Size(73, 41)
        Me.cmdSTTOModule.TabIndex = 27
        Me.ToolTip1.SetToolTip(Me.cmdSTTOModule, "Click Here to Safe-To-Turn-On Test")
        Me.cmdSTTOModule.UseVisualStyleBackColor = False
        '
        'cmdEndToEnd
        '
        Me.cmdEndToEnd.AutoSize = True
        Me.cmdEndToEnd.BackColor = System.Drawing.Color.Silver
        Me.cmdEndToEnd.ImageIndex = 0
        Me.cmdEndToEnd.ImageList = Me.ImageList3
        Me.cmdEndToEnd.Location = New System.Drawing.Point(144, 16)
        Me.cmdEndToEnd.Name = "cmdEndToEnd"
        Me.cmdEndToEnd.Size = New System.Drawing.Size(73, 41)
        Me.cmdEndToEnd.TabIndex = 28
        Me.ToolTip1.SetToolTip(Me.cmdEndToEnd, "Click Here to Run End-To-End Test")
        Me.cmdEndToEnd.UseVisualStyleBackColor = False
        '
        'MenuOption_7
        '
        Me.MenuOption_7.AutoSize = True
        Me.MenuOption_7.Image = CType(resources.GetObject("MenuOption_7.Image"), System.Drawing.Image)
        Me.MenuOption_7.Location = New System.Drawing.Point(88, 72)
        Me.MenuOption_7.Name = "MenuOption_7"
        Me.MenuOption_7.Size = New System.Drawing.Size(74, 74)
        Me.MenuOption_7.TabIndex = 98
        Me.ToolTip1.SetToolTip(Me.MenuOption_7, "Launches ID Schematic View Software")
        '
        'MenuOption_9
        '
        Me.MenuOption_9.AutoSize = True
        Me.MenuOption_9.Image = CType(resources.GetObject("MenuOption_9.Image"), System.Drawing.Image)
        Me.MenuOption_9.Location = New System.Drawing.Point(88, 280)
        Me.MenuOption_9.Name = "MenuOption_9"
        Me.MenuOption_9.Size = New System.Drawing.Size(74, 74)
        Me.MenuOption_9.TabIndex = 106
        Me.ToolTip1.SetToolTip(Me.MenuOption_9, "Launches ID Parts List View Software")
        '
        'MenuOption_4
        '
        Me.MenuOption_4.AutoSize = True
        Me.MenuOption_4.BackColor = System.Drawing.Color.Silver
        Me.MenuOption_4.Image = CType(resources.GetObject("MenuOption_4.Image"), System.Drawing.Image)
        Me.MenuOption_4.Location = New System.Drawing.Point(80, 392)
        Me.MenuOption_4.Name = "MenuOption_4"
        Me.MenuOption_4.Size = New System.Drawing.Size(74, 74)
        Me.MenuOption_4.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.MenuOption_4, "Launches Parts List View Software")
        Me.MenuOption_4.UseVisualStyleBackColor = False
        '
        'MenuOption_2
        '
        Me.MenuOption_2.AutoSize = True
        Me.MenuOption_2.BackColor = System.Drawing.Color.Silver
        Me.MenuOption_2.Image = CType(resources.GetObject("MenuOption_2.Image"), System.Drawing.Image)
        Me.MenuOption_2.Location = New System.Drawing.Point(80, 184)
        Me.MenuOption_2.Name = "MenuOption_2"
        Me.MenuOption_2.Size = New System.Drawing.Size(74, 74)
        Me.MenuOption_2.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.MenuOption_2, "Launches Schematic View Software")
        Me.MenuOption_2.UseVisualStyleBackColor = False
        '
        'MenuOption_3
        '
        Me.MenuOption_3.AutoSize = True
        Me.MenuOption_3.BackColor = System.Drawing.Color.Silver
        Me.MenuOption_3.Image = CType(resources.GetObject("MenuOption_3.Image"), System.Drawing.Image)
        Me.MenuOption_3.Location = New System.Drawing.Point(80, 288)
        Me.MenuOption_3.Name = "MenuOption_3"
        Me.MenuOption_3.Size = New System.Drawing.Size(74, 74)
        Me.MenuOption_3.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.MenuOption_3, "Launches Assembly View Software")
        Me.MenuOption_3.UseVisualStyleBackColor = False
        '
        'MenuOption_1
        '
        Me.MenuOption_1.AutoSize = True
        Me.MenuOption_1.BackColor = System.Drawing.Color.Silver
        Me.MenuOption_1.Image = CType(resources.GetObject("MenuOption_1.Image"), System.Drawing.Image)
        Me.MenuOption_1.Location = New System.Drawing.Point(80, 80)
        Me.MenuOption_1.Name = "MenuOption_1"
        Me.MenuOption_1.Size = New System.Drawing.Size(73, 73)
        Me.MenuOption_1.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.MenuOption_1, "Launches Test Program Software")
        Me.MenuOption_1.UseVisualStyleBackColor = False
        '
        'MenuOption_6
        '
        Me.MenuOption_6.AutoSize = True
        Me.MenuOption_6.BackColor = System.Drawing.Color.Silver
        Me.MenuOption_6.Image = CType(resources.GetObject("MenuOption_6.Image"), System.Drawing.Image)
        Me.MenuOption_6.Location = New System.Drawing.Point(432, 184)
        Me.MenuOption_6.Name = "MenuOption_6"
        Me.MenuOption_6.Size = New System.Drawing.Size(73, 73)
        Me.MenuOption_6.TabIndex = 39
        Me.ToolTip1.SetToolTip(Me.MenuOption_6, "Show's Test Documentation Menu")
        Me.MenuOption_6.UseVisualStyleBackColor = False
        '
        'TimerProbe
        '
        Me.TimerProbe.Interval = 500
        '
        'Timer3
        '
        Me.Timer3.Interval = 1000
        '
        'TimerStatusByteRec
        '
        Me.TimerStatusByteRec.Interval = 500
        '
        'Timer2b
        '
        Me.Timer2b.Interval = 450
        '
        'Timer1b
        '
        Me.Timer1b.Interval = 450
        '
        'ProgressBar
        '
        Me.ProgressBar.Enabled = False
        Me.ProgressBar.Location = New System.Drawing.Point(616, 608)
        Me.ProgressBar.Name = "ProgressBar"
        Me.ProgressBar.Size = New System.Drawing.Size(9, 17)
        Me.ProgressBar.TabIndex = 111
        Me.ProgressBar.Visible = False
        '
        'Timer2
        '
        Me.Timer2.Interval = 900
        '
        'Timer1
        '
        Me.Timer1.Interval = 900
        '
        'ImageList2
        '
        Me.ImageList2.ImageStream = CType(resources.GetObject("ImageList2.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList2.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList2.Images.SetKeyName(0, "ipp_0005.gif")
        Me.ImageList2.Images.SetKeyName(1, "ipp_0012.gif")
        '
        'fraProgramNavigation
        '
        Me.fraProgramNavigation.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.fraProgramNavigation.Controls.Add(Me.Quit)
        Me.fraProgramNavigation.Controls.Add(Me.MainMenuButton)
        Me.fraProgramNavigation.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraProgramNavigation.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraProgramNavigation.Location = New System.Drawing.Point(8, 600)
        Me.fraProgramNavigation.Name = "fraProgramNavigation"
        Me.fraProgramNavigation.Padding = New System.Windows.Forms.Padding(0)
        Me.fraProgramNavigation.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraProgramNavigation.Size = New System.Drawing.Size(225, 57)
        Me.fraProgramNavigation.TabIndex = 18
        Me.fraProgramNavigation.TabStop = False
        Me.fraProgramNavigation.Text = "TPS Navigation"
        '
        'Quit
        '
        Me.Quit.Enabled = False
        Me.Quit.Location = New System.Drawing.Point(8, 16)
        Me.Quit.Name = "Quit"
        Me.Quit.Size = New System.Drawing.Size(100, 29)
        Me.Quit.TabIndex = 19
        Me.Quit.Text = "&Quit"
        '
        'MainMenuButton
        '
        Me.MainMenuButton.Enabled = False
        Me.MainMenuButton.Location = New System.Drawing.Point(112, 16)
        Me.MainMenuButton.Name = "MainMenuButton"
        Me.MainMenuButton.Size = New System.Drawing.Size(100, 29)
        Me.MainMenuButton.TabIndex = 20
        Me.MainMenuButton.Text = "&Main Menu"
        '
        'fraProgramControl
        '
        Me.fraProgramControl.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.fraProgramControl.Controls.Add(Me.NewUUT)
        Me.fraProgramControl.Controls.Add(Me.YesButton)
        Me.fraProgramControl.Controls.Add(Me.AbortButton)
        Me.fraProgramControl.Controls.Add(Me.cmdContinue)
        Me.fraProgramControl.Controls.Add(Me.RerunButton)
        Me.fraProgramControl.Controls.Add(Me.PrintButton)
        Me.fraProgramControl.Controls.Add(Me.NoButton)
        Me.fraProgramControl.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraProgramControl.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraProgramControl.Location = New System.Drawing.Point(240, 600)
        Me.fraProgramControl.Name = "fraProgramControl"
        Me.fraProgramControl.Padding = New System.Windows.Forms.Padding(0)
        Me.fraProgramControl.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraProgramControl.Size = New System.Drawing.Size(321, 57)
        Me.fraProgramControl.TabIndex = 58
        Me.fraProgramControl.TabStop = False
        Me.fraProgramControl.Text = "TPS Control"
        '
        'NewUUT
        '
        Me.NewUUT.Location = New System.Drawing.Point(216, 16)
        Me.NewUUT.Name = "NewUUT"
        Me.NewUUT.Size = New System.Drawing.Size(100, 29)
        Me.NewUUT.TabIndex = 112
        Me.NewUUT.Text = "&New UUT"
        Me.NewUUT.Visible = False
        '
        'YesButton
        '
        Me.YesButton.Enabled = False
        Me.YesButton.Location = New System.Drawing.Point(112, 16)
        Me.YesButton.Name = "YesButton"
        Me.YesButton.Size = New System.Drawing.Size(100, 29)
        Me.YesButton.TabIndex = 108
        Me.YesButton.Text = "&Yes"
        Me.YesButton.Visible = False
        '
        'AbortButton
        '
        Me.AbortButton.Enabled = False
        Me.AbortButton.Location = New System.Drawing.Point(112, 16)
        Me.AbortButton.Name = "AbortButton"
        Me.AbortButton.Size = New System.Drawing.Size(100, 29)
        Me.AbortButton.TabIndex = 59
        Me.AbortButton.Text = "&Abort"
        '
        'cmdContinue
        '
        Me.cmdContinue.Enabled = False
        Me.cmdContinue.Location = New System.Drawing.Point(216, 16)
        Me.cmdContinue.Name = "cmdContinue"
        Me.cmdContinue.Size = New System.Drawing.Size(100, 29)
        Me.cmdContinue.TabIndex = 60
        Me.cmdContinue.Text = "&Continue"
        '
        'RerunButton
        '
        Me.RerunButton.Enabled = False
        Me.RerunButton.Location = New System.Drawing.Point(112, 16)
        Me.RerunButton.Name = "RerunButton"
        Me.RerunButton.Size = New System.Drawing.Size(100, 29)
        Me.RerunButton.TabIndex = 61
        Me.RerunButton.Text = "R&erun"
        Me.RerunButton.Visible = False
        '
        'PrintButton
        '
        Me.PrintButton.Enabled = False
        Me.PrintButton.Location = New System.Drawing.Point(8, 16)
        Me.PrintButton.Name = "PrintButton"
        Me.PrintButton.Size = New System.Drawing.Size(100, 29)
        Me.PrintButton.TabIndex = 62
        Me.PrintButton.Text = "&Print"
        '
        'NoButton
        '
        Me.NoButton.Enabled = False
        Me.NoButton.Location = New System.Drawing.Point(216, 16)
        Me.NoButton.Name = "NoButton"
        Me.NoButton.Size = New System.Drawing.Size(100, 29)
        Me.NoButton.TabIndex = 109
        Me.NoButton.Text = "&No"
        Me.NoButton.Visible = False
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "66x66IDSurvey.bmp")
        Me.ImageList1.Images.SetKeyName(1, "66x66IDSurveyFAILED.bmp")
        Me.ImageList1.Images.SetKeyName(2, "66x66IDSurveyPassed.bmp")
        '
        'MainPanel
        '
        Me.MainPanel.Controls.Add(Me.pinp)
        Me.MainPanel.Controls.Add(Me.MainMenu)
        Me.MainPanel.Controls.Add(Me.PictureWindow)
        Me.MainPanel.Controls.Add(Me.ModuleMenu)
        Me.MainPanel.Controls.Add(Me.SeqTextWindow)
        Me.MainPanel.Controls.Add(Me.cmdDiagnostics)
        Me.MainPanel.Controls.Add(Me.cmdFHDB)
        Me.MainPanel.Controls.Add(Me.picDanger)
        Me.MainPanel.Controls.Add(Me.fraInstructions)
        Me.MainPanel.Controls.Add(Me.fraInstrument)
        Me.MainPanel.Controls.Add(Me.fraMeasurement)
        Me.MainPanel.Controls.Add(Me.fraTestInformation)
        Me.MainPanel.Controls.Add(Me.TextWindow)
        Me.MainPanel.Controls.Add(Me.lblPowerApplied)
        Me.MainPanel.Location = New System.Drawing.Point(8, 8)
        Me.MainPanel.Name = "MainPanel"
        Me.MainPanel.Size = New System.Drawing.Size(842, 593)
        Me.MainPanel.TabIndex = 4
        '
        'pinp
        '
        Me.pinp.BackColor = System.Drawing.SystemColors.Window
        Me.pinp.Controls.Add(Me.pinp_ShapeContainer)
        Me.pinp.Cursor = System.Windows.Forms.Cursors.Default
        Me.pinp.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pinp.ForeColor = System.Drawing.SystemColors.WindowText
        Me.pinp.Location = New System.Drawing.Point(528, 172)
        Me.pinp.Name = "pinp"
        Me.pinp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.pinp.Size = New System.Drawing.Size(593, 337)
        Me.pinp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.pinp.TabIndex = 14
        Me.pinp.TabStop = False
        '
        'pinp_ShapeContainer
        '
        Me.pinp_ShapeContainer.Location = New System.Drawing.Point(0, 0)
        Me.pinp_ShapeContainer.Margin = New System.Windows.Forms.Padding(0)
        Me.pinp_ShapeContainer.Name = "pinp_ShapeContainer"
        Me.pinp_ShapeContainer.Shapes.AddRange(New Microsoft.VisualBasic.PowerPacks.Shape() {Me.pinp_Line})
        Me.pinp_ShapeContainer.Size = New System.Drawing.Size(593, 337)
        Me.pinp_ShapeContainer.TabIndex = 57
        Me.pinp_ShapeContainer.TabStop = False
        '
        'pinp_Line
        '
        Me.pinp_Line.Name = "LineShape1"
        Me.pinp_Line.Visible = False
        Me.pinp_Line.X1 = 124
        Me.pinp_Line.X2 = 169
        Me.pinp_Line.Y1 = 57
        Me.pinp_Line.Y2 = 119
        '
        'MainMenu
        '
        Me.MainMenu.BackColor = System.Drawing.SystemColors.Window
        Me.MainMenu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.MainMenu.Controls.Add(Me.cboPartsList)
        Me.MainMenu.Controls.Add(Me.cboAssembly)
        Me.MainMenu.Controls.Add(Me.cboSchematic)
        Me.MainMenu.Controls.Add(Me.picTestDocumentation)
        Me.MainMenu.Controls.Add(Me.MenuOption_4)
        Me.MainMenu.Controls.Add(Me.MenuOption_2)
        Me.MainMenu.Controls.Add(Me.MenuOption_3)
        Me.MainMenu.Controls.Add(Me.MenuOption_1)
        Me.MainMenu.Controls.Add(Me.MenuOption_5)
        Me.MainMenu.Controls.Add(Me.MenuOption_6)
        Me.MainMenu.Controls.Add(Me.MenuOption_13)
        Me.MainMenu.Controls.Add(Me.MenuOption_14)
        Me.MainMenu.Controls.Add(Me.MenuOptionText_14)
        Me.MainMenu.Controls.Add(Me.lblAbout)
        Me.MainMenu.Controls.Add(Me.MenuOptionText_13)
        Me.MainMenu.Controls.Add(Me.MenuOptionText_6)
        Me.MainMenu.Controls.Add(Me.MenuOptionText_5)
        Me.MainMenu.Controls.Add(Me.MenuOptionText_4)
        Me.MainMenu.Controls.Add(Me.MenuOptionText_3)
        Me.MainMenu.Controls.Add(Me.MenuOptionText_2)
        Me.MainMenu.Controls.Add(Me.MenuOptionText_1)
        Me.MainMenu.Controls.Add(Me.Label3)
        Me.MainMenu.Cursor = System.Windows.Forms.Cursors.Default
        Me.MainMenu.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MainMenu.ForeColor = System.Drawing.SystemColors.WindowText
        Me.MainMenu.Location = New System.Drawing.Point(16, 8)
        Me.MainMenu.Name = "MainMenu"
        Me.MainMenu.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.MainMenu.Size = New System.Drawing.Size(476, 577)
        Me.MainMenu.TabIndex = 8
        Me.MainMenu.TabStop = True
        '
        'cboPartsList
        '
        Me.cboPartsList.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.cboPartsList.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboPartsList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPartsList.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboPartsList.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboPartsList.Location = New System.Drawing.Point(168, 440)
        Me.cboPartsList.Name = "cboPartsList"
        Me.cboPartsList.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboPartsList.Size = New System.Drawing.Size(225, 24)
        Me.cboPartsList.TabIndex = 128
        '
        'cboAssembly
        '
        Me.cboAssembly.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.cboAssembly.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboAssembly.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboAssembly.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboAssembly.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboAssembly.Location = New System.Drawing.Point(168, 336)
        Me.cboAssembly.Name = "cboAssembly"
        Me.cboAssembly.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboAssembly.Size = New System.Drawing.Size(225, 24)
        Me.cboAssembly.TabIndex = 127
        '
        'cboSchematic
        '
        Me.cboSchematic.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.cboSchematic.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboSchematic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSchematic.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboSchematic.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboSchematic.Location = New System.Drawing.Point(168, 232)
        Me.cboSchematic.Name = "cboSchematic"
        Me.cboSchematic.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboSchematic.Size = New System.Drawing.Size(225, 24)
        Me.cboSchematic.TabIndex = 126
        '
        'picTestDocumentation
        '
        Me.picTestDocumentation.BackColor = System.Drawing.SystemColors.Window
        Me.picTestDocumentation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picTestDocumentation.Controls.Add(Me.frMeasContinuous)
        Me.picTestDocumentation.Controls.Add(Me.cboITAPartsList)
        Me.picTestDocumentation.Controls.Add(Me.cboITAAssy)
        Me.picTestDocumentation.Controls.Add(Me.cboITAWiring)
        Me.picTestDocumentation.Controls.Add(Me.MenuOption_7)
        Me.picTestDocumentation.Controls.Add(Me.MenuOption_8)
        Me.picTestDocumentation.Controls.Add(Me.MenuOption_10)
        Me.picTestDocumentation.Controls.Add(Me.MenuOption_11)
        Me.picTestDocumentation.Controls.Add(Me.MenuOption_9)
        Me.picTestDocumentation.Controls.Add(Me.MenuOption_12)
        Me.picTestDocumentation.Controls.Add(Me.MenuOptionText_12)
        Me.picTestDocumentation.Controls.Add(Me.MenuOptionText_8)
        Me.picTestDocumentation.Controls.Add(Me.MenuOptionText_11)
        Me.picTestDocumentation.Controls.Add(Me.MenuOptionText_10)
        Me.picTestDocumentation.Controls.Add(Me.MenuOptionText_9)
        Me.picTestDocumentation.Controls.Add(Me.MenuOptionText_7)
        Me.picTestDocumentation.Controls.Add(Me.Label1)
        Me.picTestDocumentation.Cursor = System.Windows.Forms.Cursors.Default
        Me.picTestDocumentation.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.picTestDocumentation.ForeColor = System.Drawing.SystemColors.WindowText
        Me.picTestDocumentation.Location = New System.Drawing.Point(0, 0)
        Me.picTestDocumentation.Name = "picTestDocumentation"
        Me.picTestDocumentation.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.picTestDocumentation.Size = New System.Drawing.Size(56, 577)
        Me.picTestDocumentation.TabIndex = 96
        Me.picTestDocumentation.TabStop = True
        '
        'frMeasContinuous
        '
        Me.frMeasContinuous.BackColor = System.Drawing.Color.Silver
        Me.frMeasContinuous.Controls.Add(Me.txtMeasuredBig)
        Me.frMeasContinuous.Controls.Add(Me.lblHigh)
        Me.frMeasContinuous.Controls.Add(Me.lblLow)
        Me.frMeasContinuous.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.frMeasContinuous.ForeColor = System.Drawing.SystemColors.ControlText
        Me.frMeasContinuous.Location = New System.Drawing.Point(168, 247)
        Me.frMeasContinuous.Name = "frMeasContinuous"
        Me.frMeasContinuous.Padding = New System.Windows.Forms.Padding(0)
        Me.frMeasContinuous.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.frMeasContinuous.Size = New System.Drawing.Size(273, 209)
        Me.frMeasContinuous.TabIndex = 131
        Me.frMeasContinuous.TabStop = False
        Me.frMeasContinuous.Text = "Measurement"
        Me.frMeasContinuous.Visible = False
        '
        'txtMeasuredBig
        '
        Me.txtMeasuredBig.AcceptsReturn = True
        Me.txtMeasuredBig.BackColor = System.Drawing.Color.Lime
        Me.txtMeasuredBig.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMeasuredBig.Font = New System.Drawing.Font("Arial", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMeasuredBig.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMeasuredBig.Location = New System.Drawing.Point(24, 88)
        Me.txtMeasuredBig.MaxLength = 0
        Me.txtMeasuredBig.Name = "txtMeasuredBig"
        Me.txtMeasuredBig.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMeasuredBig.Size = New System.Drawing.Size(225, 44)
        Me.txtMeasuredBig.TabIndex = 132
        Me.txtMeasuredBig.Text = "1.0"
        Me.txtMeasuredBig.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblHigh
        '
        Me.lblHigh.BackColor = System.Drawing.Color.White
        Me.lblHigh.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblHigh.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblHigh.Font = New System.Drawing.Font("Arial", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHigh.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblHigh.Location = New System.Drawing.Point(24, 24)
        Me.lblHigh.Name = "lblHigh"
        Me.lblHigh.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblHigh.Size = New System.Drawing.Size(225, 41)
        Me.lblHigh.TabIndex = 134
        Me.lblHigh.Text = "1.0"
        Me.lblHigh.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblLow
        '
        Me.lblLow.BackColor = System.Drawing.Color.White
        Me.lblLow.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblLow.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblLow.Font = New System.Drawing.Font("Arial", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLow.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLow.Location = New System.Drawing.Point(24, 144)
        Me.lblLow.Name = "lblLow"
        Me.lblLow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblLow.Size = New System.Drawing.Size(225, 41)
        Me.lblLow.TabIndex = 133
        Me.lblLow.Text = "1.0"
        Me.lblLow.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'cboITAPartsList
        '
        Me.cboITAPartsList.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.cboITAPartsList.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboITAPartsList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboITAPartsList.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboITAPartsList.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboITAPartsList.Location = New System.Drawing.Point(176, 328)
        Me.cboITAPartsList.Name = "cboITAPartsList"
        Me.cboITAPartsList.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboITAPartsList.Size = New System.Drawing.Size(225, 24)
        Me.cboITAPartsList.TabIndex = 125
        '
        'cboITAAssy
        '
        Me.cboITAAssy.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.cboITAAssy.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboITAAssy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboITAAssy.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboITAAssy.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboITAAssy.Location = New System.Drawing.Point(176, 224)
        Me.cboITAAssy.Name = "cboITAAssy"
        Me.cboITAAssy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboITAAssy.Size = New System.Drawing.Size(225, 24)
        Me.cboITAAssy.TabIndex = 124
        '
        'cboITAWiring
        '
        Me.cboITAWiring.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.cboITAWiring.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboITAWiring.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboITAWiring.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboITAWiring.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboITAWiring.Location = New System.Drawing.Point(176, 120)
        Me.cboITAWiring.Name = "cboITAWiring"
        Me.cboITAWiring.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboITAWiring.Size = New System.Drawing.Size(225, 24)
        Me.cboITAWiring.TabIndex = 123
        '
        'MenuOption_10
        '
        Me.MenuOption_10.AutoSize = True
        Me.MenuOption_10.Image = CType(resources.GetObject("MenuOption_10.Image"), System.Drawing.Image)
        Me.MenuOption_10.Location = New System.Drawing.Point(440, 72)
        Me.MenuOption_10.Name = "MenuOption_10"
        Me.MenuOption_10.Size = New System.Drawing.Size(73, 73)
        Me.MenuOption_10.TabIndex = 100
        '
        'MenuOption_11
        '
        Me.MenuOption_11.AutoSize = True
        Me.MenuOption_11.Image = CType(resources.GetObject("MenuOption_11.Image"), System.Drawing.Image)
        Me.MenuOption_11.Location = New System.Drawing.Point(440, 176)
        Me.MenuOption_11.Name = "MenuOption_11"
        Me.MenuOption_11.Size = New System.Drawing.Size(73, 73)
        Me.MenuOption_11.TabIndex = 101
        '
        'MenuOption_12
        '
        Me.MenuOption_12.AutoSize = True
        Me.MenuOption_12.Image = CType(resources.GetObject("MenuOption_12.Image"), System.Drawing.Image)
        Me.MenuOption_12.Location = New System.Drawing.Point(440, 280)
        Me.MenuOption_12.Name = "MenuOption_12"
        Me.MenuOption_12.Size = New System.Drawing.Size(73, 73)
        Me.MenuOption_12.TabIndex = 113
        '
        'MenuOptionText_12
        '
        Me.MenuOptionText_12.BackColor = System.Drawing.SystemColors.Window
        Me.MenuOptionText_12.Cursor = System.Windows.Forms.Cursors.Default
        Me.MenuOptionText_12.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MenuOptionText_12.ForeColor = System.Drawing.Color.Black
        Me.MenuOptionText_12.Location = New System.Drawing.Point(528, 288)
        Me.MenuOptionText_12.Name = "MenuOptionText_12"
        Me.MenuOptionText_12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.MenuOptionText_12.Size = New System.Drawing.Size(255, 73)
        Me.MenuOptionText_12.TabIndex = 114
        Me.MenuOptionText_12.Text = "General Information and Safety Precautions"
        Me.MenuOptionText_12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MenuOptionText_8
        '
        Me.MenuOptionText_8.BackColor = System.Drawing.SystemColors.Window
        Me.MenuOptionText_8.Cursor = System.Windows.Forms.Cursors.Default
        Me.MenuOptionText_8.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MenuOptionText_8.ForeColor = System.Drawing.Color.Black
        Me.MenuOptionText_8.Location = New System.Drawing.Point(176, 184)
        Me.MenuOptionText_8.Name = "MenuOptionText_8"
        Me.MenuOptionText_8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.MenuOptionText_8.Size = New System.Drawing.Size(225, 43)
        Me.MenuOptionText_8.TabIndex = 107
        Me.MenuOptionText_8.Text = "View ITA Assembly Drawing"
        Me.MenuOptionText_8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MenuOptionText_11
        '
        Me.MenuOptionText_11.BackColor = System.Drawing.SystemColors.Window
        Me.MenuOptionText_11.Cursor = System.Windows.Forms.Cursors.Default
        Me.MenuOptionText_11.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MenuOptionText_11.ForeColor = System.Drawing.Color.Black
        Me.MenuOptionText_11.Location = New System.Drawing.Point(528, 184)
        Me.MenuOptionText_11.Name = "MenuOptionText_11"
        Me.MenuOptionText_11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.MenuOptionText_11.Size = New System.Drawing.Size(225, 73)
        Me.MenuOptionText_11.TabIndex = 105
        Me.MenuOptionText_11.Text = "View English Language Test Description (ELTD)"
        Me.MenuOptionText_11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MenuOptionText_10
        '
        Me.MenuOptionText_10.BackColor = System.Drawing.SystemColors.Window
        Me.MenuOptionText_10.Cursor = System.Windows.Forms.Cursors.Default
        Me.MenuOptionText_10.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MenuOptionText_10.ForeColor = System.Drawing.Color.Black
        Me.MenuOptionText_10.Location = New System.Drawing.Point(528, 80)
        Me.MenuOptionText_10.Name = "MenuOptionText_10"
        Me.MenuOptionText_10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.MenuOptionText_10.Size = New System.Drawing.Size(225, 73)
        Me.MenuOptionText_10.TabIndex = 104
        Me.MenuOptionText_10.Text = "View Test Strategy Report (TSR)"
        Me.MenuOptionText_10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MenuOptionText_9
        '
        Me.MenuOptionText_9.BackColor = System.Drawing.SystemColors.Window
        Me.MenuOptionText_9.Cursor = System.Windows.Forms.Cursors.Default
        Me.MenuOptionText_9.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MenuOptionText_9.ForeColor = System.Drawing.Color.Black
        Me.MenuOptionText_9.Location = New System.Drawing.Point(176, 288)
        Me.MenuOptionText_9.Name = "MenuOptionText_9"
        Me.MenuOptionText_9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.MenuOptionText_9.Size = New System.Drawing.Size(255, 43)
        Me.MenuOptionText_9.TabIndex = 103
        Me.MenuOptionText_9.Text = "View ITA Parts List"
        Me.MenuOptionText_9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MenuOptionText_7
        '
        Me.MenuOptionText_7.BackColor = System.Drawing.SystemColors.Window
        Me.MenuOptionText_7.Cursor = System.Windows.Forms.Cursors.Default
        Me.MenuOptionText_7.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MenuOptionText_7.ForeColor = System.Drawing.Color.Black
        Me.MenuOptionText_7.Location = New System.Drawing.Point(176, 80)
        Me.MenuOptionText_7.Name = "MenuOptionText_7"
        Me.MenuOptionText_7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.MenuOptionText_7.Size = New System.Drawing.Size(225, 43)
        Me.MenuOptionText_7.TabIndex = 102
        Me.MenuOptionText_7.Text = "View ITA And Accessory Wiring Diagram"
        Me.MenuOptionText_7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Window
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 13.5!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Blue
        Me.Label1.Location = New System.Drawing.Point(240, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(277, 21)
        Me.Label1.TabIndex = 97
        Me.Label1.Text = "TEST DOCUMENTATION MENU"
        '
        'MenuOption_13
        '
        Me.MenuOption_13.AutoSize = True
        Me.MenuOption_13.BackColor = System.Drawing.Color.Silver
        Me.MenuOption_13.Image = CType(resources.GetObject("MenuOption_13.Image"), System.Drawing.Image)
        Me.MenuOption_13.Location = New System.Drawing.Point(432, 288)
        Me.MenuOption_13.Name = "MenuOption_13"
        Me.MenuOption_13.Size = New System.Drawing.Size(73, 73)
        Me.MenuOption_13.TabIndex = 120
        Me.MenuOption_13.UseVisualStyleBackColor = False
        '
        'MenuOption_14
        '
        Me.MenuOption_14.AutoSize = True
        Me.MenuOption_14.BackColor = System.Drawing.Color.Silver
        Me.MenuOption_14.Image = CType(resources.GetObject("MenuOption_14.Image"), System.Drawing.Image)
        Me.MenuOption_14.Location = New System.Drawing.Point(432, 392)
        Me.MenuOption_14.Name = "MenuOption_14"
        Me.MenuOption_14.Size = New System.Drawing.Size(73, 73)
        Me.MenuOption_14.TabIndex = 129
        Me.MenuOption_14.UseVisualStyleBackColor = False
        '
        'MenuOptionText_14
        '
        Me.MenuOptionText_14.BackColor = System.Drawing.SystemColors.Window
        Me.MenuOptionText_14.Cursor = System.Windows.Forms.Cursors.Default
        Me.MenuOptionText_14.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MenuOptionText_14.ForeColor = System.Drawing.Color.Black
        Me.MenuOptionText_14.Location = New System.Drawing.Point(520, 392)
        Me.MenuOptionText_14.Name = "MenuOptionText_14"
        Me.MenuOptionText_14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.MenuOptionText_14.Size = New System.Drawing.Size(225, 73)
        Me.MenuOptionText_14.TabIndex = 130
        Me.MenuOptionText_14.Text = "View Interactive Electronic Technical Manual (IETM)"
        Me.MenuOptionText_14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblAbout
        '
        Me.lblAbout.AutoSize = True
        Me.lblAbout.BackColor = System.Drawing.SystemColors.Window
        Me.lblAbout.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAbout.Font = New System.Drawing.Font("Arial", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAbout.ForeColor = System.Drawing.Color.Blue
        Me.lblAbout.Location = New System.Drawing.Point(296, 552)
        Me.lblAbout.Name = "lblAbout"
        Me.lblAbout.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAbout.Size = New System.Drawing.Size(140, 16)
        Me.lblAbout.TabIndex = 122
        Me.lblAbout.Text = "Program Information"
        '
        'MenuOptionText_13
        '
        Me.MenuOptionText_13.BackColor = System.Drawing.SystemColors.Window
        Me.MenuOptionText_13.Cursor = System.Windows.Forms.Cursors.Default
        Me.MenuOptionText_13.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MenuOptionText_13.ForeColor = System.Drawing.Color.Black
        Me.MenuOptionText_13.Location = New System.Drawing.Point(520, 288)
        Me.MenuOptionText_13.Name = "MenuOptionText_13"
        Me.MenuOptionText_13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.MenuOptionText_13.Size = New System.Drawing.Size(225, 73)
        Me.MenuOptionText_13.TabIndex = 121
        Me.MenuOptionText_13.Text = "View Fault-File"
        Me.MenuOptionText_13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MenuOptionText_6
        '
        Me.MenuOptionText_6.BackColor = System.Drawing.SystemColors.Window
        Me.MenuOptionText_6.Cursor = System.Windows.Forms.Cursors.Default
        Me.MenuOptionText_6.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MenuOptionText_6.ForeColor = System.Drawing.Color.Black
        Me.MenuOptionText_6.Location = New System.Drawing.Point(520, 184)
        Me.MenuOptionText_6.Name = "MenuOptionText_6"
        Me.MenuOptionText_6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.MenuOptionText_6.Size = New System.Drawing.Size(225, 73)
        Me.MenuOptionText_6.TabIndex = 40
        Me.MenuOptionText_6.Text = "View Test &Documentation"
        Me.MenuOptionText_6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MenuOptionText_5
        '
        Me.MenuOptionText_5.BackColor = System.Drawing.SystemColors.Window
        Me.MenuOptionText_5.Cursor = System.Windows.Forms.Cursors.Default
        Me.MenuOptionText_5.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MenuOptionText_5.ForeColor = System.Drawing.Color.Black
        Me.MenuOptionText_5.Location = New System.Drawing.Point(520, 80)
        Me.MenuOptionText_5.Name = "MenuOptionText_5"
        Me.MenuOptionText_5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.MenuOptionText_5.Size = New System.Drawing.Size(255, 73)
        Me.MenuOptionText_5.TabIndex = 37
        Me.MenuOptionText_5.Text = "Run &ITA Survey"
        Me.MenuOptionText_5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MenuOptionText_4
        '
        Me.MenuOptionText_4.BackColor = System.Drawing.SystemColors.Window
        Me.MenuOptionText_4.Cursor = System.Windows.Forms.Cursors.Default
        Me.MenuOptionText_4.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MenuOptionText_4.ForeColor = System.Drawing.Color.Black
        Me.MenuOptionText_4.Location = New System.Drawing.Point(168, 392)
        Me.MenuOptionText_4.Name = "MenuOptionText_4"
        Me.MenuOptionText_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.MenuOptionText_4.Size = New System.Drawing.Size(225, 43)
        Me.MenuOptionText_4.TabIndex = 13
        Me.MenuOptionText_4.Text = "View UUT Parts &List"
        Me.MenuOptionText_4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MenuOptionText_3
        '
        Me.MenuOptionText_3.BackColor = System.Drawing.SystemColors.Window
        Me.MenuOptionText_3.Cursor = System.Windows.Forms.Cursors.Default
        Me.MenuOptionText_3.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MenuOptionText_3.ForeColor = System.Drawing.Color.Black
        Me.MenuOptionText_3.Location = New System.Drawing.Point(168, 288)
        Me.MenuOptionText_3.MinimumSize = New System.Drawing.Size(225, 43)
        Me.MenuOptionText_3.Name = "MenuOptionText_3"
        Me.MenuOptionText_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.MenuOptionText_3.Size = New System.Drawing.Size(225, 43)
        Me.MenuOptionText_3.TabIndex = 12
        Me.MenuOptionText_3.Text = "View UUT Assembly &Diagram"
        '
        'MenuOptionText_2
        '
        Me.MenuOptionText_2.BackColor = System.Drawing.SystemColors.Window
        Me.MenuOptionText_2.Cursor = System.Windows.Forms.Cursors.Default
        Me.MenuOptionText_2.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MenuOptionText_2.ForeColor = System.Drawing.Color.Black
        Me.MenuOptionText_2.Location = New System.Drawing.Point(170, 184)
        Me.MenuOptionText_2.Name = "MenuOptionText_2"
        Me.MenuOptionText_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.MenuOptionText_2.Size = New System.Drawing.Size(225, 43)
        Me.MenuOptionText_2.TabIndex = 11
        Me.MenuOptionText_2.Text = "View UUT &Schematic"
        Me.MenuOptionText_2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MenuOptionText_1
        '
        Me.MenuOptionText_1.BackColor = System.Drawing.SystemColors.Window
        Me.MenuOptionText_1.Cursor = System.Windows.Forms.Cursors.Default
        Me.MenuOptionText_1.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MenuOptionText_1.ForeColor = System.Drawing.Color.Black
        Me.MenuOptionText_1.Location = New System.Drawing.Point(168, 80)
        Me.MenuOptionText_1.Name = "MenuOptionText_1"
        Me.MenuOptionText_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.MenuOptionText_1.Size = New System.Drawing.Size(225, 73)
        Me.MenuOptionText_1.TabIndex = 10
        Me.MenuOptionText_1.Text = "&Run Test Program"
        Me.MenuOptionText_1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Window
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 13.5!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Blue
        Me.Label3.Location = New System.Drawing.Point(288, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(157, 21)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "M A I N    M E N U"
        '
        'PictureWindow
        '
        Me.PictureWindow.BackColor = System.Drawing.SystemColors.Window
        Me.PictureWindow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureWindow.Controls.Add(Me.Message)
        Me.PictureWindow.Controls.Add(Me.PictureWindow_ShapeContainer)
        Me.PictureWindow.Cursor = System.Windows.Forms.Cursors.Default
        Me.PictureWindow.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PictureWindow.ForeColor = System.Drawing.SystemColors.WindowText
        Me.PictureWindow.Location = New System.Drawing.Point(624, 9)
        Me.PictureWindow.Name = "PictureWindow"
        Me.PictureWindow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.PictureWindow.Size = New System.Drawing.Size(80, 581)
        Me.PictureWindow.TabIndex = 5
        Me.PictureWindow.TabStop = True
        '
        'Message
        '
        Me.Message.AutoSize = True
        Me.Message.BackColor = System.Drawing.Color.Red
        Me.Message.Cursor = System.Windows.Forms.Cursors.Default
        Me.Message.ForeColor = System.Drawing.Color.White
        Me.Message.Location = New System.Drawing.Point(72, 16)
        Me.Message.Name = "Message"
        Me.Message.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Message.Size = New System.Drawing.Size(0, 16)
        Me.Message.TabIndex = 93
        Me.Message.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.Message.Visible = False
        '
        'PictureWindow_ShapeContainer
        '
        Me.PictureWindow_ShapeContainer.Location = New System.Drawing.Point(0, 0)
        Me.PictureWindow_ShapeContainer.Margin = New System.Windows.Forms.Padding(0)
        Me.PictureWindow_ShapeContainer.Name = "PictureWindow_ShapeContainer"
        Me.PictureWindow_ShapeContainer.Shapes.AddRange(New Microsoft.VisualBasic.PowerPacks.Shape() {Me.PictureWindow_Line})
        Me.PictureWindow_ShapeContainer.Size = New System.Drawing.Size(78, 579)
        Me.PictureWindow_ShapeContainer.TabIndex = 94
        Me.PictureWindow_ShapeContainer.TabStop = False
        '
        'PictureWindow_Line
        '
        Me.PictureWindow_Line.BorderColor = System.Drawing.Color.Black
        Me.PictureWindow_Line.Name = "PictureWindow_Line"
        Me.PictureWindow_Line.Visible = False
        Me.PictureWindow_Line.X1 = 35
        Me.PictureWindow_Line.X2 = 33
        Me.PictureWindow_Line.Y1 = 47
        Me.PictureWindow_Line.Y2 = 101
        '
        'ModuleMenu
        '
        Me.ModuleMenu.BackColor = System.Drawing.SystemColors.Window
        Me.ModuleMenu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ModuleMenu.Controls.Add(Me.ModuleInner)
        Me.ModuleMenu.Controls.Add(Me.lblModuleStatusTitle_3)
        Me.ModuleMenu.Controls.Add(Me.lblStatusTitle)
        Me.ModuleMenu.Controls.Add(Me.lblModuleStatusTitle_2)
        Me.ModuleMenu.Controls.Add(Me.lblModuleStatusTitle_1)
        Me.ModuleMenu.Cursor = System.Windows.Forms.Cursors.Default
        Me.ModuleMenu.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ModuleMenu.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ModuleMenu.Location = New System.Drawing.Point(520, 8)
        Me.ModuleMenu.Name = "ModuleMenu"
        Me.ModuleMenu.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ModuleMenu.Size = New System.Drawing.Size(97, 577)
        Me.ModuleMenu.TabIndex = 16
        Me.ModuleMenu.TabStop = True
        Me.ModuleMenu.Visible = False
        '
        'ModuleInner
        '
        Me.ModuleInner.AutoScroll = True
        Me.ModuleInner.AutoScrollMinSize = New System.Drawing.Size(650, 575)
        Me.ModuleInner.BackColor = System.Drawing.Color.White
        Me.ModuleInner.Controls.Add(Me.cmdPwrOnModule)
        Me.ModuleInner.Controls.Add(Me.cmdModule_1)
        Me.ModuleInner.Controls.Add(Me.cmdSTTOModule)
        Me.ModuleInner.Controls.Add(Me.cmdEndToEnd)
        Me.ModuleInner.Controls.Add(Me.lblModuleRunTime_1)
        Me.ModuleInner.Controls.Add(Me.lblPowerOnRunTime)
        Me.ModuleInner.Controls.Add(Me.lblSTTORunTime)
        Me.ModuleInner.Controls.Add(Me.lblEndToEndRunTime)
        Me.ModuleInner.Controls.Add(Me.lblModuleName_1)
        Me.ModuleInner.Controls.Add(Me.lblModuleStatus_1)
        Me.ModuleInner.Controls.Add(Me.lblPwrOnStatus)
        Me.ModuleInner.Controls.Add(Me.lblPwrOn)
        Me.ModuleInner.Controls.Add(Me.lblSTTOStatus)
        Me.ModuleInner.Controls.Add(Me.lblSTTO)
        Me.ModuleInner.Controls.Add(Me.lblEndToEndStatus)
        Me.ModuleInner.Controls.Add(Me.lblEndToEnd)
        Me.ModuleInner.Cursor = System.Windows.Forms.Cursors.Default
        Me.ModuleInner.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ModuleInner.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ModuleInner.Location = New System.Drawing.Point(336, 138)
        Me.ModuleInner.Name = "ModuleInner"
        Me.ModuleInner.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ModuleInner.Size = New System.Drawing.Size(673, 571)
        Me.ModuleInner.TabIndex = 24
        '
        'lblModuleRunTime_1
        '
        Me.lblModuleRunTime_1.BackColor = System.Drawing.Color.White
        Me.lblModuleRunTime_1.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblModuleRunTime_1.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblModuleRunTime_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblModuleRunTime_1.Location = New System.Drawing.Point(472, 168)
        Me.lblModuleRunTime_1.Name = "lblModuleRunTime_1"
        Me.lblModuleRunTime_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblModuleRunTime_1.Size = New System.Drawing.Size(97, 25)
        Me.lblModuleRunTime_1.TabIndex = 119
        Me.lblModuleRunTime_1.Text = "RunTime"
        '
        'lblPowerOnRunTime
        '
        Me.lblPowerOnRunTime.BackColor = System.Drawing.Color.White
        Me.lblPowerOnRunTime.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPowerOnRunTime.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPowerOnRunTime.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPowerOnRunTime.Location = New System.Drawing.Point(472, 120)
        Me.lblPowerOnRunTime.Name = "lblPowerOnRunTime"
        Me.lblPowerOnRunTime.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPowerOnRunTime.Size = New System.Drawing.Size(97, 25)
        Me.lblPowerOnRunTime.TabIndex = 118
        Me.lblPowerOnRunTime.Text = "RunTime"
        '
        'lblSTTORunTime
        '
        Me.lblSTTORunTime.BackColor = System.Drawing.Color.White
        Me.lblSTTORunTime.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSTTORunTime.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSTTORunTime.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSTTORunTime.Location = New System.Drawing.Point(472, 72)
        Me.lblSTTORunTime.Name = "lblSTTORunTime"
        Me.lblSTTORunTime.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSTTORunTime.Size = New System.Drawing.Size(97, 25)
        Me.lblSTTORunTime.TabIndex = 117
        Me.lblSTTORunTime.Text = "RunTime"
        '
        'lblEndToEndRunTime
        '
        Me.lblEndToEndRunTime.BackColor = System.Drawing.Color.White
        Me.lblEndToEndRunTime.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblEndToEndRunTime.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEndToEndRunTime.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblEndToEndRunTime.Location = New System.Drawing.Point(472, 32)
        Me.lblEndToEndRunTime.Name = "lblEndToEndRunTime"
        Me.lblEndToEndRunTime.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblEndToEndRunTime.Size = New System.Drawing.Size(97, 25)
        Me.lblEndToEndRunTime.TabIndex = 116
        Me.lblEndToEndRunTime.Text = "RunTime"
        '
        'lblModuleName_1
        '
        Me.lblModuleName_1.BackColor = System.Drawing.Color.White
        Me.lblModuleName_1.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblModuleName_1.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblModuleName_1.ForeColor = System.Drawing.Color.Black
        Me.lblModuleName_1.Location = New System.Drawing.Point(240, 168)
        Me.lblModuleName_1.Name = "lblModuleName_1"
        Me.lblModuleName_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblModuleName_1.Size = New System.Drawing.Size(209, 25)
        Me.lblModuleName_1.TabIndex = 36
        Me.lblModuleName_1.Text = "Module 1 Test"
        '
        'lblModuleStatus_1
        '
        Me.lblModuleStatus_1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblModuleStatus_1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblModuleStatus_1.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblModuleStatus_1.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblModuleStatus_1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblModuleStatus_1.Location = New System.Drawing.Point(24, 168)
        Me.lblModuleStatus_1.Name = "lblModuleStatus_1"
        Me.lblModuleStatus_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblModuleStatus_1.Size = New System.Drawing.Size(81, 25)
        Me.lblModuleStatus_1.TabIndex = 35
        Me.lblModuleStatus_1.Text = "Unknown"
        Me.lblModuleStatus_1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblPwrOnStatus
        '
        Me.lblPwrOnStatus.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblPwrOnStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPwrOnStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPwrOnStatus.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPwrOnStatus.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblPwrOnStatus.Location = New System.Drawing.Point(24, 120)
        Me.lblPwrOnStatus.Name = "lblPwrOnStatus"
        Me.lblPwrOnStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPwrOnStatus.Size = New System.Drawing.Size(81, 25)
        Me.lblPwrOnStatus.TabIndex = 34
        Me.lblPwrOnStatus.Text = "Unknown"
        Me.lblPwrOnStatus.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblPwrOn
        '
        Me.lblPwrOn.BackColor = System.Drawing.Color.White
        Me.lblPwrOn.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPwrOn.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPwrOn.ForeColor = System.Drawing.Color.Black
        Me.lblPwrOn.Location = New System.Drawing.Point(240, 120)
        Me.lblPwrOn.Name = "lblPwrOn"
        Me.lblPwrOn.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPwrOn.Size = New System.Drawing.Size(209, 25)
        Me.lblPwrOn.TabIndex = 33
        Me.lblPwrOn.Text = "Power On Test"
        '
        'lblSTTOStatus
        '
        Me.lblSTTOStatus.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblSTTOStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSTTOStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSTTOStatus.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSTTOStatus.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblSTTOStatus.Location = New System.Drawing.Point(24, 72)
        Me.lblSTTOStatus.Name = "lblSTTOStatus"
        Me.lblSTTOStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSTTOStatus.Size = New System.Drawing.Size(81, 25)
        Me.lblSTTOStatus.TabIndex = 32
        Me.lblSTTOStatus.Text = "Unknown"
        Me.lblSTTOStatus.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblSTTO
        '
        Me.lblSTTO.BackColor = System.Drawing.Color.White
        Me.lblSTTO.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSTTO.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSTTO.ForeColor = System.Drawing.Color.Black
        Me.lblSTTO.Location = New System.Drawing.Point(240, 72)
        Me.lblSTTO.Name = "lblSTTO"
        Me.lblSTTO.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSTTO.Size = New System.Drawing.Size(217, 25)
        Me.lblSTTO.TabIndex = 31
        Me.lblSTTO.Text = "Safe-To-Turn-On Test"
        '
        'lblEndToEndStatus
        '
        Me.lblEndToEndStatus.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblEndToEndStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblEndToEndStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblEndToEndStatus.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEndToEndStatus.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblEndToEndStatus.Location = New System.Drawing.Point(24, 24)
        Me.lblEndToEndStatus.Name = "lblEndToEndStatus"
        Me.lblEndToEndStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblEndToEndStatus.Size = New System.Drawing.Size(81, 25)
        Me.lblEndToEndStatus.TabIndex = 30
        Me.lblEndToEndStatus.Text = "Unknown"
        Me.lblEndToEndStatus.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblEndToEnd
        '
        Me.lblEndToEnd.BackColor = System.Drawing.Color.White
        Me.lblEndToEnd.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblEndToEnd.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEndToEnd.ForeColor = System.Drawing.Color.Black
        Me.lblEndToEnd.Location = New System.Drawing.Point(240, 32)
        Me.lblEndToEnd.Name = "lblEndToEnd"
        Me.lblEndToEnd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblEndToEnd.Size = New System.Drawing.Size(217, 25)
        Me.lblEndToEnd.TabIndex = 29
        Me.lblEndToEnd.Text = "End-To-End Test"
        '
        'lblModuleStatusTitle_3
        '
        Me.lblModuleStatusTitle_3.BackColor = System.Drawing.Color.Yellow
        Me.lblModuleStatusTitle_3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblModuleStatusTitle_3.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblModuleStatusTitle_3.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblModuleStatusTitle_3.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblModuleStatusTitle_3.Location = New System.Drawing.Point(456, 0)
        Me.lblModuleStatusTitle_3.Name = "lblModuleStatusTitle_3"
        Me.lblModuleStatusTitle_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblModuleStatusTitle_3.Size = New System.Drawing.Size(129, 25)
        Me.lblModuleStatusTitle_3.TabIndex = 115
        Me.lblModuleStatusTitle_3.Text = "Run Time"
        Me.lblModuleStatusTitle_3.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblStatusTitle
        '
        Me.lblStatusTitle.BackColor = System.Drawing.Color.Yellow
        Me.lblStatusTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblStatusTitle.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblStatusTitle.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatusTitle.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblStatusTitle.Location = New System.Drawing.Point(0, 0)
        Me.lblStatusTitle.Name = "lblStatusTitle"
        Me.lblStatusTitle.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblStatusTitle.Size = New System.Drawing.Size(129, 25)
        Me.lblStatusTitle.TabIndex = 95
        Me.lblStatusTitle.Text = "Status"
        Me.lblStatusTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblModuleStatusTitle_2
        '
        Me.lblModuleStatusTitle_2.BackColor = System.Drawing.Color.Yellow
        Me.lblModuleStatusTitle_2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblModuleStatusTitle_2.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblModuleStatusTitle_2.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblModuleStatusTitle_2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblModuleStatusTitle_2.Location = New System.Drawing.Point(232, 0)
        Me.lblModuleStatusTitle_2.Name = "lblModuleStatusTitle_2"
        Me.lblModuleStatusTitle_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblModuleStatusTitle_2.Size = New System.Drawing.Size(353, 25)
        Me.lblModuleStatusTitle_2.TabIndex = 57
        Me.lblModuleStatusTitle_2.Text = " Name"
        '
        'lblModuleStatusTitle_1
        '
        Me.lblModuleStatusTitle_1.BackColor = System.Drawing.Color.Yellow
        Me.lblModuleStatusTitle_1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblModuleStatusTitle_1.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblModuleStatusTitle_1.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblModuleStatusTitle_1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblModuleStatusTitle_1.Location = New System.Drawing.Point(128, 0)
        Me.lblModuleStatusTitle_1.Name = "lblModuleStatusTitle_1"
        Me.lblModuleStatusTitle_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblModuleStatusTitle_1.Size = New System.Drawing.Size(105, 25)
        Me.lblModuleStatusTitle_1.TabIndex = 22
        Me.lblModuleStatusTitle_1.Text = "Execute"
        Me.lblModuleStatusTitle_1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'SeqTextWindow
        '
        Me.SeqTextWindow.BackColor = System.Drawing.SystemColors.Window
        Me.SeqTextWindow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.SeqTextWindow.Controls.Add(Me.SeqTextWindowLabel)
        Me.SeqTextWindow.Controls.Add(Me.lblTestResults)
        Me.SeqTextWindow.Cursor = System.Windows.Forms.Cursors.Default
        Me.SeqTextWindow.Font = New System.Drawing.Font("Courier New", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SeqTextWindow.ForeColor = System.Drawing.SystemColors.WindowText
        Me.SeqTextWindow.Location = New System.Drawing.Point(62, 25)
        Me.SeqTextWindow.Name = "SeqTextWindow"
        Me.SeqTextWindow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.SeqTextWindow.Size = New System.Drawing.Size(428, 441)
        Me.SeqTextWindow.TabIndex = 7
        Me.SeqTextWindow.TabStop = True
        Me.SeqTextWindow.Visible = False
        '
        'SeqTextWindowLabel
        '
        Me.SeqTextWindowLabel.Location = New System.Drawing.Point(8, 25)
        Me.SeqTextWindowLabel.Name = "SeqTextWindowLabel"
        Me.SeqTextWindowLabel.Size = New System.Drawing.Size(386, 303)
        Me.SeqTextWindowLabel.TabIndex = 16
        Me.SeqTextWindowLabel.Text = "Label2"
        '
        'lblTestResults
        '
        Me.lblTestResults.BackColor = System.Drawing.Color.Yellow
        Me.lblTestResults.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTestResults.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTestResults.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTestResults.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblTestResults.Location = New System.Drawing.Point(16, 16)
        Me.lblTestResults.Name = "lblTestResults"
        Me.lblTestResults.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTestResults.Size = New System.Drawing.Size(249, 33)
        Me.lblTestResults.TabIndex = 15
        Me.lblTestResults.Text = "TEST RESULTS"
        Me.lblTestResults.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'cmdDiagnostics
        '
        Me.cmdDiagnostics.Location = New System.Drawing.Point(640, 361)
        Me.cmdDiagnostics.Name = "cmdDiagnostics"
        Me.cmdDiagnostics.Size = New System.Drawing.Size(188, 29)
        Me.cmdDiagnostics.TabIndex = 110
        Me.cmdDiagnostics.Text = "Perform Diagnostics"
        Me.cmdDiagnostics.Visible = False
        '
        'cmdFHDB
        '
        Me.cmdFHDB.Enabled = False
        Me.cmdFHDB.Location = New System.Drawing.Point(640, 329)
        Me.cmdFHDB.Name = "cmdFHDB"
        Me.cmdFHDB.Size = New System.Drawing.Size(188, 29)
        Me.cmdFHDB.TabIndex = 94
        Me.cmdFHDB.Text = "&Save Failure Data"
        Me.cmdFHDB.Visible = False
        '
        'picDanger
        '
        Me.picDanger.BackColor = System.Drawing.SystemColors.Window
        Me.picDanger.Cursor = System.Windows.Forms.Cursors.Default
        Me.picDanger.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.picDanger.ForeColor = System.Drawing.SystemColors.WindowText
        Me.picDanger.Image = CType(resources.GetObject("picDanger.Image"), System.Drawing.Image)
        Me.picDanger.Location = New System.Drawing.Point(672, 505)
        Me.picDanger.Name = "picDanger"
        Me.picDanger.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.picDanger.Size = New System.Drawing.Size(127, 57)
        Me.picDanger.TabIndex = 85
        Me.picDanger.TabStop = False
        '
        'fraInstructions
        '
        Me.fraInstructions.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.fraInstructions.Controls.Add(Me.picInstructions)
        Me.fraInstructions.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraInstructions.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraInstructions.Location = New System.Drawing.Point(640, 321)
        Me.fraInstructions.Name = "fraInstructions"
        Me.fraInstructions.Padding = New System.Windows.Forms.Padding(0)
        Me.fraInstructions.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraInstructions.Size = New System.Drawing.Size(185, 177)
        Me.fraInstructions.TabIndex = 81
        Me.fraInstructions.TabStop = False
        Me.fraInstructions.Text = "Instructions"
        Me.fraInstructions.Visible = False
        '
        'picInstructions
        '
        Me.picInstructions.BackColor = System.Drawing.SystemColors.Window
        Me.picInstructions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picInstructions.Controls.Add(Me.lblPressContinue)
        Me.picInstructions.Controls.Add(Me.lblInstruction_6)
        Me.picInstructions.Controls.Add(Me.lblInstruction_5)
        Me.picInstructions.Controls.Add(Me.lblInstruction_4)
        Me.picInstructions.Controls.Add(Me.lblInstruction_3)
        Me.picInstructions.Controls.Add(Me.lblInstruction_2)
        Me.picInstructions.Controls.Add(Me.lblInstruction_1)
        Me.picInstructions.Controls.Add(Me.lblInstructionType)
        Me.picInstructions.Cursor = System.Windows.Forms.Cursors.Default
        Me.picInstructions.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.picInstructions.ForeColor = System.Drawing.SystemColors.WindowText
        Me.picInstructions.Location = New System.Drawing.Point(8, 16)
        Me.picInstructions.Name = "picInstructions"
        Me.picInstructions.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.picInstructions.Size = New System.Drawing.Size(169, 153)
        Me.picInstructions.TabIndex = 82
        Me.picInstructions.TabStop = True
        '
        'lblPressContinue
        '
        Me.lblPressContinue.BackColor = System.Drawing.Color.White
        Me.lblPressContinue.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPressContinue.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPressContinue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblPressContinue.Location = New System.Drawing.Point(0, 120)
        Me.lblPressContinue.Name = "lblPressContinue"
        Me.lblPressContinue.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPressContinue.Size = New System.Drawing.Size(169, 25)
        Me.lblPressContinue.TabIndex = 92
        Me.lblPressContinue.Text = "PRESS CONTINUE AND/OR PROBE BUTTON"
        Me.lblPressContinue.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblInstruction_6
        '
        Me.lblInstruction_6.BackColor = System.Drawing.Color.White
        Me.lblInstruction_6.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblInstruction_6.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInstruction_6.ForeColor = System.Drawing.Color.Black
        Me.lblInstruction_6.Location = New System.Drawing.Point(0, 96)
        Me.lblInstruction_6.Name = "lblInstruction_6"
        Me.lblInstruction_6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblInstruction_6.Size = New System.Drawing.Size(169, 17)
        Me.lblInstruction_6.TabIndex = 91
        Me.lblInstruction_6.Text = "Test Data"
        '
        'lblInstruction_5
        '
        Me.lblInstruction_5.BackColor = System.Drawing.Color.White
        Me.lblInstruction_5.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblInstruction_5.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInstruction_5.ForeColor = System.Drawing.Color.Black
        Me.lblInstruction_5.Location = New System.Drawing.Point(0, 80)
        Me.lblInstruction_5.Name = "lblInstruction_5"
        Me.lblInstruction_5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblInstruction_5.Size = New System.Drawing.Size(169, 17)
        Me.lblInstruction_5.TabIndex = 90
        Me.lblInstruction_5.Text = "Test Data"
        '
        'lblInstruction_4
        '
        Me.lblInstruction_4.BackColor = System.Drawing.Color.White
        Me.lblInstruction_4.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblInstruction_4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInstruction_4.ForeColor = System.Drawing.Color.Black
        Me.lblInstruction_4.Location = New System.Drawing.Point(0, 64)
        Me.lblInstruction_4.Name = "lblInstruction_4"
        Me.lblInstruction_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblInstruction_4.Size = New System.Drawing.Size(169, 17)
        Me.lblInstruction_4.TabIndex = 89
        Me.lblInstruction_4.Text = "Test Data"
        '
        'lblInstruction_3
        '
        Me.lblInstruction_3.BackColor = System.Drawing.Color.White
        Me.lblInstruction_3.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblInstruction_3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInstruction_3.ForeColor = System.Drawing.Color.Black
        Me.lblInstruction_3.Location = New System.Drawing.Point(0, 48)
        Me.lblInstruction_3.Name = "lblInstruction_3"
        Me.lblInstruction_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblInstruction_3.Size = New System.Drawing.Size(169, 17)
        Me.lblInstruction_3.TabIndex = 88
        Me.lblInstruction_3.Text = "Test Data"
        '
        'lblInstruction_2
        '
        Me.lblInstruction_2.BackColor = System.Drawing.Color.White
        Me.lblInstruction_2.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblInstruction_2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInstruction_2.ForeColor = System.Drawing.Color.Black
        Me.lblInstruction_2.Location = New System.Drawing.Point(0, 32)
        Me.lblInstruction_2.Name = "lblInstruction_2"
        Me.lblInstruction_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblInstruction_2.Size = New System.Drawing.Size(169, 17)
        Me.lblInstruction_2.TabIndex = 87
        Me.lblInstruction_2.Text = "Test Data"
        '
        'lblInstruction_1
        '
        Me.lblInstruction_1.BackColor = System.Drawing.Color.White
        Me.lblInstruction_1.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblInstruction_1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInstruction_1.ForeColor = System.Drawing.Color.Black
        Me.lblInstruction_1.Location = New System.Drawing.Point(0, 16)
        Me.lblInstruction_1.Name = "lblInstruction_1"
        Me.lblInstruction_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblInstruction_1.Size = New System.Drawing.Size(169, 17)
        Me.lblInstruction_1.TabIndex = 84
        Me.lblInstruction_1.Text = "Test Data"
        '
        'lblInstructionType
        '
        Me.lblInstructionType.BackColor = System.Drawing.Color.White
        Me.lblInstructionType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblInstructionType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInstructionType.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblInstructionType.Location = New System.Drawing.Point(0, 0)
        Me.lblInstructionType.Name = "lblInstructionType"
        Me.lblInstructionType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblInstructionType.Size = New System.Drawing.Size(169, 17)
        Me.lblInstructionType.TabIndex = 83
        Me.lblInstructionType.Text = "*** OPERATOR ACTION***"
        Me.lblInstructionType.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'fraInstrument
        '
        Me.fraInstrument.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.fraInstrument.Controls.Add(Me.txtCommand)
        Me.fraInstrument.Controls.Add(Me.txtInstrument)
        Me.fraInstrument.Controls.Add(Me.lblCommand)
        Me.fraInstrument.Controls.Add(Me.lblInstrument)
        Me.fraInstrument.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraInstrument.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraInstrument.Location = New System.Drawing.Point(640, 113)
        Me.fraInstrument.Name = "fraInstrument"
        Me.fraInstrument.Padding = New System.Windows.Forms.Padding(0)
        Me.fraInstrument.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraInstrument.Size = New System.Drawing.Size(185, 73)
        Me.fraInstrument.TabIndex = 76
        Me.fraInstrument.TabStop = False
        Me.fraInstrument.Text = "Instrument"
        Me.fraInstrument.Visible = False
        '
        'txtCommand
        '
        Me.txtCommand.AcceptsReturn = True
        Me.txtCommand.BackColor = System.Drawing.SystemColors.Window
        Me.txtCommand.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCommand.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCommand.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.txtCommand.Location = New System.Drawing.Point(56, 40)
        Me.txtCommand.MaxLength = 0
        Me.txtCommand.Name = "txtCommand"
        Me.txtCommand.ReadOnly = True
        Me.txtCommand.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCommand.Size = New System.Drawing.Size(121, 22)
        Me.txtCommand.TabIndex = 78
        Me.txtCommand.TabStop = False
        '
        'txtInstrument
        '
        Me.txtInstrument.AcceptsReturn = True
        Me.txtInstrument.BackColor = System.Drawing.SystemColors.Window
        Me.txtInstrument.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtInstrument.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInstrument.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.txtInstrument.Location = New System.Drawing.Point(56, 16)
        Me.txtInstrument.MaxLength = 0
        Me.txtInstrument.Name = "txtInstrument"
        Me.txtInstrument.ReadOnly = True
        Me.txtInstrument.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtInstrument.Size = New System.Drawing.Size(121, 22)
        Me.txtInstrument.TabIndex = 77
        Me.txtInstrument.TabStop = False
        '
        'lblCommand
        '
        Me.lblCommand.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblCommand.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCommand.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCommand.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCommand.Location = New System.Drawing.Point(8, 40)
        Me.lblCommand.Name = "lblCommand"
        Me.lblCommand.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCommand.Size = New System.Drawing.Size(41, 17)
        Me.lblCommand.TabIndex = 80
        Me.lblCommand.Text = "Cmd:"
        Me.lblCommand.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblInstrument
        '
        Me.lblInstrument.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblInstrument.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblInstrument.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInstrument.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblInstrument.Location = New System.Drawing.Point(8, 16)
        Me.lblInstrument.Name = "lblInstrument"
        Me.lblInstrument.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblInstrument.Size = New System.Drawing.Size(41, 17)
        Me.lblInstrument.TabIndex = 79
        Me.lblInstrument.Text = "Instr:"
        Me.lblInstrument.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'fraMeasurement
        '
        Me.fraMeasurement.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.fraMeasurement.Controls.Add(Me.txtUnit)
        Me.fraMeasurement.Controls.Add(Me.txtLowerLimit)
        Me.fraMeasurement.Controls.Add(Me.txtUpperLimit)
        Me.fraMeasurement.Controls.Add(Me.txtMeasured)
        Me.fraMeasurement.Controls.Add(Me.lblUnit)
        Me.fraMeasurement.Controls.Add(Me.lbLowerLimit)
        Me.fraMeasurement.Controls.Add(Me.lbMeasured)
        Me.fraMeasurement.Controls.Add(Me.lbUpperLimit)
        Me.fraMeasurement.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraMeasurement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraMeasurement.Location = New System.Drawing.Point(640, 193)
        Me.fraMeasurement.Name = "fraMeasurement"
        Me.fraMeasurement.Padding = New System.Windows.Forms.Padding(0)
        Me.fraMeasurement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraMeasurement.Size = New System.Drawing.Size(185, 121)
        Me.fraMeasurement.TabIndex = 46
        Me.fraMeasurement.TabStop = False
        Me.fraMeasurement.Text = "Measurement"
        Me.fraMeasurement.Visible = False
        '
        'txtUnit
        '
        Me.txtUnit.AcceptsReturn = True
        Me.txtUnit.BackColor = System.Drawing.SystemColors.Window
        Me.txtUnit.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtUnit.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUnit.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.txtUnit.Location = New System.Drawing.Point(56, 16)
        Me.txtUnit.MaxLength = 0
        Me.txtUnit.Name = "txtUnit"
        Me.txtUnit.ReadOnly = True
        Me.txtUnit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtUnit.Size = New System.Drawing.Size(121, 22)
        Me.txtUnit.TabIndex = 55
        Me.txtUnit.TabStop = False
        '
        'txtLowerLimit
        '
        Me.txtLowerLimit.AcceptsReturn = True
        Me.txtLowerLimit.BackColor = System.Drawing.SystemColors.Window
        Me.txtLowerLimit.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLowerLimit.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLowerLimit.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.txtLowerLimit.Location = New System.Drawing.Point(56, 88)
        Me.txtLowerLimit.MaxLength = 0
        Me.txtLowerLimit.Name = "txtLowerLimit"
        Me.txtLowerLimit.ReadOnly = True
        Me.txtLowerLimit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLowerLimit.Size = New System.Drawing.Size(121, 22)
        Me.txtLowerLimit.TabIndex = 51
        Me.txtLowerLimit.TabStop = False
        '
        'txtUpperLimit
        '
        Me.txtUpperLimit.AcceptsReturn = True
        Me.txtUpperLimit.BackColor = System.Drawing.SystemColors.Window
        Me.txtUpperLimit.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtUpperLimit.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUpperLimit.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.txtUpperLimit.Location = New System.Drawing.Point(56, 40)
        Me.txtUpperLimit.MaxLength = 0
        Me.txtUpperLimit.Name = "txtUpperLimit"
        Me.txtUpperLimit.ReadOnly = True
        Me.txtUpperLimit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtUpperLimit.Size = New System.Drawing.Size(121, 22)
        Me.txtUpperLimit.TabIndex = 48
        Me.txtUpperLimit.TabStop = False
        '
        'txtMeasured
        '
        Me.txtMeasured.AcceptsReturn = True
        Me.txtMeasured.BackColor = System.Drawing.Color.White
        Me.txtMeasured.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMeasured.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMeasured.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.txtMeasured.Location = New System.Drawing.Point(56, 64)
        Me.txtMeasured.MaxLength = 0
        Me.txtMeasured.Name = "txtMeasured"
        Me.txtMeasured.ReadOnly = True
        Me.txtMeasured.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMeasured.Size = New System.Drawing.Size(121, 22)
        Me.txtMeasured.TabIndex = 47
        Me.txtMeasured.TabStop = False
        '
        'lblUnit
        '
        Me.lblUnit.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblUnit.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblUnit.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUnit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnit.Location = New System.Drawing.Point(8, 16)
        Me.lblUnit.Name = "lblUnit"
        Me.lblUnit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblUnit.Size = New System.Drawing.Size(41, 17)
        Me.lblUnit.TabIndex = 56
        Me.lblUnit.Text = "Unit:"
        Me.lblUnit.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lbLowerLimit
        '
        Me.lbLowerLimit.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lbLowerLimit.Cursor = System.Windows.Forms.Cursors.Default
        Me.lbLowerLimit.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbLowerLimit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lbLowerLimit.Location = New System.Drawing.Point(8, 88)
        Me.lbLowerLimit.Name = "lbLowerLimit"
        Me.lbLowerLimit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lbLowerLimit.Size = New System.Drawing.Size(41, 17)
        Me.lbLowerLimit.TabIndex = 52
        Me.lbLowerLimit.Text = "LLmt:"
        Me.lbLowerLimit.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lbMeasured
        '
        Me.lbMeasured.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lbMeasured.Cursor = System.Windows.Forms.Cursors.Default
        Me.lbMeasured.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbMeasured.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lbMeasured.Location = New System.Drawing.Point(8, 64)
        Me.lbMeasured.Name = "lbMeasured"
        Me.lbMeasured.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lbMeasured.Size = New System.Drawing.Size(41, 17)
        Me.lbMeasured.TabIndex = 49
        Me.lbMeasured.Text = "Mea:"
        Me.lbMeasured.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lbUpperLimit
        '
        Me.lbUpperLimit.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lbUpperLimit.Cursor = System.Windows.Forms.Cursors.Default
        Me.lbUpperLimit.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbUpperLimit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lbUpperLimit.Location = New System.Drawing.Point(8, 40)
        Me.lbUpperLimit.Name = "lbUpperLimit"
        Me.lbUpperLimit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lbUpperLimit.Size = New System.Drawing.Size(41, 17)
        Me.lbUpperLimit.TabIndex = 50
        Me.lbUpperLimit.Text = "ULmt:"
        Me.lbUpperLimit.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'fraTestInformation
        '
        Me.fraTestInformation.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.fraTestInformation.Controls.Add(Me.txtModule)
        Me.fraTestInformation.Controls.Add(Me.txtTestName)
        Me.fraTestInformation.Controls.Add(Me.txtStep)
        Me.fraTestInformation.Controls.Add(Me.lblModule)
        Me.fraTestInformation.Controls.Add(Me.lblName)
        Me.fraTestInformation.Controls.Add(Me.lblStep)
        Me.fraTestInformation.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraTestInformation.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraTestInformation.Location = New System.Drawing.Point(640, 9)
        Me.fraTestInformation.Name = "fraTestInformation"
        Me.fraTestInformation.Padding = New System.Windows.Forms.Padding(0)
        Me.fraTestInformation.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraTestInformation.Size = New System.Drawing.Size(185, 97)
        Me.fraTestInformation.TabIndex = 41
        Me.fraTestInformation.TabStop = False
        Me.fraTestInformation.Text = "Test Information"
        Me.fraTestInformation.Visible = False
        '
        'txtModule
        '
        Me.txtModule.AcceptsReturn = True
        Me.txtModule.BackColor = System.Drawing.SystemColors.Window
        Me.txtModule.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtModule.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtModule.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.txtModule.Location = New System.Drawing.Point(56, 16)
        Me.txtModule.MaxLength = 0
        Me.txtModule.Name = "txtModule"
        Me.txtModule.ReadOnly = True
        Me.txtModule.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtModule.Size = New System.Drawing.Size(121, 22)
        Me.txtModule.TabIndex = 53
        Me.txtModule.TabStop = False
        Me.txtModule.Text = "test"
        '
        'txtTestName
        '
        Me.txtTestName.AcceptsReturn = True
        Me.txtTestName.BackColor = System.Drawing.SystemColors.Window
        Me.txtTestName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTestName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTestName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.txtTestName.Location = New System.Drawing.Point(56, 64)
        Me.txtTestName.MaxLength = 0
        Me.txtTestName.Name = "txtTestName"
        Me.txtTestName.ReadOnly = True
        Me.txtTestName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTestName.Size = New System.Drawing.Size(121, 22)
        Me.txtTestName.TabIndex = 45
        Me.txtTestName.TabStop = False
        '
        'txtStep
        '
        Me.txtStep.AcceptsReturn = True
        Me.txtStep.BackColor = System.Drawing.SystemColors.Window
        Me.txtStep.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtStep.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStep.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.txtStep.Location = New System.Drawing.Point(56, 40)
        Me.txtStep.MaxLength = 0
        Me.txtStep.Name = "txtStep"
        Me.txtStep.ReadOnly = True
        Me.txtStep.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtStep.Size = New System.Drawing.Size(121, 22)
        Me.txtStep.TabIndex = 42
        Me.txtStep.TabStop = False
        Me.txtStep.Text = "test"
        '
        'lblModule
        '
        Me.lblModule.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblModule.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblModule.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblModule.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblModule.Location = New System.Drawing.Point(8, 16)
        Me.lblModule.Name = "lblModule"
        Me.lblModule.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblModule.Size = New System.Drawing.Size(41, 17)
        Me.lblModule.TabIndex = 54
        Me.lblModule.Text = "Mod:"
        Me.lblModule.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblName
        '
        Me.lblName.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblName.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblName.Location = New System.Drawing.Point(8, 64)
        Me.lblName.Name = "lblName"
        Me.lblName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblName.Size = New System.Drawing.Size(41, 17)
        Me.lblName.TabIndex = 44
        Me.lblName.Text = "Name:"
        Me.lblName.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblStep
        '
        Me.lblStep.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblStep.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblStep.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStep.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblStep.Location = New System.Drawing.Point(8, 40)
        Me.lblStep.Name = "lblStep"
        Me.lblStep.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblStep.Size = New System.Drawing.Size(41, 17)
        Me.lblStep.TabIndex = 43
        Me.lblStep.Text = "Step:"
        Me.lblStep.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'TextWindow
        '
        Me.TextWindow.AcceptsReturn = True
        Me.TextWindow.BackColor = System.Drawing.SystemColors.Window
        Me.TextWindow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextWindow.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TextWindow.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextWindow.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TextWindow.Location = New System.Drawing.Point(368, 192)
        Me.TextWindow.MaxLength = 0
        Me.TextWindow.Multiline = True
        Me.TextWindow.Name = "TextWindow"
        Me.TextWindow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TextWindow.Size = New System.Drawing.Size(111, 132)
        Me.TextWindow.TabIndex = 6
        Me.TextWindow.Visible = False
        '
        'lblPowerApplied
        '
        Me.lblPowerApplied.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblPowerApplied.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPowerApplied.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPowerApplied.ForeColor = System.Drawing.Color.Red
        Me.lblPowerApplied.Location = New System.Drawing.Point(648, 561)
        Me.lblPowerApplied.Name = "lblPowerApplied"
        Me.lblPowerApplied.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPowerApplied.Size = New System.Drawing.Size(177, 17)
        Me.lblPowerApplied.TabIndex = 86
        Me.lblPowerApplied.Text = "POWER APPLIED"
        Me.lblPowerApplied.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'SSPanel1
        '
        Me.SSPanel1.Controls.Add(Me.msgInBox)
        Me.SSPanel1.Location = New System.Drawing.Point(8, 8)
        Me.SSPanel1.Name = "SSPanel1"
        Me.SSPanel1.Size = New System.Drawing.Size(793, 161)
        Me.SSPanel1.TabIndex = 65
        '
        'msgInBox
        '
        Me.msgInBox.AutoScroll = True
        Me.msgInBox.AutoScrollMinSize = New System.Drawing.Size(715, 87)
        Me.msgInBox.BackColor = System.Drawing.SystemColors.Window
        Me.msgInBox.Controls.Add(Me.lblMsg_1)
        Me.msgInBox.Cursor = System.Windows.Forms.Cursors.Default
        Me.msgInBox.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.msgInBox.ForeColor = System.Drawing.SystemColors.ControlText
        Me.msgInBox.Location = New System.Drawing.Point(0, 0)
        Me.msgInBox.Name = "msgInBox"
        Me.msgInBox.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.msgInBox.Size = New System.Drawing.Size(745, 97)
        Me.msgInBox.TabIndex = 67
        '
        'lblMsg_1
        '
        Me.lblMsg_1.BackColor = System.Drawing.SystemColors.Window
        Me.lblMsg_1.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMsg_1.Font = New System.Drawing.Font("Courier New", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMsg_1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.lblMsg_1.Location = New System.Drawing.Point(10, 5)
        Me.lblMsg_1.Name = "lblMsg_1"
        Me.lblMsg_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMsg_1.Size = New System.Drawing.Size(737, 17)
        Me.lblMsg_1.TabIndex = 68
        Me.lblMsg_1.Text = "12345678901234567890123456789012345678901234567890"
        Me.lblMsg_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'SSPanel2
        '
        Me.SSPanel2.Controls.Add(Me.picGraphic)
        Me.SSPanel2.Location = New System.Drawing.Point(8, 168)
        Me.SSPanel2.Name = "SSPanel2"
        Me.SSPanel2.Size = New System.Drawing.Size(793, 433)
        Me.SSPanel2.TabIndex = 63
        '
        'picGraphic
        '
        Me.picGraphic.BackColor = System.Drawing.SystemColors.Window
        Me.picGraphic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picGraphic.Cursor = System.Windows.Forms.Cursors.Default
        Me.picGraphic.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.picGraphic.ForeColor = System.Drawing.SystemColors.WindowText
        Me.picGraphic.Location = New System.Drawing.Point(8, 8)
        Me.picGraphic.Name = "picGraphic"
        Me.picGraphic.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.picGraphic.Size = New System.Drawing.Size(777, 414)
        Me.picGraphic.TabIndex = 64
        Me.picGraphic.TabStop = False
        '
        'lblStatus
        '
        Me.lblStatus.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblStatus.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.ForeColor = System.Drawing.Color.Black
        Me.lblStatus.Location = New System.Drawing.Point(592, 632)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblStatus.Size = New System.Drawing.Size(201, 25)
        Me.lblStatus.TabIndex = 70
        Me.lblStatus.Text = "Waiting For User ..."
        Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'picStatus_4
        '
        Me.picStatus_4.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.picStatus_4.Cursor = System.Windows.Forms.Cursors.Default
        Me.picStatus_4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.picStatus_4.ForeColor = System.Drawing.SystemColors.WindowText
        Me.picStatus_4.Location = New System.Drawing.Point(728, 608)
        Me.picStatus_4.Name = "picStatus_4"
        Me.picStatus_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.picStatus_4.Size = New System.Drawing.Size(17, 17)
        Me.picStatus_4.TabIndex = 75
        Me.picStatus_4.TabStop = False
        '
        'picStatus_3
        '
        Me.picStatus_3.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.picStatus_3.Cursor = System.Windows.Forms.Cursors.Default
        Me.picStatus_3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.picStatus_3.ForeColor = System.Drawing.SystemColors.WindowText
        Me.picStatus_3.Location = New System.Drawing.Point(704, 608)
        Me.picStatus_3.Name = "picStatus_3"
        Me.picStatus_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.picStatus_3.Size = New System.Drawing.Size(17, 17)
        Me.picStatus_3.TabIndex = 74
        Me.picStatus_3.TabStop = False
        '
        'picStatus_2
        '
        Me.picStatus_2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.picStatus_2.Cursor = System.Windows.Forms.Cursors.Default
        Me.picStatus_2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.picStatus_2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.picStatus_2.Location = New System.Drawing.Point(680, 608)
        Me.picStatus_2.Name = "picStatus_2"
        Me.picStatus_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.picStatus_2.Size = New System.Drawing.Size(17, 17)
        Me.picStatus_2.TabIndex = 73
        Me.picStatus_2.TabStop = False
        '
        'picStatus_1
        '
        Me.picStatus_1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.picStatus_1.Cursor = System.Windows.Forms.Cursors.Default
        Me.picStatus_1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.picStatus_1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.picStatus_1.Location = New System.Drawing.Point(656, 608)
        Me.picStatus_1.Name = "picStatus_1"
        Me.picStatus_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.picStatus_1.Size = New System.Drawing.Size(17, 17)
        Me.picStatus_1.TabIndex = 72
        Me.picStatus_1.TabStop = False
        '
        'picStatus_0
        '
        Me.picStatus_0.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.picStatus_0.Cursor = System.Windows.Forms.Cursors.Default
        Me.picStatus_0.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.picStatus_0.ForeColor = System.Drawing.SystemColors.WindowText
        Me.picStatus_0.Location = New System.Drawing.Point(632, 608)
        Me.picStatus_0.Name = "picStatus_0"
        Me.picStatus_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.picStatus_0.Size = New System.Drawing.Size(17, 17)
        Me.picStatus_0.TabIndex = 71
        Me.picStatus_0.TabStop = False
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(865, 668)
        Me.Controls.Add(Me.ProgressBar)
        Me.Controls.Add(Me.picStatus_4)
        Me.Controls.Add(Me.picStatus_3)
        Me.Controls.Add(Me.picStatus_2)
        Me.Controls.Add(Me.picStatus_1)
        Me.Controls.Add(Me.picStatus_0)
        Me.Controls.Add(Me.fraProgramNavigation)
        Me.Controls.Add(Me.fraProgramControl)
        Me.Controls.Add(Me.MainPanel)
        Me.Controls.Add(Me.SSPanel1)
        Me.Controls.Add(Me.SSPanel2)
        Me.Controls.Add(Me.lblStatus)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(3, 29)
        Me.MaximizeBox = False
        Me.Name = "frmMain"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Text = "v"
        Me.fraProgramNavigation.ResumeLayout(False)
        Me.fraProgramControl.ResumeLayout(False)
        Me.MainPanel.ResumeLayout(False)
        Me.MainPanel.PerformLayout()
        CType(Me.pinp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pinp.ResumeLayout(False)
        Me.MainMenu.ResumeLayout(False)
        Me.MainMenu.PerformLayout()
        Me.picTestDocumentation.ResumeLayout(False)
        Me.picTestDocumentation.PerformLayout()
        Me.frMeasContinuous.ResumeLayout(False)
        Me.frMeasContinuous.PerformLayout()
        Me.PictureWindow.ResumeLayout(False)
        Me.PictureWindow.PerformLayout()
        Me.ModuleMenu.ResumeLayout(False)
        Me.ModuleInner.ResumeLayout(False)
        Me.ModuleInner.PerformLayout()
        Me.SeqTextWindow.ResumeLayout(False)
        CType(Me.picDanger, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraInstructions.ResumeLayout(False)
        Me.picInstructions.ResumeLayout(False)
        Me.fraInstrument.ResumeLayout(False)
        Me.fraInstrument.PerformLayout()
        Me.fraMeasurement.ResumeLayout(False)
        Me.fraMeasurement.PerformLayout()
        Me.fraTestInformation.ResumeLayout(False)
        Me.fraTestInformation.PerformLayout()
        Me.SSPanel1.ResumeLayout(False)
        Me.msgInBox.ResumeLayout(False)
        Me.SSPanel2.ResumeLayout(False)
        CType(Me.picGraphic, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picStatus_4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picStatus_3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picStatus_2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picStatus_1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picStatus_0, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

End Sub
    Friend WithEvents ImageList3 As System.Windows.Forms.ImageList
    Public WithEvents txtCommand As System.Windows.Forms.TextBox
    Friend WithEvents SeqTextWindowLabel As System.Windows.Forms.Label
    Friend WithEvents PictureWindow_ShapeContainer As Microsoft.VisualBasic.PowerPacks.ShapeContainer
    Friend WithEvents PictureWindow_Line As Microsoft.VisualBasic.PowerPacks.LineShape
    Friend WithEvents pinp_ShapeContainer As Microsoft.VisualBasic.PowerPacks.ShapeContainer
    Friend WithEvents pinp_Line As Microsoft.VisualBasic.PowerPacks.LineShape
#End Region
End Class