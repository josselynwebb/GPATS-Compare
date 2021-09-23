
Imports Xceed.FileSystem
Imports Xceed.Zip
Imports Xceed.Compression

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmExport
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
    Public WithEvents mnuViewer As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuImport As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuSep1 As System.Windows.Forms.ToolStripSeparator
    Public WithEvents mnuExit As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuOperation As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuAbout As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuHelp As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents MainMenu1 As System.Windows.Forms.MenuStrip
    Public WithEvents cmdZip As Button
    Public WithEvents BarGlobal As System.Windows.Forms.ProgressBar
    Public WithEvents lstResults As System.Windows.Forms.ListView
    Public WithEvents ssfraResults As GroupBox
    Public WithEvents txtZipFilename As System.Windows.Forms.TextBox
    Public WithEvents txtFilesToProcess As System.Windows.Forms.TextBox
    Public WithEvents cmdClose As Button
    Public WithEvents lblZipFileName As System.Windows.Forms.Label
    Public WithEvents _lblFilesToProcess_0 As System.Windows.Forms.Label
    Public WithEvents ssfraCommand As GroupBox
    'Public WithEvents XZip As Xceed.Zip.QuickZip
    Public WithEvents lblFilesToProcess As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmExport))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.MainMenu1 = New System.Windows.Forms.MenuStrip()
        Me.mnuOperation = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuViewer = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuImport = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSep1 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuExit = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuHelp = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAbout = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmdZip = New System.Windows.Forms.Button()
        Me.BarGlobal = New System.Windows.Forms.ProgressBar()
        Me.lstResults = New System.Windows.Forms.ListView()
        Me.ssfraResults = New System.Windows.Forms.GroupBox()
        Me.ssfraCommand = New System.Windows.Forms.GroupBox()
        Me.txtZipFilename = New System.Windows.Forms.TextBox()
        Me.txtFilesToProcess = New System.Windows.Forms.TextBox()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.lblZipFileName = New System.Windows.Forms.Label()
        Me._lblFilesToProcess_0 = New System.Windows.Forms.Label()
        Me.lblFilesToProcess = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.MainMenu1.SuspendLayout()
        Me.ssfraResults.SuspendLayout()
        Me.ssfraCommand.SuspendLayout()
        CType(Me.lblFilesToProcess, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MainMenu1
        '
        Me.MainMenu1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuOperation, Me.mnuHelp})
        Me.MainMenu1.Location = New System.Drawing.Point(0, 0)
        Me.MainMenu1.Name = "MainMenu1"
        Me.MainMenu1.Size = New System.Drawing.Size(548, 24)
        Me.MainMenu1.TabIndex = 11
        '
        'mnuOperation
        '
        Me.mnuOperation.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuViewer, Me.mnuImport, Me.mnuSep1, Me.mnuExit})
        Me.mnuOperation.Name = "mnuOperation"
        Me.mnuOperation.Size = New System.Drawing.Size(72, 20)
        Me.mnuOperation.Text = "&Operation"
        '
        'mnuViewer
        '
        Me.mnuViewer.Name = "mnuViewer"
        Me.mnuViewer.Size = New System.Drawing.Size(110, 22)
        Me.mnuViewer.Text = "Vie&wer"
        '
        'mnuImport
        '
        Me.mnuImport.Name = "mnuImport"
        Me.mnuImport.Size = New System.Drawing.Size(110, 22)
        Me.mnuImport.Text = "I&mport"
        '
        'mnuSep1
        '
        Me.mnuSep1.Name = "mnuSep1"
        Me.mnuSep1.Size = New System.Drawing.Size(107, 6)
        '
        'mnuExit
        '
        Me.mnuExit.Name = "mnuExit"
        Me.mnuExit.Size = New System.Drawing.Size(110, 22)
        Me.mnuExit.Text = "E&xit"
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
        'cmdZip
        '
        Me.cmdZip.ForeColor = System.Drawing.Color.Black
        Me.cmdZip.Location = New System.Drawing.Point(436, 16)
        Me.cmdZip.Name = "cmdZip"
        Me.cmdZip.Size = New System.Drawing.Size(75, 25)
        Me.cmdZip.TabIndex = 0
        Me.cmdZip.Text = "Zip"
        '
        'BarGlobal
        '
        Me.BarGlobal.Location = New System.Drawing.Point(12, 227)
        Me.BarGlobal.Name = "BarGlobal"
        Me.BarGlobal.Size = New System.Drawing.Size(521, 20)
        Me.BarGlobal.TabIndex = 9
        Me.BarGlobal.Visible = False
        '
        'lstResults
        '
        Me.lstResults.AllowColumnReorder = True
        Me.lstResults.BackColor = System.Drawing.SystemColors.Window
        Me.lstResults.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstResults.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lstResults.LabelEdit = True
        Me.lstResults.Location = New System.Drawing.Point(6, 18)
        Me.lstResults.Name = "lstResults"
        Me.lstResults.Size = New System.Drawing.Size(505, 81)
        Me.lstResults.TabIndex = 2
        Me.lstResults.UseCompatibleStateImageBehavior = False
        Me.lstResults.View = System.Windows.Forms.View.List
        '
        'ssfraResults
        '
        Me.ssfraResults.Controls.Add(Me.lstResults)
        Me.ssfraResults.ForeColor = System.Drawing.Color.Blue
        Me.ssfraResults.Location = New System.Drawing.Point(12, 116)
        Me.ssfraResults.Name = "ssfraResults"
        Me.ssfraResults.Size = New System.Drawing.Size(521, 105)
        Me.ssfraResults.TabIndex = 3
        Me.ssfraResults.TabStop = False
        Me.ssfraResults.Text = "Results"
        '
        'ssfraCommand
        '
        Me.ssfraCommand.Controls.Add(Me.cmdZip)
        Me.ssfraCommand.Controls.Add(Me.txtZipFilename)
        Me.ssfraCommand.Controls.Add(Me.txtFilesToProcess)
        Me.ssfraCommand.Controls.Add(Me.cmdClose)
        Me.ssfraCommand.Controls.Add(Me.lblZipFileName)
        Me.ssfraCommand.Controls.Add(Me._lblFilesToProcess_0)
        Me.ssfraCommand.ForeColor = System.Drawing.Color.Blue
        Me.ssfraCommand.Location = New System.Drawing.Point(12, 27)
        Me.ssfraCommand.Name = "ssfraCommand"
        Me.ssfraCommand.Size = New System.Drawing.Size(521, 83)
        Me.ssfraCommand.TabIndex = 4
        Me.ssfraCommand.TabStop = False
        Me.ssfraCommand.Text = "File Assignment"
        '
        'txtZipFilename
        '
        Me.txtZipFilename.AcceptsReturn = True
        Me.txtZipFilename.BackColor = System.Drawing.SystemColors.Window
        Me.txtZipFilename.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtZipFilename.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtZipFilename.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtZipFilename.Location = New System.Drawing.Point(112, 19)
        Me.txtZipFilename.MaxLength = 0
        Me.txtZipFilename.Name = "txtZipFilename"
        Me.txtZipFilename.ReadOnly = True
        Me.txtZipFilename.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtZipFilename.Size = New System.Drawing.Size(273, 20)
        Me.txtZipFilename.TabIndex = 6
        Me.txtZipFilename.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtFilesToProcess
        '
        Me.txtFilesToProcess.AcceptsReturn = True
        Me.txtFilesToProcess.BackColor = System.Drawing.SystemColors.Window
        Me.txtFilesToProcess.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFilesToProcess.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFilesToProcess.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFilesToProcess.Location = New System.Drawing.Point(112, 50)
        Me.txtFilesToProcess.MaxLength = 0
        Me.txtFilesToProcess.Name = "txtFilesToProcess"
        Me.txtFilesToProcess.ReadOnly = True
        Me.txtFilesToProcess.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFilesToProcess.Size = New System.Drawing.Size(273, 20)
        Me.txtFilesToProcess.TabIndex = 5
        Me.txtFilesToProcess.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'cmdClose
        '
        Me.cmdClose.ForeColor = System.Drawing.Color.Black
        Me.cmdClose.Location = New System.Drawing.Point(436, 47)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(75, 25)
        Me.cmdClose.TabIndex = 1
        Me.cmdClose.Text = "Close"
        '
        'lblZipFileName
        '
        Me.lblZipFileName.BackColor = System.Drawing.SystemColors.Control
        Me.lblZipFileName.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblZipFileName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblZipFileName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblZipFileName.Location = New System.Drawing.Point(16, 20)
        Me.lblZipFileName.Name = "lblZipFileName"
        Me.lblZipFileName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblZipFileName.Size = New System.Drawing.Size(73, 17)
        Me.lblZipFileName.TabIndex = 8
        Me.lblZipFileName.Text = "Zip file name : "
        '
        '_lblFilesToProcess_0
        '
        Me._lblFilesToProcess_0.BackColor = System.Drawing.SystemColors.Control
        Me._lblFilesToProcess_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblFilesToProcess_0.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblFilesToProcess_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblFilesToProcess.SetIndex(Me._lblFilesToProcess_0, CType(0, Short))
        Me._lblFilesToProcess_0.Location = New System.Drawing.Point(16, 51)
        Me._lblFilesToProcess_0.Name = "_lblFilesToProcess_0"
        Me._lblFilesToProcess_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblFilesToProcess_0.Size = New System.Drawing.Size(81, 17)
        Me._lblFilesToProcess_0.TabIndex = 7
        Me._lblFilesToProcess_0.Text = "File to process : "
        '
        'frmExport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(548, 258)
        Me.ControlBox = False
        Me.Controls.Add(Me.BarGlobal)
        Me.Controls.Add(Me.ssfraResults)
        Me.Controls.Add(Me.ssfraCommand)
        Me.Controls.Add(Me.MainMenu1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(4, 42)
        Me.MinimizeBox = False
        Me.Name = "frmExport"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds
        Me.Text = "File Exporter"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.MainMenu1.ResumeLayout(False)
        Me.MainMenu1.PerformLayout()
        Me.ssfraResults.ResumeLayout(False)
        Me.ssfraCommand.ResumeLayout(False)
        Me.ssfraCommand.PerformLayout()
        CType(Me.lblFilesToProcess, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
#End Region
End Class