
Imports System.Windows.Forms
Imports System.Text
Imports Microsoft.VisualBasic

Public Class frmSplash
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
    Friend WithEvents cmdOK As System.Windows.Forms.Button
    Friend WithEvents Frame1 As System.Windows.Forms.Panel
    Friend WithEvents Image1 As System.Windows.Forms.PictureBox
    Friend WithEvents lblProductName As System.Windows.Forms.Label
    Friend WithEvents lblHardwareSn As System.Windows.Forms.Label
    Friend WithEvents lblSwRelease As System.Windows.Forms.Label
    Friend WithEvents lblControllerName As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSplash))
        Me.cmdOK = New System.Windows.Forms.Button()
        Me.Frame1 = New System.Windows.Forms.Panel()
        Me.Image1 = New System.Windows.Forms.PictureBox()
        Me.lblProductName = New System.Windows.Forms.Label()
        Me.lblHardwareSn = New System.Windows.Forms.Label()
        Me.lblSwRelease = New System.Windows.Forms.Label()
        Me.lblControllerName = New System.Windows.Forms.Label()
        Me.Frame1.SuspendLayout()
        CType(Me.Image1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdOK
        '
        Me.cmdOK.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOK.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOK.Location = New System.Drawing.Point(352, 131)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.Size = New System.Drawing.Size(82, 33)
        Me.cmdOK.TabIndex = 6
        Me.cmdOK.Text = "OK"
        Me.cmdOK.UseVisualStyleBackColor = False
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.cmdOK)
        Me.Frame1.Controls.Add(Me.Image1)
        Me.Frame1.Controls.Add(Me.lblProductName)
        Me.Frame1.Controls.Add(Me.lblHardwareSn)
        Me.Frame1.Controls.Add(Me.lblSwRelease)
        Me.Frame1.Controls.Add(Me.lblControllerName)
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 0)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Size = New System.Drawing.Size(461, 169)
        Me.Frame1.TabIndex = 0
        '
        'Image1
        '
        Me.Image1.BackColor = System.Drawing.SystemColors.Control
        Me.Image1.Image = CType(resources.GetObject("Image1.Image"), System.Drawing.Image)
        Me.Image1.Location = New System.Drawing.Point(246, 54)
        Me.Image1.Name = "Image1"
        Me.Image1.Size = New System.Drawing.Size(188, 71)
        Me.Image1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.Image1.TabIndex = 0
        Me.Image1.TabStop = False
        '
        'lblProductName
        '
        Me.lblProductName.AutoSize = True
        Me.lblProductName.BackColor = System.Drawing.SystemColors.Control
        Me.lblProductName.Font = New System.Drawing.Font("Arial", 32.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProductName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblProductName.Location = New System.Drawing.Point(29, 0)
        Me.lblProductName.Name = "lblProductName"
        Me.lblProductName.Size = New System.Drawing.Size(403, 51)
        Me.lblProductName.TabIndex = 2
        Me.lblProductName.Text = "AN/USM-657A(V)2 "
        '
        'lblHardwareSn
        '
        Me.lblHardwareSn.AutoSize = True
        Me.lblHardwareSn.BackColor = System.Drawing.Color.Transparent
        Me.lblHardwareSn.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblHardwareSn.Location = New System.Drawing.Point(21, 71)
        Me.lblHardwareSn.Name = "lblHardwareSn"
        Me.lblHardwareSn.Size = New System.Drawing.Size(160, 13)
        Me.lblHardwareSn.TabIndex = 5
        Me.lblHardwareSn.Text = "System Serial Number: S/N XXX"
        '
        'lblSwRelease
        '
        Me.lblSwRelease.AutoSize = True
        Me.lblSwRelease.BackColor = System.Drawing.Color.Transparent
        Me.lblSwRelease.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSwRelease.Location = New System.Drawing.Point(21, 87)
        Me.lblSwRelease.Name = "lblSwRelease"
        Me.lblSwRelease.Size = New System.Drawing.Size(151, 13)
        Me.lblSwRelease.TabIndex = 4
        Me.lblSwRelease.Text = "System Software Release: X.X"
        '
        'lblControllerName
        '
        Me.lblControllerName.AutoSize = True
        Me.lblControllerName.BackColor = System.Drawing.Color.Transparent
        Me.lblControllerName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblControllerName.Location = New System.Drawing.Point(21, 103)
        Me.lblControllerName.Name = "lblControllerName"
        Me.lblControllerName.Size = New System.Drawing.Size(198, 13)
        Me.lblControllerName.TabIndex = 3
        Me.lblControllerName.Text = "Instrument Controller: VIPERT_IC_XXXX"
        '
        'frmSplash
        '
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(461, 174)
        Me.ControlBox = False
        Me.Controls.Add(Me.Frame1)
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSplash"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        CType(Me.Image1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

End Sub

#End Region

	'=========================================================
    '************************************************************
    '* Virtual Instrument Portable Equipment Repair/Test(VIPERT)*
    '*                                                          *
    '* Nomenclature   : SYSTEM: System Monitor Splash Form      *
    '* Purpose        : Identifies the System as well as        *
    '*                  Software Release, System SN and IC SN.  *
    '************************************************************

    Private Sub cmdOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        Me.Close()
    End Sub

    Private Sub frmSplash_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        lblHardwareSn.Text = "System Serial Number: S/N " & GatherIniFileInformation("System Startup", "SN", "Unknown")
        lblSwRelease.Text = "System Software Release: " & GatherIniFileInformation("System Startup", "SWR", "Unknown")
        lblProductName.Text = GatherIniFileInformation("System Startup", "SYSTEM_TYPE", "Unknown")

        lblControllerName.Text = "Instrument Controller: " & System.Environment.MachineName

        Dim EO_OPT As String
        Dim RF_OPT As String
        ' to check which version RF, EO or RF/EO
        RF_OPT = GatherIniFileInformation("System Startup", "RF_OPTION_INSTALLED", "")
        EO_OPT = GatherIniFileInformation("System Startup", "EO_OPTION_INSTALLED", "")

    End Sub



End Class