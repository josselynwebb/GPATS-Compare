
Imports System
Imports System.Windows.Forms
Imports Microsoft.VisualBasic

Public Class frmBus_IO
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
    'Friend WithEvents MSComm1 As System.Windows.Forms.Application
    'Friend WithEvents MSComm2 As System.Windows.Forms.Application
    Friend WithEvents cmdReset As System.Windows.Forms.Button
    Friend WithEvents cmdQuit As System.Windows.Forms.Button
    Friend WithEvents cmdHelp As System.Windows.Forms.Button
    Friend WithEvents CommonDialog1 As System.Windows.Forms.PrintDialog
    Friend WithEvents sbrUserInformation As System.Windows.Forms.StatusBar
    Friend WithEvents sbrUserInformation_Panel1 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents sbrUserInformation_Panel2 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents tabOptions As System.Windows.Forms.TabControl
    Friend WithEvents tabOptions_Page1 As System.Windows.Forms.TabPage
    Friend WithEvents lblReadTimeCom1 As System.Windows.Forms.Label
    Friend WithEvents txtReadTimeCom1 As System.Windows.Forms.TextBox
    Friend WithEvents frameReceiveCom1 As System.Windows.Forms.GroupBox
    Friend WithEvents optDataReceiveCom1 As System.Windows.Forms.RadioButton
    Friend WithEvents optXMLReceiveCom1 As System.Windows.Forms.RadioButton
    Friend WithEvents frameSendCom1 As System.Windows.Forms.GroupBox
    Friend WithEvents optXMLSendCom1 As System.Windows.Forms.RadioButton
    Friend WithEvents optDataSendCom1 As System.Windows.Forms.RadioButton
    Friend WithEvents cmdSendSettingsCom1 As System.Windows.Forms.Button
    Friend WithEvents cmdLoadCom1 As System.Windows.Forms.Button
    Friend WithEvents txtDataReceivedCom1 As System.Windows.Forms.TextBox
    Friend WithEvents cmdDataReceiveCom1 As System.Windows.Forms.Button
    Friend WithEvents cmdSendDataCom1 As System.Windows.Forms.Button
    Friend WithEvents txtDataSentCom1 As System.Windows.Forms.TextBox
    Friend WithEvents FrameCom1PortSettings As System.Windows.Forms.GroupBox
    Friend WithEvents cmbParityCom1 As System.Windows.Forms.ComboBox
    Friend WithEvents cmbStopBitsCom1 As System.Windows.Forms.ComboBox
    Friend WithEvents cmbBitRateCom1 As System.Windows.Forms.ComboBox
    Friend WithEvents txtWordLengthCom1 As System.Windows.Forms.TextBox
    Friend WithEvents lblWordLengthUnitsCom1 As System.Windows.Forms.Label
    Friend WithEvents lblParityCom1 As System.Windows.Forms.Label
    Friend WithEvents lblStopBitsCom1 As System.Windows.Forms.Label
    Friend WithEvents lblBitRateCom1 As System.Windows.Forms.Label
    Friend WithEvents lblBitRateUnitsCom1 As System.Windows.Forms.Label
    Friend WithEvents lblStopBitsUnitsCom1 As System.Windows.Forms.Label
    Friend WithEvents lblWordLengthCom1 As System.Windows.Forms.Label
    Friend WithEvents tabOptions_Page2 As System.Windows.Forms.TabPage
    Friend WithEvents lblReadTimeCom2 As System.Windows.Forms.Label
    Friend WithEvents txtReadTimeCom2 As System.Windows.Forms.TextBox
    Friend WithEvents frameReceiveCom2 As System.Windows.Forms.GroupBox
    Friend WithEvents optXMLReceiveCom2 As System.Windows.Forms.RadioButton
    Friend WithEvents optDataReceiveCom2 As System.Windows.Forms.RadioButton
    Friend WithEvents frameSendCom2 As System.Windows.Forms.GroupBox
    Friend WithEvents optDataSendCom2 As System.Windows.Forms.RadioButton
    Friend WithEvents optXMLSendCom2 As System.Windows.Forms.RadioButton
    Friend WithEvents cmdLoadCom2 As System.Windows.Forms.Button
    Friend WithEvents cmdSendSettingsCom2 As System.Windows.Forms.Button
    Friend WithEvents txtDataSentCom2 As System.Windows.Forms.TextBox
    Friend WithEvents txtDataReceivedCom2 As System.Windows.Forms.TextBox
    Friend WithEvents FrameCom2PortSettings As System.Windows.Forms.GroupBox
    Friend WithEvents cmbStopBitsCom2 As System.Windows.Forms.ComboBox
    Friend WithEvents cmbParityCom2 As System.Windows.Forms.ComboBox
    Friend WithEvents cmbBitRateCom2 As System.Windows.Forms.ComboBox
    Friend WithEvents txtWordLengthCom2 As System.Windows.Forms.TextBox
    Friend WithEvents lblWordLengthUnitsCom2 As System.Windows.Forms.Label
    Friend WithEvents lblBitRateCom2 As System.Windows.Forms.Label
    Friend WithEvents lblStopBitsCom2 As System.Windows.Forms.Label
    Friend WithEvents lblParityCom2 As System.Windows.Forms.Label
    Friend WithEvents lblBitRateUnitsCom2 As System.Windows.Forms.Label
    Friend WithEvents lblStopBitsUnitsCom2 As System.Windows.Forms.Label
    Friend WithEvents lblWordLengthCom2 As System.Windows.Forms.Label
    Friend WithEvents cmdSendDataCom2 As System.Windows.Forms.Button
    Friend WithEvents cmdDataReceiveCom2 As System.Windows.Forms.Button
    Friend WithEvents tabOptions_Page4 As System.Windows.Forms.TabPage
    Friend WithEvents lblMaxTimeGigabit1 As System.Windows.Forms.Label
    Friend WithEvents frameReceiveGigabit1 As System.Windows.Forms.GroupBox
    Friend WithEvents optDataReceiveGigabit1 As System.Windows.Forms.RadioButton
    Friend WithEvents optXMLReceiveGigabit1 As System.Windows.Forms.RadioButton
    Friend WithEvents frameSendGigabit1 As System.Windows.Forms.GroupBox
    Friend WithEvents optDataSendGigabit1 As System.Windows.Forms.RadioButton
    Friend WithEvents optXMLSendGigabit1 As System.Windows.Forms.RadioButton
    Friend WithEvents txtMaxTimeGigabit1 As System.Windows.Forms.TextBox
    Friend WithEvents cmdSendSettingsGigabit1 As System.Windows.Forms.Button
    Friend WithEvents cmdLoadGigabit1 As System.Windows.Forms.Button
    Friend WithEvents Frame3 As System.Windows.Forms.GroupBox
    Friend WithEvents txtRemotePortGigabit1 As System.Windows.Forms.TextBox
    Friend WithEvents txtGatewayGigabit1 As System.Windows.Forms.TextBox
    Friend WithEvents txtLocalSubnetMaskGigabit1 As System.Windows.Forms.TextBox
    Friend WithEvents txtLocalPortGigabit1 As System.Windows.Forms.TextBox
    Friend WithEvents txtLocalIPGigabit1 As System.Windows.Forms.TextBox
    Friend WithEvents txtRemoteIPGigabit1 As System.Windows.Forms.TextBox
    Friend WithEvents lblGatewayGigabit1 As System.Windows.Forms.Label
    Friend WithEvents lblLocalSubnetMaskGigabit1 As System.Windows.Forms.Label
    Friend WithEvents lblLocalIPGigabit1 As System.Windows.Forms.Label
    Friend WithEvents lblRemoteIPGigabit1 As System.Windows.Forms.Label
    Friend WithEvents FrameProtocolGigabit1 As System.Windows.Forms.GroupBox
    Friend WithEvents optUDPGigabit1 As System.Windows.Forms.RadioButton
    Friend WithEvents optTCPGigabit1 As System.Windows.Forms.RadioButton
    Friend WithEvents txtDataSentGigabit1 As System.Windows.Forms.TextBox
    Friend WithEvents cmdSendDataGigabit1 As System.Windows.Forms.Button
    Friend WithEvents cmdDataReceiveGigabit1 As System.Windows.Forms.Button
    Friend WithEvents txtDataReceivedGigabit1 As System.Windows.Forms.TextBox
    Friend WithEvents txtSaveTCPGigabit1 As System.Windows.Forms.TextBox
    Friend WithEvents tabOptions_Page3 As System.Windows.Forms.TabPage
    Friend WithEvents lblReadTimePCISer As System.Windows.Forms.Label
    Friend WithEvents txtReadTimePCISer As System.Windows.Forms.TextBox
    Friend WithEvents frameReceivePCISer As System.Windows.Forms.GroupBox
    Friend WithEvents optDataReceivePCISer As System.Windows.Forms.RadioButton
    Friend WithEvents optXMLReceivePCISer As System.Windows.Forms.RadioButton
    Friend WithEvents frameSendPCISer As System.Windows.Forms.GroupBox
    Friend WithEvents optDataSendPCISer As System.Windows.Forms.RadioButton
    Friend WithEvents optXMLSendPCISer As System.Windows.Forms.RadioButton
    Friend WithEvents cmdSendSettingsPCISer As System.Windows.Forms.Button
    Friend WithEvents cmdLoadPCISer As System.Windows.Forms.Button
    Friend WithEvents txtDataReceivedPCISer As System.Windows.Forms.TextBox
    Friend WithEvents framePciProtocol As System.Windows.Forms.GroupBox
    Friend WithEvents optRS485 As System.Windows.Forms.RadioButton
    Friend WithEvents optRS422 As System.Windows.Forms.RadioButton
    Friend WithEvents optRS232 As System.Windows.Forms.RadioButton
    Friend WithEvents FramePciResponse As System.Windows.Forms.GroupBox
    Friend WithEvents cmbBitRatePCISer As System.Windows.Forms.ComboBox
    Friend WithEvents cmbParityPCISer As System.Windows.Forms.ComboBox
    Friend WithEvents cmbStopBitsPCISer As System.Windows.Forms.ComboBox
    Friend WithEvents txtSerialMaxTimePCISer As System.Windows.Forms.TextBox
    Friend WithEvents txtSerialDelayPCISer As System.Windows.Forms.TextBox
    Friend WithEvents txtSerialWordLengthPCISer As System.Windows.Forms.TextBox
    Friend WithEvents lblParityPCISer As System.Windows.Forms.Label
    Friend WithEvents lblStopBitsPCISer As System.Windows.Forms.Label
    Friend WithEvents lblBitRatePCISer As System.Windows.Forms.Label
    Friend WithEvents lblSerialMaxTimePCISer As System.Windows.Forms.Label
    Friend WithEvents lblSerialDelayPCISer As System.Windows.Forms.Label
    Friend WithEvents lblSerialMaxTimeUnitsPCISer As System.Windows.Forms.Label
    Friend WithEvents lblSerialDelayUnitsPCISer As System.Windows.Forms.Label
    Friend WithEvents lblBitRateUnitsPCISer As System.Windows.Forms.Label
    Friend WithEvents lblStopBitsUnitsPCISer As System.Windows.Forms.Label
    Friend WithEvents lblSerialWordLengthPCISer As System.Windows.Forms.Label
    Friend WithEvents txtDataSentPCISer As System.Windows.Forms.TextBox
    Friend WithEvents cmdSendDataPCISer As System.Windows.Forms.Button
    Friend WithEvents cmdDataReceivePCISer As System.Windows.Forms.Button
    Friend WithEvents txtSaveSerialPCISer As System.Windows.Forms.TextBox
    Friend WithEvents tabOptions_Page5 As System.Windows.Forms.TabPage
    Friend WithEvents lblMaxTimeGigabit2 As System.Windows.Forms.Label
    Friend WithEvents txtSaveTCPGigabit2 As System.Windows.Forms.TextBox
    Friend WithEvents txtDataReceivedGigabit2 As System.Windows.Forms.TextBox
    Friend WithEvents cmdDataReceiveGigabit2 As System.Windows.Forms.Button
    Friend WithEvents cmdSendDataGigabit2 As System.Windows.Forms.Button
    Friend WithEvents txtDataSentGigabit2 As System.Windows.Forms.TextBox
    Friend WithEvents cmdLoadGigabit2 As System.Windows.Forms.Button
    Friend WithEvents cmdSendSettingsGigabit2 As System.Windows.Forms.Button
    Friend WithEvents frameSendGigabit2 As System.Windows.Forms.GroupBox
    Friend WithEvents optXMLSendGigabit2 As System.Windows.Forms.RadioButton
    Friend WithEvents optDataSendGigabit2 As System.Windows.Forms.RadioButton
    Friend WithEvents frameReceiveGigabit2 As System.Windows.Forms.GroupBox
    Friend WithEvents optXMLReceiveGigabit2 As System.Windows.Forms.RadioButton
    Friend WithEvents optDataReceiveGigabit2 As System.Windows.Forms.RadioButton
    Friend WithEvents Frame4 As System.Windows.Forms.GroupBox
    Friend WithEvents txtRemotePortGigabit2 As System.Windows.Forms.TextBox
    Friend WithEvents txtLocalIPGigabit2 As System.Windows.Forms.TextBox
    Friend WithEvents txtGatewayGigabit2 As System.Windows.Forms.TextBox
    Friend WithEvents txtLocalSubnetMaskGigabit2 As System.Windows.Forms.TextBox
    Friend WithEvents txtRemoteIPGigabit2 As System.Windows.Forms.TextBox
    Friend WithEvents lblLocalIPGigabit2 As System.Windows.Forms.Label
    Friend WithEvents lblGatewayGigabit2 As System.Windows.Forms.Label
    Friend WithEvents lblLocalSubnetMaskGigabit2 As System.Windows.Forms.Label
    Friend WithEvents lblRemoteIPGigabit2 As System.Windows.Forms.Label
    Friend WithEvents txtMaxTimeGigabit2 As System.Windows.Forms.TextBox
    Friend WithEvents optXMLReceive_5 As System.Windows.Forms.RadioButton
    Friend WithEvents optDataReceive_5 As System.Windows.Forms.RadioButton
    Friend WithEvents tabOptions_Page7 As System.Windows.Forms.TabPage
    Friend WithEvents lblTimingValueCAN As System.Windows.Forms.Label
    Friend WithEvents lblAcceptanceCodeCAN As System.Windows.Forms.Label
    Friend WithEvents lblAcceptanceMaskCAN As System.Windows.Forms.Label
    Friend WithEvents lblMaxTimeCAN As System.Windows.Forms.Label
    Friend WithEvents lblMessageIDCAN As System.Windows.Forms.Label
    Friend WithEvents txtAcceptanceCodeCAN As System.Windows.Forms.TextBox
    Friend WithEvents txtAcceptanceMaskCAN As System.Windows.Forms.TextBox
    Friend WithEvents txtDataSendCAN As System.Windows.Forms.TextBox
    Friend WithEvents txtDataReceivedCAN As System.Windows.Forms.TextBox
    Friend WithEvents frameSendCAN As System.Windows.Forms.GroupBox
    Friend WithEvents optXMLSendCAN As System.Windows.Forms.RadioButton
    Friend WithEvents optDataSendCAN As System.Windows.Forms.RadioButton
    Friend WithEvents frameReceiveCAN As System.Windows.Forms.GroupBox
    Friend WithEvents optXMLReceiveCAN As System.Windows.Forms.RadioButton
    Friend WithEvents optDataReceiveCAN As System.Windows.Forms.RadioButton
    Friend WithEvents cmdSendDataCAN As System.Windows.Forms.Button
    Friend WithEvents cmdDataReceiveCAN As System.Windows.Forms.Button
    Friend WithEvents txtSaveTCPCAN As System.Windows.Forms.TextBox
    Friend WithEvents txtMaxTimeCAN As System.Windows.Forms.TextBox
    Friend WithEvents chkThreeSamplesCAN As System.Windows.Forms.CheckBox
    Friend WithEvents chkSingleFilterCAN As System.Windows.Forms.CheckBox
    Friend WithEvents frameChannelCAN As System.Windows.Forms.GroupBox
    Friend WithEvents OptChannel1CAN As System.Windows.Forms.RadioButton
    Friend WithEvents OptChannel2CAN As System.Windows.Forms.RadioButton
    Friend WithEvents cmdLoadCAN As System.Windows.Forms.Button
    Friend WithEvents cmdSendSettingsCAN As System.Windows.Forms.Button
    Friend WithEvents txtMessageIDCAN As System.Windows.Forms.TextBox
    Friend WithEvents cmbTimingValueCAN As System.Windows.Forms.ComboBox
    Friend WithEvents optDataReceive_7 As System.Windows.Forms.RadioButton
    Friend WithEvents optXMLReceive_7 As System.Windows.Forms.RadioButton
    Friend WithEvents tabOptions_Page9 As System.Windows.Forms.TabPage
    Friend WithEvents Atlas_SFP As VIPERT_Common_Controls.Atlas_SFP
    Friend WithEvents txtNumChars As System.Windows.Forms.TextBox
    Friend WithEvents chkReceive As System.Windows.Forms.CheckBox
    Friend WithEvents chkSend As System.Windows.Forms.CheckBox
    Friend WithEvents Frame1 As System.Windows.Forms.GroupBox
    Friend WithEvents OptCan As System.Windows.Forms.RadioButton
    Friend WithEvents optGigabit2 As System.Windows.Forms.RadioButton
    Friend WithEvents optCOM1 As System.Windows.Forms.RadioButton
    Friend WithEvents optCom2 As System.Windows.Forms.RadioButton
    Friend WithEvents optGigabit1 As System.Windows.Forms.RadioButton
    Friend WithEvents optSerial As System.Windows.Forms.RadioButton
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents tabOptions_Page10 As System.Windows.Forms.TabPage
    Friend WithEvents Panel_Conifg As VIPERT_Common_Controls.Panel_Conifg
    Friend WithEvents FrameProtocolGigabit2 As System.Windows.Forms.GroupBox
    Friend WithEvents optUDPGigabit2 As System.Windows.Forms.RadioButton
    Friend WithEvents optTCPGigabit2 As System.Windows.Forms.RadioButton
    Friend WithEvents txtLocalPortGigabit2 As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtReceiveLengthCom1 As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtReceiveLengthCom2 As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtReceiveLengthPCISer As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents chkSelfReceptionCAN As System.Windows.Forms.CheckBox
    Friend WithEvents chkRemoteFrameCAN As System.Windows.Forms.CheckBox
    Friend WithEvents chkSingleShotCAN As System.Windows.Forms.CheckBox
    Friend WithEvents frameIDTypeCAN As System.Windows.Forms.GroupBox
    Friend WithEvents optExtendedIDCAN As System.Windows.Forms.RadioButton
    Friend WithEvents optBasicIDCAN As System.Windows.Forms.RadioButton
    Friend WithEvents cmdDataListenCAN As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBus_IO))
        Me.cmdReset = New System.Windows.Forms.Button()
        Me.cmdQuit = New System.Windows.Forms.Button()
        Me.cmdHelp = New System.Windows.Forms.Button()
        Me.CommonDialog1 = New System.Windows.Forms.PrintDialog()
        Me.sbrUserInformation = New System.Windows.Forms.StatusBar()
        Me.sbrUserInformation_Panel1 = New System.Windows.Forms.StatusBarPanel()
        Me.sbrUserInformation_Panel2 = New System.Windows.Forms.StatusBarPanel()
        Me.tabOptions = New System.Windows.Forms.TabControl()
        Me.tabOptions_Page1 = New System.Windows.Forms.TabPage()
        Me.lblReadTimeCom1 = New System.Windows.Forms.Label()
        Me.txtReadTimeCom1 = New System.Windows.Forms.TextBox()
        Me.frameReceiveCom1 = New System.Windows.Forms.GroupBox()
        Me.optDataReceiveCom1 = New System.Windows.Forms.RadioButton()
        Me.optXMLReceiveCom1 = New System.Windows.Forms.RadioButton()
        Me.frameSendCom1 = New System.Windows.Forms.GroupBox()
        Me.optXMLSendCom1 = New System.Windows.Forms.RadioButton()
        Me.optDataSendCom1 = New System.Windows.Forms.RadioButton()
        Me.cmdSendSettingsCom1 = New System.Windows.Forms.Button()
        Me.cmdLoadCom1 = New System.Windows.Forms.Button()
        Me.txtDataReceivedCom1 = New System.Windows.Forms.TextBox()
        Me.cmdDataReceiveCom1 = New System.Windows.Forms.Button()
        Me.cmdSendDataCom1 = New System.Windows.Forms.Button()
        Me.txtDataSentCom1 = New System.Windows.Forms.TextBox()
        Me.FrameCom1PortSettings = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtReceiveLengthCom1 = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbParityCom1 = New System.Windows.Forms.ComboBox()
        Me.cmbStopBitsCom1 = New System.Windows.Forms.ComboBox()
        Me.cmbBitRateCom1 = New System.Windows.Forms.ComboBox()
        Me.txtWordLengthCom1 = New System.Windows.Forms.TextBox()
        Me.lblWordLengthUnitsCom1 = New System.Windows.Forms.Label()
        Me.lblParityCom1 = New System.Windows.Forms.Label()
        Me.lblStopBitsCom1 = New System.Windows.Forms.Label()
        Me.lblBitRateCom1 = New System.Windows.Forms.Label()
        Me.lblBitRateUnitsCom1 = New System.Windows.Forms.Label()
        Me.lblStopBitsUnitsCom1 = New System.Windows.Forms.Label()
        Me.lblWordLengthCom1 = New System.Windows.Forms.Label()
        Me.tabOptions_Page2 = New System.Windows.Forms.TabPage()
        Me.lblReadTimeCom2 = New System.Windows.Forms.Label()
        Me.txtReadTimeCom2 = New System.Windows.Forms.TextBox()
        Me.frameReceiveCom2 = New System.Windows.Forms.GroupBox()
        Me.optXMLReceiveCom2 = New System.Windows.Forms.RadioButton()
        Me.optDataReceiveCom2 = New System.Windows.Forms.RadioButton()
        Me.frameSendCom2 = New System.Windows.Forms.GroupBox()
        Me.optDataSendCom2 = New System.Windows.Forms.RadioButton()
        Me.optXMLSendCom2 = New System.Windows.Forms.RadioButton()
        Me.cmdLoadCom2 = New System.Windows.Forms.Button()
        Me.cmdSendSettingsCom2 = New System.Windows.Forms.Button()
        Me.txtDataSentCom2 = New System.Windows.Forms.TextBox()
        Me.txtDataReceivedCom2 = New System.Windows.Forms.TextBox()
        Me.FrameCom2PortSettings = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtReceiveLengthCom2 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbStopBitsCom2 = New System.Windows.Forms.ComboBox()
        Me.cmbParityCom2 = New System.Windows.Forms.ComboBox()
        Me.cmbBitRateCom2 = New System.Windows.Forms.ComboBox()
        Me.txtWordLengthCom2 = New System.Windows.Forms.TextBox()
        Me.lblWordLengthUnitsCom2 = New System.Windows.Forms.Label()
        Me.lblBitRateCom2 = New System.Windows.Forms.Label()
        Me.lblStopBitsCom2 = New System.Windows.Forms.Label()
        Me.lblParityCom2 = New System.Windows.Forms.Label()
        Me.lblBitRateUnitsCom2 = New System.Windows.Forms.Label()
        Me.lblStopBitsUnitsCom2 = New System.Windows.Forms.Label()
        Me.lblWordLengthCom2 = New System.Windows.Forms.Label()
        Me.cmdSendDataCom2 = New System.Windows.Forms.Button()
        Me.cmdDataReceiveCom2 = New System.Windows.Forms.Button()
        Me.tabOptions_Page3 = New System.Windows.Forms.TabPage()
        Me.lblReadTimePCISer = New System.Windows.Forms.Label()
        Me.txtReadTimePCISer = New System.Windows.Forms.TextBox()
        Me.frameReceivePCISer = New System.Windows.Forms.GroupBox()
        Me.optDataReceivePCISer = New System.Windows.Forms.RadioButton()
        Me.optXMLReceivePCISer = New System.Windows.Forms.RadioButton()
        Me.frameSendPCISer = New System.Windows.Forms.GroupBox()
        Me.optDataSendPCISer = New System.Windows.Forms.RadioButton()
        Me.optXMLSendPCISer = New System.Windows.Forms.RadioButton()
        Me.cmdSendSettingsPCISer = New System.Windows.Forms.Button()
        Me.cmdLoadPCISer = New System.Windows.Forms.Button()
        Me.txtDataReceivedPCISer = New System.Windows.Forms.TextBox()
        Me.framePciProtocol = New System.Windows.Forms.GroupBox()
        Me.optRS485 = New System.Windows.Forms.RadioButton()
        Me.optRS422 = New System.Windows.Forms.RadioButton()
        Me.optRS232 = New System.Windows.Forms.RadioButton()
        Me.FramePciResponse = New System.Windows.Forms.GroupBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtReceiveLengthPCISer = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmbBitRatePCISer = New System.Windows.Forms.ComboBox()
        Me.cmbParityPCISer = New System.Windows.Forms.ComboBox()
        Me.cmbStopBitsPCISer = New System.Windows.Forms.ComboBox()
        Me.txtSerialMaxTimePCISer = New System.Windows.Forms.TextBox()
        Me.txtSerialDelayPCISer = New System.Windows.Forms.TextBox()
        Me.txtSerialWordLengthPCISer = New System.Windows.Forms.TextBox()
        Me.lblParityPCISer = New System.Windows.Forms.Label()
        Me.lblStopBitsPCISer = New System.Windows.Forms.Label()
        Me.lblBitRatePCISer = New System.Windows.Forms.Label()
        Me.lblSerialMaxTimePCISer = New System.Windows.Forms.Label()
        Me.lblSerialDelayPCISer = New System.Windows.Forms.Label()
        Me.lblSerialMaxTimeUnitsPCISer = New System.Windows.Forms.Label()
        Me.lblSerialDelayUnitsPCISer = New System.Windows.Forms.Label()
        Me.lblBitRateUnitsPCISer = New System.Windows.Forms.Label()
        Me.lblStopBitsUnitsPCISer = New System.Windows.Forms.Label()
        Me.lblSerialWordLengthPCISer = New System.Windows.Forms.Label()
        Me.txtDataSentPCISer = New System.Windows.Forms.TextBox()
        Me.cmdSendDataPCISer = New System.Windows.Forms.Button()
        Me.cmdDataReceivePCISer = New System.Windows.Forms.Button()
        Me.txtSaveSerialPCISer = New System.Windows.Forms.TextBox()
        Me.tabOptions_Page4 = New System.Windows.Forms.TabPage()
        Me.lblMaxTimeGigabit1 = New System.Windows.Forms.Label()
        Me.frameReceiveGigabit1 = New System.Windows.Forms.GroupBox()
        Me.optDataReceiveGigabit1 = New System.Windows.Forms.RadioButton()
        Me.optXMLReceiveGigabit1 = New System.Windows.Forms.RadioButton()
        Me.frameSendGigabit1 = New System.Windows.Forms.GroupBox()
        Me.optDataSendGigabit1 = New System.Windows.Forms.RadioButton()
        Me.optXMLSendGigabit1 = New System.Windows.Forms.RadioButton()
        Me.txtMaxTimeGigabit1 = New System.Windows.Forms.TextBox()
        Me.cmdSendSettingsGigabit1 = New System.Windows.Forms.Button()
        Me.cmdLoadGigabit1 = New System.Windows.Forms.Button()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.txtRemotePortGigabit1 = New System.Windows.Forms.TextBox()
        Me.txtGatewayGigabit1 = New System.Windows.Forms.TextBox()
        Me.txtLocalSubnetMaskGigabit1 = New System.Windows.Forms.TextBox()
        Me.txtLocalPortGigabit1 = New System.Windows.Forms.TextBox()
        Me.txtLocalIPGigabit1 = New System.Windows.Forms.TextBox()
        Me.txtRemoteIPGigabit1 = New System.Windows.Forms.TextBox()
        Me.lblGatewayGigabit1 = New System.Windows.Forms.Label()
        Me.lblLocalSubnetMaskGigabit1 = New System.Windows.Forms.Label()
        Me.lblLocalIPGigabit1 = New System.Windows.Forms.Label()
        Me.lblRemoteIPGigabit1 = New System.Windows.Forms.Label()
        Me.FrameProtocolGigabit1 = New System.Windows.Forms.GroupBox()
        Me.optUDPGigabit1 = New System.Windows.Forms.RadioButton()
        Me.optTCPGigabit1 = New System.Windows.Forms.RadioButton()
        Me.txtDataSentGigabit1 = New System.Windows.Forms.TextBox()
        Me.cmdSendDataGigabit1 = New System.Windows.Forms.Button()
        Me.cmdDataReceiveGigabit1 = New System.Windows.Forms.Button()
        Me.txtDataReceivedGigabit1 = New System.Windows.Forms.TextBox()
        Me.txtSaveTCPGigabit1 = New System.Windows.Forms.TextBox()
        Me.tabOptions_Page5 = New System.Windows.Forms.TabPage()
        Me.lblMaxTimeGigabit2 = New System.Windows.Forms.Label()
        Me.txtSaveTCPGigabit2 = New System.Windows.Forms.TextBox()
        Me.FrameProtocolGigabit2 = New System.Windows.Forms.GroupBox()
        Me.optUDPGigabit2 = New System.Windows.Forms.RadioButton()
        Me.optTCPGigabit2 = New System.Windows.Forms.RadioButton()
        Me.txtDataReceivedGigabit2 = New System.Windows.Forms.TextBox()
        Me.cmdDataReceiveGigabit2 = New System.Windows.Forms.Button()
        Me.cmdSendDataGigabit2 = New System.Windows.Forms.Button()
        Me.txtDataSentGigabit2 = New System.Windows.Forms.TextBox()
        Me.cmdLoadGigabit2 = New System.Windows.Forms.Button()
        Me.cmdSendSettingsGigabit2 = New System.Windows.Forms.Button()
        Me.frameSendGigabit2 = New System.Windows.Forms.GroupBox()
        Me.optXMLSendGigabit2 = New System.Windows.Forms.RadioButton()
        Me.optDataSendGigabit2 = New System.Windows.Forms.RadioButton()
        Me.frameReceiveGigabit2 = New System.Windows.Forms.GroupBox()
        Me.optXMLReceiveGigabit2 = New System.Windows.Forms.RadioButton()
        Me.optDataReceiveGigabit2 = New System.Windows.Forms.RadioButton()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.txtLocalPortGigabit2 = New System.Windows.Forms.TextBox()
        Me.txtRemotePortGigabit2 = New System.Windows.Forms.TextBox()
        Me.txtLocalIPGigabit2 = New System.Windows.Forms.TextBox()
        Me.txtGatewayGigabit2 = New System.Windows.Forms.TextBox()
        Me.txtLocalSubnetMaskGigabit2 = New System.Windows.Forms.TextBox()
        Me.txtRemoteIPGigabit2 = New System.Windows.Forms.TextBox()
        Me.lblLocalIPGigabit2 = New System.Windows.Forms.Label()
        Me.lblGatewayGigabit2 = New System.Windows.Forms.Label()
        Me.lblLocalSubnetMaskGigabit2 = New System.Windows.Forms.Label()
        Me.lblRemoteIPGigabit2 = New System.Windows.Forms.Label()
        Me.txtMaxTimeGigabit2 = New System.Windows.Forms.TextBox()
        Me.tabOptions_Page7 = New System.Windows.Forms.TabPage()
        Me.cmdDataListenCAN = New System.Windows.Forms.Button()
        Me.chkSelfReceptionCAN = New System.Windows.Forms.CheckBox()
        Me.chkRemoteFrameCAN = New System.Windows.Forms.CheckBox()
        Me.chkSingleShotCAN = New System.Windows.Forms.CheckBox()
        Me.frameIDTypeCAN = New System.Windows.Forms.GroupBox()
        Me.optExtendedIDCAN = New System.Windows.Forms.RadioButton()
        Me.optBasicIDCAN = New System.Windows.Forms.RadioButton()
        Me.lblTimingValueCAN = New System.Windows.Forms.Label()
        Me.lblAcceptanceCodeCAN = New System.Windows.Forms.Label()
        Me.lblAcceptanceMaskCAN = New System.Windows.Forms.Label()
        Me.lblMaxTimeCAN = New System.Windows.Forms.Label()
        Me.lblMessageIDCAN = New System.Windows.Forms.Label()
        Me.txtAcceptanceCodeCAN = New System.Windows.Forms.TextBox()
        Me.txtAcceptanceMaskCAN = New System.Windows.Forms.TextBox()
        Me.txtDataSendCAN = New System.Windows.Forms.TextBox()
        Me.txtDataReceivedCAN = New System.Windows.Forms.TextBox()
        Me.frameSendCAN = New System.Windows.Forms.GroupBox()
        Me.optXMLSendCAN = New System.Windows.Forms.RadioButton()
        Me.optDataSendCAN = New System.Windows.Forms.RadioButton()
        Me.frameReceiveCAN = New System.Windows.Forms.GroupBox()
        Me.optXMLReceiveCAN = New System.Windows.Forms.RadioButton()
        Me.optDataReceiveCAN = New System.Windows.Forms.RadioButton()
        Me.cmdSendDataCAN = New System.Windows.Forms.Button()
        Me.cmdDataReceiveCAN = New System.Windows.Forms.Button()
        Me.txtSaveTCPCAN = New System.Windows.Forms.TextBox()
        Me.txtMaxTimeCAN = New System.Windows.Forms.TextBox()
        Me.chkThreeSamplesCAN = New System.Windows.Forms.CheckBox()
        Me.chkSingleFilterCAN = New System.Windows.Forms.CheckBox()
        Me.frameChannelCAN = New System.Windows.Forms.GroupBox()
        Me.OptChannel1CAN = New System.Windows.Forms.RadioButton()
        Me.OptChannel2CAN = New System.Windows.Forms.RadioButton()
        Me.cmdLoadCAN = New System.Windows.Forms.Button()
        Me.cmdSendSettingsCAN = New System.Windows.Forms.Button()
        Me.txtMessageIDCAN = New System.Windows.Forms.TextBox()
        Me.cmbTimingValueCAN = New System.Windows.Forms.ComboBox()
        Me.tabOptions_Page9 = New System.Windows.Forms.TabPage()
        Me.Atlas_SFP = New VIPERT_Common_Controls.Atlas_SFP()
        Me.txtNumChars = New System.Windows.Forms.TextBox()
        Me.chkReceive = New System.Windows.Forms.CheckBox()
        Me.chkSend = New System.Windows.Forms.CheckBox()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.OptCan = New System.Windows.Forms.RadioButton()
        Me.optGigabit2 = New System.Windows.Forms.RadioButton()
        Me.optCOM1 = New System.Windows.Forms.RadioButton()
        Me.optCom2 = New System.Windows.Forms.RadioButton()
        Me.optGigabit1 = New System.Windows.Forms.RadioButton()
        Me.optSerial = New System.Windows.Forms.RadioButton()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.tabOptions_Page10 = New System.Windows.Forms.TabPage()
        Me.Panel_Conifg = New VIPERT_Common_Controls.Panel_Conifg()
        Me.optXMLReceive_5 = New System.Windows.Forms.RadioButton()
        Me.optDataReceive_5 = New System.Windows.Forms.RadioButton()
        Me.optDataReceive_7 = New System.Windows.Forms.RadioButton()
        Me.optXMLReceive_7 = New System.Windows.Forms.RadioButton()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        CType(Me.sbrUserInformation_Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sbrUserInformation_Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabOptions.SuspendLayout()
        Me.tabOptions_Page1.SuspendLayout()
        Me.frameReceiveCom1.SuspendLayout()
        Me.frameSendCom1.SuspendLayout()
        Me.FrameCom1PortSettings.SuspendLayout()
        Me.tabOptions_Page2.SuspendLayout()
        Me.frameReceiveCom2.SuspendLayout()
        Me.frameSendCom2.SuspendLayout()
        Me.FrameCom2PortSettings.SuspendLayout()
        Me.tabOptions_Page3.SuspendLayout()
        Me.frameReceivePCISer.SuspendLayout()
        Me.frameSendPCISer.SuspendLayout()
        Me.framePciProtocol.SuspendLayout()
        Me.FramePciResponse.SuspendLayout()
        Me.tabOptions_Page4.SuspendLayout()
        Me.frameReceiveGigabit1.SuspendLayout()
        Me.frameSendGigabit1.SuspendLayout()
        Me.Frame3.SuspendLayout()
        Me.FrameProtocolGigabit1.SuspendLayout()
        Me.tabOptions_Page5.SuspendLayout()
        Me.FrameProtocolGigabit2.SuspendLayout()
        Me.frameSendGigabit2.SuspendLayout()
        Me.frameReceiveGigabit2.SuspendLayout()
        Me.Frame4.SuspendLayout()
        Me.tabOptions_Page7.SuspendLayout()
        Me.frameIDTypeCAN.SuspendLayout()
        Me.frameSendCAN.SuspendLayout()
        Me.frameReceiveCAN.SuspendLayout()
        Me.frameChannelCAN.SuspendLayout()
        Me.tabOptions_Page9.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.tabOptions_Page10.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdReset
        '
        Me.cmdReset.BackColor = System.Drawing.SystemColors.Control
        Me.cmdReset.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdReset.Location = New System.Drawing.Point(469, 429)
        Me.cmdReset.Name = "cmdReset"
        Me.cmdReset.Size = New System.Drawing.Size(76, 23)
        Me.cmdReset.TabIndex = 2
        Me.cmdReset.Text = "&Reset"
        Me.cmdReset.UseVisualStyleBackColor = False
        '
        'cmdQuit
        '
        Me.cmdQuit.BackColor = System.Drawing.SystemColors.Control
        Me.cmdQuit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdQuit.Location = New System.Drawing.Point(566, 429)
        Me.cmdQuit.Name = "cmdQuit"
        Me.cmdQuit.Size = New System.Drawing.Size(76, 23)
        Me.cmdQuit.TabIndex = 1
        Me.cmdQuit.Text = "&Quit"
        Me.cmdQuit.UseVisualStyleBackColor = False
        '
        'cmdHelp
        '
        Me.cmdHelp.BackColor = System.Drawing.SystemColors.Control
        Me.cmdHelp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdHelp.Location = New System.Drawing.Point(372, 429)
        Me.cmdHelp.Name = "cmdHelp"
        Me.cmdHelp.Size = New System.Drawing.Size(76, 23)
        Me.cmdHelp.TabIndex = 0
        Me.cmdHelp.Text = "Help"
        Me.cmdHelp.UseVisualStyleBackColor = False
        '
        'sbrUserInformation
        '
        Me.sbrUserInformation.Location = New System.Drawing.Point(0, 463)
        Me.sbrUserInformation.Name = "sbrUserInformation"
        Me.sbrUserInformation.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.sbrUserInformation_Panel1, Me.sbrUserInformation_Panel2})
        Me.sbrUserInformation.Size = New System.Drawing.Size(678, 17)
        Me.sbrUserInformation.SizingGrip = False
        Me.sbrUserInformation.TabIndex = 3
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
        Me.sbrUserInformation_Panel2.Width = 610
        '
        'tabOptions
        '
        Me.tabOptions.Controls.Add(Me.tabOptions_Page1)
        Me.tabOptions.Controls.Add(Me.tabOptions_Page2)
        Me.tabOptions.Controls.Add(Me.tabOptions_Page3)
        Me.tabOptions.Controls.Add(Me.tabOptions_Page4)
        Me.tabOptions.Controls.Add(Me.tabOptions_Page5)
        Me.tabOptions.Controls.Add(Me.tabOptions_Page7)
        Me.tabOptions.Controls.Add(Me.tabOptions_Page9)
        Me.tabOptions.Controls.Add(Me.tabOptions_Page10)
        Me.tabOptions.Location = New System.Drawing.Point(24, 9)
        Me.tabOptions.Name = "tabOptions"
        Me.tabOptions.SelectedIndex = 0
        Me.tabOptions.Size = New System.Drawing.Size(642, 414)
        Me.tabOptions.TabIndex = 0
        '
        'tabOptions_Page1
        '
        Me.tabOptions_Page1.Controls.Add(Me.lblReadTimeCom1)
        Me.tabOptions_Page1.Controls.Add(Me.txtReadTimeCom1)
        Me.tabOptions_Page1.Controls.Add(Me.frameReceiveCom1)
        Me.tabOptions_Page1.Controls.Add(Me.frameSendCom1)
        Me.tabOptions_Page1.Controls.Add(Me.cmdSendSettingsCom1)
        Me.tabOptions_Page1.Controls.Add(Me.cmdLoadCom1)
        Me.tabOptions_Page1.Controls.Add(Me.txtDataReceivedCom1)
        Me.tabOptions_Page1.Controls.Add(Me.cmdDataReceiveCom1)
        Me.tabOptions_Page1.Controls.Add(Me.cmdSendDataCom1)
        Me.tabOptions_Page1.Controls.Add(Me.txtDataSentCom1)
        Me.tabOptions_Page1.Controls.Add(Me.FrameCom1PortSettings)
        Me.tabOptions_Page1.Location = New System.Drawing.Point(4, 22)
        Me.tabOptions_Page1.Name = "tabOptions_Page1"
        Me.tabOptions_Page1.Size = New System.Drawing.Size(634, 388)
        Me.tabOptions_Page1.TabIndex = 0
        Me.tabOptions_Page1.Text = "COM 1"
        '
        'lblReadTimeCom1
        '
        Me.lblReadTimeCom1.BackColor = System.Drawing.SystemColors.Control
        Me.lblReadTimeCom1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblReadTimeCom1.Location = New System.Drawing.Point(332, 344)
        Me.lblReadTimeCom1.Name = "lblReadTimeCom1"
        Me.lblReadTimeCom1.Size = New System.Drawing.Size(114, 17)
        Me.lblReadTimeCom1.TabIndex = 115
        Me.lblReadTimeCom1.Text = "Data Read Time (Sec)"
        Me.lblReadTimeCom1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtReadTimeCom1
        '
        Me.txtReadTimeCom1.BackColor = System.Drawing.SystemColors.Window
        Me.txtReadTimeCom1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtReadTimeCom1.Location = New System.Drawing.Point(453, 340)
        Me.txtReadTimeCom1.Name = "txtReadTimeCom1"
        Me.txtReadTimeCom1.Size = New System.Drawing.Size(58, 20)
        Me.txtReadTimeCom1.TabIndex = 5
        Me.txtReadTimeCom1.Text = "10"
        '
        'frameReceiveCom1
        '
        Me.frameReceiveCom1.BackColor = System.Drawing.SystemColors.Control
        Me.frameReceiveCom1.Controls.Add(Me.optDataReceiveCom1)
        Me.frameReceiveCom1.Controls.Add(Me.optXMLReceiveCom1)
        Me.frameReceiveCom1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.frameReceiveCom1.Location = New System.Drawing.Point(316, 191)
        Me.frameReceiveCom1.Name = "frameReceiveCom1"
        Me.frameReceiveCom1.Size = New System.Drawing.Size(292, 33)
        Me.frameReceiveCom1.TabIndex = 18
        Me.frameReceiveCom1.TabStop = False
        Me.frameReceiveCom1.Text = "Receive Signal Type"
        '
        'optDataReceiveCom1
        '
        Me.optDataReceiveCom1.BackColor = System.Drawing.SystemColors.Control
        Me.optDataReceiveCom1.Checked = True
        Me.optDataReceiveCom1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optDataReceiveCom1.Location = New System.Drawing.Point(218, 8)
        Me.optDataReceiveCom1.Name = "optDataReceiveCom1"
        Me.optDataReceiveCom1.Size = New System.Drawing.Size(50, 17)
        Me.optDataReceiveCom1.TabIndex = 20
        Me.optDataReceiveCom1.TabStop = True
        Me.optDataReceiveCom1.Text = "Data"
        Me.optDataReceiveCom1.UseVisualStyleBackColor = False
        '
        'optXMLReceiveCom1
        '
        Me.optXMLReceiveCom1.BackColor = System.Drawing.SystemColors.Control
        Me.optXMLReceiveCom1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optXMLReceiveCom1.Location = New System.Drawing.Point(113, 8)
        Me.optXMLReceiveCom1.Name = "optXMLReceiveCom1"
        Me.optXMLReceiveCom1.Size = New System.Drawing.Size(98, 17)
        Me.optXMLReceiveCom1.TabIndex = 19
        Me.optXMLReceiveCom1.Text = "XML Instruction"
        Me.optXMLReceiveCom1.UseVisualStyleBackColor = False
        Me.optXMLReceiveCom1.Visible = False
        '
        'frameSendCom1
        '
        Me.frameSendCom1.BackColor = System.Drawing.SystemColors.Control
        Me.frameSendCom1.Controls.Add(Me.optXMLSendCom1)
        Me.frameSendCom1.Controls.Add(Me.optDataSendCom1)
        Me.frameSendCom1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.frameSendCom1.Location = New System.Drawing.Point(16, 191)
        Me.frameSendCom1.Name = "frameSendCom1"
        Me.frameSendCom1.Size = New System.Drawing.Size(292, 33)
        Me.frameSendCom1.TabIndex = 30
        Me.frameSendCom1.TabStop = False
        Me.frameSendCom1.Text = "Send Signal Type"
        '
        'optXMLSendCom1
        '
        Me.optXMLSendCom1.BackColor = System.Drawing.SystemColors.Control
        Me.optXMLSendCom1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optXMLSendCom1.Location = New System.Drawing.Point(105, 8)
        Me.optXMLSendCom1.Name = "optXMLSendCom1"
        Me.optXMLSendCom1.Size = New System.Drawing.Size(98, 17)
        Me.optXMLSendCom1.TabIndex = 32
        Me.optXMLSendCom1.Text = "XML Instruction"
        Me.optXMLSendCom1.UseVisualStyleBackColor = False
        Me.optXMLSendCom1.Visible = False
        '
        'optDataSendCom1
        '
        Me.optDataSendCom1.BackColor = System.Drawing.SystemColors.Control
        Me.optDataSendCom1.Checked = True
        Me.optDataSendCom1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optDataSendCom1.Location = New System.Drawing.Point(210, 8)
        Me.optDataSendCom1.Name = "optDataSendCom1"
        Me.optDataSendCom1.Size = New System.Drawing.Size(50, 17)
        Me.optDataSendCom1.TabIndex = 31
        Me.optDataSendCom1.TabStop = True
        Me.optDataSendCom1.Text = "Data"
        Me.optDataSendCom1.UseVisualStyleBackColor = False
        '
        'cmdSendSettingsCom1
        '
        Me.cmdSendSettingsCom1.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSendSettingsCom1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSendSettingsCom1.Location = New System.Drawing.Point(477, 51)
        Me.cmdSendSettingsCom1.Name = "cmdSendSettingsCom1"
        Me.cmdSendSettingsCom1.Size = New System.Drawing.Size(147, 25)
        Me.cmdSendSettingsCom1.TabIndex = 2
        Me.cmdSendSettingsCom1.Text = "Send Settings To Device"
        Me.cmdSendSettingsCom1.UseVisualStyleBackColor = False
        '
        'cmdLoadCom1
        '
        Me.cmdLoadCom1.BackColor = System.Drawing.SystemColors.Control
        Me.cmdLoadCom1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdLoadCom1.Location = New System.Drawing.Point(477, 19)
        Me.cmdLoadCom1.Name = "cmdLoadCom1"
        Me.cmdLoadCom1.Size = New System.Drawing.Size(147, 25)
        Me.cmdLoadCom1.TabIndex = 1
        Me.cmdLoadCom1.Text = "Load Settings From Device"
        Me.cmdLoadCom1.UseVisualStyleBackColor = False
        '
        'txtDataReceivedCom1
        '
        Me.txtDataReceivedCom1.BackColor = System.Drawing.SystemColors.Window
        Me.txtDataReceivedCom1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDataReceivedCom1.Location = New System.Drawing.Point(316, 227)
        Me.txtDataReceivedCom1.Multiline = True
        Me.txtDataReceivedCom1.Name = "txtDataReceivedCom1"
        Me.txtDataReceivedCom1.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtDataReceivedCom1.Size = New System.Drawing.Size(292, 108)
        Me.txtDataReceivedCom1.TabIndex = 100
        '
        'cmdDataReceiveCom1
        '
        Me.cmdDataReceiveCom1.BackColor = System.Drawing.SystemColors.Control
        Me.cmdDataReceiveCom1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDataReceiveCom1.Location = New System.Drawing.Point(518, 340)
        Me.cmdDataReceiveCom1.Name = "cmdDataReceiveCom1"
        Me.cmdDataReceiveCom1.Size = New System.Drawing.Size(90, 25)
        Me.cmdDataReceiveCom1.TabIndex = 6
        Me.cmdDataReceiveCom1.Text = "Receive Data"
        Me.cmdDataReceiveCom1.UseVisualStyleBackColor = False
        '
        'cmdSendDataCom1
        '
        Me.cmdSendDataCom1.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSendDataCom1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSendDataCom1.Location = New System.Drawing.Point(218, 340)
        Me.cmdSendDataCom1.Name = "cmdSendDataCom1"
        Me.cmdSendDataCom1.Size = New System.Drawing.Size(90, 25)
        Me.cmdSendDataCom1.TabIndex = 4
        Me.cmdSendDataCom1.Text = "Send Data"
        Me.cmdSendDataCom1.UseVisualStyleBackColor = False
        '
        'txtDataSentCom1
        '
        Me.txtDataSentCom1.BackColor = System.Drawing.SystemColors.Window
        Me.txtDataSentCom1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDataSentCom1.Location = New System.Drawing.Point(16, 227)
        Me.txtDataSentCom1.MaxLength = 1000
        Me.txtDataSentCom1.Multiline = True
        Me.txtDataSentCom1.Name = "txtDataSentCom1"
        Me.txtDataSentCom1.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtDataSentCom1.Size = New System.Drawing.Size(292, 108)
        Me.txtDataSentCom1.TabIndex = 3
        '
        'FrameCom1PortSettings
        '
        Me.FrameCom1PortSettings.BackColor = System.Drawing.SystemColors.Control
        Me.FrameCom1PortSettings.Controls.Add(Me.Label3)
        Me.FrameCom1PortSettings.Controls.Add(Me.txtReceiveLengthCom1)
        Me.FrameCom1PortSettings.Controls.Add(Me.Label4)
        Me.FrameCom1PortSettings.Controls.Add(Me.cmbParityCom1)
        Me.FrameCom1PortSettings.Controls.Add(Me.cmbStopBitsCom1)
        Me.FrameCom1PortSettings.Controls.Add(Me.cmbBitRateCom1)
        Me.FrameCom1PortSettings.Controls.Add(Me.txtWordLengthCom1)
        Me.FrameCom1PortSettings.Controls.Add(Me.lblWordLengthUnitsCom1)
        Me.FrameCom1PortSettings.Controls.Add(Me.lblParityCom1)
        Me.FrameCom1PortSettings.Controls.Add(Me.lblStopBitsCom1)
        Me.FrameCom1PortSettings.Controls.Add(Me.lblBitRateCom1)
        Me.FrameCom1PortSettings.Controls.Add(Me.lblBitRateUnitsCom1)
        Me.FrameCom1PortSettings.Controls.Add(Me.lblStopBitsUnitsCom1)
        Me.FrameCom1PortSettings.Controls.Add(Me.lblWordLengthCom1)
        Me.FrameCom1PortSettings.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FrameCom1PortSettings.Location = New System.Drawing.Point(16, 19)
        Me.FrameCom1PortSettings.Name = "FrameCom1PortSettings"
        Me.FrameCom1PortSettings.Size = New System.Drawing.Size(260, 153)
        Me.FrameCom1PortSettings.TabIndex = 0
        Me.FrameCom1PortSettings.TabStop = False
        Me.FrameCom1PortSettings.Text = "Port Settings"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(197, 119)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(33, 13)
        Me.Label3.TabIndex = 294
        Me.Label3.Text = "Bytes"
        '
        'txtReceiveLengthCom1
        '
        Me.txtReceiveLengthCom1.Location = New System.Drawing.Point(102, 115)
        Me.txtReceiveLengthCom1.Name = "txtReceiveLengthCom1"
        Me.txtReceiveLengthCom1.Size = New System.Drawing.Size(90, 20)
        Me.txtReceiveLengthCom1.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 119)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(83, 13)
        Me.Label4.TabIndex = 292
        Me.Label4.Text = "Receive Length"
        '
        'cmbParityCom1
        '
        Me.cmbParityCom1.BackColor = System.Drawing.SystemColors.Window
        Me.cmbParityCom1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbParityCom1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbParityCom1.Items.AddRange(New Object() {"None", "Odd", "Even", "Mark"})
        Me.cmbParityCom1.Location = New System.Drawing.Point(102, 16)
        Me.cmbParityCom1.Name = "cmbParityCom1"
        Me.cmbParityCom1.Size = New System.Drawing.Size(90, 21)
        Me.cmbParityCom1.TabIndex = 0
        '
        'cmbStopBitsCom1
        '
        Me.cmbStopBitsCom1.BackColor = System.Drawing.SystemColors.Window
        Me.cmbStopBitsCom1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbStopBitsCom1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbStopBitsCom1.Items.AddRange(New Object() {"1", "1.5", "2"})
        Me.cmbStopBitsCom1.Location = New System.Drawing.Point(102, 40)
        Me.cmbStopBitsCom1.Name = "cmbStopBitsCom1"
        Me.cmbStopBitsCom1.Size = New System.Drawing.Size(90, 21)
        Me.cmbStopBitsCom1.TabIndex = 1
        '
        'cmbBitRateCom1
        '
        Me.cmbBitRateCom1.BackColor = System.Drawing.SystemColors.Window
        Me.cmbBitRateCom1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbBitRateCom1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbBitRateCom1.Items.AddRange(New Object() {"110", "300", "600", "1200", "2400", "4800", "9600", "14400", "19200", "38400", "56000", "57600", "115200"})
        Me.cmbBitRateCom1.Location = New System.Drawing.Point(102, 65)
        Me.cmbBitRateCom1.Name = "cmbBitRateCom1"
        Me.cmbBitRateCom1.Size = New System.Drawing.Size(90, 21)
        Me.cmbBitRateCom1.TabIndex = 2
        '
        'txtWordLengthCom1
        '
        Me.txtWordLengthCom1.BackColor = System.Drawing.SystemColors.Window
        Me.txtWordLengthCom1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtWordLengthCom1.Location = New System.Drawing.Point(102, 90)
        Me.txtWordLengthCom1.Name = "txtWordLengthCom1"
        Me.txtWordLengthCom1.Size = New System.Drawing.Size(90, 20)
        Me.txtWordLengthCom1.TabIndex = 3
        '
        'lblWordLengthUnitsCom1
        '
        Me.lblWordLengthUnitsCom1.BackColor = System.Drawing.SystemColors.Control
        Me.lblWordLengthUnitsCom1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblWordLengthUnitsCom1.Location = New System.Drawing.Point(197, 92)
        Me.lblWordLengthUnitsCom1.Name = "lblWordLengthUnitsCom1"
        Me.lblWordLengthUnitsCom1.Size = New System.Drawing.Size(58, 17)
        Me.lblWordLengthUnitsCom1.TabIndex = 289
        Me.lblWordLengthUnitsCom1.Text = "Bit Length"
        '
        'lblParityCom1
        '
        Me.lblParityCom1.BackColor = System.Drawing.SystemColors.Control
        Me.lblParityCom1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblParityCom1.Location = New System.Drawing.Point(45, 18)
        Me.lblParityCom1.Name = "lblParityCom1"
        Me.lblParityCom1.Size = New System.Drawing.Size(50, 17)
        Me.lblParityCom1.TabIndex = 114
        Me.lblParityCom1.Text = "Parity"
        Me.lblParityCom1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblStopBitsCom1
        '
        Me.lblStopBitsCom1.BackColor = System.Drawing.SystemColors.Control
        Me.lblStopBitsCom1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblStopBitsCom1.Location = New System.Drawing.Point(45, 42)
        Me.lblStopBitsCom1.Name = "lblStopBitsCom1"
        Me.lblStopBitsCom1.Size = New System.Drawing.Size(50, 17)
        Me.lblStopBitsCom1.TabIndex = 113
        Me.lblStopBitsCom1.Text = "Stop Bits"
        Me.lblStopBitsCom1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblBitRateCom1
        '
        Me.lblBitRateCom1.BackColor = System.Drawing.SystemColors.Control
        Me.lblBitRateCom1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBitRateCom1.Location = New System.Drawing.Point(45, 67)
        Me.lblBitRateCom1.Name = "lblBitRateCom1"
        Me.lblBitRateCom1.Size = New System.Drawing.Size(50, 17)
        Me.lblBitRateCom1.TabIndex = 112
        Me.lblBitRateCom1.Text = "Bit Rate"
        Me.lblBitRateCom1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblBitRateUnitsCom1
        '
        Me.lblBitRateUnitsCom1.BackColor = System.Drawing.SystemColors.Control
        Me.lblBitRateUnitsCom1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBitRateUnitsCom1.Location = New System.Drawing.Point(197, 67)
        Me.lblBitRateUnitsCom1.Name = "lblBitRateUnitsCom1"
        Me.lblBitRateUnitsCom1.Size = New System.Drawing.Size(41, 17)
        Me.lblBitRateUnitsCom1.TabIndex = 111
        Me.lblBitRateUnitsCom1.Text = "Bits/Sec"
        '
        'lblStopBitsUnitsCom1
        '
        Me.lblStopBitsUnitsCom1.BackColor = System.Drawing.SystemColors.Control
        Me.lblStopBitsUnitsCom1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblStopBitsUnitsCom1.Location = New System.Drawing.Point(197, 42)
        Me.lblStopBitsUnitsCom1.Name = "lblStopBitsUnitsCom1"
        Me.lblStopBitsUnitsCom1.Size = New System.Drawing.Size(25, 17)
        Me.lblStopBitsUnitsCom1.TabIndex = 110
        Me.lblStopBitsUnitsCom1.Text = "Bits"
        '
        'lblWordLengthCom1
        '
        Me.lblWordLengthCom1.BackColor = System.Drawing.SystemColors.Control
        Me.lblWordLengthCom1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblWordLengthCom1.Location = New System.Drawing.Point(45, 92)
        Me.lblWordLengthCom1.Name = "lblWordLengthCom1"
        Me.lblWordLengthCom1.Size = New System.Drawing.Size(50, 17)
        Me.lblWordLengthCom1.TabIndex = 109
        Me.lblWordLengthCom1.Text = "Word Length "
        Me.lblWordLengthCom1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'tabOptions_Page2
        '
        Me.tabOptions_Page2.Controls.Add(Me.lblReadTimeCom2)
        Me.tabOptions_Page2.Controls.Add(Me.txtReadTimeCom2)
        Me.tabOptions_Page2.Controls.Add(Me.frameReceiveCom2)
        Me.tabOptions_Page2.Controls.Add(Me.frameSendCom2)
        Me.tabOptions_Page2.Controls.Add(Me.cmdLoadCom2)
        Me.tabOptions_Page2.Controls.Add(Me.cmdSendSettingsCom2)
        Me.tabOptions_Page2.Controls.Add(Me.txtDataSentCom2)
        Me.tabOptions_Page2.Controls.Add(Me.txtDataReceivedCom2)
        Me.tabOptions_Page2.Controls.Add(Me.FrameCom2PortSettings)
        Me.tabOptions_Page2.Controls.Add(Me.cmdSendDataCom2)
        Me.tabOptions_Page2.Controls.Add(Me.cmdDataReceiveCom2)
        Me.tabOptions_Page2.Location = New System.Drawing.Point(4, 22)
        Me.tabOptions_Page2.Name = "tabOptions_Page2"
        Me.tabOptions_Page2.Size = New System.Drawing.Size(634, 388)
        Me.tabOptions_Page2.TabIndex = 1
        Me.tabOptions_Page2.Text = "COM 2"
        '
        'lblReadTimeCom2
        '
        Me.lblReadTimeCom2.BackColor = System.Drawing.SystemColors.Control
        Me.lblReadTimeCom2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblReadTimeCom2.Location = New System.Drawing.Point(332, 344)
        Me.lblReadTimeCom2.Name = "lblReadTimeCom2"
        Me.lblReadTimeCom2.Size = New System.Drawing.Size(114, 17)
        Me.lblReadTimeCom2.TabIndex = 117
        Me.lblReadTimeCom2.Text = "Data Read Time (Sec)"
        Me.lblReadTimeCom2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtReadTimeCom2
        '
        Me.txtReadTimeCom2.BackColor = System.Drawing.SystemColors.Window
        Me.txtReadTimeCom2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtReadTimeCom2.Location = New System.Drawing.Point(453, 340)
        Me.txtReadTimeCom2.Name = "txtReadTimeCom2"
        Me.txtReadTimeCom2.Size = New System.Drawing.Size(58, 20)
        Me.txtReadTimeCom2.TabIndex = 5
        Me.txtReadTimeCom2.Text = "10"
        '
        'frameReceiveCom2
        '
        Me.frameReceiveCom2.BackColor = System.Drawing.SystemColors.Control
        Me.frameReceiveCom2.Controls.Add(Me.optXMLReceiveCom2)
        Me.frameReceiveCom2.Controls.Add(Me.optDataReceiveCom2)
        Me.frameReceiveCom2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.frameReceiveCom2.Location = New System.Drawing.Point(316, 191)
        Me.frameReceiveCom2.Name = "frameReceiveCom2"
        Me.frameReceiveCom2.Size = New System.Drawing.Size(292, 33)
        Me.frameReceiveCom2.TabIndex = 15
        Me.frameReceiveCom2.TabStop = False
        Me.frameReceiveCom2.Text = "Receive Signal Type"
        '
        'optXMLReceiveCom2
        '
        Me.optXMLReceiveCom2.BackColor = System.Drawing.SystemColors.Control
        Me.optXMLReceiveCom2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optXMLReceiveCom2.Location = New System.Drawing.Point(113, 8)
        Me.optXMLReceiveCom2.Name = "optXMLReceiveCom2"
        Me.optXMLReceiveCom2.Size = New System.Drawing.Size(98, 17)
        Me.optXMLReceiveCom2.TabIndex = 17
        Me.optXMLReceiveCom2.Text = "XML Instruction"
        Me.optXMLReceiveCom2.UseVisualStyleBackColor = False
        Me.optXMLReceiveCom2.Visible = False
        '
        'optDataReceiveCom2
        '
        Me.optDataReceiveCom2.BackColor = System.Drawing.SystemColors.Control
        Me.optDataReceiveCom2.Checked = True
        Me.optDataReceiveCom2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optDataReceiveCom2.Location = New System.Drawing.Point(218, 8)
        Me.optDataReceiveCom2.Name = "optDataReceiveCom2"
        Me.optDataReceiveCom2.Size = New System.Drawing.Size(50, 17)
        Me.optDataReceiveCom2.TabIndex = 16
        Me.optDataReceiveCom2.TabStop = True
        Me.optDataReceiveCom2.Text = "Data"
        Me.optDataReceiveCom2.UseVisualStyleBackColor = False
        '
        'frameSendCom2
        '
        Me.frameSendCom2.BackColor = System.Drawing.SystemColors.Control
        Me.frameSendCom2.Controls.Add(Me.optDataSendCom2)
        Me.frameSendCom2.Controls.Add(Me.optXMLSendCom2)
        Me.frameSendCom2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.frameSendCom2.Location = New System.Drawing.Point(16, 191)
        Me.frameSendCom2.Name = "frameSendCom2"
        Me.frameSendCom2.Size = New System.Drawing.Size(292, 33)
        Me.frameSendCom2.TabIndex = 27
        Me.frameSendCom2.TabStop = False
        Me.frameSendCom2.Text = "Send Signal Type"
        '
        'optDataSendCom2
        '
        Me.optDataSendCom2.BackColor = System.Drawing.SystemColors.Control
        Me.optDataSendCom2.Checked = True
        Me.optDataSendCom2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optDataSendCom2.Location = New System.Drawing.Point(210, 8)
        Me.optDataSendCom2.Name = "optDataSendCom2"
        Me.optDataSendCom2.Size = New System.Drawing.Size(50, 17)
        Me.optDataSendCom2.TabIndex = 29
        Me.optDataSendCom2.TabStop = True
        Me.optDataSendCom2.Text = "Data"
        Me.optDataSendCom2.UseVisualStyleBackColor = False
        '
        'optXMLSendCom2
        '
        Me.optXMLSendCom2.BackColor = System.Drawing.SystemColors.Control
        Me.optXMLSendCom2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optXMLSendCom2.Location = New System.Drawing.Point(105, 8)
        Me.optXMLSendCom2.Name = "optXMLSendCom2"
        Me.optXMLSendCom2.Size = New System.Drawing.Size(98, 17)
        Me.optXMLSendCom2.TabIndex = 28
        Me.optXMLSendCom2.Text = "XML Instruction"
        Me.optXMLSendCom2.UseVisualStyleBackColor = False
        Me.optXMLSendCom2.Visible = False
        '
        'cmdLoadCom2
        '
        Me.cmdLoadCom2.BackColor = System.Drawing.SystemColors.Control
        Me.cmdLoadCom2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdLoadCom2.Location = New System.Drawing.Point(477, 19)
        Me.cmdLoadCom2.Name = "cmdLoadCom2"
        Me.cmdLoadCom2.Size = New System.Drawing.Size(147, 25)
        Me.cmdLoadCom2.TabIndex = 1
        Me.cmdLoadCom2.Text = "Load Settings From Device"
        Me.cmdLoadCom2.UseVisualStyleBackColor = False
        '
        'cmdSendSettingsCom2
        '
        Me.cmdSendSettingsCom2.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSendSettingsCom2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSendSettingsCom2.Location = New System.Drawing.Point(477, 51)
        Me.cmdSendSettingsCom2.Name = "cmdSendSettingsCom2"
        Me.cmdSendSettingsCom2.Size = New System.Drawing.Size(147, 25)
        Me.cmdSendSettingsCom2.TabIndex = 2
        Me.cmdSendSettingsCom2.Text = "Send Settings To Device"
        Me.cmdSendSettingsCom2.UseVisualStyleBackColor = False
        '
        'txtDataSentCom2
        '
        Me.txtDataSentCom2.BackColor = System.Drawing.SystemColors.Window
        Me.txtDataSentCom2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDataSentCom2.Location = New System.Drawing.Point(16, 227)
        Me.txtDataSentCom2.MaxLength = 1000
        Me.txtDataSentCom2.Multiline = True
        Me.txtDataSentCom2.Name = "txtDataSentCom2"
        Me.txtDataSentCom2.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtDataSentCom2.Size = New System.Drawing.Size(292, 108)
        Me.txtDataSentCom2.TabIndex = 3
        '
        'txtDataReceivedCom2
        '
        Me.txtDataReceivedCom2.BackColor = System.Drawing.SystemColors.Window
        Me.txtDataReceivedCom2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDataReceivedCom2.Location = New System.Drawing.Point(316, 227)
        Me.txtDataReceivedCom2.Multiline = True
        Me.txtDataReceivedCom2.Name = "txtDataReceivedCom2"
        Me.txtDataReceivedCom2.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtDataReceivedCom2.Size = New System.Drawing.Size(292, 108)
        Me.txtDataReceivedCom2.TabIndex = 43
        '
        'FrameCom2PortSettings
        '
        Me.FrameCom2PortSettings.BackColor = System.Drawing.SystemColors.Control
        Me.FrameCom2PortSettings.Controls.Add(Me.Label2)
        Me.FrameCom2PortSettings.Controls.Add(Me.txtReceiveLengthCom2)
        Me.FrameCom2PortSettings.Controls.Add(Me.Label1)
        Me.FrameCom2PortSettings.Controls.Add(Me.cmbStopBitsCom2)
        Me.FrameCom2PortSettings.Controls.Add(Me.cmbParityCom2)
        Me.FrameCom2PortSettings.Controls.Add(Me.cmbBitRateCom2)
        Me.FrameCom2PortSettings.Controls.Add(Me.txtWordLengthCom2)
        Me.FrameCom2PortSettings.Controls.Add(Me.lblWordLengthUnitsCom2)
        Me.FrameCom2PortSettings.Controls.Add(Me.lblBitRateCom2)
        Me.FrameCom2PortSettings.Controls.Add(Me.lblStopBitsCom2)
        Me.FrameCom2PortSettings.Controls.Add(Me.lblParityCom2)
        Me.FrameCom2PortSettings.Controls.Add(Me.lblBitRateUnitsCom2)
        Me.FrameCom2PortSettings.Controls.Add(Me.lblStopBitsUnitsCom2)
        Me.FrameCom2PortSettings.Controls.Add(Me.lblWordLengthCom2)
        Me.FrameCom2PortSettings.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FrameCom2PortSettings.Location = New System.Drawing.Point(16, 19)
        Me.FrameCom2PortSettings.Name = "FrameCom2PortSettings"
        Me.FrameCom2PortSettings.Size = New System.Drawing.Size(260, 153)
        Me.FrameCom2PortSettings.TabIndex = 0
        Me.FrameCom2PortSettings.TabStop = False
        Me.FrameCom2PortSettings.Text = "Port Settings"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(197, 119)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(33, 13)
        Me.Label2.TabIndex = 291
        Me.Label2.Text = "Bytes"
        '
        'txtReceiveLengthCom2
        '
        Me.txtReceiveLengthCom2.Location = New System.Drawing.Point(102, 115)
        Me.txtReceiveLengthCom2.Name = "txtReceiveLengthCom2"
        Me.txtReceiveLengthCom2.Size = New System.Drawing.Size(90, 20)
        Me.txtReceiveLengthCom2.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 119)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(83, 13)
        Me.Label1.TabIndex = 289
        Me.Label1.Text = "Receive Length"
        '
        'cmbStopBitsCom2
        '
        Me.cmbStopBitsCom2.BackColor = System.Drawing.SystemColors.Window
        Me.cmbStopBitsCom2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbStopBitsCom2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbStopBitsCom2.Items.AddRange(New Object() {"1", "1.5", "2"})
        Me.cmbStopBitsCom2.Location = New System.Drawing.Point(102, 40)
        Me.cmbStopBitsCom2.Name = "cmbStopBitsCom2"
        Me.cmbStopBitsCom2.Size = New System.Drawing.Size(90, 21)
        Me.cmbStopBitsCom2.TabIndex = 1
        '
        'cmbParityCom2
        '
        Me.cmbParityCom2.BackColor = System.Drawing.SystemColors.Window
        Me.cmbParityCom2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbParityCom2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbParityCom2.Items.AddRange(New Object() {"None", "Odd", "Even", "Mark"})
        Me.cmbParityCom2.Location = New System.Drawing.Point(102, 16)
        Me.cmbParityCom2.Name = "cmbParityCom2"
        Me.cmbParityCom2.Size = New System.Drawing.Size(90, 21)
        Me.cmbParityCom2.TabIndex = 0
        '
        'cmbBitRateCom2
        '
        Me.cmbBitRateCom2.BackColor = System.Drawing.SystemColors.Window
        Me.cmbBitRateCom2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbBitRateCom2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbBitRateCom2.Items.AddRange(New Object() {"110", "300", "600", "1200", "2400", "4800", "9600", "14400", "19200", "38400", "56000", "57600", "115200"})
        Me.cmbBitRateCom2.Location = New System.Drawing.Point(102, 65)
        Me.cmbBitRateCom2.Name = "cmbBitRateCom2"
        Me.cmbBitRateCom2.Size = New System.Drawing.Size(90, 21)
        Me.cmbBitRateCom2.TabIndex = 2
        '
        'txtWordLengthCom2
        '
        Me.txtWordLengthCom2.BackColor = System.Drawing.SystemColors.Window
        Me.txtWordLengthCom2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtWordLengthCom2.Location = New System.Drawing.Point(102, 90)
        Me.txtWordLengthCom2.Name = "txtWordLengthCom2"
        Me.txtWordLengthCom2.Size = New System.Drawing.Size(90, 20)
        Me.txtWordLengthCom2.TabIndex = 3
        '
        'lblWordLengthUnitsCom2
        '
        Me.lblWordLengthUnitsCom2.BackColor = System.Drawing.SystemColors.Control
        Me.lblWordLengthUnitsCom2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblWordLengthUnitsCom2.Location = New System.Drawing.Point(197, 92)
        Me.lblWordLengthUnitsCom2.Name = "lblWordLengthUnitsCom2"
        Me.lblWordLengthUnitsCom2.Size = New System.Drawing.Size(58, 17)
        Me.lblWordLengthUnitsCom2.TabIndex = 288
        Me.lblWordLengthUnitsCom2.Text = "Bit Length"
        '
        'lblBitRateCom2
        '
        Me.lblBitRateCom2.BackColor = System.Drawing.SystemColors.Control
        Me.lblBitRateCom2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBitRateCom2.Location = New System.Drawing.Point(45, 67)
        Me.lblBitRateCom2.Name = "lblBitRateCom2"
        Me.lblBitRateCom2.Size = New System.Drawing.Size(50, 17)
        Me.lblBitRateCom2.TabIndex = 54
        Me.lblBitRateCom2.Text = "Bit Rate"
        Me.lblBitRateCom2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblStopBitsCom2
        '
        Me.lblStopBitsCom2.BackColor = System.Drawing.SystemColors.Control
        Me.lblStopBitsCom2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblStopBitsCom2.Location = New System.Drawing.Point(45, 42)
        Me.lblStopBitsCom2.Name = "lblStopBitsCom2"
        Me.lblStopBitsCom2.Size = New System.Drawing.Size(50, 17)
        Me.lblStopBitsCom2.TabIndex = 53
        Me.lblStopBitsCom2.Text = "Stop Bits"
        Me.lblStopBitsCom2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblParityCom2
        '
        Me.lblParityCom2.BackColor = System.Drawing.SystemColors.Control
        Me.lblParityCom2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblParityCom2.Location = New System.Drawing.Point(45, 18)
        Me.lblParityCom2.Name = "lblParityCom2"
        Me.lblParityCom2.Size = New System.Drawing.Size(50, 17)
        Me.lblParityCom2.TabIndex = 52
        Me.lblParityCom2.Text = "Parity"
        Me.lblParityCom2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblBitRateUnitsCom2
        '
        Me.lblBitRateUnitsCom2.BackColor = System.Drawing.SystemColors.Control
        Me.lblBitRateUnitsCom2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBitRateUnitsCom2.Location = New System.Drawing.Point(197, 67)
        Me.lblBitRateUnitsCom2.Name = "lblBitRateUnitsCom2"
        Me.lblBitRateUnitsCom2.Size = New System.Drawing.Size(41, 17)
        Me.lblBitRateUnitsCom2.TabIndex = 51
        Me.lblBitRateUnitsCom2.Text = "Bits/Sec"
        '
        'lblStopBitsUnitsCom2
        '
        Me.lblStopBitsUnitsCom2.BackColor = System.Drawing.SystemColors.Control
        Me.lblStopBitsUnitsCom2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblStopBitsUnitsCom2.Location = New System.Drawing.Point(197, 42)
        Me.lblStopBitsUnitsCom2.Name = "lblStopBitsUnitsCom2"
        Me.lblStopBitsUnitsCom2.Size = New System.Drawing.Size(25, 17)
        Me.lblStopBitsUnitsCom2.TabIndex = 50
        Me.lblStopBitsUnitsCom2.Text = "Bits"
        '
        'lblWordLengthCom2
        '
        Me.lblWordLengthCom2.BackColor = System.Drawing.SystemColors.Control
        Me.lblWordLengthCom2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblWordLengthCom2.Location = New System.Drawing.Point(45, 92)
        Me.lblWordLengthCom2.Name = "lblWordLengthCom2"
        Me.lblWordLengthCom2.Size = New System.Drawing.Size(50, 17)
        Me.lblWordLengthCom2.TabIndex = 49
        Me.lblWordLengthCom2.Text = "Word Length "
        Me.lblWordLengthCom2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmdSendDataCom2
        '
        Me.cmdSendDataCom2.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSendDataCom2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSendDataCom2.Location = New System.Drawing.Point(218, 340)
        Me.cmdSendDataCom2.Name = "cmdSendDataCom2"
        Me.cmdSendDataCom2.Size = New System.Drawing.Size(90, 25)
        Me.cmdSendDataCom2.TabIndex = 4
        Me.cmdSendDataCom2.Text = "Send Data"
        Me.cmdSendDataCom2.UseVisualStyleBackColor = False
        '
        'cmdDataReceiveCom2
        '
        Me.cmdDataReceiveCom2.BackColor = System.Drawing.SystemColors.Control
        Me.cmdDataReceiveCom2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDataReceiveCom2.Location = New System.Drawing.Point(518, 340)
        Me.cmdDataReceiveCom2.Name = "cmdDataReceiveCom2"
        Me.cmdDataReceiveCom2.Size = New System.Drawing.Size(90, 25)
        Me.cmdDataReceiveCom2.TabIndex = 6
        Me.cmdDataReceiveCom2.Text = "Receive Data"
        Me.cmdDataReceiveCom2.UseVisualStyleBackColor = False
        '
        'tabOptions_Page3
        '
        Me.tabOptions_Page3.Controls.Add(Me.lblReadTimePCISer)
        Me.tabOptions_Page3.Controls.Add(Me.txtReadTimePCISer)
        Me.tabOptions_Page3.Controls.Add(Me.frameReceivePCISer)
        Me.tabOptions_Page3.Controls.Add(Me.frameSendPCISer)
        Me.tabOptions_Page3.Controls.Add(Me.cmdSendSettingsPCISer)
        Me.tabOptions_Page3.Controls.Add(Me.cmdLoadPCISer)
        Me.tabOptions_Page3.Controls.Add(Me.txtDataReceivedPCISer)
        Me.tabOptions_Page3.Controls.Add(Me.framePciProtocol)
        Me.tabOptions_Page3.Controls.Add(Me.FramePciResponse)
        Me.tabOptions_Page3.Controls.Add(Me.txtDataSentPCISer)
        Me.tabOptions_Page3.Controls.Add(Me.cmdSendDataPCISer)
        Me.tabOptions_Page3.Controls.Add(Me.cmdDataReceivePCISer)
        Me.tabOptions_Page3.Controls.Add(Me.txtSaveSerialPCISer)
        Me.tabOptions_Page3.Location = New System.Drawing.Point(4, 22)
        Me.tabOptions_Page3.Name = "tabOptions_Page3"
        Me.tabOptions_Page3.Size = New System.Drawing.Size(634, 388)
        Me.tabOptions_Page3.TabIndex = 3
        Me.tabOptions_Page3.Text = "PCI Serial"
        '
        'lblReadTimePCISer
        '
        Me.lblReadTimePCISer.BackColor = System.Drawing.SystemColors.Control
        Me.lblReadTimePCISer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblReadTimePCISer.Location = New System.Drawing.Point(332, 344)
        Me.lblReadTimePCISer.Name = "lblReadTimePCISer"
        Me.lblReadTimePCISer.Size = New System.Drawing.Size(114, 17)
        Me.lblReadTimePCISer.TabIndex = 118
        Me.lblReadTimePCISer.Text = "Data Read Time (Sec)"
        Me.lblReadTimePCISer.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtReadTimePCISer
        '
        Me.txtReadTimePCISer.BackColor = System.Drawing.SystemColors.Window
        Me.txtReadTimePCISer.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtReadTimePCISer.Location = New System.Drawing.Point(453, 340)
        Me.txtReadTimePCISer.Name = "txtReadTimePCISer"
        Me.txtReadTimePCISer.Size = New System.Drawing.Size(58, 20)
        Me.txtReadTimePCISer.TabIndex = 6
        Me.txtReadTimePCISer.Text = "10"
        '
        'frameReceivePCISer
        '
        Me.frameReceivePCISer.BackColor = System.Drawing.SystemColors.Control
        Me.frameReceivePCISer.Controls.Add(Me.optDataReceivePCISer)
        Me.frameReceivePCISer.Controls.Add(Me.optXMLReceivePCISer)
        Me.frameReceivePCISer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.frameReceivePCISer.Location = New System.Drawing.Point(316, 191)
        Me.frameReceivePCISer.Name = "frameReceivePCISer"
        Me.frameReceivePCISer.Size = New System.Drawing.Size(292, 33)
        Me.frameReceivePCISer.TabIndex = 12
        Me.frameReceivePCISer.TabStop = False
        Me.frameReceivePCISer.Text = "Receive Signal Type"
        '
        'optDataReceivePCISer
        '
        Me.optDataReceivePCISer.BackColor = System.Drawing.SystemColors.Control
        Me.optDataReceivePCISer.Checked = True
        Me.optDataReceivePCISer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optDataReceivePCISer.Location = New System.Drawing.Point(218, 8)
        Me.optDataReceivePCISer.Name = "optDataReceivePCISer"
        Me.optDataReceivePCISer.Size = New System.Drawing.Size(50, 17)
        Me.optDataReceivePCISer.TabIndex = 14
        Me.optDataReceivePCISer.TabStop = True
        Me.optDataReceivePCISer.Text = "Data"
        Me.optDataReceivePCISer.UseVisualStyleBackColor = False
        '
        'optXMLReceivePCISer
        '
        Me.optXMLReceivePCISer.BackColor = System.Drawing.SystemColors.Control
        Me.optXMLReceivePCISer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optXMLReceivePCISer.Location = New System.Drawing.Point(113, 8)
        Me.optXMLReceivePCISer.Name = "optXMLReceivePCISer"
        Me.optXMLReceivePCISer.Size = New System.Drawing.Size(98, 17)
        Me.optXMLReceivePCISer.TabIndex = 13
        Me.optXMLReceivePCISer.Text = "XML Instruction"
        Me.optXMLReceivePCISer.UseVisualStyleBackColor = False
        Me.optXMLReceivePCISer.Visible = False
        '
        'frameSendPCISer
        '
        Me.frameSendPCISer.BackColor = System.Drawing.SystemColors.Control
        Me.frameSendPCISer.Controls.Add(Me.optDataSendPCISer)
        Me.frameSendPCISer.Controls.Add(Me.optXMLSendPCISer)
        Me.frameSendPCISer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.frameSendPCISer.Location = New System.Drawing.Point(16, 191)
        Me.frameSendPCISer.Name = "frameSendPCISer"
        Me.frameSendPCISer.Size = New System.Drawing.Size(292, 33)
        Me.frameSendPCISer.TabIndex = 21
        Me.frameSendPCISer.TabStop = False
        Me.frameSendPCISer.Text = "Send Signal Type"
        '
        'optDataSendPCISer
        '
        Me.optDataSendPCISer.BackColor = System.Drawing.SystemColors.Control
        Me.optDataSendPCISer.Checked = True
        Me.optDataSendPCISer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optDataSendPCISer.Location = New System.Drawing.Point(210, 8)
        Me.optDataSendPCISer.Name = "optDataSendPCISer"
        Me.optDataSendPCISer.Size = New System.Drawing.Size(50, 17)
        Me.optDataSendPCISer.TabIndex = 23
        Me.optDataSendPCISer.TabStop = True
        Me.optDataSendPCISer.Text = "Data"
        Me.optDataSendPCISer.UseVisualStyleBackColor = False
        '
        'optXMLSendPCISer
        '
        Me.optXMLSendPCISer.BackColor = System.Drawing.SystemColors.Control
        Me.optXMLSendPCISer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optXMLSendPCISer.Location = New System.Drawing.Point(105, 8)
        Me.optXMLSendPCISer.Name = "optXMLSendPCISer"
        Me.optXMLSendPCISer.Size = New System.Drawing.Size(98, 17)
        Me.optXMLSendPCISer.TabIndex = 22
        Me.optXMLSendPCISer.Text = "XML Instruction"
        Me.optXMLSendPCISer.UseVisualStyleBackColor = False
        Me.optXMLSendPCISer.Visible = False
        '
        'cmdSendSettingsPCISer
        '
        Me.cmdSendSettingsPCISer.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSendSettingsPCISer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSendSettingsPCISer.Location = New System.Drawing.Point(477, 81)
        Me.cmdSendSettingsPCISer.Name = "cmdSendSettingsPCISer"
        Me.cmdSendSettingsPCISer.Size = New System.Drawing.Size(147, 25)
        Me.cmdSendSettingsPCISer.TabIndex = 3
        Me.cmdSendSettingsPCISer.Text = "Send Settings To Device"
        Me.cmdSendSettingsPCISer.UseVisualStyleBackColor = False
        '
        'cmdLoadPCISer
        '
        Me.cmdLoadPCISer.BackColor = System.Drawing.SystemColors.Control
        Me.cmdLoadPCISer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdLoadPCISer.Location = New System.Drawing.Point(477, 45)
        Me.cmdLoadPCISer.Name = "cmdLoadPCISer"
        Me.cmdLoadPCISer.Size = New System.Drawing.Size(147, 25)
        Me.cmdLoadPCISer.TabIndex = 2
        Me.cmdLoadPCISer.Text = "Load Settings From Device"
        Me.cmdLoadPCISer.UseVisualStyleBackColor = False
        '
        'txtDataReceivedPCISer
        '
        Me.txtDataReceivedPCISer.BackColor = System.Drawing.SystemColors.Window
        Me.txtDataReceivedPCISer.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDataReceivedPCISer.Location = New System.Drawing.Point(316, 227)
        Me.txtDataReceivedPCISer.Multiline = True
        Me.txtDataReceivedPCISer.Name = "txtDataReceivedPCISer"
        Me.txtDataReceivedPCISer.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtDataReceivedPCISer.Size = New System.Drawing.Size(292, 108)
        Me.txtDataReceivedPCISer.TabIndex = 75
        '
        'framePciProtocol
        '
        Me.framePciProtocol.BackColor = System.Drawing.SystemColors.Control
        Me.framePciProtocol.Controls.Add(Me.optRS485)
        Me.framePciProtocol.Controls.Add(Me.optRS422)
        Me.framePciProtocol.Controls.Add(Me.optRS232)
        Me.framePciProtocol.ForeColor = System.Drawing.SystemColors.ControlText
        Me.framePciProtocol.Location = New System.Drawing.Point(16, 37)
        Me.framePciProtocol.Name = "framePciProtocol"
        Me.framePciProtocol.Size = New System.Drawing.Size(82, 82)
        Me.framePciProtocol.TabIndex = 0
        Me.framePciProtocol.TabStop = False
        Me.framePciProtocol.Text = "Protocol"
        '
        'optRS485
        '
        Me.optRS485.BackColor = System.Drawing.SystemColors.Control
        Me.optRS485.Checked = True
        Me.optRS485.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optRS485.Location = New System.Drawing.Point(8, 52)
        Me.optRS485.Name = "optRS485"
        Me.optRS485.Size = New System.Drawing.Size(58, 25)
        Me.optRS485.TabIndex = 2
        Me.optRS485.TabStop = True
        Me.optRS485.Text = "RS485"
        Me.optRS485.UseVisualStyleBackColor = False
        '
        'optRS422
        '
        Me.optRS422.BackColor = System.Drawing.SystemColors.Control
        Me.optRS422.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optRS422.Location = New System.Drawing.Point(8, 31)
        Me.optRS422.Name = "optRS422"
        Me.optRS422.Size = New System.Drawing.Size(66, 25)
        Me.optRS422.TabIndex = 1
        Me.optRS422.Text = "RS422"
        Me.optRS422.UseVisualStyleBackColor = False
        '
        'optRS232
        '
        Me.optRS232.BackColor = System.Drawing.SystemColors.Control
        Me.optRS232.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optRS232.Location = New System.Drawing.Point(8, 11)
        Me.optRS232.Name = "optRS232"
        Me.optRS232.Size = New System.Drawing.Size(58, 25)
        Me.optRS232.TabIndex = 0
        Me.optRS232.Text = "RS232"
        Me.optRS232.UseVisualStyleBackColor = False
        '
        'FramePciResponse
        '
        Me.FramePciResponse.BackColor = System.Drawing.SystemColors.Control
        Me.FramePciResponse.Controls.Add(Me.Label5)
        Me.FramePciResponse.Controls.Add(Me.txtReceiveLengthPCISer)
        Me.FramePciResponse.Controls.Add(Me.Label6)
        Me.FramePciResponse.Controls.Add(Me.cmbBitRatePCISer)
        Me.FramePciResponse.Controls.Add(Me.cmbParityPCISer)
        Me.FramePciResponse.Controls.Add(Me.cmbStopBitsPCISer)
        Me.FramePciResponse.Controls.Add(Me.txtSerialMaxTimePCISer)
        Me.FramePciResponse.Controls.Add(Me.txtSerialDelayPCISer)
        Me.FramePciResponse.Controls.Add(Me.txtSerialWordLengthPCISer)
        Me.FramePciResponse.Controls.Add(Me.lblParityPCISer)
        Me.FramePciResponse.Controls.Add(Me.lblStopBitsPCISer)
        Me.FramePciResponse.Controls.Add(Me.lblBitRatePCISer)
        Me.FramePciResponse.Controls.Add(Me.lblSerialMaxTimePCISer)
        Me.FramePciResponse.Controls.Add(Me.lblSerialDelayPCISer)
        Me.FramePciResponse.Controls.Add(Me.lblSerialMaxTimeUnitsPCISer)
        Me.FramePciResponse.Controls.Add(Me.lblSerialDelayUnitsPCISer)
        Me.FramePciResponse.Controls.Add(Me.lblBitRateUnitsPCISer)
        Me.FramePciResponse.Controls.Add(Me.lblStopBitsUnitsPCISer)
        Me.FramePciResponse.Controls.Add(Me.lblSerialWordLengthPCISer)
        Me.FramePciResponse.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FramePciResponse.Location = New System.Drawing.Point(105, 37)
        Me.FramePciResponse.Name = "FramePciResponse"
        Me.FramePciResponse.Size = New System.Drawing.Size(333, 116)
        Me.FramePciResponse.TabIndex = 1
        Me.FramePciResponse.TabStop = False
        Me.FramePciResponse.Text = "Port Settings"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(296, 93)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(33, 13)
        Me.Label5.TabIndex = 297
        Me.Label5.Text = "Bytes"
        '
        'txtReceiveLengthPCISer
        '
        Me.txtReceiveLengthPCISer.Location = New System.Drawing.Point(252, 89)
        Me.txtReceiveLengthPCISer.Name = "txtReceiveLengthPCISer"
        Me.txtReceiveLengthPCISer.Size = New System.Drawing.Size(40, 20)
        Me.txtReceiveLengthPCISer.TabIndex = 6
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(168, 93)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(83, 13)
        Me.Label6.TabIndex = 295
        Me.Label6.Text = "Receive Length"
        '
        'cmbBitRatePCISer
        '
        Me.cmbBitRatePCISer.BackColor = System.Drawing.SystemColors.Window
        Me.cmbBitRatePCISer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbBitRatePCISer.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbBitRatePCISer.Items.AddRange(New Object() {"110", "300", "600", "1200", "2400", "4800", "9600", "14400", "19200", "38400", "56000", "57600", "115200"})
        Me.cmbBitRatePCISer.Location = New System.Drawing.Point(65, 65)
        Me.cmbBitRatePCISer.Name = "cmbBitRatePCISer"
        Me.cmbBitRatePCISer.Size = New System.Drawing.Size(66, 21)
        Me.cmbBitRatePCISer.TabIndex = 2
        '
        'cmbParityPCISer
        '
        Me.cmbParityPCISer.BackColor = System.Drawing.SystemColors.Window
        Me.cmbParityPCISer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbParityPCISer.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbParityPCISer.Items.AddRange(New Object() {"None", "Odd", "Even", "Mark"})
        Me.cmbParityPCISer.Location = New System.Drawing.Point(65, 16)
        Me.cmbParityPCISer.Name = "cmbParityPCISer"
        Me.cmbParityPCISer.Size = New System.Drawing.Size(58, 21)
        Me.cmbParityPCISer.TabIndex = 0
        '
        'cmbStopBitsPCISer
        '
        Me.cmbStopBitsPCISer.BackColor = System.Drawing.SystemColors.Window
        Me.cmbStopBitsPCISer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbStopBitsPCISer.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbStopBitsPCISer.Items.AddRange(New Object() {"1", "1.5", "2"})
        Me.cmbStopBitsPCISer.Location = New System.Drawing.Point(65, 40)
        Me.cmbStopBitsPCISer.Name = "cmbStopBitsPCISer"
        Me.cmbStopBitsPCISer.Size = New System.Drawing.Size(58, 21)
        Me.cmbStopBitsPCISer.TabIndex = 1
        '
        'txtSerialMaxTimePCISer
        '
        Me.txtSerialMaxTimePCISer.BackColor = System.Drawing.SystemColors.Window
        Me.txtSerialMaxTimePCISer.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSerialMaxTimePCISer.Location = New System.Drawing.Point(251, 65)
        Me.txtSerialMaxTimePCISer.Name = "txtSerialMaxTimePCISer"
        Me.txtSerialMaxTimePCISer.Size = New System.Drawing.Size(40, 20)
        Me.txtSerialMaxTimePCISer.TabIndex = 5
        '
        'txtSerialDelayPCISer
        '
        Me.txtSerialDelayPCISer.BackColor = System.Drawing.SystemColors.Window
        Me.txtSerialDelayPCISer.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSerialDelayPCISer.Location = New System.Drawing.Point(251, 15)
        Me.txtSerialDelayPCISer.Name = "txtSerialDelayPCISer"
        Me.txtSerialDelayPCISer.Size = New System.Drawing.Size(40, 20)
        Me.txtSerialDelayPCISer.TabIndex = 3
        '
        'txtSerialWordLengthPCISer
        '
        Me.txtSerialWordLengthPCISer.BackColor = System.Drawing.SystemColors.Window
        Me.txtSerialWordLengthPCISer.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSerialWordLengthPCISer.Location = New System.Drawing.Point(251, 40)
        Me.txtSerialWordLengthPCISer.Name = "txtSerialWordLengthPCISer"
        Me.txtSerialWordLengthPCISer.Size = New System.Drawing.Size(40, 20)
        Me.txtSerialWordLengthPCISer.TabIndex = 4
        '
        'lblParityPCISer
        '
        Me.lblParityPCISer.BackColor = System.Drawing.SystemColors.Control
        Me.lblParityPCISer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblParityPCISer.Location = New System.Drawing.Point(8, 18)
        Me.lblParityPCISer.Name = "lblParityPCISer"
        Me.lblParityPCISer.Size = New System.Drawing.Size(50, 17)
        Me.lblParityPCISer.TabIndex = 95
        Me.lblParityPCISer.Text = "Parity"
        Me.lblParityPCISer.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblStopBitsPCISer
        '
        Me.lblStopBitsPCISer.BackColor = System.Drawing.SystemColors.Control
        Me.lblStopBitsPCISer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblStopBitsPCISer.Location = New System.Drawing.Point(8, 40)
        Me.lblStopBitsPCISer.Name = "lblStopBitsPCISer"
        Me.lblStopBitsPCISer.Size = New System.Drawing.Size(50, 17)
        Me.lblStopBitsPCISer.TabIndex = 94
        Me.lblStopBitsPCISer.Text = "Stop Bits"
        Me.lblStopBitsPCISer.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblBitRatePCISer
        '
        Me.lblBitRatePCISer.BackColor = System.Drawing.SystemColors.Control
        Me.lblBitRatePCISer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBitRatePCISer.Location = New System.Drawing.Point(8, 65)
        Me.lblBitRatePCISer.Name = "lblBitRatePCISer"
        Me.lblBitRatePCISer.Size = New System.Drawing.Size(50, 17)
        Me.lblBitRatePCISer.TabIndex = 93
        Me.lblBitRatePCISer.Text = "Bit Rate"
        Me.lblBitRatePCISer.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblSerialMaxTimePCISer
        '
        Me.lblSerialMaxTimePCISer.BackColor = System.Drawing.SystemColors.Control
        Me.lblSerialMaxTimePCISer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSerialMaxTimePCISer.Location = New System.Drawing.Point(196, 65)
        Me.lblSerialMaxTimePCISer.Name = "lblSerialMaxTimePCISer"
        Me.lblSerialMaxTimePCISer.Size = New System.Drawing.Size(55, 17)
        Me.lblSerialMaxTimePCISer.TabIndex = 92
        Me.lblSerialMaxTimePCISer.Text = "Max Time"
        Me.lblSerialMaxTimePCISer.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblSerialDelayPCISer
        '
        Me.lblSerialDelayPCISer.BackColor = System.Drawing.SystemColors.Control
        Me.lblSerialDelayPCISer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSerialDelayPCISer.Location = New System.Drawing.Point(196, 17)
        Me.lblSerialDelayPCISer.Name = "lblSerialDelayPCISer"
        Me.lblSerialDelayPCISer.Size = New System.Drawing.Size(55, 17)
        Me.lblSerialDelayPCISer.TabIndex = 91
        Me.lblSerialDelayPCISer.Text = "Delay "
        Me.lblSerialDelayPCISer.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblSerialMaxTimeUnitsPCISer
        '
        Me.lblSerialMaxTimeUnitsPCISer.BackColor = System.Drawing.SystemColors.Control
        Me.lblSerialMaxTimeUnitsPCISer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSerialMaxTimeUnitsPCISer.Location = New System.Drawing.Point(295, 65)
        Me.lblSerialMaxTimeUnitsPCISer.Name = "lblSerialMaxTimeUnitsPCISer"
        Me.lblSerialMaxTimeUnitsPCISer.Size = New System.Drawing.Size(36, 17)
        Me.lblSerialMaxTimeUnitsPCISer.TabIndex = 90
        Me.lblSerialMaxTimeUnitsPCISer.Text = "Sec"
        '
        'lblSerialDelayUnitsPCISer
        '
        Me.lblSerialDelayUnitsPCISer.BackColor = System.Drawing.SystemColors.Control
        Me.lblSerialDelayUnitsPCISer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSerialDelayUnitsPCISer.Location = New System.Drawing.Point(295, 17)
        Me.lblSerialDelayUnitsPCISer.Name = "lblSerialDelayUnitsPCISer"
        Me.lblSerialDelayUnitsPCISer.Size = New System.Drawing.Size(36, 17)
        Me.lblSerialDelayUnitsPCISer.TabIndex = 89
        Me.lblSerialDelayUnitsPCISer.Text = "Sec"
        '
        'lblBitRateUnitsPCISer
        '
        Me.lblBitRateUnitsPCISer.BackColor = System.Drawing.SystemColors.Control
        Me.lblBitRateUnitsPCISer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBitRateUnitsPCISer.Location = New System.Drawing.Point(129, 65)
        Me.lblBitRateUnitsPCISer.Name = "lblBitRateUnitsPCISer"
        Me.lblBitRateUnitsPCISer.Size = New System.Drawing.Size(50, 17)
        Me.lblBitRateUnitsPCISer.TabIndex = 88
        Me.lblBitRateUnitsPCISer.Text = "Bits/Sec"
        '
        'lblStopBitsUnitsPCISer
        '
        Me.lblStopBitsUnitsPCISer.BackColor = System.Drawing.SystemColors.Control
        Me.lblStopBitsUnitsPCISer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblStopBitsUnitsPCISer.Location = New System.Drawing.Point(129, 40)
        Me.lblStopBitsUnitsPCISer.Name = "lblStopBitsUnitsPCISer"
        Me.lblStopBitsUnitsPCISer.Size = New System.Drawing.Size(25, 17)
        Me.lblStopBitsUnitsPCISer.TabIndex = 87
        Me.lblStopBitsUnitsPCISer.Text = "Bits"
        '
        'lblSerialWordLengthPCISer
        '
        Me.lblSerialWordLengthPCISer.BackColor = System.Drawing.SystemColors.Control
        Me.lblSerialWordLengthPCISer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSerialWordLengthPCISer.Location = New System.Drawing.Point(182, 40)
        Me.lblSerialWordLengthPCISer.Name = "lblSerialWordLengthPCISer"
        Me.lblSerialWordLengthPCISer.Size = New System.Drawing.Size(69, 17)
        Me.lblSerialWordLengthPCISer.TabIndex = 86
        Me.lblSerialWordLengthPCISer.Text = "Word Length "
        Me.lblSerialWordLengthPCISer.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtDataSentPCISer
        '
        Me.txtDataSentPCISer.BackColor = System.Drawing.SystemColors.Window
        Me.txtDataSentPCISer.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDataSentPCISer.Location = New System.Drawing.Point(16, 227)
        Me.txtDataSentPCISer.MaxLength = 1000
        Me.txtDataSentPCISer.Multiline = True
        Me.txtDataSentPCISer.Name = "txtDataSentPCISer"
        Me.txtDataSentPCISer.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtDataSentPCISer.Size = New System.Drawing.Size(292, 108)
        Me.txtDataSentPCISer.TabIndex = 4
        '
        'cmdSendDataPCISer
        '
        Me.cmdSendDataPCISer.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSendDataPCISer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSendDataPCISer.Location = New System.Drawing.Point(218, 340)
        Me.cmdSendDataPCISer.Name = "cmdSendDataPCISer"
        Me.cmdSendDataPCISer.Size = New System.Drawing.Size(90, 25)
        Me.cmdSendDataPCISer.TabIndex = 5
        Me.cmdSendDataPCISer.Text = "Send Data"
        Me.cmdSendDataPCISer.UseVisualStyleBackColor = False
        '
        'cmdDataReceivePCISer
        '
        Me.cmdDataReceivePCISer.BackColor = System.Drawing.SystemColors.Control
        Me.cmdDataReceivePCISer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDataReceivePCISer.Location = New System.Drawing.Point(518, 340)
        Me.cmdDataReceivePCISer.Name = "cmdDataReceivePCISer"
        Me.cmdDataReceivePCISer.Size = New System.Drawing.Size(90, 25)
        Me.cmdDataReceivePCISer.TabIndex = 7
        Me.cmdDataReceivePCISer.Text = "Receive Data"
        Me.cmdDataReceivePCISer.UseVisualStyleBackColor = False
        '
        'txtSaveSerialPCISer
        '
        Me.txtSaveSerialPCISer.BackColor = System.Drawing.SystemColors.Window
        Me.txtSaveSerialPCISer.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSaveSerialPCISer.Location = New System.Drawing.Point(24, 340)
        Me.txtSaveSerialPCISer.Name = "txtSaveSerialPCISer"
        Me.txtSaveSerialPCISer.Size = New System.Drawing.Size(50, 20)
        Me.txtSaveSerialPCISer.TabIndex = 99
        Me.txtSaveSerialPCISer.Text = "RS232"
        Me.txtSaveSerialPCISer.Visible = False
        '
        'tabOptions_Page4
        '
        Me.tabOptions_Page4.Controls.Add(Me.lblMaxTimeGigabit1)
        Me.tabOptions_Page4.Controls.Add(Me.frameReceiveGigabit1)
        Me.tabOptions_Page4.Controls.Add(Me.frameSendGigabit1)
        Me.tabOptions_Page4.Controls.Add(Me.txtMaxTimeGigabit1)
        Me.tabOptions_Page4.Controls.Add(Me.cmdSendSettingsGigabit1)
        Me.tabOptions_Page4.Controls.Add(Me.cmdLoadGigabit1)
        Me.tabOptions_Page4.Controls.Add(Me.Frame3)
        Me.tabOptions_Page4.Controls.Add(Me.FrameProtocolGigabit1)
        Me.tabOptions_Page4.Controls.Add(Me.txtDataSentGigabit1)
        Me.tabOptions_Page4.Controls.Add(Me.cmdSendDataGigabit1)
        Me.tabOptions_Page4.Controls.Add(Me.cmdDataReceiveGigabit1)
        Me.tabOptions_Page4.Controls.Add(Me.txtDataReceivedGigabit1)
        Me.tabOptions_Page4.Controls.Add(Me.txtSaveTCPGigabit1)
        Me.tabOptions_Page4.Location = New System.Drawing.Point(4, 22)
        Me.tabOptions_Page4.Name = "tabOptions_Page4"
        Me.tabOptions_Page4.Size = New System.Drawing.Size(634, 388)
        Me.tabOptions_Page4.TabIndex = 2
        Me.tabOptions_Page4.Text = "Gigabit1"
        '
        'lblMaxTimeGigabit1
        '
        Me.lblMaxTimeGigabit1.BackColor = System.Drawing.SystemColors.Control
        Me.lblMaxTimeGigabit1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMaxTimeGigabit1.Location = New System.Drawing.Point(16, 130)
        Me.lblMaxTimeGigabit1.Name = "lblMaxTimeGigabit1"
        Me.lblMaxTimeGigabit1.Size = New System.Drawing.Size(54, 20)
        Me.lblMaxTimeGigabit1.TabIndex = 116
        Me.lblMaxTimeGigabit1.Text = "Max Time"
        Me.lblMaxTimeGigabit1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'frameReceiveGigabit1
        '
        Me.frameReceiveGigabit1.BackColor = System.Drawing.SystemColors.Control
        Me.frameReceiveGigabit1.Controls.Add(Me.optDataReceiveGigabit1)
        Me.frameReceiveGigabit1.Controls.Add(Me.optXMLReceiveGigabit1)
        Me.frameReceiveGigabit1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.frameReceiveGigabit1.Location = New System.Drawing.Point(316, 186)
        Me.frameReceiveGigabit1.Name = "frameReceiveGigabit1"
        Me.frameReceiveGigabit1.Size = New System.Drawing.Size(292, 33)
        Me.frameReceiveGigabit1.TabIndex = 9
        Me.frameReceiveGigabit1.TabStop = False
        Me.frameReceiveGigabit1.Text = "Receive Signal Type"
        '
        'optDataReceiveGigabit1
        '
        Me.optDataReceiveGigabit1.BackColor = System.Drawing.SystemColors.Control
        Me.optDataReceiveGigabit1.Checked = True
        Me.optDataReceiveGigabit1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optDataReceiveGigabit1.Location = New System.Drawing.Point(218, 8)
        Me.optDataReceiveGigabit1.Name = "optDataReceiveGigabit1"
        Me.optDataReceiveGigabit1.Size = New System.Drawing.Size(50, 17)
        Me.optDataReceiveGigabit1.TabIndex = 8
        Me.optDataReceiveGigabit1.TabStop = True
        Me.optDataReceiveGigabit1.Text = "Data"
        Me.optDataReceiveGigabit1.UseVisualStyleBackColor = False
        '
        'optXMLReceiveGigabit1
        '
        Me.optXMLReceiveGigabit1.BackColor = System.Drawing.SystemColors.Control
        Me.optXMLReceiveGigabit1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optXMLReceiveGigabit1.Location = New System.Drawing.Point(113, 8)
        Me.optXMLReceiveGigabit1.Name = "optXMLReceiveGigabit1"
        Me.optXMLReceiveGigabit1.Size = New System.Drawing.Size(98, 17)
        Me.optXMLReceiveGigabit1.TabIndex = 7
        Me.optXMLReceiveGigabit1.Text = "XML Instruction"
        Me.optXMLReceiveGigabit1.UseVisualStyleBackColor = False
        Me.optXMLReceiveGigabit1.Visible = False
        '
        'frameSendGigabit1
        '
        Me.frameSendGigabit1.BackColor = System.Drawing.SystemColors.Control
        Me.frameSendGigabit1.Controls.Add(Me.optDataSendGigabit1)
        Me.frameSendGigabit1.Controls.Add(Me.optXMLSendGigabit1)
        Me.frameSendGigabit1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.frameSendGigabit1.Location = New System.Drawing.Point(16, 186)
        Me.frameSendGigabit1.Name = "frameSendGigabit1"
        Me.frameSendGigabit1.Size = New System.Drawing.Size(292, 33)
        Me.frameSendGigabit1.TabIndex = 24
        Me.frameSendGigabit1.TabStop = False
        Me.frameSendGigabit1.Text = "Send Signal Type"
        '
        'optDataSendGigabit1
        '
        Me.optDataSendGigabit1.BackColor = System.Drawing.SystemColors.Control
        Me.optDataSendGigabit1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optDataSendGigabit1.Location = New System.Drawing.Point(210, 8)
        Me.optDataSendGigabit1.Name = "optDataSendGigabit1"
        Me.optDataSendGigabit1.Size = New System.Drawing.Size(50, 17)
        Me.optDataSendGigabit1.TabIndex = 26
        Me.optDataSendGigabit1.Text = "Data"
        Me.optDataSendGigabit1.UseVisualStyleBackColor = False
        '
        'optXMLSendGigabit1
        '
        Me.optXMLSendGigabit1.BackColor = System.Drawing.SystemColors.Control
        Me.optXMLSendGigabit1.Checked = True
        Me.optXMLSendGigabit1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optXMLSendGigabit1.Location = New System.Drawing.Point(105, 8)
        Me.optXMLSendGigabit1.Name = "optXMLSendGigabit1"
        Me.optXMLSendGigabit1.Size = New System.Drawing.Size(98, 17)
        Me.optXMLSendGigabit1.TabIndex = 25
        Me.optXMLSendGigabit1.TabStop = True
        Me.optXMLSendGigabit1.Text = "XML Instruction"
        Me.optXMLSendGigabit1.UseVisualStyleBackColor = False
        Me.optXMLSendGigabit1.Visible = False
        '
        'txtMaxTimeGigabit1
        '
        Me.txtMaxTimeGigabit1.BackColor = System.Drawing.SystemColors.Window
        Me.txtMaxTimeGigabit1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMaxTimeGigabit1.Location = New System.Drawing.Point(73, 130)
        Me.txtMaxTimeGigabit1.Name = "txtMaxTimeGigabit1"
        Me.txtMaxTimeGigabit1.Size = New System.Drawing.Size(66, 20)
        Me.txtMaxTimeGigabit1.TabIndex = 1
        Me.txtMaxTimeGigabit1.Text = "2"
        '
        'cmdSendSettingsGigabit1
        '
        Me.cmdSendSettingsGigabit1.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSendSettingsGigabit1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSendSettingsGigabit1.Location = New System.Drawing.Point(477, 77)
        Me.cmdSendSettingsGigabit1.Name = "cmdSendSettingsGigabit1"
        Me.cmdSendSettingsGigabit1.Size = New System.Drawing.Size(147, 25)
        Me.cmdSendSettingsGigabit1.TabIndex = 4
        Me.cmdSendSettingsGigabit1.Text = "Send Settings To Device"
        Me.cmdSendSettingsGigabit1.UseVisualStyleBackColor = False
        '
        'cmdLoadGigabit1
        '
        Me.cmdLoadGigabit1.BackColor = System.Drawing.SystemColors.Control
        Me.cmdLoadGigabit1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdLoadGigabit1.Location = New System.Drawing.Point(477, 45)
        Me.cmdLoadGigabit1.Name = "cmdLoadGigabit1"
        Me.cmdLoadGigabit1.Size = New System.Drawing.Size(147, 25)
        Me.cmdLoadGigabit1.TabIndex = 3
        Me.cmdLoadGigabit1.Text = "Load Settings From Device"
        Me.cmdLoadGigabit1.UseVisualStyleBackColor = False
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.txtRemotePortGigabit1)
        Me.Frame3.Controls.Add(Me.txtGatewayGigabit1)
        Me.Frame3.Controls.Add(Me.txtLocalSubnetMaskGigabit1)
        Me.Frame3.Controls.Add(Me.txtLocalPortGigabit1)
        Me.Frame3.Controls.Add(Me.txtLocalIPGigabit1)
        Me.Frame3.Controls.Add(Me.txtRemoteIPGigabit1)
        Me.Frame3.Controls.Add(Me.lblGatewayGigabit1)
        Me.Frame3.Controls.Add(Me.lblLocalSubnetMaskGigabit1)
        Me.Frame3.Controls.Add(Me.lblLocalIPGigabit1)
        Me.Frame3.Controls.Add(Me.lblRemoteIPGigabit1)
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(146, 41)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Size = New System.Drawing.Size(325, 122)
        Me.Frame3.TabIndex = 2
        Me.Frame3.TabStop = False
        Me.Frame3.Text = "Address Information"
        '
        'txtRemotePortGigabit1
        '
        Me.txtRemotePortGigabit1.BackColor = System.Drawing.SystemColors.Window
        Me.txtRemotePortGigabit1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRemotePortGigabit1.Location = New System.Drawing.Point(267, 16)
        Me.txtRemotePortGigabit1.Name = "txtRemotePortGigabit1"
        Me.txtRemotePortGigabit1.Size = New System.Drawing.Size(50, 20)
        Me.txtRemotePortGigabit1.TabIndex = 4
        '
        'txtGatewayGigabit1
        '
        Me.txtGatewayGigabit1.BackColor = System.Drawing.SystemColors.Window
        Me.txtGatewayGigabit1.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGatewayGigabit1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtGatewayGigabit1.Location = New System.Drawing.Point(138, 89)
        Me.txtGatewayGigabit1.Name = "txtGatewayGigabit1"
        Me.txtGatewayGigabit1.Size = New System.Drawing.Size(130, 22)
        Me.txtGatewayGigabit1.TabIndex = 3
        Me.txtGatewayGigabit1.Text = "   .   .   .   "
        '
        'txtLocalSubnetMaskGigabit1
        '
        Me.txtLocalSubnetMaskGigabit1.BackColor = System.Drawing.SystemColors.Window
        Me.txtLocalSubnetMaskGigabit1.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocalSubnetMaskGigabit1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtLocalSubnetMaskGigabit1.Location = New System.Drawing.Point(138, 65)
        Me.txtLocalSubnetMaskGigabit1.Name = "txtLocalSubnetMaskGigabit1"
        Me.txtLocalSubnetMaskGigabit1.Size = New System.Drawing.Size(130, 22)
        Me.txtLocalSubnetMaskGigabit1.TabIndex = 2
        Me.txtLocalSubnetMaskGigabit1.Text = "255.255.255.0"
        '
        'txtLocalPortGigabit1
        '
        Me.txtLocalPortGigabit1.BackColor = System.Drawing.SystemColors.Control
        Me.txtLocalPortGigabit1.Enabled = False
        Me.txtLocalPortGigabit1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtLocalPortGigabit1.Location = New System.Drawing.Point(267, 40)
        Me.txtLocalPortGigabit1.Name = "txtLocalPortGigabit1"
        Me.txtLocalPortGigabit1.Size = New System.Drawing.Size(50, 20)
        Me.txtLocalPortGigabit1.TabIndex = 60
        '
        'txtLocalIPGigabit1
        '
        Me.txtLocalIPGigabit1.BackColor = System.Drawing.SystemColors.Window
        Me.txtLocalIPGigabit1.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocalIPGigabit1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtLocalIPGigabit1.Location = New System.Drawing.Point(138, 40)
        Me.txtLocalIPGigabit1.Name = "txtLocalIPGigabit1"
        Me.txtLocalIPGigabit1.Size = New System.Drawing.Size(130, 22)
        Me.txtLocalIPGigabit1.TabIndex = 1
        Me.txtLocalIPGigabit1.Text = "   .   .   .   "
        '
        'txtRemoteIPGigabit1
        '
        Me.txtRemoteIPGigabit1.BackColor = System.Drawing.SystemColors.Window
        Me.txtRemoteIPGigabit1.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemoteIPGigabit1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRemoteIPGigabit1.Location = New System.Drawing.Point(138, 16)
        Me.txtRemoteIPGigabit1.Name = "txtRemoteIPGigabit1"
        Me.txtRemoteIPGigabit1.Size = New System.Drawing.Size(130, 22)
        Me.txtRemoteIPGigabit1.TabIndex = 0
        Me.txtRemoteIPGigabit1.Text = "   .   .   .  "
        '
        'lblGatewayGigabit1
        '
        Me.lblGatewayGigabit1.BackColor = System.Drawing.SystemColors.Control
        Me.lblGatewayGigabit1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblGatewayGigabit1.Location = New System.Drawing.Point(17, 92)
        Me.lblGatewayGigabit1.Name = "lblGatewayGigabit1"
        Me.lblGatewayGigabit1.Size = New System.Drawing.Size(118, 17)
        Me.lblGatewayGigabit1.TabIndex = 66
        Me.lblGatewayGigabit1.Text = "Local Default Gateway"
        Me.lblGatewayGigabit1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblLocalSubnetMaskGigabit1
        '
        Me.lblLocalSubnetMaskGigabit1.BackColor = System.Drawing.SystemColors.Control
        Me.lblLocalSubnetMaskGigabit1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLocalSubnetMaskGigabit1.Location = New System.Drawing.Point(37, 68)
        Me.lblLocalSubnetMaskGigabit1.Name = "lblLocalSubnetMaskGigabit1"
        Me.lblLocalSubnetMaskGigabit1.Size = New System.Drawing.Size(98, 17)
        Me.lblLocalSubnetMaskGigabit1.TabIndex = 65
        Me.lblLocalSubnetMaskGigabit1.Text = "Local Subnet Mask"
        Me.lblLocalSubnetMaskGigabit1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblLocalIPGigabit1
        '
        Me.lblLocalIPGigabit1.BackColor = System.Drawing.SystemColors.Control
        Me.lblLocalIPGigabit1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLocalIPGigabit1.Location = New System.Drawing.Point(94, 43)
        Me.lblLocalIPGigabit1.Name = "lblLocalIPGigabit1"
        Me.lblLocalIPGigabit1.Size = New System.Drawing.Size(41, 17)
        Me.lblLocalIPGigabit1.TabIndex = 64
        Me.lblLocalIPGigabit1.Text = "Local IP"
        Me.lblLocalIPGigabit1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblRemoteIPGigabit1
        '
        Me.lblRemoteIPGigabit1.BackColor = System.Drawing.SystemColors.Control
        Me.lblRemoteIPGigabit1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblRemoteIPGigabit1.Location = New System.Drawing.Point(61, 19)
        Me.lblRemoteIPGigabit1.Name = "lblRemoteIPGigabit1"
        Me.lblRemoteIPGigabit1.Size = New System.Drawing.Size(74, 17)
        Me.lblRemoteIPGigabit1.TabIndex = 63
        Me.lblRemoteIPGigabit1.Text = "Remote IP:Port"
        Me.lblRemoteIPGigabit1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'FrameProtocolGigabit1
        '
        Me.FrameProtocolGigabit1.BackColor = System.Drawing.SystemColors.Control
        Me.FrameProtocolGigabit1.Controls.Add(Me.optUDPGigabit1)
        Me.FrameProtocolGigabit1.Controls.Add(Me.optTCPGigabit1)
        Me.FrameProtocolGigabit1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FrameProtocolGigabit1.Location = New System.Drawing.Point(16, 41)
        Me.FrameProtocolGigabit1.Name = "FrameProtocolGigabit1"
        Me.FrameProtocolGigabit1.Size = New System.Drawing.Size(122, 74)
        Me.FrameProtocolGigabit1.TabIndex = 0
        Me.FrameProtocolGigabit1.TabStop = False
        Me.FrameProtocolGigabit1.Text = "Protocol"
        '
        'optUDPGigabit1
        '
        Me.optUDPGigabit1.BackColor = System.Drawing.SystemColors.Control
        Me.optUDPGigabit1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optUDPGigabit1.Location = New System.Drawing.Point(16, 40)
        Me.optUDPGigabit1.Name = "optUDPGigabit1"
        Me.optUDPGigabit1.Size = New System.Drawing.Size(50, 25)
        Me.optUDPGigabit1.TabIndex = 1
        Me.optUDPGigabit1.Text = "UDP"
        Me.optUDPGigabit1.UseVisualStyleBackColor = False
        '
        'optTCPGigabit1
        '
        Me.optTCPGigabit1.BackColor = System.Drawing.SystemColors.Control
        Me.optTCPGigabit1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optTCPGigabit1.Location = New System.Drawing.Point(16, 16)
        Me.optTCPGigabit1.Name = "optTCPGigabit1"
        Me.optTCPGigabit1.Size = New System.Drawing.Size(50, 25)
        Me.optTCPGigabit1.TabIndex = 0
        Me.optTCPGigabit1.Text = "TCP"
        Me.optTCPGigabit1.UseVisualStyleBackColor = False
        '
        'txtDataSentGigabit1
        '
        Me.txtDataSentGigabit1.BackColor = System.Drawing.SystemColors.Window
        Me.txtDataSentGigabit1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDataSentGigabit1.Location = New System.Drawing.Point(16, 223)
        Me.txtDataSentGigabit1.MaxLength = 1000
        Me.txtDataSentGigabit1.Multiline = True
        Me.txtDataSentGigabit1.Name = "txtDataSentGigabit1"
        Me.txtDataSentGigabit1.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtDataSentGigabit1.Size = New System.Drawing.Size(292, 108)
        Me.txtDataSentGigabit1.TabIndex = 5
        '
        'cmdSendDataGigabit1
        '
        Me.cmdSendDataGigabit1.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSendDataGigabit1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSendDataGigabit1.Location = New System.Drawing.Point(218, 340)
        Me.cmdSendDataGigabit1.Name = "cmdSendDataGigabit1"
        Me.cmdSendDataGigabit1.Size = New System.Drawing.Size(90, 25)
        Me.cmdSendDataGigabit1.TabIndex = 6
        Me.cmdSendDataGigabit1.Text = "Send Data"
        Me.cmdSendDataGigabit1.UseVisualStyleBackColor = False
        '
        'cmdDataReceiveGigabit1
        '
        Me.cmdDataReceiveGigabit1.BackColor = System.Drawing.SystemColors.Control
        Me.cmdDataReceiveGigabit1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDataReceiveGigabit1.Location = New System.Drawing.Point(518, 340)
        Me.cmdDataReceiveGigabit1.Name = "cmdDataReceiveGigabit1"
        Me.cmdDataReceiveGigabit1.Size = New System.Drawing.Size(90, 25)
        Me.cmdDataReceiveGigabit1.TabIndex = 7
        Me.cmdDataReceiveGigabit1.Text = "Receive Data"
        Me.cmdDataReceiveGigabit1.UseVisualStyleBackColor = False
        '
        'txtDataReceivedGigabit1
        '
        Me.txtDataReceivedGigabit1.BackColor = System.Drawing.SystemColors.Window
        Me.txtDataReceivedGigabit1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDataReceivedGigabit1.Location = New System.Drawing.Point(316, 223)
        Me.txtDataReceivedGigabit1.Multiline = True
        Me.txtDataReceivedGigabit1.Name = "txtDataReceivedGigabit1"
        Me.txtDataReceivedGigabit1.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtDataReceivedGigabit1.Size = New System.Drawing.Size(292, 108)
        Me.txtDataReceivedGigabit1.TabIndex = 8
        '
        'txtSaveTCPGigabit1
        '
        Me.txtSaveTCPGigabit1.BackColor = System.Drawing.SystemColors.Window
        Me.txtSaveTCPGigabit1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSaveTCPGigabit1.Location = New System.Drawing.Point(16, 336)
        Me.txtSaveTCPGigabit1.Name = "txtSaveTCPGigabit1"
        Me.txtSaveTCPGigabit1.Size = New System.Drawing.Size(50, 20)
        Me.txtSaveTCPGigabit1.TabIndex = 74
        Me.txtSaveTCPGigabit1.Text = "TCP"
        Me.txtSaveTCPGigabit1.Visible = False
        '
        'tabOptions_Page5
        '
        Me.tabOptions_Page5.Controls.Add(Me.lblMaxTimeGigabit2)
        Me.tabOptions_Page5.Controls.Add(Me.txtSaveTCPGigabit2)
        Me.tabOptions_Page5.Controls.Add(Me.FrameProtocolGigabit2)
        Me.tabOptions_Page5.Controls.Add(Me.txtDataReceivedGigabit2)
        Me.tabOptions_Page5.Controls.Add(Me.cmdDataReceiveGigabit2)
        Me.tabOptions_Page5.Controls.Add(Me.cmdSendDataGigabit2)
        Me.tabOptions_Page5.Controls.Add(Me.txtDataSentGigabit2)
        Me.tabOptions_Page5.Controls.Add(Me.cmdLoadGigabit2)
        Me.tabOptions_Page5.Controls.Add(Me.cmdSendSettingsGigabit2)
        Me.tabOptions_Page5.Controls.Add(Me.frameSendGigabit2)
        Me.tabOptions_Page5.Controls.Add(Me.frameReceiveGigabit2)
        Me.tabOptions_Page5.Controls.Add(Me.Frame4)
        Me.tabOptions_Page5.Controls.Add(Me.txtMaxTimeGigabit2)
        Me.tabOptions_Page5.Location = New System.Drawing.Point(4, 22)
        Me.tabOptions_Page5.Name = "tabOptions_Page5"
        Me.tabOptions_Page5.Size = New System.Drawing.Size(634, 388)
        Me.tabOptions_Page5.TabIndex = 4
        Me.tabOptions_Page5.Text = "Gigabit2"
        '
        'lblMaxTimeGigabit2
        '
        Me.lblMaxTimeGigabit2.BackColor = System.Drawing.SystemColors.Control
        Me.lblMaxTimeGigabit2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMaxTimeGigabit2.Location = New System.Drawing.Point(7, 136)
        Me.lblMaxTimeGigabit2.Name = "lblMaxTimeGigabit2"
        Me.lblMaxTimeGigabit2.Size = New System.Drawing.Size(63, 17)
        Me.lblMaxTimeGigabit2.TabIndex = 281
        Me.lblMaxTimeGigabit2.Text = "Max Time"
        Me.lblMaxTimeGigabit2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtSaveTCPGigabit2
        '
        Me.txtSaveTCPGigabit2.BackColor = System.Drawing.SystemColors.Window
        Me.txtSaveTCPGigabit2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSaveTCPGigabit2.Location = New System.Drawing.Point(16, 343)
        Me.txtSaveTCPGigabit2.Name = "txtSaveTCPGigabit2"
        Me.txtSaveTCPGigabit2.Size = New System.Drawing.Size(50, 20)
        Me.txtSaveTCPGigabit2.TabIndex = 119
        Me.txtSaveTCPGigabit2.Text = "TCP"
        Me.txtSaveTCPGigabit2.Visible = False
        '
        'FrameProtocolGigabit2
        '
        Me.FrameProtocolGigabit2.BackColor = System.Drawing.SystemColors.Control
        Me.FrameProtocolGigabit2.Controls.Add(Me.optUDPGigabit2)
        Me.FrameProtocolGigabit2.Controls.Add(Me.optTCPGigabit2)
        Me.FrameProtocolGigabit2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FrameProtocolGigabit2.Location = New System.Drawing.Point(16, 48)
        Me.FrameProtocolGigabit2.Name = "FrameProtocolGigabit2"
        Me.FrameProtocolGigabit2.Size = New System.Drawing.Size(122, 74)
        Me.FrameProtocolGigabit2.TabIndex = 0
        Me.FrameProtocolGigabit2.TabStop = False
        Me.FrameProtocolGigabit2.Text = "Protocol"
        '
        'optUDPGigabit2
        '
        Me.optUDPGigabit2.BackColor = System.Drawing.SystemColors.Control
        Me.optUDPGigabit2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optUDPGigabit2.Location = New System.Drawing.Point(16, 40)
        Me.optUDPGigabit2.Name = "optUDPGigabit2"
        Me.optUDPGigabit2.Size = New System.Drawing.Size(50, 25)
        Me.optUDPGigabit2.TabIndex = 1
        Me.optUDPGigabit2.Text = "UDP"
        Me.optUDPGigabit2.UseVisualStyleBackColor = False
        '
        'optTCPGigabit2
        '
        Me.optTCPGigabit2.BackColor = System.Drawing.SystemColors.Control
        Me.optTCPGigabit2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optTCPGigabit2.Location = New System.Drawing.Point(16, 16)
        Me.optTCPGigabit2.Name = "optTCPGigabit2"
        Me.optTCPGigabit2.Size = New System.Drawing.Size(50, 25)
        Me.optTCPGigabit2.TabIndex = 0
        Me.optTCPGigabit2.Text = "TCP"
        Me.optTCPGigabit2.UseVisualStyleBackColor = False
        '
        'txtDataReceivedGigabit2
        '
        Me.txtDataReceivedGigabit2.BackColor = System.Drawing.SystemColors.Window
        Me.txtDataReceivedGigabit2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDataReceivedGigabit2.Location = New System.Drawing.Point(316, 227)
        Me.txtDataReceivedGigabit2.Multiline = True
        Me.txtDataReceivedGigabit2.Name = "txtDataReceivedGigabit2"
        Me.txtDataReceivedGigabit2.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtDataReceivedGigabit2.Size = New System.Drawing.Size(292, 108)
        Me.txtDataReceivedGigabit2.TabIndex = 120
        '
        'cmdDataReceiveGigabit2
        '
        Me.cmdDataReceiveGigabit2.BackColor = System.Drawing.SystemColors.Control
        Me.cmdDataReceiveGigabit2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDataReceiveGigabit2.Location = New System.Drawing.Point(518, 344)
        Me.cmdDataReceiveGigabit2.Name = "cmdDataReceiveGigabit2"
        Me.cmdDataReceiveGigabit2.Size = New System.Drawing.Size(90, 25)
        Me.cmdDataReceiveGigabit2.TabIndex = 7
        Me.cmdDataReceiveGigabit2.Text = "Receive Data"
        Me.cmdDataReceiveGigabit2.UseVisualStyleBackColor = False
        '
        'cmdSendDataGigabit2
        '
        Me.cmdSendDataGigabit2.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSendDataGigabit2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSendDataGigabit2.Location = New System.Drawing.Point(218, 344)
        Me.cmdSendDataGigabit2.Name = "cmdSendDataGigabit2"
        Me.cmdSendDataGigabit2.Size = New System.Drawing.Size(90, 25)
        Me.cmdSendDataGigabit2.TabIndex = 6
        Me.cmdSendDataGigabit2.Text = "Send Data"
        Me.cmdSendDataGigabit2.UseVisualStyleBackColor = False
        '
        'txtDataSentGigabit2
        '
        Me.txtDataSentGigabit2.BackColor = System.Drawing.SystemColors.Window
        Me.txtDataSentGigabit2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDataSentGigabit2.Location = New System.Drawing.Point(16, 227)
        Me.txtDataSentGigabit2.MaxLength = 1000
        Me.txtDataSentGigabit2.Multiline = True
        Me.txtDataSentGigabit2.Name = "txtDataSentGigabit2"
        Me.txtDataSentGigabit2.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtDataSentGigabit2.Size = New System.Drawing.Size(292, 108)
        Me.txtDataSentGigabit2.TabIndex = 5
        '
        'cmdLoadGigabit2
        '
        Me.cmdLoadGigabit2.BackColor = System.Drawing.SystemColors.Control
        Me.cmdLoadGigabit2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdLoadGigabit2.Location = New System.Drawing.Point(467, 78)
        Me.cmdLoadGigabit2.Name = "cmdLoadGigabit2"
        Me.cmdLoadGigabit2.Size = New System.Drawing.Size(147, 25)
        Me.cmdLoadGigabit2.TabIndex = 4
        Me.cmdLoadGigabit2.Text = "Load Settings From Device"
        Me.cmdLoadGigabit2.UseVisualStyleBackColor = False
        '
        'cmdSendSettingsGigabit2
        '
        Me.cmdSendSettingsGigabit2.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSendSettingsGigabit2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSendSettingsGigabit2.Location = New System.Drawing.Point(467, 48)
        Me.cmdSendSettingsGigabit2.Name = "cmdSendSettingsGigabit2"
        Me.cmdSendSettingsGigabit2.Size = New System.Drawing.Size(147, 25)
        Me.cmdSendSettingsGigabit2.TabIndex = 3
        Me.cmdSendSettingsGigabit2.Text = "Send Settings To Device"
        Me.cmdSendSettingsGigabit2.UseVisualStyleBackColor = False
        '
        'frameSendGigabit2
        '
        Me.frameSendGigabit2.BackColor = System.Drawing.SystemColors.Control
        Me.frameSendGigabit2.Controls.Add(Me.optXMLSendGigabit2)
        Me.frameSendGigabit2.Controls.Add(Me.optDataSendGigabit2)
        Me.frameSendGigabit2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.frameSendGigabit2.Location = New System.Drawing.Point(16, 191)
        Me.frameSendGigabit2.Name = "frameSendGigabit2"
        Me.frameSendGigabit2.Size = New System.Drawing.Size(292, 33)
        Me.frameSendGigabit2.TabIndex = 126
        Me.frameSendGigabit2.TabStop = False
        Me.frameSendGigabit2.Text = "Send Signal Type"
        '
        'optXMLSendGigabit2
        '
        Me.optXMLSendGigabit2.BackColor = System.Drawing.SystemColors.Control
        Me.optXMLSendGigabit2.Enabled = False
        Me.optXMLSendGigabit2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optXMLSendGigabit2.Location = New System.Drawing.Point(105, 8)
        Me.optXMLSendGigabit2.Name = "optXMLSendGigabit2"
        Me.optXMLSendGigabit2.Size = New System.Drawing.Size(98, 17)
        Me.optXMLSendGigabit2.TabIndex = 128
        Me.optXMLSendGigabit2.Text = "XML Instruction"
        Me.optXMLSendGigabit2.UseVisualStyleBackColor = False
        Me.optXMLSendGigabit2.Visible = False
        '
        'optDataSendGigabit2
        '
        Me.optDataSendGigabit2.BackColor = System.Drawing.SystemColors.Control
        Me.optDataSendGigabit2.Checked = True
        Me.optDataSendGigabit2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optDataSendGigabit2.Location = New System.Drawing.Point(210, 8)
        Me.optDataSendGigabit2.Name = "optDataSendGigabit2"
        Me.optDataSendGigabit2.Size = New System.Drawing.Size(50, 17)
        Me.optDataSendGigabit2.TabIndex = 127
        Me.optDataSendGigabit2.TabStop = True
        Me.optDataSendGigabit2.Text = "Data"
        Me.optDataSendGigabit2.UseVisualStyleBackColor = False
        '
        'frameReceiveGigabit2
        '
        Me.frameReceiveGigabit2.BackColor = System.Drawing.SystemColors.Control
        Me.frameReceiveGigabit2.Controls.Add(Me.optXMLReceiveGigabit2)
        Me.frameReceiveGigabit2.Controls.Add(Me.optDataReceiveGigabit2)
        Me.frameReceiveGigabit2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.frameReceiveGigabit2.Location = New System.Drawing.Point(316, 191)
        Me.frameReceiveGigabit2.Name = "frameReceiveGigabit2"
        Me.frameReceiveGigabit2.Size = New System.Drawing.Size(292, 33)
        Me.frameReceiveGigabit2.TabIndex = 129
        Me.frameReceiveGigabit2.TabStop = False
        Me.frameReceiveGigabit2.Text = "Receive Signal Type"
        '
        'optXMLReceiveGigabit2
        '
        Me.optXMLReceiveGigabit2.BackColor = System.Drawing.SystemColors.Control
        Me.optXMLReceiveGigabit2.Enabled = False
        Me.optXMLReceiveGigabit2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optXMLReceiveGigabit2.Location = New System.Drawing.Point(113, 8)
        Me.optXMLReceiveGigabit2.Name = "optXMLReceiveGigabit2"
        Me.optXMLReceiveGigabit2.Size = New System.Drawing.Size(98, 17)
        Me.optXMLReceiveGigabit2.TabIndex = 131
        Me.optXMLReceiveGigabit2.Text = "XML Instruction"
        Me.optXMLReceiveGigabit2.UseVisualStyleBackColor = False
        Me.optXMLReceiveGigabit2.Visible = False
        '
        'optDataReceiveGigabit2
        '
        Me.optDataReceiveGigabit2.BackColor = System.Drawing.SystemColors.Control
        Me.optDataReceiveGigabit2.Checked = True
        Me.optDataReceiveGigabit2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optDataReceiveGigabit2.Location = New System.Drawing.Point(218, 8)
        Me.optDataReceiveGigabit2.Name = "optDataReceiveGigabit2"
        Me.optDataReceiveGigabit2.Size = New System.Drawing.Size(50, 17)
        Me.optDataReceiveGigabit2.TabIndex = 130
        Me.optDataReceiveGigabit2.TabStop = True
        Me.optDataReceiveGigabit2.Text = "Data"
        Me.optDataReceiveGigabit2.UseVisualStyleBackColor = False
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.txtLocalPortGigabit2)
        Me.Frame4.Controls.Add(Me.txtRemotePortGigabit2)
        Me.Frame4.Controls.Add(Me.txtLocalIPGigabit2)
        Me.Frame4.Controls.Add(Me.txtGatewayGigabit2)
        Me.Frame4.Controls.Add(Me.txtLocalSubnetMaskGigabit2)
        Me.Frame4.Controls.Add(Me.txtRemoteIPGigabit2)
        Me.Frame4.Controls.Add(Me.lblLocalIPGigabit2)
        Me.Frame4.Controls.Add(Me.lblGatewayGigabit2)
        Me.Frame4.Controls.Add(Me.lblLocalSubnetMaskGigabit2)
        Me.Frame4.Controls.Add(Me.lblRemoteIPGigabit2)
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(144, 48)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Size = New System.Drawing.Size(317, 120)
        Me.Frame4.TabIndex = 2
        Me.Frame4.TabStop = False
        Me.Frame4.Text = "Address Information"
        '
        'txtLocalPortGigabit2
        '
        Me.txtLocalPortGigabit2.BackColor = System.Drawing.SystemColors.Window
        Me.txtLocalPortGigabit2.Enabled = False
        Me.txtLocalPortGigabit2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocalPortGigabit2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtLocalPortGigabit2.Location = New System.Drawing.Point(260, 39)
        Me.txtLocalPortGigabit2.Name = "txtLocalPortGigabit2"
        Me.txtLocalPortGigabit2.Size = New System.Drawing.Size(50, 22)
        Me.txtLocalPortGigabit2.TabIndex = 262
        '
        'txtRemotePortGigabit2
        '
        Me.txtRemotePortGigabit2.BackColor = System.Drawing.SystemColors.Window
        Me.txtRemotePortGigabit2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemotePortGigabit2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRemotePortGigabit2.Location = New System.Drawing.Point(260, 16)
        Me.txtRemotePortGigabit2.Name = "txtRemotePortGigabit2"
        Me.txtRemotePortGigabit2.Size = New System.Drawing.Size(50, 22)
        Me.txtRemotePortGigabit2.TabIndex = 261
        '
        'txtLocalIPGigabit2
        '
        Me.txtLocalIPGigabit2.BackColor = System.Drawing.SystemColors.Window
        Me.txtLocalIPGigabit2.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocalIPGigabit2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtLocalIPGigabit2.Location = New System.Drawing.Point(128, 40)
        Me.txtLocalIPGigabit2.Name = "txtLocalIPGigabit2"
        Me.txtLocalIPGigabit2.Size = New System.Drawing.Size(130, 22)
        Me.txtLocalIPGigabit2.TabIndex = 1
        Me.txtLocalIPGigabit2.Text = "   .   .   .   "
        '
        'txtGatewayGigabit2
        '
        Me.txtGatewayGigabit2.BackColor = System.Drawing.SystemColors.Window
        Me.txtGatewayGigabit2.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGatewayGigabit2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtGatewayGigabit2.Location = New System.Drawing.Point(128, 89)
        Me.txtGatewayGigabit2.Name = "txtGatewayGigabit2"
        Me.txtGatewayGigabit2.Size = New System.Drawing.Size(130, 22)
        Me.txtGatewayGigabit2.TabIndex = 3
        Me.txtGatewayGigabit2.Text = "   .   .   .   "
        '
        'txtLocalSubnetMaskGigabit2
        '
        Me.txtLocalSubnetMaskGigabit2.BackColor = System.Drawing.SystemColors.Window
        Me.txtLocalSubnetMaskGigabit2.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocalSubnetMaskGigabit2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtLocalSubnetMaskGigabit2.Location = New System.Drawing.Point(128, 65)
        Me.txtLocalSubnetMaskGigabit2.Name = "txtLocalSubnetMaskGigabit2"
        Me.txtLocalSubnetMaskGigabit2.Size = New System.Drawing.Size(130, 22)
        Me.txtLocalSubnetMaskGigabit2.TabIndex = 2
        Me.txtLocalSubnetMaskGigabit2.Text = "255.255.255.0"
        '
        'txtRemoteIPGigabit2
        '
        Me.txtRemoteIPGigabit2.BackColor = System.Drawing.SystemColors.Window
        Me.txtRemoteIPGigabit2.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemoteIPGigabit2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRemoteIPGigabit2.Location = New System.Drawing.Point(128, 16)
        Me.txtRemoteIPGigabit2.Name = "txtRemoteIPGigabit2"
        Me.txtRemoteIPGigabit2.Size = New System.Drawing.Size(130, 22)
        Me.txtRemoteIPGigabit2.TabIndex = 0
        Me.txtRemoteIPGigabit2.Text = "   .   .   .   "
        '
        'lblLocalIPGigabit2
        '
        Me.lblLocalIPGigabit2.BackColor = System.Drawing.SystemColors.Control
        Me.lblLocalIPGigabit2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLocalIPGigabit2.Location = New System.Drawing.Point(64, 45)
        Me.lblLocalIPGigabit2.Name = "lblLocalIPGigabit2"
        Me.lblLocalIPGigabit2.Size = New System.Drawing.Size(58, 17)
        Me.lblLocalIPGigabit2.TabIndex = 219
        Me.lblLocalIPGigabit2.Text = "Local"
        Me.lblLocalIPGigabit2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblGatewayGigabit2
        '
        Me.lblGatewayGigabit2.BackColor = System.Drawing.SystemColors.Control
        Me.lblGatewayGigabit2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblGatewayGigabit2.Location = New System.Drawing.Point(4, 93)
        Me.lblGatewayGigabit2.Name = "lblGatewayGigabit2"
        Me.lblGatewayGigabit2.Size = New System.Drawing.Size(118, 17)
        Me.lblGatewayGigabit2.TabIndex = 217
        Me.lblGatewayGigabit2.Text = "Local Default Gateway"
        Me.lblGatewayGigabit2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblLocalSubnetMaskGigabit2
        '
        Me.lblLocalSubnetMaskGigabit2.BackColor = System.Drawing.SystemColors.Control
        Me.lblLocalSubnetMaskGigabit2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLocalSubnetMaskGigabit2.Location = New System.Drawing.Point(24, 69)
        Me.lblLocalSubnetMaskGigabit2.Name = "lblLocalSubnetMaskGigabit2"
        Me.lblLocalSubnetMaskGigabit2.Size = New System.Drawing.Size(98, 17)
        Me.lblLocalSubnetMaskGigabit2.TabIndex = 216
        Me.lblLocalSubnetMaskGigabit2.Text = "Local Subnet Mask"
        Me.lblLocalSubnetMaskGigabit2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblRemoteIPGigabit2
        '
        Me.lblRemoteIPGigabit2.BackColor = System.Drawing.SystemColors.Control
        Me.lblRemoteIPGigabit2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblRemoteIPGigabit2.Location = New System.Drawing.Point(72, 21)
        Me.lblRemoteIPGigabit2.Name = "lblRemoteIPGigabit2"
        Me.lblRemoteIPGigabit2.Size = New System.Drawing.Size(50, 17)
        Me.lblRemoteIPGigabit2.TabIndex = 215
        Me.lblRemoteIPGigabit2.Text = "Remote"
        Me.lblRemoteIPGigabit2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtMaxTimeGigabit2
        '
        Me.txtMaxTimeGigabit2.BackColor = System.Drawing.SystemColors.Window
        Me.txtMaxTimeGigabit2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMaxTimeGigabit2.Location = New System.Drawing.Point(73, 136)
        Me.txtMaxTimeGigabit2.Name = "txtMaxTimeGigabit2"
        Me.txtMaxTimeGigabit2.Size = New System.Drawing.Size(65, 20)
        Me.txtMaxTimeGigabit2.TabIndex = 1
        Me.txtMaxTimeGigabit2.Text = "2"
        Me.ToolTip1.SetToolTip(Me.txtMaxTimeGigabit2, "Time in seconds")
        '
        'tabOptions_Page7
        '
        Me.tabOptions_Page7.Controls.Add(Me.cmdDataListenCAN)
        Me.tabOptions_Page7.Controls.Add(Me.chkSelfReceptionCAN)
        Me.tabOptions_Page7.Controls.Add(Me.chkRemoteFrameCAN)
        Me.tabOptions_Page7.Controls.Add(Me.chkSingleShotCAN)
        Me.tabOptions_Page7.Controls.Add(Me.frameIDTypeCAN)
        Me.tabOptions_Page7.Controls.Add(Me.lblTimingValueCAN)
        Me.tabOptions_Page7.Controls.Add(Me.lblAcceptanceCodeCAN)
        Me.tabOptions_Page7.Controls.Add(Me.lblAcceptanceMaskCAN)
        Me.tabOptions_Page7.Controls.Add(Me.lblMaxTimeCAN)
        Me.tabOptions_Page7.Controls.Add(Me.lblMessageIDCAN)
        Me.tabOptions_Page7.Controls.Add(Me.txtAcceptanceCodeCAN)
        Me.tabOptions_Page7.Controls.Add(Me.txtAcceptanceMaskCAN)
        Me.tabOptions_Page7.Controls.Add(Me.txtDataSendCAN)
        Me.tabOptions_Page7.Controls.Add(Me.txtDataReceivedCAN)
        Me.tabOptions_Page7.Controls.Add(Me.frameSendCAN)
        Me.tabOptions_Page7.Controls.Add(Me.frameReceiveCAN)
        Me.tabOptions_Page7.Controls.Add(Me.cmdSendDataCAN)
        Me.tabOptions_Page7.Controls.Add(Me.cmdDataReceiveCAN)
        Me.tabOptions_Page7.Controls.Add(Me.txtSaveTCPCAN)
        Me.tabOptions_Page7.Controls.Add(Me.txtMaxTimeCAN)
        Me.tabOptions_Page7.Controls.Add(Me.chkThreeSamplesCAN)
        Me.tabOptions_Page7.Controls.Add(Me.chkSingleFilterCAN)
        Me.tabOptions_Page7.Controls.Add(Me.frameChannelCAN)
        Me.tabOptions_Page7.Controls.Add(Me.cmdLoadCAN)
        Me.tabOptions_Page7.Controls.Add(Me.cmdSendSettingsCAN)
        Me.tabOptions_Page7.Controls.Add(Me.txtMessageIDCAN)
        Me.tabOptions_Page7.Controls.Add(Me.cmbTimingValueCAN)
        Me.tabOptions_Page7.Location = New System.Drawing.Point(4, 22)
        Me.tabOptions_Page7.Name = "tabOptions_Page7"
        Me.tabOptions_Page7.Size = New System.Drawing.Size(634, 388)
        Me.tabOptions_Page7.TabIndex = 6
        Me.tabOptions_Page7.Text = "CAN"
        '
        'cmdDataListenCAN
        '
        Me.cmdDataListenCAN.Location = New System.Drawing.Point(402, 344)
        Me.cmdDataListenCAN.Name = "cmdDataListenCAN"
        Me.cmdDataListenCAN.Size = New System.Drawing.Size(90, 25)
        Me.cmdDataListenCAN.TabIndex = 16
        Me.cmdDataListenCAN.Text = "Begin Listening"
        Me.cmdDataListenCAN.UseVisualStyleBackColor = True
        '
        'chkSelfReceptionCAN
        '
        Me.chkSelfReceptionCAN.AutoSize = True
        Me.chkSelfReceptionCAN.Location = New System.Drawing.Point(55, 132)
        Me.chkSelfReceptionCAN.Name = "chkSelfReceptionCAN"
        Me.chkSelfReceptionCAN.Size = New System.Drawing.Size(96, 17)
        Me.chkSelfReceptionCAN.TabIndex = 9
        Me.chkSelfReceptionCAN.Text = "Self Reception"
        Me.chkSelfReceptionCAN.UseVisualStyleBackColor = True
        '
        'chkRemoteFrameCAN
        '
        Me.chkRemoteFrameCAN.AutoSize = True
        Me.chkRemoteFrameCAN.Location = New System.Drawing.Point(55, 108)
        Me.chkRemoteFrameCAN.Name = "chkRemoteFrameCAN"
        Me.chkRemoteFrameCAN.Size = New System.Drawing.Size(95, 17)
        Me.chkRemoteFrameCAN.TabIndex = 8
        Me.chkRemoteFrameCAN.Text = "Remote Frame"
        Me.chkRemoteFrameCAN.UseVisualStyleBackColor = True
        '
        'chkSingleShotCAN
        '
        Me.chkSingleShotCAN.AutoSize = True
        Me.chkSingleShotCAN.Location = New System.Drawing.Point(55, 84)
        Me.chkSingleShotCAN.Name = "chkSingleShotCAN"
        Me.chkSingleShotCAN.Size = New System.Drawing.Size(80, 17)
        Me.chkSingleShotCAN.TabIndex = 7
        Me.chkSingleShotCAN.Text = "Single Shot"
        Me.chkSingleShotCAN.UseVisualStyleBackColor = True
        '
        'frameIDTypeCAN
        '
        Me.frameIDTypeCAN.Controls.Add(Me.optExtendedIDCAN)
        Me.frameIDTypeCAN.Controls.Add(Me.optBasicIDCAN)
        Me.frameIDTypeCAN.Location = New System.Drawing.Point(54, 16)
        Me.frameIDTypeCAN.Name = "frameIDTypeCAN"
        Me.frameIDTypeCAN.Size = New System.Drawing.Size(171, 38)
        Me.frameIDTypeCAN.TabIndex = 6
        Me.frameIDTypeCAN.TabStop = False
        Me.frameIDTypeCAN.Text = "Message ID Type"
        '
        'optExtendedIDCAN
        '
        Me.optExtendedIDCAN.AutoSize = True
        Me.optExtendedIDCAN.Location = New System.Drawing.Point(90, 16)
        Me.optExtendedIDCAN.Name = "optExtendedIDCAN"
        Me.optExtendedIDCAN.Size = New System.Drawing.Size(70, 17)
        Me.optExtendedIDCAN.TabIndex = 1
        Me.optExtendedIDCAN.Text = "Extended"
        Me.optExtendedIDCAN.UseVisualStyleBackColor = True
        '
        'optBasicIDCAN
        '
        Me.optBasicIDCAN.AutoSize = True
        Me.optBasicIDCAN.Location = New System.Drawing.Point(16, 16)
        Me.optBasicIDCAN.Name = "optBasicIDCAN"
        Me.optBasicIDCAN.Size = New System.Drawing.Size(51, 17)
        Me.optBasicIDCAN.TabIndex = 0
        Me.optBasicIDCAN.TabStop = True
        Me.optBasicIDCAN.Text = "Basic"
        Me.optBasicIDCAN.UseVisualStyleBackColor = True
        '
        'lblTimingValueCAN
        '
        Me.lblTimingValueCAN.BackColor = System.Drawing.SystemColors.Control
        Me.lblTimingValueCAN.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTimingValueCAN.Location = New System.Drawing.Point(278, 60)
        Me.lblTimingValueCAN.Name = "lblTimingValueCAN"
        Me.lblTimingValueCAN.Size = New System.Drawing.Size(98, 17)
        Me.lblTimingValueCAN.TabIndex = 184
        Me.lblTimingValueCAN.Text = "Timing Value"
        Me.lblTimingValueCAN.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblAcceptanceCodeCAN
        '
        Me.lblAcceptanceCodeCAN.BackColor = System.Drawing.SystemColors.Control
        Me.lblAcceptanceCodeCAN.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAcceptanceCodeCAN.Location = New System.Drawing.Point(247, 84)
        Me.lblAcceptanceCodeCAN.Name = "lblAcceptanceCodeCAN"
        Me.lblAcceptanceCodeCAN.Size = New System.Drawing.Size(129, 17)
        Me.lblAcceptanceCodeCAN.TabIndex = 185
        Me.lblAcceptanceCodeCAN.Text = "Acceptance Code (Hex)"
        Me.lblAcceptanceCodeCAN.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblAcceptanceMaskCAN
        '
        Me.lblAcceptanceMaskCAN.BackColor = System.Drawing.SystemColors.Control
        Me.lblAcceptanceMaskCAN.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAcceptanceMaskCAN.Location = New System.Drawing.Point(247, 108)
        Me.lblAcceptanceMaskCAN.Name = "lblAcceptanceMaskCAN"
        Me.lblAcceptanceMaskCAN.Size = New System.Drawing.Size(129, 17)
        Me.lblAcceptanceMaskCAN.TabIndex = 186
        Me.lblAcceptanceMaskCAN.Text = "Acceptance Mask (Hex)"
        Me.lblAcceptanceMaskCAN.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblMaxTimeCAN
        '
        Me.lblMaxTimeCAN.AutoSize = True
        Me.lblMaxTimeCAN.BackColor = System.Drawing.SystemColors.Control
        Me.lblMaxTimeCAN.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMaxTimeCAN.Location = New System.Drawing.Point(297, 132)
        Me.lblMaxTimeCAN.Name = "lblMaxTimeCAN"
        Me.lblMaxTimeCAN.Size = New System.Drawing.Size(79, 13)
        Me.lblMaxTimeCAN.TabIndex = 245
        Me.lblMaxTimeCAN.Text = "Max Time (sec)"
        Me.lblMaxTimeCAN.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblMessageIDCAN
        '
        Me.lblMessageIDCAN.BackColor = System.Drawing.SystemColors.Control
        Me.lblMessageIDCAN.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMessageIDCAN.Location = New System.Drawing.Point(52, 60)
        Me.lblMessageIDCAN.Name = "lblMessageIDCAN"
        Me.lblMessageIDCAN.Size = New System.Drawing.Size(66, 17)
        Me.lblMessageIDCAN.TabIndex = 277
        Me.lblMessageIDCAN.Text = "Message ID"
        '
        'txtAcceptanceCodeCAN
        '
        Me.txtAcceptanceCodeCAN.AllowDrop = True
        Me.txtAcceptanceCodeCAN.BackColor = System.Drawing.SystemColors.Window
        Me.txtAcceptanceCodeCAN.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtAcceptanceCodeCAN.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAcceptanceCodeCAN.Location = New System.Drawing.Point(382, 82)
        Me.txtAcceptanceCodeCAN.MaxLength = 8
        Me.txtAcceptanceCodeCAN.Name = "txtAcceptanceCodeCAN"
        Me.txtAcceptanceCodeCAN.Size = New System.Drawing.Size(74, 20)
        Me.txtAcceptanceCodeCAN.TabIndex = 3
        Me.txtAcceptanceCodeCAN.Text = "FFFFFFFF"
        Me.ToolTip1.SetToolTip(Me.txtAcceptanceCodeCAN, "32 bit hex code")
        '
        'txtAcceptanceMaskCAN
        '
        Me.txtAcceptanceMaskCAN.AcceptsTab = True
        Me.txtAcceptanceMaskCAN.BackColor = System.Drawing.SystemColors.Window
        Me.txtAcceptanceMaskCAN.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtAcceptanceMaskCAN.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAcceptanceMaskCAN.Location = New System.Drawing.Point(382, 106)
        Me.txtAcceptanceMaskCAN.MaxLength = 8
        Me.txtAcceptanceMaskCAN.Name = "txtAcceptanceMaskCAN"
        Me.txtAcceptanceMaskCAN.Size = New System.Drawing.Size(74, 20)
        Me.txtAcceptanceMaskCAN.TabIndex = 4
        Me.txtAcceptanceMaskCAN.Text = "FFFFFFFF"
        Me.ToolTip1.SetToolTip(Me.txtAcceptanceMaskCAN, "32 bit hex mask")
        '
        'txtDataSendCAN
        '
        Me.txtDataSendCAN.AllowDrop = True
        Me.txtDataSendCAN.BackColor = System.Drawing.SystemColors.Window
        Me.txtDataSendCAN.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDataSendCAN.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDataSendCAN.Location = New System.Drawing.Point(16, 231)
        Me.txtDataSendCAN.MaxLength = 16
        Me.txtDataSendCAN.Multiline = True
        Me.txtDataSendCAN.Name = "txtDataSendCAN"
        Me.txtDataSendCAN.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtDataSendCAN.Size = New System.Drawing.Size(292, 108)
        Me.txtDataSendCAN.TabIndex = 14
        Me.ToolTip1.SetToolTip(Me.txtDataSendCAN, "Up to eight bytes of Hexadecimal data")
        '
        'txtDataReceivedCAN
        '
        Me.txtDataReceivedCAN.BackColor = System.Drawing.SystemColors.Window
        Me.txtDataReceivedCAN.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDataReceivedCAN.Location = New System.Drawing.Point(316, 231)
        Me.txtDataReceivedCAN.Multiline = True
        Me.txtDataReceivedCAN.Name = "txtDataReceivedCAN"
        Me.txtDataReceivedCAN.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtDataReceivedCAN.Size = New System.Drawing.Size(292, 108)
        Me.txtDataReceivedCAN.TabIndex = 190
        '
        'frameSendCAN
        '
        Me.frameSendCAN.BackColor = System.Drawing.SystemColors.Control
        Me.frameSendCAN.Controls.Add(Me.optXMLSendCAN)
        Me.frameSendCAN.Controls.Add(Me.optDataSendCAN)
        Me.frameSendCAN.ForeColor = System.Drawing.SystemColors.ControlText
        Me.frameSendCAN.Location = New System.Drawing.Point(16, 191)
        Me.frameSendCAN.Name = "frameSendCAN"
        Me.frameSendCAN.Size = New System.Drawing.Size(292, 33)
        Me.frameSendCAN.TabIndex = 191
        Me.frameSendCAN.TabStop = False
        Me.frameSendCAN.Text = "Send Signal Type"
        '
        'optXMLSendCAN
        '
        Me.optXMLSendCAN.BackColor = System.Drawing.SystemColors.Control
        Me.optXMLSendCAN.Enabled = False
        Me.optXMLSendCAN.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optXMLSendCAN.Location = New System.Drawing.Point(105, 8)
        Me.optXMLSendCAN.Name = "optXMLSendCAN"
        Me.optXMLSendCAN.Size = New System.Drawing.Size(98, 17)
        Me.optXMLSendCAN.TabIndex = 193
        Me.optXMLSendCAN.Text = "XML Instruction"
        Me.optXMLSendCAN.UseVisualStyleBackColor = False
        Me.optXMLSendCAN.Visible = False
        '
        'optDataSendCAN
        '
        Me.optDataSendCAN.BackColor = System.Drawing.SystemColors.Control
        Me.optDataSendCAN.Checked = True
        Me.optDataSendCAN.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optDataSendCAN.Location = New System.Drawing.Point(210, 8)
        Me.optDataSendCAN.Name = "optDataSendCAN"
        Me.optDataSendCAN.Size = New System.Drawing.Size(50, 17)
        Me.optDataSendCAN.TabIndex = 192
        Me.optDataSendCAN.TabStop = True
        Me.optDataSendCAN.Text = "Data"
        Me.optDataSendCAN.UseVisualStyleBackColor = False
        '
        'frameReceiveCAN
        '
        Me.frameReceiveCAN.BackColor = System.Drawing.SystemColors.Control
        Me.frameReceiveCAN.Controls.Add(Me.optXMLReceiveCAN)
        Me.frameReceiveCAN.Controls.Add(Me.optDataReceiveCAN)
        Me.frameReceiveCAN.ForeColor = System.Drawing.SystemColors.ControlText
        Me.frameReceiveCAN.Location = New System.Drawing.Point(316, 191)
        Me.frameReceiveCAN.Name = "frameReceiveCAN"
        Me.frameReceiveCAN.Size = New System.Drawing.Size(292, 33)
        Me.frameReceiveCAN.TabIndex = 194
        Me.frameReceiveCAN.TabStop = False
        Me.frameReceiveCAN.Text = "Receive Signal Type"
        '
        'optXMLReceiveCAN
        '
        Me.optXMLReceiveCAN.BackColor = System.Drawing.SystemColors.Control
        Me.optXMLReceiveCAN.Enabled = False
        Me.optXMLReceiveCAN.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optXMLReceiveCAN.Location = New System.Drawing.Point(114, 8)
        Me.optXMLReceiveCAN.Name = "optXMLReceiveCAN"
        Me.optXMLReceiveCAN.Size = New System.Drawing.Size(98, 17)
        Me.optXMLReceiveCAN.TabIndex = 196
        Me.optXMLReceiveCAN.Text = "XML Instruction"
        Me.optXMLReceiveCAN.UseVisualStyleBackColor = False
        Me.optXMLReceiveCAN.Visible = False
        '
        'optDataReceiveCAN
        '
        Me.optDataReceiveCAN.BackColor = System.Drawing.SystemColors.Control
        Me.optDataReceiveCAN.Checked = True
        Me.optDataReceiveCAN.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optDataReceiveCAN.Location = New System.Drawing.Point(218, 8)
        Me.optDataReceiveCAN.Name = "optDataReceiveCAN"
        Me.optDataReceiveCAN.Size = New System.Drawing.Size(50, 17)
        Me.optDataReceiveCAN.TabIndex = 195
        Me.optDataReceiveCAN.TabStop = True
        Me.optDataReceiveCAN.Text = "Data"
        Me.optDataReceiveCAN.UseVisualStyleBackColor = False
        '
        'cmdSendDataCAN
        '
        Me.cmdSendDataCAN.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSendDataCAN.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSendDataCAN.Location = New System.Drawing.Point(218, 348)
        Me.cmdSendDataCAN.Name = "cmdSendDataCAN"
        Me.cmdSendDataCAN.Size = New System.Drawing.Size(90, 25)
        Me.cmdSendDataCAN.TabIndex = 15
        Me.cmdSendDataCAN.Text = "Send Data"
        Me.cmdSendDataCAN.UseVisualStyleBackColor = False
        '
        'cmdDataReceiveCAN
        '
        Me.cmdDataReceiveCAN.BackColor = System.Drawing.SystemColors.Control
        Me.cmdDataReceiveCAN.Enabled = False
        Me.cmdDataReceiveCAN.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDataReceiveCAN.Location = New System.Drawing.Point(518, 344)
        Me.cmdDataReceiveCAN.Name = "cmdDataReceiveCAN"
        Me.cmdDataReceiveCAN.Size = New System.Drawing.Size(90, 25)
        Me.cmdDataReceiveCAN.TabIndex = 17
        Me.cmdDataReceiveCAN.Text = "Receive Data"
        Me.cmdDataReceiveCAN.UseVisualStyleBackColor = False
        '
        'txtSaveTCPCAN
        '
        Me.txtSaveTCPCAN.BackColor = System.Drawing.SystemColors.Window
        Me.txtSaveTCPCAN.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSaveTCPCAN.Location = New System.Drawing.Point(16, 344)
        Me.txtSaveTCPCAN.Name = "txtSaveTCPCAN"
        Me.txtSaveTCPCAN.Size = New System.Drawing.Size(50, 20)
        Me.txtSaveTCPCAN.TabIndex = 210
        Me.txtSaveTCPCAN.Text = "TCP"
        Me.txtSaveTCPCAN.Visible = False
        '
        'txtMaxTimeCAN
        '
        Me.txtMaxTimeCAN.BackColor = System.Drawing.SystemColors.Window
        Me.txtMaxTimeCAN.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMaxTimeCAN.Location = New System.Drawing.Point(382, 130)
        Me.txtMaxTimeCAN.Name = "txtMaxTimeCAN"
        Me.txtMaxTimeCAN.Size = New System.Drawing.Size(41, 20)
        Me.txtMaxTimeCAN.TabIndex = 5
        Me.txtMaxTimeCAN.Text = "2"
        Me.ToolTip1.SetToolTip(Me.txtMaxTimeCAN, "Time Out in seconds")
        '
        'chkThreeSamplesCAN
        '
        Me.chkThreeSamplesCAN.BackColor = System.Drawing.SystemColors.Control
        Me.chkThreeSamplesCAN.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkThreeSamplesCAN.Location = New System.Drawing.Point(55, 155)
        Me.chkThreeSamplesCAN.Name = "chkThreeSamplesCAN"
        Me.chkThreeSamplesCAN.Size = New System.Drawing.Size(98, 17)
        Me.chkThreeSamplesCAN.TabIndex = 10
        Me.chkThreeSamplesCAN.Text = "Three Samples"
        Me.chkThreeSamplesCAN.UseVisualStyleBackColor = False
        '
        'chkSingleFilterCAN
        '
        Me.chkSingleFilterCAN.BackColor = System.Drawing.SystemColors.Control
        Me.chkSingleFilterCAN.Checked = True
        Me.chkSingleFilterCAN.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkSingleFilterCAN.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkSingleFilterCAN.Location = New System.Drawing.Point(338, 155)
        Me.chkSingleFilterCAN.Name = "chkSingleFilterCAN"
        Me.chkSingleFilterCAN.Size = New System.Drawing.Size(98, 17)
        Me.chkSingleFilterCAN.TabIndex = 11
        Me.chkSingleFilterCAN.Text = "Single Filter"
        Me.chkSingleFilterCAN.UseVisualStyleBackColor = False
        '
        'frameChannelCAN
        '
        Me.frameChannelCAN.BackColor = System.Drawing.SystemColors.Control
        Me.frameChannelCAN.Controls.Add(Me.OptChannel1CAN)
        Me.frameChannelCAN.Controls.Add(Me.OptChannel2CAN)
        Me.frameChannelCAN.ForeColor = System.Drawing.SystemColors.ControlText
        Me.frameChannelCAN.Location = New System.Drawing.Point(250, 16)
        Me.frameChannelCAN.Name = "frameChannelCAN"
        Me.frameChannelCAN.Size = New System.Drawing.Size(206, 38)
        Me.frameChannelCAN.TabIndex = 0
        Me.frameChannelCAN.TabStop = False
        Me.frameChannelCAN.Text = "Channel"
        '
        'OptChannel1CAN
        '
        Me.OptChannel1CAN.BackColor = System.Drawing.SystemColors.Control
        Me.OptChannel1CAN.Checked = True
        Me.OptChannel1CAN.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptChannel1CAN.Location = New System.Drawing.Point(18, 16)
        Me.OptChannel1CAN.Name = "OptChannel1CAN"
        Me.OptChannel1CAN.Size = New System.Drawing.Size(74, 16)
        Me.OptChannel1CAN.TabIndex = 0
        Me.OptChannel1CAN.TabStop = True
        Me.OptChannel1CAN.Text = "Channel 1"
        Me.OptChannel1CAN.UseVisualStyleBackColor = False
        '
        'OptChannel2CAN
        '
        Me.OptChannel2CAN.BackColor = System.Drawing.SystemColors.Control
        Me.OptChannel2CAN.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptChannel2CAN.Location = New System.Drawing.Point(106, 16)
        Me.OptChannel2CAN.Name = "OptChannel2CAN"
        Me.OptChannel2CAN.Size = New System.Drawing.Size(74, 16)
        Me.OptChannel2CAN.TabIndex = 1
        Me.OptChannel2CAN.Text = "Channel 2"
        Me.OptChannel2CAN.UseVisualStyleBackColor = False
        '
        'cmdLoadCAN
        '
        Me.cmdLoadCAN.BackColor = System.Drawing.SystemColors.Control
        Me.cmdLoadCAN.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdLoadCAN.Location = New System.Drawing.Point(477, 45)
        Me.cmdLoadCAN.Name = "cmdLoadCAN"
        Me.cmdLoadCAN.Size = New System.Drawing.Size(147, 25)
        Me.cmdLoadCAN.TabIndex = 12
        Me.cmdLoadCAN.Text = "Load Settings From Device"
        Me.cmdLoadCAN.UseVisualStyleBackColor = False
        '
        'cmdSendSettingsCAN
        '
        Me.cmdSendSettingsCAN.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSendSettingsCAN.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSendSettingsCAN.Location = New System.Drawing.Point(477, 77)
        Me.cmdSendSettingsCAN.Name = "cmdSendSettingsCAN"
        Me.cmdSendSettingsCAN.Size = New System.Drawing.Size(147, 25)
        Me.cmdSendSettingsCAN.TabIndex = 13
        Me.cmdSendSettingsCAN.Text = "Send Settings To Device"
        Me.cmdSendSettingsCAN.UseVisualStyleBackColor = False
        '
        'txtMessageIDCAN
        '
        Me.txtMessageIDCAN.AllowDrop = True
        Me.txtMessageIDCAN.BackColor = System.Drawing.SystemColors.Window
        Me.txtMessageIDCAN.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtMessageIDCAN.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMessageIDCAN.Location = New System.Drawing.Point(124, 58)
        Me.txtMessageIDCAN.MaxLength = 8
        Me.txtMessageIDCAN.Name = "txtMessageIDCAN"
        Me.txtMessageIDCAN.Size = New System.Drawing.Size(74, 20)
        Me.txtMessageIDCAN.TabIndex = 1
        Me.txtMessageIDCAN.Text = "0"
        Me.ToolTip1.SetToolTip(Me.txtMessageIDCAN, " 11 or 29 bits represented as Hex")
        '
        'cmbTimingValueCAN
        '
        Me.cmbTimingValueCAN.BackColor = System.Drawing.SystemColors.Window
        Me.cmbTimingValueCAN.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTimingValueCAN.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbTimingValueCAN.Location = New System.Drawing.Point(382, 58)
        Me.cmbTimingValueCAN.Name = "cmbTimingValueCAN"
        Me.cmbTimingValueCAN.Size = New System.Drawing.Size(74, 21)
        Me.cmbTimingValueCAN.TabIndex = 2
        '
        'tabOptions_Page9
        '
        Me.tabOptions_Page9.Controls.Add(Me.Atlas_SFP)
        Me.tabOptions_Page9.Controls.Add(Me.txtNumChars)
        Me.tabOptions_Page9.Controls.Add(Me.chkReceive)
        Me.tabOptions_Page9.Controls.Add(Me.chkSend)
        Me.tabOptions_Page9.Controls.Add(Me.Frame1)
        Me.tabOptions_Page9.Controls.Add(Me.Label14)
        Me.tabOptions_Page9.Location = New System.Drawing.Point(4, 22)
        Me.tabOptions_Page9.Name = "tabOptions_Page9"
        Me.tabOptions_Page9.Size = New System.Drawing.Size(634, 388)
        Me.tabOptions_Page9.TabIndex = 8
        Me.tabOptions_Page9.Text = "ATLAS"
        '
        'Atlas_SFP
        '
        Me.Atlas_SFP.ATLAS = Nothing
        Me.Atlas_SFP.BackColor = System.Drawing.SystemColors.Control
        Me.Atlas_SFP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Atlas_SFP.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Atlas_SFP.Location = New System.Drawing.Point(16, 37)
        Me.Atlas_SFP.Name = "Atlas_SFP"
        Me.Atlas_SFP.Parent_Object = Nothing
        Me.Atlas_SFP.Size = New System.Drawing.Size(397, 211)
        Me.Atlas_SFP.TabIndex = 256
        '
        'txtNumChars
        '
        Me.txtNumChars.BackColor = System.Drawing.SystemColors.Window
        Me.txtNumChars.Enabled = False
        Me.txtNumChars.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNumChars.Location = New System.Drawing.Point(421, 263)
        Me.txtNumChars.Name = "txtNumChars"
        Me.txtNumChars.Size = New System.Drawing.Size(90, 20)
        Me.txtNumChars.TabIndex = 237
        Me.txtNumChars.Visible = False
        '
        'chkReceive
        '
        Me.chkReceive.BackColor = System.Drawing.SystemColors.Control
        Me.chkReceive.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkReceive.Location = New System.Drawing.Point(129, 255)
        Me.chkReceive.Name = "chkReceive"
        Me.chkReceive.Size = New System.Drawing.Size(114, 33)
        Me.chkReceive.TabIndex = 236
        Me.chkReceive.Text = "Generate Receive Statement"
        Me.chkReceive.UseVisualStyleBackColor = False
        '
        'chkSend
        '
        Me.chkSend.BackColor = System.Drawing.SystemColors.Control
        Me.chkSend.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkSend.Location = New System.Drawing.Point(24, 255)
        Me.chkSend.Name = "chkSend"
        Me.chkSend.Size = New System.Drawing.Size(106, 33)
        Me.chkSend.TabIndex = 235
        Me.chkSend.Text = "Generate Send Statement"
        Me.chkSend.UseVisualStyleBackColor = False
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.OptCan)
        Me.Frame1.Controls.Add(Me.optGigabit2)
        Me.Frame1.Controls.Add(Me.optCOM1)
        Me.Frame1.Controls.Add(Me.optCom2)
        Me.Frame1.Controls.Add(Me.optGigabit1)
        Me.Frame1.Controls.Add(Me.optSerial)
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(429, 29)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Size = New System.Drawing.Size(163, 219)
        Me.Frame1.TabIndex = 197
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Bus Parameters"
        '
        'OptCan
        '
        Me.OptCan.BackColor = System.Drawing.SystemColors.Control
        Me.OptCan.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptCan.Location = New System.Drawing.Point(8, 174)
        Me.OptCan.Name = "OptCan"
        Me.OptCan.Size = New System.Drawing.Size(116, 16)
        Me.OptCan.TabIndex = 233
        Me.OptCan.Text = "CAN"
        Me.OptCan.UseVisualStyleBackColor = False
        '
        'optGigabit2
        '
        Me.optGigabit2.BackColor = System.Drawing.SystemColors.Control
        Me.optGigabit2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optGigabit2.Location = New System.Drawing.Point(8, 144)
        Me.optGigabit2.Name = "optGigabit2"
        Me.optGigabit2.Size = New System.Drawing.Size(116, 17)
        Me.optGigabit2.TabIndex = 231
        Me.optGigabit2.Text = "Gigabit Ethernet 2"
        Me.optGigabit2.UseVisualStyleBackColor = False
        '
        'optCOM1
        '
        Me.optCOM1.BackColor = System.Drawing.SystemColors.Control
        Me.optCOM1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optCOM1.Location = New System.Drawing.Point(8, 24)
        Me.optCOM1.Name = "optCOM1"
        Me.optCOM1.Size = New System.Drawing.Size(116, 17)
        Me.optCOM1.TabIndex = 201
        Me.optCOM1.Text = "COM 1"
        Me.optCOM1.UseVisualStyleBackColor = False
        '
        'optCom2
        '
        Me.optCom2.BackColor = System.Drawing.SystemColors.Control
        Me.optCom2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optCom2.Location = New System.Drawing.Point(8, 54)
        Me.optCom2.Name = "optCom2"
        Me.optCom2.Size = New System.Drawing.Size(116, 17)
        Me.optCom2.TabIndex = 200
        Me.optCom2.Text = "COM 2"
        Me.optCom2.UseVisualStyleBackColor = False
        '
        'optGigabit1
        '
        Me.optGigabit1.BackColor = System.Drawing.SystemColors.Control
        Me.optGigabit1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optGigabit1.Location = New System.Drawing.Point(8, 114)
        Me.optGigabit1.Name = "optGigabit1"
        Me.optGigabit1.Size = New System.Drawing.Size(116, 17)
        Me.optGigabit1.TabIndex = 199
        Me.optGigabit1.Text = "Gigabit Ethernet 1"
        Me.optGigabit1.UseVisualStyleBackColor = False
        '
        'optSerial
        '
        Me.optSerial.BackColor = System.Drawing.SystemColors.Control
        Me.optSerial.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optSerial.Location = New System.Drawing.Point(8, 84)
        Me.optSerial.Name = "optSerial"
        Me.optSerial.Size = New System.Drawing.Size(116, 17)
        Me.optSerial.TabIndex = 198
        Me.optSerial.Text = "PCI Serial"
        Me.optSerial.UseVisualStyleBackColor = False
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(275, 255)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(147, 33)
        Me.Label14.TabIndex = 238
        Me.Label14.Text = "Number of characters you are expecting to receive."
        Me.Label14.Visible = False
        '
        'tabOptions_Page10
        '
        Me.tabOptions_Page10.Controls.Add(Me.Panel_Conifg)
        Me.tabOptions_Page10.Location = New System.Drawing.Point(4, 22)
        Me.tabOptions_Page10.Name = "tabOptions_Page10"
        Me.tabOptions_Page10.Size = New System.Drawing.Size(634, 388)
        Me.tabOptions_Page10.TabIndex = 9
        Me.tabOptions_Page10.Text = "Options"
        '
        'Panel_Conifg
        '
        Me.Panel_Conifg.BackColor = System.Drawing.SystemColors.Control
        Me.Panel_Conifg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Panel_Conifg.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Panel_Conifg.Location = New System.Drawing.Point(24, 45)
        Me.Panel_Conifg.Name = "Panel_Conifg"
        Me.Panel_Conifg.Parent_Object = Nothing
        Me.Panel_Conifg.Refresh = CType(0, Short)
        Me.Panel_Conifg.Size = New System.Drawing.Size(196, 152)
        Me.Panel_Conifg.TabIndex = 257
        '
        'optXMLReceive_5
        '
        Me.optXMLReceive_5.BackColor = System.Drawing.SystemColors.Control
        Me.optXMLReceive_5.Enabled = False
        Me.optXMLReceive_5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optXMLReceive_5.Location = New System.Drawing.Point(113, 8)
        Me.optXMLReceive_5.Name = "optXMLReceive_5"
        Me.optXMLReceive_5.Size = New System.Drawing.Size(98, 17)
        Me.optXMLReceive_5.TabIndex = 183
        Me.optXMLReceive_5.Text = "XML Instruction"
        Me.optXMLReceive_5.UseVisualStyleBackColor = False
        Me.optXMLReceive_5.Visible = False
        '
        'optDataReceive_5
        '
        Me.optDataReceive_5.BackColor = System.Drawing.SystemColors.Control
        Me.optDataReceive_5.Checked = True
        Me.optDataReceive_5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optDataReceive_5.Location = New System.Drawing.Point(218, 8)
        Me.optDataReceive_5.Name = "optDataReceive_5"
        Me.optDataReceive_5.Size = New System.Drawing.Size(50, 17)
        Me.optDataReceive_5.TabIndex = 182
        Me.optDataReceive_5.TabStop = True
        Me.optDataReceive_5.Text = "Data"
        Me.optDataReceive_5.UseVisualStyleBackColor = False
        '
        'optDataReceive_7
        '
        Me.optDataReceive_7.BackColor = System.Drawing.SystemColors.Control
        Me.optDataReceive_7.Checked = True
        Me.optDataReceive_7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optDataReceive_7.Location = New System.Drawing.Point(218, 8)
        Me.optDataReceive_7.Name = "optDataReceive_7"
        Me.optDataReceive_7.Size = New System.Drawing.Size(50, 17)
        Me.optDataReceive_7.TabIndex = 134
        Me.optDataReceive_7.TabStop = True
        Me.optDataReceive_7.Text = "Data"
        Me.optDataReceive_7.UseVisualStyleBackColor = False
        '
        'optXMLReceive_7
        '
        Me.optXMLReceive_7.BackColor = System.Drawing.SystemColors.Control
        Me.optXMLReceive_7.Enabled = False
        Me.optXMLReceive_7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optXMLReceive_7.Location = New System.Drawing.Point(113, 8)
        Me.optXMLReceive_7.Name = "optXMLReceive_7"
        Me.optXMLReceive_7.Size = New System.Drawing.Size(98, 17)
        Me.optXMLReceive_7.TabIndex = 133
        Me.optXMLReceive_7.Text = "XML Instruction"
        Me.optXMLReceive_7.UseVisualStyleBackColor = False
        Me.optXMLReceive_7.Visible = False
        '
        'frmBus_IO
        '
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(678, 480)
        Me.Controls.Add(Me.cmdReset)
        Me.Controls.Add(Me.cmdQuit)
        Me.Controls.Add(Me.cmdHelp)
        Me.Controls.Add(Me.sbrUserInformation)
        Me.Controls.Add(Me.tabOptions)
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmBus_IO"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "BUS IO"
        CType(Me.sbrUserInformation_Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sbrUserInformation_Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabOptions.ResumeLayout(False)
        Me.tabOptions_Page1.ResumeLayout(False)
        Me.tabOptions_Page1.PerformLayout()
        Me.frameReceiveCom1.ResumeLayout(False)
        Me.frameSendCom1.ResumeLayout(False)
        Me.FrameCom1PortSettings.ResumeLayout(False)
        Me.FrameCom1PortSettings.PerformLayout()
        Me.tabOptions_Page2.ResumeLayout(False)
        Me.tabOptions_Page2.PerformLayout()
        Me.frameReceiveCom2.ResumeLayout(False)
        Me.frameSendCom2.ResumeLayout(False)
        Me.FrameCom2PortSettings.ResumeLayout(False)
        Me.FrameCom2PortSettings.PerformLayout()
        Me.tabOptions_Page3.ResumeLayout(False)
        Me.tabOptions_Page3.PerformLayout()
        Me.frameReceivePCISer.ResumeLayout(False)
        Me.frameSendPCISer.ResumeLayout(False)
        Me.framePciProtocol.ResumeLayout(False)
        Me.FramePciResponse.ResumeLayout(False)
        Me.FramePciResponse.PerformLayout()
        Me.tabOptions_Page4.ResumeLayout(False)
        Me.tabOptions_Page4.PerformLayout()
        Me.frameReceiveGigabit1.ResumeLayout(False)
        Me.frameSendGigabit1.ResumeLayout(False)
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
        Me.FrameProtocolGigabit1.ResumeLayout(False)
        Me.tabOptions_Page5.ResumeLayout(False)
        Me.tabOptions_Page5.PerformLayout()
        Me.FrameProtocolGigabit2.ResumeLayout(False)
        Me.frameSendGigabit2.ResumeLayout(False)
        Me.frameReceiveGigabit2.ResumeLayout(False)
        Me.Frame4.ResumeLayout(False)
        Me.Frame4.PerformLayout()
        Me.tabOptions_Page7.ResumeLayout(False)
        Me.tabOptions_Page7.PerformLayout()
        Me.frameIDTypeCAN.ResumeLayout(False)
        Me.frameIDTypeCAN.PerformLayout()
        Me.frameSendCAN.ResumeLayout(False)
        Me.frameReceiveCAN.ResumeLayout(False)
        Me.frameChannelCAN.ResumeLayout(False)
        Me.tabOptions_Page9.ResumeLayout(False)
        Me.tabOptions_Page9.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.tabOptions_Page10.ResumeLayout(False)
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
            Else
                Me.Cursor = Cursors.Default
            End If
        End Set
    End Property

    Public receivedxml As String
    Public receiveddata As String
    Public gigabit1PortNotChanged As Boolean
    Public udp As Boolean

    ' Loop Back Test variable and consts
    Private gLPSTATE As Short
    Private gLPCount As Short
    Const CLBSTART As Short = 0
    Const CLBSTATUS As Short = 1
    Const CLBRESULT As Short = 2

    ' Reset
    Private gResetCount As Short

    ' Launch Vender GUI
    Const C_RUNLABEL As String = "Run TCIM SAIS"
    Const C_STOPLABEL As String = "Stop TCIM SAIS"
    Const C_AMPGUIBATCH As String = "CBTSAMPGUI.bat"

    Public Sub ConfigGetCurrent()
        Dim sMsg As String

        sMsg = ""
        If tabOptions.TabPages(TAB_COM_1).Enabled = True Then
            ResourceName = "COM_1"
            ConfigureCom1(getSerial_settings())
        Else
            sMsg &= "COM 1 not available" & vbCrLf
        End If

        If tabOptions.TabPages(TAB_COM_2).Enabled = True Then
            ResourceName = "COM_2"
            ConfigureCom2(getSerial_settings())
        Else
            sMsg &= "COM 2 not available" & vbCrLf
        End If

        If tabOptions.TabPages(TAB_GIGABIT1).Enabled = True Then
            ResourceName = "ETHERNET_1"
            ConfigureGigabit1(getEthernet_settings())
            optXMLSendGigabit1.Checked = False
            optDataSendGigabit1.Checked = True
            optXMLReceiveGigabit1.Checked = False
            optDataReceiveGigabit1.Checked = True
        Else
            sMsg &= "Ethernet1 not available" & vbCrLf
        End If

        If tabOptions.TabPages(TAB_PCISER).Enabled = True Then
            If optRS232.Checked Then
                ResourceName = "COM_5"
            ElseIf optRS422.Checked Then
                ResourceName = "COM_4"
            Else
                ResourceName = "COM_3"
            End If
            ConfigurePCISer(getSerial_settings())
        Else
            sMsg &= "PCI Serial not available" & vbCrLf
        End If

        If tabOptions.TabPages.Item(TAB_GIGABIT2).Enabled = True Then
            ResourceName = "ETHERNET_2"
            ConfigureGigabit2(getEthernet_settings())
            optXMLSendGigabit2.Checked = False
            optDataSendGigabit2.Checked = True
            optXMLReceiveGigabit2.Checked = False
            optDataReceiveGigabit2.Checked = True
        Else
            sMsg &= "Ethernet2 bus not available" & vbCrLf
        End If

        If tabOptions.TabPages.Item(TAB_CAN).Enabled = True Then
            getCan_settings()
        Else
            sMsg &= "CAN bus not available" & vbCrLf
        End If

        If sMsg <> "" Then
            MsgBox(sMsg, MsgBoxStyle.Information, "Bus I/O Current Config")
        End If
    End Sub

    Public Function Build_Atlas() As String
        Build_Atlas = ""
        Dim remoteip As String
        Dim localip As String
        Dim gateway As String
        Dim submask As String
        Dim channel As String

        Dim sTestString As String = ""
        Dim senddata As String = ""
        Dim hexchars() As String
        Dim h As Integer
        Dim i As Integer
        Dim parity As String
        Dim bitrate As String
        Dim maxt As String
        Dim stopbits As String
        Dim wordlength As String

        If optCOM1.Checked = True Then
            If IIf(chkReceive.Checked, 1, 0) = 1 Then
                Do While txtNumChars.Text = ""
                    txtNumChars.Text = InputBox("How many characters do you wish to receive?", "Number of characters to receive")
                Loop
            End If
            If optXMLSendCom1.Checked = True Then
                senddata = ""
            End If
            If optDataSendCom1.Checked = True Then
                hexchars = converttohex(txtDataSentCom1.Text)
                h = UBound(hexchars)
                For i = 0 To h
                    If hexchars(i) <> "" Or hexchars(i) <> " " Then
                        If i = 0 Then
                            senddata = "X'" & hexchars(i) & "'"
                        Else
                            senddata &= ", X'" & Trim(hexchars(i)) & "'"
                        End If
                    End If
                Next i
            End If

            Select Case cmbParityCom1.Items.Item(cmbParityCom1.SelectedIndex).ToString()
                Case "Odd"
                    parity = "ODD"
                Case "Even"
                    parity = "EVEN"
                Case "Mark"
                    parity = "MARK"
                Case "None"
                    parity = "NONE"

                Case Else
                    parity = "NONE"
            End Select

            bitrate = Trim(Str(Val(cmbBitRateCom1.Items.Item(cmbBitRateCom1.SelectedIndex).ToString())))
            stopbits = Trim(Str(Val(cmbStopBitsCom1.Items.Item(cmbStopBitsCom1.SelectedIndex).ToString())))
            wordlength = Trim(Str(Val(txtWordLengthCom1.Text)))

            sTestString = "ESTABLISH, BUS PROTOCOL 'RS422_1', SPEC 'RS422'," & vbCrLf & "  STANDARD PRIMARY BUS," & vbCrLf & "  BUS-PARAMETER BIT-RATE " & bitrate & " BITS/SEC," & vbCrLf & "  BUS-PARAMETER STOP-BITS " & stopbits & " BITS," & vbCrLf & "  BUS-PARAMETER PARITY " & parity & "," & vbCrLf & "  BUS-PARAMETER WORD-LENGTH " & wordlength & " BITS," & vbCrLf & "  CNX HI LO $" & vbCrLf & vbCrLf
            If IIf(chkReceive.Checked, 1, 0) = 1 Then
                sTestString &= "DECLARE, VARIABLE, 'DATA WORDS-L' IS ARRAY(1 THRU " & Trim(Str(Val(txtNumChars.Text))) & ") OF STRING(" & wordlength & ") OF BIT $" & vbCrLf & vbCrLf
            End If
            If IIf(chkSend.Checked, 1, 0) = 1 Then
                sTestString &= "DEFINE, 'TEST1001-T', EXCHANGE, PROTOCOL 'RS422_1'," & vbCrLf & "  BUS-MODE ALL-LISTENER," & vbCrLf & "  DATA " & senddata & " $" & vbCrLf & vbCrLf
            End If
            If IIf(chkReceive.Checked, 1, 0) = 1 Then
                sTestString &= "DEFINE, 'TEST1001-L', EXCHANGE, PROTOCOL 'RS422_1'," & vbCrLf & "  BUS-MODE TALKER-LISTENER," & vbCrLf
            End If
            If IIf(chkSend.Checked, 1, 0) = CheckState.Checked Then
                sTestString &= "TALKER TEST-EQUIP," & vbCrLf & "LISTENER 'UUT'," & vbCrLf
            End If
            If IIf(chkReceive.Checked, 1, 0) = CheckState.Checked Then
                sTestString &= "TALKER 'UUT'," & vbCrLf & "LISTENER TEST-EQUIP," & vbCrLf
            End If
            If IIf(chkSend.Checked, 1, 0) = 1 Then
                sTestString &= "DO, EXCHANGE," & vbCrLf & "  EXCHANGE 'TEST1001-T'," & vbCrLf & "  WAIT, MAX-TIME 5 SEC $" & vbCrLf & vbCrLf
            End If
            If IIf(chkReceive.Checked, 1, 0) = 1 Then
                sTestString &= "DO, EXCHANGE," & vbCrLf & "  EXCHANGE 'TEST1001-L'," & vbCrLf & "    TEST-EQUIP-MONITOR DATA 'DATA WORDS-L'(1 THRU 5)," & vbCrLf & "  WAIT, MAX-TIME 'MAX-T'5 SEC $"
            End If
        End If

        If optCom2.Checked = True Then
            If IIf(chkReceive.Checked, 1, 0) = 1 Then
                Do While txtNumChars.Text = ""
                    txtNumChars.Text = InputBox("How many characters do you wish to receive?", "Number of characters to receive")
                Loop
            End If
            If optXMLSendCom2.Checked = True Then
                senddata = ""
            End If
            If optDataSendCom2.Checked = True Then
                hexchars = converttohex(txtDataSentCom2.Text)
                h = UBound(hexchars)
                For i = 0 To h
                    If hexchars(i) <> "" Or hexchars(i) <> " " Then
                        If i = 0 Then
                            senddata = "X'" & hexchars(i) & "'"
                        Else
                            senddata &= ", X'" & Trim(hexchars(i)) & "'"
                        End If
                    End If
                Next i
            End If
            Select Case cmbParityCom2.Items.Item(cmbParityCom2.SelectedIndex).ToString()
                Case "Odd"
                    parity = "ODD"
                Case "Even"
                    parity = "EVEN"
                Case "Mark"
                    parity = "MARK"
                Case "None"
                    parity = "NONE"

                Case Else
                    parity = "NONE"
            End Select
            bitrate = Trim(Str(Val(cmbBitRateCom2.Items.Item(cmbBitRateCom2.SelectedIndex).ToString())))
            stopbits = Trim(Str(Val(cmbStopBitsCom2.Items.Item(cmbStopBitsCom2.SelectedIndex).ToString())))
            wordlength = Trim(Str(Val(txtWordLengthCom2.Text)))

            sTestString = "ESTABLISH, BUS PROTOCOL 'RS232_1', SPEC 'RS232'," & vbCrLf & "  STANDARD PRIMARY BUS," & vbCrLf & "  BUS-PARAMETER BIT-RATE " & bitrate & " BITS/SEC," & vbCrLf & "  BUS-PARAMETER STOP-BITS " & stopbits & " BITS," & vbCrLf & "  BUS-PARAMETER PARITY " & parity & "," & vbCrLf & "  BUS-PARAMETER WORD-LENGTH " & wordlength & " BITS," & vbCrLf & "  CNX HI LO $" & vbCrLf & vbCrLf
            If IIf(chkReceive.Checked, 1, 0) = 1 Then
                sTestString &= "DECLARE, VARIABLE, 'DATA WORDS-L' IS ARRAY(1 THRU " & Trim(Str(Val(txtNumChars.Text))) & ") OF STRING(" & wordlength & ") OF BIT $" & vbCrLf & vbCrLf
            End If
            If IIf(chkSend.Checked, 1, 0) = 1 Then
                sTestString &= "DEFINE, 'TEST1001-T', EXCHANGE, PROTOCOL 'RS232_1'," & vbCrLf & "  BUS-MODE ALL-LISTENER," & vbCrLf & "  DATA " & senddata & " $" & vbCrLf & vbCrLf
            End If
            If IIf(chkReceive.Checked, 1, 0) = 1 Then
                sTestString &= "DEFINE, 'TEST1001-L', EXCHANGE, PROTOCOL 'RS232_1'," & vbCrLf & "  BUS-MODE TALKER-LISTENER," & vbCrLf
            End If
            If IIf(chkSend.Checked, 1, 0) = CheckState.Checked Then
                sTestString &= "TALKER TEST-EQUIP," & vbCrLf & "LISTENER 'UUT'," & vbCrLf
            End If
            If IIf(chkReceive.Checked, 1, 0) = CheckState.Checked Then
                sTestString &= "TALKER 'UUT'," & vbCrLf & "LISTENER TEST-EQUIP," & vbCrLf
            End If
            If IIf(chkSend.Checked, 1, 0) = 1 Then
                sTestString &= "DO, EXCHANGE," & vbCrLf & "  EXCHANGE 'TEST1001-T'," & vbCrLf & "  WAIT, MAX-TIME 5 SEC $" & vbCrLf & vbCrLf
            End If
            If IIf(chkReceive.Checked, 1, 0) = 1 Then
                sTestString &= "DO, EXCHANGE," & vbCrLf & "  EXCHANGE 'TEST1001-L'," & vbCrLf & "    TEST-EQUIP-MONITOR DATA 'DATA WORDS-L'(1 THRU 5)," & vbCrLf & "  WAIT, MAX-TIME 'MAX-T'5 SEC $"
            End If
        End If

        If optSerial.Checked = True Then
            If IIf(chkReceive.Checked, 1, 0) = 1 Then
                Do While txtNumChars.Text = ""
                    txtNumChars.Text = InputBox("How many characters do you wish to receive?", "Number of characters to receive")
                Loop
            End If
            If optXMLSendPCISer.Checked = True Then
                senddata = ""
            End If
            If optDataSendPCISer.Checked = True Then
                hexchars = converttohex(txtDataSentPCISer.Text)
                h = UBound(hexchars)
                For i = 0 To h
                    If hexchars(i) <> "" Or hexchars(i) <> " " Then
                        If i = 0 Then
                            senddata = "X'" & hexchars(i) & "'"
                        Else
                            senddata &= ", X'" & Trim(hexchars(i)) & "'"
                        End If
                    End If
                Next i
            End If
            Select Case cmbParityPCISer.Items.Item(cmbParityPCISer.SelectedIndex).ToString()
                Case "Odd"
                    parity = "ODD"
                Case "Even"
                    parity = "EVEN"
                Case "Mark"
                    parity = "MARK"
                Case "None"
                    parity = "NONE"

                Case Else
                    parity = "NONE"
            End Select
            bitrate = Trim(Str(Val(cmbBitRatePCISer.Items.Item(cmbBitRatePCISer.SelectedIndex).ToString()))) & "Hz"
            stopbits = Trim(Str(Val(cmbStopBitsPCISer.Items.Item(cmbStopBitsPCISer.SelectedIndex).ToString())))
            wordlength = Trim(Str(Val(txtSerialWordLengthPCISer.Text)))
            maxt = Trim(Str(Val(txtSerialMaxTimePCISer.Text)))

            If optRS232.Checked = True Then
                sTestString = "ESTABLISH, BUS PROTOCOL 'RS232_1', SPEC 'RS-232'," & vbCrLf & "  STANDARD PRIMARY BUS," & vbCrLf & "  BUS-PARAMETER BIT-RATE " & bitrate & " BITS/SEC," & vbCrLf & "  BUS-PARAMETER STOP-BITS " & stopbits & " BITS," & vbCrLf & "  BUS-PARAMETER PARITY " & parity & "," & vbCrLf & "  BUS-PARAMETER WORD-LENGTH " & wordlength & " BITS," & vbCrLf & "  CNX TRUE pin1 COMPL pin2 $" & vbCrLf & vbCrLf
                If IIf(chkReceive.Checked, 1, 0) = 1 Then
                    sTestString &= "DECLARE, VARIABLE, 'DATA WORDS-L' IS ARRAY(1 THRU " & Trim(Str(Val(txtNumChars.Text))) & ") OF STRING(" & wordlength & ") OF BIT $" & vbCrLf & vbCrLf
                End If
                If IIf(chkSend.Checked, 1, 0) = 1 Then
                    sTestString &= "DEFINE, 'TEST1001-T', EXCHANGE, PROTOCOL 'RS232_1'," & vbCrLf & "  BUS-MODE ALL-LISTENER," & vbCrLf & "  DATA " & senddata & " $" & vbCrLf & vbCrLf
                End If
                If IIf(chkReceive.Checked, 1, 0) = 1 Then
                    sTestString &= "DEFINE, 'TEST1001-L', EXCHANGE, PROTOCOL 'RS232_1'," & vbCrLf & "  BUS-MODE TALKER-LISTENER," & vbCrLf & "  TALKER 'UUT'," & vbCrLf & "  LISTENER TEST-EQUIP $" & vbCrLf & vbCrLf
                End If
                If IIf(chkSend.Checked, 1, 0) = 1 Then
                    sTestString &= "DO, EXCHANGE," & vbCrLf & "EXCHANGE 'TEST1001-T'," & vbCrLf & "WAIT, MAX-TIME " & maxt & " SEC $" & vbCrLf & vbCrLf
                End If
                If IIf(chkReceive.Checked, 1, 0) = 1 Then
                    sTestString &= "DO, EXCHANGE," & vbCrLf & "EXCHANGE 'TEST1001-L'," & vbCrLf & "  TEST-EQUIP-MONITOR DATA 'DATA WORDS-L'(1 THRU " & Trim(Str(Val(txtNumChars.Text))) & ")," & vbCrLf & "WAIT, MAX-TIME 'MAX-T'" & maxt & " SEC $"
                End If
            End If
            If optRS422.Checked = True Then
                sTestString = "ESTABLISH, BUS PROTOCOL 'RS422_1', SPEC 'RS-422'," & vbCrLf & "  STANDARD PRIMARY BUS," & vbCrLf & "  BUS-PARAMETER BIT-RATE " & bitrate & " BITS/SEC," & vbCrLf & "  BUS-PARAMETER STOP-BITS " & stopbits & " BITS," & vbCrLf & "  BUS-PARAMETER PARITY " & parity & "," & vbCrLf & "  BUS-PARAMETER WORD-LENGTH " & wordlength & " BITS," & vbCrLf & "  CNX TRUE pin1 COMPL pin2 $" & vbCrLf & vbCrLf
                If IIf(chkReceive.Checked, 1, 0) = 1 Then
                    sTestString &= "DECLARE, VARIABLE, 'DATA WORDS-L' IS ARRAY(1 THRU " & Trim(Str(Val(txtNumChars.Text))) & ") OF STRING(" & wordlength & ") OF BIT $" & vbCrLf & vbCrLf
                End If
                If IIf(chkSend.Checked, 1, 0) = 1 Then
                    sTestString &= "DEFINE, 'TEST1001-T', EXCHANGE, PROTOCOL 'RS422_1'," & vbCrLf & "  BUS-MODE ALL-LISTENER," & vbCrLf & "  DATA " & senddata & " $" & vbCrLf & vbCrLf
                End If
                If IIf(chkReceive.Checked, 1, 0) = 1 Then
                    sTestString &= "DEFINE, 'TEST1001-L', EXCHANGE, PROTOCOL 'RS422_1'," & vbCrLf & "  BUS-MODE TALKER-LISTENER," & vbCrLf & "  TALKER 'UUT'," & vbCrLf & "  LISTENER TEST-EQUIP $" & vbCrLf & vbCrLf
                End If
                If IIf(chkSend.Checked, 1, 0) = 1 Then
                    sTestString &= "DO, EXCHANGE," & vbCrLf & "EXCHANGE 'TEST1001-T'," & vbCrLf & "WAIT, MAX-TIME " & maxt & " SEC $" & vbCrLf & vbCrLf
                End If
                If IIf(chkReceive.Checked, 1, 0) = 1 Then
                    sTestString &= "DO, EXCHANGE," & vbCrLf & "EXCHANGE 'TEST1001-L'," & vbCrLf & "  TEST-EQUIP-MONITOR DATA 'DATA WORDS-L'(1 THRU " & Trim(Str(Val(txtNumChars.Text))) & ")," & vbCrLf & "WAIT, MAX-TIME 'MAX-T'" & maxt & " SEC $"
                End If
            End If
            If optRS485.Checked = True Then
                sTestString = "ESTABLISH, BUS PROTOCOL 'RS485_1', SPEC 'RS-485'," & vbCrLf & "  STANDARD PRIMARY BUS," & vbCrLf & "  BUS-PARAMETER BIT-RATE " & bitrate & " BITS/SEC," & vbCrLf & "  BUS-PARAMETER STOP-BITS " & stopbits & " BITS," & vbCrLf & "  BUS-PARAMETER PARITY " & parity & "," & vbCrLf & "  BUS-PARAMETER WORD-LENGTH " & wordlength & " BITS," & vbCrLf & "  CNX TRUE pin1 COMPL pin2 $" & vbCrLf & vbCrLf
                If IIf(chkReceive.Checked, 1, 0) = 1 Then
                    sTestString &= "DECLARE, VARIABLE, 'DATA WORDS-L' IS ARRAY(1 THRU " & Trim(Str(Val(txtNumChars.Text))) & ") OF STRING(" & wordlength & ") OF BIT $" & vbCrLf & vbCrLf
                End If
                If IIf(chkSend.Checked, 1, 0) = 1 Then
                    sTestString &= "DEFINE, 'TEST1001-T', EXCHANGE, PROTOCOL 'RS458_1'," & vbCrLf & "  BUS-MODE ALL-LISTENER," & vbCrLf & "  DATA " & senddata & " $" & vbCrLf & vbCrLf
                End If
                If IIf(chkReceive.Checked, 1, 0) = 1 Then
                    sTestString &= "DEFINE, 'TEST1001-L', EXCHANGE, PROTOCOL 'RS458_1'," & vbCrLf & "  BUS-MODE TALKER-LISTENER," & vbCrLf & "  TALKER 'UUT'," & vbCrLf & "  LISTENER TEST-EQUIP $" & vbCrLf & vbCrLf
                End If
                If IIf(chkSend.Checked, 1, 0) = 1 Then
                    sTestString &= "DO, EXCHANGE," & vbCrLf & "EXCHANGE 'TEST1001-T'," & vbCrLf & "WAIT, MAX-TIME " & maxt & " SEC $" & vbCrLf & vbCrLf
                End If
                If IIf(chkReceive.Checked, 1, 0) = 1 Then
                    sTestString &= "DO, EXCHANGE," & vbCrLf & "EXCHANGE 'TEST1001-L'," & vbCrLf & "  TEST-EQUIP-MONITOR DATA 'DATA WORDS-L'(1 THRU " & Trim(Str(Val(txtNumChars.Text))) & ")," & vbCrLf & "WAIT, MAX-TIME 'MAX-T'" & maxt & " SEC $"
                End If
            End If
        End If

        If optGigabit1.Checked = True Then
            Dim proto As String = "TCP"
            If optTCPGigabit1.Checked = True Then
                proto = "TCP"
            End If
            If optUDPGigabit1.Checked = True Then
                proto = "UDP"
            End If

            remoteip = stripspaces(txtRemoteIPGigabit1.Text)

            localip = stripspaces(txtLocalIPGigabit1.Text)

            gateway = stripspaces(txtGatewayGigabit1.Text)

            submask = stripspaces(txtLocalSubnetMaskGigabit1.Text)

            If optXMLSendGigabit1.Checked = True Then
                senddata = ""
            End If
            If optDataSendGigabit1.Checked = True Then
                senddata = txtDataSentGigabit1.Text
            End If

            sTestString = "ESTABLISH, BUS PROTOCOL 'Ethernet_1'," & vbCrLf & "SPEC 'ETHERNET'," & vbCrLf & "STANDARD PRIMARY BUS," & vbCrLf & "BUS-PARAMETER " & proto & "," & vbCrLf & "CNX HI $" & vbCrLf & vbCrLf
            If IIf(chkReceive.Checked, 1, 0) = CheckState.Checked Then
                sTestString &= "DECLARE, VARIABLE, 'DATA WORDS' IS STRING(" & Str(Val(txtNumChars.Text)) & ") OF CHAR $" & vbCrLf
            End If
            sTestString &= "DECLARE, TYPE, 'ADDRESS' IS STRING(20) OF CHAR $" & vbCrLf & "DECLARE, VARIABLE, 'CMD WORDS' IS ARRAY(1 THRU 4) OF 'ADDRESS' $" & vbCrLf & "DEFINE, 'Ethernet_2', EXCHANGE, PROTOCOL 'Ethernet_1'," & vbCrLf & "BUS-MODE TALKER-LISTENER," & vbCrLf
            If IIf(chkSend.Checked, 1, 0) = CheckState.Checked Then
                sTestString &= "TALKER TEST-EQUIP," & vbCrLf & "LISTENER 'UUT'," & vbCrLf
            End If
            If IIf(chkReceive.Checked, 1, 0) = CheckState.Checked Then
                sTestString &= "TALKER 'UUT'," & vbCrLf & "LISTENER TEST-EQUIP," & vbCrLf
            End If
            If IIf(chkSend.Checked, 1, 0) = CheckState.Checked Then
                sTestString &= "DATA C'" & senddata & "'," & vbCrLf
            End If
            sTestString &= "COMMAND 'CMD WORDS'(1 THRU 4) $" & vbCrLf & vbCrLf & "CALCULATE, 'CMD WORDS'(1) =C'" & remoteip & "'," & vbCrLf & "'CMD WORDS'(2) =C'" & localip & "'," & vbCrLf & "'CMD WORDS'(3) =C'" & submask & "'," & vbCrLf & "'CMD WORDS'(4) =C'" & gateway & "' $" & vbCrLf & vbCrLf & "DO, EXCHANGE," & vbCrLf & "EXCHANGE 'Ethernet_2'," & vbCrLf & "WAIT, MAX-TIME " & Trim(Str(Val(txtMaxTimeGigabit1.Text))) & " SEC $"
            If IIf(chkReceive.Checked, 1, 0) = 1 Then
                sTestString &= vbCrLf & "TEST-EQUIP-MONITOR DATA 'DATA WORDS', " & vbCrLf & "WAIT, MAX-TIME 'MAX-T'" & Trim(Str(Val(txtMaxTimeGigabit1.Text))) & " SEC $"
            End If
        End If

        If optGigabit2.Checked = True Then
            Dim proto As String = "TCP"
            If optTCPGigabit2.Checked = True Then
                proto = "TCP"
            End If
            If optUDPGigabit2.Checked = True Then
                proto = "UDP"
            End If

            remoteip = stripspaces(txtRemoteIPGigabit2.Text)

            localip = stripspaces(txtLocalIPGigabit2.Text)

            gateway = stripspaces(txtGatewayGigabit2.Text)

            submask = stripspaces(txtLocalSubnetMaskGigabit2.Text)

            If optXMLSendGigabit2.Checked = True Then
                senddata = ""
            End If
            If optDataSendGigabit2.Checked = True Then
                senddata = txtDataSentGigabit2.Text
            End If

            sTestString = "ESTABLISH, BUS PROTOCOL 'Ethernet_2'," & vbCrLf & "SPEC 'ETHERNET'," & vbCrLf & "STANDARD PRIMARY BUS," & vbCrLf & "BUS-PARAMETER " & proto & "," & vbCrLf & "CNX HI $" & vbCrLf & vbCrLf
            If IIf(chkReceive.Checked, 1, 0) = CheckState.Checked Then
                sTestString &= "DECLARE, VARIABLE, 'DATA WORDS' IS STRING(" & Str(Val(txtNumChars.Text)) & ") OF CHAR $" & vbCrLf
            End If
            sTestString &= "DECLARE, TYPE, 'ADDRESS' IS STRING(20) OF CHAR $" & vbCrLf & "DECLARE, VARIABLE, 'CMD WORDS' IS ARRAY(1 THRU 4) OF 'ADDRESS' $" & vbCrLf & "DEFINE, 'Ethernet_2', EXCHANGE, PROTOCOL 'Ethernet_2'," & vbCrLf & "BUS-MODE TALKER-LISTENER," & vbCrLf
            If IIf(chkSend.Checked, 1, 0) = CheckState.Checked Then
                sTestString &= "TALKER TEST-EQUIP," & vbCrLf & "LISTENER 'UUT'," & vbCrLf
            End If
            If IIf(chkReceive.Checked, 1, 0) = CheckState.Checked Then
                sTestString &= "TALKER 'UUT'," & vbCrLf & "LISTENER TEST-EQUIP," & vbCrLf
            End If
            If IIf(chkSend.Checked, 1, 0) = CheckState.Checked Then
                sTestString &= "DATA C'" & senddata & "'," & vbCrLf
            End If
            sTestString &= "COMMAND 'CMD WORDS'(1 THRU 4) $" & vbCrLf & vbCrLf & "CALCULATE, 'CMD WORDS'(1) =C'" & remoteip & "'," & vbCrLf & "'CMD WORDS'(2) =C'" & localip & "'," & vbCrLf & "'CMD WORDS'(3) =C'" & submask & "'," & vbCrLf & "'CMD WORDS'(4) =C'" & gateway & "' $" & vbCrLf & vbCrLf & "DO, EXCHANGE," & vbCrLf & "EXCHANGE 'Ethernet_2'," & vbCrLf & "WAIT, MAX-TIME " & Trim(Str(Val(txtMaxTimeGigabit2.Text))) & " SEC $"
            If IIf(chkReceive.Checked, 1, 0) = 1 Then
                sTestString &= vbCrLf & "TEST-EQUIP-MONITOR DATA 'DATA WORDS', " & vbCrLf & "WAIT, MAX-TIME 'MAX-T'" & Trim(Str(Val(txtMaxTimeGigabit2.Text))) & " SEC $"
            End If
        End If

        If OptCan.Checked = True Then
            maxt = Trim(Str(Val(txtMaxTimeCAN.Text)))
            If Val(maxt) > 10 Then
                maxt = "10"
            End If

            Dim TimingValue As String
            TimingValue = GetCANTimingValue()

            Dim threeSamples As String
            If IIf(chkThreeSamplesCAN.Checked, 1, 0) = CheckState.Checked Then
                threeSamples = "1"
            Else
                threeSamples = "0"
            End If

            Dim singleFilter As String
            If IIf(chkSingleFilterCAN.Checked, 1, 0) = CheckState.Checked Then
                singleFilter = "1"
            Else
                singleFilter = "0"
            End If

            Dim acceptanceCode As String
            acceptanceCode = UCase(Trim(txtAcceptanceCodeCAN.Text))
            If Mid(acceptanceCode, 1, 2) = "0x" Or Mid(acceptanceCode, 1, 2) = "0X" Then
                acceptanceCode = Mid(acceptanceCode, 3)
            End If
            Do While Len(acceptanceCode) < 8
                acceptanceCode = "0" & acceptanceCode
            Loop

            Dim acceptanceMask As String
            acceptanceMask = UCase(Trim(txtAcceptanceMaskCAN.Text))
            If Mid(acceptanceMask, 1, 2) = "0x" Or Mid(acceptanceMask, 1, 2) = "0X" Then
                acceptanceMask = Mid(acceptanceMask, 3)
            End If
            Do While Len(acceptanceMask) < 8
                acceptanceMask = "0" & acceptanceMask
            Loop


            If OptChannel1CAN.Checked = True Then
                channel = "1"
            End If
            If OptChannel2CAN.Checked = True Then
                channel = "2"
            End If

            If optXMLSendCAN.Checked = True Then
                senddata = ""
            End If
            If optDataSendCAN.Checked = True Then
                senddata = txtDataSendCAN.Text
            End If

            sTestString = "ESTABLISH, BUS PROTOCOL 'can'," & vbCrLf & "SPEC 'CAN'," & vbCrLf & "STANDARD PRIMARY BUS," & vbCrLf & "BUS-PARAMETER TIMING-VALUE " & TimingValue & " BITS/SEC," & vbCrLf & "BUS-PARAMETER THREE-SAMPLES " & threeSamples & "," & vbCrLf & "BUS-PARAMETER SINGLE-FILTER " & singleFilter & "," & vbCrLf & "BUS-PARAMETER ACCEPTANCE-CODE X'" & acceptanceCode & "'," & vbCrLf & "BUS-PARAMETER ACCEPTANCE-MASK X'" & acceptanceMask & "'," & vbCrLf & "CNX TRUE CAN2-HI COMPL CAN2-LO $" & vbCrLf & vbCrLf
            If IIf(chkReceive.Checked, 1, 0) = 1 Then
                sTestString &= "DECLARE, VARIABLE, 'DATA WORDS' IS STRING(" & Trim(Str(Val(txtNumChars.Text))) & ") OF CHAR $" & vbCrLf & vbCrLf
            End If
            sTestString &= "DEFINE, 'test', EXCHANGE, PROTOCOL 'can'," & vbCrLf & "BUS-MODE TALKER-LISTENER," & vbCrLf
            If IIf(chkSend.Checked, 1, 0) = CheckState.Checked Then
                sTestString &= "TALKER TEST-EQUIP," & vbCrLf & "LISTENER 'UUT'," & vbCrLf
            End If
            If IIf(chkReceive.Checked, 1, 0) = CheckState.Checked Then
                sTestString &= "TALKER 'UUT'," & vbCrLf & "LISTENER TEST-EQUIP," & vbCrLf
            End If

            If IIf(chkSend.Checked, 1, 0) = CheckState.Checked Then
                sTestString &= "," & vbCrLf & "DATA C'" & senddata & "' $" & vbCrLf & vbCrLf
            Else
                sTestString &= " $" & vbCrLf & vbCrLf
            End If
            sTestString &= "DO, EXCHANGE," & vbCrLf & "EXCHANGE 'test'," & vbCrLf
            If IIf(chkReceive.Checked, 1, 0) = 1 Then
                sTestString &= "TEST-EQUIP-MONITOR DATA 'DATA WORDS'," & vbCrLf
            End If
            sTestString &= "WAIT, MAX-TIME 'MAX-T'" & maxt & " SEC $"
        End If

        Build_Atlas = sTestString
    End Function

    Function stripspaces(ByVal ip As String) As String
        stripspaces = ""
        Dim i As Integer
        Dim c As String
        Dim temp As String
        temp = ""
        For i = 1 To Len(ip)
            c = Mid(ip, i, 1)
            If c = " " Then
                'do nothing
            Else
                temp &= c
            End If
        Next i
        stripspaces = temp
    End Function

    Public Sub SetMode(ByVal sMode As String)
        'No data to retrive so do nothing
    End Sub

    Public Function GetMode() As String
        GetMode = ""

        'No toolbar to save so just put something for the first line
        GetMode = "BUS"
    End Function

    Private Sub frmBus_IO_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Dim success As Integer = atxml_Close()
    End Sub

    Private Sub chkReceive_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkReceive.Click
        If IIf(chkReceive.Checked, 1, 0) = 1 Then
            txtNumChars.Enabled = True
            txtNumChars.Visible = True
            Label14.Visible = True
        Else
            txtNumChars.Enabled = False
            txtNumChars.Visible = False
            Label14.Visible = False
        End If
    End Sub

    Private Sub chkSingleFilter_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkSingleFilterCAN.Click
        Dim temp As String
        If tabOptions.SelectedIndex = TAB_CAN Then

            temp = ""
            If IIf(chkThreeSamplesCAN.Checked, 1, 0) = CheckState.Checked Then
                temp = "1"
            Else
                temp = "0"
            End If
            If IIf(chkSingleFilterCAN.Checked, 1, 0) = CheckState.Checked Then
                temp &= "1"
            Else
                temp &= "0"
            End If
            If OptChannel1CAN.Checked = True Then
                temp &= "1"
            End If
            If OptChannel2CAN.Checked = True Then
                temp &= "2"
            End If
            txtSaveTCPCAN.Text = temp
        End If
    End Sub

    Private Sub chkThreeSamples_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkThreeSamplesCAN.Click
        Dim temp As String
        If tabOptions.SelectedIndex = TAB_CAN Then

            temp = ""
            If IIf(chkThreeSamplesCAN.Checked, 1, 0) = CheckState.Checked Then
                temp = "1"
            Else
                temp = "0"
            End If
            If IIf(chkSingleFilterCAN.Checked, 1, 0) = CheckState.Checked Then
                temp &= "1"
            Else
                temp &= "0"
            End If
            If OptChannel1CAN.Checked = True Then
                temp &= "1"
            End If
            If OptChannel2CAN.Checked = True Then
                temp &= "2"
            End If
            txtSaveTCPCAN.Text = temp
        End If

    End Sub

    Private Sub cmdDataReceiveCom1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDataReceiveCom1.Click
        Dim configData As SerialConfig = BuildCOM1ConfigData()

        ReceiveSerialData(configData)
        If optXMLReceiveCom1.Checked = True Then
            txtDataReceivedCom1.Text = receivedxml
        End If

        If optDataReceiveCom1.Checked = True Then
            txtDataReceivedCom1.Text = receiveddata
        End If
    End Sub

    Private Sub cmdDataReceiveCom2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDataReceiveCom2.Click
        Dim configData As SerialConfig = BuildCOM2ConfigData()

        ReceiveSerialData(configData)
        If optXMLReceiveCom2.Checked = True Then
            txtDataReceivedCom2.Text = receivedxml
        End If
        If optDataReceiveCom2.Checked = True Then
            txtDataReceivedCom2.Text = receiveddata
        End If
    End Sub

    Private Sub cmdDataReceiveGigabit1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDataReceiveGigabit1.Click
        ReceiveGigabit1Data(False)
        If optXMLReceiveGigabit1.Checked = True Then
            receivedxml = txtDataReceivedGigabit1.Text
        End If
        If optDataReceiveGigabit1.Checked = True Then
            receiveddata = txtDataReceivedGigabit1.Text
        End If
    End Sub

    Private Sub cmdDataReceivePCISer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDataReceivePCISer.Click
        Dim configData As SerialConfig = BuildPCISerConfigData()

        ReceiveSerialData(configData)
        If optXMLReceivePCISer.Checked = True Then
            txtDataReceivedPCISer.Text = receivedxml
        End If
        If optDataReceivePCISer.Checked = True Then
            txtDataReceivedPCISer.Text = receiveddata
        End If
    End Sub

    Private Sub cmdDataReceiveGigabit2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDataReceiveGigabit2.Click
        ReceiveGigabit2Data(False)
        If optXMLReceiveGigabit2.Checked = True Then
            receivedxml = txtDataReceivedGigabit2.Text
        End If
        If optDataReceiveGigabit2.Checked = True Then
            receiveddata = txtDataReceivedGigabit2.Text
        End If
    End Sub

    Private Sub cmdDataReceiveCAN_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDataReceiveCAN.Click
        ReceiveCanData()
        If optXMLReceiveCAN.Checked = True Then
            receivedxml = txtDataReceivedCAN.Text
        End If
        If optDataReceiveCAN.Checked = True Then
            receiveddata = txtDataReceivedCAN.Text
        End If
    End Sub

    Private Sub cmdHelp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdHelp.Click
        frmHelp.ShowDialog()
    End Sub

    Private Sub cmdLoadCom1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdLoadCom1.Click
        ResourceName = "COM_1"
        ConfigureCom1(getSerial_settings())
    End Sub

    Private Sub cmdLoadCom2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdLoadCom2.Click
        ResourceName = "COM_2"
        ConfigureCom2(getSerial_settings())
    End Sub

    Private Sub cmdLoadGigabit1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdLoadGigabit1.Click
        ResourceName = "ETHERNET_1"
        ConfigureGigabit1(getEthernet_settings())
    End Sub

    Private Sub cmdLoadPCISer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdLoadPCISer.Click
        If optRS232.Checked Then
            ResourceName = "COM_5"
        ElseIf optRS422.Checked Then
            ResourceName = "COM_4"
        Else
            ResourceName = "COM_3"
        End If
        ConfigurePCISer(getSerial_settings())
    End Sub

    Private Sub cmdLoadGigabit2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdLoadGigabit2.Click
        ResourceName = "ETHERNET_2"
        ConfigureGigabit2(getEthernet_settings())
    End Sub

    Private Sub cmdLoadCAN_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdLoadCAN.Click
        getCan_settings()
    End Sub

    Private Sub cmdQuit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdQuit.Click
        cmdQuit_Click()
    End Sub
    Public Sub cmdQuit_Click()
        Me.Close()
    End Sub

    Private Sub cmdReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdReset.Click
        Dim Response As String = ""
        Dim SerialConfigData As SerialConfig = New SerialConfig()
        Dim NetworkConfigData As NetworkConfig = New NetworkConfig()

        'Block Controls in Debug mode
        If Panel_Conifg.DebugMode = True Then Exit Sub

        cmdQuit.Enabled = False

        If tabOptions.TabPages.Item(TAB_COM_1).Enabled Then
            SerialConfigData = BuildCOM1ConfigData()
            ResetSerial(SerialConfigData)
        End If

        If tabOptions.TabPages.Item(TAB_COM_2).Enabled Then
            SerialConfigData = BuildCOM2ConfigData()
            ResetSerial(SerialConfigData)
        End If

        If tabOptions.TabPages.Item(TAB_GIGABIT1).Enabled Then
            NetworkConfigData = BuildGigabit1ConfigData()
            ResetNetwork(NetworkConfigData)
        End If

        If tabOptions.TabPages.Item(TAB_PCISER).Enabled Then
            SerialConfigData = BuildPCISerConfigData()
            ResetSerial(SerialConfigData)
        End If

        If tabOptions.TabPages.Item(TAB_GIGABIT2).Enabled Then
            NetworkConfigData = BuildGigabit2ConfigData()
            ResetNetwork(NetworkConfigData)
        End If

        If tabOptions.TabPages.Item(TAB_CAN).Enabled Then
            ResetCan()
        End If

        System.Threading.Thread.Sleep(5000)
        cmdQuit.Enabled = True
    End Sub

    Private Sub cmdSendDataCom1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSendDataCom1.Click
        cmdSendData_Click(TAB_COM_1)
    End Sub

    Private Sub cmdSendDataCom2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSendDataCom2.Click
        cmdSendData_Click(TAB_COM_2)
    End Sub

    Private Sub cmdSendDataGigabit1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSendDataGigabit1.Click
        cmdSendData_Click(TAB_GIGABIT1)
    End Sub

    Private Sub cmdSendDataPCISer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSendDataPCISer.Click
        cmdSendData_Click(TAB_PCISER)
    End Sub

    Private Sub cmdSendDataGigabit2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSendDataGigabit2.Click
        cmdSendData_Click(TAB_GIGABIT2)
    End Sub

    Private Sub cmdSendDataCAN_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSendDataCAN.Click
        cmdSendData_Click(TAB_CAN)
    End Sub

    Private Sub cmdSendData_Click(ByVal Index As Short)
        'Block Controls in Debug mode
        If Panel_Conifg.DebugMode = True Then Exit Sub

        Select Case Index
            Case TAB_COM_1
                If optXMLSendCom1.Checked = True Then
                    SendOutXML(txtDataSentCom1.Text, Index)
                Else
                    If optDataSendCom1.Checked = True Then
                        Dim configData As SerialConfig = BuildCOM1ConfigData()

                        SendSerialData(txtDataSentCom1.Text, configData, txtDataReceivedCom1)
                    End If
                End If

            Case TAB_COM_2
                If optXMLSendCom2.Checked = True Then
                    SendOutXML(txtDataSentCom2.Text, Index)
                Else
                    If optDataSendCom2.Checked = True Then
                        ResourceName = "COM_2"
                        Dim configData As SerialConfig = BuildCOM2ConfigData()

                        SendSerialData(txtDataSentCom2.Text, configData, txtDataReceivedCom2)
                    End If
                End If

            Case TAB_GIGABIT1
                If optXMLSendGigabit1.Checked = True Then
                    SendOutXML(txtDataSentGigabit1.Text, Index)
                Else
                    If optDataSendGigabit1.Checked = True Then
                        Try
                            Dim configData As NetworkConfig = BuildGigabit1ConfigData()
                            SendEtherData(txtDataSentGigabit1.Text, configData, txtDataReceivedGigabit1)
                        Catch ex As SystemException
                            MessageBox.Show(ex.Message, "Network Configruation or Send Error")
                        End Try
                    End If
                End If

            Case TAB_GIGABIT2
                If optXMLSendGigabit2.Checked = True Then
                    SendOutXML(txtDataSentGigabit2.Text, Index)
                Else
                    If optDataSendGigabit2.Checked = True Then
                        Try
                            Dim configData As NetworkConfig = BuildGigabit2ConfigData()
                            SendEtherData(txtDataSentGigabit2.Text, configData, txtDataReceivedGigabit2)
                        Catch ex As SystemException
                            MessageBox.Show(ex.Message, "Network Configruation or Send Error")
                        End Try
                    End If
                End If

            Case TAB_CAN
                If optXMLSendCAN.Checked = True Then
                    SendOutXML(txtDataSendCAN.Text, Index)
                Else
                    If optDataSendCAN.Checked = True Then
                        SendCanData()
                    End If
                End If

            Case TAB_PCISER
                If optXMLSendPCISer.Checked = True Then
                    SendOutXML(txtDataSentPCISer.Text, Index)
                Else
                    If optDataSendPCISer.Checked = True Then
                        Dim configData As SerialConfig = BuildPCISerConfigData()

                        SendSerialData(txtDataSentPCISer.Text, configData, txtDataReceivedPCISer)
                    End If
                End If
        End Select
    End Sub

    Public Function BuildCOM1ConfigData() As SerialConfig
        Dim configData As SerialConfig = New SerialConfig()

        BuildCOM1ConfigData = Nothing

        ResourceName = "COM_1"
        Select Case cmbParityPCISer.Items.Item(cmbParityCom1.SelectedIndex).ToString()
            Case "Odd"
                configData.parity = "ODD"
            Case "Even"
                configData.parity = "EVEN"
            Case "Mark"
                configData.parity = "MARK"
            Case "None"
                configData.parity = "NONE"

            Case Else
                configData.parity = "NONE"
        End Select

        configData.bitrate = Trim(Str(Val(cmbBitRateCom1.Items.Item(cmbBitRateCom1.SelectedIndex).ToString()))) & "Hz"
        configData.stopbits = Str(Val(cmbStopBitsCom1.Items.Item(cmbStopBitsCom1.SelectedIndex).ToString())) & "bits"
        configData.wordlength = Str(Val(txtWordLengthCom1.Text))
        configData.maxt = "5s"
        configData.protocol = "RS_422"
        configData.receivelength = txtReceiveLengthCom1.Text
        configData.ReadTime = txtReadTimeCom1.Text
        configData.delay = " 1e-005s"

        BuildCOM1ConfigData = configData
    End Function

    Public Function BuildCOM2ConfigData() As SerialConfig
        Dim configData As SerialConfig = New SerialConfig()

        BuildCOM2ConfigData = Nothing

        ResourceName = "COM_2"
        Select Case cmbParityPCISer.Items.Item(cmbParityCom2.SelectedIndex).ToString()
            Case "Odd"
                configData.parity = "ODD"
            Case "Even"
                configData.parity = "EVEN"
            Case "Mark"
                configData.parity = "MARK"
            Case "None"
                configData.parity = "NONE"

            Case Else
                configData.parity = "NONE"
        End Select

        configData.bitrate = Trim(Str(Val(cmbBitRateCom2.Items.Item(cmbBitRateCom2.SelectedIndex).ToString()))) & "Hz"
        configData.stopbits = Str(Val(cmbStopBitsCom2.Items.Item(cmbStopBitsCom2.SelectedIndex).ToString())) & "bits"
        configData.wordlength = Str(Val(txtWordLengthCom2.Text))
        configData.maxt = "5s"
        configData.protocol = "RS_232"
        configData.receivelength = txtReceiveLengthCom2.Text
        configData.ReadTime = txtReadTimeCom2.Text
        configData.delay = " 1e-005s"

        BuildCOM2ConfigData = configData
    End Function

    Public Function BuildPCISerConfigData() As SerialConfig
        Dim configData As SerialConfig = New SerialConfig()

        BuildPCISerConfigData = Nothing

        Select Case cmbParityPCISer.Items.Item(cmbParityPCISer.SelectedIndex).ToString()
            Case "Odd"
                configData.parity = "ODD"
            Case "Even"
                configData.parity = "EVEN"
            Case "Mark"
                configData.parity = "MARK"
            Case "None"
                configData.parity = "NONE"

            Case Else
                configData.parity = "NONE"
        End Select

        configData.bitrate = Trim(Str(Val(cmbBitRatePCISer.Items.Item(cmbBitRatePCISer.SelectedIndex).ToString()))) & "Hz"
        configData.stopbits = Str(Val(cmbStopBitsPCISer.Items.Item(cmbStopBitsPCISer.SelectedIndex).ToString())) & "bits"
        configData.wordlength = Str(Val(txtSerialWordLengthPCISer.Text))
        configData.maxt = Str(Val(txtSerialMaxTimePCISer.Text)) & "s"
        configData.delay = Str(Val(txtSerialDelayPCISer.Text)) & "s"
        configData.receivelength = txtReceiveLengthPCISer.Text
        configData.ReadTime = txtReadTimePCISer.Text

        If optRS485.Checked Then
            ResourceName = "COM_3"
            configData.protocol = "RS_485"
        ElseIf optRS422.Checked Then
            ResourceName = "COM_4"
            configData.protocol = "RS_422"
        Else
            ResourceName = "COM_5"
            configData.protocol = "RS_232"
        End If

        BuildPCISerConfigData = configData
    End Function

    Public Function BuildGigabit1ConfigData() As NetworkConfig
        Dim c As String
        Dim i As Integer
        Dim tempr As String = ""
        Dim configData As NetworkConfig = New NetworkConfig()

        ResourceName = "ETHERNET_1"

        configData.remoteip = txtRemoteIPGigabit1.Text
        configData.remport = txtRemotePortGigabit1.Text

        For i = 1 To Len(configData.remoteip)
            c = Mid(configData.remoteip, i, 1)
            Select Case c
                Case " "
                    'do nothing

                Case Else
                    tempr &= c
            End Select
        Next i
        configData.remoteip = tempr

        configData.localip = txtLocalIPGigabit1.Text
        'remove spaces from localip
        tempr = ""
        For i = 1 To Len(configData.localip)
            c = Mid(configData.localip, i, 1)
            If c = " " Then
                'do nothing
            Else
                tempr &= c
            End If
        Next i
        configData.localip = tempr

        configData.mask = txtLocalSubnetMaskGigabit1.Text
        'remove spaces from mask
        tempr = ""
        For i = 1 To Len(configData.mask)
            c = Mid(configData.mask, i, 1)
            If c = " " Then
                'do nothing
            Else
                tempr &= c
            End If
        Next i
        configData.mask = tempr

        configData.gateway = txtGatewayGigabit1.Text
        'remove spaces from mask
        tempr = ""
        Dim tmpaddr As String() = configData.gateway.Split(".")
        If tmpaddr(0).Trim().Length > 0 Then
            For i = 1 To Len(configData.gateway)
                c = Mid(configData.gateway, i, 1)
                If c = " " Then
                    'do nothing
                Else
                    tempr &= c
                End If
            Next i
        End If
        configData.gateway = tempr

        If optTCPGigabit1.Checked = True Then
            configData.protocol = "TCP"
        End If
        If optUDPGigabit1.Checked = True Then
            configData.protocol = "UDP"
        End If

        configData.maxt = Str(Val(txtMaxTimeGigabit1.Text)) & "s"

        Dim rangeerr As Boolean
        rangeerr = False
        'check for settings errors
        If Val(configData.remport) > 65535 Or Val(configData.remport) < 0 Then
            rangeerr = True
            Throw New SystemException("Remote port setting is out of range.  Please use a port setting of 0 to 65535.")
        End If
        'check to see if remote ip has correct settings
        tempr = ""
        For i = 1 To Len(configData.remoteip)
            c = Mid(configData.remoteip, i, 1)
            If c = "." Then
                If Trim(tempr) <> "" Then
                    If Val(tempr) > 255 Or Val(tempr) < 0 Then
                        rangeerr = True
                        Throw New SystemException("Remote IP setting is out of range.  Please use numbers between 0 and 255.")
                    End If
                End If
                tempr = ""
            Else
                tempr &= c
            End If
        Next i
        'check to see if local ip has correct settings
        tempr = ""
        For i = 1 To Len(configData.localip)
            c = Mid(configData.localip, i, 1)
            If c = "." Then
                If Trim(tempr) <> "" Then
                    If Val(tempr) > 255 Or Val(tempr) < 0 Then
                        rangeerr = True
                        Throw New SystemException("Local IP setting is out of range.  Please use numbers between 0 and 255.")
                    End If
                End If
                tempr = ""
            Else
                tempr &= c
            End If
        Next i

        'check to see if local gateway has correct settings
        tempr = ""
        For i = 1 To Len(configData.gateway)
            c = Mid(configData.gateway, i, 1)
            If c = "." Then
                If Trim(tempr) <> "" Then
                    If Val(tempr) > 255 Or Val(tempr) < 0 Then
                        rangeerr = True
                        Throw New SystemException("Local Gateway setting is out of range.  Please use numbers between 0 and 255.")
                    End If
                End If
                tempr = ""
            Else
                tempr &= c
            End If
        Next i

        'check to see if subnet mask has correct settings
        tempr = ""
        For i = 1 To Len(configData.mask)
            c = Mid(configData.mask, i, 1)
            If c = "." Then
                If Trim(tempr) <> "" Then
                    If Val(tempr) > 255 Or Val(tempr) < 0 Then
                        rangeerr = True
                        Throw New SystemException("Subnet Mask setting is out of range.  Please use numbers between 0 and 255.")
                    End If
                End If
                tempr = ""
            Else
                tempr &= c
            End If
        Next i

        BuildGigabit1ConfigData = configData
    End Function

    Public Function BuildGigabit2ConfigData() As NetworkConfig
        Dim c As String
        Dim i As Integer
        Dim tempr As String = ""
        Dim configData As NetworkConfig = New NetworkConfig()

        ResourceName = "ETHERNET_2"

        configData.remoteip = txtRemoteIPGigabit2.Text
        configData.remport = txtRemotePortGigabit2.Text

        For i = 1 To Len(configData.remoteip)
            c = Mid(configData.remoteip, i, 1)
            Select Case c
                Case " "
                    'do nothing

                Case Else
                    tempr &= c
            End Select
        Next i
        configData.remoteip = tempr

        configData.localip = txtLocalIPGigabit2.Text
        'remove spaces from localip
        tempr = ""
        For i = 1 To Len(configData.localip)
            c = Mid(configData.localip, i, 1)
            If c = " " Then
                'do nothing
            Else
                tempr &= c
            End If
        Next i
        configData.localip = tempr

        configData.mask = txtLocalSubnetMaskGigabit2.Text
        'remove spaces from mask
        tempr = ""
        For i = 1 To Len(configData.mask)
            c = Mid(configData.mask, i, 1)
            If c = " " Then
                'do nothing
            Else
                tempr &= c
            End If
        Next i
        configData.mask = tempr

        configData.gateway = txtGatewayGigabit2.Text
        'remove spaces from mask
        tempr = ""
        For i = 1 To Len(configData.gateway)
            c = Mid(configData.gateway, i, 1)
            If c = " " Then
                'do nothing
            Else
                tempr &= c
            End If
        Next i
        configData.gateway = tempr

        If optTCPGigabit2.Checked = True Then
            configData.protocol = "TCP"
        End If
        If optUDPGigabit2.Checked = True Then
            configData.protocol = "UDP"
        End If

        configData.maxt = Str(Val(txtMaxTimeGigabit2.Text)) & "s"

        Dim rangeerr As Boolean
        rangeerr = False
        'check for settings errors
        If Val(configData.remport) > 65535 Or Val(configData.remport) < 0 Then
            rangeerr = True
            Throw New SystemException("Remote port setting is out of range.  Please use a port setting of 0 to 65535.")
        End If
        'check to see if remote ip has correct settings
        tempr = ""
        For i = 1 To Len(configData.remoteip)
            c = Mid(configData.remoteip, i, 1)
            If c = "." Then
                If Trim(tempr) <> "" Then
                    If Val(tempr) > 255 Or Val(tempr) < 0 Then
                        rangeerr = True
                        Throw New SystemException("Remote IP setting is out of range.  Please use numbers between 0 and 255.")
                    End If
                End If
                tempr = ""
            Else
                tempr &= c
            End If
        Next i
        'check to see if local ip has correct settings
        tempr = ""
        For i = 1 To Len(configData.localip)
            c = Mid(configData.localip, i, 1)
            If c = "." Then
                If Trim(tempr) <> "" Then
                    If Val(tempr) > 255 Or Val(tempr) < 0 Then
                        rangeerr = True
                        Throw New SystemException("Local IP setting is out of range.  Please use numbers between 0 and 255.")
                    End If
                End If
                tempr = ""
            Else
                tempr &= c
            End If
        Next i

        'check to see if local gateway has correct settings
        tempr = ""
        For i = 1 To Len(configData.gateway)
            c = Mid(configData.gateway, i, 1)
            If c = "." Then
                If Trim(tempr) <> "" Then
                    If Val(tempr) > 255 Or Val(tempr) < 0 Then
                        rangeerr = True
                        Throw New SystemException("Local Gateway setting is out of range.  Please use numbers between 0 and 255.")
                    End If
                End If
                tempr = ""
            Else
                tempr &= c
            End If
        Next i

        'check to see if subnet mask has correct settings
        tempr = ""
        For i = 1 To Len(configData.mask)
            c = Mid(configData.mask, i, 1)
            If c = "." Then
                If Trim(tempr) <> "" Then
                    If Val(tempr) > 255 Or Val(tempr) < 0 Then
                        rangeerr = True
                        Throw New SystemException("Subnet Mask setting is out of range.  Please use numbers between 0 and 255.")
                    End If
                End If
                tempr = ""
            Else
                tempr &= c
            End If
        Next i

        BuildGigabit2ConfigData = configData
    End Function

    ''' <summary>
    ''' Builds a CAN configuration data structure from the controls on the CAN tab
    ''' </summary>
    ''' <returns>CAN configuration structure</returns>
    ''' <remarks>Does not trap any exceptions</remarks>
    Public Function BuildCANConfigData() As CANConfig
        Dim configData As CANConfig = New CANConfig()
        Dim ioFlags As Short = 0

        configData.messageID = txtMessageIDCAN.Text

        configData.data = txtDataSendCAN.Text
        While configData.data.Length < 16   ' must be 8 bytes
            configData.data = "0" & configData.data
        End While
        For i = 15 To 2 Step -2             ' insert comma delimiters per byte
            configData.data = configData.data.Insert(i - 1, ",")
        Next

        If optExtendedIDCAN.Checked Then
            ioFlags = CAN_EXTENDED_MSG_ID
        End If

        If chkRemoteFrameCAN.Checked Then
            ioFlags = ioFlags + CAN_REMOTE_FRAME
        End If

        If chkSingleShotCAN.Checked Then
            ioFlags = ioFlags + CAN_SINGLE_SHOT
        End If

        If chkSelfReceptionCAN.Checked Then
            ioFlags = ioFlags + CAN_SELF_RECEPTION
        End If

        configData.ioFlags = ioFlags.ToString()

        configData.dataBits = configData.messageID & "," & configData.ioFlags & "," & configData.data

        Dim maxt As String
        maxt = Trim(Str(Val(txtMaxTimeCAN.Text))) & "s"
        If maxt = "s" Then
            maxt = "0s"
        End If
        configData.maxTime = maxt

        configData.timing = GetCANTimingValue()

        If chkThreeSamplesCAN.Checked Then
            configData.threeSamples = "1"
        Else
            configData.threeSamples = "0"
        End If

        If chkSingleFilterCAN.Checked Then
            configData.singleFilter = "1"
        Else
            configData.singleFilter = "0"
        End If

        configData.acceptanceCode = BuildCANAcceptanceString(txtAcceptanceCodeCAN.Text)

        configData.acceptanceMask = BuildCANAcceptanceString(txtAcceptanceMaskCAN.Text)

        configData.channel = "1"
        If OptChannel1CAN.Checked = True Then
            configData.channel = "1"
        End If
        If OptChannel2CAN.Checked = True Then
            configData.channel = "2"
        End If

        configData.listenOnly = "0"

        BuildCANConfigData = configData
    End Function

    Private Sub cmdSendSettingsCom1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSendSettingsCom1.Click
        cmdSendSettings_Click(TAB_COM_1)
    End Sub

    Private Sub cmdSendSettingsCom2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSendSettingsCom2.Click
        cmdSendSettings_Click(TAB_COM_2)
    End Sub

    Private Sub cmdSendSettingsGigabit1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSendSettingsGigabit1.Click
        cmdSendSettings_Click(TAB_GIGABIT1)
    End Sub

    Private Sub cmdSendSettingsPCISer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSendSettingsPCISer.Click
        cmdSendSettings_Click(TAB_PCISER)
    End Sub

    Private Sub cmdSendSettingsGigabit2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSendSettingsGigabit2.Click
        cmdSendSettings_Click(TAB_GIGABIT2)
    End Sub

   Private Sub cmdSendSettingsCAN_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSendSettingsCAN.Click
        cmdSendSettings_Click(TAB_CAN)
    End Sub

    Private Sub cmdSendSettings_Click(ByVal Index As Short)
        'Block Controls in Debug mode
        If Panel_Conifg.DebugMode = True Then Exit Sub

        Select Case Index
            Case TAB_COM_1
                ResourceName = "COM_1"
                Dim serialConfigData As SerialConfig = BuildCOM1ConfigData()
                RemoveSerial(serialConfigData)
                SendSerialSettings(serialConfigData, txtDataReceivedCom1)
            Case TAB_COM_2
                ResourceName = "COM_2"
                Dim configData As SerialConfig = BuildCOM2ConfigData()
                RemoveSerial(configData)
                SendSerialSettings(configData, txtDataReceivedCom2)
            Case TAB_GIGABIT1
                Dim networkConfigData As NetworkConfig = BuildGigabit1ConfigData()
                SendNetworkSettings(networkConfigData, txtDataReceivedGigabit1)
            Case TAB_PCISER
                If optRS232.Checked Then
                    ResourceName = "COM_5"
                ElseIf optRS422.Checked Then
                    ResourceName = "COM_4"
                Else
                    ResourceName = "COM_3"
                End If
                Dim configData As SerialConfig = BuildPCISerConfigData()

                RemoveSerial(configData)
                SendSerialSettings(configData, txtDataReceivedPCISer)
            Case TAB_GIGABIT2
                Dim networkConfigData As NetworkConfig = BuildGigabit2ConfigData()
                SendNetworkSettings(networkConfigData, txtDataReceivedGigabit2)
            Case TAB_CAN
                SendCanSettings()
        End Select
    End Sub

    Private Sub frmBus_IO_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        txtReadTimeCom1.Text = "2"
        txtReadTimeCom2.Text = "2"

        txtRemoteIPGigabit1.Text = "   .   .   .   "
        txtMaxTimeGigabit2.Text = 2
        cmdLoadGigabit2.Visible = False

        With cmbTimingValueCAN
            .Items.Add("20K bits") ' 0
            .Items.Add("50K bits") ' 1
            .Items.Add("100K bits") ' 2
            .Items.Add("125K bits") ' 3
            .Items.Add("250K bits") ' 4
            .Items.Add("500K bits") ' 5
            .Items.Add("1M bits") ' 6
        End With
        cmbTimingValueCAN.SelectedIndex = 0

        Atlas_SFP.Parent_Object = Me
        Panel_Conifg.Parent_Object = Me

        optBasicIDCAN.Checked = True

        Main()
    End Sub

    Private Sub frmBus_IO_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        With Me
            If .Height >= 519 Then

                tabOptions.Left = 24
                tabOptions.Top = 8
                tabOptions.Width = .Width - 52 - 24
                tabOptions.Height = .Height - 104 - 8

                cmdHelp.Left = .Width - 322
                cmdReset.Left = .Width - 225
                cmdQuit.Left = .Width - 128

                cmdHelp.Top = .Height - 89
                cmdReset.Top = .Height - 89
                cmdQuit.Top = .Height - 89

                'TAB_COM_1
                txtDataSentCom1.Left = 16
                txtDataSentCom1.Top = 227
                txtDataSentCom1.Width = (tabOptions.Width - 16 - 8 - 34) / 2
                txtDataSentCom1.Height = tabOptions.Height - 227 - 79
                txtDataReceivedCom1.Left = 16 + txtDataSentCom1.Width + 8
                txtDataReceivedCom1.Top = 227
                txtDataReceivedCom1.Width = (tabOptions.Width - 16 - 8 - 34) / 2
                txtDataReceivedCom1.Height = tabOptions.Height - 227 - 79
                cmdSendDataCom1.Top = txtDataSentCom1.Top + txtDataSentCom1.Height + 5
                cmdSendDataCom1.Left = txtDataSentCom1.Left + txtDataSentCom1.Width - cmdSendDataCom1.Width
                cmdDataReceiveCom1.Top = txtDataSentCom1.Top + txtDataSentCom1.Height + 5
                cmdDataReceiveCom1.Left = txtDataReceivedCom1.Left + txtDataReceivedCom1.Width - cmdDataReceiveCom1.Width
                frameSendCom1.Left = 16
                frameSendCom1.Top = 191
                frameSendCom1.Width = (tabOptions.Width - 16 - 8 - 34) / 2
                frameSendCom1.Height = 33
                frameReceiveCom1.Left = 16 + txtDataSentCom1.Width + 8
                frameReceiveCom1.Width = (tabOptions.Width - 16 - 8 - 34) / 2
                frameReceiveCom1.Height = 33
                frameReceiveCom1.Top = 191
                lblReadTimeCom1.Top = txtDataSentCom1.Top + txtDataSentCom1.Height + 5
                lblReadTimeCom1.Left = txtDataReceivedCom1.Left + 6
                txtReadTimeCom1.Top = txtDataSentCom1.Top + txtDataSentCom1.Height + 5
                txtReadTimeCom1.Left = lblReadTimeCom1.Left + 121
                cmdLoadCom1.Left = tabOptions.Width - 165
                cmdSendSettingsCom1.Left = tabOptions.Width - 165

                'TAB_COM_2
                txtDataSentCom2.Left = 16
                txtDataSentCom2.Top = 227
                txtDataSentCom2.Width = (tabOptions.Width - 16 - 8 - 34) / 2
                txtDataSentCom2.Height = tabOptions.Height - 227 - 79
                txtDataReceivedCom2.Left = 16 + txtDataSentCom1.Width + 8
                txtDataReceivedCom2.Top = 227
                txtDataReceivedCom2.Width = (tabOptions.Width - 16 - 8 - 34) / 2
                txtDataReceivedCom2.Height = tabOptions.Height - 227 - 79
                cmdSendDataCom2.Top = txtDataSentCom2.Top + txtDataSentCom2.Height + 5
                cmdSendDataCom2.Left = txtDataSentCom2.Left + txtDataSentCom2.Width - cmdSendDataCom2.Width
                cmdDataReceiveCom2.Top = txtDataSentCom2.Top + txtDataSentCom2.Height + 5
                cmdDataReceiveCom2.Left = txtDataReceivedCom2.Left + txtDataReceivedCom2.Width - cmdDataReceiveCom2.Width
                frameSendCom2.Left = 16
                frameSendCom2.Top = 191
                frameSendCom2.Width = (tabOptions.Width - 16 - 8 - 34) / 2
                frameSendCom2.Height = 33
                frameReceiveCom2.Left = 16 + txtDataSentCom1.Width + 8
                frameReceiveCom2.Width = (tabOptions.Width - 16 - 8 - 34) / 2
                frameReceiveCom2.Height = 33
                frameReceiveCom2.Top = 191
                lblReadTimeCom2.Top = txtDataSentCom2.Top + txtDataSentCom2.Height + 5
                lblReadTimeCom2.Left = txtDataReceivedCom2.Left + 6
                txtReadTimeCom2.Top = txtDataSentCom2.Top + txtDataSentCom2.Height + 5
                txtReadTimeCom2.Left = lblReadTimeCom2.Left + 121
                cmdLoadCom2.Left = tabOptions.Width - 165
                cmdSendSettingsCom2.Left = tabOptions.Width - 165

                'TAB_PCISER
                txtDataSentPCISer.Left = 16
                txtDataSentPCISer.Top = 227
                txtDataSentPCISer.Width = (tabOptions.Width - 16 - 8 - 34) / 2
                txtDataSentPCISer.Height = tabOptions.Height - 227 - 79
                txtDataReceivedPCISer.Left = 16 + txtDataSentCom1.Width + 8
                txtDataReceivedPCISer.Top = 227
                txtDataReceivedPCISer.Width = (tabOptions.Width - 16 - 8 - 34) / 2
                txtDataReceivedPCISer.Height = tabOptions.Height - 227 - 79
                cmdSendDataPCISer.Top = txtDataSentPCISer.Top + txtDataSentPCISer.Height + 5
                cmdSendDataPCISer.Left = txtDataSentPCISer.Left + txtDataSentPCISer.Width - cmdSendDataPCISer.Width
                cmdDataReceivePCISer.Top = txtDataSentPCISer.Top + txtDataSentPCISer.Height + 5
                cmdDataReceivePCISer.Left = txtDataReceivedPCISer.Left + txtDataReceivedPCISer.Width - cmdDataReceivePCISer.Width
                frameSendPCISer.Left = 16
                frameSendPCISer.Top = 191
                frameSendPCISer.Width = (tabOptions.Width - 16 - 8 - 34) / 2
                frameSendPCISer.Height = 33
                frameReceivePCISer.Left = 16 + txtDataSentCom1.Width + 8
                frameReceivePCISer.Width = (tabOptions.Width - 16 - 8 - 34) / 2
                frameReceivePCISer.Height = 33
                frameReceivePCISer.Top = 191
                lblReadTimePCISer.Top = txtDataSentPCISer.Top + txtDataSentPCISer.Height + 5
                lblReadTimePCISer.Left = txtDataReceivedPCISer.Left + 6
                txtReadTimePCISer.Top = txtDataSentPCISer.Top + txtDataSentPCISer.Height + 5
                txtReadTimePCISer.Left = lblReadTimePCISer.Left + 121
                cmdLoadPCISer.Left = tabOptions.Width - 165
                cmdSendSettingsPCISer.Left = tabOptions.Width - 165

                'TAB_GIGABIT1
                txtDataSentGigabit1.Left = 16
                txtDataSentGigabit1.Top = 227
                txtDataSentGigabit1.Width = (tabOptions.Width - 16 - 8 - 34) / 2
                txtDataSentGigabit1.Height = tabOptions.Height - 227 - 79
                txtDataReceivedGigabit1.Left = 16 + txtDataSentCom1.Width + 8
                txtDataReceivedGigabit1.Top = 227
                txtDataReceivedGigabit1.Width = (tabOptions.Width - 16 - 8 - 34) / 2
                txtDataReceivedGigabit1.Height = tabOptions.Height - 227 - 79
                cmdSendDataGigabit1.Top = txtDataSentGigabit1.Top + txtDataSentGigabit1.Height + 5
                cmdSendDataGigabit1.Left = txtDataSentGigabit1.Left + txtDataSentGigabit1.Width - cmdSendDataGigabit1.Width
                cmdDataReceiveGigabit1.Top = txtDataSentGigabit1.Top + txtDataSentGigabit1.Height + 5
                cmdDataReceiveGigabit1.Left = txtDataReceivedGigabit1.Left + txtDataReceivedGigabit1.Width - cmdDataReceiveGigabit1.Width
                frameSendGigabit1.Left = 16
                frameSendGigabit1.Top = 191
                frameSendGigabit1.Width = (tabOptions.Width - 16 - 8 - 34) / 2
                frameSendGigabit1.Height = 33
                frameReceiveGigabit1.Left = 16 + txtDataSentCom1.Width + 8
                frameReceiveGigabit1.Width = (tabOptions.Width - 16 - 8 - 34) / 2
                frameReceiveGigabit1.Height = 33
                frameReceiveGigabit1.Top = 191
                cmdLoadGigabit1.Left = tabOptions.Width - 165
                cmdSendSettingsGigabit1.Left = tabOptions.Width - 165

                'TAB_GIGABIT2
                txtDataSentGigabit2.Left = 16
                txtDataSentGigabit2.Top = 227
                txtDataSentGigabit2.Width = (tabOptions.Width - 16 - 8 - 34) / 2
                txtDataSentGigabit2.Height = tabOptions.Height - 227 - 79
                txtDataReceivedGigabit2.Left = 16 + txtDataSentCom1.Width + 8
                txtDataReceivedGigabit2.Top = 227
                txtDataReceivedGigabit2.Width = (tabOptions.Width - 16 - 8 - 34) / 2
                txtDataReceivedGigabit2.Height = tabOptions.Height - 227 - 79
                cmdSendDataGigabit2.Top = txtDataSentGigabit2.Top + txtDataSentGigabit2.Height + 5
                cmdSendDataGigabit2.Left = txtDataSentGigabit2.Left + txtDataSentGigabit2.Width - cmdSendDataGigabit2.Width
                cmdDataReceiveGigabit2.Top = txtDataSentGigabit2.Top + txtDataSentGigabit2.Height + 5
                cmdDataReceiveGigabit2.Left = txtDataReceivedGigabit2.Left + txtDataReceivedGigabit2.Width - cmdDataReceiveGigabit2.Width
                frameSendGigabit2.Left = 16
                frameSendGigabit2.Top = 191
                frameSendGigabit2.Width = (tabOptions.Width - 16 - 8 - 34) / 2
                frameSendGigabit2.Height = 33
                frameReceiveGigabit2.Left = 16 + txtDataSentCom1.Width + 8
                frameReceiveGigabit2.Width = (tabOptions.Width - 16 - 8 - 34) / 2
                frameReceiveGigabit2.Height = 33
                frameReceiveGigabit2.Top = 191
                cmdLoadGigabit2.Left = tabOptions.Width - 165
                cmdSendSettingsGigabit2.Left = tabOptions.Width - 165

                'TAB_CAN
                txtDataSendCAN.Left = 16
                txtDataSendCAN.Top = 227
                txtDataSendCAN.Width = (tabOptions.Width - 16 - 8 - 34) / 2
                txtDataSendCAN.Height = tabOptions.Height - 227 - 79
                txtDataReceivedCAN.Left = 16 + txtDataSentCom1.Width + 8
                txtDataReceivedCAN.Top = 227
                txtDataReceivedCAN.Width = (tabOptions.Width - 16 - 8 - 34) / 2
                txtDataReceivedCAN.Height = tabOptions.Height - 227 - 79
                cmdSendDataCAN.Top = txtDataSendCAN.Top + txtDataSendCAN.Height + 5
                cmdSendDataCAN.Left = txtDataSendCAN.Left + txtDataSendCAN.Width - cmdSendDataCAN.Width
                cmdDataReceiveCAN.Top = txtDataSendCAN.Top + txtDataSendCAN.Height + 5
                cmdDataReceiveCAN.Left = txtDataReceivedCAN.Left + txtDataReceivedCAN.Width - cmdDataReceiveCAN.Width
                frameSendCAN.Left = 16
                frameSendCAN.Top = 191
                frameSendCAN.Width = (tabOptions.Width - 16 - 8 - 34) / 2
                frameSendCAN.Height = 33
                frameReceiveCAN.Left = 16 + txtDataSendCAN.Width + 8
                frameReceiveCAN.Width = (tabOptions.Width - 16 - 8 - 34) / 2
                frameReceiveCAN.Height = 33
                frameReceiveCAN.Top = 191
                cmdLoadCAN.Left = tabOptions.Width - 165
                cmdSendSettingsCAN.Left = tabOptions.Width - 165
            Else
                If .WindowState = FormWindowState.Minimized Then
                Else
                    .Height = 518
                End If
            End If
        End With
    End Sub

    Private Sub OptCanChannel1CAN_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles OptChannel1CAN.Click
        Dim temp As String
        If tabOptions.SelectedIndex = TAB_CAN Then

            temp = ""
            If IIf(chkThreeSamplesCAN.Checked, 1, 0) = CheckState.Checked Then
                temp = "1"
            Else
                temp = "0"
            End If
            If IIf(chkSingleFilterCAN.Checked, 1, 0) = CheckState.Checked Then
                temp &= "1"
            Else
                temp &= "0"
            End If
            If OptChannel1CAN.Checked = True Then
                temp &= "1"
            End If
            If OptChannel2CAN.Checked = True Then
                temp &= "2"
            End If
            txtSaveTCPCAN.Text = temp
        End If
    End Sub

    Private Sub OptCanChannel2CAN_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles OptChannel2CAN.Click
        Dim temp As String
        If tabOptions.SelectedIndex = TAB_CAN Then

            temp = ""
            If IIf(chkThreeSamplesCAN.Checked, 1, 0) = CheckState.Checked Then
                temp = "1"
            Else
                temp = "0"
            End If
            If IIf(chkSingleFilterCAN.Checked, 1, 0) = CheckState.Checked Then
                temp &= "1"
            Else
                temp &= "0"
            End If
            If OptChannel1CAN.Checked = True Then
                temp &= "1"
            End If
            If OptChannel2CAN.Checked = True Then
                temp &= "2"
            End If
            txtSaveTCPCAN.Text = temp
        End If
    End Sub

    Private Sub optDataSendCom1_Click(ByVal sender As Object, ByVale As System.EventArgs) Handles optDataSendCom1.Click
        txtDataSentCom1.Text = ""
    End Sub

    Private Sub optDataSendCom2_Click(ByVal sender As Object, ByVale As System.EventArgs) Handles optDataSendCom2.Click
        txtDataSentCom2.Text = ""
    End Sub

    Private Sub optDataSendGigabit1_Click(ByVal sender As Object, ByVale As System.EventArgs) Handles optDataSendGigabit1.Click
        txtDataSentGigabit1.Text = ""
    End Sub

    Private Sub optDataSendPCISer_Click(ByVal sender As Object, ByVale As System.EventArgs) Handles optDataSendPCISer.Click
        txtDataSentPCISer.Text = ""
    End Sub

    Private Sub optDataSendGigabit2_Click(ByVal sender As Object, ByVale As System.EventArgs) Handles optDataSendGigabit2.Click
        txtDataSentGigabit2.Text = ""
    End Sub

    Private Sub optRS232_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles optRS232.Click
        txtSaveSerialPCISer.Text = "RS232"
    End Sub

    Private Sub optRS422_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles optRS422.Click
        txtSaveSerialPCISer.Text = "RS422"
    End Sub

    Private Sub optRS485_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles optRS485.Click
        txtSaveSerialPCISer.Text = "RS485"
    End Sub

    Private Sub optTCPGigabit1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles optTCPGigabit1.Click

        txtSaveTCPGigabit1.Text = "TCP"
    End Sub

    Private Sub optUDPGigabit1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles optUDPGigabit1.Click
        txtSaveTCPGigabit1.Text = "UDP"
    End Sub

    Private Sub optTCPGigabit2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles optTCPGigabit2.Click

        txtSaveTCPGigabit2.Text = "TCP"
    End Sub

    Private Sub optUDPGigabit2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles optUDPGigabit2.Click
        txtSaveTCPGigabit2.Text = "UDP"
    End Sub

    Private Sub optDataReceiveCom1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles optDataReceiveCom1.Click
        If receiveddata <> "" Then
            If optXMLReceiveCom1.Checked = True Then
                receivedxml = txtDataReceivedCom1.Text
            End If
            txtDataReceivedCom1.Text = receiveddata
        End If
    End Sub

    Private Sub optDataReceiveCom2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles optDataReceiveCom2.Click
        If receiveddata <> "" Then
            If optXMLReceiveCom2.Checked = True Then
                receivedxml = txtDataReceivedCom2.Text
            End If
            txtDataReceivedCom2.Text = receiveddata
        End If
    End Sub

    Private Sub optDataReceiveGigabit1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles optDataReceiveGigabit1.Click
        If receiveddata <> "" Then
            If optXMLReceiveGigabit1.Checked = True Then
                receivedxml = txtDataReceivedGigabit1.Text
            End If
            txtDataReceivedGigabit1.Text = receiveddata
        End If
    End Sub

    Private Sub optDataReceivePCISer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles optDataReceivePCISer.Click
        If receiveddata <> "" Then
            If optXMLReceivePCISer.Checked = True Then
                receivedxml = txtDataReceivedPCISer.Text
            End If
            txtDataReceivedPCISer.Text = receiveddata
        End If
    End Sub

    Private Sub optDataReceiveGigabit2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles optDataReceiveGigabit2.Click
        If receiveddata <> "" Then
            If optXMLReceiveGigabit2.Checked = True Then
                receivedxml = txtDataReceivedGigabit2.Text
            End If
            txtDataReceivedGigabit2.Text = receiveddata
        End If
    End Sub

    Private Sub optDataReceiveCAN_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles optDataReceiveCAN.Click
        If receiveddata <> "" Then
            If optXMLReceiveCAN.Checked = True Then
                receivedxml = txtDataReceivedCAN.Text
            End If
            txtDataReceivedCAN.Text = receiveddata
        End If
    End Sub

    Private Sub optDataReceive_Click(ByVal Index As Integer)
        If receiveddata <> "" Then
            Select Case Index
            Case TAB_COM_1
                If optXMLReceiveCom1.Checked = True Then
                    receivedxml = txtDataReceivedCom1.Text
                End If
                txtDataReceivedCom1.Text = receiveddata
            Case TAB_COM_2
                If optXMLReceiveCom2.Checked = True Then
                    receivedxml = txtDataReceivedCom2.Text
                End If
                txtDataReceivedCom2.Text = receiveddata
            Case TAB_GIGABIT1
                If optXMLReceiveGigabit1.Checked = True Then
                    receivedxml = txtDataReceivedGigabit1.Text
                End If
                txtDataReceivedGigabit1.Text = receiveddata
            Case TAB_PCISER
                If optXMLReceivePCISer.Checked = True Then
                    receivedxml = txtDataReceivedPCISer.Text
                End If
                txtDataReceivedPCISer.Text = receiveddata
            Case TAB_GIGABIT2
                If optXMLReceiveGigabit2.Checked = True Then
                    receivedxml = txtDataReceivedGigabit2.Text
                End If
                txtDataReceivedGigabit2.Text = receiveddata
            Case TAB_CAN
                If optXMLReceiveCAN.Checked = True Then
                    receivedxml = txtDataReceivedCAN.Text
                End If
                txtDataReceivedCAN.Text = receiveddata
            End Select
        End If
    End Sub

    Private Sub optXMLReceiveCom1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles optXMLReceiveCom1.Click
        If receiveddata <> "" Then
            receiveddata = txtDataReceivedCom1.Text
            txtDataReceivedCom1.Text = receivedxml
        End If
    End Sub

    Private Sub optXMLReceiveCom2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles optXMLReceiveCom2.Click
        If receiveddata <> "" Then
            receiveddata = txtDataReceivedCom2.Text
            txtDataReceivedCom2.Text = receivedxml
        End If
    End Sub

    Private Sub optXMLReceiveGigabit1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles optXMLReceiveGigabit1.Click
        If receiveddata <> "" Then
            receiveddata = txtDataReceivedGigabit1.Text
            txtDataReceivedGigabit1.Text = receivedxml
        End If
    End Sub

    Private Sub optXMLReceivePCISer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles optXMLReceivePCISer.Click
        If receiveddata <> "" Then
            receiveddata = txtDataReceivedPCISer.Text
            txtDataReceivedPCISer.Text = receivedxml
        End If
    End Sub

    Private Sub optXMLReceiveGigabit2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles optXMLReceiveGigabit2.Click
        If receiveddata <> "" Then
            receiveddata = txtDataReceivedGigabit2.Text
            txtDataReceivedGigabit2.Text = receivedxml
        End If
    End Sub

    Private Sub optXMLReceiveCAN_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles optXMLReceiveCAN.Click
        If receiveddata <> "" Then
            receiveddata = txtDataReceivedCAN.Text
            txtDataReceivedCAN.Text = receivedxml
        End If
    End Sub

   Private tabOptions_PreviousTab As Integer
    Private Sub tabOptions_Deselecting(ByVal sender As System.Object, ByVal e As TabControlCancelEventArgs) Handles tabOptions.Deselecting
        tabOptions_PreviousTab = e.TabPageIndex
    End Sub
    Private Sub tabOptions_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tabOptions.SelectedIndexChanged
        Dim PreviousTab As Integer = tabOptions_PreviousTab

        Dim Idx As Short
        If optUDPGigabit1.Checked = True Then
            udp = True
        Else
            udp = False
        End If
        For Idx = 0 To 5
            If tabOptions.SelectedIndex = Idx Then
                Select Case Idx
                    Case TAB_COM_1
                        ' getCom_1_settings
                        frameSendCom1.Visible = True
                        txtDataSentCom1.Visible = True
                        txtDataReceivedCom1.Visible = True
                        cmdSendDataCom1.Visible = True
                        cmdDataReceiveCom1.Visible = True
                        frameReceiveCom1.Visible = True
                        lblReadTimeCom1.Visible = True
                        txtReadTimeCom1.Visible = True
                        cmdLoadCom1.Visible = True
                        cmdSendSettingsCom1.Visible = True
                    Case TAB_COM_2
                        ' getCom_2_settings
                        frameSendCom2.Visible = True
                        txtDataSentCom2.Visible = True
                        txtDataReceivedCom2.Visible = True
                        cmdSendDataCom2.Visible = True
                        cmdDataReceiveCom2.Visible = True
                        frameReceiveCom2.Visible = True
                        lblReadTimeCom2.Visible = True
                        txtReadTimeCom2.Visible = True
                        cmdLoadCom2.Visible = True
                        cmdSendSettingsCom2.Visible = True
                    Case TAB_PCISER
                        'mh getSerial_settings
                        frameSendPCISer.Visible = True
                        txtDataSentPCISer.Visible = True
                        txtDataReceivedPCISer.Visible = True
                        cmdSendDataPCISer.Visible = True
                        cmdDataReceivePCISer.Visible = True
                        frameReceivePCISer.Visible = True
                        lblReadTimePCISer.Visible = True
                        txtReadTimePCISer.Visible = True
                        cmdLoadPCISer.Visible = True
                        cmdSendSettingsPCISer.Visible = True
                    Case TAB_GIGABIT1
                        'getEthernet_settings
                        frameSendGigabit1.Visible = True
                        txtDataSentGigabit1.Visible = True
                        txtDataReceivedGigabit1.Visible = True
                        cmdSendDataGigabit1.Visible = True
                        cmdDataReceiveGigabit1.Visible = True
                        frameReceiveGigabit1.Visible = True
                        cmdLoadGigabit1.Visible = True
                        cmdSendSettingsGigabit1.Visible = True
                        If txtSaveTCPGigabit1.Text = "TCP" Then
                            optTCPGigabit1.Checked = True
                        Else
                            optUDPGigabit1.Checked = True
                        End If

                        'Case TAB_GIGABIT2
                    Case TAB_GIGABIT2
                        frameSendGigabit2.Visible = True
                        txtDataSentGigabit2.Visible = True
                        txtDataReceivedGigabit2.Visible = True
                        cmdSendDataGigabit2.Visible = True
                        cmdDataReceiveGigabit2.Visible = True
                        frameReceiveGigabit2.Visible = True
                        cmdLoadGigabit2.Visible = True
                        cmdSendSettingsGigabit2.Visible = True
                        If txtSaveTCPGigabit2.Text = "TCP" Then
                            optTCPGigabit2.Checked = True
                        Else
                            optUDPGigabit2.Checked = True
                        End If

                End Select
            Else
                Select Case Idx
                    Case TAB_COM_1
                        frameSendCom1.Visible = False
                        txtDataSentCom1.Visible = False
                        txtDataReceivedCom1.Visible = False
                        cmdSendDataCom1.Visible = False
                        cmdDataReceiveCom1.Visible = False
                        cmdLoadCom1.Visible = False
                        cmdSendSettingsCom1.Visible = False
                        frameReceiveCom1.Visible = False
                        lblReadTimeCom1.Visible = False
                        txtReadTimeCom1.Visible = False
                    Case TAB_COM_2
                        frameSendCom2.Visible = False
                        txtDataSentCom2.Visible = False
                        txtDataReceivedCom2.Visible = False
                        cmdSendDataCom2.Visible = False
                        cmdDataReceiveCom2.Visible = False
                        cmdLoadCom2.Visible = False
                        cmdSendSettingsCom2.Visible = False
                        frameReceiveCom2.Visible = False
                        lblReadTimeCom2.Visible = False
                        txtReadTimeCom2.Visible = False
                    Case TAB_PCISER
                        frameSendPCISer.Visible = False
                        txtDataSentPCISer.Visible = False
                        txtDataReceivedPCISer.Visible = False
                        cmdSendDataPCISer.Visible = False
                        cmdDataReceivePCISer.Visible = False
                        frameReceivePCISer.Visible = False
                        cmdLoadPCISer.Visible = False
                        cmdSendSettingsPCISer.Visible = False
                        lblReadTimePCISer.Visible = False
                        txtReadTimePCISer.Visible = False
                    Case TAB_GIGABIT1
                        frameSendGigabit1.Visible = False
                        txtDataSentGigabit1.Visible = False
                        txtDataReceivedGigabit1.Visible = False
                        cmdSendDataGigabit1.Visible = False
                        cmdDataReceiveGigabit1.Visible = False
                        frameReceiveGigabit1.Visible = False
                        cmdLoadGigabit1.Visible = False
                        cmdSendSettingsGigabit1.Visible = False
                    Case TAB_GIGABIT2
                        frameSendGigabit2.Visible = False
                        txtDataSentGigabit2.Visible = False
                        txtDataReceivedGigabit2.Visible = False
                        cmdSendDataGigabit2.Visible = False
                        cmdDataReceiveGigabit2.Visible = False
                        frameReceiveGigabit2.Visible = False
                        cmdLoadGigabit2.Visible = False
                        cmdSendSettingsGigabit2.Visible = False

                End Select
            End If
        Next Idx
    End Sub

    Private Sub Text4_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSaveTCPCAN.TextChanged
        If Not sender.Created() Then Exit Sub
        If Mid(txtSaveTCPCAN.Text, 1, 1) = "1" Then
            chkThreeSamplesCAN.Checked = CheckState.Checked
        Else
            chkThreeSamplesCAN.Checked = CheckState.Unchecked
        End If
        If Mid(txtSaveTCPCAN.Text, 2, 1) = "1" Then
            chkSingleFilterCAN.Checked = CheckState.Checked
        Else
            chkSingleFilterCAN.Checked = CheckState.Unchecked
        End If

        If Mid(txtSaveTCPCAN.Text, 3, 1) = "1" Then
            OptChannel1CAN.Checked = True
        End If
        If Mid(txtSaveTCPCAN.Text, 3, 1) = "2" Then
            OptChannel2CAN.Checked = True
        End If
    End Sub

    Private Sub txtCom1WordLength_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtWordLengthCom1.Leave
        txtCom1WordLength_KeyPress(Keys.Return)
    End Sub
    Private Sub txtCom1WordLength_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtWordLengthCom1.KeyPress
        Dim Value As Short = Asc(e.KeyChar)

        txtCom1WordLength_KeyPress(Value)

        e.KeyChar = Chr(Value)
    End Sub
    Public Sub txtCom1WordLength_KeyPress(ByRef Value As Short)
        If Value = Keys.Return Then
            If Val(txtWordLengthCom1.Text) < 5 Or Val(txtWordLengthCom1.Text) > 8 Then
                MsgBox("Value out of Range, must be between 5 to 8 Bits ")
                txtWordLengthCom1.Text = 8
            End If
        End If
    End Sub

    Private Sub txtCom2WordLength_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtWordLengthCom2.Leave
        txtCom1WordLength_KeyPress(Keys.Return)
    End Sub
    Private Sub txtCom2WordLength_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtWordLengthCom2.KeyPress
        Dim Value As Short = Asc(e.KeyChar)

        If Value = Keys.Return Then
            If Val(txtWordLengthCom2.Text) < 5 Or Val(txtWordLengthCom2.Text) > 8 Then
                MsgBox("Value out of Range, must be between 5 to 8 Bits ")
                txtWordLengthCom2.Text = 8
            End If
        End If

        e.KeyChar = Chr(Value)
    End Sub

    Private Sub txtMaxTimeGigabit1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMaxTimeGigabit1.KeyPress
        Dim Value As Short = Asc(e.KeyChar)

        If Value = Keys.Return Then
            If Val(txtMaxTimeGigabit1.Text) < 0 Or Val(txtMaxTimeGigabit1.Text) > 20 Then
                MsgBox("Value out of Range, must be between 0 - 20 Sec ")
                txtMaxTimeGigabit1.Text = 10
            End If
        End If

        e.KeyChar = Chr(Value)
    End Sub

    Private Sub txtMaxTimeGigabit2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMaxTimeGigabit2.KeyPress
        Dim Value As Short = Asc(e.KeyChar)

        If Value = Keys.Return Then
            If Val(txtMaxTimeGigabit2.Text) < 0 Or Val(txtMaxTimeGigabit2.Text) > 20 Then
                MsgBox("Value out of Range, must be between 0 - 20 Sec ")
                txtMaxTimeGigabit2.Text = 10
            End If
        End If

        e.KeyChar = Chr(Value)
    End Sub

    Private Sub txtRemoteIPGigabit1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRemoteIPGigabit1.TextChanged
        Dim ip(3) As String

        If gigabit1PortNotChanged Then
            gigabit1PortNotChanged = True : Exit Sub
        End If

        addresschange(txtRemoteIPGigabit1)
        txtRemoteIPGigabit1.Text = validateIPAddress(txtRemoteIPGigabit1.Text)
        txtLocalPortGigabit1.Text = txtRemotePortGigabit1.Text
    End Sub

    Private Sub txtRemoteIPGigabit2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRemoteIPGigabit2.TextChanged
        addresschange(txtRemoteIPGigabit2)
        txtRemoteIPGigabit2.Text = validateIPAddress(txtRemoteIPGigabit2.Text)
    End Sub

    Private Sub txtGatewayGigabit1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtGatewayGigabit1.TextChanged
        addresschange(txtGatewayGigabit1)
        txtGatewayGigabit1.Text = validateIPAddress(txtGatewayGigabit1.Text)
    End Sub

    Private Sub txtEtherGatewayGigabit2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtGatewayGigabit2.TextChanged
        addresschange(txtGatewayGigabit2)
        txtGatewayGigabit2.Text = validateIPAddress(txtGatewayGigabit2.Text)
    End Sub

    Private Sub txtEtherRemotePortGigabit1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRemotePortGigabit1.TextChanged
        txtLocalPortGigabit1.Text = txtRemotePortGigabit1.Text
    End Sub

    Private Sub txtEtherRemotePortGigabit2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRemotePortGigabit2.TextChanged
        txtLocalPortGigabit2.Text = txtRemotePortGigabit2.Text
    End Sub

    Private Sub txtLocalIPGigabit1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtLocalIPGigabit1.TextChanged
        addresschange(txtLocalIPGigabit1)
        txtLocalIPGigabit1.Text = validateIPAddress(txtLocalIPGigabit1.Text)
    End Sub

    Private Sub txtLocalIPGigabit2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtLocalIPGigabit2.TextChanged
        addresschange(txtLocalIPGigabit2)
        txtLocalIPGigabit2.Text = validateIPAddress(txtLocalIPGigabit2.Text)
    End Sub


    Private Sub txtSaveTCPGigabit1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSaveTCPGigabit1.TextChanged
        If Not sender.Created() Then Exit Sub
        With txtSaveTCPGigabit1
            If .Text = "TCP" Then
                optTCPGigabit1.Checked = True
                optUDPGigabit1.Checked = False
            End If
            If .Text = "UDP" Then
                optTCPGigabit1.Checked = False
                optUDPGigabit1.Checked = True
            End If
        End With
    End Sub

    Private Sub txtSaveSerial_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSaveSerialPCISer.TextChanged
        If Not sender.Created() Then Exit Sub
        With txtSaveSerialPCISer
            If .Text = "RS232" Then
                optRS232.Checked = True
                optRS422.Checked = False
                optRS485.Checked = False
            End If
            If .Text = "RS422" Then
                optRS232.Checked = False
                optRS422.Checked = True
                optRS485.Checked = False
            End If
            If .Text = "RS485" Then
                optRS232.Checked = False
                optRS422.Checked = False
                optRS485.Checked = True
            End If
        End With
    End Sub

    Private Sub txtSerialDelay_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSerialDelayPCISer.KeyPress
        Dim Value As Short = Asc(e.KeyChar)

        If Value = 13 Then
            If txtSerialDelayPCISer.Text < 0 Or txtSerialDelayPCISer.Text > 65536 Then
                MsgBox("Value out of Range, must be 0 to 65536 Sec ")
                txtSerialDelayPCISer.Text = 0
            End If
        End If

        e.KeyChar = Chr(Value)
    End Sub

    Private Sub txtSerialMaxTime_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSerialMaxTimePCISer.KeyPress
        Dim Value As Short = Asc(e.KeyChar)

        If Value = 13 Then
            If txtSerialMaxTimePCISer.Text < 0 Or txtSerialMaxTimePCISer.Text > 20 Then
                MsgBox("Value out of Range, must be 0 to 20 ")
                txtSerialMaxTimePCISer.Text = 10
            End If
        End If

        e.KeyChar = Chr(Value)
    End Sub

    Private Sub txtSerialWordLength_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSerialWordLengthPCISer.KeyPress
        Dim Value As Short = Asc(e.KeyChar)

        If Value = 13 Then
            If txtSerialWordLengthPCISer.Text < 5 Or txtSerialWordLengthPCISer.Text > 8 Then
                MsgBox("Value out of Range, must be 5 bit to 8 bits ")
                txtSerialWordLengthPCISer.Text = 8
            End If
        End If

        e.KeyChar = Chr(Value)
    End Sub

    Private Sub txtSubnetMaskGigabit1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtLocalSubnetMaskGigabit1.TextChanged
        If Not sender.Created() Then Exit Sub
        addresschange(txtLocalSubnetMaskGigabit1)
    End Sub

    Private Sub txtLocalSubnetMaskGigabit2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtLocalSubnetMaskGigabit2.TextChanged
        If Not sender.Created() Then Exit Sub
        addresschange(txtLocalSubnetMaskGigabit2)
    End Sub

    Private Sub txtAcceptanceCodeCAN_Leave(sender As Object, e As EventArgs) Handles txtAcceptanceCodeCAN.Leave
        While txtAcceptanceCodeCAN.Text.Length < 0
            txtAcceptanceCodeCAN.Text = "0" & txtAcceptanceCodeCAN.Text
        End While
    End Sub

    Private Sub txtAcceptanceMaskCAN_Leave(sender As Object, e As EventArgs) Handles txtAcceptanceMaskCAN.Leave
        While txtAcceptanceMaskCAN.Text.Length < 0
            txtAcceptanceMaskCAN.Text = "0" & txtAcceptanceMaskCAN.Text
        End While
    End Sub

    Private Function BuildCANAcceptanceString(ByVal value As String) As String
        Dim acceptanceValue As String = ""
        Dim binValue As String = ""
        Dim convertedValue As String = ""

        acceptanceValue = UCase(Trim(value))
        binValue = hextobin(acceptanceValue)
        convertedValue = binValue.Replace("1", "H")
        convertedValue = convertedValue.Replace("0", "L")

        ' Make sure there are 32 H/L characters in the string
        While convertedValue.Length < 32
            convertedValue = "L" & convertedValue
        End While

        BuildCANAcceptanceString = convertedValue
    End Function

    Private Sub cmdDataListenCAN_Click(sender As Object, e As EventArgs) Handles cmdDataListenCAN.Click
        Dim configData As CANConfig = New CANConfig()
        Dim response As String = ""

        Try
            configData = BuildCANConfigData()
            response = Space(MAX_XML_SIZE)
            If cmdDataListenCAN.Text = "Begin Listening" Then
                cmdDataListenCAN.Text = "Quit Listening"

                SendCANMessage("Reset", configData, response)
                configData.listenOnly = "1"
                SendCANMessage("Enable", configData, response)
                cmdDataReceiveCAN.Enabled = True
                txtDataReceivedCAN.Text = ""
                If OptChannel1CAN.Checked Then
                    CANChannel1Listening = True
                End If
                If OptChannel2CAN.Checked Then
                    CANChannel2Listening = True
                End If
            Else
                cmdDataListenCAN.Text = "Begin Listening"
                SendCANMessage("Reset", configData, response)
                cmdDataReceiveCAN.Enabled = False
                If OptChannel1CAN.Checked Then
                    CANChannel1Listening = False
                End If
                If OptChannel2CAN.Checked Then
                    CANChannel2Listening = False
                End If
            End If
        Catch ex As SystemException
            txtDataReceivedCAN.Text = ex.Message & vbCrLf & "response: " & response
        End Try
    End Sub

    ''' <summary>
    ''' This event handler prevents the user from entering a non-hexadecimal character.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtAcceptanceValueCAN_KeyDown(sender As Object, e As KeyEventArgs) Handles txtAcceptanceCodeCAN.KeyDown,
                                                                                           txtAcceptanceMaskCAN.KeyDown,
                                                                                           txtDataSendCAN.KeyDown,
                                                                                           txtMessageIDCAN.KeyDown
        Dim allowedCharacters = "1234567890ABCDEF"

        If e.KeyCode.ToString().IndexOfAny(allowedCharacters) <> 0 Then
            sbrUserInformation_Panel1.Text = "Only hexadecimal characters allowed (0123456789ABCDEF)"
            Me.Refresh()
            e.SuppressKeyPress = True
            Return
        End If
        sbrUserInformation_Panel1.Text = ""
    End Sub

    Private Sub optBasicIDCAN_CheckedChanged(sender As Object, e As EventArgs) Handles optBasicIDCAN.CheckedChanged
        If optBasicIDCAN.Checked Then
            If Not String.IsNullOrEmpty(txtMessageIDCAN.Text) Then
                If Convert.ToInt64(txtMessageIDCAN.Text, 16) > 2047 Then
                    txtMessageIDCAN.Text = "7FF"
                End If
            End If

            txtMessageIDCAN.MaxLength = 3
            ToolTip1.SetToolTip(txtMessageIDCAN, "11 bits represented as a hexadecimal value (00 - 7FF)")
        End If
    End Sub

    Private Sub optExtendedIDCAN_CheckedChanged(sender As Object, e As EventArgs) Handles optExtendedIDCAN.CheckedChanged
        If optExtendedIDCAN.Checked Then
            txtMessageIDCAN.MaxLength = 8
            ToolTip1.SetToolTip(txtMessageIDCAN, "29 bits represented as a hexadecimal value (00 - 1FFFFFFF)")
        End If
    End Sub

    Private Sub txtMessageIDCAN_TextChanged(sender As Object, e As EventArgs) Handles txtMessageIDCAN.TextChanged
        If optBasicIDCAN.Checked Then
            If Not String.IsNullOrEmpty(txtMessageIDCAN.Text) Then
                If Convert.ToInt64(txtMessageIDCAN.Text, 16) > 2047 Then
                    MessageBox.Show("Maximum value for a basic message ID is 7FF hex")
                    txtMessageIDCAN.Text = "7FF"
                End If
            End If
        End If

        If optExtendedIDCAN.Checked Then
            If Not String.IsNullOrEmpty(txtMessageIDCAN.Text) Then
                If Convert.ToInt64(txtMessageIDCAN.Text, 16) > 536870911 Then
                    MessageBox.Show("Maximum value for an extended message ID is 1FFFFFFF hex")
                    txtMessageIDCAN.Text = "1FFFFFFF"
                End If
            End If

        End If

    End Sub

    Private Sub OptChannel1CAN_CheckedChanged(sender As Object, e As EventArgs) Handles OptChannel1CAN.CheckedChanged
        If OptChannel1CAN.Checked Then
            If CANChannel1Listening = True Then
                cmdDataListenCAN.Text = "Quit Listening"
                cmdDataReceiveCAN.Enabled = True
            Else
                cmdDataListenCAN.Text = "Begin Listening"
                cmdDataReceiveCAN.Enabled = False
            End If
        End If
    End Sub

    Private Sub OptChannel2CAN_CheckedChanged(sender As Object, e As EventArgs) Handles OptChannel2CAN.CheckedChanged
        If OptChannel2CAN.Checked Then
            If CANChannel2Listening = True Then
                cmdDataListenCAN.Text = "Quit Listening"
                cmdDataReceiveCAN.Enabled = True
            Else
                cmdDataListenCAN.Text = "Begin Listening"
                cmdDataReceiveCAN.Enabled = False
            End If
        End If
    End Sub

    Private Sub txtRemoteIPGigabit1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRemoteIPGigabit1.KeyPress
        If e.KeyChar = "." Then
            txtRemoteIPGigabit1.SelectionStart = txtRemoteIPGigabit1.SelectionStart + 1
            txtRemoteIPGigabit1.SelectionLength = 1
            e.Handled = True
        End If
    End Sub

    Private Sub txtLocalIPGigabit1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLocalIPGigabit1.KeyPress
        If e.KeyChar = "." Then
            txtLocalIPGigabit1.SelectionStart = txtLocalIPGigabit1.SelectionStart + 1
            txtLocalIPGigabit1.SelectionLength = 1
            e.Handled = True
        End If
    End Sub

    Private Sub txtLocalSubnetMaskGigabit1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLocalSubnetMaskGigabit1.KeyPress
        If e.KeyChar = "." Then
            txtLocalSubnetMaskGigabit1.SelectionStart = txtLocalSubnetMaskGigabit1.SelectionStart + 1
            txtLocalSubnetMaskGigabit1.SelectionLength = 1
            e.Handled = True
        End If
    End Sub

    Private Sub txtGatewayGigabit1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtGatewayGigabit1.KeyPress
        If e.KeyChar = "." Then
            txtGatewayGigabit1.SelectionStart = txtGatewayGigabit1.SelectionStart + 1
            txtGatewayGigabit1.SelectionLength = 1
            e.Handled = True
        End If
    End Sub

    Private Sub txtRemoteIPGigabit2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRemoteIPGigabit2.KeyPress
        If e.KeyChar = "." Then
            txtRemoteIPGigabit2.SelectionStart = txtRemoteIPGigabit2.SelectionStart + 1
            txtRemoteIPGigabit2.SelectionLength = 1
            e.Handled = True
        End If
    End Sub

    Private Sub txtLocalIPGigabit2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLocalIPGigabit2.KeyPress
        If e.KeyChar = "." Then
            txtLocalIPGigabit2.SelectionStart = txtLocalIPGigabit2.SelectionStart + 1
            txtLocalIPGigabit2.SelectionLength = 1
            e.Handled = True
        End If
    End Sub

    Private Sub txtLocalSubnetMaskGigabit2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLocalSubnetMaskGigabit2.KeyPress
        If e.KeyChar = "." Then
            txtLocalSubnetMaskGigabit2.SelectionStart = txtLocalSubnetMaskGigabit2.SelectionStart + 1
            txtLocalSubnetMaskGigabit2.SelectionLength = 1
            e.Handled = True
        End If
    End Sub

    Private Sub txtGatewayGigabit2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtGatewayGigabit2.KeyPress
        If e.KeyChar = "." Then
            txtGatewayGigabit2.SelectionStart = txtGatewayGigabit2.SelectionStart + 1
            txtGatewayGigabit2.SelectionLength = 1
            e.Handled = True
        End If
    End Sub

    Private Sub txtLocalSubnetMaskGigabit1_Enter(sender As Object, e As EventArgs) Handles txtLocalSubnetMaskGigabit1.Enter
        If txtLocalSubnetMaskGigabit1.Text.Length = 0 Or txtLocalSubnetMaskGigabit1.Text = "255.255.255.0" Then
            txtLocalSubnetMaskGigabit1.Text = GetNetworkMask(txtLocalIPGigabit1.Text)
        End If
    End Sub

    Private Sub txtLocalSubnetMaskGigabit2_Enter(sender As Object, e As EventArgs) Handles txtLocalSubnetMaskGigabit2.Enter
        If txtLocalSubnetMaskGigabit2.Text.Length = 0 Or txtLocalSubnetMaskGigabit2.Text = "255.255.255.0" Then
            txtLocalSubnetMaskGigabit2.Text = GetNetworkMask(txtLocalIPGigabit2.Text)
        End If
    End Sub

    Private Sub txtMaxTimeCAN_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtMaxTimeCAN.KeyPress
        Dim Value As Short = Asc(e.KeyChar)

        If Value = Keys.Return Then
            If Val(txtMaxTimeCAN.Text) < 0 Or Val(txtMaxTimeCAN.Text) > 20 Then
                MsgBox("Value out of Range, must be between 0 - 20 Sec ")
                txtMaxTimeCAN.Text = 10
            End If
        End If

        e.KeyChar = Chr(Value)
    End Sub

    Private Sub txtReceiveLengthCom1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtReceiveLengthCom1.KeyPress
        Dim Value As Short = Asc(e.KeyChar)

        If Value = Keys.Return Then
            If Val(txtReceiveLengthCom1.Text) < 0 Or Val(txtReceiveLengthCom1.Text) > 1000 Then
                MsgBox("Value out of Range, must be between 0 - 1000 Bytes ")
                txtReceiveLengthCom1.Text = 1000
            End If
        End If

        e.KeyChar = Chr(Value)
    End Sub

   
    Private Sub txtReceiveLengthCom2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtReceiveLengthCom2.KeyPress
        Dim Value As Short = Asc(e.KeyChar)

        If Value = Keys.Return Then
            If Val(txtReceiveLengthCom2.Text) < 0 Or Val(txtReceiveLengthCom2.Text) > 1000 Then
                MsgBox("Value out of Range, must be between 0 - 1000 Bytes ")
                txtReceiveLengthCom2.Text = 1000
            End If
        End If

        e.KeyChar = Chr(Value)
    End Sub

    Private Sub txtReceiveLengthPCISer_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtReceiveLengthPCISer.KeyPress
        Dim Value As Short = Asc(e.KeyChar)

        If Value = Keys.Return Then
            If Val(txtReceiveLengthPCISer.Text) < 0 Or Val(txtReceiveLengthPCISer.Text) > 1000 Then
                MsgBox("Value out of Range, must be between 0 - 1000 Bytes Sec ")
                txtReceiveLengthPCISer.Text = 1000
            End If
        End If

        e.KeyChar = Chr(Value)
    End Sub
End Class