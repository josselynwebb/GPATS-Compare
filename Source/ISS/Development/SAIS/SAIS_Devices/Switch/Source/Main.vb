'Option Strict Off
'Option Explicit On

Imports System
Imports System.Windows.Forms
Imports System.Windows.Forms.Screen
Imports System.Text
Imports Microsoft.VisualBasic
Imports Microsoft.Win32

Public Module SwitchMain


	'=========================================================
    '//2345678901234567890123456789012345678901234567890123456789012345678901234567890
    '////////////////////////////////////////////////////////////////////////////
    '//
    '// Virtual Instrument Portable Equipment Repair/Tester (VIPER/T) Software Module
    '//
    '// File:       Main.bas
    '//
    '// Date:       14FEB06
    '//
    '// Purpose:    SAIS: Switching Panel
    '//
    '// Instrument: Rac1260
    '//
    '//
    '// Revision History
    '// Rev      Date                  Reason                               Author
    '// =======  =======    =======================================       =================
    '// 1.1.0.0  27Oct06    Initial Release                               EADS, North America Defense
    '// 1.1.0.1  09Nov06    The following changes were made:              M.Hendricksen, EADS
    '//                     - Corrected the indexes to set the switches for
    '//                         the following tabs:
    '//                             - DCPower S101
    '//                             - 1x1 S301
    '//                             - 1x4 S401-S406
    '//                             - 2x8 S501-S510
    '//                             - 1x2 S601-S612
    '//                             - 1x8 S801-S804
    '//                             - 1x6 S901-S906
    '//                     - Added code to indicate when a "Load from Instr"
    '//                         is performed, now all switches are cleared then
    '//                         set according to what the instr. returns
    '//                     - Adjust code for switch S301 to be on one screen, no scrolling
    '//                     - Added help files for m1260-38, m1260-39 and m1260-58
    '// 1.2.0.1 20Dec06     The following changes were made:                M.Hendricksen, EADS
    '//                     - Corrected code to allow for S301 Columns 13, 16 to be closed
    '//                     - Corrected code for all tabs to be able to close more than
    '//                         one group of multiple switches.
    '// 1.2.0.2 02Jan07     Corrected indexing in SetGrid() and GetGrid()   M.Hendricksen, EADS
    '//                          and Open398SwitchDisp for Switch S301.
    '// 1.3.0.1 25Apr07     Corrected code in cmdTest/cmdExercise not to    M.Hendricksen, EADS
    '//                         to link to SysMon to check PPU.  This causes SFP
    '//                         to hang up.  Customer agreed to just ask user
    '//                         if safe to proceed.
    '// 1.3.0.2 25Apr07     Removed an extra msg box located in cmdTest     M. Hendricksen, EADS
    '// 1.6.1.1 12Mar09     Initialized bUserAbort and UserRest flags in    J. Colson, EADS
    '//                     cmdExercise and cmdExerciseGroup routines to
    '//                     prevent an abort when Exercise command is
    '//                     executed after a reset
    '******************************************************************************************************


    '----------------- VXIplug&play Driver Declarations ---------
    'Declare Function Sw_Init Lib "RI1260.DLL" Alias "ri1260_init" (ByVal rsrcName$, ByVal IDQuery%, ByVal reset%, Vi&) As Long
    Declare Function atxml_Initialize Lib "AtXmlApi.dll" (ByVal proctype As String, ByVal guid As String) As Integer

    Declare Function atxml_Close Lib "AtXmlApi.dll"  As Integer

    Declare Function atxml_ValidateRequirements Lib "AtXmlApi.dll" (ByVal TestRequirements As String, ByVal Allocation As String, ByVal Availability As String, ByVal BufferSize As Integer) As Integer

    Declare Function atxml_WriteCmds Lib "AtXmlApi.dll" (ByVal ResourceName As String, ByVal InstrumentCmds As String, ByRef ActWriteLen As Integer) As Integer

    Declare Function atxml_ReadCmds Lib "AtXmlApi.dll" (ByVal ResourceName As String, ByVal ReadBuffer As String, ByVal BufferSize As Integer, ByRef ActReadLen As Integer) As Integer

    '-----------------API / DLL Declarations------------------------------

    'JRC Added 033009
    Declare Function WritePrivateProfileString Lib "kernel32" Alias "WritePrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpString As String, ByVal lpFileName As String) As Integer
    Declare Function GetPrivateProfileString Lib "kernel32" Alias "GetPrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpDefault As String, ByVal lpReturnedString As String, ByVal nSize As Integer, ByVal lpFileName As String) As Integer

    '-----------------Init and GUID ---------------------------------------
    Public Const guid As String = "{663AA793-BE3F-4f22-AA3A-5B2365A6EEF2}"
    Public Const proctype As String = "SFP"

    ' WindowStates
    Public Const NORMAL As Short = 0 ' 0 - Normal
    Public Const MINIMIZED As Short = 1 ' 1 - Minimized
    Public Const MAXIMIZED As Short = 2 ' 2 - Maximized

    '----------------- Global Constants and Variables --------------------

    'Options Tab Index Constants
    Public Const TAB_DCPOWER As Short = 0
    Public Const TAB_2x24 As Short = 1
    Public Const TAB_1x1 As Short = 2
    Public Const TAB_1x4 As Short = 3
    Public Const TAB_2x8 As Short = 4
    Public Const TAB_1x2 As Short = 5
    Public Const TAB_1x24 As Short = 6
    Public Const TAB_1x8 As Short = 7
    Public Const TAB_1x6 As Short = 8
    Public Const TAB_OPTIONS As Short = 9
    Public Const S751 As Short = 10
    Public Const S701 As Short = 11

    Public Const INSTRUMENT_NAME As String = "Switching"
    Public Const MANUF As String = "Racal Instruments, Inc."
    Public Const MODEL_CODE As String = "1260 Series"
    Public Const RESOURCE_NAME As String = "VXI::38"
    Public Const SAIS_VERSION As String = "1.3"
    Public ResourceName As String

    Public SWITCH_TYPE(6) As String 'Combo Box Values
    Public iListNum As Short

    Structure Descriptor_38
        Dim Addr As Short
        Dim Inter As Short
        Dim mux As Short
        Dim Chan As Short
    End Structure
    Public Desc_38 As Descriptor_38


    Structure Descriptor_39
        Dim Addr As Short
        Dim Type As Short
        Dim mSelect As Short
        Dim Chan As Short
    End Structure
    Public Desc_39 As Descriptor_39

    Structure Descriptor_58_66
        Dim Addr As Short
        Dim mux As Short
        Dim Chan As Short
    End Structure
    Public Desc_58 As Descriptor_58_66
    Public Desc_66 As Descriptor_58_66

    Public LiveMode As Boolean ' Instrument Communication flag
    Public Quote As Char ' Holds Quote mark - Chr$(34)
    Public DoNotTalk As Boolean ' "Don't Talk" to instrument flag
    Public IHnd As Integer ' Instrument Address
    Public ErrorStatus As Integer ' Return status for instrument calls
    Public ReadBuffer As String ' Holds return strings from the instrument
    Public InstrumentError As Boolean ' True if WriteInstrument error query found an error
    Public UserReset As Boolean ' Used during Built-In Test to detect a user abort
    Public RfOptionInstalled As Short ' True if RF Option is installed on system
    Public PanelConifgPushed As Boolean ' This is used to tell if we had a load from instr.

    '***Read Instrument Mode***
    Public iStatus As Short
    Public gctlParse As New VIPERT_Common_Controls.Common
    '###############################################################

    Function CheckForRfOption() As Boolean
        'DESCRIPTION:
        '   This routine determines if the TETS RF option is installed
        'RETURNS:
        '   TRUE if the RF option is installed
        Dim lpFileName As String

        ReadBuffer = Space(260)
        lpFileName = Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\ATS\", "IniFilePath", Nothing)
        ErrorStatus = GetPrivateProfileString("System Startup", "RF_OPTION_INSTALLED", "NO", ReadBuffer, 256, lpFileName)
        If UCase(StripNonPrintChar(Trim(ReadBuffer))) = "YES" Then
            CheckForRfOption = True
        Else
            CheckForRfOption = False
        End If
    End Function

    Public Function DecodeErrorMessage(ByVal Msg As String) As String
        DecodeErrorMessage = ""
        Dim SourceOfError As String, ErrorNum As Short, ErrorText As String

        If Not LiveMode Then Exit Function
        If (Msg.Length < 12) Or (InStr(Msg, ".") = 0) Then
            DecodeErrorMessage = "Error decoding error message"
            Exit Function
        End If

        SourceOfError = Mid(Msg, InStr(Msg, ".") - 3, 3)
        Select Case SourceOfError
            Case "XXX"
                SourceOfError = ""
            Case "000"
                SourceOfError = "1260 Switch Controller"
            Case "001"
                SourceOfError = "1260-39 LF-1 1-Wire Switch Module"
            Case "002"
                SourceOfError = "1260-39 LF-2 1-Wire Switch Module"
            Case "003"
                SourceOfError = "1260-38T LF-3 2-Wire Switch Module"
            Case "004"
                SourceOfError = "1260-58 MF Coaxial Switch Module"
            Case "005"
                SourceOfError = "1260-66A HF Coaxial Switch Module"

            Case Else
                SourceOfError = "Unknown Switch Module (Module Address: " & SourceOfError & ")"
        End Select

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

    Public Sub Delay(ByVal Seconds As Single)
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

        System.Threading.Thread.Sleep(CLng(Seconds * 1000.0!))
    End Sub

    Sub DisplayErrorMessage(ByVal ErrorStatus As Integer)
        If ErrorStatus < 0 Then
            '        Error = Sw_Error_Message(IHnd&, ErrorStatus&, ReadBuffer$)
            '        MsgBox ReadBuffer$, 48, "VISA Error Message"
        End If
    End Sub

    Sub DoIt(ByVal ErrorStatus As Integer)
        If Not LiveMode Then Exit Sub

        If ErrorStatus Then
            DisplayErrorMessage(ErrorStatus)
        End If

    End Sub

    Function GetDesc_38() As String
        GetDesc_38 = ""
        GetDesc_38 = Str(Desc_38.Addr) & "." & Desc_38.Inter & Desc_38.mux.ToString("00") & Desc_38.Chan
    End Function

    Function GetDesc_39() As String
        GetDesc_39 = ""
        GetDesc_39 = Desc_39.Addr.ToString() & "." & Desc_39.Type & Desc_39.mSelect & Desc_39.Chan.ToString("00")
    End Function

    Function GetDesc_58() As String
        GetDesc_58 = ""
        GetDesc_58 = Desc_58.Addr.ToString() & "." & Desc_58.mux & Desc_58.Chan
    End Function

    Function GetDesc_66() As String
        GetDesc_66 = ""
        GetDesc_66 = Desc_66.Addr.ToString() & "." & Desc_66.mux & Desc_66.Chan
    End Function

    Public Sub HelpPanel(ByVal Text As String)
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

        '    If Screen.ActiveForm.Name = "frmAbout" Then Exit Sub

        '    If Screen.ActiveForm.sbrUserInformation.SimpleText <> Text$ Then
        '        Screen.ActiveForm.sbrUserInformation.SimpleText = Text$
        '    End If

        If frmAbout.Visible Then Exit Sub

        If frmRac1260.sbrUserInformation.Text <> Text Then
            frmRac1260.sbrUserInformation.Text = Text
        End If
    End Sub

    Sub Initialize()
        Dim i As Short
        Dim j As Short
        Dim PinData As String = ""
        Dim RowData() As String
        Dim iLoop As Short
        Dim Reader As System.IO.StreamReader

        With frmRac1260
            '*** "DC Power" Tab
            For i = 0 To .saS101.Count - 1
                .saS101.Switches(i).Pin1Text = ((i * 4) + 1).ToString()
                .saS101.Switches(i).Pin2Text = ((i * 4) + 2).ToString()
                .saS101.Switches(i).Pin3Text = ((i * 4) + 3).ToString()
                .saS101.Switches(i).Pin4Text = ((i * 4) + 4).ToString()
            Next

            '*** "2x24 2w LF" Tab
            For j = 0 To .saS201.Count - 1
                .saS201.Switches(j).Pin1Text = ((j * 2) + 5).ToString()
                .saS201.Switches(j).Pin2Text = ((j * 2) + 6).ToString()
            Next
            For j = 0 To .saS202.Count - 1
                .saS202.Switches(j).Pin1Text = ((j * 2) + 5).ToString()
                .saS202.Switches(j).Pin2Text = ((j * 2) + 6).ToString()
            Next

            '*** "1x1 1w LF" Tab
            .tabOptions.SelectedIndex = 2
            For i = 0 To .saS301.Count - 1
                .saS301.Switches(i).Pin1Text = ((i * 2) + 1).ToString()
                .saS301.Switches(i).Pin2Text = ((i * 2) + 2).ToString()
            Next

            '*** "1x4 1w LF" Tab
            For i = 0 To .saS401.Count - 1
                .saS401.Switches(i).Pin1Text = (i + 2).ToString()
            Next
            For i = 0 To .saS402.Count - 1
                .saS402.Switches(i).Pin1Text = (i + 2).ToString()
            Next
            For i = 0 To .saS403.Count - 1
                .saS403.Switches(i).Pin1Text = (i + 2).ToString()
            Next
            For i = 0 To .saS404.Count - 1
                .saS404.Switches(i).Pin1Text = (i + 2).ToString()
            Next
            For i = 0 To .saS405.Count - 1
                .saS405.Switches(i).Pin1Text = (i + 2).ToString()
            Next
            For i = 0 To .saS406.Count - 1
                .saS406.Switches(i).Pin1Text = (i + 2).ToString()
            Next

            '*** "2x8 1w LF" Tab
            For i = 0 To .saS501.Count - 1
                .saS501.Switches(i).Pin1Text = i + 3
            Next
            For i = 0 To .saS502.Count - 1
                .saS502.Switches(i).Pin1Text = i + 3
            Next
            For i = 0 To .saS503.Count - 1
                .saS503.Switches(i).Pin1Text = i + 3
            Next
            For i = 0 To .saS504.Count - 1
                .saS504.Switches(i).Pin1Text = i + 3
            Next
            For i = 0 To .saS505.Count - 1
                .saS505.Switches(i).Pin1Text = i + 3
            Next
            For i = 0 To .saS506.Count - 1
                .saS506.Switches(i).Pin1Text = i + 3
            Next
            For i = 0 To .saS507.Count - 1
                .saS507.Switches(i).Pin1Text = i + 3
            Next
            For i = 0 To .saS508.Count - 1
                .saS508.Switches(i).Pin1Text = i + 3
            Next
            For i = 0 To .saS509.Count - 1
                .saS509.Switches(i).Pin1Text = i + 3
            Next
            For i = 0 To .saS510.Count - 1
                .saS510.Switches(i).Pin1Text = i + 3
            Next

            '*** "1x2 1w LF" Tab
            For i = 0 To .saS601.Count - 1
                .saS601.Switches(i).Pin1Text = (i + 2).ToString()
            Next

            For i = 0 To .saS602.Count - 1
                .saS602.Switches(i).Pin1Text = (i + 2).ToString()
            Next

            For i = 0 To .saS603.Count - 1
                .saS603.Switches(i).Pin1Text = (i + 2).ToString()
            Next

            For i = 0 To .saS604.Count - 1
                .saS604.Switches(i).Pin1Text = (i + 2).ToString()
            Next

            For i = 0 To .saS605.Count - 1
                .saS605.Switches(i).Pin1Text = (i + 2).ToString()
            Next

            For i = 0 To .saS606.Count - 1
                .saS606.Switches(i).Pin1Text = (i + 2).ToString()
            Next

            For i = 0 To .saS607.Count - 1
                .saS607.Switches(i).Pin1Text = (i + 2).ToString()
            Next

            For i = 0 To .saS608.Count - 1
                .saS608.Switches(i).Pin1Text = (i + 2).ToString()
            Next

            For i = 0 To .saS609.Count - 1
                .saS609.Switches(i).Pin1Text = (i + 2).ToString()
            Next

            For i = 0 To .saS610.Count - 1
                .saS610.Switches(i).Pin1Text = (i + 2).ToString()
            Next

            For i = 0 To .saS611.Count - 1
                .saS611.Switches(i).Pin1Text = (i + 2).ToString()
            Next

            For i = 0 To .saS612.Count - 1
                .saS612.Switches(i).Pin1Text = (i + 2).ToString()
            Next

            '*** "1x24 2w LF" Tab
            For i = 0 To .saS701.Count - 1
                .saS701.Switches(i).Pin1Text = ((i * 2) + 3).ToString()
                .saS701.Switches(i).Pin2Text = ((i * 2) + 4).ToString()
            Next

            '*** "1x8 2w LF" Tab
            For i = 0 To .saS751.Count - 1
                .saS751.Switches(i).Pin1Text = ((i * 2) + 3).ToString()
                .saS751.Switches(i).Pin2Text = ((i * 2) + 4).ToString()
            Next

            '*** "1x8 Coax MF" Tab

            '*** "1x6 Coax HF" Tab
            If Not RfOptionInstalled Then
                .tabOptions.Controls.RemoveByKey("tabOptions_Page9")
            End If

            '*** "Options" Tab
            'ADVINT'S SUGGESTION:
            'Open App.Path & "\SORTSIG.TXT" For Input As #FileNum%
            'Open "\TETS\Config\SORTSIG.TXT" For Input As #FileNum%
            Try
                Dim sortSigFile As String = Application.StartupPath + "\..\Config\SORTSIG.TXT"
                Reader = New IO.StreamReader(sortSigFile)
                .grdSortSig.Font = New Font("Courier New", 8)
                While Not Reader.EndOfStream

                    PinData = Reader.ReadLine()
                    RowData = PinData.Split(vbTab)
                    .grdSortSig.Rows.Add(RowData)
                End While
            Catch ex As SystemException
                MsgBox("Cannot access file SORTSIG.TXT.  Pin List information sorted by Switch Pin will not be available. The following exception was caught:" + vbCrLf + ex.Message, MsgBoxStyle.Exclamation)
                .panSortSig.Visible = False
            Finally
                Reader.Close()
            End Try

            'ADVINT SUGGESTION:
            '       Open App.Path & "\SORTCNX.TXT" For Input As #FileNum%
            'Open "\TETS\Config\SORTCNX.TXT" For Input As #FileNum%
            Try
                Dim sortCnxFile As String = Application.StartupPath + "\..\Config\SORTCNX.TXT"

                Reader = New IO.StreamReader(sortCnxFile)
                .grdSortCnx.Font = New Font("Courier New", 8)

                While Not Reader.EndOfStream
                    PinData = Reader.ReadLine()
                    RowData = PinData.Split(vbTab)
                    .grdSortCnx.Rows.Add(RowData)
                End While
            Catch ex As SystemException
                MsgBox("Cannot access file SORTCNX.TXT.  Pin List information sorted by Receiver Pin will not be available. The following exception was caught:" + vbCrLf + ex.Message, MsgBoxStyle.Exclamation)
                .panSortCnx.Visible = False
            Finally
                Reader.Close()
            End Try

            'Populate the CardType ComboBox
            'Empty ComboBox
            .cboCardType.Items.Clear()

            'Fill With Specified Number of array elements necessary to Instrument Function
            For iLoop = 0 To iListNum - 1
                .cboCardType.Items.Add(SWITCH_TYPE(iLoop))
            Next iLoop

            .cboCardType.SelectedIndex = 0

            .tabOptions.SelectedIndex = 0
        End With

        SetControlsToReset()

        frmRac1260.Show()
    End Sub

    Function Limit(ByVal CheckValue As Short, ByVal LimitValue As Short) As Short
        If CheckValue < LimitValue Then
            Limit = LimitValue
        Else
            Limit = CheckValue
        End If
    End Function

    Public Sub Main()
        Dim SystemDirectory As String
        Dim S As String
        Dim SwInfo As String = ""
        Const conNoDLL As Short = 48
        'WMT (START):
        Dim Status As Integer
        Dim XmlBuf As String
        Dim Allocation As String
        Dim response As String

        Try
            XmlBuf = Space(4096)
            response = Space(4096)
            ' initialize resource name
            ResourceName = "PAWS_SWITCH"

            'indicate panel_conifg is not used currently
            PanelConifgPushed = False

            'WMT (END)

            SplashStart()
            Quote = Chr(34)
            DoNotTalk = True
            RfOptionInstalled = True

            'Check System and Instrument For Errors
            SystemDirectory = Environment.SystemDirectory

            'Determine if the instrument is functioning
            '            WMT:
            '            ErrorStatus& = Sw_Init(rsrcName:=RESOURCE_NAME$, IDQuery:=1, reset:=1, Vi:=IHnd&)
            '            WMT (START):
            Status = atxml_Initialize(proctype, guid)
            XmlBuf = "<AtXmlTestRequirements> " & _
                        "    <ResourceRequirement> " & _
                        "       <ResourceType>Source</ResourceType> " & _
                        "       <SignalResourceName>PAWS_SWITCH</SignalResourceName> " & _
                        "    </ResourceRequirement> " & _
                        "</AtXmlTestRequirements>"

            Allocation = Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\ATS\", "PAWSAllocationPath", Nothing)

            Status = atxml_ValidateRequirements(XmlBuf, Allocation, response, 4096)

            'Parse Availability XML string to get the status(Mode) of the instrument
            'Since the resource we use for the switch is defined as a virtual instument, this call will never show
            'the instrument in use.  We will use switch states during startup to set this status.  If any
            'switch is closed, we will consider the resourse InUse and go into debug mode.
            iStatus = gctlParse.ParseAvailiability(response)

            '            WMT (END)
            If Err.Number = conNoDLL Then
                MsgBox("Error in loading Ri1260.DLL.  Live mode is disabled.", MsgBoxStyle.Information)
                LiveMode = False
            ElseIf ErrorStatus Then
                MsgBox("The Switch is not responding.  Live mode is disabled.", MsgBoxStyle.Exclamation)
                LiveMode = False
            Else
                LiveMode = True
                'RfOptionInstalled = CheckForRfOption()
                RfOptionInstalled = True 'setting always to true, non rf stations still have a HF switch
            End If
            Delay(0.5)

            DoNotTalk = False

            SWITCH_TYPE(0) = "------ALL Cards------"
            iListNum = 1
            'Check for switch modules at the correct module address
            If LiveMode = True And DoNotTalk = False Then
                '        DoIt Sw_Reset(IHnd)
                With frmRac1260
                    WriteInstrument("PDATAOUT 0-5")
                    Do
                        S = ReadSwitch()
                        SwInfo &= S
                    Loop Until InStr(S, "END") Or S = ""
                    If InStr(SwInfo, "001. 1260-39") = 0 Then
                        MsgBox("Did not find a 1260-39 switch module at switch module address 1." & vbCrLf & "The following switches are not available:" & vbCrLf & "Lower half of S101 and S301, S401-S403, S501-S505, S601-S606.", MsgBoxStyle.Exclamation)
                    Else
                        SWITCH_TYPE(iListNum) = "(1) 1260-39 Card"
                        iListNum += 1
                    End If
                    If InStr(SwInfo, "002. 1260-39") = 0 Then
                        MsgBox("Did not find a 1260-39 switch module at switch module address 2." & vbCrLf & "The following switches are not available:" & vbCrLf & "Upper half of S101 and S301, S404-S406, S506-S510, S607-S612.", MsgBoxStyle.Exclamation)
                    Else
                        SWITCH_TYPE(iListNum) = "(2) 1260-39 Card"
                        iListNum += 1
                    End If
                    If InStr(SwInfo, "003. 1260-38A") = 0 Then
                        MsgBox("Did not find a 1260-38T switch module at switch module address 3." & vbCrLf & "The following switches are not available:" & vbCrLf & "S201-S202, S701, S751.", MsgBoxStyle.Exclamation)
                        .tabOptions.TabPages.Item(TAB_2x24).Enabled = False
                        .tabOptions.TabPages.Item(TAB_1x24).Enabled = False
                    Else
                        SWITCH_TYPE(iListNum) = "(3) 1260-38T Card"
                        iListNum += 1
                    End If
                    If InStr(SwInfo, "004. 1260-58") = 0 Then
                        MsgBox("Did not find a 1260-58 switch module at switch module address 4." & vbCrLf & "The following switches are not available:" & vbCrLf & "S801-S804.", MsgBoxStyle.Exclamation)
                        .tabOptions.TabPages.Item(TAB_1x8).Enabled = False
                    Else
                        SWITCH_TYPE(iListNum) = "(4) 1260-58 Card"
                        iListNum += 1
                    End If
                    If RfOptionInstalled And (InStr(SwInfo, "005. 1260-66A") = 0) Then
                        MsgBox("Did not find a 1260-66A switch module at switch module address 5." & vbCrLf & "The following switches are not available:" & vbCrLf & "S901-S906.", MsgBoxStyle.Exclamation)
                        .tabOptions.TabPages.Item(TAB_1x6).Enabled = False
                    ElseIf RfOptionInstalled Then
                        SWITCH_TYPE(iListNum) = "(5) 1260-66A Card"
                        iListNum += 1
                    Else
                        .tabOptions.TabPages.Item(TAB_1x6).Enabled = False
                    End If
                End With
            End If

            Application.DoEvents()
            frmRac1260.Cursor = Cursors.AppStarting
            Initialize()
            SplashEnd()
            frmRac1260.tabOptions.SelectedIndex = TAB_DCPOWER 'JHill 1.2

            'Set the Refresh rate when in "Debug" mode from 1000 = 1 sec to 25000 = 25 sec
            frmRac1260.Panel_Conifg.Refresh = 5000

            'Get current information from instrument
            Application.DoEvents()
            frmRac1260.ConfigGetCurrent()

            'DO NOT Put a break point After this point. Causes error if timer is running
            'Set the Mode the Instrument is in: Local = True, Debug = False
            If iStatus <> 1 Then
                frmRac1260.Panel_Conifg.SetDebugStatus(True)
            Else
                frmRac1260.Panel_Conifg.SetDebugStatus(False)
            End If
            frmRac1260.Cursor = Cursors.Default

        Catch ex As Exception
            MessageBox.Show("Exception caught in Main: " & ex.Message)
        End Try
    End Sub

    Sub CenterForm(ByVal Form As Object)
        'DESCRIPTION:
        '   This Module Centers One Form With Respect To The
        '   User's Screen.
        'EXAMPLE:
        '   CenterForm frmRi4152A

        Form.Top = PrimaryScreen.Bounds.Height / 2 - Form.Height / 2
        Form.Left = PrimaryScreen.Bounds.Width / 2 - Form.Width / 2
    End Sub

    Function ReadSwitch() As String
        ReadSwitch = ""
        Dim CharsRead As Integer
        Dim ReadString As String = ""
        Dim Status As Integer

        If LiveMode Then
            If DoNotTalk Then Exit Function

            Do
                '           ErrorStatus& = viRead(IHnd, ReadBuffer$, 255&, CharsRead&)
                '           ErrorStatus& = atxml_ReadCmds(ResourceName,
                ''                                          ReadBuffer,
                ''                                          numberBytesToRead,
                ''                                          NBR)
                '           Stop
                ReadBuffer = Space(260)
                Status = atxml_ReadCmds(ResourceName, ReadBuffer, 255, CharsRead)

                ReadString &= Strings.Left(ReadBuffer, CharsRead)
            Loop While CharsRead = 255
            '       If ErrorStatus& = VI_SUCCESS_TERM_CHAR Then 'Strip the CR-LF
            '        ReadString = Left$(ReadString, Len(ReadString) - 2)
            '       End If
        End If

        If ReadString = "" Then
            '##########################################
            'Action Required: Remove debug code
            'This will enable simulation of commands
            'If gcolCmds.count <> 0 Then
            'Dim i As Integer
            'For i = 1 To gcolCmds.count
            '    ReadString = ReadString & gcolCmds.Item(i) & vbCrLf
            'Next i''
            '
            'HelpPanel "Simulated response for: " & gcolCmds.Item(1) & Quote$
            'DoEvents
            '
            ''Clear collection
            'Set gcolCmds = New Collection
            'End If
            '##########################################
        End If

        ReadString = StripNonPrintChar(ReadString)
        DisplayErrorMessage(Status)
        ReadSwitch = ""
        ReadSwitch = ReadString
    End Function

    Sub SetControlsToReset()
        DoNotTalk = True
        UserReset = False
        With frmRac1260
            '*** Main Form Settings
            .sbrUserInformation.Panels(0).Text = "Stopped"

            '*** "DC Power" Tab
            .cmdS101Reset_Click()

            '*** "2x24 2w LF" Tab
            If .tabOptions.TabPages.Item(TAB_2x24).Enabled = True Then
                .cmdS200ResetAll_Click()
            End If

            '*** "1x1 1w LF" Tab
            .cmdS301ResetAll_Click()

            '*** "1x4 1w LF" Tab
            .cmdS400ResetAll_Click()

            '*** "2x8 1w LF" Tab
            .cmdS500ResetAll_Click()

            '*** "1x2 1w LF" Tab
            .cmdS600ResetAll_Click()

            '*** "1x24 2w LF" Tab
            If .tabOptions.TabPages.Item(TAB_1x24).Enabled = True Then
                .cmdS700ResetAll_Click()
            End If

            '*** "1x8 Coax MF" Tab
            If .tabOptions.TabPages.Item(TAB_1x8).Enabled = True Then
                .cmdS800ResetAll_Click()
            End If

            '*** "1x6 Coax HF" Tab
            If RfOptionInstalled And .tabOptions.TabPages.Item(TAB_1x6).Enabled = True Then
                .cmdS900ResetAll_Click()
            End If

            DoNotTalk = False

            'Set the necessary -38 interconnect relays
            If .tabOptions.TabPages.Item(TAB_2x24).Enabled = True Then
                WriteInstrument("CLOSE 3.1001,2002,3000,3001,4000,4001,6000,6001,7000,7001")
            End If
        End With
    End Sub

    Public Sub SplashEnd()
        frmAbout.Hide()
        frmAbout.Cursor = Cursors.Default
    End Sub

    Public Sub SplashStart()
        '! Load frmAbout
        frmAbout.cmdOK.Visible = False
        frmAbout.Show()
        frmAbout.Cursor = Cursors.AppStarting
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
        'Reqiires (3) Windows Api Functions
        'Declare Function GetPrivateProfileString Lib "kernel32" Alias "GetPrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As Any, ByVal lpDefault As String, ByVal lpReturnedString As String, ByVal nSize As Long, ByVal lpFileName As String) As Long
        'Declare Function WritePrivateProfileString Lib "kernel32" Alias "WritePrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As Any, ByVal lpString As Any, ByVal lpFileName As String) As Long

        Dim lpReturnedString As String 'Return Buffer
        Dim nSize As Integer 'Return Buffer Size
        Dim lpFileName As String 'INI File Key Name "Key=?"
        Dim ReturnValue As Integer 'Return Value Buffer
        Dim FileNameInfo As String 'Formatted Return String
        Dim lpString As String 'String to write to INI File

        'Get the ini file location from the Registry
        lpFileName = Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\ATS\", "IniFilePath", Nothing)
        nSize = 255
        'Clear String Buffer
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

    Sub WriteInstrument(ByVal Command As String)
        'Dim count As Integer
        'Dim ErrorFlag As Integer
        Dim Status As Integer

        If DoNotTalk Then Exit Sub
        If Command = "" Then Exit Sub
        If Not LiveMode Then
            HelpPanel("Instrument Command: " & Quote & Command & Quote)
            Exit Sub
        End If

        'Check mode in
        If frmRac1260.Panel_Conifg.DebugMode = True And Command.Substring(0, 2) <> "PD" Then Exit Sub

        '  DoIt viWrite(IHnd&, Command$, CLng(Len(Command$)), count)
        '  Status = atxml_WriteCmds(ResourceName$, Command$, iActWtiteLen)
        Status = atxml_WriteCmds(ResourceName, Command, Command.Length)

        Select Case UCase(Strings.Left(Command, 2))
            Case "TE", "YE", "PD", "PS"
            Case "RE"
                If UCase(Strings.Left(Command, 3)) = "RES" Then
                    Delay(0.1)
                End If

            Case Else
                InstrumentError = False
                '        Do
                '            ReadBuffer$ = ""
                '            ErrorStatus& = Sw_Error_Query(IHnd&, ErrorFlag, ReadBuffer)
                '            If ErrorFlag = 0 Then Exit Do
                '            InstrumentError% = True
                '            MsgBox DecodeErrorMessage(ReadBuffer$), 48, "Switch Error Message"
                '        Loop
        End Select
    End Sub
End Module