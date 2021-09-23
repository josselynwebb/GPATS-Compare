Partial Class frmViewer
#Region "Windows Form Designer generated code "
    <System.Diagnostics.DebuggerNonUserCode()> Public Sub New()
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'This form is an MDI child.
        'This code simulates the VB6 
        ' functionality of automatically
        ' loading and showing an MDI
        ' child's parent.
        Me.MdiParent = FHDB_Processor.MDIMain
        FHDB_Processor.MDIMain.Show()
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
    Public WithEvents mnuExportRecord As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuExportRecordSet As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuExportAsText As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuSep2 As System.Windows.Forms.ToolStripSeparator
    Public WithEvents mnuExit As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuFile As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuImport As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuExport As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuSep1 As System.Windows.Forms.ToolStripSeparator
    Public WithEvents mnuComment As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuOperation As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuFirst As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuLast As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuPrevious As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuNext As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuNavigation As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuLoadAll As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuSep3 As System.Windows.Forms.ToolStripSeparator
    Public WithEvents mnuSpecific As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuSearch As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuAbout As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuHelp As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents MainMenu1 As System.Windows.Forms.MenuStrip
    Public SaveFileOpen As System.Windows.Forms.OpenFileDialog
    Public SaveFileSave As System.Windows.Forms.SaveFileDialog
    Public SaveFileFont As System.Windows.Forms.FontDialog
    Public SaveFileColor As System.Windows.Forms.ColorDialog
    Public SaveFilePrint As System.Windows.Forms.PrintDialog
    Public WithEvents _txtData_2 As System.Windows.Forms.TextBox
    Public WithEvents _txtData_1 As System.Windows.Forms.TextBox
    Public WithEvents _txtData_16 As System.Windows.Forms.TextBox
    Public WithEvents _txtData_6 As System.Windows.Forms.TextBox
    Public WithEvents _txtData_7 As System.Windows.Forms.TextBox
    Public WithEvents _txtData_3 As System.Windows.Forms.TextBox
    Public WithEvents _txtData_0 As System.Windows.Forms.TextBox
    Public WithEvents _lblData_0 As System.Windows.Forms.Label
    Public WithEvents _lblData_3 As System.Windows.Forms.Label
    Public WithEvents _lblData_4 As System.Windows.Forms.Label
    Public WithEvents _lblData_7 As System.Windows.Forms.Label
    Public WithEvents _lblData_5 As System.Windows.Forms.Label
    Public WithEvents _lblData_6 As System.Windows.Forms.Label
    Public WithEvents _lblData_16 As System.Windows.Forms.Label
    Public WithEvents _lblData_1 As System.Windows.Forms.Label
    Public WithEvents _lblData_2 As System.Windows.Forms.Label
    Public WithEvents ssfraSystem As GroupBox
    'Public WithEvents ssfraMeasurement As TextBox
    Public WithEvents _txtData_11 As System.Windows.Forms.TextBox
    Public WithEvents _txtData_14 As System.Windows.Forms.TextBox
    Public WithEvents _txtData_13 As System.Windows.Forms.TextBox
    Public WithEvents _txtData_9 As System.Windows.Forms.TextBox
    Public WithEvents _txtData_8 As System.Windows.Forms.TextBox
    Public WithEvents _txtData_10 As System.Windows.Forms.TextBox
    Public WithEvents SSPnlPassFail As Panel
    Public WithEvents _SSPnlData_13 As Panel
    Public WithEvents _SSPnlData_9 As Panel
    Public WithEvents _SSPnlData_11 As Panel
    Public WithEvents _SSPnlData_14 As Panel
    Public WithEvents _lblData_14 As System.Windows.Forms.Label
    Public WithEvents _lblData_13 As System.Windows.Forms.Label
    Public WithEvents _lblData_11 As System.Windows.Forms.Label
    Public WithEvents _lblData_10 As System.Windows.Forms.Label
    Public WithEvents _lblData_9 As System.Windows.Forms.Label
    Public WithEvents _lblData_8 As System.Windows.Forms.Label
    Public WithEvents Groupboxasurement As GroupBox
    Public WithEvents cmdFirst As Button
    Public WithEvents txtComments As System.Windows.Forms.TextBox
    Public WithEvents cmdPrevious As Button
    Public WithEvents cmdNext As Button
    Public WithEvents cmdLast As Button
    Public WithEvents ssfraComment As GroupBox
    'Public WithEvents xUnzip As XceedZip
    Public WithEvents lblScroller As System.Windows.Forms.Label
    Public WithEvents SSPnlData As Array
    Public WithEvents lblData As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    Public WithEvents txtData As Microsoft.VisualBasic.Compatibility.VB6.TextBoxArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmViewer))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.MainMenu1 = New System.Windows.Forms.MenuStrip()
        Me.mnuFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuExportAsText = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuExportRecord = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuExportRecordSet = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSep2 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuExit = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuOperation = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuImport = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuExport = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSep1 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuComment = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuNavigation = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFirst = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuLast = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPrevious = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuNext = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSearch = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuLoadAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSep3 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuSpecific = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuHelp = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAbout = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveFileOpen = New System.Windows.Forms.OpenFileDialog()
        Me.SaveFileSave = New System.Windows.Forms.SaveFileDialog()
        Me.SaveFileFont = New System.Windows.Forms.FontDialog()
        Me.SaveFileColor = New System.Windows.Forms.ColorDialog()
        Me.SaveFilePrint = New System.Windows.Forms.PrintDialog()
        Me.ssfraSystem = New System.Windows.Forms.GroupBox()
        Me._txtData_2 = New System.Windows.Forms.TextBox()
        Me._txtData_1 = New System.Windows.Forms.TextBox()
        Me._txtData_4 = New System.Windows.Forms.TextBox()
        Me._txtData_16 = New System.Windows.Forms.TextBox()
        Me._txtData_5 = New System.Windows.Forms.TextBox()
        Me._txtData_6 = New System.Windows.Forms.TextBox()
        Me._txtData_7 = New System.Windows.Forms.TextBox()
        Me._txtData_3 = New System.Windows.Forms.TextBox()
        Me._txtData_0 = New System.Windows.Forms.TextBox()
        Me._lblData_0 = New System.Windows.Forms.Label()
        Me._lblData_3 = New System.Windows.Forms.Label()
        Me._lblData_4 = New System.Windows.Forms.Label()
        Me._lblData_7 = New System.Windows.Forms.Label()
        Me._lblData_5 = New System.Windows.Forms.Label()
        Me._lblData_6 = New System.Windows.Forms.Label()
        Me._lblData_16 = New System.Windows.Forms.Label()
        Me._lblData_1 = New System.Windows.Forms.Label()
        Me._lblData_2 = New System.Windows.Forms.Label()
        Me._txtData_8 = New System.Windows.Forms.TextBox()
        Me.Groupboxasurement = New System.Windows.Forms.GroupBox()
        Me._txtData_11 = New System.Windows.Forms.TextBox()
        Me._txtData_14 = New System.Windows.Forms.TextBox()
        Me._txtData_13 = New System.Windows.Forms.TextBox()
        Me._txtData_9 = New System.Windows.Forms.TextBox()
        Me._txtData_10 = New System.Windows.Forms.TextBox()
        Me.SSPnlPassFail = New System.Windows.Forms.Panel()
        Me._SSPnlData_13 = New System.Windows.Forms.Panel()
        Me._SSPnlData_9 = New System.Windows.Forms.Panel()
        Me._SSPnlData_11 = New System.Windows.Forms.Panel()
        Me._SSPnlData_14 = New System.Windows.Forms.Panel()
        Me._lblData_14 = New System.Windows.Forms.Label()
        Me._lblData_13 = New System.Windows.Forms.Label()
        Me._lblData_11 = New System.Windows.Forms.Label()
        Me._lblData_10 = New System.Windows.Forms.Label()
        Me._lblData_9 = New System.Windows.Forms.Label()
        Me._lblData_8 = New System.Windows.Forms.Label()
        Me.cmdFirst = New System.Windows.Forms.Button()
        Me.txtComments = New System.Windows.Forms.TextBox()
        Me.cmdPrevious = New System.Windows.Forms.Button()
        Me.cmdNext = New System.Windows.Forms.Button()
        Me.cmdLast = New System.Windows.Forms.Button()
        Me.ssfraComment = New System.Windows.Forms.GroupBox()
        Me.lblScroller = New System.Windows.Forms.Label()
        Me.lblData = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.txtData = New Microsoft.VisualBasic.Compatibility.VB6.TextBoxArray(Me.components)
        Me.ssfraMeasurement = New System.Windows.Forms.GroupBox()
        Me.SldRecord = New System.Windows.Forms.HScrollBar()
        Me.MainMenu1.SuspendLayout()
        Me.ssfraSystem.SuspendLayout()
        CType(Me.lblData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ssfraMeasurement.SuspendLayout()
        Me.SuspendLayout()
        '
        'MainMenu1
        '
        Me.MainMenu1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFile, Me.mnuOperation, Me.mnuNavigation, Me.mnuSearch, Me.mnuHelp})
        Me.MainMenu1.Location = New System.Drawing.Point(0, 0)
        Me.MainMenu1.Name = "MainMenu1"
        Me.MainMenu1.Size = New System.Drawing.Size(728, 24)
        Me.MainMenu1.TabIndex = 54
        '
        'mnuFile
        '
        Me.mnuFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuExportAsText, Me.mnuSep2, Me.mnuExit})
        Me.mnuFile.Name = "mnuFile"
        Me.mnuFile.Size = New System.Drawing.Size(37, 20)
        Me.mnuFile.Text = "&File"
        '
        'mnuExportAsText
        '
        Me.mnuExportAsText.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuExportRecord, Me.mnuExportRecordSet})
        Me.mnuExportAsText.Name = "mnuExportAsText"
        Me.mnuExportAsText.Size = New System.Drawing.Size(167, 22)
        Me.mnuExportAsText.Text = "Export as Text File"
        '
        'mnuExportRecord
        '
        Me.mnuExportRecord.Name = "mnuExportRecord"
        Me.mnuExportRecord.Size = New System.Drawing.Size(270, 22)
        Me.mnuExportRecord.Text = "Displayed Record"
        '
        'mnuExportRecordSet
        '
        Me.mnuExportRecordSet.Name = "mnuExportRecordSet"
        Me.mnuExportRecordSet.Size = New System.Drawing.Size(270, 22)
        Me.mnuExportRecordSet.Text = "Multiple Records from Search Results"
        '
        'mnuSep2
        '
        Me.mnuSep2.Name = "mnuSep2"
        Me.mnuSep2.Size = New System.Drawing.Size(164, 6)
        '
        'mnuExit
        '
        Me.mnuExit.Name = "mnuExit"
        Me.mnuExit.Size = New System.Drawing.Size(167, 22)
        Me.mnuExit.Text = "E&xit"
        '
        'mnuOperation
        '
        Me.mnuOperation.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuImport, Me.mnuExport, Me.mnuSep1, Me.mnuComment})
        Me.mnuOperation.Name = "mnuOperation"
        Me.mnuOperation.Size = New System.Drawing.Size(72, 20)
        Me.mnuOperation.Text = "&Operation"
        '
        'mnuImport
        '
        Me.mnuImport.Name = "mnuImport"
        Me.mnuImport.Size = New System.Drawing.Size(162, 22)
        Me.mnuImport.Text = "I&mport"
        '
        'mnuExport
        '
        Me.mnuExport.Name = "mnuExport"
        Me.mnuExport.Size = New System.Drawing.Size(162, 22)
        Me.mnuExport.Text = "&Export"
        '
        'mnuSep1
        '
        Me.mnuSep1.Name = "mnuSep1"
        Me.mnuSep1.Size = New System.Drawing.Size(159, 6)
        '
        'mnuComment
        '
        Me.mnuComment.Name = "mnuComment"
        Me.mnuComment.Size = New System.Drawing.Size(162, 22)
        Me.mnuComment.Text = "Add a &Comment"
        '
        'mnuNavigation
        '
        Me.mnuNavigation.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFirst, Me.mnuLast, Me.mnuPrevious, Me.mnuNext})
        Me.mnuNavigation.Name = "mnuNavigation"
        Me.mnuNavigation.Size = New System.Drawing.Size(77, 20)
        Me.mnuNavigation.Text = "Na&vigation"
        '
        'mnuFirst
        '
        Me.mnuFirst.Name = "mnuFirst"
        Me.mnuFirst.Size = New System.Drawing.Size(152, 22)
        Me.mnuFirst.Text = "&First"
        '
        'mnuLast
        '
        Me.mnuLast.Name = "mnuLast"
        Me.mnuLast.Size = New System.Drawing.Size(152, 22)
        Me.mnuLast.Text = "&Last"
        '
        'mnuPrevious
        '
        Me.mnuPrevious.Name = "mnuPrevious"
        Me.mnuPrevious.Size = New System.Drawing.Size(152, 22)
        Me.mnuPrevious.Text = "&Previous"
        '
        'mnuNext
        '
        Me.mnuNext.Name = "mnuNext"
        Me.mnuNext.Size = New System.Drawing.Size(152, 22)
        Me.mnuNext.Text = "&Next"
        '
        'mnuSearch
        '
        Me.mnuSearch.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuLoadAll, Me.mnuSep3, Me.mnuSpecific})
        Me.mnuSearch.Name = "mnuSearch"
        Me.mnuSearch.Size = New System.Drawing.Size(54, 20)
        Me.mnuSearch.Text = "&Search"
        '
        'mnuLoadAll
        '
        Me.mnuLoadAll.Name = "mnuLoadAll"
        Me.mnuLoadAll.Size = New System.Drawing.Size(165, 22)
        Me.mnuLoadAll.Text = "Load all &Records"
        '
        'mnuSep3
        '
        Me.mnuSep3.Name = "mnuSep3"
        Me.mnuSep3.Size = New System.Drawing.Size(162, 6)
        '
        'mnuSpecific
        '
        Me.mnuSpecific.Name = "mnuSpecific"
        Me.mnuSpecific.Size = New System.Drawing.Size(165, 22)
        Me.mnuSpecific.Text = "S&pecific Record/s"
        '
        'mnuHelp
        '
        Me.mnuHelp.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuAbout})
        Me.mnuHelp.Name = "mnuHelp"
        Me.mnuHelp.Size = New System.Drawing.Size(44, 20)
        Me.mnuHelp.Text = "&Help"
        '
        'mnuAbout
        '
        Me.mnuAbout.Name = "mnuAbout"
        Me.mnuAbout.Size = New System.Drawing.Size(214, 22)
        Me.mnuAbout.Text = "&About the FHDB Processor"
        '
        'ssfraSystem
        '
        Me.ssfraSystem.Controls.Add(Me._txtData_2)
        Me.ssfraSystem.Controls.Add(Me._txtData_1)
        Me.ssfraSystem.Controls.Add(Me._txtData_4)
        Me.ssfraSystem.Controls.Add(Me._txtData_16)
        Me.ssfraSystem.Controls.Add(Me._txtData_5)
        Me.ssfraSystem.Controls.Add(Me._txtData_6)
        Me.ssfraSystem.Controls.Add(Me._txtData_7)
        Me.ssfraSystem.Controls.Add(Me._txtData_3)
        Me.ssfraSystem.Controls.Add(Me._txtData_0)
        Me.ssfraSystem.Controls.Add(Me._lblData_0)
        Me.ssfraSystem.Controls.Add(Me._lblData_3)
        Me.ssfraSystem.Controls.Add(Me._lblData_4)
        Me.ssfraSystem.Controls.Add(Me._lblData_7)
        Me.ssfraSystem.Controls.Add(Me._lblData_5)
        Me.ssfraSystem.Controls.Add(Me._lblData_6)
        Me.ssfraSystem.Controls.Add(Me._lblData_16)
        Me.ssfraSystem.Controls.Add(Me._lblData_1)
        Me.ssfraSystem.Controls.Add(Me._lblData_2)
        Me.ssfraSystem.ForeColor = System.Drawing.Color.Blue
        Me.ssfraSystem.Location = New System.Drawing.Point(12, 27)
        Me.ssfraSystem.Name = "ssfraSystem"
        Me.ssfraSystem.Size = New System.Drawing.Size(703, 144)
        Me.ssfraSystem.TabIndex = 15
        Me.ssfraSystem.TabStop = False
        Me.ssfraSystem.Text = "System Data"
        '
        '_txtData_2
        '
        Me._txtData_2.AcceptsReturn = True
        Me._txtData_2.BackColor = System.Drawing.SystemColors.Window
        Me._txtData_2.Cursor = System.Windows.Forms.Cursors.IBeam
        Me._txtData_2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._txtData_2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtData.SetIndex(Me._txtData_2, CType(2, Short))
        Me._txtData_2.Location = New System.Drawing.Point(555, 101)
        Me._txtData_2.MaxLength = 0
        Me._txtData_2.Name = "_txtData_2"
        Me._txtData_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._txtData_2.Size = New System.Drawing.Size(132, 20)
        Me._txtData_2.TabIndex = 42
        '
        '_txtData_1
        '
        Me._txtData_1.AcceptsReturn = True
        Me._txtData_1.BackColor = System.Drawing.SystemColors.Window
        Me._txtData_1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me._txtData_1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._txtData_1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtData.SetIndex(Me._txtData_1, CType(1, Short))
        Me._txtData_1.Location = New System.Drawing.Point(555, 69)
        Me._txtData_1.MaxLength = 0
        Me._txtData_1.Name = "_txtData_1"
        Me._txtData_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._txtData_1.Size = New System.Drawing.Size(132, 20)
        Me._txtData_1.TabIndex = 40
        '
        '_txtData_4
        '
        Me._txtData_4.AcceptsReturn = True
        Me._txtData_4.BackColor = System.Drawing.SystemColors.Window
        Me._txtData_4.Cursor = System.Windows.Forms.Cursors.IBeam
        Me._txtData_4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._txtData_4.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtData.SetIndex(Me._txtData_4, CType(4, Short))
        Me._txtData_4.Location = New System.Drawing.Point(171, 44)
        Me._txtData_4.MaxLength = 0
        Me._txtData_4.Name = "_txtData_4"
        Me._txtData_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._txtData_4.Size = New System.Drawing.Size(116, 20)
        Me._txtData_4.TabIndex = 38
        Me._txtData_4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        '_txtData_16
        '
        Me._txtData_16.AcceptsReturn = True
        Me._txtData_16.BackColor = System.Drawing.SystemColors.Window
        Me._txtData_16.Cursor = System.Windows.Forms.Cursors.IBeam
        Me._txtData_16.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._txtData_16.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtData.SetIndex(Me._txtData_16, CType(16, Short))
        Me._txtData_16.Location = New System.Drawing.Point(555, 37)
        Me._txtData_16.MaxLength = 0
        Me._txtData_16.Name = "_txtData_16"
        Me._txtData_16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._txtData_16.Size = New System.Drawing.Size(86, 20)
        Me._txtData_16.TabIndex = 36
        Me._txtData_16.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        '_txtData_5
        '
        Me._txtData_5.AcceptsReturn = True
        Me._txtData_5.BackColor = System.Drawing.SystemColors.Window
        Me._txtData_5.Cursor = System.Windows.Forms.Cursors.IBeam
        Me._txtData_5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._txtData_5.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtData.SetIndex(Me._txtData_5, CType(5, Short))
        Me._txtData_5.Location = New System.Drawing.Point(347, 44)
        Me._txtData_5.MaxLength = 0
        Me._txtData_5.Name = "_txtData_5"
        Me._txtData_5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._txtData_5.Size = New System.Drawing.Size(86, 20)
        Me._txtData_5.TabIndex = 34
        Me._txtData_5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        '_txtData_6
        '
        Me._txtData_6.AcceptsReturn = True
        Me._txtData_6.BackColor = System.Drawing.SystemColors.Window
        Me._txtData_6.Cursor = System.Windows.Forms.Cursors.IBeam
        Me._txtData_6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._txtData_6.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtData.SetIndex(Me._txtData_6, CType(6, Short))
        Me._txtData_6.Location = New System.Drawing.Point(347, 101)
        Me._txtData_6.MaxLength = 0
        Me._txtData_6.Name = "_txtData_6"
        Me._txtData_6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._txtData_6.Size = New System.Drawing.Size(86, 20)
        Me._txtData_6.TabIndex = 32
        Me._txtData_6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        '_txtData_7
        '
        Me._txtData_7.AcceptsReturn = True
        Me._txtData_7.BackColor = System.Drawing.SystemColors.Window
        Me._txtData_7.Cursor = System.Windows.Forms.Cursors.IBeam
        Me._txtData_7.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._txtData_7.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtData.SetIndex(Me._txtData_7, CType(7, Short))
        Me._txtData_7.Location = New System.Drawing.Point(171, 101)
        Me._txtData_7.MaxLength = 0
        Me._txtData_7.Name = "_txtData_7"
        Me._txtData_7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._txtData_7.Size = New System.Drawing.Size(116, 20)
        Me._txtData_7.TabIndex = 30
        Me._txtData_7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        '_txtData_3
        '
        Me._txtData_3.AcceptsReturn = True
        Me._txtData_3.BackColor = System.Drawing.SystemColors.Window
        Me._txtData_3.Cursor = System.Windows.Forms.Cursors.IBeam
        Me._txtData_3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._txtData_3.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtData.SetIndex(Me._txtData_3, CType(3, Short))
        Me._txtData_3.Location = New System.Drawing.Point(20, 101)
        Me._txtData_3.MaxLength = 0
        Me._txtData_3.Name = "_txtData_3"
        Me._txtData_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._txtData_3.Size = New System.Drawing.Size(86, 20)
        Me._txtData_3.TabIndex = 28
        Me._txtData_3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        '_txtData_0
        '
        Me._txtData_0.AcceptsReturn = True
        Me._txtData_0.BackColor = System.Drawing.SystemColors.Window
        Me._txtData_0.Cursor = System.Windows.Forms.Cursors.IBeam
        Me._txtData_0.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._txtData_0.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtData.SetIndex(Me._txtData_0, CType(0, Short))
        Me._txtData_0.Location = New System.Drawing.Point(20, 44)
        Me._txtData_0.MaxLength = 0
        Me._txtData_0.Name = "_txtData_0"
        Me._txtData_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._txtData_0.Size = New System.Drawing.Size(85, 20)
        Me._txtData_0.TabIndex = 26
        Me._txtData_0.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        '_lblData_0
        '
        Me._lblData_0.AutoSize = True
        Me._lblData_0.BackColor = System.Drawing.SystemColors.Control
        Me._lblData_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblData_0.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblData_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblData.SetIndex(Me._lblData_0, CType(0, Short))
        Me._lblData_0.Location = New System.Drawing.Point(21, 24)
        Me._lblData_0.Name = "_lblData_0"
        Me._lblData_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblData_0.Size = New System.Drawing.Size(82, 14)
        Me._lblData_0.TabIndex = 24
        Me._lblData_0.Text = "Record Number"
        '
        '_lblData_3
        '
        Me._lblData_3.AutoSize = True
        Me._lblData_3.BackColor = System.Drawing.SystemColors.Control
        Me._lblData_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblData_3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblData_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblData.SetIndex(Me._lblData_3, CType(3, Short))
        Me._lblData_3.Location = New System.Drawing.Point(49, 84)
        Me._lblData_3.Name = "_lblData_3"
        Me._lblData_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblData_3.Size = New System.Drawing.Size(28, 14)
        Me._lblData_3.TabIndex = 23
        Me._lblData_3.Text = "ERO"
        '
        '_lblData_4
        '
        Me._lblData_4.AutoSize = True
        Me._lblData_4.BackColor = System.Drawing.SystemColors.Control
        Me._lblData_4.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblData_4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblData_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblData.SetIndex(Me._lblData_4, CType(4, Short))
        Me._lblData_4.Location = New System.Drawing.Point(209, 27)
        Me._lblData_4.Name = "_lblData_4"
        Me._lblData_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblData_4.Size = New System.Drawing.Size(40, 14)
        Me._lblData_4.TabIndex = 22
        Me._lblData_4.Text = "TPCCN"
        '
        '_lblData_7
        '
        Me._lblData_7.AutoSize = True
        Me._lblData_7.BackColor = System.Drawing.SystemColors.Control
        Me._lblData_7.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblData_7.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblData_7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblData.SetIndex(Me._lblData_7, CType(7, Short))
        Me._lblData_7.Location = New System.Drawing.Point(186, 84)
        Me._lblData_7.Name = "_lblData_7"
        Me._lblData_7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblData_7.Size = New System.Drawing.Size(86, 14)
        Me._lblData_7.TabIndex = 21
        Me._lblData_7.Text = "ID Serial Number"
        '
        '_lblData_5
        '
        Me._lblData_5.AutoSize = True
        Me._lblData_5.BackColor = System.Drawing.SystemColors.Control
        Me._lblData_5.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblData_5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblData_5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblData.SetIndex(Me._lblData_5, CType(5, Short))
        Me._lblData_5.Location = New System.Drawing.Point(342, 27)
        Me._lblData_5.Name = "_lblData_5"
        Me._lblData_5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblData_5.Size = New System.Drawing.Size(97, 14)
        Me._lblData_5.TabIndex = 20
        Me._lblData_5.Text = "UUT Serial Number"
        '
        '_lblData_6
        '
        Me._lblData_6.AutoSize = True
        Me._lblData_6.BackColor = System.Drawing.SystemColors.Control
        Me._lblData_6.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblData_6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblData_6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblData.SetIndex(Me._lblData_6, CType(6, Short))
        Me._lblData_6.Location = New System.Drawing.Point(355, 84)
        Me._lblData_6.Name = "_lblData_6"
        Me._lblData_6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblData_6.Size = New System.Drawing.Size(71, 14)
        Me._lblData_6.TabIndex = 19
        Me._lblData_6.Text = "UUT Revision"
        '
        '_lblData_16
        '
        Me._lblData_16.AutoSize = True
        Me._lblData_16.BackColor = System.Drawing.SystemColors.Control
        Me._lblData_16.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblData_16.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblData_16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblData.SetIndex(Me._lblData_16, CType(16, Short))
        Me._lblData_16.Location = New System.Drawing.Point(484, 40)
        Me._lblData_16.Name = "_lblData_16"
        Me._lblData_16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblData_16.Size = New System.Drawing.Size(67, 14)
        Me._lblData_16.TabIndex = 18
        Me._lblData_16.Text = "Temperature"
        '
        '_lblData_1
        '
        Me._lblData_1.AutoSize = True
        Me._lblData_1.BackColor = System.Drawing.SystemColors.Control
        Me._lblData_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblData_1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblData_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblData.SetIndex(Me._lblData_1, CType(1, Short))
        Me._lblData_1.Location = New System.Drawing.Point(496, 72)
        Me._lblData_1.Name = "_lblData_1"
        Me._lblData_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblData_1.Size = New System.Drawing.Size(55, 14)
        Me._lblData_1.TabIndex = 17
        Me._lblData_1.Text = "Start Time"
        '
        '_lblData_2
        '
        Me._lblData_2.AutoSize = True
        Me._lblData_2.BackColor = System.Drawing.SystemColors.Control
        Me._lblData_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblData_2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblData_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblData.SetIndex(Me._lblData_2, CType(2, Short))
        Me._lblData_2.Location = New System.Drawing.Point(496, 104)
        Me._lblData_2.Name = "_lblData_2"
        Me._lblData_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblData_2.Size = New System.Drawing.Size(54, 14)
        Me._lblData_2.TabIndex = 16
        Me._lblData_2.Text = "Stop Time"
        '
        '_txtData_8
        '
        Me._txtData_8.AcceptsReturn = True
        Me._txtData_8.BackColor = System.Drawing.SystemColors.Window
        Me._txtData_8.Cursor = System.Windows.Forms.Cursors.IBeam
        Me._txtData_8.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._txtData_8.ForeColor = System.Drawing.Color.Black
        Me.txtData.SetIndex(Me._txtData_8, CType(8, Short))
        Me._txtData_8.Location = New System.Drawing.Point(19, 40)
        Me._txtData_8.MaxLength = 0
        Me._txtData_8.Name = "_txtData_8"
        Me._txtData_8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._txtData_8.Size = New System.Drawing.Size(86, 20)
        Me._txtData_8.TabIndex = 10
        Me._txtData_8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Groupboxasurement
        '
        Me.Groupboxasurement.Location = New System.Drawing.Point(0, 0)
        Me.Groupboxasurement.Name = "Groupboxasurement"
        Me.Groupboxasurement.Size = New System.Drawing.Size(200, 100)
        Me.Groupboxasurement.TabIndex = 0
        Me.Groupboxasurement.TabStop = False
        '
        '_txtData_11
        '
        Me._txtData_11.AcceptsReturn = True
        Me._txtData_11.BackColor = System.Drawing.SystemColors.Window
        Me._txtData_11.Cursor = System.Windows.Forms.Cursors.IBeam
        Me._txtData_11.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._txtData_11.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtData.SetIndex(Me._txtData_11, CType(11, Short))
        Me._txtData_11.Location = New System.Drawing.Point(243, 58)
        Me._txtData_11.MaxLength = 0
        Me._txtData_11.Name = "_txtData_11"
        Me._txtData_11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._txtData_11.Size = New System.Drawing.Size(161, 20)
        Me._txtData_11.TabIndex = 46
        Me._txtData_11.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        '_txtData_14
        '
        Me._txtData_14.AcceptsReturn = True
        Me._txtData_14.BackColor = System.Drawing.SystemColors.Window
        Me._txtData_14.Cursor = System.Windows.Forms.Cursors.IBeam
        Me._txtData_14.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._txtData_14.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtData.SetIndex(Me._txtData_14, CType(14, Short))
        Me._txtData_14.Location = New System.Drawing.Point(243, 96)
        Me._txtData_14.MaxLength = 0
        Me._txtData_14.Name = "_txtData_14"
        Me._txtData_14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._txtData_14.Size = New System.Drawing.Size(161, 20)
        Me._txtData_14.TabIndex = 45
        Me._txtData_14.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        '_txtData_13
        '
        Me._txtData_13.AcceptsReturn = True
        Me._txtData_13.BackColor = System.Drawing.SystemColors.Window
        Me._txtData_13.Cursor = System.Windows.Forms.Cursors.IBeam
        Me._txtData_13.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._txtData_13.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtData.SetIndex(Me._txtData_13, CType(13, Short))
        Me._txtData_13.Location = New System.Drawing.Point(243, 24)
        Me._txtData_13.MaxLength = 0
        Me._txtData_13.Name = "_txtData_13"
        Me._txtData_13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._txtData_13.Size = New System.Drawing.Size(161, 20)
        Me._txtData_13.TabIndex = 44
        Me._txtData_13.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        '_txtData_9
        '
        Me._txtData_9.AcceptsReturn = True
        Me._txtData_9.BackColor = System.Drawing.SystemColors.Window
        Me._txtData_9.Cursor = System.Windows.Forms.Cursors.IBeam
        Me._txtData_9.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._txtData_9.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtData.SetIndex(Me._txtData_9, CType(9, Short))
        Me._txtData_9.Location = New System.Drawing.Point(19, 92)
        Me._txtData_9.MaxLength = 0
        Me._txtData_9.Name = "_txtData_9"
        Me._txtData_9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._txtData_9.Size = New System.Drawing.Size(86, 20)
        Me._txtData_9.TabIndex = 13
        Me._txtData_9.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        '_txtData_10
        '
        Me._txtData_10.AcceptsReturn = True
        Me._txtData_10.BackColor = System.Drawing.SystemColors.Window
        Me._txtData_10.Cursor = System.Windows.Forms.Cursors.IBeam
        Me._txtData_10.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._txtData_10.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtData.SetIndex(Me._txtData_10, CType(10, Short))
        Me._txtData_10.Location = New System.Drawing.Point(438, 33)
        Me._txtData_10.MaxLength = 0
        Me._txtData_10.Multiline = True
        Me._txtData_10.Name = "_txtData_10"
        Me._txtData_10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._txtData_10.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me._txtData_10.Size = New System.Drawing.Size(248, 75)
        Me._txtData_10.TabIndex = 6
        '
        'SSPnlPassFail
        '
        Me.SSPnlPassFail.BackColor = System.Drawing.Color.Red
        Me.SSPnlPassFail.Location = New System.Drawing.Point(16, 32)
        Me.SSPnlPassFail.Name = "SSPnlPassFail"
        Me.SSPnlPassFail.Size = New System.Drawing.Size(76, 24)
        Me.SSPnlPassFail.TabIndex = 11
        '
        '_SSPnlData_13
        '
        Me._SSPnlData_13.BackColor = System.Drawing.Color.Red
        Me._SSPnlData_13.Location = New System.Drawing.Point(240, 16)
        Me._SSPnlData_13.Name = "_SSPnlData_13"
        Me._SSPnlData_13.Size = New System.Drawing.Size(167, 20)
        Me._SSPnlData_13.TabIndex = 12
        '
        '_SSPnlData_9
        '
        Me._SSPnlData_9.BackColor = System.Drawing.Color.Red
        Me._SSPnlData_9.Location = New System.Drawing.Point(16, 87)
        Me._SSPnlData_9.Name = "_SSPnlData_9"
        Me._SSPnlData_9.Size = New System.Drawing.Size(92, 20)
        Me._SSPnlData_9.TabIndex = 14
        '
        '_SSPnlData_11
        '
        Me._SSPnlData_11.BackColor = System.Drawing.Color.Red
        Me._SSPnlData_11.Location = New System.Drawing.Point(240, 51)
        Me._SSPnlData_11.Name = "_SSPnlData_11"
        Me._SSPnlData_11.Size = New System.Drawing.Size(167, 20)
        Me._SSPnlData_11.TabIndex = 47
        '
        '_SSPnlData_14
        '
        Me._SSPnlData_14.BackColor = System.Drawing.Color.Red
        Me._SSPnlData_14.Location = New System.Drawing.Point(240, 87)
        Me._SSPnlData_14.Name = "_SSPnlData_14"
        Me._SSPnlData_14.Size = New System.Drawing.Size(167, 20)
        Me._SSPnlData_14.TabIndex = 48
        '
        '_lblData_14
        '
        Me._lblData_14.AutoSize = True
        Me._lblData_14.BackColor = System.Drawing.SystemColors.Control
        Me._lblData_14.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblData_14.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblData_14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblData.SetIndex(Me._lblData_14, CType(14, Short))
        Me._lblData_14.Location = New System.Drawing.Point(176, 95)
        Me._lblData_14.Name = "_lblData_14"
        Me._lblData_14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblData_14.Size = New System.Drawing.Size(63, 14)
        Me._lblData_14.TabIndex = 51
        Me._lblData_14.Text = "Lower Limit"
        Me._lblData_14.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        '_lblData_13
        '
        Me._lblData_13.AutoSize = True
        Me._lblData_13.BackColor = System.Drawing.SystemColors.Control
        Me._lblData_13.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblData_13.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblData_13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblData.SetIndex(Me._lblData_13, CType(13, Short))
        Me._lblData_13.Location = New System.Drawing.Point(178, 23)
        Me._lblData_13.Name = "_lblData_13"
        Me._lblData_13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblData_13.Size = New System.Drawing.Size(60, 14)
        Me._lblData_13.TabIndex = 50
        Me._lblData_13.Text = "Upper Limit"
        Me._lblData_13.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        '_lblData_11
        '
        Me._lblData_11.AutoSize = True
        Me._lblData_11.BackColor = System.Drawing.SystemColors.Control
        Me._lblData_11.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblData_11.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblData_11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblData.SetIndex(Me._lblData_11, CType(11, Short))
        Me._lblData_11.Location = New System.Drawing.Point(134, 57)
        Me._lblData_11.Name = "_lblData_11"
        Me._lblData_11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblData_11.Size = New System.Drawing.Size(105, 14)
        Me._lblData_11.TabIndex = 49
        Me._lblData_11.Text = "Failing Measurement"
        Me._lblData_11.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        '_lblData_10
        '
        Me._lblData_10.AutoSize = True
        Me._lblData_10.BackColor = System.Drawing.SystemColors.Control
        Me._lblData_10.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblData_10.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblData_10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblData.SetIndex(Me._lblData_10, CType(10, Short))
        Me._lblData_10.Location = New System.Drawing.Point(435, 16)
        Me._lblData_10.Name = "_lblData_10"
        Me._lblData_10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblData_10.Size = New System.Drawing.Size(65, 14)
        Me._lblData_10.TabIndex = 9
        Me._lblData_10.Text = "Fault Callout"
        '
        '_lblData_9
        '
        Me._lblData_9.AutoSize = True
        Me._lblData_9.BackColor = System.Drawing.SystemColors.Control
        Me._lblData_9.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblData_9.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblData_9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblData.SetIndex(Me._lblData_9, CType(9, Short))
        Me._lblData_9.Location = New System.Drawing.Point(30, 75)
        Me._lblData_9.Name = "_lblData_9"
        Me._lblData_9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblData_9.Size = New System.Drawing.Size(64, 14)
        Me._lblData_9.TabIndex = 8
        Me._lblData_9.Text = "Failure Step"
        '
        '_lblData_8
        '
        Me._lblData_8.AutoSize = True
        Me._lblData_8.BackColor = System.Drawing.SystemColors.Control
        Me._lblData_8.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblData_8.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblData_8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblData.SetIndex(Me._lblData_8, CType(8, Short))
        Me._lblData_8.Location = New System.Drawing.Point(37, 23)
        Me._lblData_8.Name = "_lblData_8"
        Me._lblData_8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblData_8.Size = New System.Drawing.Size(50, 14)
        Me._lblData_8.TabIndex = 7
        Me._lblData_8.Text = "Pass/Fail"
        '
        'cmdFirst
        '
        Me.cmdFirst.Location = New System.Drawing.Point(42, 444)
        Me.cmdFirst.Name = "cmdFirst"
        Me.cmdFirst.Size = New System.Drawing.Size(75, 23)
        Me.cmdFirst.TabIndex = 0
        Me.cmdFirst.Text = "|<<"
        '
        'txtComments
        '
        Me.txtComments.AcceptsReturn = True
        Me.txtComments.BackColor = System.Drawing.SystemColors.Window
        Me.txtComments.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtComments.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtComments.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtComments.Location = New System.Drawing.Point(25, 329)
        Me.txtComments.MaxLength = 0
        Me.txtComments.Multiline = True
        Me.txtComments.Name = "txtComments"
        Me.txtComments.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtComments.Size = New System.Drawing.Size(684, 97)
        Me.txtComments.TabIndex = 4
        '
        'cmdPrevious
        '
        Me.cmdPrevious.Location = New System.Drawing.Point(125, 444)
        Me.cmdPrevious.Name = "cmdPrevious"
        Me.cmdPrevious.Size = New System.Drawing.Size(75, 23)
        Me.cmdPrevious.TabIndex = 1
        Me.cmdPrevious.Text = "<"
        '
        'cmdNext
        '
        Me.cmdNext.Location = New System.Drawing.Point(207, 444)
        Me.cmdNext.Name = "cmdNext"
        Me.cmdNext.Size = New System.Drawing.Size(75, 23)
        Me.cmdNext.TabIndex = 2
        Me.cmdNext.Text = ">"
        '
        'cmdLast
        '
        Me.cmdLast.Location = New System.Drawing.Point(290, 444)
        Me.cmdLast.Name = "cmdLast"
        Me.cmdLast.Size = New System.Drawing.Size(75, 23)
        Me.cmdLast.TabIndex = 3
        Me.cmdLast.Text = ">>|"
        '
        'ssfraComment
        '
        Me.ssfraComment.ForeColor = System.Drawing.Color.Blue
        Me.ssfraComment.Location = New System.Drawing.Point(12, 308)
        Me.ssfraComment.Name = "ssfraComment"
        Me.ssfraComment.Size = New System.Drawing.Size(703, 124)
        Me.ssfraComment.TabIndex = 25
        Me.ssfraComment.TabStop = False
        Me.ssfraComment.Text = "Operator Comments"
        '
        'lblScroller
        '
        Me.lblScroller.AutoSize = True
        Me.lblScroller.BackColor = System.Drawing.SystemColors.Control
        Me.lblScroller.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblScroller.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblScroller.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblScroller.Location = New System.Drawing.Point(546, 435)
        Me.lblScroller.Name = "lblScroller"
        Me.lblScroller.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblScroller.Size = New System.Drawing.Size(82, 14)
        Me.lblScroller.TabIndex = 53
        Me.lblScroller.Text = "Record Scroller"
        Me.lblScroller.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.lblScroller.Visible = False
        '
        'txtData
        '
        '
        'ssfraMeasurement
        '
        Me.ssfraMeasurement.Controls.Add(Me._txtData_8)
        Me.ssfraMeasurement.Controls.Add(Me._txtData_9)
        Me.ssfraMeasurement.Controls.Add(Me._txtData_11)
        Me.ssfraMeasurement.Controls.Add(Me._txtData_13)
        Me.ssfraMeasurement.Controls.Add(Me._txtData_14)
        Me.ssfraMeasurement.Controls.Add(Me._txtData_10)
        Me.ssfraMeasurement.Controls.Add(Me._lblData_8)
        Me.ssfraMeasurement.Controls.Add(Me._lblData_9)
        Me.ssfraMeasurement.Controls.Add(Me._lblData_10)
        Me.ssfraMeasurement.Controls.Add(Me._lblData_11)
        Me.ssfraMeasurement.Controls.Add(Me._lblData_13)
        Me.ssfraMeasurement.Controls.Add(Me._lblData_14)
        Me.ssfraMeasurement.ForeColor = System.Drawing.Color.Blue
        Me.ssfraMeasurement.Location = New System.Drawing.Point(12, 177)
        Me.ssfraMeasurement.Name = "ssfraMeasurement"
        Me.ssfraMeasurement.Size = New System.Drawing.Size(702, 125)
        Me.ssfraMeasurement.TabIndex = 55
        Me.ssfraMeasurement.TabStop = False
        Me.ssfraMeasurement.Text = "Measurement Data"
        '
        'SldRecord
        '
        Me.SldRecord.LargeChange = 1
        Me.SldRecord.Location = New System.Drawing.Point(482, 461)
        Me.SldRecord.Maximum = 10000
        Me.SldRecord.Name = "SldRecord"
        Me.SldRecord.Size = New System.Drawing.Size(216, 17)
        Me.SldRecord.TabIndex = 56
        Me.SldRecord.Visible = False
        '
        'frmViewer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(728, 488)
        Me.ControlBox = False
        Me.Controls.Add(Me.SldRecord)
        Me.Controls.Add(Me.ssfraMeasurement)
        Me.Controls.Add(Me.ssfraSystem)
        Me.Controls.Add(Me.cmdFirst)
        Me.Controls.Add(Me.txtComments)
        Me.Controls.Add(Me.cmdPrevious)
        Me.Controls.Add(Me.cmdNext)
        Me.Controls.Add(Me.cmdLast)
        Me.Controls.Add(Me.ssfraComment)
        Me.Controls.Add(Me.lblScroller)
        Me.Controls.Add(Me.MainMenu1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(4, 42)
        Me.Name = "frmViewer"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "DataBase Viewer"
        Me.MainMenu1.ResumeLayout(False)
        Me.MainMenu1.PerformLayout()
        Me.ssfraSystem.ResumeLayout(False)
        Me.ssfraSystem.PerformLayout()
        CType(Me.lblData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ssfraMeasurement.ResumeLayout(False)
        Me.ssfraMeasurement.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents _txtData_4 As System.Windows.Forms.TextBox
    Public WithEvents _txtData_5 As System.Windows.Forms.TextBox
    Public WithEvents ssfraMeasurement As System.Windows.Forms.GroupBox
    Friend WithEvents SldRecord As System.Windows.Forms.HScrollBar
#End Region
End Class