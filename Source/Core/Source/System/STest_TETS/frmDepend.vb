
Imports System.Windows.Forms

Public Class frmDepend
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
    Friend WithEvents cmdOk As System.Windows.Forms.Button
    Friend WithEvents txtInstrumentList As System.Windows.Forms.RichTextBox
    Friend WithEvents panTitle As System.Windows.Forms.Label
    Friend WithEvents SSPanel1 As System.Windows.Forms.Label
    Friend WithEvents Image1 As System.Windows.Forms.PictureBox

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDepend))
        Me.cmdOk = New System.Windows.Forms.Button()
        Me.txtInstrumentList = New System.Windows.Forms.RichTextBox()
        Me.panTitle = New System.Windows.Forms.Label()
        Me.SSPanel1 = New System.Windows.Forms.Label()
        Me.Image1 = New System.Windows.Forms.PictureBox()
        CType(Me.Image1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdOk
        '
        Me.cmdOk.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOk.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOk.Location = New System.Drawing.Point(221, 199)
        Me.cmdOk.Name = "cmdOk"
        Me.cmdOk.Size = New System.Drawing.Size(73, 21)
        Me.cmdOk.TabIndex = 0
        Me.cmdOk.Text = "&Ok"
        Me.cmdOk.UseVisualStyleBackColor = False
        '
        'txtInstrumentList
        '
        Me.txtInstrumentList.BackColor = System.Drawing.SystemColors.Control
        Me.txtInstrumentList.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInstrumentList.Location = New System.Drawing.Point(1, 65)
        Me.txtInstrumentList.Name = "txtInstrumentList"
        Me.txtInstrumentList.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
        Me.txtInstrumentList.Size = New System.Drawing.Size(300, 114)
        Me.txtInstrumentList.TabIndex = 1
        Me.txtInstrumentList.Text = ""
        '
        'panTitle
        '
        Me.panTitle.BackColor = System.Drawing.Color.SteelBlue
        Me.panTitle.ForeColor = System.Drawing.Color.White
        Me.panTitle.Location = New System.Drawing.Point(55, 22)
        Me.panTitle.Name = "panTitle"
        Me.panTitle.Size = New System.Drawing.Size(246, 40)
        Me.panTitle.TabIndex = 3
        Me.panTitle.Text = "Test Instrument Name"
        '
        'SSPanel1
        '
        Me.SSPanel1.BackColor = System.Drawing.Color.SteelBlue
        Me.SSPanel1.ForeColor = System.Drawing.Color.White
        Me.SSPanel1.Location = New System.Drawing.Point(55, 4)
        Me.SSPanel1.Name = "SSPanel1"
        Me.SSPanel1.Size = New System.Drawing.Size(246, 14)
        Me.SSPanel1.TabIndex = 2
        Me.SSPanel1.Text = "The following Instruments are used to test the"
        '
        'Image1
        '
        Me.Image1.BackgroundImage = CType(resources.GetObject("Image1.BackgroundImage"), System.Drawing.Image)
        Me.Image1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.Image1.InitialImage = CType(resources.GetObject("Image1.InitialImage"), System.Drawing.Image)
        Me.Image1.Location = New System.Drawing.Point(9, 5)
        Me.Image1.Margin = New System.Windows.Forms.Padding(1)
        Me.Image1.Name = "Image1"
        Me.Image1.Size = New System.Drawing.Size(26, 27)
        Me.Image1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.Image1.TabIndex = 9
        Me.Image1.TabStop = False
        '
        'frmDepend
        '
        Me.BackColor = System.Drawing.Color.SteelBlue
        Me.ClientSize = New System.Drawing.Size(305, 227)
        Me.Controls.Add(Me.Image1)
        Me.Controls.Add(Me.cmdOk)
        Me.Controls.Add(Me.txtInstrumentList)
        Me.Controls.Add(Me.panTitle)
        Me.Controls.Add(Me.SSPanel1)
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(18, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmDepend"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Instrument Test Dependancy"
        CType(Me.Image1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    '**************************************************************
    '* Nomenclature   : ATS-TETS SYSTEM SELF TEST                 *
    '*                  Instrument Dependancy                     *
    '* Version        : 2.0                                       *
    '* Last Update    : Apr 1, 2017                               *
    '* Purpose        : This form is used to identify to the user *
    '*                  the instruments a test is dependant upon. *
    '**************************************************************



    Private Sub cmdOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdOk.Click

        Me.Visible = False

    End Sub


    Private Sub frmDepend_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated

        cmdOk.Focus()

    End Sub

    Private Sub frmDepend_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = e.KeyCode
        Dim Shift As Short = e.KeyData \ &H10000


        Dim key_escape As Short = 27

        If KeyCode=key_escape Then ' user can close form via escape key
            Me.Close()
        End If


    End Sub
End Class