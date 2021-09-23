
Imports System.Windows.Forms
Imports System.Drawing
Imports Microsoft.VisualBasic
Imports System.Text
Imports Microsoft.Win32

Public Class frmSysPanl
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()
        If bDebug Then WriteDebugInfo("frmSysPanl.New() - constructor called")
        'MessageBox.Show("Sys Menu", "Debug", MessageBoxButtons.OK) 'leave for debugging
        TPSDrive = SysPanel.GetTPSDrive()
        TPSLocation = SysPanel.GetTPSLocation()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        fraFunction(0) = fraFunction_0
        fraFunction(1) = fraFunction_1
        fraFunction(2) = fraFunction_2

        ribProgram(1) = Button1
        ribProgram(2) = Button2
        ribProgram(3) = Button3
        ribProgram(4) = Button4
        ribProgram(5) = Button5
        ribProgram(6) = Button6
        ribProgram(7) = Button7
        ribProgram(8) = Button8
        ribProgram(9) = Button9
        ribProgram(10) = Button10
        ribProgram(11) = Button11
        ribProgram(12) = Button12
        ribProgram(13) = Button13
        ribProgram(14) = Button14
        ribProgram(15) = Button15
        ribProgram(16) = Button16

        lblProgDescription(1) = lblProgDescription_1
        lblProgDescription(2) = lblProgDescription_2
        lblProgDescription(3) = lblProgDescription_3
        lblProgDescription(4) = lblProgDescription_4
        lblProgDescription(5) = lblProgDescription_5
        lblProgDescription(6) = lblProgDescription_6
        lblProgDescription(7) = lblProgDescription_7
        lblProgDescription(8) = lblProgDescription_8
        lblProgDescription(9) = lblProgDescription_9
        lblProgDescription(10) = lblProgDescription_10
        lblProgDescription(11) = lblProgDescription_11
        lblProgDescription(12) = lblProgDescription_12
        lblProgDescription(13) = lblProgDescription_13
        lblProgDescription(14) = lblProgDescription_14
        lblProgDescription(15) = lblProgDescription_15
        lblProgDescription(16) = lblProgDescription_16

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
    Friend fraFunction(3) As System.Windows.Forms.GroupBox
    Friend ribProgram(30) As Button
    Friend lblProgDescription(30) As Label
    Friend WithEvents lblTpsInformation As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    Friend WithEvents lblAttribute As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    Friend WithEvents cmdAbout As System.Windows.Forms.Button
    Friend WithEvents cmdQuit As System.Windows.Forms.Button
    Friend WithEvents Picture3 As System.Windows.Forms.PictureBox
    Friend WithEvents Picture1 As System.Windows.Forms.PictureBox
    Friend WithEvents tabUserOptions As System.Windows.Forms.TabControl
    Friend WithEvents tabUserOptions_Page1 As System.Windows.Forms.TabPage
    Friend WithEvents fraFunction_0 As System.Windows.Forms.GroupBox
    Friend WithEvents lblProgDescription_16 As System.Windows.Forms.Label
    Friend WithEvents lblProgDescription_3 As System.Windows.Forms.Label
    Friend WithEvents lblProgDescription_2 As System.Windows.Forms.Label
    Friend WithEvents lblProgDescription_1 As System.Windows.Forms.Label
    Friend WithEvents tabUserOptions_Page2 As System.Windows.Forms.TabPage
    Friend WithEvents fraFunction_1 As System.Windows.Forms.GroupBox
    Friend WithEvents tabUserOptions_Page3 As System.Windows.Forms.TabPage
    Friend WithEvents fraFunction_2 As System.Windows.Forms.GroupBox
    Friend WithEvents panCdInformation As System.Windows.Forms.Panel
    Friend WithEvents lblTpsInformation_5 As System.Windows.Forms.Label
    Friend WithEvents lblAttribute_2 As System.Windows.Forms.Label
    Friend WithEvents lblTpsInformation_4 As System.Windows.Forms.Label
    Friend WithEvents lblTpsInformation_3 As System.Windows.Forms.Label
    Friend WithEvents lblTpsInformation_2 As System.Windows.Forms.Label
    Friend WithEvents lblTpsInformation_1 As System.Windows.Forms.Label
    Friend WithEvents lblAttribute_1 As System.Windows.Forms.Label
    Friend WithEvents lblAttribute_5 As System.Windows.Forms.Label
    Friend WithEvents lblAttribute_4 As System.Windows.Forms.Label
    Friend WithEvents lblAttribute_3 As System.Windows.Forms.Label
    Friend WithEvents lblTpsInformation_0 As System.Windows.Forms.Label
    Friend WithEvents lblAttribute_0 As System.Windows.Forms.Label
    Friend WithEvents trvTpsList As System.Windows.Forms.TreeView
    Friend WithEvents lblInstructions As System.Windows.Forms.Label
    Friend WithEvents lblProgDescription_9 As System.Windows.Forms.Label
    Friend WithEvents lblProgDescription_8 As System.Windows.Forms.Label
    Friend WithEvents tmrUpdate As System.Windows.Forms.Timer
    Friend WithEvents StatusBar1 As System.Windows.Forms.StatusBar
    Friend WithEvents StatusBar1_Panel1 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button16 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Button15 As System.Windows.Forms.Button
    Friend WithEvents Button11 As System.Windows.Forms.Button
    Friend WithEvents Button10 As System.Windows.Forms.Button
    Friend WithEvents Button13 As System.Windows.Forms.Button
    Friend WithEvents Button12 As System.Windows.Forms.Button
    Friend WithEvents Button14 As System.Windows.Forms.Button
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents lblProgDescription_15 As System.Windows.Forms.Label
    Friend WithEvents lblProgDescription_14 As System.Windows.Forms.Label
    Friend WithEvents lblProgDescription_12 As System.Windows.Forms.Label
    Friend WithEvents lblProgDescription_13 As System.Windows.Forms.Label
    Friend WithEvents lblProgDescription_11 As System.Windows.Forms.Label
    Friend WithEvents lblProgDescription_10 As System.Windows.Forms.Label
    Friend WithEvents lblProgDescription_4 As System.Windows.Forms.Label
    Friend WithEvents lblProgDescription_5 As System.Windows.Forms.Label
    Friend WithEvents lblProgDescription_7 As System.Windows.Forms.Label
    Friend WithEvents lblProgDescription_6 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Button25 As System.Windows.Forms.Button
    Friend WithEvents Button26 As System.Windows.Forms.Button
    Friend WithEvents Button27 As System.Windows.Forms.Button
    Friend WithEvents Button28 As System.Windows.Forms.Button
    Friend WithEvents Button29 As System.Windows.Forms.Button
    Friend WithEvents Button30 As System.Windows.Forms.Button
    Friend WithEvents Button31 As System.Windows.Forms.Button
    Friend WithEvents Button32 As System.Windows.Forms.Button
    Friend WithEvents Button33 As System.Windows.Forms.Button
    Friend WithEvents Button34 As System.Windows.Forms.Button
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Button9 As System.Windows.Forms.Button
    Friend WithEvents Button8 As System.Windows.Forms.Button
    Friend WithEvents Button7 As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Button17 As System.Windows.Forms.Button
    Friend WithEvents lblUser As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSysPanl))
        Me.lblProgDescription_16 = New System.Windows.Forms.Label()
        Me.lblProgDescription_3 = New System.Windows.Forms.Label()
        Me.lblProgDescription_2 = New System.Windows.Forms.Label()
        Me.lblProgDescription_1 = New System.Windows.Forms.Label()
        Me.lblProgDescription_9 = New System.Windows.Forms.Label()
        Me.lblProgDescription_8 = New System.Windows.Forms.Label()
        Me.lblTpsInformation = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.lblTpsInformation_5 = New System.Windows.Forms.Label()
        Me.lblTpsInformation_4 = New System.Windows.Forms.Label()
        Me.lblTpsInformation_3 = New System.Windows.Forms.Label()
        Me.lblTpsInformation_2 = New System.Windows.Forms.Label()
        Me.lblTpsInformation_1 = New System.Windows.Forms.Label()
        Me.lblTpsInformation_0 = New System.Windows.Forms.Label()
        Me.lblAttribute = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.lblAttribute_2 = New System.Windows.Forms.Label()
        Me.lblAttribute_1 = New System.Windows.Forms.Label()
        Me.lblAttribute_5 = New System.Windows.Forms.Label()
        Me.lblAttribute_4 = New System.Windows.Forms.Label()
        Me.lblAttribute_3 = New System.Windows.Forms.Label()
        Me.lblAttribute_0 = New System.Windows.Forms.Label()
        Me.cmdAbout = New System.Windows.Forms.Button()
        Me.cmdQuit = New System.Windows.Forms.Button()
        Me.Picture3 = New System.Windows.Forms.PictureBox()
        Me.Picture1 = New System.Windows.Forms.PictureBox()
        Me.tabUserOptions = New System.Windows.Forms.TabControl()
        Me.tabUserOptions_Page1 = New System.Windows.Forms.TabPage()
        Me.fraFunction_0 = New System.Windows.Forms.GroupBox()
        Me.Button16 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.tabUserOptions_Page2 = New System.Windows.Forms.TabPage()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.Button15 = New System.Windows.Forms.Button()
        Me.lblProgDescription_15 = New System.Windows.Forms.Label()
        Me.Button13 = New System.Windows.Forms.Button()
        Me.Button11 = New System.Windows.Forms.Button()
        Me.Button10 = New System.Windows.Forms.Button()
        Me.Button12 = New System.Windows.Forms.Button()
        Me.Button14 = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.lblProgDescription_14 = New System.Windows.Forms.Label()
        Me.lblProgDescription_12 = New System.Windows.Forms.Label()
        Me.lblProgDescription_13 = New System.Windows.Forms.Label()
        Me.lblProgDescription_11 = New System.Windows.Forms.Label()
        Me.lblProgDescription_10 = New System.Windows.Forms.Label()
        Me.lblProgDescription_4 = New System.Windows.Forms.Label()
        Me.lblProgDescription_5 = New System.Windows.Forms.Label()
        Me.lblProgDescription_7 = New System.Windows.Forms.Label()
        Me.lblProgDescription_6 = New System.Windows.Forms.Label()
        Me.fraFunction_1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Button25 = New System.Windows.Forms.Button()
        Me.Button26 = New System.Windows.Forms.Button()
        Me.Button27 = New System.Windows.Forms.Button()
        Me.Button28 = New System.Windows.Forms.Button()
        Me.Button29 = New System.Windows.Forms.Button()
        Me.Button30 = New System.Windows.Forms.Button()
        Me.Button31 = New System.Windows.Forms.Button()
        Me.Button32 = New System.Windows.Forms.Button()
        Me.Button33 = New System.Windows.Forms.Button()
        Me.Button34 = New System.Windows.Forms.Button()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.tabUserOptions_Page3 = New System.Windows.Forms.TabPage()
        Me.fraFunction_2 = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Button17 = New System.Windows.Forms.Button()
        Me.Button9 = New System.Windows.Forms.Button()
        Me.Button8 = New System.Windows.Forms.Button()
        Me.panCdInformation = New System.Windows.Forms.Panel()
        Me.lblInstructions = New System.Windows.Forms.Label()
        Me.trvTpsList = New System.Windows.Forms.TreeView()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.tmrUpdate = New System.Windows.Forms.Timer(Me.components)
        Me.StatusBar1 = New System.Windows.Forms.StatusBar()
        Me.StatusBar1_Panel1 = New System.Windows.Forms.StatusBarPanel()
        Me.lblUser = New System.Windows.Forms.Label()
        CType(Me.lblTpsInformation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAttribute, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Picture3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Picture1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabUserOptions.SuspendLayout()
        Me.tabUserOptions_Page1.SuspendLayout()
        Me.fraFunction_0.SuspendLayout()
        Me.tabUserOptions_Page2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.fraFunction_1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.tabUserOptions_Page3.SuspendLayout()
        Me.fraFunction_2.SuspendLayout()
        Me.panCdInformation.SuspendLayout()
        CType(Me.StatusBar1_Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblProgDescription_16
        '
        Me.lblProgDescription_16.AutoSize = True
        Me.lblProgDescription_16.BackColor = System.Drawing.Color.Transparent
        Me.lblProgDescription_16.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblProgDescription_16.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProgDescription_16.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblProgDescription_16.Location = New System.Drawing.Point(73, 162)
        Me.lblProgDescription_16.Name = "lblProgDescription_16"
        Me.lblProgDescription_16.Size = New System.Drawing.Size(206, 17)
        Me.lblProgDescription_16.TabIndex = 16
        Me.lblProgDescription_16.Text = "ATS Interactive Program Studio"
        '
        'lblProgDescription_3
        '
        Me.lblProgDescription_3.AutoSize = True
        Me.lblProgDescription_3.BackColor = System.Drawing.Color.Transparent
        Me.lblProgDescription_3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblProgDescription_3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProgDescription_3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblProgDescription_3.Location = New System.Drawing.Point(73, 117)
        Me.lblProgDescription_3.Name = "lblProgDescription_3"
        Me.lblProgDescription_3.Size = New System.Drawing.Size(150, 17)
        Me.lblProgDescription_3.TabIndex = 6
        Me.lblProgDescription_3.Text = "ATS Technical Manual"
        '
        'lblProgDescription_2
        '
        Me.lblProgDescription_2.AutoSize = True
        Me.lblProgDescription_2.BackColor = System.Drawing.Color.Transparent
        Me.lblProgDescription_2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblProgDescription_2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProgDescription_2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblProgDescription_2.Location = New System.Drawing.Point(73, 75)
        Me.lblProgDescription_2.Name = "lblProgDescription_2"
        Me.lblProgDescription_2.Size = New System.Drawing.Size(316, 17)
        Me.lblProgDescription_2.TabIndex = 8
        Me.lblProgDescription_2.Text = "Professional Atlas Workstation Run-Time System"
        '
        'lblProgDescription_1
        '
        Me.lblProgDescription_1.AutoSize = True
        Me.lblProgDescription_1.BackColor = System.Drawing.Color.Transparent
        Me.lblProgDescription_1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblProgDescription_1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProgDescription_1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblProgDescription_1.Location = New System.Drawing.Point(73, 36)
        Me.lblProgDescription_1.Name = "lblProgDescription_1"
        Me.lblProgDescription_1.Size = New System.Drawing.Size(214, 17)
        Me.lblProgDescription_1.TabIndex = 4
        Me.lblProgDescription_1.Text = "Stand Alone Instrument Software"
        '
        'lblProgDescription_9
        '
        Me.lblProgDescription_9.AutoSize = True
        Me.lblProgDescription_9.BackColor = System.Drawing.Color.Transparent
        Me.lblProgDescription_9.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblProgDescription_9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProgDescription_9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblProgDescription_9.Location = New System.Drawing.Point(73, 77)
        Me.lblProgDescription_9.Name = "lblProgDescription_9"
        Me.lblProgDescription_9.Size = New System.Drawing.Size(176, 16)
        Me.lblProgDescription_9.TabIndex = 23
        Me.lblProgDescription_9.Text = "View TPS Technical Manual"
        Me.lblProgDescription_9.Visible = False
        '
        'lblProgDescription_8
        '
        Me.lblProgDescription_8.AutoSize = True
        Me.lblProgDescription_8.BackColor = System.Drawing.Color.Transparent
        Me.lblProgDescription_8.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblProgDescription_8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProgDescription_8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblProgDescription_8.Location = New System.Drawing.Point(73, 36)
        Me.lblProgDescription_8.Name = "lblProgDescription_8"
        Me.lblProgDescription_8.Size = New System.Drawing.Size(117, 16)
        Me.lblProgDescription_8.TabIndex = 21
        Me.lblProgDescription_8.Text = "Run Test Program"
        '
        'lblTpsInformation_5
        '
        Me.lblTpsInformation_5.BackColor = System.Drawing.SystemColors.Control
        Me.lblTpsInformation_5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTpsInformation.SetIndex(Me.lblTpsInformation_5, CType(5, Short))
        Me.lblTpsInformation_5.Location = New System.Drawing.Point(248, 40)
        Me.lblTpsInformation_5.Name = "lblTpsInformation_5"
        Me.lblTpsInformation_5.Size = New System.Drawing.Size(33, 17)
        Me.lblTpsInformation_5.TabIndex = 45
        Me.lblTpsInformation_5.Text = "0"
        '
        'lblTpsInformation_4
        '
        Me.lblTpsInformation_4.BackColor = System.Drawing.SystemColors.Control
        Me.lblTpsInformation_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTpsInformation.SetIndex(Me.lblTpsInformation_4, CType(4, Short))
        Me.lblTpsInformation_4.Location = New System.Drawing.Point(248, 24)
        Me.lblTpsInformation_4.Name = "lblTpsInformation_4"
        Me.lblTpsInformation_4.Size = New System.Drawing.Size(33, 17)
        Me.lblTpsInformation_4.TabIndex = 42
        Me.lblTpsInformation_4.Text = "0"
        '
        'lblTpsInformation_3
        '
        Me.lblTpsInformation_3.BackColor = System.Drawing.SystemColors.Control
        Me.lblTpsInformation_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTpsInformation.SetIndex(Me.lblTpsInformation_3, CType(3, Short))
        Me.lblTpsInformation_3.Location = New System.Drawing.Point(248, 8)
        Me.lblTpsInformation_3.Name = "lblTpsInformation_3"
        Me.lblTpsInformation_3.Size = New System.Drawing.Size(33, 17)
        Me.lblTpsInformation_3.TabIndex = 41
        Me.lblTpsInformation_3.Text = "0"
        '
        'lblTpsInformation_2
        '
        Me.lblTpsInformation_2.BackColor = System.Drawing.SystemColors.Control
        Me.lblTpsInformation_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTpsInformation.SetIndex(Me.lblTpsInformation_2, CType(2, Short))
        Me.lblTpsInformation_2.Location = New System.Drawing.Point(51, 40)
        Me.lblTpsInformation_2.Name = "lblTpsInformation_2"
        Me.lblTpsInformation_2.Size = New System.Drawing.Size(74, 17)
        Me.lblTpsInformation_2.TabIndex = 40
        Me.lblTpsInformation_2.Text = "Not Available"
        '
        'lblTpsInformation_1
        '
        Me.lblTpsInformation_1.BackColor = System.Drawing.SystemColors.Control
        Me.lblTpsInformation_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTpsInformation.SetIndex(Me.lblTpsInformation_1, CType(1, Short))
        Me.lblTpsInformation_1.Location = New System.Drawing.Point(51, 24)
        Me.lblTpsInformation_1.Name = "lblTpsInformation_1"
        Me.lblTpsInformation_1.Size = New System.Drawing.Size(74, 17)
        Me.lblTpsInformation_1.TabIndex = 39
        Me.lblTpsInformation_1.Text = "Not Available"
        '
        'lblTpsInformation_0
        '
        Me.lblTpsInformation_0.AutoSize = True
        Me.lblTpsInformation_0.BackColor = System.Drawing.SystemColors.Control
        Me.lblTpsInformation_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTpsInformation.SetIndex(Me.lblTpsInformation_0, CType(0, Short))
        Me.lblTpsInformation_0.Location = New System.Drawing.Point(51, 8)
        Me.lblTpsInformation_0.Name = "lblTpsInformation_0"
        Me.lblTpsInformation_0.Size = New System.Drawing.Size(70, 13)
        Me.lblTpsInformation_0.TabIndex = 34
        Me.lblTpsInformation_0.Text = "Not Available"
        '
        'lblAttribute_2
        '
        Me.lblAttribute_2.AutoSize = True
        Me.lblAttribute_2.BackColor = System.Drawing.SystemColors.Control
        Me.lblAttribute_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAttribute.SetIndex(Me.lblAttribute_2, CType(2, Short))
        Me.lblAttribute_2.Location = New System.Drawing.Point(2, 40)
        Me.lblAttribute_2.Name = "lblAttribute_2"
        Me.lblAttribute_2.Size = New System.Drawing.Size(30, 13)
        Me.lblAttribute_2.TabIndex = 44
        Me.lblAttribute_2.Text = "P/N:"
        '
        'lblAttribute_1
        '
        Me.lblAttribute_1.AutoSize = True
        Me.lblAttribute_1.BackColor = System.Drawing.SystemColors.Control
        Me.lblAttribute_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAttribute.SetIndex(Me.lblAttribute_1, CType(1, Short))
        Me.lblAttribute_1.Location = New System.Drawing.Point(2, 24)
        Me.lblAttribute_1.Name = "lblAttribute_1"
        Me.lblAttribute_1.Size = New System.Drawing.Size(45, 13)
        Me.lblAttribute_1.TabIndex = 38
        Me.lblAttribute_1.Text = "Version:"
        '
        'lblAttribute_5
        '
        Me.lblAttribute_5.AutoSize = True
        Me.lblAttribute_5.BackColor = System.Drawing.SystemColors.Control
        Me.lblAttribute_5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAttribute.SetIndex(Me.lblAttribute_5, CType(5, Short))
        Me.lblAttribute_5.Location = New System.Drawing.Point(163, 40)
        Me.lblAttribute_5.Name = "lblAttribute_5"
        Me.lblAttribute_5.Size = New System.Drawing.Size(78, 13)
        Me.lblAttribute_5.TabIndex = 37
        Me.lblAttribute_5.Text = "Test Programs:"
        '
        'lblAttribute_4
        '
        Me.lblAttribute_4.AutoSize = True
        Me.lblAttribute_4.BackColor = System.Drawing.SystemColors.Control
        Me.lblAttribute_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAttribute.SetIndex(Me.lblAttribute_4, CType(4, Short))
        Me.lblAttribute_4.Location = New System.Drawing.Point(163, 24)
        Me.lblAttribute_4.Name = "lblAttribute_4"
        Me.lblAttribute_4.Size = New System.Drawing.Size(62, 13)
        Me.lblAttribute_4.TabIndex = 36
        Me.lblAttribute_4.Text = "Assemblies:"
        '
        'lblAttribute_3
        '
        Me.lblAttribute_3.AutoSize = True
        Me.lblAttribute_3.BackColor = System.Drawing.SystemColors.Control
        Me.lblAttribute_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAttribute.SetIndex(Me.lblAttribute_3, CType(3, Short))
        Me.lblAttribute_3.Location = New System.Drawing.Point(163, 8)
        Me.lblAttribute_3.Name = "lblAttribute_3"
        Me.lblAttribute_3.Size = New System.Drawing.Size(49, 13)
        Me.lblAttribute_3.TabIndex = 35
        Me.lblAttribute_3.Text = "Systems:"
        '
        'lblAttribute_0
        '
        Me.lblAttribute_0.AutoSize = True
        Me.lblAttribute_0.BackColor = System.Drawing.SystemColors.Control
        Me.lblAttribute_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAttribute.SetIndex(Me.lblAttribute_0, CType(0, Short))
        Me.lblAttribute_0.Location = New System.Drawing.Point(2, 8)
        Me.lblAttribute_0.Name = "lblAttribute_0"
        Me.lblAttribute_0.Size = New System.Drawing.Size(45, 13)
        Me.lblAttribute_0.TabIndex = 33
        Me.lblAttribute_0.Text = "Volume:"
        '
        'cmdAbout
        '
        Me.cmdAbout.BackColor = System.Drawing.SystemColors.Control
        Me.cmdAbout.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAbout.Location = New System.Drawing.Point(469, 409)
        Me.cmdAbout.Name = "cmdAbout"
        Me.cmdAbout.Size = New System.Drawing.Size(76, 23)
        Me.cmdAbout.TabIndex = 1
        Me.cmdAbout.Text = "&About"
        Me.cmdAbout.UseVisualStyleBackColor = False
        '
        'cmdQuit
        '
        Me.cmdQuit.BackColor = System.Drawing.SystemColors.Control
        Me.cmdQuit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdQuit.Location = New System.Drawing.Point(554, 409)
        Me.cmdQuit.Name = "cmdQuit"
        Me.cmdQuit.Size = New System.Drawing.Size(76, 23)
        Me.cmdQuit.TabIndex = 2
        Me.cmdQuit.Text = "&Quit"
        Me.cmdQuit.UseVisualStyleBackColor = False
        '
        'Picture3
        '
        Me.Picture3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Picture3.BackColor = System.Drawing.SystemColors.Window
        Me.Picture3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Picture3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Picture3.Image = CType(resources.GetObject("Picture3.Image"), System.Drawing.Image)
        Me.Picture3.Location = New System.Drawing.Point(0, 441)
        Me.Picture3.Name = "Picture3"
        Me.Picture3.Size = New System.Drawing.Size(1025, 4)
        Me.Picture3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.Picture3.TabIndex = 28
        Me.Picture3.TabStop = False
        '
        'Picture1
        '
        Me.Picture1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Picture1.BackColor = System.Drawing.SystemColors.Window
        Me.Picture1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Picture1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Picture1.Image = CType(resources.GetObject("Picture1.Image"), System.Drawing.Image)
        Me.Picture1.Location = New System.Drawing.Point(0, 0)
        Me.Picture1.Name = "Picture1"
        Me.Picture1.Size = New System.Drawing.Size(1025, 4)
        Me.Picture1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.Picture1.TabIndex = 26
        Me.Picture1.TabStop = False
        '
        'tabUserOptions
        '
        Me.tabUserOptions.Controls.Add(Me.tabUserOptions_Page1)
        Me.tabUserOptions.Controls.Add(Me.tabUserOptions_Page2)
        Me.tabUserOptions.Controls.Add(Me.tabUserOptions_Page3)
        Me.tabUserOptions.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tabUserOptions.Location = New System.Drawing.Point(8, 65)
        Me.tabUserOptions.Name = "tabUserOptions"
        Me.tabUserOptions.SelectedIndex = 0
        Me.tabUserOptions.Size = New System.Drawing.Size(624, 329)
        Me.tabUserOptions.TabIndex = 0
        '
        'tabUserOptions_Page1
        '
        Me.tabUserOptions_Page1.BackColor = System.Drawing.SystemColors.Control
        Me.tabUserOptions_Page1.Controls.Add(Me.fraFunction_0)
        Me.tabUserOptions_Page1.Location = New System.Drawing.Point(4, 22)
        Me.tabUserOptions_Page1.Name = "tabUserOptions_Page1"
        Me.tabUserOptions_Page1.Size = New System.Drawing.Size(616, 303)
        Me.tabUserOptions_Page1.TabIndex = 0
        Me.tabUserOptions_Page1.Text = "Testing"
        '
        'fraFunction_0
        '
        Me.fraFunction_0.Controls.Add(Me.Button16)
        Me.fraFunction_0.Controls.Add(Me.Button3)
        Me.fraFunction_0.Controls.Add(Me.Button2)
        Me.fraFunction_0.Controls.Add(Me.Button1)
        Me.fraFunction_0.Controls.Add(Me.lblProgDescription_16)
        Me.fraFunction_0.Controls.Add(Me.lblProgDescription_3)
        Me.fraFunction_0.Controls.Add(Me.lblProgDescription_2)
        Me.fraFunction_0.Controls.Add(Me.lblProgDescription_1)
        Me.fraFunction_0.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraFunction_0.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraFunction_0.Location = New System.Drawing.Point(8, 8)
        Me.fraFunction_0.Name = "fraFunction_0"
        Me.fraFunction_0.Size = New System.Drawing.Size(608, 204)
        Me.fraFunction_0.TabIndex = 29
        Me.fraFunction_0.TabStop = False
        Me.fraFunction_0.Text = "Function"
        '
        'Button16
        '
        Me.Button16.Image = Global.SysMenu.My.Resources.Resources.TIP
        Me.Button16.Location = New System.Drawing.Point(21, 147)
        Me.Button16.Name = "Button16"
        Me.Button16.Size = New System.Drawing.Size(36, 36)
        Me.Button16.TabIndex = 20
        Me.Button16.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Image = Global.SysMenu.My.Resources.Resources.LOG2
        Me.Button3.Location = New System.Drawing.Point(21, 105)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(36, 36)
        Me.Button3.TabIndex = 18
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Image = Global.SysMenu.My.Resources.Resources.RTS
        Me.Button2.Location = New System.Drawing.Point(21, 65)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(36, 36)
        Me.Button2.TabIndex = 19
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Image = Global.SysMenu.My.Resources.Resources.SAIS
        Me.Button1.Location = New System.Drawing.Point(21, 24)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(36, 36)
        Me.Button1.TabIndex = 17
        Me.Button1.UseVisualStyleBackColor = True
        '
        'tabUserOptions_Page2
        '
        Me.tabUserOptions_Page2.Controls.Add(Me.GroupBox1)
        Me.tabUserOptions_Page2.Controls.Add(Me.fraFunction_1)
        Me.tabUserOptions_Page2.Location = New System.Drawing.Point(4, 22)
        Me.tabUserOptions_Page2.Name = "tabUserOptions_Page2"
        Me.tabUserOptions_Page2.Size = New System.Drawing.Size(616, 303)
        Me.tabUserOptions_Page2.TabIndex = 1
        Me.tabUserOptions_Page2.Text = "Maintenance"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Button7)
        Me.GroupBox1.Controls.Add(Me.Button15)
        Me.GroupBox1.Controls.Add(Me.lblProgDescription_15)
        Me.GroupBox1.Controls.Add(Me.Button13)
        Me.GroupBox1.Controls.Add(Me.Button11)
        Me.GroupBox1.Controls.Add(Me.Button10)
        Me.GroupBox1.Controls.Add(Me.Button12)
        Me.GroupBox1.Controls.Add(Me.Button14)
        Me.GroupBox1.Controls.Add(Me.Button6)
        Me.GroupBox1.Controls.Add(Me.Button5)
        Me.GroupBox1.Controls.Add(Me.Button4)
        Me.GroupBox1.Controls.Add(Me.lblProgDescription_14)
        Me.GroupBox1.Controls.Add(Me.lblProgDescription_12)
        Me.GroupBox1.Controls.Add(Me.lblProgDescription_13)
        Me.GroupBox1.Controls.Add(Me.lblProgDescription_11)
        Me.GroupBox1.Controls.Add(Me.lblProgDescription_10)
        Me.GroupBox1.Controls.Add(Me.lblProgDescription_4)
        Me.GroupBox1.Controls.Add(Me.lblProgDescription_5)
        Me.GroupBox1.Controls.Add(Me.lblProgDescription_7)
        Me.GroupBox1.Controls.Add(Me.lblProgDescription_6)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.GroupBox1.Location = New System.Drawing.Point(8, 8)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(608, 232)
        Me.GroupBox1.TabIndex = 30
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Function"
        '
        'Button7
        '
        Me.Button7.Image = Global.SysMenu.My.Resources.Resources.syscal
        Me.Button7.Location = New System.Drawing.Point(334, 186)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(36, 36)
        Me.Button7.TabIndex = 65
        Me.Button7.UseVisualStyleBackColor = True
        '
        'Button15
        '
        Me.Button15.Image = Global.SysMenu.My.Resources.Resources.Stow1
        Me.Button15.Location = New System.Drawing.Point(334, 24)
        Me.Button15.Name = "Button15"
        Me.Button15.Size = New System.Drawing.Size(36, 36)
        Me.Button15.TabIndex = 64
        Me.Button15.UseVisualStyleBackColor = True
        '
        'lblProgDescription_15
        '
        Me.lblProgDescription_15.AutoSize = True
        Me.lblProgDescription_15.BackColor = System.Drawing.Color.Transparent
        Me.lblProgDescription_15.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblProgDescription_15.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProgDescription_15.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblProgDescription_15.Location = New System.Drawing.Point(386, 36)
        Me.lblProgDescription_15.Name = "lblProgDescription_15"
        Me.lblProgDescription_15.Size = New System.Drawing.Size(138, 16)
        Me.lblProgDescription_15.TabIndex = 55
        Me.lblProgDescription_15.Text = "Stow VEO2 Collimator"
        Me.lblProgDescription_15.Visible = False
        '
        'Button13
        '
        Me.Button13.Image = Global.SysMenu.My.Resources.Resources.Setup
        Me.Button13.Location = New System.Drawing.Point(334, 146)
        Me.Button13.Name = "Button13"
        Me.Button13.Size = New System.Drawing.Size(36, 36)
        Me.Button13.TabIndex = 61
        Me.Button13.UseVisualStyleBackColor = True
        '
        'Button11
        '
        Me.Button11.Image = Global.SysMenu.My.Resources.Resources.restoredata
        Me.Button11.Location = New System.Drawing.Point(334, 105)
        Me.Button11.Name = "Button11"
        Me.Button11.Size = New System.Drawing.Size(36, 36)
        Me.Button11.TabIndex = 63
        Me.Button11.UseVisualStyleBackColor = True
        '
        'Button10
        '
        Me.Button10.Image = Global.SysMenu.My.Resources.Resources.UDD
        Me.Button10.Location = New System.Drawing.Point(334, 65)
        Me.Button10.Name = "Button10"
        Me.Button10.Size = New System.Drawing.Size(36, 36)
        Me.Button10.TabIndex = 62
        Me.Button10.UseVisualStyleBackColor = True
        '
        'Button12
        '
        Me.Button12.Image = Global.SysMenu.My.Resources.Resources.Inventory
        Me.Button12.Location = New System.Drawing.Point(21, 186)
        Me.Button12.Name = "Button12"
        Me.Button12.Size = New System.Drawing.Size(36, 36)
        Me.Button12.TabIndex = 60
        Me.Button12.UseVisualStyleBackColor = True
        '
        'Button14
        '
        Me.Button14.Image = Global.SysMenu.My.Resources.Resources.FHDB
        Me.Button14.Location = New System.Drawing.Point(21, 146)
        Me.Button14.Name = "Button14"
        Me.Button14.Size = New System.Drawing.Size(36, 36)
        Me.Button14.TabIndex = 59
        Me.Button14.UseVisualStyleBackColor = True
        '
        'Button6
        '
        Me.Button6.Image = Global.SysMenu.My.Resources.Resources.LOG1
        Me.Button6.Location = New System.Drawing.Point(21, 105)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(36, 36)
        Me.Button6.TabIndex = 58
        Me.Button6.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.Image = Global.SysMenu.My.Resources.Resources.STEST
        Me.Button5.Location = New System.Drawing.Point(21, 65)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(36, 36)
        Me.Button5.TabIndex = 57
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Image = Global.SysMenu.My.Resources.Resources.CTEST
        Me.Button4.Location = New System.Drawing.Point(21, 24)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(36, 36)
        Me.Button4.TabIndex = 56
        Me.Button4.UseVisualStyleBackColor = True
        '
        'lblProgDescription_14
        '
        Me.lblProgDescription_14.AutoSize = True
        Me.lblProgDescription_14.BackColor = System.Drawing.Color.Transparent
        Me.lblProgDescription_14.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblProgDescription_14.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProgDescription_14.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblProgDescription_14.Location = New System.Drawing.Point(73, 158)
        Me.lblProgDescription_14.Name = "lblProgDescription_14"
        Me.lblProgDescription_14.Size = New System.Drawing.Size(210, 16)
        Me.lblProgDescription_14.TabIndex = 53
        Me.lblProgDescription_14.Text = "Fault History Database Processor"
        '
        'lblProgDescription_12
        '
        Me.lblProgDescription_12.AutoSize = True
        Me.lblProgDescription_12.BackColor = System.Drawing.Color.Transparent
        Me.lblProgDescription_12.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblProgDescription_12.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProgDescription_12.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblProgDescription_12.Location = New System.Drawing.Point(73, 198)
        Me.lblProgDescription_12.Name = "lblProgDescription_12"
        Me.lblProgDescription_12.Size = New System.Drawing.Size(259, 16)
        Me.lblProgDescription_12.TabIndex = 51
        Me.lblProgDescription_12.Text = "Hardware, Cable and Accessory Inventory"
        '
        'lblProgDescription_13
        '
        Me.lblProgDescription_13.AutoSize = True
        Me.lblProgDescription_13.BackColor = System.Drawing.Color.Transparent
        Me.lblProgDescription_13.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblProgDescription_13.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProgDescription_13.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblProgDescription_13.Location = New System.Drawing.Point(386, 158)
        Me.lblProgDescription_13.Name = "lblProgDescription_13"
        Me.lblProgDescription_13.Size = New System.Drawing.Size(109, 16)
        Me.lblProgDescription_13.TabIndex = 50
        Me.lblProgDescription_13.Text = "Setup Procedure"
        '
        'lblProgDescription_11
        '
        Me.lblProgDescription_11.AutoSize = True
        Me.lblProgDescription_11.BackColor = System.Drawing.Color.Transparent
        Me.lblProgDescription_11.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblProgDescription_11.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProgDescription_11.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblProgDescription_11.Location = New System.Drawing.Point(386, 117)
        Me.lblProgDescription_11.Name = "lblProgDescription_11"
        Me.lblProgDescription_11.Size = New System.Drawing.Size(225, 16)
        Me.lblProgDescription_11.TabIndex = 18
        Me.lblProgDescription_11.Text = "Restore Data from Unique Data Disk"
        '
        'lblProgDescription_10
        '
        Me.lblProgDescription_10.AutoSize = True
        Me.lblProgDescription_10.BackColor = System.Drawing.Color.Transparent
        Me.lblProgDescription_10.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblProgDescription_10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProgDescription_10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblProgDescription_10.Location = New System.Drawing.Point(386, 77)
        Me.lblProgDescription_10.Name = "lblProgDescription_10"
        Me.lblProgDescription_10.Size = New System.Drawing.Size(158, 16)
        Me.lblProgDescription_10.TabIndex = 15
        Me.lblProgDescription_10.Text = "Unique Data Disk Wizard"
        '
        'lblProgDescription_4
        '
        Me.lblProgDescription_4.AutoSize = True
        Me.lblProgDescription_4.BackColor = System.Drawing.Color.Transparent
        Me.lblProgDescription_4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblProgDescription_4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProgDescription_4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblProgDescription_4.Location = New System.Drawing.Point(73, 36)
        Me.lblProgDescription_4.Name = "lblProgDescription_4"
        Me.lblProgDescription_4.Size = New System.Drawing.Size(98, 16)
        Me.lblProgDescription_4.TabIndex = 10
        Me.lblProgDescription_4.Text = "System Survey"
        '
        'lblProgDescription_5
        '
        Me.lblProgDescription_5.AutoSize = True
        Me.lblProgDescription_5.BackColor = System.Drawing.Color.Transparent
        Me.lblProgDescription_5.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblProgDescription_5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProgDescription_5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblProgDescription_5.Location = New System.Drawing.Point(73, 77)
        Me.lblProgDescription_5.Name = "lblProgDescription_5"
        Me.lblProgDescription_5.Size = New System.Drawing.Size(109, 16)
        Me.lblProgDescription_5.TabIndex = 12
        Me.lblProgDescription_5.Text = "System Self Test"
        '
        'lblProgDescription_7
        '
        Me.lblProgDescription_7.AutoSize = True
        Me.lblProgDescription_7.BackColor = System.Drawing.Color.Transparent
        Me.lblProgDescription_7.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblProgDescription_7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProgDescription_7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblProgDescription_7.Location = New System.Drawing.Point(386, 198)
        Me.lblProgDescription_7.Name = "lblProgDescription_7"
        Me.lblProgDescription_7.Size = New System.Drawing.Size(120, 16)
        Me.lblProgDescription_7.TabIndex = 19
        Me.lblProgDescription_7.Text = "System Calibration"
        '
        'lblProgDescription_6
        '
        Me.lblProgDescription_6.AutoSize = True
        Me.lblProgDescription_6.BackColor = System.Drawing.Color.Transparent
        Me.lblProgDescription_6.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblProgDescription_6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProgDescription_6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblProgDescription_6.Location = New System.Drawing.Point(73, 117)
        Me.lblProgDescription_6.Name = "lblProgDescription_6"
        Me.lblProgDescription_6.Size = New System.Drawing.Size(79, 16)
        Me.lblProgDescription_6.TabIndex = 13
        Me.lblProgDescription_6.Text = "System Log"
        '
        'fraFunction_1
        '
        Me.fraFunction_1.Controls.Add(Me.GroupBox2)
        Me.fraFunction_1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraFunction_1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraFunction_1.Location = New System.Drawing.Point(8, 8)
        Me.fraFunction_1.Name = "fraFunction_1"
        Me.fraFunction_1.Size = New System.Drawing.Size(608, 232)
        Me.fraFunction_1.TabIndex = 30
        Me.fraFunction_1.TabStop = False
        Me.fraFunction_1.Text = "Function"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Button25)
        Me.GroupBox2.Controls.Add(Me.Button26)
        Me.GroupBox2.Controls.Add(Me.Button27)
        Me.GroupBox2.Controls.Add(Me.Button28)
        Me.GroupBox2.Controls.Add(Me.Button29)
        Me.GroupBox2.Controls.Add(Me.Button30)
        Me.GroupBox2.Controls.Add(Me.Button31)
        Me.GroupBox2.Controls.Add(Me.Button32)
        Me.GroupBox2.Controls.Add(Me.Button33)
        Me.GroupBox2.Controls.Add(Me.Button34)
        Me.GroupBox2.Controls.Add(Me.Label11)
        Me.GroupBox2.Controls.Add(Me.Label12)
        Me.GroupBox2.Controls.Add(Me.Label13)
        Me.GroupBox2.Controls.Add(Me.Label14)
        Me.GroupBox2.Controls.Add(Me.Label15)
        Me.GroupBox2.Controls.Add(Me.Label16)
        Me.GroupBox2.Controls.Add(Me.Label17)
        Me.GroupBox2.Controls.Add(Me.Label18)
        Me.GroupBox2.Controls.Add(Me.Label19)
        Me.GroupBox2.Controls.Add(Me.Label20)
        Me.GroupBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.GroupBox2.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(608, 232)
        Me.GroupBox2.TabIndex = 30
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Function"
        '
        'Button25
        '
        Me.Button25.Image = Global.SysMenu.My.Resources.Resources.syscal
        Me.Button25.Location = New System.Drawing.Point(308, 186)
        Me.Button25.Name = "Button25"
        Me.Button25.Size = New System.Drawing.Size(36, 36)
        Me.Button25.TabIndex = 65
        Me.Button25.UseVisualStyleBackColor = True
        '
        'Button26
        '
        Me.Button26.Image = Global.SysMenu.My.Resources.Resources.PLC
        Me.Button26.Location = New System.Drawing.Point(308, 146)
        Me.Button26.Name = "Button26"
        Me.Button26.Size = New System.Drawing.Size(36, 36)
        Me.Button26.TabIndex = 64
        Me.Button26.UseVisualStyleBackColor = True
        '
        'Button27
        '
        Me.Button27.Image = Global.SysMenu.My.Resources.Resources.restoredata
        Me.Button27.Location = New System.Drawing.Point(308, 105)
        Me.Button27.Name = "Button27"
        Me.Button27.Size = New System.Drawing.Size(36, 36)
        Me.Button27.TabIndex = 63
        Me.Button27.UseVisualStyleBackColor = True
        '
        'Button28
        '
        Me.Button28.Image = Global.SysMenu.My.Resources.Resources.UDD
        Me.Button28.Location = New System.Drawing.Point(308, 65)
        Me.Button28.Name = "Button28"
        Me.Button28.Size = New System.Drawing.Size(36, 36)
        Me.Button28.TabIndex = 62
        Me.Button28.UseVisualStyleBackColor = True
        '
        'Button29
        '
        Me.Button29.Image = Global.SysMenu.My.Resources.Resources.Setup
        Me.Button29.Location = New System.Drawing.Point(308, 24)
        Me.Button29.Name = "Button29"
        Me.Button29.Size = New System.Drawing.Size(36, 36)
        Me.Button29.TabIndex = 61
        Me.Button29.UseVisualStyleBackColor = True
        '
        'Button30
        '
        Me.Button30.Image = Global.SysMenu.My.Resources.Resources.Inventory
        Me.Button30.Location = New System.Drawing.Point(21, 186)
        Me.Button30.Name = "Button30"
        Me.Button30.Size = New System.Drawing.Size(36, 36)
        Me.Button30.TabIndex = 60
        Me.Button30.UseVisualStyleBackColor = True
        '
        'Button31
        '
        Me.Button31.Image = Global.SysMenu.My.Resources.Resources.FHDB
        Me.Button31.Location = New System.Drawing.Point(21, 146)
        Me.Button31.Name = "Button31"
        Me.Button31.Size = New System.Drawing.Size(36, 36)
        Me.Button31.TabIndex = 59
        Me.Button31.UseVisualStyleBackColor = True
        '
        'Button32
        '
        Me.Button32.Image = Global.SysMenu.My.Resources.Resources.LOG1
        Me.Button32.Location = New System.Drawing.Point(21, 105)
        Me.Button32.Name = "Button32"
        Me.Button32.Size = New System.Drawing.Size(36, 36)
        Me.Button32.TabIndex = 58
        Me.Button32.UseVisualStyleBackColor = True
        '
        'Button33
        '
        Me.Button33.Image = Global.SysMenu.My.Resources.Resources.STEST
        Me.Button33.Location = New System.Drawing.Point(21, 65)
        Me.Button33.Name = "Button33"
        Me.Button33.Size = New System.Drawing.Size(36, 36)
        Me.Button33.TabIndex = 57
        Me.Button33.UseVisualStyleBackColor = True
        '
        'Button34
        '
        Me.Button34.Image = Global.SysMenu.My.Resources.Resources.CTEST
        Me.Button34.Location = New System.Drawing.Point(21, 24)
        Me.Button34.Name = "Button34"
        Me.Button34.Size = New System.Drawing.Size(36, 36)
        Me.Button34.TabIndex = 56
        Me.Button34.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label11.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label11.Location = New System.Drawing.Point(360, 158)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(124, 13)
        Me.Label11.TabIndex = 55
        Me.Label11.Text = "Path Loss Compensation"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label12.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label12.Location = New System.Drawing.Point(73, 158)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(164, 13)
        Me.Label12.TabIndex = 53
        Me.Label12.Text = "Fault History Database Processor"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label13.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label13.Location = New System.Drawing.Point(73, 198)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(206, 13)
        Me.Label13.TabIndex = 51
        Me.Label13.Text = "Hardware, Cable and Accessory Inventory"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label14.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label14.Location = New System.Drawing.Point(360, 36)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(87, 13)
        Me.Label14.TabIndex = 50
        Me.Label14.Text = "Setup Procedure"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label15.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label15.Location = New System.Drawing.Point(360, 117)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(180, 13)
        Me.Label15.TabIndex = 18
        Me.Label15.Text = "Restore Data from Unique Data Disk"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label16.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label16.Location = New System.Drawing.Point(360, 77)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(127, 13)
        Me.Label16.TabIndex = 15
        Me.Label16.Text = "Unique Data Disk Wizard"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label17.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label17.Location = New System.Drawing.Point(73, 36)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(122, 13)
        Me.Label17.TabIndex = 10
        Me.Label17.Text = "System Confidence Test"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.Color.Transparent
        Me.Label18.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label18.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label18.Location = New System.Drawing.Point(73, 77)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(86, 13)
        Me.Label18.TabIndex = 12
        Me.Label18.Text = "System Self Test"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.Color.Transparent
        Me.Label19.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label19.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label19.Location = New System.Drawing.Point(360, 198)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(93, 13)
        Me.Label19.TabIndex = 19
        Me.Label19.Text = "System Calibration"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.Color.Transparent
        Me.Label20.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label20.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label20.Location = New System.Drawing.Point(73, 117)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(62, 13)
        Me.Label20.TabIndex = 13
        Me.Label20.Text = "System Log"
        '
        'tabUserOptions_Page3
        '
        Me.tabUserOptions_Page3.Controls.Add(Me.fraFunction_2)
        Me.tabUserOptions_Page3.Location = New System.Drawing.Point(4, 22)
        Me.tabUserOptions_Page3.Name = "tabUserOptions_Page3"
        Me.tabUserOptions_Page3.Size = New System.Drawing.Size(616, 303)
        Me.tabUserOptions_Page3.TabIndex = 2
        Me.tabUserOptions_Page3.Text = "Test Program Sets"
        '
        'fraFunction_2
        '
        Me.fraFunction_2.Controls.Add(Me.Label1)
        Me.fraFunction_2.Controls.Add(Me.Button17)
        Me.fraFunction_2.Controls.Add(Me.Button9)
        Me.fraFunction_2.Controls.Add(Me.Button8)
        Me.fraFunction_2.Controls.Add(Me.panCdInformation)
        Me.fraFunction_2.Controls.Add(Me.trvTpsList)
        Me.fraFunction_2.Controls.Add(Me.lblProgDescription_9)
        Me.fraFunction_2.Controls.Add(Me.lblProgDescription_8)
        Me.fraFunction_2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraFunction_2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraFunction_2.Location = New System.Drawing.Point(8, 8)
        Me.fraFunction_2.Name = "fraFunction_2"
        Me.fraFunction_2.Size = New System.Drawing.Size(608, 250)
        Me.fraFunction_2.TabIndex = 31
        Me.fraFunction_2.TabStop = False
        Me.fraFunction_2.Text = "Function"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(73, 117)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(142, 16)
        Me.Label1.TabIndex = 60
        Me.Label1.Text = "Change TPS Directory"
        '
        'Button17
        '
        Me.Button17.Image = Global.SysMenu.My.Resources.Resources.runtp
        Me.Button17.Location = New System.Drawing.Point(21, 107)
        Me.Button17.Name = "Button17"
        Me.Button17.Size = New System.Drawing.Size(36, 36)
        Me.Button17.TabIndex = 59
        Me.Button17.UseVisualStyleBackColor = True
        '
        'Button9
        '
        Me.Button9.Image = Global.SysMenu.My.Resources.Resources.TPSTM
        Me.Button9.Location = New System.Drawing.Point(21, 65)
        Me.Button9.Name = "Button9"
        Me.Button9.Size = New System.Drawing.Size(36, 36)
        Me.Button9.TabIndex = 58
        Me.Button9.UseVisualStyleBackColor = True
        '
        'Button8
        '
        Me.Button8.Image = Global.SysMenu.My.Resources.Resources.runtp
        Me.Button8.Location = New System.Drawing.Point(21, 24)
        Me.Button8.Name = "Button8"
        Me.Button8.Size = New System.Drawing.Size(36, 36)
        Me.Button8.TabIndex = 57
        Me.Button8.UseVisualStyleBackColor = True
        '
        'panCdInformation
        '
        Me.panCdInformation.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panCdInformation.Controls.Add(Me.lblTpsInformation_5)
        Me.panCdInformation.Controls.Add(Me.lblAttribute_2)
        Me.panCdInformation.Controls.Add(Me.lblTpsInformation_4)
        Me.panCdInformation.Controls.Add(Me.lblTpsInformation_3)
        Me.panCdInformation.Controls.Add(Me.lblInstructions)
        Me.panCdInformation.Controls.Add(Me.lblTpsInformation_2)
        Me.panCdInformation.Controls.Add(Me.lblTpsInformation_1)
        Me.panCdInformation.Controls.Add(Me.lblAttribute_1)
        Me.panCdInformation.Controls.Add(Me.lblAttribute_5)
        Me.panCdInformation.Controls.Add(Me.lblAttribute_4)
        Me.panCdInformation.Controls.Add(Me.lblAttribute_3)
        Me.panCdInformation.Controls.Add(Me.lblTpsInformation_0)
        Me.panCdInformation.Controls.Add(Me.lblAttribute_0)
        Me.panCdInformation.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panCdInformation.Location = New System.Drawing.Point(6, 173)
        Me.panCdInformation.Name = "panCdInformation"
        Me.panCdInformation.Size = New System.Drawing.Size(290, 62)
        Me.panCdInformation.TabIndex = 32
        Me.panCdInformation.Visible = False
        '
        'lblInstructions
        '
        Me.lblInstructions.AutoSize = True
        Me.lblInstructions.BackColor = System.Drawing.Color.Transparent
        Me.lblInstructions.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblInstructions.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblInstructions.Location = New System.Drawing.Point(3, 21)
        Me.lblInstructions.Name = "lblInstructions"
        Me.lblInstructions.Size = New System.Drawing.Size(378, 13)
        Me.lblInstructions.TabIndex = 43
        Me.lblInstructions.Text = "Insert an Application Program Set (APS) Compact Disc into the CD-ROM Drive."
        '
        'trvTpsList
        '
        Me.trvTpsList.BackColor = System.Drawing.SystemColors.Window
        Me.trvTpsList.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvTpsList.ForeColor = System.Drawing.SystemColors.WindowText
        Me.trvTpsList.HideSelection = False
        Me.trvTpsList.ImageKey = "CLOSED"
        Me.trvTpsList.ImageList = Me.ImageList1
        Me.trvTpsList.Location = New System.Drawing.Point(307, 24)
        Me.trvTpsList.Name = "trvTpsList"
        Me.trvTpsList.SelectedImageIndex = 0
        Me.trvTpsList.Size = New System.Drawing.Size(288, 220)
        Me.trvTpsList.Sorted = True
        Me.trvTpsList.TabIndex = 24
        Me.trvTpsList.Visible = False
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ImageList1.Images.SetKeyName(0, "TPS")
        Me.ImageList1.Images.SetKeyName(1, "LEAF")
        Me.ImageList1.Images.SetKeyName(2, "MINUS")
        Me.ImageList1.Images.SetKeyName(3, "OPEN")
        Me.ImageList1.Images.SetKeyName(4, "PLUS")
        Me.ImageList1.Images.SetKeyName(5, "CLOSED")
        '
        'tmrUpdate
        '
        Me.tmrUpdate.Interval = 2000
        '
        'StatusBar1
        '
        Me.StatusBar1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.StatusBar1.Location = New System.Drawing.Point(0, 443)
        Me.StatusBar1.Name = "StatusBar1"
        Me.StatusBar1.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.StatusBar1_Panel1})
        Me.StatusBar1.ShowPanels = True
        Me.StatusBar1.Size = New System.Drawing.Size(637, 19)
        Me.StatusBar1.SizingGrip = False
        Me.StatusBar1.TabIndex = 25
        '
        'StatusBar1_Panel1
        '
        Me.StatusBar1_Panel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
        Me.StatusBar1_Panel1.Name = "StatusBar1_Panel1"
        Me.StatusBar1_Panel1.Text = "User Information"
        Me.StatusBar1_Panel1.Width = 637
        '
        'lblUser
        '
        Me.lblUser.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblUser.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblUser.Font = New System.Drawing.Font("Courier New", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUser.ForeColor = System.Drawing.Color.White
        Me.lblUser.Location = New System.Drawing.Point(8, 14)
        Me.lblUser.Name = "lblUser"
        Me.lblUser.Size = New System.Drawing.Size(622, 37)
        Me.lblUser.TabIndex = 27
        Me.lblUser.Text = "ATS Menu "
        '
        'frmSysPanl
        '
        Me.BackColor = System.Drawing.Color.DarkGray
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(637, 462)
        Me.Controls.Add(Me.cmdAbout)
        Me.Controls.Add(Me.cmdQuit)
        Me.Controls.Add(Me.Picture3)
        Me.Controls.Add(Me.Picture1)
        Me.Controls.Add(Me.tabUserOptions)
        Me.Controls.Add(Me.StatusBar1)
        Me.Controls.Add(Me.lblUser)
        Me.ForeColor = System.Drawing.SystemColors.WindowText
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmSysPanl"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Automated Test System"
        CType(Me.lblTpsInformation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAttribute, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Picture3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Picture1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabUserOptions.ResumeLayout(False)
        Me.tabUserOptions_Page1.ResumeLayout(False)
        Me.fraFunction_0.ResumeLayout(False)
        Me.fraFunction_0.PerformLayout()
        Me.tabUserOptions_Page2.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.fraFunction_1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.tabUserOptions_Page3.ResumeLayout(False)
        Me.fraFunction_2.ResumeLayout(False)
        Me.fraFunction_2.PerformLayout()
        Me.panCdInformation.ResumeLayout(False)
        Me.panCdInformation.PerformLayout()
        CType(Me.StatusBar1_Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    '=========================================================
    '******************************************************************
    'PCR VSYS-015 MSC PLC now only available through Calibration APS
    '******************************************************************



    Private Sub cmdAbout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAbout.Click
        Dim SWRVersion As New StringBuilder(255)
        GetPrivateProfileString("System Startup", "SWR", "N/A", SWRVersion, 255, iniFilePath)
        frmAbout.lblInstrument_1.Text = SWRVersion.ToString
        frmAbout.ShowDialog()
    End Sub
    Private Sub cmdQuit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdQuit.Click
        If (ciclHasBeenInitialized = True) Then
            If bDebug Then WriteDebugInfo("frmSysPanl.cmdQuit_Click() - closing CICL session")
            atxml_Close()
        End If
        Close()
    End Sub
    '#Const defUse_cmdRefresh_Click = True
#If defUse_cmdRefresh_Click Then
    Private Sub cmdRefresh_Click()

        CheckForTpsCd()

    End Sub
#End If

    Private Sub frmSysPanl_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseMove
        Dim Button As Short = e.Button \ &H100000
        Dim Shift As Short = Control.ModifierKeys \ &H10000
        Dim x As Single = VB6.PixelsToTwipsX(e.X)
        Dim y As Single = VB6.PixelsToTwipsY(e.Y)


        SendStatusBarMessage("Automated Test System (ATS) Main Menu.")

    End Sub


    Private Sub frmSysPanl_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize

        If Me.WindowState = FormWindowState.Minimized Then
            Application.DoEvents()
            Me.tmrUpdate.Enabled = False
            Application.DoEvents()
        Else
            If Me.tabUserOptions.SelectedIndex = 2 Then
                Application.DoEvents()
                Me.tmrUpdate.Enabled = True
                Application.DoEvents()
            End If
        End If

    End Sub

    Private Sub Form_Unload(ByRef Cancel As Short)
        If bDebug Then WriteDebugInfo("frmSysPanl.Form_Unload() - Form is unloading")
        Application.Exit()
    End Sub

    Private Sub frmSysPanl_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If bDebug Then WriteDebugInfo("frmSysPanl.frmSysPanl_Closing() - Form is closing")
        Dim Cancel As Short = 0
        Form_Unload(Cancel)
        If Cancel <> 0 Then e.Cancel = True
    End Sub


    Public Sub ribProgram_Click(ByVal Index As Integer, ByVal Value As Short)

        Dim Blink As Short
        Dim RetVal As Integer
        Dim ColorRed As Integer
        Dim ColorYellow As Integer

        Dim UserResponse As DialogResult
        Dim ErrorMessage As String
        Dim IatmLaunch As String
        Dim IadsFileLIst() As String
        Dim NumDirs As Short
        Dim CalPathWithDetectedCd As String

        Dim MesBoxRes As DialogResult
        Dim Temp As String



        'DME CHANGE TO POINT THE SETUP PROCEDURE TO THE RIGHT PATH. PCR VSYS-114
        Dim htm_path As String
        'htm_path = GatherIniFileInformation("File Locations", "Setup_Proc_path", "")

        'Define Colors
        ColorRed = RGB(255, 0, 0)
        ColorYellow = RGB(255, 255, 0)

        If Value Then
            'EXCEPTION: Check for calibration CD
            If Index = SYSTEM_CALIBRATION Then 'Check for CD
                Do
                    If (System.IO.File.Exists(FileNameData(SYSTEM_CALIBRATION))) Then 'TEST INI File Name First
                        Exit Do
                    Else
                        '---Added as a result of FAT for Version 1.7 DWH---
                        'The change from the LRIP Instrument Controller to the FAT Instrument Controller has caused a situation
                        'in which the CD-ROM drive is not assigned to [d:\], but to [e:\].  Therefore this change was made
                        'to First check for the drive, path, and filename in the VIPERT.INI file key configured by the setup program.
                        'Since the CD-ROM drive letter assignment can be changed in the Windows NT control panel, this software
                        'will perform a Second check with the First detected CD-ROM Drive and the path, and filename from the
                        'VIPERT.INI file.
                        CdDrivesOnSystem = GetCompactDiscDrives() 'Check for the first default system CD drive
                        CdDrivesOnSystem = Mid(CdDrivesOnSystem, 1, 3) 'Strip Null
                        CalPathWithDetectedCd = FileNameData(SYSTEM_CALIBRATION) 'Strip INI File Drive Letter
                        CalPathWithDetectedCd = Mid(CalPathWithDetectedCd, 4, Len(CalPathWithDetectedCd))
                        CalPathWithDetectedCd = CdDrivesOnSystem & CalPathWithDetectedCd 'Append Detected Drive Letter to VIPERT.INI File Description
                        'Test Again to see if the file exists
                        If (System.IO.File.Exists(CalPathWithDetectedCd)) Then
                            FileNameData(SYSTEM_CALIBRATION) = CalPathWithDetectedCd
                            Exit Do
                        Else
                            'File not found in BOTH cases, inform user
                            ErrorMessage = "File Not Found in path " & FileNameData(SYSTEM_CALIBRATION) + vbCrLf
                            ErrorMessage &= "Verify that VIPERT Calibration CD has been inserted into " + vbCrLf
                            ErrorMessage &= "the compact-disc drive and the device is working properly." + vbCrLf
                            UserResponse = MsgBox(ErrorMessage, MsgBoxStyle.Information + MsgBoxStyle.RetryCancel)
                        End If
                        '---End of modifications for Version 1.7 DWH---
                    End If
                Loop Until UserResponse = DialogResult.Cancel 'Continue prompting until user presses [Cancel] button
                If UserResponse = DialogResult.Cancel Then
                    'ribProgram(Index).Value = False
                    Exit Sub
                End If
            End If

            'EXCEPTION: Check for TPS CD
            If Index = TPS_MENU Then 'Check for CD

                If SelectedTpsProgram = "" Then
                    'ribProgram(Index).Value = False
                    If Me.trvTpsList.Visible = True Then
                        ErrorMessage = "Select a TPS by expanding the branches of the directory tree and clicking on a Test Program file." + vbCrLf
                        UserResponse = MsgBox(ErrorMessage, MsgBoxStyle.Information)
                        Exit Sub
                    Else
                        ErrorMessage = "Verify that an APS CD has been inserted into " + vbCrLf
                        ErrorMessage &= "the compact-disc drive and the device is working properly." + vbCrLf
                        UserResponse = MsgBox(ErrorMessage, MsgBoxStyle.Information)
                        Exit Sub
                    End If
                End If
                Do
                    If (System.IO.File.Exists(CdDrivesOnSystem & SelectedTpsProgram)) Then
                        Exit Do
                    Else
                        ErrorMessage = "File Not Found " & CdDrivesOnSystem & SelectedTpsProgram + vbCrLf
                        ErrorMessage &= "Verify that an APS CD has been inserted into " + vbCrLf
                        ErrorMessage &= "the compact-disc drive and the device is working properly." + vbCrLf
                        UserResponse = MsgBox(ErrorMessage, MsgBoxStyle.Information + MsgBoxStyle.RetryCancel)
                    End If
                Loop Until UserResponse = DialogResult.Cancel
                If UserResponse = DialogResult.Cancel Then
                    'ribProgram(Index).Value = False
                    Exit Sub
                End If
                FileNameData(Index) = CdDrivesOnSystem & SelectedTpsProgram
            End If

            'EXCEPTION: Check for TPS Technical Manual
            If Index = TPS_TM Then 'Check for CD

                Dim node As TreeNode = Me.trvTpsList.SelectedNode
                While (node.Parent IsNot Nothing)
                    node = node.Parent
                End While
                SelectedSystem = node.Text

                If SelectedSystem = "" Then
                    'ribProgram(Index).Value = False
                    If Me.trvTpsList.Visible = True Then
                        ErrorMessage = "Select a System by clicking on the branches of the directory tree." + vbCrLf
                        UserResponse = MsgBox(ErrorMessage, MsgBoxStyle.Information)
                        Exit Sub
                    Else
                        ErrorMessage = "Verify that an APS CD has been inserted into " + vbCrLf
                        ErrorMessage &= "the compact-disc drive and the device is working properly." + vbCrLf
                        UserResponse = MsgBox(ErrorMessage, MsgBoxStyle.Information)
                        Exit Sub
                    End If
                End If

                ReDim IadsFileLIst(50)
                NumDirs = StringToList(GetIetmFiles(CdDrivesOnSystem & SelectedSystem & "\IETM\"), IadsFileLIst, ",")
                'Changed the infomation stored in FileNameData$(Index%) to include the SelectedSystem$.  ECP0006 - GJohnson 13OCT99
                If IadsFileLIst(1) = "" Then
                    ErrorMessage = "IETM File Not Found " + vbCrLf
                    ErrorMessage &= "Verify that an APS CD has been inserted into " + vbCrLf
                    ErrorMessage &= "the compact-disc drive and the device is working properly." + vbCrLf
                    UserResponse = MsgBox(ErrorMessage, MsgBoxStyle.Information + MsgBoxStyle.RetryCancel)
                    Exit Sub
                End If

                Do
                    FileNameData(Index) = CdDrivesOnSystem & SelectedSystem & "\IETM\" & SelectedSystem & ".ide"

                    If (System.IO.File.Exists(FileNameData(Index))) Then
                        Exit Do
                    Else
                        FileNameData$(Index%) = CdDrivesOnSystem$ & SelectedSystem$ & "\IETM\" & SelectedSystem$ & ".sgm"
                        If (System.IO.File.Exists(FileNameData(Index))) Then
                            Exit Do
                        Else

                            FileNameData$(Index%) = CdDrivesOnSystem$ & SelectedSystem$ & "\IETM\" & SelectedSystem$ & ".xml"

                            If (System.IO.File.Exists(FileNameData(Index))) Then
                                Exit Do
                            Else
                                ErrorMessage = "IETM File Not Found " + vbCrLf
                                ErrorMessage &= "Verify that an APS CD has been inserted into " + vbCrLf
                                ErrorMessage &= "the compact-disc drive and the device is working properly." + vbCrLf
                                UserResponse = MsgBox(ErrorMessage, MsgBoxStyle.Information + MsgBoxStyle.RetryCancel)
                            End If
                        End If
                    End If
                Loop Until UserResponse = DialogResult.Cancel
                If UserResponse = DialogResult.Cancel Then
                    'ribProgram(Index).Value = False
                    Exit Sub
                End If

            End If

            lblProgDescription(Index).ForeColor = Color.Black
            Me.Refresh()

            If Index = UPDATE_UDD Or Index = RESTORE_UDD Then

                'reset usbflags in case second run through
                usb_flag = False
                uddHDflag = False

                USB_Detect()
                If usb_flag = False Then
                    If Index = UPDATE_UDD Then MesBoxRes = MsgBox("Would you like to store your UDD on the system hard drive?", MsgBoxStyle.YesNo)
                    If Index = RESTORE_UDD Then MesBoxRes = MsgBox("Would you like to restore your UDD from the system hard drive?", MsgBoxStyle.YesNo)
                    If MesBoxRes = DialogResult.Yes Then
                        Temp = UDD_HD_Path()
                        If Temp = "" Then Exit Sub
                        'make sure the path for the UDD is not in any system path
                        If InStr(1, UCase(Temp), "C:\WINDOWS", CompareMethod.Binary) <> 0 Or InStr(1, UCase(Temp), "C:\PROGRAM", CompareMethod.Binary) <> 0 Or InStr(1, UCase(Temp), "C:\IRWIN2001", CompareMethod.Binary) <> 0 Or InStr(1, UCase(Temp), "C:\USR", CompareMethod.Binary) <> 0 Or InStr(1, UCase(Temp), "C:\APS", CompareMethod.Binary) <> 0 Or InStr(1, UCase(Temp), "C:\VXIPNP", CompareMethod.Binary) <> 0 Or InStr(1, UCase(Temp), "C:\INTEL", CompareMethod.Binary) <> 0 Or InStr(1, UCase(Temp), "C:\IFC", CompareMethod.Binary) <> 0 Or InStr(1, UCase(Temp), "C:\IADS", CompareMethod.Binary) <> 0 Or InStr(1, UCase(Temp), "C:\EMACE", CompareMethod.Binary) <> 0 Or InStr(1, UCase(Temp), "C:\CONFIG", CompareMethod.Binary) <> 0 Then

                            MsgBox("""" & Temp & """" & " is not a valid directory for the UDD files.", MsgBoxStyle.OkOnly)
                            usb_flag = False
                            uddHDflag = False
                            Exit Sub
                        Else
                            sFDD = Temp & "\"
                            'sFDD = Temp & "\" & "uddInfo_" & GetSerialNumber(True) & "\"

                            sFileSelfX = sFDD & "FHBD_Database.zip" 'Self Extracting Zip File Name
                            sFDD_DB = sFDD & "FHDB.mdb" 'File name and location on USB Disk

                            uddHDflag = True
                        End If
                    Else
                        GoTo setcal
                    End If
                End If
            End If

            If Index = UPDATE_UDD And (usb_flag = True Or uddHDflag = True) Then
                UpdateFlag = True
                CenterForm(frmUdd)
                Me.WindowState = FormWindowState.Minimized
                frmUdd.Show()
                frmUdd.BringToFront()
                frmUdd.Activate()
                Exit Sub
            End If

            If Index = RESTORE_UDD And (usb_flag = True Or uddHDflag = True) Then
                UpdateFlag = False
                CenterForm(frmUdd)
                Me.WindowState = FormWindowState.Minimized
                frmUdd.Visible = True 'Load "Application Modal"
                frmUdd.Focus()
                Exit Sub
            End If
            '---End of Modification Version 1.7 DWH---

            Select Case Index
                Case TIPS
                    If (System.IO.File.Exists(FileNameData(TIPS))) Then
                        RetVal = Shell(Q & FileNameData(TIPS) & Q & " " & Q & FileNameData(Index) & Q, AppWinStyle.NormalFocus)
                    Else
                        MsgBox(" File " & FileNameData(TIPS) & vbCrLf & " can not be found.")
                    End If
                Case TPS_MENU
                    If bTPSExecPath(FileNameData(Index)) Then
                        RetVal = Shell(Q & FileNameData(Index) & Q, AppWinStyle.NormalFocus)
                    Else
                        RetVal = Shell(Q & FileNameData(PAWS_RTS) & Q & " " & Q & FileNameData(Index) & Q, AppWinStyle.NormalFocus)
                    End If
                Case TPS_TM
                    If (System.IO.File.Exists(CdDrivesOnSystem & "iads_3_2_7.exe")) Then 'TPS CD Inserted
                        RetVal = Shell(Q & "C:\IADS\PROGRAMS\READR" & Q$ & " -userid32767 " & FileNameData$(Index%), vbNormalFocus)
                    Else
                        RetVal = Shell(Q & FileNameData(IADS_READER) & Q & " " & FileNameData(Index), AppWinStyle.NormalFocus)
                        'MsgBox("The APS is improperly set up.", vbOKOnly)
                    End If
                Case IADS_READER
                    If (System.IO.File.Exists(CdDrivesOnSystem & "iads_3_2_7.exe") And ((System.IO.File.Exists(CdDrivesOnSystem & "VIPERT\IETM\VIPERT.ide") Or (System.IO.File.Exists(CdDrivesOnSystem & "TETS\IETM\TETS.ide"))))) Then 'TPS CD Inserted
                        RetVal = Shell(Q & "C:\IADS\PROGRAMS\READR" & Q & " F:\VIPERT\IETM\VIPERT.ide", AppWinStyle.NormalFocus)
                    ElseIf (System.IO.File.Exists(CdDrivesOnSystem & "VIPERT\IETM\VIPERT.ide") Or System.IO.File.Exists("F:\VIPERT\IETM\VIPERT.ide")) Then 'TPS CD Inserted
                        RetVal = Shell(Q & FileNameData(IADS_READER) & Q & " F:\VIPERT\IETM\VIPERT.ide", AppWinStyle.NormalFocus)
                    ElseIf (System.IO.File.Exists(CdDrivesOnSystem & "TETS\IETM\TETS.ide") Or System.IO.File.Exists("F:\TETS\IETM\TETS.ide")) Then 'TPS CD Inserted
                        RetVal = Shell(Q & FileNameData(IADS_READER) & Q & " F:\TETS\IETM\TETS.ide", AppWinStyle.NormalFocus)
                    Else
                        MsgBox("Please Insert a ATS IETM disk and try again.", MsgBoxStyle.OkOnly)
                    End If
                    'JHill V1.9 DR #109, ECP-026 begin -----
                Case INVENTORY_LIST
                    Err.Clear()
                    On Error Resume Next
                    RetVal = Shell("C:\Program Files\Plus!\Microsoft Internet\IEXPLORE.EXE" & " " & Application.StartupPath & "\InventoryLst.htm", AppWinStyle.NormalFocus)
                    If Err.Number Then
                        RetVal = Shell("EXPLORER.EXE" & " " & Application.StartupPath & "\InventoryLst.htm", AppWinStyle.NormalFocus)
                    End If
                Case SETUP_PROC
                    Err.Clear()
                    On Error Resume Next
                    RetVal = Shell("C:\Program Files\Plus!\Microsoft Internet\IEXPLORE.EXE" & " " & htm_path & "\VIPERT_SETUP_D.htm", AppWinStyle.NormalFocus)
                    If Err.Number Then
                        RetVal = Shell("EXPLORER.EXE" & " " & htm_path & "\VIPERT_SETUP_D.htm", AppWinStyle.NormalFocus)
                    End If
                    'JHill V1.9 DR #109, ECP-026 end -----

                    'VIPERT-ECP-23, FHDB        DJoiner  04/30/2001
                Case FHDB_PROCESSOR
                    RetVal = Shell(Q & FileNameData(Index) & Q, AppWinStyle.NormalFocus)
                    '******************
                Case STOW
                    If bDebug Then WriteDebugInfo("SYSPANL.ribProgram_Click() - calling StowVEO2Collimator()")
                    StowVEO2Collimator()
                    Exit Sub
                Case Else
                    RetVal = Shell(Q & FileNameData(Index) & Q, AppWinStyle.NormalFocus)
            End Select
setcal:
            'STR 15653 remove Cal Date from Sys Menu JW 10/25/18
            'setCaldates()
            Me.WindowState = FormWindowState.Minimized

        End If

    End Sub

    Public Sub ribProgram_MouseMove(ByVal Index As Integer, ByVal Button As Short, ByVal Shift As Short, ByVal x As Single, ByVal y As Single)
        ProgramComment(Index)
    End Sub


    Private tabUserOptions_PreviousTab As Integer
    Private Sub tabUserOptions_Deselecting(ByVal sender As System.Object, ByVal e As TabControlCancelEventArgs) Handles tabUserOptions.Deselecting
        tabUserOptions_PreviousTab = e.TabPageIndex
    End Sub
    Private Sub tabUserOptions_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tabUserOptions.SelectedIndexChanged
        Dim PreviousTab As Integer = tabUserOptions_PreviousTab

        'Display User Information In Status Bar
        Select Case Me.tabUserOptions.SelectedIndex
            Case 0
                SendStatusBarMessage("Click on one of the testing options to evaluate a Unit Under Test (UUT).")
                Me.tmrUpdate.Enabled = False
            Case 1
                SendStatusBarMessage("Click on one of the System Maintenance options to perform sceduled maintenance actions.")
                Me.tmrUpdate.Enabled = False
            Case 2
                SendStatusBarMessage("Select and Execute a Test Program.")
                Application.DoEvents()
                If TPSDrive = "E:\" Then
                    CheckForTpsHd()
                    RefreshTreeView = True
                ElseIf TPSDrive = "F:\" Then
                    CheckForTpsCd()
                    RefreshTreeView = True
                End If
                Me.tmrUpdate.Enabled = True
        End Select

    End Sub

    Private Sub tmrUpdate_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmrUpdate.Tick

        If TPSDrive = "E:\" Then
            CheckForTpsHd()
        ElseIf TPSDrive = "F:\" Then
            CheckForTpsCd()
        End If

    End Sub


    Private Sub trvTpsList_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles trvTpsList.DoubleClick
        Dim tree As New TreeView
        tree = sender
        Dim teststring As String = tree.SelectedNode.Text
        Dim RequiredNodeLevel As Integer

        If (SysPanel.TPSDrive = "E:\") Then
            RequiredNodeLevel = 4
        Else
            RequiredNodeLevel = 3
        End If

        If (tree.SelectedNode.Level = RequiredNodeLevel) Then
            If bDebug Then WriteDebugInfo("SYSPANL.trvTpsList_DoubleClick() - User double clicked on tps executable")

            Dim p As New Process
            Dim psi As New ProcessStartInfo
            Dim PAWSRuntimePath As New StringBuilder(4096)
            GetPrivateProfileString("File Locations", "PAWS_RTS", "", PAWSRuntimePath, 4096, iniFilePath)

            If tree.SelectedNode.ToolTipText.ToUpper.EndsWith(".OBJ") Then
                psi = New ProcessStartInfo(PAWSRuntimePath.ToString, """" & tree.SelectedNode.ToolTipText & """")
            Else
                psi = New ProcessStartInfo("""" & tree.SelectedNode.ToolTipText & """")
            End If

            p.StartInfo = psi

            Try
                p.Start()
            Catch ex As Exception
                If psi.Arguments.Contains("wrts") Then
                    MessageBox.Show("""" & PAWSRuntimePath.ToString & """ Not Found")
                Else
                    MessageBox.Show("""" & tree.SelectedNode.ToolTipText & """ Did not execute/terminate as expected")
                End If
            End Try
        End If
    End Sub


    Private Sub trvTpsList_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trvTpsList.MouseMove
        Dim Button As Short = e.Button \ &H100000
        Dim Shift As Short = Control.ModifierKeys \ &H10000
        Dim x As Single = VB6.PixelsToTwipsX(e.X)
        Dim y As Single = VB6.PixelsToTwipsY(e.Y)


        SendStatusBarMessage("Select one of the test programs from the current APS CD.")

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim saisToolbarExePath As New StringBuilder(255)
        GetPrivateProfileString("File Locations", "SAIS_TOOLBAR", "SAISMGR.EXE", saisToolbarExePath, 255, iniFilePath)
        Try
            Process.Start(saisToolbarExePath.ToString)
            Me.WindowState = FormWindowState.Minimized
        Catch ex As Exception
            MessageBox.Show("Could not find " & saisToolbarExePath.ToString, "Error")
        End Try

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim saisToolbarExePath As New StringBuilder(255)
        GetPrivateProfileString("File Locations", "PAWS_RTS", "WRTS.EXE", saisToolbarExePath, 255, iniFilePath)
        Try
            Process.Start(saisToolbarExePath.ToString)
            Me.WindowState = FormWindowState.Minimized
        Catch ex As Exception
            MessageBox.Show("Could not find " & saisToolbarExePath.ToString, "Error")
        End Try
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim CTestExePath As New StringBuilder(255)
        GetPrivateProfileString("File Locations", "SYSTEM_SURVEY", "SystemSurvey.EXE", CTestExePath, 255, iniFilePath)
        Try
            Process.Start(CTestExePath.ToString)
            Me.WindowState = FormWindowState.Minimized
        Catch ex As Exception
            MessageBox.Show("Could not find " & CTestExePath.ToString, "Error")
        End Try
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim STestExePath As New StringBuilder(255)
        GetPrivateProfileString("File Locations", "SYSTEM_SELF_TEST", "STEST.EXE", STestExePath, 255, iniFilePath)
        Try
            Process.Start(STestExePath.ToString)
            Me.WindowState = FormWindowState.Minimized
        Catch ex As Exception
            MessageBox.Show("Could not find " & STestExePath.ToString, "Error")
        End Try
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim SysLogExePath As New StringBuilder(255)
        GetPrivateProfileString("File Locations", "SYSTEM_LOG", "SYSLOG.EXE", SysLogExePath, 255, iniFilePath)
        Try
            Process.Start(SysLogExePath.ToString)
            Me.WindowState = FormWindowState.Minimized
        Catch ex As Exception
            MessageBox.Show("Could not find " & SysLogExePath.ToString, "Error")
        End Try
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        Dim FHDBProcessorExePath As New StringBuilder(255)
        GetPrivateProfileString("File Locations", "FHDB_PROCESSOR", "FHDB_PROCESSOR.EXE", FHDBProcessorExePath, 255, iniFilePath)
        Try
            Process.Start(FHDBProcessorExePath.ToString)
            Me.WindowState = FormWindowState.Minimized
        Catch ex As Exception
            MessageBox.Show("Could not find " & FHDBProcessorExePath.ToString, "Error")
        End Try
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Dim SyscalExePath As New StringBuilder(255)
        GetPrivateProfileString("File Locations", "SYSTEM_CALIBRATION", "SYSCAL.EXE", SyscalExePath, 255, iniFilePath)
        Try
            Process.Start(SyscalExePath.ToString)
            Me.WindowState = FormWindowState.Minimized
        Catch ex As Exception
            MessageBox.Show("Could not find " & SyscalExePath.ToString, "Error")
        End Try
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Me.ribProgram_Click(UPDATE_UDD, True)
    End Sub


    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Dim RequiredNodeLevel As Integer
        Dim sMsg As String = ""
        If (SysPanel.TPSDrive = "E:\") Then
            RequiredNodeLevel = 4
        Else
            RequiredNodeLevel = 3
        End If

        If (tpsDiscInserted = False) And (SysPanel.TPSDrive = "F:\") Then
            MessageBox.Show("Please Insert APS Media to Continue")

        ElseIf (trvTpsList.SelectedNode IsNot Nothing) Then
            If (trvTpsList.SelectedNode.Level = RequiredNodeLevel) Then
                Dim PAWSRuntimePath As New StringBuilder(4096)
                GetPrivateProfileString("File Locations", "PAWS_RTS", "", PAWSRuntimePath, 4096, iniFilePath)

                Dim p As New Process
                Dim psi As New ProcessStartInfo(PAWSRuntimePath.ToString, """" & trvTpsList.SelectedNode.ToolTipText & """")
                p.StartInfo = psi
                Try
                    p.Start()
                Catch ex As Exception
                    MessageBox.Show("""" & PAWSRuntimePath.ToString & """ Not Found")
                End Try

            Else
                MessageBox.Show("Please Select a Valid APS")
            End If
        End If

    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        Dim setupProcPath As New StringBuilder(255)
        Dim systemType As New StringBuilder(255)
        Dim setupHTMLFile As String

        GetPrivateProfileString("File Locations", "Setup_Proc_Path", "", setupProcPath, 255, iniFilePath)
        GetPrivateProfileString("System Startup", "SYSTEM_TYPE", "", systemType, 255, iniFilePath)

        If (systemType.ToString().Contains("AN/USM-657(V)2")) Then
            setupHTMLFile = "\TETS_SETUP.mht"
        Else
            setupHTMLFile = "\VIPERT_SETUP.mht"
        End If

        Shell("C:\Program Files\Internet Explorer\IEXPLORE.exe" & " " & setupProcPath.ToString & setupHTMLFile, AppWinStyle.NormalFocus)
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        Me.ribProgram_Click(RESTORE_UDD, True)
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        Dim setupProcPath As New StringBuilder(255)
        Dim systemType As New StringBuilder(255)
        Dim inventoryListFile As String

        GetPrivateProfileString("File Locations", "Setup_Proc_Path", "", setupProcPath, 255, iniFilePath)
        GetPrivateProfileString("System Startup", "SYSTEM_TYPE", "", systemType, 255, iniFilePath)

        If (systemType.ToString().Contains("AN/USM-657(V)2")) Then
            inventoryListFile = "\TETS_InventoryLst.mht"
        Else
            inventoryListFile = "\VIPERT_InventoryLst.mht"
        End If

        Shell("C:\Program Files\Internet Explorer\IEXPLORE.exe" & " " & setupProcPath.ToString & inventoryListFile, AppWinStyle.NormalFocus)
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ribProgram_Click(IADS_READER, True)
    End Sub

    Private Sub frmSysPanl_Load(sender As Object, e As EventArgs)
        If bDebug Then WriteDebugInfo("SYSPANL.frmSysPanl_Load() - calling SysPanel.Main()")
        SysPanel.Main()
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        ribProgram_Click(TPS_TM, True)
    End Sub

    Private Sub frmSysPanl_Load_1(sender As Object, e As EventArgs) Handles MyBase.Load
        If bDebug Then WriteDebugInfo("SYSPANL.frmSysPanl_Load_1() - calling SysPanel.Main()")
        SysPanel.Main()
    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        ribProgram_Click(STOW, True)
    End Sub

    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click
        'THis is for changing TPS directory
        Dim PawsHd As String
        PawsHd = GatherIniFileInformation("File Locations", "PAWS_HD", "")
        If UCase(PawsHd) = "E:\" Then
            UpdateIniFile("File Locations", "PAWS_HD", "F:\")
            TPSDrive = "F:\"
        ElseIf UCase(PawsHd) = "F:\" Then
            UpdateIniFile("File Locations", "PAWS_HD", "E:\")
            TPSDrive = "E:\"
        End If

        RefreshTreeView = True


    End Sub

    Private Sub trvTpsList_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles trvTpsList.AfterSelect

        Dim currNode As String = Me.trvTpsList.SelectedNode.FullPath.ToString()

        If currNode.IndexOf("\") >= 0 Then
            currNode = currNode.Remove(currNode.IndexOf("\"))
        End If

        Dim nodeObj As TreeNode = Me.trvTpsList.SelectedNode

        While (Not nodeObj.Parent Is Nothing)
            nodeObj = nodeObj.Parent()
        End While

        Dim indx As Integer = nodeObj.Index

        'MsgBox("Current Node: " + currNode + " [" + indx.ToString() + "]")
        'Update TPS info based on indx
        UpdateTPSInfo(indx)

    End Sub
End Class