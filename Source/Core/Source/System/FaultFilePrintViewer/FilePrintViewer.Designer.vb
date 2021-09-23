<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmSearch
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
	Public WithEvents picHSplitter As System.Windows.Forms.PictureBox
	Public WithEvents txFaultFileStatus As System.Windows.Forms.RichTextBox
	Public WithEvents fraStatusInfo As System.Windows.Forms.Panel
	Public WithEvents picStatusInfo As System.Windows.Forms.Panel
	Public WithEvents tiDelay As System.Windows.Forms.Timer
	Public WithEvents picVSplitter As System.Windows.Forms.PictureBox
	Public CommonDialog1Font As System.Windows.Forms.FontDialog
	Public CommonDialog1Color As System.Windows.Forms.ColorDialog
	Public CommonDialog1Print As System.Windows.Forms.PrintDialog
	Public WithEvents _lblTitle_1 As System.Windows.Forms.Label
	Public WithEvents _lblTitle_0 As System.Windows.Forms.Label
	Public WithEvents picTitles As System.Windows.Forms.Panel
	Public WithEvents cmbERONumber As System.Windows.Forms.ComboBox
	Public WithEvents cmbSerialNumber As System.Windows.Forms.ComboBox
	Public WithEvents cmbTPSName As System.Windows.Forms.ComboBox
	Public WithEvents cmbAPSName As System.Windows.Forms.ComboBox
	Public WithEvents cmbSearchNow As System.Windows.Forms.Button
	Public WithEvents cmbNewSearch As System.Windows.Forms.Button
	Public WithEvents dtpTo As AxMSComCtl2.AxDTPicker
	Public WithEvents dtpFrom As AxMSComCtl2.AxDTPicker
	Public WithEvents lblDate As System.Windows.Forms.Label
	Public WithEvents lblEro As System.Windows.Forms.Label
	Public WithEvents lblSerial As System.Windows.Forms.Label
	Public WithEvents lblTps As System.Windows.Forms.Label
	Public WithEvents lblAps As System.Windows.Forms.Label
	Public WithEvents lblTo As System.Windows.Forms.Label
	Public WithEvents lblFrom As System.Windows.Forms.Label
	Public WithEvents fraSearch As System.Windows.Forms.Panel
	Public WithEvents picSearch As System.Windows.Forms.Panel
	Public WithEvents lvSearchList As System.Windows.Forms.ListView
	Public WithEvents imlIcons As System.Windows.Forms.ImageList
	Public WithEvents lvDetail As System.Windows.Forms.ListView
	Public WithEvents tvImages As System.Windows.Forms.ImageList
	Public WithEvents _tbToolBar_Button1 As System.Windows.Forms.ToolStripSeparator
	Public WithEvents _tbToolBar_Button2 As System.Windows.Forms.ToolStripButton
	Public WithEvents _tbToolBar_Button3 As System.Windows.Forms.ToolStripButton
	Public WithEvents _tbToolBar_Button4 As System.Windows.Forms.ToolStripSeparator
	Public WithEvents _tbToolBar_Button5 As System.Windows.Forms.ToolStripButton
	Public WithEvents _tbToolBar_Button6 As System.Windows.Forms.ToolStripButton
	Public WithEvents _tbToolBar_Button7 As System.Windows.Forms.ToolStripButton
	Public WithEvents _tbToolBar_Button8 As System.Windows.Forms.ToolStripSeparator
	Public WithEvents _tbToolBar_Button9 As System.Windows.Forms.ToolStripButton
	Public WithEvents _tbToolBar_Button10 As System.Windows.Forms.ToolStripSeparator
	Public WithEvents _tbToolBar_Button11 As System.Windows.Forms.ToolStripButton
	Public WithEvents _tbToolBar_Button12 As System.Windows.Forms.ToolStripButton
	Public WithEvents _tbToolBar_Button13 As System.Windows.Forms.ToolStripButton
	Public WithEvents tbToolBar As System.Windows.Forms.ToolStrip
	Public WithEvents tvTrack As System.Windows.Forms.TreeView
	Public WithEvents imlToolbarIcons As System.Windows.Forms.ImageList
	Public WithEvents _sbStatusBar_Panel1 As System.Windows.Forms.ToolStripStatusLabel
	Public WithEvents _sbStatusBar_Panel2 As System.Windows.Forms.ToolStripStatusLabel
	Public WithEvents _sbStatusBar_Panel3 As System.Windows.Forms.ToolStripStatusLabel
	Public WithEvents sbStatusBar As System.Windows.Forms.StatusStrip
	Public WithEvents imgHSplitter As System.Windows.Forms.PictureBox
	Public WithEvents imgVSplitter As System.Windows.Forms.PictureBox
	Public WithEvents lblTitle As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
	Public WithEvents mnuListViewMode As Microsoft.VisualBasic.Compatibility.VB6.ToolStripMenuItemArray
	Public WithEvents mnuFilePrint As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents mnuFileSeperator1 As System.Windows.Forms.ToolStripSeparator
	Public WithEvents mnuFileDelete As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents mnuFileSearch As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents mnuFileSendToFloppy As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents mnuFileSeperator As System.Windows.Forms.ToolStripSeparator
	Public WithEvents mnuFileExit As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents mnuFile As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents mnuFileFont As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents mnuEditSelectAll As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents mnuEdit As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents mnuViewToolBar As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents mnuViewStatusBar As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents mnuViewSeperator1 As System.Windows.Forms.ToolStripSeparator
	Public WithEvents _mnuListViewMode_0 As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents _mnuListViewMode_1 As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents _mnuListViewMode_2 As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents mnuViewSeperator2 As System.Windows.Forms.ToolStripSeparator
	Public WithEvents mnuViewRefresh As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents mnuView As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents mnuHelpPrintViewer As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents mnuHelpSeperator As System.Windows.Forms.ToolStripSeparator
	Public WithEvents mnuHelpAbout As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents mnuHelp As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents mnuPopSeperator As System.Windows.Forms.ToolStripSeparator
	Public WithEvents mnuPopPrint As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents mnuPopMedia As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents mnuPopSeperator2 As System.Windows.Forms.ToolStripSeparator
	Public WithEvents mnuPopSearch As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents mnuPopSeperator3 As System.Windows.Forms.ToolStripSeparator
	Public WithEvents mnuPopDelete As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents mnuPopUp As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents MainMenu1 As System.Windows.Forms.MenuStrip
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSearch))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.picHSplitter = New System.Windows.Forms.PictureBox()
        Me.picStatusInfo = New System.Windows.Forms.Panel()
        Me.fraStatusInfo = New System.Windows.Forms.Panel()
        Me.txFaultFileStatus = New System.Windows.Forms.RichTextBox()
        Me.tiDelay = New System.Windows.Forms.Timer(Me.components)
        Me.picVSplitter = New System.Windows.Forms.PictureBox()
        Me.CommonDialog1Font = New System.Windows.Forms.FontDialog()
        Me.CommonDialog1Color = New System.Windows.Forms.ColorDialog()
        Me.CommonDialog1Print = New System.Windows.Forms.PrintDialog()
        Me.CommonDialog1PrintDocument = New System.Drawing.Printing.PrintDocument()
        Me.picTitles = New System.Windows.Forms.Panel()
        Me._lblTitle_0 = New System.Windows.Forms.Label()
        Me._lblTitle_1 = New System.Windows.Forms.Label()
        Me.picSearch = New System.Windows.Forms.Panel()
        Me.fraSearch = New System.Windows.Forms.Panel()
        Me.cmbERONumber = New System.Windows.Forms.ComboBox()
        Me.cmbSerialNumber = New System.Windows.Forms.ComboBox()
        Me.cmbTPSName = New System.Windows.Forms.ComboBox()
        Me.cmbAPSName = New System.Windows.Forms.ComboBox()
        Me.cmbSearchNow = New System.Windows.Forms.Button()
        Me.cmbNewSearch = New System.Windows.Forms.Button()
        Me.dtpTo = New AxMSComCtl2.AxDTPicker()
        Me.dtpFrom = New AxMSComCtl2.AxDTPicker()
        Me.lblDate = New System.Windows.Forms.Label()
        Me.lblEro = New System.Windows.Forms.Label()
        Me.lblSerial = New System.Windows.Forms.Label()
        Me.lblTps = New System.Windows.Forms.Label()
        Me.lblAps = New System.Windows.Forms.Label()
        Me.lblTo = New System.Windows.Forms.Label()
        Me.lblFrom = New System.Windows.Forms.Label()
        Me.lvSearchList = New System.Windows.Forms.ListView()
        Me.imlIcons = New System.Windows.Forms.ImageList(Me.components)
        Me.tvImages = New System.Windows.Forms.ImageList(Me.components)
        Me.lvDetail = New System.Windows.Forms.ListView()
        Me.tbToolBar = New System.Windows.Forms.ToolStrip()
        Me.imlToolbarIcons = New System.Windows.Forms.ImageList(Me.components)
        Me._tbToolBar_Button1 = New System.Windows.Forms.ToolStripSeparator()
        Me._tbToolBar_Button2 = New System.Windows.Forms.ToolStripButton()
        Me._tbToolBar_Button3 = New System.Windows.Forms.ToolStripButton()
        Me._tbToolBar_Button4 = New System.Windows.Forms.ToolStripSeparator()
        Me._tbToolBar_Button5 = New System.Windows.Forms.ToolStripButton()
        Me._tbToolBar_Button6 = New System.Windows.Forms.ToolStripButton()
        Me._tbToolBar_Button7 = New System.Windows.Forms.ToolStripButton()
        Me._tbToolBar_Button8 = New System.Windows.Forms.ToolStripSeparator()
        Me._tbToolBar_Button9 = New System.Windows.Forms.ToolStripButton()
        Me._tbToolBar_Button10 = New System.Windows.Forms.ToolStripSeparator()
        Me._tbToolBar_Button11 = New System.Windows.Forms.ToolStripButton()
        Me._tbToolBar_Button12 = New System.Windows.Forms.ToolStripButton()
        Me._tbToolBar_Button13 = New System.Windows.Forms.ToolStripButton()
        Me.tvTrack = New System.Windows.Forms.TreeView()
        Me.sbStatusBar = New System.Windows.Forms.StatusStrip()
        Me._sbStatusBar_Panel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me._sbStatusBar_Panel2 = New System.Windows.Forms.ToolStripStatusLabel()
        Me._sbStatusBar_Panel3 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.imgHSplitter = New System.Windows.Forms.PictureBox()
        Me.imgVSplitter = New System.Windows.Forms.PictureBox()
        Me.lblTitle = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.mnuListViewMode = New Microsoft.VisualBasic.Compatibility.VB6.ToolStripMenuItemArray(Me.components)
        Me._mnuListViewMode_0 = New System.Windows.Forms.ToolStripMenuItem()
        Me._mnuListViewMode_1 = New System.Windows.Forms.ToolStripMenuItem()
        Me._mnuListViewMode_2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.MainMenu1 = New System.Windows.Forms.MenuStrip()
        Me.mnuFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFilePrint = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFileSeperator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuFileDelete = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFileSearch = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFileSendToFloppy = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFileSeperator = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuFileExit = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuEdit = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFileFont = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuEditSelectAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuView = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuViewToolBar = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuViewStatusBar = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuViewSeperator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuViewSeperator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuViewRefresh = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuHelp = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuHelpPrintViewer = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuHelpSeperator = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuHelpAbout = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPopUp = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPopSeperator = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuPopPrint = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPopMedia = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPopSeperator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuPopSearch = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPopSeperator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuPopDelete = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.picHSplitter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.picStatusInfo.SuspendLayout()
        Me.fraStatusInfo.SuspendLayout()
        CType(Me.picVSplitter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.picTitles.SuspendLayout()
        Me.picSearch.SuspendLayout()
        Me.fraSearch.SuspendLayout()
        CType(Me.dtpTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tbToolBar.SuspendLayout()
        Me.sbStatusBar.SuspendLayout()
        CType(Me.imgHSplitter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgVSplitter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTitle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.mnuListViewMode, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MainMenu1.SuspendLayout()
        Me.SuspendLayout()
        '
        'picHSplitter
        '
        Me.picHSplitter.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.picHSplitter.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.picHSplitter.ForeColor = System.Drawing.SystemColors.ControlText
        Me.picHSplitter.Location = New System.Drawing.Point(16, 376)
        Me.picHSplitter.Name = "picHSplitter"
        Me.picHSplitter.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.picHSplitter.Size = New System.Drawing.Size(719, 7)
        Me.picHSplitter.TabIndex = 13
        Me.picHSplitter.TabStop = False
        Me.picHSplitter.Visible = False
        '
        'picStatusInfo
        '
        Me.picStatusInfo.BackColor = System.Drawing.SystemColors.Control
        Me.picStatusInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.picStatusInfo.Controls.Add(Me.fraStatusInfo)
        Me.picStatusInfo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.picStatusInfo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.picStatusInfo.Location = New System.Drawing.Point(376, 72)
        Me.picStatusInfo.Name = "picStatusInfo"
        Me.picStatusInfo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.picStatusInfo.Size = New System.Drawing.Size(361, 297)
        Me.picStatusInfo.TabIndex = 26
        Me.picStatusInfo.TabStop = True
        '
        'fraStatusInfo
        '
        Me.fraStatusInfo.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.fraStatusInfo.Controls.Add(Me.txFaultFileStatus)
        Me.fraStatusInfo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraStatusInfo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraStatusInfo.Location = New System.Drawing.Point(80, 40)
        Me.fraStatusInfo.Name = "fraStatusInfo"
        Me.fraStatusInfo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraStatusInfo.Size = New System.Drawing.Size(181, 191)
        Me.fraStatusInfo.TabIndex = 27
        '
        'txFaultFileStatus
        '
        Me.txFaultFileStatus.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txFaultFileStatus.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txFaultFileStatus.Location = New System.Drawing.Point(40, 48)
        Me.txFaultFileStatus.Name = "txFaultFileStatus"
        Me.txFaultFileStatus.ReadOnly = True
        Me.txFaultFileStatus.Size = New System.Drawing.Size(121, 121)
        Me.txFaultFileStatus.TabIndex = 29
        Me.txFaultFileStatus.Text = "RichTextBox1"
        '
        'tiDelay
        '
        Me.tiDelay.Interval = 1
        '
        'picVSplitter
        '
        Me.picVSplitter.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.picVSplitter.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.picVSplitter.ForeColor = System.Drawing.SystemColors.ControlText
        Me.picVSplitter.Location = New System.Drawing.Point(358, 73)
        Me.picVSplitter.Name = "picVSplitter"
        Me.picVSplitter.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.picVSplitter.Size = New System.Drawing.Size(7, 293)
        Me.picVSplitter.TabIndex = 0
        Me.picVSplitter.TabStop = False
        Me.picVSplitter.Visible = False
        '
        'CommonDialog1Print
        '
        Me.CommonDialog1Print.Document = Me.CommonDialog1PrintDocument
        '
        'CommonDialog1PrintDocument
        '
        '
        'picTitles
        '
        Me.picTitles.BackColor = System.Drawing.SystemColors.Control
        Me.picTitles.Controls.Add(Me._lblTitle_0)
        Me.picTitles.Controls.Add(Me._lblTitle_1)
        Me.picTitles.Dock = System.Windows.Forms.DockStyle.Top
        Me.picTitles.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.picTitles.ForeColor = System.Drawing.SystemColors.WindowText
        Me.picTitles.Location = New System.Drawing.Point(0, 66)
        Me.picTitles.Name = "picTitles"
        Me.picTitles.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.picTitles.Size = New System.Drawing.Size(841, 20)
        Me.picTitles.TabIndex = 23
        '
        '_lblTitle_0
        '
        Me._lblTitle_0.BackColor = System.Drawing.SystemColors.Control
        Me._lblTitle_0.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me._lblTitle_0.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblTitle_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTitle.SetIndex(Me._lblTitle_0, CType(0, Short))
        Me._lblTitle_0.Location = New System.Drawing.Point(0, 1)
        Me._lblTitle_0.Name = "_lblTitle_0"
        Me._lblTitle_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblTitle_0.Size = New System.Drawing.Size(99, 18)
        Me._lblTitle_0.TabIndex = 24
        Me._lblTitle_0.Tag = "TreeView:"
        Me._lblTitle_0.Text = "Database Contents:"
        '
        '_lblTitle_1
        '
        Me._lblTitle_1.BackColor = System.Drawing.SystemColors.Control
        Me._lblTitle_1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me._lblTitle_1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblTitle_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTitle.SetIndex(Me._lblTitle_1, CType(1, Short))
        Me._lblTitle_1.Location = New System.Drawing.Point(103, 1)
        Me._lblTitle_1.Name = "_lblTitle_1"
        Me._lblTitle_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblTitle_1.Size = New System.Drawing.Size(158, 18)
        Me._lblTitle_1.TabIndex = 25
        Me._lblTitle_1.Tag = "ListView:"
        Me._lblTitle_1.Text = "APS Names:"
        '
        'picSearch
        '
        Me.picSearch.BackColor = System.Drawing.SystemColors.Control
        Me.picSearch.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.picSearch.Controls.Add(Me.fraSearch)
        Me.picSearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.picSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.picSearch.Location = New System.Drawing.Point(0, 416)
        Me.picSearch.Name = "picSearch"
        Me.picSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.picSearch.Size = New System.Drawing.Size(193, 337)
        Me.picSearch.TabIndex = 14
        Me.picSearch.TabStop = True
        Me.picSearch.Visible = False
        '
        'fraSearch
        '
        Me.fraSearch.BackColor = System.Drawing.SystemColors.HighlightText
        Me.fraSearch.Controls.Add(Me.cmbERONumber)
        Me.fraSearch.Controls.Add(Me.cmbSerialNumber)
        Me.fraSearch.Controls.Add(Me.cmbTPSName)
        Me.fraSearch.Controls.Add(Me.cmbAPSName)
        Me.fraSearch.Controls.Add(Me.cmbSearchNow)
        Me.fraSearch.Controls.Add(Me.cmbNewSearch)
        Me.fraSearch.Controls.Add(Me.dtpTo)
        Me.fraSearch.Controls.Add(Me.dtpFrom)
        Me.fraSearch.Controls.Add(Me.lblDate)
        Me.fraSearch.Controls.Add(Me.lblEro)
        Me.fraSearch.Controls.Add(Me.lblSerial)
        Me.fraSearch.Controls.Add(Me.lblTps)
        Me.fraSearch.Controls.Add(Me.lblAps)
        Me.fraSearch.Controls.Add(Me.lblTo)
        Me.fraSearch.Controls.Add(Me.lblFrom)
        Me.fraSearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraSearch.Location = New System.Drawing.Point(0, 0)
        Me.fraSearch.Name = "fraSearch"
        Me.fraSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraSearch.Size = New System.Drawing.Size(193, 329)
        Me.fraSearch.TabIndex = 15
        '
        'cmbERONumber
        '
        Me.cmbERONumber.BackColor = System.Drawing.SystemColors.Window
        Me.cmbERONumber.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbERONumber.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbERONumber.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbERONumber.Location = New System.Drawing.Point(4, 144)
        Me.cmbERONumber.Name = "cmbERONumber"
        Me.cmbERONumber.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbERONumber.Size = New System.Drawing.Size(183, 22)
        Me.cmbERONumber.TabIndex = 7
        '
        'cmbSerialNumber
        '
        Me.cmbSerialNumber.BackColor = System.Drawing.SystemColors.Window
        Me.cmbSerialNumber.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSerialNumber.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbSerialNumber.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbSerialNumber.Location = New System.Drawing.Point(4, 104)
        Me.cmbSerialNumber.Name = "cmbSerialNumber"
        Me.cmbSerialNumber.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbSerialNumber.Size = New System.Drawing.Size(183, 22)
        Me.cmbSerialNumber.TabIndex = 6
        '
        'cmbTPSName
        '
        Me.cmbTPSName.BackColor = System.Drawing.SystemColors.Window
        Me.cmbTPSName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTPSName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbTPSName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbTPSName.Location = New System.Drawing.Point(4, 64)
        Me.cmbTPSName.Name = "cmbTPSName"
        Me.cmbTPSName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbTPSName.Size = New System.Drawing.Size(183, 22)
        Me.cmbTPSName.TabIndex = 5
        '
        'cmbAPSName
        '
        Me.cmbAPSName.BackColor = System.Drawing.SystemColors.Window
        Me.cmbAPSName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAPSName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbAPSName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbAPSName.Location = New System.Drawing.Point(4, 24)
        Me.cmbAPSName.Name = "cmbAPSName"
        Me.cmbAPSName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbAPSName.Size = New System.Drawing.Size(183, 22)
        Me.cmbAPSName.Sorted = True
        Me.cmbAPSName.TabIndex = 4
        '
        'cmbSearchNow
        '
        Me.cmbSearchNow.BackColor = System.Drawing.SystemColors.Control
        Me.cmbSearchNow.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbSearchNow.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmbSearchNow.Location = New System.Drawing.Point(4, 232)
        Me.cmbSearchNow.Name = "cmbSearchNow"
        Me.cmbSearchNow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbSearchNow.Size = New System.Drawing.Size(79, 25)
        Me.cmbSearchNow.TabIndex = 10
        Me.cmbSearchNow.Text = "&Search Now"
        Me.cmbSearchNow.UseVisualStyleBackColor = False
        '
        'cmbNewSearch
        '
        Me.cmbNewSearch.BackColor = System.Drawing.SystemColors.Control
        Me.cmbNewSearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbNewSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmbNewSearch.Location = New System.Drawing.Point(104, 232)
        Me.cmbNewSearch.Name = "cmbNewSearch"
        Me.cmbNewSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbNewSearch.Size = New System.Drawing.Size(79, 25)
        Me.cmbNewSearch.TabIndex = 11
        Me.cmbNewSearch.Text = "&New Search"
        Me.cmbNewSearch.UseVisualStyleBackColor = False
        '
        'dtpTo
        '
        Me.dtpTo.Location = New System.Drawing.Point(104, 200)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.OcxState = CType(resources.GetObject("dtpTo.OcxState"), System.Windows.Forms.AxHost.State)
        Me.dtpTo.Size = New System.Drawing.Size(79, 25)
        Me.dtpTo.TabIndex = 9
        '
        'dtpFrom
        '
        Me.dtpFrom.Location = New System.Drawing.Point(4, 200)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.OcxState = CType(resources.GetObject("dtpFrom.OcxState"), System.Windows.Forms.AxHost.State)
        Me.dtpFrom.Size = New System.Drawing.Size(79, 25)
        Me.dtpFrom.TabIndex = 8
        '
        'lblDate
        '
        Me.lblDate.BackColor = System.Drawing.SystemColors.Window
        Me.lblDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblDate.Location = New System.Drawing.Point(4, 168)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDate.Size = New System.Drawing.Size(183, 17)
        Me.lblDate.TabIndex = 22
        Me.lblDate.Text = "Date"
        Me.lblDate.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblEro
        '
        Me.lblEro.BackColor = System.Drawing.SystemColors.Window
        Me.lblEro.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEro.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblEro.Location = New System.Drawing.Point(4, 128)
        Me.lblEro.Name = "lblEro"
        Me.lblEro.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblEro.Size = New System.Drawing.Size(183, 17)
        Me.lblEro.TabIndex = 21
        Me.lblEro.Text = "ERO Number"
        Me.lblEro.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblSerial
        '
        Me.lblSerial.BackColor = System.Drawing.SystemColors.Window
        Me.lblSerial.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSerial.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblSerial.Location = New System.Drawing.Point(4, 88)
        Me.lblSerial.Name = "lblSerial"
        Me.lblSerial.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSerial.Size = New System.Drawing.Size(183, 17)
        Me.lblSerial.TabIndex = 20
        Me.lblSerial.Text = "Serial Number"
        Me.lblSerial.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblTps
        '
        Me.lblTps.BackColor = System.Drawing.SystemColors.Window
        Me.lblTps.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTps.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblTps.Location = New System.Drawing.Point(4, 48)
        Me.lblTps.Name = "lblTps"
        Me.lblTps.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTps.Size = New System.Drawing.Size(183, 17)
        Me.lblTps.TabIndex = 19
        Me.lblTps.Text = "TPS Name"
        Me.lblTps.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblAps
        '
        Me.lblAps.BackColor = System.Drawing.SystemColors.Window
        Me.lblAps.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAps.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblAps.Location = New System.Drawing.Point(4, 8)
        Me.lblAps.Name = "lblAps"
        Me.lblAps.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAps.Size = New System.Drawing.Size(183, 17)
        Me.lblAps.TabIndex = 18
        Me.lblAps.Text = "APS Name"
        Me.lblAps.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblTo
        '
        Me.lblTo.BackColor = System.Drawing.SystemColors.Window
        Me.lblTo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblTo.Location = New System.Drawing.Point(104, 184)
        Me.lblTo.Name = "lblTo"
        Me.lblTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTo.Size = New System.Drawing.Size(79, 17)
        Me.lblTo.TabIndex = 17
        Me.lblTo.Text = "To"
        Me.lblTo.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblFrom
        '
        Me.lblFrom.BackColor = System.Drawing.SystemColors.Window
        Me.lblFrom.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFrom.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblFrom.Location = New System.Drawing.Point(4, 184)
        Me.lblFrom.Name = "lblFrom"
        Me.lblFrom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblFrom.Size = New System.Drawing.Size(79, 13)
        Me.lblFrom.TabIndex = 16
        Me.lblFrom.Text = "From"
        Me.lblFrom.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lvSearchList
        '
        Me.lvSearchList.BackColor = System.Drawing.SystemColors.Window
        Me.lvSearchList.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvSearchList.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lvSearchList.HideSelection = False
        Me.lvSearchList.LargeImageList = Me.imlIcons
        Me.lvSearchList.Location = New System.Drawing.Point(200, 424)
        Me.lvSearchList.Name = "lvSearchList"
        Me.lvSearchList.Size = New System.Drawing.Size(417, 313)
        Me.lvSearchList.SmallImageList = Me.tvImages
        Me.lvSearchList.TabIndex = 12
        Me.lvSearchList.UseCompatibleStateImageBehavior = False
        '
        'imlIcons
        '
        Me.imlIcons.ImageStream = CType(resources.GetObject("imlIcons.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imlIcons.TransparentColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.imlIcons.Images.SetKeyName(0, "closed")
        Me.imlIcons.Images.SetKeyName(1, "open")
        Me.imlIcons.Images.SetKeyName(2, "closed1")
        Me.imlIcons.Images.SetKeyName(3, "file")
        '
        'tvImages
        '
        Me.tvImages.ImageStream = CType(resources.GetObject("tvImages.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.tvImages.TransparentColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.tvImages.Images.SetKeyName(0, "closed")
        Me.tvImages.Images.SetKeyName(1, "open")
        Me.tvImages.Images.SetKeyName(2, "file")
        '
        'lvDetail
        '
        Me.lvDetail.Alignment = System.Windows.Forms.ListViewAlignment.Left
        Me.lvDetail.AllowColumnReorder = True
        Me.lvDetail.BackColor = System.Drawing.SystemColors.Window
        Me.lvDetail.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvDetail.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lvDetail.HideSelection = False
        Me.lvDetail.LargeImageList = Me.imlIcons
        Me.lvDetail.Location = New System.Drawing.Point(120, 87)
        Me.lvDetail.Name = "lvDetail"
        Me.lvDetail.Size = New System.Drawing.Size(222, 279)
        Me.lvDetail.SmallImageList = Me.tvImages
        Me.lvDetail.TabIndex = 2
        Me.lvDetail.UseCompatibleStateImageBehavior = False
        '
        'tbToolBar
        '
        Me.tbToolBar.ImageList = Me.imlToolbarIcons
        Me.tbToolBar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me._tbToolBar_Button1, Me._tbToolBar_Button2, Me._tbToolBar_Button3, Me._tbToolBar_Button4, Me._tbToolBar_Button5, Me._tbToolBar_Button6, Me._tbToolBar_Button7, Me._tbToolBar_Button8, Me._tbToolBar_Button9, Me._tbToolBar_Button10, Me._tbToolBar_Button11, Me._tbToolBar_Button12, Me._tbToolBar_Button13})
        Me.tbToolBar.Location = New System.Drawing.Point(0, 24)
        Me.tbToolBar.Name = "tbToolBar"
        Me.tbToolBar.Size = New System.Drawing.Size(841, 42)
        Me.tbToolBar.TabIndex = 3
        '
        'imlToolbarIcons
        '
        Me.imlToolbarIcons.ImageStream = CType(resources.GetObject("imlToolbarIcons.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imlToolbarIcons.TransparentColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.imlToolbarIcons.Images.SetKeyName(0, "Search")
        Me.imlToolbarIcons.Images.SetKeyName(1, "Font")
        Me.imlToolbarIcons.Images.SetKeyName(2, "Print_To")
        Me.imlToolbarIcons.Images.SetKeyName(3, "Print")
        Me.imlToolbarIcons.Images.SetKeyName(4, "Delete")
        Me.imlToolbarIcons.Images.SetKeyName(5, "refresh")
        Me.imlToolbarIcons.Images.SetKeyName(6, "Large_Icons")
        Me.imlToolbarIcons.Images.SetKeyName(7, "Small_Icons")
        Me.imlToolbarIcons.Images.SetKeyName(8, "List")
        '
        '_tbToolBar_Button1
        '
        Me._tbToolBar_Button1.AutoSize = False
        Me._tbToolBar_Button1.Name = "_tbToolBar_Button1"
        Me._tbToolBar_Button1.Size = New System.Drawing.Size(40, 39)
        '
        '_tbToolBar_Button2
        '
        Me._tbToolBar_Button2.AutoSize = False
        Me._tbToolBar_Button2.ImageKey = "Print"
        Me._tbToolBar_Button2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me._tbToolBar_Button2.Name = "_tbToolBar_Button2"
        Me._tbToolBar_Button2.Size = New System.Drawing.Size(40, 39)
        Me._tbToolBar_Button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me._tbToolBar_Button2.ToolTipText = "Print"
        '
        '_tbToolBar_Button3
        '
        Me._tbToolBar_Button3.AutoSize = False
        Me._tbToolBar_Button3.ImageKey = "Font"
        Me._tbToolBar_Button3.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me._tbToolBar_Button3.Name = "_tbToolBar_Button3"
        Me._tbToolBar_Button3.Size = New System.Drawing.Size(40, 39)
        Me._tbToolBar_Button3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me._tbToolBar_Button3.ToolTipText = "Font"
        Me._tbToolBar_Button3.Visible = False
        '
        '_tbToolBar_Button4
        '
        Me._tbToolBar_Button4.AutoSize = False
        Me._tbToolBar_Button4.Name = "_tbToolBar_Button4"
        Me._tbToolBar_Button4.Size = New System.Drawing.Size(40, 39)
        '
        '_tbToolBar_Button5
        '
        Me._tbToolBar_Button5.AutoSize = False
        Me._tbToolBar_Button5.ImageKey = "Delete"
        Me._tbToolBar_Button5.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me._tbToolBar_Button5.Name = "_tbToolBar_Button5"
        Me._tbToolBar_Button5.Size = New System.Drawing.Size(40, 39)
        Me._tbToolBar_Button5.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me._tbToolBar_Button5.ToolTipText = "Delete"
        '
        '_tbToolBar_Button6
        '
        Me._tbToolBar_Button6.AutoSize = False
        Me._tbToolBar_Button6.ImageKey = "Search"
        Me._tbToolBar_Button6.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me._tbToolBar_Button6.Name = "_tbToolBar_Button6"
        Me._tbToolBar_Button6.Size = New System.Drawing.Size(40, 39)
        Me._tbToolBar_Button6.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me._tbToolBar_Button6.ToolTipText = "Search"
        '
        '_tbToolBar_Button7
        '
        Me._tbToolBar_Button7.AutoSize = False
        Me._tbToolBar_Button7.ImageKey = "Print_To"
        Me._tbToolBar_Button7.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me._tbToolBar_Button7.Name = "_tbToolBar_Button7"
        Me._tbToolBar_Button7.Size = New System.Drawing.Size(40, 39)
        Me._tbToolBar_Button7.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me._tbToolBar_Button7.ToolTipText = "Send To Removable Media"
        '
        '_tbToolBar_Button8
        '
        Me._tbToolBar_Button8.AutoSize = False
        Me._tbToolBar_Button8.Name = "_tbToolBar_Button8"
        Me._tbToolBar_Button8.Size = New System.Drawing.Size(40, 39)
        '
        '_tbToolBar_Button9
        '
        Me._tbToolBar_Button9.AutoSize = False
        Me._tbToolBar_Button9.ImageKey = "refresh"
        Me._tbToolBar_Button9.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me._tbToolBar_Button9.Name = "_tbToolBar_Button9"
        Me._tbToolBar_Button9.Size = New System.Drawing.Size(40, 39)
        Me._tbToolBar_Button9.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me._tbToolBar_Button9.ToolTipText = "Refresh"
        '
        '_tbToolBar_Button10
        '
        Me._tbToolBar_Button10.AutoSize = False
        Me._tbToolBar_Button10.Name = "_tbToolBar_Button10"
        Me._tbToolBar_Button10.Size = New System.Drawing.Size(40, 39)
        '
        '_tbToolBar_Button11
        '
        Me._tbToolBar_Button11.AutoSize = False
        Me._tbToolBar_Button11.ImageKey = "Large_Icons"
        Me._tbToolBar_Button11.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me._tbToolBar_Button11.Name = "_tbToolBar_Button11"
        Me._tbToolBar_Button11.Size = New System.Drawing.Size(40, 39)
        Me._tbToolBar_Button11.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me._tbToolBar_Button11.ToolTipText = "Large Icons"
        '
        '_tbToolBar_Button12
        '
        Me._tbToolBar_Button12.AutoSize = False
        Me._tbToolBar_Button12.ImageKey = "Small_Icons"
        Me._tbToolBar_Button12.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me._tbToolBar_Button12.Name = "_tbToolBar_Button12"
        Me._tbToolBar_Button12.Size = New System.Drawing.Size(40, 39)
        Me._tbToolBar_Button12.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me._tbToolBar_Button12.ToolTipText = "Small Icons"
        '
        '_tbToolBar_Button13
        '
        Me._tbToolBar_Button13.AutoSize = False
        Me._tbToolBar_Button13.ImageKey = "List"
        Me._tbToolBar_Button13.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me._tbToolBar_Button13.Name = "_tbToolBar_Button13"
        Me._tbToolBar_Button13.Size = New System.Drawing.Size(40, 39)
        Me._tbToolBar_Button13.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me._tbToolBar_Button13.ToolTipText = "List"
        '
        'tvTrack
        '
        Me.tvTrack.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.tvTrack.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tvTrack.HideSelection = False
        Me.tvTrack.ImageIndex = 0
        Me.tvTrack.ImageList = Me.tvImages
        Me.tvTrack.Indent = 18
        Me.tvTrack.Location = New System.Drawing.Point(0, 87)
        Me.tvTrack.Name = "tvTrack"
        Me.tvTrack.SelectedImageIndex = 0
        Me.tvTrack.Size = New System.Drawing.Size(115, 276)
        Me.tvTrack.Sorted = True
        Me.tvTrack.TabIndex = 1
        '
        'sbStatusBar
        '
        Me.sbStatusBar.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.sbStatusBar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me._sbStatusBar_Panel1, Me._sbStatusBar_Panel2, Me._sbStatusBar_Panel3})
        Me.sbStatusBar.Location = New System.Drawing.Point(0, 765)
        Me.sbStatusBar.Name = "sbStatusBar"
        Me.sbStatusBar.Size = New System.Drawing.Size(841, 22)
        Me.sbStatusBar.TabIndex = 28
        '
        '_sbStatusBar_Panel1
        '
        Me._sbStatusBar_Panel1.AutoSize = False
        Me._sbStatusBar_Panel1.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me._sbStatusBar_Panel1.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me._sbStatusBar_Panel1.Margin = New System.Windows.Forms.Padding(0)
        Me._sbStatusBar_Panel1.Name = "_sbStatusBar_Panel1"
        Me._sbStatusBar_Panel1.Size = New System.Drawing.Size(634, 22)
        Me._sbStatusBar_Panel1.Spring = True
        Me._sbStatusBar_Panel1.Text = "Status"
        Me._sbStatusBar_Panel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        '_sbStatusBar_Panel2
        '
        Me._sbStatusBar_Panel2.AutoSize = False
        Me._sbStatusBar_Panel2.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me._sbStatusBar_Panel2.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me._sbStatusBar_Panel2.Margin = New System.Windows.Forms.Padding(0)
        Me._sbStatusBar_Panel2.Name = "_sbStatusBar_Panel2"
        Me._sbStatusBar_Panel2.Size = New System.Drawing.Size(96, 22)
        Me._sbStatusBar_Panel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        '_sbStatusBar_Panel3
        '
        Me._sbStatusBar_Panel3.AutoSize = False
        Me._sbStatusBar_Panel3.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me._sbStatusBar_Panel3.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me._sbStatusBar_Panel3.Margin = New System.Windows.Forms.Padding(0)
        Me._sbStatusBar_Panel3.Name = "_sbStatusBar_Panel3"
        Me._sbStatusBar_Panel3.Size = New System.Drawing.Size(96, 22)
        Me._sbStatusBar_Panel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'imgHSplitter
        '
        Me.imgHSplitter.Location = New System.Drawing.Point(0, 400)
        Me.imgHSplitter.Name = "imgHSplitter"
        Me.imgHSplitter.Size = New System.Drawing.Size(743, 7)
        Me.imgHSplitter.TabIndex = 29
        Me.imgHSplitter.TabStop = False
        '
        'imgVSplitter
        '
        Me.imgVSplitter.Location = New System.Drawing.Point(344, 80)
        Me.imgVSplitter.Name = "imgVSplitter"
        Me.imgVSplitter.Size = New System.Drawing.Size(7, 239)
        Me.imgVSplitter.TabIndex = 30
        Me.imgVSplitter.TabStop = False
        '
        'mnuListViewMode
        '
        '
        '_mnuListViewMode_0
        '
        Me._mnuListViewMode_0.Checked = True
        Me._mnuListViewMode_0.CheckState = System.Windows.Forms.CheckState.Checked
        Me.mnuListViewMode.SetIndex(Me._mnuListViewMode_0, CType(0, Short))
        Me._mnuListViewMode_0.Name = "_mnuListViewMode_0"
        Me._mnuListViewMode_0.Size = New System.Drawing.Size(134, 22)
        Me._mnuListViewMode_0.Text = "Lar&ge Icons"
        '
        '_mnuListViewMode_1
        '
        Me._mnuListViewMode_1.Checked = True
        Me._mnuListViewMode_1.CheckState = System.Windows.Forms.CheckState.Checked
        Me.mnuListViewMode.SetIndex(Me._mnuListViewMode_1, CType(1, Short))
        Me._mnuListViewMode_1.Name = "_mnuListViewMode_1"
        Me._mnuListViewMode_1.Size = New System.Drawing.Size(134, 22)
        Me._mnuListViewMode_1.Text = "S&mall Icons"
        '
        '_mnuListViewMode_2
        '
        Me._mnuListViewMode_2.Checked = True
        Me._mnuListViewMode_2.CheckState = System.Windows.Forms.CheckState.Checked
        Me.mnuListViewMode.SetIndex(Me._mnuListViewMode_2, CType(2, Short))
        Me._mnuListViewMode_2.Name = "_mnuListViewMode_2"
        Me._mnuListViewMode_2.Size = New System.Drawing.Size(134, 22)
        Me._mnuListViewMode_2.Text = "&List"
        '
        'MainMenu1
        '
        Me.MainMenu1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFile, Me.mnuEdit, Me.mnuView, Me.mnuHelp, Me.mnuPopUp})
        Me.MainMenu1.Location = New System.Drawing.Point(0, 0)
        Me.MainMenu1.Name = "MainMenu1"
        Me.MainMenu1.Size = New System.Drawing.Size(841, 24)
        Me.MainMenu1.TabIndex = 31
        '
        'mnuFile
        '
        Me.mnuFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFilePrint, Me.mnuFileSeperator1, Me.mnuFileDelete, Me.mnuFileSearch, Me.mnuFileSendToFloppy, Me.mnuFileSeperator, Me.mnuFileExit})
        Me.mnuFile.Name = "mnuFile"
        Me.mnuFile.Size = New System.Drawing.Size(37, 20)
        Me.mnuFile.Text = "&File"
        '
        'mnuFilePrint
        '
        Me.mnuFilePrint.Name = "mnuFilePrint"
        Me.mnuFilePrint.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.P), System.Windows.Forms.Keys)
        Me.mnuFilePrint.Size = New System.Drawing.Size(258, 22)
        Me.mnuFilePrint.Text = "&Print"
        '
        'mnuFileSeperator1
        '
        Me.mnuFileSeperator1.Name = "mnuFileSeperator1"
        Me.mnuFileSeperator1.Size = New System.Drawing.Size(255, 6)
        '
        'mnuFileDelete
        '
        Me.mnuFileDelete.Name = "mnuFileDelete"
        Me.mnuFileDelete.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.D), System.Windows.Forms.Keys)
        Me.mnuFileDelete.Size = New System.Drawing.Size(258, 22)
        Me.mnuFileDelete.Text = "&Delete"
        '
        'mnuFileSearch
        '
        Me.mnuFileSearch.Name = "mnuFileSearch"
        Me.mnuFileSearch.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.mnuFileSearch.Size = New System.Drawing.Size(258, 22)
        Me.mnuFileSearch.Text = "&Search"
        '
        'mnuFileSendToFloppy
        '
        Me.mnuFileSendToFloppy.Name = "mnuFileSendToFloppy"
        Me.mnuFileSendToFloppy.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F), System.Windows.Forms.Keys)
        Me.mnuFileSendToFloppy.Size = New System.Drawing.Size(258, 22)
        Me.mnuFileSendToFloppy.Text = "Send to Removeable &Media"
        '
        'mnuFileSeperator
        '
        Me.mnuFileSeperator.Name = "mnuFileSeperator"
        Me.mnuFileSeperator.Size = New System.Drawing.Size(255, 6)
        '
        'mnuFileExit
        '
        Me.mnuFileExit.Name = "mnuFileExit"
        Me.mnuFileExit.Size = New System.Drawing.Size(258, 22)
        Me.mnuFileExit.Text = "E&xit"
        '
        'mnuEdit
        '
        Me.mnuEdit.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFileFont, Me.mnuEditSelectAll})
        Me.mnuEdit.Name = "mnuEdit"
        Me.mnuEdit.Size = New System.Drawing.Size(39, 20)
        Me.mnuEdit.Text = "&Edit"
        '
        'mnuFileFont
        '
        Me.mnuFileFont.Name = "mnuFileFont"
        Me.mnuFileFont.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.G), System.Windows.Forms.Keys)
        Me.mnuFileFont.Size = New System.Drawing.Size(164, 22)
        Me.mnuFileFont.Text = "&Font"
        Me.mnuFileFont.Visible = False
        '
        'mnuEditSelectAll
        '
        Me.mnuEditSelectAll.Name = "mnuEditSelectAll"
        Me.mnuEditSelectAll.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.A), System.Windows.Forms.Keys)
        Me.mnuEditSelectAll.Size = New System.Drawing.Size(164, 22)
        Me.mnuEditSelectAll.Text = "&Select All"
        '
        'mnuView
        '
        Me.mnuView.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuViewToolBar, Me.mnuViewStatusBar, Me.mnuViewSeperator1, Me._mnuListViewMode_0, Me._mnuListViewMode_1, Me._mnuListViewMode_2, Me.mnuViewSeperator2, Me.mnuViewRefresh})
        Me.mnuView.Name = "mnuView"
        Me.mnuView.Size = New System.Drawing.Size(44, 20)
        Me.mnuView.Text = "&View"
        '
        'mnuViewToolBar
        '
        Me.mnuViewToolBar.Checked = True
        Me.mnuViewToolBar.CheckState = System.Windows.Forms.CheckState.Checked
        Me.mnuViewToolBar.Name = "mnuViewToolBar"
        Me.mnuViewToolBar.Size = New System.Drawing.Size(134, 22)
        Me.mnuViewToolBar.Text = "&Tool Bar"
        '
        'mnuViewStatusBar
        '
        Me.mnuViewStatusBar.Checked = True
        Me.mnuViewStatusBar.CheckState = System.Windows.Forms.CheckState.Checked
        Me.mnuViewStatusBar.Name = "mnuViewStatusBar"
        Me.mnuViewStatusBar.Size = New System.Drawing.Size(134, 22)
        Me.mnuViewStatusBar.Text = "Status &Bar"
        '
        'mnuViewSeperator1
        '
        Me.mnuViewSeperator1.Name = "mnuViewSeperator1"
        Me.mnuViewSeperator1.Size = New System.Drawing.Size(131, 6)
        '
        'mnuViewSeperator2
        '
        Me.mnuViewSeperator2.Name = "mnuViewSeperator2"
        Me.mnuViewSeperator2.Size = New System.Drawing.Size(131, 6)
        '
        'mnuViewRefresh
        '
        Me.mnuViewRefresh.Name = "mnuViewRefresh"
        Me.mnuViewRefresh.ShortcutKeys = System.Windows.Forms.Keys.F5
        Me.mnuViewRefresh.Size = New System.Drawing.Size(134, 22)
        Me.mnuViewRefresh.Text = "&Refresh"
        '
        'mnuHelp
        '
        Me.mnuHelp.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuHelpPrintViewer, Me.mnuHelpSeperator, Me.mnuHelpAbout})
        Me.mnuHelp.Name = "mnuHelp"
        Me.mnuHelp.Size = New System.Drawing.Size(44, 20)
        Me.mnuHelp.Text = "&Help"
        '
        'mnuHelpPrintViewer
        '
        Me.mnuHelpPrintViewer.Name = "mnuHelpPrintViewer"
        Me.mnuHelpPrintViewer.ShortcutKeys = System.Windows.Forms.Keys.F1
        Me.mnuHelpPrintViewer.Size = New System.Drawing.Size(243, 22)
        Me.mnuHelpPrintViewer.Text = "&File Print Viewer Information"
        '
        'mnuHelpSeperator
        '
        Me.mnuHelpSeperator.Name = "mnuHelpSeperator"
        Me.mnuHelpSeperator.Size = New System.Drawing.Size(240, 6)
        '
        'mnuHelpAbout
        '
        Me.mnuHelpAbout.Name = "mnuHelpAbout"
        Me.mnuHelpAbout.Size = New System.Drawing.Size(243, 22)
        Me.mnuHelpAbout.Text = "&About File Print Viewer"
        '
        'mnuPopUp
        '
        Me.mnuPopUp.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuPopSeperator, Me.mnuPopPrint, Me.mnuPopMedia, Me.mnuPopSeperator2, Me.mnuPopSearch, Me.mnuPopSeperator3, Me.mnuPopDelete})
        Me.mnuPopUp.Name = "mnuPopUp"
        Me.mnuPopUp.Size = New System.Drawing.Size(55, 20)
        Me.mnuPopUp.Text = "PopUp"
        Me.mnuPopUp.Visible = False
        '
        'mnuPopSeperator
        '
        Me.mnuPopSeperator.Name = "mnuPopSeperator"
        Me.mnuPopSeperator.Size = New System.Drawing.Size(215, 6)
        '
        'mnuPopPrint
        '
        Me.mnuPopPrint.Name = "mnuPopPrint"
        Me.mnuPopPrint.Size = New System.Drawing.Size(218, 22)
        Me.mnuPopPrint.Text = "&Print"
        '
        'mnuPopMedia
        '
        Me.mnuPopMedia.Name = "mnuPopMedia"
        Me.mnuPopMedia.Size = New System.Drawing.Size(218, 22)
        Me.mnuPopMedia.Text = "Send to Removeable &Media"
        '
        'mnuPopSeperator2
        '
        Me.mnuPopSeperator2.Name = "mnuPopSeperator2"
        Me.mnuPopSeperator2.Size = New System.Drawing.Size(215, 6)
        '
        'mnuPopSearch
        '
        Me.mnuPopSearch.Name = "mnuPopSearch"
        Me.mnuPopSearch.Size = New System.Drawing.Size(218, 22)
        Me.mnuPopSearch.Text = "&Search"
        '
        'mnuPopSeperator3
        '
        Me.mnuPopSeperator3.Name = "mnuPopSeperator3"
        Me.mnuPopSeperator3.Size = New System.Drawing.Size(215, 6)
        '
        'mnuPopDelete
        '
        Me.mnuPopDelete.Name = "mnuPopDelete"
        Me.mnuPopDelete.Size = New System.Drawing.Size(218, 22)
        Me.mnuPopDelete.Text = "&Delete"
        '
        'frmSearch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(841, 787)
        Me.Controls.Add(Me.picHSplitter)
        Me.Controls.Add(Me.picStatusInfo)
        Me.Controls.Add(Me.picVSplitter)
        Me.Controls.Add(Me.picTitles)
        Me.Controls.Add(Me.picSearch)
        Me.Controls.Add(Me.lvSearchList)
        Me.Controls.Add(Me.tbToolBar)
        Me.Controls.Add(Me.sbStatusBar)
        Me.Controls.Add(Me.imgHSplitter)
        Me.Controls.Add(Me.imgVSplitter)
        Me.Controls.Add(Me.MainMenu1)
        Me.Controls.Add(Me.lvDetail)
        Me.Controls.Add(Me.tvTrack)
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(11, 49)
        Me.Name = "frmSearch"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "File Print Viewer"
        CType(Me.picHSplitter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.picStatusInfo.ResumeLayout(False)
        Me.fraStatusInfo.ResumeLayout(False)
        CType(Me.picVSplitter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.picTitles.ResumeLayout(False)
        Me.picSearch.ResumeLayout(False)
        Me.fraSearch.ResumeLayout(False)
        CType(Me.dtpTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFrom, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tbToolBar.ResumeLayout(False)
        Me.tbToolBar.PerformLayout()
        Me.sbStatusBar.ResumeLayout(False)
        Me.sbStatusBar.PerformLayout()
        CType(Me.imgHSplitter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgVSplitter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTitle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.mnuListViewMode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MainMenu1.ResumeLayout(False)
        Me.MainMenu1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

End Sub
 Friend WithEvents CommonDialog1PrintDocument As System.Drawing.Printing.PrintDocument
#End Region 
End Class