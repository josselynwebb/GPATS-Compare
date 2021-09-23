'Option Strict Off
'Option Explicit On

Imports System
Imports System.Windows.Forms
Imports System.Windows.Forms.Screen
Imports System.Text
Imports System.Drawing
Imports System.Math
Imports Microsoft.VisualBasic
Imports Microsoft.Win32

Public Module DcpMain


	'=========================================================
    '//2345678901234567890123456789012345678901234567890123456789012345678901234567890
    '///////////////////////////////////////////////////////////////////////////////////////////////////////
    '//
    '// Virtual Instrument Portable Equipment Repair/Tester (VIPER/T) Software Module
    '//
    '// File:       Main.bas
    '//
    '// Date:       27OCT06
    '//
    '// Purpose:    SAIS: Power Distribution Unit Front Panel
    '//
    '// Instrument: Freedom01502 Power Distribution Unit (Pdu)
    '//
    '//
    '// Revision History
    '// Rev      Date                  Reason                                       Author
    '// =======  =======    =======================================                 ========================
    '// 1.1.0.0  27OCT06    Initial Release                                         EADS, North America Defense
    '// 1.1.0.1  16NOV06    Following Changes were made:                            M.Hendricksen, EADS
    '//                     - Enabled the "Show In Taskbar" property of the SFP
    '//                     - Changed Logo
    '//                     - Added Sub Exit_App to close the program in task mangaer when
    '//                         Windows 'X' button is used.
    '//                     - Removed code that converted null characters to 'ef', because
    '//                         cicl now handles it.
    '// 1.3.0.1  16Apr07    Added Delay of 5 in function CmdSelfTestAll             M.Hendricksen, EADS
    '//                         to allow time for instruement to run test before
    '//                         quering results.
    '// 1.3.0.2  18Apr07    Corrected Array index in function CmdSelfTestAll        M.Hendricksen, EADS
    '// 1.3.0.3  21Apr07    Corrected code to input programmed value under          M.Hendricksen, EADS
    '//                         each supply and not the measured value.
    '// 1.3.0.4  30Apr07    Corrected code not to send a command over when          M.Hendricksen, EADS
    '//                         sfp is starting up.
    '// 1.3.0.5  02May07    Corrected code to initializes structure to defaults,    M.Hendricksen, EADS
    '//                         and to set the structure variables depending on
    '//                         what the instrument sends back from queries.  Added
    '//                         MeasVolt and MeasCurr to the structure to use to display
    '//                         the measured characterics from instr.
    '// 1.3.0.6 02May07     Corrected code to go into Monitor mode if in use        M.Hendricksen, EADS
    '//                     Corrected code to display measurement under each PPU
    '//                         if there were not a programmed voltage.
    '//                     Corrected code to update each ppu when the SFP comes up
    '// 1.3.0.7 03May07     Corrected code not to reset instr or SFP parameters     M.Hendricksen, EADS
    '//                         while in Monitor Mode.  Also added messages to
    '//                         status bar on query and reset conditions.  Took out
    '//                         setting the mouse to a hourglass figure.
    '// 1.3.0.8 07May07     Corrected code not to call DoEvents in Monitor mode     M.Hendricksen, EADS
    '//                         because if somehow (processing last key stroke) keeps
    '//                         the sfp in monitor mode even though it should be out.
    '// 1.3.0.9 08May07     Correct code in SendGpibCommand to not send any         M.Hendricksen, EADS
    '//                         commands out other than query cmds.
    '//                     Removed delays throughout and put a delay after cmd
    '//                         sent in SendGpibCommand().
    '//                     Corrected code to set the lblMeas strip during measurments
    '//                         and not set the Programmed voltage box with measurments.
    '////////////////////////////////////////////////////////////////////////////////////////////////////////
    '

    '-----------------API / DLL Declarations------..\Build\------------------------

    Declare Function atxml_Initialize Lib "AtXmlApi.dll" (ByVal ProcType As String, ByVal ProcUuid As String) As Short

    Declare Function atxml_Close Lib "AtXmlApi.dll"  As Integer

    Declare Function atxml_ValidateRequirements Lib "AtXmlApi.dll" (ByVal TestRequirements As String, ByVal Allocation As String, ByVal Availability As String, ByVal BufferSize As Short) As Integer

    Declare Function atxml_IssueDriverFunctionCall Lib "AtXmlApi.dll" (ByVal XmlBuffer As String, ByVal Response As String, ByVal BufferSize As Short) As Integer

    Declare Function atxmlDF_viWrite Lib "AtxmlDriverFunc.DLL" (ByVal resourceName As String, ByVal instrumentHandle As Short, ByVal InstrumentCmds As String, ByVal BufferSize As Integer, ByRef ActWriteLen As Integer) As Integer

    Declare Function atxmlDF_viRead Lib "AtxmlDriverFunc.DLL" (ByVal resourceName As String, ByVal instrumentHandle As Short, ByVal ReadBuffer As String, ByVal BufferSize As Integer, ByRef ActReadLen As Integer) As Integer

    Declare Function GetPrivateProfileString Lib "kernel32" Alias "GetPrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpDefault As String, ByVal lpReturnedString As String, ByVal nSize As Integer, ByVal lpFileName As String) As Integer
    Declare Function WritePrivateProfileString Lib "kernel32" Alias "WritePrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpString As String, ByVal lpFileName As String) As Integer

    '-----------------Init and GUID ---------------------------------------
    Public Const guid As String = "{E67DEB0B-75DE-43db-828D-2B018F4A4FB9}"
    Public Const ProcType As String = "SFP"

    '-----------------User Defined Types----------------------------------

    Structure PowerSupplyData 'Default Values
        Dim FuncVoltVal As String '"00.00"
        Dim FuncAmpVal As String '"0.076"
        Dim SensLocal As Short 'True
        Dim FuncRelayClosed As Short 'False
        Dim Master As Short 'True
        Dim CurProt As Short 'True (voltage regulation mode)
        Dim RevPol As Short 'False
        'The following are added to track current settings in ASCII values for TIPs
        Dim sCmdVolt As String 'Volts Setting
        Dim sCmdAmp As String 'Amps Setting
        Dim sCmdOpt As String 'Options Setting (Output, Polarity, Remote, Slave, CC)
        Dim MeasVolt As String '"00.00" Measured voltage from instrument query
        Dim MeasCurr As String '"00.00" Measured string from instrument query
    End Structure

    '-----------------Global Arrays---------------------------------------
    Public SupplyData(10) As PowerSupplyData
    '-----------------Global Constants------------------------------------
    Public Q As String 'Utility ASCII Quote (") Symbol
    Public Const VI_SUCCESS As Short = &H0
    Public Const VI_SUCCESS_DEV_NPRESENT As Integer = &H3FFF007D
    '-----------------Delay Constants-------------------------------------
    Const SECS_IN_DAY As Integer = 86400
    '-----------------About Box Constants---------------------------------
    Public Const INSTRUMENT_NAME As String = "Programmable Power Unit"
    Public Const MANUF As String = "Freedom Power Systems"
    Public Const MODEL_CODE As String = "01-502-0001"
    Public Const RESOURCE_NAME As String = "GPIB0::5::1-10"
    Public Const VI_ATTR_TMO_VALUE As Integer = &H3FFF001A
    '-----------------Global Variables------------------------------------
    Public LiveMode As Boolean 'Enable/Disable Live Instrument Communication
    'mh Global instrumentMode%
    Public ErrorStatus As Integer
    Public resourceName As String
    Public ReadBuffer As String
    Public EditSupply As Short
    '---- The following are to support TIPs
    Dim sWindowsDir As String 'Path of Windows Directory
    Dim lpString As String
    Public sTipCmds As String
    Public sTipMode As String
    Public instrumentHandle As Short
    Public instrumentHandle2 As Integer
    Dim sMeasVal As String
    Dim nNumChars As Integer
    Dim sCommandLine As String
    Dim bytModeByte As Byte
    Dim bytStatusByte As Byte
    Public bLoadingTipCmds As Boolean
    Public bInstrumentMode As Boolean 'mh
    '-----------------Global Arrays---------------------------------------
    'Global iHandle&(1 To 10)
    Public SignalResourceNameArray(10) As String
    '-----------------Input Text Box / Spin Button Instruction -----------
    Public Const MAX_SETTINGS As Short = 10
    Public SetCurVolt(MAX_SETTINGS) As String ' "Voltage Settings" Array
    Public SetCurCurr(MAX_SETTINGS) As String ' "Current Settings" Array
    Public SetDef(2) As String ' "Default Settings" Array
    Public SetMinVolt(MAX_SETTINGS) As String ' "Minimum Voltage Settings" Array
    Public SetMinCurr(MAX_SETTINGS) As String ' "Minimum Current Settings" Array
    Public SetMaxVolt(MAX_SETTINGS) As String ' "Maximum Voltage Settings" Array
    Public SetMaxCurr(MAX_SETTINGS) As String ' "Maximum Current Settings" Array
    Public SetIncVolt(MAX_SETTINGS) As String ' "VoltageIncrements" Array
    Public SetIncCurr(MAX_SETTINGS) As String ' "Current Increments" Array
    Public SetRes(2) As String ' "Resolution" Array
    Public SetRngMsgVolt(MAX_SETTINGS) As String ' "Voltage Range Message" for Status Bar Array
    Public SetRngMsgCurr(MAX_SETTINGS) As String ' "CurrentRange Message" for Status Bar Array
    Public SetMinInc(MAX_SETTINGS) As String ' "Increments" Array
    Public Const VOLTAGE As Short = 1
    Public Const CURRENT As Short = 2
    Public EnableCommunication As Boolean
    Public NewSN As String
    Public ResetAllSequence As String

    Const INFO As MsgBoxStyle = MsgBoxStyle.Information
    Const EXPLANATION As MsgBoxStyle = MsgBoxStyle.Exclamation

    '***Read Instrument Mode***
    Public iStatus As Short
    Public gctlParse As New VIPERT_Common_Controls.Common
    '###############################################################

    Private Function bActivateControl(ByVal sCmd As String) As Boolean
        'DESCRIPTION:
        '   Sets a panel control to the state indicated by the command. It simulates the action
        '   of a user selecting the control including sending the actual commands to the DCPS.
        'PARAMETERS:
        '   sCmd:  An 8-byte string in ASCII-fied HEX format representing 3 bytes seperated
        '           by commas,
        '           OR a message command in the format "Mtn" for measurement TIP step, where
        '                   t = "V" for volts or "A" for amps, and
        '                   n = the DCPS number in decimal
        'RETURNS:
        '   True if no error, else False

        Dim i As Short
        Dim sErrMsg As String = ""
        Dim sList() As String = {""}
        Dim fVolts As Single
        Dim fAmps As Single
        Dim bytB1 As Byte
        Dim bytB2 As Byte
        Dim bytB3 As Byte
        Dim sMeasType As String

        With frmAPS6062
            Try
                bActivateControl = True

                If Len(sCmd) <= 1 Then Exit Function

                'Check for Measurement command
                If Strings.Left(sCmd, 1) = "M" Then
                    i = Val(Mid(sCmd, 3))
                    If (i < 0) Or (i > 10) Then
                        sErrMsg = "Invalid Supply Number: " & CStr(i) & " in command: " & sCmd
                        Throw (New SystemException(""))
                    Else
                        EditSupply = i
                    End If
                    .tbrUutPuFunctions.Buttons(EditSupply - 1).Pushed = True
                    sMeasType = Mid(sCmd, 2, 1)
                    If sMeasType <> "V" And sMeasType <> "A" Then
                        sErrMsg = "Invalid Measurement Command Type: " & sCmd
                        Throw (New SystemException(""))
                    End If
                    frmAPS6062.tabUserOptions.SelectedIndex = 1 'Measure
                    If sTipMode = "TIP_RunPersist" Then
                        .chkContinuous.Checked = True
                    End If
                    CommandMeasure(EditSupply, sMeasType)
                    Exit Function
                End If

                'Check for a Reset state.
                'For a supply that is being used in one step, and is being set to all default states
                'in the next step, it's important to do this with the Reset command rather than
                'setting each parameter to it's default state in the order of commands (current,
                'relays, volts). This order was chosen to prevent hot-switching when supplies are
                'turned on. However, when turing off, the first setting for a supply is curr-limit
                'to default (75mA). This may cause the supply, which is probably connected to a UUT,
                'to trip and generate an over_current error before the next command, which opens the
                'output relay, gets executed.
                If UCase(Strings.Left(sCmd, 6)) = "RESET_" Then
                    'Check range
                    i = Val(Mid(sCmd, 7))
                    If (i < LBound(SupplyData)) Or (i > UBound(SupplyData)) Then
                        sErrMsg = "Invalid Supply Number: " & CStr(i) & " in command: " & sCmd
                        Throw (New SystemException(""))
                    End If
                    'But execute it only if not already in a reset state
                    If (Mid(SupplyData(i).sCmdAmp, 4) <> "40,26") Or (Mid(SupplyData(i).sCmdOpt, 4) <> "AA,22") Or (Mid(SupplyData(i).sCmdVolt, 4) <> "50,00") Then
                        ResetPowerSupply(i)
                    End If
                    Exit Function
                End If

                'Do some validating
                If Len(sCmd) <> 8 Then
                    sErrMsg = "Invalid Command String Size: " & sCmd
                    Throw (New SystemException(""))
                End If

                'Check if the command matches a current setting.
                'If so, save time by not sending it. This will usually be the case.
                For i = 1 To 10
                    If (sCmd = SupplyData(i).sCmdAmp) Or (sCmd = SupplyData(i).sCmdOpt) Or (sCmd = SupplyData(i).sCmdVolt) Then
                        Exit Function
                    End If
                Next i

                'Parse and validate the command
                For i = 1 To iStringToList(sCmd, 1, sList, ",")
                    If Len(sList(i)) <> 2 Then
                        sErrMsg = "Invalid Command Byte Size: " & sList(i)
                        Throw (New SystemException(""))
                    End If
                    If Not IsNumeric("&H" & sList(i)) Then
                        sErrMsg = "Invalid Command Byte Data: " & sList(i)
                        Throw (New SystemException(""))
                    End If
                Next i
                If i <> 4 Then
                    sErrMsg = "Invalid Number of Command Bytes: " & sCmd
                    Throw (New SystemException(""))
                End If

                'Validate Command Type
                i = CInt("&H" & Mid(sList(1), 1, 1))
                If i <> 2 Then
                    sErrMsg = "Invalid Command Type (1st byte): " & sCmd
                    Throw (New SystemException(""))
                End If

                'Validate Power Supply number
                i = CInt("&H" & Mid(sList(1), 2, 1))
                If (i < 0) Or (i > 10) Then
                    sErrMsg = "Invalid Supply Number: " & CStr(i) & " in command: " & sCmd
                    Throw (New SystemException(""))
                Else
                    EditSupply = i
                End If

                'Identify/Validate Command sub-type
                i = CInt("&H" & Mid(sList(2), 1, 1))
                If i = 4 Then 'Amps setting
                    fAmps = 256 * CInt("&H" & Mid(sList(2), 2, 1))
                    fAmps += CInt("&H" & sList(3))
                    fAmps /= 500
                    SupplyData(EditSupply).sCmdAmp = sCmd
                    SetCurCurr(EditSupply) = CStr(fAmps)
                    SupplyData(EditSupply).FuncAmpVal = SetCurCurr(EditSupply)
                    .tbrUutPuFunctions_ButtonClick(.tbrUutPuFunctions.Buttons(EditSupply - 1))
                    SetMinCurr(EditSupply) = "0.000"
                    .txtCurrent.Text = SetCurCurr(EditSupply)
                    .TextBox_LostFocus(CURRENT)
                ElseIf i = 5 Then                'Voltage setting
                    fVolts = 256 * CInt("&H" & Mid(sList(2), 2, 1))
                    fVolts += CInt("&H" & sList(3))
                    If EditSupply = 10 Then
                        fVolts /= 50
                    Else
                        fVolts /= 100
                    End If
                    SupplyData(EditSupply).sCmdVolt = sCmd
                    SetCurVolt(EditSupply) = CStr(fVolts)
                    SupplyData(EditSupply).FuncVoltVal = SetCurVolt(EditSupply)
                    .tbrUutPuFunctions_ButtonClick(.tbrUutPuFunctions.Buttons(EditSupply - 1))
                    .txtVoltage.Text = SetCurVolt(EditSupply)
                    .TextBox_LostFocus(VOLTAGE)
                ElseIf i And &H8 Then                'Options setting
                    bytB1 = CByte("&H" & sList(1))
                    bytB2 = CByte("&H" & sList(2))
                    bytB3 = CByte("&H" & sList(3))
                    SupplyData(EditSupply).FuncRelayClosed = IIf(bytB2 And &H10, True, False)
                    SupplyData(EditSupply).Master = IIf(bytB2 And &H4, False, True)
                    SupplyData(EditSupply).SensLocal = IIf(bytB2 And &H1, False, True)
                    SupplyData(EditSupply).CurProt = IIf(bytB3 And &H10, False, True)
                    SupplyData(EditSupply).RevPol = IIf(bytB3 And &H1, True, False)
                    SupplyData(EditSupply).sCmdOpt = sCmd
                    .tbrUutPuFunctions.Buttons(EditSupply - 1).Pushed = True
                    .tbrUutPuFunctions_ButtonClick(.tbrUutPuFunctions.Buttons(EditSupply - 1))
                    SendGpibCommand(EditSupply, Convert.ToString(Chr(bytB1)) & Convert.ToString(Chr(bytB2)) & Convert.ToString(Chr(bytB3)))
                Else
                    sErrMsg = "Invalid Command Type (2nd byte): " & sCmd
                    Throw (New SystemException(""))
                End If
            Catch
                If sErrMsg <> "" Then
                    MsgBox(sErrMsg & vbCrLf & vbCrLf & "Instrument will be set to RESET state.", MsgBoxStyle.Exclamation)
                    If InStr(sTipMode, "TIP_Run") Then
                        SetKey("TIPS", "STATUS", "Error from DCPS SAIS: " & sErrMsg)
                        .cmdQuit_Click()
                    Else
                        ResetPowerSupplies()
                    End If
                End If
                bActivateControl = False
            End Try
        End With
    End Function

    Sub CommandSelfTestAll()
        '************************************************************
        '* ManTech Test Systems Software Module                     *
        '************************************************************
        '* Nomenclature   : 7081-PPU                                *
        '* Written By     : Randy Zeger                             *
        '*    DESCRIPTION:                                          *
        '*     Sends a self test command to each module and displays*
        '*      the results collectively in a message box           *
        '*    EXAMPLE:                                              *
        '*       CommandSelfTestAll                                 *
        '************************************************************
        Dim CharsRead As Integer
        Dim PwrSupply As Integer
        Dim TestCommandData As String
        Dim CommandData As String
        Dim NBR As Integer
        Dim ErrorText As String = ""
        Dim FailMode(10) As String
        Dim Result(10) As String
        Dim SupplyIndex As Short
        Dim sErrorText(14) As String 'Final Supply's Error Text Array
        Dim iBITErrorCode As Integer 'BIT Error Code
        Dim iStatusErrCode As Integer 'Status Error Code
        Dim sTextFormat1 As String 'Format Text for Message Box
        Dim sTextFormat2 As String 'Format Text for Message Box
        Dim sSupply As String 'Format Supply Number for Message Box
        Dim i As Short 'Loop Index
        Dim sStatusErrors As String 'Status Error Text
        Dim sBitErrors As String 'BIT Error Text
        Dim sSupplyErrorText As String = "" 'Error Text for Power Supply

        frmAPS6062.Cursor = Cursors.WaitCursor
        For SupplyIndex = 1 To 10
            frmAPS6062.lblVolt(SupplyIndex).Text = "0.00"
            SupplyData(SupplyIndex).FuncVoltVal = "00.00"
            SupplyData(SupplyIndex).FuncAmpVal = "0.076"
            SupplyData(SupplyIndex).Master = True
            frmAPS6062.lblVolt(SupplyIndex).ForeColor = SystemColors.ControlText
            frmAPS6062.lblVolt(SupplyIndex).BackColor = SystemColors.Control
            frmAPS6062.lblVolt(SupplyIndex).Text = SupplyData(SupplyIndex).FuncVoltVal
            frmAPS6062.lblMeas(SupplyIndex).Text = SupplyData(SupplyIndex).MeasVolt
            SetCurVolt(SupplyIndex) = "00.00"
            SetCurCurr(SupplyIndex) = "0.076"
            SupplyData(SupplyIndex).SensLocal = True
            SupplyData(SupplyIndex).FuncRelayClosed = False
            SupplyData(SupplyIndex).CurProt = True
            SupplyData(SupplyIndex).RevPol = False
            frmAPS6062.cwbRelay(SupplyIndex - 1).Value = False
        Next SupplyIndex
        'Reset GUI Buttons
        frmAPS6062.tbrUutPuFunctions.Buttons(1 - 1).Pushed = True
        frmAPS6062.OptModeMaster.Checked = True
        frmAPS6062.optSenseLocal.Checked = True
        frmAPS6062.optRelayOpen.Checked = True
        frmAPS6062.optPolarityNormal.Checked = True
        frmAPS6062.optPolarityNormal.Enabled = True
        frmAPS6062.optPolarityReverse.Enabled = True
        frmAPS6062.optRelayOpen.Enabled = True
        frmAPS6062.optCurProtConstantCurrent.Enabled = True

        frmAPS6062.fraMode.Visible = True
        frmAPS6062.fraSense.Visible = True
        frmAPS6062.fraOutReg.Visible = True
        frmAPS6062.fraVoltage.Visible = True
        frmAPS6062.fraCurrent.Visible = True

        'Reset Voltage / Current Text Boxes
        SetCurVolt(EditSupply) = "00.00"
        SetCurCurr(EditSupply) = "0.076"
        frmAPS6062.txtVoltage.Text = SetCurVolt(EditSupply)
        frmAPS6062.txtCurrent.Text = SetCurCurr(EditSupply)
        'Set / Re-Set Displays To Ready States
        frmAPS6062.txtSenseDisplayA.Text = "Ready"
        frmAPS6062.txtSenseDisplayV.Text = "Ready"
        SendStatusBarMessage("Supply 1 Selected")
        EnableCommunication = True
        'PS1 Cannot be a slave
        frmAPS6062.optModeSlave.Checked = False
        frmAPS6062.optModeSlave.Enabled = False
        frmAPS6062.optModeMaster.Checked = True

        For PwrSupply = 10 To 1 Step -1
            TestCommandData = Convert.ToString(Chr(PwrSupply + &H40)) + Convert.ToString(Chr(&H80)) + Convert.ToString(Chr(&H80))
            If LiveMode Then
                ErrorStatus = atxmlDF_viWrite(SignalResourceNameArray(PwrSupply), 0, TestCommandData, CLng(Len(TestCommandData)), NBR)
            End If
        Next PwrSupply

        WaitForReady(iSupply:=1)
        Delay(5)

        'Get Results from all supplies
        For PwrSupply = 1 To 10
            For i = 1 To 14
                sErrorText(i) = ""
            Next
            sBitErrors = ""
            sStatusErrors = ""

            ReadBuffer = Space(4000)
            CommandData = Convert.ToString(Chr(PwrSupply)) + Convert.ToString(Chr(&H44)) + Convert.ToString(Chr(&H80))
            If LiveMode Then
                ErrorStatus = atxmlDF_viWrite(SignalResourceNameArray(PwrSupply), 0, CommandData, CLng(Len(CommandData)), NBR)

                Delay(1)
                ReadBuffer = Space(4000)
                ErrorStatus = atxmlDF_viRead(SignalResourceNameArray(PwrSupply), 0, ReadBuffer, 255, CharsRead)

            End If

            If ErrorStatus <> 0 Then
                MsgBox("GPIB Communication Error.  Timeout occurred.")
                sErrorText(14) = " Timed Out"
            Else
                If ErrorStatus <> 0 Then 'If Timeout Go To end of Sub, No return to analyze
                    MsgBox("GPIB Communication Error.  Timeout occurred.")
                Else
                    'Check Status Byte
                    iStatusErrCode = Asc(Mid(ReadBuffer.ToString(), 2, 1))

                    If (iStatusErrCode And &H40) <> 0 Then
                        sErrorText(8) = "Calibration in Progress"
                    End If
                    If (iStatusErrCode And &H20) <> 0 Then
                        sErrorText(9) = "Invalid Command or Sequence"
                    End If
                    If (iStatusErrCode And &H10) <> 0 Then
                        sErrorText(10) = "Over Voltage Fault"
                    End If
                    If (iStatusErrCode And &H8) <> 0 Then
                        sErrorText(11) = "Over Current Fault"
                    End If
                    If (iStatusErrCode And &H4) <> 0 Then
                        sErrorText(12) = "Constant Current Mode"
                    End If
                    If (iStatusErrCode And &H1) <> 0 Then
                        sErrorText(13) = "Under Voltage Fault (may also indicate certain over current faults)"
                    End If

                    If Asc(Mid(ReadBuffer.ToString(), 2, 1)) = &H80 Then
                        Result(PwrSupply) = "Passed"
                    Else                        'Find out why BIT Failed
                        iBITErrorCode = Asc(Mid(ReadBuffer.ToString(), 3, 1))
                        If (iBITErrorCode And &H20) <> 0 Then
                            sErrorText(1) = "EEPROM test failed"
                        End If
                        If (iBITErrorCode And &H10) <> 0 Then
                            sErrorText(2) = "65V test failed"
                        End If
                        If (iBITErrorCode And &H8) <> 0 Then
                            sErrorText(3) = "40V test failed"
                        End If
                        If (iBITErrorCode And &H4) <> 0 Then
                            sErrorText(4) = "20V test failed"
                        End If
                        If (iBITErrorCode And &H2) <> 0 Then
                            sErrorText(5) = "10V test failed"
                        End If
                        If (iBITErrorCode And &H1) <> 0 Then
                            sErrorText(6) = "2V test failed"
                        End If
                        If iBITErrorCode = 0 Then
                            sErrorText(7) = "Failed to respond"
                        End If

                        'Built-In Test Failed
                        Result(PwrSupply) = "Failed"
                    End If
                End If
            End If

            '************  Format Text for Message Box  *************
            If Len(sErrorText(14)) > 0 Then 'If Instrument Timed Out, skip BIT and Status Failures
                sTextFormat1 = ""
            Else
                sTextFormat1 = "Built-In Test " & Result(PwrSupply) & "."

                For i = 1 To 13
                    If i < 8 Then 'Build BIT Failure Text
                        'If second error then insert a comma after first
                        If Len(sBitErrors) > 0 And Len(sErrorText(i)) > 0 Then
                            sBitErrors &= ", " & sErrorText(i)
                        Else
                            sBitErrors &= sErrorText(i)
                        End If
                    Else                        'Build Status Failure Text
                        'If second error then insert a comma after first
                        If Len(sStatusErrors) > 0 And Len(sErrorText(i)) > 0 Then
                            sStatusErrors &= ", " & sErrorText(i)
                        Else
                            sStatusErrors &= sErrorText(i)
                        End If
                    End If
                Next
            End If

            'If there are any BIT Errors, put a period at the end.
            If Len(sBitErrors) > 0 Then sBitErrors &= ". "

            If Len(sStatusErrors) > 0 Then
                sTextFormat2 = "  Status: " & sStatusErrors
            Else
                sTextFormat2 = ""
            End If

            If PwrSupply = 10 Then
                sSupply = CStr(PwrSupply) & ": "
            Else
                sSupply = " " & CStr(PwrSupply) & ": "
            End If

            '*****  Build Text for Message Box  *****
            sSupplyErrorText &= "Power Supply " & sSupply & sTextFormat1 & " " & sBitErrors & sTextFormat2 & vbCrLf
            ErrorText &= sErrorText(PwrSupply)
        Next PwrSupply

        frmAPS6062.Cursor = Cursors.Default
        MsgBox(sSupplyErrorText) 'Display Message Box to Operator
    End Sub

    Sub CommandSelfTestSupply(ByVal PowerSupply As Integer)
        '************************************************************
        '* ManTech Test Systems Software Module                     *
        '************************************************************
        '* Nomenclature   : 7081-PPU                                *
        '* Written By     : Randy Zeger                             *
        '*    DESCRIPTION:                                          *
        '*     Just tests a single supply                           *
        '*    EXAMPLE:                                              *
        '*       CommandSelfTestSupply EditSupply%                  *
        '************************************************************
        Dim TestCommandData As String
        Dim CommandData As String = ""
        Dim NBR As Integer
        Dim ErrCode As Integer
        Dim Cnt As Short
        Dim i As Short 'Loop Index
        Dim iErrStatusCode As Integer 'Error Status Code
        Dim sErrorText(13) As String 'Error Text Array
        Dim sStatusErrors As String = "" 'Status Error Text
        Dim sBitErrors As String = "" 'BIT Error Text
        Dim sPassFail As String 'Self Test Pass/Fail Status
        Dim sTextFormat2 As String 'Format Text for Message Box
        Dim oIcon As MsgBoxStyle = MsgBoxStyle.Information

        frmAPS6062.Cursor = Cursors.WaitCursor
        'Clear Supply Settings to Default
        EnableCommunication = False
        frmAPS6062.lblVolt(PowerSupply).Text = "0.00"
        SupplyData(PowerSupply).FuncVoltVal = "00.00"
        SupplyData(PowerSupply).MeasVolt = "0.00"
        SupplyData(PowerSupply).MeasCurr = "0.00"
        SupplyData(PowerSupply).FuncAmpVal = "0.076"
        SupplyData(PowerSupply).Master = True
        SupplyData(PowerSupply).SensLocal = True
        SupplyData(PowerSupply).FuncRelayClosed = False
        SupplyData(PowerSupply).CurProt = True
        SupplyData(PowerSupply).RevPol = False
        frmAPS6062.cwbRelay(PowerSupply - 1).Value = False
        'Reset GUI Buttons
        frmAPS6062.optModeMaster.Checked = True
        frmAPS6062.optSenseLocal.Checked = True
        frmAPS6062.optRelayOpen.Checked = True
        frmAPS6062.optCurProtConstantVoltage.Checked = True
        frmAPS6062.optPolarityNormal.Checked = True
        frmAPS6062.optPolarityNormal.Enabled = True
        frmAPS6062.optPolarityReverse.Enabled = True
        frmAPS6062.optRelayOpen.Enabled = True
        frmAPS6062.optCurProtConstantCurrent.Enabled = True

        frmAPS6062.fraMode.Visible = True
        frmAPS6062.fraSense.Visible = True
        frmAPS6062.fraOutReg.Visible = True
        frmAPS6062.fraVoltage.Visible = True
        frmAPS6062.fraCurrent.Visible = True

        'Reset Voltage / Current Text Boxes
        SetCurVolt(EditSupply) = "00.00"
        SetCurCurr(EditSupply) = "0.076"
        frmAPS6062.txtVoltage.Text = SetCurVolt(EditSupply)
        frmAPS6062.txtCurrent.Text = SetCurCurr(EditSupply)
        frmAPS6062.lblVolt(EditSupply).ForeColor = SystemColors.ControlText
        frmAPS6062.lblVolt(EditSupply).BackColor = SystemColors.Control
        frmAPS6062.lblVolt(EditSupply).Text = SupplyData(EditSupply).FuncVoltVal
        frmAPS6062.lblMeas(EditSupply).Text = SupplyData(EditSupply).MeasVolt

        'Set / Re-Set Displays To Ready States
        frmAPS6062.txtSenseDisplayA.Text = "Ready"
        frmAPS6062.txtSenseDisplayV.Text = "Ready"
        SendStatusBarMessage("Supply " & Str(PowerSupply) & " Selected")
        EnableCommunication = True
        'PS1 Cannot be a slave
        If PowerSupply = 1 Then
            frmAPS6062.optModeSlave.Enabled = False
        End If
        '*********************************************************************************
        'Added in v1.6 To protect against BITing a Master with slaves connected
        EnableCommunication = True
        'When master or slave is reset, also reset all slaves to the right
        For i = (PowerSupply + 1) To 10 Step 1
            If SupplyData(i).Master = False Then
                Cnt += 1
            Else
                Exit For
            End If
        Next i

        'Start resetting at last slave in the chain and continue until the current supply
        i -= 1
        For Cnt = i To PowerSupply Step -1
            'Init/Reset Supply
            SetMinVolt(i) = 0
            SetMinCurr(i) = 0
            frmAPS6062.lblVolt(Cnt).Text = "0.00"
            frmAPS6062.lblMeas(Cnt).Text = "0.00"
            SupplyData(Cnt).FuncVoltVal = "00.00"
            SupplyData(Cnt).MeasVolt = "0.00"
            SupplyData(Cnt).MeasCurr = "00.00"
            SupplyData(Cnt).FuncAmpVal = "0.076"
            SupplyData(Cnt).Master = True
            SupplyData(Cnt).SensLocal = True
            SupplyData(Cnt).FuncRelayClosed = False
            SupplyData(Cnt).CurProt = True
            SupplyData(Cnt).RevPol = False
            frmAPS6062.cwbRelay(Cnt - 1).Value = False
            frmAPS6062.optRelayOpen.Checked = True
            CommandSupplyReset(Cnt)
        Next Cnt
        '**********************************************************************************

        ReadBuffer = Space(4000)
        'Open All Relays
        TestCommandData = Convert.ToString(Chr(PowerSupply + 32)) + Convert.ToString(Chr(&H80)) + Convert.ToString(Chr(&H80))
        If LiveMode Then
            ErrorStatus = atxmlDF_viWrite(resourceName, 0, TestCommandData, CLng(Len(TestCommandData)), NBR)
        End If

        'Self Test Command
        TestCommandData = Convert.ToString(Chr(PowerSupply + 64)) + Convert.ToString(Chr(&H80)) + Convert.ToString(Chr(&H80))
        If LiveMode Then
            ErrorStatus = atxmlDF_viWrite(resourceName, 0, TestCommandData, CLng(Len(TestCommandData)), NBR)
        End If

        'wait for self test to complete
        Delay(5)
        'Self Test Status Query
        CommandData = Convert.ToString(Chr(PowerSupply)) + Convert.ToString(Chr(&H44)) + Convert.ToString(Chr(&H80))
        If LiveMode Then
            ErrorStatus = atxmlDF_viWrite(resourceName, 0, CommandData, CLng(Len(CommandData)), NBR)

            Delay(1)
            'retrieve results
            ErrorStatus = atxmlDF_viRead(SignalResourceNameArray(PowerSupply), 0, ReadBuffer, 255, NBR)
        End If

        If ErrorStatus <> 0 Then 'If Timeout Go To end of Sub, No return to analyze
            MsgBox("GPIB Communication Error.  Timeout occurred.")
        Else
            '***  Check Status Byte  ***
            iErrStatusCode = Asc(Mid(ReadBuffer.ToString(), 2, 1))

            If (iErrStatusCode And &H40) <> 0 Then
                sErrorText(8) = "Calibration in Progress"
            End If

            If (iErrStatusCode And &H20) <> 0 Then
                sErrorText(9) = "Invalid Command or Sequence"
            End If

            If (iErrStatusCode And &H10) <> 0 Then
                sErrorText(10) = "Over Voltage Fault"
            End If

            If (iErrStatusCode And &H8) <> 0 Then
                sErrorText(11) = "Over Current Fault"
            End If

            If (iErrStatusCode And &H4) <> 0 Then
                sErrorText(12) = "Constant Current Mode"
            End If

            If (iErrStatusCode And &H1) <> 0 Then
                sErrorText(13) = "Under Voltage Fault (may also indicate certain over current faults)"
            End If

            If Asc(Mid(ReadBuffer.ToString(), 2, 1)) = &H80 Then
                sPassFail = "     Built-In Test Passed"
            Else
                '***  Find out why BIT Failed  ***
                ErrCode = Asc(Mid(ReadBuffer.ToString(), 3, 1))
                If (ErrCode And &H20) <> 0 Then
                    sErrorText(1) = "EEPROM test failed"
                End If
                If (ErrCode And &H10) <> 0 Then
                    sErrorText(2) = "65V test failed"
                End If
                If (ErrCode And &H8) <> 0 Then
                    sErrorText(3) = "40V test failed"
                End If
                If (ErrCode And &H4) <> 0 Then
                    sErrorText(4) = "20V test failed"
                End If
                If (ErrCode And &H2) <> 0 Then
                    sErrorText(5) = "10V test failed"
                End If
                If (ErrCode And &H1) <> 0 Then
                    sErrorText(6) = "2V test failed"
                End If
                If ErrCode = 0 Then
                    sErrorText(7) = "Failed to respond"
                End If

                'Built-In Test Failed, Set MsgBox Icon
                sPassFail = "     Built-In Test Failed"
                oIcon = MsgBoxStyle.Exclamation 'Set MsgBox Icon to vbExplaination
            End If

            'Build Text to display in Message Box
            For i = 1 To 13
                If i < 8 Then 'Build BIT Failure Text
                    'If second error then insert a comma after first
                    If Len(sBitErrors) > 0 And Len(sErrorText(i)) > 0 Then
                        sBitErrors &= ", " & sErrorText(i)
                    Else
                        sBitErrors &= sErrorText(i)
                    End If
                Else                    'Build Status Failure Text
                    'If second error then insert a comma after first
                    If Len(sStatusErrors) > 0 And Len(sErrorText(i)) > 0 Then
                        sStatusErrors &= ", " & sErrorText(i)
                    Else
                        sStatusErrors &= sErrorText(i)
                    End If
                End If
            Next

            'If there are any BIT Errors, put a period at the end.
            If Len(sBitErrors) > 0 Then sBitErrors &= ". "

            If Len(sStatusErrors) > 0 Then
                sTextFormat2 = "Status: " & sStatusErrors
            Else
                sTextFormat2 = ""
            End If

            'Display Message Box
            MsgBox("Power Supply: " & Str(PowerSupply) & " " & sPassFail & vbCrLf & sBitErrors & vbCrLf & sTextFormat2, oIcon)
        End If
        frmAPS6062.Cursor = Cursors.Default
    End Sub

    Sub CommandSetOptions(ByVal Supply As Short, ByVal OpenRelay As Short, ByVal SetMaster As Short, ByVal SenseLocal As Short, ByVal CurrentLimit As Short, ByVal ReversePolarity As Short)
        '   Stores current voltage setting TIP command for supply.
        '************************************************************
        '* ManTech Test Systems Software Module                     *
        '************************************************************
        '* Nomenclature   : 7081-PPU                                *
        '* Written By     : Randy Zeger                             *
        '*    DESCRIPTION:                                          *
        '*     Sets options of selected supply                      *
        '*      a +1= no action, a -1=true and a 0 = false          *
        '*     Stores current relay settings TIP command for supply *
        '*    EXAMPLE:                                              *
        '*       CommandSetOptions EditSupply%, -1, 1, 0, -1, 0     *
        '************************************************************

        Dim B1 As Byte
        Dim B2 As Byte
        Dim B3 As Byte
        Dim bB2 As Byte 'Current cumulative B2 options state
        Dim bB3 As Byte 'Current cumulative B3 options state

        If EnableCommunication = False Then Exit Sub

        B1 = &H20 + Supply
        B2 = &H80
        B3 = &H0

        If Len(SupplyData(Supply).sCmdOpt) > 8 Then
            bB2 = CByte("&H" & Mid(SupplyData(Supply).sCmdOpt, 4, 2))
            bB3 = CByte("&H" & Mid(SupplyData(Supply).sCmdOpt, 7, 2))
        End If

        'Note: If a +1 is passed do nothing
        Select Case OpenRelay
            Case True
                '(-1)
                B2 = B2 Or &HA0 'Enable / Relay Open
                bB2 = (bB2 And &HCF) Or &HA0
                SupplyData(Supply).FuncRelayClosed = False
            Case False
                '( 0)
                B2 = B2 Or &HB0 'Enable / Relay Closed
                bB2 = (bB2 And &HCF) Or &HB0
                SupplyData(Supply).FuncRelayClosed = True
        End Select

        Select Case SetMaster
            Case True
                '(-1)
                B2 = B2 Or &H88 'Enable /Set as Master
                bB2 = (bB2 And &HF3) Or &H88
                SupplyData(Supply).Master = True
            Case False
                '( 0)
                B2 = B2 Or &H8C 'Enable / Set as Slave
                bB2 = (bB2 And &HF3) Or &H8C
                SupplyData(Supply).Master = False
        End Select

        Select Case SenseLocal
            Case True
                '(-1)
                B2 = B2 Or &H82 'Enable / Sense Local
                bB2 = (bB2 And &HFC) Or &H82
                SupplyData(Supply).SensLocal = True
            Case False
                '( 0)
                B2 = B2 Or &H83 'Enable / Sense Remote
                bB2 = (bB2 And &HFC) Or &H83
                SupplyData(Supply).SensLocal = False
        End Select

        Select Case CurrentLimit
            Case True
                '(-1)
                B3 = B3 Or &H20 'Enable / Constant Voltage (Trip @ Current Setting)
                bB3 = (bB3 And &HCF) Or &H20
                SupplyData(Supply).CurProt = True
            Case False
                '( 0)
                B3 = B3 Or &H30 'Enable / Constant Current
                bB3 = (bB3 And &HCF) Or &H30
                SupplyData(Supply).CurProt = False
        End Select

        Select Case ReversePolarity
            Case True
                '(-1)
                B3 = B3 Or &H3 'Enable / Reverse polarity
                bB3 = (bB3 And &HFC) Or &H3
                SupplyData(Supply).RevPol = True
            Case False
                '( 0)
                B3 = B3 Or &H2 'Enable / Normal polarity
                bB3 = (bB3 And &HFC) Or &H2
                SupplyData(Supply).RevPol = False
        End Select

        'Send Command
        SupplyData(Supply).sCmdOpt = Hex(B1) & "," & Hex(bB2) & "," & IIf(bB3 > &HF, Hex(bB3), ("0" & Hex(bB3)))
        SendGpibCommand(Supply, Convert.ToString(Chr(B1)) & Convert.ToString(Chr(B2)) & Convert.ToString(Chr(B3)))
    End Sub

    Public Sub CommandSetVoltage(ByVal Supply As Short, ByRef Volts As Double)
        'DESCRIPTION:
        '   Sets the voltage level of a DC Supply.
        '   Stores current voltage setting TIP command for supply.
        'PARAMETERS:
        '   Supply%:    Number of supply to set (1-10)
        '   Volts!:     The Voltage level value
        'GLOBAL VARIABLES MODIFIED:
        '   none
        Dim B1 As Byte, B2 As Byte, B3 As Byte

        'mh set the lbl
        frmAPS6062.lblVolt(Supply).Text = Str(Volts)

        'Convert to Integer DAC code
        If Supply = 10 Then
            Volts *= 50 '20mV Resolution
        Else
            Volts *= 100 '10mV Resolution
        End If

        B1 = &H20 + Supply
        B2 = &H50 + (Volts \ &H100)
        B3 = Volts Mod &H100

        'Send Command
        SendGpibCommand(Supply, Convert.ToString(Chr(B1)) & Convert.ToString(Chr(B2)) & Convert.ToString(Chr(B3)))
        SupplyData(Supply).sCmdVolt = Hex(B1) & "," & Hex(B2) & "," & IIf(B3 > &HF, Hex(B3), ("0" & Hex(B3)))
    End Sub

    Public Sub CommandSetCurrent(ByVal Supply As Short, ByRef fAmps As Single)
        'DESCRIPTION:
        '   Sets the current limit value of a DC Supply
        '   Stores current amps setting TIP command for supply.
        'PARAMETERS:
        '   Supply%:    Number of supply to set (1-10)
        '   Amps!:      The Current limit value
        'GLOBAL VARIABLES MODIFIED:
        '   none
        Dim B1 As Byte, B2 As Byte, B3 As Byte

        'Convert to Integer DAC code
        fAmps *= 500 '2mA Resolution

        B1 = &H20 + Supply
        B2 = &H40 + (fAmps \ &H100)
        B3 = fAmps Mod &H100

        'Send Command
        SendGpibCommand(Supply, Convert.ToString(Chr(B1)) & Convert.ToString(Chr(B2)) & Convert.ToString(Chr(B3)))
        SupplyData(Supply).sCmdAmp = Hex(B1) & "," & Hex(B2) & "," & IIf(B3 > &HF, Hex(B3), ("0" & Hex(B3)))
    End Sub

    Sub CommandSupplyReset(ByVal Supply As Short)
        'Send "Reset" Command
        SendGpibCommand(Supply, Convert.ToString(Chr(&H10 + Supply)) & Convert.ToString(Chr(0)) & Convert.ToString(Chr(0)))
    End Sub

    Sub ParseTipCmds()
        'DESCRIPTION:
        '   Parses sTipCmds to extract the TIP mode and TIP commands.
        'PARAMETERS:    None
        'GLOBAL VARIABLES USED:
        '   sTipCmds:   Expected to already contain the VIPERT.INI CMD key contents.
        '       Example: "DCPS TIP_Design 21,49,C4;..."
        '       Will be written with the command section, if any, of its original contents.
        '   sTipMode:   Will be written with the TIP mode from the CMD key.
        Dim i As Short
        Dim sErrMsg As String
        Dim sList() As String = {""}

        sErrMsg = ""
        Try
            i = iStringToList(sTipCmds, 1, sList, " ")
            If i < 2 Then
                sErrMsg = "Invalid CMD Key Format, too few arguments: " & Strings.Left(sTipCmds, 30)
                Throw (New SystemException(""))
            End If

            If sList(1) <> "DCPS" Then
                sErrMsg = "Invalid CMD Key 1st Argument: " & sList(1)
                Throw (New SystemException(""))
            End If

            Select Case sList(2)
                Case "TIP_Design", "TIP_RunSetup", "TIP_RunPersist"
                    sTipMode = sList(2)

                Case Else
                    sErrMsg = "Invalid CMD Key 2nd Argument: " & sList(2)
                    Throw (New SystemException(""))
            End Select

            If i = 3 Then
                sTipCmds = sList(3)
            ElseIf i = 2 Then
                sTipCmds = ""
            Else
                sTipCmds = ""
                sErrMsg = "Invalid CMD Key Format, too many arguments: " & Strings.Left(sTipCmds, 30)
                Throw (New SystemException(""))
            End If

        Catch
            'Clear CMD key to prevent subsequent executions during monitor
            SetKey("TIPS", "CMD", "")
            If sErrMsg <> "" Then
                MsgBox(sErrMsg & vbCrLf & vbCrLf & "Instrument will be set to RESET state.", MsgBoxStyle.Exclamation)
                If InStr(sTipMode, "TIP_Run") Or InStr(sCommandLine, "TIP_Run") Or sTipMode = "" Then
                    SetKey("TIPS", "STATUS", "Error from DCPS SAIS: " & sErrMsg)
                    frmAPS6062.cmdQuit_Click()
                Else
                    If sCommandLine = "" Then 'If not the initial run, where reset is going to occur anyway
                        ResetPowerSupplies()
                    End If
                End If
            End If
        End Try
    End Sub

    Sub SendGpibCommand(ByVal iSupply As Short, ByVal sCommand As String)
        'DESCRIPTION:
        '   This procedure sends a 3-byte command string to a DC Supply. If the command
        '   is not a query command, this procedure will error-check the supply.
        'PARAMETERS:
        '   iSupply:    DCPS Supply Number
        '   sCommand:   3-byte command string
        Dim sStatus As String
        Dim bytStatus As Byte
        Dim sErrors As String
        Dim TestCommandData As String
        Dim NBR As Integer

        If EnableCommunication = False Or LiveMode = False Then Exit Sub

        'Check mode in
        If frmAPS6062.Panel_Conifg.DebugMode = True And Strings.Right(sCommand, 1) <> Convert.ToString(Chr(&H0)) Then Exit Sub

        If bInstrumentMode Then
            If sCommand <> (Convert.ToString(Chr(iSupply)) & Convert.ToString(Chr(&H44)) & Convert.ToString(Chr(&H0))) Then
                If sCommand <> (Convert.ToString(Chr(iSupply)) & Convert.ToString(Chr(&H42)) & Convert.ToString(Chr(&H0))) Then Exit Sub
            End If
        End If

        ' ensure that when in monitor mode no other commands are sent but queries
        If frmAPS6062.Panel_Conifg.DebugMode = True Then
            If sCommand <> (Convert.ToString(Chr(iSupply)) & Convert.ToString(Chr(&H44)) & Convert.ToString(Chr(&H0))) Then
                If sCommand <> (Convert.ToString(Chr(iSupply)) & Convert.ToString(Chr(&H42)) & Convert.ToString(Chr(&H0))) Then Exit Sub
            End If
        End If

        If Len(sCommand) <> 3 Then
            MsgBox("Invalid Command length in SendGpibCommand Sub: " & sCommand)
            Exit Sub
        End If

        ErrorStatus = atxmlDF_viWrite(SignalResourceNameArray(iSupply), 0, sCommand, CLng(Len(sCommand)), NBR)

        Delay(0.3) 'mh added

        DisplayVisaErrorMsg(iSupply, ErrorStatus)

        If (Asc(Strings.Left(sCommand, 1)) And &HF0) <> 0 Then 'If NOT a Query command
            'Send Status Query Command
            TestCommandData = Convert.ToString(Chr(iSupply)) & Convert.ToString(Chr(&H44)) & Convert.ToString(Chr(0))
            ErrorStatus = atxmlDF_viWrite(SignalResourceNameArray(iSupply), 0, TestCommandData, CLng(Len(TestCommandData)), NBR)
            DisplayVisaErrorMsg(iSupply, ErrorStatus)
            'Get Status Query Returned Data (5 bytes)
            sStatus = ReadInstrumentBuffer(iSupply)
            'Extract just the Status Byte
            If Not (sStatus = "") Then
                bytStatus = CByte(Asc(Mid(sStatus, 2, 1)))
            End If

            sErrors = ""
            If (bytStatus And &H40) Then sErrors &= " Cal_in_Progress"
            If (bytStatus And &H20) Then sErrors &= " Invalid_Command"
            If (bytStatus And &H10) Then sErrors &= " Over_Voltage"
            If (bytStatus And &H8) Then sErrors &= " Over_Current"
            If (bytStatus And &H2) Then sErrors &= " BIT_Failure_&H" & Hex(CByte(Asc(Mid(sStatus, 3, 1))))
            If (bytStatus And &H1) Then sErrors &= " Under_Voltage"

            If sErrors <> "" Then
                If InStr(sTipMode, "TIP_Run") Then
                    MsgBox(sErrors, MsgBoxStyle.Exclamation, "DCPS " & iSupply & " Error Message")
                    SetKey("TIPS", "STATUS", "Error from DCPS " & iSupply & ": H" & bytStatus.ToString("X2") & sErrors)
                    frmAPS6062.cmdQuit_Click()
                End If
                MsgBox(sErrors & vbCrLf & vbCrLf & "DCPS " & iSupply & " will be set to RESET state", MsgBoxStyle.Exclamation, "DCPS " & iSupply & " Error Message")
                ResetPowerSupply(iSupply)
            End If
        End If
    End Sub

    Sub InitInputBoxLists()
        '************************************************************
        '* Nomenclature   : 7081-PPU                                *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     This Module Initializes all text and combo boxes     *
        '*    EXAMPLE:                                              *
        '*     InitInputBoxLists                                    *
        '************************************************************
        Dim i As Short
        'Initialize Defaults
        SetDef(VOLTAGE) = "00.00"
        SetDef(CURRENT) = "0.076"
        'Initialize Resolution
        SetRes(VOLTAGE) = "0.00"
        SetRes(CURRENT) = "0.000"

        For i = 1 To 9
            'Initialize Increment Values
            SetIncVolt(i) = "0.1"
            SetIncCurr(i) = "0.1"
            'Initialize Minimums
            SetMinVolt(i) = "00.00"
            SetMinCurr(i) = "0.000"
            'Initialize Maximums
            SetMaxVolt(i) = "40.00"
            SetMaxCurr(i) = "5.000"
            'Initialize Current Value
            SetCurVolt(i) = "00.00"
            SetCurCurr(i) = "0.076"
            'Initialize Range Message
            SetRngMsgVolt(i) = "Min: 0 Volt    Def: 0 Volt    Max: 40 Volt"
            SetRngMsgCurr(i) = "Min: 0 Amp    Def: 76 mAmp   Max: 5 Amp"
            'Initialize SupplyData
            SupplyData(i).FuncVoltVal = SetCurVolt(i)
            SupplyData(i).FuncAmpVal = SetCurCurr(i)
        Next i

        i = 10
        'Initialize Increment Values
        SetIncVolt(i) = "0.1"
        SetIncCurr(i) = "0.1"
        'Initialize Minimums
        SetMinVolt(i) = "00.00"
        SetMinCurr(i) = "0.000"
        'Initialize Maximums
        SetMaxVolt(i) = "65.00"
        SetMaxCurr(i) = "5.000"
        'Initialize Current Value
        SetCurVolt(i) = "00.00"
        SetCurCurr(i) = "0.076"
        'Initialize Range Message
        SetRngMsgVolt(i) = "Min: 0 Volt    Def: 0 Volt    Max: 65 Volt"
        SetRngMsgCurr(i) = "Min: 0 Amp    Def: 76 mAmp   Max: 5 Amp"
        'Initialize SupplyData
        SupplyData(i).FuncVoltVal = SetCurVolt(i)
        SupplyData(i).FuncAmpVal = SetCurCurr(i)

        'Init Display
        frmAPS6062.txtVoltage.Text = SetCurVolt(1)
        frmAPS6062.txtCurrent.Text = SetCurCurr(1)
    End Sub

    Public Sub Main()
        '************************************************************
        '* Nomenclature   : 7081-PPU                                *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     This module is the program entry (starting) point.   *
        '************************************************************
        Dim iSupply As Short
        Dim sErrors As String
        Dim bytStatus As Byte

        'Find Windows Directory
        sWindowsDir = Environment.GetFolderPath(Environment.SpecialFolder.Windows)

        bLoadingTipCmds = False
        EditSupply = 1

        'Check the command line argument to see if called by TIP Studio
        sCommandLine = Microsoft.VisualBasic.Command()
        If Strings.Left(sCommandLine, 4) = "TIP_" Then
            sTipCmds = sGetKey("TIPS", "CMD")
            ParseTipCmds()
            sCommandLine = ""
        Else
            sTipMode = ""
            sTipCmds = ""
        End If

        'Show Introduction Form
        'mh  Screen.MousePointer = vbHourglass
        SplashStart()
        Application.DoEvents()
        'Check System and Instrument For Errors
        'LiveMode% = True
        LiveMode = VerifyInstrumentCommunication()

        'Set-Up Main Form
        Q = Convert.ToString(Chr(34))
        EditSupply = 1
        InitInputBoxLists()

        SetTipButtons(False)
        'Set the width so it doesn't include space for the SSPanelAuxCMD panel
        frmAPS6062.Width = 627
        ' 10000 TOO BIG 7000 TOO SMALL
        If InStr(sTipMode, "TIP_") Then
            frmAPS6062.Top = 0
            frmAPS6062.Left = PrimaryScreen.Bounds.Width - frmAPS6062.Width
        Else
            CenterForm(frmAPS6062)
        End If

        'Fix Toolbar Custom Control Bug
        frmAPS6062.tbrUutPuFunctions.Height = 510

        'Reset to Power-On Defaults
        'ResetPowerSupplies

        'JRC 09-12-06
        ' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        'Clear Supply Settings to Default

        'JRC 09-12-06
        'EnableCommunication% = False
        EnableCommunication = True


        'Remove Introduction Form
        frmAbout.Hide()

        'Show the Front Panel (Modal)
        'mh Screen.MousePointer = vbArrow
        frmAPS6062.Show()
        frmAPS6062.Refresh()
        SendStatusBarMessage("Supply 1 Selected")
        Application.DoEvents()
        frmAPS6062.tbrUutPuFunctions.Buttons(1 - 1).Pushed = True

        'mh initialize the structure to defaults
        For iSupply = 1 To 10 Step 1
            SupplyData(iSupply).Master = True 'default to all being master
            SupplyData(iSupply).CurProt = True 'defaul to constant voltage mode
            SupplyData(iSupply).MeasCurr = "0.00"
            SupplyData(iSupply).MeasVolt = "0.00"
            SupplyData(iSupply).RevPol = False 'default to positive voltage
            SupplyData(iSupply).SensLocal = True 'default to use of Local Sense Lines
            SupplyData(iSupply).FuncRelayClosed = False 'default the supply being open/off
        Next iSupply

        'V1.6 force the form to start at Functions Tab - TDR98289
        Application.DoEvents()
        frmAPS6062.tabUserOptions.SelectedIndex = 0
        Application.DoEvents()

        'mh  Screen.MousePointer = vbDefault

        'If not a TIP execution, do no more
        If sTipMode <> "" Then
            'Do first TIP step then monitor for subsequent TIP steps
            Do
                Select Case sTipMode
                    Case "TIP_Design"
                        RunTipCmds()
                        SetTipButtons(True)
                    Case "TIP_RunSetup"
                        SetTipButtons(False)
                        RunTipCmds()
                        frmAPS6062.WindowState = FormWindowState.Minimized
                    Case "TIP_RunPersist"
                        SetTipButtons(False)
                        RunTipCmds()
                        If InStr(sTipCmds, ";M") Then 'If IS a measurement TIP step
                            frmAPS6062.WindowState = FormWindowState.Minimized
                        End If
                End Select

                If Not frmAPS6062.cmdUpdateTip.Visible Then 'If not in Design mode
                    'Check for tripped supplies
                    For iSupply = 10 To 1 Step -1
                        'Only check supplies being used
                        If SupplyData(iSupply).FuncRelayClosed Then
                            bytCommandReadStatus(iSupply)
                            'Look at all but the CC Mode bit
                            bytStatus = bytStatusByte And &H7B
                            If bytStatus <> 0 Then
                                sErrors = ""
                                If (bytStatus And &H40) Then sErrors &= " Cal_in_Progress"
                                If (bytStatus And &H20) Then sErrors &= " Invalid_Command"
                                If (bytStatus And &H10) Then sErrors &= " Over_Voltage"
                                If (bytStatus And &H8) Then sErrors &= " Over_Current"
                                If (bytStatus And &H2) Then sErrors &= " BIT_Failure"
                                If (bytStatus And &H1) Then sErrors &= " Under_Voltage"
                                MsgBox(sErrors, MsgBoxStyle.Exclamation, "DCPS " & iSupply & " Error Message")
                                SetKey("TIPS", "STATUS", "Error from DCPS " & iSupply & ": H" & bytStatus.ToString("X2") & sErrors)
                                frmAPS6062.cmdQuit_Click()
                            End If
                        End If
                    Next iSupply

                    If sTipMode <> "" Then
                        If sMeasVal = "" Then
                            SetKey("TIPS", "STATUS", "Ready")
                        Else
                            SetKey("TIPS", "STATUS", "Ready " & sMeasVal)
                        End If
                    End If
                End If

                Delay(1) 'Monitor at 1 second intervals
                sTipCmds = sGetKey("TIPS", "CMD")

                If Strings.Left(sTipCmds, 4) = "DCPS" Then
                    frmAPS6062.WindowState = FormWindowState.Normal
                    ParseTipCmds()
                Else
                    'Clear TIP Mode and monitor for more TIPs steps
                    sTipMode = ""
                    sTipCmds = ""
                End If
            Loop
        End If

        'Set the Refresh rate when in "Debug" mode from 1000 = 1 sec to 25000 = 25 sec
        frmAPS6062.Panel_Conifg.Refresh = 15000

        'mh indicate that sfp is only to query instrument
        bInstrumentMode = True
        'Initalize
        frmAPS6062.Refresh()
        frmAPS6062.ConfigGetCurrent()

        bInstrumentMode = False

        Application.DoEvents()
        frmAPS6062.tbrUutPuFunctions_ButtonClick(frmAPS6062.tbrUutPuFunctions.Buttons("ButtonPS1"))

        'DO NOT Put a break point After this point. Causes error if timer is running
        'Set the Mode the Instrument is in: Local = True, Debug = False
        If iStatus <> 1 Then
            frmAPS6062.Panel_Conifg.SetDebugStatus(True)
        Else
            frmAPS6062.Panel_Conifg.SetDebugStatus(False)
        End If
    End Sub

    Public Function EngNotate(ByVal Number As Double, ByVal Digits As Short, ByVal Unit As String) As String
        EngNotate = ""
        '************************************************************
        '* ManTech Test Systems Software Module                     *
        '************************************************************
        '* Nomenclature   : 7081-PPU                                *
        '* Written By     : David Hartley                           *
        '*    DESCRIPTION:                                          *
        '*   Returns passed number as numeric string in Engineering *
        '*      notation (every 3rd exponent) with selectable       *
        '*      precision along with Unit Of Measure.               *
        '*    EXAMPLE:                                              *
        '*      frmAPS6062.txtSenseDisplayV.Text =                  *
        '*          EngNotate$(Val(ReadBufferV$), 2, "Volts")       *
        '************************************************************
        Dim Multiplier As Short, Negative As Boolean
        Dim Prefix As String, ReturnString As String

        Multiplier = 0 : Negative = False 'Initialize local variables

        If Number < 0 Then 'If negative
            Number = Math.Abs(Number) 'Make it positive for now
            Negative = True 'Set flag
        End If

        If Number >= 1000 Then 'For positive exponent
            Do While Number >= 1000 And Multiplier <= 4
                Number /= 1000
                Multiplier += 1
            Loop
        ElseIf Number < 1 And Number <> 0 Then            'For negative exponent (but not 0)
            Do While Number < 1 And Multiplier >= -4
                Number *= 1000
                Multiplier -= 1
            Loop
        End If

        Select Case Multiplier
            Case 4
                Prefix = " T" 'Terra  E+12
            Case 3
                Prefix = " G" 'Giga   E+09
            Case 2
                Prefix = " M" 'Mega   E+06
            Case 1
                Prefix = " K" 'Kilo   E+03
            Case 0
                Prefix = "  " '<none> E+00
            Case -1
                Prefix = " m" 'milli  E-03
            Case -2
                Prefix = " " & Convert.ToString(Chr(181)) 'micro  E-06
            Case -3
                Prefix = " n" 'nano   E-09
            Case -4
                Prefix = " p" 'pico   E-12

            Case Else
                Prefix = " "
        End Select

        If Negative Then Number = -Number

        If Multiplier > 4 Then
            ReturnString = "Ovr Rng"
        ElseIf Multiplier < -4 Then
            ReturnString = "UndrRng"
        Else
            Select Case Digits
                Case 0
                    ReturnString = Number.ToString("0")
                Case 1
                    ReturnString = Number.ToString("0.0")
                Case 2
                    ReturnString = Number.ToString("0.00")
                Case 3
                    ReturnString = Number.ToString("0.000")
                Case 4
                    ReturnString = Number.ToString("0.0000")
                Case 5
                    ReturnString = Number.ToString("0.00000")
                Case 6
                    ReturnString = Number.ToString("0.000000")
                Case 7
                    ReturnString = Number.ToString("0.0000000")
                Case 8
                    ReturnString = Number.ToString("0.00000000")
                Case Else
                    ReturnString = Number.ToString("0.000000000")
            End Select
        End If

        If Not (Negative) Then
            EngNotate = "+" & ReturnString & Prefix & Unit
        Else
            EngNotate = ReturnString & Prefix & Unit
        End If
    End Function

    Function ReadInstrumentBuffer(ByVal Slot As Short) As String
        '************************************************************
        '* Nomenclature   : 7081-PPU                                *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     This Module reads a string from the instrument buffer*
        '*    EXAMPLE:                                              *
        '*     Data$=ReadInstrumentBuffer$(EditSupply%)             *
        '************************************************************
        Dim NBR As Integer
        Dim retStr As String = "0.0"
        If LiveMode Then
            ReadBuffer = Space(4000)
            ErrorStatus = atxmlDF_viRead(SignalResourceNameArray(Slot), 0, ReadBuffer, 255, NBR)
            If ErrorStatus Then
                '          DisplayVisaErrorMsg Slot%, ErrorStatus&
            End If
            retStr = Strings.Left(ReadBuffer.ToString(), NBR)
        End If
        ReadInstrumentBuffer = retStr
    End Function

    Public Sub Delay(ByVal dSeconds As Double)
        'DESCRIPTION:
        '   Delays for a specified number of seconds.
        '   50 milli-second resolution.
        '   Minimum delay is about 50 milli-seconds.
        'PARAMETERS:
        '   dSeconds: The number of seconds to Delay.
        Dim t As Double

        t = dGetTime()
        dSeconds /= SECS_IN_DAY
        Do
            'mh DoEvents
        Loop While dGetTime() - t < dSeconds
    End Sub

    Public Function dGetTime() As Double
        'DESCRIPTION:
        '   Determines the time since 1900.
        '   Avoids errors caused by 'Timer' when crossing midnight.
        'PARAMETERS:
        '   None
        'RETURNS:
        '   A double-precision floating-point number in the format d.s where:
        '     d = the number of days since 1900 and
        '     s = the fraction of a day with 10 milli-second resolution

        'The CDbl(Now) function returns a number formatted d.s where:
        ' d = the number of days since 1900 and
        ' s = the fraction of today with 1 second resolution.
        'CLng(Now) returns only the number of days (the "d." part).
        'Timer returns the number of seconds since midnight with 10 mSec resoulution.
        'So, the following statement returns a composite double-float number representing
        ' the time since 1900 with 10 mSec resoultion. (no cross-over midnight bug)
        Dim date1 As New Date(1900, 1, 1, 0, 0, 0)
        dGetTime = DateDiff(DateInterval.Day, Now, date1) + (DateTime.Now.TimeOfDay.TotalSeconds / SECS_IN_DAY)
    End Function

    Sub SetTipButtons(ByVal bState As Boolean)
        frmAPS6062.cmdUpdateTip.Visible = bState
        frmAPS6062.cmdUpdateTipVoltage.Visible = bState
        frmAPS6062.cmdUpdateTipCurrent.Visible = bState
    End Sub

    Public Sub SplashStart()
        '************************************************************
        '* Nomenclature   : 7081-PPU                                *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*      Displays the Splash screen                          *
        '*    EXAMPLE:                                              *
        '*     SplashStart                                          *
        '************************************************************
        '! Load frmAbout
        frmAbout.cmdOk.Visible = False
        frmAbout.Show()
        frmAbout.Cursor = Cursors.WaitCursor
        Application.DoEvents()
    End Sub

    Public Function iStringToList(ByVal sStr As String, ByVal iLower As Short, ByRef List() As String, ByVal sDelimiter As String) As Short
        'DESCRIPTION:
        '   Procedure to convert a delimited string into a dynamic string array
        '   ReDims the array from iLower to the number of elements in string
        'Parameters:
        '   sStr       : String to be parsed.
        '   iLower     : Lower bound of target array
        '   List$()    : Dynamic array in which to return list of strings
        '   sDelimiter : Delimiter string.
        'Returns:
        '   Number of items in string
        '   or 0 if string is empty

        Dim numels As Short, i As Integer
        Dim iDelimiterLength As Integer

        iDelimiterLength = Len(sDelimiter)
        If sStr = "" Then
            iStringToList = 0
            Exit Function
        End If

        numels = 1
        ReDim List(iLower)
        'Go through parsed string a character at a time.
        For i = 1 To Len(sStr)
            'Test for delimiter
            If Mid(sStr, i, iDelimiterLength) <> sDelimiter Then
                'Add the character to the current argument.
                List(iLower + numels - 1) &= Mid(sStr, i, 1)
            Else
                'Found a delimiter.
                ReDim Preserve List(iLower + numels)
                numels += 1
                i += iDelimiterLength - 1
            End If
        Next i
        iStringToList = numels
    End Function

    Sub ResetPowerSupplies()
        'DESCRIPTION:
        '   This procedure resets all 10 PPU DC Power Supplies.
        'PARAMETERS:    None
        'GLOBAL VARIABLES USED:
        '   bytStatusByte
        '   bytModeByte

        Dim SupplyIndex As Short
        Dim TestCommandData As String
        Dim NBR As Integer

        frmAPS6062.WindowState = FormWindowState.Normal
        'mh Screen.MousePointer = vbHourglass
        Application.DoEvents()

        'Clear Supply Settings to Default
        If LiveMode Then
            If frmAPS6062.Panel_Conifg.DebugMode = True Then Exit Sub

            frmAPS6062.Cursor = Cursors.WaitCursor 'Must do this again sometimes for unknown reason
            SendStatusBarMessage("Resetting PPU")

            EnableCommunication = False
            frmAPS6062.txtLastCalDate.Text = ""
            frmAPS6062.txtCurrentSN.Text = ""
            frmAPS6062.txtNewSN.Text = ""

            For SupplyIndex = 1 To 10
                SetMinVolt(SupplyIndex) = 0
                SetMinCurr(SupplyIndex) = 0
                SetCurVolt(SupplyIndex) = "00.00"
                SetCurCurr(SupplyIndex) = "0.076"
                frmAPS6062.lblVolt(SupplyIndex).Text = "0.00"
                frmAPS6062.lblMeas(SupplyIndex).Text = "0.00"
                frmAPS6062.lblVolt(SupplyIndex).ForeColor = SystemColors.ControlText
                frmAPS6062.lblVolt(SupplyIndex).BackColor = SystemColors.Control
                SupplyData(SupplyIndex).FuncVoltVal = "00.00"
                SupplyData(SupplyIndex).sCmdVolt = "2" & Hex(SupplyIndex) & ",50,00"
                SupplyData(SupplyIndex).FuncAmpVal = "0.076"
                SupplyData(SupplyIndex).sCmdAmp = "2" & Hex(SupplyIndex) & ",40,26"
                SupplyData(SupplyIndex).Master = True
                SupplyData(SupplyIndex).SensLocal = True
                SupplyData(SupplyIndex).FuncRelayClosed = False
                SupplyData(SupplyIndex).CurProt = True
                SupplyData(SupplyIndex).RevPol = False
                SupplyData(SupplyIndex).sCmdOpt = "2" & Hex(SupplyIndex) & ",AA,22"
                frmAPS6062.cwbRelay(SupplyIndex - 1).Value = False

                ' initialize the structure to defaults
                SupplyData(SupplyIndex).MeasCurr = "0.00"
                SupplyData(SupplyIndex).MeasVolt = "0.076"
            Next SupplyIndex

            'Reset GUI Buttons
            For Each btn In frmAPS6062.tbrUutPuFunctions.Buttons
                btn.Pushed = False
            Next
            frmAPS6062.tbrUutPuFunctions.Buttons(0).Pushed = True
            frmAPS6062.optModeMaster.Checked = True
            frmAPS6062.optSenseLocal.Checked = True
            frmAPS6062.optRelayOpen.Checked = True
            frmAPS6062.optPolarityNormal.Checked = True
            frmAPS6062.optCurProtConstantVoltage.Checked = True

            frmAPS6062.fraMode.Visible = True
            frmAPS6062.fraSense.Visible = True
            frmAPS6062.fraOutReg.Visible = True
            frmAPS6062.fraVoltage.Visible = True
            frmAPS6062.fraCurrent.Visible = True
            frmAPS6062.optCurProtConstantCurrent.Enabled = True
            frmAPS6062.optRelayOpen.Enabled = True
            frmAPS6062.optPolarityNormal.Enabled = True
            frmAPS6062.optPolarityReverse.Enabled = True
            frmAPS6062.optModeMaster.Enabled = True

            'Reset Voltage / Current Text Boxes
            SetCurVolt(EditSupply) = "00.00"
            SetCurCurr(EditSupply) = "0.076"
            frmAPS6062.txtVoltage.Text = SetCurVolt(EditSupply)
            frmAPS6062.txtCurrent.Text = SetCurCurr(EditSupply)

            'Set / Re-Set Displays To Ready States
            frmAPS6062.txtSenseDisplayA.Text = "Ready"
            frmAPS6062.txtSenseDisplayV.Text = "Ready"
            EnableCommunication = True
            'PS1 Cannot be a slave
            frmAPS6062.optModeSlave.Enabled = False

            EnableCommunication = True
            frmAPS6062.Enabled = False

            'Open relay(s) in reverse sequence of application
            If (ResetAllSequence <> Nothing) Then
                Dim ResetSupplies() As String = ResetAllSequence.Split(",")
                For i As Integer = ResetSupplies.Length - 2 To 0 Step -1
                    SupplyIndex = ResetSupplies(i)
                    SendStatusBarMessage("Resetting Supply " & SupplyIndex.ToString() & " In Reverse Sequence")
                    Delay(0.2)
                    WaitForReady(SupplyIndex)
                    If (bytModeByte <> &H20) Or (bytStatusByte <> &H80) Then
                        TestCommandData = Convert.ToString(Chr(&H10 + SupplyIndex)) & Convert.ToString(Chr(0)) & Convert.ToString(Chr(0))
                        ErrorStatus = atxmlDF_viWrite(SignalResourceNameArray(SupplyIndex), 0, TestCommandData, CLng(Len(TestCommandData)), NBR)

                        If ErrorStatus <> VI_SUCCESS Then
                            MsgBox("Error sending Status Query to DCPS " & SupplyIndex & ", Error code: " & Hex(ErrorStatus))
                        End If
                    End If

                Next i
            End If

            'Reset All PPU Supplies
            SendStatusBarMessage("Resetting All Supplies")
            For SupplyIndex = 10 To 1 Step -1
                'Init/Reset Supplies
                TestCommandData$ = Chr(&H10 + SupplyIndex) & Chr(0) & Chr(0)
                ErrorStatus = atxmlDF_viWrite(SignalResourceNameArray(SupplyIndex), 0, TestCommandData$, CLng(Len(TestCommandData$)), NBR)

                If ErrorStatus <> VI_SUCCESS Then
                    MsgBox("Error sending fast Reset to DCPS " & SupplyIndex & _
                        ", Error code: " & Hex$(ErrorStatus))
                End If
            Next SupplyIndex

            'Wait until the first commanded supply is finished resetting
            WaitForReady(iSupply:=10)

            For SupplyIndex = 10 To 1 Step -1
                SendStatusBarMessage("Resetting All Supply " & SupplyIndex.ToString())
                Delay(0.2)
                WaitForReady(SupplyIndex)
                If (bytModeByte <> &H20) Or (bytStatusByte <> &H80) Then
                    TestCommandData = Convert.ToString(Chr(&H10 + SupplyIndex)) & Convert.ToString(Chr(0)) & Convert.ToString(Chr(0))
                    ErrorStatus = atxmlDF_viWrite(SignalResourceNameArray(SupplyIndex), 0, TestCommandData, CLng(Len(TestCommandData)), NBR)

                    If ErrorStatus <> VI_SUCCESS Then
                        MsgBox("Error sending Status Query to DCPS " & SupplyIndex & ", Error code: " & Hex(ErrorStatus))
                    End If
                End If
            Next SupplyIndex

            SendStatusBarMessage("Releasing DCPS")
            ReleaseDCPS()
        End If

        'clear reset all list
        ResetAllSequence = ""

        EditSupply = 1
        frmAPS6062.Enabled = True
        frmAPS6062.Cursor = Cursors.Default
        Application.DoEvents()
        SendStatusBarMessage("Supply 1 Selected")
    End Sub

    Sub ResetPowerSupply(ByVal OneSingleSupply As Short)
        Dim i As Short
        Dim Cnt As Short
        If LiveMode Then
            'mh Screen.MousePointer = vbHourglass
            If frmAPS6062.Panel_Conifg.DebugMode = True Then Exit Sub

            SendStatusBarMessage("Resetting Supply" & OneSingleSupply)

            'Clear Supply Settings to Default
            EnableCommunication = False
            frmAPS6062.lblVolt(OneSingleSupply).Text = "0.00"
            frmAPS6062.lblMeas(OneSingleSupply).Text = "0.00"
            SupplyData(OneSingleSupply).FuncVoltVal = "00.00"
            SupplyData(OneSingleSupply).MeasCurr = "0.00"
            SupplyData(OneSingleSupply).MeasVolt = "0.00"
            SupplyData(OneSingleSupply).sCmdVolt = "2" & Hex(OneSingleSupply) & ",50,00"
            SupplyData(OneSingleSupply).FuncAmpVal = "0.076"
            SupplyData(OneSingleSupply).sCmdAmp = "2" & Hex(OneSingleSupply) & ",40,26"
            SupplyData(OneSingleSupply).Master = True
            SupplyData(OneSingleSupply).SensLocal = True
            SupplyData(OneSingleSupply).FuncRelayClosed = False
            SupplyData(OneSingleSupply).CurProt = True
            SupplyData(OneSingleSupply).RevPol = False
            SupplyData(OneSingleSupply).sCmdOpt = "2" & Hex(OneSingleSupply) & ",AA,22"
            frmAPS6062.cwbRelay(OneSingleSupply - 1).Value = False

            'Reset GUI Buttons
            frmAPS6062.optModeMaster.Checked = True
            frmAPS6062.optSenseLocal.Checked = True
            frmAPS6062.optRelayOpen.Checked = True
            frmAPS6062.optCurProtConstantVoltage.Checked = True
            frmAPS6062.optPolarityNormal.Checked = True
            frmAPS6062.fraMode.Visible = True
            frmAPS6062.fraSense.Visible = True
            frmAPS6062.fraOutReg.Visible = True
            frmAPS6062.fraVoltage.Visible = True
            frmAPS6062.fraCurrent.Visible = True
            frmAPS6062.optCurProtConstantCurrent.Enabled = True
            frmAPS6062.optRelayOpen.Enabled = True
            frmAPS6062.optModeMaster.Enabled = True
            frmAPS6062.optPolarityNormal.Enabled = True
            frmAPS6062.optPolarityReverse.Enabled = True

            'Reset Voltage / Current Text Boxes
            SetCurVolt(OneSingleSupply) = "00.00"
            SetCurCurr(OneSingleSupply) = "0.076"
            SetMinVolt(OneSingleSupply) = 0
            SetMinCurr(OneSingleSupply) = 0
            frmAPS6062.txtVoltage.Text = SetCurVolt(OneSingleSupply)
            frmAPS6062.txtCurrent.Text = SetCurCurr(OneSingleSupply)
            frmAPS6062.lblVolt(OneSingleSupply).ForeColor = SystemColors.ControlText
            frmAPS6062.lblVolt(OneSingleSupply).BackColor = SystemColors.Control

            'Set / Re-Set Displays To Ready States
            frmAPS6062.txtSenseDisplayA.Text = "Ready"
            frmAPS6062.txtSenseDisplayV.Text = "Ready"
            SendStatusBarMessage("Supply " & Str(OneSingleSupply) & " Selected")
            EnableCommunication = True
            'PS1 Cannot be a slave
            If OneSingleSupply = 1 Then
                frmAPS6062.optModeSlave.Enabled = False
            End If

            EnableCommunication = True
            'When master or slave is reset, also reset all slaves to the right
            For i = (OneSingleSupply + 1) To 10 Step 1
                If SupplyData(i).Master = False Then
                    Cnt += 1
                Else
                    Exit For
                End If
            Next i

            'Start resetting at last slave in the chain and continue until the current supply
            i -= 1
            For Cnt = i To OneSingleSupply Step -1
                'Init/Reset Supply
                SetMinVolt(i) = 0
                SetMinCurr(i) = 0
                frmAPS6062.lblVolt(Cnt).Text = "0.00"
                frmAPS6062.lblMeas(Cnt).Text = "0.00"
                SupplyData(Cnt).FuncVoltVal = "00.00"
                SupplyData(Cnt).MeasCurr = "0.00"
                SupplyData(Cnt).MeasVolt = "0.00"
                SupplyData(Cnt).FuncAmpVal = "0.076"
                SupplyData(Cnt).Master = True
                SupplyData(Cnt).SensLocal = True
                SupplyData(Cnt).FuncRelayClosed = False
                SupplyData(Cnt).CurProt = True
                SupplyData(Cnt).RevPol = False
                frmAPS6062.cwbRelay(Cnt - 1).Value = False
                frmAPS6062.optRelayOpen.Checked = True
                frmAPS6062.optRelayClosed.Checked = False
                CommandSupplyReset(Cnt)
            Next Cnt
            'Wait for hardware reset to finish
            WaitForReady(OneSingleSupply)

            frmAPS6062.Cursor = Cursors.Default
        End If
    End Sub

    Sub RunTipCmds()
        'DESCRIPTION:
        '   This procedure takes a previously-stored DCPS command string, sets up the
        '   DCPS GUI to match those commands and programs the DCPS with this command string.
        'PARAMETERS:    None
        'RETURNS:       None
        'GLOBAL VARIABLES USED:
        '   sTipCmds
        'EXAMPLE:
        '   RunTipCmds
        Dim sCmds() As String = {""}
        Dim i As Short
        Dim iMaster As Short
        Dim iSlave As Short

        SendStatusBarMessage("Please wait, Setting to new configuration")
        frmAPS6062.Cursor = Cursors.WaitCursor
        bLoadingTipCmds = True
        Application.DoEvents()
        frmAPS6062.tabUserOptions.SelectedIndex = 0 'Functions

        sMeasVal = ""
        For i = 1 To iStringToList(sTipCmds, 1, sCmds, ";")
            If  Not bActivateControl(sCmds(i)) Then Exit For
        Next i

        'Resolve functional current settings for Masters with Slaves
        For iMaster = 1 To 8 'Only supplies 1 thru 8 can be masters
            i = 1 'Init factor
            For iSlave = iMaster+1 To 9 'Only supplies 2 thru 9 can be slaves
                If SupplyData(iSlave).Master=False Then 'If a Slave
                    i += 1 'Increment factor
                Else
                    Exit For
                End If
            Next iSlave
            SupplyData(iMaster).FuncAmpVal = Truncate(SetCurCurr(iMaster) * i).ToString(SetRes(CURRENT))
            SetCurCurr(iMaster) = SupplyData(iMaster).FuncAmpVal
            SetMaxCurr(iMaster) = CStr(5*i)
            iMaster = iSlave-1 'Jump to next supply after last slave, if any
        Next iMaster

        If sMeasVal="" Then 'If not a measurement step
            frmAPS6062.tbrUutPuFunctions_ButtonClick(frmAPS6062.tbrUutPuFunctions.Buttons(EditSupply - 1))
            frmAPS6062.tbrUutPuFunctions.Buttons(EditSupply - 1).Pushed = True
        End If

        bLoadingTipCmds = False
        frmAPS6062.Cursor = Cursors.Default
        SendStatusBarMessage("")

        'Clear CMD key to prevent subsequent executions during monitor
        SetKey("TIPS", "CMD", "")
        Application.DoEvents()
    End Sub

    Sub SendStatusBarMessage(ByVal MessageString As String)
        '************************************************************
        '* Nomenclature   : 7081-PPU                                *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     This Module displays MessageString$ in the Instrument*
        '*      Status bar.                                         *
        '*    EXAMPLE:                                              *
        '*     SendStatusBarMessage "Select Option"                 *
        '************************************************************
        'MessageString$ is printed in the Instrument Status Bar
        If frmAbout.Visible = True Then
            frmAbout.lblStatus.Text = Convert.ToString(MessageString)
        Else
            frmAPS6062.sbrUserInformation.Panels(1 - 1).Text = Convert.ToString(MessageString)
        End If
    End Sub

    Sub SetKey(ByRef sSection As String, ByVal sKey As String, ByVal sKeyVal As String)
        'DESCRIPTION:
        '   This function sets a key value in VIPERT.INI
        'PARAMETERS:
        '   sSection    The VIPERT.INI file section where the key is located.
        '   sKey        The Key name.
        '   sKeyVal     The string to set the key value to.
        'RETURNS:   None
        'GLOBAL VARIABLES USED:
        '   sWindowsDir
        'EXAMPLE:
        '   SetKey "TIPS", "STATUS", "Ready"
        Dim lpFileName As String 'INI File Key Name "Key=?"

        'Get the ini file location from the Registry
        lpFileName = Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\ATS\", "IniFilePath", Nothing)

        WritePrivateProfileString(sSection, sKey, sKeyVal, lpFileName)
    End Sub

    Function sGetKey(ByVal sSection As String, ByVal sKey As String) As String
        sGetKey = ""
        'DESCRIPTION:
        '   This function retrieves a key value from VIPERT.INI
        'PARAMETERS:
        '   sSection    The VIPERT.INI file section where the key is located.
        '   sKey        The Key name.
        'RETURNS:
        '   The key value.
        'GLOBAL VARIABLES USED:
        '   sWindowsDir
        'EXAMPLE:
        '   sTipCmds = sGetKey("TIPS", "CMD")

        'Clear String Buffer
        lpString = Space(4000)
        Dim lpFileName As String 'INI File Key Name "Key=?"

        'Get the ini file location from the Registry
        lpFileName = Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\ATS\", "IniFilePath", Nothing)

        nNumChars = GetPrivateProfileString(sSection, sKey, "", lpString, lpString.Length, lpFileName)
        sGetKey = Strings.Left(lpString, nNumChars)
    End Function

    Function sGetTipCmds(ByVal sMeasType As String) As String
        sGetTipCmds = ""
        'DESCRIPTION:
        '   This function creates a complete command string
        '   that represents the current state of the SAIS and PPU.
        'PARAMETERS:
        '   sMeasType:  "" for non-measurement TIP step
        '               "Mtn" for measurement TIP step, where
        '                   t = "V" for volts and "A" for amps, and
        '                   n = the DCPS number
        'RETURNS:
        '   A string variable containing the command string.
        'EXAMPLE:
        '   sTipCmds = sGetTipCmds()
        Dim i As Short

        SendStatusBarMessage("Please wait, Accumulating commands")
        frmAPS6062.Cursor = Cursors.WaitCursor
        Application.DoEvents()

        sGetTipCmds = ""

        For i = 10 To 1 Step -1
            'Check for a reset state
            If (Mid(SupplyData(i).sCmdAmp, 4)="40,26") And (Mid(SupplyData(i).sCmdOpt, 4)="AA,22") And (Mid(SupplyData(i).sCmdVolt, 4)="50,00") Then
                sGetTipCmds = sGetTipCmds & "Reset_" & CStr(i) & ";"
            Else
                sGetTipCmds = sGetTipCmds & SupplyData(i).sCmdAmp & ";"
                sGetTipCmds = sGetTipCmds & SupplyData(i).sCmdOpt & ";"
                sGetTipCmds = sGetTipCmds & SupplyData(i).sCmdVolt & ";"
            End If
        Next i
        If sMeasType<>"" Then
            sGetTipCmds = sGetTipCmds & sMeasType
        Else            'Else, strip the last ";"
            sGetTipCmds = Strings.Left(sGetTipCmds, Len(sGetTipCmds)-1)
        End If

        frmAPS6062.Cursor = Cursors.Default
        SendStatusBarMessage("")

        Application.DoEvents()
    End Function

    Sub CenterForm(ByVal Form As Object)
        '************************************************************
        '* ManTech Test Systems Software Module                     *
        '************************************************************
        '* Nomenclature   : 7081-PPU                                *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     This Module Centers One Form With Respect To The     *
        '*     User's Screen.                                       *
        '*    EXAMPLE:                                              *
        '*     CenterForm frmMain                                   *
        '************************************************************
        Form.Top = PrimaryScreen.Bounds.Height / 2 - Form.Height / 2
        Form.Left = PrimaryScreen.Bounds.Width / 2 - Form.Width / 2
    End Sub

    Function VerifyInstrumentCommunication() As Boolean
        '************************************************************
        '* ManTech Test Systems Software Module                     *
        '************************************************************
        '* Nomenclature   : 7081-PPU                                *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     Verifies Communication With Message Delivery System  *
        '*    EXAMPLE:                                              *
        '*      LiveMode% = VerifyInstrumentCommunication%          *
        '************************************************************
        Dim Pass As Boolean 'Instrument Pass Test (True/False)
        Dim SystemDir As String
        Dim status As Integer
        Dim XmlBuf As String
        Dim Allocation As String
        Dim Response As String

        Pass = False 'Until Proven True
        ErrorStatus = 1
        'Check For THe Resource Manager
        SystemDir = Environment.SystemDirectory

        XmlBuf = Space(4096)
        Response = Space(4096)

        SignalResourceNameArray(1) = "DCPS_1"
        SignalResourceNameArray(2) = "DCPS_2"
        SignalResourceNameArray(3) = "DCPS_3"
        SignalResourceNameArray(4) = "DCPS_4"
        SignalResourceNameArray(5) = "DCPS_5"
        SignalResourceNameArray(6) = "DCPS_6"
        SignalResourceNameArray(7) = "DCPS_7"
        SignalResourceNameArray(8) = "DCPS_8"
        SignalResourceNameArray(9) = "DCPS_9"
        SignalResourceNameArray(10) = "DCPS_10"

        XmlBuf = "<AtXmlTestRequirements>"
        XmlBuf &= "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & "  <SignalResourceName>DCPS_1</SignalResourceName> " & "</ResourceRequirement> "
        XmlBuf &= "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & "  <SignalResourceName>DCPS_2</SignalResourceName> " & "</ResourceRequirement>"
        XmlBuf &= "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & "  <SignalResourceName>DCPS_3</SignalResourceName> " & "</ResourceRequirement>"
        XmlBuf &= "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & "  <SignalResourceName>DCPS_4</SignalResourceName> " & "</ResourceRequirement>"
        XmlBuf &= "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & "  <SignalResourceName>DCPS_5</SignalResourceName> " & "</ResourceRequirement>"
        XmlBuf &= "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & "  <SignalResourceName>DCPS_6</SignalResourceName> " & "</ResourceRequirement>"
        XmlBuf &= "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & "  <SignalResourceName>DCPS_7</SignalResourceName> " & "</ResourceRequirement>"
        XmlBuf &= "<ResourceRequirement> " & "       <ResourceType>Source</ResourceType> " & "       <SignalResourceName>DCPS_8</SignalResourceName> " & "</ResourceRequirement>"
        XmlBuf &= "<ResourceRequirement> " & "       <ResourceType>Source</ResourceType> " & "       <SignalResourceName>DCPS_9</SignalResourceName> " & "</ResourceRequirement>"
        XmlBuf &= "<ResourceRequirement> " & "       <ResourceType>Source</ResourceType> " & "       <SignalResourceName>DCPS_10</SignalResourceName> " & "</ResourceRequirement>"
        XmlBuf &= "</AtXmlTestRequirements>"

        status = atxml_Initialize(ProcType, guid)

        Allocation = Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\ATS\", "PAWSAllocationPath", Nothing)

        status = atxml_ValidateRequirements(XmlBuf, Allocation, Response, Response.Length)

        'Parse Availability XML string to get the status(Mode) of the instrument
        iStatus = gctlParse.ParseAvailiability(Response, True)

        If status Then
            MsgBox("The PPU is not responding.  Live mode is disabled.", MsgBoxStyle.Exclamation)
            Pass = False
        Else
            Pass = True
        End If

        VerifyInstrumentCommunication = Pass
    End Function

    Function StripNullCharacters(ByVal Parsed As String) As String
        StripNullCharacters = ""
        '************************************************************
        '* Nomenclature   : 7081-PPU                                *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     This Module removes null characters from a string    *
        '*    EXAMPLE:                                              *
        '*     SystemDir$ = StripNullCharacters(lpBuffer$)          *
        '************************************************************
        '32 - 126
        Dim X As Integer
        For X = 1 To Convert.ToString(Parsed).Length
            If Asc(Mid(Convert.ToString(Parsed), X, 1)) < 32 Then
                Exit For
            End If
        Next X
        StripNullCharacters = Strings.Left(Convert.ToString(Parsed), X - 1)
    End Function

    Sub CommandMeasure(ByVal Supply As Short, ByVal sMeasType As String)
        '************************************************************
        '* ManTech Test Systems Software Module                     *
        '************************************************************
        '* Nomenclature   : 7081-PPU                                *
        '* Written By     : Randy Zeger                             *
        '*    DESCRIPTION:                                          *
        '*     Takes voltage and current measurements from the      *
        '*      selected supply and displays them on the measurement*
        '*      tab.                                                *
        '*    EXAMPLE:                                              *
        '*       CommandMeasure EditSupply%                         *
        '************************************************************
        Dim B1 As Short
        Dim B2 As Short
        Dim B3 As Short
        Dim D1 As Single
        Dim D2 As Single
        Dim ReadByte As Short
        Dim Data As String
        Dim ReadBufferV As String = ""
        Dim ReadBufferA As String = ""

        'Disable Measure Button During Measure
        frmAPS6062.cmdMeasure.Enabled = False
        frmAPS6062.Cursor = Cursors.Default
        do
            'Program Query Supply For Sense Measurement
            B1 = Supply
            B2 = &H42
            B3 = &H0
            'Send Command
            SendGpibCommand(Supply, Convert.ToString(Chr(B1)) & Convert.ToString(Chr(B2)) & Convert.ToString(Chr(B3)))
            'Read Sense Measurement
            Data = ReadInstrumentBuffer(Supply)
            'Format The Display Units According To Instrument Mode
            'Voltage Measurement
            'only update display if valid data is returned (not zeros)
            If Data="" Then Data = " "
            If Asc(Mid(Data, 1, 1))>64 Then
                D1 = (Asc(Mid(Data, 3, 1))) And 15
                D1 *= 256
                D2 = (Asc(Mid(Data, 4, 1)))
                'V1.6 Added If-Then to check for and display negative measurements
                If ((Asc(Mid(Data, 3, 1))) And 128)=128 Then
                    ReadBufferV = -(Convert.ToDouble((D1 + D2) / 100.0))
                Else
                    ReadBufferV = Convert.ToDouble((D1 + D2) / 100.0)
                End If
                'Current Measurement
                'DisplayUnit$ = "Amps"
                D1 = (Asc(Mid(Data, 1, 1))) And 15
                D1 *= 256
                D2 = (Asc(Mid(Data, 2, 1)))
                ReadBufferA = Convert.ToDouble((D1 + D2) / 500.0)
                'Format And Display Measurement

                '65 Volt Supply Re-Scale Voltage Here
                If Supply = 10 Then
                    D1 = (Asc(Mid(Data, 3, 1))) And 15
                    D1 *= 256
                    D2 = (Asc(Mid(Data, 4, 1)))
                    'V1.6 Added If-Then to check for and display negative measurements
                    If ((Asc(Mid(Data, 3, 1))) And 128) = 128 Then
                        ReadBufferV = -(Convert.ToDouble((D1 + D2) / 50.0))
                    Else
                        ReadBufferV = Convert.ToDouble((D1 + D2) / 50.0)
                    End If
                End If

                If (Len(ReadBufferV) >= 1) Then
                    frmAPS6062.txtSenseDisplayV.Text = EngNotate(Val(ReadBufferV), 2, "Volts")
                    SupplyData(Supply).MeasVolt = Convert.ToDouble(ReadBufferV).ToString("#0.0#")
                    frmAPS6062.lblMeas(Supply).Text = SupplyData(Supply).MeasVolt
                Else
                    frmAPS6062.txtSenseDisplayV.Text = "Out Of Range"
                    SupplyData(Supply).MeasVolt = "0.00"
                    frmAPS6062.lblMeas(Supply).Text = SupplyData(Supply).MeasVolt
                End If

                If (Len(ReadBufferA) >= 1) Then
                    frmAPS6062.txtSenseDisplayA.Text = EngNotate(Val(ReadBufferA), 2, "Amps")
                    SupplyData(Supply).MeasCurr = Convert.ToDouble(ReadBufferA).ToString("#0.0#")
                Else
                    frmAPS6062.txtSenseDisplayA.Text = "Out Of Range"
                    SupplyData(Supply).MeasCurr = "0.00"
                End If
                'Clear If Error Or Zero Value
            Else
                On Error Resume Next
                If ReadBufferV = "" Then
                    frmAPS6062.txtSenseDisplayV.Text = "Ready"
                    SupplyData(Supply).MeasVolt = "0.00"
                    frmAPS6062.lblMeas(Supply).Text = SupplyData(Supply).MeasVolt
                End If

                If ReadBufferA = "" Then
                    frmAPS6062.txtSenseDisplayA.Text = "Ready"
                    SupplyData(Supply).MeasCurr = "0.00"
                End If
            End If

            If sTipMode = "TIP_RunPersist" Then
                If frmAPS6062.sbrUserInformation.Panels(1 - 1).Text = "" Then
                    SendStatusBarMessage("UNCHECK 'Continuous' TO CONTINUE")
                Else
                    SendStatusBarMessage("")
                End If

                Delay(0.25)
            End If
            Application.DoEvents()
        Loop Until frmAPS6062.chkContinuous.Checked = False

        frmAPS6062.cmdMeasure.Enabled = True
        If sMeasType="V" Then
            sMeasVal = ReadBufferV
        ElseIf sMeasType="A" Then
            sMeasVal = ReadBufferA
        End If

        If sMeasVal="" And sMeasType<>"" Then
            sMeasVal = "Unknown " & sMeasType
        End If
    End Sub

    Sub CalADC(ByVal Supply As Short)
        '************************************************************
        '* ManTech Test Systems Software Module                     *
        '************************************************************
        '* Nomenclature   : 7081-PPU                                *
        '* Written By     : Randy Zeger                             *
        '*    DESCRIPTION:  (Maintenance Only)                      *
        '*     Calibrates A to D converter on the selected supply   *
        '*    EXAMPLE:                                              *
        '*       CalADC EditSupply%                                 *
        '************************************************************
        Dim B1 As Short
        Dim B2 As Short
        Dim B3 As Short

        frmAPS6062.Cursor = Cursors.WaitCursor
        B1 = Supply
        B2 = 64
        B3 = 32

        'Send Command
        SendGpibCommand(Supply, Convert.ToString(Chr(B1)) & Convert.ToString(Chr(B2)) & Convert.ToString(Chr(B3)))
        Delay(8) 'For Cal to complete

        B1 = Supply + &H10 'Reset
        B2 = 1
        B3 = 1

        'Send Command
        SendGpibCommand(Supply, Convert.ToString(Chr(B1)) & Convert.ToString(Chr(B2)) & Convert.ToString(Chr(B3)))
        Delay(5)

        frmAPS6062.Cursor = Cursors.Default
    End Sub

    Sub CmdClearCalConstants(ByVal Supply As Short)
        '************************************************************
        '* ManTech Test Systems Software Module                     *
        '************************************************************
        '* Nomenclature   : 7081-PPU                                *
        '* Written By     : Randy Zeger                             *
        '*    DESCRIPTION:  (Maintenance Only)                      *
        '*     Clears out all calibration constants on the selected *
        '*      supply                                              *
        '*    EXAMPLE:                                              *
        '*       CmdClearCalConstants EditSupply%                   *
        '************************************************************
        Dim B1 As Short
        Dim B2 As Short
        Dim B3 As Short

        frmAPS6062.Cursor = Cursors.WaitCursor
        B1 = &HA0 + Supply
        B2 = &H40 'starting location of 0
        B3 = 1

        'Send Command
        SendGpibCommand(Supply, Convert.ToString(Chr(B1)) & Convert.ToString(Chr(B2)) & Convert.ToString(Chr(B3)))

        Delay(0.3)

        B1 = &HC0 + Supply
        B2 = &H4F 'ending location of FFF
        B3 = &HFF

        'Send Command
        SendGpibCommand(Supply, Convert.ToString(Chr(B1)) & Convert.ToString(Chr(B2)) & Convert.ToString(Chr(B3)))

        Delay(0.3)

        B1 = &HE0 + Supply
        B2 = &HE0 'offset of 0
        B3 = 1

        'Send Command
        SendGpibCommand(Supply, Convert.ToString(Chr(B1)) & Convert.ToString(Chr(B2)) & Convert.ToString(Chr(B3)))

        Delay(15)

        B1 = &H90 + Supply
        B2 = 1 'done
        B3 = 1

        'Send Command
        SendGpibCommand(Supply, Convert.ToString(Chr(B1)) & Convert.ToString(Chr(B2)) & Convert.ToString(Chr(B3)))
        frmAPS6062.Cursor = Cursors.Default
    End Sub

    Sub CommandInternalCal(ByVal Supply As Short)
        '************************************************************
        '* ManTech Test Systems Software Module                     *
        '************************************************************
        '* Nomenclature   : 7081-PPU                                *
        '* Written By     : Randy Zeger                             *
        '*    DESCRIPTION:  (Maintenance Only)                      *
        '*     Calibrates the selected supply using the internal A/D*
        '*      as the reference.  Only useful as an initial cal    *
        '*      to remove the random data stored in the EEPROM      *
        '*    EXAMPLE:                                              *
        '*       CommandInternalCal EditSupply%                     *
        '************************************************************
        'takes about 35 Sec to complete
        Dim B1 As Short
        Dim B2 As Short
        Dim B3 As Short

        frmAPS6062.Cursor = Cursors.WaitCursor
        B1 = Supply
        B2 = &H48
        B3 = 1

        'Send Command
        SendGpibCommand(Supply, Convert.ToString(Chr(B1)) & Convert.ToString(Chr(B2)) & Convert.ToString(Chr(B3)))
        Delay(30)
        ResetPowerSupply(Supply)
        Delay(5)
        frmAPS6062.Cursor = Cursors.Default
    End Sub

    Sub NoFaultPowerSupply(ByVal Supply As Short)
        '************************************************************
        '* Nomenclature   : 7081-PPU                                *
        '* Written By     : Randy Zeger                             *
        '*    DESCRIPTION:  (Maintenance Only)                      *
        '*     Used to clear a faulted condition and prevent fault  *
        '*      checking                                            *
        '*    EXAMPLE:                                              *
        '*     NoFaultPowerSupply EditSupply%                       *
        '************************************************************

        Dim B1 As Short
        Dim B2 As Short
        Dim B3 As Short

        B1 = &H20 + Supply
        B2 = &H80
        B3 = &H40

        'Send Command
        SendGpibCommand(Supply, Convert.ToString(Chr(B1)) & Convert.ToString(Chr(B2)) & Convert.ToString(Chr(B3)))
    End Sub

    Public Sub cmdEEPROMRead(ByVal Address As Short)
        '************************************************************
        '* ManTech Test Systems Software Module                     *
        '************************************************************
        '* Nomenclature   : 7081-PPU                                *
        '* Written By     : Randy Zeger                             *
        '*    DESCRIPTION:  (Maintenance Only)                      *
        '*     Reads a byte of data from the specified address of   *
        '*      the EEPROM of the supply at EditSupply%             *
        '*    EXAMPLE:                                              *
        '*       cmdEEPROMRead 4000                                 *
        '************************************************************

        Dim B1 As Short
        Dim B2 As Short
        Dim B3 As Short
        Dim MSNAddress As Short
        Dim LSBAddress As Short
        Dim Data As String

        MSNAddress = (Address \ 256)
        LSBAddress = Address Mod 256
        B1 = EditSupply
        B2 = MSNAddress + &H60
        B3 = LSBAddress

        'Send Command
        SendGpibCommand(EditSupply, Convert.ToString(Chr(B1)) & Convert.ToString(Chr(B2)) & Convert.ToString(Chr(B3)))

        Delay(0.2)

        'get Status Query
        B1 = EditSupply
        B2 = &H44
        B3 = 0

        'Send Command
        SendGpibCommand(EditSupply, Convert.ToString(Chr(B1)) & Convert.ToString(Chr(B2)) & Convert.ToString(Chr(B3)))

        Delay(0.2)

        Data = ReadInstrumentBuffer(EditSupply)
        frmAPS6062.txtEEReadData.Text = Asc(Mid(Data, 5, 1))
    End Sub

    Public Sub cmdEEPROMWriteAdd(ByVal Address As Short)
        '************************************************************
        '* ManTech Test Systems Software Module                     *
        '************************************************************
        '* Nomenclature   : 7081-PPU                                *
        '* Written By     : Randy Zeger                             *
        '*    DESCRIPTION:  (Maintenance Only)                      *
        '*     Sends the address of the next write to EEPROM command*
        '*      cmdEEPROMWriteData is used to send the data byte    *
        '*      address = 0-8191                                    *
        '*    EXAMPLE:                                              *
        '*       cmdEEPROMWriteAdd 4000                             *
        '************************************************************
        Dim B1 As Short
        Dim B2 As Short
        Dim B3 As Short
        Dim MSNAddress As Short
        Dim LSBAddress As Short

        MSNAddress = (Address \ 256)
        LSBAddress = Address Mod 256
        B1 = &H80 + EditSupply
        B2 = MSNAddress + &H40
        B3 = LSBAddress

        'Send Command
        SendGpibCommand(EditSupply, Convert.ToString(Chr(B1)) & Convert.ToString(Chr(B2)) & Convert.ToString(Chr(B3)))
    End Sub

    Public Sub cmdEEPROMWriteData(ByVal Data As Short)
        '************************************************************
        '* ManTech Test Systems Software Module                     *
        '************************************************************
        '* Nomenclature   : 7081-PPU                                *
        '* Written By     : Randy Zeger                             *
        '*    DESCRIPTION:  (Maintenance Only)                      *
        '*     Sends a data byte to be stored in EEPROM.  Needs to  *
        '*      be preceeded with a cmdEEPROMWriteAdd to specify the*
        '*      address.               Data = 0-255                 *
        '*    EXAMPLE:                                              *
        '*       cmdEEPROMWriteData 255                             *
        '************************************************************
        Dim B1 As Short
        Dim B2 As Short
        Dim B3 As Short

        B1 = &H80 + EditSupply
        B2 = &H60
        B3 = Data

        'Send Command
        SendGpibCommand(EditSupply, Convert.ToString(Chr(B1)) & Convert.ToString(Chr(B2)) & Convert.ToString(Chr(B3)))
    End Sub

    Public Function bytCommandReadStatus(ByVal Supply As Short) As Byte
        'DESCRIPTION:
        '   This function performs a status query on a supply. It also populates
        '   the status display with the results (maintenance mode only).
        'PARAMETERS:
        '   Supply%:    The power supply to query.
        'RETURNS:
        '   Byte 1 of the query return (the Mode byte)
        'GLOBAL VARIABLES USED:
        '   bytModeByte:      Contains byte 1 of the query return
        '   bytStatusByte:    Contains byte 2 of the query return
        Dim B1 As Short
        Dim B2 As Short
        Dim B3 As Short
        Dim Data As String = ""

        ' added to store each char temp

        'get Status Query
        B1 = Supply
        B2 = &H44
        B3 = 0

        bytModeByte = 0
        bytStatusByte = 0

        'Send Command
        SendGpibCommand(Supply, Convert.ToString(Chr(B1)) & Convert.ToString(Chr(B2)) & Convert.ToString(Chr(B3)))
        'Read Sense Measurement
        Data = ReadInstrumentBuffer(Supply)

        If Len(Data) < 5 Then Return bytModeByte

        'Display Data
        frmAPS6062.txtStatusBox.Text = Hex(Asc(Mid(Data, 1, 1))) & "   Mode" & vbCrLf & Hex(Asc(Mid(Data, 2, 1))) & "   Status" & vbCrLf & Hex(Asc(Mid(Data, 3, 1))) & "   Self Test" & vbCrLf & Hex(Asc(Mid(Data, 4, 1))) & "   Rev." & vbCrLf & Hex(Asc(Mid(Data, 5, 1))) & "   EEPROM" & vbCrLf

        bytModeByte = CByte(Asc(Strings.Left(Data, 1)))
        bytStatusByte = CByte(Asc(Mid(Data, 2, 1)))
        bytCommandReadStatus = bytModeByte
    End Function

    Public Sub CommandSNRead(ByVal Supply As Short)
        '************************************************************
        '* ManTech Test Systems Software Module                     *
        '************************************************************
        '* Nomenclature   : 7081-PPU                                *
        '* Written By     : Randy Zeger                             *
        '*    DESCRIPTION:  (Maintenance Only)                      *
        '*     Reads the serial number of the selected supply       *
        '*      stored in EEPROM                                    *
        '*    EXAMPLE:                                              *
        '*       CommandSNRead EditSupply%                          *
        '************************************************************
        'S/N stored at 1FF7-1FFE 7 digits represented by ASCII equiv.

        Dim B1 As Short
        Dim B2 As Short
        Dim B3 As Short
        Dim Data As String
        Dim DispData As String = ""
        Dim LSN As Short

        For LSN = &HF7 To &HFD Step 1
            B1 = Supply
            B2 = &H1F+&H60
            B3 = LSN

            'Send Command
            SendGpibCommand(Supply, Convert.ToString(Chr(B1)) & Convert.ToString(Chr(B2)) & Convert.ToString(Chr(B3)))

            'get Status Query
            B1 = Supply
            B2 = &H44
            B3 = 0

            'Send Command
            SendGpibCommand(Supply, Convert.ToString(Chr(B1)) & Convert.ToString(Chr(B2)) & Convert.ToString(Chr(B3)))

            'collect data
            Data = ReadInstrumentBuffer(Supply)
            DispData &= Mid(Data, 5, 1)
        Next LSN

        frmAPS6062.txtCurrentSN.Text = DispData
    End Sub

    Public Sub CommandSNWrite(ByVal NewSN As String)
        '************************************************************
        '* ManTech Test Systems Software Module                     *
        '************************************************************
        '* Nomenclature   : 7081-PPU                                *
        '* Written By     : Randy Zeger                             *
        '*    DESCRIPTION:  (Maintenance Only)                      *
        '*     Used to write 7 digit serial number to EEPROM        *
        '*    EXAMPLE:                                              *
        '*       CommandSNWrite 7110003                             *
        '************************************************************
        Dim B1 As Short
        Dim B2 As Short
        Dim B3 As Short
        Dim Data As String
        Dim i As Short

        'Address of S/N
        B1 = &H80+EditSupply
        B2 = &H1F+&H40
        B3 = &HF7

        'Send Command
        SendGpibCommand(EditSupply, Convert.ToString(Chr(B1)) & Convert.ToString(Chr(B2)) & Convert.ToString(Chr(B3)))

        If (NewSN <> "") And (NewSN.Length = 7) Then
            For i = 1 To 7 Step 1
                Data = Asc(Mid(Convert.ToString(NewSN), i, 1))
                B1 = &H80 + EditSupply
                B2 = &H60
                B3 = Data

                'Send Command
                SendGpibCommand(EditSupply, Convert.ToString(Chr(B1)) & Convert.ToString(Chr(B2)) & Convert.ToString(Chr(B3)))
            Next i
        End If
    End Sub

    Public Sub CommandReadCalDate()
        '************************************************************
        '* ManTech Test Systems Software Module                     *
        '************************************************************
        '* Nomenclature   : 7081-PPU                                *
        '* Written By     : Randy Zeger                             *
        '*    DESCRIPTION:  (Maintenance Only)                      *
        '*     Reads the last Cal date from EEPROM and displays it  *
        '*      on the Options tab.  Performed on EditSupply%       *
        '*      Format Bytes 1 and 2= year, Byte 3=Month, Byte 4=Day*
        '*      11/20/1997 = 19, 97, 11, 20                         *
        '*    EXAMPLE:                                              *
        '*       commandReadCalDate                                 *
        '************************************************************
        Dim B1 As Short
        Dim B2 As Short
        Dim B3 As Short
        Dim i As Short
        Dim Data As String
        Dim CalDate As String = ""

        For i = 0 To 3 Step 1
            'Send Address of Cal Date
            B1 = EditSupply
            B2 = &HF+&H60
            B3 = &HAA+i

            'Send Command
            SendGpibCommand(EditSupply, Convert.ToString(Chr(B1)) & Convert.ToString(Chr(B2)) & Convert.ToString(Chr(B3)))

            Delay(0.2)

            'get Status Query
            B1 = EditSupply
            B2 = &H44
            B3 = 0

            'Send Command
            SendGpibCommand(EditSupply, Convert.ToString(Chr(B1)) & Convert.ToString(Chr(B2)) & Convert.ToString(Chr(B3)))

            Delay(0.2)

            Data = ReadInstrumentBuffer(EditSupply)
            CalDate &= Mid(Data, 5, 1)
        Next i

        frmAPS6062.txtLastCalDate.Text = Asc(Mid(CalDate, 3, 1)) & "/" & Asc(Mid(CalDate, 4, 1)) & "/" & Asc(Mid(CalDate, 1, 1)) & Asc(Mid(CalDate, 2, 1))
    End Sub

    Public Sub DisableFailsafePowerSupply(ByVal Supply As Short)
        '************************************************************
        '* Nomenclature   : 7081-PPU                                *
        '* Written By     : Randy Zeger                             *
        '*    DESCRIPTION:  (Maintenance Only)                      *
        '*     Used to Keep individual supply from "flashing" when  *
        '*     Slot 0 is tied up                                    *
        '*    EXAMPLE:                                              *
        '*     DisableFailsafePowerSupply EditSupply%               *
        '************************************************************
        Dim B1 As Short
        Dim B2 As Short
        Dim B3 As Short

        B1 = &H20 + Supply
        B2 = &H80
        B3 = 8

        'Send Command
        SendGpibCommand(Supply, Convert.ToString(Chr(B1)) & Convert.ToString(Chr(B2)) & Convert.ToString(Chr(B3)))
    End Sub

    Sub DisplayVisaErrorMsg(ByVal iSupply As Short, ByVal nErrorStatus As Integer)
        Dim sBuf As String = ""

        If nErrorStatus Then
            '        viStatusDesc instrumentHandle&(iSupply), nErrorStatus, sBuf
            MsgBox(sBuf, MsgBoxStyle.Exclamation, "VISA Error Message (DCPS" & Str(iSupply) & ")")
            If InStr(sTipMode, "TIP_Run") Then
                SetKey("TIPS", "STATUS", "Error from VISA (DCPS" & Str(iSupply) & "): " & Hex(nErrorStatus) & " " & sBuf)
                frmAPS6062.cmdQuit_Click()
            End If
        End If
    End Sub

    Sub WaitForReady(ByVal iSupply As Short)
        Dim iCnt As Short

        'Wait for hardware reset to finish, but not too long
        '(bytCommandReadStatus() has a 0.2 sec delay)
        iCnt = 0
        Do While bytCommandReadStatus(iSupply)=0
            iCnt += 1
            If iCnt>20 Then Exit Do
        Loop
    End Sub

    ' Convert this Integer into a binary string.
    Function IntToBinary(ByVal iValue As Integer) As String
        IntToBinary = ""

        Dim result_string As String = ""
        Dim digit_num As Short
        Dim factor As Short

        If iValue > 255 Then Exit Function
        ' Read the hexadecimal digits
        ' one at a time from right to left.
        factor = 128
        For digit_num = 0 To 7
            ' Convert the value into bits.

            If iValue And factor Then
                result_string &= "1"
            Else
                result_string &= "0"
            End If
            factor = factor \ 2

        Next digit_num

        ' Return the result.
        IntToBinary = result_string
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
        'Reqiires (2) Windows Api Functions
        'Declare Function GetPrivateProfileString Lib "kernel32" Alias "GetPrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As Any, ByVal lpDefault As String, ByVal lpReturnedString As String, ByVal nSize As Long, ByVal lpFileName As String) As Long
        'Declare Function WritePrivateProfileString Lib "kernel32" Alias "WritePrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As Any, ByVal lpString As Any, ByVal lpFileName As String) As Long

        Dim lpReturnedString As String = "" 'Return Buffer
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
        FileNameInfo = Trim(lpReturnedString.ToString())
        FileNameInfo = Mid(FileNameInfo, 1, Len(FileNameInfo)-1)
        'If File Locations Missing, then create empty keys
        If FileNameInfo=lpDefault+Convert.ToString(Chr(0)) Or FileNameInfo=lpDefault Then
            lpString = Trim(lpDefault)
            ReturnValue = WritePrivateProfileString(lpApplicationName, lpKeyName, lpString, lpFileName)
        End If

        'Return Information In INI File
        GatherIniFileInformation = FileNameInfo
    End Function

    Public Sub ReleaseDCPS()
        Dim status As Integer
        Dim XmlBuf As String
        Dim Response As String = Space(4096)

        'Release DCPS1
        XmlBuf = "<AtXmlDriverFunctionCall>" & _
                    "<SignalResourceName>DCPS_1</SignalResourceName>" & _
                    "<DriverFunctionCall>" & _
                        "<FunctionName>aps6062_reset</FunctionName>" & _
                        "<ReturnType>RetInt32</ReturnType>" & _
                        "<Parameter   ParamNumber=""1"" ParamType=""Handle"" Value=""0""/>" & _
                    "</DriverFunctionCall>" & _
                 "</AtXmlDriverFunctionCall>"
        status = atxml_IssueDriverFunctionCall(XmlBuf, Response, 4096)
        Threading.Thread.Sleep(50)

        'Release DCPS2
        XmlBuf = "<AtXmlDriverFunctionCall>" & _
                    "<SignalResourceName>DCPS_2</SignalResourceName>" & _
                    "<DriverFunctionCall>" & _
                        "<FunctionName>aps6062_reset</FunctionName>" & _
                        "<ReturnType>RetInt32</ReturnType>" & _
                        "<Parameter   ParamNumber=""1"" ParamType=""Handle"" Value=""0""/>" & _
                    "</DriverFunctionCall>" & _
                 "</AtXmlDriverFunctionCall>"
        status = atxml_IssueDriverFunctionCall(XmlBuf, Response, 4096)
        Threading.Thread.Sleep(50)

        'Release DCPS3
        XmlBuf = "<AtXmlDriverFunctionCall>" & _
                    "<SignalResourceName>DCPS_3</SignalResourceName>" & _
                    "<DriverFunctionCall>" & _
                        "<FunctionName>aps6062_reset</FunctionName>" & _
                        "<ReturnType>RetInt32</ReturnType>" & _
                        "<Parameter   ParamNumber=""1"" ParamType=""Handle"" Value=""0""/>" & _
                    "</DriverFunctionCall>" & _
                 "</AtXmlDriverFunctionCall>"
        status = atxml_IssueDriverFunctionCall(XmlBuf, Response, 4096)
        Threading.Thread.Sleep(30)

        'Release DCPS4
        XmlBuf = "<AtXmlDriverFunctionCall>" & _
                    "<SignalResourceName>DCPS_4</SignalResourceName>" & _
                    "<DriverFunctionCall>" & _
                        "<FunctionName>aps6062_reset</FunctionName>" & _
                        "<ReturnType>RetInt32</ReturnType>" & _
                        "<Parameter   ParamNumber=""1"" ParamType=""Handle"" Value=""0""/>" & _
                    "</DriverFunctionCall>" & _
                 "</AtXmlDriverFunctionCall>"
        status = atxml_IssueDriverFunctionCall(XmlBuf, Response, 4096)
        Threading.Thread.Sleep(50)

        'Release DCPS5
        XmlBuf = "<AtXmlDriverFunctionCall>" & _
                    "<SignalResourceName>DCPS_5</SignalResourceName>" & _
                    "<DriverFunctionCall>" & _
                        "<FunctionName>aps6062_reset</FunctionName>" & _
                        "<ReturnType>RetInt32</ReturnType>" & _
                        "<Parameter   ParamNumber=""1"" ParamType=""Handle"" Value=""0""/>" & _
                    "</DriverFunctionCall>" & _
                 "</AtXmlDriverFunctionCall>"
        status = atxml_IssueDriverFunctionCall(XmlBuf, Response, 4096)
        Threading.Thread.Sleep(50)

        'Release DCPS6
        XmlBuf = "<AtXmlDriverFunctionCall>" & _
                    "<SignalResourceName>DCPS_6</SignalResourceName>" & _
                    "<DriverFunctionCall>" & _
                        "<FunctionName>aps6062_reset</FunctionName>" & _
                        "<ReturnType>RetInt32</ReturnType>" & _
                        "<Parameter   ParamNumber=""1"" ParamType=""Handle"" Value=""0""/>" & _
                    "</DriverFunctionCall>" & _
                 "</AtXmlDriverFunctionCall>"
        status = atxml_IssueDriverFunctionCall(XmlBuf, Response, 4096)
        Threading.Thread.Sleep(50)

        'Release DCPS7
        XmlBuf = "<AtXmlDriverFunctionCall>" & _
                    "<SignalResourceName>DCPS_7</SignalResourceName>" & _
                    "<DriverFunctionCall>" & _
                        "<FunctionName>aps6062_reset</FunctionName>" & _
                        "<ReturnType>RetInt32</ReturnType>" & _
                        "<Parameter   ParamNumber=""1"" ParamType=""Handle"" Value=""0""/>" & _
                    "</DriverFunctionCall>" & _
                 "</AtXmlDriverFunctionCall>"
        status = atxml_IssueDriverFunctionCall(XmlBuf, Response, 4096)
        Threading.Thread.Sleep(50)

        'Release DCPS8
        XmlBuf = "<AtXmlDriverFunctionCall>" & _
                    "<SignalResourceName>DCPS_8</SignalResourceName>" & _
                    "<DriverFunctionCall>" & _
                        "<FunctionName>aps6062_reset</FunctionName>" & _
                        "<ReturnType>RetInt32</ReturnType>" & _
                        "<Parameter   ParamNumber=""1"" ParamType=""Handle"" Value=""0""/>" & _
                    "</DriverFunctionCall>" & _
                 "</AtXmlDriverFunctionCall>"
        status = atxml_IssueDriverFunctionCall(XmlBuf, Response, 4096)
        Threading.Thread.Sleep(50)

        'Release DCPS9
        XmlBuf = "<AtXmlDriverFunctionCall>" & _
                    "<SignalResourceName>DCPS_9</SignalResourceName>" & _
                    "<DriverFunctionCall>" & _
                        "<FunctionName>aps6062_reset</FunctionName>" & _
                        "<ReturnType>RetInt32</ReturnType>" & _
                        "<Parameter   ParamNumber=""1"" ParamType=""Handle"" Value=""0""/>" & _
                    "</DriverFunctionCall>" & _
                 "</AtXmlDriverFunctionCall>"
        status = atxml_IssueDriverFunctionCall(XmlBuf, Response, 4096)
        Threading.Thread.Sleep(50)

        'Release DCPS10
        XmlBuf = "<AtXmlDriverFunctionCall>" & _
                    "<SignalResourceName>DCPS_10</SignalResourceName>" & _
                    "<DriverFunctionCall>" & _
                        "<FunctionName>aps6062_reset</FunctionName>" & _
                        "<ReturnType>RetInt32</ReturnType>" & _
                        "<Parameter   ParamNumber=""1"" ParamType=""Handle"" Value=""0""/>" & _
                    "</DriverFunctionCall>" & _
                 "</AtXmlDriverFunctionCall>"
        status = atxml_IssueDriverFunctionCall(XmlBuf, Response, 4096)
        Threading.Thread.Sleep(200)
    End Sub
End Module