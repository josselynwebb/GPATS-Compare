
Imports System.Windows.Forms

Public Class frmDialog
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
    Friend WithEvents pbarInit As System.Windows.Forms.ProgressBar
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    Friend WithEvents lblDialog As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDialog))
        Me.pbarInit = New System.Windows.Forms.ProgressBar()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.lblDialog = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'pbarInit
        '
        Me.pbarInit.Location = New System.Drawing.Point(194, 32)
        Me.pbarInit.Maximum = 120
        Me.pbarInit.Name = "pbarInit"
        Me.pbarInit.Size = New System.Drawing.Size(82, 17)
        Me.pbarInit.TabIndex = 2
        Me.pbarInit.Visible = False
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Location = New System.Drawing.Point(198, 49)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(76, 23)
        Me.cmdClose.TabIndex = 0
        Me.cmdClose.Text = "&Shut Down"
        Me.cmdClose.UseVisualStyleBackColor = False
        Me.cmdClose.Visible = False
        '
        'lblDialog
        '
        Me.lblDialog.BackColor = System.Drawing.SystemColors.Control
        Me.lblDialog.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDialog.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDialog.Location = New System.Drawing.Point(8, 8)
        Me.lblDialog.Name = "lblDialog"
        Me.lblDialog.Size = New System.Drawing.Size(264, 20)
        Me.lblDialog.TabIndex = 1
        Me.lblDialog.Text = "Dialog Label -----------------------------"
        '
        'frmDialog
        '
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(279, 78)
        Me.ControlBox = False
        Me.Controls.Add(Me.pbarInit)
        Me.Controls.Add(Me.cmdClose)
        Me.Controls.Add(Me.lblDialog)
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmDialog"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Automated Test System (ATS)"
        Me.ResumeLayout(False)

    End Sub

#End Region

	'=========================================================
    '************************************************************
    '* Virtual Instrument Portable Equipment Repair/Test(VIPERT)*
    '*                                                          *
    '* Nomenclature   : SYSTEM: System Monitor CheckList Form   *
    '* Purpose        : Displays actions taken to user. such as *
    '*                  FPU Startup and Shutdown.               *
    '************************************************************


    Private Sub cmdClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdClose.Click

        Me.Visible = False

    End Sub


    Private Sub frmDialog_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        BringToTop(Me.Handle.ToInt32)
    End Sub



End Class