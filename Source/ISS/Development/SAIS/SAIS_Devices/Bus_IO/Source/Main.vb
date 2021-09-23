'Option Strict Off
'Option Explicit On

Imports System
Imports System.Windows.Forms
Imports System.Text
Imports Microsoft.VisualBasic
Imports Microsoft.Win32

Public Module BusMain


	'=========================================================
    '******************************************************************************************************************
    '* Virtual Instrument Portable Equipment Repair/Tester (VIPER/T) Software Module                                                               *
    '*                                                                                                                *
    '* Purpose        : SAIS: Bus Soft Front Panel                                                                    *
    '*                                                                                                                *
    '* Instrument     : COM 1, COM2, Ethernet, Serial, CDDI, MIC, CAN, TCIM                                           *
    '*                                                                                                                *
    '* Required Libraries / DLL's                                                                                     *
    '*                                                                                                                *
    '*      Library/DLL                 Purpose                                                                       *
    '*  =========================     ==============================================================================  *
    '*  TETS_Common_Controls.ocx      ..\..\Common\Build  (Control for ATLAS Generation and Setting Load and Save)    *
    '*  AtXmlApi.dll                  TETS\Bin (ATXML Interface API)                                                  *
    '*  AtXmlAMCOM.dll                TETS\Bin (COM Port Device Driver)                                               *
    '*  AtXmlSbsGigabit.dll           TETS\Bin (Ethernet Device Driver)                                               *
    '*  AtXmlSLS5102.dll              TETS\Bin (PCI Serial Port Device Driver)                                        *
    '*                                                                                                                *
    '* Revision History                                                                                               *
    '*   Rev       Date                  Reason                         AUTHOR                                              *
    '*   =======  =======  =====================================        ==================================================  *
    '*   1.1.0.0  27OCT06  Initial Release                              A. Buehner, EADS North America Defense              *
    '*   1.1.0.1  16NOV06  Following changes were made:                 M. Hendrickesn, EADS
    '*                      - PCI Serial Tab changes:
    '*                          = changed Bit Rate to a drop down box
    '*                          = Ranged checked Delay, Word Length,
    '*                              and Max-Time.
    '*                      - All Tabs:
    '*                          = changed the code to NOT perform a
    '*                              Load from Instr. when switching tabs.
    '*                      - COM1 & COM2 Tabs:
    '*                          = Added range checking on MaxTime and word-length
    '*                          = Corrected code to retrieve word-lenght from inst
    '*                              and place in word-lenth txt box.
    '*                      - Ethernet
    '*                          = Added range checking on max-time
    '*                          = Added code to retrieve the Addresses from instr.
    '*                          = Added code to retrieve and send Remote IP Port
    '*                              address.
    '*   1.4.1.0  04JUN06  CBTS update per DME                         DME
    '******************************************************************************************************************

    Public Structure SerialConfig
        Public protocol As String
        Public parity As String
        Public bitrate As String
        Public stopbits As String
        Public maxt As String
        Public term As String
        Public delay As String
        Public wordlength As String
        Public spec As String
        Public receivelength As String
        Public ReadTime As String
    End Structure

    Public Structure NetworkConfig
        Public protocol As String
        Public localip As String
        Public mask As String
        Public gateway As String
        Public remoteip As String
        Public remport As String
        Public maxt As String
        Public spec As String
    End Structure

    Public Structure CANConfig
        Public timing As String
        Public threeSamples As String ' "0" or "1"
        Public singleFilter As String ' "0" or "1"
        Public acceptanceCode As String ' HL pattern for four bytes
        Public acceptanceMask As String ' HL pattern for four bytes
        Public channel As String ' "1" or "2"
        Public maxTime As String
        Public ioFlags As String
        Public messageID As String
        Public data As String
        Public dataBits As String ' data to transmit as an HL pattern
        Public listenOnly As String
    End Structure

    '----------------- VXIplug&play Driver Declarations ---------

    Declare Function atxml_Initialize Lib "AtXmlApi.dll" (ByVal proctype As String, ByVal guid As String) As Integer

    Declare Function atxml_Close Lib "AtXmlApi.dll" () As Integer

    Declare Function atxml_ValidateRequirements Lib "AtXmlApi.dll" (ByVal TestRequirements As String, ByVal Allocation As String, ByVal Availability As String, ByVal BufferSize As Short) As Integer

    Declare Function atxml_GetSingleIntValue Lib "AtXmlApi.dll" (ByVal Response As String, ByVal Attr As String, ByRef IntValue As Integer) As Integer
    Declare Function atxml_GetIntArrayValue Lib "AtXmlApi.dll" (ByVal Response As String, ByVal Attr As String, ByRef IntArrayPtr As Short, ByVal ArraySize As Short) As Integer
    Declare Function atxml_GetSingleDblValue Lib "AtXmlApi.dll" (ByVal Response As String, ByVal Attr As String, ByRef DblValue As Double) As Integer
    Declare Function atxml_GetDblArrayValue Lib "AtXmlApi.dll" (ByVal Response As String, ByVal Attr As String, ByRef DblArrayPtr As Double, ByVal ArraySize As Short) As Integer
    Declare Function atxml_GetStringValue Lib "AtXmlApi.dll" (ByVal Response As String, ByVal Attr As String, ByVal StrPtr As String, ByVal BufferSize As Short) As Integer

    Declare Function atxml_IssueSignal Lib "AtXmlApi.dll" (ByVal SignalDescription As String, ByVal Response As String, ByVal BufferSize As Short) As Integer

    '-----------------API / DLL Declarations------------------------------
    Declare Function WritePrivateProfileString Lib "kernel32" Alias "WritePrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpString As String, ByVal lpFileName As String) As Integer
    Declare Function GetPrivateProfileString Lib "kernel32" Alias "GetPrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpDefault As String, ByVal lpReturnedString As String, ByVal nSize As Integer, ByVal lpFileName As String) As Integer


    '-----------------Init and GUID ---------------------------------------
    Public Const guid As String = "{663AA793-BE3F-4f22-AA3A-5B2365A6EEF2}"
    Public Const proctype As String = "SFP"

    ' WindowStates
    Public Const NORMAL As Short = 0 ' 0 - Normal
    Public Const MINIMIZED As Short = 1 ' 1 - Minimized
    Public Const MAXIMIZED As Short = 2 ' 2 - Maximized
    Public Const MAX_XML_SIZE As Short = 5000

    '----------------- Global Constants and Variables --------------------
    Public ResName(7) As String


    'Options Tab Index Constants
    Public Const TAB_COM_1 As Short = 0
    Public Const TAB_COM_2 As Short = 1
    Public Const TAB_PCISER As Short = 2
    Public Const TAB_GIGABIT1 As Short = 3
    Public Const TAB_GIGABIT2 As Short = 4
    Public Const TAB_CAN As Short = 5
    Public Const TAB_ATLAS As Short = 6
    Public Const TAB_OPTIONS As Short = 7

    ' CAN I/O flags
    Public Const CAN_EXTENDED_MSG_ID As Short = 1
    Public Const CAN_REMOTE_FRAME As Short = 2
    Public Const CAN_SINGLE_SHOT As Short = 4
    Public Const CAN_SELF_RECEPTION As Short = 8

    Public Const INSTRUMENT_NAME As String = "Bus I/O Controller"
    Public Const MANUF As String = "Astronics Test Systems"
    Public Const MODEL_CODE As String = "Bus I/O"
    Public Const RESOURCE_NAME As String = "VXI::38"
    Public Const SAIS_VERSION As String = "1.3"
    Public ResourceName As String

    Public LiveMode As Boolean ' Instrument Communication flag
    Public Quote As String ' Holds Quote mark - Chr$(34)
    Public DoNotTalk As Boolean ' "Don't Talk" to instrument flag
    Public ErrorStatus As Integer ' Return status for instrument calls
    Public IHnd As Integer ' Instrument Address
    Public ReadBuffer As String = Space(MAX_XML_SIZE) ' Holds return strings from the instrument
    Public InstrumentError As Short ' True if WriteInstrument error query found an error
    Public UserReset As Short ' Used during Built-In Test to detect a user abort
    Public CANChannel1Listening As Boolean 'flag indicating Channel 1 on the CAN bus is/not listening for data
    Public CANChannel2Listening As Boolean 'flag indicating Channel 2 on the CAN bus is/not listening for data

    '***Read Instrument Mode***
    Public iStatus As Short
    Public gctlParse As New VIPERT_Common_Controls.Common
    '###############################################################


    Sub Main()
        Dim NumChars As Integer
        Dim SystemDirectory As String
        Dim S As String
        Dim SwInfo As String
        Const conNoDLL As Short = 48
        'WMT (START):
        Dim status As Integer
        Dim XmlBuf As String
        Dim Allocation As String
        Dim Response As String

        XmlBuf = Space(4096)
        Response = Space(4096)
        ' initialize resource name
        ResourceName = "COM_1"

        CANChannel1Listening = False
        CANChannel2Listening = False
        'WMT (END)

        SplashStart()

        Quote = Convert.ToString(Chr(34))
        DoNotTalk = True

        'Check System and Instrument For Errors
        SystemDirectory = Environment.SystemDirectory
        Dim fi As System.IO.FileInfo = New IO.FileInfo(SystemDirectory & "\VISA32.DLL")
        If fi.Exists Then
            'Determine if the instrument is functioning
            On Error Resume Next
            '            WMT:
            '            ErrorStatus& = Sw_Init(rsrcName:=RESOURCE_NAME$, IDQuery:=1, reset:=1, Vi:=IHnd&)
            '            WMT (START):
            status = atxml_Initialize(proctype, guid)
            XmlBuf = "<AtXmlTestRequirements>" &
                     "    <ResourceRequirement>" &
                     "        <ResourceType>Source</ResourceType>" &
                     "        <SignalResourceName>ETHERNET_1</SignalResourceName> " &
                     "    </ResourceRequirement> " &
                     "    <ResourceRequirement>" &
                     "        <ResourceType>Source</ResourceType>" &
                     "        <SignalResourceName>ETHERNET_2</SignalResourceName> " &
                     "    </ResourceRequirement> " &
                     "    <ResourceRequirement>" &
                     "        <ResourceType>Source</ResourceType>" &
                     "        <SignalResourceName>COM_1</SignalResourceName> " &
                     "    </ResourceRequirement> " &
                    "    <ResourceRequirement>" &
                    "        <ResourceType>Source</ResourceType>" &
                    "        <SignalResourceName>COM_2</SignalResourceName> " &
                    "    </ResourceRequirement> " &
                    "    <ResourceRequirement>" &
                    "        <ResourceType>Source</ResourceType>" &
                    "        <SignalResourceName>COM_3</SignalResourceName> " &
                    "    </ResourceRequirement> "

            XmlBuf &= "    <ResourceRequirement>" &
                      "        <ResourceType>Source</ResourceType>" &
                      "        <SignalResourceName>COM_4</SignalResourceName> " &
                      "    </ResourceRequirement> " &
                      "    <ResourceRequirement>" &
                      "        <ResourceType>Source</ResourceType>" &
                      "        <SignalResourceName>COM_5</SignalResourceName> " &
                      "    </ResourceRequirement> " &
                      "    <ResourceRequirement>" &
                      "        <ResourceType>Source</ResourceType>" &
                      "        <SignalResourceName>CAN_1</SignalResourceName> " &
                      "    </ResourceRequirement> " &
                      "</AtXmlTestRequirements>"


            Allocation = Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\ATS\", "PAWSAllocationPath", Nothing)

            status = atxml_ValidateRequirements(XmlBuf, Allocation, Response, 4096)

            'Parse Availability XML string to get the status(Mode) of the instrument
            iStatus = gctlParse.ParseAvailiability(Response)

            'frmBus_IO.txtDataSentCom1.text = Response
            '            WMT (END)
            If Err.Number = conNoDLL Then
                MsgBox("Error in loading Communication Bus Driver.", MsgBoxStyle.Information)
                LiveMode = False
            ElseIf ErrorStatus Then
                MsgBox("The Bus is not responding.", MsgBoxStyle.Exclamation)
                LiveMode = False
            Else
                LiveMode = True

            End If
            delay(0.5)
            On Error GoTo 0
        Else
            MsgBox("VISA Message Delivery System is not installed.  Live mode is disabled.", MsgBoxStyle.Information)
            LiveMode = False
        End If

        DoNotTalk = False

        'Check for switch modules at the correct module address
        Dim find As String
        Dim find2 As String
        Dim find3 As String

        find = "resourceName=""ETHERNET_1"" availability=""Available"""
        find2 = "resourceName=""ETHERNET_1"" availability=""Simulated"""
        find3 = "resourceName=""ETHERNET_1"" availability=""AvailableInUse"""

        If LiveMode = True And DoNotTalk = False Then
            '        DoIt Sw_Reset(IHnd)
            With frmBus_IO
                If  InStr(Response, find2) Then
                    MsgBox("The Gigabit Ethernet 1 bus is being simulated", MsgBoxStyle.OkOnly, "Gigabit Ethernet 1 Bus Simulated")
                End If

                find = "resourceName=""COM_1"" availability=""Available"""
                find2 = "resourceName=""COM_1"" availability=""Simulated"""
                find3 = "resourceName=""COM_1"" availability=""AvailableInUse"""

                If InStr(Response, find2) Then
                    MsgBox("The COM 1 bus is being simulated", vbOKOnly, "COM 1 Bus Simulated")
                End If

                find = "resourceName=""COM_2"" availability=""Available"""
                find2 = "resourceName=""COM_2"" availability=""Simulated"""
                find3 = "resourceName=""COM_2"" availability=""AvailableInUse"""

                If InStr(Response, find2) Then
                    MsgBox("The COM 2 bus is being simulated", vbOKOnly, "COM 2 Bus Simulated")
                End If

                find = "resourceName=""COM_3"" availability=""Available"""
                find2 = "resourceName=""COM_3"" availability=""Simulated"""
                find3 = "resourceName=""COM_3"" availability=""AvailableInUse"""
                If InStr(Response, find2) Then
                    MsgBox("The COM 3 bus is being simulated", MsgBoxStyle.OkOnly, "COM 3 Bus Simulated")
                End If

                find = "resourceName=""COM_4"" availability=""Available"""
                find2 = "resourceName=""COM_4"" availability=""Simulated"""
                find3 = "resourceName=""COM_4"" availability=""AvailableInUse"""
                If InStr(Response, find2) Then
                    MsgBox("The COM 4 bus is being simulated", MsgBoxStyle.OkOnly, "COM 4 Bus Simulated")
                End If

                find = "resourceName=""COM_5"" availability=""Available"""
                find2 = "resourceName=""COM_5"" availability=""Simulated"""
                find3 = "resourceName=""COM_5"" availability=""AvailableInUse"""
                If InStr(Response, find2) Then
                    MsgBox("The COM 5 bus is being simulated", MsgBoxStyle.OkOnly, "COM 5 Bus Simulated")
                End If

                find = "resourceName=""ETHERNET_2"" availability=""Available"""
                find2 = "resourceName=""ETHERNET_2"" availability=""Simulated"""
                find3 = "resourceName=""ETHERNET_2"" availability=""AvailableInUse"""
                If InStr(Response, find2) Then
                    MsgBox("The Gigabit Ethernet 2 bus is being simulated", MsgBoxStyle.OkOnly, "Gigabit Ethernet 2 Bus Simulated")
                End If

                find = "resourceName=""CAN_1"" availability=""Available"""
                find2 = "resourceName=""CAN_1"" availability=""Simulated"""
                find3 = "resourceName=""CAN_1"" availability=""AvailableInUse"""
                If InStr(Response, find2) Then
                    MsgBox("The CAN bus is being simulated", MsgBoxStyle.OkOnly, "CAN Bus Simulated")
                End If
            End With
        End If

        Initialize()

        'Set the Refresh rate when in "Debug" mode from 1000 = 1 sec to 25000 = 25 sec
        frmBus_IO.Panel_Conifg.Refresh = 50

        ' how to get iStatus = 1. Need to get
        ' help from EADS.
        frmBus_IO.tabOptions.SelectedIndex = TAB_PCISER
        frmBus_IO.optRS485.Checked = True
        SplashEnd()
        frmBus_IO.tabOptions.SelectedIndex = TAB_COM_1

        'Get current information from instrument "DONE ABOVE"
        frmBus_IO.ConfigGetCurrent()
        Application.DoEvents()

        'DO NOT Put a break point After this point. Causes error if timer is running
        'Set the Mode the Instrument is in: Local = True, Debug = False
        frmBus_IO.Panel_Conifg.SetDebugStatus(True)
    End Sub

    Sub Initialize()
        'Set Resource names
        ResName(0) = "COM_1"
        ResName(1) = "COM_2"
        ResName(2) = "ETHERNET_1"
        ResName(3) = "COM_3"
        ResName(4) = "ETHERNET_2"
        ResName(5) = "CAN_1"

        frmBus_IO.optBasicIDCAN.Checked = True
        frmBus_IO.Show()
    End Sub

    Public Function DecodeErrorMessage(ByVal Msg As String) As String
        DecodeErrorMessage = ""
        'Error Control
        Dim SourceOfError As String, ErrorNum As Short, ErrorText As String

        If Not LiveMode Then Exit Function
        If (Msg.Length < 12) Or (InStr(Msg, ".") = 0) Then
            DecodeErrorMessage = "Error decoding error message"
            Exit Function
        End If

        SourceOfError = Mid(Msg, InStr(Msg, ".") - 3, 3)

        If SourceOfError <> "" Then
            SourceOfError = "Error reported from: " & SourceOfError & vbCrLf
        End If

        ErrorNum = Val(Mid(Msg, InStr(Msg, ".") + 1, 2))
        Select Case ErrorNum
            Case 0
                ErrorText = "No error"
            Case 1
                ErrorText = "Invalid module number specified"
            Case 2
                ErrorText = "Specified module not installed"
            Case 3
                ErrorText = "Invalid channel number specified"
            Case 4
                ErrorText = "Invalid port number specified"
            Case 5
                ErrorText = "Command syntax error"
            Case 6
                ErrorText = "Read value larger than expected"
            Case 7
                ErrorText = "Function not supported by module"
            Case 8
                ErrorText = "Expected line terminator not found"
            Case 9
                ErrorText = "Valid command not found"
            Case 20
                ErrorText = "Exclude list too long"
            Case 21
                ErrorText = "Channel entered on exclude list twice"
            Case 22
                ErrorText = "Module doesn't allow exclude function"
            Case 23
                ErrorText = "Scan list too long"
            Case 24
                ErrorText = "Module doesn't allow scan"
            Case 25
                ErrorText = "Equate list too long"
            Case 26
                ErrorText = "Module entered on scan list twice"
            Case 27
                ErrorText = "Incompatible modules equated or digital modules invalid"
            Case 31
                ErrorText = "SRQMASK invalid"
            Case 40
                ErrorText = "Number invalid as a test number"
            Case 41
                ErrorText = "RAM test failure"
            Case 42
                ErrorText = "ROM test failure"
            Case 43
                ErrorText = "Non-vol memory test failure"
            Case 44
                ErrorText = "Incompatible operating system EPROMS, software revisions"
            Case 45
                ErrorText = "Self-test CPU or related circuitry failure"
            Case 46
                ErrorText = "Self-test 24 V supply failure"
            Case 47
                ErrorText = "Self-test timer chip failure"
            Case 48
                ErrorText = "Insufficient RAM for option module"
            Case 49
                ErrorText = "Checksum error reading from option module EPROM"
            Case 50
                ErrorText = "Option module EPROM incompatible with CPU EPROM"
            Case 51
                ErrorText = "Failed confidence test"
            Case 55
                ErrorText = "Error STOREing to non-vol memory"
            Case 56
                ErrorText = "Error REDALLing from non-vol memory"
            Case 57
                ErrorText = "Non-vol storage location number out of range"
            Case 58
                ErrorText = "RECALLing from an empty non-vol location not allowed"
            Case 59
                ErrorText = "Non-vol memory initialization required"

            Case Else
                ErrorText = "Unknown Error Code"
        End Select
        ErrorText = "Error Number: " & ErrorNum.ToString("00") & vbCrLf & "Error Message: " & ErrorText

        DecodeErrorMessage = SourceOfError & ErrorText

    End Function

    Public Sub delay(ByVal Seconds As Single)
        'DESCRIPTION:
        '   Delays the program for a specified time
        'PARAMETERS:
        '   Seconds! = number of seconds to delay
        'EXAMPLE:
        '   Delay 2.3
        'GLOBAL VARIABLES USED: None
        'REQUIRES:  None
        'VERSION:   8/13/96
        'CUSTODIAN: J. Hill
        System.Threading.Thread.Sleep(Convert.ToInt32(Seconds * 1000))
    End Sub

    Sub DisplayErrorMessage(ByVal ErrorStatus As Integer)
        If ErrorStatus < 0 Then
            '        Error = Sw_Error_Message(IHnd&, ErrorStatus&, ReadBuffer$)
            '        MsgBox ReadBuffer$, 48, "VISA Error Message"
        End If
    End Sub

    'JRC Added 033009
    Function GatherIniFileInformation(ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpDefault As String) As String
        GatherIniFileInformation = ""
        '************************************************************
        '*     Finds a value on in the VIPERT.INI File                *
        '*    PARAMETERS:                                           *
        '*     lpApplicationName$ -[Application] in TETS.INI File   *
        '*     lpKeyName$ - KEYNAME= in TETS.INI File               *
        '*     lpDefault$ - Default value to return if not found    *
        '*    RETURNS                                               *
        '*     String containing the key value or the lpDefault     *
        '*    EXAMPLE:                                              *
        '*     FilePath$ = GatherIniFileInformation("Heading", ...  *
        '*      ..."MY_FILE", "")                                   *
        '************************************************************
        Dim lpReturnedString As String 'Return Buffer
        Dim nSize As Integer 'Return Buffer Size
        Dim lpFileName As String 'INI File Key Name "Key=?"
        Dim ReturnValue As Integer 'Return Value Buffer
        Dim FileNameInfo As String 'Formatted Return String
        Dim lpString As String 'String to write to INI File

        'Clear String Buffer
        'Get the ini file location from the Registry
        lpFileName = Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\ATS\", "IniFilePath", Nothing)
        nSize = 255
        lpReturnedString = Space(260)
        FileNameInfo = ""
        'Find File Locations
        ReturnValue = GetPrivateProfileString(lpApplicationName, lpKeyName, lpDefault, lpReturnedString, nSize, lpFileName)
        FileNameInfo = Trim(lpReturnedString)
        FileNameInfo = Mid(FileNameInfo, 1, Len(FileNameInfo) - 1)
        'If File Locations Missing, then create empty keys
        If FileNameInfo = lpDefault + Convert.ToString(Chr(0)) Or FileNameInfo = lpDefault Then
            lpString = Trim(lpDefault)
            ReturnValue = WritePrivateProfileString(lpApplicationName, lpKeyName, lpString, lpFileName)
        End If

        'Return Information In INI File
        GatherIniFileInformation = FileNameInfo
    End Function

    Public Sub HelpPanel(ByVal text As String)
        'DESCRIPTION:
        '   Displays "Text$" on a Status Bar names 'sbrUserInformation'.
        '   Normally called from the 'ShowVals' sub or an opject's 'MouseMove' event.
        '   Only updates when new different text is passed. Prevents blinking.
        'EXAMPLE:       HelpPanel "Loading file"
        'PARAMETERS:
        '   Text$:      The string to display on the Status Bar
        'GLOBAL VARIABLES USED: none
        'REQUIRES:      Nothing
        'VERSION:       2/20/97
        'CUSTODIAN:     J. Hill

        If frmAbout.Visible Then Exit Sub

        If frmBus_IO.sbrUserInformation.Text <> text Then
            frmBus_IO.sbrUserInformation.Text = text
        End If
    End Sub

    Sub ReceiveSerialData(ByVal configData As SerialConfig)
        Dim status As Integer
        Dim XmlBuf As String
        Dim Response As String

        XmlBuf = Space(4096)
        Response = Space(MAX_XML_SIZE)

        'sleep for the length specified
        System.Threading.Thread.Sleep(Convert.ToInt32(Val(configData.ReadTime) * 1000))

        'perform fetch
        XmlBuf = "<AtXmlSignalDescription>" & vbCrLf &
                    "   <SignalAction>Fetch</SignalAction>" & vbCrLf &
                    "   <SignalResourceName>" & ResourceName &
                    "</SignalResourceName>" & vbCrLf &
                    "   <SignalSnippit>" & vbCrLf &
                    "       <Signal Name=""RS_SIGNAL"" Out=""exchange"">" & vbCrLf &
                    "           <" & configData.protocol & " name=""exchange" &
                                                                """ data_bits=""" &
                                                                """ parity=""" & configData.parity &
                                                                """ baud_rate=""" & configData.bitrate &
                                                                """ stop_bits=""" & configData.stopbits &
                                                                """ maxTime=""" & configData.maxt &
                                                                """ terminate=""OFF" &
                                                                """ delay=""" & configData.delay &
                                                                """ wordLength=""" & configData.wordlength &
                                                                """ readLength=""" & configData.receivelength &
                                                                """ attribute=""data""" & " />" & vbCrLf &
                    "       </Signal>" & vbCrLf &
                    "   </SignalSnippit>" & vbCrLf &
                    "</AtXmlSignalDescription>"

        status = atxml_IssueSignal(XmlBuf, Response, MAX_XML_SIZE)

        Dim temp As String
        temp = Space(5000)
        If InStr(Response, "ErrStatus") = 0 Then
            status = atxml_GetStringValue(Response, "data", temp, 5000)
            Dim dataR As String
            Dim trans As String
            trans = BitsToUnicode(Trim(temp), 8)
            dataR = Trim(temp)

            frmBus_IO.receiveddata = "Chars:  " & HLToStr(dataR, Convert.ToInt16(configData.wordlength)) & vbCrLf & "Bits: " & dataR
        Else
            frmBus_IO.receiveddata = CheckInstrumentError(Response)
        End If
    End Sub

    Sub ReceiveGigabit1Data(ByVal senddata As Boolean)
        With frmBus_IO
            Dim status As Integer
            Dim XmlBuf As String
            Dim Response As String
            Dim i As Integer
            Dim tempr As String = ""

            Try
                XmlBuf = Space(4096)
                Response = Space(MAX_XML_SIZE)
                '' initialize resource name
                ResourceName = "ETHERNET_1"

                Dim configData As NetworkConfig = frmBus_IO.BuildGigabit1ConfigData()

                Dim datasend As String
                If senddata = True Then
                    datasend = " data_bits=""" & .txtDataSentGigabit1.Text & """"
                Else
                    datasend = ""
                End If

                XmlBuf = "<AtXmlSignalDescription> " & vbCrLf &
                         "   <SignalAction>Enable</SignalAction>" & vbCrLf &
                         "   <SignalResourceName>" & ResourceName &
                         "</SignalResourceName>" & vbCrLf &
                         "   <SignalSnippit>" & vbCrLf &
                         "       <Signal Name=""ETHERNET_SIGNAL"" Out=""exchange"">" & vbCrLf &
                         "           <" & configData.protocol & " name=""exchange"" maxTime=""" & configData.maxt &
                                                                     """ localIP=""" & configData.localip &
                                                                     """ localSubnetMask=""" & configData.mask &
                                                                     """ localGateway=""" & configData.gateway &
                                                                     """ remoteIP=""" & configData.remoteip &
                                                                     """ remotePort=""" & configData.remport &
                                                                     """" & datasend & "  />" & vbCrLf &
                         "       </Signal>" & vbCrLf &
                         "   </SignalSnippit>" & vbCrLf &
                         "</AtXmlSignalDescription>"

                status = atxml_IssueSignal(XmlBuf, Response, MAX_XML_SIZE)
                Dim ch As String
                Dim t As String = ""

                For i = 1 To Len(.txtDataReceivedGigabit1.Text)
                    ch = Mid(.txtDataReceivedGigabit1.Text, i, 1)
                    If Asc(ch) = 10 Then
                        t &= vbCrLf
                    Else
                        t &= ch
                    End If
                Next i
                Dim res(3) As String
                'perform fetch
                XmlBuf = "<AtXmlSignalDescription> " & vbCrLf &
                         "   <SignalAction>Fetch</SignalAction>" & vbCrLf &
                         "   <SignalResourceName>" & ResourceName &
                         "</SignalResourceName>" & vbCrLf &
                         "   <SignalSnippit>" & vbCrLf &
                         "       <Signal Name=""ETHERNET_SIGNAL"" Out=""exchange"">" & vbCrLf &
                         "           <" & configData.protocol & " name=""exchange"" maxTime=""" & configData.maxt &
                                                                     """ localIP=""" & configData.localip &
                                                                     """ localSubnetMask=""" & configData.mask &
                                                                     """ localGateway=""" & configData.gateway &
                                                                     """ remoteIP=""" & configData.remoteip &
                                                                     """ remotePort=""" & configData.remport & """ attribute=""data"" />" & vbCrLf &
                         "       </Signal>" & vbCrLf &
                         "   </SignalSnippit>" & vbCrLf &
                         "</AtXmlSignalDescription>"

                status = atxml_IssueSignal(XmlBuf, Response, MAX_XML_SIZE)

                Dim temp As String
                temp = Space(5000)

                If InStr(Response, "ErrStatus") = 0 Then
                    status = atxml_GetStringValue(Response, "data", temp, 5000)
                    Dim dataR As String
                    dataR = Trim(temp)

                    .receiveddata = dataR
                    If .optDataReceiveGigabit1.Checked = True Then
                        .txtDataReceivedGigabit1.Text = HLToStr(dataR)
                    End If

                Else
                    .receiveddata = CheckInstrumentError(Response)
                    If (.receiveddata <> "") Then .txtDataReceivedGigabit1.Text = .receiveddata
                End If

                t = ""
                For i = 1 To Len(.txtDataReceivedGigabit1.Text)
                    ch = Mid(.txtDataReceivedGigabit1.Text, i, 1)
                    If Asc(ch) = 10 Then
                        t &= vbCrLf
                    Else
                        t &= ch
                    End If
                Next i
                'res(1) = "Data Received from Fetch:" & vbCrLf & t & vbCrLf
                .txtDataReceivedGigabit1.Text = vbCrLf & t

                'perform a Disable
                XmlBuf = "<AtXmlSignalDescription> " & vbCrLf &
                         "   <SignalAction>Disable</SignalAction>" & vbCrLf &
                         "   <SignalResourceName>" & ResourceName &
                         "</SignalResourceName>" & vbCrLf &
                         "   <SignalSnippit>" & vbCrLf &
                         "       <Signal Name=""ETHERNET_SIGNAL"" Out=""exchange"">" & vbCrLf &
                         "           <" & configData.protocol & " name=""exchange"" maxTime=""" & configData.maxt &
                                                                     """ localIP=""" & configData.localip &
                                                                     """ localSubnetMask=""" & configData.mask &
                                                                     """ localGateway=""" & configData.gateway &
                                                                     """ remoteIP=""" & configData.remoteip &
                                                                     """ remotePort=""" & configData.remport & """ attribute=""data"" />" & vbCrLf &
                         "       </Signal>" & vbCrLf &
                         "   </SignalSnippit>" & vbCrLf &
                         "</AtXmlSignalDescription>"

                status = atxml_IssueSignal(XmlBuf, Response, MAX_XML_SIZE)

                t = ""
                For i = 1 To Len(Response)
                    ch = Mid(Response, i, 1)
                    If Asc(ch) = 10 Then
                        t &= vbCrLf
                    Else
                        t &= ch
                    End If
                Next i

            Catch ex As SystemException
                MessageBox.Show(ex.Message)
            End Try
        End With
    End Sub

    Sub ReceiveGigabit2Data(ByVal senddata As Boolean)
        With frmBus_IO
            Dim status As Integer
            Dim XmlBuf As String
            Dim Response As String
            Dim i As Integer
            Dim tempr As String = ""

            Try
                XmlBuf = Space(4096)
                Response = Space(MAX_XML_SIZE)
                '' initialize resource name
                ResourceName = "ETHERNET_2"

                Dim configData As NetworkConfig = frmBus_IO.BuildGigabit2ConfigData()

                Dim datasend As String
                If senddata = True Then
                    datasend = " data_bits=""" & .txtDataSentGigabit2.Text & """"
                Else
                    datasend = ""
                End If

                XmlBuf = "<AtXmlSignalDescription> " & vbCrLf &
                         "   <SignalAction>Enable</SignalAction>" & vbCrLf &
                         "   <SignalResourceName>" & ResourceName &
                         "</SignalResourceName>" & vbCrLf &
                         "   <SignalSnippit>" & vbCrLf &
                         "       <Signal Name=""ETHERNET_SIGNAL"" Out=""exchange"">" & vbCrLf &
                         "           <" & configData.protocol & " name=""exchange"" maxTime=""" & configData.maxt &
                                                                     """ localIP=""" & configData.localip &
                                                                     """ localSubnetMask=""" & configData.mask &
                                                                     """ localGateway=""" & configData.gateway &
                                                                     """ remoteIP=""" & configData.remoteip &
                                                                     """ remotePort=""" & configData.remport &
                                                                     """" & datasend & "  />" & vbCrLf &
                         "       </Signal>" & vbCrLf &
                         "   </SignalSnippit>" & vbCrLf &
                         "</AtXmlSignalDescription>"

                status = atxml_IssueSignal(XmlBuf, Response, MAX_XML_SIZE)

                Dim ch As String
                Dim t As String = ""

                For i = 1 To Len(.txtDataReceivedGigabit2.Text)
                    ch = Mid(.txtDataReceivedGigabit2.Text, i, 1)
                    If Asc(ch) = 10 Then
                        t &= vbCrLf
                    Else
                        t &= ch
                    End If
                Next i
                Dim res(3) As String

                'perform fetch
                XmlBuf = "<AtXmlSignalDescription> " & vbCrLf &
                         "   <SignalAction>Fetch</SignalAction>" & vbCrLf &
                         "   <SignalResourceName>" & ResourceName &
                         "</SignalResourceName>" & vbCrLf &
                         "   <SignalSnippit>" & vbCrLf &
                         "       <Signal Name=""ETHERNET_SIGNAL"" Out=""exchange"">" & vbCrLf &
                         "           <" & configData.protocol & " name=""exchange"" maxTime=""" & configData.maxt &
                                                                     """ localIP=""" & configData.localip &
                                                                     """ localSubnetMask=""" & configData.mask &
                                                                     """ localGateway=""" & configData.gateway &
                                                                     """ remoteIP=""" & configData.remoteip &
                                                                     """ remotePort=""" & configData.remport & """ attribute=""data"" />" & vbCrLf &
                         "       </Signal>" & vbCrLf &
                         "   </SignalSnippit>" & vbCrLf &
                         "</AtXmlSignalDescription>"

                status = atxml_IssueSignal(XmlBuf, Response, MAX_XML_SIZE)

                Dim temp As String
                temp = Space(5000)

                If InStr(Response, "ErrStatus") = 0 Then
                    status = atxml_GetStringValue(Response, "data", temp, 5000)
                    Dim dataR As String
                    dataR = Trim(temp)

                    .receiveddata = dataR
                    If .optDataReceiveGigabit2.Checked = True Then
                        .txtDataReceivedGigabit2.Text = HLToStr(dataR)
                    End If
                Else
                    .receiveddata = CheckInstrumentError(Response)
                    If (.receiveddata <> "") Then .txtDataReceivedGigabit2.Text = .receiveddata
                End If

                t = ""
                For i = 1 To Len(.txtDataReceivedGigabit2.Text)
                    ch = Mid(.txtDataReceivedGigabit2.Text, i, 1)
                    If Asc(ch) = 10 Then
                        t &= vbCrLf
                    Else
                        t &= ch
                    End If
                Next i

                'perform a Disable
                XmlBuf = "<AtXmlSignalDescription> " & vbCrLf &
                         "   <SignalAction>Disable</SignalAction>" & vbCrLf &
                         "   <SignalResourceName>" & ResourceName &
                         "</SignalResourceName>" & vbCrLf &
                         "   <SignalSnippit>" & vbCrLf &
                         "       <Signal Name=""ETHERNET_SIGNAL"" Out=""exchange"">" & vbCrLf &
                         "           <" & configData.protocol & " name=""exchange"" maxTime=""" & configData.maxt &
                                                                     """ localIP=""" & configData.localip &
                                                                     """ localSubnetMask=""" & configData.mask &
                                                                     """ localGateway=""" & configData.gateway &
                                                                     """ remoteIP=""" & configData.remoteip &
                                                                     """ remotePort=""" & configData.remport & """ attribute=""data"" />" & vbCrLf &
                         "       </Signal>" & vbCrLf &
                         "   </SignalSnippit>" & vbCrLf &
                         "</AtXmlSignalDescription>"

                status = atxml_IssueSignal(XmlBuf, Response, MAX_XML_SIZE)

                t = ""
                For i = 1 To Len(Response)
                    ch = Mid(Response, i, 1)
                    If Asc(ch) = 10 Then
                        t &= vbCrLf
                    Else
                        t &= ch
                    End If
                Next i

            Catch ex As SystemException
                MessageBox.Show(ex.Message)
            End Try
        End With
    End Sub

    Sub ReceiveCanData()
        Dim status As Integer
        Dim response As String
        Dim configData As CANConfig = New CANConfig()

        Try
            response = Space(MAX_XML_SIZE)

            configData = frmBus_IO.BuildCANConfigData()
            configData.listenOnly = "1"
            SendCANMessage("Fetch", configData, response)

            Dim temp As String
            temp = Space(5000)

            If InStr(response, "ErrStatus") = 0 Then
                status = atxml_GetStringValue(response, "data", temp, 5000)
                Dim dataR As String
                dataR = Trim(temp)

                frmBus_IO.receiveddata = dataR
                If frmBus_IO.optDataReceiveCAN.Checked = True Then
                    Dim hexDataSet As String() = converttohex(dataR)
                    Dim hexDataStr As String = ""
                    For i = 0 To hexDataSet.Count - 1
                        hexDataStr = hexDataStr & hexDataSet(i)
                    Next
                    frmBus_IO.txtDataReceivedCAN.Text = hexDataStr
                End If

            Else
                frmBus_IO.receiveddata = CheckInstrumentError(response)
                If (frmBus_IO.receiveddata <> "") Then frmBus_IO.txtDataReceivedCAN.Text = frmBus_IO.receiveddata.Replace(",", "")
            End If

        Catch ex As SystemException
            frmBus_IO.txtDataReceivedCAN.Text = ex.Message
        End Try
    End Sub

    Sub SendCanData()
        Dim response As String
        Dim configData As CANConfig = New CANConfig()

        Try
            response = Space(MAX_XML_SIZE)

            configData = frmBus_IO.BuildCANConfigData()

            SendCANMessage("Apply", configData, response)
            If response.Length > 0 Then
                frmBus_IO.txtDataReceivedCAN.Text = response
            End If
        Catch ex As SystemException
            frmBus_IO.txtDataReceivedCAN.Text = ex.Message
        End Try
    End Sub

    Function CheckInstrumentError(ByVal res As String) As String
        CheckInstrumentError = ""

        If InStr(res, "ErrStatus") <> 0 Then
            Dim startPos As String
            Dim endPos As String
            startPos = InStr(res, "errText=")
            startPos += 8
            endPos = InStr(startPos, res, ">", CompareMethod.Binary)
            CheckInstrumentError = Mid(res, startPos, endPos - startPos)
        Else
            CheckInstrumentError = ""
        End If
    End Function

    Function BitsToUnicode(ByVal bits As String, ByVal wordsize As Short) As String
        BitsToUnicode = ""
        Dim Temp2 As String = ""

        'strip , and spaces and convert to 1 and 0
        Dim temp As String = ""
        Dim c As String

        Dim i As Short
        For i = 1 To Len(bits)
            c = Mid(bits, i, 1)
            Select Case c
                Case " "
                    'do nothing
                Case ","
                    'do nothing
                Case "L"
                    temp &= "0"
                Case "H"
                    temp &= "1"

                Case Else
                    'do nothing
            End Select
        Next i

        'read through temp and translate a byte at a time
        If wordsize = 8 Then
            Dim GetBinary As Integer, Num As String, Binary As Integer
            '            Dim FinalString As String, NewString As String

            For i = 0 To Len(temp) / wordsize - 1
                c = Mid(temp, i * wordsize + 1, wordsize)
                Binary = 0
                For GetBinary = 1 To wordsize
                    Num = Mid(c, GetBinary, 1)
                    Select Case Num
                        Case "1"
                            If GetBinary = 1 Then
                                Binary += 128
                            ElseIf GetBinary = 2 Then
                                Binary += 64
                            ElseIf GetBinary = 3 Then
                                Binary += 32
                            ElseIf GetBinary = 4 Then
                                Binary += 16
                            ElseIf GetBinary = 5 Then
                                Binary += 8
                            ElseIf GetBinary = 6 Then
                                Binary += 4
                            ElseIf GetBinary = 7 Then
                                Binary += 2
                            ElseIf GetBinary = 8 Then
                                Binary += 1
                            End If
                    End Select
                Next GetBinary
                Temp2 &= Convert.ToString(Chr(Binary))
            Next i
        Else
            Temp2 = "Can only translate 8 bit word length"
        End If
        BitsToUnicode = Temp2
    End Function

    Function converttohex(ByVal data As String) As String()
        converttohex = Nothing
        ' VB6 bad scope Dim:
        Dim d As String

        'Converts any datatype (LHLHLHLL, 01110111, decimal, and text) to Hex
        'determine type of data
        Dim i As Integer
        Dim c As String
        Dim isLHbits As Boolean
        isLHbits = True
        'Remove null character
        Dim ts As String
        ts = ""
        For i = 1 To Len(data)
            c = Mid(data, i, 1)
            If c = Convert.ToString(Chr(0)) Then

            Else
                ts &= c
            End If
        Next i
        data = ts
        'check to see if data is in the form "LHLHHHLL, LLHHLLHH"
        For i = 1 To Len(data)
            c = Mid(data, i, 1)
            Select Case c
                Case "L", "H", ",", " ", "l", "h"

                Case Else
                    isLHbits = False
            End Select
        Next i
        'check to see if data is in the form "01001001, 01010010"
        Dim is01bits As Boolean
        is01bits = True
        For i = 1 To Len(data)
            c = Mid(data, i, 1)
            Select Case c
                Case "0", "1", ",", " "

                Case Else
                    is01bits = False
            End Select
        Next i
        Select Case InStr(data, ",")
            Case 6, 7, 8, 9, 0

            Case Else
                is01bits = False
                isLHbits = False
        End Select

        Dim pos As Integer

        'check to see if data is Hex "05, 3F, 2B"
        Dim ishex As Boolean
        ishex = True
        For i = 1 To Len(data)
            c = Mid(data, i, 1)
            Select Case c
                Case "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "a", "b", "c", "d", "e", "f", ",", " "

                Case Else
                    ishex = False
            End Select
        Next i
        'determine if there are always two characters seperated by ", " for isHex=true
        If ishex = True Then
            For i = 0 To Len(data) / 2 - 1
                c = Mid(data, i * 2 + 1, 2)
                If c = ", " Then
                    'do nothing
                Else
                    Dim j As Short

                    For j = 1 To 2
                        d = Mid(c, j, 1)
                        Select Case d
                            Case "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "a", "b", "c", "d", "e", "f"

                            Case Else
                                ishex = False
                        End Select
                    Next j
                End If
            Next i
        End If
        'check to see if the wordlength is greater than 4
        If Len(data) < 5 Then
            is01bits = False
            isLHbits = False
        End If
        'check to see if it is 2 characters followed by a comma and a space
        Dim seltype As New frmDataType
        seltype.formShown = False
        If Len(data) > 2 And ishex = True Then
            If InStr(data, ",") <> 3 Then
                ishex = False
            End If
        Else
            'make a form to have the user pick their data type
            'if the user enters a two digit number or a, b, c, d, e, or f then it could be hex
            'or it could be a string they want to send.
            If ishex = True Then
                seltype.bits01 = is01bits
                seltype.bitsLH = isLHbits
                seltype.hex = ishex
                seltype.senddata = data
                seltype.ShowDialog()
            End If
        End If

        Dim isText As Boolean
        isText = False
        If isLHbits = False And is01bits = False And ishex = False Then
            isText = True
        End If

        'if there is no commas then we need to have the user pick the type
        If Len(data) <= 8 And InStr(data, ",") = 0 And (isLHbits = True Or is01bits = True Or ishex = True) And seltype.formShown = False Then
            If Not (isText = True And isLHbits = False And is01bits = False And ishex = False) Then
                seltype.bits01 = is01bits
                seltype.bitsLH = isLHbits
                seltype.hex = ishex
                seltype.senddata = data
                seltype.ShowDialog()
            End If
        Else
            If Not (ishex = True Or isLHbits = True Or is01bits = True) Then
                isText = True
            End If
        End If
        If seltype.formShown = True Then
            is01bits = seltype.bits01
            isLHbits = seltype.bitsLH
            ishex = seltype.hex
            isText = seltype.txt
            data = seltype.senddata
        End If

        'if data is 01001001 or LHLHHHLL determine the word length
        If is01bits = True Or isLHbits = True Then

            pos = InStr(data, ",") 'find the position of the first comma, ","
            If pos = 0 Then
                pos = Len(data) 'pos = wordlength
            Else
                pos -= 1 'pos = wordlength
            End If
        End If

        Dim hexchars() As String = {""}
        Dim hex1 As String
        Dim hex2 As String

        If isLHbits = True Then
            'convert bits to hex
            'count the number of commas to find number of characters
            'and convert from L & H to 1 & 0
            Dim count As Short
            count = 0
            Dim temp As String = ""
            For i = 1 To Len(data)
                c = Mid(data, i, 1)
                Select Case c
                    Case ","
                        count += 1
                        temp &= c
                    Case "L", "l"
                        temp &= "0"
                    Case "H", "h"
                        temp &= "1"

                    Case Else
                        temp &= c
                End Select
            Next i
            data = temp

            'get the first byte
            Select Case pos
                Case Is > 8
                    For i = 0 To Len(data) / 8 - 1
                        c = Mid(data, 8 * i + 1, 8)
                        'get first nibble (4 bits)
                        d = Mid(c, 1, 4)
                        hex1 = nibbletohex(d)
                        hex2 = nibbletohex(Mid(c, 5, 4))
                        ReDim Preserve hexchars(i)
                        hexchars(i) = hex1 & hex2
                    Next i
                Case 8
                    For i = 0 To Len(data) / 10 - 1
                        c = Mid(data, 10 * i + 1, 10)
                        'get first nibble (4 bits)
                        d = Mid(c, 1, 4)
                        hex1 = nibbletohex(d)
                        hex2 = nibbletohex(Mid(c, 5, 4))
                        ReDim Preserve hexchars(i)
                        hexchars(i) = hex1 & hex2
                    Next i
                Case 7
                    For i = 0 To Len(data) / 9 - 1
                        c = Mid(data, 9 * i + 1, 7)
                        'get first nibble (4 bits)
                        d = Mid(c, 1, 3)
                        hex1 = nibbletohex("0" & d)
                        hex2 = nibbletohex(Mid(c, 4, 4))
                        ReDim Preserve hexchars(i)
                        hexchars(i) = hex1 & hex2
                    Next i
                Case 6
                    For i = 0 To Len(data) / 8 - 1
                        c = Mid(data, 8 * i + 1, 6)
                        'get first nibble (4 bits)
                        d = Mid(c, 1, 2)
                        hex1 = nibbletohex("00" & d)
                        hex2 = nibbletohex(Mid(c, 3, 4))
                        ReDim Preserve hexchars(i)
                        hexchars(i) = hex1 & hex2
                    Next i
                Case 5
                    For i = 0 To Len(data) / 7 - 1
                        c = Mid(data, 7 * i + 1, 5)
                        'get first nibble (4 bits)
                        d = Mid(c, 1, 1)
                        hex1 = nibbletohex("000" & d)
                        hex2 = nibbletohex(Mid(c, 2, 4))
                        ReDim Preserve hexchars(i)
                        hexchars(i) = hex1 & hex2
                    Next i

                Case Else
            End Select
        End If
        If is01bits = True Then
            'convert bits to hex
            'get the first byte
            Select Case pos
                Case 8
                    For i = 0 To Len(data) / 10 - 1
                        c = Mid(data, 10 * i + 1, 10)
                        'get first nibble (4 bits)
                        d = Mid(c, 1, 4)
                        hex1 = nibbletohex(d)
                        hex2 = nibbletohex(Mid(c, 5, 4))
                        ReDim Preserve hexchars(i)
                        hexchars(i) = hex1 & hex2
                    Next i
                Case 7
                    For i = 0 To Len(data) / 9 - 1
                        c = Mid(data, 9 * i + 1, 7)
                        'get first nibble (4 bits)
                        d = Mid(c, 1, 3)
                        hex1 = nibbletohex("0" & d)
                        hex2 = nibbletohex(Mid(c, 4, 4))
                        ReDim Preserve hexchars(i)
                        hexchars(i) = hex1 & hex2
                    Next i
                Case 6
                    For i = 0 To Len(data) / 8 - 1
                        c = Mid(data, 8 * i + 1, 6)
                        'get first nibble (4 bits)
                        d = Mid(c, 1, 2)
                        hex1 = nibbletohex("00" & d)
                        hex2 = nibbletohex(Mid(c, 3, 4))
                        ReDim Preserve hexchars(i)
                        hexchars(i) = hex1 & hex2
                    Next i
                Case 5
                    For i = 0 To Len(data) / 7 - 1
                        c = Mid(data, 7 * i + 1, 5)
                        'get first nibble (4 bits)
                        d = Mid(c, 1, 1)
                        hex1 = nibbletohex("000" & d)
                        hex2 = nibbletohex(Mid(c, 2, 4))
                        ReDim Preserve hexchars(i)
                        hexchars(i) = hex1 & hex2
                    Next i

                Case Else
            End Select
        End If
        If ishex = True Then
            'make array of hex chars
            For i = 0 To Len(data) / 4 - 1
                c = Mid(data, i * 4 + 1, 2)
                ReDim Preserve hexchars(i)
                hexchars(i) = UCase(c)
            Next i
        End If
        Dim ccode As Integer
        Dim Temp2 As String
        If isText = True Then
            'Convertstring to hex
            For i = 1 To Len(data)
                c = Mid(data, i, 1)
                ccode = Asc(c)
                Temp2 = Hex(ccode)
                If Len(Temp2) = 1 Then
                    Temp2 = "0" & Temp2
                End If
                ReDim Preserve hexchars(i - 1)
                hexchars(i - 1) = Temp2
            Next i
        End If
        converttohex = hexchars
    End Function

    Function nibbletohex(ByVal d As String) As String
        nibbletohex = ""
        'Converts 4 bit binary to hex
        'convert to decimal
        Dim i As Short
        Dim c As String
        Dim bin As Integer
        For i = 0 To 3
            c = Mid(d, i + 1, 1)
            If c = "1" Then
                bin += 2 ^ (3 - i)
            End If
        Next i

        Select Case bin
            Case 0, 1, 2, 3, 4, 5, 6, 7, 8, 9
                nibbletohex = Trim(Str(bin))
            Case 10
                nibbletohex = "A"
            Case 11
                nibbletohex = "B"
            Case 12
                nibbletohex = "C"
            Case 13
                nibbletohex = "D"
            Case 14
                nibbletohex = "E"
            Case 15
                nibbletohex = "F"

            Case Else
        End Select
    End Function

    Function dectohex(ByVal ccode As Short) As String
        dectohex = ""
        Dim whole As Short
        Dim remainder As Double
        whole = ccode \ 16 '33 \ 16 = 2
        remainder = ccode Mod 16 '33 Mod 16 = 1
        Dim hex1 As String = ""
        Dim hex2 As String = ""
        Select Case whole
            Case 0, 1, 2, 3, 4, 5, 6, 7, 8, 9
                hex1 = Trim(Str(whole))
            Case 10
                hex1 = "A"
            Case 11
                hex1 = "B"
            Case 12
                hex1 = "C"
            Case 13
                hex1 = "D"
            Case 14
                hex1 = "E"
            Case 15
                hex1 = "F"

            Case Else
        End Select
        Select Case remainder
            Case 0, 1, 2, 3, 4, 5, 6, 7, 8, 9
                hex2 = Trim(Str(remainder))
            Case 10
                hex2 = "A"
            Case 11
                hex2 = "B"
            Case 12
                hex2 = "C"
            Case 13
                hex2 = "D"
            Case 14
                hex2 = "E"
            Case 15
                hex2 = "F"

            Case Else
        End Select
        dectohex = hex1 & hex2
    End Function

    Function LHto01(ByVal lh As String) As String
        LHto01 = ""
        'converts an LH bit string to 01 bit string
        Dim i As Integer
        Dim ch As String
        Dim temp As String
        temp = ""
        For i = 1 To Len(lh)
            ch = Mid(lh, i, 1)
            If ch = "L" Or ch = "l" Then
                temp &= "0"
            End If
            If ch = "H" Or ch = "h" Then
                temp &= "1"
            End If
        Next i
        LHto01 = temp
    End Function

    Function hextobin(ByVal hexstr As String) As String
        hextobin = ""
        'Converts a hex string to binary
        'take one hex character at a time and convert it
        Dim i As Integer
        Dim ch As String
        Dim temp As String
        temp = ""
        For i = 1 To Len(hexstr)
            ch = Mid(hexstr, i, 1)
            Select Case ch
                Case "0"
                    temp &= "LLLL"
                Case "1"
                    temp &= "LLLH"
                Case "2"
                    temp &= "LLHL"
                Case "3"
                    temp &= "LLHH"
                Case "4"
                    temp &= "LHLL"
                Case "5"
                    temp &= "LHLH"
                Case "6"
                    temp &= "LHHL"
                Case "7"
                    temp &= "LHHH"
                Case "8"
                    temp &= "HLLL"
                Case "9"
                    temp &= "HLLH"
                Case "A"
                    temp &= "HLHL"
                Case "B"
                    temp &= "HLHH"
                Case "C"
                    temp &= "HHLL"
                Case "D"
                    temp &= "HHLH"
                Case "E"
                    temp &= "HHHL"
                Case "F"
                    temp &= "HHHH"
            End Select
        Next i
        hextobin = temp
    End Function


    Sub RemoveSerial(ByVal configData As SerialConfig)
        'Removes PCI Serial from kernel
        Dim status As Integer
        Dim XmlBuf As String
        Dim Response As String

        XmlBuf = Space(4096)
        Response = Space(MAX_XML_SIZE)

        XmlBuf = "<AtXmlSignalDescription>" & vbCrLf &
                    "<SignalAction>Remove</SignalAction>" & vbCrLf &
                    "<SignalResourceName>" & ResourceName &
                    "</SignalResourceName>" & vbCrLf &
                    "<SignalSnippit>" & vbCrLf &
                    "<Signal Name=""RS_SIGNAL"" Out=""exchange"">" & vbCrLf &
                    "<" & configData.protocol & " name=""exchange"" parity=""" & configData.parity &
                                                            """ baud_rate=""" & configData.bitrate &
                                                            """ stop_bits=""" & configData.stopbits &
                                                            """ terminate=""" & configData.term &
                                                            """ maxTime=""" & configData.maxt &
                                                            """ delay=""" & configData.delay &
                                                            """ wordLength=""" & configData.wordlength & """ />" & vbCrLf &
                    "</Signal>" & vbCrLf &
                    "</SignalSnippit>" & vbCrLf &
                    "</AtXmlSignalDescription>"

        status = atxml_IssueSignal(XmlBuf, Response, MAX_XML_SIZE)
    End Sub

    Sub ResetNetwork(ByVal configData As NetworkConfig)
        'Resets Ethernet
        Dim status As Integer
        Dim XmlBuf As String
        Dim Response As String
        Dim tempr As String = ""

        Try
            XmlBuf = Space(4096)
            Response = Space(MAX_XML_SIZE)

            XmlBuf = "<AtXmlSignalDescription> " & vbCrLf &
                        "   <SignalAction>Reset</SignalAction>" & vbCrLf &
                        "   <SignalResourceName>" & ResourceName &
                        "</SignalResourceName>" & vbCrLf &
                        "   <SignalSnippit>" & vbCrLf &
                        "       <Signal Name=""ETHERNET_SIGNAL"" Out=""exchange"">" & vbCrLf &
                        "           <" & configData.protocol & " name=""exchange"" maxTime=""" & configData.maxt &
                                                                    """ localIP=""" & configData.localip &
                                                                    """ localSubnetMask=""" & configData.mask &
                                                                    """ localGateway=""" & configData.gateway &
                                                                    """ remoteIP=""" & configData.remoteip &
                                                                    """ remotePort=""" & configData.remport & """ />" & vbCrLf &
                        "       </Signal>" & vbCrLf &
                        "   </SignalSnippit>" & vbCrLf &
                        "</AtXmlSignalDescription>"

            status = atxml_IssueSignal(XmlBuf, Response, MAX_XML_SIZE)

        Catch ex As SystemException
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Sub ResetSerial(ByVal configData As SerialConfig)
        'Resets Serial
        Dim status As Integer
        Dim XmlBuf As String
        Dim Response As String

        XmlBuf = Space(4096)
        Response = Space(MAX_XML_SIZE)

        XmlBuf = "<AtXmlSignalDescription>" & vbCrLf &
                    "<SignalAction>Reset</SignalAction>" & vbCrLf &
                    "<SignalResourceName>" & ResourceName &
                    "</SignalResourceName>" & vbCrLf &
                    "<SignalSnippit>" & vbCrLf &
                    "<Signal Name=""RS_SIGNAL"" Out=""exchange"">" & vbCrLf &
                    "<" & configData.protocol & " name=""exchange""" &
                                                    """ data_bits=""""" &
                                                    """ parity=""" & configData.parity &
                                                    """ baud_rate=""" & configData.bitrate &
                                                    """ stop_bits=""" & configData.stopbits &
                                                    """ maxTime=""" & configData.maxt &
                                                    """ delay=""" & configData.delay &
                                                    """ terminate= ""OFF""" &
                                                    """ wordLength=""" & configData.wordlength & """ />" & vbCrLf &
                    "</Signal>" & vbCrLf &
                    "</SignalSnippit>" & vbCrLf &
                    "</AtXmlSignalDescription>"

        status = atxml_IssueSignal(XmlBuf, Response, MAX_XML_SIZE)
    End Sub

    Sub SendEtherData(ByVal datatext As String, ByVal configData As NetworkConfig, ByRef returnDisplay As TextBox)
        Dim status As Integer
        Dim XmlBuf As String
        Dim Response As String

        Try
            XmlBuf = Space(4096)
            Response = Space(MAX_XML_SIZE)

            XmlBuf = "<AtXmlSignalDescription> " & vbCrLf &
                        "   <SignalAction>Enable</SignalAction>" & vbCrLf &
                        "   <SignalResourceName>" & ResourceName &
                        "</SignalResourceName>" & vbCrLf &
                        "   <SignalSnippit>" & vbCrLf &
                        "       <Signal Name=""ETHERNET_SIGNAL"" Out=""exchange"">" & vbCrLf &
                        "           <" & configData.protocol & " name=""exchange"" maxTime=""" & configData.maxt &
                                                                    """ data_bits=""" & StrToHL(datatext) &
                                                                    """ localIP=""" & configData.localip &
                                                                    """ localSubnetMask=""" & configData.mask &
                                                                    """ localGateway=""" & configData.gateway &
                                                                    """ remoteIP=""" & configData.remoteip &
                                                                    """ remotePort=""" & configData.remport & """ />" & vbCrLf &
                        "       </Signal>" & vbCrLf &
                        "   </SignalSnippit>" & vbCrLf &
                        "</AtXmlSignalDescription>"

            status = atxml_IssueSignal(XmlBuf, Response, MAX_XML_SIZE)

            Dim ch As String
            Dim t As String = ""
            Dim res(2) As String
            For i = 1 To Len(Response)
                ch = Mid(Response, i, 1)
                If Asc(ch) = 10 Then
                    t &= vbCrLf
                Else
                    t &= ch
                End If
            Next i
            'res(0) = t
            frmBus_IO.txtDataReceivedGigabit1.Text = t

            'perform disable
            XmlBuf = "<AtXmlSignalDescription> " & vbCrLf &
                        "   <SignalAction>Disable</SignalAction>" & vbCrLf &
                        "   <SignalResourceName>" & ResourceName &
                        "</SignalResourceName>" & vbCrLf &
                        "   <SignalSnippit>" & vbCrLf &
                        "       <Signal Name=""ETHERNET_SIGNAL"" Out=""exchange"">" & vbCrLf &
                        "           <" & configData.protocol & " name=""exchange"" maxTime=""" & configData.maxt &
                                                                    """ localIP=""" & configData.localip &
                                                                    """ localSubnetMask=""" & configData.mask &
                                                                    """ localGateway=""" & configData.gateway &
                                                                    """ remoteIP=""" & configData.remoteip &
                                                                    """ remotePort=""" & configData.remport &
                                                                    """ attribute=""data"" />" & vbCrLf &
                        "       </Signal>" & vbCrLf &
                        "   </SignalSnippit>" & vbCrLf &
                        "</AtXmlSignalDescription>"

            status = atxml_IssueSignal(XmlBuf, Response, MAX_XML_SIZE)
            frmBus_IO.receiveddata = CheckInstrumentError(Response)
            If (frmBus_IO.receiveddata <> "") Then returnDisplay.Text = frmBus_IO.receiveddata

        Catch ex As SystemException
            MessageBox.Show(ex.Message, "Network Configruation Error")
        End Try
    End Sub

    Sub SendSerialData(ByVal datatext As String, ByVal configData As SerialConfig, ByRef returnDisplay As TextBox)
        Dim status As Integer
        Dim XmlBuf As String
        Dim Response As String

        XmlBuf = Space(4096)
        Response = Space(MAX_XML_SIZE)
        ' initialize resource name

        XmlBuf = "<AtXmlSignalDescription>" & vbCrLf &
                    "   <SignalAction>Apply</SignalAction>" & vbCrLf &
                    "   <SignalResourceName>" & ResourceName &
                    "</SignalResourceName>" & vbCrLf &
                    "   <SignalSnippit>" & vbCrLf &
                    "       <Signal Name=""RS_SIGNAL"" Out=""exchange"">" & vbCrLf &
                    "           <" & configData.protocol & " name=""exchange" &
                                            """ data_bits=""" & StrToHL(datatext, Convert.ToInt16(configData.wordlength)) &
                                            """ parity=""" & configData.parity &
                                            """ baud_rate=""" & configData.bitrate &
                                            """ stop_bits=""" & configData.stopbits &
                                            """ maxTime=""" & configData.maxt &
                                            """ terminate= ""OFF" &
                                            """ delay=""" & configData.delay &
                                            """ wordLength=""" & configData.wordlength & """ />" & vbCrLf &
                    "       </Signal>" & vbCrLf &
                    "   </SignalSnippit>" & vbCrLf &
                    "</AtXmlSignalDescription>"

        status = atxml_IssueSignal(XmlBuf, Response, MAX_XML_SIZE)
        frmBus_IO.receiveddata = CheckInstrumentError(Response)

        If (frmBus_IO.receiveddata <> "") Then
            returnDisplay.Text = frmBus_IO.receiveddata
        End If
    End Sub

    'Sub SendCom1XML(ByVal xml As String)
    Sub SendOutXML(ByVal xml As String, ByVal iResource As Short)
        With frmBus_IO
            Dim status As Integer
            Dim XmlBuf As String
            Dim Response As String
            Dim RecievedTextBox As TextBox = New TextBox()

            Select Case iResource
                Case TAB_COM_1
                    RecievedTextBox = .txtDataReceivedCom1
                    ResourceName = "COM_1"
                Case TAB_COM_2
                    RecievedTextBox = .txtDataReceivedCom2
                    ResourceName = "COM_2"
                Case TAB_GIGABIT1
                    RecievedTextBox = .txtDataReceivedGigabit1
                    ResourceName = "ETHERNET_1"
                Case TAB_PCISER
                    If frmBus_IO.optRS485.Checked Then
                        ResourceName = "COM_3"
                    ElseIf frmBus_IO.optRS422.Checked Then
                        ResourceName = "COM_4"
                    Else
                        ResourceName = "COM_5"
                    End If
                    RecievedTextBox = .txtDataReceivedPCISer
                Case TAB_GIGABIT2
                    RecievedTextBox = .txtDataReceivedGigabit2
                    ResourceName = "ETHERNET_2"
                Case TAB_CAN
                    RecievedTextBox = .txtDataReceivedCAN
                    ResourceName = "CAN_1"
            End Select

            If iResource < 0 Or iResource > UBound(ResName) Then Exit Sub
            XmlBuf = Space(4096)
            Response = Space(4096)
            XmlBuf = xml

            status = atxml_IssueSignal(XmlBuf, Response, MAX_XML_SIZE)

            RecievedTextBox.Text = Response
            .receiveddata = ""
            Dim ch As String

            Dim t As String = ""
            Dim i As Integer
            For i = 1 To RecievedTextBox.Text.Length
                ch = Mid(RecievedTextBox.Text, i, 1)
                If Asc(ch) = 10 Then
                    t &= vbCrLf
                Else
                    t &= ch
                End If
            Next i
        End With
    End Sub

   
    Sub SendNetworkSettings(ByVal configData As NetworkConfig, ByRef returnDisplay As TextBox)
        Dim status As Integer
        Dim XmlBuf As String
        Dim Response As String
        Dim tempr As String = ""

        Try
            XmlBuf = Space(4096)
            Response = Space(MAX_XML_SIZE)

            XmlBuf = "<AtXmlSignalDescription> " & vbCrLf &
                        "   <SignalAction>Setup</SignalAction>" & vbCrLf &
                        "   <SignalResourceName>" & ResourceName &
                        "</SignalResourceName>" & vbCrLf &
                        "   <SignalSnippit>" & vbCrLf &
                        "       <Signal Name=""ETHERNET_SIGNAL"" Out=""exchange"">" & vbCrLf &
                        "           <" & configData.protocol & " name=""exchange"" maxTime=""" & configData.maxt &
                                                                    """ localIP=""" & configData.localip &
                                                                    """ localSubnetMask=""" & configData.mask &
                                                                    """ localGateway=""" & configData.gateway &
                                                                    """ remoteIP=""" & configData.remoteip &
                                                                    """ remotePort=""" & configData.remport & """ />" & vbCrLf &
                        "       </Signal>" & vbCrLf &
                        "   </SignalSnippit>" & vbCrLf &
                        "</AtXmlSignalDescription>"

            status = atxml_IssueSignal(XmlBuf, Response, MAX_XML_SIZE)
            frmBus_IO.receiveddata = CheckInstrumentError(Response)
            If (frmBus_IO.receiveddata <> "") Then returnDisplay.Text = frmBus_IO.receiveddata

        Catch ex As SystemException
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Sub SendSerialSettings(ByVal configData As SerialConfig, ByRef returnDisplay As TextBox)
        Dim status As Integer
        Dim XmlBuf As String
        Dim Response As String

        Try
            XmlBuf = Space(4096)
            Response = Space(MAX_XML_SIZE)

            XmlBuf = "<AtXmlSignalDescription>" & vbCrLf &
                     "<SignalAction>Setup</SignalAction>" & vbCrLf &
                     "<SignalResourceName>" & ResourceName &
                     "</SignalResourceName>" & vbCrLf &
                     "<SignalSnippit>" & vbCrLf &
                     "<Signal Name=""RS_SIGNAL"" Out=""exchange"">" & vbCrLf &
                         "<" & configData.protocol & " name=""exchange"" parity=""" & configData.parity &
                                                                     """ baud_rate=""" & configData.bitrate &
                                                                     """ stop_bits=""" & configData.stopbits &
                                                                     """ terminate=""OFF" &
                                                                     """ data_bits=""" &
                                                                     """ maxTime=""" & configData.maxt &
                                                                     """ delay=""" & configData.delay &
                                                                     """ wordLength=""" & configData.wordlength & """ />" & vbCrLf &
                     "</Signal>" & vbCrLf &
                     "</SignalSnippit>" & vbCrLf &
                     "</AtXmlSignalDescription>"

            status = atxml_IssueSignal(XmlBuf, Response, MAX_XML_SIZE)
            frmBus_IO.receiveddata = CheckInstrumentError(Response)
            If (frmBus_IO.receiveddata <> "") Then returnDisplay.Text = frmBus_IO.receiveddata
        Catch ex As SystemException
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Sub SendCanSettings()
        Dim response As String
        Dim configData As CANConfig = New CANConfig()

        Try
            response = Space(MAX_XML_SIZE)

            configData = frmBus_IO.BuildCANConfigData()

            SendCANMessage("Setup", configData, response)
            frmBus_IO.txtDataReceivedCAN.Text = response
        Catch ex As SystemException
            frmBus_IO.txtDataReceivedCAN.Text = ex.Message
        End Try
    End Sub

    ''' <summary>
    ''' Builds the XML string for a CAN message, sends it, and returns the response message
    ''' </summary>
    ''' <param name="command">String for the type of message to send. i.e. "Fetch", "Apply", "Setup", etc.</param>
    ''' <param name="configData">a CAN Configuration structure containing the message to send</param>
    ''' <param name="messageResponse">Any data received in response to the message.  NOTE: enough space must be allocated for the anticipated return prior to calling this sub</param>
    ''' <param name="configure">Used if command is Fetch to set attribute to "config" if true or "data" if false - defaults to false</param>
    ''' <remarks>Does not catch any exceptions, the caller is responsible</remarks>
    Public Sub SendCANMessage(ByVal command As String, ByVal configData As CANConfig, ByRef messageResponse As String, Optional ByVal configure As Boolean = False)
            Dim status As Integer
            Dim XmlBuf As String
            Dim Response As String = ""
            Dim attributeStr As String = "config"
            XmlBuf = Space(4096)

            Response = Space(MAX_XML_SIZE)
            ' initialize resource name
            ResourceName = "CAN_1"

            XmlBuf = "<AtXmlSignalDescription> " & vbCrLf
            XmlBuf = XmlBuf & "   <SignalAction>" & command & "</SignalAction>" & vbCrLf
            XmlBuf = XmlBuf & "   <SignalResourceName>" & ResourceName
            XmlBuf = XmlBuf & "</SignalResourceName>" & vbCrLf
            XmlBuf = XmlBuf & "   <SignalSnippit>" & vbCrLf
            XmlBuf = XmlBuf & "       <Signal Name=""CAN_SIGNAL"" Out=""exchange"">" & vbCrLf
            XmlBuf = XmlBuf & "           <CAN name=""exchange"" maxTime=""" & configData.maxTime & """"
            XmlBuf = XmlBuf & "                       timingValue=""" & configData.timing & """"
            XmlBuf = XmlBuf & "                      threeSamples=""" & configData.threeSamples & """"
            XmlBuf = XmlBuf & "                      singleFilter=""" & configData.singleFilter & """"
            XmlBuf = XmlBuf & "                      acceptanceCode=""" & configData.acceptanceCode & """"
            XmlBuf = XmlBuf & "                      acceptanceMask=""" & configData.acceptanceMask & """"
            XmlBuf = XmlBuf & "                      channel=""" & configData.channel & """"
            XmlBuf = XmlBuf & "                      listenOnly=""" & configData.listenOnly & """"
            If command = "Apply" Then
                XmlBuf = XmlBuf & "                  data_bits=""" & configData.dataBits & """"
            End If
            If command = "Fetch" Then
                If configure Then
                    XmlBuf = XmlBuf & "             attribute=""config"""
                Else
                    XmlBuf = XmlBuf & "             attribute=""data"""
                End If
            End If
            XmlBuf = XmlBuf & "/>" & vbCrLf
            XmlBuf = XmlBuf & "       </Signal>" & vbCrLf
            XmlBuf = XmlBuf & "   </SignalSnippit>" & vbCrLf
            XmlBuf = XmlBuf & "</AtXmlSignalDescription>"

            status = atxml_IssueSignal(XmlBuf, Response, MAX_XML_SIZE)
            If status <> 0 Then
                Throw New SystemException(CheckInstrumentError(Response))
            End If
            frmBus_IO.receiveddata = Response
            messageResponse = Response
    End Sub

    Function Limit(ByVal CheckValue As Short, ByVal LimitValue As Short) As Short
        If CheckValue < LimitValue Then
            Limit = LimitValue
        Else
            Limit = CheckValue
        End If
    End Function

    Function getEthernet_settings() As NetworkConfig
        Dim configData As NetworkConfig = New NetworkConfig()
        Dim status As Integer
        Dim XmlBuf As String
        Dim Response As String
        Dim Temp2 As Double
        Dim Wdata As String = ""

        XmlBuf = Space(4096)
        Response = Space(MAX_XML_SIZE)
        ' initialize resource name
        XmlBuf = "<AtXmlSignalDescription xmlns:atXml=""ATXML_TSF"">" & vbCrLf &
                 "<SignalAction>Fetch</SignalAction>" & vbCrLf &
                 "<SignalResourceName>" & ResourceName &
                 "</SignalResourceName>" & vbCrLf &
                 "<SignalSnippit>" & vbCrLf &
                 "<Signal Name=""ETHERNET_SIGNAL"" Out=""exchange"">" & vbCrLf &
                 "<TCP name=""exchange""" &
                    " data_bits=""" & Wdata & """" &
                    " maxTime=""" & configData.maxt & """" &
                    " localIP=""" & configData.localip & """" &
                    " localSubnetMask=""" & configData.mask & """" &
                    " localGateway=""" & configData.gateway & """" &
                    " remoteIP=""" & configData.remoteip & """" &
                    " remotePort=""" & configData.remport & """" &
                    " attribute=""config"" />" & vbCrLf &
                 "</Signal>" & vbCrLf &
                 "</SignalSnippit>" & vbCrLf &
                 "</AtXmlSignalDescription>"

        status = atxml_IssueSignal(XmlBuf, Response, MAX_XML_SIZE)

        Dim temp As String
        temp = Space(5000)

        status = atxml_GetStringValue(Response, "spec", temp, 255)
        configData.spec = Trim(temp)

        temp = Space(5000)
        status = atxml_GetStringValue(Response, "remoteIP", temp, 255)
        configData.remoteip = Trim(temp)

        Temp2 = 0
        status = atxml_GetSingleDblValue(Response, "remotePort", Temp2)
        configData.remport = Trim(Str(Temp2))

        temp = Space(5000)
        status = atxml_GetStringValue(Response, "localIP", temp, 255)
        configData.localip = Trim(temp)

        temp = Space(5000)
        status = atxml_GetStringValue(Response, "localSubnetMask", temp, 255)
        configData.mask = Trim(temp)

        temp = Space(5000)
        status = atxml_GetStringValue(Response, "localGateway", temp, 255)
        configData.gateway = Trim(temp)

        Temp2 = 0
        status = atxml_GetSingleDblValue(Response, "maxTime", Temp2)
        configData.maxt = Trim(Str(Temp2))

        getEthernet_settings = configData
    End Function

    Sub ConfigureGigabit1(ByVal configData As NetworkConfig)
        frmBus_IO.txtMaxTimeGigabit1.Text = configData.maxt
        frmBus_IO.txtGatewayGigabit1.Text = configData.gateway
        frmBus_IO.txtRemotePortGigabit1.Text = configData.remport
        frmBus_IO.txtRemoteIPGigabit1.Text = configData.remoteip
        frmBus_IO.txtLocalIPGigabit1.Text = configData.localip
        frmBus_IO.txtLocalSubnetMaskGigabit1.Text = configData.mask

        If InStr(configData.spec, "TCP") = 1 Then
            frmBus_IO.optTCPGigabit1.Checked = True
            frmBus_IO.optUDPGigabit1.Checked = False
        End If

        If InStr(configData.spec, "UDP") = 1 Then
            frmBus_IO.optTCPGigabit1.Checked = False
            frmBus_IO.optUDPGigabit1.Checked = True
        End If
    End Sub

    Sub ConfigureGigabit2(ByVal configData As NetworkConfig)
        frmBus_IO.txtMaxTimeGigabit2.Text = configData.maxt
        frmBus_IO.txtGatewayGigabit2.Text = configData.gateway
        frmBus_IO.txtRemotePortGigabit2.Text = configData.remport
        frmBus_IO.txtRemoteIPGigabit2.Text = configData.remoteip
        frmBus_IO.txtLocalIPGigabit2.Text = configData.localip
        frmBus_IO.txtLocalSubnetMaskGigabit2.Text = configData.mask

        If InStr(configData.spec, "TCP") = 1 Then
            frmBus_IO.optTCPGigabit2.Checked = True
            frmBus_IO.optUDPGigabit2.Checked = False
        End If

        If InStr(configData.spec, "UDP") = 1 Then
            frmBus_IO.optTCPGigabit2.Checked = False
            frmBus_IO.optUDPGigabit2.Checked = True
        End If
    End Sub

    Function getSerial_settings() As SerialConfig
        Dim configData As SerialConfig = New SerialConfig()
        Dim status As Integer
        Dim XmlBuf As String
        Dim Response As String

        XmlBuf = Space(4096)
        Response = Space(MAX_XML_SIZE)
        XmlBuf = "<AtXmlSignalDescription xmlns:atXml=""ATXML_TSF"">" &
                 "<SignalAction>Fetch</SignalAction>" &
                 "<SignalResourceName>" & ResourceName &
                 "</SignalResourceName>" &
                 "<SignalSnippit>" &
                 "<Signal Name=""RS_SIGNAL"" Out=""exchange"">" &
                 "<TCP name=""exchange"" attribute=""config"" />" &
                 "</Signal>" &
                 "</SignalSnippit>" &
                 "</AtXmlSignalDescription>"

        status = atxml_IssueSignal(XmlBuf, Response, MAX_XML_SIZE)

        Dim temp As String = ""
        Dim valueStart As Integer = InStr(Response, "<c:Value>")
        If valueStart > 0 Then
            Response = Response.Substring(valueStart + 8, InStr(Response, "</c:Value>") - valueStart - 9)
        End If
        Dim values() As String = Split(Response, ",")
        configData.bitrate = Convert.ToInt32(Decimal.Parse(values(0), Globalization.NumberStyles.Any)).ToString()
        configData.wordlength = values(1)
        Select Case values(2)
            Case "0"
                configData.parity = "NONE"
            Case "1"
                configData.parity = "ODD"
            Case "2"
                configData.parity = "EVEN"
            Case "3"
                configData.parity = "MARK"
        End Select
        Select Case values(3)
            Case "0"
                configData.stopbits = "1"
            Case "1"
                configData.stopbits = "1.5"
            Case "2"
                configData.stopbits = "2"
        End Select
        configData.maxt = values(4)

        getSerial_settings = configData
    End Function

    Sub ConfigurePCISer(ByVal configData As SerialConfig)
        With frmBus_IO
            For i = 0 To .cmbParityPCISer.Items.Count - 1
                If InStr(configData.parity, UCase(.cmbParityPCISer.Items.Item(i).ToString())) = 1 Then
                    .cmbParityPCISer.SelectedIndex = i
                End If
            Next i

            For i = 0 To .cmbStopBitsPCISer.Items.Count - 1
                If InStr(configData.stopbits, UCase(.cmbStopBitsPCISer.Items.Item(i).ToString())) = 1 Then
                    .cmbStopBitsPCISer.SelectedIndex = i
                End If
            Next i

            For i = 0 To .cmbBitRatePCISer.Items.Count - 1
                If InStr(configData.bitrate, UCase(.cmbBitRatePCISer.Items.Item(i).ToString())) = 1 Then
                    .cmbBitRatePCISer.SelectedIndex = i
                End If
            Next i

            .txtSerialDelayPCISer.Text = configData.delay
            .txtSerialMaxTimePCISer.Text = configData.maxt
            .txtSerialWordLengthPCISer.Text = configData.wordlength
        End With
    End Sub

    Sub ConfigureCom1(ByVal configData As SerialConfig)
        With frmBus_IO
            For i = 0 To .cmbParityCom1.Items.Count - 1
                If InStr(configData.parity, UCase(.cmbParityCom1.Items.Item(i).ToString())) = 1 Then
                    .cmbParityCom1.SelectedIndex = i
                End If
            Next i

            For i = 0 To .cmbStopBitsCom1.Items.Count - 1
                If InStr(configData.stopbits, UCase(.cmbStopBitsCom1.Items.Item(i).ToString())) = 1 Then
                    .cmbStopBitsCom1.SelectedIndex = i
                End If
            Next i

            For i = 0 To .cmbBitRateCom1.Items.Count - 1
                If InStr(configData.bitrate, UCase(.cmbBitRateCom1.Items.Item(i).ToString())) = 1 Then
                    .cmbBitRateCom1.SelectedIndex = i
                End If
            Next i

            .txtWordLengthCom1.Text = configData.wordlength
        End With
    End Sub

    Sub ConfigureCom2(ByVal configData As SerialConfig)
        With frmBus_IO
            For i = 0 To .cmbParityCom2.Items.Count - 1
                If InStr(configData.parity, UCase(.cmbParityCom2.Items.Item(i).ToString())) = 1 Then
                    .cmbParityCom2.SelectedIndex = i
                End If
            Next i

            For i = 0 To .cmbStopBitsCom2.Items.Count - 1
                If InStr(configData.stopbits, UCase(.cmbStopBitsCom2.Items.Item(i).ToString())) = 1 Then
                    .cmbStopBitsCom2.SelectedIndex = i
                End If
            Next i

            For i = 0 To .cmbBitRateCom2.Items.Count - 1
                If InStr(configData.bitrate, UCase(.cmbBitRateCom2.Items.Item(i).ToString())) = 1 Then
                    .cmbBitRateCom2.SelectedIndex = i
                End If
            Next i

            .txtWordLengthCom2.Text = configData.wordlength
        End With
    End Sub

    Sub getCan_settings()
        Dim status As Integer
        Dim response As String
        Dim configData As CANConfig = New CANConfig()

        Try
            response = Space(MAX_XML_SIZE)

            configData = frmBus_IO.BuildCANConfigData()

            SendCANMessage("Fetch", configData, response, True)

            Dim temp As String
            temp = Space(5000)

            status = atxml_GetStringValue(response, "spec", temp, 255)
            Dim spec As String
            spec = Trim(temp)

            Dim Temp2 As Double

            status = atxml_GetSingleDblValue(response, "maxTime", Temp2)
            Dim maxtime As String
            maxtime = Trim(Str(Temp2))

            Dim temp3 As Integer
            'temp3 = 0
            status = atxml_GetSingleIntValue(response, "timingValue", temp3)
            Dim tValue As String
            tValue = Trim(Str(temp3))


            status = atxml_GetSingleIntValue(response, "threeSamples", temp3)
            Dim tSamples As String
            tSamples = Trim(Str(temp3))

            status = atxml_GetSingleIntValue(response, "singleFilter", temp3)
            Dim sFilter As String
            sFilter = Trim(Str(temp3))

            status = atxml_GetStringValue(response, "acceptanceCode", temp, 255)
            Dim aCode() As String
            aCode = converttohex(Trim(temp))

            status = atxml_GetStringValue(response, "acceptanceMask", temp, 255)
            Dim aMask() As String
            aMask = converttohex(Trim(temp))

            status = atxml_GetSingleIntValue(response, "m_Channel", temp3)
            Dim chan As String
            chan = Trim(Str(temp3))

            With frmBus_IO
                SetCANTimingValue(tValue)

                If tSamples = "1" Then
                    .chkThreeSamplesCAN.Checked = True
                Else
                    .chkThreeSamplesCAN.Checked = False
                End If
                If sFilter = "1" Then
                    .chkSingleFilterCAN.Checked = True
                Else
                    .chkSingleFilterCAN.Checked = False
                End If

                .txtAcceptanceCodeCAN.Text = ""
                For i = 0 To UBound(aCode)
                    .txtAcceptanceCodeCAN.Text = .txtAcceptanceCodeCAN.Text & aCode(i)
                Next i

                .txtAcceptanceMaskCAN.Text = ""
                For i = 0 To UBound(aMask)
                    .txtAcceptanceMaskCAN.Text = .txtAcceptanceMaskCAN.Text & aMask(i)
                Next i

                If chan = "1" Then
                    .OptChannel1CAN.Checked = True
                End If

                If chan = "2" Then
                    .OptChannel2CAN.Checked = True
                End If

                .txtMaxTimeCAN.Text = maxtime
            End With
        Catch ex As SystemException
            frmBus_IO.txtDataReceivedCAN.Text = ex.Message
        End Try
    End Sub

    Sub ResetCan()
        Dim response As String
        Dim configData As CANConfig = New CANConfig()

        Try
            response = Space(MAX_XML_SIZE)

            configData = frmBus_IO.BuildCANConfigData()

            SendCANMessage("Reset", configData, response)
            frmBus_IO.txtDataReceivedCAN.Text = response
        Catch ex As SystemException
            frmBus_IO.txtDataReceivedCAN.Text = ex.Message
        End Try
    End Sub

    Sub RemoveCan()
        Dim response As String
        Dim configData As CANConfig = New CANConfig()

        Try
            response = Space(MAX_XML_SIZE)

            configData = frmBus_IO.BuildCANConfigData()

            SendCANMessage("Remove", configData, response)
            frmBus_IO.txtDataReceivedCAN.Text = response
        Catch ex As SystemException
            frmBus_IO.txtDataReceivedCAN.Text = ex.Message
        End Try
    End Sub

    Sub addresschange(ByVal caller As TextBox)
        Dim temp As String
        Dim ip(3) As String

        Dim c As String

        temp = caller.Text
        Dim i As Integer
        Dim S As Short

        Dim curse As Integer
        Dim tcurse As Integer
        curse = caller.SelectionStart
        Select Case curse
            Case 1 To 4
                tcurse = 3
            Case 5 To 8
                tcurse = 7
            Case 9 To 12
                tcurse = 11
            Case 13 To 16
                tcurse = 15

            Case Else
                tcurse = curse
        End Select

        'determines where to add characters
        S = 0

        For i = 1 To Len(temp)
            c = Mid(temp, i, 1)
            Select Case c
                Case "."
                    S += 1
                Case ":"
                    S += 1

                Case Else
                    If Len(Trim(ip(S))) < 3 Then
                        ip(S) &= c
                    Else
                        If S < 3 Then
                            ip(S + 1) &= c
                            curse = caller.SelectionStart + 2
                            Select Case curse
                                Case 1 To 4
                                    tcurse = 3
                                Case 5 To 8
                                    tcurse = 7
                                Case 9 To 12
                                    tcurse = 11
                                Case 13 To 16
                                    tcurse = 15

                                Case Else
                                    tcurse = curse
                            End Select
                        Else
                            curse = caller.SelectionStart + 2
                            Select Case curse
                                Case 1 To 4
                                    tcurse = 3
                                Case 5 To 8
                                    tcurse = 7
                                Case 9 To 12
                                    tcurse = 11
                                Case 13 To 16
                                    tcurse = 15

                                Case Else
                                    tcurse = curse
                            End Select
                        End If
                    End If
            End Select
        Next i
        ip(0) = Trim(ip(0))
        ip(1) = Trim(ip(1))
        ip(2) = Trim(ip(2))
        ip(3) = Trim(ip(3))

        If ip(0) <> "" Then
            Select Case Len(ip(0))
                Case 1
                    ip(0) = "  " & ip(0)
                Case 2
                    ip(0) = " " & ip(0)

                Case Else
            End Select
        Else
            ip(0) = "   "
        End If

        If ip(1) <> "" Then
            Select Case Len(ip(1))
                Case 1
                    ip(1) = "  " & ip(1)
                Case 2
                    ip(1) = " " & ip(1)

                Case Else
            End Select
        Else
            ip(1) = "   "
        End If

        If ip(2) <> "" Then
            Select Case Len(ip(2))
                Case 1
                    ip(2) = "  " & ip(2)
                Case 2
                    ip(2) = " " & ip(2)

                Case Else
            End Select
        Else
            ip(2) = "   "
        End If
        If ip(2) <> "" Then
            Select Case Len(ip(2))
                Case 1
                    ip(2) = "  " & ip(2)
                Case 2
                    ip(2) = " " & ip(2)

                Case Else
            End Select
        Else
            ip(2) = "   "
        End If

        If ip(3) <> "" Then
            Select Case Len(ip(3))
                Case 1
                    ip(3) = "  " & ip(3)
                Case 2
                    ip(3) = " " & ip(3)

                Case Else
            End Select
        Else
            ip(3) = "   "
        End If

        caller.Text = ip(0) & "." & ip(1) & "." & ip(2) & "." & ip(3)
        caller.SelectionStart = tcurse
    End Sub

    Sub SetControlsToReset()
        DoNotTalk = True
        UserReset = False
        With frmBus_IO


            Dim serialConfigData As SerialConfig = frmBus_IO.BuildCOM1ConfigData()
            ResetSerial(serialConfigData)
            serialConfigData = frmBus_IO.BuildCOM2ConfigData()
            ResetSerial(serialConfigData)
            Dim networkConfigData As NetworkConfig = frmBus_IO.BuildGigabit1ConfigData()
            ResetNetwork(networkConfigData)
            serialConfigData = frmBus_IO.BuildPCISerConfigData()
            ResetSerial(serialConfigData)
            networkConfigData = frmBus_IO.BuildGigabit2ConfigData()
            ResetNetwork(networkConfigData)
        End With
    End Sub

    Public Sub SplashEnd()
        frmAbout.Hide()
    End Sub

    Public Sub SplashStart()
        '! Load frmAbout
        frmAbout.cmdOK.Visible = False
        frmAbout.Show()
        Application.DoEvents()
    End Sub

    Function StripNonPrintChar(ByVal Parsed As String) As String
        StripNonPrintChar = ""
        'DESCRIPTION:
        '   This routine strips trailing characters with ASCII values
        '   less than 32 from the end of a string
        'PARAMTERS:
        '   Parsed$ = String from which to remove null characters
        'RETURNS:
        '   A the reultant parsed string
        Dim i As Integer

        For i = Parsed.Length To 1 Step -1
            If Asc(Mid(Parsed, i, 1)) > 32 Then
                Exit For
            End If
        Next i
        StripNonPrintChar = Strings.Left(Parsed, i)
    End Function

    Function GetCANTimingValue() As String
        GetCANTimingValue = ""
        Select Case frmBus_IO.cmbTimingValueCAN.SelectedIndex
            Case 0
                GetCANTimingValue = "20000"
            Case 1
                GetCANTimingValue = "50000"
            Case 2
                GetCANTimingValue = "100000"
            Case 3
                GetCANTimingValue = "125000"
            Case 4
                GetCANTimingValue = "250000"
            Case 5
                GetCANTimingValue = "500000"
            Case 6
                GetCANTimingValue = "1000000"

            Case Else
                GetCANTimingValue = "20000"
        End Select
    End Function

    Sub SetCANTimingValue(ByVal TimingValue As String)
        Select Case TimingValue
            Case "20"
                frmBus_IO.cmbTimingValueCAN.SelectedIndex = 0
            Case "50"
                frmBus_IO.cmbTimingValueCAN.SelectedIndex = 1
            Case "100"
                frmBus_IO.cmbTimingValueCAN.SelectedIndex = 2
            Case "125"
                frmBus_IO.cmbTimingValueCAN.SelectedIndex = 3
            Case "250"
                frmBus_IO.cmbTimingValueCAN.SelectedIndex = 4
            Case "500"
                frmBus_IO.cmbTimingValueCAN.SelectedIndex = 5
            Case "1000"
                frmBus_IO.cmbTimingValueCAN.SelectedIndex = 6

            Case Else
                frmBus_IO.cmbTimingValueCAN.SelectedIndex = 0
        End Select
    End Sub

    Private Function StrToHL(ByRef Str As String, Optional ByVal WordLength As Short = 8) As String
        Dim strHL As String = ""
        Dim strBin As String = ""
        Dim i As Integer
        Dim retStr As String = ""

        If Str.Length > 0 Then
            Dim byteData As Byte() = Encoding.ASCII.GetBytes(Str)

            For i = 0 To byteData.Length - 1
                strBin = Convert.ToString(byteData(i), 2)
                If strBin.Length > WordLength Then
                    strBin = strBin.Substring(strBin.Length - WordLength, WordLength)
                End If
                strHL = strBin.Replace("1", "H")
                strHL = strHL.Replace("0", "L")
                While strHL.Length < WordLength
                    strHL = "L" & strHL
                End While
                retStr = retStr & strHL
                If i < byteData.Length - 1 Then
                    retStr = retStr & ", "
                End If
            Next
        End If
        Return retStr
    End Function

    Private Function HLToStr(ByVal HL As String, Optional ByVal WordLength As Short = 8) As String
        Dim strChar As String = ""
        Dim strByte As Byte = 94
        Dim strBin As String = ""
        Dim i As Integer
        Dim retStr As String = ""

        If HL.Length > 0 Then
            Dim HLStrs As String() = Split(HL, ", ")

            For i = 0 To HLStrs.Length - 1
                strBin = HLStrs(i).Replace("H", "1")
                strBin = strBin.Replace("L", "0")
                While strBin.Length > WordLength
                    strBin = strBin.Remove(WordLength, 1)
                End While
                strByte = Convert.ToByte(strBin, 2)
                strChar = Convert.ToChar(strByte)
                retStr = retStr & strChar
            Next
        End If
        Return retStr
    End Function

    Public Function validateIPAddress(ByVal ipAddress As String) As String
        Dim octets As String() = {"   ", "   ", "   ", "   "}
        Dim currentOctet As Short = 0
        Dim updatedOctets As String() = {"   ", "   ", "   ", "   "}

        If ipAddress.Length > 6 And ipAddress.Contains(".") Then
            octets = ipAddress.Split(".")
            If octets.Length = 4 Then
                If octets(0).Length > 0 Then
                    If IsNumeric(octets(0)) Then
                        currentOctet = Convert.ToInt16(octets(0))
                        If currentOctet > 223 Then
                            updatedOctets(0) = "223"
                        ElseIf currentOctet < 1 Then
                            updatedOctets(0) = "1"
                        Else
                            updatedOctets(0) = currentOctet.ToString
                        End If
                    Else
                        updatedOctets(0) = "   "
                    End If
                End If
                While updatedOctets(0).Length < 3
                    updatedOctets(0) = " " + updatedOctets(0)
                End While

                If octets(1).Length > 0 Then
                    If IsNumeric(octets(1)) Then
                        currentOctet = Convert.ToInt16(octets(1))
                        updatedOctets(1) = currentOctet.ToString()
                    Else
                        updatedOctets(1) = "   "
                    End If
                End If
                While updatedOctets(1).Length < 3
                    updatedOctets(1) = " " + updatedOctets(1)
                End While

                If octets(2).Length > 0 Then
                    If IsNumeric(octets(2)) Then
                        currentOctet = Convert.ToInt16(octets(2))
                        updatedOctets(2) = currentOctet.ToString()
                    Else
                        updatedOctets(2) = "   "
                    End If
                End If
                While updatedOctets(2).Length < 3
                    updatedOctets(2) = " " + updatedOctets(2)
                End While

                If octets(3).Length > 0 Then
                    If IsNumeric(octets(3)) Then
                        currentOctet = Convert.ToInt16(octets(3))
                        updatedOctets(3) = currentOctet.ToString()
                    Else
                        updatedOctets(3) = "   "
                    End If
                End If
                While updatedOctets(3).Length < 3
                    updatedOctets(3) = " " + updatedOctets(3)
                End While
            End If
        End If

        validateIPAddress = updatedOctets(0) & "." & updatedOctets(1) & "." & updatedOctets(2) & "." & updatedOctets(3)
    End Function

    ''' <summary>
    ''' Builds a default network mask based on the local IP address and the standard network blocks (A, B, and C)
    ''' </summary>
    ''' <param name="ipAddress">Local IP address as four octet dot delimited string</param>
    ''' <returns>Standard network mask formated as four octet dot delimited string</returns>
    ''' <remarks></remarks>
    Public Function GetNetworkMask(ByVal ipAddress As String) As String
        Dim octets As String() = {"   ", "   ", "   ", "   "}
        Dim currentOctet As Short = 0
        Dim networkMask As String = "255.255.255.0"

        If ipAddress.Length > 6 And ipAddress.Contains(".") Then
            octets = ipAddress.Split(".")
            If octets(0).Trim().Length > 0 Then
                currentOctet = Convert.ToInt16(octets(0))
                If currentOctet < 128 Then
                    networkMask = "255.0.0.0"
                ElseIf currentOctet > 127 And currentOctet < 192 Then
                    networkMask = "255.255.0.0"
                ElseIf currentOctet > 191 And currentOctet < 224 Then
                    networkMask = "255.255.255.0"
                End If
            End If
        End If

        GetNetworkMask = networkMask
    End Function
End Module