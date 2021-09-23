
Imports System.Windows.Forms

Public Class frmHelp
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
    Friend WithEvents m3152a As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmHelp))
        Me.m3152a = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'm3152a
        '
        Me.m3152a.BackColor = System.Drawing.SystemColors.Control
        Me.m3152a.ForeColor = System.Drawing.SystemColors.ControlText
        Me.m3152a.Image = CType(resources.GetObject("m3152a.Image"), System.Drawing.Image)
        Me.m3152a.Location = New System.Drawing.Point(121, 16)
        Me.m3152a.Name = "m3152a"
        Me.m3152a.Size = New System.Drawing.Size(98, 163)
        Me.m3152a.TabIndex = 0
        Me.m3152a.Text = "m3152a"
        Me.m3152a.UseVisualStyleBackColor = False
        '
        'frmHelp
        '
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(368, 206)
        Me.Controls.Add(Me.m3152a)
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmHelp"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Help"
        Me.ResumeLayout(False)

End Sub

#End Region

	'=========================================================
    Private Const SW_SHOWNORMAL As Short = 1
    Private Const SW_SHOWMINIMIZED As Short = 2
    Private Const SW_SHOWMAXIMIZED As Short = 3

    Private Declare Function ShellExecute Lib "shell32.dll" Alias "ShellExecuteA" (ByVal hwnd As Integer, ByVal lpOperation As String, ByVal lpFile As String, ByVal lpParameters As String, ByVal lpDirectory As String, ByVal nShowCmd As Integer) As Integer

    Private Sub m3152a_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles m3152a.Click
        Dim helpFile As String = Application.StartupPath + "\..\Config\Help\FG_Ri3152A\m3152a.PDF"
        Dim helpFile2 As String = Application.StartupPath + "\..\Config\Help\FG_Ri3152A\m3152.PDF"
        Dim Version As String
        Dim SystemType As String

        Version = GatherIniFileInformation("System Startup", "SWR", "Unknown")
        SystemType = GatherIniFileInformation("System Startup", "SYSTEM_TYPE", "Unknown")

        If (InStr(SystemType, "AN/USM-657(V)2")) Then
            ShellExecute(0, "Open", helpFile2, 0, 0, SW_SHOWNORMAL)
        Else
            ShellExecute(0, "Open", helpFile, 0, 0, SW_SHOWNORMAL)
        End If

    End Sub

End Class