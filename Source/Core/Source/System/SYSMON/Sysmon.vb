
Imports System
Imports System.Windows.Forms
Imports System.Text
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Imports Scripting
Imports NationalInstruments.VisaNS

Public Class frmSysMon
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        fraChasTemp(2) = fraChasTemp_2
        fraChasTemp(1) = fraChasTemp_1
        fraChasTemp(0) = fraChasTemp_0
        fraChasTemp(4) = fraChasTemp_4
        fraChasTemp(5) = fraChasTemp_5
        fraChasTemp(3) = fraChasTemp_3
        fraChassisTest(2) = fraChassisTest_2
        fraChassisTest(1) = fraChassisTest_1
        fraFanSpeed(1) = fraFanSpeed_1
        fraFanSpeed(0) = fraFanSpeed_0
        fraFanSpeed(2) = fraFanSpeed_2
        fraHeatingUnits(1) = fraHeatingUnits_1
        fraHeatingUnits(0) = fraHeatingUnits_0
        fraAirflowSensors(1) = fraAirflowSensors_1
        fraAirflowSensors(0) = fraAirflowSensors_0
        fraPDU(5) = fraPDU_5
        fraPDU(1) = fraPDU_1
        fraPDU(2) = fraPDU_2
        fraPDU(4) = fraPDU_4
        fraPDU(3) = fraPDU_3
        fraPDU(0) = fraPDU_0
        fraMaintenance(0) = fraMaintenance_0
        fraMaintenance(7) = fraMaintenance_7
        fraMaintenance(4) = fraMaintenance_4
        fraMaintenance(6) = fraMaintenance_6
        fraMaintenance(5) = fraMaintenance_5
        fraMaintenance(1) = fraMaintenance_1
        fraMaintenance(2) = fraMaintenance_2
        fraMaintenance(3) = fraMaintenance_3
        optFpuState(1) = optFpuState_1
        optFpuState(0) = optFpuState_0
        cwsSlotRise(0) = cwsSlotRise_0
        cwsSlotRise(1) = cwsSlotRise_1
        cwsSlotRise(2) = cwsSlotRise_2
        cwsSlotRise(3) = cwsSlotRise_3
        cwsSlotRise(4) = cwsSlotRise_4
        cwsSlotRise(5) = cwsSlotRise_5
        cwsSlotRise(6) = cwsSlotRise_6
        cwsSlotRise(7) = cwsSlotRise_7
        cwsSlotRise(8) = cwsSlotRise_8
        cwsSlotRise(9) = cwsSlotRise_9
        cwsSlotRise(10) = cwsSlotRise_10
        cwsSlotRise(11) = cwsSlotRise_11
        cwsSlotRise(12) = cwsSlotRise_12
        cwsSlotRise(13) = cwsSlotRise_13
        cwsSlotRise(14) = cwsSlotRise_14
        cwsSlotRise(15) = cwsSlotRise_15
        cwsSlotRise(16) = cwsSlotRise_16
        cwsSlotRise(17) = cwsSlotRise_17
        cwsSlotRise(18) = cwsSlotRise_18
        cwsSlotRise(19) = cwsSlotRise_19
        cwsSlotRise(20) = cwsSlotRise_20
        cwsSlotRise(21) = cwsSlotRise_21
        cwsSlotRise(22) = cwsSlotRise_22
        cwsSlotRise(23) = cwsSlotRise_23
        cwsSlotRise(24) = cwsSlotRise_24
        cwsSlotRise(25) = cwsSlotRise_25
        cwsSlotRiseActual(0) = cwsSlotRiseActual_0
        cwsSlotRiseActual(1) = cwsSlotRiseActual_1
        cwsSlotRiseActual(2) = cwsSlotRiseActual_2
        cwsSlotRiseActual(3) = cwsSlotRiseActual_3
        cwsSlotRiseActual(4) = cwsSlotRiseActual_4
        cwsSlotRiseActual(5) = cwsSlotRiseActual_5
        cwsSlotRiseActual(6) = cwsSlotRiseActual_6
        cwsSlotRiseActual(7) = cwsSlotRiseActual_7
        cwsSlotRiseActual(8) = cwsSlotRiseActual_8
        cwsSlotRiseActual(9) = cwsSlotRiseActual_9
        cwsSlotRiseActual(10) = cwsSlotRiseActual_10
        cwsSlotRiseActual(11) = cwsSlotRiseActual_11
        cwsSlotRiseActual(12) = cwsSlotRiseActual_12
        cwsSlotRiseActual(13) = cwsSlotRiseActual_13
        cwsSlotRiseActual(14) = cwsSlotRiseActual_14
        cwsSlotRiseActual(15) = cwsSlotRiseActual_15
        cwsSlotRiseActual(16) = cwsSlotRiseActual_16
        cwsSlotRiseActual(17) = cwsSlotRiseActual_17
        cwsSlotRiseActual(18) = cwsSlotRiseActual_18
        cwsSlotRiseActual(19) = cwsSlotRiseActual_19
        cwsSlotRiseActual(20) = cwsSlotRiseActual_20
        cwsSlotRiseActual(21) = cwsSlotRiseActual_21
        cwsSlotRiseActual(22) = cwsSlotRiseActual_22
        cwsSlotRiseActual(23) = cwsSlotRiseActual_23
        cwsSlotRiseActual(24) = cwsSlotRiseActual_24
        cwsSlotRiseActual(25) = cwsSlotRiseActual_25

        cwbChasSelfTestIndicator(1) = cwbChasSelfTestIndicator_1
        cwbChasSelfTestIndicator(2) = cwbChasSelfTestIndicator_2

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
    Friend fraChasTemp(6) As System.Windows.Forms.GroupBox
    Friend WithEvents picIconGraphic As Microsoft.VisualBasic.Compatibility.VB6.PictureBoxArray
    Friend WithEvents lblIntakeValue As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    Friend WithEvents lblUnit As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    Friend WithEvents cmdResetThresholds As Microsoft.VisualBasic.Compatibility.VB6.ButtonArray
    Friend WithEvents linRise As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    Friend WithEvents lblChassTemp As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    Friend WithEvents lblExhaustValue As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    Friend WithEvents cmdSaveThresholds As Microsoft.VisualBasic.Compatibility.VB6.ButtonArray
    Friend fraChassisTest(3) As System.Windows.Forms.GroupBox
    Friend cwbChasSelfTestIndicator(3) As NationalInstruments.UI.WindowsForms.Led
    Friend WithEvents lblCst As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    Friend fraFanSpeed(3) As System.Windows.Forms.GroupBox
    Friend WithEvents txtFanSpeedValue As Microsoft.VisualBasic.Compatibility.VB6.TextBoxArray
    Friend panFanSpeed(2) As System.Windows.Forms.Panel
    Friend fraHeatingUnits(2) As System.Windows.Forms.GroupBox
    Friend WithEvents lblHu As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    Friend fraAirflowSensors(2) As System.Windows.Forms.GroupBox
    Friend WithEvents lblPca As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    Friend WithEvents lblLevel As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    Friend WithEvents Label2 As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    Friend WithEvents lblCp As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    Friend WithEvents lblBackVolt As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    Friend WithEvents lblBackCurr As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    Friend WithEvents lblUutVolt As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    Friend WithEvents lblUut As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    Friend WithEvents lblUutCur As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    Friend fraPDU(6) As System.Windows.Forms.GroupBox
    Friend fraMaintenance(8) As System.Windows.Forms.GroupBox
    Friend WithEvents lblSysPower As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    Friend WithEvents lblInputPower As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    Friend cwbHamPhase(10) As System.Windows.Forms.Button
    Friend WithEvents lblInPower As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    Friend WithEvents lblmode As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    Friend Panel(5) As System.Windows.Forms.Panel
    Friend WithEvents TextBox As Microsoft.VisualBasic.Compatibility.VB6.TextBoxArray
    Friend SpinButton(5) As System.Windows.Forms.NumericUpDown
    Friend chkHeater(6) As System.Windows.Forms.CheckBox
    Friend chkEnableChassisOption(10) As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    Friend WithEvents cmdCalChass As Microsoft.VisualBasic.Compatibility.VB6.ButtonArray
    Friend chkPpuNoFault(11) As System.Windows.Forms.CheckBox
    Friend optFpuState(2) As System.Windows.Forms.RadioButton
    Friend WithEvents cmdResetChassis As Microsoft.VisualBasic.Compatibility.VB6.ButtonArray
    Friend WithEvents cmdResetPdu As Microsoft.VisualBasic.Compatibility.VB6.ButtonArray
    Friend WithEvents ETITimer As System.Windows.Forms.Timer
    Friend WithEvents tmrUpdateStop As System.Windows.Forms.Timer
    Friend WithEvents cmdAbout As System.Windows.Forms.Button
    Friend WithEvents cmdShutDown As System.Windows.Forms.Button
    Friend WithEvents tmrDataPoll As System.Windows.Forms.Timer
    Friend WithEvents cmdQuitSysMon As System.Windows.Forms.Button
    Friend WithEvents tabSysmon As System.Windows.Forms.TabControl
    Friend WithEvents tabSysmon_Page1 As System.Windows.Forms.TabPage
    Friend WithEvents fraChasTemp_2 As System.Windows.Forms.GroupBox
    Friend WithEvents picIconGraphic_1 As System.Windows.Forms.PictureBox
    Friend WithEvents lblIntakeValue_0 As System.Windows.Forms.Label
    Friend WithEvents lblUnit_7 As System.Windows.Forms.Label
    Friend WithEvents lblUnit_8 As System.Windows.Forms.Label
    Friend WithEvents cmdResetThresholds_1 As System.Windows.Forms.Button
    Friend WithEvents fraChasTemp_1 As System.Windows.Forms.GroupBox
    Friend WithEvents linRise_11 As System.Windows.Forms.Label
    Friend WithEvents linRise_0 As System.Windows.Forms.Label
    Friend WithEvents linRise_4 As System.Windows.Forms.Label
    Friend WithEvents lblChassTemp_25 As System.Windows.Forms.Label
    Friend WithEvents lblChassTemp_24 As System.Windows.Forms.Label
    Friend WithEvents lblChassTemp_23 As System.Windows.Forms.Label
    Friend WithEvents lblChassTemp_22 As System.Windows.Forms.Label
    Friend WithEvents lblChassTemp_21 As System.Windows.Forms.Label
    Friend WithEvents lblChassTemp_20 As System.Windows.Forms.Label
    Friend WithEvents lblChassTemp_19 As System.Windows.Forms.Label
    Friend WithEvents lblChassTemp_18 As System.Windows.Forms.Label
    Friend WithEvents lblChassTemp_17 As System.Windows.Forms.Label
    Friend WithEvents lblChassTemp_16 As System.Windows.Forms.Label
    Friend WithEvents lblChassTemp_15 As System.Windows.Forms.Label
    Friend WithEvents lblChassTemp_14 As System.Windows.Forms.Label
    Friend WithEvents lblChassTemp_13 As System.Windows.Forms.Label
    Friend WithEvents linRise_1 As System.Windows.Forms.Label
    Friend WithEvents linRise_2 As System.Windows.Forms.Label
    Friend WithEvents linRise_3 As System.Windows.Forms.Label
    Friend WithEvents linRise_10 As System.Windows.Forms.Label
    Friend WithEvents fraChasTemp_0 As System.Windows.Forms.GroupBox
    Friend WithEvents lblExhaustValue_12 As System.Windows.Forms.Label
    Friend WithEvents lblExhaustValue_11 As System.Windows.Forms.Label
    Friend WithEvents lblExhaustValue_10 As System.Windows.Forms.Label
    Friend WithEvents lblExhaustValue_9 As System.Windows.Forms.Label
    Friend WithEvents lblExhaustValue_8 As System.Windows.Forms.Label
    Friend WithEvents lblExhaustValue_7 As System.Windows.Forms.Label
    Friend WithEvents lblExhaustValue_6 As System.Windows.Forms.Label
    Friend WithEvents lblExhaustValue_5 As System.Windows.Forms.Label
    Friend WithEvents lblExhaustValue_4 As System.Windows.Forms.Label
    Friend WithEvents lblExhaustValue_3 As System.Windows.Forms.Label
    Friend WithEvents lblExhaustValue_2 As System.Windows.Forms.Label
    Friend WithEvents lblExhaustValue_1 As System.Windows.Forms.Label
    Friend WithEvents lblExhaustValue_0 As System.Windows.Forms.Label
    Friend WithEvents lblChassTemp_0 As System.Windows.Forms.Label
    Friend WithEvents lblChassTemp_1 As System.Windows.Forms.Label
    Friend WithEvents lblChassTemp_2 As System.Windows.Forms.Label
    Friend WithEvents lblChassTemp_3 As System.Windows.Forms.Label
    Friend WithEvents lblChassTemp_4 As System.Windows.Forms.Label
    Friend WithEvents lblChassTemp_5 As System.Windows.Forms.Label
    Friend WithEvents lblChassTemp_6 As System.Windows.Forms.Label
    Friend WithEvents lblChassTemp_7 As System.Windows.Forms.Label
    Friend WithEvents lblChassTemp_8 As System.Windows.Forms.Label
    Friend WithEvents lblChassTemp_9 As System.Windows.Forms.Label
    Friend WithEvents lblChassTemp_10 As System.Windows.Forms.Label
    Friend WithEvents lblChassTemp_11 As System.Windows.Forms.Label
    Friend WithEvents lblChassTemp_12 As System.Windows.Forms.Label
    Friend WithEvents cmdSaveThresholds_1 As System.Windows.Forms.Button
    Friend WithEvents tabSysmon_Page2 As System.Windows.Forms.TabPage
    Friend WithEvents fraChasTemp_4 As System.Windows.Forms.GroupBox
    Friend WithEvents linRise_9 As System.Windows.Forms.Label
    Friend WithEvents linRise_8 As System.Windows.Forms.Label
    Friend WithEvents linRise_7 As System.Windows.Forms.Label
    Friend WithEvents linRise_6 As System.Windows.Forms.Label
    Friend WithEvents linRise_5 As System.Windows.Forms.Label
    Friend WithEvents lblChassTemp_51 As System.Windows.Forms.Label
    Friend WithEvents lblChassTemp_50 As System.Windows.Forms.Label
    Friend WithEvents lblChassTemp_49 As System.Windows.Forms.Label
    Friend WithEvents lblChassTemp_48 As System.Windows.Forms.Label
    Friend WithEvents lblChassTemp_47 As System.Windows.Forms.Label
    Friend WithEvents lblChassTemp_46 As System.Windows.Forms.Label
    Friend WithEvents lblChassTemp_45 As System.Windows.Forms.Label
    Friend WithEvents lblChassTemp_44 As System.Windows.Forms.Label
    Friend WithEvents lblChassTemp_43 As System.Windows.Forms.Label
    Friend WithEvents lblChassTemp_42 As System.Windows.Forms.Label
    Friend WithEvents lblChassTemp_41 As System.Windows.Forms.Label
    Friend WithEvents lblChassTemp_40 As System.Windows.Forms.Label
    Friend WithEvents lblChassTemp_39 As System.Windows.Forms.Label
    Friend WithEvents linRise_12 As System.Windows.Forms.Label
    Friend WithEvents linRise_13 As System.Windows.Forms.Label
    Friend WithEvents fraChasTemp_5 As System.Windows.Forms.GroupBox
    Friend WithEvents picIconGraphic_4 As System.Windows.Forms.PictureBox
    Friend WithEvents lblUnit_0 As System.Windows.Forms.Label
    Friend WithEvents lblUnit_2 As System.Windows.Forms.Label
    Friend WithEvents lblIntakeValue_1 As System.Windows.Forms.Label
    Friend WithEvents fraChasTemp_3 As System.Windows.Forms.GroupBox
    Friend WithEvents lblChassTemp_38 As System.Windows.Forms.Label
    Friend WithEvents lblChassTemp_37 As System.Windows.Forms.Label
    Friend WithEvents lblChassTemp_36 As System.Windows.Forms.Label
    Friend WithEvents lblChassTemp_35 As System.Windows.Forms.Label
    Friend WithEvents lblChassTemp_34 As System.Windows.Forms.Label
    Friend WithEvents lblChassTemp_33 As System.Windows.Forms.Label
    Friend WithEvents lblChassTemp_32 As System.Windows.Forms.Label
    Friend WithEvents lblChassTemp_31 As System.Windows.Forms.Label
    Friend WithEvents lblChassTemp_30 As System.Windows.Forms.Label
    Friend WithEvents lblChassTemp_29 As System.Windows.Forms.Label
    Friend WithEvents lblChassTemp_28 As System.Windows.Forms.Label
    Friend WithEvents lblChassTemp_27 As System.Windows.Forms.Label
    Friend WithEvents lblChassTemp_26 As System.Windows.Forms.Label
    Friend WithEvents lblExhaustValue_13 As System.Windows.Forms.Label
    Friend WithEvents lblExhaustValue_14 As System.Windows.Forms.Label
    Friend WithEvents lblExhaustValue_15 As System.Windows.Forms.Label
    Friend WithEvents lblExhaustValue_16 As System.Windows.Forms.Label
    Friend WithEvents lblExhaustValue_17 As System.Windows.Forms.Label
    Friend WithEvents lblExhaustValue_18 As System.Windows.Forms.Label
    Friend WithEvents lblExhaustValue_19 As System.Windows.Forms.Label
    Friend WithEvents lblExhaustValue_20 As System.Windows.Forms.Label
    Friend WithEvents lblExhaustValue_21 As System.Windows.Forms.Label
    Friend WithEvents lblExhaustValue_22 As System.Windows.Forms.Label
    Friend WithEvents lblExhaustValue_23 As System.Windows.Forms.Label
    Friend WithEvents lblExhaustValue_24 As System.Windows.Forms.Label
    Friend WithEvents lblExhaustValue_25 As System.Windows.Forms.Label
    Friend WithEvents cmdResetThresholds_2 As System.Windows.Forms.Button
    Friend WithEvents cmdSaveThresholds_2 As System.Windows.Forms.Button
    Friend WithEvents tabSysmon_Page3 As System.Windows.Forms.TabPage
    Friend WithEvents fraChassisTest_2 As System.Windows.Forms.GroupBox
    Friend WithEvents picIconGraphic_3 As System.Windows.Forms.PictureBox
    Friend WithEvents lblCst_2 As System.Windows.Forms.Label
    Friend WithEvents fraFanSpeed_1 As System.Windows.Forms.GroupBox
    Friend WithEvents picIconGraphic_9 As System.Windows.Forms.PictureBox
    Friend WithEvents txtFanSpeedValue_2 As System.Windows.Forms.TextBox
    Friend WithEvents fraHeatingUnits_1 As System.Windows.Forms.GroupBox
    Friend WithEvents picIconGraphic_5 As System.Windows.Forms.PictureBox
    Friend WithEvents lblHu_3 As System.Windows.Forms.Label
    Friend WithEvents lblHu_2 As System.Windows.Forms.Label
    Friend WithEvents fraAirflowSensors_1 As System.Windows.Forms.GroupBox
    Friend WithEvents picIconGraphic_7 As System.Windows.Forms.PictureBox
    Friend WithEvents lblPca_2 As System.Windows.Forms.Label
    Friend WithEvents lblPca_0 As System.Windows.Forms.Label
    Friend WithEvents lblPca_3 As System.Windows.Forms.Label
    Friend WithEvents fraFanSpeed_0 As System.Windows.Forms.GroupBox
    Friend WithEvents picIconGraphic_8 As System.Windows.Forms.PictureBox
    Friend WithEvents txtFanSpeedValue_1 As System.Windows.Forms.TextBox
    Friend WithEvents fraAirflowSensors_0 As System.Windows.Forms.GroupBox
    Friend WithEvents picIconGraphic_6 As System.Windows.Forms.PictureBox
    Friend WithEvents lblPca_5 As System.Windows.Forms.Label
    Friend WithEvents lblPca_4 As System.Windows.Forms.Label
    Friend WithEvents lblPca_1 As System.Windows.Forms.Label
    Friend WithEvents fraHeatingUnits_0 As System.Windows.Forms.GroupBox
    Friend WithEvents picIconGraphic_0 As System.Windows.Forms.PictureBox
    Friend WithEvents lblHu_0 As System.Windows.Forms.Label
    Friend WithEvents lblHu_1 As System.Windows.Forms.Label
    Friend WithEvents fraChassisTest_1 As System.Windows.Forms.GroupBox
    Friend WithEvents picIconGraphic_2 As System.Windows.Forms.PictureBox
    Friend WithEvents lblCst_1 As System.Windows.Forms.Label
    Friend WithEvents tabSysmon_Page4 As System.Windows.Forms.TabPage
    Friend WithEvents SSFrame1 As System.Windows.Forms.GroupBox
    Friend WithEvents picIconGraphic_15 As System.Windows.Forms.PictureBox
    Friend WithEvents lblLevel_13 As System.Windows.Forms.Label
    Friend WithEvents lblLevel_12 As System.Windows.Forms.Label
    Friend WithEvents lblLevel_11 As System.Windows.Forms.Label
    Friend WithEvents lblLevel_10 As System.Windows.Forms.Label
    Friend WithEvents lblLevel_9 As System.Windows.Forms.Label
    Friend WithEvents lblLevel_8 As System.Windows.Forms.Label
    Friend WithEvents lblLevel_7 As System.Windows.Forms.Label
    Friend WithEvents Label2_6 As System.Windows.Forms.Label
    Friend WithEvents Label2_5 As System.Windows.Forms.Label
    Friend WithEvents Label2_4 As System.Windows.Forms.Label
    Friend WithEvents Label2_3 As System.Windows.Forms.Label
    Friend WithEvents Label2_2 As System.Windows.Forms.Label
    Friend WithEvents Label2_1 As System.Windows.Forms.Label
    Friend WithEvents Label2_0 As System.Windows.Forms.Label
    Friend WithEvents lblCp_9 As System.Windows.Forms.Label
    Friend WithEvents lblCp_8 As System.Windows.Forms.Label
    Friend WithEvents SSFrame2 As System.Windows.Forms.GroupBox
    Friend WithEvents picIconGraphic_14 As System.Windows.Forms.PictureBox
    Friend WithEvents lblLevel_6 As System.Windows.Forms.Label
    Friend WithEvents lblLevel_5 As System.Windows.Forms.Label
    Friend WithEvents lblLevel_4 As System.Windows.Forms.Label
    Friend WithEvents lblLevel_3 As System.Windows.Forms.Label
    Friend WithEvents lblLevel_2 As System.Windows.Forms.Label
    Friend WithEvents lblLevel_1 As System.Windows.Forms.Label
    Friend WithEvents lblCp_5 As System.Windows.Forms.Label
    Friend WithEvents lblCp_4 As System.Windows.Forms.Label
    Friend WithEvents Label2_76 As System.Windows.Forms.Label
    Friend WithEvents Label2_75 As System.Windows.Forms.Label
    Friend WithEvents Label2_74 As System.Windows.Forms.Label
    Friend WithEvents Label2_73 As System.Windows.Forms.Label
    Friend WithEvents Label2_72 As System.Windows.Forms.Label
    Friend WithEvents Label2_71 As System.Windows.Forms.Label
    Friend WithEvents Label2_70 As System.Windows.Forms.Label
    Friend WithEvents lblLevel_0 As System.Windows.Forms.Label
    Friend WithEvents fraChassisVoltage As System.Windows.Forms.GroupBox
    Friend WithEvents lblBackVolt_13 As System.Windows.Forms.Label
    Friend WithEvents lblBackVolt_12 As System.Windows.Forms.Label
    Friend WithEvents lblBackVolt_11 As System.Windows.Forms.Label
    Friend WithEvents lblBackVolt_10 As System.Windows.Forms.Label
    Friend WithEvents lblBackVolt_9 As System.Windows.Forms.Label
    Friend WithEvents lblBackVolt_8 As System.Windows.Forms.Label
    Friend WithEvents lblBackVolt_7 As System.Windows.Forms.Label
    Friend WithEvents lblCp_7 As System.Windows.Forms.Label
    Friend WithEvents lblCp_6 As System.Windows.Forms.Label
    Friend WithEvents lblBackVolt_6 As System.Windows.Forms.Label
    Friend WithEvents lblBackVolt_5 As System.Windows.Forms.Label
    Friend WithEvents lblBackVolt_4 As System.Windows.Forms.Label
    Friend WithEvents lblBackVolt_3 As System.Windows.Forms.Label
    Friend WithEvents lblBackVolt_2 As System.Windows.Forms.Label
    Friend WithEvents lblBackVolt_1 As System.Windows.Forms.Label
    Friend WithEvents lblBackVolt_0 As System.Windows.Forms.Label
    Friend WithEvents lblCp_0 As System.Windows.Forms.Label
    Friend WithEvents lblCp_1 As System.Windows.Forms.Label
    Friend WithEvents Label2_60 As System.Windows.Forms.Label
    Friend WithEvents Label2_59 As System.Windows.Forms.Label
    Friend WithEvents Label2_58 As System.Windows.Forms.Label
    Friend WithEvents Label2_55 As System.Windows.Forms.Label
    Friend WithEvents Label2_54 As System.Windows.Forms.Label
    Friend WithEvents Label2_53 As System.Windows.Forms.Label
    Friend WithEvents Label2_52 As System.Windows.Forms.Label
    Friend WithEvents picIconGraphic_10 As System.Windows.Forms.PictureBox
    Friend WithEvents fraChassisCurrent As System.Windows.Forms.GroupBox
    Friend WithEvents picIconGraphic_11 As System.Windows.Forms.PictureBox
    Friend WithEvents lblBackCurr_13 As System.Windows.Forms.Label
    Friend WithEvents lblBackCurr_12 As System.Windows.Forms.Label
    Friend WithEvents lblBackCurr_11 As System.Windows.Forms.Label
    Friend WithEvents lblBackCurr_10 As System.Windows.Forms.Label
    Friend WithEvents lblBackCurr_9 As System.Windows.Forms.Label
    Friend WithEvents lblBackCurr_8 As System.Windows.Forms.Label
    Friend WithEvents lblBackCurr_7 As System.Windows.Forms.Label
    Friend WithEvents lblCp_11 As System.Windows.Forms.Label
    Friend WithEvents lblCp_10 As System.Windows.Forms.Label
    Friend WithEvents Label2_13 As System.Windows.Forms.Label
    Friend WithEvents Label2_12 As System.Windows.Forms.Label
    Friend WithEvents Label2_11 As System.Windows.Forms.Label
    Friend WithEvents Label2_10 As System.Windows.Forms.Label
    Friend WithEvents Label2_9 As System.Windows.Forms.Label
    Friend WithEvents Label2_8 As System.Windows.Forms.Label
    Friend WithEvents Label2_7 As System.Windows.Forms.Label
    Friend WithEvents lblBackCurr_6 As System.Windows.Forms.Label
    Friend WithEvents lblBackCurr_5 As System.Windows.Forms.Label
    Friend WithEvents lblBackCurr_4 As System.Windows.Forms.Label
    Friend WithEvents lblBackCurr_3 As System.Windows.Forms.Label
    Friend WithEvents lblBackCurr_2 As System.Windows.Forms.Label
    Friend WithEvents lblBackCurr_1 As System.Windows.Forms.Label
    Friend WithEvents lblBackCurr_0 As System.Windows.Forms.Label
    Friend WithEvents lblCp_2 As System.Windows.Forms.Label
    Friend WithEvents lblCp_3 As System.Windows.Forms.Label
    Friend WithEvents tabSysmon_Page5 As System.Windows.Forms.TabPage
    Friend WithEvents fraUutVoltage As System.Windows.Forms.GroupBox
    Friend WithEvents picIconGraphic_12 As System.Windows.Forms.PictureBox
    Friend WithEvents lblUutVolt_10 As System.Windows.Forms.Label
    Friend WithEvents lblUutVolt_9 As System.Windows.Forms.Label
    Friend WithEvents lblUutVolt_8 As System.Windows.Forms.Label
    Friend WithEvents lblUutVolt_7 As System.Windows.Forms.Label
    Friend WithEvents lblUutVolt_6 As System.Windows.Forms.Label
    Friend WithEvents lblUutVolt_5 As System.Windows.Forms.Label
    Friend WithEvents lblUutVolt_4 As System.Windows.Forms.Label
    Friend WithEvents lblUutVolt_3 As System.Windows.Forms.Label
    Friend WithEvents lblUutVolt_2 As System.Windows.Forms.Label
    Friend WithEvents lblUutVolt_1 As System.Windows.Forms.Label
    Friend WithEvents Label2_89 As System.Windows.Forms.Label
    Friend WithEvents Label2_88 As System.Windows.Forms.Label
    Friend WithEvents Label2_87 As System.Windows.Forms.Label
    Friend WithEvents Label2_86 As System.Windows.Forms.Label
    Friend WithEvents Label2_85 As System.Windows.Forms.Label
    Friend WithEvents Label2_84 As System.Windows.Forms.Label
    Friend WithEvents Label2_83 As System.Windows.Forms.Label
    Friend WithEvents Label2_82 As System.Windows.Forms.Label
    Friend WithEvents Label2_81 As System.Windows.Forms.Label
    Friend WithEvents Label2_80 As System.Windows.Forms.Label
    Friend WithEvents lblUut_1 As System.Windows.Forms.Label
    Friend WithEvents lblUut_0 As System.Windows.Forms.Label
    Friend WithEvents fraUutCurrent As System.Windows.Forms.GroupBox
    Friend WithEvents lblUutCur_10 As System.Windows.Forms.Label
    Friend WithEvents lblUutCur_9 As System.Windows.Forms.Label
    Friend WithEvents lblUutCur_8 As System.Windows.Forms.Label
    Friend WithEvents lblUutCur_7 As System.Windows.Forms.Label
    Friend WithEvents lblUutCur_6 As System.Windows.Forms.Label
    Friend WithEvents lblUutCur_5 As System.Windows.Forms.Label
    Friend WithEvents lblUutCur_4 As System.Windows.Forms.Label
    Friend WithEvents lblUutCur_3 As System.Windows.Forms.Label
    Friend WithEvents lblUutCur_2 As System.Windows.Forms.Label
    Friend WithEvents lblUutCur_1 As System.Windows.Forms.Label
    Friend WithEvents Label2_99 As System.Windows.Forms.Label
    Friend WithEvents Label2_98 As System.Windows.Forms.Label
    Friend WithEvents Label2_97 As System.Windows.Forms.Label
    Friend WithEvents Label2_96 As System.Windows.Forms.Label
    Friend WithEvents Label2_95 As System.Windows.Forms.Label
    Friend WithEvents Label2_94 As System.Windows.Forms.Label
    Friend WithEvents Label2_93 As System.Windows.Forms.Label
    Friend WithEvents Label2_92 As System.Windows.Forms.Label
    Friend WithEvents Label2_91 As System.Windows.Forms.Label
    Friend WithEvents Label2_90 As System.Windows.Forms.Label
    Friend WithEvents lblUut_3 As System.Windows.Forms.Label
    Friend WithEvents lblUut_2 As System.Windows.Forms.Label
    Friend WithEvents tabSysmon_Page6 As System.Windows.Forms.TabPage
    Friend WithEvents fraPDU_5 As System.Windows.Forms.GroupBox
    Friend WithEvents picIconGraphic_18 As System.Windows.Forms.PictureBox
    Friend WithEvents fraFanSpeed_2 As System.Windows.Forms.GroupBox
    Friend WithEvents lblPower As System.Windows.Forms.Label
    Friend WithEvents fraMaintenance_0 As System.Windows.Forms.GroupBox
    Friend WithEvents lblSysPower_7 As System.Windows.Forms.Label
    Friend WithEvents lblSysPower_6 As System.Windows.Forms.Label
    Friend WithEvents lblSysPower_5 As System.Windows.Forms.Label
    Friend WithEvents lblInputPower_17 As System.Windows.Forms.Label
    Friend WithEvents lblInputPower_11 As System.Windows.Forms.Label
    Friend WithEvents lblInputPower_10 As System.Windows.Forms.Label
    Friend WithEvents fraPDU_1 As System.Windows.Forms.GroupBox
    Friend WithEvents picIconGraphic_16 As System.Windows.Forms.PictureBox
    Friend WithEvents fraPDU_2 As System.Windows.Forms.GroupBox
    Friend WithEvents picIconGraphic_17 As System.Windows.Forms.PictureBox
    Friend WithEvents fraPDU_4 As System.Windows.Forms.GroupBox
    Friend WithEvents lblInputPower_16 As System.Windows.Forms.Label
    Friend WithEvents lblSysPower_0 As System.Windows.Forms.Label
    Friend WithEvents lblSysPower_4 As System.Windows.Forms.Label
    Friend WithEvents lblSysPower_3 As System.Windows.Forms.Label
    Friend WithEvents lblSysPower_2 As System.Windows.Forms.Label
    Friend WithEvents lblSysPower_1 As System.Windows.Forms.Label
    Friend WithEvents lblInputPower_15 As System.Windows.Forms.Label
    Friend WithEvents lblInputPower_14 As System.Windows.Forms.Label
    Friend WithEvents lblInputPower_13 As System.Windows.Forms.Label
    Friend WithEvents lblInputPower_12 As System.Windows.Forms.Label
    Friend WithEvents fraPDU_3 As System.Windows.Forms.GroupBox
    Friend WithEvents lblSourceMode As System.Windows.Forms.Label
    Friend WithEvents lblInputPower_7 As System.Windows.Forms.Label
    Friend WithEvents lblInputPower_3 As System.Windows.Forms.Label
    Friend WithEvents lblInputPower_2 As System.Windows.Forms.Label
    Friend WithEvents lblInputPower_1 As System.Windows.Forms.Label
    Friend WithEvents lblInputPower_0 As System.Windows.Forms.Label
    Friend WithEvents fraPDU_0 As System.Windows.Forms.GroupBox
    Friend WithEvents lblInputPower_8 As System.Windows.Forms.Label
    Friend WithEvents lblInPower_4 As System.Windows.Forms.Label
    Friend WithEvents lblInPower_3 As System.Windows.Forms.Label
    Friend WithEvents lblInPower_2 As System.Windows.Forms.Label
    Friend WithEvents lblInPower_1 As System.Windows.Forms.Label
    Friend WithEvents lblInPower_0 As System.Windows.Forms.Label
    Friend WithEvents lblInputPower_9 As System.Windows.Forms.Label
    Friend WithEvents lblInputPower_6 As System.Windows.Forms.Label
    Friend WithEvents lblInputPower_5 As System.Windows.Forms.Label
    Friend WithEvents lblInputPower_4 As System.Windows.Forms.Label
    Friend WithEvents tabSysmon_Page7 As System.Windows.Forms.TabPage
    Friend WithEvents lblAccount As System.Windows.Forms.Label
    Friend WithEvents fraMaintenance_7 As System.Windows.Forms.GroupBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Line1 As System.Windows.Forms.Label
    Friend WithEvents lblmode_9 As System.Windows.Forms.Label
    Friend WithEvents lblmode_8 As System.Windows.Forms.Label
    Friend WithEvents lblmode_7 As System.Windows.Forms.Label
    Friend WithEvents lblmode_0 As System.Windows.Forms.Label
    Friend WithEvents fraMaintenance_4 As System.Windows.Forms.GroupBox
    Friend WithEvents chkHeater_2 As System.Windows.Forms.CheckBox
    Friend WithEvents chkHeater_3 As System.Windows.Forms.CheckBox
    Friend WithEvents chkEnableChassisOption_6 As System.Windows.Forms.CheckBox
    Friend WithEvents chkEnableChassisOption_2 As System.Windows.Forms.CheckBox
    Friend WithEvents chkEnableChassisOption_4 As System.Windows.Forms.CheckBox
    Friend WithEvents lblmode_6 As System.Windows.Forms.Label
    Friend WithEvents lblmode_2 As System.Windows.Forms.Label
    Friend WithEvents lblmode_4 As System.Windows.Forms.Label
    Friend WithEvents Label1_5 As System.Windows.Forms.Label
    Friend WithEvents Label1_4 As System.Windows.Forms.Label
    Friend WithEvents Label1_3 As System.Windows.Forms.Label
    Friend WithEvents lblUnit_21 As System.Windows.Forms.Label
    Friend WithEvents lblUnit_22 As System.Windows.Forms.Label
    Friend WithEvents lblUnit_18 As System.Windows.Forms.Label
    Friend WithEvents fraMaintenance_6 As System.Windows.Forms.GroupBox
    Friend WithEvents cmdCalChass_1 As System.Windows.Forms.Button
    Friend WithEvents cmdCalChass_2 As System.Windows.Forms.Button
    Friend WithEvents fraMaintenance_5 As System.Windows.Forms.GroupBox
    Friend WithEvents chkPpuNoFault_10 As System.Windows.Forms.CheckBox
    Friend WithEvents chkPpuNoFault_9 As System.Windows.Forms.CheckBox
    Friend WithEvents chkPpuNoFault_8 As System.Windows.Forms.CheckBox
    Friend WithEvents chkPpuNoFault_7 As System.Windows.Forms.CheckBox
    Friend WithEvents chkPpuNoFault_6 As System.Windows.Forms.CheckBox
    Friend WithEvents chkPpuNoFault_5 As System.Windows.Forms.CheckBox
    Friend WithEvents chkPpuNoFault_4 As System.Windows.Forms.CheckBox
    Friend WithEvents chkPpuNoFault_3 As System.Windows.Forms.CheckBox
    Friend WithEvents chkPpuNoFault_2 As System.Windows.Forms.CheckBox
    Friend WithEvents chkPpuNoFault_1 As System.Windows.Forms.CheckBox
    Friend WithEvents fraMaintenance_1 As System.Windows.Forms.GroupBox
    Friend WithEvents chkDataInterval As System.Windows.Forms.CheckBox
    Friend WithEvents lblUnit_16 As System.Windows.Forms.Label
    Friend WithEvents lblActivity As System.Windows.Forms.Label
    Friend WithEvents fraMaintenance_2 As System.Windows.Forms.GroupBox
    Friend WithEvents optFpuState_1 As System.Windows.Forms.RadioButton
    Friend WithEvents optFpuState_0 As System.Windows.Forms.RadioButton
    Friend WithEvents chkFpuNoFault As System.Windows.Forms.CheckBox
    Friend WithEvents cmdResetChassis_1 As System.Windows.Forms.Button
    Friend WithEvents cmdResetChassis_2 As System.Windows.Forms.Button
    Friend WithEvents cmdResetPdu_0 As System.Windows.Forms.Button
    Friend WithEvents fraMaintenance_3 As System.Windows.Forms.GroupBox
    Friend WithEvents chkHeater_0 As System.Windows.Forms.CheckBox
    Friend WithEvents chkHeater_1 As System.Windows.Forms.CheckBox
    Friend WithEvents chkEnableChassisOption_5 As System.Windows.Forms.CheckBox
    Friend WithEvents chkEnableChassisOption_1 As System.Windows.Forms.CheckBox
    Friend WithEvents chkEnableChassisOption_3 As System.Windows.Forms.CheckBox
    Friend WithEvents lblmode_5 As System.Windows.Forms.Label
    Friend WithEvents lblmode_1 As System.Windows.Forms.Label
    Friend WithEvents lblmode_3 As System.Windows.Forms.Label
    Friend WithEvents Label1_2 As System.Windows.Forms.Label
    Friend WithEvents Label1_1 As System.Windows.Forms.Label
    Friend WithEvents Label1_0 As System.Windows.Forms.Label
    Friend WithEvents lblUnit_17 As System.Windows.Forms.Label
    Friend WithEvents lblUnit_19 As System.Windows.Forms.Label
    Friend WithEvents lblUnit_20 As System.Windows.Forms.Label
    Friend WithEvents cmdFixEthernet As System.Windows.Forms.Button
    Friend WithEvents sbrUserInformation As System.Windows.Forms.StatusBar
    Friend WithEvents sbrUserInformation_Panel1 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents cwsSlotRise_0 As NationalInstruments.UI.WindowsForms.Slide
    Friend WithEvents cwsSlotRise_9 As NationalInstruments.UI.WindowsForms.Slide
    Friend WithEvents cwsSlotRise_8 As NationalInstruments.UI.WindowsForms.Slide
    Friend WithEvents cwsSlotRise_7 As NationalInstruments.UI.WindowsForms.Slide
    Friend WithEvents cwsSlotRise_6 As NationalInstruments.UI.WindowsForms.Slide
    Friend WithEvents cwsSlotRise_5 As NationalInstruments.UI.WindowsForms.Slide
    Friend WithEvents cwsSlotRise_4 As NationalInstruments.UI.WindowsForms.Slide
    Friend WithEvents cwsSlotRise_3 As NationalInstruments.UI.WindowsForms.Slide
    Friend WithEvents cwsSlotRise_2 As NationalInstruments.UI.WindowsForms.Slide
    Friend WithEvents cwsSlotRise_1 As NationalInstruments.UI.WindowsForms.Slide
    Friend WithEvents cwsSlotRise_12 As NationalInstruments.UI.WindowsForms.Slide
    Friend WithEvents cwsSlotRise_11 As NationalInstruments.UI.WindowsForms.Slide
    Friend WithEvents cwsSlotRise_10 As NationalInstruments.UI.WindowsForms.Slide
    Friend WithEvents cwsSlotRise_25 As NationalInstruments.UI.WindowsForms.Slide
    Friend WithEvents cwsSlotRise_24 As NationalInstruments.UI.WindowsForms.Slide
    Friend WithEvents cwsSlotRise_23 As NationalInstruments.UI.WindowsForms.Slide
    Friend WithEvents cwsSlotRise_22 As NationalInstruments.UI.WindowsForms.Slide
    Friend WithEvents cwsSlotRise_21 As NationalInstruments.UI.WindowsForms.Slide
    Friend WithEvents cwsSlotRise_20 As NationalInstruments.UI.WindowsForms.Slide
    Friend WithEvents cwsSlotRise_19 As NationalInstruments.UI.WindowsForms.Slide
    Friend WithEvents cwsSlotRise_18 As NationalInstruments.UI.WindowsForms.Slide
    Friend WithEvents cwsSlotRise_17 As NationalInstruments.UI.WindowsForms.Slide
    Friend WithEvents cwsSlotRise_16 As NationalInstruments.UI.WindowsForms.Slide
    Friend WithEvents cwsSlotRise_15 As NationalInstruments.UI.WindowsForms.Slide
    Friend WithEvents cwsSlotRise_14 As NationalInstruments.UI.WindowsForms.Slide
    Friend WithEvents cwsSlotRise_13 As NationalInstruments.UI.WindowsForms.Slide
    Friend cwsSlotRise(26) As NationalInstruments.UI.WindowsForms.Slide
    Friend WithEvents cwbChasSelfTestIndicator_1 As NationalInstruments.UI.WindowsForms.Led
    Friend WithEvents cwbChasSelfTestIndicator_2 As NationalInstruments.UI.WindowsForms.Led
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents cwsSlotRiseActual_1 As NationalInstruments.UI.WindowsForms.Tank
    Friend WithEvents cwsSlotRiseActual_2 As NationalInstruments.UI.WindowsForms.Tank
    Friend WithEvents cwsSlotRiseActual_3 As NationalInstruments.UI.WindowsForms.Tank
    Friend WithEvents cwsSlotRiseActual_4 As NationalInstruments.UI.WindowsForms.Tank
    Friend WithEvents cwsSlotRiseActual_5 As NationalInstruments.UI.WindowsForms.Tank
    Friend WithEvents cwsSlotRiseActual_6 As NationalInstruments.UI.WindowsForms.Tank
    Friend WithEvents cwsSlotRiseActual_7 As NationalInstruments.UI.WindowsForms.Tank
    Friend WithEvents cwsSlotRiseActual_8 As NationalInstruments.UI.WindowsForms.Tank
    Friend WithEvents cwsSlotRiseActual_9 As NationalInstruments.UI.WindowsForms.Tank
    Friend WithEvents cwsSlotRiseActual_12 As NationalInstruments.UI.WindowsForms.Tank
    Friend WithEvents cwsSlotRiseActual_11 As NationalInstruments.UI.WindowsForms.Tank
    Friend WithEvents cwsSlotRiseActual_10 As NationalInstruments.UI.WindowsForms.Tank
    Friend WithEvents cwsSlotRiseActual_0 As NationalInstruments.UI.WindowsForms.Tank
    Friend cwsSlotRiseActual(26) As NationalInstruments.UI.WindowsForms.Tank
    Friend WithEvents dummySlide_0 As NationalInstruments.UI.WindowsForms.Slide
    Friend WithEvents dummySlide_1 As NationalInstruments.UI.WindowsForms.Slide
    Friend WithEvents cwsSlotRiseActual_13 As NationalInstruments.UI.WindowsForms.Tank
    Friend WithEvents Slide1 As NationalInstruments.UI.WindowsForms.Slide
    Friend WithEvents Slide2 As NationalInstruments.UI.WindowsForms.Slide
    Friend WithEvents cwsSlotRiseActual_25 As NationalInstruments.UI.WindowsForms.Tank
    Friend WithEvents cwsSlotRiseActual_24 As NationalInstruments.UI.WindowsForms.Tank
    Friend WithEvents cwsSlotRiseActual_23 As NationalInstruments.UI.WindowsForms.Tank
    Friend WithEvents cwsSlotRiseActual_22 As NationalInstruments.UI.WindowsForms.Tank
    Friend WithEvents cwsSlotRiseActual_21 As NationalInstruments.UI.WindowsForms.Tank
    Friend WithEvents cwsSlotRiseActual_20 As NationalInstruments.UI.WindowsForms.Tank
    Friend WithEvents cwsSlotRiseActual_19 As NationalInstruments.UI.WindowsForms.Tank
    Friend WithEvents cwsSlotRiseActual_18 As NationalInstruments.UI.WindowsForms.Tank
    Friend WithEvents cwsSlotRiseActual_17 As NationalInstruments.UI.WindowsForms.Tank
    Friend WithEvents cwsSlotRiseActual_16 As NationalInstruments.UI.WindowsForms.Tank
    Friend WithEvents cwsSlotRiseActual_15 As NationalInstruments.UI.WindowsForms.Tank
    Friend WithEvents cwsSlotRiseActual_14 As NationalInstruments.UI.WindowsForms.Tank
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents cwbRcvrSwitch As NationalInstruments.UI.WindowsForms.Led
    Friend WithEvents cwb28VOk As NationalInstruments.UI.WindowsForms.Led
    Friend WithEvents cwbHamPhase_1 As NationalInstruments.UI.WindowsForms.Led
    Friend WithEvents cwbHamPhase_2 As NationalInstruments.UI.WindowsForms.Led
    Friend WithEvents cwbHamPhase_3 As NationalInstruments.UI.WindowsForms.Led
    Friend WithEvents cwbPowerConverters As NationalInstruments.UI.WindowsForms.Led
    Friend WithEvents cwbInputPower As NationalInstruments.UI.WindowsForms.Led
    Friend WithEvents cwbProbe As NationalInstruments.UI.WindowsForms.Led
    Friend WithEvents panPower As System.Windows.Forms.ProgressBar
    Friend WithEvents panFanSpeed_1 As System.Windows.Forms.ProgressBar
    Friend WithEvents panFanSpeed_0 As System.Windows.Forms.ProgressBar
    Friend WithEvents cwbAirFlowIndicatorPF_3 As NationalInstruments.UI.WindowsForms.Led
    Friend WithEvents cwbAirFlowIndicatorPF_2 As NationalInstruments.UI.WindowsForms.Led
    Friend WithEvents cwbAirFlowIndicatorPF_1 As NationalInstruments.UI.WindowsForms.Led
    Friend WithEvents cwbAirFlowIndicatorPF_6 As NationalInstruments.UI.WindowsForms.Led
    Friend WithEvents cwbAirFlowIndicatorPF_5 As NationalInstruments.UI.WindowsForms.Led
    Friend WithEvents cwbAirFlowIndicatorPF_4 As NationalInstruments.UI.WindowsForms.Led
    Friend WithEvents cwbHeaterIndicatorOO_1 As NationalInstruments.UI.WindowsForms.Led
    Friend WithEvents cwbHeaterIndicatorOO_0 As NationalInstruments.UI.WindowsForms.Led
    Friend WithEvents cwbHeaterIndicatorOO_3 As NationalInstruments.UI.WindowsForms.Led
    Friend WithEvents PictureBox3 As System.Windows.Forms.PictureBox
    Friend WithEvents Panel_4 As NationalInstruments.UI.WindowsForms.NumericEdit
    Friend WithEvents Panel_3 As NationalInstruments.UI.WindowsForms.NumericEdit
    Friend WithEvents Panel_1 As NationalInstruments.UI.WindowsForms.NumericEdit
    Friend WithEvents Panel_2 As NationalInstruments.UI.WindowsForms.NumericEdit
    Friend WithEvents cwbHeaterIndicatorOO_2 As NationalInstruments.UI.WindowsForms.Led
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSysMon))
        Me.picIconGraphic = New Microsoft.VisualBasic.Compatibility.VB6.PictureBoxArray(Me.components)
        Me.picIconGraphic_1 = New System.Windows.Forms.PictureBox()
        Me.picIconGraphic_4 = New System.Windows.Forms.PictureBox()
        Me.picIconGraphic_3 = New System.Windows.Forms.PictureBox()
        Me.picIconGraphic_9 = New System.Windows.Forms.PictureBox()
        Me.picIconGraphic_5 = New System.Windows.Forms.PictureBox()
        Me.picIconGraphic_7 = New System.Windows.Forms.PictureBox()
        Me.picIconGraphic_8 = New System.Windows.Forms.PictureBox()
        Me.picIconGraphic_6 = New System.Windows.Forms.PictureBox()
        Me.picIconGraphic_0 = New System.Windows.Forms.PictureBox()
        Me.picIconGraphic_2 = New System.Windows.Forms.PictureBox()
        Me.picIconGraphic_15 = New System.Windows.Forms.PictureBox()
        Me.picIconGraphic_14 = New System.Windows.Forms.PictureBox()
        Me.picIconGraphic_10 = New System.Windows.Forms.PictureBox()
        Me.picIconGraphic_11 = New System.Windows.Forms.PictureBox()
        Me.picIconGraphic_12 = New System.Windows.Forms.PictureBox()
        Me.picIconGraphic_18 = New System.Windows.Forms.PictureBox()
        Me.picIconGraphic_16 = New System.Windows.Forms.PictureBox()
        Me.picIconGraphic_17 = New System.Windows.Forms.PictureBox()
        Me.lblIntakeValue = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.lblIntakeValue_0 = New System.Windows.Forms.Label()
        Me.lblIntakeValue_1 = New System.Windows.Forms.Label()
        Me.lblUnit = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.lblUnit_7 = New System.Windows.Forms.Label()
        Me.lblUnit_8 = New System.Windows.Forms.Label()
        Me.lblUnit_0 = New System.Windows.Forms.Label()
        Me.lblUnit_2 = New System.Windows.Forms.Label()
        Me.lblUnit_21 = New System.Windows.Forms.Label()
        Me.lblUnit_22 = New System.Windows.Forms.Label()
        Me.lblUnit_18 = New System.Windows.Forms.Label()
        Me.lblUnit_16 = New System.Windows.Forms.Label()
        Me.lblUnit_17 = New System.Windows.Forms.Label()
        Me.lblUnit_19 = New System.Windows.Forms.Label()
        Me.lblUnit_20 = New System.Windows.Forms.Label()
        Me.cmdResetThresholds = New Microsoft.VisualBasic.Compatibility.VB6.ButtonArray(Me.components)
        Me.cmdResetThresholds_1 = New System.Windows.Forms.Button()
        Me.cmdResetThresholds_2 = New System.Windows.Forms.Button()
        Me.linRise = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.linRise_11 = New System.Windows.Forms.Label()
        Me.linRise_0 = New System.Windows.Forms.Label()
        Me.linRise_4 = New System.Windows.Forms.Label()
        Me.linRise_1 = New System.Windows.Forms.Label()
        Me.linRise_2 = New System.Windows.Forms.Label()
        Me.linRise_3 = New System.Windows.Forms.Label()
        Me.linRise_10 = New System.Windows.Forms.Label()
        Me.linRise_9 = New System.Windows.Forms.Label()
        Me.linRise_8 = New System.Windows.Forms.Label()
        Me.linRise_7 = New System.Windows.Forms.Label()
        Me.linRise_6 = New System.Windows.Forms.Label()
        Me.linRise_5 = New System.Windows.Forms.Label()
        Me.linRise_12 = New System.Windows.Forms.Label()
        Me.linRise_13 = New System.Windows.Forms.Label()
        Me.lblChassTemp = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.lblChassTemp_25 = New System.Windows.Forms.Label()
        Me.lblChassTemp_24 = New System.Windows.Forms.Label()
        Me.lblChassTemp_23 = New System.Windows.Forms.Label()
        Me.lblChassTemp_22 = New System.Windows.Forms.Label()
        Me.lblChassTemp_21 = New System.Windows.Forms.Label()
        Me.lblChassTemp_20 = New System.Windows.Forms.Label()
        Me.lblChassTemp_19 = New System.Windows.Forms.Label()
        Me.lblChassTemp_18 = New System.Windows.Forms.Label()
        Me.lblChassTemp_17 = New System.Windows.Forms.Label()
        Me.lblChassTemp_16 = New System.Windows.Forms.Label()
        Me.lblChassTemp_15 = New System.Windows.Forms.Label()
        Me.lblChassTemp_14 = New System.Windows.Forms.Label()
        Me.lblChassTemp_13 = New System.Windows.Forms.Label()
        Me.lblChassTemp_0 = New System.Windows.Forms.Label()
        Me.lblChassTemp_1 = New System.Windows.Forms.Label()
        Me.lblChassTemp_2 = New System.Windows.Forms.Label()
        Me.lblChassTemp_3 = New System.Windows.Forms.Label()
        Me.lblChassTemp_4 = New System.Windows.Forms.Label()
        Me.lblChassTemp_5 = New System.Windows.Forms.Label()
        Me.lblChassTemp_6 = New System.Windows.Forms.Label()
        Me.lblChassTemp_7 = New System.Windows.Forms.Label()
        Me.lblChassTemp_8 = New System.Windows.Forms.Label()
        Me.lblChassTemp_9 = New System.Windows.Forms.Label()
        Me.lblChassTemp_10 = New System.Windows.Forms.Label()
        Me.lblChassTemp_11 = New System.Windows.Forms.Label()
        Me.lblChassTemp_12 = New System.Windows.Forms.Label()
        Me.lblChassTemp_51 = New System.Windows.Forms.Label()
        Me.lblChassTemp_50 = New System.Windows.Forms.Label()
        Me.lblChassTemp_49 = New System.Windows.Forms.Label()
        Me.lblChassTemp_48 = New System.Windows.Forms.Label()
        Me.lblChassTemp_47 = New System.Windows.Forms.Label()
        Me.lblChassTemp_46 = New System.Windows.Forms.Label()
        Me.lblChassTemp_45 = New System.Windows.Forms.Label()
        Me.lblChassTemp_44 = New System.Windows.Forms.Label()
        Me.lblChassTemp_43 = New System.Windows.Forms.Label()
        Me.lblChassTemp_42 = New System.Windows.Forms.Label()
        Me.lblChassTemp_41 = New System.Windows.Forms.Label()
        Me.lblChassTemp_40 = New System.Windows.Forms.Label()
        Me.lblChassTemp_39 = New System.Windows.Forms.Label()
        Me.lblChassTemp_38 = New System.Windows.Forms.Label()
        Me.lblChassTemp_37 = New System.Windows.Forms.Label()
        Me.lblChassTemp_36 = New System.Windows.Forms.Label()
        Me.lblChassTemp_35 = New System.Windows.Forms.Label()
        Me.lblChassTemp_34 = New System.Windows.Forms.Label()
        Me.lblChassTemp_33 = New System.Windows.Forms.Label()
        Me.lblChassTemp_32 = New System.Windows.Forms.Label()
        Me.lblChassTemp_31 = New System.Windows.Forms.Label()
        Me.lblChassTemp_30 = New System.Windows.Forms.Label()
        Me.lblChassTemp_29 = New System.Windows.Forms.Label()
        Me.lblChassTemp_28 = New System.Windows.Forms.Label()
        Me.lblChassTemp_27 = New System.Windows.Forms.Label()
        Me.lblChassTemp_26 = New System.Windows.Forms.Label()
        Me.lblExhaustValue = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.lblExhaustValue_12 = New System.Windows.Forms.Label()
        Me.lblExhaustValue_11 = New System.Windows.Forms.Label()
        Me.lblExhaustValue_10 = New System.Windows.Forms.Label()
        Me.lblExhaustValue_9 = New System.Windows.Forms.Label()
        Me.lblExhaustValue_8 = New System.Windows.Forms.Label()
        Me.lblExhaustValue_7 = New System.Windows.Forms.Label()
        Me.lblExhaustValue_6 = New System.Windows.Forms.Label()
        Me.lblExhaustValue_5 = New System.Windows.Forms.Label()
        Me.lblExhaustValue_4 = New System.Windows.Forms.Label()
        Me.lblExhaustValue_3 = New System.Windows.Forms.Label()
        Me.lblExhaustValue_2 = New System.Windows.Forms.Label()
        Me.lblExhaustValue_1 = New System.Windows.Forms.Label()
        Me.lblExhaustValue_0 = New System.Windows.Forms.Label()
        Me.lblExhaustValue_13 = New System.Windows.Forms.Label()
        Me.lblExhaustValue_14 = New System.Windows.Forms.Label()
        Me.lblExhaustValue_15 = New System.Windows.Forms.Label()
        Me.lblExhaustValue_16 = New System.Windows.Forms.Label()
        Me.lblExhaustValue_17 = New System.Windows.Forms.Label()
        Me.lblExhaustValue_18 = New System.Windows.Forms.Label()
        Me.lblExhaustValue_19 = New System.Windows.Forms.Label()
        Me.lblExhaustValue_20 = New System.Windows.Forms.Label()
        Me.lblExhaustValue_21 = New System.Windows.Forms.Label()
        Me.lblExhaustValue_22 = New System.Windows.Forms.Label()
        Me.lblExhaustValue_23 = New System.Windows.Forms.Label()
        Me.lblExhaustValue_24 = New System.Windows.Forms.Label()
        Me.lblExhaustValue_25 = New System.Windows.Forms.Label()
        Me.cmdSaveThresholds = New Microsoft.VisualBasic.Compatibility.VB6.ButtonArray(Me.components)
        Me.cmdSaveThresholds_1 = New System.Windows.Forms.Button()
        Me.cmdSaveThresholds_2 = New System.Windows.Forms.Button()
        Me.lblCst = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.lblCst_2 = New System.Windows.Forms.Label()
        Me.lblCst_1 = New System.Windows.Forms.Label()
        Me.txtFanSpeedValue = New Microsoft.VisualBasic.Compatibility.VB6.TextBoxArray(Me.components)
        Me.txtFanSpeedValue_2 = New System.Windows.Forms.TextBox()
        Me.txtFanSpeedValue_1 = New System.Windows.Forms.TextBox()
        Me.lblHu = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.lblHu_3 = New System.Windows.Forms.Label()
        Me.lblHu_2 = New System.Windows.Forms.Label()
        Me.lblHu_0 = New System.Windows.Forms.Label()
        Me.lblHu_1 = New System.Windows.Forms.Label()
        Me.lblPca = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.lblPca_2 = New System.Windows.Forms.Label()
        Me.lblPca_0 = New System.Windows.Forms.Label()
        Me.lblPca_3 = New System.Windows.Forms.Label()
        Me.lblPca_5 = New System.Windows.Forms.Label()
        Me.lblPca_4 = New System.Windows.Forms.Label()
        Me.lblPca_1 = New System.Windows.Forms.Label()
        Me.lblLevel = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.lblLevel_13 = New System.Windows.Forms.Label()
        Me.lblLevel_12 = New System.Windows.Forms.Label()
        Me.lblLevel_11 = New System.Windows.Forms.Label()
        Me.lblLevel_10 = New System.Windows.Forms.Label()
        Me.lblLevel_9 = New System.Windows.Forms.Label()
        Me.lblLevel_8 = New System.Windows.Forms.Label()
        Me.lblLevel_7 = New System.Windows.Forms.Label()
        Me.lblLevel_6 = New System.Windows.Forms.Label()
        Me.lblLevel_5 = New System.Windows.Forms.Label()
        Me.lblLevel_4 = New System.Windows.Forms.Label()
        Me.lblLevel_3 = New System.Windows.Forms.Label()
        Me.lblLevel_2 = New System.Windows.Forms.Label()
        Me.lblLevel_1 = New System.Windows.Forms.Label()
        Me.lblLevel_0 = New System.Windows.Forms.Label()
        Me.Label2 = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.Label2_6 = New System.Windows.Forms.Label()
        Me.Label2_5 = New System.Windows.Forms.Label()
        Me.Label2_4 = New System.Windows.Forms.Label()
        Me.Label2_3 = New System.Windows.Forms.Label()
        Me.Label2_2 = New System.Windows.Forms.Label()
        Me.Label2_1 = New System.Windows.Forms.Label()
        Me.Label2_0 = New System.Windows.Forms.Label()
        Me.Label2_76 = New System.Windows.Forms.Label()
        Me.Label2_75 = New System.Windows.Forms.Label()
        Me.Label2_74 = New System.Windows.Forms.Label()
        Me.Label2_73 = New System.Windows.Forms.Label()
        Me.Label2_72 = New System.Windows.Forms.Label()
        Me.Label2_71 = New System.Windows.Forms.Label()
        Me.Label2_70 = New System.Windows.Forms.Label()
        Me.Label2_60 = New System.Windows.Forms.Label()
        Me.Label2_59 = New System.Windows.Forms.Label()
        Me.Label2_58 = New System.Windows.Forms.Label()
        Me.Label2_55 = New System.Windows.Forms.Label()
        Me.Label2_54 = New System.Windows.Forms.Label()
        Me.Label2_53 = New System.Windows.Forms.Label()
        Me.Label2_52 = New System.Windows.Forms.Label()
        Me.Label2_13 = New System.Windows.Forms.Label()
        Me.Label2_12 = New System.Windows.Forms.Label()
        Me.Label2_11 = New System.Windows.Forms.Label()
        Me.Label2_10 = New System.Windows.Forms.Label()
        Me.Label2_9 = New System.Windows.Forms.Label()
        Me.Label2_8 = New System.Windows.Forms.Label()
        Me.Label2_7 = New System.Windows.Forms.Label()
        Me.Label2_89 = New System.Windows.Forms.Label()
        Me.Label2_88 = New System.Windows.Forms.Label()
        Me.Label2_87 = New System.Windows.Forms.Label()
        Me.Label2_86 = New System.Windows.Forms.Label()
        Me.Label2_85 = New System.Windows.Forms.Label()
        Me.Label2_84 = New System.Windows.Forms.Label()
        Me.Label2_83 = New System.Windows.Forms.Label()
        Me.Label2_82 = New System.Windows.Forms.Label()
        Me.Label2_81 = New System.Windows.Forms.Label()
        Me.Label2_80 = New System.Windows.Forms.Label()
        Me.Label2_99 = New System.Windows.Forms.Label()
        Me.Label2_98 = New System.Windows.Forms.Label()
        Me.Label2_97 = New System.Windows.Forms.Label()
        Me.Label2_96 = New System.Windows.Forms.Label()
        Me.Label2_95 = New System.Windows.Forms.Label()
        Me.Label2_94 = New System.Windows.Forms.Label()
        Me.Label2_93 = New System.Windows.Forms.Label()
        Me.Label2_92 = New System.Windows.Forms.Label()
        Me.Label2_91 = New System.Windows.Forms.Label()
        Me.Label2_90 = New System.Windows.Forms.Label()
        Me.lblCp = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.lblCp_9 = New System.Windows.Forms.Label()
        Me.lblCp_8 = New System.Windows.Forms.Label()
        Me.lblCp_5 = New System.Windows.Forms.Label()
        Me.lblCp_4 = New System.Windows.Forms.Label()
        Me.lblCp_7 = New System.Windows.Forms.Label()
        Me.lblCp_6 = New System.Windows.Forms.Label()
        Me.lblCp_0 = New System.Windows.Forms.Label()
        Me.lblCp_1 = New System.Windows.Forms.Label()
        Me.lblCp_11 = New System.Windows.Forms.Label()
        Me.lblCp_10 = New System.Windows.Forms.Label()
        Me.lblCp_2 = New System.Windows.Forms.Label()
        Me.lblCp_3 = New System.Windows.Forms.Label()
        Me.lblBackVolt = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.lblBackVolt_13 = New System.Windows.Forms.Label()
        Me.lblBackVolt_12 = New System.Windows.Forms.Label()
        Me.lblBackVolt_11 = New System.Windows.Forms.Label()
        Me.lblBackVolt_10 = New System.Windows.Forms.Label()
        Me.lblBackVolt_9 = New System.Windows.Forms.Label()
        Me.lblBackVolt_8 = New System.Windows.Forms.Label()
        Me.lblBackVolt_7 = New System.Windows.Forms.Label()
        Me.lblBackVolt_6 = New System.Windows.Forms.Label()
        Me.lblBackVolt_5 = New System.Windows.Forms.Label()
        Me.lblBackVolt_4 = New System.Windows.Forms.Label()
        Me.lblBackVolt_3 = New System.Windows.Forms.Label()
        Me.lblBackVolt_2 = New System.Windows.Forms.Label()
        Me.lblBackVolt_1 = New System.Windows.Forms.Label()
        Me.lblBackVolt_0 = New System.Windows.Forms.Label()
        Me.lblBackCurr = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.lblBackCurr_13 = New System.Windows.Forms.Label()
        Me.lblBackCurr_12 = New System.Windows.Forms.Label()
        Me.lblBackCurr_11 = New System.Windows.Forms.Label()
        Me.lblBackCurr_10 = New System.Windows.Forms.Label()
        Me.lblBackCurr_9 = New System.Windows.Forms.Label()
        Me.lblBackCurr_8 = New System.Windows.Forms.Label()
        Me.lblBackCurr_7 = New System.Windows.Forms.Label()
        Me.lblBackCurr_6 = New System.Windows.Forms.Label()
        Me.lblBackCurr_5 = New System.Windows.Forms.Label()
        Me.lblBackCurr_4 = New System.Windows.Forms.Label()
        Me.lblBackCurr_3 = New System.Windows.Forms.Label()
        Me.lblBackCurr_2 = New System.Windows.Forms.Label()
        Me.lblBackCurr_1 = New System.Windows.Forms.Label()
        Me.lblBackCurr_0 = New System.Windows.Forms.Label()
        Me.lblUutVolt = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.lblUutVolt_10 = New System.Windows.Forms.Label()
        Me.lblUutVolt_9 = New System.Windows.Forms.Label()
        Me.lblUutVolt_8 = New System.Windows.Forms.Label()
        Me.lblUutVolt_7 = New System.Windows.Forms.Label()
        Me.lblUutVolt_6 = New System.Windows.Forms.Label()
        Me.lblUutVolt_5 = New System.Windows.Forms.Label()
        Me.lblUutVolt_4 = New System.Windows.Forms.Label()
        Me.lblUutVolt_3 = New System.Windows.Forms.Label()
        Me.lblUutVolt_2 = New System.Windows.Forms.Label()
        Me.lblUutVolt_1 = New System.Windows.Forms.Label()
        Me.lblUut = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.lblUut_1 = New System.Windows.Forms.Label()
        Me.lblUut_0 = New System.Windows.Forms.Label()
        Me.lblUut_3 = New System.Windows.Forms.Label()
        Me.lblUut_2 = New System.Windows.Forms.Label()
        Me.lblUutCur = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.lblUutCur_10 = New System.Windows.Forms.Label()
        Me.lblUutCur_9 = New System.Windows.Forms.Label()
        Me.lblUutCur_8 = New System.Windows.Forms.Label()
        Me.lblUutCur_7 = New System.Windows.Forms.Label()
        Me.lblUutCur_6 = New System.Windows.Forms.Label()
        Me.lblUutCur_5 = New System.Windows.Forms.Label()
        Me.lblUutCur_4 = New System.Windows.Forms.Label()
        Me.lblUutCur_3 = New System.Windows.Forms.Label()
        Me.lblUutCur_2 = New System.Windows.Forms.Label()
        Me.lblUutCur_1 = New System.Windows.Forms.Label()
        Me.lblSysPower = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.lblSysPower_7 = New System.Windows.Forms.Label()
        Me.lblSysPower_6 = New System.Windows.Forms.Label()
        Me.lblSysPower_5 = New System.Windows.Forms.Label()
        Me.lblSysPower_0 = New System.Windows.Forms.Label()
        Me.lblSysPower_4 = New System.Windows.Forms.Label()
        Me.lblSysPower_3 = New System.Windows.Forms.Label()
        Me.lblSysPower_2 = New System.Windows.Forms.Label()
        Me.lblSysPower_1 = New System.Windows.Forms.Label()
        Me.lblInputPower = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.lblInputPower_17 = New System.Windows.Forms.Label()
        Me.lblInputPower_11 = New System.Windows.Forms.Label()
        Me.lblInputPower_10 = New System.Windows.Forms.Label()
        Me.lblInputPower_16 = New System.Windows.Forms.Label()
        Me.lblInputPower_15 = New System.Windows.Forms.Label()
        Me.lblInputPower_14 = New System.Windows.Forms.Label()
        Me.lblInputPower_13 = New System.Windows.Forms.Label()
        Me.lblInputPower_12 = New System.Windows.Forms.Label()
        Me.lblInputPower_7 = New System.Windows.Forms.Label()
        Me.lblInputPower_3 = New System.Windows.Forms.Label()
        Me.lblInputPower_2 = New System.Windows.Forms.Label()
        Me.lblInputPower_1 = New System.Windows.Forms.Label()
        Me.lblInputPower_0 = New System.Windows.Forms.Label()
        Me.lblInputPower_8 = New System.Windows.Forms.Label()
        Me.lblInputPower_9 = New System.Windows.Forms.Label()
        Me.lblInputPower_6 = New System.Windows.Forms.Label()
        Me.lblInputPower_5 = New System.Windows.Forms.Label()
        Me.lblInputPower_4 = New System.Windows.Forms.Label()
        Me.lblInPower = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.lblInPower_4 = New System.Windows.Forms.Label()
        Me.lblInPower_3 = New System.Windows.Forms.Label()
        Me.lblInPower_2 = New System.Windows.Forms.Label()
        Me.lblInPower_1 = New System.Windows.Forms.Label()
        Me.lblInPower_0 = New System.Windows.Forms.Label()
        Me.lblmode = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.lblmode_9 = New System.Windows.Forms.Label()
        Me.lblmode_8 = New System.Windows.Forms.Label()
        Me.lblmode_7 = New System.Windows.Forms.Label()
        Me.lblmode_0 = New System.Windows.Forms.Label()
        Me.lblmode_6 = New System.Windows.Forms.Label()
        Me.lblmode_2 = New System.Windows.Forms.Label()
        Me.lblmode_4 = New System.Windows.Forms.Label()
        Me.lblmode_5 = New System.Windows.Forms.Label()
        Me.lblmode_1 = New System.Windows.Forms.Label()
        Me.lblmode_3 = New System.Windows.Forms.Label()
        Me.TextBox = New Microsoft.VisualBasic.Compatibility.VB6.TextBoxArray(Me.components)
        Me.chkHeater_2 = New System.Windows.Forms.CheckBox()
        Me.chkHeater_3 = New System.Windows.Forms.CheckBox()
        Me.chkHeater_0 = New System.Windows.Forms.CheckBox()
        Me.chkHeater_1 = New System.Windows.Forms.CheckBox()
        Me.chkEnableChassisOption_6 = New System.Windows.Forms.CheckBox()
        Me.chkEnableChassisOption_2 = New System.Windows.Forms.CheckBox()
        Me.chkEnableChassisOption_4 = New System.Windows.Forms.CheckBox()
        Me.chkEnableChassisOption_5 = New System.Windows.Forms.CheckBox()
        Me.chkEnableChassisOption_1 = New System.Windows.Forms.CheckBox()
        Me.chkEnableChassisOption_3 = New System.Windows.Forms.CheckBox()
        Me.Label1 = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.Label1_5 = New System.Windows.Forms.Label()
        Me.Label1_4 = New System.Windows.Forms.Label()
        Me.Label1_3 = New System.Windows.Forms.Label()
        Me.Label1_2 = New System.Windows.Forms.Label()
        Me.Label1_1 = New System.Windows.Forms.Label()
        Me.Label1_0 = New System.Windows.Forms.Label()
        Me.cmdCalChass = New Microsoft.VisualBasic.Compatibility.VB6.ButtonArray(Me.components)
        Me.cmdCalChass_1 = New System.Windows.Forms.Button()
        Me.cmdCalChass_2 = New System.Windows.Forms.Button()
        Me.chkPpuNoFault_10 = New System.Windows.Forms.CheckBox()
        Me.chkPpuNoFault_9 = New System.Windows.Forms.CheckBox()
        Me.chkPpuNoFault_8 = New System.Windows.Forms.CheckBox()
        Me.chkPpuNoFault_7 = New System.Windows.Forms.CheckBox()
        Me.chkPpuNoFault_6 = New System.Windows.Forms.CheckBox()
        Me.chkPpuNoFault_5 = New System.Windows.Forms.CheckBox()
        Me.chkPpuNoFault_4 = New System.Windows.Forms.CheckBox()
        Me.chkPpuNoFault_3 = New System.Windows.Forms.CheckBox()
        Me.chkPpuNoFault_2 = New System.Windows.Forms.CheckBox()
        Me.chkPpuNoFault_1 = New System.Windows.Forms.CheckBox()
        Me.cmdResetChassis = New Microsoft.VisualBasic.Compatibility.VB6.ButtonArray(Me.components)
        Me.cmdResetChassis_1 = New System.Windows.Forms.Button()
        Me.cmdResetChassis_2 = New System.Windows.Forms.Button()
        Me.cmdResetPdu = New Microsoft.VisualBasic.Compatibility.VB6.ButtonArray(Me.components)
        Me.cmdResetPdu_0 = New System.Windows.Forms.Button()
        Me.ETITimer = New System.Windows.Forms.Timer(Me.components)
        Me.tmrUpdateStop = New System.Windows.Forms.Timer(Me.components)
        Me.cmdAbout = New System.Windows.Forms.Button()
        Me.cmdShutDown = New System.Windows.Forms.Button()
        Me.tmrDataPoll = New System.Windows.Forms.Timer(Me.components)
        Me.cmdQuitSysMon = New System.Windows.Forms.Button()
        Me.tabSysmon = New System.Windows.Forms.TabControl()
        Me.tabSysmon_Page1 = New System.Windows.Forms.TabPage()
        Me.fraChasTemp_2 = New System.Windows.Forms.GroupBox()
        Me.fraChasTemp_1 = New System.Windows.Forms.GroupBox()
        Me.cwsSlotRise_12 = New NationalInstruments.UI.WindowsForms.Slide()
        Me.dummySlide_1 = New NationalInstruments.UI.WindowsForms.Slide()
        Me.cwsSlotRiseActual_0 = New NationalInstruments.UI.WindowsForms.Tank()
        Me.dummySlide_0 = New NationalInstruments.UI.WindowsForms.Slide()
        Me.cwsSlotRiseActual_12 = New NationalInstruments.UI.WindowsForms.Tank()
        Me.cwsSlotRiseActual_11 = New NationalInstruments.UI.WindowsForms.Tank()
        Me.cwsSlotRiseActual_10 = New NationalInstruments.UI.WindowsForms.Tank()
        Me.cwsSlotRiseActual_9 = New NationalInstruments.UI.WindowsForms.Tank()
        Me.cwsSlotRiseActual_8 = New NationalInstruments.UI.WindowsForms.Tank()
        Me.cwsSlotRiseActual_7 = New NationalInstruments.UI.WindowsForms.Tank()
        Me.cwsSlotRiseActual_6 = New NationalInstruments.UI.WindowsForms.Tank()
        Me.cwsSlotRiseActual_5 = New NationalInstruments.UI.WindowsForms.Tank()
        Me.cwsSlotRiseActual_4 = New NationalInstruments.UI.WindowsForms.Tank()
        Me.cwsSlotRiseActual_3 = New NationalInstruments.UI.WindowsForms.Tank()
        Me.cwsSlotRiseActual_2 = New NationalInstruments.UI.WindowsForms.Tank()
        Me.cwsSlotRise_1 = New NationalInstruments.UI.WindowsForms.Slide()
        Me.cwsSlotRiseActual_1 = New NationalInstruments.UI.WindowsForms.Tank()
        Me.cwsSlotRise_11 = New NationalInstruments.UI.WindowsForms.Slide()
        Me.cwsSlotRise_10 = New NationalInstruments.UI.WindowsForms.Slide()
        Me.cwsSlotRise_9 = New NationalInstruments.UI.WindowsForms.Slide()
        Me.cwsSlotRise_8 = New NationalInstruments.UI.WindowsForms.Slide()
        Me.cwsSlotRise_7 = New NationalInstruments.UI.WindowsForms.Slide()
        Me.cwsSlotRise_6 = New NationalInstruments.UI.WindowsForms.Slide()
        Me.cwsSlotRise_5 = New NationalInstruments.UI.WindowsForms.Slide()
        Me.cwsSlotRise_4 = New NationalInstruments.UI.WindowsForms.Slide()
        Me.cwsSlotRise_3 = New NationalInstruments.UI.WindowsForms.Slide()
        Me.cwsSlotRise_2 = New NationalInstruments.UI.WindowsForms.Slide()
        Me.cwsSlotRise_0 = New NationalInstruments.UI.WindowsForms.Slide()
        Me.fraChasTemp_0 = New System.Windows.Forms.GroupBox()
        Me.tabSysmon_Page2 = New System.Windows.Forms.TabPage()
        Me.fraChasTemp_4 = New System.Windows.Forms.GroupBox()
        Me.cwsSlotRiseActual_25 = New NationalInstruments.UI.WindowsForms.Tank()
        Me.cwsSlotRiseActual_24 = New NationalInstruments.UI.WindowsForms.Tank()
        Me.cwsSlotRiseActual_23 = New NationalInstruments.UI.WindowsForms.Tank()
        Me.cwsSlotRiseActual_22 = New NationalInstruments.UI.WindowsForms.Tank()
        Me.cwsSlotRiseActual_21 = New NationalInstruments.UI.WindowsForms.Tank()
        Me.cwsSlotRiseActual_20 = New NationalInstruments.UI.WindowsForms.Tank()
        Me.cwsSlotRiseActual_19 = New NationalInstruments.UI.WindowsForms.Tank()
        Me.cwsSlotRiseActual_18 = New NationalInstruments.UI.WindowsForms.Tank()
        Me.cwsSlotRiseActual_17 = New NationalInstruments.UI.WindowsForms.Tank()
        Me.cwsSlotRiseActual_16 = New NationalInstruments.UI.WindowsForms.Tank()
        Me.cwsSlotRiseActual_15 = New NationalInstruments.UI.WindowsForms.Tank()
        Me.cwsSlotRiseActual_14 = New NationalInstruments.UI.WindowsForms.Tank()
        Me.cwsSlotRise_25 = New NationalInstruments.UI.WindowsForms.Slide()
        Me.Slide2 = New NationalInstruments.UI.WindowsForms.Slide()
        Me.cwsSlotRiseActual_13 = New NationalInstruments.UI.WindowsForms.Tank()
        Me.Slide1 = New NationalInstruments.UI.WindowsForms.Slide()
        Me.cwsSlotRise_24 = New NationalInstruments.UI.WindowsForms.Slide()
        Me.cwsSlotRise_23 = New NationalInstruments.UI.WindowsForms.Slide()
        Me.cwsSlotRise_22 = New NationalInstruments.UI.WindowsForms.Slide()
        Me.cwsSlotRise_21 = New NationalInstruments.UI.WindowsForms.Slide()
        Me.cwsSlotRise_20 = New NationalInstruments.UI.WindowsForms.Slide()
        Me.cwsSlotRise_19 = New NationalInstruments.UI.WindowsForms.Slide()
        Me.cwsSlotRise_18 = New NationalInstruments.UI.WindowsForms.Slide()
        Me.cwsSlotRise_17 = New NationalInstruments.UI.WindowsForms.Slide()
        Me.cwsSlotRise_16 = New NationalInstruments.UI.WindowsForms.Slide()
        Me.cwsSlotRise_15 = New NationalInstruments.UI.WindowsForms.Slide()
        Me.cwsSlotRise_14 = New NationalInstruments.UI.WindowsForms.Slide()
        Me.cwsSlotRise_13 = New NationalInstruments.UI.WindowsForms.Slide()
        Me.fraChasTemp_5 = New System.Windows.Forms.GroupBox()
        Me.fraChasTemp_3 = New System.Windows.Forms.GroupBox()
        Me.tabSysmon_Page3 = New System.Windows.Forms.TabPage()
        Me.fraChassisTest_2 = New System.Windows.Forms.GroupBox()
        Me.cwbChasSelfTestIndicator_2 = New NationalInstruments.UI.WindowsForms.Led()
        Me.fraFanSpeed_1 = New System.Windows.Forms.GroupBox()
        Me.panFanSpeed_1 = New System.Windows.Forms.ProgressBar()
        Me.fraHeatingUnits_1 = New System.Windows.Forms.GroupBox()
        Me.cwbHeaterIndicatorOO_3 = New NationalInstruments.UI.WindowsForms.Led()
        Me.cwbHeaterIndicatorOO_2 = New NationalInstruments.UI.WindowsForms.Led()
        Me.fraAirflowSensors_1 = New System.Windows.Forms.GroupBox()
        Me.cwbAirFlowIndicatorPF_6 = New NationalInstruments.UI.WindowsForms.Led()
        Me.cwbAirFlowIndicatorPF_5 = New NationalInstruments.UI.WindowsForms.Led()
        Me.cwbAirFlowIndicatorPF_4 = New NationalInstruments.UI.WindowsForms.Led()
        Me.fraFanSpeed_0 = New System.Windows.Forms.GroupBox()
        Me.panFanSpeed_0 = New System.Windows.Forms.ProgressBar()
        Me.fraAirflowSensors_0 = New System.Windows.Forms.GroupBox()
        Me.cwbAirFlowIndicatorPF_3 = New NationalInstruments.UI.WindowsForms.Led()
        Me.cwbAirFlowIndicatorPF_2 = New NationalInstruments.UI.WindowsForms.Led()
        Me.cwbAirFlowIndicatorPF_1 = New NationalInstruments.UI.WindowsForms.Led()
        Me.fraHeatingUnits_0 = New System.Windows.Forms.GroupBox()
        Me.cwbHeaterIndicatorOO_1 = New NationalInstruments.UI.WindowsForms.Led()
        Me.cwbHeaterIndicatorOO_0 = New NationalInstruments.UI.WindowsForms.Led()
        Me.fraChassisTest_1 = New System.Windows.Forms.GroupBox()
        Me.cwbChasSelfTestIndicator_1 = New NationalInstruments.UI.WindowsForms.Led()
        Me.tabSysmon_Page4 = New System.Windows.Forms.TabPage()
        Me.SSFrame1 = New System.Windows.Forms.GroupBox()
        Me.SSFrame2 = New System.Windows.Forms.GroupBox()
        Me.fraChassisVoltage = New System.Windows.Forms.GroupBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.fraChassisCurrent = New System.Windows.Forms.GroupBox()
        Me.tabSysmon_Page5 = New System.Windows.Forms.TabPage()
        Me.fraUutVoltage = New System.Windows.Forms.GroupBox()
        Me.fraUutCurrent = New System.Windows.Forms.GroupBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.tabSysmon_Page6 = New System.Windows.Forms.TabPage()
        Me.fraPDU_5 = New System.Windows.Forms.GroupBox()
        Me.cwb28VOk = New NationalInstruments.UI.WindowsForms.Led()
        Me.fraFanSpeed_2 = New System.Windows.Forms.GroupBox()
        Me.panPower = New System.Windows.Forms.ProgressBar()
        Me.lblPower = New System.Windows.Forms.Label()
        Me.fraMaintenance_0 = New System.Windows.Forms.GroupBox()
        Me.fraPDU_1 = New System.Windows.Forms.GroupBox()
        Me.cwbRcvrSwitch = New NationalInstruments.UI.WindowsForms.Led()
        Me.fraPDU_2 = New System.Windows.Forms.GroupBox()
        Me.cwbProbe = New NationalInstruments.UI.WindowsForms.Led()
        Me.fraPDU_4 = New System.Windows.Forms.GroupBox()
        Me.fraPDU_3 = New System.Windows.Forms.GroupBox()
        Me.cwbInputPower = New NationalInstruments.UI.WindowsForms.Led()
        Me.cwbPowerConverters = New NationalInstruments.UI.WindowsForms.Led()
        Me.cwbHamPhase_3 = New NationalInstruments.UI.WindowsForms.Led()
        Me.cwbHamPhase_2 = New NationalInstruments.UI.WindowsForms.Led()
        Me.cwbHamPhase_1 = New NationalInstruments.UI.WindowsForms.Led()
        Me.lblSourceMode = New System.Windows.Forms.Label()
        Me.fraPDU_0 = New System.Windows.Forms.GroupBox()
        Me.tabSysmon_Page7 = New System.Windows.Forms.TabPage()
        Me.fraMaintenance_7 = New System.Windows.Forms.GroupBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Line1 = New System.Windows.Forms.Label()
        Me.fraMaintenance_4 = New System.Windows.Forms.GroupBox()
        Me.Panel_2 = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.Panel_4 = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.fraMaintenance_6 = New System.Windows.Forms.GroupBox()
        Me.fraMaintenance_5 = New System.Windows.Forms.GroupBox()
        Me.fraMaintenance_1 = New System.Windows.Forms.GroupBox()
        Me.chkDataInterval = New System.Windows.Forms.CheckBox()
        Me.lblActivity = New System.Windows.Forms.Label()
        Me.fraMaintenance_2 = New System.Windows.Forms.GroupBox()
        Me.chkFpuNoFault = New System.Windows.Forms.CheckBox()
        Me.optFpuState_1 = New System.Windows.Forms.RadioButton()
        Me.optFpuState_0 = New System.Windows.Forms.RadioButton()
        Me.fraMaintenance_3 = New System.Windows.Forms.GroupBox()
        Me.Panel_1 = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.Panel_3 = New NationalInstruments.UI.WindowsForms.NumericEdit()
        Me.cmdFixEthernet = New System.Windows.Forms.Button()
        Me.lblAccount = New System.Windows.Forms.Label()
        Me.sbrUserInformation = New System.Windows.Forms.StatusBar()
        Me.sbrUserInformation_Panel1 = New System.Windows.Forms.StatusBarPanel()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        CType(Me.picIconGraphic, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picIconGraphic_1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picIconGraphic_4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picIconGraphic_3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picIconGraphic_9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picIconGraphic_5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picIconGraphic_7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picIconGraphic_8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picIconGraphic_6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picIconGraphic_0, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picIconGraphic_2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picIconGraphic_15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picIconGraphic_14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picIconGraphic_10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picIconGraphic_11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picIconGraphic_12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picIconGraphic_18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picIconGraphic_16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picIconGraphic_17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblIntakeValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblUnit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmdResetThresholds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.linRise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblChassTemp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblExhaustValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmdSaveThresholds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCst, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFanSpeedValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblHu, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPca, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLevel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBackVolt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBackCurr, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblUutVolt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblUut, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblUutCur, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSysPower, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblInputPower, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblInPower, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblmode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextBox, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmdCalChass, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmdResetChassis, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmdResetPdu, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabSysmon.SuspendLayout()
        Me.tabSysmon_Page1.SuspendLayout()
        Me.fraChasTemp_2.SuspendLayout()
        Me.fraChasTemp_1.SuspendLayout()
        CType(Me.cwsSlotRise_12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dummySlide_1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cwsSlotRiseActual_0, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dummySlide_0, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cwsSlotRiseActual_12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cwsSlotRiseActual_11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cwsSlotRiseActual_10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cwsSlotRiseActual_9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cwsSlotRiseActual_8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cwsSlotRiseActual_7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cwsSlotRiseActual_6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cwsSlotRiseActual_5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cwsSlotRiseActual_4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cwsSlotRiseActual_3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cwsSlotRiseActual_2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cwsSlotRise_1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cwsSlotRiseActual_1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cwsSlotRise_11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cwsSlotRise_10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cwsSlotRise_9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cwsSlotRise_8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cwsSlotRise_7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cwsSlotRise_6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cwsSlotRise_5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cwsSlotRise_4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cwsSlotRise_3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cwsSlotRise_2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cwsSlotRise_0, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraChasTemp_0.SuspendLayout()
        Me.tabSysmon_Page2.SuspendLayout()
        Me.fraChasTemp_4.SuspendLayout()
        CType(Me.cwsSlotRiseActual_25, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cwsSlotRiseActual_24, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cwsSlotRiseActual_23, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cwsSlotRiseActual_22, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cwsSlotRiseActual_21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cwsSlotRiseActual_20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cwsSlotRiseActual_19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cwsSlotRiseActual_18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cwsSlotRiseActual_17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cwsSlotRiseActual_16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cwsSlotRiseActual_15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cwsSlotRiseActual_14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cwsSlotRise_25, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Slide2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cwsSlotRiseActual_13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Slide1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cwsSlotRise_24, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cwsSlotRise_23, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cwsSlotRise_22, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cwsSlotRise_21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cwsSlotRise_20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cwsSlotRise_19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cwsSlotRise_18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cwsSlotRise_17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cwsSlotRise_16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cwsSlotRise_15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cwsSlotRise_14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cwsSlotRise_13, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraChasTemp_5.SuspendLayout()
        Me.fraChasTemp_3.SuspendLayout()
        Me.tabSysmon_Page3.SuspendLayout()
        Me.fraChassisTest_2.SuspendLayout()
        CType(Me.cwbChasSelfTestIndicator_2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraFanSpeed_1.SuspendLayout()
        Me.fraHeatingUnits_1.SuspendLayout()
        CType(Me.cwbHeaterIndicatorOO_3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cwbHeaterIndicatorOO_2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraAirflowSensors_1.SuspendLayout()
        CType(Me.cwbAirFlowIndicatorPF_6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cwbAirFlowIndicatorPF_5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cwbAirFlowIndicatorPF_4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraFanSpeed_0.SuspendLayout()
        Me.fraAirflowSensors_0.SuspendLayout()
        CType(Me.cwbAirFlowIndicatorPF_3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cwbAirFlowIndicatorPF_2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cwbAirFlowIndicatorPF_1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraHeatingUnits_0.SuspendLayout()
        CType(Me.cwbHeaterIndicatorOO_1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cwbHeaterIndicatorOO_0, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraChassisTest_1.SuspendLayout()
        CType(Me.cwbChasSelfTestIndicator_1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabSysmon_Page4.SuspendLayout()
        Me.SSFrame1.SuspendLayout()
        Me.SSFrame2.SuspendLayout()
        Me.fraChassisVoltage.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraChassisCurrent.SuspendLayout()
        Me.tabSysmon_Page5.SuspendLayout()
        Me.fraUutVoltage.SuspendLayout()
        Me.fraUutCurrent.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabSysmon_Page6.SuspendLayout()
        Me.fraPDU_5.SuspendLayout()
        CType(Me.cwb28VOk, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraFanSpeed_2.SuspendLayout()
        Me.fraMaintenance_0.SuspendLayout()
        Me.fraPDU_1.SuspendLayout()
        CType(Me.cwbRcvrSwitch, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraPDU_2.SuspendLayout()
        CType(Me.cwbProbe, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraPDU_4.SuspendLayout()
        Me.fraPDU_3.SuspendLayout()
        CType(Me.cwbInputPower, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cwbPowerConverters, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cwbHamPhase_3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cwbHamPhase_2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cwbHamPhase_1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraPDU_0.SuspendLayout()
        Me.tabSysmon_Page7.SuspendLayout()
        Me.fraMaintenance_7.SuspendLayout()
        Me.fraMaintenance_4.SuspendLayout()
        CType(Me.Panel_2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Panel_4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraMaintenance_6.SuspendLayout()
        Me.fraMaintenance_5.SuspendLayout()
        Me.fraMaintenance_1.SuspendLayout()
        Me.fraMaintenance_2.SuspendLayout()
        Me.fraMaintenance_3.SuspendLayout()
        CType(Me.Panel_1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Panel_3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sbrUserInformation_Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'picIconGraphic_1
        '
        Me.picIconGraphic_1.BackColor = System.Drawing.SystemColors.Control
        Me.picIconGraphic_1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.picIconGraphic_1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.picIconGraphic_1.Image = Global.sysmon.My.Resources.Resources.frmSysMon_picIconGraphic_0
        Me.picIconGraphic.SetIndex(Me.picIconGraphic_1, CType(1, Short))
        Me.picIconGraphic_1.Location = New System.Drawing.Point(142, 16)
        Me.picIconGraphic_1.Name = "picIconGraphic_1"
        Me.picIconGraphic_1.Size = New System.Drawing.Size(36, 36)
        Me.picIconGraphic_1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picIconGraphic_1.TabIndex = 137
        Me.picIconGraphic_1.TabStop = False
        '
        'picIconGraphic_4
        '
        Me.picIconGraphic_4.BackColor = System.Drawing.SystemColors.Control
        Me.picIconGraphic_4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.picIconGraphic_4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.picIconGraphic_4.Image = CType(resources.GetObject("picIconGraphic_4.Image"), System.Drawing.Image)
        Me.picIconGraphic.SetIndex(Me.picIconGraphic_4, CType(4, Short))
        Me.picIconGraphic_4.Location = New System.Drawing.Point(142, 16)
        Me.picIconGraphic_4.Name = "picIconGraphic_4"
        Me.picIconGraphic_4.Size = New System.Drawing.Size(36, 36)
        Me.picIconGraphic_4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picIconGraphic_4.TabIndex = 286
        Me.picIconGraphic_4.TabStop = False
        '
        'picIconGraphic_3
        '
        Me.picIconGraphic_3.BackColor = System.Drawing.SystemColors.Control
        Me.picIconGraphic_3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.picIconGraphic_3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.picIconGraphic_3.Image = CType(resources.GetObject("picIconGraphic_3.Image"), System.Drawing.Image)
        Me.picIconGraphic.SetIndex(Me.picIconGraphic_3, CType(3, Short))
        Me.picIconGraphic_3.Location = New System.Drawing.Point(227, 16)
        Me.picIconGraphic_3.Name = "picIconGraphic_3"
        Me.picIconGraphic_3.Size = New System.Drawing.Size(36, 36)
        Me.picIconGraphic_3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picIconGraphic_3.TabIndex = 59
        Me.picIconGraphic_3.TabStop = False
        '
        'picIconGraphic_9
        '
        Me.picIconGraphic_9.BackColor = System.Drawing.SystemColors.Control
        Me.picIconGraphic_9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.picIconGraphic_9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.picIconGraphic_9.Image = CType(resources.GetObject("picIconGraphic_9.Image"), System.Drawing.Image)
        Me.picIconGraphic.SetIndex(Me.picIconGraphic_9, CType(9, Short))
        Me.picIconGraphic_9.Location = New System.Drawing.Point(227, 20)
        Me.picIconGraphic_9.Name = "picIconGraphic_9"
        Me.picIconGraphic_9.Size = New System.Drawing.Size(36, 36)
        Me.picIconGraphic_9.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picIconGraphic_9.TabIndex = 70
        Me.picIconGraphic_9.TabStop = False
        '
        'picIconGraphic_5
        '
        Me.picIconGraphic_5.BackColor = System.Drawing.SystemColors.Control
        Me.picIconGraphic_5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.picIconGraphic_5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.picIconGraphic_5.Image = CType(resources.GetObject("picIconGraphic_5.Image"), System.Drawing.Image)
        Me.picIconGraphic.SetIndex(Me.picIconGraphic_5, CType(5, Short))
        Me.picIconGraphic_5.Location = New System.Drawing.Point(227, 36)
        Me.picIconGraphic_5.Name = "picIconGraphic_5"
        Me.picIconGraphic_5.Size = New System.Drawing.Size(36, 36)
        Me.picIconGraphic_5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picIconGraphic_5.TabIndex = 121
        Me.picIconGraphic_5.TabStop = False
        '
        'picIconGraphic_7
        '
        Me.picIconGraphic_7.BackColor = System.Drawing.SystemColors.Control
        Me.picIconGraphic_7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.picIconGraphic_7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.picIconGraphic_7.Image = CType(resources.GetObject("picIconGraphic_7.Image"), System.Drawing.Image)
        Me.picIconGraphic.SetIndex(Me.picIconGraphic_7, CType(7, Short))
        Me.picIconGraphic_7.Location = New System.Drawing.Point(227, 53)
        Me.picIconGraphic_7.Name = "picIconGraphic_7"
        Me.picIconGraphic_7.Size = New System.Drawing.Size(36, 36)
        Me.picIconGraphic_7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picIconGraphic_7.TabIndex = 68
        Me.picIconGraphic_7.TabStop = False
        '
        'picIconGraphic_8
        '
        Me.picIconGraphic_8.BackColor = System.Drawing.SystemColors.Control
        Me.picIconGraphic_8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.picIconGraphic_8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.picIconGraphic_8.Image = CType(resources.GetObject("picIconGraphic_8.Image"), System.Drawing.Image)
        Me.picIconGraphic.SetIndex(Me.picIconGraphic_8, CType(8, Short))
        Me.picIconGraphic_8.Location = New System.Drawing.Point(227, 20)
        Me.picIconGraphic_8.Name = "picIconGraphic_8"
        Me.picIconGraphic_8.Size = New System.Drawing.Size(36, 36)
        Me.picIconGraphic_8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picIconGraphic_8.TabIndex = 69
        Me.picIconGraphic_8.TabStop = False
        '
        'picIconGraphic_6
        '
        Me.picIconGraphic_6.BackColor = System.Drawing.SystemColors.Control
        Me.picIconGraphic_6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.picIconGraphic_6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.picIconGraphic_6.Image = CType(resources.GetObject("picIconGraphic_6.Image"), System.Drawing.Image)
        Me.picIconGraphic.SetIndex(Me.picIconGraphic_6, CType(6, Short))
        Me.picIconGraphic_6.Location = New System.Drawing.Point(227, 53)
        Me.picIconGraphic_6.Name = "picIconGraphic_6"
        Me.picIconGraphic_6.Size = New System.Drawing.Size(36, 36)
        Me.picIconGraphic_6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picIconGraphic_6.TabIndex = 67
        Me.picIconGraphic_6.TabStop = False
        '
        'picIconGraphic_0
        '
        Me.picIconGraphic_0.BackColor = System.Drawing.SystemColors.Control
        Me.picIconGraphic_0.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.picIconGraphic_0.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.picIconGraphic_0.Image = CType(resources.GetObject("picIconGraphic_0.Image"), System.Drawing.Image)
        Me.picIconGraphic.SetIndex(Me.picIconGraphic_0, CType(0, Short))
        Me.picIconGraphic_0.Location = New System.Drawing.Point(227, 36)
        Me.picIconGraphic_0.Name = "picIconGraphic_0"
        Me.picIconGraphic_0.Size = New System.Drawing.Size(36, 36)
        Me.picIconGraphic_0.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picIconGraphic_0.TabIndex = 120
        Me.picIconGraphic_0.TabStop = False
        '
        'picIconGraphic_2
        '
        Me.picIconGraphic_2.BackColor = System.Drawing.SystemColors.Control
        Me.picIconGraphic_2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.picIconGraphic_2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.picIconGraphic_2.Image = CType(resources.GetObject("picIconGraphic_2.Image"), System.Drawing.Image)
        Me.picIconGraphic.SetIndex(Me.picIconGraphic_2, CType(2, Short))
        Me.picIconGraphic_2.Location = New System.Drawing.Point(227, 16)
        Me.picIconGraphic_2.Name = "picIconGraphic_2"
        Me.picIconGraphic_2.Size = New System.Drawing.Size(36, 36)
        Me.picIconGraphic_2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picIconGraphic_2.TabIndex = 60
        Me.picIconGraphic_2.TabStop = False
        '
        'picIconGraphic_15
        '
        Me.picIconGraphic_15.BackColor = System.Drawing.SystemColors.Control
        Me.picIconGraphic_15.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.picIconGraphic_15.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.picIconGraphic_15.Image = CType(resources.GetObject("picIconGraphic_15.Image"), System.Drawing.Image)
        Me.picIconGraphic.SetIndex(Me.picIconGraphic_15, CType(15, Short))
        Me.picIconGraphic_15.Location = New System.Drawing.Point(506, 24)
        Me.picIconGraphic_15.Name = "picIconGraphic_15"
        Me.picIconGraphic_15.Size = New System.Drawing.Size(36, 36)
        Me.picIconGraphic_15.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picIconGraphic_15.TabIndex = 292
        Me.picIconGraphic_15.TabStop = False
        '
        'picIconGraphic_14
        '
        Me.picIconGraphic_14.BackColor = System.Drawing.SystemColors.Control
        Me.picIconGraphic_14.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.picIconGraphic_14.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.picIconGraphic_14.Image = CType(resources.GetObject("picIconGraphic_14.Image"), System.Drawing.Image)
        Me.picIconGraphic.SetIndex(Me.picIconGraphic_14, CType(14, Short))
        Me.picIconGraphic_14.Location = New System.Drawing.Point(506, 24)
        Me.picIconGraphic_14.Name = "picIconGraphic_14"
        Me.picIconGraphic_14.Size = New System.Drawing.Size(36, 36)
        Me.picIconGraphic_14.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picIconGraphic_14.TabIndex = 290
        Me.picIconGraphic_14.TabStop = False
        '
        'picIconGraphic_10
        '
        Me.picIconGraphic_10.BackColor = System.Drawing.SystemColors.Control
        Me.picIconGraphic_10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.picIconGraphic_10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.picIconGraphic_10.Image = CType(resources.GetObject("picIconGraphic_10.Image"), System.Drawing.Image)
        Me.picIconGraphic.SetIndex(Me.picIconGraphic_10, CType(10, Short))
        Me.picIconGraphic_10.Location = New System.Drawing.Point(514, 41)
        Me.picIconGraphic_10.Name = "picIconGraphic_10"
        Me.picIconGraphic_10.Size = New System.Drawing.Size(20, 20)
        Me.picIconGraphic_10.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picIconGraphic_10.TabIndex = 61
        Me.picIconGraphic_10.TabStop = False
        '
        'picIconGraphic_11
        '
        Me.picIconGraphic_11.BackColor = System.Drawing.SystemColors.Control
        Me.picIconGraphic_11.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.picIconGraphic_11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.picIconGraphic_11.Image = CType(resources.GetObject("picIconGraphic_11.Image"), System.Drawing.Image)
        Me.picIconGraphic.SetIndex(Me.picIconGraphic_11, CType(11, Short))
        Me.picIconGraphic_11.Location = New System.Drawing.Point(506, 36)
        Me.picIconGraphic_11.Name = "picIconGraphic_11"
        Me.picIconGraphic_11.Size = New System.Drawing.Size(36, 36)
        Me.picIconGraphic_11.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picIconGraphic_11.TabIndex = 62
        Me.picIconGraphic_11.TabStop = False
        '
        'picIconGraphic_12
        '
        Me.picIconGraphic_12.BackColor = System.Drawing.SystemColors.Control
        Me.picIconGraphic_12.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.picIconGraphic_12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.picIconGraphic_12.Image = Global.sysmon.My.Resources.Resources.frmSysMon_picIconGraphic_10
        Me.picIconGraphic.SetIndex(Me.picIconGraphic_12, CType(12, Short))
        Me.picIconGraphic_12.Location = New System.Drawing.Point(506, 34)
        Me.picIconGraphic_12.Name = "picIconGraphic_12"
        Me.picIconGraphic_12.Size = New System.Drawing.Size(36, 36)
        Me.picIconGraphic_12.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picIconGraphic_12.TabIndex = 63
        Me.picIconGraphic_12.TabStop = False
        '
        'picIconGraphic_18
        '
        Me.picIconGraphic_18.BackColor = System.Drawing.SystemColors.Control
        Me.picIconGraphic_18.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.picIconGraphic_18.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.picIconGraphic_18.Image = CType(resources.GetObject("picIconGraphic_18.Image"), System.Drawing.Image)
        Me.picIconGraphic.SetIndex(Me.picIconGraphic_18, CType(18, Short))
        Me.picIconGraphic_18.Location = New System.Drawing.Point(77, 24)
        Me.picIconGraphic_18.Name = "picIconGraphic_18"
        Me.picIconGraphic_18.Size = New System.Drawing.Size(36, 36)
        Me.picIconGraphic_18.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picIconGraphic_18.TabIndex = 355
        Me.picIconGraphic_18.TabStop = False
        '
        'picIconGraphic_16
        '
        Me.picIconGraphic_16.BackColor = System.Drawing.SystemColors.Control
        Me.picIconGraphic_16.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.picIconGraphic_16.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.picIconGraphic_16.Image = CType(resources.GetObject("picIconGraphic_16.Image"), System.Drawing.Image)
        Me.picIconGraphic.SetIndex(Me.picIconGraphic_16, CType(16, Short))
        Me.picIconGraphic_16.Location = New System.Drawing.Point(69, 24)
        Me.picIconGraphic_16.Name = "picIconGraphic_16"
        Me.picIconGraphic_16.Size = New System.Drawing.Size(36, 36)
        Me.picIconGraphic_16.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picIconGraphic_16.TabIndex = 65
        Me.picIconGraphic_16.TabStop = False
        '
        'picIconGraphic_17
        '
        Me.picIconGraphic_17.BackColor = System.Drawing.SystemColors.Control
        Me.picIconGraphic_17.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.picIconGraphic_17.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.picIconGraphic_17.Image = CType(resources.GetObject("picIconGraphic_17.Image"), System.Drawing.Image)
        Me.picIconGraphic.SetIndex(Me.picIconGraphic_17, CType(17, Short))
        Me.picIconGraphic_17.Location = New System.Drawing.Point(69, 24)
        Me.picIconGraphic_17.Name = "picIconGraphic_17"
        Me.picIconGraphic_17.Size = New System.Drawing.Size(36, 36)
        Me.picIconGraphic_17.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picIconGraphic_17.TabIndex = 66
        Me.picIconGraphic_17.TabStop = False
        '
        'lblIntakeValue_0
        '
        Me.lblIntakeValue_0.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblIntakeValue_0.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblIntakeValue_0.ForeColor = System.Drawing.Color.White
        Me.lblIntakeValue.SetIndex(Me.lblIntakeValue_0, CType(0, Short))
        Me.lblIntakeValue_0.Location = New System.Drawing.Point(20, 28)
        Me.lblIntakeValue_0.Name = "lblIntakeValue_0"
        Me.lblIntakeValue_0.Size = New System.Drawing.Size(33, 19)
        Me.lblIntakeValue_0.TabIndex = 136
        Me.lblIntakeValue_0.Text = "00.00"
        '
        'lblIntakeValue_1
        '
        Me.lblIntakeValue_1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblIntakeValue_1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblIntakeValue_1.ForeColor = System.Drawing.Color.White
        Me.lblIntakeValue.SetIndex(Me.lblIntakeValue_1, CType(1, Short))
        Me.lblIntakeValue_1.Location = New System.Drawing.Point(20, 28)
        Me.lblIntakeValue_1.Name = "lblIntakeValue_1"
        Me.lblIntakeValue_1.Size = New System.Drawing.Size(33, 19)
        Me.lblIntakeValue_1.TabIndex = 287
        Me.lblIntakeValue_1.Text = "00.00"
        '
        'lblUnit_7
        '
        Me.lblUnit_7.AutoSize = True
        Me.lblUnit_7.BackColor = System.Drawing.SystemColors.Control
        Me.lblUnit_7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUnit.SetIndex(Me.lblUnit_7, CType(7, Short))
        Me.lblUnit_7.Location = New System.Drawing.Point(73, 32)
        Me.lblUnit_7.Name = "lblUnit_7"
        Me.lblUnit_7.Size = New System.Drawing.Size(40, 13)
        Me.lblUnit_7.TabIndex = 31
        Me.lblUnit_7.Text = "Celsius"
        '
        'lblUnit_8
        '
        Me.lblUnit_8.BackColor = System.Drawing.SystemColors.Control
        Me.lblUnit_8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUnit.SetIndex(Me.lblUnit_8, CType(8, Short))
        Me.lblUnit_8.Location = New System.Drawing.Point(65, 28)
        Me.lblUnit_8.Name = "lblUnit_8"
        Me.lblUnit_8.Size = New System.Drawing.Size(38, 13)
        Me.lblUnit_8.TabIndex = 32
        Me.lblUnit_8.Text = "o"
        '
        'lblUnit_0
        '
        Me.lblUnit_0.AutoSize = True
        Me.lblUnit_0.BackColor = System.Drawing.SystemColors.Control
        Me.lblUnit_0.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUnit.SetIndex(Me.lblUnit_0, CType(0, Short))
        Me.lblUnit_0.Location = New System.Drawing.Point(73, 32)
        Me.lblUnit_0.Name = "lblUnit_0"
        Me.lblUnit_0.Size = New System.Drawing.Size(40, 13)
        Me.lblUnit_0.TabIndex = 288
        Me.lblUnit_0.Text = "Celsius"
        '
        'lblUnit_2
        '
        Me.lblUnit_2.BackColor = System.Drawing.SystemColors.Control
        Me.lblUnit_2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUnit.SetIndex(Me.lblUnit_2, CType(2, Short))
        Me.lblUnit_2.Location = New System.Drawing.Point(65, 28)
        Me.lblUnit_2.Name = "lblUnit_2"
        Me.lblUnit_2.Size = New System.Drawing.Size(38, 13)
        Me.lblUnit_2.TabIndex = 289
        Me.lblUnit_2.Text = "o"
        '
        'lblUnit_21
        '
        Me.lblUnit_21.AutoSize = True
        Me.lblUnit_21.BackColor = System.Drawing.SystemColors.Control
        Me.lblUnit_21.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUnit.SetIndex(Me.lblUnit_21, CType(21, Short))
        Me.lblUnit_21.Location = New System.Drawing.Point(133, 49)
        Me.lblUnit_21.Name = "lblUnit_21"
        Me.lblUnit_21.Size = New System.Drawing.Size(40, 13)
        Me.lblUnit_21.TabIndex = 387
        Me.lblUnit_21.Text = "Celsius"
        '
        'lblUnit_22
        '
        Me.lblUnit_22.BackColor = System.Drawing.SystemColors.Control
        Me.lblUnit_22.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUnit.SetIndex(Me.lblUnit_22, CType(22, Short))
        Me.lblUnit_22.Location = New System.Drawing.Point(125, 44)
        Me.lblUnit_22.Name = "lblUnit_22"
        Me.lblUnit_22.Size = New System.Drawing.Size(39, 13)
        Me.lblUnit_22.TabIndex = 388
        Me.lblUnit_22.Text = "o"
        '
        'lblUnit_18
        '
        Me.lblUnit_18.AutoSize = True
        Me.lblUnit_18.BackColor = System.Drawing.SystemColors.Control
        Me.lblUnit_18.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUnit.SetIndex(Me.lblUnit_18, CType(18, Short))
        Me.lblUnit_18.Location = New System.Drawing.Point(125, 101)
        Me.lblUnit_18.Name = "lblUnit_18"
        Me.lblUnit_18.Size = New System.Drawing.Size(15, 13)
        Me.lblUnit_18.TabIndex = 394
        Me.lblUnit_18.Text = "%"
        '
        'lblUnit_16
        '
        Me.lblUnit_16.AutoSize = True
        Me.lblUnit_16.BackColor = System.Drawing.SystemColors.Control
        Me.lblUnit_16.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUnit.SetIndex(Me.lblUnit_16, CType(16, Short))
        Me.lblUnit_16.Location = New System.Drawing.Point(7, 49)
        Me.lblUnit_16.Name = "lblUnit_16"
        Me.lblUnit_16.Size = New System.Drawing.Size(64, 13)
        Me.lblUnit_16.TabIndex = 78
        Me.lblUnit_16.Text = "Acquisition: "
        '
        'lblUnit_17
        '
        Me.lblUnit_17.AutoSize = True
        Me.lblUnit_17.BackColor = System.Drawing.SystemColors.Control
        Me.lblUnit_17.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUnit.SetIndex(Me.lblUnit_17, CType(17, Short))
        Me.lblUnit_17.Location = New System.Drawing.Point(125, 101)
        Me.lblUnit_17.Name = "lblUnit_17"
        Me.lblUnit_17.Size = New System.Drawing.Size(15, 13)
        Me.lblUnit_17.TabIndex = 376
        Me.lblUnit_17.Text = "%"
        '
        'lblUnit_19
        '
        Me.lblUnit_19.AutoSize = True
        Me.lblUnit_19.BackColor = System.Drawing.SystemColors.Control
        Me.lblUnit_19.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUnit.SetIndex(Me.lblUnit_19, CType(19, Short))
        Me.lblUnit_19.Location = New System.Drawing.Point(133, 49)
        Me.lblUnit_19.Name = "lblUnit_19"
        Me.lblUnit_19.Size = New System.Drawing.Size(40, 13)
        Me.lblUnit_19.TabIndex = 369
        Me.lblUnit_19.Text = "Celsius"
        '
        'lblUnit_20
        '
        Me.lblUnit_20.BackColor = System.Drawing.SystemColors.Control
        Me.lblUnit_20.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUnit.SetIndex(Me.lblUnit_20, CType(20, Short))
        Me.lblUnit_20.Location = New System.Drawing.Point(125, 44)
        Me.lblUnit_20.Name = "lblUnit_20"
        Me.lblUnit_20.Size = New System.Drawing.Size(39, 13)
        Me.lblUnit_20.TabIndex = 370
        Me.lblUnit_20.Text = "o"
        '
        'cmdResetThresholds
        '
        '
        'cmdResetThresholds_1
        '
        Me.cmdResetThresholds_1.BackColor = System.Drawing.SystemColors.Control
        Me.cmdResetThresholds_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdResetThresholds.SetIndex(Me.cmdResetThresholds_1, CType(1, Short))
        Me.cmdResetThresholds_1.Location = New System.Drawing.Point(457, 332)
        Me.cmdResetThresholds_1.Name = "cmdResetThresholds_1"
        Me.cmdResetThresholds_1.Size = New System.Drawing.Size(100, 23)
        Me.cmdResetThresholds_1.TabIndex = 119
        Me.cmdResetThresholds_1.Text = "&Reset Thresholds"
        Me.cmdResetThresholds_1.UseVisualStyleBackColor = False
        Me.cmdResetThresholds_1.Visible = False
        '
        'cmdResetThresholds_2
        '
        Me.cmdResetThresholds_2.BackColor = System.Drawing.SystemColors.Control
        Me.cmdResetThresholds_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdResetThresholds.SetIndex(Me.cmdResetThresholds_2, CType(2, Short))
        Me.cmdResetThresholds_2.Location = New System.Drawing.Point(457, 332)
        Me.cmdResetThresholds_2.Name = "cmdResetThresholds_2"
        Me.cmdResetThresholds_2.Size = New System.Drawing.Size(100, 23)
        Me.cmdResetThresholds_2.TabIndex = 122
        Me.cmdResetThresholds_2.Text = "&Reset Thresholds"
        Me.cmdResetThresholds_2.UseVisualStyleBackColor = False
        Me.cmdResetThresholds_2.Visible = False
        '
        'linRise_11
        '
        Me.linRise_11.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.linRise.SetIndex(Me.linRise_11, CType(11, Short))
        Me.linRise_11.Location = New System.Drawing.Point(26, 127)
        Me.linRise_11.Name = "linRise_11"
        Me.linRise_11.Size = New System.Drawing.Size(493, 1)
        Me.linRise_11.TabIndex = 325
        '
        'linRise_0
        '
        Me.linRise_0.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.linRise.SetIndex(Me.linRise_0, CType(0, Short))
        Me.linRise_0.Location = New System.Drawing.Point(28, 47)
        Me.linRise_0.Name = "linRise_0"
        Me.linRise_0.Size = New System.Drawing.Size(493, 1)
        Me.linRise_0.TabIndex = 326
        '
        'linRise_4
        '
        Me.linRise_4.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.linRise.SetIndex(Me.linRise_4, CType(4, Short))
        Me.linRise_4.Location = New System.Drawing.Point(20, 208)
        Me.linRise_4.Name = "linRise_4"
        Me.linRise_4.Size = New System.Drawing.Size(511, 1)
        Me.linRise_4.TabIndex = 327
        '
        'linRise_1
        '
        Me.linRise_1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.linRise.SetIndex(Me.linRise_1, CType(1, Short))
        Me.linRise_1.Location = New System.Drawing.Point(24, 74)
        Me.linRise_1.Name = "linRise_1"
        Me.linRise_1.Size = New System.Drawing.Size(493, 1)
        Me.linRise_1.TabIndex = 328
        '
        'linRise_2
        '
        Me.linRise_2.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.linRise.SetIndex(Me.linRise_2, CType(2, Short))
        Me.linRise_2.Location = New System.Drawing.Point(33, 100)
        Me.linRise_2.Name = "linRise_2"
        Me.linRise_2.Size = New System.Drawing.Size(493, 1)
        Me.linRise_2.TabIndex = 329
        '
        'linRise_3
        '
        Me.linRise_3.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.linRise.SetIndex(Me.linRise_3, CType(3, Short))
        Me.linRise_3.Location = New System.Drawing.Point(24, 155)
        Me.linRise_3.Name = "linRise_3"
        Me.linRise_3.Size = New System.Drawing.Size(493, 1)
        Me.linRise_3.TabIndex = 330
        '
        'linRise_10
        '
        Me.linRise_10.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.linRise.SetIndex(Me.linRise_10, CType(10, Short))
        Me.linRise_10.Location = New System.Drawing.Point(27, 181)
        Me.linRise_10.Name = "linRise_10"
        Me.linRise_10.Size = New System.Drawing.Size(493, 1)
        Me.linRise_10.TabIndex = 331
        '
        'linRise_9
        '
        Me.linRise_9.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.linRise.SetIndex(Me.linRise_9, CType(9, Short))
        Me.linRise_9.Location = New System.Drawing.Point(22, 208)
        Me.linRise_9.Name = "linRise_9"
        Me.linRise_9.Size = New System.Drawing.Size(507, 1)
        Me.linRise_9.TabIndex = 339
        '
        'linRise_8
        '
        Me.linRise_8.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.linRise.SetIndex(Me.linRise_8, CType(8, Short))
        Me.linRise_8.Location = New System.Drawing.Point(28, 47)
        Me.linRise_8.Name = "linRise_8"
        Me.linRise_8.Size = New System.Drawing.Size(493, 1)
        Me.linRise_8.TabIndex = 340
        '
        'linRise_7
        '
        Me.linRise_7.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.linRise.SetIndex(Me.linRise_7, CType(7, Short))
        Me.linRise_7.Location = New System.Drawing.Point(31, 100)
        Me.linRise_7.Name = "linRise_7"
        Me.linRise_7.Size = New System.Drawing.Size(493, 1)
        Me.linRise_7.TabIndex = 341
        '
        'linRise_6
        '
        Me.linRise_6.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.linRise.SetIndex(Me.linRise_6, CType(6, Short))
        Me.linRise_6.Location = New System.Drawing.Point(28, 127)
        Me.linRise_6.Name = "linRise_6"
        Me.linRise_6.Size = New System.Drawing.Size(493, 1)
        Me.linRise_6.TabIndex = 342
        '
        'linRise_5
        '
        Me.linRise_5.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.linRise.SetIndex(Me.linRise_5, CType(5, Short))
        Me.linRise_5.Location = New System.Drawing.Point(24, 74)
        Me.linRise_5.Name = "linRise_5"
        Me.linRise_5.Size = New System.Drawing.Size(493, 1)
        Me.linRise_5.TabIndex = 343
        '
        'linRise_12
        '
        Me.linRise_12.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.linRise.SetIndex(Me.linRise_12, CType(12, Short))
        Me.linRise_12.Location = New System.Drawing.Point(28, 155)
        Me.linRise_12.Name = "linRise_12"
        Me.linRise_12.Size = New System.Drawing.Size(493, 1)
        Me.linRise_12.TabIndex = 352
        '
        'linRise_13
        '
        Me.linRise_13.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.linRise.SetIndex(Me.linRise_13, CType(13, Short))
        Me.linRise_13.Location = New System.Drawing.Point(27, 181)
        Me.linRise_13.Name = "linRise_13"
        Me.linRise_13.Size = New System.Drawing.Size(493, 1)
        Me.linRise_13.TabIndex = 353
        '
        'lblChassTemp_25
        '
        Me.lblChassTemp_25.AutoSize = True
        Me.lblChassTemp_25.BackColor = System.Drawing.SystemColors.Control
        Me.lblChassTemp_25.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChassTemp_25.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblChassTemp.SetIndex(Me.lblChassTemp_25, CType(25, Short))
        Me.lblChassTemp_25.Location = New System.Drawing.Point(506, 24)
        Me.lblChassTemp_25.Name = "lblChassTemp_25"
        Me.lblChassTemp_25.Size = New System.Drawing.Size(33, 12)
        Me.lblChassTemp_25.TabIndex = 104
        Me.lblChassTemp_25.Text = "Slot 12"
        '
        'lblChassTemp_24
        '
        Me.lblChassTemp_24.AutoSize = True
        Me.lblChassTemp_24.BackColor = System.Drawing.SystemColors.Control
        Me.lblChassTemp_24.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChassTemp_24.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblChassTemp.SetIndex(Me.lblChassTemp_24, CType(24, Short))
        Me.lblChassTemp_24.Location = New System.Drawing.Point(465, 24)
        Me.lblChassTemp_24.Name = "lblChassTemp_24"
        Me.lblChassTemp_24.Size = New System.Drawing.Size(33, 12)
        Me.lblChassTemp_24.TabIndex = 103
        Me.lblChassTemp_24.Text = "Slot 11"
        '
        'lblChassTemp_23
        '
        Me.lblChassTemp_23.AutoSize = True
        Me.lblChassTemp_23.BackColor = System.Drawing.SystemColors.Control
        Me.lblChassTemp_23.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChassTemp_23.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblChassTemp.SetIndex(Me.lblChassTemp_23, CType(23, Short))
        Me.lblChassTemp_23.Location = New System.Drawing.Point(425, 24)
        Me.lblChassTemp_23.Name = "lblChassTemp_23"
        Me.lblChassTemp_23.Size = New System.Drawing.Size(33, 12)
        Me.lblChassTemp_23.TabIndex = 102
        Me.lblChassTemp_23.Text = "Slot 10"
        '
        'lblChassTemp_22
        '
        Me.lblChassTemp_22.AutoSize = True
        Me.lblChassTemp_22.BackColor = System.Drawing.SystemColors.Control
        Me.lblChassTemp_22.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChassTemp_22.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblChassTemp.SetIndex(Me.lblChassTemp_22, CType(22, Short))
        Me.lblChassTemp_22.Location = New System.Drawing.Point(384, 24)
        Me.lblChassTemp_22.Name = "lblChassTemp_22"
        Me.lblChassTemp_22.Size = New System.Drawing.Size(28, 12)
        Me.lblChassTemp_22.TabIndex = 101
        Me.lblChassTemp_22.Text = "Slot 9"
        '
        'lblChassTemp_21
        '
        Me.lblChassTemp_21.AutoSize = True
        Me.lblChassTemp_21.BackColor = System.Drawing.SystemColors.Control
        Me.lblChassTemp_21.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChassTemp_21.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblChassTemp.SetIndex(Me.lblChassTemp_21, CType(21, Short))
        Me.lblChassTemp_21.Location = New System.Drawing.Point(344, 24)
        Me.lblChassTemp_21.Name = "lblChassTemp_21"
        Me.lblChassTemp_21.Size = New System.Drawing.Size(28, 12)
        Me.lblChassTemp_21.TabIndex = 100
        Me.lblChassTemp_21.Text = "Slot 8"
        '
        'lblChassTemp_20
        '
        Me.lblChassTemp_20.AutoSize = True
        Me.lblChassTemp_20.BackColor = System.Drawing.SystemColors.Control
        Me.lblChassTemp_20.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChassTemp_20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblChassTemp.SetIndex(Me.lblChassTemp_20, CType(20, Short))
        Me.lblChassTemp_20.Location = New System.Drawing.Point(303, 24)
        Me.lblChassTemp_20.Name = "lblChassTemp_20"
        Me.lblChassTemp_20.Size = New System.Drawing.Size(28, 12)
        Me.lblChassTemp_20.TabIndex = 99
        Me.lblChassTemp_20.Text = "Slot 7"
        '
        'lblChassTemp_19
        '
        Me.lblChassTemp_19.AutoSize = True
        Me.lblChassTemp_19.BackColor = System.Drawing.SystemColors.Control
        Me.lblChassTemp_19.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChassTemp_19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblChassTemp.SetIndex(Me.lblChassTemp_19, CType(19, Short))
        Me.lblChassTemp_19.Location = New System.Drawing.Point(263, 24)
        Me.lblChassTemp_19.Name = "lblChassTemp_19"
        Me.lblChassTemp_19.Size = New System.Drawing.Size(28, 12)
        Me.lblChassTemp_19.TabIndex = 98
        Me.lblChassTemp_19.Text = "Slot 6"
        '
        'lblChassTemp_18
        '
        Me.lblChassTemp_18.AutoSize = True
        Me.lblChassTemp_18.BackColor = System.Drawing.SystemColors.Control
        Me.lblChassTemp_18.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChassTemp_18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblChassTemp.SetIndex(Me.lblChassTemp_18, CType(18, Short))
        Me.lblChassTemp_18.Location = New System.Drawing.Point(222, 24)
        Me.lblChassTemp_18.Name = "lblChassTemp_18"
        Me.lblChassTemp_18.Size = New System.Drawing.Size(28, 12)
        Me.lblChassTemp_18.TabIndex = 97
        Me.lblChassTemp_18.Text = "Slot 5"
        '
        'lblChassTemp_17
        '
        Me.lblChassTemp_17.AutoSize = True
        Me.lblChassTemp_17.BackColor = System.Drawing.SystemColors.Control
        Me.lblChassTemp_17.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChassTemp_17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblChassTemp.SetIndex(Me.lblChassTemp_17, CType(17, Short))
        Me.lblChassTemp_17.Location = New System.Drawing.Point(182, 24)
        Me.lblChassTemp_17.Name = "lblChassTemp_17"
        Me.lblChassTemp_17.Size = New System.Drawing.Size(28, 12)
        Me.lblChassTemp_17.TabIndex = 96
        Me.lblChassTemp_17.Text = "Slot 4"
        '
        'lblChassTemp_16
        '
        Me.lblChassTemp_16.AutoSize = True
        Me.lblChassTemp_16.BackColor = System.Drawing.SystemColors.Control
        Me.lblChassTemp_16.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChassTemp_16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblChassTemp.SetIndex(Me.lblChassTemp_16, CType(16, Short))
        Me.lblChassTemp_16.Location = New System.Drawing.Point(142, 24)
        Me.lblChassTemp_16.Name = "lblChassTemp_16"
        Me.lblChassTemp_16.Size = New System.Drawing.Size(28, 12)
        Me.lblChassTemp_16.TabIndex = 95
        Me.lblChassTemp_16.Text = "Slot 3"
        '
        'lblChassTemp_15
        '
        Me.lblChassTemp_15.AutoSize = True
        Me.lblChassTemp_15.BackColor = System.Drawing.SystemColors.Control
        Me.lblChassTemp_15.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChassTemp_15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblChassTemp.SetIndex(Me.lblChassTemp_15, CType(15, Short))
        Me.lblChassTemp_15.Location = New System.Drawing.Point(100, 24)
        Me.lblChassTemp_15.Name = "lblChassTemp_15"
        Me.lblChassTemp_15.Size = New System.Drawing.Size(28, 12)
        Me.lblChassTemp_15.TabIndex = 94
        Me.lblChassTemp_15.Text = "Slot 2"
        '
        'lblChassTemp_14
        '
        Me.lblChassTemp_14.AutoSize = True
        Me.lblChassTemp_14.BackColor = System.Drawing.SystemColors.Control
        Me.lblChassTemp_14.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChassTemp_14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblChassTemp.SetIndex(Me.lblChassTemp_14, CType(14, Short))
        Me.lblChassTemp_14.Location = New System.Drawing.Point(61, 24)
        Me.lblChassTemp_14.Name = "lblChassTemp_14"
        Me.lblChassTemp_14.Size = New System.Drawing.Size(28, 12)
        Me.lblChassTemp_14.TabIndex = 93
        Me.lblChassTemp_14.Text = "Slot 1"
        '
        'lblChassTemp_13
        '
        Me.lblChassTemp_13.AutoSize = True
        Me.lblChassTemp_13.BackColor = System.Drawing.SystemColors.Control
        Me.lblChassTemp_13.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChassTemp_13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblChassTemp.SetIndex(Me.lblChassTemp_13, CType(13, Short))
        Me.lblChassTemp_13.Location = New System.Drawing.Point(20, 24)
        Me.lblChassTemp_13.Name = "lblChassTemp_13"
        Me.lblChassTemp_13.Size = New System.Drawing.Size(28, 12)
        Me.lblChassTemp_13.TabIndex = 92
        Me.lblChassTemp_13.Text = "Slot 0"
        '
        'lblChassTemp_0
        '
        Me.lblChassTemp_0.AutoSize = True
        Me.lblChassTemp_0.BackColor = System.Drawing.SystemColors.Control
        Me.lblChassTemp_0.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChassTemp_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblChassTemp.SetIndex(Me.lblChassTemp_0, CType(0, Short))
        Me.lblChassTemp_0.Location = New System.Drawing.Point(16, 25)
        Me.lblChassTemp_0.Name = "lblChassTemp_0"
        Me.lblChassTemp_0.Size = New System.Drawing.Size(28, 12)
        Me.lblChassTemp_0.TabIndex = 118
        Me.lblChassTemp_0.Text = "Slot 0"
        '
        'lblChassTemp_1
        '
        Me.lblChassTemp_1.AutoSize = True
        Me.lblChassTemp_1.BackColor = System.Drawing.SystemColors.Control
        Me.lblChassTemp_1.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChassTemp_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblChassTemp.SetIndex(Me.lblChassTemp_1, CType(1, Short))
        Me.lblChassTemp_1.Location = New System.Drawing.Point(58, 25)
        Me.lblChassTemp_1.Name = "lblChassTemp_1"
        Me.lblChassTemp_1.Size = New System.Drawing.Size(28, 12)
        Me.lblChassTemp_1.TabIndex = 117
        Me.lblChassTemp_1.Text = "Slot 1"
        '
        'lblChassTemp_2
        '
        Me.lblChassTemp_2.AutoSize = True
        Me.lblChassTemp_2.BackColor = System.Drawing.SystemColors.Control
        Me.lblChassTemp_2.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChassTemp_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblChassTemp.SetIndex(Me.lblChassTemp_2, CType(2, Short))
        Me.lblChassTemp_2.Location = New System.Drawing.Point(97, 25)
        Me.lblChassTemp_2.Name = "lblChassTemp_2"
        Me.lblChassTemp_2.Size = New System.Drawing.Size(28, 12)
        Me.lblChassTemp_2.TabIndex = 116
        Me.lblChassTemp_2.Text = "Slot 2"
        '
        'lblChassTemp_3
        '
        Me.lblChassTemp_3.AutoSize = True
        Me.lblChassTemp_3.BackColor = System.Drawing.SystemColors.Control
        Me.lblChassTemp_3.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChassTemp_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblChassTemp.SetIndex(Me.lblChassTemp_3, CType(3, Short))
        Me.lblChassTemp_3.Location = New System.Drawing.Point(138, 25)
        Me.lblChassTemp_3.Name = "lblChassTemp_3"
        Me.lblChassTemp_3.Size = New System.Drawing.Size(28, 12)
        Me.lblChassTemp_3.TabIndex = 115
        Me.lblChassTemp_3.Text = "Slot 3"
        '
        'lblChassTemp_4
        '
        Me.lblChassTemp_4.AutoSize = True
        Me.lblChassTemp_4.BackColor = System.Drawing.SystemColors.Control
        Me.lblChassTemp_4.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChassTemp_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblChassTemp.SetIndex(Me.lblChassTemp_4, CType(4, Short))
        Me.lblChassTemp_4.Location = New System.Drawing.Point(178, 25)
        Me.lblChassTemp_4.Name = "lblChassTemp_4"
        Me.lblChassTemp_4.Size = New System.Drawing.Size(28, 12)
        Me.lblChassTemp_4.TabIndex = 114
        Me.lblChassTemp_4.Text = "Slot 4"
        '
        'lblChassTemp_5
        '
        Me.lblChassTemp_5.AutoSize = True
        Me.lblChassTemp_5.BackColor = System.Drawing.SystemColors.Control
        Me.lblChassTemp_5.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChassTemp_5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblChassTemp.SetIndex(Me.lblChassTemp_5, CType(5, Short))
        Me.lblChassTemp_5.Location = New System.Drawing.Point(218, 25)
        Me.lblChassTemp_5.Name = "lblChassTemp_5"
        Me.lblChassTemp_5.Size = New System.Drawing.Size(28, 12)
        Me.lblChassTemp_5.TabIndex = 113
        Me.lblChassTemp_5.Text = "Slot 5"
        '
        'lblChassTemp_6
        '
        Me.lblChassTemp_6.AutoSize = True
        Me.lblChassTemp_6.BackColor = System.Drawing.SystemColors.Control
        Me.lblChassTemp_6.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChassTemp_6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblChassTemp.SetIndex(Me.lblChassTemp_6, CType(6, Short))
        Me.lblChassTemp_6.Location = New System.Drawing.Point(259, 25)
        Me.lblChassTemp_6.Name = "lblChassTemp_6"
        Me.lblChassTemp_6.Size = New System.Drawing.Size(28, 12)
        Me.lblChassTemp_6.TabIndex = 112
        Me.lblChassTemp_6.Text = "Slot 6"
        '
        'lblChassTemp_7
        '
        Me.lblChassTemp_7.AutoSize = True
        Me.lblChassTemp_7.BackColor = System.Drawing.SystemColors.Control
        Me.lblChassTemp_7.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChassTemp_7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblChassTemp.SetIndex(Me.lblChassTemp_7, CType(7, Short))
        Me.lblChassTemp_7.Location = New System.Drawing.Point(300, 25)
        Me.lblChassTemp_7.Name = "lblChassTemp_7"
        Me.lblChassTemp_7.Size = New System.Drawing.Size(28, 12)
        Me.lblChassTemp_7.TabIndex = 111
        Me.lblChassTemp_7.Text = "Slot 7"
        '
        'lblChassTemp_8
        '
        Me.lblChassTemp_8.AutoSize = True
        Me.lblChassTemp_8.BackColor = System.Drawing.SystemColors.Control
        Me.lblChassTemp_8.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChassTemp_8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblChassTemp.SetIndex(Me.lblChassTemp_8, CType(8, Short))
        Me.lblChassTemp_8.Location = New System.Drawing.Point(340, 25)
        Me.lblChassTemp_8.Name = "lblChassTemp_8"
        Me.lblChassTemp_8.Size = New System.Drawing.Size(28, 12)
        Me.lblChassTemp_8.TabIndex = 110
        Me.lblChassTemp_8.Text = "Slot 8"
        '
        'lblChassTemp_9
        '
        Me.lblChassTemp_9.AutoSize = True
        Me.lblChassTemp_9.BackColor = System.Drawing.SystemColors.Control
        Me.lblChassTemp_9.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChassTemp_9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblChassTemp.SetIndex(Me.lblChassTemp_9, CType(9, Short))
        Me.lblChassTemp_9.Location = New System.Drawing.Point(380, 25)
        Me.lblChassTemp_9.Name = "lblChassTemp_9"
        Me.lblChassTemp_9.Size = New System.Drawing.Size(28, 12)
        Me.lblChassTemp_9.TabIndex = 109
        Me.lblChassTemp_9.Text = "Slot 9"
        '
        'lblChassTemp_10
        '
        Me.lblChassTemp_10.AutoSize = True
        Me.lblChassTemp_10.BackColor = System.Drawing.SystemColors.Control
        Me.lblChassTemp_10.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChassTemp_10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblChassTemp.SetIndex(Me.lblChassTemp_10, CType(10, Short))
        Me.lblChassTemp_10.Location = New System.Drawing.Point(421, 25)
        Me.lblChassTemp_10.Name = "lblChassTemp_10"
        Me.lblChassTemp_10.Size = New System.Drawing.Size(33, 12)
        Me.lblChassTemp_10.TabIndex = 108
        Me.lblChassTemp_10.Text = "Slot 10"
        '
        'lblChassTemp_11
        '
        Me.lblChassTemp_11.AutoSize = True
        Me.lblChassTemp_11.BackColor = System.Drawing.SystemColors.Control
        Me.lblChassTemp_11.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChassTemp_11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblChassTemp.SetIndex(Me.lblChassTemp_11, CType(11, Short))
        Me.lblChassTemp_11.Location = New System.Drawing.Point(460, 25)
        Me.lblChassTemp_11.Name = "lblChassTemp_11"
        Me.lblChassTemp_11.Size = New System.Drawing.Size(33, 12)
        Me.lblChassTemp_11.TabIndex = 107
        Me.lblChassTemp_11.Text = "Slot 11"
        '
        'lblChassTemp_12
        '
        Me.lblChassTemp_12.AutoSize = True
        Me.lblChassTemp_12.BackColor = System.Drawing.SystemColors.Control
        Me.lblChassTemp_12.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChassTemp_12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblChassTemp.SetIndex(Me.lblChassTemp_12, CType(12, Short))
        Me.lblChassTemp_12.Location = New System.Drawing.Point(502, 25)
        Me.lblChassTemp_12.Name = "lblChassTemp_12"
        Me.lblChassTemp_12.Size = New System.Drawing.Size(33, 12)
        Me.lblChassTemp_12.TabIndex = 106
        Me.lblChassTemp_12.Text = "Slot 12"
        '
        'lblChassTemp_51
        '
        Me.lblChassTemp_51.AutoSize = True
        Me.lblChassTemp_51.BackColor = System.Drawing.SystemColors.Control
        Me.lblChassTemp_51.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChassTemp_51.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblChassTemp.SetIndex(Me.lblChassTemp_51, CType(51, Short))
        Me.lblChassTemp_51.Location = New System.Drawing.Point(20, 24)
        Me.lblChassTemp_51.Name = "lblChassTemp_51"
        Me.lblChassTemp_51.Size = New System.Drawing.Size(28, 12)
        Me.lblChassTemp_51.TabIndex = 351
        Me.lblChassTemp_51.Text = "Slot 0"
        '
        'lblChassTemp_50
        '
        Me.lblChassTemp_50.AutoSize = True
        Me.lblChassTemp_50.BackColor = System.Drawing.SystemColors.Control
        Me.lblChassTemp_50.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChassTemp_50.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblChassTemp.SetIndex(Me.lblChassTemp_50, CType(50, Short))
        Me.lblChassTemp_50.Location = New System.Drawing.Point(61, 24)
        Me.lblChassTemp_50.Name = "lblChassTemp_50"
        Me.lblChassTemp_50.Size = New System.Drawing.Size(28, 12)
        Me.lblChassTemp_50.TabIndex = 350
        Me.lblChassTemp_50.Text = "Slot 1"
        '
        'lblChassTemp_49
        '
        Me.lblChassTemp_49.AutoSize = True
        Me.lblChassTemp_49.BackColor = System.Drawing.SystemColors.Control
        Me.lblChassTemp_49.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChassTemp_49.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblChassTemp.SetIndex(Me.lblChassTemp_49, CType(49, Short))
        Me.lblChassTemp_49.Location = New System.Drawing.Point(100, 24)
        Me.lblChassTemp_49.Name = "lblChassTemp_49"
        Me.lblChassTemp_49.Size = New System.Drawing.Size(28, 12)
        Me.lblChassTemp_49.TabIndex = 349
        Me.lblChassTemp_49.Text = "Slot 2"
        '
        'lblChassTemp_48
        '
        Me.lblChassTemp_48.AutoSize = True
        Me.lblChassTemp_48.BackColor = System.Drawing.SystemColors.Control
        Me.lblChassTemp_48.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChassTemp_48.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblChassTemp.SetIndex(Me.lblChassTemp_48, CType(48, Short))
        Me.lblChassTemp_48.Location = New System.Drawing.Point(142, 24)
        Me.lblChassTemp_48.Name = "lblChassTemp_48"
        Me.lblChassTemp_48.Size = New System.Drawing.Size(28, 12)
        Me.lblChassTemp_48.TabIndex = 348
        Me.lblChassTemp_48.Text = "Slot 3"
        '
        'lblChassTemp_47
        '
        Me.lblChassTemp_47.AutoSize = True
        Me.lblChassTemp_47.BackColor = System.Drawing.SystemColors.Control
        Me.lblChassTemp_47.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChassTemp_47.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblChassTemp.SetIndex(Me.lblChassTemp_47, CType(47, Short))
        Me.lblChassTemp_47.Location = New System.Drawing.Point(182, 24)
        Me.lblChassTemp_47.Name = "lblChassTemp_47"
        Me.lblChassTemp_47.Size = New System.Drawing.Size(28, 12)
        Me.lblChassTemp_47.TabIndex = 347
        Me.lblChassTemp_47.Text = "Slot 4"
        '
        'lblChassTemp_46
        '
        Me.lblChassTemp_46.AutoSize = True
        Me.lblChassTemp_46.BackColor = System.Drawing.SystemColors.Control
        Me.lblChassTemp_46.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChassTemp_46.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblChassTemp.SetIndex(Me.lblChassTemp_46, CType(46, Short))
        Me.lblChassTemp_46.Location = New System.Drawing.Point(222, 24)
        Me.lblChassTemp_46.Name = "lblChassTemp_46"
        Me.lblChassTemp_46.Size = New System.Drawing.Size(28, 12)
        Me.lblChassTemp_46.TabIndex = 346
        Me.lblChassTemp_46.Text = "Slot 5"
        '
        'lblChassTemp_45
        '
        Me.lblChassTemp_45.AutoSize = True
        Me.lblChassTemp_45.BackColor = System.Drawing.SystemColors.Control
        Me.lblChassTemp_45.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChassTemp_45.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblChassTemp.SetIndex(Me.lblChassTemp_45, CType(45, Short))
        Me.lblChassTemp_45.Location = New System.Drawing.Point(263, 24)
        Me.lblChassTemp_45.Name = "lblChassTemp_45"
        Me.lblChassTemp_45.Size = New System.Drawing.Size(28, 12)
        Me.lblChassTemp_45.TabIndex = 345
        Me.lblChassTemp_45.Text = "Slot 6"
        '
        'lblChassTemp_44
        '
        Me.lblChassTemp_44.AutoSize = True
        Me.lblChassTemp_44.BackColor = System.Drawing.SystemColors.Control
        Me.lblChassTemp_44.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChassTemp_44.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblChassTemp.SetIndex(Me.lblChassTemp_44, CType(44, Short))
        Me.lblChassTemp_44.Location = New System.Drawing.Point(303, 24)
        Me.lblChassTemp_44.Name = "lblChassTemp_44"
        Me.lblChassTemp_44.Size = New System.Drawing.Size(28, 12)
        Me.lblChassTemp_44.TabIndex = 344
        Me.lblChassTemp_44.Text = "Slot 7"
        '
        'lblChassTemp_43
        '
        Me.lblChassTemp_43.AutoSize = True
        Me.lblChassTemp_43.BackColor = System.Drawing.SystemColors.Control
        Me.lblChassTemp_43.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChassTemp_43.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblChassTemp.SetIndex(Me.lblChassTemp_43, CType(43, Short))
        Me.lblChassTemp_43.Location = New System.Drawing.Point(344, 24)
        Me.lblChassTemp_43.Name = "lblChassTemp_43"
        Me.lblChassTemp_43.Size = New System.Drawing.Size(28, 12)
        Me.lblChassTemp_43.TabIndex = 343
        Me.lblChassTemp_43.Text = "Slot 8"
        '
        'lblChassTemp_42
        '
        Me.lblChassTemp_42.AutoSize = True
        Me.lblChassTemp_42.BackColor = System.Drawing.SystemColors.Control
        Me.lblChassTemp_42.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChassTemp_42.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblChassTemp.SetIndex(Me.lblChassTemp_42, CType(42, Short))
        Me.lblChassTemp_42.Location = New System.Drawing.Point(384, 24)
        Me.lblChassTemp_42.Name = "lblChassTemp_42"
        Me.lblChassTemp_42.Size = New System.Drawing.Size(28, 12)
        Me.lblChassTemp_42.TabIndex = 342
        Me.lblChassTemp_42.Text = "Slot 9"
        '
        'lblChassTemp_41
        '
        Me.lblChassTemp_41.AutoSize = True
        Me.lblChassTemp_41.BackColor = System.Drawing.SystemColors.Control
        Me.lblChassTemp_41.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChassTemp_41.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblChassTemp.SetIndex(Me.lblChassTemp_41, CType(41, Short))
        Me.lblChassTemp_41.Location = New System.Drawing.Point(425, 24)
        Me.lblChassTemp_41.Name = "lblChassTemp_41"
        Me.lblChassTemp_41.Size = New System.Drawing.Size(33, 12)
        Me.lblChassTemp_41.TabIndex = 341
        Me.lblChassTemp_41.Text = "Slot 10"
        '
        'lblChassTemp_40
        '
        Me.lblChassTemp_40.AutoSize = True
        Me.lblChassTemp_40.BackColor = System.Drawing.SystemColors.Control
        Me.lblChassTemp_40.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChassTemp_40.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblChassTemp.SetIndex(Me.lblChassTemp_40, CType(40, Short))
        Me.lblChassTemp_40.Location = New System.Drawing.Point(465, 24)
        Me.lblChassTemp_40.Name = "lblChassTemp_40"
        Me.lblChassTemp_40.Size = New System.Drawing.Size(33, 12)
        Me.lblChassTemp_40.TabIndex = 340
        Me.lblChassTemp_40.Text = "Slot 11"
        '
        'lblChassTemp_39
        '
        Me.lblChassTemp_39.AutoSize = True
        Me.lblChassTemp_39.BackColor = System.Drawing.SystemColors.Control
        Me.lblChassTemp_39.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChassTemp_39.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblChassTemp.SetIndex(Me.lblChassTemp_39, CType(39, Short))
        Me.lblChassTemp_39.Location = New System.Drawing.Point(506, 24)
        Me.lblChassTemp_39.Name = "lblChassTemp_39"
        Me.lblChassTemp_39.Size = New System.Drawing.Size(33, 12)
        Me.lblChassTemp_39.TabIndex = 339
        Me.lblChassTemp_39.Text = "Slot 12"
        '
        'lblChassTemp_38
        '
        Me.lblChassTemp_38.AutoSize = True
        Me.lblChassTemp_38.BackColor = System.Drawing.SystemColors.Control
        Me.lblChassTemp_38.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChassTemp_38.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblChassTemp.SetIndex(Me.lblChassTemp_38, CType(38, Short))
        Me.lblChassTemp_38.Location = New System.Drawing.Point(502, 25)
        Me.lblChassTemp_38.Name = "lblChassTemp_38"
        Me.lblChassTemp_38.Size = New System.Drawing.Size(33, 12)
        Me.lblChassTemp_38.TabIndex = 284
        Me.lblChassTemp_38.Text = "Slot 12"
        '
        'lblChassTemp_37
        '
        Me.lblChassTemp_37.AutoSize = True
        Me.lblChassTemp_37.BackColor = System.Drawing.SystemColors.Control
        Me.lblChassTemp_37.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChassTemp_37.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblChassTemp.SetIndex(Me.lblChassTemp_37, CType(37, Short))
        Me.lblChassTemp_37.Location = New System.Drawing.Point(460, 25)
        Me.lblChassTemp_37.Name = "lblChassTemp_37"
        Me.lblChassTemp_37.Size = New System.Drawing.Size(33, 12)
        Me.lblChassTemp_37.TabIndex = 283
        Me.lblChassTemp_37.Text = "Slot 11"
        '
        'lblChassTemp_36
        '
        Me.lblChassTemp_36.AutoSize = True
        Me.lblChassTemp_36.BackColor = System.Drawing.SystemColors.Control
        Me.lblChassTemp_36.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChassTemp_36.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblChassTemp.SetIndex(Me.lblChassTemp_36, CType(36, Short))
        Me.lblChassTemp_36.Location = New System.Drawing.Point(421, 25)
        Me.lblChassTemp_36.Name = "lblChassTemp_36"
        Me.lblChassTemp_36.Size = New System.Drawing.Size(33, 12)
        Me.lblChassTemp_36.TabIndex = 282
        Me.lblChassTemp_36.Text = "Slot 10"
        '
        'lblChassTemp_35
        '
        Me.lblChassTemp_35.AutoSize = True
        Me.lblChassTemp_35.BackColor = System.Drawing.SystemColors.Control
        Me.lblChassTemp_35.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChassTemp_35.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblChassTemp.SetIndex(Me.lblChassTemp_35, CType(35, Short))
        Me.lblChassTemp_35.Location = New System.Drawing.Point(380, 25)
        Me.lblChassTemp_35.Name = "lblChassTemp_35"
        Me.lblChassTemp_35.Size = New System.Drawing.Size(28, 12)
        Me.lblChassTemp_35.TabIndex = 281
        Me.lblChassTemp_35.Text = "Slot 9"
        '
        'lblChassTemp_34
        '
        Me.lblChassTemp_34.AutoSize = True
        Me.lblChassTemp_34.BackColor = System.Drawing.SystemColors.Control
        Me.lblChassTemp_34.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChassTemp_34.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblChassTemp.SetIndex(Me.lblChassTemp_34, CType(34, Short))
        Me.lblChassTemp_34.Location = New System.Drawing.Point(340, 25)
        Me.lblChassTemp_34.Name = "lblChassTemp_34"
        Me.lblChassTemp_34.Size = New System.Drawing.Size(28, 12)
        Me.lblChassTemp_34.TabIndex = 280
        Me.lblChassTemp_34.Text = "Slot 8"
        '
        'lblChassTemp_33
        '
        Me.lblChassTemp_33.AutoSize = True
        Me.lblChassTemp_33.BackColor = System.Drawing.SystemColors.Control
        Me.lblChassTemp_33.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChassTemp_33.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblChassTemp.SetIndex(Me.lblChassTemp_33, CType(33, Short))
        Me.lblChassTemp_33.Location = New System.Drawing.Point(300, 25)
        Me.lblChassTemp_33.Name = "lblChassTemp_33"
        Me.lblChassTemp_33.Size = New System.Drawing.Size(28, 12)
        Me.lblChassTemp_33.TabIndex = 279
        Me.lblChassTemp_33.Text = "Slot 7"
        '
        'lblChassTemp_32
        '
        Me.lblChassTemp_32.AutoSize = True
        Me.lblChassTemp_32.BackColor = System.Drawing.SystemColors.Control
        Me.lblChassTemp_32.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChassTemp_32.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblChassTemp.SetIndex(Me.lblChassTemp_32, CType(32, Short))
        Me.lblChassTemp_32.Location = New System.Drawing.Point(259, 25)
        Me.lblChassTemp_32.Name = "lblChassTemp_32"
        Me.lblChassTemp_32.Size = New System.Drawing.Size(28, 12)
        Me.lblChassTemp_32.TabIndex = 278
        Me.lblChassTemp_32.Text = "Slot 6"
        '
        'lblChassTemp_31
        '
        Me.lblChassTemp_31.AutoSize = True
        Me.lblChassTemp_31.BackColor = System.Drawing.SystemColors.Control
        Me.lblChassTemp_31.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChassTemp_31.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblChassTemp.SetIndex(Me.lblChassTemp_31, CType(31, Short))
        Me.lblChassTemp_31.Location = New System.Drawing.Point(218, 25)
        Me.lblChassTemp_31.Name = "lblChassTemp_31"
        Me.lblChassTemp_31.Size = New System.Drawing.Size(28, 12)
        Me.lblChassTemp_31.TabIndex = 277
        Me.lblChassTemp_31.Text = "Slot 5"
        '
        'lblChassTemp_30
        '
        Me.lblChassTemp_30.AutoSize = True
        Me.lblChassTemp_30.BackColor = System.Drawing.SystemColors.Control
        Me.lblChassTemp_30.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChassTemp_30.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblChassTemp.SetIndex(Me.lblChassTemp_30, CType(30, Short))
        Me.lblChassTemp_30.Location = New System.Drawing.Point(178, 25)
        Me.lblChassTemp_30.Name = "lblChassTemp_30"
        Me.lblChassTemp_30.Size = New System.Drawing.Size(28, 12)
        Me.lblChassTemp_30.TabIndex = 276
        Me.lblChassTemp_30.Text = "Slot 4"
        '
        'lblChassTemp_29
        '
        Me.lblChassTemp_29.AutoSize = True
        Me.lblChassTemp_29.BackColor = System.Drawing.SystemColors.Control
        Me.lblChassTemp_29.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChassTemp_29.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblChassTemp.SetIndex(Me.lblChassTemp_29, CType(29, Short))
        Me.lblChassTemp_29.Location = New System.Drawing.Point(138, 25)
        Me.lblChassTemp_29.Name = "lblChassTemp_29"
        Me.lblChassTemp_29.Size = New System.Drawing.Size(28, 12)
        Me.lblChassTemp_29.TabIndex = 275
        Me.lblChassTemp_29.Text = "Slot 3"
        '
        'lblChassTemp_28
        '
        Me.lblChassTemp_28.AutoSize = True
        Me.lblChassTemp_28.BackColor = System.Drawing.SystemColors.Control
        Me.lblChassTemp_28.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChassTemp_28.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblChassTemp.SetIndex(Me.lblChassTemp_28, CType(28, Short))
        Me.lblChassTemp_28.Location = New System.Drawing.Point(97, 25)
        Me.lblChassTemp_28.Name = "lblChassTemp_28"
        Me.lblChassTemp_28.Size = New System.Drawing.Size(28, 12)
        Me.lblChassTemp_28.TabIndex = 274
        Me.lblChassTemp_28.Text = "Slot 2"
        '
        'lblChassTemp_27
        '
        Me.lblChassTemp_27.AutoSize = True
        Me.lblChassTemp_27.BackColor = System.Drawing.SystemColors.Control
        Me.lblChassTemp_27.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChassTemp_27.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblChassTemp.SetIndex(Me.lblChassTemp_27, CType(27, Short))
        Me.lblChassTemp_27.Location = New System.Drawing.Point(58, 25)
        Me.lblChassTemp_27.Name = "lblChassTemp_27"
        Me.lblChassTemp_27.Size = New System.Drawing.Size(28, 12)
        Me.lblChassTemp_27.TabIndex = 273
        Me.lblChassTemp_27.Text = "Slot 1"
        '
        'lblChassTemp_26
        '
        Me.lblChassTemp_26.AutoSize = True
        Me.lblChassTemp_26.BackColor = System.Drawing.SystemColors.Control
        Me.lblChassTemp_26.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChassTemp_26.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblChassTemp.SetIndex(Me.lblChassTemp_26, CType(26, Short))
        Me.lblChassTemp_26.Location = New System.Drawing.Point(16, 25)
        Me.lblChassTemp_26.Name = "lblChassTemp_26"
        Me.lblChassTemp_26.Size = New System.Drawing.Size(28, 12)
        Me.lblChassTemp_26.TabIndex = 272
        Me.lblChassTemp_26.Text = "Slot 0"
        '
        'lblExhaustValue_12
        '
        Me.lblExhaustValue_12.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblExhaustValue_12.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblExhaustValue_12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblExhaustValue_12.ForeColor = System.Drawing.Color.White
        Me.lblExhaustValue.SetIndex(Me.lblExhaustValue_12, CType(12, Short))
        Me.lblExhaustValue_12.Location = New System.Drawing.Point(502, 40)
        Me.lblExhaustValue_12.Name = "lblExhaustValue_12"
        Me.lblExhaustValue_12.Size = New System.Drawing.Size(33, 19)
        Me.lblExhaustValue_12.TabIndex = 135
        Me.lblExhaustValue_12.Text = "00.00"
        '
        'lblExhaustValue_11
        '
        Me.lblExhaustValue_11.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblExhaustValue_11.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblExhaustValue_11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblExhaustValue_11.ForeColor = System.Drawing.Color.White
        Me.lblExhaustValue.SetIndex(Me.lblExhaustValue_11, CType(11, Short))
        Me.lblExhaustValue_11.Location = New System.Drawing.Point(461, 40)
        Me.lblExhaustValue_11.Name = "lblExhaustValue_11"
        Me.lblExhaustValue_11.Size = New System.Drawing.Size(33, 19)
        Me.lblExhaustValue_11.TabIndex = 134
        Me.lblExhaustValue_11.Text = "00.00"
        '
        'lblExhaustValue_10
        '
        Me.lblExhaustValue_10.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblExhaustValue_10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblExhaustValue_10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblExhaustValue_10.ForeColor = System.Drawing.Color.White
        Me.lblExhaustValue.SetIndex(Me.lblExhaustValue_10, CType(10, Short))
        Me.lblExhaustValue_10.Location = New System.Drawing.Point(421, 40)
        Me.lblExhaustValue_10.Name = "lblExhaustValue_10"
        Me.lblExhaustValue_10.Size = New System.Drawing.Size(33, 19)
        Me.lblExhaustValue_10.TabIndex = 133
        Me.lblExhaustValue_10.Text = "00.00"
        '
        'lblExhaustValue_9
        '
        Me.lblExhaustValue_9.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblExhaustValue_9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblExhaustValue_9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblExhaustValue_9.ForeColor = System.Drawing.Color.White
        Me.lblExhaustValue.SetIndex(Me.lblExhaustValue_9, CType(9, Short))
        Me.lblExhaustValue_9.Location = New System.Drawing.Point(380, 40)
        Me.lblExhaustValue_9.Name = "lblExhaustValue_9"
        Me.lblExhaustValue_9.Size = New System.Drawing.Size(33, 19)
        Me.lblExhaustValue_9.TabIndex = 132
        Me.lblExhaustValue_9.Text = "00.00"
        '
        'lblExhaustValue_8
        '
        Me.lblExhaustValue_8.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblExhaustValue_8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblExhaustValue_8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblExhaustValue_8.ForeColor = System.Drawing.Color.White
        Me.lblExhaustValue.SetIndex(Me.lblExhaustValue_8, CType(8, Short))
        Me.lblExhaustValue_8.Location = New System.Drawing.Point(340, 40)
        Me.lblExhaustValue_8.Name = "lblExhaustValue_8"
        Me.lblExhaustValue_8.Size = New System.Drawing.Size(33, 19)
        Me.lblExhaustValue_8.TabIndex = 131
        Me.lblExhaustValue_8.Text = "00.00"
        '
        'lblExhaustValue_7
        '
        Me.lblExhaustValue_7.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblExhaustValue_7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblExhaustValue_7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblExhaustValue_7.ForeColor = System.Drawing.Color.White
        Me.lblExhaustValue.SetIndex(Me.lblExhaustValue_7, CType(7, Short))
        Me.lblExhaustValue_7.Location = New System.Drawing.Point(299, 40)
        Me.lblExhaustValue_7.Name = "lblExhaustValue_7"
        Me.lblExhaustValue_7.Size = New System.Drawing.Size(33, 19)
        Me.lblExhaustValue_7.TabIndex = 130
        Me.lblExhaustValue_7.Text = "00.00"
        '
        'lblExhaustValue_6
        '
        Me.lblExhaustValue_6.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblExhaustValue_6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblExhaustValue_6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblExhaustValue_6.ForeColor = System.Drawing.Color.White
        Me.lblExhaustValue.SetIndex(Me.lblExhaustValue_6, CType(6, Short))
        Me.lblExhaustValue_6.Location = New System.Drawing.Point(259, 40)
        Me.lblExhaustValue_6.Name = "lblExhaustValue_6"
        Me.lblExhaustValue_6.Size = New System.Drawing.Size(33, 19)
        Me.lblExhaustValue_6.TabIndex = 129
        Me.lblExhaustValue_6.Text = "00.00"
        '
        'lblExhaustValue_5
        '
        Me.lblExhaustValue_5.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblExhaustValue_5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblExhaustValue_5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblExhaustValue_5.ForeColor = System.Drawing.Color.White
        Me.lblExhaustValue.SetIndex(Me.lblExhaustValue_5, CType(5, Short))
        Me.lblExhaustValue_5.Location = New System.Drawing.Point(218, 40)
        Me.lblExhaustValue_5.Name = "lblExhaustValue_5"
        Me.lblExhaustValue_5.Size = New System.Drawing.Size(33, 19)
        Me.lblExhaustValue_5.TabIndex = 128
        Me.lblExhaustValue_5.Text = "00.00"
        '
        'lblExhaustValue_4
        '
        Me.lblExhaustValue_4.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblExhaustValue_4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblExhaustValue_4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblExhaustValue_4.ForeColor = System.Drawing.Color.White
        Me.lblExhaustValue.SetIndex(Me.lblExhaustValue_4, CType(4, Short))
        Me.lblExhaustValue_4.Location = New System.Drawing.Point(178, 40)
        Me.lblExhaustValue_4.Name = "lblExhaustValue_4"
        Me.lblExhaustValue_4.Size = New System.Drawing.Size(33, 19)
        Me.lblExhaustValue_4.TabIndex = 127
        Me.lblExhaustValue_4.Text = "00.00"
        '
        'lblExhaustValue_3
        '
        Me.lblExhaustValue_3.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblExhaustValue_3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblExhaustValue_3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblExhaustValue_3.ForeColor = System.Drawing.Color.White
        Me.lblExhaustValue.SetIndex(Me.lblExhaustValue_3, CType(3, Short))
        Me.lblExhaustValue_3.Location = New System.Drawing.Point(138, 40)
        Me.lblExhaustValue_3.Name = "lblExhaustValue_3"
        Me.lblExhaustValue_3.Size = New System.Drawing.Size(33, 19)
        Me.lblExhaustValue_3.TabIndex = 126
        Me.lblExhaustValue_3.Text = "00.00"
        '
        'lblExhaustValue_2
        '
        Me.lblExhaustValue_2.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblExhaustValue_2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblExhaustValue_2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblExhaustValue_2.ForeColor = System.Drawing.Color.White
        Me.lblExhaustValue.SetIndex(Me.lblExhaustValue_2, CType(2, Short))
        Me.lblExhaustValue_2.Location = New System.Drawing.Point(97, 40)
        Me.lblExhaustValue_2.Name = "lblExhaustValue_2"
        Me.lblExhaustValue_2.Size = New System.Drawing.Size(33, 19)
        Me.lblExhaustValue_2.TabIndex = 125
        Me.lblExhaustValue_2.Text = "00.00"
        '
        'lblExhaustValue_1
        '
        Me.lblExhaustValue_1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblExhaustValue_1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblExhaustValue_1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblExhaustValue_1.ForeColor = System.Drawing.Color.White
        Me.lblExhaustValue.SetIndex(Me.lblExhaustValue_1, CType(1, Short))
        Me.lblExhaustValue_1.Location = New System.Drawing.Point(57, 40)
        Me.lblExhaustValue_1.Name = "lblExhaustValue_1"
        Me.lblExhaustValue_1.Size = New System.Drawing.Size(33, 19)
        Me.lblExhaustValue_1.TabIndex = 124
        Me.lblExhaustValue_1.Text = "00.00"
        '
        'lblExhaustValue_0
        '
        Me.lblExhaustValue_0.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblExhaustValue_0.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblExhaustValue_0.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblExhaustValue_0.ForeColor = System.Drawing.Color.White
        Me.lblExhaustValue.SetIndex(Me.lblExhaustValue_0, CType(0, Short))
        Me.lblExhaustValue_0.Location = New System.Drawing.Point(16, 40)
        Me.lblExhaustValue_0.Name = "lblExhaustValue_0"
        Me.lblExhaustValue_0.Size = New System.Drawing.Size(33, 19)
        Me.lblExhaustValue_0.TabIndex = 123
        Me.lblExhaustValue_0.Text = "00.00"
        '
        'lblExhaustValue_13
        '
        Me.lblExhaustValue_13.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblExhaustValue_13.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblExhaustValue_13.ForeColor = System.Drawing.Color.White
        Me.lblExhaustValue.SetIndex(Me.lblExhaustValue_13, CType(13, Short))
        Me.lblExhaustValue_13.Location = New System.Drawing.Point(16, 40)
        Me.lblExhaustValue_13.Name = "lblExhaustValue_13"
        Me.lblExhaustValue_13.Size = New System.Drawing.Size(33, 19)
        Me.lblExhaustValue_13.TabIndex = 271
        Me.lblExhaustValue_13.Text = "00.00"
        '
        'lblExhaustValue_14
        '
        Me.lblExhaustValue_14.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblExhaustValue_14.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblExhaustValue_14.ForeColor = System.Drawing.Color.White
        Me.lblExhaustValue.SetIndex(Me.lblExhaustValue_14, CType(14, Short))
        Me.lblExhaustValue_14.Location = New System.Drawing.Point(57, 40)
        Me.lblExhaustValue_14.Name = "lblExhaustValue_14"
        Me.lblExhaustValue_14.Size = New System.Drawing.Size(33, 19)
        Me.lblExhaustValue_14.TabIndex = 270
        Me.lblExhaustValue_14.Text = "00.00"
        '
        'lblExhaustValue_15
        '
        Me.lblExhaustValue_15.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblExhaustValue_15.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblExhaustValue_15.ForeColor = System.Drawing.Color.White
        Me.lblExhaustValue.SetIndex(Me.lblExhaustValue_15, CType(15, Short))
        Me.lblExhaustValue_15.Location = New System.Drawing.Point(97, 40)
        Me.lblExhaustValue_15.Name = "lblExhaustValue_15"
        Me.lblExhaustValue_15.Size = New System.Drawing.Size(33, 19)
        Me.lblExhaustValue_15.TabIndex = 269
        Me.lblExhaustValue_15.Text = "00.00"
        '
        'lblExhaustValue_16
        '
        Me.lblExhaustValue_16.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblExhaustValue_16.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblExhaustValue_16.ForeColor = System.Drawing.Color.White
        Me.lblExhaustValue.SetIndex(Me.lblExhaustValue_16, CType(16, Short))
        Me.lblExhaustValue_16.Location = New System.Drawing.Point(138, 40)
        Me.lblExhaustValue_16.Name = "lblExhaustValue_16"
        Me.lblExhaustValue_16.Size = New System.Drawing.Size(33, 19)
        Me.lblExhaustValue_16.TabIndex = 268
        Me.lblExhaustValue_16.Text = "00.00"
        '
        'lblExhaustValue_17
        '
        Me.lblExhaustValue_17.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblExhaustValue_17.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblExhaustValue_17.ForeColor = System.Drawing.Color.White
        Me.lblExhaustValue.SetIndex(Me.lblExhaustValue_17, CType(17, Short))
        Me.lblExhaustValue_17.Location = New System.Drawing.Point(178, 40)
        Me.lblExhaustValue_17.Name = "lblExhaustValue_17"
        Me.lblExhaustValue_17.Size = New System.Drawing.Size(33, 19)
        Me.lblExhaustValue_17.TabIndex = 267
        Me.lblExhaustValue_17.Text = "00.00"
        '
        'lblExhaustValue_18
        '
        Me.lblExhaustValue_18.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblExhaustValue_18.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblExhaustValue_18.ForeColor = System.Drawing.Color.White
        Me.lblExhaustValue.SetIndex(Me.lblExhaustValue_18, CType(18, Short))
        Me.lblExhaustValue_18.Location = New System.Drawing.Point(218, 40)
        Me.lblExhaustValue_18.Name = "lblExhaustValue_18"
        Me.lblExhaustValue_18.Size = New System.Drawing.Size(33, 19)
        Me.lblExhaustValue_18.TabIndex = 266
        Me.lblExhaustValue_18.Text = "00.00"
        '
        'lblExhaustValue_19
        '
        Me.lblExhaustValue_19.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblExhaustValue_19.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblExhaustValue_19.ForeColor = System.Drawing.Color.White
        Me.lblExhaustValue.SetIndex(Me.lblExhaustValue_19, CType(19, Short))
        Me.lblExhaustValue_19.Location = New System.Drawing.Point(259, 40)
        Me.lblExhaustValue_19.Name = "lblExhaustValue_19"
        Me.lblExhaustValue_19.Size = New System.Drawing.Size(33, 19)
        Me.lblExhaustValue_19.TabIndex = 265
        Me.lblExhaustValue_19.Text = "00.00"
        '
        'lblExhaustValue_20
        '
        Me.lblExhaustValue_20.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblExhaustValue_20.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblExhaustValue_20.ForeColor = System.Drawing.Color.White
        Me.lblExhaustValue.SetIndex(Me.lblExhaustValue_20, CType(20, Short))
        Me.lblExhaustValue_20.Location = New System.Drawing.Point(299, 40)
        Me.lblExhaustValue_20.Name = "lblExhaustValue_20"
        Me.lblExhaustValue_20.Size = New System.Drawing.Size(33, 19)
        Me.lblExhaustValue_20.TabIndex = 264
        Me.lblExhaustValue_20.Text = "00.00"
        '
        'lblExhaustValue_21
        '
        Me.lblExhaustValue_21.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblExhaustValue_21.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblExhaustValue_21.ForeColor = System.Drawing.Color.White
        Me.lblExhaustValue.SetIndex(Me.lblExhaustValue_21, CType(21, Short))
        Me.lblExhaustValue_21.Location = New System.Drawing.Point(340, 40)
        Me.lblExhaustValue_21.Name = "lblExhaustValue_21"
        Me.lblExhaustValue_21.Size = New System.Drawing.Size(33, 19)
        Me.lblExhaustValue_21.TabIndex = 263
        Me.lblExhaustValue_21.Text = "00.00"
        '
        'lblExhaustValue_22
        '
        Me.lblExhaustValue_22.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblExhaustValue_22.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblExhaustValue_22.ForeColor = System.Drawing.Color.White
        Me.lblExhaustValue.SetIndex(Me.lblExhaustValue_22, CType(22, Short))
        Me.lblExhaustValue_22.Location = New System.Drawing.Point(380, 40)
        Me.lblExhaustValue_22.Name = "lblExhaustValue_22"
        Me.lblExhaustValue_22.Size = New System.Drawing.Size(33, 19)
        Me.lblExhaustValue_22.TabIndex = 262
        Me.lblExhaustValue_22.Text = "00.00"
        '
        'lblExhaustValue_23
        '
        Me.lblExhaustValue_23.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblExhaustValue_23.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblExhaustValue_23.ForeColor = System.Drawing.Color.White
        Me.lblExhaustValue.SetIndex(Me.lblExhaustValue_23, CType(23, Short))
        Me.lblExhaustValue_23.Location = New System.Drawing.Point(421, 40)
        Me.lblExhaustValue_23.Name = "lblExhaustValue_23"
        Me.lblExhaustValue_23.Size = New System.Drawing.Size(33, 19)
        Me.lblExhaustValue_23.TabIndex = 261
        Me.lblExhaustValue_23.Text = "00.00"
        '
        'lblExhaustValue_24
        '
        Me.lblExhaustValue_24.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblExhaustValue_24.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblExhaustValue_24.ForeColor = System.Drawing.Color.White
        Me.lblExhaustValue.SetIndex(Me.lblExhaustValue_24, CType(24, Short))
        Me.lblExhaustValue_24.Location = New System.Drawing.Point(461, 40)
        Me.lblExhaustValue_24.Name = "lblExhaustValue_24"
        Me.lblExhaustValue_24.Size = New System.Drawing.Size(33, 19)
        Me.lblExhaustValue_24.TabIndex = 260
        Me.lblExhaustValue_24.Text = "00.00"
        '
        'lblExhaustValue_25
        '
        Me.lblExhaustValue_25.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblExhaustValue_25.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblExhaustValue_25.ForeColor = System.Drawing.Color.White
        Me.lblExhaustValue.SetIndex(Me.lblExhaustValue_25, CType(25, Short))
        Me.lblExhaustValue_25.Location = New System.Drawing.Point(502, 40)
        Me.lblExhaustValue_25.Name = "lblExhaustValue_25"
        Me.lblExhaustValue_25.Size = New System.Drawing.Size(33, 19)
        Me.lblExhaustValue_25.TabIndex = 259
        Me.lblExhaustValue_25.Text = "00.00"
        '
        'cmdSaveThresholds
        '
        '
        'cmdSaveThresholds_1
        '
        Me.cmdSaveThresholds_1.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSaveThresholds_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSaveThresholds.SetIndex(Me.cmdSaveThresholds_1, CType(1, Short))
        Me.cmdSaveThresholds_1.Location = New System.Drawing.Point(348, 332)
        Me.cmdSaveThresholds_1.Name = "cmdSaveThresholds_1"
        Me.cmdSaveThresholds_1.Size = New System.Drawing.Size(100, 23)
        Me.cmdSaveThresholds_1.TabIndex = 357
        Me.cmdSaveThresholds_1.Text = "Save &Thresholds"
        Me.cmdSaveThresholds_1.UseVisualStyleBackColor = False
        Me.cmdSaveThresholds_1.Visible = False
        '
        'cmdSaveThresholds_2
        '
        Me.cmdSaveThresholds_2.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSaveThresholds_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSaveThresholds.SetIndex(Me.cmdSaveThresholds_2, CType(2, Short))
        Me.cmdSaveThresholds_2.Location = New System.Drawing.Point(348, 332)
        Me.cmdSaveThresholds_2.Name = "cmdSaveThresholds_2"
        Me.cmdSaveThresholds_2.Size = New System.Drawing.Size(100, 23)
        Me.cmdSaveThresholds_2.TabIndex = 358
        Me.cmdSaveThresholds_2.Text = "Save &Thresholds"
        Me.cmdSaveThresholds_2.UseVisualStyleBackColor = False
        Me.cmdSaveThresholds_2.Visible = False
        '
        'lblCst_2
        '
        Me.lblCst_2.AutoSize = True
        Me.lblCst_2.BackColor = System.Drawing.SystemColors.Control
        Me.lblCst_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCst.SetIndex(Me.lblCst_2, CType(2, Short))
        Me.lblCst_2.Location = New System.Drawing.Point(12, 28)
        Me.lblCst_2.Name = "lblCst_2"
        Me.lblCst_2.Size = New System.Drawing.Size(57, 13)
        Me.lblCst_2.TabIndex = 47
        Me.lblCst_2.Text = "Pass / Fail"
        '
        'lblCst_1
        '
        Me.lblCst_1.AutoSize = True
        Me.lblCst_1.BackColor = System.Drawing.SystemColors.Control
        Me.lblCst_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCst.SetIndex(Me.lblCst_1, CType(1, Short))
        Me.lblCst_1.Location = New System.Drawing.Point(12, 28)
        Me.lblCst_1.Name = "lblCst_1"
        Me.lblCst_1.Size = New System.Drawing.Size(57, 13)
        Me.lblCst_1.TabIndex = 45
        Me.lblCst_1.Text = "Pass / Fail"
        '
        'txtFanSpeedValue
        '
        '
        'txtFanSpeedValue_2
        '
        Me.txtFanSpeedValue_2.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.txtFanSpeedValue_2.ForeColor = System.Drawing.Color.White
        Me.txtFanSpeedValue.SetIndex(Me.txtFanSpeedValue_2, CType(2, Short))
        Me.txtFanSpeedValue_2.Location = New System.Drawing.Point(166, 32)
        Me.txtFanSpeedValue_2.Name = "txtFanSpeedValue_2"
        Me.txtFanSpeedValue_2.Size = New System.Drawing.Size(33, 20)
        Me.txtFanSpeedValue_2.TabIndex = 17
        Me.txtFanSpeedValue_2.Text = "0"
        Me.txtFanSpeedValue_2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtFanSpeedValue_1
        '
        Me.txtFanSpeedValue_1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.txtFanSpeedValue_1.ForeColor = System.Drawing.Color.White
        Me.txtFanSpeedValue.SetIndex(Me.txtFanSpeedValue_1, CType(1, Short))
        Me.txtFanSpeedValue_1.Location = New System.Drawing.Point(166, 32)
        Me.txtFanSpeedValue_1.Name = "txtFanSpeedValue_1"
        Me.txtFanSpeedValue_1.Size = New System.Drawing.Size(33, 20)
        Me.txtFanSpeedValue_1.TabIndex = 16
        Me.txtFanSpeedValue_1.Text = "0"
        Me.txtFanSpeedValue_1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblHu_3
        '
        Me.lblHu_3.AutoSize = True
        Me.lblHu_3.BackColor = System.Drawing.SystemColors.Control
        Me.lblHu_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblHu.SetIndex(Me.lblHu_3, CType(3, Short))
        Me.lblHu_3.Location = New System.Drawing.Point(12, 65)
        Me.lblHu_3.Name = "lblHu_3"
        Me.lblHu_3.Size = New System.Drawing.Size(75, 13)
        Me.lblHu_3.TabIndex = 40
        Me.lblHu_3.Text = "Heating Unit 2"
        '
        'lblHu_2
        '
        Me.lblHu_2.AutoSize = True
        Me.lblHu_2.BackColor = System.Drawing.SystemColors.Control
        Me.lblHu_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblHu.SetIndex(Me.lblHu_2, CType(2, Short))
        Me.lblHu_2.Location = New System.Drawing.Point(12, 36)
        Me.lblHu_2.Name = "lblHu_2"
        Me.lblHu_2.Size = New System.Drawing.Size(75, 13)
        Me.lblHu_2.TabIndex = 39
        Me.lblHu_2.Text = "Heating Unit 1"
        '
        'lblHu_0
        '
        Me.lblHu_0.AutoSize = True
        Me.lblHu_0.BackColor = System.Drawing.SystemColors.Control
        Me.lblHu_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblHu.SetIndex(Me.lblHu_0, CType(0, Short))
        Me.lblHu_0.Location = New System.Drawing.Point(12, 65)
        Me.lblHu_0.Name = "lblHu_0"
        Me.lblHu_0.Size = New System.Drawing.Size(75, 13)
        Me.lblHu_0.TabIndex = 38
        Me.lblHu_0.Text = "Heating Unit 2"
        '
        'lblHu_1
        '
        Me.lblHu_1.AutoSize = True
        Me.lblHu_1.BackColor = System.Drawing.SystemColors.Control
        Me.lblHu_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblHu.SetIndex(Me.lblHu_1, CType(1, Short))
        Me.lblHu_1.Location = New System.Drawing.Point(12, 36)
        Me.lblHu_1.Name = "lblHu_1"
        Me.lblHu_1.Size = New System.Drawing.Size(75, 13)
        Me.lblHu_1.TabIndex = 14
        Me.lblHu_1.Text = "Heating Unit 1"
        '
        'lblPca_2
        '
        Me.lblPca_2.AutoSize = True
        Me.lblPca_2.BackColor = System.Drawing.SystemColors.Control
        Me.lblPca_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPca.SetIndex(Me.lblPca_2, CType(2, Short))
        Me.lblPca_2.Location = New System.Drawing.Point(12, 97)
        Me.lblPca_2.Name = "lblPca_2"
        Me.lblPca_2.Size = New System.Drawing.Size(34, 13)
        Me.lblPca_2.TabIndex = 37
        Me.lblPca_2.Text = "Fan 3"
        '
        'lblPca_0
        '
        Me.lblPca_0.AutoSize = True
        Me.lblPca_0.BackColor = System.Drawing.SystemColors.Control
        Me.lblPca_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPca.SetIndex(Me.lblPca_0, CType(0, Short))
        Me.lblPca_0.Location = New System.Drawing.Point(12, 65)
        Me.lblPca_0.Name = "lblPca_0"
        Me.lblPca_0.Size = New System.Drawing.Size(34, 13)
        Me.lblPca_0.TabIndex = 36
        Me.lblPca_0.Text = "Fan 2"
        '
        'lblPca_3
        '
        Me.lblPca_3.AutoSize = True
        Me.lblPca_3.BackColor = System.Drawing.SystemColors.Control
        Me.lblPca_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPca.SetIndex(Me.lblPca_3, CType(3, Short))
        Me.lblPca_3.Location = New System.Drawing.Point(12, 32)
        Me.lblPca_3.Name = "lblPca_3"
        Me.lblPca_3.Size = New System.Drawing.Size(34, 13)
        Me.lblPca_3.TabIndex = 12
        Me.lblPca_3.Text = "Fan 1"
        '
        'lblPca_5
        '
        Me.lblPca_5.AutoSize = True
        Me.lblPca_5.BackColor = System.Drawing.SystemColors.Control
        Me.lblPca_5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPca.SetIndex(Me.lblPca_5, CType(5, Short))
        Me.lblPca_5.Location = New System.Drawing.Point(12, 97)
        Me.lblPca_5.Name = "lblPca_5"
        Me.lblPca_5.Size = New System.Drawing.Size(34, 13)
        Me.lblPca_5.TabIndex = 35
        Me.lblPca_5.Text = "Fan 3"
        '
        'lblPca_4
        '
        Me.lblPca_4.AutoSize = True
        Me.lblPca_4.BackColor = System.Drawing.SystemColors.Control
        Me.lblPca_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPca.SetIndex(Me.lblPca_4, CType(4, Short))
        Me.lblPca_4.Location = New System.Drawing.Point(12, 65)
        Me.lblPca_4.Name = "lblPca_4"
        Me.lblPca_4.Size = New System.Drawing.Size(34, 13)
        Me.lblPca_4.TabIndex = 34
        Me.lblPca_4.Text = "Fan 2"
        '
        'lblPca_1
        '
        Me.lblPca_1.AutoSize = True
        Me.lblPca_1.BackColor = System.Drawing.SystemColors.Control
        Me.lblPca_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPca.SetIndex(Me.lblPca_1, CType(1, Short))
        Me.lblPca_1.Location = New System.Drawing.Point(12, 32)
        Me.lblPca_1.Name = "lblPca_1"
        Me.lblPca_1.Size = New System.Drawing.Size(34, 13)
        Me.lblPca_1.TabIndex = 10
        Me.lblPca_1.Text = "Fan 1"
        '
        'lblLevel_13
        '
        Me.lblLevel_13.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblLevel_13.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblLevel_13.ForeColor = System.Drawing.Color.White
        Me.lblLevel.SetIndex(Me.lblLevel_13, CType(13, Short))
        Me.lblLevel_13.Location = New System.Drawing.Point(413, 40)
        Me.lblLevel_13.Name = "lblLevel_13"
        Me.lblLevel_13.Size = New System.Drawing.Size(41, 19)
        Me.lblLevel_13.TabIndex = 293
        Me.lblLevel_13.Text = "00.00"
        '
        'lblLevel_12
        '
        Me.lblLevel_12.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblLevel_12.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblLevel_12.ForeColor = System.Drawing.Color.White
        Me.lblLevel.SetIndex(Me.lblLevel_12, CType(12, Short))
        Me.lblLevel_12.Location = New System.Drawing.Point(356, 40)
        Me.lblLevel_12.Name = "lblLevel_12"
        Me.lblLevel_12.Size = New System.Drawing.Size(41, 19)
        Me.lblLevel_12.TabIndex = 294
        Me.lblLevel_12.Text = "00.00"
        '
        'lblLevel_11
        '
        Me.lblLevel_11.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblLevel_11.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblLevel_11.ForeColor = System.Drawing.Color.White
        Me.lblLevel.SetIndex(Me.lblLevel_11, CType(11, Short))
        Me.lblLevel_11.Location = New System.Drawing.Point(299, 40)
        Me.lblLevel_11.Name = "lblLevel_11"
        Me.lblLevel_11.Size = New System.Drawing.Size(41, 19)
        Me.lblLevel_11.TabIndex = 295
        Me.lblLevel_11.Text = "00.00"
        '
        'lblLevel_10
        '
        Me.lblLevel_10.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblLevel_10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblLevel_10.ForeColor = System.Drawing.Color.White
        Me.lblLevel.SetIndex(Me.lblLevel_10, CType(10, Short))
        Me.lblLevel_10.Location = New System.Drawing.Point(243, 40)
        Me.lblLevel_10.Name = "lblLevel_10"
        Me.lblLevel_10.Size = New System.Drawing.Size(41, 19)
        Me.lblLevel_10.TabIndex = 296
        Me.lblLevel_10.Text = "00.00"
        '
        'lblLevel_9
        '
        Me.lblLevel_9.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblLevel_9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblLevel_9.ForeColor = System.Drawing.Color.White
        Me.lblLevel.SetIndex(Me.lblLevel_9, CType(9, Short))
        Me.lblLevel_9.Location = New System.Drawing.Point(186, 40)
        Me.lblLevel_9.Name = "lblLevel_9"
        Me.lblLevel_9.Size = New System.Drawing.Size(41, 19)
        Me.lblLevel_9.TabIndex = 297
        Me.lblLevel_9.Text = "00.00"
        '
        'lblLevel_8
        '
        Me.lblLevel_8.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblLevel_8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblLevel_8.ForeColor = System.Drawing.Color.White
        Me.lblLevel.SetIndex(Me.lblLevel_8, CType(8, Short))
        Me.lblLevel_8.Location = New System.Drawing.Point(129, 40)
        Me.lblLevel_8.Name = "lblLevel_8"
        Me.lblLevel_8.Size = New System.Drawing.Size(41, 19)
        Me.lblLevel_8.TabIndex = 298
        Me.lblLevel_8.Text = "00.00"
        '
        'lblLevel_7
        '
        Me.lblLevel_7.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblLevel_7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblLevel_7.ForeColor = System.Drawing.Color.White
        Me.lblLevel.SetIndex(Me.lblLevel_7, CType(7, Short))
        Me.lblLevel_7.Location = New System.Drawing.Point(73, 40)
        Me.lblLevel_7.Name = "lblLevel_7"
        Me.lblLevel_7.Size = New System.Drawing.Size(41, 19)
        Me.lblLevel_7.TabIndex = 308
        Me.lblLevel_7.Text = "00.00"
        '
        'lblLevel_6
        '
        Me.lblLevel_6.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblLevel_6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblLevel_6.ForeColor = System.Drawing.Color.White
        Me.lblLevel.SetIndex(Me.lblLevel_6, CType(6, Short))
        Me.lblLevel_6.Location = New System.Drawing.Point(413, 40)
        Me.lblLevel_6.Name = "lblLevel_6"
        Me.lblLevel_6.Size = New System.Drawing.Size(41, 19)
        Me.lblLevel_6.TabIndex = 257
        Me.lblLevel_6.Text = "00.00"
        '
        'lblLevel_5
        '
        Me.lblLevel_5.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblLevel_5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblLevel_5.ForeColor = System.Drawing.Color.White
        Me.lblLevel.SetIndex(Me.lblLevel_5, CType(5, Short))
        Me.lblLevel_5.Location = New System.Drawing.Point(356, 40)
        Me.lblLevel_5.Name = "lblLevel_5"
        Me.lblLevel_5.Size = New System.Drawing.Size(41, 19)
        Me.lblLevel_5.TabIndex = 256
        Me.lblLevel_5.Text = "00.00"
        '
        'lblLevel_4
        '
        Me.lblLevel_4.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblLevel_4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblLevel_4.ForeColor = System.Drawing.Color.White
        Me.lblLevel.SetIndex(Me.lblLevel_4, CType(4, Short))
        Me.lblLevel_4.Location = New System.Drawing.Point(299, 40)
        Me.lblLevel_4.Name = "lblLevel_4"
        Me.lblLevel_4.Size = New System.Drawing.Size(41, 19)
        Me.lblLevel_4.TabIndex = 255
        Me.lblLevel_4.Text = "00.00"
        '
        'lblLevel_3
        '
        Me.lblLevel_3.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblLevel_3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblLevel_3.ForeColor = System.Drawing.Color.White
        Me.lblLevel.SetIndex(Me.lblLevel_3, CType(3, Short))
        Me.lblLevel_3.Location = New System.Drawing.Point(243, 40)
        Me.lblLevel_3.Name = "lblLevel_3"
        Me.lblLevel_3.Size = New System.Drawing.Size(41, 19)
        Me.lblLevel_3.TabIndex = 254
        Me.lblLevel_3.Text = "00.00"
        '
        'lblLevel_2
        '
        Me.lblLevel_2.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblLevel_2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblLevel_2.ForeColor = System.Drawing.Color.White
        Me.lblLevel.SetIndex(Me.lblLevel_2, CType(2, Short))
        Me.lblLevel_2.Location = New System.Drawing.Point(186, 40)
        Me.lblLevel_2.Name = "lblLevel_2"
        Me.lblLevel_2.Size = New System.Drawing.Size(41, 19)
        Me.lblLevel_2.TabIndex = 253
        Me.lblLevel_2.Text = "00.00"
        '
        'lblLevel_1
        '
        Me.lblLevel_1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblLevel_1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblLevel_1.ForeColor = System.Drawing.Color.White
        Me.lblLevel.SetIndex(Me.lblLevel_1, CType(1, Short))
        Me.lblLevel_1.Location = New System.Drawing.Point(129, 40)
        Me.lblLevel_1.Name = "lblLevel_1"
        Me.lblLevel_1.Size = New System.Drawing.Size(41, 19)
        Me.lblLevel_1.TabIndex = 252
        Me.lblLevel_1.Text = "00.00"
        '
        'lblLevel_0
        '
        Me.lblLevel_0.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblLevel_0.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblLevel_0.ForeColor = System.Drawing.Color.White
        Me.lblLevel.SetIndex(Me.lblLevel_0, CType(0, Short))
        Me.lblLevel_0.Location = New System.Drawing.Point(73, 40)
        Me.lblLevel_0.Name = "lblLevel_0"
        Me.lblLevel_0.Size = New System.Drawing.Size(41, 19)
        Me.lblLevel_0.TabIndex = 242
        Me.lblLevel_0.Text = "00.00"
        '
        'Label2_6
        '
        Me.Label2_6.AutoSize = True
        Me.Label2_6.BackColor = System.Drawing.SystemColors.Control
        Me.Label2_6.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2_6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.SetIndex(Me.Label2_6, CType(6, Short))
        Me.Label2_6.Location = New System.Drawing.Point(73, 24)
        Me.Label2_6.Name = "Label2_6"
        Me.Label2_6.Size = New System.Drawing.Size(20, 12)
        Me.Label2_6.TabIndex = 307
        Me.Label2_6.Text = "+24"
        Me.Label2_6.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label2_5
        '
        Me.Label2_5.AutoSize = True
        Me.Label2_5.BackColor = System.Drawing.SystemColors.Control
        Me.Label2_5.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2_5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.SetIndex(Me.Label2_5, CType(5, Short))
        Me.Label2_5.Location = New System.Drawing.Point(129, 24)
        Me.Label2_5.Name = "Label2_5"
        Me.Label2_5.Size = New System.Drawing.Size(20, 12)
        Me.Label2_5.TabIndex = 306
        Me.Label2_5.Text = "+12"
        Me.Label2_5.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label2_4
        '
        Me.Label2_4.AutoSize = True
        Me.Label2_4.BackColor = System.Drawing.SystemColors.Control
        Me.Label2_4.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.SetIndex(Me.Label2_4, CType(4, Short))
        Me.Label2_4.Location = New System.Drawing.Point(186, 24)
        Me.Label2_4.Name = "Label2_4"
        Me.Label2_4.Size = New System.Drawing.Size(15, 12)
        Me.Label2_4.TabIndex = 305
        Me.Label2_4.Text = "+5"
        Me.Label2_4.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label2_3
        '
        Me.Label2_3.AutoSize = True
        Me.Label2_3.BackColor = System.Drawing.SystemColors.Control
        Me.Label2_3.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.SetIndex(Me.Label2_3, CType(3, Short))
        Me.Label2_3.Location = New System.Drawing.Point(413, 24)
        Me.Label2_3.Name = "Label2_3"
        Me.Label2_3.Size = New System.Drawing.Size(18, 12)
        Me.Label2_3.TabIndex = 304
        Me.Label2_3.Text = "-24"
        Me.Label2_3.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label2_2
        '
        Me.Label2_2.AutoSize = True
        Me.Label2_2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2_2.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.SetIndex(Me.Label2_2, CType(2, Short))
        Me.Label2_2.Location = New System.Drawing.Point(356, 24)
        Me.Label2_2.Name = "Label2_2"
        Me.Label2_2.Size = New System.Drawing.Size(18, 12)
        Me.Label2_2.TabIndex = 303
        Me.Label2_2.Text = "-12"
        Me.Label2_2.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label2_1
        '
        Me.Label2_1.AutoSize = True
        Me.Label2_1.BackColor = System.Drawing.SystemColors.Control
        Me.Label2_1.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.SetIndex(Me.Label2_1, CType(1, Short))
        Me.Label2_1.Location = New System.Drawing.Point(299, 24)
        Me.Label2_1.Name = "Label2_1"
        Me.Label2_1.Size = New System.Drawing.Size(21, 12)
        Me.Label2_1.TabIndex = 302
        Me.Label2_1.Text = "-5.2"
        Me.Label2_1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label2_0
        '
        Me.Label2_0.AutoSize = True
        Me.Label2_0.BackColor = System.Drawing.SystemColors.Control
        Me.Label2_0.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.SetIndex(Me.Label2_0, CType(0, Short))
        Me.Label2_0.Location = New System.Drawing.Point(243, 24)
        Me.Label2_0.Name = "Label2_0"
        Me.Label2_0.Size = New System.Drawing.Size(13, 12)
        Me.Label2_0.TabIndex = 301
        Me.Label2_0.Text = "-2"
        Me.Label2_0.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label2_76
        '
        Me.Label2_76.AutoSize = True
        Me.Label2_76.BackColor = System.Drawing.SystemColors.Control
        Me.Label2_76.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2_76.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.SetIndex(Me.Label2_76, CType(76, Short))
        Me.Label2_76.Location = New System.Drawing.Point(243, 24)
        Me.Label2_76.Name = "Label2_76"
        Me.Label2_76.Size = New System.Drawing.Size(13, 12)
        Me.Label2_76.TabIndex = 249
        Me.Label2_76.Text = "-2"
        Me.Label2_76.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label2_75
        '
        Me.Label2_75.AutoSize = True
        Me.Label2_75.BackColor = System.Drawing.SystemColors.Control
        Me.Label2_75.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2_75.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.SetIndex(Me.Label2_75, CType(75, Short))
        Me.Label2_75.Location = New System.Drawing.Point(299, 24)
        Me.Label2_75.Name = "Label2_75"
        Me.Label2_75.Size = New System.Drawing.Size(21, 12)
        Me.Label2_75.TabIndex = 248
        Me.Label2_75.Text = "-5.2"
        Me.Label2_75.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label2_74
        '
        Me.Label2_74.AutoSize = True
        Me.Label2_74.BackColor = System.Drawing.SystemColors.Control
        Me.Label2_74.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2_74.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.SetIndex(Me.Label2_74, CType(74, Short))
        Me.Label2_74.Location = New System.Drawing.Point(356, 24)
        Me.Label2_74.Name = "Label2_74"
        Me.Label2_74.Size = New System.Drawing.Size(18, 12)
        Me.Label2_74.TabIndex = 247
        Me.Label2_74.Text = "-12"
        Me.Label2_74.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label2_73
        '
        Me.Label2_73.AutoSize = True
        Me.Label2_73.BackColor = System.Drawing.SystemColors.Control
        Me.Label2_73.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2_73.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.SetIndex(Me.Label2_73, CType(73, Short))
        Me.Label2_73.Location = New System.Drawing.Point(413, 24)
        Me.Label2_73.Name = "Label2_73"
        Me.Label2_73.Size = New System.Drawing.Size(18, 12)
        Me.Label2_73.TabIndex = 246
        Me.Label2_73.Text = "-24"
        Me.Label2_73.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label2_72
        '
        Me.Label2_72.AutoSize = True
        Me.Label2_72.BackColor = System.Drawing.SystemColors.Control
        Me.Label2_72.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2_72.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.SetIndex(Me.Label2_72, CType(72, Short))
        Me.Label2_72.Location = New System.Drawing.Point(186, 24)
        Me.Label2_72.Name = "Label2_72"
        Me.Label2_72.Size = New System.Drawing.Size(15, 12)
        Me.Label2_72.TabIndex = 245
        Me.Label2_72.Text = "+5"
        Me.Label2_72.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label2_71
        '
        Me.Label2_71.AutoSize = True
        Me.Label2_71.BackColor = System.Drawing.SystemColors.Control
        Me.Label2_71.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2_71.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.SetIndex(Me.Label2_71, CType(71, Short))
        Me.Label2_71.Location = New System.Drawing.Point(129, 24)
        Me.Label2_71.Name = "Label2_71"
        Me.Label2_71.Size = New System.Drawing.Size(20, 12)
        Me.Label2_71.TabIndex = 244
        Me.Label2_71.Text = "+12"
        Me.Label2_71.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label2_70
        '
        Me.Label2_70.AutoSize = True
        Me.Label2_70.BackColor = System.Drawing.SystemColors.Control
        Me.Label2_70.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2_70.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.SetIndex(Me.Label2_70, CType(70, Short))
        Me.Label2_70.Location = New System.Drawing.Point(73, 24)
        Me.Label2_70.Name = "Label2_70"
        Me.Label2_70.Size = New System.Drawing.Size(20, 12)
        Me.Label2_70.TabIndex = 243
        Me.Label2_70.Text = "+24"
        Me.Label2_70.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label2_60
        '
        Me.Label2_60.AutoSize = True
        Me.Label2_60.BackColor = System.Drawing.SystemColors.Control
        Me.Label2_60.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2_60.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.SetIndex(Me.Label2_60, CType(60, Short))
        Me.Label2_60.Location = New System.Drawing.Point(413, 57)
        Me.Label2_60.Name = "Label2_60"
        Me.Label2_60.Size = New System.Drawing.Size(18, 12)
        Me.Label2_60.TabIndex = 144
        Me.Label2_60.Text = "-24"
        Me.Label2_60.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label2_59
        '
        Me.Label2_59.AutoSize = True
        Me.Label2_59.BackColor = System.Drawing.SystemColors.Control
        Me.Label2_59.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2_59.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.SetIndex(Me.Label2_59, CType(59, Short))
        Me.Label2_59.Location = New System.Drawing.Point(356, 57)
        Me.Label2_59.Name = "Label2_59"
        Me.Label2_59.Size = New System.Drawing.Size(18, 12)
        Me.Label2_59.TabIndex = 143
        Me.Label2_59.Text = "-12"
        Me.Label2_59.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label2_58
        '
        Me.Label2_58.AutoSize = True
        Me.Label2_58.BackColor = System.Drawing.SystemColors.Control
        Me.Label2_58.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2_58.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.SetIndex(Me.Label2_58, CType(58, Short))
        Me.Label2_58.Location = New System.Drawing.Point(299, 57)
        Me.Label2_58.Name = "Label2_58"
        Me.Label2_58.Size = New System.Drawing.Size(21, 12)
        Me.Label2_58.TabIndex = 142
        Me.Label2_58.Text = "-5.2"
        Me.Label2_58.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label2_55
        '
        Me.Label2_55.AutoSize = True
        Me.Label2_55.BackColor = System.Drawing.SystemColors.Control
        Me.Label2_55.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2_55.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.SetIndex(Me.Label2_55, CType(55, Short))
        Me.Label2_55.Location = New System.Drawing.Point(202, 57)
        Me.Label2_55.Name = "Label2_55"
        Me.Label2_55.Size = New System.Drawing.Size(15, 12)
        Me.Label2_55.TabIndex = 141
        Me.Label2_55.Text = "+5"
        Me.Label2_55.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label2_54
        '
        Me.Label2_54.AutoSize = True
        Me.Label2_54.BackColor = System.Drawing.SystemColors.Control
        Me.Label2_54.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2_54.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.SetIndex(Me.Label2_54, CType(54, Short))
        Me.Label2_54.Location = New System.Drawing.Point(129, 57)
        Me.Label2_54.Name = "Label2_54"
        Me.Label2_54.Size = New System.Drawing.Size(20, 12)
        Me.Label2_54.TabIndex = 140
        Me.Label2_54.Text = "+12"
        Me.Label2_54.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label2_53
        '
        Me.Label2_53.AutoSize = True
        Me.Label2_53.BackColor = System.Drawing.SystemColors.Control
        Me.Label2_53.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2_53.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.SetIndex(Me.Label2_53, CType(53, Short))
        Me.Label2_53.Location = New System.Drawing.Point(259, 57)
        Me.Label2_53.Name = "Label2_53"
        Me.Label2_53.Size = New System.Drawing.Size(13, 12)
        Me.Label2_53.TabIndex = 139
        Me.Label2_53.Text = "-2"
        Me.Label2_53.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label2_52
        '
        Me.Label2_52.AutoSize = True
        Me.Label2_52.BackColor = System.Drawing.SystemColors.Control
        Me.Label2_52.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2_52.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.SetIndex(Me.Label2_52, CType(52, Short))
        Me.Label2_52.Location = New System.Drawing.Point(73, 57)
        Me.Label2_52.Name = "Label2_52"
        Me.Label2_52.Size = New System.Drawing.Size(20, 12)
        Me.Label2_52.TabIndex = 138
        Me.Label2_52.Text = "+24"
        Me.Label2_52.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label2_13
        '
        Me.Label2_13.AutoSize = True
        Me.Label2_13.BackColor = System.Drawing.SystemColors.Control
        Me.Label2_13.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2_13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.SetIndex(Me.Label2_13, CType(13, Short))
        Me.Label2_13.Location = New System.Drawing.Point(73, 57)
        Me.Label2_13.Name = "Label2_13"
        Me.Label2_13.Size = New System.Drawing.Size(20, 12)
        Me.Label2_13.TabIndex = 420
        Me.Label2_13.Text = "+24"
        Me.Label2_13.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label2_12
        '
        Me.Label2_12.AutoSize = True
        Me.Label2_12.BackColor = System.Drawing.SystemColors.Control
        Me.Label2_12.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2_12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.SetIndex(Me.Label2_12, CType(12, Short))
        Me.Label2_12.Location = New System.Drawing.Point(259, 57)
        Me.Label2_12.Name = "Label2_12"
        Me.Label2_12.Size = New System.Drawing.Size(13, 12)
        Me.Label2_12.TabIndex = 419
        Me.Label2_12.Text = "-2"
        Me.Label2_12.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label2_11
        '
        Me.Label2_11.AutoSize = True
        Me.Label2_11.BackColor = System.Drawing.SystemColors.Control
        Me.Label2_11.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2_11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.SetIndex(Me.Label2_11, CType(11, Short))
        Me.Label2_11.Location = New System.Drawing.Point(129, 57)
        Me.Label2_11.Name = "Label2_11"
        Me.Label2_11.Size = New System.Drawing.Size(20, 12)
        Me.Label2_11.TabIndex = 418
        Me.Label2_11.Text = "+12"
        Me.Label2_11.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label2_10
        '
        Me.Label2_10.AutoSize = True
        Me.Label2_10.BackColor = System.Drawing.SystemColors.Control
        Me.Label2_10.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2_10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.SetIndex(Me.Label2_10, CType(10, Short))
        Me.Label2_10.Location = New System.Drawing.Point(202, 57)
        Me.Label2_10.Name = "Label2_10"
        Me.Label2_10.Size = New System.Drawing.Size(15, 12)
        Me.Label2_10.TabIndex = 417
        Me.Label2_10.Text = "+5"
        Me.Label2_10.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label2_9
        '
        Me.Label2_9.AutoSize = True
        Me.Label2_9.BackColor = System.Drawing.SystemColors.Control
        Me.Label2_9.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2_9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.SetIndex(Me.Label2_9, CType(9, Short))
        Me.Label2_9.Location = New System.Drawing.Point(299, 57)
        Me.Label2_9.Name = "Label2_9"
        Me.Label2_9.Size = New System.Drawing.Size(21, 12)
        Me.Label2_9.TabIndex = 416
        Me.Label2_9.Text = "-5.2"
        Me.Label2_9.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label2_8
        '
        Me.Label2_8.AutoSize = True
        Me.Label2_8.BackColor = System.Drawing.SystemColors.Control
        Me.Label2_8.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2_8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.SetIndex(Me.Label2_8, CType(8, Short))
        Me.Label2_8.Location = New System.Drawing.Point(356, 57)
        Me.Label2_8.Name = "Label2_8"
        Me.Label2_8.Size = New System.Drawing.Size(18, 12)
        Me.Label2_8.TabIndex = 415
        Me.Label2_8.Text = "-12"
        Me.Label2_8.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label2_7
        '
        Me.Label2_7.AutoSize = True
        Me.Label2_7.BackColor = System.Drawing.SystemColors.Control
        Me.Label2_7.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2_7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.SetIndex(Me.Label2_7, CType(7, Short))
        Me.Label2_7.Location = New System.Drawing.Point(413, 57)
        Me.Label2_7.Name = "Label2_7"
        Me.Label2_7.Size = New System.Drawing.Size(18, 12)
        Me.Label2_7.TabIndex = 414
        Me.Label2_7.Text = "-24"
        Me.Label2_7.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label2_89
        '
        Me.Label2_89.AutoSize = True
        Me.Label2_89.BackColor = System.Drawing.SystemColors.Control
        Me.Label2_89.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2_89.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.SetIndex(Me.Label2_89, CType(89, Short))
        Me.Label2_89.Location = New System.Drawing.Point(453, 32)
        Me.Label2_89.Name = "Label2_89"
        Me.Label2_89.Size = New System.Drawing.Size(15, 12)
        Me.Label2_89.TabIndex = 154
        Me.Label2_89.Text = "10"
        Me.Label2_89.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label2_88
        '
        Me.Label2_88.AutoSize = True
        Me.Label2_88.BackColor = System.Drawing.SystemColors.Control
        Me.Label2_88.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2_88.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.SetIndex(Me.Label2_88, CType(88, Short))
        Me.Label2_88.Location = New System.Drawing.Point(409, 32)
        Me.Label2_88.Name = "Label2_88"
        Me.Label2_88.Size = New System.Drawing.Size(10, 12)
        Me.Label2_88.TabIndex = 153
        Me.Label2_88.Text = "9"
        Me.Label2_88.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label2_87
        '
        Me.Label2_87.AutoSize = True
        Me.Label2_87.BackColor = System.Drawing.SystemColors.Control
        Me.Label2_87.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2_87.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.SetIndex(Me.Label2_87, CType(87, Short))
        Me.Label2_87.Location = New System.Drawing.Point(364, 32)
        Me.Label2_87.Name = "Label2_87"
        Me.Label2_87.Size = New System.Drawing.Size(10, 12)
        Me.Label2_87.TabIndex = 152
        Me.Label2_87.Text = "8"
        Me.Label2_87.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label2_86
        '
        Me.Label2_86.AutoSize = True
        Me.Label2_86.BackColor = System.Drawing.SystemColors.Control
        Me.Label2_86.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2_86.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.SetIndex(Me.Label2_86, CType(86, Short))
        Me.Label2_86.Location = New System.Drawing.Point(320, 32)
        Me.Label2_86.Name = "Label2_86"
        Me.Label2_86.Size = New System.Drawing.Size(10, 12)
        Me.Label2_86.TabIndex = 151
        Me.Label2_86.Text = "7"
        Me.Label2_86.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label2_85
        '
        Me.Label2_85.AutoSize = True
        Me.Label2_85.BackColor = System.Drawing.SystemColors.Control
        Me.Label2_85.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2_85.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.SetIndex(Me.Label2_85, CType(85, Short))
        Me.Label2_85.Location = New System.Drawing.Point(275, 32)
        Me.Label2_85.Name = "Label2_85"
        Me.Label2_85.Size = New System.Drawing.Size(10, 12)
        Me.Label2_85.TabIndex = 150
        Me.Label2_85.Text = "6"
        Me.Label2_85.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label2_84
        '
        Me.Label2_84.AutoSize = True
        Me.Label2_84.BackColor = System.Drawing.SystemColors.Control
        Me.Label2_84.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2_84.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.SetIndex(Me.Label2_84, CType(84, Short))
        Me.Label2_84.Location = New System.Drawing.Point(231, 32)
        Me.Label2_84.Name = "Label2_84"
        Me.Label2_84.Size = New System.Drawing.Size(10, 12)
        Me.Label2_84.TabIndex = 149
        Me.Label2_84.Text = "5"
        Me.Label2_84.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label2_83
        '
        Me.Label2_83.AutoSize = True
        Me.Label2_83.BackColor = System.Drawing.SystemColors.Control
        Me.Label2_83.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2_83.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.SetIndex(Me.Label2_83, CType(83, Short))
        Me.Label2_83.Location = New System.Drawing.Point(186, 32)
        Me.Label2_83.Name = "Label2_83"
        Me.Label2_83.Size = New System.Drawing.Size(10, 12)
        Me.Label2_83.TabIndex = 148
        Me.Label2_83.Text = "4"
        Me.Label2_83.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label2_82
        '
        Me.Label2_82.AutoSize = True
        Me.Label2_82.BackColor = System.Drawing.SystemColors.Control
        Me.Label2_82.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2_82.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.SetIndex(Me.Label2_82, CType(82, Short))
        Me.Label2_82.Location = New System.Drawing.Point(142, 32)
        Me.Label2_82.Name = "Label2_82"
        Me.Label2_82.Size = New System.Drawing.Size(10, 12)
        Me.Label2_82.TabIndex = 147
        Me.Label2_82.Text = "3"
        Me.Label2_82.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label2_81
        '
        Me.Label2_81.AutoSize = True
        Me.Label2_81.BackColor = System.Drawing.SystemColors.Control
        Me.Label2_81.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2_81.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.SetIndex(Me.Label2_81, CType(81, Short))
        Me.Label2_81.Location = New System.Drawing.Point(97, 32)
        Me.Label2_81.Name = "Label2_81"
        Me.Label2_81.Size = New System.Drawing.Size(10, 12)
        Me.Label2_81.TabIndex = 146
        Me.Label2_81.Text = "2"
        Me.Label2_81.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label2_80
        '
        Me.Label2_80.AutoSize = True
        Me.Label2_80.BackColor = System.Drawing.SystemColors.Control
        Me.Label2_80.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2_80.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.SetIndex(Me.Label2_80, CType(80, Short))
        Me.Label2_80.Location = New System.Drawing.Point(53, 32)
        Me.Label2_80.Name = "Label2_80"
        Me.Label2_80.Size = New System.Drawing.Size(10, 12)
        Me.Label2_80.TabIndex = 145
        Me.Label2_80.Text = "1"
        Me.Label2_80.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label2_99
        '
        Me.Label2_99.AutoSize = True
        Me.Label2_99.BackColor = System.Drawing.SystemColors.Control
        Me.Label2_99.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2_99.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.SetIndex(Me.Label2_99, CType(99, Short))
        Me.Label2_99.Location = New System.Drawing.Point(453, 32)
        Me.Label2_99.Name = "Label2_99"
        Me.Label2_99.Size = New System.Drawing.Size(15, 12)
        Me.Label2_99.TabIndex = 164
        Me.Label2_99.Text = "10"
        Me.Label2_99.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label2_98
        '
        Me.Label2_98.AutoSize = True
        Me.Label2_98.BackColor = System.Drawing.SystemColors.Control
        Me.Label2_98.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2_98.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.SetIndex(Me.Label2_98, CType(98, Short))
        Me.Label2_98.Location = New System.Drawing.Point(409, 32)
        Me.Label2_98.Name = "Label2_98"
        Me.Label2_98.Size = New System.Drawing.Size(10, 12)
        Me.Label2_98.TabIndex = 163
        Me.Label2_98.Text = "9"
        Me.Label2_98.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label2_97
        '
        Me.Label2_97.AutoSize = True
        Me.Label2_97.BackColor = System.Drawing.SystemColors.Control
        Me.Label2_97.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2_97.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.SetIndex(Me.Label2_97, CType(97, Short))
        Me.Label2_97.Location = New System.Drawing.Point(364, 32)
        Me.Label2_97.Name = "Label2_97"
        Me.Label2_97.Size = New System.Drawing.Size(10, 12)
        Me.Label2_97.TabIndex = 162
        Me.Label2_97.Text = "8"
        Me.Label2_97.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label2_96
        '
        Me.Label2_96.AutoSize = True
        Me.Label2_96.BackColor = System.Drawing.SystemColors.Control
        Me.Label2_96.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2_96.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.SetIndex(Me.Label2_96, CType(96, Short))
        Me.Label2_96.Location = New System.Drawing.Point(320, 32)
        Me.Label2_96.Name = "Label2_96"
        Me.Label2_96.Size = New System.Drawing.Size(10, 12)
        Me.Label2_96.TabIndex = 161
        Me.Label2_96.Text = "7"
        Me.Label2_96.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label2_95
        '
        Me.Label2_95.AutoSize = True
        Me.Label2_95.BackColor = System.Drawing.SystemColors.Control
        Me.Label2_95.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2_95.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.SetIndex(Me.Label2_95, CType(95, Short))
        Me.Label2_95.Location = New System.Drawing.Point(275, 32)
        Me.Label2_95.Name = "Label2_95"
        Me.Label2_95.Size = New System.Drawing.Size(10, 12)
        Me.Label2_95.TabIndex = 160
        Me.Label2_95.Text = "6"
        Me.Label2_95.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label2_94
        '
        Me.Label2_94.AutoSize = True
        Me.Label2_94.BackColor = System.Drawing.SystemColors.Control
        Me.Label2_94.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2_94.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.SetIndex(Me.Label2_94, CType(94, Short))
        Me.Label2_94.Location = New System.Drawing.Point(231, 32)
        Me.Label2_94.Name = "Label2_94"
        Me.Label2_94.Size = New System.Drawing.Size(10, 12)
        Me.Label2_94.TabIndex = 159
        Me.Label2_94.Text = "5"
        Me.Label2_94.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label2_93
        '
        Me.Label2_93.AutoSize = True
        Me.Label2_93.BackColor = System.Drawing.SystemColors.Control
        Me.Label2_93.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2_93.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.SetIndex(Me.Label2_93, CType(93, Short))
        Me.Label2_93.Location = New System.Drawing.Point(186, 32)
        Me.Label2_93.Name = "Label2_93"
        Me.Label2_93.Size = New System.Drawing.Size(10, 12)
        Me.Label2_93.TabIndex = 158
        Me.Label2_93.Text = "4"
        Me.Label2_93.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label2_92
        '
        Me.Label2_92.AutoSize = True
        Me.Label2_92.BackColor = System.Drawing.SystemColors.Control
        Me.Label2_92.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2_92.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.SetIndex(Me.Label2_92, CType(92, Short))
        Me.Label2_92.Location = New System.Drawing.Point(142, 32)
        Me.Label2_92.Name = "Label2_92"
        Me.Label2_92.Size = New System.Drawing.Size(10, 12)
        Me.Label2_92.TabIndex = 157
        Me.Label2_92.Text = "3"
        Me.Label2_92.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label2_91
        '
        Me.Label2_91.AutoSize = True
        Me.Label2_91.BackColor = System.Drawing.SystemColors.Control
        Me.Label2_91.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2_91.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.SetIndex(Me.Label2_91, CType(91, Short))
        Me.Label2_91.Location = New System.Drawing.Point(97, 32)
        Me.Label2_91.Name = "Label2_91"
        Me.Label2_91.Size = New System.Drawing.Size(10, 12)
        Me.Label2_91.TabIndex = 156
        Me.Label2_91.Text = "2"
        Me.Label2_91.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label2_90
        '
        Me.Label2_90.AutoSize = True
        Me.Label2_90.BackColor = System.Drawing.SystemColors.Control
        Me.Label2_90.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2_90.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.SetIndex(Me.Label2_90, CType(90, Short))
        Me.Label2_90.Location = New System.Drawing.Point(53, 32)
        Me.Label2_90.Name = "Label2_90"
        Me.Label2_90.Size = New System.Drawing.Size(10, 12)
        Me.Label2_90.TabIndex = 155
        Me.Label2_90.Text = "1"
        Me.Label2_90.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblCp_9
        '
        Me.lblCp_9.AutoSize = True
        Me.lblCp_9.BackColor = System.Drawing.SystemColors.Control
        Me.lblCp_9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCp.SetIndex(Me.lblCp_9, CType(9, Short))
        Me.lblCp_9.Location = New System.Drawing.Point(16, 49)
        Me.lblCp_9.Name = "lblCp_9"
        Me.lblCp_9.Size = New System.Drawing.Size(33, 13)
        Me.lblCp_9.TabIndex = 300
        Me.lblCp_9.Text = "Level"
        '
        'lblCp_8
        '
        Me.lblCp_8.AutoSize = True
        Me.lblCp_8.BackColor = System.Drawing.SystemColors.Control
        Me.lblCp_8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCp.SetIndex(Me.lblCp_8, CType(8, Short))
        Me.lblCp_8.Location = New System.Drawing.Point(16, 32)
        Me.lblCp_8.Name = "lblCp_8"
        Me.lblCp_8.Size = New System.Drawing.Size(39, 13)
        Me.lblCp_8.TabIndex = 299
        Me.lblCp_8.Text = "Supply"
        '
        'lblCp_5
        '
        Me.lblCp_5.AutoSize = True
        Me.lblCp_5.BackColor = System.Drawing.SystemColors.Control
        Me.lblCp_5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCp.SetIndex(Me.lblCp_5, CType(5, Short))
        Me.lblCp_5.Location = New System.Drawing.Point(16, 32)
        Me.lblCp_5.Name = "lblCp_5"
        Me.lblCp_5.Size = New System.Drawing.Size(39, 13)
        Me.lblCp_5.TabIndex = 251
        Me.lblCp_5.Text = "Supply"
        '
        'lblCp_4
        '
        Me.lblCp_4.AutoSize = True
        Me.lblCp_4.BackColor = System.Drawing.SystemColors.Control
        Me.lblCp_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCp.SetIndex(Me.lblCp_4, CType(4, Short))
        Me.lblCp_4.Location = New System.Drawing.Point(16, 49)
        Me.lblCp_4.Name = "lblCp_4"
        Me.lblCp_4.Size = New System.Drawing.Size(33, 13)
        Me.lblCp_4.TabIndex = 250
        Me.lblCp_4.Text = "Level"
        '
        'lblCp_7
        '
        Me.lblCp_7.AutoSize = True
        Me.lblCp_7.BackColor = System.Drawing.SystemColors.Control
        Me.lblCp_7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCp.SetIndex(Me.lblCp_7, CType(7, Short))
        Me.lblCp_7.Location = New System.Drawing.Point(8, 65)
        Me.lblCp_7.Name = "lblCp_7"
        Me.lblCp_7.Size = New System.Drawing.Size(61, 13)
        Me.lblCp_7.TabIndex = 406
        Me.lblCp_7.Text = "Secondary "
        '
        'lblCp_6
        '
        Me.lblCp_6.AutoSize = True
        Me.lblCp_6.BackColor = System.Drawing.SystemColors.Control
        Me.lblCp_6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCp.SetIndex(Me.lblCp_6, CType(6, Short))
        Me.lblCp_6.Location = New System.Drawing.Point(16, 81)
        Me.lblCp_6.Name = "lblCp_6"
        Me.lblCp_6.Size = New System.Drawing.Size(43, 13)
        Me.lblCp_6.TabIndex = 405
        Me.lblCp_6.Text = "Voltage"
        '
        'lblCp_0
        '
        Me.lblCp_0.AutoSize = True
        Me.lblCp_0.BackColor = System.Drawing.SystemColors.Control
        Me.lblCp_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCp.SetIndex(Me.lblCp_0, CType(0, Short))
        Me.lblCp_0.Location = New System.Drawing.Point(16, 40)
        Me.lblCp_0.Name = "lblCp_0"
        Me.lblCp_0.Size = New System.Drawing.Size(43, 13)
        Me.lblCp_0.TabIndex = 22
        Me.lblCp_0.Text = "Voltage"
        '
        'lblCp_1
        '
        Me.lblCp_1.AutoSize = True
        Me.lblCp_1.BackColor = System.Drawing.SystemColors.Control
        Me.lblCp_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCp.SetIndex(Me.lblCp_1, CType(1, Short))
        Me.lblCp_1.Location = New System.Drawing.Point(16, 24)
        Me.lblCp_1.Name = "lblCp_1"
        Me.lblCp_1.Size = New System.Drawing.Size(44, 13)
        Me.lblCp_1.TabIndex = 21
        Me.lblCp_1.Text = "Primary "
        '
        'lblCp_11
        '
        Me.lblCp_11.AutoSize = True
        Me.lblCp_11.BackColor = System.Drawing.SystemColors.Control
        Me.lblCp_11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCp.SetIndex(Me.lblCp_11, CType(11, Short))
        Me.lblCp_11.Location = New System.Drawing.Point(8, 65)
        Me.lblCp_11.Name = "lblCp_11"
        Me.lblCp_11.Size = New System.Drawing.Size(58, 13)
        Me.lblCp_11.TabIndex = 422
        Me.lblCp_11.Text = "Secondary"
        '
        'lblCp_10
        '
        Me.lblCp_10.AutoSize = True
        Me.lblCp_10.BackColor = System.Drawing.SystemColors.Control
        Me.lblCp_10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCp.SetIndex(Me.lblCp_10, CType(10, Short))
        Me.lblCp_10.Location = New System.Drawing.Point(16, 81)
        Me.lblCp_10.Name = "lblCp_10"
        Me.lblCp_10.Size = New System.Drawing.Size(41, 13)
        Me.lblCp_10.TabIndex = 421
        Me.lblCp_10.Text = "Current"
        '
        'lblCp_2
        '
        Me.lblCp_2.AutoSize = True
        Me.lblCp_2.BackColor = System.Drawing.SystemColors.Control
        Me.lblCp_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCp.SetIndex(Me.lblCp_2, CType(2, Short))
        Me.lblCp_2.Location = New System.Drawing.Point(16, 40)
        Me.lblCp_2.Name = "lblCp_2"
        Me.lblCp_2.Size = New System.Drawing.Size(41, 13)
        Me.lblCp_2.TabIndex = 24
        Me.lblCp_2.Text = "Current"
        '
        'lblCp_3
        '
        Me.lblCp_3.AutoSize = True
        Me.lblCp_3.BackColor = System.Drawing.SystemColors.Control
        Me.lblCp_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCp.SetIndex(Me.lblCp_3, CType(3, Short))
        Me.lblCp_3.Location = New System.Drawing.Point(16, 24)
        Me.lblCp_3.Name = "lblCp_3"
        Me.lblCp_3.Size = New System.Drawing.Size(41, 13)
        Me.lblCp_3.TabIndex = 23
        Me.lblCp_3.Text = "Primary"
        '
        'lblBackVolt_13
        '
        Me.lblBackVolt_13.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblBackVolt_13.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBackVolt_13.ForeColor = System.Drawing.Color.White
        Me.lblBackVolt.SetIndex(Me.lblBackVolt_13, CType(13, Short))
        Me.lblBackVolt_13.Location = New System.Drawing.Point(413, 73)
        Me.lblBackVolt_13.Name = "lblBackVolt_13"
        Me.lblBackVolt_13.Size = New System.Drawing.Size(41, 19)
        Me.lblBackVolt_13.TabIndex = 413
        Me.lblBackVolt_13.Text = "00.00"
        '
        'lblBackVolt_12
        '
        Me.lblBackVolt_12.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblBackVolt_12.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBackVolt_12.ForeColor = System.Drawing.Color.White
        Me.lblBackVolt.SetIndex(Me.lblBackVolt_12, CType(12, Short))
        Me.lblBackVolt_12.Location = New System.Drawing.Point(356, 73)
        Me.lblBackVolt_12.Name = "lblBackVolt_12"
        Me.lblBackVolt_12.Size = New System.Drawing.Size(41, 19)
        Me.lblBackVolt_12.TabIndex = 412
        Me.lblBackVolt_12.Text = "00.00"
        '
        'lblBackVolt_11
        '
        Me.lblBackVolt_11.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblBackVolt_11.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBackVolt_11.ForeColor = System.Drawing.Color.White
        Me.lblBackVolt.SetIndex(Me.lblBackVolt_11, CType(11, Short))
        Me.lblBackVolt_11.Location = New System.Drawing.Point(299, 73)
        Me.lblBackVolt_11.Name = "lblBackVolt_11"
        Me.lblBackVolt_11.Size = New System.Drawing.Size(41, 19)
        Me.lblBackVolt_11.TabIndex = 411
        Me.lblBackVolt_11.Text = "00.00"
        '
        'lblBackVolt_10
        '
        Me.lblBackVolt_10.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblBackVolt_10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBackVolt_10.ForeColor = System.Drawing.Color.White
        Me.lblBackVolt.SetIndex(Me.lblBackVolt_10, CType(10, Short))
        Me.lblBackVolt_10.Location = New System.Drawing.Point(243, 73)
        Me.lblBackVolt_10.Name = "lblBackVolt_10"
        Me.lblBackVolt_10.Size = New System.Drawing.Size(41, 19)
        Me.lblBackVolt_10.TabIndex = 410
        Me.lblBackVolt_10.Text = "00.00"
        '
        'lblBackVolt_9
        '
        Me.lblBackVolt_9.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblBackVolt_9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBackVolt_9.ForeColor = System.Drawing.Color.White
        Me.lblBackVolt.SetIndex(Me.lblBackVolt_9, CType(9, Short))
        Me.lblBackVolt_9.Location = New System.Drawing.Point(186, 73)
        Me.lblBackVolt_9.Name = "lblBackVolt_9"
        Me.lblBackVolt_9.Size = New System.Drawing.Size(41, 19)
        Me.lblBackVolt_9.TabIndex = 409
        Me.lblBackVolt_9.Text = "00.00"
        '
        'lblBackVolt_8
        '
        Me.lblBackVolt_8.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblBackVolt_8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBackVolt_8.ForeColor = System.Drawing.Color.White
        Me.lblBackVolt.SetIndex(Me.lblBackVolt_8, CType(8, Short))
        Me.lblBackVolt_8.Location = New System.Drawing.Point(129, 73)
        Me.lblBackVolt_8.Name = "lblBackVolt_8"
        Me.lblBackVolt_8.Size = New System.Drawing.Size(41, 19)
        Me.lblBackVolt_8.TabIndex = 408
        Me.lblBackVolt_8.Text = "00.00"
        '
        'lblBackVolt_7
        '
        Me.lblBackVolt_7.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblBackVolt_7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBackVolt_7.ForeColor = System.Drawing.Color.White
        Me.lblBackVolt.SetIndex(Me.lblBackVolt_7, CType(7, Short))
        Me.lblBackVolt_7.Location = New System.Drawing.Point(73, 73)
        Me.lblBackVolt_7.Name = "lblBackVolt_7"
        Me.lblBackVolt_7.Size = New System.Drawing.Size(41, 19)
        Me.lblBackVolt_7.TabIndex = 407
        Me.lblBackVolt_7.Text = "00.00"
        '
        'lblBackVolt_6
        '
        Me.lblBackVolt_6.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblBackVolt_6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBackVolt_6.ForeColor = System.Drawing.Color.White
        Me.lblBackVolt.SetIndex(Me.lblBackVolt_6, CType(6, Short))
        Me.lblBackVolt_6.Location = New System.Drawing.Point(413, 32)
        Me.lblBackVolt_6.Name = "lblBackVolt_6"
        Me.lblBackVolt_6.Size = New System.Drawing.Size(41, 19)
        Me.lblBackVolt_6.TabIndex = 171
        Me.lblBackVolt_6.Text = "00.00"
        '
        'lblBackVolt_5
        '
        Me.lblBackVolt_5.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblBackVolt_5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBackVolt_5.ForeColor = System.Drawing.Color.White
        Me.lblBackVolt.SetIndex(Me.lblBackVolt_5, CType(5, Short))
        Me.lblBackVolt_5.Location = New System.Drawing.Point(356, 32)
        Me.lblBackVolt_5.Name = "lblBackVolt_5"
        Me.lblBackVolt_5.Size = New System.Drawing.Size(41, 19)
        Me.lblBackVolt_5.TabIndex = 170
        Me.lblBackVolt_5.Text = "00.00"
        '
        'lblBackVolt_4
        '
        Me.lblBackVolt_4.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblBackVolt_4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBackVolt_4.ForeColor = System.Drawing.Color.White
        Me.lblBackVolt.SetIndex(Me.lblBackVolt_4, CType(4, Short))
        Me.lblBackVolt_4.Location = New System.Drawing.Point(299, 32)
        Me.lblBackVolt_4.Name = "lblBackVolt_4"
        Me.lblBackVolt_4.Size = New System.Drawing.Size(41, 19)
        Me.lblBackVolt_4.TabIndex = 169
        Me.lblBackVolt_4.Text = "00.00"
        '
        'lblBackVolt_3
        '
        Me.lblBackVolt_3.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblBackVolt_3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBackVolt_3.ForeColor = System.Drawing.Color.White
        Me.lblBackVolt.SetIndex(Me.lblBackVolt_3, CType(3, Short))
        Me.lblBackVolt_3.Location = New System.Drawing.Point(243, 32)
        Me.lblBackVolt_3.Name = "lblBackVolt_3"
        Me.lblBackVolt_3.Size = New System.Drawing.Size(41, 19)
        Me.lblBackVolt_3.TabIndex = 168
        Me.lblBackVolt_3.Text = "00.00"
        '
        'lblBackVolt_2
        '
        Me.lblBackVolt_2.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblBackVolt_2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBackVolt_2.ForeColor = System.Drawing.Color.White
        Me.lblBackVolt.SetIndex(Me.lblBackVolt_2, CType(2, Short))
        Me.lblBackVolt_2.Location = New System.Drawing.Point(186, 32)
        Me.lblBackVolt_2.Name = "lblBackVolt_2"
        Me.lblBackVolt_2.Size = New System.Drawing.Size(41, 19)
        Me.lblBackVolt_2.TabIndex = 167
        Me.lblBackVolt_2.Text = "00.00"
        '
        'lblBackVolt_1
        '
        Me.lblBackVolt_1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblBackVolt_1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBackVolt_1.ForeColor = System.Drawing.Color.White
        Me.lblBackVolt.SetIndex(Me.lblBackVolt_1, CType(1, Short))
        Me.lblBackVolt_1.Location = New System.Drawing.Point(129, 32)
        Me.lblBackVolt_1.Name = "lblBackVolt_1"
        Me.lblBackVolt_1.Size = New System.Drawing.Size(41, 19)
        Me.lblBackVolt_1.TabIndex = 166
        Me.lblBackVolt_1.Text = "00.00"
        '
        'lblBackVolt_0
        '
        Me.lblBackVolt_0.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblBackVolt_0.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBackVolt_0.ForeColor = System.Drawing.Color.White
        Me.lblBackVolt.SetIndex(Me.lblBackVolt_0, CType(0, Short))
        Me.lblBackVolt_0.Location = New System.Drawing.Point(73, 32)
        Me.lblBackVolt_0.Name = "lblBackVolt_0"
        Me.lblBackVolt_0.Size = New System.Drawing.Size(41, 19)
        Me.lblBackVolt_0.TabIndex = 165
        Me.lblBackVolt_0.Text = "00.00"
        '
        'lblBackCurr_13
        '
        Me.lblBackCurr_13.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblBackCurr_13.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBackCurr_13.ForeColor = System.Drawing.Color.White
        Me.lblBackCurr.SetIndex(Me.lblBackCurr_13, CType(13, Short))
        Me.lblBackCurr_13.Location = New System.Drawing.Point(413, 73)
        Me.lblBackCurr_13.Name = "lblBackCurr_13"
        Me.lblBackCurr_13.Size = New System.Drawing.Size(41, 19)
        Me.lblBackCurr_13.TabIndex = 429
        Me.lblBackCurr_13.Text = "00.00"
        '
        'lblBackCurr_12
        '
        Me.lblBackCurr_12.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblBackCurr_12.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBackCurr_12.ForeColor = System.Drawing.Color.White
        Me.lblBackCurr.SetIndex(Me.lblBackCurr_12, CType(12, Short))
        Me.lblBackCurr_12.Location = New System.Drawing.Point(356, 73)
        Me.lblBackCurr_12.Name = "lblBackCurr_12"
        Me.lblBackCurr_12.Size = New System.Drawing.Size(41, 19)
        Me.lblBackCurr_12.TabIndex = 428
        Me.lblBackCurr_12.Text = "00.00"
        '
        'lblBackCurr_11
        '
        Me.lblBackCurr_11.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblBackCurr_11.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBackCurr_11.ForeColor = System.Drawing.Color.White
        Me.lblBackCurr.SetIndex(Me.lblBackCurr_11, CType(11, Short))
        Me.lblBackCurr_11.Location = New System.Drawing.Point(299, 73)
        Me.lblBackCurr_11.Name = "lblBackCurr_11"
        Me.lblBackCurr_11.Size = New System.Drawing.Size(41, 19)
        Me.lblBackCurr_11.TabIndex = 427
        Me.lblBackCurr_11.Text = "00.00"
        '
        'lblBackCurr_10
        '
        Me.lblBackCurr_10.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblBackCurr_10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBackCurr_10.ForeColor = System.Drawing.Color.White
        Me.lblBackCurr.SetIndex(Me.lblBackCurr_10, CType(10, Short))
        Me.lblBackCurr_10.Location = New System.Drawing.Point(243, 73)
        Me.lblBackCurr_10.Name = "lblBackCurr_10"
        Me.lblBackCurr_10.Size = New System.Drawing.Size(41, 19)
        Me.lblBackCurr_10.TabIndex = 426
        Me.lblBackCurr_10.Text = "00.00"
        '
        'lblBackCurr_9
        '
        Me.lblBackCurr_9.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblBackCurr_9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBackCurr_9.ForeColor = System.Drawing.Color.White
        Me.lblBackCurr.SetIndex(Me.lblBackCurr_9, CType(9, Short))
        Me.lblBackCurr_9.Location = New System.Drawing.Point(186, 73)
        Me.lblBackCurr_9.Name = "lblBackCurr_9"
        Me.lblBackCurr_9.Size = New System.Drawing.Size(41, 19)
        Me.lblBackCurr_9.TabIndex = 425
        Me.lblBackCurr_9.Text = "00.00"
        '
        'lblBackCurr_8
        '
        Me.lblBackCurr_8.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblBackCurr_8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBackCurr_8.ForeColor = System.Drawing.Color.White
        Me.lblBackCurr.SetIndex(Me.lblBackCurr_8, CType(8, Short))
        Me.lblBackCurr_8.Location = New System.Drawing.Point(129, 73)
        Me.lblBackCurr_8.Name = "lblBackCurr_8"
        Me.lblBackCurr_8.Size = New System.Drawing.Size(41, 19)
        Me.lblBackCurr_8.TabIndex = 424
        Me.lblBackCurr_8.Text = "00.00"
        '
        'lblBackCurr_7
        '
        Me.lblBackCurr_7.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblBackCurr_7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBackCurr_7.ForeColor = System.Drawing.Color.White
        Me.lblBackCurr.SetIndex(Me.lblBackCurr_7, CType(7, Short))
        Me.lblBackCurr_7.Location = New System.Drawing.Point(73, 73)
        Me.lblBackCurr_7.Name = "lblBackCurr_7"
        Me.lblBackCurr_7.Size = New System.Drawing.Size(41, 19)
        Me.lblBackCurr_7.TabIndex = 423
        Me.lblBackCurr_7.Text = "00.00"
        '
        'lblBackCurr_6
        '
        Me.lblBackCurr_6.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblBackCurr_6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBackCurr_6.ForeColor = System.Drawing.Color.White
        Me.lblBackCurr.SetIndex(Me.lblBackCurr_6, CType(6, Short))
        Me.lblBackCurr_6.Location = New System.Drawing.Point(413, 32)
        Me.lblBackCurr_6.Name = "lblBackCurr_6"
        Me.lblBackCurr_6.Size = New System.Drawing.Size(41, 19)
        Me.lblBackCurr_6.TabIndex = 178
        Me.lblBackCurr_6.Text = "00.00"
        '
        'lblBackCurr_5
        '
        Me.lblBackCurr_5.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblBackCurr_5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBackCurr_5.ForeColor = System.Drawing.Color.White
        Me.lblBackCurr.SetIndex(Me.lblBackCurr_5, CType(5, Short))
        Me.lblBackCurr_5.Location = New System.Drawing.Point(356, 32)
        Me.lblBackCurr_5.Name = "lblBackCurr_5"
        Me.lblBackCurr_5.Size = New System.Drawing.Size(41, 19)
        Me.lblBackCurr_5.TabIndex = 177
        Me.lblBackCurr_5.Text = "00.00"
        '
        'lblBackCurr_4
        '
        Me.lblBackCurr_4.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblBackCurr_4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBackCurr_4.ForeColor = System.Drawing.Color.White
        Me.lblBackCurr.SetIndex(Me.lblBackCurr_4, CType(4, Short))
        Me.lblBackCurr_4.Location = New System.Drawing.Point(299, 32)
        Me.lblBackCurr_4.Name = "lblBackCurr_4"
        Me.lblBackCurr_4.Size = New System.Drawing.Size(41, 19)
        Me.lblBackCurr_4.TabIndex = 176
        Me.lblBackCurr_4.Text = "00.00"
        '
        'lblBackCurr_3
        '
        Me.lblBackCurr_3.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblBackCurr_3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBackCurr_3.ForeColor = System.Drawing.Color.White
        Me.lblBackCurr.SetIndex(Me.lblBackCurr_3, CType(3, Short))
        Me.lblBackCurr_3.Location = New System.Drawing.Point(243, 32)
        Me.lblBackCurr_3.Name = "lblBackCurr_3"
        Me.lblBackCurr_3.Size = New System.Drawing.Size(41, 19)
        Me.lblBackCurr_3.TabIndex = 175
        Me.lblBackCurr_3.Text = "00.00"
        '
        'lblBackCurr_2
        '
        Me.lblBackCurr_2.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblBackCurr_2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBackCurr_2.ForeColor = System.Drawing.Color.White
        Me.lblBackCurr.SetIndex(Me.lblBackCurr_2, CType(2, Short))
        Me.lblBackCurr_2.Location = New System.Drawing.Point(186, 32)
        Me.lblBackCurr_2.Name = "lblBackCurr_2"
        Me.lblBackCurr_2.Size = New System.Drawing.Size(41, 19)
        Me.lblBackCurr_2.TabIndex = 174
        Me.lblBackCurr_2.Text = "00.00"
        '
        'lblBackCurr_1
        '
        Me.lblBackCurr_1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblBackCurr_1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBackCurr_1.ForeColor = System.Drawing.Color.White
        Me.lblBackCurr.SetIndex(Me.lblBackCurr_1, CType(1, Short))
        Me.lblBackCurr_1.Location = New System.Drawing.Point(129, 32)
        Me.lblBackCurr_1.Name = "lblBackCurr_1"
        Me.lblBackCurr_1.Size = New System.Drawing.Size(41, 19)
        Me.lblBackCurr_1.TabIndex = 173
        Me.lblBackCurr_1.Text = "00.00"
        '
        'lblBackCurr_0
        '
        Me.lblBackCurr_0.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblBackCurr_0.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBackCurr_0.ForeColor = System.Drawing.Color.White
        Me.lblBackCurr.SetIndex(Me.lblBackCurr_0, CType(0, Short))
        Me.lblBackCurr_0.Location = New System.Drawing.Point(73, 32)
        Me.lblBackCurr_0.Name = "lblBackCurr_0"
        Me.lblBackCurr_0.Size = New System.Drawing.Size(41, 19)
        Me.lblBackCurr_0.TabIndex = 172
        Me.lblBackCurr_0.Text = "00.00"
        '
        'lblUutVolt_10
        '
        Me.lblUutVolt_10.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblUutVolt_10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblUutVolt_10.ForeColor = System.Drawing.Color.White
        Me.lblUutVolt.SetIndex(Me.lblUutVolt_10, CType(10, Short))
        Me.lblUutVolt_10.Location = New System.Drawing.Point(453, 49)
        Me.lblUutVolt_10.Name = "lblUutVolt_10"
        Me.lblUutVolt_10.Size = New System.Drawing.Size(41, 19)
        Me.lblUutVolt_10.TabIndex = 188
        Me.lblUutVolt_10.Text = "00.00"
        '
        'lblUutVolt_9
        '
        Me.lblUutVolt_9.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblUutVolt_9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblUutVolt_9.ForeColor = System.Drawing.Color.White
        Me.lblUutVolt.SetIndex(Me.lblUutVolt_9, CType(9, Short))
        Me.lblUutVolt_9.Location = New System.Drawing.Point(409, 49)
        Me.lblUutVolt_9.Name = "lblUutVolt_9"
        Me.lblUutVolt_9.Size = New System.Drawing.Size(41, 19)
        Me.lblUutVolt_9.TabIndex = 187
        Me.lblUutVolt_9.Text = "00.00"
        '
        'lblUutVolt_8
        '
        Me.lblUutVolt_8.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblUutVolt_8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblUutVolt_8.ForeColor = System.Drawing.Color.White
        Me.lblUutVolt.SetIndex(Me.lblUutVolt_8, CType(8, Short))
        Me.lblUutVolt_8.Location = New System.Drawing.Point(364, 49)
        Me.lblUutVolt_8.Name = "lblUutVolt_8"
        Me.lblUutVolt_8.Size = New System.Drawing.Size(41, 19)
        Me.lblUutVolt_8.TabIndex = 186
        Me.lblUutVolt_8.Text = "00.00"
        '
        'lblUutVolt_7
        '
        Me.lblUutVolt_7.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblUutVolt_7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblUutVolt_7.ForeColor = System.Drawing.Color.White
        Me.lblUutVolt.SetIndex(Me.lblUutVolt_7, CType(7, Short))
        Me.lblUutVolt_7.Location = New System.Drawing.Point(320, 49)
        Me.lblUutVolt_7.Name = "lblUutVolt_7"
        Me.lblUutVolt_7.Size = New System.Drawing.Size(41, 19)
        Me.lblUutVolt_7.TabIndex = 185
        Me.lblUutVolt_7.Text = "00.00"
        '
        'lblUutVolt_6
        '
        Me.lblUutVolt_6.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblUutVolt_6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblUutVolt_6.ForeColor = System.Drawing.Color.White
        Me.lblUutVolt.SetIndex(Me.lblUutVolt_6, CType(6, Short))
        Me.lblUutVolt_6.Location = New System.Drawing.Point(275, 49)
        Me.lblUutVolt_6.Name = "lblUutVolt_6"
        Me.lblUutVolt_6.Size = New System.Drawing.Size(41, 19)
        Me.lblUutVolt_6.TabIndex = 184
        Me.lblUutVolt_6.Text = "00.00"
        '
        'lblUutVolt_5
        '
        Me.lblUutVolt_5.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblUutVolt_5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblUutVolt_5.ForeColor = System.Drawing.Color.White
        Me.lblUutVolt.SetIndex(Me.lblUutVolt_5, CType(5, Short))
        Me.lblUutVolt_5.Location = New System.Drawing.Point(231, 49)
        Me.lblUutVolt_5.Name = "lblUutVolt_5"
        Me.lblUutVolt_5.Size = New System.Drawing.Size(41, 19)
        Me.lblUutVolt_5.TabIndex = 183
        Me.lblUutVolt_5.Text = "00.00"
        '
        'lblUutVolt_4
        '
        Me.lblUutVolt_4.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblUutVolt_4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblUutVolt_4.ForeColor = System.Drawing.Color.White
        Me.lblUutVolt.SetIndex(Me.lblUutVolt_4, CType(4, Short))
        Me.lblUutVolt_4.Location = New System.Drawing.Point(186, 49)
        Me.lblUutVolt_4.Name = "lblUutVolt_4"
        Me.lblUutVolt_4.Size = New System.Drawing.Size(41, 19)
        Me.lblUutVolt_4.TabIndex = 182
        Me.lblUutVolt_4.Text = "00.00"
        '
        'lblUutVolt_3
        '
        Me.lblUutVolt_3.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblUutVolt_3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblUutVolt_3.ForeColor = System.Drawing.Color.White
        Me.lblUutVolt.SetIndex(Me.lblUutVolt_3, CType(3, Short))
        Me.lblUutVolt_3.Location = New System.Drawing.Point(142, 49)
        Me.lblUutVolt_3.Name = "lblUutVolt_3"
        Me.lblUutVolt_3.Size = New System.Drawing.Size(41, 19)
        Me.lblUutVolt_3.TabIndex = 181
        Me.lblUutVolt_3.Text = "00.00"
        '
        'lblUutVolt_2
        '
        Me.lblUutVolt_2.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblUutVolt_2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblUutVolt_2.ForeColor = System.Drawing.Color.White
        Me.lblUutVolt.SetIndex(Me.lblUutVolt_2, CType(2, Short))
        Me.lblUutVolt_2.Location = New System.Drawing.Point(97, 49)
        Me.lblUutVolt_2.Name = "lblUutVolt_2"
        Me.lblUutVolt_2.Size = New System.Drawing.Size(41, 19)
        Me.lblUutVolt_2.TabIndex = 180
        Me.lblUutVolt_2.Text = "00.00"
        '
        'lblUutVolt_1
        '
        Me.lblUutVolt_1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblUutVolt_1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblUutVolt_1.ForeColor = System.Drawing.Color.White
        Me.lblUutVolt.SetIndex(Me.lblUutVolt_1, CType(1, Short))
        Me.lblUutVolt_1.Location = New System.Drawing.Point(53, 49)
        Me.lblUutVolt_1.Name = "lblUutVolt_1"
        Me.lblUutVolt_1.Size = New System.Drawing.Size(41, 19)
        Me.lblUutVolt_1.TabIndex = 179
        Me.lblUutVolt_1.Text = "00.00"
        '
        'lblUut_1
        '
        Me.lblUut_1.AutoSize = True
        Me.lblUut_1.BackColor = System.Drawing.SystemColors.Control
        Me.lblUut_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUut.SetIndex(Me.lblUut_1, CType(1, Short))
        Me.lblUut_1.Location = New System.Drawing.Point(8, 32)
        Me.lblUut_1.Name = "lblUut_1"
        Me.lblUut_1.Size = New System.Drawing.Size(39, 13)
        Me.lblUut_1.TabIndex = 27
        Me.lblUut_1.Text = "Supply"
        '
        'lblUut_0
        '
        Me.lblUut_0.AutoSize = True
        Me.lblUut_0.BackColor = System.Drawing.SystemColors.Control
        Me.lblUut_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUut.SetIndex(Me.lblUut_0, CType(0, Short))
        Me.lblUut_0.Location = New System.Drawing.Point(8, 57)
        Me.lblUut_0.Name = "lblUut_0"
        Me.lblUut_0.Size = New System.Drawing.Size(43, 13)
        Me.lblUut_0.TabIndex = 26
        Me.lblUut_0.Text = "Voltage"
        '
        'lblUut_3
        '
        Me.lblUut_3.AutoSize = True
        Me.lblUut_3.BackColor = System.Drawing.SystemColors.Control
        Me.lblUut_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUut.SetIndex(Me.lblUut_3, CType(3, Short))
        Me.lblUut_3.Location = New System.Drawing.Point(8, 32)
        Me.lblUut_3.Name = "lblUut_3"
        Me.lblUut_3.Size = New System.Drawing.Size(39, 13)
        Me.lblUut_3.TabIndex = 30
        Me.lblUut_3.Text = "Supply"
        '
        'lblUut_2
        '
        Me.lblUut_2.AutoSize = True
        Me.lblUut_2.BackColor = System.Drawing.SystemColors.Control
        Me.lblUut_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUut.SetIndex(Me.lblUut_2, CType(2, Short))
        Me.lblUut_2.Location = New System.Drawing.Point(8, 57)
        Me.lblUut_2.Name = "lblUut_2"
        Me.lblUut_2.Size = New System.Drawing.Size(41, 13)
        Me.lblUut_2.TabIndex = 29
        Me.lblUut_2.Text = "Current"
        '
        'lblUutCur_10
        '
        Me.lblUutCur_10.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblUutCur_10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblUutCur_10.ForeColor = System.Drawing.Color.White
        Me.lblUutCur.SetIndex(Me.lblUutCur_10, CType(10, Short))
        Me.lblUutCur_10.Location = New System.Drawing.Point(453, 49)
        Me.lblUutCur_10.Name = "lblUutCur_10"
        Me.lblUutCur_10.Size = New System.Drawing.Size(41, 19)
        Me.lblUutCur_10.TabIndex = 198
        Me.lblUutCur_10.Text = "00.00"
        '
        'lblUutCur_9
        '
        Me.lblUutCur_9.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblUutCur_9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblUutCur_9.ForeColor = System.Drawing.Color.White
        Me.lblUutCur.SetIndex(Me.lblUutCur_9, CType(9, Short))
        Me.lblUutCur_9.Location = New System.Drawing.Point(409, 49)
        Me.lblUutCur_9.Name = "lblUutCur_9"
        Me.lblUutCur_9.Size = New System.Drawing.Size(41, 19)
        Me.lblUutCur_9.TabIndex = 197
        Me.lblUutCur_9.Text = "00.00"
        '
        'lblUutCur_8
        '
        Me.lblUutCur_8.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblUutCur_8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblUutCur_8.ForeColor = System.Drawing.Color.White
        Me.lblUutCur.SetIndex(Me.lblUutCur_8, CType(8, Short))
        Me.lblUutCur_8.Location = New System.Drawing.Point(364, 49)
        Me.lblUutCur_8.Name = "lblUutCur_8"
        Me.lblUutCur_8.Size = New System.Drawing.Size(41, 19)
        Me.lblUutCur_8.TabIndex = 196
        Me.lblUutCur_8.Text = "00.00"
        '
        'lblUutCur_7
        '
        Me.lblUutCur_7.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblUutCur_7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblUutCur_7.ForeColor = System.Drawing.Color.White
        Me.lblUutCur.SetIndex(Me.lblUutCur_7, CType(7, Short))
        Me.lblUutCur_7.Location = New System.Drawing.Point(320, 49)
        Me.lblUutCur_7.Name = "lblUutCur_7"
        Me.lblUutCur_7.Size = New System.Drawing.Size(41, 19)
        Me.lblUutCur_7.TabIndex = 195
        Me.lblUutCur_7.Text = "00.00"
        '
        'lblUutCur_6
        '
        Me.lblUutCur_6.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblUutCur_6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblUutCur_6.ForeColor = System.Drawing.Color.White
        Me.lblUutCur.SetIndex(Me.lblUutCur_6, CType(6, Short))
        Me.lblUutCur_6.Location = New System.Drawing.Point(275, 49)
        Me.lblUutCur_6.Name = "lblUutCur_6"
        Me.lblUutCur_6.Size = New System.Drawing.Size(41, 19)
        Me.lblUutCur_6.TabIndex = 194
        Me.lblUutCur_6.Text = "00.00"
        '
        'lblUutCur_5
        '
        Me.lblUutCur_5.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblUutCur_5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblUutCur_5.ForeColor = System.Drawing.Color.White
        Me.lblUutCur.SetIndex(Me.lblUutCur_5, CType(5, Short))
        Me.lblUutCur_5.Location = New System.Drawing.Point(231, 49)
        Me.lblUutCur_5.Name = "lblUutCur_5"
        Me.lblUutCur_5.Size = New System.Drawing.Size(41, 19)
        Me.lblUutCur_5.TabIndex = 193
        Me.lblUutCur_5.Text = "00.00"
        '
        'lblUutCur_4
        '
        Me.lblUutCur_4.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblUutCur_4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblUutCur_4.ForeColor = System.Drawing.Color.White
        Me.lblUutCur.SetIndex(Me.lblUutCur_4, CType(4, Short))
        Me.lblUutCur_4.Location = New System.Drawing.Point(186, 49)
        Me.lblUutCur_4.Name = "lblUutCur_4"
        Me.lblUutCur_4.Size = New System.Drawing.Size(41, 19)
        Me.lblUutCur_4.TabIndex = 192
        Me.lblUutCur_4.Text = "00.00"
        '
        'lblUutCur_3
        '
        Me.lblUutCur_3.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblUutCur_3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblUutCur_3.ForeColor = System.Drawing.Color.White
        Me.lblUutCur.SetIndex(Me.lblUutCur_3, CType(3, Short))
        Me.lblUutCur_3.Location = New System.Drawing.Point(142, 49)
        Me.lblUutCur_3.Name = "lblUutCur_3"
        Me.lblUutCur_3.Size = New System.Drawing.Size(41, 19)
        Me.lblUutCur_3.TabIndex = 191
        Me.lblUutCur_3.Text = "00.00"
        '
        'lblUutCur_2
        '
        Me.lblUutCur_2.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblUutCur_2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblUutCur_2.ForeColor = System.Drawing.Color.White
        Me.lblUutCur.SetIndex(Me.lblUutCur_2, CType(2, Short))
        Me.lblUutCur_2.Location = New System.Drawing.Point(97, 49)
        Me.lblUutCur_2.Name = "lblUutCur_2"
        Me.lblUutCur_2.Size = New System.Drawing.Size(41, 19)
        Me.lblUutCur_2.TabIndex = 190
        Me.lblUutCur_2.Text = "00.00"
        '
        'lblUutCur_1
        '
        Me.lblUutCur_1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblUutCur_1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblUutCur_1.ForeColor = System.Drawing.Color.White
        Me.lblUutCur.SetIndex(Me.lblUutCur_1, CType(1, Short))
        Me.lblUutCur_1.Location = New System.Drawing.Point(53, 49)
        Me.lblUutCur_1.Name = "lblUutCur_1"
        Me.lblUutCur_1.Size = New System.Drawing.Size(41, 19)
        Me.lblUutCur_1.TabIndex = 189
        Me.lblUutCur_1.Text = "00.00"
        '
        'lblSysPower_7
        '
        Me.lblSysPower_7.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblSysPower_7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSysPower_7.ForeColor = System.Drawing.Color.White
        Me.lblSysPower.SetIndex(Me.lblSysPower_7, CType(7, Short))
        Me.lblSysPower_7.Location = New System.Drawing.Point(101, 97)
        Me.lblSysPower_7.Name = "lblSysPower_7"
        Me.lblSysPower_7.Size = New System.Drawing.Size(62, 21)
        Me.lblSysPower_7.TabIndex = 235
        Me.lblSysPower_7.Text = "00.00"
        '
        'lblSysPower_6
        '
        Me.lblSysPower_6.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblSysPower_6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSysPower_6.ForeColor = System.Drawing.Color.White
        Me.lblSysPower.SetIndex(Me.lblSysPower_6, CType(6, Short))
        Me.lblSysPower_6.Location = New System.Drawing.Point(101, 61)
        Me.lblSysPower_6.Name = "lblSysPower_6"
        Me.lblSysPower_6.Size = New System.Drawing.Size(62, 21)
        Me.lblSysPower_6.TabIndex = 234
        Me.lblSysPower_6.Text = "00.00"
        '
        'lblSysPower_5
        '
        Me.lblSysPower_5.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblSysPower_5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSysPower_5.ForeColor = System.Drawing.Color.White
        Me.lblSysPower.SetIndex(Me.lblSysPower_5, CType(5, Short))
        Me.lblSysPower_5.Location = New System.Drawing.Point(101, 24)
        Me.lblSysPower_5.Name = "lblSysPower_5"
        Me.lblSysPower_5.Size = New System.Drawing.Size(62, 21)
        Me.lblSysPower_5.TabIndex = 233
        Me.lblSysPower_5.Text = "00.00"
        '
        'lblSysPower_0
        '
        Me.lblSysPower_0.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblSysPower_0.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSysPower_0.ForeColor = System.Drawing.Color.White
        Me.lblSysPower.SetIndex(Me.lblSysPower_0, CType(0, Short))
        Me.lblSysPower_0.Location = New System.Drawing.Point(105, 162)
        Me.lblSysPower_0.Name = "lblSysPower_0"
        Me.lblSysPower_0.Size = New System.Drawing.Size(70, 19)
        Me.lblSysPower_0.TabIndex = 361
        Me.lblSysPower_0.Text = "00.00"
        '
        'lblSysPower_4
        '
        Me.lblSysPower_4.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblSysPower_4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSysPower_4.ForeColor = System.Drawing.Color.White
        Me.lblSysPower.SetIndex(Me.lblSysPower_4, CType(4, Short))
        Me.lblSysPower_4.Location = New System.Drawing.Point(105, 129)
        Me.lblSysPower_4.Name = "lblSysPower_4"
        Me.lblSysPower_4.Size = New System.Drawing.Size(70, 19)
        Me.lblSysPower_4.TabIndex = 232
        Me.lblSysPower_4.Text = "00.00"
        '
        'lblSysPower_3
        '
        Me.lblSysPower_3.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblSysPower_3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSysPower_3.ForeColor = System.Drawing.Color.White
        Me.lblSysPower.SetIndex(Me.lblSysPower_3, CType(3, Short))
        Me.lblSysPower_3.Location = New System.Drawing.Point(105, 97)
        Me.lblSysPower_3.Name = "lblSysPower_3"
        Me.lblSysPower_3.Size = New System.Drawing.Size(70, 19)
        Me.lblSysPower_3.TabIndex = 231
        Me.lblSysPower_3.Text = "00.00"
        '
        'lblSysPower_2
        '
        Me.lblSysPower_2.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblSysPower_2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSysPower_2.ForeColor = System.Drawing.Color.White
        Me.lblSysPower.SetIndex(Me.lblSysPower_2, CType(2, Short))
        Me.lblSysPower_2.Location = New System.Drawing.Point(105, 65)
        Me.lblSysPower_2.Name = "lblSysPower_2"
        Me.lblSysPower_2.Size = New System.Drawing.Size(70, 19)
        Me.lblSysPower_2.TabIndex = 230
        Me.lblSysPower_2.Text = "00.00"
        '
        'lblSysPower_1
        '
        Me.lblSysPower_1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblSysPower_1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSysPower_1.ForeColor = System.Drawing.Color.White
        Me.lblSysPower.SetIndex(Me.lblSysPower_1, CType(1, Short))
        Me.lblSysPower_1.Location = New System.Drawing.Point(105, 32)
        Me.lblSysPower_1.Name = "lblSysPower_1"
        Me.lblSysPower_1.Size = New System.Drawing.Size(70, 19)
        Me.lblSysPower_1.TabIndex = 229
        Me.lblSysPower_1.Text = "0000.00 W"
        '
        'lblInputPower_17
        '
        Me.lblInputPower_17.AutoSize = True
        Me.lblInputPower_17.BackColor = System.Drawing.SystemColors.Control
        Me.lblInputPower_17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblInputPower.SetIndex(Me.lblInputPower_17, CType(17, Short))
        Me.lblInputPower_17.Location = New System.Drawing.Point(16, 28)
        Me.lblInputPower_17.Name = "lblInputPower_17"
        Me.lblInputPower_17.Size = New System.Drawing.Size(64, 13)
        Me.lblInputPower_17.TabIndex = 220
        Me.lblInputPower_17.Text = "Input Power"
        '
        'lblInputPower_11
        '
        Me.lblInputPower_11.AutoSize = True
        Me.lblInputPower_11.BackColor = System.Drawing.SystemColors.Control
        Me.lblInputPower_11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblInputPower.SetIndex(Me.lblInputPower_11, CType(11, Short))
        Me.lblInputPower_11.Location = New System.Drawing.Point(16, 57)
        Me.lblInputPower_11.Name = "lblInputPower_11"
        Me.lblInputPower_11.Size = New System.Drawing.Size(65, 26)
        Me.lblInputPower_11.TabIndex = 219
        Me.lblInputPower_11.Text = "TOTAL " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Power Used"
        '
        'lblInputPower_10
        '
        Me.lblInputPower_10.AutoSize = True
        Me.lblInputPower_10.BackColor = System.Drawing.SystemColors.Control
        Me.lblInputPower_10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblInputPower.SetIndex(Me.lblInputPower_10, CType(10, Short))
        Me.lblInputPower_10.Location = New System.Drawing.Point(16, 93)
        Me.lblInputPower_10.Name = "lblInputPower_10"
        Me.lblInputPower_10.Size = New System.Drawing.Size(83, 26)
        Me.lblInputPower_10.TabIndex = 218
        Me.lblInputPower_10.Text = "TOTAL " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Power Available"
        '
        'lblInputPower_16
        '
        Me.lblInputPower_16.AutoSize = True
        Me.lblInputPower_16.BackColor = System.Drawing.SystemColors.Control
        Me.lblInputPower_16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblInputPower.SetIndex(Me.lblInputPower_16, CType(16, Short))
        Me.lblInputPower_16.Location = New System.Drawing.Point(16, 162)
        Me.lblInputPower_16.Name = "lblInputPower_16"
        Me.lblInputPower_16.Size = New System.Drawing.Size(50, 13)
        Me.lblInputPower_16.TabIndex = 362
        Me.lblInputPower_16.Text = "IC Power"
        '
        'lblInputPower_15
        '
        Me.lblInputPower_15.AutoSize = True
        Me.lblInputPower_15.BackColor = System.Drawing.SystemColors.Control
        Me.lblInputPower_15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblInputPower.SetIndex(Me.lblInputPower_15, CType(15, Short))
        Me.lblInputPower_15.Location = New System.Drawing.Point(16, 32)
        Me.lblInputPower_15.Name = "lblInputPower_15"
        Me.lblInputPower_15.Size = New System.Drawing.Size(77, 13)
        Me.lblInputPower_15.TabIndex = 216
        Me.lblInputPower_15.Text = "Climate Control"
        '
        'lblInputPower_14
        '
        Me.lblInputPower_14.AutoSize = True
        Me.lblInputPower_14.BackColor = System.Drawing.SystemColors.Control
        Me.lblInputPower_14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblInputPower.SetIndex(Me.lblInputPower_14, CType(14, Short))
        Me.lblInputPower_14.Location = New System.Drawing.Point(16, 65)
        Me.lblInputPower_14.Name = "lblInputPower_14"
        Me.lblInputPower_14.Size = New System.Drawing.Size(71, 13)
        Me.lblInputPower_14.TabIndex = 215
        Me.lblInputPower_14.Text = "Heating Units"
        '
        'lblInputPower_13
        '
        Me.lblInputPower_13.AutoSize = True
        Me.lblInputPower_13.BackColor = System.Drawing.SystemColors.Control
        Me.lblInputPower_13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblInputPower.SetIndex(Me.lblInputPower_13, CType(13, Short))
        Me.lblInputPower_13.Location = New System.Drawing.Point(16, 97)
        Me.lblInputPower_13.Name = "lblInputPower_13"
        Me.lblInputPower_13.Size = New System.Drawing.Size(62, 13)
        Me.lblInputPower_13.TabIndex = 214
        Me.lblInputPower_13.Text = "PPU Power"
        '
        'lblInputPower_12
        '
        Me.lblInputPower_12.AutoSize = True
        Me.lblInputPower_12.BackColor = System.Drawing.SystemColors.Control
        Me.lblInputPower_12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblInputPower.SetIndex(Me.lblInputPower_12, CType(12, Short))
        Me.lblInputPower_12.Location = New System.Drawing.Point(16, 129)
        Me.lblInputPower_12.Name = "lblInputPower_12"
        Me.lblInputPower_12.Size = New System.Drawing.Size(61, 13)
        Me.lblInputPower_12.TabIndex = 213
        Me.lblInputPower_12.Text = "FPU Power"
        '
        'lblInputPower_7
        '
        Me.lblInputPower_7.AutoSize = True
        Me.lblInputPower_7.BackColor = System.Drawing.SystemColors.Control
        Me.lblInputPower_7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblInputPower.SetIndex(Me.lblInputPower_7, CType(7, Short))
        Me.lblInputPower_7.Location = New System.Drawing.Point(12, 150)
        Me.lblInputPower_7.Name = "lblInputPower_7"
        Me.lblInputPower_7.Size = New System.Drawing.Size(96, 13)
        Me.lblInputPower_7.TabIndex = 53
        Me.lblInputPower_7.Text = "2800W Converters"
        '
        'lblInputPower_3
        '
        Me.lblInputPower_3.AutoSize = True
        Me.lblInputPower_3.BackColor = System.Drawing.SystemColors.Control
        Me.lblInputPower_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblInputPower.SetIndex(Me.lblInputPower_3, CType(3, Short))
        Me.lblInputPower_3.Location = New System.Drawing.Point(12, 182)
        Me.lblInputPower_3.Name = "lblInputPower_3"
        Me.lblInputPower_3.Size = New System.Drawing.Size(97, 13)
        Me.lblInputPower_3.TabIndex = 52
        Me.lblInputPower_3.Text = "Input Power Status"
        '
        'lblInputPower_2
        '
        Me.lblInputPower_2.AutoSize = True
        Me.lblInputPower_2.BackColor = System.Drawing.SystemColors.Control
        Me.lblInputPower_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblInputPower.SetIndex(Me.lblInputPower_2, CType(2, Short))
        Me.lblInputPower_2.Location = New System.Drawing.Point(12, 109)
        Me.lblInputPower_2.Name = "lblInputPower_2"
        Me.lblInputPower_2.Size = New System.Drawing.Size(79, 13)
        Me.lblInputPower_2.TabIndex = 51
        Me.lblInputPower_2.Text = "AC Converter 3"
        '
        'lblInputPower_1
        '
        Me.lblInputPower_1.AutoSize = True
        Me.lblInputPower_1.BackColor = System.Drawing.SystemColors.Control
        Me.lblInputPower_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblInputPower.SetIndex(Me.lblInputPower_1, CType(1, Short))
        Me.lblInputPower_1.Location = New System.Drawing.Point(12, 81)
        Me.lblInputPower_1.Name = "lblInputPower_1"
        Me.lblInputPower_1.Size = New System.Drawing.Size(79, 13)
        Me.lblInputPower_1.TabIndex = 50
        Me.lblInputPower_1.Text = "AC Converter 2"
        '
        'lblInputPower_0
        '
        Me.lblInputPower_0.AutoSize = True
        Me.lblInputPower_0.BackColor = System.Drawing.SystemColors.Control
        Me.lblInputPower_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblInputPower.SetIndex(Me.lblInputPower_0, CType(0, Short))
        Me.lblInputPower_0.Location = New System.Drawing.Point(12, 53)
        Me.lblInputPower_0.Name = "lblInputPower_0"
        Me.lblInputPower_0.Size = New System.Drawing.Size(79, 13)
        Me.lblInputPower_0.TabIndex = 49
        Me.lblInputPower_0.Text = "AC Converter 1"
        '
        'lblInputPower_8
        '
        Me.lblInputPower_8.AutoSize = True
        Me.lblInputPower_8.BackColor = System.Drawing.SystemColors.Control
        Me.lblInputPower_8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblInputPower.SetIndex(Me.lblInputPower_8, CType(8, Short))
        Me.lblInputPower_8.Location = New System.Drawing.Point(12, 162)
        Me.lblInputPower_8.Name = "lblInputPower_8"
        Me.lblInputPower_8.Size = New System.Drawing.Size(88, 13)
        Me.lblInputPower_8.TabIndex = 360
        Me.lblInputPower_8.Text = "DC Current Level"
        '
        'lblInputPower_9
        '
        Me.lblInputPower_9.AutoSize = True
        Me.lblInputPower_9.BackColor = System.Drawing.SystemColors.Control
        Me.lblInputPower_9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblInputPower.SetIndex(Me.lblInputPower_9, CType(9, Short))
        Me.lblInputPower_9.Location = New System.Drawing.Point(12, 129)
        Me.lblInputPower_9.Name = "lblInputPower_9"
        Me.lblInputPower_9.Size = New System.Drawing.Size(90, 13)
        Me.lblInputPower_9.TabIndex = 55
        Me.lblInputPower_9.Text = "DC Voltage Level"
        '
        'lblInputPower_6
        '
        Me.lblInputPower_6.AutoSize = True
        Me.lblInputPower_6.BackColor = System.Drawing.SystemColors.Control
        Me.lblInputPower_6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblInputPower.SetIndex(Me.lblInputPower_6, CType(6, Short))
        Me.lblInputPower_6.Location = New System.Drawing.Point(12, 97)
        Me.lblInputPower_6.Name = "lblInputPower_6"
        Me.lblInputPower_6.Size = New System.Drawing.Size(85, 13)
        Me.lblInputPower_6.TabIndex = 43
        Me.lblInputPower_6.Text = "AC Input Current"
        '
        'lblInputPower_5
        '
        Me.lblInputPower_5.AutoSize = True
        Me.lblInputPower_5.BackColor = System.Drawing.SystemColors.Control
        Me.lblInputPower_5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblInputPower.SetIndex(Me.lblInputPower_5, CType(5, Short))
        Me.lblInputPower_5.Location = New System.Drawing.Point(12, 57)
        Me.lblInputPower_5.Name = "lblInputPower_5"
        Me.lblInputPower_5.Size = New System.Drawing.Size(72, 26)
        Me.lblInputPower_5.TabIndex = 42
        Me.lblInputPower_5.Text = "Three Phase " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Voltage Level"
        '
        'lblInputPower_4
        '
        Me.lblInputPower_4.AutoSize = True
        Me.lblInputPower_4.BackColor = System.Drawing.SystemColors.Control
        Me.lblInputPower_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblInputPower.SetIndex(Me.lblInputPower_4, CType(4, Short))
        Me.lblInputPower_4.Location = New System.Drawing.Point(12, 24)
        Me.lblInputPower_4.Name = "lblInputPower_4"
        Me.lblInputPower_4.Size = New System.Drawing.Size(72, 26)
        Me.lblInputPower_4.TabIndex = 41
        Me.lblInputPower_4.Text = "Single Phase" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Voltage Level"
        '
        'lblInPower_4
        '
        Me.lblInPower_4.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblInPower_4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblInPower_4.ForeColor = System.Drawing.Color.White
        Me.lblInPower.SetIndex(Me.lblInPower_4, CType(4, Short))
        Me.lblInPower_4.Location = New System.Drawing.Point(113, 162)
        Me.lblInPower_4.Name = "lblInPower_4"
        Me.lblInPower_4.Size = New System.Drawing.Size(55, 19)
        Me.lblInPower_4.TabIndex = 359
        Me.lblInPower_4.Text = "00.00"
        '
        'lblInPower_3
        '
        Me.lblInPower_3.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblInPower_3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblInPower_3.ForeColor = System.Drawing.Color.White
        Me.lblInPower.SetIndex(Me.lblInPower_3, CType(3, Short))
        Me.lblInPower_3.Location = New System.Drawing.Point(113, 129)
        Me.lblInPower_3.Name = "lblInPower_3"
        Me.lblInPower_3.Size = New System.Drawing.Size(55, 19)
        Me.lblInPower_3.TabIndex = 239
        Me.lblInPower_3.Text = "00.00"
        '
        'lblInPower_2
        '
        Me.lblInPower_2.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblInPower_2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblInPower_2.ForeColor = System.Drawing.Color.White
        Me.lblInPower.SetIndex(Me.lblInPower_2, CType(2, Short))
        Me.lblInPower_2.Location = New System.Drawing.Point(113, 97)
        Me.lblInPower_2.Name = "lblInPower_2"
        Me.lblInPower_2.Size = New System.Drawing.Size(55, 19)
        Me.lblInPower_2.TabIndex = 238
        Me.lblInPower_2.Text = "00.00"
        '
        'lblInPower_1
        '
        Me.lblInPower_1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblInPower_1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblInPower_1.ForeColor = System.Drawing.Color.White
        Me.lblInPower.SetIndex(Me.lblInPower_1, CType(1, Short))
        Me.lblInPower_1.Location = New System.Drawing.Point(113, 65)
        Me.lblInPower_1.Name = "lblInPower_1"
        Me.lblInPower_1.Size = New System.Drawing.Size(55, 19)
        Me.lblInPower_1.TabIndex = 237
        Me.lblInPower_1.Text = "00.00"
        '
        'lblInPower_0
        '
        Me.lblInPower_0.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblInPower_0.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblInPower_0.ForeColor = System.Drawing.Color.White
        Me.lblInPower.SetIndex(Me.lblInPower_0, CType(0, Short))
        Me.lblInPower_0.Location = New System.Drawing.Point(113, 32)
        Me.lblInPower_0.Name = "lblInPower_0"
        Me.lblInPower_0.Size = New System.Drawing.Size(55, 19)
        Me.lblInPower_0.TabIndex = 236
        Me.lblInPower_0.Text = "00.00"
        '
        'lblmode_9
        '
        Me.lblmode_9.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblmode_9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblmode_9.ForeColor = System.Drawing.Color.White
        Me.lblmode.SetIndex(Me.lblmode_9, CType(9, Short))
        Me.lblmode_9.Location = New System.Drawing.Point(97, 69)
        Me.lblmode_9.Name = "lblmode_9"
        Me.lblmode_9.Size = New System.Drawing.Size(80, 17)
        Me.lblmode_9.TabIndex = 434
        Me.lblmode_9.Text = "00000:00:00"
        '
        'lblmode_8
        '
        Me.lblmode_8.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblmode_8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblmode_8.ForeColor = System.Drawing.Color.White
        Me.lblmode.SetIndex(Me.lblmode_8, CType(8, Short))
        Me.lblmode_8.Location = New System.Drawing.Point(97, 44)
        Me.lblmode_8.Name = "lblmode_8"
        Me.lblmode_8.Size = New System.Drawing.Size(80, 17)
        Me.lblmode_8.TabIndex = 433
        Me.lblmode_8.Text = "00000:00:00"
        '
        'lblmode_7
        '
        Me.lblmode_7.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblmode_7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblmode_7.ForeColor = System.Drawing.Color.White
        Me.lblmode.SetIndex(Me.lblmode_7, CType(7, Short))
        Me.lblmode_7.Location = New System.Drawing.Point(97, 20)
        Me.lblmode_7.Name = "lblmode_7"
        Me.lblmode_7.Size = New System.Drawing.Size(80, 17)
        Me.lblmode_7.TabIndex = 432
        Me.lblmode_7.Text = "00000:00:00"
        '
        'lblmode_0
        '
        Me.lblmode_0.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblmode_0.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblmode_0.ForeColor = System.Drawing.Color.White
        Me.lblmode.SetIndex(Me.lblmode_0, CType(0, Short))
        Me.lblmode_0.Location = New System.Drawing.Point(97, 97)
        Me.lblmode_0.Name = "lblmode_0"
        Me.lblmode_0.Size = New System.Drawing.Size(80, 17)
        Me.lblmode_0.TabIndex = 431
        Me.lblmode_0.Text = "00000:00:00"
        '
        'lblmode_6
        '
        Me.lblmode_6.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblmode_6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblmode_6.ForeColor = System.Drawing.Color.White
        Me.lblmode.SetIndex(Me.lblmode_6, CType(6, Short))
        Me.lblmode_6.Location = New System.Drawing.Point(133, 125)
        Me.lblmode_6.Name = "lblmode_6"
        Me.lblmode_6.Size = New System.Drawing.Size(41, 17)
        Me.lblmode_6.TabIndex = 395
        Me.lblmode_6.Text = "Auto"
        '
        'lblmode_2
        '
        Me.lblmode_2.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblmode_2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblmode_2.ForeColor = System.Drawing.Color.White
        Me.lblmode.SetIndex(Me.lblmode_2, CType(2, Short))
        Me.lblmode_2.Location = New System.Drawing.Point(133, 73)
        Me.lblmode_2.Name = "lblmode_2"
        Me.lblmode_2.Size = New System.Drawing.Size(41, 17)
        Me.lblmode_2.TabIndex = 392
        Me.lblmode_2.Text = "Auto"
        '
        'lblmode_4
        '
        Me.lblmode_4.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblmode_4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblmode_4.ForeColor = System.Drawing.Color.White
        Me.lblmode.SetIndex(Me.lblmode_4, CType(4, Short))
        Me.lblmode_4.Location = New System.Drawing.Point(133, 20)
        Me.lblmode_4.Name = "lblmode_4"
        Me.lblmode_4.Size = New System.Drawing.Size(41, 17)
        Me.lblmode_4.TabIndex = 385
        Me.lblmode_4.Text = "Auto"
        '
        'lblmode_5
        '
        Me.lblmode_5.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblmode_5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblmode_5.ForeColor = System.Drawing.Color.White
        Me.lblmode.SetIndex(Me.lblmode_5, CType(5, Short))
        Me.lblmode_5.Location = New System.Drawing.Point(133, 125)
        Me.lblmode_5.Name = "lblmode_5"
        Me.lblmode_5.Size = New System.Drawing.Size(41, 17)
        Me.lblmode_5.TabIndex = 377
        Me.lblmode_5.Text = "Auto"
        '
        'lblmode_1
        '
        Me.lblmode_1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblmode_1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblmode_1.ForeColor = System.Drawing.Color.White
        Me.lblmode.SetIndex(Me.lblmode_1, CType(1, Short))
        Me.lblmode_1.Location = New System.Drawing.Point(133, 73)
        Me.lblmode_1.Name = "lblmode_1"
        Me.lblmode_1.Size = New System.Drawing.Size(41, 17)
        Me.lblmode_1.TabIndex = 374
        Me.lblmode_1.Text = "Auto"
        '
        'lblmode_3
        '
        Me.lblmode_3.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblmode_3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblmode_3.ForeColor = System.Drawing.Color.White
        Me.lblmode.SetIndex(Me.lblmode_3, CType(3, Short))
        Me.lblmode_3.Location = New System.Drawing.Point(133, 24)
        Me.lblmode_3.Name = "lblmode_3"
        Me.lblmode_3.Size = New System.Drawing.Size(41, 17)
        Me.lblmode_3.TabIndex = 367
        Me.lblmode_3.Text = "Auto"
        '
        'TextBox
        '
        '
        'chkHeater_2
        '
        Me.chkHeater_2.Enabled = False
        Me.chkHeater_2.Location = New System.Drawing.Point(77, 146)
        Me.chkHeater_2.Name = "chkHeater_2"
        Me.chkHeater_2.Size = New System.Drawing.Size(102, 17)
        Me.chkHeater_2.TabIndex = 398
        Me.chkHeater_2.Text = "Heating Unit #1"
        '
        'chkHeater_3
        '
        Me.chkHeater_3.Enabled = False
        Me.chkHeater_3.Location = New System.Drawing.Point(77, 162)
        Me.chkHeater_3.Name = "chkHeater_3"
        Me.chkHeater_3.Size = New System.Drawing.Size(102, 17)
        Me.chkHeater_3.TabIndex = 397
        Me.chkHeater_3.Text = "Heating Unit #2"
        '
        'chkHeater_0
        '
        Me.chkHeater_0.Enabled = False
        Me.chkHeater_0.Location = New System.Drawing.Point(77, 146)
        Me.chkHeater_0.Name = "chkHeater_0"
        Me.chkHeater_0.Size = New System.Drawing.Size(105, 17)
        Me.chkHeater_0.TabIndex = 380
        Me.chkHeater_0.Text = "Heating Unit #1"
        '
        'chkHeater_1
        '
        Me.chkHeater_1.Enabled = False
        Me.chkHeater_1.Location = New System.Drawing.Point(77, 162)
        Me.chkHeater_1.Name = "chkHeater_1"
        Me.chkHeater_1.Size = New System.Drawing.Size(105, 17)
        Me.chkHeater_1.TabIndex = 379
        Me.chkHeater_1.Text = "Heating Unit #2"
        '
        'chkEnableChassisOption_6
        '
        Me.chkEnableChassisOption_6.Location = New System.Drawing.Point(12, 154)
        Me.chkEnableChassisOption_6.Name = "chkEnableChassisOption_6"
        Me.chkEnableChassisOption_6.Size = New System.Drawing.Size(62, 17)
        Me.chkEnableChassisOption_6.TabIndex = 396
        Me.chkEnableChassisOption_6.Text = "Manual"
        '
        'chkEnableChassisOption_2
        '
        Me.chkEnableChassisOption_2.Location = New System.Drawing.Point(12, 101)
        Me.chkEnableChassisOption_2.Name = "chkEnableChassisOption_2"
        Me.chkEnableChassisOption_2.Size = New System.Drawing.Size(62, 17)
        Me.chkEnableChassisOption_2.TabIndex = 393
        Me.chkEnableChassisOption_2.Text = "Manual"
        '
        'chkEnableChassisOption_4
        '
        Me.chkEnableChassisOption_4.Location = New System.Drawing.Point(12, 49)
        Me.chkEnableChassisOption_4.Name = "chkEnableChassisOption_4"
        Me.chkEnableChassisOption_4.Size = New System.Drawing.Size(62, 17)
        Me.chkEnableChassisOption_4.TabIndex = 386
        Me.chkEnableChassisOption_4.Text = "Manual"
        '
        'chkEnableChassisOption_5
        '
        Me.chkEnableChassisOption_5.Location = New System.Drawing.Point(12, 154)
        Me.chkEnableChassisOption_5.Name = "chkEnableChassisOption_5"
        Me.chkEnableChassisOption_5.Size = New System.Drawing.Size(62, 17)
        Me.chkEnableChassisOption_5.TabIndex = 378
        Me.chkEnableChassisOption_5.Text = "Manual"
        '
        'chkEnableChassisOption_1
        '
        Me.chkEnableChassisOption_1.Location = New System.Drawing.Point(12, 101)
        Me.chkEnableChassisOption_1.Name = "chkEnableChassisOption_1"
        Me.chkEnableChassisOption_1.Size = New System.Drawing.Size(62, 17)
        Me.chkEnableChassisOption_1.TabIndex = 375
        Me.chkEnableChassisOption_1.Text = "Manual"
        '
        'chkEnableChassisOption_3
        '
        Me.chkEnableChassisOption_3.Location = New System.Drawing.Point(12, 49)
        Me.chkEnableChassisOption_3.Name = "chkEnableChassisOption_3"
        Me.chkEnableChassisOption_3.Size = New System.Drawing.Size(62, 17)
        Me.chkEnableChassisOption_3.TabIndex = 368
        Me.chkEnableChassisOption_3.Text = "Manual"
        '
        'Label1_5
        '
        Me.Label1_5.BackColor = System.Drawing.SystemColors.Control
        Me.Label1_5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1_5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.SetIndex(Me.Label1_5, CType(5, Short))
        Me.Label1_5.Location = New System.Drawing.Point(12, 129)
        Me.Label1_5.Name = "Label1_5"
        Me.Label1_5.Size = New System.Drawing.Size(70, 13)
        Me.Label1_5.TabIndex = 404
        Me.Label1_5.Text = "Heater State"
        '
        'Label1_4
        '
        Me.Label1_4.BackColor = System.Drawing.SystemColors.Control
        Me.Label1_4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.SetIndex(Me.Label1_4, CType(4, Short))
        Me.Label1_4.Location = New System.Drawing.Point(12, 77)
        Me.Label1_4.Name = "Label1_4"
        Me.Label1_4.Size = New System.Drawing.Size(70, 13)
        Me.Label1_4.TabIndex = 403
        Me.Label1_4.Text = "Fan Speed"
        '
        'Label1_3
        '
        Me.Label1_3.AutoSize = True
        Me.Label1_3.BackColor = System.Drawing.SystemColors.Control
        Me.Label1_3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.SetIndex(Me.Label1_3, CType(3, Short))
        Me.Label1_3.Location = New System.Drawing.Point(12, 24)
        Me.Label1_3.Name = "Label1_3"
        Me.Label1_3.Size = New System.Drawing.Size(111, 13)
        Me.Label1_3.TabIndex = 402
        Me.Label1_3.Text = "Optimum Temperature"
        '
        'Label1_2
        '
        Me.Label1_2.BackColor = System.Drawing.SystemColors.Control
        Me.Label1_2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.SetIndex(Me.Label1_2, CType(2, Short))
        Me.Label1_2.Location = New System.Drawing.Point(12, 129)
        Me.Label1_2.Name = "Label1_2"
        Me.Label1_2.Size = New System.Drawing.Size(70, 13)
        Me.Label1_2.TabIndex = 401
        Me.Label1_2.Text = "Heater State"
        '
        'Label1_1
        '
        Me.Label1_1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1_1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.SetIndex(Me.Label1_1, CType(1, Short))
        Me.Label1_1.Location = New System.Drawing.Point(12, 77)
        Me.Label1_1.Name = "Label1_1"
        Me.Label1_1.Size = New System.Drawing.Size(70, 13)
        Me.Label1_1.TabIndex = 400
        Me.Label1_1.Text = "Fan Speed"
        '
        'Label1_0
        '
        Me.Label1_0.AutoSize = True
        Me.Label1_0.BackColor = System.Drawing.SystemColors.Control
        Me.Label1_0.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.SetIndex(Me.Label1_0, CType(0, Short))
        Me.Label1_0.Location = New System.Drawing.Point(12, 24)
        Me.Label1_0.Name = "Label1_0"
        Me.Label1_0.Size = New System.Drawing.Size(111, 13)
        Me.Label1_0.TabIndex = 399
        Me.Label1_0.Text = "Optimum Temperature"
        '
        'cmdCalChass
        '
        '
        'cmdCalChass_1
        '
        Me.cmdCalChass_1.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCalChass_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCalChass.SetIndex(Me.cmdCalChass_1, CType(1, Short))
        Me.cmdCalChass_1.Location = New System.Drawing.Point(8, 24)
        Me.cmdCalChass_1.Name = "cmdCalChass_1"
        Me.cmdCalChass_1.Size = New System.Drawing.Size(155, 23)
        Me.cmdCalChass_1.TabIndex = 200
        Me.cmdCalChass_1.Text = "Primary Chassis Sensors"
        Me.cmdCalChass_1.UseVisualStyleBackColor = False
        '
        'cmdCalChass_2
        '
        Me.cmdCalChass_2.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCalChass_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCalChass.SetIndex(Me.cmdCalChass_2, CType(2, Short))
        Me.cmdCalChass_2.Location = New System.Drawing.Point(8, 73)
        Me.cmdCalChass_2.Name = "cmdCalChass_2"
        Me.cmdCalChass_2.Size = New System.Drawing.Size(155, 23)
        Me.cmdCalChass_2.TabIndex = 199
        Me.cmdCalChass_2.Text = "Secondary Chassis Sensors"
        Me.cmdCalChass_2.UseVisualStyleBackColor = False
        '
        'chkPpuNoFault_10
        '
        Me.chkPpuNoFault_10.Location = New System.Drawing.Point(20, 200)
        Me.chkPpuNoFault_10.Name = "chkPpuNoFault_10"
        Me.chkPpuNoFault_10.Size = New System.Drawing.Size(100, 17)
        Me.chkPpuNoFault_10.TabIndex = 89
        Me.chkPpuNoFault_10.Tag = "10"
        Me.chkPpuNoFault_10.Text = "PPU Supply 10"
        '
        'chkPpuNoFault_9
        '
        Me.chkPpuNoFault_9.Location = New System.Drawing.Point(20, 180)
        Me.chkPpuNoFault_9.Name = "chkPpuNoFault_9"
        Me.chkPpuNoFault_9.Size = New System.Drawing.Size(100, 17)
        Me.chkPpuNoFault_9.TabIndex = 88
        Me.chkPpuNoFault_9.Tag = "9"
        Me.chkPpuNoFault_9.Text = "PPU Supply 9"
        '
        'chkPpuNoFault_8
        '
        Me.chkPpuNoFault_8.Location = New System.Drawing.Point(20, 160)
        Me.chkPpuNoFault_8.Name = "chkPpuNoFault_8"
        Me.chkPpuNoFault_8.Size = New System.Drawing.Size(100, 17)
        Me.chkPpuNoFault_8.TabIndex = 87
        Me.chkPpuNoFault_8.Tag = "8"
        Me.chkPpuNoFault_8.Text = "PPU Supply 8"
        '
        'chkPpuNoFault_7
        '
        Me.chkPpuNoFault_7.Location = New System.Drawing.Point(20, 140)
        Me.chkPpuNoFault_7.Name = "chkPpuNoFault_7"
        Me.chkPpuNoFault_7.Size = New System.Drawing.Size(100, 17)
        Me.chkPpuNoFault_7.TabIndex = 86
        Me.chkPpuNoFault_7.Tag = "7"
        Me.chkPpuNoFault_7.Text = "PPU Supply 7"
        '
        'chkPpuNoFault_6
        '
        Me.chkPpuNoFault_6.Location = New System.Drawing.Point(20, 120)
        Me.chkPpuNoFault_6.Name = "chkPpuNoFault_6"
        Me.chkPpuNoFault_6.Size = New System.Drawing.Size(100, 17)
        Me.chkPpuNoFault_6.TabIndex = 85
        Me.chkPpuNoFault_6.Tag = "6"
        Me.chkPpuNoFault_6.Text = "PPU Supply 6"
        '
        'chkPpuNoFault_5
        '
        Me.chkPpuNoFault_5.Location = New System.Drawing.Point(20, 100)
        Me.chkPpuNoFault_5.Name = "chkPpuNoFault_5"
        Me.chkPpuNoFault_5.Size = New System.Drawing.Size(100, 17)
        Me.chkPpuNoFault_5.TabIndex = 84
        Me.chkPpuNoFault_5.Tag = "5"
        Me.chkPpuNoFault_5.Text = "PPU Supply 5"
        '
        'chkPpuNoFault_4
        '
        Me.chkPpuNoFault_4.Location = New System.Drawing.Point(20, 80)
        Me.chkPpuNoFault_4.Name = "chkPpuNoFault_4"
        Me.chkPpuNoFault_4.Size = New System.Drawing.Size(100, 17)
        Me.chkPpuNoFault_4.TabIndex = 83
        Me.chkPpuNoFault_4.Tag = "4"
        Me.chkPpuNoFault_4.Text = "PPU Supply 4"
        '
        'chkPpuNoFault_3
        '
        Me.chkPpuNoFault_3.Location = New System.Drawing.Point(20, 60)
        Me.chkPpuNoFault_3.Name = "chkPpuNoFault_3"
        Me.chkPpuNoFault_3.Size = New System.Drawing.Size(100, 17)
        Me.chkPpuNoFault_3.TabIndex = 82
        Me.chkPpuNoFault_3.Tag = "3"
        Me.chkPpuNoFault_3.Text = "PPU Supply 3"
        '
        'chkPpuNoFault_2
        '
        Me.chkPpuNoFault_2.Location = New System.Drawing.Point(20, 40)
        Me.chkPpuNoFault_2.Name = "chkPpuNoFault_2"
        Me.chkPpuNoFault_2.Size = New System.Drawing.Size(100, 17)
        Me.chkPpuNoFault_2.TabIndex = 81
        Me.chkPpuNoFault_2.Tag = "2"
        Me.chkPpuNoFault_2.Text = "PPU Supply 2"
        '
        'chkPpuNoFault_1
        '
        Me.chkPpuNoFault_1.Location = New System.Drawing.Point(20, 20)
        Me.chkPpuNoFault_1.Name = "chkPpuNoFault_1"
        Me.chkPpuNoFault_1.Size = New System.Drawing.Size(100, 17)
        Me.chkPpuNoFault_1.TabIndex = 80
        Me.chkPpuNoFault_1.Tag = "1"
        Me.chkPpuNoFault_1.Text = "PPU Supply 1"
        '
        'cmdResetChassis
        '
        '
        'cmdResetChassis_1
        '
        Me.cmdResetChassis_1.BackColor = System.Drawing.SystemColors.Control
        Me.cmdResetChassis_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdResetChassis.SetIndex(Me.cmdResetChassis_1, CType(1, Short))
        Me.cmdResetChassis_1.Location = New System.Drawing.Point(421, 336)
        Me.cmdResetChassis_1.Name = "cmdResetChassis_1"
        Me.cmdResetChassis_1.Size = New System.Drawing.Size(115, 23)
        Me.cmdResetChassis_1.TabIndex = 56
        Me.cmdResetChassis_1.Text = "Reset &Prim. Chassis"
        Me.cmdResetChassis_1.UseVisualStyleBackColor = False
        '
        'cmdResetChassis_2
        '
        Me.cmdResetChassis_2.BackColor = System.Drawing.SystemColors.Control
        Me.cmdResetChassis_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdResetChassis.SetIndex(Me.cmdResetChassis_2, CType(2, Short))
        Me.cmdResetChassis_2.Location = New System.Drawing.Point(421, 312)
        Me.cmdResetChassis_2.Name = "cmdResetChassis_2"
        Me.cmdResetChassis_2.Size = New System.Drawing.Size(115, 23)
        Me.cmdResetChassis_2.TabIndex = 74
        Me.cmdResetChassis_2.Text = "Reset S&ec. Chassis"
        Me.cmdResetChassis_2.UseVisualStyleBackColor = False
        '
        'cmdResetPdu
        '
        '
        'cmdResetPdu_0
        '
        Me.cmdResetPdu_0.BackColor = System.Drawing.SystemColors.Control
        Me.cmdResetPdu_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdResetPdu.SetIndex(Me.cmdResetPdu_0, CType(0, Short))
        Me.cmdResetPdu_0.Location = New System.Drawing.Point(421, 360)
        Me.cmdResetPdu_0.Name = "cmdResetPdu_0"
        Me.cmdResetPdu_0.Size = New System.Drawing.Size(115, 23)
        Me.cmdResetPdu_0.TabIndex = 356
        Me.cmdResetPdu_0.Text = "&Reset PDU"
        Me.cmdResetPdu_0.UseVisualStyleBackColor = False
        '
        'ETITimer
        '
        Me.ETITimer.Interval = 1000
        '
        'tmrUpdateStop
        '
        Me.tmrUpdateStop.Interval = 60000
        '
        'cmdAbout
        '
        Me.cmdAbout.BackColor = System.Drawing.SystemColors.Control
        Me.cmdAbout.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAbout.Location = New System.Drawing.Point(320, 433)
        Me.cmdAbout.Name = "cmdAbout"
        Me.cmdAbout.Size = New System.Drawing.Size(76, 23)
        Me.cmdAbout.TabIndex = 311
        Me.cmdAbout.Text = "&About"
        Me.cmdAbout.UseVisualStyleBackColor = False
        '
        'cmdShutDown
        '
        Me.cmdShutDown.BackColor = System.Drawing.SystemColors.Control
        Me.cmdShutDown.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdShutDown.Location = New System.Drawing.Point(409, 433)
        Me.cmdShutDown.Name = "cmdShutDown"
        Me.cmdShutDown.Size = New System.Drawing.Size(76, 23)
        Me.cmdShutDown.TabIndex = 71
        Me.cmdShutDown.Text = "&Shut Down"
        Me.cmdShutDown.UseVisualStyleBackColor = False
        '
        'tmrDataPoll
        '
        Me.tmrDataPoll.Interval = 500
        '
        'cmdQuitSysMon
        '
        Me.cmdQuitSysMon.BackColor = System.Drawing.SystemColors.Control
        Me.cmdQuitSysMon.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdQuitSysMon.Location = New System.Drawing.Point(498, 433)
        Me.cmdQuitSysMon.Name = "cmdQuitSysMon"
        Me.cmdQuitSysMon.Size = New System.Drawing.Size(76, 23)
        Me.cmdQuitSysMon.TabIndex = 1
        Me.cmdQuitSysMon.Text = "&Minimize"
        Me.cmdQuitSysMon.UseVisualStyleBackColor = False
        '
        'tabSysmon
        '
        Me.tabSysmon.Controls.Add(Me.tabSysmon_Page1)
        Me.tabSysmon.Controls.Add(Me.tabSysmon_Page2)
        Me.tabSysmon.Controls.Add(Me.tabSysmon_Page3)
        Me.tabSysmon.Controls.Add(Me.tabSysmon_Page4)
        Me.tabSysmon.Controls.Add(Me.tabSysmon_Page5)
        Me.tabSysmon.Controls.Add(Me.tabSysmon_Page6)
        Me.tabSysmon.Controls.Add(Me.tabSysmon_Page7)
        Me.tabSysmon.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tabSysmon.Location = New System.Drawing.Point(8, 8)
        Me.tabSysmon.Name = "tabSysmon"
        Me.tabSysmon.SelectedIndex = 0
        Me.tabSysmon.Size = New System.Drawing.Size(586, 418)
        Me.tabSysmon.TabIndex = 0
        '
        'tabSysmon_Page1
        '
        Me.tabSysmon_Page1.Controls.Add(Me.fraChasTemp_2)
        Me.tabSysmon_Page1.Controls.Add(Me.cmdResetThresholds_1)
        Me.tabSysmon_Page1.Controls.Add(Me.fraChasTemp_1)
        Me.tabSysmon_Page1.Controls.Add(Me.fraChasTemp_0)
        Me.tabSysmon_Page1.Controls.Add(Me.cmdSaveThresholds_1)
        Me.tabSysmon_Page1.Location = New System.Drawing.Point(4, 22)
        Me.tabSysmon_Page1.Name = "tabSysmon_Page1"
        Me.tabSysmon_Page1.Size = New System.Drawing.Size(578, 392)
        Me.tabSysmon_Page1.TabIndex = 0
        Me.tabSysmon_Page1.Text = "Prim. Chassis Temp"
        '
        'fraChasTemp_2
        '
        Me.fraChasTemp_2.Controls.Add(Me.picIconGraphic_1)
        Me.fraChasTemp_2.Controls.Add(Me.lblIntakeValue_0)
        Me.fraChasTemp_2.Controls.Add(Me.lblUnit_7)
        Me.fraChasTemp_2.Controls.Add(Me.lblUnit_8)
        Me.fraChasTemp_2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraChasTemp_2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraChasTemp_2.Location = New System.Drawing.Point(8, 300)
        Me.fraChasTemp_2.Name = "fraChasTemp_2"
        Me.fraChasTemp_2.Size = New System.Drawing.Size(187, 58)
        Me.fraChasTemp_2.TabIndex = 9
        Me.fraChasTemp_2.TabStop = False
        Me.fraChasTemp_2.Text = "Chassis Intake Temperature"
        '
        'fraChasTemp_1
        '
        Me.fraChasTemp_1.Controls.Add(Me.lblChassTemp_25)
        Me.fraChasTemp_1.Controls.Add(Me.cwsSlotRise_12)
        Me.fraChasTemp_1.Controls.Add(Me.dummySlide_1)
        Me.fraChasTemp_1.Controls.Add(Me.lblChassTemp_13)
        Me.fraChasTemp_1.Controls.Add(Me.cwsSlotRiseActual_0)
        Me.fraChasTemp_1.Controls.Add(Me.dummySlide_0)
        Me.fraChasTemp_1.Controls.Add(Me.cwsSlotRiseActual_12)
        Me.fraChasTemp_1.Controls.Add(Me.cwsSlotRiseActual_11)
        Me.fraChasTemp_1.Controls.Add(Me.cwsSlotRiseActual_10)
        Me.fraChasTemp_1.Controls.Add(Me.cwsSlotRiseActual_9)
        Me.fraChasTemp_1.Controls.Add(Me.cwsSlotRiseActual_8)
        Me.fraChasTemp_1.Controls.Add(Me.cwsSlotRiseActual_7)
        Me.fraChasTemp_1.Controls.Add(Me.cwsSlotRiseActual_6)
        Me.fraChasTemp_1.Controls.Add(Me.cwsSlotRiseActual_5)
        Me.fraChasTemp_1.Controls.Add(Me.cwsSlotRiseActual_4)
        Me.fraChasTemp_1.Controls.Add(Me.cwsSlotRiseActual_3)
        Me.fraChasTemp_1.Controls.Add(Me.cwsSlotRiseActual_2)
        Me.fraChasTemp_1.Controls.Add(Me.cwsSlotRise_1)
        Me.fraChasTemp_1.Controls.Add(Me.cwsSlotRiseActual_1)
        Me.fraChasTemp_1.Controls.Add(Me.lblChassTemp_14)
        Me.fraChasTemp_1.Controls.Add(Me.cwsSlotRise_11)
        Me.fraChasTemp_1.Controls.Add(Me.cwsSlotRise_10)
        Me.fraChasTemp_1.Controls.Add(Me.cwsSlotRise_9)
        Me.fraChasTemp_1.Controls.Add(Me.cwsSlotRise_8)
        Me.fraChasTemp_1.Controls.Add(Me.cwsSlotRise_7)
        Me.fraChasTemp_1.Controls.Add(Me.cwsSlotRise_6)
        Me.fraChasTemp_1.Controls.Add(Me.cwsSlotRise_5)
        Me.fraChasTemp_1.Controls.Add(Me.cwsSlotRise_4)
        Me.fraChasTemp_1.Controls.Add(Me.cwsSlotRise_3)
        Me.fraChasTemp_1.Controls.Add(Me.cwsSlotRise_2)
        Me.fraChasTemp_1.Controls.Add(Me.cwsSlotRise_0)
        Me.fraChasTemp_1.Controls.Add(Me.linRise_11)
        Me.fraChasTemp_1.Controls.Add(Me.linRise_0)
        Me.fraChasTemp_1.Controls.Add(Me.linRise_4)
        Me.fraChasTemp_1.Controls.Add(Me.lblChassTemp_24)
        Me.fraChasTemp_1.Controls.Add(Me.lblChassTemp_23)
        Me.fraChasTemp_1.Controls.Add(Me.lblChassTemp_22)
        Me.fraChasTemp_1.Controls.Add(Me.lblChassTemp_21)
        Me.fraChasTemp_1.Controls.Add(Me.lblChassTemp_20)
        Me.fraChasTemp_1.Controls.Add(Me.lblChassTemp_19)
        Me.fraChasTemp_1.Controls.Add(Me.lblChassTemp_18)
        Me.fraChasTemp_1.Controls.Add(Me.lblChassTemp_17)
        Me.fraChasTemp_1.Controls.Add(Me.lblChassTemp_16)
        Me.fraChasTemp_1.Controls.Add(Me.lblChassTemp_15)
        Me.fraChasTemp_1.Controls.Add(Me.linRise_1)
        Me.fraChasTemp_1.Controls.Add(Me.linRise_2)
        Me.fraChasTemp_1.Controls.Add(Me.linRise_3)
        Me.fraChasTemp_1.Controls.Add(Me.linRise_10)
        Me.fraChasTemp_1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraChasTemp_1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraChasTemp_1.Location = New System.Drawing.Point(8, 73)
        Me.fraChasTemp_1.Name = "fraChasTemp_1"
        Me.fraChasTemp_1.Size = New System.Drawing.Size(562, 220)
        Me.fraChasTemp_1.TabIndex = 8
        Me.fraChasTemp_1.TabStop = False
        Me.fraChasTemp_1.Text = "Temperature Rise"
        '
        'cwsSlotRise_12
        '
        Me.cwsSlotRise_12.FillBackColor = System.Drawing.Color.LightGray
        Me.cwsSlotRise_12.FillMode = NationalInstruments.UI.NumericFillMode.None
        Me.cwsSlotRise_12.Location = New System.Drawing.Point(517, 38)
        Me.cwsSlotRise_12.Name = "cwsSlotRise_12"
        Me.cwsSlotRise_12.OutOfRangeMode = NationalInstruments.UI.NumericOutOfRangeMode.CoerceToRange
        Me.cwsSlotRise_12.PointerColor = System.Drawing.Color.Black
        Me.cwsSlotRise_12.Range = New NationalInstruments.UI.Range(0.0R, 30.0R)
        Me.cwsSlotRise_12.ScalePosition = NationalInstruments.UI.NumericScalePosition.Right
        Me.cwsSlotRise_12.ScaleVisible = False
        Me.cwsSlotRise_12.Size = New System.Drawing.Size(25, 183)
        Me.cwsSlotRise_12.SlideStyle = NationalInstruments.UI.SlideStyle.SunkenWithGrip
        Me.cwsSlotRise_12.TabIndex = 344
        Me.cwsSlotRise_12.Tag = "12"
        Me.cwsSlotRise_12.Value = 15.0R
        '
        'dummySlide_1
        '
        Me.dummySlide_1.Location = New System.Drawing.Point(524, 29)
        Me.dummySlide_1.Name = "dummySlide_1"
        Me.dummySlide_1.Range = New NationalInstruments.UI.Range(0.0R, 30.0R)
        Me.dummySlide_1.ScalePosition = NationalInstruments.UI.NumericScalePosition.Right
        Me.dummySlide_1.Size = New System.Drawing.Size(40, 198)
        Me.dummySlide_1.TabIndex = 371
        '
        'cwsSlotRiseActual_0
        '
        Me.cwsSlotRiseActual_0.FillBackColor = System.Drawing.Color.LightGray
        Me.cwsSlotRiseActual_0.FillColor = System.Drawing.Color.Green
        Me.cwsSlotRiseActual_0.Location = New System.Drawing.Point(17, 38)
        Me.cwsSlotRiseActual_0.Name = "cwsSlotRiseActual_0"
        Me.cwsSlotRiseActual_0.Range = New NationalInstruments.UI.Range(0.0R, 30.0R)
        Me.cwsSlotRiseActual_0.ScaleVisible = False
        Me.cwsSlotRiseActual_0.Size = New System.Drawing.Size(15, 183)
        Me.cwsSlotRiseActual_0.TabIndex = 370
        Me.cwsSlotRiseActual_0.Tag = ""
        Me.cwsSlotRiseActual_0.TankStyle = NationalInstruments.UI.TankStyle.Flat
        '
        'dummySlide_0
        '
        Me.dummySlide_0.Location = New System.Drawing.Point(-5, 28)
        Me.dummySlide_0.Name = "dummySlide_0"
        Me.dummySlide_0.Range = New NationalInstruments.UI.Range(0.0R, 30.0R)
        Me.dummySlide_0.Size = New System.Drawing.Size(40, 200)
        Me.dummySlide_0.TabIndex = 358
        '
        'cwsSlotRiseActual_12
        '
        Me.cwsSlotRiseActual_12.FillBackColor = System.Drawing.Color.LightGray
        Me.cwsSlotRiseActual_12.FillColor = System.Drawing.Color.Green
        Me.cwsSlotRiseActual_12.Location = New System.Drawing.Point(502, 38)
        Me.cwsSlotRiseActual_12.Name = "cwsSlotRiseActual_12"
        Me.cwsSlotRiseActual_12.Range = New NationalInstruments.UI.Range(0.0R, 30.0R)
        Me.cwsSlotRiseActual_12.ScaleVisible = False
        Me.cwsSlotRiseActual_12.Size = New System.Drawing.Size(15, 183)
        Me.cwsSlotRiseActual_12.TabIndex = 369
        Me.cwsSlotRiseActual_12.TankStyle = NationalInstruments.UI.TankStyle.Flat
        '
        'cwsSlotRiseActual_11
        '
        Me.cwsSlotRiseActual_11.FillBackColor = System.Drawing.Color.LightGray
        Me.cwsSlotRiseActual_11.FillColor = System.Drawing.Color.Green
        Me.cwsSlotRiseActual_11.Location = New System.Drawing.Point(462, 38)
        Me.cwsSlotRiseActual_11.Name = "cwsSlotRiseActual_11"
        Me.cwsSlotRiseActual_11.Range = New NationalInstruments.UI.Range(0.0R, 30.0R)
        Me.cwsSlotRiseActual_11.ScaleVisible = False
        Me.cwsSlotRiseActual_11.Size = New System.Drawing.Size(15, 183)
        Me.cwsSlotRiseActual_11.TabIndex = 368
        Me.cwsSlotRiseActual_11.TankStyle = NationalInstruments.UI.TankStyle.Flat
        '
        'cwsSlotRiseActual_10
        '
        Me.cwsSlotRiseActual_10.FillBackColor = System.Drawing.Color.LightGray
        Me.cwsSlotRiseActual_10.FillColor = System.Drawing.Color.Green
        Me.cwsSlotRiseActual_10.Location = New System.Drawing.Point(421, 38)
        Me.cwsSlotRiseActual_10.Name = "cwsSlotRiseActual_10"
        Me.cwsSlotRiseActual_10.Range = New NationalInstruments.UI.Range(0.0R, 30.0R)
        Me.cwsSlotRiseActual_10.ScaleVisible = False
        Me.cwsSlotRiseActual_10.Size = New System.Drawing.Size(15, 183)
        Me.cwsSlotRiseActual_10.TabIndex = 367
        Me.cwsSlotRiseActual_10.TankStyle = NationalInstruments.UI.TankStyle.Flat
        '
        'cwsSlotRiseActual_9
        '
        Me.cwsSlotRiseActual_9.FillBackColor = System.Drawing.Color.LightGray
        Me.cwsSlotRiseActual_9.FillColor = System.Drawing.Color.Green
        Me.cwsSlotRiseActual_9.Location = New System.Drawing.Point(380, 38)
        Me.cwsSlotRiseActual_9.Name = "cwsSlotRiseActual_9"
        Me.cwsSlotRiseActual_9.Range = New NationalInstruments.UI.Range(0.0R, 30.0R)
        Me.cwsSlotRiseActual_9.ScaleVisible = False
        Me.cwsSlotRiseActual_9.Size = New System.Drawing.Size(15, 183)
        Me.cwsSlotRiseActual_9.TabIndex = 366
        Me.cwsSlotRiseActual_9.TankStyle = NationalInstruments.UI.TankStyle.Flat
        '
        'cwsSlotRiseActual_8
        '
        Me.cwsSlotRiseActual_8.FillBackColor = System.Drawing.Color.LightGray
        Me.cwsSlotRiseActual_8.FillColor = System.Drawing.Color.Green
        Me.cwsSlotRiseActual_8.Location = New System.Drawing.Point(340, 38)
        Me.cwsSlotRiseActual_8.Name = "cwsSlotRiseActual_8"
        Me.cwsSlotRiseActual_8.Range = New NationalInstruments.UI.Range(0.0R, 30.0R)
        Me.cwsSlotRiseActual_8.ScaleVisible = False
        Me.cwsSlotRiseActual_8.Size = New System.Drawing.Size(15, 183)
        Me.cwsSlotRiseActual_8.TabIndex = 365
        Me.cwsSlotRiseActual_8.TankStyle = NationalInstruments.UI.TankStyle.Flat
        '
        'cwsSlotRiseActual_7
        '
        Me.cwsSlotRiseActual_7.FillBackColor = System.Drawing.Color.LightGray
        Me.cwsSlotRiseActual_7.FillColor = System.Drawing.Color.Green
        Me.cwsSlotRiseActual_7.Location = New System.Drawing.Point(299, 38)
        Me.cwsSlotRiseActual_7.Name = "cwsSlotRiseActual_7"
        Me.cwsSlotRiseActual_7.Range = New NationalInstruments.UI.Range(0.0R, 30.0R)
        Me.cwsSlotRiseActual_7.ScaleVisible = False
        Me.cwsSlotRiseActual_7.Size = New System.Drawing.Size(15, 183)
        Me.cwsSlotRiseActual_7.TabIndex = 364
        Me.cwsSlotRiseActual_7.TankStyle = NationalInstruments.UI.TankStyle.Flat
        '
        'cwsSlotRiseActual_6
        '
        Me.cwsSlotRiseActual_6.FillBackColor = System.Drawing.Color.LightGray
        Me.cwsSlotRiseActual_6.FillColor = System.Drawing.Color.Green
        Me.cwsSlotRiseActual_6.Location = New System.Drawing.Point(259, 38)
        Me.cwsSlotRiseActual_6.Name = "cwsSlotRiseActual_6"
        Me.cwsSlotRiseActual_6.Range = New NationalInstruments.UI.Range(0.0R, 30.0R)
        Me.cwsSlotRiseActual_6.ScaleVisible = False
        Me.cwsSlotRiseActual_6.Size = New System.Drawing.Size(15, 183)
        Me.cwsSlotRiseActual_6.TabIndex = 363
        Me.cwsSlotRiseActual_6.TankStyle = NationalInstruments.UI.TankStyle.Flat
        '
        'cwsSlotRiseActual_5
        '
        Me.cwsSlotRiseActual_5.FillBackColor = System.Drawing.Color.LightGray
        Me.cwsSlotRiseActual_5.FillColor = System.Drawing.Color.Green
        Me.cwsSlotRiseActual_5.Location = New System.Drawing.Point(218, 38)
        Me.cwsSlotRiseActual_5.Name = "cwsSlotRiseActual_5"
        Me.cwsSlotRiseActual_5.Range = New NationalInstruments.UI.Range(0.0R, 30.0R)
        Me.cwsSlotRiseActual_5.ScaleVisible = False
        Me.cwsSlotRiseActual_5.Size = New System.Drawing.Size(15, 183)
        Me.cwsSlotRiseActual_5.TabIndex = 362
        Me.cwsSlotRiseActual_5.TankStyle = NationalInstruments.UI.TankStyle.Flat
        '
        'cwsSlotRiseActual_4
        '
        Me.cwsSlotRiseActual_4.FillBackColor = System.Drawing.Color.LightGray
        Me.cwsSlotRiseActual_4.FillColor = System.Drawing.Color.Green
        Me.cwsSlotRiseActual_4.Location = New System.Drawing.Point(178, 38)
        Me.cwsSlotRiseActual_4.Name = "cwsSlotRiseActual_4"
        Me.cwsSlotRiseActual_4.Range = New NationalInstruments.UI.Range(0.0R, 30.0R)
        Me.cwsSlotRiseActual_4.ScaleVisible = False
        Me.cwsSlotRiseActual_4.Size = New System.Drawing.Size(15, 183)
        Me.cwsSlotRiseActual_4.TabIndex = 361
        Me.cwsSlotRiseActual_4.TankStyle = NationalInstruments.UI.TankStyle.Flat
        '
        'cwsSlotRiseActual_3
        '
        Me.cwsSlotRiseActual_3.FillBackColor = System.Drawing.Color.LightGray
        Me.cwsSlotRiseActual_3.FillColor = System.Drawing.Color.Green
        Me.cwsSlotRiseActual_3.Location = New System.Drawing.Point(138, 38)
        Me.cwsSlotRiseActual_3.Name = "cwsSlotRiseActual_3"
        Me.cwsSlotRiseActual_3.Range = New NationalInstruments.UI.Range(0.0R, 30.0R)
        Me.cwsSlotRiseActual_3.ScaleVisible = False
        Me.cwsSlotRiseActual_3.Size = New System.Drawing.Size(15, 183)
        Me.cwsSlotRiseActual_3.TabIndex = 360
        Me.cwsSlotRiseActual_3.TankStyle = NationalInstruments.UI.TankStyle.Flat
        '
        'cwsSlotRiseActual_2
        '
        Me.cwsSlotRiseActual_2.FillBackColor = System.Drawing.Color.LightGray
        Me.cwsSlotRiseActual_2.FillColor = System.Drawing.Color.Green
        Me.cwsSlotRiseActual_2.Location = New System.Drawing.Point(97, 38)
        Me.cwsSlotRiseActual_2.Name = "cwsSlotRiseActual_2"
        Me.cwsSlotRiseActual_2.Range = New NationalInstruments.UI.Range(0.0R, 30.0R)
        Me.cwsSlotRiseActual_2.ScaleVisible = False
        Me.cwsSlotRiseActual_2.Size = New System.Drawing.Size(15, 183)
        Me.cwsSlotRiseActual_2.TabIndex = 359
        Me.cwsSlotRiseActual_2.TankStyle = NationalInstruments.UI.TankStyle.Flat
        '
        'cwsSlotRise_1
        '
        Me.cwsSlotRise_1.FillBackColor = System.Drawing.Color.LightGray
        Me.cwsSlotRise_1.FillMode = NationalInstruments.UI.NumericFillMode.None
        Me.cwsSlotRise_1.Location = New System.Drawing.Point(71, 38)
        Me.cwsSlotRise_1.Name = "cwsSlotRise_1"
        Me.cwsSlotRise_1.OutOfRangeMode = NationalInstruments.UI.NumericOutOfRangeMode.CoerceToRange
        Me.cwsSlotRise_1.PointerColor = System.Drawing.Color.Black
        Me.cwsSlotRise_1.Range = New NationalInstruments.UI.Range(0.0R, 30.0R)
        Me.cwsSlotRise_1.ScaleVisible = False
        Me.cwsSlotRise_1.Size = New System.Drawing.Size(25, 183)
        Me.cwsSlotRise_1.SlideStyle = NationalInstruments.UI.SlideStyle.SunkenWithGrip
        Me.cwsSlotRise_1.TabIndex = 333
        Me.cwsSlotRise_1.Tag = "1"
        Me.cwsSlotRise_1.Value = 15.0R
        '
        'cwsSlotRiseActual_1
        '
        Me.cwsSlotRiseActual_1.FillBackColor = System.Drawing.Color.LightGray
        Me.cwsSlotRiseActual_1.FillColor = System.Drawing.Color.Green
        Me.cwsSlotRiseActual_1.Location = New System.Drawing.Point(59, 38)
        Me.cwsSlotRiseActual_1.Name = "cwsSlotRiseActual_1"
        Me.cwsSlotRiseActual_1.Range = New NationalInstruments.UI.Range(0.0R, 30.0R)
        Me.cwsSlotRiseActual_1.ScaleVisible = False
        Me.cwsSlotRiseActual_1.Size = New System.Drawing.Size(15, 183)
        Me.cwsSlotRiseActual_1.TabIndex = 358
        Me.cwsSlotRiseActual_1.TankStyle = NationalInstruments.UI.TankStyle.Flat
        '
        'cwsSlotRise_11
        '
        Me.cwsSlotRise_11.FillBackColor = System.Drawing.Color.LightGray
        Me.cwsSlotRise_11.FillMode = NationalInstruments.UI.NumericFillMode.None
        Me.cwsSlotRise_11.Location = New System.Drawing.Point(476, 38)
        Me.cwsSlotRise_11.Name = "cwsSlotRise_11"
        Me.cwsSlotRise_11.OutOfRangeMode = NationalInstruments.UI.NumericOutOfRangeMode.CoerceToRange
        Me.cwsSlotRise_11.PointerColor = System.Drawing.Color.Black
        Me.cwsSlotRise_11.Range = New NationalInstruments.UI.Range(0.0R, 30.0R)
        Me.cwsSlotRise_11.ScaleVisible = False
        Me.cwsSlotRise_11.Size = New System.Drawing.Size(25, 183)
        Me.cwsSlotRise_11.SlideStyle = NationalInstruments.UI.SlideStyle.SunkenWithGrip
        Me.cwsSlotRise_11.TabIndex = 343
        Me.cwsSlotRise_11.Tag = "11"
        Me.cwsSlotRise_11.Value = 15.0R
        '
        'cwsSlotRise_10
        '
        Me.cwsSlotRise_10.FillBackColor = System.Drawing.Color.LightGray
        Me.cwsSlotRise_10.FillMode = NationalInstruments.UI.NumericFillMode.None
        Me.cwsSlotRise_10.Location = New System.Drawing.Point(436, 38)
        Me.cwsSlotRise_10.Name = "cwsSlotRise_10"
        Me.cwsSlotRise_10.OutOfRangeMode = NationalInstruments.UI.NumericOutOfRangeMode.CoerceToRange
        Me.cwsSlotRise_10.PointerColor = System.Drawing.Color.Black
        Me.cwsSlotRise_10.Range = New NationalInstruments.UI.Range(0.0R, 30.0R)
        Me.cwsSlotRise_10.ScaleVisible = False
        Me.cwsSlotRise_10.Size = New System.Drawing.Size(25, 183)
        Me.cwsSlotRise_10.SlideStyle = NationalInstruments.UI.SlideStyle.SunkenWithGrip
        Me.cwsSlotRise_10.TabIndex = 342
        Me.cwsSlotRise_10.Tag = "10"
        Me.cwsSlotRise_10.Value = 15.0R
        '
        'cwsSlotRise_9
        '
        Me.cwsSlotRise_9.FillBackColor = System.Drawing.Color.LightGray
        Me.cwsSlotRise_9.FillMode = NationalInstruments.UI.NumericFillMode.None
        Me.cwsSlotRise_9.Location = New System.Drawing.Point(395, 38)
        Me.cwsSlotRise_9.Name = "cwsSlotRise_9"
        Me.cwsSlotRise_9.OutOfRangeMode = NationalInstruments.UI.NumericOutOfRangeMode.CoerceToRange
        Me.cwsSlotRise_9.PointerColor = System.Drawing.Color.Black
        Me.cwsSlotRise_9.Range = New NationalInstruments.UI.Range(0.0R, 30.0R)
        Me.cwsSlotRise_9.ScaleVisible = False
        Me.cwsSlotRise_9.Size = New System.Drawing.Size(25, 183)
        Me.cwsSlotRise_9.SlideStyle = NationalInstruments.UI.SlideStyle.SunkenWithGrip
        Me.cwsSlotRise_9.TabIndex = 341
        Me.cwsSlotRise_9.Tag = "9"
        Me.cwsSlotRise_9.Value = 15.0R
        '
        'cwsSlotRise_8
        '
        Me.cwsSlotRise_8.FillBackColor = System.Drawing.Color.LightGray
        Me.cwsSlotRise_8.FillMode = NationalInstruments.UI.NumericFillMode.None
        Me.cwsSlotRise_8.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.cwsSlotRise_8.Location = New System.Drawing.Point(354, 38)
        Me.cwsSlotRise_8.Name = "cwsSlotRise_8"
        Me.cwsSlotRise_8.OutOfRangeMode = NationalInstruments.UI.NumericOutOfRangeMode.CoerceToRange
        Me.cwsSlotRise_8.PointerColor = System.Drawing.Color.Black
        Me.cwsSlotRise_8.Range = New NationalInstruments.UI.Range(0.0R, 30.0R)
        Me.cwsSlotRise_8.ScaleVisible = False
        Me.cwsSlotRise_8.Size = New System.Drawing.Size(25, 183)
        Me.cwsSlotRise_8.SlideStyle = NationalInstruments.UI.SlideStyle.SunkenWithGrip
        Me.cwsSlotRise_8.TabIndex = 340
        Me.cwsSlotRise_8.Tag = "8"
        Me.cwsSlotRise_8.Value = 15.0R
        '
        'cwsSlotRise_7
        '
        Me.cwsSlotRise_7.FillBackColor = System.Drawing.Color.LightGray
        Me.cwsSlotRise_7.FillMode = NationalInstruments.UI.NumericFillMode.None
        Me.cwsSlotRise_7.Location = New System.Drawing.Point(314, 38)
        Me.cwsSlotRise_7.Name = "cwsSlotRise_7"
        Me.cwsSlotRise_7.OutOfRangeMode = NationalInstruments.UI.NumericOutOfRangeMode.CoerceToRange
        Me.cwsSlotRise_7.PointerColor = System.Drawing.Color.Black
        Me.cwsSlotRise_7.Range = New NationalInstruments.UI.Range(0.0R, 30.0R)
        Me.cwsSlotRise_7.ScaleVisible = False
        Me.cwsSlotRise_7.Size = New System.Drawing.Size(25, 183)
        Me.cwsSlotRise_7.SlideStyle = NationalInstruments.UI.SlideStyle.SunkenWithGrip
        Me.cwsSlotRise_7.TabIndex = 339
        Me.cwsSlotRise_7.Tag = "7"
        Me.cwsSlotRise_7.Value = 15.0R
        '
        'cwsSlotRise_6
        '
        Me.cwsSlotRise_6.FillBackColor = System.Drawing.Color.LightGray
        Me.cwsSlotRise_6.FillMode = NationalInstruments.UI.NumericFillMode.None
        Me.cwsSlotRise_6.Location = New System.Drawing.Point(273, 38)
        Me.cwsSlotRise_6.Name = "cwsSlotRise_6"
        Me.cwsSlotRise_6.OutOfRangeMode = NationalInstruments.UI.NumericOutOfRangeMode.CoerceToRange
        Me.cwsSlotRise_6.PointerColor = System.Drawing.Color.Black
        Me.cwsSlotRise_6.Range = New NationalInstruments.UI.Range(0.0R, 30.0R)
        Me.cwsSlotRise_6.ScaleVisible = False
        Me.cwsSlotRise_6.Size = New System.Drawing.Size(25, 183)
        Me.cwsSlotRise_6.SlideStyle = NationalInstruments.UI.SlideStyle.SunkenWithGrip
        Me.cwsSlotRise_6.TabIndex = 338
        Me.cwsSlotRise_6.Tag = "6"
        Me.cwsSlotRise_6.Value = 15.0R
        '
        'cwsSlotRise_5
        '
        Me.cwsSlotRise_5.FillBackColor = System.Drawing.Color.LightGray
        Me.cwsSlotRise_5.FillMode = NationalInstruments.UI.NumericFillMode.None
        Me.cwsSlotRise_5.Location = New System.Drawing.Point(233, 38)
        Me.cwsSlotRise_5.Name = "cwsSlotRise_5"
        Me.cwsSlotRise_5.OutOfRangeMode = NationalInstruments.UI.NumericOutOfRangeMode.CoerceToRange
        Me.cwsSlotRise_5.PointerColor = System.Drawing.Color.Black
        Me.cwsSlotRise_5.Range = New NationalInstruments.UI.Range(0.0R, 30.0R)
        Me.cwsSlotRise_5.ScaleVisible = False
        Me.cwsSlotRise_5.Size = New System.Drawing.Size(25, 183)
        Me.cwsSlotRise_5.SlideStyle = NationalInstruments.UI.SlideStyle.SunkenWithGrip
        Me.cwsSlotRise_5.TabIndex = 337
        Me.cwsSlotRise_5.Tag = "5"
        Me.cwsSlotRise_5.Value = 15.0R
        '
        'cwsSlotRise_4
        '
        Me.cwsSlotRise_4.FillBackColor = System.Drawing.Color.LightGray
        Me.cwsSlotRise_4.FillMode = NationalInstruments.UI.NumericFillMode.None
        Me.cwsSlotRise_4.Location = New System.Drawing.Point(192, 38)
        Me.cwsSlotRise_4.Name = "cwsSlotRise_4"
        Me.cwsSlotRise_4.OutOfRangeMode = NationalInstruments.UI.NumericOutOfRangeMode.CoerceToRange
        Me.cwsSlotRise_4.PointerColor = System.Drawing.Color.Black
        Me.cwsSlotRise_4.Range = New NationalInstruments.UI.Range(0.0R, 30.0R)
        Me.cwsSlotRise_4.ScaleVisible = False
        Me.cwsSlotRise_4.Size = New System.Drawing.Size(25, 183)
        Me.cwsSlotRise_4.SlideStyle = NationalInstruments.UI.SlideStyle.SunkenWithGrip
        Me.cwsSlotRise_4.TabIndex = 336
        Me.cwsSlotRise_4.Tag = "4"
        Me.cwsSlotRise_4.Value = 15.0R
        '
        'cwsSlotRise_3
        '
        Me.cwsSlotRise_3.FillBackColor = System.Drawing.Color.LightGray
        Me.cwsSlotRise_3.FillMode = NationalInstruments.UI.NumericFillMode.None
        Me.cwsSlotRise_3.Location = New System.Drawing.Point(152, 38)
        Me.cwsSlotRise_3.Name = "cwsSlotRise_3"
        Me.cwsSlotRise_3.OutOfRangeMode = NationalInstruments.UI.NumericOutOfRangeMode.CoerceToRange
        Me.cwsSlotRise_3.PointerColor = System.Drawing.Color.Black
        Me.cwsSlotRise_3.Range = New NationalInstruments.UI.Range(0.0R, 30.0R)
        Me.cwsSlotRise_3.ScaleVisible = False
        Me.cwsSlotRise_3.Size = New System.Drawing.Size(25, 183)
        Me.cwsSlotRise_3.SlideStyle = NationalInstruments.UI.SlideStyle.SunkenWithGrip
        Me.cwsSlotRise_3.TabIndex = 335
        Me.cwsSlotRise_3.Tag = "3"
        Me.cwsSlotRise_3.Value = 15.0R
        '
        'cwsSlotRise_2
        '
        Me.cwsSlotRise_2.FillBackColor = System.Drawing.Color.LightGray
        Me.cwsSlotRise_2.FillMode = NationalInstruments.UI.NumericFillMode.None
        Me.cwsSlotRise_2.Location = New System.Drawing.Point(112, 38)
        Me.cwsSlotRise_2.Name = "cwsSlotRise_2"
        Me.cwsSlotRise_2.OutOfRangeMode = NationalInstruments.UI.NumericOutOfRangeMode.CoerceToRange
        Me.cwsSlotRise_2.PointerColor = System.Drawing.Color.Black
        Me.cwsSlotRise_2.Range = New NationalInstruments.UI.Range(0.0R, 30.0R)
        Me.cwsSlotRise_2.ScaleVisible = False
        Me.cwsSlotRise_2.Size = New System.Drawing.Size(25, 183)
        Me.cwsSlotRise_2.SlideStyle = NationalInstruments.UI.SlideStyle.SunkenWithGrip
        Me.cwsSlotRise_2.TabIndex = 334
        Me.cwsSlotRise_2.Tag = "2"
        Me.cwsSlotRise_2.Value = 15.0R
        '
        'cwsSlotRise_0
        '
        Me.cwsSlotRise_0.FillBackColor = System.Drawing.Color.LightGray
        Me.cwsSlotRise_0.FillMode = NationalInstruments.UI.NumericFillMode.None
        Me.cwsSlotRise_0.Location = New System.Drawing.Point(33, 38)
        Me.cwsSlotRise_0.Name = "cwsSlotRise_0"
        Me.cwsSlotRise_0.OutOfRangeMode = NationalInstruments.UI.NumericOutOfRangeMode.CoerceToRange
        Me.cwsSlotRise_0.PointerColor = System.Drawing.Color.Black
        Me.cwsSlotRise_0.Range = New NationalInstruments.UI.Range(0.0R, 30.0R)
        Me.cwsSlotRise_0.ScaleVisible = False
        Me.cwsSlotRise_0.Size = New System.Drawing.Size(25, 183)
        Me.cwsSlotRise_0.SlideStyle = NationalInstruments.UI.SlideStyle.SunkenWithGrip
        Me.cwsSlotRise_0.TabIndex = 332
        Me.cwsSlotRise_0.Tag = "0"
        Me.cwsSlotRise_0.Value = 15.0R
        '
        'fraChasTemp_0
        '
        Me.fraChasTemp_0.Controls.Add(Me.lblExhaustValue_12)
        Me.fraChasTemp_0.Controls.Add(Me.lblExhaustValue_11)
        Me.fraChasTemp_0.Controls.Add(Me.lblExhaustValue_10)
        Me.fraChasTemp_0.Controls.Add(Me.lblExhaustValue_9)
        Me.fraChasTemp_0.Controls.Add(Me.lblExhaustValue_8)
        Me.fraChasTemp_0.Controls.Add(Me.lblExhaustValue_7)
        Me.fraChasTemp_0.Controls.Add(Me.lblExhaustValue_6)
        Me.fraChasTemp_0.Controls.Add(Me.lblExhaustValue_5)
        Me.fraChasTemp_0.Controls.Add(Me.lblExhaustValue_4)
        Me.fraChasTemp_0.Controls.Add(Me.lblExhaustValue_3)
        Me.fraChasTemp_0.Controls.Add(Me.lblExhaustValue_2)
        Me.fraChasTemp_0.Controls.Add(Me.lblExhaustValue_1)
        Me.fraChasTemp_0.Controls.Add(Me.lblExhaustValue_0)
        Me.fraChasTemp_0.Controls.Add(Me.lblChassTemp_0)
        Me.fraChasTemp_0.Controls.Add(Me.lblChassTemp_1)
        Me.fraChasTemp_0.Controls.Add(Me.lblChassTemp_2)
        Me.fraChasTemp_0.Controls.Add(Me.lblChassTemp_3)
        Me.fraChasTemp_0.Controls.Add(Me.lblChassTemp_4)
        Me.fraChasTemp_0.Controls.Add(Me.lblChassTemp_5)
        Me.fraChasTemp_0.Controls.Add(Me.lblChassTemp_6)
        Me.fraChasTemp_0.Controls.Add(Me.lblChassTemp_7)
        Me.fraChasTemp_0.Controls.Add(Me.lblChassTemp_8)
        Me.fraChasTemp_0.Controls.Add(Me.lblChassTemp_9)
        Me.fraChasTemp_0.Controls.Add(Me.lblChassTemp_10)
        Me.fraChasTemp_0.Controls.Add(Me.lblChassTemp_11)
        Me.fraChasTemp_0.Controls.Add(Me.lblChassTemp_12)
        Me.fraChasTemp_0.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraChasTemp_0.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraChasTemp_0.Location = New System.Drawing.Point(8, 4)
        Me.fraChasTemp_0.Name = "fraChasTemp_0"
        Me.fraChasTemp_0.Size = New System.Drawing.Size(551, 70)
        Me.fraChasTemp_0.TabIndex = 4
        Me.fraChasTemp_0.TabStop = False
        Me.fraChasTemp_0.Text = "VXI Slot Exhaust (Celsius)"
        '
        'tabSysmon_Page2
        '
        Me.tabSysmon_Page2.Controls.Add(Me.fraChasTemp_4)
        Me.tabSysmon_Page2.Controls.Add(Me.fraChasTemp_5)
        Me.tabSysmon_Page2.Controls.Add(Me.fraChasTemp_3)
        Me.tabSysmon_Page2.Controls.Add(Me.cmdResetThresholds_2)
        Me.tabSysmon_Page2.Controls.Add(Me.cmdSaveThresholds_2)
        Me.tabSysmon_Page2.Location = New System.Drawing.Point(4, 22)
        Me.tabSysmon_Page2.Name = "tabSysmon_Page2"
        Me.tabSysmon_Page2.Size = New System.Drawing.Size(578, 392)
        Me.tabSysmon_Page2.TabIndex = 1
        Me.tabSysmon_Page2.Text = "Sec. Chassis Temp"
        '
        'fraChasTemp_4
        '
        Me.fraChasTemp_4.Controls.Add(Me.cwsSlotRiseActual_25)
        Me.fraChasTemp_4.Controls.Add(Me.cwsSlotRiseActual_24)
        Me.fraChasTemp_4.Controls.Add(Me.cwsSlotRiseActual_23)
        Me.fraChasTemp_4.Controls.Add(Me.cwsSlotRiseActual_22)
        Me.fraChasTemp_4.Controls.Add(Me.cwsSlotRiseActual_21)
        Me.fraChasTemp_4.Controls.Add(Me.cwsSlotRiseActual_20)
        Me.fraChasTemp_4.Controls.Add(Me.cwsSlotRiseActual_19)
        Me.fraChasTemp_4.Controls.Add(Me.cwsSlotRiseActual_18)
        Me.fraChasTemp_4.Controls.Add(Me.cwsSlotRiseActual_17)
        Me.fraChasTemp_4.Controls.Add(Me.cwsSlotRiseActual_16)
        Me.fraChasTemp_4.Controls.Add(Me.cwsSlotRiseActual_15)
        Me.fraChasTemp_4.Controls.Add(Me.cwsSlotRiseActual_14)
        Me.fraChasTemp_4.Controls.Add(Me.lblChassTemp_39)
        Me.fraChasTemp_4.Controls.Add(Me.cwsSlotRise_25)
        Me.fraChasTemp_4.Controls.Add(Me.Slide2)
        Me.fraChasTemp_4.Controls.Add(Me.lblChassTemp_51)
        Me.fraChasTemp_4.Controls.Add(Me.cwsSlotRiseActual_13)
        Me.fraChasTemp_4.Controls.Add(Me.Slide1)
        Me.fraChasTemp_4.Controls.Add(Me.cwsSlotRise_24)
        Me.fraChasTemp_4.Controls.Add(Me.cwsSlotRise_23)
        Me.fraChasTemp_4.Controls.Add(Me.cwsSlotRise_22)
        Me.fraChasTemp_4.Controls.Add(Me.cwsSlotRise_21)
        Me.fraChasTemp_4.Controls.Add(Me.cwsSlotRise_20)
        Me.fraChasTemp_4.Controls.Add(Me.cwsSlotRise_19)
        Me.fraChasTemp_4.Controls.Add(Me.cwsSlotRise_18)
        Me.fraChasTemp_4.Controls.Add(Me.cwsSlotRise_17)
        Me.fraChasTemp_4.Controls.Add(Me.cwsSlotRise_16)
        Me.fraChasTemp_4.Controls.Add(Me.cwsSlotRise_15)
        Me.fraChasTemp_4.Controls.Add(Me.cwsSlotRise_14)
        Me.fraChasTemp_4.Controls.Add(Me.cwsSlotRise_13)
        Me.fraChasTemp_4.Controls.Add(Me.linRise_9)
        Me.fraChasTemp_4.Controls.Add(Me.linRise_8)
        Me.fraChasTemp_4.Controls.Add(Me.linRise_7)
        Me.fraChasTemp_4.Controls.Add(Me.linRise_6)
        Me.fraChasTemp_4.Controls.Add(Me.linRise_5)
        Me.fraChasTemp_4.Controls.Add(Me.lblChassTemp_50)
        Me.fraChasTemp_4.Controls.Add(Me.lblChassTemp_49)
        Me.fraChasTemp_4.Controls.Add(Me.lblChassTemp_48)
        Me.fraChasTemp_4.Controls.Add(Me.lblChassTemp_47)
        Me.fraChasTemp_4.Controls.Add(Me.lblChassTemp_46)
        Me.fraChasTemp_4.Controls.Add(Me.lblChassTemp_45)
        Me.fraChasTemp_4.Controls.Add(Me.lblChassTemp_44)
        Me.fraChasTemp_4.Controls.Add(Me.lblChassTemp_43)
        Me.fraChasTemp_4.Controls.Add(Me.lblChassTemp_42)
        Me.fraChasTemp_4.Controls.Add(Me.lblChassTemp_41)
        Me.fraChasTemp_4.Controls.Add(Me.lblChassTemp_40)
        Me.fraChasTemp_4.Controls.Add(Me.linRise_12)
        Me.fraChasTemp_4.Controls.Add(Me.linRise_13)
        Me.fraChasTemp_4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraChasTemp_4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraChasTemp_4.Location = New System.Drawing.Point(8, 73)
        Me.fraChasTemp_4.Name = "fraChasTemp_4"
        Me.fraChasTemp_4.Size = New System.Drawing.Size(562, 223)
        Me.fraChasTemp_4.TabIndex = 325
        Me.fraChasTemp_4.TabStop = False
        Me.fraChasTemp_4.Text = "Temperature Rise"
        '
        'cwsSlotRiseActual_25
        '
        Me.cwsSlotRiseActual_25.FillBackColor = System.Drawing.Color.LightGray
        Me.cwsSlotRiseActual_25.FillColor = System.Drawing.Color.Green
        Me.cwsSlotRiseActual_25.Location = New System.Drawing.Point(502, 38)
        Me.cwsSlotRiseActual_25.Name = "cwsSlotRiseActual_25"
        Me.cwsSlotRiseActual_25.Range = New NationalInstruments.UI.Range(0.0R, 30.0R)
        Me.cwsSlotRiseActual_25.ScaleVisible = False
        Me.cwsSlotRiseActual_25.Size = New System.Drawing.Size(15, 183)
        Me.cwsSlotRiseActual_25.TabIndex = 384
        Me.cwsSlotRiseActual_25.Tag = ""
        Me.cwsSlotRiseActual_25.TankStyle = NationalInstruments.UI.TankStyle.Flat
        '
        'cwsSlotRiseActual_24
        '
        Me.cwsSlotRiseActual_24.FillBackColor = System.Drawing.Color.LightGray
        Me.cwsSlotRiseActual_24.FillColor = System.Drawing.Color.Green
        Me.cwsSlotRiseActual_24.Location = New System.Drawing.Point(462, 38)
        Me.cwsSlotRiseActual_24.Name = "cwsSlotRiseActual_24"
        Me.cwsSlotRiseActual_24.Range = New NationalInstruments.UI.Range(0.0R, 30.0R)
        Me.cwsSlotRiseActual_24.ScaleVisible = False
        Me.cwsSlotRiseActual_24.Size = New System.Drawing.Size(15, 183)
        Me.cwsSlotRiseActual_24.TabIndex = 383
        Me.cwsSlotRiseActual_24.Tag = ""
        Me.cwsSlotRiseActual_24.TankStyle = NationalInstruments.UI.TankStyle.Flat
        '
        'cwsSlotRiseActual_23
        '
        Me.cwsSlotRiseActual_23.FillBackColor = System.Drawing.Color.LightGray
        Me.cwsSlotRiseActual_23.FillColor = System.Drawing.Color.Green
        Me.cwsSlotRiseActual_23.Location = New System.Drawing.Point(421, 38)
        Me.cwsSlotRiseActual_23.Name = "cwsSlotRiseActual_23"
        Me.cwsSlotRiseActual_23.Range = New NationalInstruments.UI.Range(0.0R, 30.0R)
        Me.cwsSlotRiseActual_23.ScaleVisible = False
        Me.cwsSlotRiseActual_23.Size = New System.Drawing.Size(15, 183)
        Me.cwsSlotRiseActual_23.TabIndex = 382
        Me.cwsSlotRiseActual_23.Tag = ""
        Me.cwsSlotRiseActual_23.TankStyle = NationalInstruments.UI.TankStyle.Flat
        '
        'cwsSlotRiseActual_22
        '
        Me.cwsSlotRiseActual_22.FillBackColor = System.Drawing.Color.LightGray
        Me.cwsSlotRiseActual_22.FillColor = System.Drawing.Color.Green
        Me.cwsSlotRiseActual_22.Location = New System.Drawing.Point(380, 38)
        Me.cwsSlotRiseActual_22.Name = "cwsSlotRiseActual_22"
        Me.cwsSlotRiseActual_22.Range = New NationalInstruments.UI.Range(0.0R, 30.0R)
        Me.cwsSlotRiseActual_22.ScaleVisible = False
        Me.cwsSlotRiseActual_22.Size = New System.Drawing.Size(15, 183)
        Me.cwsSlotRiseActual_22.TabIndex = 381
        Me.cwsSlotRiseActual_22.Tag = ""
        Me.cwsSlotRiseActual_22.TankStyle = NationalInstruments.UI.TankStyle.Flat
        '
        'cwsSlotRiseActual_21
        '
        Me.cwsSlotRiseActual_21.FillBackColor = System.Drawing.Color.LightGray
        Me.cwsSlotRiseActual_21.FillColor = System.Drawing.Color.Green
        Me.cwsSlotRiseActual_21.Location = New System.Drawing.Point(340, 38)
        Me.cwsSlotRiseActual_21.Name = "cwsSlotRiseActual_21"
        Me.cwsSlotRiseActual_21.Range = New NationalInstruments.UI.Range(0.0R, 30.0R)
        Me.cwsSlotRiseActual_21.ScaleVisible = False
        Me.cwsSlotRiseActual_21.Size = New System.Drawing.Size(15, 183)
        Me.cwsSlotRiseActual_21.TabIndex = 380
        Me.cwsSlotRiseActual_21.Tag = ""
        Me.cwsSlotRiseActual_21.TankStyle = NationalInstruments.UI.TankStyle.Flat
        '
        'cwsSlotRiseActual_20
        '
        Me.cwsSlotRiseActual_20.FillBackColor = System.Drawing.Color.LightGray
        Me.cwsSlotRiseActual_20.FillColor = System.Drawing.Color.Green
        Me.cwsSlotRiseActual_20.Location = New System.Drawing.Point(299, 38)
        Me.cwsSlotRiseActual_20.Name = "cwsSlotRiseActual_20"
        Me.cwsSlotRiseActual_20.Range = New NationalInstruments.UI.Range(0.0R, 30.0R)
        Me.cwsSlotRiseActual_20.ScaleVisible = False
        Me.cwsSlotRiseActual_20.Size = New System.Drawing.Size(15, 183)
        Me.cwsSlotRiseActual_20.TabIndex = 379
        Me.cwsSlotRiseActual_20.Tag = ""
        Me.cwsSlotRiseActual_20.TankStyle = NationalInstruments.UI.TankStyle.Flat
        '
        'cwsSlotRiseActual_19
        '
        Me.cwsSlotRiseActual_19.FillBackColor = System.Drawing.Color.LightGray
        Me.cwsSlotRiseActual_19.FillColor = System.Drawing.Color.Green
        Me.cwsSlotRiseActual_19.Location = New System.Drawing.Point(259, 38)
        Me.cwsSlotRiseActual_19.Name = "cwsSlotRiseActual_19"
        Me.cwsSlotRiseActual_19.Range = New NationalInstruments.UI.Range(0.0R, 30.0R)
        Me.cwsSlotRiseActual_19.ScaleVisible = False
        Me.cwsSlotRiseActual_19.Size = New System.Drawing.Size(15, 183)
        Me.cwsSlotRiseActual_19.TabIndex = 378
        Me.cwsSlotRiseActual_19.Tag = ""
        Me.cwsSlotRiseActual_19.TankStyle = NationalInstruments.UI.TankStyle.Flat
        '
        'cwsSlotRiseActual_18
        '
        Me.cwsSlotRiseActual_18.FillBackColor = System.Drawing.Color.LightGray
        Me.cwsSlotRiseActual_18.FillColor = System.Drawing.Color.Green
        Me.cwsSlotRiseActual_18.Location = New System.Drawing.Point(218, 38)
        Me.cwsSlotRiseActual_18.Name = "cwsSlotRiseActual_18"
        Me.cwsSlotRiseActual_18.Range = New NationalInstruments.UI.Range(0.0R, 30.0R)
        Me.cwsSlotRiseActual_18.ScaleVisible = False
        Me.cwsSlotRiseActual_18.Size = New System.Drawing.Size(15, 183)
        Me.cwsSlotRiseActual_18.TabIndex = 377
        Me.cwsSlotRiseActual_18.Tag = ""
        Me.cwsSlotRiseActual_18.TankStyle = NationalInstruments.UI.TankStyle.Flat
        '
        'cwsSlotRiseActual_17
        '
        Me.cwsSlotRiseActual_17.FillBackColor = System.Drawing.Color.LightGray
        Me.cwsSlotRiseActual_17.FillColor = System.Drawing.Color.Green
        Me.cwsSlotRiseActual_17.Location = New System.Drawing.Point(178, 38)
        Me.cwsSlotRiseActual_17.Name = "cwsSlotRiseActual_17"
        Me.cwsSlotRiseActual_17.Range = New NationalInstruments.UI.Range(0.0R, 30.0R)
        Me.cwsSlotRiseActual_17.ScaleVisible = False
        Me.cwsSlotRiseActual_17.Size = New System.Drawing.Size(15, 183)
        Me.cwsSlotRiseActual_17.TabIndex = 376
        Me.cwsSlotRiseActual_17.Tag = ""
        Me.cwsSlotRiseActual_17.TankStyle = NationalInstruments.UI.TankStyle.Flat
        '
        'cwsSlotRiseActual_16
        '
        Me.cwsSlotRiseActual_16.FillBackColor = System.Drawing.Color.LightGray
        Me.cwsSlotRiseActual_16.FillColor = System.Drawing.Color.Green
        Me.cwsSlotRiseActual_16.Location = New System.Drawing.Point(138, 38)
        Me.cwsSlotRiseActual_16.Name = "cwsSlotRiseActual_16"
        Me.cwsSlotRiseActual_16.Range = New NationalInstruments.UI.Range(0.0R, 30.0R)
        Me.cwsSlotRiseActual_16.ScaleVisible = False
        Me.cwsSlotRiseActual_16.Size = New System.Drawing.Size(15, 183)
        Me.cwsSlotRiseActual_16.TabIndex = 375
        Me.cwsSlotRiseActual_16.Tag = ""
        Me.cwsSlotRiseActual_16.TankStyle = NationalInstruments.UI.TankStyle.Flat
        '
        'cwsSlotRiseActual_15
        '
        Me.cwsSlotRiseActual_15.FillBackColor = System.Drawing.Color.LightGray
        Me.cwsSlotRiseActual_15.FillColor = System.Drawing.Color.Green
        Me.cwsSlotRiseActual_15.Location = New System.Drawing.Point(97, 38)
        Me.cwsSlotRiseActual_15.Name = "cwsSlotRiseActual_15"
        Me.cwsSlotRiseActual_15.Range = New NationalInstruments.UI.Range(0.0R, 30.0R)
        Me.cwsSlotRiseActual_15.ScaleVisible = False
        Me.cwsSlotRiseActual_15.Size = New System.Drawing.Size(15, 183)
        Me.cwsSlotRiseActual_15.TabIndex = 374
        Me.cwsSlotRiseActual_15.Tag = ""
        Me.cwsSlotRiseActual_15.TankStyle = NationalInstruments.UI.TankStyle.Flat
        '
        'cwsSlotRiseActual_14
        '
        Me.cwsSlotRiseActual_14.FillBackColor = System.Drawing.Color.LightGray
        Me.cwsSlotRiseActual_14.FillColor = System.Drawing.Color.Green
        Me.cwsSlotRiseActual_14.Location = New System.Drawing.Point(59, 38)
        Me.cwsSlotRiseActual_14.Name = "cwsSlotRiseActual_14"
        Me.cwsSlotRiseActual_14.Range = New NationalInstruments.UI.Range(0.0R, 30.0R)
        Me.cwsSlotRiseActual_14.ScaleVisible = False
        Me.cwsSlotRiseActual_14.Size = New System.Drawing.Size(15, 183)
        Me.cwsSlotRiseActual_14.TabIndex = 373
        Me.cwsSlotRiseActual_14.Tag = ""
        Me.cwsSlotRiseActual_14.TankStyle = NationalInstruments.UI.TankStyle.Flat
        '
        'cwsSlotRise_25
        '
        Me.cwsSlotRise_25.FillBackColor = System.Drawing.Color.LightGray
        Me.cwsSlotRise_25.FillMode = NationalInstruments.UI.NumericFillMode.None
        Me.cwsSlotRise_25.Location = New System.Drawing.Point(517, 38)
        Me.cwsSlotRise_25.Name = "cwsSlotRise_25"
        Me.cwsSlotRise_25.OutOfRangeMode = NationalInstruments.UI.NumericOutOfRangeMode.CoerceToRange
        Me.cwsSlotRise_25.PointerColor = System.Drawing.Color.Black
        Me.cwsSlotRise_25.Range = New NationalInstruments.UI.Range(0.0R, 30.0R)
        Me.cwsSlotRise_25.ScalePosition = NationalInstruments.UI.NumericScalePosition.Right
        Me.cwsSlotRise_25.ScaleVisible = False
        Me.cwsSlotRise_25.Size = New System.Drawing.Size(25, 183)
        Me.cwsSlotRise_25.SlideStyle = NationalInstruments.UI.SlideStyle.SunkenWithGrip
        Me.cwsSlotRise_25.TabIndex = 355
        Me.cwsSlotRise_25.Tag = "25"
        Me.cwsSlotRise_25.Value = 15.0R
        '
        'Slide2
        '
        Me.Slide2.Location = New System.Drawing.Point(524, 29)
        Me.Slide2.Name = "Slide2"
        Me.Slide2.Range = New NationalInstruments.UI.Range(0.0R, 30.0R)
        Me.Slide2.ScalePosition = NationalInstruments.UI.NumericScalePosition.Right
        Me.Slide2.Size = New System.Drawing.Size(40, 198)
        Me.Slide2.TabIndex = 372
        '
        'cwsSlotRiseActual_13
        '
        Me.cwsSlotRiseActual_13.FillBackColor = System.Drawing.Color.LightGray
        Me.cwsSlotRiseActual_13.FillColor = System.Drawing.Color.Green
        Me.cwsSlotRiseActual_13.Location = New System.Drawing.Point(17, 38)
        Me.cwsSlotRiseActual_13.Name = "cwsSlotRiseActual_13"
        Me.cwsSlotRiseActual_13.Range = New NationalInstruments.UI.Range(0.0R, 30.0R)
        Me.cwsSlotRiseActual_13.ScaleVisible = False
        Me.cwsSlotRiseActual_13.Size = New System.Drawing.Size(15, 183)
        Me.cwsSlotRiseActual_13.TabIndex = 371
        Me.cwsSlotRiseActual_13.Tag = ""
        Me.cwsSlotRiseActual_13.TankStyle = NationalInstruments.UI.TankStyle.Flat
        '
        'Slide1
        '
        Me.Slide1.Location = New System.Drawing.Point(-5, 28)
        Me.Slide1.Name = "Slide1"
        Me.Slide1.Range = New NationalInstruments.UI.Range(0.0R, 30.0R)
        Me.Slide1.Size = New System.Drawing.Size(40, 200)
        Me.Slide1.TabIndex = 372
        '
        'cwsSlotRise_24
        '
        Me.cwsSlotRise_24.FillBackColor = System.Drawing.Color.LightGray
        Me.cwsSlotRise_24.FillMode = NationalInstruments.UI.NumericFillMode.None
        Me.cwsSlotRise_24.Location = New System.Drawing.Point(476, 38)
        Me.cwsSlotRise_24.Name = "cwsSlotRise_24"
        Me.cwsSlotRise_24.OutOfRangeMode = NationalInstruments.UI.NumericOutOfRangeMode.CoerceToRange
        Me.cwsSlotRise_24.PointerColor = System.Drawing.Color.Black
        Me.cwsSlotRise_24.Range = New NationalInstruments.UI.Range(0.0R, 30.0R)
        Me.cwsSlotRise_24.ScaleVisible = False
        Me.cwsSlotRise_24.Size = New System.Drawing.Size(25, 183)
        Me.cwsSlotRise_24.SlideStyle = NationalInstruments.UI.SlideStyle.SunkenWithGrip
        Me.cwsSlotRise_24.TabIndex = 365
        Me.cwsSlotRise_24.Tag = "24"
        Me.cwsSlotRise_24.Value = 15.0R
        '
        'cwsSlotRise_23
        '
        Me.cwsSlotRise_23.FillBackColor = System.Drawing.Color.LightGray
        Me.cwsSlotRise_23.FillMode = NationalInstruments.UI.NumericFillMode.None
        Me.cwsSlotRise_23.Location = New System.Drawing.Point(436, 38)
        Me.cwsSlotRise_23.Name = "cwsSlotRise_23"
        Me.cwsSlotRise_23.OutOfRangeMode = NationalInstruments.UI.NumericOutOfRangeMode.CoerceToRange
        Me.cwsSlotRise_23.PointerColor = System.Drawing.Color.Black
        Me.cwsSlotRise_23.Range = New NationalInstruments.UI.Range(0.0R, 30.0R)
        Me.cwsSlotRise_23.ScaleVisible = False
        Me.cwsSlotRise_23.Size = New System.Drawing.Size(25, 183)
        Me.cwsSlotRise_23.SlideStyle = NationalInstruments.UI.SlideStyle.SunkenWithGrip
        Me.cwsSlotRise_23.TabIndex = 364
        Me.cwsSlotRise_23.Tag = "23"
        Me.cwsSlotRise_23.Value = 15.0R
        '
        'cwsSlotRise_22
        '
        Me.cwsSlotRise_22.FillBackColor = System.Drawing.Color.LightGray
        Me.cwsSlotRise_22.FillMode = NationalInstruments.UI.NumericFillMode.None
        Me.cwsSlotRise_22.Location = New System.Drawing.Point(395, 38)
        Me.cwsSlotRise_22.Name = "cwsSlotRise_22"
        Me.cwsSlotRise_22.OutOfRangeMode = NationalInstruments.UI.NumericOutOfRangeMode.CoerceToRange
        Me.cwsSlotRise_22.PointerColor = System.Drawing.Color.Black
        Me.cwsSlotRise_22.Range = New NationalInstruments.UI.Range(0.0R, 30.0R)
        Me.cwsSlotRise_22.ScaleVisible = False
        Me.cwsSlotRise_22.Size = New System.Drawing.Size(25, 183)
        Me.cwsSlotRise_22.SlideStyle = NationalInstruments.UI.SlideStyle.SunkenWithGrip
        Me.cwsSlotRise_22.TabIndex = 363
        Me.cwsSlotRise_22.Tag = "22"
        Me.cwsSlotRise_22.Value = 15.0R
        '
        'cwsSlotRise_21
        '
        Me.cwsSlotRise_21.FillBackColor = System.Drawing.Color.LightGray
        Me.cwsSlotRise_21.FillMode = NationalInstruments.UI.NumericFillMode.None
        Me.cwsSlotRise_21.Location = New System.Drawing.Point(354, 38)
        Me.cwsSlotRise_21.Name = "cwsSlotRise_21"
        Me.cwsSlotRise_21.OutOfRangeMode = NationalInstruments.UI.NumericOutOfRangeMode.CoerceToRange
        Me.cwsSlotRise_21.PointerColor = System.Drawing.Color.Black
        Me.cwsSlotRise_21.Range = New NationalInstruments.UI.Range(0.0R, 30.0R)
        Me.cwsSlotRise_21.ScaleVisible = False
        Me.cwsSlotRise_21.Size = New System.Drawing.Size(25, 183)
        Me.cwsSlotRise_21.SlideStyle = NationalInstruments.UI.SlideStyle.SunkenWithGrip
        Me.cwsSlotRise_21.TabIndex = 362
        Me.cwsSlotRise_21.Tag = "21"
        Me.cwsSlotRise_21.Value = 15.0R
        '
        'cwsSlotRise_20
        '
        Me.cwsSlotRise_20.FillBackColor = System.Drawing.Color.LightGray
        Me.cwsSlotRise_20.FillMode = NationalInstruments.UI.NumericFillMode.None
        Me.cwsSlotRise_20.Location = New System.Drawing.Point(314, 38)
        Me.cwsSlotRise_20.Name = "cwsSlotRise_20"
        Me.cwsSlotRise_20.OutOfRangeMode = NationalInstruments.UI.NumericOutOfRangeMode.CoerceToRange
        Me.cwsSlotRise_20.PointerColor = System.Drawing.Color.Black
        Me.cwsSlotRise_20.Range = New NationalInstruments.UI.Range(0.0R, 30.0R)
        Me.cwsSlotRise_20.ScaleVisible = False
        Me.cwsSlotRise_20.Size = New System.Drawing.Size(25, 183)
        Me.cwsSlotRise_20.SlideStyle = NationalInstruments.UI.SlideStyle.SunkenWithGrip
        Me.cwsSlotRise_20.TabIndex = 361
        Me.cwsSlotRise_20.Tag = "20"
        Me.cwsSlotRise_20.Value = 15.0R
        '
        'cwsSlotRise_19
        '
        Me.cwsSlotRise_19.FillBackColor = System.Drawing.Color.LightGray
        Me.cwsSlotRise_19.FillMode = NationalInstruments.UI.NumericFillMode.None
        Me.cwsSlotRise_19.Location = New System.Drawing.Point(273, 38)
        Me.cwsSlotRise_19.Name = "cwsSlotRise_19"
        Me.cwsSlotRise_19.OutOfRangeMode = NationalInstruments.UI.NumericOutOfRangeMode.CoerceToRange
        Me.cwsSlotRise_19.PointerColor = System.Drawing.Color.Black
        Me.cwsSlotRise_19.Range = New NationalInstruments.UI.Range(0.0R, 30.0R)
        Me.cwsSlotRise_19.ScaleVisible = False
        Me.cwsSlotRise_19.Size = New System.Drawing.Size(25, 183)
        Me.cwsSlotRise_19.SlideStyle = NationalInstruments.UI.SlideStyle.SunkenWithGrip
        Me.cwsSlotRise_19.TabIndex = 360
        Me.cwsSlotRise_19.Tag = "19"
        Me.cwsSlotRise_19.Value = 15.0R
        '
        'cwsSlotRise_18
        '
        Me.cwsSlotRise_18.FillBackColor = System.Drawing.Color.LightGray
        Me.cwsSlotRise_18.FillMode = NationalInstruments.UI.NumericFillMode.None
        Me.cwsSlotRise_18.Location = New System.Drawing.Point(233, 38)
        Me.cwsSlotRise_18.Name = "cwsSlotRise_18"
        Me.cwsSlotRise_18.OutOfRangeMode = NationalInstruments.UI.NumericOutOfRangeMode.CoerceToRange
        Me.cwsSlotRise_18.PointerColor = System.Drawing.Color.Black
        Me.cwsSlotRise_18.Range = New NationalInstruments.UI.Range(0.0R, 30.0R)
        Me.cwsSlotRise_18.ScaleVisible = False
        Me.cwsSlotRise_18.Size = New System.Drawing.Size(25, 183)
        Me.cwsSlotRise_18.SlideStyle = NationalInstruments.UI.SlideStyle.SunkenWithGrip
        Me.cwsSlotRise_18.TabIndex = 359
        Me.cwsSlotRise_18.Tag = "18"
        Me.cwsSlotRise_18.Value = 15.0R
        '
        'cwsSlotRise_17
        '
        Me.cwsSlotRise_17.FillBackColor = System.Drawing.Color.LightGray
        Me.cwsSlotRise_17.FillMode = NationalInstruments.UI.NumericFillMode.None
        Me.cwsSlotRise_17.Location = New System.Drawing.Point(192, 38)
        Me.cwsSlotRise_17.Name = "cwsSlotRise_17"
        Me.cwsSlotRise_17.OutOfRangeMode = NationalInstruments.UI.NumericOutOfRangeMode.CoerceToRange
        Me.cwsSlotRise_17.PointerColor = System.Drawing.Color.Black
        Me.cwsSlotRise_17.Range = New NationalInstruments.UI.Range(0.0R, 30.0R)
        Me.cwsSlotRise_17.ScaleVisible = False
        Me.cwsSlotRise_17.Size = New System.Drawing.Size(25, 183)
        Me.cwsSlotRise_17.SlideStyle = NationalInstruments.UI.SlideStyle.SunkenWithGrip
        Me.cwsSlotRise_17.TabIndex = 358
        Me.cwsSlotRise_17.Tag = "17"
        Me.cwsSlotRise_17.Value = 15.0R
        '
        'cwsSlotRise_16
        '
        Me.cwsSlotRise_16.FillBackColor = System.Drawing.Color.LightGray
        Me.cwsSlotRise_16.FillMode = NationalInstruments.UI.NumericFillMode.None
        Me.cwsSlotRise_16.Location = New System.Drawing.Point(152, 38)
        Me.cwsSlotRise_16.Name = "cwsSlotRise_16"
        Me.cwsSlotRise_16.OutOfRangeMode = NationalInstruments.UI.NumericOutOfRangeMode.CoerceToRange
        Me.cwsSlotRise_16.PointerColor = System.Drawing.Color.Black
        Me.cwsSlotRise_16.Range = New NationalInstruments.UI.Range(0.0R, 30.0R)
        Me.cwsSlotRise_16.ScaleVisible = False
        Me.cwsSlotRise_16.Size = New System.Drawing.Size(25, 183)
        Me.cwsSlotRise_16.SlideStyle = NationalInstruments.UI.SlideStyle.SunkenWithGrip
        Me.cwsSlotRise_16.TabIndex = 357
        Me.cwsSlotRise_16.Tag = "16"
        Me.cwsSlotRise_16.Value = 15.0R
        '
        'cwsSlotRise_15
        '
        Me.cwsSlotRise_15.FillBackColor = System.Drawing.Color.LightGray
        Me.cwsSlotRise_15.FillMode = NationalInstruments.UI.NumericFillMode.None
        Me.cwsSlotRise_15.Location = New System.Drawing.Point(112, 38)
        Me.cwsSlotRise_15.Name = "cwsSlotRise_15"
        Me.cwsSlotRise_15.OutOfRangeMode = NationalInstruments.UI.NumericOutOfRangeMode.CoerceToRange
        Me.cwsSlotRise_15.PointerColor = System.Drawing.Color.Black
        Me.cwsSlotRise_15.Range = New NationalInstruments.UI.Range(0.0R, 30.0R)
        Me.cwsSlotRise_15.ScaleVisible = False
        Me.cwsSlotRise_15.Size = New System.Drawing.Size(25, 183)
        Me.cwsSlotRise_15.SlideStyle = NationalInstruments.UI.SlideStyle.SunkenWithGrip
        Me.cwsSlotRise_15.TabIndex = 356
        Me.cwsSlotRise_15.Tag = "15"
        Me.cwsSlotRise_15.Value = 15.0R
        '
        'cwsSlotRise_14
        '
        Me.cwsSlotRise_14.FillBackColor = System.Drawing.Color.LightGray
        Me.cwsSlotRise_14.FillMode = NationalInstruments.UI.NumericFillMode.None
        Me.cwsSlotRise_14.Location = New System.Drawing.Point(71, 38)
        Me.cwsSlotRise_14.Name = "cwsSlotRise_14"
        Me.cwsSlotRise_14.OutOfRangeMode = NationalInstruments.UI.NumericOutOfRangeMode.CoerceToRange
        Me.cwsSlotRise_14.PointerColor = System.Drawing.Color.Black
        Me.cwsSlotRise_14.Range = New NationalInstruments.UI.Range(0.0R, 30.0R)
        Me.cwsSlotRise_14.ScaleVisible = False
        Me.cwsSlotRise_14.Size = New System.Drawing.Size(25, 183)
        Me.cwsSlotRise_14.SlideStyle = NationalInstruments.UI.SlideStyle.SunkenWithGrip
        Me.cwsSlotRise_14.TabIndex = 355
        Me.cwsSlotRise_14.Tag = "14"
        Me.cwsSlotRise_14.Value = 15.0R
        '
        'cwsSlotRise_13
        '
        Me.cwsSlotRise_13.FillBackColor = System.Drawing.Color.LightGray
        Me.cwsSlotRise_13.FillMode = NationalInstruments.UI.NumericFillMode.None
        Me.cwsSlotRise_13.Location = New System.Drawing.Point(33, 38)
        Me.cwsSlotRise_13.Name = "cwsSlotRise_13"
        Me.cwsSlotRise_13.OutOfRangeMode = NationalInstruments.UI.NumericOutOfRangeMode.CoerceToRange
        Me.cwsSlotRise_13.PointerColor = System.Drawing.Color.Black
        Me.cwsSlotRise_13.Range = New NationalInstruments.UI.Range(0.0R, 30.0R)
        Me.cwsSlotRise_13.ScaleVisible = False
        Me.cwsSlotRise_13.Size = New System.Drawing.Size(25, 183)
        Me.cwsSlotRise_13.SlideStyle = NationalInstruments.UI.SlideStyle.SunkenWithGrip
        Me.cwsSlotRise_13.TabIndex = 354
        Me.cwsSlotRise_13.Tag = "13"
        Me.cwsSlotRise_13.Value = 15.0R
        '
        'fraChasTemp_5
        '
        Me.fraChasTemp_5.Controls.Add(Me.picIconGraphic_4)
        Me.fraChasTemp_5.Controls.Add(Me.lblUnit_0)
        Me.fraChasTemp_5.Controls.Add(Me.lblUnit_2)
        Me.fraChasTemp_5.Controls.Add(Me.lblIntakeValue_1)
        Me.fraChasTemp_5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraChasTemp_5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraChasTemp_5.Location = New System.Drawing.Point(8, 300)
        Me.fraChasTemp_5.Name = "fraChasTemp_5"
        Me.fraChasTemp_5.Size = New System.Drawing.Size(187, 58)
        Me.fraChasTemp_5.TabIndex = 285
        Me.fraChasTemp_5.TabStop = False
        Me.fraChasTemp_5.Text = "Chassis Intake Temperature"
        '
        'fraChasTemp_3
        '
        Me.fraChasTemp_3.Controls.Add(Me.lblChassTemp_38)
        Me.fraChasTemp_3.Controls.Add(Me.lblChassTemp_37)
        Me.fraChasTemp_3.Controls.Add(Me.lblChassTemp_36)
        Me.fraChasTemp_3.Controls.Add(Me.lblChassTemp_35)
        Me.fraChasTemp_3.Controls.Add(Me.lblChassTemp_34)
        Me.fraChasTemp_3.Controls.Add(Me.lblChassTemp_33)
        Me.fraChasTemp_3.Controls.Add(Me.lblChassTemp_32)
        Me.fraChasTemp_3.Controls.Add(Me.lblChassTemp_31)
        Me.fraChasTemp_3.Controls.Add(Me.lblChassTemp_30)
        Me.fraChasTemp_3.Controls.Add(Me.lblChassTemp_29)
        Me.fraChasTemp_3.Controls.Add(Me.lblChassTemp_28)
        Me.fraChasTemp_3.Controls.Add(Me.lblChassTemp_27)
        Me.fraChasTemp_3.Controls.Add(Me.lblChassTemp_26)
        Me.fraChasTemp_3.Controls.Add(Me.lblExhaustValue_13)
        Me.fraChasTemp_3.Controls.Add(Me.lblExhaustValue_14)
        Me.fraChasTemp_3.Controls.Add(Me.lblExhaustValue_15)
        Me.fraChasTemp_3.Controls.Add(Me.lblExhaustValue_16)
        Me.fraChasTemp_3.Controls.Add(Me.lblExhaustValue_17)
        Me.fraChasTemp_3.Controls.Add(Me.lblExhaustValue_18)
        Me.fraChasTemp_3.Controls.Add(Me.lblExhaustValue_19)
        Me.fraChasTemp_3.Controls.Add(Me.lblExhaustValue_20)
        Me.fraChasTemp_3.Controls.Add(Me.lblExhaustValue_21)
        Me.fraChasTemp_3.Controls.Add(Me.lblExhaustValue_22)
        Me.fraChasTemp_3.Controls.Add(Me.lblExhaustValue_23)
        Me.fraChasTemp_3.Controls.Add(Me.lblExhaustValue_24)
        Me.fraChasTemp_3.Controls.Add(Me.lblExhaustValue_25)
        Me.fraChasTemp_3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraChasTemp_3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraChasTemp_3.Location = New System.Drawing.Point(8, 4)
        Me.fraChasTemp_3.Name = "fraChasTemp_3"
        Me.fraChasTemp_3.Size = New System.Drawing.Size(551, 70)
        Me.fraChasTemp_3.TabIndex = 258
        Me.fraChasTemp_3.TabStop = False
        Me.fraChasTemp_3.Text = "VXI Slot Exhaust (Celsius)"
        '
        'tabSysmon_Page3
        '
        Me.tabSysmon_Page3.Controls.Add(Me.fraChassisTest_2)
        Me.tabSysmon_Page3.Controls.Add(Me.fraFanSpeed_1)
        Me.tabSysmon_Page3.Controls.Add(Me.fraHeatingUnits_1)
        Me.tabSysmon_Page3.Controls.Add(Me.fraAirflowSensors_1)
        Me.tabSysmon_Page3.Controls.Add(Me.fraFanSpeed_0)
        Me.tabSysmon_Page3.Controls.Add(Me.fraAirflowSensors_0)
        Me.tabSysmon_Page3.Controls.Add(Me.fraHeatingUnits_0)
        Me.tabSysmon_Page3.Controls.Add(Me.fraChassisTest_1)
        Me.tabSysmon_Page3.Location = New System.Drawing.Point(4, 22)
        Me.tabSysmon_Page3.Name = "tabSysmon_Page3"
        Me.tabSysmon_Page3.Size = New System.Drawing.Size(578, 392)
        Me.tabSysmon_Page3.TabIndex = 2
        Me.tabSysmon_Page3.Text = "Environmental"
        '
        'fraChassisTest_2
        '
        Me.fraChassisTest_2.Controls.Add(Me.cwbChasSelfTestIndicator_2)
        Me.fraChassisTest_2.Controls.Add(Me.picIconGraphic_3)
        Me.fraChassisTest_2.Controls.Add(Me.lblCst_2)
        Me.fraChassisTest_2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraChassisTest_2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraChassisTest_2.Location = New System.Drawing.Point(287, 4)
        Me.fraChassisTest_2.Name = "fraChassisTest_2"
        Me.fraChassisTest_2.Size = New System.Drawing.Size(272, 62)
        Me.fraChassisTest_2.TabIndex = 46
        Me.fraChassisTest_2.TabStop = False
        Me.fraChassisTest_2.Text = "Secondary Chassis Status"
        '
        'cwbChasSelfTestIndicator_2
        '
        Me.cwbChasSelfTestIndicator_2.LedStyle = NationalInstruments.UI.LedStyle.Square3D
        Me.cwbChasSelfTestIndicator_2.Location = New System.Drawing.Point(121, 20)
        Me.cwbChasSelfTestIndicator_2.Name = "cwbChasSelfTestIndicator_2"
        Me.cwbChasSelfTestIndicator_2.OffColor = System.Drawing.Color.Red
        Me.cwbChasSelfTestIndicator_2.Size = New System.Drawing.Size(29, 29)
        Me.cwbChasSelfTestIndicator_2.TabIndex = 60
        '
        'fraFanSpeed_1
        '
        Me.fraFanSpeed_1.Controls.Add(Me.panFanSpeed_1)
        Me.fraFanSpeed_1.Controls.Add(Me.picIconGraphic_9)
        Me.fraFanSpeed_1.Controls.Add(Me.txtFanSpeedValue_2)
        Me.fraFanSpeed_1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraFanSpeed_1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraFanSpeed_1.Location = New System.Drawing.Point(287, 292)
        Me.fraFanSpeed_1.Name = "fraFanSpeed_1"
        Me.fraFanSpeed_1.Size = New System.Drawing.Size(272, 66)
        Me.fraFanSpeed_1.TabIndex = 15
        Me.fraFanSpeed_1.TabStop = False
        Me.fraFanSpeed_1.Text = "Secondary Chassis Fan Speed"
        '
        'panFanSpeed_1
        '
        Me.panFanSpeed_1.Location = New System.Drawing.Point(12, 32)
        Me.panFanSpeed_1.Name = "panFanSpeed_1"
        Me.panFanSpeed_1.Size = New System.Drawing.Size(139, 17)
        Me.panFanSpeed_1.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.panFanSpeed_1.TabIndex = 71
        '
        'fraHeatingUnits_1
        '
        Me.fraHeatingUnits_1.Controls.Add(Me.cwbHeaterIndicatorOO_3)
        Me.fraHeatingUnits_1.Controls.Add(Me.cwbHeaterIndicatorOO_2)
        Me.fraHeatingUnits_1.Controls.Add(Me.picIconGraphic_5)
        Me.fraHeatingUnits_1.Controls.Add(Me.lblHu_3)
        Me.fraHeatingUnits_1.Controls.Add(Me.lblHu_2)
        Me.fraHeatingUnits_1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraHeatingUnits_1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraHeatingUnits_1.Location = New System.Drawing.Point(287, 69)
        Me.fraHeatingUnits_1.Name = "fraHeatingUnits_1"
        Me.fraHeatingUnits_1.Size = New System.Drawing.Size(272, 90)
        Me.fraHeatingUnits_1.TabIndex = 13
        Me.fraHeatingUnits_1.TabStop = False
        Me.fraHeatingUnits_1.Text = "Secondary Chassis Heating Units"
        '
        'cwbHeaterIndicatorOO_3
        '
        Me.cwbHeaterIndicatorOO_3.LedStyle = NationalInstruments.UI.LedStyle.Square3D
        Me.cwbHeaterIndicatorOO_3.Location = New System.Drawing.Point(122, 58)
        Me.cwbHeaterIndicatorOO_3.Name = "cwbHeaterIndicatorOO_3"
        Me.cwbHeaterIndicatorOO_3.Size = New System.Drawing.Size(29, 29)
        Me.cwbHeaterIndicatorOO_3.TabIndex = 214
        '
        'cwbHeaterIndicatorOO_2
        '
        Me.cwbHeaterIndicatorOO_2.LedStyle = NationalInstruments.UI.LedStyle.Square3D
        Me.cwbHeaterIndicatorOO_2.Location = New System.Drawing.Point(122, 29)
        Me.cwbHeaterIndicatorOO_2.Name = "cwbHeaterIndicatorOO_2"
        Me.cwbHeaterIndicatorOO_2.Size = New System.Drawing.Size(29, 29)
        Me.cwbHeaterIndicatorOO_2.TabIndex = 213
        Me.cwbHeaterIndicatorOO_2.Value = True
        '
        'fraAirflowSensors_1
        '
        Me.fraAirflowSensors_1.Controls.Add(Me.cwbAirFlowIndicatorPF_6)
        Me.fraAirflowSensors_1.Controls.Add(Me.cwbAirFlowIndicatorPF_5)
        Me.fraAirflowSensors_1.Controls.Add(Me.cwbAirFlowIndicatorPF_4)
        Me.fraAirflowSensors_1.Controls.Add(Me.picIconGraphic_7)
        Me.fraAirflowSensors_1.Controls.Add(Me.lblPca_2)
        Me.fraAirflowSensors_1.Controls.Add(Me.lblPca_0)
        Me.fraAirflowSensors_1.Controls.Add(Me.lblPca_3)
        Me.fraAirflowSensors_1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraAirflowSensors_1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraAirflowSensors_1.Location = New System.Drawing.Point(287, 162)
        Me.fraAirflowSensors_1.Name = "fraAirflowSensors_1"
        Me.fraAirflowSensors_1.Size = New System.Drawing.Size(272, 130)
        Me.fraAirflowSensors_1.TabIndex = 11
        Me.fraAirflowSensors_1.TabStop = False
        Me.fraAirflowSensors_1.Text = "Secondary Chassis Airflow"
        '
        'cwbAirFlowIndicatorPF_6
        '
        Me.cwbAirFlowIndicatorPF_6.LedStyle = NationalInstruments.UI.LedStyle.Square3D
        Me.cwbAirFlowIndicatorPF_6.Location = New System.Drawing.Point(121, 90)
        Me.cwbAirFlowIndicatorPF_6.Name = "cwbAirFlowIndicatorPF_6"
        Me.cwbAirFlowIndicatorPF_6.OffColor = System.Drawing.Color.Red
        Me.cwbAirFlowIndicatorPF_6.Size = New System.Drawing.Size(29, 29)
        Me.cwbAirFlowIndicatorPF_6.TabIndex = 211
        '
        'cwbAirFlowIndicatorPF_5
        '
        Me.cwbAirFlowIndicatorPF_5.LedStyle = NationalInstruments.UI.LedStyle.Square3D
        Me.cwbAirFlowIndicatorPF_5.Location = New System.Drawing.Point(121, 58)
        Me.cwbAirFlowIndicatorPF_5.Name = "cwbAirFlowIndicatorPF_5"
        Me.cwbAirFlowIndicatorPF_5.OffColor = System.Drawing.Color.Red
        Me.cwbAirFlowIndicatorPF_5.Size = New System.Drawing.Size(29, 29)
        Me.cwbAirFlowIndicatorPF_5.TabIndex = 210
        '
        'cwbAirFlowIndicatorPF_4
        '
        Me.cwbAirFlowIndicatorPF_4.LedStyle = NationalInstruments.UI.LedStyle.Square3D
        Me.cwbAirFlowIndicatorPF_4.Location = New System.Drawing.Point(121, 25)
        Me.cwbAirFlowIndicatorPF_4.Name = "cwbAirFlowIndicatorPF_4"
        Me.cwbAirFlowIndicatorPF_4.OffColor = System.Drawing.Color.Red
        Me.cwbAirFlowIndicatorPF_4.Size = New System.Drawing.Size(29, 29)
        Me.cwbAirFlowIndicatorPF_4.TabIndex = 209
        '
        'fraFanSpeed_0
        '
        Me.fraFanSpeed_0.Controls.Add(Me.panFanSpeed_0)
        Me.fraFanSpeed_0.Controls.Add(Me.picIconGraphic_8)
        Me.fraFanSpeed_0.Controls.Add(Me.txtFanSpeedValue_1)
        Me.fraFanSpeed_0.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraFanSpeed_0.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraFanSpeed_0.Location = New System.Drawing.Point(8, 292)
        Me.fraFanSpeed_0.Name = "fraFanSpeed_0"
        Me.fraFanSpeed_0.Size = New System.Drawing.Size(272, 66)
        Me.fraFanSpeed_0.TabIndex = 3
        Me.fraFanSpeed_0.TabStop = False
        Me.fraFanSpeed_0.Text = "Primary Chassis Fan Speed"
        '
        'panFanSpeed_0
        '
        Me.panFanSpeed_0.Location = New System.Drawing.Point(12, 32)
        Me.panFanSpeed_0.Name = "panFanSpeed_0"
        Me.panFanSpeed_0.Size = New System.Drawing.Size(139, 17)
        Me.panFanSpeed_0.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.panFanSpeed_0.TabIndex = 70
        '
        'fraAirflowSensors_0
        '
        Me.fraAirflowSensors_0.Controls.Add(Me.cwbAirFlowIndicatorPF_3)
        Me.fraAirflowSensors_0.Controls.Add(Me.cwbAirFlowIndicatorPF_2)
        Me.fraAirflowSensors_0.Controls.Add(Me.cwbAirFlowIndicatorPF_1)
        Me.fraAirflowSensors_0.Controls.Add(Me.picIconGraphic_6)
        Me.fraAirflowSensors_0.Controls.Add(Me.lblPca_5)
        Me.fraAirflowSensors_0.Controls.Add(Me.lblPca_4)
        Me.fraAirflowSensors_0.Controls.Add(Me.lblPca_1)
        Me.fraAirflowSensors_0.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraAirflowSensors_0.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraAirflowSensors_0.Location = New System.Drawing.Point(8, 162)
        Me.fraAirflowSensors_0.Name = "fraAirflowSensors_0"
        Me.fraAirflowSensors_0.Size = New System.Drawing.Size(272, 130)
        Me.fraAirflowSensors_0.TabIndex = 2
        Me.fraAirflowSensors_0.TabStop = False
        Me.fraAirflowSensors_0.Text = "Primary Chassis Airflow"
        '
        'cwbAirFlowIndicatorPF_3
        '
        Me.cwbAirFlowIndicatorPF_3.LedStyle = NationalInstruments.UI.LedStyle.Square3D
        Me.cwbAirFlowIndicatorPF_3.Location = New System.Drawing.Point(121, 90)
        Me.cwbAirFlowIndicatorPF_3.Name = "cwbAirFlowIndicatorPF_3"
        Me.cwbAirFlowIndicatorPF_3.OffColor = System.Drawing.Color.Red
        Me.cwbAirFlowIndicatorPF_3.Size = New System.Drawing.Size(29, 29)
        Me.cwbAirFlowIndicatorPF_3.TabIndex = 208
        '
        'cwbAirFlowIndicatorPF_2
        '
        Me.cwbAirFlowIndicatorPF_2.LedStyle = NationalInstruments.UI.LedStyle.Square3D
        Me.cwbAirFlowIndicatorPF_2.Location = New System.Drawing.Point(121, 58)
        Me.cwbAirFlowIndicatorPF_2.Name = "cwbAirFlowIndicatorPF_2"
        Me.cwbAirFlowIndicatorPF_2.OffColor = System.Drawing.Color.Red
        Me.cwbAirFlowIndicatorPF_2.Size = New System.Drawing.Size(29, 29)
        Me.cwbAirFlowIndicatorPF_2.TabIndex = 207
        '
        'cwbAirFlowIndicatorPF_1
        '
        Me.cwbAirFlowIndicatorPF_1.LedStyle = NationalInstruments.UI.LedStyle.Square3D
        Me.cwbAirFlowIndicatorPF_1.Location = New System.Drawing.Point(121, 25)
        Me.cwbAirFlowIndicatorPF_1.Name = "cwbAirFlowIndicatorPF_1"
        Me.cwbAirFlowIndicatorPF_1.OffColor = System.Drawing.Color.Red
        Me.cwbAirFlowIndicatorPF_1.Size = New System.Drawing.Size(29, 29)
        Me.cwbAirFlowIndicatorPF_1.TabIndex = 206
        '
        'fraHeatingUnits_0
        '
        Me.fraHeatingUnits_0.Controls.Add(Me.cwbHeaterIndicatorOO_1)
        Me.fraHeatingUnits_0.Controls.Add(Me.cwbHeaterIndicatorOO_0)
        Me.fraHeatingUnits_0.Controls.Add(Me.picIconGraphic_0)
        Me.fraHeatingUnits_0.Controls.Add(Me.lblHu_0)
        Me.fraHeatingUnits_0.Controls.Add(Me.lblHu_1)
        Me.fraHeatingUnits_0.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraHeatingUnits_0.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraHeatingUnits_0.Location = New System.Drawing.Point(8, 69)
        Me.fraHeatingUnits_0.Name = "fraHeatingUnits_0"
        Me.fraHeatingUnits_0.Size = New System.Drawing.Size(272, 90)
        Me.fraHeatingUnits_0.TabIndex = 5
        Me.fraHeatingUnits_0.TabStop = False
        Me.fraHeatingUnits_0.Text = "Primary Chassis Heating Units"
        '
        'cwbHeaterIndicatorOO_1
        '
        Me.cwbHeaterIndicatorOO_1.LedStyle = NationalInstruments.UI.LedStyle.Square3D
        Me.cwbHeaterIndicatorOO_1.Location = New System.Drawing.Point(122, 58)
        Me.cwbHeaterIndicatorOO_1.Name = "cwbHeaterIndicatorOO_1"
        Me.cwbHeaterIndicatorOO_1.Size = New System.Drawing.Size(29, 29)
        Me.cwbHeaterIndicatorOO_1.TabIndex = 212
        '
        'cwbHeaterIndicatorOO_0
        '
        Me.cwbHeaterIndicatorOO_0.LedStyle = NationalInstruments.UI.LedStyle.Square3D
        Me.cwbHeaterIndicatorOO_0.Location = New System.Drawing.Point(122, 29)
        Me.cwbHeaterIndicatorOO_0.Name = "cwbHeaterIndicatorOO_0"
        Me.cwbHeaterIndicatorOO_0.Size = New System.Drawing.Size(29, 29)
        Me.cwbHeaterIndicatorOO_0.TabIndex = 211
        Me.cwbHeaterIndicatorOO_0.Value = True
        '
        'fraChassisTest_1
        '
        Me.fraChassisTest_1.Controls.Add(Me.cwbChasSelfTestIndicator_1)
        Me.fraChassisTest_1.Controls.Add(Me.picIconGraphic_2)
        Me.fraChassisTest_1.Controls.Add(Me.lblCst_1)
        Me.fraChassisTest_1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraChassisTest_1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraChassisTest_1.Location = New System.Drawing.Point(8, 4)
        Me.fraChassisTest_1.Name = "fraChassisTest_1"
        Me.fraChassisTest_1.Size = New System.Drawing.Size(272, 62)
        Me.fraChassisTest_1.TabIndex = 44
        Me.fraChassisTest_1.TabStop = False
        Me.fraChassisTest_1.Text = "Primary Chassis Status"
        '
        'cwbChasSelfTestIndicator_1
        '
        Me.cwbChasSelfTestIndicator_1.LedStyle = NationalInstruments.UI.LedStyle.Square3D
        Me.cwbChasSelfTestIndicator_1.Location = New System.Drawing.Point(121, 20)
        Me.cwbChasSelfTestIndicator_1.Name = "cwbChasSelfTestIndicator_1"
        Me.cwbChasSelfTestIndicator_1.OffColor = System.Drawing.Color.Red
        Me.cwbChasSelfTestIndicator_1.Size = New System.Drawing.Size(29, 29)
        Me.cwbChasSelfTestIndicator_1.TabIndex = 202
        '
        'tabSysmon_Page4
        '
        Me.tabSysmon_Page4.Controls.Add(Me.SSFrame1)
        Me.tabSysmon_Page4.Controls.Add(Me.SSFrame2)
        Me.tabSysmon_Page4.Controls.Add(Me.fraChassisVoltage)
        Me.tabSysmon_Page4.Controls.Add(Me.picIconGraphic_10)
        Me.tabSysmon_Page4.Controls.Add(Me.fraChassisCurrent)
        Me.tabSysmon_Page4.Location = New System.Drawing.Point(4, 22)
        Me.tabSysmon_Page4.Name = "tabSysmon_Page4"
        Me.tabSysmon_Page4.Size = New System.Drawing.Size(578, 392)
        Me.tabSysmon_Page4.TabIndex = 3
        Me.tabSysmon_Page4.Text = "FPU Power"
        '
        'SSFrame1
        '
        Me.SSFrame1.Controls.Add(Me.picIconGraphic_15)
        Me.SSFrame1.Controls.Add(Me.lblLevel_13)
        Me.SSFrame1.Controls.Add(Me.lblLevel_12)
        Me.SSFrame1.Controls.Add(Me.lblLevel_11)
        Me.SSFrame1.Controls.Add(Me.lblLevel_10)
        Me.SSFrame1.Controls.Add(Me.lblLevel_9)
        Me.SSFrame1.Controls.Add(Me.lblLevel_8)
        Me.SSFrame1.Controls.Add(Me.lblLevel_7)
        Me.SSFrame1.Controls.Add(Me.Label2_6)
        Me.SSFrame1.Controls.Add(Me.Label2_5)
        Me.SSFrame1.Controls.Add(Me.Label2_4)
        Me.SSFrame1.Controls.Add(Me.Label2_3)
        Me.SSFrame1.Controls.Add(Me.Label2_2)
        Me.SSFrame1.Controls.Add(Me.Label2_1)
        Me.SSFrame1.Controls.Add(Me.Label2_0)
        Me.SSFrame1.Controls.Add(Me.lblCp_9)
        Me.SSFrame1.Controls.Add(Me.lblCp_8)
        Me.SSFrame1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SSFrame1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.SSFrame1.Location = New System.Drawing.Point(8, 312)
        Me.SSFrame1.Name = "SSFrame1"
        Me.SSFrame1.Size = New System.Drawing.Size(551, 78)
        Me.SSFrame1.TabIndex = 291
        Me.SSFrame1.TabStop = False
        Me.SSFrame1.Text = "Secondary VXI Chassis Voltage Levels"
        '
        'SSFrame2
        '
        Me.SSFrame2.Controls.Add(Me.picIconGraphic_14)
        Me.SSFrame2.Controls.Add(Me.lblLevel_6)
        Me.SSFrame2.Controls.Add(Me.lblLevel_5)
        Me.SSFrame2.Controls.Add(Me.lblLevel_4)
        Me.SSFrame2.Controls.Add(Me.lblLevel_3)
        Me.SSFrame2.Controls.Add(Me.lblLevel_2)
        Me.SSFrame2.Controls.Add(Me.lblLevel_1)
        Me.SSFrame2.Controls.Add(Me.lblCp_5)
        Me.SSFrame2.Controls.Add(Me.lblCp_4)
        Me.SSFrame2.Controls.Add(Me.Label2_76)
        Me.SSFrame2.Controls.Add(Me.Label2_75)
        Me.SSFrame2.Controls.Add(Me.Label2_74)
        Me.SSFrame2.Controls.Add(Me.Label2_73)
        Me.SSFrame2.Controls.Add(Me.Label2_72)
        Me.SSFrame2.Controls.Add(Me.Label2_71)
        Me.SSFrame2.Controls.Add(Me.Label2_70)
        Me.SSFrame2.Controls.Add(Me.lblLevel_0)
        Me.SSFrame2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SSFrame2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.SSFrame2.Location = New System.Drawing.Point(8, 231)
        Me.SSFrame2.Name = "SSFrame2"
        Me.SSFrame2.Size = New System.Drawing.Size(551, 78)
        Me.SSFrame2.TabIndex = 241
        Me.SSFrame2.TabStop = False
        Me.SSFrame2.Text = "Primary VXI Chassis Voltage Levels"
        '
        'fraChassisVoltage
        '
        Me.fraChassisVoltage.Controls.Add(Me.PictureBox2)
        Me.fraChassisVoltage.Controls.Add(Me.lblBackVolt_13)
        Me.fraChassisVoltage.Controls.Add(Me.lblBackVolt_12)
        Me.fraChassisVoltage.Controls.Add(Me.lblBackVolt_11)
        Me.fraChassisVoltage.Controls.Add(Me.lblBackVolt_10)
        Me.fraChassisVoltage.Controls.Add(Me.lblBackVolt_9)
        Me.fraChassisVoltage.Controls.Add(Me.lblBackVolt_8)
        Me.fraChassisVoltage.Controls.Add(Me.lblBackVolt_7)
        Me.fraChassisVoltage.Controls.Add(Me.lblCp_7)
        Me.fraChassisVoltage.Controls.Add(Me.lblCp_6)
        Me.fraChassisVoltage.Controls.Add(Me.lblBackVolt_6)
        Me.fraChassisVoltage.Controls.Add(Me.lblBackVolt_5)
        Me.fraChassisVoltage.Controls.Add(Me.lblBackVolt_4)
        Me.fraChassisVoltage.Controls.Add(Me.lblBackVolt_3)
        Me.fraChassisVoltage.Controls.Add(Me.lblBackVolt_2)
        Me.fraChassisVoltage.Controls.Add(Me.lblBackVolt_1)
        Me.fraChassisVoltage.Controls.Add(Me.lblBackVolt_0)
        Me.fraChassisVoltage.Controls.Add(Me.lblCp_0)
        Me.fraChassisVoltage.Controls.Add(Me.lblCp_1)
        Me.fraChassisVoltage.Controls.Add(Me.Label2_60)
        Me.fraChassisVoltage.Controls.Add(Me.Label2_59)
        Me.fraChassisVoltage.Controls.Add(Me.Label2_58)
        Me.fraChassisVoltage.Controls.Add(Me.Label2_55)
        Me.fraChassisVoltage.Controls.Add(Me.Label2_54)
        Me.fraChassisVoltage.Controls.Add(Me.Label2_53)
        Me.fraChassisVoltage.Controls.Add(Me.Label2_52)
        Me.fraChassisVoltage.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraChassisVoltage.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraChassisVoltage.Location = New System.Drawing.Point(8, 4)
        Me.fraChassisVoltage.Name = "fraChassisVoltage"
        Me.fraChassisVoltage.Size = New System.Drawing.Size(551, 110)
        Me.fraChassisVoltage.TabIndex = 6
        Me.fraChassisVoltage.TabStop = False
        Me.fraChassisVoltage.Text = "FPU Output Voltage"
        '
        'PictureBox2
        '
        Me.PictureBox2.BackColor = System.Drawing.SystemColors.Control
        Me.PictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PictureBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(506, 36)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(36, 36)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox2.TabIndex = 414
        Me.PictureBox2.TabStop = False
        '
        'fraChassisCurrent
        '
        Me.fraChassisCurrent.Controls.Add(Me.picIconGraphic_11)
        Me.fraChassisCurrent.Controls.Add(Me.lblBackCurr_13)
        Me.fraChassisCurrent.Controls.Add(Me.lblBackCurr_12)
        Me.fraChassisCurrent.Controls.Add(Me.lblBackCurr_11)
        Me.fraChassisCurrent.Controls.Add(Me.lblBackCurr_10)
        Me.fraChassisCurrent.Controls.Add(Me.lblBackCurr_9)
        Me.fraChassisCurrent.Controls.Add(Me.lblBackCurr_8)
        Me.fraChassisCurrent.Controls.Add(Me.lblBackCurr_7)
        Me.fraChassisCurrent.Controls.Add(Me.lblCp_11)
        Me.fraChassisCurrent.Controls.Add(Me.lblCp_10)
        Me.fraChassisCurrent.Controls.Add(Me.Label2_13)
        Me.fraChassisCurrent.Controls.Add(Me.Label2_12)
        Me.fraChassisCurrent.Controls.Add(Me.Label2_11)
        Me.fraChassisCurrent.Controls.Add(Me.Label2_10)
        Me.fraChassisCurrent.Controls.Add(Me.Label2_9)
        Me.fraChassisCurrent.Controls.Add(Me.Label2_8)
        Me.fraChassisCurrent.Controls.Add(Me.Label2_7)
        Me.fraChassisCurrent.Controls.Add(Me.lblBackCurr_6)
        Me.fraChassisCurrent.Controls.Add(Me.lblBackCurr_5)
        Me.fraChassisCurrent.Controls.Add(Me.lblBackCurr_4)
        Me.fraChassisCurrent.Controls.Add(Me.lblBackCurr_3)
        Me.fraChassisCurrent.Controls.Add(Me.lblBackCurr_2)
        Me.fraChassisCurrent.Controls.Add(Me.lblBackCurr_1)
        Me.fraChassisCurrent.Controls.Add(Me.lblBackCurr_0)
        Me.fraChassisCurrent.Controls.Add(Me.lblCp_2)
        Me.fraChassisCurrent.Controls.Add(Me.lblCp_3)
        Me.fraChassisCurrent.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraChassisCurrent.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraChassisCurrent.Location = New System.Drawing.Point(8, 118)
        Me.fraChassisCurrent.Name = "fraChassisCurrent"
        Me.fraChassisCurrent.Size = New System.Drawing.Size(551, 110)
        Me.fraChassisCurrent.TabIndex = 20
        Me.fraChassisCurrent.TabStop = False
        Me.fraChassisCurrent.Text = "FPU Output Current"
        '
        'tabSysmon_Page5
        '
        Me.tabSysmon_Page5.Controls.Add(Me.fraUutVoltage)
        Me.tabSysmon_Page5.Controls.Add(Me.fraUutCurrent)
        Me.tabSysmon_Page5.Location = New System.Drawing.Point(4, 22)
        Me.tabSysmon_Page5.Name = "tabSysmon_Page5"
        Me.tabSysmon_Page5.Size = New System.Drawing.Size(578, 392)
        Me.tabSysmon_Page5.TabIndex = 4
        Me.tabSysmon_Page5.Text = "PPU Power"
        '
        'fraUutVoltage
        '
        Me.fraUutVoltage.Controls.Add(Me.picIconGraphic_12)
        Me.fraUutVoltage.Controls.Add(Me.lblUutVolt_10)
        Me.fraUutVoltage.Controls.Add(Me.lblUutVolt_9)
        Me.fraUutVoltage.Controls.Add(Me.lblUutVolt_8)
        Me.fraUutVoltage.Controls.Add(Me.lblUutVolt_7)
        Me.fraUutVoltage.Controls.Add(Me.lblUutVolt_6)
        Me.fraUutVoltage.Controls.Add(Me.lblUutVolt_5)
        Me.fraUutVoltage.Controls.Add(Me.lblUutVolt_4)
        Me.fraUutVoltage.Controls.Add(Me.lblUutVolt_3)
        Me.fraUutVoltage.Controls.Add(Me.lblUutVolt_2)
        Me.fraUutVoltage.Controls.Add(Me.lblUutVolt_1)
        Me.fraUutVoltage.Controls.Add(Me.Label2_89)
        Me.fraUutVoltage.Controls.Add(Me.Label2_88)
        Me.fraUutVoltage.Controls.Add(Me.Label2_87)
        Me.fraUutVoltage.Controls.Add(Me.Label2_86)
        Me.fraUutVoltage.Controls.Add(Me.Label2_85)
        Me.fraUutVoltage.Controls.Add(Me.Label2_84)
        Me.fraUutVoltage.Controls.Add(Me.Label2_83)
        Me.fraUutVoltage.Controls.Add(Me.Label2_82)
        Me.fraUutVoltage.Controls.Add(Me.Label2_81)
        Me.fraUutVoltage.Controls.Add(Me.Label2_80)
        Me.fraUutVoltage.Controls.Add(Me.lblUut_1)
        Me.fraUutVoltage.Controls.Add(Me.lblUut_0)
        Me.fraUutVoltage.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraUutVoltage.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraUutVoltage.Location = New System.Drawing.Point(8, 4)
        Me.fraUutVoltage.Name = "fraUutVoltage"
        Me.fraUutVoltage.Size = New System.Drawing.Size(551, 94)
        Me.fraUutVoltage.TabIndex = 25
        Me.fraUutVoltage.TabStop = False
        Me.fraUutVoltage.Text = "PPU Supply Voltage"
        '
        'fraUutCurrent
        '
        Me.fraUutCurrent.Controls.Add(Me.PictureBox1)
        Me.fraUutCurrent.Controls.Add(Me.lblUutCur_10)
        Me.fraUutCurrent.Controls.Add(Me.lblUutCur_9)
        Me.fraUutCurrent.Controls.Add(Me.lblUutCur_8)
        Me.fraUutCurrent.Controls.Add(Me.lblUutCur_7)
        Me.fraUutCurrent.Controls.Add(Me.lblUutCur_6)
        Me.fraUutCurrent.Controls.Add(Me.lblUutCur_5)
        Me.fraUutCurrent.Controls.Add(Me.lblUutCur_4)
        Me.fraUutCurrent.Controls.Add(Me.lblUutCur_3)
        Me.fraUutCurrent.Controls.Add(Me.lblUutCur_2)
        Me.fraUutCurrent.Controls.Add(Me.lblUutCur_1)
        Me.fraUutCurrent.Controls.Add(Me.Label2_99)
        Me.fraUutCurrent.Controls.Add(Me.Label2_98)
        Me.fraUutCurrent.Controls.Add(Me.Label2_97)
        Me.fraUutCurrent.Controls.Add(Me.Label2_96)
        Me.fraUutCurrent.Controls.Add(Me.Label2_95)
        Me.fraUutCurrent.Controls.Add(Me.Label2_94)
        Me.fraUutCurrent.Controls.Add(Me.Label2_93)
        Me.fraUutCurrent.Controls.Add(Me.Label2_92)
        Me.fraUutCurrent.Controls.Add(Me.Label2_91)
        Me.fraUutCurrent.Controls.Add(Me.Label2_90)
        Me.fraUutCurrent.Controls.Add(Me.lblUut_3)
        Me.fraUutCurrent.Controls.Add(Me.lblUut_2)
        Me.fraUutCurrent.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraUutCurrent.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraUutCurrent.Location = New System.Drawing.Point(8, 102)
        Me.fraUutCurrent.Name = "fraUutCurrent"
        Me.fraUutCurrent.Size = New System.Drawing.Size(551, 94)
        Me.fraUutCurrent.TabIndex = 28
        Me.fraUutCurrent.TabStop = False
        Me.fraUutCurrent.Text = "PPU Supply Current"
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.SystemColors.Control
        Me.PictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PictureBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PictureBox1.Image = Global.sysmon.My.Resources.Resources.frmSysMon_picIconGraphic_10
        Me.PictureBox1.Location = New System.Drawing.Point(506, 34)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(36, 36)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox1.TabIndex = 199
        Me.PictureBox1.TabStop = False
        '
        'tabSysmon_Page6
        '
        Me.tabSysmon_Page6.Controls.Add(Me.fraPDU_5)
        Me.tabSysmon_Page6.Controls.Add(Me.fraFanSpeed_2)
        Me.tabSysmon_Page6.Controls.Add(Me.fraMaintenance_0)
        Me.tabSysmon_Page6.Controls.Add(Me.fraPDU_1)
        Me.tabSysmon_Page6.Controls.Add(Me.fraPDU_2)
        Me.tabSysmon_Page6.Controls.Add(Me.fraPDU_4)
        Me.tabSysmon_Page6.Controls.Add(Me.fraPDU_3)
        Me.tabSysmon_Page6.Controls.Add(Me.fraPDU_0)
        Me.tabSysmon_Page6.Location = New System.Drawing.Point(4, 22)
        Me.tabSysmon_Page6.Name = "tabSysmon_Page6"
        Me.tabSysmon_Page6.Size = New System.Drawing.Size(578, 392)
        Me.tabSysmon_Page6.TabIndex = 5
        Me.tabSysmon_Page6.Text = "PDU"
        '
        'fraPDU_5
        '
        Me.fraPDU_5.Controls.Add(Me.cwb28VOk)
        Me.fraPDU_5.Controls.Add(Me.picIconGraphic_18)
        Me.fraPDU_5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraPDU_5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraPDU_5.Location = New System.Drawing.Point(437, 4)
        Me.fraPDU_5.Name = "fraPDU_5"
        Me.fraPDU_5.Size = New System.Drawing.Size(138, 70)
        Me.fraPDU_5.TabIndex = 352
        Me.fraPDU_5.TabStop = False
        Me.fraPDU_5.Text = "Chassis Power Switch"
        '
        'cwb28VOk
        '
        Me.cwb28VOk.LedStyle = NationalInstruments.UI.LedStyle.Square3D
        Me.cwb28VOk.Location = New System.Drawing.Point(16, 24)
        Me.cwb28VOk.Name = "cwb28VOk"
        Me.cwb28VOk.Size = New System.Drawing.Size(29, 29)
        Me.cwb28VOk.TabIndex = 356
        '
        'fraFanSpeed_2
        '
        Me.fraFanSpeed_2.Controls.Add(Me.panPower)
        Me.fraFanSpeed_2.Controls.Add(Me.lblPower)
        Me.fraFanSpeed_2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraFanSpeed_2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraFanSpeed_2.Location = New System.Drawing.Point(194, 77)
        Me.fraFanSpeed_2.Name = "fraFanSpeed_2"
        Me.fraFanSpeed_2.Size = New System.Drawing.Size(381, 82)
        Me.fraFanSpeed_2.TabIndex = 226
        Me.fraFanSpeed_2.TabStop = False
        Me.fraFanSpeed_2.Text = "System Power Usage"
        '
        'panPower
        '
        Me.panPower.Location = New System.Drawing.Point(16, 36)
        Me.panPower.Name = "panPower"
        Me.panPower.Size = New System.Drawing.Size(284, 17)
        Me.panPower.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.panPower.TabIndex = 229
        '
        'lblPower
        '
        Me.lblPower.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblPower.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblPower.ForeColor = System.Drawing.Color.White
        Me.lblPower.Location = New System.Drawing.Point(311, 36)
        Me.lblPower.Name = "lblPower"
        Me.lblPower.Size = New System.Drawing.Size(37, 19)
        Me.lblPower.TabIndex = 228
        Me.lblPower.Text = "00.00"
        '
        'fraMaintenance_0
        '
        Me.fraMaintenance_0.Controls.Add(Me.lblSysPower_7)
        Me.fraMaintenance_0.Controls.Add(Me.lblSysPower_6)
        Me.fraMaintenance_0.Controls.Add(Me.lblSysPower_5)
        Me.fraMaintenance_0.Controls.Add(Me.lblInputPower_17)
        Me.fraMaintenance_0.Controls.Add(Me.lblInputPower_11)
        Me.fraMaintenance_0.Controls.Add(Me.lblInputPower_10)
        Me.fraMaintenance_0.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraMaintenance_0.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraMaintenance_0.Location = New System.Drawing.Point(8, 223)
        Me.fraMaintenance_0.Name = "fraMaintenance_0"
        Me.fraMaintenance_0.Size = New System.Drawing.Size(179, 130)
        Me.fraMaintenance_0.TabIndex = 217
        Me.fraMaintenance_0.TabStop = False
        Me.fraMaintenance_0.Text = "Available Power"
        '
        'fraPDU_1
        '
        Me.fraPDU_1.Controls.Add(Me.cwbRcvrSwitch)
        Me.fraPDU_1.Controls.Add(Me.picIconGraphic_16)
        Me.fraPDU_1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraPDU_1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraPDU_1.Location = New System.Drawing.Point(323, 4)
        Me.fraPDU_1.Name = "fraPDU_1"
        Me.fraPDU_1.Size = New System.Drawing.Size(114, 70)
        Me.fraPDU_1.TabIndex = 58
        Me.fraPDU_1.TabStop = False
        Me.fraPDU_1.Text = "ITA Engaged"
        '
        'cwbRcvrSwitch
        '
        Me.cwbRcvrSwitch.LedStyle = NationalInstruments.UI.LedStyle.Square3D
        Me.cwbRcvrSwitch.Location = New System.Drawing.Point(16, 24)
        Me.cwbRcvrSwitch.Name = "cwbRcvrSwitch"
        Me.cwbRcvrSwitch.Size = New System.Drawing.Size(29, 29)
        Me.cwbRcvrSwitch.TabIndex = 225
        '
        'fraPDU_2
        '
        Me.fraPDU_2.Controls.Add(Me.cwbProbe)
        Me.fraPDU_2.Controls.Add(Me.picIconGraphic_17)
        Me.fraPDU_2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraPDU_2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraPDU_2.Location = New System.Drawing.Point(194, 4)
        Me.fraPDU_2.Name = "fraPDU_2"
        Me.fraPDU_2.Size = New System.Drawing.Size(132, 70)
        Me.fraPDU_2.TabIndex = 57
        Me.fraPDU_2.TabStop = False
        Me.fraPDU_2.Text = "Analog Probe Button"
        '
        'cwbProbe
        '
        Me.cwbProbe.LedStyle = NationalInstruments.UI.LedStyle.Square3D
        Me.cwbProbe.Location = New System.Drawing.Point(16, 24)
        Me.cwbProbe.Name = "cwbProbe"
        Me.cwbProbe.Size = New System.Drawing.Size(29, 29)
        Me.cwbProbe.TabIndex = 226
        '
        'fraPDU_4
        '
        Me.fraPDU_4.Controls.Add(Me.lblInputPower_16)
        Me.fraPDU_4.Controls.Add(Me.lblSysPower_0)
        Me.fraPDU_4.Controls.Add(Me.lblSysPower_4)
        Me.fraPDU_4.Controls.Add(Me.lblSysPower_3)
        Me.fraPDU_4.Controls.Add(Me.lblSysPower_2)
        Me.fraPDU_4.Controls.Add(Me.lblSysPower_1)
        Me.fraPDU_4.Controls.Add(Me.lblInputPower_15)
        Me.fraPDU_4.Controls.Add(Me.lblInputPower_14)
        Me.fraPDU_4.Controls.Add(Me.lblInputPower_13)
        Me.fraPDU_4.Controls.Add(Me.lblInputPower_12)
        Me.fraPDU_4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraPDU_4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraPDU_4.Location = New System.Drawing.Point(376, 162)
        Me.fraPDU_4.Name = "fraPDU_4"
        Me.fraPDU_4.Size = New System.Drawing.Size(199, 195)
        Me.fraPDU_4.TabIndex = 48
        Me.fraPDU_4.TabStop = False
        Me.fraPDU_4.Text = "Power Demands"
        '
        'fraPDU_3
        '
        Me.fraPDU_3.Controls.Add(Me.cwbInputPower)
        Me.fraPDU_3.Controls.Add(Me.cwbPowerConverters)
        Me.fraPDU_3.Controls.Add(Me.cwbHamPhase_3)
        Me.fraPDU_3.Controls.Add(Me.cwbHamPhase_2)
        Me.fraPDU_3.Controls.Add(Me.cwbHamPhase_1)
        Me.fraPDU_3.Controls.Add(Me.lblSourceMode)
        Me.fraPDU_3.Controls.Add(Me.lblInputPower_7)
        Me.fraPDU_3.Controls.Add(Me.lblInputPower_3)
        Me.fraPDU_3.Controls.Add(Me.lblInputPower_2)
        Me.fraPDU_3.Controls.Add(Me.lblInputPower_1)
        Me.fraPDU_3.Controls.Add(Me.lblInputPower_0)
        Me.fraPDU_3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraPDU_3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraPDU_3.Location = New System.Drawing.Point(8, 4)
        Me.fraPDU_3.Name = "fraPDU_3"
        Me.fraPDU_3.Size = New System.Drawing.Size(179, 215)
        Me.fraPDU_3.TabIndex = 33
        Me.fraPDU_3.TabStop = False
        Me.fraPDU_3.Text = "Power Distribution Status"
        '
        'cwbInputPower
        '
        Me.cwbInputPower.LedStyle = NationalInstruments.UI.LedStyle.Square3D
        Me.cwbInputPower.Location = New System.Drawing.Point(125, 175)
        Me.cwbInputPower.Name = "cwbInputPower"
        Me.cwbInputPower.OffColor = System.Drawing.Color.Red
        Me.cwbInputPower.Size = New System.Drawing.Size(29, 29)
        Me.cwbInputPower.TabIndex = 359
        '
        'cwbPowerConverters
        '
        Me.cwbPowerConverters.LedStyle = NationalInstruments.UI.LedStyle.Square3D
        Me.cwbPowerConverters.Location = New System.Drawing.Point(125, 143)
        Me.cwbPowerConverters.Name = "cwbPowerConverters"
        Me.cwbPowerConverters.Size = New System.Drawing.Size(29, 29)
        Me.cwbPowerConverters.TabIndex = 358
        '
        'cwbHamPhase_3
        '
        Me.cwbHamPhase_3.LedStyle = NationalInstruments.UI.LedStyle.Square3D
        Me.cwbHamPhase_3.Location = New System.Drawing.Point(125, 102)
        Me.cwbHamPhase_3.Name = "cwbHamPhase_3"
        Me.cwbHamPhase_3.Size = New System.Drawing.Size(29, 29)
        Me.cwbHamPhase_3.TabIndex = 357
        Me.cwbHamPhase_3.Value = True
        '
        'cwbHamPhase_2
        '
        Me.cwbHamPhase_2.LedStyle = NationalInstruments.UI.LedStyle.Square3D
        Me.cwbHamPhase_2.Location = New System.Drawing.Point(125, 74)
        Me.cwbHamPhase_2.Name = "cwbHamPhase_2"
        Me.cwbHamPhase_2.Size = New System.Drawing.Size(29, 29)
        Me.cwbHamPhase_2.TabIndex = 356
        '
        'cwbHamPhase_1
        '
        Me.cwbHamPhase_1.LedStyle = NationalInstruments.UI.LedStyle.Square3D
        Me.cwbHamPhase_1.Location = New System.Drawing.Point(125, 46)
        Me.cwbHamPhase_1.Name = "cwbHamPhase_1"
        Me.cwbHamPhase_1.Size = New System.Drawing.Size(29, 29)
        Me.cwbHamPhase_1.TabIndex = 355
        Me.cwbHamPhase_1.Value = True
        '
        'lblSourceMode
        '
        Me.lblSourceMode.AutoSize = True
        Me.lblSourceMode.BackColor = System.Drawing.SystemColors.Control
        Me.lblSourceMode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSourceMode.Location = New System.Drawing.Point(12, 24)
        Me.lblSourceMode.Name = "lblSourceMode"
        Me.lblSourceMode.Size = New System.Drawing.Size(92, 13)
        Me.lblSourceMode.TabIndex = 54
        Me.lblSourceMode.Text = "Source Mode: DC"
        '
        'fraPDU_0
        '
        Me.fraPDU_0.Controls.Add(Me.lblInputPower_8)
        Me.fraPDU_0.Controls.Add(Me.lblInPower_4)
        Me.fraPDU_0.Controls.Add(Me.lblInPower_3)
        Me.fraPDU_0.Controls.Add(Me.lblInPower_2)
        Me.fraPDU_0.Controls.Add(Me.lblInPower_1)
        Me.fraPDU_0.Controls.Add(Me.lblInPower_0)
        Me.fraPDU_0.Controls.Add(Me.lblInputPower_9)
        Me.fraPDU_0.Controls.Add(Me.lblInputPower_6)
        Me.fraPDU_0.Controls.Add(Me.lblInputPower_5)
        Me.fraPDU_0.Controls.Add(Me.lblInputPower_4)
        Me.fraPDU_0.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraPDU_0.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraPDU_0.Location = New System.Drawing.Point(194, 162)
        Me.fraPDU_0.Name = "fraPDU_0"
        Me.fraPDU_0.Size = New System.Drawing.Size(175, 193)
        Me.fraPDU_0.TabIndex = 7
        Me.fraPDU_0.TabStop = False
        Me.fraPDU_0.Text = "Input Power"
        '
        'tabSysmon_Page7
        '
        Me.tabSysmon_Page7.Controls.Add(Me.fraMaintenance_7)
        Me.tabSysmon_Page7.Controls.Add(Me.fraMaintenance_4)
        Me.tabSysmon_Page7.Controls.Add(Me.fraMaintenance_6)
        Me.tabSysmon_Page7.Controls.Add(Me.fraMaintenance_5)
        Me.tabSysmon_Page7.Controls.Add(Me.fraMaintenance_1)
        Me.tabSysmon_Page7.Controls.Add(Me.fraMaintenance_2)
        Me.tabSysmon_Page7.Controls.Add(Me.cmdResetChassis_1)
        Me.tabSysmon_Page7.Controls.Add(Me.cmdResetChassis_2)
        Me.tabSysmon_Page7.Controls.Add(Me.cmdResetPdu_0)
        Me.tabSysmon_Page7.Controls.Add(Me.fraMaintenance_3)
        Me.tabSysmon_Page7.Controls.Add(Me.cmdFixEthernet)
        Me.tabSysmon_Page7.Controls.Add(Me.lblAccount)
        Me.tabSysmon_Page7.Location = New System.Drawing.Point(4, 22)
        Me.tabSysmon_Page7.Name = "tabSysmon_Page7"
        Me.tabSysmon_Page7.Size = New System.Drawing.Size(578, 392)
        Me.tabSysmon_Page7.TabIndex = 6
        Me.tabSysmon_Page7.Text = "Maintenance"
        '
        'fraMaintenance_7
        '
        Me.fraMaintenance_7.Controls.Add(Me.Label6)
        Me.fraMaintenance_7.Controls.Add(Me.Label5)
        Me.fraMaintenance_7.Controls.Add(Me.Label4)
        Me.fraMaintenance_7.Controls.Add(Me.Label3)
        Me.fraMaintenance_7.Controls.Add(Me.Line1)
        Me.fraMaintenance_7.Controls.Add(Me.lblmode_9)
        Me.fraMaintenance_7.Controls.Add(Me.lblmode_8)
        Me.fraMaintenance_7.Controls.Add(Me.lblmode_7)
        Me.fraMaintenance_7.Controls.Add(Me.lblmode_0)
        Me.fraMaintenance_7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraMaintenance_7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraMaintenance_7.Location = New System.Drawing.Point(198, 267)
        Me.fraMaintenance_7.Name = "fraMaintenance_7"
        Me.fraMaintenance_7.Size = New System.Drawing.Size(183, 118)
        Me.fraMaintenance_7.TabIndex = 430
        Me.fraMaintenance_7.TabStop = False
        Me.fraMaintenance_7.Text = "System Elapsed Time"
        Me.fraMaintenance_7.Visible = False
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(8, 97)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(85, 17)
        Me.Label6.TabIndex = 438
        Me.Label6.Text = "Total Sys. Time"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(8, 69)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(82, 17)
        Me.Label5.TabIndex = 437
        Me.Label5.Text = "DC "
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(8, 44)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(82, 17)
        Me.Label4.TabIndex = 436
        Me.Label4.Text = "1-Phase A/C "
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(8, 20)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(90, 17)
        Me.Label3.TabIndex = 435
        Me.Label3.Text = "3-Phase A/C "
        '
        'Line1
        '
        Me.Line1.BackColor = System.Drawing.SystemColors.WindowText
        Me.Line1.Location = New System.Drawing.Point(8, 89)
        Me.Line1.Name = "Line1"
        Me.Line1.Size = New System.Drawing.Size(154, 1)
        Me.Line1.TabIndex = 439
        '
        'fraMaintenance_4
        '
        Me.fraMaintenance_4.Controls.Add(Me.Panel_2)
        Me.fraMaintenance_4.Controls.Add(Me.Panel_4)
        Me.fraMaintenance_4.Controls.Add(Me.chkHeater_2)
        Me.fraMaintenance_4.Controls.Add(Me.chkHeater_3)
        Me.fraMaintenance_4.Controls.Add(Me.chkEnableChassisOption_6)
        Me.fraMaintenance_4.Controls.Add(Me.chkEnableChassisOption_2)
        Me.fraMaintenance_4.Controls.Add(Me.chkEnableChassisOption_4)
        Me.fraMaintenance_4.Controls.Add(Me.lblmode_6)
        Me.fraMaintenance_4.Controls.Add(Me.lblmode_2)
        Me.fraMaintenance_4.Controls.Add(Me.lblmode_4)
        Me.fraMaintenance_4.Controls.Add(Me.Label1_5)
        Me.fraMaintenance_4.Controls.Add(Me.Label1_4)
        Me.fraMaintenance_4.Controls.Add(Me.Label1_3)
        Me.fraMaintenance_4.Controls.Add(Me.lblUnit_21)
        Me.fraMaintenance_4.Controls.Add(Me.lblUnit_22)
        Me.fraMaintenance_4.Controls.Add(Me.lblUnit_18)
        Me.fraMaintenance_4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.24!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraMaintenance_4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraMaintenance_4.Location = New System.Drawing.Point(198, 77)
        Me.fraMaintenance_4.Name = "fraMaintenance_4"
        Me.fraMaintenance_4.Size = New System.Drawing.Size(183, 187)
        Me.fraMaintenance_4.TabIndex = 381
        Me.fraMaintenance_4.TabStop = False
        Me.fraMaintenance_4.Text = "Secondary Chassis Control"
        Me.fraMaintenance_4.Visible = False
        '
        'Panel_2
        '
        Me.Panel_2.Enabled = False
        Me.Panel_2.FormatMode = NationalInstruments.UI.NumericFormatMode.CreateSimpleDoubleMode(0)
        Me.Panel_2.Location = New System.Drawing.Point(77, 98)
        Me.Panel_2.Name = "Panel_2"
        Me.Panel_2.OutOfRangeMode = NationalInstruments.UI.NumericOutOfRangeMode.CoerceToRange
        Me.Panel_2.Range = New NationalInstruments.UI.Range(50.0R, 100.0R)
        Me.Panel_2.Size = New System.Drawing.Size(47, 20)
        Me.Panel_2.TabIndex = 408
        Me.Panel_2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.Panel_2.Value = 100.0R
        '
        'Panel_4
        '
        Me.Panel_4.Enabled = False
        Me.Panel_4.FormatMode = NationalInstruments.UI.NumericFormatMode.CreateSimpleDoubleMode(0)
        Me.Panel_4.Location = New System.Drawing.Point(77, 45)
        Me.Panel_4.Name = "Panel_4"
        Me.Panel_4.OutOfRangeMode = NationalInstruments.UI.NumericOutOfRangeMode.CoerceToRange
        Me.Panel_4.Range = New NationalInstruments.UI.Range(10.0R, 55.0R)
        Me.Panel_4.Size = New System.Drawing.Size(47, 20)
        Me.Panel_4.TabIndex = 405
        Me.Panel_4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.Panel_4.Value = 35.0R
        '
        'fraMaintenance_6
        '
        Me.fraMaintenance_6.Controls.Add(Me.cmdCalChass_1)
        Me.fraMaintenance_6.Controls.Add(Me.cmdCalChass_2)
        Me.fraMaintenance_6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraMaintenance_6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraMaintenance_6.Location = New System.Drawing.Point(8, 267)
        Me.fraMaintenance_6.Name = "fraMaintenance_6"
        Me.fraMaintenance_6.Size = New System.Drawing.Size(179, 118)
        Me.fraMaintenance_6.TabIndex = 90
        Me.fraMaintenance_6.TabStop = False
        Me.fraMaintenance_6.Text = "Temperature Compensation"
        Me.fraMaintenance_6.Visible = False
        '
        'fraMaintenance_5
        '
        Me.fraMaintenance_5.Controls.Add(Me.chkPpuNoFault_10)
        Me.fraMaintenance_5.Controls.Add(Me.chkPpuNoFault_9)
        Me.fraMaintenance_5.Controls.Add(Me.chkPpuNoFault_8)
        Me.fraMaintenance_5.Controls.Add(Me.chkPpuNoFault_7)
        Me.fraMaintenance_5.Controls.Add(Me.chkPpuNoFault_6)
        Me.fraMaintenance_5.Controls.Add(Me.chkPpuNoFault_5)
        Me.fraMaintenance_5.Controls.Add(Me.chkPpuNoFault_4)
        Me.fraMaintenance_5.Controls.Add(Me.chkPpuNoFault_3)
        Me.fraMaintenance_5.Controls.Add(Me.chkPpuNoFault_2)
        Me.fraMaintenance_5.Controls.Add(Me.chkPpuNoFault_1)
        Me.fraMaintenance_5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraMaintenance_5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraMaintenance_5.Location = New System.Drawing.Point(388, 77)
        Me.fraMaintenance_5.Name = "fraMaintenance_5"
        Me.fraMaintenance_5.Size = New System.Drawing.Size(171, 228)
        Me.fraMaintenance_5.TabIndex = 79
        Me.fraMaintenance_5.TabStop = False
        Me.fraMaintenance_5.Text = "PPU Fault Ignore"
        Me.fraMaintenance_5.Visible = False
        '
        'fraMaintenance_1
        '
        Me.fraMaintenance_1.Controls.Add(Me.chkDataInterval)
        Me.fraMaintenance_1.Controls.Add(Me.lblUnit_16)
        Me.fraMaintenance_1.Controls.Add(Me.lblActivity)
        Me.fraMaintenance_1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.22!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraMaintenance_1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraMaintenance_1.Location = New System.Drawing.Point(8, 4)
        Me.fraMaintenance_1.Name = "fraMaintenance_1"
        Me.fraMaintenance_1.Size = New System.Drawing.Size(183, 70)
        Me.fraMaintenance_1.TabIndex = 75
        Me.fraMaintenance_1.TabStop = False
        Me.fraMaintenance_1.Text = "Data Acquisition"
        Me.fraMaintenance_1.Visible = False
        '
        'chkDataInterval
        '
        Me.chkDataInterval.Checked = True
        Me.chkDataInterval.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkDataInterval.Location = New System.Drawing.Point(12, 20)
        Me.chkDataInterval.Name = "chkDataInterval"
        Me.chkDataInterval.Size = New System.Drawing.Size(132, 21)
        Me.chkDataInterval.TabIndex = 76
        Me.chkDataInterval.Text = "Acquire New Data"
        '
        'lblActivity
        '
        Me.lblActivity.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.lblActivity.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblActivity.ForeColor = System.Drawing.Color.White
        Me.lblActivity.Location = New System.Drawing.Point(72, 48)
        Me.lblActivity.Name = "lblActivity"
        Me.lblActivity.Size = New System.Drawing.Size(108, 17)
        Me.lblActivity.TabIndex = 77
        Me.lblActivity.Text = "Secondary Chassis"
        '
        'fraMaintenance_2
        '
        Me.fraMaintenance_2.Controls.Add(Me.chkFpuNoFault)
        Me.fraMaintenance_2.Controls.Add(Me.optFpuState_1)
        Me.fraMaintenance_2.Controls.Add(Me.optFpuState_0)
        Me.fraMaintenance_2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraMaintenance_2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraMaintenance_2.Location = New System.Drawing.Point(198, 4)
        Me.fraMaintenance_2.Name = "fraMaintenance_2"
        Me.fraMaintenance_2.Size = New System.Drawing.Size(280, 70)
        Me.fraMaintenance_2.TabIndex = 72
        Me.fraMaintenance_2.TabStop = False
        Me.fraMaintenance_2.Text = "Fixed Power Unit (FPU)"
        Me.fraMaintenance_2.Visible = False
        '
        'chkFpuNoFault
        '
        Me.chkFpuNoFault.Location = New System.Drawing.Point(129, 29)
        Me.chkFpuNoFault.Name = "chkFpuNoFault"
        Me.chkFpuNoFault.Size = New System.Drawing.Size(146, 25)
        Me.chkFpuNoFault.TabIndex = 73
        Me.chkFpuNoFault.Text = "Ignore Fault Conditions"
        '
        'optFpuState_1
        '
        Me.optFpuState_1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optFpuState_1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optFpuState_1.Location = New System.Drawing.Point(12, 40)
        Me.optFpuState_1.Name = "optFpuState_1"
        Me.optFpuState_1.Size = New System.Drawing.Size(112, 17)
        Me.optFpuState_1.TabIndex = 310
        Me.optFpuState_1.Text = "FPU Supplies Off"
        '
        'optFpuState_0
        '
        Me.optFpuState_0.Checked = True
        Me.optFpuState_0.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optFpuState_0.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optFpuState_0.Location = New System.Drawing.Point(12, 24)
        Me.optFpuState_0.Name = "optFpuState_0"
        Me.optFpuState_0.Size = New System.Drawing.Size(112, 17)
        Me.optFpuState_0.TabIndex = 309
        Me.optFpuState_0.TabStop = True
        Me.optFpuState_0.Text = "FPU Supplies On"
        '
        'fraMaintenance_3
        '
        Me.fraMaintenance_3.Controls.Add(Me.Panel_1)
        Me.fraMaintenance_3.Controls.Add(Me.Panel_3)
        Me.fraMaintenance_3.Controls.Add(Me.chkHeater_0)
        Me.fraMaintenance_3.Controls.Add(Me.chkHeater_1)
        Me.fraMaintenance_3.Controls.Add(Me.chkEnableChassisOption_5)
        Me.fraMaintenance_3.Controls.Add(Me.chkEnableChassisOption_1)
        Me.fraMaintenance_3.Controls.Add(Me.chkEnableChassisOption_3)
        Me.fraMaintenance_3.Controls.Add(Me.lblmode_5)
        Me.fraMaintenance_3.Controls.Add(Me.lblmode_1)
        Me.fraMaintenance_3.Controls.Add(Me.lblmode_3)
        Me.fraMaintenance_3.Controls.Add(Me.Label1_2)
        Me.fraMaintenance_3.Controls.Add(Me.Label1_1)
        Me.fraMaintenance_3.Controls.Add(Me.Label1_0)
        Me.fraMaintenance_3.Controls.Add(Me.lblUnit_17)
        Me.fraMaintenance_3.Controls.Add(Me.lblUnit_19)
        Me.fraMaintenance_3.Controls.Add(Me.lblUnit_20)
        Me.fraMaintenance_3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.24!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraMaintenance_3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraMaintenance_3.Location = New System.Drawing.Point(8, 77)
        Me.fraMaintenance_3.Name = "fraMaintenance_3"
        Me.fraMaintenance_3.Size = New System.Drawing.Size(183, 187)
        Me.fraMaintenance_3.TabIndex = 363
        Me.fraMaintenance_3.TabStop = False
        Me.fraMaintenance_3.Text = "Primary Chassis Control"
        Me.fraMaintenance_3.Visible = False
        '
        'Panel_1
        '
        Me.Panel_1.Enabled = False
        Me.Panel_1.FormatMode = NationalInstruments.UI.NumericFormatMode.CreateSimpleDoubleMode(0)
        Me.Panel_1.Location = New System.Drawing.Point(77, 98)
        Me.Panel_1.Name = "Panel_1"
        Me.Panel_1.OutOfRangeMode = NationalInstruments.UI.NumericOutOfRangeMode.CoerceToRange
        Me.Panel_1.Range = New NationalInstruments.UI.Range(50.0R, 100.0R)
        Me.Panel_1.Size = New System.Drawing.Size(47, 20)
        Me.Panel_1.TabIndex = 407
        Me.Panel_1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.Panel_1.Value = 100.0R
        '
        'Panel_3
        '
        Me.Panel_3.Enabled = False
        Me.Panel_3.FormatMode = NationalInstruments.UI.NumericFormatMode.CreateSimpleDoubleMode(0)
        Me.Panel_3.Location = New System.Drawing.Point(77, 45)
        Me.Panel_3.Name = "Panel_3"
        Me.Panel_3.OutOfRangeMode = NationalInstruments.UI.NumericOutOfRangeMode.CoerceToRange
        Me.Panel_3.Range = New NationalInstruments.UI.Range(10.0R, 55.0R)
        Me.Panel_3.Size = New System.Drawing.Size(47, 20)
        Me.Panel_3.TabIndex = 406
        Me.Panel_3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.Panel_3.Value = 35.0R
        '
        'cmdFixEthernet
        '
        Me.cmdFixEthernet.BackColor = System.Drawing.SystemColors.Control
        Me.cmdFixEthernet.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdFixEthernet.Location = New System.Drawing.Point(485, 21)
        Me.cmdFixEthernet.Name = "cmdFixEthernet"
        Me.cmdFixEthernet.Size = New System.Drawing.Size(74, 50)
        Me.cmdFixEthernet.TabIndex = 439
        Me.cmdFixEthernet.Text = "Reset Ethernet Ports"
        Me.cmdFixEthernet.UseVisualStyleBackColor = False
        Me.cmdFixEthernet.Visible = False
        '
        'lblAccount
        '
        Me.lblAccount.AutoSize = True
        Me.lblAccount.BackColor = System.Drawing.SystemColors.Control
        Me.lblAccount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAccount.Location = New System.Drawing.Point(28, 126)
        Me.lblAccount.Name = "lblAccount"
        Me.lblAccount.Size = New System.Drawing.Size(489, 13)
        Me.lblAccount.TabIndex = 91
        Me.lblAccount.Text = "*Other options in the Maintenance Tab are only available when logged in to the maintainer account."
        '
        'sbrUserInformation
        '
        Me.sbrUserInformation.Location = New System.Drawing.Point(0, 460)
        Me.sbrUserInformation.Name = "sbrUserInformation"
        Me.sbrUserInformation.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.sbrUserInformation_Panel1})
        Me.sbrUserInformation.ShowPanels = True
        Me.sbrUserInformation.Size = New System.Drawing.Size(606, 18)
        Me.sbrUserInformation.SizingGrip = False
        Me.sbrUserInformation.TabIndex = 105
        '
        'sbrUserInformation_Panel1
        '
        Me.sbrUserInformation_Panel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
        Me.sbrUserInformation_Panel1.Name = "sbrUserInformation_Panel1"
        Me.sbrUserInformation_Panel1.Width = 606
        '
        'PictureBox3
        '
        Me.PictureBox3.Image = Global.sysmon.My.Resources.Resources.AstronicsLogoTransparent1
        Me.PictureBox3.Location = New System.Drawing.Point(8, 428)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(102, 28)
        Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox3.TabIndex = 312
        Me.PictureBox3.TabStop = False
        '
        'frmSysMon
        '
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(606, 478)
        Me.Controls.Add(Me.PictureBox3)
        Me.Controls.Add(Me.cmdAbout)
        Me.Controls.Add(Me.cmdShutDown)
        Me.Controls.Add(Me.cmdQuitSysMon)
        Me.Controls.Add(Me.tabSysmon)
        Me.Controls.Add(Me.sbrUserInformation)
        Me.ForeColor = System.Drawing.SystemColors.WindowText
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSysMon"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ATS System Monitor "
        CType(Me.picIconGraphic, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picIconGraphic_1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picIconGraphic_4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picIconGraphic_3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picIconGraphic_9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picIconGraphic_5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picIconGraphic_7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picIconGraphic_8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picIconGraphic_6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picIconGraphic_0, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picIconGraphic_2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picIconGraphic_15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picIconGraphic_14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picIconGraphic_10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picIconGraphic_11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picIconGraphic_12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picIconGraphic_18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picIconGraphic_16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picIconGraphic_17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblIntakeValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblUnit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmdResetThresholds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.linRise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblChassTemp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblExhaustValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmdSaveThresholds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCst, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFanSpeedValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblHu, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPca, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLevel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBackVolt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBackCurr, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblUutVolt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblUut, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblUutCur, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSysPower, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblInputPower, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblInPower, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblmode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextBox, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmdCalChass, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmdResetChassis, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmdResetPdu, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabSysmon.ResumeLayout(False)
        Me.tabSysmon_Page1.ResumeLayout(False)
        Me.fraChasTemp_2.ResumeLayout(False)
        Me.fraChasTemp_2.PerformLayout()
        Me.fraChasTemp_1.ResumeLayout(False)
        Me.fraChasTemp_1.PerformLayout()
        CType(Me.cwsSlotRise_12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dummySlide_1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cwsSlotRiseActual_0, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dummySlide_0, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cwsSlotRiseActual_12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cwsSlotRiseActual_11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cwsSlotRiseActual_10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cwsSlotRiseActual_9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cwsSlotRiseActual_8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cwsSlotRiseActual_7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cwsSlotRiseActual_6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cwsSlotRiseActual_5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cwsSlotRiseActual_4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cwsSlotRiseActual_3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cwsSlotRiseActual_2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cwsSlotRise_1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cwsSlotRiseActual_1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cwsSlotRise_11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cwsSlotRise_10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cwsSlotRise_9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cwsSlotRise_8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cwsSlotRise_7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cwsSlotRise_6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cwsSlotRise_5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cwsSlotRise_4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cwsSlotRise_3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cwsSlotRise_2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cwsSlotRise_0, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraChasTemp_0.ResumeLayout(False)
        Me.fraChasTemp_0.PerformLayout()
        Me.tabSysmon_Page2.ResumeLayout(False)
        Me.fraChasTemp_4.ResumeLayout(False)
        Me.fraChasTemp_4.PerformLayout()
        CType(Me.cwsSlotRiseActual_25, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cwsSlotRiseActual_24, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cwsSlotRiseActual_23, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cwsSlotRiseActual_22, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cwsSlotRiseActual_21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cwsSlotRiseActual_20, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cwsSlotRiseActual_19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cwsSlotRiseActual_18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cwsSlotRiseActual_17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cwsSlotRiseActual_16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cwsSlotRiseActual_15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cwsSlotRiseActual_14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cwsSlotRise_25, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Slide2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cwsSlotRiseActual_13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Slide1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cwsSlotRise_24, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cwsSlotRise_23, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cwsSlotRise_22, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cwsSlotRise_21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cwsSlotRise_20, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cwsSlotRise_19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cwsSlotRise_18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cwsSlotRise_17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cwsSlotRise_16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cwsSlotRise_15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cwsSlotRise_14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cwsSlotRise_13, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraChasTemp_5.ResumeLayout(False)
        Me.fraChasTemp_5.PerformLayout()
        Me.fraChasTemp_3.ResumeLayout(False)
        Me.fraChasTemp_3.PerformLayout()
        Me.tabSysmon_Page3.ResumeLayout(False)
        Me.fraChassisTest_2.ResumeLayout(False)
        Me.fraChassisTest_2.PerformLayout()
        CType(Me.cwbChasSelfTestIndicator_2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraFanSpeed_1.ResumeLayout(False)
        Me.fraFanSpeed_1.PerformLayout()
        Me.fraHeatingUnits_1.ResumeLayout(False)
        Me.fraHeatingUnits_1.PerformLayout()
        CType(Me.cwbHeaterIndicatorOO_3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cwbHeaterIndicatorOO_2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraAirflowSensors_1.ResumeLayout(False)
        Me.fraAirflowSensors_1.PerformLayout()
        CType(Me.cwbAirFlowIndicatorPF_6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cwbAirFlowIndicatorPF_5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cwbAirFlowIndicatorPF_4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraFanSpeed_0.ResumeLayout(False)
        Me.fraFanSpeed_0.PerformLayout()
        Me.fraAirflowSensors_0.ResumeLayout(False)
        Me.fraAirflowSensors_0.PerformLayout()
        CType(Me.cwbAirFlowIndicatorPF_3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cwbAirFlowIndicatorPF_2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cwbAirFlowIndicatorPF_1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraHeatingUnits_0.ResumeLayout(False)
        Me.fraHeatingUnits_0.PerformLayout()
        CType(Me.cwbHeaterIndicatorOO_1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cwbHeaterIndicatorOO_0, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraChassisTest_1.ResumeLayout(False)
        Me.fraChassisTest_1.PerformLayout()
        CType(Me.cwbChasSelfTestIndicator_1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabSysmon_Page4.ResumeLayout(False)
        Me.tabSysmon_Page4.PerformLayout()
        Me.SSFrame1.ResumeLayout(False)
        Me.SSFrame1.PerformLayout()
        Me.SSFrame2.ResumeLayout(False)
        Me.SSFrame2.PerformLayout()
        Me.fraChassisVoltage.ResumeLayout(False)
        Me.fraChassisVoltage.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraChassisCurrent.ResumeLayout(False)
        Me.fraChassisCurrent.PerformLayout()
        Me.tabSysmon_Page5.ResumeLayout(False)
        Me.fraUutVoltage.ResumeLayout(False)
        Me.fraUutVoltage.PerformLayout()
        Me.fraUutCurrent.ResumeLayout(False)
        Me.fraUutCurrent.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabSysmon_Page6.ResumeLayout(False)
        Me.fraPDU_5.ResumeLayout(False)
        Me.fraPDU_5.PerformLayout()
        CType(Me.cwb28VOk, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraFanSpeed_2.ResumeLayout(False)
        Me.fraMaintenance_0.ResumeLayout(False)
        Me.fraMaintenance_0.PerformLayout()
        Me.fraPDU_1.ResumeLayout(False)
        Me.fraPDU_1.PerformLayout()
        CType(Me.cwbRcvrSwitch, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraPDU_2.ResumeLayout(False)
        Me.fraPDU_2.PerformLayout()
        CType(Me.cwbProbe, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraPDU_4.ResumeLayout(False)
        Me.fraPDU_4.PerformLayout()
        Me.fraPDU_3.ResumeLayout(False)
        Me.fraPDU_3.PerformLayout()
        CType(Me.cwbInputPower, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cwbPowerConverters, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cwbHamPhase_3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cwbHamPhase_2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cwbHamPhase_1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraPDU_0.ResumeLayout(False)
        Me.fraPDU_0.PerformLayout()
        Me.tabSysmon_Page7.ResumeLayout(False)
        Me.tabSysmon_Page7.PerformLayout()
        Me.fraMaintenance_7.ResumeLayout(False)
        Me.fraMaintenance_4.ResumeLayout(False)
        Me.fraMaintenance_4.PerformLayout()
        CType(Me.Panel_2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Panel_4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraMaintenance_6.ResumeLayout(False)
        Me.fraMaintenance_5.ResumeLayout(False)
        Me.fraMaintenance_1.ResumeLayout(False)
        Me.fraMaintenance_1.PerformLayout()
        Me.fraMaintenance_2.ResumeLayout(False)
        Me.fraMaintenance_3.ResumeLayout(False)
        Me.fraMaintenance_3.PerformLayout()
        CType(Me.Panel_1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Panel_3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sbrUserInformation_Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

	'=========================================================
    '************************************************************
    '* Virtual Instrument Portable Equipment Repair/Test(VIPERT)*
    '*                                                          *
    '* Nomenclature   : SYSTEM: System Monitor Form             *
    '* Purpose        : Displays the main System Monitor Form.  *
    '*                  Primary and Secondary Chassis Data as   *
    '*                  PPU, FPU, and Environmental Data.       *
    '*                  Allows user control over the various    *
    '*                  Systems.                                *
    '************************************************************
    
    Public msgResponse As DialogResult

   
    Sub DispTxt(ByVal SetIndex As Short)

        Dim B1 As Short
        
        Dim B2 As Short
        Dim B3 As Short
        Dim Handle As Integer
        Dim Acount As Integer
        Dim Buffer As String
        Dim retCount As Integer
        
        Dim Pass As Short

        TextBox(SetIndex).Text = VB6.Format(Val(SetCur(SetIndex))/Val(SetUOM(SetIndex)), SetRes(SetIndex))

        'WriteMsg instrumentHandle&, SetCmd$(SetIndex%) & TextBox(SetIndex%)
        Select Case SetIndex
            Case FAN_PRIMARY:
                B1 = &H6F
                B2 = Val(TextBox(SetIndex).Text)*2
                Handle = ChassisControllerHandle1
            Case FAN_SECONDARY:
                B1 = &H6F
                B2 = Val(TextBox(SetIndex).Text)*2
                Handle = ChassisControllerHandle2
            Case OPT_TEMP_PRIMARY:
                B1 = &H4F
                B2 = (Val(TextBox(SetIndex).Text)+45)*2
                Handle = ChassisControllerHandle1
            Case OPT_TEMP_SECONDARY:
                B1 = &H4F
                B2 = (Val(TextBox(SetIndex).Text)+45)*2
                Handle = ChassisControllerHandle2
        End Select
        'SendScpiCommand SetCmd$(SetIndex%) & TextBox(SetIndex%)

        Acount = 3
        B3 = &HFF
        Buffer = Convert.ToString(Chr(B1)) & Convert.ToString(Chr(B2)) & Convert.ToString(Chr(B3))
        RetValue = viWrite(Handle, Buffer, Acount, retCount)

        If RetValue<>VI_SUCCESS Then
            Pass = False
        End If

    End Sub


    Public Sub chkEnableChassisOption_Click(ByVal Index As Integer, ByVal Value As Short)

        'Debounce Check Box Button to ensure that this event
        'is processed completely before other click actions on the
        'same controll can be processed.

        '---------------Version 1.8 Modification By DWH---------------
        'TDR98262
        Application.DoEvents()
        chkEnableChassisOption(Index).Enabled = False
        Me.lblmode(Index).Text = "Reset"
        If Value=True Then 'If Clicked
            Select Case Index
                Case 1:
                    'Primary Chassis Fan Speed Overide
                    'Enable Text Entry Box
                    Me.Panel(Index).Enabled = True
                    Me.TextBox(Index).Enabled = True
                    Me.SpinButton(Index).Enabled = True
                    'Set Box To Current Fan Speed
                    SetCur(Index) = PrimaryChassis.FanSpeed
                    Me.TextBox(Index).Text = SetCur(Index)
                    'Override Firmware Fan Control
                    Me.TextBox(Index).Focus()
                    Me.tabSysmon.Focus()
                    Me.TextBox(Index).Focus()
                Case 2:
                    'Secondary Chassis Fan Speed Overide
                    'Enable Text Entry Box
                    Me.Panel(Index).Enabled = True
                    Me.TextBox(Index).Enabled = True
                    Me.SpinButton(Index).Enabled = True
                    'Set Box To Current Fan Speed
                    SetCur(Index) = SecondaryChassis.FanSpeed
                    Me.TextBox(Index).Text = SetCur(Index)
                    'Override Firmware Fan Control
                    Me.TextBox(Index).Focus()
                    Me.tabSysmon.Focus()
                    Me.TextBox(Index).Focus()
                Case 3:
                    'Primary Chassis Optimum Temperature Setting
                    Me.Panel(Index).Enabled = True
                    Me.TextBox(Index).Enabled = True
                    Me.SpinButton(Index).Enabled = True
                    Me.TextBox(Index).Focus()
                Case 4:
                    'Secondary Chassis Optimum Temperature Setting
                    Me.Panel(Index).Enabled = True
                    Me.TextBox(Index).Enabled = True
                    Me.SpinButton(Index).Enabled = True
                    Me.TextBox(Index).Focus()
                Case 5:
                    'Primary Chassis Heater Control Override
                    HeaterAdjustInhibitFlag = True
                    'Set Box To Current Heating Unit State
                    CheckChassisStatusByte(PrimaryChassis.ChassisStatus)
                    If StatHeater1 Then
                        Me.chkHeater(0).Checked = True
                    Else
                        Me.chkHeater(0).Checked = False
                    End If
                    If StatHeater2 Then
                        Me.chkHeater(1).Checked = True
                    Else
                        Me.chkHeater(1).Checked = False
                    End If
                    'Override Firmware Heating Control
                    Me.chkHeater(0).Enabled = True
                    Me.chkHeater(1).Enabled = True
                    Me.chkHeater(0).Focus()
                Case 6:
                    'Secondary Chassis Heater Control Override
                    HeaterAdjustInhibitFlag = True
                    'Set Box To Current Heating Unit State
                    CheckChassisStatusByte(SecondaryChassis.ChassisStatus)
                    If StatHeater1 Then
                        Me.chkHeater(2).Checked = True
                    Else
                        Me.chkHeater(2).Checked = False
                    End If
                    If StatHeater2 Then
                        Me.chkHeater(3).Checked = True
                    Else
                        Me.chkHeater(3).Checked = False
                    End If
                    'Override Firmware Heating Control
                    Me.chkHeater(2).Enabled = True
                    Me.chkHeater(3).Enabled = True
                    Me.chkHeater(2).Focus()
            End Select
            Me.lblmode(Index).Text = "Manual"
        Else
            Me.lblmode(Index).Text = "Auto"
        End If

        chkEnableChassisOption(Index).Enabled = True
        '----------------End Version 1.8 Modification-----------------

    End Sub


    Public Sub chkEnableChassisOption_MouseMove(ByVal Index As Integer, ByVal Button As Short, ByVal Shift As Short, ByVal X As Single, ByVal Y As Single)

        Select Case Index
            Case 1:
                SendSysMonStatusBarMessage("Enable or Disable Manual Primary VXI Chassis Fan Speed Control")
            Case 2:
                SendSysMonStatusBarMessage("Enable or Disable Manual Secondary VXI Chassis Fan Speed Control")
            Case 3:
                SendSysMonStatusBarMessage("Enable or Disable Manual Primary VXI Chassis Optimum Temperature Setting")
            Case 4:
                SendSysMonStatusBarMessage("Enable or Disable Manual Secondary VXI Chassis Optimum Temperature Setting")
                '---------------Version 1.8 Modification By DWH---------------
            Case 5:
                SendSysMonStatusBarMessage("Enable or Disable Manual Primary VXI Chassis Heater Settings")
            Case 6:
                SendSysMonStatusBarMessage("Enable or Disable Manual Secondary VXI Chassis Heater Settings")
                '-----------------End Version 1.8 Modification----------------

        End Select


    End Sub


    Public Sub chkFpuNoFault_Click(ByVal Value As Short)

        Dim B1 As Short
        Dim B2 As Short
        Dim B3 As Short
        Dim Handle As Integer
        Dim Acount As Integer
        Dim Buffer As String
        Dim retCount As Integer
        
        Dim Pass As Short

        If Value Then
            B1 = &H8F
            B2 = &H2F
            Handle = GpibControllerHandle11
            FPU_IgnoreFaults = True
        Else
            B1 = &H8F
            B2 = &H8F
            Handle = GpibControllerHandle11
            FPU_IgnoreFaults = False
        End If
        Acount = 3
        B3 = &HFF
        Buffer = Convert.ToString(Chr(B1)) & Convert.ToString(Chr(B2)) & Convert.ToString(Chr(B3))
        RetValue = viWrite(Handle, Buffer, Acount, retCount)
        If RetValue <> VI_SUCCESS Then
            Pass = False
        End If

    End Sub


    Public Sub chkHeater_Click(ByVal Index As Integer, ByVal Value As Short)

        SetHeater(Index, Value)

    End Sub


    Public Sub chkHeater_MouseMove(ByVal Index As Integer, ByVal Button As Short, ByVal Shift As Short, ByVal X As Single, ByVal Y As Single)

        Select Case Index
            Case 0:
                SendSysMonStatusBarMessage("Manually Enable or Disable Primary VXI Chassis Heating Unit 1")
            Case 1:
                SendSysMonStatusBarMessage("Manually Enable or Disable Primary VXI Chassis Heating Unit 2")
            Case 2:
                SendSysMonStatusBarMessage("Manually Enable or Disable Secondary VXI Chassis Heating Unit 1")
            Case 3:
                SendSysMonStatusBarMessage("Manually Enable or Disable Secondary VXI Chassis Heating Unit 2")
        End Select
    End Sub


    Public Sub chkPpuNoFault_Click(ByVal Index As Integer, ByVal Value As Short)

        Dim B1 As Short
        Dim B2 As Short
        Dim B3 As Short
        Dim Handle As Integer
        Dim Acount As Integer
        Dim Buffer As String
        Dim retCount As Integer
        
        Dim Pass As Short

        If Value Then
            B1 = &H20
            B2 = &H80
            B3 = &HCF
            Handle = PpuControllerHandle(Index)
        Else
            B1 = &H20
            B2 = &H80
            B3 = &H8F
            Handle = PpuControllerHandle(Index)
        End If
        Acount = 3
        Buffer = Convert.ToString(Chr(B1)) & Convert.ToString(Chr(B2)) & Convert.ToString(Chr(B3))
        RetValue = viWrite(Handle, Buffer, Acount, retCount)
        If RetValue <> VI_SUCCESS Then
            Pass = False
        End If
    End Sub


    Public Sub chkPpuNoFault_MouseMove(ByVal Index As Integer, ByVal Button As Short, ByVal Shift As Short, ByVal X As Single, ByVal Y As Single)

        SendSysMonStatusBarMessage("Manually Enable or Disable Programmable Power Unit (PPU) Fault Ignore Mode for Supply#" & CStr(Index))

    End Sub


    Private Sub cmdAbout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAbout.Click

        If  Not (frmSplash.Visible) Then
            'Display About Box
            frmSplash.ShowDialog()
        End If

    End Sub


    Private Sub cmdAbout_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmdAbout.MouseMove
        Dim Button As Short = e.Button \ &H100000
        Dim Shift As Short = Control.ModifierKeys \ &H10000
        Dim X As Single = VB6.PixelsToTwipsX(e.X)
        Dim Y As Single = VB6.PixelsToTwipsY(e.Y)


        SendSysMonStatusBarMessage("Information About the System Monitor Application")

    End Sub


    Private Sub cmdCalChass_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCalChass.Click
        Dim Index As Short = cmdCalChass.GetIndex(sender)

        Dim MessString As String
        
        Dim Answer As DialogResult
        Dim Handle As Integer
        Dim B1 As Short
        Dim B2 As Short
        Dim B3 As Short
        Dim Acount As Integer
        Dim TemperatureLoop As Short
        Dim Buffer As String
        Dim retCount As Integer
        
        Dim Pass As Short
        Dim TemperatureAvg As Single
        Dim TempertureDeviation(13) As Single
        
        Dim SendTemperature As Short


        'Warn User
        MessString = "Caution: The temperature sensor compensation should only be performed while the chassis sensors "
        MessString &= "are at a uniform temperature (intake temperature is equal to exhaust temperature.)"
        MessString &= "  If the chassis unit has been recently operating, the calibration procedure "
        MessString &= "will cause the temperature sensors to report inaccurate results."
        MessString &= vbCrLf
        MessString &= vbCrLf
        MessString &= "Do you wish to continue with temperature sensor compensation?"
        Answer = MsgBox(MessString, MsgBoxStyle.Exclamation+MsgBoxStyle.OkCancel+MsgBoxStyle.DefaultButton2)
        If Answer<>DialogResult.OK Then
            Exit Sub
        End If

        SendSysMonStatusBarMessage("Beginning Temperature Sensor Compensation.")
        If Index=1 Then
            Handle = ChassisControllerHandle1
        Else
            Handle = ChassisControllerHandle2
        End If

        B1 = &H5F ' Program For Set Calibration Temperture
        B3 = &HFF ' XXXX XXXX Holding Space For system FIFO Memory
        '            Byte 2  A4 A2 A1 A0 S D2 D1 D0
        '            where    A=Address of sensor, S=Sign Bit, D=Offset in 0.5 degrees C
        Acount = 3 ' Three Byte Instructions

        'Set All Sensors To Offset Zero
        For TemperatureLoop = 0 To 14
            SendSysMonStatusBarMessage("Setting Temperature Sensor: " & Str(TemperatureLoop) & " to a Zero Offset.")
            B2 = (TemperatureLoop*16)
            Buffer = Convert.ToString(Chr(B1)) & Convert.ToString(Chr(B2)) & Convert.ToString(Chr(B3))
            Delay(1) 'The System Requires Time To Process the Command
            RetValue = viWrite(Handle, Buffer, Acount, retCount)

            If RetValue<>VI_SUCCESS Then
                Pass = False
            End If
        Next TemperatureLoop

        SendSysMonStatusBarMessage("Calculating Temperature Sensor Offsets.")

        TimerPollingEvent
        Delay(3) 'Get New Temp Data
        TimerPollingEvent
        Delay(3)
        TimerPollingEvent
        Delay(3)


        'Get Temp Average
        For TemperatureLoop = 0 To 12 'Find Exhaust Temps
            If Index=1 Then
                TemperatureAvg = TemperatureAvg + PrimaryChassis.Temperature!(TemperatureLoop)
            Else
                TemperatureAvg = TemperatureAvg + SecondaryChassis.Temperature!(TemperatureLoop)
            End If
        Next TemperatureLoop
        If Index=1 Then 'Add in Intake Temp
            TemperatureAvg += PrimaryChassis.IntakeTemperature
        Else
            TemperatureAvg += SecondaryChassis.IntakeTemperature
        End If
        TemperatureAvg /= 14 'Average Values

        TimerPollingEvent
        Delay(3) 'Get New Temp Data
        TimerPollingEvent
        Delay(3)
        TimerPollingEvent
        Delay(3)

        'Calculate Exhaust Temperture Sensor Correction
        For TemperatureLoop = 0 To 12
            If Index=1 Then
                TempertureDeviation(TemperatureLoop) = PrimaryChassis.Temperature!(TemperatureLoop) - TemperatureAvg
            Else
                TempertureDeviation(TemperatureLoop) = SecondaryChassis.Temperature!(TemperatureLoop) - TemperatureAvg
            End If
        Next TemperatureLoop

        'Calculate Intake Temperture Sensor Correction
        If Index=1 Then
            TempertureDeviation(13) = (PrimaryChassis.IntakeTemperature)-(TemperatureAvg)
        Else
            TempertureDeviation(13) = SecondaryChassis.IntakeTemperature-(TemperatureAvg)
        End If

        'Send Corrections To Temperature Sensors
        'B2% = A3 A2 A1 A0 S D2 D1 D0
        For TemperatureLoop = 0 To 13
            SendTemperature = CInt(TempertureDeviation(TemperatureLoop)*(-2)) 'Format Temperature to 0.5 Resolution
            If SendTemperature>7 Then SendTemperature = 7 'Beyond Maximum Compensation
            If SendTemperature<-7 Then SendTemperature = -7 'Beyond Minimum Compensation
            If SendTemperature<0 Then
                SendTemperature = (Math.Abs(SendTemperature) Or 8) 'Set Sign Bit
            Else
                SendTemperature = (Math.Abs(SendTemperature) And 7) 'Clear Sign Bit
            End If
            B2 = (TemperatureLoop*16)+SendTemperature
            Buffer = Convert.ToString(Chr(B1)) & Convert.ToString(Chr(B2)) & Convert.ToString(Chr(B3))
            SendSysMonStatusBarMessage("Setting Temperature Sensor: " & Str(TemperatureLoop) & " to Offset: " & Str(SendTemperature))
            Delay(0.5) 'The System Requires Time To Process the Command
            RetValue = viWrite(Handle, Buffer, Acount, retCount)
            SetTempSensorOffsets(Index, TemperatureLoop, Hex(B2)) 'Send Offsets To INI File
            If RetValue<>VI_SUCCESS Then
                Pass = False
            End If
        Next TemperatureLoop
        SendSysMonStatusBarMessage("Temperature Sensor Compensation Complete.")
    End Sub


    Private Sub cmdCalChass_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmdCalChass.MouseMove
        Dim Index As Short = cmdCalChass.GetIndex(sender)
        Dim Button As Short = e.Button \ &H100000
        Dim Shift As Short = Control.ModifierKeys \ &H10000
        Dim X As Single = VB6.PixelsToTwipsX(e.X)
        Dim Y As Single = VB6.PixelsToTwipsY(e.Y)

        cmdCalChass_MouseMove(Index, Button, Shift, X, Y)
    End Sub


    Public Sub cmdCalChass_MouseMove(ByVal Index As Integer, ByRef Button As Short, ByRef Shift As Short, ByRef X As Single, ByRef Y As Single)

        If Index = 1 Then
            SendSysMonStatusBarMessage("Perform Primary VXI Chassis Temperature Sensor Compensation procedure")
        Else
            SendSysMonStatusBarMessage("Perform Secondary VXI Chassis Temperature Sensor Compensation procedure")
        End If

    End Sub


    Private Sub cmdFixEthernet_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdFixEthernet.Click

        Dim result As Object, result1 As Boolean
        
        Dim MesBoxRes As DialogResult
        Dim CheckNameRes As String

        CEPFlag = False

        CheckNameRes = CheckEthernetNames

        If CheckNameRes = "GOOD" Then
            MesBoxRes = MsgBox("It does not appear that the Ethernet Ports need to be fixed.  Do you still wish to Fix/Reset the Ethernet Ports?", MsgBoxStyle.YesNo)
        ElseIf CheckNameRes = "CANCEL" Then
            MesBoxRes = MsgBox("Operation Cancelled", MsgBoxStyle.OkOnly)
            Exit Sub
        End If

        If MesBoxRes=DialogResult.No Then
            Exit Sub
        Else
            If (RenameEthernetPorts(1) = True) Then 'if cancel button is selected
                Return
            End If

            Do While CheckEthernetNames() = "NAMES"
                If (RenameEthernetPorts(2) = True) Then 'if cancel button is selected
                    Return
                Else
                    MsgBox("Please Verify the Ethernet Port Names are Configured as Described in the following Dialog Box.")
                End If
                ' 1 for find hardware
            Loop

            MsgBox("Ethernet names are correct.  Press [Ok] to set up the IP Address, do NOT press anything until confirmed.", MsgBoxStyle.OkOnly)

            Do
                SetIPAddress()
            Loop While CheckEthernetNames() = "ADDRESS"

            MsgBox("IP Address has been set.  Ethernet Fix complete.", MsgBoxStyle.OkOnly)
            KillappNT("cmd.exe")

            '    Else
            '    MsgBox("Fix Ethernet tool Failed.  Please do not press any buttons or run any other application while this is running.  Press [Ok] and try again.", MsgBoxStyle.OkOnly)
            'End If
        End If
    End Sub


    Private Sub cmdQuitSysMon_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdQuitSysMon.Click

        Me.Hide()

    End Sub


    Private Sub cmdQuitSysMon_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmdQuitSysMon.MouseMove
        Dim Button As Short = e.Button \ &H100000
        Dim Shift As Short = Control.ModifierKeys \ &H10000
        Dim X As Single = VB6.PixelsToTwipsX(e.X)
        Dim Y As Single = VB6.PixelsToTwipsY(e.Y)


        SendSysMonStatusBarMessage("Minimize System Monitoring Application")

    End Sub


    Private Sub cmdResetChassis_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmdResetChassis.MouseMove
        Dim Index As Short = cmdResetChassis.GetIndex(sender)
        Dim Button As Short = e.Button \ &H100000
        Dim Shift As Short = Control.ModifierKeys \ &H10000
        Dim X As Single = VB6.PixelsToTwipsX(e.X)
        Dim Y As Single = VB6.PixelsToTwipsY(e.Y)

        cmdResetChassis_MouseMove(Index, Button, Shift, X, Y)
    End Sub


    Public Sub cmdResetChassis_MouseMove(ByVal Index As Integer, ByRef Button As Short, ByRef Shift As Short, ByRef X As Single, ByRef Y As Single)

        If Index = 1 Then
            SendSysMonStatusBarMessage("Reset the Primary VXI Chassis monitoring circuitry")
        Else
            SendSysMonStatusBarMessage("Reset the Secondary VXI Chassis monitoring circuitry")
        End If

    End Sub


    Private Sub cmdResetPdu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdResetPdu.Click
        Dim Index As Short = cmdResetPdu.GetIndex(sender)

        ResetPdu()

    End Sub


    Private Sub cmdResetPdu_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmdResetPdu.MouseMove
        Dim Index As Short = cmdResetPdu.GetIndex(sender)
        Dim Button As Short = e.Button \ &H100000
        Dim Shift As Short = Control.ModifierKeys \ &H10000
        Dim X As Single = VB6.PixelsToTwipsX(e.X)
        Dim Y As Single = VB6.PixelsToTwipsY(e.Y)

        cmdResetPdu_MouseMove(Index, Button, Shift, X, Y)
    End Sub


    Public Sub cmdResetPdu_MouseMove(ByVal Index As Integer, ByRef Button As Short, ByRef Shift As Short, ByRef X As Single, ByRef Y As Single)

        SendSysMonStatusBarMessage("Reset the Power Distrubution Unit (PDU) and protection circuitry")

    End Sub


    Private Sub cmdResetThresholds_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdResetThresholds.Click
        Dim Index As Short = cmdResetThresholds.GetIndex(sender)

        Dim slot As Short

        'Init Thresholds
        If Index = 1 Then
            For slot = 0 To 12
                TempertureThreshold(slot) = CSng(FactoryDef(slot))
                'Me.cwsSlotRise(slot).Pointers(1).Value = FactoryDef(slot)
            Next slot
        Else
            For slot = 13 To 25
                TempertureThreshold(slot) = CSng(FactoryDef(slot))
                'Me.cwsSlotRise(slot).Pointers(1).Value = FactoryDef(slot)
            Next slot
        End If

    End Sub


    Private Sub cmdResetThresholds_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmdResetThresholds.MouseMove
        Dim Index As Short = cmdResetThresholds.GetIndex(sender)
        Dim Button As Short = e.Button \ &H100000
        Dim Shift As Short = Control.ModifierKeys \ &H10000
        Dim X As Single = VB6.PixelsToTwipsX(e.X)
        Dim Y As Single = VB6.PixelsToTwipsY(e.Y)

        cmdResetThresholds_MouseMove(Index, Button, Shift, X, Y)
    End Sub


    Public Sub cmdResetThresholds_MouseMove(ByVal Index As Integer, ByRef Button As Short, ByRef Shift As Short, ByRef X As Single, ByRef Y As Single)

        If Index = 1 Then
            SendSysMonStatusBarMessage("Reset Primary Chassis Temperature Rise Threshold Levels to Defaults")
        Else
            SendSysMonStatusBarMessage("Reset Secondary Chassis Temperature Rise Threshold Levels to Defaults")
        End If

    End Sub


    Private Sub cmdSaveThresholds_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSaveThresholds.Click
        Dim Index As Short = cmdSaveThresholds.GetIndex(sender)

        Dim slot As Short

        'Init Thresholds
        If Index = 1 Then
            For slot = 0 To 12
                SetTemperatureThresholds(slot, CSng(TempertureThreshold(slot)))
                UserDef(slot) = Str(TempertureThreshold(slot))
            Next slot
        End If
        If Index = 2 Then
            For slot = 13 To 25
                SetTemperatureThresholds(slot, CSng(TempertureThreshold(slot)))
                UserDef(slot) = Str(TempertureThreshold(slot))
            Next slot
        End If

    End Sub


    Private Sub cmdSaveThresholds_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmdSaveThresholds.MouseMove
        Dim Index As Short = cmdSaveThresholds.GetIndex(sender)
        Dim Button As Short = e.Button \ &H100000
        Dim Shift As Short = Control.ModifierKeys \ &H10000
        Dim X As Single = VB6.PixelsToTwipsX(e.X)
        Dim Y As Single = VB6.PixelsToTwipsY(e.Y)

        cmdSaveThresholds_MouseMove(Index, Button, Shift, X, Y)
    End Sub


    Public Sub cmdSaveThresholds_MouseMove(ByVal Index As Integer, ByRef Button As Short, ByRef Shift As Short, ByRef X As Single, ByRef Y As Single)

        If Index = 1 Then
            SendSysMonStatusBarMessage("Save Primary Chassis Temperature Rise Threshold Levels")
        Else
            SendSysMonStatusBarMessage("Save Secondary Chassis Temperature Rise Threshold Levels")
        End If
    End Sub


    Private Sub cmdShutDown_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdShutDown.Click

        
        Dim Answer As DialogResult

        Answer = MsgBox("Shut Down ATS System?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "ATS System Shut Down")
        If Answer = DialogResult.Yes Then
            SHUTDOWN_FROM_SYSMON = True
            ShutDownSysmon()
        End If

    End Sub


    Private Sub cmdShutDown_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmdShutDown.MouseMove
        Dim Button As Short = e.Button \ &H100000
        Dim Shift As Short = Control.ModifierKeys \ &H10000
        Dim X As Single = VB6.PixelsToTwipsX(e.X)
        Dim Y As Single = VB6.PixelsToTwipsY(e.Y)


        SendSysMonStatusBarMessage("Perform an orderly shut down of the Automated Test System")

    End Sub


    Public Sub cwsSlotRise_MouseMove(ByVal Index As Integer, ByVal Button As Short, ByVal Shift As Short, ByVal X As Single, ByVal Y As Single)

        SendSysMonStatusBarMessage("Temperature Rise Threshold Value: " & CStr(TempertureThreshold(Index)))

    End Sub


    Public Sub cwsSlotRise_PointerValueChanged(ByVal Index As Integer, ByVal Pointer As Integer, ByRef Value As Short)

        If Pointer = 1 Then
            If Value = 0 Then Value = 1
            TempertureThreshold(Index) = CStr(Value)
        End If

    End Sub


    Private Sub frmSysMon_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.lblSysPower(0).Text = CStr(IC_POWER) & " W"
    End Sub


    Private Sub frmSysMon_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseMove
        Dim Button As Short = e.Button \ &H100000
        Dim Shift As Short = Control.ModifierKeys \ &H10000
        Dim X As Single = VB6.PixelsToTwipsX(e.X)
        Dim Y As Single = VB6.PixelsToTwipsY(e.Y)


        SendSysMonStatusBarMessage("Automated Test System (ATS) System Monitor")

    End Sub


    Private Sub Form_Unload(ByRef Cancel As Short)

        Me.Hide()
        Cancel = True

    End Sub


    Private Sub frmSysMon_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Dim Cancel As Short = 0
        Form_Unload(Cancel)
        If Cancel <> 0 Then e.Cancel = True
    End Sub


    Public Sub optFpuState_Click(ByVal Index As Integer, ByVal Value As Short)

        If (Index = 0) Then 'ON Button, Off button already pressed
            If Value Then
                'Aquire Handles
                If msgResponse <> DialogResult.Cancel Then
                    MessageBox.Show("Sysmon Will Now Restart", "ATS Notification")
                    Dim newSysmonProcess As New Process()
                    newSysmonProcess.StartInfo.FileName = "C:\Program Files (x86)\ATS\Sysmon.exe"
                    newSysmonProcess.StartInfo.Arguments = "-Restart"
                    newSysmonProcess.Start()
                    'hide first sysmon tray icon
                    frmToolbar.trayIcon.Visible = False
                    'write shutdown event to fhdb
                    FHDB_Shutdown()
                    Environment.Exit(0)
                    'ShortStart()
                    chkFpuNoFault.Checked = False
                    FPU_IgnoreFaults = False
                End If
            End If
        Else 'OFF Button
            If Value Then
                If FPU_IgnoreFaults = False Then
                    msgResponse = MsgBox("It is recommended that you ignore FPU faults when turning off the FPU." & vbCrLf & vbCrLf & "Would you like to ignore FPU faults?", MsgBoxStyle.YesNoCancel)
                    If msgResponse = DialogResult.Yes Then
                        Me.chkFpuNoFault.Checked = True
                        chkFpuNoFault_Click(-1)
                    ElseIf msgResponse = DialogResult.No Then
                        ' do nothing
                    ElseIf msgResponse = DialogResult.Cancel Then
                        optFpuState(0).Checked = True
                        optFpuState(1).Checked = False
                        Exit Sub
                    End If
                Else
                    msgResponse = MsgBox("Are you sure you want to turn off FPUs?", MsgBoxStyle.YesNo)
                    If msgResponse = DialogResult.No Then
                        msgResponse = DialogResult.Cancel
                        optFpuState(0).Checked = True
                        optFpuState(1).Checked = False
                        chkFpuNoFault.Checked = False
                        FPU_IgnoreFaults = False
                        msgResponse = DialogResult.No
                        Exit Sub
                    ElseIf msgResponse = DialogResult.Yes Then
                        ' do nothing
                    End If

                End If

                'Shut Down Chassis Power
                SetFpu(False)
                'Release Handles
                'ReleaseHandles

            End If
        End If

    End Sub


    Private Sub SpinButton_SpinDown(ByVal Index As Short)

        Dim OldVal As Double
        Dim NewVal As Double

        OldVal = Val(SetCur(Index))
        NewVal = Val(SetCur(Index)) - Val(SetInc(Index))
        If NewVal >= Val(SetMin(Index)) Then
            SetCur(Index) = Str(NewVal)
        Else
            SetCur(Index) = SetMin(Index)
        End If
        DispTxt(Index)
        Application.DoEvents()

    End Sub


    Private Sub SpinButton_SpinUp(ByVal Index As Short)

        Dim OldVal As Double
        Dim NewVal As Double

        OldVal = Val(SetCur(Index))
        NewVal = Val(SetCur(Index)) + Val(SetInc(Index))
        If NewVal <= Val(SetMax(Index)) Then
            SetCur(Index) = Str(NewVal)
        Else
            SetCur(Index) = SetMax(Index)
        End If
        DispTxt(Index)
        Application.DoEvents()

    End Sub


    Private Sub tabSysmon_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles tabSysmon.MouseMove
        Dim Button As Short = e.Button \ &H100000
        Dim Shift As Short = Control.ModifierKeys \ &H10000
        Dim X As Single = VB6.PixelsToTwipsX(e.X)
        Dim Y As Single = VB6.PixelsToTwipsY(e.Y)


        SendSysMonStatusBarMessage("Automated Test System (ATS) System Monitor")

    End Sub


    Private Sub TextBox_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox.Click
        Dim Index As Short = TextBox.GetIndex(sender)
        TextBox_Click(Index)
    End Sub


    Public Sub TextBox_Click(ByVal Index As Integer)

        TextBox(Index).SelectionStart = 0
        TextBox(Index).SelectionLength = Len(TextBox(Index).Text)

    End Sub


    Private Sub TextBox_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox.Enter
        Dim Index As Short = TextBox.GetIndex(sender)

        RangeDisplay = True
        SendStatusBarMessage(SetRngMsg(Index))

    End Sub


    Private Sub TextBox_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox.KeyPress
        Dim Index As Short = TextBox.GetIndex(sender)
        Dim KeyAscii As Short = Asc(e.KeyChar)

        TextBox_KeyPress(Index, KeyAscii)

        e.KeyChar = Chr(KeyAscii)
    End Sub


    Public Sub TextBox_KeyPress(ByVal Index As Integer, ByRef KeyAscii As Short)

        If KeyAscii = 13 Or KeyAscii = 9 Then
            KeyAscii = 0
            tabSysmon.Focus()
        ElseIf KeyAscii = 27 Then
            KeyAscii = 0
            TextBox(Index).Text = Str(Val(SetCur(Index)) / Val(SetUOM(Index)))
            tabSysmon.Focus()
        End If

    End Sub


    Private Sub TextBox_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox.Leave
        Dim Index As Short = TextBox.GetIndex(sender)

        Dim First3 As String
        Dim NewVal As Double

        First3 = UCase(Strings.Left(TextBox(Index).Text, 3))
        Select Case First3
            Case "MIN"
                NewVal = Val(SetMin(Index))
            Case "MAX"
                NewVal = Val(SetMax(Index))
            Case "DEF"
                NewVal = Val(SetDef(Index))

            Case Else
                If Not IsNumeric(TextBox(Index).Text) Then
                    MsgBox(SetRngMsg(Index), MsgBoxStyle.Exclamation, "Invalid Value")
                    TextBox(Index).Focus()
                    TextBox_Click(Index)
                    Exit Sub
                End If
                NewVal = Val(TextBox(Index).Text) * Val(SetUOM(Index))
        End Select

        If NewVal < Val(SetMin(Index)) Then
            MsgBox(SetRngMsg(Index), MsgBoxStyle.Exclamation, "Invalid Value")
            TextBox(Index).Focus()
            TextBox_Click(Index)
        ElseIf NewVal > Val(SetMax(Index)) Then
            MsgBox(SetRngMsg(Index), MsgBoxStyle.Exclamation, "Invalid Value")
            TextBox(Index).Focus()
            TextBox_Click(Index)
        Else
            SetCur(Index) = Str(NewVal)
            DispTxt(Index)
            RangeDisplay = False
        End If
        SendStatusBarMessage("")

    End Sub


    Public Sub tmrDataPoll_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmrDataPoll.Tick

        Dim KeyInfo As String = GatherIniFileInformation("System Startup", "SYSTEM_SURVEY", "")
        'only poll if ctest is not currently running
        If (KeyInfo.Contains("RUNNING")) Then
            chkDataInterval.Enabled = False
        Else
            TimerPollingEvent()
            chkDataInterval.Enabled = True
        End If

    End Sub


    Private Sub txtFanSpeedValue_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFanSpeedValue.Enter
        Dim Index As Short = txtFanSpeedValue.GetIndex(sender)

        tabSysmon.Focus()

    End Sub


    Private Sub tmrUpdateStop_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmrUpdateStop.Tick
        'Timer event added to support the FHDB
        'Checks for Import or Expot operations due, accounting for Defered operation also.
        'Updates the Time Running to us for evaluating a Proper Shutdown
        'Timer Interval is set for 60 Secs.
        'ECP-VIPERT-023   Dave Joiner   04/12/2001

        CheckImportExport() 'Check for Import/Export operation Due
        UpdateIniFile("FHDB", "RUN_TIME", "") 'Update Running Time

    End Sub


    Private Sub ETITimer_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ETITimer.Tick
        '*************************************************************************
        '*  This is a Timer to record the time the system runs on 3phase, 1phase *
        '*  or DC power.  It will record the length of time the system runs in   *
        '*  each phase and save it to a log file.                                *
        '*************************************************************************

        'variables for total time recorded and math calculations
        
        
        
        Dim t3P As String, m3 As Short, h3 As Short, s3 As Integer
        
        
        
        Dim t1P As String, m1 As Short, h1 As Short, s1 As Integer
        
        
        
        Dim tDC As String, mdc As Short, hdc As Short, sdc As Integer
        
        
        
        Dim tTotal As String, mtotal As Short, htotal As Short, stotal As Integer
        Dim i As Short


        'Static timerCount As Integer

        Dim LogPath As String
        Dim FileName As String
        Dim FullPath As String

        Dim File As Short
        Dim errorWrite As Integer
        Dim Answer As DialogResult


        'for file creation
        
        
        'Dim filesys As Scripting.FileSystemObject, filetxt As Scripting.TextStream
        Const ForReading As Short = 1, ForWriting As Short = 2

        Try ' On Error GoTo ETIErrorHandler


            FileName = "ETI_Timer_Log.txt"
            LogPath = GatherIniFileInformation("File Locations", "ETI_Timer", "c:\Program Files (x86)\ATS\log\")
            FullPath = LogPath & FileName


            'If File is found, load previous values
            'Else file doesn't exist: reset ETI or shutdown
            If System.IO.File.Exists(FullPath) Then
                t1P = CLng(GatherETIFileInfo("ETI", "1pac", CStr(PreT1P)))
                t3P = CLng(GatherETIFileInfo("ETI", "3pac", CStr(PreT3P)))
                tDC = CLng(GatherETIFileInfo("ETI", "dc", CStr(PreTDC)))
                tTotal = CLng(GatherETIFileInfo("ETI", "total", CStr(PreTTotal)))
            Else

                'Kill FullPath


                System.IO.File.Create(FullPath)

                Using streamWriter As New System.IO.StreamWriter(FullPath)
                    streamWriter.WriteLine("[ETI]")
                    streamWriter.WriteLine("1pac=0")
                    streamWriter.WriteLine("3pac=0")
                    streamWriter.WriteLine("dc=0")
                    streamWriter.WriteLine("total=0")
                    streamWriter.Close()
                End Using

                'filesys = CreateObject("Scripting.FileSystemObject")
                'filesys.CreateTextFile(FullPath)
                'filetxt = filesys.OpenTextFile(FullPath, ForWriting)
                'filetxt.Write("[ETI]" & vbCr & vbLf)
                'filetxt.Write("1pac=0" & vbCr & vbLf)
                'filetxt.Write("3pac=0" & vbCr & vbLf)
                'filetxt.Write("dc=0" & vbCr & vbLf)
                'filetxt.Write("total=0" & vbCr & vbLf)
                'filetxt.Close()

                t1P = PreT1P
                t3P = PreT3P
                tDC = PreTDC
                tTotal = PreTTotal

            End If

            'DC mode
            If (PowerStatusDc <> 0) Then
                tDC += 1
            End If
            'Single Phase AC mode
            If (PowerStatusAc <> 0 And PowerStatusSingle = 0) Then
                t1P += 1
            End If
            'Three Phase AC mode
            If (PowerStatusAc <> 0 And PowerStatusSingle <> 0) Then
                t3P += 1
            End If

            'format hour:min:sec for 3 phase
            s3 = t3P Mod 3600
            m3 = s3 \ 60
            h3 = t3P \ 3600
            s3 = s3 Mod 60
            'format hour:min:sec for 1 phase
            s1 = t1P Mod 3600
            m1 = s1 \ 60
            h1 = t1P \ 3600
            s1 = s1 Mod 60
            'format hour:min:sec for DC
            sdc = tDC Mod 3600
            mdc = sdc \ 60
            hdc = tDC \ 3600
            sdc = sdc Mod 60

            'print times in window
            lblmode(7).Text = CStr(h3) & ":" & VB6.Format(m3, "00") & ":" & VB6.Format(s3, "00")
            lblmode(8).Text = CStr(h1) & ":" & VB6.Format(m1, "00") & ":" & VB6.Format(s1, "00")
            lblmode(9).Text = CStr(hdc) & ":" & VB6.Format(mdc, "00") & ":" & VB6.Format(sdc, "00")

            'calculate total
            tTotal = Convert.ToInt32(tDC) + Convert.ToInt32(t1P) + Convert.ToInt32(t3P)
            'format hour:min:sec for Total
            stotal = tTotal Mod 3600
            mtotal = stotal \ 60
            htotal = tTotal \ 3600
            stotal = stotal Mod 60
            'print total
            lblmode(0).Text = CStr(htotal) & ":" & VB6.Format(mtotal, "00") & ":" & VB6.Format(stotal, "00")

            'save data to text file
            PreT1P = t1P
            PreT3P = t3P
            PreTDC = tDC
            PreTTotal = tTotal
            errorWrite = WritePrivateProfileString("ETI", "1pac", CStr(t1P), FullPath)
            errorWrite = WritePrivateProfileString("ETI", "3pac", CStr(t3P), FullPath)
            errorWrite = WritePrivateProfileString("ETI", "dc", CStr(tDC), FullPath)
            errorWrite = WritePrivateProfileString("ETI", "total", CStr(tTotal), FullPath)

            'Application.DoEvents()

        Catch   ' ETIErrorHandler:
            'let program continue.

        End Try
    End Sub


    Function GatherETIFileInfo(ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpDefault As String) As String
        GatherETIFileInfo = ""

        
        Dim lpReturnedString As New StringBuilder(255) 'Return Buffer
        Dim nSize As Integer 'Return Buffer Size
        Dim lpFileName As String 'INI File Key Name "Key=?"
        Dim ReturnValue As Integer 'Return Value Buffer
        Dim FileNameInfo As String 'Formatted Return String

        'Find Windows Directory
        lpFileName = GatherIniFileInformation("File Locations", "ETI_Timer", "")
        lpFileName &= "ETI_Timer_Log.txt"
        nSize = 255
        FileNameInfo = ""
        'Find File Locations
        ReturnValue = GetPrivateProfileString(lpApplicationName, lpKeyName, lpDefault, lpReturnedString, nSize, lpFileName)
        FileNameInfo = Trim(lpReturnedString.ToString())
        'FileNameInfo = Mid(FileNameInfo, 1, Len(FileNameInfo) - 1)
        'If File Locations Missing, then assign default value
        If FileNameInfo = "" Then
            FileNameInfo = Trim(lpDefault)
        End If
        'Return Information In INI File
        GatherETIFileInfo = FileNameInfo
    End Function


    Private Sub cmdSaveThresholds_1_Click(sender As Object, e As EventArgs) Handles cmdSaveThresholds_1.Click
        For slot% = 0 To 12
            SetTemperatureThresholds(slot%, CSng(TempertureThreshold(slot%)))
            UserDef$(slot%) = Str(TempertureThreshold(slot%))
        Next slot%
    End Sub


    Private Sub cmdResetThresholds_1_Click(sender As Object, e As EventArgs) Handles cmdResetThresholds_1.Click
        'Init Thresholds
        For slot% = 0 To 12
            TempertureThreshold!(slot%) = CSng(FactoryDef$(slot%))
            Me.cwsSlotRise(slot%).Value = FactoryDef$(slot%)
        Next slot%
    End Sub


    Private Sub cmdSaveThresholds_2_Click(sender As Object, e As EventArgs) Handles cmdSaveThresholds_2.Click
        For slot% = 13 To 25
            SetTemperatureThresholds(slot%, CSng(TempertureThreshold(slot%)))
            UserDef$(slot%) = Str(TempertureThreshold(slot%))
        Next slot%
    End Sub


    Private Sub cmdResetThresholds_2_Click(sender As Object, e As EventArgs) Handles cmdResetThresholds_2.Click
        For slot% = 13 To 25
            TempertureThreshold!(slot%) = CSng(FactoryDef$(slot%))
            Me.cwsSlotRise(slot%).Value = FactoryDef$(slot%)
        Next slot%
    End Sub


    Private Sub chkDataInterval_CheckedChanged(sender As Object, e As EventArgs)

    End Sub


    Private Sub chkEnableChassisOption_3_CheckedChanged(sender As Object, e As EventArgs) Handles chkEnableChassisOption_3.CheckedChanged
        Dim checkBox As CheckBox = sender
        If (checkBox.Checked = True) Then
            Panel_3.Enabled = True
        Else
            Panel_3.Enabled = False
        End If
    End Sub


    Private Sub chkEnableChassisOption_1_CheckedChanged(sender As Object, e As EventArgs) Handles chkEnableChassisOption_1.CheckedChanged
        If (Me.chkEnableChassisOption_1.Checked = True) Then
            Panel_1.Enabled = True
        Else
            Panel_1.Enabled = False
            Panel_1.Value = 100
            SetFanSpeed(1, CInt(Panel_1.Value))
        End If
    End Sub


    Private Sub chkEnableChassisOption_5_CheckedChanged(sender As Object, e As EventArgs) Handles chkEnableChassisOption_5.CheckedChanged
        Dim checkBox As CheckBox = sender
        If checkBox.Checked = True Then
            chkHeater_0.Enabled = True
            chkHeater_1.Enabled = True
        Else
            chkHeater_0.Enabled = False
            chkHeater_1.Enabled = False
            chkHeater_0.Checked = False
            chkHeater_1.Checked = False
            Me.Cursor = Cursors.WaitCursor
            ResetChassis(1)
            Me.Cursor = Cursors.Default
        End If
    End Sub


    Private Sub chkEnableChassisOption_4_CheckedChanged(sender As Object, e As EventArgs) Handles chkEnableChassisOption_4.CheckedChanged
        Dim checkBox As CheckBox = sender
        If (checkBox.Checked = True) Then
            Panel_4.Enabled = True
        Else
            Panel_4.Enabled = False
        End If

    End Sub


    Private Sub chkEnableChassisOption_2_CheckedChanged(sender As Object, e As EventArgs) Handles chkEnableChassisOption_2.CheckedChanged
        If (Me.chkEnableChassisOption_2.Checked = True) Then
            Panel_2.Enabled = True
        Else
            Panel_2.Enabled = False
            Panel_2.Value = 100
            SetFanSpeed(2, Panel_2.Value)
        End If
    End Sub


    Private Sub chkEnableChassisOption_6_CheckedChanged(sender As Object, e As EventArgs) Handles chkEnableChassisOption_6.CheckedChanged
        Dim checkBox As CheckBox = sender
        If checkBox.Checked = True Then
            chkHeater_2.Enabled = True
            chkHeater_3.Enabled = True
        Else
            chkHeater_2.Enabled = False
            chkHeater_3.Enabled = False
            chkHeater_2.Checked = False
            chkHeater_3.Checked = False
            Me.Cursor = Cursors.WaitCursor
            ResetChassis(2)
            Me.Cursor = Cursors.Default
        End If
    End Sub


    Private Sub cmdSetThreshold(sender As Object, e As AfterChangeNumericValueEventArgs) Handles cwsSlotRise_0.AfterChangeValue, cwsSlotRise_1.AfterChangeValue, cwsSlotRise_2.AfterChangeValue, cwsSlotRise_3.AfterChangeValue, cwsSlotRise_4.AfterChangeValue, cwsSlotRise_5.AfterChangeValue, cwsSlotRise_6.AfterChangeValue, cwsSlotRise_7.AfterChangeValue, cwsSlotRise_8.AfterChangeValue, cwsSlotRise_9.AfterChangeValue, cwsSlotRise_10.AfterChangeValue, cwsSlotRise_11.AfterChangeValue, cwsSlotRise_12.AfterChangeValue, cwsSlotRise_13.AfterChangeValue, cwsSlotRise_14.AfterChangeValue, cwsSlotRise_15.AfterChangeValue, cwsSlotRise_16.AfterChangeValue, cwsSlotRise_17.AfterChangeValue, cwsSlotRise_18.AfterChangeValue, cwsSlotRise_19.AfterChangeValue, cwsSlotRise_20.AfterChangeValue, cwsSlotRise_21.AfterChangeValue, cwsSlotRise_22.AfterChangeValue, cwsSlotRise_23.AfterChangeValue, cwsSlotRise_24.AfterChangeValue, cwsSlotRise_25.AfterChangeValue
        Dim slotRiseControl As New NationalInstruments.UI.WindowsForms.Slide
        Dim slot As Integer = 0

        slotRiseControl = sender
        slot = slotRiseControl.Tag
        TempertureThreshold(slot) = slotRiseControl.Value
    End Sub


    Private Sub cmdResetPdu_0_Click(sender As Object, e As EventArgs) Handles cmdResetPdu_0.Click
        ResetPdu()
        MsgBox("PDU has been reset.", MsgBoxStyle.OkOnly)
    End Sub


    Private Sub chkDataInterval_Click(sender As Object, e As EventArgs) Handles chkDataInterval.Click
        Dim dataIntervalCheck As CheckBox = sender
        If dataIntervalCheck.Checked = True Then
            Me.tmrDataPoll.Enabled = True
        Else
            Me.tmrDataPoll.Enabled = False
        End If
    End Sub


    Private Sub chkHeater_0_CheckedChanged(sender As Object, e As EventArgs) Handles chkHeater_0.CheckedChanged
        Dim checkBox As CheckBox = sender
        If checkBox.Checked = True Then
            SetHeater(0, True)
        Else
            SetHeater(0, False)
        End If
    End Sub


    Private Sub chkHeater_1_CheckedChanged(sender As Object, e As EventArgs) Handles chkHeater_1.CheckedChanged
        Dim checkBox As CheckBox = sender
        If checkBox.Checked = True Then
            SetHeater(1, True)
        Else
            SetHeater(1, False)
        End If
    End Sub


    Private Sub chkHeater_2_CheckedChanged(sender As Object, e As EventArgs) Handles chkHeater_2.CheckedChanged
        Dim checkBox As CheckBox = sender
        If checkBox.Checked = True Then
            SetHeater(2, True)
        Else
            SetHeater(2, False)
        End If
    End Sub


    Private Sub chkHeater_3_CheckedChanged(sender As Object, e As EventArgs) Handles chkHeater_3.CheckedChanged
        Dim checkBox As CheckBox = sender
        If checkBox.Checked = True Then
            SetHeater(3, True)
        Else
            SetHeater(3, False)
        End If
    End Sub


    Private Sub optFpuState_0_Click(sender As Object, e As EventArgs) Handles optFpuState_0.Click
        optFpuState_Click(0, True)
    End Sub


    Private Sub optFpuState_1_Click(sender As Object, e As EventArgs) Handles optFpuState_1.Click
        optFpuState_Click(1, True)
    End Sub


    Private Sub frmSysMon_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        e.Cancel() = True
        Me.Hide()
    End Sub


    Private Sub Panel_1_ValueChanged(sender As Object, e As EventArgs) Handles Panel_1.ValueChanged
        SetFanSpeed(1, Panel_1.Value)
    End Sub


    Private Sub Panel_2_ValueChanged(sender As Object, e As EventArgs) Handles Panel_2.ValueChanged
        SetFanSpeed(2, Panel_2.Value)
    End Sub


    Private Sub cmdResetChassis_1_Click(sender As Object, e As EventArgs) Handles cmdResetChassis_1.Click
        Me.Cursor = Cursors.WaitCursor
        ResetChassis(1)
        Me.Cursor = Cursors.Default
        Me.chkHeater_0.Enabled = False
        Me.chkHeater_1.Enabled = False
        Me.chkHeater_0.Checked = False
        Me.chkHeater_1.Checked = False
        Me.chkEnableChassisOption_5.Checked = False
        Me.chkEnableChassisOption_1.Checked = False
        Me.Panel_1.Enabled = False
        Me.Panel_1.Value = 100
        Me.chkEnableChassisOption_3.Checked = False
        Me.Panel_3.Enabled = False
        Me.Panel_3.Value = 35
        MsgBox("Primary Chassis has been reset.", MsgBoxStyle.OkOnly)
    End Sub


    Private Sub cmdResetChassis_2_Click(sender As Object, e As EventArgs) Handles cmdResetChassis_2.Click
        Me.Cursor = Cursors.WaitCursor
        ResetChassis(2)
        Me.Cursor = Cursors.Default
        Me.chkHeater_2.Enabled = False
        Me.chkHeater_3.Enabled = False
        Me.chkHeater_2.Checked = False
        Me.chkHeater_3.Checked = False
        Me.chkEnableChassisOption_6.Checked = False
        Me.chkEnableChassisOption_2.Checked = False
        Me.Panel_2.Enabled = False
        Me.Panel_2.Value = 100
        Me.chkEnableChassisOption_4.Checked = False
        Me.Panel_4.Enabled = False
        Me.Panel_4.Value = 35
        MsgBox("Secondary Chassis has been reset.", MsgBoxStyle.OkOnly)
    End Sub

    Private Sub ppuSupplyFaultEventHandler(sender As Object, e As EventArgs) Handles chkPpuNoFault_1.CheckedChanged, chkPpuNoFault_2.CheckedChanged,
        chkPpuNoFault_3.CheckedChanged, chkPpuNoFault_4.CheckedChanged, chkPpuNoFault_5.CheckedChanged, chkPpuNoFault_6.CheckedChanged,
        chkPpuNoFault_7.CheckedChanged, chkPpuNoFault_8.CheckedChanged, chkPpuNoFault_9.CheckedChanged, chkPpuNoFault_10.CheckedChanged

        Dim ppuFaultCheckBox As CheckBox = sender

        chkPpuNoFault_Click(ppuFaultCheckBox.Tag, ppuFaultCheckBox.Checked)

    End Sub


    Private Sub frmSysMon_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Me.BringToFront()
    End Sub
End Class