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
		Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmSearch))
		Me.components = New System.ComponentModel.Container()
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(components)
		Me.picHSplitter = New System.Windows.Forms.PictureBox
		Me.picStatusInfo = New System.Windows.Forms.Panel
		Me.fraStatusInfo = New System.Windows.Forms.Panel
		Me.txFaultFileStatus = New System.Windows.Forms.RichTextBox
		Me.tiDelay = New System.Windows.Forms.Timer(components)
		Me.picVSplitter = New System.Windows.Forms.PictureBox
		Me.CommonDialog1Font = New System.Windows.Forms.FontDialog
		Me.CommonDialog1Color = New System.Windows.Forms.ColorDialog
		Me.CommonDialog1Print = New System.Windows.Forms.PrintDialog
		Me.CommonDialog1Print.PrinterSettings = New System.Drawing.Printing.PrinterSettings
		Me.picTitles = New System.Windows.Forms.Panel
		Me._lblTitle_1 = New System.Windows.Forms.Label
		Me._lblTitle_0 = New System.Windows.Forms.Label
		Me.picSearch = New System.Windows.Forms.Panel
		Me.fraSearch = New System.Windows.Forms.Panel
		Me.cmbERONumber = New System.Windows.Forms.ComboBox
		Me.cmbSerialNumber = New System.Windows.Forms.ComboBox
		Me.cmbTPSName = New System.Windows.Forms.ComboBox
		Me.cmbAPSName = New System.Windows.Forms.ComboBox
		Me.cmbSearchNow = New System.Windows.Forms.Button
		Me.cmbNewSearch = New System.Windows.Forms.Button
		Me.dtpTo = New AxMSComCtl2.AxDTPicker
		Me.dtpFrom = New AxMSComCtl2.AxDTPicker
		Me.lblDate = New System.Windows.Forms.Label
		Me.lblEro = New System.Windows.Forms.Label
		Me.lblSerial = New System.Windows.Forms.Label
		Me.lblTps = New System.Windows.Forms.Label
		Me.lblAps = New System.Windows.Forms.Label
		Me.lblTo = New System.Windows.Forms.Label
		Me.lblFrom = New System.Windows.Forms.Label
		Me.lvSearchList = New System.Windows.Forms.ListView
		Me.imlIcons = New System.Windows.Forms.ImageList
		Me.lvDetail = New System.Windows.Forms.ListView
		Me.tvImages = New System.Windows.Forms.ImageList
		Me.tbToolBar = New System.Windows.Forms.ToolStrip
		Me._tbToolBar_Button1 = New System.Windows.Forms.ToolStripSeparator
		Me._tbToolBar_Button2 = New System.Windows.Forms.ToolStripButton
		Me._tbToolBar_Button3 = New System.Windows.Forms.ToolStripButton
		Me._tbToolBar_Button4 = New System.Windows.Forms.ToolStripSeparator
		Me._tbToolBar_Button5 = New System.Windows.Forms.ToolStripButton
		Me._tbToolBar_Button6 = New System.Windows.Forms.ToolStripButton
		Me._tbToolBar_Button7 = New System.Windows.Forms.ToolStripButton
		Me._tbToolBar_Button8 = New System.Windows.Forms.ToolStripSeparator
		Me._tbToolBar_Button9 = New System.Windows.Forms.ToolStripButton
		Me._tbToolBar_Button10 = New System.Windows.Forms.ToolStripSeparator
		Me._tbToolBar_Button11 = New System.Windows.Forms.ToolStripButton
		Me._tbToolBar_Button12 = New System.Windows.Forms.ToolStripButton
		Me._tbToolBar_Button13 = New System.Windows.Forms.ToolStripButton
		Me.tvTrack = New System.Windows.Forms.TreeView
		Me.imlToolbarIcons = New System.Windows.Forms.ImageList
		Me.sbStatusBar = New System.Windows.Forms.StatusStrip
		Me._sbStatusBar_Panel1 = New System.Windows.Forms.ToolStripStatusLabel
		Me._sbStatusBar_Panel2 = New System.Windows.Forms.ToolStripStatusLabel
		Me._sbStatusBar_Panel3 = New System.Windows.Forms.ToolStripStatusLabel
		Me.imgHSplitter = New System.Windows.Forms.PictureBox
		Me.imgVSplitter = New System.Windows.Forms.PictureBox
		Me.lblTitle = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(components)
		Me.mnuListViewMode = New Microsoft.VisualBasic.Compatibility.VB6.ToolStripMenuItemArray(components)
		Me.MainMenu1 = New System.Windows.Forms.MenuStrip
		Me.mnuFile = New System.Windows.Forms.ToolStripMenuItem
		Me.mnuFilePrint = New System.Windows.Forms.ToolStripMenuItem
		Me.mnuFileSeperator1 = New System.Windows.Forms.ToolStripSeparator
		Me.mnuFileDelete = New System.Windows.Forms.ToolStripMenuItem
		Me.mnuFileSearch = New System.Windows.Forms.ToolStripMenuItem
		Me.mnuFileSendToFloppy = New System.Windows.Forms.ToolStripMenuItem
		Me.mnuFileSeperator = New System.Windows.Forms.ToolStripSeparator
		Me.mnuFileExit = New System.Windows.Forms.ToolStripMenuItem
		Me.mnuEdit = New System.Windows.Forms.ToolStripMenuItem
		Me.mnuFileFont = New System.Windows.Forms.ToolStripMenuItem
		Me.mnuEditSelectAll = New System.Windows.Forms.ToolStripMenuItem
		Me.mnuView = New System.Windows.Forms.ToolStripMenuItem
		Me.mnuViewToolBar = New System.Windows.Forms.ToolStripMenuItem
		Me.mnuViewStatusBar = New System.Windows.Forms.ToolStripMenuItem
		Me.mnuViewSeperator1 = New System.Windows.Forms.ToolStripSeparator
		Me._mnuListViewMode_0 = New System.Windows.Forms.ToolStripMenuItem
		Me._mnuListViewMode_1 = New System.Windows.Forms.ToolStripMenuItem
		Me._mnuListViewMode_2 = New System.Windows.Forms.ToolStripMenuItem
		Me.mnuViewSeperator2 = New System.Windows.Forms.ToolStripSeparator
		Me.mnuViewRefresh = New System.Windows.Forms.ToolStripMenuItem
		Me.mnuHelp = New System.Windows.Forms.ToolStripMenuItem
		Me.mnuHelpPrintViewer = New System.Windows.Forms.ToolStripMenuItem
		Me.mnuHelpSeperator = New System.Windows.Forms.ToolStripSeparator
		Me.mnuHelpAbout = New System.Windows.Forms.ToolStripMenuItem
		Me.mnuPopUp = New System.Windows.Forms.ToolStripMenuItem
		Me.mnuPopSeperator = New System.Windows.Forms.ToolStripSeparator
		Me.mnuPopPrint = New System.Windows.Forms.ToolStripMenuItem
		Me.mnuPopMedia = New System.Windows.Forms.ToolStripMenuItem
		Me.mnuPopSeperator2 = New System.Windows.Forms.ToolStripSeparator
		Me.mnuPopSearch = New System.Windows.Forms.ToolStripMenuItem
		Me.mnuPopSeperator3 = New System.Windows.Forms.ToolStripSeparator
		Me.mnuPopDelete = New System.Windows.Forms.ToolStripMenuItem
		Me.picStatusInfo.SuspendLayout()
		Me.fraStatusInfo.SuspendLayout()
		Me.picTitles.SuspendLayout()
		Me.picSearch.SuspendLayout()
		Me.fraSearch.SuspendLayout()
		Me.tbToolBar.SuspendLayout()
		Me.sbStatusBar.SuspendLayout()
		Me.MainMenu1.SuspendLayout()
		Me.SuspendLayout()
		Me.ToolTip1.Active = True
		CType(Me.dtpTo, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.dtpFrom, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.lblTitle, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.mnuListViewMode, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.Text = "File Print Viewer"
		Me.ClientSize = New System.Drawing.Size(792, 554)
		Me.Location = New System.Drawing.Point(11, 49)
		Me.Icon = CType(resources.GetObject("frmSearch.Icon"), System.Drawing.Icon)
		Me.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultLocation
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
		Me.Name = "frmSearch"
		Me.picHSplitter.BackColor = System.Drawing.Color.FromARGB(128, 128, 128)
		Me.picHSplitter.Size = New System.Drawing.Size(719, 7)
		Me.picHSplitter.Location = New System.Drawing.Point(16, 376)
		Me.picHSplitter.TabIndex = 13
		Me.picHSplitter.Visible = False
		Me.picHSplitter.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.picHSplitter.Dock = System.Windows.Forms.DockStyle.None
		Me.picHSplitter.CausesValidation = True
		Me.picHSplitter.Enabled = True
		Me.picHSplitter.ForeColor = System.Drawing.SystemColors.ControlText
		Me.picHSplitter.Cursor = System.Windows.Forms.Cursors.Default
		Me.picHSplitter.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.picHSplitter.TabStop = True
		Me.picHSplitter.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Normal
		Me.picHSplitter.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.picHSplitter.Name = "picHSplitter"
		Me.picStatusInfo.Size = New System.Drawing.Size(361, 297)
		Me.picStatusInfo.Location = New System.Drawing.Point(376, 72)
		Me.picStatusInfo.TabIndex = 26
		Me.picStatusInfo.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.picStatusInfo.Dock = System.Windows.Forms.DockStyle.None
		Me.picStatusInfo.BackColor = System.Drawing.SystemColors.Control
		Me.picStatusInfo.CausesValidation = True
		Me.picStatusInfo.Enabled = True
		Me.picStatusInfo.ForeColor = System.Drawing.SystemColors.ControlText
		Me.picStatusInfo.Cursor = System.Windows.Forms.Cursors.Default
		Me.picStatusInfo.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.picStatusInfo.TabStop = True
		Me.picStatusInfo.Visible = True
		Me.picStatusInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.picStatusInfo.Name = "picStatusInfo"
		Me.fraStatusInfo.BackColor = System.Drawing.SystemColors.InactiveBorder
		Me.fraStatusInfo.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.fraStatusInfo.Size = New System.Drawing.Size(181, 191)
		Me.fraStatusInfo.Location = New System.Drawing.Point(80, 40)
		Me.fraStatusInfo.TabIndex = 27
		Me.fraStatusInfo.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.fraStatusInfo.Enabled = True
		Me.fraStatusInfo.ForeColor = System.Drawing.SystemColors.ControlText
		Me.fraStatusInfo.Cursor = System.Windows.Forms.Cursors.Default
		Me.fraStatusInfo.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.fraStatusInfo.Visible = True
		Me.fraStatusInfo.Name = "fraStatusInfo"
		Me.txFaultFileStatus.Size = New System.Drawing.Size(121, 121)
		Me.txFaultFileStatus.Location = New System.Drawing.Point(40, 48)
		Me.txFaultFileStatus.TabIndex = 29
		Me.txFaultFileStatus.Enabled = True
		Me.txFaultFileStatus.ReadOnly = True
		Me.txFaultFileStatus.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical
		Me.txFaultFileStatus.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Both
		Me.txFaultFileStatus.RTF = resources.GetString("txFaultFileStatus.TextRTF")
		Me.txFaultFileStatus.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txFaultFileStatus.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.txFaultFileStatus.Name = "txFaultFileStatus"
		Me.tiDelay.Enabled = False
		Me.tiDelay.Interval = 1
		Me.picVSplitter.BackColor = System.Drawing.Color.FromARGB(128, 128, 128)
		Me.picVSplitter.Size = New System.Drawing.Size(7, 293)
		Me.picVSplitter.Location = New System.Drawing.Point(358, 73)
		Me.picVSplitter.TabIndex = 0
		Me.picVSplitter.Visible = False
		Me.picVSplitter.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.picVSplitter.Dock = System.Windows.Forms.DockStyle.None
		Me.picVSplitter.CausesValidation = True
		Me.picVSplitter.Enabled = True
		Me.picVSplitter.ForeColor = System.Drawing.SystemColors.ControlText
		Me.picVSplitter.Cursor = System.Windows.Forms.Cursors.Default
		Me.picVSplitter.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.picVSplitter.TabStop = True
		Me.picVSplitter.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Normal
		Me.picVSplitter.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.picVSplitter.Name = "picVSplitter"
		Me.picTitles.Dock = System.Windows.Forms.DockStyle.Top
		Me.picTitles.ForeColor = System.Drawing.SystemColors.WindowText
		Me.picTitles.Size = New System.Drawing.Size(792, 20)
		Me.picTitles.Location = New System.Drawing.Point(0, 44)
		Me.picTitles.TabIndex = 23
		Me.picTitles.TabStop = False
		Me.picTitles.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.picTitles.BackColor = System.Drawing.SystemColors.Control
		Me.picTitles.CausesValidation = True
		Me.picTitles.Enabled = True
		Me.picTitles.Cursor = System.Windows.Forms.Cursors.Default
		Me.picTitles.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.picTitles.Visible = True
		Me.picTitles.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.picTitles.Name = "picTitles"
		Me._lblTitle_1.Text = "APS Names:"
		Me._lblTitle_1.Size = New System.Drawing.Size(158, 18)
		Me._lblTitle_1.Location = New System.Drawing.Point(103, 1)
		Me._lblTitle_1.TabIndex = 25
		Me._lblTitle_1.Tag = "ListView:"
		Me._lblTitle_1.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me._lblTitle_1.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me._lblTitle_1.BackColor = System.Drawing.SystemColors.Control
		Me._lblTitle_1.Enabled = True
		Me._lblTitle_1.ForeColor = System.Drawing.SystemColors.ControlText
		Me._lblTitle_1.Cursor = System.Windows.Forms.Cursors.Default
		Me._lblTitle_1.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me._lblTitle_1.UseMnemonic = True
		Me._lblTitle_1.Visible = True
		Me._lblTitle_1.AutoSize = False
		Me._lblTitle_1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me._lblTitle_1.Name = "_lblTitle_1"
		Me._lblTitle_0.Size = New System.Drawing.Size(99, 18)
		Me._lblTitle_0.Location = New System.Drawing.Point(0, 1)
		Me._lblTitle_0.TabIndex = 24
		Me._lblTitle_0.Tag = "TreeView:"
		Me._lblTitle_0.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me._lblTitle_0.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me._lblTitle_0.BackColor = System.Drawing.SystemColors.Control
		Me._lblTitle_0.Enabled = True
		Me._lblTitle_0.ForeColor = System.Drawing.SystemColors.ControlText
		Me._lblTitle_0.Cursor = System.Windows.Forms.Cursors.Default
		Me._lblTitle_0.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me._lblTitle_0.UseMnemonic = True
		Me._lblTitle_0.Visible = True
		Me._lblTitle_0.AutoSize = False
		Me._lblTitle_0.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me._lblTitle_0.Name = "_lblTitle_0"
		Me.picSearch.Size = New System.Drawing.Size(193, 337)
		Me.picSearch.Location = New System.Drawing.Point(0, 416)
		Me.picSearch.TabIndex = 14
		Me.picSearch.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.picSearch.Dock = System.Windows.Forms.DockStyle.None
		Me.picSearch.BackColor = System.Drawing.SystemColors.Control
		Me.picSearch.CausesValidation = True
		Me.picSearch.Enabled = True
		Me.picSearch.ForeColor = System.Drawing.SystemColors.ControlText
		Me.picSearch.Cursor = System.Windows.Forms.Cursors.Default
		Me.picSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.picSearch.TabStop = True
		Me.picSearch.Visible = True
		Me.picSearch.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.picSearch.Name = "picSearch"
		Me.fraSearch.BackColor = System.Drawing.SystemColors.highlightText
		Me.fraSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.fraSearch.Size = New System.Drawing.Size(193, 329)
		Me.fraSearch.Location = New System.Drawing.Point(0, 0)
		Me.fraSearch.TabIndex = 15
		Me.fraSearch.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.fraSearch.Enabled = True
		Me.fraSearch.ForeColor = System.Drawing.SystemColors.ControlText
		Me.fraSearch.Cursor = System.Windows.Forms.Cursors.Default
		Me.fraSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.fraSearch.Visible = True
		Me.fraSearch.Name = "fraSearch"
		Me.cmbERONumber.Size = New System.Drawing.Size(183, 21)
		Me.cmbERONumber.Location = New System.Drawing.Point(4, 144)
		Me.cmbERONumber.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cmbERONumber.TabIndex = 7
		Me.cmbERONumber.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmbERONumber.BackColor = System.Drawing.SystemColors.Window
		Me.cmbERONumber.CausesValidation = True
		Me.cmbERONumber.Enabled = True
		Me.cmbERONumber.ForeColor = System.Drawing.SystemColors.WindowText
		Me.cmbERONumber.IntegralHeight = True
		Me.cmbERONumber.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmbERONumber.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmbERONumber.Sorted = False
		Me.cmbERONumber.TabStop = True
		Me.cmbERONumber.Visible = True
		Me.cmbERONumber.Name = "cmbERONumber"
		Me.cmbSerialNumber.Size = New System.Drawing.Size(183, 21)
		Me.cmbSerialNumber.Location = New System.Drawing.Point(4, 104)
		Me.cmbSerialNumber.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cmbSerialNumber.TabIndex = 6
		Me.cmbSerialNumber.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmbSerialNumber.BackColor = System.Drawing.SystemColors.Window
		Me.cmbSerialNumber.CausesValidation = True
		Me.cmbSerialNumber.Enabled = True
		Me.cmbSerialNumber.ForeColor = System.Drawing.SystemColors.WindowText
		Me.cmbSerialNumber.IntegralHeight = True
		Me.cmbSerialNumber.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmbSerialNumber.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmbSerialNumber.Sorted = False
		Me.cmbSerialNumber.TabStop = True
		Me.cmbSerialNumber.Visible = True
		Me.cmbSerialNumber.Name = "cmbSerialNumber"
		Me.cmbTPSName.Size = New System.Drawing.Size(183, 21)
		Me.cmbTPSName.Location = New System.Drawing.Point(4, 64)
		Me.cmbTPSName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cmbTPSName.TabIndex = 5
		Me.cmbTPSName.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmbTPSName.BackColor = System.Drawing.SystemColors.Window
		Me.cmbTPSName.CausesValidation = True
		Me.cmbTPSName.Enabled = True
		Me.cmbTPSName.ForeColor = System.Drawing.SystemColors.WindowText
		Me.cmbTPSName.IntegralHeight = True
		Me.cmbTPSName.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmbTPSName.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmbTPSName.Sorted = False
		Me.cmbTPSName.TabStop = True
		Me.cmbTPSName.Visible = True
		Me.cmbTPSName.Name = "cmbTPSName"
		Me.cmbAPSName.Size = New System.Drawing.Size(183, 21)
		Me.cmbAPSName.Location = New System.Drawing.Point(4, 24)
		Me.cmbAPSName.Sorted = True
		Me.cmbAPSName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cmbAPSName.TabIndex = 4
		Me.cmbAPSName.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmbAPSName.BackColor = System.Drawing.SystemColors.Window
		Me.cmbAPSName.CausesValidation = True
		Me.cmbAPSName.Enabled = True
		Me.cmbAPSName.ForeColor = System.Drawing.SystemColors.WindowText
		Me.cmbAPSName.IntegralHeight = True
		Me.cmbAPSName.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmbAPSName.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmbAPSName.TabStop = True
		Me.cmbAPSName.Visible = True
		Me.cmbAPSName.Name = "cmbAPSName"
		Me.cmbSearchNow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		Me.cmbSearchNow.Text = "&Search Now"
		Me.cmbSearchNow.Size = New System.Drawing.Size(79, 25)
		Me.cmbSearchNow.Location = New System.Drawing.Point(4, 232)
		Me.cmbSearchNow.TabIndex = 10
		Me.cmbSearchNow.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmbSearchNow.BackColor = System.Drawing.SystemColors.Control
		Me.cmbSearchNow.CausesValidation = True
		Me.cmbSearchNow.Enabled = True
		Me.cmbSearchNow.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmbSearchNow.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmbSearchNow.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmbSearchNow.TabStop = True
		Me.cmbSearchNow.Name = "cmbSearchNow"
		Me.cmbNewSearch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		Me.cmbNewSearch.Text = "&New Search"
		Me.cmbNewSearch.Size = New System.Drawing.Size(79, 25)
		Me.cmbNewSearch.Location = New System.Drawing.Point(104, 232)
		Me.cmbNewSearch.TabIndex = 11
		Me.cmbNewSearch.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmbNewSearch.BackColor = System.Drawing.SystemColors.Control
		Me.cmbNewSearch.CausesValidation = True
		Me.cmbNewSearch.Enabled = True
		Me.cmbNewSearch.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmbNewSearch.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmbNewSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmbNewSearch.TabStop = True
		Me.cmbNewSearch.Name = "cmbNewSearch"
		dtpTo.OcxState = CType(resources.GetObject("dtpTo.OcxState"), System.Windows.Forms.AxHost.State)
		Me.dtpTo.Size = New System.Drawing.Size(79, 25)
		Me.dtpTo.Location = New System.Drawing.Point(104, 200)
		Me.dtpTo.TabIndex = 9
		Me.dtpTo.Name = "dtpTo"
		dtpFrom.OcxState = CType(resources.GetObject("dtpFrom.OcxState"), System.Windows.Forms.AxHost.State)
		Me.dtpFrom.Size = New System.Drawing.Size(79, 25)
		Me.dtpFrom.Location = New System.Drawing.Point(4, 200)
		Me.dtpFrom.TabIndex = 8
		Me.dtpFrom.Name = "dtpFrom"
		Me.lblDate.TextAlign = System.Drawing.ContentAlignment.TopCenter
		Me.lblDate.BackColor = System.Drawing.SystemColors.Window
		Me.lblDate.Text = "Date"
		Me.lblDate.ForeColor = System.Drawing.SystemColors.WindowText
		Me.lblDate.Size = New System.Drawing.Size(183, 17)
		Me.lblDate.Location = New System.Drawing.Point(4, 168)
		Me.lblDate.TabIndex = 22
		Me.lblDate.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblDate.Enabled = True
		Me.lblDate.Cursor = System.Windows.Forms.Cursors.Default
		Me.lblDate.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.lblDate.UseMnemonic = True
		Me.lblDate.Visible = True
		Me.lblDate.AutoSize = False
		Me.lblDate.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.lblDate.Name = "lblDate"
		Me.lblEro.TextAlign = System.Drawing.ContentAlignment.TopCenter
		Me.lblEro.BackColor = System.Drawing.SystemColors.Window
		Me.lblEro.Text = "ERO Number"
		Me.lblEro.ForeColor = System.Drawing.SystemColors.WindowText
		Me.lblEro.Size = New System.Drawing.Size(183, 17)
		Me.lblEro.Location = New System.Drawing.Point(4, 128)
		Me.lblEro.TabIndex = 21
		Me.lblEro.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblEro.Enabled = True
		Me.lblEro.Cursor = System.Windows.Forms.Cursors.Default
		Me.lblEro.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.lblEro.UseMnemonic = True
		Me.lblEro.Visible = True
		Me.lblEro.AutoSize = False
		Me.lblEro.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.lblEro.Name = "lblEro"
		Me.lblSerial.TextAlign = System.Drawing.ContentAlignment.TopCenter
		Me.lblSerial.BackColor = System.Drawing.SystemColors.Window
		Me.lblSerial.Text = "Serial Number"
		Me.lblSerial.ForeColor = System.Drawing.SystemColors.WindowText
		Me.lblSerial.Size = New System.Drawing.Size(183, 17)
		Me.lblSerial.Location = New System.Drawing.Point(4, 88)
		Me.lblSerial.TabIndex = 20
		Me.lblSerial.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblSerial.Enabled = True
		Me.lblSerial.Cursor = System.Windows.Forms.Cursors.Default
		Me.lblSerial.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.lblSerial.UseMnemonic = True
		Me.lblSerial.Visible = True
		Me.lblSerial.AutoSize = False
		Me.lblSerial.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.lblSerial.Name = "lblSerial"
		Me.lblTps.TextAlign = System.Drawing.ContentAlignment.TopCenter
		Me.lblTps.BackColor = System.Drawing.SystemColors.Window
		Me.lblTps.Text = "TPS Name"
		Me.lblTps.ForeColor = System.Drawing.SystemColors.WindowText
		Me.lblTps.Size = New System.Drawing.Size(183, 17)
		Me.lblTps.Location = New System.Drawing.Point(4, 48)
		Me.lblTps.TabIndex = 19
		Me.lblTps.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblTps.Enabled = True
		Me.lblTps.Cursor = System.Windows.Forms.Cursors.Default
		Me.lblTps.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.lblTps.UseMnemonic = True
		Me.lblTps.Visible = True
		Me.lblTps.AutoSize = False
		Me.lblTps.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.lblTps.Name = "lblTps"
		Me.lblAps.TextAlign = System.Drawing.ContentAlignment.TopCenter
		Me.lblAps.BackColor = System.Drawing.SystemColors.Window
		Me.lblAps.Text = "APS Name"
		Me.lblAps.ForeColor = System.Drawing.SystemColors.WindowText
		Me.lblAps.Size = New System.Drawing.Size(183, 17)
		Me.lblAps.Location = New System.Drawing.Point(4, 8)
		Me.lblAps.TabIndex = 18
		Me.lblAps.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblAps.Enabled = True
		Me.lblAps.Cursor = System.Windows.Forms.Cursors.Default
		Me.lblAps.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.lblAps.UseMnemonic = True
		Me.lblAps.Visible = True
		Me.lblAps.AutoSize = False
		Me.lblAps.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.lblAps.Name = "lblAps"
		Me.lblTo.TextAlign = System.Drawing.ContentAlignment.TopCenter
		Me.lblTo.BackColor = System.Drawing.SystemColors.Window
		Me.lblTo.Text = "To"
		Me.lblTo.ForeColor = System.Drawing.SystemColors.WindowText
		Me.lblTo.Size = New System.Drawing.Size(79, 17)
		Me.lblTo.Location = New System.Drawing.Point(104, 184)
		Me.lblTo.TabIndex = 17
		Me.lblTo.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblTo.Enabled = True
		Me.lblTo.Cursor = System.Windows.Forms.Cursors.Default
		Me.lblTo.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.lblTo.UseMnemonic = True
		Me.lblTo.Visible = True
		Me.lblTo.AutoSize = False
		Me.lblTo.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.lblTo.Name = "lblTo"
		Me.lblFrom.TextAlign = System.Drawing.ContentAlignment.TopCenter
		Me.lblFrom.BackColor = System.Drawing.SystemColors.Window
		Me.lblFrom.Text = "From"
		Me.lblFrom.ForeColor = System.Drawing.SystemColors.WindowText
		Me.lblFrom.Size = New System.Drawing.Size(79, 13)
		Me.lblFrom.Location = New System.Drawing.Point(4, 184)
		Me.lblFrom.TabIndex = 16
		Me.lblFrom.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblFrom.Enabled = True
		Me.lblFrom.Cursor = System.Windows.Forms.Cursors.Default
		Me.lblFrom.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.lblFrom.UseMnemonic = True
		Me.lblFrom.Visible = True
		Me.lblFrom.AutoSize = False
		Me.lblFrom.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.lblFrom.Name = "lblFrom"
		Me.lvSearchList.Size = New System.Drawing.Size(417, 313)
		Me.lvSearchList.Location = New System.Drawing.Point(200, 424)
		Me.lvSearchList.TabIndex = 12
		Me.lvSearchList.LabelEdit = False
		Me.lvSearchList.MultiSelect = True
		Me.lvSearchList.LabelWrap = True
		Me.lvSearchList.HideSelection = False
		Me.lvSearchList.LargeImageList = imlIcons
		Me.lvSearchList.SmallImageList = tvImages
		Me.lvSearchList.ForeColor = System.Drawing.SystemColors.WindowText
		Me.lvSearchList.BackColor = System.Drawing.SystemColors.Window
		Me.lvSearchList.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lvSearchList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.lvSearchList.Name = "lvSearchList"
		Me.imlIcons.ImageSize = New System.Drawing.Size(32, 32)
		Me.imlIcons.TransparentColor = System.Drawing.Color.FromARGB(192, 192, 192)
		Me.imlIcons.ImageStream = CType(resources.GetObject("imlIcons.ImageStream"), System.Windows.Forms.ImageListStreamer)
		Me.imlIcons.Images.SetKeyName(0, "closed")
		Me.imlIcons.Images.SetKeyName(1, "open")
		Me.imlIcons.Images.SetKeyName(2, "closed1")
		Me.imlIcons.Images.SetKeyName(3, "file")
		Me.lvDetail.Size = New System.Drawing.Size(222, 299)
		Me.lvDetail.Location = New System.Drawing.Point(120, 72)
		Me.lvDetail.TabIndex = 2
		Me.lvDetail.Alignment = System.Windows.Forms.ListViewAlignment.Left
		Me.lvDetail.LabelEdit = False
		Me.lvDetail.MultiSelect = True
		Me.lvDetail.LabelWrap = True
		Me.lvDetail.HideSelection = False
		Me.lvDetail.AllowColumnReorder = -1
		Me.lvDetail.LargeImageList = imlIcons
		Me.lvDetail.SmallImageList = tvImages
		Me.lvDetail.ForeColor = System.Drawing.SystemColors.WindowText
		Me.lvDetail.BackColor = System.Drawing.SystemColors.Window
		Me.lvDetail.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lvDetail.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.lvDetail.Name = "lvDetail"
		Me.tvImages.ImageSize = New System.Drawing.Size(13, 13)
		Me.tvImages.TransparentColor = System.Drawing.Color.FromARGB(192, 192, 192)
		Me.tvImages.ImageStream = CType(resources.GetObject("tvImages.ImageStream"), System.Windows.Forms.ImageListStreamer)
		Me.tvImages.Images.SetKeyName(0, "closed")
		Me.tvImages.Images.SetKeyName(1, "open")
		Me.tvImages.Images.SetKeyName(2, "file")
		Me.tbToolBar.ShowItemToolTips = True
		Me.tbToolBar.Dock = System.Windows.Forms.DockStyle.Top
		Me.tbToolBar.Size = New System.Drawing.Size(792, 44)
		Me.tbToolBar.Location = New System.Drawing.Point(0, 0)
		Me.tbToolBar.TabIndex = 3
		Me.tbToolBar.ImageList = imlToolbarIcons
		Me.tbToolBar.Name = "tbToolBar"
		Me._tbToolBar_Button1.Size = New System.Drawing.Size(40, 39)
		Me._tbToolBar_Button1.AutoSize = False
		Me._tbToolBar_Button1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText
		Me._tbToolBar_Button1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
		Me._tbToolBar_Button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
		Me._tbToolBar_Button2.Size = New System.Drawing.Size(40, 39)
		Me._tbToolBar_Button2.AutoSize = False
		Me._tbToolBar_Button2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText
		Me._tbToolBar_Button2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
		Me._tbToolBar_Button2.Name = "Print"
		Me._tbToolBar_Button2.ToolTipText = "Print"
		Me._tbToolBar_Button2.ImageKey = "Print"
		Me._tbToolBar_Button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
		Me._tbToolBar_Button3.Size = New System.Drawing.Size(40, 39)
		Me._tbToolBar_Button3.AutoSize = False
		Me._tbToolBar_Button3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText
		Me._tbToolBar_Button3.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
		Me._tbToolBar_Button3.Name = "Font"
		Me._tbToolBar_Button3.ToolTipText = "Font"
		Me._tbToolBar_Button3.ImageKey = "Font"
		Me._tbToolBar_Button3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
		Me._tbToolBar_Button4.Size = New System.Drawing.Size(40, 39)
		Me._tbToolBar_Button4.AutoSize = False
		Me._tbToolBar_Button4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText
		Me._tbToolBar_Button4.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
		Me._tbToolBar_Button4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
		Me._tbToolBar_Button5.Size = New System.Drawing.Size(40, 39)
		Me._tbToolBar_Button5.AutoSize = False
		Me._tbToolBar_Button5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText
		Me._tbToolBar_Button5.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
		Me._tbToolBar_Button5.Name = "Delete"
		Me._tbToolBar_Button5.ToolTipText = "Delete"
		Me._tbToolBar_Button5.ImageKey = "Delete"
		Me._tbToolBar_Button5.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
		Me._tbToolBar_Button6.Size = New System.Drawing.Size(40, 39)
		Me._tbToolBar_Button6.AutoSize = False
		Me._tbToolBar_Button6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText
		Me._tbToolBar_Button6.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
		Me._tbToolBar_Button6.Name = "Search"
		Me._tbToolBar_Button6.ToolTipText = "Search"
		Me._tbToolBar_Button6.ImageKey = "Search"
		Me._tbToolBar_Button6.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
		Me._tbToolBar_Button7.Size = New System.Drawing.Size(40, 39)
		Me._tbToolBar_Button7.AutoSize = False
		Me._tbToolBar_Button7.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText
		Me._tbToolBar_Button7.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
		Me._tbToolBar_Button7.Name = "Print_To"
		Me._tbToolBar_Button7.ToolTipText = "Send To Removable Media"
		Me._tbToolBar_Button7.ImageKey = "Print_To"
		Me._tbToolBar_Button7.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
		Me._tbToolBar_Button8.Size = New System.Drawing.Size(40, 39)
		Me._tbToolBar_Button8.AutoSize = False
		Me._tbToolBar_Button8.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText
		Me._tbToolBar_Button8.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
		Me._tbToolBar_Button8.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
		Me._tbToolBar_Button9.Size = New System.Drawing.Size(40, 39)
		Me._tbToolBar_Button9.AutoSize = False
		Me._tbToolBar_Button9.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText
		Me._tbToolBar_Button9.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
		Me._tbToolBar_Button9.Name = "Refresh"
		Me._tbToolBar_Button9.ToolTipText = "Refresh"
		Me._tbToolBar_Button9.ImageKey = "refresh"
		Me._tbToolBar_Button9.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
		Me._tbToolBar_Button10.Size = New System.Drawing.Size(40, 39)
		Me._tbToolBar_Button10.AutoSize = False
		Me._tbToolBar_Button10.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText
		Me._tbToolBar_Button10.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
		Me._tbToolBar_Button10.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
		Me._tbToolBar_Button11.Size = New System.Drawing.Size(40, 39)
		Me._tbToolBar_Button11.AutoSize = False
		Me._tbToolBar_Button11.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText
		Me._tbToolBar_Button11.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
		Me._tbToolBar_Button11.Name = "View Large Icons"
		Me._tbToolBar_Button11.ToolTipText = "Large Icons"
		Me._tbToolBar_Button11.ImageKey = "Large_Icons"
		Me._tbToolBar_Button11.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
		Me._tbToolBar_Button12.Size = New System.Drawing.Size(40, 39)
		Me._tbToolBar_Button12.AutoSize = False
		Me._tbToolBar_Button12.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText
		Me._tbToolBar_Button12.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
		Me._tbToolBar_Button12.Name = "View Small Icons"
		Me._tbToolBar_Button12.ToolTipText = "Small Icons"
		Me._tbToolBar_Button12.ImageKey = "Small_Icons"
		Me._tbToolBar_Button12.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
		Me._tbToolBar_Button13.Size = New System.Drawing.Size(40, 39)
		Me._tbToolBar_Button13.AutoSize = False
		Me._tbToolBar_Button13.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText
		Me._tbToolBar_Button13.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
		Me._tbToolBar_Button13.Name = "View List"
		Me._tbToolBar_Button13.ToolTipText = "List"
		Me._tbToolBar_Button13.ImageKey = "List"
		Me._tbToolBar_Button13.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
		Me.tvTrack.CausesValidation = True
		Me.tvTrack.Size = New System.Drawing.Size(115, 296)
		Me.tvTrack.Location = New System.Drawing.Point(0, 72)
		Me.tvTrack.TabIndex = 1
		Me.tvTrack.HideSelection = False
		Me.tvTrack.Indent = 18
		Me.tvTrack.LabelEdit = False
		Me.tvTrack.Sorted = True
		Me.tvTrack.ImageList = tvImages
		Me.tvTrack.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.tvTrack.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.tvTrack.Name = "tvTrack"
		Me.imlToolbarIcons.ImageSize = New System.Drawing.Size(32, 32)
		Me.imlToolbarIcons.TransparentColor = System.Drawing.Color.FromARGB(192, 192, 192)
		Me.imlToolbarIcons.ImageStream = CType(resources.GetObject("imlToolbarIcons.ImageStream"), System.Windows.Forms.ImageListStreamer)
		Me.imlToolbarIcons.Images.SetKeyName(0, "Search")
		Me.imlToolbarIcons.Images.SetKeyName(1, "Font")
		Me.imlToolbarIcons.Images.SetKeyName(2, "Print_To")
		Me.imlToolbarIcons.Images.SetKeyName(3, "Print")
		Me.imlToolbarIcons.Images.SetKeyName(4, "Delete")
		Me.imlToolbarIcons.Images.SetKeyName(5, "refresh")
		Me.imlToolbarIcons.Images.SetKeyName(6, "Large_Icons")
		Me.imlToolbarIcons.Images.SetKeyName(7, "Small_Icons")
		Me.imlToolbarIcons.Images.SetKeyName(8, "List")
		Me.sbStatusBar.Dock = System.Windows.Forms.DockStyle.Bottom
		Me.sbStatusBar.Size = New System.Drawing.Size(792, 17)
		Me.sbStatusBar.Location = New System.Drawing.Point(0, 537)
		Me.sbStatusBar.TabIndex = 28
		Me.sbStatusBar.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.sbStatusBar.Name = "sbStatusBar"
		Me._sbStatusBar_Panel1.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
		Me._sbStatusBar_Panel1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
		Me._sbStatusBar_Panel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me._sbStatusBar_Panel1.Size = New System.Drawing.Size(581, 17)
		Me._sbStatusBar_Panel1.Text = "Status"
		Me._sbStatusBar_Panel1.Spring = True
		Me._sbStatusBar_Panel1.AutoSize = True
		Me._sbStatusBar_Panel1.BorderSides = CType(System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom, System.Windows.Forms.ToolStripStatusLabelBorderSides)
		Me._sbStatusBar_Panel1.Margin = New System.Windows.Forms.Padding(0)
		Me._sbStatusBar_Panel1.AutoSize = False
		Me._sbStatusBar_Panel2.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
		Me._sbStatusBar_Panel2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
		Me._sbStatusBar_Panel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me._sbStatusBar_Panel2.AutoSize = True
		Me._sbStatusBar_Panel2.BorderSides = CType(System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom, System.Windows.Forms.ToolStripStatusLabelBorderSides)
		Me._sbStatusBar_Panel2.Margin = New System.Windows.Forms.Padding(0)
		Me._sbStatusBar_Panel2.Size = New System.Drawing.Size(96, 17)
		Me._sbStatusBar_Panel2.AutoSize = False
		Me._sbStatusBar_Panel3.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
		Me._sbStatusBar_Panel3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
		Me._sbStatusBar_Panel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me._sbStatusBar_Panel3.AutoSize = True
		Me._sbStatusBar_Panel3.BorderSides = CType(System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom, System.Windows.Forms.ToolStripStatusLabelBorderSides)
		Me._sbStatusBar_Panel3.Margin = New System.Windows.Forms.Padding(0)
		Me._sbStatusBar_Panel3.Size = New System.Drawing.Size(96, 17)
		Me._sbStatusBar_Panel3.AutoSize = False
		Me.imgHSplitter.Size = New System.Drawing.Size(743, 7)
		Me.imgHSplitter.Location = New System.Drawing.Point(0, 400)
		Me.imgHSplitter.Cursor = System.Windows.Forms.Cursors.SizeNS
		Me.imgHSplitter.Enabled = True
		Me.imgHSplitter.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Normal
		Me.imgHSplitter.Visible = True
		Me.imgHSplitter.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.imgHSplitter.Name = "imgHSplitter"
		Me.imgVSplitter.Size = New System.Drawing.Size(7, 239)
		Me.imgVSplitter.Location = New System.Drawing.Point(344, 80)
		Me.imgVSplitter.Cursor = System.Windows.Forms.Cursors.SizeWE
		Me.imgVSplitter.Enabled = True
		Me.imgVSplitter.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Normal
		Me.imgVSplitter.Visible = True
		Me.imgVSplitter.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.imgVSplitter.Name = "imgVSplitter"
		Me.mnuFile.Name = "mnuFile"
		Me.mnuFile.Text = "&File"
		Me.mnuFile.Checked = False
		Me.mnuFile.Enabled = True
		Me.mnuFile.Visible = True
		Me.mnuFilePrint.Name = "mnuFilePrint"
		Me.mnuFilePrint.Text = "&Print"
		Me.mnuFilePrint.ShortcutKeys = CType(System.Windows.Forms.Keys.Control or System.Windows.Forms.Keys.P, System.Windows.Forms.Keys)
		Me.mnuFilePrint.Checked = False
		Me.mnuFilePrint.Enabled = True
		Me.mnuFilePrint.Visible = True
		Me.mnuFileSeperator1.Enabled = True
		Me.mnuFileSeperator1.Visible = True
		Me.mnuFileSeperator1.Name = "mnuFileSeperator1"
		Me.mnuFileDelete.Name = "mnuFileDelete"
		Me.mnuFileDelete.Text = "&Delete"
		Me.mnuFileDelete.ShortcutKeys = CType(System.Windows.Forms.Keys.Control or System.Windows.Forms.Keys.D, System.Windows.Forms.Keys)
		Me.mnuFileDelete.Checked = False
		Me.mnuFileDelete.Enabled = True
		Me.mnuFileDelete.Visible = True
		Me.mnuFileSearch.Name = "mnuFileSearch"
		Me.mnuFileSearch.Text = "&Search"
		Me.mnuFileSearch.ShortcutKeys = CType(System.Windows.Forms.Keys.Control or System.Windows.Forms.Keys.S, System.Windows.Forms.Keys)
		Me.mnuFileSearch.Checked = False
		Me.mnuFileSearch.Enabled = True
		Me.mnuFileSearch.Visible = True
		Me.mnuFileSendToFloppy.Name = "mnuFileSendToFloppy"
		Me.mnuFileSendToFloppy.Text = "Send to Removeable &Media"
		Me.mnuFileSendToFloppy.ShortcutKeys = CType(System.Windows.Forms.Keys.Control or System.Windows.Forms.Keys.F, System.Windows.Forms.Keys)
		Me.mnuFileSendToFloppy.Checked = False
		Me.mnuFileSendToFloppy.Enabled = True
		Me.mnuFileSendToFloppy.Visible = True
		Me.mnuFileSeperator.Enabled = True
		Me.mnuFileSeperator.Visible = True
		Me.mnuFileSeperator.Name = "mnuFileSeperator"
		Me.mnuFileExit.Name = "mnuFileExit"
		Me.mnuFileExit.Text = "E&xit"
		Me.mnuFileExit.Checked = False
		Me.mnuFileExit.Enabled = True
		Me.mnuFileExit.Visible = True
		Me.mnuEdit.Name = "mnuEdit"
		Me.mnuEdit.Text = "&Edit"
		Me.mnuEdit.Checked = False
		Me.mnuEdit.Enabled = True
		Me.mnuEdit.Visible = True
		Me.mnuFileFont.Name = "mnuFileFont"
		Me.mnuFileFont.Text = "&Font"
		Me.mnuFileFont.ShortcutKeys = CType(System.Windows.Forms.Keys.Control or System.Windows.Forms.Keys.G, System.Windows.Forms.Keys)
		Me.mnuFileFont.Checked = False
		Me.mnuFileFont.Enabled = True
		Me.mnuFileFont.Visible = True
		Me.mnuEditSelectAll.Name = "mnuEditSelectAll"
		Me.mnuEditSelectAll.Text = "&Select All"
		Me.mnuEditSelectAll.ShortcutKeys = CType(System.Windows.Forms.Keys.Control or System.Windows.Forms.Keys.A, System.Windows.Forms.Keys)
		Me.mnuEditSelectAll.Checked = False
		Me.mnuEditSelectAll.Enabled = True
		Me.mnuEditSelectAll.Visible = True
		Me.mnuView.Name = "mnuView"
		Me.mnuView.Text = "&View"
		Me.mnuView.Checked = False
		Me.mnuView.Enabled = True
		Me.mnuView.Visible = True
		Me.mnuViewToolBar.Name = "mnuViewToolBar"
		Me.mnuViewToolBar.Text = "&Tool Bar"
		Me.mnuViewToolBar.Checked = True
		Me.mnuViewToolBar.Enabled = True
		Me.mnuViewToolBar.Visible = True
		Me.mnuViewStatusBar.Name = "mnuViewStatusBar"
		Me.mnuViewStatusBar.Text = "Status &Bar"
		Me.mnuViewStatusBar.Checked = True
		Me.mnuViewStatusBar.Enabled = True
		Me.mnuViewStatusBar.Visible = True
		Me.mnuViewSeperator1.Enabled = True
		Me.mnuViewSeperator1.Visible = True
		Me.mnuViewSeperator1.Name = "mnuViewSeperator1"
		Me._mnuListViewMode_0.Name = "_mnuListViewMode_0"
		Me._mnuListViewMode_0.Text = "Lar&ge Icons"
		Me._mnuListViewMode_0.Checked = True
		Me._mnuListViewMode_0.Enabled = True
		Me._mnuListViewMode_0.Visible = True
		Me._mnuListViewMode_1.Name = "_mnuListViewMode_1"
		Me._mnuListViewMode_1.Text = "S&mall Icons"
		Me._mnuListViewMode_1.Checked = True
		Me._mnuListViewMode_1.Enabled = True
		Me._mnuListViewMode_1.Visible = True
		Me._mnuListViewMode_2.Name = "_mnuListViewMode_2"
		Me._mnuListViewMode_2.Text = "&List"
		Me._mnuListViewMode_2.Checked = True
		Me._mnuListViewMode_2.Enabled = True
		Me._mnuListViewMode_2.Visible = True
		Me.mnuViewSeperator2.Enabled = True
		Me.mnuViewSeperator2.Visible = True
		Me.mnuViewSeperator2.Name = "mnuViewSeperator2"
		Me.mnuViewRefresh.Name = "mnuViewRefresh"
		Me.mnuViewRefresh.Text = "&Refresh"
		Me.mnuViewRefresh.ShortcutKeys = CType(System.Windows.Forms.Keys.F5, System.Windows.Forms.Keys)
		Me.mnuViewRefresh.Checked = False
		Me.mnuViewRefresh.Enabled = True
		Me.mnuViewRefresh.Visible = True
		Me.mnuHelp.Name = "mnuHelp"
		Me.mnuHelp.Text = "&Help"
		Me.mnuHelp.Checked = False
		Me.mnuHelp.Enabled = True
		Me.mnuHelp.Visible = True
		Me.mnuHelpPrintViewer.Name = "mnuHelpPrintViewer"
		Me.mnuHelpPrintViewer.Text = "&File Print Viewer Information"
		Me.mnuHelpPrintViewer.ShortcutKeys = CType(System.Windows.Forms.Keys.F1, System.Windows.Forms.Keys)
		Me.mnuHelpPrintViewer.Checked = False
		Me.mnuHelpPrintViewer.Enabled = True
		Me.mnuHelpPrintViewer.Visible = True
		Me.mnuHelpSeperator.Enabled = True
		Me.mnuHelpSeperator.Visible = True
		Me.mnuHelpSeperator.Name = "mnuHelpSeperator"
		Me.mnuHelpAbout.Name = "mnuHelpAbout"
		Me.mnuHelpAbout.Text = "&About File Print Viewer"
		Me.mnuHelpAbout.Checked = False
		Me.mnuHelpAbout.Enabled = True
		Me.mnuHelpAbout.Visible = True
		Me.mnuPopUp.Name = "mnuPopUp"
		Me.mnuPopUp.Text = "PopUp"
		Me.mnuPopUp.Visible = False
		Me.mnuPopUp.Checked = False
		Me.mnuPopUp.Enabled = True
		Me.mnuPopSeperator.Enabled = True
		Me.mnuPopSeperator.Visible = True
		Me.mnuPopSeperator.Name = "mnuPopSeperator"
		Me.mnuPopPrint.Name = "mnuPopPrint"
		Me.mnuPopPrint.Text = "&Print"
		Me.mnuPopPrint.Checked = False
		Me.mnuPopPrint.Enabled = True
		Me.mnuPopPrint.Visible = True
		Me.mnuPopMedia.Name = "mnuPopMedia"
		Me.mnuPopMedia.Text = "Send to Removeable &Media"
		Me.mnuPopMedia.Checked = False
		Me.mnuPopMedia.Enabled = True
		Me.mnuPopMedia.Visible = True
		Me.mnuPopSeperator2.Enabled = True
		Me.mnuPopSeperator2.Visible = True
		Me.mnuPopSeperator2.Name = "mnuPopSeperator2"
		Me.mnuPopSearch.Name = "mnuPopSearch"
		Me.mnuPopSearch.Text = "&Search"
		Me.mnuPopSearch.Checked = False
		Me.mnuPopSearch.Enabled = True
		Me.mnuPopSearch.Visible = True
		Me.mnuPopSeperator3.Enabled = True
		Me.mnuPopSeperator3.Visible = True
		Me.mnuPopSeperator3.Name = "mnuPopSeperator3"
		Me.mnuPopDelete.Name = "mnuPopDelete"
		Me.mnuPopDelete.Text = "&Delete"
		Me.mnuPopDelete.Checked = False
		Me.mnuPopDelete.Enabled = True
		Me.mnuPopDelete.Visible = True
		Me.Controls.Add(picHSplitter)
		Me.Controls.Add(picStatusInfo)
		Me.Controls.Add(picVSplitter)
		Me.Controls.Add(picTitles)
		Me.Controls.Add(picSearch)
		Me.Controls.Add(lvSearchList)
		Me.Controls.Add(lvDetail)
		Me.Controls.Add(tbToolBar)
		Me.Controls.Add(tvTrack)
		Me.Controls.Add(sbStatusBar)
		Me.Controls.Add(imgHSplitter)
		Me.Controls.Add(imgVSplitter)
		Me.picStatusInfo.Controls.Add(fraStatusInfo)
		Me.fraStatusInfo.Controls.Add(txFaultFileStatus)
		Me.picTitles.Controls.Add(_lblTitle_1)
		Me.picTitles.Controls.Add(_lblTitle_0)
		Me.picSearch.Controls.Add(fraSearch)
		Me.fraSearch.Controls.Add(cmbERONumber)
		Me.fraSearch.Controls.Add(cmbSerialNumber)
		Me.fraSearch.Controls.Add(cmbTPSName)
		Me.fraSearch.Controls.Add(cmbAPSName)
		Me.fraSearch.Controls.Add(cmbSearchNow)
		Me.fraSearch.Controls.Add(cmbNewSearch)
		Me.fraSearch.Controls.Add(dtpTo)
		Me.fraSearch.Controls.Add(dtpFrom)
		Me.fraSearch.Controls.Add(lblDate)
		Me.fraSearch.Controls.Add(lblEro)
		Me.fraSearch.Controls.Add(lblSerial)
		Me.fraSearch.Controls.Add(lblTps)
		Me.fraSearch.Controls.Add(lblAps)
		Me.fraSearch.Controls.Add(lblTo)
		Me.fraSearch.Controls.Add(lblFrom)
		Me.tbToolBar.Items.Add(_tbToolBar_Button1)
		Me.tbToolBar.Items.Add(_tbToolBar_Button2)
		Me.tbToolBar.Items.Add(_tbToolBar_Button3)
		Me.tbToolBar.Items.Add(_tbToolBar_Button4)
		Me.tbToolBar.Items.Add(_tbToolBar_Button5)
		Me.tbToolBar.Items.Add(_tbToolBar_Button6)
		Me.tbToolBar.Items.Add(_tbToolBar_Button7)
		Me.tbToolBar.Items.Add(_tbToolBar_Button8)
		Me.tbToolBar.Items.Add(_tbToolBar_Button9)
		Me.tbToolBar.Items.Add(_tbToolBar_Button10)
		Me.tbToolBar.Items.Add(_tbToolBar_Button11)
		Me.tbToolBar.Items.Add(_tbToolBar_Button12)
		Me.tbToolBar.Items.Add(_tbToolBar_Button13)
		Me.sbStatusBar.Items.AddRange(New System.Windows.Forms.ToolStripItem(){Me._sbStatusBar_Panel1})
		Me.sbStatusBar.Items.AddRange(New System.Windows.Forms.ToolStripItem(){Me._sbStatusBar_Panel2})
		Me.sbStatusBar.Items.AddRange(New System.Windows.Forms.ToolStripItem(){Me._sbStatusBar_Panel3})
		Me.lblTitle.SetIndex(_lblTitle_1, CType(1, Short))
		Me.lblTitle.SetIndex(_lblTitle_0, CType(0, Short))
		Me.mnuListViewMode.SetIndex(_mnuListViewMode_0, CType(0, Short))
		Me.mnuListViewMode.SetIndex(_mnuListViewMode_1, CType(1, Short))
		Me.mnuListViewMode.SetIndex(_mnuListViewMode_2, CType(2, Short))
		CType(Me.mnuListViewMode, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.lblTitle, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.dtpFrom, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.dtpTo, System.ComponentModel.ISupportInitialize).EndInit()
		MainMenu1.Items.AddRange(New System.Windows.Forms.ToolStripItem(){Me.mnuFile, Me.mnuEdit, Me.mnuView, Me.mnuHelp, Me.mnuPopUp})
		mnuFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem(){Me.mnuFilePrint, Me.mnuFileSeperator1, Me.mnuFileDelete, Me.mnuFileSearch, Me.mnuFileSendToFloppy, Me.mnuFileSeperator, Me.mnuFileExit})
		mnuEdit.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem(){Me.mnuFileFont, Me.mnuEditSelectAll})
		mnuView.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem(){Me.mnuViewToolBar, Me.mnuViewStatusBar, Me.mnuViewSeperator1, Me._mnuListViewMode_0, Me._mnuListViewMode_1, Me._mnuListViewMode_2, Me.mnuViewSeperator2, Me.mnuViewRefresh})
		mnuHelp.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem(){Me.mnuHelpPrintViewer, Me.mnuHelpSeperator, Me.mnuHelpAbout})
		mnuPopUp.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem(){Me.mnuPopSeperator, Me.mnuPopPrint, Me.mnuPopMedia, Me.mnuPopSeperator2, Me.mnuPopSearch, Me.mnuPopSeperator3, Me.mnuPopDelete})
		Me.Controls.Add(MainMenu1)
		Me.picStatusInfo.ResumeLayout(False)
		Me.fraStatusInfo.ResumeLayout(False)
		Me.picTitles.ResumeLayout(False)
		Me.picSearch.ResumeLayout(False)
		Me.fraSearch.ResumeLayout(False)
		Me.tbToolBar.ResumeLayout(False)
		Me.sbStatusBar.ResumeLayout(False)
		Me.MainMenu1.ResumeLayout(False)
		Me.ResumeLayout(False)
		Me.PerformLayout()
	End Sub
#End Region 
End Class