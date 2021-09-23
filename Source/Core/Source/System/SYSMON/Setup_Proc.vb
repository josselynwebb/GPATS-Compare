
Imports System.Windows.Forms
Imports Microsoft.VisualBasic
Imports Microsoft.Win32
Imports System.Text

Public Class Setup_Proc
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
    Friend WithEvents run_setup As System.Windows.Forms.Form
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents WaitTimer As System.Windows.Forms.Timer
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents run_setup_13 As Form
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Setup_Proc))
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.WaitTimer = New System.Windows.Forms.Timer(Me.components)
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.ForeColor = System.Drawing.Color.Black
        Me.Button1.Image = Global.sysmon.My.Resources.Resources.Setup
        Me.Button1.Location = New System.Drawing.Point(98, 40)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(35, 35)
        Me.Button1.TabIndex = 0
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.Color.Gray
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(216, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Press this button to run the Setup Procedure"
        '
        'WaitTimer
        '
        Me.WaitTimer.Interval = 1000
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.Color.Gray
        Me.Label2.Location = New System.Drawing.Point(161, 51)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(41, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "10 Sec"
        '
        'Setup_Proc
        '
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(238, 107)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button1)
        Me.ForeColor = System.Drawing.SystemColors.Menu
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Setup_Proc"
        Me.ShowInTaskbar = False
        Me.Text = "Setup Procedure"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

	'=========================================================
    Dim RetVal As Integer
    Public flag As Boolean
    Dim WaitTime = 10


    Public Sub Setup_Proc_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        flag = False
    End Sub

    Private Sub WaitTimer_Tick(sender As Object, e As EventArgs) Handles WaitTimer.Tick
        Label2.Text = WaitTime & " Sec"
        If (WaitTime = 0) Then
            WaitTimer.Enabled = False
            Me.Close()
        End If
        Me.WaitTime -= 1

    End Sub

    Private Sub run_setup_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim setupProcPath As New StringBuilder(255)
        Dim lpFileName As String
        Dim systemType As New StringBuilder(255)
        Dim setupHTMLFile As String

        lpFileName = Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\ATS\", "IniFilePath", Nothing)

        GetPrivateProfileString("File Locations", "Setup_Proc_Path", "", setupProcPath, 255, lpFileName)
        GetPrivateProfileString("System Startup", "SYSTEM_TYPE", "", systemType, 255, lpFileName)

        If (systemType.ToString().Contains("AN/USM-657(V)2")) Then
            setupHTMLFile = "\TETS_SETUP.mht"
        Else
            setupHTMLFile = "\VIPERT_SETUP.mht"
        End If

        Shell("C:\Program Files\Internet Explorer\IEXPLORE.exe" & " " & setupProcPath.ToString & setupHTMLFile, AppWinStyle.NormalFocus)
        flag = True

    End Sub
End Class