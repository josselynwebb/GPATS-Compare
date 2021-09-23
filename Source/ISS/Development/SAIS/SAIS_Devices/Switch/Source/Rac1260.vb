
Imports System
Imports System.Windows.Forms
Imports System.Windows.Forms.Screen
Imports Microsoft.VisualBasic
Imports SwitchLib

Public Class frmRac1260
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
    Friend WithEvents cmdHelp As System.Windows.Forms.Button
    Friend WithEvents CommonDialog1 As System.Windows.Forms.PrintDialog
    Friend WithEvents cmdQuit As System.Windows.Forms.Button
    Friend WithEvents cmdReset As System.Windows.Forms.Button
    Friend WithEvents tabOptions As System.Windows.Forms.TabControl
    Friend WithEvents tabOptions_Page1 As System.Windows.Forms.TabPage
    Friend WithEvents panS101 As System.Windows.Forms.Panel
    Friend WithEvents saS101 As SwitchLib.DPSTArray
    Friend WithEvents lblS101 As System.Windows.Forms.Label
    Friend WithEvents cmdS101Reset As System.Windows.Forms.Button
    Friend WithEvents cmdExerciseGroupDCPower As System.Windows.Forms.Button
    Friend WithEvents tabOptions_Page2 As System.Windows.Forms.TabPage
    Friend WithEvents cmdExerciseGroup2X24_2W_LF As System.Windows.Forms.Button
    Friend WithEvents cmdS200ResetAll As System.Windows.Forms.Button
    Friend WithEvents panS200_0 As System.Windows.Forms.Panel
    Friend WithEvents saS201 As SwitchLib.ctl2W2RowArray
    Friend WithEvents lblS200_0 As System.Windows.Forms.Label
    Friend WithEvents lblS201Pin1 As System.Windows.Forms.Label
    Friend WithEvents lblS201Pin2 As System.Windows.Forms.Label
    Friend WithEvents lblS201Pin3 As System.Windows.Forms.Label
    Friend WithEvents lblS201Pin4 As System.Windows.Forms.Label
    Friend WithEvents panS200_1 As System.Windows.Forms.Panel
    Friend WithEvents saS202 As SwitchLib.ctl2W2RowArray
    Friend WithEvents lblS202Pin3 As System.Windows.Forms.Label
    Friend WithEvents lblS202Pin1 As System.Windows.Forms.Label
    Friend WithEvents lblS200_1 As System.Windows.Forms.Label
    Friend WithEvents tabOptions_Page3 As System.Windows.Forms.TabPage
    Friend WithEvents cmdExerciseGroup1x1_1W_LF As System.Windows.Forms.Button
    Friend WithEvents cmdS301ResetAll As System.Windows.Forms.Button
    Friend WithEvents panS301 As System.Windows.Forms.Panel
    Friend WithEvents saS301 As SwitchLib.SPSTArray
    Friend WithEvents lblS301 As System.Windows.Forms.Label
    Friend WithEvents tabOptions_Page4 As System.Windows.Forms.TabPage
    Friend WithEvents panS400_5 As System.Windows.Forms.Panel
    Friend WithEvents saS406 As SwitchLib.ctl1W1RowArray
    Friend WithEvents lblS400Row_5 As System.Windows.Forms.Label
    Friend WithEvents lblS400_5 As System.Windows.Forms.Label
    Friend WithEvents panS400_4 As System.Windows.Forms.Panel
    Friend WithEvents saS405 As SwitchLib.ctl1W1RowArray
    Friend WithEvents lblS400Row_4 As System.Windows.Forms.Label
    Friend WithEvents lblS400_4 As System.Windows.Forms.Label
    Friend WithEvents panS400_3 As System.Windows.Forms.Panel
    Friend WithEvents saS404 As SwitchLib.ctl1W1RowArray
    Friend WithEvents lblS400Row_3 As System.Windows.Forms.Label
    Friend WithEvents lblS400_3 As System.Windows.Forms.Label
    Friend WithEvents panS400_2 As System.Windows.Forms.Panel
    Friend WithEvents saS403 As SwitchLib.ctl1W1RowArray
    Friend WithEvents lblS400Row_2 As System.Windows.Forms.Label
    Friend WithEvents lblS400_2 As System.Windows.Forms.Label
    Friend WithEvents panS400_1 As System.Windows.Forms.Panel
    Friend WithEvents saS402 As SwitchLib.ctl1W1RowArray
    Friend WithEvents lblS400Row_1 As System.Windows.Forms.Label
    Friend WithEvents lblS400_1 As System.Windows.Forms.Label
    Friend WithEvents panS400_0 As System.Windows.Forms.Panel
    Friend WithEvents saS401 As SwitchLib.ctl1W1RowArray
    Friend WithEvents lblS400_0 As System.Windows.Forms.Label
    Friend WithEvents lblS400Row_0 As System.Windows.Forms.Label
    Friend WithEvents cmdS400ResetAll As System.Windows.Forms.Button
    Friend WithEvents cmdExerciseGroup1x4_1W_LF As System.Windows.Forms.Button
    Friend WithEvents tabOptions_Page5 As System.Windows.Forms.TabPage
    Friend WithEvents panS500_9 As System.Windows.Forms.Panel
    Friend WithEvents saS510 As SwitchLib.ctl1W2RowArray
    Friend WithEvents lblS500Row1_9 As System.Windows.Forms.Label
    Friend WithEvents lblS500Row2_9 As System.Windows.Forms.Label
    Friend WithEvents lblS500_9 As System.Windows.Forms.Label
    Friend WithEvents panS500_8 As System.Windows.Forms.Panel
    Friend WithEvents saS509 As SwitchLib.ctl1W2RowArray
    Friend WithEvents lblS500Row1_8 As System.Windows.Forms.Label
    Friend WithEvents lblS500Row2_8 As System.Windows.Forms.Label
    Friend WithEvents lblS500_8 As System.Windows.Forms.Label
    Friend WithEvents panS500_7 As System.Windows.Forms.Panel
    Friend WithEvents saS508 As SwitchLib.ctl1W2RowArray
    Friend WithEvents lblS500Row1_7 As System.Windows.Forms.Label
    Friend WithEvents lblS500Row2_7 As System.Windows.Forms.Label
    Friend WithEvents lblS500_7 As System.Windows.Forms.Label
    Friend WithEvents panS500_6 As System.Windows.Forms.Panel
    Friend WithEvents saS507 As SwitchLib.ctl1W2RowArray
    Friend WithEvents lblS500Row1_6 As System.Windows.Forms.Label
    Friend WithEvents lblS500Row2_6 As System.Windows.Forms.Label
    Friend WithEvents lblS500_6 As System.Windows.Forms.Label
    Friend WithEvents panS500_5 As System.Windows.Forms.Panel
    Friend WithEvents saS506 As SwitchLib.ctl1W2RowArray
    Friend WithEvents lblS500_5 As System.Windows.Forms.Label
    Friend WithEvents lblS500Row2_5 As System.Windows.Forms.Label
    Friend WithEvents lblS500Row1_5 As System.Windows.Forms.Label
    Friend WithEvents panS500_4 As System.Windows.Forms.Panel
    Friend WithEvents saS505 As SwitchLib.ctl1W2RowArray
    Friend WithEvents lblS500_4 As System.Windows.Forms.Label
    Friend WithEvents lblS500Row2_4 As System.Windows.Forms.Label
    Friend WithEvents lblS500Row1_4 As System.Windows.Forms.Label
    Friend WithEvents panS500_3 As System.Windows.Forms.Panel
    Friend WithEvents saS504 As SwitchLib.ctl1W2RowArray
    Friend WithEvents lblS500_3 As System.Windows.Forms.Label
    Friend WithEvents lblS500Row2_3 As System.Windows.Forms.Label
    Friend WithEvents lblS500Row1_3 As System.Windows.Forms.Label
    Friend WithEvents panS500_2 As System.Windows.Forms.Panel
    Friend WithEvents saS503 As SwitchLib.ctl1W2RowArray
    Friend WithEvents lblS500_2 As System.Windows.Forms.Label
    Friend WithEvents lblS500Row2_2 As System.Windows.Forms.Label
    Friend WithEvents lblS500Row1_2 As System.Windows.Forms.Label
    Friend WithEvents panS500_1 As System.Windows.Forms.Panel
    Friend WithEvents saS502 As SwitchLib.ctl1W2RowArray
    Friend WithEvents lblS500_1 As System.Windows.Forms.Label
    Friend WithEvents lblS500Row2_1 As System.Windows.Forms.Label
    Friend WithEvents lblS500Row1_1 As System.Windows.Forms.Label
    Friend WithEvents panS500_0 As System.Windows.Forms.Panel
    Friend WithEvents saS501 As SwitchLib.ctl1W2RowArray
    Friend WithEvents lblS500Row1_0 As System.Windows.Forms.Label
    Friend WithEvents lblS500Row2_0 As System.Windows.Forms.Label
    Friend WithEvents lblS500_0 As System.Windows.Forms.Label
    Friend WithEvents cmdS500ResetAll As System.Windows.Forms.Button
    Friend WithEvents cmdExerciseGroup2x8_1W_LF As System.Windows.Forms.Button
    Friend WithEvents tabOptions_Page6 As System.Windows.Forms.TabPage
    Friend WithEvents cmdExerciseGroup1x2_1W_LF As System.Windows.Forms.Button
    Friend WithEvents cmdS600ResetAll As System.Windows.Forms.Button
    Friend WithEvents panS600_0 As System.Windows.Forms.Panel
    Friend WithEvents lblS600Row_0 As System.Windows.Forms.Label
    Friend WithEvents lblS600_0 As System.Windows.Forms.Label
    Friend WithEvents saS601 As SwitchLib.ctl1W1RowArray
    Friend WithEvents panS600_1 As System.Windows.Forms.Panel
    Friend WithEvents lblS600_1 As System.Windows.Forms.Label
    Friend WithEvents lblS600Row_1 As System.Windows.Forms.Label
    Friend WithEvents saS602 As SwitchLib.ctl1W1RowArray
    Friend WithEvents panS600_2 As System.Windows.Forms.Panel
    Friend WithEvents lblS600_2 As System.Windows.Forms.Label
    Friend WithEvents lblS600Row_2 As System.Windows.Forms.Label
    Friend WithEvents saS603 As SwitchLib.ctl1W1RowArray
    Friend WithEvents panS600_3 As System.Windows.Forms.Panel
    Friend WithEvents lblS600_3 As System.Windows.Forms.Label
    Friend WithEvents lblS600Row_3 As System.Windows.Forms.Label
    Friend WithEvents saS604 As SwitchLib.ctl1W1RowArray
    Friend WithEvents panS600_4 As System.Windows.Forms.Panel
    Friend WithEvents lblS600_4 As System.Windows.Forms.Label
    Friend WithEvents lblS600Row_4 As System.Windows.Forms.Label
    Friend WithEvents saS605 As SwitchLib.ctl1W1RowArray
    Friend WithEvents panS600_5 As System.Windows.Forms.Panel
    Friend WithEvents lblS600_5 As System.Windows.Forms.Label
    Friend WithEvents lblS600Row_5 As System.Windows.Forms.Label
    Friend WithEvents saS606 As SwitchLib.ctl1W1RowArray
    Friend WithEvents panS600_6 As System.Windows.Forms.Panel
    Friend WithEvents lblS600_6 As System.Windows.Forms.Label
    Friend WithEvents lblS600Row_6 As System.Windows.Forms.Label
    Friend WithEvents saS607 As SwitchLib.ctl1W1RowArray
    Friend WithEvents panS600_7 As System.Windows.Forms.Panel
    Friend WithEvents lblS600Row_7 As System.Windows.Forms.Label
    Friend WithEvents lblS600_7 As System.Windows.Forms.Label
    Friend WithEvents saS608 As SwitchLib.ctl1W1RowArray
    Friend WithEvents panS600_8 As System.Windows.Forms.Panel
    Friend WithEvents lblS600Row_8 As System.Windows.Forms.Label
    Friend WithEvents lblS600_8 As System.Windows.Forms.Label
    Friend WithEvents saS609 As SwitchLib.ctl1W1RowArray
    Friend WithEvents panS600_9 As System.Windows.Forms.Panel
    Friend WithEvents lblS600Row_9 As System.Windows.Forms.Label
    Friend WithEvents lblS600_9 As System.Windows.Forms.Label
    Friend WithEvents saS610 As SwitchLib.ctl1W1RowArray
    Friend WithEvents panS600_10 As System.Windows.Forms.Panel
    Friend WithEvents lblS600Row_10 As System.Windows.Forms.Label
    Friend WithEvents lblS600_10 As System.Windows.Forms.Label
    Friend WithEvents saS611 As SwitchLib.ctl1W1RowArray
    Friend WithEvents panS600_11 As System.Windows.Forms.Panel
    Friend WithEvents lblS600Row_11 As System.Windows.Forms.Label
    Friend WithEvents lblS600_11 As System.Windows.Forms.Label
    Friend WithEvents saS612 As SwitchLib.ctl1W1RowArray
    Friend WithEvents tabOptions_Page7 As System.Windows.Forms.TabPage
    Friend WithEvents cmdExerciseGroup1x24_1x8_2W_LF As System.Windows.Forms.Button
    Friend WithEvents cmdS700ResetAll As System.Windows.Forms.Button
    Friend WithEvents panS701 As System.Windows.Forms.Panel
    Friend WithEvents saS701 As SwitchLib.ctl2W1RowArray
    Friend WithEvents lblS701 As System.Windows.Forms.Label
    Friend WithEvents lblS701Row1 As System.Windows.Forms.Label
    Friend WithEvents panS751 As System.Windows.Forms.Panel
    Friend WithEvents saS751 As SwitchLib.ctl2W1RowArray
    Friend WithEvents lblS751Row As System.Windows.Forms.Label
    Friend WithEvents lblS751 As System.Windows.Forms.Label
    Friend WithEvents tabOptions_Page8 As System.Windows.Forms.TabPage
    Friend WithEvents cmdExerciseGroup1x8_Coax_MF As System.Windows.Forms.Button
    Friend WithEvents cmdS800ResetAll As System.Windows.Forms.Button
    Friend WithEvents panS800_0 As System.Windows.Forms.Panel
    Friend WithEvents saS801 As SwitchLib.ctl1x8Mux
    Friend WithEvents lblS800Pin1_0 As System.Windows.Forms.Label
    Friend WithEvents lblS800_0 As System.Windows.Forms.Label
    Friend WithEvents panS800_1 As System.Windows.Forms.Panel
    Friend WithEvents saS802 As SwitchLib.ctl1x8Mux
    Friend WithEvents lblS800Pin1_1 As System.Windows.Forms.Label
    Friend WithEvents lblS800_1 As System.Windows.Forms.Label
    Friend WithEvents panS800_2 As System.Windows.Forms.Panel
    Friend WithEvents saS803 As SwitchLib.ctl1x8Mux
    Friend WithEvents lblS800Pin1_2 As System.Windows.Forms.Label
    Friend WithEvents lblS800_2 As System.Windows.Forms.Label
    Friend WithEvents panS800_3 As System.Windows.Forms.Panel
    Friend WithEvents saS804 As SwitchLib.ctl1x8Mux
    Friend WithEvents lblS800Pin1_3 As System.Windows.Forms.Label
    Friend WithEvents lblS800_3 As System.Windows.Forms.Label
    Friend WithEvents tabOptions_Page9 As System.Windows.Forms.TabPage
    Friend WithEvents cmdExerciseGroup1x6_Coax_HF As System.Windows.Forms.Button
    Friend WithEvents cmdS900ResetAll As System.Windows.Forms.Button
    Friend WithEvents panS900_0 As System.Windows.Forms.Panel
    Friend WithEvents saS901 As SwitchLib.ctl1x6Mux
    Friend WithEvents lblS900Pin1_0 As System.Windows.Forms.Label
    Friend WithEvents lblS900_0 As System.Windows.Forms.Label
    Friend WithEvents panS900_1 As System.Windows.Forms.Panel
    Friend WithEvents saS902 As SwitchLib.ctl1x6Mux
    Friend WithEvents lblS900Pin1_1 As System.Windows.Forms.Label
    Friend WithEvents lblS900_1 As System.Windows.Forms.Label
    Friend WithEvents panS900_2 As System.Windows.Forms.Panel
    Friend WithEvents saS903 As SwitchLib.ctl1x6Mux
    Friend WithEvents lblS900Pin1_2 As System.Windows.Forms.Label
    Friend WithEvents lblS900_2 As System.Windows.Forms.Label
    Friend WithEvents panS900_3 As System.Windows.Forms.Panel
    Friend WithEvents saS904 As SwitchLib.ctl1x6Mux
    Friend WithEvents lblS900Pin1_3 As System.Windows.Forms.Label
    Friend WithEvents lblS900_3 As System.Windows.Forms.Label
    Friend WithEvents panS900_4 As System.Windows.Forms.Panel
    Friend WithEvents saS905 As SwitchLib.ctl1x6Mux
    Friend WithEvents lblS900Pin1_4 As System.Windows.Forms.Label
    Friend WithEvents lblS900_4 As System.Windows.Forms.Label
    Friend WithEvents panS900_5 As System.Windows.Forms.Panel
    Friend WithEvents saS906 As SwitchLib.ctl1x6Mux
    Friend WithEvents lblS900Pin1_5 As System.Windows.Forms.Label
    Friend WithEvents lblS900_5 As System.Windows.Forms.Label
    Friend WithEvents tabOptions_Page10 As System.Windows.Forms.TabPage
    Friend WithEvents fraPinList As System.Windows.Forms.GroupBox
    Friend WithEvents panSortSig As System.Windows.Forms.Panel
    Friend WithEvents grdSortSig As System.Windows.Forms.DataGridView
    Friend WithEvents lblSortSigCnx As System.Windows.Forms.Label
    Friend WithEvents lblSortSigSig As System.Windows.Forms.Label
    Friend WithEvents panSortCnx As System.Windows.Forms.Panel
    Friend WithEvents grdSortCnx As System.Windows.Forms.DataGridView
    Friend WithEvents cmdAbout As System.Windows.Forms.Button
    Friend WithEvents cmdTest As System.Windows.Forms.Button
    Friend WithEvents fraRelayExerciser As System.Windows.Forms.GroupBox
    Friend WithEvents cmdExercise As System.Windows.Forms.Button
    Friend WithEvents cboCardType As System.Windows.Forms.ComboBox
    Friend WithEvents cmdOpenAll As System.Windows.Forms.Button
    Friend WithEvents Panel_Conifg As VIPERT_Common_Controls.Panel_Conifg
    Friend WithEvents tabOptions_Page11 As System.Windows.Forms.TabPage
    Friend WithEvents Atlas_SFP As VIPERT_Common_Controls.Atlas_SFP
    Friend WithEvents sbrUserInformation As System.Windows.Forms.StatusBar
    Friend WithEvents sbrUserInformation_Panel1 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents sbrUserInformation_Panel2 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents pcp1x6Mux As System.Windows.Forms.PictureBox
    Friend WithEvents lblS202Pin4 As System.Windows.Forms.Label
    Friend WithEvents lblS202Pin2 As System.Windows.Forms.Label
    Friend WithEvents lblS701Row2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cvxReceiver As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cvxSwitch As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgvSwitch As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgvReceiver As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents lblDdeLink As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRac1260))
        Me.cmdHelp = New System.Windows.Forms.Button()
        Me.CommonDialog1 = New System.Windows.Forms.PrintDialog()
        Me.cmdQuit = New System.Windows.Forms.Button()
        Me.cmdReset = New System.Windows.Forms.Button()
        Me.tabOptions = New System.Windows.Forms.TabControl()
        Me.tabOptions_Page1 = New System.Windows.Forms.TabPage()
        Me.panS101 = New System.Windows.Forms.Panel()
        Me.lblS101 = New System.Windows.Forms.Label()
        Me.saS101 = New SwitchLib.DPSTArray()
        Me.cmdS101Reset = New System.Windows.Forms.Button()
        Me.cmdExerciseGroupDCPower = New System.Windows.Forms.Button()
        Me.tabOptions_Page2 = New System.Windows.Forms.TabPage()
        Me.cmdExerciseGroup2X24_2W_LF = New System.Windows.Forms.Button()
        Me.cmdS200ResetAll = New System.Windows.Forms.Button()
        Me.panS200_0 = New System.Windows.Forms.Panel()
        Me.lblS201Pin4 = New System.Windows.Forms.Label()
        Me.lblS201Pin2 = New System.Windows.Forms.Label()
        Me.lblS200_0 = New System.Windows.Forms.Label()
        Me.saS201 = New SwitchLib.ctl2W2RowArray()
        Me.lblS201Pin1 = New System.Windows.Forms.Label()
        Me.lblS201Pin3 = New System.Windows.Forms.Label()
        Me.panS200_1 = New System.Windows.Forms.Panel()
        Me.lblS202Pin4 = New System.Windows.Forms.Label()
        Me.lblS202Pin2 = New System.Windows.Forms.Label()
        Me.lblS200_1 = New System.Windows.Forms.Label()
        Me.saS202 = New SwitchLib.ctl2W2RowArray()
        Me.lblS202Pin1 = New System.Windows.Forms.Label()
        Me.lblS202Pin3 = New System.Windows.Forms.Label()
        Me.tabOptions_Page3 = New System.Windows.Forms.TabPage()
        Me.cmdExerciseGroup1x1_1W_LF = New System.Windows.Forms.Button()
        Me.cmdS301ResetAll = New System.Windows.Forms.Button()
        Me.panS301 = New System.Windows.Forms.Panel()
        Me.lblS301 = New System.Windows.Forms.Label()
        Me.saS301 = New SwitchLib.SPSTArray()
        Me.tabOptions_Page4 = New System.Windows.Forms.TabPage()
        Me.panS400_5 = New System.Windows.Forms.Panel()
        Me.lblS400Row_5 = New System.Windows.Forms.Label()
        Me.lblS400_5 = New System.Windows.Forms.Label()
        Me.saS406 = New SwitchLib.ctl1W1RowArray()
        Me.panS400_4 = New System.Windows.Forms.Panel()
        Me.lblS400Row_4 = New System.Windows.Forms.Label()
        Me.lblS400_4 = New System.Windows.Forms.Label()
        Me.saS405 = New SwitchLib.ctl1W1RowArray()
        Me.panS400_3 = New System.Windows.Forms.Panel()
        Me.lblS400Row_3 = New System.Windows.Forms.Label()
        Me.lblS400_3 = New System.Windows.Forms.Label()
        Me.saS404 = New SwitchLib.ctl1W1RowArray()
        Me.panS400_2 = New System.Windows.Forms.Panel()
        Me.lblS400Row_2 = New System.Windows.Forms.Label()
        Me.lblS400_2 = New System.Windows.Forms.Label()
        Me.saS403 = New SwitchLib.ctl1W1RowArray()
        Me.panS400_1 = New System.Windows.Forms.Panel()
        Me.lblS400Row_1 = New System.Windows.Forms.Label()
        Me.lblS400_1 = New System.Windows.Forms.Label()
        Me.saS402 = New SwitchLib.ctl1W1RowArray()
        Me.panS400_0 = New System.Windows.Forms.Panel()
        Me.lblS400_0 = New System.Windows.Forms.Label()
        Me.lblS400Row_0 = New System.Windows.Forms.Label()
        Me.saS401 = New SwitchLib.ctl1W1RowArray()
        Me.cmdS400ResetAll = New System.Windows.Forms.Button()
        Me.cmdExerciseGroup1x4_1W_LF = New System.Windows.Forms.Button()
        Me.tabOptions_Page5 = New System.Windows.Forms.TabPage()
        Me.panS500_9 = New System.Windows.Forms.Panel()
        Me.lblS500Row1_9 = New System.Windows.Forms.Label()
        Me.lblS500Row2_9 = New System.Windows.Forms.Label()
        Me.lblS500_9 = New System.Windows.Forms.Label()
        Me.saS510 = New SwitchLib.ctl1W2RowArray()
        Me.panS500_8 = New System.Windows.Forms.Panel()
        Me.lblS500Row1_8 = New System.Windows.Forms.Label()
        Me.lblS500Row2_8 = New System.Windows.Forms.Label()
        Me.lblS500_8 = New System.Windows.Forms.Label()
        Me.saS509 = New SwitchLib.ctl1W2RowArray()
        Me.panS500_7 = New System.Windows.Forms.Panel()
        Me.lblS500Row1_7 = New System.Windows.Forms.Label()
        Me.lblS500Row2_7 = New System.Windows.Forms.Label()
        Me.lblS500_7 = New System.Windows.Forms.Label()
        Me.saS508 = New SwitchLib.ctl1W2RowArray()
        Me.panS500_6 = New System.Windows.Forms.Panel()
        Me.lblS500Row1_6 = New System.Windows.Forms.Label()
        Me.lblS500Row2_6 = New System.Windows.Forms.Label()
        Me.lblS500_6 = New System.Windows.Forms.Label()
        Me.saS507 = New SwitchLib.ctl1W2RowArray()
        Me.panS500_5 = New System.Windows.Forms.Panel()
        Me.lblS500_5 = New System.Windows.Forms.Label()
        Me.lblS500Row2_5 = New System.Windows.Forms.Label()
        Me.lblS500Row1_5 = New System.Windows.Forms.Label()
        Me.saS506 = New SwitchLib.ctl1W2RowArray()
        Me.panS500_4 = New System.Windows.Forms.Panel()
        Me.lblS500_4 = New System.Windows.Forms.Label()
        Me.lblS500Row2_4 = New System.Windows.Forms.Label()
        Me.lblS500Row1_4 = New System.Windows.Forms.Label()
        Me.saS505 = New SwitchLib.ctl1W2RowArray()
        Me.panS500_3 = New System.Windows.Forms.Panel()
        Me.lblS500_3 = New System.Windows.Forms.Label()
        Me.lblS500Row2_3 = New System.Windows.Forms.Label()
        Me.lblS500Row1_3 = New System.Windows.Forms.Label()
        Me.saS504 = New SwitchLib.ctl1W2RowArray()
        Me.panS500_2 = New System.Windows.Forms.Panel()
        Me.lblS500_2 = New System.Windows.Forms.Label()
        Me.lblS500Row2_2 = New System.Windows.Forms.Label()
        Me.lblS500Row1_2 = New System.Windows.Forms.Label()
        Me.saS503 = New SwitchLib.ctl1W2RowArray()
        Me.panS500_1 = New System.Windows.Forms.Panel()
        Me.lblS500_1 = New System.Windows.Forms.Label()
        Me.lblS500Row2_1 = New System.Windows.Forms.Label()
        Me.lblS500Row1_1 = New System.Windows.Forms.Label()
        Me.saS502 = New SwitchLib.ctl1W2RowArray()
        Me.panS500_0 = New System.Windows.Forms.Panel()
        Me.lblS500Row1_0 = New System.Windows.Forms.Label()
        Me.lblS500Row2_0 = New System.Windows.Forms.Label()
        Me.lblS500_0 = New System.Windows.Forms.Label()
        Me.saS501 = New SwitchLib.ctl1W2RowArray()
        Me.cmdS500ResetAll = New System.Windows.Forms.Button()
        Me.cmdExerciseGroup2x8_1W_LF = New System.Windows.Forms.Button()
        Me.tabOptions_Page6 = New System.Windows.Forms.TabPage()
        Me.cmdExerciseGroup1x2_1W_LF = New System.Windows.Forms.Button()
        Me.cmdS600ResetAll = New System.Windows.Forms.Button()
        Me.panS600_0 = New System.Windows.Forms.Panel()
        Me.lblS600Row_0 = New System.Windows.Forms.Label()
        Me.lblS600_0 = New System.Windows.Forms.Label()
        Me.saS601 = New SwitchLib.ctl1W1RowArray()
        Me.panS600_1 = New System.Windows.Forms.Panel()
        Me.lblS600_1 = New System.Windows.Forms.Label()
        Me.lblS600Row_1 = New System.Windows.Forms.Label()
        Me.saS602 = New SwitchLib.ctl1W1RowArray()
        Me.panS600_2 = New System.Windows.Forms.Panel()
        Me.lblS600_2 = New System.Windows.Forms.Label()
        Me.lblS600Row_2 = New System.Windows.Forms.Label()
        Me.saS603 = New SwitchLib.ctl1W1RowArray()
        Me.panS600_3 = New System.Windows.Forms.Panel()
        Me.lblS600_3 = New System.Windows.Forms.Label()
        Me.lblS600Row_3 = New System.Windows.Forms.Label()
        Me.saS604 = New SwitchLib.ctl1W1RowArray()
        Me.panS600_4 = New System.Windows.Forms.Panel()
        Me.lblS600_4 = New System.Windows.Forms.Label()
        Me.lblS600Row_4 = New System.Windows.Forms.Label()
        Me.saS605 = New SwitchLib.ctl1W1RowArray()
        Me.panS600_5 = New System.Windows.Forms.Panel()
        Me.lblS600_5 = New System.Windows.Forms.Label()
        Me.lblS600Row_5 = New System.Windows.Forms.Label()
        Me.saS606 = New SwitchLib.ctl1W1RowArray()
        Me.panS600_6 = New System.Windows.Forms.Panel()
        Me.lblS600_6 = New System.Windows.Forms.Label()
        Me.lblS600Row_6 = New System.Windows.Forms.Label()
        Me.saS607 = New SwitchLib.ctl1W1RowArray()
        Me.panS600_7 = New System.Windows.Forms.Panel()
        Me.lblS600Row_7 = New System.Windows.Forms.Label()
        Me.lblS600_7 = New System.Windows.Forms.Label()
        Me.saS608 = New SwitchLib.ctl1W1RowArray()
        Me.panS600_8 = New System.Windows.Forms.Panel()
        Me.lblS600Row_8 = New System.Windows.Forms.Label()
        Me.lblS600_8 = New System.Windows.Forms.Label()
        Me.saS609 = New SwitchLib.ctl1W1RowArray()
        Me.panS600_9 = New System.Windows.Forms.Panel()
        Me.lblS600Row_9 = New System.Windows.Forms.Label()
        Me.lblS600_9 = New System.Windows.Forms.Label()
        Me.saS610 = New SwitchLib.ctl1W1RowArray()
        Me.panS600_10 = New System.Windows.Forms.Panel()
        Me.lblS600Row_10 = New System.Windows.Forms.Label()
        Me.lblS600_10 = New System.Windows.Forms.Label()
        Me.saS611 = New SwitchLib.ctl1W1RowArray()
        Me.panS600_11 = New System.Windows.Forms.Panel()
        Me.lblS600Row_11 = New System.Windows.Forms.Label()
        Me.lblS600_11 = New System.Windows.Forms.Label()
        Me.saS612 = New SwitchLib.ctl1W1RowArray()
        Me.tabOptions_Page7 = New System.Windows.Forms.TabPage()
        Me.cmdExerciseGroup1x24_1x8_2W_LF = New System.Windows.Forms.Button()
        Me.cmdS700ResetAll = New System.Windows.Forms.Button()
        Me.panS701 = New System.Windows.Forms.Panel()
        Me.lblS701Row2 = New System.Windows.Forms.Label()
        Me.lblS701 = New System.Windows.Forms.Label()
        Me.lblS701Row1 = New System.Windows.Forms.Label()
        Me.saS701 = New SwitchLib.ctl2W1RowArray()
        Me.panS751 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblS751Row = New System.Windows.Forms.Label()
        Me.lblS751 = New System.Windows.Forms.Label()
        Me.saS751 = New SwitchLib.ctl2W1RowArray()
        Me.tabOptions_Page8 = New System.Windows.Forms.TabPage()
        Me.cmdExerciseGroup1x8_Coax_MF = New System.Windows.Forms.Button()
        Me.cmdS800ResetAll = New System.Windows.Forms.Button()
        Me.panS800_0 = New System.Windows.Forms.Panel()
        Me.lblS800_0 = New System.Windows.Forms.Label()
        Me.lblS800Pin1_0 = New System.Windows.Forms.Label()
        Me.saS801 = New SwitchLib.ctl1x8Mux()
        Me.panS800_2 = New System.Windows.Forms.Panel()
        Me.lblS800_2 = New System.Windows.Forms.Label()
        Me.lblS800Pin1_2 = New System.Windows.Forms.Label()
        Me.saS803 = New SwitchLib.ctl1x8Mux()
        Me.panS800_3 = New System.Windows.Forms.Panel()
        Me.lblS800_3 = New System.Windows.Forms.Label()
        Me.lblS800Pin1_3 = New System.Windows.Forms.Label()
        Me.saS804 = New SwitchLib.ctl1x8Mux()
        Me.panS800_1 = New System.Windows.Forms.Panel()
        Me.lblS800_1 = New System.Windows.Forms.Label()
        Me.lblS800Pin1_1 = New System.Windows.Forms.Label()
        Me.saS802 = New SwitchLib.ctl1x8Mux()
        Me.tabOptions_Page9 = New System.Windows.Forms.TabPage()
        Me.panS900_3 = New System.Windows.Forms.Panel()
        Me.lblS900_3 = New System.Windows.Forms.Label()
        Me.lblS900Pin1_3 = New System.Windows.Forms.Label()
        Me.saS904 = New SwitchLib.ctl1x6Mux()
        Me.cmdExerciseGroup1x6_Coax_HF = New System.Windows.Forms.Button()
        Me.cmdS900ResetAll = New System.Windows.Forms.Button()
        Me.panS900_0 = New System.Windows.Forms.Panel()
        Me.lblS900_0 = New System.Windows.Forms.Label()
        Me.lblS900Pin1_0 = New System.Windows.Forms.Label()
        Me.saS901 = New SwitchLib.ctl1x6Mux()
        Me.panS900_1 = New System.Windows.Forms.Panel()
        Me.lblS900_1 = New System.Windows.Forms.Label()
        Me.lblS900Pin1_1 = New System.Windows.Forms.Label()
        Me.saS902 = New SwitchLib.ctl1x6Mux()
        Me.panS900_2 = New System.Windows.Forms.Panel()
        Me.lblS900_2 = New System.Windows.Forms.Label()
        Me.lblS900Pin1_2 = New System.Windows.Forms.Label()
        Me.saS903 = New SwitchLib.ctl1x6Mux()
        Me.panS900_4 = New System.Windows.Forms.Panel()
        Me.lblS900_4 = New System.Windows.Forms.Label()
        Me.lblS900Pin1_4 = New System.Windows.Forms.Label()
        Me.saS905 = New SwitchLib.ctl1x6Mux()
        Me.panS900_5 = New System.Windows.Forms.Panel()
        Me.lblS900_5 = New System.Windows.Forms.Label()
        Me.lblS900Pin1_5 = New System.Windows.Forms.Label()
        Me.saS906 = New SwitchLib.ctl1x6Mux()
        Me.tabOptions_Page10 = New System.Windows.Forms.TabPage()
        Me.fraPinList = New System.Windows.Forms.GroupBox()
        Me.panSortSig = New System.Windows.Forms.Panel()
        Me.grdSortSig = New System.Windows.Forms.DataGridView()
        Me.dgvSwitch = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgvReceiver = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lblSortSigCnx = New System.Windows.Forms.Label()
        Me.lblSortSigSig = New System.Windows.Forms.Label()
        Me.panSortCnx = New System.Windows.Forms.Panel()
        Me.grdSortCnx = New System.Windows.Forms.DataGridView()
        Me.cvxReceiver = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cvxSwitch = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cmdAbout = New System.Windows.Forms.Button()
        Me.cmdTest = New System.Windows.Forms.Button()
        Me.fraRelayExerciser = New System.Windows.Forms.GroupBox()
        Me.cmdExercise = New System.Windows.Forms.Button()
        Me.cboCardType = New System.Windows.Forms.ComboBox()
        Me.cmdOpenAll = New System.Windows.Forms.Button()
        Me.Panel_Conifg = New VIPERT_Common_Controls.Panel_Conifg()
        Me.tabOptions_Page11 = New System.Windows.Forms.TabPage()
        Me.Atlas_SFP = New VIPERT_Common_Controls.Atlas_SFP()
        Me.sbrUserInformation = New System.Windows.Forms.StatusBar()
        Me.sbrUserInformation_Panel1 = New System.Windows.Forms.StatusBarPanel()
        Me.sbrUserInformation_Panel2 = New System.Windows.Forms.StatusBarPanel()
        Me.pcp1x6Mux = New System.Windows.Forms.PictureBox()
        Me.lblDdeLink = New System.Windows.Forms.Label()
        Me.tabOptions.SuspendLayout()
        Me.tabOptions_Page1.SuspendLayout()
        Me.panS101.SuspendLayout()
        Me.tabOptions_Page2.SuspendLayout()
        Me.panS200_0.SuspendLayout()
        Me.panS200_1.SuspendLayout()
        Me.tabOptions_Page3.SuspendLayout()
        Me.panS301.SuspendLayout()
        Me.tabOptions_Page4.SuspendLayout()
        Me.panS400_5.SuspendLayout()
        Me.panS400_4.SuspendLayout()
        Me.panS400_3.SuspendLayout()
        Me.panS400_2.SuspendLayout()
        Me.panS400_1.SuspendLayout()
        Me.panS400_0.SuspendLayout()
        Me.tabOptions_Page5.SuspendLayout()
        Me.panS500_9.SuspendLayout()
        Me.panS500_8.SuspendLayout()
        Me.panS500_7.SuspendLayout()
        Me.panS500_6.SuspendLayout()
        Me.panS500_5.SuspendLayout()
        Me.panS500_4.SuspendLayout()
        Me.panS500_3.SuspendLayout()
        Me.panS500_2.SuspendLayout()
        Me.panS500_1.SuspendLayout()
        Me.panS500_0.SuspendLayout()
        Me.tabOptions_Page6.SuspendLayout()
        Me.panS600_0.SuspendLayout()
        Me.panS600_1.SuspendLayout()
        Me.panS600_2.SuspendLayout()
        Me.panS600_3.SuspendLayout()
        Me.panS600_4.SuspendLayout()
        Me.panS600_5.SuspendLayout()
        Me.panS600_6.SuspendLayout()
        Me.panS600_7.SuspendLayout()
        Me.panS600_8.SuspendLayout()
        Me.panS600_9.SuspendLayout()
        Me.panS600_10.SuspendLayout()
        Me.panS600_11.SuspendLayout()
        Me.tabOptions_Page7.SuspendLayout()
        Me.panS701.SuspendLayout()
        Me.panS751.SuspendLayout()
        Me.tabOptions_Page8.SuspendLayout()
        Me.panS800_0.SuspendLayout()
        Me.panS800_2.SuspendLayout()
        Me.panS800_3.SuspendLayout()
        Me.panS800_1.SuspendLayout()
        Me.tabOptions_Page9.SuspendLayout()
        Me.panS900_3.SuspendLayout()
        Me.panS900_0.SuspendLayout()
        Me.panS900_1.SuspendLayout()
        Me.panS900_2.SuspendLayout()
        Me.panS900_4.SuspendLayout()
        Me.panS900_5.SuspendLayout()
        Me.tabOptions_Page10.SuspendLayout()
        Me.fraPinList.SuspendLayout()
        Me.panSortSig.SuspendLayout()
        CType(Me.grdSortSig, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panSortCnx.SuspendLayout()
        CType(Me.grdSortCnx, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraRelayExerciser.SuspendLayout()
        Me.tabOptions_Page11.SuspendLayout()
        CType(Me.sbrUserInformation_Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sbrUserInformation_Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pcp1x6Mux, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdHelp
        '
        Me.cmdHelp.BackColor = System.Drawing.SystemColors.Control
        Me.cmdHelp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdHelp.Location = New System.Drawing.Point(340, 443)
        Me.cmdHelp.Name = "cmdHelp"
        Me.cmdHelp.Size = New System.Drawing.Size(76, 23)
        Me.cmdHelp.TabIndex = 211
        Me.cmdHelp.Text = "Help"
        Me.cmdHelp.UseVisualStyleBackColor = False
        '
        'cmdQuit
        '
        Me.cmdQuit.BackColor = System.Drawing.SystemColors.Control
        Me.cmdQuit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdQuit.Location = New System.Drawing.Point(526, 443)
        Me.cmdQuit.Name = "cmdQuit"
        Me.cmdQuit.Size = New System.Drawing.Size(76, 23)
        Me.cmdQuit.TabIndex = 9
        Me.cmdQuit.Text = "&Quit"
        Me.cmdQuit.UseVisualStyleBackColor = False
        '
        'cmdReset
        '
        Me.cmdReset.BackColor = System.Drawing.SystemColors.Control
        Me.cmdReset.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdReset.Location = New System.Drawing.Point(429, 443)
        Me.cmdReset.Name = "cmdReset"
        Me.cmdReset.Size = New System.Drawing.Size(76, 23)
        Me.cmdReset.TabIndex = 8
        Me.cmdReset.Text = "&Reset"
        Me.cmdReset.UseVisualStyleBackColor = False
        '
        'tabOptions
        '
        Me.tabOptions.Controls.Add(Me.tabOptions_Page1)
        Me.tabOptions.Controls.Add(Me.tabOptions_Page2)
        Me.tabOptions.Controls.Add(Me.tabOptions_Page3)
        Me.tabOptions.Controls.Add(Me.tabOptions_Page4)
        Me.tabOptions.Controls.Add(Me.tabOptions_Page5)
        Me.tabOptions.Controls.Add(Me.tabOptions_Page6)
        Me.tabOptions.Controls.Add(Me.tabOptions_Page7)
        Me.tabOptions.Controls.Add(Me.tabOptions_Page8)
        Me.tabOptions.Controls.Add(Me.tabOptions_Page9)
        Me.tabOptions.Controls.Add(Me.tabOptions_Page10)
        Me.tabOptions.Controls.Add(Me.tabOptions_Page11)
        Me.tabOptions.Location = New System.Drawing.Point(8, 8)
        Me.tabOptions.Multiline = True
        Me.tabOptions.Name = "tabOptions"
        Me.tabOptions.SelectedIndex = 0
        Me.tabOptions.Size = New System.Drawing.Size(601, 424)
        Me.tabOptions.TabIndex = 1
        '
        'tabOptions_Page1
        '
        Me.tabOptions_Page1.BackColor = System.Drawing.Color.White
        Me.tabOptions_Page1.Controls.Add(Me.panS101)
        Me.tabOptions_Page1.Controls.Add(Me.cmdS101Reset)
        Me.tabOptions_Page1.Controls.Add(Me.cmdExerciseGroupDCPower)
        Me.tabOptions_Page1.Location = New System.Drawing.Point(4, 40)
        Me.tabOptions_Page1.Name = "tabOptions_Page1"
        Me.tabOptions_Page1.Size = New System.Drawing.Size(593, 380)
        Me.tabOptions_Page1.TabIndex = 0
        Me.tabOptions_Page1.Text = " DC Power     "
        '
        'panS101
        '
        Me.panS101.BackColor = System.Drawing.Color.Silver
        Me.panS101.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panS101.Controls.Add(Me.lblS101)
        Me.panS101.Controls.Add(Me.saS101)
        Me.panS101.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panS101.Location = New System.Drawing.Point(8, 28)
        Me.panS101.Margin = New System.Windows.Forms.Padding(0)
        Me.panS101.Name = "panS101"
        Me.panS101.Size = New System.Drawing.Size(201, 204)
        Me.panS101.TabIndex = 2
        '
        'lblS101
        '
        Me.lblS101.AutoSize = True
        Me.lblS101.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblS101.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblS101.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblS101.Location = New System.Drawing.Point(1, 1)
        Me.lblS101.Name = "lblS101"
        Me.lblS101.Size = New System.Drawing.Size(32, 13)
        Me.lblS101.TabIndex = 146
        Me.lblS101.Text = "S101"
        Me.lblS101.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'saS101
        '
        Me.saS101.BackColor = System.Drawing.Color.Silver
        Me.saS101.Columns = 2
        Me.saS101.Location = New System.Drawing.Point(0, 16)
        Me.saS101.Margin = New System.Windows.Forms.Padding(2)
        Me.saS101.Name = "saS101"
        Me.saS101.Rows = 5
        Me.saS101.Size = New System.Drawing.Size(196, 180)
        Me.saS101.TabIndex = 147
        '
        'cmdS101Reset
        '
        Me.cmdS101Reset.BackColor = System.Drawing.SystemColors.Control
        Me.cmdS101Reset.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdS101Reset.Location = New System.Drawing.Point(10, 274)
        Me.cmdS101Reset.Name = "cmdS101Reset"
        Me.cmdS101Reset.Size = New System.Drawing.Size(103, 23)
        Me.cmdS101Reset.TabIndex = 12
        Me.cmdS101Reset.Text = "&Open Group"
        Me.cmdS101Reset.UseVisualStyleBackColor = False
        '
        'cmdExerciseGroupDCPower
        '
        Me.cmdExerciseGroupDCPower.BackColor = System.Drawing.SystemColors.Control
        Me.cmdExerciseGroupDCPower.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdExerciseGroupDCPower.Location = New System.Drawing.Point(119, 274)
        Me.cmdExerciseGroupDCPower.Name = "cmdExerciseGroupDCPower"
        Me.cmdExerciseGroupDCPower.Size = New System.Drawing.Size(117, 23)
        Me.cmdExerciseGroupDCPower.TabIndex = 216
        Me.cmdExerciseGroupDCPower.Text = "&Exercise Group"
        Me.cmdExerciseGroupDCPower.UseVisualStyleBackColor = False
        '
        'tabOptions_Page2
        '
        Me.tabOptions_Page2.Controls.Add(Me.cmdExerciseGroup2X24_2W_LF)
        Me.tabOptions_Page2.Controls.Add(Me.cmdS200ResetAll)
        Me.tabOptions_Page2.Controls.Add(Me.panS200_0)
        Me.tabOptions_Page2.Controls.Add(Me.panS200_1)
        Me.tabOptions_Page2.Location = New System.Drawing.Point(4, 40)
        Me.tabOptions_Page2.Name = "tabOptions_Page2"
        Me.tabOptions_Page2.Size = New System.Drawing.Size(593, 380)
        Me.tabOptions_Page2.TabIndex = 1
        Me.tabOptions_Page2.Text = " 2x24-2W-LF     "
        '
        'cmdExerciseGroup2X24_2W_LF
        '
        Me.cmdExerciseGroup2X24_2W_LF.BackColor = System.Drawing.SystemColors.Control
        Me.cmdExerciseGroup2X24_2W_LF.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdExerciseGroup2X24_2W_LF.Location = New System.Drawing.Point(111, 340)
        Me.cmdExerciseGroup2X24_2W_LF.Name = "cmdExerciseGroup2X24_2W_LF"
        Me.cmdExerciseGroup2X24_2W_LF.Size = New System.Drawing.Size(118, 23)
        Me.cmdExerciseGroup2X24_2W_LF.TabIndex = 217
        Me.cmdExerciseGroup2X24_2W_LF.Text = "&Exercise Group"
        Me.cmdExerciseGroup2X24_2W_LF.UseVisualStyleBackColor = False
        '
        'cmdS200ResetAll
        '
        Me.cmdS200ResetAll.BackColor = System.Drawing.SystemColors.Control
        Me.cmdS200ResetAll.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdS200ResetAll.Location = New System.Drawing.Point(8, 340)
        Me.cmdS200ResetAll.Name = "cmdS200ResetAll"
        Me.cmdS200ResetAll.Size = New System.Drawing.Size(97, 23)
        Me.cmdS200ResetAll.TabIndex = 13
        Me.cmdS200ResetAll.Text = "&Open Group"
        Me.cmdS200ResetAll.UseVisualStyleBackColor = False
        '
        'panS200_0
        '
        Me.panS200_0.AutoScroll = True
        Me.panS200_0.BackColor = System.Drawing.Color.Silver
        Me.panS200_0.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panS200_0.Controls.Add(Me.lblS201Pin4)
        Me.panS200_0.Controls.Add(Me.lblS201Pin2)
        Me.panS200_0.Controls.Add(Me.lblS200_0)
        Me.panS200_0.Controls.Add(Me.saS201)
        Me.panS200_0.Controls.Add(Me.lblS201Pin1)
        Me.panS200_0.Controls.Add(Me.lblS201Pin3)
        Me.panS200_0.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panS200_0.Location = New System.Drawing.Point(8, 28)
        Me.panS200_0.Name = "panS200_0"
        Me.panS200_0.Size = New System.Drawing.Size(556, 118)
        Me.panS200_0.TabIndex = 4
        '
        'lblS201Pin4
        '
        Me.lblS201Pin4.BackColor = System.Drawing.Color.Silver
        Me.lblS201Pin4.Font = New System.Drawing.Font("Microsoft Sans Serif", 5.0!)
        Me.lblS201Pin4.Location = New System.Drawing.Point(23, 73)
        Me.lblS201Pin4.Name = "lblS201Pin4"
        Me.lblS201Pin4.Size = New System.Drawing.Size(15, 15)
        Me.lblS201Pin4.TabIndex = 150
        Me.lblS201Pin4.Text = "4"
        Me.lblS201Pin4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblS201Pin2
        '
        Me.lblS201Pin2.BackColor = System.Drawing.Color.Silver
        Me.lblS201Pin2.Font = New System.Drawing.Font("Microsoft Sans Serif", 5.0!)
        Me.lblS201Pin2.Location = New System.Drawing.Point(23, 36)
        Me.lblS201Pin2.Name = "lblS201Pin2"
        Me.lblS201Pin2.Size = New System.Drawing.Size(15, 15)
        Me.lblS201Pin2.TabIndex = 149
        Me.lblS201Pin2.Text = "2"
        Me.lblS201Pin2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblS200_0
        '
        Me.lblS200_0.AutoSize = True
        Me.lblS200_0.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblS200_0.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblS200_0.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblS200_0.Location = New System.Drawing.Point(1, 1)
        Me.lblS200_0.Name = "lblS200_0"
        Me.lblS200_0.Size = New System.Drawing.Size(32, 13)
        Me.lblS200_0.TabIndex = 147
        Me.lblS200_0.Text = "S201"
        Me.lblS200_0.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'saS201
        '
        Me.saS201.Count = 24
        Me.saS201.Location = New System.Drawing.Point(40, 2)
        Me.saS201.Margin = New System.Windows.Forms.Padding(2)
        Me.saS201.Name = "saS201"
        Me.saS201.Size = New System.Drawing.Size(864, 86)
        Me.saS201.TabIndex = 148
        '
        'lblS201Pin1
        '
        Me.lblS201Pin1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblS201Pin1.Font = New System.Drawing.Font("Microsoft Sans Serif", 5.0!)
        Me.lblS201Pin1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblS201Pin1.Location = New System.Drawing.Point(23, 20)
        Me.lblS201Pin1.Name = "lblS201Pin1"
        Me.lblS201Pin1.Size = New System.Drawing.Size(15, 15)
        Me.lblS201Pin1.TabIndex = 6
        Me.lblS201Pin1.Text = "1"
        Me.lblS201Pin1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblS201Pin3
        '
        Me.lblS201Pin3.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblS201Pin3.Font = New System.Drawing.Font("Microsoft Sans Serif", 5.0!)
        Me.lblS201Pin3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblS201Pin3.Location = New System.Drawing.Point(23, 59)
        Me.lblS201Pin3.Name = "lblS201Pin3"
        Me.lblS201Pin3.Size = New System.Drawing.Size(15, 15)
        Me.lblS201Pin3.TabIndex = 7
        Me.lblS201Pin3.Text = "3"
        Me.lblS201Pin3.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'panS200_1
        '
        Me.panS200_1.AutoScroll = True
        Me.panS200_1.BackColor = System.Drawing.Color.Silver
        Me.panS200_1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panS200_1.Controls.Add(Me.lblS202Pin4)
        Me.panS200_1.Controls.Add(Me.lblS202Pin2)
        Me.panS200_1.Controls.Add(Me.lblS200_1)
        Me.panS200_1.Controls.Add(Me.saS202)
        Me.panS200_1.Controls.Add(Me.lblS202Pin1)
        Me.panS200_1.Controls.Add(Me.lblS202Pin3)
        Me.panS200_1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panS200_1.Location = New System.Drawing.Point(8, 181)
        Me.panS200_1.Name = "panS200_1"
        Me.panS200_1.Size = New System.Drawing.Size(556, 118)
        Me.panS200_1.TabIndex = 148
        '
        'lblS202Pin4
        '
        Me.lblS202Pin4.BackColor = System.Drawing.Color.Silver
        Me.lblS202Pin4.Font = New System.Drawing.Font("Microsoft Sans Serif", 5.0!)
        Me.lblS202Pin4.Location = New System.Drawing.Point(23, 71)
        Me.lblS202Pin4.Name = "lblS202Pin4"
        Me.lblS202Pin4.Size = New System.Drawing.Size(15, 15)
        Me.lblS202Pin4.TabIndex = 153
        Me.lblS202Pin4.Text = "4"
        Me.lblS202Pin4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblS202Pin2
        '
        Me.lblS202Pin2.BackColor = System.Drawing.Color.Silver
        Me.lblS202Pin2.Font = New System.Drawing.Font("Microsoft Sans Serif", 5.0!)
        Me.lblS202Pin2.Location = New System.Drawing.Point(23, 37)
        Me.lblS202Pin2.Name = "lblS202Pin2"
        Me.lblS202Pin2.Size = New System.Drawing.Size(15, 15)
        Me.lblS202Pin2.TabIndex = 152
        Me.lblS202Pin2.Text = "2"
        Me.lblS202Pin2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblS200_1
        '
        Me.lblS200_1.AutoSize = True
        Me.lblS200_1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblS200_1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblS200_1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblS200_1.Location = New System.Drawing.Point(1, 1)
        Me.lblS200_1.Name = "lblS200_1"
        Me.lblS200_1.Size = New System.Drawing.Size(32, 13)
        Me.lblS200_1.TabIndex = 149
        Me.lblS200_1.Text = "S202"
        Me.lblS200_1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'saS202
        '
        Me.saS202.Count = 24
        Me.saS202.Location = New System.Drawing.Point(40, 2)
        Me.saS202.Margin = New System.Windows.Forms.Padding(2)
        Me.saS202.Name = "saS202"
        Me.saS202.Size = New System.Drawing.Size(864, 86)
        Me.saS202.TabIndex = 150
        '
        'lblS202Pin1
        '
        Me.lblS202Pin1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblS202Pin1.Font = New System.Drawing.Font("Microsoft Sans Serif", 5.0!)
        Me.lblS202Pin1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblS202Pin1.Location = New System.Drawing.Point(23, 21)
        Me.lblS202Pin1.Name = "lblS202Pin1"
        Me.lblS202Pin1.Size = New System.Drawing.Size(15, 15)
        Me.lblS202Pin1.TabIndex = 150
        Me.lblS202Pin1.Text = "1"
        Me.lblS202Pin1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblS202Pin3
        '
        Me.lblS202Pin3.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblS202Pin3.Font = New System.Drawing.Font("Microsoft Sans Serif", 5.0!)
        Me.lblS202Pin3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblS202Pin3.Location = New System.Drawing.Point(23, 57)
        Me.lblS202Pin3.Name = "lblS202Pin3"
        Me.lblS202Pin3.Size = New System.Drawing.Size(15, 15)
        Me.lblS202Pin3.TabIndex = 151
        Me.lblS202Pin3.Text = "3"
        Me.lblS202Pin3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'tabOptions_Page3
        '
        Me.tabOptions_Page3.Controls.Add(Me.cmdExerciseGroup1x1_1W_LF)
        Me.tabOptions_Page3.Controls.Add(Me.cmdS301ResetAll)
        Me.tabOptions_Page3.Controls.Add(Me.panS301)
        Me.tabOptions_Page3.Location = New System.Drawing.Point(4, 40)
        Me.tabOptions_Page3.Name = "tabOptions_Page3"
        Me.tabOptions_Page3.Size = New System.Drawing.Size(593, 380)
        Me.tabOptions_Page3.TabIndex = 2
        Me.tabOptions_Page3.Text = " 1x1-1W-LF     "
        '
        'cmdExerciseGroup1x1_1W_LF
        '
        Me.cmdExerciseGroup1x1_1W_LF.BackColor = System.Drawing.SystemColors.Control
        Me.cmdExerciseGroup1x1_1W_LF.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdExerciseGroup1x1_1W_LF.Location = New System.Drawing.Point(118, 335)
        Me.cmdExerciseGroup1x1_1W_LF.Name = "cmdExerciseGroup1x1_1W_LF"
        Me.cmdExerciseGroup1x1_1W_LF.Size = New System.Drawing.Size(127, 23)
        Me.cmdExerciseGroup1x1_1W_LF.TabIndex = 218
        Me.cmdExerciseGroup1x1_1W_LF.Text = "&Exercise Group"
        Me.cmdExerciseGroup1x1_1W_LF.UseVisualStyleBackColor = False
        '
        'cmdS301ResetAll
        '
        Me.cmdS301ResetAll.BackColor = System.Drawing.SystemColors.Control
        Me.cmdS301ResetAll.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdS301ResetAll.Location = New System.Drawing.Point(11, 335)
        Me.cmdS301ResetAll.Name = "cmdS301ResetAll"
        Me.cmdS301ResetAll.Size = New System.Drawing.Size(101, 23)
        Me.cmdS301ResetAll.TabIndex = 15
        Me.cmdS301ResetAll.Text = "&Open Group"
        Me.cmdS301ResetAll.UseVisualStyleBackColor = False
        '
        'panS301
        '
        Me.panS301.BackColor = System.Drawing.Color.Silver
        Me.panS301.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panS301.Controls.Add(Me.lblS301)
        Me.panS301.Controls.Add(Me.saS301)
        Me.panS301.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panS301.Location = New System.Drawing.Point(8, 10)
        Me.panS301.Name = "panS301"
        Me.panS301.Size = New System.Drawing.Size(574, 314)
        Me.panS301.TabIndex = 14
        '
        'lblS301
        '
        Me.lblS301.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblS301.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblS301.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblS301.Location = New System.Drawing.Point(1, 1)
        Me.lblS301.Name = "lblS301"
        Me.lblS301.Size = New System.Drawing.Size(41, 19)
        Me.lblS301.TabIndex = 153
        Me.lblS301.Text = "S301"
        Me.lblS301.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'saS301
        '
        Me.saS301.Columns = 6
        Me.saS301.Location = New System.Drawing.Point(2, 19)
        Me.saS301.Margin = New System.Windows.Forms.Padding(2)
        Me.saS301.Name = "saS301"
        Me.saS301.Rows = 16
        Me.saS301.Size = New System.Drawing.Size(564, 288)
        Me.saS301.TabIndex = 154
        '
        'tabOptions_Page4
        '
        Me.tabOptions_Page4.Controls.Add(Me.panS400_5)
        Me.tabOptions_Page4.Controls.Add(Me.panS400_4)
        Me.tabOptions_Page4.Controls.Add(Me.panS400_3)
        Me.tabOptions_Page4.Controls.Add(Me.panS400_2)
        Me.tabOptions_Page4.Controls.Add(Me.panS400_1)
        Me.tabOptions_Page4.Controls.Add(Me.panS400_0)
        Me.tabOptions_Page4.Controls.Add(Me.cmdS400ResetAll)
        Me.tabOptions_Page4.Controls.Add(Me.cmdExerciseGroup1x4_1W_LF)
        Me.tabOptions_Page4.Location = New System.Drawing.Point(4, 40)
        Me.tabOptions_Page4.Name = "tabOptions_Page4"
        Me.tabOptions_Page4.Size = New System.Drawing.Size(593, 380)
        Me.tabOptions_Page4.TabIndex = 3
        Me.tabOptions_Page4.Text = " 1x4-1W-LF     "
        '
        'panS400_5
        '
        Me.panS400_5.BackColor = System.Drawing.Color.Silver
        Me.panS400_5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panS400_5.Controls.Add(Me.lblS400Row_5)
        Me.panS400_5.Controls.Add(Me.lblS400_5)
        Me.panS400_5.Controls.Add(Me.saS406)
        Me.panS400_5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panS400_5.Location = New System.Drawing.Point(14, 283)
        Me.panS400_5.Name = "panS400_5"
        Me.panS400_5.Size = New System.Drawing.Size(122, 40)
        Me.panS400_5.TabIndex = 88
        '
        'lblS400Row_5
        '
        Me.lblS400Row_5.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblS400Row_5.Font = New System.Drawing.Font("Microsoft Sans Serif", 5.0!)
        Me.lblS400Row_5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblS400Row_5.Location = New System.Drawing.Point(23, 21)
        Me.lblS400Row_5.Name = "lblS400Row_5"
        Me.lblS400Row_5.Size = New System.Drawing.Size(19, 13)
        Me.lblS400Row_5.TabIndex = 91
        Me.lblS400Row_5.Text = "1"
        Me.lblS400Row_5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblS400_5
        '
        Me.lblS400_5.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblS400_5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblS400_5.Location = New System.Drawing.Point(1, 1)
        Me.lblS400_5.Name = "lblS400_5"
        Me.lblS400_5.Size = New System.Drawing.Size(42, 15)
        Me.lblS400_5.TabIndex = 89
        Me.lblS400_5.Text = "S406"
        Me.lblS400_5.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'saS406
        '
        Me.saS406.Count = 4
        Me.saS406.Location = New System.Drawing.Point(42, 2)
        Me.saS406.Margin = New System.Windows.Forms.Padding(2)
        Me.saS406.Name = "saS406"
        Me.saS406.Size = New System.Drawing.Size(76, 36)
        Me.saS406.TabIndex = 92
        '
        'panS400_4
        '
        Me.panS400_4.BackColor = System.Drawing.Color.Silver
        Me.panS400_4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panS400_4.Controls.Add(Me.lblS400Row_4)
        Me.panS400_4.Controls.Add(Me.lblS400_4)
        Me.panS400_4.Controls.Add(Me.saS405)
        Me.panS400_4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panS400_4.Location = New System.Drawing.Point(14, 232)
        Me.panS400_4.Name = "panS400_4"
        Me.panS400_4.Size = New System.Drawing.Size(122, 40)
        Me.panS400_4.TabIndex = 84
        '
        'lblS400Row_4
        '
        Me.lblS400Row_4.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblS400Row_4.Font = New System.Drawing.Font("Microsoft Sans Serif", 5.0!)
        Me.lblS400Row_4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblS400Row_4.Location = New System.Drawing.Point(23, 21)
        Me.lblS400Row_4.Name = "lblS400Row_4"
        Me.lblS400Row_4.Size = New System.Drawing.Size(19, 13)
        Me.lblS400Row_4.TabIndex = 87
        Me.lblS400Row_4.Text = "1"
        Me.lblS400Row_4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblS400_4
        '
        Me.lblS400_4.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblS400_4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblS400_4.Location = New System.Drawing.Point(1, 1)
        Me.lblS400_4.Name = "lblS400_4"
        Me.lblS400_4.Size = New System.Drawing.Size(42, 15)
        Me.lblS400_4.TabIndex = 85
        Me.lblS400_4.Text = "S405"
        Me.lblS400_4.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'saS405
        '
        Me.saS405.Count = 4
        Me.saS405.Location = New System.Drawing.Point(42, 2)
        Me.saS405.Margin = New System.Windows.Forms.Padding(2)
        Me.saS405.Name = "saS405"
        Me.saS405.Size = New System.Drawing.Size(76, 36)
        Me.saS405.TabIndex = 88
        '
        'panS400_3
        '
        Me.panS400_3.BackColor = System.Drawing.Color.Silver
        Me.panS400_3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panS400_3.Controls.Add(Me.lblS400Row_3)
        Me.panS400_3.Controls.Add(Me.lblS400_3)
        Me.panS400_3.Controls.Add(Me.saS404)
        Me.panS400_3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panS400_3.Location = New System.Drawing.Point(14, 181)
        Me.panS400_3.Name = "panS400_3"
        Me.panS400_3.Size = New System.Drawing.Size(122, 40)
        Me.panS400_3.TabIndex = 80
        '
        'lblS400Row_3
        '
        Me.lblS400Row_3.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblS400Row_3.Font = New System.Drawing.Font("Microsoft Sans Serif", 5.0!)
        Me.lblS400Row_3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblS400Row_3.Location = New System.Drawing.Point(23, 22)
        Me.lblS400Row_3.Name = "lblS400Row_3"
        Me.lblS400Row_3.Size = New System.Drawing.Size(19, 13)
        Me.lblS400Row_3.TabIndex = 83
        Me.lblS400Row_3.Text = "1"
        Me.lblS400Row_3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblS400_3
        '
        Me.lblS400_3.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblS400_3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblS400_3.Location = New System.Drawing.Point(1, 1)
        Me.lblS400_3.Name = "lblS400_3"
        Me.lblS400_3.Size = New System.Drawing.Size(42, 15)
        Me.lblS400_3.TabIndex = 81
        Me.lblS400_3.Text = "S404"
        Me.lblS400_3.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'saS404
        '
        Me.saS404.Count = 4
        Me.saS404.Location = New System.Drawing.Point(42, 2)
        Me.saS404.Margin = New System.Windows.Forms.Padding(2)
        Me.saS404.Name = "saS404"
        Me.saS404.Size = New System.Drawing.Size(76, 36)
        Me.saS404.TabIndex = 84
        '
        'panS400_2
        '
        Me.panS400_2.BackColor = System.Drawing.Color.Silver
        Me.panS400_2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panS400_2.Controls.Add(Me.lblS400Row_2)
        Me.panS400_2.Controls.Add(Me.lblS400_2)
        Me.panS400_2.Controls.Add(Me.saS403)
        Me.panS400_2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panS400_2.Location = New System.Drawing.Point(14, 130)
        Me.panS400_2.Name = "panS400_2"
        Me.panS400_2.Size = New System.Drawing.Size(122, 40)
        Me.panS400_2.TabIndex = 76
        '
        'lblS400Row_2
        '
        Me.lblS400Row_2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblS400Row_2.Font = New System.Drawing.Font("Microsoft Sans Serif", 5.0!)
        Me.lblS400Row_2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblS400Row_2.Location = New System.Drawing.Point(23, 22)
        Me.lblS400Row_2.Name = "lblS400Row_2"
        Me.lblS400Row_2.Size = New System.Drawing.Size(19, 13)
        Me.lblS400Row_2.TabIndex = 79
        Me.lblS400Row_2.Text = "1"
        Me.lblS400Row_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblS400_2
        '
        Me.lblS400_2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblS400_2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblS400_2.Location = New System.Drawing.Point(1, 1)
        Me.lblS400_2.Name = "lblS400_2"
        Me.lblS400_2.Size = New System.Drawing.Size(42, 15)
        Me.lblS400_2.TabIndex = 77
        Me.lblS400_2.Text = "S403"
        Me.lblS400_2.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'saS403
        '
        Me.saS403.Count = 4
        Me.saS403.Location = New System.Drawing.Point(42, 2)
        Me.saS403.Margin = New System.Windows.Forms.Padding(2)
        Me.saS403.Name = "saS403"
        Me.saS403.Size = New System.Drawing.Size(76, 36)
        Me.saS403.TabIndex = 80
        '
        'panS400_1
        '
        Me.panS400_1.BackColor = System.Drawing.Color.Silver
        Me.panS400_1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panS400_1.Controls.Add(Me.lblS400Row_1)
        Me.panS400_1.Controls.Add(Me.lblS400_1)
        Me.panS400_1.Controls.Add(Me.saS402)
        Me.panS400_1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panS400_1.Location = New System.Drawing.Point(14, 79)
        Me.panS400_1.Name = "panS400_1"
        Me.panS400_1.Size = New System.Drawing.Size(122, 40)
        Me.panS400_1.TabIndex = 72
        '
        'lblS400Row_1
        '
        Me.lblS400Row_1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblS400Row_1.Font = New System.Drawing.Font("Microsoft Sans Serif", 5.0!)
        Me.lblS400Row_1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblS400Row_1.Location = New System.Drawing.Point(23, 22)
        Me.lblS400Row_1.Name = "lblS400Row_1"
        Me.lblS400Row_1.Size = New System.Drawing.Size(19, 13)
        Me.lblS400Row_1.TabIndex = 75
        Me.lblS400Row_1.Text = "1"
        Me.lblS400Row_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblS400_1
        '
        Me.lblS400_1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblS400_1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblS400_1.Location = New System.Drawing.Point(1, 1)
        Me.lblS400_1.Name = "lblS400_1"
        Me.lblS400_1.Size = New System.Drawing.Size(42, 15)
        Me.lblS400_1.TabIndex = 73
        Me.lblS400_1.Text = "S402"
        Me.lblS400_1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'saS402
        '
        Me.saS402.Count = 4
        Me.saS402.Location = New System.Drawing.Point(42, 2)
        Me.saS402.Margin = New System.Windows.Forms.Padding(2)
        Me.saS402.Name = "saS402"
        Me.saS402.Size = New System.Drawing.Size(76, 36)
        Me.saS402.TabIndex = 76
        '
        'panS400_0
        '
        Me.panS400_0.BackColor = System.Drawing.Color.Silver
        Me.panS400_0.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panS400_0.Controls.Add(Me.lblS400_0)
        Me.panS400_0.Controls.Add(Me.lblS400Row_0)
        Me.panS400_0.Controls.Add(Me.saS401)
        Me.panS400_0.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panS400_0.Location = New System.Drawing.Point(14, 28)
        Me.panS400_0.Name = "panS400_0"
        Me.panS400_0.Size = New System.Drawing.Size(122, 40)
        Me.panS400_0.TabIndex = 68
        '
        'lblS400_0
        '
        Me.lblS400_0.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblS400_0.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblS400_0.Location = New System.Drawing.Point(1, 1)
        Me.lblS400_0.Name = "lblS400_0"
        Me.lblS400_0.Size = New System.Drawing.Size(42, 15)
        Me.lblS400_0.TabIndex = 71
        Me.lblS400_0.Text = "S401"
        Me.lblS400_0.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblS400Row_0
        '
        Me.lblS400Row_0.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblS400Row_0.Font = New System.Drawing.Font("Microsoft Sans Serif", 5.0!)
        Me.lblS400Row_0.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblS400Row_0.Location = New System.Drawing.Point(23, 22)
        Me.lblS400Row_0.Name = "lblS400Row_0"
        Me.lblS400Row_0.Size = New System.Drawing.Size(19, 13)
        Me.lblS400Row_0.TabIndex = 69
        Me.lblS400Row_0.Text = "1"
        Me.lblS400Row_0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'saS401
        '
        Me.saS401.Count = 4
        Me.saS401.Location = New System.Drawing.Point(42, 2)
        Me.saS401.Margin = New System.Windows.Forms.Padding(2)
        Me.saS401.Name = "saS401"
        Me.saS401.Size = New System.Drawing.Size(76, 36)
        Me.saS401.TabIndex = 72
        '
        'cmdS400ResetAll
        '
        Me.cmdS400ResetAll.BackColor = System.Drawing.SystemColors.Control
        Me.cmdS400ResetAll.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdS400ResetAll.Location = New System.Drawing.Point(150, 36)
        Me.cmdS400ResetAll.Name = "cmdS400ResetAll"
        Me.cmdS400ResetAll.Size = New System.Drawing.Size(100, 23)
        Me.cmdS400ResetAll.TabIndex = 92
        Me.cmdS400ResetAll.Text = "&Open Group"
        Me.cmdS400ResetAll.UseVisualStyleBackColor = False
        '
        'cmdExerciseGroup1x4_1W_LF
        '
        Me.cmdExerciseGroup1x4_1W_LF.BackColor = System.Drawing.SystemColors.Control
        Me.cmdExerciseGroup1x4_1W_LF.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdExerciseGroup1x4_1W_LF.Location = New System.Drawing.Point(150, 65)
        Me.cmdExerciseGroup1x4_1W_LF.Name = "cmdExerciseGroup1x4_1W_LF"
        Me.cmdExerciseGroup1x4_1W_LF.Size = New System.Drawing.Size(118, 23)
        Me.cmdExerciseGroup1x4_1W_LF.TabIndex = 219
        Me.cmdExerciseGroup1x4_1W_LF.Text = "&Exercise Group"
        Me.cmdExerciseGroup1x4_1W_LF.UseVisualStyleBackColor = False
        '
        'tabOptions_Page5
        '
        Me.tabOptions_Page5.Controls.Add(Me.panS500_9)
        Me.tabOptions_Page5.Controls.Add(Me.panS500_8)
        Me.tabOptions_Page5.Controls.Add(Me.panS500_7)
        Me.tabOptions_Page5.Controls.Add(Me.panS500_6)
        Me.tabOptions_Page5.Controls.Add(Me.panS500_5)
        Me.tabOptions_Page5.Controls.Add(Me.panS500_4)
        Me.tabOptions_Page5.Controls.Add(Me.panS500_3)
        Me.tabOptions_Page5.Controls.Add(Me.panS500_2)
        Me.tabOptions_Page5.Controls.Add(Me.panS500_1)
        Me.tabOptions_Page5.Controls.Add(Me.panS500_0)
        Me.tabOptions_Page5.Controls.Add(Me.cmdS500ResetAll)
        Me.tabOptions_Page5.Controls.Add(Me.cmdExerciseGroup2x8_1W_LF)
        Me.tabOptions_Page5.Location = New System.Drawing.Point(4, 40)
        Me.tabOptions_Page5.Name = "tabOptions_Page5"
        Me.tabOptions_Page5.Size = New System.Drawing.Size(593, 380)
        Me.tabOptions_Page5.TabIndex = 4
        Me.tabOptions_Page5.Text = " 2x8-1W-LF     "
        '
        'panS500_9
        '
        Me.panS500_9.BackColor = System.Drawing.Color.Silver
        Me.panS500_9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panS500_9.Controls.Add(Me.lblS500Row1_9)
        Me.panS500_9.Controls.Add(Me.lblS500Row2_9)
        Me.panS500_9.Controls.Add(Me.lblS500_9)
        Me.panS500_9.Controls.Add(Me.saS510)
        Me.panS500_9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panS500_9.Location = New System.Drawing.Point(213, 251)
        Me.panS500_9.Name = "panS500_9"
        Me.panS500_9.Size = New System.Drawing.Size(193, 52)
        Me.panS500_9.TabIndex = 63
        '
        'lblS500Row1_9
        '
        Me.lblS500Row1_9.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblS500Row1_9.Font = New System.Drawing.Font("Microsoft Sans Serif", 5.0!)
        Me.lblS500Row1_9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblS500Row1_9.Location = New System.Drawing.Point(20, 15)
        Me.lblS500Row1_9.Name = "lblS500Row1_9"
        Me.lblS500Row1_9.Size = New System.Drawing.Size(19, 13)
        Me.lblS500Row1_9.TabIndex = 67
        Me.lblS500Row1_9.Text = "1"
        Me.lblS500Row1_9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblS500Row2_9
        '
        Me.lblS500Row2_9.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblS500Row2_9.Font = New System.Drawing.Font("Microsoft Sans Serif", 5.0!)
        Me.lblS500Row2_9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblS500Row2_9.Location = New System.Drawing.Point(22, 31)
        Me.lblS500Row2_9.Name = "lblS500Row2_9"
        Me.lblS500Row2_9.Size = New System.Drawing.Size(17, 13)
        Me.lblS500Row2_9.TabIndex = 66
        Me.lblS500Row2_9.Text = "2"
        Me.lblS500Row2_9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblS500_9
        '
        Me.lblS500_9.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblS500_9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblS500_9.Location = New System.Drawing.Point(1, 1)
        Me.lblS500_9.Name = "lblS500_9"
        Me.lblS500_9.Size = New System.Drawing.Size(42, 15)
        Me.lblS500_9.TabIndex = 64
        Me.lblS500_9.Text = "S510"
        Me.lblS500_9.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'saS510
        '
        Me.saS510.Count = 8
        Me.saS510.Location = New System.Drawing.Point(42, 0)
        Me.saS510.Margin = New System.Windows.Forms.Padding(2)
        Me.saS510.Name = "saS510"
        Me.saS510.Size = New System.Drawing.Size(144, 52)
        Me.saS510.TabIndex = 68
        '
        'panS500_8
        '
        Me.panS500_8.BackColor = System.Drawing.Color.Silver
        Me.panS500_8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panS500_8.Controls.Add(Me.lblS500Row1_8)
        Me.panS500_8.Controls.Add(Me.lblS500Row2_8)
        Me.panS500_8.Controls.Add(Me.lblS500_8)
        Me.panS500_8.Controls.Add(Me.saS509)
        Me.panS500_8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panS500_8.Location = New System.Drawing.Point(213, 191)
        Me.panS500_8.Name = "panS500_8"
        Me.panS500_8.Size = New System.Drawing.Size(193, 52)
        Me.panS500_8.TabIndex = 58
        '
        'lblS500Row1_8
        '
        Me.lblS500Row1_8.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblS500Row1_8.Font = New System.Drawing.Font("Microsoft Sans Serif", 5.0!)
        Me.lblS500Row1_8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblS500Row1_8.Location = New System.Drawing.Point(20, 15)
        Me.lblS500Row1_8.Name = "lblS500Row1_8"
        Me.lblS500Row1_8.Size = New System.Drawing.Size(19, 13)
        Me.lblS500Row1_8.TabIndex = 62
        Me.lblS500Row1_8.Text = "1"
        Me.lblS500Row1_8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblS500Row2_8
        '
        Me.lblS500Row2_8.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblS500Row2_8.Font = New System.Drawing.Font("Microsoft Sans Serif", 5.0!)
        Me.lblS500Row2_8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblS500Row2_8.Location = New System.Drawing.Point(22, 31)
        Me.lblS500Row2_8.Name = "lblS500Row2_8"
        Me.lblS500Row2_8.Size = New System.Drawing.Size(17, 13)
        Me.lblS500Row2_8.TabIndex = 61
        Me.lblS500Row2_8.Text = "2"
        Me.lblS500Row2_8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblS500_8
        '
        Me.lblS500_8.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblS500_8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblS500_8.Location = New System.Drawing.Point(1, 1)
        Me.lblS500_8.Name = "lblS500_8"
        Me.lblS500_8.Size = New System.Drawing.Size(42, 15)
        Me.lblS500_8.TabIndex = 59
        Me.lblS500_8.Text = "S509"
        Me.lblS500_8.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'saS509
        '
        Me.saS509.Count = 8
        Me.saS509.Location = New System.Drawing.Point(42, 0)
        Me.saS509.Margin = New System.Windows.Forms.Padding(2)
        Me.saS509.Name = "saS509"
        Me.saS509.Size = New System.Drawing.Size(144, 52)
        Me.saS509.TabIndex = 63
        '
        'panS500_7
        '
        Me.panS500_7.BackColor = System.Drawing.Color.Silver
        Me.panS500_7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panS500_7.Controls.Add(Me.lblS500Row1_7)
        Me.panS500_7.Controls.Add(Me.lblS500Row2_7)
        Me.panS500_7.Controls.Add(Me.lblS500_7)
        Me.panS500_7.Controls.Add(Me.saS508)
        Me.panS500_7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panS500_7.Location = New System.Drawing.Point(213, 131)
        Me.panS500_7.Name = "panS500_7"
        Me.panS500_7.Size = New System.Drawing.Size(193, 52)
        Me.panS500_7.TabIndex = 53
        '
        'lblS500Row1_7
        '
        Me.lblS500Row1_7.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblS500Row1_7.Font = New System.Drawing.Font("Microsoft Sans Serif", 5.0!)
        Me.lblS500Row1_7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblS500Row1_7.Location = New System.Drawing.Point(20, 15)
        Me.lblS500Row1_7.Name = "lblS500Row1_7"
        Me.lblS500Row1_7.Size = New System.Drawing.Size(19, 13)
        Me.lblS500Row1_7.TabIndex = 57
        Me.lblS500Row1_7.Text = "1"
        Me.lblS500Row1_7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblS500Row2_7
        '
        Me.lblS500Row2_7.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblS500Row2_7.Font = New System.Drawing.Font("Microsoft Sans Serif", 5.0!)
        Me.lblS500Row2_7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblS500Row2_7.Location = New System.Drawing.Point(22, 31)
        Me.lblS500Row2_7.Name = "lblS500Row2_7"
        Me.lblS500Row2_7.Size = New System.Drawing.Size(17, 13)
        Me.lblS500Row2_7.TabIndex = 56
        Me.lblS500Row2_7.Text = "2"
        Me.lblS500Row2_7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblS500_7
        '
        Me.lblS500_7.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblS500_7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblS500_7.Location = New System.Drawing.Point(1, 1)
        Me.lblS500_7.Name = "lblS500_7"
        Me.lblS500_7.Size = New System.Drawing.Size(42, 15)
        Me.lblS500_7.TabIndex = 54
        Me.lblS500_7.Text = "S508"
        Me.lblS500_7.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'saS508
        '
        Me.saS508.Count = 8
        Me.saS508.Location = New System.Drawing.Point(42, 0)
        Me.saS508.Margin = New System.Windows.Forms.Padding(2)
        Me.saS508.Name = "saS508"
        Me.saS508.Size = New System.Drawing.Size(144, 52)
        Me.saS508.TabIndex = 58
        '
        'panS500_6
        '
        Me.panS500_6.BackColor = System.Drawing.Color.Silver
        Me.panS500_6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panS500_6.Controls.Add(Me.lblS500Row1_6)
        Me.panS500_6.Controls.Add(Me.lblS500Row2_6)
        Me.panS500_6.Controls.Add(Me.lblS500_6)
        Me.panS500_6.Controls.Add(Me.saS507)
        Me.panS500_6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panS500_6.Location = New System.Drawing.Point(213, 71)
        Me.panS500_6.Name = "panS500_6"
        Me.panS500_6.Size = New System.Drawing.Size(193, 52)
        Me.panS500_6.TabIndex = 48
        '
        'lblS500Row1_6
        '
        Me.lblS500Row1_6.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblS500Row1_6.Font = New System.Drawing.Font("Microsoft Sans Serif", 5.0!)
        Me.lblS500Row1_6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblS500Row1_6.Location = New System.Drawing.Point(20, 15)
        Me.lblS500Row1_6.Name = "lblS500Row1_6"
        Me.lblS500Row1_6.Size = New System.Drawing.Size(19, 13)
        Me.lblS500Row1_6.TabIndex = 52
        Me.lblS500Row1_6.Text = "1"
        Me.lblS500Row1_6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblS500Row2_6
        '
        Me.lblS500Row2_6.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblS500Row2_6.Font = New System.Drawing.Font("Microsoft Sans Serif", 5.0!)
        Me.lblS500Row2_6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblS500Row2_6.Location = New System.Drawing.Point(22, 31)
        Me.lblS500Row2_6.Name = "lblS500Row2_6"
        Me.lblS500Row2_6.Size = New System.Drawing.Size(17, 13)
        Me.lblS500Row2_6.TabIndex = 51
        Me.lblS500Row2_6.Text = "2"
        Me.lblS500Row2_6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblS500_6
        '
        Me.lblS500_6.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblS500_6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblS500_6.Location = New System.Drawing.Point(1, 1)
        Me.lblS500_6.Name = "lblS500_6"
        Me.lblS500_6.Size = New System.Drawing.Size(42, 15)
        Me.lblS500_6.TabIndex = 49
        Me.lblS500_6.Text = "S507"
        Me.lblS500_6.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'saS507
        '
        Me.saS507.Count = 8
        Me.saS507.Location = New System.Drawing.Point(42, 0)
        Me.saS507.Margin = New System.Windows.Forms.Padding(2)
        Me.saS507.Name = "saS507"
        Me.saS507.Size = New System.Drawing.Size(144, 52)
        Me.saS507.TabIndex = 53
        '
        'panS500_5
        '
        Me.panS500_5.BackColor = System.Drawing.Color.Silver
        Me.panS500_5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panS500_5.Controls.Add(Me.lblS500_5)
        Me.panS500_5.Controls.Add(Me.lblS500Row2_5)
        Me.panS500_5.Controls.Add(Me.lblS500Row1_5)
        Me.panS500_5.Controls.Add(Me.saS506)
        Me.panS500_5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panS500_5.Location = New System.Drawing.Point(213, 11)
        Me.panS500_5.Name = "panS500_5"
        Me.panS500_5.Size = New System.Drawing.Size(193, 52)
        Me.panS500_5.TabIndex = 43
        '
        'lblS500_5
        '
        Me.lblS500_5.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblS500_5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblS500_5.Location = New System.Drawing.Point(1, 1)
        Me.lblS500_5.Name = "lblS500_5"
        Me.lblS500_5.Size = New System.Drawing.Size(42, 15)
        Me.lblS500_5.TabIndex = 47
        Me.lblS500_5.Text = "S506"
        Me.lblS500_5.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblS500Row2_5
        '
        Me.lblS500Row2_5.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblS500Row2_5.Font = New System.Drawing.Font("Microsoft Sans Serif", 5.0!)
        Me.lblS500Row2_5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblS500Row2_5.Location = New System.Drawing.Point(22, 30)
        Me.lblS500Row2_5.Name = "lblS500Row2_5"
        Me.lblS500Row2_5.Size = New System.Drawing.Size(17, 13)
        Me.lblS500Row2_5.TabIndex = 45
        Me.lblS500Row2_5.Text = "2"
        Me.lblS500Row2_5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblS500Row1_5
        '
        Me.lblS500Row1_5.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblS500Row1_5.Font = New System.Drawing.Font("Microsoft Sans Serif", 5.0!)
        Me.lblS500Row1_5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblS500Row1_5.Location = New System.Drawing.Point(20, 15)
        Me.lblS500Row1_5.Name = "lblS500Row1_5"
        Me.lblS500Row1_5.Size = New System.Drawing.Size(19, 13)
        Me.lblS500Row1_5.TabIndex = 44
        Me.lblS500Row1_5.Text = "1"
        Me.lblS500Row1_5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'saS506
        '
        Me.saS506.Count = 8
        Me.saS506.Location = New System.Drawing.Point(42, 0)
        Me.saS506.Margin = New System.Windows.Forms.Padding(2)
        Me.saS506.Name = "saS506"
        Me.saS506.Size = New System.Drawing.Size(144, 52)
        Me.saS506.TabIndex = 48
        '
        'panS500_4
        '
        Me.panS500_4.BackColor = System.Drawing.Color.Silver
        Me.panS500_4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panS500_4.Controls.Add(Me.lblS500_4)
        Me.panS500_4.Controls.Add(Me.lblS500Row2_4)
        Me.panS500_4.Controls.Add(Me.lblS500Row1_4)
        Me.panS500_4.Controls.Add(Me.saS505)
        Me.panS500_4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panS500_4.Location = New System.Drawing.Point(8, 251)
        Me.panS500_4.Name = "panS500_4"
        Me.panS500_4.Size = New System.Drawing.Size(193, 52)
        Me.panS500_4.TabIndex = 38
        '
        'lblS500_4
        '
        Me.lblS500_4.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblS500_4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblS500_4.Location = New System.Drawing.Point(1, 1)
        Me.lblS500_4.Name = "lblS500_4"
        Me.lblS500_4.Size = New System.Drawing.Size(42, 15)
        Me.lblS500_4.TabIndex = 42
        Me.lblS500_4.Text = "S505"
        Me.lblS500_4.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblS500Row2_4
        '
        Me.lblS500Row2_4.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblS500Row2_4.Font = New System.Drawing.Font("Microsoft Sans Serif", 5.0!)
        Me.lblS500Row2_4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblS500Row2_4.Location = New System.Drawing.Point(23, 31)
        Me.lblS500Row2_4.Name = "lblS500Row2_4"
        Me.lblS500Row2_4.Size = New System.Drawing.Size(17, 13)
        Me.lblS500Row2_4.TabIndex = 40
        Me.lblS500Row2_4.Text = "2"
        Me.lblS500Row2_4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblS500Row1_4
        '
        Me.lblS500Row1_4.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblS500Row1_4.Font = New System.Drawing.Font("Microsoft Sans Serif", 5.0!)
        Me.lblS500Row1_4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblS500Row1_4.Location = New System.Drawing.Point(21, 15)
        Me.lblS500Row1_4.Name = "lblS500Row1_4"
        Me.lblS500Row1_4.Size = New System.Drawing.Size(19, 13)
        Me.lblS500Row1_4.TabIndex = 39
        Me.lblS500Row1_4.Text = "1"
        Me.lblS500Row1_4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'saS505
        '
        Me.saS505.Count = 8
        Me.saS505.Location = New System.Drawing.Point(42, 0)
        Me.saS505.Margin = New System.Windows.Forms.Padding(2)
        Me.saS505.Name = "saS505"
        Me.saS505.Size = New System.Drawing.Size(144, 52)
        Me.saS505.TabIndex = 43
        '
        'panS500_3
        '
        Me.panS500_3.BackColor = System.Drawing.Color.Silver
        Me.panS500_3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panS500_3.Controls.Add(Me.lblS500_3)
        Me.panS500_3.Controls.Add(Me.lblS500Row2_3)
        Me.panS500_3.Controls.Add(Me.lblS500Row1_3)
        Me.panS500_3.Controls.Add(Me.saS504)
        Me.panS500_3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panS500_3.Location = New System.Drawing.Point(8, 191)
        Me.panS500_3.Name = "panS500_3"
        Me.panS500_3.Size = New System.Drawing.Size(193, 52)
        Me.panS500_3.TabIndex = 33
        '
        'lblS500_3
        '
        Me.lblS500_3.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblS500_3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblS500_3.Location = New System.Drawing.Point(1, 1)
        Me.lblS500_3.Name = "lblS500_3"
        Me.lblS500_3.Size = New System.Drawing.Size(42, 15)
        Me.lblS500_3.TabIndex = 37
        Me.lblS500_3.Text = "S504"
        Me.lblS500_3.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblS500Row2_3
        '
        Me.lblS500Row2_3.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblS500Row2_3.Font = New System.Drawing.Font("Microsoft Sans Serif", 5.0!)
        Me.lblS500Row2_3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblS500Row2_3.Location = New System.Drawing.Point(23, 31)
        Me.lblS500Row2_3.Name = "lblS500Row2_3"
        Me.lblS500Row2_3.Size = New System.Drawing.Size(17, 13)
        Me.lblS500Row2_3.TabIndex = 35
        Me.lblS500Row2_3.Text = "2"
        Me.lblS500Row2_3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblS500Row1_3
        '
        Me.lblS500Row1_3.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblS500Row1_3.Font = New System.Drawing.Font("Microsoft Sans Serif", 5.0!)
        Me.lblS500Row1_3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblS500Row1_3.Location = New System.Drawing.Point(21, 15)
        Me.lblS500Row1_3.Name = "lblS500Row1_3"
        Me.lblS500Row1_3.Size = New System.Drawing.Size(19, 13)
        Me.lblS500Row1_3.TabIndex = 34
        Me.lblS500Row1_3.Text = "1"
        Me.lblS500Row1_3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'saS504
        '
        Me.saS504.Count = 8
        Me.saS504.Location = New System.Drawing.Point(42, 0)
        Me.saS504.Margin = New System.Windows.Forms.Padding(2)
        Me.saS504.Name = "saS504"
        Me.saS504.Size = New System.Drawing.Size(144, 52)
        Me.saS504.TabIndex = 38
        '
        'panS500_2
        '
        Me.panS500_2.BackColor = System.Drawing.Color.Silver
        Me.panS500_2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panS500_2.Controls.Add(Me.lblS500_2)
        Me.panS500_2.Controls.Add(Me.lblS500Row2_2)
        Me.panS500_2.Controls.Add(Me.lblS500Row1_2)
        Me.panS500_2.Controls.Add(Me.saS503)
        Me.panS500_2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panS500_2.Location = New System.Drawing.Point(8, 131)
        Me.panS500_2.Name = "panS500_2"
        Me.panS500_2.Size = New System.Drawing.Size(193, 52)
        Me.panS500_2.TabIndex = 28
        '
        'lblS500_2
        '
        Me.lblS500_2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblS500_2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblS500_2.Location = New System.Drawing.Point(1, 1)
        Me.lblS500_2.Name = "lblS500_2"
        Me.lblS500_2.Size = New System.Drawing.Size(42, 15)
        Me.lblS500_2.TabIndex = 32
        Me.lblS500_2.Text = "S503"
        Me.lblS500_2.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblS500Row2_2
        '
        Me.lblS500Row2_2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblS500Row2_2.Font = New System.Drawing.Font("Microsoft Sans Serif", 5.0!)
        Me.lblS500Row2_2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblS500Row2_2.Location = New System.Drawing.Point(23, 31)
        Me.lblS500Row2_2.Name = "lblS500Row2_2"
        Me.lblS500Row2_2.Size = New System.Drawing.Size(17, 13)
        Me.lblS500Row2_2.TabIndex = 30
        Me.lblS500Row2_2.Text = "2"
        Me.lblS500Row2_2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblS500Row1_2
        '
        Me.lblS500Row1_2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblS500Row1_2.Font = New System.Drawing.Font("Microsoft Sans Serif", 5.0!)
        Me.lblS500Row1_2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblS500Row1_2.Location = New System.Drawing.Point(21, 15)
        Me.lblS500Row1_2.Name = "lblS500Row1_2"
        Me.lblS500Row1_2.Size = New System.Drawing.Size(19, 13)
        Me.lblS500Row1_2.TabIndex = 29
        Me.lblS500Row1_2.Text = "1"
        Me.lblS500Row1_2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'saS503
        '
        Me.saS503.Count = 8
        Me.saS503.Location = New System.Drawing.Point(42, 0)
        Me.saS503.Margin = New System.Windows.Forms.Padding(2)
        Me.saS503.Name = "saS503"
        Me.saS503.Size = New System.Drawing.Size(144, 52)
        Me.saS503.TabIndex = 33
        '
        'panS500_1
        '
        Me.panS500_1.BackColor = System.Drawing.Color.Silver
        Me.panS500_1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panS500_1.Controls.Add(Me.lblS500_1)
        Me.panS500_1.Controls.Add(Me.lblS500Row2_1)
        Me.panS500_1.Controls.Add(Me.lblS500Row1_1)
        Me.panS500_1.Controls.Add(Me.saS502)
        Me.panS500_1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panS500_1.Location = New System.Drawing.Point(8, 71)
        Me.panS500_1.Name = "panS500_1"
        Me.panS500_1.Size = New System.Drawing.Size(193, 52)
        Me.panS500_1.TabIndex = 23
        '
        'lblS500_1
        '
        Me.lblS500_1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblS500_1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblS500_1.Location = New System.Drawing.Point(1, 1)
        Me.lblS500_1.Name = "lblS500_1"
        Me.lblS500_1.Size = New System.Drawing.Size(42, 15)
        Me.lblS500_1.TabIndex = 27
        Me.lblS500_1.Text = "S502"
        Me.lblS500_1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblS500Row2_1
        '
        Me.lblS500Row2_1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblS500Row2_1.Font = New System.Drawing.Font("Microsoft Sans Serif", 5.0!)
        Me.lblS500Row2_1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblS500Row2_1.Location = New System.Drawing.Point(23, 31)
        Me.lblS500Row2_1.Name = "lblS500Row2_1"
        Me.lblS500Row2_1.Size = New System.Drawing.Size(17, 13)
        Me.lblS500Row2_1.TabIndex = 25
        Me.lblS500Row2_1.Text = "2"
        Me.lblS500Row2_1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblS500Row1_1
        '
        Me.lblS500Row1_1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblS500Row1_1.Font = New System.Drawing.Font("Microsoft Sans Serif", 5.0!)
        Me.lblS500Row1_1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblS500Row1_1.Location = New System.Drawing.Point(21, 15)
        Me.lblS500Row1_1.Name = "lblS500Row1_1"
        Me.lblS500Row1_1.Size = New System.Drawing.Size(19, 13)
        Me.lblS500Row1_1.TabIndex = 24
        Me.lblS500Row1_1.Text = "1"
        Me.lblS500Row1_1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'saS502
        '
        Me.saS502.Count = 8
        Me.saS502.Location = New System.Drawing.Point(42, 0)
        Me.saS502.Margin = New System.Windows.Forms.Padding(2)
        Me.saS502.Name = "saS502"
        Me.saS502.Size = New System.Drawing.Size(144, 52)
        Me.saS502.TabIndex = 28
        '
        'panS500_0
        '
        Me.panS500_0.BackColor = System.Drawing.Color.Silver
        Me.panS500_0.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panS500_0.Controls.Add(Me.lblS500Row1_0)
        Me.panS500_0.Controls.Add(Me.lblS500Row2_0)
        Me.panS500_0.Controls.Add(Me.lblS500_0)
        Me.panS500_0.Controls.Add(Me.saS501)
        Me.panS500_0.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panS500_0.Location = New System.Drawing.Point(8, 11)
        Me.panS500_0.Name = "panS500_0"
        Me.panS500_0.Size = New System.Drawing.Size(193, 52)
        Me.panS500_0.TabIndex = 18
        '
        'lblS500Row1_0
        '
        Me.lblS500Row1_0.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblS500Row1_0.Font = New System.Drawing.Font("Microsoft Sans Serif", 5.0!)
        Me.lblS500Row1_0.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblS500Row1_0.Location = New System.Drawing.Point(21, 15)
        Me.lblS500Row1_0.Name = "lblS500Row1_0"
        Me.lblS500Row1_0.Size = New System.Drawing.Size(19, 13)
        Me.lblS500Row1_0.TabIndex = 22
        Me.lblS500Row1_0.Text = "1"
        Me.lblS500Row1_0.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblS500Row2_0
        '
        Me.lblS500Row2_0.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblS500Row2_0.Font = New System.Drawing.Font("Microsoft Sans Serif", 5.0!)
        Me.lblS500Row2_0.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblS500Row2_0.Location = New System.Drawing.Point(23, 30)
        Me.lblS500Row2_0.Name = "lblS500Row2_0"
        Me.lblS500Row2_0.Size = New System.Drawing.Size(17, 13)
        Me.lblS500Row2_0.TabIndex = 21
        Me.lblS500Row2_0.Text = "2"
        Me.lblS500Row2_0.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblS500_0
        '
        Me.lblS500_0.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblS500_0.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblS500_0.Location = New System.Drawing.Point(1, 1)
        Me.lblS500_0.Name = "lblS500_0"
        Me.lblS500_0.Size = New System.Drawing.Size(42, 15)
        Me.lblS500_0.TabIndex = 19
        Me.lblS500_0.Text = "S501"
        Me.lblS500_0.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'saS501
        '
        Me.saS501.Count = 8
        Me.saS501.Location = New System.Drawing.Point(42, 0)
        Me.saS501.Margin = New System.Windows.Forms.Padding(0)
        Me.saS501.Name = "saS501"
        Me.saS501.Size = New System.Drawing.Size(144, 52)
        Me.saS501.TabIndex = 23
        '
        'cmdS500ResetAll
        '
        Me.cmdS500ResetAll.BackColor = System.Drawing.SystemColors.Control
        Me.cmdS500ResetAll.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdS500ResetAll.Location = New System.Drawing.Point(17, 314)
        Me.cmdS500ResetAll.Name = "cmdS500ResetAll"
        Me.cmdS500ResetAll.Size = New System.Drawing.Size(95, 23)
        Me.cmdS500ResetAll.TabIndex = 16
        Me.cmdS500ResetAll.Text = "&Open Group"
        Me.cmdS500ResetAll.UseVisualStyleBackColor = False
        '
        'cmdExerciseGroup2x8_1W_LF
        '
        Me.cmdExerciseGroup2x8_1W_LF.BackColor = System.Drawing.SystemColors.Control
        Me.cmdExerciseGroup2x8_1W_LF.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdExerciseGroup2x8_1W_LF.Location = New System.Drawing.Point(118, 314)
        Me.cmdExerciseGroup2x8_1W_LF.Name = "cmdExerciseGroup2x8_1W_LF"
        Me.cmdExerciseGroup2x8_1W_LF.Size = New System.Drawing.Size(121, 23)
        Me.cmdExerciseGroup2x8_1W_LF.TabIndex = 220
        Me.cmdExerciseGroup2x8_1W_LF.Text = "&Exercise Group"
        Me.cmdExerciseGroup2x8_1W_LF.UseVisualStyleBackColor = False
        '
        'tabOptions_Page6
        '
        Me.tabOptions_Page6.Controls.Add(Me.cmdExerciseGroup1x2_1W_LF)
        Me.tabOptions_Page6.Controls.Add(Me.cmdS600ResetAll)
        Me.tabOptions_Page6.Controls.Add(Me.panS600_0)
        Me.tabOptions_Page6.Controls.Add(Me.panS600_1)
        Me.tabOptions_Page6.Controls.Add(Me.panS600_2)
        Me.tabOptions_Page6.Controls.Add(Me.panS600_3)
        Me.tabOptions_Page6.Controls.Add(Me.panS600_4)
        Me.tabOptions_Page6.Controls.Add(Me.panS600_5)
        Me.tabOptions_Page6.Controls.Add(Me.panS600_6)
        Me.tabOptions_Page6.Controls.Add(Me.panS600_7)
        Me.tabOptions_Page6.Controls.Add(Me.panS600_8)
        Me.tabOptions_Page6.Controls.Add(Me.panS600_9)
        Me.tabOptions_Page6.Controls.Add(Me.panS600_10)
        Me.tabOptions_Page6.Controls.Add(Me.panS600_11)
        Me.tabOptions_Page6.Location = New System.Drawing.Point(4, 40)
        Me.tabOptions_Page6.Name = "tabOptions_Page6"
        Me.tabOptions_Page6.Size = New System.Drawing.Size(593, 380)
        Me.tabOptions_Page6.TabIndex = 5
        Me.tabOptions_Page6.Text = " 1x2-1W-LF     "
        '
        'cmdExerciseGroup1x2_1W_LF
        '
        Me.cmdExerciseGroup1x2_1W_LF.BackColor = System.Drawing.SystemColors.Control
        Me.cmdExerciseGroup1x2_1W_LF.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdExerciseGroup1x2_1W_LF.Location = New System.Drawing.Point(202, 49)
        Me.cmdExerciseGroup1x2_1W_LF.Name = "cmdExerciseGroup1x2_1W_LF"
        Me.cmdExerciseGroup1x2_1W_LF.Size = New System.Drawing.Size(117, 23)
        Me.cmdExerciseGroup1x2_1W_LF.TabIndex = 221
        Me.cmdExerciseGroup1x2_1W_LF.Text = "&Exercise Group"
        Me.cmdExerciseGroup1x2_1W_LF.UseVisualStyleBackColor = False
        '
        'cmdS600ResetAll
        '
        Me.cmdS600ResetAll.BackColor = System.Drawing.SystemColors.Control
        Me.cmdS600ResetAll.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdS600ResetAll.Location = New System.Drawing.Point(202, 16)
        Me.cmdS600ResetAll.Name = "cmdS600ResetAll"
        Me.cmdS600ResetAll.Size = New System.Drawing.Size(96, 23)
        Me.cmdS600ResetAll.TabIndex = 141
        Me.cmdS600ResetAll.Text = "&Open Group"
        Me.cmdS600ResetAll.UseVisualStyleBackColor = False
        '
        'panS600_0
        '
        Me.panS600_0.BackColor = System.Drawing.Color.Silver
        Me.panS600_0.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panS600_0.Controls.Add(Me.lblS600Row_0)
        Me.panS600_0.Controls.Add(Me.lblS600_0)
        Me.panS600_0.Controls.Add(Me.saS601)
        Me.panS600_0.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panS600_0.Location = New System.Drawing.Point(8, 13)
        Me.panS600_0.Name = "panS600_0"
        Me.panS600_0.Size = New System.Drawing.Size(79, 36)
        Me.panS600_0.TabIndex = 93
        '
        'lblS600Row_0
        '
        Me.lblS600Row_0.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblS600Row_0.Font = New System.Drawing.Font("Microsoft Sans Serif", 5.0!)
        Me.lblS600Row_0.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblS600Row_0.Location = New System.Drawing.Point(24, 14)
        Me.lblS600Row_0.Margin = New System.Windows.Forms.Padding(0)
        Me.lblS600Row_0.Name = "lblS600Row_0"
        Me.lblS600Row_0.Size = New System.Drawing.Size(16, 16)
        Me.lblS600Row_0.TabIndex = 96
        Me.lblS600Row_0.Text = "1"
        Me.lblS600Row_0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblS600_0
        '
        Me.lblS600_0.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblS600_0.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblS600_0.Location = New System.Drawing.Point(1, 1)
        Me.lblS600_0.Name = "lblS600_0"
        Me.lblS600_0.Size = New System.Drawing.Size(41, 18)
        Me.lblS600_0.TabIndex = 94
        Me.lblS600_0.Text = "S601"
        Me.lblS600_0.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'saS601
        '
        Me.saS601.Count = 2
        Me.saS601.Location = New System.Drawing.Point(41, 0)
        Me.saS601.Margin = New System.Windows.Forms.Padding(2)
        Me.saS601.Name = "saS601"
        Me.saS601.Size = New System.Drawing.Size(38, 33)
        Me.saS601.TabIndex = 97
        '
        'panS600_1
        '
        Me.panS600_1.BackColor = System.Drawing.Color.Silver
        Me.panS600_1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panS600_1.Controls.Add(Me.lblS600_1)
        Me.panS600_1.Controls.Add(Me.lblS600Row_1)
        Me.panS600_1.Controls.Add(Me.saS602)
        Me.panS600_1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panS600_1.Location = New System.Drawing.Point(8, 59)
        Me.panS600_1.Name = "panS600_1"
        Me.panS600_1.Size = New System.Drawing.Size(79, 36)
        Me.panS600_1.TabIndex = 97
        '
        'lblS600_1
        '
        Me.lblS600_1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblS600_1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblS600_1.Location = New System.Drawing.Point(1, 1)
        Me.lblS600_1.Name = "lblS600_1"
        Me.lblS600_1.Size = New System.Drawing.Size(41, 18)
        Me.lblS600_1.TabIndex = 100
        Me.lblS600_1.Text = "S602"
        Me.lblS600_1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblS600Row_1
        '
        Me.lblS600Row_1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblS600Row_1.Font = New System.Drawing.Font("Microsoft Sans Serif", 5.0!)
        Me.lblS600Row_1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblS600Row_1.Location = New System.Drawing.Point(24, 14)
        Me.lblS600Row_1.Name = "lblS600Row_1"
        Me.lblS600Row_1.Size = New System.Drawing.Size(16, 16)
        Me.lblS600Row_1.TabIndex = 98
        Me.lblS600Row_1.Text = "1"
        Me.lblS600Row_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'saS602
        '
        Me.saS602.Count = 2
        Me.saS602.Location = New System.Drawing.Point(41, 0)
        Me.saS602.Margin = New System.Windows.Forms.Padding(2)
        Me.saS602.Name = "saS602"
        Me.saS602.Size = New System.Drawing.Size(38, 33)
        Me.saS602.TabIndex = 0
        '
        'panS600_2
        '
        Me.panS600_2.BackColor = System.Drawing.Color.Silver
        Me.panS600_2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panS600_2.Controls.Add(Me.lblS600_2)
        Me.panS600_2.Controls.Add(Me.lblS600Row_2)
        Me.panS600_2.Controls.Add(Me.saS603)
        Me.panS600_2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panS600_2.Location = New System.Drawing.Point(8, 105)
        Me.panS600_2.Name = "panS600_2"
        Me.panS600_2.Size = New System.Drawing.Size(79, 36)
        Me.panS600_2.TabIndex = 101
        '
        'lblS600_2
        '
        Me.lblS600_2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblS600_2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblS600_2.Location = New System.Drawing.Point(1, 1)
        Me.lblS600_2.Name = "lblS600_2"
        Me.lblS600_2.Size = New System.Drawing.Size(41, 18)
        Me.lblS600_2.TabIndex = 104
        Me.lblS600_2.Text = "S603"
        Me.lblS600_2.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblS600Row_2
        '
        Me.lblS600Row_2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblS600Row_2.Font = New System.Drawing.Font("Microsoft Sans Serif", 5.0!)
        Me.lblS600Row_2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblS600Row_2.Location = New System.Drawing.Point(24, 15)
        Me.lblS600Row_2.Name = "lblS600Row_2"
        Me.lblS600Row_2.Size = New System.Drawing.Size(16, 16)
        Me.lblS600Row_2.TabIndex = 102
        Me.lblS600Row_2.Text = "1"
        Me.lblS600Row_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'saS603
        '
        Me.saS603.Count = 2
        Me.saS603.Location = New System.Drawing.Point(41, 0)
        Me.saS603.Margin = New System.Windows.Forms.Padding(2)
        Me.saS603.Name = "saS603"
        Me.saS603.Size = New System.Drawing.Size(38, 33)
        Me.saS603.TabIndex = 0
        '
        'panS600_3
        '
        Me.panS600_3.BackColor = System.Drawing.Color.Silver
        Me.panS600_3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panS600_3.Controls.Add(Me.lblS600_3)
        Me.panS600_3.Controls.Add(Me.lblS600Row_3)
        Me.panS600_3.Controls.Add(Me.saS604)
        Me.panS600_3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panS600_3.Location = New System.Drawing.Point(8, 151)
        Me.panS600_3.Name = "panS600_3"
        Me.panS600_3.Size = New System.Drawing.Size(79, 36)
        Me.panS600_3.TabIndex = 105
        '
        'lblS600_3
        '
        Me.lblS600_3.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblS600_3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblS600_3.Location = New System.Drawing.Point(1, 1)
        Me.lblS600_3.Name = "lblS600_3"
        Me.lblS600_3.Size = New System.Drawing.Size(41, 18)
        Me.lblS600_3.TabIndex = 108
        Me.lblS600_3.Text = "S604"
        Me.lblS600_3.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblS600Row_3
        '
        Me.lblS600Row_3.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblS600Row_3.Font = New System.Drawing.Font("Microsoft Sans Serif", 5.0!)
        Me.lblS600Row_3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblS600Row_3.Location = New System.Drawing.Point(24, 15)
        Me.lblS600Row_3.Name = "lblS600Row_3"
        Me.lblS600Row_3.Size = New System.Drawing.Size(16, 16)
        Me.lblS600Row_3.TabIndex = 106
        Me.lblS600Row_3.Text = "1"
        Me.lblS600Row_3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'saS604
        '
        Me.saS604.Count = 2
        Me.saS604.Location = New System.Drawing.Point(41, 0)
        Me.saS604.Margin = New System.Windows.Forms.Padding(2)
        Me.saS604.Name = "saS604"
        Me.saS604.Size = New System.Drawing.Size(38, 33)
        Me.saS604.TabIndex = 0
        '
        'panS600_4
        '
        Me.panS600_4.BackColor = System.Drawing.Color.Silver
        Me.panS600_4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panS600_4.Controls.Add(Me.lblS600_4)
        Me.panS600_4.Controls.Add(Me.lblS600Row_4)
        Me.panS600_4.Controls.Add(Me.saS605)
        Me.panS600_4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panS600_4.Location = New System.Drawing.Point(8, 197)
        Me.panS600_4.Name = "panS600_4"
        Me.panS600_4.Size = New System.Drawing.Size(79, 36)
        Me.panS600_4.TabIndex = 109
        '
        'lblS600_4
        '
        Me.lblS600_4.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblS600_4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblS600_4.Location = New System.Drawing.Point(1, 1)
        Me.lblS600_4.Name = "lblS600_4"
        Me.lblS600_4.Size = New System.Drawing.Size(41, 18)
        Me.lblS600_4.TabIndex = 112
        Me.lblS600_4.Text = "S605"
        Me.lblS600_4.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblS600Row_4
        '
        Me.lblS600Row_4.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblS600Row_4.Font = New System.Drawing.Font("Microsoft Sans Serif", 5.0!)
        Me.lblS600Row_4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblS600Row_4.Location = New System.Drawing.Point(24, 15)
        Me.lblS600Row_4.Name = "lblS600Row_4"
        Me.lblS600Row_4.Size = New System.Drawing.Size(16, 16)
        Me.lblS600Row_4.TabIndex = 110
        Me.lblS600Row_4.Text = "1"
        Me.lblS600Row_4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'saS605
        '
        Me.saS605.Count = 2
        Me.saS605.Location = New System.Drawing.Point(41, 0)
        Me.saS605.Margin = New System.Windows.Forms.Padding(2)
        Me.saS605.Name = "saS605"
        Me.saS605.Size = New System.Drawing.Size(38, 33)
        Me.saS605.TabIndex = 0
        '
        'panS600_5
        '
        Me.panS600_5.BackColor = System.Drawing.Color.Silver
        Me.panS600_5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panS600_5.Controls.Add(Me.lblS600_5)
        Me.panS600_5.Controls.Add(Me.lblS600Row_5)
        Me.panS600_5.Controls.Add(Me.saS606)
        Me.panS600_5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panS600_5.Location = New System.Drawing.Point(8, 243)
        Me.panS600_5.Name = "panS600_5"
        Me.panS600_5.Size = New System.Drawing.Size(79, 36)
        Me.panS600_5.TabIndex = 113
        '
        'lblS600_5
        '
        Me.lblS600_5.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblS600_5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblS600_5.Location = New System.Drawing.Point(1, 1)
        Me.lblS600_5.Name = "lblS600_5"
        Me.lblS600_5.Size = New System.Drawing.Size(41, 18)
        Me.lblS600_5.TabIndex = 116
        Me.lblS600_5.Text = "S606"
        Me.lblS600_5.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblS600Row_5
        '
        Me.lblS600Row_5.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblS600Row_5.Font = New System.Drawing.Font("Microsoft Sans Serif", 5.0!)
        Me.lblS600Row_5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblS600Row_5.Location = New System.Drawing.Point(24, 14)
        Me.lblS600Row_5.Name = "lblS600Row_5"
        Me.lblS600Row_5.Size = New System.Drawing.Size(16, 16)
        Me.lblS600Row_5.TabIndex = 114
        Me.lblS600Row_5.Text = "1"
        Me.lblS600Row_5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'saS606
        '
        Me.saS606.Count = 2
        Me.saS606.Location = New System.Drawing.Point(41, 0)
        Me.saS606.Margin = New System.Windows.Forms.Padding(2)
        Me.saS606.Name = "saS606"
        Me.saS606.Size = New System.Drawing.Size(38, 33)
        Me.saS606.TabIndex = 0
        '
        'panS600_6
        '
        Me.panS600_6.BackColor = System.Drawing.Color.Silver
        Me.panS600_6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panS600_6.Controls.Add(Me.lblS600_6)
        Me.panS600_6.Controls.Add(Me.lblS600Row_6)
        Me.panS600_6.Controls.Add(Me.saS607)
        Me.panS600_6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panS600_6.Location = New System.Drawing.Point(109, 13)
        Me.panS600_6.Name = "panS600_6"
        Me.panS600_6.Size = New System.Drawing.Size(79, 36)
        Me.panS600_6.TabIndex = 117
        '
        'lblS600_6
        '
        Me.lblS600_6.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblS600_6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblS600_6.Location = New System.Drawing.Point(1, 1)
        Me.lblS600_6.Name = "lblS600_6"
        Me.lblS600_6.Size = New System.Drawing.Size(41, 18)
        Me.lblS600_6.TabIndex = 120
        Me.lblS600_6.Text = "S607"
        Me.lblS600_6.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblS600Row_6
        '
        Me.lblS600Row_6.BackColor = System.Drawing.Color.Transparent
        Me.lblS600Row_6.Font = New System.Drawing.Font("Microsoft Sans Serif", 5.0!)
        Me.lblS600Row_6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblS600Row_6.Location = New System.Drawing.Point(25, 14)
        Me.lblS600Row_6.Name = "lblS600Row_6"
        Me.lblS600Row_6.Size = New System.Drawing.Size(16, 16)
        Me.lblS600Row_6.TabIndex = 118
        Me.lblS600Row_6.Text = "1"
        Me.lblS600Row_6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'saS607
        '
        Me.saS607.Count = 2
        Me.saS607.Location = New System.Drawing.Point(41, 0)
        Me.saS607.Margin = New System.Windows.Forms.Padding(2)
        Me.saS607.Name = "saS607"
        Me.saS607.Size = New System.Drawing.Size(38, 33)
        Me.saS607.TabIndex = 0
        '
        'panS600_7
        '
        Me.panS600_7.BackColor = System.Drawing.Color.Silver
        Me.panS600_7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panS600_7.Controls.Add(Me.lblS600Row_7)
        Me.panS600_7.Controls.Add(Me.lblS600_7)
        Me.panS600_7.Controls.Add(Me.saS608)
        Me.panS600_7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panS600_7.Location = New System.Drawing.Point(109, 59)
        Me.panS600_7.Name = "panS600_7"
        Me.panS600_7.Size = New System.Drawing.Size(79, 36)
        Me.panS600_7.TabIndex = 121
        '
        'lblS600Row_7
        '
        Me.lblS600Row_7.BackColor = System.Drawing.Color.Transparent
        Me.lblS600Row_7.Font = New System.Drawing.Font("Microsoft Sans Serif", 5.0!)
        Me.lblS600Row_7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblS600Row_7.Location = New System.Drawing.Point(25, 14)
        Me.lblS600Row_7.Name = "lblS600Row_7"
        Me.lblS600Row_7.Size = New System.Drawing.Size(16, 16)
        Me.lblS600Row_7.TabIndex = 124
        Me.lblS600Row_7.Text = "1"
        Me.lblS600Row_7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblS600_7
        '
        Me.lblS600_7.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblS600_7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblS600_7.Location = New System.Drawing.Point(1, 1)
        Me.lblS600_7.Name = "lblS600_7"
        Me.lblS600_7.Size = New System.Drawing.Size(41, 18)
        Me.lblS600_7.TabIndex = 122
        Me.lblS600_7.Text = "S608"
        Me.lblS600_7.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'saS608
        '
        Me.saS608.Count = 2
        Me.saS608.Location = New System.Drawing.Point(41, 0)
        Me.saS608.Margin = New System.Windows.Forms.Padding(2)
        Me.saS608.Name = "saS608"
        Me.saS608.Size = New System.Drawing.Size(38, 33)
        Me.saS608.TabIndex = 0
        '
        'panS600_8
        '
        Me.panS600_8.BackColor = System.Drawing.Color.Silver
        Me.panS600_8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panS600_8.Controls.Add(Me.lblS600Row_8)
        Me.panS600_8.Controls.Add(Me.lblS600_8)
        Me.panS600_8.Controls.Add(Me.saS609)
        Me.panS600_8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panS600_8.Location = New System.Drawing.Point(109, 105)
        Me.panS600_8.Name = "panS600_8"
        Me.panS600_8.Size = New System.Drawing.Size(79, 36)
        Me.panS600_8.TabIndex = 125
        '
        'lblS600Row_8
        '
        Me.lblS600Row_8.BackColor = System.Drawing.Color.Transparent
        Me.lblS600Row_8.Font = New System.Drawing.Font("Microsoft Sans Serif", 5.0!)
        Me.lblS600Row_8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblS600Row_8.Location = New System.Drawing.Point(25, 15)
        Me.lblS600Row_8.Name = "lblS600Row_8"
        Me.lblS600Row_8.Size = New System.Drawing.Size(16, 16)
        Me.lblS600Row_8.TabIndex = 128
        Me.lblS600Row_8.Text = "1"
        Me.lblS600Row_8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblS600_8
        '
        Me.lblS600_8.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblS600_8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblS600_8.Location = New System.Drawing.Point(1, 1)
        Me.lblS600_8.Name = "lblS600_8"
        Me.lblS600_8.Size = New System.Drawing.Size(41, 18)
        Me.lblS600_8.TabIndex = 126
        Me.lblS600_8.Text = "S609"
        Me.lblS600_8.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'saS609
        '
        Me.saS609.Count = 2
        Me.saS609.Location = New System.Drawing.Point(41, 0)
        Me.saS609.Margin = New System.Windows.Forms.Padding(2)
        Me.saS609.Name = "saS609"
        Me.saS609.Size = New System.Drawing.Size(38, 33)
        Me.saS609.TabIndex = 0
        '
        'panS600_9
        '
        Me.panS600_9.BackColor = System.Drawing.Color.Silver
        Me.panS600_9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panS600_9.Controls.Add(Me.lblS600Row_9)
        Me.panS600_9.Controls.Add(Me.lblS600_9)
        Me.panS600_9.Controls.Add(Me.saS610)
        Me.panS600_9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panS600_9.Location = New System.Drawing.Point(109, 151)
        Me.panS600_9.Name = "panS600_9"
        Me.panS600_9.Size = New System.Drawing.Size(79, 36)
        Me.panS600_9.TabIndex = 129
        '
        'lblS600Row_9
        '
        Me.lblS600Row_9.BackColor = System.Drawing.Color.Transparent
        Me.lblS600Row_9.Font = New System.Drawing.Font("Microsoft Sans Serif", 5.0!)
        Me.lblS600Row_9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblS600Row_9.Location = New System.Drawing.Point(25, 15)
        Me.lblS600Row_9.Name = "lblS600Row_9"
        Me.lblS600Row_9.Size = New System.Drawing.Size(16, 16)
        Me.lblS600Row_9.TabIndex = 132
        Me.lblS600Row_9.Text = "1"
        Me.lblS600Row_9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblS600_9
        '
        Me.lblS600_9.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblS600_9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblS600_9.Location = New System.Drawing.Point(1, 1)
        Me.lblS600_9.Name = "lblS600_9"
        Me.lblS600_9.Size = New System.Drawing.Size(41, 18)
        Me.lblS600_9.TabIndex = 130
        Me.lblS600_9.Text = "S610"
        Me.lblS600_9.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'saS610
        '
        Me.saS610.Count = 2
        Me.saS610.Location = New System.Drawing.Point(41, 0)
        Me.saS610.Margin = New System.Windows.Forms.Padding(2)
        Me.saS610.Name = "saS610"
        Me.saS610.Size = New System.Drawing.Size(38, 33)
        Me.saS610.TabIndex = 0
        '
        'panS600_10
        '
        Me.panS600_10.BackColor = System.Drawing.Color.Silver
        Me.panS600_10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panS600_10.Controls.Add(Me.lblS600Row_10)
        Me.panS600_10.Controls.Add(Me.lblS600_10)
        Me.panS600_10.Controls.Add(Me.saS611)
        Me.panS600_10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panS600_10.Location = New System.Drawing.Point(109, 197)
        Me.panS600_10.Name = "panS600_10"
        Me.panS600_10.Size = New System.Drawing.Size(79, 36)
        Me.panS600_10.TabIndex = 133
        '
        'lblS600Row_10
        '
        Me.lblS600Row_10.BackColor = System.Drawing.Color.Transparent
        Me.lblS600Row_10.Font = New System.Drawing.Font("Microsoft Sans Serif", 5.0!)
        Me.lblS600Row_10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblS600Row_10.Location = New System.Drawing.Point(25, 15)
        Me.lblS600Row_10.Name = "lblS600Row_10"
        Me.lblS600Row_10.Size = New System.Drawing.Size(16, 16)
        Me.lblS600Row_10.TabIndex = 136
        Me.lblS600Row_10.Text = "1"
        Me.lblS600Row_10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblS600_10
        '
        Me.lblS600_10.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblS600_10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblS600_10.Location = New System.Drawing.Point(1, 1)
        Me.lblS600_10.Name = "lblS600_10"
        Me.lblS600_10.Size = New System.Drawing.Size(41, 18)
        Me.lblS600_10.TabIndex = 134
        Me.lblS600_10.Text = "S611"
        Me.lblS600_10.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'saS611
        '
        Me.saS611.Count = 2
        Me.saS611.Location = New System.Drawing.Point(41, 0)
        Me.saS611.Margin = New System.Windows.Forms.Padding(2)
        Me.saS611.Name = "saS611"
        Me.saS611.Size = New System.Drawing.Size(38, 33)
        Me.saS611.TabIndex = 0
        '
        'panS600_11
        '
        Me.panS600_11.BackColor = System.Drawing.Color.Silver
        Me.panS600_11.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panS600_11.Controls.Add(Me.lblS600Row_11)
        Me.panS600_11.Controls.Add(Me.lblS600_11)
        Me.panS600_11.Controls.Add(Me.saS612)
        Me.panS600_11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panS600_11.Location = New System.Drawing.Point(109, 243)
        Me.panS600_11.Name = "panS600_11"
        Me.panS600_11.Size = New System.Drawing.Size(79, 36)
        Me.panS600_11.TabIndex = 137
        '
        'lblS600Row_11
        '
        Me.lblS600Row_11.BackColor = System.Drawing.Color.Transparent
        Me.lblS600Row_11.Font = New System.Drawing.Font("Microsoft Sans Serif", 5.0!)
        Me.lblS600Row_11.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblS600Row_11.Location = New System.Drawing.Point(25, 14)
        Me.lblS600Row_11.Name = "lblS600Row_11"
        Me.lblS600Row_11.Size = New System.Drawing.Size(16, 16)
        Me.lblS600Row_11.TabIndex = 140
        Me.lblS600Row_11.Text = "1"
        Me.lblS600Row_11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblS600_11
        '
        Me.lblS600_11.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblS600_11.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblS600_11.Location = New System.Drawing.Point(1, 1)
        Me.lblS600_11.Name = "lblS600_11"
        Me.lblS600_11.Size = New System.Drawing.Size(41, 18)
        Me.lblS600_11.TabIndex = 138
        Me.lblS600_11.Text = "S612"
        Me.lblS600_11.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'saS612
        '
        Me.saS612.Count = 2
        Me.saS612.Location = New System.Drawing.Point(41, 0)
        Me.saS612.Margin = New System.Windows.Forms.Padding(2)
        Me.saS612.Name = "saS612"
        Me.saS612.Size = New System.Drawing.Size(38, 33)
        Me.saS612.TabIndex = 0
        '
        'tabOptions_Page7
        '
        Me.tabOptions_Page7.Controls.Add(Me.cmdExerciseGroup1x24_1x8_2W_LF)
        Me.tabOptions_Page7.Controls.Add(Me.cmdS700ResetAll)
        Me.tabOptions_Page7.Controls.Add(Me.panS701)
        Me.tabOptions_Page7.Controls.Add(Me.panS751)
        Me.tabOptions_Page7.Location = New System.Drawing.Point(4, 40)
        Me.tabOptions_Page7.Name = "tabOptions_Page7"
        Me.tabOptions_Page7.Size = New System.Drawing.Size(593, 380)
        Me.tabOptions_Page7.TabIndex = 6
        Me.tabOptions_Page7.Text = " 1x24, 1x8-2W-LF     "
        '
        'cmdExerciseGroup1x24_1x8_2W_LF
        '
        Me.cmdExerciseGroup1x24_1x8_2W_LF.BackColor = System.Drawing.SystemColors.Control
        Me.cmdExerciseGroup1x24_1x8_2W_LF.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdExerciseGroup1x24_1x8_2W_LF.Location = New System.Drawing.Point(118, 196)
        Me.cmdExerciseGroup1x24_1x8_2W_LF.Name = "cmdExerciseGroup1x24_1x8_2W_LF"
        Me.cmdExerciseGroup1x24_1x8_2W_LF.Size = New System.Drawing.Size(117, 23)
        Me.cmdExerciseGroup1x24_1x8_2W_LF.TabIndex = 222
        Me.cmdExerciseGroup1x24_1x8_2W_LF.Text = "&Exercise Group"
        Me.cmdExerciseGroup1x24_1x8_2W_LF.UseVisualStyleBackColor = False
        '
        'cmdS700ResetAll
        '
        Me.cmdS700ResetAll.BackColor = System.Drawing.SystemColors.Control
        Me.cmdS700ResetAll.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdS700ResetAll.Location = New System.Drawing.Point(16, 196)
        Me.cmdS700ResetAll.Name = "cmdS700ResetAll"
        Me.cmdS700ResetAll.Size = New System.Drawing.Size(97, 23)
        Me.cmdS700ResetAll.TabIndex = 145
        Me.cmdS700ResetAll.Text = "&Open Group"
        Me.cmdS700ResetAll.UseVisualStyleBackColor = False
        '
        'panS701
        '
        Me.panS701.AutoScroll = True
        Me.panS701.BackColor = System.Drawing.Color.Silver
        Me.panS701.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panS701.Controls.Add(Me.lblS701Row2)
        Me.panS701.Controls.Add(Me.lblS701)
        Me.panS701.Controls.Add(Me.lblS701Row1)
        Me.panS701.Controls.Add(Me.saS701)
        Me.panS701.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panS701.Location = New System.Drawing.Point(8, 28)
        Me.panS701.Name = "panS701"
        Me.panS701.Size = New System.Drawing.Size(571, 77)
        Me.panS701.TabIndex = 142
        '
        'lblS701Row2
        '
        Me.lblS701Row2.BackColor = System.Drawing.Color.Silver
        Me.lblS701Row2.Font = New System.Drawing.Font("Microsoft Sans Serif", 5.0!)
        Me.lblS701Row2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblS701Row2.Location = New System.Drawing.Point(22, 30)
        Me.lblS701Row2.Name = "lblS701Row2"
        Me.lblS701Row2.Size = New System.Drawing.Size(15, 13)
        Me.lblS701Row2.TabIndex = 156
        Me.lblS701Row2.Text = "2"
        Me.lblS701Row2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblS701
        '
        Me.lblS701.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblS701.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblS701.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblS701.Location = New System.Drawing.Point(1, 1)
        Me.lblS701.Name = "lblS701"
        Me.lblS701.Size = New System.Drawing.Size(41, 18)
        Me.lblS701.TabIndex = 154
        Me.lblS701.Text = "S701"
        Me.lblS701.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblS701Row1
        '
        Me.lblS701Row1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblS701Row1.Font = New System.Drawing.Font("Microsoft Sans Serif", 5.0!)
        Me.lblS701Row1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblS701Row1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblS701Row1.Location = New System.Drawing.Point(22, 17)
        Me.lblS701Row1.Name = "lblS701Row1"
        Me.lblS701Row1.Size = New System.Drawing.Size(15, 13)
        Me.lblS701Row1.TabIndex = 143
        Me.lblS701Row1.Text = "1"
        Me.lblS701Row1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'saS701
        '
        Me.saS701.Count = 24
        Me.saS701.Location = New System.Drawing.Point(41, 0)
        Me.saS701.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.saS701.Name = "saS701"
        Me.saS701.Size = New System.Drawing.Size(864, 52)
        Me.saS701.TabIndex = 155
        '
        'panS751
        '
        Me.panS751.BackColor = System.Drawing.Color.Silver
        Me.panS751.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panS751.Controls.Add(Me.Label1)
        Me.panS751.Controls.Add(Me.lblS751Row)
        Me.panS751.Controls.Add(Me.lblS751)
        Me.panS751.Controls.Add(Me.saS751)
        Me.panS751.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panS751.Location = New System.Drawing.Point(8, 124)
        Me.panS751.Name = "panS751"
        Me.panS751.Size = New System.Drawing.Size(571, 52)
        Me.panS751.TabIndex = 155
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Silver
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 5.0!)
        Me.Label1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label1.Location = New System.Drawing.Point(22, 30)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(15, 13)
        Me.Label1.TabIndex = 157
        Me.Label1.Text = "2"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblS751Row
        '
        Me.lblS751Row.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblS751Row.Font = New System.Drawing.Font("Microsoft Sans Serif", 5.0!)
        Me.lblS751Row.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblS751Row.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblS751Row.Location = New System.Drawing.Point(22, 16)
        Me.lblS751Row.Name = "lblS751Row"
        Me.lblS751Row.Size = New System.Drawing.Size(15, 13)
        Me.lblS751Row.TabIndex = 158
        Me.lblS751Row.Text = "1"
        Me.lblS751Row.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblS751
        '
        Me.lblS751.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblS751.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblS751.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblS751.Location = New System.Drawing.Point(1, 1)
        Me.lblS751.Name = "lblS751"
        Me.lblS751.Size = New System.Drawing.Size(41, 18)
        Me.lblS751.TabIndex = 156
        Me.lblS751.Text = "S751"
        Me.lblS751.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'saS751
        '
        Me.saS751.Count = 8
        Me.saS751.Location = New System.Drawing.Point(41, 0)
        Me.saS751.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.saS751.Name = "saS751"
        Me.saS751.Size = New System.Drawing.Size(288, 52)
        Me.saS751.TabIndex = 159
        '
        'tabOptions_Page8
        '
        Me.tabOptions_Page8.Controls.Add(Me.cmdExerciseGroup1x8_Coax_MF)
        Me.tabOptions_Page8.Controls.Add(Me.cmdS800ResetAll)
        Me.tabOptions_Page8.Controls.Add(Me.panS800_0)
        Me.tabOptions_Page8.Controls.Add(Me.panS800_2)
        Me.tabOptions_Page8.Controls.Add(Me.panS800_3)
        Me.tabOptions_Page8.Controls.Add(Me.panS800_1)
        Me.tabOptions_Page8.Location = New System.Drawing.Point(4, 40)
        Me.tabOptions_Page8.Name = "tabOptions_Page8"
        Me.tabOptions_Page8.Size = New System.Drawing.Size(593, 380)
        Me.tabOptions_Page8.TabIndex = 7
        Me.tabOptions_Page8.Text = " 1x8-Coax-MF     "
        '
        'cmdExerciseGroup1x8_Coax_MF
        '
        Me.cmdExerciseGroup1x8_Coax_MF.BackColor = System.Drawing.SystemColors.Control
        Me.cmdExerciseGroup1x8_Coax_MF.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdExerciseGroup1x8_Coax_MF.Location = New System.Drawing.Point(302, 69)
        Me.cmdExerciseGroup1x8_Coax_MF.Name = "cmdExerciseGroup1x8_Coax_MF"
        Me.cmdExerciseGroup1x8_Coax_MF.Size = New System.Drawing.Size(118, 23)
        Me.cmdExerciseGroup1x8_Coax_MF.TabIndex = 223
        Me.cmdExerciseGroup1x8_Coax_MF.Text = "&Exercise Group"
        Me.cmdExerciseGroup1x8_Coax_MF.UseVisualStyleBackColor = False
        '
        'cmdS800ResetAll
        '
        Me.cmdS800ResetAll.BackColor = System.Drawing.SystemColors.Control
        Me.cmdS800ResetAll.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdS800ResetAll.Location = New System.Drawing.Point(302, 36)
        Me.cmdS800ResetAll.Name = "cmdS800ResetAll"
        Me.cmdS800ResetAll.Size = New System.Drawing.Size(100, 23)
        Me.cmdS800ResetAll.TabIndex = 175
        Me.cmdS800ResetAll.Text = "&Open Group"
        Me.cmdS800ResetAll.UseVisualStyleBackColor = False
        '
        'panS800_0
        '
        Me.panS800_0.BackColor = System.Drawing.Color.Silver
        Me.panS800_0.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panS800_0.Controls.Add(Me.lblS800_0)
        Me.panS800_0.Controls.Add(Me.lblS800Pin1_0)
        Me.panS800_0.Controls.Add(Me.saS801)
        Me.panS800_0.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panS800_0.Location = New System.Drawing.Point(8, 28)
        Me.panS800_0.Name = "panS800_0"
        Me.panS800_0.Size = New System.Drawing.Size(136, 128)
        Me.panS800_0.TabIndex = 159
        '
        'lblS800_0
        '
        Me.lblS800_0.AutoSize = True
        Me.lblS800_0.BackColor = System.Drawing.Color.Transparent
        Me.lblS800_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblS800_0.Location = New System.Drawing.Point(1, 1)
        Me.lblS800_0.Name = "lblS800_0"
        Me.lblS800_0.Size = New System.Drawing.Size(32, 13)
        Me.lblS800_0.TabIndex = 161
        Me.lblS800_0.Text = "S801"
        '
        'lblS800Pin1_0
        '
        Me.lblS800Pin1_0.BackColor = System.Drawing.Color.Silver
        Me.lblS800Pin1_0.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!)
        Me.lblS800Pin1_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblS800Pin1_0.Location = New System.Drawing.Point(33, 52)
        Me.lblS800Pin1_0.Margin = New System.Windows.Forms.Padding(0)
        Me.lblS800Pin1_0.Name = "lblS800Pin1_0"
        Me.lblS800Pin1_0.Size = New System.Drawing.Size(10, 13)
        Me.lblS800Pin1_0.TabIndex = 162
        Me.lblS800Pin1_0.Text = "1"
        '
        'saS801
        '
        Me.saS801.Location = New System.Drawing.Point(46, 0)
        Me.saS801.Margin = New System.Windows.Forms.Padding(2)
        Me.saS801.Name = "saS801"
        Me.saS801.Pin1Text = "2"
        Me.saS801.Pin2Text = "3"
        Me.saS801.Pin3Text = "4"
        Me.saS801.Pin4Text = "5"
        Me.saS801.Pin5Text = "6"
        Me.saS801.Pin6Text = "7"
        Me.saS801.Pin7Text = "8"
        Me.saS801.Pin8Text = "9"
        Me.saS801.Pin9Text = "NC"
        Me.saS801.Size = New System.Drawing.Size(82, 120)
        Me.saS801.SwitchState = SwitchLib.ctl1x8Mux.SwitchStates.Pin9
        Me.saS801.TabIndex = 163
        '
        'panS800_2
        '
        Me.panS800_2.BackColor = System.Drawing.Color.Silver
        Me.panS800_2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panS800_2.Controls.Add(Me.lblS800_2)
        Me.panS800_2.Controls.Add(Me.lblS800Pin1_2)
        Me.panS800_2.Controls.Add(Me.saS803)
        Me.panS800_2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panS800_2.Location = New System.Drawing.Point(155, 28)
        Me.panS800_2.Name = "panS800_2"
        Me.panS800_2.Size = New System.Drawing.Size(136, 128)
        Me.panS800_2.TabIndex = 167
        '
        'lblS800_2
        '
        Me.lblS800_2.AutoSize = True
        Me.lblS800_2.BackColor = System.Drawing.Color.Transparent
        Me.lblS800_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblS800_2.Location = New System.Drawing.Point(2, 1)
        Me.lblS800_2.Name = "lblS800_2"
        Me.lblS800_2.Size = New System.Drawing.Size(32, 13)
        Me.lblS800_2.TabIndex = 169
        Me.lblS800_2.Text = "S803"
        '
        'lblS800Pin1_2
        '
        Me.lblS800Pin1_2.BackColor = System.Drawing.Color.Silver
        Me.lblS800Pin1_2.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!)
        Me.lblS800Pin1_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblS800Pin1_2.Location = New System.Drawing.Point(31, 53)
        Me.lblS800Pin1_2.Margin = New System.Windows.Forms.Padding(0)
        Me.lblS800Pin1_2.Name = "lblS800Pin1_2"
        Me.lblS800Pin1_2.Size = New System.Drawing.Size(10, 13)
        Me.lblS800Pin1_2.TabIndex = 168
        Me.lblS800Pin1_2.Text = "1"
        Me.lblS800Pin1_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'saS803
        '
        Me.saS803.Location = New System.Drawing.Point(46, 0)
        Me.saS803.Margin = New System.Windows.Forms.Padding(2)
        Me.saS803.Name = "saS803"
        Me.saS803.Pin1Text = "2"
        Me.saS803.Pin2Text = "3"
        Me.saS803.Pin3Text = "4"
        Me.saS803.Pin4Text = "5"
        Me.saS803.Pin5Text = "6"
        Me.saS803.Pin6Text = "7"
        Me.saS803.Pin7Text = "8"
        Me.saS803.Pin8Text = "9"
        Me.saS803.Pin9Text = "NC"
        Me.saS803.Size = New System.Drawing.Size(82, 120)
        Me.saS803.SwitchState = SwitchLib.ctl1x8Mux.SwitchStates.Pin9
        Me.saS803.TabIndex = 170
        '
        'panS800_3
        '
        Me.panS800_3.BackColor = System.Drawing.Color.Silver
        Me.panS800_3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panS800_3.Controls.Add(Me.lblS800_3)
        Me.panS800_3.Controls.Add(Me.lblS800Pin1_3)
        Me.panS800_3.Controls.Add(Me.saS804)
        Me.panS800_3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panS800_3.Location = New System.Drawing.Point(155, 167)
        Me.panS800_3.Name = "panS800_3"
        Me.panS800_3.Size = New System.Drawing.Size(136, 128)
        Me.panS800_3.TabIndex = 171
        '
        'lblS800_3
        '
        Me.lblS800_3.AutoSize = True
        Me.lblS800_3.BackColor = System.Drawing.Color.Transparent
        Me.lblS800_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblS800_3.Location = New System.Drawing.Point(1, 1)
        Me.lblS800_3.Name = "lblS800_3"
        Me.lblS800_3.Size = New System.Drawing.Size(32, 13)
        Me.lblS800_3.TabIndex = 173
        Me.lblS800_3.Text = "S804"
        '
        'lblS800Pin1_3
        '
        Me.lblS800Pin1_3.BackColor = System.Drawing.Color.Silver
        Me.lblS800Pin1_3.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!)
        Me.lblS800Pin1_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblS800Pin1_3.Location = New System.Drawing.Point(31, 51)
        Me.lblS800Pin1_3.Margin = New System.Windows.Forms.Padding(0)
        Me.lblS800Pin1_3.Name = "lblS800Pin1_3"
        Me.lblS800Pin1_3.Size = New System.Drawing.Size(10, 13)
        Me.lblS800Pin1_3.TabIndex = 172
        Me.lblS800Pin1_3.Text = "1"
        '
        'saS804
        '
        Me.saS804.Location = New System.Drawing.Point(46, 0)
        Me.saS804.Margin = New System.Windows.Forms.Padding(2)
        Me.saS804.Name = "saS804"
        Me.saS804.Pin1Text = "2"
        Me.saS804.Pin2Text = "3"
        Me.saS804.Pin3Text = "4"
        Me.saS804.Pin4Text = "5"
        Me.saS804.Pin5Text = "6"
        Me.saS804.Pin6Text = "7"
        Me.saS804.Pin7Text = "8"
        Me.saS804.Pin8Text = "9"
        Me.saS804.Pin9Text = "NC"
        Me.saS804.Size = New System.Drawing.Size(82, 120)
        Me.saS804.SwitchState = SwitchLib.ctl1x8Mux.SwitchStates.Pin9
        Me.saS804.TabIndex = 174
        '
        'panS800_1
        '
        Me.panS800_1.BackColor = System.Drawing.Color.Silver
        Me.panS800_1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panS800_1.Controls.Add(Me.lblS800_1)
        Me.panS800_1.Controls.Add(Me.lblS800Pin1_1)
        Me.panS800_1.Controls.Add(Me.saS802)
        Me.panS800_1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panS800_1.Location = New System.Drawing.Point(8, 167)
        Me.panS800_1.Name = "panS800_1"
        Me.panS800_1.Size = New System.Drawing.Size(136, 128)
        Me.panS800_1.TabIndex = 163
        '
        'lblS800_1
        '
        Me.lblS800_1.AutoSize = True
        Me.lblS800_1.BackColor = System.Drawing.Color.Transparent
        Me.lblS800_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblS800_1.Location = New System.Drawing.Point(2, 1)
        Me.lblS800_1.Name = "lblS800_1"
        Me.lblS800_1.Size = New System.Drawing.Size(32, 13)
        Me.lblS800_1.TabIndex = 165
        Me.lblS800_1.Text = "S802"
        '
        'lblS800Pin1_1
        '
        Me.lblS800Pin1_1.BackColor = System.Drawing.Color.Silver
        Me.lblS800Pin1_1.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!)
        Me.lblS800Pin1_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblS800Pin1_1.Location = New System.Drawing.Point(33, 53)
        Me.lblS800Pin1_1.Margin = New System.Windows.Forms.Padding(0)
        Me.lblS800Pin1_1.Name = "lblS800Pin1_1"
        Me.lblS800Pin1_1.Size = New System.Drawing.Size(10, 13)
        Me.lblS800Pin1_1.TabIndex = 164
        Me.lblS800Pin1_1.Text = "1"
        '
        'saS802
        '
        Me.saS802.Location = New System.Drawing.Point(46, 0)
        Me.saS802.Margin = New System.Windows.Forms.Padding(2)
        Me.saS802.Name = "saS802"
        Me.saS802.Pin1Text = "2"
        Me.saS802.Pin2Text = "3"
        Me.saS802.Pin3Text = "4"
        Me.saS802.Pin4Text = "5"
        Me.saS802.Pin5Text = "6"
        Me.saS802.Pin6Text = "7"
        Me.saS802.Pin7Text = "8"
        Me.saS802.Pin8Text = "9"
        Me.saS802.Pin9Text = "NC"
        Me.saS802.Size = New System.Drawing.Size(82, 120)
        Me.saS802.SwitchState = SwitchLib.ctl1x8Mux.SwitchStates.Pin9
        Me.saS802.TabIndex = 166
        '
        'tabOptions_Page9
        '
        Me.tabOptions_Page9.Controls.Add(Me.panS900_3)
        Me.tabOptions_Page9.Controls.Add(Me.cmdExerciseGroup1x6_Coax_HF)
        Me.tabOptions_Page9.Controls.Add(Me.cmdS900ResetAll)
        Me.tabOptions_Page9.Controls.Add(Me.panS900_0)
        Me.tabOptions_Page9.Controls.Add(Me.panS900_1)
        Me.tabOptions_Page9.Controls.Add(Me.panS900_2)
        Me.tabOptions_Page9.Controls.Add(Me.panS900_4)
        Me.tabOptions_Page9.Controls.Add(Me.panS900_5)
        Me.tabOptions_Page9.Location = New System.Drawing.Point(4, 40)
        Me.tabOptions_Page9.Name = "tabOptions_Page9"
        Me.tabOptions_Page9.Size = New System.Drawing.Size(593, 380)
        Me.tabOptions_Page9.TabIndex = 8
        Me.tabOptions_Page9.Text = " 1x6-Coax-HF     "
        '
        'panS900_3
        '
        Me.panS900_3.BackColor = System.Drawing.Color.Silver
        Me.panS900_3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panS900_3.Controls.Add(Me.lblS900_3)
        Me.panS900_3.Controls.Add(Me.lblS900Pin1_3)
        Me.panS900_3.Controls.Add(Me.saS904)
        Me.panS900_3.Location = New System.Drawing.Point(147, 162)
        Me.panS900_3.Name = "panS900_3"
        Me.panS900_3.Size = New System.Drawing.Size(128, 97)
        Me.panS900_3.TabIndex = 188
        '
        'lblS900_3
        '
        Me.lblS900_3.AutoSize = True
        Me.lblS900_3.BackColor = System.Drawing.Color.Transparent
        Me.lblS900_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblS900_3.Location = New System.Drawing.Point(1, 1)
        Me.lblS900_3.Name = "lblS900_3"
        Me.lblS900_3.Size = New System.Drawing.Size(32, 13)
        Me.lblS900_3.TabIndex = 189
        Me.lblS900_3.Text = "S904"
        '
        'lblS900Pin1_3
        '
        Me.lblS900Pin1_3.BackColor = System.Drawing.Color.Silver
        Me.lblS900Pin1_3.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!)
        Me.lblS900Pin1_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblS900Pin1_3.Location = New System.Drawing.Point(27, 39)
        Me.lblS900Pin1_3.Name = "lblS900Pin1_3"
        Me.lblS900Pin1_3.Size = New System.Drawing.Size(10, 13)
        Me.lblS900Pin1_3.TabIndex = 191
        Me.lblS900Pin1_3.Text = "1"
        '
        'saS904
        '
        Me.saS904.Location = New System.Drawing.Point(41, 0)
        Me.saS904.Margin = New System.Windows.Forms.Padding(2)
        Me.saS904.Name = "saS904"
        Me.saS904.Pin1Text = "2"
        Me.saS904.Pin2Text = "3"
        Me.saS904.Pin3Text = "4"
        Me.saS904.Pin4Text = "5"
        Me.saS904.Pin5Text = "6"
        Me.saS904.Pin6Text = "7"
        Me.saS904.Pin7Text = "NC"
        Me.saS904.Size = New System.Drawing.Size(83, 95)
        Me.saS904.SwitchState = SwitchLib.ctl1x6Mux.SwitchStates.Pin7
        Me.saS904.TabIndex = 192
        '
        'cmdExerciseGroup1x6_Coax_HF
        '
        Me.cmdExerciseGroup1x6_Coax_HF.BackColor = System.Drawing.SystemColors.Control
        Me.cmdExerciseGroup1x6_Coax_HF.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdExerciseGroup1x6_Coax_HF.Location = New System.Drawing.Point(117, 283)
        Me.cmdExerciseGroup1x6_Coax_HF.Name = "cmdExerciseGroup1x6_Coax_HF"
        Me.cmdExerciseGroup1x6_Coax_HF.Size = New System.Drawing.Size(114, 23)
        Me.cmdExerciseGroup1x6_Coax_HF.TabIndex = 224
        Me.cmdExerciseGroup1x6_Coax_HF.Text = "&Exercise Group"
        Me.cmdExerciseGroup1x6_Coax_HF.UseVisualStyleBackColor = False
        '
        'cmdS900ResetAll
        '
        Me.cmdS900ResetAll.BackColor = System.Drawing.SystemColors.Control
        Me.cmdS900ResetAll.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdS900ResetAll.Location = New System.Drawing.Point(8, 283)
        Me.cmdS900ResetAll.Name = "cmdS900ResetAll"
        Me.cmdS900ResetAll.Size = New System.Drawing.Size(101, 23)
        Me.cmdS900ResetAll.TabIndex = 200
        Me.cmdS900ResetAll.Text = "&Open Group"
        Me.cmdS900ResetAll.UseVisualStyleBackColor = False
        '
        'panS900_0
        '
        Me.panS900_0.BackColor = System.Drawing.Color.Silver
        Me.panS900_0.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panS900_0.Controls.Add(Me.lblS900_0)
        Me.panS900_0.Controls.Add(Me.lblS900Pin1_0)
        Me.panS900_0.Controls.Add(Me.saS901)
        Me.panS900_0.Location = New System.Drawing.Point(8, 28)
        Me.panS900_0.Name = "panS900_0"
        Me.panS900_0.Size = New System.Drawing.Size(128, 97)
        Me.panS900_0.TabIndex = 176
        '
        'lblS900_0
        '
        Me.lblS900_0.BackColor = System.Drawing.Color.Transparent
        Me.lblS900_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblS900_0.Location = New System.Drawing.Point(1, 1)
        Me.lblS900_0.Name = "lblS900_0"
        Me.lblS900_0.Size = New System.Drawing.Size(41, 17)
        Me.lblS900_0.TabIndex = 178
        Me.lblS900_0.Text = "S901"
        '
        'lblS900Pin1_0
        '
        Me.lblS900Pin1_0.BackColor = System.Drawing.Color.Silver
        Me.lblS900Pin1_0.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!)
        Me.lblS900Pin1_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblS900Pin1_0.Location = New System.Drawing.Point(28, 39)
        Me.lblS900Pin1_0.Name = "lblS900Pin1_0"
        Me.lblS900Pin1_0.Size = New System.Drawing.Size(10, 13)
        Me.lblS900Pin1_0.TabIndex = 177
        Me.lblS900Pin1_0.Text = "1"
        '
        'saS901
        '
        Me.saS901.Location = New System.Drawing.Point(41, 0)
        Me.saS901.Margin = New System.Windows.Forms.Padding(2)
        Me.saS901.Name = "saS901"
        Me.saS901.Pin1Text = "2"
        Me.saS901.Pin2Text = "3"
        Me.saS901.Pin3Text = "4"
        Me.saS901.Pin4Text = "5"
        Me.saS901.Pin5Text = "6"
        Me.saS901.Pin6Text = "7"
        Me.saS901.Pin7Text = "NC"
        Me.saS901.Size = New System.Drawing.Size(83, 95)
        Me.saS901.SwitchState = SwitchLib.ctl1x6Mux.SwitchStates.Pin7
        Me.saS901.TabIndex = 179
        '
        'panS900_1
        '
        Me.panS900_1.BackColor = System.Drawing.Color.Silver
        Me.panS900_1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panS900_1.Controls.Add(Me.lblS900_1)
        Me.panS900_1.Controls.Add(Me.lblS900Pin1_1)
        Me.panS900_1.Controls.Add(Me.saS902)
        Me.panS900_1.Location = New System.Drawing.Point(8, 162)
        Me.panS900_1.Name = "panS900_1"
        Me.panS900_1.Size = New System.Drawing.Size(128, 97)
        Me.panS900_1.TabIndex = 180
        '
        'lblS900_1
        '
        Me.lblS900_1.AutoSize = True
        Me.lblS900_1.BackColor = System.Drawing.Color.Transparent
        Me.lblS900_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblS900_1.Location = New System.Drawing.Point(1, 1)
        Me.lblS900_1.Name = "lblS900_1"
        Me.lblS900_1.Size = New System.Drawing.Size(32, 13)
        Me.lblS900_1.TabIndex = 181
        Me.lblS900_1.Text = "S902"
        '
        'lblS900Pin1_1
        '
        Me.lblS900Pin1_1.BackColor = System.Drawing.Color.Silver
        Me.lblS900Pin1_1.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!)
        Me.lblS900Pin1_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblS900Pin1_1.Location = New System.Drawing.Point(28, 39)
        Me.lblS900Pin1_1.Margin = New System.Windows.Forms.Padding(0)
        Me.lblS900Pin1_1.Name = "lblS900Pin1_1"
        Me.lblS900Pin1_1.Size = New System.Drawing.Size(10, 13)
        Me.lblS900Pin1_1.TabIndex = 183
        Me.lblS900Pin1_1.Text = "1"
        '
        'saS902
        '
        Me.saS902.Location = New System.Drawing.Point(41, 0)
        Me.saS902.Margin = New System.Windows.Forms.Padding(2)
        Me.saS902.Name = "saS902"
        Me.saS902.Pin1Text = "2"
        Me.saS902.Pin2Text = "3"
        Me.saS902.Pin3Text = "4"
        Me.saS902.Pin4Text = "5"
        Me.saS902.Pin5Text = "6"
        Me.saS902.Pin6Text = "7"
        Me.saS902.Pin7Text = "NC"
        Me.saS902.Size = New System.Drawing.Size(83, 95)
        Me.saS902.SwitchState = SwitchLib.ctl1x6Mux.SwitchStates.Pin7
        Me.saS902.TabIndex = 184
        '
        'panS900_2
        '
        Me.panS900_2.BackColor = System.Drawing.Color.Silver
        Me.panS900_2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panS900_2.Controls.Add(Me.lblS900_2)
        Me.panS900_2.Controls.Add(Me.lblS900Pin1_2)
        Me.panS900_2.Controls.Add(Me.saS903)
        Me.panS900_2.Location = New System.Drawing.Point(147, 28)
        Me.panS900_2.Name = "panS900_2"
        Me.panS900_2.Size = New System.Drawing.Size(128, 97)
        Me.panS900_2.TabIndex = 184
        '
        'lblS900_2
        '
        Me.lblS900_2.AutoSize = True
        Me.lblS900_2.BackColor = System.Drawing.Color.Transparent
        Me.lblS900_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblS900_2.Location = New System.Drawing.Point(1, 1)
        Me.lblS900_2.Name = "lblS900_2"
        Me.lblS900_2.Size = New System.Drawing.Size(32, 13)
        Me.lblS900_2.TabIndex = 185
        Me.lblS900_2.Text = "S903"
        '
        'lblS900Pin1_2
        '
        Me.lblS900Pin1_2.BackColor = System.Drawing.Color.Silver
        Me.lblS900Pin1_2.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!)
        Me.lblS900Pin1_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblS900Pin1_2.Location = New System.Drawing.Point(27, 38)
        Me.lblS900Pin1_2.Name = "lblS900Pin1_2"
        Me.lblS900Pin1_2.Size = New System.Drawing.Size(10, 13)
        Me.lblS900Pin1_2.TabIndex = 187
        Me.lblS900Pin1_2.Text = "1"
        '
        'saS903
        '
        Me.saS903.Location = New System.Drawing.Point(41, 0)
        Me.saS903.Margin = New System.Windows.Forms.Padding(2)
        Me.saS903.Name = "saS903"
        Me.saS903.Pin1Text = "2"
        Me.saS903.Pin2Text = "3"
        Me.saS903.Pin3Text = "4"
        Me.saS903.Pin4Text = "5"
        Me.saS903.Pin5Text = "6"
        Me.saS903.Pin6Text = "7"
        Me.saS903.Pin7Text = "NC"
        Me.saS903.Size = New System.Drawing.Size(83, 95)
        Me.saS903.SwitchState = SwitchLib.ctl1x6Mux.SwitchStates.Pin7
        Me.saS903.TabIndex = 188
        '
        'panS900_4
        '
        Me.panS900_4.BackColor = System.Drawing.Color.Silver
        Me.panS900_4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panS900_4.Controls.Add(Me.lblS900_4)
        Me.panS900_4.Controls.Add(Me.lblS900Pin1_4)
        Me.panS900_4.Controls.Add(Me.saS905)
        Me.panS900_4.Location = New System.Drawing.Point(276, 28)
        Me.panS900_4.Name = "panS900_4"
        Me.panS900_4.Size = New System.Drawing.Size(128, 97)
        Me.panS900_4.TabIndex = 192
        '
        'lblS900_4
        '
        Me.lblS900_4.AutoSize = True
        Me.lblS900_4.BackColor = System.Drawing.Color.Transparent
        Me.lblS900_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblS900_4.Location = New System.Drawing.Point(1, 1)
        Me.lblS900_4.Name = "lblS900_4"
        Me.lblS900_4.Size = New System.Drawing.Size(32, 13)
        Me.lblS900_4.TabIndex = 193
        Me.lblS900_4.Text = "S905"
        '
        'lblS900Pin1_4
        '
        Me.lblS900Pin1_4.BackColor = System.Drawing.Color.Silver
        Me.lblS900Pin1_4.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!)
        Me.lblS900Pin1_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblS900Pin1_4.Location = New System.Drawing.Point(28, 39)
        Me.lblS900Pin1_4.Name = "lblS900Pin1_4"
        Me.lblS900Pin1_4.Size = New System.Drawing.Size(10, 13)
        Me.lblS900Pin1_4.TabIndex = 195
        Me.lblS900Pin1_4.Text = "1"
        '
        'saS905
        '
        Me.saS905.Location = New System.Drawing.Point(41, 0)
        Me.saS905.Margin = New System.Windows.Forms.Padding(2)
        Me.saS905.Name = "saS905"
        Me.saS905.Pin1Text = "2"
        Me.saS905.Pin2Text = "3"
        Me.saS905.Pin3Text = "4"
        Me.saS905.Pin4Text = "5"
        Me.saS905.Pin5Text = "6"
        Me.saS905.Pin6Text = "7"
        Me.saS905.Pin7Text = "NC"
        Me.saS905.Size = New System.Drawing.Size(83, 95)
        Me.saS905.SwitchState = SwitchLib.ctl1x6Mux.SwitchStates.Pin7
        Me.saS905.TabIndex = 196
        '
        'panS900_5
        '
        Me.panS900_5.BackColor = System.Drawing.Color.Silver
        Me.panS900_5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panS900_5.Controls.Add(Me.lblS900_5)
        Me.panS900_5.Controls.Add(Me.lblS900Pin1_5)
        Me.panS900_5.Controls.Add(Me.saS906)
        Me.panS900_5.Location = New System.Drawing.Point(276, 162)
        Me.panS900_5.Name = "panS900_5"
        Me.panS900_5.Size = New System.Drawing.Size(128, 97)
        Me.panS900_5.TabIndex = 196
        '
        'lblS900_5
        '
        Me.lblS900_5.AutoSize = True
        Me.lblS900_5.BackColor = System.Drawing.Color.Transparent
        Me.lblS900_5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblS900_5.Location = New System.Drawing.Point(1, 1)
        Me.lblS900_5.Name = "lblS900_5"
        Me.lblS900_5.Size = New System.Drawing.Size(32, 13)
        Me.lblS900_5.TabIndex = 197
        Me.lblS900_5.Text = "S906"
        '
        'lblS900Pin1_5
        '
        Me.lblS900Pin1_5.BackColor = System.Drawing.Color.Silver
        Me.lblS900Pin1_5.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!)
        Me.lblS900Pin1_5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblS900Pin1_5.Location = New System.Drawing.Point(28, 38)
        Me.lblS900Pin1_5.Name = "lblS900Pin1_5"
        Me.lblS900Pin1_5.Size = New System.Drawing.Size(10, 13)
        Me.lblS900Pin1_5.TabIndex = 199
        Me.lblS900Pin1_5.Text = "1"
        '
        'saS906
        '
        Me.saS906.Location = New System.Drawing.Point(41, 0)
        Me.saS906.Margin = New System.Windows.Forms.Padding(2)
        Me.saS906.Name = "saS906"
        Me.saS906.Pin1Text = "2"
        Me.saS906.Pin2Text = "3"
        Me.saS906.Pin3Text = "4"
        Me.saS906.Pin4Text = "5"
        Me.saS906.Pin5Text = "6"
        Me.saS906.Pin6Text = "7"
        Me.saS906.Pin7Text = "NC"
        Me.saS906.Size = New System.Drawing.Size(83, 95)
        Me.saS906.SwitchState = SwitchLib.ctl1x6Mux.SwitchStates.Pin7
        Me.saS906.TabIndex = 200
        '
        'tabOptions_Page10
        '
        Me.tabOptions_Page10.Controls.Add(Me.fraPinList)
        Me.tabOptions_Page10.Controls.Add(Me.cmdAbout)
        Me.tabOptions_Page10.Controls.Add(Me.cmdTest)
        Me.tabOptions_Page10.Controls.Add(Me.fraRelayExerciser)
        Me.tabOptions_Page10.Controls.Add(Me.cmdOpenAll)
        Me.tabOptions_Page10.Controls.Add(Me.Panel_Conifg)
        Me.tabOptions_Page10.Location = New System.Drawing.Point(4, 40)
        Me.tabOptions_Page10.Name = "tabOptions_Page10"
        Me.tabOptions_Page10.Size = New System.Drawing.Size(593, 380)
        Me.tabOptions_Page10.TabIndex = 9
        Me.tabOptions_Page10.Text = " Options     "
        '
        'fraPinList
        '
        Me.fraPinList.Controls.Add(Me.panSortSig)
        Me.fraPinList.Controls.Add(Me.panSortCnx)
        Me.fraPinList.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraPinList.Location = New System.Drawing.Point(8, 24)
        Me.fraPinList.Name = "fraPinList"
        Me.fraPinList.Size = New System.Drawing.Size(387, 228)
        Me.fraPinList.TabIndex = 201
        Me.fraPinList.TabStop = False
        Me.fraPinList.Text = "Switch Pin List"
        '
        'panSortSig
        '
        Me.panSortSig.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panSortSig.Controls.Add(Me.grdSortSig)
        Me.panSortSig.Controls.Add(Me.lblSortSigCnx)
        Me.panSortSig.Controls.Add(Me.lblSortSigSig)
        Me.panSortSig.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panSortSig.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.panSortSig.Location = New System.Drawing.Point(8, 20)
        Me.panSortSig.Name = "panSortSig"
        Me.panSortSig.Size = New System.Drawing.Size(178, 199)
        Me.panSortSig.TabIndex = 202
        '
        'grdSortSig
        '
        Me.grdSortSig.AllowUserToAddRows = False
        Me.grdSortSig.AllowUserToDeleteRows = False
        Me.grdSortSig.AllowUserToResizeColumns = False
        Me.grdSortSig.AllowUserToResizeRows = False
        Me.grdSortSig.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
        Me.grdSortSig.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        Me.grdSortSig.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdSortSig.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.dgvSwitch, Me.dgvReceiver})
        Me.grdSortSig.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdSortSig.Location = New System.Drawing.Point(0, 0)
        Me.grdSortSig.Name = "grdSortSig"
        Me.grdSortSig.ReadOnly = True
        Me.grdSortSig.RowHeadersVisible = False
        Me.grdSortSig.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.grdSortSig.Size = New System.Drawing.Size(174, 195)
        Me.grdSortSig.TabIndex = 203
        '
        'dgvSwitch
        '
        Me.dgvSwitch.HeaderText = "Switch"
        Me.dgvSwitch.Name = "dgvSwitch"
        Me.dgvSwitch.ReadOnly = True
        Me.dgvSwitch.Width = 80
        '
        'dgvReceiver
        '
        Me.dgvReceiver.HeaderText = "Receiver"
        Me.dgvReceiver.Name = "dgvReceiver"
        Me.dgvReceiver.ReadOnly = True
        Me.dgvReceiver.Width = 85
        '
        'lblSortSigCnx
        '
        Me.lblSortSigCnx.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblSortSigCnx.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSortSigCnx.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblSortSigCnx.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblSortSigCnx.Location = New System.Drawing.Point(67, 16)
        Me.lblSortSigCnx.Name = "lblSortSigCnx"
        Me.lblSortSigCnx.Size = New System.Drawing.Size(67, 17)
        Me.lblSortSigCnx.TabIndex = 205
        Me.lblSortSigCnx.Text = "Receiver"
        Me.lblSortSigCnx.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblSortSigSig
        '
        Me.lblSortSigSig.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblSortSigSig.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSortSigSig.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblSortSigSig.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblSortSigSig.Location = New System.Drawing.Point(1, 16)
        Me.lblSortSigSig.Name = "lblSortSigSig"
        Me.lblSortSigSig.Size = New System.Drawing.Size(67, 17)
        Me.lblSortSigSig.TabIndex = 204
        Me.lblSortSigSig.Text = "Switch"
        Me.lblSortSigSig.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'panSortCnx
        '
        Me.panSortCnx.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panSortCnx.Controls.Add(Me.grdSortCnx)
        Me.panSortCnx.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panSortCnx.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.panSortCnx.Location = New System.Drawing.Point(201, 20)
        Me.panSortCnx.Name = "panSortCnx"
        Me.panSortCnx.Size = New System.Drawing.Size(178, 199)
        Me.panSortCnx.TabIndex = 206
        '
        'grdSortCnx
        '
        Me.grdSortCnx.AllowUserToAddRows = False
        Me.grdSortCnx.AllowUserToDeleteRows = False
        Me.grdSortCnx.AllowUserToResizeColumns = False
        Me.grdSortCnx.AllowUserToResizeRows = False
        Me.grdSortCnx.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
        Me.grdSortCnx.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        Me.grdSortCnx.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdSortCnx.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cvxReceiver, Me.cvxSwitch})
        Me.grdSortCnx.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdSortCnx.Location = New System.Drawing.Point(0, 0)
        Me.grdSortCnx.Name = "grdSortCnx"
        Me.grdSortCnx.ReadOnly = True
        Me.grdSortCnx.RowHeadersVisible = False
        Me.grdSortCnx.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.grdSortCnx.Size = New System.Drawing.Size(174, 195)
        Me.grdSortCnx.TabIndex = 207
        '
        'cvxReceiver
        '
        Me.cvxReceiver.HeaderText = "Receiver"
        Me.cvxReceiver.Name = "cvxReceiver"
        Me.cvxReceiver.ReadOnly = True
        Me.cvxReceiver.Width = 80
        '
        'cvxSwitch
        '
        Me.cvxSwitch.HeaderText = "Switch"
        Me.cvxSwitch.Name = "cvxSwitch"
        Me.cvxSwitch.ReadOnly = True
        Me.cvxSwitch.Width = 85
        '
        'cmdAbout
        '
        Me.cmdAbout.BackColor = System.Drawing.SystemColors.Control
        Me.cmdAbout.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAbout.Location = New System.Drawing.Point(8, 259)
        Me.cmdAbout.Name = "cmdAbout"
        Me.cmdAbout.Size = New System.Drawing.Size(76, 23)
        Me.cmdAbout.TabIndex = 10
        Me.cmdAbout.Text = "&About"
        Me.cmdAbout.UseVisualStyleBackColor = False
        '
        'cmdTest
        '
        Me.cmdTest.BackColor = System.Drawing.SystemColors.Control
        Me.cmdTest.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdTest.Location = New System.Drawing.Point(89, 259)
        Me.cmdTest.Name = "cmdTest"
        Me.cmdTest.Size = New System.Drawing.Size(101, 23)
        Me.cmdTest.TabIndex = 11
        Me.cmdTest.Text = "Built-In &Test"
        Me.cmdTest.UseVisualStyleBackColor = False
        '
        'fraRelayExerciser
        '
        Me.fraRelayExerciser.Controls.Add(Me.cmdExercise)
        Me.fraRelayExerciser.Controls.Add(Me.cboCardType)
        Me.fraRelayExerciser.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraRelayExerciser.Location = New System.Drawing.Point(401, 174)
        Me.fraRelayExerciser.Name = "fraRelayExerciser"
        Me.fraRelayExerciser.Size = New System.Drawing.Size(181, 90)
        Me.fraRelayExerciser.TabIndex = 212
        Me.fraRelayExerciser.TabStop = False
        Me.fraRelayExerciser.Text = "Relay Exerciser"
        '
        'cmdExercise
        '
        Me.cmdExercise.BackColor = System.Drawing.SystemColors.Control
        Me.cmdExercise.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdExercise.Location = New System.Drawing.Point(90, 57)
        Me.cmdExercise.Name = "cmdExercise"
        Me.cmdExercise.Size = New System.Drawing.Size(72, 23)
        Me.cmdExercise.TabIndex = 214
        Me.cmdExercise.Text = "&Exercise"
        Me.cmdExercise.UseVisualStyleBackColor = False
        '
        'cboCardType
        '
        Me.cboCardType.BackColor = System.Drawing.SystemColors.Window
        Me.cboCardType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCardType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboCardType.Location = New System.Drawing.Point(8, 24)
        Me.cboCardType.Name = "cboCardType"
        Me.cboCardType.Size = New System.Drawing.Size(154, 21)
        Me.cboCardType.TabIndex = 213
        '
        'cmdOpenAll
        '
        Me.cmdOpenAll.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOpenAll.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOpenAll.Location = New System.Drawing.Point(195, 259)
        Me.cmdOpenAll.Name = "cmdOpenAll"
        Me.cmdOpenAll.Size = New System.Drawing.Size(88, 23)
        Me.cmdOpenAll.TabIndex = 215
        Me.cmdOpenAll.Text = "&Open All"
        Me.cmdOpenAll.UseVisualStyleBackColor = False
        '
        'Panel_Conifg
        '
        Me.Panel_Conifg.BackColor = System.Drawing.SystemColors.Control
        Me.Panel_Conifg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Panel_Conifg.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Panel_Conifg.Location = New System.Drawing.Point(401, 28)
        Me.Panel_Conifg.Name = "Panel_Conifg"
        Me.Panel_Conifg.Parent_Object = Nothing
        Me.Panel_Conifg.Refresh = CType(0, Short)
        Me.Panel_Conifg.Size = New System.Drawing.Size(181, 139)
        Me.Panel_Conifg.TabIndex = 225
        '
        'tabOptions_Page11
        '
        Me.tabOptions_Page11.Controls.Add(Me.Atlas_SFP)
        Me.tabOptions_Page11.Location = New System.Drawing.Point(4, 40)
        Me.tabOptions_Page11.Name = "tabOptions_Page11"
        Me.tabOptions_Page11.Size = New System.Drawing.Size(593, 380)
        Me.tabOptions_Page11.TabIndex = 10
        Me.tabOptions_Page11.Text = " ATLAS     "
        '
        'Atlas_SFP
        '
        Me.Atlas_SFP.ATLAS = Nothing
        Me.Atlas_SFP.BackColor = System.Drawing.SystemColors.Control
        Me.Atlas_SFP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Atlas_SFP.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Atlas_SFP.Location = New System.Drawing.Point(49, 44)
        Me.Atlas_SFP.Name = "Atlas_SFP"
        Me.Atlas_SFP.Parent_Object = Nothing
        Me.Atlas_SFP.Size = New System.Drawing.Size(397, 211)
        Me.Atlas_SFP.TabIndex = 226
        '
        'sbrUserInformation
        '
        Me.sbrUserInformation.Location = New System.Drawing.Point(0, 482)
        Me.sbrUserInformation.Name = "sbrUserInformation"
        Me.sbrUserInformation.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.sbrUserInformation_Panel1, Me.sbrUserInformation_Panel2})
        Me.sbrUserInformation.Size = New System.Drawing.Size(622, 17)
        Me.sbrUserInformation.SizingGrip = False
        Me.sbrUserInformation.TabIndex = 0
        '
        'sbrUserInformation_Panel1
        '
        Me.sbrUserInformation_Panel1.MinWidth = 48
        Me.sbrUserInformation_Panel1.Name = "sbrUserInformation_Panel1"
        Me.sbrUserInformation_Panel1.Text = "Running"
        Me.sbrUserInformation_Panel1.Width = 48
        '
        'sbrUserInformation_Panel2
        '
        Me.sbrUserInformation_Panel2.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
        Me.sbrUserInformation_Panel2.Name = "sbrUserInformation_Panel2"
        Me.sbrUserInformation_Panel2.Width = 539
        '
        'pcp1x6Mux
        '
        Me.pcp1x6Mux.Location = New System.Drawing.Point(170, 259)
        Me.pcp1x6Mux.Name = "pcp1x6Mux"
        Me.pcp1x6Mux.Size = New System.Drawing.Size(100, 50)
        Me.pcp1x6Mux.TabIndex = 219
        Me.pcp1x6Mux.TabStop = False
        '
        'lblDdeLink
        '
        Me.lblDdeLink.BackColor = System.Drawing.SystemColors.Control
        Me.lblDdeLink.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDdeLink.Location = New System.Drawing.Point(164, 438)
        Me.lblDdeLink.Name = "lblDdeLink"
        Me.lblDdeLink.Size = New System.Drawing.Size(47, 18)
        Me.lblDdeLink.TabIndex = 210
        Me.lblDdeLink.Text = "DDE Link"
        Me.lblDdeLink.Visible = False
        '
        'frmRac1260
        '
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(622, 499)
        Me.Controls.Add(Me.cmdHelp)
        Me.Controls.Add(Me.cmdQuit)
        Me.Controls.Add(Me.cmdReset)
        Me.Controls.Add(Me.tabOptions)
        Me.Controls.Add(Me.sbrUserInformation)
        Me.Controls.Add(Me.pcp1x6Mux)
        Me.Controls.Add(Me.lblDdeLink)
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmRac1260"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Switching"
        Me.tabOptions.ResumeLayout(False)
        Me.tabOptions_Page1.ResumeLayout(False)
        Me.panS101.ResumeLayout(False)
        Me.panS101.PerformLayout()
        Me.tabOptions_Page2.ResumeLayout(False)
        Me.panS200_0.ResumeLayout(False)
        Me.panS200_0.PerformLayout()
        Me.panS200_1.ResumeLayout(False)
        Me.panS200_1.PerformLayout()
        Me.tabOptions_Page3.ResumeLayout(False)
        Me.panS301.ResumeLayout(False)
        Me.tabOptions_Page4.ResumeLayout(False)
        Me.panS400_5.ResumeLayout(False)
        Me.panS400_4.ResumeLayout(False)
        Me.panS400_3.ResumeLayout(False)
        Me.panS400_2.ResumeLayout(False)
        Me.panS400_1.ResumeLayout(False)
        Me.panS400_0.ResumeLayout(False)
        Me.tabOptions_Page5.ResumeLayout(False)
        Me.panS500_9.ResumeLayout(False)
        Me.panS500_8.ResumeLayout(False)
        Me.panS500_7.ResumeLayout(False)
        Me.panS500_6.ResumeLayout(False)
        Me.panS500_5.ResumeLayout(False)
        Me.panS500_4.ResumeLayout(False)
        Me.panS500_3.ResumeLayout(False)
        Me.panS500_2.ResumeLayout(False)
        Me.panS500_1.ResumeLayout(False)
        Me.panS500_0.ResumeLayout(False)
        Me.tabOptions_Page6.ResumeLayout(False)
        Me.panS600_0.ResumeLayout(False)
        Me.panS600_1.ResumeLayout(False)
        Me.panS600_2.ResumeLayout(False)
        Me.panS600_3.ResumeLayout(False)
        Me.panS600_4.ResumeLayout(False)
        Me.panS600_5.ResumeLayout(False)
        Me.panS600_6.ResumeLayout(False)
        Me.panS600_7.ResumeLayout(False)
        Me.panS600_8.ResumeLayout(False)
        Me.panS600_9.ResumeLayout(False)
        Me.panS600_10.ResumeLayout(False)
        Me.panS600_11.ResumeLayout(False)
        Me.tabOptions_Page7.ResumeLayout(False)
        Me.panS701.ResumeLayout(False)
        Me.panS751.ResumeLayout(False)
        Me.tabOptions_Page8.ResumeLayout(False)
        Me.panS800_0.ResumeLayout(False)
        Me.panS800_0.PerformLayout()
        Me.panS800_2.ResumeLayout(False)
        Me.panS800_2.PerformLayout()
        Me.panS800_3.ResumeLayout(False)
        Me.panS800_3.PerformLayout()
        Me.panS800_1.ResumeLayout(False)
        Me.panS800_1.PerformLayout()
        Me.tabOptions_Page9.ResumeLayout(False)
        Me.panS900_3.ResumeLayout(False)
        Me.panS900_3.PerformLayout()
        Me.panS900_0.ResumeLayout(False)
        Me.panS900_1.ResumeLayout(False)
        Me.panS900_1.PerformLayout()
        Me.panS900_2.ResumeLayout(False)
        Me.panS900_2.PerformLayout()
        Me.panS900_4.ResumeLayout(False)
        Me.panS900_4.PerformLayout()
        Me.panS900_5.ResumeLayout(False)
        Me.panS900_5.PerformLayout()
        Me.tabOptions_Page10.ResumeLayout(False)
        Me.fraPinList.ResumeLayout(False)
        Me.panSortSig.ResumeLayout(False)
        CType(Me.grdSortSig, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panSortCnx.ResumeLayout(False)
        CType(Me.grdSortCnx, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraRelayExerciser.ResumeLayout(False)
        Me.tabOptions_Page11.ResumeLayout(False)
        CType(Me.sbrUserInformation_Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sbrUserInformation_Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pcp1x6Mux, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

End Sub
#End Region

    '=========================================================

    Private _updatingFromFile As Boolean
    Property UpdatingFromFile() As Boolean
        Get
            Return _updatingFromFile
        End Get
        Set(value As Boolean)
            _updatingFromFile = value
            If value Then
                Me.Cursor = Cursors.WaitCursor
                Me.Refresh()
            Else
                If RfOptionInstalled Then
                    Me.tabOptions.SelectedIndex = 9
                Else
                    Me.tabOptions.SelectedIndex = 8
                End If

                Me.Cursor = Cursors.Default
            End If
        End Set
    End Property

    Private Sub cmdAbout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAbout.Click
        frmAbout.cmdOK.Visible = True
       frmAbout.ShowDialog()
    End Sub

    Public Function Build_Atlas() As String
        Build_Atlas = ""
        Build_Atlas = "CONNECT, SHORT, CNX FROM TO $"
    End Function

    Public Sub ConfigGetCurrent()
        Dim SwInfo As String = ""
        Dim nLoop As Short = 0
        Dim nswitchSet As Short = 0
        Dim nGrid As Short = 0
        Dim tmpString As String = ""
        Dim dtmpNum As Double = 0.0
        Dim dswitch As Double = 0.0
        Dim i As Short = 0
        Dim currentTab As Integer = tabOptions.SelectedIndex

        nswitchSet = 0

        If PanelConifgPushed = True Then
            DoNotTalk = True
            cmdS101Reset_Click()
            cmdS301ResetAll_Click()
            cmdS200ResetAll_Click()
            cmdS400ResetAll_Click()
            cmdS500ResetAll_Click()
            cmdS600ResetAll_Click()
            cmdS700ResetAll_Click()
            cmdS800ResetAll_Click()
            cmdS900ResetAll_Click()
            PanelConifgPushed = False
            DoNotTalk = False
        End If

        'Read Switch Settings for Address 001
        WriteInstrument("PD 1")
        '####################################
        'Action Required: Remove debug code
        'Fill return value selections
        'gcolCmds.Add "001. 1260-39 HIGH DENSITY BASIC SWITCH MODULE"
        'gcolCmds.Add "001. 0000-0003,1000,1024-1027,1033,1035,1037,1039,2001,2100"
        'gcolCmds.Add "001. 2201,2300,3000-3003,3100,3101,3103,3201,3202,4001-4006,"
        'gcolCmds.Add "001. 4110,4213-4217,4403,4405,4411,4412,4417"
        'gcolCmds.Add "001. END"
        '####################################
        SwInfo = ReadSwitch()

        If InStr(SwInfo, "001. 1260-39") <> 0 Then
            SwInfo = Mid(SwInfo, InStr(SwInfo, vbCrLf) + 7) 'remove first line

            SwInfo = ReadSwitch()
            Do
                If Len(SwInfo) <> 4 Then
                    '================================================================
                    ' First half of the DC Power tab
                    If InStr(SwInfo, "0000") Or InStr(SwInfo, "0001") Or InStr(SwInfo, "0002") Or InStr(SwInfo, "0003") Or InStr(SwInfo, "0004") Then
                        SetDCPower(SwInfo, RI1260_39_MODULE)
                    End If
                    '================================================================
                    ' First half of the 1X1-1W LF tab
                    ' just checking to see if any switch on this card is set
                    For i = 1000 To 1047
                        tmpString = CStr(i)
                        If InStr(SwInfo, tmpString) Then
                            nswitchSet += 1
                        End If
                    Next
                    'if switch is set then go set it, else move on
                    If nswitchSet > 0 Then
                        nswitchSet = 0
                        SetS301(SwInfo, RI1260_39_MODULE)
                    End If

                    '================================================================
                    ' First half of the 1X2-1W LF tab
                    ' see if this set of switches are set
                    If InStr(SwInfo, "2000") Or InStr(SwInfo, "2001") Or InStr(SwInfo, "2100") Or InStr(SwInfo, "2101") Or InStr(SwInfo, "2200") Or InStr(SwInfo, "2201") Or InStr(SwInfo, "2300") Or InStr(SwInfo, "2301") Or InStr(SwInfo, "2400") Or InStr(SwInfo, "2401") Or InStr(SwInfo, "2500") Or InStr(SwInfo, "2501") Then
                        SetS600(SwInfo, RI1260_39_MODULE)
                    End If
                    '================================================================
                    ' First half of the 1X4-1W LF tab
                    If InStr(SwInfo, "3000") Or InStr(SwInfo, "3001") Or InStr(SwInfo, "3002") Or InStr(SwInfo, "3003") Or InStr(SwInfo, "3100") Or InStr(SwInfo, "3101") Or InStr(SwInfo, "3102") Or InStr(SwInfo, "3103") Or InStr(SwInfo, "3200") Or InStr(SwInfo, "3201") Or InStr(SwInfo, "3202") Or InStr(SwInfo, "3203") Then
                        SetS400(SwInfo, RI1260_39_MODULE)

                    End If

                    '================================================================
                    ' First half of the 2X8-1W LF tab
                    For i = 4000 To 4017
                        tmpString = CStr(i)
                        If InStr(SwInfo, tmpString) Then
                            nswitchSet += 1
                        End If
                    Next
                    For i = 4100 To 4117
                        tmpString = CStr(i)
                        If InStr(SwInfo, tmpString) Then
                            nswitchSet += 1
                        End If
                    Next
                    For i = 4200 To 4217
                        tmpString = CStr(i)
                        If InStr(SwInfo, tmpString) Then
                            nswitchSet += 1
                        End If
                    Next
                    For i = 4300 To 4317
                        tmpString = CStr(i)
                        If InStr(SwInfo, tmpString) Then
                            nswitchSet += 1
                        End If
                    Next
                    For i = 4400 To 4417
                        tmpString = CStr(i)
                        If InStr(SwInfo, tmpString) Then
                            nswitchSet += 1
                        End If
                    Next
                    'if switch is set then go set it, else move on
                    If nswitchSet > 0 Then
                        nswitchSet = 0
                        SetS500(SwInfo, RI1260_39_MODULE)
                    End If
                    '================================================================
                End If

                SwInfo = ReadSwitch()
            Loop While InStr(SwInfo, "001.END") = 0
        End If

        'Read Switch Settings for Address 002
        WriteInstrument("PD 2")
        '####################################
        'Action Required: Remove debug code
        'Fill return value selections
        'gcolCmds.Add "002. 1260-39 HIGH DENSITY BASIC SWITCH MODULE"
        'gcolCmds.Add "002. 0000,0001,0004,1000-1016,1027-1033,1035,1037,1039,2001,"
        'gcolCmds.Add "002. 2400,2501,3001,3003,3101,3102,3201,3203,4001,4112,"
        'gcolCmds.Add "002. 4300-4307,4310-4317,4401,4406,4410,4413,4417"
        'gcolCmds.Add "002. END"
        '####################################
        SwInfo = ReadSwitch()

        If InStr(SwInfo, "002. 1260-39") <> 0 Then
            SwInfo = Mid(SwInfo, InStr(SwInfo, vbCrLf) + 7) 'remove first line

            SwInfo = ReadSwitch()
            Do
                If Len(SwInfo) <> 4 Then
                    '================================================================
                    ' Second half of the DC Power tab
                    If InStr(SwInfo, "0000") Or InStr(SwInfo, "0001") Or InStr(SwInfo, "0002") Or InStr(SwInfo, "0003") Or InStr(SwInfo, "0004") Then
                        SetDCPower(SwInfo, RI1260_39S_MODULE)
                    End If

                    '================================================================
                    ' Second half of the 1X1-1W LF tab
                    ' just see if one of the switches is this set, if it is go ahead and set switch
                    For i = 1000 To 1047
                        tmpString = CStr(i)
                        If InStr(SwInfo, tmpString) Then
                            nswitchSet += 1
                        End If
                    Next
                    If nswitchSet > 0 Then
                        nswitchSet = 0
                        SetS301(SwInfo, RI1260_39S_MODULE)
                    End If
                    '================================================================
                    ' Second half of the 1X2-1W LF tab
                    If InStr(SwInfo, "2000") Or InStr(SwInfo, "2001") Or InStr(SwInfo, "2100") Or InStr(SwInfo, "2101") Or InStr(SwInfo, "2200") Or InStr(SwInfo, "2201") Or InStr(SwInfo, "2300") Or InStr(SwInfo, "2301") Or InStr(SwInfo, "2400") Or InStr(SwInfo, "2401") Or InStr(SwInfo, "2500") Or InStr(SwInfo, "2501") Then
                        SetS600(SwInfo, RI1260_39S_MODULE)
                    End If
                    '================================================================
                    ' Second half of the 1X4-1W LF tab
                    If InStr(SwInfo, "3000") Or InStr(SwInfo, "3001") Or InStr(SwInfo, "3002") Or InStr(SwInfo, "3003") Or InStr(SwInfo, "3100") Or InStr(SwInfo, "3101") Or InStr(SwInfo, "3102") Or InStr(SwInfo, "3103") Or InStr(SwInfo, "3200") Or InStr(SwInfo, "3201") Or InStr(SwInfo, "3202") Or InStr(SwInfo, "3203") Then
                        SetS400(SwInfo, RI1260_39S_MODULE)
                    End If

                    '================================================================
                    ' Second half of the 2X8-1W LF tab
                    For i = 4000 To 4017
                        tmpString = CStr(i)
                        If InStr(SwInfo, tmpString) Then
                            nswitchSet += 1
                        End If
                    Next
                    For i = 4100 To 4117
                        tmpString = CStr(i)
                        If InStr(SwInfo, tmpString) Then
                            nswitchSet += 1
                        End If
                    Next
                    For i = 4200 To 4217
                        tmpString = CStr(i)
                        If InStr(SwInfo, tmpString) Then
                            nswitchSet += 1
                        End If
                    Next
                    For i = 4300 To 4317
                        tmpString = CStr(i)
                        If InStr(SwInfo, tmpString) Then
                            nswitchSet += 1
                        End If
                    Next
                    For i = 4400 To 4417
                        tmpString = CStr(i)
                        If InStr(SwInfo, tmpString) Then
                            nswitchSet += 1
                        End If
                    Next
                    'if switch is set then go set it, else move on
                    If nswitchSet > 0 Then
                        nswitchSet = 0
                        SetS500(SwInfo, RI1260_39S_MODULE)
                    End If
                    '================================================================
                End If

                SwInfo = ReadSwitch()
            Loop While InStr(SwInfo, "002.END") = 0
        End If

        'Read Switch Settings for Address 003
        WriteInstrument("PD 3")
        '####################################
        'Action Required: Remove debug code
        'Fill return value selections
        'gcolCmds.Add "003. 1260-38A 1x128 2-WIRE SCANNER/MULTIPLEXER"
        'gcolCmds.Add "003. 0003,0005,0007,0011,0022-0027,0033,0035,0037,0040,0051,"
        'gcolCmds.Add "003. 0055,0060-0067,0077,0080-0087,0092,0095,0097,0122-0127,"
        'gcolCmds.Add "003. 0133,0135,0137,0140"
        'gcolCmds.Add "003. END"
        '####################################
        SwInfo = ReadSwitch()

        If InStr(SwInfo, "003. 1260-38A") <> 0 Then
            SwInfo = Mid(SwInfo, InStr(SwInfo, vbCrLf) + 7) 'remove first line

            SwInfo = ReadSwitch()
            Do
                If Len(SwInfo) <> 4 Then
                    SetSwitch38A(SwInfo)
                End If

                SwInfo = ReadSwitch()
            Loop While InStr(SwInfo, "003.END") = 0
        End If

        'Read Switch Settings for Address 004
        WriteInstrument("PD 4")
        '####################################
        'Action Required: Remove debug code
        'Fill return value selections
        'gcolCmds.Add "004. 1260-58 4 1x8 750 MHZ SWITCH MODULE"
        'gcolCmds.Add "004. 02,13,25,37"
        'gcolCmds.Add "004. END"
        '####################################
        SwInfo = ReadSwitch()

        If InStr(SwInfo, "004. 1260-58") <> 0 Then
            Me.tabOptions.SelectedIndex = 7
            SwInfo = Mid(SwInfo, InStr(SwInfo, vbCrLf) + 7) 'remove first line

            SwInfo = ReadSwitch()
            dtmpNum = 0
            Do
                If Len(SwInfo) <> 4 Then
                    For dswitch = 0 To 30 Step 10
                        nGrid = dswitch / 10
                        For nLoop = 0 To 7
                            dtmpNum = nLoop + dswitch
                            tmpString = dtmpNum.ToString("00")
                            If InStr((Mid(SwInfo, 3)), tmpString) Then
                                Select Case nGrid
                                    Case 0
                                        Set1x8Mux(saS801, nLoop)

                                    Case 1
                                        Set1x8Mux(saS802, nLoop)

                                    Case 2
                                        Set1x8Mux(saS803, nLoop)

                                    Case 3
                                        Set1x8Mux(saS804, nLoop)
                                End Select
                            End If
                        Next nLoop
                    Next dswitch
                End If
                SwInfo = ReadSwitch()
            Loop While InStr(SwInfo, "004.END") = 0
        End If

        'Read Switch Settings for Address 005
        '    If RfOptionInstalled% = True Then
        WriteInstrument("PD 5")
        '####################################
        'Action Required: Remove debug code
        'Fill return value selections
        '   gcolCmds.Add "005. 1260-66A SIX 1x6 SWITCHING MODULE"
        '   gcolCmds.Add "005. 00,11,22,33,44,55"
        '   gcolCmds.Add "005. END"
        '####################################
        SwInfo = ReadSwitch()

        If InStr(SwInfo, "005. 1260-66A") <> 0 Then
            Me.tabOptions.SelectedIndex = 8
            SwInfo = Mid(SwInfo, InStr(SwInfo, vbCrLf) + 7) 'remove first line

            SwInfo = ReadSwitch()
            dtmpNum = 0
            nGrid = 0
            Do
                If Len(SwInfo) <> 4 Then
                    For dswitch = 0 To 50 Step 10
                        nGrid = dswitch / 10
                        For nLoop = 0 To 6
                            dtmpNum = nLoop + dswitch
                            tmpString = dtmpNum.ToString("00")
                            If InStr((Mid(SwInfo, 3)), tmpString) Then
                                Select Case nGrid
                                    Case 0
                                        Set1x6Mux(saS901, nLoop)

                                    Case 1
                                        Set1x6Mux(saS902, nLoop)

                                    Case 2
                                        Set1x6Mux(saS903, nLoop)

                                    Case 3
                                        Set1x6Mux(saS904, nLoop)

                                    Case 4
                                        Set1x6Mux(saS905, nLoop)

                                    Case 5
                                        Set1x6Mux(saS906, nLoop)
                                End Select
                            End If
                        Next nLoop
                    Next dswitch
                End If
                SwInfo = ReadSwitch()
            Loop While InStr(SwInfo, "005.END") = 0
        End If
        tabOptions.SelectedIndex = currentTab
    End Sub

    Private Sub SetDCPower(ByVal sSwRng As String, ByVal card As Short)
        Dim sParameter As String
        Dim nEnd As Short
        Dim i As Short
        Dim nCol As Short
        Dim j As Integer

        Me.tabOptions.SelectedIndex = 0
        For i = 0 To 4
            sParameter = i.ToString("0000")
            If card = RI1260_39_MODULE Then
                nCol = 0
            ElseIf card = RI1260_39S_MODULE Then
                nCol = 1
            End If
            If InStr(sSwRng, sParameter) Or nEnd > i Then
                'Look for combined switches
                If nEnd = 0 And Mid(sSwRng, InStr(sSwRng, sParameter) + 4, 1) = "-" Then
                    nEnd = CInt(Mid(sSwRng, InStr(sSwRng, sParameter) + 5, 4))
                End If

                For j = 0 To saS101.Count - 1
                    If saS101.Switches(j).Row = i And saS101.Switches(j).Column = nCol Then
                        saS101.Switches(j).SwitchState = DPST.SwitchStates.Closed
                        saS101.Switches(j).Text = "Close"
                        iStatus = 1
                        Exit For
                    End If
                Next
            End If
        Next i
        saS101.Refresh()
    End Sub

    Private Sub SetS301(ByVal sSwRng As String, ByVal card As Short)
        Dim sParameter As String
        Dim nEnd As Short
        Dim nCol As Short
        Dim nFactor As Short
        Dim d As Short
        Dim i As Short
        Dim j As Integer

        Me.tabOptions.SelectedIndex = 2
        For d = 0 To 2
            If d = 0 Then
                nFactor = 1000
            ElseIf d = 1 Then
                nFactor = 1016
            ElseIf d = 2 Then
                nFactor = 1032
            End If
            For i = 0 To 15
                sParameter = CStr(i + nFactor)
                If InStr(sSwRng, sParameter) Or nEnd - nFactor > i Then
                    'Look for combined switches
                    If nEnd = sParameter Then
                        nEnd = 0
                    End If

                    If nEnd = 0 And Mid(sSwRng, InStr(sSwRng, sParameter) + 4, 1) = "-" Then
                        nEnd = CInt(Mid(sSwRng, InStr(sSwRng, sParameter) + 5, 4))
                    End If

                    If d = 0 Then
                        If card = RI1260_39_MODULE Then
                            nCol = 0
                        ElseIf card = RI1260_39S_MODULE Then
                            nCol = 3
                        End If
                    ElseIf d = 1 Then
                        If card = RI1260_39_MODULE Then
                            nCol = 1
                        ElseIf card = RI1260_39S_MODULE Then
                            nCol = 4
                        End If
                    ElseIf d = 2 Then
                        If card = RI1260_39_MODULE Then
                            nCol = 2
                        ElseIf card = RI1260_39S_MODULE Then
                            nCol = 5
                        End If
                    End If

                    For j = 0 To saS301.Count - 1
                        If (saS301.Switches(j).Row = i) And (saS301.Switches(j).Column = nCol) Then
                            saS301.Switches(j).Text = "Close"
                            saS301.Switches(j).SwitchState = SPST.SwitchStates.Closed
                            iStatus = 1
                            Exit For
                        End If
                    Next
                End If
            Next i
        Next d
        saS301.Refresh()
    End Sub

    Private Sub SetS600(ByVal sSwRng As String, ByVal card As Short)
        Dim sParameter As String
        Dim nGrid As Short
        Dim switch As Short
        Dim i As Short
        Dim nEnd As Short

        Me.tabOptions.SelectedIndex = 5
        For switch = 0 To 5
            For i = 0 To 1
                sParameter = ((switch * 100) + i).ToString("2000")
                If InStr(sSwRng, sParameter) Or nEnd - sParameter > i Then

                    If nEnd = sParameter Then
                        nEnd = 0
                    End If
                    'Look for combined switches
                    If nEnd = 0 And Mid(sSwRng, InStr(sSwRng, sParameter) + 4, 1) = "-" Then
                        nEnd = CInt(Mid(sSwRng, InStr(sSwRng, sParameter) + 5, 4))
                    End If
                    'figure out the grid number
                    If card = RI1260_39_MODULE Then
                        nGrid = switch
                    ElseIf card = RI1260_39S_MODULE Then
                        nGrid = switch + 6
                    End If

                    Select Case nGrid
                        Case 0
                            saS601.Switches(i).Text = "Close"
                            saS601.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Closed
                            iStatus = 1

                        Case 1
                            saS602.Switches(i).Text = "Close"
                            saS602.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Closed
                            iStatus = 1

                        Case 2
                            saS603.Switches(i).Text = "Close"
                            saS603.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Closed
                            iStatus = 1

                        Case 3
                            saS604.Switches(i).Text = "Close"
                            saS604.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Closed
                            iStatus = 1

                        Case 4
                            saS605.Switches(i).Text = "Close"
                            saS605.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Closed
                            iStatus = 1

                        Case 5
                            saS606.Switches(i).Text = "Close"
                            saS606.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Closed
                            iStatus = 1

                        Case 6
                            saS607.Switches(i).Text = "Close"
                            saS607.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Closed
                            iStatus = 1

                        Case 7
                            saS608.Switches(i).Text = "Close"
                            saS608.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Closed
                            iStatus = 1

                        Case 8
                            saS609.Switches(i).Text = "Close"
                            saS609.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Closed
                            iStatus = 1

                        Case 9
                            saS610.Switches(i).Text = "Close"
                            saS610.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Closed
                            iStatus = 1

                        Case 10
                            saS611.Switches(i).Text = "Close"
                            saS611.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Closed
                            iStatus = 1

                        Case 11
                            saS612.Switches(i).Text = "Close"
                            saS612.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Closed
                            iStatus = 1
                    End Select
                End If
            Next i
        Next switch
        tabOptions_Page6.Refresh()
    End Sub

    Private Sub SetS400(ByVal sSwRng As String, ByVal card As Short)
        Dim sParameter As String
        Dim nEnd As Short
        Dim nFactor As Short
        Dim nGrid As Short
        Dim g As Short
        Dim i As Short

        Me.tabOptions.SelectedIndex = 3
        For g = 0 To 2
            'mh nFactor = 4000 + (g * 100)
            '1x4 is 3000 series
            nFactor = 3000 + (g * 100)
            If card = RI1260_39_MODULE Then
                If nFactor = 3000 Then
                    nGrid = 0
                ElseIf nFactor = 3100 Then
                    nGrid = 1
                ElseIf nFactor = 3200 Then
                    nGrid = 2
                End If
            ElseIf card = RI1260_39S_MODULE Then
                If nFactor = 3000 Then
                    nGrid = 3
                ElseIf nFactor = 3100 Then
                    nGrid = 4
                ElseIf nFactor = 3200 Then
                    nGrid = 5
                End If
            End If
            ' nEnd = 0 'reset
            For i = 0 To 3
                sParameter = CStr(nFactor + i)
                If InStr(sSwRng, sParameter) Or sParameter <= nEnd Then 'nEnd - sParameter > i Then
                    'Look for combined switches
                    If nEnd = sParameter Then
                        nEnd = 0
                    End If
                    If nEnd = 0 And Mid(sSwRng, InStr(sSwRng, sParameter) + 4, 1) = "-" Then
                        nEnd = CInt(Mid(sSwRng, InStr(sSwRng, sParameter) + 5, 4))
                    End If

                    'mh see what card it is
                    Select Case nGrid
                        Case 0
                            saS401.Switches(i).Text = "Close"
                            saS401.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Closed
                            iStatus = 1

                        Case 1
                            saS402.Switches(i).Text = "Close"
                            saS402.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Closed
                            iStatus = 1

                        Case 2
                            saS403.Switches(i).Text = "Close"
                            saS403.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Closed
                            iStatus = 1

                        Case 3
                            saS404.Switches(i).Text = "Close"
                            saS404.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Closed
                            iStatus = 1

                        Case 4
                            saS405.Switches(i).Text = "Close"
                            saS405.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Closed
                            iStatus = 1

                        Case 5
                            saS406.Switches(i).Text = "Close"
                            saS406.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Closed
                            iStatus = 1
                    End Select
                End If
            Next i
        Next g
        tabOptions_Page4.Refresh()
    End Sub

    Private Sub SetS500(ByVal sSwRng As String, ByVal card As Short)
        Dim sParameter As String
        Dim nEnd As Short
        Dim nFactor As Short
        Dim switch As Short
        Dim d As Short
        Dim i As Short
        Dim switchArray As SwitchLib.ctl1W2RowArray = saS501

        Me.tabOptions.SelectedIndex = 4
        For switch = 0 To 4
            nFactor = 4000 + (switch * 100)
            For d = 1 To 2
                nFactor += ((d - 1) * 10)
                'nEnd = 0    'reset
                For i = 0 To 7
                    sParameter = CStr(nFactor + i)
                    If InStr(sSwRng, sParameter) Or sParameter <= nEnd Then 'nEnd - sParameter > i Then

                        If nEnd = sParameter Then
                            nEnd = 0
                        End If
                        'Look for combined switches
                        If nEnd = 0 And Mid(sSwRng, InStr(sSwRng, sParameter) + 4, 1) = "-" Then
                            nEnd = CInt(Mid(sSwRng, InStr(sSwRng, sParameter) + 5, 4))
                        End If

                        If switch = 0 Then
                            If card = RI1260_39_MODULE Then
                                switchArray = saS501
                            ElseIf card = RI1260_39S_MODULE Then
                                switchArray = saS506
                            End If
                        ElseIf switch = 1 Then
                            If card = RI1260_39_MODULE Then
                                switchArray = saS502
                            ElseIf card = RI1260_39S_MODULE Then
                                switchArray = saS507
                            End If
                        ElseIf switch = 2 Then
                            If card = RI1260_39_MODULE Then
                                switchArray = saS503
                            ElseIf card = RI1260_39S_MODULE Then
                                switchArray = saS508
                            End If
                        ElseIf switch = 3 Then
                            If card = RI1260_39_MODULE Then
                                switchArray = saS504
                            ElseIf card = RI1260_39S_MODULE Then
                                switchArray = saS509
                            End If
                        ElseIf switch = 4 Then
                            If card = RI1260_39_MODULE Then
                                switchArray = saS505
                            ElseIf card = RI1260_39S_MODULE Then
                                switchArray = saS510
                            End If
                        End If

                        If d = 1 Then
                            switchArray.Switches(i).TopSwitchText = "Close"
                            switchArray.Switches(i).TopSwitchState = ctl1W2Row.SwitchStates.Closed
                            iStatus = 1
                        Else
                            switchArray.Switches(i).BottomSwitchText = "Close"
                            switchArray.Switches(i).BottomSwitchState = ctl1W2Row.SwitchStates.Closed
                            iStatus = 1
                        End If
                    End If
                Next i
            Next d
        Next switch
        Me.tabOptions_Page5.Refresh()
    End Sub

    Private Sub SetSwitch38A(ByVal sSwRng As String)
        Dim nEnd As Short
        Dim nBottom As Short
        Dim nRow As Short
        Dim i As Short
        Dim nPixIndx As Short
        Dim nLoop As Short
        Dim sParameter As String
        Dim nInner As Short
        Dim nGrid As Short
        Dim switch As Short
        Dim switch200Array As SwitchLib.ctl2W2RowArray
        Dim switch200 As SwitchLib.ctl2W2Row
        nPixIndx = 1 'set inital switch pic for closed
        Dim Col As Integer = 0

        i = 0
        nGrid = 0
        nEnd = -1
        For nLoop = 0 To 15
            For nInner = 0 To 7
                sParameter = nLoop.ToString("000") & nInner

                ' check for closed switches
                If InStr(sSwRng, sParameter) Or sParameter <= nEnd Then
                    If nEnd = sParameter Then
                        nEnd = -1
                    End If

                    'Look for combined switches
                    If nEnd = -1 And Mid(sSwRng, InStr(sSwRng, sParameter) + 4, 1) = "-" Then
                        nEnd = CInt(Mid(sSwRng, InStr(sSwRng, sParameter) + 5, 4))
                    End If

                    If sParameter = "0000" Or sParameter = "0001" Or sParameter = "0002" Or sParameter = "0003" Or sParameter = "0004" Or sParameter = "0005" Or sParameter = "0006" Or sParameter = "0007" Then
                        tabOptions.SelectedIndex = 6
                        nGrid = Convert.ToInt16(sParameter)
                        switch = S751

                        saS751.Switches(nGrid).Text = "Close"
                        saS751.Switches(nGrid).SwitchState = ctl2W1Row.SwitchStates.Closed
                        saS751.Refresh()
                        iStatus = 1
                    Else
                        switch = S701

                        nGrid = Convert.ToInt16(sParameter)
                        If nGrid < 39 Then
                            tabOptions.SelectedIndex = 6
                            If nGrid > 29 Then
                                Col = sParameter - 14
                            ElseIf nGrid > 19 Then
                                Col = sParameter - 12
                            ElseIf nGrid > 9 Then
                                Col = sParameter - 10
                            End If

                            saS701.Switches(Col).Text = "Close"
                            saS701.Switches(Col).SwitchState = ctl2W1Row.SwitchStates.Closed
                            saS701.Refresh()
                            iStatus = 1
                        Else
                            tabOptions.SelectedIndex = 1
                            If nGrid < 100 Then
                                switch200Array = Me.saS201
                            Else
                                switch200Array = Me.saS202
                                nBottom = 1
                            End If

                            If nGrid > 149 Then
                                switch200 = switch200Array.Switches(nGrid - 134)
                                nRow = 2
                            ElseIf nGrid > 139 Then
                                switch200 = switch200Array.Switches(nGrid - 124)
                                nRow = 1
                            ElseIf nGrid > 129 Then
                                switch200 = switch200Array.Switches(nGrid - 122)
                                nRow = 2
                            ElseIf nGrid > 119 Then
                                switch200 = switch200Array.Switches(nGrid - 112)
                                nRow = 1
                            ElseIf nGrid > 109 Then
                                switch200 = switch200Array.Switches(nGrid - 110)
                                nRow = 2
                            ElseIf nGrid > 99 Then
                                switch200 = switch200Array.Switches(nGrid - 100)
                                nRow = 1
                            ElseIf nGrid > 89 Then
                                switch200 = switch200Array.Switches(nGrid - 74)
                                nRow = 2
                            ElseIf nGrid > 79 Then
                                switch200 = switch200Array.Switches(nGrid - 64)
                                nRow = 1
                            ElseIf nGrid > 69 Then
                                switch200 = switch200Array.Switches(nGrid - 62)
                                nRow = 2
                            ElseIf nGrid > 59 Then
                                switch200 = switch200Array.Switches(nGrid - 52)
                                nRow = 1
                            ElseIf nGrid > 49 Then
                                switch200 = switch200Array.Switches(nGrid - 50)
                                nRow = 2
                            ElseIf nGrid > 39 Then
                                switch200 = switch200Array.Switches(nGrid - 40)
                                nRow = 1
                            Else
                                switch200 = switch200Array.Switches(0)
                                nRow = 1
                            End If

                            If nRow = 1 Then
                                switch200.TopSwitchState = ctl2W2Row.SwitchStates.Closed
                                switch200.TopSwitchText = "Close"
                                iStatus = 1
                            Else
                                switch200.BottomSwitchState = ctl2W2Row.SwitchStates.Closed
                                switch200.BottomSwitchText = "Close"
                                iStatus = 1
                            End If
                            tabOptions_Page2.Refresh()
                        End If
                    End If
                End If
            Next nInner
        Next nLoop
    End Sub

    Private Sub GridSw200(ByVal nBottom As Short, ByVal nRow As Short, ByVal nStart As Short, ByVal nEnd As Short)
        Dim i As Short
        Dim switch200Array As SwitchLib.ctl2W2RowArray

        If nBottom = 0 Then
            switch200Array = saS202
        Else
            switch200Array = saS202
        End If
        For i = 0 To switch200Array.Count - 1
            If nStart = i And nEnd < nStart Then
                If nRow = 1 Then
                    switch200Array.Switches(i).TopSwitchText = "Close"
                    switch200Array.Switches(i).TopSwitchState = ctl2W2Row.SwitchStates.Open
                    iStatus = 1
                Else
                    switch200Array.Switches(i).BottomSwitchText = "Close"
                    switch200Array.Switches(i).BottomSwitchState = ctl2W2Row.SwitchStates.Open
                    iStatus = 1
                End If
                Exit For
            ElseIf nStart <= i And nEnd > i Then
                If nRow = 1 Then
                    switch200Array.Switches(i).TopSwitchText = "Close"
                    switch200Array.Switches(i).TopSwitchState = ctl2W2Row.SwitchStates.Open
                    iStatus = 1
                Else
                    switch200Array.Switches(i).BottomSwitchText = "Close"
                    switch200Array.Switches(i).BottomSwitchState = ctl2W2Row.SwitchStates.Open
                    iStatus = 1
                End If
            End If
        Next i
    End Sub

    Public Sub SetMode(ByVal sMode As String)
        'No data to retrive so do nothing
    End Sub

    Public Function GetMode() As String
        GetMode = ""

        'No toolbar to save so just put something for the first line
        GetMode = "SWITCH"
    End Function

    Public Function GetData(ByVal sName As String) As String
        GetData = ""

        Dim sData As String = ""
        'Dim g As Integer
        Dim i As Short

        If sName = "saS101" Then
            For i = 0 To saS101.Count - 1
                sData += saS101.Switches(i).Text + ":"
            Next
        End If

        If sName = "saS201" Then
            For i = 0 To saS201.Count - 1
                sData += saS201.Switches(i).TopSwitchText + ":"
                sData += saS201.Switches(i).BottomSwitchText + ":"
            Next
        End If
        If sName = "saS202" Then
            For i = 0 To saS202.Count - 1
                sData += saS202.Switches(i).TopSwitchText + ":"
                sData += saS202.Switches(i).BottomSwitchText + ":"
            Next
        End If

        If sName = "saS301" Then
            For i = 0 To saS301.Count - 1
                sData += saS301.Switches(i).Text + ":"
            Next
        End If

        If sName = "saS401" Then
            For i = 0 To saS401.Count - 1
                sData += saS401.Switches(i).Text + ":"
            Next
        End If

        If sName = "saS402" Then
            For i = 0 To saS402.Count - 1
                sData += saS402.Switches(i).Text + ":"
            Next
        End If

        If sName = "saS403" Then
            For i = 0 To saS403.Count - 1
                sData += saS403.Switches(i).Text + ":"
            Next
        End If

        If sName = "saS404" Then
            For i = 0 To saS404.Count - 1
                sData += saS404.Switches(i).Text + ":"
            Next
        End If

        If sName = "saS405" Then
            For i = 0 To saS405.Count - 1
                sData += saS405.Switches(i).Text + ":"
            Next
        End If

        If sName = "saS406" Then
            For i = 0 To saS406.Count - 1
                sData += saS406.Switches(i).Text + ":"
            Next
        End If

        If sName = "saS501" Then
            For i = 0 To saS501.Count - 1
                sData += saS501.Switches(i).TopSwitchText + ":"
                sData += saS501.Switches(i).BottomSwitchText + ":"
            Next
        End If

        If sName = "saS502" Then
            For i = 0 To saS502.Count - 1
                sData += saS502.Switches(i).TopSwitchText + ":"
                sData += saS502.Switches(i).BottomSwitchText + ":"
            Next
        End If

        If sName = "saS503" Then
            For i = 0 To saS503.Count - 1
                sData += saS503.Switches(i).TopSwitchText + ":"
                sData += saS503.Switches(i).BottomSwitchText + ":"
            Next
        End If

        If sName = "saS504" Then
            For i = 0 To saS504.Count - 1
                sData += saS504.Switches(i).TopSwitchText + ":"
                sData += saS504.Switches(i).BottomSwitchText + ":"
            Next
        End If

        If sName = "saS505" Then
            For i = 0 To saS505.Count - 1
                sData += saS505.Switches(i).TopSwitchText + ":"
                sData += saS505.Switches(i).BottomSwitchText + ":"
            Next
        End If

        If sName = "saS506" Then
            For i = 0 To saS506.Count - 1
                sData += saS506.Switches(i).TopSwitchText + ":"
                sData += saS506.Switches(i).BottomSwitchText + ":"
            Next
        End If

        If sName = "saS507" Then
            For i = 0 To saS507.Count - 1
                sData += saS507.Switches(i).TopSwitchText + ":"
                sData += saS507.Switches(i).BottomSwitchText + ":"
            Next
        End If

        If sName = "saS508" Then
            For i = 0 To saS508.Count - 1
                sData += saS508.Switches(i).TopSwitchText + ":"
                sData += saS508.Switches(i).BottomSwitchText + ":"
            Next
        End If

        If sName = "saS509" Then
            For i = 0 To saS509.Count - 1
                sData += saS509.Switches(i).TopSwitchText + ":"
                sData += saS509.Switches(i).BottomSwitchText + ":"
            Next
        End If

        If sName = "saS510" Then
            For i = 0 To saS510.Count - 1
                sData += saS510.Switches(i).TopSwitchText + ":"
                sData += saS510.Switches(i).BottomSwitchText + ":"
            Next
        End If

        If sName = "saS601" Then
            For i = 0 To saS601.Count - 1
                sData += saS601.Switches(i).Text + ":"
            Next
        End If

        If sName = "saS602" Then
            For i = 0 To saS602.Count - 1
                sData += saS602.Switches(i).Text + ":"
            Next
        End If

        If sName = "saS603" Then
            For i = 0 To saS603.Count - 1
                sData += saS603.Switches(i).Text + ":"
            Next
        End If

        If sName = "saS604" Then
            For i = 0 To saS604.Count - 1
                sData += saS604.Switches(i).Text + ":"
            Next
        End If

        If sName = "saS605" Then
            For i = 0 To saS605.Count - 1
                sData += saS605.Switches(i).Text + ":"
            Next
        End If

        If sName = "saS606" Then
            For i = 0 To saS606.Count - 1
                sData += saS606.Switches(i).Text + ":"
            Next
        End If

        If sName = "saS607" Then
            For i = 0 To saS607.Count - 1
                sData += saS607.Switches(i).Text + ":"
            Next
        End If

        If sName = "saS608" Then
            For i = 0 To saS608.Count - 1
                sData += saS608.Switches(i).Text + ":"
            Next
        End If

        If sName = "saS609" Then
            For i = 0 To saS609.Count - 1
                sData += saS609.Switches(i).Text + ":"
            Next
        End If

        If sName = "saS610" Then
            For i = 0 To saS610.Count - 1
                sData += saS610.Switches(i).Text + ":"
            Next
        End If

        If sName = "saS611" Then
            For i = 0 To saS611.Count - 1
                sData += saS611.Switches(i).Text + ":"
            Next
        End If

        If sName = "saS612" Then
            For i = 0 To saS612.Count - 1
                sData += saS612.Switches(i).Text + ":"
            Next
        End If

        If sName = "saS701" Then
            For i = 0 To saS701.Count - 1
                sData += saS701.Switches(i).Text + ":"
            Next
        End If

        If sName = "saS751" Then
            For i = 0 To saS751.Count - 1
                sData += saS751.Switches(i).Text + ":"
            Next
        End If

        If sName = "saS801" Then
            sData += Convert.ToInt16(saS801.SwitchState).ToString()
        End If

        If sName = "saS802" Then
            sData += Convert.ToInt16(saS802.SwitchState).ToString()
        End If

        If sName = "saS803" Then
            sData += Convert.ToInt16(saS803.SwitchState).ToString()
        End If

        If sName = "saS804" Then
            sData += Convert.ToInt16(saS804.SwitchState).ToString()
        End If

        If sName = "saS901" Then
            sData += Convert.ToInt16(saS901.SwitchState).ToString()
        End If

        If sName = "saS902" Then
            sData += Convert.ToInt16(saS902.SwitchState).ToString()
        End If

        If sName = "saS903" Then
            sData += Convert.ToInt16(saS903.SwitchState).ToString()
        End If

        If sName = "saS904" Then
            sData += Convert.ToInt16(saS904.SwitchState).ToString()
        End If

        If sName = "saS905" Then
            sData += Convert.ToInt16(saS905.SwitchState).ToString()
        End If

        If sName = "saS906" Then
            sData += Convert.ToInt16(saS906.SwitchState).ToString()
        End If

        GetData = sData
    End Function

    Public Sub SetData(ByVal sName As String, ByRef sData As String)
        Dim i As Short
        Dim sNextValue As String
        Dim closedSwitchesExist As Boolean = False
        Dim bank1Value As Integer = 0
        Dim bank1List As System.Collections.Generic.List(Of Integer) = New Generic.List(Of Integer)
        Dim bank2Value As Integer = 0
        Dim bank2List As System.Collections.Generic.List(Of Integer) = New Generic.List(Of Integer)
        Dim bank1Str As String = "1."
        Dim bank2Str As String = "2."
        Dim bankStr As String = "1."
        Dim lastClosed As Integer = -1
        Dim inSwitchGroup As Boolean = False
        Dim row As Integer = 2
        Dim switchList As System.Collections.Generic.List(Of Integer) = New Generic.List(Of Integer)()
        Dim switchValue As Integer = 0
        Dim currentSwitchAddedToList As Boolean = False

        Me.Refresh()

        If sName = "saS101" Then
            closedSwitchesExist = False
            bank1Value = 0
            bank1List.Clear()
            bank2Value = 0
            bank2List.Clear()
            bank1Str = "1."
            bank2Str = "2."
            lastClosed = -1
            inSwitchGroup = False

            For i = 0 To saS101.Count - 1
                sNextValue = Strings.Left(sData, InStr(sData, ":") - 1)
                sData = Strings.Right(sData, Len(sData) - InStr(sData, ":"))
                saS101.Switches(i).Text = sNextValue
                If sNextValue.ToUpper() = "CLOSE" Then
                    closedSwitchesExist = True
                    saS101.Switches(i).SwitchState = DPST.SwitchStates.Closed
                    If saS101.Switches(i).Column = 0 Then
                        bank1Value = saS101.Switches(i).Row
                        bank1List.Add(bank1Value)
                    Else
                        bank2Value = saS101.Switches(i).Row
                        bank2List.Add(bank2Value)
                    End If
                Else
                    saS101.Switches(i).SwitchState = DPST.SwitchStates.Open
                End If
            Next

            'Open all of the switches
            WriteInstrument("OPEN 1.0000-0004;2.0000-0004")

            'Close the closed switches
            currentSwitchAddedToList = False
            If closedSwitchesExist Then
                inSwitchGroup = False
                bank1List.Sort()
                For Each switchItem In bank1List
                    If bank1Str = "1." Then
                        bank1Str = bank1Str & switchItem.ToString("0000")
                        lastClosed = switchItem
                        currentSwitchAddedToList = True
                    Else
                        If lastClosed <> switchItem - 1 Then
                            If inSwitchGroup Then
                                bank1Str = bank1Str & "-" & lastClosed.ToString("0000") & "," & switchItem.ToString("0000")
                            Else
                                bank1Str = bank1Str & "," & switchItem.ToString("0000")
                            End If
                            inSwitchGroup = False
                            currentSwitchAddedToList = True
                        Else
                            inSwitchGroup = True
                            currentSwitchAddedToList = False
                        End If
                    End If
                    lastClosed = switchItem
                Next
                If Not currentSwitchAddedToList Then
                    If inSwitchGroup Then
                        bank1Str = bank1Str & "-" & lastClosed.ToString("0000")
                    Else
                        bank1Str = bank1Str & "," & lastClosed.ToString("0000")
                    End If
                End If

                inSwitchGroup = False
                currentSwitchAddedToList = False
                bank2List.Sort()
                For Each switchItem In bank2List
                    If bank2Str = "2." Then
                        bank2Str = bank2Str & switchItem.ToString("0000")
                        lastClosed = switchItem
                        currentSwitchAddedToList = True
                    Else
                        If lastClosed <> switchItem - 1 Then
                            If inSwitchGroup Then
                                bank2Str = bank2Str & "-" & lastClosed.ToString("0000") & "," & switchItem.ToString("0000")
                            Else
                                bank2Str = bank2Str & "," & switchItem.ToString("0000")
                            End If
                            inSwitchGroup = False
                            currentSwitchAddedToList = True
                        Else
                            inSwitchGroup = True
                            currentSwitchAddedToList = False
                        End If
                    End If
                    lastClosed = switchItem
                Next
                If Not currentSwitchAddedToList Then
                    If inSwitchGroup Then
                        bank2Str = bank2Str & "-" & lastClosed.ToString("0000")
                    Else
                        bank2Str = bank2Str & "," & lastClosed.ToString("0000")
                    End If
                End If
                WriteInstrument("CLOSE " & bank1Str & ";" & bank2Str)
            End If
        End If

        If sName = "saS201" Then
            closedSwitchesExist = False
            bankStr = "3."
            lastClosed = -1
            inSwitchGroup = False
            row = 2
            switchList.Clear()
            switchValue = 0

            Me.tabOptions.SelectedIndex = 1
            For i = 0 To saS201.Count - 1
                sNextValue = Strings.Left(sData, InStr(sData, ":") - 1)
                sData = Strings.Right(sData, Len(sData) - InStr(sData, ":"))
                saS201.Switches(i).TopSwitchText = sNextValue
                If sNextValue.ToUpper() = "CLOSE" Then
                    saS201.Switches(i).TopSwitchState = ctl2W2Row.SwitchStates.Closed
                    closedSwitchesExist = True

                    row = 1
                    switchValue = ((((saS201.Switches(i).Index \ 8) * 2) + (row - 1) + 4) * 10) + (saS201.Switches(i).Index Mod 8)
                    switchList.Add(switchValue)
                Else
                    saS201.Switches(i).TopSwitchState = ctl2W2Row.SwitchStates.Open
                End If

                sNextValue = Strings.Left(sData, InStr(sData, ":") - 1)
                sData = Strings.Right(sData, Len(sData) - InStr(sData, ":"))
                saS201.Switches(i).BottomSwitchText = sNextValue
                If sNextValue.ToUpper() = "CLOSE" Then
                    saS201.Switches(i).BottomSwitchState = ctl2W2Row.SwitchStates.Closed
                    closedSwitchesExist = True

                    row = 2
                    switchValue = ((((saS201.Switches(i).Index \ 8) * 2) + (row - 1) + 4) * 10) + (saS201.Switches(i).Index Mod 8)
                    switchList.Add(switchValue)
                Else
                    saS201.Switches(i).BottomSwitchState = ctl2W2Row.SwitchStates.Open
                End If
                Me.tabOptions_Page2.Refresh()
            Next

            'Open all of the switches
            WriteInstrument("OPEN 3.0040-0047,0050-0057,0060-0067,0070-0077,0080-0087,0090-0097")

            'Close the closed switches
            currentSwitchAddedToList = False
            If closedSwitchesExist Then
                inSwitchGroup = False
                switchList.Sort()
                For Each switchItem In switchList
                    If bankStr = "3." Then
                        bankStr = bankStr & switchItem.ToString("0000")
                        currentSwitchAddedToList = True
                    Else
                        If lastClosed <> switchItem - 1 Then
                            If inSwitchGroup Then
                                bankStr = bankStr & "-" & lastClosed.ToString("0000") & "," & switchItem.ToString("0000")
                            Else
                                bankStr = bankStr & "," & switchItem.ToString("0000")
                            End If
                            inSwitchGroup = False
                            currentSwitchAddedToList = True
                        Else
                            inSwitchGroup = True
                            currentSwitchAddedToList = False
                        End If
                    End If
                    lastClosed = switchItem
                Next
                If Not currentSwitchAddedToList Then
                    If inSwitchGroup Then
                        bankStr = bankStr & "-" & lastClosed.ToString("0000")
                    Else
                        bankStr = bankStr & "," & lastClosed.ToString("0000")
                    End If
                End If
                WriteInstrument("CLOSE " & bankStr)
            End If
        End If

        If sName = "saS202" Then
            closedSwitchesExist = False
            bankStr = "3."
            lastClosed = -1
            inSwitchGroup = False
            row = 2
            switchList.Clear()
            switchValue = 0

            Me.tabOptions.SelectedIndex = 1
            For i = 0 To saS202.Count - 1
                sNextValue = Strings.Left(sData, InStr(sData, ":") - 1)
                sData = Strings.Right(sData, Len(sData) - InStr(sData, ":"))
                saS202.Switches(i).TopSwitchText = sNextValue
                If sNextValue.ToUpper() = "CLOSE" Then
                    saS202.Switches(i).TopSwitchState = ctl2W2Row.SwitchStates.Closed
                    closedSwitchesExist = True

                    row = 1
                    switchValue = ((((saS202.Switches(i).Index \ 8) * 2) + (row - 1) + 4 + 6) * 10) + (saS202.Switches(i).Index Mod 8)
                    switchList.Add(switchValue)
                Else
                    saS202.Switches(i).TopSwitchState = ctl2W2Row.SwitchStates.Open
                End If

                sNextValue = Strings.Left(sData, InStr(sData, ":") - 1)
                sData = Strings.Right(sData, Len(sData) - InStr(sData, ":"))
                saS202.Switches(i).BottomSwitchText = sNextValue
                If sNextValue.ToUpper() = "CLOSE" Then
                    saS202.Switches(i).BottomSwitchState = ctl2W2Row.SwitchStates.Closed
                    closedSwitchesExist = True

                    row = 2
                    switchValue = ((((saS202.Switches(i).Index \ 8) * 2) + (row - 1) + 4 + 6) * 10) + (saS202.Switches(i).Index Mod 8)
                    switchList.Add(switchValue)
                Else
                    saS202.Switches(i).BottomSwitchState = ctl2W2Row.SwitchStates.Open
                End If
                Me.tabOptions_Page2.Refresh()
            Next

            'Open all of the switches
            WriteInstrument("OPEN 3.0100-0107,0110-0117,0120-0127,0130-0137,0140-0147,0150-0157")

            'Close the closed switches
            If closedSwitchesExist Then
                inSwitchGroup = False
                switchList.Sort()
                For Each switchItem In switchList
                    If bankStr = "3." Then
                        bankStr = bankStr & switchItem.ToString("0000")
                        currentSwitchAddedToList = True
                    Else
                        If lastClosed <> switchItem - 1 Then
                            If inSwitchGroup Then
                                bankStr = bankStr & "-" & lastClosed.ToString("0000") & "," & switchItem.ToString("0000")
                            Else
                                bankStr = bankStr & "," & switchItem.ToString("0000")
                            End If
                            inSwitchGroup = False
                            currentSwitchAddedToList = True
                        Else
                            inSwitchGroup = True
                            currentSwitchAddedToList = False
                        End If
                    End If
                    lastClosed = switchItem
                Next
                If Not currentSwitchAddedToList Then
                    If inSwitchGroup Then
                        bankStr = bankStr & "-" & lastClosed.ToString("0000")
                    Else
                        bankStr = bankStr & "," & lastClosed.ToString("0000")
                    End If
                End If
                WriteInstrument("CLOSE " & bankStr)
            End If
        End If

        If sName = "saS301" Then
            closedSwitchesExist = False
            bank1Value = 0
            bank1List.Clear()
            bank2Value = 0
            bank2List.Clear()
            bank1Str = "1."
            bank2Str = "2."
            lastClosed = -1
            inSwitchGroup = False
            Dim chan As Integer = 0
            Dim subChan As Integer = 0

            Me.tabOptions.SelectedIndex = 2
            For i = 0 To saS301.Count - 1
                'extract value fron data
                sNextValue = Strings.Left(sData, InStr(sData, ":") - 1)
                sData = Strings.Right(sData, Len(sData) - InStr(sData, ":"))

                saS301.Switches(i).Text = sNextValue
                If sNextValue.ToUpper() = "CLOSE" Then
                    saS301.Switches(i).SwitchState = SPST.SwitchStates.Closed
                    closedSwitchesExist = True

                    Select Case saS301.Switches(i).Column
                        Case 0
                            chan = saS301.Switches(i).Row
                            subChan = 0
                            bank1Value = 1000 + saS301.Switches(i).Row
                            bank1List.Add(bank1Value)
                        Case 1
                            chan = saS301.Switches(i).Row + 16
                            subChan = 0
                            bank1Value = 1000 + saS301.Switches(i).Row + 16
                            bank1List.Add(bank1Value)
                        Case 2
                            chan = saS301.Switches(i).Row + 32
                            subChan = 0
                            bank1Value = 1000 + saS301.Switches(i).Row + 32
                            bank1List.Add(bank1Value)
                        Case 3
                            chan = saS301.Switches(i).Row + 48
                            subChan = 48
                            bank2Value = 1000 + saS301.Switches(i).Row
                            bank2List.Add(bank2Value)
                        Case 4
                            chan = saS301.Switches(i).Row + 64
                            subChan = 48
                            bank2Value = 1000 + saS301.Switches(i).Row + 16
                            bank2List.Add(bank2Value)
                        Case 5
                            chan = saS301.Switches(i).Row + 72
                            subChan = 40
                            bank2Value = 1000 + saS301.Switches(i).Row + 32
                            bank2List.Add(bank2Value)
                        End Select
                Else
                    saS301.Switches(i).SwitchState = SPST.SwitchStates.Open
                End If
                Me.tabOptions_Page3.Refresh()
            Next

            'Open all of the switches
            WriteInstrument("OPEN 1.1000-1047;2.1000-1047")

            'Close the closed switches
            currentSwitchAddedToList = False
            If closedSwitchesExist Then
                inSwitchGroup = False
                bank1List.Sort()
                For Each switchItem In bank1List
                    If bank1Str = "1." Then
                        bank1Str = bank1Str & switchItem.ToString("0000")
                        lastClosed = switchItem
                        currentSwitchAddedToList = True
                    Else
                        If lastClosed <> switchItem - 1 Then
                            If inSwitchGroup Then
                                bank1Str = bank1Str & "-" & lastClosed.ToString("0000") & "," & switchItem.ToString("0000")
                            Else
                                bank1Str = bank1Str & "," & switchItem.ToString("0000")
                            End If
                            inSwitchGroup = False
                            currentSwitchAddedToList = True
                        Else
                            inSwitchGroup = True
                            currentSwitchAddedToList = False
                        End If
                    End If
                    lastClosed = switchItem
                Next
                If Not currentSwitchAddedToList Then
                    If inSwitchGroup Then
                        bank1Str = bank1Str & "-" & lastClosed.ToString("0000")
                    Else
                        bank1Str = bank1Str & "," & lastClosed.ToString("0000")
                    End If
                End If

                inSwitchGroup = False
                currentSwitchAddedToList = False
                bank2List.Sort()
                For Each switchItem In bank2List
                    If bank2Str = "2." Then
                        bank2Str = bank2Str & switchItem.ToString("0000")
                        lastClosed = switchItem
                        currentSwitchAddedToList = True
                    Else
                        If lastClosed <> switchItem - 1 Then
                            If inSwitchGroup Then
                                bank2Str = bank2Str & "-" & lastClosed.ToString("0000") & "," & switchItem.ToString("0000")
                            Else
                                bank2Str = bank2Str & "," & switchItem.ToString("0000")
                            End If
                            inSwitchGroup = False
                            currentSwitchAddedToList = True
                        Else
                            inSwitchGroup = True
                            currentSwitchAddedToList = False
                        End If
                    End If
                    lastClosed = switchItem
                Next
                If Not currentSwitchAddedToList Then
                    If inSwitchGroup Then
                        bank2Str = bank2Str & "-" & lastClosed.ToString("0000")
                    Else
                        bank2Str = bank2Str & "," & lastClosed.ToString("0000")
                    End If
                End If
                WriteInstrument("CLOSE " & bank1Str & ";" & bank2Str)
            End If
        End If

        If sName = "saS401" Then
            closedSwitchesExist = False
            bankStr = "1."
            lastClosed = -1
            inSwitchGroup = False
            row = 2
            switchList.Clear()
            switchValue = 0

            Me.tabOptions.SelectedIndex = 3
            For i = 0 To saS401.Count - 1
                'extract value fron data
                sNextValue = Strings.Left(sData, InStr(sData, ":") - 1)
                sData = Strings.Right(sData, Len(sData) - InStr(sData, ":"))

                saS401.Switches(i).Text = sNextValue
                If sNextValue.ToUpper() = "CLOSE" Then
                    saS401.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Closed
                    closedSwitchesExist = True

                    switchValue = 3000 + (0 Mod 3) * 100 + saS401.Switches(i).Index
                    switchList.Add(switchValue)
                Else
                    saS401.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Open
                End If
                Me.tabOptions_Page4.Refresh()
            Next

            'Open all of the switches
            WriteInstrument("OPEN 1.3000-3003")

            'Close the closed switches
            currentSwitchAddedToList = False
            If closedSwitchesExist Then
                inSwitchGroup = False
                switchList.Sort()
                For Each switchItem In switchList
                    If bankStr = "1." Then
                        bankStr = bankStr & switchItem.ToString("0000")
                        currentSwitchAddedToList = True
                    Else
                        If lastClosed <> switchItem - 1 Then
                            If inSwitchGroup Then
                                bankStr = bankStr & "-" & lastClosed.ToString("0000") & "," & switchItem.ToString("0000")
                            Else
                                bankStr = bankStr & "," & switchItem.ToString("0000")
                            End If
                            inSwitchGroup = False
                            currentSwitchAddedToList = True
                        Else
                            inSwitchGroup = True
                            currentSwitchAddedToList = False
                        End If
                    End If
                    lastClosed = switchItem
                Next
                If Not currentSwitchAddedToList Then
                    If inSwitchGroup Then
                        bankStr = bankStr & "-" & lastClosed.ToString("0000")
                    Else
                        bankStr = bankStr & "," & lastClosed.ToString("0000")
                    End If
                End If
                WriteInstrument("CLOSE " & bankStr)
            End If
        End If

        If sName = "saS402" Then
            closedSwitchesExist = False
            bankStr = "1."
            lastClosed = -1
            inSwitchGroup = False
            row = 2
            switchList.Clear()
            switchValue = 0

            tabOptions.SelectedIndex = 3
            For i = 0 To saS402.Count - 1
                'extract value fron data
                sNextValue = Strings.Left(sData, InStr(sData, ":") - 1)
                sData = Strings.Right(sData, Len(sData) - InStr(sData, ":"))

                saS402.Switches(i).Text = sNextValue
                If sNextValue.ToUpper() = "CLOSE" Then
                    saS402.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Closed
                    closedSwitchesExist = True

                    switchValue = 3000 + (1 Mod 3) * 100 + saS402.Switches(i).Index
                    switchList.Add(switchValue)
                Else
                    saS402.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Open
                End If
                Me.tabOptions_Page4.Refresh()
            Next

            'Open all of the switches
            WriteInstrument("OPEN 1.3100-3103")

            'Close the closed switches
            currentSwitchAddedToList = False
            If closedSwitchesExist Then
                inSwitchGroup = False
                switchList.Sort()
                For Each switchItem In switchList
                    If bankStr = "1." Then
                        bankStr = bankStr & switchItem.ToString("0000")
                        currentSwitchAddedToList = True
                    Else
                        If lastClosed <> switchItem - 1 Then
                            If inSwitchGroup Then
                                bankStr = bankStr & "-" & lastClosed.ToString("0000") & "," & switchItem.ToString("0000")
                            Else
                                bankStr = bankStr & "," & switchItem.ToString("0000")
                            End If
                            inSwitchGroup = False
                            currentSwitchAddedToList = True
                        Else
                            inSwitchGroup = True
                            currentSwitchAddedToList = False
                        End If
                    End If
                    lastClosed = switchItem
                Next
                If Not currentSwitchAddedToList Then
                    If inSwitchGroup Then
                        bankStr = bankStr & "-" & lastClosed.ToString("0000")
                    Else
                        bankStr = bankStr & "," & lastClosed.ToString("0000")
                    End If
                End If
                WriteInstrument("CLOSE " & bankStr)
            End If
        End If

        If sName = "saS403" Then
            closedSwitchesExist = False
            bankStr = "1."
            lastClosed = -1
            inSwitchGroup = False
            row = 2
            switchList.Clear()
            switchValue = 0

            Me.tabOptions.SelectedIndex = 3
            For i = 0 To saS403.Count - 1
                'extract value fron data
                sNextValue = Strings.Left(sData, InStr(sData, ":") - 1)
                sData = Strings.Right(sData, Len(sData) - InStr(sData, ":"))

                saS403.Switches(i).Text = sNextValue
                If sNextValue.ToUpper() = "CLOSE" Then
                    saS403.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Closed
                    closedSwitchesExist = True

                    switchValue = 3000 + (2 Mod 3) * 100 + saS403.Switches(i).Index
                    switchList.Add(switchValue)
                Else
                    saS403.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Open
                End If
                Me.tabOptions_Page4.Refresh()
            Next

            'Open all of the switches
            WriteInstrument("OPEN 1.3200-3203")

            'Close the closed switches
            currentSwitchAddedToList = False
            If closedSwitchesExist Then
                inSwitchGroup = False
                switchList.Sort()
                For Each switchItem In switchList
                    If bankStr = "1." Then
                        bankStr = bankStr & switchItem.ToString("0000")
                        currentSwitchAddedToList = True
                    Else
                        If lastClosed <> switchItem - 1 Then
                            If inSwitchGroup Then
                                bankStr = bankStr & "-" & lastClosed.ToString("0000") & "," & switchItem.ToString("0000")
                            Else
                                bankStr = bankStr & "," & switchItem.ToString("0000")
                            End If
                            inSwitchGroup = False
                            currentSwitchAddedToList = True
                        Else
                            inSwitchGroup = True
                            currentSwitchAddedToList = False
                        End If
                    End If
                    lastClosed = switchItem
                Next
                If Not currentSwitchAddedToList Then
                    If inSwitchGroup Then
                        bankStr = bankStr & "-" & lastClosed.ToString("0000")
                    Else
                        bankStr = bankStr & "," & lastClosed.ToString("0000")
                    End If
                End If
                WriteInstrument("CLOSE " & bankStr)
            End If
        End If

        If sName = "saS404" Then
            closedSwitchesExist = False
            bankStr = "2."
            lastClosed = -1
            inSwitchGroup = False
            row = 2
            switchList.Clear()
            switchValue = 0

            Me.tabOptions.SelectedIndex = 3
            For i = 0 To saS404.Count - 1
                'extract value fron data
                sNextValue = Strings.Left(sData, InStr(sData, ":") - 1)
                sData = Strings.Right(sData, Len(sData) - InStr(sData, ":"))

                saS404.Switches(i).Text = sNextValue
                If sNextValue.ToUpper() = "CLOSE" Then
                    saS404.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Closed
                    closedSwitchesExist = True

                    switchValue = 3000 + (3 Mod 3) * 100 + saS404.Switches(i).Index
                    switchList.Add(switchValue)
                Else
                    saS404.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Open
                End If
                Me.tabOptions_Page4.Refresh()
            Next

            'Open all of the switches
            WriteInstrument("OPEN 2.3000-3003")

            'Close the closed switches
            currentSwitchAddedToList = False
            If closedSwitchesExist Then
                inSwitchGroup = False
                switchList.Sort()
                For Each switchItem In switchList
                    If bankStr = "2." Then
                        bankStr = bankStr & switchItem.ToString("0000")
                        currentSwitchAddedToList = True
                    Else
                        If lastClosed <> switchItem - 1 Then
                            If inSwitchGroup Then
                                bankStr = bankStr & "-" & lastClosed.ToString("0000") & "," & switchItem.ToString("0000")
                            Else
                                bankStr = bankStr & "," & switchItem.ToString("0000")
                            End If
                            inSwitchGroup = False
                            currentSwitchAddedToList = True
                        Else
                            inSwitchGroup = True
                            currentSwitchAddedToList = False
                        End If
                    End If
                    lastClosed = switchItem
                Next
                If Not currentSwitchAddedToList Then
                    If inSwitchGroup Then
                        bankStr = bankStr & "-" & lastClosed.ToString("0000")
                    Else
                        bankStr = bankStr & "," & lastClosed.ToString("0000")
                    End If
                End If
                WriteInstrument("CLOSE " & bankStr)
            End If
        End If

        If sName = "saS405" Then
            closedSwitchesExist = False
            bankStr = "2."
            lastClosed = -1
            inSwitchGroup = False
            row = 2
            switchList.Clear()
            switchValue = 0

            Me.tabOptions.SelectedIndex = 3
            For i = 0 To saS405.Count - 1
                'extract value fron data
                sNextValue = Strings.Left(sData, InStr(sData, ":") - 1)
                sData = Strings.Right(sData, Len(sData) - InStr(sData, ":"))

                saS405.Switches(i).Text = sNextValue
                If sNextValue.ToUpper() = "CLOSE" Then
                    saS405.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Closed
                    closedSwitchesExist = True

                    switchValue = 3000 + (4 Mod 3) * 100 + saS405.Switches(i).Index
                    switchList.Add(switchValue)
                Else
                    saS405.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Open
                End If
                Me.tabOptions_Page4.Refresh()
            Next

            'Open all of the switches
            WriteInstrument("OPEN 2.3100-3103")

            'Close the closed switches
            currentSwitchAddedToList = False
            If closedSwitchesExist Then
                inSwitchGroup = False
                switchList.Sort()
                For Each switchItem In switchList
                    If bankStr = "2." Then
                        bankStr = bankStr & switchItem.ToString("0000")
                        currentSwitchAddedToList = True
                    Else
                        If lastClosed <> switchItem - 1 Then
                            If inSwitchGroup Then
                                bankStr = bankStr & "-" & lastClosed.ToString("0000") & "," & switchItem.ToString("0000")
                            Else
                                bankStr = bankStr & "," & switchItem.ToString("0000")
                            End If
                            inSwitchGroup = False
                            currentSwitchAddedToList = True
                        Else
                            inSwitchGroup = True
                            currentSwitchAddedToList = False
                        End If
                    End If
                    lastClosed = switchItem
                Next
                If Not currentSwitchAddedToList Then
                    If inSwitchGroup Then
                        bankStr = bankStr & "-" & lastClosed.ToString("0000")
                    Else
                        bankStr = bankStr & "," & lastClosed.ToString("0000")
                    End If
                End If
                WriteInstrument("CLOSE " & bankStr)
            End If
        End If

        If sName = "saS406" Then
            closedSwitchesExist = False
            bankStr = "2."
            lastClosed = -1
            inSwitchGroup = False
            row = 2
            switchList.Clear()
            switchValue = 0

            Me.tabOptions.SelectedIndex = 3
            For i = 0 To saS406.Count - 1
                'extract value fron data
                sNextValue = Strings.Left(sData, InStr(sData, ":") - 1)
                sData = Strings.Right(sData, Len(sData) - InStr(sData, ":"))

                saS406.Switches(i).Text = sNextValue
                If sNextValue.ToUpper() = "CLOSE" Then
                    saS406.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Closed
                    closedSwitchesExist = True

                    switchValue = 3000 + (5 Mod 3) * 100 + saS406.Switches(i).Index
                    switchList.Add(switchValue)
                Else
                    saS406.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Open
                End If
                Me.tabOptions_Page4.Refresh()
            Next

            'Open all of the switches
            WriteInstrument("OPEN 2.3200-3203")

            'Close the closed switches
            currentSwitchAddedToList = False
            If closedSwitchesExist Then
                inSwitchGroup = False
                switchList.Sort()
                For Each switchItem In switchList
                    If bankStr = "2." Then
                        bankStr = bankStr & switchItem.ToString("0000")
                        currentSwitchAddedToList = True
                    Else
                        If lastClosed <> switchItem - 1 Then
                            If inSwitchGroup Then
                                bankStr = bankStr & "-" & lastClosed.ToString("0000") & "," & switchItem.ToString("0000")
                            Else
                                bankStr = bankStr & "," & switchItem.ToString("0000")
                            End If
                            inSwitchGroup = False
                            currentSwitchAddedToList = True
                        Else
                            inSwitchGroup = True
                            currentSwitchAddedToList = False
                        End If
                    End If
                    lastClosed = switchItem
                Next
                If Not currentSwitchAddedToList Then
                    If inSwitchGroup Then
                        bankStr = bankStr & "-" & lastClosed.ToString("0000")
                    Else
                        bankStr = bankStr & "," & lastClosed.ToString("0000")
                    End If
                End If
                WriteInstrument("CLOSE " & bankStr)
            End If
        End If

        If sName = "saS501" Then
            closedSwitchesExist = False
            bankStr = "1."
            lastClosed = -1
            inSwitchGroup = False
            row = 2
            switchList.Clear()
            switchValue = 0

            Me.tabOptions.SelectedIndex = 4
            For i = 0 To saS501.Count - 1
                sNextValue = Strings.Left(sData, InStr(sData, ":") - 1)
                sData = Strings.Right(sData, Len(sData) - InStr(sData, ":"))
                saS501.Switches(i).TopSwitchText = sNextValue
                If sNextValue.ToUpper() = "CLOSE" Then
                    saS501.Switches(i).TopSwitchState = ctl1W2Row.SwitchStates.Closed
                    closedSwitchesExist = True

                    row = 1
                    switchValue = 4000 + ((0 Mod 5) * 100) + (saS501.Switches(i).Index + (10 * (row - 1)))
                    switchList.Add(switchValue)
                Else
                    saS501.Switches(i).TopSwitchState = ctl1W2Row.SwitchStates.Open
                End If

                sNextValue = Strings.Left(sData, InStr(sData, ":") - 1)
                sData = Strings.Right(sData, Len(sData) - InStr(sData, ":"))
                saS501.Switches(i).BottomSwitchText = sNextValue
                If sNextValue.ToUpper() = "CLOSE" Then
                    saS501.Switches(i).BottomSwitchState = ctl1W2Row.SwitchStates.Closed
                    closedSwitchesExist = True

                    row = 2
                    switchValue = 4000 + ((0 Mod 5) * 100) + (saS501.Switches(i).Index + (10 * (row - 1)))
                    switchList.Add(switchValue)
                Else
                    saS501.Switches(i).BottomSwitchState = ctl1W2Row.SwitchStates.Open
                End If
                Me.tabOptions_Page5.Refresh()
            Next

            'Open all of the switches
            WriteInstrument("OPEN 1.4000-4007,4010-4017")

            'Close the closed switches
            currentSwitchAddedToList = False
            If closedSwitchesExist Then
                inSwitchGroup = False
                switchList.Sort()
                For Each switchItem In switchList
                    If bankStr = "1." Then
                        bankStr = bankStr & switchItem.ToString("0000")
                        currentSwitchAddedToList = True
                    Else
                        If lastClosed <> switchItem - 1 Then
                            If inSwitchGroup Then
                                bankStr = bankStr & "-" & lastClosed.ToString("0000") & "," & switchItem.ToString("0000")
                            Else
                                bankStr = bankStr & "," & switchItem.ToString("0000")
                            End If
                            inSwitchGroup = False
                            currentSwitchAddedToList = True
                        Else
                            inSwitchGroup = True
                            currentSwitchAddedToList = False
                        End If
                    End If
                    lastClosed = switchItem
                Next
                If Not currentSwitchAddedToList Then
                    If inSwitchGroup Then
                        bankStr = bankStr & "-" & lastClosed.ToString("0000")
                    Else
                        bankStr = bankStr & "," & lastClosed.ToString("0000")
                    End If
                End If
                WriteInstrument("CLOSE " & bankStr)
            End If
        End If

        If sName = "saS502" Then
            closedSwitchesExist = False
            bankStr = "1."
            lastClosed = -1
            inSwitchGroup = False
            row = 2
            switchList.Clear()
            switchValue = 0

            Me.tabOptions.SelectedIndex = 4
            For i = 0 To saS502.Count - 1
                sNextValue = Strings.Left(sData, InStr(sData, ":") - 1)
                sData = Strings.Right(sData, Len(sData) - InStr(sData, ":"))
                saS502.Switches(i).TopSwitchText = sNextValue
                If sNextValue.ToUpper() = "CLOSE" Then
                    saS502.Switches(i).TopSwitchState = ctl1W2Row.SwitchStates.Closed
                    closedSwitchesExist = True

                    row = 1
                    switchValue = 4000 + ((1 Mod 5) * 100) + (saS502.Switches(i).Index + (10 * (row - 1)))
                    switchList.Add(switchValue)
                Else
                    saS502.Switches(i).TopSwitchState = ctl1W2Row.SwitchStates.Open
                End If

                sNextValue = Strings.Left(sData, InStr(sData, ":") - 1)
                sData = Strings.Right(sData, Len(sData) - InStr(sData, ":"))
                saS502.Switches(i).BottomSwitchText = sNextValue
                If sNextValue.ToUpper() = "CLOSE" Then
                    saS502.Switches(i).BottomSwitchState = ctl1W2Row.SwitchStates.Closed
                    closedSwitchesExist = True

                    row = 2
                    switchValue = 4000 + ((1 Mod 5) * 100) + (saS502.Switches(i).Index + (10 * (row - 1)))
                    switchList.Add(switchValue)
                Else
                    saS502.Switches(i).BottomSwitchState = ctl1W2Row.SwitchStates.Open
                End If
                Me.tabOptions_Page5.Refresh()
            Next

            'Open all of the switches
            WriteInstrument("OPEN 1.4100-4107,4110-4117")

            'Close the closed switches
            currentSwitchAddedToList = False
            If closedSwitchesExist Then
                inSwitchGroup = False
                switchList.Sort()
                For Each switchItem In switchList
                    If bankStr = "1." Then
                        bankStr = bankStr & switchItem.ToString("0000")
                        currentSwitchAddedToList = True
                    Else
                        If lastClosed <> switchItem - 1 Then
                            If inSwitchGroup Then
                                bankStr = bankStr & "-" & lastClosed.ToString("0000") & "," & switchItem.ToString("0000")
                            Else
                                bankStr = bankStr & "," & switchItem.ToString("0000")
                            End If
                            inSwitchGroup = False
                            currentSwitchAddedToList = True
                        Else
                            inSwitchGroup = True
                            currentSwitchAddedToList = False
                        End If
                    End If
                    lastClosed = switchItem
                Next
                If Not currentSwitchAddedToList Then
                    If inSwitchGroup Then
                        bankStr = bankStr & "-" & lastClosed.ToString("0000")
                    Else
                        bankStr = bankStr & "," & lastClosed.ToString("0000")
                    End If
                End If
                WriteInstrument("CLOSE " & bankStr)
            End If
        End If

        If sName = "saS503" Then
            closedSwitchesExist = False
            bankStr = "1."
            lastClosed = -1
            inSwitchGroup = False
            row = 2
            switchList.Clear()
            switchValue = 0

            Me.tabOptions.SelectedIndex = 4
            For i = 0 To saS503.Count - 1
                sNextValue = Strings.Left(sData, InStr(sData, ":") - 1)
                sData = Strings.Right(sData, Len(sData) - InStr(sData, ":"))
                saS503.Switches(i).TopSwitchText = sNextValue
                If sNextValue.ToUpper() = "CLOSE" Then
                    saS503.Switches(i).TopSwitchState = ctl1W2Row.SwitchStates.Closed
                    closedSwitchesExist = True

                    row = 1
                    switchValue = 4000 + ((2 Mod 5) * 100) + (saS503.Switches(i).Index + (10 * (row - 1)))
                    switchList.Add(switchValue)
                Else
                    saS503.Switches(i).TopSwitchState = ctl1W2Row.SwitchStates.Open
                End If

                sNextValue = Strings.Left(sData, InStr(sData, ":") - 1)
                sData = Strings.Right(sData, Len(sData) - InStr(sData, ":"))
                saS503.Switches(i).BottomSwitchText = sNextValue
                If sNextValue.ToUpper() = "CLOSE" Then
                    saS503.Switches(i).BottomSwitchState = ctl1W2Row.SwitchStates.Closed
                    closedSwitchesExist = True

                    row = 2
                    switchValue = 4000 + ((2 Mod 5) * 100) + (saS503.Switches(i).Index + (10 * (row - 1)))
                    switchList.Add(switchValue)
                Else
                     saS503.Switches(i).BottomSwitchState = ctl1W2Row.SwitchStates.Open
               End If
                Me.tabOptions_Page5.Refresh()
            Next

            'Open all of the switches
            WriteInstrument("OPEN 1.4200-4207,4210-4217")

            'Close the closed switches
            currentSwitchAddedToList = False
            If closedSwitchesExist Then
                inSwitchGroup = False
                switchList.Sort()
                For Each switchItem In switchList
                    If bankStr = "1." Then
                        bankStr = bankStr & switchItem.ToString("0000")
                        currentSwitchAddedToList = True
                    Else
                        If lastClosed <> switchItem - 1 Then
                            If inSwitchGroup Then
                                bankStr = bankStr & "-" & lastClosed.ToString("0000") & "," & switchItem.ToString("0000")
                            Else
                                bankStr = bankStr & "," & switchItem.ToString("0000")
                            End If
                            inSwitchGroup = False
                            currentSwitchAddedToList = True
                        Else
                            inSwitchGroup = True
                            currentSwitchAddedToList = False
                        End If
                    End If
                    lastClosed = switchItem
                Next
                If Not currentSwitchAddedToList Then
                    If inSwitchGroup Then
                        bankStr = bankStr & "-" & lastClosed.ToString("0000")
                    Else
                        bankStr = bankStr & "," & lastClosed.ToString("0000")
                    End If
                End If
                WriteInstrument("CLOSE " & bankStr)
            End If
        End If

        If sName = "saS504" Then
            closedSwitchesExist = False
            bankStr = "1."
            lastClosed = -1
            inSwitchGroup = False
            row = 2
            switchList.Clear()
            switchValue = 0

            Me.tabOptions.SelectedIndex = 4
            For i = 0 To saS504.Count - 1
                sNextValue = Strings.Left(sData, InStr(sData, ":") - 1)
                sData = Strings.Right(sData, Len(sData) - InStr(sData, ":"))
                saS504.Switches(i).TopSwitchText = sNextValue
                If sNextValue.ToUpper() = "CLOSE" Then
                    saS504.Switches(i).TopSwitchState = ctl1W2Row.SwitchStates.Closed
                    closedSwitchesExist = True

                    row = 1
                    switchValue = 4000 + ((3 Mod 5) * 100) + (saS504.Switches(i).Index + (10 * (row - 1)))
                    switchList.Add(switchValue)
                Else
                    saS504.Switches(i).TopSwitchState = ctl1W2Row.SwitchStates.Open
                End If

                sNextValue = Strings.Left(sData, InStr(sData, ":") - 1)
                sData = Strings.Right(sData, Len(sData) - InStr(sData, ":"))
                saS504.Switches(i).BottomSwitchText = sNextValue
                If sNextValue.ToUpper() = "CLOSE" Then
                    saS504.Switches(i).BottomSwitchState = ctl1W2Row.SwitchStates.Closed
                    closedSwitchesExist = True

                    row = 2
                    switchValue = 4000 + ((3 Mod 5) * 100) + (saS504.Switches(i).Index + (10 * (row - 1)))
                    switchList.Add(switchValue)
                Else
                    saS504.Switches(i).BottomSwitchState = ctl1W2Row.SwitchStates.Open
                End If
                Me.tabOptions_Page5.Refresh()
            Next

            'Open all of the switches
            WriteInstrument("OPEN 1.4300-4307,4310-4317")

            'Close the closed switches
            currentSwitchAddedToList = False
            If closedSwitchesExist Then
                inSwitchGroup = False
                switchList.Sort()
                For Each switchItem In switchList
                    If bankStr = "1." Then
                        bankStr = bankStr & switchItem.ToString("0000")
                        currentSwitchAddedToList = True
                    Else
                        If lastClosed <> switchItem - 1 Then
                            If inSwitchGroup Then
                                bankStr = bankStr & "-" & lastClosed.ToString("0000") & "," & switchItem.ToString("0000")
                            Else
                                bankStr = bankStr & "," & switchItem.ToString("0000")
                            End If
                            inSwitchGroup = False
                            currentSwitchAddedToList = True
                        Else
                            inSwitchGroup = True
                            currentSwitchAddedToList = False
                        End If
                    End If
                    lastClosed = switchItem
                Next
                If Not currentSwitchAddedToList Then
                    If inSwitchGroup Then
                        bankStr = bankStr & "-" & lastClosed.ToString("0000")
                    Else
                        bankStr = bankStr & "," & lastClosed.ToString("0000")
                    End If
                End If
                WriteInstrument("CLOSE " & bankStr)
            End If
        End If

        If sName = "saS505" Then
            closedSwitchesExist = False
            bankStr = "1."
            lastClosed = -1
            inSwitchGroup = False
            row = 2
            switchList.Clear()
            switchValue = 0

            Me.tabOptions.SelectedIndex = 4
            For i = 0 To saS505.Count - 1
                sNextValue = Strings.Left(sData, InStr(sData, ":") - 1)
                sData = Strings.Right(sData, Len(sData) - InStr(sData, ":"))
                saS505.Switches(i).TopSwitchText = sNextValue
                If sNextValue.ToUpper() = "CLOSE" Then
                    saS505.Switches(i).TopSwitchState = ctl1W2Row.SwitchStates.Closed
                    closedSwitchesExist = True

                    row = 1
                    switchValue = 4000 + ((4 Mod 5) * 100) + (saS505.Switches(i).Index + (10 * (row - 1)))
                    switchList.Add(switchValue)
                Else
                    saS505.Switches(i).TopSwitchState = ctl1W2Row.SwitchStates.Open
                End If

                sNextValue = Strings.Left(sData, InStr(sData, ":") - 1)
                sData = Strings.Right(sData, Len(sData) - InStr(sData, ":"))
                saS505.Switches(i).BottomSwitchText = sNextValue
                If sNextValue.ToUpper() = "CLOSE" Then
                    saS505.Switches(i).BottomSwitchState = ctl1W2Row.SwitchStates.Closed
                    closedSwitchesExist = True

                    row = 2
                    switchValue = 4000 + ((4 Mod 5) * 100) + (saS505.Switches(i).Index + (10 * (row - 1)))
                    switchList.Add(switchValue)
                Else
                    saS505.Switches(i).BottomSwitchState = ctl1W2Row.SwitchStates.Open
                End If
                Me.tabOptions_Page5.Refresh()
            Next

            'Open all of the switches
            WriteInstrument("OPEN 1.4400-4407,4410-4417")

            'Close the closed switches
            currentSwitchAddedToList = False
            If closedSwitchesExist Then
                inSwitchGroup = False
                switchList.Sort()
                For Each switchItem In switchList
                    If bankStr = "1." Then
                        bankStr = bankStr & switchItem.ToString("0000")
                        currentSwitchAddedToList = True
                    Else
                        If lastClosed <> switchItem - 1 Then
                            If inSwitchGroup Then
                                bankStr = bankStr & "-" & lastClosed.ToString("0000") & "," & switchItem.ToString("0000")
                            Else
                                bankStr = bankStr & "," & switchItem.ToString("0000")
                            End If
                            inSwitchGroup = False
                            currentSwitchAddedToList = True
                        Else
                            inSwitchGroup = True
                            currentSwitchAddedToList = False
                        End If
                    End If
                    lastClosed = switchItem
                Next
                If Not currentSwitchAddedToList Then
                    If inSwitchGroup Then
                        bankStr = bankStr & "-" & lastClosed.ToString("0000")
                    Else
                        bankStr = bankStr & "," & lastClosed.ToString("0000")
                    End If
                End If
                WriteInstrument("CLOSE " & bankStr)
            End If
        End If

        If sName = "saS506" Then
            closedSwitchesExist = False
            bankStr = "2."
            lastClosed = -1
            inSwitchGroup = False
            row = 2
            switchList.Clear()
            switchValue = 0

            Me.tabOptions.SelectedIndex = 4
            For i = 0 To saS506.Count - 1
                sNextValue = Strings.Left(sData, InStr(sData, ":") - 1)
                sData = Strings.Right(sData, Len(sData) - InStr(sData, ":"))
                saS506.Switches(i).TopSwitchText = sNextValue
                If sNextValue.ToUpper() = "CLOSE" Then
                    saS506.Switches(i).TopSwitchState = ctl1W2Row.SwitchStates.Closed
                    closedSwitchesExist = True

                    row = 1
                    switchValue = 4000 + ((5 Mod 5) * 100) + (saS506.Switches(i).Index + (10 * (row - 1)))
                    switchList.Add(switchValue)
                Else
                    saS506.Switches(i).TopSwitchState = ctl1W2Row.SwitchStates.Open
                End If

                sNextValue = Strings.Left(sData, InStr(sData, ":") - 1)
                sData = Strings.Right(sData, Len(sData) - InStr(sData, ":"))
                saS506.Switches(i).BottomSwitchText = sNextValue
                If sNextValue.ToUpper() = "CLOSE" Then
                    saS506.Switches(i).BottomSwitchState = ctl1W2Row.SwitchStates.Closed
                    closedSwitchesExist = True

                    row = 2
                    switchValue = 4000 + ((5 Mod 5) * 100) + (saS506.Switches(i).Index + (10 * (row - 1)))
                    switchList.Add(switchValue)
                Else
                    saS506.Switches(i).BottomSwitchState = ctl1W2Row.SwitchStates.Open
                End If
                Me.tabOptions_Page5.Refresh()
            Next

            'Open all of the switches
            WriteInstrument("OPEN 2.4000-4007,4010-4017")

            'Close the closed switches
            currentSwitchAddedToList = False
            If closedSwitchesExist Then
                inSwitchGroup = False
                switchList.Sort()
                For Each switchItem In switchList
                    If bankStr = "2." Then
                        bankStr = bankStr & switchItem.ToString("0000")
                        currentSwitchAddedToList = True
                    Else
                        If lastClosed <> switchItem - 1 Then
                            If inSwitchGroup Then
                                bankStr = bankStr & "-" & lastClosed.ToString("0000") & "," & switchItem.ToString("0000")
                            Else
                                bankStr = bankStr & "," & switchItem.ToString("0000")
                            End If
                            inSwitchGroup = False
                            currentSwitchAddedToList = True
                        Else
                            inSwitchGroup = True
                            currentSwitchAddedToList = False
                        End If
                    End If
                    lastClosed = switchItem
                Next
                If Not currentSwitchAddedToList Then
                    If inSwitchGroup Then
                        bankStr = bankStr & "-" & lastClosed.ToString("0000")
                    Else
                        bankStr = bankStr & "," & lastClosed.ToString("0000")
                    End If
                End If
                WriteInstrument("CLOSE " & bankStr)
            End If
        End If

        If sName = "saS507" Then
            closedSwitchesExist = False
            bankStr = "2."
            lastClosed = -1
            inSwitchGroup = False
            row = 2
            switchList.Clear()
            switchValue = 0

            Me.tabOptions.SelectedIndex = 4
            For i = 0 To saS507.Count - 1
                sNextValue = Strings.Left(sData, InStr(sData, ":") - 1)
                sData = Strings.Right(sData, Len(sData) - InStr(sData, ":"))
                saS507.Switches(i).TopSwitchText = sNextValue
                If sNextValue.ToUpper() = "CLOSE" Then
                    saS507.Switches(i).TopSwitchState = ctl1W2Row.SwitchStates.Closed
                    closedSwitchesExist = True

                    row = 1
                    switchValue = 4000 + ((6 Mod 5) * 100) + (saS507.Switches(i).Index + (10 * (row - 1)))
                    switchList.Add(switchValue)
                Else
                    saS507.Switches(i).TopSwitchState = ctl1W2Row.SwitchStates.Open
                End If

                sNextValue = Strings.Left(sData, InStr(sData, ":") - 1)
                sData = Strings.Right(sData, Len(sData) - InStr(sData, ":"))
                saS507.Switches(i).BottomSwitchText = sNextValue
                If sNextValue.ToUpper() = "CLOSE" Then
                    saS507.Switches(i).BottomSwitchState = ctl1W2Row.SwitchStates.Closed
                    closedSwitchesExist = True

                    row = 2
                    switchValue = 4000 + ((6 Mod 5) * 100) + (saS507.Switches(i).Index + (10 * (row - 1)))
                    switchList.Add(switchValue)
                Else
                    saS507.Switches(i).BottomSwitchState = ctl1W2Row.SwitchStates.Open
                End If
                Me.tabOptions_Page5.Refresh()
            Next

            'Open all of the switches
            WriteInstrument("OPEN 2.4100-4107,4110-4117")

            'Close the closed switches
            currentSwitchAddedToList = False
            If closedSwitchesExist Then
                inSwitchGroup = False
                switchList.Sort()
                For Each switchItem In switchList
                    If bankStr = "2." Then
                        bankStr = bankStr & switchItem.ToString("0000")
                    Else
                        currentSwitchAddedToList = True
                        If lastClosed <> switchItem - 1 Then
                            If inSwitchGroup Then
                                bankStr = bankStr & "-" & lastClosed.ToString("0000") & "," & switchItem.ToString("0000")
                            Else
                                bankStr = bankStr & "," & switchItem.ToString("0000")
                            End If
                            inSwitchGroup = False
                            currentSwitchAddedToList = True
                        Else
                            inSwitchGroup = True
                            currentSwitchAddedToList = False
                        End If
                    End If
                    lastClosed = switchItem
                Next
                If Not currentSwitchAddedToList Then
                    If inSwitchGroup Then
                        bankStr = bankStr & "-" & lastClosed.ToString("0000")
                    Else
                        bankStr = bankStr & "," & lastClosed.ToString("0000")
                    End If
                End If
                WriteInstrument("CLOSE " & bankStr)
            End If
        End If

        If sName = "saS508" Then
            closedSwitchesExist = False
            bankStr = "2."
            lastClosed = -1
            inSwitchGroup = False
            row = 2
            switchList.Clear()
            switchValue = 0

            Me.tabOptions.SelectedIndex = 4
            For i = 0 To saS508.Count - 1
                sNextValue = Strings.Left(sData, InStr(sData, ":") - 1)
                sData = Strings.Right(sData, Len(sData) - InStr(sData, ":"))
                saS508.Switches(i).TopSwitchText = sNextValue
                If sNextValue.ToUpper() = "CLOSE" Then
                    saS508.Switches(i).TopSwitchState = ctl1W2Row.SwitchStates.Closed
                    closedSwitchesExist = True

                    row = 1
                    switchValue = 4000 + ((7 Mod 5) * 100) + (saS508.Switches(i).Index + (10 * (row - 1)))
                    switchList.Add(switchValue)
                Else
                    saS508.Switches(i).TopSwitchState = ctl1W2Row.SwitchStates.Open
                End If

                sNextValue = Strings.Left(sData, InStr(sData, ":") - 1)
                sData = Strings.Right(sData, Len(sData) - InStr(sData, ":"))
                saS508.Switches(i).BottomSwitchText = sNextValue
                If sNextValue.ToUpper() = "CLOSE" Then
                    saS508.Switches(i).BottomSwitchState = ctl1W2Row.SwitchStates.Closed
                    closedSwitchesExist = True

                    row = 2
                    switchValue = 4000 + ((7 Mod 5) * 100) + (saS508.Switches(i).Index + (10 * (row - 1)))
                    switchList.Add(switchValue)
                Else
                    saS508.Switches(i).BottomSwitchState = ctl1W2Row.SwitchStates.Open
                End If
                Me.tabOptions_Page5.Refresh()
            Next

            'Open all of the switches
            WriteInstrument("OPEN 2.4200-4207,4210-4217")

            'Close the closed switches
            currentSwitchAddedToList = False
            If closedSwitchesExist Then
                inSwitchGroup = False
                switchList.Sort()
                For Each switchItem In switchList
                    If bankStr = "2." Then
                        bankStr = bankStr & switchItem.ToString("0000")
                        currentSwitchAddedToList = True
                    Else
                        If lastClosed <> switchItem - 1 Then
                            If inSwitchGroup Then
                                bankStr = bankStr & "-" & lastClosed.ToString("0000") & "," & switchItem.ToString("0000")
                            Else
                                bankStr = bankStr & "," & switchItem.ToString("0000")
                            End If
                            inSwitchGroup = False
                            currentSwitchAddedToList = True
                        Else
                            inSwitchGroup = True
                            currentSwitchAddedToList = False
                        End If
                    End If
                    lastClosed = switchItem
                Next
                If Not currentSwitchAddedToList Then
                    If inSwitchGroup Then
                        bankStr = bankStr & "-" & lastClosed.ToString("0000")
                    Else
                        bankStr = bankStr & "," & lastClosed.ToString("0000")
                    End If
                End If
                WriteInstrument("CLOSE " & bankStr)
            End If
        End If

        If sName = "saS509" Then
            closedSwitchesExist = False
            bankStr = "2."
            lastClosed = -1
            inSwitchGroup = False
            row = 2
            switchList.Clear()
            switchValue = 0

            Me.tabOptions.SelectedIndex = 4
            For i = 0 To saS509.Count - 1
                sNextValue = Strings.Left(sData, InStr(sData, ":") - 1)
                sData = Strings.Right(sData, Len(sData) - InStr(sData, ":"))
                saS509.Switches(i).TopSwitchText = sNextValue
                If sNextValue.ToUpper() = "CLOSE" Then
                    saS509.Switches(i).TopSwitchState = ctl1W2Row.SwitchStates.Closed
                    closedSwitchesExist = True

                    row = 1
                    switchValue = 4000 + ((8 Mod 5) * 100) + (saS509.Switches(i).Index + (10 * (row - 1)))
                    switchList.Add(switchValue)
                Else
                    saS509.Switches(i).TopSwitchState = ctl1W2Row.SwitchStates.Open
                End If

                sNextValue = Strings.Left(sData, InStr(sData, ":") - 1)
                sData = Strings.Right(sData, Len(sData) - InStr(sData, ":"))
                saS509.Switches(i).BottomSwitchText = sNextValue
                If sNextValue.ToUpper() = "CLOSE" Then
                    saS509.Switches(i).BottomSwitchState = ctl1W2Row.SwitchStates.Closed
                    closedSwitchesExist = True

                    row = 2
                    switchValue = 4000 + ((8 Mod 5) * 100) + (saS509.Switches(i).Index + (10 * (row - 1)))
                    switchList.Add(switchValue)
                Else
                    saS509.Switches(i).BottomSwitchState = ctl1W2Row.SwitchStates.Open
                End If
                Me.tabOptions_Page5.Refresh()
            Next

            'Open all of the switches
            WriteInstrument("OPEN 2.4300-4307,4310-4317")

            'Close the closed switches
            currentSwitchAddedToList = False
            If closedSwitchesExist Then
                inSwitchGroup = False
                switchList.Sort()
                For Each switchItem In switchList
                    If bankStr = "2." Then
                        bankStr = bankStr & switchItem.ToString("0000")
                        currentSwitchAddedToList = True
                    Else
                        If lastClosed <> switchItem - 1 Then
                            If inSwitchGroup Then
                                bankStr = bankStr & "-" & lastClosed.ToString("0000") & "," & switchItem.ToString("0000")
                            Else
                                bankStr = bankStr & "," & switchItem.ToString("0000")
                            End If
                            inSwitchGroup = False
                            currentSwitchAddedToList = True
                        Else
                            inSwitchGroup = True
                            currentSwitchAddedToList = False
                        End If
                    End If
                    lastClosed = switchItem
                Next
                If Not currentSwitchAddedToList Then
                    If inSwitchGroup Then
                        bankStr = bankStr & "-" & lastClosed.ToString("0000")
                    Else
                        bankStr = bankStr & "," & lastClosed.ToString("0000")
                    End If
                End If
                WriteInstrument("CLOSE " & bankStr)
            End If
        End If

        If sName = "saS510" Then
            closedSwitchesExist = False
            bankStr = "2."
            lastClosed = -1
            inSwitchGroup = False
            row = 2
            switchList.Clear()
            switchValue = 0

            Me.tabOptions.SelectedIndex = 4
            For i = 0 To saS510.Count - 1
                sNextValue = Strings.Left(sData, InStr(sData, ":") - 1)
                sData = Strings.Right(sData, Len(sData) - InStr(sData, ":"))
                saS510.Switches(i).TopSwitchText = sNextValue
                If sNextValue.ToUpper() = "CLOSE" Then
                    saS510.Switches(i).TopSwitchState = ctl1W2Row.SwitchStates.Closed
                    closedSwitchesExist = True

                    row = 1
                    switchValue = 4000 + ((9 Mod 5) * 100) + (saS510.Switches(i).Index + (10 * (row - 1)))
                    switchList.Add(switchValue)
                Else
                    saS510.Switches(i).TopSwitchState = ctl1W2Row.SwitchStates.Open
                End If

                sNextValue = Strings.Left(sData, InStr(sData, ":") - 1)
                sData = Strings.Right(sData, Len(sData) - InStr(sData, ":"))
                saS510.Switches(i).BottomSwitchText = sNextValue
                If sNextValue.ToUpper() = "CLOSE" Then
                    saS510.Switches(i).BottomSwitchState = ctl1W2Row.SwitchStates.Closed
                    closedSwitchesExist = True

                    row = 2
                    switchValue = 4000 + ((9 Mod 5) * 100) + (saS510.Switches(i).Index + (10 * (row - 1)))
                    switchList.Add(switchValue)
                Else
                    saS510.Switches(i).BottomSwitchState = ctl1W2Row.SwitchStates.Open
                End If
                Me.tabOptions_Page5.Refresh()
            Next

            'Open all of the switches
            WriteInstrument("OPEN 2.4400-4407,4410-4417")

            'Close the closed switches
            currentSwitchAddedToList = False
            If closedSwitchesExist Then
                inSwitchGroup = False
                switchList.Sort()
                For Each switchItem In switchList
                    If bankStr = "2." Then
                        bankStr = bankStr & switchItem.ToString("0000")
                        currentSwitchAddedToList = True
                    Else
                        If lastClosed <> switchItem - 1 Then
                            If inSwitchGroup Then
                                bankStr = bankStr & "-" & lastClosed.ToString("0000") & "," & switchItem.ToString("0000")
                            Else
                                bankStr = bankStr & "," & switchItem.ToString("0000")
                            End If
                            inSwitchGroup = False
                            currentSwitchAddedToList = True
                        Else
                            inSwitchGroup = True
                            currentSwitchAddedToList = False
                        End If
                    End If
                    lastClosed = switchItem
                Next
                If Not currentSwitchAddedToList Then
                    If inSwitchGroup Then
                        bankStr = bankStr & "-" & lastClosed.ToString("0000")
                    Else
                        bankStr = bankStr & "," & lastClosed.ToString("0000")
                    End If
                End If
                WriteInstrument("CLOSE " & bankStr)
            End If
        End If

        If sName = "saS601" Then
            closedSwitchesExist = False
            bankStr = "1."
            lastClosed = -1
            inSwitchGroup = False
            row = 2
            switchList.Clear()
            switchValue = 0

            Me.tabOptions.SelectedIndex = 5
            For i = 0 To saS601.Count - 1
                sNextValue = Strings.Left(sData, InStr(sData, ":") - 1)
                sData = Strings.Right(sData, Len(sData) - InStr(sData, ":"))
                saS601.Switches(i).Text = sNextValue
                If sNextValue.ToUpper() = "CLOSE" Then
                    saS601.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Closed
                    closedSwitchesExist = True

                    switchValue = 2000 + (0 Mod 6) * 100 + saS601.Switches(i).Index
                    switchList.Add(switchValue)
                Else
                    saS601.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Open
                End If
                Me.tabOptions_Page6.Refresh()
            Next

            'Open all of the switches
            WriteInstrument("OPEN 2.3200-3203")

            'Close the closed switches
            currentSwitchAddedToList = False
            If closedSwitchesExist Then
                inSwitchGroup = False
                switchList.Sort()
                For Each switchItem In switchList
                    If bankStr = "1." Then
                        bankStr = bankStr & switchItem.ToString("0000")
                        currentSwitchAddedToList = True
                    Else
                        If lastClosed <> switchItem - 1 Then
                            If inSwitchGroup Then
                                bankStr = bankStr & "-" & lastClosed.ToString("0000") & "," & switchItem.ToString("0000")
                            Else
                                bankStr = bankStr & "," & switchItem.ToString("0000")
                            End If
                            inSwitchGroup = False
                            currentSwitchAddedToList = True
                        Else
                            inSwitchGroup = True
                            currentSwitchAddedToList = False
                        End If
                    End If
                    lastClosed = switchItem
                Next
                If Not currentSwitchAddedToList Then
                    If inSwitchGroup Then
                        bankStr = bankStr & "-" & lastClosed.ToString("0000")
                    Else
                        bankStr = bankStr & "," & lastClosed.ToString("0000")
                    End If
                End If
                WriteInstrument("CLOSE " & bankStr)
            End If
        End If

        If sName = "saS602" Then
            closedSwitchesExist = False
            bankStr = "1."
            lastClosed = -1
            inSwitchGroup = False
            row = 2
            switchList.Clear()
            switchValue = 0

            Me.tabOptions.SelectedIndex = 5
            For i = 0 To saS602.Count - 1
                sNextValue = Strings.Left(sData, InStr(sData, ":") - 1)
                sData = Strings.Right(sData, Len(sData) - InStr(sData, ":"))
                saS602.Switches(i).Text = sNextValue
                If sNextValue.ToUpper() = "CLOSE" Then
                    saS602.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Closed
                    closedSwitchesExist = True

                    switchValue = 2000 + (1 Mod 6) * 100 + saS602.Switches(i).Index
                    switchList.Add(switchValue)
                Else
                    saS602.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Open
                End If
                Me.tabOptions_Page6.Refresh()
            Next

            'Open all of the switches
            WriteInstrument("OPEN 2.3200-3203")

            'Close the closed switches
            currentSwitchAddedToList = False
            If closedSwitchesExist Then
                inSwitchGroup = False
                switchList.Sort()
                For Each switchItem In switchList
                    If bankStr = "1." Then
                        bankStr = bankStr & switchItem.ToString("0000")
                        currentSwitchAddedToList = True
                    Else
                        If lastClosed <> switchItem - 1 Then
                            If inSwitchGroup Then
                                bankStr = bankStr & "-" & lastClosed.ToString("0000") & "," & switchItem.ToString("0000")
                            Else
                                bankStr = bankStr & "," & switchItem.ToString("0000")
                            End If
                            inSwitchGroup = False
                            currentSwitchAddedToList = True
                        Else
                            inSwitchGroup = True
                            currentSwitchAddedToList = False
                        End If
                    End If
                    lastClosed = switchItem
                Next
                If Not currentSwitchAddedToList Then
                    If inSwitchGroup Then
                        bankStr = bankStr & "-" & lastClosed.ToString("0000")
                    Else
                        bankStr = bankStr & "," & lastClosed.ToString("0000")
                    End If
                End If
                WriteInstrument("CLOSE " & bankStr)
            End If
        End If

        If sName = "saS603" Then
            closedSwitchesExist = False
            bankStr = "1."
            lastClosed = -1
            inSwitchGroup = False
            row = 2
            switchList.Clear()
            switchValue = 0

            Me.tabOptions.SelectedIndex = 5
            For i = 0 To saS603.Count - 1
                sNextValue = Strings.Left(sData, InStr(sData, ":") - 1)
                sData = Strings.Right(sData, Len(sData) - InStr(sData, ":"))
                saS603.Switches(i).Text = sNextValue
                If sNextValue.ToUpper() = "CLOSE" Then
                    saS603.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Closed
                    closedSwitchesExist = True

                    switchValue = 2000 + (2 Mod 6) * 100 + saS603.Switches(i).Index
                    switchList.Add(switchValue)
                Else
                    saS603.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Open
                End If
                Me.tabOptions_Page6.Refresh()
            Next

            'Open all of the switches
            WriteInstrument("OPEN 2.3200-3203")

            'Close the closed switches
            currentSwitchAddedToList = False
            If closedSwitchesExist Then
                inSwitchGroup = False
                switchList.Sort()
                For Each switchItem In switchList
                    If bankStr = "1." Then
                        bankStr = bankStr & switchItem.ToString("0000")
                        currentSwitchAddedToList = True
                    Else
                        If lastClosed <> switchItem - 1 Then
                            If inSwitchGroup Then
                                bankStr = bankStr & "-" & lastClosed.ToString("0000") & "," & switchItem.ToString("0000")
                            Else
                                bankStr = bankStr & "," & switchItem.ToString("0000")
                            End If
                            inSwitchGroup = False
                            currentSwitchAddedToList = True
                        Else
                            inSwitchGroup = True
                            currentSwitchAddedToList = False
                        End If
                    End If
                    lastClosed = switchItem
                Next
                If Not currentSwitchAddedToList Then
                    If inSwitchGroup Then
                        bankStr = bankStr & "-" & lastClosed.ToString("0000")
                    Else
                        bankStr = bankStr & "," & lastClosed.ToString("0000")
                    End If
                End If
                WriteInstrument("CLOSE " & bankStr)
            End If
        End If

        If sName = "saS604" Then
            closedSwitchesExist = False
            bankStr = "1."
            lastClosed = -1
            inSwitchGroup = False
            row = 2
            switchList.Clear()
            switchValue = 0

            Me.tabOptions.SelectedIndex = 5
            For i = 0 To saS604.Count - 1
                sNextValue = Strings.Left(sData, InStr(sData, ":") - 1)
                sData = Strings.Right(sData, Len(sData) - InStr(sData, ":"))
                saS604.Switches(i).Text = sNextValue
                If sNextValue.ToUpper() = "CLOSE" Then
                    saS604.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Closed
                    closedSwitchesExist = True

                    switchValue = 2000 + (3 Mod 6) * 100 + saS604.Switches(i).Index
                    switchList.Add(switchValue)
                Else
                    saS604.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Open
                End If
                Me.tabOptions_Page6.Refresh()
            Next

            'Open all of the switches
            WriteInstrument("OPEN 2.3200-3203")

            'Close the closed switches
            currentSwitchAddedToList = False
            If closedSwitchesExist Then
                inSwitchGroup = False
                switchList.Sort()
                For Each switchItem In switchList
                    If bankStr = "1." Then
                        bankStr = bankStr & switchItem.ToString("0000")
                        currentSwitchAddedToList = True
                    Else
                        If lastClosed <> switchItem - 1 Then
                            If inSwitchGroup Then
                                bankStr = bankStr & "-" & lastClosed.ToString("0000") & "," & switchItem.ToString("0000")
                            Else
                                bankStr = bankStr & "," & switchItem.ToString("0000")
                            End If
                            inSwitchGroup = False
                            currentSwitchAddedToList = True
                        Else
                            inSwitchGroup = True
                            currentSwitchAddedToList = False
                        End If
                    End If
                    lastClosed = switchItem
                Next
                If Not currentSwitchAddedToList Then
                    If inSwitchGroup Then
                        bankStr = bankStr & "-" & lastClosed.ToString("0000")
                    Else
                        bankStr = bankStr & "," & lastClosed.ToString("0000")
                    End If
                End If
                WriteInstrument("CLOSE " & bankStr)
            End If
        End If

        If sName = "saS605" Then
            closedSwitchesExist = False
            bankStr = "1."
            lastClosed = -1
            inSwitchGroup = False
            row = 2
            switchList.Clear()
            switchValue = 0

            tabOptions.SelectedIndex = 5
            For i = 0 To saS605.Count - 1
                sNextValue = Strings.Left(sData, InStr(sData, ":") - 1)
                sData = Strings.Right(sData, Len(sData) - InStr(sData, ":"))
                saS605.Switches(i).Text = sNextValue
                If sNextValue.ToUpper() = "CLOSE" Then
                    saS605.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Closed
                    closedSwitchesExist = True

                    switchValue = 2000 + (4 Mod 6) * 100 + saS605.Switches(i).Index
                    switchList.Add(switchValue)
                Else
                    saS605.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Open
                End If
                Me.tabOptions_Page6.Refresh()
            Next

            'Open all of the switches
            WriteInstrument("OPEN 2.3200-3203")

            'Close the closed switches
            currentSwitchAddedToList = False
            If closedSwitchesExist Then
                inSwitchGroup = False
                switchList.Sort()
                For Each switchItem In switchList
                    If bankStr = "1." Then
                        bankStr = bankStr & switchItem.ToString("0000")
                        currentSwitchAddedToList = True
                    Else
                        If lastClosed <> switchItem - 1 Then
                            If inSwitchGroup Then
                                bankStr = bankStr & "-" & lastClosed.ToString("0000") & "," & switchItem.ToString("0000")
                            Else
                                bankStr = bankStr & "," & switchItem.ToString("0000")
                            End If
                            inSwitchGroup = False
                            currentSwitchAddedToList = True
                        Else
                            inSwitchGroup = True
                            currentSwitchAddedToList = False
                        End If
                    End If
                    lastClosed = switchItem
                Next
                If Not currentSwitchAddedToList Then
                    If inSwitchGroup Then
                        bankStr = bankStr & "-" & lastClosed.ToString("0000")
                    Else
                        bankStr = bankStr & "," & lastClosed.ToString("0000")
                    End If
                End If
                WriteInstrument("CLOSE " & bankStr)
            End If
        End If

        If sName = "saS606" Then
            closedSwitchesExist = False
            bankStr = "1."
            lastClosed = -1
            inSwitchGroup = False
            row = 2
            switchList.Clear()
            switchValue = 0

            Me.tabOptions.SelectedIndex = 5
            For i = 0 To saS606.Count - 1
                sNextValue = Strings.Left(sData, InStr(sData, ":") - 1)
                sData = Strings.Right(sData, Len(sData) - InStr(sData, ":"))
                saS606.Switches(i).Text = sNextValue
                If sNextValue.ToUpper() = "CLOSE" Then
                    saS606.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Closed
                    closedSwitchesExist = True

                    switchValue = 2000 + (5 Mod 6) * 100 + saS606.Switches(i).Index
                    switchList.Add(switchValue)
                Else
                    saS606.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Open
                End If
                Me.tabOptions_Page6.Refresh()
            Next

            'Open all of the switches
            WriteInstrument("OPEN 2.3200-3203")

            'Close the closed switches
            currentSwitchAddedToList = False
            If closedSwitchesExist Then
                inSwitchGroup = False
                switchList.Sort()
                For Each switchItem In switchList
                    If bankStr = "1." Then
                        bankStr = bankStr & switchItem.ToString("0000")
                        currentSwitchAddedToList = True
                    Else
                        If lastClosed <> switchItem - 1 Then
                            If inSwitchGroup Then
                                bankStr = bankStr & "-" & lastClosed.ToString("0000") & "," & switchItem.ToString("0000")
                            Else
                                bankStr = bankStr & "," & switchItem.ToString("0000")
                            End If
                            inSwitchGroup = False
                            currentSwitchAddedToList = True
                        Else
                            inSwitchGroup = True
                            currentSwitchAddedToList = False
                        End If
                    End If
                    lastClosed = switchItem
                Next
                If Not currentSwitchAddedToList Then
                    If inSwitchGroup Then
                        bankStr = bankStr & "-" & lastClosed.ToString("0000")
                    Else
                        bankStr = bankStr & "," & lastClosed.ToString("0000")
                    End If
                End If
                WriteInstrument("CLOSE " & bankStr)
            End If
        End If

        If sName = "saS607" Then
            closedSwitchesExist = False
            bankStr = "2."
            lastClosed = -1
            inSwitchGroup = False
            row = 2
            switchList.Clear()
            switchValue = 0

            tabOptions.SelectedIndex = 5
            For i = 0 To saS607.Count - 1
                sNextValue = Strings.Left(sData, InStr(sData, ":") - 1)
                sData = Strings.Right(sData, Len(sData) - InStr(sData, ":"))
                saS607.Switches(i).Text = sNextValue
                If sNextValue.ToUpper() = "CLOSE" Then
                    saS607.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Closed
                    closedSwitchesExist = True

                    switchValue = 2000 + (6 Mod 6) * 100 + saS607.Switches(i).Index
                    switchList.Add(switchValue)
                Else
                    saS607.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Open
                End If
                Me.tabOptions_Page6.Refresh()
            Next

            'Open all of the switches
            WriteInstrument("OPEN 2.3200-3203")

            'Close the closed switches
            currentSwitchAddedToList = False
            If closedSwitchesExist Then
                inSwitchGroup = False
                switchList.Sort()
                For Each switchItem In switchList
                    If bankStr = "1." Then
                        bankStr = bankStr & switchItem.ToString("0000")
                        currentSwitchAddedToList = True
                    Else
                        If lastClosed <> switchItem - 1 Then
                            If inSwitchGroup Then
                                bankStr = bankStr & "-" & lastClosed.ToString("0000") & "," & switchItem.ToString("0000")
                            Else
                                bankStr = bankStr & "," & switchItem.ToString("0000")
                            End If
                            inSwitchGroup = False
                            currentSwitchAddedToList = True
                        Else
                            inSwitchGroup = True
                            currentSwitchAddedToList = False
                        End If
                    End If
                    lastClosed = switchItem
                Next
                If Not currentSwitchAddedToList Then
                    If inSwitchGroup Then
                        bankStr = bankStr & "-" & lastClosed.ToString("0000")
                    Else
                        bankStr = bankStr & "," & lastClosed.ToString("0000")
                    End If
                End If
                WriteInstrument("CLOSE " & bankStr)
            End If
        End If

        If sName = "saS608" Then
            closedSwitchesExist = False
            bankStr = "2."
            lastClosed = -1
            inSwitchGroup = False
            row = 2
            switchList.Clear()
            switchValue = 0

            Me.tabOptions.SelectedIndex = 5
            For i = 0 To saS608.Count - 1
                sNextValue = Strings.Left(sData, InStr(sData, ":") - 1)
                sData = Strings.Right(sData, Len(sData) - InStr(sData, ":"))
                saS608.Switches(i).Text = sNextValue
                If sNextValue.ToUpper() = "CLOSE" Then
                    saS608.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Closed
                    closedSwitchesExist = True

                    switchValue = 2000 + (7 Mod 6) * 100 + saS608.Switches(i).Index
                    switchList.Add(switchValue)
                Else
                    saS608.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Open
                End If
                Me.tabOptions_Page6.Refresh()
            Next

            'Open all of the switches
            WriteInstrument("OPEN 2.3200-3203")

            'Close the closed switches
            currentSwitchAddedToList = False
            If closedSwitchesExist Then
                inSwitchGroup = False
                switchList.Sort()
                For Each switchItem In switchList
                    If bankStr = "1." Then
                        bankStr = bankStr & switchItem.ToString("0000")
                        currentSwitchAddedToList = True
                    Else
                        If lastClosed <> switchItem - 1 Then
                            If inSwitchGroup Then
                                bankStr = bankStr & "-" & lastClosed.ToString("0000") & "," & switchItem.ToString("0000")
                            Else
                                bankStr = bankStr & "," & switchItem.ToString("0000")
                            End If
                            inSwitchGroup = False
                            currentSwitchAddedToList = True
                        Else
                            inSwitchGroup = True
                            currentSwitchAddedToList = False
                        End If
                    End If
                    lastClosed = switchItem
                Next
                If Not currentSwitchAddedToList Then
                    If inSwitchGroup Then
                        bankStr = bankStr & "-" & lastClosed.ToString("0000")
                    Else
                        bankStr = bankStr & "," & lastClosed.ToString("0000")
                    End If
                End If
                WriteInstrument("CLOSE " & bankStr)
            End If
        End If

        If sName = "saS609" Then
            closedSwitchesExist = False
            bankStr = "2."
            lastClosed = -1
            inSwitchGroup = False
            row = 2
            switchList.Clear()
            switchValue = 0

            Me.tabOptions.SelectedIndex = 5
            For i = 0 To saS609.Count - 1
                sNextValue = Strings.Left(sData, InStr(sData, ":") - 1)
                sData = Strings.Right(sData, Len(sData) - InStr(sData, ":"))
                saS609.Switches(i).Text = sNextValue
                If sNextValue.ToUpper() = "CLOSE" Then
                    saS609.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Closed
                    closedSwitchesExist = True

                    switchValue = 2000 + (8 Mod 6) * 100 + saS609.Switches(i).Index
                    switchList.Add(switchValue)
                Else
                    saS609.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Open
                End If
                Me.tabOptions_Page6.Refresh()
            Next

            'Open all of the switches
            WriteInstrument("OPEN 2.3200-3203")

            'Close the closed switches
            currentSwitchAddedToList = False
            If closedSwitchesExist Then
                inSwitchGroup = False
                switchList.Sort()
                For Each switchItem In switchList
                    If bankStr = "1." Then
                        bankStr = bankStr & switchItem.ToString("0000")
                        currentSwitchAddedToList = True
                    Else
                        If lastClosed <> switchItem - 1 Then
                            If inSwitchGroup Then
                                bankStr = bankStr & "-" & lastClosed.ToString("0000") & "," & switchItem.ToString("0000")
                            Else
                                bankStr = bankStr & "," & switchItem.ToString("0000")
                            End If
                            inSwitchGroup = False
                            currentSwitchAddedToList = True
                        Else
                            inSwitchGroup = True
                            currentSwitchAddedToList = False
                        End If
                    End If
                    lastClosed = switchItem
                Next
                If Not currentSwitchAddedToList Then
                    If inSwitchGroup Then
                        bankStr = bankStr & "-" & lastClosed.ToString("0000")
                    Else
                        bankStr = bankStr & "," & lastClosed.ToString("0000")
                    End If
                End If
                WriteInstrument("CLOSE " & bankStr)
            End If
        End If

        If sName = "saS610" Then
            closedSwitchesExist = False
            bankStr = "2."
            lastClosed = -1
            inSwitchGroup = False
            row = 2
            switchList.Clear()
            switchValue = 0

            Me.tabOptions.SelectedIndex = 5
            For i = 0 To saS610.Count - 1
                sNextValue = Strings.Left(sData, InStr(sData, ":") - 1)
                sData = Strings.Right(sData, Len(sData) - InStr(sData, ":"))
                saS610.Switches(i).Text = sNextValue
                If sNextValue.ToUpper() = "CLOSE" Then
                    saS610.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Closed
                    closedSwitchesExist = True

                    switchValue = 2000 + (9 Mod 6) * 100 + saS610.Switches(i).Index
                    switchList.Add(switchValue)
                Else
                    saS610.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Open
                End If
                Me.tabOptions_Page6.Refresh()
            Next

            'Open all of the switches
            WriteInstrument("OPEN 2.3200-3203")

            'Close the closed switches
            currentSwitchAddedToList = False
            If closedSwitchesExist Then
                inSwitchGroup = False
                switchList.Sort()
                For Each switchItem In switchList
                    If bankStr = "1." Then
                        bankStr = bankStr & switchItem.ToString("0000")
                        currentSwitchAddedToList = True
                    Else
                        If lastClosed <> switchItem - 1 Then
                            If inSwitchGroup Then
                                bankStr = bankStr & "-" & lastClosed.ToString("0000") & "," & switchItem.ToString("0000")
                            Else
                                bankStr = bankStr & "," & switchItem.ToString("0000")
                            End If
                            inSwitchGroup = False
                            currentSwitchAddedToList = True
                        Else
                            inSwitchGroup = True
                            currentSwitchAddedToList = False
                        End If
                    End If
                    lastClosed = switchItem
                Next
                If Not currentSwitchAddedToList Then
                    If inSwitchGroup Then
                        bankStr = bankStr & "-" & lastClosed.ToString("0000")
                    Else
                        bankStr = bankStr & "," & lastClosed.ToString("0000")
                    End If
                End If
                WriteInstrument("CLOSE " & bankStr)
            End If
        End If

        If sName = "saS611" Then
            closedSwitchesExist = False
            bankStr = "2."
            lastClosed = -1
            inSwitchGroup = False
            row = 2
            switchList.Clear()
            switchValue = 0

            Me.tabOptions.SelectedIndex = 5
            For i = 0 To saS611.Count - 1
                sNextValue = Strings.Left(sData, InStr(sData, ":") - 1)
                sData = Strings.Right(sData, Len(sData) - InStr(sData, ":"))
                saS611.Switches(i).Text = sNextValue
                If sNextValue.ToUpper() = "CLOSE" Then
                    saS611.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Closed
                    closedSwitchesExist = True

                    switchValue = 2000 + (10 Mod 6) * 100 + saS611.Switches(i).Index
                    switchList.Add(switchValue)
                Else
                    saS611.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Open
                End If
                Me.tabOptions_Page6.Refresh()
            Next

            'Open all of the switches
            WriteInstrument("OPEN 2.3200-3203")

            'Close the closed switches
            currentSwitchAddedToList = False
            If closedSwitchesExist Then
                inSwitchGroup = False
                switchList.Sort()
                For Each switchItem In switchList
                    If bankStr = "1." Then
                        bankStr = bankStr & switchItem.ToString("0000")
                        currentSwitchAddedToList = True
                    Else
                        If lastClosed <> switchItem - 1 Then
                            If inSwitchGroup Then
                                bankStr = bankStr & "-" & lastClosed.ToString("0000") & "," & switchItem.ToString("0000")
                            Else
                                bankStr = bankStr & "," & switchItem.ToString("0000")
                            End If
                            inSwitchGroup = False
                            currentSwitchAddedToList = True
                        Else
                            inSwitchGroup = True
                            currentSwitchAddedToList = False
                        End If
                    End If
                    lastClosed = switchItem
                Next
                If Not currentSwitchAddedToList Then
                    If inSwitchGroup Then
                        bankStr = bankStr & "-" & lastClosed.ToString("0000")
                    Else
                        bankStr = bankStr & "," & lastClosed.ToString("0000")
                    End If
                End If
                WriteInstrument("CLOSE " & bankStr)
            End If
        End If

        If sName = "saS612" Then
            closedSwitchesExist = False
            bankStr = "2."
            lastClosed = -1
            inSwitchGroup = False
            row = 2
            switchList.Clear()
            switchValue = 0

            Me.tabOptions.SelectedIndex = 5
            For i = 0 To saS612.Count - 1
                sNextValue = Strings.Left(sData, InStr(sData, ":") - 1)
                sData = Strings.Right(sData, Len(sData) - InStr(sData, ":"))
                saS612.Switches(i).Text = sNextValue
                If sNextValue.ToUpper() = "CLOSE" Then
                    saS612.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Closed
                    closedSwitchesExist = True

                    switchValue = 2000 + (11 Mod 6) * 100 + saS612.Switches(i).Index
                    switchList.Add(switchValue)
                Else
                    saS612.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Open
                End If
                Me.tabOptions_Page6.Refresh()
            Next

            'Open all of the switches
            WriteInstrument("OPEN 2.3200-3203")

            'Close the closed switches
            currentSwitchAddedToList = False
            If closedSwitchesExist Then
                inSwitchGroup = False
                switchList.Sort()
                For Each switchItem In switchList
                    If bankStr = "1." Then
                        bankStr = bankStr & switchItem.ToString("0000")
                        currentSwitchAddedToList = True
                    Else
                        If lastClosed <> switchItem - 1 Then
                            If inSwitchGroup Then
                                bankStr = bankStr & "-" & lastClosed.ToString("0000") & "," & switchItem.ToString("0000")
                            Else
                                bankStr = bankStr & "," & switchItem.ToString("0000")
                            End If
                            inSwitchGroup = False
                            currentSwitchAddedToList = True
                        Else
                            inSwitchGroup = True
                            currentSwitchAddedToList = False
                        End If
                    End If
                    lastClosed = switchItem
                Next
                If Not currentSwitchAddedToList Then
                    If inSwitchGroup Then
                        bankStr = bankStr & "-" & lastClosed.ToString("0000")
                    Else
                        bankStr = bankStr & "," & lastClosed.ToString("0000")
                    End If
                End If
                WriteInstrument("CLOSE " & bankStr)
            End If
        End If

        If sName = "saS701" Then
            closedSwitchesExist = False
            bankStr = "3."
            lastClosed = -1
            inSwitchGroup = False
            row = 2
            switchList.Clear()
            switchValue = 0

            Me.tabOptions.SelectedIndex = 6
            For i = 0 To saS701.Count - 1
                sNextValue = Strings.Left(sData, InStr(sData, ":") - 1)
                sData = Strings.Right(sData, Len(sData) - InStr(sData, ":"))
                saS701.Switches(i).Text = sNextValue
                If sNextValue.ToUpper() = "CLOSE" Then
                    saS701.Switches(i).SwitchState = ctl2W1Row.SwitchStates.Closed
                    closedSwitchesExist = True

                    switchValue = ((saS701.Switches(i).Index \ 8) + 1) * 10 + saS701.Switches(i).Index Mod 8
                    switchList.Add(switchValue)
                Else
                    saS701.Switches(i).SwitchState = ctl2W1Row.SwitchStates.Open
                End If
                Me.tabOptions_Page7.Refresh()
            Next

            'Open all of the switches
            WriteInstrument("OPEN 3.0010-0017,0020-0027,0030-0037")

            'Close the closed switches
            currentSwitchAddedToList = False
            If closedSwitchesExist Then
                inSwitchGroup = False
                switchList.Sort()
                For Each switchItem In switchList
                    If bankStr = "3." Then
                        bankStr = bankStr & switchItem.ToString("0000")
                        currentSwitchAddedToList = True
                    Else
                        If lastClosed <> switchItem - 1 Then
                            If inSwitchGroup Then
                                bankStr = bankStr & "-" & lastClosed.ToString("0000") & "," & switchItem.ToString("0000")
                            Else
                                bankStr = bankStr & "," & switchItem.ToString("0000")
                            End If
                            inSwitchGroup = False
                            currentSwitchAddedToList = True
                        Else
                            inSwitchGroup = True
                            currentSwitchAddedToList = False
                        End If
                    End If
                    lastClosed = switchItem
                Next
                If Not currentSwitchAddedToList Then
                    If inSwitchGroup Then
                        bankStr = bankStr & "-" & lastClosed.ToString("0000")
                    Else
                        bankStr = bankStr & "," & lastClosed.ToString("0000")
                    End If
                End If
                WriteInstrument("CLOSE " & bankStr)
            End If
        End If

        If sName = "saS751" Then
            closedSwitchesExist = False
            bankStr = "3."
            lastClosed = -1
            inSwitchGroup = False
            row = 2
            switchList.Clear()
            switchValue = 0

            Me.tabOptions.SelectedIndex = 6
            For i = 0 To saS751.Count - 1
                sNextValue = Strings.Left(sData, InStr(sData, ":") - 1)
                sData = Strings.Right(sData, Len(sData) - InStr(sData, ":"))
                saS751.Switches(i).Text = sNextValue
                If sNextValue.ToUpper() = "CLOSE" Then
                    saS751.Switches(i).SwitchState = ctl2W1Row.SwitchStates.Closed
                    closedSwitchesExist = True

                    switchValue = saS751.Switches(i).Index Mod 8
                    switchList.Add(switchValue)
                Else
                    saS751.Switches(i).SwitchState = ctl2W1Row.SwitchStates.Open
                End If
                Me.tabOptions_Page7.Refresh()
            Next

            'Open all of the switches
            WriteInstrument("OPEN 3.0010-0017")

            'Close the closed switches
            currentSwitchAddedToList = False
            If closedSwitchesExist Then
                inSwitchGroup = False
                switchList.Sort()
                For Each switchItem In switchList
                    If bankStr = "3." Then
                        bankStr = bankStr & switchItem.ToString("0000")
                        currentSwitchAddedToList = True
                    Else
                        If lastClosed <> switchItem - 1 Then
                            If inSwitchGroup Then
                                bankStr = bankStr & "-" & lastClosed.ToString("0000") & "," & switchItem.ToString("0000")
                            Else
                                bankStr = bankStr & "," & switchItem.ToString("0000")
                            End If
                            inSwitchGroup = False
                            currentSwitchAddedToList = True
                        Else
                            inSwitchGroup = True
                            currentSwitchAddedToList = False
                        End If
                    End If
                    lastClosed = switchItem
                Next
                If Not currentSwitchAddedToList Then
                    If inSwitchGroup Then
                        bankStr = bankStr & "-" & lastClosed.ToString("0000")
                    Else
                        bankStr = bankStr & "," & lastClosed.ToString("0000")
                    End If
                End If
                WriteInstrument("CLOSE " & bankStr)
            End If
        End If

        If sName = "saS801" Then
            Me.tabOptions.SelectedIndex = 7
            Set1x8Mux(saS801, Convert.ToInt16(sData))
            bankStr = "4.0" & Convert.ToInt16(sData).ToString()
            WriteInstrument("CLOSE " & bankStr)
            Me.tabOptions_Page8.Refresh()
        End If

        If sName = "saS802" Then
            Me.tabOptions.SelectedIndex = 7
            Set1x8Mux(saS802, Convert.ToInt16(sData))
            bankStr = "4.1" & Convert.ToInt16(sData).ToString()
            WriteInstrument("CLOSE " & bankStr)
            Me.tabOptions_Page8.Refresh()
        End If

        If sName = "saS803" Then
            Me.tabOptions.SelectedIndex = 7
            Set1x8Mux(saS803, Convert.ToInt16(sData))
            bankStr = "4.2" & Convert.ToInt16(sData).ToString()
            WriteInstrument("CLOSE " & bankStr)
            Me.tabOptions_Page8.Refresh()
        End If

        If sName = "saS804" Then
            Me.tabOptions.SelectedIndex = 7
            Set1x8Mux(saS804, Convert.ToInt16(sData))
            bankStr = "4.3" & Convert.ToInt16(sData).ToString()
            WriteInstrument("CLOSE " & bankStr)
            Me.tabOptions_Page8.Refresh()
        End If

        If sName = "saS901" Then
            Me.tabOptions.SelectedIndex = 8
            Set1x6Mux(saS901, Convert.ToInt16(sData))
            bankStr = "5.0" & Convert.ToInt16(sData).ToString()
            WriteInstrument("CLOSE " & bankStr)
            Me.tabOptions_Page9.Refresh()
        End If

        If sName = "saS902" Then
            Me.tabOptions.SelectedIndex = 8
            Set1x6Mux(saS902, Convert.ToInt16(sData))
            bankStr = "5.1" & Convert.ToInt16(sData).ToString()
            WriteInstrument("CLOSE " & bankStr)
            Me.tabOptions_Page9.Refresh()
        End If

        If sName = "saS903" Then
            Me.tabOptions.SelectedIndex = 8
            Set1x6Mux(saS903, Convert.ToInt16(sData))
            bankStr = "5.2" & Convert.ToInt16(sData).ToString()
            WriteInstrument("CLOSE " & bankStr)
            Me.tabOptions_Page9.Refresh()
        End If

        If sName = "saS904" Then
            Me.tabOptions.SelectedIndex = 8
            Set1x6Mux(saS904, Convert.ToInt16(sData))
            bankStr = "5.3" & Convert.ToInt16(sData).ToString()
            WriteInstrument("CLOSE " & bankStr)
            Me.tabOptions_Page9.Refresh()
        End If

        If sName = "saS905" Then
            Me.tabOptions.SelectedIndex = 8
            Set1x6Mux(saS905, Convert.ToInt16(sData))
            bankStr = "5.4" & Convert.ToInt16(sData).ToString()
            WriteInstrument("CLOSE " & bankStr)
            Me.tabOptions_Page9.Refresh()
        End If

        If sName = "saS906" Then
            Me.tabOptions.SelectedIndex = 8
            Set1x6Mux(saS906, Convert.ToInt16(sData))
            bankStr = "5.5" & Convert.ToInt16(sData).ToString()
            WriteInstrument("CLOSE " & bankStr)
            Me.tabOptions_Page9.Refresh()
        End If
        Me.Refresh()
    End Sub

    Private Sub Set1x8Mux(ByRef switch As SwitchLib.ctl1x8Mux, ByVal pin As Short)
        Select Case pin
            Case 0
                switch.SwitchState = ctl1x8Mux.SwitchStates.Pin1
                iStatus = 1

            Case 1
                switch.SwitchState = ctl1x8Mux.SwitchStates.Pin2
                iStatus = 1

            Case 2
                switch.SwitchState = ctl1x8Mux.SwitchStates.Pin3
                iStatus = 1

            Case 3
                switch.SwitchState = ctl1x8Mux.SwitchStates.Pin4
                iStatus = 1

            Case 4
                switch.SwitchState = ctl1x8Mux.SwitchStates.Pin5
                iStatus = 1

            Case 5
                switch.SwitchState = ctl1x8Mux.SwitchStates.Pin6
                iStatus = 1

            Case 6
                switch.SwitchState = ctl1x8Mux.SwitchStates.Pin7
                iStatus = 1

            Case 7
                switch.SwitchState = ctl1x8Mux.SwitchStates.Pin8
                iStatus = 1

            Case 8
                switch.SwitchState = ctl1x8Mux.SwitchStates.Pin9
        End Select
    End Sub

    Private Sub Set1x6Mux(ByRef switch As SwitchLib.ctl1x6Mux, ByVal pin As Short)
        Select Case pin
            Case 0
                switch.SwitchState = ctl1x6Mux.SwitchStates.Pin1
                iStatus = 1

            Case 1
                switch.SwitchState = ctl1x6Mux.SwitchStates.Pin2
                iStatus = 1

            Case 2
                switch.SwitchState = ctl1x6Mux.SwitchStates.Pin3
                iStatus = 1

            Case 3
                switch.SwitchState = ctl1x6Mux.SwitchStates.Pin4
                iStatus = 1

            Case 4
                switch.SwitchState = ctl1x6Mux.SwitchStates.Pin5
                iStatus = 1

            Case 5
                switch.SwitchState = ctl1x6Mux.SwitchStates.Pin6
                iStatus = 1

            Case 6
                switch.SwitchState = ctl1x6Mux.SwitchStates.Pin7
        End Select
    End Sub

    Private Sub cmdExercise_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdExercise.Click
        Dim iLoop As Short
        Dim bUserAbort As Boolean
        Dim ErrorFlag As Boolean

        If MsgBox("The Relay Exerciser will close and open all switches of the selected card(s) ten(10) times." & vbCrLf & "Is it safe to continue?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Critical + MsgBoxStyle.Question) = DialogResult.No Then
            HelpPanel("")
            Exit Sub
        End If

        'mh lblDdeLink.LinkMode = vbLinkNone

        'Check to see if any DC supplies are ON
        'mh If DCSupply_Check(bUserAbort) = False Then Exit Sub

        'jrc 031209
        bUserAbort = False
        'If bUserAbort = True Then GoTo UserAbort
        Try
            'exercise the selected card
            Select Case cboCardType.Text
                Case "------ALL Cards------"
                    For iLoop = 1 To 10
                        'Test the two 1260-39's
                        If Relay39_Test(1, bUserAbort) = True Then ErrorFlag = True
                        If bUserAbort = True Then Throw New SystemException("")

                        If Relay39_Test(2, bUserAbort) = True Then ErrorFlag = True
                        If bUserAbort = True Then Throw New SystemException("")

                        'Test the 1260-38T
                        If Relay38T_Test(bUserAbort) = True Then ErrorFlag = True
                        If bUserAbort = True Then Throw New SystemException("")

                        'Test the 1260-58
                        If Relay58_Test(bUserAbort) = True Then ErrorFlag = True
                        If bUserAbort = True Then Throw New SystemException("")

                        'Test the 1260-66A
                        If Relay66A_Test(bUserAbort) = True Then ErrorFlag = True
                        If bUserAbort = True Then Throw New SystemException("")
                    Next iLoop
                    'Update display
                    cmdS101Reset_Click()
                    cmdS200ResetAll_Click()
                    cmdS301ResetAll_Click()
                    cmdS400ResetAll_Click()
                    cmdS500ResetAll_Click()
                    cmdS600ResetAll_Click()
                    cmdS700ResetAll_Click()
                    cmdS800ResetAll_Click()
                    cmdS900ResetAll_Click()

                Case "(1) 1260-39 Card"
                    For iLoop = 1 To 10
                        'Test the first 1260-39
                        If Relay39_Test(1, bUserAbort) = True Then ErrorFlag = True
                        If bUserAbort = True Then Throw New SystemException("")
                    Next iLoop
                    'Update display
                    Open39SwitchDisp(1)

                Case "(2) 1260-39 Card"
                    For iLoop = 1 To 10
                        'Test the second 1260-39
                        If Relay39_Test(2, bUserAbort) = True Then ErrorFlag = True
                        If bUserAbort = True Then Throw New SystemException("")
                    Next iLoop
                    'Update display
                    Open39SwitchDisp(2)

                Case "(3) 1260-38T Card"
                    For iLoop = 1 To 10
                        'Test the 1260-38T
                        If Relay38T_Test(bUserAbort) = True Then ErrorFlag = True
                        If bUserAbort = True Then Throw New SystemException("")
                    Next iLoop
                    'Update display
                    cmdS200ResetAll_Click()
                    cmdS700ResetAll_Click()

                Case "(4) 1260-58 Card"
                    For iLoop = 1 To 10
                        'Test the 1260-58
                        If Relay58_Test(bUserAbort) = True Then ErrorFlag = True
                        If bUserAbort = True Then Throw New SystemException("")
                    Next iLoop
                    'Update display
                    cmdS800ResetAll_Click()

                Case "(5) 1260-66A Card"
                    For iLoop = 1 To 10
                        'Test the 1260-66A
                        If Relay66A_Test(bUserAbort) = True Then ErrorFlag = True
                        If bUserAbort = True Then Throw New SystemException("")
                    Next iLoop
                    'Update display
                    cmdS900ResetAll_Click()


                Case Else
                    MsgBox("No Valid Card selected.", MsgBoxStyle.Information)
            End Select

            HelpPanel("Relay Exerciser completed.")
'            Application.DoEvents()
            If Not ErrorFlag Then
                MsgBox("Relay Exerciser Passed", MsgBoxStyle.Information)
            End If
            HelpPanel("")
        Catch
            HelpPanel("")
            MsgBox("Relay Exerciser aborted by user.", MsgBoxStyle.Information)
        End Try
    End Sub

    Private Sub Open39SwitchDisp(ByRef iNum As Short)
        'Opens the switches by the card number 1 or 2 Display only
        Dim i As Short, j As Short
        Dim iStart As Short, iStop As Short
        Dim col As Integer = 0

        If iNum < 1 Or iNum > 2 Then Exit Sub

        If iNum = 2 Then iNum = 4

        If iNum = 2 Then col = 1
        For i = 0 To saS101.Count - 1
            If saS101.Switches(i).Column = col Then
                saS101.Switches(i).SwitchState = DPST.SwitchStates.Open
                saS101.Switches(i).Text = "Open"
            End If
        Next

        'set range for card
        If iNum = 1 Then
            iStart = 1
            iStop = 2
        Else
            iStart = 3
            iStop = 4
        End If

        'Open the 301 switches
        For i = 1 To saS301.Count - 1
            For j = iStart To iStop
                If saS301.Switches(i).Column = j Then
                    saS301.Switches(i).Text = "Open"
                    saS301.Switches(i).SwitchState = SPST.SwitchStates.Open
                End If
            Next
        Next

        'Open the 400 switches
        If iNum = 1 Then
            For i = 0 To saS401.Count - 1
                saS401.Switches(i).Text = "Open"
                saS401.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Open
            Next

            For i = 0 To saS402.Count - 1
                saS402.Switches(i).Text = "Open"
                saS402.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Open
            Next

            For i = 0 To saS403.Count - 1
                saS403.Switches(i).Text = "Open"
                saS403.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Open
            Next
        Else
            For i = 0 To saS404.Count - 1
                saS404.Switches(i).Text = "Open"
                saS404.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Open
            Next

            For i = 0 To saS405.Count - 1
                saS405.Switches(i).Text = "Open"
                saS405.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Open
            Next

            For i = 0 To saS406.Count - 1
                saS406.Switches(i).Text = "Open"
                saS406.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Open
            Next
        End If

        'Open the 500 switches
        If iNum = 1 Then
            For i = 0 To saS501.Count - 1
                saS501.Switches(i).TopSwitchText = "Open"
                saS501.Switches(i).TopSwitchState = ctl1W2Row.SwitchStates.Open
                saS501.Switches(i).BottomSwitchText = "Open"
                saS501.Switches(i).BottomSwitchState = ctl1W2Row.SwitchStates.Open
            Next

            For i = 0 To saS502.Count - 1
                saS502.Switches(i).TopSwitchText = "Open"
                saS502.Switches(i).TopSwitchState = ctl1W2Row.SwitchStates.Open
                saS502.Switches(i).BottomSwitchText = "Open"
                saS502.Switches(i).BottomSwitchState = ctl1W2Row.SwitchStates.Open
            Next

            For i = 0 To saS503.Count - 1
                saS503.Switches(i).TopSwitchText = "Open"
                saS503.Switches(i).TopSwitchState = ctl1W2Row.SwitchStates.Open
                saS503.Switches(i).BottomSwitchText = "Open"
                saS503.Switches(i).BottomSwitchState = ctl1W2Row.SwitchStates.Open
            Next

            For i = 0 To saS504.Count - 1
                saS504.Switches(i).TopSwitchText = "Open"
                saS504.Switches(i).TopSwitchState = ctl1W2Row.SwitchStates.Open
                saS504.Switches(i).BottomSwitchText = "Open"
                saS504.Switches(i).BottomSwitchState = ctl1W2Row.SwitchStates.Open
            Next

            For i = 0 To saS505.Count - 1
                saS505.Switches(i).TopSwitchText = "Open"
                saS505.Switches(i).TopSwitchState = ctl1W2Row.SwitchStates.Open
                saS505.Switches(i).BottomSwitchText = "Open"
                saS505.Switches(i).BottomSwitchState = ctl1W2Row.SwitchStates.Open
            Next
        Else
            For i = 0 To saS506.Count - 1
                saS506.Switches(i).TopSwitchText = "Open"
                saS506.Switches(i).TopSwitchState = ctl1W2Row.SwitchStates.Open
                saS506.Switches(i).BottomSwitchText = "Open"
                saS506.Switches(i).BottomSwitchState = ctl1W2Row.SwitchStates.Open
            Next

            For i = 0 To saS507.Count - 1
                saS507.Switches(i).TopSwitchText = "Open"
                saS507.Switches(i).TopSwitchState = ctl1W2Row.SwitchStates.Open
                saS507.Switches(i).BottomSwitchText = "Open"
                saS507.Switches(i).BottomSwitchState = ctl1W2Row.SwitchStates.Open
            Next

            For i = 0 To saS508.Count - 1
                saS508.Switches(i).TopSwitchText = "Open"
                saS508.Switches(i).TopSwitchState = ctl1W2Row.SwitchStates.Open
                saS508.Switches(i).BottomSwitchText = "Open"
                saS508.Switches(i).BottomSwitchState = ctl1W2Row.SwitchStates.Open
            Next

            For i = 0 To saS509.Count - 1
                saS509.Switches(i).TopSwitchText = "Open"
                saS509.Switches(i).TopSwitchState = ctl1W2Row.SwitchStates.Open
                saS509.Switches(i).BottomSwitchText = "Open"
                saS509.Switches(i).BottomSwitchState = ctl1W2Row.SwitchStates.Open
            Next

            For i = 0 To saS510.Count - 1
                saS510.Switches(i).TopSwitchText = "Open"
                saS510.Switches(i).TopSwitchState = ctl1W2Row.SwitchStates.Open
                saS510.Switches(i).BottomSwitchText = "Open"
                saS510.Switches(i).BottomSwitchState = ctl1W2Row.SwitchStates.Open
            Next
        End If

        'Open the 600 switches
        If iNum = 1 Then
            For i = 0 To saS601.Count - 1
                saS601.Switches(i).Text = "Open"
                saS601.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Open
            Next

            For i = 0 To saS602.Count - 1
                saS602.Switches(i).Text = "Open"
                saS602.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Open
            Next

            For i = 0 To saS603.Count - 1
                saS603.Switches(i).Text = "Open"
                saS603.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Open
            Next

            For i = 0 To saS604.Count - 1
                saS604.Switches(i).Text = "Open"
                saS604.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Open
            Next

            For i = 0 To saS605.Count - 1
                saS605.Switches(i).Text = "Open"
                saS605.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Open
            Next

            For i = 0 To saS606.Count - 1
                saS606.Switches(i).Text = "Open"
                saS606.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Open
            Next
        Else
            For i = 0 To saS607.Count - 1
                saS607.Switches(i).Text = "Open"
                saS607.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Open
            Next

            For i = 0 To saS608.Count - 1
                saS608.Switches(i).Text = "Open"
                saS608.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Open
            Next

            For i = 0 To saS609.Count - 1
                saS609.Switches(i).Text = "Open"
                saS609.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Open
            Next

            For i = 0 To saS610.Count - 1
                saS610.Switches(i).Text = "Open"
                saS610.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Open
            Next

            For i = 0 To saS611.Count - 1
                saS611.Switches(i).Text = "Open"
                saS611.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Open
            Next

            For i = 0 To saS612.Count - 1
                saS612.Switches(i).Text = "Open"
                saS612.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Open
            Next
        End If
    End Sub

    Private Sub cmdExerciseGroupDCPower_Click(sender As Object, e As EventArgs) Handles cmdExerciseGroupDCPower.Click
        cmdExerciseGroup_Click(0)
    End Sub

    Private Sub cmdExerciseGroup2X24_2W_LF_Click(sender As Object, e As EventArgs) Handles cmdExerciseGroup2X24_2W_LF.Click
        cmdExerciseGroup_Click(1)
    End Sub

    Private Sub cmdExerciseGroup1x1_1W_LF_Click(sender As Object, e As EventArgs) Handles cmdExerciseGroup1x1_1W_LF.Click
        cmdExerciseGroup_Click(2)
    End Sub

    Private Sub cmdExerciseGroup1x4_1W_LF_Click(sender As Object, e As EventArgs) Handles cmdExerciseGroup1x4_1W_LF.Click
        cmdExerciseGroup_Click(3)
    End Sub

    Private Sub cmdExerciseGroup2x8_1W_LF_Click(sender As Object, e As EventArgs) Handles cmdExerciseGroup2x8_1W_LF.Click
        cmdExerciseGroup_Click(4)
    End Sub

    Private Sub cmdExerciseGroup1x2_1W_LF_Click(sender As Object, e As EventArgs) Handles cmdExerciseGroup1x2_1W_LF.Click
        cmdExerciseGroup_Click(5)
    End Sub

    Private Sub cmdExerciseGroup1x24_1x8_2W_LF_Click(sender As Object, e As EventArgs) Handles cmdExerciseGroup1x24_1x8_2W_LF.Click
        cmdExerciseGroup_Click(6)
    End Sub

    Private Sub cmdExerciseGroup1x8_Coax_MF_Click(sender As Object, e As EventArgs) Handles cmdExerciseGroup1x8_Coax_MF.Click
        cmdExerciseGroup_Click(7)
    End Sub

    Private Sub cmdExerciseGroup1x6_Coax_HF_Click(sender As Object, e As EventArgs) Handles cmdExerciseGroup1x6_Coax_HF.Click
        cmdExerciseGroup_Click(8)
    End Sub

    Private Sub cmdExerciseGroup_Click(Index As Integer)
        Dim iLoop As Short
        Dim bUserAbort As Boolean
        Dim ErrorFlag As Boolean
        Dim sMsg As String

        If MsgBox("The Relay Exerciser will close and open all switches of the Group ten(10) times." & vbCrLf & _
                  "VERIFY THE PPU ARE NOT ACTIVATED" & vbCrLf & "Is it safe to continue?", _
                  MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Critical + MsgBoxStyle.Question) = DialogResult.No Then
            HelpPanel("")
            Exit Sub
        End If

        'mh lblDdeLink.LinkMode = vbLinkNone

        'Check to see if any DC supplies are ON
        'mh  If DCSupply_Check(bUserAbort) = False Then Exit Sub

        'jrc 031209
        bUserAbort = False
        'If bUserAbort = True Then GoTo UserAbort

        Try
            ErrorFlag = False
            'jrc 031209
            UserReset = False
            'exercise the selected Group
            Select Case Index
                Case 0
                    For iLoop = 1 To 10
                        'Test the 101 Series switches
                        If UserReset Then Throw New SystemException("")

                        WriteInstrument("Close 1.0000-0004;2.0000-0004")
                        If Not InstrumentError Then
                            WriteInstrument("Open 1.0000-0004;2.0000-0004")
                        End If
                        If InstrumentError Then
                            sMsg = "Built-In Test Failed." & vbCrLf
                            sMsg &= "Switch group 101 Series failed relay coil test."
                            MsgBox(sMsg, MsgBoxStyle.Critical)
                            ErrorFlag = True
                        End If
                    Next iLoop
                    'Update the display
                    cmdS101Reset_Click()
                Case 1
                    For iLoop = 1 To 10
                        'Test the 201 Series switches
                        If UserReset Then Throw New SystemException("")

                        WriteInstrument("Close 3.0040-0157")
                        If Not InstrumentError Then
                            WriteInstrument("Open 3.0040-0157")
                        End If
                        If InstrumentError Then
                            sMsg = "Built-In Test Failed." & vbCrLf
                            sMsg &= "Switch group 201 Series failed relay coil test."
                            MsgBox(sMsg, MsgBoxStyle.Critical)
                            ErrorFlag = True
                        End If
                    Next iLoop
                    'Update the display
                    cmdS200ResetAll_Click()
                Case 2
                    For iLoop = 1 To 10
                        'Test the 301 Series switches
                        If UserReset Then Throw New SystemException("")

                        WriteInstrument("Close 1.1000-1047;2.1000-1047")
                        If Not InstrumentError Then
                            WriteInstrument("Open 1.1000-1047;2.1000-1047")
                        End If
                        If InstrumentError Then
                            sMsg = "Built-In Test Failed." & vbCrLf
                            sMsg &= "Switch group 301 Series failed relay coil test."
                            MsgBox(sMsg, MsgBoxStyle.Critical)
                            ErrorFlag = True
                        End If
                    Next iLoop
                    'Update the display
                    cmdS301ResetAll_Click()
                Case 3
                    For iLoop = 1 To 10
                        'Test the 400 Series switches
                        If UserReset Then Throw New SystemException("")

                        WriteInstrument("Close 1.3000-3203;2.3000-3203")
                        If Not InstrumentError Then
                            WriteInstrument("Open 1.3000-3203;2.3000-3203")
                        End If
                        If InstrumentError Then
                            sMsg = "Built-In Test Failed." & vbCrLf
                            sMsg &= "Switch group 400 Series failed relay coil test."
                            MsgBox(sMsg, MsgBoxStyle.Critical)
                            ErrorFlag = True
                        End If
                    Next iLoop
                    'Update the display
                    cmdS400ResetAll_Click()
                Case 4
                    For iLoop = 1 To 10
                        'Test the 500 Series switches
                        If UserReset Then Throw New SystemException("")

                        WriteInstrument("Close 1.4000-4417;2.4000-4417")
                        If Not InstrumentError Then
                            WriteInstrument("Open 1.4000-4417;2.4000-4417")
                        End If
                        If InstrumentError Then
                            sMsg = "Built-In Test Failed." & vbCrLf
                            sMsg &= "Switch group 500 Series failed relay coil test."
                            MsgBox(sMsg, MsgBoxStyle.Critical)
                            ErrorFlag = True
                        End If
                    Next iLoop
                    'Update the display
                    cmdS500ResetAll_Click()
                Case 5
                    For iLoop = 1 To 10
                        'Test the 600 Series switches
                        If UserReset Then Throw New SystemException("")

                        WriteInstrument("Close 1.2000-2501;2.2000-2501")
                        If Not InstrumentError Then
                            WriteInstrument("Open 1.2000-2501;2.2000-2501")
                        End If
                        If InstrumentError Then
                            sMsg = "Built-In Test Failed." & vbCrLf
                            sMsg &= "Switch group 600 Series failed relay coil test."
                            MsgBox(sMsg, MsgBoxStyle.Critical)
                            ErrorFlag = True
                        End If
                    Next iLoop
                    'Update the display
                    cmdS600ResetAll_Click()
                Case 6
                    For iLoop = 1 To 10
                        'Test the 701 and 751 Series switches
                        If UserReset Then Throw New SystemException("")

                        WriteInstrument("Close 3.0000-0037")
                        If Not InstrumentError Then
                            WriteInstrument("Open 3.0000-0037")
                        End If
                        If InstrumentError Then
                            sMsg = "Built-In Test Failed." & vbCrLf
                            sMsg &= "Switch group 701 and 751 Series failed relay coil test."
                            MsgBox(sMsg, MsgBoxStyle.Critical)
                            ErrorFlag = True
                        End If
                    Next iLoop
                    'Update the display
                    cmdS700ResetAll_Click()
                Case 7
                    For iLoop = 1 To 10
                        'Test the 800 Series switches (all on one card)
                        ErrorFlag = Relay58_Test(bUserAbort)
                        If bUserAbort = True Then Throw New SystemException("")
                    Next iLoop
                    'Update the display
                    cmdS800ResetAll_Click()
                Case 8
                    For iLoop = 1 To 10
                        'Test the 900 Series switches (all on one card)
                        ErrorFlag = Relay66A_Test(bUserAbort)
                        If bUserAbort = True Then Throw New SystemException("")
                    Next iLoop
                    'Update the display
                    cmdS900ResetAll_Click()

                Case Else
                    MsgBox("Invalid Group selected", MsgBoxStyle.Information)
            End Select

            HelpPanel("Relay Exerciser completed.")
            If Not ErrorFlag Then
                MsgBox("Relay Exerciser Passed", MsgBoxStyle.Information)
            End If
            HelpPanel("")
        Catch
            HelpPanel("")
            MsgBox("Relay Exerciser aborted by user.", MsgBoxStyle.Information)

        End Try
    End Sub

    Private Sub cmdHelp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdHelp.Click
        frmHelp.ShowDialog()
    End Sub

    Private Sub cmdOpenAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdOpenAll.Click
        'set a wait cursor
        Me.Cursor = Cursors.WaitCursor
        ' open all groups
        cmdS101Reset_Click()
        cmdS200ResetAll_Click()
        cmdS301ResetAll_Click()
        cmdS400ResetAll_Click()
        cmdS500ResetAll_Click()
        cmdS600ResetAll_Click()
        cmdS700ResetAll_Click()
        cmdS800ResetAll_Click()
        cmdS900ResetAll_Click()
        'set a default cursor
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub cmdQuit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdQuit.Click
        Me.Close()
    End Sub

    Private Sub cmdReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdReset.Click
        WriteInstrument("RESET")
        SetControlsToReset()
        tabOptions.SelectedIndex = TAB_DCPOWER 'JHill 1.2
        UserReset = True
    End Sub

    Public Sub cmdS101Reset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdS101Reset.Click
        cmdS101Reset_Click()
    End Sub
    Public Sub cmdS101Reset_Click()
        Dim i As Short

        For i = 0 To saS101.Count - 1
            saS101.Switches(i).SwitchState = DPST.SwitchStates.Open
            saS101.Switches(i).Text = "Open"
        Next

        If PanelConifgPushed = True Then Exit Sub

        WriteInstrument("OPEN 1.0000-0004;2.0000-0004")
    End Sub

    Public Sub cmdS200ResetAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdS200ResetAll.Click
        cmdS200ResetAll_Click()
    End Sub
    Public Sub cmdS200ResetAll_Click()
        Dim i As Short

        tabOptions.SelectedIndex = 1
        For i = 0 To saS201.Count - 1
            saS201.Switches(i).TopSwitchText = "Open"
            saS201.Switches(i).TopSwitchState = ctl2W2Row.SwitchStates.Open
            saS201.Switches(i).BottomSwitchText = "Open"
            saS201.Switches(i).BottomSwitchState = ctl2W2Row.SwitchStates.Open
        Next
        saS201.Refresh()
        For i = 0 To saS202.Count - 1
            saS202.Switches(i).TopSwitchText = "Open"
            saS202.Switches(i).TopSwitchState = ctl2W2Row.SwitchStates.Open
            saS202.Switches(i).BottomSwitchText = "Open"
            saS202.Switches(i).BottomSwitchState = ctl2W2Row.SwitchStates.Open
        Next
        saS202.Refresh()

        If PanelConifgPushed = True Then Exit Sub
        WriteInstrument("OPEN 3.0040-0157")
    End Sub


    Public Sub cmdS301ResetAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdS301ResetAll.Click
        cmdS301ResetAll_Click()
    End Sub
    Public Sub cmdS301ResetAll_Click()
        Dim i As Short

        tabOptions.SelectedIndex = 2
        For i = 0 To saS301.Count - 1
            saS301.Switches(i).Text = "Open"
            saS301.Switches(i).SwitchState = SPST.SwitchStates.Open
        Next
        If PanelConifgPushed = True Then Exit Sub
        WriteInstrument("OPEN 1.1000-1047;2.1000-1047")
    End Sub

    Public Sub cmdS400ResetAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdS400ResetAll.Click
        cmdS400ResetAll_Click()
    End Sub
    Public Sub cmdS400ResetAll_Click()
        Dim i As Short

        tabOptions.SelectedIndex = 3
        For i = 0 To saS401.Count - 1
            saS401.Switches(i).Text = "Open"
            saS401.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Open
        Next

        For i = 0 To saS402.Count - 1
            saS402.Switches(i).Text = "Open"
            saS402.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Open
        Next

        For i = 0 To saS403.Count - 1
            saS403.Switches(i).Text = "Open"
            saS403.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Open
        Next

        For i = 0 To saS404.Count - 1
            saS404.Switches(i).Text = "Open"
            saS404.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Open
        Next

        For i = 0 To saS405.Count - 1
            saS405.Switches(i).Text = "Open"
            saS405.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Open
        Next

        For i = 0 To saS406.Count - 1
            saS406.Switches(i).Text = "Open"
            saS406.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Open
        Next

        If PanelConifgPushed = True Then Exit Sub
        WriteInstrument("OPEN 1.3000-3203;2.3000-3203")
    End Sub

    Public Sub cmdS500ResetAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdS500ResetAll.Click
        cmdS500ResetAll_Click()
    End Sub
    Public Sub cmdS500ResetAll_Click()
        Dim i As Short

        tabOptions.SelectedIndex = 4
        For i = 0 To saS501.Count - 1
            saS501.Switches(i).TopSwitchText = "Open"
            saS501.Switches(i).TopSwitchState = ctl1W2Row.SwitchStates.Open
            saS501.Switches(i).BottomSwitchText = "Open"
            saS501.Switches(i).BottomSwitchState = ctl1W2Row.SwitchStates.Open
        Next

        For i = 0 To saS502.Count - 1
            saS502.Switches(i).TopSwitchText = "Open"
            saS502.Switches(i).TopSwitchState = ctl1W2Row.SwitchStates.Open
            saS502.Switches(i).BottomSwitchText = "Open"
            saS502.Switches(i).BottomSwitchState = ctl1W2Row.SwitchStates.Open
        Next

        For i = 0 To saS503.Count - 1
            saS503.Switches(i).TopSwitchText = "Open"
            saS503.Switches(i).TopSwitchState = ctl1W2Row.SwitchStates.Open
            saS503.Switches(i).BottomSwitchText = "Open"
            saS503.Switches(i).BottomSwitchState = ctl1W2Row.SwitchStates.Open
        Next

        For i = 0 To saS504.Count - 1
            saS504.Switches(i).TopSwitchText = "Open"
            saS504.Switches(i).TopSwitchState = ctl1W2Row.SwitchStates.Open
            saS504.Switches(i).BottomSwitchText = "Open"
            saS504.Switches(i).BottomSwitchState = ctl1W2Row.SwitchStates.Open
        Next

        For i = 0 To saS505.Count - 1
            saS505.Switches(i).TopSwitchText = "Open"
            saS505.Switches(i).TopSwitchState = ctl1W2Row.SwitchStates.Open
            saS505.Switches(i).BottomSwitchText = "Open"
            saS505.Switches(i).BottomSwitchState = ctl1W2Row.SwitchStates.Open
        Next

        For i = 0 To saS506.Count - 1
            saS506.Switches(i).TopSwitchText = "Open"
            saS506.Switches(i).TopSwitchState = ctl1W2Row.SwitchStates.Open
            saS506.Switches(i).BottomSwitchText = "Open"
            saS506.Switches(i).BottomSwitchState = ctl1W2Row.SwitchStates.Open
        Next

        For i = 0 To saS507.Count - 1
            saS507.Switches(i).TopSwitchText = "Open"
            saS507.Switches(i).TopSwitchState = ctl1W2Row.SwitchStates.Open
            saS507.Switches(i).BottomSwitchText = "Open"
            saS507.Switches(i).BottomSwitchState = ctl1W2Row.SwitchStates.Open
        Next

        For i = 0 To saS508.Count - 1
            saS508.Switches(i).TopSwitchText = "Open"
            saS508.Switches(i).TopSwitchState = ctl1W2Row.SwitchStates.Open
            saS508.Switches(i).BottomSwitchText = "Open"
            saS508.Switches(i).BottomSwitchState = ctl1W2Row.SwitchStates.Open
        Next

        For i = 0 To saS509.Count - 1
            saS509.Switches(i).TopSwitchText = "Open"
            saS509.Switches(i).TopSwitchState = ctl1W2Row.SwitchStates.Open
            saS509.Switches(i).BottomSwitchText = "Open"
            saS509.Switches(i).BottomSwitchState = ctl1W2Row.SwitchStates.Open
        Next

        For i = 0 To saS510.Count - 1
            saS510.Switches(i).TopSwitchText = "Open"
            saS510.Switches(i).TopSwitchState = ctl1W2Row.SwitchStates.Open
            saS510.Switches(i).BottomSwitchText = "Open"
            saS510.Switches(i).BottomSwitchState = ctl1W2Row.SwitchStates.Open
        Next

        If PanelConifgPushed = True Then Exit Sub
        WriteInstrument("OPEN 1.4000-4417;2.4000-4417")
    End Sub


    Public Sub cmdS600ResetAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdS600ResetAll.Click
        cmdS600ResetAll_Click()
    End Sub
    Public Sub cmdS600ResetAll_Click()
        Dim i As Integer

        tabOptions.SelectedIndex = 5
        For i = 0 To saS601.Count - 1
            saS601.Switches(i).Text = "Open"
            saS601.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Open
        Next

        For i = 0 To saS602.Count - 1
            saS602.Switches(i).Text = "Open"
            saS602.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Open
        Next

        For i = 0 To saS603.Count - 1
            saS603.Switches(i).Text = "Open"
            saS603.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Open
        Next

        For i = 0 To saS604.Count - 1
            saS604.Switches(i).Text = "Open"
            saS604.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Open
        Next

        For i = 0 To saS605.Count - 1
            saS605.Switches(i).Text = "Open"
            saS605.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Open
        Next

        For i = 0 To saS606.Count - 1
            saS606.Switches(i).Text = "Open"
            saS606.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Open
        Next

        For i = 0 To saS607.Count - 1
            saS607.Switches(i).Text = "Open"
            saS607.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Open
        Next

        For i = 0 To saS608.Count - 1
            saS608.Switches(i).Text = "Open"
            saS608.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Open
        Next

        For i = 0 To saS609.Count - 1
            saS609.Switches(i).Text = "Open"
            saS609.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Open
        Next

        For i = 0 To saS610.Count - 1
            saS610.Switches(i).Text = "Open"
            saS610.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Open
        Next

        For i = 0 To saS611.Count - 1
            saS611.Switches(i).Text = "Open"
            saS611.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Open
        Next

        For i = 0 To saS612.Count - 1
            saS612.Switches(i).Text = "Open"
            saS612.Switches(i).SwitchState = ctl1W1Row.SwitchStates.Open
        Next

        If PanelConifgPushed = True Then Exit Sub
        WriteInstrument("OPEN 1.2000-2501;2.2000-2501")
    End Sub

    Public Sub cmdS700ResetAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdS700ResetAll.Click
        cmdS700ResetAll_Click()
    End Sub
    Public Sub cmdS700ResetAll_Click()
        Dim i As Short

        tabOptions.SelectedIndex = 6
        For i = 0 To saS701.Count - 1
            saS701.Switches(i).Text = "Open"
            saS701.Switches(i).SwitchState = ctl2W1Row.SwitchStates.Open
        Next

        For i = 0 To saS751.Count - 1
            saS751.Switches(i).Text = "Open"
            saS751.Switches(i).SwitchState = ctl2W1Row.SwitchStates.Open
        Next

        If PanelConifgPushed = True Then Exit Sub
        WriteInstrument("OPEN 3.0000-0037")
    End Sub

    Public Sub cmdS800ResetAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdS800ResetAll.Click
        cmdS800ResetAll_Click()
    End Sub
    Public Sub cmdS800ResetAll_Click()
        tabOptions.SelectedIndex = 7
        saS801.SwitchState = ctl1x8Mux.SwitchStates.Pin9
        saS802.SwitchState = ctl1x8Mux.SwitchStates.Pin9
        saS803.SwitchState = ctl1x8Mux.SwitchStates.Pin9
        saS804.SwitchState = ctl1x8Mux.SwitchStates.Pin9

        If PanelConifgPushed = True Then Exit Sub
        WriteInstrument("OPEN 4.00-37")
    End Sub

    Public Sub cmdS900ResetAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdS900ResetAll.Click
        cmdS900ResetAll_Click()
    End Sub
    Public Sub cmdS900ResetAll_Click()
        tabOptions.SelectedIndex = 8
        saS901.SwitchState = ctl1x6Mux.SwitchStates.Pin7
        saS902.SwitchState = ctl1x6Mux.SwitchStates.Pin7
        saS903.SwitchState = ctl1x6Mux.SwitchStates.Pin7
        saS904.SwitchState = ctl1x6Mux.SwitchStates.Pin7
        saS905.SwitchState = ctl1x6Mux.SwitchStates.Pin7
        saS906.SwitchState = ctl1x6Mux.SwitchStates.Pin7

        If PanelConifgPushed = True Then Exit Sub
        WriteInstrument("Open 5.00-55")
    End Sub

    Private Sub cmdTest_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdTest.Click
        Dim i As Short
        Dim ErrorFlag As Boolean
        Dim message As String
        Dim bUserAbort As Boolean

        HelpPanel("Performing Built-In Test...")

        If Not LiveMode Then
            MsgBox("Built-In Test is not available.  Live mode is disabled.", MsgBoxStyle.Information)
            HelpPanel("")
            Exit Sub
        End If

        UserReset = False
        Try
        'Run the Controller tests
            For i = 1 To 3
                If UserReset Then Throw New SystemException("")
                WriteInstrument("TEST 0." & i)
                If ReadSwitch() <> "7F" Then
                    message = "Built-In Test " & i & " Failed."
                    WriteInstrument("YERR")
                    message &= vbCrLf & DecodeErrorMessage(ReadSwitch())
                    MsgBox(message, MsgBoxStyle.Critical)
                    HelpPanel("")
                    Exit Sub
                End If
            Next i

            If MsgBox("The following tests will close and open all switches." & vbCrLf & "VERIFY THE PPUs ARE NOT ACTIVATED" & vbCrLf & "Is it safe to continue?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Critical + MsgBoxStyle.Question) = DialogResult.No Then
                HelpPanel("")
                Exit Sub
            End If

            'mh lblDdeLink.LinkMode = vbLinkNone

            'ADVINT Note: Moved several sections to functions to be used with RelayExercisers

            'Check to see if any DC supplies are ON
            'mh If DCSupply_Check(bUserAbort) = False Then Exit Sub
            If bUserAbort = True Then Throw New SystemException("")

            SetControlsToReset()
            WriteInstrument("RESET")
            ErrorFlag = False

            'Perform confidence test on all relays with module level diagnostic resolution
            WriteInstrument("CNF ON")

            'Test the two 1260-39's
            For i = 1 To 2
                If Relay39_Test(i, bUserAbort) = True Then ErrorFlag = True
                If bUserAbort = True Then Throw New SystemException("")
            Next i

            'Test the 1260-38T
            If Relay38T_Test(bUserAbort) = True Then ErrorFlag = True
            If bUserAbort = True Then Throw New SystemException("")

            'Test the 1260-58
            If Relay58_Test(bUserAbort) = True Then ErrorFlag = True
            If bUserAbort = True Then Throw New SystemException("")

            'Test the 1260-66A
            If Relay66A_Test(bUserAbort) = True Then ErrorFlag = True
            If bUserAbort = True Then Throw New SystemException("")


            WriteInstrument("CNF OFF")
            WriteInstrument("RESET")
            SetControlsToReset()
            HelpPanel("Built-In Test completed.")
            If Not ErrorFlag Then
                MsgBox("Built-In Test Passed", MsgBoxStyle.Information)
            End If
            HelpPanel("")
        Catch
            HelpPanel("")
            MsgBox("Built-In Test aborted by user.", MsgBoxStyle.Information)
        End Try
    End Sub
'#Const defUse_DCSupply_Check = True
#If defUse_DCSupply_Check Then
    Private Function DCSupply_Check(ByRef bUserAbort As Boolean) As Boolean
        Const conDdeNoApp As Short = 282
        Dim i As Short
        'mhlblDdeLink.LinkTopic = "SysMon|frmSysMon"
        lblDdeLink.LinkTopic = "Sysmon|Sysmon"
        'This loop checks to see if any DC supplies are ON by looking at the
        'voltage displays in the System Monitor application.
        'This is a safety check done because 1) the SAIF wiring connects
        'the DC supply outputs together through the DC Power relays and
        '2) this built-in test will close all relays, causing a potential
        'short.
        For i = 1 To 10
            If UserReset Then
                bUserAbort = True
                Exit Function
            End If
            lblDdeLink.LinkItem = "lblUutVolt(" & i & ")"
            On Error Resume Next
            'The next statement will generate error code 282 if the System Monitor is not running.
            '  If that occurs, the assumption is made that the DC supplies are off.
            '  For any other error, some kind of malfunction is assumed and will be reported.
            '  Then, since the operator already was already asked if it's safe, we will continue.
            lblDdeLink.LinkMode = vbLinkManual
            If CompareErrNumber("=", conDdeNoApp) Then
                Exit For
            ElseIf Err Then
                MsgBox(ErrorToString(Err.Number), MsgBoxStyle.Information)
                Exit For
            End If
            lblDdeLink.LinkRequest 'Retrieve the power supply voltage
            If Val(lblDdeLink.Text)<>0 Then
                MsgBox("DC" & i & " is at " & lblDdeLink.Text & "V.  Turn off all UUT supplies to test switches.  Built-In Test aborted...", MsgBoxStyle.Exclamation)
                lblDdeLink.LinkMode = vbLinkNone 'Terminate the DDE Link
                HelpPanel("")
                DCSupply_Check = False
                Exit Function
            End If
        Next i

        lblDdeLink.LinkMode = vbLinkNone 'Terminate the DDE Link

        DCSupply_Check = True
    End Function
#End If

    Private Function Relay39_Test(ByVal Num As Short, ByRef bUserAbort As Boolean) As Boolean
        Dim ErrorFlag As Boolean
        Dim sMsg As String

        ErrorFlag = False
        'Test a 1260-39
        If UserReset Then
            bUserAbort = True
            Relay39_Test = ErrorFlag
            Exit Function
        End If
        WriteInstrument("Close " & Convert.ToString(Num) & ".0000-4417")
        If Not InstrumentError Then
            WriteInstrument("Open " & Convert.ToString(Num) & ".0000-4417")
        End If
        If InstrumentError Then
            sMsg = "Built-In Test Failed." & vbCrLf
            sMsg &= "Switch module LF-" & Convert.ToString(Num) & " (1260-39) failed relay coil test."
            MsgBox(sMsg, MsgBoxStyle.Critical)
            ErrorFlag = True
        End If

        Relay39_Test = ErrorFlag
    End Function
    Private Function Relay38T_Test(ByRef bUserAbort As Boolean) As Boolean
        Dim ErrorFlag As Boolean
        Dim sMsg As String

        ErrorFlag = False
        'Test the 1260-38T
        If UserReset Then
            bUserAbort = True
            Relay38T_Test = ErrorFlag
            Exit Function
        End If

        WriteInstrument("Close 3.0000-8002")
        If Not InstrumentError Then
            WriteInstrument("Open 3.0000-8002")
        End If
        If InstrumentError Then
            sMsg = "Built-In Test Failed." & vbCrLf
            sMsg &= "Switch module LF-3 (1260-38T) failed relay coil test."
            MsgBox(sMsg, MsgBoxStyle.Critical)
            ErrorFlag = True
        End If

        Relay38T_Test = ErrorFlag
    End Function

    Private Function Relay58_Test(ByRef bUserAbort As Boolean) As Boolean
        Dim i As Short, j As Short
        Dim ErrorFlag As Boolean
        Dim sMsg As String

        ErrorFlag = False
        'Test the 1260-58
        For i = 0 To 7
            For j = 0 To 3
                If UserReset Then
                    bUserAbort = True
                    Relay58_Test = ErrorFlag
                    Exit Function
                End If
                WriteInstrument("Close 4." & j & i)
                If InstrumentError Then Exit For
                WriteInstrument("Open 4." & j & i)
                If InstrumentError Then Exit For
            Next j
            If InstrumentError Then Exit For
        Next i
        If InstrumentError Then
            sMsg = "Built-In Test Failed." & vbCrLf
            sMsg &= "Switch module MF (1260-58) failed relay coil test."
            MsgBox(sMsg, MsgBoxStyle.Critical)
            ErrorFlag = True
        End If

        Relay58_Test = ErrorFlag
    End Function

    Private Function Relay66A_Test(ByRef bUserAbort As Boolean) As Boolean
        Dim i As Short, j As Short
        Dim ErrorFlag As Boolean
        Dim sMsg As String

        ErrorFlag = False
        'Test the 1260-66A
        If RfOptionInstalled Then
            For i = 0 To 5
                For j = 0 To 5
                    If UserReset Then
                        bUserAbort = True
                        Relay66A_Test = ErrorFlag
                        Exit Function
                    End If
                    WriteInstrument("Close 5." & j & i)
                    If InstrumentError Then Exit For
                    WriteInstrument("Open 5." & j & i)
                    If InstrumentError Then Exit For
                Next j
                If InstrumentError Then Exit For
            Next i
            If InstrumentError Then
                sMsg = "Built-In Test Failed." & vbCrLf
                sMsg &= "Switch module HF (1260-66A) failed relay coil test."
                MsgBox(sMsg, MsgBoxStyle.Critical)
                ErrorFlag = True
            End If
        End If

        Relay66A_Test = ErrorFlag
    End Function

    Private Sub frmRac1260_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Center this form
        Me.Top = PrimaryScreen.Bounds.Height / 2 - Me.Height / 2
        Me.Left = PrimaryScreen.Bounds.Width / 2 - Me.Width / 2

        '   Set Common Controls parent properties
        Atlas_SFP.Parent_Object = Me
        Panel_Conifg.Parent_Object = Me

        Main()
    End Sub

    Private Sub frmRac1260_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        If DoNotTalk Then Exit Sub
        If Me.WindowState = MINIMIZED Then Exit Sub
        Dim i As Short

        'Keep a minimun to the height and width.  Otherwise part of the 1x1-1W-LF switch set will not be accessable
        Me.Height = Limit(Me.Height, 537)
        Me.Width = Limit(Me.Width, 638)

        tabOptions.SetBounds(8, 8, Limit(Me.Width - 37, 0), Limit(Me.Height - 113, 0), BoundsSpecified.Location Or BoundsSpecified.Size)
        cmdHelp.SetBounds(Limit(Me.Width - 298, 0), Limit(Me.Height - 94, 0), 0, 0, BoundsSpecified.Location)
        cmdReset.SetBounds(Limit(Me.Width - 209, 0), Limit(Me.Height - 94, 0), 0, 0, BoundsSpecified.Location)
        cmdQuit.SetBounds(Limit(Me.Width - 112, 0), Limit(Me.Height - 94, 0), 0, 0, BoundsSpecified.Location)

        For i = 0 To 1
            panS200_0.Width = Limit(tabOptions.Width - 45, 0)
            panS200_1.Width = Limit(tabOptions.Width - 45, 0)
        Next i

        panS301.Width = Limit(tabOptions.Width - 27, 0)
        panS301.Height = Limit(tabOptions.Height - 110, 0)
        cmdS301ResetAll.Top = Limit(tabOptions.Height - 89, 0)
        cmdExerciseGroup1x1_1W_LF.Top = Limit(tabOptions.Height - 89, 0)

        panS701.Width = Limit(tabOptions.Width - 30, 0)
        panS751.Width = Limit(tabOptions.Width - 30, 0)

        fraPinList.Height = Limit(tabOptions.Height - 196, 0)
        cmdAbout.Top = Limit(fraPinList.Height + 31, 0)
        cmdTest.Top = Limit(fraPinList.Height + 31, 0)
        cmdOpenAll.Top = Limit(fraPinList.Height + 31, 0)
        ' grouped differently now  cmdAtlas.Top = Limit(fraPinList.Height + 765, 0)

        panSortSig.Height = Limit(fraPinList.Height - 29, 0)

        panSortCnx.Height = Limit(fraPinList.Height - 29, 0)
    End Sub

    Private Sub saS101_SwitchClick(sender As Object, e As SwitchEventArgs) Handles saS101.SwitchClick
        saS101.Switches(e.Index).Text = "Open"
        If saS101.Switches(e.Index).SwitchState = DPST.SwitchStates.Closed Then
            saS101.Switches(e.Index).Text = "Close"
        End If

        If saS101.Switches(e.Index).Column = 0 Then
            Desc_39.Addr = 1
        Else
            Desc_39.Addr = 2
        End If
        Desc_39.Type = 0
        Desc_39.mSelect = 0
        Desc_39.Chan = saS101.Switches(e.Index).Row Mod 5

        WriteInstrument(saS101.Switches(e.Index).Text + " " + GetDesc_39())
    End Sub

    Private Sub saS201_SwitchClick(sender As Object, e As SwitchEventArgs) Handles saS201.SwitchClick
        Dim commandBase As String
        Dim row As Integer = 2

        If e.WhichSwitch = SwitchEventArgs.SwitchLocations.Top Then
            row = 1
            If saS201.Switches(e.Index).TopSwitchState = ctl2W2Row.SwitchStates.Open Then
                saS201.Switches(e.Index).TopSwitchText = "Open"
                commandBase = "Open"
            Else
                saS201.Switches(e.Index).TopSwitchText = "Close"
                commandBase = "Close"
            End If
        Else
            If saS201.Switches(e.Index).BottomSwitchState = ctl2W2Row.SwitchStates.Open Then
                saS201.Switches(e.Index).BottomSwitchText = "Open"
                commandBase = "Open"
           Else
                saS201.Switches(e.Index).BottomSwitchText = "Close"
                commandBase = "Close"
            End If
        End If

        Desc_38.Addr = 3
        Desc_38.Inter = 0
        Desc_38.mux = ((e.Index \ 8) * 2) + (row - 1) + 4
        Desc_38.Chan = e.Index Mod 8

        WriteInstrument(commandBase & " " & GetDesc_38())
    End Sub

    Private Sub saS202_SwitchClick(sender As Object, e As SwitchEventArgs) Handles saS202.SwitchClick
        Dim commandBase As String
        Dim row As Integer = 2

        If e.WhichSwitch = SwitchEventArgs.SwitchLocations.Top Then
            row = 1
            If saS202.Switches(e.Index).TopSwitchState = ctl2W2Row.SwitchStates.Open Then
                saS202.Switches(e.Index).TopSwitchText = "Open"
                commandBase = "Open"
            Else
                saS202.Switches(e.Index).TopSwitchText = "Close"
                commandBase = "Close"
            End If
        Else
            If saS202.Switches(e.Index).BottomSwitchState = ctl2W2Row.SwitchStates.Open Then
                saS202.Switches(e.Index).BottomSwitchText = "Open"
                commandBase = "Open"
           Else
                saS202.Switches(e.Index).BottomSwitchText = "Close"
                commandBase = "Close"
            End If
        End If

        Desc_38.Addr = 3
        Desc_38.Inter = 0
        Desc_38.mux = ((e.Index \ 8) * 2) + (row - 1) + 4 + 6
        Desc_38.Chan = e.Index Mod 8

        WriteInstrument(commandBase & " " & GetDesc_38())
    End Sub

    Private Sub saS301_SwitchClick(sender As Object, e As SwitchEventArgs) Handles saS301.SwitchClick
        Dim Chan As Short
        Dim subChan As Short

        If saS301.Switches(e.Index).SwitchState = SPST.SwitchStates.Open Then
            saS301.Switches(e.Index).Text = "Open"
        Else
            saS301.Switches(e.Index).Text = "Close"
        End If

        Select Case saS301.Switches(e.Index).Column
            Case 0
                Chan = saS301.Switches(e.Index).Row
                subChan = 0
            Case 1
                Chan = saS301.Switches(e.Index).Row + 16
                subChan = 0
            Case 2
                Chan = saS301.Switches(e.Index).Row + 32
                subChan = 0
            Case 3
                Chan = saS301.Switches(e.Index).Row + 48
                subChan = 48
            Case 4
                Chan = saS301.Switches(e.Index).Row + 64
                subChan = 48
            Case 5
                Chan = saS301.Switches(e.Index).Row + 72
                subChan = 40
        End Select

        Desc_39.Addr = 1 + (Chan \ 48)
        Desc_39.Type = 1
        Desc_39.mSelect = 0
        Desc_39.Chan = Chan - subChan 'mh subChan is used to correct the indexing of rows

        WriteInstrument(saS301.Switches(e.Index).Text & " " & GetDesc_39())
    End Sub

    Private Sub saS401_SwitchClick(ByVal sender As Object, ByVal e As SwitchEventArgs) Handles saS401.SwitchClick
        If saS401.Switches(e.Index).SwitchState = ctl1W1Row.SwitchStates.Open Then
            saS401.Switches(e.Index).Text = "Open"
        Else
            saS401.Switches(e.Index).Text = "Close"
        End If
        saS400_ClickEvent(0, e.Index, saS401.Switches(e.Index).Text)
    End Sub

    Private Sub saS402_SwitchClick(ByVal sender As Object, ByVal e As SwitchEventArgs) Handles saS402.SwitchClick
        If saS402.Switches(e.Index).SwitchState = ctl1W1Row.SwitchStates.Open Then
            saS402.Switches(e.Index).Text = "Open"
        Else
            saS402.Switches(e.Index).Text = "Close"
        End If
        saS400_ClickEvent(1, e.Index, saS402.Switches(e.Index).Text)
    End Sub

    Private Sub saS403_SwitchClick(ByVal sender As Object, ByVal e As SwitchEventArgs) Handles saS403.SwitchClick
        If saS403.Switches(e.Index).SwitchState = ctl1W1Row.SwitchStates.Open Then
            saS403.Switches(e.Index).Text = "Open"
        Else
            saS403.Switches(e.Index).Text = "Close"
        End If
        saS400_ClickEvent(2, e.Index, saS403.Switches(e.Index).Text)
    End Sub

    Private Sub saS404_SwitchClick(ByVal sender As Object, ByVal e As SwitchEventArgs) Handles saS404.SwitchClick
        If saS404.Switches(e.Index).SwitchState = ctl1W1Row.SwitchStates.Open Then
            saS404.Switches(e.Index).Text = "Open"
        Else
            saS404.Switches(e.Index).Text = "Close"
        End If
        saS400_ClickEvent(3, e.Index, saS404.Switches(e.Index).Text)
    End Sub

    Private Sub saS405_SwitchClick(ByVal sender As Object, ByVal e As SwitchEventArgs) Handles saS405.SwitchClick
        If saS405.Switches(e.Index).SwitchState = ctl1W1Row.SwitchStates.Open Then
            saS405.Switches(e.Index).Text = "Open"
        Else
            saS405.Switches(e.Index).Text = "Close"
        End If
        saS400_ClickEvent(4, e.Index, saS405.Switches(e.Index).Text)
    End Sub

    Private Sub saS406_SwitchClick(ByVal sender As Object, ByVal e As SwitchEventArgs) Handles saS406.SwitchClick
        If saS406.Switches(e.Index).SwitchState = ctl1W1Row.SwitchStates.Open Then
            saS406.Switches(e.Index).Text = "Open"
        Else
            saS406.Switches(e.Index).Text = "Close"
        End If
        saS400_ClickEvent(5, e.Index, saS406.Switches(e.Index).Text)
    End Sub

    Private Sub saS400_ClickEvent(ByVal Index As Integer, ByVal Col As Integer, ByVal Command As String)
        Desc_39.Addr = 1 + (Index \ 3)
        Desc_39.Type = 3
        Desc_39.mSelect = Index Mod 3
        Desc_39.Chan = Col

        WriteInstrument(Command & " " & GetDesc_39())
    End Sub

    Private Sub saS501_SwitchClick(sender As Object, e As SwitchEventArgs) Handles saS501.SwitchClick
        Dim command As String = "Close"

        If e.WhichSwitch = SwitchEventArgs.SwitchLocations.Top Then
            If saS501.Switches(e.Index).TopSwitchState = ctl1W2Row.SwitchStates.Open Then
                saS501.Switches(e.Index).TopSwitchText = "Open"
                command = "Open"
            Else
                saS501.Switches(e.Index).TopSwitchText = "Close"
            End If
            saS500_ClickEvent(0, e.Index, 1, command)
        Else
            If saS501.Switches(e.Index).BottomSwitchState = ctl1W2Row.SwitchStates.Open Then
                saS501.Switches(e.Index).BottomSwitchText = "Open"
                command = "Open"
            Else
                saS501.Switches(e.Index).BottomSwitchText = "Close"
            End If
            saS500_ClickEvent(0, e.Index, 2, command)
        End If
    End Sub

    Private Sub saS502_SwitchClick(sender As Object, e As SwitchEventArgs) Handles saS502.SwitchClick
        Dim command As String = "Close"

        If e.WhichSwitch = SwitchEventArgs.SwitchLocations.Top Then
            If saS502.Switches(e.Index).TopSwitchState = ctl1W2Row.SwitchStates.Open Then
                saS502.Switches(e.Index).TopSwitchText = "Open"
                command = "Open"
            Else
                saS502.Switches(e.Index).TopSwitchText = "Close"
            End If
            saS500_ClickEvent(1, e.Index, 1, command)
        Else
            If saS502.Switches(e.Index).BottomSwitchState = ctl1W2Row.SwitchStates.Open Then
                saS502.Switches(e.Index).BottomSwitchText = "Open"
                command = "Open"
            Else
                saS502.Switches(e.Index).BottomSwitchText = "Close"
            End If
            saS500_ClickEvent(1, e.Index, 2, command)
        End If
    End Sub

    Private Sub saS503_SwitchClick(sender As Object, e As SwitchEventArgs) Handles saS503.SwitchClick
        Dim command As String = "Close"

        If e.WhichSwitch = SwitchEventArgs.SwitchLocations.Top Then
            If saS503.Switches(e.Index).TopSwitchState = ctl1W2Row.SwitchStates.Open Then
                saS503.Switches(e.Index).TopSwitchText = "Open"
                command = "Open"
            Else
                saS503.Switches(e.Index).TopSwitchText = "Close"
            End If
            saS500_ClickEvent(2, e.Index, 1, command)
        Else
            If saS503.Switches(e.Index).BottomSwitchState = ctl1W2Row.SwitchStates.Open Then
                saS503.Switches(e.Index).BottomSwitchText = "Open"
                command = "Open"
            Else
                saS503.Switches(e.Index).BottomSwitchText = "Close"
            End If
            saS500_ClickEvent(2, e.Index, 2, command)
        End If
    End Sub

    Private Sub saS504_SwitchClick(sender As Object, e As SwitchEventArgs) Handles saS504.SwitchClick
        Dim command As String = "Close"

        If e.WhichSwitch = SwitchEventArgs.SwitchLocations.Top Then
            If saS504.Switches(e.Index).TopSwitchState = ctl1W2Row.SwitchStates.Open Then
                saS504.Switches(e.Index).TopSwitchText = "Open"
                command = "Open"
            Else
                saS504.Switches(e.Index).TopSwitchText = "Close"
            End If
            saS500_ClickEvent(3, e.Index, 1, command)
        Else
            If saS504.Switches(e.Index).BottomSwitchState = ctl1W2Row.SwitchStates.Open Then
                saS504.Switches(e.Index).BottomSwitchText = "Open"
                command = "Open"
            Else
                saS504.Switches(e.Index).BottomSwitchText = "Close"
            End If
            saS500_ClickEvent(3, e.Index, 2, command)
        End If
    End Sub

    Private Sub saS505_SwitchClick(sender As Object, e As SwitchEventArgs) Handles saS505.SwitchClick
        Dim command As String = "Close"

        If e.WhichSwitch = SwitchEventArgs.SwitchLocations.Top Then
            If saS505.Switches(e.Index).TopSwitchState = ctl1W2Row.SwitchStates.Open Then
                saS505.Switches(e.Index).TopSwitchText = "Open"
                command = "Open"
            Else
                saS505.Switches(e.Index).TopSwitchText = "Close"
            End If
            saS500_ClickEvent(4, e.Index, 1, command)
        Else
            If saS505.Switches(e.Index).BottomSwitchState = ctl1W2Row.SwitchStates.Open Then
                saS505.Switches(e.Index).BottomSwitchText = "Open"
                command = "Open"
            Else
                saS505.Switches(e.Index).BottomSwitchText = "Close"
            End If
            saS500_ClickEvent(4, e.Index, 2, command)
        End If
    End Sub

    Private Sub saS506_SwitchClick(sender As Object, e As SwitchEventArgs) Handles saS506.SwitchClick
        Dim command As String = "Close"

        If e.WhichSwitch = SwitchEventArgs.SwitchLocations.Top Then
            If saS506.Switches(e.Index).TopSwitchState = ctl1W2Row.SwitchStates.Open Then
                saS506.Switches(e.Index).TopSwitchText = "Open"
                command = "Open"
            Else
                saS506.Switches(e.Index).TopSwitchText = "Close"
            End If
            saS500_ClickEvent(5, e.Index, 1, command)
        Else
            If saS506.Switches(e.Index).BottomSwitchState = ctl1W2Row.SwitchStates.Open Then
                saS506.Switches(e.Index).BottomSwitchText = "Open"
                command = "Open"
            Else
                saS506.Switches(e.Index).BottomSwitchText = "Close"
            End If
            saS500_ClickEvent(5, e.Index, 2, command)
        End If
    End Sub

    Private Sub saS507_SwitchClick(sender As Object, e As SwitchEventArgs) Handles saS507.SwitchClick
        Dim command As String = "Close"

        If e.WhichSwitch = SwitchEventArgs.SwitchLocations.Top Then
            If saS507.Switches(e.Index).TopSwitchState = ctl1W2Row.SwitchStates.Open Then
                saS507.Switches(e.Index).TopSwitchText = "Open"
                command = "Open"
            Else
                saS507.Switches(e.Index).TopSwitchText = "Close"
            End If
            saS500_ClickEvent(6, e.Index, 1, command)
        Else
            If saS507.Switches(e.Index).BottomSwitchState = ctl1W2Row.SwitchStates.Open Then
                saS507.Switches(e.Index).BottomSwitchText = "Open"
                command = "Open"
            Else
                saS507.Switches(e.Index).BottomSwitchText = "Close"
            End If
            saS500_ClickEvent(6, e.Index, 2, command)
        End If
    End Sub

    Private Sub saS508_SwitchClick(sender As Object, e As SwitchEventArgs) Handles saS508.SwitchClick
        Dim command As String = "Close"

        If e.WhichSwitch = SwitchEventArgs.SwitchLocations.Top Then
            If saS508.Switches(e.Index).TopSwitchState = ctl1W2Row.SwitchStates.Open Then
                saS508.Switches(e.Index).TopSwitchText = "Open"
                command = "Open"
            Else
                saS508.Switches(e.Index).TopSwitchText = "Close"
            End If
            saS500_ClickEvent(7, e.Index, 1, command)
        Else
            If saS508.Switches(e.Index).BottomSwitchState = ctl1W2Row.SwitchStates.Open Then
                saS508.Switches(e.Index).BottomSwitchText = "Open"
                command = "Open"
            Else
                saS508.Switches(e.Index).BottomSwitchText = "Close"
            End If
            saS500_ClickEvent(7, e.Index, 2, command)
        End If
    End Sub

    Private Sub saS509_SwitchClick(sender As Object, e As SwitchEventArgs) Handles saS509.SwitchClick
        Dim command As String = "Close"

        If e.WhichSwitch = SwitchEventArgs.SwitchLocations.Top Then
            If saS509.Switches(e.Index).TopSwitchState = ctl1W2Row.SwitchStates.Open Then
                saS509.Switches(e.Index).TopSwitchText = "Open"
                command = "Open"
            Else
                saS509.Switches(e.Index).TopSwitchText = "Close"
            End If
            saS500_ClickEvent(8, e.Index, 1, command)
        Else
            If saS509.Switches(e.Index).BottomSwitchState = ctl1W2Row.SwitchStates.Open Then
                saS509.Switches(e.Index).BottomSwitchText = "Open"
                command = "Open"
            Else
                saS509.Switches(e.Index).BottomSwitchText = "Close"
            End If
            saS500_ClickEvent(8, e.Index, 2, command)
        End If
    End Sub

    Private Sub saS510_SwitchClick(sender As Object, e As SwitchEventArgs) Handles saS510.SwitchClick
        Dim command As String = "Close"

        If e.WhichSwitch = SwitchEventArgs.SwitchLocations.Top Then
            If saS510.Switches(e.Index).TopSwitchState = ctl1W2Row.SwitchStates.Open Then
                saS510.Switches(e.Index).TopSwitchText = "Open"
                command = "Open"
            Else
                saS510.Switches(e.Index).TopSwitchText = "Close"
            End If
            saS500_ClickEvent(9, e.Index, 1, command)
        Else
            If saS510.Switches(e.Index).BottomSwitchState = ctl1W2Row.SwitchStates.Open Then
                saS510.Switches(e.Index).BottomSwitchText = "Open"
                command = "Open"
            Else
                saS510.Switches(e.Index).BottomSwitchText = "Close"
            End If
            saS500_ClickEvent(9, e.Index, 2, command)
        End If
    End Sub

    Private Sub saS500_ClickEvent(ByVal Index As Integer, ByVal Col As Integer, ByVal row As Integer, ByVal command As String)
        Desc_39.Addr = 1 + (Index \ 5)
        Desc_39.Type = 4
        Desc_39.mSelect = Index Mod 5
        Desc_39.Chan = Col + (10 * (row - 1))

        WriteInstrument(command & " " & GetDesc_39())
    End Sub

    Private Sub saS601_SwitchClick(sender As Object, e As SwitchEventArgs) Handles saS601.SwitchClick
        If saS601.Switches(e.Index).SwitchState = ctl1W1Row.SwitchStates.Open Then
            saS601.Switches(e.Index).Text = "Open"
            saS600_ClickEvent(0, e.Index, "Open")
        Else
            saS601.Switches(e.Index).Text = "Close"
            saS600_ClickEvent(0, e.Index, "Close")
        End If
    End Sub

    Private Sub saS602_SwitchClick(sender As Object, e As SwitchEventArgs) Handles saS602.SwitchClick
        If saS602.Switches(e.Index).SwitchState = ctl1W1Row.SwitchStates.Open Then
            saS602.Switches(e.Index).Text = "Open"
            saS600_ClickEvent(1, e.Index, "Open")
        Else
            saS602.Switches(e.Index).Text = "Close"
            saS600_ClickEvent(1, e.Index, "Close")
        End If
    End Sub

    Private Sub saS603_SwitchClick(sender As Object, e As SwitchEventArgs) Handles saS603.SwitchClick
        If saS603.Switches(e.Index).SwitchState = ctl1W1Row.SwitchStates.Open Then
            saS603.Switches(e.Index).Text = "Open"
            saS600_ClickEvent(2, e.Index, "Open")
        Else
            saS603.Switches(e.Index).Text = "Close"
            saS600_ClickEvent(2, e.Index, "Close")
        End If
    End Sub

    Private Sub saS604_SwitchClick(sender As Object, e As SwitchEventArgs) Handles saS604.SwitchClick
        If saS604.Switches(e.Index).SwitchState = ctl1W1Row.SwitchStates.Open Then
            saS604.Switches(e.Index).Text = "Open"
            saS600_ClickEvent(3, e.Index, "Open")
        Else
            saS604.Switches(e.Index).Text = "Close"
            saS600_ClickEvent(3, e.Index, "Close")
        End If
    End Sub

    Private Sub saS605_SwitchClick(sender As Object, e As SwitchEventArgs) Handles saS605.SwitchClick
        If saS605.Switches(e.Index).SwitchState = ctl1W1Row.SwitchStates.Open Then
            saS605.Switches(e.Index).Text = "Open"
            saS600_ClickEvent(4, e.Index, "Open")
        Else
            saS605.Switches(e.Index).Text = "Close"
            saS600_ClickEvent(4, e.Index, "Close")
        End If
    End Sub

    Private Sub saS606_SwitchClick(sender As Object, e As SwitchEventArgs) Handles saS606.SwitchClick
        If saS606.Switches(e.Index).SwitchState = ctl1W1Row.SwitchStates.Open Then
            saS606.Switches(e.Index).Text = "Open"
            saS600_ClickEvent(5, e.Index, "Open")
        Else
            saS606.Switches(e.Index).Text = "Close"
            saS600_ClickEvent(5, e.Index, "Close")
        End If
    End Sub

    Private Sub saS607_SwitchClick(sender As Object, e As SwitchEventArgs) Handles saS607.SwitchClick
        If saS607.Switches(e.Index).SwitchState = ctl1W1Row.SwitchStates.Open Then
            saS607.Switches(e.Index).Text = "Open"
            saS600_ClickEvent(6, e.Index, "Open")
        Else
            saS607.Switches(e.Index).Text = "Close"
            saS600_ClickEvent(6, e.Index, "Close")
        End If
    End Sub

    Private Sub saS608_SwitchClick(sender As Object, e As SwitchEventArgs) Handles saS608.SwitchClick
        If saS608.Switches(e.Index).SwitchState = ctl1W1Row.SwitchStates.Open Then
            saS608.Switches(e.Index).Text = "Open"
            saS600_ClickEvent(7, e.Index, "Open")
        Else
            saS608.Switches(e.Index).Text = "Close"
            saS600_ClickEvent(7, e.Index, "Close")
        End If
    End Sub

    Private Sub saS609_SwitchClick(sender As Object, e As SwitchEventArgs) Handles saS609.SwitchClick
        If saS609.Switches(e.Index).SwitchState = ctl1W1Row.SwitchStates.Open Then
            saS609.Switches(e.Index).Text = "Open"
            saS600_ClickEvent(8, e.Index, "Open")
        Else
            saS609.Switches(e.Index).Text = "Close"
            saS600_ClickEvent(8, e.Index, "Close")
        End If
    End Sub

    Private Sub saS610_SwitchClick(sender As Object, e As SwitchEventArgs) Handles saS610.SwitchClick
        If saS610.Switches(e.Index).SwitchState = ctl1W1Row.SwitchStates.Open Then
            saS610.Switches(e.Index).Text = "Open"
            saS600_ClickEvent(9, e.Index, "Open")
        Else
            saS610.Switches(e.Index).Text = "Close"
            saS600_ClickEvent(9, e.Index, "Close")
        End If
    End Sub

    Private Sub saS611_SwitchClick(sender As Object, e As SwitchEventArgs) Handles saS611.SwitchClick
        If saS611.Switches(e.Index).SwitchState = ctl1W1Row.SwitchStates.Open Then
            saS611.Switches(e.Index).Text = "Open"
            saS600_ClickEvent(10, e.Index, "Open")
        Else
            saS611.Switches(e.Index).Text = "Close"
            saS600_ClickEvent(10, e.Index, "Close")
        End If
    End Sub

    Private Sub saS612_SwitchClick(sender As Object, e As SwitchEventArgs) Handles saS612.SwitchClick
        If saS612.Switches(e.Index).SwitchState = ctl1W1Row.SwitchStates.Open Then
            saS612.Switches(e.Index).Text = "Open"
            saS600_ClickEvent(11, e.Index, "Open")
        Else
            saS612.Switches(e.Index).Text = "Close"
            saS600_ClickEvent(11, e.Index, "Close")
        End If
    End Sub

    Private Sub saS600_ClickEvent(ByVal Index As Integer, ByVal Col As Short, ByVal command As String)
        Desc_39.Addr = 1 + (Index \ 6)
        Desc_39.Type = 2
        Desc_39.mSelect = Index Mod 6
        Desc_39.Chan = Col

        WriteInstrument(command & " " & GetDesc_39())
    End Sub

    Private Sub saS701_SwitchClick(ByVal sender As Object, ByVal e As SwitchEventArgs) Handles saS701.SwitchClick
        If saS701.Switches(e.Index).SwitchState = ctl2W1Row.SwitchStates.Open Then
            saS701.Switches(e.Index).Text = "Open"
        Else
            saS701.Switches(e.Index).Text = "Close"
        End If

        Desc_38.Addr = 3
        Desc_38.Inter = 0
        Desc_38.mux = (e.Index \ 8) + 1
        Desc_38.Chan = e.Index Mod 8

        WriteInstrument(saS701.Switches(e.Index).Text & " " & GetDesc_38())
    End Sub

    Private Sub saS751_SwitchClick(ByVal sender As Object, ByVal e As SwitchEventArgs) Handles saS751.SwitchClick
        If saS751.Switches(e.Index).SwitchState = ctl2W1Row.SwitchStates.Open Then
            saS751.Switches(e.Index).Text = "Open"
        Else
            saS751.Switches(e.Index).Text = "Close"
        End If

        Desc_38.Addr = 3
        Desc_38.Inter = 0
        Desc_38.mux = 0
        Desc_38.Chan = e.Index Mod 8

        WriteInstrument(saS751.Switches(e.Index).Text & " " & GetDesc_38())
    End Sub

    Private Sub saS801_SwitchClick(sender As Object, e As SwitchEventArgs) Handles saS801.SwitchClick
        saS800_ClickEvent(0, Convert.ToInt16(saS801.SwitchState))
    End Sub

    Private Sub saS802_SwitchClick(sender As Object, e As SwitchEventArgs) Handles saS802.SwitchClick
        saS800_ClickEvent(1, Convert.ToInt16(saS802.SwitchState))
    End Sub

    Private Sub saS803_SwitchClick(sender As Object, e As SwitchEventArgs) Handles saS803.SwitchClick
        saS800_ClickEvent(2, Convert.ToInt16(saS803.SwitchState))
    End Sub

    Private Sub saS804_SwitchClick(sender As Object, e As SwitchEventArgs) Handles saS804.SwitchClick
        saS800_ClickEvent(3, Convert.ToInt16(saS804.SwitchState))
    End Sub

    Private Sub saS800_ClickEvent(ByVal Index As Short, ByVal PinVal As Short)
        'The 1260-58 employs "implicit exclusion".
        'Each Close statement implicitly opens any previous closure.
        'CLOSE 8 connects common to the OPEN position, effectively opening the mux.
        'Therefore, no Open statements need be executed.

        Desc_58.Addr = 4
        Desc_58.mux = Index
        Desc_58.Chan = PinVal
        WriteInstrument("Close " & GetDesc_58())
    End Sub

    Private Sub saS901_SwitchClick(sender As Object, e As SwitchEventArgs) Handles saS901.SwitchClick
        saS900_ClickEvent(0, Convert.ToInt16(saS901.SwitchState))
    End Sub

    Private Sub saS902_SwitchClick(sender As Object, e As SwitchEventArgs) Handles saS902.SwitchClick
        saS900_ClickEvent(1, Convert.ToInt16(saS902.SwitchState))
    End Sub

    Private Sub saS903_SwitchClick(sender As Object, e As SwitchEventArgs) Handles saS903.SwitchClick
        saS900_ClickEvent(2, Convert.ToInt16(saS903.SwitchState))
    End Sub

    Private Sub saS904_SwitchClick(sender As Object, e As SwitchEventArgs) Handles saS904.SwitchClick
        saS900_ClickEvent(3, Convert.ToInt16(saS904.SwitchState))
    End Sub

    Private Sub saS905_SwitchClick(sender As Object, e As SwitchEventArgs) Handles saS905.SwitchClick
        saS900_ClickEvent(4, Convert.ToInt16(saS905.SwitchState))
    End Sub

    Private Sub saS906_SwitchClick(sender As Object, e As SwitchEventArgs) Handles saS906.SwitchClick
        saS900_ClickEvent(5, Convert.ToInt16(saS906.SwitchState))
    End Sub

    Private Sub saS900_ClickEvent(ByVal Index As Short, ByVal PinVal As Short)
        'The 1260-66 employs "implicit exclusion".
        'Each Close statement implicitly opens any previous closure.

        Desc_66.Addr = 5
        Desc_66.mux = Index
        Desc_66.Chan = PinVal

        If PinVal = 6 Then
            WriteInstrument("Open 5." & Index & "0-" & Index & "5")
        Else
            WriteInstrument("Close " & GetDesc_66())
        End If
    End Sub

    '#Const defHas_imgLogo = True
#If defHas_imgLogo Then
    Private Sub imgLogo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles imgLogo.Click
        Me.WindowState = FormWindowState.Normal
'The appropriate value for these in pixels instead of twips will have to be calculated once the need for this function is determined
'        Me.Width = 6000
'        Me.Height = 6000
    End Sub


    Private Sub imgLogo_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles imgLogo.MouseDown
        Dim Button As Short = e.Button \ &H100000
        Dim Shift As Short = Control.ModifierKeys \ &H10000

        Static Prompt As String = ""
        Dim ReturnString As String = ""
        Dim S As String

        If Button = 1 And Shift = 2 Then ' If Ctrl-LEFT button
            Prompt = InputBox("Enter an instrument command:", , Prompt)
            If InStr(UCase(Prompt), "DEBUG ON") Then
                LiveMode = False
            ElseIf InStr(UCase(Prompt), "DEBUG OFF") Then
                LiveMode = True
            Else
                WriteInstrument(Prompt)
            End If
            Select Case UCase(Strings.Left(Prompt, 2))
                Case "TE", "YE"
                    ReturnString = ReadSwitch()
                    If ReturnString <> "" Then
                        MsgBox(ReturnString, MsgBoxStyle.Information, "Read from instrument:")
                    End If
                Case "PD", "PS"
                    Do
                        S = ReadSwitch()
                        ReturnString &= S & vbCrLf
                    Loop Until InStr(S, "END") Or S = ""
                    MsgBox(ReturnString, MsgBoxStyle.Information, "Read from instrument:")
            End Select
        ElseIf Button = 2 Then             ' If Right button
            '        ReadBuffer = ""
            '        ErrorStatus& = Sw_Error_Query(IHnd&, ErrorFlag, ReadBuffer)
            '        MsgBox DecodeErrorMessage(ReadBuffer), vbInformation, "Switch Error Message"
        End If
    End Sub
#End If

    Private Sub Panel_Conifg_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Panel_Conifg.Leave
        PanelConifgPushed = False
    End Sub

    Private Sub Panel_Conifg_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles Panel_Conifg.Enter
        PanelConifgPushed = True
    End Sub

    Private Sub frmRac1260_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If LiveMode Then
            ErrorStatus = atxml_Close()
        End If
    End Sub

    Private Sub tabOptions_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tabOptions.SelectedIndexChanged
        tabOptions.SelectedTab.Refresh()
    End Sub
End Class