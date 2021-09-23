
Imports System.Windows.Forms
Imports Microsoft.VisualBasic

Public Class frmSyslogViewer
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
    Friend WithEvents tbrSyslog As System.Windows.Forms.ToolBar
    Friend WithEvents Button1 As System.Windows.Forms.ToolBarButton
    Friend WithEvents SaveAs As System.Windows.Forms.ToolBarButton
    Friend WithEvents DeleteLogFile As System.Windows.Forms.ToolBarButton
    Friend WithEvents RefreshFile As System.Windows.Forms.ToolBarButton
    Friend WithEvents Button5 As System.Windows.Forms.ToolBarButton
    Friend WithEvents PrintSetup As System.Windows.Forms.ToolBarButton
    Friend WithEvents Print As System.Windows.Forms.ToolBarButton
    Friend WithEvents Button8 As System.Windows.Forms.ToolBarButton
    Friend WithEvents Copy As System.Windows.Forms.ToolBarButton
    Friend WithEvents SelectAll As System.Windows.Forms.ToolBarButton
    Friend WithEvents Find As System.Windows.Forms.ToolBarButton
    Friend WithEvents Button12 As System.Windows.Forms.ToolBarButton
    Friend WithEvents Top As System.Windows.Forms.ToolBarButton
    Friend WithEvents mEnd As System.Windows.Forms.ToolBarButton
    Friend WithEvents Button15 As System.Windows.Forms.ToolBarButton
    Friend WithEvents PageUp As System.Windows.Forms.ToolBarButton
    Friend WithEvents PageDown As System.Windows.Forms.ToolBarButton
    Friend WithEvents picDivider As System.Windows.Forms.PictureBox
    Friend WithEvents rtbLogViewer As System.Windows.Forms.RichTextBox
    Friend WithEvents sbrUserInformation As System.Windows.Forms.StatusBar
    Friend WithEvents sbrUserInformation_Panel1 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents sbrUserInformation_Panel2 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents cdlSyslog As System.Windows.Forms.SaveFileDialog
    Friend WithEvents cdlSyslog_Printer As System.Windows.Forms.PrintDialog
    Friend WithEvents imgSmallButtons As System.Windows.Forms.ImageList
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents mnuFile As System.Windows.Forms.MenuItem
    Friend WithEvents mnuFileSavelogas As System.Windows.Forms.MenuItem
    Friend WithEvents mnuFileDeletelogfile As System.Windows.Forms.MenuItem
    Friend WithEvents mnuDivider1 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuFileRefresh As System.Windows.Forms.MenuItem
    Friend WithEvents mnuFileDivider As System.Windows.Forms.MenuItem
    Friend WithEvents mnuFilePrintsetup As System.Windows.Forms.MenuItem
    Friend WithEvents mnuFilePrint As System.Windows.Forms.MenuItem
    Friend WithEvents mnuDivider2 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuFileExit As System.Windows.Forms.MenuItem
    Friend WithEvents mnuEdit As System.Windows.Forms.MenuItem
    Friend WithEvents mnuEditCopy As System.Windows.Forms.MenuItem
    Friend WithEvents mnuEditSelectAll As System.Windows.Forms.MenuItem
    Friend WithEvents mnuDivider3 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuEditFind As System.Windows.Forms.MenuItem
    Friend WithEvents mnuEditFindnext As System.Windows.Forms.MenuItem
    Friend WithEvents mnuHelp As System.Windows.Forms.MenuItem
    Friend WithEvents mnuHelpAboutSystemlogviewer As System.Windows.Forms.MenuItem
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSyslogViewer))
        Me.tbrSyslog = New System.Windows.Forms.ToolBar()
        Me.Button1 = New System.Windows.Forms.ToolBarButton()
        Me.SaveAs = New System.Windows.Forms.ToolBarButton()
        Me.DeleteLogFile = New System.Windows.Forms.ToolBarButton()
        Me.RefreshFile = New System.Windows.Forms.ToolBarButton()
        Me.Button5 = New System.Windows.Forms.ToolBarButton()
        Me.PrintSetup = New System.Windows.Forms.ToolBarButton()
        Me.Print = New System.Windows.Forms.ToolBarButton()
        Me.Button8 = New System.Windows.Forms.ToolBarButton()
        Me.Copy = New System.Windows.Forms.ToolBarButton()
        Me.SelectAll = New System.Windows.Forms.ToolBarButton()
        Me.Find = New System.Windows.Forms.ToolBarButton()
        Me.Button12 = New System.Windows.Forms.ToolBarButton()
        Me.Top = New System.Windows.Forms.ToolBarButton()
        Me.mEnd = New System.Windows.Forms.ToolBarButton()
        Me.Button15 = New System.Windows.Forms.ToolBarButton()
        Me.PageUp = New System.Windows.Forms.ToolBarButton()
        Me.PageDown = New System.Windows.Forms.ToolBarButton()
        Me.imgSmallButtons = New System.Windows.Forms.ImageList(Me.components)
        Me.picDivider = New System.Windows.Forms.PictureBox()
        Me.rtbLogViewer = New System.Windows.Forms.RichTextBox()
        Me.sbrUserInformation = New System.Windows.Forms.StatusBar()
        Me.sbrUserInformation_Panel1 = New System.Windows.Forms.StatusBarPanel()
        Me.sbrUserInformation_Panel2 = New System.Windows.Forms.StatusBarPanel()
        Me.cdlSyslog = New System.Windows.Forms.SaveFileDialog()
        Me.cdlSyslog_Printer = New System.Windows.Forms.PrintDialog()
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.mnuFile = New System.Windows.Forms.MenuItem()
        Me.mnuFileSavelogas = New System.Windows.Forms.MenuItem()
        Me.mnuFileDeletelogfile = New System.Windows.Forms.MenuItem()
        Me.mnuDivider1 = New System.Windows.Forms.MenuItem()
        Me.mnuFileRefresh = New System.Windows.Forms.MenuItem()
        Me.mnuFileDivider = New System.Windows.Forms.MenuItem()
        Me.mnuFilePrintsetup = New System.Windows.Forms.MenuItem()
        Me.mnuFilePrint = New System.Windows.Forms.MenuItem()
        Me.mnuDivider2 = New System.Windows.Forms.MenuItem()
        Me.mnuFileExit = New System.Windows.Forms.MenuItem()
        Me.mnuEdit = New System.Windows.Forms.MenuItem()
        Me.mnuEditCopy = New System.Windows.Forms.MenuItem()
        Me.mnuEditSelectAll = New System.Windows.Forms.MenuItem()
        Me.mnuDivider3 = New System.Windows.Forms.MenuItem()
        Me.mnuEditFind = New System.Windows.Forms.MenuItem()
        Me.mnuEditFindnext = New System.Windows.Forms.MenuItem()
        Me.mnuHelp = New System.Windows.Forms.MenuItem()
        Me.mnuHelpAboutSystemlogviewer = New System.Windows.Forms.MenuItem()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        CType(Me.picDivider, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sbrUserInformation_Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sbrUserInformation_Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tbrSyslog
        '
        Me.tbrSyslog.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.Button1, Me.SaveAs, Me.DeleteLogFile, Me.RefreshFile, Me.Button5, Me.PrintSetup, Me.Print, Me.Button8, Me.Copy, Me.SelectAll, Me.Find, Me.Button12, Me.Top, Me.mEnd, Me.Button15, Me.PageUp, Me.PageDown})
        Me.tbrSyslog.ButtonSize = New System.Drawing.Size(16, 16)
        Me.tbrSyslog.Dock = System.Windows.Forms.DockStyle.None
        Me.tbrSyslog.DropDownArrows = True
        Me.tbrSyslog.ImageList = Me.imgSmallButtons
        Me.tbrSyslog.Location = New System.Drawing.Point(0, 3)
        Me.tbrSyslog.Name = "tbrSyslog"
        Me.tbrSyslog.ShowToolTips = True
        Me.tbrSyslog.Size = New System.Drawing.Size(1036, 28)
        Me.tbrSyslog.TabIndex = 2
        '
        'Button1
        '
        Me.Button1.Name = "Button1"
        Me.Button1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'SaveAs
        '
        Me.SaveAs.ImageIndex = 13
        Me.SaveAs.Name = "SaveAs"
        Me.SaveAs.ToolTipText = "Save Log As"
        '
        'DeleteLogFile
        '
        Me.DeleteLogFile.ImageIndex = 2
        Me.DeleteLogFile.Name = "DeleteLogFile"
        Me.DeleteLogFile.ToolTipText = "Delete Log File"
        '
        'RefreshFile
        '
        Me.RefreshFile.ImageIndex = 11
        Me.RefreshFile.Name = "RefreshFile"
        Me.RefreshFile.ToolTipText = "Refresh Viewer"
        '
        'Button5
        '
        Me.Button5.Name = "Button5"
        Me.Button5.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'PrintSetup
        '
        Me.PrintSetup.ImageIndex = 9
        Me.PrintSetup.Name = "PrintSetup"
        Me.PrintSetup.ToolTipText = "Print Setup"
        '
        'Print
        '
        Me.Print.ImageIndex = 8
        Me.Print.Name = "Print"
        Me.Print.ToolTipText = "Print"
        '
        'Button8
        '
        Me.Button8.Name = "Button8"
        Me.Button8.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'Copy
        '
        Me.Copy.ImageIndex = 1
        Me.Copy.Name = "Copy"
        Me.Copy.ToolTipText = "Copy To Clipboard"
        '
        'SelectAll
        '
        Me.SelectAll.ImageIndex = 12
        Me.SelectAll.Name = "SelectAll"
        Me.SelectAll.ToolTipText = "Select All"
        '
        'Find
        '
        Me.Find.ImageIndex = 5
        Me.Find.Name = "Find"
        Me.Find.ToolTipText = "Find"
        '
        'Button12
        '
        Me.Button12.Name = "Button12"
        Me.Button12.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'Top
        '
        Me.Top.ImageIndex = 0
        Me.Top.Name = "Top"
        Me.Top.ToolTipText = "Go To Top of Text"
        '
        'mEnd
        '
        Me.mEnd.ImageIndex = 3
        Me.mEnd.Name = "mEnd"
        Me.mEnd.ToolTipText = "Go To End of Text"
        '
        'Button15
        '
        Me.Button15.Name = "Button15"
        Me.Button15.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'PageUp
        '
        Me.PageUp.ImageIndex = 10
        Me.PageUp.Name = "PageUp"
        Me.PageUp.ToolTipText = "Page Up"
        '
        'PageDown
        '
        Me.PageDown.ImageIndex = 7
        Me.PageDown.Name = "PageDown"
        Me.PageDown.ToolTipText = "Page Down"
        '
        'imgSmallButtons
        '
        Me.imgSmallButtons.ImageStream = CType(resources.GetObject("imgSmallButtons.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgSmallButtons.TransparentColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.imgSmallButtons.Images.SetKeyName(0, "")
        Me.imgSmallButtons.Images.SetKeyName(1, "")
        Me.imgSmallButtons.Images.SetKeyName(2, "")
        Me.imgSmallButtons.Images.SetKeyName(3, "")
        Me.imgSmallButtons.Images.SetKeyName(4, "")
        Me.imgSmallButtons.Images.SetKeyName(5, "")
        Me.imgSmallButtons.Images.SetKeyName(6, "")
        Me.imgSmallButtons.Images.SetKeyName(7, "")
        Me.imgSmallButtons.Images.SetKeyName(8, "")
        Me.imgSmallButtons.Images.SetKeyName(9, "")
        Me.imgSmallButtons.Images.SetKeyName(10, "")
        Me.imgSmallButtons.Images.SetKeyName(11, "")
        Me.imgSmallButtons.Images.SetKeyName(12, "")
        Me.imgSmallButtons.Images.SetKeyName(13, "")
        Me.imgSmallButtons.Images.SetKeyName(14, "")
        '
        'picDivider
        '
        Me.picDivider.BackColor = System.Drawing.SystemColors.Window
        Me.picDivider.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picDivider.Image = CType(resources.GetObject("picDivider.Image"), System.Drawing.Image)
        Me.picDivider.Location = New System.Drawing.Point(0, 0)
        Me.picDivider.Name = "picDivider"
        Me.picDivider.Size = New System.Drawing.Size(1025, 4)
        Me.picDivider.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picDivider.TabIndex = 3
        Me.picDivider.TabStop = False
        '
        'rtbLogViewer
        '
        Me.rtbLogViewer.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rtbLogViewer.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtbLogViewer.Location = New System.Drawing.Point(8, 32)
        Me.rtbLogViewer.Name = "rtbLogViewer"
        Me.rtbLogViewer.Size = New System.Drawing.Size(624, 331)
        Me.rtbLogViewer.TabIndex = 1
        Me.rtbLogViewer.Text = ""
        '
        'sbrUserInformation
        '
        Me.sbrUserInformation.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.sbrUserInformation.Location = New System.Drawing.Point(0, 406)
        Me.sbrUserInformation.Name = "sbrUserInformation"
        Me.sbrUserInformation.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.sbrUserInformation_Panel1, Me.sbrUserInformation_Panel2})
        Me.sbrUserInformation.ShowPanels = True
        Me.sbrUserInformation.Size = New System.Drawing.Size(642, 17)
        Me.sbrUserInformation.SizingGrip = False
        Me.sbrUserInformation.TabIndex = 0
        '
        'sbrUserInformation_Panel1
        '
        Me.sbrUserInformation_Panel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
        Me.sbrUserInformation_Panel1.Name = "sbrUserInformation_Panel1"
        Me.sbrUserInformation_Panel1.Text = "User Information"
        Me.sbrUserInformation_Panel1.Width = 593
        '
        'sbrUserInformation_Panel2
        '
        Me.sbrUserInformation_Panel2.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents
        Me.sbrUserInformation_Panel2.MinWidth = 34
        Me.sbrUserInformation_Panel2.Name = "sbrUserInformation_Panel2"
        Me.sbrUserInformation_Panel2.Text = "Row: 0 "
        Me.sbrUserInformation_Panel2.Width = 49
        '
        'cdlSyslog
        '
        Me.cdlSyslog.DefaultExt = "*.txt"
        Me.cdlSyslog.FileName = "Syslog.txt"
        Me.cdlSyslog.Filter = "Text(*.txt;*.log)|*.txt;*.log|Rich Text(*.rtf)|*.rtf|Archive(*.arc;*.bak)|*.arc;*" & _
    ".bak|All Files(*.*)|*.*"
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuFile, Me.mnuEdit, Me.mnuHelp})
        '
        'mnuFile
        '
        Me.mnuFile.Index = 0
        Me.mnuFile.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuFileSavelogas, Me.mnuFileDeletelogfile, Me.mnuDivider1, Me.mnuFileRefresh, Me.mnuFileDivider, Me.mnuFilePrintsetup, Me.mnuFilePrint, Me.mnuDivider2, Me.mnuFileExit})
        Me.mnuFile.Text = "&File"
        '
        'mnuFileSavelogas
        '
        Me.mnuFileSavelogas.Index = 0
        Me.mnuFileSavelogas.Text = "Save Log &As..."
        '
        'mnuFileDeletelogfile
        '
        Me.mnuFileDeletelogfile.Index = 1
        Me.mnuFileDeletelogfile.Text = "&Delete Log File"
        '
        'mnuDivider1
        '
        Me.mnuDivider1.Index = 2
        Me.mnuDivider1.Text = "-"
        '
        'mnuFileRefresh
        '
        Me.mnuFileRefresh.Index = 3
        Me.mnuFileRefresh.Shortcut = System.Windows.Forms.Shortcut.F5
        Me.mnuFileRefresh.Text = "&Refresh Display"
        '
        'mnuFileDivider
        '
        Me.mnuFileDivider.Index = 4
        Me.mnuFileDivider.Text = "-"
        '
        'mnuFilePrintsetup
        '
        Me.mnuFilePrintsetup.Index = 5
        Me.mnuFilePrintsetup.Text = "Print Set&up..."
        '
        'mnuFilePrint
        '
        Me.mnuFilePrint.Index = 6
        Me.mnuFilePrint.Shortcut = System.Windows.Forms.Shortcut.CtrlP
        Me.mnuFilePrint.Text = "P&rint..."
        '
        'mnuDivider2
        '
        Me.mnuDivider2.Index = 7
        Me.mnuDivider2.Text = "-"
        '
        'mnuFileExit
        '
        Me.mnuFileExit.Index = 8
        Me.mnuFileExit.Text = "E&xit"
        '
        'mnuEdit
        '
        Me.mnuEdit.Index = 1
        Me.mnuEdit.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuEditCopy, Me.mnuEditSelectAll, Me.mnuDivider3, Me.mnuEditFind, Me.mnuEditFindnext})
        Me.mnuEdit.Text = "&Edit"
        '
        'mnuEditCopy
        '
        Me.mnuEditCopy.Index = 0
        Me.mnuEditCopy.Shortcut = System.Windows.Forms.Shortcut.CtrlC
        Me.mnuEditCopy.Text = "Copy"
        '
        'mnuEditSelectAll
        '
        Me.mnuEditSelectAll.Index = 1
        Me.mnuEditSelectAll.Shortcut = System.Windows.Forms.Shortcut.CtrlA
        Me.mnuEditSelectAll.Text = "Select A&ll"
        '
        'mnuDivider3
        '
        Me.mnuDivider3.Index = 2
        Me.mnuDivider3.Text = "-"
        '
        'mnuEditFind
        '
        Me.mnuEditFind.Index = 3
        Me.mnuEditFind.Shortcut = System.Windows.Forms.Shortcut.CtrlF
        Me.mnuEditFind.Text = "&Find..."
        '
        'mnuEditFindnext
        '
        Me.mnuEditFindnext.Index = 4
        Me.mnuEditFindnext.Shortcut = System.Windows.Forms.Shortcut.F3
        Me.mnuEditFindnext.Text = "Find &Next"
        '
        'mnuHelp
        '
        Me.mnuHelp.Index = 2
        Me.mnuHelp.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuHelpAboutSystemlogviewer})
        Me.mnuHelp.Text = "&Help"
        '
        'mnuHelpAboutSystemlogviewer
        '
        Me.mnuHelpAboutSystemlogviewer.Index = 0
        Me.mnuHelpAboutSystemlogviewer.Text = "&About System Log Viewer..."
        '
        'frmSyslogViewer
        '
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(642, 423)
        Me.Controls.Add(Me.tbrSyslog)
        Me.Controls.Add(Me.picDivider)
        Me.Controls.Add(Me.rtbLogViewer)
        Me.Controls.Add(Me.sbrUserInformation)
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Menu = Me.MainMenu1
        Me.Name = "frmSyslogViewer"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "System Log Viewer"
        CType(Me.picDivider, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sbrUserInformation_Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sbrUserInformation_Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

End Sub

#End Region

	'=========================================================

    Private Sub Form_QueryUnload(ByRef Cancel As Short, ByVal UnloadMode As Short)
        Application.Exit()
    End Sub

    Private Sub frmSyslogViewer_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Dim Cancel As Short = 0
        Form_QueryUnload(Cancel, 0)
        If Cancel <> 0 Then
            e.Cancel = True
            Exit Sub
        End If
        Form_Unload(Cancel)
        If Cancel <> 0 Then e.Cancel = True
    End Sub


    Private Sub frmSyslogViewer_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        Application.Exit()
    End Sub

    Private Sub Form_Unload(ByRef Cancel As Short)
        Application.Exit()
    End Sub


    Public Sub mnuEditCopy_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuEditCopy.Click

        Clipboard.Clear()
        If (rtbLogViewer.SelectedText <> "") Then
            Clipboard.SetText(rtbLogViewer.SelectedText)
            EchoToStatusBar("Text Copied To Windows Clipboard", 1)
        End If
    End Sub


    Public Sub mnuEditFind_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuEditFind.Click

        frmSearch.ShowDialog()

    End Sub

    Private Sub mnuEditFindnext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuEditFindnext.Click

        SearchLog(False)

    End Sub


    Public Sub mnuEditSelectAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuEditSelectAll.Click

        Me.rtbLogViewer.Focus()
        SendKeys.Send("^{HOME}")
        Me.rtbLogViewer.Focus()
        SendKeys.Send("+^{END}")
        Me.rtbLogViewer.Focus()
        EchoToStatusBar("All Text Selected", 1)
    End Sub



    Public Sub mnuFileDeletelogfile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuFileDeletelogfile.Click

        Dim Mess As String
        
        Dim ReturnValue As DialogResult
        Dim LoadFileName As String

        Mess = "Caution: This action will delete all system log file data."+vbCrLf
        Mess &= "Delete System Log File?"

        ReturnValue = MsgBox(Mess, MsgBoxStyle.YesNo+MsgBoxStyle.Critical+MsgBoxStyle.DefaultButton2, "System Log Viewer")


        If ReturnValue=DialogResult.Yes Then 'If Yes
            Kill(SyslogName)
            FCreate(SyslogName)
            LoadFileName = SyslogName
            Me.rtbLogViewer.LoadFile(LoadFileName, RichTextBoxStreamType.PlainText)
            EchoToStatusBar("Log File Deleted", 1) 'Added 05/03/01  DJoiner
        End If

    End Sub


    Public Sub mnuFilePrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuFilePrint.Click

        Dim PrintCopies As Short
        Dim CopyLoop As Short

        Try ' On Error GoTo ErrHandler

            If Me.rtbLogViewer.SelectionLength=0 Then
            Else
            End If

            Me.cdlSyslog_Printer.ShowDialog()


            PrintCopies = Me.cdlSyslog_Printer.PrinterSettings.Copies

            Exit Sub
        Catch	' ErrHandler:
            Exit Sub
        End Try
    End Sub


    Public Sub mnuFilePrintsetup_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuFilePrintsetup.Click

        On Error Resume Next
        Dim pageSetup As New PageSetupDialog
        pageSetup.ShowDialog()
    End Sub



    Public Sub mnuFileRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuFileRefresh.Click
        UpdateLogViewer()
    End Sub

    Public Sub mnuFileSavelogas_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuFileSavelogas.Click

        Dim SaveFileName As String

        Me.cdlSyslog.Filter = "Archive(*.arc;*.bak)|*.arc;*.bak|Text(*.txt;*.log)|*.txt;*.log|All Files(*.*)|*.*"
        Me.cdlSyslog.DefaultExt = "*.arc"
        Me.cdlSyslog.FileName = "Syslog.arc"

        Try ' On Error GoTo ErrHandler

            Me.cdlSyslog.ShowDialog()
            SaveFileName = Me.cdlSyslog.FileName

            Me.rtbLogViewer.SaveFile(SaveFileName, RichTextBoxStreamType.RichText)
            EchoToStatusBar("Log File Archived", 1)
            Exit Sub
        Catch	' ErrHandler:
            Exit Sub
        End Try
    End Sub


    Private Sub mnuHelpAboutSystemlogviewer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuHelpAboutSystemlogviewer.Click
        '! Load frmAbout
        'CenterChildForm(Me, frmAbout)
        frmAbout.ShowDialog()
    End Sub



    Private Sub rtbLogViewer_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rtbLogViewer.SelectionChanged

        Dim textEditor As RichTextBox = sender
        'Update Row Indicator
        Dim index = textEditor.SelectionStart
        Dim line = textEditor.GetLineFromCharIndex(index)
        Me.sbrUserInformation_Panel2.Text = "Row: " & line


    End Sub


    Private Sub tbrSyslog_ButtonClick(sender As Object, e As ToolBarButtonClickEventArgs) Handles tbrSyslog.ButtonClick
        Dim selectedButton As ToolBarButton = e.Button
        If (selectedButton.Name = "SaveAs") Then
            mnuFileSavelogas_Click(sender, e)

        ElseIf (selectedButton.Name = "DeleteLogFile") Then
            mnuFileDeletelogfile_Click(sender, e)

        ElseIf (selectedButton.Name = "RefreshFile") Then
            UpdateLogViewer()

        ElseIf (selectedButton.Name = "PrintSetup") Then
            On Error Resume Next
            Me.cdlSyslog_Printer.ShowDialog()

        ElseIf (selectedButton.Name = "Print") Then
            mnuFilePrint_Click(sender, e)

        ElseIf (selectedButton.Name = "Copy") Then
            mnuEditCopy_Click(sender, e)

        ElseIf (selectedButton.Name = "SelectAll") Then
            mnuEditSelectAll_Click(sender, e)

        ElseIf (selectedButton.Name = "Find") Then
            mnuEditFind_Click(sender, e)

        ElseIf (selectedButton.Name = "Top") Then
            Me.rtbLogViewer.Focus()
            SendKeys.Send("^{HOME}")
            Me.rtbLogViewer.Focus()
            EchoToStatusBar("Cursor Sent To Begining of System Log", 1)

        ElseIf (selectedButton.Name = "mEnd") Then
            Me.rtbLogViewer.Focus()
            SendKeys.Send("^{END}")
            Me.rtbLogViewer.Focus()
            EchoToStatusBar("Cursor Sent To End of System Log", 1)

        ElseIf (selectedButton.Name = "PageUp") Then
            Me.rtbLogViewer.Focus()
            SendKeys.Send("{PGUP}")
            Me.rtbLogViewer.Focus()
            EchoToStatusBar("Cursor Up", 1)

        ElseIf (selectedButton.Name = "PageDown") Then
            Me.rtbLogViewer.Focus()
            SendKeys.Send("{PGDN}")
            Me.rtbLogViewer.Focus()
            EchoToStatusBar("Cursor Down", 1)

        End If


    End Sub

    Private Sub frmSyslogViewer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        basLogMain.Main()
    End Sub

    Private Sub mnuFileExit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuFileExit.Click
        Me.Close()
    End Sub
End Class