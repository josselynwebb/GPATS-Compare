
Imports System
Imports System.Windows.Forms
Imports System.Text
Imports System.Drawing
Imports System.ServiceProcess
Imports Microsoft.VisualBasic

Public Class frmCheckList
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
    Friend WithEvents PassFrame As Microsoft.VisualBasic.Compatibility.VB6.PictureBoxArray
    Friend WithEvents Icon As Microsoft.VisualBasic.Compatibility.VB6.PictureBoxArray
    Friend WithEvents FailFrame As Microsoft.VisualBasic.Compatibility.VB6.PictureBoxArray
    Friend WithEvents InstrumentLabel As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    Friend WithEvents lblUnit As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    Friend WithEvents picFail As System.Windows.Forms.PictureBox
    Friend WithEvents picPass As System.Windows.Forms.PictureBox
    Friend WithEvents tmrFlash As System.Windows.Forms.Timer
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    Friend WithEvents PassFrame_3 As System.Windows.Forms.PictureBox
    Friend WithEvents PassFrame_1 As System.Windows.Forms.PictureBox
    Friend WithEvents panStartup As System.Windows.Forms.Panel
    Friend WithEvents Icon_3 As System.Windows.Forms.PictureBox
    Friend WithEvents PassFrame_8 As System.Windows.Forms.PictureBox
    Friend WithEvents FailFrame_8 As System.Windows.Forms.PictureBox
    Friend WithEvents Icon_8 As System.Windows.Forms.PictureBox
    Friend WithEvents Icon_5 As System.Windows.Forms.PictureBox
    Friend WithEvents PassFrame_7 As System.Windows.Forms.PictureBox
    Friend WithEvents FailFrame_7 As System.Windows.Forms.PictureBox
    Friend WithEvents Icon_7 As System.Windows.Forms.PictureBox
    Friend WithEvents Icon_6 As System.Windows.Forms.PictureBox
    Friend WithEvents Icon_4 As System.Windows.Forms.PictureBox
    Friend WithEvents Icon_1 As System.Windows.Forms.PictureBox
    Friend WithEvents Icon_2 As System.Windows.Forms.PictureBox
    Friend WithEvents Icon_0 As System.Windows.Forms.PictureBox
    Friend WithEvents FailFrame_6 As System.Windows.Forms.PictureBox
    Friend WithEvents FailFrame_5 As System.Windows.Forms.PictureBox
    Friend WithEvents FailFrame_4 As System.Windows.Forms.PictureBox
    Friend WithEvents FailFrame_3 As System.Windows.Forms.PictureBox
    Friend WithEvents FailFrame_2 As System.Windows.Forms.PictureBox
    Friend WithEvents FailFrame_1 As System.Windows.Forms.PictureBox
    Friend WithEvents FailFrame_0 As System.Windows.Forms.PictureBox
    Friend WithEvents PassFrame_6 As System.Windows.Forms.PictureBox
    Friend WithEvents PassFrame_5 As System.Windows.Forms.PictureBox
    Friend WithEvents PassFrame_4 As System.Windows.Forms.PictureBox
    Friend WithEvents PassFrame_2 As System.Windows.Forms.PictureBox
    Friend WithEvents PassFrame_0 As System.Windows.Forms.PictureBox
    Friend WithEvents InstrumentLabel_8 As System.Windows.Forms.Label
    Friend WithEvents InstrumentLabel_5 As System.Windows.Forms.Label
    Friend WithEvents InstrumentLabel_7 As System.Windows.Forms.Label
    Friend WithEvents InstrumentLabel_2 As System.Windows.Forms.Label
    Friend WithEvents InstrumentLabel_6 As System.Windows.Forms.Label
    Friend WithEvents InstrumentLabel_3 As System.Windows.Forms.Label
    Friend WithEvents InstrumentLabel_4 As System.Windows.Forms.Label
    Friend WithEvents InstrumentLabel_1 As System.Windows.Forms.Label
    Friend WithEvents InstrumentLabel_0 As System.Windows.Forms.Label
    Friend WithEvents lblUnit_2 As System.Windows.Forms.Label
    Friend WithEvents lblUnit_1 As System.Windows.Forms.Label
    Friend WithEvents lblUnit_0 As System.Windows.Forms.Label
    Friend WithEvents sbrUserInformation As System.Windows.Forms.StatusBar
    Friend WithEvents sbrUserInformation_Panel1 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents sbrUserInformation_Panel2 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents startupWorker As System.ComponentModel.BackgroundWorker
    Friend WithEvents Callback1 As Object
    'As AxCbk.AxCallback
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCheckList))
        Me.PassFrame = New Microsoft.VisualBasic.Compatibility.VB6.PictureBoxArray(Me.components)
        Me.PassFrame_3 = New System.Windows.Forms.PictureBox()
        Me.PassFrame_1 = New System.Windows.Forms.PictureBox()
        Me.PassFrame_8 = New System.Windows.Forms.PictureBox()
        Me.PassFrame_7 = New System.Windows.Forms.PictureBox()
        Me.PassFrame_6 = New System.Windows.Forms.PictureBox()
        Me.PassFrame_5 = New System.Windows.Forms.PictureBox()
        Me.PassFrame_4 = New System.Windows.Forms.PictureBox()
        Me.PassFrame_2 = New System.Windows.Forms.PictureBox()
        Me.PassFrame_0 = New System.Windows.Forms.PictureBox()
        Me.Icon = New Microsoft.VisualBasic.Compatibility.VB6.PictureBoxArray(Me.components)
        Me.Icon_3 = New System.Windows.Forms.PictureBox()
        Me.Icon_8 = New System.Windows.Forms.PictureBox()
        Me.Icon_5 = New System.Windows.Forms.PictureBox()
        Me.Icon_7 = New System.Windows.Forms.PictureBox()
        Me.Icon_6 = New System.Windows.Forms.PictureBox()
        Me.Icon_4 = New System.Windows.Forms.PictureBox()
        Me.Icon_1 = New System.Windows.Forms.PictureBox()
        Me.Icon_2 = New System.Windows.Forms.PictureBox()
        Me.Icon_0 = New System.Windows.Forms.PictureBox()
        Me.FailFrame = New Microsoft.VisualBasic.Compatibility.VB6.PictureBoxArray(Me.components)
        Me.FailFrame_8 = New System.Windows.Forms.PictureBox()
        Me.FailFrame_7 = New System.Windows.Forms.PictureBox()
        Me.FailFrame_6 = New System.Windows.Forms.PictureBox()
        Me.FailFrame_5 = New System.Windows.Forms.PictureBox()
        Me.FailFrame_4 = New System.Windows.Forms.PictureBox()
        Me.FailFrame_3 = New System.Windows.Forms.PictureBox()
        Me.FailFrame_2 = New System.Windows.Forms.PictureBox()
        Me.FailFrame_1 = New System.Windows.Forms.PictureBox()
        Me.FailFrame_0 = New System.Windows.Forms.PictureBox()
        Me.InstrumentLabel = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.InstrumentLabel_8 = New System.Windows.Forms.Label()
        Me.InstrumentLabel_5 = New System.Windows.Forms.Label()
        Me.InstrumentLabel_7 = New System.Windows.Forms.Label()
        Me.InstrumentLabel_2 = New System.Windows.Forms.Label()
        Me.InstrumentLabel_6 = New System.Windows.Forms.Label()
        Me.InstrumentLabel_3 = New System.Windows.Forms.Label()
        Me.InstrumentLabel_4 = New System.Windows.Forms.Label()
        Me.InstrumentLabel_1 = New System.Windows.Forms.Label()
        Me.InstrumentLabel_0 = New System.Windows.Forms.Label()
        Me.lblUnit = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.lblUnit_2 = New System.Windows.Forms.Label()
        Me.lblUnit_1 = New System.Windows.Forms.Label()
        Me.lblUnit_0 = New System.Windows.Forms.Label()
        Me.picFail = New System.Windows.Forms.PictureBox()
        Me.picPass = New System.Windows.Forms.PictureBox()
        Me.tmrFlash = New System.Windows.Forms.Timer(Me.components)
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.panStartup = New System.Windows.Forms.Panel()
        Me.sbrUserInformation = New System.Windows.Forms.StatusBar()
        Me.sbrUserInformation_Panel1 = New System.Windows.Forms.StatusBarPanel()
        Me.sbrUserInformation_Panel2 = New System.Windows.Forms.StatusBarPanel()
        Me.startupWorker = New System.ComponentModel.BackgroundWorker()
        CType(Me.PassFrame, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PassFrame_3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PassFrame_1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PassFrame_8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PassFrame_7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PassFrame_6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PassFrame_5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PassFrame_4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PassFrame_2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PassFrame_0, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Icon, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Icon_3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Icon_8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Icon_5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Icon_7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Icon_6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Icon_4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Icon_1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Icon_2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Icon_0, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FailFrame, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FailFrame_8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FailFrame_7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FailFrame_6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FailFrame_5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FailFrame_4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FailFrame_3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FailFrame_2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FailFrame_1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FailFrame_0, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.InstrumentLabel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblUnit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picFail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picPass, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panStartup.SuspendLayout()
        CType(Me.sbrUserInformation_Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sbrUserInformation_Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PassFrame_3
        '
        Me.PassFrame_3.BackColor = System.Drawing.SystemColors.Control
        Me.PassFrame_3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PassFrame_3.Image = CType(resources.GetObject("PassFrame_3.Image"), System.Drawing.Image)
        Me.PassFrame.SetIndex(Me.PassFrame_3, CType(3, Short))
        Me.PassFrame_3.Location = New System.Drawing.Point(18, 99)
        Me.PassFrame_3.Name = "PassFrame_3"
        Me.PassFrame_3.Size = New System.Drawing.Size(32, 16)
        Me.PassFrame_3.TabIndex = 12
        Me.PassFrame_3.TabStop = False
        '
        'PassFrame_1
        '
        Me.PassFrame_1.BackColor = System.Drawing.SystemColors.Control
        Me.PassFrame_1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PassFrame_1.Image = CType(resources.GetObject("PassFrame_1.Image"), System.Drawing.Image)
        Me.PassFrame.SetIndex(Me.PassFrame_1, CType(1, Short))
        Me.PassFrame_1.Location = New System.Drawing.Point(18, 66)
        Me.PassFrame_1.Name = "PassFrame_1"
        Me.PassFrame_1.Size = New System.Drawing.Size(32, 16)
        Me.PassFrame_1.TabIndex = 10
        Me.PassFrame_1.TabStop = False
        '
        'PassFrame_8
        '
        Me.PassFrame_8.BackColor = System.Drawing.SystemColors.Control
        Me.PassFrame_8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PassFrame_8.Image = CType(resources.GetObject("PassFrame_8.Image"), System.Drawing.Image)
        Me.PassFrame.SetIndex(Me.PassFrame_8, CType(8, Short))
        Me.PassFrame_8.Location = New System.Drawing.Point(8, 171)
        Me.PassFrame_8.Name = "PassFrame_8"
        Me.PassFrame_8.Size = New System.Drawing.Size(32, 16)
        Me.PassFrame_8.TabIndex = 42
        Me.PassFrame_8.TabStop = False
        '
        'PassFrame_7
        '
        Me.PassFrame_7.BackColor = System.Drawing.SystemColors.Control
        Me.PassFrame_7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PassFrame_7.Image = CType(resources.GetObject("PassFrame_7.Image"), System.Drawing.Image)
        Me.PassFrame.SetIndex(Me.PassFrame_7, CType(7, Short))
        Me.PassFrame_7.Location = New System.Drawing.Point(8, 155)
        Me.PassFrame_7.Name = "PassFrame_7"
        Me.PassFrame_7.Size = New System.Drawing.Size(32, 16)
        Me.PassFrame_7.TabIndex = 36
        Me.PassFrame_7.TabStop = False
        '
        'PassFrame_6
        '
        Me.PassFrame_6.BackColor = System.Drawing.SystemColors.Control
        Me.PassFrame_6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PassFrame_6.Image = CType(resources.GetObject("PassFrame_6.Image"), System.Drawing.Image)
        Me.PassFrame.SetIndex(Me.PassFrame_6, CType(6, Short))
        Me.PassFrame_6.Location = New System.Drawing.Point(8, 139)
        Me.PassFrame_6.Name = "PassFrame_6"
        Me.PassFrame_6.Size = New System.Drawing.Size(32, 16)
        Me.PassFrame_6.TabIndex = 15
        Me.PassFrame_6.TabStop = False
        '
        'PassFrame_5
        '
        Me.PassFrame_5.BackColor = System.Drawing.SystemColors.Control
        Me.PassFrame_5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PassFrame_5.Image = CType(resources.GetObject("PassFrame_5.Image"), System.Drawing.Image)
        Me.PassFrame.SetIndex(Me.PassFrame_5, CType(5, Short))
        Me.PassFrame_5.Location = New System.Drawing.Point(8, 122)
        Me.PassFrame_5.Name = "PassFrame_5"
        Me.PassFrame_5.Size = New System.Drawing.Size(32, 16)
        Me.PassFrame_5.TabIndex = 14
        Me.PassFrame_5.TabStop = False
        '
        'PassFrame_4
        '
        Me.PassFrame_4.BackColor = System.Drawing.SystemColors.Control
        Me.PassFrame_4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PassFrame_4.Image = CType(resources.GetObject("PassFrame_4.Image"), System.Drawing.Image)
        Me.PassFrame.SetIndex(Me.PassFrame_4, CType(4, Short))
        Me.PassFrame_4.Location = New System.Drawing.Point(8, 105)
        Me.PassFrame_4.Name = "PassFrame_4"
        Me.PassFrame_4.Size = New System.Drawing.Size(32, 16)
        Me.PassFrame_4.TabIndex = 13
        Me.PassFrame_4.TabStop = False
        '
        'PassFrame_2
        '
        Me.PassFrame_2.BackColor = System.Drawing.SystemColors.Control
        Me.PassFrame_2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PassFrame_2.Image = CType(resources.GetObject("PassFrame_2.Image"), System.Drawing.Image)
        Me.PassFrame.SetIndex(Me.PassFrame_2, CType(2, Short))
        Me.PassFrame_2.Location = New System.Drawing.Point(8, 73)
        Me.PassFrame_2.Name = "PassFrame_2"
        Me.PassFrame_2.Size = New System.Drawing.Size(32, 16)
        Me.PassFrame_2.TabIndex = 11
        Me.PassFrame_2.TabStop = False
        '
        'PassFrame_0
        '
        Me.PassFrame_0.BackColor = System.Drawing.SystemColors.Control
        Me.PassFrame_0.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PassFrame_0.Image = CType(resources.GetObject("PassFrame_0.Image"), System.Drawing.Image)
        Me.PassFrame.SetIndex(Me.PassFrame_0, CType(0, Short))
        Me.PassFrame_0.Location = New System.Drawing.Point(8, 40)
        Me.PassFrame_0.Name = "PassFrame_0"
        Me.PassFrame_0.Size = New System.Drawing.Size(32, 16)
        Me.PassFrame_0.TabIndex = 9
        Me.PassFrame_0.TabStop = False
        '
        'Icon_3
        '
        Me.Icon_3.BackColor = System.Drawing.SystemColors.Control
        Me.Icon_3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon_3.Image = CType(resources.GetObject("Icon_3.Image"), System.Drawing.Image)
        Me.Icon.SetIndex(Me.Icon_3, CType(3, Short))
        Me.Icon_3.Location = New System.Drawing.Point(89, 89)
        Me.Icon_3.Name = "Icon_3"
        Me.Icon_3.Size = New System.Drawing.Size(17, 17)
        Me.Icon_3.TabIndex = 43
        Me.Icon_3.TabStop = False
        '
        'Icon_8
        '
        Me.Icon_8.BackColor = System.Drawing.SystemColors.Control
        Me.Icon_8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon_8.Image = CType(resources.GetObject("Icon_8.Image"), System.Drawing.Image)
        Me.Icon.SetIndex(Me.Icon_8, CType(8, Short))
        Me.Icon_8.Location = New System.Drawing.Point(89, 170)
        Me.Icon_8.Name = "Icon_8"
        Me.Icon_8.Size = New System.Drawing.Size(17, 17)
        Me.Icon_8.TabIndex = 39
        Me.Icon_8.TabStop = False
        '
        'Icon_5
        '
        Me.Icon_5.BackColor = System.Drawing.SystemColors.Control
        Me.Icon_5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon_5.Image = CType(resources.GetObject("Icon_5.Image"), System.Drawing.Image)
        Me.Icon.SetIndex(Me.Icon_5, CType(5, Short))
        Me.Icon_5.Location = New System.Drawing.Point(89, 121)
        Me.Icon_5.Name = "Icon_5"
        Me.Icon_5.Size = New System.Drawing.Size(17, 17)
        Me.Icon_5.TabIndex = 37
        Me.Icon_5.TabStop = False
        '
        'Icon_7
        '
        Me.Icon_7.BackColor = System.Drawing.SystemColors.Control
        Me.Icon_7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon_7.Image = CType(resources.GetObject("Icon_7.Image"), System.Drawing.Image)
        Me.Icon.SetIndex(Me.Icon_7, CType(7, Short))
        Me.Icon_7.Location = New System.Drawing.Point(89, 154)
        Me.Icon_7.Name = "Icon_7"
        Me.Icon_7.Size = New System.Drawing.Size(17, 17)
        Me.Icon_7.TabIndex = 31
        Me.Icon_7.TabStop = False
        '
        'Icon_6
        '
        Me.Icon_6.BackColor = System.Drawing.SystemColors.Control
        Me.Icon_6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon_6.Image = CType(resources.GetObject("Icon_6.Image"), System.Drawing.Image)
        Me.Icon.SetIndex(Me.Icon_6, CType(6, Short))
        Me.Icon_6.Location = New System.Drawing.Point(89, 138)
        Me.Icon_6.Name = "Icon_6"
        Me.Icon_6.Size = New System.Drawing.Size(17, 17)
        Me.Icon_6.TabIndex = 30
        Me.Icon_6.TabStop = False
        '
        'Icon_4
        '
        Me.Icon_4.BackColor = System.Drawing.SystemColors.Control
        Me.Icon_4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon_4.Image = CType(resources.GetObject("Icon_4.Image"), System.Drawing.Image)
        Me.Icon.SetIndex(Me.Icon_4, CType(4, Short))
        Me.Icon_4.Location = New System.Drawing.Point(89, 105)
        Me.Icon_4.Name = "Icon_4"
        Me.Icon_4.Size = New System.Drawing.Size(17, 17)
        Me.Icon_4.TabIndex = 29
        Me.Icon_4.TabStop = False
        '
        'Icon_1
        '
        Me.Icon_1.BackColor = System.Drawing.SystemColors.Control
        Me.Icon_1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon_1.Image = CType(resources.GetObject("Icon_1.Image"), System.Drawing.Image)
        Me.Icon.SetIndex(Me.Icon_1, CType(1, Short))
        Me.Icon_1.Location = New System.Drawing.Point(89, 57)
        Me.Icon_1.Name = "Icon_1"
        Me.Icon_1.Size = New System.Drawing.Size(17, 17)
        Me.Icon_1.TabIndex = 28
        Me.Icon_1.TabStop = False
        '
        'Icon_2
        '
        Me.Icon_2.BackColor = System.Drawing.SystemColors.Control
        Me.Icon_2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon_2.Image = CType(resources.GetObject("Icon_2.Image"), System.Drawing.Image)
        Me.Icon.SetIndex(Me.Icon_2, CType(2, Short))
        Me.Icon_2.Location = New System.Drawing.Point(89, 73)
        Me.Icon_2.Name = "Icon_2"
        Me.Icon_2.Size = New System.Drawing.Size(17, 17)
        Me.Icon_2.TabIndex = 27
        Me.Icon_2.TabStop = False
        '
        'Icon_0
        '
        Me.Icon_0.BackColor = System.Drawing.SystemColors.Control
        Me.Icon_0.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon_0.Image = CType(resources.GetObject("Icon_0.Image"), System.Drawing.Image)
        Me.Icon.SetIndex(Me.Icon_0, CType(0, Short))
        Me.Icon_0.Location = New System.Drawing.Point(89, 40)
        Me.Icon_0.Name = "Icon_0"
        Me.Icon_0.Size = New System.Drawing.Size(17, 17)
        Me.Icon_0.TabIndex = 26
        Me.Icon_0.TabStop = False
        '
        'FailFrame_8
        '
        Me.FailFrame_8.BackColor = System.Drawing.SystemColors.Control
        Me.FailFrame_8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FailFrame_8.Image = CType(resources.GetObject("FailFrame_8.Image"), System.Drawing.Image)
        Me.FailFrame.SetIndex(Me.FailFrame_8, CType(8, Short))
        Me.FailFrame_8.Location = New System.Drawing.Point(49, 170)
        Me.FailFrame_8.Name = "FailFrame_8"
        Me.FailFrame_8.Size = New System.Drawing.Size(32, 16)
        Me.FailFrame_8.TabIndex = 41
        Me.FailFrame_8.TabStop = False
        '
        'FailFrame_7
        '
        Me.FailFrame_7.BackColor = System.Drawing.SystemColors.Control
        Me.FailFrame_7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FailFrame_7.Image = CType(resources.GetObject("FailFrame_7.Image"), System.Drawing.Image)
        Me.FailFrame.SetIndex(Me.FailFrame_7, CType(7, Short))
        Me.FailFrame_7.Location = New System.Drawing.Point(49, 154)
        Me.FailFrame_7.Name = "FailFrame_7"
        Me.FailFrame_7.Size = New System.Drawing.Size(32, 16)
        Me.FailFrame_7.TabIndex = 35
        Me.FailFrame_7.TabStop = False
        '
        'FailFrame_6
        '
        Me.FailFrame_6.BackColor = System.Drawing.SystemColors.Control
        Me.FailFrame_6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FailFrame_6.Image = CType(resources.GetObject("FailFrame_6.Image"), System.Drawing.Image)
        Me.FailFrame.SetIndex(Me.FailFrame_6, CType(6, Short))
        Me.FailFrame_6.Location = New System.Drawing.Point(49, 138)
        Me.FailFrame_6.Name = "FailFrame_6"
        Me.FailFrame_6.Size = New System.Drawing.Size(32, 16)
        Me.FailFrame_6.TabIndex = 22
        Me.FailFrame_6.TabStop = False
        '
        'FailFrame_5
        '
        Me.FailFrame_5.BackColor = System.Drawing.SystemColors.Control
        Me.FailFrame_5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FailFrame_5.Image = CType(resources.GetObject("FailFrame_5.Image"), System.Drawing.Image)
        Me.FailFrame.SetIndex(Me.FailFrame_5, CType(5, Short))
        Me.FailFrame_5.Location = New System.Drawing.Point(49, 121)
        Me.FailFrame_5.Name = "FailFrame_5"
        Me.FailFrame_5.Size = New System.Drawing.Size(32, 16)
        Me.FailFrame_5.TabIndex = 21
        Me.FailFrame_5.TabStop = False
        '
        'FailFrame_4
        '
        Me.FailFrame_4.BackColor = System.Drawing.SystemColors.Control
        Me.FailFrame_4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FailFrame_4.Image = CType(resources.GetObject("FailFrame_4.Image"), System.Drawing.Image)
        Me.FailFrame.SetIndex(Me.FailFrame_4, CType(4, Short))
        Me.FailFrame_4.Location = New System.Drawing.Point(49, 105)
        Me.FailFrame_4.Name = "FailFrame_4"
        Me.FailFrame_4.Size = New System.Drawing.Size(32, 16)
        Me.FailFrame_4.TabIndex = 20
        Me.FailFrame_4.TabStop = False
        '
        'FailFrame_3
        '
        Me.FailFrame_3.BackColor = System.Drawing.SystemColors.Control
        Me.FailFrame_3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FailFrame_3.Image = CType(resources.GetObject("FailFrame_3.Image"), System.Drawing.Image)
        Me.FailFrame.SetIndex(Me.FailFrame_3, CType(3, Short))
        Me.FailFrame_3.Location = New System.Drawing.Point(49, 89)
        Me.FailFrame_3.Name = "FailFrame_3"
        Me.FailFrame_3.Size = New System.Drawing.Size(32, 16)
        Me.FailFrame_3.TabIndex = 19
        Me.FailFrame_3.TabStop = False
        '
        'FailFrame_2
        '
        Me.FailFrame_2.BackColor = System.Drawing.SystemColors.Control
        Me.FailFrame_2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FailFrame_2.Image = CType(resources.GetObject("FailFrame_2.Image"), System.Drawing.Image)
        Me.FailFrame.SetIndex(Me.FailFrame_2, CType(2, Short))
        Me.FailFrame_2.Location = New System.Drawing.Point(49, 73)
        Me.FailFrame_2.Name = "FailFrame_2"
        Me.FailFrame_2.Size = New System.Drawing.Size(32, 16)
        Me.FailFrame_2.TabIndex = 18
        Me.FailFrame_2.TabStop = False
        '
        'FailFrame_1
        '
        Me.FailFrame_1.BackColor = System.Drawing.SystemColors.Control
        Me.FailFrame_1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FailFrame_1.Image = CType(resources.GetObject("FailFrame_1.Image"), System.Drawing.Image)
        Me.FailFrame.SetIndex(Me.FailFrame_1, CType(1, Short))
        Me.FailFrame_1.Location = New System.Drawing.Point(49, 57)
        Me.FailFrame_1.Name = "FailFrame_1"
        Me.FailFrame_1.Size = New System.Drawing.Size(32, 16)
        Me.FailFrame_1.TabIndex = 17
        Me.FailFrame_1.TabStop = False
        '
        'FailFrame_0
        '
        Me.FailFrame_0.BackColor = System.Drawing.SystemColors.Control
        Me.FailFrame_0.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FailFrame_0.Image = CType(resources.GetObject("FailFrame_0.Image"), System.Drawing.Image)
        Me.FailFrame.SetIndex(Me.FailFrame_0, CType(0, Short))
        Me.FailFrame_0.Location = New System.Drawing.Point(49, 40)
        Me.FailFrame_0.Name = "FailFrame_0"
        Me.FailFrame_0.Size = New System.Drawing.Size(32, 16)
        Me.FailFrame_0.TabIndex = 16
        Me.FailFrame_0.TabStop = False
        '
        'InstrumentLabel_8
        '
        Me.InstrumentLabel_8.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.InstrumentLabel_8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InstrumentLabel_8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.InstrumentLabel.SetIndex(Me.InstrumentLabel_8, CType(8, Short))
        Me.InstrumentLabel_8.Location = New System.Drawing.Point(113, 170)
        Me.InstrumentLabel_8.Name = "InstrumentLabel_8"
        Me.InstrumentLabel_8.Size = New System.Drawing.Size(229, 13)
        Me.InstrumentLabel_8.TabIndex = 40
        Me.InstrumentLabel_8.Text = "Check System Calibration Dates"
        '
        'InstrumentLabel_5
        '
        Me.InstrumentLabel_5.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.InstrumentLabel_5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InstrumentLabel_5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.InstrumentLabel.SetIndex(Me.InstrumentLabel_5, CType(5, Short))
        Me.InstrumentLabel_5.Location = New System.Drawing.Point(113, 121)
        Me.InstrumentLabel_5.Name = "InstrumentLabel_5"
        Me.InstrumentLabel_5.Size = New System.Drawing.Size(222, 13)
        Me.InstrumentLabel_5.TabIndex = 38
        Me.InstrumentLabel_5.Text = "Initialize MXI-VXI Resource Manager"
        '
        'InstrumentLabel_7
        '
        Me.InstrumentLabel_7.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.InstrumentLabel_7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InstrumentLabel_7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.InstrumentLabel.SetIndex(Me.InstrumentLabel_7, CType(7, Short))
        Me.InstrumentLabel_7.Location = New System.Drawing.Point(113, 154)
        Me.InstrumentLabel_7.Name = "InstrumentLabel_7"
        Me.InstrumentLabel_7.Size = New System.Drawing.Size(197, 13)
        Me.InstrumentLabel_7.TabIndex = 7
        Me.InstrumentLabel_7.Text = "Run System Survey"
        '
        'InstrumentLabel_2
        '
        Me.InstrumentLabel_2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.InstrumentLabel_2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InstrumentLabel_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.InstrumentLabel.SetIndex(Me.InstrumentLabel_2, CType(2, Short))
        Me.InstrumentLabel_2.Location = New System.Drawing.Point(113, 73)
        Me.InstrumentLabel_2.Name = "InstrumentLabel_2"
        Me.InstrumentLabel_2.Size = New System.Drawing.Size(222, 13)
        Me.InstrumentLabel_2.TabIndex = 25
        Me.InstrumentLabel_2.Text = "Check FPU Power Supplies"
        '
        'InstrumentLabel_6
        '
        Me.InstrumentLabel_6.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.InstrumentLabel_6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InstrumentLabel_6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.InstrumentLabel.SetIndex(Me.InstrumentLabel_6, CType(6, Short))
        Me.InstrumentLabel_6.Location = New System.Drawing.Point(113, 138)
        Me.InstrumentLabel_6.Name = "InstrumentLabel_6"
        Me.InstrumentLabel_6.Size = New System.Drawing.Size(140, 13)
        Me.InstrumentLabel_6.TabIndex = 24
        Me.InstrumentLabel_6.Text = "Start System Monitoring"
        '
        'InstrumentLabel_3
        '
        Me.InstrumentLabel_3.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.InstrumentLabel_3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InstrumentLabel_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.InstrumentLabel.SetIndex(Me.InstrumentLabel_3, CType(3, Short))
        Me.InstrumentLabel_3.Location = New System.Drawing.Point(113, 89)
        Me.InstrumentLabel_3.Name = "InstrumentLabel_3"
        Me.InstrumentLabel_3.Size = New System.Drawing.Size(200, 13)
        Me.InstrumentLabel_3.TabIndex = 8
        Me.InstrumentLabel_3.Text = "Start VXI Chassis Mainframes"
        '
        'InstrumentLabel_4
        '
        Me.InstrumentLabel_4.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.InstrumentLabel_4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InstrumentLabel_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.InstrumentLabel.SetIndex(Me.InstrumentLabel_4, CType(4, Short))
        Me.InstrumentLabel_4.Location = New System.Drawing.Point(113, 105)
        Me.InstrumentLabel_4.Name = "InstrumentLabel_4"
        Me.InstrumentLabel_4.Size = New System.Drawing.Size(222, 13)
        Me.InstrumentLabel_4.TabIndex = 6
        Me.InstrumentLabel_4.Text = "Initialize PCI-MXI Controller"
        '
        'InstrumentLabel_1
        '
        Me.InstrumentLabel_1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.InstrumentLabel_1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InstrumentLabel_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.InstrumentLabel.SetIndex(Me.InstrumentLabel_1, CType(1, Short))
        Me.InstrumentLabel_1.Location = New System.Drawing.Point(113, 57)
        Me.InstrumentLabel_1.Name = "InstrumentLabel_1"
        Me.InstrumentLabel_1.Size = New System.Drawing.Size(230, 13)
        Me.InstrumentLabel_1.TabIndex = 5
        Me.InstrumentLabel_1.Text = "Check VXI Chassis Operating Temperature"
        '
        'InstrumentLabel_0
        '
        Me.InstrumentLabel_0.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.InstrumentLabel_0.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InstrumentLabel_0.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.InstrumentLabel.SetIndex(Me.InstrumentLabel_0, CType(0, Short))
        Me.InstrumentLabel_0.Location = New System.Drawing.Point(113, 40)
        Me.InstrumentLabel_0.Name = "InstrumentLabel_0"
        Me.InstrumentLabel_0.Size = New System.Drawing.Size(230, 13)
        Me.InstrumentLabel_0.TabIndex = 4
        Me.InstrumentLabel_0.Text = "Start General Purpose Interface Bus (GPIB)"
        '
        'lblUnit_2
        '
        Me.lblUnit_2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblUnit_2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUnit_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnit.SetIndex(Me.lblUnit_2, CType(2, Short))
        Me.lblUnit_2.Location = New System.Drawing.Point(49, 24)
        Me.lblUnit_2.Name = "lblUnit_2"
        Me.lblUnit_2.Size = New System.Drawing.Size(33, 17)
        Me.lblUnit_2.TabIndex = 3
        Me.lblUnit_2.Text = "Fail"
        '
        'lblUnit_1
        '
        Me.lblUnit_1.BackColor = System.Drawing.Color.Silver
        Me.lblUnit_1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUnit_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnit.SetIndex(Me.lblUnit_1, CType(1, Short))
        Me.lblUnit_1.Location = New System.Drawing.Point(8, 24)
        Me.lblUnit_1.Name = "lblUnit_1"
        Me.lblUnit_1.Size = New System.Drawing.Size(33, 17)
        Me.lblUnit_1.TabIndex = 2
        Me.lblUnit_1.Text = "Pass"
        '
        'lblUnit_0
        '
        Me.lblUnit_0.BackColor = System.Drawing.Color.Silver
        Me.lblUnit_0.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUnit_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnit.SetIndex(Me.lblUnit_0, CType(0, Short))
        Me.lblUnit_0.Location = New System.Drawing.Point(8, 8)
        Me.lblUnit_0.Name = "lblUnit_0"
        Me.lblUnit_0.Size = New System.Drawing.Size(58, 17)
        Me.lblUnit_0.TabIndex = 1
        Me.lblUnit_0.Text = "Status:"
        '
        'picFail
        '
        Me.picFail.BackColor = System.Drawing.SystemColors.Control
        Me.picFail.Image = CType(resources.GetObject("picFail.Image"), System.Drawing.Image)
        Me.picFail.Location = New System.Drawing.Point(170, 239)
        Me.picFail.Name = "picFail"
        Me.picFail.Size = New System.Drawing.Size(32, 16)
        Me.picFail.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picFail.TabIndex = 34
        Me.picFail.TabStop = False
        Me.picFail.Visible = False
        '
        'picPass
        '
        Me.picPass.BackColor = System.Drawing.SystemColors.Control
        Me.picPass.Image = CType(resources.GetObject("picPass.Image"), System.Drawing.Image)
        Me.picPass.Location = New System.Drawing.Point(138, 239)
        Me.picPass.Name = "picPass"
        Me.picPass.Size = New System.Drawing.Size(32, 16)
        Me.picPass.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picPass.TabIndex = 33
        Me.picPass.TabStop = False
        Me.picPass.Visible = False
        '
        'tmrFlash
        '
        Me.tmrFlash.Interval = 300
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Location = New System.Drawing.Point(279, 226)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(76, 23)
        Me.cmdClose.TabIndex = 23
        Me.cmdClose.Text = "&Close"
        Me.cmdClose.UseVisualStyleBackColor = False
        Me.cmdClose.Visible = False
        '
        'panStartup
        '
        Me.panStartup.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panStartup.Controls.Add(Me.Icon_3)
        Me.panStartup.Controls.Add(Me.PassFrame_8)
        Me.panStartup.Controls.Add(Me.FailFrame_8)
        Me.panStartup.Controls.Add(Me.Icon_8)
        Me.panStartup.Controls.Add(Me.Icon_5)
        Me.panStartup.Controls.Add(Me.PassFrame_7)
        Me.panStartup.Controls.Add(Me.FailFrame_7)
        Me.panStartup.Controls.Add(Me.Icon_7)
        Me.panStartup.Controls.Add(Me.Icon_6)
        Me.panStartup.Controls.Add(Me.Icon_4)
        Me.panStartup.Controls.Add(Me.Icon_1)
        Me.panStartup.Controls.Add(Me.Icon_2)
        Me.panStartup.Controls.Add(Me.Icon_0)
        Me.panStartup.Controls.Add(Me.FailFrame_6)
        Me.panStartup.Controls.Add(Me.FailFrame_5)
        Me.panStartup.Controls.Add(Me.FailFrame_4)
        Me.panStartup.Controls.Add(Me.FailFrame_3)
        Me.panStartup.Controls.Add(Me.FailFrame_2)
        Me.panStartup.Controls.Add(Me.FailFrame_1)
        Me.panStartup.Controls.Add(Me.FailFrame_0)
        Me.panStartup.Controls.Add(Me.PassFrame_6)
        Me.panStartup.Controls.Add(Me.PassFrame_5)
        Me.panStartup.Controls.Add(Me.PassFrame_4)
        Me.panStartup.Controls.Add(Me.PassFrame_2)
        Me.panStartup.Controls.Add(Me.PassFrame_0)
        Me.panStartup.Controls.Add(Me.InstrumentLabel_8)
        Me.panStartup.Controls.Add(Me.InstrumentLabel_5)
        Me.panStartup.Controls.Add(Me.InstrumentLabel_7)
        Me.panStartup.Controls.Add(Me.InstrumentLabel_2)
        Me.panStartup.Controls.Add(Me.InstrumentLabel_6)
        Me.panStartup.Controls.Add(Me.InstrumentLabel_3)
        Me.panStartup.Controls.Add(Me.InstrumentLabel_4)
        Me.panStartup.Controls.Add(Me.InstrumentLabel_1)
        Me.panStartup.Controls.Add(Me.InstrumentLabel_0)
        Me.panStartup.Controls.Add(Me.lblUnit_2)
        Me.panStartup.Controls.Add(Me.lblUnit_1)
        Me.panStartup.Controls.Add(Me.lblUnit_0)
        Me.panStartup.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panStartup.Location = New System.Drawing.Point(8, 8)
        Me.panStartup.Name = "panStartup"
        Me.panStartup.Size = New System.Drawing.Size(349, 207)
        Me.panStartup.TabIndex = 0
        '
        'sbrUserInformation
        '
        Me.sbrUserInformation.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.sbrUserInformation.Location = New System.Drawing.Point(0, 294)
        Me.sbrUserInformation.Name = "sbrUserInformation"
        Me.sbrUserInformation.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.sbrUserInformation_Panel1, Me.sbrUserInformation_Panel2})
        Me.sbrUserInformation.ShowPanels = True
        Me.sbrUserInformation.Size = New System.Drawing.Size(373, 21)
        Me.sbrUserInformation.SizingGrip = False
        Me.sbrUserInformation.TabIndex = 32
        '
        'sbrUserInformation_Panel1
        '
        Me.sbrUserInformation_Panel1.Name = "sbrUserInformation_Panel1"
        Me.sbrUserInformation_Panel1.Width = 330
        '
        'sbrUserInformation_Panel2
        '
        Me.sbrUserInformation_Panel2.MinWidth = 34
        Me.sbrUserInformation_Panel2.Name = "sbrUserInformation_Panel2"
        Me.sbrUserInformation_Panel2.Width = 50
        '
        'startupWorker
        '
        '
        'frmCheckList
        '
        Me.BackColor = System.Drawing.Color.Silver
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(373, 315)
        Me.ControlBox = False
        Me.Controls.Add(Me.picFail)
        Me.Controls.Add(Me.picPass)
        Me.Controls.Add(Me.cmdClose)
        Me.Controls.Add(Me.PassFrame_3)
        Me.Controls.Add(Me.PassFrame_1)
        Me.Controls.Add(Me.panStartup)
        Me.Controls.Add(Me.sbrUserInformation)
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCheckList"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ATS System Startup"
        CType(Me.PassFrame, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PassFrame_3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PassFrame_1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PassFrame_8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PassFrame_7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PassFrame_6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PassFrame_5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PassFrame_4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PassFrame_2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PassFrame_0, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Icon, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Icon_3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Icon_8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Icon_5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Icon_7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Icon_6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Icon_4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Icon_1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Icon_2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Icon_0, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FailFrame, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FailFrame_8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FailFrame_7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FailFrame_6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FailFrame_5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FailFrame_4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FailFrame_3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FailFrame_2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FailFrame_1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FailFrame_0, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.InstrumentLabel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblUnit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picFail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picPass, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panStartup.ResumeLayout(False)
        CType(Me.sbrUserInformation_Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sbrUserInformation_Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

End Sub

#End Region

    '=========================================================
    '************************************************************
    '* Virtual Instrument Portable Equipment Repair/Test(VIPERT)*
    '*                                                          *
    '* Nomenclature   : SYSTEM: System Monitor CheckList Form   *
    '* Purpose        : Displays a check list of the system     *
    '*                  startup steps.                          *
    '************************************************************

    Private Sub cmdClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdClose.Click

        UserTimeout = 0

    End Sub


    Private Sub tmrFlash_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmrFlash.Tick
        
        Static ColorRed As Short
        
        Dim LabelIndexToBlink As Short

        'LabelIndexToBlink = ValFromObject(tmrFlash.Tag)
        LabelIndexToBlink = tmrFlash.Tag
        If ColorRed Then
            InstrumentLabel(LabelIndexToBlink).ForeColor = ColorTranslator.FromOle(RGB(255, 0, 0))
            ColorRed = False
        Else
            InstrumentLabel(LabelIndexToBlink).ForeColor = ColorTranslator.FromOle(RGB(255, 255, 0))
            ColorRed = True
        End If

    End Sub



    Private Sub startupWorker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles startupWorker.DoWork

        Select Case StartupTestIndex
            Case 0
                'TestName = "General Purpose Instruction Bus Initialization"
                FailureMessage = InitGpibSupplies()
            Case 1
                'TestName = "VXI Chassis Operating Temperature"
                FailureMessage = ChassisTemperatureCheck()
            Case 2
                'TestName = "VXI Chassis Backplane Power Supplies"
                FailureMessage = ChassisBackplaneSupplyCheck()
            Case 3
                'TestName = "VXI Chassis Mainframes"
                'Check if CICL is running, kill service if no
                Dim service As ServiceController = New ServiceController("CICLKernel")
                If service.Status.Equals(ServiceControllerStatus.Running) Then
                    service.Stop()
                End If

                'Removed...it was causing Maintenance account not be able to start chasis.  RF is being removed

                'Check if RFMS is running, kill service if no    
                'Dim RFMSservice As ServiceController = New ServiceController("RFMS")
                'If RFMSservice.Status.Equals(ServiceControllerStatus.Running) Then
                '    RFMSservice.Stop()
                'End If

                FailureMessage = StartChassis()
                
            Case 4
                'TestName = "PCI-MXI Controller"
                FailureMessage = sInitDevice(MXI2)
            Case 5
                'TestName = "MXI-VXI Resource Manager"
                'Run resman
                FailureMessage = RunResMan()

                'Check that current system configuration matches controller
                Dim correctConfiguration = CheckSystemConfiguration()
                If (correctConfiguration = False) Then
                    Dim result As Integer = MessageBox.Show("Invalid System Configuration Detected. Would You Like run Setup?",
                        "Invalid Configuration", MessageBoxButtons.YesNo)
                    If (result = DialogResult.Yes) Then
                        FailureMessage = "Invalid System Configuration Detected. Setup Called"
                    Else
                        FailureMessage = "Invalid System Configuration Detected. Aborting ATS Startup"
                    End If
                End If

                If FailureMessage = "" Then 'Test Passed
                    Try
                        'Start the CICL
                        Dim service As ServiceController = New ServiceController("CICLKernel")
                        service.Start()
                    Catch ex As Exception
                        FailureMessage = "The CICL has failed to start"
                    End Try
                End If

            Case 6
                'Wait for CICL to Start
                'Delay(15)
                WriteChassisStateToIniFile(True)
                Init_CICL()
                Delay(0.5)
            Case 7
                If (RestartSysmonFlag = False) Then
                    UpdateLogFile() 'Write startup header to log
                    'update ethernet port ip addresses
                    SetIPAddress()
                    FailureMessage = ShellToCtest()
                End If

            Case 8
                'TestName = "System Calibration Period"
                FailureMessage = CheckCalDates()
                Delay(0.5)
        End Select
    End Sub

End Class