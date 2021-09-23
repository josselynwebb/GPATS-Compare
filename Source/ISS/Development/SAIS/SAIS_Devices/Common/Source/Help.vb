
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
    Friend WithEvents fraMain As System.Windows.Forms.GroupBox
    Friend WithEvents WebBrowser As AxSHDocVw.AxWebBrowser
    Friend WithEvents TextBox As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmHelp))
        Me.fraMain = New System.Windows.Forms.GroupBox()
        Me.WebBrowser = New AxSHDocVw.AxWebBrowser()
        Me.TextBox = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'fraMain
        '
        Me.fraMain.Controls.AddRange(New System.Windows.Forms.Control() {Me.WebBrowser, Me.TextBox})
        Me.fraMain.Name = "fraMain"
        Me.fraMain.TabIndex = 0
        Me.fraMain.Location = New System.Drawing.Point(0, 0)
        Me.fraMain.Size = New System.Drawing.Size(422, 300)
        Me.fraMain.Text = "Display"
        Me.fraMain.BackColor = System.Drawing.SystemColors.Control
        Me.fraMain.ForeColor = System.Drawing.SystemColors.ControlText
        '
        'WebBrowser
        '
        Me.WebBrowser.TabIndex = 2
        Me.WebBrowser.Location = New System.Drawing.Point(24, 121)
        Me.WebBrowser.Size = New System.Drawing.Size(203, 74)
        Me.WebBrowser.OcxState = CType(resources.GetObject("WebBrowser.OcxState"), System.Windows.Forms.AxHost.State)
        '
        'TextBox
        '
        Me.TextBox.Name = "TextBox"
        Me.TextBox.TabIndex = 1
        Me.TextBox.Location = New System.Drawing.Point(24, 16)
        Me.TextBox.Size = New System.Drawing.Size(203, 82)
        Me.TextBox.Text = "Text1"
        Me.TextBox.BackColor = System.Drawing.SystemColors.Window
        Me.TextBox.ForeColor = System.Drawing.SystemColors.WindowText
        '
        'frmHelp
        '
        Me.ClientSize = New System.Drawing.Size(458, 378)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.fraMain})
        Me.Name = "frmHelp"
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.MinimizeBox = True
        Me.MaximizeBox = True
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultLocation
        Me.Text = "General Form"
        CType(Me.WebBrowser, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraMain.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

	'=========================================================
    Private Sub frmHelp_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        WebBrowser.Silent = True
        WebBrowser.CausesValidation = False
        WebBrowser.Offline = True
        WebBrowser.Silent = True
    End Sub

    Private Sub frmHelp_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize

        If Me.Height<3000 Then
            Exit Sub
        End If

        If Me.Width<3000 Then
            Exit Sub
        End If

        fraMain.Left = 10
        fraMain.Top = 10
        fraMain.Width = Me.Width-50
        fraMain.Height = Me.Height-600
        WebBrowser.Left = fraMain.Left-10
        WebBrowser.Top = fraMain.Top-10
        WebBrowser.Width = fraMain.Width-20
        WebBrowser.Height = fraMain.Height-20

    End Sub

    Private Sub Form_Unload(ByRef Cancel As Short)

        Application.DoEvents()
        WebBrowser.Stop()

    End Sub

    Private Sub frmHelp_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Dim Cancel As Short = 0
        Form_Unload(Cancel)
        If Cancel <> 0 Then e.Cancel = True
    End Sub

End Class