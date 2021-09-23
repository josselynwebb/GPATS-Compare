
Imports System
Imports System.Windows.Forms
Imports System.Text
Imports System.Runtime.InteropServices
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility.VB6

Public Class frmToolbar
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
    Friend WithEvents mnuTeteMenu As Microsoft.VisualBasic.Compatibility.VB6.MenuItemArray
    Friend WithEvents mnuShutDown As Microsoft.VisualBasic.Compatibility.VB6.MenuItemArray
    Friend WithEvents trayIcon As System.Windows.Forms.NotifyIcon
    Friend WithEvents trayIconMenuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents VIPERTSystemMonitorToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents VIPERTMenuToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents AboutVIPERTSystemToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ShutDownVIPERTToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LogoutATSToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents pichook As System.Windows.Forms.PictureBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmToolbar))
        Me.mnuTeteMenu = New Microsoft.VisualBasic.Compatibility.VB6.MenuItemArray(Me.components)
        Me.mnuShutDown = New Microsoft.VisualBasic.Compatibility.VB6.MenuItemArray(Me.components)
        Me.pichook = New System.Windows.Forms.PictureBox()
        Me.trayIcon = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.trayIconMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.VIPERTMenuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.VIPERTSystemMonitorToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.AboutVIPERTSystemToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShutDownVIPERTToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LogoutATSToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.mnuTeteMenu, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.mnuShutDown, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pichook, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.trayIconMenuStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'mnuTeteMenu
        '
        '
        'mnuShutDown
        '
        '
        'pichook
        '
        Me.pichook.BackColor = System.Drawing.SystemColors.Control
        Me.pichook.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pichook.Location = New System.Drawing.Point(299, 4)
        Me.pichook.Name = "pichook"
        Me.pichook.Size = New System.Drawing.Size(58, 37)
        Me.pichook.TabIndex = 0
        Me.pichook.TabStop = False
        '
        'trayIcon
        '
        Me.trayIcon.ContextMenuStrip = Me.trayIconMenuStrip
        Me.trayIcon.Icon = CType(resources.GetObject("trayIcon.Icon"), System.Drawing.Icon)
        Me.trayIcon.Text = "ATS System Monitor"
        Me.trayIcon.Visible = True
        '
        'trayIconMenuStrip
        '
        Me.trayIconMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.VIPERTMenuToolStripMenuItem, Me.VIPERTSystemMonitorToolStripMenuItem, Me.ToolStripSeparator1, Me.AboutVIPERTSystemToolStripMenuItem, Me.LogoutATSToolStripMenuItem, Me.ShutDownVIPERTToolStripMenuItem})
        Me.trayIconMenuStrip.Name = "trayIconMenuStrip"
        Me.trayIconMenuStrip.Size = New System.Drawing.Size(187, 142)
        '
        'VIPERTMenuToolStripMenuItem
        '
        Me.VIPERTMenuToolStripMenuItem.Name = "VIPERTMenuToolStripMenuItem"
        Me.VIPERTMenuToolStripMenuItem.Size = New System.Drawing.Size(186, 22)
        Me.VIPERTMenuToolStripMenuItem.Text = "ATS Menu"
        '
        'VIPERTSystemMonitorToolStripMenuItem
        '
        Me.VIPERTSystemMonitorToolStripMenuItem.Name = "VIPERTSystemMonitorToolStripMenuItem"
        Me.VIPERTSystemMonitorToolStripMenuItem.Size = New System.Drawing.Size(186, 22)
        Me.VIPERTSystemMonitorToolStripMenuItem.Text = "View System Monitor"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(183, 6)
        '
        'AboutVIPERTSystemToolStripMenuItem
        '
        Me.AboutVIPERTSystemToolStripMenuItem.Name = "AboutVIPERTSystemToolStripMenuItem"
        Me.AboutVIPERTSystemToolStripMenuItem.Size = New System.Drawing.Size(186, 22)
        Me.AboutVIPERTSystemToolStripMenuItem.Text = "About ATS"
        '
        'ShutDownVIPERTToolStripMenuItem
        '
        Me.ShutDownVIPERTToolStripMenuItem.Name = "ShutDownVIPERTToolStripMenuItem"
        Me.ShutDownVIPERTToolStripMenuItem.Size = New System.Drawing.Size(186, 22)
        Me.ShutDownVIPERTToolStripMenuItem.Text = "Shut Down ATS"
        '
        'LogoutATSToolStripMenuItem
        '
        Me.LogoutATSToolStripMenuItem.Name = "LogoutATSToolStripMenuItem"
        Me.LogoutATSToolStripMenuItem.Size = New System.Drawing.Size(186, 22)
        Me.LogoutATSToolStripMenuItem.Text = "Logout ATS"
        '
        'frmToolbar
        '
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(359, 90)
        Me.ControlBox = False
        Me.Controls.Add(Me.pichook)
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmToolbar"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Toolbar, PopUp, and Status Byte Text Container"
        CType(Me.mnuTeteMenu, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.mnuShutDown, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pichook, System.ComponentModel.ISupportInitialize).EndInit()
        Me.trayIconMenuStrip.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    '=========================================================
    '************************************************************
    '* Virtual Instrument Portable Equipment Repair/Test(VIPERT)*
    '*                                                          *
    '* Nomenclature   : SYSTEM: System Monitor Toolbar          *
    '* Purpose        : Displays the taskbar Icon and popup     *
    '*                  Menu.                                   *
    '************************************************************


    <StructLayout(LayoutKind.Sequential)> _
    Structure NOTIFYICONDATA
        Dim cbSize As Integer
        Dim hWnd As Integer
        Dim uId As Integer
        Dim uFlags As Integer
        Dim ucallbackMessage As Integer
        
        Dim hIcon As Integer
        
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=64), VBFixedString(64)> Dim szTip As String
    End Structure

    Private Const NIM_ADD As Short = &H0
    Private Const NIM_MODIFY As Short = &H1
    Private Const NIM_DELETE As Short = &H2
    Private Const WM_MOUSEMOVE As Short = &H200
    Private Const NIF_MESSAGE As Short = &H1
    Private Const NIF_ICON As Short = &H2
    Private Const NIF_TIP As Short = &H4

    Private Const WM_LBUTTONDBLCLK As Short = &H203
    Private Const WM_LBUTTONDOWN As Short = &H201
    Private Const WM_LBUTTONUP As Short = &H202
    Private Const WM_RBUTTONDBLCLK As Short = &H206
    Private Const WM_RBUTTONDOWN As Short = &H204
    Private Const WM_RBUTTONUP As Short = &H205

    Private Declare Function Shell_NotifyIcon Lib "shell32" Alias "Shell_NotifyIconA" (ByVal dwMessage As Integer, ByRef pnid As NOTIFYICONDATA) As Boolean
    Dim t As NOTIFYICONDATA





    Private Sub frmToolbar_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        t.cbSize = Len(t)
        t.hWnd = pichook.Handle.ToInt32
        t.uId = 1
        t.uFlags = NIF_ICON Or NIF_TIP Or NIF_MESSAGE
        t.ucallbackMessage = WM_MOUSEMOVE
        t.hIcon = Me.Icon.Handle
        t.szTip = "ATS System Monitor" & Convert.ToString(Chr(0))
        'Shell_NotifyIcon(NIM_ADD, t)
        Me.Hide()
        'AppTaskVisible = False
        Me.ShowInTaskbar = False

    End Sub

    Private Sub Form_QueryUnload(ByRef Cancel As Short, ByVal UnloadMode As Short)

        t.cbSize = Len(t)
        t.hWnd = pichook.Handle.ToInt32
        t.uId = 1
        Shell_NotifyIcon(NIM_DELETE, t)

    End Sub

    Private Sub frmToolbar_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Dim Cancel As Short = 0
        Form_QueryUnload(Cancel, 0)
        If Cancel <> 0 Then
            e.Cancel = True
            Exit Sub
        End If
    End Sub



    Private Sub mnuAboutSystem_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        'frmAbout.cmdOk.Visible = True

        If Not (frmSplash.Visible) Then
            frmSplash.cmdOK.Visible = True
            CenterForm(frmSplash)
            frmSplash.Show()
            frmSplash.Refresh()
        End If

    End Sub

    Private Sub mnuShutDown_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuShutDown.Click
        Dim Index As Short = mnuShutDown.GetIndex(sender)

        
        Dim Answer As DialogResult

        Answer = MsgBox("Shut Down ATS System?", MsgBoxStyle.Question + MsgBoxStyle.YesNoCancel, "ATS System Shut Down")
        If Answer = DialogResult.Yes Then
            ShutDownSysmon()
        End If

    End Sub




    Private Sub mnuTeteMenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuTeteMenu.Click
        Dim Index As Short = mnuTeteMenu.GetIndex(sender)

        Dim hWindShell As Integer

        'Disable in Factory Version
        If SING_CHASSIS_OPTION Then Exit Sub

        If FileExists(VIPERTMenuFilePath) Then
            hWindShell = Shell(Q & VIPERTMenuFilePath & Q, AppWinStyle.NormalFocus)
        Else
            MsgBox("System Menu File Not Found " & VIPERTMenuFilePath, MsgBoxStyle.Exclamation)
        End If

    End Sub

    Private Sub mnuViewSystemMonitor_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If frmSysMon.Visible = False Then
            frmSysMon.WindowState = 0
            CenterForm(frmSysMon)
            frmSysMon.Show()
            UpdateSysMonGui(PrimaryChassis)
            UpdateSysMonGui(SecondaryChassis)
            frmSysMon.tabSysmon.SelectedIndex = 0
        End If

    End Sub

    Private Sub pichook_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pichook.MouseMove
        Dim Button As Short = e.Button \ &H100000
        Dim Shift As Short = Control.ModifierKeys \ &H10000
        Dim X As Single = VB6.PixelsToTwipsX(e.X)
        Dim Y As Single = VB6.PixelsToTwipsY(e.Y)


        Static rec As Boolean
        
        Static msg As Integer

        msg = X / VB6.TwipsPerPixelX
        If rec = False Then
            rec = True
            Select Case msg
                Case WM_LBUTTONDBLCLK
                    If frmSysMon.Visible = False Then
                        frmSysMon.WindowState = 0
                        CenterForm(frmSysMon)
                        UpdateSysMonGui(PrimaryChassis)
                        UpdateSysMonGui(SecondaryChassis)
                        frmSysMon.Show()
                        frmSysMon.tabSysmon.SelectedIndex = 0
                    End If
                Case WM_LBUTTONDOWN
                Case WM_LBUTTONUP
                Case WM_RBUTTONDBLCLK
                Case WM_RBUTTONDOWN
                Case WM_RBUTTONUP
                    Application.DoEvents()
                    'ShowPopupMenu(mnuBar.Enabled, Me, e.Location)
            End Select
            rec = False
        End If

    End Sub

    Private Sub VIPERTSystemMonitorToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VIPERTSystemMonitorToolStripMenuItem.Click
        frmSysMon.Show()
    End Sub

    Private Sub ShutDownVIPERTToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShutDownVIPERTToolStripMenuItem.Click
        SHUTDOWN_FROM_SYSMON = True
        ShutDownSysmon()
    End Sub

    Private Sub AboutVIPERTSystemToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutVIPERTSystemToolStripMenuItem.Click
        frmSplash.ShowDialog()
    End Sub

    Private Sub VIPERTMenuToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VIPERTMenuToolStripMenuItem.Click

        Dim sysmenuPath = GatherIniFileInformation("File Locations", "SYSTEM_MENU", "C:\Program Files (x86)\ATS\SYSMENU.EXE")
        Try
            Process.Start(sysmenuPath)
        Catch ex As Exception
            MessageBox.Show("Could not find " & sysmenuPath, "Error")
        End Try

    End Sub

    Private Sub trayIcon_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles trayIcon.MouseDoubleClick
        frmSysMon.Show()
    End Sub

    Private Sub LogoutATSToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LogoutATSToolStripMenuItem.Click
        LOGOUT_FROM_SYSMON = True
        SHUTDOWN_FROM_SYSMON = True
        ShutDownSysmon()
    End Sub
End Class