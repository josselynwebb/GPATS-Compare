
Imports System.Windows.Forms

Public Class frmCTest
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
    Friend WithEvents pctIcon As Microsoft.VisualBasic.Compatibility.VB6.PictureBoxArray
    Friend WithEvents InstrumentLabel As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    Friend WithEvents imgPassFrame As Microsoft.VisualBasic.Compatibility.VB6.PictureBoxArray
    Friend WithEvents imgFailFrame As Microsoft.VisualBasic.Compatibility.VB6.PictureBoxArray
    Friend WithEvents CloseButton As System.Windows.Forms.Button
    Friend WithEvents Details As System.Windows.Forms.Button
    Friend WithEvents SSPanel1 As System.Windows.Forms.PictureBox
    Friend WithEvents pctIcon_26 As System.Windows.Forms.PictureBox
    Friend WithEvents pctIcon_27 As System.Windows.Forms.PictureBox
    Friend WithEvents pctIcon_17 As System.Windows.Forms.PictureBox
    Friend WithEvents pctIcon_1 As System.Windows.Forms.PictureBox
    Friend WithEvents pctIcon_3 As System.Windows.Forms.PictureBox
    Friend WithEvents pctIcon_6 As System.Windows.Forms.PictureBox
    Friend WithEvents pctIcon_7 As System.Windows.Forms.PictureBox
    Friend WithEvents pctIcon_2 As System.Windows.Forms.PictureBox
    Friend WithEvents pctIcon_5 As System.Windows.Forms.PictureBox
    Friend WithEvents pctIcon_4 As System.Windows.Forms.PictureBox
    Friend WithEvents pctIcon_23 As System.Windows.Forms.PictureBox
    Friend WithEvents pctIcon_25 As System.Windows.Forms.PictureBox
    Friend WithEvents pctIcon_22 As System.Windows.Forms.PictureBox
    Friend WithEvents pctIcon_24 As System.Windows.Forms.PictureBox
    Friend WithEvents pctIcon_20 As System.Windows.Forms.PictureBox
    Friend WithEvents pctIcon_15 As System.Windows.Forms.PictureBox
    Friend WithEvents pctIcon_14 As System.Windows.Forms.PictureBox
    Friend WithEvents pctIcon_13 As System.Windows.Forms.PictureBox
    Friend WithEvents pctIcon_12 As System.Windows.Forms.PictureBox
    Friend WithEvents pctIcon_11 As System.Windows.Forms.PictureBox
    Friend WithEvents pctIcon_10 As System.Windows.Forms.PictureBox
    Friend WithEvents pctIcon_9 As System.Windows.Forms.PictureBox
    Friend WithEvents pctIcon_16 As System.Windows.Forms.PictureBox
    Friend WithEvents pctIcon_8 As System.Windows.Forms.PictureBox
    Friend WithEvents pctIcon_0 As System.Windows.Forms.PictureBox
    Friend WithEvents InstrumentLabel_26 As System.Windows.Forms.Label
    Friend WithEvents InstrumentLabel_27 As System.Windows.Forms.Label
    Friend WithEvents imgPassFrame_27 As System.Windows.Forms.PictureBox
    Friend WithEvents imgFailFrame_27 As System.Windows.Forms.PictureBox
    Friend WithEvents imgFailFrame_12 As System.Windows.Forms.PictureBox
    Friend WithEvents imgPassFrame_26 As System.Windows.Forms.PictureBox
    Friend WithEvents imgPassFrame_14 As System.Windows.Forms.PictureBox
    Friend WithEvents imgFailFrame_26 As System.Windows.Forms.PictureBox
    Friend WithEvents InstrumentLabel_17 As System.Windows.Forms.Label
    Friend WithEvents InstrumentLabel_1 As System.Windows.Forms.Label
    Friend WithEvents InstrumentLabel_3 As System.Windows.Forms.Label
    Friend WithEvents InstrumentLabel_2 As System.Windows.Forms.Label
    Friend WithEvents InstrumentLabel_6 As System.Windows.Forms.Label
    Friend WithEvents InstrumentLabel_5 As System.Windows.Forms.Label
    Friend WithEvents InstrumentLabel_4 As System.Windows.Forms.Label
    Friend WithEvents InstrumentLabel_7 As System.Windows.Forms.Label
    Friend WithEvents InstrumentLabel_25 As System.Windows.Forms.Label
    Friend WithEvents InstrumentLabel_22 As System.Windows.Forms.Label
    Friend WithEvents InstrumentLabel_23 As System.Windows.Forms.Label
    Friend WithEvents InstrumentLabel_24 As System.Windows.Forms.Label
    Friend WithEvents InstrumentLabel_20 As System.Windows.Forms.Label
    Friend WithEvents imgPassFrame_20 As System.Windows.Forms.PictureBox
    Friend WithEvents imgPassFrame_22 As System.Windows.Forms.PictureBox
    Friend WithEvents imgPassFrame_23 As System.Windows.Forms.PictureBox
    Friend WithEvents imgPassFrame_24 As System.Windows.Forms.PictureBox
    Friend WithEvents imgPassFrame_25 As System.Windows.Forms.PictureBox
    Friend WithEvents imgFailFrame_20 As System.Windows.Forms.PictureBox
    Friend WithEvents imgFailFrame_22 As System.Windows.Forms.PictureBox
    Friend WithEvents imgFailFrame_23 As System.Windows.Forms.PictureBox
    Friend WithEvents imgFailFrame_24 As System.Windows.Forms.PictureBox
    Friend WithEvents imgFailFrame_25 As System.Windows.Forms.PictureBox
    Friend WithEvents imgFailFrame_17 As System.Windows.Forms.PictureBox
    Friend WithEvents imgFailFrame_16 As System.Windows.Forms.PictureBox
    Friend WithEvents imgFailFrame_15 As System.Windows.Forms.PictureBox
    Friend WithEvents imgFailFrame_14 As System.Windows.Forms.PictureBox
    Friend WithEvents imgFailFrame_13 As System.Windows.Forms.PictureBox
    Friend WithEvents imgFailFrame_11 As System.Windows.Forms.PictureBox
    Friend WithEvents imgFailFrame_10 As System.Windows.Forms.PictureBox
    Friend WithEvents imgFailFrame_9 As System.Windows.Forms.PictureBox
    Friend WithEvents imgFailFrame_8 As System.Windows.Forms.PictureBox
    Friend WithEvents imgFailFrame_7 As System.Windows.Forms.PictureBox
    Friend WithEvents imgFailFrame_6 As System.Windows.Forms.PictureBox
    Friend WithEvents imgFailFrame_5 As System.Windows.Forms.PictureBox
    Friend WithEvents imgFailFrame_4 As System.Windows.Forms.PictureBox
    Friend WithEvents imgFailFrame_3 As System.Windows.Forms.PictureBox
    Friend WithEvents imgFailFrame_2 As System.Windows.Forms.PictureBox
    Friend WithEvents imgFailFrame_1 As System.Windows.Forms.PictureBox
    Friend WithEvents imgFailFrame_0 As System.Windows.Forms.PictureBox
    Friend WithEvents imgPassFrame_17 As System.Windows.Forms.PictureBox
    Friend WithEvents imgPassFrame_16 As System.Windows.Forms.PictureBox
    Friend WithEvents imgPassFrame_15 As System.Windows.Forms.PictureBox
    Friend WithEvents imgPassFrame_13 As System.Windows.Forms.PictureBox
    Friend WithEvents imgPassFrame_12 As System.Windows.Forms.PictureBox
    Friend WithEvents imgPassFrame_11 As System.Windows.Forms.PictureBox
    Friend WithEvents imgPassFrame_10 As System.Windows.Forms.PictureBox
    Friend WithEvents imgPassFrame_9 As System.Windows.Forms.PictureBox
    Friend WithEvents imgPassFrame_8 As System.Windows.Forms.PictureBox
    Friend WithEvents imgPassFrame_7 As System.Windows.Forms.PictureBox
    Friend WithEvents imgPassFrame_6 As System.Windows.Forms.PictureBox
    Friend WithEvents imgPassFrame_5 As System.Windows.Forms.PictureBox
    Friend WithEvents imgPassFrame_4 As System.Windows.Forms.PictureBox
    Friend WithEvents imgPassFrame_3 As System.Windows.Forms.PictureBox
    Friend WithEvents imgPassFrame_2 As System.Windows.Forms.PictureBox
    Friend WithEvents imgPassFrame_1 As System.Windows.Forms.PictureBox
    Friend WithEvents imgPassFrame_0 As System.Windows.Forms.PictureBox
    Friend WithEvents InstrumentLabel_15 As System.Windows.Forms.Label
    Friend WithEvents InstrumentLabel_14 As System.Windows.Forms.Label
    Friend WithEvents InstrumentLabel_13 As System.Windows.Forms.Label
    Friend WithEvents InstrumentLabel_12 As System.Windows.Forms.Label
    Friend WithEvents InstrumentLabel_11 As System.Windows.Forms.Label
    Friend WithEvents InstrumentLabel_10 As System.Windows.Forms.Label
    Friend WithEvents InstrumentLabel_9 As System.Windows.Forms.Label
    Friend WithEvents InstrumentLabel_16 As System.Windows.Forms.Label
    Friend WithEvents InstrumentLabel_8 As System.Windows.Forms.Label
    Friend WithEvents InstrumentLabel_0 As System.Windows.Forms.Label
    Friend WithEvents DetailsText As System.Windows.Forms.RichTextBox
    Friend WithEvents sbrUserInformation As System.Windows.Forms.StatusBar
    Friend WithEvents sbrUserInformation_Panel1 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents imgLogo As System.Windows.Forms.PictureBox
    Friend WithEvents imlImageList As System.Windows.Forms.ImageList
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCTest))
        Me.pctIcon = New Microsoft.VisualBasic.Compatibility.VB6.PictureBoxArray(Me.components)
        Me.pctIcon_26 = New System.Windows.Forms.PictureBox()
        Me.pctIcon_27 = New System.Windows.Forms.PictureBox()
        Me.pctIcon_17 = New System.Windows.Forms.PictureBox()
        Me.pctIcon_1 = New System.Windows.Forms.PictureBox()
        Me.pctIcon_3 = New System.Windows.Forms.PictureBox()
        Me.pctIcon_6 = New System.Windows.Forms.PictureBox()
        Me.pctIcon_7 = New System.Windows.Forms.PictureBox()
        Me.pctIcon_2 = New System.Windows.Forms.PictureBox()
        Me.pctIcon_5 = New System.Windows.Forms.PictureBox()
        Me.pctIcon_4 = New System.Windows.Forms.PictureBox()
        Me.pctIcon_23 = New System.Windows.Forms.PictureBox()
        Me.pctIcon_25 = New System.Windows.Forms.PictureBox()
        Me.pctIcon_22 = New System.Windows.Forms.PictureBox()
        Me.pctIcon_24 = New System.Windows.Forms.PictureBox()
        Me.pctIcon_20 = New System.Windows.Forms.PictureBox()
        Me.pctIcon_15 = New System.Windows.Forms.PictureBox()
        Me.pctIcon_14 = New System.Windows.Forms.PictureBox()
        Me.pctIcon_13 = New System.Windows.Forms.PictureBox()
        Me.pctIcon_12 = New System.Windows.Forms.PictureBox()
        Me.pctIcon_11 = New System.Windows.Forms.PictureBox()
        Me.pctIcon_10 = New System.Windows.Forms.PictureBox()
        Me.pctIcon_9 = New System.Windows.Forms.PictureBox()
        Me.pctIcon_16 = New System.Windows.Forms.PictureBox()
        Me.pctIcon_8 = New System.Windows.Forms.PictureBox()
        Me.pctIcon_0 = New System.Windows.Forms.PictureBox()
        Me.InstrumentLabel = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.InstrumentLabel_26 = New System.Windows.Forms.Label()
        Me.InstrumentLabel_27 = New System.Windows.Forms.Label()
        Me.InstrumentLabel_17 = New System.Windows.Forms.Label()
        Me.InstrumentLabel_1 = New System.Windows.Forms.Label()
        Me.InstrumentLabel_3 = New System.Windows.Forms.Label()
        Me.InstrumentLabel_2 = New System.Windows.Forms.Label()
        Me.InstrumentLabel_6 = New System.Windows.Forms.Label()
        Me.InstrumentLabel_5 = New System.Windows.Forms.Label()
        Me.InstrumentLabel_4 = New System.Windows.Forms.Label()
        Me.InstrumentLabel_7 = New System.Windows.Forms.Label()
        Me.InstrumentLabel_25 = New System.Windows.Forms.Label()
        Me.InstrumentLabel_22 = New System.Windows.Forms.Label()
        Me.InstrumentLabel_23 = New System.Windows.Forms.Label()
        Me.InstrumentLabel_24 = New System.Windows.Forms.Label()
        Me.InstrumentLabel_20 = New System.Windows.Forms.Label()
        Me.InstrumentLabel_15 = New System.Windows.Forms.Label()
        Me.InstrumentLabel_14 = New System.Windows.Forms.Label()
        Me.InstrumentLabel_13 = New System.Windows.Forms.Label()
        Me.InstrumentLabel_12 = New System.Windows.Forms.Label()
        Me.InstrumentLabel_11 = New System.Windows.Forms.Label()
        Me.InstrumentLabel_10 = New System.Windows.Forms.Label()
        Me.InstrumentLabel_9 = New System.Windows.Forms.Label()
        Me.InstrumentLabel_16 = New System.Windows.Forms.Label()
        Me.InstrumentLabel_8 = New System.Windows.Forms.Label()
        Me.InstrumentLabel_0 = New System.Windows.Forms.Label()
        Me.imgPassFrame = New Microsoft.VisualBasic.Compatibility.VB6.PictureBoxArray(Me.components)
        Me.imgPassFrame_27 = New System.Windows.Forms.PictureBox()
        Me.imgPassFrame_26 = New System.Windows.Forms.PictureBox()
        Me.imgPassFrame_14 = New System.Windows.Forms.PictureBox()
        Me.imgPassFrame_20 = New System.Windows.Forms.PictureBox()
        Me.imgPassFrame_22 = New System.Windows.Forms.PictureBox()
        Me.imgPassFrame_23 = New System.Windows.Forms.PictureBox()
        Me.imgPassFrame_24 = New System.Windows.Forms.PictureBox()
        Me.imgPassFrame_25 = New System.Windows.Forms.PictureBox()
        Me.imgPassFrame_17 = New System.Windows.Forms.PictureBox()
        Me.imgPassFrame_16 = New System.Windows.Forms.PictureBox()
        Me.imgPassFrame_15 = New System.Windows.Forms.PictureBox()
        Me.imgPassFrame_13 = New System.Windows.Forms.PictureBox()
        Me.imgPassFrame_12 = New System.Windows.Forms.PictureBox()
        Me.imgPassFrame_11 = New System.Windows.Forms.PictureBox()
        Me.imgPassFrame_10 = New System.Windows.Forms.PictureBox()
        Me.imgPassFrame_9 = New System.Windows.Forms.PictureBox()
        Me.imgPassFrame_8 = New System.Windows.Forms.PictureBox()
        Me.imgPassFrame_7 = New System.Windows.Forms.PictureBox()
        Me.imgPassFrame_6 = New System.Windows.Forms.PictureBox()
        Me.imgPassFrame_5 = New System.Windows.Forms.PictureBox()
        Me.imgPassFrame_4 = New System.Windows.Forms.PictureBox()
        Me.imgPassFrame_3 = New System.Windows.Forms.PictureBox()
        Me.imgPassFrame_2 = New System.Windows.Forms.PictureBox()
        Me.imgPassFrame_1 = New System.Windows.Forms.PictureBox()
        Me.imgPassFrame_0 = New System.Windows.Forms.PictureBox()
        Me.imgFailFrame = New Microsoft.VisualBasic.Compatibility.VB6.PictureBoxArray(Me.components)
        Me.imgFailFrame_27 = New System.Windows.Forms.PictureBox()
        Me.imgFailFrame_12 = New System.Windows.Forms.PictureBox()
        Me.imgFailFrame_26 = New System.Windows.Forms.PictureBox()
        Me.imgFailFrame_20 = New System.Windows.Forms.PictureBox()
        Me.imgFailFrame_22 = New System.Windows.Forms.PictureBox()
        Me.imgFailFrame_23 = New System.Windows.Forms.PictureBox()
        Me.imgFailFrame_24 = New System.Windows.Forms.PictureBox()
        Me.imgFailFrame_25 = New System.Windows.Forms.PictureBox()
        Me.imgFailFrame_17 = New System.Windows.Forms.PictureBox()
        Me.imgFailFrame_16 = New System.Windows.Forms.PictureBox()
        Me.imgFailFrame_15 = New System.Windows.Forms.PictureBox()
        Me.imgFailFrame_14 = New System.Windows.Forms.PictureBox()
        Me.imgFailFrame_13 = New System.Windows.Forms.PictureBox()
        Me.imgFailFrame_11 = New System.Windows.Forms.PictureBox()
        Me.imgFailFrame_10 = New System.Windows.Forms.PictureBox()
        Me.imgFailFrame_9 = New System.Windows.Forms.PictureBox()
        Me.imgFailFrame_8 = New System.Windows.Forms.PictureBox()
        Me.imgFailFrame_7 = New System.Windows.Forms.PictureBox()
        Me.imgFailFrame_6 = New System.Windows.Forms.PictureBox()
        Me.imgFailFrame_5 = New System.Windows.Forms.PictureBox()
        Me.imgFailFrame_4 = New System.Windows.Forms.PictureBox()
        Me.imgFailFrame_3 = New System.Windows.Forms.PictureBox()
        Me.imgFailFrame_2 = New System.Windows.Forms.PictureBox()
        Me.imgFailFrame_1 = New System.Windows.Forms.PictureBox()
        Me.imgFailFrame_0 = New System.Windows.Forms.PictureBox()
        Me.CloseButton = New System.Windows.Forms.Button()
        Me.Details = New System.Windows.Forms.Button()
        Me.SSPanel1 = New System.Windows.Forms.PictureBox()
        Me.DetailsText = New System.Windows.Forms.RichTextBox()
        Me.sbrUserInformation = New System.Windows.Forms.StatusBar()
        Me.sbrUserInformation_Panel1 = New System.Windows.Forms.StatusBarPanel()
        Me.imlImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.imgLogo = New System.Windows.Forms.PictureBox()
        CType(Me.pctIcon, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pctIcon_26, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pctIcon_27, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pctIcon_17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pctIcon_1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pctIcon_3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pctIcon_6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pctIcon_7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pctIcon_2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pctIcon_5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pctIcon_4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pctIcon_23, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pctIcon_25, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pctIcon_22, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pctIcon_24, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pctIcon_20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pctIcon_15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pctIcon_14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pctIcon_13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pctIcon_12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pctIcon_11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pctIcon_10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pctIcon_9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pctIcon_16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pctIcon_8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pctIcon_0, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.InstrumentLabel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgPassFrame, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgPassFrame_27, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgPassFrame_26, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgPassFrame_14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgPassFrame_20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgPassFrame_22, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgPassFrame_23, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgPassFrame_24, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgPassFrame_25, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgPassFrame_17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgPassFrame_16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgPassFrame_15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgPassFrame_13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgPassFrame_12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgPassFrame_11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgPassFrame_10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgPassFrame_9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgPassFrame_8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgPassFrame_7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgPassFrame_6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgPassFrame_5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgPassFrame_4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgPassFrame_3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgPassFrame_2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgPassFrame_1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgPassFrame_0, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgFailFrame, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgFailFrame_27, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgFailFrame_12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgFailFrame_26, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgFailFrame_20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgFailFrame_22, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgFailFrame_23, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgFailFrame_24, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgFailFrame_25, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgFailFrame_17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgFailFrame_16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgFailFrame_15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgFailFrame_14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgFailFrame_13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgFailFrame_11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgFailFrame_10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgFailFrame_9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgFailFrame_8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgFailFrame_7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgFailFrame_6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgFailFrame_5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgFailFrame_4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgFailFrame_3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgFailFrame_2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgFailFrame_1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgFailFrame_0, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SSPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SSPanel1.SuspendLayout()
        CType(Me.sbrUserInformation_Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pctIcon
        '
        '
        'pctIcon_26
        '
        Me.pctIcon_26.BackColor = System.Drawing.SystemColors.Control
        Me.pctIcon_26.Image = CType(resources.GetObject("pctIcon_26.Image"), System.Drawing.Image)
        Me.pctIcon.SetIndex(Me.pctIcon_26, CType(26, Short))
        Me.pctIcon_26.Location = New System.Drawing.Point(353, 94)
        Me.pctIcon_26.Name = "pctIcon_26"
        Me.pctIcon_26.Size = New System.Drawing.Size(16, 16)
        Me.pctIcon_26.TabIndex = 56
        Me.pctIcon_26.TabStop = False
        '
        'pctIcon_27
        '
        Me.pctIcon_27.BackColor = System.Drawing.SystemColors.Control
        Me.pctIcon_27.Image = CType(resources.GetObject("pctIcon_27.Image"), System.Drawing.Image)
        Me.pctIcon.SetIndex(Me.pctIcon_27, CType(27, Short))
        Me.pctIcon_27.Location = New System.Drawing.Point(353, 111)
        Me.pctIcon_27.Name = "pctIcon_27"
        Me.pctIcon_27.Size = New System.Drawing.Size(16, 16)
        Me.pctIcon_27.TabIndex = 55
        Me.pctIcon_27.TabStop = False
        '
        'pctIcon_17
        '
        Me.pctIcon_17.BackColor = System.Drawing.SystemColors.Control
        Me.pctIcon_17.Image = CType(resources.GetObject("pctIcon_17.Image"), System.Drawing.Image)
        Me.pctIcon.SetIndex(Me.pctIcon_17, CType(17, Short))
        Me.pctIcon_17.Location = New System.Drawing.Point(353, 59)
        Me.pctIcon_17.Name = "pctIcon_17"
        Me.pctIcon_17.Size = New System.Drawing.Size(16, 16)
        Me.pctIcon_17.TabIndex = 53
        Me.pctIcon_17.TabStop = False
        '
        'pctIcon_1
        '
        Me.pctIcon_1.BackColor = System.Drawing.SystemColors.Control
        Me.pctIcon_1.Image = CType(resources.GetObject("pctIcon_1.Image"), System.Drawing.Image)
        Me.pctIcon.SetIndex(Me.pctIcon_1, CType(1, Short))
        Me.pctIcon_1.Location = New System.Drawing.Point(82, 21)
        Me.pctIcon_1.Name = "pctIcon_1"
        Me.pctIcon_1.Size = New System.Drawing.Size(16, 16)
        Me.pctIcon_1.TabIndex = 45
        Me.pctIcon_1.TabStop = False
        '
        'pctIcon_3
        '
        Me.pctIcon_3.BackColor = System.Drawing.SystemColors.Control
        Me.pctIcon_3.Image = CType(resources.GetObject("pctIcon_3.Image"), System.Drawing.Image)
        Me.pctIcon.SetIndex(Me.pctIcon_3, CType(3, Short))
        Me.pctIcon_3.Location = New System.Drawing.Point(82, 56)
        Me.pctIcon_3.Name = "pctIcon_3"
        Me.pctIcon_3.Size = New System.Drawing.Size(16, 16)
        Me.pctIcon_3.TabIndex = 44
        Me.pctIcon_3.TabStop = False
        '
        'pctIcon_6
        '
        Me.pctIcon_6.BackColor = System.Drawing.SystemColors.Control
        Me.pctIcon_6.Image = CType(resources.GetObject("pctIcon_6.Image"), System.Drawing.Image)
        Me.pctIcon.SetIndex(Me.pctIcon_6, CType(6, Short))
        Me.pctIcon_6.Location = New System.Drawing.Point(82, 107)
        Me.pctIcon_6.Name = "pctIcon_6"
        Me.pctIcon_6.Size = New System.Drawing.Size(16, 16)
        Me.pctIcon_6.TabIndex = 43
        Me.pctIcon_6.TabStop = False
        '
        'pctIcon_7
        '
        Me.pctIcon_7.BackColor = System.Drawing.SystemColors.Control
        Me.pctIcon_7.Image = CType(resources.GetObject("pctIcon_7.Image"), System.Drawing.Image)
        Me.pctIcon.SetIndex(Me.pctIcon_7, CType(7, Short))
        Me.pctIcon_7.Location = New System.Drawing.Point(82, 124)
        Me.pctIcon_7.Name = "pctIcon_7"
        Me.pctIcon_7.Size = New System.Drawing.Size(16, 16)
        Me.pctIcon_7.TabIndex = 42
        Me.pctIcon_7.TabStop = False
        '
        'pctIcon_2
        '
        Me.pctIcon_2.BackColor = System.Drawing.SystemColors.Control
        Me.pctIcon_2.Image = CType(resources.GetObject("pctIcon_2.Image"), System.Drawing.Image)
        Me.pctIcon.SetIndex(Me.pctIcon_2, CType(2, Short))
        Me.pctIcon_2.Location = New System.Drawing.Point(82, 38)
        Me.pctIcon_2.Name = "pctIcon_2"
        Me.pctIcon_2.Size = New System.Drawing.Size(16, 16)
        Me.pctIcon_2.TabIndex = 41
        Me.pctIcon_2.TabStop = False
        '
        'pctIcon_5
        '
        Me.pctIcon_5.BackColor = System.Drawing.SystemColors.Control
        Me.pctIcon_5.Image = CType(resources.GetObject("pctIcon_5.Image"), System.Drawing.Image)
        Me.pctIcon.SetIndex(Me.pctIcon_5, CType(5, Short))
        Me.pctIcon_5.Location = New System.Drawing.Point(82, 90)
        Me.pctIcon_5.Name = "pctIcon_5"
        Me.pctIcon_5.Size = New System.Drawing.Size(16, 16)
        Me.pctIcon_5.TabIndex = 40
        Me.pctIcon_5.TabStop = False
        '
        'pctIcon_4
        '
        Me.pctIcon_4.BackColor = System.Drawing.SystemColors.Control
        Me.pctIcon_4.Image = CType(resources.GetObject("pctIcon_4.Image"), System.Drawing.Image)
        Me.pctIcon.SetIndex(Me.pctIcon_4, CType(4, Short))
        Me.pctIcon_4.Location = New System.Drawing.Point(82, 73)
        Me.pctIcon_4.Name = "pctIcon_4"
        Me.pctIcon_4.Size = New System.Drawing.Size(16, 16)
        Me.pctIcon_4.TabIndex = 39
        Me.pctIcon_4.TabStop = False
        '
        'pctIcon_23
        '
        Me.pctIcon_23.BackColor = System.Drawing.SystemColors.Control
        Me.pctIcon_23.Image = CType(resources.GetObject("pctIcon_23.Image"), System.Drawing.Image)
        Me.pctIcon.SetIndex(Me.pctIcon_23, CType(23, Short))
        Me.pctIcon_23.Location = New System.Drawing.Point(353, 143)
        Me.pctIcon_23.Name = "pctIcon_23"
        Me.pctIcon_23.Size = New System.Drawing.Size(16, 16)
        Me.pctIcon_23.TabIndex = 38
        Me.pctIcon_23.TabStop = False
        '
        'pctIcon_25
        '
        Me.pctIcon_25.BackColor = System.Drawing.SystemColors.Control
        Me.pctIcon_25.Image = CType(resources.GetObject("pctIcon_25.Image"), System.Drawing.Image)
        Me.pctIcon.SetIndex(Me.pctIcon_25, CType(25, Short))
        Me.pctIcon_25.Location = New System.Drawing.Point(353, 177)
        Me.pctIcon_25.Name = "pctIcon_25"
        Me.pctIcon_25.Size = New System.Drawing.Size(16, 16)
        Me.pctIcon_25.TabIndex = 31
        Me.pctIcon_25.TabStop = False
        '
        'pctIcon_22
        '
        Me.pctIcon_22.BackColor = System.Drawing.SystemColors.Control
        Me.pctIcon_22.Image = CType(resources.GetObject("pctIcon_22.Image"), System.Drawing.Image)
        Me.pctIcon.SetIndex(Me.pctIcon_22, CType(22, Short))
        Me.pctIcon_22.Location = New System.Drawing.Point(353, 126)
        Me.pctIcon_22.Name = "pctIcon_22"
        Me.pctIcon_22.Size = New System.Drawing.Size(16, 16)
        Me.pctIcon_22.TabIndex = 30
        Me.pctIcon_22.TabStop = False
        '
        'pctIcon_24
        '
        Me.pctIcon_24.BackColor = System.Drawing.SystemColors.Control
        Me.pctIcon_24.Image = CType(resources.GetObject("pctIcon_24.Image"), System.Drawing.Image)
        Me.pctIcon.SetIndex(Me.pctIcon_24, CType(24, Short))
        Me.pctIcon_24.Location = New System.Drawing.Point(353, 160)
        Me.pctIcon_24.Name = "pctIcon_24"
        Me.pctIcon_24.Size = New System.Drawing.Size(16, 16)
        Me.pctIcon_24.TabIndex = 29
        Me.pctIcon_24.TabStop = False
        '
        'pctIcon_20
        '
        Me.pctIcon_20.BackColor = System.Drawing.SystemColors.Control
        Me.pctIcon_20.Image = CType(resources.GetObject("pctIcon_20.Image"), System.Drawing.Image)
        Me.pctIcon.SetIndex(Me.pctIcon_20, CType(20, Short))
        Me.pctIcon_20.Location = New System.Drawing.Point(353, 76)
        Me.pctIcon_20.Name = "pctIcon_20"
        Me.pctIcon_20.Size = New System.Drawing.Size(16, 16)
        Me.pctIcon_20.TabIndex = 27
        Me.pctIcon_20.TabStop = False
        '
        'pctIcon_15
        '
        Me.pctIcon_15.BackColor = System.Drawing.SystemColors.Control
        Me.pctIcon_15.Image = CType(resources.GetObject("pctIcon_15.Image"), System.Drawing.Image)
        Me.pctIcon.SetIndex(Me.pctIcon_15, CType(15, Short))
        Me.pctIcon_15.Location = New System.Drawing.Point(353, 24)
        Me.pctIcon_15.Name = "pctIcon_15"
        Me.pctIcon_15.Size = New System.Drawing.Size(16, 16)
        Me.pctIcon_15.TabIndex = 21
        Me.pctIcon_15.TabStop = False
        '
        'pctIcon_14
        '
        Me.pctIcon_14.BackColor = System.Drawing.SystemColors.Control
        Me.pctIcon_14.Image = CType(resources.GetObject("pctIcon_14.Image"), System.Drawing.Image)
        Me.pctIcon.SetIndex(Me.pctIcon_14, CType(14, Short))
        Me.pctIcon_14.Location = New System.Drawing.Point(353, 5)
        Me.pctIcon_14.Name = "pctIcon_14"
        Me.pctIcon_14.Size = New System.Drawing.Size(16, 16)
        Me.pctIcon_14.TabIndex = 20
        Me.pctIcon_14.TabStop = False
        '
        'pctIcon_13
        '
        Me.pctIcon_13.BackColor = System.Drawing.SystemColors.Control
        Me.pctIcon_13.Image = CType(resources.GetObject("pctIcon_13.Image"), System.Drawing.Image)
        Me.pctIcon.SetIndex(Me.pctIcon_13, CType(13, Short))
        Me.pctIcon_13.Location = New System.Drawing.Point(84, 228)
        Me.pctIcon_13.Name = "pctIcon_13"
        Me.pctIcon_13.Size = New System.Drawing.Size(17, 17)
        Me.pctIcon_13.TabIndex = 19
        Me.pctIcon_13.TabStop = False
        '
        'pctIcon_12
        '
        Me.pctIcon_12.BackColor = System.Drawing.SystemColors.Control
        Me.pctIcon_12.Image = CType(resources.GetObject("pctIcon_12.Image"), System.Drawing.Image)
        Me.pctIcon.SetIndex(Me.pctIcon_12, CType(12, Short))
        Me.pctIcon_12.Location = New System.Drawing.Point(84, 211)
        Me.pctIcon_12.Name = "pctIcon_12"
        Me.pctIcon_12.Size = New System.Drawing.Size(16, 16)
        Me.pctIcon_12.TabIndex = 18
        Me.pctIcon_12.TabStop = False
        '
        'pctIcon_11
        '
        Me.pctIcon_11.BackColor = System.Drawing.SystemColors.Control
        Me.pctIcon_11.Image = CType(resources.GetObject("pctIcon_11.Image"), System.Drawing.Image)
        Me.pctIcon.SetIndex(Me.pctIcon_11, CType(11, Short))
        Me.pctIcon_11.Location = New System.Drawing.Point(82, 193)
        Me.pctIcon_11.Name = "pctIcon_11"
        Me.pctIcon_11.Size = New System.Drawing.Size(16, 16)
        Me.pctIcon_11.TabIndex = 17
        Me.pctIcon_11.TabStop = False
        '
        'pctIcon_10
        '
        Me.pctIcon_10.BackColor = System.Drawing.SystemColors.Control
        Me.pctIcon_10.Image = CType(resources.GetObject("pctIcon_10.Image"), System.Drawing.Image)
        Me.pctIcon.SetIndex(Me.pctIcon_10, CType(10, Short))
        Me.pctIcon_10.Location = New System.Drawing.Point(82, 176)
        Me.pctIcon_10.Name = "pctIcon_10"
        Me.pctIcon_10.Size = New System.Drawing.Size(16, 16)
        Me.pctIcon_10.TabIndex = 16
        Me.pctIcon_10.TabStop = False
        '
        'pctIcon_9
        '
        Me.pctIcon_9.BackColor = System.Drawing.SystemColors.Control
        Me.pctIcon_9.Image = CType(resources.GetObject("pctIcon_9.Image"), System.Drawing.Image)
        Me.pctIcon.SetIndex(Me.pctIcon_9, CType(9, Short))
        Me.pctIcon_9.Location = New System.Drawing.Point(82, 159)
        Me.pctIcon_9.Name = "pctIcon_9"
        Me.pctIcon_9.Size = New System.Drawing.Size(16, 16)
        Me.pctIcon_9.TabIndex = 15
        Me.pctIcon_9.TabStop = False
        '
        'pctIcon_16
        '
        Me.pctIcon_16.BackColor = System.Drawing.SystemColors.Control
        Me.pctIcon_16.Image = CType(resources.GetObject("pctIcon_16.Image"), System.Drawing.Image)
        Me.pctIcon.SetIndex(Me.pctIcon_16, CType(16, Short))
        Me.pctIcon_16.Location = New System.Drawing.Point(353, 41)
        Me.pctIcon_16.Name = "pctIcon_16"
        Me.pctIcon_16.Size = New System.Drawing.Size(16, 16)
        Me.pctIcon_16.TabIndex = 14
        Me.pctIcon_16.TabStop = False
        '
        'pctIcon_8
        '
        Me.pctIcon_8.BackColor = System.Drawing.SystemColors.Control
        Me.pctIcon_8.Image = CType(resources.GetObject("pctIcon_8.Image"), System.Drawing.Image)
        Me.pctIcon.SetIndex(Me.pctIcon_8, CType(8, Short))
        Me.pctIcon_8.Location = New System.Drawing.Point(82, 142)
        Me.pctIcon_8.Name = "pctIcon_8"
        Me.pctIcon_8.Size = New System.Drawing.Size(16, 16)
        Me.pctIcon_8.TabIndex = 13
        Me.pctIcon_8.TabStop = False
        '
        'pctIcon_0
        '
        Me.pctIcon_0.BackColor = System.Drawing.SystemColors.Control
        Me.pctIcon_0.Image = CType(resources.GetObject("pctIcon_0.Image"), System.Drawing.Image)
        Me.pctIcon.SetIndex(Me.pctIcon_0, CType(0, Short))
        Me.pctIcon_0.Location = New System.Drawing.Point(82, 4)
        Me.pctIcon_0.Name = "pctIcon_0"
        Me.pctIcon_0.Size = New System.Drawing.Size(16, 16)
        Me.pctIcon_0.TabIndex = 12
        Me.pctIcon_0.TabStop = False
        '
        'InstrumentLabel_26
        '
        Me.InstrumentLabel_26.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.InstrumentLabel_26.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InstrumentLabel_26.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.InstrumentLabel.SetIndex(Me.InstrumentLabel_26, CType(26, Short))
        Me.InstrumentLabel_26.Location = New System.Drawing.Point(380, 94)
        Me.InstrumentLabel_26.Name = "InstrumentLabel_26"
        Me.InstrumentLabel_26.Size = New System.Drawing.Size(167, 17)
        Me.InstrumentLabel_26.TabIndex = 58
        Me.InstrumentLabel_26.Text = "Video Capture"
        '
        'InstrumentLabel_27
        '
        Me.InstrumentLabel_27.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.InstrumentLabel_27.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InstrumentLabel_27.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.InstrumentLabel.SetIndex(Me.InstrumentLabel_27, CType(27, Short))
        Me.InstrumentLabel_27.Location = New System.Drawing.Point(380, 111)
        Me.InstrumentLabel_27.Name = "InstrumentLabel_27"
        Me.InstrumentLabel_27.Size = New System.Drawing.Size(167, 17)
        Me.InstrumentLabel_27.TabIndex = 57
        Me.InstrumentLabel_27.Text = "EO Module"
        '
        'InstrumentLabel_17
        '
        Me.InstrumentLabel_17.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.InstrumentLabel_17.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InstrumentLabel_17.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.InstrumentLabel.SetIndex(Me.InstrumentLabel_17, CType(17, Short))
        Me.InstrumentLabel_17.Location = New System.Drawing.Point(380, 59)
        Me.InstrumentLabel_17.Name = "InstrumentLabel_17"
        Me.InstrumentLabel_17.Size = New System.Drawing.Size(167, 17)
        Me.InstrumentLabel_17.TabIndex = 54
        Me.InstrumentLabel_17.Text = "Gigabit Ethernet 1/2 "
        '
        'InstrumentLabel_1
        '
        Me.InstrumentLabel_1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.InstrumentLabel_1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InstrumentLabel_1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.InstrumentLabel.SetIndex(Me.InstrumentLabel_1, CType(1, Short))
        Me.InstrumentLabel_1.Location = New System.Drawing.Point(109, 21)
        Me.InstrumentLabel_1.Name = "InstrumentLabel_1"
        Me.InstrumentLabel_1.Size = New System.Drawing.Size(167, 17)
        Me.InstrumentLabel_1.TabIndex = 52
        Me.InstrumentLabel_1.Text = "Programmable Power Unit"
        '
        'InstrumentLabel_3
        '
        Me.InstrumentLabel_3.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.InstrumentLabel_3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InstrumentLabel_3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.InstrumentLabel.SetIndex(Me.InstrumentLabel_3, CType(3, Short))
        Me.InstrumentLabel_3.Location = New System.Drawing.Point(109, 56)
        Me.InstrumentLabel_3.Name = "InstrumentLabel_3"
        Me.InstrumentLabel_3.Size = New System.Drawing.Size(167, 17)
        Me.InstrumentLabel_3.TabIndex = 51
        Me.InstrumentLabel_3.Text = "Function Generator"
        '
        'InstrumentLabel_2
        '
        Me.InstrumentLabel_2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.InstrumentLabel_2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InstrumentLabel_2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.InstrumentLabel.SetIndex(Me.InstrumentLabel_2, CType(2, Short))
        Me.InstrumentLabel_2.Location = New System.Drawing.Point(109, 38)
        Me.InstrumentLabel_2.Name = "InstrumentLabel_2"
        Me.InstrumentLabel_2.Size = New System.Drawing.Size(167, 17)
        Me.InstrumentLabel_2.TabIndex = 50
        Me.InstrumentLabel_2.Text = "Digital Test Subsystem (DTS)"
        '
        'InstrumentLabel_6
        '
        Me.InstrumentLabel_6.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.InstrumentLabel_6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InstrumentLabel_6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.InstrumentLabel.SetIndex(Me.InstrumentLabel_6, CType(6, Short))
        Me.InstrumentLabel_6.Location = New System.Drawing.Point(109, 107)
        Me.InstrumentLabel_6.Name = "InstrumentLabel_6"
        Me.InstrumentLabel_6.Size = New System.Drawing.Size(167, 17)
        Me.InstrumentLabel_6.TabIndex = 49
        Me.InstrumentLabel_6.Text = "Digital Multimeter"
        '
        'InstrumentLabel_5
        '
        Me.InstrumentLabel_5.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.InstrumentLabel_5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InstrumentLabel_5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.InstrumentLabel.SetIndex(Me.InstrumentLabel_5, CType(5, Short))
        Me.InstrumentLabel_5.Location = New System.Drawing.Point(109, 90)
        Me.InstrumentLabel_5.Name = "InstrumentLabel_5"
        Me.InstrumentLabel_5.Size = New System.Drawing.Size(167, 17)
        Me.InstrumentLabel_5.TabIndex = 48
        Me.InstrumentLabel_5.Text = "Arbitrary Waveform Generator"
        '
        'InstrumentLabel_4
        '
        Me.InstrumentLabel_4.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.InstrumentLabel_4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InstrumentLabel_4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.InstrumentLabel.SetIndex(Me.InstrumentLabel_4, CType(4, Short))
        Me.InstrumentLabel_4.Location = New System.Drawing.Point(109, 73)
        Me.InstrumentLabel_4.Name = "InstrumentLabel_4"
        Me.InstrumentLabel_4.Size = New System.Drawing.Size(167, 17)
        Me.InstrumentLabel_4.TabIndex = 47
        Me.InstrumentLabel_4.Text = "Counter/Timer"
        '
        'InstrumentLabel_7
        '
        Me.InstrumentLabel_7.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.InstrumentLabel_7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InstrumentLabel_7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.InstrumentLabel.SetIndex(Me.InstrumentLabel_7, CType(7, Short))
        Me.InstrumentLabel_7.Location = New System.Drawing.Point(109, 124)
        Me.InstrumentLabel_7.Name = "InstrumentLabel_7"
        Me.InstrumentLabel_7.Size = New System.Drawing.Size(167, 17)
        Me.InstrumentLabel_7.TabIndex = 46
        Me.InstrumentLabel_7.Text = "Digitizing Oscilloscope"
        '
        'InstrumentLabel_25
        '
        Me.InstrumentLabel_25.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.InstrumentLabel_25.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InstrumentLabel_25.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.InstrumentLabel.SetIndex(Me.InstrumentLabel_25, CType(25, Short))
        Me.InstrumentLabel_25.Location = New System.Drawing.Point(380, 177)
        Me.InstrumentLabel_25.Name = "InstrumentLabel_25"
        Me.InstrumentLabel_25.Size = New System.Drawing.Size(167, 17)
        Me.InstrumentLabel_25.TabIndex = 37
        Me.InstrumentLabel_25.Text = "Digitizer and Reference"
        '
        'InstrumentLabel_22
        '
        Me.InstrumentLabel_22.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.InstrumentLabel_22.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InstrumentLabel_22.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.InstrumentLabel.SetIndex(Me.InstrumentLabel_22, CType(22, Short))
        Me.InstrumentLabel_22.Location = New System.Drawing.Point(380, 126)
        Me.InstrumentLabel_22.Name = "InstrumentLabel_22"
        Me.InstrumentLabel_22.Size = New System.Drawing.Size(167, 17)
        Me.InstrumentLabel_22.TabIndex = 36
        Me.InstrumentLabel_22.Text = "RF Down Converter/Pwr Meter"
        '
        'InstrumentLabel_23
        '
        Me.InstrumentLabel_23.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.InstrumentLabel_23.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InstrumentLabel_23.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.InstrumentLabel.SetIndex(Me.InstrumentLabel_23, CType(23, Short))
        Me.InstrumentLabel_23.Location = New System.Drawing.Point(380, 143)
        Me.InstrumentLabel_23.Name = "InstrumentLabel_23"
        Me.InstrumentLabel_23.Size = New System.Drawing.Size(167, 17)
        Me.InstrumentLabel_23.TabIndex = 35
        Me.InstrumentLabel_23.Text = "RF Stimulus"
        '
        'InstrumentLabel_24
        '
        Me.InstrumentLabel_24.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.InstrumentLabel_24.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InstrumentLabel_24.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.InstrumentLabel.SetIndex(Me.InstrumentLabel_24, CType(24, Short))
        Me.InstrumentLabel_24.Location = New System.Drawing.Point(380, 160)
        Me.InstrumentLabel_24.Name = "InstrumentLabel_24"
        Me.InstrumentLabel_24.Size = New System.Drawing.Size(167, 17)
        Me.InstrumentLabel_24.TabIndex = 34
        Me.InstrumentLabel_24.Text = "Synthesized Signal Source/ LO"
        '
        'InstrumentLabel_20
        '
        Me.InstrumentLabel_20.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.InstrumentLabel_20.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InstrumentLabel_20.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.InstrumentLabel.SetIndex(Me.InstrumentLabel_20, CType(20, Short))
        Me.InstrumentLabel_20.Location = New System.Drawing.Point(380, 76)
        Me.InstrumentLabel_20.Name = "InstrumentLabel_20"
        Me.InstrumentLabel_20.Size = New System.Drawing.Size(167, 17)
        Me.InstrumentLabel_20.TabIndex = 32
        Me.InstrumentLabel_20.Text = "CAN"
        '
        'InstrumentLabel_15
        '
        Me.InstrumentLabel_15.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.InstrumentLabel_15.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InstrumentLabel_15.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.InstrumentLabel.SetIndex(Me.InstrumentLabel_15, CType(15, Short))
        Me.InstrumentLabel_15.Location = New System.Drawing.Point(380, 24)
        Me.InstrumentLabel_15.Name = "InstrumentLabel_15"
        Me.InstrumentLabel_15.Size = New System.Drawing.Size(167, 17)
        Me.InstrumentLabel_15.TabIndex = 11
        Me.InstrumentLabel_15.Text = "COM 1 - RS422"
        '
        'InstrumentLabel_14
        '
        Me.InstrumentLabel_14.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.InstrumentLabel_14.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InstrumentLabel_14.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.InstrumentLabel.SetIndex(Me.InstrumentLabel_14, CType(14, Short))
        Me.InstrumentLabel_14.Location = New System.Drawing.Point(380, 5)
        Me.InstrumentLabel_14.Name = "InstrumentLabel_14"
        Me.InstrumentLabel_14.Size = New System.Drawing.Size(167, 17)
        Me.InstrumentLabel_14.TabIndex = 10
        Me.InstrumentLabel_14.Text = "Programmable Serial"
        '
        'InstrumentLabel_13
        '
        Me.InstrumentLabel_13.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.InstrumentLabel_13.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InstrumentLabel_13.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.InstrumentLabel.SetIndex(Me.InstrumentLabel_13, CType(13, Short))
        Me.InstrumentLabel_13.Location = New System.Drawing.Point(111, 228)
        Me.InstrumentLabel_13.Name = "InstrumentLabel_13"
        Me.InstrumentLabel_13.Size = New System.Drawing.Size(165, 17)
        Me.InstrumentLabel_13.TabIndex = 9
        Me.InstrumentLabel_13.Text = "Synchro/Resolver"
        '
        'InstrumentLabel_12
        '
        Me.InstrumentLabel_12.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.InstrumentLabel_12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InstrumentLabel_12.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.InstrumentLabel.SetIndex(Me.InstrumentLabel_12, CType(12, Short))
        Me.InstrumentLabel_12.Location = New System.Drawing.Point(111, 211)
        Me.InstrumentLabel_12.Name = "InstrumentLabel_12"
        Me.InstrumentLabel_12.Size = New System.Drawing.Size(167, 17)
        Me.InstrumentLabel_12.TabIndex = 8
        Me.InstrumentLabel_12.Text = "High Frequency Switches"
        '
        'InstrumentLabel_11
        '
        Me.InstrumentLabel_11.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.InstrumentLabel_11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InstrumentLabel_11.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.InstrumentLabel.SetIndex(Me.InstrumentLabel_11, CType(11, Short))
        Me.InstrumentLabel_11.Location = New System.Drawing.Point(109, 193)
        Me.InstrumentLabel_11.Name = "InstrumentLabel_11"
        Me.InstrumentLabel_11.Size = New System.Drawing.Size(167, 17)
        Me.InstrumentLabel_11.TabIndex = 7
        Me.InstrumentLabel_11.Text = "Medium Frequency Switches"
        '
        'InstrumentLabel_10
        '
        Me.InstrumentLabel_10.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.InstrumentLabel_10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InstrumentLabel_10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.InstrumentLabel.SetIndex(Me.InstrumentLabel_10, CType(10, Short))
        Me.InstrumentLabel_10.Location = New System.Drawing.Point(109, 176)
        Me.InstrumentLabel_10.Name = "InstrumentLabel_10"
        Me.InstrumentLabel_10.Size = New System.Drawing.Size(167, 17)
        Me.InstrumentLabel_10.TabIndex = 6
        Me.InstrumentLabel_10.Text = "Low Frequency Switches #3"
        '
        'InstrumentLabel_9
        '
        Me.InstrumentLabel_9.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.InstrumentLabel_9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InstrumentLabel_9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.InstrumentLabel.SetIndex(Me.InstrumentLabel_9, CType(9, Short))
        Me.InstrumentLabel_9.Location = New System.Drawing.Point(109, 159)
        Me.InstrumentLabel_9.Name = "InstrumentLabel_9"
        Me.InstrumentLabel_9.Size = New System.Drawing.Size(167, 17)
        Me.InstrumentLabel_9.TabIndex = 5
        Me.InstrumentLabel_9.Text = "Low Frequency Switches #2"
        '
        'InstrumentLabel_16
        '
        Me.InstrumentLabel_16.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.InstrumentLabel_16.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InstrumentLabel_16.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.InstrumentLabel.SetIndex(Me.InstrumentLabel_16, CType(16, Short))
        Me.InstrumentLabel_16.Location = New System.Drawing.Point(380, 41)
        Me.InstrumentLabel_16.Name = "InstrumentLabel_16"
        Me.InstrumentLabel_16.Size = New System.Drawing.Size(167, 17)
        Me.InstrumentLabel_16.TabIndex = 4
        Me.InstrumentLabel_16.Text = "COM 2 - RS232"
        '
        'InstrumentLabel_8
        '
        Me.InstrumentLabel_8.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.InstrumentLabel_8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InstrumentLabel_8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.InstrumentLabel.SetIndex(Me.InstrumentLabel_8, CType(8, Short))
        Me.InstrumentLabel_8.Location = New System.Drawing.Point(109, 142)
        Me.InstrumentLabel_8.Name = "InstrumentLabel_8"
        Me.InstrumentLabel_8.Size = New System.Drawing.Size(167, 17)
        Me.InstrumentLabel_8.TabIndex = 3
        Me.InstrumentLabel_8.Text = "Low Frequency Switches #1"
        '
        'InstrumentLabel_0
        '
        Me.InstrumentLabel_0.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.InstrumentLabel_0.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InstrumentLabel_0.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.InstrumentLabel.SetIndex(Me.InstrumentLabel_0, CType(0, Short))
        Me.InstrumentLabel_0.Location = New System.Drawing.Point(109, 4)
        Me.InstrumentLabel_0.Name = "InstrumentLabel_0"
        Me.InstrumentLabel_0.Size = New System.Drawing.Size(171, 17)
        Me.InstrumentLabel_0.TabIndex = 2
        Me.InstrumentLabel_0.Text = "MIL-STD-1553 Bus"
        '
        'imgPassFrame_27
        '
        Me.imgPassFrame_27.BackColor = System.Drawing.SystemColors.Control
        Me.imgPassFrame_27.Image = CType(resources.GetObject("imgPassFrame_27.Image"), System.Drawing.Image)
        Me.imgPassFrame.SetIndex(Me.imgPassFrame_27, CType(27, Short))
        Me.imgPassFrame_27.Location = New System.Drawing.Point(279, 107)
        Me.imgPassFrame_27.Name = "imgPassFrame_27"
        Me.imgPassFrame_27.Size = New System.Drawing.Size(32, 16)
        Me.imgPassFrame_27.TabIndex = 59
        Me.imgPassFrame_27.TabStop = False
        '
        'imgPassFrame_26
        '
        Me.imgPassFrame_26.BackColor = System.Drawing.SystemColors.Control
        Me.imgPassFrame_26.Image = CType(resources.GetObject("imgPassFrame_26.Image"), System.Drawing.Image)
        Me.imgPassFrame.SetIndex(Me.imgPassFrame_26, CType(26, Short))
        Me.imgPassFrame_26.Location = New System.Drawing.Point(279, 90)
        Me.imgPassFrame_26.Name = "imgPassFrame_26"
        Me.imgPassFrame_26.Size = New System.Drawing.Size(32, 16)
        Me.imgPassFrame_26.TabIndex = 62
        Me.imgPassFrame_26.TabStop = False
        '
        'imgPassFrame_14
        '
        Me.imgPassFrame_14.BackColor = System.Drawing.SystemColors.Control
        Me.imgPassFrame_14.Image = CType(resources.GetObject("imgPassFrame_14.Image"), System.Drawing.Image)
        Me.imgPassFrame.SetIndex(Me.imgPassFrame_14, CType(14, Short))
        Me.imgPassFrame_14.Location = New System.Drawing.Point(279, 5)
        Me.imgPassFrame_14.Name = "imgPassFrame_14"
        Me.imgPassFrame_14.Size = New System.Drawing.Size(32, 16)
        Me.imgPassFrame_14.TabIndex = 63
        Me.imgPassFrame_14.TabStop = False
        '
        'imgPassFrame_20
        '
        Me.imgPassFrame_20.BackColor = System.Drawing.SystemColors.Control
        Me.imgPassFrame_20.Image = CType(resources.GetObject("imgPassFrame_20.Image"), System.Drawing.Image)
        Me.imgPassFrame.SetIndex(Me.imgPassFrame_20, CType(20, Short))
        Me.imgPassFrame_20.Location = New System.Drawing.Point(279, 73)
        Me.imgPassFrame_20.Name = "imgPassFrame_20"
        Me.imgPassFrame_20.Size = New System.Drawing.Size(32, 16)
        Me.imgPassFrame_20.TabIndex = 65
        Me.imgPassFrame_20.TabStop = False
        '
        'imgPassFrame_22
        '
        Me.imgPassFrame_22.BackColor = System.Drawing.SystemColors.Control
        Me.imgPassFrame_22.Image = CType(resources.GetObject("imgPassFrame_22.Image"), System.Drawing.Image)
        Me.imgPassFrame.SetIndex(Me.imgPassFrame_22, CType(22, Short))
        Me.imgPassFrame_22.Location = New System.Drawing.Point(279, 124)
        Me.imgPassFrame_22.Name = "imgPassFrame_22"
        Me.imgPassFrame_22.Size = New System.Drawing.Size(32, 16)
        Me.imgPassFrame_22.TabIndex = 67
        Me.imgPassFrame_22.TabStop = False
        '
        'imgPassFrame_23
        '
        Me.imgPassFrame_23.BackColor = System.Drawing.SystemColors.Control
        Me.imgPassFrame_23.Image = CType(resources.GetObject("imgPassFrame_23.Image"), System.Drawing.Image)
        Me.imgPassFrame.SetIndex(Me.imgPassFrame_23, CType(23, Short))
        Me.imgPassFrame_23.Location = New System.Drawing.Point(279, 141)
        Me.imgPassFrame_23.Name = "imgPassFrame_23"
        Me.imgPassFrame_23.Size = New System.Drawing.Size(32, 16)
        Me.imgPassFrame_23.TabIndex = 68
        Me.imgPassFrame_23.TabStop = False
        '
        'imgPassFrame_24
        '
        Me.imgPassFrame_24.BackColor = System.Drawing.SystemColors.Control
        Me.imgPassFrame_24.Image = CType(resources.GetObject("imgPassFrame_24.Image"), System.Drawing.Image)
        Me.imgPassFrame.SetIndex(Me.imgPassFrame_24, CType(24, Short))
        Me.imgPassFrame_24.Location = New System.Drawing.Point(279, 158)
        Me.imgPassFrame_24.Name = "imgPassFrame_24"
        Me.imgPassFrame_24.Size = New System.Drawing.Size(32, 16)
        Me.imgPassFrame_24.TabIndex = 69
        Me.imgPassFrame_24.TabStop = False
        '
        'imgPassFrame_25
        '
        Me.imgPassFrame_25.BackColor = System.Drawing.SystemColors.Control
        Me.imgPassFrame_25.Image = CType(resources.GetObject("imgPassFrame_25.Image"), System.Drawing.Image)
        Me.imgPassFrame.SetIndex(Me.imgPassFrame_25, CType(25, Short))
        Me.imgPassFrame_25.Location = New System.Drawing.Point(279, 175)
        Me.imgPassFrame_25.Name = "imgPassFrame_25"
        Me.imgPassFrame_25.Size = New System.Drawing.Size(32, 16)
        Me.imgPassFrame_25.TabIndex = 70
        Me.imgPassFrame_25.TabStop = False
        '
        'imgPassFrame_17
        '
        Me.imgPassFrame_17.BackColor = System.Drawing.SystemColors.Control
        Me.imgPassFrame_17.Image = CType(resources.GetObject("imgPassFrame_17.Image"), System.Drawing.Image)
        Me.imgPassFrame.SetIndex(Me.imgPassFrame_17, CType(17, Short))
        Me.imgPassFrame_17.Location = New System.Drawing.Point(279, 56)
        Me.imgPassFrame_17.Name = "imgPassFrame_17"
        Me.imgPassFrame_17.Size = New System.Drawing.Size(32, 16)
        Me.imgPassFrame_17.TabIndex = 98
        Me.imgPassFrame_17.TabStop = False
        '
        'imgPassFrame_16
        '
        Me.imgPassFrame_16.BackColor = System.Drawing.SystemColors.Control
        Me.imgPassFrame_16.Image = CType(resources.GetObject("imgPassFrame_16.Image"), System.Drawing.Image)
        Me.imgPassFrame.SetIndex(Me.imgPassFrame_16, CType(16, Short))
        Me.imgPassFrame_16.Location = New System.Drawing.Point(279, 39)
        Me.imgPassFrame_16.Name = "imgPassFrame_16"
        Me.imgPassFrame_16.Size = New System.Drawing.Size(32, 16)
        Me.imgPassFrame_16.TabIndex = 99
        Me.imgPassFrame_16.TabStop = False
        '
        'imgPassFrame_15
        '
        Me.imgPassFrame_15.BackColor = System.Drawing.SystemColors.Control
        Me.imgPassFrame_15.Image = CType(resources.GetObject("imgPassFrame_15.Image"), System.Drawing.Image)
        Me.imgPassFrame.SetIndex(Me.imgPassFrame_15, CType(15, Short))
        Me.imgPassFrame_15.Location = New System.Drawing.Point(279, 22)
        Me.imgPassFrame_15.Name = "imgPassFrame_15"
        Me.imgPassFrame_15.Size = New System.Drawing.Size(32, 16)
        Me.imgPassFrame_15.TabIndex = 100
        Me.imgPassFrame_15.TabStop = False
        '
        'imgPassFrame_13
        '
        Me.imgPassFrame_13.BackColor = System.Drawing.SystemColors.Control
        Me.imgPassFrame_13.Image = CType(resources.GetObject("imgPassFrame_13.Image"), System.Drawing.Image)
        Me.imgPassFrame.SetIndex(Me.imgPassFrame_13, CType(13, Short))
        Me.imgPassFrame_13.Location = New System.Drawing.Point(10, 226)
        Me.imgPassFrame_13.Name = "imgPassFrame_13"
        Me.imgPassFrame_13.Size = New System.Drawing.Size(32, 16)
        Me.imgPassFrame_13.TabIndex = 101
        Me.imgPassFrame_13.TabStop = False
        '
        'imgPassFrame_12
        '
        Me.imgPassFrame_12.BackColor = System.Drawing.SystemColors.Control
        Me.imgPassFrame_12.Image = CType(resources.GetObject("imgPassFrame_12.Image"), System.Drawing.Image)
        Me.imgPassFrame.SetIndex(Me.imgPassFrame_12, CType(12, Short))
        Me.imgPassFrame_12.Location = New System.Drawing.Point(10, 209)
        Me.imgPassFrame_12.Name = "imgPassFrame_12"
        Me.imgPassFrame_12.Size = New System.Drawing.Size(32, 16)
        Me.imgPassFrame_12.TabIndex = 102
        Me.imgPassFrame_12.TabStop = False
        '
        'imgPassFrame_11
        '
        Me.imgPassFrame_11.BackColor = System.Drawing.SystemColors.Control
        Me.imgPassFrame_11.Image = CType(resources.GetObject("imgPassFrame_11.Image"), System.Drawing.Image)
        Me.imgPassFrame.SetIndex(Me.imgPassFrame_11, CType(11, Short))
        Me.imgPassFrame_11.Location = New System.Drawing.Point(8, 191)
        Me.imgPassFrame_11.Name = "imgPassFrame_11"
        Me.imgPassFrame_11.Size = New System.Drawing.Size(32, 16)
        Me.imgPassFrame_11.TabIndex = 103
        Me.imgPassFrame_11.TabStop = False
        '
        'imgPassFrame_10
        '
        Me.imgPassFrame_10.BackColor = System.Drawing.SystemColors.Control
        Me.imgPassFrame_10.Image = CType(resources.GetObject("imgPassFrame_10.Image"), System.Drawing.Image)
        Me.imgPassFrame.SetIndex(Me.imgPassFrame_10, CType(10, Short))
        Me.imgPassFrame_10.Location = New System.Drawing.Point(8, 174)
        Me.imgPassFrame_10.Name = "imgPassFrame_10"
        Me.imgPassFrame_10.Size = New System.Drawing.Size(32, 16)
        Me.imgPassFrame_10.TabIndex = 104
        Me.imgPassFrame_10.TabStop = False
        '
        'imgPassFrame_9
        '
        Me.imgPassFrame_9.BackColor = System.Drawing.SystemColors.Control
        Me.imgPassFrame_9.Image = CType(resources.GetObject("imgPassFrame_9.Image"), System.Drawing.Image)
        Me.imgPassFrame.SetIndex(Me.imgPassFrame_9, CType(9, Short))
        Me.imgPassFrame_9.Location = New System.Drawing.Point(8, 157)
        Me.imgPassFrame_9.Name = "imgPassFrame_9"
        Me.imgPassFrame_9.Size = New System.Drawing.Size(32, 16)
        Me.imgPassFrame_9.TabIndex = 105
        Me.imgPassFrame_9.TabStop = False
        '
        'imgPassFrame_8
        '
        Me.imgPassFrame_8.BackColor = System.Drawing.SystemColors.Control
        Me.imgPassFrame_8.Image = CType(resources.GetObject("imgPassFrame_8.Image"), System.Drawing.Image)
        Me.imgPassFrame.SetIndex(Me.imgPassFrame_8, CType(8, Short))
        Me.imgPassFrame_8.Location = New System.Drawing.Point(8, 140)
        Me.imgPassFrame_8.Name = "imgPassFrame_8"
        Me.imgPassFrame_8.Size = New System.Drawing.Size(32, 16)
        Me.imgPassFrame_8.TabIndex = 106
        Me.imgPassFrame_8.TabStop = False
        '
        'imgPassFrame_7
        '
        Me.imgPassFrame_7.BackColor = System.Drawing.SystemColors.Control
        Me.imgPassFrame_7.Image = CType(resources.GetObject("imgPassFrame_7.Image"), System.Drawing.Image)
        Me.imgPassFrame.SetIndex(Me.imgPassFrame_7, CType(7, Short))
        Me.imgPassFrame_7.Location = New System.Drawing.Point(8, 123)
        Me.imgPassFrame_7.Name = "imgPassFrame_7"
        Me.imgPassFrame_7.Size = New System.Drawing.Size(32, 16)
        Me.imgPassFrame_7.TabIndex = 107
        Me.imgPassFrame_7.TabStop = False
        '
        'imgPassFrame_6
        '
        Me.imgPassFrame_6.BackColor = System.Drawing.SystemColors.Control
        Me.imgPassFrame_6.Image = CType(resources.GetObject("imgPassFrame_6.Image"), System.Drawing.Image)
        Me.imgPassFrame.SetIndex(Me.imgPassFrame_6, CType(6, Short))
        Me.imgPassFrame_6.Location = New System.Drawing.Point(8, 106)
        Me.imgPassFrame_6.Name = "imgPassFrame_6"
        Me.imgPassFrame_6.Size = New System.Drawing.Size(32, 16)
        Me.imgPassFrame_6.TabIndex = 108
        Me.imgPassFrame_6.TabStop = False
        '
        'imgPassFrame_5
        '
        Me.imgPassFrame_5.BackColor = System.Drawing.SystemColors.Control
        Me.imgPassFrame_5.Image = CType(resources.GetObject("imgPassFrame_5.Image"), System.Drawing.Image)
        Me.imgPassFrame.SetIndex(Me.imgPassFrame_5, CType(5, Short))
        Me.imgPassFrame_5.Location = New System.Drawing.Point(8, 89)
        Me.imgPassFrame_5.Name = "imgPassFrame_5"
        Me.imgPassFrame_5.Size = New System.Drawing.Size(32, 16)
        Me.imgPassFrame_5.TabIndex = 109
        Me.imgPassFrame_5.TabStop = False
        '
        'imgPassFrame_4
        '
        Me.imgPassFrame_4.BackColor = System.Drawing.SystemColors.Control
        Me.imgPassFrame_4.Image = CType(resources.GetObject("imgPassFrame_4.Image"), System.Drawing.Image)
        Me.imgPassFrame.SetIndex(Me.imgPassFrame_4, CType(4, Short))
        Me.imgPassFrame_4.Location = New System.Drawing.Point(8, 72)
        Me.imgPassFrame_4.Name = "imgPassFrame_4"
        Me.imgPassFrame_4.Size = New System.Drawing.Size(32, 16)
        Me.imgPassFrame_4.TabIndex = 110
        Me.imgPassFrame_4.TabStop = False
        '
        'imgPassFrame_3
        '
        Me.imgPassFrame_3.BackColor = System.Drawing.SystemColors.Control
        Me.imgPassFrame_3.Image = CType(resources.GetObject("imgPassFrame_3.Image"), System.Drawing.Image)
        Me.imgPassFrame.SetIndex(Me.imgPassFrame_3, CType(3, Short))
        Me.imgPassFrame_3.Location = New System.Drawing.Point(8, 55)
        Me.imgPassFrame_3.Name = "imgPassFrame_3"
        Me.imgPassFrame_3.Size = New System.Drawing.Size(32, 16)
        Me.imgPassFrame_3.TabIndex = 111
        Me.imgPassFrame_3.TabStop = False
        '
        'imgPassFrame_2
        '
        Me.imgPassFrame_2.BackColor = System.Drawing.SystemColors.Control
        Me.imgPassFrame_2.Image = CType(resources.GetObject("imgPassFrame_2.Image"), System.Drawing.Image)
        Me.imgPassFrame.SetIndex(Me.imgPassFrame_2, CType(2, Short))
        Me.imgPassFrame_2.Location = New System.Drawing.Point(8, 38)
        Me.imgPassFrame_2.Name = "imgPassFrame_2"
        Me.imgPassFrame_2.Size = New System.Drawing.Size(32, 16)
        Me.imgPassFrame_2.TabIndex = 112
        Me.imgPassFrame_2.TabStop = False
        '
        'imgPassFrame_1
        '
        Me.imgPassFrame_1.BackColor = System.Drawing.SystemColors.Control
        Me.imgPassFrame_1.Image = CType(resources.GetObject("imgPassFrame_1.Image"), System.Drawing.Image)
        Me.imgPassFrame.SetIndex(Me.imgPassFrame_1, CType(1, Short))
        Me.imgPassFrame_1.Location = New System.Drawing.Point(8, 21)
        Me.imgPassFrame_1.Name = "imgPassFrame_1"
        Me.imgPassFrame_1.Size = New System.Drawing.Size(32, 16)
        Me.imgPassFrame_1.TabIndex = 113
        Me.imgPassFrame_1.TabStop = False
        '
        'imgPassFrame_0
        '
        Me.imgPassFrame_0.BackColor = System.Drawing.SystemColors.Control
        Me.imgPassFrame_0.Image = CType(resources.GetObject("imgPassFrame_0.Image"), System.Drawing.Image)
        Me.imgPassFrame.SetIndex(Me.imgPassFrame_0, CType(0, Short))
        Me.imgPassFrame_0.Location = New System.Drawing.Point(8, 4)
        Me.imgPassFrame_0.Name = "imgPassFrame_0"
        Me.imgPassFrame_0.Size = New System.Drawing.Size(32, 16)
        Me.imgPassFrame_0.TabIndex = 114
        Me.imgPassFrame_0.TabStop = False
        '
        'imgFailFrame_27
        '
        Me.imgFailFrame_27.BackColor = System.Drawing.SystemColors.Control
        Me.imgFailFrame_27.Image = CType(resources.GetObject("imgFailFrame_27.Image"), System.Drawing.Image)
        Me.imgFailFrame.SetIndex(Me.imgFailFrame_27, CType(27, Short))
        Me.imgFailFrame_27.Location = New System.Drawing.Point(320, 107)
        Me.imgFailFrame_27.Name = "imgFailFrame_27"
        Me.imgFailFrame_27.Size = New System.Drawing.Size(32, 16)
        Me.imgFailFrame_27.TabIndex = 60
        Me.imgFailFrame_27.TabStop = False
        '
        'imgFailFrame_12
        '
        Me.imgFailFrame_12.BackColor = System.Drawing.SystemColors.Control
        Me.imgFailFrame_12.Image = CType(resources.GetObject("imgFailFrame_12.Image"), System.Drawing.Image)
        Me.imgFailFrame.SetIndex(Me.imgFailFrame_12, CType(12, Short))
        Me.imgFailFrame_12.Location = New System.Drawing.Point(51, 209)
        Me.imgFailFrame_12.Name = "imgFailFrame_12"
        Me.imgFailFrame_12.Size = New System.Drawing.Size(32, 16)
        Me.imgFailFrame_12.TabIndex = 61
        Me.imgFailFrame_12.TabStop = False
        '
        'imgFailFrame_26
        '
        Me.imgFailFrame_26.BackColor = System.Drawing.SystemColors.Control
        Me.imgFailFrame_26.Image = CType(resources.GetObject("imgFailFrame_26.Image"), System.Drawing.Image)
        Me.imgFailFrame.SetIndex(Me.imgFailFrame_26, CType(26, Short))
        Me.imgFailFrame_26.Location = New System.Drawing.Point(320, 90)
        Me.imgFailFrame_26.Name = "imgFailFrame_26"
        Me.imgFailFrame_26.Size = New System.Drawing.Size(32, 16)
        Me.imgFailFrame_26.TabIndex = 64
        Me.imgFailFrame_26.TabStop = False
        '
        'imgFailFrame_20
        '
        Me.imgFailFrame_20.BackColor = System.Drawing.SystemColors.Control
        Me.imgFailFrame_20.Image = CType(resources.GetObject("imgFailFrame_20.Image"), System.Drawing.Image)
        Me.imgFailFrame.SetIndex(Me.imgFailFrame_20, CType(20, Short))
        Me.imgFailFrame_20.Location = New System.Drawing.Point(320, 73)
        Me.imgFailFrame_20.Name = "imgFailFrame_20"
        Me.imgFailFrame_20.Size = New System.Drawing.Size(32, 16)
        Me.imgFailFrame_20.TabIndex = 71
        Me.imgFailFrame_20.TabStop = False
        '
        'imgFailFrame_22
        '
        Me.imgFailFrame_22.BackColor = System.Drawing.SystemColors.Control
        Me.imgFailFrame_22.Image = CType(resources.GetObject("imgFailFrame_22.Image"), System.Drawing.Image)
        Me.imgFailFrame.SetIndex(Me.imgFailFrame_22, CType(22, Short))
        Me.imgFailFrame_22.Location = New System.Drawing.Point(320, 124)
        Me.imgFailFrame_22.Name = "imgFailFrame_22"
        Me.imgFailFrame_22.Size = New System.Drawing.Size(32, 16)
        Me.imgFailFrame_22.TabIndex = 73
        Me.imgFailFrame_22.TabStop = False
        '
        'imgFailFrame_23
        '
        Me.imgFailFrame_23.BackColor = System.Drawing.SystemColors.Control
        Me.imgFailFrame_23.Image = CType(resources.GetObject("imgFailFrame_23.Image"), System.Drawing.Image)
        Me.imgFailFrame.SetIndex(Me.imgFailFrame_23, CType(23, Short))
        Me.imgFailFrame_23.Location = New System.Drawing.Point(320, 141)
        Me.imgFailFrame_23.Name = "imgFailFrame_23"
        Me.imgFailFrame_23.Size = New System.Drawing.Size(32, 16)
        Me.imgFailFrame_23.TabIndex = 74
        Me.imgFailFrame_23.TabStop = False
        '
        'imgFailFrame_24
        '
        Me.imgFailFrame_24.BackColor = System.Drawing.SystemColors.Control
        Me.imgFailFrame_24.Image = CType(resources.GetObject("imgFailFrame_24.Image"), System.Drawing.Image)
        Me.imgFailFrame.SetIndex(Me.imgFailFrame_24, CType(24, Short))
        Me.imgFailFrame_24.Location = New System.Drawing.Point(320, 158)
        Me.imgFailFrame_24.Name = "imgFailFrame_24"
        Me.imgFailFrame_24.Size = New System.Drawing.Size(32, 16)
        Me.imgFailFrame_24.TabIndex = 75
        Me.imgFailFrame_24.TabStop = False
        '
        'imgFailFrame_25
        '
        Me.imgFailFrame_25.BackColor = System.Drawing.SystemColors.Control
        Me.imgFailFrame_25.Image = CType(resources.GetObject("imgFailFrame_25.Image"), System.Drawing.Image)
        Me.imgFailFrame.SetIndex(Me.imgFailFrame_25, CType(25, Short))
        Me.imgFailFrame_25.Location = New System.Drawing.Point(320, 175)
        Me.imgFailFrame_25.Name = "imgFailFrame_25"
        Me.imgFailFrame_25.Size = New System.Drawing.Size(32, 16)
        Me.imgFailFrame_25.TabIndex = 76
        Me.imgFailFrame_25.TabStop = False
        '
        'imgFailFrame_17
        '
        Me.imgFailFrame_17.BackColor = System.Drawing.SystemColors.Control
        Me.imgFailFrame_17.Image = CType(resources.GetObject("imgFailFrame_17.Image"), System.Drawing.Image)
        Me.imgFailFrame.SetIndex(Me.imgFailFrame_17, CType(17, Short))
        Me.imgFailFrame_17.Location = New System.Drawing.Point(320, 56)
        Me.imgFailFrame_17.Name = "imgFailFrame_17"
        Me.imgFailFrame_17.Size = New System.Drawing.Size(32, 16)
        Me.imgFailFrame_17.TabIndex = 79
        Me.imgFailFrame_17.TabStop = False
        '
        'imgFailFrame_16
        '
        Me.imgFailFrame_16.BackColor = System.Drawing.SystemColors.Control
        Me.imgFailFrame_16.Image = CType(resources.GetObject("imgFailFrame_16.Image"), System.Drawing.Image)
        Me.imgFailFrame.SetIndex(Me.imgFailFrame_16, CType(16, Short))
        Me.imgFailFrame_16.Location = New System.Drawing.Point(320, 39)
        Me.imgFailFrame_16.Name = "imgFailFrame_16"
        Me.imgFailFrame_16.Size = New System.Drawing.Size(32, 16)
        Me.imgFailFrame_16.TabIndex = 82
        Me.imgFailFrame_16.TabStop = False
        '
        'imgFailFrame_15
        '
        Me.imgFailFrame_15.BackColor = System.Drawing.SystemColors.Control
        Me.imgFailFrame_15.Image = CType(resources.GetObject("imgFailFrame_15.Image"), System.Drawing.Image)
        Me.imgFailFrame.SetIndex(Me.imgFailFrame_15, CType(15, Short))
        Me.imgFailFrame_15.Location = New System.Drawing.Point(320, 22)
        Me.imgFailFrame_15.Name = "imgFailFrame_15"
        Me.imgFailFrame_15.Size = New System.Drawing.Size(32, 16)
        Me.imgFailFrame_15.TabIndex = 83
        Me.imgFailFrame_15.TabStop = False
        '
        'imgFailFrame_14
        '
        Me.imgFailFrame_14.BackColor = System.Drawing.SystemColors.Control
        Me.imgFailFrame_14.Image = CType(resources.GetObject("imgFailFrame_14.Image"), System.Drawing.Image)
        Me.imgFailFrame.SetIndex(Me.imgFailFrame_14, CType(14, Short))
        Me.imgFailFrame_14.Location = New System.Drawing.Point(320, 5)
        Me.imgFailFrame_14.Name = "imgFailFrame_14"
        Me.imgFailFrame_14.Size = New System.Drawing.Size(32, 16)
        Me.imgFailFrame_14.TabIndex = 84
        Me.imgFailFrame_14.TabStop = False
        '
        'imgFailFrame_13
        '
        Me.imgFailFrame_13.BackColor = System.Drawing.SystemColors.Control
        Me.imgFailFrame_13.Image = CType(resources.GetObject("imgFailFrame_13.Image"), System.Drawing.Image)
        Me.imgFailFrame.SetIndex(Me.imgFailFrame_13, CType(13, Short))
        Me.imgFailFrame_13.Location = New System.Drawing.Point(51, 226)
        Me.imgFailFrame_13.Name = "imgFailFrame_13"
        Me.imgFailFrame_13.Size = New System.Drawing.Size(32, 16)
        Me.imgFailFrame_13.TabIndex = 85
        Me.imgFailFrame_13.TabStop = False
        '
        'imgFailFrame_11
        '
        Me.imgFailFrame_11.BackColor = System.Drawing.SystemColors.Control
        Me.imgFailFrame_11.Image = CType(resources.GetObject("imgFailFrame_11.Image"), System.Drawing.Image)
        Me.imgFailFrame.SetIndex(Me.imgFailFrame_11, CType(11, Short))
        Me.imgFailFrame_11.Location = New System.Drawing.Point(49, 191)
        Me.imgFailFrame_11.Name = "imgFailFrame_11"
        Me.imgFailFrame_11.Size = New System.Drawing.Size(32, 16)
        Me.imgFailFrame_11.TabIndex = 86
        Me.imgFailFrame_11.TabStop = False
        '
        'imgFailFrame_10
        '
        Me.imgFailFrame_10.BackColor = System.Drawing.SystemColors.Control
        Me.imgFailFrame_10.Image = CType(resources.GetObject("imgFailFrame_10.Image"), System.Drawing.Image)
        Me.imgFailFrame.SetIndex(Me.imgFailFrame_10, CType(10, Short))
        Me.imgFailFrame_10.Location = New System.Drawing.Point(49, 174)
        Me.imgFailFrame_10.Name = "imgFailFrame_10"
        Me.imgFailFrame_10.Size = New System.Drawing.Size(32, 16)
        Me.imgFailFrame_10.TabIndex = 87
        Me.imgFailFrame_10.TabStop = False
        '
        'imgFailFrame_9
        '
        Me.imgFailFrame_9.BackColor = System.Drawing.SystemColors.Control
        Me.imgFailFrame_9.Image = CType(resources.GetObject("imgFailFrame_9.Image"), System.Drawing.Image)
        Me.imgFailFrame.SetIndex(Me.imgFailFrame_9, CType(9, Short))
        Me.imgFailFrame_9.Location = New System.Drawing.Point(49, 157)
        Me.imgFailFrame_9.Name = "imgFailFrame_9"
        Me.imgFailFrame_9.Size = New System.Drawing.Size(32, 16)
        Me.imgFailFrame_9.TabIndex = 88
        Me.imgFailFrame_9.TabStop = False
        '
        'imgFailFrame_8
        '
        Me.imgFailFrame_8.BackColor = System.Drawing.SystemColors.Control
        Me.imgFailFrame_8.Image = CType(resources.GetObject("imgFailFrame_8.Image"), System.Drawing.Image)
        Me.imgFailFrame.SetIndex(Me.imgFailFrame_8, CType(8, Short))
        Me.imgFailFrame_8.Location = New System.Drawing.Point(49, 140)
        Me.imgFailFrame_8.Name = "imgFailFrame_8"
        Me.imgFailFrame_8.Size = New System.Drawing.Size(32, 16)
        Me.imgFailFrame_8.TabIndex = 89
        Me.imgFailFrame_8.TabStop = False
        '
        'imgFailFrame_7
        '
        Me.imgFailFrame_7.BackColor = System.Drawing.SystemColors.Control
        Me.imgFailFrame_7.Image = CType(resources.GetObject("imgFailFrame_7.Image"), System.Drawing.Image)
        Me.imgFailFrame.SetIndex(Me.imgFailFrame_7, CType(7, Short))
        Me.imgFailFrame_7.Location = New System.Drawing.Point(49, 123)
        Me.imgFailFrame_7.Name = "imgFailFrame_7"
        Me.imgFailFrame_7.Size = New System.Drawing.Size(32, 16)
        Me.imgFailFrame_7.TabIndex = 90
        Me.imgFailFrame_7.TabStop = False
        '
        'imgFailFrame_6
        '
        Me.imgFailFrame_6.BackColor = System.Drawing.SystemColors.Control
        Me.imgFailFrame_6.Image = CType(resources.GetObject("imgFailFrame_6.Image"), System.Drawing.Image)
        Me.imgFailFrame.SetIndex(Me.imgFailFrame_6, CType(6, Short))
        Me.imgFailFrame_6.Location = New System.Drawing.Point(49, 106)
        Me.imgFailFrame_6.Name = "imgFailFrame_6"
        Me.imgFailFrame_6.Size = New System.Drawing.Size(32, 16)
        Me.imgFailFrame_6.TabIndex = 91
        Me.imgFailFrame_6.TabStop = False
        '
        'imgFailFrame_5
        '
        Me.imgFailFrame_5.BackColor = System.Drawing.SystemColors.Control
        Me.imgFailFrame_5.Image = CType(resources.GetObject("imgFailFrame_5.Image"), System.Drawing.Image)
        Me.imgFailFrame.SetIndex(Me.imgFailFrame_5, CType(5, Short))
        Me.imgFailFrame_5.Location = New System.Drawing.Point(49, 89)
        Me.imgFailFrame_5.Name = "imgFailFrame_5"
        Me.imgFailFrame_5.Size = New System.Drawing.Size(32, 16)
        Me.imgFailFrame_5.TabIndex = 92
        Me.imgFailFrame_5.TabStop = False
        '
        'imgFailFrame_4
        '
        Me.imgFailFrame_4.BackColor = System.Drawing.SystemColors.Control
        Me.imgFailFrame_4.Image = CType(resources.GetObject("imgFailFrame_4.Image"), System.Drawing.Image)
        Me.imgFailFrame.SetIndex(Me.imgFailFrame_4, CType(4, Short))
        Me.imgFailFrame_4.Location = New System.Drawing.Point(49, 72)
        Me.imgFailFrame_4.Name = "imgFailFrame_4"
        Me.imgFailFrame_4.Size = New System.Drawing.Size(32, 16)
        Me.imgFailFrame_4.TabIndex = 93
        Me.imgFailFrame_4.TabStop = False
        '
        'imgFailFrame_3
        '
        Me.imgFailFrame_3.BackColor = System.Drawing.SystemColors.Control
        Me.imgFailFrame_3.Image = CType(resources.GetObject("imgFailFrame_3.Image"), System.Drawing.Image)
        Me.imgFailFrame.SetIndex(Me.imgFailFrame_3, CType(3, Short))
        Me.imgFailFrame_3.Location = New System.Drawing.Point(49, 55)
        Me.imgFailFrame_3.Name = "imgFailFrame_3"
        Me.imgFailFrame_3.Size = New System.Drawing.Size(32, 16)
        Me.imgFailFrame_3.TabIndex = 94
        Me.imgFailFrame_3.TabStop = False
        '
        'imgFailFrame_2
        '
        Me.imgFailFrame_2.BackColor = System.Drawing.SystemColors.Control
        Me.imgFailFrame_2.Image = CType(resources.GetObject("imgFailFrame_2.Image"), System.Drawing.Image)
        Me.imgFailFrame.SetIndex(Me.imgFailFrame_2, CType(2, Short))
        Me.imgFailFrame_2.Location = New System.Drawing.Point(49, 38)
        Me.imgFailFrame_2.Name = "imgFailFrame_2"
        Me.imgFailFrame_2.Size = New System.Drawing.Size(32, 16)
        Me.imgFailFrame_2.TabIndex = 95
        Me.imgFailFrame_2.TabStop = False
        '
        'imgFailFrame_1
        '
        Me.imgFailFrame_1.BackColor = System.Drawing.SystemColors.Control
        Me.imgFailFrame_1.Image = CType(resources.GetObject("imgFailFrame_1.Image"), System.Drawing.Image)
        Me.imgFailFrame.SetIndex(Me.imgFailFrame_1, CType(1, Short))
        Me.imgFailFrame_1.Location = New System.Drawing.Point(49, 21)
        Me.imgFailFrame_1.Name = "imgFailFrame_1"
        Me.imgFailFrame_1.Size = New System.Drawing.Size(32, 16)
        Me.imgFailFrame_1.TabIndex = 96
        Me.imgFailFrame_1.TabStop = False
        '
        'imgFailFrame_0
        '
        Me.imgFailFrame_0.BackColor = System.Drawing.SystemColors.Control
        Me.imgFailFrame_0.Image = CType(resources.GetObject("imgFailFrame_0.Image"), System.Drawing.Image)
        Me.imgFailFrame.SetIndex(Me.imgFailFrame_0, CType(0, Short))
        Me.imgFailFrame_0.Location = New System.Drawing.Point(49, 4)
        Me.imgFailFrame_0.Name = "imgFailFrame_0"
        Me.imgFailFrame_0.Size = New System.Drawing.Size(32, 16)
        Me.imgFailFrame_0.TabIndex = 97
        Me.imgFailFrame_0.TabStop = False
        '
        'CloseButton
        '
        Me.CloseButton.BackColor = System.Drawing.SystemColors.Control
        Me.CloseButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CloseButton.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CloseButton.Location = New System.Drawing.Point(348, 275)
        Me.CloseButton.Name = "CloseButton"
        Me.CloseButton.Size = New System.Drawing.Size(90, 25)
        Me.CloseButton.TabIndex = 60
        Me.CloseButton.Text = "&Close"
        Me.CloseButton.UseVisualStyleBackColor = False
        Me.CloseButton.Visible = False
        '
        'Details
        '
        Me.Details.BackColor = System.Drawing.SystemColors.Control
        Me.Details.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Details.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Details.Location = New System.Drawing.Point(447, 275)
        Me.Details.Name = "Details"
        Me.Details.Size = New System.Drawing.Size(90, 25)
        Me.Details.TabIndex = 59
        Me.Details.Text = "&Details >>"
        Me.Details.UseVisualStyleBackColor = False
        '
        'SSPanel1
        '
        Me.SSPanel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.SSPanel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SSPanel1.Controls.Add(Me.pctIcon_1)
        Me.SSPanel1.Controls.Add(Me.pctIcon_3)
        Me.SSPanel1.Controls.Add(Me.pctIcon_6)
        Me.SSPanel1.Controls.Add(Me.pctIcon_7)
        Me.SSPanel1.Controls.Add(Me.pctIcon_2)
        Me.SSPanel1.Controls.Add(Me.pctIcon_5)
        Me.SSPanel1.Controls.Add(Me.pctIcon_4)
        Me.SSPanel1.Controls.Add(Me.pctIcon_11)
        Me.SSPanel1.Controls.Add(Me.pctIcon_10)
        Me.SSPanel1.Controls.Add(Me.pctIcon_9)
        Me.SSPanel1.Controls.Add(Me.pctIcon_8)
        Me.SSPanel1.Controls.Add(Me.pctIcon_0)
        Me.SSPanel1.Controls.Add(Me.InstrumentLabel_1)
        Me.SSPanel1.Controls.Add(Me.InstrumentLabel_3)
        Me.SSPanel1.Controls.Add(Me.InstrumentLabel_2)
        Me.SSPanel1.Controls.Add(Me.InstrumentLabel_6)
        Me.SSPanel1.Controls.Add(Me.InstrumentLabel_5)
        Me.SSPanel1.Controls.Add(Me.InstrumentLabel_4)
        Me.SSPanel1.Controls.Add(Me.InstrumentLabel_7)
        Me.SSPanel1.Controls.Add(Me.imgFailFrame_11)
        Me.SSPanel1.Controls.Add(Me.imgFailFrame_10)
        Me.SSPanel1.Controls.Add(Me.imgFailFrame_9)
        Me.SSPanel1.Controls.Add(Me.imgFailFrame_8)
        Me.SSPanel1.Controls.Add(Me.imgFailFrame_7)
        Me.SSPanel1.Controls.Add(Me.imgFailFrame_6)
        Me.SSPanel1.Controls.Add(Me.imgFailFrame_5)
        Me.SSPanel1.Controls.Add(Me.imgFailFrame_4)
        Me.SSPanel1.Controls.Add(Me.imgFailFrame_3)
        Me.SSPanel1.Controls.Add(Me.imgFailFrame_2)
        Me.SSPanel1.Controls.Add(Me.imgFailFrame_1)
        Me.SSPanel1.Controls.Add(Me.imgFailFrame_0)
        Me.SSPanel1.Controls.Add(Me.imgPassFrame_11)
        Me.SSPanel1.Controls.Add(Me.imgPassFrame_10)
        Me.SSPanel1.Controls.Add(Me.imgPassFrame_9)
        Me.SSPanel1.Controls.Add(Me.imgPassFrame_8)
        Me.SSPanel1.Controls.Add(Me.imgPassFrame_7)
        Me.SSPanel1.Controls.Add(Me.imgPassFrame_6)
        Me.SSPanel1.Controls.Add(Me.imgPassFrame_5)
        Me.SSPanel1.Controls.Add(Me.imgPassFrame_4)
        Me.SSPanel1.Controls.Add(Me.imgPassFrame_3)
        Me.SSPanel1.Controls.Add(Me.imgPassFrame_2)
        Me.SSPanel1.Controls.Add(Me.imgPassFrame_1)
        Me.SSPanel1.Controls.Add(Me.imgPassFrame_0)
        Me.SSPanel1.Controls.Add(Me.InstrumentLabel_11)
        Me.SSPanel1.Controls.Add(Me.InstrumentLabel_10)
        Me.SSPanel1.Controls.Add(Me.InstrumentLabel_9)
        Me.SSPanel1.Controls.Add(Me.InstrumentLabel_8)
        Me.SSPanel1.Controls.Add(Me.InstrumentLabel_0)
        Me.SSPanel1.Location = New System.Drawing.Point(0, -1)
        Me.SSPanel1.Name = "SSPanel1"
        Me.SSPanel1.Size = New System.Drawing.Size(559, 252)
        Me.SSPanel1.TabIndex = 1
        Me.SSPanel1.TabStop = False
        '
        'DetailsText
        '
        Me.DetailsText.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DetailsText.Location = New System.Drawing.Point(0, 340)
        Me.DetailsText.Name = "DetailsText"
        Me.DetailsText.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical
        Me.DetailsText.Size = New System.Drawing.Size(552, 118)
        Me.DetailsText.TabIndex = 22
        Me.DetailsText.Text = ""
        Me.DetailsText.Visible = False
        '
        'sbrUserInformation
        '
        Me.sbrUserInformation.Location = New System.Drawing.Point(0, 464)
        Me.sbrUserInformation.Name = "sbrUserInformation"
        Me.sbrUserInformation.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.sbrUserInformation_Panel1})
        Me.sbrUserInformation.Size = New System.Drawing.Size(552, 17)
        Me.sbrUserInformation.SizingGrip = False
        Me.sbrUserInformation.TabIndex = 0
        '
        'sbrUserInformation_Panel1
        '
        Me.sbrUserInformation_Panel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
        Me.sbrUserInformation_Panel1.Name = "sbrUserInformation_Panel1"
        Me.sbrUserInformation_Panel1.Text = "APS-6061 Fixed Power Unit Failed"
        Me.sbrUserInformation_Panel1.Width = 550
        '
        'imlImageList
        '
        Me.imlImageList.ImageStream = CType(resources.GetObject("imlImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imlImageList.TransparentColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.imlImageList.Images.SetKeyName(0, "Pass")
        Me.imlImageList.Images.SetKeyName(1, "Fail")
        Me.imlImageList.Images.SetKeyName(2, "NoTest")
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 1
        '
        'imgLogo
        '
        Me.imgLogo.Location = New System.Drawing.Point(12, 257)
        Me.imgLogo.Name = "imgLogo"
        Me.imgLogo.Size = New System.Drawing.Size(99, 77)
        Me.imgLogo.TabIndex = 103
        Me.imgLogo.TabStop = False
        '
        'frmCTest
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(552, 481)
        Me.ControlBox = False
        Me.Controls.Add(Me.imgLogo)
        Me.Controls.Add(Me.InstrumentLabel_12)
        Me.Controls.Add(Me.imgPassFrame_12)
        Me.Controls.Add(Me.imgFailFrame_12)
        Me.Controls.Add(Me.pctIcon_13)
        Me.Controls.Add(Me.pctIcon_12)
        Me.Controls.Add(Me.pctIcon_26)
        Me.Controls.Add(Me.pctIcon_27)
        Me.Controls.Add(Me.pctIcon_17)
        Me.Controls.Add(Me.pctIcon_23)
        Me.Controls.Add(Me.pctIcon_25)
        Me.Controls.Add(Me.pctIcon_22)
        Me.Controls.Add(Me.pctIcon_24)
        Me.Controls.Add(Me.imgPassFrame_15)
        Me.Controls.Add(Me.pctIcon_20)
        Me.Controls.Add(Me.InstrumentLabel_16)
        Me.Controls.Add(Me.imgFailFrame_13)
        Me.Controls.Add(Me.InstrumentLabel_15)
        Me.Controls.Add(Me.imgPassFrame_16)
        Me.Controls.Add(Me.pctIcon_15)
        Me.Controls.Add(Me.imgPassFrame_17)
        Me.Controls.Add(Me.imgFailFrame_15)
        Me.Controls.Add(Me.imgFailFrame_16)
        Me.Controls.Add(Me.pctIcon_16)
        Me.Controls.Add(Me.imgFailFrame_17)
        Me.Controls.Add(Me.InstrumentLabel_26)
        Me.Controls.Add(Me.InstrumentLabel_27)
        Me.Controls.Add(Me.imgPassFrame_13)
        Me.Controls.Add(Me.imgPassFrame_27)
        Me.Controls.Add(Me.imgFailFrame_25)
        Me.Controls.Add(Me.imgFailFrame_27)
        Me.Controls.Add(Me.imgFailFrame_24)
        Me.Controls.Add(Me.imgPassFrame_26)
        Me.Controls.Add(Me.imgFailFrame_23)
        Me.Controls.Add(Me.imgFailFrame_26)
        Me.Controls.Add(Me.imgFailFrame_22)
        Me.Controls.Add(Me.InstrumentLabel_17)
        Me.Controls.Add(Me.imgFailFrame_20)
        Me.Controls.Add(Me.imgPassFrame_25)
        Me.Controls.Add(Me.InstrumentLabel_13)
        Me.Controls.Add(Me.imgPassFrame_24)
        Me.Controls.Add(Me.imgPassFrame_23)
        Me.Controls.Add(Me.imgPassFrame_22)
        Me.Controls.Add(Me.imgPassFrame_20)
        Me.Controls.Add(Me.InstrumentLabel_25)
        Me.Controls.Add(Me.InstrumentLabel_20)
        Me.Controls.Add(Me.InstrumentLabel_22)
        Me.Controls.Add(Me.InstrumentLabel_23)
        Me.Controls.Add(Me.InstrumentLabel_24)
        Me.Controls.Add(Me.imgPassFrame_14)
        Me.Controls.Add(Me.InstrumentLabel_14)
        Me.Controls.Add(Me.imgFailFrame_14)
        Me.Controls.Add(Me.pctIcon_14)
        Me.Controls.Add(Me.CloseButton)
        Me.Controls.Add(Me.Details)
        Me.Controls.Add(Me.SSPanel1)
        Me.Controls.Add(Me.DetailsText)
        Me.Controls.Add(Me.sbrUserInformation)
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCTest"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "System Survey"
        CType(Me.pctIcon, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pctIcon_26, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pctIcon_27, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pctIcon_17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pctIcon_1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pctIcon_3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pctIcon_6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pctIcon_7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pctIcon_2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pctIcon_5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pctIcon_4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pctIcon_23, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pctIcon_25, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pctIcon_22, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pctIcon_24, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pctIcon_20, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pctIcon_15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pctIcon_14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pctIcon_13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pctIcon_12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pctIcon_11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pctIcon_10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pctIcon_9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pctIcon_16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pctIcon_8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pctIcon_0, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.InstrumentLabel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgPassFrame, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgPassFrame_27, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgPassFrame_26, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgPassFrame_14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgPassFrame_20, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgPassFrame_22, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgPassFrame_23, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgPassFrame_24, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgPassFrame_25, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgPassFrame_17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgPassFrame_16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgPassFrame_15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgPassFrame_13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgPassFrame_12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgPassFrame_11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgPassFrame_10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgPassFrame_9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgPassFrame_8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgPassFrame_7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgPassFrame_6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgPassFrame_5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgPassFrame_4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgPassFrame_3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgPassFrame_2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgPassFrame_1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgPassFrame_0, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgFailFrame, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgFailFrame_27, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgFailFrame_12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgFailFrame_26, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgFailFrame_20, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgFailFrame_22, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgFailFrame_23, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgFailFrame_24, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgFailFrame_25, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgFailFrame_17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgFailFrame_16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgFailFrame_15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgFailFrame_14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgFailFrame_13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgFailFrame_11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgFailFrame_10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgFailFrame_9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgFailFrame_8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgFailFrame_7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgFailFrame_6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgFailFrame_5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgFailFrame_4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgFailFrame_3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgFailFrame_2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgFailFrame_1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgFailFrame_0, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SSPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SSPanel1.ResumeLayout(False)
        CType(Me.sbrUserInformation_Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    '=========================================================


    Private Sub Close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CloseButton.Click
        EndProgram()
    End Sub


    Private Sub Details_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Details.Click
        HandleDetails()
    End Sub

    Private Sub imgLogo_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles imgLogo.MouseDown

        Dim Button As Short = e.Button \ &H100000
        Dim Shift As Short = Control.ModifierKeys \ &H10000
        Dim X As Single = VB6.PixelsToTwipsX(e.X)
        Dim Y As Single = VB6.PixelsToTwipsY(e.Y)

        'Escape code to get past Confidence Test at startup
        If Button = 1 And Shift = 2 Then ' If Ctrl-LEFT button
            SystemStatus = PASSED
            EOPower("OFF")
            EndProgram()
        End If
    End Sub


    Sub pctIcon_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles pctIcon.Click, pctIcon_0.Click, pctIcon_1.Click, pctIcon_14.Click
        Dim instrumentindex As Short = pctIcon.GetIndex(sender)

        'V1.6 added to clear P/F status upon retest
        Me.imgPassFrame(instrumentindex).Image = Me.imlImageList.Images("NoTest")
        Me.imgFailFrame(instrumentindex).Image = Me.imlImageList.Images("NoTest")

        'Reset the caption to signify that another EO module may be tested
        If instrumentindex = EO_MOD Then
            InstrumentDescription(EO_MOD) = "SBIR EO Module"
            VIPERTName(EO_MOD) = "EO Module"
            Me.InstrumentLabel(EO_MOD).Text = "Any EO Module"
        End If
        If (Not InstrumentInitialized(instrumentindex)) Or (instrumentindex = EO_MOD) Then
            bRetryInit = True 'Set Retry Flag
            InitInstrument(instrumentindex) 'V1.6 added to re-init if first attempt failed
            bRetryInit = False 'Reset Flag
        End If

        TestInstrument(instrumentindex, False)

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Me.Timer1.Enabled = False
        System.Threading.Thread.Sleep(100)
        CTestMain.Main()
    End Sub
End Class