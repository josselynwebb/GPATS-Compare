
Imports System.Windows.Forms

Public Class frmVCC
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
    Friend WithEvents Display As System.Windows.Forms.Panel
    Friend WithEvents Update_Status As System.Windows.Forms.Timer
    Friend WithEvents sbStatusBar As System.Windows.Forms.StatusBar
    Friend WithEvents sbStatusBar_Panel1 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents sbStatusBar_Panel2 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents sbStatusBar_Panel3 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents sbStatusBar_Panel4 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents sbStatusBar_Panel5 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents mnuExit As System.Windows.Forms.MenuItem
    Friend WithEvents mnu_Options As System.Windows.Forms.MenuItem
    Friend WithEvents mnu_Zoom As System.Windows.Forms.MenuItem
    Friend WithEvents mnuHelp As System.Windows.Forms.MenuItem
    Friend WithEvents mnuHelpBar1 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuHelpAbout As System.Windows.Forms.MenuItem
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmVCC))
        Me.Display = New System.Windows.Forms.Panel()
        Me.Update_Status = New System.Windows.Forms.Timer(Me.components)
        Me.sbStatusBar = New System.Windows.Forms.StatusBar()
        Me.sbStatusBar_Panel1 = New System.Windows.Forms.StatusBarPanel()
        Me.sbStatusBar_Panel2 = New System.Windows.Forms.StatusBarPanel()
        Me.sbStatusBar_Panel3 = New System.Windows.Forms.StatusBarPanel()
        Me.sbStatusBar_Panel4 = New System.Windows.Forms.StatusBarPanel()
        Me.sbStatusBar_Panel5 = New System.Windows.Forms.StatusBarPanel()
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.mnuExit = New System.Windows.Forms.MenuItem()
        Me.mnu_Options = New System.Windows.Forms.MenuItem()
        Me.mnu_Zoom = New System.Windows.Forms.MenuItem()
        Me.mnuHelp = New System.Windows.Forms.MenuItem()
        Me.mnuHelpBar1 = New System.Windows.Forms.MenuItem()
        Me.mnuHelpAbout = New System.Windows.Forms.MenuItem()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        CType(Me.sbStatusBar_Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sbStatusBar_Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sbStatusBar_Panel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sbStatusBar_Panel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sbStatusBar_Panel5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Display
        '
        Me.Display.BackColor = System.Drawing.SystemColors.Control
        Me.Display.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Display.Location = New System.Drawing.Point(0, 0)
        Me.Display.Name = "Display"
        Me.Display.Size = New System.Drawing.Size(551, 381)
        Me.Display.TabIndex = 1
        '
        'Update_Status
        '
        Me.Update_Status.Enabled = True
        Me.Update_Status.Interval = 1000
        '
        'sbStatusBar
        '
        Me.sbStatusBar.Location = New System.Drawing.Point(0, 476)
        Me.sbStatusBar.Name = "sbStatusBar"
        Me.sbStatusBar.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.sbStatusBar_Panel1, Me.sbStatusBar_Panel2, Me.sbStatusBar_Panel3, Me.sbStatusBar_Panel4, Me.sbStatusBar_Panel5})
        Me.sbStatusBar.ShowPanels = True
        Me.sbStatusBar.Size = New System.Drawing.Size(652, 18)
        Me.sbStatusBar.SizingGrip = False
        Me.sbStatusBar.TabIndex = 0
        '
        'sbStatusBar_Panel1
        '
        Me.sbStatusBar_Panel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
        Me.sbStatusBar_Panel1.Name = "sbStatusBar_Panel1"
        Me.sbStatusBar_Panel1.Text = "Board: 0"
        Me.sbStatusBar_Panel1.ToolTipText = "Capture Board selected"
        Me.sbStatusBar_Panel1.Width = 402
        '
        'sbStatusBar_Panel2
        '
        Me.sbStatusBar_Panel2.Name = "sbStatusBar_Panel2"
        Me.sbStatusBar_Panel2.Text = "Camera: 0"
        Me.sbStatusBar_Panel2.ToolTipText = "Current Camera"
        '
        'sbStatusBar_Panel3
        '
        Me.sbStatusBar_Panel3.Name = "sbStatusBar_Panel3"
        Me.sbStatusBar_Panel3.Text = "Mode: DIB"
        '
        'sbStatusBar_Panel4
        '
        Me.sbStatusBar_Panel4.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents
        Me.sbStatusBar_Panel4.Name = "sbStatusBar_Panel4"
        Me.sbStatusBar_Panel4.Text = "0 X 0"
        Me.sbStatusBar_Panel4.ToolTipText = "Image Size"
        Me.sbStatusBar_Panel4.Width = 40
        '
        'sbStatusBar_Panel5
        '
        Me.sbStatusBar_Panel5.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents
        Me.sbStatusBar_Panel5.Name = "sbStatusBar_Panel5"
        Me.sbStatusBar_Panel5.Width = 10
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuExit, Me.mnu_Options, Me.mnuHelp})
        '
        'mnuExit
        '
        Me.mnuExit.Index = 0
        Me.mnuExit.Text = "&Exit"
        Me.mnuExit.Visible = False
        '
        'mnu_Options
        '
        Me.mnu_Options.Index = 1
        Me.mnu_Options.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnu_Zoom})
        Me.mnu_Options.Text = "Options"
        Me.mnu_Options.Visible = False
        '
        'mnu_Zoom
        '
        Me.mnu_Zoom.Index = 0
        Me.mnu_Zoom.Text = "Auto Zoom"
        '
        'mnuHelp
        '
        Me.mnuHelp.Index = 2
        Me.mnuHelp.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuHelpBar1, Me.mnuHelpAbout})
        Me.mnuHelp.Text = "&Help"
        Me.mnuHelp.Visible = False
        '
        'mnuHelpBar1
        '
        Me.mnuHelpBar1.Index = 0
        Me.mnuHelpBar1.Text = "-"
        '
        'mnuHelpAbout
        '
        Me.mnuHelpAbout.Index = 1
        Me.mnuHelpAbout.Text = "&About VBExample..."
        '
        'frmVCC
        '
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(652, 494)
        Me.Controls.Add(Me.Display)
        Me.Controls.Add(Me.sbStatusBar)
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Menu = Me.MainMenu1
        Me.Name = "frmVCC"
        Me.Text = "RS-170 CROSS"
        CType(Me.sbStatusBar_Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sbStatusBar_Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sbStatusBar_Panel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sbStatusBar_Panel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sbStatusBar_Panel5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

End Sub

#End Region

    '**************************************************************
    '* Nomenclature   : ATS-ViperT SYSTEM SELF TEST               *
    '*                  Video Capture Card                        *
    '* Version        : 2.0                                       *
    '* Last Update    : Mar 1, 2017                               *
    '**************************************************************


    Private Declare Function OSWinHelp Lib "user32" Alias "WinHelpA" (ByVal hwnd As Integer, ByVal HelpFile As String, ByVal wCommand As Short, ByRef dwData As System.Delegate) As Object

    Private Sub frmVCC_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        ''Dim translation As Single

        ''translation = (Me.Width/Me.ClientRectangle.Width)
        ''Me.Width = translation*Image_DX
        ''Me.Height = translation*(Image_DY+sbStatusBar.Height)

        ''CreateSourcesAndSinks()
        ''CreateHostConn()
        ''grab_IFC()

    End Sub

    Private Sub frmVCC_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize

        Display.Width = Me.ClientRectangle.Width
        If (Me.ClientRectangle.Height-sbStatusBar.Height>0) Then
            Display.Height = Me.ClientRectangle.Height-sbStatusBar.Height
        Else
            Display.Height = 0
        End If

    End Sub

    Private Sub Form_Unload(ByRef Cancel As Short)

        ''Dim i As Short
        ''If Me.WindowState<>FormWindowState.Minimized Then
        ''    SaveSetting(My.Application.Info.AssemblyName, "Settings", "MainLeft", Me.Left)
        ''    SaveSetting(My.Application.Info.AssemblyName, "Settings", "MainTop", Me.Top)
        ''    SaveSetting(My.Application.Info.AssemblyName, "Settings", "MainWidth", Me.Width)
        ''    SaveSetting(My.Application.Info.AssemblyName, "Settings", "MainHeight", Me.Height)
        ''End If
        ''Pr_Stop = True ' Stop any processing, if there is
        ' ''SHutdown_IFC

    End Sub

    Private Sub frmVCC_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Dim Cancel As Short = 0
        Form_Unload(Cancel)
        If Cancel <> 0 Then e.Cancel = True
    End Sub

    Private Sub mnu_Zoom_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnu_Zoom.Click

        ''mnu_Zoom.Checked =  Not mnu_Zoom.Checked
        ''If mnu_Zoom.Checked Then
        ''    ZoomAutoSize = True
        ''Else
        ''    ZoomAutoSize = False
        ''End If

    End Sub

    Private Sub mnuExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuExit.Click

        ''Pr_Stop = True ' Stop any processing, if there is
        ''Close()

    End Sub

    Private Sub mnuHelpAbout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuHelpAbout.Click

        frmAbout.ShowDialog()

    End Sub

    Private Sub Update_Status_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Update_Status.Tick

        ''sbStatusBar.Panels(1 - 1).Text = "Board: " & Current_Board
        ''sbStatusBar.Panels(2 - 1).Text = "Camera: " & Current_Camera
        ''If Current_Mode=IFC_DDRAW_SINK Then
        ''    sbStatusBar.Panels(3 - 1).Text = "Mode: DDRAW"
        ''Else
        ''    sbStatusBar.Panels(3 - 1).Text = "Mode: DIB"
        ''End If

        ''sbStatusBar.Panels(4 - 1).Text = Image_DX & "X" & Image_DY

    End Sub

End Class